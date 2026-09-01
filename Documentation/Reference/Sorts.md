# Le système de sorts

Comment un personnage apprend, garde et lance un sort, relevé le 2026-08-29 dans
`TryLearnSpell` (`0x0053962b`), `PickCastTarget_0051A5D0` (`0x0051a5d0`),
`CastSpell_0053A980` (`0x0053a980`), la branche `case 1/3/5/7` de `ApplyGroundMotion_0051A020`, et
les tables de `.data` qu'ils lisent.

Compagnon de `MonteeDeNiveau.md` : l'apprentissage est une étape de la montée de niveau, et il n'y
en a pas d'autre en jeu normal.

---

## Qui lance des sorts

**Quatre classes sur huit**, et la liste est écrite deux fois dans le code, aux deux bouts de la
chaîne :

- `TryLearnSpell` sort immédiatement si la classe n'est pas 1, 3, 5 ou 7 ;
- la posture d'incantation est le `case 1: case 3: case 5: case 7:` de
  `ApplyGroundMotion_0051A020`. Les autres classes ont autre chose à cet emplacement — la charge
  pour le Voleur (2), l'objet consommable pour le Chasseur (4), la marche pour le Guerrier (0) et le
  Nain (6).

L'école est `+0x2b5`, posée à la création depuis `s_ClassStartIcon_00562ab8`
(`[0, 1, 0, 0, 3, 0, 0, 2]`) :

| classe | école | sorts dans le fichier | accessibles par montée de niveau |
|---|---|---|---|
| 1 Prêtre | 1 | 24 | 22 |
| 3 Sorcier | 0 | 29 | 26 |
| 5 Elfe | 0 | 29 | **20** (voir plus bas) |
| 7 Fée | 2 | 20 | 16 |

**L'école 3 est inatteignable — mais elle n'est pas vide.** Le Chasseur s'en voit attribuer une, et
elle ne mène nulle part : la table de pointeurs `PTR_DAT_00564640` porte trois rangées puis un mot
nul, `DAT_0056d330` donne `00 00 00 00 08` pour cette école, la création n'initialise le masque des
sorts connus que pour les écoles 0, 1 et 2 — et de toute façon le Chasseur n'entre jamais dans la
posture d'incantation.

> Le CD, lui, y porte **sept fiches**, et ce sont des **plantes** : Lavande, Sauge, Verveine,
> Mercuriale, Fenouil, Hysopius, Belladone. Toutes sept ont la même forme — aucune combinaison, un
> coût d'un point, et le même sous-type `0x4f`, qui n'est donc qu'un seul corps. `DAT_00564600`
> n'en déclare qu'une. Une herboristerie de Chasseur a été écrite puis débranchée, pas « prévue et
> jamais remplie ». Relevé le 2026-08-31 en lisant le CD ; voir
> `Documentation/Etat/SortsAtteignables.md`.

**Ce qui paramètre le corps, c'est `+0x06`, pas `+0x12`.** Correction du 2026-09-01, en transcrivant
`FUN_0047a5d0` : le corps lit l'octet `+0x06` de son entité — **l'indice de la plante dans son
école**, que `SpawnEffectEntity` lui recopie depuis le lanceur — et s'en sert pour deux choses, et
deux seulement : choisir l'**effet** dans une table de sept pointeurs relevée à `0x0056d4c0`, et
choisir l'**atlas** de son halo dans une table de huit octets du répertoire de ressources
(`+0x94` → `+0x1c`). Tout le reste de la mise en scène est commun aux sept.

