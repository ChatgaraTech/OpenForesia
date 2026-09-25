# Ce qu'utiliser un objet peut atteindre

**Genere** — ne pas editer a la main. Regenerer par :

```sh
sh OpenForesia.Tools/census-item-effects.sh > Documentation/Etat/ObjetsAtteignables.md
```

Obtenu en deroulant hors ligne les deux scripts des 2 370 fiches d'objet du CD
(`Documentation/Reference/Bestiaire/objets.csv`, colonne `octets`). Aucune partie
n'a ete jouee pour le produire : les scripts sont de la donnee.

## Pourquoi on peut s'y fier

Le derouleur a ete verifie contre la realite avant d'etre cru : la **Petite potion**
(zone 0, fiche 0) se decode en

```
0090  11  cible-recherche   00        <- le mode 0, celui du meme camp
0092  02  saut-si           95 00
0095  00  consommer         64        <- cent pour cent
0097  0a  naitre-cible      40 00 01  <- sous-type 0x40 -> 0x00446790
009b  0b  alu-banque        20 4b 29
009f  0b  alu-banque        20 41 28
00a3  06  alu-stats-cible   4c 59 20  <- +0x4c, LES POINTS DE VIE
00a7  14  particule-cible   01 20 01
00ab  06  alu-stats-cible   48 4c 20
00af  06  alu-stats-cible   4c 75 20
00b3  06  alu-stats-cible   4c 58 20
00b7  ff  FIN
```

Ce sont **exactement les deux levees rencontrees en jeu le 2026-08-30, dans cet
ordre** : l'opcode 17 d'abord, puis le sous-type `0x40`. Le derouleur les avait
toutes deux, sans qu'on ait eu besoin de jouer.

**Et il corrige une idee fausse au passage.** Une potion ne soigne pas par la table des
48 effets : elle soigne par l'**opcode 6**, l'unite arithmetique, sur le champ
`+0x4c` du bloc de la cible. C'est pourquoi le genre 43 — la guerison — n'apparait
pas dans le tableau des effets plus bas. La table des 48 sert aux **alterations
d'etat** et aux guerisons des sorts et des pieges, pas au soin d'une potion.

---

## Les gestionnaires d'entite que les objets font naitre

Colonne **emplois** : le nombre de sites d'appel dans l'ensemble des scripts, pas le
nombre d'objets. C'est l'ordre de travail — le plus employe d'abord.

| gestionnaire | emplois | sous-type | implemente | teste | zones |
|---|---:|---|---|---|---|
| `0x00446790` | 259 | 0x40 | oui | oui | 0,1,3,4,5,6,7,8,10 |
| `0x004753D0` | 215 | 0xbd | oui | oui | 0,1,2,3,4,5,6,7,8,9,10 |
| `0x00446DD0` | 189 | 0x3e | oui | oui | 0,1,3,4,5,6,9,10 |
| `0x00445F60` | 166 | 0x44 | oui | oui | 0,1,3,4,5,6,7,8,9,10 |
| `0x004462B0` | 162 | 0x43 | oui | oui | 0,1,2,3,4,5,6,7,9,10 |
| `0x00446900` | 110 | 0x41 | oui | oui | 0,1,2,3,4,5,6,9,10 |
| `0x00446F30` | 95 | 0x3d | oui | oui | 0,1,2,3,4,5,6,10 |
| `0x00476500` | 75 | 0x5b | oui | oui | 0,1,3,4,5,6,7,10 |
| `0x00446200` | 36 | 0x45 | oui | oui | 0,1,3,4,5,6,10 |
| `0x00446410` | 25 | 0x42 | oui | oui | 0,1,3,4,5,6,10 |
| `0x00476350` | 17 | 0x5a | oui | oui | 0,1,4,5,7,10 |
| `0x00445E70` | 17 | 0x48 | oui | oui | 0,3,4,6 |
| `0x00445CF0` | 15 | 0x4c | oui | oui | 0,1,3,5,6,10 |
| `0x00447630` | 14 | 0x3b | oui | **non** | 0,1,3,4,5,10 |
| `0x0047A100` | 10 | 0x49 | oui | oui | 0,4,6 |
| `0x00446170` | 10 | 0x4d | oui | oui | 0,1,4,10 |
| `0x00404920` | 10 | 0x55 | oui | oui | 0,6,10 |
| `0x00475CC0` | 8 | 0x4e | oui | oui | 0,1,10 |
| `0x00446A90` | 4 | 0x3f | oui | **non** | 0,5,10 |
| `0x004477D0` | 2 | 0x4b | oui | oui | 0,2 |
| `0x00447170` | 1 | 0x3c | oui | **non** | 1 |

---

## Le compte

| | |
|---|---|
| gestionnaires distincts atteignables par un objet | **21** |
| deja ecrits | **21** |
| a transcrire | **0** |
| exerces en jeu (sonde des gestionnaires) | **18** |
| naissances resolues par la table STATIQUE | **1440** |
| naissances resolues par une table de SOUS-ZONE | **0** |

**La derniere ligne est le resultat le plus utile du releve.** Aucune naissance
d'objet n'emploie un sous-type superieur ou egal a `0xc0` : tout passe par la table
statique `0x00551c30`, la meme dans les onze zones. **Un objet fait donc exactement la
meme chose ou qu'on soit** — ecrire ces corps une fois les couvre partout, et la
colonne `zones` ne dit que dans quels lieux l'objet se trouve, pas de quoi depend son
comportement. La branche par sous-zone reste dans le script parce que c'est la regle
du binaire ; elle ne se declenche simplement jamais ici.

---

## Les objets, un par un

Meme lecture que `GestionnairesEntites.md`, mais sur les objets et non sur les
gestionnaires. **Les deux colonnes sont independantes** :

