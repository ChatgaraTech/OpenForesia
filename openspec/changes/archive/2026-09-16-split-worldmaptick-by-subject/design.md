## Context

`WorldMapTick.cs` : 74 072 lignes, une seule `partial class Blaze`, préambule aux lignes 1..24,
accolade fermante en 74 072. Tout le reste — 74 047 lignes — est déplaçable.

Trois faits du terrain décident de l'approche, et ils sont tous favorables :

1. **`partial class` partout.** Aucune signature ne change, aucun `using` n'est à ajuster, aucune
   visibilité n'est à élargir. Le découpage est mécaniquement gratuit côté compilation.

2. **L'outillage existe et il est paramétré.** `apply-split.ps1`, `verify-split.ps1` et
   `split-manifest.awk` ont été écrits pour `split-blaze-into-subsystems` et prennent déjà
   `-Manifest`, `-Baseline`, `-Root`, `-Source`, `-PreambleEnd`. Ils lisent et écrivent en
   **ISO-8859-1** (un octet ↔ un caractère), ce qui rend la comparaison exacte à l'octet quel que
   soit l'encodage réel du contenu — la propriété qui a permis de prouver le découpage précédent.

3. **Le couplage réel est faible.** Un seul point de rendez-vous entre l'infrastructure et les
   68 000 lignes de corps : `DispatchEntityHandlerCore`, un `switch` de 426 cases `VA → méthode`.
   Il ne se découpe pas, il se déplace entier.

Deux choses ne sont pas favorables et sont traitées en décisions ci-dessous : le préambule n'est pas
contigu (D3), et quatre scripts codent le chemin en dur sans crier quand il change (D8).

## Goals / Non-Goals

**Goals:**

- Aucune ligne de code modifiée. La preuve est une comparaison **octet à octet** contre une
  référence figée avant le premier déplacement, pas une relecture.
- Une arborescence où le nom du répertoire dit le rôle et le nom du fichier dit le sujet.
- Un découpage **par étapes vérifiables** : chaque lot compile, chaque lot est prouvé, et le dépôt
  est laissé dans un état cohérent entre deux séances.
- Les trois tableaux de bord (`GestionnairesEntites.md`, `ObjetsAtteignables.md`,
  `SortsAtteignables.md`) sortent **identiques** avant et après.

**Non-Goals:**

- Aucune reconstruction, aucune correction de fidélité, aucun renommage de symbole, aucune
  suppression de `TODO(REVERSE)`. Ce qui est laid reste laid, à la ligne près.
- Aucune promotion vers `Engine/`. La règle de routage (3) d'`ARCHITECTURE.md` se déclenchera le
  jour où un second écran appellera `Spells/` ou `Combat/` ; ce n'est pas aujourd'hui.
- Aucune correction des trois bandeaux menteurs relevés dans la proposition **en tant que
  commentaires** : ils disparaissent parce que le contenu se range ailleurs, pas parce qu'on les
  réécrit en place.
- Aucun `git`. L'utilisateur seul commite.

## Decisions

### D1 — L'unité de découpe est « ce qu'on ouvre exprès »

Ni l'adresse, ni la taille, ni la table de sauts. Le critère est : *quand j'ouvre ce fichier,
qu'est-ce que je viens y faire ?*

- On vient travailler **sur le Grizzly** → `Ai/Forest/Grizzly.cs`. Une créature est une unité
  réelle : `Comportement.md` l'indexe ainsi, la sonde la relève ainsi, et sa transcription est une
  séance de travail entière.
- On vient travailler **sur un sort**, pas sur « les sorts 10 à 19 » → `Spells/School0Wizard.cs`,
  une école par fichier, les bandeaux par sort faisant la navigation interne.

Conséquence assumée : `Spells/School0Wizard.cs` fait 9 123 lignes. `ARCHITECTURE.md` dit
« la métrique est la cohérence, pas la taille », et `Screens/State06/Inventory.cs` (2 937 lignes
pour trois sous-écrans) est le précédent. Une école est cohérente ; 74 072 lignes ne l'étaient pas.

