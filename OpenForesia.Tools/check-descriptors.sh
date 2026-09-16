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
#   2. MARGE    pour chaque appel a `Descriptors…() [+ N]`, combien d'enregistrements restent
#               apres N. Le rendu peut en consommer autant que le compteur d'images de l'entite
#               (+0x40, plafonne dans son corps) : une marge de 1 n'est SURE que si le corps laisse
#               +0x40 a zero. Les marges <= 2 sont signalees, a verifier a la main.
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
# Usage :
#   sh OpenForesia.Tools/check-descriptors.sh

set -e

BIN=Legacy/Binaries/Blaze.exe
SRC=OpenForesia/src
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
    }' $(find "$SRC/Blaze/Screens/State03" -name '*.cs' | sort) | sort -u |
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
    [ "$left" -le 2 ] && flag="   <-- marge courte, verifier le plafond de +0x40"
    printf "%-34s 0x%s +0x%02x  %2d restant(s)%s\n" "$fn" "$base" "$off" "$left" "$flag"
done
