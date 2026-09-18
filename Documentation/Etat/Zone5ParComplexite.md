# Zone 5 — Le Labyrinthe des morts, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-zone-complexity.sh 5 > Documentation/Etat/Zone5ParComplexite.md
```

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
`GestionnairesEntites.md` : le regenerer d'abord.

**160 gestionnaires, 152 implementes, 8 a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
| **D — gros** | pas d'IA, > 250 instr. | 8 | 7 | 2617 |

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.

## D — gros (8)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x00484190` | 4,9 | 0056B1C8 | 252 | 0 | non |  |  |
| `0x00420480` | 5,6,7,8 | 0056B1C8 | 261 | 0 | non |  | appelee(s) inconnue(s) : `0x004207E0` `0x00420820` |
| `0x00422E50` | 4,9 | 0056B1C8 | 266 | 0 | non |  | appelee(s) inconnue(s) : `0x004231C0` `0x00423200` |
| `0x0041F760` | 5,6,7,8 | 0056B2C8 | 430 | 0 | oui | `0x0041FF40` |  |
| `0x0041FF40` | 5,6,7,8 | 0056B2C8 | 430 | 0 | oui | `0x0041F760` |  |
| `0x0041D310` | 0,1,2,3 | 0056B2C8 | 431 | 0 | oui |  |  |
| `0x00421DC0` | 4,9 | 0056B2C8 | 441 | 0 | oui |  |  |
| `0x00462A80` | 4,9 | 0056ADC8 | 536 | 0 | oui |  |  |
