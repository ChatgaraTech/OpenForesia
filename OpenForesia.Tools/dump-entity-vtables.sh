#!/bin/sh
# Releve les sept tables de sauts d'entites de Blaze.exe et regenere
# OpenForesia/src/Blaze/Engine/EntityVTables.cs.
#
# Ghidra: la branche DAT_00600904 de FUN_0052a660 (Blaze.c 75118) choisit sept blocs dans sept
# tables de pointeurs contigues, indexees par `zone * 0x10 + sous-zone`, puis les recopie sur les
# sept tables actives 0x0056ad48 .. 0x0056b2c8.
#
#   PTR_PTR_00566d98  ->  0x0056ad48  (0x20 entrees)
#   PTR_PTR_00567058  ->  0x0056adc8  (0x40)
#   PTR_PTR_00567318  ->  0x0056aec8  (0x40)
#   PTR_PTR_005675d8  ->  0x0056afc8  (0x40)
#   PTR_PTR_00567898  ->  0x0056b0c8  (0x40)
#   PTR_PTR_00567b58  ->  0x0056b1c8  (0x40)
#   PTR_PTR_00567e18  ->  0x0056b2c8  (0x40)
#
# Aucune valeur n'est ecrite a la main : le fichier genere est un releve, pas une recopie.
# Usage : sh OpenForesia.Tools/dump-entity-vtables.sh > OpenForesia/src/Blaze/Engine/EntityVTables.cs

set -e
BIN=Legacy/Binaries/Blaze.exe
DATA_DELTA=$((0x401A00))          # .data : VA 0x0054F000 -> offset 0x0014D600
SEL_VA=$((0x00566d98))            # la premiere des sept tables de pointeurs
SEL_COUNT=176                     # 11 zones x 16 sous-zones
TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

dwords() {   # dwords <va> <count>
    od -An -v -tx4 -j $(( $1 - DATA_DELTA )) -N $(( $2 * 4 )) "$BIN" | tr -s ' ' '\n' | grep -v '^$'
}

dwords $SEL_VA $(( SEL_COUNT * 7 )) > "$TMP/sel"
awk -v n=$SEL_COUNT '{ c = int((NR - 1) / n); print c, $0 }' "$TMP/sel" > "$TMP/col"

# Taille d'un bloc : 0x20 s'il n'est cite que par la colonne 0, 0x40 sinon.
awk '{ seen[$2] = 1; if ($1 > 0) big[$2] = 1 }
     END { for (va in seen) print va, (va in big) ? 64 : 32 }' "$TMP/col" | sort > "$TMP/blocks"

while read -r va size; do
    dwords $(( 0x$va )) "$size" > "$TMP/b.$va"
done < "$TMP/blocks"

emit_list() {   # emit_list <fichier de valeurs> <indentation>
    awk -v pad="$2" '
        { buf = buf sprintf("0x%s, ", toupper($0)); if (++i % 8 == 0) { print pad substr(buf, 1, length(buf) - 1); buf = "" } }
        END { if (buf != "") print pad substr(buf, 1, length(buf) - 1) }' "$1"
}

cat <<'HEADER'
// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// FICHIER GENERE — ne pas editer a la main.
// Regenerer avec : sh OpenForesia.Tools/dump-entity-vtables.sh > OpenForesia/src/Blaze/Engine/EntityVTables.cs
//
// =================================================================================================
// LES SEPT TABLES DE SAUTS D'ENTITES, POUR LES ONZE ZONES (Ghidra: la branche DAT_00600904 de
// FUN_0052a660, Blaze.c 75118)
//
// Le chargement d'une zone choisit sept blocs dans sept tables de pointeurs contigues, indexees par
// `zone * 0x10 + sous-zone`, et les recopie sur les sept tables actives 0x0056ad48 .. 0x0056b2c8.
// C'est ce qui donne a chaque zone ses propres gestionnaires d'entite.
//
// Ces valeurs vivent en `.data` : elles ne sont a aucun prix dans `Blaze.c`, qui est une
// decompilation du CODE seul. Elles sont relevees dans le binaire par le script ci-dessus, et le
// releve est verifie : les sept blocs de l'index 0 sont identiques, entree pour entree, aux sept
// tables `s_VtSource_*` qui etaient ecrites a la main dans `Resources.cs` avant cette generation.
//
// Onze zones ont un contenu (0..10) ; le reste de chaque ligne de seize renvoie a 0x0056B3C8, un
// bloc de soixante-quatre `FUN_00536170` — **l'arret fatal**, et non la souche a corps vide : une
// sous-zone qui n'existe pas ne se traverse pas en silence, elle tue le processus.
// =================================================================================================

namespace OpenForesia.Blaze;

internal sealed unsafe partial class Resources
{
HEADER

i=0
for spec in "00566d98:0056ad48:0x20" "00567058:0056adc8:0x40" "00567318:0056aec8:0x40" \
            "005675d8:0056afc8:0x40" "00567898:0056b0c8:0x40" "00567b58:0056b1c8:0x40" \
            "00567e18:0056b2c8:0x40"; do
    sel=${spec%%:*}; rest=${spec#*:}; dst=${rest%%:*}; len=${rest#*:}
    echo "    /// <summary>Ghidra: PTR_PTR_$sel — les 176 blocs candidats pour"
    echo "    /// <c>s_Vt_$dst</c> ($len entrees), un par <c>zone * 0x10 + sous-zone</c>.</summary>"
    echo "    private readonly uint[] s_VtPtr_$sel ="
    echo "    ["
    awk -v c=$i '$1 == c { print $2 }' "$TMP/col" > "$TMP/sel.$i"
    awk 'NR % 16 == 1 { printf "        // zone %d\n", int((NR - 1) / 16) }
         { buf = buf sprintf("0x%s, ", toupper($0)); if (NR % 8 == 0) { print "        " substr(buf, 1, length(buf) - 1); buf = "" } }' "$TMP/sel.$i"
    echo "    ];"
    echo ""
    i=$(( i + 1 ))
done

cat <<'POOL'
    /// <summary>Les adresses des blocs cites par les sept tables ci-dessus, triees — la cle de
    /// <see cref="s_VtBlock"/>, cherchee par dichotomie.</summary>
    private readonly uint[] s_VtBlockVa =
    [
POOL
awk '{ print $1 }' "$TMP/blocks" > "$TMP/vas"
emit_list "$TMP/vas" "        "
cat <<'POOL2'
    ];

    /// <summary>Le contenu de chaque bloc, dans l'ordre de <see cref="s_VtBlockVa"/>.</summary>
    private readonly uint[][] s_VtBlock =
    [
POOL2

while read -r va size; do
    printf '        // 0x%s — %d entrees\n' "$(echo "$va" | tr 'a-f' 'A-F')" "$size"
    echo "        ["
    emit_list "$TMP/b.$va" "            "
    echo "        ],"
done < "$TMP/blocks"

echo "    ];"
echo "}"
