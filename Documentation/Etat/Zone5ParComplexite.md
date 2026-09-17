# Zone 5 — Le Labyrinthe des morts, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-zone-complexity.sh 5 > Documentation/Etat/Zone5ParComplexite.md
```

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
`GestionnairesEntites.md` : le regenerer d'abord.

**160 gestionnaires, 42 implementes, 118 a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
| **B — simple** | pas d'IA, <= 100 instr., sans table de sauts ni inconnue | 56 | 47 | 2880 |
| **C — moyen** | pas d'IA, <= 250 instr., ou une table de sauts, ou une inconnue | 35 | 33 | 4642 |
| **D — gros** | pas d'IA, > 250 instr. | 11 | 10 | 3782 |
| **E — intelligence** | une routine de bete | 16 | 15 | 13504 |

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.

## B — simple (56)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x004610E0` | 5,6,7,8 | 0056ADC8 | 6 | 0 | non |  |  |
| `0x00460240` | 0,1,2,3 | 0056ADC8 | 10 | 0 | non |  |  |
| `0x00460A90` | 0,1,2,3 | 0056ADC8 | 13 | 0 | non |  |  |
| `0x00444620` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | 18 | 0 | non |  |  |
| `0x004603C0` | 0,1,2,3 | 0056ADC8 | 25 | 0 | oui |  |  |
| `0x00460D10` | 0,1,2,3 | 0056ADC8 | 25 | 0 | non |  |  |
| `0x00444400` | 0,1,2,3 | 0056AEC8 | 30 | 0 | oui | `0x00444450` |  |
| `0x00444450` | 5,6,7,8 | 0056AEC8 | 30 | 0 | oui | `0x00444400` |  |
| `0x00461BE0` | 5,6,7,8 | 0056ADC8 | 33 | 0 | oui |  |  |
| `0x00461E00` | 5,6,7,8 | 0056ADC8 | 35 | 0 | oui |  |  |
| `0x00483C50` | 4,9 | 0056B0C8 | 36 | 0 | oui |  |  |
| `0x00483E90` | 4,9 | 0056B1C8 | 37 | 0 | non |  |  |
| `0x00444340` | 0,1,2,3 | 0056AEC8 | 40 | 0 | non |  |  |
| `0x00420860` | 5,6,7,8 | 0056B0C8 | 50 | 0 | oui |  |  |
| `0x00460260` | 0,1,2,3 | 0056ADC8 | 51 | 0 | oui |  |  |
| `0x0041D860` | 0,1,2,3 | 0056B0C8 | 53 | 0 | oui |  |  |
| `0x00425350` | 4,9 | 0056B1C8 | 54 | 0 | non |  |  |
| `0x00463300` | 4,9 | 0056ADC8 | 55 | 0 | oui |  |  |
| `0x004840E0` | 4,9 | 0056B1C8 | 56 | 0 | non |  |  |
| `0x00460310` | 0,1,2,3 | 0056ADC8 | 57 | 0 | non |  |  |
| `0x00461B00` | 5,6,7,8 | 0056ADC8 | 57 | 0 | oui |  |  |
| `0x00461E80` | 5,6,7,8 | 0056ADC8 | 57 | 0 | oui |  |  |
| `0x004618B0` | 5,6,7,8 | 0056ADC8 | 60 | 0 | non |  |  |
| `0x00460FD0` | 0,1,2,3 | 0056ADC8 | 62 | 0 | oui |  |  |
| `0x00425270` | 4,9 | 0056B1C8 | 65 | 0 | non |  |  |
| `0x00462250` | 5,6,7,8 | 0056ADC8 | 65 | 0 | oui |  |  |
| `0x00462990` | 4,9 | 0056ADC8 | 68 | 0 | non |  |  |
| `0x0041F520` | 5,6,7,8 | 0056B2C8 | 69 | 0 | non |  |  |
| `0x00425410` | 4,9 | 0056B2C8 | 71 | 0 | oui |  |  |
| `0x00460990` | 0,1,2,3 | 0056ADC8 | 72 | 0 | non |  |  |
| `0x0041E340` | 0,1,2,3 | 0056B2C8 | 73 | 0 | non | `0x00421330`, `0x00425500` |  |
| `0x00421330` | 5,6,7,8 | 0056B2C8 | 73 | 0 | non | `0x0041E340`, `0x00425500` |  |
| `0x00425500` | 4,9 | 0056B2C8 | 73 | 0 | non | `0x0041E340`, `0x00421330` |  |
| `0x00422C60` | 4,9 | 0056B2C8 | 74 | 0 | non | (+1 ailleurs) |  |
| `0x00422D50` | 4,9 | 0056B0C8 | 77 | 0 | non |  |  |
| `0x0041E610` | 0,1,2,3 | 0056B2C8 | 78 | 0 | non | `0x00421600` |  |
| `0x0041E700` | 0,1,2,3 | 0056B2C8 | 78 | 0 | non | `0x004216F0` |  |
| `0x00421600` | 5,6,7,8 | 0056B2C8 | 78 | 0 | non | `0x0041E610` |  |
| `0x004216F0` | 5,6,7,8 | 0056B2C8 | 78 | 0 | non | `0x0041E700` |  |
| `0x00460C00` | 0,1,2,3 | 0056ADC8 | 81 | 0 | oui |  |  |
| `0x0041D090` | 0,1,2,3 | 0056B2C8 | 83 | 0 | non |  |  |
| `0x00461260` | 5,6,7,8 | 0056ADC8 | 83 | 0 | non |  |  |
| `0x0041DF60` | 0,1,2,3 | 0056B2C8 | 84 | 0 | non | `0x00420F50` |  |
| `0x00420F50` | 5,6,7,8 | 0056B2C8 | 84 | 0 | non | `0x0041DF60` |  |
| `0x004249D0` | 4,9 | 0056B2C8 | 85 | 0 | non |  |  |
| `0x0041E9D0` | 0,1,2,3 | 0056B2C8 | 89 | 0 | non | `0x004219C0` |  |
| `0x004219C0` | 5,6,7,8 | 0056B2C8 | 89 | 0 | non | `0x0041E9D0` |  |
| `0x00462860` | 5,6,7,8 | 0056ADC8 | 91 | 0 | non | `0x00463640` |  |
| `0x00463640` | 4,9 | 0056ADC8 | 91 | 0 | non | `0x00462860` |  |
| `0x00461390` | 5,6,7,8 | 0056ADC8 | 93 | 0 | oui |  |  |
| `0x004606C0` | 0,1,2,3 | 0056ADC8 | 95 | 0 | oui |  |  |
| `0x00461980` | 5,6,7,8 | 0056ADC8 | 95 | 0 | oui |  |  |
| `0x0041DA80` | 0,1,2,3 | 0056B2C8 | 96 | 0 | oui | `0x00420A70` |  |
| `0x00420A70` | 5,6,7,8 | 0056B2C8 | 96 | 0 | oui | `0x0041DA80` |  |
| `0x00461100` | 5,6,7,8 | 0056ADC8 | 96 | 0 | oui |  |  |
| `0x00460AC0` | 0,1,2,3 | 0056ADC8 | 99 | 0 | non |  |  |

