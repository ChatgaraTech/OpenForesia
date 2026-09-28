## Why

Le hub de l'auberge (LP-009) existe mais il est vide. Vincent veut y installer quatre marchands,
chacun spécialisé, pour remplacer la boutique de l'armoire (LP-008) : celle-ci ne vend que de
l'équipement, et elle vit en carte 82, qui n'est la chambre « coffre » que du **dernier** palier de
l'histoire — la boutique n'est donc accessible qu'en fin de partie. Le hub, lui, s'ouvre depuis
toutes les réceptions (legacy ++, LP-010).

## What Changes

- **LP-010 — les marchands du hub** : quatre PNJ debout dans le hub (sous-zone 86), en animation de
  repos, le long du mur ouest (x = −1301, cap 1024), chacun sous le modèle d'une **classe jouable** :
  - le **Lutin** (identité 7) vend les objets **utilisables** (ni arme, ni armure, ni bouclier, ni
    accessoire) — z = 1060 ;
  - le **Voleur** (identité 2) vend les **accessoires** — z = 1210 ;
  - le **Kobold** (identité 6) vend les **armures et les boucliers** — z = 1360 ;
  - la **Sylphide** (identité 13, l'Elfe au féminin) vend les **armes** — z = 1510.
- Face à un marchand, bouton d'action : l'écran de boutique de LP-008 s'ouvre sur **son** catalogue.
  L'écran, l'achat et la remise de l'objet sont repris tels quels.
- **Quatre fichiers de réglages**, un par boutique, dans un dossier dédié `LegacyPlus\` à côté de
  l'exécutable : activation et catalogue (nom = prix). La catégorie d'un objet est lue dans sa fiche
  (`+0x12`, octet haut : arme, armure, bouclier, accessoire), le fichier ne fait que la liste.
- **Un catalogue d'objets utilisables** (potions, aiguilles, cristaux…), avec des prix proposés par
  le générateur et retouchés par Vincent.
- **Étape 1 — la mesure**, avant d'écrire : la carte de la VRAM dans le hub (où poser les textures de
  quatre personnages), la taille des paquets de modèle et d'animation des classes, et l'indice de
  l'animation de repos.
- **BREAKING (réglages)** : la boutique de l'armoire (LP-008) est **retirée** ; `Boutique.ini` n'est
  plus lu ni déposé par l'installeur.

## Capabilities

### New Capabilities

- `gameplay-hub-merchants` : les quatre marchands du hub — leur présence, leur apparence, leur
  catalogue, leur ouverture.

### Modified Capabilities

- `gameplay-inn-shop` : la boutique de l'armoire disparaît ; l'écran et l'achat deviennent ceux des
  marchands du hub, et les réglages passent de `Boutique.ini` aux quatre fichiers du dossier
  `LegacyPlus\`.
- `game-data-install` : l'installeur dépose les quatre fichiers de boutique dans `LegacyPlus\` (sans
  écraser ceux du joueur) au lieu de `Boutique.ini`.

## Impact

- **Legacy** (`src/Blaze/`) : aucune couture nouvelle attendue — les PNJ sont dessinés par la couture
  `DessinSurImpression` et ouverts par `PasDuMembre`, toutes deux existantes. À confirmer à l'étape 1.
- **LegacyPlus** : `Boutique.cs` (l'armoire) retiré ; `BoutiqueEcran.cs`, `BoutiqueAchat.cs`,
  `CatalogueObjets.cs` repris ; `ReglagesBoutique.cs` lit quatre fichiers ; un fichier nouveau pour
  les marchands (nom proposé à Vincent).
- **Outils** : `generer-boutique.mjs` produit les quatre fichiers, objets utilisables compris.
- **Installeur** : `Reglages/` porte les quatre modèles, déposés dans `LegacyPlus\`.
- **Documentation** : `legacy.md` (LP-008 retiré, LP-010), références des réglages, `HISTORY.md`.