| colonne | ce qu'elle dit | d'ou elle vient |
|---|---|---|
| **implemente** | tous les corps que ses scripts font naitre existent | le releve croise avec les `case` du repartiteur |
| **teste** | l'objet a reellement ete employe en jeu | la sonde d'objets, `objets-exerces.tsv` |

`implemente = ?` veut dire : tout ce qui est resolvable est ecrit, **mais le script
emploie l'opcode 14**, dont le sous-type engendre vit dans le repertoire des sorts et
n'est donc pas resolu ici. Ces objets peuvent encore lever une fois en jeu.

Un objet **sans naissance** est implemente par construction : son script ne fait
appel qu'a des opcodes tous transcrits.

**Les noms illisibles sont dans la donnee, pas dans ce rapport.** Le disque francais
porte encore des fiches nommees dans le jeu de caracteres d'origine (katakana
demi-chasse), qu'`objets.csv` rend octet par octet. Ils sont recopies tels quels ici :
les corriger demanderait de savoir quelle table de caracteres le jeu applique a
l'affichage, ce qui n'est pas etabli. L'identite d'une ligne ne depend de toute
facon pas de son nom mais de sa **cle** — l'hexa des seize premiers octets de la
fiche —, la meme des deux cotes de la sonde.

| objet | gestionnaires requis | implemente | teste |
|---|---|---|---|
| ¸µò | — | ? | **non** |
| ¸ÏÃØ±Ù | `0x004462B0` | oui | **non** |
| ¸áï | — | oui | **non** |
| »¶èôÝ | `0x004753D0`, `0x00446410` | oui | **non** |
| »¶ï×¯ã | `0x004753D0`, `0x00446410` | oui | **non** |
| ½µò | — | ? | **non** |
| ½¼Ùï | `0x00446900` | oui | **non** |
| ½Ã¨±× | `0x00446DD0` | oui | **non** |
| ½ÏÃØ±Ù | `0x004462B0` | oui | **non** |
| ½ç­´Ù | `0x004753D0` | ? | **non** |
| ÀÑ¹÷ | — | oui | **non** |
| Á¬è±Û | — | oui | **non** |
| Á¬èáï | `0x00446DD0` | oui | **non** |
| ÓÝÒ²Ù | — | oui | **non** |
| ×±Ï | — | ? | **non** |
| Ø³«À | — | ? | **non** |
| Øµò | — | ? | **non** |
| ØÏÃØ±Ù | `0x004462B0` | oui | **non** |
| Ø×Ý÷ | `0x004753D0` | ? | **non** |
| ØçªÑ | `0x004753D0` | ? | **non** |
| ØÝãºÛÝ | `0x00445E70` | oui | **non** |
| Ù½ÄÝ | `0x004753D0`, `0x00446790` | oui | **non** |
| ÙÀØ½ÏÝ | — | oui | **non** |
| ÙÆïÙ | `0x004753D0`, `0x00446900` | oui | **non** |
| ÙÊÝÏ | — | oui | **non** |
| ÙÛò | — | oui | **non** |
| ÙÛ¯ï | — | oui | **non** |
| Ùù¼®Ý | `0x00446790` | oui | **non** |
| Ý±Ð­Ú¯Ä | — | oui | **non** |
| Ý¸×³Ý | — | oui | **non** |
| ÝãÛò | — | oui | **non** |
| à§Ã²Ý | — | ? | **non** |
| ò·¬Ýî¨ | `0x00475CC0` | oui | **non** |
| ÷Ø­Ä | `0x004753D0` | ? | **non** |
| Aiguille médicinale | `0x004753D0`, `0x00446900` | oui | oui |
| Aiguille médicinale | `0x004753D0`, `0x00446900` | oui | oui |
| Aile de Lucifer | `0x00446F30`, `0x00446170` | oui | oui |
| Aile de Séraphin | `0x00446900`, `0x00446200` | oui | oui |
| Aile de fée | `0x004753D0` | ? | oui |
| Almader | — | oui | oui |
| Ambrosia | `0x00476500` | oui | oui |
| Amulette de la lumière | `0x00446DD0` | oui | oui |
| Amulette des runes | — | oui | oui |
| Amulette du dragon | — | oui | oui |
| Anneau de Merlin | — | oui | oui |
| Anneau de Satan | `0x004753D0`, `0x00446F30` | oui | oui |
| Anneau de l'hermite | — | oui | oui |
| Anneau des philosophes | `0x00446900` | oui | oui |
| Anneau des éléments | — | oui | oui |
| Anneau du roi Salomon | — | oui | oui |
| Answerer | — | oui | oui |
| Aquachâle | `0x00446DD0` | oui | oui |
| Arc classique | — | oui | oui |
| Arc de bois | — | oui | oui |
| Arc de chasseur | — | oui | oui |
| Arc des elfes | — | oui | oui |
| Arc en croix | — | oui | oui |
| Arc gris | — | oui | oui |
| Arc noble | — | oui | oui |
| Armure | — | oui | oui |
| Armure anti-sortilège | — | oui | oui |
| Armure d'argent | — | oui | oui |
| Armure de Pawas | `0x00446DD0` | oui | oui |
| Armure de Satan | — | oui | oui |
| Armure de chevalier | — | oui | oui |
| Armure du dragon | — | oui | oui |
| Armure du gardien | — | oui | oui |
| Armure mirage | — | oui | oui |
| Armure noire | — | oui | oui |
| Armure sacrée | `0x00446DD0` | oui | oui |
| Artémis | — | oui | oui |
| Bague au joyau | — | oui | oui |
| Bague de protection | — | oui | oui |
| Bague de résistance | — | oui | oui |
| Bague réflectrice | — | oui | oui |
| Bague sacrée | — | oui | oui |
| Baguette aux joyaux | — | oui | oui |
| Baguette d'Ishtar | — | oui | oui |
| Baguette d'argent | — | oui | oui |
| Baguette de cristal | — | oui | oui |
| Baguette de l'air | — | oui | oui |
| Baguette de l'alchimiste | — | oui | oui |
| Baguette de l'arc-en-ciel | — | oui | oui |
| Baguette de l'eau | — | oui | oui |
| Baguette de la féerie | — | oui | oui |
| Baguette de la licorne | — | oui | oui |
| Baguette de la terre | — | oui | oui |
| Baguette des anges | — | oui | oui |
| Baguette des runes | — | oui | oui |
| Baguette du feu | — | oui | oui |
| Baguette du mal | — | oui | oui |
| Baguette guérisseuse | — | oui | oui |
| Baguette magique | — | oui | oui |
| Baguette spatiale | — | oui | oui |
| Baguette stellaire | — | oui | oui |
| Baguette-Misuril | — | oui | oui |
| Baguette-vampire | — | oui | oui |
| Bahamut | `0x004477D0` | oui | oui |
| Bahil | — | oui | oui |
| Balance des juges | `0x004753D0` | ? | oui |
| Balle de cristal | `0x00446790` | oui | oui |
| Balle de feu | — | ? | oui |
| Balle de gel | — | ? | oui |
| Balle de l'arc-en-ciel | — | ? | oui |
| Balle de l'illusion | `0x004753D0` | ? | oui |
| Balle de lumière | — | ? | oui |
| Balle de terre | — | ? | oui |
| Balle des ténèbres | — | ? | oui |
| Balle des éléments | — | ? | oui |
| Balle du tonnerre | — | ? | oui |
| Balle maléfique | — | ? | oui |
| Balle sacrée | — | ? | oui |
| Bannière de chevalier | — | oui | oui |
| Belladone | — | oui | oui (herbe, sonde des sorts) |
| Black Jack | `0x00447170` | oui | oui |
| Bonbon aux plantes | `0x00475CC0` | oui | oui |
| Bottes ailées | — | oui | oui |
| Bottes d'elfes | — | oui | oui |
| Bottes de cuir | — | oui | oui |
| Bottes de fer | — | oui | oui |
| Bottes de sept lieux | — | oui | oui |
| Boucle de cristal | — | oui | oui |
| Boucle de la chance | — | oui | oui |
| Bouclier d'argent | — | oui | oui |
| Bouclier de Bartschas | `0x00446DD0` | oui | oui |
| Bouclier de Lucifer | — | ? | oui |
| Bouclier de Séraphin | — | oui | oui |
| Bouclier de brigand | — | oui | oui |
| Bouclier de glace | `0x00446900` | oui | oui |
| Bouclier de l'air | `0x00446900` | oui | oui |
| Bouclier de l'ombre | — | oui | oui |
| Bouclier de la terre | `0x00446900` | oui | oui |
| Bouclier de vie | — | oui | oui |
| Bouclier des elfes | — | oui | oui |
| Bouclier des runes | — | oui | oui |
| Bouclier des élémentaux | `0x00446DD0` | oui | oui |
| Bouclier du feu | `0x00446900` | oui | oui |
| Bouclier du gardien | — | oui | oui |
| Bouclier lunaire | — | oui | oui |
| Bouclier rond | — | oui | oui |
| Bouclier réflecteur | — | oui | oui |
| Bouclier sacré | `0x00446DD0` | oui | oui |
| Bouclier solaire | — | oui | oui |
| Boîte de Pandore | `0x00446170`, `0x00475CC0` | oui | oui |
| Boîte du silence | `0x004753D0` | ? | oui |
| Boîte à bijoux | `0x00445F60` | oui | oui |
| Bracelet d'argent | — | oui | oui |
| Bracelet de reine | — | oui | oui |
| Broche au joyau | — | oui | oui |
| Broche d'argent | — | oui | oui |
| Bâton d'Apollon | — | oui | oui |
| Bâton de cristal | — | oui | oui |
| Bâton de fer | — | oui | oui |
| Bâton des sages | — | oui | oui |
| Bâton divin | — | ? | oui |
| Bâton lunaire | — | oui | oui |
| Bâton stellaire | — | ? | oui |
| Béquille | `0x00446F30` | oui | oui |
| Cape d'automne | — | oui | oui |
| Cape d'hiver | — | oui | oui |
| Cape d'été | — | oui | oui |
| Cape des runes | — | oui | oui |
| Cape du printemps | — | oui | oui |
| Carte de la peur | `0x00446F30`, `0x00446170` | oui | oui |
| Carte de tarot | — | ? | oui |
| Casque ailé | — | oui | oui |
| Casque d'argent | — | oui | oui |
| Casque de cuir | — | oui | oui |
| Casque de fer | — | oui | oui |
| Casque splendide | — | oui | oui |
| Casquette de chasseur | — | oui | oui |
| Casse-tête | — | oui | oui |
| Casse-tête à chaîne | — | oui | oui |
| Cendre blanche | `0x00445F60` | oui | oui |
| Cendre bleue | `0x00445F60` | oui | oui |
| Cendre grise | `0x00445F60` | oui | oui |
| Cendre noire | `0x00445F60` | oui | oui |
| Cendre rouge | `0x00445F60` | oui | oui |
| Cendre verte | `0x00445F60` | oui | oui |
| Chapeau d'elfe | — | oui | oui |
| Chapeau de sorcière | — | oui | oui |
| Chaussures à plumes | — | oui | oui |
| Chaussuresvol | `0x004753D0` | oui | oui |
| Chaîne de l'air | — | oui | oui |
| Chaîne de l'eau | — | oui | oui |
| Chaîne de la lumière | — | oui | oui |
| Chaîne de la terre | — | oui | oui |
| Chaîne des ténèbres | — | oui | oui |
| Chaîne du feu | — | oui | oui |
| Chaîne du mal | — | oui | oui |
| Chaîne du souffle | — | oui | oui |
| Chaîne sacrée | — | oui | oui |
| Chaînette d'argent | — | oui | oui |
| Chaînette de plumes | — | oui | oui |
| Châle précieux | — | oui | oui |
| Corde du retour | — | ? | oui |
| Corne du Minotaure | `0x00447630` | oui | oui |
| Corne du diable | — | oui | oui |
| Corne du dragon | — | oui | oui |
| Cotte de maille | — | oui | oui |
| Couronne de la lune | — | oui | oui |
| Couronne du soleil | — | oui | oui |
| Craimore | — | oui | oui |
| Cristal d'air | `0x004462B0` | oui | oui |
| Cristal d'eau | `0x004462B0` | oui | oui |
| Cristal d'ombre | `0x004462B0` | oui | oui |
| Cristal d'énergie | `0x004753D0` | ? | oui |
| Cristal de feu | `0x004462B0` | oui | oui |
| Cristal de lumière | `0x004462B0` | oui | oui |
| Cristal de magie | `0x004462B0` | oui | oui |
| Cristal de terre | `0x004462B0` | oui | oui |
| Cristal du mal | `0x004462B0` | oui | oui |
| Cristal saint | `0x004462B0` | oui | oui |
| Croix | — | oui | oui |
| Crosse d'ambre | — | oui | oui |
| Crosse de Séraphin | — | oui | oui |
| Crosse de voeux | — | oui | oui |
| Crosse des anciens | — | oui | oui |
| Crosse du diable | — | oui | oui |
| Crosse du jugement | — | oui | oui |
| Crosse sacrée | — | oui | oui |
| Cuir fin | — | oui | oui |
| Cuirasse au miroir | — | oui | oui |
| Cuirasse de bronze | — | oui | oui |
| Cuirasse de maître | — | oui | oui |
| Daak | — | oui | oui |
| Debacle | — | oui | oui |
| Dominique | — | oui | oui |
| Dropneal | — | oui | oui |
| Durandal | `0x00447630` | oui | oui |
| Délicieux drop | `0x00446900` | oui | oui |
| Eau bénite | — | ? | oui |
| Ecailles de Belzébuth | — | oui | oui |
| Ecailles de dragon | — | oui | oui |
| Ecu du destin | `0x00445F60` | oui | oui |
| Elixir de magicien | `0x00446790` | oui | oui |
| Elixir de vie | `0x00446790` | oui | oui |
| Elixir des esprits | `0x00446790` | oui | oui |
| Epice rouge | `0x00446F30` | oui | oui |
| Epée d'argent | — | oui | oui |
| Epée de Damas | — | oui | oui |
| Epée de feu | — | oui | oui |
| Epée de glace | — | oui | oui |
| Epée de la fatalité | `0x00446F30` | oui | oui |
| Epée de sang | — | oui | oui |
| Epée du bâtard | — | oui | oui |
| Epée du gardien | — | oui | oui |
| Epée du tonnerre | — | oui | oui |
| Epée gauloise | `0x00446A90` | oui | oui |
| Epée noire | — | oui | oui |
| Excalibur | — | oui | oui |
| Extrait d'esprits | `0x00445F60` | oui | oui |
| Extrait de sang | `0x00445F60` | oui | oui |
| Fabnihl | `0x00446F30` | oui | oui |
| Faux de la mort | `0x00404920` | oui | oui |
| Fenouil | — | oui | oui (herbe, sonde des sorts) |
| Fenril | — | oui | oui |
| Fine rapière | — | oui | oui |
| Flacon d'énergie | `0x004753D0`, `0x00446790`, `0x00446200` | oui | oui |
| Flèche de Persée | — | oui | oui |
| Flèche de faucon | `0x0047A100` | oui | oui |
| Flèche de l'ombre | — | oui | oui |
| Flèche de lumière | — | ? | oui |
| Flèche des Gouivres | `0x00446F30` | oui | oui |
| Flèche du vent | — | oui | oui |
| Flûte des moutons | `0x004753D0` | ? | oui |
| Forgée de glace | — | oui | oui |
| Franberg | — | oui | oui |
| Féline | `0x00446F30` | oui | oui |
| Gabriel | — | oui | oui |
| Gantelet de force | — | oui | oui |
| Gantelets | — | oui | oui |
| Gantelets ailés | — | oui | oui |
| Gantelets d'argent | — | oui | oui |
| Gantelets de chevaliers | — | oui | oui |
| Gants ailés | — | oui | oui |
| Gants de cuir | — | oui | oui |
| Gants de force | — | oui | oui |
| Gants des runes | — | oui | oui |
| Gants sacrés | — | oui | oui |
| Genocide | `0x00404920` | oui | oui |
| Glaive | — | oui | oui |
| Grand arc | — | oui | oui |
| Grande hache | — | oui | oui |
| Gulwick | `0x00446F30` | oui | oui |
| Hache d'Ouga | — | oui | oui |
| Hache d'argent | — | oui | oui |
| Hache de Mars | — | oui | oui |
| Hache de Viking | — | oui | oui |
| Hache de boucher | — | oui | oui |
| Hache de guerre | — | oui | oui |
| Hache de la sauvagerie | `0x004753D0`, `0x00447630`, `0x00446410` | oui | oui |
| Hache du diable | `0x004753D0`, `0x00446A90` | oui | oui |
| Hache du feu | — | oui | oui |
| Hache du gel | — | oui | oui |
| Hache empoisonnée | `0x00446F30` | oui | oui |
| Hache en demi-lune | — | oui | oui |
| Hache fine | — | oui | oui |
| Hachette | — | oui | oui |
| Hallebarde | — | oui | oui |
| Harpe d'or | — | oui | oui |
| Horloge des mystères | `0x004753D0` | ? | oui |
| Horloge du bonheur | `0x004753D0` | ? | oui |
| Huile de sang | `0x00445CF0` | oui | oui |
| Huile du pouvoir | `0x00445CF0` | oui | oui |
| Hysopius | — | oui | oui (herbe, sonde des sorts) |
| Insigne de brigand | — | oui | oui |
| Joyau de feu | `0x004753D0` | ? | oui |
| Joyau de glace | `0x004753D0` | ? | oui |
| Joyau de terre | `0x004753D0` | ? | oui |
| Joyau de vent | `0x004753D0` | ? | oui |
| La dévoreuse | — | oui | oui |
| La massacreuse | `0x00404920` | oui | oui |
| Lame de Calvin | — | oui | oui |
| Lame de la terre | — | oui | oui |
| Lame du soleil | — | oui | oui |
| Lampe sacrée | `0x004753D0` | ? | oui |
| Large épée | — | oui | oui |
| Lavande | — | oui | oui (herbe, sonde des sorts) |
| Livre de la technique | — | oui | oui |
| Long marteau | — | oui | oui |
| Longue épée | — | oui | oui |
| Manteau | — | oui | oui |
| Manteau d'elfe | — | oui | oui |
| Manteau de brigand | — | oui | oui |
| Manteau de croisé | — | oui | oui |
| Manteau de cuir | — | oui | oui |
| Manteau de résistance | — | oui | oui |
| Manteau des esprits | `0x00446DD0` | oui | oui |
| Manteau à plumes | — | oui | oui |
| Marteau de Thor | — | oui | oui |
| Marteau de guerre | — | oui | oui |
| Marteau de prêtre | — | oui | oui |
| Masque de chevalier | — | oui | oui |
| Masque de la mort | — | oui | oui |
| Masse d'armes | — | oui | oui |
| Mercuriale | — | oui | oui (herbe, sonde des sorts) |
| Minegosh | — | oui | oui |
| Miroir d'eau | — | oui | oui |
| Miroir de la porte | — | oui | oui |
| Mistortain | — | oui | oui |
| Médaillon d'argent | — | oui | oui |
| Médaillon d'or | — | oui | oui |
| Médaillon de bronze | — | oui | oui |
| Nobles bottes | — | oui | oui |
| Nobles chaussures | — | oui | oui |
| Os de guerrier sauvage | `0x004753D0`, `0x00446410` | oui | oui |
| Panoramix | `0x00446790`, `0x00476350` | oui | oui |
| Parfum d'impératrice | `0x00446790`, `0x00445E70` | oui | oui |
| Parfum de convalescence | `0x00445E70` | oui | oui |
| Parfum de feu | — | ? | oui |
| Parfum de guérison | `0x00445E70` | oui | oui |
| Pavois de chevalier | — | oui | oui |
| Pavois fin | — | oui | oui |
| Pendentif de princesse | — | oui | oui |
| Pendentif vaporeux | — | oui | oui |
| Pentacle de prince | — | oui | oui |
| Pentacle magique | — | oui | oui |
| Petite potion | `0x00446790` | oui | oui |
| Pierre magique | `0x004753D0`, `0x00446790` | oui | oui |
| Pierre médicinale | `0x004753D0`, `0x00446790` | oui | oui |
| Pique | — | oui | oui |
| Plastron | — | oui | oui |
| Plume | `0x00445F60` | oui | oui |
| Plume dominicale | `0x004753D0`, `0x00476500` | oui | oui |
| Poignard d'Orihalcon | — | oui | oui |
| Poignard d'argent | — | oui | oui |
| Poignard de brigand | — | oui | oui |
| Poignard de sang | — | oui | oui |
| Poignard des brouillards | — | oui | oui |
| Pointe de sang | `0x00445F60` | oui | oui |
| Porte-bonheur | — | oui | oui |
| Potion med | `0x00446790` | oui | oui |
| Potion olympique | `0x00446790`, `0x00446200` | oui | oui |
| Poudre de fées | — | ? | oui |
| Poudre de zombie | `0x004753D0`, `0x00476500` | oui | oui |
| Poupée Dummy | `0x004753D0`, `0x00476500` | oui | oui |
| Poupée dansante | `0x004753D0`, `0x00446790` | oui | oui |
| Queue de dragon | — | oui | oui |
| Rapière | — | oui | oui |
| Rapière Misuril | — | oui | oui |
| Rapière aux éclairs | — | oui | oui |
| Rapière d'argent | — | oui | oui |
| Rapière de Zeus | — | oui | oui |
| Rapière du chaos | `0x00446F30` | oui | oui |
| Rapière à poison | `0x00446F30` | oui | oui |
| Renegeton | — | oui | oui |
| Revatin | — | ? | oui |
| Robe de Saulus | `0x00446DD0` | oui | oui |
| Robe de fantôme | — | oui | oui |
| Robe de l'ombre | — | oui | oui |
| Robe de la terre | — | oui | oui |
| Robe de reine | — | oui | oui |
| Robe des anciens | — | oui | oui |
| Robe des esprits | — | oui | oui |
| Robe des runes | — | oui | oui |
| Robe du démon | — | oui | oui |
| Robe du gardien | `0x00446DD0` | oui | oui |
| Robe fine | — | oui | oui |
| Robe magique | — | oui | oui |
| Robe médicinale | — | oui | oui |
| Robe sacrée | — | oui | oui |
| Robe épiscopale | — | oui | oui |
| Ruban de force | — | oui | oui |
| Rubis | `0x00446900` | oui | oui |
| Réflector | — | oui | oui |
| Sabre de la terre | — | oui | oui |
| Saphir | `0x00446900` | oui | oui |
| Sauge | — | oui | oui (herbe, sonde des sorts) |
| Sauvagerie | `0x004753D0`, `0x00446410` | oui | oui |
| Sceptre de bataille | — | oui | oui |
| Sceptre de la mort | — | oui | oui |
| Simple bâton | — | oui | oui |
| Solitaire | — | oui | oui |
| Stylet de bague | — | oui | oui |
| Symbole de force | — | oui | oui |
| Symbole des ténébres | — | oui | oui |
| Table de vision | — | oui | oui |
| Table des anciens | — | oui | oui |
| Tablier de cuir | — | oui | oui |
| Talisman | — | oui | oui |
| Talisman médicinal | — | oui | oui |
| Tiare de la terre | `0x00446DD0` | oui | oui |
| Tiare des étoiles | `0x00446DD0` | oui | oui |
| Verveine | — | oui | oui (herbe, sonde des sorts) |
| Voile de beauté | — | oui | oui |
| Voile des ombres | `0x00446900` | oui | oui |
| Voile mystique | — | oui | oui |
| Walkyrie | — | oui | oui |
| Walmunk | — | oui | oui |
| ýòÂ | — | oui | **non** |
| ±²½¼Ùï | `0x00446900` | oui | **non** |
| ±²½ç­´Ù | `0x004753D0` | ? | **non** |
| ±¸±¼®Ù | `0x00446DD0` | oui | **non** |
| ±¸¾ÙòÂ | — | oui | **non** |
| ±Ù¹Ð½ÄÛ¯ï | — | oui | **non** |
| ±ÙÃÐ½ | — | oui | **non** |
| ±Ý½³ª× | — | oui | **non** |
| ±ÝòÛ¼± | `0x00476500` | oui | **non** |
| ²¼­ÀÙÛ¯ï | — | oui | **non** |
| ²ñÙµò | — | ? | **non** |
| ²ñÙÏÃØ±Ù | `0x004462B0` | oui | **non** |
| ³¨¯¼­ÊÝÏ | — | oui | **non** |
| ³¨¯ÁÊ¯Ä | — | oui | **non** |
| ³¨ÝÀ¹÷ | — | oui | **non** |
| ³¨ÝïÏÃØ±Ù | `0x004462B0` | oui | **non** |
| ³¨æïù¼®Ý | `0x00446790` | oui | **non** |
| ³«ÀÏÃØ±Ù | `0x004462B0` | oui | **non** |
| ³«ÀÐ× | — | oui | **non** |
| ³²ÝãòÂ | — | oui | **non** |
| ³²Ýïç­´Ù | `0x004753D0` | ? | **non** |
| ´±¼Ùï | `0x00446900` | oui | **non** |
| ´¸½¶Øð | — | oui | **non** |
| ´Åç¸Ø½ÀÙ | `0x004753D0` | ? | **non** |
| ´Åçç­´Ù | — | ? | **non** |
| ´ÅçôÄÙ | `0x004753D0`, `0x00446790`, `0x00446200` | oui | **non** |
| ´Ø¸¼Ù | `0x00446790`, `0x00476350` | oui | **non** |
| ´ÙàÝÊ¯Ä | — | oui | **non** |
| ´ÙòÝòÂ | — | oui | **non** |
| ´ÚÒÝÀÙµò | — | ? | **non** |
| ´ÚÒÝÀÙÏÝÄ | `0x00446DD0` | oui | **non** |
| ´ÚÒÝÀÙØÝã | — | oui | **non** |
| ´ÚÒÝÀÙáï | `0x00446DD0` | oui | **non** |
| ´Ý¼ªÝÄ±Ï | — | oui | **non** |
| ´Ý¼ªÝÄ½À¯Ì | — | oui | **non** |
| ´Ý¼ªÝÄÛò | `0x00446DD0` | oui | **non** |
| ´ÝÊÝ½Ûò | — | oui | **non** |
| ´ÝçªÙÛ¯ï | — | oui | **non** |
| ´Ý÷Ú½ºÛÝ | `0x00446790`, `0x00445E70` | oui | **non** |
| ¶µ½Ú²ö± | `0x00446F30` | oui | **non** |
| ·­±ù¼®Ý | `0x00446790`, `0x00446200` | oui | **non** |
| ¸Ø½ÀÙµò | `0x00446790` | oui | **non** |
| ¸Û½ | — | oui | **non** |
| ¹ÙñÑòÚï | — | oui | **non** |
| »²ÚÝÄô¯¸½ | `0x004753D0` | ? | **non** |
| »ÀÝØÝã | `0x004753D0`, `0x00446F30` | oui | **non** |
| »Ï¹÷ | — | oui | **non** |
| »Ýëµò | — | ? | **non** |
| ¼¬ï³Ûò | — | oui | **non** |
| ¼¬ï³óÙ | `0x00446900` | oui | **non** |
| ¼ÙðÍÙÑ | — | oui | **non** |
| ¼ÙðáÝÄÚ¯Ä | — | oui | **non** |
| ½ÀÃ¨±× | `0x00446DD0` | oui | **non** |
| ½ÛÝèÚ²ö± | — | oui | **non** |
| ½ÛÝèÛò | `0x00446DD0` | oui | **non** |
| ½öïºÛÝ | — | ? | **non** |
| ½öØ¯Ä´·½ | `0x00445F60` | oui | **non** |
| ½÷ØÝã¹÷ | — | oui | **non** |
| ¾×Ì¨Ñ³²Ýã | `0x00446900`, `0x00446200` | oui | **non** |
| ¾×Ì¨Ñ¼Ùï | — | oui | **non** |
| ¾×Ì¨ÑÒ²½ | — | oui | **non** |
| ¿³Ù²À | — | oui | **non** |
| ¿Ù¸×³Ý | — | oui | **non** |
| ¿Ùáï | — | oui | **non** |
| ¿ÛÓÝèØÝã | — | oui | **non** |
| ÀÛ¯Ä¶ï | — | ? | **non** |
| Ã×¶ï | `0x00446F30`, `0x00446170` | oui | **non** |
| Ä©ÙÐ× | — | oui | **non** |
| ÊÝÃ¨Ýã·¬¯÷ | — | oui | **non** |
| Ì§²±µò | — | ? | **non** |
| Ì§²±ç­´Ù | `0x004753D0` | ? | **non** |
| Ì§²Ýµ²Ù | `0x00445CF0` | oui | **non** |
| Ì§ÙºÝ±Û | `0x0047A100` | oui | **non** |
| Ì§ÝÀèÑµò | `0x004753D0` | ? | **non** |
| Ì§ÝÄÑÛò | — | oui | **non** |
| Ì¨Û¿ÊØÝã | `0x00446900` | oui | **non** |
| Ìª±Ø³²Ýã | `0x004753D0` | ? | **non** |
| Ìª±ØºÛÝ | — | ? | **non** |
| Ìª±ØÛ¯ï | — | oui | **non** |
| Ìª±ØïÛ¯÷ | `0x00445E70` | oui | **non** |
| Ìª±Øõ³ë | — | ? | **non** |
| Ìªæ¼­è | — | oui | **non** |
| Ìªæ¼®Ù | `0x00446DD0` | oui | **non** |
| ÌªæøÝ | `0x00445F60` | oui | **non** |
| Ì«Á­Ý¸Û¯¸ | `0x004753D0` | ? | **non** |
| ÌØèµò | — | ? | **non** |
| ÌÚ²Ñ¼Ùï | `0x00446900` | oui | **non** |
| ÌÚ²ÑÏÃØ±Ù | `0x004462B0` | oui | **non** |
| ÌÛÄ¼­è | `0x004753D0` | oui | **non** |
| ÎÜ²Ä±¯¼­ | `0x00445F60` | oui | **non** |
| Ï²Ýïù¼®Ý | `0x00446790` | oui | **non** |
| Ïç¯¸ÏÃØ±Ù | `0x004462B0` | oui | **non** |
| Ïç¯¸õÝÀ¸Ù | — | oui | **non** |
| Ïç¶Ù½ÄÝ | `0x004753D0`, `0x00446790` | oui | **non** |
| Ð½Ã¨¯¸óÙ | — | oui | **non** |
| Ð½ÃØ¸Û¯¸ | `0x004753D0` | ? | **non** |
| Ð½ÄøÝëÝÄ | — | oui | **non** |
| ÐÉÀ³Û½ÎÝ | `0x00447630` | oui | **non** |
| Ð×±Ï | — | oui | **non** |
| Ð×ç­¼Ùï | `0x00446DD0` | oui | **non** |
| Ð×¸Ù¸Ù$õ³õ³ë | `0x00476500` | oui | **non** |
| Ð×¸Ùõ³ë | `0x004753D0`, `0x00476500` | oui | **non** |
| ÒÃµ½À¯Ì | — | oui | **non** |
| ×²Ä±Ð­Ú¯Ä | `0x00446DD0` | oui | **non** |
| ×²Ä±Û | — | ? | **non** |
| ×²Äµò | — | ? | **non** |
| ×²ÄÏÃØ±Ù | `0x004462B0` | oui | **non** |
| ×²Ì²À | — | oui | **non** |
| ×²Ì¿ï | `0x00404920` | oui | **non** |
| ×²Ìù¼®Ý | `0x00446790` | oui | **non** |
| ×Ú²´±Û | `0x00446F30` | oui | **non** |
| Ø¶ðºÛÝ | `0x00445E70` | oui | **non** |
| ØÀÝÛ÷ | — | ? | **non** |
| ØÌÚ¯¸½ | — | oui | **non** |
| ØÌÚ¸Ä¼Ùï | — | oui | **non** |
| ØÌÚ¸ÄØÝã | — | oui | **non** |
| Ù¼Ì§³²Ýã | `0x00446F30`, `0x00446170` | oui | **non** |
| Ù¼Ì§¼Ùï | — | ? | **non** |
| Ú¯ï±¯¼­ | `0x00445F60` | oui | **non** |
| Ú¯ï±² | `0x00446F30` | oui | **non** |
| Ú¯ï½õ²½ | `0x00446F30` | oui | **non** |
| Ú¯ïö±½ | `0x00446900` | oui | **non** |
| Ú²Ýôµò | — | ? | **non** |
| Úç½ÄØÝã | — | oui | **non** |
| à§ÙÑÝ¸ | — | oui | **non** |
| à«õÙ±¸½ | `0x00404920` | oui | **non** |
| á²±½À¯Ì | — | ? | **non** |
| áØ±Ý¿ï | `0x00446A90` | oui | **non** |
| áòØ´Ù | — | oui | **non** |
| ãØÝ±¯¼­ | `0x00445F60` | oui | **non** |
| ãÙàª²ã | `0x00446F30` | oui | **non** |
| ãÚ²±¸ | — | oui | **non** |
| ãÚ²±¯¼­ | `0x00445F60` | oui | **non** |
| ç¬¯çÒÝÄÒ²½ | — | oui | **non** |
| ç¬¯çð×Ý½ | `0x004753D0` | ? | **non** |
| ç­´Ùô¯¸½ | `0x00445F60` | oui | **non** |
| ëÐïÙ | `0x004753D0`, `0x00476500` | oui | **non** |
| ëÝ¼ÝãïÙ | `0x004753D0`, `0x00446790` | oui | **non** |
| î¨½Ã¨Æº²Ý | `0x00445F60` | oui | **non** |
| î¨ð²Ý±Ï | `0x00446DD0` | oui | **non** |
| î¨ð²Ý½À¯Ì | — | ? | **non** |
| î¨ð²Ýáï | `0x00446DD0` | oui | **non** |
| î­×ÝëÙ | `0x00447630` | oui | **non** |
| î½$Ï½¶Ú²ï | — | oui | **non** |
| î½Ï½¶Ú²ï | — | oui | **non** |
| îÓÝÛò | — | oui | **non** |
| îØ¼¬½ïÛ¯÷ | `0x00446900` | oui | **non** |
| îñÙ±¸½ | `0x004753D0`, `0x00446A90` | oui | **non** |
| îñÙÎÝ | — | oui | **non** |
| îñÙÒ²½ | — | oui | **non** |
| ï©Ñ¿ï | `0x00446F30` | oui | **non** |
| ïÐÆµÝÌªæ | `0x004753D0`, `0x00476500` | oui | **non** |
| ïÚ²ÝÛ¯ï | — | oui | **non** |
| ïÛ÷ÆÙ | — | oui | **non** |
| ðÊÑÄ | `0x004477D0` | oui | **non** |
| ðçØ½¸½¹²Ù | — | oui | **non** |
| ñ­Ã¨óÙ | — | oui | **non** |
| ò×¯¸±Ï | — | oui | **non** |
| ò×¯¸±¯¼­ | `0x00445F60` | oui | **non** |
| ò×¯ï´·½ | `0x00445F60` | oui | **non** |
| ò×¯ïµ²Ù | `0x00445CF0` | oui | **non** |
| ò×¯ï¿ï | — | oui | **non** |
| ò×¯ïøÝ | `0x00445F60` | oui | **non** |
| òØ¼ÝãÈ¯¸Ú½ | — | oui | **non** |
| òÙ±¯¼­ | `0x00445F60` | oui | **non** |
| òÙö±½ | `0x00446900` | oui | **non** |
| óÙ¾Ù¸ | `0x004753D0`, `0x00447630`, `0x00446410` | oui | **non** |
| õÜè±Ï | `0x00446DD0` | oui | **non** |
| õÜè±¸½ | — | oui | **non** |
| õÝï×ô¯¸½ | `0x00446170`, `0x00475CC0` | oui | **non** |
| ÷ØÝ½õÝÀ¸Ù | — | oui | **non** |
| ÷ØÝ¾½øÝëÝÄ | — | oui | **non** |
| ÷Ú½ÊÝÏ | — | ? | **non** |
| ÷Úî¨¸¼®Ý÷ÚÄ | — | oui | **non** |
| ÷ÛÃ¸ÄØÝã | — | oui | **non** |
| ù²èÝ±¸½ | `0x00446F30` | oui | **non** |
| ù²èÝÚ²ö± | `0x00446F30` | oui | **non** |

