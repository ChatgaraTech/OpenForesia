# Les exporteurs — cartes, modèles, scénario

**État au 2026-11-20 : l'exporteur de cartes est fait** (verbe `exporter-carte`, chantier
`openspec/changes/add-map-exporter`). Il reste les modèles articulés et le scénario. Ce document
fixe le but et le format de sortie, et consigne ce qui est établi sur les données.

## Le but

Donner à une équipe Unreal des **gabarits**, pas des assets finis : tous les modèles seront refaits.

- **Les cartes** : la géométrie de chaque sous-zone, ses sols et ses murs, et la position de tout ce
  qui y vit (monstres, coffres, portes, déclencheurs, entrées). Elles servent à bloquer le level
  design à la bonne échelle. **Fait.**
- **Les modèles** : personnages, monstres, objets de décor, avec leur squelette de parties et leurs
  animations, comme référence de proportions et de gestes.
- **Le scénario** (plus tard) : les dialogues, l'enchaînement des scènes et ce qui débloque quoi.

## Le format de sortie

- **glTF 2.0** (`.gltf` + `.bin`, textures en `.png`) pour la géométrie et les modèles. Unreal
  l'importe nativement (Interchange), Blender aussi, et il porte hiérarchie, matériaux et animations.
- **JSON** pour les placements et le scénario. Il se charge en *Data Table* ou par un petit script
  d'import.
- Un dossier par zone, un sous-dossier par sous-zone. **La sortie se fait sur la machine de qui
  possède le jeu, hors du dépôt** : ce sont des données du jeu.
- **L'échelle** : le jeu compte en unités entières (un sol de forêt est à 140, une cellule de grille
  fait 512 unités). L'export pose **1 unité = 1 cm** (0,01 m en glTF). C'est un choix, pas une
  mesure : le facteur est écrit dans chaque `placements.json`, pour le corriger d'un coup quand on
  aura mesuré un personnage.

## L'exporteur de cartes

```powershell
dotnet run --project OpenForesia.Tools -- exporter-carte [dossier-du-jeu] [zone [sous-zone]] --sortie <dossier hors du dépôt>
```

- Sans zone, il exporte tout le jeu : 160 sous-zones, environ 450 Mo, trois minutes.
- Sans sous-zone, il exporte toute la zone.
- Sans dossier du jeu, il prend `OpenForesia\bin\Debug\net10.0`.
- Il **refuse un dossier de sortie situé dans le dépôt**.

Il écrit un dossier par zone, nommé comme le jeu (`00 - L'auberge`, `03 - La forêt`…), puis un
sous-dossier par sous-zone : son numéro, et son nom de lieu quand le jeu en donne un
(`00 - La forêt aux ruines`). Un `resume.txt` à la racine donne, pour chaque sous-zone, les triangles,
décors, faces de collision, textures et repères écrits, et signale toute face illisible. Pour
l'export complet, ce compte est de zéro.

| fichier | contenu |
|---|---|
| `carte.gltf` + `carte.bin` | Trois nœuds. **`affichage`** : le terrain texturé, un primitif par texture. **`collision`** : les faces de collision, un primitif par couple (sol, genre), avec les deux valeurs en `extras` ; vert pour les sols, rouge pour le reste. **`decor`** : un nœud par modèle de décor placé, partageant un maillage par modèle. Tous les matériaux sont `doubleSided`. Un texel transparent passe en `MASK`, une face semi-transparente en `BLEND` à 50 %. |
| `textures/tNN.png` | Une page de 256 × 256 par couple page + palette, `0x0000` transparent. |
| `placements.json` | Les repères, en coordonnées glTF (`position`) et en coordonnées du jeu (`positionJeu`). **Formations** : espèce et son nom, formation, rang, orientation. **Décor actif** : section, rang, sous-type, gestionnaire, forme. **Coffres** : numéro et contenu avec ses chances. **Entrées.** |
| `vue-du-dessus.png` | La sous-zone entière vue d'en haut, texturée, orthographique, sans miroir : x du jeu vers la droite, z du jeu vers le haut. Jusqu'à 4 096 pixels de côté. |
| `vue-du-dessus-reperes.png` + `legende.txt` | La même vue, assombrie, avec les repères : formations en rouge (`M`), coffres en jaune (`C`), entrées en cyan (`E`), décor actif en points blancs. La légende nomme chaque code. |
| `visite.html` | Une page autonome : la carte en 3D (WebGL), qu'on fait tourner et parcourir. On y affiche ou masque les collisions et chaque genre de repère, et un repère se nomme au survol. Une vue s'enregistre dans l'adresse (`#angle=lacet,tangage,distance`). |

