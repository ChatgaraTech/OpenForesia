# Zone 6 — Le chateau de l'immortel, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-zone-complexity.sh 6 > Documentation/Etat/Zone6ParComplexite.md
```

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
`GestionnairesEntites.md` : le regenerer d'abord.

**170 gestionnaires, 161 implementes, 9 a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
| **E — intelligence** | une routine de bete | 9 | 9 | 9717 |

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.

## E — intelligence (9)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x004DACC0` | 5,11 | 0056AD48 | 682 | 2 | non |  | **Nocturio** |
| `0x004C9E00` | 0,1,2,3,4 | 0056AD48 | 861 | 1 | non |  | **Fantôme** |
| `0x004D80D0` | 5,11 | 0056AD48 | 1075 | 1 | non |  | **Tigre** |
| `0x004D7200` | 5,11 | 0056AD48 | 1106 | 1 | non |  | **Loup-garou** |
| `0x004D2FC0` | 6,7,8,9,10 | 0056AD48 | 1122 | 1 | non |  | **Chimère** |
| `0x004D9D90` | 5,11 | 0056AD48 | 1126 | 1 | non | (+1 ailleurs) | **Sacuba** |
| `0x004D3EE0` | 6,7,8,9,10 | 0056AD48 | 1129 | 1 | non |  | **Sacuba** ; appelee(s) inconnue(s) : `0x004D4DD0` |
| `0x004CB4F0` | 0,1,2,3,4 | 0056AD48 | 1308 | 1 | non | (+1 ailleurs) | **Harpie** |
| `0x004D60A0` | 5,11 | 0056AD48 | 1308 | 1 | non |  | **Harpie** |