## C — moyen (35)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x004443D0` | 0,1,2,3 | 0056AEC8 | 7 | 0 | non |  | appelee(s) inconnue(s) : `0x00545A28` |
| `0x00424C70` | 4,9 | 0056B2C8 | 106 | 0 | oui |  |  |
| `0x004620D0` | 5,6,7,8 | 0056ADC8 | 109 | 0 | oui |  |  |
| `0x0041F600` | 5,6,7,8 | 0056B0C8 | 110 | 0 | oui |  |  |
| `0x0041D1A0` | 0,1,2,3 | 0056B0C8 | 112 | 0 | oui |  |  |
| `0x00424AE0` | 4,9 | 0056B2C8 | 114 | 0 | non |  |  |
| `0x004444A0` | 4,9 | 0056AEC8 | 114 | 0 | non |  |  |
| `0x0041DBD0` | 0,1,2,3 | 0056B2C8 | 116 | 0 | oui | `0x00420BC0` |  |
| `0x00420BC0` | 5,6,7,8 | 0056B2C8 | 116 | 0 | oui | `0x0041DBD0` |  |
| `0x00421C10` | 4,9 | 0056B0C8 | 116 | 0 | oui |  | appelee(s) inconnue(s) : `0x00421D80` |
| `0x0041EC20` | 5,6,7,8 | 0056B0C8 | 121 | 0 | non |  |  |
| `0x0041C740` | 0,1,2,3 | 0056B0C8 | 123 | 0 | non |  |  |
| `0x00460800` | 0,1,2,3 | 0056ADC8 | 124 | 0 | non |  |  |
| `0x00461F50` | 5,6,7,8 | 0056ADC8 | 124 | 0 | non |  |  |
| `0x00422330` | 4,9 | 0056B0C8 | 125 | 0 | non |  |  |
| `0x00424800` | 4,9 | 0056B1C8 | 125 | 0 | oui |  |  |
| `0x00461730` | 5,6,7,8 | 0056ADC8 | 129 | 0 | oui |  |  |
| `0x00461C60` | 5,6,7,8 | 0056ADC8 | 130 | 0 | non |  |  |
| `0x00463150` | 4,9 | 0056ADC8 | 133 | 0 | non |  |  |
| `0x00460D60` | 0,1,2,3 | 0056ADC8 | 135 | 0 | non |  |  |
| `0x004250B0` | 4,9 | 0056B2C8 | 136 | 0 | non |  |  |
| `0x00483F30` | 4,9 | 0056B0C8 | 139 | 0 | oui |  |  |
| `0x004633C0` | 4,9 | 0056ADC8 | 141 | 0 | non |  |  |
| `0x00483CC0` | 4,9 | 0056B1C8 | 149 | 0 | non |  |  |
| `0x0041E7F0` | 0,1,2,3 | 0056B2C8 | 154 | 0 | non |  |  |
| `0x004217E0` | 5,6,7,8 | 0056B2C8 | 154 | 0 | non |  |  |
| `0x0041DD40` | 0,1,2,3 | 0056B2C8 | 163 | 0 | oui | `0x00420D30` |  |
| `0x00420D30` | 5,6,7,8 | 0056B2C8 | 163 | 0 | oui | `0x0041DD40` |  |
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

