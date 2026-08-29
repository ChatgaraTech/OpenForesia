# La montée de niveau

Ce que le jeu calcule quand un personnage gagne un niveau, relevé le 2026-08-29 dans
`FUN_005391f0` (`0x005391f0`, transcrite sous `LevelUpCharacter_005391F0`) et dans les tables de
`.rdata` qu'elle lit. Écrit parce que la question se cherche deux fois : à l'écran, la progression
a l'air purement aléatoire.

Elle l'est en partie. Mais l'aléatoire est encadré par trois choses fixes — **les statistiques de
départ**, **la classe** et **l'élément** — et par un plafond calculé qui arrête la progression
bien avant la borne apparente.

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

La septième valeur, `+0x2ac`, est lue comme **la chance**. L'identification n'est pas un libellé du
binaire mais elle tient : dans `s_ClassBaseStats_0056c7e0` ce septième champ vaut 16 pour les huit
classes **sauf le Voleur, à 24**, et une statistique où seul le Voleur sort du lot est une chance.
Elle sert de facteur commun à toute la fonction, et le tirage du butin s'en sert aussi.

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

Avec une statistique de départ à 24 et une chance de départ à 16, cela donne un gain de **1 à 6**.
C'est petit, et c'est voulu : la progression se joue sur la fréquence, pas sur l'ampleur.

Les sept gains servent ensuite à trois choses différentes — les six caractéristiques, les deux
jauges (qui réutilisent `gain[4]` et `gain[5]`), et la chance (`gain[6]`).

### D'où vient l'amplitude visible à l'écran

Voir « +1 » à un niveau puis « +4 » au suivant sur la même caractéristique est normal : **les deux
termes sont retirés à chaque montée**. Le tableau donne la fourchette et la moyenne selon la valeur
de départ de la caractéristique et la chance de départ.

| départ de la carac. | chance 16 | chance 24 | chance 48 | chance 99 |
|---|---|---|---|---|
| 8 | 1..4 (moy. 2,5) | 1..5 (3,0) | 1..8 (4,5) | 1..14 (7,5) |
| 16 | 1..5 (3,0) | 1..6 (3,5) | 1..9 (5,0) | 1..15 (8,0) |
| 20 | 1..5 (3,0) | 1..6 (3,5) | 1..9 (5,0) | 1..15 (8,0) |
| 24 | 1..6 (3,5) | 1..7 (4,0) | 1..10 (5,5) | 1..16 (8,5) |
| 48 | 1..9 (5,0) | 1..10 (5,5) | 1..13 (7,0) | 1..19 (10,0) |
| 81 | 1..13 (7,0) | 1..14 (7,5) | 1..17 (9,0) | 1..23 (12,0) |
| 99 | 1..15 (8,0) | 1..16 (8,5) | 1..19 (10,0) | 1..25 (13,0) |

**Le minimum est toujours 1** — le `+ 1` de la formule est hors des deux tirages, donc une
caractéristique qui monte ne monte jamais de zéro. Le maximum vaut
`(départ >> 3) + (chance >> 3) + 1`.

**Oui, la chance élargit l'amplitude**, mais elle n'est que la moitié de l'affaire : le premier
terme vient de la caractéristique elle-même. Une force de départ à 8 ne dépassera jamais +4 tant que
la chance reste à 16, quoi qu'il arrive.

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

Le test est `rand() % 100 <= probabilité`, donc une probabilité de 73 vaut **74 %**. Avec la chance
de départ standard de 16 :

| famille | physique | mentale |
|---|---|---|
| Guerrier / Nain | **74 %** | **45 %** |
| Prêtre / Sorcier / Fée | **40 %** | **69 %** |
| Voleur / Chasseur / Elfe | **56 %** | **56 %** |

Le Voleur, avec sa chance de départ à 24, monte à **59 %** sur les deux.

Les six caractéristiques se répartissent ainsi, et l'ordre est en dur dans le code :

- **physiques** : indices **0, 3, 4**
- **mentales** : indices **1, 2, 5**

La répartition se recoupe avec `s_ClassBaseStats_0056c7e0` : le Guerrier a ses trois plus hautes
valeurs en 0, 3, 4 et le Sorcier en 1, 2, 5.

### À chance égale, quelle famille progresse le plus ?

La question se pose parce que le Voleur est la seule classe dont la chance de départ diffère, et
qu'on peut vouloir séparer les deux effets. En sommant les six probabilités — trois physiques et
trois mentales — à chance identique :

