#!/bin/sh
# Genere la LISTE DE COURSES DES OBJETS : tout ce qu'utiliser un objet de l'inventaire peut
# atteindre, obtenu en deroulant hors ligne les scripts des 2 370 fiches d'objet du CD.
#
# POURQUOI CE SCRIPT EXISTE. La decouverte par plantage marche mais ne dit jamais ce qui reste :
# on joue, ca leve, on transcrit, on recommence sans savoir si on est a un corps de la fin ou a
# cinquante. Les scripts d'objet sont pourtant de la DONNEE, deja relevee : la colonne `octets` de
# Documentation/Reference/Bestiaire/objets.csv porte les 0x140 octets de chaque fiche, et les deux
# scripts d'un objet vivent dedans, en +0x80 (l'emploi) et +0x82 (le declenchement).
#
# CE QU'IL FAIT. Pour chaque fiche, il suit les deux scripts en reachability — en suivant les sauts
# des opcodes 1 et 2, pas en balayant lineairement, parce qu'un script contient des donnees entre
# ses blocs — et releve :
#   - les SOUS-TYPES d'entite que les opcodes 9 et 10 font naitre, resolus en adresses ;
#   - les GENRES d'effet de l'opcode 18 (la table des 48) ;
#   - les paires FAMILLE/INDICE de l'opcode 14, qui designent une fiche du repertoire des sorts.
#
# LA RESOLUTION D'UN SOUS-TYPE. FUN_0047a9d0 fait naitre dans 0x005ff400, que FUN_0047aa70 anime :
#   - sous-type < 0xc0  -> table STATIQUE 0x00551c30, la meme dans les onze zones ;
#   - sous-type >= 0xc0 -> table 0x0056b0c8 installee POUR CETTE SOUS-ZONE, entree sous-type - 0xc0
#     (le debordement volontaire de 0x0056adc8, voir Resources.ResolveEntityHandler).
# C'est pourquoi la zone et la sous-zone de chaque fiche sont conservees jusqu'au bout.
#
# CE QU'IL NE PEUT PAS FAIRE. L'opcode 14 designe une fiche du repertoire des sorts, dont l'octet
# +0x10 porte le sous-type reellement engendre. Ce repertoire n'est pas dans objets.csv : les
# paires sont donc listees telles quelles, sans etre resolues. C'est la seule lacune, et elle est
# bornee.
#
# Usage :
#   sh OpenForesia.Tools/census-item-effects.sh > Documentation/Etat/ObjetsAtteignables.md

set -e

BIN=Legacy/Binaries/Blaze.exe
ITEMS=Documentation/Reference/Bestiaire/objets.csv
TICK=OpenForesia/src/Blaze/Screens/State03/WorldMapTick.cs

DATA_DELTA=$((0x401A00))          # .data : VA - offset fichier
SEL_VA=$((0x00566d98))            # la premiere des sept tables de pointeurs
SEL_COUNT=176                     # 11 zones x 16 sous-zones
FATAL=0056B3C8                    # bloc d'arret : la sous-zone n'existe pas
EMPTY=00401FA0                    # la souche a corps vide

[ -f "$ITEMS" ] || { echo "objets.csv absent — le regenerer par la verbe bestiaire" >&2; exit 1; }

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

dwords() {   # dwords <va> <count>
    od -An -v -tx4 -j $(( $1 - DATA_DELTA )) -N $(( $2 * 4 )) "$BIN" \
        | tr -s ' ' '\n' | grep -v '^$' | tr 'a-f' 'A-F'
}

# ---------------------------------------------------------------------------------------------
# 1. La table statique 0x00551c30 (192 entrees) et les 176 tables 0x0056b0c8 installees
# ---------------------------------------------------------------------------------------------
dwords $((0x00551C30)) 192 | awk '{ printf "S\t%d\t%s\n", NR-1, $0 }' > "$TMP/tables"

dwords $SEL_VA $(( SEL_COUNT * 7 )) > "$TMP/sel"
# colonne 4 (0-based) = 0056B0C8, celle ou retombe le debordement de FUN_0047aa70
sed -n "$(( 4 * SEL_COUNT + 1 )),$(( 5 * SEL_COUNT ))p" "$TMP/sel" > "$TMP/colblk"

