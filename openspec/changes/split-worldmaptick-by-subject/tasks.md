> **Ne pas commit.** Seul l'utilisateur commit, même après un lot terminé.
>
> **Jamais `Get-Content` / `Set-Content`** sur `WorldMapTick.cs` ou ses descendants. Tout passe par
> `[IO.File]::ReadAllBytes` / `WriteAllBytes` en **ISO-8859-1** — c'est ce qui rend la comparaison
> de lignes équivalente à une comparaison d'octets.
>
> **Le SDK et node ne sont pas dans le `PATH` de l'agent.**
> `$env:PATH = "C:\Program Files (x86)\dotnet;D:\Logiciels\nodejs;$env:PATH"`
>
> **Aucune ligne de code n'est modifiée.** La seule exception autorisée est la transformation d'un
> champ statique en propriété calculée si et seulement si 1.6 en démontre la nécessité — et elle est
> alors signalée explicitement, jamais glissée.

## 1. Inventaire, outillage, filets — rien ne bouge encore

- [x] 1.1 Générer l'inventaire des membres de `WorldMapTick.cs` : nom C#, adresse virtuelle Ghidra
      (depuis le commentaire `// Ghidra:` ou le suffixe `_00XXXXXX`), ligne de début (première ligne
      du bloc de commentaires qui le précède), ligne de fin, bandeau `// ====` courant, fichier cible
      proposé selon D10. Sortie dans le scratchpad, format TSV + rendu lisible.
      → `OpenForesia.Tools/worldmaptick-inventory.awk`, `scratchpad/inventaire.tsv`. **1 291
      membres.** Le fichier porte **130 blocs de bandeau**, pas 77 — le premier relevé les avait
      sous-comptés ; corrigé dans les trois artefacts.
- [x] 1.2 Vérifier que l'inventaire **couvre exactement** les lignes 25..74071 : plages contiguës,
      sans trou et sans recouvrement. Un trou = une ligne perdue au déplacement.
      → **Conforme** : début 25, fin 74071, aucun trou, aucun recouvrement. Le manifeste dérivé
      couvre 74 047 lignes en 75 plages sur 68 fichiers, et **chaque point de coupe est soit une
      tête de bandeau soit une ligne de déclaration** (seule exception : 25, la ligne de départ).
