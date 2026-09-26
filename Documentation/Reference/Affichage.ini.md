# `Affichage.ini` — les réglages d'affichage du joueur

Écart legacy ++ de confort, **LB-055** (`legacy-bug.md`), introduit par
`openspec/changes/enhance-display-resolution-filtering` et ramené à quatre clés par
`openspec/changes/archive/2026-09-26-simplify-display-settings`. Le jeu de 1998 n'a ni résolution interne, ni
anticrénelage, ni mode de fenêtre, et son filtrage ne se règle que par `Filter=` (`OpenForesia.cfg`).
La touche F10, prévue pour basculer le filtrage, ne fait rien — ni dans le jeu de 1998, ni ici (voir
plus bas).

## Le fichier

- À côté de l'exécutable, en UTF-8, section `[Affichage]`, `clé = valeur`, commentaires après `;`.
- **Lu une fois au démarrage, jamais écrit par le jeu** (`Host/DisplaySettings.cs`). Les menus du jeu
  et `OpenForesia.cfg` n'en savent rien : le `.cfg` reste au format de 1998 et le jeu en reste le seul
  écrivain.
- Absent, clé absente, valeur refusée : **la valeur par défaut, clé par clé**. Chaque refus fait une
  ligne `[AFFICHAGE]` dans le journal ; rien n'empêche le démarrage.