| chance | Guerrier / Nain | Prêtre / Sorcier / Fée | Voleur / Chasseur / Elfe |
|---|---|---|---|
| 16 | **357** | 327 | 336 |
| 24 | **375** | 345 | 354 |
| 48 | **411** | 381 | 393 |
| 99 | **495** | 480 | 504 |

**La famille du Guerrier et du Nain est la plus généreuse à chance ordinaire**, et la famille des
hybrides — celle du Voleur — arrive deuxième. Le Voleur ne progresse donc **pas** mieux que les
autres par nature : à chance égale il est derrière un Guerrier. Ses 24 de chance de départ lui
rendent 354 contre 357, ce qui les met à égalité en pratique.

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

C'est bien l'élément et non la classe : `record[0x151]` est rempli depuis la **huitième** colonne de
`s_ClassBaseStats_0056c7e0`, celle que `LISEZMOI.md` du bestiaire a identifiée comme l'élément.

### La courbe culmine à 510, et redescend

| départ | plafond | + élément favorisé | + élément 6/7 |
|---|---|---|---|
| 8 | 177 | 212 | 185 |
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
gain, soit une vingtaine de points dans le pire cas. L'écrêtage à 999 qui suit les six tirages reste
inatteignable : même 612 + 24 n'en approche pas.

### Recoupement avec l'observation

Un Nain de niveau 110 à 496 en force se relit sans peine. Sans élément favorisé, un plafond de 496
correspond à un départ de **66** ; avec l'élément favorisé, 496 se situe encore sous le plafond de
612 qu'un départ vers 80 lui donnerait, et la progression peut continuer.

Le rythme concorde aussi : à un départ de 66, le gain moyen vaut `(66 >> 3) / 2 + 2 = 6` et la
probabilité physique du Nain est de 74 %, soit 4,44 points par niveau. En 109 montées cela fait
484 points — bien assez pour buter dans le plafond avant le niveau 110, ce qui est exactement ce
qu'on observe.

---

## La chance, la seule statistique qui peut BAISSER

```
tirage = rand() % 100 + chance_départ / 4

tirage < 0x1f (31)          →  chance -= gain[6]
0x1f <= tirage < 0x47 (71)  →  gain[6] = 0, la chance ne bouge pas
tirage >= 0x47              →  chance += gain[6]
```

Bornée à **0..200** — pas à 999 comme les six autres. Avec la chance de départ à 16, le tirage
couvre 4..103, ce qui donne **27 % de perte, 40 % rien, 33 % de gain**. À 24 (le Voleur) :
25 % / 40 % / 35 %.

C'est le seul champ du bloc de personnage qui recule à une montée de niveau.

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

- les sept lignes partent des **valeurs de classe** de `s_ClassBaseStats_0056c7e0` ;
- le **pool** part de **15** et reçoit la somme de **trois dés à six faces** (`rand() % 6 + 1`),
  soit 18 à 33 dans le cas ordinaire ;
- **un triple offre un relancer**, dont la somme s'ajoute au pool. Le pool est **plafonné à 99** ;
- une ligne ne peut pas descendre sous sa valeur de classe, ni monter **au-dessus de 99**.

Donc oui, **99 en chance est atteignable dès la création** — il faut 83 points pour un personnage
qui part de 16, ce qui suppose une belle série de triples. Un pool ordinaire de 25 points ne permet
qu'un déplacement modeste.

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
elle-même — mais pour la *croissance*, seuls les franchissements de multiple de 8 achètent quelque
chose.

### La chance qui monte n'est pas celle qui compte

Point d'attention permanent : la progression lit **`+0x2ac`, la chance de DÉPART**, figée. La chance
qui monte et redescend en jeu est `+0x28c`. Perdre de la chance pendant la partie **ne ralentit
donc rien** ; en gagner n'accélère rien non plus.

---

## Les huit classes, caractéristique par caractéristique

Valeurs de départ de classe, sans aucun point dépensé et sans élément favorisé. « P » est la
probabilité qu'elle monte à un niveau donné, « gain » sa valeur moyenne quand elle monte,
« espérance » le produit des deux — les points gagnés par niveau en moyenne. « Plafond » est fixé
par la valeur de départ ; « niveaux » estime combien il en faut pour l'atteindre.

