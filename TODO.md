# TODO

Point d'entrée entre deux sessions. Remis à plat le **2026-08-19** en fin de soirée. L'historique
détaillé est dans `REVERSE_STATUS.md` (sections datées) ; ce fichier ne garde que l'état courant et
la suite à faire.

**Ne pas commit** — seul l'utilisateur commit, même après une tâche terminée.

**Rien n'est commité.** Tout le travail des 2026-08-19 est dans l'arbre de travail : le découpage,
les cinq vraies classes, `OpenForesia.Tests/`, les références figées de `OpenForesia.Tools/`
(`blaze-baseline.txt`, `bodies-baseline.txt`). Ces références n'existent **que là** — les perdre
ferait perdre l'oracle.

---

# Priorité 1 — `Engine/` en vraies classes · **5 fichiers sur 9**

## Reprendre ici

Le prochain fichier est **`Engine/Audio.cs`** (1 253 l.). Ce n'est ni le plus petit ni le plus
grand : c'est celui dont la **clôture** est la meilleure parmi les restants. Refaire la mesure
avant de partir, elle a changé à chaque conversion :

```bash
cd OpenForesia/src/Blaze
for f in Engine/UiWindow.cs Engine/Model3d.cs Engine/Resources.cs Engine/Audio.cs; do
  gm=0; ic=0; en=0; sc=0
  for s in $(grep -oE "\bs_[A-Za-z0-9_]+" $f | sort -u); do
    if grep -qE "^    (private|internal).*\b$s\b" $f; then ic=$((ic+1)); continue; fi
    src=$(grep -rlE "^    (private|internal).*\b$s\b" --include=*.cs . | head -1 | sed 's|^\./||')
    case "$src" in Memory/GameMemory.cs) gm=$((gm+1));; Screens/*) sc=$((sc+1));; "") ;; *) en=$((en+1));; esac
  done
  printf "%-22s %5s | GameMem %-3s ici %-3s autreEngine %-3s SCREENS %s\n" "$f" "$(wc -l < $f)" "$gm" "$ic" "$en" "$sc"
done
```

