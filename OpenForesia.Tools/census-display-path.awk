# Le recensement du CHEMIN D'AFFICHAGE de winsys3d : le seul lot qui reste dans la masse.
#
# Entrees, dans cet ordre :
#   winsys3d.c        la decompilation
#   racines.tsv       adresse (sans zeros de tete) -> ordinal importe
#   ports.tsv         les adresses deja transcrites
#
# CE QU'IL CALCULE. Le perimetre est ferme par une graine et une fermeture descendante :
#   graine    = tout corps qui touche le tampon de commandes ou un memo d'etat ;
#   fermeture = tout ce que la graine appelle.
# Le resultat est un LOT INDIVISIBLE : le mot « nombre » du dernier enregistrement du paquet est
# incremente sur place par le producteur suivant, donc deux moities ne peuvent pas coexister.
#
# L'ORDRE DE TRAVAIL sort du regroupement par SQUELETTE DE CONTROLE. C'est la lecon de
# `FUN_004747F0` : ce qui predit le cout est le nombre de formes distinctes, pas le nombre de
# lignes. Une famille de quinze corps au meme squelette, c'est une forme a comprendre et quinze
# transcriptions mecaniques.

function squelette(texte,   n, L, i, l, sq) {
    n = split(texte, L, "\n")
    sq = ""
    for (i = 1; i <= n; i++) {
        l = L[i]
        if (l ~ /^[ \t]*$/) continue
        if (l ~ /(^|[^A-Za-z_])if[ ]*\(/)             sq = sq "I"
        if (l ~ /(^|[^A-Za-z_])else([^A-Za-z_]|$)/)   sq = sq "E"
        if (l ~ /(^|[^A-Za-z_])for[ ]*\(/)            sq = sq "F"
        if (l ~ /(^|[^A-Za-z_])while[ ]*\(/)          sq = sq "W"
        if (l ~ /(^|[^A-Za-z_])do[ ]*\{/)             sq = sq "D"
        if (l ~ /(^|[^A-Za-z_])switch[ ]*\(/)         sq = sq "S"
        if (l ~ /(^|[^A-Za-z_])return([^A-Za-z_]|$)/) sq = sq "R"
        if (l ~ /FUN_[0-9a-fA-F]+\(/)                 sq = sq "C"
        if (l ~ /\*\*\(code \*\*\)/)                  sq = sq "V"
    }
    return sq == "" ? "-" : sq
}

function milliers(n,   s, r) {
    s = sprintf("%d", n); r = ""
    while (length(s) > 3) { r = " " substr(s, length(s) - 2) r; s = substr(s, 1, length(s) - 3) }
    return s r
}

FILENAME ~ /winsys3d\.c$/ {
    if ($0 ~ /^\/\* [A-Za-z_][A-Za-z0-9_]* @ [0-9a-f]+ \*\/$/) { nom = $2; adr = $4; sub(/^0+/, "", adr); adrs[nom] = adr; ordre[++nfn] = nom; next }
    if (nom == "") next
    corps[nom] = corps[nom] $0 "\n"
    lignes[nom]++

    l = $0
    while (match(l, /FUN_[0-9a-fA-F]+\(/)) {
        c = substr(l, RSTART, RLENGTH - 1); l = substr(l, RSTART + RLENGTH)
        if (c == nom) continue
        if (!((nom SUBSEP c) in vu))  { vu[nom SUBSEP c] = 1;  app[nom] = app[nom] " " c }
        if (!((c SUBSEP nom) in vi))  { vi[c SUBSEP nom] = 1;  par[c] = par[c] " " nom }
    }

    # La graine : le tampon (base, curseur, zone de sommets) et les memos d'etat.
    if ($0 ~ /DAT_011be7ac|DAT_011be7dc|DAT_011be7a8/)                 { graine[nom] = 1; tampon[nom] = 1 }
    if ($0 ~ /DAT_00a2c48[0-9a-c]|DAT_00a2c578|DAT_00a2c57c/)          { graine[nom] = 1; memos[nom] = 1 }
    if ($0 ~ /DAT_011be760/)                                            device[nom] = 1

    # Les globales, pour le tableau de queue.
    l2 = $0
    while (match(l2, /(EXT_|PTR_|_?DAT_)[0-9a-fA-F]{6,8}/)) {
        g = substr(l2, RSTART, RLENGTH); l2 = substr(l2, RSTART + RLENGTH)
        if (!((nom SUBSEP g) in vg)) { vg[nom SUBSEP g] = 1; ng[g]++ }
    }
    next
}

FILENAME ~ /racines\.tsv$/ { racine[$1] = $2; next }
FILENAME ~ /ports\.tsv$/   { porte[$1] = 1;   next }

END {
    # --- fermeture descendante depuis la graine -------------------------------------------
    for (f in graine) { q[++nq] = f; dans[f] = 1 }
    ngraine = nq
    i = 1
    while (i <= nq) {
        f = q[i++]
        n = split(app[f], cs, " ")
        for (j = 1; j <= n; j++)
            if (!(cs[j] in dans) && (cs[j] in lignes)) { dans[cs[j]] = 1; q[++nq] = cs[j] }
    }

    total = 0; nporte = 0
    for (f in dans) { total += lignes[f]; if (adrs[f] in porte) nporte++ }

    # --- les appelants restes dehors --------------------------------------------------------
    for (f in dans) {
        n = split(par[f], ps, " ")
        for (j = 1; j <= n; j++) if (!(ps[j] in dans) && (ps[j] in lignes)) dehors[ps[j]] = 1
    }

    # =========================================================================================
    print "# Le chemin d'affichage de `winsys3d` — le dernier lot"
    print ""
    print "**Genere** par `sh OpenForesia.Tools/census-display-path.sh` — ne pas editer a la main."
    print ""
    print "Tout ce qui se detachait de `winsys3d` est porte ; il ne reste que la masse, et dans la"
    print "masse il ne reste qu'un lot : le chemin d'affichage. Ce document le delimite, le chiffre,"
    print "et en donne l'ordre de travail."
    print ""
    print "## Le perimetre, et pourquoi il est indivisible"
    print ""
    print "La graine est **tout corps qui touche le tampon de commandes ou un memo d'etat** ; le"
    print "perimetre est sa fermeture descendante. Ce n'est pas un choix de decoupage, c'est une"
    print "contrainte : a etat inchange, un producteur **allonge l'enregistrement precedent** —"
    print "`paquet[curseur - 1] += n` — et range ses sommets a la suite dans la zone partagee. Deux"
    print "moities ne peuvent pas coexister ; l'une allongerait un enregistrement ecrit par l'autre."
    print ""
    printf "| | corps | lignes |\n|---|---:|---:|\n"
    printf "| graine (tampon ou memo) | %s | |\n", milliers(ngraine)
    printf "| **perimetre ferme** | **%s** | **%s** |\n", milliers(nq), milliers(total)
    printf "| deja transcrits | %s | |\n", milliers(nporte)
    print ""

    nd = 0; for (f in dehors) nd++
    printf "**%d corps restent dehors tout en appelant le perimetre**", nd
    if (nd > 0) {
        print " — ce sont les coutures, et elles sont toutes minuscules :"
        print ""
        print "| corps | adresse | lignes | ordinal |"
        print "|---|---|---:|---|"
        for (f in dehors)
            printf "| `%s` | `0x00%s` | %d | %s |\n", f, adrs[f], lignes[f],
                   (adrs[f] in racine) ? racine[adrs[f]] : "—"
    } else print "."
    print ""

    # --- les points d'entree importes dans le perimetre -------------------------------------
    print "## Les points d'entree importes qui tombent dedans"
    print ""
    print "Ce sont les `[DllImport]` que ce lot retirerait. La facade ne change qu'a la fin :"
    print "le lot se porte entier ou pas du tout."
    print ""
    print "| ordinal | corps | lignes |"
    print "|---|---|---:|"
    np = 0
    for (k = 1; k <= nfn; k++) {
        f = ordre[k]
        if (!(f in dans)) continue
        if (!(adrs[f] in racine)) continue
        printf "| **%s** | `%s` | %d |\n", racine[adrs[f]], f, lignes[f]
        np++
    }
    printf "\n**%d points d'entree.**\n\n", np

    # --- l'ordre de travail : par squelette --------------------------------------------------
    print "## L'ordre de travail — par forme, pas par taille"
    print ""
    print "Chaque corps est reduit a son **squelette de controle** : `I` un `if`, `E` un `else`,"
    print "`F`/`W`/`D` les boucles, `S` un `switch`, `R` un `return`, `C` un appel interne, `V` un"
    print "appel de vtable du device. Deux corps au meme squelette se transcrivent de la meme facon."
    print ""
    print "Les familles sont triees par population : **commencer par les plus peuplees**, une forme"
    print "comprise y vaut pour dix a quinze transcriptions."
    print ""
    for (f in dans) { sq = squelette(corps[f]); pop[sq]++; lsq[sq] += lignes[f]; membres[sq] = membres[sq] " " f }
    m = 0; for (s in pop) fam[m++] = s
    for (x = 0; x < m; x++) for (y = x + 1; y < m; y++)
        if (pop[fam[y]] > pop[fam[x]] || (pop[fam[y]] == pop[fam[x]] && lsq[fam[y]] > lsq[fam[x]]))
            { t = fam[x]; fam[x] = fam[y]; fam[y] = t }

    print "| # | corps | lignes | squelette | exemple |"
    print "|---:|---:|---:|---|---|"
    for (x = 0; x < m; x++) {
        s = fam[x]
        split(membres[s], mm, " ")
        sqa = length(s) > 40 ? substr(s, 1, 37) "..." : s
        printf "| %d | %d | %s | `%s` | `%s` |\n", x + 1, pop[s], milliers(lsq[s]), sqa, mm[1]
    }
    printf "\n**%d formes distinctes pour %s corps.**\n", m, milliers(nq)
    print ""

    # --- les globales du perimetre -----------------------------------------------------------
    print "## Les globales que le lot emporte"
    print ""
    print "Celles du noyau d'abord — elles sont la raison d'etre du perimetre —, puis les plus"
    print "partagees. Une globale citee ici ne doit **jamais** exister en double : tant que la"
    print "moitie native la relit, notre copie serait morte."
    print ""
    print "| globale | role | corps qui la nomment |"
    print "|---|---|---:|"
    printf "| `DAT_011be7ac` | le paquet courant — rempli, rejoue, libere | %d |\n", ng["DAT_011be7ac"]
    printf "| `DAT_011be7dc` | le curseur d'ecriture, en mots | %d |\n", ng["DAT_011be7dc"]
    printf "| `DAT_011be7a8` | la zone de sommets, dans le meme bloc | %d |\n", ng["DAT_011be7a8"]
    printf "| `DAT_00a2c480` | l'etat de rendu courant | %d |\n", ng["DAT_00a2c480"]
    printf "| `DAT_00a2c484` | memo d'etat | %d |\n", ng["DAT_00a2c484"]
    printf "| `DAT_00a2c488` | le type de primitive courant | %d |\n", ng["DAT_00a2c488"]
    printf "| `DAT_00a2c48c` | memo d'etat | %d |\n", ng["DAT_00a2c48c"]
    printf "| `DAT_00a2c578` | la derniere commande d'etat ecrite | %d |\n", ng["DAT_00a2c578"]
    printf "| `DAT_00a2c57c` | son argument | %d |\n", ng["DAT_00a2c57c"]
    printf "| `DAT_011be760` | le device — **une poignee, pas un etat** ; relue par l'ordinal 750 | %d |\n", ng["DAT_011be760"]
    print ""
    nt = 0; nm = 0; nv = 0
    for (f in dans) { if (f in tampon) nt++; if (f in memos) nm++; if (f in device) nv++ }
    printf "Dans le perimetre : **%d corps touchent le tampon**, **%d un memo**, **%d le device**.\n", nt, nm, nv
}