**0 — Guerrier** · famille Guerrier/Nain · chance 16 · physique **74 %**, mental **45 %**

| # | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|
| 0 | physique | 24 | 307 | 74 % | 3,5 | **2,59** | ~109 |
| 1 | mental | 8 | 177 | 45 % | 2,5 | 1,13 | ~150 |
| 2 | mental | 8 | 177 | 45 % | 2,5 | 1,13 | ~150 |
| 3 | physique | 16 | 246 | 74 % | 3,0 | 2,22 | ~104 |
| 4 | physique | 16 | 246 | 74 % | 3,0 | 2,22 | ~104 |
| 5 | mental | 8 | 177 | 45 % | 2,5 | 1,13 | ~150 |

**1 — Prêtre** · famille des lanceurs · chance 16 · physique **40 %**, mental **69 %**

| # | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|
| 0 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 1 | mental | 16 | 246 | 69 % | 3,0 | 2,07 | ~111 |
| 2 | mental | 24 | 307 | 69 % | 3,5 | **2,42** | ~117 |
| 3 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 4 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 5 | mental | 16 | 246 | 69 % | 3,0 | 2,07 | ~111 |

**2 — Voleur** · famille des hybrides · **chance 24** · physique **59 %**, mental **59 %**

| # | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|
| 0 | physique | 8 | 177 | 59 % | 3,0 | 1,77 | ~96 |
| 1 | mental | 8 | 177 | 59 % | 3,0 | 1,77 | ~96 |
| 2 | mental | 8 | 177 | 59 % | 3,0 | 1,77 | ~96 |
| 3 | physique | 24 | 307 | 59 % | 4,0 | **2,36** | ~120 |
| 4 | physique | 16 | 246 | 59 % | 3,5 | 2,07 | ~111 |
| 5 | mental | 8 | 177 | 59 % | 3,0 | 1,77 | ~96 |

**3 — Sorcier** · famille des lanceurs · chance 16 · physique **40 %**, mental **69 %**

| # | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|
| 0 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 1 | mental | 16 | 246 | 69 % | 3,0 | 2,07 | ~111 |
| 2 | mental | 16 | 246 | 69 % | 3,0 | 2,07 | ~111 |
| 3 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 4 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 5 | mental | 24 | 307 | 69 % | 3,5 | **2,42** | ~117 |

**4 — Chasseur** · famille des hybrides · chance 16 · physique **56 %**, mental **56 %**

| # | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|
| 0 | physique | 16 | 246 | 56 % | 3,0 | 1,68 | ~137 |
| 1 | mental | 8 | 177 | 56 % | 2,5 | 1,40 | ~121 |
| 2 | mental | 8 | 177 | 56 % | 2,5 | 1,40 | ~121 |
| 3 | physique | 20 | 277 | 56 % | 3,0 | **1,68** | ~153 |
| 4 | physique | 20 | 277 | 56 % | 3,0 | **1,68** | ~153 |
| 5 | mental | 8 | 177 | 56 % | 2,5 | 1,40 | ~121 |

**5 — Elfe** · famille des hybrides · chance 16 · physique **56 %**, mental **56 %**

| # | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|
| 0 | physique | 8 | 177 | 56 % | 2,5 | 1,40 | ~121 |
| 1 | mental | 20 | 277 | 56 % | 3,0 | **1,68** | ~153 |
| 2 | mental | 16 | 246 | 56 % | 3,0 | 1,68 | ~137 |
| 3 | physique | 8 | 177 | 56 % | 2,5 | 1,40 | ~121 |
| 4 | physique | 8 | 177 | 56 % | 2,5 | 1,40 | ~121 |
| 5 | mental | 20 | 277 | 56 % | 3,0 | **1,68** | ~153 |

**6 — Nain** · famille Guerrier/Nain · chance 16 · physique **74 %**, mental **45 %**

| # | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|
| 0 | physique | 20 | 277 | 74 % | 3,0 | **2,22** | ~116 |
| 1 | mental | 8 | 177 | 45 % | 2,5 | 1,13 | ~150 |
| 2 | mental | 8 | 177 | 45 % | 2,5 | 1,13 | ~150 |
| 3 | physique | 16 | 246 | 74 % | 3,0 | 2,22 | ~104 |
| 4 | physique | 20 | 277 | 74 % | 3,0 | **2,22** | ~116 |
| 5 | mental | 8 | 177 | 45 % | 2,5 | 1,13 | ~150 |

