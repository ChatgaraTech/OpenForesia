#!/bin/sh
# Genere LA CARTE DES GLOBALES d'une DLL d'aide : quels corps sont obliges de partir ensemble.
#
# POURQUOI CE SCRIPT EXISTE. `census-dll-surface.sh` dit ce qu'il faut transcrire ; il ne dit pas
# dans quel ORDRE c'est possible. La difference a coute deux lots a moitie : `InitTrigTables` et
# `DrawOTag` paraissaient des points d'entree comme les autres, et tous deux sont soudes a des
# dizaines de corps restes natifs par les globales qu'ils ecrivent. Porter l'un des deux seul
# laisserait la copie de la DLL figee et tout le chemin natif lirait des valeurs mortes — sans
# erreur, sans message, et sans rien qui le signale avant l'ecran.
#
# CE QU'IL FAIT. Il soude les corps par deux relations, puis sort les composantes :
#   1. un appel vers une fonction NON EXPORTEE — elle ne peut pas rester derriere ;
#   2. une globale qu'un corps ECRIT pendant qu'un autre la touche.
# Deux lecteurs d'une meme globale ne sont PAS soudes : c'est ce qui a permis de porter `rcos` et
# `rsin` en laissant `InitTrigTables` natif.
#
# Usage :
#   sh OpenForesia.Tools/census-dll-globals.sh winsys3d > Documentation/Etat/DLLGlobales.md
#   sh OpenForesia.Tools/census-dll-globals.sh wsyskern >> Documentation/Etat/DLLGlobales.md

set -e

DLL=${1:-winsys3d}
BIN=Legacy/Binaries
DEC=Legacy/Decompiled
SRC=OpenForesia/src
INTEROP=$SRC/Blaze/Interop

[ -f "$DEC/$DLL.c" ] || { echo "$DEC/$DLL.c absent" >&2; exit 1; }

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

u32() { od -An -v -tu4 -j "$2" -N 4 "$1" | tr -d ' \n'; }
u16() { od -An -v -tu2 -j "$2" -N 2 "$1" | tr -d ' \n'; }

# ---------------------------------------------------------------------------
# Les racines : ordinal -> adresse, par la table d'exports du PE. Meme lecture que dans
# `census-dll-surface.sh`, reduite a ce qui sert ici.
# ---------------------------------------------------------------------------
f="$BIN/$DLL.dll"
lfanew=$(u32 "$f" 60)
nsec=$(u16 "$f" $((lfanew + 6)))
optsize=$(u16 "$f" $((lfanew + 20)))
imagebase=$(u32 "$f" $((lfanew + 52)))
edir=$(u32 "$f" $((lfanew + 120)))

SECS=""
i=0
while [ "$i" -lt "$nsec" ]; do
    o=$((lfanew + 24 + optsize + i * 40))
    SECS="$SECS $(u32 "$f" $((o + 12))):$(u32 "$f" $((o + 16))):$(u32 "$f" $((o + 20)))"
    i=$((i + 1))
done

rva2off() {
    r=$1
    for s in $SECS; do
        va=${s%%:*}; rest=${s#*:}; rs=${rest%%:*}; pr=${rest#*:}
        if [ "$r" -ge "$va" ] && [ "$r" -lt $((va + rs)) ]; then echo $((pr + r - va)); return 0; fi
    done
    echo "RVA hors section" >&2; exit 1
}

eoff=$(rva2off "$edir")
obase=$(u32 "$f" $((eoff + 16)))
nfun=$(u32 "$f" $((eoff + 20)))
ofun=$(rva2off "$(u32 "$f" $((eoff + 28)))")
od -An -v -tu4 -j "$ofun" -N $((nfun * 4)) "$f" | tr -s ' ' '\n' | grep -v '^$' > "$TMP/fun.txt"

# Les ordinaux que la reconstruction importe encore.
grep -oE "EntryPoint = \"#[0-9]+\"" "$INTEROP/$DLL.cs" 2>/dev/null \
  | grep -oE '[0-9]+' | sort -un > "$TMP/importes.txt" || : > "$TMP/importes.txt"

awk -v obase="$obase" -v base="$imagebase" '
    FILENAME ~ /fun\.txt$/ { rva[FNR - 1] = $1; next }
    { want[$1] = 1 }
    END {
        for (i = 0; i in rva; i++) {
            o = obase + i
            if (!(o in want) || rva[i] == 0) continue
            a = sprintf("%x", base + rva[i])
            sub(/^0+/, "", a)
            if (a in deja) { nom[a] = nom[a] "/" o; continue }
            deja[a] = 1; nom[a] = o
        }
        for (a in nom) printf "%s\t%s\n", a, nom[a]
    }
' "$TMP/fun.txt" "$TMP/importes.txt" > "$TMP/racines.tsv"

# Les corps deja portes : meme forme canonique que `census-dll-surface.sh`.
cat > "$TMP/impl.awk" <<'AWKEOF'
{
    low = tolower($0)
    rest = low
    while (match(rest, /(winsys3d|wsyskern|playst|3dhelp|gldhelp)\.c:? +[a-z_][a-z0-9_]* @ 0x[0-9a-f]{6,8}/)) {
        m = substr(rest, RSTART, RLENGTH)
        rest = substr(rest, RSTART + RLENGTH)
        match(m, /^[a-z0-9]+/);        d = substr(m, RSTART, RLENGTH)
        match(m, /0x[0-9a-f]{6,8}$/);  a = substr(m, RSTART + 2, RLENGTH - 2)
        sub(/^0+/, "", a)
        if (a != "" && d == DLL) print a
    }
}
AWKEOF

find "$SRC" -name '*.cs' -not -path "*/Interop/*" -print0 \
  | xargs -0 cat 2>/dev/null \
  | awk -v DLL="$DLL" -f "$TMP/impl.awk" | sort -u > "$TMP/ports.tsv"

awk -v dll="$DLL" -f OpenForesia.Tools/census-dll-globals.awk \
    "$DEC/$DLL.c" "$TMP/ports.tsv" "$TMP/racines.tsv"