Le code est dans `OpenForesia.Tools/ExportCarte/`. Il ne modifie pas `src/Blaze/` : chaque lecteur
recopie une formule du jeu et porte un commentaire `Miroir de :` qui la nomme.

- Les **coffres** et les **gestionnaires** de décor sont lus dans `coffres.csv` et `decor.csv` du
  bestiaire. Les identifier demande `Blaze.exe`, que l'exporteur n'exige pas.
- Ce que le jeu ne calcule qu'en jouant n'est pas rendu :
  - l'**éclairage** des sommets (les normales de `+0x24`) ;
  - le **brouillard** ;
  - les **palettes animées** ;
  - ce que les gestionnaires de décor changent en jeu (une porte ouverte, un modèle qui change) ;
  - la **visibilité par caméra** (`+0x80`) : tout morceau est exporté.
- Les sections de décor (`+0x30`) sont toutes exportées ensemble. Si certaines sont des états
  alternatifs du même lieu, leurs modèles se superposent.

## Ce qui est établi

Tout ce qui suit vient du code reconstruit ; les références sont les fonctions C#.

### La chaîne de chargement

L'outil `bestiaire` la rejoue hors jeu (`OpenForesia.Tools/Bestiaire.cs`) : `SYSTEM.INI` →
répertoire de l'état 3 (`+0x48`) → zone (`+0x48`) → sous-zone (`+0x48`). Chaque blob de `BLAZE.ALL`
est adressé au secteur et ses pointeurs sont remis en place (`Blob.Fixup`).

### Les morceaux d'une sous-zone

