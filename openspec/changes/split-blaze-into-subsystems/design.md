## Context

Voir `proposal.md` — *Why* pour la motivation. Ce qui suit n'est que l'état mesuré qui contraint
l'approche.

**Cinq mesures décident du découpage.**

| Mesure | Valeur | Conséquence |
|---|---|---|
| `Blaze` est déjà `partial` | 3 fichiers l'utilisent (`Blaze.cs`, `DeviceShim.cs`, `SyntheticDevice.cs`) | le découpage en fichiers est **sémantiquement nul** |
| Globales `s_` distinctes | 593 | dont **333 mono-région (56 %)**, **260 transversales (44 %)**, **59 sur 4+ régions** |
| Définitions par plage d'adresses — **moteur** | 90 à 100 % dans une plage unique | l'adresse est une règle de rangement fiable |
| Définitions par plage d'adresses — **écrans** | 38 à 55 % | l'adresse ne dit rien ; seul le graphe d'appel tranche |
| Lignes à encodage double | 871, réparties L1→L20255 | commentaires et journaux seulement |

L'asymétrie de la troisième et de la quatrième ligne n'est pas un accident de présentation :
l'éditeur de liens de 1998 a groupé les unités de compilation d'un sous-système, alors qu'un écran
appelle des aides prises partout. C'est elle qui donne la règle de routage.

**La contrainte dominante du dépôt** est le défaut d'aliasing, corrigé **seize fois** : deux
symboles voisins d'adresse déclarés séparément ne partagent plus leur mémoire, et une écriture faite
par un bout n'est jamais lue par l'autre. Il naît toujours du même geste — **séparer ce que le
binaire garde ensemble**. Un découpage en fichiers est précisément ce geste, appliqué à grande
échelle. Tout le reste de ce document existe pour l'empêcher de se reproduire.

## Goals / Non-Goals

**Goals :**

- Qu'un lecteur sache, **au chemin du fichier**, s'il regarde de la transcription fidèle ou notre
  échafaudage.
- Qu'une fonction dont on vient d'établir le rôle ait **une destination mécanique**, sans débat.
- Que le contrôle d'adjacence d'adresses — celui qui attrape l'aliasing — reste **une seule
  consultation** après le découpage.
- Que chaque étape soit **vérifiable à l'octet**, donc réversible sans jugement.

**Non-Goals :**

- Créer des classes encapsulées. Le binaire n'a pas d'encapsulation ; en simuler une oblige à
  déplacer de l'état, ce qui est le défaut à éviter.
- Réparer les 871 lignes à encodage double. Changement séparé, voir *Risks*.
- Renommer des `FUN_` ou avancer une reconstruction. Ce changement ne fait que **déplacer**.
- Introduire une taille maximale de fichier.

## Decisions

### D1 — Des fichiers, pas des classes

`partial class Blaze` partout dans `src/Blaze/`.

> **Correction du 2026-08-19, trouvée en exécutant le groupe 4.** Ce document affirmait que le
> découpage en fichiers est « sémantiquement nul ». **C'est faux, et le jeu a planté pour le
> prouver.**
>
> Les initialiseurs de champs statiques d'une classe partielle s'exécutent dans l'ordre où le
> compilateur reçoit les **fichiers**, ordre qui n'est pas spécifié. Tant que tout vivait dans
> `Blaze.cs`, l'ordre textuel garantissait que l'arène `0x005e9000` (L5373) était allouée avant
> `s_SlotArea_005e9180 = Arena(0x005e9180)` (L16663). Une fois `Arena()` parti dans
> `Memory/Arena.GameState.cs`, `Blaze.cs` s'initialise d'abord : `Arena()` rend `null + décalage`,
> et `CurrentPadState()` lève une `NullReferenceException` dès la première image du menu.
>
> **Huit champs** étaient dans ce cas, tous de la forme `static readonly … = Arena(0x…)`. Ils
> deviennent des **propriétés calculées** `=> Arena(0x…)` : la base d'arène est allouée une fois et
> ne change jamais, donc réévaluer `base + décalage` rend toujours le même pointeur. Le motif est
> déjà celui du dépôt (`s_PlayerSlots_005e9188 => s_SlotArea_005e9180 + 8`), et la conversion ne
> change pas le nombre de lignes, donc pas le manifeste.
>
> **La règle pour les groupes 5 et 6** : un champ `static readonly` dont l'initialiseur lit un
> membre statique qui partira dans un **autre** fichier doit devenir une propriété. Les dépendances
> intra-fichier restent sûres — l'ordre textuel est garanti à l'intérieur d'un fichier.
>
> Ce que la mesure a aussi montré : les 4 champs dérivés de `s_EnvBlock_005e9880` et la paire
> `s_GlyphWidths*` restent dans le même fichier que leur dépendance, donc intacts. Restent
> **41 champs initialisés par un appel de méthode** (`Build*`, `ToAnsiZ`, `Alloc*`) : une méthode
> n'a pas besoin d'être « initialisée », mais si son corps lit un champ statique d'un autre
> fichier, le même piège s'applique. À surveiller au groupe 5.

