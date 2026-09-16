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
# Trois passes :
#   1. CONTENU  chaque tableau est relu dans Blaze.exe a son adresse ; DIFF = releve faux.
#   2. MARGE    pour chaque appel a `Descriptors…() [+ N]`, combien d'enregistrements restent
#               apres N. Le rendu peut en consommer autant que le compteur d'images de l'entite
#               (+0x40, plafonne dans son corps) : une marge de 1 n'est SURE que si le corps laisse
#               +0x40 a zero. Les marges <= 2 sont signalees, a verifier a la main — sauf celles
#               deja prouvees et scellees dans marges-verifiees.txt.
#   3. HERITAGE les pools 0x005ff400 et 0x005edee0 naissent avec le +0x40 du parent ; aucun de
#               leurs gestionnaires ne pose de planche dans le binaire. Un corps C# qui le fait
#               est signale comme erreur de transcription.
#
# CE QUE LA CORRECTION DU 2026-09-16 A OUVERT. Les deux passes rataient chacune une famille
# entiere, en silence — elles rendaient une liste plus courte, pas une erreur :
#
#   - la passe 2 ne reconnaissait que le motif LITTERAL `(e + 0x3c) = Descriptors…()`. Tout site
#     qui passe la planche AUTREMENT lui echappait : les corps factorises qui la prennent en
#     parametre (`TickRisingBubble(e, Descriptors0054F6F8())`), et les planches passees a un
#     semeur (`SpawnStatusEffectEntity_0047AC40(…, Descriptors00551BC8(), …)`). Desormais tout
#     appel compte, ou qu'il apparaisse ; seules la declaration de l'accesseur et les lignes de
#     commentaire sont exclues. Les ACCESSEURS ALIAS — `DescriptorsA() => DescriptorsB() + off` —
#     sont resolus en chaine, sans quoi leur marge sortait a zero et se lisait comme un defaut.
#
#   - la passe 1 n'acceptait que le nom `s_Descriptors_<VA>`. Les tableaux qualifies
#     (`s_SplashDescriptors_…`, `s_DustDescriptors_…`, `s_TrapBurstDescriptors_…`) n'etaient donc
#     PAS relus dans le binaire.
#
# Mesure avant / apres : 53 -> 62 planches verifiees, 55 -> 155 sites mesures.
#
# Et une troisieme famille, fermee le meme jour : le decalage pris A L'INTERIEUR d'un corps
# factorise (`atlas + 8` dans `TickFivePhaseBurst`) n'etait compte nulle part. Les sites
# correspondants sortent sous le nom `appelant/appele` ; tout emploi d'une planche recue que le
# script ne sait pas mesurer est liste dans une section a part, au lieu d'etre ignore.
#
# Usage :
#   sh OpenForesia.Tools/check-descriptors.sh              le controle
#   sh OpenForesia.Tools/check-descriptors.sh --sceller    et scelle les preuves nouvelles
#                                                          de marges-verifiees.txt

set -e

SCELLER=0
[ "${1:-}" = "--sceller" ] && SCELLER=1

BIN=Legacy/Binaries/Blaze.exe
SRC=OpenForesia/src
PREUVES=OpenForesia.Tools/marges-verifiees.txt
DELTA=$((0x401A00))          # .data : VA - offset fichier

TMP=$(mktemp -d)
trap 'rm -rf "$TMP"' EXIT

