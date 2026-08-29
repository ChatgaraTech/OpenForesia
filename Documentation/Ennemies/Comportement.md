# Le comportement des ennemis — combien, où, et dans quel ordre

Relevé du **2026-08-28**, à froid : aucune valeur n'est estimée, tout vient de `Blaze.exe`.
Reproductible par `sh OpenForesia.Tools/census-ai-handlers.sh`.

---

## Ce qu'on cherchait à savoir

Trois plantages successifs en forêt le même jour ont tous eu la même cause : un **gestionnaire
d'entité de scène non transcrit**. Après en avoir écrit deux, la question s'est posée — combien y
en a-t-il, et faut-il vraiment attendre qu'ils plantent un par un ?

**Ce fichier répond par un compte exact.** Il ne remplace pas `REVERSE_STATUS.md`, qui garde le
détail de chaque fonction écrite ; il donne la carte.

---

## Le mécanisme, en trois phrases

La passe `FUN_0050e680` balaie les 0x20 entités de scène de la table `0x005ecaa0` et appelle, pour
chacune, `(&PTR_0056ad48)[entité[5]]` — son **sous-type** indexe une table de sauts de 0x20 entrées.

Cette table est **rechargée à chaque sous-zone** : le chargement lit `PTR_PTR_00566d98` à l'index
`zone * 0x10 + sous-zone`, y trouve l'adresse d'un bloc de 0x20 pointeurs, et le recopie sur
`0x0056ad48`. **Un numéro de sous-type ne veut donc rien dire tout seul** — le sous-type 2 est une
bête en forêt et tout autre chose ailleurs.

Le gestionnaire ainsi désigné est appelé une fois par image, avec l'entité pour seul argument. C'est
lui qui décide ce que la créature fait : attendre, se tourner, approcher, errer, frapper.

---

## Le compte

| | |
|---|---|
| emplacements de sous-type remplis, toutes zones | **509** |
| emplacements laissés à la souche vide `0x00401FA0` | 1 987 |
| **gestionnaires distincts** | **166** |
| dont routines d'intelligence complètes | **112** |
| dont gestionnaires simples (décor, coffres, déclencheurs) | 54 |
| **écrits au 2026-08-29** | **4** (`0x00486EE0`, `0x00499DE0`, `0x0049A360`, `0x0049A980`) |

Un gestionnaire est compté comme **routine d'intelligence** s'il appelle `FUN_00535F20`, la lecture
de situation — c'est le marqueur sûr de la famille, et il ne se partage avec rien d'autre.

Les 166 s'étendent de `0x00486EE0` à `0x00502CD0`, et **aucun n'est dans `Blaze.c`** : Ghidra n'a
décompilé que cinq corps sur toute cette plage. Tous demandent le désassembleur.

### Par zone

| zone | lieu | gestionnaires | dont IA | IA écrites |
|---|---|---|---|---|
| 0 | L'auberge | 2 | 1 | 0 |
| 1 | La mine abandonnée | 23 | 17 | 0 |
| 2 | La montagne du dragon de feu | 2 | 1 | 0 |
| **3** | **La forêt** | **23** | **22** | **3** |
| 4 | La vieille tour | 22 | 10 | 0 |
| 5 | Le Labyrinthe des morts | 19 | 13 | 0 |
| 6 | Le château de l'immortel | 21 | 9 | 0 |
| 7 | Les ruines souterraines | 6 | 5 | 0 |
| 8 | La vallée d'argent | 4 | 4 | 0 |
| 9 | Les ruines dans le lac | 5 | 5 | 0 |
| 10 | La grotte scellée | 39 | 25 | 0 |

Les totaux par zone dépassent 166 : **plusieurs zones partagent des gestionnaires**, et une même
zone réutilise le même bloc sur plusieurs sous-zones.

---

## Ce n'est pas « une fonction par ennemi »

