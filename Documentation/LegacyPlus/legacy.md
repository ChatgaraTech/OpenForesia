# Legacy ++ — le registre des tweaks

Les **tweaks de gameplay** changent l'équilibre ou les règles du jeu de 1998, au choix du joueur,
dans `LegacyPlus.ini`. Ils sont **tous désactivés par défaut** : sans le fichier, ou avec toutes les
clés à leur défaut, c'est le jeu de 1998.

Ce sont des numéros `LP-NNN`, distincts des `LB-NNN` de `Reference/legacy-bug.md`, qui corrigent les
défauts du jeu d'origine.

**La règle** : aucune condition d'option dans le code transcrit (`src/Blaze/`). Le legacy n'appelle
que des **coutures** (`src/LegacyPlus/Coutures.cs`), dont le défaut ne change rien. Chaque couture est
marquée sur place `// Couture legacy ++ (LP-NNN)`. Le code d'un tweak vit dans `src/LegacyPlus/`, un
fichier par tweak. Un tweak ne tire jamais de nombre aléatoire.

Les clés et leurs bornes : `Documentation/Reference/LegacyPlus.ini.md`.

## Les tweaks

> Au passage (LP-005) : dans `DespawnSceneEntity_0051a740`, le commentaire « rien que de l'argent »
> désigne en fait **le cristal d'expérience** (`0x80081000`) ; ce sont les « éclats »
> (`0x90081000`) qui sont **les pièces**. Le ramassage (`PartyMemberStep.cs`) fait foi.


