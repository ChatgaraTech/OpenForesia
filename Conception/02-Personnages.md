# Les personnages

Sources : `Documentation/Reference/MonteeDeNiveau.md` (tout le détail, avec les tableaux par classe),
`Screens/State04/CharacterCreation.cs` (`ComputeDerivedStats`), `Documentation/Reference/Sorts.md`.
Le simulateur `tests\montee.bat` rejoue la progression niveau par niveau.

## Les huit classes

Chaque classe a un nom masculin et un nom féminin, et pour trois d'entre elles ce sont deux mots
différents (**établi**, `BLAZE.ALL` à `0x0090dea4` et `0x0090def0`).

| # | masculin | féminin | rôle | magie | bouton de classe |
|---|---|---|---|---|---|
| 0 | Guerrier | Guerrière | mêlée, bouclier | — | marche |
| 1 | Prêtre | Prêtresse | soins, soutien | école 1 | incantation |
| 2 | Voleur | Voleuse | mêlée rapide | — | charge |
| 3 | Magicien | Magicienne | dégâts magiques | école 0 | incantation |
| 4 | Chasseur | Chasseresse | arc | (école 3, débranchée) | objet consommable |
| 5 | Elfe | Sylphide | hybride rapière + magie | école 0, bridée | incantation |
| 6 | Kobold | Naine | mêlée lourde, bouclier | — | marche |
| 7 | Lutin | Fée | sorts de soutien et d'altération | école 2 | incantation |

Le dépôt emploie par continuité « Sorcier », « Nain » et « Fée ». Seuls le Guerrier et le Nain
portent un bouclier ; les autres classes ont des armures qui en tiennent lieu.

## Les sept caractéristiques

