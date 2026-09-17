# Feuille de route des gestionnaires d'entite

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-entity-handlers.sh > Documentation/Etat/GestionnairesEntites.md
```

Le script ne recopie aucune valeur : il lit les tables dans `Blaze.exe`, les `case` du
repartiteur dans les sources, et le releve de la sonde dans `handlers-exerces.tsv`.

---

## Comment lire

**Zone et sous-zone ne sont pas la meme chose.** Une **zone** est un *lieu* — la foret,
la mine — et il y en a onze. Une **sous-zone** est une *carte* dans ce lieu, seize au
plus par zone : c'est ce que l'ecran montre. Les sept tables de sauts sont **rechargees
a chaque sous-zone**, donc un numero de sous-type ne veut rien dire tout seul.

**Les deux colonnes sont independantes.**

| colonne | ce qu'elle dit | d'ou elle vient |
|---|---|---|
| **implemente** | le corps existe et est branche | un `case` du repartiteur |
| **teste** | le corps a reellement tourne en jeu | la sonde, dans cette zone |

`implemente=oui, teste=non` est une **dette** : un corps transcrit d'apres le
desassembleur et qui compile n'a rien prouve. Rien ne signale une lecture d'offset de
travers tant que le code n'a pas tourne.

**Les quatre combinaisons se lisent comme un ordre de travail.**

| implemente | teste | ce que ca veut dire |
|---|---|---|
| non | **oui** | **la priorite** : le jeu y va vraiment, et il plante en y allant |
| non | non | a ecrire un jour ; rien ne prouve encore qu'on y passe |
| oui | non | une **dette** : le corps existe et n'a jamais tourne |
| oui | oui | acquis |

La ligne `non / oui` n'est possible que parce que la sonde note l'adresse **avant**
d'appeler : un gestionnaire absent est donc enregistre par le plantage meme qu'il cause.

---

## Le compte

| | |
|---|---|
| gestionnaires distincts atteignables | **1430** |
| testes en jeu | **327** |
| implementes, jamais appeles | **298** |
| a transcrire | **805** |

La souche a corps vide `0x00401FA0` est exclue : elle occupe la majorite des creneaux
et n'est pas un comportement.

---

## L'avancement par zone

| zone | lieu | gestionnaires | implementes | testes |
|---|---|---|---|---|
| — | **tables statiques** (partout) | 274 | 274 | 219 |
| 0 | L'auberge | 48 | 48 | 16 |
| 1 | La mine abandonnee | 157 | 157 | 0 |
| 2 | La montagne du dragon de feu | 42 | 40 | 0 |
| 3 | La foret | 136 | 136 | 90 |
| 4 | La vieille tour | 176 | 28 | 3 |
| 5 | Le Labyrinthe des morts | 160 | 23 | 0 |
| 6 | Le chateau de l'immortel | 170 | 26 | 0 |
| 7 | Les ruines souterraines | 35 | 18 | 0 |
| 8 | La vallee d'argent | 46 | 18 | 0 |
| 9 | Les ruines dans le lac | 41 | 18 | 0 |
| 10 | La grotte scellee | 349 | 38 | 0 |

Un gestionnaire employe dans plusieurs zones compte dans chacune : le tableau se lit
« ce qui m'attend si je vais la », pas « ce qui n'existe qu'ici ». Les totaux par zone
depassent donc le total global.

---

## Tables statiques — valables dans toutes les zones

Les trois tables gravees dans `.data` que les passes `0x0047aa70`, `0x0047ab70` et
`0x0047acf0` lisent pour un indice sous `0xc0`. Elles ne dependent d'aucune zone.

274 gestionnaires, 274 implementes, 219 testes.

| gestionnaire | tables | implemente | teste |
|---|---|---|---|
| `0x00403E80` | 00551C30 | oui | **non** |
| `0x00404000` | 00552230 | oui | **non** |
| `0x00404920` | 00551C30 | oui | **non** |
| `0x00404A50` | 00552230 | oui | **non** |
| `0x00445950` | 00551C30 | oui | **non** |
| `0x004459E0` | 00552230 | oui | **non** |
| `0x00445CF0` | 00551C30 | oui | **non** |
| `0x00445E70` | 00551C30 | oui | **non** |
| `0x00445F60` | 00551C30 | oui | **non** |
| `0x00446060` | 00552230 | oui | **non** |
| `0x00446170` | 00551C30 | oui | **non** |
| `0x00446200` | 00551C30 | oui | **non** |
| `0x004462B0` | 00551C30 | oui | **non** |
| `0x00446410` | 00551C30 | oui | **non** |
| `0x00446620` | 00551F30 | oui | **non** |
| `0x00446900` | 00551C30 | oui | **non** |
| `0x00446A90` | 00551C30 | oui | **non** |
| `0x00446C90` | 00552230 | oui | **non** |
| `0x00446DD0` | 00551C30 | oui | **non** |
| `0x00446E20` | 00551F30 | oui | **non** |
| `0x00446F30` | 00551C30 | oui | **non** |
| `0x00447050` | 00552230 | oui | **non** |
| `0x00447170` | 00551C30 | oui | **non** |
| `0x00447630` | 00551C30 | oui | **non** |
| `0x004477D0` | 00551C30 | oui | **non** |
| `0x004478F0` | 00551F30 | oui | **non** |
| `0x00447A90` | 00551C30 | oui | **non** |
| `0x00447D50` | 00551F30 | oui | **non** |
| `0x0044D9B0` | 00552230 | oui | **non** |
| `0x0044DB30` | 00551F30 | oui | **non** |
| `0x0044EF40` | 00552230 | oui | **non** |
| `0x0044F290` | 00551F30 | oui | **non** |
| `0x0044F600` | 00551F30 | oui | **non** |
| `0x0044F750` | 00552230 | oui | **non** |
| `0x0044FC90` | 00552230 | oui | **non** |
| `0x0044FF40` | 00552230 | oui | **non** |
| `0x00475170` | 00551C30 | oui | **non** |
| `0x004751E0` | 00551F30 | oui | **non** |
| `0x004752F0` | 00552230 | oui | **non** |
| `0x004753D0` | 00551C30 | oui | **non** |
| `0x004755F0` | 00551C30 | oui | **non** |
| `0x00475730` | 00552230 | oui | **non** |
| `0x00475B90` | 00551C30 | oui | **non** |
| `0x00475BE0` | 00551F30 | oui | **non** |
| `0x00475CC0` | 00551C30 | oui | **non** |
| `0x00476350` | 00551C30 | oui | **non** |
| `0x00476410` | 00551C30 | oui | **non** |
| `0x00476500` | 00551C30 | oui | **non** |
| `0x00476730` | 00551C30 | oui | **non** |
| `0x004768A0` | 00552230 | oui | **non** |
| `0x00476950` | 00551C30 | oui | **non** |
| `0x00476970` | 00551C30 | oui | **non** |
| `0x0047ADC0` | 00551C30 00551F30 00552230 | oui | **non** |
| `0x00481250` | 00551F30 | oui | **non** |
| `0x00482FA0` | 00552230 | oui | **non** |
| `0x00402FC0` | 00551C30 | oui | oui |
| `0x00403250` | 00552230 | oui | oui |
| `0x004034A0` | 00551F30 | oui | oui |
| `0x00403710` | 00551C30 | oui | oui |
| `0x004038F0` | 00552230 | oui | oui |
| `0x00403A90` | 00551F30 | oui | oui |
| `0x00403C10` | 00551C30 | oui | oui |
| `0x00403D60` | 00552230 | oui | oui |
| `0x004041C0` | 00551C30 | oui | oui |
| `0x00404310` | 00552230 | oui | oui |
| `0x00404460` | 00551C30 | oui | oui |
| `0x004045F0` | 00552230 | oui | oui |
| `0x00404730` | 00552230 | oui | oui |
| `0x00445860` | 00552230 | oui | oui |
| `0x00446790` | 00551C30 | oui | oui |
| `0x00447280` | 00551C30 | oui | oui |
| `0x00447380` | 00551F30 | oui | oui |
| `0x004474C0` | 00552230 | oui | oui |
| `0x00447DF0` | 00551C30 | oui | oui |
| `0x00448060` | 00551C30 | oui | oui |
| `0x00448250` | 00551F30 | oui | oui |
| `0x00448300` | 00551C30 | oui | oui |
| `0x004484F0` | 00551C30 | oui | oui |
| `0x004487F0` | 00551F30 | oui | oui |
| `0x00448C40` | 00551F30 | oui | oui |
| `0x00448E20` | 00552230 | oui | oui |
| `0x00448EF0` | 00551C30 | oui | oui |
| `0x004491F0` | 00551C30 | oui | oui |
| `0x00449590` | 00551C30 | oui | oui |
| `0x00449760` | 00552230 | oui | oui |
| `0x004498B0` | 00551C30 | oui | oui |
| `0x00449A70` | 00551F30 | oui | oui |
| `0x00449C30` | 00551C30 | oui | oui |
| `0x00449E30` | 00552230 | oui | oui |
| `0x00449F90` | 00551C30 | oui | oui |
| `0x0044A080` | 00551F30 | oui | oui |
| `0x0044A320` | 00552230 | oui | oui |
| `0x0044A4B0` | 00551C30 | oui | oui |
| `0x0044A6A0` | 00551F30 | oui | oui |
| `0x0044A7A0` | 00551C30 | oui | oui |
| `0x0044A880` | 00551F30 | oui | oui |
| `0x0044AE90` | 00551C30 | oui | oui |
| `0x0044AF70` | 00551F30 | oui | oui |
| `0x0044B510` | 00551C30 | oui | oui |
| `0x0044B6F0` | 00551F30 | oui | oui |
| `0x0044B7B0` | 00551C30 | oui | oui |
| `0x0044B950` | 00552230 | oui | oui |
| `0x0044BA00` | 00551F30 | oui | oui |
| `0x0044BAF0` | 00551C30 | oui | oui |
| `0x0044BCC0` | 00552230 | oui | oui |
| `0x0044BE40` | 00551F30 | oui | oui |
| `0x0044BF00` | 00551C30 | oui | oui |
| `0x0044C110` | 00551F30 | oui | oui |
| `0x0044C260` | 00552230 | oui | oui |
| `0x0044C3A0` | 00551C30 | oui | oui |
| `0x0044C730` | 00551F30 | oui | oui |
| `0x0044C800` | 00551F30 | oui | oui |
| `0x0044C8A0` | 00551C30 | oui | oui |
| `0x0044CAC0` | 00551F30 | oui | oui |
| `0x0044CBD0` | 00551F30 | oui | oui |
| `0x0044CD00` | 00551C30 | oui | oui |
| `0x0044CF30` | 00551F30 | oui | oui |
| `0x0044D030` | 00551F30 | oui | oui |
| `0x0044D170` | 00552230 | oui | oui |
| `0x0044D370` | 00551C30 | oui | oui |
| `0x0044D8E0` | 00551F30 | oui | oui |
| `0x0044DC60` | 00551C30 | oui | oui |
| `0x0044DED0` | 00551F30 | oui | oui |
| `0x0044EA60` | 00551F30 | oui | oui |
| `0x0044F060` | 00551C30 | oui | oui |
| `0x0044F930` | 00551C30 | oui | oui |
| `0x0044FB10` | 00552230 | oui | oui |
| `0x0044FE00` | 00552230 | oui | oui |
| `0x004500B0` | 00552230 | oui | oui |
| `0x004501B0` | 00551F30 | oui | oui |
| `0x00475400` | 00552230 | oui | oui |
| `0x004754C0` | 00551C30 | oui | oui |
| `0x004755C0` | 00552230 | oui | oui |
| `0x00475760` | 00551C30 | oui | oui |
| `0x004757A0` | 00552230 | oui | oui |
| `0x004758C0` | 00551C30 | oui | oui |
| `0x00475910` | 00552230 | oui | oui |
| `0x00475A30` | 00551C30 | oui | oui |
| `0x00475A80` | 00552230 | oui | oui |
| `0x00475D40` | 00551C30 | oui | oui |
| `0x00475EA0` | 00551C30 | oui | oui |
| `0x00475F60` | 00552230 | oui | oui |
| `0x00475FC0` | 00552230 | oui | oui |
| `0x004760C0` | 00551C30 | oui | oui |
| `0x00476140` | 00552230 | oui | oui |
| `0x004761D0` | 00551C30 | oui | oui |
| `0x00476290` | 00552230 | oui | oui |
| `0x00476320` | 00552230 | oui | oui |
| `0x004765D0` | 00551F30 | oui | oui |
| `0x00476770` | 00551C30 | oui | oui |
| `0x004767E0` | 00551F30 | oui | oui |
| `0x004769A0` | 00551C30 | oui | oui |
| `0x004769F0` | 00552230 | oui | oui |
| `0x00476C70` | 00551C30 | oui | oui |
| `0x00476CF0` | 00552230 | oui | oui |
| `0x00476FD0` | 00551C30 | oui | oui |
| `0x004770A0` | 00551F30 | oui | oui |
| `0x00477360` | 00551C30 | oui | oui |
| `0x00477420` | 00551F30 | oui | oui |
| `0x004774C0` | 00551C30 | oui | oui |
| `0x00477600` | 00552230 | oui | oui |
| `0x00477660` | 00551C30 | oui | oui |
| `0x004777A0` | 00551C30 | oui | oui |
| `0x004778E0` | 00551C30 | oui | oui |
| `0x00477A20` | 00551C30 | oui | oui |
| `0x00477A90` | 00552230 | oui | oui |
| `0x00477D20` | 00551C30 | oui | oui |
| `0x00477DA0` | 00551F30 | oui | oui |
| `0x00477E40` | 00552230 | oui | oui |
| `0x004780B0` | 00551C30 | oui | oui |
| `0x004785F0` | 00551C30 | oui | oui |
| `0x004786C0` | 00551C30 | oui | oui |
| `0x00478C20` | 00551C30 | oui | oui |
| `0x00478D10` | 00551C30 | oui | oui |
| `0x00478DF0` | 00551C30 | oui | oui |
| `0x00478FC0` | 00551F30 | oui | oui |
| `0x00479030` | 00551F30 | oui | oui |
| `0x004790C0` | 00551C30 | oui | oui |
| `0x00479250` | 00551F30 | oui | oui |
| `0x00479300` | 00551C30 | oui | oui |
| `0x00479490` | 00551F30 | oui | oui |
| `0x00479B90` | 00551F30 | oui | oui |
| `0x00479BC0` | 00552230 | oui | oui |
| `0x00479C50` | 00551C30 | oui | oui |
| `0x00479DE0` | 00551C30 | oui | oui |
| `0x00479F70` | 00551C30 | oui | oui |
| `0x0047A100` | 00551C30 | oui | oui |
| `0x0047A270` | 00551F30 | oui | oui |
| `0x0047A5D0` | 00551C30 | oui | oui |
| `0x0047A730` | 00552230 | oui | oui |
| `0x0047A7D0` | 00552230 | oui | oui |
| `0x0047A880` | 00552230 | oui | oui |
| `0x0047A950` | 00552230 | oui | oui |
| `0x0047ADD0` | 00551C30 | oui | oui |
| `0x0047AE80` | 00551F30 | oui | oui |
| `0x0047B050` | 00552230 | oui | oui |
| `0x0047B0B0` | 00551C30 | oui | oui |
| `0x0047B150` | 00552230 | oui | oui |
| `0x0047B340` | 00552230 | oui | oui |
| `0x0047B3A0` | 00552230 | oui | oui |
| `0x0047B460` | 00551C30 | oui | oui |
| `0x0047B6B0` | 00551F30 | oui | oui |
| `0x0047B750` | 00552230 | oui | oui |
| `0x0047B7A0` | 00551C30 | oui | oui |
| `0x0047B850` | 00551F30 | oui | oui |
| `0x0047BB10` | 00552230 | oui | oui |
| `0x0047BB50` | 00551C30 | oui | oui |
| `0x0047BC40` | 00551F30 | oui | oui |
| `0x0047BD00` | 00551F30 | oui | oui |
| `0x0047BDF0` | 00552230 | oui | oui |
| `0x0047BF00` | 00551C30 | oui | oui |
| `0x0047C1E0` | 00551F30 | oui | oui |
| `0x0047C2C0` | 00552230 | oui | oui |
| `0x0047C3A0` | 00551C30 | oui | oui |
| `0x0047C450` | 00551F30 | oui | oui |
| `0x0047C660` | 00551C30 | oui | oui |
| `0x0047C830` | 00551F30 | oui | oui |
| `0x0047C910` | 00551C30 | oui | oui |
| `0x0047CA50` | 00551F30 | oui | oui |
| `0x0047CE40` | 00551C30 | oui | oui |
| `0x0047CF30` | 00551F30 | oui | oui |
| `0x0047CFF0` | 00551C30 | oui | oui |
| `0x0047D270` | 00551F30 | oui | oui |
| `0x0047D2A0` | 00552230 | oui | oui |
| `0x0047D330` | 00551C30 | oui | oui |
| `0x0047D470` | 00551F30 | oui | oui |
| `0x0047D600` | 00551C30 | oui | oui |
| `0x0047D670` | 00551F30 | oui | oui |
| `0x0047D950` | 00551F30 | oui | oui |
| `0x0047DA50` | 00552230 | oui | oui |
| `0x0047DB50` | 00551C30 | oui | oui |
| `0x0047DE10` | 00552230 | oui | oui |
| `0x0047DF70` | 00551C30 | oui | oui |
| `0x0047E150` | 00551F30 | oui | oui |
| `0x0047E200` | 00551F30 | oui | oui |
| `0x0047E260` | 00551C30 | oui | oui |
| `0x0047E370` | 00551F30 | oui | oui |
| `0x0047E590` | 00551C30 | oui | oui |
| `0x0047E6F0` | 00551C30 | oui | oui |
| `0x0047EA80` | 00551F30 | oui | oui |
| `0x0047EAE0` | 00551C30 | oui | oui |
| `0x0047EC30` | 00551F30 | oui | oui |
| `0x0047EF60` | 00551F30 | oui | oui |
| `0x0047F070` | 00551C30 | oui | oui |
| `0x0047F290` | 00551F30 | oui | oui |
| `0x0047F2B0` | 00551C30 | oui | oui |
| `0x0047F400` | 00551F30 | oui | oui |
| `0x0047F470` | 00551F30 | oui | oui |
| `0x0047F570` | 00551C30 | oui | oui |
| `0x0047FB10` | 00552230 | oui | oui |
| `0x0047FB80` | 00551C30 | oui | oui |
| `0x0047FE40` | 00551F30 | oui | oui |
| `0x0047FE60` | 00551C30 | oui | oui |
| `0x00480080` | 00551F30 | oui | oui |
| `0x00480490` | 00551F30 | oui | oui |
| `0x00480600` | 00551C30 | oui | oui |
| `0x00480A20` | 00551F30 | oui | oui |
| `0x00480A70` | 00551F30 | oui | oui |
| `0x00480B90` | 00551C30 | oui | oui |
| `0x00480E40` | 00551F30 | oui | oui |
| `0x00481670` | 00551C30 | oui | oui |
| `0x00481720` | 00551F30 | oui | oui |
| `0x004819D0` | 00551C30 | oui | oui |
| `0x00481D50` | 00551F30 | oui | oui |
| `0x00481E30` | 00551F30 | oui | oui |
| `0x004821A0` | 00551F30 | oui | oui |
| `0x00482300` | 00551F30 | oui | oui |
| `0x00482370` | 00551C30 | oui | oui |
| `0x004824F0` | 00551C30 | oui | oui |
| `0x004828B0` | 00551F30 | oui | oui |
| `0x004828F0` | 00551C30 | oui | oui |
| `0x00482DE0` | 00551F30 | oui | oui |
| `0x00482ED0` | 00551F30 | oui | oui |
| `0x00482EF0` | 00552230 | oui | oui |

## Zone 0 — L'auberge

48 gestionnaires, 48 implementes, 16 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x00404B90` | 0 | 0056B0C8 | oui | **non** |
| `0x00404C80` | 0 | 0056B2C8 | oui | **non** |
| `0x00404E50` | 0 | 0056B0C8 | oui | **non** |
| `0x00405E70` | 0 | 0056B0C8 | oui | **non** |
| `0x00405FE0` | 0 | 0056B0C8 | oui | **non** |
| `0x00442D90` | 0 | 0056AEC8 | oui | **non** |
| `0x00442DB0` | 0 | 0056AEC8 | oui | **non** |
| `0x00442E10` | 0 | 0056AEC8 | oui | **non** |
| `0x00442E20` | 0 | 0056AEC8 | oui | **non** |
| `0x00442E70` | 0 | 0056AEC8 | oui | **non** |
| `0x00442EA0` | 0 | 0056AEC8 | oui | **non** |
| `0x00442F20` | 0 | 0056AEC8 | oui | **non** |
| `0x004430B0` | 0 | 0056AEC8 | oui | **non** |
| `0x004431F0` | 0 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0 | 0056AEC8 | oui | **non** |
| `0x004502E0` | 0 | 0056ADC8 | oui | **non** |
| `0x00450760` | 0 | 0056ADC8 | oui | **non** |
| `0x004734B0` | 0 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0 | 0056ADC8 | oui | **non** |
| `0x00487260` | 0 | 0056AD48 | oui | **non** |
| `0x0053DCB0` | 0 | 0056AFC8 | oui | **non** |
| `0x0053DD20` | 0 | 0056AEC8 | oui | **non** |
| `0x0053DEA0` | 0 | 0056AEC8 | oui | **non** |
| `0x0053DF20` | 0 | 0056AEC8 | oui | **non** |
| `0x0053DF90` | 0 | 0056AFC8 | oui | **non** |
| `0x00404EF0` | 0 | 0056B0C8 | oui | oui |
| `0x004055C0` | 0 | 0056B0C8 | oui | oui |
| `0x00405A70` | 0 | 0056B0C8 | oui | oui |
| `0x00405D60` | 0 | 0056B0C8 | oui | oui |
| `0x00442F60` | 0 | 0056AEC8 | oui | oui |
| `0x00443100` | 0 | 0056AEC8 | oui | oui |
| `0x004441F0` | 0 | 0056AEC8 | oui | oui |
| `0x004508B0` | 0 | 0056ADC8 | oui | oui |
| `0x004508D0` | 0 | 0056ADC8 | oui | oui |
| `0x004509B0` | 0 | 0056ADC8 | oui | oui |
| `0x00450AA0` | 0 | 0056ADC8 | oui | oui |
| `0x00450BD0` | 0 | 0056ADC8 | oui | oui |
| `0x00450C60` | 0 | 0056ADC8 | oui | oui |
| `0x00450D30` | 0 | 0056ADC8 | oui | oui |
| `0x004736D0` | 0 | 0056ADC8 | oui | oui |
| `0x00486EE0` | 0 | 0056AD48 | oui | oui |

