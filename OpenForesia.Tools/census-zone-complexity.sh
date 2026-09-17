#!/bin/sh
# Trie les gestionnaires A ECRIRE d'une zone PAR COMPLEXITE, pour decider dans quel ordre les
# prendre : d'abord ce qui est deja ecrit ailleurs, puis les simples, puis le reste.
#
# Rien n'est estime a la main. Chaque gestionnaire est mesure sur son desassemblage :
#
#   instr        le nombre d'instructions jusqu'au dernier `ret` (la mesure de census-duplicates.sh)
#   sauts        le nombre de tables de sauts (`jmp dword [reg*4+…]`) dans le corps
#   Blaze.c      oui si Ghidra l'a decompile — sinon, c'est le desassembleur
#   inconnues    les appelees dont l'adresse n'apparait nulle part dans les sources C#
#   jumeau       un corps identique en LOOSE (adresses masquees) — deja ecrit, ou encore a ecrire
#
# UNE AMORCE N'EST PAS UN PETIT CORPS. Un gestionnaire de moins de seize instructions qui appelle
# une seule fonction est une amorce : sa complexite est celle de l'appelee. Le script compare donc
# l'appelee, normalisee, aux appelees des amorces DEJA ECRITES ; si elles sont identiques, l'amorce
# est classee « deja ecrite ailleurs ». C'est ainsi que les quatre amorces de scene parlante de la
# vieille tour se sont revelees etre le corps de la mine et de la foret, a la base du bloc pres.
#
# Les cinq categories, dans l'ordre ou il faut les prendre :
#
#   A  deja ecrit ailleurs   copie LOOSE d'un corps ecrit, ou amorce d'une appelee ecrite
#                            -> un renvoi, et la verification des immediates de donnee
#   B  simple                pas d'IA, <= 100 instructions, aucune table de sauts, aucune inconnue
#   C  moyen                 pas d'IA, <= 250 instructions (ou une table de sauts, ou une inconnue)
#   D  gros                  pas d'IA, > 250 instructions
#   E  intelligence          appelle FUN_00535f20 — une routine de bete, toujours longue
#
# Dans chaque categorie, « lectures » compte les FORMES distinctes : deux corps a ecrire qui sont
# jumeaux entre eux ne coutent qu'une lecture.
#
# Depend du cache de census-duplicates.sh (desassemblage et normalisation), qu'il lance au besoin.
#
# Usage :
#   sh OpenForesia.Tools/census-zone-complexity.sh 4 > Documentation/Etat/Zone4ParComplexite.md

set -e
cd "$(dirname "$0")/.."

ZONE=${1:?usage: census-zone-complexity.sh <zone>}
EXE=OpenForesia.Tools/bin/Debug/net9.0/OpenForesia.Tools.exe
BIN=Legacy/Binaries/Blaze.exe
ROADMAP=Documentation/Etat/GestionnairesEntites.md
TMP_DUP=${TMP_DUP:-/tmp/census-duplicates}
WORK=$(mktemp -d)
trap 'rm -rf "$WORK"' EXIT

[ -x "$EXE" ] || { echo "Desassembleur absent : dotnet build OpenForesia.sln" >&2; exit 1; }

# Le cache de census-duplicates.sh, rafraichi a chaque fois : il est rapide une fois le desassemblage
# en place, et l'etat implemente/non doit etre celui du jour.
TMP_DUP="$TMP_DUP" sh OpenForesia.Tools/census-duplicates.sh > "$WORK/dup.out" 2>&1
B="$TMP_DUP/bodies.tsv"
H="$TMP_DUP/handlers.tsv"

# ---------------------------------------------------------------- 1. les gestionnaires de la zone
NEXT=$((ZONE + 1))
TITLE=$(grep -E "^## Zone $ZONE — " "$ROADMAP" | sed "s/^## Zone $ZONE — //")
sed -n "/^## Zone $ZONE — /,/^## \(Zone $NEXT \|Les tables\)/p" "$ROADMAP" \
  | grep -E '^\| `0x' \
  | awk -F'|' '{a=$2; gsub(/[` ]/,"",a); s=$3; gsub(/ /,"",s); t=$4; gsub(/ /,"",t);
               i=$5; gsub(/[* ]/,"",i); print a"\t"s"\t"t"\t"i}' > "$WORK/zone.tsv"