**7 — Fée** · famille des lanceurs · chance 16 · physique **40 %**, mental **69 %**

| # | type | départ | plafond | P | gain | espérance | niveaux |
|---|---|---|---|---|---|---|---|
| 0 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 1 | mental | 20 | 277 | 69 % | 3,0 | **2,07** | ~124 |
| 2 | mental | 20 | 277 | 69 % | 3,0 | **2,07** | ~124 |
| 3 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 4 | physique | 8 | 177 | 40 % | 2,5 | 1,00 | ~169 |
| 5 | mental | 16 | 246 | 69 % | 3,0 | 2,07 | ~111 |

**Lecture d'ensemble.** Le Guerrier et le Nain progressent **deux fois plus vite en physique qu'en
mental** ; les trois lanceurs, l'inverse ; les trois hybrides ne font aucune différence. Aucune
classe n'a une caractéristique qui progresse plus vite qu'une autre du *même* type — seule la valeur
de départ, par ses multiples de 8, les sépare.

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
favorisé :

- **A** — 99 dans la signature, chance laissée à sa valeur de classe ;
- **B** — 99 en chance, signature laissée à sa valeur de classe ;
- **C** — **81** dans la signature (le sommet de la courbe), chance laissée à sa valeur de classe.

| classe | signature | A : 99 dans la signature | B : 99 en chance | C : 81 dans la signature |
|---|---|---|---|---|
| 0 Guerrier | #0 (24) | plafond **490** · 5,92/niv · ~66 niv · coût 75 | plafond 307 · 8,50/niv · ~33 niv · coût 83 | plafond **510** · 5,18/niv · ~83 niv · **coût 57** |
| 1 Prêtre | #2 (24) | plafond **490** · 5,52/niv · ~71 niv · coût 75 | plafond 307 · 8,50/niv · ~33 niv · coût 83 | plafond **510** · 4,83/niv · ~89 niv · **coût 57** |
| 2 Voleur | #3 (24) | plafond **490** · 5,02/niv · ~78 niv · coût 75 | plafond 307 · 7,14/niv · ~40 niv · coût 75 | plafond **510** · 4,43/niv · ~97 niv · **coût 57** |
| 3 Sorcier | #5 (24) | plafond **490** · 5,52/niv · ~71 niv · coût 75 | plafond 307 · 8,50/niv · ~33 niv · coût 83 | plafond **510** · 4,83/niv · ~89 niv · **coût 57** |
| 4 Chasseur | #3/#4 (20) | plafond **490** · 4,48/niv · ~87 niv · coût 79 | plafond 277 · 6,72/niv · ~38 niv · coût 83 | plafond **510** · 3,92/niv · ~109 niv · **coût 61** |
| 5 Elfe | #1/#5 (20) | plafond **490** · 4,48/niv · ~87 niv · coût 79 | plafond 277 · 6,72/niv · ~38 niv · coût 83 | plafond **510** · 3,92/niv · ~109 niv · **coût 61** |
| 6 Nain | #0/#4 (20) | plafond **490** · 5,92/niv · ~66 niv · coût 79 | plafond 277 · 8,00/niv · ~32 niv · coût 83 | plafond **510** · 5,18/niv · ~83 niv · **coût 61** |
| 7 Fée | #1/#2 (20) | plafond **490** · 5,52/niv · ~71 niv · coût 79 | plafond 277 · 8,00/niv · ~32 niv · coût 83 | plafond **510** · 4,83/niv · ~89 niv · **coût 61** |

### Ce que la comparaison dit

**Les points de création achètent des plafonds ; la chance achète de la vitesse.** C'est la
formulation la plus utile des trois formules réunies, et elle sépare proprement les deux
répartitions.

**A contre C : 81 bat 99, partout et sans exception.** Le plafond y est plus haut (510 contre 490)
et il coûte 18 points de moins. Il n'y a aucun cas de figure, dans aucune classe, où porter une
caractéristique de 81 à 99 soit rentable — la courbe redescend, et les 18 points sont mieux placés
n'importe où ailleurs. **C'est le piège principal de l'écran de création.**

