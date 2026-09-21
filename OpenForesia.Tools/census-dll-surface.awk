# Le corps de `census-dll-surface.sh` — appele une fois par DLL, en deux modes.
#
#   mode=sum      une ligne TSV de synthese, que le script met en tableau ;
#   mode=detail   la section Markdown complete de cette DLL.
#
# Entrees, dans cet ordre exact :
#   <dll>.exports.tsv   ordinal \t RVA \t nom \t FWD        (lu dans le PE)
#   <dll>.graph.tsv     FN \t nom \t adresse \t lignes \t appels
#   roots.tsv           dll \t point d'entree               (les [DllImport])
#   impl.tsv            dll \t adresse sans zeros de tete   (la colonne implemente)
#   sisters.tsv         dll \t export nomme                 (la colonne « chez »)
#   blazeord.txt        ordinal                             (ce que Blaze.c appelle)
#
# Variables : dll, base (ImageBase du PE), mode.

function norm(a) { sub(/^0+/, "", a); return a }

function milliers(n,   s, out) {
    s = sprintf("%d", n)
    while (length(s) > 3) { out = " " substr(s, length(s) - 2) out; s = substr(s, 1, length(s) - 3) }
    return s out
}

function etat(f) { return (norm(at[f]) in impl) ? "oui" : "**non**" }

# Plie un paragraphe a la largeur du reste de la documentation. Les textes de role et de cas vide
# sont ecrits d'un seul tenant dans le BEGIN ; sans cela ils sortiraient en lignes de 600 colonnes,
# lisibles par un rendu Markdown mais pas par `git diff`.
function plier(s, largeur,   n, i, ligne, mots) {
    n = split(s, mots, " ")
    ligne = ""
    for (i = 1; i <= n; i++) {
        if (ligne == "") { ligne = mots[i]; continue }
        if (length(ligne) + 1 + length(mots[i]) > largeur) { print ligne; ligne = mots[i] }
        else ligne = ligne " " mots[i]
    }
    if (ligne != "") print ligne
}

# Tri decroissant par lignes, puis croissant par adresse. Le second critere n'est pas cosmetique :
# sans lui les corps de meme taille sortent dans l'ordre de hachage d'awk, qui change d'une machine
# a l'autre — et ce fichier est suivi par git.
function tri(tab, m,   x, y, t) {
    for (x = 0; x < m; x++)
        for (y = x + 1; y < m; y++)
            if (lines[tab[y]] > lines[tab[x]] ||
                (lines[tab[y]] == lines[tab[x]] && at[tab[y]] < at[tab[x]]))
                { t = tab[x]; tab[x] = tab[y]; tab[y] = t }
}