**Alternative écartée** : couper les écoles par indice de sort (`School0Wizard_00_09.cs`, …).
L'indice est une vraie coordonnée du jeu — c'est l'ordre d'apprentissage — mais personne n'ouvre
« les sorts 10 à 19 ». Réservé comme molette si un fichier d'école devient inconfortable à l'usage.

**Alternative écartée** : grouper l'IA de la forêt par bloc de la table 0x0056adc8
(`BlockA/B/C.cs`, 3 fichiers au lieu de 17). Fidèle au binaire, mais on perd le « ah oui, ce fichier
c'est le Grizzly », qui est précisément ce que le découpage cherche.

**Le nom d'un fichier de créature n'est pas une supposition : il vient de `Comportement.md`**
(2026-09-16, lot B). Deux noms posés à l'exploration étaient faux et ont été corrigés au moment de
l'extraction — `BlockAPatrol.cs` n'existait pas (c'étaient **le Goblin-sorcier** `0x0049E190`,
sous-type 9, et **le Crotale** `0x0049E920`, sous-type 18), et `FlyingBeast.cs` est **l'Abeille
tueuse** `0x004A3C30` du bloc C. Un troisième fichier cachait une bête : **la Fourmi géante**
`0x00499DE0`, sous-type 0, était rangée sous `Ladybug.cs`.

Deux pièges de nommage que la table impose et que les bandeaux d'en-tête énoncent :

- **Deux « Goblin » distincts** dans le même bloc A — sous-type 2 (`0x0049A980`) et sous-type 8
  (`0x0049DA40`), deux corps sans rapport. Le premier reste dans `SceneEntityAiCommon.cs` : c'est la
  charpente de référence à cinq postures, et l'y séparer de ses feuilles n'aurait rien éclairci.
- **Deux « Abeille tueuse »** sous deux gestionnaires différents, bloc A (`Bee.cs`) et bloc C
  (`KillerBee.cs`). Le nom d'espèce ne détermine pas le comportement ; le bloc et le sous-type le
  font.

### D2 — Onze répertoires nommés par rôle, sous `Screens/State03/`

Rien ne quitte `State03/` : la règle de routage (2) tient, un seul écran appelle tout cela. Les
7 fichiers existants restent à la racine du répertoire — ils sont le niveau « écran », les nouveaux
répertoires sont le niveau « ce que l'écran atteint ».

| Répertoire | Ce qu'il contient | Le test d'appartenance |
|---|---|---|
| `Tick/` | la boucle par image et son dessin | appelé **par image**, depuis `RunWorldMapTick` |
| `Dispatch/` | le carrefour adresse → méthode | ne fait que router |
| `Combat/` | ce qui décide qui perd des points de vie | lit ou écrit un bloc de combat / une fiche |
| `Physics/` | ce que **toute** entité subit | ne connaît pas le type de l'entité |
| `Entities/` | l'infrastructure commune aux gestionnaires | éveil, animation, ciblage |
| `Effects/` | ce qui naît, scintille et meurt | ne décide de rien |
| `Items/` | l'objet et ce qu'il déclenche | part d'une fiche d'objet |
| `Spells/` | la magie | part d'une fiche de sort |
| `Cutscene/` | les scènes et leurs verbes | la partie ne répond plus aux boutons |
| `Ai/` | l'intelligence | choisit une action pour une entité |
| `Handlers/` | décor et scénario | ni IA ni magie : coffres, radeau, portes, sons |

Noms **en anglais, PascalCase**, comme tout le dépôt (`EntityVTables.cs`, `Primitives2d.cs`,
`Inventory.cs`). Les bandeaux de commentaire restent en français.

**Alternative écartée** : un répertoire-chapeau `State03/World/`. Il ajoute un niveau sans ajouter
d'information — `State03` *est* la carte du monde.

### D3 — Le préambule n'est pas contigu : lignes 1..6 + 17..24

