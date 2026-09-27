## Why

Dans le jeu de 1998, l'or que l'on ramasse ne sert à rien : aucune boutique, aucun achat. Vincent
veut une boutique dans l'auberge — quête 0, carte 82 « Le coffre », à gauche du coffre — qui vende
des équipements. C'est du contenu nouveau, à sa demande (legacy ++, LP-008).

L'étape 1 (mesure, 2026-09-27) a tranché la forme : la carte 82 ne charge aucun modèle de
personnage, donc **pas de PNJ** ; **l'armoire** de la carte (décor en −1372, 400, 900, relevé en jeu)
est le point de vente. Voir `Documentation/LegacyPlus/legacy.md`, « En cours : LP-008 ».

## What Changes

- **LP-008 — la boutique de l'auberge**, désactivée par défaut, réglée par un fichier dédié
  `Boutique.ini` (activation et catalogue : un objet par ligne, avec son prix).
- **Étape 2 — l'ouverture** : un joueur debout devant l'armoire, qui la regarde et appuie sur son
  bouton d'action, ouvre une boîte de texte du jeu (« Bienvenue à la boutique de l'auberge ! »), avec
  la mise en scène d'un coffre (entités gelées, main rendue à la fermeture). L'appui est consommé :
  le joueur ne frappe pas l'armoire.
- **Étape 3 — la boutique** (à préciser dans ce change avant de l'écrire) : la liste des objets, les
  prix, la bourse du joueur qui a ouvert la boutique, l'achat, et la remise de l'objet par le chemin
  des coffres, à partir de sa fiche brute.
- **Une ligne dans le legacy** pour l'étape 2 (`Tick/PartyMemberStep.cs`), marquée
  `// Couture legacy ++ (LP-008)`.
- **Un modèle `Boutique.ini`** dans l'installeur, généré par `OpenForesia.Tools/generer-boutique.mjs`.

## Capabilities

### New Capabilities

- `gameplay-inn-shop` : la boutique de l'auberge — ouverture à l'armoire, catalogue, achat.

### Modified Capabilities

- `game-data-install` : l'installeur dépose aussi un `Boutique.ini` par défaut (boutique
  désactivée, catalogue), sans écraser celui du joueur.

## Impact

- **Legacy** : deux lignes — `Tick/PartyMemberStep.cs` (`PasDuMembre`, l'ouverture) et
  `Tick/WorldMapHud.cs` (`DessinSurImpression`, l'écran).
- **LegacyPlus** : `Coutures.cs`, `ReglagesBoutique.cs` (lecture de `Boutique.ini`), `Boutique.cs`
  (l'ouverture), `BoutiqueEcran.cs` (l'écran), `CatalogueObjets.cs` (fiches et icônes lues dans les
  fichiers du jeu), `BoutiqueAchat.cs` (l'achat).
- **Installeur** : `Reglages/Boutique.ini`, embarqué et déposé.
- **Outils** : `OpenForesia.Tools/generer-boutique.mjs` (déjà écrit à l'étape 1).