**FO** (force), **INT** (intelligence), **VO** (volonté), **AGI** (agilité), **CON** (constitution),
**PM** (lu « puissance magique » : c'est elle qui nourrit la MATT), **CH** (chance). Libellés relevés
à l'écran ; seul « PM » reste ambigu entre « puissance magique » et « points de magie ».

Elles se rangent en deux familles, **et l'ordre est en dur dans le code** :
- **physiques** : FO, AGI, CON ;
- **mentales** : INT, VO, PM.

La chance est à part : elle ne monte ni ne descend comme les autres (voir plus bas).

### Valeurs de départ par classe (établi)

| classe | FO | INT | VO | AGI | CON | PM | CH | PV / PM au niveau 1 |
|---|---|---|---|---|---|---|---|---|
| Guerrier | **13** | 8 | 8 | 9 | 11 | 8 | 9 | 28 / 10 |
| Prêtre | 9 | 10 | **13** | 9 | 8 | **13** | 11 | 22 / 21 |
| Voleur | 10 | 8 | 9 | **13** | 9 | 8 | 12 | 24 / 12 |
| Sorcier | 8 | 13 | 12 | 9 | 8 | **14** | 10 | 22 / 21 |
| Chasseur | 12 | 8 | 9 | 10 | 11 | 8 | 9 | 26 / 12 |
| Elfe | 11 | 10 | 10 | 9 | 9 | **12** | 9 | 25 / 15 |
| Nain | 14 | 8 | 9 | 8 | **15** | 8 | 9 | 30 / 10 |
| Fée | 6 | 10 | 10 | 13 | 6 | 11 | **16** | 20 / 18 |

## Les valeurs dérivées — ce que le combat lit

Le combat ne lit presque jamais les caractéristiques : il lit **huit valeurs dérivées**, recalculées
à chaque montée de niveau et à chaque échange de coups. `niv` est le niveau. Toutes les divisions
tronquent, et chaque valeur est plafonnée à 999 (**établi**).

| valeur | formule |
|---|---|
| **ATT** | (niv + FO) / 4 |
| **DEF** | ((CON/4 + AGI + CON) / 4 + niv) / 4 |
| **Force d'altération I** | ((FO + AGI) / 2 + niv) / 4 |
| **Résistance I** | ((AGI/4 + AGI + CON) / 2 + niv/2) / 4 |
| **MATT** | (niv + PM) / 4 |
| **MDEF** | ((INT/4 + VO + INT) / 4 + niv) / 4 |
| **Force d'altération II** | ((PM + VO) / 2 + niv) / 4 |
| **Résistance II** | ((VO/4 + VO + INT) / 2 + niv/2) / 4 |

Les familles « I » et « II » sont les deux familles d'altérations d'état, physiques et mentales
([03-Combat](03-Combat.md)).

**Un fait qui surprend, et qui est établi** : l'équipement s'ajoute **après** ce calcul. Un objet qui
donne +20 en FO **ne fait pas monter l'ATT** ; seul compte son bonus d'ATT propre. Les
caractéristiques apportées par les objets servent à d'autres lectures (certains pouvoirs d'objet,
l'intensité de quelques effets).

> **Pour un jeu neuf.** C'est probablement une maladresse plus qu'un dessein : le joueur voit monter
> une caractéristique sans effet. Soit l'équipement nourrit les caractéristiques *avant* les dérivées,
> soit les objets ne donnent que des dérivées.

## La création

**Établi** (`EditCharacterStats`, `FUN_005098f0`) :
- on choisit une classe, un nom, un **élément** (parmi huit, librement — il ne découle pas de la
  classe) ;
- une **réserve de points** part de 15 et reçoit **3d6** (18 à 33 en général) ; **un triple offre un
  relancer** qui s'ajoute, la réserve étant plafonnée à 99 ;
- on répartit la réserve sur les sept lignes, sans descendre sous la valeur de classe ni dépasser 99.

**La création décide de presque tout**, parce que la progression lit les valeurs *de départ*, figées
pour la partie : elles fixent à la fois la taille des gains, et le plafond de chaque caractéristique.

## L'élément choisi

Il donne un **plafond plus haut d'un cinquième** à une caractéristique, ou d'un vingtième aux six :

| élément | pousse |
|---|---|
| Feu | FO |
| Eau | INT |
| Terre | CON |
| Air | AGI |
| Lumière | VO |
| Nuit | PM |
| Divin | les six, d'un vingtième |
| Maléfique | les six, d'un vingtième |

C'est le seul choix de création qui déplace un plafond sans coûter de point. Les noms d'éléments à la
création (Nuit, Divin, Maléfique) ne sont pas exactement ceux des objets (Ténèbres, Sacré, Mal) ; ce
sont les mêmes huit cases.

## La montée de niveau

**Établi** (`LevelUpCharacter_005391F0`) :

```
seuil d'expérience = niv / 4 + 48 + niv × 2           (plafonné à 9999)
```

La jauge d'expérience **repart à zéro** à chaque niveau, et **le surplus est perdu** : on ne gagne
jamais deux niveaux d'un coup.

À chaque niveau :

1. **PV max** += un petit tirage + 2 ; **PM max** += un petit tirage + 1 — toujours, sans condition.
2. Pour **chaque** caractéristique physique, une probabilité de classe décide si elle monte ; idem
   pour chaque mentale. Le montant est petit (1 à 3 en général) et ne dépend que des valeurs de
   départ :
   `gain = rand() % (départ/8 + 1) + 1 + rand() % (chance_départ/8 + 1)`.
3. Une caractéristique ne monte que sous son **plafond** :
   `plafond = v/8 − v²/16 + (v + 10) × 10`, où `v` est la valeur de départ, puis ajusté par
   l'élément. Aux valeurs de classe, il est entre 158 et 237, et il faut 107 à 233 niveaux pour
   l'atteindre.
4. **La chance** peut monter (≈ 31 %), rester (40 %) ou **baisser** (≈ 29 %). Bornée à 0..200.
5. Un **sort** peut être appris (au plus un par niveau, [04-Magie](04-Magie.md)).

| famille de classe | classes | physique monte | mentale monte |
|---|---|---|---|
| mêlée | Guerrier, Nain | 70 % | 43 % |
| lanceurs | Prêtre, Sorcier, Fée | 38 à 40 % | 66 à 69 % |
| hybrides | Voleur, Chasseur, Elfe | 54 à 55 % | 54 à 55 % |

Les écarts à l'intérieur d'une famille viennent de la chance de départ.

**Curiosités établies** :
- le plafond culmine pour un départ de 80 à 83, et **redescend** au-delà : mettre 99 plafonne plus
  bas que 81 ;
- seuls les **multiples de 8** des valeurs de départ élargissent les gains : passer de 16 à 23 ne
  change rien, passer à 24 change quelque chose ;
- la progression lit la chance **de départ**, figée ; la chance courante, qui fluctue, ne sert
  qu'au butin.

> **Pour un jeu neuf.** Le cœur — probabilité de classe × petit gain × plafond fixé à la création —
> est sain et lisible. Trois choses à reconsidérer : le surplus d'expérience perdu (punitif sans
> raison visible) ; les paliers de 8 invisibles (un joueur ne peut pas les deviner) ; le plafond qui
> baisse quand on investit trop (contre-intuitif).

## À établir

- le rôle exact de deux champs voisins de la chance (`+0x12c`, et la petite valeur `+0x28e`
  plafonnée à 15, qui pourrait être une chance de critique) ;
- comment l'expérience se gagne exactement en dehors des coups portés (des **cristaux d'expérience**
  existent comme objets à ramasser).
