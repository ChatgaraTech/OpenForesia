## Context

Ce que la lecture du code a établi (exploration du 2026-09-28) :

- **Le numéro d'une carte de l'auberge = palier de l'histoire + pièce.** `FUN_004747F0`
  (`WorldMap.cs`, ~l. 2116) choisit la réception parmi 0x00, 0x08 … 0x50 selon les drapeaux du groupe
  (`0x005e9001`, les neuf capacités, le bit 7 de `0x005e9006`) ; la pièce s'ajoute (+1 chambre de
  l'expert, +2 le coffre, +3 3e chambre, +4 aubergiste, +5 salon). **80 = réception du dernier
  palier**, 82 = son « coffre ». La boutique LP-008, codée sur 82, n'existe donc qu'en fin de partie
  (à confirmer, hors de ce change).
- **La carte 80 est toute la coque**, en trois sections (`0x005de038`) : la salle (liste 0 de
  `decor.csv`), le couloir de l'étage avec ses quatre portes (liste 1, sous-type 2,
  `EntityHandler_004736D0`), le salon (liste 2). Les chambres 81 à 84 sont des sous-zones à part, dans
  le **même repère** de coordonnées.
- **Deux façons de changer de lieu** : une face de sol marquée change de *section* sans rechargement
  (`PartyMemberStep.cs` ~1454, pas = 1) ; un volume de déclenchement lance un script dont l'opcode 17
  (`ScriptOpTransferArea`) pose zone, sous-zone, point d'entrée et le pas 2 : rechargement
  (`LoadWorldMapSubArea`) puis groupe replacé au point d'entrée (`PlaceWorldMapParty`).
- **Le répertoire d'une sous-zone** est `((int*)(state + 0x48))[sous-zone]` (`WorldMap.cs:758`) ;
  la zone 0 installe **toujours** les tables de gestionnaires d'index 0 (`Resources.cs:137`) : un hub
  en zone 0 en hérite sans rien faire. Le banc de sons de la zone 0 ne dépend pas de la sous-zone
  (`Q00_00.snd`).
- **La carte 82**, entrée par entrée : 0 la porte (sous-type 2) et 1 son échelle (15) en (−556, 400,
  1000) ; 15 le corps du coffre (`0x004734B0`) et 16 son couvercle scripté (`0x00450760`) en
  (−1400, 4x0, 1100) ; 7 l'armoire ; le reste, meubles ordinaires et deux effets (11 scintille, 12
  palpite).
- **Les précédents** : l'armoire de LP-008 est détectée par la couture `PasDuMembre` (membre debout,
  distance, cap, bouton d'action consommé). Le hub reprend cette forme pour la porte d'entrée.

## Goals / Non-Goals

**Goals :** une porte dans toutes les réceptions ; une pièce « 82 vide » sous un numéro à nous ;
l'aller, le retour vers la réception d'origine ; une sauvegarde qui ne désigne jamais le hub ; tout
derrière `[Auberge] Hub`, défaut `non`.

**Non-Goals :** PNJ, boutique ou toute fonction dans le hub ; géométrie inédite ; déplacer LP-008 ;
corriger l'accès à LP-008 avant le dernier palier.

## Decisions

### D1. Étape 1 d'abord : mesurer, puis écrire

Cinq inconnues conditionnent les sites des coutures ; elles sont levées et consignées dans
`legacy.md` (LP-009) avant d'écrire le tweak :

1. **Collisions** : les meubles de la 82 sont-ils seulement des entités, ou aussi dans le maillage de
   sol / de collision de la pièce ? (lecture des faces de sol `+0x7c` de la sous-zone, ou essai en jeu
   avec les décors filtrés). *Si les meubles sont dans le maillage*, on le dit et Vincent choisit :
   garder ces obstacles, ou prendre une autre pièce.
2. **Numéro du hub** : la longueur de la table `state + 0x48` de la zone 0. Un numéro libre
   (86, 87 ou au-delà) si la table en a ; sinon, l'interception de la lecture suffit — le numéro
   n'est alors lu que par nous.
3. **Sauvegarde** : ce que `FormatSauvegarde.md` et le chemin d'écriture retiennent de la zone, de la
   sous-zone et du point d'entrée.
4. **La porte dans la salle** : les modèles d'une sous-zone sont-ils visibles de toutes ses sections ?
   Le modèle de porte (sous-type 2) est chargé en carte 80 pour le couloir ; il doit pouvoir être posé
   en section 0.
5. **Le mur** : relevé par Vincent avec `[Diagnostic] TracePosition`, dans la réception du palier 80 ;
   vérifié ensuite dans une réception d'un palier antérieur (même coque attendue).

**Résultat de la mesure (2026-09-28, détail dans `legacy.md`, LP-009)** : table de 105
sous-zones, **86 retenu** (emplacement vide existant) ; pièce « coffre » identique à tous les
paliers ; aucun lecteur de la sous-zone en zone 0 hors LP-008, sauvegarde et repères ; sortie de la
82 par l'opcode 17 seulement ; modèles par sous-zone, donc porte possible en section 0 ;
**collisions non tranchées** par la lecture → marche en jeu dès le filtre écrit.

### D2. Le hub : le numéro 86, qui se charge comme la 82

Une couture sur la lecture de l'entrée de répertoire dans `LoadWorldMapSubArea` :
`subEntry = Coutures.EntreeSousZone(zone, sousZone, subEntry)` — identité par défaut ; le tweak rend
l'entrée de la **82** quand la sous-zone vaut le numéro du hub. Tout le reste du chargement
(textures, morceaux, modèles, scripts, volumes) est celui de la 82, lu dans les données du joueur.
*Écarté* : copier ou modifier les données — interdit (les données ne quittent pas le jeu du joueur),
et inutile.

