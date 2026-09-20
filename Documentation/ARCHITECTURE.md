# Architecture d'OpenForesia

Où vit chaque chose, et **la règle qui décide où va une fonction**. Écrit le 2026-08-19, au
découpage de `Blaze.cs` (`openspec/changes/split-blaze-into-subsystems`).

Pour l'état de la reconstruction — couverture du graphe d'appel, `TODO(REVERSE)`, point de blocage
courant — voir `REVERSE_STATUS.md`. Pour la méthode, `Prompt.md` et `Strategy.md`.

---

## Les deux moitiés du dépôt, et leurs règles opposées

C'est la distinction la plus importante du dépôt, et la seule que la disposition des fichiers doit
rendre évidente au premier coup d'œil.

| | `src/Blaze/` | `src/Host/` |
|---|---|---|
| Ce que c'est | la **transcription** de `Blaze.exe` | notre **échafaudage** pour 2026 |
| Règle | *fidelity beats compilability* | ça doit **marcher** |
| Source de vérité | `Blaze.c` | aucune — c'est nous qui décidons |
| Un `TODO(REVERSE)` y a un sens ? | oui | non |
| Refactorisable librement ? | **non** | oui |

`src/Host/` remplace la chaîne d'affichage de 1998 : `3dhelp.dll` prend l'écran en mode exclusif et
change le mode du bureau, ce qui fait clignoter l'écran sur Windows moderne. Cinq fichiers, un seul
travail.

```
   winsys3d.dll   LoadLibraryA("3dhelp") -> GetProcAddress("Create3DDevice")
        |  detourne dans la table d'imports
        v
   SyntheticDevice    rend NOTRE device ; 3dhelp ne s'initialise jamais
        v
   DeviceShim         facade de vtable, slots chauds detournes
        v
   D3D11Backend       les sommets sont DEJA projetes (portage PSX : le GTE projette cote CPU)
        v
   D3D11Window        aucun appelant ne la passe a DirectDraw -> pas de mode exclusif
   StartupSurface     regime GDI titre/attract : DIB section pour StretchDIBits
```

**La couture entre les deux moitiés est étroite et nommée** — pas à sens unique, ce qui serait
impossible : une façade s'installe depuis le chemin qu'elle détourne.

- `Host` → `Blaze` : 5 symboles (`ToAnsiZ`, `RenderSizeFromSizeSetting`, `s_WindowedStartup`,
  `GetModuleHandleA`, `VirtualAlloc`), passés `internal` et importés par `using static`.
- `Blaze` → `Host` : 4 symboles, **tous des points d'installation** (`InstallSyntheticDevice`,
  `InstallDeviceShim`, `CreateGameWindow`, `LogDisplayMode`). Ils sont **qualifiés explicitement**
  au site d'appel (`DeviceShim.LogDisplayMode(…)`) pour que la couture reste visible.

---

## Les trois couches

Le refactor `extract-real-classes-from-partial` a separe trois choses que le code ecrasait l'une
sur l'autre. C'est la lecture la plus utile de `src/Blaze/` :

```
┌─────────────────────────────────────────────────────────────────────┐
│  1. INTEROP        Interop/          static PAR NECESSITE           │
│     Win32 · Winsys3d · Wsyskern      160 P/Invoke                   │
│     [DllImport] exige `extern static`, et il n'y a aucun etat.      │
│     Une classe statique est ici la forme CORRECTE, pas un pis-aller.│
└─────────────────────────────────────────────────────────────────────┘
                                  ▲
┌─────────────────────────────────┴───────────────────────────────────┐
│  2. MEMOIRE        Memory/GameMemory.cs                             │
│     UN objet, parce que le binaire a UN segment .data.              │
│     Ce que la classe apporte : une SIGNATURE. Un sous-systeme qui   │
│     la prend en parametre declare qu'il touche a .data ; avant, il  │
│     le faisait en silence.                                          │
└─────────────────────────────────────────────────────────────────────┘
                                  ▲
┌─────────────────────────────────┴───────────────────────────────────┐
│  3. COMPORTEMENT   Engine/ · Platform/ · Screens/                   │
│     Engine/ : NEUF vraies classes d'instance depuis le 2026-08-21   │
│               (etape C terminee), chacune declarant ses dependances │
│               dans son constructeur.                                │
│     Platform/ et Screens/ : encore `partial class Blaze`. Screens/  │
│               deviendra douze classes a l'etape D ; Platform/ porte │
│               l'interop et le demarrage, il n'a pas vocation a y    │
│               passer.                                               │
└─────────────────────────────────────────────────────────────────────┘
```

### Le graphe de dependances d'`Engine/`, et pourquoi il est un DAG

Depuis le retrait interieur des points d'entree (2026-08-21), **aucune classe n'atteint une soeur
par `Subsystems.cs`** : tout passe par le constructeur, donc le graphe est lisible dans les
signatures.

