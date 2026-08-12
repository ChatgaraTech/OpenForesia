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
      → **Cause trouvée le 2026-08-12, ce n'est aucune des hypothèses envisagées.** Mesuré : `Size`
      vaut 21 (facteur 3) au premier dessin et **11 (facteur 2)** en régime établi du menu. La garde
      de `SetShift` (`winsys3d.c:4941`) n'appelle le slot viewport `+0x50` **que si son
      quatrième argument est nul**, alors qu'elle change `Size` et la table de conversion
      inconditionnellement. Appelée avec ce drapeau non nul, elle modifie donc l'échelle des sommets
      sans toucher au viewport : la cible reste dimensionnée par le dernier `Size` ayant posé un
      viewport (21 → 960x720) pendant que les sommets sont émis pour le `Size` courant
      (11 → 640x480).
      → Vérification arithmétique : avec le facteur 2 et le décalage `(32, 24)`, l'enveloppe
      mesurée `X=[32..352] Y=[24..216]` correspond à des brutes `0..160` par `0..96`.
      → **Deux hypothèses écartées, à ne pas rouvrir** : les tables sont correctes et correctement
      choisies (`table[100]=300` pour `Size=21`), et la branche « haute résolution » de
      `SetDispEnv` n'est **pas** active.
      → **Correctif tenté et inerte, ce qui est un fait de plus.** Faire suivre la taille de rendu au
      `Size` courant, relu à `BeginScene`, ne déclenche jamais : la valeur y est **21 à chaque
      début de scène** et 11 au dix-millième dessin de la même image. `Size` change donc *dans*
      l'image et est remis avant la suivante — le moteur pose un viewport 960x720 à chaque image puis
      dessine avec le facteur 2, délibérément. Code retiré plutôt que laissé inerte.
      → **Reste ouvert** : soit le device d'origine recevait lui aussi un viewport 960x720 avec des
      sommets en 640x480, et la compensation est ailleurs ; soit `+0x50` fait quelque chose qu'une
      simple taille de cible ne capture pas. `+0x50` correspond à `SetCurrentViewport` sur le
      device D3D3, qui pose un viewport **dans** une surface dimensionnée par le mode d'affichage —
      ce n'est pas la même chose que choisir la taille d'une cible hors écran, et c'est là que le
      facteur manquant se cache le plus probablement.
      → **Séquence complète relevée** (`OPENFORESIA_TRACE_VP=1`), trois faits stables :
      `+0x50` est appelé **une seule fois**, au démarrage — `SetViewport(32, 24, 960x720)` — et
      `(32,24)` est exactement le décalage composé, donc c'est un **sous-rectangle** dans une
      surface plus grande, pas une taille de cible ; sur D3D3, `SetCurrentViewport` désigne bien une
      fenêtre dans une surface dimensionnée par le mode d'affichage (1024x768 pour `Resolution=4`).
      Le motif par image est `Size=21` à `BeginScene` puis `11` deux dessins plus tard et pour
      tout le corps de l'image, identiquement à chaque image — le 21 est transitoire. Et le contenu
      émis est une grille de cellules de 30 px débutant à `(64,56)`, enveloppe `320x192` : un
      espace PSX 320x240 jamais agrandi.
      → **Mesure décisive suivante** : le rendu du menu étant reconstruit en C# (change
      `reconstruct-main-menu-rendering`), les coordonnées **brutes** de la liste d'affichage sont
      disponibles de notre côté. Les journaliser en regard des `TLVERTEX` qui sortent de
      `DrawPrimitive` donne le facteur réellement appliqué comme un **rapport mesuré**, ce qui
      tranche entre les deux lectures que l'arithmétique seule ne sépare pas — facteur 2 sur des
      brutes 0..160, ou facteur 1 sur des brutes 0..320.