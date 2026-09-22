# Le chemin d'affichage de `winsys3d` — le dernier lot

**Genere** par `sh OpenForesia.Tools/census-display-path.sh` — ne pas editer a la main.

Tout ce qui se detachait de `winsys3d` est porte ; il ne reste que la masse, et dans la
masse il ne reste qu'un lot : le chemin d'affichage. Ce document le delimite, le chiffre,
et en donne l'ordre de travail.

## Le perimetre, et pourquoi il est indivisible

La graine est **tout corps qui touche le tampon de commandes ou un memo d'etat** ; le
perimetre est sa fermeture descendante. Ce n'est pas un choix de decoupage, c'est une
contrainte : a etat inchange, un producteur **allonge l'enregistrement precedent** —
`paquet[curseur - 1] += n` — et range ses sommets a la suite dans la zone partagee. Deux
moities ne peuvent pas coexister ; l'une allongerait un enregistrement ecrit par l'autre.

| | corps | lignes |
|---|---:|---:|
| graine (tampon ou memo) | 94 | |
| **perimetre ferme** | **157** | **12 408** |
| deja transcrits | 4 | |

**26 corps restent dehors tout en appelant le perimetre** — ce sont les coutures, et elles sont toutes minuscules :

| corps | adresse | lignes | ordinal |
|---|---|---:|---|
| `FUN_00a1d260` | `0x00a1d260` | 30 | — |
| `FUN_00a1d040` | `0x00a1d040` | 13 | — |
| `FUN_00a27770` | `0x00a27770` | 33 | — |
| `FUN_00a1d600` | `0x00a1d600` | 33 | — |
| `FUN_00a1d330` | `0x00a1d330` | 34 | — |
| `FUN_00a062d0` | `0x00a062d0` | 13 | — |
| `FUN_00a27560` | `0x00a27560` | 27 | — |
| `FUN_00a1d570` | `0x00a1d570` | 30 | — |
| `FUN_00a062e0` | `0x00a062e0` | 14 | — |
| `WinDrawText` | `0x00a01f90` | 78 | — |
| `FUN_00a1d860` | `0x00a1d860` | 22 | — |
| `FUN_00a1d1f0` | `0x00a1d1f0` | 36 | — |
| `FUN_00a03110` | `0x00a03110` | 123 | — |
| `FUN_00a1d2c0` | `0x00a1d2c0` | 34 | — |
| `FUN_00a02640` | `0x00a02640` | 48 | — |
| `FUN_00a1d090` | `0x00a1d090` | 15 | — |
| `FUN_00a27650` | `0x00a27650` | 49 | — |
| `FUN_00a27700` | `0x00a27700` | 18 | — |
| `FUN_00a1d150` | `0x00a1d150` | 34 | — |
| `FUN_00a277f0` | `0x00a277f0` | 18 | — |
| `FUN_00a1d730` | `0x00a1d730` | 35 | — |
| `FUN_00a1d3a0` | `0x00a1d3a0` | 85 | — |
| `FUN_00a1d0c0` | `0x00a1d0c0` | 36 | — |
| `FUN_00a27730` | `0x00a27730` | 30 | — |
| `FUN_00a25d30` | `0x00a25d30` | 30 | — |
| `FUN_00a1d6a0` | `0x00a1d6a0` | 30 | — |

## Les points d'entree importes qui tombent dedans

Ce sont les `[DllImport]` que ce lot retirerait. La facade ne change qu'a la fin :
le lot se porte entier ou pas du tout.

| ordinal | corps | lignes |
|---|---|---:|
| **701** | `FUN_00a061b0` | 59 |

**1 points d'entree.**

## L'ordre de travail — par forme, pas par taille

Chaque corps est reduit a son **squelette de controle** : `I` un `if`, `E` un `else`,
`F`/`W`/`D` les boucles, `S` un `switch`, `R` un `return`, `C` un appel interne, `V` un
appel de vtable du device. Deux corps au meme squelette se transcrivent de la meme facon.

Les familles sont triees par population : **commencer par les plus peuplees**, une forme
comprise y vaut pour dix a quinze transcriptions.

