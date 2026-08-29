# La montée de niveau

Ce que le jeu calcule quand un personnage gagne un niveau, relevé le 2026-08-29 dans
`FUN_005391f0` (`0x005391f0`, transcrite sous `LevelUpCharacter_005391F0`) et dans les tables de
`.rdata` qu'elle lit. Écrit parce que la question se cherche deux fois : à l'écran, la progression
a l'air purement aléatoire.

Elle l'est en partie. Mais l'aléatoire est encadré par trois choses fixes — **les statistiques de
départ**, **la classe** et **l'élément** —, et par un plafond calculé qui, aux valeurs de création
ordinaires, se tient si haut qu'aucune partie normale ne l'atteint.

> ## ⚠ D'où viennent les valeurs de départ — correction du 2026-08-29
>
> Ce document a d'abord été écrit à partir de `s_ClassBaseStats_0056c7e0` (24/8/8/16/16/8/16 pour le
> Guerrier). **Cette table est celle de l'écran de groupe de mise au point, pas celle de la
> création de personnage** : ses deux seuls sites de lecture dans `Blaze.c` sont les lignes 82448
> et 82449, dans `RunDebugPartySelect`. Aucun personnage jouable n'en sort.
>
> L'écran de création résout les siennes **sur le CD** :
> `**(int **)(DAT_005e97c4 + 0x74) + (classe & 7) * 8`, lisible en clair dans le désassemblage de
> `FUN_00509470` à `0x005094e0` (`MOV EAX,[EDX+0x74]` · `MOV EDX,[EAX]` · `LEA ECX,[EDX+ECX*8]`),
> et la boucle qui suit n'en copie que **sept** octets. Relevées dans
> `E:\French\BLAZE.ALL` à l'offset `0x0090f0b4` — la suite `0d 08 08 09 0b 08 09` n'apparaît
> qu'**une seule fois** sur les 46 Mo de l'archive :
>
> | classe | FO | INT | VO | AGI | CON | PM | CH |
> |---|---|---|---|---|---|---|---|
> | 0 Guerrier | **13** | 8 | 8 | 9 | 11 | 8 | 9 |
> | 1 Prêtre | 9 | 10 | **13** | 9 | 8 | **13** | 11 |
> | 2 Voleur | 10 | 8 | 9 | **13** | 9 | 8 | 12 |
> | 3 Sorcier | 8 | 13 | 12 | 9 | 8 | **14** | 10 |
> | 4 Chasseur | 12 | 8 | 9 | 10 | 11 | 8 | 9 |
> | 5 Elfe | 11 | 10 | 10 | 9 | 9 | **12** | 9 |
> | 6 Nain | 14 | 8 | 9 | 8 | **15** | 8 | 9 |
> | 7 Fée | 6 | 10 | 10 | 13 | 6 | 11 | **16** |
>
> **Les sept lignes portent enfin leur nom** — relevés à l'écran par Vincent le 2026-08-29 : FO,
> INT, VO, AGI, CON, PM, CH. Les profils les confirment un par un, et le regroupement du code
> tombe juste : les physiques 0, 3, 4 sont **FO, AGI, CON**, les mentales 1, 2, 5 sont
> **INT, VO, PM**.
>
> **Ce que la vraie table change.** Les plafonds réels d'un personnage qui ne dépense aucun point
> vont de **158 à 237**, pas de 177 à 307. Et le tirage de gain, qui lit `départ >> 3`, ne vaut
> plus grand-chose au départ : à 13, `(13>>3)+1 = 2`, donc 0 ou 1 ; **à 6 — la force et la
> constitution de la Fée — le terme est toujours nul**. Le cas « une valeur sous 8 n'apporte rien
> à sa propre croissance », écrit plus bas comme une curiosité théorique, arrive en vrai.
>
> | classe | plafonds FO · INT · VO · AGI · CON · PM | P. physique | P. mentale | PV / PM au niveau 1 |
> |---|---|---|---|---|
> | 0 Guerrier | 221 · 177 · 177 · 186 · 204 · 177 | **70 %** | 43 % | 28 / 10 |
> | 1 Prêtre | 186 · 195 · 221 · 186 · 177 · 221 | 38 % | **66 %** | 22 / 21 |
> | 2 Voleur | 195 · 177 · 186 · 221 · 186 · 177 | 55 % | 55 % | 24 / 12 |
> | 3 Sorcier | 177 · 221 · 212 · 186 · 177 · 229 | 38 % | **66 %** | 22 / 21 |
> | 4 Chasseur | 212 · 177 · 186 · 195 · 204 · 177 | 54 % | 54 % | 26 / 12 |
> | 5 Elfe | 204 · 195 · 195 · 186 · 186 · 212 | 54 % | 54 % | 25 / 15 |
> | 6 Nain | 229 · 177 · 186 · 177 · 237 · 177 | **70 %** | 43 % | 30 / 10 |
> | 7 Fée | 158 · 195 · 195 · 221 · 158 · 204 | 40 % | **69 %** | 20 / 18 |
>
> **Ce n'est pas le Voleur, la classe chanceuse — c'est la Fée**, à 16 contre 12. Le paragraphe
> plus bas qui déduit « une statistique où seul le Voleur sort du lot est une chance » raisonnait
> sur la table de mise au point ; la conclusion tient (le libellé de l'écran dit CH), pas
> l'argument.
>
> **L'élément se CHOISIT.** `FUN_00508e00` lui donne un sélecteur gauche/droite masqué `& 7` sur
> `+0x151` : il ne découle pas de la classe. La huitième colonne de la table du CD est nulle
> partout, et seule celle de `DAT_0056c7e0` — la table de mise au point — porte un élément par
> classe.
>
> **Le document a été entièrement recalculé sur cette table le 2026-08-29** — les deux sections de
> tableaux par classe comprises, avec les espérances recoupées contre 2 000 parties simulées et les
> estimations de niveau contre 300. Ce qui suit est à jour ; ce cadre reste pour dire d'où viennent
> les valeurs, et pour que personne ne remette `DAT_0056c7e0` à leur place.
>
> ---

> **Le simulateur.** Tout ce que ce document calcule à la main se rejoue à l'écran :
> `tests\montee.bat` (ou `dotnet run --project OpenForesia.Tools -- montee`) ouvre une fenêtre où
> l'on compose un personnage comme à la création, on choisit un niveau visé et une graine, et
> `FUN_005391f0` est rejouée niveau par niveau — courbes, plafonds, tableau des écarts. Les
> chiffres ci-dessous ont été recoupés avec lui.

---

## Où vivent les valeurs

Le bloc de personnage porte **deux** copies des sept caractéristiques, et c'est le point qui
explique tout le reste.

| adresse | rôle |
|---|---|
| `+0x280` .. `+0x28c` | les **sept valeurs courantes** — celles qui montent |
| `+0x2a0` .. `+0x2ac` | les **mêmes sept valeurs, figées à la création du personnage** |
| `+0x120` .. `+0x12c` | la copie de travail, refaite à chaque montée par `FUN_00538ab0` |
| `+0x144` | le **plancher** de niveau |
| `+0x146` | le niveau courant |
| `+0x148` / `+0x14a` | PV max / PM max |
| `+0x14c` / `+0x14e` | PV / PM courants |
| `+0x158` | la **jauge** d'expérience |
| `+0x150` | la classe (masquée `& 7`) |
| `+0x151` | l'**élément** |

`FinalizeCharacterStats` (écran de création) écrit les sept mêmes valeurs dans `+0x280` **et**
`+0x2a0`. Ensuite, plus rien ne réécrit jamais `+0x2a0` : `FUN_00538ab0` ne recopie que
`+0x280` → `+0x120`.

**`+0x2a0` est donc la photographie de tes statistiques de départ, et c'est elle — pas tes
statistiques actuelles — qui dimensionne tous les gains, pour toute la partie.**

La septième valeur, `+0x2ac`, est **la chance** — l'écran de création l'appelle « CH ». Elle sert de
facteur commun à toute la fonction, et le tirage du butin s'en sert aussi. Les huit classes s'y
étalent de 9 à 16, la Fée en tête.

---

## Le palier, et pourquoi on ne gagne jamais deux niveaux d'un coup

```
palier = niveau / 4 + 0x30 + niveau * 2      (plafonné à 9999)
```

La jauge `+0x158` doit atteindre ce palier. Elle est ensuite **remise à zéro** — ce n'est pas un
total cumulé, et **le surplus est perdu**.

Le corps est écrit comme une boucle (`goto LAB_00539207` en queue de fonction), ce qui suggère
qu'un seul coup pourrait faire gagner plusieurs niveaux. **Ce n'est pas le cas** : la jauge est mise
à zéro *à l'intérieur* de la boucle, dès le franchissement, et le palier vaut au minimum `0x30`.
Le second tour ne peut donc jamais repasser le test.

> Le commentaire de `LevelUpCharacter_005391F0` affirmait le contraire jusqu'au 2026-08-29
> (« donc un seul coup peut faire gagner plusieurs niveaux »). Corrigé.

Le niveau courant `+0x146` reçoit `niveau + 1`, et le plancher `+0x144` avance d'un ; si le plancher
dépasse le niveau — parce qu'un effet l'avait abaissé — il le tire vers le haut.

---

## Les sept gains, tirés en premier

Avant que quoi que ce soit ne soit appliqué, la fonction tire sept valeurs dans
`DAT_005e85b0` (une réserve de brouillon que le compilateur de 1998 a mise en données plutôt que
sur la pile) :

```
gain[i] = rand() % ((départ[i] >> 3) + 1) + 1  +  rand() % ((chance_départ >> 3) + 1)
```

où `départ` est le bloc `+0x2a0`. Le minimum est **1**, le maximum
`(départ[i] >> 3) + (chance_départ >> 3) + 1`.

Aux valeurs de départ réelles, cela donne un gain de **1 à 3** dans la quasi-totalité des cas — un
Guerrier a 13 de FO et 9 de CH, donc `rand()%2 + 1 + rand()%2`. C'est minuscule, et c'est voulu : la
progression se joue sur la fréquence, pas sur l'ampleur, et **l'ampleur ne s'achète qu'à la
création**.

Les sept gains servent ensuite à trois choses différentes — les six caractéristiques, les deux
jauges (qui réutilisent `gain[4]` et `gain[5]`), et la chance (`gain[6]`).

### D'où vient l'amplitude visible à l'écran

Voir « +1 » à un niveau puis « +3 » au suivant sur la même caractéristique est normal : **les deux
termes sont retirés à chaque montée**. Le tableau donne la fourchette et la moyenne selon la valeur
de départ de la caractéristique et la chance de départ. Les cinq premières colonnes sont les chances
de départ que les huit classes portent réellement ; les deux dernières, ce qu'un pool exceptionnel
permet d'acheter.

| départ de la carac. | CH 9 | CH 10 | CH 11 | CH 12 | CH 16 | CH 48 | CH 99 |
|---|---|---|---|---|---|---|---|
| 6 | 1..2 (moy. 1,5) | 1..2 (1,5) | 1..2 (1,5) | 1..2 (1,5) | 1..3 (2,0) | 1..7 (4,0) | 1..13 (7,0) |
| 8 | 1..3 (2,0) | 1..3 (2,0) | 1..3 (2,0) | 1..3 (2,0) | 1..4 (2,5) | 1..8 (4,5) | 1..14 (7,5) |
| 10 | 1..3 (2,0) | 1..3 (2,0) | 1..3 (2,0) | 1..3 (2,0) | 1..4 (2,5) | 1..8 (4,5) | 1..14 (7,5) |
| 13 | 1..3 (2,0) | 1..3 (2,0) | 1..3 (2,0) | 1..3 (2,0) | 1..4 (2,5) | 1..8 (4,5) | 1..14 (7,5) |
| 16 | 1..4 (2,5) | 1..4 (2,5) | 1..4 (2,5) | 1..4 (2,5) | 1..5 (3,0) | 1..9 (5,0) | 1..15 (8,0) |
| 24 | 1..5 (3,0) | 1..5 (3,0) | 1..5 (3,0) | 1..5 (3,0) | 1..6 (3,5) | 1..10 (5,5) | 1..16 (8,5) |
| 48 | 1..8 (4,5) | 1..8 (4,5) | 1..8 (4,5) | 1..8 (4,5) | 1..9 (5,0) | 1..13 (7,0) | 1..19 (10,0) |
| 81 | 1..12 (6,5) | 1..12 (6,5) | 1..12 (6,5) | 1..12 (6,5) | 1..13 (7,0) | 1..17 (9,0) | 1..23 (12,0) |
| 99 | 1..14 (7,5) | 1..14 (7,5) | 1..14 (7,5) | 1..14 (7,5) | 1..15 (8,0) | 1..19 (10,0) | 1..25 (13,0) |

**Le minimum est toujours 1** — le `+ 1` de la formule est hors des deux tirages, donc une
caractéristique qui monte ne monte jamais de zéro. Le maximum vaut
`(départ >> 3) + (chance >> 3) + 1`.

**Les quatre premières colonnes sont identiques**, et c'est le fait le plus important du tableau :
entre 9 et 12 de chance, le décalage de trois bits donne le même `rand() % 2`. **Sept des huit
classes ne se distinguent donc en rien par l'ampleur de leurs gains** — seule la Fée, à 16, achète
un demi-point de plus sur chacune des sept lignes. Tout le reste de la table réelle tient dans les
trois premières lignes : les valeurs de départ vont de 6 à 15, soit `rand()%1`, `rand()%2` ou rien
de plus.

**Une caractéristique de départ sous 8 n'apporte rien du tout à sa propre croissance** :
`rand() % 1` vaut toujours 0. C'est le cas de la **force et de la constitution de la Fée**, à 6 —
les deux seules occurrences dans les huit classes, et elles sont exactement les deux lignes que la
Fée est censée être mauvaise à faire monter.

---

## Les PV et les PM montent toujours

```
+0x148 (PV max) += rand() % gain[4] + 2
+0x14a (PM max) += rand() % gain[5] + 1
```

Tous deux plafonnés à 9999. **Aucune condition, aucun tirage de réussite** : ce sont les deux seules
valeurs qui montent à chaque niveau sans exception. Noter que `gain[4]` et `gain[5]` sont *les mêmes*
tirages que ceux des caractéristiques 4 et 5 — le jeu ne re-tire pas.

---

## Les deux probabilités, par classe

`DAT_0056d310` porte un dword par classe. Chaque dword est lu **quatre octets deux par deux** :

```
chance_phys = chance_départ / diviseur_phys + base_phys
chance_ment = chance_départ / diviseur_ment + base_ment
```

Huit classes, mais **trois valeurs seulement** — donc trois familles de progression :

| dword | classes | physique | mentale |
|---|---|---|---|
| `0x04280241` | 0 Guerrier, 6 Nain | `chance/2 + 65` | `chance/4 + 40` |
| `0x023c0423` | 1 Prêtre, 3 Sorcier, 7 Fée | `chance/4 + 35` | `chance/2 + 60` |
| `0x03320332` | 2 Voleur, 4 Chasseur, 5 Elfe | `chance/3 + 50` | `chance/3 + 50` |

Les familles ne sont pas arbitraires : **les deux classes purement physiques**, **les trois
lanceurs de sorts**, **les trois hybrides**. Le même regroupement apparaît ailleurs dans le jeu.

Le test est `rand() % 100 <= probabilité`, donc une probabilité de 69 vaut **70 %**. Aux chances de
départ réelles, cela donne une valeur par classe et non par famille — les huit classes n'ont pas la
même chance de départ :

| classe | CH de départ | physique (FO, AGI, CON) | mentale (INT, VO, PM) |
|---|---|---|---|
| 0 Guerrier | 9 | **70 %** | **43 %** |
| 6 Nain | 9 | **70 %** | **43 %** |
| 1 Prêtre | 11 | **38 %** | **66 %** |
| 3 Sorcier | 10 | **38 %** | **66 %** |
| 7 Fée | 16 | **40 %** | **69 %** |
| 2 Voleur | 12 | **55 %** | **55 %** |
| 4 Chasseur | 9 | **54 %** | **54 %** |
| 5 Elfe | 9 | **54 %** | **54 %** |

**La Fée est la mieux servie des trois lanceurs**, à 69 % contre 66 %, parce qu'elle est la seule à
partir avec 16 de chance. Le Voleur prend le même avantage sur ses deux collègues hybrides, en plus
petit : 55 % contre 54 %.

Les six caractéristiques se répartissent ainsi, et l'ordre est en dur dans le code :

- **physiques** : indices **0, 3, 4** — **FO, AGI, CON**
- **mentales** : indices **1, 2, 5** — **INT, VO, PM**

La répartition se recoupe avec la table de départ du CD : le Guerrier a ses trois plus hautes
valeurs en FO, CON, AGI et le Sorcier en PM, INT, VO.

### À chance égale, quelle famille progresse le plus ?

La question se pose parce que les chances de départ diffèrent d'une classe à l'autre, et qu'on peut
vouloir séparer les deux effets. En sommant les six probabilités — trois physiques et trois
mentales — à chance identique :

| CH | Guerrier / Nain | Prêtre / Sorcier / Fée | Voleur / Chasseur / Elfe |
|---|---|---|---|
| 9 | **339** | 309 | 324 |
| 12 | **348** | 318 | 330 |
| 16 | **357** | 327 | 336 |
| 48 | **429** | 399 | 402 |
| 99 | 495 | 480 | **504** |

**La famille du Guerrier et du Nain est la plus généreuse à chance ordinaire**, et celle des
hybrides arrive deuxième. Aucune classe ne rattrape cet écart par sa chance de départ : la Fée, la
mieux dotée à 16, atteint 327 — toujours sous les 339 d'un Guerrier à 9. **La famille décide plus
que la chance, tant qu'on ne dépense pas de points de création.**

À très haute chance le classement s'inverse — les hybrides passent en tête (504) parce que leurs
deux formules divisent la chance par 3 au lieu de 2 et 4, donc les deux progressent ensemble au lieu
qu'une seule sature. Mais la lecture est trompeuse : à ce niveau-là les deux probabilités du
Guerrier valent 100 % et 65 %, et une probabilité de 100 % ne peut plus rien rendre de plus. Le
total masque le fait qu'une moitié est déjà certaine.

---

## Le plafond — décidé à la création, une fois pour toutes

> **Correction du 2026-08-29.** Une première rédaction de ce document affirmait que le plafond se
> recalculait à partir de la valeur *courante* et arrêtait donc toute progression vers 157. C'est
> faux, et une observation en jeu l'a démenti : un Nain de niveau 110 avec **496 en force**.
> Le plafond se calcule sur les valeurs **de départ**.

Une caractéristique ne monte que si elle est **sous** un plafond :

```
plafond = v / 8  -  v² / 16  +  (v + 10) * 10        où v = départ[i], le bloc +0x2a0
```

`Blaze.c` est sans ambiguïté — `psVar9 = (short *)(param_2 + 0x2a0);` juste avant la boucle. Le
plafond est donc **constant pour toute la partie** : c'est la valeur choisie à la création qui
décide, seule, jusqu'où la caractéristique pourra monter.

Puis l'**élément** (`+0x151`) l'ajuste :

- élément **0 à 5** — une caractéristique désignée par `DAT_0056d308` (`00 01 04 03 02 05`, une
  permutation de 0..5) reçoit **+1/5** ;
- élément **6 ou 7** — les six reçoivent **+1/20**.

**Et l'élément se CHOISIT** : `FUN_00508e00` lui donne un sélecteur gauche/droite masqué `& 7` sur
`+0x151`, dans un sous-état dédié de la création. Il ne découle pas de la classe — la huitième
colonne de la table du CD est nulle pour les huit lignes. Seule `DAT_0056c7e0`, la table de l'écran
de mise au point, porte un élément par classe, et c'est de là que venait l'affirmation inverse.

**C'est donc le seul choix de la création qui déplace un plafond sans coûter de point.** Un élément
favorisé rend un cinquième sur une caractéristique — 44 points sur un plafond de 221 — pour rien.

### La courbe culmine à 510, et redescend

Les neuf premières lignes sont la plage réelle des valeurs de classe (6 à 15) : c'est là que se joue
la partie d'un personnage qui ne dépense aucun point.

| départ | plafond | + élément favorisé | + élément 6/7 |
|---|---|---|---|
| 6 | 158 | 189 | 165 |
| 8 | 177 | 212 | 185 |
| 9 | 186 | 223 | 195 |
| 10 | 195 | 234 | 204 |
| 11 | 204 | 244 | 214 |
| 12 | 212 | 254 | 222 |
| 13 | 221 | 265 | 232 |
| 14 | 229 | 274 | 240 |
| 15 | 237 | 284 | 248 |
| 16 | 246 | 295 | 258 |
| 20 | 277 | 332 | 290 |
| 24 | 307 | 368 | 322 |
| 32 | 360 | 432 | 378 |
| 40 | 405 | 486 | 425 |
| 48 | 442 | 530 | 464 |
| 56 | 471 | 565 | 494 |
| 64 | 492 | 590 | 516 |
| 72 | 505 | 606 | 530 |
| **80 à 83** | **510** | **612** | **535** |
| 88 | 507 | 608 | 532 |
| 96 | 496 | 595 | 520 |
| 99 | 490 | 588 | 514 |

**Le maximum n'est pas à 99.** La courbe atteint son sommet pour un départ de **80 à 83** et
redescend ensuite : un départ de 99 plafonne à 490, soit **20 de moins** qu'un départ de 80 — tout
en coûtant 19 points de plus à la création. Mettre 99 dans une caractéristique est strictement
perdant sur les deux tableaux.

Le tirage lui-même est le même aux six emplacements :

```
si (v < plafond && rand() % 100 <= probabilité)  v += gain[i]
```

Le test précède l'addition, donc **la valeur finale dépasse légèrement le plafond** — d'au plus un
gain. Aux valeurs de départ de classe, cela vaut **deux ou trois points** ; il faut un personnage
monté à 99 partout pour que le dépassement approche 25. L'écrêtage à 999 qui suit les six tirages
reste inatteignable dans tous les cas.

### Recoupement avec l'observation

Un Nain de niveau 110 à 496 en force. Sans élément favorisé, un plafond de 496 correspond à un
départ de **66** exactement — et un Nain part à 14 de force, donc **52 points de création** placés
sur cette seule ligne.

Le rythme concorde : à un départ de 66 et 9 de chance, le gain moyen vaut
`1 + (66>>3)/2 + (9>>3)/2 = 5,5` et la probabilité physique du Nain est de 70 %, soit 3,85 points
par niveau. En 109 montées cela fait 420 points, plus les 66 du départ : 486, à trois points d'un
plafond de 496 que le dépassement franchit. Le compte tombe juste.

**Mais 52 points de création demandent un pool exceptionnel** — le pool ordinaire vaut 18 à 33, et
il faut plusieurs triples pour atteindre 52. Deux lectures restent ouvertes : ce Nain a eu une série
de dés remarquable, ou l'écran de personnage n'affiche pas `+0x280` mais `+0x120`, la copie de
travail, qui porte en plus les bonus d'équipement. **Non tranché** — la question à reposer à
l'observation est la valeur de départ de ce personnage, pas sa valeur finale.

---

## La chance, la seule statistique qui peut BAISSER

```
tirage = rand() % 100 + chance_départ / 4

tirage < 0x1f (31)          →  chance -= gain[6]
0x1f <= tirage < 0x47 (71)  →  gain[6] = 0, la chance ne bouge pas
tirage >= 0x47              →  chance += gain[6]
```

Bornée à **0..200** — pas à 999 comme les six autres. Le décalage `chance_départ / 4` est tout ce
qui sépare les huit classes, et à des chances de 9 à 16 il ne vaut que 2 à 4 :

| CH de départ | classes | perte | rien | gain |
|---|---|---|---|---|
| 9 | Guerrier, Chasseur, Elfe, Nain | 29 % | 40 % | 31 % |
| 10 | Sorcier | 29 % | 40 % | 31 % |
| 11 | Prêtre | 29 % | 40 % | 31 % |
| 12 | Voleur | 28 % | 40 % | 32 % |
| 16 | Fée | 27 % | 40 % | 33 % |
| 99 | (pool exceptionnel) | 7 % | 40 % | 53 % |

**Les huit classes sont donc à peu près à égalité** — la chance courante monte un peu plus souvent
qu'elle ne descend, de deux points de pourcentage. Il faut acheter de la chance à la création pour
que le déséquilibre devienne net.

C'est le seul champ du bloc de personnage qui recule à une montée de niveau. Rappel : c'est la
chance **courante** (`+0x28c`) qui bouge ; celle qui pilote la progression est `+0x2ac`, figée.

---

## Ce qui suit

Les dérivées sont refaites dans l'ordre — `FUN_00538ab0` (fige `+0x280` → `+0x120`),
`FUN_00538f50` (échelle de difficulté), `FUN_00538c50` (dérivées), `FUN_00538e20` (bonus
d'équipement), `FUN_00538fa0` (effets d'équipement, avec son écrêtage `0..999` qui avale les malus,
voir `legacy-bug.md`).

Puis un sort peut être appris, **réservé aux classes 1, 3, 5 et 7** (Prêtre, Sorcier, Elfe, Fée) :
un seul par montée, le premier de l'école encore inconnu dont le coût tienne dans le tirage. La
classe 5 voit son niveau ramené à `0x50` au-delà de `0x6d` avant le calcul du palier de sorts — un
plafonnement propre à elle.

Enfin, si au moins un niveau a été gagné, l'auréole (`0x29` avec le descripteur `0x005648a8`) et la
gerbe (`0x59`, qui mène à `RestoreVitalsEntity_00403710` et donc à un **soin complet**) naissent sur
le personnage. C'est pour cela que monter de niveau soigne : le jeu ne réutilise pas seulement
l'effet visuel du soin, il réutilise le soin.

---

## Ce que la création laisse mettre, et jusqu'où

Les exemples qui suivent n'ont de sens qu'avec les bornes de l'écran de répartition
(`EditCharacterStats`, `FUN_005098f0`) :

- les sept lignes partent des **valeurs de classe** lues sur le CD (voir la correction en tête) ;
- le **pool** part de **15** et reçoit la somme de **trois dés à six faces** (`rand() % 6 + 1`),
  soit 18 à 33 dans le cas ordinaire ;
- **un triple offre un relancer**, dont la somme s'ajoute au pool. Le pool est **plafonné à 99** ;
- une ligne ne peut pas descendre sous sa valeur de classe, ni monter **au-dessus de 99**.

Donc oui, **99 en chance est atteignable dès la création** — il faut **83 points pour la Fée**, qui
part de 16, et **90 pour les quatre classes qui partent de 9**. Cela suppose une longue série de
triples : un pool ordinaire de 25 points ne permet qu'un déplacement modeste, et c'est le cas
normal. **Les tableaux de cas limites plus bas décrivent donc des personnages exceptionnels, pas la
partie ordinaire.**

### Le seuil qui compte est le multiple de 8

Le gain tire dans `rand() % ((départ >> 3) + 1)`. Le décalage de trois bits fait que **les points
dépensés entre deux multiples de 8 n'élargissent pas le tirage** :

| valeur de départ | terme de tirage | apport moyen au gain |
|---|---|---|
| 0..7 | `rand() % 1` = **toujours 0** | 0 |
| 8..15 | `rand() % 2` | 0,5 |
| 16..23 | `rand() % 3` | 1,0 |
| 24..31 | `rand() % 4` | 1,5 |
| 32..39 | `rand() % 5` | 2,0 |
| … | … | … |
| 96..99 | `rand() % 13` | 6,0 |

Passer une caractéristique de 16 à 23 ne change **rien** à sa vitesse de progression ; la passer de
23 à 24 la change. Idem pour la chance. Les points comptent toujours pour la valeur de départ
elle-même — et donc pour le plafond — mais pour la *croissance*, seuls les franchissements de
multiple de 8 achètent quelque chose.

**Toutes les valeurs de départ des huit classes tiennent dans les deux premières lignes** : de 6 à
15, soit `rand() % 1` ou `rand() % 2`. Les deux 6 de la Fée sont donc les seules valeurs de classe
du jeu qui n'apportent strictement rien à leur propre croissance, et le premier point utile pour
n'importe quelle autre ligne est celui qui la porte à 16.

### La chance qui monte n'est pas celle qui compte

Point d'attention permanent : la progression lit **`+0x2ac`, la chance de DÉPART**, figée. La chance
qui monte et redescend en jeu est `+0x28c`. Perdre de la chance pendant la partie **ne ralentit
donc rien** ; en gagner n'accélère rien non plus.

---

## Les huit classes, caractéristique par caractéristique

Valeurs de départ de classe, sans aucun point dépensé et sans élément choisi. « P » est la
probabilité qu'elle monte à un niveau donné, « gain » sa valeur moyenne quand elle monte,
« espérance » le produit des deux — les points gagnés par niveau en moyenne. « Plafond » est fixé
par la valeur de départ ; « niveaux » estime combien il en faut pour l'atteindre.

Les espérances ont été recoupées avec 2 000 parties simulées par classe : elles tombent à 0,01
près.

**0 — Guerrier** · famille Guerrier/Nain · CH 9 · physique **70 %**, mental **43 %**

| # | carac. | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|---|
| 0 | FO | physique | 13 | 221 | 70 % | 2,0 | **1,40** | ~149 |
| 1 | INT | mental | 8 | 177 | 43 % | 2,0 | 0,86 | ~197 |
| 2 | VO | mental | 8 | 177 | 43 % | 2,0 | 0,86 | ~197 |
| 3 | AGI | physique | 9 | 186 | 70 % | 2,0 | 1,40 | ~126 |
| 4 | CON | physique | 11 | 204 | 70 % | 2,0 | 1,40 | ~138 |
| 5 | PM | mental | 8 | 177 | 43 % | 2,0 | 0,86 | ~197 |

**1 — Prêtre** · famille lanceurs · CH 11 · physique **38 %**, mental **66 %**

| # | carac. | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|---|
| 0 | FO | physique | 9 | 186 | 38 % | 2,0 | 0,76 | ~233 |
| 1 | INT | mental | 10 | 195 | 66 % | 2,0 | **1,32** | ~140 |
| 2 | VO | mental | 13 | 221 | 66 % | 2,0 | 1,32 | ~158 |
| 3 | AGI | physique | 9 | 186 | 38 % | 2,0 | 0,76 | ~233 |
| 4 | CON | physique | 8 | 177 | 38 % | 2,0 | 0,76 | ~222 |
| 5 | PM | mental | 13 | 221 | 66 % | 2,0 | 1,32 | ~158 |

**2 — Voleur** · famille hybrides · CH 12 · physique **55 %**, mental **55 %**

| # | carac. | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|---|
| 0 | FO | physique | 10 | 195 | 55 % | 2,0 | **1,10** | ~168 |
| 1 | INT | mental | 8 | 177 | 55 % | 2,0 | 1,10 | ~154 |
| 2 | VO | mental | 9 | 186 | 55 % | 2,0 | 1,10 | ~161 |
| 3 | AGI | physique | 13 | 221 | 55 % | 2,0 | 1,10 | ~189 |
| 4 | CON | physique | 9 | 186 | 55 % | 2,0 | 1,10 | ~161 |
| 5 | PM | mental | 8 | 177 | 55 % | 2,0 | 1,10 | ~154 |

**3 — Sorcier** · famille lanceurs · CH 10 · physique **38 %**, mental **66 %**

| # | carac. | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|---|
| 0 | FO | physique | 8 | 177 | 38 % | 2,0 | 0,76 | ~222 |
| 1 | INT | mental | 13 | 221 | 66 % | 2,0 | **1,32** | ~158 |
| 2 | VO | mental | 12 | 212 | 66 % | 2,0 | 1,32 | ~152 |
| 3 | AGI | physique | 9 | 186 | 38 % | 2,0 | 0,76 | ~233 |
| 4 | CON | physique | 8 | 177 | 38 % | 2,0 | 0,76 | ~222 |
| 5 | PM | mental | 14 | 229 | 66 % | 2,0 | 1,32 | ~163 |

**4 — Chasseur** · famille hybrides · CH 9 · physique **54 %**, mental **54 %**

| # | carac. | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|---|
| 0 | FO | physique | 12 | 212 | 54 % | 2,0 | **1,08** | ~185 |
| 1 | INT | mental | 8 | 177 | 54 % | 2,0 | 1,08 | ~156 |
| 2 | VO | mental | 9 | 186 | 54 % | 2,0 | 1,08 | ~164 |
| 3 | AGI | physique | 10 | 195 | 54 % | 2,0 | 1,08 | ~171 |
| 4 | CON | physique | 11 | 204 | 54 % | 2,0 | 1,08 | ~179 |
| 5 | PM | mental | 8 | 177 | 54 % | 2,0 | 1,08 | ~156 |

**5 — Elfe** · famille hybrides · CH 9 · physique **54 %**, mental **54 %**

| # | carac. | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|---|
| 0 | FO | physique | 11 | 204 | 54 % | 2,0 | **1,08** | ~179 |
| 1 | INT | mental | 10 | 195 | 54 % | 2,0 | 1,08 | ~171 |
| 2 | VO | mental | 10 | 195 | 54 % | 2,0 | 1,08 | ~171 |
| 3 | AGI | physique | 9 | 186 | 54 % | 2,0 | 1,08 | ~164 |
| 4 | CON | physique | 9 | 186 | 54 % | 2,0 | 1,08 | ~164 |
| 5 | PM | mental | 12 | 212 | 54 % | 2,0 | 1,08 | ~185 |

**6 — Nain** · famille Guerrier/Nain · CH 9 · physique **70 %**, mental **43 %**

| # | carac. | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|---|
| 0 | FO | physique | 14 | 229 | 70 % | 2,0 | **1,40** | ~154 |
| 1 | INT | mental | 8 | 177 | 43 % | 2,0 | 0,86 | ~197 |
| 2 | VO | mental | 9 | 186 | 43 % | 2,0 | 0,86 | ~206 |
| 3 | AGI | physique | 8 | 177 | 70 % | 2,0 | 1,40 | ~121 |
| 4 | CON | physique | 15 | 237 | 70 % | 2,0 | 1,40 | ~159 |
| 5 | PM | mental | 8 | 177 | 43 % | 2,0 | 0,86 | ~197 |

**7 — Fée** · famille lanceurs · CH 16 · physique **40 %**, mental **69 %**

| # | carac. | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|---|
| 0 | FO | physique | 6 | 158 | 40 % | 2,0 | 0,80 | ~190 |
| 1 | INT | mental | 10 | 195 | 69 % | 2,5 | **1,73** | ~107 |
| 2 | VO | mental | 10 | 195 | 69 % | 2,5 | 1,73 | ~107 |
| 3 | AGI | physique | 13 | 221 | 40 % | 2,5 | 1,00 | ~208 |
| 4 | CON | physique | 6 | 158 | 40 % | 2,0 | 0,80 | ~190 |
| 5 | PM | mental | 11 | 204 | 69 % | 2,5 | 1,73 | ~112 |

**Lecture d'ensemble.** Le Guerrier et le Nain progressent **1,6 fois plus vite en physique qu'en
mental** (1,40 contre 0,86) ; les lanceurs, 1,7 fois plus vite en mental (1,32 contre 0,76) ; les
trois hybrides ne font aucune différence, à 1,08 ou 1,10 partout. Aucune classe n'a une
caractéristique qui progresse plus vite qu'une autre du *même* type — seule la valeur de départ, par
ses multiples de 8, les sépare, et **à des valeurs de 6 à 15 elle ne les sépare presque jamais** :
sur les 48 lignes du tableau, le gain moyen vaut 2,0 partout sauf les quatre lignes de la Fée à 2,5.

**La Fée est la classe la plus rapide du jeu**, et de loin : la somme de ses six espérances vaut
**7,78** points par niveau contre 6,78 pour le Guerrier et 6,24 pour le Sorcier. Ses 16 de chance
de départ lui donnent trois points de probabilité et un demi-point de gain sur **chacune** des sept
lignes. Elle le paie par ses deux 6 en FO et CON, qui ne dépasseront jamais 158.

**La conclusion la plus importante du tableau est la colonne « niveaux ».** Aucune caractéristique
n'atteint son plafond avant le niveau **107**, et la plupart en demandent 150 à 230. Une partie
ordinaire s'arrête bien avant. **Pour un personnage qui ne dépense aucun point de création, le
plafond n'existe pas** : il est hors de portée, et tout ce qui compte est la vitesse. C'est
exactement l'inverse de ce que ce document affirmait quand il calculait sur la table de mise au
point, où les valeurs de départ deux fois plus grandes rendaient les plafonds atteignables vers le
niveau 110.

**Les deux seules exceptions sont l'INT et la VO de la Fée**, à ~107 niveaux, et sa PM à ~112 : une
partie menée très loin les verra buter. C'est la seule classe pour qui la question du plafond se
pose sans passer par l'écran de répartition.

**Le paradoxe du Nain.** Son agilité, sa plus mauvaise ligne (8), est celle qui atteint son plafond
le plus tôt de tout le jeu — ~121 niveaux. Une valeur de départ basse donne un plafond bas *et* le
fait atteindre plus vite, parce que la probabilité, elle, ne dépend que de la famille. Le rang final
d'une caractéristique se décide donc bien plus à la création qu'en jouant.

---

## Les cas limites, classe par classe

La question posée : **tout mettre dans la caractéristique signature de la classe**, ou **tout mettre
dans la chance** ? On suppose ici un pool exceptionnel permettant d'amener une ligne à 99.

### Ce que 99 en chance change avant tout : deux probabilités saturent

`rand() % 100 <= P` est toujours vrai dès que `P >= 99`. En remontant les trois formules :

| famille | condition de certitude | chance nécessaire |
|---|---|---|
| Guerrier / Nain — **physique** | `chance/2 + 65 >= 99` | **68** |
| Lanceurs — **mental** | `chance/2 + 60 >= 99` | **78** |
| Guerrier / Nain — mental | `chance/4 + 40 >= 99` | 236, **impossible** |
| Lanceurs — physique | `chance/4 + 35 >= 99` | 256, **impossible** |
| Hybrides — les deux | `chance/3 + 50 >= 99` | 147, **impossible** |

**Deux combinaisons seulement peuvent atteindre la certitude**, et elles sont exactement le type
« naturel » de leur famille. Un Guerrier à 68 de chance de départ voit ses trois caractéristiques
physiques monter **à chaque niveau, sans exception** ; une Fée à 78 de chance, ses trois mentales.
Les hybrides plafonnent à 84 % même à 99 de chance.

### Les probabilités à 99 de chance

| famille | physique | mental |
|---|---|---|
| Guerrier / Nain | **100 %** | 65 % |
| Prêtre / Sorcier / Fée | 60 % | **100 %** |
| Voleur / Chasseur / Elfe | 84 % | 84 % |

### Les huit comparaisons

« Signature » = la caractéristique la plus haute de la classe. Trois répartitions, sans élément
choisi :

- **A** — 99 dans la signature, chance laissée à sa valeur de classe ;
- **B** — 99 en chance, signature laissée à sa valeur de classe ;
- **C** — **81** dans la signature (le sommet de la courbe), chance laissée à sa valeur de classe.

Les colonnes « niv » ont été recoupées avec 300 parties simulées sur les cinq cas les plus
représentatifs : la mesure tombe deux niveaux au-dessus de l'estimation, l'écart d'arrondi du
premier niveau.

| classe | signature | A : 99 dans la signature | B : 99 en CH | C : 81 dans la signature |
|---|---|---|---|---|
| 0 Guerrier | FO (13) | plafond **490** · 5,25/niv · ~74 niv · coût 86 | plafond **221** · 7,50/niv · ~28 niv · coût 90 | plafond **510** · 4,55/niv · ~94 niv · coût 68 |
| 1 Prêtre | VO/PM (13) | plafond **490** · 4,95/niv · ~79 niv · coût 86 | plafond **221** · 7,50/niv · ~28 niv · coût 88 | plafond **510** · 4,29/niv · ~100 niv · coût 68 |
| 2 Voleur | AGI (13) | plafond **490** · 4,13/niv · ~95 niv · coût 86 | plafond **221** · 6,30/niv · ~33 niv · coût 87 | plafond **510** · 3,58/niv · ~120 niv · coût 68 |
| 3 Sorcier | PM (14) | plafond **490** · 4,95/niv · ~79 niv · coût 85 | plafond **229** · 7,50/niv · ~29 niv · coût 89 | plafond **510** · 4,29/niv · ~100 niv · coût 67 |
| 4 Chasseur | FO (12) | plafond **490** · 4,05/niv · ~97 niv · coût 87 | plafond **212** · 6,30/niv · ~32 niv · coût 90 | plafond **510** · 3,51/niv · ~122 niv · coût 69 |
| 5 Elfe | PM (12) | plafond **490** · 4,05/niv · ~97 niv · coût 87 | plafond **212** · 6,30/niv · ~32 niv · coût 90 | plafond **510** · 3,51/niv · ~122 niv · coût 69 |
| 6 Nain | CON (15) | plafond **490** · 5,25/niv · ~74 niv · coût 84 | plafond **237** · 7,50/niv · ~30 niv · coût 90 | plafond **510** · 4,55/niv · ~94 niv · coût 66 |
| 7 Fée | AGI (13) | plafond **490** · 3,20/niv · ~122 niv · coût 86 | plafond **221** · 4,50/niv · ~46 niv · coût 83 | plafond **510** · 2,80/niv · ~153 niv · coût 68 |

**La Fée est le cas tordu du tableau** : sa caractéristique la plus haute est l'**agilité**, une
ligne *physique*, et sa famille est celle des lanceurs, qui monte le physique à 40 %. Sa signature
au sens du tableau est donc la ligne qu'elle est la plus mauvaise à faire grandir. Lue autrement —
en plaçant les 99 sur sa PM à 11 — elle passe à 5,52/niv et ~71 niveaux, à égalité avec le Prêtre et
le Sorcier au lieu du dernier rang. **« La plus haute » n'est pas « la bonne » : c'est le type qui
décide, pas la valeur de départ.**

### Ce que la comparaison dit

**Les points de création achètent des plafonds ; la chance achète de la vitesse.** C'est la
formulation la plus utile des trois formules réunies, et elle sépare proprement les deux
répartitions.

**A contre C : 81 bat 99, partout et sans exception.** Le plafond y est plus haut (510 contre 490)
et il coûte 18 points de moins. Il n'y a aucun cas de figure, dans aucune classe, où porter une
caractéristique de 81 à 99 soit rentable — la courbe redescend, et les 18 points sont mieux placés
n'importe où ailleurs. **C'est le piège principal de l'écran de création.**

**B contre A/C : deux marchandises différentes.** Une caractéristique laissée à sa valeur de classe
est plafonnée entre 158 et 237 pour toute la partie, et aucune quantité de niveaux ne la fera monter
plus haut. En échange, 99 en chance donne sur **les six** caractéristiques des probabilités relevées
et des gains bien plus gros — le second terme passe de `rand() % 2` à `rand() % 13`, soit +5,5 en
moyenne partout. Sur un Guerrier, la somme des six espérances passe de **6,78** points par niveau —
le total du tableau de la classe 0 plus haut — à **37,1**, cinq fois et demie plus. Le personnage
atteint donc ses plafonds bien plus tôt, mais ce sont des plafonds bas.

**Et c'est précisément ce qui rend B attirant à la vraie table.** Sans points dépensés, aucun
plafond n'est atteignable avant le niveau 107 : les plafonds ne sont pas une contrainte, ce sont des
décors. Mettre les points en chance ne renonce donc à rien de concret sur une partie ordinaire,
alors que mettre les points dans une caractéristique laisse les six autres à leur rythme d'origine.
**C'est l'inverse de la conclusion que ce document tirait sur la table de mise au point**, où les
plafonds étaient réellement atteints vers le niveau 110.

**La question reste le niveau visé**, mais le seuil a bougé. Une partie qui s'arrête vers 50 ou 70 —
c'est-à-dire la quasi-totalité des parties — ne verra jamais un plafond sans répartition extrême :
c'est la vitesse qui compte, donc la chance. Il faut viser au-delà du niveau 100 *et* avoir concentré
la création sur une ligne pour que le plafond redevienne la contrainte, et là seulement 81 bat 99.

**Trois avantages de plus pour la chance, qui ne coûtent rien.** Elle nourrit aussi le tirage du
butin (`odds + chance/2` sur mille pour qu'un objet tombe plutôt que de l'argent, et
`+0x12c/8 + 0x3c + chance/2` pour sa qualité) et le tirage de la chance courante, qui à 99 de départ
monte 53 % du temps et ne descend que 7 % du temps au lieu de 31 % / 29 %.

### Trois cas limites de plus

**Les deux 6 de la Fée** — force et constitution — ne dépasseront **jamais 158**, quel que soit le
nombre de niveaux et quelle que soit la chance. C'est le plancher du jeu. Juste au-dessus, les
caractéristiques de départ à 8 sont bornées à 177 : le Guerrier en a trois (INT, VO, PM), le
Sorcier deux, le Nain trois. **Aucune classe n'échappe à deux ou trois lignes structurellement
bornées sous 190** ; ce qui distingue les classes, c'est lesquelles.

**Le dépassement du plafond.** Le test précède l'addition, donc la valeur finale s'arrête entre le
plafond et le plafond plus un gain — deux ou trois points aux valeurs de classe. Un personnage qui
affiche 223 avec un plafond calculé de 221 n'a rien d'anormal.

**La chance à sa valeur plancher.** Une chance de départ **inférieure à 8** annulerait complètement
le second terme du gain (`rand() % 1` vaut toujours 0). Cela n'arrive pas, mais il s'en faut d'un
point : la plus basse valeur de classe est **9** — le Guerrier, le Chasseur, l'Elfe et le Nain —, et
l'écran de création interdit de descendre sous la valeur de classe. Sur la table de mise au point,
où le minimum était 16, le cas paraissait purement théorique ; à la vraie table, le jeu passe à un
point de le rendre réel.

---

## Ce que ça veut dire en jouant

Six conséquences tombent directement des formules, et elles sont vérifiables en jeu :

1. **Le bloc `+0x2a0` étant figé, la création décide de trois choses à la fois** : la valeur de
   départ, la taille des gains, et **le plafond**. Un point placé là achète les trois ; un point
   placé ailleurs n'en achète aucune.
2. **En partie ordinaire, le plafond ne se rencontre jamais.** Aux valeurs de classe, il faut 107 à
   233 niveaux pour l'atteindre. Ce que la création achète réellement, ce n'est donc pas un plafond
   lointain, c'est **la vitesse** — sauf si l'on concentre assez de points pour changer d'échelle.
3. **Le plafond culmine pour un départ de 80 à 83, et redescend après.** Porter une caractéristique
   à 99 la plafonne plus bas qu'à 81, pour 18 points de plus. C'est le seul endroit du système où
   dépenser davantage donne strictement moins — et cela ne concerne que les répartitions extrêmes,
   les seules où le plafond compte.
4. **L'élément est gratuit et il déplace un plafond d'un cinquième.** C'est le seul choix de la
   création qui achète 44 points de plafond sans coûter un point de pool.
5. **La classe décide de la fréquence, pas de l'ampleur.** Un Guerrier voit son physique monter
   sept fois sur dix et son mental un peu plus de quatre fois sur dix ; un Voleur voit tout monter
   une fois sur deux. Le gain, lui, ne dépend que des valeurs de départ — et aux valeurs de classe
   il vaut 2,0 pour sept classes sur huit.
6. **La classe chanceuse est la Fée, et son avantage est général.** Ses 16 de CH lui donnent trois
   points sur ses deux probabilités, un demi-point sur le gain moyen de **chacune** des sept
   lignes, et du butin plus fréquent et de meilleure qualité. C'est la seule classe dont la somme
   des six espérances dépasse 7 points par niveau. Elle le paie par ses deux 6, en force et en
   constitution, bornés à 158 pour toute la partie.

---

## Ce qui n'est pas établi

~~**Le nom des caractéristiques.**~~ **Résolu le 2026-08-29** : les sept lignes sont **FO, INT, VO,
AGI, CON, PM, CH**, relevées à l'écran. L'inférence notée ici — l'indice 0 est la force, l'indice 5
la magie — était juste, mais elle s'appuyait sur la mauvaise table (voir la correction en tête de
document) ; c'est le libellé de l'écran qui tranche, pas le profil. Seul « PM » reste ambigu :
l'écran ne dit pas s'il faut lire « puissance magique » ou « points de magie ». Il nourrit la jauge
de PM par la moitié de (VO + PM).

**Le rôle de `+0x12c`.** L'autre contributeur du tirage de qualité du butin n'a pas de rôle
identifié au-delà d'être la copie de travail de la chance.