Le bestiaire relevé sur le CD compte **555 espèces**. Il y a **112 routines d'intelligence**. Une
routine n'est donc pas un monstre : c'est un **comportement** — « charge en ligne droite et bondit »,
« tourne autour et ne frappe que de près » — et des dizaines d'espèces partagent le même, avec des
statistiques, un modèle et un butin différents.

C'est la table de sous-zone qui fait le lien : elle dit quel sous-type utilise quelle routine.

---

## La forêt (zone 3), en détail

Dix sous-zones, **trois blocs** de table seulement. C'est ce qui rend la forêt traitable d'un bloc.

| bloc | sous-zones | lieux | gestionnaires |
|---|---|---|---|
| A | 0, 1, 2, 3, 4 | La forêt aux ruines, le pré au bord du lac, le hangar à bateaux, les chutes d'eau de Maldo, le terrain marécageux, la forêt, la cave de la cabane | 11 |
| B | 6, 7, 8 | L'île aux ruines, un sentier raide et pierreux, la vallée ensablée | 7 |
| C | 5, 9 | Le lieu de coupe, la grotte souterraine | 5 |

### Bloc A — les sous-zones à ciel ouvert, celles où le joueur arrive

La colonne « bête » vient du croisement `especes.csv` (colonne `ia`) × `modeles.csv` (le nom), fait
le 2026-08-29 ; les trois premières sont confirmées **en jeu** par le bilan de la salle d'essai.

| sous-type | gestionnaire | bête | état |
|---|---|---|---|
| 0 | `0x00499DE0` | Fourmi géante | **écrit** |
| 1 | `0x0049A360` | Coccinelle | **écrit** |
| 2 | `0x0049A980` | Goblin | **écrit** |
| 3 | `0x0049B220` | Abeille · Abeille tueuse | — |
| 4 | `0x0049B840` | Grizzly | — |
| 5 | `0x0049C0B0` | Crabe géant | — |
| 6 | `0x0049C8E0` | Poisson volant | — |
| 7 | `0x0049D030` | Homme-lézard | — |
| 8 | `0x0049DA40` | Goblin | — |
| 9 | `0x0049E190` | Goblin-sorcier | — |
| 18 | `0x0049E920` | Crotale | — |

**Les onze se suivent dans le binaire**, dans l'ordre des sous-types. Les deux plantages signalés
étaient les sous-types 0 et 2 ; les neuf autres attendent, et ce sont eux qui se réveilleront à la
prochaine rencontre.

Une espèce de la sous-zone 3 porte le **sous-type 30, « Barricade »**, absent de ce tableau : son
créneau ne fait pas partie des onze relevés, donc il pointe sur la souche vide ou sur un
gestionnaire simple. À vérifier quand on ira dans cette sous-zone — le nom sent le décor.

### Bloc B — l'île, le sentier, la vallée

| sous-type | gestionnaire | bête |
|---|---|---|
| 7 | `0x0049F0C0` | Homme-lézard · Lézard noir |
| 10 | `0x0049FAD0` | Trent |
| 11 | `0x004A0480` | Fleur du mal |
| 12 | `0x004A13E0` | Wyrm |
| 13 | `0x004A1D50` | Varan |
| 14 | `0x004A2C90` | Géant |
| 15 | `0x004A35B0` | Willie |

**Le même numéro ne désigne pas la même bête d'un bloc à l'autre** : le sous-type 7 est
`0x0049D030` en bloc A et `0x0049F0C0` en bloc B. C'est le rappel concret que la table est
rechargée par sous-zone.

### Bloc C — le lieu de coupe et la grotte

| sous-type | gestionnaire | bête | genre |
|---|---|---|---|
| 3 | `0x004A3C30` | Abeille tueuse | IA |
| 4 | `0x004A4270` | Grizzly | IA |
| 16 | `0x004A4AE0` | Méchant ours | IA |
| 17 | `0x004A54E0` | Behemoth | IA |
| 19 | `0x004A5F10` | Abeille tueuse | simple |

**Dix-neuf routines restent pour rendre la forêt entièrement stable**, dont huit pour les seules
sous-zones d'arrivée.

---

## Pourquoi une par une, et pourquoi ce n'est pas si grave