**607 objets distincts**, dont **536** pleinement implementes et **396** deja employes
en jeu.

### Vus en jeu, absents du catalogue

**11 cle(s)** relevee(s) par la sonde ne correspondent a aucune fiche d'`objets.csv`.

Ce n'est pas une erreur de la sonde : `objets.csv` porte les catalogues **par
sous-zone** — ce qu'on ramasse et ce que les coffres donnent —, et **pas**
l'equipement que la creation de personnage accorde au depart. Un objet obtenu
autrement echappe donc au releve statique, et seule la sonde le voit. C'est
exactement le role de filet qu'on lui a laisse.

Le nom ci-dessous est decode **au mieux** depuis la cle, faute de fiche ou lire
le vrai libelle.

| cle | nom probable |
|---|---|
| `6d546f6d616861776b00000000000000` | Tomahawk |
| `6d4d6173737565000000000000000000` | Massue |
| `77526f62650000000000000000000000` | Robe |
| `7745706565206e6f726d616c65000000` | Epee normale |
| `6d4261746f6e20646520626f69730000` | Baton de bois |
| `77426167756574746500000000000000` | Baguette |
| `6d506f69676e61726400000000000000` | Poignard |
| `77436f75727465206570656500000000` | Courte epee |
| `6d426f75636c69657263756972000000` | Boucliercuir |
| `6d53696d706c65206172630000000000` | Simple arc |
| `7741726d757265206375697200000000` | Armure cuir |