**Les sept effets sont un bloc à part.** `0x0056d4c0` n'est pas la table des 48 effets d'objet :
celle-là commence huit dwords plus loin, à `0x0056d4e0`, et les deux blocs sont séparés par un
pointeur **nul** (l'entrée 7). Aucun autre appelant du binaire n'atteint les sept. Elles se
distinguent aussi par où elles écrivent : les effets d'objet appareillent par `FUN_00538870` et
travaillent sur le **bloc de combat**, les sept plantes se contentent de `FUN_0053a860` et écrivent
**directement dans la fiche de personnage** — la même mémoire vue `0x100` octets plus bas, puisque
`FUN_005387c0` rend `fiche + 0x100`.

| plante | effet | ce qu'il fait |
|---|---|---|
| Lavande | `0x0053ace0` | rend `rand() % 27 + 4 + carac/4` **PV**, plafonnés au maximum |
| Sauge | `0x0053ad80` | la même chose en **PM** |
| Verveine | `0x0053ae20` | lève le bit `0x1` de `+0x140` et sa durée `+0x1e0` |
| Mercuriale | `0x0053ae70` | lève le bit `0x4` et sa durée `+0x18e` |
| Fenouil | `0x0053aec0` | lève le bit `0x100` (la pétrification) et `+0x19a` |
| Hysopius | `0x0053af10` | lève le bit `0x2` — **sans toucher à la durée `+0x1e2`** |
| Belladone | `0x0053af60` | lève le bit `0x10` — **sans toucher à la durée `+0x192`** |

Les cinq levées ont chacune leur jumelle exacte parmi les effets d'objet 34 à 42, à cette
dissymétrie près : les jumelles d'Hysopius et de Belladone remettent la durée à zéro, elles non. Ce
n'est pas une lecture approximative — leurs corps font 70 octets contre 79 pour les trois autres, et
les neuf octets d'écart sont exactement le `mov word [eax+…],0` qui leur manque. Sans conséquence en
jeu, l'école étant injouable.

---

## Où vivent les données

Le répertoire de ressources principal porte à `+0x9c` un tableau de pointeurs, un par école. Chaque
école est un tableau de **fiches de `0x30` octets**. Les champs relevés :

| décalage | rôle | lu par |
|---|---|---|
| `+0x10` | le **genre d'effet**, indice dans la table des 48 effets `0x0056d4e0` | `CastSpell_0053A980` |
| `+0x11` | premier paramètre de l'effet | idem, quand l'appelant passe `-1` |
| `+0x12` | second paramètre | idem |
| `+0x13` | la **durée d'incantation**, en images | la posture, dans `+0x14` de l'entité |
| `+0x14` | le **coût en PM** (mot) | recopié dans `personnage + 0x204` |
| `+0x1d` | le **coût d'apprentissage** (octet) | `TryLearnSpell` |
| `+0x1f` | la **longueur de la combinaison** de boutons | `PickCastTarget_0051A5D0` |
| `+0x20`..`+0x2f` | la **combinaison** elle-même, un coup par octet | idem |

Les seize octets de combinaison tiennent **exactement** entre `+0x20` et la fin de la fiche, et le
tampon de saisie du joueur est plafonné à `0x10` coups. Les deux bornes se confirment l'une l'autre.

> **La combinaison n'est pas faite de directions**, contrairement à ce que ce document a dit
> jusqu'au 2026-08-31. La saisie est `(manette[2..3] >> 8) & 0xf0` et le masque qui l'ouvre est
> `0xf000` : ce sont les quatre bits que `s_PadButtonMasks_00564f18` nomme **triangle, rond, croix
> et carré**, pas ceux de la croix directionnelle (`0x0010`..`0x0080`). Un octet peut porter
> plusieurs bits — deux boutons tenus dans la même image ne font qu'un seul coup.
> `Documentation/Etat/SortsAtteignables.md` donne la combinaison de chaque sort, lue sur le CD.

Le nombre de sorts par école est dans `DAT_00564600` : `1d 18 14` — **29, 24, 20**. La rangée
cumulée voisine (`00 1d 35 49`) est la somme courante des trois, ce qui recoupe le relevé.

---

## Le masque des sorts connus

`personnage + 0x160`, **huit octets par école** — soit 64 bits pour au plus 29 sorts.

À la création, `FinalizeCharacterStats` pose le **bit 0** des trois écoles réelles (`+0x160`,
`+0x168`, `+0x170`). Tout personnage naît donc en connaissant le premier sort de son école, même
une classe qui ne lancera jamais rien.

**Ce masque n'a qu'un seul écrivain en jeu normal** — la vérification a porté sur les 98 000 lignes
de `Blaze.c` autant que sur la reconstruction. Il n'y a ni parchemin, ni maître, ni objet qui
apprenne un sort : **seule la montée de niveau en donne.**

> La même région est lue à un second endroit — la branche d'incantation rapide de
> `ApplyGroundMotion_0051A020` la traite comme **huit compteurs de charges** et décrémente
> `charges[sort]`, là où tout le reste la lit comme un masque de bits. Les deux vues coexistent dans
> le binaire et sont transcrites telles quelles. `TODO(REVERSE)` : établir laquelle est la bonne, ou
> si les deux chemins ne se recouvrent jamais.

---

## Apprendre un sort

À chaque montée de niveau, et **au plus un sort par montée** (la boucle finit par un `break`).

**Le palier** vient du **plancher de niveau** `+0x144`, comparé à quatre seuils :

| plancher de niveau | palier |
|---|---|
| 1..19 | 0 |
| 20..49 | 1 |
| 50..79 | 2 |
| 80..109 | 3 |
| 110 et au-delà | 4 |

`DAT_0056d330` dit alors **combien de sorts de l'école sont accessibles** à ce palier :

| école | palier 0 | 1 | 2 | 3 | 4 |
|---|---|---|---|---|---|
| 0 (Sorcier, Elfe) | 5 | 10 | 15 | 20 | **26** |
| 1 (Prêtre) | 5 | 11 | 16 | 19 | **22** |
| 2 (Fée) | 3 | 7 | 9 | 12 | **16** |
| 3 (morte) | 0 | 0 | 0 | 0 | 8 |

**L'Elfe est plafonnée au palier 3.** Son corps ramène le niveau à `0x50` (80) dès qu'il dépasse
`0x6d` (109), ce qui la fait retomber dans la tranche 80..109. Elle n'atteint donc jamais le palier
4 et **s'arrête à 20 sorts sur les 26 accessibles à son école** — six de moins que le Sorcier, qui
partage pourtant la même école.

> Le commentaire de `TryLearnSpell` disait « elle ne dépasse jamais le palier 2 » jusqu'au
> 2026-08-29. C'est le palier **3** : `80 > 0x4f` est vrai, `80 > 0x6d` est faux.

**La portée du tirage** décide ensuite quel sort tombe :

```
portée = rand() % (courant[1] + 1) / 8  +  départ[1] + (courant[1] >> 3)
```

`courant[1]` est la deuxième caractéristique (`+0x122`), `départ[1]` sa valeur figée à la création
(`+0x2a2`) — le champ `+0x154` porte la somme des deux derniers termes, recalculée par
`ComputeDerivedStats` à chaque montée. **La deuxième caractéristique est donc la statistique
d'apprentissage**, et elle compte deux fois : par sa valeur de départ et par sa valeur courante.

La boucle parcourt les sorts accessibles **dans l'ordre**, saute ceux dont le coût est nul, saute
ceux déjà connus, et **apprend le premier dont le coût d'apprentissage (`+0x1d`) tient dans la
portée**. Un coût élevé n'est donc pas seulement rare : il bloque aussi tout ce qui le suit tant
qu'il n'est pas franchi.

Quand un sort est appris, l'entité du personnage porte brièvement le descripteur de son école et
lâche un `0xbc` — l'annonce visuelle.

---

## Les sorts hors d'atteinte

Le tableau ci-dessus s'arrête sous le compte réel de chaque école :

| école | sorts | apprenables | **jamais appris en jeu normal** |
|---|---|---|---|
| 0 | 29 | 26 | les indices 26, 27, 28 |
| 1 | 24 | 22 | 22, 23 |
| 2 | 20 | 16 | 16, 17, 18, 19 |

Ces sorts existent, ont une fiche complète, et **`PickCastTarget_0051A5D0` balaye jusqu'au compte
réel** (`DAT_00564600`), pas jusqu'au palier — leur combinaison est donc reconnue. Ce qui manque est
le bit dans le masque, et rien en jeu normal ne le pose.

---

## La triche « Gurkha » les débloque tous

`CheatCodeProbe` guette la séquence **`Gurkha`** frappée pendant que le moteur de texte dessine, et
met `DAT_005633a8` à zéro. Ce verrou vaut **1** au chargement.

Le listeur de sorts du menu porte alors ceci :

```csharp
if (Mem.s_DebugModeLatch_005633a8 == 0) known[id >> 5] |= 1u << (id & 0x1f);
```

**Une fois la triche active, ouvrir la page de sorts apprend tout ce qu'elle affiche** — y compris
les sorts que la montée de niveau ne donne jamais. C'est le second écrivain du masque, et le seul
autre.

---

## Lancer un sort

La posture vit dans `ApplyGroundMotion_0051A020`, sur `entité + 0x18` :

**État 5 — l'incantation.** Le bouton est maintenu. Le compte à rebours `+0x14` part de la durée
`+0x13` de la fiche du sort **mémorisé** (`+0x2b6`), et le coût de ce sort est déjà recopié dans
`+0x204`. Chaque bouton de face pressé s'empile dans `entrée[0x0c + n]` sous la forme
`(manette >> 8) & 0xf0`, et `entrée[0x0b]` compte les coups, plafonné à seize.

**Le relâchement — le choix du sort.** `PickCastTarget_0051A5D0` compare la combinaison saisie à
celle de chaque fiche de l'école, longueur d'abord, puis octet par octet. Elle rend l'indice du
premier sort qui correspond, ou `-1`.

- combinaison reconnue **et** sort connu → son coût remplace `+0x204`, l'indice est retenu, on passe
  à l'état 6 ;
- sinon → la posture est abandonnée, sans effet et sans coût.

**État 6 — le déclenchement**, à la fin de l'animation :

```csharp
if (*(ushort*)(perChar + 0x204) <= *(short*)(perChar + 0x14e))
{
    *(ushort*)(perChar + 0x14e) -= *(ushort*)(perChar + 0x204);
    CastSpell_0053A980(slot, perChar[0x2b5], indice, -1, -1);
}
```

**Le coût n'est prélevé que s'il est couvert, et sinon rien ne se passe du tout** : l'incantation
part, l'animation se joue, et le sort ne sort pas. Il n'y a ni message ni son de refus à cet
endroit — le personnage a simplement l'air de rater.

**L'annulation** (bouton 1 pendant l'incantation) laisse l'animation aller au bout mais lâche un
`0xbd` et n'arme rien.

