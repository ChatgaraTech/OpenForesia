## Context

Voir `proposal.md` — *Why* pour la motivation et les chiffres de couplage. Ce qui suit n'est que
l'état mesuré qui contraint l'approche.

**Trois populations de membres**, et elles n'ont pas les mêmes degrés de liberté :

```
   405 membres   static PAR NECESSITE          202 [DllImport] extern
                                                58 [UnmanagedCallersOnly]
                                               145 delegate*<...>

   ~640 champs   l'etat .data                  8 arenes + tables + globales isolees
                                               ~870 references croisees entre fichiers

   ~600 methodes le comportement transcrit     1 834 ancres FUN_/DAT_/LAB_
                                                 90 TODO(REVERSE) ouverts
```

**Deux populations de code**, qui n'obéissent pas aux mêmes règles :

| | lignes | ancres Ghidra | contrainte |
|---|---:|---:|---|
| `src/Blaze/` | ~24 200 | 1 834 | la fidélité prime ; toute transformation doit rester vérifiable contre `Blaze.c` |
| `src/Host/` | 3 477 | **1** | notre code ; toutes les bonnes pratiques s'appliquent pleinement |

## Goals / Non-Goals

**Goals :**

- Qu'une signature de constructeur **déclare** ce qu'une classe touche, au lieu que ce soit ambiant.
- Que la disposition mémoire soit **testable sans lancer le jeu** — le défaut d'aliasing a été
  corrigé seize fois en le voyant à l'écran, jamais avant.
- Que chaque transformation reste **vérifiable mécaniquement**, comme le découpage l'était.
- Que le socle soit posé **avant** de toucher aux ~21 500 lignes d'écrans et de moteur.

**Non-Goals :**

- Supprimer `unsafe`. Voir `proposal.md`.
- Découper en plusieurs assemblages. Un seul, décision prise.
- Introduire une interface là où il n'existe pas de second implémenteur.
- Traiter C (`Engine/`) ou D (`Screens/`) dans ce changement.
- Réécrire la moindre logique transcrite. **Ce changement change des formes, pas des comportements.**

## Decisions

### D1 — `GameMemory` est un seul objet, et ce n'est pas un pis-aller

Les ~870 références croisées vers de l'état ne sont pas réparties selon les fichiers : elles vont
partout. Aucun découpage de l'état ne les ferait disparaître, parce que le binaire n'a **qu'un seul
segment `.data`**, lu et écrit par tous les sous-systèmes.

```
   sealed class GameMemory
       les 8 blocs adresses par decalage
       les tables .data recouvrees
       les globales isolees
```

*Alternative écartée : un objet d'état par sous-système* (`AudioState`, `InputState`…). La mesure dit
que les découpes ne s'alignent pas sur les fichiers — de 7 à 73 références sortantes par fichier. On
passerait le chantier à trancher des frontières que les données contredisent.

*Alternative écartée : garder l'état statique et ne rendre que le comportement instanciable.* Demi-
mesure : les classes seraient réelles mais leur état resterait ambiant, donc le gain principal —
« la signature déclare ce qu'on touche » — serait perdu.

**Le gain concret** : `Audio(GameMemory mem, IWinsys3d gfx)` déclare ce que `Engine/Audio.cs` fait
aujourd'hui en silence sur 29 symboles d'ailleurs.

**Correction du 2026-08-19, à l'exécution : pas de pont statique temporaire.** Le plan prévoyait que
le reste de `src/Blaze/` accède à `GameMemory` par un pont, le temps d'arriver à l'étape C. La
mesure l'a écarté : **595 lignes** hors `Memory/` référencent les 94 membres, et **l'oracle
normalise les qualifications**. Écrire `Mem.X` au site d'appel lui est donc invisible, tandis que le
pont aurait changé les 94 corps d'accesseurs et déclenché autant de signalements légitimes, qu'il
aurait fallu neutraliser par une liste d'exceptions.

Le pont était pire sur les deux tableaux : il gardait une indirection statique **et** il abîmait le
seul outil qui prouve que la logique n'a pas bougé. On qualifie directement. La liste blanche
envisagée n'existe pas.

*Deux formes de qualification*, selon la nature du membre : `Mem.X` pour les membres d'instance,
`GameMemory.X` pour les 18 `const` — une constante ne s'accède pas via une instance en C#. Vérifié
qu'aucune n'est employée en position exigeant une constante, donc elles restent `const`, ce qui est
plus fidèle à une adresse qui ne change jamais.

### D2 — Une interface seulement là où un double existe déjà

```
   85 P/Invoke winsys3d.dll  -> IWinsys3d    SyntheticDevice PROUVE deux implementations
    9 P/Invoke wsyskern.dll  -> IWsyskern    petit, meme nature
  ---
  108 P/Invoke Win32         -> classe statique, AUCUNE interface
```

