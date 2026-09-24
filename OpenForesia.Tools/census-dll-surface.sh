#!/bin/sh
# Genere LA FEUILLE DE ROUTE DES DLL : pour chacune des cinq DLL d'aide de 1998, ce que la
# reconstruction en appelle VRAIMENT, et donc ce qu'il faudrait transcrire pour s'en passer.
#
#   employe      la reconstruction importe ce point d'entree          -> il est atteint
#   implemente   un corps C# le porte, hors de Interop/               -> il est ecrit
#
# POURQUOI CE SCRIPT EXISTE. Les cinq decompilations pesent 139 770 lignes, et la tentation est de
# lire ce nombre comme le devis du chantier. Il est faux d'un ordre de grandeur : le jeu n'appelle
# qu'une poignee de points d'entree, et la cloture de leurs appels ne couvre qu'une fraction de
# chaque DLL. Le reste est du code que `Blaze.exe` n'atteint jamais. Ce document mesure cette
# fraction au lieu de la deviner, et se regenere — c'est ce qui a rendu les 1 430 gestionnaires
# d'entite tenables, pour exactement la meme raison.
#
# CE QU'IL FAIT, en quatre temps :
#   1. il lit la TABLE D'EXPORTS de chaque .dll directement dans le binaire (pas de `dotnet`, pas
#      de table ecrite a la main) : ordinal -> RVA -> nom. C'est ce qui resout les ordinaux que le
#      commentaire de corps de Ghidra ne porte pas — 24 des 81 employes, dont les souches vides ;
#   2. il decoupe la decompilation en fonctions sur les en-tetes `/* nom @ adresse */`, et releve
#      les appels de chaque corps ;
#   3. il prend pour RACINES les `[DllImport]` de `OpenForesia/src/Blaze/Interop/`, resolus par la
#      table d'exports, et calcule la CLOTURE par parcours en largeur ;
#   4. il marque `implemente` toute fonction dont l'adresse est citee sous `OpenForesia/src/` en
#      dehors de `Interop/`, sur une ligne qui nomme aussi la decompilation d'origine.
#
# LA CONVENTION DE TRACABILITE que le point 4 suppose. Elle prolonge le `// Ghidra: FUN_… @ 0x…`
# que le depot applique deja a `Blaze.c`, en y nommant la DLL : chaque corps de DLL transcrit porte
#
#       // Ghidra: winsys3d.c FUN_00a05060 @ 0x00a05060
#
# Les trois pieces ENSEMBLE — la decompilation d'origine, le nom Ghidra, l'adresse. C'est ce qui
# distingue une transcription d'une simple mention : le depot cite ces fonctions en prose a longueur
# de commentaire, et une regle plus lache comptait 14 corps portes la ou il y en avait 5.
#
# LA BASE DE CHARGEMENT n'est pas devinee : elle sort de l'`ImageBase` du PE, et le script VERIFIE
# qu'elle redonne l'adresse du point d'entree lue dans le `.c` (`/* entry @ ... */`). Si les deux
# divergent, il s'arrete — une base fausse ferait sortir un recensement A ZERO, sans erreur et sans
# message, le mode d'echec le plus couteux de ce depot.
#
# LES SOURCES, aucune ecrite a la main :
#   - Legacy/Binaries/*.dll                  les tables d'exports, les bases, les dependances ;
#   - Legacy/Decompiled/*.c                  les corps, leurs tailles et leurs appels ;
#   - OpenForesia/src/Blaze/Interop/*.cs     les racines : ce que la reconstruction importe ;
#   - OpenForesia/src/**/*.cs hors Interop/  la colonne implemente ;
#   - Legacy/Decompiled/Blaze.c              les ordinaux appeles mais pas encore importes.
#
# Usage :
#   sh OpenForesia.Tools/census-dll-surface.sh > Documentation/Etat/DLLAtteignables.md

set -e

BIN=Legacy/Binaries
DEC=Legacy/Decompiled
SRC=OpenForesia/src
INTEROP=$SRC/Blaze/Interop

DLLS="wsyskern winsys3d 3dhelp gldhelp playst"

