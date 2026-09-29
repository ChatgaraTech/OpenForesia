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
| LP-012 | L'arène du hub : un téléporteur, un menu (mode, palier 1 à 10 : PV et dégâts × palier), la salle du boss de la vieille tour (4/9) vidée pour un combat rejouable ; fin native (cinématique, portail vers le hub) et une bourse en pièces d'or pour le groupe — les deux coffres du début retirés le 2026-09-29 —, défaite = retour au hub | aucune : suit `[Auberge] Hub` | `PasDuMembre` (la question), `DessinSurImpression` (le menu), `EntreeSousZone`, `PointDEntree`, `DecorNe`, `Rencontre`, `LieuSauve`, `ImageSuivante` (directeur, fin, journal Debug), `PvALaNaissance`, `DegatsSubis`, et trois coutures neuves : `VolumesTus` (`TriggerVolumes.cs`), `DefaiteEvitee` (`WorldMapHud.cs`, le passage à l'état 9) et `ScriptTu` (`RunEntityScript`, aucun script de la sous-zone en arène) | validé en jeu (2026-09-28) — `src/LegacyPlus/AreneTeleporteur.cs`, `Arene.cs`, `AreneDirecteur.cs` ; `openspec/changes/archive/2026-09-28-add-legacy-plus-arena` |
| LP-011 | Le rachat : la Voleuse du hub rachète l'inventaire (hors équipement) et le coffre du joueur, à un taux du prix d'achat des quatre boutiques ; un objet absent des catalogues ne se revend pas ; une pile se vend en entier | `LegacyPlus\BoutiqueVente.ini` : `[Vente] Taux` (50) | celles de LP-010 | écrit le 2026-09-28, à tester en jeu — `src/LegacyPlus/BoutiqueVente.cs`, `MarchandsHub.cs` ; une vente depuis le coffre écrit la sauvegarde (emplacement vidé, or crédité, sommes refaites) |
| LP-013 | Les boss greffés de l'arène : un monstre d'une autre sous-zone combat dans 4/9 comme chez lui ; dix-huit programmes greffés (plus ENCHANTERESSE, native), chacun le combat de boss d'une sous-zone avec son escorte, ses sons et ses effets : MECHANT OURS, JEUNE DRAGON, LOUP-GAROU ET TIGRE, NOCTURIO, SYLPHIDE, FOUGRE, DURAHAN, TROLL, BEHEMOTH, GROS POILU, ANGE NOIR, EIFRIDT, ROI-ZOMBIE, LORDEMON, CERBERE, BELZEBUTH, DRAGON ZOMBIE, DRAGON ROUGE (le Kraaken abandonné) ; le menu trié par PV du combat ; la VRAM attribuée par programme (cinq bandes) ; les effets par le contexte de la sous-zone d'origine | aucune : suit `[Auberge] Hub` | `ListeDApparition` (l'installation), `PvALaNaissance` et `DegatsSubis` (le palier, par le directeur de LP-012), et sept coutures neuves : `EffetCoupe` (`SpawnEffectEntity`), `SonPositionnel` (`PlayPositionalSound`), `SonJoue` (`PlaySoundEffect`), `ButinNe` (`DespawnSceneEntity_0051a740`), `AvantGestionnaire` et `ApresGestionnaire` (autour du gestionnaire, passe de scène et `CallIndexedEntityHandler`), `GestionnaireInstalle` (`CallIndexedEntityHandler`) | validé en jeu (2026-09-29, chaque programme) — `src/LegacyPlus/AreneGreffe.cs` ; `openspec/changes/archive/2026-09-29-add-legacy-plus-boss-graft` |
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
la cinématique de fin du jeu, un portail vers le hub, deux coffres (remplacés le 2026-09-29 par une bourse, voir LP-013) ; la défaite ramène au hub. Change :
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
| `AreneDirecteur.cs` | les programmes et leurs vagues, la naissance, le palier, la fin (scénette, portail, bourse) |

Coutures nouvelles dans `src/Blaze/` : deux, une ligne chacune — l'entrée des volumes de
déclenchement et le passage à l'état 9.

## LP-013, les boss greffés de l'arène (étape 1, mesure du 2026-09-28)

Demande de Vincent (2026-09-28) : « ajouter le Grizzly comme boss affrontable dans l'arène ». Change :
`openspec/changes/archive/2026-09-29-add-legacy-plus-boss-graft`. Mesure refaisable hors jeu :
`OpenForesia.Tools greffe <zone> <sous-zone>` (espèces, fiches, maillages et leur pagination,
animations, textures et leur place en VRAM, sons de niveau, tables `0x0056ad48` et `0x0056b0c8`).

**Ce que la mesure établit :**

- **Le « Grizzly boss » du design n'existait pas tel quel.** En 3/5 « Le lieu de coupe », la
  formation 1 (espèce 3, seule) est une Abeille tueuse « simple » (IA 19 = `0x004A5F10`, fiche 1).
  **Le combat de boss de la forêt est la formation 0** : le **Méchant ours** (espèce 0 : maillage 0,
  IA 16 = `0x004A4AE0`, fiche 0 — 300 PV, rang 38) flanqué de **deux Grizzlys** (espèce 4 :
  maillage 2, IA 4 = `0x004A4270`, fiche 3 — 80 PV, rang 25). Vincent a choisi le combat entier.
- **Le son 3 et l'effet 0x39 du Grizzly ne dépendent pas de la zone.** Un son d'id < 60 vient du banc
  commun (`Base.snd`) ; un effet de sous-type < `0xc0` passe par la table **statique**
  `0x00551c30` (l'éclat d'impact `0x00447280`, le même partout). Rien à traduire pour le Grizzly.
- **Le Méchant ours a deux références de zone** : le son **`0x3f`** (id ≥ 60, banc de niveau
  `Q03_05.snd` : 16 004 octets ; dans 4/9 le même id est un autre son) et **le souffle**, un
  sous-type `0xc0 + octet 9 du premier bloc de +0xcc` (`0x1e` en 3/5, `0` en 4/9) — la table par
  sous-zone `0x0056b0c8`. En 3/5 c'est `0x00413330`, le voile rouge, qui fait naître quatorze
  anneaux `0xde` (`0x004134C0`, table `0x0056b1c8`) dessinés avec deux modèles de décor de 3/5
  (7 et `0x30`), et joue un son de la table `+0xa8`. Dans 4/9, le même calcul désignerait un
  créneau vide : un effet qui ne ferait rien et ne mourrait jamais.
- **La mort** : `DespawnSceneEntity_0051a740` tire le butin dans la table `+0xb4` de la sous-zone
  **courante**, au rang `espèce[5]` — dans la tour, un objet de la tour. Les éclats d'expérience
  n'en dépendent pas.
- **La place dans 4/9** : trois espèces, trois fiches, trois maillages (rangs 0..2 des tables
  `0x005e96a0` / `0x005e9640`) ; IA occupées 2, 4, 8, 9, 10, 13, 14, 15 (16 et 17 libres) ; sons de
  niveau 60..68 (69..76 libres, le banc occupe 160 136 octets sur 260 000). Le relevé VRAM de 4/9
  (`legacyplus-vram-z4-s9.txt`, 2026-09-28) montre libres x 128..255 × y 16..223 (une page 8 bits
  en (128, 0)) et les lignes 288..319 en x 0..255 (des palettes) ; l'entrée `0xe8` et suivantes de
  `0x005e8f00` sont libres.

**Le plan retenu, lot 1 (`AreneGreffe.cs`, nom validé par Vincent) :**

| Espèce | Maillage (4/9) | IA | Texture (3/5 → 4/9) | Palette | Entrée | Sons |
|---|---|---|---|---|---|---|
| Méchant ours (espèce 0) | rang 3 | `0x004A4AE0` | entrée 5, 60 × 48 mots → (128, 128), bande V `0x80` | ligne 288 | `0xe8` | `0x3f` → 69 |
| Grizzly (espèce 4) | rang 4 | `0x004A4270` | entrée 7, 128 × 64 mots → (128, 64), bande V `0x40` | ligne 289 | `0xec` | — |

Espèces et fiches prennent les rangs 16 et 17 de tables recopiées, sur lesquelles le répertoire de
4/9 est repointé jusqu'au prochain chargement. Pour une créature greffée : un son de niveau non
traduit est coupé, un effet `>= 0xc0` est coupé (**le souffle, lot 2**), le butin est éteint (ni
objet ni pièces, décision de Vincent : la récompense de l'arène, ce sont les coffres — puis, le 2026-09-29, la bourse). Chaque coupure
est écrite une fois au journal `[GREFFE]`.

**Lot 2, le souffle** : greffer `0x00413330` et `0x004134C0` dans deux créneaux libres de
`0x0056b0c8` / `0x0056b1c8`, les modèles de décor 7 et `0x30` de 3/5 et leur texture, le son de la
table `+0xa8` ; traduire les deux sous-types.

### Le Behemoth et la bourse (2026-09-29)

Demande de Vincent (2026-09-29) : ajouter le Behemoth ; supprimer « tout compte fait » les coffres de
l'arène au profit d'une somme d'argent, « moins pour les boss faibles, plus pour les boss forts »
(le Méchant ours à 40 pièces), répartie entre tous les membres du groupe.

**Le Behemoth** (`OpenForesia.Tools greffe 3 9`) : 3/9 « La grotte souterraine », formation 0 seule
(espèce 0 : maillage 0, IA 17 = `0x004A54E0`, fiche 0 — 2 160 PV, 400 PM, rang 150). Il est **moins
cher à greffer que l'ours** : ses sons (3, `0x20`, `0x35`) sont tous du banc commun, son lancement
de sort (`0xbd`) et son éclat (`0x39`) de la table statique, et ses sorts (famille 0, indices
13/19/23) sont des fiches du répertoire d'état, les mêmes partout. **Une seule référence de zone** :
l'atterrissage de son bond, sous-type `0xc0 + octet 2 du premier bloc de +0xcc` (`0xdf` en 3/9 =
`0x00483650`) — coupé comme le souffle de l'ours. Sa texture (entrée 5, 92 × 48 mots, 8 bits) prend
**la place du Méchant ours** — (128, 128), bande de V `0x80`, palette 288, entrée `0xe8` — : la VRAM
libre de 4/9 n'a plus de bande de 48 lignes entière à côté de l'ours et du Grizzly, et les deux boss
ne sont jamais dans le même programme. Un boss rush qui les mettrait ensemble devra lui trouver une
autre place.

**La bourse** (`Arene.Recompenser`) : à l'ouverture du portail, la bourse du programme × le palier
est versée au groupe, part égale pour **tous** les membres, debout ou non, le reste de la division à
raison d'une pièce aux premiers, chaque bourse bornée à 99 999 comme au ramassage d'origine. Un
message « Le groupe remporte N pièces d'or » s'ouvre au premier pas d'un membre debout. Montants :
MECHANT OURS **40** (Vincent), ENCHANTERESSE **150** et BEHEMOTH **300** (proposés à l'échelle des
PV et du rang, confirmés par Vincent). Validé en jeu le 2026-09-29. Les deux coffres (copies du coffre de la liste 3, numéros 510 et 511,
et leurs bits de la banque `0x005e96e0`) sont retirés ; le coffre natif de la salle reste rendu au
néant.

### Les boss restants (relevé du 2026-09-29)

Demande de Vincent (2026-09-29) : « tous les autres boss ». Relevé hors jeu :
`OpenForesia.Tools greffe <z> <s>` sur chaque sous-zone, puis lecture des fichiers `Ai/` des IA. Un
boss est ici le chef d'une formation à script de mort (genre 11) de forte fiche, avec son escorte.
**Les dix-huit IA sont écrites et ont été vues en jeu** (`GestionnairesEntites.md`) : aucune
transcription ne bloque. Ce qui coûte, c'est ce que chaque boss emprunte à SA sous-zone.

Légende des colonnes :
- **Effets** : les naissances de sous-type `>= 0xc0`, prises dans la table par sous-zone
  `0x0056b0c8` au rang que donne le premier bloc de `+0xcc`. En lot 1, on les coupe.
- **Sons** : les sons de niveau (id ≥ 60) ; la table `+0xa8` est signalée à part.
- **Texture** : la place qu'elle prend en VRAM, en mots.

| # | Boss (sous-zone) | Escorte | PV du combat | PV / rang du boss | IA | Effets par sous-zone | Sons de niveau | Textures (mots) | Bourse | État |
|---|---|---|---|---|---|---|---|---|---|---|
| 1 | Jeune dragon (1/4) | — | 700 | 700 / 40 | `0x004944E0` | 4 (`0x00483b50` → `0x00482ff0`, `0x00409e40` → `0x00409f40`), greffés | `0x41`, greffé | 96×48 | 100 | **validé** |
| 2 | Loup-garou et Tigre (6/5) | les deux ensemble | 968 | 468 + 500 | `0x004D7200`, `0x004D80D0` | 1 chacun (`0xde` = `0x0042a440` → `0x0042a540`), greffés | table `+0xa8` (`[0x2c]`), par `SonJoue` | 60×48 + 128×64 | 140 | **validé** |
| 3 | Nocturio (6/11) | 2 Sacubas (form. 1) | 1 220 | 840 / 150 | `0x004DACC0`, `0x004D9D90` | 7 (`0x00429a20`, `0x0042a920` et leurs enfants), greffés | — | 116×64 + 128×64 | 170 | **validé** |
| 4 | Sylphide (4/7, form. 1) | Wybarn | 1 263 | 563 / 100 (Wybarn 700) | `0x004B4570`, `0x004B7540` | Sylphide : aucun (des sorts). Wybarn : 1 (`0x00483930`), coupé | Wybarn : `0x47`, greffé | 128×64 + 128×64 | 180 | **écrit** (lot 1) |
| 5 | Fougre (8/1) | 2 Grifons | 1 272 | 1 030 / 180 | `0x004E20F0`, `0x004E1380` | 1 (`0xc4` = `0x0042f2a0`), coupé | — | 104×64 + 112×48 | 180 | **écrit** (lot 1) |
| 6 | Durahan (5/4) | 3 Sanguinaires | 1 531 | 808 / 80 | `0x004C4F90`, `0x004C2770` | 2 (`0x00422d50` → `0x00422e50`), greffés ; le `+0x14` vaut `0x36`, statique | `0x47`, greffé | 104×64 + 128×64 | 220 | **validé** |
| 7 | Troll (1/9) | 2 Géants verts | 1 650 | 850 / 130 | `0x004965B0`, `0x00495C90` | 2 (`0x00409e40` → `0x00409f40`), greffés | `0x40`, greffé | 128×64 + 128×64 | 230 | **validé** |
| 8 | Le gros poilu (10/12) | — | 2 300 | 2 300 / 218 | `0x00500E10` | 1, rang variable (`+0x16`), coupé | — | 96×64 | 330 | **écrit** (lot 1) |
| 9 | L'Ange noir (10/14) | — | 2 391 | 2 391 / 238 | `0x00502210` | 8, la pluie comprise (`+0xd4[7]`), greffés | — | 128×64 | 340 | **validé** |
| 10 | Eifridt (10/11) | — | 2 400 | 2 400 / 212 | `0x005003E0` | 2 (`0x00483b50` → `0x00482ff0`), greffés | — | 128×128, sur deux bandes | 340 | **validé** |
| 11 | Kraaken (9/0–9/1, form. 2) | bras ×2, pieds ×6 | 2 601 | corps 1 / 200 | `0x004E40E0`, `0x004E4890`, `0x004E4F20` | 1 (octet `0x0a`) | — | corps **4 bits 64×256** + 48×64 + 24×64 | 400 | **abandonné** (Vincent) |
| 12 | Roi-zombie (5/9) | Durahan-noir, Revenant, Ombre rouge (×2 chacun) | 2 726 | 1 350 / 150 | `0x004C7240` + 3 | 13, greffés | — | **4 textures**, sur quatre des cinq bandes | 330 | **validé** |
| 13 | Lordémon (10/13) | — | 2 763 | 2 763 / 235 | `0x00501840` | 6 (`0x0043eb60`, `0x00486680`, `0x004868e0` et leurs enfants), greffés | — | 120×64 | 400 | **validé** |
| 14 | Cerbère (10/9) | 4 Groklebs | 2 812 | 2 100 / 200 | `0x004F7980`, `0x004F5630` | 9, greffés (dont l'enfant de `0x00484f80`, lu en jeu seulement) | — | 128×64 + 128×64 | 400 | **validé** |
| 15 | Belzébuth (10/15) | — | 3 000 | 3 000 / 320 | `0x00502CD0` | 11, greffés | `0x3c`, greffé | **128×192**, les trois bandes | 430 | **validé** |
| 16 | Dragon zombie (10/4) | 2 Maudites | 5 438 | 4 400 / 180 | `0x004F6740`, `0x004F4FD0` | 4 + un tiré au sort (`+0xcc[0x10 / 0x0c / 0x11]`), greffés | `0x42`, greffé | **120×96** (deux bandes) + 128×64 | 780 | **validé** |
| 17 | Dragon rouge (2/0) | 2 Salamandres feu | 8 480 | 8 000 / 306 | `0x00499140`, `0x00497EF0` | 7, greffés | `0x45`, greffé | 128×64 + 128×64 | 1 200 | **validé** |

Trié par **PV du combat** au palier 1, boss et escorte compris (Vincent, 2026-09-29). L'ordre de
travail, lui, est plus bas.

Hors liste : le Cristal maudit de 4/8 (900 PV, rang 255, IA 30) est un décor d'événement que des
scripts « au coup reçu » (genre 10) animent, pas un combat. Or l'arène coupe tous les scripts.

**Le travail commun, avant les boss :**

1. **La VRAM par programme.** Les places fixes ne suffisent plus. La page (128, 0) de 4/9 offre trois
   bandes de V libres de 128 × 64 mots (`0x40`, `0x80`, `0xC0`). Le relevé montre aussi y 224..255
   libre, à revérifier sur une tour sans greffe. Il faut donc attribuer les bandes à chaque entrée
   dans l'arène, les textures de plus de 64 lignes sur des bandes contiguës. Belzébuth prend les
   trois (y 64..255). Le Roi-zombie avec toute son escorte (4 textures) et le corps 4 bits du Kraaken
   (64 × 256) n'y tiennent pas : il faut une autre place, ou une escorte réduite.
2. **La greffe des effets par sous-zone.** C'est le lot 2 de l'ours, généralisé. On copie le
   gestionnaire dans un créneau libre de `0x0056b0c8` de 4/9 et on traduit le sous-type. On greffe
   aussi ce que l'effet charge de sa sous-zone : modèles de décor, textures, sons `+0xa8`. Quinze
   boss sur dix-sept en dépendent. Sans elle, on ne peut que couper l'effet, et le souffle du Dragon
   rouge ou la pluie de l'Ange noir disparaîtraient.
3. **La table de sons `+0xa8`** : le Loup-garou, le Tigre et le souffle de l'ours la lisent.
4. **Le recensement (tâche 6)**, en suivant les appelées : les effets greffés font eux-mêmes naître
   d'autres effets. L'outil `greffe` doit aussi imprimer le premier bloc de `+0xcc` au-delà de
   16 octets, car les octets `0x12` à `0x19` servent au Dragon rouge, au Cerbère, au Lordémon, à
   l'Ange noir et au Dragon zombie.
5. **Les scripts.** L'arène les coupe tous. Or le geste 2 de Belzébuth lance le script
   `0x00553278` : il faut décider, boss par boss, lesquels laisser passer.

**Où en est le travail commun (2026-09-29, suite) :**

- **8.1.1, fait** (`AreneGreffe.Placer`, `ChargerModele`). À chaque entrée dans l'arène, les textures
  du programme reçoivent, dans l'ordre du programme, les bandes y 64, 128 et 192 de la page (128, 0),
  les palettes à partir de la ligne 288 et les entrées de décodage à partir de `0xe8`. Une texture
  plus haute que 64 lignes prend des bandes qui se suivent. Le maillage relogé garde en dur sa bande
  de V et son entrée : il est rechargé depuis l'archive quand sa place change. Une texture trop
  grande arrête la greffe avec un message `[GREFFE]`. La bande y 192..255 est libre au relevé ;
  Belzébuth et le Dragon zombie l'ont validée en jeu. Pour le Roi-zombie, qui a quatre textures, deux
  bandes de plus ont été prises dans la page (128, 256), en y 320 et 384 : le relevé les montre
  libres, alors que y 256..303 porte d'autres pages et nos palettes, et y 448.. est occupé. Une
  texture haute reste dans une seule page ; la page passe par l'entrée de décodage.
- **8.1.2, mesuré, pas commencé.** Un effet de sous-type `>= 0xc0` né de la table des effets
  (`0x005ff400`) trouve son gestionnaire dans `0x0056b0c8`, que chaque sous-zone recharge
  (`ResolveEntityHandler` : `0x0056adc8` + sous-type × 4 déborde dans `0x0056b0c8`). Les deux autres
  pools en font autant, vers `0x0056b1c8` et `0x0056b2c8`. Mettre le gestionnaire d'origine dans un
  créneau libre de la tour et traduire le sous-type ne suffit pas. L'exemple de `0x00409E40`, l'éclair
  du Jeune dragon et du Troll, le montre :
  - il joue un son de la table `+0xa8` de sa sous-zone (`ZoneSound(0x2c)`) ;
  - il fait naître onze enfants `0xde`, eux-mêmes par sous-zone, par un autre pool, donc une autre
    table ;
  - un effet dessine des planches dont chaque image désigne sa texture par une **entrée de la table
    de décodage `0x005e8f00`**. Les numéros de ces entrées sont écrits dans le binaire, et, dans la
    tour, ils désignent les textures de la tour.

  Il faut donc, pour chaque effet greffé :
  1. traduire les sous-types de toute la descendance d'une créature greffée, dans les trois tables ;
  2. traduire la table de sons `+0xa8` (8.1.3) ;
  3. faire venir les textures de ses planches et des modèles de décor qu'il emploie, puis
     **renvoyer ses entrées de décodage** vers les nôtres.

  Le point 3 demande une couture dans le rendu (`Primitives2d`, `Model3d`) ou une copie traduite des
  planches. Vincent a choisi la couture (option A), en commençant par un outil de mesure.
- **La mesure (8.1.4), faite** : `OpenForesia.Tools effets-boss` écrit
  `Documentation/Etat/EffetsDesBoss.md`. L'outil désassemble chaque IA, suit ses appels et chaque
  naissance d'effet, et descend dans les gestionnaires qu'il trouve. Trois faits en sortent :
  - **les sous-types par sous-zone se lisent dans trois catalogues**, `+0xcc`, `+0xd0` et `+0xd4`,
    et pas seulement `+0xcc` ;
  - **toutes les planches et tous les modèles des effets désignent des textures communes** (entrées
    `0x10` de l'état 3 et `0x30` du chargement global). Aucune texture de zone, aucun modèle de décor
    propre à une sous-zone. Même les anneaux du souffle de l'ours emploient les modèles 7 et 48,
    communs à l'état 3, et non des modèles de 3/5 comme la mesure à la main le disait ;
  - **le seul champ du répertoire lu en dehors des catalogues est `+0xa8`**, la table de sons.

  **La couture du rendu n'est donc pas nécessaire en l'état.** Greffer un effet, c'est :
  1. mettre son gestionnaire dans un créneau libre de la table de la tour ;
  2. traduire son sous-type, pour toute la descendance d'une créature greffée ;
  3. traduire ses sons `+0xa8`.

- **8.1.2 et 8.1.3, écrits, d'abord pour le souffle de l'ours (2026-09-29).** La mesure a montré
  un piège : le Méchant ours calcule le sous-type de son souffle dans le catalogue de la sous-zone
  **chargée** (`0xc0 + [[+0xcc]][9]`). Dans la tour, cet octet vaut 0, pas le `0x1e` de la forêt :
  à la naissance, le sous-type est donc déjà faux. Au lieu de traduire des sous-types, la greffe
  rend à la lignée d'une créature greffée **le contexte de sa sous-zone** :
  - pendant son gestionnaire, le répertoire de 4/9 désigne les catalogues et la table de sons de la
    sous-zone d'origine ;
  - un effet `>= 0xc0` se résout dans les tables d'origine (`Resources.SubAreaEffectHandler`,
    lecture seule) ;
  - tout effet né pendant ce temps entre dans la lignée, relevé en comparant les emplacements
    vivants des trois pools avant et après l'appel ;
  - un son de niveau d'origine est greffé au banc de 4/9 à sa première demande.

  Trois coutures d'une ligne s'ajoutent : `AvantGestionnaire` et `ApresGestionnaire` (passe de
  scène et `CallIndexedEntityHandler`), et `GestionnaireInstalle`. Aucun créneau de la tour n'est
  pris. Le mécanisme est générique, avec un interrupteur **par espèce** (`Espece.EffetsGreffes`).
  Le souffle de l'ours a été validé en jeu le 2026-09-29, puis l'interrupteur mis à vrai pour
  toutes les espèces (Vincent), ce qui fait naître les effets du Behemoth, du Wybarn et du Fougre.
  Le mettre à faux pour une espèce la ramène au lot 1 : ses effets par sous-zone sont coupés à la
  naissance.

  La mesure est statique et approchée. Un sous-type lu dans l'entité reste « à lire à la main »
  (cinq cas), et la longueur d'une planche n'est pas établie. L'essai en jeu tranchera. Le
  Roi-zombie a une texture de plus (s9, 64 × 256 en 4 bits, lue dans la planche `0x00550460`), peut-être
  un débordement de lecture, à vérifier.

**L'ordre proposé**, du moins cher au plus cher :

- **Sans le travail commun** (effets coupés en lot 1, comme le Behemoth ; **écrits le 2026-09-29**) : Sylphide et Wybarn, le
  gros poilu, Fougre.
- **Après la VRAM par programme et la greffe des effets** : Jeune dragon, Troll, Durahan,
  Loup-garou et Tigre (plus `+0xa8`), Nocturio, Eifridt, Lordémon, l'Ange noir, Cerbère, Dragon
  rouge.
- **En dernier, un à un** : Belzébuth (toute la VRAM, un script), le Dragon zombie (texture de
  96 lignes), le Roi-zombie (quatre textures, des invocations), le Kraaken (plusieurs corps, eau,
  texture 4 bits).

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