*Alternative écartée : de vraies classes par sous-système.* 44 % des globales traversent les
frontières envisagées. Une vraie classe force alors un choix : soit 260 champs passent `internal`
et la classe ne cache rien — elle ment ; soit on déplace l'état pour fermer la classe, et c'est
exactement le défaut d'aliasing. `partial` donne **la totalité du bénéfice de navigation pour aucun
risque sémantique**, et le dépôt a déjà pris cette décision deux fois.

Une vraie classe ne naît que si sa clôture est **mesurée**, pas supposée — voir D4.

### D2 — Deux axes orthogonaux, jamais alignés de force

```
   AXE A — LE CODE     se range par RESPONSABILITÉ   (fichiers)
   AXE B — L'ÉTAT      se range par ADRESSE          (arènes)
```

Corollaire, et c'est la règle qui protège le dépôt :

> **Une globale ne se déplace jamais pour rendre un fichier autonome.**
> Elle se déplace uniquement quand la carte d'adresses le dit. Les arènes se déplacent **en bloc**.

*Alternative écartée : colocaliser chaque globale avec le code qui l'utilise.* Séduisant — on lit
l'écran, on voit son état — mais deux voisins d'adresse finissent alors dans deux fichiers, et
l'adjacence qui révèle l'aliasing devient invisible. La résolution retenue **sépare le diagnostic
du rangement** : les mono-région suivent leur code, les arènes restent entières, et l'index trié par
adresse (D7) conserve la vue d'ensemble.

**La règle d'étendue d'arène** (ajoutée après les mesures des tâches 1.2 et 1.3).

Le dépôt modélise **huit** blocs adressés par décalage, pas un seul : `EntityParams` (0x00551640),
`EntityState` (0x00571500), `CameraBlock` (0x005de030), `ItemMenuArena` (0x005de100), `PartyList`
(0x005e85c0), `GameArena` (0x005e9000), `EntityArena` (0x005eca00, 0x14200 octets) et `ViewBlock`
(0x006067c0). **142 symboles tombent dans l'une de ces étendues.**

Mais les 142 ne présentent pas le même risque, et la mesure le montre :

| Forme de déclaration | Nombre | Risque d'aliasing |
|---|---:|---|
| **Vue** — passe par l'accesseur (`=> ref *Arena(0x…)`) | 61 | **aucun** : la mémoire est déjà partagée |
| **Allocation propre** — `NativeMemory.Alloc…` / `new byte[]` | 11 | **le défaut en formation** |
| Autre (constantes, décalages dérivés) | 70 | aucun |

D'où la formulation retenue, qui **protège le stockage, pas les vues** :

> Un symbole dont l'adresse tombe dans l'étendue d'une arène modélisée :
> - s'il **passe par l'accesseur** de l'arène, il **reste avec son code** — il ne peut pas aliaser ;
> - s'il a sa **propre allocation**, il **part dans le fichier de l'arène** et devient un décalage.
>
> Les **déclarations** d'arène elles-mêmes (constante de base, allocation, accesseur) vivent toutes
> dans `Memory/`, triées par adresse.