## Zone 1 — La mine abandonnee

157 gestionnaires, 157 implementes, 0 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x004060C0` | 0,1,2 | 0056B0C8 | oui | **non** |
| `0x00406120` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00406570` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x004065D0` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00406680` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00406810` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00406970` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00406AA0` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00406C40` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00406DF0` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00406F60` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x004070F0` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x004071E0` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00407800` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x004079E0` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00407AD0` | 0,1,2 | 0056B2C8 | oui | **non** |
| `0x00407C10` | 5,6,7,8 | 0056B0C8 | oui | **non** |
| `0x00407D70` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00407FE0` | 5,6,7,8 | 0056B0C8 | oui | **non** |
| `0x00408040` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00408490` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x004084F0` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00408680` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x004087E0` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00408910` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00408AB0` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00408C60` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00408DD0` | 5,6,7,8 | 0056B0C8 | oui | **non** |
| `0x00408EF0` | 5,6,7,8 | 0056B1C8 | oui | **non** |
| `0x00409120` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x004092B0` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00409320` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00409410` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00409A30` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00409C10` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00409D00` | 5,6,7,8 | 0056B2C8 | oui | **non** |
| `0x00409E40` | 3,4,9 | 0056B0C8 | oui | **non** |
| `0x00409F40` | 3,4,9 | 0056B1C8 | oui | **non** |
| `0x0040A600` | 5,6,7,8,3,4,9,3,4,9,3,4,9 | 0056ADC8 0056AEC8 0056B0C8 0056B2C8 | oui | **non** |
| `0x0040A610` | 3,4,9 | 0056B0C8 | oui | **non** |
| `0x0040A670` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040AAC0` | 3,4,9 | 0056B0C8 | oui | **non** |
| `0x0040B1B0` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040B8D0` | 3,4,9 | 0056B0C8 | oui | **non** |
| `0x0040B900` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040B940` | 3,4,9 | 0056B0C8 | oui | **non** |
| `0x0040BCA0` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040BE50` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040BF50` | 3,4,9 | 0056B0C8 | oui | **non** |
| `0x0040C460` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040C5D0` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040C730` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040C820` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040C910` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040CA80` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x0040CC60` | 3,4,9 | 0056B2C8 | oui | **non** |
| `0x00442E10` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00443100` | 3,4,9 | 0056AEC8 | oui | **non** |
| `0x00443170` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x004431B0` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x004431F0` | 0,1,2,3,4,9 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004432A0` | 3,4,9 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004439A0` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004441F0` | 0,1,2,5,6,7,8 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 3,4,9 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00451170` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004511F0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00451250` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00451460` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00451630` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00451790` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004518D0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00451A00` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00451B40` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00452090` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004523A0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00452410` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00452670` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00452790` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004528D0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00452950` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004529F0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00452A90` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00452B20` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00452B60` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00452BD0` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00452E40` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00453000` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00453070` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00453280` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x004533A0` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00453650` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x004538C0` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00453DE0` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00453E60` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00453F60` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00454130` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00454220` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x004542C0` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00454360` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x00454450` | 5,6,7,8 | 0056ADC8 | oui | **non** |
| `0x004544E0` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x004545B0` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00454610` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00454800` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00454AD0` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00454B40` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00454BB0` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00454D90` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00454E10` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00454ED0` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x004551D0` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00455570` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00455A90` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00455EB0` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x00456010` | 3,4,9 | 0056ADC8 | oui | **non** |
| `0x004734B0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00474380` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746F0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00482FF0` | 3,4,9 | 0056B1C8 | oui | **non** |
| `0x00483B50` | 3,4,9 | 0056B0C8 | oui | **non** |
| `0x004879F0` | 0,1,2 | 0056AD48 | oui | **non** |
| `0x00488210` | 0,1,2 | 0056AD48 | oui | **non** |
| `0x004889A0` | 0,1,2 | 0056AD48 | oui | **non** |
| `0x00489CA0` | 0,1,2 | 0056AD48 | oui | **non** |
| `0x0048AD80` | 0,1,2 | 0056AD48 | oui | **non** |
| `0x0048B530` | 5,6,7,8 | 0056AD48 | oui | **non** |
| `0x0048C530` | 5,6,7,8 | 0056AD48 | oui | **non** |
| `0x0048CBB0` | 5,6,7,8 | 0056AD48 | oui | **non** |
| `0x0048D300` | 5,6,7,8 | 0056AD48 | oui | **non** |
| `0x0048DB30` | 5,6,7,8 | 0056AD48 | oui | **non** |
| `0x0048E3A0` | 5,6,7,8 | 0056AD48 | oui | **non** |
| `0x0048F230` | 5,6,7,8 | 0056AD48 | oui | **non** |
| `0x004901B0` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x00491650` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x004921A0` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x00492E40` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x004935F0` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x00493C70` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x004944E0` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x00494D10` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x00495C90` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x004965B0` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x00497370` | 3,4,9 | 0056AD48 | oui | **non** |
| `0x005413E0` | 0,1,2,3,4,5,6,7,8,9 | 0056AFC8 | oui | **non** |

## Zone 2 — La montagne du dragon de feu

42 gestionnaires, 40 implementes, 0 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x0040D930` | 0 | 0056B0C8 | **non** | **non** |
| `0x0040E3A0` | 0 | 0056B0C8 | **non** | **non** |
| `0x0040CDA0` | 0 | 0056B0C8 | oui | **non** |
| `0x0040CEA0` | 0 | 0056B1C8 | oui | **non** |
| `0x0040D560` | 0 | 0056B0C8 | oui | **non** |
| `0x0040D6C0` | 0 | 0056B2C8 | oui | **non** |
| `0x0040DDE0` | 0 | 0056B2C8 | oui | **non** |
| `0x0040E6F0` | 0 | 0056B2C8 | oui | **non** |
| `0x0040E860` | 0 | 0056B2C8 | oui | **non** |
| `0x0040EA00` | 0 | 0056B2C8 | oui | **non** |
| `0x0040EAF0` | 0 | 0056B2C8 | oui | **non** |
| `0x0040EBE0` | 0 | 0056B2C8 | oui | **non** |
| `0x00442E10` | 0 | 0056AEC8 | oui | **non** |
| `0x004431F0` | 0 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0 | 0056AEC8 | oui | **non** |
| `0x004432A0` | 0 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0 | 0056AEC8 | oui | **non** |
| `0x004441F0` | 0 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 0 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0 | 0056AEC8 | oui | **non** |
| `0x004560A0` | 0 | 0056ADC8 | oui | **non** |
| `0x00456110` | 0 | 0056ADC8 | oui | **non** |
| `0x00456180` | 0 | 0056ADC8 | oui | **non** |
| `0x00456270` | 0 | 0056ADC8 | oui | **non** |
| `0x00456310` | 0 | 0056ADC8 | oui | **non** |
| `0x00456590` | 0 | 0056ADC8 | oui | **non** |
| `0x004734B0` | 0 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0 | 0056ADC8 | oui | **non** |
| `0x004830F0` | 0 | 0056B0C8 | oui | **non** |
| `0x00483260` | 0 | 0056B1C8 | oui | **non** |
| `0x00483360` | 0 | 0056B2C8 | oui | **non** |
| `0x00497EF0` | 0 | 0056AD48 | oui | **non** |
| `0x00499140` | 0 | 0056AD48 | oui | **non** |
| `0x005413E0` | 0 | 0056AFC8 | oui | **non** |