- [x] 1.3 Croiser l'inventaire avec les **130 bandeaux** et lister tous les écarts — les trois déjà
      connus (`L20940`, `L24443`, `L67638`) plus ceux que l'inventaire révèle. Un bandeau dont le
      contenu se répartit sur plusieurs fichiers cibles est un écart à consigner.
      → `scratchpad/ecarts.md`. **14 écarts sur 130.** Trois genres : 7 dérives de queue (bénignes,
      le découpage les résout), **5 bandeaux-cimetière** (L4325, L67638, L8174, L20940, L42746 —
      jusqu'à 6 fichiers cibles sous un seul titre), et 1 écart de provenance (L71728). Les trois
      écarts annoncés dans la proposition sont confirmés et **ne sont pas les pires** : L4325 et
      L67638 couvrent six fichiers chacun.
- [x] 1.4 **Faire valider l'inventaire et la carte des 68 fichiers par l'utilisateur.** Aucune ligne
      ne bouge avant cet accord. Les deux molettes de D1 (découpe des écoles, groupement de l'IA
      forêt) sont tranchées ici.
- [x] 1.5 Écrire `OpenForesia.Tools/worldmaptick-manifest.tsv` depuis l'inventaire validé
      (`fichier<TAB>début<TAB>fin`, plusieurs lignes par fichier autorisées pour les blocs recollés
      de D7), et figer la référence :
      `verify-split.ps1 -Snapshot -Source ...\State03\WorldMapTick.cs -Baseline OpenForesia.Tools\worldmaptick-baseline.txt`
      → `worldmaptick-manifest.awk` (le jumeau de `split-manifest.awk`, même trois règles) et
      `worldmaptick-manifest.tsv` : **75 plages, 68 fichiers, 74 047 lignes.** Référence figée :
      `worldmaptick-baseline.txt`, **3 289 798 octets, 74 072 lignes**.
- [x] 1.6 **Auditer les sept champs `static readonly` à initialiseur appelant** (D6) : lire le corps
      d'`AllocBytes` et de `BuildDeveloperNotice_00551814`, et établir pour chacun s'il lit un champ
      statique. Consigner le verdict champ par champ. Si un seul est en cause, décider avec
      l'utilisateur de la propriété calculée avant tout déplacement.
      → **Les sept sont sûrs ; aucune modification de code n'est nécessaire.** `AllocBytes` et
      `BuildDeveloperNotice_00551814` ne lisent aucun champ statique. Le balayage a été **élargi à
      tous** les initialiseurs du fichier, multi-lignes compris : ils se réduisent à `new()`,
      `Environment.GetEnvironmentVariable(…)` ou `NativeMemory.AllocZeroed(<constante>)`. Verdict
      détaillé dans D6. **Le piège de 2026-08-19 ne se rejoue pas ici.**
- [x] 1.7 Étendre `apply-split.ps1` : paramètre `-Residual` (D4) et paramètre `-PreambleRanges`
      acceptant des plages non contiguës (D3). Vérifier en `-WhatIf` que le préambule produit est
      bien `1..7` + `18..24`, sans les dix lignes propres à `FUN_00516fc0`.
      → Fait, **rétrocompatible** (`-Residual` vaut `Blaze.cs` par défaut, `-PreambleRanges` vide
      retombe sur `1..PreambleEnd`). Préambule retenu : **`1-6,17-24`** (14 lignes) — la ligne vide
      17 manquait dans le premier essai `1-7,18-24`, qui collait les `using` au bloc de commentaires.
      Deux ajouts non prévus : quand le reliquat devient **vide**, le script n'écrit pas une classe
      partielle fantôme mais signale le fichier à supprimer (cas du lot H) ; et
      **`-ResidualPreambleRanges "1-24"`** donne au reliquat son propre préambule, sans quoi les
      lignes 8..16 — qui ne sont dans aucune plage du manifeste — disparaîtraient du dépôt pendant
      sept lots. Voir D3.
- [x] 1.8 ~~Étendre~~ **Prouver** `verify-split.ps1` : le bandeau d'en-tête va **au-dessus de
      `using`** (convention déjà suivie par `WorldMapCamera.cs`), donc hors des bornes `{` … `}` que
      le script utilise pour délimiter un corps. **Aucune modification nécessaire** — voir D3.
      → Preuve faite **en grand** : les 68 fichiers ont été extraits dans un arbre du scratchpad,
      puis vérifiés. Résultat : couverture 25..74071 sans perte ni doublon, **68 vérifiés, 0 en
      échec**, « Aucune ligne modifiee ». **Le découpage entier est prouvé à l'octet avant qu'une
      seule ligne n'ait bougé dans le dépôt** — ce qui pré-valide aussi la tâche 9.5.
- [x] 1.9 **Corriger les quatre scripts qui codent le chemin en dur** (D8) —
      `census-entity-handlers.sh:38`, `census-item-effects.sh:36`, `census-spells.sh:42`,
      `check-descriptors.sh:79` — pour lire `State03/**/*.cs` au lieu du fichier unique.
      → Les trois recensements prennent un `TICK_DIR` et concatènent l'arborescence dans leur `$TMP`
      déjà existant : **tous les `"$TICK"` en aval restent inchangés**, le diff est minimal.
      `check-descriptors.sh` passe son awk de la section 2 sur `find … -name '*.cs'` — sa section 1
      était déjà récursive, la 2 ne l'était pas.
- [x] 1.10 **Prouver 1.9 sur le fichier encore entier** : regénérer `GestionnairesEntites.md`,
      `ObjetsAtteignables.md`, `SortsAtteignables.md` et la sortie de `check-descriptors.sh`, et
      vérifier un **diff strictement vide**. Sans cette preuve, on ne saura plus distinguer une
      régression de script d'une régression de découpage.
      → Fait en deux temps : **avant** correction, les quatre scripts reproduisent déjà l'état
      committé à l'identique (`git status Documentation/` vide) — la référence est donc saine ;
      **après** correction, `check-descriptors.sh` rend les mêmes 83 lignes au caractère près et les
      trois recensements laissent `git status Documentation/` vide. **Diff strictement vide.**
- [x] 1.11 Relever la ligne de base de la recette : `dotnet build OpenForesia.sln` → noter le nombre
      d'erreurs (0) et **d'avertissements**, qui ne doit plus bouger d'un seul lot.
      → **0 erreur, 39 avertissements**, soit 21 couples (code, fichier) distincts :
      CA2014 ×5, CS0649 ×5, CS0414 ×4, CS8618 ×2, CS0675 ×2, CS0169 ×1, CS0652 ×1, NETSDK1189 ×1.
      Relevé détaillé dans `scratchpad/warnings-avant.txt`.

## 2. Lot A — `Spells/` (~24 300 lignes, 6 fichiers)

- [x] 2.1 Extraire `Spells/SpellCasting.cs`, `School0Wizard.cs`, `School1Priest.cs`,
      `School2Elementalist.cs`, `Herbalism.cs`, `MonsterSpells.cs` via `apply-split.ps1`.
      → **24 294 lignes parties**, le reliquat passe de 74 072 à 50 047 lignes de corps.
- [x] 2.2 Écrire le bandeau d'en-tête de chacun des 6 fichiers : ce qu'il contient, la table
      d'origine (`+0x9c` du répertoire d'état, `CastSpell_0053A980`, famille × 4 puis indice × 0x30),
      et le renvoi vers `Documentation/Reference/Sorts.md`.
      → Fait, via `scratchpad/insert-banner.ps1` (insertion **en octets**, ISO-8859-1 des deux
      côtés : c'est ce qui laisse passer un tiret cadratin UTF-8 sans jamais le décoder — vérifié,
      `e2 80 94`). Deux relevés faits en écrivant les bandeaux : l'école 1 porte **deux sous-types
      hors suite** (Sonde 0x37 et 3ᵉ œil 0x38), et l'école 2 n'a que **14 bandeaux pour 20 fiches**
      — les quatre Matéria partagent un corps, ainsi qu'Escargot / Ralenti / Turbo.