*Alternative écartée : faire migrer les 142.* Elle déplaçait 61 vues qui ne peuvent pas aliaser, et
aurait vidé `Screens/State06/Inventory.cs` de son état pour aucun gain de sûreté.

Sur les 11 allocations propres, **7 sont les bases d'arène elles-mêmes** — légitimes. Les **4
restantes sont des suspects réels** : une allocation indépendante à une adresse qu'une arène couvre
déjà, donc une écriture par l'arène et une lecture par le symbole ne se voient pas.

| Symbole | Adresse | Arène englobante | Fichier actuel |
|---|---|---|---|
| `s_EntityList_005de558` | `0x005de558` | `ItemMenuArena` | Engine/Resources.cs |
| `s_ModelResultB_005f7b40` | `0x005f7b40` | `EntityArena` | State03/WorldMap.cs |
| `s_ModelResultA_005fa588` | `0x005fa588` | `EntityArena` | State03/WorldMap.cs |
| `s_CameraMatrix_006008e0` | `0x006008e0` | `EntityArena` | Memory/DataTables.cs |

Les trois derniers sont dans le voisinage exact que la priorité 2 doit lire (`DAT_005f7bc0`,
`DAT_005f7cc0`, `DAT_005fa508`). **Constatés, non corrigés** — les corriger changerait le
comportement, ce que ce changement s'interdit. Consignés en 8.3.

Sans la règle, les « scalaires isolés » de l'arène `0x005e9000 … 0x005ea600` — ceux que le
commentaire de l'arène signale déjà en `TODO(REVERSE)` — se disperseraient sur **quatre fichiers** :

```
  0x005e9820  s_Unknown_005e9820      -> Memory/Arena.GameState.cs
  0x005e9824  s_Unknown_005e9824      -> Engine/Resources.cs
  0x005e9828  s_Unknown_005e9828      -> Screens/State03/WorldMap.cs
  0x005e9960  s_FadeCounter_005e9960  -> Screens/State00/MainMenu.cs
  0x005e9980  s_StateBank6_005e9980   -> Memory/Arena.GameState.cs
```

Ces symboles sont **dans** l'arène. Les laisser suivre leur code contredirait la règle du bloc et
refabriquerait le défaut que tout ce changement cherche à éviter. La règle d'étendue referme du
même coup le `TODO(REVERSE)` que le commentaire de l'arène porte déjà.

La mesure qui justifie l'ensemble : **107 paires de symboles adjacents (écart ≤ 64 octets)
atterrissent dans deux fichiers différents**, et **23 adresses portent plus d'un symbole C#**.

### D3 — `src/Host/` sort de `src/Blaze/`

Les cinq fichiers du remplacement de la chaîne d'affichage forment **un sous-système à un seul
travail** : faire afficher le jeu dans une fenêtre Direct3D 11 au lieu de laisser `3dhelp.dll`
prendre l'écran en mode exclusif et changer le mode du bureau.

```
   winsys3d.dll  LoadLibraryA("3dhelp") → GetProcAddress("Create3DDevice")
        │  ← détourné dans la table d'imports
        ▼
   SyntheticDevice   rend NOTRE device ; 3dhelp ne s'initialise jamais
        ▼
   DeviceShim        façade de vtable, slots chauds détournés
        ▼
   D3D11Backend      sommets DÉJÀ projetés (portage PSX : le GTE projette côté CPU)
        ▼
   D3D11Window       aucun appelant ne la passe à DirectDraw → pas de mode exclusif
   StartupSurface    régime GDI titre/attract : DIB section pour StretchDIBits
```

La raison de les extraire n'est pas le rangement, c'est que **la règle qui les gouverne est
l'inverse de celle du reste du dépôt** :

| | `src/Blaze/` | `src/Host/` |
|---|---|---|
| Règle | *fidelity beats compilability* | ça doit **marcher** |
| Source de vérité | `Blaze.c` | aucune |
| Un `TODO(REVERSE)` y a un sens ? | oui | non |
| Refactorisable librement ? | non | oui |

Aujourd'hui les deux cohabitent et rien ne signale au lecteur laquelle s'applique. C'est le seul
endroit du dépôt où la disposition induit activement en erreur.

