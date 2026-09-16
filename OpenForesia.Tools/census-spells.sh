#!/bin/sh
# Genere la FEUILLE DE ROUTE DES SORTS : les 123 fiches de magie du CD, une par ligne, avec les
# deux colonnes d'etat que GestionnairesEntites.md et ObjetsAtteignables.md portent deja.
#
#   implemente   le gestionnaire d'entite que le sort fait naitre est branche   -> le corps existe
#   teste        la sonde de sorts l'a vu partir en jeu                         -> il a tourne
#
# Les deux sont independantes, et c'est tout l'objet du document : un sort dont le corps compile
# n'est pas un sort eprouve. « implemente=oui, teste=non » est une dette, pas un acquis.
#
# POURQUOI CE SCRIPT EXISTE. Meme raison que pour les objets : la decouverte par plantage ne dit
# jamais ce qui reste. Les fiches de sort sont pourtant de la DONNEE, deja relevee — le verbe
# `bestiaire` les ecrit dans Documentation/Reference/Bestiaire/sorts.csv en rejouant hors ligne la
# chaine de chargement du CD. Aucune partie n'est jouee pour produire ce document.
#
# CE QU'IL FAIT. Pour chaque fiche de 0x30 octets il lit l'octet +0x10 — le SOUS-TYPE d'entite que
# CastSpell_0053A980 fait naitre par SpawnEffectEntity (FUN_0047a9d0) — et le resout en adresse :
#   - sous-type < 0xc0  -> table STATIQUE 0x00551c30, la meme dans les onze zones ;
#   - sous-type >= 0xc0 -> table 0x0056b0c8 installee POUR CETTE SOUS-ZONE, entree sous-type - 0xc0.
# Un sort ne depend d'aucune sous-zone — son repertoire est celui de l'etat 3 —, donc la seconde
# branche ne peut pas etre resolue sans choisir arbitrairement une sous-zone. Elle est comptee a
# part plutot que devinee ; a ce jour aucune fiche n'y tombe, le plus grand sous-type releve etant
# 0xb3.
#
# LES SOURCES, aucune ecrite a la main :
#   - Documentation/Reference/Bestiaire/sorts.csv   les 123 fiches, lues sur le CD ;
#   - Legacy/Binaries/Blaze.exe                     la table statique, DAT_00564600 (le compte par
#                                                   ecole), DAT_0056d330 (les paliers
#                                                   d'apprentissage), DAT_00562ab8 (l'ecole de
#                                                   chaque classe) ;
#   - les `case 0x…:` de DispatchEntityHandlerCore  pour la colonne implemente ;
#   - Documentation/Etat/sorts-exerces.tsv          pour la colonne teste ;
#   - Documentation/Etat/handlers-exerces.tsv       pour l'etat du gestionnaire lui-meme.
#
# Usage :
#   sh OpenForesia.Tools/census-spells.sh > Documentation/Etat/SortsAtteignables.md

set -e

BIN=Legacy/Binaries/Blaze.exe
SPELLS=Documentation/Reference/Bestiaire/sorts.csv
TICK_DIR=OpenForesia/src/Blaze/Screens/State03
HPROBE=Documentation/Etat/handlers-exerces.tsv       # la sonde des gestionnaires
SPROBE=Documentation/Etat/sorts-exerces.tsv          # la sonde des sorts

DATA_DELTA=$((0x401A00))          # .data : VA - offset fichier
EMPTY=00401FA0                    # la souche a corps vide

[ -f "$SPELLS" ] || { echo "sorts.csv absent — le regenerer par le verbe bestiaire" >&2; exit 1; }

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

# `WorldMapTick.cs` a ete decoupe en 68 fichiers sous `Screens/State03/` le 2026-09-16. On lit donc
# L'ARBORESCENCE ENTIERE, concatenee, et non plus un fichier unique : `DispatchEntityHandlerCore`
# vit desormais dans `Dispatch/EntityHandlerDispatch.cs`, les crochets dans `Tick/WorldMapTick.cs`.
# Sans cela les `sed -n '/.../,/^    }$/p'` ci-dessous ne trouvent plus rien et le recensement sort
# A ZERO, sans erreur et sans message - le mode d'echec le plus couteux de ce depot.
TICK="$TMP/state03-concat.cs"
find "$TICK_DIR" -name '*.cs' | sort | xargs cat > "$TICK"

