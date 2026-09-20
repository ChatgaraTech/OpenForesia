# Zone 10 — La grotte scellee, par complexite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-zone-complexity.sh 10 > Documentation/Etat/Zone10ParComplexite.md
```

Le script mesure chaque gestionnaire **a ecrire** sur son desassemblage — nombre d'instructions,
tables de sauts, appelees que le C# ne connait pas encore, jumeaux — et les range en cinq
categories, dans l'ordre ou il vaut la peine de les prendre. L'etat implemente/non vient de
`GestionnairesEntites.md` : le regenerer d'abord.

**349 gestionnaires, 303 implementes, 46 a ecrire.**

## Le compte

« Lectures » compte les formes distinctes : des jumeaux a ecrire ne coutent qu'une lecture.

| categorie | ce que c'est | corps | lectures | instructions |
|---|---|---:|---:|---:|
| **C — moyen** | pas d'IA, <= 250 instr., ou une table de sauts, ou une inconnue | 21 | 21 | 3667 |
| **D — gros** | pas d'IA, > 250 instr. | 25 | 18 | 6299 |

La colonne « instructions » additionne une seule fois chaque forme ; la categorie A n'en compte
aucune, puisque son corps est deja ecrit.

**Les sous-zones se lisent par bloc.** Une zone installe ses tables par groupes de sous-zones ;
les gestionnaires d'un meme groupe vivent ensemble, et c'est souvent le bon decoupage pour un lot.

## C — moyen (21)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x00441670` | 13,14,15 | 0056B1C8 | 107 | 0 | oui |  |  |
| `0x00441C30` | 13,14,15 | 0056B1C8 | 108 | 0 | oui |  |  |
| `0x004412D0` | 13,14,15 | 0056B1C8 | 109 | 0 | non |  |  |
| `0x0043CBE0` | 11,12 | 0056B2C8 | 125 | 0 | non |  |  |
| `0x0043E690` | 11,12 | 0056B2C8 | 128 | 0 | oui |  |  |
| `0x00486720` | 13,14,15 | 0056B1C8 | 129 | 0 | non |  |  |
| `0x00485A40` | 13,14,15 | 0056B1C8 | 139 | 0 | non |  |  |
| `0x0043D850` | 11,12 | 0056B1C8 | 148 | 0 | oui |  |  |
| `0x0043D190` | 11,12 | 0056B2C8 | 159 | 0 | non |  |  |
| `0x00486B10` | 13,14,15 | 0056B0C8 | 174 | 0 | non |  |  |
| `0x00442870` | 13,14,15 | 0056B1C8 | 179 | 0 | non |  |  |
| `0x0043CEE0` | 11,12 | 0056B0C8 | 196 | 0 | non |  |  |
| `0x00485C00` | 13,14,15 | 0056B0C8 | 197 | 0 | oui |  |  |
| `0x0043C900` | 11,12 | 0056B0C8 | 204 | 0 | oui |  |  |
| `0x0043E820` | 13,14,15 | 0056B2C8 | 204 | 0 | non |  | appelee(s) inconnue(s) : `0x0043EB20` |
| `0x00441EB0` | 13,14,15 | 0056B0C8 | 207 | 0 | non |  |  |
| `0x0043DB30` | 11,12 | 0056B0C8 | 220 | 0 | oui |  |  |
| `0x00485F50` | 13,14,15 | 0056B0C8 | 222 | 0 | non |  |  |
| `0x004403B0` | 13,14,15 | 0056B2C8 | 229 | 0 | non |  |  |
| `0x004417F0` | 13,14,15 | 0056B0C8 | 237 | 0 | non |  |  |
| `0x0043D4D0` | 11,12 | 0056B0C8 | 246 | 0 | oui |  |  |

## D — gros (25)