```
   Video ──► Resources ──┬──► Primitives2d ──┬──► UiWindow ──┬──► UiText
                         │         │         │               ├──► Input
                         ├──► Input│         └──► UiText     └──► Audio ──► Model3d
                         └──► Audio ──► Model3d
```

Les quatre feuilles — `UiText`, `Input`, `SaveData`, `Model3d` — ne dependent d'aucune soeur et
gardent `(GameMemory mem)`.

**`Audio → Model3d`, ajoutee le 2026-08-22**, en transcrivant `FUN_0053db20` : un son place dans le
monde se **projette avec la camera courante** avant d'etre panoramique, donc la couche son appelle
`Model3d.ProjectPoint`. `Audio` cesse d'etre une feuille et prend `(GameMemory mem, Model3d mdl)`.
Le graphe reste un DAG : `Model3d` ne depend d'aucune soeur. C'est la premiere arete que la
**transcription** ajoute, et non un choix de conception — elle est dans le binaire.

**Que ce soit un DAG n'etait pas acquis** : la premiere mesure comptait une arete
`Input → Video` qui fermait un cycle `Input → Video → Resources → Input`, et un cycle aurait
interdit l'injection. Cette arete etait une mention **en commentaire**. Verifier les lignes d'une
arete avant d'en tirer une conclusion d'architecture.

**Une seule exception, permanente** : les sept `Vid.` de `Engine/Video.cs` sont des
**auto-references** — deux lambdas `static` et `MoviePlayerWindowProc`, que `[UnmanagedCallersOnly]`
force a rester statique. Une methode statique ne peut rejoindre son instance QUE par un point
d'entree ; elles ne relevent donc pas du retrait.

### Aucune interface, et c'est mesure

Le critere est : **une interface la ou un double existe deja, nulle part ailleurs.** Applique
honnetement, il n'en justifie aucune aujourd'hui.

`IWinsys3d` semblait acquis — `SyntheticDevice` paraissait en etre une seconde implementation.
Verification faite : **c'est faux.** `SyntheticDevice` detourne `GetProcAddress` dans la table
d'imports de winsys3d pour remplacer `3dhelp.dll::Create3DDevice`. Il substitue le **device 3D de
3dhelp**, jamais winsys3d, qui reste toujours la vraie DLL — 173 `Ordinal_NNN(...)` directs.

La couture substituable est **entre winsys3d et 3dhelp**, et elle est deja traitee par
`SyntheticDevice` et `DeviceShim`. Creer `IWinsys3d` aurait ete l'indirection sans second
implementeur que ce critere interdit : **l'appliquer correctement consistait a ne pas creer
l'interface.**

## L'arborescence du depot

Reorganisee le **2026-08-21**. Avant, les six decompilations Ghidra, les cinq DLL et onze fichiers
`.md` etaient poses en vrac a la racine. **La racine ne porte plus que deux fichiers** :
`CLAUDE.md` et `OpenForesia.sln`.

```
Legacy/          tout ce qui vient de 1998 — rien d'ecrit par nous, tout en LECTURE SEULE
  Binaries/      Blaze.exe, Blaze.cfg, et les cinq DLL d'aide
  Decompiled/    Blaze.c et les cinq .c de DLL — la sortie Ghidra
  Raw/           releves bruts par fonction (desassemblage, exports)

Documentation/   tout ce que le projet a appris
  ARCHITECTURE.md
  DLL/           un fichier par DLL d'aide
  Etat/          TODO.md, REVERSE_STATUS.md, HISTORY.md
  Methode/       Prompt.md, Strategy.md
  Reference/     DataSymbols.md, legacy-bug.md, OpenForesia.cfg.md
  Tests/         test-protocol.md, test-todo-later.md, analyses ponctuelles

Analyse/         observations du jeu original (comportement, sessions x32dbg)
OpenForesia/     la reconstruction et l'echafaudage d'hote — detaille ci-dessous
OpenForesia.Tools/ · OpenForesia.Tests/ · openspec/
```

**`Legacy/Decompiled` n'est pas un nom neuf** : c'etait deja le dossier virtuel du `.sln`, ou
`Blaze.c` etait range « tout en vivant physiquement a la racine » (voir
`openspec/changes/recover-blaze-data-section`). Le disque coincide enfin avec la solution.

## L'arborescence de `src/`