## Zone 3 — La foret

136 gestionnaires, 136 implementes, 90 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x0040FAC0` | 0,1,2,3,4 | 0056B2C8 | oui | **non** |
| `0x0040FBB0` | 0,1,2,3,4 | 0056B2C8 | oui | **non** |
| `0x004101D0` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00410370` | 6,7,8 | 0056B0C8 | oui | **non** |
| `0x004103D0` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00410820` | 6,7,8 | 0056B0C8 | oui | **non** |
| `0x00410CD0` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00411240` | 6,7,8 | 0056B0C8 | oui | **non** |
| `0x00411860` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00411F80` | 6,7,8 | 0056B0C8 | oui | **non** |
| `0x00412240` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00412330` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00412460` | 6,7,8 | 0056B0C8 | oui | **non** |
| `0x00412720` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00412810` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00412950` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00412B30` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00412C20` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x00412D10` | 6,7,8 | 0056B2C8 | oui | **non** |
| `0x004137E0` | 5,9 | 0056B0C8 | oui | **non** |
| `0x00413840` | 5,9 | 0056B2C8 | oui | **non** |
| `0x00415F40` | 5,9 | 0056B2C8 | oui | **non** |
| `0x00416120` | 5,9 | 0056B2C8 | oui | **non** |
| `0x00416210` | 5,9 | 0056B2C8 | oui | **non** |
| `0x00416300` | 5,9 | 0056B2C8 | oui | **non** |
| `0x004432E0` | 0,1,2,3,4,6,7,8 | 0056AEC8 | oui | **non** |
| `0x00443300` | 0,1,2,3,4 | 0056AEC8 | oui | **non** |
| `0x00443A00` | 6,7,8 | 0056AEC8 | oui | **non** |
| `0x00457770` | 0,1,2,3,4 | 0056ADC8 | oui | **non** |
| `0x00458070` | 6,7,8 | 0056ADC8 | oui | **non** |
| `0x00458E90` | 6,7,8 | 0056ADC8 | oui | **non** |
| `0x00458FC0` | 6,7,8 | 0056ADC8 | oui | **non** |
| `0x004593C0` | 6,7,8 | 0056ADC8 | oui | **non** |
| `0x00459840` | 6,7,8 | 0056ADC8 | oui | **non** |
| `0x00459A70` | 6,7,8 | 0056ADC8 | oui | **non** |
| `0x00459B20` | 6,7,8 | 0056ADC8 | oui | **non** |
| `0x00459FA0` | 6,7,8 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00483760` | 5,9 | 0056B1C8 | oui | **non** |
| `0x00483920` | 5,9 | 0056B2C8 | oui | **non** |
| `0x004A0480` | 6,7,8 | 0056AD48 | oui | **non** |
| `0x00541550` | 6,7,8 | 0056AFC8 | oui | **non** |
| `0x0040EDF0` | 0,1,2,3,4 | 0056B0C8 | oui | oui |
| `0x0040EF40` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x0040F190` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x0040F260` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x0040F370` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x0040F470` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x0040F650` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x0040F7F0` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x0040F8E0` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x00413330` | 5,9 | 0056B0C8 | oui | oui |
| `0x004134C0` | 5,9 | 0056B1C8 | oui | oui |
| `0x00413C90` | 5,9 | 0056B0C8 | oui | oui |
| `0x00414140` | 5,9 | 0056B2C8 | oui | oui |
| `0x004146B0` | 5,9 | 0056B0C8 | oui | oui |
| `0x00414CD0` | 5,9 | 0056B2C8 | oui | oui |
| `0x004153F0` | 5,9 | 0056B0C8 | oui | oui |
| `0x004156B0` | 5,9 | 0056B2C8 | oui | oui |
| `0x004157A0` | 5,6,7,8,9 | 0056B1C8 | oui | oui |
| `0x00415860` | 5,9 | 0056B2C8 | oui | oui |
| `0x00415990` | 5,9 | 0056B0C8 | oui | oui |
| `0x00415C50` | 5,9 | 0056B2C8 | oui | oui |
| `0x00415D40` | 5,6,7,8,9 | 0056B1C8 | oui | oui |
| `0x00415E00` | 5,9 | 0056B2C8 | oui | oui |
| `0x00442E10` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x004431F0` | 0,1,2,3,4 | 0056AEC8 | oui | oui |
| `0x00443260` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x004432A0` | 5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x004433B0` | 0,1,2,3,4 | 0056AEC8 | oui | oui |
| `0x00443830` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x004438A0` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x00443960` | 5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x004439A0` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x00443B20` | 5,9 | 0056AEC8 | oui | oui |
| `0x004441F0` | 0,1,2,3,4 | 0056AEC8 | oui | oui |
| `0x00444E60` | 5,9 | 0056AEC8 | oui | oui |
| `0x00445480` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x004457F0` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x00445840` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x004566C0` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00456730` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00456750` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00456A30` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00456CF0` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00456E80` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00456F40` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00457140` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x004571D0` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00457240` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00457370` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00457BF0` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00457E20` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00457EC0` | 0,1,2,3,4 | 0056ADC8 | oui | oui |
| `0x00457FC0` | 0,1,2,3,4,6,7,8 | 0056ADC8 | oui | oui |
| `0x00458170` | 0,1,2,3,4,6,7,8 | 0056ADC8 | oui | oui |
| `0x00458200` | 6,7,8 | 0056ADC8 | oui | oui |
| `0x00458DE0` | 6,7,8 | 0056ADC8 | oui | oui |
| `0x00459C20` | 6,7,8 | 0056ADC8 | oui | oui |
| `0x00459DC0` | 6,7,8 | 0056ADC8 | oui | oui |
| `0x004734B0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | oui |
| `0x004736D0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | oui |
| `0x00473ED0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | oui |
| `0x00474380` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | oui |
| `0x004746F0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | oui |
| `0x00483450` | 0,1,2,3,4 | 0056B0C8 | oui | oui |
| `0x004834D0` | 0,1,2,3,4 | 0056B2C8 | oui | oui |
| `0x00483650` | 5,9 | 0056B0C8 | oui | oui |
| `0x00499DE0` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049A360` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049A980` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049B220` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049B840` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049C0B0` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049C8E0` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049D030` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049DA40` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049E190` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049E920` | 0,1,2,3,4 | 0056AD48 | oui | oui |
| `0x0049F0C0` | 6,7,8 | 0056AD48 | oui | oui |
| `0x0049FAD0` | 6,7,8 | 0056AD48 | oui | oui |
| `0x004A13E0` | 6,7,8 | 0056AD48 | oui | oui |
| `0x004A1D50` | 6,7,8 | 0056AD48 | oui | oui |
| `0x004A2C90` | 6,7,8 | 0056AD48 | oui | oui |
| `0x004A35B0` | 6,7,8 | 0056AD48 | oui | oui |
| `0x004A3C30` | 5,9 | 0056AD48 | oui | oui |
| `0x004A4270` | 5,9 | 0056AD48 | oui | oui |
| `0x004A4AE0` | 5,9 | 0056AD48 | oui | oui |
| `0x004A54E0` | 5,9 | 0056AD48 | oui | oui |
| `0x004A5F10` | 5,9 | 0056AD48 | oui | oui |
| `0x00541440` | 0,1,2,3,4,6,7,8 | 0056AFC8 | oui | oui |
| `0x00541B80` | 5,9 | 0056AFC8 | oui | oui |