BEGIN {
    FS = "\t"
    role["wsyskern"] = "La feuille du graphe : tout le monde en depend, elle ne depend d\47aucune des notres. Services generaux — bitmaps et DIB, gamma, tableaux de chaines, memoire."
    role["winsys3d"] = "Le portage sur Win32 de la BIBLIOTHEQUE PSY-Q de la PlayStation : libgpu (liste d\47affichage), libgte (le coprocesseur geometrique), le son, la carte memoire, le clavier. C\47est le vrai sujet du chantier."
    role["3dhelp"]   = "Le device DirectDraw / Direct3D, charge dynamiquement par winsys3d (`LoadLibraryA` + `GetProcAddress(\"Create3DDevice\")`, winsys3d.c:5854). UN SEUL EXPORT. Deja remplace en pratique par `Host/SyntheticDevice.cs`."
    role["gldhelp"]  = "Le meme contrat que 3dhelp, mais sur Glide — les cartes 3Dfx Voodoo. UN SEUL EXPORT, et le corps n\47est guere que des relais vers `glide2x.dll`, qui n\47est pas livree. Inatteignable sur une machine d\47aujourd\47hui."
    role["playst"]   = "Le SDK PSY-Q de Sony, porte sur PC : les noms d\47exports sont ceux de la documentation PlayStation (`GsSortObject5`, `RotTransPers`, `SsSeqOpenJ`, `CdSearchFile`, `card_read`). Selon toute vraisemblance la generation qui a precede winsys3d, restee sur le CD."

    # Le message du cas vide, par DLL. « Aucun point d'entree employe » ne veut pas dire la meme
    # chose pour une DLL que personne ne charge et pour un device que winsys3d appelle par sa
    # VTABLE — un chemin que ce releve, qui ne suit que les appels nommes, ne peut pas voir.
    vide["3dhelp"]  = "**Aucun point d\47entree employe, et c\47est trompeur.** `Create3DDevice` est son unique export ; tout le reste de la DLL s\47appelle par la VTABLE du device qu\47il rend, un chemin que ce releve ne suit pas. `winsys3d` y adresse le rendu entier. Ce que la reconstruction en fait est deja tranche ailleurs : `Host/SyntheticDevice.cs` rend son propre device a la place, et `Host/DeviceShim.cs` detourne les slots du chemin chaud vers Direct3D 11. Le jour ou `winsys3d` sera en C#, plus personne ne chargera cette DLL — elle sortira d\47elle-meme."
    vide["gldhelp"] = "**Aucun point d\47entree employe.** Meme forme que `3dhelp` — un seul export, le reste par vtable — mais sans l\47equivalent de `SyntheticDevice` parce que rien ne l\47atteint : `glide2x.dll` n\47est pas livree avec le jeu et aucune machine d\47aujourd\47hui ne porte de carte 3Dfx. Transcriptible a peu de frais si on veut la completude, jamais executable."
    vide["playst"]  = "**Aucun point d\47entree employe, et personne ne la charge.** Ni `Blaze.exe` ni `winsys3d.dll` ne nomme `playst.dll` — voir le graphe de dependances plus haut, releve dans les binaires. Elle n\47exporte pas non plus `Create3DDevice`, donc elle ne peut pas servir de device. **Rien a transcrire**, et le jour ou cela change, ce document le dira."
}

FILENAME ~ /exports\.tsv$/ {
    nexports++
    orva[$1] = $2; onom[$1] = $3; ofwd[$1] = $4
    if ($3 != "-") byname[$3] = $1
    next
}

FILENAME ~ /graph\.tsv$/ {
    f = $2
    known[f] = 1; at[f] = $3; lines[f] = $4; calls[f] = $5
    byaddr[$3] = f
    bynorm[norm($3)] = f
    nfn++; totlines += $4
    next
}

FILENAME ~ /roots\.tsv$/ { if ($1 == dll) want[$2] = 1; next }
FILENAME ~ /impl\.tsv$/  { if ($1 == dll) impl[$2] = 1; next }
FILENAME ~ /sisters\.tsv$/ {
    # Un meme nom peut etre exporte par plusieurs soeurs — `ScaleMatrix` l'est par wsyskern ET par
    # playst. On les accumule au lieu d'en retenir une au hasard : la colonne est un indice, et un
    # indice ambigu doit se voir.
    if ($1 != dll) soeur[$2] = (soeur[$2] == "" ? $1 : soeur[$2] ", " $1)
    next
}
FILENAME ~ /crt\.tsv$/   { if ($1 == dll) amorce[$2] = 1; next }
FILENAME ~ /blazeord/    { bord[$1] = 1; next }

