# Le corps de `census-dll-globals.sh` — la carte des globales d'une DLL.
#
# Entrees, dans cet ordre :
#   <dll>.c          la decompilation
#   ports.tsv        adresse sans zeros de tete   (les corps deja portes en C#)
#   racines.tsv      adresse sans zeros de tete   (les corps qu'un [DllImport] atteint)
#
# Variable : dll.
#
# ============================================================================================
# CE QU'IL CALCULE, ET POURQUOI
#
# Porter un corps en C# le coupe de la DLL. Deux choses l'empechent de partir seul :
#
#   1. **ses appelees.** Elles ne sont pas exportees, donc un corps C# ne peut pas les appeler ;
#      elles doivent partir avec lui.
#   2. **les globales qu'il ECRIT.** Si un corps reste natif et lit la meme globale, il lira la
#      copie de la DLL, restee figee. Deux lecteurs ne se genent pas ; un ecrivain et un lecteur,
#      si.
#
# Le script soude donc les corps par ces deux relations et sort les COMPOSANTES obtenues. Une
# composante est un lot indivisible : on la porte entiere ou pas du tout.
#
# ============================================================================================
# LA LIMITE DE LA CLASSIFICATION, DITE AVANT LES CHIFFRES
#
# Ghidra ecrit une globale de tableau de trois facons, et la troisieme est indecidable :
#
#     DAT_x = v;                 ecriture, evidente
#     (&DAT_x)[i] = v;           ecriture par indice — le cote du `=` tranche
#     p = &DAT_x; ... *p = v;    ecriture par alias — RIEN dans la ligne ne le dit
#
# La regle retenue : `&DAT_x` suivi d'un `)` et d'un `[` est un acces indice, classe par le cote
# du `=` ; tout autre `&DAT_x` est tenu pour une ECRITURE POSSIBLE. C'est volontairement pessimiste
# — une adresse prise peut toujours finir en ecriture —, donc les composantes sorties sont un
# MAJORANT : elles ne manquent aucune soudure, elles peuvent en inventer. Un corps qu'on veut
# detacher d'une composante se verifie a la main, il ne se croit pas sur parole.

function norm(a) { sub(/^0+/, "", a); return a }

function milliers(n,   s, out) {
    s = sprintf("%d", n)
    while (length(s) > 3) { out = " " substr(s, length(s) - 2) out; s = substr(s, 1, length(s) - 3) }
    return s out
}

# --- union-find ---------------------------------------------------------------------------
function trouve(x) { while (pere[x] != x) { pere[x] = pere[pere[x]]; x = pere[x] } return x }
function unir(a, b,   ra, rb) {
    ra = trouve(a); rb = trouve(b)
    if (ra != rb) pere[ra] = rb
}

BEGIN { FS = "\t" }

FILENAME ~ /ports\.tsv$/   { porte[$1] = 1; next }
FILENAME ~ /racines\.tsv$/ { racine[$1] = $2; next }

# --- la decompilation -----------------------------------------------------------------------
#
# `FS` vaut une tabulation pour les deux TSV ci-dessus, donc les champs d'une ligne de C ne sont
# PAS decoupes : l'en-tete de fonction se relit a la main. Premiere version du script : `$2` et
# `$4` rendaient la ligne entiere, aucune fonction n'etait reconnue, et le document sortait VIDE
# sans une seule erreur — le mode d'echec le plus couteux de ce depot.
FILENAME ~ /\.c$/ && /^\/\* [A-Za-z_][A-Za-z0-9_@$]* @ [0-9a-f]+ \*\/$/ {
    split($0, entete, " ")
    cur = entete[2]
    if (cur == "") next
    at[cur] = entete[4]; lignes[cur] = 0; ordre[++nfn] = cur
    pere[cur] = cur
    next
}

