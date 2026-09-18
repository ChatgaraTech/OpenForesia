# Zone 5 — Le Labyrinthe des morts, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-zone-complexity.sh 5 > Documentation/Etat/Zone5ParComplexite.md
```

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
`GestionnairesEntites.md` : le regenerer d'abord.

**160 gestionnaires, 124 implementes, 36 a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
| **C — moyen** | pas d'IA, <= 250 instr., ou une table de sauts, ou une inconnue | 25 | 25 | 3573 |
| **D — gros** | pas d'IA, > 250 instr. | 11 | 10 | 3782 |

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.

## C — moyen (25)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x004443D0` | 0,1,2,3 | 0056AEC8 | 7 | 0 | non |  | appelee(s) inconnue(s) : `0x00545A28` |
| `0x00424C70` | 4,9 | 0056B2C8 | 106 | 0 | oui |  |  |
| `0x0041F600` | 5,6,7,8 | 0056B0C8 | 110 | 0 | oui |  |  |
| `0x0041D1A0` | 0,1,2,3 | 0056B0C8 | 112 | 0 | oui |  |  |
| `0x00424AE0` | 4,9 | 0056B2C8 | 114 | 0 | non |  |  |
| `0x004444A0` | 4,9 | 0056AEC8 | 114 | 0 | non |  |  |
| `0x00421C10` | 4,9 | 0056B0C8 | 116 | 0 | oui |  | appelee(s) inconnue(s) : `0x00421D80` |
| `0x0041EC20` | 5,6,7,8 | 0056B0C8 | 121 | 0 | non |  |  |
| `0x0041C740` | 0,1,2,3 | 0056B0C8 | 123 | 0 | non |  |  |
| `0x00424800` | 4,9 | 0056B1C8 | 125 | 0 | oui |  |  |
| `0x00461730` | 5,6,7,8 | 0056ADC8 | 129 | 0 | oui |  |  |
| `0x00461C60` | 5,6,7,8 | 0056ADC8 | 130 | 0 | non |  |  |
| `0x00463150` | 4,9 | 0056ADC8 | 133 | 0 | non |  |  |
| `0x00460D60` | 0,1,2,3 | 0056ADC8 | 135 | 0 | non |  |  |
| `0x004250B0` | 4,9 | 0056B2C8 | 136 | 0 | non |  |  |
| `0x00483F30` | 4,9 | 0056B0C8 | 139 | 0 | oui |  |  |
| `0x004633C0` | 4,9 | 0056ADC8 | 141 | 0 | non |  |  |
| `0x00483CC0` | 4,9 | 0056B1C8 | 149 | 0 | non |  |  |
| `0x0041FCA0` | 5,6,7,8 | 0056B0C8 | 185 | 0 | oui |  | appelee(s) inconnue(s) : `0x0041FF00` |
| `0x00462330` | 5,6,7,8 | 0056ADC8 | 193 | 0 | non |  |  |
| `0x00460430` | 0,1,2,3 | 0056ADC8 | 202 | 0 | non |  |  |
| `0x004614B0` | 5,6,7,8 | 0056ADC8 | 204 | 0 | non |  |  |
| `0x004625B0` | 5,6,7,8 | 0056ADC8 | 208 | 0 | non |  |  |
| `0x00424DC0` | 4,9 | 0056B0C8 | 217 | 0 | oui |  |  |
| `0x004244D0` | 4,9 | 0056B0C8 | 224 | 0 | oui |  |  |

## D — gros (11)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x00484190` | 4,9 | 0056B1C8 | 252 | 0 | non |  |  |
| `0x00420480` | 5,6,7,8 | 0056B1C8 | 261 | 0 | non |  | appelee(s) inconnue(s) : `0x004207E0` `0x00420820` |
| `0x00422E50` | 4,9 | 0056B1C8 | 266 | 0 | non |  | appelee(s) inconnue(s) : `0x004231C0` `0x00423200` |
| `0x0041EDD0` | 5,6,7,8 | 0056B1C8 | 380 | 1 | non |  | appelee(s) inconnue(s) : `0x0041F2E0` `0x0041F320` `0x0041F360` `0x0041F3A0` `0x0041F3E0` `0x0041F420` `0x0041F460` `0x0041F4A0` `0x0041F4E0` |
| `0x004224F0` | 4,9 | 0056B1C8 | 389 | 1 | non |  | appelee(s) inconnue(s) : `0x00422A20` `0x00422A60` `0x00422AA0` `0x00422AE0` `0x00422B20` `0x00422B60` `0x00422BA0` `0x00422BE0` `0x00422C20` |
| `0x0041C900` | 0,1,2,3 | 0056B1C8 | 396 | 1 | non |  | appelee(s) inconnue(s) : `0x0041CE50` `0x0041CE90` `0x0041CED0` `0x0041CF10` `0x0041CF50` `0x0041CF90` `0x0041CFD0` `0x0041D010` `0x0041D050` |
| `0x0041F760` | 5,6,7,8 | 0056B2C8 | 430 | 0 | oui | `0x0041FF40` |  |
| `0x0041FF40` | 5,6,7,8 | 0056B2C8 | 430 | 0 | oui | `0x0041F760` |  |
| `0x0041D310` | 0,1,2,3 | 0056B2C8 | 431 | 0 | oui |  |  |
| `0x00421DC0` | 4,9 | 0056B2C8 | 441 | 0 | oui |  |  |
| `0x00462A80` | 4,9 | 0056ADC8 | 536 | 0 | oui |  |  |
