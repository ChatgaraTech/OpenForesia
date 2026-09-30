## Why

L'arène (LP-012) et la greffe (LP-013) permettent de rejouer chaque combat de boss du jeu, un par un.
Vincent veut maintenant des **challenges** : plusieurs boss à la suite, dans un même passage dans
l'arène, sans escorte. Le directeur sait déjà enchaîner des vagues ; ce qui manque, c'est la place :
la greffe installe toutes les espèces d'un programme à l'entrée dans 4/9, dans cinq bandes de VRAM,
et les boss du troisième challenge en demandent huit (Eifridt deux, Belzébuth trois).

## What Changes

- **Quatre challenges** au menu de l'arène, sur la **page 3**, après les dix-neuf combats :
  - `[CHALLENGE] - Boss Rush 1` : Méchant ours, Jeune dragon, Enchanteresse, Loup-garou et Tigre
    (ensemble), Nocturio ;
  - `[CHALLENGE] - Boss Rush 2` : Sylphide, Fougre, Durahan, Troll, Behemoth ;
  - `[CHALLENGE] - Boss Rush 3` : Gros poilu, Roi-zombie, Cerbère, Eifridt, Belzébuth ;
  - `[CHALLENGE] - Ultimate` : Lordémon, Ange noir, deux Dragons zombies ensemble, deux Dragons
    rouges ensemble.
- **Le déroulé** : un boss (ou un couple) par vague ; quand son corps a disparu, **cinq secondes** de
  pause, puis le suivant ; après le dernier, la fin de l'arène telle qu'elle est (cinématique,
  bourse, portail). Aucune escorte.
- **La difficulté** : les quatre paliers existants (PV × 1 à 4, 30 000 au plus ; dégâts × 1 à 2,5 ;
  bourse × 1 à 4), inchangés.
- **La bourse** : la somme des bourses des combats qui composent le challenge, + 25 %, arrondie à la
  dizaine quand le compte ne tombe pas juste : 750, 1 390, 2 290 et 5 875 pièces au palier Normal.
- **La greffe par vague** : les textures des espèces greffées sont placées en VRAM au moment de
  naître, pour la seule vague qui naît, et plus pour tout le programme à l'entrée — chaque vague
  dispose des cinq bandes.
- **L'écran de menu** : une ligne peut ouvrir une nouvelle page (le saut de page des challenges).

## Capabilities

### New Capabilities
- `gameplay-arena-boss-rush` : les challenges de l'arène — leur contenu, leur déroulé en vagues avec
  pause, leur bourse, leur place au menu.

### Modified Capabilities
(aucune : les combats d'un seul boss gardent leur comportement ; la greffe par vague ne change rien
de visible pour eux)

## Impact

- `OpenForesia/src/LegacyPlus/AreneDirecteur.cs` : les quatre programmes, une pause par programme,
  l'appel de la greffe avant chaque vague.
- `OpenForesia/src/LegacyPlus/AreneGreffe.cs` : l'installation en deux temps (en mémoire à l'entrée,
  en VRAM par vague).
- `OpenForesia/src/LegacyPlus/AreneTeleporteur.cs`, `EcranMenu.cs` : le saut de page.
- `Documentation/LegacyPlus/legacy.md` (LP-014), `HISTORY.md`.
- Hors du legacy : aucune couture nouvelle, rien dans `src/Blaze/`.
