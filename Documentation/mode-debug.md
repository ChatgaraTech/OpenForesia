# Le mode développeur (« Gurkha ») et son menu de mise au point

Ce que le jeu de 1998 garde derrière le mot « Gurkha », décrit depuis la reconstruction. Tout ce qui
suit est lu dans le code transcrit — `Screens/State03/WorldMapDebugMenu.cs` (`FUN_0051aa30`),
`Screens/DebugParty.cs` (`FUN_00536270`) et les lecteurs du verrou `DAT_005633a8` — et non deviné.
Le sens exact de la plupart des bits des banques n'est **pas** établi.

## L'activer

Il suffit de faire **afficher** le mot « Gurkha » à l'écran : la sonde `CheatCodeProbe`
(`FUN_0051c0e0`) suit les caractères dessinés, pas la saisie. Nommer un personnage « Gurkha »
suffit. Le verrou est à sens unique : une fois tombé, le mode reste actif jusqu'à la fermeture du
jeu.

La recette complète, relevée sur le jeu officiel (détails dans `Tests/test-protocol.md`) :

1. créer un personnage nommé **Gurkha** et le sauvegarder ;
2. choisir **« Constituer un groupe »** ;
3. **maintenir F1** (SELECT) pendant le chargement qui suit ;
4. l'écran de groupe de mise au point apparaît ; après le choix, la mappemonde est entièrement
   débloquée.

Voir aussi `Etat/NOTE_DEVELOPPEUR.md`, sections 4 et 5.

## Les touches

| Manette | Clavier |
|---|---|
| **Carré** | Ctrl gauche / Entrée |
| **Croix** | **Échap** (Alt gauche aussi, mais Alt fait perdre le focus) |
| **SELECT** | F1 |
| **START** | F2 |
| **Rond** | Maj gauche |
| **Triangle** | Espace |
| Directions | flèches |

Sur la carte, **F1 bascule l'affichage du menu** (bit 1 de `DAT_005de02c`). ↑/↓ choisit la ligne,
**sauf quand Carré est maintenu** : Carré tenu fige la sélection et fait passer la ligne en mode
édition.

## Les treize lignes du menu

| # | Ligne | Effet |
|---|---|---|
| 0 | **QUEST** | ←/→ choisit une zone (`DAT_005dbc14`). **Carré** : s'y téléporter, en sous-zone 0. |
| 1 | **MAP** | ←/→ choisit une sous-zone (`DAT_005ddfd0`). **Carré** : s'y téléporter, toujours dans la zone courante. |
| 2 | **PLAYER** | ←/→ : taille du groupe, 1 à 4. **Ne marche qu'avec des personnages déjà chargés**, ou avec le groupe de mise au point : un membre ajouté reprend la place du meneur sur la carte, mais n'a pas de fiche (0 PV), et le jeu plante en dessinant sa jauge — défaut d'origine, `Reference/legacy-bug.md` LB-049. |
| 3 | **FLAG** | Banque `0x005e96e0`, 64 octets de variables de scénario. ←/→ choisit l'octet. **Carré tenu** : ←/→ déplace le curseur `v` sur les bits, ↑ lève le bit, ↓ le baisse. **Croix** : les 64 octets à zéro. |
| 4 | **WORK** | Banque `0x005e9780`, même maniement que FLAG. |
| 5 | **TEMP** | Banque `0x005e9060`, mais **par octet entier** : Carré tenu + ↑/↓ change la valeur, affichée en décimal. Croix : remise à zéro. |
| 6 | **ITEM** | Banque `0x005e9840`, même maniement que FLAG. |
| 7 | **SYSTEM** | `0x005e9000`, 32 octets de progression **rangés dans chaque personnage** (fiche `+0x2e0`) : chaque écriture est recopiée dans la fiche de chaque membre du groupe. L'octet **30** (`0x1e`) décide des destinations débloquées sur la mappemonde ; les bits 0 à 9 d'un autre octet sont les cristaux de la « Table ». |
| 8 | **TEX** | Visionneuse : affiche une page de texture en 256×224 (pages 4–15 et 20–31). ←/→ change de page, Carré tenu + flèches fait défiler. |
| 9 | **REBOOT** | Carré : état de jeu 10. |
| 10 | **MENU** | Carré : retour à l'état de jeu 1 (composition du groupe). |
| 11 | **MONITOR** | → allume, ← éteint l'affichage de mise au point du combat (voir plus bas). |
| 12 | **FONT** | → / ← bascule `DAT_00566be0`, que **rien ne lit** dans `Blaze.c`. Sans effet connu. |

