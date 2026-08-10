## 1. Dépendance et faisabilité

- [x] 1.1 Ajouter `Silk.NET.Direct3D11`, `Silk.NET.DXGI` et `Silk.NET.Direct3D.Compilers` (2.23.0)
- [x] 1.2 Sonde : créer un device Direct3D 11 dans le processus 32 bits et journaliser le feature
      level obtenu (résultat : 11_0)
- [x] 1.3 Retirer l'appel de sonde de `Program.cs` une fois la fenêtre en place

## 2. Fenêtre, device et présentation

- [x] 2.1 Créer la fenêtre du jeu (classe, procédure de fenêtre, pompe de messages) sans la lier à
      DirectDraw
- [x] 2.2 Créer le device Direct3D 11 et une swap chain DXGI **en mode fenêtré** sur cette fenêtre
- [x] 2.3 Créer la cible de rendu hors écran et le tampon de profondeur aux dimensions demandées par
      le jeu, et les recréer quand ces dimensions changent
- [x] 2.4 Écrire les deux shaders : sommets pré-transformés vers coordonnées normalisées, pixels
      texture modulée par la couleur diffuse
- [x] 2.5 Présenter la cible de rendu mise à l'échelle, rapport d'aspect préservé, image centrée et
      bandes unies
- [x] 2.6 Vérifier de bout en bout avec une image de test, `3dhelp` encore en place

## 3. Portage du chemin chaud

- [x] 3.1 Effacement de la cible de couleur (slot `+0x5C`) et du tampon de profondeur (`+0x60`)
- [x] 3.2 Début et fin de scène (`+0xB0`, `+0xB4`)
- [x] 3.3 `DrawPrimitive` (`+0xBC`) — traduction de `D3DTLVERTEX` vers le format de sommet du
      backend, sans transformation
- [x] 3.4 Mode d'ombrage (`+0x8C`) — plat et Gouraud
- [ ] 3.5 Liaison de texture (`+0x64`), création et conversion des textures depuis le format du jeu
- [x] 3.6 Présentation branchée sur la swap chain — **à `EndScene` (`+0xB4`), pas à `+0xDC`** :
      mesuré, `+0xDC` n'est appelé aucune fois sur 121 images
- [x] 3.9 Vérification visuelle du chemin porté — **faite après 4.3** : la géométrie du menu
      apparaît dans la fenêtre du backend
- [x] 3.7 Pose du viewport (`+0x50`) appliquée à la cible de rendu
- [x] 3.8 Faire lever les slots non portés avec leur décalage dans le message

## 4. Débranchement de 3dhelp

- [ ] 4.1 Inventorier les slots encore appelés par `winsys3d` et non portés ; décider pour chacun
      entre portage et levée
- [x] 4.2 Identifier ce qui dépend de la fenêtre de `3dhelp` (saisie clavier, pompe de messages) —
      **rien à transférer** : la saisie passe par `GetAsyncKeyState` (`winsys3d.c:4109+`), qui est
      globale, et le pump de `winsys3d.c:610` utilise `hWnd = NULL`, donc il dispatche déjà vers
      notre fenêtre. Le risque consigné au design n'existait pas.
- [ ] 4.5 Échelle des coordonnées : le jeu émet sa géométrie dans un espace d'environ 320x240 alors
      que la cible fait 960x720, d'où l'image cantonnée en haut à gauche. Déterminer où le facteur
      d'échelle devrait s'appliquer (`SetShift` / niveau de zoom `Size` / viewport D3D3)
- [x] 4.3 Cesser d'initialiser le chemin DirectDraw de `3dhelp`
- [x] 4.4 Vérifier que la résolution du bureau n'est plus jamais modifiée, y compris après une
      terminaison anormale

## 5. Bascule fenêtre / plein écran

- [x] 5.1 Implémenter le plein écran sans bordure par redimensionnement de la fenêtre
- [x] 5.2 Mémoriser position et dimensions pour le retour en fenêtre
- [x] 5.3 Redimensionner la swap chain au redimensionnement de la fenêtre

## 6. Vérification et nettoyage

- [ ] 6.1 Comparer le menu côte à côte avec le jeu d'origine (référence : le rendu actuel est déjà
      jugé fidèle)
- [x] 6.2 Relever le taux d'écran noir sur au moins 10 lancements et le consigner dans
      `ANALYSE_ECRAN_NOIR_MENU.md`
- [x] 6.3 Retirer l'échafaudage de journalisation (`[VP]`, `[CFG]`, `[MODE]`, `[D3D11]`) et les
      marqueurs `ECHAFAUDAGE TEMPORAIRE`
- [x] 6.4 Documenter le backend dans `Documentation/DLL/3dhelp.md` (ce qui est porté, ce qui lève)
- [x] 6.5 Mettre à jour `HISTORY.md` et `REVERSE_STATUS.md`