*Nom retenu `Host/` plutôt que `Display/`* : le jour où on détourne autre chose que l'affichage,
il n'y a rien à renommer.

### D4 — Deux vraies classes, et seulement deux

`DeviceShim` et `SyntheticDevice` sortent de `partial class Blaze`. Le test appliqué est la
**clôture mesurée** :

| Fichier | symboles `s_` utilisés | déclarés sur place | clôture |
|---|---:|---:|---:|
| `DeviceShim.cs` | 29 | 27 | **93 %** |
| `SyntheticDevice.cs` | 19 | 18 | **95 %** |

Ce sont les deux seuls fichiers du dépôt qui passent ce test aujourd'hui. Le pont résiduel fait
quatre symboles :

**Corrigé après exécution (2026-08-19).** L'estimation ci-dessus était fondée sur les symboles `s_`
seuls ; le compilateur a donné la vraie surface, et elle est **plus large et bidirectionnelle**.
25 symboles, en trois groupes :

| Groupe | Nombre | Symboles | Traitement |
|---|---:|---|---|
| **Host → Blaze** | 5 | `ToAnsiZ`, `RenderSizeFromSizeSetting`, `s_WindowedStartup`, `GetModuleHandleA`, `VirtualAlloc` | `internal` + `using static OpenForesia.Blaze.Blaze;` |
| **Blaze → Host** | 4 | `InstallSyntheticDevice`, `InstallDeviceShim`, `CreateGameWindow`, `LogDisplayMode` | qualifiés explicitement au site d'appel |
| **Host ↔ Host** | 16 | les thunks `Shim_*`, `Guarded`, `DeviceVtableSlots`, `s_SyntheticDevice` | `internal` + `using static` |

Deux corrections de fond :

**`s_WindowedStartup` ne part pas chez `Host/`.** Sa nature est bien une décision d'hôte, mais son
usage ne l'est pas : **huit de ses dix sites de lecture sont dans `Blaze.cs`** — le niveau de
coopération DirectDraw et les quatre sites `VfwClass`. Le déplacer aurait fait lire `Blaze` depuis
`Host`. Il reste dans la transcription et passe `internal`.

**La dépendance n'est pas à sens unique, et ne peut pas l'être.** Le démarrage de `Blaze` doit
appeler les installateurs de `Host` : une façade s'installe nécessairement depuis le chemin qu'elle
détourne. La propriété à viser n'était pas la bonne. Ce qui est vrai, et vérifié, c'est que la
couture est **étroite et nommée** : les 4 appels `Blaze → Host` sont tous des points d'installation,
qualifiés au site d'appel pour rester visibles, et non du couplage diffus.

*Ce que cela ne remet pas en cause :* la clôture mesurée (93 % / 95 %) portait sur l'**état**, et
elle tient — aucun champ n'a eu à traverser la frontière. Ce sont les **méthodes** que le premier
comptage n'avait pas cherchées.

Bénéfice supplémentaire, et non secondaire : `SyntheticDevice.cs` documente déjà que l'ordre
d'initialisation des champs statiques **entre fichiers d'une même classe partielle n'est pas
garanti**, ce qui l'a forcé à un contournement en propriété calculée. Sortir de la classe partielle
supprime le danger — C# garantit l'ordre au sein d'une classe, et l'accès inter-classes déclenche
l'initialiseur de la cible de façon déterministe.

### D5 — La règle de routage

C'est ce qui rend l'architecture auto-entretenue : elle répond mécaniquement à « je viens de nommer
`FUN_00XXXXXX`, où va-t-elle ? ».

```
   ① Son VA tombe dans une plage MOTEUR connue ?  ──oui──►  Engine/<ce fichier>.cs
   ② Un SEUL écran l'appelle ?                    ──oui──►  Screens/StateNN/<fichier>.cs
   ③ PLUSIEURS écrans l'appellent ?               ──oui──►  promotion en Engine/,
                                                             et on lui trouve un nom
   ④ sinon                                        ────────►  RESTE CHEZ SON APPELANT
                                                             jusqu'à ce qu'on sache
```