## Zone 4 — La vieille tour

176 gestionnaires, 28 implementes, 3 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x00416920` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00416A80` | 0,1,2,4 | 0056B0C8 | **non** | **non** |
| `0x00416C40` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00416DE0` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00416EE0` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417000` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417110` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417420` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417650` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417770` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417930` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417A20` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417B10` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417CA0` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417E10` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00417F80` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00418120` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00418290` | 0,1,2,4 | 0056B2C8 | **non** | **non** |
| `0x00418370` | 10,5,6,8 | 0056B0C8 | **non** | **non** |
| `0x00418640` | 10,5,6,8 | 0056B0C8 | **non** | **non** |
| `0x004186A0` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00418AF0` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00418C40` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419030` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419260` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419380` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419540` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419630` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419720` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x004198B0` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419A20` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419B90` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419D30` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419E60` | 10,5,6,8 | 0056B2C8 | **non** | **non** |
| `0x00419F40` | 3,7,9 | 0056B0C8 | **non** | **non** |
| `0x0041A560` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041AC80` | 3,7,9 | 0056B0C8 | **non** | **non** |
| `0x0041AFA0` | 3,7,9 | 0056B1C8 | **non** | **non** |
| `0x0041B220` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041B2F0` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041B3F0` | 3,7,9 | 0056B0C8 | **non** | **non** |
| `0x0041B420` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041B460` | 3,7,9 | 0056B0C8 | **non** | **non** |
| `0x0041B7C0` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041B9F0` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041BB20` | 3,7,9 | 0056B0C8 | **non** | **non** |
| `0x0041BC90` | 3,7,9 | 0056B1C8 | **non** | **non** |
| `0x0041BF00` | 3,7,9 | 0056B1C8 | **non** | **non** |
| `0x0041BFA0` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041C0E0` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041C250` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041C560` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x0041C650` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x00443C60` | 0,1,2,4 | 0056AEC8 | **non** | **non** |
| `0x00443CB0` | 0,1,2,4 | 0056AEC8 | **non** | **non** |
| `0x00443D00` | 0,1,2,4 | 0056AEC8 | **non** | **non** |
| `0x00444200` | 3,7,9 | 0056AEC8 | **non** | **non** |
| `0x004442F0` | 3,7,9 | 0056AEC8 | **non** | **non** |
| `0x0045A310` | 0,1,2,3,4,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045A3B0` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045A4D0` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045A630` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045A9B0` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045AA20` | 0,1,2,4 | 0056ADC8 | **non** | oui |
| `0x0045AA90` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045ABC0` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045AD20` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045AE90` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045B030` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045B290` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045B3B0` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045B540` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045B6C0` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045B750` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045B7E0` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045B9E0` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045BB10` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045BE00` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045BF20` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045C090` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045C120` | 0,1,2,4 | 0056ADC8 | **non** | **non** |
| `0x0045C2E0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045C660` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045C6E0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045C7C0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045C870` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045CE50` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045CEE0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045CF70` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D1B0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D240` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D2B0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D340` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D3A0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D400` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D4C0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D550` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D5E0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D6F0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045D750` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045DB10` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045DBE0` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045DD00` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045DF60` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045E040` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045E100` | 10,5,6,8 | 0056ADC8 | **non** | **non** |
| `0x0045E300` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045E4B0` | 0,1,2,3,4,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045E4E0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045E860` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045EAB0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045EB30` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045EC10` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045ECC0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045F290` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045F320` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045F3B0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045F5F0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045F6C0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045F7E0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045FA80` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045FBA0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045FD40` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x0045FFA0` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x00460080` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x00460140` | 3,7,9 | 0056ADC8 | **non** | **non** |
| `0x00483930` | 3,7,9 | 0056B0C8 | **non** | **non** |
| `0x00483A30` | 3,7,9 | 0056B1C8 | **non** | **non** |
| `0x00484F80` | 3,7,9 | 0056B0C8 | **non** | **non** |
| `0x004850C0` | 3,7,9 | 0056B2C8 | **non** | **non** |
| `0x004A6A00` | 0,1,2,4 | 0056AD48 | **non** | **non** |
| `0x004A8510` | 0,1,2,4 | 0056AD48 | **non** | **non** |
| `0x004A9150` | 0,1,2,4 | 0056AD48 | **non** | **non** |
| `0x004AA070` | 0,1,2,4 | 0056AD48 | **non** | **non** |
| `0x004AAA80` | 0,1,2,4 | 0056AD48 | **non** | **non** |
| `0x004AB940` | 10,5,6,8 | 0056AD48 | **non** | **non** |
| `0x004ADAE0` | 10,5,6,8 | 0056AD48 | **non** | **non** |
| `0x004AE820` | 10,5,6,8 | 0056AD48 | **non** | **non** |
| `0x004AF560` | 10,5,6,8 | 0056AD48 | **non** | **non** |
| `0x004B0600` | 10,5,6,8 | 0056AD48 | **non** | **non** |
| `0x004B1660` | 10,5,6,8 | 0056AD48 | **non** | **non** |
| `0x004B3650` | 3,7,9 | 0056AD48 | **non** | **non** |
| `0x004B4570` | 3,7,9 | 0056AD48 | **non** | **non** |
| `0x004B4DB0` | 3,7,9 | 0056AD48 | **non** | **non** |
| `0x004B5AF0` | 3,7,9 | 0056AD48 | **non** | **non** |
| `0x004B6B90` | 3,7,9 | 0056AD48 | **non** | **non** |
| `0x004B7540` | 3,7,9 | 0056AD48 | **non** | **non** |
| `0x004B7E20` | 3,7,9 | 0056AD48 | **non** | **non** |
| `0x0040A600` | 3,7,9 | 0056ADC8 | oui | **non** |
| `0x00442E10` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00443170` | 0,1,2,4 | 0056AEC8 | oui | **non** |
| `0x004432A0` | 3,7,9 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004439A0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 3,7,9 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004734B0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00474380` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00482FF0` | 3,7,9 | 0056B1C8 | oui | **non** |
| `0x00483B50` | 3,7,9 | 0056B0C8 | oui | **non** |
| `0x004A6290` | 0,1,2,4 | 0056AD48 | oui | **non** |
| `0x004A7190` | 0,1,2,4 | 0056AD48 | oui | **non** |
| `0x004ACC20` | 10,5,6,8 | 0056AD48 | oui | **non** |
| `0x004B22D0` | 3,7,9 | 0056AD48 | oui | **non** |
| `0x005413E0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AFC8 | oui | **non** |
| `0x00443260` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | oui |
| `0x004441F0` | 0,1,2,4 | 0056AEC8 | oui | oui |

## Zone 5 — Le Labyrinthe des morts

160 gestionnaires, 23 implementes, 0 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x0041C740` | 0,1,2,3 | 0056B0C8 | **non** | **non** |
| `0x0041C900` | 0,1,2,3 | 0056B1C8 | **non** | **non** |
| `0x0041D090` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041D1A0` | 0,1,2,3 | 0056B0C8 | **non** | **non** |
| `0x0041D310` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041D860` | 0,1,2,3 | 0056B0C8 | **non** | **non** |
| `0x0041D920` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041DA80` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041DBD0` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041DD40` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041DF60` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041E070` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041E250` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041E340` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041E430` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041E610` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041E700` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041E7F0` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041E9D0` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041EAF0` | 0,1,2,3 | 0056B2C8 | **non** | **non** |
| `0x0041EC20` | 5,6,7,8 | 0056B0C8 | **non** | **non** |
| `0x0041EDD0` | 5,6,7,8 | 0056B1C8 | **non** | **non** |
| `0x0041F520` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x0041F600` | 5,6,7,8 | 0056B0C8 | **non** | **non** |
| `0x0041F760` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x0041FCA0` | 5,6,7,8 | 0056B0C8 | **non** | **non** |
| `0x0041FF40` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00420480` | 5,6,7,8 | 0056B1C8 | **non** | **non** |
| `0x00420860` | 5,6,7,8 | 0056B0C8 | **non** | **non** |
| `0x00420910` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00420A70` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00420BC0` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00420D30` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00420F50` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00421060` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00421240` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00421330` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00421420` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00421600` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x004216F0` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x004217E0` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x004219C0` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00421AE0` | 5,6,7,8 | 0056B2C8 | **non** | **non** |
| `0x00421C10` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00421DC0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00422330` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x004224F0` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00422C60` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00422D50` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00422E50` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00423240` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x004232A0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x004236F0` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00423D40` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00424460` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00424490` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x004244D0` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00424800` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x004249D0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00424AE0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00424C70` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00424DC0` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x004250B0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00425270` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00425350` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00425410` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00425500` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00444340` | 0,1,2,3 | 0056AEC8 | **non** | **non** |
| `0x004443D0` | 0,1,2,3 | 0056AEC8 | **non** | **non** |
| `0x00444400` | 0,1,2,3 | 0056AEC8 | **non** | **non** |
| `0x00444450` | 5,6,7,8 | 0056AEC8 | **non** | **non** |
| `0x004444A0` | 4,9 | 0056AEC8 | **non** | **non** |
| `0x00444620` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | **non** | **non** |
| `0x00460240` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460260` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460310` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x004603C0` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460430` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x004606C0` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460800` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460990` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460A90` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460AC0` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460C00` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460D10` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460D60` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460F30` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x00460FD0` | 0,1,2,3 | 0056ADC8 | **non** | **non** |
| `0x004610E0` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461100` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461260` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461390` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x004614B0` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461730` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x004618B0` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461980` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461B00` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461BE0` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461C60` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461E00` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461E80` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00461F50` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x004620D0` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00462250` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00462330` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x004625B0` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00462860` | 5,6,7,8 | 0056ADC8 | **non** | **non** |
| `0x00462990` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x00462A80` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x00463150` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x00463300` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x004633C0` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x004635B0` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x00463640` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x00483C50` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00483CC0` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00483E90` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00483F30` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x004840E0` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00484190` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x004B8540` | 0,1,2,3 | 0056AD48 | **non** | **non** |
| `0x004B95E0` | 0,1,2,3 | 0056AD48 | **non** | **non** |
| `0x004BA110` | 0,1,2,3 | 0056AD48 | **non** | **non** |
| `0x004BAB60` | 0,1,2,3 | 0056AD48 | **non** | **non** |
| `0x004BB860` | 0,1,2,3 | 0056AD48 | **non** | **non** |
| `0x004BD170` | 5,6,7,8 | 0056AD48 | **non** | **non** |
| `0x004BEE40` | 5,6,7,8 | 0056AD48 | **non** | **non** |
| `0x004BFE90` | 5,6,7,8 | 0056AD48 | **non** | **non** |
| `0x004C0B60` | 5,6,7,8 | 0056AD48 | **non** | **non** |
| `0x004C1C30` | 5,6,7,8 | 0056AD48 | **non** | **non** |
| `0x004C2770` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004C3840` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004C4290` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004C4F90` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004C5A30` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004C6700` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004C7240` | 4,9 | 0056AD48 | **non** | **non** |
| `0x0040A600` | 0,1,2,3,0,1,2,3,5,6,7,8,0,1,2,3,5,6,7,8 | 0056ADC8 0056B0C8 0056B2C8 | oui | **non** |
| `0x00442E10` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004432A0` | 4,9 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004439A0` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004441F0` | 0,1,2,3,5,6,7,8 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 4,9 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0,1,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004734B0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746F0` | 0,1,2,3 | 0056ADC8 | oui | **non** |
| `0x004BC530` | 0,1,2,3 | 0056AD48 | oui | **non** |
| `0x004BE200` | 5,6,7,8 | 0056AD48 | oui | **non** |
| `0x005413E0` | 0,1,2,3,4,5,6,7,8,9 | 0056AFC8 | oui | **non** |

## Zone 6 — Le chateau de l'immortel

170 gestionnaires, 26 implementes, 0 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x004255F0` | 0,1,2,3,4 | 0056B0C8 | **non** | **non** |
| `0x00425750` | 0,1,2,3,4 | 0056B2C8 | **non** | **non** |
| `0x00425E50` | 0,1,2,3,4 | 0056B2C8 | **non** | **non** |
| `0x004263B0` | 0,1,2,3,4 | 0056B0C8 | **non** | **non** |
| `0x00426410` | 0,1,2,3,4 | 0056B2C8 | **non** | **non** |
| `0x00426860` | 0,1,10,2,3,4,6,7,8,9 | 0056B0C8 | **non** | **non** |
| `0x00426940` | 0,1,2,3,4 | 0056B2C8 | **non** | **non** |
| `0x00426B10` | 0,1,2,3,4 | 0056B0C8 | **non** | **non** |
| `0x00426C40` | 0,1,2,3,4 | 0056B2C8 | **non** | **non** |
| `0x00426D20` | 0,1,10,2,3,4,6,7,8,9 | 0056B1C8 | **non** | **non** |
| `0x00426E20` | 0,1,2,3,4 | 0056B1C8 | **non** | **non** |
| `0x00426E80` | 0,1,2,3,4 | 0056B2C8 | **non** | **non** |
| `0x00426FD0` | 0,1,2,3,4 | 0056B2C8 | **non** | **non** |
| `0x004270C0` | 0,1,2,3,4 | 0056B2C8 | **non** | **non** |
| `0x004271B0` | 10,6,7,8,9 | 0056B0C8 | **non** | **non** |
| `0x004274C0` | 10,6,7,8,9 | 0056B1C8 | **non** | **non** |
| `0x00427C10` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00427CE0` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00428420` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00428990` | 10,6,7,8,9 | 0056B0C8 | **non** | **non** |
| `0x004289F0` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00428E40` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00429010` | 10,6,7,8,9 | 0056B0C8 | **non** | **non** |
| `0x00429270` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00429330` | 10,6,7,8,9 | 0056B0C8 | **non** | **non** |
| `0x004293F0` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x004294D0` | 0,1,10,2,3,4,6,7,8,9 | 0056B0C8 | **non** | **non** |
| `0x00429680` | 10,6,7,8,9 | 0056B1C8 | **non** | **non** |
| `0x004296F0` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00429840` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00429930` | 10,6,7,8,9 | 0056B2C8 | **non** | **non** |
| `0x00429A20` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x00429BE0` | 11,5 | 0056B1C8 | **non** | **non** |
| `0x0042A350` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042A440` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042A540` | 11,5 | 0056B1C8 | **non** | **non** |
| `0x0042A920` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042ABF0` | 11,5 | 0056B1C8 | **non** | **non** |
| `0x0042B0C0` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042B190` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042B430` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042B9A0` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042BF10` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042BF70` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042C3C0` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042C9E0` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042D100` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042D130` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042D170` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042D350` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042D4E0` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042D770` | 11,5 | 0056B1C8 | **non** | **non** |
| `0x0042D9B0` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042DB10` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042DC80` | 11,5 | 0056B0C8 | **non** | **non** |
| `0x0042DF70` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x0042E060` | 11,5 | 0056B2C8 | **non** | **non** |
| `0x00443C60` | 10,6,7,8,9 | 0056AEC8 | **non** | **non** |
| `0x00444670` | 0,1,2,3,4 | 0056AEC8 | **non** | **non** |
| `0x004446F0` | 0,1,2,3,4 | 0056AEC8 | **non** | **non** |
| `0x00444A40` | 10,6,7,8,9 | 0056AEC8 | **non** | **non** |
| `0x00444AF0` | 10,6,7,8,9 | 0056AEC8 | **non** | **non** |
| `0x00444E00` | 0,1,10,2,3,4,6,7,8,9 | 0056AEC8 | **non** | **non** |
| `0x00444EF0` | 11,5 | 0056AEC8 | **non** | **non** |
| `0x004450A0` | 11,5 | 0056AEC8 | **non** | **non** |
| `0x00463770` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00463850` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00463A10` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00463AE0` | 0,1,10,2,3,4,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00463B30` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00463C80` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00463CA0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00463D90` | 0,1,10,2,3,4,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00463E80` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00463EF0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x004642C0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x004643B0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464440` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x004644F0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464660` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x004646A0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x004647F0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464900` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464A10` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464AC0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464B30` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464BC0` | 0,1,10,2,3,4,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00464C30` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464CF0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464DB0` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00464E70` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00465020` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00465070` | 0,1,2,3,4 | 0056ADC8 | **non** | **non** |
| `0x00465150` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465180` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465220` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465290` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465330` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x004653F0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x004654C0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465610` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465640` | 0,1,10,2,3,4,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465670` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x004656C0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x004657B0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465820` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465BF0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465CE0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465D70` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465E30` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465FA0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00465FE0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466130` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466240` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466350` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466400` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466470` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466500` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x004665C0` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466680` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466740` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466840` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466890` | 10,6,7,8,9 | 0056ADC8 | **non** | **non** |
| `0x00466970` | 11,5 | 0056ADC8 | **non** | **non** |
| `0x00466A10` | 11,5 | 0056ADC8 | **non** | **non** |
| `0x00466AA0` | 11,5 | 0056ADC8 | **non** | **non** |
| `0x004C7D00` | 0,1,2,3,4 | 0056AD48 | **non** | **non** |
| `0x004C9140` | 0,1,2,3,4 | 0056AD48 | **non** | **non** |
| `0x004C9E00` | 0,1,2,3,4 | 0056AD48 | **non** | **non** |
| `0x004CA9C0` | 0,1,2,3,4 | 0056AD48 | **non** | **non** |
| `0x004CB4F0` | 0,1,2,3,4 | 0056AD48 | **non** | **non** |
| `0x004CC650` | 0,1,2,3,4 | 0056AD48 | **non** | **non** |
| `0x004CD260` | 0,1,2,3,4 | 0056AD48 | **non** | **non** |
| `0x004CF3A0` | 10,6,7,8,9 | 0056AD48 | **non** | **non** |
| `0x004D12A0` | 10,6,7,8,9 | 0056AD48 | **non** | **non** |
| `0x004D2FC0` | 10,6,7,8,9 | 0056AD48 | **non** | **non** |
| `0x004D3EE0` | 10,6,7,8,9 | 0056AD48 | **non** | **non** |
| `0x004D4E10` | 11,5 | 0056AD48 | **non** | **non** |
| `0x004D60A0` | 11,5 | 0056AD48 | **non** | **non** |
| `0x004D7200` | 11,5 | 0056AD48 | **non** | **non** |
| `0x004D80D0` | 11,5 | 0056AD48 | **non** | **non** |
| `0x004D8F70` | 11,5 | 0056AD48 | **non** | **non** |
| `0x004D9D90` | 11,5 | 0056AD48 | **non** | **non** |
| `0x004DACC0` | 11,5 | 0056AD48 | **non** | **non** |
| `0x0040A600` | 0,1,10,2,3,4,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00442E10` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004431B0` | 0,1,2,3,4 | 0056AEC8 | oui | **non** |
| `0x004431F0` | 0,1,2,3,4 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004432A0` | 11,5 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004439A0` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004441F0` | 0,1,10,2,3,4,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 11,5 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004734B0` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1,10,2,3,4,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746F0` | 0,1,10,2,3,4,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004CE120` | 10,6,7,8,9 | 0056AD48 | oui | **non** |
| `0x004D01C0` | 10,6,7,8,9 | 0056AD48 | oui | **non** |
| `0x004D1F70` | 10,6,7,8,9 | 0056AD48 | oui | **non** |
| `0x005413E0` | 0,1,10,11,2,3,4,5,6,7,8,9 | 0056AFC8 | oui | **non** |

