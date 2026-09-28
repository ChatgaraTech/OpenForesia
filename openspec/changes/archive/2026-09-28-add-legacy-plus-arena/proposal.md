## Why

Vincent veut un mode de jeu rejouable à l'infini depuis le hub de l'auberge : affronter un boss, à
une difficulté choisie, dans une salle-arène, avec une récompense — et, plus tard, des boss rush et
des monstres à la chaîne. La salle retenue est « Parmi les Elus », la salle du boss de la vieille
tour (zone 4, sous-zone 9) : elle « fait vraiment arène », et elle porte déjà la cinématique de fin de
boss du jeu (le portail qui apparaît, sous-type 22, `0x00474380`) — on la réutilise au lieu de la
recréer (legacy ++, LP-012).

Ce changement construit **toute la mécanique de l'arène** et la valide sur un boss **natif** de la
tour, l'Enchanteresse : aucun monstre n'y vient d'une autre zone. Faire venir le Grizzly de la forêt,
et après lui d'autres boss, est le changement suivant (`add-legacy-plus-boss-graft`) : c'est la
partie risquée, et elle ne doit pas se mêler à la validation du reste.

## What Changes

- **Le téléporteur du hub** : un téléporteur, du même modèle que celui de l'entrée du Labyrinthe
  des morts (zone 5, sous-zone 0), posé dans le hub en (−766, 1235). À son approche, la question
  « Ce téléporteur semble dangereux, on y va ? » Oui / Non.
- **Le menu de l'arène**, sur le modèle de l'écran des boutiques, tenu par le joueur qui a touché le
  téléporteur : d'abord le **mode** (un boss, plus tard des enchaînements), puis le **palier** de 1 à
  10, puis la confirmation. Au palier *n*, les PV et l'attaque du boss sont multipliés par *n*
  (palier 1 = le jeu d'origine).
- **Le mode arène** : la sous-zone 4/9 chargée avec un interrupteur à nous. Levé, la salle est
  **vidée** à la naissance de ses entités (monstres, coffre, décor scripté) sauf ce que l'arène
  garde (la scénette de sortie), le script de la sous-zone ne touche à aucun drapeau de scénario, et
  la partie ne s'y sauvegarde pas. Baissé, la vraie tour est intacte.
- **Le directeur de l'arène** : un programme de vagues (une seule, un boss, dans ce changement) ; il
  fait naître la vague, attend sa mort, puis déclenche la fin.
- **La fin** : la cinématique native (scénette 22), puis un portail au centre de la salle qui ramène
  le groupe **au centre du hub**, et **deux coffres** au centre (leur butin est défini à part ; ici,
  des coffres posés et vides ou au contenu provisoire).
- **La défaite** : quand tout le groupe tombe dans l'arène, le fondu de mort du jeu joue, puis — au
  lieu de la fin de partie — le groupe est **ressuscité** et ramené au hub, avec le message « Ouille,
  nom d'un panda à la sauce tomate, j'ai mal à la tête ».
- **Rejouable à l'infini** : rien de ce que fait l'arène n'écrit de drapeau de scénario ; le
  téléporteur du hub y ramène autant de fois qu'on veut.
- L'arène existe dès que le hub existe (`[Auberge] Hub = oui`) : aucune clé de réglage de plus.

## Capabilities

### New Capabilities

- `gameplay-hub-arena` : le téléporteur du hub, le menu (mode, palier, confirmation), le mode arène
  de la sous-zone 4/9, le directeur de vagues, la fin (cinématique, portail, coffres), la défaite
  (retour au hub), et l'absence de toute trace dans le scénario et la sauvegarde.

### Modified Capabilities

(aucune — le hub, ses marchands et l'écran de boutique gardent leurs exigences ; l'écran de menu de
l'arène réutilise l'écran de boutique sans en changer le comportement)

## Impact

- **Legacy** (`src/Blaze/`) : des coutures nouvelles, une ligne chacune, identité par défaut — le
  passage à l'état 9 (`Tick/WorldMapHud.cs:181`), la naissance des monstres (pour le palier), et ce
  que l'étape de mesure désignera pour les drapeaux et la sauvegarde. Aucun corps transcrit modifié.
- **LegacyPlus** : fichiers nouveaux pour le téléporteur, le menu, le mode arène et le directeur
  (noms proposés à Vincent) ; `HubAuberge.cs` (téléporteur, retour au centre), `BoutiqueEcran.cs`
  (un écran de menu générique, ou une sœur).
- **Documentation** : `legacy.md` (LP-012), `LegacyPlus.ini.md` (l'arène suit le hub), `HISTORY.md`.
- **Suite** : `add-legacy-plus-boss-graft` (le Grizzly, puis la greffe générique et son
  recensement) s'appuie sur le directeur et le programme de vagues d'ici.
