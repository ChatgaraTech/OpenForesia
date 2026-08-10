## Context

Voir `proposal.md — Why` pour la motivation et les mesures.

Contraintes qui pèsent sur l'architecture :

- Le processus est **32 bits** (`PlatformTarget=x86`, imposé par le chargement des DLL d'origine).
  Sonde passée : Silk.NET 2.23.0 crée un device Direct3D 11 feature level 11_0 dans ce processus.
- La reconstruction reste en **phase 1** : un seul fichier `Blaze.cs`. Le backend est du code neuf,
  pas de la reconstruction — il vit dans ses propres fichiers sans enfreindre cette règle.
- `winsys3d.dll` est appelé tel quel et appelle le device par sa vtable. Le point d'entrée du
  backend est donc contraint : c'est une table de pointeurs de fonctions, pas une interface C#.
- La cartographie des 37 slots (`Documentation/DLL/3dhelp.md`) montre un chemin chaud très étroit :
  `DrawPrimitive` (45 sites), `SHADEMODE` (45), liaison de texture (18), effacement, début/fin de
  scène, présentation, viewport. Les quatre grosses fonctions ont **un seul site d'appel chacune**.
- Le mécanisme d'échange de vtable est déjà en place et éprouvé (`DeviceShim.cs`), aux deux niveaux
  (device `3dhelp` et objets COM sous-jacents).

## Goals / Non-Goals

**Goals**

- Rendre le chemin d'affichage déterministe et observable.
- Garder la migration réversible slot par slot : à tout instant, un slot est soit porté, soit
  déclaré non porté — jamais silencieusement faux.

**Non-Goals**

- Porter les 37 slots. Seuls ceux réellement appelés par `winsys3d` le sont ; les autres lèvent.
- Améliorer le rendu (filtrage, résolution interne, correction d'aspect au-delà du cadrage). La
  fidélité prime ; toute amélioration est une change ultérieure.
- Quitter Windows. Silk.NET rendrait un portage Vulkan/OpenGL envisageable, ce n'est pas l'objet.
- Réécrire `3dhelp.dll` en C#. On le remplace sur le chemin d'affichage, on ne le transcrit pas.

## Decisions

### Silk.NET plutôt que Vortice, MonoGame ou OpenTK

Silk.NET est un jeu de liaisons, pas un moteur : il ne réclame jamais la boucle principale. C'est
éliminatoire ici, parce que la boucle appartient au jeu. MonoGame et OpenTK veulent l'inverse.

Vortice.Windows est un choix équivalent en qualité, avec un enrobage C# plus idiomatique. Silk.NET
l'emporte sur deux points mineurs mais réels : une seule dépendance couvre D3D11 aujourd'hui et
Vulkan/OpenGL si le projet le souhaite un jour, et son style à base de pointeurs est plus proche du
code d'interop existant. Le backend étant derrière notre propre façade, en changer resterait de
l'ordre de la journée.

### Direct3D 11 plutôt que Direct3D 9

D3D9 aurait offert un pipeline fixe et un format de sommet (`D3DFVF_XYZRHW`) qui correspond
exactement à `D3DTLVERTEX` — une traduction quasi mécanique. Écarté malgré cet avantage : D3D9
conserve la notion de device perdu et de plein écran exclusif, c'est-à-dire précisément la classe de
problème dont on cherche à sortir. D3D11 impose d'écrire deux shaders triviaux ; c'est le prix à
payer pour un modèle de présentation qui ne peut pas retomber dans le même piège.

### Cible hors écran + présentation, plutôt que rendu direct dans la fenêtre

Le jeu choisit sa taille de rendu (320x240 à 1024x768) et n'a aucune raison d'apprendre l'existence
d'une fenêtre. On rend donc dans une cible hors écran à la taille exacte qu'il demande, puis on la
présente mise à l'échelle.

Cette indirection est ce qui rend le mode fenêtré **gratuit et fidèle en même temps** : le
redimensionnement de la fenêtre n'a aucun effet observable côté jeu, et le cadrage devient un
problème de présentation au lieu d'un problème de mode d'affichage.

### Fenêtre propre plutôt que réutilisation du HWND de 3dhelp

`3dhelp` crée sa fenêtre dans `FUN_0ffe1c20` (classe `Example`, titre `Spieltitel` — restes d'un
code d'exemple), en `WS_POPUP` 640x480, avec sa propre procédure de fenêtre. La réutiliser
imposerait de cohabiter avec un `3dhelp` encore initialisé, donc encore en train de changer le mode
d'affichage. On crée notre fenêtre et on cesse d'initialiser le chemin DirectDraw de `3dhelp`.

Conséquence à traiter explicitement : la saisie clavier et la pompe de messages passent aujourd'hui
par cette fenêtre. Voir Risques.

### Deux shaders, pas de pipeline de transformation

Le jeu est un portage PlayStation : le GTE projette côté processeur, donc tous les sommets arrivent
déjà en coordonnées écran. Le shader de sommets se borne à convertir les coordonnées écran en
coordonnées normalisées de l'API ; le shader de pixels échantillonne la texture et la module par la
couleur diffuse. Cela vaut pour les menus **comme pour la 3D** — le backend n'aura jamais besoin
d'une matrice.

### Les slots non portés lèvent

Conforme à la règle du dépôt : un manque doit être bruyant. Un slot non porté lève une exception
nommant son décalage, plutôt que de retourner `S_OK` — c'est exactement le comportement de la couche
de compatibilité DirectDraw qui a coûté des jours de diagnostic.

## Risks / Trade-offs

- **La saisie et la pompe de messages dépendent de la fenêtre de `3dhelp`** → Identifier ce qui lit
  les messages avant de retirer cette fenêtre ; si la saisie en dépend, porter la procédure de
  fenêtre sur la nôtre dans la même tâche, pas plus tard.
- **Cesser d'initialiser le chemin DirectDraw de `3dhelp` peut casser des slots encore
  non portés** (les quatre grosses fonctions, la gestion des textures) → Les inventorier avant de
  couper ; ils n'ont qu'un site d'appel chacun, le coût est borné.
- **Écarts de rastérisation** (règle du demi-texel, adressage de texture, arrondi) → Invisibles à
  l'API, visibles à l'œil. Comparaison côte à côte avec le jeu d'origine sur le menu, qui est déjà
  jugé fidèle et sert donc de référence.
- **L'écran noir survit au changement de backend** → Hypothèse non prouvée, seulement étayée. Le
  mode fenêtré rendrait alors le bug enfin observable (capture d'écran fiable, pas de mode
  exclusif), ce qui reste un gain net même en cas d'échec.
- **Formats de texture** : le jeu vient d'un modèle PlayStation 4 bits avec palettes. La conversion
  existe déjà côté reconstruction ; il faut vérifier qu'elle produit un format acceptable par D3D11
  → À lever dès la première texture affichée.

## Migration Plan

1. Sonde de faisabilité — **faite** (device 11_0 en 32 bits).
2. Fenêtre, device, swap chain fenêtrée, cible de rendu et tampon de profondeur ; présentation d'une
   image de test. À ce stade `3dhelp` n'est pas encore débranché.
3. Portage du chemin chaud : effacements, début/fin de scène, `DrawPrimitive`, mode d'ombrage,
   liaison de texture, présentation.
4. Débranchement de l'initialisation DirectDraw de `3dhelp`, transfert de la pompe de messages et de
   la saisie.
5. Bascule fenêtre / plein écran sans bordure.
6. Retrait de l'échafaudage de journalisation.

Retour arrière : les étapes 2 et 3 laissent `3dhelp` fonctionnel. Tant que l'étape 4 n'est pas
franchie, revenir consiste à ne pas installer la vtable du backend.

## Open Questions

- Quel format de tampon de profondeur reproduit le mieux le tri d'origine ? Le jeu vient d'une table
  d'ordonnancement PlayStation, pas d'un tampon Z ; la question ne se posera vraiment qu'avec les
  scènes 3D, bien après les menus.
- Faut-il exposer la bascule plein écran par une touche, ou la lier au réglage `Small` / à une clé
  du `.cfg` ? Sans incidence sur les specs ni sur le découpage.