**Les exceptions relevées en 1.3 sont appliquées, pas subies.** 64 fonctions portent un VA hors de
la plage attendue de leur fichier ; c'est un signal, pas un verdict, puisqu'un écran appelle
légitimement des aides prises ailleurs. Après relecture, **14 relèvent bien de la règle ①** et sont
redirigées :

| Fonction | VA | Découpe brute | Redirigée vers |
|---|---|---|---|
| `WriteIniInt`, `NormalizePath`, `OpenExistingFileReadOnly` | `00a26…` | Audio | `Platform/Winsys3d.cs` |
| `LoadKeyBindings`, `SaveKeyBindings` | `00523…` | Audio | `Engine/Input.cs` |
| `LoadOptionsIni`, `SaveOptionsIni` | `0050f…` | Audio | `Screens/State00/Options.cs` |
| `CheckDeviceCapabilityFlag` | `0050f120` | Input | `Screens/State00/Options.cs` |
| `SetShadingDefaults`, `InitViewportSurfaces` | `00529e…` | Input | `Engine/Video.cs` |
| `RunGameLoop` | `0056c96c` | Resources | `Screens/GameStates.cs` |
| `CrtMemMove` | `00547a00` | UiText | `Platform/Crt.cs` |
| `PlayUiSoundError` | `0053dbc0` | Options | `Engine/Audio.cs` |
| `AnimatePalettes` | `00533000` | Input | `Screens/State03/WorldMap.cs` |

Les 50 autres sont du légitime — un écran qui appelle `PlayUiSound38`, `CrtRand`, `BuildMenuLabels`.

La règle est fondée sur la mesure, pas sur le goût : ① n'est fiable que parce que le moteur est
concentré à 90–100 % dans une plage, et ② est nécessaire précisément parce que les écrans ne le sont
qu'à 38–55 %.

**Le passage de ② à ③ est le seul événement qui crée un nouveau fichier moteur.** L'architecture
grandit sur preuve. `Engine/Primitives2d.cs` est né de cette règle pendant la cartographie.

*Alternative écartée : un fichier de rebut `Unclassified.cs`.* La branche ④ se replie mieux sur
« reste chez son appelant » — le projet avance par le graphe d'appel, donc une fonction fraîchement
transcrite est presque toujours une feuille de l'écran en cours, et la séparer de son appelant coûte
plus qu'elle ne rapporte. La règle couvre même les corps seulement adressés (procédures de fenêtre,
slots de vtable) : leur « appelant » est le code qui installe leur adresse. Aucun cas orphelin ne
subsiste, donc aucun fichier de rebut n'est nécessaire.

### D6 — Découper au bandeau est faux : inventaire par fonction d'abord

Les 37 bandeaux `// ====` sont le sommaire naturel, mais **trois sur 37 sont démontrés faux** (voir
`proposal.md` — *Why*). Le taux d'erreur constaté est de 8 %, sur un échantillon où l'on n'a examiné
que les plus grosses régions : rien ne garantit que le reste soit sain.

Donc la **tâche 1 est un inventaire des 600 fonctions** — nom, adresse virtuelle Ghidra, ligne
actuelle, fichier cible — généré puis **relu et validé avant qu'une seule ligne bouge**. C'est
l'application directe de la règle de méthode du dépôt : *mesurer avant d'écrire*.

### D7 — L'index d'adresses rejoint `Documentation/Reference/DataSymbols.md`

`DataSymbols.md` (874 l.) est déjà **clé par l'adresse virtuelle Ghidra**. L'index dont le découpage
a besoin l'est aussi. Ils sont complémentaires, pas redondants :

| | clé | contenu |
|---|---|---|
| `DataSymbols.md`, existant | VA | **ce que le binaire contient** à cette adresse |
| section ajoutée | VA | **où on l'a modélisé** : symbole C# → fichier |

Le contrôle anti-aliasing — « ce `DAT_` isolé est-il adjacent à une région déjà modélisée ? » — a
besoin des deux à la fois. *Alternative écartée : un fichier séparé à la racine.* Deux fichiers
triés par la même clé garantissent qu'on n'en consultera qu'un.

### D8 — Pas de taille maximale de fichier

