## Why

`split-blaze-into-subsystems` a rangé la reconstruction en 30 fichiers. Elle n'a rien changé à la
**conception** : tout `src/Blaze/` reste une seule `partial class Blaze`, 640 champs statiques et
600 fonctions statiques. Le fichier dit *où* lire ; il ne dit toujours pas *qui touche quoi*.

La mesure le montre crûment. En séparant les dépendances vers des **fonctions** (normales, elles
deviennent des dépendances de constructeur) de celles vers de l'**état mutable** (le vrai
obstacle) :

| Fichier | déclare | fn | **état étranger** |
|---|---:|---:|---:|
| `Memory/Arenas.cs` | 70 | 0 | **7** |
| `Memory/DataTables.cs` | 203 | 1 | **13** |
| `Engine/Audio.cs` | 279 | 16 | **29** |
| `Engine/UiText.cs` | 143 | 6 | **31** |
| `Screens/State06/Inventory.cs` | 252 | 40 | **50** |
| `Screens/State03/WorldMap.cs` | 138 | 44 | **64** |
| `Screens/State04/SaveSlots.cs` | 133 | 48 | **73** |

**Aucun fichier n'est fermé sur son état** — environ **870 références croisées** au total. Aujourd'hui
`Engine/Audio.cs` touche l'état de 29 symboles d'ailleurs **sans que rien ne le déclare**. C'est
exactement ce qu'une signature de constructeur rendrait visible.

Ce n'est pas un défaut du découpage : c'est ce qu'est un programme de 1998 avec un segment `.data`
plat. La conséquence de conception est que **« chaque classe possède son état » est impossible**, et
qu'il faut une autre réponse — pas renoncer aux vraies classes.

## What Changes

Ce changement livre **le socle** : A, B, E et l'oracle. Les étapes C et D sont esquissées mais
volontairement non détaillées — les planifier avant d'avoir fait A serait de la planification en
aveugle.

- **A — `GameMemory`.** Les quatre fichiers de `Memory/` (795 lignes, 99 membres) deviennent une
  classe scellée instanciable : les 8 arènes, les tables `.data` recouvrées, les globales. **Un seul
  objet, parce que le binaire a un seul segment `.data`** — ce n'est pas un compromis, c'est le bon
  modèle. Ce sont déjà les fichiers les plus fermés du dépôt (7 à 13 références sortantes contre 73
  pour le pire), donc le point de départ le moins risqué.

- **A′ — les premiers tests du dépôt**, sur la disposition mémoire et non sur le comportement :
  chaque symbole tombe dans l'étendue de son arène ; aucune allocation indépendante à une adresse
  déjà couverte ; la taille déclarée d'un symbole ne déborde pas sur son voisin ; deux symboles ne
  partagent pas une adresse sans partager leur mémoire.
  **Ces quatre tests auraient attrapé les seize défauts d'aliasing du projet**, et attrapent
  aujourd'hui `0x00566c70` et les quatre allocations logées dans une arène. Aucun ne lance le jeu.

- **B — l'interop derrière une interface, là où un double existe déjà.** `winsys3d.dll` (85 P/Invoke)
  passe derrière `IWinsys3d` : `SyntheticDevice` **prouve** qu'il y a deux implémentations.
  `wsyskern.dll` (9) suit. Les **108 P/Invoke Win32** (user32, kernel32, gdi32, winmm, ole32, ddraw,
  msvcrt, dinput) restent une classe statique **sans interface** : aucun second implémenteur, une
  interface n'y serait que de l'indirection.

- **E — un composition root**, qui instancie une fois et câble explicitement. Plus
  **`IDisposable` sur `src/Host/` uniquement**.

- **L'oracle de remplacement.** Un vérificateur qui compare le **corps** d'une méthode en ignorant
  `static` et les qualifications, pour prouver « j'ai changé la forme, pas la logique ».

**Assemblage unique.** Pas de découpage en `OpenForesia.Interop` / `.Game` / `.Host` : la séparation
des couches reste conventionnelle, portée par `ARCHITECTURE.md`.

### Deux choses qu'on ne fera PAS, et pourquoi

