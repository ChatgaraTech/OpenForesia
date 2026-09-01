#!/bin/sh
# Genere la FEUILLE DE ROUTE des gestionnaires d'entite : tout ce que le jeu peut atteindre par une
# table de sauts, groupe par zone, avec deux colonnes d'etat.
#
#   implemente   un `case` du repartiteur branche cette adresse         -> le corps existe
#   teste        la sonde l'a vu passer en jeu                          -> le corps a tourne
#
# Les deux sont independantes, et c'est tout l'objet du document : un corps transcrit d'apres le
# desassembleur et qui compile n'est pas un corps eprouve. « implemente=oui, teste=non » est une
# dette, pas un acquis.
#
# Les sources, aucune ecrite a la main :
#   - les SEPT tables installees par sous-zone (0x0056ad48 .. 0x0056b2c8), lues pour les 176
#     couples (zone, sous-zone) via les sept selecteurs contigus a 0x00566d98 ;
#   - les TROIS tables statiques (0x00551c30, 0x00551f30, 0x00552230), 192 entrees chacune,
#     valables partout ;
#   - les `case 0x…:` de DispatchEntityHandlerCore et de CallSubAreaMoveProbe, et les `0x… =>` de
#     ScriptOpAreaVerb ;
#
# CallSubAreaMoveProbe a ete ajoute le 2026-09-01 : le creneau 63 de la table 0x0056afc8 n'est PAS
# appele comme un gestionnaire d'entite — il prend trois arguments, pas un — et son aiguillage vit
# donc dans sa propre fonction. Sans cette ligne, 0x00541550 serait reste marque « a transcrire »
# alors que le corps est ecrit et branche.
#   - Documentation/Etat/handlers-exerces.tsv, ecrit par la sonde des qu'un couple est nouveau.
#
# Usage :
#   sh OpenForesia.Tools/census-entity-handlers.sh > Documentation/Etat/GestionnairesEntites.md

set -e

BIN=Legacy/Binaries/Blaze.exe
TICK=OpenForesia/src/Blaze/Screens/State03/WorldMapTick.cs
SCRIPT=OpenForesia/src/Blaze/Screens/State03/WorldMapScript.cs
PROBE=Documentation/Etat/handlers-exerces.tsv

DATA_DELTA=$((0x401A00))          # .data : VA - offset fichier
SEL_VA=$((0x00566d98))            # la premiere des sept tables de pointeurs
SEL_COUNT=176                     # 11 zones x 16 sous-zones
FATAL=0056B3C8                    # bloc d'arret : la sous-zone n'existe pas
EMPTY=00401FA0                    # la souche a corps vide

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

dwords() {   # dwords <va> <count>
    od -An -v -tx4 -j $(( $1 - DATA_DELTA )) -N $(( $2 * 4 )) "$BIN" \
        | tr -s ' ' '\n' | grep -v '^$' | tr 'a-f' 'A-F'
}

# =============================================================================================
# 1. Les sept tables installees, pour les 176 sous-zones  ->  handler TAB table TAB zone TAB sous
# =============================================================================================
set -- 0056AD48 0056ADC8 0056AEC8 0056AFC8 0056B0C8 0056B1C8 0056B2C8

dwords $SEL_VA $(( SEL_COUNT * 7 )) > "$TMP/sel"

: > "$TMP/use"
col=0
while [ $col -lt 7 ]; do
    eval "name=\${$(( col + 1 ))}"
    size=32
    [ $col -gt 0 ] && size=64

    sed -n "$(( col * SEL_COUNT + 1 )),$(( (col + 1) * SEL_COUNT ))p" "$TMP/sel" > "$TMP/colblk"

    i=0
    while read -r blk; do
        zone=$(( i / 16 ))
        sub=$(( i % 16 ))
        i=$(( i + 1 ))
        [ "$blk" = "$FATAL" ] && continue

        dwords $(( 0x$blk )) $size \
            | awk -v t="$name" -v z="$zone" -v s="$sub" \
                  '{ printf "%s\t%s\t%d\t%d\n", $0, t, z, s }' >> "$TMP/use"
    done < "$TMP/colblk"

    col=$(( col + 1 ))
