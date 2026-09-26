## 1. Réglages

- [x] 1.1 Créer `Host/DisplaySettings.cs` : lecture unique de `Affichage.ini` (dossier de l'exécutable, UTF-8, sections `[Affichage]`/`[Textures]`, commentaires `;`), validation clé par clé, défauts de `design.md` D1, une ligne de journal par valeur refusée
- [x] 1.2 L'appeler au démarrage avant `CreateGameWindow` ; vérifier qu'aucun chemin n'écrit le fichier
- [x] 1.3 Écrire `Documentation/Reference/Affichage.ini.md` (clés, valeurs, défauts, les trois lignes du rendu d'origine, la limite du tremblement PSX)

## 2. Décision de filtrage du moteur (à valider avant le shader)

- [x] 2.1 Brancher le slot `+0x90` dans `SyntheticDevice.cs` (thiscall, `uint`) sur un thunk qui mémorise la classe de liaison (interface / 3D non filtrée / 3D filtrée) d'après la valeur reçue et `FiltreTexture_00a2c508` (design D4)
- [x] 2.2 Journal de mise au point derrière une variable `OPENFORESIA_TRACE_FILTER=1` : compte des liaisons par classe et par page de texture
- [x] 2.3 Vincent : une partie courte (titre, menu, sélection, auberge, forêt) avec la trace ; confirmer que la classe « interface » recouvre menus, polices et fenêtres et presque pas la 3D — sinon réviser le critère de D4 avant la suite
- [x] 2.4 Vincent : seconde trace avec le critère révisé (genre sprite / polygone, design D4) — textes, icônes et cadres en « Interface », décor et personnages côté 3D

## 3. Résolution interne et MSAA

- [x] 3.1 Séparer taille logique et taille physique dans `D3D11Backend` : `InvTargetSize` reste logique, viewport de scène physique ; recenser tout autre état en pixels physiques
- [x] 3.2 Facteur entier `ResolutionInterne` dans `EnsureSceneTarget`, y compris quand le jeu change sa taille (F11/F12)
- [x] 3.3 Mode `auto` : taille physique = rectangle de destination de `PresentView` ; recréation de la cible au redimensionnement de la fenêtre
- [x] 3.4 MSAA : cible couleur et profondeur multi-échantillonnées, `CheckMultisampleQualityLevels` et repli signalé une fois, `ResolveSubresource` avant présentation ; chemin inchangé à 1
- [x] 3.5 Mettre à jour `SelfTest` pour qu'il exerce un facteur 2 et le MSAA

## 4. Mise à l'échelle finale

- [x] 4.1 `PresentView` : échantillonneur linéaire en `ajustee`, facteur entier et point en `entiere`, repli si le facteur vaut 0 ; présentation 1:1 en `auto`
- [x] 4.2 Appliquer la même règle aux films (`PresentImage`)

## 5. Filtrage des textures

- [x] 5.1 Sommet de l'hôte : ajouter `uvMin`/`uvMax` (`nointerpolation`), calculés par triangle au dépliage de `DrawPrimitive` ; mettre à jour la disposition d'entrée
- [x] 5.2 Shader de pixels : chemin filtré manuel (4 `Load` bornés, poids des texels opaques, renormalisation, `discard` sous 0,5) derrière une constante `Filtered` ; chemin proche inchangé
- [x] 5.3 Choisir `Filtered` par lot d'après la classe de liaison (tâche 2.1) et `Filtrage3D`/`FiltrageInterface` (tableau de D4)
- [x] 5.4 Vérifier que `ResolutionInterne = 1`, `Anticrenelage = 1`, `Filtrage3D = proche` suit exactement le code d'avant (lecture du chemin, aucun calcul nouveau sur ce chemin)

## 6. Installeur

- [x] 6.1 Embarquer le modèle `Affichage.ini` commenté dans `OpenForesia.Installer` ; l'écrire après extraction s'il n'existe pas ; ne jamais l'écraser
- [x] 6.2 Vérifier que `publish-release.ps1` passe sans avertissement de trim et que le contrôle « aucune donnée du jeu » reste vert

## 7. Vérification (Vincent, en jeu)

- [x] 7.1 Rendu fidèle : captures avec les trois réglages d'origine comparées à celles d'avant le changement (titre, menu, sélection, auberge, forêt, un combat) — identique, constaté par Vincent le 2026-09-26
- [x] 7.2 Défauts : même jeu d'écrans en `auto`/MSAA 4/`jeu`/`net` sur l'écran 1440p ; noter toute fente, bavure de page ou halo — menus, donjon et combat sans défaut, constaté par Vincent le 2026-09-26
- [x] 7.3 `entiere` donne des pixels réguliers (image centrée, bandes noires) ; un `Affichage.ini` invalide ne bloque pas le démarrage (valeur refusée et clé inconnue signalées) ; F10 ne fait rien — inerte dès 1998 (`DAT_00dedc40` jamais posé), spec et doc corrigées
- [x] 7.4 Installer puis réinstaller : le fichier par défaut apparaît, puis un réglage modifié survit — constaté par Vincent le 2026-09-26

## 8. Documentation et clôture

- [x] 8.1 Entrée `LB-055` dans `legacy-bug.md` (confort, `Contourné`, renvoi à ce changement et à `Affichage.ini.md`)
- [x] 8.2 `test-protocol.md` : où poser `Affichage.ini` pour un essai, et le jeu d'écrans de comparaison
- [x] 8.3 `CLAUDE.md` (ligne `Host/` et « The game's data ») et `HISTORY.md`
