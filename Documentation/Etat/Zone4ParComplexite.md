# Zone 4 — La vieille tour, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-zone-complexity.sh 4 > Documentation/Etat/Zone4ParComplexite.md
```

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
`GestionnairesEntites.md` : le regenerer d'abord.

**176 gestionnaires, 156 implementes, 20 a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
| **D — gros** | pas d'IA, > 250 instr. | 4 | 4 | 1548 |
| **E — intelligence** | une routine de bete | 16 | 15 | 14235 |

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.

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
