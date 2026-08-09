# TODO

## Présentation

Ce fichier est à destination du responsable du projet. Il sert de point de reprise entre deux
sessions : l'IA le relit en début de séance pour repartir sur les mêmes rails.

L'historique complet et les décisions techniques sont dans `OpenForesia/src/Blaze/REVERSE_STATUS.md`.
Ce fichier-ci ne garde que l'état courant et ce qu'il faut faire ensuite.

---

## Où en est le projet

L'application **affiche l'écran-titre du CD** et **entre dans la boucle de jeu**. Le démarrage
traverse : CRT → scan CD → `BLAZE.cfg` → séquence d'attract (avec l'image de `TITLE640.BMP`)
→ init moteur/3D → chargement des ressources → boucle de jeu → menu principal.

L'état de jeu 0 est le **menu principal**, confirmé par ses propres données : la table de libellés
lue dans le binaire contient « Début du jeu », « Sélection des personnages », « Menu objets »,
« Options », « Quitter » — exactement les cinq cas du `switch`.

### Ça tourne

- La frame complète du menu : préparation, animation de palette, deux renderers de primitives,
  `DrawOTag`, permutation des tampons, vidage de la file d'uploads VRAM.
- `Legacy/Blaze.exe` est dans le dépôt, et `OpenForesia.Tools at <va>` lit n'importe quelle donnée.
- Six changes OpenSpec, cinq implémentées.

---

## ⚠ Le point de reprise : une régression non résolue

**Symptôme exact**

```
Fatal error. 0xC0000005
   at Blaze.Ordinal_701(UInt32*)          ← DrawOTag
   at Blaze.FrameEnd(...)
   at Blaze.RunMainMenuFrameLoop()
```

Crash **natif**, pas une exception descriptive. Il survient sur la **seconde** frame : la première
passe entièrement. Donc quelque chose dans le premier passage du sondage d'entrées corrompt l'état
entre les deux frames.

**Ce qui a introduit la régression** : la reconstruction des quatre fonctions de sondage
(`PollInputAndAudio` dans `Blaze.cs`). Avant elles, le programme levait proprement à cet endroit.

**Ce qui est écrit et compile** (0 erreur) :

| Fonction | État |
|---|---|
| `FUN_00512520` → `PollDebugHotkeys` | complet — F3 options, F11/F12 détail, F9 textures |
| `FUN_00504a30` → `UpdateButtonEdges` | complet — fronts + auto-répétition |
| `FUN_005357a0` → `AssignControllersToSlots` | complet — 4 rangées de remappage lues du binaire |
| `FUN_005048f0` → `PollJoystickState` | gardé : le device DirectInput n'existe pas (`FUN_00523120` non reconstruite) |

### Mes deux suspects — non vérifiés

1. **Le calcul d'index dans `PollInputAndAudio`.** Il traverse trois indirections
   (`DAT_005e93c8` → `DAT_005e9188` → `DAT_00604a20`) sur des tables dont je **n'ai pas dumpé le
   contenu initial**. Si une de ces valeurs n'est pas ce que je suppose, j'écris à côté.
2. **`PollDebugHotkeys` déclenchant une branche F11/F12 au premier appel.** `GetAsyncKeyState`
   renvoie un bit « pressé depuis le dernier appel » qui peut être vrai spontanément ; le `SetShift`
   qui suit modifierait l'état du viewport entre deux frames.

### Les deux façons de trancher

**A — Isoler (aucune dépendance externe).** Ne garder qu'un des quatre appels dans
`PollInputAndAudio`, relancer, répéter. Quatre lancements et on sait lequel corrompt.

**B — Exporter les `.asm` de `FUN_00512520` et `FUN_005357a0`.** Les deux font de l'arithmétique
de pointeurs dense sur des tables dont Ghidra rend les indices de façon ambiguë. C'est exactement
le profil des quatre cas où le désassemblage a tranché en minutes.

> **Leçon de la session, vérifiée quatre fois** (`FUN_005322f0`, `FUN_00542600`, `FUN_00512930`,
> `FUN_0052a280`) : quand Ghidra perd des paramètres, **lire le désassemblage**. Deux fois le
> pseudo-code n'était pas seulement incomplet mais **faux**. Et sur le chemin d'affichage, trois
> hypothèses successives se sont révélées fausses — c'est l'instrumentation dans un fichier qui a
> donné la réponse, pas le raisonnement.

---

## Ensuite, par ordre de rentabilité

1. **Résoudre la régression** (ci-dessus). Prérequis à tout le reste.
2. **`FUN_00545340`** — le moteur de menu/UI, 1742 lignes, 25 appels (TODO #36). Le gros morceau,
   et ce qui ferait apparaître un menu réellement affiché. Corps disponible dans `Blaze.c`.
3. **`FUN_005443a0`** (233 l.) et **`FUN_00512320`** (la page d'options, 80 l.) — stubs actuels.
4. **`FUN_00523120`** — création du device DirectInput. Débloquerait `FUN_005048f0` et supprimerait
   la déviation.
5. **Le device DirectSound** (TODO #26) — aucun `DirectSoundCreate` localisé. Toute la pile audio
   est reconstruite mais inerte : `PlayMusicTrack` s'exécute sans rien produire, ce n'est pas un bug.

---

## Dette et points ouverts

- **Rien n'est commité.** L'arbre cumule le travail d'affichage, l'état 0 et le sondage d'entrées.
  Ça fait beaucoup pour un seul commit — probablement à découper.
- **Six changes OpenSpec non archivées.**
- **Deux déviations environnementales** documentées dans `REVERSE_STATUS.md` : le repli 32 bits
  quand `SetDisplayMode(640,480,16)` renvoie `E_NOTIMPL`, et `Restore` sur `DDERR_SURFACELOST`.
- **Deux tâches non faites** dans `reconstruct-main-menu-state` : la table musicale complète
  (seules les entrées échantillonnées sont transcrites) et `FUN_005048f0`.
- **`GetMainMemPtr()` fonctionne** — le TODO #33 qui le disait nul est obsolète.

---

## Comment lancer

Build et exécution demandent des environnements **incompatibles** (l'appli est x86, le seul SDK
est x64) :

```
Build :  $env:DOTNET_ROOT="C:\Users\Sagara\.dotnet" ; dotnet build OpenForesia.sln
Run   :  DOTNET_ROOT non défini, lancer OpenForesia\bin\Debug\net9.0\OpenForesia.exe
```

Le CD doit être monté. Cliquer la fenêtre dans la barre des tâches pour lui donner le focus,
puis une touche pour sortir de l'attract.