done

# =============================================================================================
# 2. Les trois tables statiques : zone -1, sous-zone -1
# =============================================================================================
for st in 00551C30 00551F30 00552230; do
    dwords $(( 0x$st )) 192 | awk -v t="$st" '{ printf "%s\t%s\t-1\t-1\n", $0, t }' >> "$TMP/use"
done

sort -u "$TMP/use" | awk -F'\t' -v empty="$EMPTY" '$1 != empty' > "$TMP/use.s"

# =============================================================================================
# 3. Ce que la reconstruction branche, et ce qu'une partie a exerce
# =============================================================================================
{
    sed -n '/private static void DispatchEntityHandlerCore/,/^    }$/p' "$TICK" \
        | grep -oE 'case 0x[0-9A-Fa-f]{8}:'
    sed -n '/private static void CallSubAreaMoveProbe/,/^    }$/p' "$TICK" \
        | grep -oE 'case 0x[0-9A-Fa-f]{8}:'
    sed -n '/private static byte\* ScriptOpAreaVerb/,/^    }$/p' "$SCRIPT" \
        | grep -oE '0x[0-9A-Fa-f]{8} =>'
} | grep -oE '[0-9A-Fa-f]{8}' | tr 'a-f' 'A-F' | sort -u > "$TMP/wired"

if [ -f "$PROBE" ]; then
    grep -v '^#' "$PROBE" | grep -v '^$' | awk -F'\t' 'NF>=3 { print $1 "\t" $2 "\t" $3 }' \
        | tr 'a-f' 'A-F' | sort -u > "$TMP/seen"
else
    : > "$TMP/seen"
fi

cut -f1 "$TMP/seen" | sort -u > "$TMP/seen.any"

