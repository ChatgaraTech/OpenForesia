# TODO

Point d'entrée entre deux sessions. Remis à plat le **2026-08-18** en fin de soirée. L'historique
détaillé est dans `REVERSE_STATUS.md` (sections datées) ; ce fichier ne garde que l'état courant et
la suite à faire.

**Ne pas commit** — seul l'utilisateur commit, même après une tâche terminée.

---

# Priorité 1 — ~~La refonte de `Blaze.cs`~~ **FAITE le 2026-08-19**

`Blaze.cs` est passé de **23 680 à 17 lignes**. La reconstruction vit maintenant dans 30 fichiers
sous `OpenForesia/src/Blaze/` (`Platform/`, `Memory/`, `Engine/`, `Screens/`), plus 5 fichiers
d'échafaudage sous `OpenForesia/src/Host/`.

**Lire `ARCHITECTURE.md`** (racine) avant d'ajouter ou de déplacer du code : il porte la carte des
répertoires, la règle qui décide où va une fonction, et la règle d'état qui protège du défaut
d'aliasing.

Le découpage est prouvé sans perte : les 28 corps réassemblés dans l'ordre du manifeste sont
**identiques à la référence sur 23 663 lignes**, la seule différence étant l'accolade de classe
repositionnée. Build propre, 382,3 et 387 primitives par image, captures identiques.

Trois choses apprises, toutes consignées dans `ARCHITECTURE.md` et `CLAUDE.md` :

- **Le découpage en fichiers n'est pas sémantiquement neutre.** L'ordre d'initialisation des champs
  statiques entre fichiers d'une classe partielle n'est pas garanti ; huit champs `= Arena(0x…)`
  rendaient `null + décalage`. Ils sont devenus des propriétés `=>`.
- **107 paires de symboles voisins d'adresse vivent désormais dans deux fichiers différents.** Leur
  adjacence n'est plus visible dans le code — d'où la carte des symboles reconstruits ajoutée à
  `Documentation/DataSymbols.md`, à consulter avant de déclarer un `DAT_` isolé.
- **Un vrai défaut d'aliasing dormant a été trouvé** : `0x00566c70` porte deux allocations
  indépendantes (`s_MenuRenderBlock` dans le menu principal, `s_ViewRect` dans la carte du monde).
  **Non corrigé** — le réparer changerait le comportement. Il mordra quand la carte du monde
  affichera quelque chose, donc à traiter avant la priorité 2.

Reste à faire, hors périmètre de ce changement : la réparation des **871 lignes à encodage double**
(commentaires et journaux seulement, aucun texte visible en jeu).
# Priorité 2 — Finir `FUN_00533940`

## Où on en est exactement

L'**état de jeu 3, la carte du monde**, est le front. Le chemin d'appel :

```
RunGameStateTick (état 3)
└─ RunWorldMapState            FUN_00516020   [FAIT]
   ├─ LoadWorldMapResources    FUN_005160e0   [FAIT]
   └─ boucle par image
      ├─ LoadWorldMapArea      FUN_00516250   [FAIT]
      │  └─ CallAreaHandler(8) → 0x004747F0   [ÉCHAFAUDÉ, voir plus bas]
      ├─ RunWorldMapFrame      FUN_00516350   [corps FAIT]
      │  ├─ LoadWorldMapSubArea                [FAIT]
      │  │  ├─ InstallEntityVTables            [FAIT — 7 tables + 11 initialiseurs]
      │  │  └─ UploadRawAreaBlocks FUN_005355f0 [FAIT]
      │  └─ PlaceWorldMapParty                 [FAIT]
      │     ├─ TryPlaceEntity   FUN_005330d0   [FAIT]
      │     ├─ FUN_00533940                    ←←← ON S'ARRÊTE ICI
      │     └─ ApplyAreaPalette FUN_005350b0   [à faire, 95 lignes]
      ├─ ReInitializeAllTextures / Ordinal_674 [FAIT]
      └─ RunWorldMapTick       FUN_00516fc0    [à faire, 1226 lignes — le gros morceau]
```

## Comment reproduire l'arrêt en trente secondes

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"     # le SDK n'est pas dans le PATH du shell
$env:OPENFORESIA_SKIP_AREA_HANDLER = '1'
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 24 -WorkingDir OpenForesia `
  -Then WAIT4,ENTER,WAIT1,START,WAIT4,ENTER,WAIT3,ENTER,WAIT8
```

Séquence : « Début du jeu » → un personnage retenu (`ENTER`) → `START` → « Veux-tu jouer avec ce
groupe? » → `ENTER` → « Oui » → `ENTER`. L'exception nommée tombe avec la pile complète.

