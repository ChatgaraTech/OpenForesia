#!/bin/sh
# VERIFIE LES TABLES DE DESCRIPTEURS D'ATLAS contre le binaire, et mesure la marge de chaque site
# qui en installe une sur une entite.
#
# Pourquoi ce controle existe : une plage de `.data` transcrite TROP COURT ne se voit pas a la
# compilation. Le rendu lit `*(byte**)(e + 0x3c) + e[0x40] * 8`, donc il INDEXE EN AVANT a partir
# de l'adresse posee ; deborder rend huit octets de tas jamais ecrits, et le sprite sort avec une
# taille et des coordonnees aleatoires. Le defaut s'est produit trois fois :
#
#   2026-09-01  0x00551b00 arretee a 5 enregistrements  -> Boule de feu sans boule, eclats parasites
#   2026-09-01  0x00551528 arretee a 16 enregistrements -> les etoiles de Marteau, gigantesques
#
# Deux passes :
#   1. CONTENU  chaque tableau est relu dans Blaze.exe a son adresse ; DIFF = releve faux.
#   2. MARGE    pour chaque `*(byte**)(e + 0x3c) = Descriptors…() [+ N]`, combien d'enregistrements
#               restent apres N. Le rendu peut en consommer autant que le compteur d'images de
#               l'entite (+0x40, plafonne dans son corps) : une marge de 1 n'est SURE que si le
#               corps laisse +0x40 a zero. Les marges <= 2 sont signalees, a verifier a la main.
#
# Usage :
#   sh OpenForesia.Tools/check-descriptors.sh

set -e

BIN=Legacy/Binaries/Blaze.exe
SRC=OpenForesia/src
DELTA=$((0x401A00))          # .data : VA - offset fichier

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

# ---------------------------------------------------------------- 1. le contenu des tableaux
grep -rl 's_Descriptors_00[0-9A-F]\+ =$' --include=*.cs "$SRC" | while read -r f; do
    awk '
        /private static readonly byte\[\] s_Descriptors_00[0-9A-F]+ =/ {
            n=$0; sub(/.*s_Descriptors_/,"",n); sub(/ =.*/,"",n); cur=n; buf=""; next
        }
        cur!="" {
            if ($0 ~ /\];/) { print cur "\t" buf; cur=""; next }
            l=$0; sub(/\/\/.*/,"",l); gsub(/[ \t]/,"",l)
            k=split(l,a,","); for(i=1;i<=k;i++) if(a[i]~/^0x/) buf = buf substr(a[i],3) " "
        }' "$f"
done | sort -u > "$TMP/tables"

echo "## Contenu des tableaux"
echo
BAD=0
while IFS='	' read -r va bytes; do
    n=$(echo "$bytes" | wc -w)
    ours=$(echo "$bytes" | tr -d ' ' | tr 'A-F' 'a-f')
    real=$(od -An -v -tx1 -j $((0x$va - DELTA)) -N "$n" "$BIN" | tr -d ' \n')
    if [ "$ours" = "$real" ]; then st="ok  "; else st="DIFF"; BAD=$((BAD+1)); fi
    printf "%s 0x%s  %3d octets = %2d enregistrements, fin 0x%X\n" \
        "$st" "$va" "$n" $((n / 8)) $((0x$va + n))
    echo "$va $((n / 8))" >> "$TMP/len"
done < "$TMP/tables"

echo
echo "$BAD tableau(x) en desaccord avec le binaire."
echo

# ---------------------------------------------------------------- 2. la marge des sites d'appel
echo "## Marge des sites qui installent un descripteur"
echo
echo "fonction / tableau / decalage / enregistrements restants"
echo

awk '
    /private static [A-Za-z0-9_*]+ [A-Za-z_][A-Za-z0-9_]*_00[0-9A-F]+\(/ {
        fn=$0; sub(/.*static [^ ]+ /,"",fn); sub(/\(.*/,"",fn)
    }
    /\(e \+ 0x3c\) = Descriptors00[0-9A-F]+\(\)/ {
        b=$0; sub(/.*Descriptors/,"",b); sub(/\(\).*/,"",b)
        off=0
        if (match($0, /\(\) \+ (0x)?[0-9a-fA-F]+/)) {
            o=substr($0, RSTART, RLENGTH); sub(/.*\+ /,"",o); off=strtonum(o)
        }
        printf "%s\t%s\t%d\n", fn, b, off
    }' "$SRC/Blaze/Screens/State03/WorldMapTick.cs" | sort -u |
while IFS='	' read -r fn base off; do
    recs=$(awk -v n="$base" '$1==n{print $2}' "$TMP/len")
    left=$((recs - off / 8))
    flag=""
    [ "$left" -le 2 ] && flag="   <-- marge courte, verifier le plafond de +0x40"
    printf "%-34s 0x%s +0x%02x  %2d restant(s)%s\n" "$fn" "$base" "$off" "$left" "$flag"
done