**Ne pas se téléporter (QUEST / MAP) pendant une scénette** — le wagonnet de la grotte scellée, une
caméra qui suit un boss, une porte qui s'ouvre. Le curseur de la scène (`DAT_005719b4`) n'est remis
à zéro qu'à la fin de la scène (verbe 0) ou par `FUN_005035c0` ; aucun chargement de zone n'y
touche. Après la téléportation il pointe donc dans les données de l'**ancienne** zone, que le
chargement a remplacées, et lit n'importe quoi : le 2026-09-25, sur le wagonnet, un « verbe 32 »,
qui n'existe pas (la table `0x0055eb68` en a 32, de 0 à 31 ; sa 33ᵉ case, `0x00504c80`, est un
doublon de l'entrée de l'état de jeu 5, pas un verbe). La reconstruction lève
`NotImplementedException` ; le jeu d'origine, d'après le binaire, sauterait dans ce code — **non
vérifié** sur le jeu d'origine. Attendre la fin de la scène avant de se téléporter.

Les quatre noms FLAG, WORK, TEMP, ITEM sont ceux du développeur : ce sont les banques que les
opcodes de script 4 à 7 manipulent, et quatre des huit banques de 64 octets que la sauvegarde
recopie. **Attention** : Croix efface la banque entière sans confirmation, et lever des bits au
hasard peut casser un scénario.

## Ce que le mode change hors du menu

- **Le groupe de mise au point** (maintenir F1 à l'entrée de l'état 1) : deux colonnes
  « MALE / FEMALE » et les huit classes, Carré ajoute un membre, Croix le retire. Chaque membre naît
  avec **9000 PV et 200 PM**, les autres statistiques venant d'une table par classe. Il connaît
  d'office les **32 premiers sorts de chaque école** (les quatre premiers octets de `+0x160`, `+0x168` et `+0x170` à `0xff`), et un Chasseur
  part avec 11 à 77 exemplaires de chacune des sept herbes.
- **Menu de jeu, premier onglet, valider un membre** : PV et PM remis au maximum, états effacés, et
  le membre prend la tête du groupe — y compris s'il est mort. **Un soin complet à volonté.**
- **Sorts** : ouvrir une page de sorts en mode développeur **fait apprendre tous les sorts de la
  page** au personnage. Dans la liste d'objets/sorts, tout ce qui est listé est aussi marqué
  disponible.
- **Fiche de personnage** : quatre valeurs de plus sont affichées, les rangs 4 à 7 de `+0x130`.
- **Déplacement** : Maj gauche (Rond) maintenu donne une marche rapide (`0x3c000`) ; F1 maintenu
  pendant un saut en allonge fortement la portée.
- **Bandeau de combat** : le nom et les PV de la cible s'affichent à chaque coup — pratique pour
  suivre les PV d'un boss.
- **MONITOR allumé** : les zones de coup sont tracées en étincelles, et chaque coup affiche
  attaque, défense et dégâts.
- **Écran de sélection des personnages** : pilotage libre de la scène (rotation, couleur de fond).

## Ce que le mode ne fait pas

**Il n'y a ni invincibilité ni coup mortel dans le jeu de 1998.** Contre un boss, le plus efficace
est de combiner le groupe de mise au point (9000 PV), le soin à volonté par le premier onglet du
menu, et une classe de lanceur de sorts (Sorcerer, Priest), qui connaît alors tous ses sorts avec
200 PM. Les dégâts restent toutefois ceux du niveau de départ — le Dragon rouge a 8 000 PV.

Aucun échafaudage de type « tuer en un coup » n'a été ajouté, volontairement : il fausserait les
tests.

**Pour muscler un personnage, passer par sa sauvegarde, pas par le jeu** : l'éditeur de sauvegarde
(`OpenForesia.Tools/Html/editeur-sauvegarde.html`, raccourci `testsediteur-sauvegarde.bat`) change
les caractéristiques, le niveau, les PV/PM, l'or, l'inventaire, l'équipement et les sorts sans
toucher au code. Le format du fichier est dans `Reference/FormatSauvegarde.md`.