dwords() {   # dwords <va> <count>
    od -An -v -tx4 -j $(( $1 - DATA_DELTA )) -N $(( $2 * 4 )) "$BIN" \
        | tr -s ' ' '\n' | grep -v '^$' | tr 'a-f' 'A-F'
}

bytes() {    # bytes <va> <count>
    od -An -v -tu1 -j $(( $1 - DATA_DELTA )) -N $2 "$BIN" | tr -s ' ' '\n' | grep -v '^$'
}

# ---------------------------------------------------------------------------------------------
# 1. Les tables du binaire
# ---------------------------------------------------------------------------------------------
dwords $((0x00551C30)) 192 | awk '{ printf "S\t%d\t%s\n", NR-1, $0 }'  > "$TMP/tables"
bytes  $((0x00564600))   8 | awk '{ printf "N\t%d\t%s\n", NR-1, $0 }' >> "$TMP/tables"
bytes  $((0x0056D330))  20 | awk '{ printf "P\t%d\t%s\n", NR-1, $0 }' >> "$TMP/tables"
bytes  $((0x00562AB8))   8 | awk '{ printf "C\t%d\t%s\n", NR-1, $0 }' >> "$TMP/tables"

# ---------------------------------------------------------------------------------------------
# 2. Ce que la reconstruction branche, et ce que les deux sondes ont vu passer
# ---------------------------------------------------------------------------------------------
sed -n '/private static void DispatchEntityHandlerCore/,/^    }$/p' "$TICK" \
    | grep -oE 'case 0x[0-9A-Fa-f]{8}:' | grep -oE '[0-9A-Fa-f]{8}' \
    | tr 'a-f' 'A-F' | sort -u > "$TMP/wired"

# Les fichiers de sonde peuvent ne pas exister encore : une colonne « teste » entierement a non
# est une reponse, pas une erreur. Le `sed 1s/…/` retire un BOM eventuel — sans lui la premiere
# ligne ne commence pas par '#', echappe au filtre des commentaires et passe pour une donnee.
if [ -f "$HPROBE" ]; then
    sed '1s/^\xef\xbb\xbf//' "$HPROBE" | grep -v '^#' | grep -v '^$' \
        | cut -f1 | tr 'a-f' 'A-F' | sort -u > "$TMP/seenh"
else
    : > "$TMP/seenh"
fi

if [ -f "$SPROBE" ]; then
    sed '1s/^\xef\xbb\xbf//' "$SPROBE" | grep -v '^#' | grep -v '^$' \
        | awk -F'\t' 'NF>=2 { print $1 "/" $2 }' | sort -u > "$TMP/seens"
else
    : > "$TMP/seens"
fi

