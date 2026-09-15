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
| **écrits au 2026-09-15** | **24** (`0x00486EE0`, **`0x00487260`**, `0x00499DE0`, `0x0049A360`, `0x0049A980`, `0x0049B220`, `0x0049B840`, `0x0049C0B0`, `0x0049C8E0`, `0x0049D030`, `0x0049DA40`, `0x0049E190`, `0x0049E920`, `0x0049F0C0`, `0x004A3C30`, `0x004A4270`, `0x004A4AE0`, `0x004A54E0`, `0x004A2C90`, `0x004A35B0`, `0x0049FAD0`, `0x004A1D50`, `0x004A13E0`, `0x004A0480`) |

Un gestionnaire est compté comme **routine d'intelligence** s'il appelle `FUN_00535F20`, la lecture
de situation — c'est le marqueur sûr de la famille, et il ne se partage avec rien d'autre.

Les 166 s'étendent de `0x00486EE0` à `0x00502CD0`, et **aucun n'est dans `Blaze.c`** : Ghidra n'a
décompilé que cinq corps sur toute cette plage. Tous demandent le désassembleur.

### Par zone

| zone | lieu | gestionnaires | dont IA | IA écrites |
|---|---|---|---|---|
| **0** | **L'auberge** | **2** | **1** | **1 — complète** |
| 1 | La mine abandonnée | 23 | 17 | 0 |
| 2 | La montagne du dragon de feu | 2 | 1 | 0 |
| **3** | **La forêt** | **23** | **22** | **22 — complète** |
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
| 3 | `0x0049B220` | Abeille · Abeille tueuse | **écrit** |
| 4 | `0x0049B840` | Grizzly | **écrit** |
| 5 | `0x0049C0B0` | Crabe géant | **écrit** |
| 6 | `0x0049C8E0` | Poisson volant | **écrit** |
| 7 | `0x0049D030` | Homme-lézard | **écrit** |
| 8 | `0x0049DA40` | Goblin | **écrit** |
| 9 | `0x0049E190` | Goblin-sorcier | **écrit** |
| 18 | `0x0049E920` | Crotale | **écrit** |

**Les onze se suivent dans le binaire**, dans l'ordre des sous-types. Les deux plantages signalés
étaient les sous-types 0 et 2. **Le bloc A est complet depuis le 2026-09-01** : les onze sont
écrits, et plus aucune ligne « ça plante » ne subsiste dans les sous-zones d'arrivée.

Une espèce de la sous-zone 3 porte le **sous-type 30, « Barricade »**, absent de ce tableau : son
créneau ne fait pas partie des onze relevés, donc il pointe sur la souche vide ou sur un
gestionnaire simple. À vérifier quand on ira dans cette sous-zone — le nom sent le décor.

### Bloc B — l'île, le sentier, la vallée

| sous-type | gestionnaire | bête |
|---|---|---|
| 7 | `0x0049F0C0` | Homme-lézard · Lézard noir |
| 10 | `0x0049FAD0` | Trent — **écrite** |
| 11 | `0x004A0480` | Fleur du mal — **écrite** |
| 12 | `0x004A13E0` | Wyrm — **écrite** |
| 13 | `0x004A1D50` | Varan — **écrite** |
| 14 | `0x004A2C90` | Géant — **écrite** |
| 15 | `0x004A35B0` | Willie — **écrite** |

**Le même numéro ne désigne pas la même bête d'un bloc à l'autre** : le sous-type 7 est
`0x0049D030` en bloc A et `0x0049F0C0` en bloc B. C'est le rappel concret que la table est
rechargée par sous-zone.

**Les sept du bloc B sont écrits depuis le 2026-09-02**, et avec eux la forêt entière.

**La Fleur du mal `0x004A0480` est la seconde routine de la charpente du Varan**, et c'est ce qui
en fait une famille plutôt qu'un cas isolé : aucune des quatre feuilles partagées, les quatre
feuilles propres au Varan (`FUN_004A1020`, `FUN_004C3810`, `FUN_004D5DE0`, `FUN_004D5E60`), et
**exactement la même sonde bâtie sur la pile**, `{0, 0x1e, 0x32, 0x64, 0, 0x64}`, mot pour mot. Son
bloc de heurt ne diffère que par le masque de contact — `0x2000af82` contre `0x2000ad82`, un bit —,
si bien qu'il est désormais partagé avec un paramètre.