- L'installeur en dépose un exemplaire commenté s'il n'y en a pas, et ne l'écrase jamais. Le modèle
  est `OpenForesia.Installer/Reglages/Affichage.ini` — un seul exemplaire, embarqué dans l'installeur.
  En développement, le copier à la main dans `OpenForesia\bin\Debug\net10.0\`.

## Les clés

| clé | valeurs | défaut | effet |
|---|---|---|---|
| `Rendu` | `moderne`, `origine` | `moderne` | voir ci-dessous |
| `Mode` | `fenetre`, `fenetre-sans-bordure`, `plein-ecran` | `fenetre` | le mode dans lequel la fenêtre s'ouvre |
| `Fenetre` | `auto`, `<largeur>x<hauteur>` | `auto` | la zone cliente en `fenetre` et `fenetre-sans-bordure` |
| `MiseAEchelle` | `ajustee`, `entiere` | `ajustee` | plus grand 4:3 qui tient, lissé ; ou plus grand facteur entier, au plus proche |

**`Rendu`.**

| | `moderne` | `origine` |
|---|---|---|
| taille de rastérisation | la taille à l'écran (jamais moins que la logique) | la taille logique |
| anticrénelage | MSAA 4 (repli sur 2, puis 1, si non supporté) | aucun |
| polygones que le jeu filtrait (`Filter=1`) | lissés | nets |
| autres polygones, sprites (textes, icônes, cadres) | nets | nets |

**Le rendu d'origine** : `Rendu = origine`. C'est l'image d'avant LB-055, au pixel près — la
référence des comparaisons de captures. Supprimer le fichier ne la rend **pas** : sans fichier, c'est
`moderne`.

**`Mode`.** Les trois modes d'un jeu actuel :

| mode | ce que c'est | un second écran |
|---|---|---|
| `fenetre` | fenêtre à cadre, redimensionnable | libre |
| `fenetre-sans-bordure` | sans cadre, centrée ; tout l'écran en `Fenetre = auto` ; le mode d'affichage ne change jamais | libre : le jeu reste en place quand on travaille à côté (le bestiaire, par exemple) |
| `plein-ecran` | plein écran **exclusif** (DXGI), à la définition du bureau | cliquer ailleurs fait quitter l'exclusif ; revenir au jeu le reprend |

L'exclusif est pris par la swap chain Direct3D 11 à la première image Direct3D — jusque-là
(écran-titre, attract), la fenêtre couvre l'écran sans cadre. **Alt+Entrée** bascule entre le mode
de départ et `plein-ecran` ; depuis un départ `plein-ecran`, elle ramène une fenêtre à cadre de
taille `Fenetre`.

**`Fenetre`.** Nommée ainsi, et **pas** « Resolution », parce que `OpenForesia.cfg` a déjà
`Resolution=`/`Size=` : la taille **logique** du jeu (320x240 à 1024x768, F11/F12), autre chose.

- En `fenetre-sans-bordure`, tout se mesure sur **l'écran entier**, barre des tâches comprise :
  `auto` = tout l'écran, et `1920x1080` sur un écran 1080p le couvre exactement.
- Sinon, sur la **zone de travail** (l'écran moins la barre des tâches), cadre compris : `auto` = le
  plus grand rectangle 4:3 qui tienne — 1324x993 pour un jeu en 1024x768 sur un 1080p —, ou le plus
  grand multiple entier de la taille logique en `MiseAEchelle = entiere`.

Une taille trop grande est réduite en gardant ses proportions, avec une ligne `[FEN]` au journal ;
la zone cliente retenue s'y imprime aussi (`[FEN] zone cliente de départ`).

**Clés retirées.** `ResolutionInterne`, `Anticrenelage`, `Filtrage3D` et `FiltrageInterface` (la
première version, 2026-09-26) sont ignorées avec la ligne `[AFFICHAGE] clé retirée, ignorée` — un
fichier déposé alors les porte encore, l'installeur n'écrasant jamais celui du joueur. Pourquoi elles
sont parties : une résolution interne au-delà de la taille à l'écran sur-échantillonne une image que
la fenêtre réduit aussitôt, sans rien affiner des textures ; le MSAA par-dessus lisse une seconde fois
les mêmes arêtes ; et le filtrage avait déjà son réglage, celui du jeu.

## Ce que le rendu `moderne` filtre

Le jeu est un portage PlayStation : tout arrive à l'écran **déjà projeté, sans profondeur**. Un mur
de donjon, un portrait et un cadre de fenêtre sont tous des polygones à plat ; il n'existe pas de
critère « 2D / 3D » à ce niveau. Le plus proche, et celui que le moteur connaît, est **sprite /
polygone** — le `genre` que `LierTextureOpaque` passe à la liaison (1 pour un `SPRT`, 0 pour un
polygone), mémorisé par winsys3d dans `Memo_00a2c48c`. Les sprites (textes, icônes, cadres,
curseurs) ne sont jamais filtrés.

**Ce que le filtrage des polygones reproduit.** À chaque liaison, `LierEmplacement` (winsys3d
`FUN_00a27430`) demande un mode : 0 pour un sprite, **0 aussi pour un polygone sur page 8 bits quand
`Zoom8BitTextures` est posé** — les petits personnages 3D —, 1 sinon. `SetTextureFilterMode`
(ordinal 798) transmet au device « filtre » si ce mode est non nul **et** que `TexFilterPossible`
l'autorise (`Filter=`, lu au chargement). Un polygone est donc lissé exactement quand le jeu de 1998
le lissait : le décor et les portraits oui, les personnages non. Avant LB-055, le device synthétique
ignorait ce slot (`+0x90`) et tout était au plus proche.

D'où vient ce découpage : une première trace (`OPENFORESIA_TRACE_FILTER=1`, 2026-09-26, 105 couples
texture/classe) classait par le seul mode demandé, et mettait les personnages 3D avec les textes
tandis que portraits, fonds, AUTO/MAN et boussole tombaient du côté filtrable. Le classement par
genre est venu de là ; la ligne `[FILTRE]` imprime le genre.

## Limites connues

- **Le tremblement des polygones reste.** Le moteur produit ses coordonnées écran en entiers sur une
  grille de 320 de large (`Documentation/DLL/winsys3d.md`) ; rastériser plus fin affine les arêtes,
  pas les positions. Le corriger est un autre chantier.
- **L'image reste en 4:3**, avec des bandes noires sur un écran large.
- Le `discard` de la couleur-clé n'est pas anticrénelé : seules les arêtes de géométrie le sont.
- **F10 ne bascule rien, comme en 1998.** winsys3d la lit dans `LireManettes` (`FUN_00a049c0`), qui
  sort d'emblée tant que `DAT_00dedc40` vaut 0 — et rien ne le pose : les ordinaux 36/759 qui le
  feraient ne sont jamais appelés par `Blaze.exe`. Une touche de développeur restée dans la version
  vendue, inactive. Constaté en jeu le 2026-09-26.