`CastSpell_0053A980` finit le travail : elle écrit la famille et l'indice dans `+6` de l'entité
(famille en octet haut, indice en octet bas), neutralise `+0x74`, `+0x78` et `+0x0a` le temps de
l'appel, puis fait naître l'entité d'effet dont le genre est `fiche[0x10]`. Les deux paramètres
`fiche[0x11]` et `fiche[0x12]` ne servent que quand l'appelant passe `-1` — ce que fait
l'incantation, et ce que le script d'objet ne fait jamais.

---

## Le sort mémorisé, et le menu rapide

`+0x2b6` est le sort courant, `+0x2bf` le témoin de celui d'avant. Le menu rapide — l'anneau
au-dessus du personnage — parcourt l'école **en sautant ceux qui ne sont pas appris**, borné par
`DAT_00564600`. La page de sorts du grand menu écrit `+0x2b6` de la même façon.

C'est ce sort mémorisé qui fixe la durée d'incantation avant même que la combinaison ne soit saisie
— donc **le temps de préparation est celui du sort affiché, pas celui du sort qui sortira**.

---

## Ce qui n'est pas établi

**Les 48 effets.** `0x0056d4e0` porte quarante-huit fonctions, de `0x0053afb0` à `0x0053cdf0` ;
`ApplyStatusEffect_0053B3C0` et ses trois sœurs (`b4b0`, `b580`, `b660`) ainsi que
`ApplyTrapEffect_0053C420` en font partie, transcrites parce que les pièges de coffre y mènent. Les
autres attendent. C'est là que vivent les dégâts et les altérations d'état des sorts.

**Le nom des écoles.** Il vit dans les fichiers du CD, pas dans le binaire ; rien n'a été relevé.
Le nom des **sorts**, lui, l'a été le 2026-08-31 : ce sont les seize premiers octets de chaque
fiche, et `Documentation/Etat/SortsAtteignables.md` les donne tous, avec leur combinaison, leur
coût et le corps qu'ils font naître.

**La double lecture de `+0x160`** — masque de bits contre compteurs de charges, voir plus haut.

**Le rôle de `+0x1fc4`**, le masque de boutons comparé à la manette pour ouvrir la posture.