Ce qui lui appartient : **son geste sème des braises** — la posture `0x0b` fait naître une image sur
trois le sous-type `0xEF`, qui se résout en `0x004101D0`, la braise qui dérive, déjà écrite ; et
**elle ne choisit pas sa proie, elle la tire au sort** (même chercheur que le Varan, mais avec le
mode 0, qui n'a pas de `case` : seule la pièce à 60 % agit). Elle n'a ni sort, ni prise, ni poison —
cinq de ses douze cases sont vides —, sa posture 7 n'est joignable que par un mur, et **ses deux
dernières bandes de replanification ne se recouvrent pas**, ce qui range le recouvrement du Varan du
côté des singularités.

**Le Wyrm `0x004A13E0` est la cinquième bête volante du dépôt, et la seule des cinq à porter DEUX
gestes.** Le crible l'annonçait près du Crabe géant, une terrestre ; sa liste d'appels dit
`FUN_0049A8E0` et `FUN_004A1CF0` — donc volante, et sœur de Willie, avec la même interruption
conditionnelle sur `+0x16`. Le comptage de lignes ne pouvait pas trancher (584 contre 594) ; la
liste d'appels, si.

Ses deux gestes se lisent dans `+0x42`, comme chez les terrestres : la posture 0 **le coup** — sonde
`0x005529b0`, décalage vertical `-0x100`, il frappe sous lui, à pleine attaque, sans contrôle
d'image — et la posture 1 **la charge**, en deux phases, dont la seconde balaie tant que sa vitesse
à plat dépasse 3. **La charge porte la prise** : le bit `0x20` se pose au premier contact et interdit
tout second balayage, donc elle ne blesse qu'une fois ; sa victime part à 30 unités vers la cible et
à `0x18000` de haut, et l'effet naît à la position de la victime. **C'est le geste du Géant greffé
sur le corps du Trent.**

Ce qui lui appartient en propre est le mouvement : **ses quatre postures amortissent leur poussée
d'un seizième** au lieu de la poser ou de l'effacer d'un coup — une bête qui vole n'a pas de frein —
et elle **garde l'animation 0 pour se tourner**, là où toute la famille passe à 1 : elle ne marche
pas, elle dérive. Enfin, **une fois sur cinq sa replanification à moyenne distance ne change rien du
tout** et recommence à l'image suivante. Le détail est dans `REVERSE_STATUS.md`.

**Le Varan `0x004A1D50` n'est pas de la même famille que les treize autres.** C'est la première
routine d'intelligence du dépôt qui n'appelle **aucune** des quatre feuilles partagées
(`FUN_00488160`, `FUN_004DB5A0`, `FUN_004DFAF0`, `FUN_004E3770`) : elle inline les deux premières et
remplace les deux dernières par un bloc à elle. Sa machine à postures a **treize** cases au lieu de
quatre, son bloc de geste **n'interrompt pas** la posture — les deux tournent la même image —, et
son cône de décision n'est jamais la largeur dont elle tourne.

Ce qui lui appartient : elle **choisit sa proie** à la naissance, par classe, via `FUN_004A1020`
(mode 5, la classe 3) — aucune autre ne touche à `+0x6c` ; elle **contourne les murs**, en annulant
l'image entière puis en demandant à `FUN_004D5E60` de quel côté passer ; elle **tourne sur place en
fonçant** (posture 9, où `+0x10` porte une vitesse angulaire et non un cap) ; et **sa morsure
empoisonne une fois sur quinze**, par `FUN_004A2BF0`, le premier effet de piège appliqué par une
bête.

C'est aussi le **troisième lanceur de sort** de la forêt, avec une seule fiche — `0x005529c8`,
famille 0 indice 19 — et c'est précisément celle que le Behemoth et le Trent ont en commun. Enfin,
sa posture 8 arme une **prise** complète (attaque du modèle divisée par quatre, victime projetée)
qui est **inatteignable** : rien n'y pose le bit de geste qui la garde. Défaut de 1998, transcrit
tel quel. Le détail est dans `REVERSE_STATUS.md`.

**Le Trent `0x0049FAD0` est la seconde bête du dépôt qui lance un sort**, et elle le fait par le
mécanisme du Behemoth **au complet** : même bit `0x800` qui bascule sur une seconde machine à
postures, mêmes quatre états dans `+0x10`, même entité de lancement `0xbd`, même durée
d'incantation lue dans la fiche de sort. Deux constantes seulement les séparent, et leurs deux
tables de sorts — `0x005529A8` contre `0x00552A30` — partagent deux fiches sur trois. **Un
mécanisme de cette taille est donc réutilisé d'un bloc à l'autre, pas réécrit.**

Ce qui lui appartient : sa **charge ne frappe qu'en mouvement** (elle mesure sa propre vitesse et
n'ouvre le balayage qu'au-dessus de 6), son **coup gèle l'image d'animation tant qu'elle est en
l'air**, et elle **saute d'autant plus souvent qu'elle est hardie** — `hasard % 100 < hardiesse / 8`.
Elle n'a aucune gravité propre.

**Willie `0x004A35B0` est la quatrième bête volante du dépôt**, et la sœur de la Coccinelle du bloc
A : même queue de vol, même cône, même replanification à un champ près (`+0x10` au lieu de `+0x12`).
Ce qui lui appartient est visuel — **elle enfle jusqu'à trois fois sa taille pour frapper**, sa
sonde de contact est **centrée sur son propre corps** (le seul `probe[1]` négatif du dépôt), et hors
geste sa taille, sa couleur et son altitude de vol pulsent toutes trois sur la même phase `+0x14`.
Elle ne change jamais d'animation, et c'est la seule routine qui **choisisse** entre l'interruption
terrestre et la volante, selon `+0x16`.

### Bloc C — le lieu de coupe et la grotte

| sous-type | gestionnaire | bête | genre |
|---|---|---|---|
| 3 | `0x004A3C30` | Abeille tueuse | IA — **écrite** |
| 4 | `0x004A4270` | Grizzly | IA — **écrite** (le même corps que `0x0049B840`) |
| 16 | `0x004A4AE0` | Méchant ours | IA — **écrite** |
| 17 | `0x004A54E0` | Behemoth | IA — **écrite** |
| 19 | `0x004A5F10` | Abeille tueuse | simple |

**L'abeille tueuse `0x004A3C30` est la troisième bête volante du dépôt** — après la Coccinelle
`0x0049A360` et l'Abeille `0x0049B220` —, écrite le 2026-09-01 : même vol que la Coccinelle,
`BeatWings_0049A68F` est partagé au mot près ; mais sa posture 3 est une **ruée**, une impulsion
d'une seule image, et le bond du corps à corps part à `+0x800` du cap visé : elle recule en restant
face au groupe. Le détail est dans `REVERSE_STATUS.md`.

Noter que le bloc A porte déjà une « Abeille tueuse » sous un **autre** gestionnaire
(`0x0049B220`) : le nom d'espèce ne détermine pas le comportement, le bloc et le sous-type le font.

**Et le Grizzly `0x004A4270` n'a pas de corps à lui** : c'est le **même code** que `0x0049B840`, le
Grizzly du bloc A, émis une seconde fois par le compilateur. Les deux désassemblages donnent 705
lignes identiques sur 705, à deux immédiates près — les adresses de ses deux gabarits de portée,
`0x005529e8` et `0x005529f4` au lieu de `0x00552960` et `0x0055296c`. **Et les vingt-quatre octets
de ces deux copies sont eux-mêmes identiques**, ce qui règle la question : les deux Grizzlys se
comportent exactement pareil.

C'est un cas de plus pour la recette : quand deux corps ne diffèrent que par des immédiates qui sont
des **adresses**, la comparaison n'est pas finie — il faut aller lire ce qu'elles désignent. Un
`od` du seul code aurait conclu « fonction différente ».

**Et le Méchant ours `0x004A4AE0` est le corps le plus riche de la famille** : **trois** gestes là
où le Crabe et le Grizzly en ont deux. La griffe, la **prise** — elle empoigne, inflige la moitié de
l'attaque du bloc de modèle, projette la victime et la fait décoller — et le **souffle**, la seule
attaque à distance de la famille. Son souffle fait naître un sous-type `>= 0xc0`, donc pris dans la
table **par sous-zone** `0x0056adc8` : c'est la première routine d'intelligence dont l'effet dépende
de la carte. Et c'est la seule des quatorze qui **secoue la caméra** quand elle marche.