`apply-split.ps1` recopie `1..PreambleEnd` en tête de chaque fichier produit. Ici, les lignes 8..16
décrivent `FUN_00516fc0` en particulier (« 1 226 lignes de pseudo-C, le plus gros corps que le dépôt
ait abordé… ») : les recopier dans 56 fichiers y écrirait une contre-vérité.

Le script gagne donc un paramètre `-PreambleRanges` acceptant plusieurs plages. Valeur retenue :
**`1-6,17-24`** — les six lignes génériques, puis la ligne vide, les `using`, le `namespace`, la
déclaration de classe et l'accolade. La ligne 7 (`//`) et les lignes 8..16 restent dehors. Chaque
fichier reçoit ensuite **son propre bandeau d'en-tête, écrit à la main**, inséré entre la ligne 6 et
la ligne vide : il rouvre le bloc par un `//` et le referme en laissant la ligne vide en place.

**Les lignes 8..16 ne sont dans aucune plage du manifeste** (qui commence à 25), donc les exclure du
préambule les ferait *disparaître du dépôt* pendant les sept premiers lots. Le script gagne pour
cela un second paramètre, **`-ResidualPreambleRanges "1-24"`** : le reliquat garde son en-tête
d'origine intact tant qu'il porte `FUN_00516fc0`, et au lot H ces neuf lignes deviennent le bandeau
de `Tick/WorldMapTick.cs`. Ce besoin n'était pas vu à l'écriture de D3 ; il est apparu au premier
`-WhatIf` du lot A.

**Où va ce bandeau : au-dessus de `using`, pas dans la classe.** C'est déjà la convention du dépôt —
`WorldMapCamera.cs` porte sa description en tête de fichier, `using System;` n'arrive qu'en ligne 26.
Le bandeau se glisse donc dans le bloc de commentaires du préambule, avant la ligne vide qui précède
les `using`.

Ce choix a une conséquence heureuse, découverte en écrivant la tâche 1.8 : `verify-split.ps1`
délimite le corps d'un fichier de **la première ligne valant exactement `{`** à la dernière valant
`}`. Un bandeau posé au-dessus de `using` est hors de ces bornes. **Le script n'a donc rien à
apprendre** — la tâche 1.8 se réduit à le prouver, et D5 n'a plus de dérogation à porter.

### D4 — `apply-split.ps1` gagne `-Residual`

Le script écrit aujourd'hui le reliquat dans `Join-Path $Root "Blaze.cs"`, codé en dur. Il lui faut
un `-Residual Screens\State03\Tick\WorldMapTick.cs`. C'est une ligne, et elle rend le script
réutilisable pour le prochain fichier qui enflera.

### D5 — La recette est octet à octet, pas visuelle

Trois barrières, dans cet ordre, **à chaque lot** :

1. **`verify-split.ps1`** — reconstitue chaque plage depuis `worldmaptick-baseline.txt` et la
   compare au corps du fichier cible, ligne par ligne, en ISO-8859-1. Le corps va de la première
   ligne valant exactement `{` à la dernière valant `}` ; le bandeau d'en-tête étant posé au-dessus
   de `using` (D3), il est hors de ces bornes et **le script est utilisable sans modification**.
2. **`dotnet build OpenForesia.sln`** — 0 erreur, et le **nombre d'avertissements inchangé**. Un
   avertissement de plus est un symptôme, pas un détail.
3. **Les trois recensements** — `census-entity-handlers.sh`, `census-item-effects.sh`,
   `census-spells.sh` regénèrent leurs `.md` ; le diff doit être **vide**. Plus
   `check-descriptors.sh`, dont la sortie doit être identique.

La barrière 3 est la seule qui détecte D8, et c'est la plus importante : elle échoue en silence si
on ne la regarde pas.

### D6 — L'ordre d'initialisation des champs statiques : les sept à vérifier

Le piège documenté dans `CLAUDE.md` et payé au découpage de 2026-08-19 : dans une classe partielle,
l'ordre d'exécution des initialiseurs de champs statiques entre **fichiers** n'est pas spécifié.

Sur 122 champs `static readonly`, **103 sont des littéraux `byte[]`/`uint[]`/`int[]`** — sûrs par
construction. Restent **sept** dont l'initialiseur appelle une méthode :

