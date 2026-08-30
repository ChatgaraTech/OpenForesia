# Ce qu'utiliser un objet peut atteindre

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-item-effects.sh > Documentation/Etat/ObjetsAtteignables.md
```

Obtenu en deroulant hors ligne les deux scripts des 2 370 fiches d'objet du CD
(`Documentation/Reference/Bestiaire/objets.csv`, colonne `octets`). Aucune partie
n'a ete jouee pour le produire : les scripts sont de la donnee.

## Pourquoi on peut s'y fier

Le derouleur a ete verifie contre la realite avant d'etre cru : la **Petite potion**
(zone 0, fiche 0) se decode en

```
0090  11  cible-recherche   00        <- le mode 0, celui du meme camp
0092  02  saut-si           95 00
0095  00  consommer         64        <- cent pour cent
0097  0a  naitre-cible      40 00 01  <- sous-type 0x40 -> 0x00446790
009b  0b  alu-banque        20 4b 29
009f  0b  alu-banque        20 41 28
00a3  06  alu-stats-cible   4c 59 20  <- +0x4c, LES POINTS DE VIE
00a7  14  particule-cible   01 20 01
00ab  06  alu-stats-cible   48 4c 20
00af  06  alu-stats-cible   4c 75 20
00b3  06  alu-stats-cible   4c 58 20
00b7  ff  FIN
```

Ce sont **exactement les deux levees rencontrees en jeu le 2026-08-30, dans cet
ordre** : l'opcode 17 d'abord, puis le sous-type `0x40`. Le derouleur les avait
toutes deux, sans qu'on ait eu besoin de jouer.

**Et il corrige une idee fausse au passage.** Une potion ne soigne pas par la table des
48 effets : elle soigne par l'**opcode 6**, l'unite arithmetique, sur le champ
`+0x4c` du bloc de la cible. C'est pourquoi le genre 43 — la guerison — n'apparait
pas dans le tableau des effets plus bas. La table des 48 sert aux **alterations
d'etat** et aux guerisons des sorts et des pieges, pas au soin d'une potion.

---

## Les gestionnaires d'entite que les objets font naitre

Colonne **emplois** : le nombre de sites d'appel dans l'ensemble des scripts, pas le
nombre d'objets. C'est l'ordre de travail — le plus employe d'abord.

| gestionnaire | emplois | sous-type | ecrit | zones |
|---|---:|---|---|---|
| `0x00446790` | 259 | 0x40 | oui | 0,1,3,4,5,6,7,8,10 |
| `0x004753D0` | 215 | 0xbd | oui | 0,1,2,3,4,5,6,7,8,9,10 |
| `0x00446DD0` | 189 | 0x3e | **non** | 0,1,3,4,5,6,9,10 |
| `0x00445F60` | 166 | 0x44 | **non** | 0,1,3,4,5,6,7,8,9,10 |
| `0x004462B0` | 162 | 0x43 | **non** | 0,1,2,3,4,5,6,7,9,10 |
| `0x00446900` | 110 | 0x41 | **non** | 0,1,2,3,4,5,6,9,10 |
| `0x00446F30` | 95 | 0x3d | **non** | 0,1,2,3,4,5,6,10 |
| `0x00476500` | 75 | 0x5b | **non** | 0,1,3,4,5,6,7,10 |
| `0x00446200` | 36 | 0x45 | **non** | 0,1,3,4,5,6,10 |
| `0x00446410` | 25 | 0x42 | **non** | 0,1,3,4,5,6,10 |
| `0x00476350` | 17 | 0x5a | **non** | 0,1,4,5,7,10 |
| `0x00445E70` | 17 | 0x48 | **non** | 0,3,4,6 |
| `0x00445CF0` | 15 | 0x4c | **non** | 0,1,3,5,6,10 |
| `0x00447630` | 14 | 0x3b | **non** | 0,1,3,4,5,10 |
| `0x0047A100` | 10 | 0x49 | oui | 0,4,6 |
| `0x00446170` | 10 | 0x4d | **non** | 0,1,4,10 |
| `0x00404920` | 10 | 0x55 | **non** | 0,6,10 |
| `0x00475CC0` | 8 | 0x4e | **non** | 0,1,10 |
| `0x00446A90` | 4 | 0x3f | **non** | 0,5,10 |
| `0x004477D0` | 2 | 0x4b | **non** | 0,2 |
| `0x00447170` | 1 | 0x3c | **non** | 1 |

---

## Le compte

| | |
|---|---|
| gestionnaires distincts atteignables par un objet | **21** |
| deja ecrits | **3** |
| a transcrire | **18** |
| naissances resolues par la table STATIQUE | **1440** |
| naissances resolues par une table de SOUS-ZONE | **0** |

**La derniere ligne est le resultat le plus utile du releve.** Aucune naissance
d'objet n'emploie un sous-type superieur ou egal a `0xc0` : tout passe par la table
statique `0x00551c30`, la meme dans les onze zones. **Un objet fait donc exactement la
meme chose ou qu'on soit** — ecrire ces corps une fois les couvre partout, et la
colonne `zones` ne dit que dans quels lieux l'objet se trouve, pas de quoi depend son
comportement. La branche par sous-zone reste dans le script parce que c'est la regle
du binaire ; elle ne se declenche simplement jamais ici.

---

## Les genres de la table des 48 effets (opcode 18)

| genre | emplois |
|---:|---:|
| 46 | 54 |
| 41 | 29 |
| 42 | 17 |
| 8 | 13 |
| 0 | 8 |
| 30 | 6 |
| 13 | 5 |
| 9 | 4 |
| 27 | 4 |
| 36 | 3 |
| 15 | 3 |
| 7 | 2 |
| 12 | 1 |

Les 48 sont transcrites ; ce tableau sert a savoir **lesquelles un objet peut reellement
exercer**, donc lesquelles valent un essai en jeu.

---

## Les fiches de sort que l'opcode 14 designe

Le sous-type reellement engendre est l'octet `+0x10` de la fiche, qui vit dans le
repertoire des sorts et non dans `objets.csv` : ces paires ne sont donc **pas resolues**.
C'est la seule lacune de ce releve.

| famille/indice | emplois |
|---|---:|
| 2/14 | 14 |
| 0/21 | 13 |
| 0/25 | 13 |
| 0/17 | 13 |
| 0/24 | 12 |
| 0/11 | 11 |
| 2/15 | 9 |
| 2/10 | 9 |
| 2/18 | 8 |
| 2/19 | 8 |
| 2/17 | 8 |
| 2/16 | 8 |
| 0/12 | 8 |
| 1/23 | 7 |
| 1/16 | 7 |
| 0/10 | 7 |
| 0/13 | 6 |
| 2/0 | 6 |
| 0/16 | 6 |
| 0/22 | 6 |
| 1/21 | 5 |
| 0/20 | 5 |
| 1/12 | 4 |
| 2/12 | 4 |
| 0/8 | 4 |
| 2/13 | 4 |
| 0/6 | 4 |
| 2/7 | 4 |
| 1/0 | 3 |
| 0/15 | 3 |
| 0/0 | 3 |
| 0/3 | 3 |
| 2/8 | 3 |
| 0/27 | 3 |
| 0/23 | 3 |
| 0/18 | 2 |
| 2/2 | 1 |
| 0/1 | 1 |
| 1/15 | 1 |
| 0/2 | 1 |
| 0/7 | 1 |