- **Pas d'`IDisposable` dans `src/Blaze/`.** La transcription a **déjà** sa séquence d'extinction, et
  elle est fidèle : `ShutdownStage1/3/4/5` (`FUN_00504600`, `FUN_00512ff0`, `FUN_00542f50`,
  `FUN_00542f90`), appelée depuis `RunApplication`. Ajouter `IDisposable` créerait un mécanisme de
  libération **parallèle et non fidèle**. Les 91 `NativeMemory.Alloc` pour 2 `Free` ne sont pas des
  fuites : ils modélisent des globales `.data`, qui vivent le temps du processus.

- **Pas de suppression du `unsafe`.** 3 131 lignes manipulent des pointeurs bruts, 35 fichiers sur
  35 sont `unsafe`. Les pointeurs **sont** le modèle mémoire — ils représentent les adresses de
  `.data`. Les enlever n'est pas un refactor, c'est une réécriture qui emporterait la fidélité aux
  dispositions de structures.

## Capabilities

### New Capabilities

Aucune. Le changement transforme des méthodes et champs statiques en membres d'instance et introduit
un câblage explicite. Aucun comportement observable ne change : les mêmes fonctions s'exécutent dans
le même ordre sur les mêmes données.

### Modified Capabilities

Aucune. `openspec/specs/` ne contient que `blaze-intro-attract`, dont le chemin passe par
`StartupSurface` et `D3D11Window` — touchés par `IDisposable` mais sans changement de comportement.

`.openspec.yaml` porte donc `skip_specs: true`, comme `split-blaze-into-subsystems` et les cinq
changements de reconstruction qui l'ont précédé.

## Impact

**Touché par ce changement** :

| | Ampleur |
|---|---:|
| `src/Blaze/Memory/**` → `GameMemory` | ~795 l. |
| `src/Blaze/Platform/Winsys3d.cs` → `IWinsys3d` + implémentation | ~739 l. |
| `src/Host/**` → `IDisposable` | ~3 477 l. |
| composition root (`Program.cs`) | neuf |
| projet de tests | neuf |
| outil de comparaison de corps | neuf |

**Esquissé, non détaillé** : C (`Engine/` en 9 classes d'instance, ~7 700 l.) fichier par fichier ;
puis D (`Screens/` en 12 classes, ~13 800 l.) en dernier, quand les écrans cesseront de bouger —
`Screens/State03/WorldMap.cs` est le front actif de la priorité 2.

**Contraintes dures relevées**, qui bornent ce qui peut devenir une méthode d'instance :

```
  202  [DllImport]              extern -> static obligatoire
   58  [UnmanagedCallersOnly]   appeles DEPUIS le natif -> static, sans `this`
  145  delegate*<...>           pointeurs de fonction manages -> static
```

Sur les 58 rappels natifs, **48 sont dans `src/Host/`** et reçoivent `void* self`, donc savent se
router. **Seuls 10 sont dans `src/Blaze/`** (`SaveData` 8, `Input` 1, `Video` 1) et demanderont un
pont statique vers le graphe d'instances.

**Risque principal — la traçabilité Ghidra.** Le dépôt porte **1 834 mentions** `FUN_`/`DAT_`/`LAB_`
et 90 `TODO(REVERSE)` ouverts. La valeur du projet tient à ce qu'on puisse poser `Blaze.c` et le C#
côte à côte. Le critère d'acceptation n'est donc pas « est-ce du beau C# ? » mais :

> **peut-on encore vérifier cette fonction contre `Blaze.c` en trente secondes ?**

Ce qui interdit ici trois pratiques par ailleurs saines : l'extraction de méthode (une méthode =
une `FUN_`), le remplacement des conditions par du polymorphisme (les `switch` sur sous-état **sont**
la machine à états du binaire), et le renommage sans conserver l'ancre.

**Risque secondaire — la perte de l'oracle.** `verify-split.ps1` prouve l'égalité octet à octet ;
transformer du statique en instance change les octets par construction. D'où l'outil de comparaison
de corps, à écrire **avant** C.

**Non touché** : `Blaze.c`, `Legacy/Blaze.exe`, les `.c` des DLL, `Screens/**` (étape D).
