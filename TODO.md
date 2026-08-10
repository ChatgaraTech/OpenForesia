# TODO

## Présentation

Ce fichier est à destination du responsable du projet. Il sert de point de reprise entre deux
sessions : l'IA le relit en début de séance pour repartir sur les mêmes rails.

L'historique complet et les décisions techniques sont dans `OpenForesia/src/Blaze/REVERSE_STATUS.md`.
Ce fichier-ci ne garde que l'état courant et ce qu'il faut faire ensuite.

---

## Où en est le projet

L'application **affiche l'écran-titre du CD**, **entre dans la boucle de jeu** et **rend des
frames**. Le démarrage traverse : arène display-list → CRT → scan CD → `BLAZE.cfg` → séquence
d'attract (avec l'image de `TITLE640.BMP`) → init moteur/3D → chargement des ressources → boucle
de jeu → menu principal → boucle de frame.

L'état de jeu 0 est le **menu principal**, confirmé par ses propres données : la table de libellés
lue dans le binaire contient « Début du jeu », « Sélection des personnages », « Menu objets »,
« Options », « Quitter » — exactement les cinq cas du `switch`.

### Ça tourne

- La frame complète du menu : préparation, animation de palette, deux renderers de primitives,
  `DrawOTag`, permutation des tampons, vidage de la file d'uploads VRAM.
- **`DrawOTag` s'exécute sans fauter, frame après frame.** C'était le blocage précédent.
- Le sondage d'entrées, atteint pour la première fois (il est situé après `DrawOTag`).
- `Legacy/Blaze.exe` est dans le dépôt, et `OpenForesia.Tools at <va>` lit n'importe quelle donnée.
- Sept changes OpenSpec, six implémentées.

---

## Le point de reprise

```
Unhandled exception. System.NotImplementedException:
   FUN_005443a0: menu background renderer (233 lines)
   at Blaze.MenuBackgroundStub(UInt32*)
   at Blaze.RunMainMenuFrameLoop()
```

Ce n'est plus un crash natif : c'est du **code ordinaire non couvert**, avec un corps décompilé
disponible dans `Blaze.c`. Le prochain pas est de le reconstruire.

---

## Ensuite, par ordre de rentabilité

1. **`FUN_005443a0`** (233 l.) — le renderer de fond du menu. C'est là que ça s'arrête aujourd'hui.
2. **`FUN_00545340`** — le moteur de menu/UI, 1742 lignes, 25 appels (TODO #36). Le gros morceau,
   et ce qui ferait apparaître un menu réellement affiché.
3. **`FUN_00512320`** — la page d'options (80 l.), stub actuel.
4. **`FUN_00523120`** — création du device DirectInput. Débloquerait `FUN_005048f0` et supprimerait
   la déviation du sondage joystick.
5. **Le device DirectSound** (TODO #26) — aucun `DirectSoundCreate` localisé. Toute la pile audio
   est reconstruite mais inerte : `PlayMusicTrack` s'exécute sans rien produire, ce n'est pas un bug.

---

## Dette et points ouverts

- **Sept changes OpenSpec non archivées.**
- **Trois déviations environnementales** documentées dans `REVERSE_STATUS.md` : le repli 32 bits
  quand `SetDisplayMode(640,480,16)` renvoie `E_NOTIMPL`, `Restore` sur `DDERR_SURFACELOST`, et le
  placement de l'arène display-list (voir ci-dessous).
- **Une infidélité connue, non corrigée** : l'index de `PollInputAndAudio` code en dur l'index
  externe à 0 au lieu de lire `_DAT_005de538`. Équivalent aujourd'hui, à corriger quand on
  retouchera cette zone. Détail dans `REVERSE_STATUS.md`.
- **Deux tâches non faites** dans `reconstruct-main-menu-state` : la table musicale complète
  (seules les entrées échantillonnées sont transcrites) et `FUN_005048f0`.

---

## La contrainte 24 bits — à connaître avant de toucher au rendu

Toute la display list (table d'ordre **et** primitives) est adressée sur **24 bits** : `ClearOTagR`
et `DrawOTag` masquent par `0xffffff` et se servent de l'octet de poids fort comme champ de
longueur, et `Blaze.exe` fait pareil (`*prim = *otEntry | 0x2000000`). Tout ce qui entre dans une
display list doit donc vivre **sous `0x01000000`**.

En 1998 c'était automatique. Aujourd'hui l'ASLR déplace `winsys3d.dll`, donc au démarrage on alloue
l'arène nous-mêmes en mémoire basse et on écrit son adresse dans le pointeur `MainMem` que la DLL
exporte. Le fichier `.dll` n'est pas modifié.

La ligne affichée au lancement dit où elle a atterri :

```
winsys3d arena at MainMem=0x00B6DBD8 — original 1998 layout, addresses match winsys3d.c one for one
winsys3d arena at MainMem=0x0083DBD8 — 0x00B6DBD8 was unavailable; subtract 0x330000 from ...
```

Dans le second cas, **il faut appliquer le delta** à toute adresse lue dans `winsys3d.c`.

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