| N° | Tweak | Clé (`LegacyPlus.ini`) | Couture | État |
|---|---|---|---|---|
| LP-001 | Dégâts des monstres, multipliés | `[Difficulte] DegatsMonstres` | `DegatsSubis`, entrée d'`ApplyDamage_00539A20` | validé en jeu (2026-09-27) — `src/LegacyPlus/Difficulte.cs` |
| LP-002 | PV des monstres, multipliés | `[Difficulte] PvMonstres` | `PvALaNaissance`, `ResetEntityStats` (`FUN_005389e0`) | validé en jeu (2026-09-27) — `src/LegacyPlus/Difficulte.cs` |
| LP-003 | Montant des sorts du groupe × (1 + coef × MAG / 100) | `[Magie] ScalingMagique` | `MontantDeSort`, ajout du montant dans `ResolveSpellHit_0053A2B0` | validé en jeu (2026-09-27) — `src/LegacyPlus/ScalingMagique.cs` |
| LP-005 | Ramassage automatique des pièces et des cristaux d'expérience (pas les coffres) | `[Butin] RamassageAuto` | `ButinAtteint`, l'entité que la collision désigne dans `PartyMemberStep.cs` | validé en jeu (2026-09-27) — `src/LegacyPlus/RamassageAuto.cs` |
| LP-006 | Se déplacer en lançant un sort, à une part de la vitesse de marche | `[Magie] DeplacementEnIncantation` | `VitesseEnIncantation`, le zéro de vitesse des postures d'incantation dans `ApplyGroundMotion_0051A020` | validé en jeu (2026-09-27) — `src/LegacyPlus/DeplacementEnIncantation.cs` |
| LP-004 | Frames d'invulnérabilité des ennemis tenues par attaquant | `[Combat] InvulnerabiliteEnnemis` | `VictimeExclue` (bit `0x40`), `VictimeProtegee` (`+0x96`), `CoupPorte` dans les deux balayages (`FUN_00519940`, `FUN_00519af0`), `ImageSuivante` en tête du pas de simulation (boucle par image de `RunWorldMapTick`) | validé en jeu (2026-09-27) — `src/LegacyPlus/VerrouParAttaquant.cs` |
| LP-008 | La boutique de l'auberge : l'armoire de la carte 82 vend le catalogue de `Boutique.ini` | `Boutique.ini` : `[Boutique] Actif`, `[Catalogue]` | `PasDuMembre` (l'ouverture), `DessinSurImpression` (l'écran) | **retiré** le 2026-09-28, remplacé par LP-010 — l'écran (`BoutiqueEcran.cs`), l'achat (`BoutiqueAchat.cs`) et l'index (`CatalogueObjets.cs`) servent aux marchands |
| LP-010 | Les marchands du hub : Lutin (objets), Voleur (accessoires), Kobold (armures, boucliers), Sylphide (armes), sous des modèles de classe ; les quatre catalogues filtrés par la classe de l'acheteur | `LegacyPlus\Boutique{Objets,Accessoires,Armures,Armes}.ini` : `[Catalogue]` — présents dès que le hub l'est (`[Boutique] Actif` retiré le 2026-09-28) | `ListeDApparition` (le chargement), `DessinSurImpression` (le dessin, l'écran), `PasDuMembre` (la collision, l'ouverture) | validé en jeu (2026-09-28) — `src/LegacyPlus/MarchandsHub.cs` |
| LP-009 | Le hub de l'auberge : une porte dans chaque réception, vers la carte 82 vidée sous le numéro 86 | `[Auberge] Hub` | `EntreeSousZone` (`LoadWorldMapSubArea`), `ListeDApparition` et `DecorNe` (`SpawnSubAreaEntities`), `PasDuMembre` (la porte), `TransfertScripte` (opcode 17), `PointDEntree` et `EcartDeReprise` (`PlaceWorldMapParty`), `LieuSauve` (menu et coffre) | écrit le 2026-09-28, à tester en jeu — `src/LegacyPlus/HubAuberge.cs`, `openspec/changes/archive/2026-09-28-add-legacy-plus-inn-hub` |
| LP-012 | L'arène du hub : un téléporteur, un menu (mode, palier 1 à 10 : PV et dégâts × palier), la salle du boss de la vieille tour (4/9) vidée pour un combat rejouable ; fin native (cinématique, portail vers le hub, deux coffres), défaite = retour au hub | aucune : suit `[Auberge] Hub` | `PasDuMembre` (la question), `DessinSurImpression` (le menu), `EntreeSousZone`, `PointDEntree`, `DecorNe`, `Rencontre`, `LieuSauve`, `ImageSuivante` (directeur, fin, journal Debug), `PvALaNaissance`, `DegatsSubis`, `ListeDApparition` (les coffres), et trois coutures neuves : `VolumesTus` (`TriggerVolumes.cs`), `DefaiteEvitee` (`WorldMapHud.cs`, le passage à l'état 9) et `ScriptTu` (`RunEntityScript`, aucun script de la sous-zone en arène) | en cours — lot 1 écrit (question, menu ; téléporteur vu en jeu), lots 2 à 4 écrits (l'aller, la salle vidée ; le directeur, le palier ; la fin, la défaite), à tester en jeu ; `openspec/changes/add-legacy-plus-arena` |
| LP-011 | Le rachat : la Voleuse du hub rachète l'inventaire (hors équipement) et le coffre du joueur, à un taux du prix d'achat des quatre boutiques ; un objet absent des catalogues ne se revend pas ; une pile se vend en entier | `LegacyPlus\BoutiqueVente.ini` : `[Vente] Taux` (50) | celles de LP-010 | écrit le 2026-09-28, à tester en jeu — `src/LegacyPlus/BoutiqueVente.cs`, `MarchandsHub.cs` ; une vente depuis le coffre écrit la sauvegarde (emplacement vidé, or crédité, sommes refaites) |
| — | Vitesse des monstres | — | — | **abandonné** par Vincent le 2026-09-27 — les vitesses sont écrites en dur dans chacun des 166 gestionnaires de comportement |
| LP-007 | Rencontres aléatoires : cadence, chevauchement pendant un combat, plusieurs formations | `[Rencontres] Frequence`, `MonstresMax`, `Formations` | `Rencontre` (l'appel de `TrySpawnRandomEncounter`), `RencontreEnAttente` et `RencontreBloquee` (ses deux gardes), `DelaiRencontre` (les deux rechargements du minuteur) | validé en jeu (2026-09-27) — `src/LegacyPlus/Rencontres.cs` |

## LP-008, la boutique de l'auberge (validée en jeu le 2026-09-27)

Demande de Vincent (2026-09-27) : une boutique dans l'auberge — quête 0, carte 82 « Le coffre », à
gauche du coffre, près de l'armoire — qui vend des équipements. Catalogue et prix :
`Boutique.ini` (modèle : `OpenForesia.Installer/Reglages/Boutique.ini`, généré par
`node OpenForesia.Tools/generer-boutique.mjs`, puis retouché à la main).

**Décidé** : le joueur qui ouvre la boutique paie avec sa bourse (`+0x26c`) et reçoit l'objet ;
l'objet est remis comme par un coffre (`GrantChestContents_00530F20`) ; **pas de personnage** — au vu
de l'étape 1, le point de vente est **l'armoire** de la carte 82 : on s'en approche et on appuie sur
le bouton d'action.

**Étape 1, ce qui est établi :**
- **L'or ne sert à rien dans le jeu de 1998** : la bourse se remplit et s'affiche, rien ne la dépense.
- **Aucun prix dans les données** : les fiches d'objets n'en portent pas. Le catalogue les déduit de
  la puissance (ATT `+0x30`, DEF `+0x32`, bonus `+0x20`), de la zone d'apparition et de l'or
  gagné par monstre dans cette zone (`(rang + 9) / 10` par pièce, 1,5 pièce en moyenne).
  272 équipements obtenables sont en vente ; 116 introuvables en jeu sont désactivés.
- **Les modèles 3D sont chargés par carte** : `LoadWorldMapArea` charge le répertoire de la quête,
  puis `LoadWorldMapSubArea` recharge par-dessus, **pour chaque carte**, son bloc propre — espèces
  (`+0x4c`), modèles (`+0x98`), maillages, **textures** décodées dans des emplacements numérotés
  depuis 0. La carte 82 n'a **aucun** personnage ; le PNJ de l'auberge (modèle « ｵﾔ… », 1 PV,
  sous-type 0 : `EntityHandler_00486EE0`, qui lance son script quand un membre le voit) vit dans
  les blocs des cartes **81, 83, 84 et 85**. L'amener en 82 demande de charger le bloc d'une autre
  carte, et de placer ses textures dans des emplacements libres sans écraser celles de la 82.
- **Le numéro d'objet n'est pas global** : chaque carte a son catalogue d'une quarantaine d'objets,
  et `GrantChestContents` reçoit un indice dans **celui de la carte courante**. Vendre n'importe
  quel objet demandera de remettre la **fiche brute** (colonne `octets` d'`objets.csv`), comme
  le fait déjà l'éditeur de sauvegarde.
- **Position** : `[Diagnostic] TracePosition = oui` écrit la position du joueur 1 à chaque arrêt
  d'une seconde (`legacyplus-position.log`). Le coffre de la carte 82 est vers x = −556, z = 1000.
- **L'armoire** (relevé de Vincent, 2026-09-27) : debout devant elle, face à elle, le joueur 1 est
  à x = −1285, y = 400, z = 900, cap 3072 (il regarde vers les x décroissants). Dans cette direction,
  à 87 unités, `decor.csv` place un décor ordinaire (entrée 7 de la carte 82, gestionnaire
  `0x004734b0`) à **(−1372, 400, 900)** : l'armoire. Déclenchement proposé : à moins de ~120 unités
  de ce point, face à lui à 45° près, bouton d'action.
- **À vérifier à l'étape 2** : le relevé, accroché à la couture `ButinAtteint`, n'a écrit aucune ligne
  périodique ; cette partie du pas d'un membre n'est peut-être pas atteinte à chaque image. La
  détection de l'armoire devra s'accrocher à un point atteint à coup sûr.

**Étape 2, écrite (à tester)** — change `openspec/changes/archive/2026-09-27-add-legacy-plus-inn-shop` : une couture
`PasDuMembre` à l'entrée de `TickPartyMember` ; `Boutique.cs` ouvre une boîte du jeu (`BindMenuLabels`,
mise en scène d'un coffre, rappel `RestorePartyReactions_00530ED0`) quand un membre debout, à moins de
120 unités de l'armoire et face à elle à 45° près, appuie sur son bouton d'action ; l'appui est
consommé. Le programme de texte reprend les codes d'`AnnounceItem` (`labels + 0x2a8` / `+ 0x2ac`), et
ses octets étaient écrits dans `legacyplus-boutique.log` pour vérification (fichier supprimé le
2026-09-28 : les marchands et l'écran de boutique écrivent leurs lignes `[MARCHANDS]` et
`[BOUTIQUE]` sur la sortie d'erreurs).

## LP-009, le hub de l'auberge (étape 1, mesure du 2026-09-28)

Demande de Vincent (2026-09-28) : une porte dans la salle de la réception, vers une pièce à nous —
la carte 82 « Le coffre », **vide** hormis sa porte et son coffre de stockage —, futur lieu des PNJ
marchands et d'autres fonctions. Change : `openspec/changes/archive/2026-09-28-add-legacy-plus-inn-hub`.

**Le numéro d'une carte de l'auberge = palier + pièce.** `FUN_004747F0` (`WorldMap.cs`) choisit la
réception parmi 0, 8, 16 … 80 selon la progression ; la pièce s'ajoute (+2 = le coffre). La carte 80
n'est que la réception du **dernier** palier ; la boutique LP-008, codée sur 82, n'existe donc qu'en
fin de partie (lecture à confirmer en jeu).

**Ce que la mesure établit :**

- **La table des sous-zones de la zone 0 a 105 entrées** (`bestiaire.md`). Les pièces +6 et +7 de
  chaque palier (6, 7, 14, 15 … **86, 87**) sont des **emplacements vides** : répertoire de 264
  octets, rien dedans. **88 à 104** sont dix-sept **copies de la 82** (mêmes décors, plus des
  meubles et deux monstres placés dans la pièce) qu'aucune partie n'a jamais chargées — curiosité
  à part. **Numéro retenu pour le hub : 86** (`0x56`) — il existe dans la table (le jeu sans le
  tweak le charge sans planter : la tournée l'a fait, `handlers-exerces.tsv`), et aucun palier ne
  l'emploie.
- **La pièce « coffre » est la même à tous les paliers** : répertoires de 2 244 octets de 10 à 82
  (2 748 en 2, 2 384 en 26), mêmes décors aux mêmes places. Le hub charge le répertoire de la 82, quel
  que soit le palier d'origine.
- **Ce qui lit le numéro de sous-zone, en zone 0** : les tables de gestionnaires sont toujours
  celles de l'index 0 (`Resources.cs`), le banc de sons toujours `Q00_00.snd`, aucun gestionnaire ni
  verbe de l'auberge ne teste la sous-zone. Restent : LP-008 (`== 82`, le hub n'est donc pas une
  boutique), la vidange de manette de `RunWorldMapFrame` (`& 7`, sans effet pour 6), la
  **sauvegarde** et l'**étiquette des repères** du menu (ci-dessous).
- **Les meubles à garder en 82** : 0 (porte, sous-type 2) et 1 (son échelle, 15) en (−556, 400,
  1000) ; 15 (corps du coffre) et 16 (couvercle, `0x00450760`, script au contact) en
  (−1400, 400/440, 1100). Tout le reste s'en va, dont l'armoire (7).
- **Les collisions des meubles — non tranché par la lecture.** Le contact entre entités
  (`ResolveEntityContact_00534330`) teste le maillage **de l'occupant**, donc un meuble-entité retiré
  emporte sa collision. Mais un décor statique (`kind & 3 == 0` dans `SpawnSubAreaEntities`) reçoit
  le bit `0x2000`, que le balayage de contact (`ApplyEntityMotion_00519DF0`) **exclut** : un tel
  meuble ne heurte personne par ce chemin, et s'il bloque en jeu, c'est que le **terrain** de la
  pièce le fait. `decor.csv` ne relève pas `kind`. **Réponse la moins chère : le filtre écrit, une
  marche en jeu.** Repli si le terrain bloque : Vincent choisit (garder ces obstacles, autre pièce).
- **La sauvegarde retient la sous-zone chargée** : `+0x1f01..+0x1f03` = zone, `DAT_00566c88`, et
  `DAT_005de044` (le point de reprise, posé par un volume de déclenchement de type 8). **Deux
  écrivains** : le menu (`CopyGlobalBanksIntoSaveBlock`, `WorldMapMenu.cs`) et **le coffre de
  stockage lui-même** (`WorldMapDepot.cs`, écrit à chaque usage). Tel quel, ouvrir le coffre dans le
  hub écrirait 86. D'où : **retenir, au départ de la réception, `00566c88` et `005de044`**, et les
  écrire à la place quand la sous-zone chargée est le hub. Même traitement pour
  `CurrentMarkerTag` (les repères de la page 4 du menu).
- **Sortir de la 82 = l'opcode 17** (`ScriptOpTransferArea`) : tous les écrivains de `DAT_005de52c`
  sont recensés, c'est le seul qui change de sous-zone depuis une carte (l'opcode 36 et la face
  marchée restent dans la sous-zone chargée). Sa destination est un octet du script de la 82 :
  **la couture est dans l'opcode 17**, quand la sous-zone chargée est le hub.
- **La porte dans la salle est possible.** Modèles (`areaDir + 0x38`) et maillages (`0x005f7bc0`)
  sont chargés **par sous-zone**, pas par section : la porte de la liste 1 (le couloir) est donc
  valable en section 0. Les entités d'une section naissent par `SpawnSubAreaEntities(sheet[6])` à
  chaque entrée dans la section (`PlaceWorldMapParty`, et l'opcode de script qui repeuple) : la porte
  ajoutée doit renaître au même moment, par une couture juste après.

**Tranché au premier essai (2026-09-28)** :

- **Les collisions des meubles sont dans le terrain.** Le morceau 0 de la sous-zone (celui que
  `TryPlaceEntity` reçoit) est un maillage de collision : faces de 16 octets, attributs au quatrième
  dword, sommets de 8 octets (x, y, z ; y négatif vers le haut). Dans la 82, le plancher est à
  y = −400 et chaque meuble y est une **face de sol surélevée** (−432 à −520) sans plancher dessous ;
  trois faces portent le bit `0x0800` (on ne s'y tient pas). Relevé par un verbe ajouté pour
  l'occasion : `OpenForesia.Tools terrain <zone> <sous-zone>` (CSV). Le hub ramène ces sommets au
  plancher et efface le bit, à chaque chargement.
- **« - Le coffre - » est le script d'arrivée** du point d'entrée (moitié haute du premier dword,
  lancé au pas 2 par `0x005de048`) ; `0xffff` = aucun script. Le hub arrive par son propre point.
- **Une porte de l'auberge est un battant** que le groupe pousse (0x004736D0 fait tourner `+0x32` de
  la vitesse reçue au contact) : la porte ajoutée porte `0x2000`, écartée du contact.
- **Son point d'ancrage est le gond** : le battant s'étend vers les z décroissants.
- **Les places retenues** (relevés de Vincent) : porte en (−400, 0, 583), passée depuis la bande
  z 383..583 face au mur ; arrivée dans le hub en (−695, 888), cap 3072 ; retour en (−109, 504),
  cap 1024 — en (−236, 504) le jeu posait le groupe sur l'estrade, la face la plus haute sous le
  point ; coffre du hub (corps et couvercle) déplacé en (−1337, 886).

**Restent pour l'écriture** : le mur (relevé de Vincent, `TracePosition`) ; le point d'entrée que le
couloir donne à l'opcode 17 pour arriver en 82 (un journal au premier passage) ; les collisions.

## LP-010, les marchands du hub (validés en jeu le 2026-09-28)

Change : `openspec/changes/archive/2026-09-28-add-legacy-plus-hub-merchants`. Quatre PNJ sous des modèles de classe
jouable (Lutin 7, Voleur 2, Kobold 6, Sylphide 13), une boutique par catégorie d'objet.

- **Les ressources d'une classe sont dans le répertoire de l'ÉTAT 1** (la composition du groupe),
  pas de l'état 3 : `LoadPartyMemberResources` tourne pendant l'état 1, et sur la carte ce qu'il a
  chargé reste en place. Relevé hors jeu par `OpenForesia.Tools classes` : modèle 14 à 22 Ko,
  animations 29 à 52 Ko en 8 ou 9 clips, deux textures aux mêmes dimensions pour toutes les classes —
  une 8 bits de 128 mots × 64 lignes avec quatre palettes de 256 couleurs, une 4 bits de 16 mots × 64.
- **Où le jeu les pose, pour l'emplacement `i`** : texture 1 en x 256..383, y 256 + 64·i (les quatre
  membres remplissent donc x 256..383, y 256..511) ; texture 2 en x 384 + 16·i, y 448..511 ;
  palettes aux lignes 480 + 4·i et 496. Un cinquième emplacement tomberait en y = 512, hors de la
  VRAM : les marchands ont besoin d'une place trouvée ailleurs (relevé `[Diagnostic] ReleveVram`).
- **Ce qu'un modèle de classe lit en VRAM** (`DrawAnimatedModel`, `EmitModelQuads`) : UNE page et
  UNE palette pour tout le modèle, prises dans `0x005e8f00` à l'entrée de l'octet 2 de son `+0x3c`
  (`0x20 + i` pour le membre `i`) ; les enregistrements n'apportent que les UV, et
  `AdjustModelTexturePages` leur ajoute `64·i` en V (octet 1 de la pagination). La texture 2 n'entre
  pas dans le rendu du personnage. Les octets `0x04`/`0x06` des parties sont des drapeaux (`0x400` :
  sans tri en profondeur). La palette n'est pas contrainte à la bande 448..511 : n'importe où tant que
  y ≤ 507 (le plafond `0x7ec0` du mot UV).
- **La VRAM du hub** (relevé du 2026-09-28, groupe de quatre) : la bande **x 832..1023, y 256..511**
  est entièrement vide — le même gabarit que le bloc du groupe (x 256..447), neuf pages plus loin ;
  **x 0..255, y 224..447** aussi. Ni le coffre (vignettes en x ≥ 960, y 136..208 ; palettes en
  y 508..511) ni l'achat (x 384..443) n'y écrivent.
- **Le plan retenu** : le marchand `k` (0..3) prend l'emplacement virtuel `i = k` — texture 1 en
  (832, 256 + 64·k), 128 mots × 64 lignes (la page 13, 8 bits, couvre x 832..959) ; ses quatre
  palettes en (0, 256 + 4·k), 256 × 4 ; une entrée libre de `0x005e8f00` (`0xcc + 4·k`) portant cette
  page et cette palette ; pagination du modèle : octet 1 = `0x40·k`, octet 2 = l'index de l'entrée.
- **L'animation de repos est le clip 0** (relevé en jeu : un membre immobile joue le clip 0).

## LP-012, l'arène du hub (étape 1, mesure du 2026-09-28)

Demande de Vincent (2026-09-28) : depuis le hub, un téléporteur vers une arène — la salle du boss
de la vieille tour, 4/9 « Parmi les Elus » —, un boss à un palier de 1 à 10 (PV et dégâts × palier),
la cinématique de fin du jeu, un portail vers le hub, deux coffres ; la défaite ramène au hub. Change :
`openspec/changes/add-legacy-plus-arena`. Validée d'abord sur l'Enchanteresse (native), puis le
Grizzly (`add-legacy-plus-boss-graft`), puis tous les boss l'un après l'autre.

**Ce que la mesure établit (lecture du code, sans jeu) :**

- **La naissance d'une formation** : l'opcode 13 des scripts (`ScriptOpSpawnFormation`,
  `WorldMapScript.cs:1170`) prend la liste `*(int**)(ctx + 0x34)[n]` et appelle
  `SpawnEntityFormation(ctx, liste, 0, mode)` (`FUN_0051b850`, `:1455`) : des enregistrements de
  `0x20` octets terminés par `-1` — mot 0 le genre, X / Y / Z en `+4` / `+6` / `+8`, cap en `+0xc`,
  `+0xa` et `+0xe` recopiés en `+0x30` / `+0x34`, `+0x10`/`+0x14` les références, `+0x18` le mot
  `+0x14` de l'entité. `mode` non nul : échelle `0x1000`, teinte blanche. Rend le nombre né. **Le
  directeur l'appelle tel quel**, sur une copie du premier enregistrement de la formation 0 de 4/9
  (l'Enchanteresse seule, sans ses deux Ténébreux).
- **Le palier n'a besoin d'aucune couture nouvelle** : `PvALaNaissance` (LP-002, `ResetEntityStats`)
  porte les PV maximum `+0x48` et courants `+0x4c` — des **`ushort`**, 65 535 au plus : 910 × 10
  tient, 800 aussi — et `DegatsSubis` (LP-001, `ApplyDamage_00539A20`, par où passent **tous** les
  coups) porte les dégâts, un `short` (à borner à 32 767 après × palier). Reste à vérifier à l'essai
  qui est l'« attaquant » d'un sort de l'Enchanteresse : elle, ou l'entité du projectile.
- **Les scripts de la salle** entrent par deux portes : les **volumes de déclenchement**
  (`TestTriggerVolumes_00534CD0`, `Physics/TriggerVolumes.cs`) — entrée, sortie, première sortie —
  et la **réaction d'une entité frappée** de genre 10 (`HitResolution.cs:1081`, `:1271`), qui lance
  le script `+0x0e` de la victime. Le directeur fait naître ses créatures sans script ; les volumes
  sont tus par **une couture nouvelle en tête de `TestTriggerVolumes_00534CD0`** (le bit `0x10` de
  `0x005de02c`, qui les coupe déjà, gèle aussi la boucle du monde : inutilisable). Conséquence
  voulue : en arène, les sorties de la salle ne mènent nulle part ; on en sort par le portail ou par
  la défaite.
- **Rien ne traite (4, 9) à part** dans le legacy : aucun test `zone == 4 && sous-zone == 9` ; seules
  des tables de gestionnaires par groupe de sous-zones (`LabyrinthParticles.cs:1686`) — des données.
  La scénette de sortie (`0x00474380`) ne court-circuite que (3, 8) et (4, 6).
- **La sauvegarde** s'écrit par deux chemins, tous deux déjà passés par la couture `LieuSauve` :
  le menu du jeu (`WorldMapMenu.cs:3779`) et le coffre de stockage (`WorldMapDepot.cs:2279`, absent
  de l'arène vidée). En arène, l'entrée « sauvegarder » du menu est refusée ; `LieuSauve` écrit le
  lieu du hub, en filet.
- **La défaite** : `Tick/WorldMapHud.cs:175-182` — tous tombés, `0x005de02c |= 2` lance le fondu ;
  `s_GameStateId_005de510 = 9` quand il a fini. **Une couture nouvelle** sur ce passage à l'état 9.
- **Le téléporteur du hub** (relevé et captures de Vincent, `Tests/11. Teleporteur` ;
  `OpenForesia.Tools decor-modele 5 9 2 <rang>`) : en 5/9, section 2, une salle à **quatre piliers
  lumineux** aux coins (rangs 2 à 5, le modèle 1 ; deux portent une lueur, une particule, rangs 7 et 8)
  et, **au centre, le téléporteur** : rang 6, genre 2, le **modèle 6** du paquet `+0x24`, descripteur
  `01 03 01 20` (entrée de texture 1, une page 4 bits entière, semi-transparent), en (−2500, 55, −100),
  paramètre de dessin `0x000c` (la palette 12). Deux essais ratés avant lui : les effets de la 82
  (une flamme), puis un pilier et sa lueur (« un bloc lumineux »). Greffé dans le hub : le répertoire
  de 5/9 relu, le paquet relogé, la page envoyée **en (960, 256)**, palette **ligne 276**, entrée de
  table **`0xe0`**, à 55 au-dessus du sol comme en 5/9. La VRAM libre du hub restante : x 0..255,
  y 224..447 hors marchands (palettes 256..275) et Voleuse (x 0..127, y 320..383).

**Fichiers proposés** (`src/LegacyPlus/`, à valider par Vincent) :

| Fichier | Ce qu'il porte |
|---|---|
| `EcranMenu.cs` | l'écran de menu générique tiré de `BoutiqueEcran` (liste, pas, question Oui / Non) ; la boutique et le rachat passent dessus |
| `AreneTeleporteur.cs` | le téléporteur du hub, la question « …dangereux, on y va ? », le menu en trois pas |
| `Arene.cs` | le mode arène : l'interrupteur, l'aller, la salle vidée, les volumes tus, la sauvegarde, la défaite |
| `AreneDirecteur.cs` | les programmes et leurs vagues, la naissance, le palier, la fin (scénette, portail, coffres) |

Coutures nouvelles dans `src/Blaze/` : deux, une ligne chacune — l'entrée des volumes de
déclenchement et le passage à l'état 9.

## Préconisations en attente

### La limite de rendu : 2 500 primitives par image (constatée avec LP-007, le 2026-09-27)

**Le constat.** Avec beaucoup de monstres à l'écran, la carte se dégrade : des morceaux de décor
manquent, et changent d'une image à l'autre. La cause est un budget du moteur de 1998, hérité de la
PlayStation : `FrameBegin` (`Screens/State00/MainMenu.cs`) pose `s_Unknown_005e9818 = 0x9c4`, soit
**2 500 primitives par image**, et toutes les boucles de dessin (`Engine/Model3d.cs`, 8 boucles ;
`Engine/Primitives2d.cs`) s'arrêtent net quand il tombe à zéro. Ce qui vient ensuite n'est pas
dessiné. Le jeu d'origine n'a jamais qu'une formation à la fois et reste sous le budget.

**Ce qui a été fait.** Rien sur le rendu, à la demande de Vincent. LP-007 est plafonné à **12
entités de scène vivantes** (`Rencontres.LimiteRendu`) : au-delà, le tweak ne fait plus naître de
rencontre ni de formation supplémentaire.

**La préconisation, pour le jour où l'on veut plus de monstres.** Relever la limite est un écart
legacy ++ (une contrainte matérielle de 1998, entrée `LB-NNN`), en trois pièces, aucune n'ayant de
marge aujourd'hui :

1. **le budget** `0x9c4` dans `FrameBegin` — une ligne ;
2. **les deux tampons de primitives**, 112 Ko chacun (`MainMem + 0x1c8000` et `+ 0x1e4000`), à
   **déplacer** dans une allocation plus grande : l'arène `MainMem` est pleine (la DLL d'affichage
   occupe `+0x200000`), et la nouvelle doit rester **sous 16 Mo** (liste d'affichage adressée sur
   24 bits, voir `AllocLowZeroed` dans `Platform/Winsys3d.cs`) ;
3. **l'arène de travail de la DLL d'affichage** (`src/Libraries/winsys3d/CheminAffichage.cs`,
   2 × 256 Ko à `MainMem + 0x200000`), qui traduit ces primitives et déborderait à son tour.

**Première étape recommandée : mesurer** — combien de primitives consomment le décor et un monstre
typique par zone, et combien d'octets l'arène de travail utilise par primitive —, pour choisir le
nouveau budget (×2 ou ×4) et les tailles en connaissance de cause. Ensuite, un change OpenSpec à
part, sur le chemin d'affichage.
