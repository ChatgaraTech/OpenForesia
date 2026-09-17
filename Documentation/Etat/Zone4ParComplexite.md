# Zone 4 — La vieille tour, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-zone-complexity.sh 4 > Documentation/Etat/Zone4ParComplexite.md
```

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
`GestionnairesEntites.md` : le regenerer d'abord.

**176 gestionnaires, 97 implementes, 79 a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
| **B — simple** | pas d'IA, <= 100 instr., sans table de sauts ni inconnue | 4 | 4 | 139 |
| **C — moyen** | pas d'IA, <= 250 instr., ou une table de sauts, ou une inconnue | 55 | 46 | 6439 |
| **D — gros** | pas d'IA, > 250 instr. | 4 | 4 | 1548 |
| **E — intelligence** | une routine de bete | 16 | 15 | 14235 |

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.

## B — simple (4)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x00443C60` | 0,1,2,4 | 0056AEC8 | 22 | 0 | non |  |  |
| `0x004442F0` | 3,7,9 | 0056AEC8 | 25 | 0 | non |  |  |
| `0x00443CB0` | 0,1,2,4 | 0056AEC8 | 30 | 0 | oui |  |  |
| `0x00444200` | 3,7,9 | 0056AEC8 | 62 | 0 | oui |  |  |

## C — moyen (55)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x0045E040` | 5,6,8,10 | 0056ADC8 | 47 | 1 | non | `0x00460080` |  |
| `0x00460080` | 3,7,9 | 0056ADC8 | 47 | 1 | non | `0x0045E040` |  |
| `0x0045E100` | 5,6,8,10 | 0056ADC8 | 56 | 1 | non | `0x00460140` |  |
| `0x00460140` | 3,7,9 | 0056ADC8 | 56 | 1 | non | `0x0045E100` |  |
| `0x0045DF60` | 5,6,8,10 | 0056ADC8 | 59 | 1 | oui | `0x0045FFA0` |  |
| `0x0045FFA0` | 3,7,9 | 0056ADC8 | 59 | 1 | oui | `0x0045DF60` |  |
| `0x0045BF20` | 0,1,2,4 | 0056ADC8 | 74 | 0 | oui |  | appelee(s) inconnue(s) : `0x0045C000` |
| `0x0045AA90` | 0,1,2,4 | 0056ADC8 | 101 | 0 | oui |  |  |
| `0x00418AF0` | 5,6,8,10 | 0056B2C8 | 103 | 0 | oui |  |  |
| `0x004850C0` | 3,7,9 | 0056B2C8 | 105 | 0 | non |  |  |
| `0x0045A4D0` | 0,1,2,4 | 0056ADC8 | 106 | 0 | non |  |  |
| `0x0045FBA0` | 3,7,9 | 0056ADC8 | 106 | 0 | oui |  |  |
| `0x0041BB20` | 3,7,9 | 0056B0C8 | 107 | 0 | non |  |  |
| `0x00417CA0` | 0,1,2,4 | 0056B2C8 | 111 | 0 | non |  |  |
| `0x00417E10` | 0,1,2,4 | 0056B2C8 | 111 | 0 | non | `0x00419A20` |  |
| `0x004198B0` | 5,6,8,10 | 0056B2C8 | 111 | 0 | non |  |  |
| `0x00419A20` | 5,6,8,10 | 0056B2C8 | 111 | 0 | non | `0x00417E10` |  |
| `0x0041C0E0` | 3,7,9 | 0056B2C8 | 113 | 0 | non |  |  |
| `0x0045B3B0` | 0,1,2,4 | 0056ADC8 | 113 | 0 | oui |  |  |
| `0x0045B540` | 0,1,2,4 | 0056ADC8 | 113 | 0 | non |  |  |
| `0x00417B10` | 0,1,2,4 | 0056B2C8 | 115 | 0 | oui |  |  |
| `0x00419720` | 5,6,8,10 | 0056B2C8 | 115 | 0 | oui |  |  |
| `0x00418120` | 0,1,2,4 | 0056B2C8 | 116 | 0 | non |  |  |
| `0x0045AD20` | 0,1,2,4 | 0056ADC8 | 116 | 0 | oui |  |  |
| `0x00416920` | 0,1,2,4 | 0056B2C8 | 117 | 0 | oui |  |  |
| `0x00416C40` | 0,1,2,4 | 0056B2C8 | 119 | 0 | non |  |  |
| `0x0045AE90` | 0,1,2,4 | 0056ADC8 | 122 | 0 | oui |  |  |
| `0x0045E300` | 3,7,9 | 0056ADC8 | 123 | 0 | non |  |  |
| `0x00417F80` | 0,1,2,4 | 0056B2C8 | 131 | 0 | oui | `0x00419B90` |  |
| `0x00419B90` | 5,6,8,10 | 0056B2C8 | 131 | 0 | oui | `0x00417F80` |  |
| `0x00416A80` | 0,1,2,4 | 0056B0C8 | 136 | 0 | non |  |  |
| `0x0041BC90` | 3,7,9 | 0056B1C8 | 141 | 0 | non |  |  |
| `0x00417770` | 0,1,2,4 | 0056B2C8 | 142 | 0 | non |  |  |
| `0x00419380` | 5,6,8,10 | 0056B2C8 | 142 | 0 | non |  |  |
| `0x0045B7E0` | 0,1,2,4 | 0056ADC8 | 156 | 0 | oui |  |  |
| `0x00417420` | 0,1,2,4 | 0056B2C8 | 166 | 0 | non |  |  |
| `0x00419030` | 5,6,8,10 | 0056B2C8 | 166 | 0 | non |  |  |
| `0x0041B7C0` | 3,7,9 | 0056B2C8 | 167 | 0 | non |  |  |
| `0x0045CF70` | 5,6,8,10 | 0056ADC8 | 176 | 0 | non | `0x0045F3B0` |  |
| `0x0045F3B0` | 3,7,9 | 0056ADC8 | 176 | 0 | non | `0x0045CF70` |  |
| `0x0045DD00` | 5,6,8,10 | 0056ADC8 | 178 | 0 | non |  |  |
| `0x0045FD40` | 3,7,9 | 0056ADC8 | 178 | 0 | non |  |  |
| `0x0045B030` | 0,1,2,4 | 0056ADC8 | 186 | 0 | non |  |  |
| `0x0041AFA0` | 3,7,9 | 0056B1C8 | 188 | 0 | non |  |  |
| `0x0045BB10` | 0,1,2,4 | 0056ADC8 | 188 | 0 | oui |  |  |
| `0x0045E860` | 3,7,9 | 0056ADC8 | 188 | 0 | non |  |  |
| `0x0041AC80` | 3,7,9 | 0056B0C8 | 189 | 0 | non |  |  |
| `0x0045F7E0` | 3,7,9 | 0056ADC8 | 201 | 0 | oui |  |  |
| `0x0045D750` | 5,6,8,10 | 0056ADC8 | 227 | 0 | non |  |  |
| `0x0041B460` | 3,7,9 | 0056B0C8 | 232 | 0 | non |  |  |
| `0x0045A630` | 0,1,2,4 | 0056ADC8 | 239 | 0 | oui | `0x0045E4E0` |  |
| `0x0045E4E0` | 3,7,9 | 0056ADC8 | 239 | 0 | oui | `0x0045A630` |  |
| `0x00417110` | 0,1,2,4 | 0056B2C8 | 243 | 0 | oui | `0x00418C40`, `0x0041C250` |  |
| `0x00418C40` | 5,6,8,10 | 0056B2C8 | 243 | 0 | oui | `0x00417110`, `0x0041C250` |  |
| `0x0041C250` | 3,7,9 | 0056B2C8 | 243 | 0 | oui | `0x00417110`, `0x00418C40` |  |