# ---------------------------------------------------------------- 1b. le nom des betes
# especes.csv donne, par sous-zone, l'indice `ia` de chaque espece dans la table 0x0056ad48 et son
# modele ; modeles.csv donne le nom du modele. L'adresse du gestionnaire se lit dans le binaire :
# le selecteur 0x00566d98 + (zone * 16 + sous-zone) * 4 designe le bloc, et le bloc + ia * 4 le
# corps — le chemin de dump-entity-vtables.sh.
DATA_DELTA=$((0x401A00))
dword() { od -An -v -tx4 -j $(( $1 - DATA_DELTA )) -N 4 "$BIN" | tr -d ' '; }
awk -F'"' 'NR>1 { split($1, p, ","); if (p[1]=="'"$ZONE"'") print p[2]"\t"p[3]"\t"$2 }' \
  Documentation/Reference/Bestiaire/modeles.csv > "$WORK/models.tsv"
awk -F, 'NR>1 && $1=="'"$ZONE"'" { print $2"\t"$4"\t"$5 }' Documentation/Reference/Bestiaire/especes.csv \
  | sort -u | while IFS="$(printf '\t')" read -r sz ia model; do
      block=$(dword $(( 0x00566d98 + (ZONE * 16 + sz) * 4 )))
      handler=$(dword $(( 0x$block + ia * 4 )) | tr a-f A-F)
      name=$(awk -F'\t' -v s="$sz" -v m="$model" '$1==s && $2==m {print $3; exit}' "$WORK/models.tsv")
      [ -n "$name" ] && printf "0x%s\t%s\n" "$handler" "$name"
    done | sort -u > "$WORK/beasts.tsv"

# ---------------------------------------------------------------- 2. ce que le C# connait deja
grep -rohiE "(0x|FUN_|LAB_)00[0-9a-f]{6}" OpenForesia/src --include=*.cs \
  | sed -E 's/^(0x|FUN_|LAB_)//I' | tr a-f A-F | sort -u > "$WORK/known.txt"
grep -oE "^/\* FUN_[0-9a-f]+ @" Legacy/Decompiled/Blaze.c \
  | awk '{print "0x" toupper(substr($2,5))}' | sort -u > "$WORK/decomp.txt"

# ---------------------------------------------------------------- 3. tables de sauts
while IFS="$(printf '\t')" read -r a rest; do
  n=$(awk -F'\t' -v a="$a" '$1==a{print $2}' "$B")
  j=$(grep -E "^  [0-9a-f]{8}  " "$TMP_DUP/raw/$a.asm" | head -n "${n:-0}" | grep -cE "jmp +dword \[[a-z]+\*4\+" || true)
  printf "%s\t%s\n" "$a" "$j"
done < "$WORK/zone.tsv" > "$WORK/jt.tsv"

# ---------------------------------------------------------------- 4. les amorces et leur appelee
# Empreinte d'une appelee : son desassemblage jusqu'au premier `ret`, adresses masquees.
callee_of() { grep -oE "call +[0-9A-F]+h" "$TMP_DUP/raw/$1.asm" | head -1 | grep -oE "[0-9A-F]{6,8}" || true; }
fingerprint() {
  "$EXE" disasm "$BIN" "0x$1" 2400 2>/dev/null | grep -E "^  [0-9a-f]{8}  " \
    | sed -E 's/^ +[0-9a-f]{8} +[0-9a-f]+ +//; s/[ \t]+/ /g; s/(00)?[0-9A-F]{6}h/TGT/g' \
    | awk '{print} /^ret/{exit}' | md5sum | cut -c1-16
}