| Champ | Ligne | Initialiseur |
|---|---|---|
| `s_ClassBodyMetrics_0056d440` | 1613 | `AllocBytes([…])` |
| `s_MenuLayout_0056a850` | 1671 | `AllocBytes([…])` |
| `s_IconTables_0056aaa0` | 23971 | `AllocBytes([…])` |
| `s_ReachMarkerAtlas_0056aca0` | 24723 | `AllocBytes([…])` |
| `s_ErrorMarkerAtlas_00564808` | 24996 | `AllocBytes([…])` |
| `s_SpellCounts_00564600` | 42057 | `AllocBytes([…])` |
| `s_DeveloperNotice_00551814` | 73536 | `BuildDeveloperNotice_00551814()` |

Les six premiers sont sûrs si et seulement si `AllocBytes` ne lit aucun champ statique. Le septième
est à lire ligne par ligne. **Règle** : tout champ dont l'initialiseur s'avère lire un champ
statique d'un autre fichier devient une propriété calculée `=>`, et **cette transformation est la
seule modification de code que ce changement s'autorise** — signalée explicitement, jamais glissée.

Les ~90 champs mutables restants sont des `…Storage` paresseux : ils voyagent avec leur descripteur,
sans risque.

**VERDICT DE LA TÂCHE 1.6 (2026-09-16) — les sept sont sûrs, aucune modification de code n'est
nécessaire.**

- `AllocBytes(byte[])` (`Platform/Startup.cs:321`) : `NativeMemory.Alloc` puis une boucle de copie
  depuis son paramètre. **Il ne lit aucun champ statique.** Les six champs qui l'appellent sont donc
  sûrs où qu'ils atterrissent.
- `BuildDeveloperNotice_00551814()` : un `ReadOnlySpan<byte>` **local**, `NativeMemory.AllocZeroed`,
  une boucle de copie. Aucun champ statique lu non plus. De surcroît le champ (73536) et sa méthode
  (73538) sont adjacents, donc le manifeste les garde dans le même fichier — l'ordre est garanti par
  la position textuelle, pas seulement par l'analyse.

Un balayage plus large a été fait sur **tous** les initialiseurs de champ du fichier, y compris les
multi-lignes que le premier relevé avait manqués (`s_TraceEntity`, `s_AiStub`, `s_ProbeEnabled`,
`s_MissingHandlers`, `s_SeenEmptySlots`, `s_HandlersSeen`, `s_ItemsSeen`, `s_SpellsSeen`,
`s_ItemMessageScratch_005de620`, `s_CameraResetArmed_00604b80`, `s_CameraResetMask_00604b84`). Ils
se réduisent tous à `new()`, `Environment.GetEnvironmentVariable(…)` ou
`NativeMemory.AllocZeroed(<constante>)` : **aucun initialiseur de `WorldMapTick.cs` ne lit un champ
statique de `Blaze`.** Le piège de 2026-08-19 ne se rejoue pas ici.

### D7 — Les blocs qui se recollent, et ceux qu'on laisse en l'état

Quatre sujets sont aujourd'hui éparpillés en deux endroits du fichier. Le manifeste les recolle en
un seul fichier — le format `fichier / début / fin` accepte plusieurs plages par fichier, c'est
prévu :

| Fichier cible | Plages |
|---|---|
| `Tick/PartyMemberStep.cs` | `FUN_0050bf20` + `TickPartyMemberTail` |
| `Handlers/Chests.cs` | les coffres du bloc 0x0045 + l'ouverture et les coffres de capacité |
| `Handlers/Forest.cs` | l'embarcation + le radeau, l'ascenseur, les portes, les volumes de caméra |
| `Dispatch/CoverageProbes.cs` | la sonde de gestionnaires + celle d'objets + celle de sorts |

En revanche, **l'ordre interne d'un fichier reste l'ordre du fichier d'origine**. On ne réordonne
pas : un déplacement non prouvable par le manifeste est un déplacement non prouvable.