**Et le Behemoth `0x004A54E0` ferme le bloc.** C'est la seule bête du dépôt qui ait **deux machines
à postures** — le bit `0x800` des drapeaux choisit entre le régime ordinaire (quatre postures dans
`+0x42`) et l'**incantation** (quatre états dans `+0x10`) —, et **la première qui lance un sort** :
elle en tire un sur trois dans `0x00552A30`, famille 0, indices 13/19/23, et va lire la durée de
l'incantation dans la fiche de sort elle-même. Sa gravité `0x7fff` est le maximum d'un `short`
signé ; ses cônes de virage sont des constantes et son rythme ignore la timidité — une masse, pas un
duelliste.

### Rien ne reste : la forêt est complète (2026-09-02)

**Les 136 créneaux de la zone 3 ont un corps.** C'est la **première zone entièrement implémentée du
dépôt** — la zone 0 en est à 261 sur 274, et les neuf autres entre 16 et 22 sur des centaines. Les
trois dernières, le Varan `0x004A1D50`, le Wyrm `0x004A13E0` et la Fleur du mal `0x004A0480`, ont
été écrites le 2026-09-02.

**La dette qui reste est celle de l'épreuve, pas de l'écriture** : 24 des 136 seulement ont été vus
tourner en jeu. La forêt est donc le premier lieu où une séance de test se lit comme une mesure et
non comme une chasse au plantage.

Le Géant `0x004A2C90`, Willie `0x004A35B0` et le Trent `0x0049FAD0` ont été écrits le 2026-09-01,
dans l'ordre du crible ci-dessous plutôt que dans celui des tailles, et **le crible a eu raison les
trois fois** : le Géant annoncé à 283 lignes du Grizzly en est un à trois différences près, Willie
annoncée à 417 de la Coccinelle en est la sœur, et le Trent annoncé à 454 du Goblin-sorcier est,
comme lui, un **lanceur de sorts**.
**Les blocs A et C sont complets** : les sous-zones d'arrivée (0-4) et celles du lieu de coupe et de
la grotte (5 et 9) n'ont plus de trou.

Le relevé du 2026-08-29 en comptait treize, dont deux pour les seules sous-zones d'arrivée ; ces
deux-là sont faites.

**Les six ont été passées au crible du doublon le 2026-09-01**, avant d'en écrire une seule :
désassemblage, effacement de tout ce qui n'est que placement (déplacements d'appel, cibles de saut,
étiquettes), et comptage des lignes qui divergent. **Aucun doublon** — un doublon vrai sort à deux
ou trois lignes, comme `0x004A4270`.

| routine | bête | ≤ octets | le plus proche déjà écrit | écart |
|---|---|---:|---|---:|
| `0x004A2C90` | Géant | 2 336 | `0x0049B840`, le Grizzly | **283** — **écrite** |
| `0x004A35B0` | Willie | 1 664 | `0x0049A360`, la Coccinelle | **417** — **écrite** |
| `0x0049FAD0` | Trent | 2 480 | `0x0049E190`, le Goblin-sorcier | **454** — **écrite** |
| `0x004A13E0` | Wyrm | 2 320 | `0x0049C0B0`, le Crabe | 583 — **écrite, et le voisin était FAUX** |
| `0x004A1D50` | Varan | 3 904 | `0x00499DE0`, la Fourmi géante | 1 171 — **écrite** |
| `0x004A0480` | Fleur du mal | 3 936 | `0x00499DE0`, la Fourmi géante | 1 262 — **écrite, et le voisin était FAUX** |

**La colonne de droite est la recette « calquer sur une sœur » rendue mesurable** : elle dit sur
quel corps déjà transcrit ouvrir la comparaison. Et elle **change l'ordre de travail** — par la
taille il fallait commencer par le Willie, par l'écart il faut commencer par le **Géant**, qui est
un Grizzly modifié. Le coût d'une transcription se mesure au nombre de formes distinctes, pas au
nombre d'octets.

**Les trois premières l'ont confirmé**, et la troisième d'une façon que le crible ne pouvait pas
prévoir. Le Géant est bien le Grizzly qu'il annonçait ; Willie bien la sœur de la Coccinelle,
jusqu'à une replanification identique décision pour décision, à un champ près ; et le Trent, placé
près du **Goblin-sorcier**, s'est révélé être ce que ce voisinage suggérait sans le dire — un
**lanceur de sorts**, portant le mécanisme d'incantation du Behemoth au complet.

Autrement dit, **la colonne « le plus proche déjà écrit » ne mesure pas que du code : elle attrape
aussi des parentés de comportement.**

**Le Varan et le Wyrm, écrits le 2026-09-02, ont donné la limite de l'instrument — deux fois, et de
deux façons différentes.**

Le **Varan**, annoncé à 1 171 lignes de la Fourmi géante, n'est pas une bête exotique : c'est un
corps qui **inline** les feuilles que ses sœurs appellent, et qui en a donc mécaniquement beaucoup
plus de lignes. Un vrai changement de charpente s'y ajoutait, mais les deux se confondaient dans le
chiffre.

Le **Wyrm** est le cas symétrique, et le plus instructif : le crible désignait le **Crabe géant**,
une terrestre, alors que le Wyrm est une **volante**. Recompté après coup, l'écart avec sa vraie
sœur Willie est de **584 lignes contre 594 pour le Crabe** — dix lignes sur six cents, c'est-à-dire
une égalité que le comptage ne peut pas trancher, et il avait tranché dans le mauvais sens.

**Donc : avant de mesurer les lignes, comparer les listes d'appels.**

```sh
grep -o "call      00[0-9A-F]*h" Legacy/Raw/Blaze/FUN_XXXXXXXX.asm | sort -u
```

`FUN_0049A8E0` + `FUN_004A1CF0` ⇒ volante ; `FUN_00488160` + `FUN_004DFAF0` + `FUN_004E3770` ⇒
terrestre ; **aucun des deux** ⇒ charpente propre, comme le Varan. Deux corps qui n'appellent pas
les mêmes feuilles ne sont pas comparables ligne à ligne, et la colonne « écart » ne veut alors
rien dire.

