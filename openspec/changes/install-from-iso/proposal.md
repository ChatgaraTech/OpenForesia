## Why

Pour jouer, il faut aujourd'hui monter le CD (ou son image) à chaque lancement : le démarrage balaie
les lecteurs à la recherche d'un `DRIVE_CDROM`, exige le fichier-leurre `cd.idx` de 640 Mo, et boucle
sur « insérez le CD » sinon. Le projet passe en mode **« legacy ++ »** (décision du 2026-09-26) : il
doit devenir un jeu **autonome, clé en main**, distribuable en release GitHub — sans jamais livrer les
données du jeu, dont le projet n'a pas les droits. Le joueur fournit son ISO ; un installeur en extrait
ce qu'il faut.

## What Changes

- **BREAKING** — le jeu ne connaît plus de CD. Suppression du balayage des lecteurs C..Z (et du repli
  X / Z / P), du message « insérez le CD », de la lecture de `cd.idx` et du multiplicateur qu'il
  alimente, de la lettre de lecteur substituée dans les chemins, de `CdSearchFile` (ordinal 92) et de la
  branche `cdrom:` des descripteurs de carte mémoire.
- Le jeu lit ses données **dans le dossier de l'exécutable**, qui devient aussi le répertoire
  courant ; tous ses chemins (données, `save\`, `OpenForesia.cfg`) sont relatifs — donc `save\` est
  enfin toujours à côté de l'exe. En développement, les données s'extraient dans le dossier de
  sortie de la build.
- **Vérification au démarrage** : la liste des fichiers attendus (nom et taille) est contrôlée avant
  toute création de fenêtre ; s'il en manque, un message en français le dit et le jeu s'arrête.
- **Nouvel installeur** `OpenForesia-Setup.exe` : choisir l'ISO, la reconnaître, choisir le dossier,
  extraire les ≈ 280 Mo utiles (et non les 709 Mo de l'image), y déposer le jeu publié autonome,
  créer `save\`, les raccourcis bureau et menu Démarrer, et une désinstallation propre.
- La lecture de l'ISO (ISO 9660 / Joliet) vit **dans l'installeur**, qui sert aussi, par un mode
  caché, à générer la liste des fichiers attendus depuis l'ISO de référence. Les outils (`bestiaire`,
  `indeo`, scripts de films) lisent le **dossier installé** et ne supposent plus `E:\` ; ils ne lisent
  pas l'ISO.
- Les règles du projet (`CLAUDE.md`, `Strategy.md`, `legacy-bug.md`) sont réécrites pour le mode
  « legacy ++ » : un écart qui répare un défaut ou retire une dépendance au matériel de 1998 est
  légitime, à condition d'être déclaré.

## Capabilities

### New Capabilities

- `game-data-install` : l'installation depuis l'ISO du joueur — reconnaissance de l'édition,
  extraction sélective et vérifiée, dossier d'installation, raccourcis, désinstallation.
- `game-data-location` : où et comment le jeu trouve ses données et ses sauvegardes à l'exécution,
  et ce qu'il fait quand elles manquent — sans aucune notion de CD.

### Modified Capabilities

- `blaze-intro-attract` : l'exigence « le démarrage atteint toujours l'initialisation du moteur, CD ou
  pas » change — sans données, le démarrage s'arrête désormais sur un message avant l'initialisation ;
  les références à la « racine du CD configurée » deviennent le dossier des données.

## Impact

- **Code du jeu** : `Platform/Startup.cs` (`RunApplication`), `Memory/GameMemory.cs`
  (`s_CdDataRootPath`, `s_CdCheckFileSize_0056321c`), `Engine/Resources.cs` (`BuildCdPath`,
  `BuildCdRootPath`), `Engine/UiText.cs` (le multiplicateur), `Engine/Video.cs`,
  `Screens/State00/MainMenu.cs` (table des musiques), `Screens/State03/WorldMap.cs`,
  `Libraries/winsys3d/Cdrom.cs` et `CarteMemoire.cs`, `Interop/Winsys3d.cs`.
- **Nouveau projet** : `OpenForesia.Installer`, ajouté à `OpenForesia.sln`, qui porte aussi le
  lecteur d'ISO et `editions.json`.
- **Outillage** : `OpenForesia.Tools` (verbes `bestiaire`, `indeo`), `build-bestiaire-page.ps1`,
  `check-movie-audio.ps1`, `decode-movie.ps1`, `playtest.ps1` et `test-protocol.md` (le dossier de
  données en développement, et `save\` qui change de place).
- **Sondes de couverture** (`CoverageProbes.cs`) : elles écrivent dans le dépôt, relatif à
  l'exécutable ; elles doivent rester muettes dans une build publiée.
- **Documentation** : `CLAUDE.md`, `Strategy.md`, `legacy-bug.md`, `OpenForesia.cfg.md`,
  `DataSymbols.md` (`cd.idx`), `ARCHITECTURE.md` (nouveaux projets).
- **Distribution** : première release publique ; l'exécutable n'est pas signé (SmartScreen avertira).