**Dans le binaire, ce sont bel et bien 112 fonctions distinctes.** Le compilateur de 1998 les a
émises séparément, chacune avec ses propres constantes — il n'existe aucune fonction partagée qu'on
pourrait transcrire une seule fois. Il faut donc les prendre une par une.

**Mais le squelette est identique**, au sens de *charpente de code* : trois temps — décider,
appliquer, replanifier — et **toutes les feuilles sont déjà écrites** :

| feuille | rôle |
|---|---|
| `ReadSceneEntitySituation_00535F20` | hardiesse, distance, cap, vitesse, traits |
| `IntegrateSceneEntityMotion_00488160` | amortissement des vitesses et avance |
| `EndSceneEntityAnimation_004DFAF0` | fin de geste |
| `InterruptSceneEntity_004E3770` | remise à l'attente sur coup reçu |
| `SweepPartyForMeleeHit_00519C30` | qui est sous le gabarit, et la parade |
| `TestMeleeReach_00535A90` | le croisement des deux figures |
| `ResolveMeleeHit_00539B80` | dégâts, éléments, expérience, mort |
| `TurnSceneEntityTowardTarget`, `SceneEntityDelay`, `HesitateSceneEntity`, `RestSceneEntity`, `HoldSceneEntityAtRange`, `WanderSceneEntity` | les postures partagées |

Écrire la deuxième routine (`0x00499DE0`) n'a demandé **aucune** modification de ces feuilles, sauf
un paramètre sur `HoldSceneEntityAtRange`. C'est la preuve que le découpage tient.

### Ce qui change d'une routine à l'autre

Cinq constantes, et parfois la forme d'une posture :

| | `0x0049A980` | `0x00499DE0` |
|---|---|---|
| sonde : tolérance en hauteur, portée | `0x64`, `0x50` | `0x50`, `0x3c` |
| image du geste où le coup porte | 8 | 12 |
| nombre de postures | 5 | 4 |
| cône de virage, posture 1 | `hardiesse * 2 + 0x80` | `hardiesse + 0x20` |
| cône de virage, posture 2 | `hardiesse * 2 + 0x80` | `hardiesse + 0x40` |
| bond, recul, esquive | oui | aucun |
| posture 3 | marche ou bondit | pivote vers un cap rangé en `+0x10` |

### Recette pour la suivante

1. `OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe <adresse> 2600` — l'adresse est dans le
   message de plantage, dans le journal `[LACUNE]` du filet, ou dans les tables ci-dessus.
2. Repérer la fin sur le bourrage de `nop`, et relever la **table de sauts** qui suit immédiatement
   (une entrée par posture) avec le verbe `at`.
3. Calquer sur `TickSceneEntityAi_0049A980` (cinq postures) ou `TickSceneEntityAi_00499DE0`
   (quatre), en substituant les constantes.
4. Ajouter le `case` au répartiteur `DispatchEntityHandler`, et garder le relevé sous
   `Legacy/Raw/Blaze/`.

Compter **environ une heure** par routine, et beaucoup moins dès qu'on en enchaîne plusieurs du
même bloc — elles se suivent dans le binaire et se ressemblent.

---


### La quatrième routine : ce que la Coccinelle a appris (2026-08-29)

Elle est la première **bête volante** écrite, et elle casse trois hypothèses que les deux premières
avaient laissé s'installer.

| | terrestres (`0x00499DE0`, `0x0049A980`) | Coccinelle (`0x0049A360`) |
|---|---|---|
| intégration du mouvement | `FUN_00488160`, avec le rappel de `+0x9a` qui plaque au sol | `FUN_0049a8e0`, **sans ce rappel** |
| fin de geste | `FUN_004dfaf0` | **rien** — un bloc de vol la remplace |
| interruption | `FUN_004e3770` (`0x2000a882` / `0x817`) | `FUN_004a1cf0` (`0x8882` / `0x816`) |
| image où le coup porte | 8ᵉ ou 12ᵉ | **aucune** : elle balaie à chaque image du geste |
| poussée entre deux gestes | remise à zéro | **amortie** d'un seizième |
| cap d'errance | `+0x10` | **`+0x12`** |
| compte à rebours | `rand%10 + timidité/2 + 0x14` | `rand%10 + timidité/4 + 0x0a` |

