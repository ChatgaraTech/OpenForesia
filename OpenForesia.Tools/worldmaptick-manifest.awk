# Construit le manifeste de decoupage de Screens/State03/WorldMapTick.cs :
# fichier <TAB> debut <TAB> fin.
#
# Jumeau de split-manifest.awk, meme algorithme, meme trois regles, dans cet ordre :
#
#  1. Un BANDEAU (`// ====` … `// ====`) introduit une REGION. Il appartient au fichier de son
#     point de coupe, jamais au membre qui le suit.
#
#  2. Sinon, la ligne appartient au membre qui la couvre. La trivia de tete (lignes vides,
#     commentaires de doc, attributs) va au membre qui SUIT, mais la remontee S'ARRETE au
#     delimiteur de bandeau.
#
#  3. Le fichier d'un membre vient de son point de coupe, sauf redirection nominative.
#
# CHAQUE POINT DE COUPE EST SOIT LA PREMIERE LIGNE D'UN BANDEAU, SOIT LA LIGNE DE DECLARATION
# D'UN MEMBRE. Aucune autre valeur n'est admise : la tache 1.2 le verifie contre l'inventaire
# produit par worldmaptick-inventory.awk, avec lequel ce fichier doit etre modifie ensemble.
#
# Un meme fichier peut apparaitre a plusieurs points de coupe : il recoit alors plusieurs plages,
# ce que le format du manifeste et apply-split.ps1 acceptent (decision D7).
#
# A LIRE DEPUIS LA REFERENCE FIGEE, JAMAIS DEPUIS LE FICHIER DE TRAVAIL. Des le premier lot,
# `WorldMapTick.cs` n'est plus que le reliquat : les numeros de ligne du manifeste sont ceux de la
# reference, qui ne bouge pas. Le garde-fou de fin de fichier le verifie et refuse de sortir un
# manifeste tronque - sans lui, l'erreur passe inapercue (elle rend un manifeste PLAUSIBLE, juste
# ampute de la moitie de ses plages).
#
# Usage :
#   awk -f worldmaptick-manifest.awk OpenForesia.Tools/worldmaptick-baseline.txt > worldmaptick-manifest.tsv

BEGIN {
    FS = "\n"; nc = 0
    startline = 25          # 1..24 = entete, using, namespace, declaration de classe, accolade
    endline   = 74071       # 74072 = accolade fermante, reposee par apply-split.ps1

    # ---- Tick : la boucle par image ----
    cut(25,    "Tick/WorldMapTick.cs")
    cut(776,   "Tick/WorldMapHud.cs")
    cut(2253,  "Tick/SpriteAtlas.cs")
    cut(2406,  "Tick/WorldSimulationStep.cs")

    # ---- Dispatch : le carrefour adresse -> methode ----
    cut(4048,  "Dispatch/StaticEntityTables.cs")
    cut(4279,  "Dispatch/EntityHandlerDispatch.cs")
    cut(4325,  "Dispatch/CoverageProbes.cs")
    cut(4454,  "Dispatch/EntityHandlerDispatch.cs")

    # ---- les marques d'etat, la trigonometrie, l'appariement de combat ----
    cut(5626,  "Effects/StatusMarkers.cs")
    cut(7152,  "Physics/AngleMath.cs")
    cut(7260,  "Effects/MiscMarkers.cs")
    cut(7585,  "Combat/CombatPairing.cs")

    # ---- la table des 48 effets et l'herboristerie ----
    cut(8041,  "Combat/EffectTable.cs")
    cut(8249,  "Combat/HerbEffects.cs")
    cut(8431,  "Combat/StatusEffects.cs")

    # ---- pieges, marques de coup, particules de la foret ----
    cut(9832,  "Effects/TrapScripts.cs")
    cut(10394, "Effects/HitAndResistMarkers.cs")
    cut(12296, "Effects/ForestParticlesA.cs")
    cut(14645, "Effects/ForestParticlesB.cs")

    # ---- l'embarcation, le maillage, les scenes, les objets, le tir ----
    cut(16083, "Handlers/Forest.cs")
    cut(16690, "Physics/TerrainMesh.cs")
    cut(17046, "Cutscene/ForestScenes.cs")
    cut(18918, "Items/ItemSpawns.cs")
    cut(20940, "Combat/Projectiles.cs")

    # ---- eveil, auberge, sons, coffres, resolution du coup ----
    cut(21503, "Entities/WakeState.cs")
    cut(22651, "Ai/Inn.cs")
    cut(23353, "Handlers/Sound.cs")
    cut(23934, "Handlers/Chests.cs")
    cut(24443, "Combat/HitResolution.cs")

    # ---- l'intelligence : le commun, puis une creature par fichier ----
    # Les betes sont nommees dans Documentation/Ennemies/Comportement.md, tableaux par bloc. Les
    # noms poses ici viennent de la : `BlockAPatrol` et `FlyingBeast` etaient des suppositions.
    # 0x0049A980 est le Goblin (bloc A, sous-type 2), mais il reste avec les feuilles partagees
    # qu'il a introduites - c'est la charpente de reference a cinq postures, et son bandeau le dit.
    cut(25765, "Ai/SceneEntityAiCommon.cs")   # 0x0049A980 Goblin st.2 + les feuilles partagees
    cut(26413, "Ai/Forest/GoblinShaman.cs")   # 0x0049E190 Goblin-sorcier, bloc A, sous-type 9
    cut(26864, "Ai/Forest/Rattlesnake.cs")    # 0x0049E920 Crotale,        bloc A, sous-type 18
    cut(27436, "Ai/Forest/GiantAnt.cs")       # 0x00499DE0 Fourmi geante,  bloc A, sous-type 0
    cut(27710, "Ai/Forest/Ladybug.cs")        # 0x0049A360 Coccinelle,     bloc A, sous-type 1
    cut(28190, "Ai/Forest/KillerBee.cs")      # 0x004A3C30 Abeille tueuse, bloc C, sous-type 3
    cut(28594, "Ai/Forest/FlyingFish.cs")
    cut(29118, "Ai/Forest/GiantCrab.cs")
    cut(29613, "Ai/Forest/Grizzly.cs")
    cut(30169, "Ai/Forest/NastyBear.cs")
    cut(30829, "Ai/Forest/Behemoth.cs")
    cut(31573, "Ai/Forest/Giant.cs")
    cut(32127, "Ai/Forest/Willie.cs")
    cut(32475, "Ai/Forest/Trent.cs")
    cut(33138, "Ai/Forest/Varan.cs")
    cut(34373, "Ai/OldTower/Gremlin.cs")
    cut(35278, "Ai/ImmortalCastle/PurpleKnight.cs")
    cut(36045, "Ai/Amphibians.cs")
    cut(36811, "Ai/SealedCave/Gremlin.cs")
    cut(37479, "Ai/Forest/Wyrm.cs")
    cut(38074, "Ai/Forest/EvilFlower.cs")
    cut(38624, "Ai/Forest/Bee.cs")
    cut(39020, "Ai/Forest/LizardMan.cs")
    cut(39581, "Ai/Forest/Goblin.cs")

    # ---- cinema, pas par personnage, menu rapide, machine a opcodes ----
    cut(40377, "Cutscene/CutsceneVerbs.cs")
    cut(41221, "Tick/PartyMemberStep.cs")
    cut(42061, "Items/QuickMenu.cs")
    cut(42587, "Items/ItemScript.cs")
    cut(42629, "Dispatch/CoverageProbes.cs")     # les sondes d'objets et de sorts
    cut(42863, "Items/ItemScript.cs")

    # ---- la naissance d'une entite, puis la magie ----
    cut(43233, "Entities/Spawning.cs")
    cut(43260, "Spells/SpellCasting.cs")
    cut(43955, "Spells/School0Wizard.cs")
    cut(53078, "Spells/School1Priest.cs")
    cut(61905, "Spells/School2Elementalist.cs")
    cut(66489, "Spells/Herbalism.cs")
    cut(66792, "Effects/StatusAuras.cs")
    cut(66949, "Spells/MonsterSpells.cs")

    # ---- ciblage, postures, physique, progression ----
    cut(67430, "Entities/Targeting.cs")
    cut(67461, "Tick/PostureAndInput.cs")
    cut(67740, "Entities/Targeting.cs")
    cut(67874, "Physics/MotionAndContact.cs")
    cut(68435, "Tick/PartyMemberStep.cs")
    cut(69150, "Combat/Progression.cs")
    cut(69730, "Physics/TriggerVolumes.cs")

    # ---- decor et scenario ----
    cut(70017, "Handlers/Common.cs")
    cut(71468, "Handlers/Chests.cs")
    cut(71728, "Handlers/Forest.cs")
}