## Zone 7 — Les ruines souterraines

35 gestionnaires, 18 implementes, 0 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x0042E150` | 0,1,2 | 0056B0C8 | **non** | **non** |
| `0x0042E600` | 0,1,2 | 0056B2C8 | **non** | **non** |
| `0x0042E940` | 0,1,2 | 0056B2C8 | **non** | **non** |
| `0x0042EC30` | 0,1,2 | 0056B2C8 | **non** | **non** |
| `0x0042ED20` | 0,1,2 | 0056B1C8 | **non** | **non** |
| `0x0042EDB0` | 0,1,2 | 0056B2C8 | **non** | **non** |
| `0x0042EEA0` | 0,1,2 | 0056B2C8 | **non** | **non** |
| `0x0042EF90` | 0,1,2 | 0056B0C8 | **non** | **non** |
| `0x004450E0` | 0,1,2 | 0056AEC8 | **non** | **non** |
| `0x00445120` | 0,1,2 | 0056AEC8 | **non** | **non** |
| `0x00466BE0` | 0,1,2 | 0056ADC8 | **non** | **non** |
| `0x004DC890` | 0,1,2 | 0056AD48 | **non** | **non** |
| `0x004DCF10` | 0,1,2 | 0056AD48 | **non** | **non** |
| `0x004DDE30` | 0,1,2 | 0056AD48 | **non** | **non** |
| `0x004DEA40` | 0,1,2 | 0056AD48 | **non** | **non** |
| `0x004DF680` | 0,1,2 | 0056AD48 | **non** | **non** |
| `0x00541C90` | 0,1,2 | 0056AFC8 | **non** | **non** |
| `0x00442E10` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x004441F0` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0,1,2 | 0056AEC8 | oui | **non** |
| `0x004734B0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1,2 | 0056ADC8 | oui | **non** |
| `0x004DB610` | 0,1,2 | 0056AD48 | oui | **non** |
| `0x005413E0` | 0,1,2 | 0056AFC8 | oui | **non** |

## Zone 8 — La vallee d'argent

46 gestionnaires, 18 implementes, 0 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x0042F2A0` | 0,1 | 0056B0C8 | **non** | **non** |
| `0x0042F3B0` | 0,1 | 0056B1C8 | **non** | **non** |
| `0x0042F620` | 0,1 | 0056B0C8 | **non** | **non** |
| `0x0042F860` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x0042F970` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x0042FA10` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x0042FB50` | 0,1 | 0056B1C8 | **non** | **non** |
| `0x0042FC10` | 0,1 | 0056B1C8 | **non** | **non** |
| `0x0042FCC0` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x0042FDB0` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x0042FF50` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x00430040` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x004301E0` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x00430360` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x00445150` | 0,1 | 0056AEC8 | **non** | **non** |
| `0x00466C30` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00466C70` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00466DB0` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00466DF0` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00466E60` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00466ED0` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00466F50` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00467280` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00467440` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x004DFB40` | 0,1 | 0056AD48 | **non** | **non** |
| `0x004E0B10` | 0,1 | 0056AD48 | **non** | **non** |
| `0x004E1380` | 0,1 | 0056AD48 | **non** | **non** |
| `0x004E20F0` | 0,1 | 0056AD48 | **non** | **non** |
| `0x00442E10` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0,1 | 0056AEC8 | oui | **non** |
| `0x004432A0` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0,1 | 0056AEC8 | oui | **non** |
| `0x004441F0` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0,1 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0,1 | 0056AEC8 | oui | **non** |
| `0x004734B0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x005413E0` | 0,1 | 0056AFC8 | oui | **non** |