```
src/
├─ Host/  ····································· 5 fichiers   3 477 l.
│   D3D11Backend · SyntheticDevice · DeviceShim · D3D11Window · StartupSurface
│
└─ Blaze/  ···································· 31 fichiers  25 006 l.
    ├─ Blaze.cs                    17 l.   l'en-tete, les using, la declaration partielle
    ├─ Subsystems.cs              234 l.   LE COMPOSITION ROOT. `Mem` immediat, les NEUF
    │                                      sous-systemes PARESSEUX (voir la note du fichier).
    │
    ├─ Interop/  ·······················    539 l.   la surface P/Invoke, hors de la classe
    │   ├─ Winsys3d.cs      309   85 declarations, commentaires d'ordinal conserves
    │   ├─ Win32.cs         165   66 declarations, huit DLL systeme
    │   └─ Wsyskern.cs       65    9 declarations
    │
    ├─ Memory/  ························    814 l.   le modele de .data
    │   └─ GameMemory.cs    814   ~115 membres : 8 arenes, tables recouvrees, globales
    │
    ├─ Platform/  ······················  1 758 l.   ce qui n'est pas le jeu
    │   ├─ Startup.cs       626   entry + RunApplication + RunMainLoop + extinction
    │   ├─ Winsys3d.cs      670   file clavier + arene de liste d'affichage + banc GTE
    │   ├─ Crt.cs           390   runtime MSVC 6
    │   └─ Win32.cs          72   structures d'interop et constantes
    │
    ├─ Engine/  ························  7 903 l.   appele par plusieurs ecrans
    │   Les NEUF sont des classes d'instance depuis le 2026-08-21 (etape C terminee).
    │   « pure » = aucun membre statique ; « mixte » = quelques-uns, chacun justifie sur place.
    │   ├─ UiText.cs      1 344   0x00544                          VRAIE CLASSE (pure)   -> Ui
    │   ├─ Audio.cs       1 315   0x00513 · 0x0053d               VRAIE CLASSE (mixte)  -> Aud
    │   ├─ Video.cs       1 143   0x00512 · 0x00542               VRAIE CLASSE (mixte)  -> Vid
    │   ├─ Resources.cs   1 133   0x00529 · 0x0052a               VRAIE CLASSE (pure)   -> Res
    │   ├─ Input.cs       1 044   0x00523 · 0x00504               VRAIE CLASSE (mixte)  -> Inp
    │   ├─ SaveData.cs      623   E/S carte memoire seule          VRAIE CLASSE (mixte)  -> Saves
    │   ├─ Model3d.cs       615   0x0051d                          VRAIE CLASSE (pure)   -> Mdl
    │   ├─ UiWindow.cs      402   0x00545                          VRAIE CLASSE (pure)   -> UiWin
    │   └─ Primitives2d.cs  284   DrawFlatPrimitive, DrawSprite*   VRAIE CLASSE (pure)   -> Prims
    │
    └─ Screens/  ······················· 13 741 l.   un etat de jeu par repertoire
        ├─ GameStates.cs    154   le repartiteur (FUN_00537ca0)
        ├─ DebugParty.cs    753   mode de mise au point (pas un etat numerote)
        ├─ State00/  MainMenu.cs 526 · Options.cs 2 517
        ├─ State01/  PartySetup.cs 544
        ├─ State02/  Destination.cs 451
        ├─ State03/  WorldMap.cs 1 173  (+ 11 sous-repertoires, voir ci-dessous)
        ├─ State04/  SaveSlots.cs 2 109 · CharacterCreation.cs 1 539 · CharacterSelect.cs 581
        ├─ State06/  Inventory.cs 2 937 · ItemMenu.cs 493
        └─ State10/  RebootScreen.cs 120   (atteint seulement par la ligne REBOOT du menu de mise au point)
```

Tout `src/Blaze/` est **une seule classe partielle `Blaze`**. Le fichier est une unité de
navigation, pas une unité d'encapsulation — voir « pourquoi pas de vraies classes » plus bas.

**Une seule exception dans tout `src/Blaze/` : `Screens/State03/AiBench.cs`.** Ce n'est pas une
transcription — c'est la **salle d'essai**, un banc qui fait naître un ennemi par comportement
installé pour les exercer sans jouer jusqu'à eux. Il vit là et non dans `src/Host/` parce qu'il
appelle les fonctions du jeu (`SpawnEntityFormation`, `ResolveEntityHandler`) et doit donc faire
partie de la classe partielle. Son en-tête le dit en majuscules ; voir
`Documentation/Tests/test-protocol.md`.

---

## `State03/` — le seul écran qui a des sous-répertoires

**Découpé le 2026-09-16** (`openspec/changes/split-worldmaptick-by-subject`). `WorldMapTick.cs`
avait atteint **74 072 lignes** : la boucle par image (1 %), son dessin et son pas de simulation
(5 %), et **tout ce que son répartiteur d'entité pouvait atteindre** (92 %). Il n'avait pas dérivé —
c'est la branche (4) de la règle de routage qui l'avait rempli, fonction après fonction, pendant un
an. Il est aujourd'hui réparti en **70 fichiers**, onze répertoires, plus gros 9 137 lignes,
médiane 566.

Les **sept fichiers d'écran restent à la racine** de `State03/` — ils sont le niveau « écran » ;
les répertoires sont le niveau « ce que l'écran atteint ».