## E — intelligence (16)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x004C4F90` | 4,9 | 0056AD48 | 709 | 2 | non |  | **Durahan, Durahan-noir** |
| `0x004BA110` | 0,1,2,3 | 0056AD48 | 766 | 1 | non |  | **Dunoeud** ; appelee(s) inconnue(s) : `0x004B9220` `0x004BAAE0` `0x004BAB20` |
| `0x004C3840` | 4,9 | 0056AD48 | 766 | 1 | non |  | **Dunoeud** ; appelee(s) inconnue(s) : `0x004C3450` |
| `0x004C7240` | 4,9 | 0056AD48 | 790 | 4 | non |  | **Roi-zombie** |
| `0x004B95E0` | 0,1,2,3 | 0056AD48 | 836 | 1 | non | (+1 ailleurs) | **Zombie** ; appelee(s) inconnue(s) : `0x004B9220` |
| `0x004C1C30` | 5,6,7,8 | 0056AD48 | 853 | 1 | non |  | **Revenant** |
| `0x004C6700` | 4,9 | 0056AD48 | 853 | 1 | non |  | **Revenant** ; appelee(s) inconnue(s) : `0x004C3450` |
| `0x004B8540` | 0,1,2,3 | 0056AD48 | 941 | 1 | non | `0x004C2770` | **Sanguinaire, Tas d'os** ; appelee(s) inconnue(s) : `0x004B9220` |
| `0x004C2770` | 4,9 | 0056AD48 | 941 | 1 | non | `0x004B8540` | **Sanguinaire** ; appelee(s) inconnue(s) : `0x004C3450` |
| `0x004BAB60` | 0,1,2,3 | 0056AD48 | 966 | 1 | non |  | **Persil** ; appelee(s) inconnue(s) : `0x004B9220` |
| `0x004C4290` | 4,9 | 0056AD48 | 966 | 1 | non |  | **Persil** ; appelee(s) inconnue(s) : `0x004C3450` |
| `0x004BB860` | 0,1,2,3 | 0056AD48 | 970 | 1 | non |  | **Mort-vivant** ; appelee(s) inconnue(s) : `0x004B9220` |
| `0x004BD170` | 5,6,7,8 | 0056AD48 | 970 | 1 | non |  | **Mort-vivant** |
| `0x004BFE90` | 5,6,7,8 | 0056AD48 | 970 | 1 | non | (+2 ailleurs) | **Ombre** |
| `0x004C5A30` | 4,9 | 0056AD48 | 970 | 1 | non |  | **Ombre rouge** ; appelee(s) inconnue(s) : `0x004C3450` |
| `0x004C0B60` | 5,6,7,8 | 0056AD48 | 1178 | 1 | non |  | **Momie, Reine momie, Sanglante, Vielle momie** ; appelee(s) inconnue(s) : `0x004C1B70` `0x004C1BB0` `0x004C1BF0` |
