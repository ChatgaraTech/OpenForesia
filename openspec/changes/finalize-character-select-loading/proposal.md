## Why

La reconstruction du chargement de l'écran de sélection des personnages (`FUN_005066e0`) existe déjà
dans l'arbre de travail — 215 lignes, six fonctions — mais elle est restée en plan : **elle ne
compile pas**. Huit erreurs, toutes issues de deux causes mécaniques (le mot-clé contextuel `nint`
relu comme une multiplication dans `(int*)(nint)*entry`, et la surcharge `NativeMemory.AllocZeroed`
qui exige des `nuint`). Tant que ça ne compile pas, rien de la suite n'est atteignable.

Surtout, la relecture contre `Blaze.c` a montré que deux symboles y sont nommés avec une **portée
fausse**. `DAT_005e96a0` et `DAT_005e9640` sont baptisés `s_SelectSpriteTable_*` et
`s_SelectPaletteTable_*` comme s'ils appartenaient à l'écran de sélection. Ils sont en réalité
globaux : une trentaine de sites d'accès les indexent dans tout `Blaze.c` (lignes 2043, 14900,
19190, 23424, 25455, 52332, 64765, 79042…), et `FUN_005069e0` les remplit exactement de la même
manière hors de cet écran (ligne 78876). Corriger le nom maintenant coûte deux remplacements ; le
corriger quand les trente sites seront reconstruits coûtera bien davantage.

Enfin, plusieurs faits établis en lisant le binaire ne sont consignés nulle part — au premier rang
desquels la table de répartition `PTR_LAB_00560708`, longtemps classée « irrécupérable ».

## What Changes

- **Correction des huit erreurs de compilation** du bloc « Game state 4 », sans toucher à la
  sémantique reconstruite : parenthéser les casts `nint` et convertir les arguments de
  `NativeMemory.AllocZeroed` en `nuint`.

- **Renommage des deux tables vers leur portée réelle**, avec la justification en commentaire :
  - `s_SelectSpriteTable_005e96a0` → table de **modèles** (`s_ModelTable_005e96a0`).
    `FUN_00532450` divise deux champs par trois (sommets → triangles), relocalise une table de
    `0x400` entrées, et `FUN_00532530` ajoute un décalage de page de texture à deux octets de
    chaque enregistrement de **16 octets**, dont l'origine se calcule par huit compteurs rangés
    par type de primitive. C'est une structure de modèle PSX, pas un paquet de sprites.
  - `s_SelectPaletteTable_005e9640` → table d'**animations** (`s_AnimationTable_005e9640`).
    En `Blaze.c:55840`, un compteur de frame est incrémenté puis comparé au champ `+0x16` de
    l'entrée et remis à zéro : c'est la longueur de l'animation. En `Blaze.c:79042`, l'entrée est
    lue à `[-4]`, ce qui est précisément le `count` que la relocalisation laisse derrière elle en
    écrivant `*result = bundle + 1`.
  - Par conséquent `RelocatePaletteBundle` / `RelocatePaletteEntry` → `RelocateAnimationBundle` /
    `RelocateAnimationEntry`, et `RelocateSpriteBundle` / `RelocateSpriteEntry` /
    `AdjustSpritePages` → `RelocateModelBundle` / `RelocateModelEntry` /
    `AdjustModelTexturePages`.

- **Capacité réelle des deux tables ramenée de `0x40` à 24 entrées** : `DAT_005e9640` et
  `DAT_005e96a0` sont distants de `0x60` octets, soit 24 pointeurs en 32 bits. Les deux vivent dans
  la queue non initialisée de `.data`, donc il n'y a aucun contenu à récupérer — seulement une
  borne à respecter.

- **Deux écarts avec l'original assumés en commentaire plutôt que laissés implicites** : les gardes
  `null` ajoutées sur le répertoire et sur `MainMem`, que l'original n'a pas ; et l'omission de
  `FUN_0040a600()`, dont le corps décompilé est **vide** (`Blaze.c:4319`) et dont l'omission est
  donc comportementalement neutre — mais dont Ghidra recompose une signature fausse, puisqu'il est
  appelé avec des arguments ailleurs (`FUN_0040a600(0x25)`, `FUN_0040a600(4)`).

- **Consignation dans `Documentation/Reference/DataSymbols.md`** des valeurs relevées dans `Legacy/Binaries/Blaze.exe` :
  la table `PTR_LAB_00560708` (huit entrées, résolue au décalage fichier `0x15ED08`), la table
  d'octets `DAT_00562a98`, et la disposition des structures de modèle et d'animation déduite des
  relocalisations.

- **Mise à jour de `REVERSE_STATUS.md`** : l'état 4 passe de « non couvert » à « chargement
  reconstruit, boucle de frame à venir », et le blocage historique sur `PTR_LAB_00560708` est levé.

## Capabilities

### New Capabilities

- `blaze-character-select-loading` : le chargement des ressources de l'écran de sélection des
  personnages — les quatre listes du répertoire, et la relocalisation des paquets de modèles et
  d'animations vers les deux tables globales.

### Modified Capabilities

<!-- Aucune : aucune capacité existante ne voit ses exigences changer. -->

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — le bloc « Game state 4 » (~215 lignes, actuellement non
  commité et non compilable). Aucune autre région n'est touchée : les deux tables renommées ne sont
  encore lues par aucun autre code reconstruit.
- `Documentation/Reference/DataSymbols.md` — ajouts seulement.
- `OpenForesia/src/Blaze/REVERSE_STATUS.md` — ajouts et mise à jour de l'état 4.
- `Blaze.c` et les `.c` des DLL : **non modifiés**, conformément à la règle du projet.
- Aucune dépendance nouvelle. Aucun effet à l'exécution pour l'instant : rien n'appelle encore
  `LoadCharacterSelectResources`, qui n'est atteignable que via `FUN_005066d0`, non reconstruit.