### D8 — Quatre scripts codent le chemin en dur, et échouent à zéro

```
OpenForesia.Tools/census-entity-handlers.sh:38   TICK=…/State03/WorldMapTick.cs
OpenForesia.Tools/census-item-effects.sh:36      TICK=…/State03/WorldMapTick.cs
OpenForesia.Tools/census-spells.sh:42            TICK=…/State03/WorldMapTick.cs
OpenForesia.Tools/check-descriptors.sh:79        "$SRC/Blaze/Screens/State03/WorldMapTick.cs"
```

Ils cherchent des déclarations par `grep`. Après le découpage, le chemin existera encore (c'est le
reliquat) mais ne contiendra plus que la boucle par image : **les recensements trouveront presque
tout à zéro, sans erreur et sans message**. `GestionnairesEntites.md` tomberait de 1 430 lignes à
presque rien, et rien ne le signalerait.

La correction est d'élargir la lecture à l'arborescence : `State03/**/*.cs` au lieu du fichier
unique. Elle doit être faite **avant** le premier déplacement, et validée par une regénération à
diff vide sur le fichier encore entier — sinon on ne saura pas distinguer une régression du script
d'une régression du découpage.

### D8bis — Deux gardes-fous nés des lots eux-mêmes

**`apply-split.ps1 -KeepDone`.** Le script réécrit les fichiers `-Done` depuis la référence. C'est
sain tant que rien n'a été écrit à la main — mais dès le lot B cela **effaçait en silence les six
bandeaux du lot A**, et rien ne l'aurait signalé : le fichier redevient conforme au manifeste, donc
`verify-split.ps1` reste vert. C'est le seul endroit de ce chantier où « redériver depuis la
référence » est la mauvaise chose à faire. `-KeepDone` retire les fichiers déjà faits du reliquat
sans les réécrire.

**Le manifeste se génère depuis la référence, jamais depuis le fichier de travail.** Dès le lot A,
`WorldMapTick.cs` n'est plus que le reliquat : le regénérer dessus rend un manifeste **plausible et
faux** — 45 plages au lieu de 77, sans erreur. `worldmaptick-manifest.awk` refuse maintenant de
sortir quoi que ce soit si le fichier lu est plus court que la référence.

