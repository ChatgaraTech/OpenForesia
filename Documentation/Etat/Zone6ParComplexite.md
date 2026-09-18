# Zone 6 — Le chateau de l'immortel, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-zone-complexity.sh 6 > Documentation/Etat/Zone6ParComplexite.md
```

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
`GestionnairesEntites.md` : le regenerer d'abord.

**170 gestionnaires, 148 implementes, 22 a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
| **D — gros** | pas d'IA, > 250 instr. | 9 | 8 | 3131 |
| **E — intelligence** | une routine de bete | 13 | 12 | 12624 |

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.

## D — gros (9)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x00465820` | 6,7,8,9,10 | 0056ADC8 | 269 | 0 | oui |  |  |
| `0x00463EF0` | 0,1,2,3,4 | 0056ADC8 | 270 | 0 | oui |  |  |
| `0x0042ABF0` | 5,11 | 0056B1C8 | 362 | 1 | non |  | appelee(s) inconnue(s) : `0x0042A110` `0x0042A150` `0x0042A190` `0x0042A1D0` `0x0042A210` `0x0042A290` `0x0042A2D0` |
| `0x00425E50` | 0,1,2,3,4 | 0056B2C8 | 372 | 0 | oui |  | appelee(s) inconnue(s) : `0x00426370` |
| `0x00428420` | 6,7,8,9,10 | 0056B2C8 | 372 | 0 | oui | `0x0042B9A0` | appelee(s) inconnue(s) : `0x00428950` |
| `0x0042B9A0` | 5,11 | 0056B2C8 | 372 | 0 | oui | `0x00428420` | appelee(s) inconnue(s) : `0x0042BED0` |
| `0x004274C0` | 6,7,8,9,10 | 0056B1C8 | 373 | 1 | non |  | appelee(s) inconnue(s) : `0x004279D0` `0x00427A10` `0x00427A50` `0x00427A90` `0x00427AD0` `0x00427B10` `0x00427B50` `0x00427B90` `0x00427BD0` |
| `0x00425750` | 0,1,2,3,4 | 0056B2C8 | 548 | 0 | oui |  |  |
| `0x00427CE0` | 6,7,8,9,10 | 0056B2C8 | 565 | 0 | oui |  |  |

## E — intelligence (13)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x004DACC0` | 5,11 | 0056AD48 | 682 | 2 | non |  | **Nocturio** |
| `0x004C9E00` | 0,1,2,3,4 | 0056AD48 | 861 | 1 | non |  | **Fantôme** |
| `0x004CC650` | 0,1,2,3,4 | 0056AD48 | 934 | 1 | non |  | **Loup, Loup gris** |
| `0x004D4E10` | 5,11 | 0056AD48 | 934 | 1 | non | (+2 ailleurs) | **Loup gris** ; appelee(s) inconnue(s) : `0x004D5A20` |
| `0x004CF3A0` | 6,7,8,9,10 | 0056AD48 | 1039 | 1 | non | `0x004D8F70` | **Petit vampire, Vampire** |
| `0x004D8F70` | 5,11 | 0056AD48 | 1039 | 1 | non | `0x004CF3A0` | appelee(s) inconnue(s) : `0x004D5A20` |
| `0x004D80D0` | 5,11 | 0056AD48 | 1075 | 1 | non |  | **Tigre** ; appelee(s) inconnue(s) : `0x004D5A20` |
| `0x004D7200` | 5,11 | 0056AD48 | 1106 | 1 | non |  | **Loup-garou** ; appelee(s) inconnue(s) : `0x004D5A20` |
| `0x004D2FC0` | 6,7,8,9,10 | 0056AD48 | 1122 | 1 | non |  | **Chimère** |
| `0x004D9D90` | 5,11 | 0056AD48 | 1126 | 1 | non | (+1 ailleurs) | **Sacuba** ; appelee(s) inconnue(s) : `0x004D5A20` |
| `0x004D3EE0` | 6,7,8,9,10 | 0056AD48 | 1129 | 1 | non |  | **Sacuba** ; appelee(s) inconnue(s) : `0x004D4DD0` |
| `0x004CB4F0` | 0,1,2,3,4 | 0056AD48 | 1308 | 1 | non | (+1 ailleurs) | **Harpie** |
| `0x004D60A0` | 5,11 | 0056AD48 | 1308 | 1 | non |  | **Harpie** ; appelee(s) inconnue(s) : `0x004D5A20` |