# Les amorces ECRITES, toutes zones confondues.
awk -F'\t' 'NR==FNR{st[$1]=$2; next} st[$1]=="oui" && $2<=15 {print $1}' "$H" "$B" \
  | while read -r a; do
      c=$(callee_of "$a"); [ -n "$c" ] || continue
      printf "%s\t%s\n" "$(fingerprint "$c")" "$a"
    done > "$WORK/written-trampolines.tsv"

# Les amorces A ECRIRE de la zone.
awk -F'\t' 'NR==FNR{n[$1]=$2; next} $4=="non" && n[$1]<=15 {print $1}' "$B" "$WORK/zone.tsv" \
  | while read -r a; do
      c=$(callee_of "$a"); [ -n "$c" ] || continue
      f=$(fingerprint "$c")
      w=$(awk -F'\t' -v f="$f" '$1==f{print $2; exit}' "$WORK/written-trampolines.tsv")
      [ -n "$w" ] && printf "%s\t%s\t%s\n" "$a" "$c" "$w"
    done > "$WORK/trampoline-twins.tsv"

# ---------------------------------------------------------------- 5. le classement
awk -F'\t' '
FILENAME ~ /known.txt$/  { known[$1]=1; next }
FILENAME ~ /decomp.txt$/ { dec[$1]=1; next }
FILENAME ~ /handlers.tsv$/ { st[$1]=$2; next }
FILENAME ~ /jt.tsv$/     { jt[$1]=$2; next }
FILENAME ~ /trampoline-twins.tsv$/ { tramp[$1]=$3; trc[$1]=$2; next }
FILENAME ~ /beasts.tsv$/ { beast[$1] = beast[$1] (beast[$1]=="" ? "" : ", ") $2; next }
FILENAME ~ /bodies.tsv$/ { n[$1]=$2; loose[$1]=$4; sig[$1]=$5; grp[$4]=grp[$4]" "$1; next }
FILENAME ~ /zone.tsv$/ && FNR==1 { while ((getline line < FILENAME) > 0) { split(line, z, "\t"); inzone[z[1]]=1 }; close(FILENAME) }
FILENAME ~ /zone.tsv$/ {
  a=$1; if ($4 != "non") next
  ia = (sig[a] ~ /00535F20h/)
  m = split(sig[a], cs, ","); unk=0; ul=""
  for (i=1;i<=m;i++) { c=cs[i]; if (c=="" || c=="(aucun)") continue; sub(/h$/,"",c)
    c=substr("00000000" c, length(c)+1); if (!(c in known)) { unk++; ul=ul" `0x" c "`" } }
  # Les jumeaux : un corps ecrit n importe ou ; les jumeaux a ecrire, ceux de la zone nommes, les
  # autres comptes.
  split(grp[loose[a]], g, " "); tw=""; tu=""; away=0
  for (k in g) { b=g[k]; if (b=="" || b==a) continue
    if (st[b]=="oui") { if (tw=="") tw=b }
    else if (b in inzone) tu = tu (tu=="" ? "" : ", ") "`" b "`"
    else away++ }
  note=""
  if (a in tramp) { tw=tramp[a]; unk=0; note="amorce de `0x" trc[a] "`, appelee identique a celle de `" tramp[a] "`" }
  if (tw != "" && note=="") note="copie LOOSE de `" tw "`"
  cat = (tw != "") ? "A" : ia ? "E" : (n[a]<=100 && jt[a]==0 && unk==0) ? "B" : (n[a]<=250) ? "C" : "D"
  if (unk>0) note = note (note==""?"":" ; ") "appelee(s) inconnue(s) :" ul
  if (a in beast) note = "**" beast[a] "**" (note==""?"":" ; ") note
  if (away>0) tu = tu (tu=="" ? "" : " ") "(+" away " ailleurs)"
  # sous-zones triees numeriquement
  ns = split($2, szs, ","); for (i=1;i<=ns;i++) for (j=i+1;j<=ns;j++) if (szs[j]+0 < szs[i]+0) { t=szs[i]; szs[i]=szs[j]; szs[j]=t }
  $2 = szs[1]; for (i=2;i<=ns;i++) $2 = $2 "," szs[i]
  # la cle de lecture : la forme LOOSE, partagee par les jumeaux a ecrire
  printf "%s\t%s\t%s\t%s\t%d\t%d\t%s\t%s\t%s\t%s\n", cat, a, $2, $3, n[a], jt[a], \
         ((a in dec) ? "oui" : "non"), tu, note, loose[a]
}' "$WORK/known.txt" "$WORK/decomp.txt" "$H" "$WORK/jt.tsv" "$WORK/trampoline-twins.tsv" "$WORK/beasts.tsv" "$B" \
   "$WORK/zone.tsv" | sort -t"$(printf '\t')" -k1,1 -k5,5n -k2,2 > "$WORK/rows.tsv"