**Prérequis** : au moins une sauvegarde dans `OpenForesia\save\`. Il y en a quatre.

## `FUN_00533940` — ce qu'on sait déjà, pour ne pas le refaire

`Blaze.c` **80454..80827** (375 lignes). Signature : `FUN_00533940(uint *param_1)` où `param_1` est
l'**emplacement de personnage** de 0x9c octets (celui que `PlaceWorldMapParty` vient de remplir).

**Ce que la fonction fait, en une phrase** : elle part de la face du terrain sous l'entité et
**marche de proche en proche** à travers le maillage, en franchissant les arêtes partagées, jusqu'à
trouver la face qui contient réellement la position — puis elle en déduit la hauteur du sol.

### La structure de flot, déjà cartographiée

C'est le point difficile : **cinq labels sautés depuis plusieurs endroits**. Il faut reconstruire
le flot en `while`/`break`/`continue`, pas le translittérer (règle de `Strategy.md`).

| label | rôle |
|---|---|
| `LAB_005339a8` | tête du corps externe : éteint le bit 8 du short `+2` du curseur, remet le compteur `local_48` à 0x10 |
| `LAB_005339b6` | tête de la boucle interne : sort vers `LAB_00534018` si `local_48` est nul |
| `LAB_00533af7` | ré-entrée après un premier test de signe (`Ordinal_808` = MAC0, le NCLIP) |
| `LAB_00533b96` | ré-entrée pour le troisième côté |
| `LAB_00533c2a` | la **marche vers la face voisine** — c'est la boucle centrale |
| `LAB_00533e9f` | branche des faces à quatre sommets |
| `LAB_0053430a` | ré-entrée vers `LAB_005339b6` |
| `LAB_00534010` / `LAB_00534018` | les deux sorties |

Deux boucles imbriquées cadrent le tout : `for (local_4c = 4; local_4c != 0; local_4c--)` (les
**quatre** références de face de l'entité, à `slot + 0x80`), et `local_48 = 0x10` (seize pas de
marche au maximum). Après elles, une **seconde boucle** appelle `FUN_00533450`.

### Les données

* `iVar7 = *(int *)(DAT_005f7bc0 + 0x28)` — la liste de faces du premier morceau chargé. Côté port :
  `*(byte**)Entity(0x005f7bc0)`, puis `+0x28`.
* `DAT_005f7cc0` — la table de sommets, déjà posée par `LoadWorldMapSubArea`
  (`premierMorceau + 0x40`). **Un sommet fait 8 octets** : `x` et `y` dans le dword de tête (bas et
  haut), `z` dans le short de `+4`.
* `DAT_005fa508` — la table de matières (`premierMorceau + 0x24`). Le short de `+2` d'une entrée
  décide si la face participe au sol.
* **Une face fait 16 octets** : trois index de sommet répartis sur les deux premiers dwords, un
  index de matière au troisième, les attributs au quatrième. Le bit 1 des attributs = face à
  quatre sommets. L'octet `+0xe` est le *type* de face, qui indexe `slot + 0x80 + type * 4`.
* `_DAT_005de730` / `_DAT_005de734` — deux shorts mis à -1 en entrée, comparés plus loin
  (`(&DAT_005de730)[local_50]`, donc un petit tableau indexé). **Pas encore modélisés, et ils
  tombent hors de tout ce qui existe** : `s_CameraBlock_005de030` s'arrête à 0x5de0b0, et l'arène
  du menu objets à 0x5de560. Il faudra une arène de plus. Le vérifier avant d'écrire, plutôt que
  d'ajouter deux champs C# isolés — c'est exactement la forme du défaut d'aliasing du projet.
* `Ordinal_807_exref` reçoit en entrée `(param_1[7] >> 0xc & 0xffff) | (param_1[9] & 0xfffff000) << 4`
  — c'est-à-dire la position (x, z) de l'entité empaquetée, `slot + 0x1c` et `slot + 0x24`.

### `FUN_00533450` — sa dépendance, 52 lignes, facile

`FUN_00533450(face, position)` rend la **hauteur du sol** au point (x, z), par interpolation de
plan : mêmes deux arêtes dans les vecteurs GTE 799 et 800, produit vectoriel dans 801, puis

```
((px>>0xc - x0) * N.x + (pz>>0xc - z0) * N.z) / N.y - y0    puis * 0x1000
```

et **0 si `N.y` est nul**. À écrire en premier, elle est indépendante et elle sert de vérification :
si la hauteur rendue est absurde, c'est la géométrie qui est fausse, pas le flot.

### Le modèle à suivre

`TryPlaceEntity` (`FUN_005330d0`), transcrite aujourd'hui, fait **exactement la même géométrie** :
même disposition de sommet, même produit vectoriel dans 799/800/801, même saturation dans 813. La
relire d'abord — elle donne les bons types et les bons décalages, et elle est **vérifiée par le
comportement** (sa boucle de tirage aléatoire sort, donc elle trouve bien des faces praticables sur
les données réelles).

### Les ordinaux GTE

799 et 800 ont été **identifiés aujourd'hui** (les deux vecteurs d'entrée du produit vectoriel) et
sont résolus dans `EnsureGteRegisters`. 801 = `MAC1..3`, 808 = `MAC0` (où le NCLIP dépose son
signe), 813 = `IR1..3`, 805/806/807 = les trois sommets projetés. Voir
`Documentation/DLL/winsys3d.md`, section « The emulated GTE register file ».

---

## L'échafaudage à retirer — dette assumée

`OPENFORESIA_SKIP_AREA_HANDLER=1` neutralise l'appel à `FUN_004747F0` (`0x004747F0..0x00474AC3`,
~723 octets, neuf sorties convergentes), que **Ghidra n'a pas décompilée** : corps uniquement
adressé, aucune fonction entre `FUN_004742b0` et `FUN_00474ad0` dans `Blaze.c`.

C'est **une déviation de comportement**, pas une omission forcée par la plateforme. La fonction
sautée est une machine de transition de sous-zone (elle éteint des bits dans `DAT_005E9840/41` et
`DAT_005E9000/01/06`, compte les bits levés de `DAT_005E9000`, compare `DAT_005DE52C`/`DAT_005DE530`
à zéro). **Tout ce qui a été observé sous ce drapeau est à re-vérifier** une fois la fonction
décompilée.

→ **Ça demande une re-passe Ghidra ciblée sur `0x004747F0`.** Décoder 723 octets à la main avec neuf
branches convergentes serait déraisonnable — à ne pas confondre avec les deux « re-passes requises »
de cette semaine qui se sont révélées faire vingt octets chacune.

---

## Le reste, par ordre de valeur

1. **`FUN_005350b0`** (95 lignes) — la palette de zone, appelée en fin de `PlaceWorldMapParty`.
   Petite, sur le chemin chaud.
2. **`FUN_00516fc0`** (1226 lignes) — la logique par image de la carte du monde. C'est **elle** qui
   pose `DAT_005645f8` et décide des sorties, et c'est elle qui fera enfin apparaître quelque chose
   à l'écran. Mesurer ses dépendances manquantes avant de s'y mettre, comme on l'a fait pour les
   autres.
3. **`FUN_00549380`** (424 lignes) — le vrai moteur `sprintf`. Aujourd'hui le seul format utilisé
   (`"q:\snd\Q%02d_%02d.snd"`) est posé en dur dans `LoadWorldMapSubArea`. À généraliser le jour où
   un second format apparaît, **pas avant**.
4. Les phases 9 et 10 de `FUN_00524880` et toute la boucle de sauvegarde du menu objets sont
   transcrites mais **injoignables dans le jeu de 1998** (voir `legacy-bug.md`, LB-004) : rien à y
   faire, ne pas les « réparer ».

---

## Rappels de méthode qui ont servi cette semaine

- **Mesurer avant d'écrire.** Un balayage transitif du sous-graphe (index des fonctions construit en
  une passe `awk` sur `Blaze.c`, croisé avec les `FUN_` déjà présents dans `Blaze.cs`) donne en
  quelques secondes « N fonctions manquantes, M lignes ». C'est ce qui a évité de partir sur les
  17 400 lignes de l'état 3 d'un bloc.
- **Quand la pseudo-C est ambiguë, lire le désassemblage** — pas renoncer. Trois « re-passe Ghidra
  requise » de cette semaine faisaient vingt octets. La distinction utile est la **taille**.
- **Une constante 64 bits dans du code 32 bits est un indice, pas un mur** :
  `pbVar14[-0xffffffff00000008]` est l'indice `-8`.
- **Un `DAT_` isolé au milieu d'une région modélisée est presque toujours un champ de cette
  région.** Le défaut d'aliasing s'est reproduit une vingtaine de fois ; le dernier en date
  (`DAT_005e9080`, « Au prix de: 0 ») ne s'est vu qu'à l'écran, le compte de primitives étant
  identique.
- **Vérifier contre l'écran, pas contre un nombre.** Voir la mémoire du projet à ce sujet.
- Le `save\` suit le **répertoire de travail**, le `.cfg` suit le chemin de l'**exe**. Un `save\`
  vide fait rebondir « Invertaire » et « Enchère » vers l'écran racine — c'est le comportement de
  l'original, pas un lien mort.