| gestionnaire | sous-zones | table | instr. | sauts | Blaze.c | jumeaux a ecrire | note |
|---|---|---|---:|---:|---|---|---|
| `0x00440060` | 13,14,15 | 0056B2C8 | 256 | 0 | oui |  |  |
| `0x00468AC0` | 0 | 0056ADC8 | 261 | 0 | non | `0x0046B6E0`, `0x0046D860`, `0x0046FAD0`, `0x004702D0` |  |
| `0x0046B6E0` | 2,3 | 0056ADC8 | 261 | 0 | non | `0x00468AC0`, `0x0046D860`, `0x0046FAD0`, `0x004702D0` |  |
| `0x0046D860` | 5,8,10 | 0056ADC8 | 261 | 0 | non | `0x00468AC0`, `0x0046B6E0`, `0x0046FAD0`, `0x004702D0` |  |
| `0x0046FAD0` | 6,7 | 0056ADC8 | 261 | 0 | non | `0x00468AC0`, `0x0046B6E0`, `0x0046D860`, `0x004702D0` |  |
| `0x004702D0` | 1 | 0056ADC8 | 261 | 0 | non | `0x00468AC0`, `0x0046B6E0`, `0x0046D860`, `0x0046FAD0` |  |
| `0x00469BC0` | 0 | 0056ADC8 | 263 | 0 | non |  | appelee(s) inconnue(s) : `0x0046A5A0` `0x0046A630` |
| `0x004424B0` | 13,14,15 | 0056B0C8 | 272 | 0 | non |  |  |
| `0x00486320` | 13,14,15 | 0056B1C8 | 274 | 0 | non |  |  |
| `0x00469100` | 0 | 0056ADC8 | 284 | 0 | non |  |  |
| `0x004396E0` | 6,7 | 0056B1C8 | 289 | 0 | non |  | appelee(s) inconnue(s) : `0x00439AC0` |
| `0x0043C4A0` | 11,12 | 0056B1C8 | 289 | 0 | non |  |  |
| `0x00436070` | 4,9 | 0056B1C8 | 311 | 1 | non | `0x0043EC60` | appelee(s) inconnue(s) : `0x00436470` `0x00436610` `0x00436650` `0x00436690` `0x004366D0` `0x00436710` `0x00436750` |
| `0x0043EC60` | 13,14,15 | 0056B1C8 | 311 | 1 | non | `0x00436070` | appelee(s) inconnue(s) : `0x0043F0A0` `0x0043F0E0` `0x0043F120` `0x0043F160` `0x0043F1A0` `0x0043F1E0` `0x0043F220` `0x0043F260` `0x0043F2A0` `0x0043F2E0` |
| `0x00469F60` | 0 | 0056ADC8 | 342 | 0 | non |  | appelee(s) inconnue(s) : `0x0046A3F0` `0x0046A5A0` `0x0046A630` |
| `0x0043FB80` | 13,14,15 | 0056B0C8 | 359 | 0 | oui |  |  |
| `0x00469480` | 0 | 0056ADC8 | 363 | 0 | non |  |  |
| `0x0043B870` | 1 | 0056B2C8 | 369 | 0 | oui |  | appelee(s) inconnue(s) : `0x0043BD90` |
| `0x00470C20` | 1 | 0056ADC8 | 385 | 0 | non |  | appelee(s) inconnue(s) : `0x00471DD0` `0x004725A0` `0x00472630` |
| `0x00436950` | 4,9 | 0056B1C8 | 436 | 1 | non |  | appelee(s) inconnue(s) : `0x00436470` `0x00436690` `0x00436750` `0x00436F20` `0x00436F60` |
| `0x0046AD70` | 2,3 | 0056ADC8 | 459 | 0 | non | `0x0046D070`, `0x0046F2E0` |  |
| `0x0046D070` | 5,8,10 | 0056ADC8 | 459 | 0 | non | `0x0046AD70`, `0x0046F2E0` |  |
| `0x0046F2E0` | 6,7 | 0056ADC8 | 459 | 0 | non | `0x0046AD70`, `0x0046D070` |  |
| `0x00440840` | 13,14,15 | 0056B0C8 | 477 | 0 | non |  |  |
| `0x00471150` | 1 | 0056ADC8 | 610 | 0 | non |  | appelee(s) inconnue(s) : `0x00471A10` `0x00471DD0` `0x00471F80` `0x004725A0` `0x00472630` |