- [x] 2.3 Recette du lot : `verify-split.ps1` conforme, `dotnet build` 0 erreur / avertissements
      inchangés, les trois recensements + `check-descriptors.sh` à diff vide.
      → **6 vérifiés, 0 en échec**, « Aucune ligne modifiee » — les bandeaux d'en-tête sont bien
      invisibles au vérificateur. Build **0 erreur, 39 avertissements**, ensemble **identique** à la
      référence. `check-descriptors.sh` à diff vide, `git status Documentation/` vide.

## 3. Lot B — `Ai/` (~16 400 lignes, **25** fichiers, 4 sous-répertoires)

- [x] 3.1 Extraire `Ai/SceneEntityAiCommon.cs` et `Ai/Inn.cs`.
- [x] 3.2 Extraire les **19** fichiers de `Ai/Forest/`.
- [x] 3.3 Extraire `Ai/OldTower/Gremlin.cs`, `Ai/ImmortalCastle/PurpleKnight.cs`,
      `Ai/SealedCave/Gremlin.cs`, `Ai/Amphibians.cs`.
      → 3.1 à 3.3 : **25 fichiers**, pas 21. Le manifeste passe de 68 à **70 fichiers** parce que
      **trois bêtes étaient cachées** sous des noms inventés à l'exploration (voir 3.4).
- [x] 3.4 Écrire le bandeau d'en-tête de chacun : sous-type, bloc de la table `0x0056adc8` ou
      `0x0056ad48`, zone et sous-zones, et l'état `implémenté / testé` tel que
      `GestionnairesEntites.md` le porte. Les bandeaux `// ====` existants deviennent ce bandeau —
      on les déplace, on ne les réécrit pas.
      → **Les noms de créature viennent de `Comportement.md`, pas de moi.** Trois corrections :
      `BlockAPatrol.cs` n'existait pas (c'étaient le **Goblin-sorcier** `0x0049E190` st.9 et le
      **Crotale** `0x0049E920` st.18) ; `FlyingBeast.cs` est l'**Abeille tueuse** `0x004A3C30` du
      bloc C ; et la **Fourmi géante** `0x00499DE0` st.0 était rangée sous `Ladybug.cs`. Deux pièges
      énoncés dans les bandeaux : **deux « Goblin »** distincts (st.2 et st.8) et **deux « Abeille
      tueuse »** (bloc A et bloc C). 25 bandeaux posés, générés depuis une table
      (`scratchpad/ai-banners.tsv`) pour rester homogènes.
