## 1. Dépendance et faisabilité

- [x] 1.1 Ajouter `Silk.NET.Direct3D11`, `Silk.NET.DXGI` et `Silk.NET.Direct3D.Compilers` (2.23.0)
- [x] 1.2 Sonde : créer un device Direct3D 11 dans le processus 32 bits et journaliser le feature
      level obtenu (résultat : 11_0)
- [ ] 1.3 Retirer l'appel de sonde de `Program.cs` une fois la fenêtre en place

## 2. Fenêtre, device et présentation

- [ ] 2.1 Créer la fenêtre du jeu (classe, procédure de fenêtre, pompe de messages) sans la lier à
      DirectDraw
- [ ] 2.2 Créer le device Direct3D 11 et une swap chain DXGI **en mode fenêtré** sur cette fenêtre
- [ ] 2.3 Créer la cible de rendu hors écran et le tampon de profondeur aux dimensions demandées par
      le jeu, et les recréer quand ces dimensions changent
- [ ] 2.4 Écrire les deux shaders : sommets pré-transformés vers coordonnées normalisées, pixels
      texture modulée par la couleur diffuse
- [ ] 2.5 Présenter la cible de rendu mise à l'échelle, rapport d'aspect préservé, image centrée et
      bandes unies
- [ ] 2.6 Vérifier de bout en bout avec une image de test, `3dhelp` encore en place

## 3. Portage du chemin chaud

- [ ] 3.1 Effacement de la cible de couleur (slot `+0x5C`) et du tampon de profondeur (`+0x60`)
- [ ] 3.2 Début et fin de scène (`+0xB0`, `+0xB4`)
- [ ] 3.3 `DrawPrimitive` (`+0xBC`) — traduction de `D3DTLVERTEX` vers le format de sommet du
      backend, sans transformation
- [ ] 3.4 Mode d'ombrage (`+0x8C`) — plat et Gouraud
- [ ] 3.5 Liaison de texture (`+0x64`), création et conversion des textures depuis le format du jeu
- [ ] 3.6 Présentation (`+0xDC`) branchée sur la swap chain
- [ ] 3.7 Pose du viewport (`+0x50`) appliquée à la cible de rendu
- [ ] 3.8 Faire lever les slots non portés avec leur décalage dans le message

## 4. Débranchement de 3dhelp

- [ ] 4.1 Inventorier les slots encore appelés par `winsys3d` et non portés ; décider pour chacun
      entre portage et levée
- [ ] 4.2 Identifier ce qui dépend de la fenêtre de `3dhelp` (saisie clavier, pompe de messages) et
      le transférer sur la nôtre
- [ ] 4.3 Cesser d'initialiser le chemin DirectDraw de `3dhelp`
- [ ] 4.4 Vérifier que la résolution du bureau n'est plus jamais modifiée, y compris après une
      terminaison anormale

## 5. Bascule fenêtre / plein écran

- [ ] 5.1 Implémenter le plein écran sans bordure par redimensionnement de la fenêtre
- [ ] 5.2 Mémoriser position et dimensions pour le retour en fenêtre
- [ ] 5.3 Redimensionner la swap chain au redimensionnement de la fenêtre

## 6. Vérification et nettoyage

- [ ] 6.1 Comparer le menu côte à côte avec le jeu d'origine (référence : le rendu actuel est déjà
      jugé fidèle)
- [ ] 6.2 Relever le taux d'écran noir sur au moins 10 lancements et le consigner dans
      `ANALYSE_ECRAN_NOIR_MENU.md`
- [ ] 6.3 Retirer l'échafaudage de journalisation (`[VP]`, `[CFG]`, `[MODE]`, `[D3D11]`) et les
      marqueurs `ECHAFAUDAGE TEMPORAIRE`
- [ ] 6.4 Documenter le backend dans `Documentation/DLL/3dhelp.md` (ce qui est porté, ce qui lève)
- [ ] 6.5 Mettre à jour `HISTORY.md` et `REVERSE_STATUS.md`
