# Les globales de `winsys3d`, et les lots qu'elles imposent

**Genere** par `sh OpenForesia.Tools/census-dll-globals.sh` — ne pas editer a la main.

Ce document repond a une question que `DLLAtteignables.md` ne sait pas poser : **quels
corps sont obliges de partir ensemble**. Deux relations les soudent — un appel vers une
fonction non exportee, et une globale qu'un corps ECRIT pendant qu'un autre la touche.
Le resultat est un decoupage en composantes, et **une composante se porte entiere ou pas
du tout**.

La classification lecture/ecriture est **volontairement pessimiste** : une adresse prise
(`&DAT_x`) hors acces indice compte comme une ecriture possible, parce que rien dans la
ligne ne dit ce qu'un alias en fera. Les composantes sont donc un MAJORANT — elles ne
manquent aucune soudure, elles peuvent en inventer. Detacher un corps d'une composante se
verifie a la main.

## Les composantes qui portent au moins un point d'entree

| # | corps | lignes | portes | points d'entree |
|---:|---:|---:|---:|---|
| 1 | 425 | 22 415 | 14 | 750, 668, 710, 793, 6, 9, 57, 68, 67/69, 132, 657, 674, 676, 665, 684, 701, 790, 789, 337, 360, 366, 367, 368, 369, 376, 378, 379, 418, 795, 792, 439, 442, 788 |

## Les globales qui soudent le plus

Colonne **touchee par** : le nombre de corps qui la nomment. Colonne **ecrite par** :
ceux qui, selon la regle ci-dessus, pourraient l'ecrire. Une globale a un seul ecrivain
et beaucoup de lecteurs est le bon endroit ou chercher une coupure : si l'ecrivain part
avec tous ses lecteurs, la soudure disparait.

| globale | touchee par | ecrite par |
|---|---:|---:|
| `DAT_00a2c488` | 85 | 54 |
| `DAT_00a2c480` | 84 | 83 |
| `DAT_011be760` | 73 | 1 |
| `DAT_00a2c484` | 53 | 22 |
| `DAT_011be770` | 47 | 3 |
| `DAT_011be774` | 47 | 3 |
| `DAT_011be7ac` | 46 | 45 |
| `PTR_DAT_00a2c4a0` | 46 | 2 |
| `PTR_DAT_00a2c4a4` | 46 | 2 |
| `DAT_00a2c578` | 38 | 38 |
| `DAT_00a2c57c` | 38 | 38 |
| `DAT_011be7a8` | 38 | 38 |
| `DAT_011be7dc` | 38 | 38 |
| `DAT_00a2c5a4` | 34 | 0 |
| `DAT_00a2c48c` | 33 | 3 |
| `DAT_011cb138` | 28 | 28 |
| `_DAT_011cb13c` | 28 | 28 |
| `_DAT_011cb148` | 28 | 28 |
| `_DAT_011cb158` | 28 | 28 |
| `_DAT_011cb15c` | 28 | 28 |
| `_DAT_011cb168` | 28 | 28 |
| `DAT_011cb1c8` | 27 | 1 |
| `DAT_011cb1cc` | 27 | 1 |
| `EXT_011d0870` | 27 | 13 |
| `_DAT_011cb178` | 26 | 26 |

142 globales sont touchees par trois corps ou plus.

