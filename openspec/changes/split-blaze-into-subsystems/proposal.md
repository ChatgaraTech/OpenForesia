## Why

`Blaze.cs` fait **23 673 lignes** : 600 fonctions, 593 globales distinctes, 353 noms `FUN_` encore
non résolus, le tout dans une seule classe. C'était la décision de phase 1, écrite dans `CLAUDE.md`
— *« subsystem splitting is a deliberate second phase, once enough behavior is understood »*. Le
seuil est franchi : dix-neuf sous-systèmes sont identifiés et nommés, les états de jeu 0 à 6 sont
atteignables, et la question qui coûte du temps à chaque session n'est plus « que fait cette
fonction » mais « où est-elle, et qu'est-ce qui la touche ».

Le coût est mesurable, pas théorique. Les 37 bandeaux `// ====` servent aujourd'hui de sommaire, et
**trois d'entre eux mentent** :

| Bandeau | Annonce | Contenu réel |
|---|---|---|
| `L20172` « Not yet reconstructed » | sous-systèmes à faire | l'**extinction** (`ShutdownStage1/3/4/5`), reconstruite depuis longtemps |
| `L11696` « Les huit sous-états » | les sous-états de l'état 4 | quatre choses sans rapport : tables `.data`, chargement de ressources, **l'état 0**, et les **émetteurs de primitives 2D** |
| `L9018` « Sous-couche carte mémoire » | l'E/S carte mémoire | l'E/S (595 l.) **plus les sous-états 0, 3, 4 et 7 de l'état 4** (2 082 l.) |

Un sommaire qui dérive dans un fichier de 23 673 lignes n'est pas rattrapable par la relecture : il
faut que la structure du dépôt porte l'information à la place du commentaire.

**La fenêtre est maintenant.** La priorité 2 (`FUN_00533940`, la carte du monde) est mise de côté le
temps de ce chantier, donc aucun travail n'est en cours dans le fichier. Découper pendant qu'une
reconstruction est ouverte reviendrait à déplacer un échafaudage sous quelqu'un.

## What Changes

- **`src/Host/` est extrait de `src/Blaze/`.** Les cinq fichiers du remplacement de la chaîne
  d'affichage (`D3D11Backend`, `SyntheticDevice`, `DeviceShim`, `D3D11Window`, `StartupSurface`,
  3 459 l.) n'ont **aucun équivalent dans `Blaze.exe`** et sont soumis à la règle inverse du reste
  du dépôt : ils doivent marcher, pas être fidèles. Les mélanger à la transcription empêche un
  lecteur de savoir laquelle des deux règles s'applique au fichier qu'il a sous les yeux.

- **`SyntheticDevice` et `DeviceShim` sortent de `partial class Blaze`** et deviennent de vraies
  classes. Mesuré : ils déclarent eux-mêmes 27 de leurs 29 symboles et 18 de leurs 19 — **93 % et
  95 % de clôture**. Ce sont les deux seuls fichiers du dépôt qui passent ce test. En prime, cela
  supprime un danger que la classe partielle a créé et que `SyntheticDevice.cs` documente déjà :
  l'ordre d'initialisation des champs statiques entre fichiers d'une même classe partielle n'est
  pas garanti, ce qui a forcé un contournement en propriété calculée.

- **`s_WindowedStartup` migre vers `Host/`.** `REVERSE_STATUS.md` le qualifie de *« fourth deviation
  — deliberate, not environmental »* : c'est une décision d'hôte, pas un symbole du binaire. Il n'a
  jamais eu sa place dans la transcription.

- **`src/Blaze/` se découpe en quatre répertoires** — `Platform/` (1 762 l.), `Memory/` (625 l.),
  `Engine/` (7 808 l.), `Screens/` (13 444 l.) — soit **29 fichiers**, tous en `partial class
  Blaze`. Aucune vraie classe n'est créée : 44 % des globales traversent les frontières envisagées,
  et fermer un fichier en déplaçant son état est exactement le défaut d'aliasing corrigé **seize
  fois** dans ce dépôt.

- **Un fichier moteur nouveau, trouvé en cartographiant** : `Engine/Primitives2d.cs` (245 l.) —
  `DrawFlatPrimitive`, `DrawSpritePrimitive`, `DrawSpriteVariant`, `DrawTextRun`, appelés par tous
  les écrans et jusqu'ici enfouis sous un bandeau qui annonçait autre chose.