**Le vol tient en dix lignes** : `+0x14` accumule une phase, et le sinus de cette phase fait osciller
la hauteur autour d'un plafond. Le plafond est le plus haut de `+0x60` (le sol sous la bête) et de
`+0x48`, recopié du `+0x60` de la cible — **elle se cale sur l'altitude de ce qu'elle poursuit**,
sans jamais descendre sous son propre sol. Pendant un geste l'oscillation cesse et elle prend de la
hauteur.

Sa sonde le disait déjà : `probe[4]` vaut `-0x100` là où les deux terrestres ont 0.

**Conséquence pour la suite du bloc A.** « Calquer sur une des deux sœurs en substituant cinq
constantes » n'est vrai qu'entre bêtes de même milieu. Le Poisson volant (sous-type 6) se calquera
sur celle-ci ; le Grizzly et le Crabe sur les terrestres. **Regarder d'abord quelle fonction
d'intégration la routine appelle** — c'est le tri le moins cher, une seule ligne du désassemblage.

### L'oracle qui manquait, et qu'on avait sous la main

Ce fichier disait — et c'est toujours vrai — qu'**aucune vérification automatique ne peut dire si un
comportement est fidèle**. Il n'existe pas de sortie attendue à comparer : la salle d'essai prouve
qu'une routine ne plante pas et qu'elle atteint ses postures, pas qu'elle se comporte comme en 1998.

Le 2026-08-29 a montré qu'il existe pourtant un oracle, et qu'il n'est pas dans le dépôt :
**quelqu'un qui connaît le jeu**. La Coccinelle a été lâchée dans la forêt, elle a volé en oscillant
puis foncé en prenant de la hauteur, et Vincent a reconnu la bête. C'est la première routine
d'intelligence dont le **comportement** — et pas seulement la compilation — soit confirmé.

**Ce que cet oracle vaut, et ce qu'il ne vaut pas.** Il valide une *forme* : vole ou marche, charge
ou tourne autour, frappe de près ou de loin. Il ne dira jamais qu'un cône de virage vaut
`hardiesse/4 + 0x38` plutôt que `+0x30`. Les constantes restent l'affaire du désassembleur ; la
silhouette, elle, se vérifie à l'œil, et c'est exactement ce qui manquait.

Conséquence pratique : **faire tourner chaque routine neuve devant quelqu'un qui a joué au jeu**,
une fois, dans sa sous-zone d'origine et avec son vrai modèle — donc sans greffe. Une routine dont
la silhouette est reconnue est bien plus sûre qu'une routine qui n'a fait que ne pas planter.

---

## Savoir lesquels manquent vraiment : le filet

Ce relevé dit ce que le jeu **contient**. Il ne dit pas ce qu'un joueur **rencontre**, et les deux
listes n'ont pas la même longueur : 166 gestionnaires existent, une partie n'en croise qu'une
poignée à la fois.

Depuis le 2026-08-29, `OPENFORESIA_AI_STUB=1` récolte la seconde liste. Sous ce drapeau, un
gestionnaire non transcrit ne tue plus le processus : il se journalise et rend la main, la créature
reste immobile, et la partie continue jusqu'au bout. Une seule partie rapporte donc **toutes** les
lacunes de son parcours, là où une levée n'en donne jamais qu'une — celle qui a tué le processus.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 120 -EnvVar AI_STUB -WorkingDir OpenForesia
Select-String -Path scratchpad\playtest-*.log -Pattern '\[LACUNE\]'
```

Le journal compte les appels par couple *(gestionnaire, sous-zone)*, et **ce compte est l'ordre de
transcription** : un gestionnaire appelé des milliers de fois est une bête sous les yeux du joueur,
un appel unique peut n'être qu'un passage. Le mode d'emploi complet est dans
`Documentation/Tests/test-protocol.md`, section « Le filet ».

---

## Les exercer sans jouer jusqu'à eux : la salle d'essai

Le filet dit ce qui manque ; il faut encore aller au-devant des bêtes pour le savoir. Depuis le
2026-08-29, `OPENFORESIA_AI_BENCH` supprime ce trajet : à l'arrivée dans une sous-zone, le banc lit
sa table d'espèces, retient **une espèce par sous-type**, et les fait toutes naître d'un coup autour
du groupe.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 90 -EnvVar AI_BENCH=3:0 -WorkingDir OpenForesia
```

