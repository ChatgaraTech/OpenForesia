# Construit le manifeste de decoupage : fichier <TAB> debut <TAB> fin.
#
# Attribution LIGNE PAR LIGNE, puis fusion des suites contigues. Trois regles, dans cet ordre :
#
#  1. Un BANDEAU (`// ====` … `// ====`) introduit une REGION. Il appartient au fichier de son
#     point de coupe, jamais au membre qui le suit. Sans cette regle, un membre redirige ailleurs
#     emporte le bandeau avec lui : les 17 lignes du bandeau « Le menu objets » partaient dans
#     Memory/Arenas.cs parce que les declarations d'arene sont les premiers membres apres lui.
#
#  2. Sinon, la ligne appartient au membre qui la couvre. La trivia de tete (lignes vides,
#     commentaires de doc, attributs) va au membre qui SUIT, mais la remontee S'ARRETE au
#     delimiteur de bandeau.
#
#  3. Le fichier d'un membre vient de son point de coupe, sauf redirection nominative.
#
# Le fichier des points de coupe et des redirections est partage avec inventory.awk : les deux
# doivent etre modifies ensemble.

BEGIN {
    FS = "\n"; nc = 0
    cut(17,    "Platform/Win32.cs");                   cut(243,   "Platform/Winsys3d.cs")
    cut(911,   "Engine/Model3d.cs");                   cut(1479,  "Platform/Crt.cs")
    cut(1851,  "Platform/Startup.cs");                 cut(2190,  "Screens/State00/Options.cs")
    cut(2320,  "Engine/Audio.cs");                     cut(3742,  "Engine/Video.cs")
    cut(4725,  "Platform/Startup.cs");                 cut(4797,  "Engine/Input.cs")
    cut(5338,  "Memory/Arena.GameState.cs");           cut(5597,  "Engine/Resources.cs")
    cut(6683,  "Screens/State00/MainMenu.cs");         cut(6938,  "Screens/State04/CharacterSelect.cs")
    cut(7532,  "Screens/State04/CharacterCreation.cs");cut(9025,  "Engine/SaveData.cs")
    cut(9620,  "Screens/State04/SaveSlots.cs");        cut(11703, "Memory/DataTables.cs")
    cut(12088, "Engine/Resources.cs");                 cut(12149, "Screens/State00/MainMenu.cs")
    cut(12410, "Engine/Primitives2d.cs");              cut(12655, "Engine/UiText.cs")
    cut(13989, "Engine/UiWindow.cs");                  cut(14339, "Screens/State00/Options.cs")
    cut(16645, "Engine/Input.cs");                     cut(17262, "Screens/GameStates.cs")
    cut(17362, "Screens/State03/WorldMap.cs");         cut(18471, "Screens/State01/PartySetup.cs")
    cut(19007, "Screens/State02/Destination.cs");      cut(19442, "Screens/DebugParty.cs")
    cut(20179, "Platform/Startup.cs");                 cut(20265, "Screens/State06/ItemMenu.cs")
    cut(20760, "Screens/State06/Inventory.cs")

    red(2517, "Platform/Winsys3d.cs");   red(2542, "Platform/Winsys3d.cs")
    red(2563, "Platform/Winsys3d.cs");   red(3619, "Engine/Input.cs")
    red(3641, "Engine/Input.cs");        red(3668, "Screens/State00/Options.cs")
    red(3717, "Screens/State00/Options.cs"); red(5175, "Screens/State00/Options.cs")
    red(5196, "Engine/Video.cs");        red(5224, "Engine/Video.cs")
    red(6467, "Screens/GameStates.cs");  red(12804, "Platform/Crt.cs")
    red(15914, "Engine/Audio.cs");       red(17204, "Screens/State03/WorldMap.cs")
    red(5862, "Memory/Arenas.cs");  red(5863, "Memory/Arenas.cs");  red(5864, "Memory/Arenas.cs")
    red(5866, "Memory/Arenas.cs");  red(5867, "Memory/Arenas.cs");  red(5868, "Memory/Arenas.cs")
    red(17608,"Memory/Arenas.cs");  red(17609,"Memory/Arenas.cs");  red(17610,"Memory/Arenas.cs")
    red(17614,"Memory/Arenas.cs");  red(17615,"Memory/Arenas.cs");  red(17616,"Memory/Arenas.cs")
    red(17620,"Memory/Arenas.cs");  red(17621,"Memory/Arenas.cs");  red(17622,"Memory/Arenas.cs")
    red(18490,"Memory/Arenas.cs");  red(18491,"Memory/Arenas.cs")
    red(20295,"Memory/Arenas.cs");  red(20296,"Memory/Arenas.cs");  red(20297,"Memory/Arenas.cs")
    red(20298,"Memory/Arenas.cs")
}
function cut(line, file) { nc++; cutLine[nc] = line; cutFile[nc] = file }
function red(line, file) { redir[line] = file }
function cutOf(l,   c) { for (c = nc; c >= 1; c--) if (cutLine[c] <= l) return cutFile[c]; return "(hors plage)" }

{ L[NR] = $0 }

END {
    # --- 1. les blocs de bandeau ---
    for (i = 1; i <= NR; i++) {
        if (L[i] !~ /^    \/\/ ={20,}$/) continue
        if (inBanner[i]) continue
        for (j = i; j <= NR; j++) {
            if (L[j] !~ /^[ ]*\/\//) break          # sortie de secours : le bandeau doit rester du commentaire
            inBanner[j] = 1
            if (j > i && L[j] ~ /^    \/\/ ={20,}$/) break
        }
    }

    # --- 2. les membres et leur tete de trivia ---
    nm = 0
    for (i = 1; i <= NR; i++) {
        d = L[i]
        if (d ~ /^    \[.*\][ ]*(private|internal|public|protected)[ ]/) sub(/^    \[.*\][ ]*/, "    ", d)
        if (d !~ /^    (private|internal|public|protected)[ ]/) continue
        nm++; mLine[nm] = i
    }
    for (k = 1; k <= nm; k++) {
        t = mLine[k]
        for (i = mLine[k] - 1; i >= 1; i--) {
            if (inBanner[i]) break                              # regle 1 : ne jamais franchir un bandeau
            if (L[i] ~ /^[ \t]*$/)               { t = i; continue }
            if (L[i] ~ /^[ ]*(\/\/|\/\*|\*|\[)/) { t = i; continue }
            break
        }
        if (k > 1 && t <= mLine[k-1]) t = mLine[k-1] + 1
        if (k == 1 && t < 17)         t = 17
        mHead[k] = t
        mFile[k] = (mLine[k] in redir) ? redir[mLine[k]] : cutOf(mLine[k])
    }

    # --- 3. attribution ligne par ligne ---
    k = 0
    for (i = 17; i <= NR; i++) {
        while (k < nm && i >= mHead[k+1]) k++
        owner[i] = inBanner[i] ? cutOf(i) : (k >= 1 ? mFile[k] : cutOf(i))
    }

    # --- 4. fusion des suites contigues ---
    for (i = 17; i <= NR; i++) {
        if (owner[i] != cf) {
            if (cf != "") printf "%s\t%d\t%d\n", cf, cs, i - 1
            cf = owner[i]; cs = i
        }
    }
    if (cf != "") printf "%s\t%d\t%d\n", cf, cs, NR
}