`Screens/State06/Inventory.cs` fera 2 920 lignes pour trois sous-écrans d'un même menu. C'est
cohérent. Une règle « max N lignes » redécouperait des unités cohérentes selon un critère
arbitraire, et ferait perdre exactement ce que le changement cherche. **La métrique est la
cohérence.**

### D9 — Uniformité des répertoires d'état

`Screens/StateNN/` existe même quand il ne contient qu'un fichier. `State03/` accueillera
`WorldMapTick.cs` (les 1 226 lignes de `FUN_00516fc0`) dès la reprise de la priorité 2 ; le
répertoire est déjà là, donc l'ajout ne rouvre pas la question du rangement.

`GameStates.cs` et `DebugParty.cs` restent à la racine de `Screens/` : le premier est le répartiteur
de tous les états, le second n'est pas un état numéroté — il est verrouillé par
`s_DebugModeLatch_005633a8`.

### D10 — L'arborescence retenue

```
src/
├─ Host/  ······································· 3 459   NON soumis à la fidélité
│   ├─ D3D11Backend.cs        1 038  ·  SyntheticDevice.cs   912
│   ├─ DeviceShim.cs            870  ·  D3D11Window.cs       445
│   └─ StartupSurface.cs        194     (+ s_WindowedStartup migré)
│
└─ Blaze/  ······································ 20 220   soumis à la fidélité
    ├─ Blaze.cs                  40   en-tête, usings, déclaration partielle
    │
    ├─ Platform/  ·······················  1 762
    │   ├─ Winsys3d.cs          668   file clavier + arène liste d'affichage + banc GTE
    │   ├─ Startup.cs           496   entry + RunApplication + RunMainLoop + extinction
    │   ├─ Crt.cs               372   ·  Win32.cs            226
    │
    ├─ Memory/  ·························   ~680
    │   ├─ DataTables.cs        336   les Build* : tables .data recouvrées
    │   ├─ Arena.GameState.cs   259   0x005e9000 .. 0x005ea600 — en bloc, jamais éclatée
    │   ├─ Arenas.cs            ~55   les sept autres blocs adressés par décalage :
    │   │                             0x00551640 EntityParams · 0x00571500 EntityState
    │   │                             0x005de030 CameraBlock  · 0x005de100 ItemMenuArena
    │   │                             0x005e85c0 PartyList    · 0x005eca00 EntityArena
    │   │                             0x006067c0 ViewBlock — **triés par adresse**
    │   └─ Globals.cs           ~30   vide au départ, se remplit sur preuve
    │
    ├─ Engine/  ·························  7 808
    │   ├─ Audio.cs           1 422   0x00513 · 0x0053d
    │   ├─ UiText.cs          1 334   0x00544
    │   ├─ Input.cs           1 169   0x00523 · 0x00504
    │   ├─ Resources.cs       1 147   0x00529 · 0x0052a
    │   ├─ Video.cs             978   0x00512 · 0x00542
    │   ├─ SaveData.cs          595   E/S carte mémoire seule
    │   ├─ Model3d.cs           568   0x0051d
    │   ├─ UiWindow.cs          350   0x00545
    │   └─ Primitives2d.cs      245   ◄ créé par la règle ③
    │
    └─ Screens/  ························ 13 444
        ├─ GameStates.cs         89   le répartiteur FUN_00537ca0
        ├─ DebugParty.cs        737   mode de mise au point
        ├─ State00/  MainMenu.cs 516 · Options.cs 2 435
        ├─ State01/  PartySetup.cs                    536
        ├─ State02/  Destination.cs                   435
        ├─ State03/  WorldMap.cs                    1 109
        ├─ State04/  SaveSlots.cs 2 082 · CharacterCreation.cs ~1 490
        │            CharacterSelect.cs ~600
        └─ State06/  Inventory.cs 2 920 · ItemMenu.cs  495
```

Le total retombe sur **23 679 lignes**, cohérent avec les 23 673 mesurées à la ligne près (l'écart
est le préambule ajouté par fichier).

