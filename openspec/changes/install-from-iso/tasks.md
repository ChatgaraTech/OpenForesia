## 1. L'installeur : projet, lecteur d'ISO et liste d'édition

- [ ] 1.1 Créer `OpenForesia.Installer` (WinForms, net10.0-windows, x86, fichier unique autonome) et l'ajouter à `OpenForesia.sln` ; le lecteur d'ISO dans son sous-dossier `Iso/`
- [ ] 1.2 Lire les descripteurs de volume (primaire + Joliet), l'étiquette et la taille en secteurs ; refuser une image dont la taille n'est pas un multiple de 2048 ou sans `CD001` au secteur 16
- [ ] 1.3 Parcourir l'arborescence et lire un fichier par secteurs, en flux (blocs d'1 Mo)
- [ ] 1.4 Couper un fichier RIFF à `8 + taille RIFF` quand l'entrée de répertoire déclare davantage
- [ ] 1.5 Mode caché `--generer-edition <iso> <sortie>` qui produit `editions.json` (étiquette, MD5 de `BLAZE.ALL`, et par fichier : chemin ISO, chemin installé, taille installée, MD5, coupe RIFF) selon la sélection de `design.md` D4 ; le fichier vit dans `OpenForesia.Installer/Editions/`
- [ ] 1.6 Générer `editions.json` depuis l'ISO de référence et vérifier à la main : ≈ 130 fichiers, ≈ 280 Mo, aucun `CD.IDX` / `FRENCH2` / `DIRECTX` / `DATA\` racine / `.EXE` / `.DLL`, `OPENING.AVI` à 28 101 316 et `ANI01.AVI` à 31 216 900
- [ ] 1.7 Mode caché `--extraire-donnees <iso> <dossier>` (sans interface : extraction + vérification MD5) ; s'en servir pour la première installation de développement, qui sert au lot 2

## 2. Le jeu sans CD

- [ ] 2.1 Lire la clé `[Donnees] Racine` de `OpenForesia.cfg` ; à défaut, prendre le dossier de l'exe ; en faire le répertoire courant au tout début de `RunApplication`
- [ ] 2.2 Embarquer `editions.json` et vérifier existence + taille de chaque fichier avant toute fenêtre ; en cas d'échec, un seul message en français (nombre, premier fichier, « réinstallez ») puis sortie avec code non nul
- [ ] 2.3 Supprimer le balayage des lecteurs, les replis X / Z / P, la boucle « insérez le CD » et `s_PleaseInsertCdMessage` (garder une ligne `// Ghidra:` qui dit ce qu'il y avait)
- [ ] 2.4 Supprimer la lecture de `cd.idx`, `s_CdCheckFileSize_0056321c` et ses deux écritures ; `UiText.cs:672` rend `text + 3`
- [ ] 2.5 Rendre les chemins relatifs : `s_CdDataRootPath` → `"FRENCH\"`, `@:\snd\Base.snd` → `SND\BASE.SND`, la table des musiques de `MainMenu.cs` → `MUSIC\…` ; supprimer `BuildCdRootPath` et les recopies de lettre (`Video.cs`, `MainMenu.cs`, `WorldMap.cs`, `Startup.cs:615`)
- [ ] 2.6 Supprimer l'appel à `CdSearchFile` (ordinal 92), `Libraries/winsys3d/Cdrom.cs` et la branche `cdrom:` de `CarteMemoire.cs` ; mettre à jour `DLLAtteignables.md` (`sh OpenForesia.Tools/census-dll-surface.sh`)
- [ ] 2.7 Vérifier que `grep "@:"` sur `OpenForesia/src/` ne rend plus que des commentaires
- [ ] 2.8 Désarmer sondes, `AiBench` et captures `scratchpad` hors dépôt (marqueur `OpenForesia.sln` absent)
- [ ] 2.9 Build, `dotnet test OpenForesia.Tests` (les rouges volontaires restent rouges), puis Vincent lance le jeu depuis un dossier installé, sans CD monté, et depuis un chemin accentué

## 3. Outils et protocole de test

- [ ] 3.1 `bestiaire` et `indeo` lisent le dossier des données installé (paramètre, ou la clé `[Donnees] Racine` du `.cfg`) ; plus de `E:\` par défaut
- [ ] 3.2 Mettre à jour `build-bestiaire-page.ps1`, `check-movie-audio.ps1`, `decode-movie.ps1` et `MonteeDeNiveau/Simulateur.cs`
- [ ] 3.3 `playtest.ps1` : dossier de données en paramètre ou lu dans le `.cfg` ; `save\` au nouvel endroit
- [ ] 3.4 Réécrire les passages CD de `test-protocol.md` et `OpenForesia.cfg.md` (la section `[Donnees]`, `save\` qui suit le dossier des données)

## 4. L'installeur

- [ ] 4.2 Étape ISO : choix du fichier, reconnaissance d'édition, messages de refus (format, édition inconnue)
- [ ] 4.3 Étape dossier : défaut `%LOCALAPPDATA%\Programs\OpenForesia`, avertissement sous `Program Files`, refus au-delà de 200 caractères, contrôle de l'espace libre
- [ ] 4.4 Extraction avec barre de progression par octets et annulation ; vérification MD5 ; nettoyage complet en cas d'échec ou d'annulation
- [ ] 4.5 Dépôt du jeu embarqué (zip du `dotnet publish` autonome x86) et création de `save\`
- [ ] 4.6 Raccourcis bureau et menu Démarrer (`IShellLinkW`), entrée « Désinstaller » dans le menu Démarrer
- [ ] 4.7 Entrée `HKCU\…\Uninstall\OpenForesia` (nom, version, éditeur, icône, taille, commande de désinstallation)
- [ ] 4.8 Désinstallation : recopie dans `%TEMP%`, question « garder les sauvegardes ? », suppression des fichiers, raccourcis et clé
- [ ] 4.9 Libellés en français, sans jargon (règle des outils pour joueurs)

## 5. Release

- [ ] 5.1 `OpenForesia.Tools/publish-release.ps1` (BOM UTF-8) : publie le jeu, le zippe, publie l'installeur qui l'embarque, sort `OpenForesia-Setup.exe`
- [ ] 5.2 Contrôler que la release ne contient aucune donnée du jeu (seulement noms, tailles et sommes dans `editions.json`)
- [ ] 5.3 Vincent déroule le parcours complet sur une machine propre (ou un compte Windows neuf) : installation, lancement, sauvegarde, désinstallation en gardant puis sans garder `save\`

## 6. Règles « legacy ++ » et documentation

- [x] 6.1 Réécrire dans `CLAUDE.md` et `Strategy.md` la règle « la fidélité avant tout » : un écart qui répare un défaut ou retire une dépendance au matériel de 1998 est légitime s'il est déclaré
- [x] 6.2a `legacy-bug.md` : en-tête réécrit (statuts Reproduit / Contourné / Corrigé / Retiré), ancienne règle historisée
- [ ] 6.2 `legacy-bug.md` : ajouter l'entrée « protection `cd.idx` retirée », avec l'arithmétique d'origine
- [ ] 6.3 `DataSymbols.md` (`0x00563280`, `0x005631f8`), `ARCHITECTURE.md` (les deux nouveaux projets), `REVERSE_STATUS.md`, `CLAUDE.md` (section Layout et « Build and run »)
- [ ] 6.4 `HISTORY.md`