- [x] 3.5 Recette du lot (identique à 2.3).
      → **31 vérifiés, 0 en échec** (les 6 du lot A revérifiés au passage), « Aucune ligne
      modifiee ». Build **0 erreur, 39 avertissements**, ensemble identique. `check-descriptors.sh`
      à diff vide, `git status Documentation/` vide.

## 4. Lot C — `Effects/` (~8 260 lignes, 7 fichiers)

- [x] 4.1 Extraire les 7 fichiers d'`Effects/`.
      → 8 306 lignes parties ; le reliquat passe de 34 762 à 26 452 lignes.
- [x] 4.2 Écrire les bandeaux d'en-tête. **Conserver intactes** les notes sur les corps dupliqués
      (« EN TROIS EXEMPLAIRES », les carrés à deux paramètres croisés) et sur les planches relevées
      entières — ce sont les leçons de `check-descriptors.sh`, pas de la décoration.
      → Les notes d'origine sont intactes : les 12 bandeaux `// ====` « EN N EXEMPLAIRES » et les
      deux carrés à paramètres croisés n'ont pas été touchés. Les en-têtes les résument et
      **rappellent la règle de la plage** (relever le bloc entier, jamais le seul descripteur lu)
      sur les cinq fichiers qui portent des planches. `MiscMarkers.cs` est le seul en-tête qui dit
      « ce fichier n'est pas un sujet » : cinq corps dont le rôle commun n'est pas établi, gardés
      ensemble par la règle de routage (4) plutôt que dispersés là où ils mentiraient.
- [x] 4.3 Recette du lot.
      → **38 vérifiés, 0 en échec**, « Aucune ligne modifiee ». Build **0 erreur,
      39 avertissements**, ensemble identique. `git status Documentation/` vide.
- [x] 4.4 **Rejouer `check-descriptors.sh` avec attention particulière** : ce lot déplace la
      quasi-totalité des `s_Descriptors_…` du fichier. La marge par site d'installation doit être
      identique à celle de 1.10.
      → **114 planches déplacées** (27 + 13 + 30 + 2 + 23 + 19 sur les six fichiers concernés), et
      `check-descriptors.sh` rend les **mêmes 83 lignes, marges comprises**. C'est la barrière qui
      comptait pour ce lot : elle prouve que la section 1 du script, déjà récursive, et la section 2
      corrigée en 1.9 voient exactement le même ensemble qu'avant le découpage.

## 5. Lot D — `Items/` et `Cutscene/` (~5 970 lignes, 5 fichiers)

- [x] 5.1 Extraire `Items/ItemScript.cs` (deux plages, D7), `Items/ItemSpawns.cs`,
      `Items/QuickMenu.cs`.
      → `ItemScript.cs` est bien recollé de ses **deux plages** (42587–42628 et 42850–43232), les
      sondes d'objets et de sorts restant au milieu pour `Dispatch/CoverageProbes.cs` au lot G.
- [x] 5.2 Extraire `Cutscene/CutsceneVerbs.cs`, `Cutscene/ForestScenes.cs`.
      → 5.1 et 5.2 : 5 725 lignes parties ; le reliquat passe de 26 452 à 20 752 lignes.
- [x] 5.3 Écrire les bandeaux d'en-tête (table `PTR_LAB_00564f80` pour les opcodes d'objet, table
      `0x0055eb68` pour les 32 verbes de cinématique).
      → Fait. Le bandeau d'`ItemScript.cs` nomme **les trois opcodes qui sortent du fichier** — le
      18 vers la table des 48 effets, le 14 vers `CastSpell_0053A980`, les 9 et 10 qui font naître
      un sous-type par le même mécanisme qu'une fiche de sort : c'est ce qui relie `Items/`,
      `Combat/` et `Spells/`. Celui de `CutsceneVerbs.cs` prévient de **ne pas confondre** les
      32 verbes de cinématique avec les 39 opcodes de l'interpréteur de script d'entité
      (`WorldMapScript.cs`) — même forme, deux tables différentes.
- [x] 5.4 Recette du lot.
      → **43 vérifiés, 0 en échec**, « Aucune ligne modifiee ». Build **0 erreur,
      39 avertissements**, ensemble identique. `check-descriptors.sh` à diff vide,
      `git status Documentation/` vide.