| Répertoire | Ce qu'il contient | Le test d'appartenance |
|---|---|---|
| `Tick/` | la boucle par image, son dessin, son pas de simulation | appelé **à chaque image**, depuis `RunWorldMapTick` |
| `Dispatch/` | le carrefour adresse → méthode, les tables statiques, les 3 sondes | ne fait que router |
| `Combat/` | ce qui décide qui perd des points de vie | lit ou écrit un bloc de combat / une fiche |
| `Physics/` | ce que **toute** entité subit | **ne consulte jamais le sous-type** de l'entité |
| `Entities/` | l'infrastructure commune aux gestionnaires | éveil, ciblage, naissance |
| `Effects/` | ce qui naît, scintille et meurt | ne décide de rien |
| `Items/` | l'objet et ce qu'il déclenche | part d'une fiche d'objet |
| `Spells/` | la magie, une école par fichier | part d'une fiche de sort |
| `Cutscene/` | les scènes et leurs 32 verbes | la partie ne répond plus aux boutons |
| `Ai/` | l'intelligence, **une créature par fichier** | choisit une action pour une entité |
| `Handlers/` | décor et scénario | ni IA ni magie : coffres, radeau, portes, sons |

**Une zone peut avoir deux fichiers dans `Handlers/`, et la coupure a son propre test.** La mine
en a deux depuis le 2026-09-16 : `Mine.cs` (2 079 l.) et `MineMecanismes.cs` (1 694 l.) ; le
labyrinthe des morts depuis le 2026-09-18 : `Labyrinth.cs` (2 212 l.) et `LabyrinthMecanismes.cs`
(950 l.) ; le château de l'immortel le même jour, `ImmortalCastle.cs` et
`ImmortalCastleMecanismes.cs`, celui-ci ouvert à **quatre** corps. Ce n'est pas une coupure de
taille — le second a toujours été ouvert bien avant que le premier ne grossisse —, c'est une
coupure de rôle, et elle se lit **dans le corps** :

| fichier | ce qui y va | test d'appartenance |
|---|---|---|
| `<Zone>.cs` | le décor : ce qui sonne, bouge, brille, tombe — **et ce qui frappe** | ne consulte jamais le groupe |
| `<Zone>Mecanismes.cs` | ce qui prend le **groupe** ou la **partie** en charge | compte les membres, lit la table des emplacements, lance un script, coupe les manettes, change de carte |

Le point qui tranche les cas douteux : **abîmer n'est pas décider**. Le pilon et les deux masses
de la mine enlèvent des points de vie au groupe et restent pourtant du côté décor ; la trémie
ressemble à un bloc poussé et se range du côté mécanismes, parce qu'elle change de carte. Les deux
fichiers sont la même classe partielle, et les aides partagées (`ZoneSound`,
`AddEntityVelocity_00454580`, `EmitJetPair`, `ScrapeSoundTick`) restent dans `<Zone>.cs` —
**rien n'est dupliqué**.

**Une zone neuve s'ouvre avec trois fichiers, pas un.** Le château de l'immortel (zone 6) les a
reçus le 2026-09-18, au palier 1 : `Effects/ImmortalCastleParticles.cs`,
`Handlers/ImmortalCastle.cs` et `Cutscene/ImmortalCastleScenes.cs`. C'est le découpage qu'ont déjà
la mine, le Labyrinthe et la vieille tour, et il se décide **avant** d'écrire — pas quand un
fichier grossit. **Son `<Zone>Mecanismes.cs` est venu au palier 2, le même jour** : quatre corps
appellent `RunEntityScript` et aucun autre du palier ne le fait — le critère se lit dans le corps,
pas dans la table d'origine, et deux de ces quatre viennent d'une table d'effets. La coupure est
de rôle ; un rôle absent n'ouvre pas de fichier, un rôle présent l'ouvre tout de suite, fût-ce à
quatre corps.

**Les ruines souterraines (zone 7) ont reçu les quatre d'un coup, le 2026-09-19**, et avec eux le
répertoire `Ai/UndergroundRuins/` : `Effects/UndergroundRuinsParticles.cs` (six corps),
`Handlers/UndergroundRuins.cs` (**un** corps — le reste de son décor est commun),
`Handlers/UndergroundRuinsMecanismes.cs` (deux : un réveil qui lance des scripts, et la recopie du
groupe dans les modèles « Sosie »), `Cutscene/UndergroundRuinsScenes.cs` (une scène) et
`Ai/UndergroundRuins/Doppelganger.cs`. Fichiers courts, mais chacun à sa place : la zone a été
fermée en un lot, et un fichier de décor à un corps vaut mieux qu'un corps rangé chez un voisin.

