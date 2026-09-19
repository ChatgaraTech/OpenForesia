#!/bin/sh
# Mesure du TAUX DE DOUBLON sur les gestionnaires d'entite.
#
# Le compilateur de 1998 a emis plusieurs fois le meme corps — une copie par zone. La question
# « combien reste-t-il de FORMES a comprendre, et non de creneaux a remplir ? » est le principal
# levier sur toute estimation de charge, et ce script y repond sans rien estimer.
#
# Methode, en quatre temps :
#   1. la liste des 1430 gestionnaires distincts et leur etat vient de GestionnairesEntites.md
#      (donc du recensement, qui lui-meme lit les tables dans Blaze.exe) ;
#   2. la BORNE de chaque corps est la premiere adresse connue qui le suit — l'union des adresses
#      de gestionnaires et des `FUN_xxxxxx @ 0x…` de Blaze.c ;
#   3. chaque corps est desassemble puis TRONQUE A SA DERNIERE INSTRUCTION `ret` : sans cela, deux
#      emissions du meme corps ne se ressemblent pas, parce que leurs fenetres traînent un
#      bourrage de longueur differente. C'est ce detail qui fait ou defait la mesure ;
#   4. deux normalisations, et il faut les deux :
#        STRICT  ne masque que les cibles de saut et d'appel — le placement, rien d'autre ;
#        LOOSE   masque TOUTE immediate qui ressemble a une adresse.
#
# LE CONTROLE est la paire de Grizzlys `0x0049B840` / `0x004A4270`, dont HISTORY.md etablit le
# 2026-09-01 qu'elle est le meme corps a deux immediates pres, et que ces deux immediates designent
# des donnees identiques. Une mesure correcte doit donc rendre STRICT different et LOOSE identique.
# Le script le verifie et le dit. S'il ne le dit pas, la mesure est fausse : ne pas lire la suite.
#
# Usage :
#   sh OpenForesia.Tools/census-duplicates.sh
#
# Le desassemblage est mis en cache sous $TMP/raw ; une seconde execution est immediate.

set -e
cd "$(dirname "$0")/.."

EXE=OpenForesia.Tools/bin/Debug/net9.0/OpenForesia.Tools.exe
BIN=Legacy/Binaries/Blaze.exe
ROADMAP=Documentation/Etat/GestionnairesEntites.md
TMP=${TMP_DUP:-/tmp/census-duplicates}

[ -x "$EXE" ] || { echo "Desassembleur absent : dotnet build OpenForesia.sln" >&2; exit 1; }

mkdir -p "$TMP/raw"

# ---------------------------------------------------------------- 1. les gestionnaires distincts
# Les sections « zone » ont cinq colonnes, celle des tables statiques en a quatre : l'etat n'est
# donc pas dans le meme champ. Les dix adresses de TABLES listees en fin de document sont exclues.
grep -E "^\| \`0x[0-9A-F]{8}\` \|" "$ROADMAP" \
  | awk -F'|' '{a=$2; gsub(/[` ]/,"",a); f=(NF==6)?$4:$5; gsub(/[* ]/,"",f); print a"\t"f}' \
  | grep -vE "^0x(0056AD48|0056ADC8|0056AEC8|0056AFC8|0056B0C8|0056B1C8|0056B2C8|00551C30|00551F30|00552230)\b" \
  | awk -F'\t' '{if($2=="oui") s[$1]="oui"; else if(!($1 in s)) s[$1]="non"}
                END{for(a in s) print a"\t"s[a]}' \
  | sort > "$TMP/handlers.tsv"

# ---------------------------------------------------------------- 2. les bornes de fin de corps
grep -oE "^/\* FUN_[0-9a-f]+ @ [0-9a-f]+ \*/" Legacy/Decompiled/Blaze.c \
  | awk '{print "0x"toupper($4)}' > "$TMP/bounds.raw"
cut -f1 "$TMP/handlers.tsv" >> "$TMP/bounds.raw"
sort -u "$TMP/bounds.raw" | awk '{print strtonum($1)}' | sort -n > "$TMP/bounds.num"

