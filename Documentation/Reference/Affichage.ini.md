# `Affichage.ini` — les réglages d'affichage du joueur

Écart legacy ++ de confort, **LB-055** (`legacy-bug.md`), introduit par
`openspec/changes/enhance-display-resolution-filtering`. Le jeu de 1998 n'a ni résolution interne ni
anticrénelage, et son filtrage ne se règle que par `Filter=` (`OpenForesia.cfg`). La touche F10, prévue pour
basculer le filtrage, ne fait rien — ni dans le jeu de 1998, ni ici (voir plus bas).

## Le fichier

- À côté de l'exécutable, en UTF-8, sections `[Affichage]` et `[Textures]`, `clé = valeur`,
  commentaires après `;`.
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
| `[Affichage] ResolutionInterne` | `auto`, `1`..`8` | `auto` | la scène est rastérisée N fois plus fin que la taille logique du jeu ; `auto` = exactement la taille à l'écran |
| `[Affichage] Anticrenelage` | `1`, `2`, `4`, `8` | `4` | MSAA de la scène ; repli sur le plus grand nombre supporté inférieur |
| `[Affichage] MiseAEchelle` | `ajustee`, `entiere` | `ajustee` | plus grand 4:3 qui tient, lissé ; ou plus grand facteur entier, au plus proche |
| `[Textures] Filtrage3D` | `jeu`, `proche`, `bilineaire` | `jeu` | les polygones : décor, personnages, portraits, fonds |
| `[Textures] FiltrageInterface` | `net`, `lisse` | `net` | les sprites : textes, icônes, cadres |

**Le rendu d'origine** : `ResolutionInterne = 1`, `Anticrenelage = 1`, `Filtrage3D = proche`. C'est
l'image d'avant ce changement, au pixel près. Supprimer le fichier ne la rend **pas** : sans fichier,
ce sont les défauts modernes.

## Ce que filtrent `Filtrage3D` et `FiltrageInterface`

Le jeu est un portage PlayStation : tout arrive à l'écran **déjà projeté, sans profondeur**. Un mur
de donjon, un portrait et un cadre de fenêtre sont tous des polygones à plat ; il n'existe pas de
critère « 2D / 3D » à ce niveau. Le plus proche, et celui que le moteur connaît, est **sprite /
polygone** — le `genre` que `LierTextureOpaque` passe à la liaison (1 pour un `SPRT`, 0 pour un
polygone), mémorisé par winsys3d dans `Memo_00a2c48c`.

| ce qui est dessiné | exemples | clé |
|---|---|---|
| **sprite** (genre ≠ 0) | textes, icônes, cadres, curseurs | `FiltrageInterface` |
| **polygone** | décor, personnages, grands portraits, fonds d'écran, AUTO/MAN, boussole | `Filtrage3D` |

**Ce que `Filtrage3D = jeu` reproduit.** À chaque liaison, `LierEmplacement` (winsys3d
`FUN_00a27430`) demande un mode : 0 pour un sprite, **0 aussi pour un polygone sur page 8 bits quand
`Zoom8BitTextures` est posé** — les petits personnages 3D —, 1 sinon. `SetTextureFilterMode`
(ordinal 798) transmet au device « filtre » si ce mode est non nul **et** que `TexFilterPossible`
l'autorise (`Filter=`, lu au chargement). En `jeu`, un polygone est donc lissé exactement
quand le jeu de 1998 le lissait : le décor et les portraits oui, les personnages non. Jusqu'à ce
changement, le device synthétique ignorait ce slot (`+0x90`) et tout était au plus proche.

| polygone… | `jeu` | `proche` | `bilineaire` |
|---|---|---|---|
| que le jeu filtrait (`Filter=1`) | lissé | net | lissé |
| filtre coupé (`Filter=0`) | net | net | lissé |
| sur page 8 bits zoomée (personnages) | net | net | lissé |

D'où vient ce découpage : une première trace (`OPENFORESIA_TRACE_FILTER=1`, 2026-09-26, 105 couples
texture/classe) classait par le seul mode demandé, et mettait les personnages 3D avec les textes
tandis que portraits, fonds, AUTO/MAN et boussole tombaient du côté filtrable. Le classement par
genre est venu de là ; il reste à le confirmer par une seconde trace — la ligne `[FILTRE]` imprime
désormais le genre.

## Limites connues

- **Le tremblement des polygones reste.** Le moteur produit ses coordonnées écran en entiers sur une
  grille de 320 de large (`Documentation/DLL/winsys3d.md`) ; une résolution interne affine les
  arêtes, pas les positions. Le corriger est un autre chantier.
- **L'image reste en 4:3**, avec des bandes noires sur un écran large.
- Le `discard` de la couleur-clé n'est pas anticrénelé : seules les arêtes de géométrie le sont.
- **F10 ne bascule rien, comme en 1998.** winsys3d la lit dans `LireManettes` (`FUN_00a049c0`), qui
  sort d'emblée tant que `DAT_00dedc40` vaut 0 — et rien ne le pose : les ordinaux 36/759 qui le
  feraient ne sont jamais appelés par `Blaze.exe`. Une touche de développeur restée dans la version
  vendue, inactive. Constaté en jeu le 2026-09-26.
