## 1. Réglages

- [x] 1.1 `Host/DisplaySettings.cs` : retirer `ResolutionInterne`, `Anticrenelage`, `Filtrage3D`, `FiltrageInterface` et les enums `Texture3dFilter`/`InterfaceFilter` ; ajouter `Rendu` (`moderne`/`origine`), `Mode` (`fenetre`/`fenetre-sans-bordure`/`plein-ecran`), `Fenetre` (`auto` ou `LxH`, entiers positifs) ; garder `MiseAEchelle`
- [x] 1.2 Reconnaître les quatre clés retirées : une ligne « clé retirée, ignorée » chacune, distincte de « clé inconnue » (design D4)
- [x] 1.3 Exposer les propriétés dérivées de D2 (rendu à la taille affichée, nombre d'échantillons voulu, filtrage des polygones) pour que les consommateurs ne lisent plus `Rendu` directement ; mettre à jour `Describe()`

## 2. Rendu

- [x] 2.1 `D3D11Backend.PhysicalTargetSize` : taille affichée en `moderne`, taille logique en `origine` ; `HandleResize` marque la cible sale en `moderne` seulement
- [x] 2.2 `EnsureSceneTarget` : MSAA 4 en `moderne` (repli existant conservé), 1 en `origine`
- [x] 2.3 `DeviceShim.CurrentBindFiltered` : `origine` → jamais ; `moderne` → seulement `Filtered3d` (design D3)
- [x] 2.4 Relire que `Rendu = origine` suit exactement le chemin d'avant LB-055 : cible logique, 1 échantillon, `Filtered = 0`, `ClampNearest = 0` (aucun calcul nouveau sur ce chemin)
- [x] 2.5 `SelfTest` : l'adapter s'il citait les anciennes clés ou le facteur entier

## 3. Fenêtre

- [x] 3.1 Calcul de la taille `Fenetre` (design D7) : `auto` = plus grand multiple entier de la taille logique qui tienne dans la zone de travail (cadre compris en `fenetre`), taille explicite réduite proportionnellement si trop grande, une ligne de journal
- [x] 3.2 `DeviceShim.CreateGameWindow` : créer à la taille `Fenetre`, `SavePlacement`, puis appliquer `Mode` (design D5)
- [x] 3.3 `D3D11Window` : mode `fenetre-sans-bordure` (retrait du cadre, zone cliente conservée, centré sur le moniteur) ; `plein-ecran` par le chemin de `ToggleFullscreen`
- [x] 3.4 Alt+Entrée : bascule entre le mode de départ et `plein-ecran` ; depuis un départ `plein-ecran`, retour en `fenetre` à la taille `Fenetre`
- [x] 3.5 `RestorePlacement` : en `plein-ecran` et `fenetre-sans-bordure`, ré-ajuster au rectangle attendu au lieu de ne rien faire (design D6)
- [x] 3.6 Après essai : `Fenetre = auto` = plus grand 4:3 (le multiple entier seulement en `entiere`) ; `fenetre-sans-bordure` mesurée sur l'écran entier, `auto` = tout l'écran (design D7)
- [x] 3.7 `plein-ecran` exclusif (design D9) : `SetFullscreenState` depuis `PresentView`, repris au retour du focus (`WM_ACTIVATEAPP`), quitté avant le retour d'Alt+Entrée et avant `Shutdown`

## 4. Installeur et documentation

- [x] 4.1 Réécrire `OpenForesia.Installer/Reglages/Affichage.ini` : `[Affichage]` avec `Rendu`, `Mode`, `Fenetre`, `MiseAEchelle`, commentaires pour un joueur ; plus de section `[Textures]`
- [x] 4.2 Réécrire `Documentation/Reference/Affichage.ini.md` ; mettre à jour LB-055 dans `legacy-bug.md` (interrupteurs), la recette de comparaison dans `test-protocol.md` et `CLAUDE.md` (`Rendu = origine`)
- [x] 4.3 `dotnet build OpenForesia.sln` sans erreur ; `publish-release.ps1` sans avertissement de trim, contrôle « aucune donnée du jeu » vert
- [x] 4.4 `HISTORY.md`

## 5. Vérification (Vincent, en jeu)

- [x] 5.1 Réinstaller depuis le Setup **fraîchement publié** (celui du 2026-09-26 16:11 précède les correctifs des traits) et comparer Debug et installé : plus de traits sous les textes
- [ ] 5.2 Ancien `Affichage.ini` (clés ×8) : le jeu démarre, le journal cite les clés retirées, l'image est celle des défauts
- [ ] 5.3 `Rendu = origine` : capture identique à la référence d'avant LB-055
- [x] 5.4 Les trois modes au démarrage, écran-titre puis menu puis jeu : pas de déplacement durable de la fenêtre au passage titre → menu ; Alt+Entrée aller-retour dans chaque mode
- [x] 5.6 Second écran : en `fenetre-sans-bordure`, travailler sur l'autre écran (bestiaire) laisse le jeu en place ; en `plein-ecran`, cliquer ailleurs quitte l'exclusif et revenir le reprend, sans perte d'image ni plantage ; définition du bureau conservée
- [ ] 5.5 `Fenetre = auto` et une taille explicite ; trancher le défaut de `Mode` (question ouverte de `design.md`)