Le détail est dans `REVERSE_STATUS.md`, section « les six du bloc B passées au crible ».

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
2. **Le tri de départ, quatre lectures d'une minute chacune, avant d'écrire une ligne :**
   - quelle **fonction d'intégration** la routine appelle — `FUN_00488160` terrestre,
     `FUN_0049a8e0` volante ;
   - y a-t-il un **`jmp dword [reg*4+…]`** ? Sinon l'aiguillage est une cascade de comparaisons et
     il n'y a pas de table de sauts à relever ;
   - **d'où vient la sonde** — six `mov word [esp+…]` avant l'appel, ou un `push <adresse>` juste
     avant `call 00519C30`, auquel cas elle est dans `.data` et se lit au verbe `at`. Il peut y en
     avoir **deux**, une par geste ;
   - **combien d'appels à `FUN_00539b80`**, la résolution du coup. C'est le compte des gestes qui
     blessent vraiment : l'Homme-lézard balaie son gabarit dans ses deux gestes et n'appelle la
     résolution que dans un seul — sa course bouscule sans faire de dégât. Deux balayages ne font
     pas deux attaques.
3. Relever au verbe `at` ce que l'étape 2 a désigné : la table de sauts qui suit le bourrage de
   `nop`, et la ou les sondes statiques.
4. **Calquer sur DEUX sœurs, pas une** — le milieu et la charpente sont des axes indépendants, et
   l'Abeille l'a montré le 2026-08-29 : elle est volante et bâtie comme une terrestre.
   - le **milieu** décide de trois appels et du bloc de vol : `IntegrateSceneEntityMotion_00488160`
     + `EndSceneEntityAnimation_004DFAF0` + `InterruptSceneEntity_004E3770` pour une terrestre ;
     `IntegrateFlyingEntityMotion_0049A8E0` + `BeatWings_0049A68F` +
     `InterruptFlyingEntity_004A1CF0` pour une volante ;
   - la **charpente** décide des postures et de la replanification : `TickSceneEntityAi_0049A980`
     (cinq postures) ou `TickSceneEntityAi_00499DE0` (quatre) pour la forme classique à trois
     plages, `TickSceneEntityAi_0049C0B0` / `0049B840` pour une bête à deux gestes,
     `TickSceneEntityAi_0049D030` pour la forme complète à cinq postures avec recul et esquive,
     `TickSceneEntityAi_0049A360` pour la forme courte à deux plages.

   La charpente et les postures tenues se recopient ; **ce sont les gestes qui ne se devinent pas**,
   et c'est là que passe le temps.