# ---------------------------------------------------------------- 1. le contenu des tableaux
#
# LE NOM DU TABLEAU N'EST PAS TOUJOURS `s_Descriptors_<VA>` (2026-09-16). Plusieurs plages portent
# un qualificatif — `s_SplashDescriptors_0054F7A8`, `s_DustDescriptors_0054F118`,
# `s_TrapBurstDescriptors_005519D0`… Le motif ne les prenait pas, donc leur CONTENU n'etait pas
# relu dans le binaire et la section 2 les declarait « non relevees ». Le qualificatif est
# maintenant accepte ; seule compte la terminaison `Descriptors_<VA>`.
grep -rl 's_[A-Za-z0-9]*Descriptors_00[0-9A-F]\+ =$' --include=*.cs "$SRC" | while read -r f; do
    awk '
        /private static readonly byte\[\] s_[A-Za-z0-9]*Descriptors_00[0-9A-F]+ =/ {
            n=$0; sub(/.*Descriptors_/,"",n); sub(/ =.*/,"",n); cur=n; buf=""; next
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

# `WorldMapTick.cs` a ete decoupe en 68 fichiers sous `Screens/State03/` le 2026-09-16 : on balaie
# l'arborescence entiere, pas un fichier. La section 1 ci-dessus etait deja recursive ; celle-ci ne
# l'etait pas, et aurait rendu une liste VIDE apres le decoupage - sans erreur et sans message.
#
# ON SUIT AUSSI LES PLANCHES PASSEES EN PARAMETRE (2026-09-16). Cette section ne reconnaissait que
# le motif litteral `(e + 0x3c) = DescriptorsXXXX()`. Or le depot FACTORISE les corps que le
# compilateur de 1998 a emis plusieurs fois : le corps prend la planche en parametre, et les sites
# la passent a l'appel — `TickRisingBubble(e, Descriptors0054F6F8())`. Ces sites devenaient
# invisibles, et la marge n'etait plus verifiee pour leur planche.
#
# Mesure au moment de la correction : QUINZE sites echappaient au controle — les dix corps
# parametres de la foret et les cinq de la mine. Leur CONTENU restait verifie par la section 1 ;
# c'est la marge qui manquait, donc precisement le garde-fou contre la plage relevee trop courte.
#
# La regle est donc : tout appel a `DescriptorsXXXX()` est un site d'installation, ou qu'il
# apparaisse. Deux exclusions, et deux seulement :
#   - la ligne qui DECLARE l'accesseur (`private static byte* DescriptorsXXXX()`) ;
#   - les lignes de commentaire, ou un `<see cref=...>` pourrait citer un accesseur.
# Une meme ligne peut porter DEUX planches (`TickMineSparkle_00406810(e, plancheA(), plancheB())`) :
# la boucle les prend toutes.
# La table des accesseurs ALIAS, lue avant tout : `DescriptorsA() => DescriptorsB() + off`.
awk '
    /private static byte\* Descriptors00[0-9A-F]+\(\)[ ]*=>[ ]*Descriptors00[0-9A-F]+\(\)/ {
        a=$0; sub(/.*byte\* Descriptors/,"",a); sub(/\(\).*/,"",a)
        r=$0; sub(/.*=>[ ]*Descriptors/,"",r); sub(/\(\).*/,"",r)
        off=0
        if (match($0, /=>[ ]*Descriptors00[0-9A-F]+\(\)[ ]*\+[ ]*(0x)?[0-9a-fA-F]+/)) {
            o=substr($0, RSTART, RLENGTH); sub(/.*\+[ ]*/,"",o); off=strtonum(o)
        }
        printf "%s %s %d\n", a, r, off
    }' $(find "$SRC/Blaze/Screens/State03" -name '*.cs' | sort) > "$TMP/alias"

awk '
    /private static [A-Za-z0-9_*]+ [A-Za-z_][A-Za-z0-9_]*_00[0-9A-F]+\(/ {
        fn=$0; sub(/.*static [^ ]+ /,"",fn); sub(/\(.*/,"",fn)
    }
    /^[ \t]*\/\// { next }                                        # commentaire
    /private static byte\* Descriptors00[0-9A-F]+\(\)/ { next }   # la declaration de l accesseur
    {
        rest = $0
        while (match(rest, /Descriptors00[0-9A-F]+\(\)([ ]*\+[ ]*(0x)?[0-9a-fA-F]+)?/)) {
            tok  = substr(rest, RSTART, RLENGTH)
            rest = substr(rest, RSTART + RLENGTH)

            b = tok; sub(/.*Descriptors/,"",b); sub(/\(\).*/,"",b)

            off = 0
            if (match(tok, /\+[ ]*(0x)?[0-9a-fA-F]+/)) {
                o = substr(tok, RSTART, RLENGTH); sub(/^\+[ ]*/,"",o); off = strtonum(o)
            }
            printf "%s\t%s\t%d\n", fn, b, off
        }
    }' $(find "$SRC/Blaze/Screens/State03" -name '*.cs' | sort) > "$TMP/sites"

# LE DECALAGE PRIS A L'INTERIEUR DU CORPS APPELE (2026-09-16). Ce qui precede mesure l'ARGUMENT
# d'un corps factorise — `TickFivePhaseBurst(e, Descriptors0054F808())` compte pour 0x0054f808
# a +0. Mais le corps peut poser sa planche plus loin : le role 3 de la gerbe installe
# `atlas + 8`, soit 0x0054f810, a UN enregistrement de la fin. Cette marge-la n'apparaissait nulle
# part, et c'etait une marge de 1 — precisement ce que le controle doit signaler.
#
# Deux temps :
#   a. chaque appel qui passe une planche en argument : appelant, appele, rang de l'argument,
#      planche, decalage deja present dans l'argument ;
#   b. dans chaque corps appele, ce que devient le parametre de ce rang. Deux emplois seulement
#      sont compris — `= param [+ N];` (l'installation) et `(param + N)[k]` (l'ecriture dans
#      l'enregistrement). TOUT AUTRE EMPLOI est signale « emploi non mesure » plutot que tu :
#      passer le parametre a un troisieme corps, le ranger dans une locale, l'indexer en avant
#      sont autant de sites que ce controle ne suivrait pas. Le silence est le defaut a eviter.
FILES=$(find "$SRC/Blaze/Screens/State03" -name '*.cs' | sort)

awk '
    /private static [A-Za-z0-9_*]+ [A-Za-z_][A-Za-z0-9_]*_00[0-9A-F]+\(/ {
        fn=$0; sub(/.*static [^ ]+ /,"",fn); sub(/\(.*/,"",fn)
    }
    /^[ \t]*\/\// { next }
    /private static byte\* Descriptors00[0-9A-F]+\(\)/ { next }
    {
        # Un appel peut s etendre sur plusieurs lignes : on accumule jusqu a l equilibre.
        line = $0; sub(/\/\/.*/, "", line)
        stmt = stmt " " line
        opened = gsub(/\(/, "(", line); closed = gsub(/\)/, ")", line)
        depth += opened - closed
        if (depth > 0) next
        depth = 0

        s = stmt; stmt = ""
        while (match(s, /[A-Za-z_][A-Za-z0-9_]*\(/)) {
            name = substr(s, RSTART, RLENGTH - 1)
            s = substr(s, RSTART + RLENGTH)       # les appels imbriques restent a balayer

            if (name ~ /^Descriptors00/) continue

            d = 1; args = ""
            for (j = 1; j <= length(s); j++) {
                c = substr(s, j, 1)
                if (c == "(") d++
                else if (c == ")") { d--; if (d == 0) break }
                args = args c
            }
            if (args !~ /Descriptors00/) continue

            n = 0; d = 0; cur = ""
            for (j = 1; j <= length(args); j++) {
                c = substr(args, j, 1)
                if (c == "(") d++
                if (c == ")") d--
                if (c == "," && d == 0) { arg[n++] = cur; cur = ""; continue }
                cur = cur c
            }
            arg[n++] = cur

            for (k = 0; k < n; k++) {
                t = arg[k]; gsub(/^[ \t]+|[ \t]+$/, "", t)
                if (t !~ /^Descriptors00[0-9A-F]+\(\)([ ]*\+[ ]*(0x)?[0-9a-fA-F]+)?$/) continue
                b = t; sub(/^Descriptors/, "", b); sub(/\(\).*/, "", b)
                off = 0
                if (match(t, /\+[ ]*(0x)?[0-9a-fA-F]+/)) {
                    o = substr(t, RSTART, RLENGTH); sub(/^\+[ ]*/, "", o); off = strtonum(o)
                }
                printf "%s\t%s\t%d\t%s\t%d\n", fn, name, k, b, off
            }
        }
    }' $FILES > "$TMP/calls.raw"
sort -u "$TMP/calls.raw" > "$TMP/calls"

# Le depot en compte une douzaine. Zero voudrait dire que l analyse est cassee, pas que le depot
# est propre — et la section « emplois » afficherait alors « aucun » en toute bonne foi.
if [ ! -s "$TMP/calls" ]; then
    echo "ERREUR : aucun appel ne passe de planche en argument — l'analyse est cassee." >&2
    exit 1
fi

awk '
    NR == FNR { split($0, c, "\t"); want[c[2] SUBSEP c[3]] = 1; wanted[c[2]] = 1; next }

    FNR == 1 { body = "" }

    body == "" {
        if (!match($0, /private static [^ ]+ [A-Za-z_][A-Za-z0-9_]*\(/)) next
        name = substr($0, RSTART, RLENGTH - 1); sub(/.* /, "", name)
        if (!(name in wanted)) next

        body = name; np = 0
        sig = $0; sub(/^[^(]*\(/, "", sig); sub(/\).*/, "", sig)
        k = split(sig, p, ",")
        for (i = 1; i <= k; i++) {
            if (!((name SUBSEP (i - 1)) in want)) continue
            pn = p[i]; gsub(/^[ \t]+|[ \t]+$/, "", pn); sub(/.*[ *]/, "", pn)
            param[np] = pn; rank[np] = i - 1; np++
        }
        next
    }

    /^    }$/ { body = ""; next }
    /^[ \t]*\/\// { next }

    {
        line = $0; sub(/\/\/.*/, "", line)
        for (q = 0; q < np; q++) {
            pn = param[q]; rest = line
            while (match(rest, "(^|[^A-Za-z0-9_.])" pn "([^A-Za-z0-9_]|$)")) {
                before = substr(rest, 1, RSTART)
                after  = substr(rest, RSTART + RLENGTH - 1)
                rest   = substr(rest, RSTART + RLENGTH - 1)

                if (match(after, /^[ ]*\+[ ]*(0x)?[0-9a-fA-F]+/)) {
                    o = substr(after, RSTART, RLENGTH); tail = substr(after, RLENGTH + 1)
                    sub(/^[ ]*\+[ ]*/, "", o); n = strtonum(o)
                    # `(e + 0x3c) = param + N;` ou `(param + N)[k]` — rien d autre.
                    if (before ~ /\+ 0x3c\)[ ]*=[ ]*$/ && tail ~ /^[ ]*;/)   ok = 1
                    else if (before ~ /\([ ]*$/ && tail ~ /^[ ]*\)[ ]*\[/) ok = 1
                    else ok = 0
                } else {
                    # `(e + 0x3c) = param;` — une locale `byte* p = param;` n est PAS acceptee.
                    n = 0
                    ok = (before ~ /\+ 0x3c\)[ ]*=[ ]*$/ && after ~ /^[ ]*;/)
                }

                if (!ok) printf "EMPLOI\t%s\t%s\t%d\t%s\n", body, pn, FNR, line
                else if (n > 0) printf "OFFSET\t%s\t%d\t%d\n", body, rank[q], n
            }
        }
    }' "$TMP/calls" $FILES > "$TMP/inner.raw"
sort -u "$TMP/inner.raw" > "$TMP/inner"

# Les sites ajoutes portent le nom `appelant/appele`, pour qu on retrouve le corps a lire.
awk -F'\t' '
    NR == FNR { if ($1 == "OFFSET") inner[$2 SUBSEP $3] = inner[$2 SUBSEP $3] " " $4; next }
    ((($2 SUBSEP $3) in inner)) {
        k = split(inner[$2 SUBSEP $3], v, " ")
        for (i = 1; i <= k; i++) printf "%s/%s\t%s\t%d\n", $1, $2, $4, $5 + v[i]
    }' "$TMP/inner" "$TMP/calls" >> "$TMP/sites"

# LES MARGES DEJA PROUVEES (2026-09-16). Une marge courte se prouve a la main, une fois ; la
# signaler ensuite a chaque passage noierait le site NOUVEAU dans les sites connus. Chaque preuve
# est scellee sur le texte des corps dont elle depend : si l'un change, elle est perimee et le site
# redevient a verifier. Voir l'en-tete de marges-verifiees.txt.
#
# L'empreinte d'un corps : de sa declaration a son accolade fermante (ou au `;` d'un corps en
# `=>`), commentaires et lignes vides retires. Un nom introuvable ou ambigu CASSE la preuve.
body_text() {
    f=$(grep -rlE "static [^ (]+ $1\(" --include=*.cs "$SRC/Blaze")
    [ "$(printf '%s\n' "$f" | grep -c .)" -eq 1 ] || return 1
    awk -v fn="$1" '
        !on && index($0, "static ") && index($0, " " fn "(") { on = 1 }
        on {
            # Sans le retour chariot : une extraction CRLF ou LF ne doit pas perimer une preuve.
            l = $0; gsub(/\r/, "", l); sub(/[ \t]*\/\/.*$/, "", l)
            if (l !~ /^[ \t]*$/) print l
            if ($0 ~ /\{/) braced = 1
            if (!braced && l ~ /;[ \t]*$/) exit
            if (braced && $0 ~ /^    }$/) exit
        }' "$f"
}

empreinte() {
    : > "$TMP/corps"
    for d in $(echo "$1" | tr ',' ' '); do
        if ! body_text "$d" >> "$TMP/corps"; then echo "introuvable:$d"; return 0; fi
    done
    md5sum < "$TMP/corps" | cut -c1-12
}

# Une ligne par preuve : cle, etat, empreinte actuelle.
evaluate_proofs() {
    : > "$TMP/preuves"
    [ -f "$PREUVES" ] || return 0
    grep -vE '^[ \t]*(#|$)' "$PREUVES" |
    while IFS='|' read -r site planche decal deps sceau raison; do
        site=$(echo $site); planche=$(echo $planche); decal=$(echo $decal)
        deps=$(echo "$deps" | tr -d ' \t'); sceau=$(echo "$sceau" | tr -d ' \t')
        e=$(empreinte "$deps")
        case "$e" in
            introuvable:*)       st="PREUVE CASSEE, ${e#introuvable:} introuvable ou ambigu" ;;
            *) if   [ -z "$sceau" ];      then st="preuve non scellee (--sceller)"
               elif [ "$sceau" = "$e" ];  then st="ok"
               else                            st="PREUVE PERIMEE, un corps a change : relire"
               fi ;;
        esac
        printf '%s %s %s\t%s\t%s\n' "$site" "$planche" "$decal" "$st" "$e" >> "$TMP/preuves"
    done
}

evaluate_proofs

# --sceller : pose l'empreinte sur les lignes dont la colonne est VIDE, et sur elles seules.
if [ "$SCELLER" = 1 ]; then
    awk '
        NR == FNR { split($0, t, "\t"); e[t[1]] = t[3]; next }
        /^[ \t]*(#|$)/ { print; next }
        {
            n = split($0, c, "|")
            k1 = c[1]; k2 = c[2]; k3 = c[3]; s = c[5]
            gsub(/^[ \t]+|[ \t]+$/, "", k1); gsub(/^[ \t]+|[ \t]+$/, "", k2)
            gsub(/^[ \t]+|[ \t]+$/, "", k3); gsub(/[ \t]/, "", s)
            key = k1 " " k2 " " k3
            if (s == "" && (key in e) && e[key] !~ /^introuvable/) {
                c[5] = " " e[key] " "; sealed++
            }
            line = c[1]; for (i = 2; i <= n; i++) line = line "|" c[i]
            print line
        }
        END { print sealed + 0 > "/dev/stderr" }' "$TMP/preuves" "$PREUVES" > "$TMP/preuves.new" 2> "$TMP/sealed"
    cp "$TMP/preuves.new" "$PREUVES"
    echo "$(cat "$TMP/sealed") preuve(s) scellee(s) dans $PREUVES."
    echo
    evaluate_proofs
fi

: > "$TMP/vues"

sort -u "$TMP/sites" |
while IFS='	' read -r fn base off; do
    # LES ACCESSEURS ALIAS. Certaines planches n'ont pas de tableau a elles : leur accesseur pointe
    # dans une autre, a un decalage — `Descriptors00551BC8() => Descriptors00551BA0() + 0x28`.
    # Sans les resoudre, la section 1 ne connait pas leur nom et la marge sortirait a zero, ce qui
    # se lirait comme un defaut grave alors que c'est un simple renvoi. La chaine est suivie, avec
    # un garde-fou contre un cycle.
    guard=0
    while [ "$guard" -lt 8 ]; do
        al=$(awk -v n="$base" '$1==n{print $2" "$3; exit}' "$TMP/alias")
        [ -z "$al" ] && break
        base=${al%% *}
        off=$(( off + ${al##* } ))
        guard=$(( guard + 1 ))
    done

    recs=$(awk -v n="$base" '$1==n{print $2}' "$TMP/len")

    # Une planche sans relevé sous ce nom : le site est reel, mais la section 1 ne la mesure pas.
    # Le cas se presente quand le tableau porte un autre nom que `s_Descriptors_<VA>`.
    if [ -z "$recs" ]; then
        printf "%-34s 0x%s +0x%02x  PLAGE NON RELEVEE sous ce nom\n" "$fn" "$base" "$off"
        continue
    fi

    left=$((recs - off / 8))
    flag=""
    if [ "$left" -le 2 ]; then
        key=$(printf '%s 0x%s +0x%02x' "$fn" "$base" "$off")
        echo "$key" >> "$TMP/vues"
        st=$(awk -F'\t' -v k="$key" '$1 == k { print $2; exit }' "$TMP/preuves")
        case "$st" in
            ok) flag="   prouve" ;;
            "") flag="   <-- marge courte, verifier le plafond de +0x40" ;;
            *)  flag="   <-- marge courte, $st" ;;
        esac
    fi
    printf "%-34s 0x%s +0x%02x  %2d restant(s)%s\n" "$fn" "$base" "$off" "$left" "$flag"
done

echo
echo "## Marges courtes"
echo
# Le bilan, et les preuves ORPHELINES : une ligne de marges-verifiees.txt qui ne correspond plus a
# aucun site a marge courte — site renomme, planche allongee, decalage change. Elle ne protege plus
# rien et doit etre retiree ou corrigee ; la laisser, c'est laisser la liste pourrir.
awk -F'\t' '
    NR == FNR { vu[$0] = 1; total++; next }
    { etat[$1] = $2 }
    END {
        for (k in vu) if (etat[k] == "ok") ok++
        for (k in etat) if (!(k in vu)) { printf "ORPHELINE : %s — plus aucun site a marge courte sous cette cle\n", k; orph++ }
        if (orph) print ""
        printf "%d marge(s) courte(s) : %d prouvee(s), %d a verifier.\n", total, ok, total - ok
    }' "$TMP/vues" "$TMP/preuves"

echo
echo "## Planches recues en parametre et employees autrement"
echo
echo "$(cut -f2,3 "$TMP/calls" | sort -u | wc -l) parametre(s) recevant une planche, examines dans leur corps."
echo
awk -F'\t' '$1 == "EMPLOI" {
    n++; l = $5; sub(/^[ \t]+/, "", l)
    printf "%s, parametre %s, ligne %s : %s\n", $2, $3, $4, l
} END {
    if (n == 0) print "aucun : chaque planche recue est installee telle quelle ou a un decalage fixe."
    else        print "\n" n " emploi(s) que la marge ci-dessus NE MESURE PAS — a lire a la main."
}' "$TMP/inner"

# ---------------------------------------------------------------- 3. les pools qui heritent +0x40
#
# FUN_0047ac40 remet +0x40 a zero ; FUN_0047a9d0 et FUN_0047aae0 le RECOPIENT du parent, avec
# +0x3c. Dans le binaire, c'est sans danger, et la preuve a ete faite le 2026-09-16 sur les 430
# gestionnaires que ces deux pools peuvent executer (Blaze.c, releves, binaire) : AUCUN ne pose de
# planche sur son propre enregistrement. L'enfant garde donc la paire +0x3c / +0x40 de son parent.
#
# Ce qui peut encore casser cette paire, c'est la TRANSCRIPTION : un corps C# de ces pools qui
# ecrit `(e + 0x3c)` pose une planche que le binaire ne pose pas, sous un +0x40 herite qui peut
# valoir n'importe quoi — et aucune autre passe ne le verrait. Cette section le signale.
#
# Les gestionnaires de ces pools : les creneaux 0x0056B0C8 (pool 0x005ff400, au-dela de 0xc0) et
# 0x0056B1C8 (pool 0x005edee0), relus pour les 176 sous-zones comme le fait
# census-entity-handlers.sh, plus les tables statiques 0x00551C30 et 0x00551F30.
echo
echo "## Pools qui heritent +0x40 du parent (0x005ff400, 0x005edee0)"
echo

dwords() {   # dwords <va> <nombre>
    od -An -v -tx4 -j $(( $1 - DELTA )) -N $(( $2 * 4 )) "$BIN" \
        | tr -s ' ' '\n' | grep -v '^$' | tr 'a-f' 'A-F'
}

SEL_VA=$((0x00566d98))    # sept colonnes de selecteurs, 11 zones x 16 sous-zones chacune
SEL_COUNT=176

: > "$TMP/herit"
for col in 4 5; do        # dans l'ordre 0x0056AD48 .. 0x0056B2C8 : 4 = 0x0056B0C8, 5 = 0x0056B1C8
    dwords $(( SEL_VA + col * SEL_COUNT * 4 )) $SEL_COUNT | sort -u > "$TMP/blocs"
    while read -r blk; do
        [ "$blk" = "0056B3C8" ] && continue          # le bloc d'arret : sous-zone inexistante
        dwords $(( 0x$blk )) 64 >> "$TMP/herit"
    done < "$TMP/blocs"
done
dwords $((0x00551C30)) 192 >> "$TMP/herit"
dwords $((0x00551F30)) 192 >> "$TMP/herit"
sort -u "$TMP/herit" | grep -vxE "00401FA0|0047ADC0" > "$TMP/herit.s" || true

# Les deux souches exclues : 0x00401FA0 ne fait rien, 0x0047ADC0 efface l'entite. Au-dela, zero
# adresse voudrait dire une lecture cassee, pas des pools vides.
if [ ! -s "$TMP/herit.s" ]; then
    echo "ERREUR : aucun gestionnaire relu pour les pools 0x005ff400 / 0x005edee0." >&2
    exit 1
fi

# Adresse -> methode, par les `case` du repartiteur ; un `case` sans appel tombe dans le suivant.
awk '
    match($0, /case 0x00[0-9A-F]+:/) {
        a = substr($0, RSTART + 7, RLENGTH - 8); pending = pending " " a
    }
    match($0, /: [A-Za-z_][A-Za-z0-9_]*\(/) && pending != "" {
        m = substr($0, RSTART + 2, RLENGTH - 3)
        n = split(pending, p, " "); for (i = 1; i <= n; i++) print p[i] "\t" m
        pending = ""
    }' "$SRC/Blaze/Screens/State03/Dispatch/EntityHandlerDispatch.cs" > "$TMP/cases"

# Les lignes d'un corps qui posent +0x3c sur l'enregistrement RECU — le parametre, ses alias
# `byte* e = record;`, et sa forme `uint*` (`entity[15]`). Un corps en `=>` qui delegue est suivi.
#
# Tout est fait en operations de CHAINE, pas en expressions construites a partir d'un nom : une
# regex fabriquee casse sur le premier caractere special et awk meurt — ce qui, au bout d'un tube,
# rendait « aucun » en toute bonne foi. Une erreur d'awk est desormais fatale, et un corps dont le
# parametre ne se lit pas est SIGNALE, jamais saute.
own_installs() {
    if ! body_text "$1" > "$TMP/h.body"; then echo "CORPS INTROUVABLE : $1"; return 0; fi
    awk -v fn="$1" '
        # La signature peut tenir sur plusieurs lignes : on accumule jusqu a la parenthese fermante.
        !sigdone {
            sig = sig " " $0
            if (index(sig, ")") == 0) next
            sigdone = 1
            p = substr(sig, index(sig, "(") + 1)
            cut = index(p, ")")
            if (index(p, ",") && index(p, ",") < cut) cut = index(p, ",")
            k = split(substr(p, 1, cut - 1), w, " ")
            if (k >= 2 && w[k] ~ /^[A-Za-z_][A-Za-z0-9_]*$/) names[w[k]] = 1
            else unreadable = 1
            next
        }
        {
            line = $0; sub(/^[ \t]+/, "", line)

            # Un alias de l enregistrement : `byte* X = P;` ou `byte* X = (byte*)P;`.
            if (substr(line, 1, 6) == "byte* " && (eq = index(line, " = "))) {
                a = substr(line, 7, eq - 7); v = substr(line, eq + 3)
                if (substr(v, 1, 7) == "(byte*)") v = substr(v, 8)
                if (substr(v, length(v)) == ";") v = substr(v, 1, length(v) - 1)
                if ((v in names) && a ~ /^[A-Za-z_][A-Za-z0-9_]*$/) names[a] = 1
            }

            for (n in names) {
                if (index(line, "(" n " + 0x3c) =") || index(line, n "[15] =") ||
                    index(line, n "[0xf] =") || index(line, "(byte*)" n " + 0x3c) =")) {
                    print fn " : " line; break
                }
            }
        }
        END { if (unreadable || !sigdone) print "PARAMETRE ILLISIBLE : " fn }' "$TMP/h.body" > "$TMP/h.hits" || return 2
    cat "$TMP/h.hits"

    # Un corps en `=>` delegue : on suit l appele, qui recoit l enregistrement.
    if ! grep -q '{' "$TMP/h.body"; then
        callee=$(sed -n 's/.*=> *\([A-Za-z_][A-Za-z0-9_]*\)(.*/\1/p' "$TMP/h.body" | head -1)
        if [ -n "$callee" ] && [ "$callee" != "$1" ]; then own_installs "$callee" || return 2; fi
    fi
    return 0
}

awk -F'\t' 'NR == FNR { h[$1] = 1; next } ($1 in h)' "$TMP/herit.s" "$TMP/cases" | sort -u > "$TMP/herit.cs"
total=$(wc -l < "$TMP/herit.s")
ecrits=$(wc -l < "$TMP/herit.cs")

# Des dizaines de ces gestionnaires sont transcrits. Zero examine veut dire que la jointure
# adresse -> methode est cassee, et le verdict « aucun » plus bas serait un mensonge.
if [ "$ecrits" -eq 0 ]; then
    echo "ERREUR : aucun gestionnaire de ces pools retrouve dans le repartiteur — jointure cassee." >&2
    exit 1
fi

: > "$TMP/herit.out"
while IFS='	' read -r addr name; do
    if ! own_installs "$name" > "$TMP/one"; then
        echo "ERREUR : l'examen de $name ($addr) a echoue — le verdict ne serait pas fiable." >&2
        exit 1
    fi
    sed "s|^|0x$addr |" "$TMP/one" >> "$TMP/herit.out"
done < "$TMP/herit.cs"

echo "$total gestionnaire(s) dans ces pools, dont $ecrits transcrit(s) en C# et examine(s)."
echo
if [ -s "$TMP/herit.out" ]; then
    cat "$TMP/herit.out"
    echo
    echo "$(wc -l < "$TMP/herit.out") ecriture(s) de +0x3c sur l'enregistrement recu — le binaire n'en fait AUCUNE"
    echo "dans ces pools : erreur de transcription, sous un +0x40 herite du parent."
else
    echo "aucun ne pose +0x3c sur son propre enregistrement, comme dans le binaire."
fi