Le recensement est fait : en zone 0, rien d'autre que LP-008 (`== 82`) et la vidange de manette de
`RunWorldMapFrame` (`& 7`, sans effet pour 6) ne lit le numéro. Le hub vaut « une pièce qui n'est
pas la 82 » : LP-008 ne s'y ouvre pas.

### D3. Vider la pièce : un filtre au placement des décors

Une couture sur le placement de chaque entrée de liste de décors : dans le hub, seules les entrées
0, 1 (la porte), 15 et 16 (le coffre) naissent. Les index sont ceux de `decor.csv` ; ils sont
revérifiés à l'étape 1 contre les données de la 82 du palier retenu.

### D4. La porte d'entrée : une entité posée par nous, déclenchée comme l'armoire

Dans chaque réception (`zone 0, sous-zone & 7 == 0`), le tweak fait naître un décor de sous-type 2
(le modèle de porte de l'auberge) au mur relevé, à l'arrivée dans la section 0. L'enregistrement de
placement est **celui de la porte de la 82 dans la liste 1 de la réception** (même `kind`, même
modèle), position changée : modèles et maillages sont chargés par sous-zone, il est valable en
section 0. Il naît par une couture juste après `SpawnSubAreaEntities` (ses deux sites :
`PlaceWorldMapParty` et l'opcode de script qui repeuple), dans les emplacements de `0x005fa5e0`. La détection reprend
la forme de LP-008 (couture `PasDuMembre`) : membre debout, à moins de ~120 unités, face à la porte à
45° près, bouton d'action consommé, aucune boîte ouverte. Le passage reproduit l'effet de l'opcode 17 :
sous-zone = hub, point d'entrée = celui de la porte de la 82, pas = 2 ; le tweak mémorise la
réception d'origine.

*Repli si D1.4 échoue* (la porte ne s'affiche pas en section 0) : pas de modèle, un emplacement de mur
sans décor, signalé par une boîte de texte à l'approche — à décider avec Vincent.

### D5. Le retour : rediriger le transfert qui sort du hub

La sortie d'origine de la 82 (volume + script, opcode 17 — le seul chemin, établi par le
recensement des écrivains de `DAT_005de52c`) mène au couloir de l'étage. Une couture sur
`ScriptOpTransferArea` :
quand la sous-zone courante est le hub, la destination devient la **réception d'origine**. Le groupe
est ensuite replacé **devant notre porte** par une couture sur `PlaceWorldMapParty` (position et cap
imposés après le placement au point d'entrée), puisque aucun point d'entrée des données n'y mène.

### D6. La sauvegarde ne voit jamais le hub

La reprise est `+0x1f01..+0x1f03` = zone, `DAT_00566c88` (sous-zone chargée), `DAT_005de044`
(point de reprise, posé par les volumes de type 8). **Deux écrivains** : le menu
(`CopyGlobalBanksIntoSaveBlock`) et **le coffre de stockage** (`WorldMapDepot.cs`), qui sauvegarde
à chaque usage — donc dès qu'on s'en sert dans le hub.

Au départ de la réception, le tweak retient `00566c88` et `005de044`. Une couture
`LieuDeReprise(zone, sousZone, point)` aux deux écrivains rend, quand la sous-zone chargée est le
hub, la réception et le point retenus. Même couture pour `CurrentMarkerTag` (les repères de la page
4 du menu), qui porte aussi `00566c88`. Une sauvegarde faite dans le hub se recharge dans la
réception, tweak actif ou non.

### D7. Un fichier, une clé

`src/LegacyPlus/HubAuberge.cs` (nom à valider par Vincent), installé depuis le constructeur statique
de `Coutures` comme les autres tweaks ; clé `[Auberge] Hub` lue par `ReglagesLegacyPlus`. Aucun
nombre aléatoire.

## Risks / Trade-offs

- **[Meubles dans le maillage]** → obstacles invisibles dans le hub. Mitigation : mesuré en D1.1 avant
  d'écrire ; décision de Vincent si c'est le cas.
- **[Scripts de la 82 qui lisent la sous-zone]** : un script ou un gestionnaire de la 82 peut tester
  son propre numéro et se comporter autrement sous le numéro du hub. Mitigation : recensement en
  D2 ; au pire, le hub se déclare 82 auprès de ces lecteurs.
- **[Coffre de stockage]** : il dépend peut-être d'un drapeau de palier ou de pièce. Mitigation :
  essai en jeu dès la première version.
- **[Palier ↔ hub]** : le hub charge la 82 du palier 80 quelle que soit la réception d'origine ;
  si les données de la 82 dépendent du palier (autre version de la pièce), on le constate à l'étape
  1 et on choisit un palier de référence.
- **[Sauvegarde]** : un format mal lu rendrait une sauvegarde illisible. Mitigation : D1.3 d'abord,
  essai de la sauvegarde avec et sans tweak, sur copie.
- **[Le coffre sauvegarde]** : le coffre de stockage écrit la sauvegarde à chaque usage ; sans la
  couture D6 active dès le premier essai, une partie reprendrait en sous-zone 86. Mitigation : D6
  livrée avec le hub, pas après ; essai sur copie de sauvegarde.
- **[Budget de 2 500 primitives]** : une pièce vide en consomme moins que la 82 ; sans risque.