*Alternative écartée : une interface par DLL.* Réflexe classique, mais 108 P/Invoke Win32 n'ont
aucun second implémenteur : l'interface n'y serait que de l'indirection, et un `IUser32` de
47 membres serait un mensonge sur la substituabilité.

Le critère retenu, réutilisable : **une interface là où un double existe déjà, nulle part ailleurs.**

**Correction du 2026-08-19, à l'exécution : aucune interface n'est justifiée, pas même `IWinsys3d`.**
La vérification prévue par la tâche 3.4 a été faite en premier, et elle annule la prémisse.
`SyntheticDevice` détourne `GetProcAddress` **dans la table d'imports de winsys3d**, pour remplacer
`3dhelp.dll::Create3DDevice`. Il substitue le **device 3D de 3dhelp**, jamais winsys3d, qui reste
toujours la vraie DLL — 173 `Ordinal_NNN(...)` directs, tous `static extern`.

La couture substituable est donc **entre winsys3d et 3dhelp**, et elle est **déjà traitée** par
`SyntheticDevice` et `DeviceShim`, deux vraies classes de `src/Host/`. Créer `IWinsys3d` aurait été
l'indirection sans second implémenteur que cette décision interdit : appliquer D2 correctement
consiste ici à **ne pas créer l'interface**.

Ce que l'étape B devient : l'extraction des **160 P/Invoke de `src/Blaze`** hors de
`partial class Blaze`, en classes statiques par DLL (`Win32`, `Winsys3d`, `Wsyskern`). Pas
d'interface, pas d'instance — `[DllImport]` exige `static`, et il n'y a aucun état. Une classe
statique est ici la forme correcte, pas un pis-aller. Le gain est au site d'appel :
`Winsys3d.Ordinal_688(…)` dit ce que `Ordinal_688(…)` taisait.

### D3 — Les instances sont uniques, et le composition root le dit

Il n'y a qu'un sous-système audio, une table d'état d'entrée, une arène. `new Audio()` dispersé
modéliserait une multiplicité qui n'existe pas.

```
   Program.cs
       |
       +-- Win32              statique, pas d'interface
       +-- Winsys3d           : IWinsys3d
       +-- GameMemory         <- A
       |
       +-- (etape C) Audio(mem, gfx) · UiText(mem, gfx) · ...
       +-- (Host)    HostDisplay(...) : IDisposable
```

C'est de la vraie injection par constructeur, testable, sans prétendre à des instances multiples.

### D4 — Le pont pour les dix rappels natifs de `src/Blaze/`

48 des 58 rappels `[UnmanagedCallersOnly]` sont dans `src/Host/` et reçoivent `void* self` : ils
savent se router. Les **10 autres** (`SaveData` 8, `Input` 1, `Video` 1) n'ont pas de paramètre de
contexte — c'est une contrainte de la signature native, pas un choix.

Pour ceux-là : un **champ statique unique**, posé par le composition root au démarrage, qui donne
accès au graphe d'instances. C'est le motif standard pour un rappel natif sans contexte.

*À vérifier en cours de route* : ces dix rappels touchent-ils réellement de l'état de jeu ? S'ils ne
font que relayer vers un handle, le pont est inutile. À mesurer avant d'écrire.

### D5 — Les tests portent sur la disposition, pas sur le comportement

Premier projet de tests du dépôt. Il ne teste **pas** la logique transcrite : l'oracle y reste
l'écran, via `playtest.ps1`.

Il teste la **carte mémoire**, qui est une propriété statique et vérifiable :

```
   1. tout symbole tombe dans l'etendue de l'arene qui le couvre
   2. aucune allocation independante a une adresse deja couverte par une arene
   3. la taille declaree d'un symbole ne deborde pas sur son voisin d'adresse
   4. deux symboles a la meme adresse partagent bien leur memoire
```

Ces quatre assertions **auraient attrapé les seize défauts d'aliasing** du projet. Elles échouent
aujourd'hui sur des cas connus, ce qui est la meilleure preuve qu'elles servent :

| Cas | Test qui le prend |
|---|---|
| `0x00566c70` — `s_MenuRenderBlock` / `s_ViewRect`, deux allocations | 4 |
| `s_EntityList_005de558` dans `ItemMenuArena` | 2 |
| `s_ModelResultA/B`, `s_CameraMatrix` dans `EntityArena` | 2 |

**Ils doivent donc démarrer en échec**, et ces échecs sont documentés comme attendus jusqu'à ce
qu'un changement ultérieur corrige les défauts. Un test qui passe tout de suite ne prouverait rien.

*Source de vérité des tests* : la carte des symboles reconstruits en fin de
`Documentation/DataSymbols.md`, produite au découpage.

### D6 — L'oracle de remplacement, avant C