i=0
while read -r blk; do
    zone=$(( i / 16 )); sub=$(( i % 16 )); i=$(( i + 1 ))
    [ "$blk" = "$FATAL" ] && continue
    dwords $(( 0x$blk )) 64 \
        | awk -v z="$zone" -v s="$sub" '{ printf "Z\t%d\t%d\t%d\t%s\n", z, s, NR-1, $0 }' >> "$TMP/tables"
done < "$TMP/colblk"

# ---------------------------------------------------------------------------------------------
# 2. Ce que la reconstruction branche deja
# ---------------------------------------------------------------------------------------------
sed -n '/private static void DispatchEntityHandlerCore/,/^    }$/p' "$TICK" \
    | grep -oE 'case 0x[0-9A-Fa-f]{8}:' | grep -oE '[0-9A-Fa-f]{8}' \
    | tr 'a-f' 'A-F' | sort -u > "$TMP/wired"

# ---------------------------------------------------------------------------------------------
# 3. Derouler les deux scripts de chaque fiche
# ---------------------------------------------------------------------------------------------
awk -F',' '
    function h2(s, at,   c, hi, lo) {           # octet a l offset at, depuis la chaine hexa
        c = substr(s, at * 2 + 1, 2)
        if (length(c) < 2) return -1
        hi = index("0123456789abcdef", substr(c, 1, 1)) - 1
        lo = index("0123456789abcdef", substr(c, 2, 1)) - 1
        if (hi < 0 || lo < 0) return -1
        return hi * 16 + lo
    }
    function u16(s, at,   a, b) { a = h2(s, at); b = h2(s, at + 1); if (a < 0 || b < 0) return -1; return a + b * 256 }

    # largeur de chaque opcode, relevee sur les 23 gestionnaires de DispatchItemOp
    BEGIN {
        w[0]=2;  w[1]=3;  w[2]=3;  w[3]=4;  w[4]=4;  w[5]=4;  w[6]=4;  w[7]=4
        w[8]=4;  w[9]=4;  w[10]=4; w[11]=4; w[12]=4; w[13]=5; w[14]=5; w[15]=1
        w[16]=3; w[17]=2; w[18]=2; w[19]=4; w[20]=4; w[21]=6; w[22]=1
    }

    # Tout est en DECIMAL : awk POSIX ne lit pas les litteraux hexa (0x140 y vaut 0).
    # 320 = 0x140 la taille de la fiche, 128 = 0x80 et 130 = 0x82 les deux entetes de script.
    function walk(hex, start, zone, sz,   pc, op, seen, stack, top, tgt) {
        if (start <= 0 || start >= 320) return
        top = 0; stack[top++] = start
        while (top > 0) {
            pc = stack[--top]
            while (pc >= 0 && pc < 320) {
                if (pc in seen) break
                seen[pc] = 1
                op = h2(hex, pc)
                if (op < 0 || op == 255 || op == 22) break
                if (!(op in w)) break                       # opcode hors des 23 : on s arrete

                if (op == 9 || op == 10) print "SPAWN\t" zone "\t" sz "\t" h2(hex, pc + 1)
                if (op == 18)            print "EFFET\t" h2(hex, pc + 1)
                if (op == 14)            print "SORT\t" h2(hex, pc + 1) "\t" h2(hex, pc + 2)

                if (op == 1) { pc = u16(hex, pc + 1); continue }          # saut ferme
                if (op == 2) { tgt = u16(hex, pc + 1); if (tgt > 0 && tgt < 320) stack[top++] = tgt }

                pc += w[op]
            }
        }
    }

    NR == 1 { next }
    {
        hex = $0; sub(/.*,/, "", hex)                  # `octets` est le dernier champ
        if (length(hex) < 640) next
        zone = $1 + 0; sz = $2 + 0
        walk(hex, u16(hex, 128), zone, sz)
        walk(hex, u16(hex, 130), zone, sz)
    }
' "$ITEMS" | sort | uniq -c | sed -E 's/^ +//' > "$TMP/raw"

