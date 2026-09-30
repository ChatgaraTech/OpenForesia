## 1. La greffe par vague (D2)

- [x] 1.1 `AreneGreffe` : retenir le rang de maillage de chaque espèce à l'installation en mémoire ; ne plus placer ni envoyer de texture au peuplement
- [x] 1.2 `AreneGreffe.PreparerVague` : `Placer` sur les espèces greffées de la vague, maillage relogé si besoin, texture, palette, entrée de décodage, pointeur de la table des maillages ; journal `[GREFFE]` avec les entités de lignée encore vivantes
- [x] 1.3 `AreneDirecteur` : appeler `PreparerVague` juste avant chaque naissance de vague

## 2. Les challenges (D1, D5)

- [x] 2.1 `Programme` : champs `Pause` (500 ms par défaut, en temps réel) et `Challenge` (faux par défaut) ; le délai entre deux vagues lu dans le programme
- [x] 2.2 Les quatre programmes, après les dix-neuf combats : vagues sans escorte, places des couples, bourses 750 / 1 390 / 2 290 / 5 875, pause de 5 000 ms

## 3. Le menu (D3, D4)

- [x] 3.1 `EcranMenu` : `Ligne.NouvellePage`, pagination par débuts de page, gauche / droite de page en page
- [x] 3.2 `EcranMenu` : un nom a toute la largeur quand la colonne de droite est vide
- [x] 3.3 `AreneTeleporteur` : le premier challenge ouvre une page

## 4. Vérification et documentation

- [x] 4.1 `dotnet build` sans erreur
- [x] 4.2 `legacy.md` (LP-014) et `HISTORY.md`
- [x] 4.3 En jeu (Vincent) : un combat seul inchangé ; Boss Rush 1 en entier — l'Enchanteresse au milieu ne déclenche pas la fin ; Boss Rush 3 — Belzébuth après Eifridt, textures justes ; Ultimate — deux Dragons rouges ; la page 3 du menu ; la bourse versée une fois