Le lien sous-type ↔ espèce n'est pas deviné : `ResolveEntityBlocks` donne
`espèce = *(répertoire + 0x4c) + index * 8`, et **`espèce[1]` est le numéro de comportement** — le
même octet que `SpawnEntityFormation` recopie en `entité[5]`. C'est ce qui répond à la première des
questions laissées ouvertes plus bas, au moins en jeu : le bilan du banc nomme chaque bête, en
lisant les seize premiers octets de son modèle.

**Une sous-zone ne porte que trois à huit espèces**, alors que sa table en installe jusqu'à onze.
Relevé du CD pour la forêt :

| sous-zone | comportements portés |
|---|---|
| 0 | 2, 0, 1 |
| 1 | 2, 1, 6, 5 |
| 2 | 18, 4, 3, 5 |
| 3 | 8, 9, 7, 5, 6, 30 |
| 4 | 3, 7, 4 |

Le bloc A se couvre donc soit **en marchant** — le banc se réarme à chaque changement de lieu — soit
en **greffant** (`AI_BENCH_ORPHANS=1`) les sous-types que le lieu installe sans qu'aucune de ses
espèces les porte. La greffe exerce le corps de la routine avec de vraies statistiques, mais sous un
modèle qui n'est pas le sien : une levée venant de l'animation y demande confirmation là où l'espèce
vit vraiment. Mode d'emploi complet dans `Documentation/Tests/test-protocol.md`, section « La salle
d'essai ».

---

## Ce que ce relevé ne dit pas

- **Quelle espèce utilise quel sous-type — la donnée est là, la table de correspondance non.** La
  colonne `ia` d'`especes.csv` *est* le sous-type (`espèce[1]`), et `modeles.csv` porte les noms :
  tout est relevé, mais le croisement systématique « comportement → liste d'espèces, par nom »
  n'est pas écrit. En jeu, la salle d'essai le fait au coup par coup — son bilan nomme chaque bête
  qu'elle engendre. Le tableau d'ensemble reste à produire, et c'est un simple `join` sur les deux
  CSV.
- **Si les 54 gestionnaires « simples » sont vraiment tous du décor.** Le classement repose sur un
  seul critère — appellent-ils `FUN_00535F20` ? — qui est sûr dans un sens (un appelant est une
  routine d'IA) mais pas dans l'autre : un gestionnaire simple peut être un coffre, un déclencheur,
  ou une bête immobile.
- **Le compte est-il complet ?** Oui pour ce que `PTR_PTR_00566d98` désigne, et c'est bien la table
  que `FUN_0050e680` aiguille. Mais **six autres tables** de sauts d'entités sont installées en
  parallèle (`0x0056adc8` .. `0x0056b2c8`, voir `EntityVTables.cs`), balayées par cinq autres
  passes. Elles ont leurs propres gestionnaires, comptés nulle part ici.

---

## Références

- `OpenForesia.Tools/census-ai-handlers.sh` — le relevé, reproductible.
- `OpenForesia/src/Blaze/Engine/EntityVTables.cs` — les sept tables, fichier **généré**.
- `Documentation/Etat/REVERSE_STATUS.md`, sections du 2026-08-28 — le détail de chaque fonction
  écrite, et les deux pièges de signature que `Blaze.c` tend sur cette famille.
- `Documentation/Reference/Bestiaire/` — les 555 espèces, leurs statistiques et leur butin.