**Les ruines dans le lac (zone 9), le même jour** : `Effects/LakeRuinsParticles.cs`,
`Handlers/LakeRuins.cs`, `Cutscene/LakeRuinsScenes.cs` et `Ai/LakeRuins/Kraaken.cs` — **sans**
fichier de mécanismes : aucun corps de la zone ne consulte le groupe pour décider (le pilier qui
tombe frappe le groupe, mais abîmer n'est pas décider). Le Kraaken est **une créature en trois
routines** (corps, bras, pied) : un seul fichier, la règle « une créature par fichier » compte les
créatures, pas les routines.

**La vallée d'argent (zone 8), le même jour** : les cinq fichiers de la zone 7
(`Effects/SilverValleyParticles.cs`, `Handlers/SilverValley.cs`, `Handlers/SilverValleyMecanismes.cs`
— la tempête, le radeau et la luge comptent le groupe —, `Cutscene/SilverValleyScenes.cs`) et
**deux** fichiers d'IA dans `Ai/SilverValley/`, `Griffin.cs` et `Fougre.cs` : deux créatures, deux
fichiers, même si Fougre emprunte les aides que `Griffin.cs` porte.

**La grotte scellée (zone 10) s'ouvre au lot, pas d'un coup** (décision du 2026-09-19) : au lot 1,
seulement `Effects/SealedCaveParticles.cs` et `Handlers/SealedCave.cs`, à côté de
`Ai/SealedCave/Gremlin.cs` qui existait déjà. Les scènes et les mécanismes s'ouvriront avec leur
premier corps — `Handlers/SealedCaveMecanismes.cs` s'est ouvert au lot 2 avec la dalle de pression,
et **`Cutscene/SealedCaveScenes.cs` au lot 8** (2026-09-20) avec les deux morts de membre des
sous-zones 4 et 9, qui gèlent la partie. La zone a donc ses quatre fichiers, et c'est la règle
annoncée qui les a ouverts, un rôle à la fois.

**Le fichier d'effets a dépassé le seuil annoncé** : à 3 749 lignes après le lot 8, `Effects/SealedCaveParticles.cs`
est **à couper par bloc de sous-zones** (0 / 2-3 / 5-8-10, 4-9, 6-7, 11-12, 13-14-15). La coupe se
propose, elle ne se fait pas d'office — mais elle n'est plus hypothétique. `Combat/Projectiles.cs`
(3 365 lignes) est à surveiller pour la même raison, avec la nuance qu'il n'est pas propre à une
zone.

Au lot 3 (les intelligences), **une bête par fichier** dans `Ai/SealedCave/` : `ZombieDragon.cs`,
`Cerberus.cs`, `Gorgon.cs`, `Eifridt.cs`, `Lordemon.cs`, `BlackAngel.cs`, `Beelzebub.cs`. Lordémon,
l'Ange noir et Belzébuth empruntent les petites aides d'armement (`EifridtTurn`,
`EifridtStartGesture`…) que porte `Eifridt.cs`. Le corps de 39 instructions `0x005017C0`, partagé par
quatre espèces des salles 11 à 15, est allé dans `Ai/SceneEntityAiCommon.cs`, avec les feuilles
communes.

Au lot 4, un corps de la grotte a quitté les fichiers de zone par le test d'appartenance : **la
flamme qui recule** (`0x0043C010`) blesse le groupe à chaque image, elle est donc dans
`Combat/Projectiles.cs`, comme la braise `0x00416C40` de la vieille tour. Et les dalles qui
**comptent le groupe** (`0x0046AC50`, `0x004699F0`, `0x00470A00`, `0x0046B440`) sont dans
`Handlers/SealedCaveMecanismes.cs`, selon la règle du lot 2.


**Où va un gestionnaire nouvellement transcrit.** La règle de routage ci-dessous s'applique
d'abord ; c'est seulement quand elle a dit « `Screens/State03/` » (branche 2) ou « chez son
appelant » (branche 4) que ce tableau tranche, **par le test d'appartenance, pas par le sujet
apparent**. Une routine d'ennemi va dans `Ai/<Zone>/<Bête>.cs`, nommée d'après
`Documentation/Ennemies/Comportement.md` — **jamais d'après une supposition** : trois noms inventés
à la conception du découpage se sont révélés faux, et deux d'entre eux cachaient une créature
entière.

Deux pièges de nommage que la table des comportements impose : il y a **deux « Goblin »** dans le
bloc A de la forêt (sous-types 2 et 8, deux corps sans rapport) et **deux « Abeille tueuse »**
(blocs A et C). *Le nom d'espèce ne détermine pas le comportement ; le bloc et le sous-type le
font.*

### Tenir la dynamique — la règle qui évite le prochain fichier de 74 000 lignes

**`WorldMapTick.cs` n'a pas enflé par négligence : personne n'a jamais décidé qu'il enflerait.** La
branche (4) de la règle de routage l'a rempli fonction après fonction pendant un an, chaque ajout
étant individuellement justifié. C'est le cumul qui ne l'était pas. Le découpage a coûté huit lots ;
le rangement au fil de l'eau ne coûte rien.

Donc, pour tout ce qui sera transcrit dans `State03/` désormais :

1. **Ranger tout de suite dans le bon répertoire**, par le test d'appartenance du tableau ci-dessus.
   La branche (4) — « ça reste chez l'appelant » — ne vaut plus comme réponse par défaut ici : les
   onze répertoires couvrent le terrain, et un corps dont le rôle est assez établi pour être
   transcrit l'est assez pour être rangé.
2. **Ne pas entasser « en attendant ».** Si un fichier existant ne convient pas vraiment, c'est le
   signal qu'il en faut un neuf — pas celui qu'il faut forcer.
3. **Un nouveau fichier se propose, il ne se crée pas tout seul.** Soumettre le nom envisagé et ce
   qu'il contiendrait ; c'est l'utilisateur qui tranche. Un répertoire neuf, a fortiori.
4. **Surveiller ce qui grossit.** `Spells/School0Wizard.cs` (9 137 l.) et `Handlers/Forest.cs`
   (2 969 l.) sont les deux à regarder en premier. La molette est prête pour le premier : couper par
   **indice de sort**, qui est une vraie coordonnée du jeu — c'est l'ordre d'apprentissage.
   `Handlers/Mine.cs` a été coupé le 2026-09-16 à 3 734 lignes, par le test décor / mécanismes
   ci-dessus ; `Forest.cs` s'y prêterait de la même façon le jour où il le faudra.

L'objectif est énoncé simplement : **plus jamais un fichier de 70 000 lignes.**

---

## La règle de routage : où va une fonction

C'est ce qui rend l'architecture auto-entretenue. Quand le rôle d'une `FUN_00XXXXXX` est établi :

```
   (1) Son VA tombe dans une plage MOTEUR connue ?   --oui-->  Engine/<ce fichier>.cs
   (2) Un SEUL ecran l'appelle ?                     --oui-->  Screens/StateNN/<fichier>.cs
   (3) PLUSIEURS ecrans l'appellent ?                --oui-->  promotion en Engine/,
                                                                et on lui trouve un nom
   (4) sinon                                         ------->  RESTE CHEZ SON APPELANT
                                                                jusqu'a ce qu'on sache
```

**La règle est fondée sur la mesure, pas sur le goût.** Les définitions du moteur sont concentrées
à **90–100 %** dans une plage d'adresses — l'éditeur de liens de 1998 groupait les unités de
compilation d'un sous-système. Celles des écrans ne le sont qu'à **38–55 %**, parce qu'un écran
appelle des aides prises partout. D'où deux critères différents pour deux populations différentes.

**Le passage de (2) à (3) est le seul événement qui crée un nouveau fichier moteur.**
L'architecture grandit sur preuve. `Engine/Primitives2d.cs` est né de cette règle.

La branche (4) couvre même les corps seulement adressés (procédures de fenêtre, slots de vtable) :
leur « appelant » est le code qui installe leur adresse. Aucun cas orphelin, donc **aucun fichier
de rebut**.

### Deux règles de forme

- **Pas de taille maximale de fichier.** `Inventory.cs` fait 2 937 lignes pour trois sous-écrans
  d'un même menu : c'est cohérent. La métrique est la cohérence, pas la taille.
- **On ne déplace pas un chantier en cours.** Un chantier terminé se range ; si rien ne colle, il
  reste chez son appelant.

---

## La règle d'état : ce qui protège le dépôt

> **Une globale ne se déplace JAMAIS pour rendre un fichier autonome.**
> Elle se déplace uniquement quand la carte d'adresses le dit. Les arènes se déplacent **en bloc**.

Le code se range par responsabilité ; **l'état se range par adresse**. Les deux axes sont
orthogonaux et ne doivent jamais être alignés de force.

Pourquoi : le **défaut d'aliasing** a été corrigé **seize fois** dans ce dépôt. Il naît toujours du
même geste — séparer ce que le binaire garde ensemble. Deux symboles voisins d'adresse déclarés
séparément ne partagent plus leur mémoire ; une écriture faite par un bout n'est jamais lue par
l'autre. Aucune erreur, aucun plantage : une valeur qui reste à zéro.

**Mesuré au découpage** : 398 déclarateurs sur 375 adresses, dont 187 ont un voisin à 4 octets ou
moins, et **107 paires adjacentes atterrissent dans deux fichiers différents**. Tant que tout
vivait dans `Blaze.cs`, cette adjacence se voyait en faisant défiler le fichier. Elle ne se voit
plus.

→ **La carte des symboles reconstruits, dans `Documentation/Reference/DataSymbols.md`, est ce qui la rend
encore consultable.** Y chercher une adresse avant de déclarer un `DAT_` isolé.

### La règle d'étendue d'arène

Huit blocs sont adressés par décalage depuis une allocation unique. Un symbole qui tombe dans l'une
de ces étendues :

- s'il **passe par l'accesseur** de l'arène (`=> ref *Arena(0x…)`), il **reste avec son code** —
  il ne peut pas aliaser ;
- s'il a sa **propre allocation**, il **part dans le fichier de l'arène** et devient un décalage.

La règle protège **le stockage, pas les vues**. Mesuré : sur 142 symboles logés dans une arène,
61 sont des vues sans risque et 11 ont leur propre allocation — dont 7 sont les arènes elles-mêmes.


### La règle de tri d'une globale empruntée

Dégagée en convertissant `Engine/` en vraies classes : quand une classe lit un symbole déclaré dans
un autre fichier, la question « où doit-il vivre ? » se tranche sur ce que le symbole **est**, pas
sur qui le lit.

| ce que porte le symbole | où il va | pourquoi |
|---|---|---|
| **suffixe VA** — `s_SizeSetting_005de160` | `Memory/GameMemory.cs` | c'est du `.data`, et `GameMemory` en est le modèle. L'atteindre par `using static Blaze` masquerait la dépendance que la conversion cherche justement à rendre visible. |
| **pas de suffixe VA** — `s_KeyBindings` | il reste chez son propriétaire, exposé `internal` | c'est notre échafaudage, pas de la mémoire du jeu. Il deviendra un paramètre de constructeur quand son propriétaire deviendra une classe. |
| **champ d'un blob non modélisé** — `s_SmallTextureFlag` (`AU_00564a05._51_4_`) | **il ne bouge pas** | le sortir seul de son blob est exactement le geste qui fabrique le défaut d'aliasing. |

Deux corollaires, appris à leurs dépens :

- **Un symbole qui en aliase un autre déménage avec lui.** `s_Unknown_005d9de0` est le même global
  que `s_GammaIni` ; déplacer le premier seul aurait rompu la paire — et le symptôme (palettes
  d'options noires) n'apparaît qu'à l'écran, jamais au compteur de primitives.
- **Un bloc déménage entier.** `s_GammaIni` est parti avec ses deux voisines de déclaration,
  `s_SoundVolumeIni` et `s_CdVolumeIni`, les trois clés d'ini que le binaire garde ensemble.

Ce tri est distinct de l'étape D : rapatrier une globale `.data` depuis un fichier d'écran relève de
l'achèvement de `GameMemory`, pas de la conversion des écrans en classes.
---

## Pourquoi des fichiers et pas de vraies classes

Une vraie classe promet de l'encapsulation. Le binaire n'en a aucune : **44 % des globales
traversent les frontières** que le découpage pose. Créer `AudioManager` obligerait soit à rendre
260 champs `internal` — une classe qui ne cache rien, donc qui ment — soit à déplacer de l'état
pour fermer la classe, ce qui est exactement le défaut d'aliasing.

Une vraie classe ne naît que si sa **clôture est mesurée**. `DeviceShim` et `SyntheticDevice` sont
les deux seuls fichiers qui passent ce test aujourd'hui (93 % et 95 % de symboles auto-déclarés) —
d'où leur statut de vraies classes dans `src/Host/`.

**Nommage** : noms neutres et descriptifs, tout `static`. Jamais `Manager`, `Service`,
`Controller`, `Provider` — ces noms promettent un objet avec un cycle de vie, que la transcription
n'a pas, et invitent à « améliorer » vers un design que la source de vérité ne contient pas.

### Un piège que le découpage a révélé

Le découpage en fichiers **n'est pas sémantiquement neutre**. Les initialiseurs de champs statiques
d'une classe partielle s'exécutent dans l'ordre où le compilateur reçoit les **fichiers**, ordre qui
n'est pas spécifié. Un champ `static readonly` dont l'initialiseur lit un membre statique **d'un
autre fichier** peut donc s'initialiser trop tôt.

C'est arrivé : `s_SlotArea_005e9180 = Arena(0x005e9180)` a rendu `null + décalage` une fois
`Arena()` parti dans `Memory/`, et le jeu a levé une `NullReferenceException` dès la première image.

> **Règle** : un champ `static readonly` dont l'initialiseur lit un membre statique qui vit dans un
> **autre** fichier doit devenir une **propriété calculée** `=>`. Les dépendances intra-fichier sont
> sûres : l'ordre textuel est garanti à l'intérieur d'un fichier.

**Le piège ne s'est pas rejoué au découpage de `WorldMapTick.cs` (2026-09-16)**, et la raison mérite
d'être connue : **un appel de méthode n'est pas une lecture de champ**. Les sept champs suspects
appelaient tous `AllocBytes(…)` ou `BuildDeveloperNotice_00551814()`, et ces méthodes ne lisent
aucun champ statique — une méthode est disponible dès le chargement du type, où qu'elle vive. Le
balayage a couvert **tous** les initialiseurs du fichier, multi-lignes compris ; ils se réduisaient
à `new()`, `Environment.GetEnvironmentVariable(…)` ou `NativeMemory.AllocZeroed(<constante>)`.
Zéro propriété calculée à créer.

Ce qu'il faut chercher, donc, n'est pas « l'initialiseur appelle-t-il quelque chose », mais
**« l'initialiseur, ou ce qu'il appelle, lit-il un champ statique ? »**

---

## L'outillage du découpage

Dans `OpenForesia.Tools/`, réutilisable si un redécoupage s'impose :

| Fichier | Rôle |
|---|---|
| `split-manifest.awk` | construit le manifeste : attribution ligne par ligne, un bandeau va au fichier de son point de coupe |
| `split-manifest.tsv` | 56 plages, `fichier <TAB> début <TAB> fin`, en coordonnées de la référence |
| `blaze-baseline.txt` | la référence figée de `Blaze.cs` avant découpage, 23 680 lignes |
| `apply-split.ps1` | écrit les fichiers **en redérivant depuis la référence** — jamais d'édition en place, donc aucune dérive de numérotation |
| `verify-split.ps1` | comparaison octet à octet contre la référence, en ISO-8859-1 |
| `worldmaptick-inventory.awk` | l'inventaire des membres : tête de trivia, déclaration, fin, VA, bandeau courant |
| `worldmaptick-manifest.awk` | le jumeau de `split-manifest.awk` pour `WorldMapTick.cs` — 77 plages, 70 fichiers |
| `worldmaptick-manifest.tsv` | le manifeste : 77 plages, 70 fichiers, 74 047 lignes |
| `worldmaptick-baseline.txt` | la référence figée (3 289 798 octets, 74 072 lignes) — **ignorée par git**, voir ci-dessous |

**La référence de `WorldMapTick.cs` n'est pas dans le dépôt.** 3,1 Mo qui ne servent qu'à rejouer ou
vérifier le découpage, et qui se redérivent de l'historique — le fichier d'origine est dans le
parent du commit qui porte le découpage :

```bash
git show <sha-avant-decoupage>:OpenForesia/src/Blaze/Screens/State03/WorldMapTick.cs \
    > OpenForesia.Tools/worldmaptick-baseline.txt
```

Ce dépôt a `core.autocrlf=true` et **pas de `.gitattributes`** : `git show` rend le blob **tel qu'il
est stocké, en LF**, là où le fichier de travail était en CRLF. Sans conséquence pour les deux
scripts — ils découpent sur `\r\n|\n` et comparent **ligne par ligne** — mais une comparaison
d'octets brute sur le fichier entier échouerait, et c'est bon à savoir avant de conclure à une
régression.

`blaze-baseline.txt` (découpage de 2026-08-19, 1,2 Mo) reste suivi : on ne désindexe pas un fichier
déjà committé sans raison. L'asymétrie est assumée.

Tout lit et écrit en **ISO-8859-1** (un octet ↔ un caractère) via `[IO.File]::ReadAllBytes` /
`WriteAllBytes`. `Get-Content` / `Set-Content` sont proscrits sur ces fichiers : ils cassent sur la
région à encodage double et ont déjà aggravé le problème une fois.

### Ce que le découpage de 2026-09-16 a ajouté aux deux scripts

Les deux `.ps1` sont désormais entièrement paramétrés et servent les deux chantiers. Trois
paramètres sont nés du second, et chacun répare un défaut qui ne criait pas :

- **`-PreambleRanges`** — le préambule commun n'est pas forcément contigu. Celui de
  `WorldMapTick.cs` est `1-6,17-24` : ses lignes 8..16 décrivent `FUN_00516fc0` en particulier et
  n'ont rien à faire en tête des 69 autres fichiers.
- **`-ResidualPreambleRanges`** — le reliquat garde son propre en-tête. Sans lui, les lignes 8..16,
  qui ne sont dans **aucune plage du manifeste**, disparaissaient du dépôt pendant sept lots.
- **`-KeepDone`** — ne pas réécrire les fichiers déjà extraits. Sans lui, la redérivation **efface
  en silence** les bandeaux d'en-tête écrits à la main : le fichier redevient conforme au manifeste,
  donc le vérificateur reste vert et rien ne signale la perte. C'est le seul endroit d'un découpage
  où « redériver depuis la référence » est la mauvaise chose à faire.

Et deux règles de méthode, payées sur le terrain :

- **Le manifeste se génère depuis la référence figée, jamais depuis le fichier de travail.** Dès le
  premier lot, celui-ci n'est plus que le reliquat ; le regénérer dessus rend un manifeste
  **plausible et faux** (45 plages au lieu de 77, sans erreur). `worldmaptick-manifest.awk` porte
  maintenant un garde-fou qui refuse de sortir si le fichier lu est plus court que la référence.
- **Le bandeau d'en-tête d'un fichier va AU-DESSUS de `using`**, comme le fait déjà
  `WorldMapCamera.cs`. Le vérificateur délimite un corps de la première ligne valant exactement `{`
  à la dernière valant `}` : un bandeau posé là est hors de ces bornes, et le script n'a donc rien à
  apprendre.

### Le contrôle de bandeau orphelin

Un bandeau `// ====` est **orphelin** quand le *premier membre qui le suit* atterrit dans un autre
fichier — à distinguer de la **dérive de queue**, où c'est un membre tardif qui part, ce qui est
normal et se résout par un bandeau d'en-tête neuf sur la tranche suivante.

Sur les 130 bandeaux de `WorldMapTick.cs`, 14 dérivaient en queue (dont **cinq
bandeaux-cimetière** couvrant jusqu'à six fichiers cibles sous un seul titre) et **un seul** était
orphelin. Le contrôle croise le manifeste et l'inventaire ; il se rejoue à chaque modification du
manifeste et doit rendre zéro.