`LoadWorldMapSubArea` charge quatre listes de morceaux (`+0x14..+0x20`, des entrées de 8 octets :
l'entrée d'archive, puis l'attribut qui devient le champ `+0x3c` du morceau). Ensuite, il ajoute à
la même table **les modèles du dernier paquet `+0x24`**.

| liste | rôle | lu par |
|---|---|---|
| 0 | **la collision** | `TryPlaceEntity` (`FUN_005330d0`), sur le morceau 0 |
| 1 | terrain, balayé par cellules, trié face par face | `FUN_0051fa40` |
| 2 | terrain, balayé par cellules | `FUN_00521000` |
| 3 | faces à la file, sans grille | `FUN_00522140` |
| paquet `+0x24` | modèles de décor | `DrawStaticModel` (`FUN_0051c420`), `FUN_0051e700` |

**Les morceaux d'affichage sont dessinés en placement identité** : leurs sommets sont déjà des
coordonnées du monde. L'en-tête commun, dont les pointeurs sont relatifs au début du morceau :

| champ | contenu |
|---|---|
| `+0x04` | (collision) nombre de faces |
| `+0x08` | nombre de sommets (mot bas) et de normales (mot haut) ; le chargement les divise par 3 en mémoire |
| `+0x10..+0x1e` | huit compteurs de faces : quatre seaux sautés, puis triangles plats, quads plats, triangles dégradés, quads dégradés |
| `+0x24` | les normales (éclairage) ; pour la collision, les matières |
| `+0x28` | les faces |
| `+0x34` | une **grille de 32 × 32 cellules** : par cellule, quatre comptes sautés, quatre comptes dessinés, puis les indices de face |
| `+0x3c` | les attributs : page de texture (octet bas), décalage de page (octet 1), emplacement de la table de décodage (octet 2) |
| `+0x40` | les sommets, 8 octets (`x`, `y`, `z`, bourrage) ; **y vers le bas** |

- **Face d'affichage, 0x20 octets.**
  - `mot0 = i0 | i1 << 16`, `mot1 = i2 | i3 << 16` : les indices de sommet.
  - Mots 2 et 3 : les couleurs (éclairage).
  - `mot4` : UV de v0 (bas) et de v1 (haut) ; `mot5` : UV de v2 et de v3. Dans chaque UV, `u` est
    l'octet bas.
  - `mot7` : les attributs. L'octet haut est le code GPU : bit 0x04 texturé, 0x02 semi-transparent.
  - Le quad part au GPU dans l'ordre v1, v0, v2, v3.
  - Avec la grille, seuls les indices listés par les cellules sont dessinés (une face peut y figurer
    deux fois). Sans grille, on saute les quatre premiers seaux.
- **Face de collision, 16 octets.**
  - Indices `i0 = mot0 bas`, `i2 = mot0 haut`, `i1 = mot1 bas`, `i3 = mot1 haut` (le quatrième si
    l'attribut porte le bit 0). Le polygone se parcourt dans l'ordre i0, i2, i1, i3.
  - La matière est en `+8` : c'est un **sol** si le short `matières + 2 + mat × 8` est négatif.
  - Le **genre** est en `+0xe`.
  - Le verbe `terrain` lit à tort les quatre listes dans ce format ; seule la liste 0 en est.
- **Face de modèle de décor, 16 octets.**
  - `rec0` : quatre indices sur un octet.
  - `rec2` : UV de v0 et v1 ; `rec3` : UV de v2 et v3.
  - Pour un modèle du paquet `+0x24`, `AdjustModelTexturePages` a déjà ajouté
    `(attr >> 8) & 0xc0` au `v` de chaque UV.

### Le placement du décor

Un enregistrement de décor fait 0x20 octets (liste `+0x30`, une liste par section ; lu par
`SpawnSubAreaEntities`) :

| champ | contenu |
|---|---|
| octet 0 & 3 | la forme : 0 particule, 1 modèle, 2 modèle trié par face |
| `+4`, `+6`, `+8` | position x, y, z, **avec y vers le haut**. Le moteur en prend l'opposé dans l'espace des sommets (`TryPlaceEntity`, `LoadPointAsMatrixTranslation`) |
| `+0xa`, `+0xc`, `+0xe` | angles x, y, z, sur 4 096 par tour (`RotMatrix`) |
| `+0x1c` | l'indice du modèle dans le paquet |
| `+0x1d` | le sous-type (le gestionnaire, via la table de la sous-zone) |
| `+0x1e` | un décalage de palette |

Un sommet du monde vaut `R(angles) · v + (x, −y, z)`. Les formations (`+0x34`) ont la même
disposition de position et d'orientation.

### Les textures

Ce sont des **TIM**, listées en `+0x10`. `DecodeResourceChunk` (`FUN_005322f0`) **ignore les
positions de l'en-tête** :

- l'image va en `x = (méta0 & 0xf) × 64`, `y = ((méta0 & 0x10) + méta2 × 4) × 16`, sauf si
  `méta0 = 0x1c` ;
- la palette va en `x = 0`, `y = méta1 + 0x1c0`, rangée sur 256 mots de large ;
- l'emplacement i de la table `0x005e8f00` reçoit le mot de page (à la façon de `GetTPage`) et le
  mot de palette.

Une face résout sa page et sa palette ainsi, avec `slot = table[(attr >> 16) & 0xff]` :

- **face d'affichage** :
  - palette = `((attr_face & 0xfcf0) × 0x1000 + (slot & 0xffff0000)) >> 16` ;
  - page = `((attr_face & 0xef0000) + (((slot | 0x800) & 0xffff) << 16)) >> 16` ;
- **modèle de décor** :
  - page = `slot bas | (attr & 0xff) << 5` ;
  - palette = `slot haut` + le décalage du mot `+0x1e` ;
  - semi-transparence par le bit 0x200 de l'attribut.

Sur les 160 sous-zones exportées, **aucun texel n'est lu hors des images que la sous-zone charge** :
les textures d'une carte lui appartiennent entièrement.

### Les personnages

Le verbe `classes` liste, pour les seize identités (classe × sexe), le paquet de modèle (en
**parties**), le paquet d'animation et les deux textures, depuis le répertoire de l'état 1.

### Les modèles animés — `exporter-modeles`

```powershell
dotnet run --project OpenForesia.Tools -- exporter-modeles [dossier] --sortie <hors du depot>
```

Tous les modèles animés du jeu : les seize héros (répertoire de l'état 1) puis, sous-zone par
sous-zone, les paquets `+0x2c` avec leurs animations `+0x7c` (même rang), nommés par les espèces
`+0x4c` qui les emploient (octet 0 = le modèle, octets 2-3 = le décalage de palette, octet 4 = la
fiche). Un modèle identique (mêmes octets, mêmes animations, même texture) n'est écrit qu'une fois ;
les variantes de palette (« Goblin (2) ») sont des modèles distincts. Les gens de l'auberge n'ont
pas de nom dans leur fiche (« £ ») : ils sortent en « Personnage N ». Code dans
`OpenForesia.Tools/ExportModeles/`, chaque lecteur marqué `Miroir de :`.

Sortie : un dossier par zone (et `Héros/`), un sous-dossier par modèle avec `modele.gltf`,
`modele.bin`, `texture.png` et `donnees.js`, plus `index.html` (la galerie : liste, recherche,
visionneuse qui rejoue les animations comme le jeu les calcule) et `resume.txt`.

**Le format établi** (Miroir de : `RelocateAnimationBundle`, `DrawAnimatedModel`) — une animation
porte son propre squelette : `+0x12` os, `+0x14` parties, `+0x16` images ; huit pointeurs en
`+0x20`, relatifs à `entrée + entrée[0]` ; `+0x20` les poses (0x10 octets : trois angles sur 4 096,
une translation, une échelle uniforme où 0x1000 = 1), `+0x24` le parent de chaque os puis l'os de
chaque partie, `+0x28` l'indice de pose de chaque os, image par image. `W[0] = L[0]`,
`W[i] = W[parent] · L[i]`. **Le nombre d'os change d'une animation à l'autre chez un modèle sur
deux** (156 sur 299) : le glTF anime donc directement les parties (un nœud par partie, la
transformation monde de son os), pas un squelette commun. La texture se calcule une seule fois,
sur la première partie, et sert à toutes.

**Ce qui n'est pas établi** : le rythme — l'export suppose **30 images par seconde** ; les
animations n'ont pas de nom (elles sont numérotées) ; certaines faces lisent une zone de la page
qui n'est pas leur image (bruit multicolore, par exemple sur les oreilles du Goblin) — la règle de
texture du jeu n'est donc pas entièrement reproduite ; la « Barricade » de la forêt n'a aucun quad
(le jeu ne dessine que les quads d'un modèle animé).

## Ce qui reste à établir

1. Le rythme des animations, et la règle de texture des faces qui lisent hors de leur image.
2. Le facteur d'échelle vers Unreal, sur une mesure connue.
3. Le sens des **sections** de décor (`+0x30`) : des états du scénario, ou des groupes chargés
   ensemble ?
4. L'éclairage des sommets, si l'on veut un rendu plus proche du jeu.

## L'ordre de travail proposé

1. ~~Cartes texturées, placements, toutes les sous-zones.~~ Fait (`exporter-carte`).
2. **Modèles fixes**, puis **modèles articulés et animations**.
3. **Le scénario** : les textes et les scènes (les 32 verbes des cinématiques sont transcrits dans
   `Screens/State03/Cutscene/`).

Chaque étape est assez grosse pour un chantier OpenSpec à elle seule.