`verify-split.ps1` prouve l'égalité octet à octet. Passer du statique à l'instance change les octets
par construction : il cesse de s'appliquer dès l'étape C.

L'outil à écrire compare le **corps** d'une méthode entre deux révisions, **en normalisant** ce que
le refactor est autorisé à changer :

```
   ignore :  `static`  ·  les qualifications ajoutees (mem.X, gfx.Y)
             les modificateurs d'acces  ·  l'indentation
   compare : la suite des instructions
```

Il prouve exactement la bonne chose — « j'ai changé la forme, pas la logique » — et il sert pour C
**et** pour D.

*Alternative écartée : se reposer sur `playtest.ps1` seul.* Il est lent, il ne couvre que les écrans
atteignables, et il ne dirait pas *où* la logique a changé. Il reste le contrôle final, pas le
contrôle de chaque étape.

### D7 — Ce que le refactor n'a pas le droit de faire

Trois pratiques par ailleurs saines sont **interdites dans `src/Blaze/`**, parce qu'elles détruisent
la correspondance avec `Blaze.c` :

- **l'extraction de méthode** — une méthode = une `FUN_` ;
- **le remplacement des conditions par du polymorphisme** — les `switch` sur sous-état *sont* la
  machine à états du binaire ;
- **le renommage sans conserver l'ancre** `// Ghidra: FUN_00XXXXXX`.

Le critère d'acceptation de chaque étape : **peut-on encore vérifier cette fonction contre `Blaze.c`
en trente secondes ?**

Dans `src/Host/` (3 477 lignes, 1 ancre), les trois sont autorisées.

## Risks / Trade-offs

**[La traçabilité Ghidra se dilue]** → D7 l'interdit explicitement, et D6 la vérifie
mécaniquement. Le contrôle est automatisable, pas seulement culturel.

**[`GameMemory` devient un objet-dieu]** → Assumé, et c'est le bon modèle : le binaire a un seul
`.data`. Le gain n'est pas de le découper mais de le rendre **explicite** — on voit qui le prend en
paramètre. Le découper selon des frontières que les données contredisent serait pire.

**[L'ordre d'initialisation, encore]** → Le découpage précédent a fait planter le jeu parce que les
initialiseurs de champs statiques d'une classe partielle s'exécutent dans un ordre non spécifié.
Passer à des instances **supprime** ce risque : un constructeur s'exécute dans un ordre déterminé par
le composition root. C'est un gain, pas un risque — mais la conversion elle-même doit être
surveillée.

**[Les 405 membres qui doivent rester statiques]** → Ils bornent l'exercice. Un `Audio` d'instance
appellera toujours des `Ordinal_*` statiques ; c'est acceptable si ces appels passent par
`IWinsys3d`. Le piège serait de croire qu'on peut tout instancier.

**[Le socle passe, mais C s'enlise]** → C touche ~7 700 lignes de transcription. Il est
explicitement **hors de ce changement**, fichier par fichier, et chaque fichier est réversible
isolément. Si le premier fichier de C coûte trop cher, on s'arrête après le socle — qui a déjà de la
valeur par lui-même.

**[Le projet de tests attire des tests de comportement]** → D5 le borne : la disposition mémoire,
pas la logique. Un test qui a besoin de lancer le jeu n'a rien à faire dans ce projet.

## Migration Plan

```
  0. l'oracle           l'outil de comparaison de corps + sa propre verification
  1. A  GameMemory      ~795 l., les 4 fichiers de Memory/
  2. A' les tests       4 assertions de disposition, DEMARRANT EN ECHEC sur les cas connus
  3. B  IWinsys3d       85 P/Invoke + IWsyskern 9 ; Win32 reste statique sans interface
  4. E  root + Dispose  composition root, IDisposable sur src/Host/ UNIQUEMENT
  ---------------------------------------------------------------------------
  puis, hors de ce changement :
  C.  Engine/ en 9 classes, fichier par fichier, oracle a chaque fichier
  D.  Screens/ en 12 classes, en dernier, quand les ecrans cesseront de bouger
```

**Retour arrière** : chaque étape est indépendante et se révoque par un `git revert`. `A` sans `B`
est cohérent ; `A + B` sans `E` l'est aussi.

**Vérification à chaque étape** : `dotnet build` · les tests de disposition · l'oracle de comparaison
de corps · `playtest.ps1` sur les écrans de référence, captures comparées.

## Open Questions

- **Les dix rappels natifs de `src/Blaze/` touchent-ils de l'état de jeu ?** À mesurer au début de
  l'étape E. S'ils ne relaient que des handles, le pont statique de D4 est inutile. La réponse ne
  change ni l'approche ni le découpage des tâches.

- **`GameMemory` sera-t-il passé partout, ou seulement là où il est lu ?** Se tranchera au premier
  fichier de C, sur pièces. Sans effet sur le socle.
