## Why

`Affichage.ini` (livré le 2026-09-26 par `enhance-display-resolution-filtering`, LB-055) expose cinq
réglages dont trois ne servent presque à rien au joueur. `ResolutionInterne` n'affine que les arêtes
des polygones, jamais les textures : sur les écrans faits de sprites (titre, menus) ×1 et ×8 donnent
la même image, et au-delà de `auto` on sur-échantillonne une image que la fenêtre réduit aussitôt
(×8 = 5120x3840 pour une fenêtre de 1340x1000). `Anticrenelage` par-dessus ×8 lisse une deuxième fois
les mêmes arêtes. `Filtrage3D` et `FiltrageInterface` doublent un réglage que le jeu a déjà
(`Filter=`), dont le comportement par défaut convient. Vincent l'a constaté en jeu : « passer à 8 ne
change pas grand-chose ».

Ce qui manque en revanche, c'est ce qu'un joueur cherche d'abord : **dans quelle fenêtre** le jeu
s'ouvre. Aujourd'hui elle s'ouvre toujours fenêtrée, à la taille logique du jeu (640x480, minuscule
sur un écran moderne), et le plein écran n'existe que par Alt+Entrée, sans être retenu.

## What Changes

- **BREAKING** (pour le fichier, pas pour le jeu) : retrait des clés `ResolutionInterne`,
  `Anticrenelage`, `Filtrage3D` et `FiltrageInterface`. Présentes dans un ancien `Affichage.ini`,
  elles sont ignorées avec une ligne de journal « clé retirée » — jamais une erreur.
- Le rendu moderne devient fixe : scène rastérisée à la taille où elle est affichée (l'ancien
  `auto`), MSAA 4 (ou le plus haut supporté en dessous), textures 3D filtrées **comme le jeu le
  demande** (`Filter=` du `.cfg`), interface toujours nette.
- Nouvelle clé `[Affichage] Rendu = moderne | origine`. `origine` reproduit l'image de 1998 au pixel
  près (taille logique, sans MSAA, au plus proche partout) — l'interrupteur que demandent les
  comparaisons de captures du protocole de test, et le seul qui reste pour « revenir en arrière ».
- Nouvelle clé `[Affichage] Mode = fenetre | fenetre-sans-bordure | plein-ecran` : le mode dans
  lequel la fenêtre s'ouvre — les trois modes d'un jeu actuel. `fenetre-sans-bordure` couvre l'écran
  sans jamais changer le mode d'affichage : la souris va et vient sur un second écran (le bestiaire
  ouvert à côté) sans que le jeu s'efface. `plein-ecran` est le plein écran **exclusif** (DXGI), à la
  définition du bureau, que le jeu quitte quand il perd le focus.
- Nouvelle clé `[Affichage] Fenetre = auto | <largeur>x<hauteur>` : la taille de la zone cliente
  en `fenetre` et `fenetre-sans-bordure`. `auto` = le plus grand 4:3 qui tienne dans l'écran de
  travail (tout l'écran en `fenetre-sans-bordure`). Nommée « Fenetre » et non « Resolution » : le
  `.cfg` a déjà un `Resolution=` (la taille logique, F11/F12), qui est une autre chose.
- `MiseAEchelle = ajustee | entiere` est conservée telle quelle.
- Le modèle d'`Affichage.ini` de l'installeur et `Documentation/Reference/Affichage.ini.md` sont
  réécrits sur ces trois clés + `MiseAEchelle`.

Le plein écran exclusif avait d'abord été écarté (la fenêtre du jeu est faite pour que `3dhelp` ne
déclenche plus de changement de mode). Vincent l'a demandé après essai : sans lui, `plein-ecran` et
`fenetre-sans-bordure` étaient la même chose. Il est pris par la swap chain Direct3D 11, jamais par
`3dhelp`, et seulement après que celui-ci a rendu l'affichage (design D9).

## Capabilities

### New Capabilities

_(aucune)_

### Modified Capabilities

- `display-enhancements` : les réglages de résolution interne, d'anticrénelage et de filtrage
  disparaissent au profit d'un rendu moderne fixe et d'un interrupteur `Rendu` ; le mode et la
  taille de la fenêtre au démarrage entrent dans le fichier.

## Impact

- `OpenForesia/src/Host/DisplaySettings.cs` — analyseur : trois clés en moins, trois en plus, clés
  retirées reconnues.
- `OpenForesia/src/Host/D3D11Backend.cs` — `PhysicalTargetSize` et `SupportedSampleCount` lisent
  `Rendu` au lieu des deux clés retirées.
- `OpenForesia/src/Host/DeviceShim.cs` — le choix du filtrage par genre de liaison (`jeu` fixe,
  `origine` = au plus proche) ; `CreateGameWindow` passe la taille et le mode de fenêtre.
- `OpenForesia/src/Host/D3D11Window.cs` — création dans le mode demandé ; Alt+Entrée bascule entre
  le mode de départ et `plein-ecran`.
- `OpenForesia.Installer/Reglages/Affichage.ini` — modèle réécrit. L'installeur continue de ne
  jamais écraser le fichier du joueur : un ancien fichier garde ses anciennes clés, ignorées.
- Documentation : `Affichage.ini.md`, `legacy-bug.md` (LB-055, interrupteurs), `test-protocol.md`
  et `CLAUDE.md` (la recette de comparaison devient `Rendu = origine`).
- Aucun code reconstruit (`src/Blaze/`) n'est touché ; `OpenForesia.cfg` non plus.
