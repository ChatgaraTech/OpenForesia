## Why

Vincent veut un lieu à nous dans l'auberge : une pièce « legacy ++ » où viendront, par la suite, des
PNJ marchands, le coffre personnel et d'autres fonctions propres au projet. Le jeu de 1998 n'a aucune
place pour cela ; la carte 82 « Le coffre » sert déjà de point de vente (LP-008), mais c'est une
chambre du jeu, meublée, et elle n'existe sous ce numéro qu'au **dernier palier** de l'histoire.

Ce change ne pose que **le lieu** : une porte dans la réception, une pièce vide derrière, un retour
fiable. Aucune fonction dans le hub (legacy ++, LP-009).

## What Changes

- **LP-009 — le hub de l'auberge**, désactivé par défaut, sous la clé `[Auberge] Hub` de
  `LegacyPlus.ini` (`oui` / `non`, défaut `non` = jeu de 1998).
- **Étape 1 — la mesure**, avant toute écriture : collisions de la carte 82 une fois vidée, numéro de
  sous-zone libre (ou interception de la lecture du répertoire), ce que la sauvegarde retient de la
  position, affichage du modèle de porte dans la salle (section 0 de la réception), mur choisi (relevé
  `TracePosition` par Vincent).
- **Une porte dans la réception de chaque palier** (sous-zones 0, 8, 16 … 80, `sous-zone & 7 == 0`) :
  le modèle de porte de l'auberge, posé contre le mur relevé. Face à elle, bouton d'action : on passe
  dans le hub.
- **Le hub** : un numéro de sous-zone virtuel, dont le contenu est celui de la carte 82 lu dans les
  données du joueur, **sans ses meubles** — seuls la porte et le coffre de stockage restent posés.
  La carte 82 elle-même n'est pas modifiée (la boutique LP-008 y reste).
- **Le retour** : la porte du hub ramène à la réception **d'où l'on vient** (même palier), le groupe
  replacé devant notre porte — et non au couloir de l'étage du palier 80, où le script d'origine de
  la 82 mènerait.
- **La sauvegarde** : une partie enregistrée dans le hub se recharge dans la réception d'origine,
  pour qu'aucune sauvegarde ne désigne une sous-zone que le jeu sans le tweak ignorerait.
- **Des coutures dans le legacy**, une ligne chacune, marquées `// Couture legacy ++ (LP-009)` :
  leur nombre et leurs sites exacts sont fixés par l'étape 1 (lecture du répertoire de sous-zone,
  placement des décors, transfert de zone, placement du groupe, sauvegarde).

**Hors de ce change** : PNJ, boutique dans le hub, toute autre fonction du hub, géométrie inédite.

## Capabilities

### New Capabilities

- `gameplay-inn-hub` : le hub de l'auberge — la porte dans la réception, la pièce vide, l'aller et le
  retour, la sauvegarde qui n'y reste pas.

### Modified Capabilities

Aucune. `gameplay-tweaks` fixe le contrat de `LegacyPlus.ini` (lu, jamais écrit ; défauts = 1998 ;
coutures) sans énumérer les clés : la nouvelle clé s'y conforme sans le modifier.

## Impact

- **Legacy** (`src/Blaze/`) : quelques lignes de couture, aucune condition d'option — probablement
  `Screens/State03/WorldMap.cs` (`LoadWorldMapSubArea`, `PlaceWorldMapParty`), le placement des
  décors, `WorldMapScript.cs` (`ScriptOpTransferArea`) et le chemin de sauvegarde. Sites confirmés à
  l'étape 1.
- **LegacyPlus** : `Coutures.cs`, `ReglagesLegacyPlus.cs` (la clé), un nouveau fichier
  `src/LegacyPlus/HubAuberge.cs` (nom à valider par Vincent).
- **Installeur** : le modèle `Reglages/LegacyPlus.ini` gagne la clé, commentée.
- **Documentation** : `Documentation/LegacyPlus/legacy.md` (LP-009), `Reference/LegacyPlus.ini.md`.
- **Données** : aucune donnée du jeu n'est ajoutée au dépôt ; le hub est lu dans les fichiers du
  joueur.
