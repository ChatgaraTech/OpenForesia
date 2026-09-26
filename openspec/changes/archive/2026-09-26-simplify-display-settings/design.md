## Context

`Affichage.ini` est lu par `DisplaySettings` (Host, hors reconstruction, LB-055) et consommé à trois
endroits : `D3D11Backend.PhysicalTargetSize` / `SupportedSampleCount` (taille et MSAA de la cible de
scène), `DeviceShim.CurrentBindFiltered` (filtrage par genre de liaison : interface, polygone filtré,
polygone jamais filtré) et `D3D11Backend.DestinationRect` (`MiseAEchelle`). La fenêtre est créée par
`DeviceShim.CreateGameWindow` → `D3D11Window.Create`, **toujours fenêtrée, à la taille logique**
(`RenderSizeFromSizeSetting`, 640x480 en pratique) ; le plein écran sans bordure n'existe que par
`ToggleFullscreen` (Alt+Entrée). Deux mécanismes protègent la géométrie de la fenêtre contre les
changements de mode que `3dhelp` négocie encore à son initialisation : `SavePlacement` au démarrage
et à chaque redimensionnement voulu, `RestorePlacement` à la bascule vers le régime Direct3D — tous
deux **inertes quand la fenêtre est en plein écran** (`s_Fullscreen`).

Voir `proposal.md` pour le pourquoi, `specs/display-enhancements/spec.md` pour le comportement.

## Goals / Non-Goals

**Goals:**
- Un `Affichage.ini` de quatre clés, dont aucune n'exige de comprendre le rendu.
- Garder, derrière `Rendu = origine`, l'image pixel pour pixel du port d'avant LB-055 — la référence
  des comparaisons de captures.
- Ouvrir le jeu directement dans le mode et à la taille voulus, sans étape Alt+Entrée.

**Non-Goals:**
- Un changement de **définition** d'écran : le plein écran exclusif garde celle du bureau.
- Retenir le mode ou la taille choisis en cours de partie : le fichier reste en lecture seule.
- Toucher au shader : le filtrage manuel, le bornage au rectangle de texels et l'interpolation
  `centroid` restent tels quels — ils servent toujours dès que la cible est agrandie ou que
  `Filter=1`.
- Un configurateur graphique.

## Decisions

**D1 — Un interrupteur `Rendu` plutôt que des valeurs fixes sans échappatoire.** Supprimer toute
possibilité de revenir à ×1 casserait la recette de comparaison de `test-protocol.md` et de
`CLAUDE.md` (`ResolutionInterne = 1`, `Anticrenelage = 1`, `Filtrage3D = proche`). Une seule clé à
deux valeurs remplace ces trois lignes. Alternative écartée : garder les clés retirées en « mode
expert » non documenté — c'est précisément la surface qu'on veut réduire.

**D2 — `moderne` = l'ancien `auto` + MSAA 4 + filtrage `jeu` + interface nette.** Ce sont les
défauts actuels ; le changement ne modifie donc pas l'image d'un joueur qui n'a jamais touché le
fichier. `DisplaySettings` expose désormais des propriétés dérivées (`RenderAtDisplaySize`,
`SampleCount`, `FilterPolygons`) plutôt que les clés : les trois consommateurs ne lisent plus de
valeur brute, et `origine` se décide en un seul endroit. Le repli MSAA de `SupportedSampleCount`
(4 → 2 → 1) et celui de la cible trop grande sont conservés tels quels.

**D3 — Filtrage.** `CurrentBindFiltered` devient : `origine` → jamais ; `moderne` → seulement
`TextureFilterClass.Filtered3d`. Les enums `Texture3dFilter` et `InterfaceFilter` disparaissent.
L'`if (Filtered)` du shader et le reste de la chaîne ne changent pas.

**D4 — Clés retirées reconnues, pas inconnues.** L'installeur n'écrase jamais l'`Affichage.ini` du
joueur : une installation du 2026-09-26 garde ses quatre anciennes clés pour toujours. Elles sont
listées dans `DisplaySettings` et produisent `[AFFICHAGE] clé retirée, ignorée : …` — une ligne
distincte de « clé inconnue », pour qu'on ne cherche pas une faute de frappe.

**D5 — `Mode` s'applique après `SavePlacement`, pas à la création.** `D3D11Window.Create` reste
« fenêtre à cadre, taille donnée » ; `CreateGameWindow` calcule la taille (`Fenetre`), crée, relève
la géométrie, **puis** applique le mode : `fenetre-sans-bordure` retire le cadre en gardant la zone
cliente et centre sur le moniteur ; `plein-ecran` appelle le chemin de `ToggleFullscreen` (D9).
Ainsi le relevé de géométrie décrit toujours la fenêtre à cadre à la taille `Fenetre`, et Alt+Entrée
depuis `plein-ecran` retombe sur elle sans code de plus. Alternative écartée : créer directement en
`WS_POPUP` — il faudrait alors fabriquer un relevé fictif pour le retour.

**D6 — `RestorePlacement` doit aussi protéger le plein écran.** Il est aujourd'hui inerte quand
`s_Fullscreen` : personne ne démarrait en plein écran avant la bascule vers Direct3D, où `3dhelp`
repositionne encore la fenêtre. Démarrer en `plein-ecran` le rend nécessaire : en plein écran (et en
`fenetre-sans-bordure`), il ré-ajuste la fenêtre à son rectangle attendu (le moniteur, ou le
rectangle centré) au lieu de ne rien faire.

