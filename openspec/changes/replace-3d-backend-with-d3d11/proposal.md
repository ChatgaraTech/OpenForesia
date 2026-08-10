## Why

`3dhelp.dll` affiche via DirectDraw en mode exclusif : il change physiquement le mode de l'écran
(`SetDisplayMode`, `3dhelp.c:1275`), prend le coopératif exclusif (`3dhelp.c:1240`) et présente par
flip matériel. Ce trio ne s'exécute plus nativement sur Windows 10 et passe par une couche de
compatibilité qui **répond `S_OK` à tout** puis fait ce qu'elle peut.

La mesure du 2026-08-10 le montre sans ambiguïté : le mode négocié est **1440x900** alors que le jeu
ne rend jamais plus de 1024x768, et ce quelle que soit la clé `Resolution`. L'image se retrouve donc
cantonnée au coin haut gauche avec du remplissage autour — symptôme confirmé à l'œil et par la
mesure. Aucun mode 16:10 de ce type n'existait sur le matériel de 1998 : la négociation de mode ne
survit pas au matériel moderne.

Le même sous-système est le suspect principal des écrans noirs intermittents (environ 6 lancements
sur 8 lors du dernier relevé), non reproductibles et non observables : tous les appels Direct3D
retournent `S_OK`, y compris ceux des lancements noirs. Toute la chaîne côté jeu — liste
d'affichage, sommets, textures, états, pose du viewport — a été instrumentée et mesurée identique
entre un lancement visible et un lancement noir. Il ne reste rien d'observable à corriger de ce
côté.

## What Changes

- Ajout de **Silk.NET** (2.23.0) comme première dépendance externe du projet : `Silk.NET.Direct3D11`,
  `Silk.NET.DXGI`, `Silk.NET.Direct3D.Compilers`. Faisabilité validée en 32 bits — device créé,
  feature level 11_0.
- Nouveau backend Direct3D 11 rendant dans une cible hors écran aux dimensions exactes que le jeu
  demande, présentée par une **swap chain DXGI en mode fenêtré**.
- **BREAKING (interne)** : le device fourni par `3dhelp.dll` cesse d'être utilisé sur le chemin
  d'affichage. `3dhelp.dll` reste dans le dépôt comme référence de rétro-ingénierie et n'est pas
  modifié.
- Suppression de fait de `SetDisplayMode`, du mode coopératif exclusif et du flip matériel : la
  résolution du bureau n'est plus touchée.
- Mode **fenêtré par défaut**, avec bascule vers un plein écran sans bordure. Le jeu continue de
  croire qu'il rend en plein écran à la résolution qu'il a choisie ; rien de ce qu'il observe n'est
  altéré.
- La façade `DeviceShim` passe de pass-through instrumenté à implémentation réelle, méthode par
  méthode. Les slots non encore portés lèvent une exception descriptive plutôt que de ne rien faire.

## Capabilities

### New Capabilities

- `d3d11-backend` : rendu des primitives du jeu par Direct3D 11 derrière la vtable de device
  attendue par `winsys3d`, sans pipeline de transformation (les sommets arrivent déjà projetés).
- `windowed-presentation` : présentation de la cible de rendu dans une fenêtre redimensionnable ou
  en plein écran sans bordure, sans changement de mode d'affichage.

### Modified Capabilities

Aucune : les capacités existantes décrivent la reconstruction du jeu, qui n'est pas modifiée par
cette change. Le jeu émet exactement les mêmes appels ; seul ce qui les exécute change.

## Impact

- **Dépendances** : première dépendance NuGet du dépôt. Justifiée par le fait qu'aucun levier
  observable ne subsiste côté reconstruction.
- **Code** : `OpenForesia/src/Blaze/D3D11Backend.cs` (nouveau), `DeviceShim.cs` (de façade à
  implémentation), `OpenForesia.csproj`.
- **Non touché** : `Blaze.cs`. La reconstruction du jeu ignore complètement ce niveau — c'est la
  propriété qui rend la migration sûre et réversible.
- **Non touché** : `3dhelp.dll`, `winsys3d.dll` et leurs décompilations, qui restent sources de
  vérité.
- **Contrainte de fidélité** : le backend doit reproduire les conventions de rastérisation dont le
  jeu dépend — sommets pré-transformés, adressage de texture, règle du demi-texel, tampon de
  profondeur. Un écart ici se verra comme un défaut de rendu, pas comme une erreur d'API.
