#!/bin/sh
# Genere `Documentation/Etat/CheminAffichage.md` — la carte du dernier lot de `winsys3d`.
#
# POURQUOI CE SCRIPT EXISTE. `DLLGlobales.md` dit qu'il ne reste qu'une composante, la masse ;
# il ne dit pas ce qu'il y a dedans. Trois pistes successives ont ete ouvertes puis fermees a la
# main — `DAT_011be760` (mesure : detache un corps sur 425), le tampon seul (sa base est aussi
# celle du rejeu), le rejeu seul (sa cloture contient vingt producteurs). Ce script fixe le
# resultat pour qu'on ne refasse pas le chemin.
#
# Usage :
#   sh OpenForesia.Tools/census-display-path.sh > Documentation/Etat/CheminAffichage.md

set -e

DLL=winsys3d
BIN=Legacy/Binaries
DEC=Legacy/Decompiled
SRC=OpenForesia/src
INTEROP=$SRC/Blaze/Interop

[ -f "$DEC/$DLL.c" ] || { echo "$DEC/$DLL.c absent" >&2; exit 1; }

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

u32() { od -An -v -tu4 -j "$2" -N 4 "$1" | tr -d ' \n'; }
u16() { od -An -v -tu2 -j "$2" -N 2 "$1" | tr -d ' \n'; }

# --- les racines : ordinal -> adresse, par la table d'exports du PE -------------------------
# Meme lecture que `census-dll-globals.sh`, reduite a ce qui sert ici.
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

grep -oE "EntryPoint = \"#[0-9]+\"" "$INTEROP/$DLL.cs" 2>/dev/null \
  | grep -oE '[0-9]+' | sort -un > "$TMP/importes.txt" || : > "$TMP/importes.txt"

awk -v obase="$obase" -v base="$imagebase" '
    FILENAME ~ /fun\.txt$/ { rva[FNR - 1] = $1; next }
    { want[$1] = 1 }
    END {
        for (i = 0; i in rva; i++) {
            o = obase + i
            if (!(o in want) || rva[i] == 0) continue
            a = sprintf("%x", base + rva[i]); sub(/^0+/, "", a)
            if (a in deja) { nom[a] = nom[a] "/" o; continue }
            deja[a] = 1; nom[a] = o
        }
        for (a in nom) printf "%s\t%s\n", a, nom[a]
    }
' "$TMP/fun.txt" "$TMP/importes.txt" > "$TMP/racines.tsv"

# --- les corps deja transcrits : meme forme canonique que les autres recensements -----------
cat > "$TMP/impl.awk" <<'AWKEOF'
{
    low = tolower($0); rest = low
    while (match(rest, /winsys3d\.c:? +[a-z_][a-z0-9_]* @ 0x[0-9a-f]{6,8}/)) {
        m = substr(rest, RSTART, RLENGTH); rest = substr(rest, RSTART + RLENGTH)
        match(m, /0x[0-9a-f]{6,8}$/); a = substr(m, RSTART + 2, RLENGTH - 2)
        sub(/^0+/, "", a)
        if (a != "") print a
    }
}
AWKEOF

find "$SRC" -name '*.cs' -not -path "*/Interop/*" -print0 \
  | xargs -0 cat 2>/dev/null \
  | awk -f "$TMP/impl.awk" | sort -u > "$TMP/ports.tsv"

awk -f OpenForesia.Tools/census-display-path.awk \
    "$DEC/$DLL.c" "$TMP/racines.tsv" "$TMP/ports.tsv"
