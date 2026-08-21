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
│  3. COMPORTEMENT   Platform/ · Engine/ · Screens/                   │
│     Encore `partial class Blaze` aujourd'hui. C'est ici que les     │
│     vraies classes d'instance naitront (etapes C puis D).           │
└─────────────────────────────────────────────────────────────────────┘
```

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

## L'arborescence

```
src/
├─ Host/  ····································· 5 fichiers   3 477 l.
│   D3D11Backend · SyntheticDevice · DeviceShim · D3D11Window · StartupSurface
│
└─ Blaze/  ···································· 31 fichiers  24 912 l.
    ├─ Blaze.cs                    17 l.   l'en-tete, les using, la declaration partielle
    ├─ Subsystems.cs              174 l.   LE COMPOSITION ROOT. `Mem` immediat, les huit
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
    ├─ Engine/  ························  7 869 l.   appele par plusieurs ecrans
    │   ├─ Audio.cs       1 315   0x00513 · 0x0053d               VRAIE CLASSE (mixte)
    │   ├─ UiText.cs      1 344   0x00544                          VRAIE CLASSE
    │   ├─ Resources.cs   1 133   0x00529 · 0x0052a               VRAIE CLASSE (pure)
    │   ├─ Video.cs       1 143   0x00512 · 0x00542               VRAIE CLASSE (mixte)
    │   ├─ Input.cs       1 044   0x00523 · 0x00504               VRAIE CLASSE (mixte)
    │   ├─ SaveData.cs      623   E/S carte memoire seule          VRAIE CLASSE (mixte)
    │   ├─ Model3d.cs       615   0x0051d                          VRAIE CLASSE (pure)
    │   ├─ UiWindow.cs      368   0x00545
    │   └─ Primitives2d.cs  284   DrawFlatPrimitive, DrawSprite*   VRAIE CLASSE
    │
    └─ Screens/  ······················· 13 741 l.   un etat de jeu par repertoire
        ├─ GameStates.cs    154   le repartiteur (FUN_00537ca0)
        ├─ DebugParty.cs    753   mode de mise au point (pas un etat numerote)
        ├─ State00/  MainMenu.cs 526 · Options.cs 2 517
        ├─ State01/  PartySetup.cs 544
        ├─ State02/  Destination.cs 451
        ├─ State03/  WorldMap.cs 1 173
        ├─ State04/  SaveSlots.cs 2 109 · CharacterCreation.cs 1 539 · CharacterSelect.cs 581
        └─ State06/  Inventory.cs 2 937 · ItemMenu.cs 493
```

Tout `src/Blaze/` est **une seule classe partielle `Blaze`**. Le fichier est une unité de
navigation, pas une unité d'encapsulation — voir « pourquoi pas de vraies classes » plus bas.

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

→ **La carte des symboles reconstruits, dans `Documentation/DataSymbols.md`, est ce qui la rend
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

Tout lit et écrit en **ISO-8859-1** (un octet ↔ un caractère) via `[IO.File]::ReadAllBytes` /
`WriteAllBytes`. `Get-Content` / `Set-Content` sont proscrits sur ces fichiers : ils cassent sur la
région à encodage double et ont déjà aggravé le problème une fois.