[ -f "$BIN/winsys3d.dll" ] || { echo "Legacy/Binaries absent — lancer depuis la racine du depot" >&2; exit 1; }

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

u32() { od -An -v -tu4 -j "$2" -N 4 "$1" | tr -d ' \n'; }
u16() { od -An -v -tu2 -j "$2" -N 2 "$1" | tr -d ' \n'; }

rva2off() {
    r=$1
    for s in $SECS; do
        va=${s%%:*}; rest=${s#*:}; rs=${rest%%:*}; pr=${rest#*:}
        if [ "$r" -ge "$va" ] && [ "$r" -lt $((va + rs)) ]; then echo $((pr + r - va)); return 0; fi
    done
    echo "RVA 0x$(printf %x "$r") hors section" >&2
    exit 1
}

# ---------------------------------------------------------------------------
# 1. La table d'exports, lue dans le PE.
#
# Sort quatre colonnes : ordinal, RVA, nom (`-` pour un export par ordinal seul), et `FWD` quand la
# RVA retombe DANS le repertoire d'exports — c'est alors une chaine « AUTREDLL.fonction » et non du
# code, donc pas une racine transcriptible.
# ---------------------------------------------------------------------------
exports() {                       # exports <fichier.dll> ; ecrit aussi $TMP/base et $TMP/entryrva
    f=$1
    lfanew=$(u32 "$f" 60)
    nsec=$(u16 "$f" $((lfanew + 6)))
    optsize=$(u16 "$f" $((lfanew + 20)))
    entryrva=$(u32 "$f" $((lfanew + 40)))
    imagebase=$(u32 "$f" $((lfanew + 52)))
    edir=$(u32 "$f" $((lfanew + 120)))
    esize=$(u32 "$f" $((lfanew + 124)))

    SECS=""
    i=0
    while [ "$i" -lt "$nsec" ]; do
        o=$((lfanew + 24 + optsize + i * 40))
        SECS="$SECS $(u32 "$f" $((o + 12))):$(u32 "$f" $((o + 16))):$(u32 "$f" $((o + 20)))"
        i=$((i + 1))
    done

    echo "$imagebase" > "$TMP/base"
    echo "$entryrva"  > "$TMP/entryrva"

    eoff=$(rva2off "$edir")
    obase=$(u32 "$f" $((eoff + 16)))
    nfun=$(u32 "$f" $((eoff + 20)))
    nnam=$(u32 "$f" $((eoff + 24)))
    ofun=$(rva2off "$(u32 "$f" $((eoff + 28)))")
    onam=$(rva2off "$(u32 "$f" $((eoff + 32)))")
    oord=$(rva2off "$(u32 "$f" $((eoff + 36)))")

    od -An -v -tu4 -j "$ofun" -N $((nfun * 4)) "$f" | tr -s ' ' '\n' | grep -v '^$' > "$TMP/fun.txt"
    od -An -v -tu4 -j "$onam" -N $((nnam * 4)) "$f" | tr -s ' ' '\n' | grep -v '^$' > "$TMP/nam.txt"
    od -An -v -tu2 -j "$oord" -N $((nnam * 2)) "$f" | tr -s ' ' '\n' | grep -v '^$' > "$TMP/onm.txt"
    od -An -v -tu1 -j "$eoff" -N "$esize"      "$f" | tr -s ' ' '\n' | grep -v '^$' > "$TMP/blob.txt"

    awk -v obase="$obase" -v edir="$edir" -v esize="$esize" -v nnam="$nnam" '
        FILENAME ~ /fun\.txt$/  { fun[FNR - 1] = $1; next }
        FILENAME ~ /nam\.txt$/  { nam[FNR - 1] = $1; next }
        FILENAME ~ /onm\.txt$/  { ono[FNR - 1] = $1; next }
        FILENAME ~ /blob\.txt$/ { blob[FNR - 1] = $1; next }
        END {
            for (i = 0; i < nnam; i++) {
                p = nam[i] - edir; s = ""
                while ((p in blob) && blob[p] != 0) { s = s sprintf("%c", blob[p]); p++ }
                names[ono[i]] = s
            }
            for (i = 0; i in fun; i++) {
                if (fun[i] == 0) continue
                fwd = (fun[i] >= edir && fun[i] < edir + esize) ? "FWD" : "-"
                printf "%d\t%d\t%s\t%s\n", obase + i, fun[i], (i in names ? names[i] : "-"), fwd
            }
        }
    ' "$TMP/fun.txt" "$TMP/nam.txt" "$TMP/onm.txt" "$TMP/blob.txt"
}

# ---------------------------------------------------------------------------
# 2. Le graphe d'appels de la decompilation.
#
# Une fonction commence a `/* nom @ adresse */` et court jusqu'au prochain en-tete. On releve sa
# taille en lignes et l'ensemble des identifiants suivis d'une parenthese. Les mots-cles du C et
# les pseudo-fonctions de Ghidra (CONCAT44, SUB84, ZEXT…) sont ecartes : ce ne sont pas des appels.
# ---------------------------------------------------------------------------
cat > "$TMP/graph.awk" <<'AWKEOF'
/^\/\* [A-Za-z_][A-Za-z0-9_@$]* @ [0-9a-f]+ \*\/$/ {
    cur = $2; addr = $4
    lines[cur] = 0; at[cur] = addr; order[++n] = cur
    next
}
{
    if (cur == "") next
    lines[cur]++
    line = $0
    while (match(line, /[A-Za-z_][A-Za-z0-9_]*\(/)) {
        callee = substr(line, RSTART, RLENGTH - 1)
        line = substr(line, RSTART + RLENGTH)
        if (callee == cur) continue
        if (callee ~ /^(if|while|for|switch|return|sizeof|do|else|case)$/) continue
        if (callee ~ /^(CONCAT|SUB|ZEXT|SEXT|NAN|ABS|POPCOUNT|SBORROW|CARRY)[0-9]*$/) continue
        key = cur SUBSEP callee
        if (!(key in seen)) { seen[key] = 1; calls[cur] = calls[cur] " " callee }
    }
}
END {
    for (i = 1; i <= n; i++) {
        f = order[i]
        printf "FN\t%s\t%s\t%d\t%s\n", f, at[f], lines[f], calls[f]
    }
}
AWKEOF

# ---------------------------------------------------------------------------
# 3. Les racines : ce que `Interop/` importe.
#
# Une declaration court de `[DllImport(` au `;` qui la ferme. `EntryPoint = "..."` l'emporte sur le
# nom de la methode C# quand il est present — `WsysFree` importe l'export `Free`, et rien dans le
# nom C# ne le dit.
# ---------------------------------------------------------------------------
cat > "$TMP/roots.awk" <<'AWKEOF'
/\[DllImport\(/ { acc = ""; inside = 1 }
inside { acc = acc " " $0 }
inside && /;/ {
    inside = 0
    if (match(acc, /DllImport\("[^"]+"/) == 0) next
    dll = substr(acc, RSTART + 11, RLENGTH - 12)
    sub(/\.(dll|ax)$/, "", dll)
    if (match(acc, /EntryPoint *= *"[^"]+"/)) {
        ep = substr(acc, RSTART, RLENGTH)
        sub(/^EntryPoint *= *"/, "", ep); sub(/"$/, "", ep)
    } else if (match(acc, /extern[^(]+\(/)) {
        ep = substr(acc, RSTART, RLENGTH)
        sub(/ *\($/, "", ep)
        sub(/^.*[ *]/, "", ep)
    } else next
    if (ep != "") printf "%s\t%s\n", dll, ep
}
AWKEOF

awk -f "$TMP/roots.awk" "$INTEROP"/*.cs | sort -u > "$TMP/roots.tsv"

# ---------------------------------------------------------------------------
# 4. La colonne `implemente` — voir LA CONVENTION DE TRACABILITE en tete de fichier.
# ---------------------------------------------------------------------------
# Ce programme vit dans un heredoc et non entre apostrophes simples, comme les deux precedents :
# une apostrophe francaise dans un commentaire refermerait la chaine du shell, et awk recevrait un
# programme tronque. La panne se lit « must contain complete functions or rules », loin de sa cause.
cat > "$TMP/impl.awk" <<'AWKEOF'
# Seule LA FORME CANONIQUE compte — `<dll>.c <nom> @ 0x<adresse>`, les trois pieces ensemble.
#
# Une regle plus lache a ete essayee : « l'adresse figure sur une ligne qui nomme la DLL ». Elle
# comptait 14 corps portes la ou il y en avait 5. La prose du depot cite abondamment ces fonctions
# pour EXPLIQUER quelque chose — `SyntheticDevice.cs` mentionne `FUN_00a03460` precisement pour
# dire qu'il n'est atteint par aucun chemin. Une mention n'est pas une transcription, et un
# recensement qui confond les deux se felicite d'un travail qui n'existe pas.
#
# Exiger ` @ 0x…` APRES le nom tranche : c'est une declaration de paternite, pas une reference.
{
    low = tolower($0)
    rest = low
    while (match(rest, /(winsys3d|wsyskern|playst|3dhelp|gldhelp)\.c:? +[a-z_][a-z0-9_]* @ 0x[0-9a-f]{6,8}/)) {
        m = substr(rest, RSTART, RLENGTH)
        rest = substr(rest, RSTART + RLENGTH)
        match(m, /^[a-z0-9]+/);        dll = substr(m, RSTART, RLENGTH)
        match(m, /0x[0-9a-f]{6,8}$/);  a = substr(m, RSTART + 2, RLENGTH - 2)
        sub(/^0+/, "", a)
        if (a != "") printf "%s\t%s\n", dll, a
    }
}
AWKEOF

find "$SRC" -name '*.cs' -not -path "*/Interop/*" -print0 \
  | xargs -0 cat 2>/dev/null \
  | awk -f "$TMP/impl.awk" | sort -u > "$TMP/impl.tsv"

# Les amorces de CRT, avec leur preuve. Voir `crt-dll.txt` : Ghidra laisse une grande part de la
# bibliotheque standard en `FUN_…` anonyme, et rien de mecanique ne l'en distingue.
grep -v '^#' OpenForesia.Tools/crt-dll.txt | awk 'NF >= 2 { printf "%s\t%s\n", $1, $2 }' > "$TMP/crt.tsv"

# Les ordinaux que `Blaze.c` appelle. `Blaze.exe` n'importe par ordinal que chez winsys3d — ses
# imports de wsyskern sont tous nommes —, donc `Ordinal_NNN` y designe winsys3d sans ambiguite.
grep -ohE '\bOrdinal_[0-9]+\b' "$DEC/Blaze.c" | sed 's/Ordinal_//' | sort -un > "$TMP/blazeord.txt"

# ---------------------------------------------------------------------------
# 5. Le graphe de dependances, releve dans les binaires eux-memes.
# ---------------------------------------------------------------------------
: > "$TMP/deps.tsv"
for d in Blaze.exe winsys3d.dll wsyskern.dll playst.dll 3dhelp.dll gldhelp.dll; do
    tr -c '[:print:]' '\n' < "$BIN/$d" \
      | grep -iE '^(winsys3d|wsyskern|playst|3dhelp|gldhelp|glide2x|ir41_32)\.(dll|ax)$' \
      | tr 'A-Z' 'a-z' | sort -u \
      | while read -r dep; do
            [ "$dep" = "$(echo "$d" | tr 'A-Z' 'a-z')" ] && continue
            printf '%s\t%s\n' "$d" "$dep" >> "$TMP/deps.tsv"
        done
done

# ---------------------------------------------------------------------------
# 6. Le releve par DLL : exports, graphe, verification de la base.
# ---------------------------------------------------------------------------
for d in $DLLS; do
    exports "$BIN/$d.dll" > "$TMP/$d.exports.tsv"
    base=$(cat "$TMP/base"); entryrva=$(cat "$TMP/entryrva")

    want=$(printf '%08x' $((base + entryrva)))
    got=$(grep -oE '^/\* entry @ [0-9a-f]+ \*/$' "$DEC/$d.c" | head -1 | awk '{ print $4 }')
    if [ -n "$got" ] && [ "$want" != "$got" ]; then
        echo "$d : base de chargement incoherente — le PE donne $want, $d.c dit $got" >&2
        exit 1
    fi

    awk -f "$TMP/graph.awk" "$DEC/$d.c" > "$TMP/$d.graph.tsv"
    echo "$base" > "$TMP/$d.base"

    awk -v dll="$d" -v base="$base" -v mode=sum -f OpenForesia.Tools/census-dll-surface.awk \
        "$TMP/$d.exports.tsv" "$TMP/$d.graph.tsv" "$TMP/roots.tsv" "$TMP/impl.tsv" \
        "$TMP/crt.tsv" "$TMP/blazeord.txt" \
        >> "$TMP/summary.tsv"
done

# Les exports NOMMES de toutes les DLL reunis : c'est ce qui permet de dire, d'un appel sortant,
# qu'il retombe chez une soeur plutot que dans Win32 — la colonne « chez ».
: > "$TMP/sisters.tsv"
for d in $DLLS; do
    awk -F'\t' -v dll="$d" '$3 != "-" { printf "%s\t%s\n", dll, $3 }' "$TMP/$d.exports.tsv" >> "$TMP/sisters.tsv"
done

# ---------------------------------------------------------------------------
# 7. Le document.
# ---------------------------------------------------------------------------
cat <<'MDEOF'
# Les DLL d'aide, et ce que la reconstruction en atteint

**Genere** par `sh OpenForesia.Tools/census-dll-surface.sh` — ne pas editer a la main.
**Le regenerer apres chaque corps de DLL transcrit.**

Le jeu de 1998 se repose sur cinq DLL d'aide, livrees sur le CD et jamais reconstruites : elles
tournent encore en natif, appelees par `[DllImport]` depuis `OpenForesia/src/Blaze/Interop/`. Ce
document est la feuille de route pour s'en passer.

**Il ne mesure pas la taille des DLL, il mesure ce que le jeu en atteint.** La distinction est tout
l'objet du releve : la somme des cinq decompilations est un devis faux d'un ordre de grandeur. Ce
qui compte est la CLOTURE — les points d'entree que `Interop/` importe, plus tout ce que leurs corps
appellent, de proche en proche. Le reste de chaque DLL est du code mort pour `Blaze.exe`.

Les deux colonnes d'etat sont celles de `GestionnairesEntites.md`, et se lisent pareil :

| colonne | ce qu'elle dit |
|---|---|
| **employe** | la reconstruction importe ce point d'entree — il est atteint |
| **implemente** | un corps C# le porte, hors de `Interop/` — il est ecrit |

MDEOF

echo "## Le graphe de dependances"
echo
echo "Releve dans les binaires eux-memes — les noms de DLL qu'ils portent en clair, que ce soit par"
echo "table d'imports ou par \`LoadLibraryA\`. Une DLL que personne ne nomme n'est chargee par"
echo "personne."
echo
echo '```'
awk -F'\t' '
    { dep[$1] = dep[$1] " " $2 }
    END {
        n = split("Blaze.exe winsys3d.dll wsyskern.dll 3dhelp.dll gldhelp.dll playst.dll", o, " ")
        for (i = 1; i <= n; i++) {
            d = dep[o[i]]
            gsub(/^ /, "", d); gsub(/ /, ", ", d)
            printf "%-14s --> %s\n", o[i], (d == "" ? "(rien)" : d)
        }
    }
' "$TMP/deps.tsv"
echo '```'
echo

cat <<'MDEOF'
## Le releve

MDEOF

echo "L'ordre des lignes est l'ORDRE DE TRAVAIL recommande, pas un tri : la feuille du graphe"
echo "d'abord, le gros morceau ensuite, les deux devices en dernier — voir \`Documentation/Etat/TODO.md\`."
echo
echo "| DLL | fonctions | lignes | employes | cloture | lignes | dont CRT | part | portees |"
echo "|---|---:|---:|---:|---:|---:|---:|---:|---:|"
awk -F'\t' '
    function milliers(n,   s, out) {
        s = sprintf("%d", n)
        while (length(s) > 3) { out = " " substr(s, length(s) - 2) out; s = substr(s, 1, length(s) - 3) }
        return s out
    }
    {
        # $1 dll  $2 fonctions  $3 lignes  $4 employes  $5 a transcrire  $6 devis
        # $7 fonctions CRT  $8 lignes CRT  $9 part  $10 implementees
        vide = ($4 == 0)
        printf "| `%s` | %s | %s | %s | %s | %s | %s | %s | %s |\n",
               $1, milliers($2), milliers($3),
               vide ? "—" : milliers($4), vide ? "—" : milliers($5),
               vide ? "—" : milliers($6), vide ? "—" : milliers($8),
               vide ? "—" : sprintf("%d %%", $9), vide ? "—" : sprintf("%d", $10)
        tl += $3; td += (vide ? 0 : $6)
    }
    END {
        printf "| **total** | | **%s** | | | **%s** | | **%d %%** | |\n",
               milliers(tl), milliers(td), 100 * td / tl
    }
' "$TMP/summary.tsv"
echo
cat <<'MDEOF'
La colonne **cloture** compte les fonctions a transcrire, **portees** celles qui le sont deja, **lignes** leur taille cumulee en lignes de
Ghidra, et **dont CRT** ce que la cloture traverse de bibliotheque standard liee statiquement — qui
se rebranche au lieu de se transcrire, et qui est deja retranche du devis. La colonne **part**
rapporte le devis a la taille de la DLL entiere.

MDEOF

for d in $DLLS; do
    base=$(cat "$TMP/$d.base")
    awk -v dll="$d" -v base="$base" -v mode=detail -f OpenForesia.Tools/census-dll-surface.awk \
        "$TMP/$d.exports.tsv" "$TMP/$d.graph.tsv" "$TMP/roots.tsv" "$TMP/impl.tsv" \
        "$TMP/sisters.tsv" "$TMP/crt.tsv" "$TMP/blazeord.txt"
done

cat <<'MDEOF'
---

## Ce que ce releve ne dit pas

**Il ne dit pas que la cloture est complete.** Elle suit les appels que Ghidra a su nommer. Un appel
par POINTEUR — la vtable d'un device, un rappel installe par `Ordinal_790` — n'y figure pas, et il y
en a. La cloture est donc un PLANCHER : ce qu'il faut transcrire au minimum, jamais un plafond. Le
chemin d'affichage, en particulier, sort de `winsys3d` par la vtable de `3dhelp` et cette arete est
invisible ici.

**Il ne dit pas qu'un corps hors cloture est mort.** Il dit que rien de ce que la reconstruction
importe AUJOURD'HUI n'y mene. Un ecran non encore atteint peut faire apparaitre un ordinal de plus —
la liste « appeles par `Blaze.c`, pas encore importes » de chaque section est precisement ce
compteur-la.

**Il ne dit pas qu'une fonction implementee est juste.** `implemente` lit un commentaire de
tracabilite, pas un comportement. C'est la meme lecture que `implemente` dans
`GestionnairesEntites.md` : le corps existe, rien de plus.

**Il ne compte pas `ir41_32.ax`.** Ce n'est pas une DLL du jeu mais le codec Indeo 4.1 d'Intel,
et il n'en existe aucune decompilation dans le depot. Il n'est plus livre depuis le 2026-09-24 :
le decodeur de FFmpeg, porte en C# dans `OpenForesia/src/Host/Indeo4/`, le remplace, identique a
l'octet pres — voir `Documentation/DLL/ir41_32.md`.

**Une fonction se transcrit ENTIERE.** Ne transcrire d'une DLL que ce qu'on en atteint est un choix
delibere et ce document le sert. Ne transcrire d'une FONCTION que la branche deja vue en jeu n'en
est pas un : c'est ainsi qu'on obtient un corps qui compile, qui tourne, et qui repond faux sur le
cas d'a cote. Partielle a l'echelle de la DLL, complete a l'echelle du corps.
MDEOF