# =============================================================================================
# 4. Le rapport
# =============================================================================================
awk -F'\t' -v empty="$EMPTY" -v wiredf="$TMP/wired" -v seenf="$TMP/seen" -v anyf="$TMP/seen.any" '
    function nom(z) {
        if (z ==  0) return "L\47auberge"
        if (z ==  1) return "La mine abandonnee"
        if (z ==  2) return "La montagne du dragon de feu"
        if (z ==  3) return "La foret"
        if (z ==  4) return "La vieille tour"
        if (z ==  5) return "Le Labyrinthe des morts"
        if (z ==  6) return "Le chateau de l\47immortel"
        if (z ==  7) return "Les ruines souterraines"
        if (z ==  8) return "La vallee d\47argent"
        if (z ==  9) return "Les ruines dans le lac"
        if (z == 10) return "La grotte scellee"
        return "?"
    }
    function oui(b) { return b ? "oui" : "**non**" }

    # --- les trois entrees ---
    FILENAME == wiredf { wired[$1] = 1; next }
    FILENAME == seenf  { seenHere[$1 "\t" $2 "\t" $3] = 1; next }
    FILENAME == anyf   { seenAny[$1] = 1; next }

    # --- le releve : handler, table, zone, sous-zone ---
    {
        h = $1; t = $2; z = $3 + 0; s = $4 + 0

        key = z "\t" h
        if (!(key in vu)) { vu[key] = 1; liste[z] = liste[z] " " h }

        if (s >= 0) {
            if (index(" " subs[key] " ", " " s " ") == 0)
                subs[key] = subs[key] (subs[key] ? "," : "") s
            if (seenHere[h "\t" z "\t" s]) testeIci[key] = 1
        }
        # LES TABLES STATIQUES SE TESTENT PARTOUT. Elles portent zone -1 / sous-zone -1, alors que
        # la sonde note toujours une zone reelle : le rapprochement par triplet ne pouvait donc
        # JAMAIS aboutir, et la section des tables statiques affichait « 0 testes » quoi qu il
        # arrive — 274 gestionnaires en angle mort. Une table gravee dans .data est la meme dans les
        # onze zones : l avoir vue passer quelque part suffit a la dire exercee.
        else if (z < 0 && seenAny[h]) testeIci[key] = 1

        if (index(" " tables[key] " ", " " t " ") == 0)
            tables[key] = tables[key] (tables[key] ? " " : "") t

        tousH[h] = 1
    }

    END {
        # --- le compte global ---
        for (h in tousH) {
            total++
            if (seenAny[h]) nbTeste++
            else if (wired[h]) nbEcrit++
            else nbAbsent++
        }

        print "# Feuille de route des gestionnaires d\47entite"
        print ""
        print "**Genere** — ne pas editer a la main. Regenerer par :"
        print ""
        print "```sh"
        print "sh OpenForesia.Tools/census-entity-handlers.sh > Documentation/Etat/GestionnairesEntites.md"
        print "```"
        print ""
        print "Le script ne recopie aucune valeur : il lit les tables dans `Blaze.exe`, les `case` du"
        print "repartiteur dans les sources, et le releve de la sonde dans `handlers-exerces.tsv`."
        print ""
        print "---"
        print ""
        print "## Comment lire"
        print ""
        print "**Zone et sous-zone ne sont pas la meme chose.** Une **zone** est un *lieu* — la foret,"
        print "la mine — et il y en a onze. Une **sous-zone** est une *carte* dans ce lieu, seize au"
        print "plus par zone : c\47est ce que l\47ecran montre. Les sept tables de sauts sont **rechargees"
        print "a chaque sous-zone**, donc un numero de sous-type ne veut rien dire tout seul."
        print ""
        print "**Les deux colonnes sont independantes.**"
        print ""
        print "| colonne | ce qu\47elle dit | d\47ou elle vient |"
        print "|---|---|---|"
        print "| **implemente** | le corps existe et est branche | un `case` du repartiteur |"
        print "| **teste** | le corps a reellement tourne en jeu | la sonde, dans cette zone |"
        print ""
        print "`implemente=oui, teste=non` est une **dette** : un corps transcrit d\47apres le"
        print "desassembleur et qui compile n\47a rien prouve. Rien ne signale une lecture d\47offset de"
        print "travers tant que le code n\47a pas tourne."
        print ""
        print "**Les quatre combinaisons se lisent comme un ordre de travail.**"
        print ""
        print "| implemente | teste | ce que ca veut dire |"
        print "|---|---|---|"
        print "| non | **oui** | **la priorite** : le jeu y va vraiment, et il plante en y allant |"
        print "| non | non | a ecrire un jour ; rien ne prouve encore qu\47on y passe |"
        print "| oui | non | une **dette** : le corps existe et n\47a jamais tourne |"
        print "| oui | oui | acquis |"
        print ""
        print "La ligne `non / oui` n\47est possible que parce que la sonde note l\47adresse **avant**"
        print "d\47appeler : un gestionnaire absent est donc enregistre par le plantage meme qu\47il cause."
        print ""
        print "---"
        print ""
        print "## Le compte"
        print ""
        print "| | |"
        print "|---|---|"
        printf "| gestionnaires distincts atteignables | **%d** |\n", total
        printf "| testes en jeu | **%d** |\n", nbTeste
        printf "| implementes, jamais appeles | **%d** |\n", nbEcrit
        printf "| a transcrire | **%d** |\n", nbAbsent
        print ""
        print "La souche a corps vide `0x" empty "` est exclue : elle occupe la majorite des creneaux"
        print "et n\47est pas un comportement."
        print ""
        print "---"
        print ""
        print "## L\47avancement par zone"
        print ""
        print "| zone | lieu | gestionnaires | implementes | testes |"
        print "|---|---|---|---|---|"

        for (z = -1; z <= 10; z++) {
            if (!(z in liste)) continue
            n = split(liste[z], hs, " ")
            ci = 0; ct = 0
            for (k = 1; k <= n; k++) {
                if (wired[hs[k]]) ci++
                if (testeIci[z "\t" hs[k]]) ct++
            }
            compte[z] = n; impl[z] = ci; test[z] = ct

            if (z == -1) printf "| — | **tables statiques** (partout) | %d | %d | %d |\n", n, ci, ct
            else printf "| %d | %s | %d | %d | %d |\n", z, nom(z), n, ci, ct
        }

        print ""
        print "Un gestionnaire employe dans plusieurs zones compte dans chacune : le tableau se lit"
        print "« ce qui m\47attend si je vais la », pas « ce qui n\47existe qu\47ici ». Les totaux par zone"
        print "depassent donc le total global."
        print ""
        print "---"

        for (z = -1; z <= 10; z++) {
            if (!(z in liste)) continue

            print ""
            if (z == -1) {
                print "## Tables statiques — valables dans toutes les zones"
                print ""
                print "Les trois tables gravees dans `.data` que les passes `0x0047aa70`, `0x0047ab70` et"
                print "`0x0047acf0` lisent pour un indice sous `0xc0`. Elles ne dependent d\47aucune zone."
            } else {
                printf "## Zone %d — %s\n", z, nom(z)
            }
            printf "\n%d gestionnaires, %d implementes, %d testes.\n\n", compte[z], impl[z], test[z]

            if (z == -1) print "| gestionnaire | tables | implemente | teste |"
            else         print "| gestionnaire | sous-zones | tables | implemente | teste |"
            if (z == -1) print "|---|---|---|---|"
            else         print "|---|---|---|---|---|"

            n = split(liste[z], hs, " ")
            # tri : a faire d\47abord (non implementes), puis a eprouver, puis faits
            for (rang = 0; rang <= 2; rang++) {
                for (k = 1; k <= n; k++) {
                    h = hs[k]; key = z "\t" h
                    w = wired[h] ? 1 : 0
                    t = testeIci[key] ? 1 : 0
                    r = (!w) ? 0 : (!t ? 1 : 2)
                    if (r != rang) continue

                    if (z == -1)
                        printf "| `0x%s` | %s | %s | %s |\n", h, tables[key], oui(w), oui(t)
                    else
                        printf "| `0x%s` | %s | %s | %s | %s |\n", \
                               h, subs[key], tables[key], oui(w), oui(t)
                }
            }
        }

        print ""
        print "---"
        print ""
        print "## Les tables"
        print ""
        print "| table | qui la lit |"
        print "|---|---|"
        print "| `0x0056AD48` | `FUN_0050e680`, les entites de scene — **l\47intelligence des especes** |"
        print "| `0x0056ADC8` | `FUN_0050e5e0` (table `0x005fa5e0`) et `FUN_0047aa70` au-dela de `0xc0` |"
        print "| `0x0056AEC8` | `ScriptOpAreaVerb`, **les verbes de zone**, et `FUN_0047ab70` au-dela de `0xc0` |"
        print "| `0x0056AFC8` | `FUN_0047acf0` (table `0x005f2d40`) au-dela de `0xc0` |"
        print "| `0x0056B0C8` | le debordement de `0x0056ADC8` |"
        print "| `0x0056B1C8` | le debordement de `0x0056AEC8` |"
        print "| `0x0056B2C8` | le debordement de `0x0056AFC8` |"
        print "| `0x00551C30` | la table **statique** de `FUN_0047aa70` |"
        print "| `0x00551F30` | la table statique de `FUN_0047ab70` |"
        print "| `0x00552230` | la table statique de `FUN_0047acf0` |"
    }
' "$TMP/wired" "$TMP/seen" "$TMP/seen.any" "$TMP/use.s"