## 6. Lot E — `Handlers/` (~5 750 lignes, 4 fichiers)

- [x] 6.1 Extraire `Handlers/Common.cs`, `Handlers/Sound.cs`.
- [x] 6.2 Extraire `Handlers/Chests.cs` et `Handlers/Forest.cs` — **les deux à plages multiples**
      (D7). Vérifier que le recollage n'a pas inversé l'ordre des plages.
      → Ordre **correct** dans les deux : `Chests.cs` donne `PickWeightedTableEntry` …
      `GrantChestContents_00530F20` (plage 23918–24442) puis `EntityHandler_00473ED0` …
      `ChestGrantContents` (plage 71440–71727) ; `Forest.cs` donne `TickPartyRaft_00458200`
      (16083–16689) puis la queue jusqu'à `AbilityChestGrantContents` (71728–74071). 5 779 lignes
      parties ; le reliquat passe de 20 752 à 14 973 lignes.
- [x] 6.3 Écrire les bandeaux d'en-tête.
      → **Une affirmation corrigée avant d'être écrite.** J'allais écrire que le bandeau de
      provenance « Les gestionnaires que Ghidra n'a pas décompilés » annonce six corps dont trois le
      suivent. Vérification faite : il en annonce six dont **un seul** le suit, cinq sont plus bas
      dans le même fichier, et **deux corps qui le suivent n'y figurent pas** (0x00456730,
      0x00458170). Le découpage a réuni les six dans `Forest.cs` — heureux, mais par hasard. Le
      bandeau le dit ainsi.
- [x] 6.4 Recette du lot.
      → **47 vérifiés, 0 en échec**, « Aucune ligne modifiee ». Build **0 erreur,
      39 avertissements** ; **premier déplacement d'avertissement** du chantier — un `CS0675` passe
      de `WorldMapTick.cs` à `Handlers/Forest.cs` avec son code. Le compte par **code** est
      strictement identique, c'est le contrôle qui compte ; la comparaison par (code, fichier)
      signalera un déplacement à chaque lot désormais, et c'est normal.
      `check-descriptors.sh` à diff vide, `git status Documentation/` vide.

## 7. Lot F — `Combat/`, `Physics/`, `Entities/` (~7 300 lignes, 12 fichiers)

- [x] 7.1 Extraire les 7 fichiers de `Combat/`.
- [x] 7.2 Extraire les 4 fichiers de `Physics/` et les **3** d'`Entities/`.
      → 7.1 et 7.2 : **14 fichiers**, 7 388 lignes. Le reliquat passe de 14 973 à 7 610 lignes.
- [x] 7.3 Écrire les bandeaux d'en-tête. Celui de `Combat/EffectTable.cs` **doit reprendre** la note
      sur les deux tables `0x0056d4e0` / `0x0056d4c0` séparées par un zéro, et celui de
      `Combat/StatusEffects.cs` le squelette commun des entrées 0 à 33.
      → Fait, les deux. Trois autres en-têtes portent un avertissement de lecture :
      `CombatPairing.cs` sur **l'appel à deux sorties écrites par adresse** (`param_2` cesse d'être
      la victime pour devenir le bloc de l'assaillant — le piège n° 1 du répertoire) ;
      `HerbEffects.cs` sur le fait que **le bloc de combat et la fiche sont la même mémoire à
      0x100 près** ; et `HitResolution.cs` dit explicitement que c'est **le fichier que le bandeau
      « L'INTELLIGENCE DES ENTITÉS DE SCÈNE » cachait** — 1 300 lignes de combat sous un titre d'IA.
- [x] 7.4 Recette du lot.
      → **61 vérifiés, 0 en échec**, « Aucune ligne modifiee ». Build **0 erreur,
      39 avertissements** ; **21 couples (code, fichier) uniques, exactement comme la référence**,
      et la répartition par code est identique — le seul écart reste le `CS0675` déplacé au lot E.
      `check-descriptors.sh` à diff vide, `git status Documentation/` vide.
