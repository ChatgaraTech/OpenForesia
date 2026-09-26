## Why

Sur un écran d'aujourd'hui (1440p, 2160p), le jeu calcule son image dans une cible de 640x480
(au plus 1024x768), puis l'étire **au pixel le plus proche** jusqu'à la fenêtre : à un facteur non
entier, les pixels sortent de largeurs inégales et l'image paraît sale. En parallèle, le réglage de
filtrage de texture **que le jeu de 1998 possédait déjà** (`Filter=` dans `OpenForesia.cfg` et
le choix texture par texture de `LierEmplacement`) n'arrive nulle part : le slot
`+0x90` du device synthétique n'est pas branché et le backend D3D11 n'a qu'un échantillonneur
« au plus proche » codé en dur. Le mode « legacy ++ » vise un jeu propre et jouable sur le matériel
actuel ; c'est le premier chantier graphique.

## What Changes

- **Résolution interne** : le backend rastérise la scène dans une cible N fois plus grande que la
  taille logique demandée par le jeu (N = 1 à 8, ou « auto » = exactement la taille affichée),
  sans que le moteur voie la différence — ses sommets restent en pixels logiques.
- **Anticrénelage** multi-échantillonné (MSAA 1/2/4/8) de cette cible.
- **Mise à l'échelle finale** au choix : ajustée et lissée (remplit la fenêtre en 4:3), ou entière
  (pixels réguliers, bandes noires plus larges).
- **Filtrage des textures 3D** : `jeu` (suit `Filter=` et la décision par texture du moteur
  d'origine), `proche`, ou `bilineaire` forcé — avec un filtrage qui respecte la couleur-clé (pas
  de halo sombre autour des texels transparents) et ne déborde pas hors de la zone de la page de
  texture que la primitive adresse.
- **Filtrage de l'interface** (textures que le moteur d'origine ne filtrait jamais) : `net` ou
  `lisse`.
- **Un fichier de réglages dédié, `Affichage.ini`**, à côté de l'exécutable : **lu** par le jeu au
  démarrage, **jamais écrit** par lui. Absent, clé absente ou valeur invalide → valeur par défaut.
  Les valeurs par défaut sont « modernes » (`auto`, MSAA 4, `jeu`, `net`) ; le rendu de 1998 reste
  à quelques lignes de réglage. Aucun menu du jeu n'est modifié.
- **L'installeur** dépose un `Affichage.ini` commenté s'il n'en existe pas, sans jamais écraser
  celui du joueur.

Hors périmètre, en changements séparés : lissage « pixel art » de l'interface (xBR/ScaleFX),
écran large, précision des sommets (tremblement PSX), configurateur graphique.

## Capabilities

### New Capabilities

- `display-enhancements` : comment l'image du jeu est rendue et présentée sur un écran moderne —
  résolution interne, anticrénelage, mise à l'échelle, filtrage des textures 3D et de l'interface —
  et comment le joueur le règle par `Affichage.ini`.

### Modified Capabilities

- `game-data-install` : l'installation dépose un `Affichage.ini` par défaut, commenté, et ne
  l'écrase pas lors d'une réinstallation.

## Impact

- **Hôte** (`OpenForesia/src/Host/`, non tenu à la fidélité) : `D3D11Backend.cs` (cible hors écran
  mise à l'échelle et multi-échantillonnée, résolution MSAA, échantillonneurs, shader de pixels,
  présentation), `SyntheticDevice.cs` (branchement du slot `+0x90`, le filtrage), `DeviceShim.cs`
  si la taille logique y transite ; un nouveau lecteur de réglages dans `Host/`.
- **Moteur** (`src/Blaze`, `src/Libraries`) : **aucune modification** de code transcrit. Le choix de
  filtrage par texture est déjà calculé par `LierEmplacement` (winsys3d) ; il est seulement enfin
  reçu.
- **Installeur** (`OpenForesia.Installer`) : écriture du fichier par défaut.
- **Documentation** : `Documentation/Reference/Affichage.ini.md` (nouveau, à côté de
  `OpenForesia.cfg.md`), une entrée de confort dans `legacy-bug.md`, `test-protocol.md` (captures à
  comparer en `ResolutionInterne=1` contre le rendu actuel), `HISTORY.md`.
- **Performances** : une cible 2560x1920 en MSAA 4 reste modeste pour un GPU actuel ; la valeur 1
  retrouve le coût d'aujourd'hui.