**La taille d'un fichier ne dit rien de son coût de conversion ; sa clôture, si.** État au
2026-08-19 au soir (avant conversion d'`Audio`) :

| fichier | lignes | déjà dans `GameMemory` | à lui | autre `Engine/` | `Screens/` |
|---|---:|---:|---:|---:|---:|
| `Audio.cs` | 1 253 | 0 | 25 | 3 | 2 |
| `Resources.cs` | 1 110 | 5 | 34 | 4 | 6 |
| `UiWindow.cs` | 368 | 4 | **0** | 5 | 6 |
| `Model3d.cs` | 583 | 10 | 0 | **18** | 1 |

`UiWindow` et `Model3d` sont **à faire en dernier** : le premier ne possède aucune globale
et n'est qu'un consommateur, le second emprunte 18 symboles à des fichiers `Engine/` non convertis.
Les faire en dernier.

## La recette, éprouvée cinq fois

1. **Mesurer** : membres, `[UnmanagedCallersOnly]`, `delegate*`, et d'où vient chaque `s_*` lu.
2. **Trancher les globales empruntées**, selon une règle simple :
   * suffixe VA (`s_Foo_005e9180`) → c'est du `.data`, ça va dans `GameMemory` ;
   * pas de suffixe VA → c'est notre échafaudage, ça reste chez son propriétaire, exposé `internal` ;
   * **un symbole qui en aliase un autre déménage avec lui**, et un bloc déménage entier
     (les trois clés d'ini `s_SoundVolumeIni`/`s_CdVolumeIni`/`s_GammaIni` sont parties ensemble
     parce que `s_GammaIni` **est** `s_Unknown_005d9de0`).
3. Déclaration de classe → `internal sealed unsafe class X(GameMemory mem)`, puis retirer `static`
   des membres. Le champ injecté s'appelle **`mem`, sans underscore** : le retour arrière négatif
   de l'oracle exclut `_`, donc `_mem.` serait lu comme un changement de logique.
4. Les membres appelés depuis l'extérieur passent `internal`. Vérifier que chaque membre exposé a un
   usage **réel** — plusieurs candidats n'étaient cités qu'en commentaire.
5. `Mem.` → `mem.`, sauf dans les membres restés statiques.
6. Ajouter `using static OpenForesia.Blaze.Blaze;` pour les aides encore dans `Blaze`.
7. Point d'entrée **paresseux** dans `Subsystems.cs` : `internal static X Xx => s_xx ??= new X(Mem);`
8. Qualifier les sites d'appel. **Une constante se qualifie par le nom de type**
   (`Input.s_GammaScale_0054d000`), pas par l'instance — sinon CS0176.
9. Vérifier : build, oracle, tests, playtest **et le taux de course** (voir plus bas).

## Choisir le nom du point d'entrée — ce n'est pas cosmétique

La liste blanche de `compare-bodies.ps1` est **insensible à la casse**. Y inscrire un nom qui
ressemble à une variable locale courante rend l'oracle aveugle à de vrais changements sur ces
lignes. `Prims` a été choisi plutôt que `Prim` parce que `prim` est un local dans 196 lignes du code
de dessin. Vérifier avant de nommer :

```bash
grep -rn --include=*.cs -iE "\bnomcandidat\b" OpenForesia/src | wc -l
```

Les occurrences **en commentaire** sont sans risque : l'oracle retire les commentaires avant de
normaliser. Noms pris : `Ui`, `Prims`, `Saves`, `Vid`, `Inp`.

## Ce qui reste statique, et pourquoi

Une classe mixte n'est pas un compromis raté. Trois motifs rencontrés, tous légitimes :

* **`[UnmanagedCallersOnly]`** exige une méthode statique. Si son corps est une une-ligne qui écrit
  une globale (`SaveData`), rien d'autre à faire ; si son corps appelle d'autres membres
  (`Video.MoviePlayerWindowProc`), il rejoint l'instance par le point d'entrée statique (`Vid.`) ;
  s'il n'utilise que ses paramètres (`Input.CountJoystickCallback`), il reste autonome.
* **Un lambda `static`** garde son mot-clé et passe par le point d'entrée : le `static` d'un lambda
  est une garantie de non-capture, on ne la brade pas pour économiser une qualification.
* **Une décision d'hôte** lue par `src/Host/` à l'initialisation de ses propres types
  (`Video.s_WindowedStartup`) reste statique : en faire un champ d'instance coupleraient cette init
  à la construction complète des sous-systèmes.

## Le piège qui a coûté le plus cher — à ne pas rouvrir

Les sous-systèmes de `Subsystems.cs` sont **paresseux**, `Mem` est **immédiat**. Ce n'est pas un
détail de style :

```
[ModuleInitializer] → TryPrepareDisplayListArena() → lit un champ statique de Blaze
                    → construit tout ce qui est immédiat      ← devant la course
                    → PUIS réclame l'arène basse              ← la course
```

La réclamation ne lit que `Mem`. Tant que les quatre points d'entrée étaient `static readonly`,
chaque conversion ajoutait son travail d'allocation devant une course non déterministe : mesuré
**5 échecs sur 8** contre 1 sur 4 documenté. Rendus paresseux : retour à 25 %.

**Toute nouvelle conversion doit donc être vérifiée sur le taux de course**, pas seulement sur le
build et le débit — ce dernier était vert pendant toute la régression. La boucle de mesure est dans
`test-protocol.md`, section « relance détectée ». **Seize essais minimum, machine au repos** : à ce
taux, huit essais ne distinguent pas 25 % de 40 %, et un build concurrent fausse le résultat
(mesuré 44 % sous charge, 12 % au repos sur le même binaire).

## Ce que l'étape C laissera ouvert

* **L'étape D** — `Screens/` en 12 classes (~13 800 l.), à ne commencer que quand les écrans
  cesseront de bouger. `Screens/State03/WorldMap.cs` est le front de la priorité 2.
* **`GameMemory` reste incomplet** : il modélise ~110 des 398 symboles `.data`. Le reste est
  dispersé dans les fichiers qui les utilisent. Ce n'est pas bloquant, mais chaque conversion en
  rapatrie une poignée.
* **Les cinq défauts d'aliasing** que `OpenForesia.Tests` signale (3 tests rouges **attendus**).
  Les corriger change le comportement → changement séparé. `0x00566c70` doit être traité **avant**
  la reprise de la carte du monde.
* La réparation des **~870 lignes à encodage double** (`repair-double-encoded-comments`, ouvert,
  non commencé). Commentaires et journaux seulement, rien de visible en jeu.

---

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