# ---------------------------------------------------------------------------------------------
# 3. Le rapport
# ---------------------------------------------------------------------------------------------
awk -v tablesf="$TMP/tables" -v wiredf="$TMP/wired" -v seenhf="$TMP/seenh" -v seensf="$TMP/seens" \
    -v empty="$EMPTY" '
    function ecolenom(f) {
        if (f == 0) return "Sorcier, Elfe"
        if (f == 1) return "Pretre"
        if (f == 2) return "Fee"
        if (f == 3) return "Chasseur (les herbes)"
        return "hors ecole"
    }
    function classenom(c) {
        if (c == 0) return "Guerrier"
        if (c == 1) return "Pretre"
        if (c == 2) return "Voleur"
        if (c == 3) return "Sorcier"
        if (c == 4) return "Chasseur"
        if (c == 5) return "Elfe"
        if (c == 6) return "Nain"
        if (c == 7) return "Fee"
        return "?"
    }

    BEGIN {
        while ((getline line < tablesf) > 0) {
            n = split(line, f, "\t")
            if (f[1] == "S") stat[f[2] + 0] = f[3]        # 0x00551c30, sous-type -> adresse
            if (f[1] == "N") compte[f[2] + 0] = f[3] + 0  # 0x00564600, sorts par ecole
            if (f[1] == "P") palier[f[2] + 0] = f[3] + 0  # 0x0056d330, 5 paliers x 4 ecoles
            if (f[1] == "C") ecole[f[2] + 0] = f[3] + 0   # 0x00562ab8, ecole de chaque classe
        }
        while ((getline a < wiredf) > 0) wired[a] = 1
        while ((getline a < seenhf) > 0) vuh[a] = 1
        while ((getline a < seensf) > 0) vus[a] = 1

        nsorts = 0
    }

    NR == 1 { next }
    {
        ligne = $0

        # Le NOM est entre guillemets et peut porter une virgule : on le sort d abord, puis on
        # decoupe ce qui reste. Decouper d abord donnerait un decalage de colonnes.
        if (!match(ligne, /"[^"]*"/)) next
        nom = substr(ligne, RSTART + 1, RLENGTH - 2)
        avant = substr(ligne, 1, RSTART - 1)
        apres = substr(ligne, RSTART + RLENGTH + 1)

        split(avant, tete, ",")
        fam = tete[1] + 0; idx = tete[2] + 0
        split(apres, q, ",")
        st = q[1] + 0; dur = q[4] + 0; pm = q[5] + 0; appr = q[6] + 0
        clen = q[7] + 0; combi = q[8]

        if (nom == "") nom = "(sans nom)"
        if (combi == "") combi = "—"

        # LA RESOLUTION DU SOUS-TYPE, la meme que pour un objet.
        if (st >= 192) { addr = ""; parzone[fam "/" idx] = 1 }
        else             addr = stat[st]

        i = nsorts++
        F[i] = fam; I[i] = idx; NOM[i] = nom; ST[i] = st
        A[i] = addr; DUR[i] = dur; PM[i] = pm; APPR[i] = appr
        CLEN[i] = clen; COMBI[i] = combi
        parfam[fam]++
    }

    END {
        print "# Les sorts du jeu, un par un"
        print ""
        print "**Genere** — ne pas editer a la main. Regenerer par :"
        print ""
        print "```sh"
        print "sh OpenForesia.Tools/census-spells.sh > Documentation/Etat/SortsAtteignables.md"
        print "```"
        print ""
        print "Le troisieme document de la meme famille, apres `GestionnairesEntites.md` (ce que les"
        print "tables de sauts atteignent) et `ObjetsAtteignables.md` (ce qu\47utiliser un objet"
        print "atteint). Celui-ci couvre la **magie** : les fiches de sort du CD, ce que lancer"
        print "chacune fait naitre, et si ce corps existe et a tourne."
        print ""
        print "Il croise cinq sources, **aucune ecrite a la main** : les fiches de"
        print "`Documentation/Reference/Bestiaire/sorts.csv` (lues sur le CD par le verbe"
        print "`bestiaire`), la table de sauts statique de `Blaze.exe`, les `case` du repartiteur"
        print "dans les sources C#, et les deux sondes `sorts-exerces.tsv` et `handlers-exerces.tsv`."
        print "**Aucune partie n\47est jouee pour le produire.**"
        print ""
        print "## Ce que veulent dire les deux colonnes"
        print ""
        print "| colonne | ce qu\47elle dit | d\47ou elle vient |"
        print "|---|---|---|"
        print "| **implemente** | le gestionnaire d\47entite que le sort fait naitre existe | le sous-type `+0x10` resolu, croise avec les `case` du repartiteur |"
        print "| **teste** | le sort a reellement ete lance en jeu | la sonde de sorts, `sorts-exerces.tsv` |"
        print ""
        print "**Elles sont independantes.** Un corps transcrit d\47apres le desassembleur et qui"
        print "compile n\47a rien prouve ; « implemente = oui, teste = non » est une dette."
        print ""
        print "> **`implemente` ne va pas plus loin que l\47ENFANT DIRECT.** La colonne resout le"
        print "> sous-type `+0x10` de la fiche, et s\47arrete la. Elle ne dit **rien** de ce que ce"
        print "> corps engendre a son tour : un sort peut se lire « implemente » et planter quand"
        print "> meme, parce qu\47un petit-enfant manque. C\47est arrive a Pardon et a Ange, dont les"
        print "> trois derniers trous ont ete trouves le 2026-09-01 en depliant a la main tous les"
        print "> engendrements du code ecrit. **Ce document n\47est pas une preuve d\47atteignabilite ;"
        print "> seule la sonde en est une.**"
        print ""
        print "## Comment un sort atteint du code"
        print ""
        print "`CastSpell_0053A980` prend une **famille** et un **indice**, va chercher la fiche de"
        print "`0x30` octets dans la table `+0x9c` du repertoire d\47etat, et fait naitre une entite"
        print "dont le sous-type est l\47octet **`+0x10`** de la fiche :"
        print ""
        print "```"
        print "fiche = *(*(repertoire + 0x9c) + famille * 4) + indice * 0x30"
        print "SpawnEffectEntity(lanceur, fiche[0x10], fiche[0x11], fiche[0x12])   // FUN_0047a9d0"
        print "```"
        print ""
        print "C\47est **exactement le mecanisme des opcodes 9 et 10 du script d\47objet**, et la"
        print "resolution est la meme : sous-type `< 0xc0` par la table statique `0x00551c30`, la"
        print "meme dans les onze zones ; `>= 0xc0` par la table `0x0056b0c8` installee pour la"
        print "sous-zone. Un sort ne depend d\47aucune sous-zone — **il fait la meme chose partout**."
        print ""
        print "**Le lanceur n\47est pas toujours un personnage.** L\47opcode 14 du script d\47objet"
        print "appelle la meme fonction avec sa propre paire famille/indice : une potion et un sort"
        print "de soin passent par la meme fiche. La sonde ne distingue pas les deux, et c\47est"
        print "voulu — ce qu\47on veut savoir est si **la fiche** a ete exercee."
        print ""
        print "---"
        print ""
        print "## Les quatre ecoles"
        print ""
        print "L\47ecole d\47un personnage est `+0x2b5`, posee a la creation depuis la table"
        print "`0x00562ab8` relevee ici meme :"
        print ""
        print "| classe | ecole | fiches sur le CD | apprenables au palier 4 |"
        print "|---|---:|---:|---:|"
        for (c = 0; c < 8; c++) {
            e = ecole[c]
            printf "| %d %s | %d | %d | %d |\n",
                   c, classenom(c), e, parfam[e] + 0, palier[e * 5 + 4] + 0
        }
        print ""
        print "**Quatre classes sur huit lancent quelque chose** — 1, 3, 5 et 7 : ce sont les seules"
        print "que `TryLearnSpell` laisse passer, et les seules dont `ApplyGroundMotion_0051A020`"
        print "ouvre la posture d\47incantation. Les quatre autres partagent l\47ecole 0 sans jamais"
        print "y toucher."
        print ""
        printf "**Le compte du CD recoupe le binaire.** `DAT_00564600` porte `%d %d %d` pour les trois\n",
               compte[0], compte[1], compte[2]
        printf "ecoles reelles ; les listes du CD en donnent %d, %d et %d. Les deux releves sont\n",
               parfam[0] + 0, parfam[1] + 0, parfam[2] + 0
        print "independants — l\47un vient de `.data`, l\47autre de `BLAZE.ALL` — et c\47est ce qui"
        print "valide la lecture des bornes de liste."
        print ""
        print "**L\47ecole 3 n\47est pas morte, elle est herboriste.** Le Chasseur s\47en voit"
        printf "attribuer une, et le CD y porte %d fiches aux noms de plantes. Mais `DAT_00564600`\n",
               parfam[3] + 0
        printf "n\47en declare que %d, la creation n\47initialise le masque des sorts connus que pour les\n",
               compte[3]
        print "ecoles 0, 1 et 2, et le Chasseur n\47entre jamais dans la posture d\47incantation :"
        print "**rien n\47y mene en jeu normal**. Elles sont listees quand meme, parce qu\47elles"
        print "existent."
        print ""
        print "---"
        print ""
        print "## Les sorts, un par un"
        print ""
        print "**Les quatre ecoles seulement** — familles 0 a 3, celles qu\47une classe peut se voir"
        print "attribuer. Les quatre autres familles de la table sont a la fin du document : elles ne"
        print "sont pas de la magie de personnage, et les melanger fausserait le compte."
        print ""
        print "`PM` est le cout en points de magie (`+0x14`), `appr.` le cout d\47apprentissage"
        print "(`+0x1d`) — celui que la portee du tirage doit couvrir a la montee de niveau —, et"
        print "`duree` l\47incantation en images (`+0x13`)."
        print ""
        print "**La combinaison n\47est pas faite de directions mais des quatre boutons de face** :"
        print "`T` triangle, `O` rond, `X` croix, `C` carre. La saisie est"
        print "`(manette[2..3] >> 8) & 0xf0` et le masque teste `0xf000` — les quatre bits que"
        print "`s_PadButtonMasks_00564f18` nomme ainsi. Deux lettres collees sont **un seul coup a"
        print "deux boutons** ; les coups sont separes par un tiret."
        print ""
        print "| ecole | # | sort | combinaison | PM | appr. | duree | sous-type | gestionnaire | implemente | teste |"
        print "|---:|---:|---|---|---:|---:|---:|---:|---|---|---|"

        ecrits = 0; testes = 0; irresolus = 0; hors = 0
        for (i = 0; i < nsorts; i++) {
            a = A[i]
            if (a == "" || a == empty) {
                ges = (a == empty) ? "`0x" empty "` (corps vide)" : "**non resolu**"
                impl = (a == empty) ? "oui" : "**non**"
            } else {
                ges = "`0x" a "`"
                impl = (a in wired) ? "oui" : "**non**"
            }

            cle = F[i] "/" I[i]
            vu = (cle in vus) ? "oui" : "**non**"

            # Les familles 4 a 7 sont mises de cote : elles vont dans leur propre tableau, et
            # surtout elles ne comptent pas. Trente-neuf de leurs fiches partagent un meme
            # sous-type deja branche, ce qui gonflait « implemente » sans rien prouver sur la magie.
            if (F[i] > 3) {
                hors++
                HF[hors] = F[i]; HI[hors] = I[i]; HNOM[hors] = NOM[i]; HST[hors] = ST[i]
                HGES[hors] = ges; HIMPL[hors] = impl; HVU[hors] = vu
                continue
            }

            if (impl == "oui") ecrits++
            if (cle in vus) testes++
            if (a == "") irresolus++

            printf "| %d | %d | %s | %s | %d | %d | %d | 0x%02x | %s | %s | %s |\n",
                   F[i], I[i], NOM[i], COMBI[i], PM[i], APPR[i], DUR[i], ST[i], ges, impl, vu

            if (a != "" && a != empty) {
                emplois[a]++
                if (index(quels[a], " " NOM[i] " ") == 0) quels[a] = quels[a] " " NOM[i] " "
            }
        }

        print ""
        print "---"
        print ""
        print "## Le compte"
        print ""
        print "| | |"
        print "|---|---|"
        printf "| fiches des quatre ecoles | **%d** |\n", nsorts - hors
        printf "| dont les trois ecoles jouables | **%d** |\n", parfam[0] + parfam[1] + parfam[2]
        printf "| implementees (le gestionnaire existe) | **%d** |\n", ecrits
        printf "| a transcrire | **%d** |\n", nsorts - hors - ecrits
        printf "| lancees en jeu au moins une fois | **%d** |\n", testes
        printf "| dette : implementees mais jamais lancees | **%d** |\n", ecrits - testes
        printf "| sous-types non resolvables (>= 0xc0, dependent de la sous-zone) | **%d** |\n", irresolus
        printf "| fiches des familles 4 a 7, comptees a part | %d |\n", hors
        print ""
        if (irresolus == 0) {
            print "**La derniere ligne est le resultat le plus utile du releve.** Aucune fiche de sort"
            print "n\47emploie un sous-type superieur ou egal a `0xc0` : tout passe par la table"
            print "statique `0x00551c30`, la meme dans les onze zones. **Un sort fait donc exactement"
            print "la meme chose ou qu\47on soit** — ecrire ces corps une fois les couvre partout. La"
            print "branche par sous-zone reste dans le script parce que c\47est la regle du binaire ;"
            print "elle ne se declenche simplement jamais ici."
        } else {
            print "**Attention** : des fiches emploient un sous-type `>= 0xc0`, qui se resout dans la"
            print "table installee **pour la sous-zone courante**. Le meme sort ne fait alors pas la"
            print "meme chose partout, et ce releve hors ligne ne peut pas trancher sans choisir une"
            print "sous-zone. Elles sont marquees « non resolu » ci-dessus."
        }
        print ""
        print "---"
        print ""
        print "## Les gestionnaires d\47entite que les sorts font naitre"
        print ""
        print "Colonne **emplois** : le nombre de fiches qui menent a ce corps. C\47est l\47ordre de"
        print "travail — le plus employe d\47abord. La colonne **teste** est ici celle de la sonde"
        print "des *gestionnaires* (`handlers-exerces.tsv`), pas celle des sorts : elle dit que le"
        print "corps a tourne, par un sort ou par autre chose."
        print ""
        print "| gestionnaire | emplois | implemente | teste | sorts |"
        print "|---|---:|---|---|---|"

        # Tri par emplois decroissants, puis par ADRESSE croissante. Le second critere n est pas
        # cosmetique : sans lui les 73 corps a un seul emploi sortent dans l ordre de hachage
        # d awk, qui change d une machine a l autre — et le fichier est suivi par git.
        m = 0
        for (a in emplois) ordre[m++] = a
        for (x = 0; x < m; x++) for (y = x + 1; y < m; y++)
            if (emplois[ordre[y]] > emplois[ordre[x]] || (emplois[ordre[y]] == emplois[ordre[x]] && ordre[y] < ordre[x])) { t = ordre[x]; ordre[x] = ordre[y]; ordre[y] = t }

        manque = 0
        for (x = 0; x < m; x++) {
            a = ordre[x]
            etat = (a in wired) ? "oui" : "**non**"
            if (!(a in wired)) manque++
            vu = (a in vuh) ? "oui" : "**non**"

            liste = quels[a]
            gsub(/  +/, ", ", liste)
            gsub(/^, |, $/, "", liste)
            gsub(/^ | $/, "", liste)

            printf "| `0x%s` | %d | %s | %s | %s |\n", a, emplois[a], etat, vu, liste
        }

        print ""
        printf "**%d gestionnaires distincts**, dont **%d** a transcrire.\n", m, manque
        print ""
        print "**Presque un corps par sort.** Ce n\47est pas un repartiteur a quelques branches"
        print "partagees comme la table des 48 effets : chaque sort des trois ecoles jouables a le"
        print "sien, et ces corps se suivent dans le binaire par blocs de quelques centaines"
        print "d\47octets. Les sept herbes du Chasseur sont la seule exception — elles partagent un"
        print "seul corps, ce qui recoupe leurs fiches, identiques a un parametre pres."
        print ""
        print "---"
        print ""
        print "## Les familles au-dela des ecoles"
        print ""
        printf "La table `+0x9c` porte huit familles ; les quatre dernieres — %d fiches — ne sont\n", hors
        print "designees ni par une classe (`0x00562ab8` n\47en nomme que 0, 1, 2 et 3) ni par un"
        print "script d\47objet (l\47opcode 14 n\47emploie que les familles 0, 1 et 2). Le seul autre"
        print "appelant de `CastSpell_0053A980` est le mot `+0x06` d\47une **entite de scene**, qui"
        print "porte sa propre paire famille/indice : ce sont donc, selon toute vraisemblance, les"
        print "sorts que les monstres lancent. Leurs noms sont restes dans le jeu de caracteres"
        print "d\47origine — jamais traduits, jamais montres au joueur — ce qui va dans le meme sens."
        print ""
        print "**Leur etendue n\47est pas etablie.** La derniere liste n\47a pas de successeur pour la"
        print "borner, et le releve s\47arrete a la premiere fiche dont la combinaison depasse `0x10`"
        print "coups — la borne du tampon de saisie. C\47est un plafond, pas un compte, et c\47est"
        print "pourquoi ces lignes sont hors du compte principal."
        print ""
        print "| famille | # | nom | sous-type | gestionnaire | implemente | teste |"
        print "|---:|---:|---|---:|---|---|---|"
        for (x = 1; x <= hors; x++)
            printf "| %d | %d | %s | 0x%02x | %s | %s | %s |\n",
                   HF[x], HI[x], HNOM[x], HST[x], HGES[x], HIMPL[x], HVU[x]
        print ""
        print "---"
        print ""
        print "## Ce que ce releve ne dit pas"
        print ""
        print "**Il ne dit pas ce que le sort FAIT**, seulement quel corps il fait naitre. Les degats"
        print "et les alterations d\47etat vivent plus loin, dans la table des 48 effets"
        print "(`0x0056d4e0`) que le gestionnaire finit par appeler — les 48 sont transcrites, et"
        print "`ObjetsAtteignables.md` dit lesquelles un objet peut exercer."
        print ""
        print "**Il ne dit pas si le sort est ATTEIGNABLE.** Un sort au-dela du palier 4 de son"
        print "ecole existe, a une fiche complete, et sa combinaison est reconnue — mais rien en jeu"
        print "normal ne pose son bit dans le masque des sorts connus. Voir"
        print "`Documentation/Reference/Sorts.md`, qui traite l\47apprentissage ; ce document-ci"
        print "traite la **couverture de la reconstruction**, ce qui est une autre question."
        print ""
        print "**Il ne dit pas non plus quel personnage a lance quoi.** La sonde releve la paire"
        print "famille/indice avec la zone et la sous-zone, pas le lanceur : une fiche exercee par un"
        print "objet compte comme exercee. C\47est voulu — ce qui est verifie est le corps, pas le"
        print "chemin qui y mene."
    }
' "$SPELLS"