---

## Les genres de la table des 48 effets (opcode 18)

| genre | emplois |
|---:|---:|
| 46 | 54 |
| 41 | 29 |
| 42 | 17 |
| 8 | 13 |
| 0 | 8 |
| 30 | 6 |
| 13 | 5 |
| 9 | 4 |
| 27 | 4 |
| 36 | 3 |
| 15 | 3 |
| 7 | 2 |
| 12 | 1 |

Les 48 sont transcrites ; ce tableau sert a savoir **lesquelles un objet peut reellement
exercer**, donc lesquelles valent un essai en jeu.

---

## Les fiches de sort que l'opcode 14 designe

Le sous-type reellement engendre est l'octet `+0x10` de la fiche, qui vit dans le
repertoire des sorts et non dans `objets.csv` : ces paires ne sont donc **pas resolues**.
C'est la seule lacune de ce releve.

| famille/indice | emplois |
|---|---:|
| 2/14 | 14 |
| 0/21 | 13 |
| 0/25 | 13 |
| 0/17 | 13 |
| 0/24 | 12 |
| 0/11 | 11 |
| 2/15 | 9 |
| 2/10 | 9 |
| 2/18 | 8 |
| 2/19 | 8 |
| 2/17 | 8 |
| 2/16 | 8 |
| 0/12 | 8 |
| 1/23 | 7 |
| 1/16 | 7 |
| 0/10 | 7 |
| 0/13 | 6 |
| 2/0 | 6 |
| 0/16 | 6 |
| 0/22 | 6 |
| 1/21 | 5 |
| 0/20 | 5 |
| 1/12 | 4 |
| 2/12 | 4 |
| 0/8 | 4 |
| 2/13 | 4 |
| 0/6 | 4 |
| 2/7 | 4 |
| 1/0 | 3 |
| 0/15 | 3 |
| 0/0 | 3 |
| 0/3 | 3 |
| 2/8 | 3 |
| 0/27 | 3 |
| 0/23 | 3 |
| 0/18 | 2 |
| 2/2 | 1 |
| 0/1 | 1 |
| 1/15 | 1 |
| 0/2 | 1 |
| 0/7 | 1 |