cut -f1 "$TMP/handlers.tsv" | awk 'NR==FNR{b[NR]=$1;n=NR;next}
  { a=strtonum($1); end=a+8192
    for(i=1;i<=n;i++) if(b[i]>a){ end=b[i]; break }
    len=end-a; if(len>8192) len=8192; if(len<16) len=16
    printf "%s\t%d\n", $1, len }' "$TMP/bounds.num" - > "$TMP/spans.tsv"

# ---------------------------------------------------------------- 3. desassemblage (mis en cache)
while IFS="$(printf '\t')" read -r addr len; do
    [ -s "$TMP/raw/$addr.asm" ] || "$EXE" disasm "$BIN" "$addr" "$len" > "$TMP/raw/$addr.asm" 2>/dev/null
done < "$TMP/spans.tsv"

# ---------------------------------------------------------------- 4. normalisation, en une passe
awk '
BEGIN { FS = "\n" }
FNR == 1 { if (NR > 1) emit()
    addr = FILENAME; sub(/^.*\//, "", addr); sub(/\.asm$/, "", addr)
    n = 0; lastret = 0; delete raw; delete calls }
/^  [0-9a-f]{8}  [0-9a-f]+ / {
    line = $0
    sub(/^ +[0-9a-f]{8} +[0-9a-f]+ +/, "", line)
    gsub(/[ \t]+/, " ", line); sub(/ $/, "", line)
    raw[++n] = line
    if (line == "ret" || line ~ /^ret /) lastret = n
    if (line ~ /^call /) { t = line; sub(/^call /, "", t)
        if (t ~ /^[0-9A-F]+h$/) calls[t] = 1 } }
END { emit() }
function emit(   i, s, l, strict, loose, k, arr, m, j, sig, last) {
    if (addr == "") return
    last = (lastret > 0) ? lastret : n
    strict = ""; loose = ""
    for (i = 1; i <= last; i++) {
        s = raw[i]
        if (s ~ /^(call|jmp|j[a-z]+|loop[a-z]*) /) gsub(/[0-9A-F]{4,8}h/, "TGT", s)
        strict = strict s ";"
        l = raw[i]; gsub(/(00)?[0-9A-F]{6}h/, "TGT", l); loose = loose l ";" }
    m = 0; for (k in calls) arr[++m] = k
    for (i = 2; i <= m; i++) { k = arr[i]; j = i - 1
        while (j > 0 && arr[j] > k) { arr[j+1] = arr[j]; j-- }; arr[j+1] = k }
    sig = ""; for (i = 1; i <= m; i++) sig = sig arr[i] ","
    if (sig == "") sig = "(aucun)"
    printf "%s\t%d\t%s\t%s\t%s\n", addr, last, strict, loose, sig }
' "$TMP"/raw/*.asm > "$TMP/bodies.tsv"

# ---------------------------------------------------------------- 5. le controle, puis le rapport
awk -F'\t' '$1=="0x0049B840"||$1=="0x004A4270"{s[$1]=$3;l[$1]=$4}
  END{ ok = (s["0x0049B840"]!=s["0x004A4270"]) && (l["0x0049B840"]==l["0x004A4270"])
       printf "CONTROLE  paire Grizzly 0x0049B840 / 0x004A4270 : STRICT %s, LOOSE %s  -> %s\n\n",
              (s["0x0049B840"]==s["0x004A4270"]?"identiques":"differents"),
              (l["0x0049B840"]==l["0x004A4270"]?"identiques":"differents"),
              (ok ? "CONFORME" : "*** NON CONFORME : ne pas lire la suite ***") }' "$TMP/bodies.tsv"

awk -F'\t' '
NR==FNR { etat[$1]=$2; next }
{ a=$1; n=$2; s=$3; l=$4; g=$5; ia=(g ~ /00535F20h/)
  st=etat[a]; total++
  if (st=="oui") { ecrits++; wbrut+=n; if(!(wl[l]++)) { wnet+=n; wnet_k[ia]+=n; wnet_c[ia]++ } ; W[l]=1 }
  else { afaire++; tbrut+=n; tc[ia]++; tk[ia]+=n; T[l]=T[l]" "a; TC[l]++; TN[l]=n; TIA[l]=ia
         cs[s]++; if(st=="oui") cs_d[s]++; else cs_t[s]++ } }
END {
  for (l in T) { if (l in W) { couv[TIA[l]]+=TC[l]; couvi[TIA[l]]+=TC[l]*TN[l] }
                 else { neuf[TIA[l]]++; neufi[TIA[l]]+=TN[l]
                        if (TC[l]>1) { fr[TIA[l]]+=TC[l]-1; fri[TIA[l]]+=(TC[l]-1)*TN[l] } } }
  for (s in cs) { formes_s++; if (cs[s]>1) redond_s += cs[s]-1 }

  printf "PERIMETRE\n"
  printf "  gestionnaires distincts atteignables : %d\n", total
  printf "  deja ecrits                          : %d   (%d instructions, %d nettes de doublon)\n", ecrits, wbrut, wnet
  printf "  A TRANSCRIRE                         : %d   (%d instructions)\n\n", afaire, tbrut

  printf "TAUX DE DOUBLON SUR LE RESTE\n"
  printf "  %-28s %6s %8s %8s %8s\n", "", "corps", "->reels", "gratuits", "instr a lire"
  for (k = 1; k >= 0; k--) {
    nom = k ? "routines d IA" : "gestionnaires simples"
    r = neuf[k]; tt = tc[k]
    # Une filiere vide (toutes les routines d IA ecrites, 2026-09-19) n a pas de taux : 0 %.
    printf "  %-28s %6d %8d %7.0f %% %10d  sur %d\n", nom, tt, r, tt ? 100.0*(tt-r)/tt : 0, neufi[k], tk[k] }
  tt = tc[0]+tc[1]; r = neuf[0]+neuf[1]
  printf "  %-28s %6d %8d %7.0f %% %10d  sur %d\n\n", "TOTAL", tt, r, tt ? 100.0*(tt-r)/tt : 0, neufi[0]+neufi[1], tbrut

  printf "  dont copies exactes d un corps DEJA ECRIT : %d IA + %d simples = %d corps, %d instructions\n",
         couv[1], couv[0], couv[0]+couv[1], couvi[0]+couvi[1]
  printf "  dont copies d un FRERE non ecrit          : %d IA + %d simples = %d corps, %d instructions\n\n",
         fr[1], fr[0], fr[0]+fr[1], fri[0]+fri[1]

  printf "RYTHME OBSERVE (net de doublon, sur les 11 seances du 22 aout au 2 septembre 2026)\n"
  printf "  filiere IA     : %5d instructions / seance   (%d ecrites en 6 seances d IA)\n", wnet_k[1]/6, wnet_k[1]
  printf "  filiere simple : %5d instructions / seance   (%d ecrites en 11 seances)\n", wnet_k[0]/11, wnet_k[0]
  printf "  toutes filieres: %5d instructions / seance\n\n", wnet/11

  printf "PROJECTION\n"
  printf "  filiere IA     : %6d / %4d = %4.0f seances\n", neufi[1], int(wnet_k[1]/6), neufi[1]/(wnet_k[1]/6)
  printf "  filiere simple : %6d / %4d = %4.0f seances\n", neufi[0], int(wnet_k[0]/11), neufi[0]/(wnet_k[0]/11)
  printf "  menees ENSEMBLE dans les memes seances (ce qui s est fait le 29 aout) : ~%.0f seances\n",
         (neufi[1]/(wnet_k[1]/6) > neufi[0]/(wnet_k[0]/11) ? neufi[1]/(wnet_k[1]/6) : neufi[0]/(wnet_k[0]/11)) * 1.15
  printf "  menees EN ALTERNANCE                                                 : ~%.0f seances\n",
         neufi[1]/(wnet_k[1]/6) + neufi[0]/(wnet_k[0]/11)
}' "$TMP/handlers.tsv" "$TMP/bodies.tsv"

echo
echo "LES PLUS GROS GROUPES A ECRIRE UNE FOIS  (ecrire le representant, couvrir les autres)"
awk -F'\t' 'NR==FNR{e[$1]=$2;next}
  { if (e[$1]!="non") { w[$4]=1; next }
    T[$4]=T[$4]" "$1; C[$4]++; N[$4]=$2; IA[$4]=($5 ~ /00535F20h/) }
  END{ for (l in T) if (!(l in w) && C[l] > 1)
         printf "%3d corps  %5d instr  %s %s\n", C[l], N[l], (IA[l]?"IA    ":"simple"), substr(T[l],1,66) }' \
  "$TMP/handlers.tsv" "$TMP/bodies.tsv" | sort -rn | head -12