END {
    # --- resolution des racines ---------------------------------------------
    nq = 0
    for (ep in want) {
        if (ep ~ /^#[0-9]+$/) o = substr(ep, 2); else o = byname[ep]
        if (o == "" || !(o in orva)) { norse = norse " " ep; nnorse++; continue }
        addr = sprintf("%08x", base + orva[o])
        f = byaddr[addr]
        epord[o] = ep
        nemployes++
        if (ofwd[o] == "FWD") { fwdroot = fwdroot " " o; continue }
        if (f == "") { nobody = nobody " " o; nnobody++; continue }
        rootfn[o] = f
        if (!(f in reach)) { reach[f] = 1; queue[++nq] = f; isroot[f] = 1 }
    }

    # LES CORPS DEJA TRANSCRITS SONT AUSSI DES RACINES, et c'est ce qui rend ce releve MONOTONE.
    # Transcrire une fonction fait disparaitre son `[DllImport]` — donc sa racine, donc tout ce
    # qu'elle seule atteignait. Sans cette seconde source, le devis retrecirait a chaque fonction
    # ecrite et le document se feliciterait d'un travail qu'il aurait cesse de voir. Une adresse
    # portee en C# est atteinte par definition : elle entre dans la cloture au meme titre.
    for (a in impl) {
        f = bynorm[a]                            # impl[] et bynorm[] sont sans zeros de tete
        if (f == "" || (f in reach)) continue
        reach[f] = 1; queue[++nq] = f; isroot[f] = 1; nporte++
    }

    # --- cloture -------------------------------------------------------------
    i = 1
    while (i <= nq) {
        f = queue[i++]
        c = split(calls[f], cs, " ")
        for (j = 1; j <= c; j++) {
            g = cs[j]
            if (!(g in known)) { ext[g]++; continue }
            indeg[g]++
            if (!(g in reach)) { reach[g] = 1; queue[++nq] = g }
        }
    }

    # --- la CRT liee statiquement --------------------------------------------
    #
    # Elle fait partie de la cloture et n'est PAS du sujet : `__alldiv` est un `long` en C#,
    # `_malloc` est le tas du runtime. Elle se rebranche, elle ne se transcrit pas, et la laisser
    # dans le devis le gonflerait d'un travail qui n'existe pas.
    #
    # Deux sources, parce qu'une seule ne suffit pas. Ghidra nomme une partie de la CRT depuis sa
    # base de signatures — souligne en tete, `_strncpy`, `__ftol`, `__local_unwind2` — mais il en
    # laisse beaucoup en `FUN_…` anonyme, et rien de mecanique ne les distingue d'un corps de la
    # DLL. Celles-la viennent de `crt-dll.txt`, une par une, avec leur preuve. Sur wsyskern elles
    # pesaient 40 fonctions et 1 655 lignes, soit les trois quarts du devis apparent.
    #
    # LA DESCENDANCE D'UNE AMORCE EST CRT. C'est ce qui fait tenir un fichier de trois lignes : le
    # tas a petits blocs de MSVC ne s'appelle que lui-meme. Une amorce mal posee emporte en
    # revanche tout son sous-arbre — d'ou l'exigence de preuve.
    ncrt = 0; crtlines = 0; nq2 = 0
    for (f in reach) {
        if (f ~ /^_/ || (norm(at[f]) in amorce)) { iscrt[f] = 1; q2[++nq2] = f }
    }
    i = 1
    while (i <= nq2) {
        f = q2[i++]
        c = split(calls[f], cs, " ")
        for (j = 1; j <= c; j++) {
            g = cs[j]
            if (!(g in reach) || (g in iscrt)) continue
            iscrt[g] = 1; q2[++nq2] = g
        }
    }

    nreach = 0; reachlines = 0; nimpl = 0
    for (f in reach) {
        nreach++; reachlines += lines[f]
        if (f in iscrt) { ncrt++; crtlines += lines[f] }
        else if (norm(at[f]) in impl) nimpl++
    }
    devis = reachlines - crtlines

    if (mode == "sum") {
        printf "%s\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%.0f\t%d\n",
               dll, nfn, totlines, nemployes, nreach - ncrt, devis, ncrt, crtlines,
               (totlines ? 100 * devis / totlines : 0), nimpl
        exit
    }

    # --- la section -----------------------------------------------------------
    print ""
    print "---"
    print ""
    printf "## `%s` — %s export%s, %s fonctions, %s lignes\n",
           dll, milliers(nexports), (nexports > 1 ? "s" : ""), milliers(nfn), milliers(totlines)
    print ""
    plier(role[dll], 98)
    print ""

    if (nemployes == 0) {
        plier(vide[dll] != "" ? vide[dll] : \
              "**Aucun point d\47entree employe.** Ni `Blaze.exe` ni la reconstruction n\47importe " \
              "quoi que ce soit de cette DLL. Rien a transcrire tant que cela reste vrai.", 98)
        print ""
        return_early = 1
    }

    if (!return_early) {
        printf "**%s points d\47entree employes**, dont la cloture d\47appels couvre **%s fonctions**,\n",
               milliers(nemployes), milliers(nreach)
        printf "soit **%s lignes de Ghidra sur %s**. Retranchee la CRT liee statiquement — %s fonctions,\n",
               milliers(reachlines), milliers(totlines), milliers(ncrt)
        printf "%s lignes, qui se rebranchent au lieu de se transcrire —, le devis est de **%s lignes,\n",
               milliers(crtlines), milliers(devis)
        printf "%.0f %% de la DLL**. Le reste n\47est atteint par rien de ce que la reconstruction importe\n",
               (totlines ? 100 * devis / totlines : 0)
        print "aujourd\47hui."
        print ""
        if (nporte > 0) {
            printf "**%d corps entrent dans la cloture par la reconstruction et non par un `[DllImport]`** :\n", nporte
            print "ils sont deja transcrits, donc leur import a disparu. Ils restent comptes — c\47est ce qui"
            print "empeche le devis de retrecir a mesure qu\47on avance."
            print ""
        }
        printf "### Les points d\47entree employes (%d)\n", nemployes
        print ""
        print "| ordinal | nom d\47export | adresse | fonction | lignes | implemente |"
        print "|---:|---|---|---|---:|---|"

        m = 0
        for (o in epord) ords[m++] = o + 0
        for (x = 0; x < m; x++) for (y = x + 1; y < m; y++)
            if (ords[y] < ords[x]) { t = ords[x]; ords[x] = ords[y]; ords[y] = t }
        for (x = 0; x < m; x++) {
            o = ords[x]
            f = rootfn[o]
            printf "| %d | %s | `0x%08x` | %s | %s | %s |\n",
                   o, (onom[o] == "-" ? "—" : "`" onom[o] "`"), base + orva[o],
                   (f == "" ? "*(non decompilee)*" : "`" f "`"),
                   (f == "" ? "—" : milliers(lines[f])),
                   (f == "" ? "—" : etat(f))
        }
        print ""

        if (nnobody > 0) {
            printf "**%d de ces points d\47entree n\47ont pas de corps dans `%s.c`.** Ghidra ne les a pas\n", nnobody, dll
            print "decompiles — code atteint seulement par son adresse, le plus souvent. Ils se transcrivent"
            print "au desassembleur :"
            print ""
            printf "```\ndotnet run --project OpenForesia.Tools -- disasm %s/%s.dll <adresse> <octets>\n```\n", "Legacy/Binaries", dll
            print ""
        }

        if (nnorse > 0) {
            printf "**%d racines non resolues** : la table d\47exports ne les connait pas —%s.\n", nnorse, norse
            print "Un nom mal orthographie dans un `[DllImport]`, ou un export disparu. A regarder."
            print ""
        }

        printf "### La cloture — ce qu\47il faut transcrire (%s fonctions, %s lignes)\n",
               milliers(nreach - ncrt), milliers(devis)
        print ""
        print "Triee par taille decroissante : c\47est le devis, du plus cher au moins cher. **Les lignes"
        print "sont des lignes de Ghidra**, declarations de variables comprises — une mesure de cout,"
        print "pas un compte de lignes C# a ecrire. La colonne **appelants** compte les corps de la"
        print "cloture qui menent a celui-la ; une fonction a zero appelant est une racine, appelee"
        print "directement par la reconstruction, et son nom est en gras."
        print ""
        print "| adresse | fonction | lignes | appelants | implemente |"
        print "|---|---|---:|---:|---|"

        m = 0
        for (f in reach) if (!(f in iscrt)) cl[m++] = f
        tri(cl, m)
        for (x = 0; x < m; x++) {
            f = cl[x]
            printf "| `0x%s` | %s | %s | %d | %s |\n",
                   at[f], (isroot[f] ? "**`" f "`**" : "`" f "`"),
                   milliers(lines[f]), indeg[f] + 0, etat(f)
        }
        print ""

        if (ncrt > 0) {
            printf "### La CRT liee statiquement (%s fonctions, %s lignes) — hors devis\n",
                   milliers(ncrt), milliers(crtlines)
            print ""
            print "La DLL embarque sa bibliotheque standard, et la cloture la traverse. Ghidra la nomme"
            print "depuis sa base de signatures — d\47ou le souligne en tete. **Rien de tout cela ne se"
            print "transcrit** : `__alldiv`, `__allmul` et `__allshr` sont l\47arithmetique 64 bits que le"
            print "compilateur de 1998 appelait et qu\47un `long` C# fait nativement ; `__ftol` est une"
            print "conversion ; `__local_unwind2` et `__amsg_exit` sont le deroulage SEH ; le reste est"
            print "`string.h`, deja porte dans `Platform/Crt.cs`."
            print ""
            print "| adresse | fonction | lignes |"
            print "|---|---|---:|"
            m = 0
            for (f in iscrt) cr[m++] = f
            tri(cr, m)
            for (x = 0; x < m; x++) printf "| `0x%s` | `%s` | %s |\n", at[cr[x]], cr[x], milliers(lines[cr[x]])
            print ""
        }

        m = 0
        for (g in ext) m++
        if (m > 0) {
            printf "### Ce qui sort de la DLL (%d appels distincts)\n", m
            print ""
            print "Les appels que la cloture adresse en dehors de la DLL. Ils ne se transcrivent pas — ils"
            print "se rebranchent : sur `Platform/Win32.cs` pour Win32, sur `Platform/Crt.cs` pour la CRT,"
            print "et sur la reconstruction de la DLL soeur quand la colonne **chez** en nomme une. Cette"
            print "derniere colonne est l\47argument de l\47ordre de travail : une DLL dont les appels"
            print "sortants retombent chez une soeur se transcrit APRES elle, sinon le portage s\47appuie"
            print "sur du natif qu\47il faudra rebrancher deux fois."
            print ""
            print "| appel | sites | chez |"
            print "|---|---:|---|"
            m = 0
            for (g in ext) exts[m++] = g
            for (x = 0; x < m; x++) for (y = x + 1; y < m; y++)
                if (ext[exts[y]] > ext[exts[x]] || (ext[exts[y]] == ext[exts[x]] && exts[y] < exts[x]))
                    { t = exts[x]; exts[x] = exts[y]; exts[y] = t }
            for (x = 0; x < m; x++)
                printf "| `%s` | %d | %s |\n", exts[x], ext[exts[x]],
                       (exts[x] in soeur ? "`" soeur[exts[x]] "`" : "—")
            print ""
        }
    }

    # --- ce que Blaze.c appelle et que Interop n'importe pas encore -----------
    if (dll == "winsys3d") {
        avenir = ""; navenir = 0
        for (o in bord) {
            if (o in epord) continue            # deja importe
            if (!(o in orva)) continue
            # Deja transcrit : l'ordinal n'a plus de `[DllImport]` parce qu'il n'en a plus besoin,
            # pas parce que son appelant manque. Sans ce test, une fonction finie reapparait dans
            # la liste de ce qui reste a faire.
            f = byaddr[sprintf("%08x", base + orva[o])]
            if (f != "" && (norm(at[f]) in impl)) continue
            avenir = avenir sprintf(" %d", o)
            navenir++
        }
        if (navenir > 0) {
            printf "### Appeles par `Blaze.c`, pas encore importes (%d)\n", navenir
            print ""
            print "Ces ordinaux apparaissent dans la decompilation du jeu mais aucun `[DllImport]` ne les"
            print "declare : la fonction appelante n\47est pas encore transcrite. Ils rejoindront la"
            print "cloture le jour ou elle le sera — c\47est le compteur qui dit que ce releve n\47est pas"
            print "un plafond."
            print ""
            n = split(avenir, av, " ")
            for (x = 1; x <= n; x++) for (y = x + 1; y <= n; y++)
                if (av[y] + 0 < av[x] + 0) { t = av[x]; av[x] = av[y]; av[y] = t }
            printf "| ordinal | adresse | fonction | lignes |\n|---:|---|---|---:|\n"
            for (x = 1; x <= n; x++) {
                o = av[x]
                addr = sprintf("%08x", base + orva[o])
                f = byaddr[addr]
                printf "| %d | `0x%s` | %s | %s |\n", o, addr,
                       (f == "" ? "*(non decompilee)*" : "`" f "`"),
                       (f == "" ? "—" : milliers(lines[f]))
            }
            print ""
        }
    }
}