5. **Lire comment chaque geste se termine** — quatre mécanismes différents ont été rencontrés sur
   neuf routines, et rien ne permet de deviner lequel : un compte à rebours `+0x43` (le Crabe), le
   contact avec le sol (le Grizzly), un seuil de vitesse horizontale mesuré au `sqrt` (l'Homme-lézard
   et l'Abeille), ou la seule fin d'animation `0x2000000` (l'Abeille, la Coccinelle).
6. Ajouter le `case` au répartiteur `DispatchEntityHandler`, et garder le relevé sous
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


### La cinquième routine : le Poisson volant ne vole pas (2026-08-29)

**Le tri par la fonction d'intégration a marché du premier coup, et il a démenti le nom.** La
routine appelle `FUN_00488160`, l'intégration terrestre, pas `FUN_0049a8e0`. Le « Poisson volant »
ne vole pas : il **bondit**. Le bit 1 de ses drapeaux veut dire « en l'air », `+0x54` porte sa
vitesse verticale, et l'atterrissage se détecte en comparant `+0x20` au sol de `+0x60`. Une ligne du
désassemblage a évité de le calquer sur la mauvaise famille.

**Elle s'accroche**, et c'est nouveau. Quand son bond touche un membre du groupe, elle se colle à
lui : bit 0 de `+0x40`, l'écart de fixation rangé dans `+0x44`/`+0x48`/`+0x4c`, et à partir de là sa
position est **recopiée de sa victime** au lieu d'être intégrée. Elle mord toutes les `0x1e` images.

**Le joueur s'en débarrasse en remuant.** La routine remonte jusqu'au *pad* de sa victime — par
`0x005e9188` puis `0x00604a20`, l'indexation exacte des sondes d'entrée — et n'use le compteur de
prise `+0x10` que sur les images où une direction est tenue. À zéro elle lâche et part en arrière,
cap + `0x800`, détente verticale fixe. La force de la prise vaut `(hasard & 7) + hardiesse/4 + 8` :
une bête hardie tient plus longtemps.

**Trois postures, et aucune table de sauts.** L'aiguillage est une cascade de comparaisons. C'est la
première des cinq routines dans ce cas — donc l'étape 2 de la recette (« relever la table de sauts
qui suit le corps ») **ne s'applique pas partout** : chercher d'abord un `jmp dword [reg*4+…]`, et
s'il n'y en a pas, lire la cascade. Cette espèce n'a par ailleurs ni repos ni errance : elle attend,
se tourne, ou bondit.

**Ce que ça dit du reste du bloc.** Deux prédictions de suite se sont révélées fausses — j'avais
annoncé que le Poisson volant se calquerait sur la Coccinelle et coûterait moins cher ; il est
terrestre et c'est la plus riche des cinq. **Ne rien prédire à partir du nom de la bête.** Le
désassemblage se lit d'abord, sur trois points qui coûtent une minute chacun : la fonction
d'intégration, la présence d'une table de sauts, et la sonde.
Sa sonde le disait déjà : `probe[4]` vaut `-0x100` là où les deux terrestres ont 0.

**Conséquence pour la suite du bloc A.** « Calquer sur une des deux sœurs en substituant cinq
constantes » n'est vrai qu'entre bêtes de même milieu. Le Poisson volant (sous-type 6) se calquera
sur celle-ci ; le Grizzly et le Crabe sur les terrestres. **Regarder d'abord quelle fonction
d'intégration la routine appelle** — c'est le tri le moins cher, une seule ligne du désassemblage.


### La sixième routine : le Crabe géant a deux attaques (2026-08-29)

**Le tri par la fonction d'intégration a de nouveau tenu**, et cette fois il confirmait la prévision
plutôt que de la démentir : le Crabe appelle `FUN_00488160`, l'intégration terrestre, comme annoncé
au paragraphe précédent. La routine est la plus proche parente des deux premières — même queue de
quatre appels, même table de sauts (quatre entrées, relevées à `0x0049c8cc`), mêmes feuilles
partagées, et une replanification qui est celle de `0x0049a980` à deux branches près.

**Sa nouveauté est ailleurs : elle a deux attaques, là où les cinq précédentes n'en ont qu'une.**
La forme du geste ne se lit pas sur un champ à part mais sur la **posture** — c'est-à-dire que
`+0x42` sert à deux choses selon que le bit 1 des drapeaux est mis ou non.

| | la pince (posture 0) | la ruée (posture 1) |
|---|---|---|
| déclenchée depuis | le corps à corps, sous `0xfa` | l'approche, entre `0xfa` et `0x1f4` |
| animation | 3 | 4 |
| contact | images 12 à 15, **une par image** | une seule fois, verrou `0x20` |
| dégâts | pleins | **moitié** |

**La pince frappe quatre fois.** Elle croise le gabarit à chacune des images 12, 13, 14 et 15, sans
le verrou de coup unique que la ruée possède. Une victime restée dessous encaisse quatre fois. Ce
n'est pas une garde oubliée à la transcription : le binaire ne teste rien à cet endroit.

**La ruée est une machine à trois temps**, portée par `+0x10` décompté de 2 à 0 — à 2 la bête amortit
sa poussée d'un seizième et vise ; à la quatorzième image du geste elle passe à 1 ; à 1 elle se
lance, en versant `(hardiesse + 0x380) << 7` d'un coup dans ses vitesses `+0x50`/`+0x58` ; à 0 elle
cherche ce qu'elle a percuté. C'est la plus forte poussée de toutes les routines écrites.

**Et la ruée ne porte qu'à moitié.** Avant d'appeler `FUN_00539b80`, la routine divise par deux le
premier des huit caractères du bloc de **modèle** (`+0x30`, l'attaque), puis le remet tel quel
aussitôt après. Comme `FUN_00538af0` recopie ce bloc dans la fiche au début de chaque résolution, la
division se propage jusqu'aux dégâts et disparaît ensuite sans laisser de trace. **C'est la première
routine écrite qui module les dégâts**, et le détour par le modèle partagé — et non par la fiche de
la bête — est ce qui rend l'astuce invisible à la lecture de `FUN_00539b80` seule. À chercher
désormais dans les routines suivantes : une écriture dans le bloc de modèle autour d'un appel de
combat est une modulation de puissance, pas une corruption.

Un contact réussi projette en outre la victime à trente unités par image dans le cap du crabe, la
décolle du sol si elle y était, et fait naître l'effet `0x39` **au point d'impact** — pour cela le
crabe se pose une image sur la position de sa victime, puis reprend la sienne. C'est la même ruse
que la traînée de la Fourmi géante, appliquée à la position au lieu de la vitesse.

**Deux écarts de constante à noter**, tous deux vérifiés dans le désassemblage et non déduits :

- **sa marche ignore la vitesse** rendue par la lecture de situation, là où `0x0049a980` multiplie
  sa poussée par elle avant d'avancer. Les deux corps diffèrent bien d'un `imul` ;
- **le compte à rebours de la ruée vaut `(timidité >> 8) + 8`**, et la timidité ne dépasse jamais
  `0x40` : le décalage rend toujours zéro, donc la durée est invariablement 8. Le `sar ecx,8` est
  dans le binaire ; c'est vraisemblablement un `/2` écrit trop grand, mais rien ne le prouve, et la
  transcription garde le calcul tel quel.

**Ce qui reste vrai de la recette.** Le Crabe est la première des trois dernières à se laisser
calquer pour de bon : les quatre postures, les cônes de virage (`hardiesse * 2 + 0x80`), la distance
tenue `0x64` et les trois plages de replanification sont mot pour mot celles de `0x0049a980`. Ce qui
a demandé du travail, c'est uniquement ce que les deux gestes font — donc **lire le geste avant la
posture** : la charpente se recopie, la frappe ne se devine pas.


### La septième routine : le Grizzly, ou la preuve que le calque fonctionne (2026-08-29)

**C'est le jumeau du Crabe**, et la première fois que « calquer sur une sœur de même milieu » a
vraiment tenu de bout en bout. Intégration terrestre `FUN_00488160`, quatre postures aiguillées par
une table de sauts (`0x0049c0a0`), même découpage des gestes sur `+0x42`, même machine à trois temps
sur `+0x10`, et **la même division par deux de l'attaque du modèle** autour de `FUN_00539b80`. Les
quatre postures tenues sont identiques au mot près — mêmes cônes `hardiesse * 2 + 0x80`, même
distance tenue `0x64`, même poussée `(hardiesse + 0xc0) << 7`.

**La demi-attaque n'est donc pas une singularité du Crabe : c'est un idiome de la famille.** Deux
routines voisines la partagent, écrites à un jour d'intervalle. À chercher systématiquement dans les
suivantes — une écriture dans le bloc de modèle autour d'un appel de combat.

#### Trois choses lui sont propres, et elles changent la silhouette

**Il quitte le sol.** Là où le Crabe se rue à plat, le Grizzly pose une vitesse verticale de
`0x24000` et allume le bit 0 (« en l'air ») en se lançant. Son geste ne se ferme donc **pas** sur un
compte à rebours mais à l'**atterrissage**, quand ce bit retombe : `LAB_0049b981` teste `al,1` là où
le Crabe décomptait `+0x43`. Un bond long dure plus longtemps qu'un bond court, ce qui n'a pas
d'équivalent chez les six précédentes.

Et il écrit `0x6000` dans **`+0x9a` à chaque image**, sur le chemin commun de sortie, juste avant
l'intégration. C'est le terme que `FUN_00488160` retranche de la vitesse verticale — sa gravité
propre, qu'aucune routine écrite avant lui ne touchait. Elle est reposée même quand sa vitesse est
nulle.

**Ses gabarits sont dans `.data`.** Ses deux sondes ne sont pas construites sur la pile mais lues à
`0x00552960` (la griffe) et `0x0055296c` (le bond) — un cas que ce fichier n'avait pas encore
rencontré, et qui a valu une étape de plus à la recette. Le relevé complet, avec les trois entrées
voisines non attribuées, est dans `Documentation/Reference/DataSymbols.md`.

| | `[1]` | `[2]` | `[3]` | `[4]` | `[5]` |
|---|---:|---:|---:|---:|---:|
| la griffe, `0x00552960` | `0x1e` | `0x46` | `0x96` | **`-0x100`** | `0x50` |
| le bond, `0x0055296c` | `0x1e` | `0x32` | `0x96` | 0 | `0x50` |

Le cinquième terme est un décalage vertical : négatif, le gabarit se lève. **La griffe s'abat d'en
haut, le bond frappe au ras du sol** — et c'est la donnée seule qui le dit, le code étant le même
des deux côtés.

**Sa griffe se corrige en cours de geste.** Jusqu'à la neuvième image elle continue de se tourner
vers sa cible (`hardiesse / 2 + 0x20` par image), à la neuvième elle avance d'un à-coup
`(hardiesse + 0x300) << 7`, et elle balaie ensuite les images 10 à 13. Le Crabe, lui, part droit et
ne rectifie plus une fois le geste commencé. C'est pour cela qu'un Grizzly rate beaucoup moins
souvent — et c'est une **différence de forme, pas de constante** : rien dans la table des cinq
nombres ne l'aurait fait apparaître.

#### Deux écarts de replanification

**Il bondit depuis les deux plages utiles**, pas seulement depuis l'approche. Au corps à corps un
tirage sur cent le fait griffer sous `0x32`, tenir sa distance sous `0x4b`, **bondir** entre `0x4b`
et `0x59` s'il est dans le cône, et s'écarter au-delà. Le Crabe n'avait qu'une seule entrée vers sa
ruée.

**Il n'hésite jamais de loin.** Son errance lointaine n'a que deux sorties — repos sous `0x32`,
direction entièrement tirée au-delà — contre trois chez toutes les précédentes.

#### Ce que ça dit du coût des suivantes

Le Grizzly a coûté beaucoup moins cher que le Crabe, et pour une raison qui se généralise : **la
charpente et les postures tenues se recopient, seuls les gestes se lisent**. Sur 2 141 octets de
corps, les quatre postures tenues et les trois plages de replanification — soit les deux tiers du
texte — étaient déjà écrites. Le travail réel a porté sur deux blocs de gestes et deux tables de
`.data`.

Conséquence pratique pour les quatre qui restent au bloc d’arrivée : **lire le geste avant la
posture**, et ne pas laisser la ressemblance de la charpente donner l'illusion que la routine est
finie.


### La huitième routine : l'Abeille, et les deux axes qui se croisent (2026-08-29)

**Le tri à trois lectures l'a classée en une minute** : elle appelle `FUN_0049A8E0`, l'intégration
volante, et `FUN_004A1CF0`, l'interruption volante. C'est donc une sœur de la Coccinelle. Table de
sauts à `0x0049b828`, sonde construite sur la pile.

**Et pourtant elle est bâtie comme une terrestre.** Son squelette est celui de `0x0049a980` : quatre
postures, trois plages de replanification à `0xfa` et `0x1f4`, le même cône `hardiesse * 2 + 0x80`,
la même distance tenue `0x64`, le même `SceneEntityDelay`. La Coccinelle, elle, n'a que deux plages,
un délai qui lui est propre (`timidité / 4 + 10`) et un engagement sans animation.

| | Coccinelle `0x0049a360` | Abeille `0x0049b220` |
|---|---|---|
| intégration, interruption, bloc de vol | volantes | **les mêmes** |
| plages de replanification | 2 (`0x12c`, `0x258`) | **3 (`0xfa`, `0x1f4`) — celles des terrestres** |
| compte à rebours de posture | `timidité / 4 + 10` | **`SceneEntityDelay`, celui des terrestres** |
| cap d'errance | `+0x12` | **`+0x10`** |
| image où le coup porte | aucune, balayage continu | **la seizième, une seule fois** |
| sonde | `{0, 0x1e, 0x50, 0x64, -0x100, 0x50}` | `{0, 0x1e, 0x32, 0x64, 0, 0x50}` |

**C'est la leçon de méthode de la journée.** « Volante ou terrestre » ne décide que de trois appels
et du bloc de vol ; **la charpente, elle, se choisit indépendamment**. Le tri par la fonction
d'intégration reste le bon premier geste — il évite de calquer le mauvais bloc de mouvement — mais
il ne dit pas sur quelle sœur calquer la replanification. Les deux axes se croisent, et il faut les
lire séparément.

#### Sa posture 3 est un trait, pas une marche

C'est ce qu'elle a de plus caractéristique, et c'est un usage de `+0x43` qu'aucune routine
précédente ne fait : **une machine à deux temps au lieu d'un compte à rebours**.

- à **2**, l'abeille verse d'un coup `(timidité + 0x120) << 8` dans ses vitesses, le long du cap
  rangé en `+0x10`, puis décrémente ;
- à **1**, elle attend que cette vitesse horizontale soit retombée sous `hardiesse / 8 + 1` avant de
  rendre la main à la replanification ;
- et **à chaque image de la posture, elle oriente son corps plein cap sur la cible**, sans cône.

Le résultat est le vol d'insecte : des traits rectilignes séparés par des arrêts face au joueur. Les
trois sorties d'errance de sa replanification arment ce trait (`+0x43 = 2`) au lieu de tirer un
délai — elle ne tient jamais un cap pendant vingt images.

**La poussée du trait se prend sur la TIMIDITÉ, pas sur la hardiesse.** Une abeille craintive file
plus vite. Aucune autre routine écrite ne fait cela, et le registre a été vérifié deux fois avant
d'être transcrit : `edi` vaut `0x40 - hardiesse` depuis `0x0049b29a` et n'est pas réécrit sur ce
chemin. Le décalage est en outre de huit bits et non de sept comme partout ailleurs.

#### Son coup tient en une image

La seizième du geste, exactement — ni les quatre images du Crabe et du Grizzly, ni le balayage
continu de la Coccinelle. **Un dard, une fois**, et aucun verrou n'est nécessaire : l'égalité stricte
sur `+0x1a` en tient lieu. Le geste n'a d'ailleurs pas de forme à choisir, si bien que son engagement
ne touche pas `+0x42` : il pose `+0x43 = 1`, dont le seul rôle est de retenir la replanification
jusqu'à la fin de l'animation.

#### Deux détails de drapeaux

Elle **ne pose pas** le bit 0 à l'entrée, là où la Coccinelle le fait à chaque image ; et son garde
de posture teste `0x10` seul quand les terrestres testent `0x11`. Les deux choix vont ensemble et
sont cohérents avec `0x816` — le masque de l'interruption volante — dans la garde de
replanification.


### La neuvième routine : l'Homme-lézard, et une charge qui ne blesse pas (2026-08-29)

**C'est la plus riche de la famille**, et la première à réunir tout ce que les sept précédentes
avaient chacune de leur côté : les **cinq** postures de la Fourmi géante (`0x0049a980`), sa branche
de recul, sa relance d'attaque en fin de geste, son esquive de côté — **et** les deux gestes du
Crabe et du Grizzly. Rien de neuf dans la charpente, tout dans la combinaison. Terrestre
(`FUN_00488160`), table de sauts à `0x0049da24`, deux sondes construites sur la pile.

#### Sa charge ne fait aucun dégât

C'est le fait le plus inattendu de la journée, et il est net dans le binaire : **`FUN_00539b80`
n'est appelé qu'une seule fois** dans les 2 546 octets, depuis le geste de posture 0. Le geste de
posture 1 — la course — croise pourtant le gabarit à chaque image, mais tout ce qu'il fait d'une
victime touchée, c'est la faire clignoter, la projeter à trente unités par image, la décoller du sol
et poser l'effet `0x39`. **Elle bouscule, elle ne blesse pas.**

Cela vaut aussi comme méthode : *compter les appels de `FUN_00539b80` est une lecture d'une seconde
qui dit combien des gestes d'une bête sont des attaques*. Deux balayages du gabarit ne font pas deux
attaques.

#### Sa course tourne en boucle

Dès que l'animation dépasse sa huitième image, la routine la **remet à 8** : le cycle de course se
rejoue tant que la bête file. Elle ne s'arrête ni sur un compte à rebours (le Crabe) ni à
l'atterrissage (le Grizzly), mais quand sa vitesse horizontale — mesurée au `sqrt` de `FUN_00545a10`
sur `+0x50` et `+0x58` — est retombée sous `hardiesse / 8 + 1`. **Une bête hardie court donc moins
longtemps**, ce qui est l'inverse de l'intuition et se lit tel quel. Le même seuil sert à l'Abeille
pour terminer son trait : c'est un idiome partagé, comme la demi-attaque du Crabe et du Grizzly.

Trois fins de geste, donc, pour trois espèces voisines — compte à rebours, contact avec le sol,
seuil de vitesse. **Ne pas supposer laquelle** avant d'avoir lu le bloc.

#### Le bit 0 retient le coup

Si la bête est en l'air quand son geste atteint sa fenêtre de contact, l'image d'animation
**recule** d'un cran au lieu d'avancer : le coup attend qu'elle ait touché terre. Le motif existait
déjà chez la Fourmi géante et il avait été transcrit sans être compris ; il prend ici tout son sens,
puisque la posture 3 de l'Homme-lézard peut bondir.

#### Ce qui a changé de valeur

| | Fourmi géante `0x0049a980` | Homme-lézard `0x0049d030` |
|---|---|---|
| sonde du coup | `{0, 0x1e, 0x32, 0x64, 0, 0x50}` | `{0, 0x28, 0x32, 0x64, 0, 0x78}` |
| allonge tirée au corps à corps | `100 + hasard` | **`0xc8` + hasard** |
| chance de tenter le coup | `0x1e` sur cent | **`0x32` sur cent** |
| animation de l'esquive | 5 | 4 |
| chance de bondir en posture 3 | `hardiesse / 8` | **`hardiesse / 16`**, mais plus fort et plus haut |
| poussée d'approche | `(hardiesse + 0xc0) << 7` | `(hardiesse + 0x100) << 7` |

La sonde à `0x78` et l'allonge à `0xc8` vont ensemble : **c'est la plus longue portée de la
famille**, et cela se verra à l'œil — il touche de plus loin que tout ce qui a été écrit.

#### Une sortie que la Fourmi géante n'a pas

Entre `0x46` et `0x59` du tirage de corps à corps, et si la cible est dans le cône, il arme **la
course** au lieu d'errer. Avec le « foncer » de la posture 4 juste avant, cela lui fait deux façons
distinctes de fondre sur le groupe : l'une est une posture tenue, l'autre un geste.


### La dixième routine : le Goblin attaque de loin (2026-08-29)

**C'est la première bête qui ne frappe pas.** Ses deux gestes ne se ressemblent en rien :

| | posture 0 en geste | posture 1 en geste |
|---|---|---|
| ce qu'elle fait | un balayage du gabarit, à la **neuvième** image | fait naître l'effet `0x49` à la **septième**, et rien d'autre |
| gabarit croisé | oui | **aucun** |
| dégâts | `FUN_00539b80` | **c'est le projectile qui portera le coup** |

La routine ne contient donc **qu'un seul** appel à `FUN_00539b80`, comme l'Homme-lézard — mais pour
une raison opposée : chez le lézard, le second geste bouscule sans blesser ; chez le Goblin, le
second geste ne touche pas du tout, il lance.

#### Le jet part des deux plages lointaines

C'est ce qui change le plus la silhouette, et aucune des neuf précédentes ne le fait : **à
l'approche comme dans l'errance**, un tirage arme le jet. De loin il faut en plus être à moins de
`0x2bc` et dans le cône ; à l'approche, il suffit d'avoir la cible dans la moitié avant. Un goblin
vous vise de l'autre bout de la clairière, là où toutes les autres bêtes se contentent de marcher.

**Et le seuil de ce tirage se prend sur la HARDIESSE**, `hardiesse / 2 + 8` sur cent, aux deux
plages. C'est le seul tirage de la famille dont le seuil monte avec l'état de la bête au lieu de
descendre avec sa timidité — une bête en forme lance plus souvent.

#### Le reste est la forme complète, moins deux branches

Cinq postures (table de sauts `0x0049e174`), relance d'attaque en fin de geste, corps à corps de la
Fourmi géante — mais **ni branche de recul ni esquive de côté** : le bit `0x10` renvoie directement
à l'application, et une attaque refusée mène droit à l'hésitation. Sa sonde `{0, 0x1e, 0x32, 0x64,
0, 0x46}` est la plus courte des terrestres, ce qui va avec une bête qui préfère lancer.

**Sa marche et son « foncer » tiennent compte de la vitesse** rendue par la lecture de situation, là
où le Crabe, le Grizzly et l'Homme-lézard l'ignorent dans leur marche. Et son « foncer » **s'ajoute**
à la vitesse au lieu de l'écraser, détente verticale comprise.

#### Ce que le jet ouvre : le tir, écrit dans la foulée

L'effet `0x49` est le **lanceur** `0x0047A100`, et il ouvre les trois seuls corps du jeu qui font
voler quelque chose. Ils ont été transcrits le même jour ; aucun n'était dans `Blaze.c`.

| | rôle |
|---|---|
| `0x0047A100`, sous-type `0x49` | **le lanceur** : choisit une cible dans un cône (`FUN_00474f00`), oriente le tir en **cap et en site**, relève le départ à la hauteur de bulle du tireur, fait naître le projectile et **s'éteint dans la même image** |
| `0x0047A270`, sous-type `0x52` | **la trajectoire** : vole, se dirige, touche, puis se **plante** dans sa victime et la suit vingt images en clignotant |
| `0x00476320`, sous-type `0xbd` | **l'éclaboussure** : sept images de sprite, et c'est fini |

**Trois choses de ce sous-système méritent d'être retenues pour la suite.**

Le lanceur vise en **trois dimensions** — `+0x32` reçoit l'azimut, `+0x34` le site —, ce qu'aucune
routine de bête ne fait. Et il ajoute `distance / 4` au site : **un tir long part plus haut**, une
garde-au-but grossière mais réelle.

La trajectoire avance en **huit sous-pas** d'un huitième de vitesse, en appelant le placement à
chaque sous-pas et en accumulant les drapeaux de collision. C'est ce qui empêche un projectile
rapide de traverser un mur en une image, et c'est le seul endroit reconstruit qui procède ainsi.
Son virage est plafonné à `±0x20` par image, jamais plus : un tir suit sa cible sans jamais la
rattraper d'un coup.

Et **c'est le propriétaire qui frappe, pas la flèche**. Le lanceur range le tireur en `+0x74`, et
c'est lui que la trajectoire passe à `FUN_00539b80` : l'attaque, l'expérience et le compte de
victimes reviennent au Goblin. À retenir avant de chercher pourquoi un projectile « n'a pas de
statistiques ».

Le sous-système servira aux autres lanceurs : le Goblin-sorcier (sous-type 9) est le suivant du
bloc, et son nom promet la même chose.

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

---

## L'auberge (zone 0) — deux créneaux, et le second est un lanceur de sorts (2026-09-15)

La zone la plus petite du relevé : **deux** sous-types remplis dans son unique bloc de table,
`0x00553288`. Le sous-type 0 est `0x00486EE0`, la bestiole errante, écrite le 2026-08-30. Le
sous-type 1 est `0x00487260`, et il ne ressemble pas à ce qu'on attendait d'une auberge.

**C'est le quatrième lanceur de sorts du dépôt**, après le Behemoth (`0x004A54E0`), le Trent
(`0x0049FAD0`) et le Varan (`0x004A1D50`). Il porte le **mécanisme d'incantation du Behemoth au
complet** — même bit `0x800` qui bascule sur une seconde machine à états logée dans `+0x10`, mêmes
quatre états dans le même ordre, même entité de lancement `0xbd`, même masque de traits
`0x400003e8` qui l'annule net, même durée lue dans le `+0x13` de la fiche de sort.

**C'est donc le troisième emploi de ce bloc**, et le premier hors de la forêt. Le constat du
2026-09-02 tient : un morceau de comportement de cette taille se recopie, il ne se réécrit pas. Ce
qui change d'une bête à l'autre tient à quatre valeurs — ici l'animation de lancement est la **5**
et non la 7, et le coup part à l'image **7** et non à la 14.

### Ce qui lui appartient : ses sorts viennent du PLACEMENT

Le Behemoth tire sa fiche dans une liste fixe de trois (`0x00552A30`). Celui-ci lit d'abord une
**rangée** dans `0x00552868`, et la rangée vient de `+0x14` — c'est-à-dire de l'**octet 6 du relevé
de placement**, que la naissance d'une entité de scène recopie tel quel
(`WorldMapScript`, `*(int*)(slot + 0x14) = record[6]`).

**Deux exemplaires de la même bête posés sur la même carte peuvent donc n'avoir pas les mêmes
sorts.** Aucune autre routine écrite ne fait dépendre son arsenal de l'endroit où elle a été posée ;
partout ailleurs, l'arsenal est une propriété de l'espèce.

La table compte huit rangées de trois fiches (`0x00552868`–`0x00552897`), bornée par la sonde de
geste qui commence à `0x00552898`. **Les quatre dernières répètent les quatre premières**, octet
pour octet — c'est la donnée de 1998 qui est redondante, pas la lecture qui déborde.

### Trois autres choses qui la distinguent de la famille

**Elle n'a qu'un seul geste.** Sa sonde `{0, 0, 0x32, 0x64, 0, 0x5a}` est croisée à **chaque** image
à partir de la huitième, sans que `+0x42` ne la nuance : là où le Goblin choisit son coup selon sa
posture, celle-ci frappe et c'est tout. Et **son plancher de sonde est à zéro** quand toute la
famille commence à `0x1e` — elle atteint ce qui est à terre.

**Elle fuit.** Dans la plage de corps à corps, la sortie la plus probable (40 %) pose la posture 3
sur un cap tiré autour de la cible **plus un demi-tour**. C'est la contrepartie du lanceur : elle
n'a rien à gagner à rester au contact. Aucune autre routine écrite ne prend cette sortie.

**« Charger » veut dire « incanter ».** Dans les deux plages lointaines, la branche qui correspond à
la charge des autres bêtes ne pose pas de posture : elle lève les bits `0x1000804` et met `+0x10` à
3, c'est-à-dire qu'elle entre dans la machine d'incantation. Elle n'a ni bond, ni charge, ni
posture 4 ; sa posture 3 marche à allure **fixe**, sans multiplier son pas par sa vitesse
d'animation — la seule de la famille dans ce cas.

Enfin, **son recul ne fait rien du tout** : le bit `0x10` rend la main sans même la tourner vers ce
qui l'a touchée, là où le Goblin s'y retourne.
