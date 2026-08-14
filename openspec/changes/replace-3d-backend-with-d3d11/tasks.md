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
- [x] 3.5 Liaison de texture (`+0x64`), création et conversion des textures depuis le format du jeu
      → **Fait le 2026-08-14.** Le menu s'affiche texturé dans la fenêtre, mesuré **identique à
      `3dhelp`** : boîte englobante `290x148` des deux côtés, écart moyen **0,2/255 par canal**,
      0,1 % de pixels franchement différents (comparaison automatisée de deux captures du même
      binaire, l'une avec `OPENFORESIA_LEGACY_3DHELP=1`).
      → **Le chargeur n'est pas le slot que la doc annonçait.** Le slot `[2]` de la classe texture
      (chemin DIB palettisé, `FUN_00a03460`) n'est **jamais appelé**. Le jeu passe par
      `FUN_00a03980` (`winsys3d.c:3064`), qui appelle `vtable[3](largeur, hauteur, texels,
      DDPIXELFORMAT*)` avec des texels **déjà en 16 bits**. Rien à décoder d'une palette : on lit
      les masques du `DDPIXELFORMAT` fourni (ARGB1555 observé, ARGB4444 possible) et on développe
      en BGRA. Mesuré sur les trois premières textures : 256x256, 256x32, 128x128, masques
      `7C00/03E0/001F/8000`, avec 4264 et 3779 texels transparents — le bit d'alpha porte bien la
      transparence.
      → **Deux corrections d'hypothèses au passage**, toutes deux issues de la comparaison d'images
      et non de la lecture :
      1. le mode « décalque » que le shader appliquait aux quads texturés était faux —
         `TEXTUREMAPBLEND` (slot `+0x94`) ne vaut que **2 (MODULATE)** ou **4 (MODULATEALPHA)**,
         jamais DECAL (`winsys3d.c:2522/2526`, `6024/6027`) ; sans modulation le panneau du menu
         sortait gris au lieu de bleu nuit ;
      2. il fallait porter le **mélange** avec, sinon l'image sortait exactement deux fois trop
         claire : la diffuse des quads du panneau porte `0x7F` et `FUN_00a03db0`
         (`winsys3d.c:3157-3160`) active `ALPHABLENDENABLE` avec `SRCALPHA`/`INVSRCALPHA` dès que
         les textures font 16 bits. Relevé au runtime : le jeu pose bien **MODULATEALPHA**.
      → Portage de la transparence : test dans le shader (`discard` sur alpha nul), qui reproduit
      la couleur-clé `DDCKEY_SRCBLT` de Direct3D 3 sans introduire de dépendance à l'ordre de
      dessin. Le slot `[6]` (couleur-clé explicite) reste neutre : `winsys3d` ne l'appelle qu'en
      dessous de 16 bits par texel, ce qui n'arrive pas ici.
      → **Non couvert, à traiter en 4.1** : les mêmes états de mélange ne sont pas posés sur le
      chemin de repli `OPENFORESIA_LEGACY_STARTUP=1` (façade sur le `3dhelp` réel), où les objets
      texture appartiennent à `3dhelp` et restent donc invisibles au registre — ce chemin rend
      toujours en aplats, comme avant cette tâche.
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
      → **2026-08-14 — LA PRÉMISSE DE CETTE TÂCHE EST FAUSSE, mesurée par comparaison d'images.**
      Deux lancements du même binaire, même `OpenForesia.cfg` (`Resolution=4 Size=21`) : l'un avec
      `OPENFORESIA_LEGACY_3DHELP=1` (rendu par `3dhelp`, celui que le responsable décrit comme
      correct), l'autre par défaut (backend D3D11). Boîte englobante du menu, mesurée au pixel :

      | chemin | origine | taille |
      |---|---|---|
      | `3dhelp`, dans son primaire 1440x900, viewport `(32,24,960x720)` | `(48,54)` | **290x148** |
      | D3D11, dans la cible 960x720 | `(48,40)` | **304x176** |

      Même origine, même ordre de grandeur — **il n'y a pas de facteur d'échelle manquant**. Le
      rendu d'origine cantonne le menu exactement au même endroit et à la même taille : à
      `Resolution=4`, `3dhelp` **n'agrandit pas non plus** la géométrie du menu, le reste de l'écran
      est noir chez lui aussi. L'écart résiduel (304x176 contre 290x148) s'explique par la réunion
      des quads blancs, qui englobe des primitives que `3dhelp` dessine en sombre.
      → **Conséquence** : cette tâche ne décrit pas un défaut de portage mais le comportement propre
      du jeu à cette résolution. Ce qui reste éventuellement à décider est un choix de présentation
      (jouer à une `Resolution` plus basse, ou présenter un sous-rectangle agrandi), pas une
      correction de fidélité. À rouvrir sous cet angle ou à clore.
      → **Contre-épreuve du 2026-08-14, une fois le fond du menu réparé.** La première réfutation
      ci-dessus comparait deux images qui n'avaient **ni l'une ni l'autre** de fond — donc rien qui
      donne l'échelle. Avec le fond, l'écart saute aux yeux : le panneau occupe 30 % de la largeur
      chez nous contre **45 % sur la capture officielle en 640x480**, mesuré, le panneau faisant
      `290x144` dans les deux cas. Mais `3dhelp`, relancé sur le même build à la même
      `Resolution=4`, donne **les mêmes proportions que nous** — fond à l'échelle, interface non.
      La réfutation tient donc, et pour une meilleure raison qu'avant : ce n'est pas que l'échelle
      soit correcte, c'est que **le jeu lui-même ne met pas son interface à l'échelle** au-delà de
      640x480. Comparaison plein cadre des deux rendus : écart moyen **4/255 par canal**, une fois
      pris en compte le décalage `(32,24)` que la façade abandonne délibérément — l'alignement
      optimal, cherché sans a priori, tombe exactement sur l'origine du viewport.