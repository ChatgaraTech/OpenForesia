## Why

Au lancement, le jeu affiche encore son écran-titre et son attract dans l'affichage exclusif de
`3dhelp`, puis bascule dans la fenêtre du backend Direct3D 11 au moment où le menu commence à
dessiner. L'utilisateur voit donc un changement de mode d'affichage, une image plein écran, puis un
saut vers une fenêtre. La demande du responsable, consignée dans `TODO.md` le 2026-08-10, est
sans ambiguïté : « j'aimerai qu'on ait qu'une fenêtre, point barre ».

Ce chantier était réputé bloqué par le portage des textures (tâche 3.5 de
`replace-3d-backend-with-d3d11`). Il ne l'est pas : l'écran-titre ne dessine pas en Direct3D mais
en **GDI**, par un `StretchDIBits`. Le porter ne demande de lire aucune surface DirectDraw.

> **Révision du 2026-08-12, après les mesures 1.1 à 1.3.** Une première rédaction plaçait ce
> `StretchDIBits` derrière trois slots de la vtable device `3dhelp` (`+0xC4`, `+0xCC`, `+0xD0`) que
> `DeviceShim` intercepte déjà. **Mesuré faux** : ces trois slots reçoivent zéro appel sur un
> démarrage complet, attract compris. L'écran-titre est dessiné par la reconstruction elle-même.
> Le point d'accroche ci-dessous est le vrai, et il est plus favorable — c'est du code C# que le
> projet contrôle, pas une DLL à détourner.

## Ce qui produit réellement le plein écran

Deux causes cumulées, toutes deux dans `OpenForesia/src/Blaze/Blaze.cs` :

- `CreateMoviePlayerWindow` crée une fenêtre `WS_EX_TOPMOST | WS_POPUP` de classe `VfwClass`, puis
  la maximise et l'étend à 2000x2000 ;
- `SetupDirectDrawSurfaces` prend DirectDraw en `DDSCL_FULLSCREEN | DDSCL_EXCLUSIVE` et appelle
  `SetDisplayMode(640, 480)`.

Le fait qui commande tout le reste : **cet appareillage DirectDraw ne produit qu'une seule chose
réellement consommée, un `HDC`**, celui que `BlitTitleScreen` obtient par `GetDC` sur la surface
primaire. Le back buffer récupéré n'est jamais stocké, aucun `Flip` n'est jamais appelé, et les
autres champs du wrapper ne sont lus que par le chemin vidéo DirectShow, non reconstruit (TODO #13).

## What Changes

- L'écran-titre et l'attract sont rendus **dans la fenêtre du jeu**, la même que le menu, depuis la
  première image affichée.
- `BlitTitleScreen` dessine dans une **DIB section détenue par le backend** au lieu d'une surface
  DirectDraw ; le backend la présente ensuite dans la fenêtre. Le `StretchDIBits` lui-même est
  inchangé — seule sa cible change.
- `SetupDirectDrawSurfaces` cesse de prendre l'affichage en exclusif et de changer le mode.
  **BREAKING (fidélité)** : c'est une déviation assumée par rapport au binaire d'origine, de la même
  classe que les trois déjà consignées dans `REVERSE_STATUS.md`. Elle est l'objet même de ce
  chantier, elle doit être conditionnelle et documentée comme telle, pas silencieuse.
- La fenêtre `VfwClass` reste **créée mais non affichée** : `joySetCapture` et sa procédure de
  fenêtre en dépendent, son affichage plein écran non.
- La fenêtre du backend est créée tôt sur le chemin de démarrage, avant l'écran-titre. Mesuré sans
  effet sur la boucle de rendu (tâche 1.1).
- **Aucun changement** sur le chemin Direct3D du menu, ni sur la liaison de texture (`+0x64`), ni
  sur aucun slot de la vtable `3dhelp`.

## Capabilities

### New Capabilities

- `single-window-startup` : de l'exécution du binaire jusqu'au menu, tout ce que le jeu affiche
  apparaît dans une seule et même fenêtre, sans changement de mode d'affichage ni bascule visible.
  Couvre l'écran-titre, l'attract, et la continuité entre eux et le menu.

### Modified Capabilities

Aucune. `windowed-presentation` traite de la présentation de la cible de rendu Direct3D et n'est pas
modifiée : ce chantier ajoute une seconde source d'image (GDI) à la même fenêtre, sans toucher aux
exigences existantes. La capacité n'est d'ailleurs pas encore versée dans `openspec/specs/` — elle
vit dans la change `replace-3d-backend-with-d3d11`, non archivée.

## Impact

Code touché :

- `OpenForesia/src/Blaze/Blaze.cs` — `BlitTitleScreen` (cible du blit), `SetupDirectDrawSurfaces`
  (niveau de coopération et mode d'affichage), `CreateMoviePlayerWindow` (affichage de la fenêtre
  `VfwClass`). Reconstruction, donc conforme à la contrainte de phase 1 : tout reste dans ce fichier.
- `OpenForesia/src/Blaze/D3D11Backend.cs` ou un fichier voisin — la DIB section et sa présentation.
- Le point de création de la fenêtre du backend, avancé sur le chemin de démarrage.

Dépendances : aucune nouvelle. La DIB section passe par `CreateDIBSection` / `CreateCompatibleDC`
(GDI, déjà utilisé par le chemin d'origine).

**Aucun slot de vtable supplémentaire n'est intercepté.** `DeviceShim` n'est pas touché, hors
retrait des échafaudages de mesure.

Contrainte connue, **levée par la mesure** : on craignait que créer la fenêtre pendant
l'initialisation empêche le jeu d'atteindre sa boucle de rendu. La tâche 1.1 montre que non — c'est
la création du **device** qui pose problème, pas celle de la fenêtre. Comme le régime de démarrage
présente en GDI, aucun device n'est nécessaire avant le premier `BeginScene` et la contrainte
disparaît.

Risque de régression : la séquence de démarrage est aussi celle que couvre la capacité
`blaze-intro-attract`. Deux points sensibles s'ajoutent au fait de ne plus prendre DirectDraw en
exclusif :

- `ReleaseDirectDrawSurfaces` rend l'affichage avant que `Ordinal_789` n'initialise `3dhelp` ; si
  cette prise n'a plus lieu, l'ordre de démontage doit rester cohérent malgré tout ;
- les touches qui terminent l'attract sont lues par un `PeekMessageA(NULL, …)`, donc à l'échelle du
  thread. Avec la fenêtre `VfwClass` non affichée, elles devront arriver par la fenêtre du backend.
  Déduit, **non mesuré** — à vérifier avant de s'y fier.

Risque de régression : la séquence de démarrage est aussi celle que couvre la capacité
`blaze-intro-attract` (« startup always reaches engine initialization »). Toute reprise de slot doit
laisser la séquence se terminer, y compris quand le CD ou le bitmap de titre sont absents.

Hors périmètre : la liaison de texture `+0x64`, les textures du menu, l'échelle des coordonnées
(tâche 4.5). Ce chantier est indépendant des trois.
