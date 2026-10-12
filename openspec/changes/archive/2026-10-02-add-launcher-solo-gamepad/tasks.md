## 1. Lot 1 — Le renommage, à comportement constant (design D1)

- [x] 1.1 `OpenForesia.Multiplayer/` → `OpenForesia.Lanceur/` : dossier, csproj, `AssemblyName` `OpenForesiaLanceur`, espace de noms `OpenForesia.Lanceur`, entrée de `OpenForesia.sln` ; `dotnet build OpenForesia.sln` sans nouvel avertissement
- [x] 1.2 Titre de l'accueil « OpenForesia », en-tête de `Program.cs`, message de mode inconnu
- [x] 1.3 `publish-release.ps1` : projet, exe attendus (`OpenForesia.exe`, `OpenForesiaLanceur.exe`) ; publication élaguée sans avertissement
- [x] 1.4 Essai (Vincent) : Héberger et Rejoindre comme avant, avec le nouvel exe

## 2. Lot 2 — Les touches fantômes du joueur 1 (design D2)

- [ ] 2.1 Relancer `--mesurer-touches` ; choisir les quatorze codes qui suivent les 42 retenus, dans l'ordre du rapport ; les consigner dans `Documentation/Reference/OpenForesia.cfg.md` — codes choisis et consignés le 2026-10-02 (`MapVirtualKeyEx`, sans frappe) ; reste la mesure de confirmation, à faire par Vincent
- [x] 2.2 `ConfigTouches` : codes du joueur 1, écriture de sa **table 1** seule, liste des joueurs à écrire en paramètre ; sauvegarde `OpenForesia.cfg.lanceur`
- [x] 2.3 `ConfigTouches.Remettre` lit aussi l'ancien `OpenForesia.cfg.multijoueur` ; appelé au démarrage du lanceur quel que soit le mode (design D6)
- [x] 2.4 `Injection.Appliquer` et `CodePour` acceptent le joueur 1

## 3. Lot 3 — Le relais local du joueur 1 (design D3, D4)

- [x] 3.1 Le relais : un fil toutes les 4 ms, `Entrees.Relever` sans clavier, `ConfigManette.Mot`, mot nul si l'écran de configuration est ouvert ou si le jeu n'est pas au premier plan, `Injection.Appliquer(1, …)` ; tout relâché à l'arrêt
- [x] 3.2 Mode `--solo [--jeu <dossier>]` en console : écrit les codes du joueur 1, lance le jeu, relaie jusqu'à sa fermeture, remet le cfg après la fin du processus
- [x] 3.3 Essai (Vincent) : `--solo` avec sa manette — directions, croix directionnelle, quatre boutons, gâchettes, Start/Select ; clavier principal en même temps ; Alt+Tab relâche tout

## 4. Lot 4 — Les fenêtres (design D5)

- [x] 4.1 Accueil : Jouer seul, Héberger, Rejoindre, Configurer ma manette, ligne d'état de la manette
- [x] 4.1b Accueil réduit sans `OpenForesia.exe` à côté (design D8) : Rejoindre et Configurer ma manette seuls, `--invite-seul` pour le forcer dans le dépôt
- [x] 4.2 Fenêtre de partie solo : état de la partie et du relais, bouton « Configurer ma manette », fermeture avec le jeu ; la fermer arrête le relais sans remettre le cfg
- [x] 4.3 `EcranManette` ouvert depuis le lanceur, libellé « Fermer », relais au mot nul tant qu'il est ouvert, changement pris en compte sans relancer le jeu
- [x] 4.4 Hôte : relais du joueur 1 dans `Session` (codes des joueurs 1 à 4 écrits), bouton « Configurer ma manette » et état du joueur 1 dans la fenêtre de l'hôte
- [x] 4.5 Essai (Vincent) : partie solo depuis l'accueil, reconfiguration en pleine partie ; hébergement avec manette du joueur 1 et un invité

## 5. Lot 5 — L'installeur et la publication (design D7)

- [x] 5.1 `Installation.cs` : déposer `OpenForesiaLanceur.exe`, raccourcis « OpenForesia » (le jeu) et « OpenForesia Lanceur », retirer `OpenForesiaMultiplayer.exe` et les raccourcis « OpenForesia Multijoueur » d'une version précédente
- [x] 5.2 `Desinstallation.cs` : retirer les deux noms d'exe et de raccourci du lanceur
- [x] 5.3 `publish-release.ps1` : le lanceur aussi seul à côté du Setup, contrôle « aucune donnée du jeu » sur les deux ; taille de chaque fichier affichée
- [ ] 5.4 Essai (Vincent) : mise à jour d'une installation 0.3.0, puis désinstallation propre
- [ ] 5.5 Essai (Vincent) : le lanceur seul, copié dans un dossier vide d'un autre PC, rejoint une partie

## 6. Lot 6 — Documentation

- [x] 6.1 LISEZMOI du lanceur : les quatre choix, le relais du joueur 1, le clavier de secours coupé pendant une session, la remise du cfg
- [x] 6.2 `CLAUDE.md` : la ligne `OpenForesia.Multiplayer/` devient `OpenForesia.Lanceur/`, la mention dans « The game's data » (publication)
- [ ] 6.3 Quand `add-multiplayer-streaming` est archivé : mettre à jour l'exigence « One program hosts and joins » de `openspec/specs/multiplayer-streaming` (nom du programme)
- [x] 6.4 `HISTORY.md`