**Le contrôle de bandeau orphelin.** Un bandeau est orphelin quand le **premier membre qui le suit**
atterrit dans un autre fichier — à distinguer de la dérive de queue, où c'est un membre tardif qui
part (14 cas, attendus, résolus par les bandeaux d'en-tête). Le contrôle a trouvé **un** orphelin,
`L26413`, et la coupe a été déplacée de 26453 à 26413. Il est rejoué à chaque changement du
manifeste.

### D9 — Découper par lots, du moins couplé au plus couplé

Chaque lot est un point d'arrêt propre : le dépôt compile, les recensements passent, l'utilisateur
peut commiter. L'ordre remonte le couplage :

```
  Lot A  Spells/                       ~24 300 l.   le plus gros, le plus isolé — feuilles pures
  Lot B  Ai/                           ~16 400 l.   feuilles, un fichier par créature
  Lot C  Effects/                      ~ 8 260 l.   feuilles
  Lot D  Items/ + Cutscene/            ~ 5 970 l.   feuilles
  Lot E  Handlers/                     ~ 5 750 l.   feuilles
  Lot F  Combat/ Physics/ Entities/    ~ 7 300 l.   appelé par les feuilles
  Lot G  Dispatch/                     ~ 1 910 l.   le carrefour
  Lot H  Tick/                         reliquat     ce qui reste EST la boucle par image
```

Le reliquat n'est pas une poubelle : après les sept premiers lots, ce qui reste dans
`WorldMapTick.cs` est exactement `RunWorldMapTick` et son dessin, et c'est la vérification finale du
découpage — **si autre chose y reste, c'est qu'un sujet a été mal classé.**

### D10 — La carte des fichiers cibles

Frontières à ±quelques lignes : elles sont arrêtées par l'inventaire de la tâche 1 et **validées par
l'utilisateur avant le premier déplacement**. La règle de frontière est : *la coupe tombe sur la
première ligne du bloc de commentaires (`///` ou `//`) qui précède la première déclaration du
fichier suivant*, bandeau `// ====` compris.

| Fichier | Plage(s) d'origine | ~Lignes |
|---|---|---|
| `Ai/Amphibians.cs` | 36045–36810 | 766 |
| `Ai/Inn.cs` | 22627–23326 | 700 |
| `Ai/SceneEntityAiCommon.cs` | 25749–26412 | 664 |
| `Ai/Forest/Varan.cs` | 33138–34372 | 1235 |
| `Ai/Forest/Goblin.cs` | 39581–40358 | 778 |
| `Ai/Forest/Behemoth.cs` | 30829–31572 | 744 |
| `Ai/Forest/Trent.cs` | 32475–33137 | 663 |
| `Ai/Forest/NastyBear.cs` | 30169–30828 | 660 |
| `Ai/Forest/Wyrm.cs` | 37479–38073 | 595 |
| `Ai/Forest/LizardMan.cs` | 39020–39580 | 561 |
| `Ai/Forest/Grizzly.cs` | 29613–30168 | 556 |
| `Ai/Forest/Giant.cs` | 31573–32126 | 554 |
| `Ai/Forest/EvilFlower.cs` | 38074–38623 | 550 |
| `Ai/Forest/Rattlesnake.cs` | 26864–27394 | 531 |
| `Ai/Forest/FlyingFish.cs` | 28594–29117 | 524 |
| `Ai/Forest/GiantCrab.cs` | 29118–29612 | 495 |
| `Ai/Forest/Ladybug.cs` | 27710–28189 | 480 |
| `Ai/Forest/GoblinShaman.cs` | 26413–26863 | 451 |
| `Ai/Forest/KillerBee.cs` | 28190–28593 | 404 |
| `Ai/Forest/Bee.cs` | 38624–39019 | 396 |
| `Ai/Forest/Willie.cs` | 32127–32474 | 348 |
| `Ai/Forest/GiantAnt.cs` | 27395–27709 | 315 |
| `Ai/ImmortalCastle/PurpleKnight.cs` | 35278–36044 | 767 |
| `Ai/OldTower/Gremlin.cs` | 34373–35277 | 905 |
| `Ai/SealedCave/Gremlin.cs` | 36811–37478 | 668 |
| `Combat/StatusEffects.cs` | 8425–9818 | 1394 |
| `Combat/HitResolution.cs` | 24443–25748 | 1306 |
| `Combat/Progression.cs` | 69123–69689 | 567 |
| `Combat/Projectiles.cs` | 20940–21465 | 526 |
| `Combat/CombatPairing.cs` | 7574–8040 | 467 |
| `Combat/EffectTable.cs` | 8041–8232 | 192 |
| `Combat/HerbEffects.cs` | 8233–8424 | 192 |
| `Cutscene/ForestScenes.cs` | 17046–18917 | 1872 |
| `Cutscene/CutsceneVerbs.cs` | 40359–41220 | 862 |
| `Dispatch/EntityHandlerDispatch.cs` | 4247–4324 + 4448–5589 | 1220 |
| `Dispatch/CoverageProbes.cs` | 4325–4447 + 42629–42849 | 344 |
| `Dispatch/StaticEntityTables.cs` | 4014–4246 | 233 |
| `Effects/ForestParticlesA.cs` | 12296–14644 | 2349 |
| `Effects/HitAndResistMarkers.cs` | 10366–12295 | 1930 |
| `Effects/StatusMarkers.cs` | 5590–7144 | 1555 |
| `Effects/ForestParticlesB.cs` | 14645–16082 | 1438 |
| `Effects/TrapScripts.cs` | 9819–10365 | 547 |
| `Effects/MiscMarkers.cs` | 7240–7573 | 334 |
| `Effects/StatusAuras.cs` | 66792–66948 | 157 |
| `Entities/WakeState.cs` | 21466–22626 | 1161 |
| `Entities/Targeting.cs` | 67404–67454 + 67720–67848 | 180 |
| `Entities/Spawning.cs` | 43226–43246 | 21 |
| `Handlers/Forest.cs` | 16083–16689 + 71728–74071 | 2951 |
| `Handlers/Common.cs` | 70017–71439 | 1423 |
| `Handlers/Chests.cs` | 23918–24442 + 71440–71727 | 813 |
| `Handlers/Sound.cs` | 23327–23917 | 591 |
| `Items/ItemSpawns.cs` | 18918–20939 | 2022 |
| `Items/QuickMenu.cs` | 42061–42586 | 526 |
| `Items/ItemScript.cs` | 42587–42628 + 42850–43225 | 418 |
| `Physics/MotionAndContact.cs` | 67849–68428 | 580 |
| `Physics/TerrainMesh.cs` | 16690–17045 | 356 |
| `Physics/TriggerVolumes.cs` | 69690–70016 | 327 |
| `Physics/AngleMath.cs` | 7145–7239 | 95 |
| `Spells/School0Wizard.cs` | 43955–53077 | 9123 |
| `Spells/School1Priest.cs` | 53078–61904 | 8827 |
| `Spells/School2Elementalist.cs` | 61905–66488 | 4584 |
| `Spells/SpellCasting.cs` | 43247–43954 | 708 |
| `Spells/MonsterSpells.cs` | 66949–67403 | 455 |
| `Spells/Herbalism.cs` | 66489–66791 | 303 |
| `Tick/WorldSimulationStep.cs` | 2406–4013 | 1608 |
| `Tick/PartyMemberStep.cs` | 41221–42060 + 68429–69122 | 1534 |
| `Tick/WorldMapHud.cs` | 764–2251 | 1488 |
| `Tick/WorldMapTick.cs` | 25–763 | 739 |
| `Tick/PostureAndInput.cs` | 67455–67719 | 265 |
| `Tick/SpriteAtlas.cs` | 2252–2405 | 154 |
**70 fichiers, 11 répertoires.** Plus gros : 9 123 lignes. Médiane : 566. Total : 74 047 lignes.

Ce tableau est **généré** depuis `OpenForesia.Tools/worldmaptick-manifest.tsv`, qui est la source de
vérité. Les plages sont celles des **têtes de trivia**, pas des lignes de déclaration : la trivia de
tête suit son membre, et un bandeau `// ====` suit son point de coupe. C'est ce que
`worldmaptick-manifest.awk` produit et ce qu'`apply-split.ps1` consomme.

## Risks / Trade-offs

| Risque | Gravité | Parade |
|---|---|---|
| **Les quatre scripts tombent à zéro sans crier** (D8) | **Haute** — les trois tableaux de bord du dépôt deviennent faux sans symptôme | Les corriger **avant** le premier déplacement, prouver le diff vide sur le fichier entier, puis rejouer à chaque lot |
| **Ordre d'initialisation des statiques** (D6) | Haute — plantage à la première image, déjà payé une fois | Les sept champs sont listés ; audit de `AllocBytes` et de `BuildDeveloperNotice_00551814` en tâche 1 |
| Une frontière tombe au milieu d'un bloc de commentaires | Moyenne — compile encore, mais une doc se retrouve orpheline | La règle de frontière de D10, et `verify-split.ps1` qui compare corps à corps |
| `DataSymbols.md` devient faux sur ~220 symboles | Moyenne — c'est l'outil qui protège des adresses adjacentes séparées | Régénération en fin de chantier, avant de rendre la main |
| Les renvois `WorldMapTick.cs:NNNN` de `REVERSE_STATUS.md` deviennent faux | Faible — ils sont historiques, datés, et lus comme tels | Consigné, non réécrit |
| 68 fichiers d'un coup, revue impossible | Moyenne | Le découpage par lots de D9 : huit points d'arrêt, chacun compilé et prouvé |
| Un sujet mal classé n'apparaît qu'au lot H | Faible | C'est précisément le test de D9 : si le reliquat n'est pas `RunWorldMapTick` et son dessin, quelque chose s'est mal rangé |