function cut(line, file) { nc++; cutLine[nc] = line; cutFile[nc] = file }
function red(line, file) { redir[line] = file }
function cutOf(l,   c) { for (c = nc; c >= 1; c--) if (cutLine[c] <= l) return cutFile[c]; return "(hors plage)" }

{ L[NR] = $0 }

END {
    # --- 0. le garde-fou : est-on bien sur la reference ? ---
    # `endline` vaut la derniere ligne de corps de la reference. Lu sur le reliquat, NR est plus
    # petit : on sortirait un manifeste tronque, plausible, et faux.
    if (NR < endline) {
        printf "worldmaptick-manifest.awk : %d lignes lues, %d attendues au minimum.\n", NR, endline > "/dev/stderr"
        printf "  Le manifeste se genere depuis OpenForesia.Tools/worldmaptick-baseline.txt,\n" > "/dev/stderr"
        printf "  jamais depuis le fichier de travail - il est deja reduit par les lots precedents.\n" > "/dev/stderr"
        exit 1
    }

    # --- 1. les blocs de bandeau ---
    for (i = 1; i <= NR; i++) {
        if (L[i] !~ /^    \/\/ ={20,}$/) continue
        if (inBanner[i]) continue
        for (j = i; j <= NR; j++) {
            if (L[j] !~ /^[ ]*\/\//) break          # sortie de secours : le bandeau reste du commentaire
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
        if (k == 1 && t < startline)  t = startline
        mHead[k] = t
        mFile[k] = (mLine[k] in redir) ? redir[mLine[k]] : cutOf(mLine[k])
    }

    # --- 3. attribution ligne par ligne ---
    k = 0
    for (i = startline; i <= endline; i++) {
        while (k < nm && i >= mHead[k+1]) k++
        owner[i] = inBanner[i] ? cutOf(i) : (k >= 1 ? mFile[k] : cutOf(i))
    }

    # --- 4. fusion des suites contigues ---
    for (i = startline; i <= endline; i++) {
        if (owner[i] != cf) {
            if (cf != "") printf "%s\t%d\t%d\n", cf, cs, i - 1
            cf = owner[i]; cs = i
        }
    }
    if (cf != "") printf "%s\t%d\t%d\n", cf, cs, endline
}