- [x] 7.5 Vérifier explicitement les champs de 1.6 qui atterrissent dans ce lot
      (`s_ReachMarkerAtlas_0056aca0`, `s_ErrorMarkerAtlas_00564808`) : lancer le jeu n'est pas
      demandé, mais le verdict de 1.6 doit rester valable après le déplacement.
      → **Toujours valable.** Les deux vivent maintenant dans `Combat/HitResolution.cs` et
      appellent `AllocBytes` avec un tableau littéral. `AllocBytes` est dans `Platform/Startup.cs`,
      donc dans un autre fichier — mais c'est une **méthode**, pas un champ : le danger de D6 est
      l'ordre des *initialiseurs de champs*, et une méthode est disponible dès le chargement du
      type. Aucun des deux initialiseurs ne lit de champ statique.

## 8. Lot G — `Dispatch/` (~1 910 lignes, 3 fichiers)

- [x] 8.1 Extraire `Dispatch/EntityHandlerDispatch.cs` (le switch de 426 cases, **entier, non
      découpé**), `Dispatch/StaticEntityTables.cs`, `Dispatch/CoverageProbes.cs` (**deux** plages,
      pas trois — 4325–4447 et 42629–42849).
      → Le switch est bien sorti **entier** : 426 `case` comptés dans le fichier produit.
      1 822 lignes parties ; le reliquat passe de 7 610 à 5 813 lignes.
- [x] 8.2 Écrire les bandeaux d'en-tête. Celui de `CoverageProbes.cs` reprend les trois sondes et
      leurs trois `.tsv`, plus `OPENFORESIA_NO_PROBE=1`.
      → Fait, avec les trois propriétés communes qui font la valeur des sondes (union, suivi git,
      tri) et les deux subtilités de clé : l'objet est relevé par **l'hexa de ses seize premiers
      octets** (les noms d'objet ne sont pas de l'ASCII) et le sort par sa **fiche**, jamais par son
      lanceur. Le bandeau d'`EntityHandlerDispatch.cs` dit **pourquoi il ne se découpe pas** et
      nomme les deux créneaux à connaître : `0x00401FA0` (souche `ret` nu, pas une lacune) et
      `0x0047ADC0` (« sous-type inconnu » — il **éteint** l'entité, 69 créneaux sur 192 y pointent).
- [x] 8.3 Recette du lot.
      → **64 vérifiés, 0 en échec**, « Aucune ligne modifiee ». Build **0 erreur,
      39 avertissements**, 21 couples uniques comme la référence ; deuxième déplacement attendu, un
      `CS8618` suit les sondes vers `CoverageProbes.cs`. `check-descriptors.sh` à diff vide, et
      surtout **`git status Documentation/` vide** — c'est le lot où les trois recensements lisaient
      le plus gros risque, puisque `DispatchEntityHandlerCore` est précisément ce qu'ils extraient
      au `sed`.

## 9. Lot H — `Tick/` et le reliquat

- [x] 9.1 Extraire `Tick/WorldMapHud.cs`, `Tick/SpriteAtlas.cs`, `Tick/WorldSimulationStep.cs`,
      `Tick/PartyMemberStep.cs` (deux plages), `Tick/PostureAndInput.cs`.
- [x] 9.2 Déplacer le reliquat en `Screens/State03/Tick/WorldMapTick.cs` (`-Residual`), avec les
      lignes 8..16 du préambule d'origine rattachées — c'est là qu'elles disent vrai.
      → `apply-split.ps1` a signalé **« RELIQUAT VIDE »** et refusé d'écrire une classe partielle
      fantôme, comme prévu en 1.7 ; l'ancien `State03/WorldMapTick.cs` a été supprimé à la main. Les
      lignes 7..16 d'origine ont été reprises de la référence pour composer le bandeau du nouveau
      `Tick/WorldMapTick.cs`, complétées par la carte de « où est passé le reste ».
- [x] 9.3 **Contrôle final de classement** : lire le reliquat en entier. S'il contient autre chose
      que `RunWorldMapTick`, les deux crochets indirects et leurs directs, c'est qu'un sujet a été
      mal classé — le signaler avant de continuer.
      → **PASSÉ EXACTEMENT.** Avant extraction, le reliquat (5 813 lignes) ne contenait que du
      `Tick/` : la boucle, le dessin, le pas de simulation, le pas par personnage, les postures.
      **Aucun intrus.** Après extraction, `Tick/WorldMapTick.cs` déclare exactement trois membres —
      `RunWorldMapTick`, `CallEntityHook_0056afe8`, `CallEntityHook_0056aff0` — et rien d'autre.
      Le classement a tenu sur les huit lots sans rattrapage.
- [x] 9.4 Recette du lot.
      → Build **0 erreur, 39 avertissements**, **21 couples uniques comme la référence**,
      répartition par code identique. Quatre déplacements attendus au total (CA2014 → `WorldMapHud`,
      CS0649 → `SpriteAtlas`, CS0675 → `Handlers/Forest`, CS8618 → `CoverageProbes`) : les
      avertissements ont suivi leur code. `check-descriptors.sh` à diff vide,
      `git status Documentation/` vide.
- [x] 9.5 `verify-split.ps1` **sur le manifeste complet** : les **70** fichiers reconstituent les
      lignes 25..74071 de la référence, à l'octet.
      → **70 vérifiés, 0 en échec, 0 pas encore créés. « Aucune ligne modifiee. »**
      Couverture 25..74071, 74 047 lignes, aucune perdue ni dupliquée.

## 10. Fermeture

- [x] 10.1 Régénérer la **carte des symboles reconstruits** dans
      `Documentation/Reference/DataSymbols.md` — la colonne « fichier » est fausse pour ~220
      symboles. Vérifier que les paires d'adresses adjacentes séparées en deux fichiers sont
      toujours signalées.
      → **Cinq entrées, pas ~220** — l'estimation du changement était fausse, et l'écart est
      instructif : cette table indexe les symboles de `.data` **modélisés dans le port**, pas les
      122 tableaux `static readonly` de descripteurs, qui sont documentés par adresse dans leurs
      propres sections sans nom de fichier. Le contrôle d'adjacence n'en est pas affaibli — ces
      tableaux sont protégés par `check-descriptors.sh`, pas par cette table. Les trois lignes du
      tableau et les deux renvois en prose sont corrigés ; une note datée explique l'écart.
- [x] 10.2 Mettre à jour `Documentation/ARCHITECTURE.md` : la carte des répertoires de `State03/`,
      les 11 rôles et leur test d'appartenance (tableau de D2), et la règle « où va un gestionnaire
      nouvellement transcrit » — c'est la question que ce découpage doit rendre facile.
      → Nouvelle section « `State03/` — le seul écran qui a des sous-répertoires », avec le tableau
      des onze rôles et leur test d'appartenance. Deux sections existantes complétées : le piège de
      l'ordre d'initialisation gagne **pourquoi il ne s'est pas rejoué** (un appel de méthode n'est
      pas une lecture de champ), et l'outillage du découpage gagne les trois nouveaux paramètres,
      les deux règles de méthode (générer depuis la référence ; bandeau au-dessus de `using`) et le
      **contrôle de bandeau orphelin**.
