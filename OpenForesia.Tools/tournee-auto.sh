#!/bin/sh
# tournee-auto.sh — la liste des etapes de la tournee automatique du banc.
#
# Lit Documentation/Etat/GestionnairesEntites.md et sort, sur une ligne, les sous-zones qui
# portent encore au moins un gestionnaire « teste = non », sous la forme que le jeu attend
# dans OPENFORESIA_AI_BENCH_TOUR :
#
#   0:0,1:0,1:1,…
#
# Avec --detail, une ligne par etape : zone, sous-zone, gestionnaires non testes, dont IA (0056AD48).
#
# Regenerer GestionnairesEntites.md d'abord (tests\update-GestionnaireEntite.bat) : une sous-zone
# dont tout a tourne disparait alors d'elle-meme de la tournee suivante.
#
# Une sous-zone listee ici n'est pas une promesse : la colonne « sous-zones » dit ou une TABLE est
# installee, pas ou chaque decor est pose. Le banc peuple toujours les IA ; le reste tourne s'il
# est pose pres du point d'entree ou si un combat le fait naitre.

cd "$(dirname "$0")/.." || exit 1

awk -v detail="$1" '
/^## Zone [0-9]+/ { z = $3; next }
/^## / { z = ""; next }
z != "" && /\*\*non\*\*/ {
    split($0, f, "|")
    gsub(/[ `]/, "", f[3]); gsub(/ /, "", f[4])
    n = split(f[3], s, ",")
    for (i = 1; i <= n; i++) {
        k = z ":" s[i]
        if (!(k in c)) { order[++m] = k }
        c[k]++
        if (f[4] ~ /0056AD48/) a[k]++
    }
}
END {
    # tri zone puis sous-zone, numerique
    for (i = 1; i <= m; i++) for (j = i + 1; j <= m; j++) {
        split(order[i], p, ":"); split(order[j], q, ":")
        if (q[1] + 0 < p[1] + 0 || (q[1] + 0 == p[1] + 0 && q[2] + 0 < p[2] + 0)) {
            t = order[i]; order[i] = order[j]; order[j] = t
        }
    }
    if (detail == "--detail") {
        for (i = 1; i <= m; i++) {
            split(order[i], p, ":")
            printf "%s\t%s\t%d\t%d\n", p[1], p[2], c[order[i]], a[order[i]] + 0
        }
        exit
    }
    line = ""
    for (i = 1; i <= m; i++) line = line (i > 1 ? "," : "") order[i]
    print line
}' Documentation/Etat/GestionnairesEntites.md
