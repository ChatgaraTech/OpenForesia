# Inventaire des membres de Screens/State03/WorldMapTick.cs.
#
# Sortie TSV : tete <TAB> declaration <TAB> fin <TAB> nom <TAB> VA <TAB> ligne du bandeau courant
#
#   tete         premiere ligne de la trivia de tete (commentaires de doc, attributs, lignes
#                vides) qui precede la declaration. La remontee S'ARRETE au delimiteur de
#                bandeau : un bandeau introduit une REGION, il n'appartient jamais au membre
#                qui le suit. Meme regle que split-manifest.awk, dont ce fichier est le jumeau.
#   declaration  la ligne `    private static ...`
#   fin          la ligne qui precede la tete du membre suivant (derniere ligne possedee)
#   nom          l'identifiant C# du membre
#   VA           l'adresse virtuelle Ghidra si le nom la porte en suffixe, sinon vide
#   bandeau      la premiere ligne du bandeau `// ====` courant, 0 s'il n'y en a pas
#
# L'invariant que verifie la tache 1.2 : les plages [tete, fin] se suivent sans trou ni
# recouvrement et couvrent exactement STARTLINE..ENDLINE.
#
# Usage :
#   awk -v startline=25 -v endline=74071 -f worldmaptick-inventory.awk <fichier>

BEGIN { FS = "\n"; if (startline == 0) startline = 25 }

{ L[NR] = $0 }

END {
    if (endline == 0) endline = NR - 1

    # --- 1. les blocs de bandeau ---
    for (i = 1; i <= NR; i++) {
        if (L[i] !~ /^    \/\/ ={20,}$/) continue
        if (inBanner[i]) continue
        for (j = i; j <= NR; j++) {
            if (L[j] !~ /^[ ]*\/\//) break      # sortie de secours : un bandeau reste du commentaire
            inBanner[j] = 1
            bannerHead[j] = i
            if (j > i && L[j] ~ /^    \/\/ ={20,}$/) break
        }
    }

    # --- 2. le bandeau courant, ligne par ligne ---
    cur = 0
    for (i = 1; i <= NR; i++) {
        if (inBanner[i]) cur = bannerHead[i]
        banner[i] = cur
    }

    # --- 3. les membres ---
    nm = 0
    for (i = 1; i <= NR; i++) {
        d = L[i]
        if (d ~ /^    \[.*\][ ]*(private|internal|public|protected)[ ]/) sub(/^    \[.*\][ ]*/, "    ", d)
        if (d !~ /^    (private|internal|public|protected)[ ]/) continue
        nm++; mLine[nm] = i; mText[nm] = d
    }

    # --- 4. la tete de trivia de chaque membre ---
    for (k = 1; k <= nm; k++) {
        t = mLine[k]
        for (i = mLine[k] - 1; i >= 1; i--) {
            if (inBanner[i]) break                              # regle 1 : ne jamais franchir un bandeau
            if (L[i] ~ /^[ \t]*$/)               { t = i; continue }
            if (L[i] ~ /^[ ]*(\/\/|\/\*|\*|\[)/) { t = i; continue }
            break
        }
        if (k > 1 && t <= mLine[k-1]) t = mLine[k-1] + 1
        if (k == 1 && t < startline)  t = startline
        mHead[k] = t
    }

    # --- 5. la fin de chaque membre ---
    for (k = 1; k <= nm; k++) mEnd[k] = (k < nm) ? mHead[k+1] - 1 : endline

    # --- 6. sortie ---
    print "tete\tdecl\tfin\tnom\tva\tbandeau"
    for (k = 1; k <= nm; k++) {
        name = nameOf(mText[k])
        va   = ""
        if (name ~ /_[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]+$/) {
            va = name; sub(/^.*_/, "", va)
            if (va !~ /^[0-9A-Fa-f]+$/ || length(va) < 6) va = ""
        }
        printf "%d\t%d\t%d\t%s\t%s\t%d\n", mHead[k], mLine[k], mEnd[k], name, va, banner[mLine[k]]
    }
}

# L'identifiant declare : le dernier mot avant `(`, `=`, `;`, `=>` ou `{`.
function nameOf(s,   t, n, i, p) {
    t = s
    sub(/^[ ]*/, "", t)
    # coupe a la premiere parenthese, affectation, point-virgule ou accolade
    p = index(t, "(")
    if (p > 0) t = substr(t, 1, p - 1)
    sub(/=.*$/, "", t)
    sub(/;.*$/, "", t)
    sub(/\{.*$/, "", t)
    sub(/=>.*$/, "", t)
    gsub(/[ ]+$/, "", t)
    n = split(t, w, /[ ]+/)
    return (n >= 1) ? w[n] : "?"
}
