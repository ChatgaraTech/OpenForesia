## Context

Voir `proposal.md — Why` pour la motivation.

> **Révisé le 2026-08-12 après les mesures 1.1 à 1.3.** La première rédaction plaçait le point
> d'accroche sur trois slots de la vtable `3dhelp` (`+0xC4`, `+0xCC`, `+0xD0`) et faisait demander
> la taille de la surface à `+0xD8`. Les trois slots reçoivent **zéro appel** et `+0xD8` n'est pas
> `GetSurfaceDesc`. Les décisions qui en dépendaient sont remplacées ci-dessous ; celles qui
> tenaient au régime GDI lui-même sont conservées, la mesure les a confortées.

L'état du code qui pèse sur l'approche :

- **L'écran-titre est dessiné par la reconstruction elle-même**, pas par une DLL.
  `Blaze.BlitTitleScreen` fait `GetDC` sur la surface primaire d'un wrapper DirectDraw que le jeu
  construit, `StretchDIBits` du `TITLE640.BMP` en 640x480 (`0x280 × 0x1e0`, constantes de
  l'original), puis `ReleaseDC`, avec reprise sur `DDERR_SURFACELOST`.
- **Tout l'appareillage DirectDraw de `SetupDirectDrawSurfaces` ne produit qu'une chose consommée :
  ce `HDC`.** Le back buffer obtenu par `GetAttachedSurface` n'est jamais stocké, aucun `Flip` n'est
  appelé, et les autres champs du wrapper ne sont lus que par le chemin vidéo DirectShow, laissé
  `null` (TODO #13). Le mode exclusif et le `SetDisplayMode(640, 480)` n'achètent donc rien d'autre.
- La fenêtre de l'attract est de classe `VfwClass`, `WS_EX_TOPMOST | WS_POPUP`, créée en 10x10 puis
  maximisée et étendue à 2000x2000. C'est la seconde moitié du « plein écran » perçu.
- **`D3D11Window.Create` existe déjà et ne dépend d'aucun objet Direct3D.** La création de la
  fenêtre et celle du device sont séparées dans l'API ; elles sont seulement appelées au même
  instant par `D3D11Backend.Initialize`.
- La contrainte « créer la fenêtre tôt casse la boucle de rendu » est **levée** : la tâche 1.1
  montre que la fenêtre seule est sans effet, 3 lancements sur 3. C'est le device qui est en cause.

## Goals / Non-Goals

**Goals**

- Afficher l'écran-titre sans avancer d'un instant la création du device Direct3D 11.
- Garder les deux sources d'image — GDI au démarrage, Direct3D ensuite — dans une seule fenêtre,
  avec une frontière nette entre les deux régimes.
- Rester réversible slot par slot, comme la change `replace-3d-backend-with-d3d11` : un slot est
  soit repris, soit passé à `3dhelp`, jamais silencieusement faux.

**Non-Goals**

- Diagnostiquer *pourquoi* la création du device pendant l'initialisation casse la boucle de rendu.
  Ce design contourne la contrainte au lieu de la lever ; la lever reste un chantier ouvert.
- Porter la lecture des surfaces DirectDraw. Rien ici n'en lit une.
- Toucher au chemin Direct3D du menu ou à la liaison de texture.

## Decisions

### Le démarrage reste en GDI de bout en bout, sans Direct3D

C'est la décision structurante. Le jeu dessine son écran-titre en GDI ; on le présente en GDI, par
un `StretchBlt` de notre DIB section vers le DC de la fenêtre. Aucun device, aucune swap chain,
aucune texture pendant cette phase.

La contrainte d'ordonnancement disparaît alors au lieu d'être contournée : on n'avance que la
création de la **fenêtre**, qui est un appel `CreateWindowExA` sans dépendance Direct3D.

*Alternatives écartées.* Créer le device tôt malgré tout et diagnostiquer la casse : coût non borné,
et la mesure existante est sans appel. Mettre les images de titre en file d'attente pour les rejouer
après création du device : l'écran-titre s'afficherait en retard, ce qui contredit l'exigence de
fidélité, et la complexité serait bien supérieure à un `StretchBlt`.

### On substitue la cible du blit, pas le blit

`BlitTitleScreen` conserve son `StretchDIBits`, sa reprise sur `DDERR_SURFACELOST` et sa structure.
Seule change la provenance du `HDC` : celui d'une **DIB section détenue par le backend**, au lieu de
`GetDC` sur la surface primaire DirectDraw.

Le DC d'une fenêtre ne conviendrait pas : il est découpé par la zone de clip courante, effacé sur
`WM_ERASEBKGND` et invalidé au redimensionnement, alors que l'attract ne redessine que sur
`WM_COMMAND` ou toutes les 20 secondes. Il faut de toute façon une mémoire d'image ; autant que ce
soit elle qui serve de cible.

C'est la transposition exacte de la décision déjà prise pour le chemin Direct3D — rendre dans une
cible hors écran puis présenter — ce qui donne un cadrage identique pour les deux régimes.

*Alternative écartée.* Garder DirectDraw en `DDSCL_NORMAL` avec une surface hors écran au lieu d'une
primaire : plus proche de la forme de l'original, mais ça conserve une dépendance DirectDraw, l'ordre
de démontage vis-à-vis de `Ordinal_789`, et ça n'apporte rien de plus qu'une DIB section.

### La présentation a lieu à la fin de `BlitTitleScreen`

C'est la frontière d'image du régime GDI, au même titre que `EndScene` l'est du régime Direct3D :
la fonction est appelée une fois par redessin, et chaque appel produit une image complète. On y
recopie la DIB vers la fenêtre et on y vide la file de messages.

### La taille est celle de l'original, lue dans son code

640x480 — les constantes `0x280 × 0x1e0` que `BlitTitleScreen` passe déjà à `StretchDIBits`, en
source comme en destination, et que `SetupDirectDrawSurfaces` passe à `SetDisplayMode`. Ce n'est pas
une valeur choisie par nous : c'est la seule que le binaire d'origine emploie sur ce chemin.

La cible de présentation, elle, applique les mêmes règles de cadrage que le régime Direct3D : mise à
l'échelle, rapport d'aspect préservé, image centrée.

### `SetupDirectDrawSurfaces` cesse de prendre l'affichage

Le niveau de coopération passe de `DDSCL_FULLSCREEN | DDSCL_EXCLUSIVE` à normal, et le
`SetDisplayMode(640, 480)` disparaît. Comme plus rien ne consomme la surface primaire, la chaîne
primaire/flip n'a plus lieu d'être créée.

**C'est une déviation de fidélité, et elle doit être traitée comme telle** : conditionnelle,
réversible, et commentée sur place en nommant l'appel d'origine — la forme que le dépôt emploie déjà
pour les trois déviations environnementales consignées dans `REVERSE_STATUS.md`. La différence est
qu'ici la déviation n'est pas subie mais voulue : c'est l'objet du chantier.

Effet de bord favorable : `ReleaseDirectDrawSurfaces` n'a plus à rendre l'affichage avant que
`Ordinal_789` n'initialise `3dhelp`. La contrainte d'ordonnancement qui faisait sortir `3dhelp` en
`ExitProcess(3)` disparaît d'elle-même.

### La fenêtre `VfwClass` reste créée, mais n'est plus affichée

`joySetCapture` et la procédure de fenêtre reconstruite en dépendent ; son `ShowWindow(SW_MAXIMIZE)`
et son `MoveWindow(0, 0, 2000, 2000)` non. On les retire, la fenêtre reste un popup de 10x10 jamais
montré.

Les touches qui terminent l'attract sont lues par `PeekMessageA(NULL, …)`, donc à l'échelle du
thread : elles continueront d'être vues même si elles arrivent par la fenêtre du backend, qui est
sur le même thread. **Déduit, non mesuré** — voir Risques.

### La fenêtre du backend est créée avant l'écran-titre

Elle devient le seul élément visible du démarrage. Sa création est sans effet sur la boucle de rendu
(tâche 1.1) et n'entraîne aucun device : le régime GDI n'en a pas besoin.

`D3D11Window.ReleaseLegacyDisplay` perd l'essentiel de son objet, puisque plus personne ne prend
l'affichage en exclusif avant elle. Elle reste appelée pour le mode que `3dhelp` négocie encore à
son initialisation.

### Bascule explicite entre les deux régimes

Le backend porte un état à deux valeurs : régime GDI tant qu'aucune scène Direct3D n'a commencé,
régime Direct3D ensuite. La bascule est déclenchée par la création du device au premier `BeginScene`
et **ne revient jamais en arrière**.

Une bascule explicite plutôt qu'implicite parce que les deux régimes écrivent dans la même fenêtre
par des moyens incompatibles : après création de la swap chain, on cesse de dessiner en GDI sur ce
`HWND`.

## Risks / Trade-offs

- ~~**Créer la fenêtre tôt pourrait casser la boucle de rendu, comme la créer avec le device.**~~
  **Risque levé le 2026-08-12 par la tâche 1.1.** La fenêtre créée seule juste après `Ordinal_789`,
  sans device ni swap chain, laisse le jeu atteindre sa boucle de rendu — 3 lancements sur 3,
  compteurs de scène et de primitives identiques à la référence. Ce que la mesure d'origine
  imputait au couple « fenêtre + device » vient donc du **device**. Le design tient sur son point
  le plus fragile.

  Deux éléments recueillis au passage, qui expliquent en partie la mesure d'origine :
  `D3D11Window.Create` fait déjà son affichage **sans activation** et rend le premier plan à la
  fenêtre qui l'avait, précisément parce que voler le focus empêchait l'écran-titre de recevoir la
  moindre touche ; et la saisie du jeu passe par `GetAsyncKeyState`, globale, donc indifférente au
  focus une fois celui-ci non volé.
- ~~**Risque principal : les touches n'atteignent plus l'attract.**~~ **Levé le 2026-08-12 par la
  tâche 2.2.** Premier plan sur la fenêtre du backend, sites d'affichage de `VfwClass` neutralisés :
  une frappe termine l'attract et le jeu atteint sa boucle de rendu, 3 lancements sur 3. Le
  `PeekMessageA(NULL, …)` de la boucle est bien à l'échelle du thread.

  Deux faits recueillis pendant cette mesure, qui changent le groupe 6 :
  `OnMoviePlayerActivate` refait `SetFocus` + `ShowWindow(SW_MAXIMIZE)` sur `VfwClass` **à chaque
  `WM_ACTIVATEAPP`** — l'original rétablit donc le plein écran en cours d'attract, et neutraliser
  la création ne suffit pas ; et une fois les quatre sites d'affichage neutralisés, `VfwClass`
  **reste visible en 640x480**, affichée par le mode exclusif DirectDraw lui-même. C'est donc bien
  l'abandon du niveau de coopération exclusif qui la fait disparaître, pas les `ShowWindow`.
- **Cohabitation GDI et DXGI sur le même `HWND`.** Légale tant que les deux ne se chevauchent pas
  dans le temps. → La bascule à sens unique garantit l'absence de chevauchement ; à vérifier
  visuellement à la première image du menu.
- **Ne plus prendre DirectDraw en exclusif change le comportement du wrapper.** Le blit visait une
  surface primaire ; il vise maintenant une DIB. → Les champs du wrapper que plus personne
  n'alimente ne sont lus que par le chemin DirectShow, laissé `null` ; à re-vérifier le jour où
  TODO #13 sera repris.
- **La séquence de démarrage cesse d'aboutir.** → `BlitTitleScreen` conserve ses sorties anticipées
  et sa reprise sur surface perdue ; un échec de la présentation doit dégrader le seul redessin,
  jamais interrompre la boucle d'attract.
- **L'attract joue aussi des vidéos**, dans une fenêtre `VfwClass` distincte. Le chemin DirectShow
  n'étant pas reconstruit (TODO #13), aucune vidéo ne s'affiche aujourd'hui et le sujet est
  théorique. → À rouvrir avec TODO #13, pas ici.

## Migration Plan

1. Mesurer que les touches terminent toujours l'attract avec la fenêtre `VfwClass` non affichée.
   Cette étape valide ou invalide le design.
2. Fenêtre du backend créée avant l'écran-titre. À ce stade rien n'est encore affiché.
3. DIB section créée, `BlitTitleScreen` dessine dedans, sans présentation : l'image est écrite mais
   pas montrée — vérifiable en la vidant sur disque.
4. Présentation en fin de `BlitTitleScreen`, avec pompe de messages.
5. `SetupDirectDrawSurfaces` cesse de prendre l'affichage ; `ShowWindow` / `MoveWindow` retirés de
   `CreateMoviePlayerWindow`.
6. Bascule de régime au premier `BeginScene`, vérification visuelle de la continuité titre → menu.

Retour arrière : les étapes 2 à 4 sont additives et n'enlèvent rien au comportement d'origine —
l'écran-titre continue de s'afficher comme avant, en plus de la fenêtre. C'est l'étape 5 qui coupe,
et elle tient en trois appels à neutraliser.

Retour arrière : à toutes les étapes, ne pas installer les trois slots dans la copie de vtable
rétablit le comportement actuel. Une variable d'environnement dédiée est superflue —
`OPENFORESIA_LEGACY_3DHELP=1` couvre déjà le retour au chemin `3dhelp` intégral.

## Open Questions

Aucune. La seule question ouverte de ce document — la disponibilité de la bascule plein écran
pendant l'écran-titre — est tranchée ci-dessous.

## Décision de périmètre : le mode fenêtré, et rien de plus, pour l'instant

Tranché le 2026-08-12. Le mode fenêtré est retenu **parce qu'il sert les tests** : il rend
l'affichage capturable et le bureau utilisable pendant qu'on travaille. C'est sa justification
immédiate, avant tout confort d'utilisateur final.

La bascule plein écran n'est donc **pas** exposée pendant l'écran-titre. Le régime GDI présente en
fenêtre, point. Aucune tâche de ce chantier n'y touche.

**Intention à plus long terme, à ne pas implémenter ici** : pouvoir choisir la résolution et
basculer entre fenêtre, fenêtre sans bordure et plein écran. Deux briques existent déjà côté
Direct3D (`replace-3d-backend-with-d3d11`, tâches 5.1 à 5.3 : plein écran sans bordure par
redimensionnement, mémorisation de la géométrie de fenêtre, redimensionnement de la swap chain), et
le mécanisme de résolution du jeu est cartographié dans `Documentation/DLL/winsys3d.md`, section
« Coordinate scaling » — `Size` et la table des cinq modes d'affichage. Une change ultérieure aura
donc de quoi partir.

La priorité déclarée reste : **les menus, puis le jeu lui-même**. Ce chantier ne doit pas dériver
vers un gestionnaire de modes d'affichage.