## Zone 9 — Les ruines dans le lac

41 gestionnaires, 18 implementes, 0 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x00430950` | 0,1 | 0056B0C8 | **non** | **non** |
| `0x00430C40` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x00430E20` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x00430F10` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x00431000` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x00431170` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x004312E0` | 0,1 | 0056B2C8 | **non** | **non** |
| `0x004451B0` | 0,1 | 0056AEC8 | **non** | **non** |
| `0x00445250` | 0,1 | 0056AEC8 | **non** | **non** |
| `0x004452D0` | 0,1 | 0056AEC8 | **non** | **non** |
| `0x00445360` | 0,1 | 0056AEC8 | **non** | **non** |
| `0x00467C10` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00467DB0` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00467E80` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x004681A0` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x00468210` | 0,1 | 0056ADC8 | **non** | **non** |
| `0x004844A0` | 0,1 | 0056B0C8 | **non** | **non** |
| `0x00484560` | 0,1 | 0056B1C8 | **non** | **non** |
| `0x004E2D60` | 0,1 | 0056AD48 | **non** | **non** |
| `0x004E37D0` | 0,1 | 0056AD48 | **non** | **non** |
| `0x004E40E0` | 0,1 | 0056AD48 | **non** | **non** |
| `0x004E4890` | 0,1 | 0056AD48 | **non** | **non** |
| `0x004E4F20` | 0,1 | 0056AD48 | **non** | **non** |
| `0x00442E10` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0,1 | 0056AEC8 | oui | **non** |
| `0x004432A0` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0,1 | 0056AEC8 | oui | **non** |
| `0x004441F0` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0,1 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0,1 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0,1 | 0056AEC8 | oui | **non** |
| `0x004734B0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1 | 0056ADC8 | oui | **non** |
| `0x005413E0` | 0,1 | 0056AFC8 | oui | **non** |

## Zone 10 — La grotte scellee

349 gestionnaires, 38 implementes, 0 testes.

| gestionnaire | sous-zones | tables | implemente | teste |
|---|---|---|---|---|
| `0x004314C0` | 0 | 0056B0C8 | **non** | **non** |
| `0x00431600` | 0 | 0056B2C8 | **non** | **non** |
| `0x00431870` | 0 | 0056B2C8 | **non** | **non** |
| `0x00431A20` | 0 | 0056B0C8 | **non** | **non** |
| `0x00431AB0` | 0 | 0056B2C8 | **non** | **non** |
| `0x00431C30` | 0 | 0056B1C8 | **non** | **non** |
| `0x00431D80` | 0 | 0056B1C8 | **non** | **non** |
| `0x00431F20` | 0 | 0056B2C8 | **non** | **non** |
| `0x004320B0` | 0 | 0056B2C8 | **non** | **non** |
| `0x00432190` | 0 | 0056B2C8 | **non** | **non** |
| `0x00432240` | 0 | 0056B2C8 | **non** | **non** |
| `0x00432390` | 0,10,2,3,5,8 | 0056B0C8 | **non** | **non** |
| `0x00432520` | 0 | 0056B2C8 | **non** | **non** |
| `0x00432630` | 0 | 0056B2C8 | **non** | **non** |
| `0x004327A0` | 0 | 0056B2C8 | **non** | **non** |
| `0x004328E0` | 0 | 0056B2C8 | **non** | **non** |
| `0x004329D0` | 0 | 0056B2C8 | **non** | **non** |
| `0x00432C90` | 0 | 0056B2C8 | **non** | **non** |
| `0x00432D80` | 0 | 0056B2C8 | **non** | **non** |
| `0x00432EA0` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433050` | 2,3 | 0056B0C8 | **non** | **non** |
| `0x004330E0` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433260` | 2,3 | 0056B1C8 | **non** | **non** |
| `0x004333B0` | 0,10,2,3,5,8 | 0056B0C8 | **non** | **non** |
| `0x00433440` | 2,3 | 0056B1C8 | **non** | **non** |
| `0x004335E0` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433770` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433850` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433900` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433A50` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433B60` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433CD0` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433E10` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x00433F00` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x004341C0` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x004342B0` | 2,3 | 0056B2C8 | **non** | **non** |
| `0x004343D0` | 10,5,8 | 0056B0C8 | **non** | **non** |
| `0x00434510` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00434790` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00434940` | 10,5,8 | 0056B1C8 | **non** | **non** |
| `0x00434A90` | 10,5,8 | 0056B1C8 | **non** | **non** |
| `0x00434BE0` | 10,5,8 | 0056B1C8 | **non** | **non** |
| `0x00434C80` | 10,5,8 | 0056B0C8 | **non** | **non** |
| `0x00434D10` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00434E90` | 10,5,8 | 0056B1C8 | **non** | **non** |
| `0x00434FE0` | 10,5,8 | 0056B1C8 | **non** | **non** |
| `0x00435180` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00435310` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x004353F0` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x004354A0` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x004355F0` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00435700` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00435870` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x004359B0` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00435AA0` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00435D60` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00435E50` | 10,5,8 | 0056B2C8 | **non** | **non** |
| `0x00435F70` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00436070` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00436790` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00436950` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00436FA0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x004370E0` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x004372B0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00437630` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00437790` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00437AA0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00437BB0` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00437C80` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00437DD0` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00438260` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00438380` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x004384F0` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00438710` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00438850` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x004389F0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00438AE0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00438BD0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00438CF0` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x00438EA0` | 6,7 | 0056B0C8 | **non** | **non** |
| `0x00439170` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x004396E0` | 6,7 | 0056B1C8 | **non** | **non** |
| `0x00439B00` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x00439BD0` | 6,7 | 0056B0C8 | **non** | **non** |
| `0x00439FA0` | 6,7 | 0056B0C8 | **non** | **non** |
| `0x0043A160` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043A440` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043A600` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043A730` | 6,7 | 0056B0C8 | **non** | **non** |
| `0x0043A860` | 6,7 | 0056B1C8 | **non** | **non** |
| `0x0043A8E0` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043AA90` | 6,7 | 0056B0C8 | **non** | **non** |
| `0x0043ABF0` | 6,7 | 0056B1C8 | **non** | **non** |
| `0x0043ADC0` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043AEF0` | 6,7 | 0056B0C8 | **non** | **non** |
| `0x0043AF70` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043B060` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043B1D0` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043B310` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043B400` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043B660` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043B750` | 6,7 | 0056B2C8 | **non** | **non** |
| `0x0043B870` | 1 | 0056B2C8 | **non** | **non** |
| `0x0043BDD0` | 1 | 0056B2C8 | **non** | **non** |
| `0x0043BF20` | 1 | 0056B0C8 | **non** | **non** |
| `0x0043BF50` | 1 | 0056B1C8 | **non** | **non** |
| `0x0043C010` | 1 | 0056B2C8 | **non** | **non** |
| `0x0043C120` | 1 | 0056B2C8 | **non** | **non** |
| `0x0043C380` | 11,12 | 0056B0C8 | **non** | **non** |
| `0x0043C4A0` | 11,12 | 0056B1C8 | **non** | **non** |
| `0x0043C880` | 11,12 | 0056B2C8 | **non** | **non** |
| `0x0043C900` | 11,12 | 0056B0C8 | **non** | **non** |
| `0x0043CBE0` | 11,12 | 0056B2C8 | **non** | **non** |
| `0x0043CD70` | 11,12 | 0056B1C8 | **non** | **non** |
| `0x0043CE30` | 11,12 | 0056B1C8 | **non** | **non** |
| `0x0043CEE0` | 11,12 | 0056B0C8 | **non** | **non** |
| `0x0043D190` | 11,12 | 0056B2C8 | **non** | **non** |
| `0x0043D3A0` | 11,12 | 0056B2C8 | **non** | **non** |
| `0x0043D4D0` | 11,12 | 0056B0C8 | **non** | **non** |
| `0x0043D850` | 11,12 | 0056B1C8 | **non** | **non** |
| `0x0043DA60` | 11,12 | 0056B2C8 | **non** | **non** |
| `0x0043DB30` | 11,12 | 0056B0C8 | **non** | **non** |
| `0x0043DE50` | 11,12 | 0056B2C8 | **non** | **non** |
| `0x0043DF60` | 11,12 | 0056B1C8 | **non** | **non** |
| `0x0043E030` | 11,12 | 0056B2C8 | **non** | **non** |
| `0x0043E690` | 11,12 | 0056B2C8 | **non** | **non** |
| `0x0043E820` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x0043EB60` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x0043EC60` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x0043F320` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x0043F4E0` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x0043FA90` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x0043FB80` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00440060` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x004403B0` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x004406B0` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x004407B0` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00440840` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00440F00` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x00441040` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x00441180` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x004412D0` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00441450` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x004414E0` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00441570` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00441670` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x004417F0` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00441B80` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00441C30` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00441DB0` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00441EB0` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x004421B0` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x00442290` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x004423D0` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x004424B0` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00442870` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00442AC0` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x00442C20` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x004453E0` | 2,3 | 0056AEC8 | **non** | **non** |
| `0x004454C0` | 10,5,8 | 0056AEC8 | **non** | **non** |
| `0x004454E0` | 10,5,8 | 0056AEC8 | **non** | **non** |
| `0x00445610` | 10,5,8 | 0056AEC8 | **non** | **non** |
| `0x00445740` | 4,9 | 0056AEC8 | **non** | **non** |
| `0x00445760` | 6,7 | 0056AEC8 | **non** | **non** |
| `0x004457A0` | 6,7 | 0056AEC8 | **non** | **non** |
| `0x004457E0` | 1 | 0056AEC8 | **non** | **non** |
| `0x00445810` | 1 | 0056AEC8 | **non** | **non** |
| `0x00461BE0` | 4,6,7,9 | 0056ADC8 | **non** | **non** |
| `0x00468580` | 0 | 0056ADC8 | **non** | **non** |
| `0x00468810` | 0 | 0056ADC8 | **non** | **non** |
| `0x00468850` | 0 | 0056ADC8 | **non** | **non** |
| `0x00468950` | 0 | 0056ADC8 | **non** | **non** |
| `0x00468A20` | 0 | 0056ADC8 | **non** | **non** |
| `0x00468AC0` | 0 | 0056ADC8 | **non** | **non** |
| `0x00468E30` | 0 | 0056ADC8 | **non** | **non** |
| `0x00469100` | 0 | 0056ADC8 | **non** | **non** |
| `0x00469480` | 0 | 0056ADC8 | **non** | **non** |
| `0x00469930` | 0 | 0056ADC8 | **non** | **non** |
| `0x004699F0` | 0 | 0056ADC8 | **non** | **non** |
| `0x00469AF0` | 0 | 0056ADC8 | **non** | **non** |
| `0x00469BC0` | 0 | 0056ADC8 | **non** | **non** |
| `0x00469F60` | 0 | 0056ADC8 | **non** | **non** |
| `0x0046A780` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046A810` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046A9E0` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046ABC0` | 0,2,3 | 0056ADC8 | **non** | **non** |
| `0x0046AC50` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046AD70` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046B340` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046B440` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046B570` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046B610` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046B6E0` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046BA50` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046BCD0` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046BE10` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046BF80` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046C020` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046C160` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046C260` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046C2F0` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046C3B0` | 2,3 | 0056ADC8 | **non** | **non** |
| `0x0046C540` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046C690` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046C800` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046C830` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046CA80` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046CC60` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046CE60` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046CF10` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046D070` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046D640` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046D760` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046D860` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046DBD0` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046DD90` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046DE80` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046DFC0` | 10,5,8 | 0056ADC8 | **non** | **non** |
| `0x0046E070` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x0046E100` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x0046E1C0` | 4,9 | 0056ADC8 | **non** | **non** |
| `0x0046E2C0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046E360` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046E410` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046E610` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046E6D0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046E770` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046E890` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046EA40` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046EB10` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046EDE0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046F070` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046F180` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046F1C0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046F2E0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046F8B0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046F9D0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046FAD0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x0046FE40` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x004700C0` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x00470130` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x00470190` | 6,7 | 0056ADC8 | **non** | **non** |
| `0x00470260` | 1 | 0056ADC8 | **non** | **non** |
| `0x004702D0` | 1 | 0056ADC8 | **non** | **non** |
| `0x00470640` | 1 | 0056ADC8 | **non** | **non** |
| `0x00470750` | 1 | 0056ADC8 | **non** | **non** |
| `0x00470860` | 1 | 0056ADC8 | **non** | **non** |
| `0x00470900` | 1 | 0056ADC8 | **non** | **non** |
| `0x00470A00` | 1 | 0056ADC8 | **non** | **non** |
| `0x00470B00` | 1 | 0056ADC8 | **non** | **non** |
| `0x00470BE0` | 1 | 0056ADC8 | **non** | **non** |
| `0x00470C20` | 1 | 0056ADC8 | **non** | **non** |
| `0x00471150` | 1 | 0056ADC8 | **non** | **non** |
| `0x00484760` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00484860` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00484AB0` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00484BB0` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00484CD0` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00484DA0` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00484E80` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00484F80` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x004850C0` | 4,9 | 0056B2C8 | **non** | **non** |
| `0x00485220` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x00485330` | 4,9 | 0056B1C8 | **non** | **non** |
| `0x00485450` | 4,9 | 0056B0C8 | **non** | **non** |
| `0x004855F0` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00485680` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00485760` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00485850` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00485940` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00485A40` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00485C00` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00485EC0` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00485F50` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00486240` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00486320` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x00486680` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00486720` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x004868E0` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x004869D0` | 13,14,15 | 0056B2C8 | **non** | **non** |
| `0x00486B10` | 13,14,15 | 0056B0C8 | **non** | **non** |
| `0x00486D60` | 13,14,15 | 0056B1C8 | **non** | **non** |
| `0x004E56C0` | 0 | 0056AD48 | **non** | **non** |
| `0x004E7810` | 0 | 0056AD48 | **non** | **non** |
| `0x004E84E0` | 0 | 0056AD48 | **non** | **non** |
| `0x004E9DB0` | 0 | 0056AD48 | **non** | **non** |
| `0x004EA430` | 2,3 | 0056AD48 | **non** | **non** |
| `0x004EC580` | 2,3 | 0056AD48 | **non** | **non** |
| `0x004ED250` | 2,3 | 0056AD48 | **non** | **non** |
| `0x004EEB20` | 10,5,8 | 0056AD48 | **non** | **non** |
| `0x004EFC20` | 10,5,8 | 0056AD48 | **non** | **non** |
| `0x004F0530` | 10,5,8 | 0056AD48 | **non** | **non** |
| `0x004F14F0` | 10,5,8 | 0056AD48 | **non** | **non** |
| `0x004F40A0` | 10,5,8 | 0056AD48 | **non** | **non** |
| `0x004F4FD0` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004F6740` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004F6E60` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004F7980` | 4,9 | 0056AD48 | **non** | **non** |
| `0x004F95F0` | 6,7 | 0056AD48 | **non** | **non** |
| `0x004FABF0` | 6,7 | 0056AD48 | **non** | **non** |
| `0x004FBA80` | 6,7 | 0056AD48 | **non** | **non** |
| `0x004FD700` | 1 | 0056AD48 | **non** | **non** |
| `0x004FDFE0` | 1 | 0056AD48 | **non** | **non** |
| `0x004FF500` | 1 | 0056AD48 | **non** | **non** |
| `0x004FFC50` | 1 | 0056AD48 | **non** | **non** |
| `0x005003E0` | 11,12 | 0056AD48 | **non** | **non** |
| `0x00500E10` | 11,12 | 0056AD48 | **non** | **non** |
| `0x005017C0` | 11,12,13,14,15 | 0056AD48 | **non** | **non** |
| `0x00501840` | 13,14,15 | 0056AD48 | **non** | **non** |
| `0x00502210` | 13,14,15 | 0056AD48 | **non** | **non** |
| `0x00502CD0` | 13,14,15 | 0056AD48 | **non** | **non** |
| `0x0040A600` | 13,14,15,13,14,15,4,9 | 0056B0C8 0056B2C8 | oui | **non** |
| `0x004380C0` | 4,9 | 0056B2C8 | oui | **non** |
| `0x00439D30` | 6,7 | 0056B2C8 | oui | **non** |
| `0x0043E170` | 11,12 | 0056B0C8 | oui | **non** |
| `0x00442E10` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00443100` | 10,5,8 | 0056AEC8 | oui | **non** |
| `0x004431F0` | 4,6,7,9 | 0056AEC8 | oui | **non** |
| `0x00443260` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004432A0` | 4,6,7,9 | 0056AEC8 | oui | **non** |
| `0x00443830` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004441F0` | 0,10,2,3,5,8 | 0056AEC8 | oui | **non** |
| `0x00444E60` | 11,12,13,14,15,4,9 | 0056AEC8 | oui | **non** |
| `0x00445480` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004457F0` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x00445840` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056AEC8 | oui | **non** |
| `0x004734B0` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004736D0` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004738A0` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A70` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473A90` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473CB0` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00473ED0` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00474380` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x004746C0` | 0,1,10,11,12,13,14,15,2,3,4,5,6,7,8,9 | 0056ADC8 | oui | **non** |
| `0x00482FF0` | 11,12,13,14,15,4,9 | 0056B1C8 | oui | **non** |
| `0x00483B50` | 11,12,4,9 | 0056B0C8 | oui | **non** |
| `0x00486DE0` | 13,14,15 | 0056B0C8 | oui | **non** |
| `0x004E67C0` | 0 | 0056AD48 | oui | **non** |
| `0x004E8DF0` | 0 | 0056AD48 | oui | **non** |
| `0x004EB530` | 2,3 | 0056AD48 | oui | **non** |
| `0x004EDB60` | 2,3 | 0056AD48 | oui | **non** |
| `0x004F23D0` | 10,5,8 | 0056AD48 | oui | **non** |
| `0x004F3350` | 10,5,8 | 0056AD48 | oui | **non** |
| `0x004F5630` | 4,9 | 0056AD48 | oui | **non** |
| `0x004F8270` | 6,7 | 0056AD48 | oui | **non** |
| `0x004F9C70` | 6,7 | 0056AD48 | oui | **non** |
| `0x004FC9B0` | 6,7 | 0056AD48 | oui | **non** |
| `0x005413E0` | 0,1,10,2,3,4,5,6,7,8,9 | 0056AFC8 | oui | **non** |

---

## Les tables

| table | qui la lit |
|---|---|
| `0x0056AD48` | `FUN_0050e680`, les entites de scene — **l'intelligence des especes** |
| `0x0056ADC8` | `FUN_0050e5e0` (table `0x005fa5e0`) et `FUN_0047aa70` au-dela de `0xc0` |
| `0x0056AEC8` | `ScriptOpAreaVerb`, **les verbes de zone**, et `FUN_0047ab70` au-dela de `0xc0` |
| `0x0056AFC8` | `FUN_0047acf0` (table `0x005f2d40`) au-dela de `0xc0` |
| `0x0056B0C8` | le debordement de `0x0056ADC8` |
| `0x0056B1C8` | le debordement de `0x0056AEC8` |
| `0x0056B2C8` | le debordement de `0x0056AFC8` |
| `0x00551C30` | la table **statique** de `FUN_0047aa70` |
| `0x00551F30` | la table statique de `FUN_0047ab70` |
| `0x00552230` | la table statique de `FUN_0047acf0` |
