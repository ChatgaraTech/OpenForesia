#!/bin/sh
# Releve QUELS gestionnaires d'entite de scene chaque sous-zone installe sur la table 0x0056ad48 —
# celle que la passe FUN_0050e680 aiguille, c'est-a-dire l'INTELLIGENCE DES ESPECES.
#
# Le chargement d'une zone choisit un bloc de 0x20 pointeurs dans PTR_PTR_00566d98, indexe par
# `zone * 0x10 + sous-zone`, et le recopie sur 0x0056ad48. Le meme relevé que
# dump-entity-vtables.sh fait pour generer EntityVTables.cs ; ici il est croise pour repondre a
# « combien de comportements distincts le jeu utilise-t-il, et ou ? ».
#
# Aucune valeur n'est ecrite a la main. Usage :
#   sh OpenForesia.Tools/census-ai-handlers.sh
#
# Sort trois blocs sur la sortie standard : le compte par gestionnaire, la carte par zone, et la
# liste des gestionnaires distincts. Voir Documentation/Ennemies/Comportement.md.

set -e
BIN=Legacy/Binaries/Blaze.exe
DATA_DELTA=$((0x401A00))          # .data : VA - offset
SEL_VA=$((0x00566d98))            # PTR_PTR_00566d98 -> 0x0056ad48
SEL_COUNT=176                     # 11 zones x 16 sous-zones
FATAL=0056B3C8                    # le bloc d'arret fatal : la sous-zone n'existe pas
EMPTY=00401FA0                    # la souche a corps vide

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

dwords() {   # dwords <va> <count>
    od -An -v -tx4 -j $(( $1 - DATA_DELTA )) -N $(( $2 * 4 )) "$BIN" | tr -s ' ' '\n' | grep -v '^$'
}

# --- les 176 selecteurs de la colonne 0 -----------------------------------------------------
dwords $SEL_VA $SEL_COUNT > "$TMP/sel"

# --- le bloc de 0x20 gestionnaires de chaque sous-zone reelle --------------------------------
: > "$TMP/use"
i=0
while read -r blk; do
    zone=$(( i / 16 ))
    sub=$(( i % 16 ))
    i=$(( i + 1 ))
    [ "$(echo "$blk" | tr 'a-f' 'A-F')" = "$FATAL" ] && continue

    dwords $(( 0x$blk )) 32 | awk -v z="$zone" -v s="$sub" \
        '{ printf "%s %d %d %d\n", toupper($0), z, s, NR - 1 }' >> "$TMP/use"
done < "$TMP/sel"

# --- 1. le compte par gestionnaire ------------------------------------------------------------
echo "== gestionnaires par nombre d'emplois (gestionnaire, emplois, zones) =="
awk -v empty="$EMPTY" '$1 != empty {
        n[$1]++
        if (!(($1 SUBSEP $2) in z)) { z[$1 SUBSEP $2] = 1; zl[$1] = zl[$1] " " $2 }
    }
    END { for (h in n) printf "0x%s %d %s\n", h, n[h], zl[h] }' "$TMP/use" | sort -k2,2nr -k1,1

echo
echo "== par zone : gestionnaires distincts =="
awk -v empty="$EMPTY" '$1 != empty { if (!(($2 SUBSEP $1) in seen)) { seen[$2 SUBSEP $1] = 1; c[$2]++; l[$2] = l[$2] " 0x" $1 } }
    END { for (zz = 0; zz <= 10; zz++) if (zz in c) printf "zone %2d : %2d gestionnaires :%s\n", zz, c[zz], l[zz] }' "$TMP/use"

echo
echo "== total =="
awk -v empty="$EMPTY" '$1 != empty { d[$1] = 1; u++ } END { printf "%d gestionnaires distincts, %d emplacements remplis\n", length(d), u }' "$TMP/use"

echo
echo "== emplacements a la souche vide (0x$EMPTY) =="
awk -v empty="$EMPTY" '$1 == empty { u++ } END { printf "%d\n", u + 0 }' "$TMP/use"