**D7 — `Fenetre = auto`.** En `fenetre` et `plein-ecran` (dont le retour d'Alt+Entrée est une
fenêtre à cadre) : le plus grand rectangle aux proportions de la taille logique qui tienne dans la
zone de travail du moniteur principal, cadre compris (`AdjustWindowRect`) ; en `MiseAEchelle =
entiere`, le plus grand multiple entier `k ≥ 1`, pour que la fenêtre se remplisse sans bande. En
`fenetre-sans-bordure` : l'écran entier, barre des tâches comprise, et une taille explicite se
borne à l'écran, pas à la zone de travail. Révisé après l'essai de Vincent (2026-09-26) : la
première version prenait le multiple entier dans tous les cas — avec `Size=23` (1024x768) sur un
1080p, ×2 ne tient pas et `auto` restait à 1024x768 — et bornait la fenêtre sans bordure à la zone
de travail, si bien qu'un `Fenetre = 1920x1080` devenait 1834x1032 et ne couvrait pas l'écran. La
zone cliente choisie est gardée (`s_StartClientSize`) : la relire sur la fenêtre à cadre, que
Windows peut rogner à la création, ne rendrait pas toujours la taille demandée. Taille logique = celle de `Size=` au
démarrage ; un F11/F12 ultérieur ne redimensionne pas la fenêtre (comportement actuel, spec « The
game changes its render size »). Une taille explicite trop grande est réduite proportionnellement.

**D8 — Nommage.** `Fenetre`, jamais `Resolution` : `OpenForesia.cfg` porte déjà `Resolution=` et
`Size=`, qui fixent la taille *logique* du jeu. Deux réglages du même nom qui ne font pas la même
chose seraient un piège pour le joueur comme pour nous.

**D9 — `plein-ecran` est exclusif, et c'est la swap chain qui le prend.** Ajouté après l'essai de
Vincent : il veut les trois modes d'un jeu actuel, et en particulier pouvoir travailler sur un second
écran (le bestiaire) à côté d'une fenêtre sans bordure — ce que l'exclusif, justement, ne permet pas.
Mécanique :
- la fenêtre s'étend d'abord au moniteur sans cadre (`FitToMonitor`) — c'est tout ce qui est
  possible pendant le régime GDI du démarrage, sans swap chain ;
- `PresentView` réconcilie ensuite l'état voulu (plein écran demandé ET application active) avec
  `GetFullscreenState`, et appelle `SetFullscreenState` si besoin. Les tampons ont la taille du
  moniteur, donc DXGI garde la définition du bureau ;
- DXGI sort seul de l'exclusif quand l'application perd le focus ; `WM_ACTIVATEAPP` demande un nouvel
  examen, et l'exclusif est repris à l'image suivante — jamais depuis la procédure de fenêtre ;
- au retour d'Alt+Entrée, on sort de l'exclusif **avant** de rendre style et rectangle à la
  fenêtre, sinon DXGI la remettrait dans l'état où il l'a prise ;
- `Shutdown` sort de l'exclusif avant de libérer la swap chain (règle DXGI).
Alt+Entrée reste à nous (`MakeWindowAssociation`, `DXGI_MWA_NO_ALT_ENTER`) : DXGI ne saurait pas
revenir au mode de départ. Alternative écartée : laisser DXGI gérer Alt+Entrée — il bascule entre
exclusif et fenêtre à cadre, et perd `fenetre-sans-bordure`.

## Risks / Trade-offs

- [L'exclusif choisit la fréquence la plus proche de ce que la swap chain déclare (`0/1`, sans
  préférence) : sur un écran 144 Hz, il peut prendre 60 Hz] → sans conséquence de cadence, le jeu
  étant réglé sur la synchronisation verticale (60 Hz est même plus fidèle) ; à vérifier en jeu.
- [Refus de l'exclusif (bureau distant, autre application au premier plan)] → une ligne de journal,
  et la fenêtre reste étendue au moniteur sans cadre : l'image est la même.

- [Démarrer en `plein-ecran` pendant le régime GDI de démarrage (écran-titre, attract) : la surface
  GDI est présentée à la taille de la zone cliente] → `StartupSurface.PresentToDc` reçoit déjà
  `ClientSize()` ; à vérifier en jeu par Vincent (tâche dédiée), pas par une supposition.
- [`3dhelp` repositionne la fenêtre en plein écran avant `RestorePlacement`] → D6 ; si un
  déplacement reste visible, c'est un clignotement d'une image au passage titre → menu, pas un état
  durable.
- [Un joueur voulait `Filtrage3D = bilineaire` avec `Filter=0`, ou une interface lissée] → plus
  possible ; `Filter=1` couvre le premier cas. Assumé : c'est la simplification demandée.
- [Perte du sur-échantillonnage ×N pour qui voulait des arêtes plus fines que MSAA 4] → gain
  invisible mesuré par Vincent ; si le besoin revient, MSAA 8 est une constante à changer.

## Migration Plan

Aucune donnée à migrer. Un ancien `Affichage.ini` continue de fonctionner (D4) : il perd ses
réglages de rendu au profit des défauts `moderne`, qui sont ceux qu'il avait si rien n'avait été
modifié. L'installeur dépose le nouveau modèle seulement là où il n'y en a pas — comme aujourd'hui.
Retour arrière : restaurer `DisplaySettings.cs`, les trois consommateurs et le modèle.

## Open Questions

- Défaut de `Mode` : `fenetre` (conservé ici, comportement actuel) ou `plein-ecran` (attente d'un
  joueur moderne). Se change en une ligne sans toucher aux specs autres que la valeur par défaut —
  à trancher par Vincent après essai.
