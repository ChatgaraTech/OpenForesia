## Purpose

Charger les ressources de l'écran de sélection des personnages — l'état 4 du répartiteur de jeu —
en reproduisant fidèlement `FUN_005066e0` : les quatre listes du répertoire de ressources, et la
relocalisation des paquets de modèles et d'animations vers les deux tables globales que le rendu
consulte ensuite.

## ADDED Requirements

### Requirement: Entrée dans l'état

Le système SHALL, à l'entrée de l'état, remettre à zéro les neuf entiers contigus de son bloc d'état
(`DAT_00578de8`…`DAT_00578e08`) et mettre à 1 les trois drapeaux `DAT_00562a48`…`DAT_00562a50`.

Le système SHALL ensuite charger l'entrée de ressource principale et relocaliser son en-tête, comme
le fait déjà le chargement du menu.

#### Scenario: Premier passage

- **WHEN** l'état 4 est chargé
- **THEN** les neuf entiers du bloc d'état valent 0
- **AND** les trois drapeaux valent 1
- **AND** le répertoire de ressources est relocalisé

### Requirement: Chargement des quatre listes du répertoire

Le système SHALL parcourir quatre listes du répertoire, chacune terminée par une entrée nulle, et
les traiter selon leur nature :

| Liste | Traitement | Destination |
|---|---|---|
| `+0x10` | décodage TIM vers la VRAM émulée | table de pages `DAT_005e8f00`, un slot de 4 octets par entrée |
| `+0x2c` | relocalisation de paquet de **modèles** puis repagination des textures | table de modèles `DAT_005e96a0` |
| `+0x7c` | relocalisation de paquet d'**animations** | table d'animations `DAT_005e9640` |
| `+0xb0` | aucun — copie brute | `MainMem + 0xf8000`, un bloc tous les `0x2000` octets |

Les listes `+0x10` et `+0x2c` SHALL avancer de deux mots par entrée ; les listes `+0x7c` et `+0xb0`
SHALL avancer d'un seul mot.

#### Scenario: Liste vide

- **WHEN** la première entrée d'une liste est nulle
- **THEN** cette liste n'est pas parcourue
- **AND** le curseur de chargement n'est pas déplacé

### Requirement: Empilement contigu des paquets

Le système SHALL charger les paquets de modèles et d'animations à la suite les uns des autres à
partir de `MainMem + 0x138000`, chaque chargement rendant la fin du bloc écrit, qui devient la
destination du suivant.

Le curseur SHALL être partagé entre la liste `+0x2c` et la liste `+0x7c` : les animations
s'empilent derrière les modèles, sans trou.

#### Scenario: Deux listes non vides

- **WHEN** la liste `+0x2c` charge N paquets puis la liste `+0x7c` en charge M
- **THEN** les M paquets d'animations commencent à la fin du dernier paquet de modèles

#### Scenario: Liste de modèles vide, liste d'animations non vide

- **WHEN** la liste `+0x2c` est vide
- **THEN** le premier paquet d'animations est chargé à `MainMem + 0x138000`

### Requirement: Relocalisation d'un paquet

Un paquet SHALL être lu comme un compte suivi d'autant de décalages relatifs. Le système SHALL
convertir chaque décalage en adresse absolue et SHALL publier dans la table de destination un
pointeur sur le **tableau de décalages**, et non sur le compte.

Le compte SHALL rester lisible à `[-4]` du pointeur publié, puisque c'est ainsi que le code de rendu
le consulte.

#### Scenario: Publication dans la table

- **WHEN** un paquet de N entrées est relocalisé
- **THEN** la table contient un pointeur `p` sur le tableau de décalages
- **AND** `p[-1]` vaut N

### Requirement: Relocalisation d'une entrée de modèle

Pour chaque entrée d'un paquet de modèles, le système SHALL :

- reporter dans l'entrée les bits de page pris à `+4` du descripteur de la liste, après les avoir
  fusionnés par OU dans ce descripteur ;
- relocaliser les trois pointeurs simples de l'entrée ;
- relocaliser la table dont la longueur est le premier champ de l'entrée, puis la table de longueur
  fixe `0x400`, puis la liste terminée par `-1` dont il SHALL ranger la longueur mesurée dans
  l'entrée ;
- diviser par trois les deux champs de comptage de sommets.

La division par trois SHALL être une division entière ordinaire : les champs sont non signés, donc
l'idiome signé de l'original s'y réduit exactement.

#### Scenario: Champ de liste absent

- **WHEN** l'un des pointeurs relocalisables de l'entrée est nul
- **THEN** il est laissé nul
- **AND** la longueur rangée pour la liste terminée par `-1` vaut 0

### Requirement: Repagination des textures d'un modèle

Le système SHALL ajouter le décalage de page — les bits `0xc0` du champ `+0x3c` décalé de 8 — à deux
octets de chaque enregistrement de 16 octets du modèle.

Le nombre d'enregistrements à traiter SHALL être la somme des quatre compteurs `+0x18`…`+0x1e`, et
les enregistrements à sauter la somme des quatre compteurs `+0x10`…`+0x16`.

#### Scenario: Modèle sans primitive texturée

- **WHEN** la somme des quatre compteurs `+0x18`…`+0x1e` vaut 0
- **THEN** aucun enregistrement n'est modifié

### Requirement: Portée des deux tables globales

Les tables de modèles et d'animations SHALL être nommées et documentées comme **globales**, et non
comme propres à l'écran de sélection : elles sont remplies de la même manière par d'autres états et
lues par une trentaine de sites d'accès dans tout le jeu.

Leur capacité SHALL être de 24 entrées, la distance de `0x60` octets qui sépare les deux symboles
dans la section de données.

#### Scenario: Indexation par une autre partie du jeu

- **WHEN** un autre état remplit ces mêmes tables
- **THEN** il utilise les mêmes fonctions de relocalisation
- **AND** aucune donnée propre à la sélection des personnages n'est supposée