- [x] 10.3 Mettre à jour `CLAUDE.md` : la sonde n'est plus « de `WorldMapTick.cs` » mais de
      `State03/Dispatch/CoverageProbes.cs` ; ajouter le répertoire `State03/` à la section
      « Layout of the reconstruction ».
- [x] 10.4 Consigner dans `Documentation/Etat/REVERSE_STATUS.md` : le découpage, la preuve octet à
      octet, le verdict de 1.6, et le fait que les renvois historiques `WorldMapTick.cs:NNNN` de ce
      même fichier ne sont **pas** réécrits.
- [x] 10.5 Ajouter les entrées à `Documentation/Etat/HISTORY.md` (une ligne par lot, préfixe
      `feat :` / `doc :`).
- [x] 10.6 Rendre la main à l'utilisateur pour le commit. **Ne pas commiter.**
      → Fait. Décision de l'utilisateur en fermeture : **`worldmaptick-baseline.txt` (3,1 Mo) est
      ajouté au `.gitignore`** plutôt que suivi. Il n'est utile qu'à rejouer le découpage et se
      redérive de l'historique ; la recette et le piège `core.autocrlf=true` (git rend le blob en
      LF, le fichier de travail était en CRLF) sont écrits dans le `.gitignore`, dans
      `ARCHITECTURE.md`, et dans le message d'erreur de `verify-split.ps1`. `blaze-baseline.txt`
      reste suivi : asymétrie assumée.