Deux rangements corrigent une erreur existante : `GameStates.cs` extrait le répartiteur
`RunGameStateTick` de la queue de la scrutation d'entrées où il est mal classé, et
`Platform/Startup.cs` récupère l'extinction que le bandeau « Not yet reconstructed » masquait.

## Risks / Trade-offs

**[Les bandeaux dérivent — 3 sur 37 démontrés faux]** → Inventaire par fonction en tâche 1, relu et
validé avant tout déplacement. Aucune ligne ne bouge sur la foi d'un bandeau.

**[La région à encodage double, 871 lignes]** → Tout déplacement passe par
`[IO.File]::ReadAllBytes` / `WriteAllBytes` ou les outils d'édition ; **jamais** `Get-Content` /
`Set-Content`, qui ont déjà aggravé le problème une fois. La réparation elle-même est **hors
périmètre** : le déplacement se vérifie par `cmp` octet à octet, la réparation par relecture. Mêler
les deux rend les deux invérifiables.

**[Déplacer 23 679 lignes peut casser silencieusement]** → Trois barreaux à chaque étape :
`dotnet build` ; `playtest.ps1` sur un écran de référence, conformément à `test-protocol.md` ; et
surtout la **comparaison octet à octet** de la plage déplacée contre sa version dans `git HEAD`.
Le troisième est le seul qui prouve l'absence de modification accidentelle.

**[Vérifier par un nombre plutôt que par l'écran]** → Le dépôt a déjà été trompé par un compte de
primitives identique alors que l'affichage était faux. La vérification `playtest.ps1` se lit **sur
la capture**, pas sur le compteur.

**[L'historique `git blame` se dilue]** → Un fichier de 23 673 lignes éclaté en 29 fait perdre à la
détection de renommage une partie de sa précision. Assumé : `REVERSE_STATUS.md` porte déjà
l'historique narratif, daté par session, et c'est lui qu'on consulte en pratique. Atténuation :
chaque étape de migration reste un déplacement pur, sans édition, ce qui maximise ce que la
détection de similarité peut retrouver.

**[L'ordre d'initialisation des champs statiques entre fichiers partiels]** → Le danger existe déjà
aujourd'hui et a déjà mordu une fois. Il n'augmente pas avec le nombre de fichiers, mais il devient
plus facile à rencontrer. Atténuation : ne pas introduire de nouveau champ statique dont
l'initialiseur lit un champ d'un autre fichier ; utiliser une propriété calculée, comme le
précédent l'a fait.

**[`CLAUDE.md` contredirait le dépôt]** → Il est mis à jour **dans le même changement**, pas dans un
suivant. La dernière tâche ne peut pas être reportée sans laisser le dépôt incohérent.

## Migration Plan

L'ordre minimise le risque : on commence par ce qui n'a pas de sémantique de jeu, on finit par ce
qui en a le plus.

```
  1 ── INVENTAIRE     600 fonctions → VA, ligne actuelle, fichier cible.
       │              Relu et validé. RIEN NE BOUGE AVANT.
  2 ── Host/          5 fichiers + les 2 sorties de partial Blaze + s_WindowedStartup
  3 ── Platform/      ~1 760 l., aucune sémantique de jeu
  4 ── Memory/        les arènes EN BLOC + la section de DataSymbols.md
  5 ── Engine/        un fichier par étape, build entre chaque
  6 ── Screens/       par répertoire d'état
  7 ── DOCS           REVERSE_STATUS.md déplacé, ARCHITECTURE.md créé, CLAUDE.md réécrit
```

**Retour arrière** : chaque étape est un déplacement pur, vérifié à l'octet, et sans dépendance sur
la suivante. Revenir en arrière est un `git revert` d'une étape, jamais une reconstruction.

## Open Questions

- **Que devient `Memory/Globals.cs` à terme ?** Il naît vide. Les 260 globales transversales n'y
  entrent que si un contrôle d'adjacence montre qu'elles appartiennent à une région contiguë — donc
  au fil des découvertes, pas d'avance. Répondre plus tard ne change ni l'approche ni le découpage.

- **`Host/` accueillera-t-il autre chose que l'affichage ?** Le nom a été choisi pour que la réponse
  n'oblige à rien renommer. Sans effet sur ce changement.