- **`Screens/` porte un répertoire par état de jeu** (`State00/` … `State06/`), y compris quand il
  n'en contient qu'un fichier. `GameStates.cs` (le répartiteur `FUN_00537ca0`, aujourd'hui mal rangé
  en queue de la scrutation d'entrées) et `DebugParty.cs` restent à la racine de `Screens/`.

- **Une règle de routage remplace le jugement au cas par cas** : plage d'adresses connue →
  `Engine/`, un seul écran appelant → `Screens/StateNN/`, plusieurs écrans → promotion en `Engine/`,
  sinon la fonction **reste chez son appelant** jusqu'à ce qu'on sache. Aucun fichier de rebut.

- **Une règle d'état, qui est le point de tout l'exercice** : une globale ne se déplace jamais pour
  rendre un fichier autonome. Les arènes se déplacent **en bloc**.

- **La documentation quitte les répertoires sources.** `REVERSE_STATUS.md` remonte à la racine,
  `ARCHITECTURE.md` y est créé, et l'index des symboles reconstruits devient une **section de
  `Documentation/DataSymbols.md`** — même clé (l'adresse virtuelle Ghidra), donc une seule
  consultation pour le contrôle d'adjacence qui détecte l'aliasing.

- **`CLAUDE.md` est mis à jour dans le même changement**, sans quoi il contredit le dépôt dès le
  premier fichier créé. La « Phase 1 constraint » disparaît, les noms `GraphicsService` /
  `AudioManager` qu'elle annonçait sont explicitement écartés, et deux erreurs de fait sont
  corrigées : le dépôt openspec n'est pas vide (11 changements actifs, 1 archivé, 1 spec), et `node`
  vit sur `D:\Logiciels\nodejs`, hors du `PATH` — même piège que le SDK dotnet.

**Aucun changement de comportement.** Chaque déplacement est vérifié octet à octet contre `git HEAD`.
La réparation des 871 lignes à encodage double — commentaires et journaux uniquement, aucun texte
visible en jeu — est **explicitement hors périmètre** : mêler les deux rendrait les deux
invérifiables. Elle fera l'objet d'un changement séparé.

## Capabilities

### New Capabilities

Aucune. Ce changement ne déplace que du code entre fichiers, à l'octet près, et met à jour de la
documentation. Il n'introduit, ne modifie ni ne retire **aucun comportement observable** : les mêmes
fonctions s'exécutent dans le même ordre sur les mêmes données.

### Modified Capabilities

Aucune. `openspec/specs/` ne contient que `blaze-intro-attract`, dont le comportement est inchangé —
le chemin titre/attract passe par `StartupSurface` et `D3D11Window`, qui sont **déplacés sans être
modifiés**.

Une spécification décrit un comportement ; aucun comportement ne change, donc aucune spécification
ne doit changer. `.openspec.yaml` porte donc `skip_specs: true`, comme les cinq changements de
reconstruction qui l'ont précédé.

## Impact

**Code déplacé, non modifié** — 23 679 lignes réparties sur 34 fichiers :

| Répertoire | Fichiers | Lignes |
|---|---:|---:|
| `src/Host/` | 5 | 3 459 |
| `src/Blaze/Platform/` | 4 | 1 762 |
| `src/Blaze/Memory/` | 3 | 625 |
| `src/Blaze/Engine/` | 9 | 7 808 |
| `src/Blaze/Screens/` | 12 | 13 444 |
| `src/Blaze/Blaze.cs` | 1 | 40 |

**Code réellement modifié** — quatre points seulement :

- `SyntheticDevice` et `DeviceShim` : sortie de `partial class Blaze`, plus la suppression du
  contournement en propriété calculée que la classe partielle imposait.
- `s_WindowedStartup` : déclaration migrée vers `Host/`.
- `ToAnsiZ` (`Platform/Crt.cs`) et `RenderSizeFromSizeSetting` (`Screens/State00/Options.cs`) :
  passent `internal`. Ce sont les **deux seuls** appels de `Host/` vers la transcription — la
  dépendance devient à sens unique, `Blaze` ne lit rien de `Host`.

**Documentation** : `REVERSE_STATUS.md` (déplacé), `ARCHITECTURE.md` (créé), `CLAUDE.md` (réécrit
partiellement), `Documentation/DataSymbols.md` (nouvelle section). Deux références de chemin
explicite à corriger — `CLAUDE.md:89` et l'en-tête de `Blaze.cs:6`. Les cinq autres vivent dans des
changements openspec **archivés ou terminés** : ce sont des documents historiques, à ne pas
réécrire.

**Non touché** : `Blaze.c`, `Legacy/Blaze.exe`, les `.c` des DLL, `OpenForesia.Tools/`,
`playtest.ps1`, `Prompt.md`, `Strategy.md`, `legacy-bug.md`.

**Risque principal** : le découpage par bandeau est faux — trois bandeaux sur 37 sont déjà démontrés
faux. La première tâche est donc un **inventaire des 600 fonctions** (nom, adresse virtuelle, ligne
actuelle, fichier cible), relu et validé **avant qu'une seule ligne bouge**.

**Second risque** : la région à encodage double. Tout déplacement passe par
`[IO.File]::ReadAllBytes` / `WriteAllBytes` ou les outils d'édition — **jamais** `Get-Content` /
`Set-Content`, qui ont déjà aggravé le problème une fois.