| # | corps | lignes | squelette | exemple |
|---:|---:|---:|---|---|
| 1 | 16 | 844 | `CIDCWR` | `FUN_00a0d260` |
| 2 | 9 | 467 | `CIVIVCIIIVR` | `FUN_00a111c0` |
| 3 | 7 | 399 | `CIICRCIIVVR` | `FUN_00a13220` |
| 4 | 7 | 350 | `CICECIVVR` | `FUN_00a11000` |
| 5 | 6 | 579 | `CIIIR` | `FUN_00a094d0` |
| 6 | 6 | 558 | `CIIIIR` | `FUN_00a0a1a0` |
| 7 | 5 | 961 | `CICIIIIDIIIIIIEWICR` | `FUN_00a1a190` |
| 8 | 5 | 937 | `CICIIIIDIIIIIWICR` | `FUN_00a15f10` |
| 9 | 5 | 361 | `CIICRCIIVIIIVR` | `FUN_00a128d0` |
| 10 | 5 | 323 | `CICECIVIIIVR` | `FUN_00a0f090` |
| 11 | 5 | 92 | `CR` | `FUN_00a25c40` |
| 12 | 4 | 908 | `CICIIIEIIIDIIIIIEIIWICR` | `FUN_00a18c70` |
| 13 | 4 | 402 | `CICIIIIDWICR` | `FUN_00a1b3f0` |
| 14 | 3 | 304 | `CIIIEIICIR` | `FUN_00a0c110` |
| 15 | 3 | 53 | `CIR` | `FUN_00a0ddb0` |
| 16 | 3 | 53 | `CICR` | `FUN_00a024a0` |
| 17 | 3 | 41 | `CCR` | `FUN_00a26a70` |
| 18 | 2 | 258 | `CIIIEIIR` | `FUN_00a08080` |
| 19 | 2 | 176 | `CIVIVCIIIIIIIIIIIIVR` | `FUN_00a0ecf0` |
| 20 | 2 | 148 | `CIVIVCIIIIIIIIIVR` | `FUN_00a0f940` |
| 21 | 2 | 114 | `CIIIIIICR` | `FUN_00a268c0` |
| 22 | 2 | 90 | `CIIIICR` | `FUN_00a26720` |
| 23 | 2 | 57 | `CIIR` | `FUN_00a07ca0` |
| 24 | 2 | 52 | `CIDICWCR` | `FUN_00a027a0` |
| 25 | 2 | 35 | `CICCR` | `FUN_00a0e000` |
| 26 | 1 | 540 | `CDISCCCCCCCCCCCCCCCCCCCCCCCCCCCISCCCC...` | `FUN_00a13400` |
| 27 | 1 | 316 | `CIEIIIFDWIEIFFFIEIDIDWWIEDIDWWIEIIDDW...` | `FUN_00a03460` |
| 28 | 1 | 259 | `CICCFFIDSWDIDISICICICICICICICICICICIC...` | `FUN_00a0c4f0` |
| 29 | 1 | 235 | `CIIIIIWWDWIICECIVIIIEIEEIEIIIEIEEIEVIIIR` | `FUN_00a142a0` |
| 30 | 1 | 217 | `CIRDSCCCCCCCICIRIRW` | `FUN_00a26b20` |
| 31 | 1 | 150 | `CIEIEIDWIDIDWWIEDWFIDWIEFIDWVIIIR` | `FUN_00a03980` |
| 32 | 1 | 129 | `CIRIIICECEIICICIRIIEEIIIEEIIIEEIIEIIR` | `FUN_00a271e0` |
| 33 | 1 | 128 | `CIIIEIIIR` | `FUN_00a09980` |
| 34 | 1 | 127 | `CIICECIVIECCIEIEVR` | `FUN_00a0e0c0` |
| 35 | 1 | 115 | `CIICRCIIVIIIIIIIIIIIIVR` | `FUN_00a12350` |
| 36 | 1 | 111 | `CDIDICCICIWIFDDIWWIECIDIIECWIRW` | `FUN_00a26f20` |
| 37 | 1 | 100 | `CICECIVIIIIIIIIIIIIVR` | `FUN_00a101c0` |
| 38 | 1 | 96 | `CIICRCIIVIIIIIIIIIVR` | `FUN_00a11ec0` |
| 39 | 1 | 88 | `CIRICICIDIWICCIIIEIIIR` | `FUN_00a27e00` |
| 40 | 1 | 83 | `CICECIVIIIIIIIIIVR` | `FUN_00a0fb40` |
| 41 | 1 | 82 | `CIEIDIIEEIICWR` | `FUN_00a024e0` |
| 42 | 1 | 81 | `CICSCRCRCRCRCRCRCRCRCRCRCRCRCRCRCRCR` | `FUN_00a0dde0` |
| 43 | 1 | 81 | `CIDIIDCWWR` | `FUN_00a02380` |
| 44 | 1 | 72 | `CIISSIIIR` | `FUN_00a26e10` |
| 45 | 1 | 60 | `CCICCRIRIIRVEVSVVRVVR` | `FUN_00a03c80` |
| 46 | 1 | 59 | `CIICDICWCCIVEVVDICWCVCIR` | `FUN_00a061b0` |
| 47 | 1 | 59 | `CIVIVCIIIIIIVR` | `FUN_00a0e970` |
| 48 | 1 | 51 | `CICIRIIER` | `FUN_00a264f0` |
| 49 | 1 | 51 | `CIRICIRCIEICECVIVRR` | `FUN_00a27430` |
| 50 | 1 | 46 | `CIDSVCVVWR` | `FUN_00a0cd20` |
| 51 | 1 | 42 | `CDIDWWWR` | `FUN_00a01f00` |
| 52 | 1 | 39 | `CIEIRCR` | `FUN_00a06a40` |
| 53 | 1 | 35 | `CIDCWFFR` | `FUN_00a26490` |
| 54 | 1 | 30 | `CIRDIRWR` | `FUN_00a278c0` |
| 55 | 1 | 30 | `CCFR` | `FUN_00a14980` |
| 56 | 1 | 30 | `CIECR` | `FUN_00a269b0` |
| 57 | 1 | 29 | `CCIECCICR` | `FUN_00a275e0` |
| 58 | 1 | 29 | `CIIDWR` | `FUN_00a1cff0` |
| 59 | 1 | 27 | `CIIVVVVIEVVVRR` | `FUN_00a03db0` |
| 60 | 1 | 24 | `CICCCVCVIR` | `FUN_00a06300` |
| 61 | 1 | 24 | `CIVICVR` | `FUN_00a02820` |
| 62 | 1 | 24 | `CIRIR` | `FUN_00a27920` |
| 63 | 1 | 24 | `CFWICR` | `FUN_00a27fb0` |
| 64 | 1 | 22 | `CIFR` | `FUN_00a28010` |
| 65 | 1 | 22 | `CIICR` | `FUN_00a275a0` |
| 66 | 1 | 18 | `CIRCR` | `FUN_00a27880` |
| 67 | 1 | 17 | `CCIRR` | `FUN_00a03430` |
| 68 | 1 | 15 | `CCCR` | `FUN_00a27850` |
| 69 | 1 | 15 | `CCICR` | `FUN_00a27820` |
| 70 | 1 | 14 | `CVR` | `FUN_00a27120` |

**70 formes distinctes pour 157 corps.**

## Les globales que le lot emporte

Celles du noyau d'abord — elles sont la raison d'etre du perimetre —, puis les plus
partagees. Une globale citee ici ne doit **jamais** exister en double : tant que la
moitie native la relit, notre copie serait morte.

| globale | role | corps qui la nomment |
|---|---|---:|
| `DAT_011be7ac` | le paquet courant — rempli, rejoue, libere | 46 |
| `DAT_011be7dc` | le curseur d'ecriture, en mots | 38 |
| `DAT_011be7a8` | la zone de sommets, dans le meme bloc | 38 |
| `DAT_00a2c480` | l'etat de rendu courant | 84 |
| `DAT_00a2c484` | memo d'etat | 53 |
| `DAT_00a2c488` | le type de primitive courant | 85 |
| `DAT_00a2c48c` | memo d'etat | 33 |
| `DAT_00a2c578` | la derniere commande d'etat ecrite | 38 |
| `DAT_00a2c57c` | son argument | 38 |
| `DAT_011be760` | le device — **une poignee, pas un etat** ; relue par l'ordinal 750 | 73 |

Dans le perimetre : **46 corps touchent le tampon**, **87 un memo**, **55 le device**.