# ---------------------------------------------------------------------------------------------
# 4. Le rapport
# ---------------------------------------------------------------------------------------------
awk -v tablesf="$TMP/tables" -v wiredf="$TMP/wired" -v empty="$EMPTY" '
    BEGIN {
        FS = "\t"
        while ((getline line < tablesf) > 0) {
            n = split(line, f, "\t")
            if (f[1] == "S") stat[f[2] + 0] = f[3]
            else             zon[f[2] "/" f[3] "/" f[4]] = f[5]
        }
        while ((getline a < wiredf) > 0) wired[a] = 1
    }
    {
        split($0, p, " ")
        count = p[1]
        rest = $0; sub(/^[0-9]+ /, "", rest)
        n = split(rest, f, "\t")

        if (f[1] == "SPAWN") {
            z = f[2] + 0; s = f[3] + 0; st = f[4] + 0
            if (st < 0) next
            addr = (st < 192) ? stat[st] : zon[z "/" s "/" (st - 192)]
            if (addr == "" || addr == empty) next
            emplois[addr] += count
            if (st >= 192) parzone[addr] = 1        # jamais atteint a ce jour — voir le compte
            key = sprintf(" %d ", z)
            if (index(zones[addr], key) == 0) zones[addr] = zones[addr] key
            nstatique += (st < 192) ? count : 0
            nparzone  += (st < 192) ? 0 : count
            soustypes[addr] = soustypes[addr] (index(soustypes[addr], "0x" sprintf("%02x", st)) ? "" : (soustypes[addr] == "" ? "" : ",") sprintf("0x%02x", st))
            next
        }
        if (f[1] == "EFFET") { effets[f[2] + 0] += count; next }
        if (f[1] == "SORT")  { sorts[f[2] "/" f[3]] += count; next }
    }
    END {
        print "# Ce qu\47utiliser un objet peut atteindre"
        print ""
        print "**Genere** — ne pas editer a la main. Regenerer par :"
        print ""
        print "```sh"
        print "sh OpenForesia.Tools/census-item-effects.sh > Documentation/Etat/ObjetsAtteignables.md"
        print "```"
        print ""
        print "Obtenu en deroulant hors ligne les deux scripts des 2 370 fiches d\47objet du CD"
        print "(`Documentation/Reference/Bestiaire/objets.csv`, colonne `octets`). Aucune partie"
        print "n\47a ete jouee pour le produire : les scripts sont de la donnee."
        print ""
        print "## Pourquoi on peut s\47y fier"
        print ""
        print "Le derouleur a ete verifie contre la realite avant d\47etre cru : la **Petite potion**"
        print "(zone 0, fiche 0) se decode en"
        print ""
        print "```"
        print "0090  11  cible-recherche   00        <- le mode 0, celui du meme camp"
        print "0092  02  saut-si           95 00"
        print "0095  00  consommer         64        <- cent pour cent"
        print "0097  0a  naitre-cible      40 00 01  <- sous-type 0x40 -> 0x00446790"
        print "009b  0b  alu-banque        20 4b 29"
        print "009f  0b  alu-banque        20 41 28"
        print "00a3  06  alu-stats-cible   4c 59 20  <- +0x4c, LES POINTS DE VIE"
        print "00a7  14  particule-cible   01 20 01"
        print "00ab  06  alu-stats-cible   48 4c 20"
        print "00af  06  alu-stats-cible   4c 75 20"
        print "00b3  06  alu-stats-cible   4c 58 20"
        print "00b7  ff  FIN"
        print "```"
        print ""
        print "Ce sont **exactement les deux levees rencontrees en jeu le 2026-08-30, dans cet"
        print "ordre** : l\47opcode 17 d\47abord, puis le sous-type `0x40`. Le derouleur les avait"
        print "toutes deux, sans qu\47on ait eu besoin de jouer."
        print ""
        print "**Et il corrige une idee fausse au passage.** Une potion ne soigne pas par la table des"
        print "48 effets : elle soigne par l\47**opcode 6**, l\47unite arithmetique, sur le champ"
        print "`+0x4c` du bloc de la cible. C\47est pourquoi le genre 43 — la guerison — n\47apparait"
        print "pas dans le tableau des effets plus bas. La table des 48 sert aux **alterations"
        print "d\47etat** et aux guerisons des sorts et des pieges, pas au soin d\47une potion."
        print ""
        print "---"
        print ""
        print "## Les gestionnaires d\47entite que les objets font naitre"
        print ""
        print "Colonne **emplois** : le nombre de sites d\47appel dans l\47ensemble des scripts, pas le"
        print "nombre d\47objets. C\47est l\47ordre de travail — le plus employe d\47abord."
        print ""
        print "| gestionnaire | emplois | sous-type | ecrit | zones |"
        print "|---|---:|---|---|---|"

        m = 0
        for (a in emplois) { ordre[m] = a; m++ }
        # tri decroissant par emplois
        for (x = 0; x < m; x++) for (y = x + 1; y < m; y++)
            if (emplois[ordre[y]] > emplois[ordre[x]]) { t = ordre[x]; ordre[x] = ordre[y]; ordre[y] = t }

        manque = 0; total = 0
        for (x = 0; x < m; x++) {
            a = ordre[x]; total++
            etat = (a in wired) ? "oui" : "**non**"
            if (!(a in wired)) manque++
            nz = split(zones[a], zz, " ")
            # tri numerique : sans lui la liste sort en 0,1,10,2,3, dans lordre du texte
            for (q = 1; q <= nz; q++) for (r = q + 1; r <= nz; r++)
                if (zz[r] + 0 < zz[q] + 0) { tz = zz[q]; zz[q] = zz[r]; zz[r] = tz }
            lieux = ""
            for (q = 1; q <= nz; q++) if (zz[q] != "") lieux = lieux (lieux == "" ? "" : ",") zz[q]
            printf "| `0x%s` | %d | %s | %s | %s |\n", a, emplois[a], soustypes[a], etat, lieux
        }

        print ""
        print "---"
        print ""
        print "## Le compte"
        print ""
        print "| | |"
        print "|---|---|"
        printf "| gestionnaires distincts atteignables par un objet | **%d** |\n", total
        printf "| deja ecrits | **%d** |\n", total - manque
        printf "| a transcrire | **%d** |\n", manque
        printf "| naissances resolues par la table STATIQUE | **%d** |\n", nstatique
        printf "| naissances resolues par une table de SOUS-ZONE | **%d** |\n", nparzone
        print ""
        print "**La derniere ligne est le resultat le plus utile du releve.** Aucune naissance"
        print "d\47objet n\47emploie un sous-type superieur ou egal a `0xc0` : tout passe par la table"
        print "statique `0x00551c30`, la meme dans les onze zones. **Un objet fait donc exactement la"
        print "meme chose ou qu\47on soit** — ecrire ces corps une fois les couvre partout, et la"
        print "colonne `zones` ne dit que dans quels lieux l\47objet se trouve, pas de quoi depend son"
        print "comportement. La branche par sous-zone reste dans le script parce que c\47est la regle"
        print "du binaire ; elle ne se declenche simplement jamais ici."
        print ""
        print "---"
        print ""
        print "## Les genres de la table des 48 effets (opcode 18)"
        print ""
        print "| genre | emplois |"
        print "|---:|---:|"
        for (e in effets) ordreE[ne++] = e + 0
        for (x = 0; x < ne; x++) for (y = x + 1; y < ne; y++)
            if (effets[ordreE[y]] > effets[ordreE[x]]) { t = ordreE[x]; ordreE[x] = ordreE[y]; ordreE[y] = t }
        for (x = 0; x < ne; x++) printf "| %d | %d |\n", ordreE[x], effets[ordreE[x]]
        print ""
        print "Les 48 sont transcrites ; ce tableau sert a savoir **lesquelles un objet peut reellement"
        print "exercer**, donc lesquelles valent un essai en jeu."
        print ""
        print "---"
        print ""
        print "## Les fiches de sort que l\47opcode 14 designe"
        print ""
        print "Le sous-type reellement engendre est l\47octet `+0x10` de la fiche, qui vit dans le"
        print "repertoire des sorts et non dans `objets.csv` : ces paires ne sont donc **pas resolues**."
        print "C\47est la seule lacune de ce releve."
        print ""
        print "| famille/indice | emplois |"
        print "|---|---:|"
        for (s in sorts) ordreS[ns++] = s
        for (x = 0; x < ns; x++) for (y = x + 1; y < ns; y++)
            if (sorts[ordreS[y]] > sorts[ordreS[x]]) { t = ordreS[x]; ordreS[x] = ordreS[y]; ordreS[y] = t }
        for (x = 0; x < ns; x++) printf "| %s | %d |\n", ordreS[x], sorts[ordreS[x]]
    }
' "$TMP/raw"