**B contre A/C : deux marchandises différentes.** Une caractéristique laissée à sa valeur de classe
est plafonnée à 277 ou 307 pour toute la partie, et aucune quantité de niveaux ne la fera monter
plus haut. En échange, 99 en chance donne sur **les six** caractéristiques des probabilités
relevées, et des gains plus gros (le second terme passe de `rand() % 3` à `rand() % 13`, soit +5 en
moyenne partout). Sur un Guerrier, la somme des six espérances passe de **10,4** points par
niveau — le total du tableau de la classe 0 plus haut — à **39,1**, presque le quadruple. Le
personnage atteint donc ses plafonds bien plus tôt, mais ce sont des plafonds plus bas.

**En pratique, la question est le niveau visé.** Une partie qui s'arrête vers le niveau 40 ou 50
n'approchera aucun plafond : c'est la vitesse qui compte, donc la chance. Une partie menée jusqu'au
niveau 110 les atteindra tous : ce sont les plafonds qui comptent, donc les points placés dans les
caractéristiques — vers 80, pas vers 99.

**Trois avantages de plus pour la chance, qui ne coûtent rien.** Elle nourrit aussi le tirage du
butin (`odds + chance/2` sur mille pour qu'un objet tombe plutôt que de l'argent, et
`+0x12c/8 + 0x3c + chance/2` pour sa qualité) et le tirage de la chance courante, qui à 99 de départ
monte 53 % du temps et ne descend que 7 % du temps au lieu de 33 % / 27 %.

### Trois cas limites de plus

**Une caractéristique laissée à 8** ne dépassera **jamais 177**, quel que soit le nombre de niveaux
et quelle que soit la chance. Cinq des six caractéristiques de la Fée et du Sorcier partent à 8 ou
16 ; ces classes sont donc structurellement bornées là où le Guerrier part à 24 sur sa signature.

**Le dépassement du plafond.** Le test précède l'addition, donc la valeur finale s'arrête entre le
plafond et le plafond plus un gain — jusqu'à une vingtaine de points au-dessus. Un personnage qui
affiche 315 avec un plafond calculé de 307 n'a rien d'anormal.

**La chance à sa valeur plancher.** Une chance de départ **inférieure à 8** annulerait complètement
le second terme du gain (`rand() % 1` vaut toujours 0). Cela ne peut pas arriver : la plus basse
valeur de classe est 16, et l'écran de création interdit de descendre sous la valeur de classe. Le
cas est mort — mais il explique pourquoi le plancher a été mis là.

---

## Ce que ça veut dire en jouant

Quatre conséquences tombent directement des formules, et elles sont vérifiables en jeu :

1. **Le bloc `+0x2a0` étant figé, la création décide de trois choses à la fois** : la valeur de
   départ, la taille des gains, et **le plafond**. Un point placé là achète les trois ; un point
   placé ailleurs n'en achète aucune.
2. **Le plafond culmine pour un départ de 80 à 83, et redescend après.** Porter une caractéristique
   à 99 la plafonne plus bas qu'à 81, pour 18 points de plus. C'est le seul endroit du système où
   dépenser davantage donne strictement moins.
3. **La classe décide de la fréquence, pas de l'ampleur.** Un Guerrier voit son physique monter
   trois fois sur quatre et son mental moins d'une fois sur deux ; un Voleur voit tout monter une
   fois sur deux. Le gain, lui, ne dépend que des valeurs de départ.
4. **Le Voleur est avantagé par sa chance de départ à 24, et par elle seule.** Sa famille de
   progression est la plus plate des trois (56 % partout contre 74 %/45 % pour le Guerrier) ; à
   chance égale il progresse *moins* qu'un Guerrier ou un Nain. Ce que ses 24 lui donnent, c'est
   +3 points sur ses deux probabilités, +0,5 sur le gain moyen de **chacune** des sept
   caractéristiques, et du butin plus fréquent et de meilleure qualité.

---

## Ce qui n'est pas établi

**Le nom des caractéristiques.** Des sept, **seule la troisième est nommée à ce jour — Volonté**,
relevée à l'écran (voir `Bestiaire/LISEZMOI.md`). Les six autres restent numérotées ici plutôt que
devinées. Le profil des classes suggère fortement que l'indice 0 est la force (le Guerrier y a 24)
et que l'indice 5 est la magie (le Sorcier y a 24), mais **c'est une inférence, pas un relevé** :
aucun libellé du binaire ne l'a confirmée.

**Le rôle de `+0x12c`.** L'autre contributeur du tirage de qualité du butin n'a pas de rôle
identifié au-delà d'être la copie de travail de la chance.
