## Why

L'arène (`add-legacy-plus-arena`, LP-012) se valide sur l'Enchanteresse, un boss natif de la tour.
Vincent veut y affronter le **Grizzly**, premier boss du jeu, qui vit dans la forêt (zone 3,
sous-zone 5) — et, plus tard, des boss rush tirés de toutes les zones. Or un monstre n'existe que dans
sa zone : son modèle, sa texture, sa fiche, ses sons et ses effets sont chargés avec elle, et en zone 4
rien de tout cela n'est là. Il faut une **greffe** : faire venir un monstre d'une autre zone et le faire
combattre dans l'arène comme chez lui (legacy ++, LP-013).

## What Changes

- **La greffe générique** : relire le répertoire d'état d'une autre zone, y prendre le modèle, les
  animations, la texture et la fiche d'une espèce, lui trouver une place en VRAM, et le faire naître
  dans l'arène avec son IA d'origine — la technique de `MarchandsHub`, étendue aux monstres.
- **La traduction des références de zone** : un boss joue des sons et fait naître des effets par
  **numéro**, et ces numéros désignent autre chose dans la tour. Chaque boss greffé porte sa table de
  traduction (le son 3 et l'effet 0x39 pour le Grizzly), appliquée par une couture tant qu'il agit ;
  une référence non traduite est coupée plutôt que de désigner l'effet d'une autre zone.
- **Le mode GRIZZLY** au menu de l'arène : une vague, le Grizzly de la forêt (modèle 3 : 80 PV,
  rang 25) greffé à la place du boss de la tour, palier 1 à 10 (PV et dégâts × palier).
- **Un outil de recensement** (`OpenForesia.Tools`) qui, pour chaque IA de monstre, liste ses
  références dépendantes de sa zone (effets, sons, sous-entités, textures) — le devis de chaque boss
  pour les boss rush à venir, comme `Zone<N>ParComplexite.md` pour les lots d'IA.

## Capabilities

### New Capabilities

- `gameplay-arena-bosses` : un boss venu d'une autre zone combat dans l'arène comme chez lui (aspect,
  comportement, sons, effets), et le mode GRIZZLY.

### Modified Capabilities

(aucune — `gameplay-hub-arena` n'existe qu'après l'archivage de `add-legacy-plus-arena` ; ce
changement s'y ajoute par une capacité à part et n'en change aucune exigence)

## Impact

- **Dépend de** `add-legacy-plus-arena` : le directeur, le programme de vagues, le palier.
- **Legacy** (`src/Blaze/`) : des coutures sur le son positionnel et la naissance d'un effet (la
  traduction), identité hors arène. Aucun corps transcrit modifié.
- **LegacyPlus** : la greffe (fichier nouveau, nom proposé à Vincent), la table de traduction du
  Grizzly, le programme GRIZZLY.
- **Outils** : le recensement des dépendances de zone, et son document généré dans
  `Documentation/Etat/`.
- **Documentation** : `legacy.md` (LP-013), `HISTORY.md`.