## D — gros (4)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x0045C2E0` | 5,6,8,10 | 0056ADC8 | 267 | 0 | non |  | appelee(s) inconnue(s) : `0x00514020` |
| `0x00443D00` | 0,1,2,4 | 0056AEC8 | 363 | 0 | non |  | appelee(s) inconnue(s) : `0x00514020` |
| `0x0045ECC0` | 3,7,9 | 0056ADC8 | 458 | 0 | oui |  |  |
| `0x0045C870` | 5,6,8,10 | 0056ADC8 | 460 | 0 | oui |  |  |

## E — intelligence (16)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x004B7540` | 3,7,9 | 0056AD48 | 476 | 0 | non |  | **Wybarn** |
| `0x004B7E20` | 3,7,9 | 0056AD48 | 533 | 2 | non |  | **Enchanteresse** |
| `0x004B4570` | 3,7,9 | 0056AD48 | 621 | 2 | non |  | **Sylphide** |
| `0x004B6B90` | 3,7,9 | 0056AD48 | 747 | 1 | non | (+1 ailleurs) | **Ténébreux** |
| `0x004A8510` | 0,1,2,4 | 0056AD48 | 883 | 1 | non | (+1 ailleurs) | **Belle verte** |
| `0x004B1660` | 5,6,8,10 | 0056AD48 | 942 | 1 | non |  | **Sabre volant** |
| `0x004ADAE0` | 5,6,8,10 | 0056AD48 | 986 | 1 | non |  | **Rampant** |
| `0x004AE820` | 5,6,8,10 | 0056AD48 | 1000 | 1 | non |  | **Archange noir, Sorcier** |
| `0x004B4DB0` | 3,7,9 | 0056AD48 | 1000 | 1 | non |  | **Sorcier** |
| `0x004AAA80` | 0,1,2,4 | 0056AD48 | 1110 | 1 | non | (+1 ailleurs) | **Crapouille** |
| `0x004A9150` | 0,1,2,4 | 0056AD48 | 1115 | 1 | non | `0x004B3650` | **Garde golem** |
| `0x004B3650` | 3,7,9 | 0056AD48 | 1115 | 1 | non | `0x004A9150` | **Armure volante** |
| `0x004AB940` | 5,6,8,10 | 0056AD48 | 1143 | 1 | non |  | **Armure volante** |
| `0x004B0600` | 5,6,8,10 | 0056AD48 | 1211 | 1 | non |  | **Golem-sequelette, Gros dur** ; appelee(s) inconnue(s) : `0x004B1620` |
| `0x004AF560` | 5,6,8,10 | 0056AD48 | 1234 | 1 | non |  | **Chevalier noir** |
| `0x004B5AF0` | 3,7,9 | 0056AD48 | 1234 | 1 | non |  | **Platinum** |