TOTAL=$(wc -l < "$WORK/zone.tsv")
DONE=$(awk -F'\t' '$4=="oui"' "$WORK/zone.tsv" | wc -l)
TODO=$(wc -l < "$WORK/rows.tsv")

# ---------------------------------------------------------------- 6. le document
cat <<EOF
# Zone $ZONE — $TITLE, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

\`\`\`sh
sh OpenForesia.Tools/census-zone-complexity.sh $ZONE > Documentation/Etat/Zone${ZONE}ParComplexite.md
\`\`\`

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
\`GestionnairesEntites.md\` : le regenerer d'abord.

**$TOTAL gestionnaires, $DONE implementes, $TODO a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
EOF

awk -F'\t' '
BEGIN {
  lab["A"]="**A — deja ecrit ailleurs**|un renvoi, et verifier les immediates de donnee"
  lab["B"]="**B — simple**|pas d IA, <= 100 instr., sans table de sauts ni inconnue"
  lab["C"]="**C — moyen**|pas d IA, <= 250 instr., ou une table de sauts, ou une inconnue"
  lab["D"]="**D — gros**|pas d IA, > 250 instr."
  lab["E"]="**E — intelligence**|une routine de bete"
}
{ c[$1]++; if (!(($1 SUBSEP $10) in seen)) { seen[$1,$10]=1; l[$1]++; s[$1]+=$5 } }
END { split("A B C D E", o, " ")
  for (i=1;i<=5;i++) { k=o[i]; if (!(k in c)) continue
    split(lab[k], p, "|"); gsub(/ d IA/, " d'"'"'IA", p[2])
    printf "| %s | %s | %d | %d | %d |\n", p[1], p[2], c[k], (k=="A" ? 0 : l[k]), (k=="A" ? 0 : s[k]) } }' "$WORK/rows.tsv"

cat <<'EOF'

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.
EOF

for k in A B C D E; do
  cnt=$(awk -F'\t' -v k=$k '$1==k' "$WORK/rows.tsv" | wc -l)
  [ "$cnt" -gt 0 ] || continue
  case $k in
    A) h="A — deja ecrit ailleurs" ;;
    B) h="B — simple" ;;
    C) h="C — moyen" ;;
    D) h="D — gros" ;;
    E) h="E — intelligence" ;;
  esac
  printf '\n## %s (%d)\n\n' "$h" "$cnt"
  if [ $k = A ]; then
    printf '%s\n\n' "Une copie LOOSE a les memes instructions a des adresses pres : ces adresses peuvent designer une planche ou une table dont les octets DIFFERENT (le cas de \`0x0040EA00\` en zone 2). Une amorce de scene appelle un corps ecrit, mais avec sa propre base de bloc prive : verifier que cette adresse est couverte par une allocation (le cas de \`0x005713f0\`)."
  fi
  printf '| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |\n'
  printf '|---|---|---|---:|---:|---|---|---|\n'
  awk -F'\t' -v k=$k '$1==k {
    printf "| `%s` | %s | %s | %d | %d | %s | %s | %s |\n", $2, $3, $4, $5, $6, $7, $8, $9 }' "$WORK/rows.tsv"
done