FILENAME ~ /\.c$/ && cur != "" {
    lignes[cur]++
    ligne = $0

    # La position du premier `=` d'affectation : ni `==`, ni `!=`, ni `<=`, ni `>=`.
    coupe = 0
    for (i = 1; i <= length(ligne); i++) {
        c = substr(ligne, i, 1)
        if (c != "=") continue
        if (substr(ligne, i + 1, 1) == "=") { i++; continue }
        p = substr(ligne, i - 1, 1)
        if (p == "!" || p == "<" || p == ">" || p == "=") continue
        coupe = i
        break
    }

    reste = ligne
    decalage = 0
    # Les prefixes composes doivent passer AVANT les simples : `PTR_DAT_100542a0` se lisait
    # `PTR_DA` — `D` et `A` sont des chiffres hexadecimaux, et la capture s'arretait au `T`. Le
    # tableau des globales portait alors une entree fantome a cinquante references.
    while (match(reste, /&?_?(PTR_(DAT|FUN|LAB)_|DAT_|EXT_|PTR_)[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]+/)) {
        debut = decalage + RSTART
        jeton = substr(reste, RSTART, RLENGTH)
        suite = substr(reste, RSTART + RLENGTH, 2)
        decalage += RSTART + RLENGTH - 1
        reste = substr(reste, RSTART + RLENGTH)

        adresse = jeton
        sub(/^&?_?(PTR_(DAT|FUN|LAB)_|DAT_|EXT_|PTR_)/, "", adresse)
        nom = jeton; sub(/^&/, "", nom)

        ecrit = 0
        if (substr(jeton, 1, 1) == "&") {
            # `&X)[` est un acces indice ; le reste est une adresse prise, donc suspecte.
            if (suite ~ /^\)\[/) ecrit = (coupe > 0 && debut < coupe)
            else ecrit = 1
        } else {
            ecrit = (coupe > 0 && debut < coupe)
        }

        cle = cur SUBSEP nom
        if (!(cle in vu)) {
            vu[cle] = 1
            touche[nom] = touche[nom] " " cur
        }
        if (ecrit) {
            cle2 = cur SUBSEP nom
            if (!(cle2 in vuw)) { vuw[cle2] = 1; ecrivains[nom] = ecrivains[nom] " " cur }
        }
    }

    # Les appels : une appelee non exportee part avec son appelant.
    l2 = ligne
    while (match(l2, /FUN_[0-9a-fA-F]+\(/)) {
        appelee = substr(l2, RSTART, RLENGTH - 1)
        l2 = substr(l2, RSTART + RLENGTH)
        if (appelee == cur) continue
        cle = cur SUBSEP appelee
        if (!(cle in vuc)) { vuc[cle] = 1; appels[cur] = appels[cur] " " appelee }
    }
}

END {
    # --- soudure 1 : appelant -> appelee ---------------------------------------------------
    for (f in appels) {
        n = split(appels[f], cs, " ")
        for (i = 1; i <= n; i++) if (cs[i] in pere) { unir(f, cs[i]); nappel++ }
    }

    # --- soudure 2 : un ecrivain avec tous ceux qui touchent la meme globale ----------------
    for (g in ecrivains) {
        ne = split(ecrivains[g], es, " ")
        nt = split(touche[g], ts, " ")
        if (nt < 2) continue
        for (i = 1; i <= ne; i++)
            for (j = 1; j <= nt; j++)
                if (es[i] != ts[j]) { unir(es[i], ts[j]); soude[g] = soude[g] + 1 }
    }

    # --- les composantes --------------------------------------------------------------------
    for (i = 1; i <= nfn; i++) {
        f = ordre[i]
        r = trouve(f)
        taille[r]++
        poids[r] += lignes[f]
        if (norm(at[f]) in racine) { pts[r] = pts[r] " " racine[norm(at[f])]; npts[r]++ }
        if (norm(at[f]) in porte) nporte[r]++
        membres[r] = membres[r] " " f
    }

    # =========================================================================================
    printf "# Les globales de `%s`, et les lots qu'elles imposent\n\n", dll
    print "**Genere** par `sh OpenForesia.Tools/census-dll-globals.sh` — ne pas editer a la main."
    print ""
    print "Ce document repond a une question que `DLLAtteignables.md` ne sait pas poser : **quels"
    print "corps sont obliges de partir ensemble**. Deux relations les soudent — un appel vers une"
    print "fonction non exportee, et une globale qu'un corps ECRIT pendant qu'un autre la touche."
    print "Le resultat est un decoupage en composantes, et **une composante se porte entiere ou pas"
    print "du tout**."
    print ""
    print "La classification lecture/ecriture est **volontairement pessimiste** : une adresse prise"
    print "(`&DAT_x`) hors acces indice compte comme une ecriture possible, parce que rien dans la"
    print "ligne ne dit ce qu'un alias en fera. Les composantes sont donc un MAJORANT — elles ne"
    print "manquent aucune soudure, elles peuvent en inventer. Detacher un corps d'une composante se"
    print "verifie a la main."
    print ""

    # --- tri des composantes par poids ------------------------------------------------------
    m = 0
    for (r in taille) comp[m++] = r
    for (x = 0; x < m; x++) for (y = x + 1; y < m; y++)
        if (poids[comp[y]] > poids[comp[x]] || (poids[comp[y]] == poids[comp[x]] && comp[y] < comp[x]))
            { t = comp[x]; comp[x] = comp[y]; comp[y] = t }

    print "## Les composantes qui portent au moins un point d'entree"
    print ""
    print "| # | corps | lignes | portes | points d'entree |"
    print "|---:|---:|---:|---:|---|"
    rang = 0
    for (x = 0; x < m; x++) {
        r = comp[x]
        if (npts[r] + 0 == 0) continue
        rang++
        liste = pts[r]
        gsub(/^ /, "", liste); gsub(/ /, ", ", liste)
        if (length(liste) > 300) liste = substr(liste, 1, 297) "..."
        printf "| %d | %s | %s | %d | %s |\n", rang, milliers(taille[r]), milliers(poids[r]), nporte[r] + 0, liste
    }
    print ""

    # --- les globales qui soudent le plus ---------------------------------------------------
    print "## Les globales qui soudent le plus"
    print ""
    print "Colonne **touchee par** : le nombre de corps qui la nomment. Colonne **ecrite par** :"
    print "ceux qui, selon la regle ci-dessus, pourraient l'ecrire. Une globale a un seul ecrivain"
    print "et beaucoup de lecteurs est le bon endroit ou chercher une coupure : si l'ecrivain part"
    print "avec tous ses lecteurs, la soudure disparait."
    print ""
    print "| globale | touchee par | ecrite par |"
    print "|---|---:|---:|"
    k = 0
    for (g in touche) { nt = split(touche[g], ts, " "); if (nt >= 3) { gl[k] = g; cnt[g] = nt; k++ } }
    for (x = 0; x < k; x++) for (y = x + 1; y < k; y++)
        if (cnt[gl[y]] > cnt[gl[x]] || (cnt[gl[y]] == cnt[gl[x]] && gl[y] < gl[x]))
            { t = gl[x]; gl[x] = gl[y]; gl[y] = t }
    for (x = 0; x < k && x < 25; x++) {
        g = gl[x]
        ne = split(ecrivains[g], es, " ")
        printf "| `%s` | %d | %d |\n", g, cnt[g], ne
    }
    print ""
    printf "%d globales sont touchees par trois corps ou plus.\n", k
    print ""
}
