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
| `0x004753D0` | 215 | 0xbd | oui | **non** | 0,1,2,3,4,5,6,7,8,9,10 |
| `0x00446DD0` | 189 | 0x3e | oui | **non** | 0,1,3,4,5,6,9,10 |
| `0x00445F60` | 166 | 0x44 | oui | **non** | 0,1,3,4,5,6,7,8,9,10 |
| `0x004462B0` | 162 | 0x43 | oui | **non** | 0,1,2,3,4,5,6,7,9,10 |
| `0x00446900` | 110 | 0x41 | oui | **non** | 0,1,2,3,4,5,6,9,10 |
| `0x00446F30` | 95 | 0x3d | oui | **non** | 0,1,2,3,4,5,6,10 |
| `0x00476500` | 75 | 0x5b | oui | **non** | 0,1,3,4,5,6,7,10 |
| `0x00446200` | 36 | 0x45 | oui | **non** | 0,1,3,4,5,6,10 |
| `0x00446410` | 25 | 0x42 | oui | **non** | 0,1,3,4,5,6,10 |
| `0x00476350` | 17 | 0x5a | oui | **non** | 0,1,4,5,7,10 |
| `0x00445E70` | 17 | 0x48 | oui | **non** | 0,3,4,6 |
| `0x00445CF0` | 15 | 0x4c | oui | **non** | 0,1,3,5,6,10 |
| `0x00447630` | 14 | 0x3b | oui | **non** | 0,1,3,4,5,10 |
| `0x0047A100` | 10 | 0x49 | oui | oui | 0,4,6 |
| `0x00446170` | 10 | 0x4d | oui | **non** | 0,1,4,10 |
| `0x00404920` | 10 | 0x55 | oui | **non** | 0,6,10 |
| `0x00475CC0` | 8 | 0x4e | oui | **non** | 0,1,10 |
| `0x00446A90` | 4 | 0x3f | oui | **non** | 0,5,10 |
| `0x004477D0` | 2 | 0x4b | oui | **non** | 0,2 |
| `0x00447170` | 1 | 0x3c | oui | **non** | 1 |

---

## Le compte

| | |
|---|---|
| gestionnaires distincts atteignables par un objet | **21** |
| deja ecrits | **21** |
| a transcrire | **0** |
| exerces en jeu (sonde des gestionnaires) | **2** |
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
| Aiguille médicinale | `0x004753D0`, `0x00446900` | oui | **non** |
| Aiguille médicinale | `0x004753D0`, `0x00446900` | oui | **non** |
| Aile de Lucifer | `0x00446F30`, `0x00446170` | oui | **non** |
| Aile de Séraphin | `0x00446900`, `0x00446200` | oui | **non** |
| Aile de fée | `0x004753D0` | ? | **non** |
| Almader | — | oui | **non** |
| Ambrosia | `0x00476500` | oui | **non** |
| Amulette de la lumière | `0x00446DD0` | oui | **non** |
| Amulette des runes | — | oui | **non** |
| Amulette du dragon | — | oui | **non** |
| Anneau de Merlin | — | oui | **non** |
| Anneau de Satan | `0x004753D0`, `0x00446F30` | oui | **non** |
| Anneau de l'hermite | — | oui | **non** |
| Anneau des philosophes | `0x00446900` | oui | **non** |
| Anneau des éléments | — | oui | **non** |
| Anneau du roi Salomon | — | oui | **non** |
| Answerer | — | oui | **non** |
| Aquachâle | `0x00446DD0` | oui | **non** |
| Arc classique | — | oui | **non** |
| Arc de bois | — | oui | **non** |
| Arc de chasseur | — | oui | **non** |
| Arc des elfes | — | oui | **non** |
| Arc en croix | — | oui | **non** |
| Arc gris | — | oui | **non** |
| Arc noble | — | oui | **non** |
| Armure | — | oui | **non** |
| Armure anti-sortilège | — | oui | **non** |
| Armure d'argent | — | oui | **non** |
| Armure de Pawas | `0x00446DD0` | oui | **non** |
| Armure de Satan | — | oui | **non** |
| Armure de chevalier | — | oui | **non** |
| Armure du dragon | — | oui | **non** |
| Armure du gardien | — | oui | **non** |
| Armure mirage | — | oui | **non** |
| Armure noire | — | oui | **non** |
| Armure sacrée | `0x00446DD0` | oui | **non** |
| Artémis | — | oui | **non** |
| Bague au joyau | — | oui | **non** |
| Bague de protection | — | oui | **non** |
| Bague de résistance | — | oui | **non** |
| Bague réflectrice | — | oui | **non** |
| Bague sacrée | — | oui | **non** |
| Baguette aux joyaux | — | oui | **non** |
| Baguette d'Ishtar | — | oui | **non** |
| Baguette d'argent | — | oui | **non** |
| Baguette de cristal | — | oui | **non** |
| Baguette de l'air | — | oui | **non** |
| Baguette de l'alchimiste | — | oui | **non** |
| Baguette de l'arc-en-ciel | — | oui | **non** |
| Baguette de l'eau | — | oui | **non** |
| Baguette de la féerie | — | oui | **non** |
| Baguette de la licorne | — | oui | **non** |
| Baguette de la terre | — | oui | **non** |
| Baguette des anges | — | oui | **non** |
| Baguette des runes | — | oui | **non** |
| Baguette du feu | — | oui | **non** |
| Baguette du mal | — | oui | **non** |
| Baguette guérisseuse | — | oui | **non** |
| Baguette magique | — | oui | **non** |
| Baguette spatiale | — | oui | **non** |
| Baguette stellaire | — | oui | **non** |
| Baguette-Misuril | — | oui | **non** |
| Baguette-vampire | — | oui | **non** |
| Bahamut | `0x004477D0` | oui | **non** |
| Bahil | — | oui | **non** |
| Balance des juges | `0x004753D0` | ? | **non** |
| Balle de cristal | `0x00446790` | oui | **non** |
| Balle de feu | — | ? | **non** |
| Balle de gel | — | ? | **non** |
| Balle de l'arc-en-ciel | — | ? | **non** |
| Balle de l'illusion | `0x004753D0` | ? | **non** |
| Balle de lumière | — | ? | **non** |
| Balle de terre | — | ? | **non** |
| Balle des ténèbres | — | ? | **non** |
| Balle des éléments | — | ? | **non** |
| Balle du tonnerre | — | ? | **non** |
| Balle maléfique | — | ? | **non** |
| Balle sacrée | — | ? | **non** |
| Bannière de chevalier | — | oui | **non** |
| Belladone | — | oui | **non** |
| Black Jack | `0x00447170` | oui | **non** |
| Bonbon aux plantes | `0x00475CC0` | oui | **non** |
| Bottes ailées | — | oui | **non** |
| Bottes d'elfes | — | oui | **non** |
| Bottes de cuir | — | oui | **non** |
| Bottes de fer | — | oui | **non** |
| Bottes de sept lieux | — | oui | **non** |
| Boucle de cristal | — | oui | **non** |
| Boucle de la chance | — | oui | **non** |
| Bouclier d'argent | — | oui | **non** |
| Bouclier de Bartschas | `0x00446DD0` | oui | **non** |
| Bouclier de Lucifer | — | ? | **non** |
| Bouclier de Séraphin | — | oui | **non** |
| Bouclier de brigand | — | oui | **non** |
| Bouclier de glace | `0x00446900` | oui | **non** |
| Bouclier de l'air | `0x00446900` | oui | **non** |
| Bouclier de l'ombre | — | oui | **non** |
| Bouclier de la terre | `0x00446900` | oui | **non** |
| Bouclier de vie | — | oui | **non** |
| Bouclier des elfes | — | oui | **non** |
| Bouclier des runes | — | oui | **non** |
| Bouclier des élémentaux | `0x00446DD0` | oui | **non** |
| Bouclier du feu | `0x00446900` | oui | **non** |
| Bouclier du gardien | — | oui | **non** |
| Bouclier lunaire | — | oui | **non** |
| Bouclier rond | — | oui | **non** |
| Bouclier réflecteur | — | oui | **non** |
| Bouclier sacré | `0x00446DD0` | oui | **non** |
| Bouclier solaire | — | oui | **non** |
| Boîte de Pandore | `0x00446170`, `0x00475CC0` | oui | **non** |
| Boîte du silence | `0x004753D0` | ? | **non** |
| Boîte à bijoux | `0x00445F60` | oui | **non** |
| Bracelet d'argent | — | oui | **non** |
| Bracelet de reine | — | oui | **non** |
| Broche au joyau | — | oui | **non** |
| Broche d'argent | — | oui | **non** |
| Bâton d'Apollon | — | oui | oui |
| Bâton de cristal | — | oui | **non** |
| Bâton de fer | — | oui | **non** |
| Bâton des sages | — | oui | **non** |
| Bâton divin | — | ? | **non** |
| Bâton lunaire | — | oui | **non** |
| Bâton stellaire | — | ? | **non** |
| Béquille | `0x00446F30` | oui | **non** |
| Cape d'automne | — | oui | **non** |
| Cape d'hiver | — | oui | **non** |
| Cape d'été | — | oui | **non** |
| Cape des runes | — | oui | **non** |
| Cape du printemps | — | oui | **non** |
| Carte de la peur | `0x00446F30`, `0x00446170` | oui | **non** |
| Carte de tarot | — | ? | **non** |
| Casque ailé | — | oui | **non** |
| Casque d'argent | — | oui | **non** |
| Casque de cuir | — | oui | **non** |
| Casque de fer | — | oui | **non** |
| Casque splendide | — | oui | **non** |
| Casquette de chasseur | — | oui | **non** |
| Casse-tête | — | oui | **non** |
| Casse-tête à chaîne | — | oui | **non** |
| Cendre blanche | `0x00445F60` | oui | **non** |
| Cendre bleue | `0x00445F60` | oui | **non** |
| Cendre grise | `0x00445F60` | oui | **non** |
| Cendre noire | `0x00445F60` | oui | **non** |
| Cendre rouge | `0x00445F60` | oui | **non** |
| Cendre verte | `0x00445F60` | oui | **non** |
| Chapeau d'elfe | — | oui | **non** |
| Chapeau de sorcière | — | oui | **non** |
| Chaussures à plumes | — | oui | **non** |
| Chaussuresvol | `0x004753D0` | oui | **non** |
| Chaîne de l'air | — | oui | **non** |
| Chaîne de l'eau | — | oui | **non** |
| Chaîne de la lumière | — | oui | **non** |
| Chaîne de la terre | — | oui | **non** |
| Chaîne des ténèbres | — | oui | **non** |
| Chaîne du feu | — | oui | **non** |
| Chaîne du mal | — | oui | **non** |
| Chaîne du souffle | — | oui | **non** |
| Chaîne sacrée | — | oui | **non** |
| Chaînette d'argent | — | oui | **non** |
| Chaînette de plumes | — | oui | **non** |
| Châle précieux | — | oui | **non** |
| Corde du retour | — | ? | **non** |
| Corne du Minotaure | `0x00447630` | oui | **non** |
| Corne du diable | — | oui | **non** |
| Corne du dragon | — | oui | **non** |
| Cotte de maille | — | oui | **non** |
| Couronne de la lune | — | oui | **non** |
| Couronne du soleil | — | oui | **non** |
| Craimore | — | oui | **non** |
| Cristal d'air | `0x004462B0` | oui | **non** |
| Cristal d'eau | `0x004462B0` | oui | **non** |
| Cristal d'ombre | `0x004462B0` | oui | **non** |
| Cristal d'énergie | `0x004753D0` | ? | **non** |
| Cristal de feu | `0x004462B0` | oui | **non** |
| Cristal de lumière | `0x004462B0` | oui | **non** |
| Cristal de magie | `0x004462B0` | oui | **non** |
| Cristal de terre | `0x004462B0` | oui | **non** |
| Cristal du mal | `0x004462B0` | oui | **non** |
| Cristal saint | `0x004462B0` | oui | **non** |
| Croix | — | oui | **non** |
| Crosse d'ambre | — | oui | **non** |
| Crosse de Séraphin | — | oui | **non** |
| Crosse de voeux | — | oui | **non** |
| Crosse des anciens | — | oui | **non** |
| Crosse du diable | — | oui | **non** |
| Crosse du jugement | — | oui | **non** |
| Crosse sacrée | — | oui | **non** |
| Cuir fin | — | oui | **non** |
| Cuirasse au miroir | — | oui | **non** |
| Cuirasse de bronze | — | oui | **non** |
| Cuirasse de maître | — | oui | **non** |
| Daak | — | oui | **non** |
| Debacle | — | oui | **non** |
| Dominique | — | oui | **non** |
| Dropneal | — | oui | **non** |
| Durandal | `0x00447630` | oui | **non** |
| Délicieux drop | `0x00446900` | oui | **non** |
| Eau bénite | — | ? | **non** |
| Ecailles de Belzébuth | — | oui | **non** |
| Ecailles de dragon | — | oui | **non** |
| Ecu du destin | `0x00445F60` | oui | **non** |
| Elixir de magicien | `0x00446790` | oui | **non** |
| Elixir de vie | `0x00446790` | oui | **non** |
| Elixir des esprits | `0x00446790` | oui | **non** |
| Epice rouge | `0x00446F30` | oui | **non** |
| Epée d'argent | — | oui | **non** |
| Epée de Damas | — | oui | **non** |
| Epée de feu | — | oui | **non** |
| Epée de glace | — | oui | **non** |
| Epée de la fatalité | `0x00446F30` | oui | **non** |
| Epée de sang | — | oui | **non** |
| Epée du bâtard | — | oui | **non** |
| Epée du gardien | — | oui | **non** |
| Epée du tonnerre | — | oui | **non** |
| Epée gauloise | `0x00446A90` | oui | **non** |
| Epée noire | — | oui | **non** |
| Excalibur | — | oui | **non** |
| Extrait d'esprits | `0x00445F60` | oui | **non** |
| Extrait de sang | `0x00445F60` | oui | **non** |
| Fabnihl | `0x00446F30` | oui | **non** |
| Faux de la mort | `0x00404920` | oui | **non** |
| Fenouil | — | oui | **non** |
| Fenril | — | oui | **non** |
| Fine rapière | — | oui | **non** |
| Flacon d'énergie | `0x004753D0`, `0x00446790`, `0x00446200` | oui | **non** |
| Flèche de Persée | — | oui | **non** |
| Flèche de faucon | `0x0047A100` | oui | **non** |
| Flèche de l'ombre | — | oui | **non** |
| Flèche de lumière | — | ? | **non** |
| Flèche des Gouivres | `0x00446F30` | oui | **non** |
| Flèche du vent | — | oui | **non** |
| Flûte des moutons | `0x004753D0` | ? | **non** |
| Forgée de glace | — | oui | **non** |
| Franberg | — | oui | **non** |
| Féline | `0x00446F30` | oui | **non** |
| Gabriel | — | oui | **non** |
| Gantelet de force | — | oui | **non** |
| Gantelets | — | oui | **non** |
| Gantelets ailés | — | oui | **non** |
| Gantelets d'argent | — | oui | **non** |
| Gantelets de chevaliers | — | oui | **non** |
| Gants ailés | — | oui | **non** |
| Gants de cuir | — | oui | **non** |
| Gants de force | — | oui | **non** |
| Gants des runes | — | oui | **non** |
| Gants sacrés | — | oui | **non** |
| Genocide | `0x00404920` | oui | **non** |
| Glaive | — | oui | **non** |
| Grand arc | — | oui | **non** |
| Grande hache | — | oui | **non** |
| Gulwick | `0x00446F30` | oui | **non** |
| Hache d'Ouga | — | oui | **non** |
| Hache d'argent | — | oui | **non** |
| Hache de Mars | — | oui | **non** |
| Hache de Viking | — | oui | **non** |
| Hache de boucher | — | oui | **non** |
| Hache de guerre | — | oui | **non** |
| Hache de la sauvagerie | `0x004753D0`, `0x00447630`, `0x00446410` | oui | **non** |
| Hache du diable | `0x004753D0`, `0x00446A90` | oui | **non** |
| Hache du feu | — | oui | **non** |
| Hache du gel | — | oui | **non** |
| Hache empoisonnée | `0x00446F30` | oui | **non** |
| Hache en demi-lune | — | oui | **non** |
| Hache fine | — | oui | **non** |
| Hachette | — | oui | **non** |
| Hallebarde | — | oui | **non** |
| Harpe d'or | — | oui | **non** |
| Horloge des mystères | `0x004753D0` | ? | **non** |
| Horloge du bonheur | `0x004753D0` | ? | **non** |
| Huile de sang | `0x00445CF0` | oui | **non** |
| Huile du pouvoir | `0x00445CF0` | oui | **non** |
| Hysopius | — | oui | **non** |
| Insigne de brigand | — | oui | **non** |
| Joyau de feu | `0x004753D0` | ? | **non** |
| Joyau de glace | `0x004753D0` | ? | **non** |
| Joyau de terre | `0x004753D0` | ? | **non** |
| Joyau de vent | `0x004753D0` | ? | **non** |
| La dévoreuse | — | oui | **non** |
| La massacreuse | `0x00404920` | oui | **non** |
| Lame de Calvin | — | oui | **non** |
| Lame de la terre | — | oui | **non** |
| Lame du soleil | — | oui | **non** |
| Lampe sacrée | `0x004753D0` | ? | **non** |
| Large épée | — | oui | oui |
| Lavande | — | oui | **non** |
| Livre de la technique | — | oui | **non** |
| Long marteau | — | oui | **non** |
| Longue épée | — | oui | **non** |
| Manteau | — | oui | **non** |
| Manteau d'elfe | — | oui | **non** |
| Manteau de brigand | — | oui | **non** |
| Manteau de croisé | — | oui | **non** |
| Manteau de cuir | — | oui | **non** |
| Manteau de résistance | — | oui | **non** |
| Manteau des esprits | `0x00446DD0` | oui | **non** |
| Manteau à plumes | — | oui | **non** |
| Marteau de Thor | — | oui | **non** |
| Marteau de guerre | — | oui | **non** |
| Marteau de prêtre | — | oui | **non** |
| Masque de chevalier | — | oui | **non** |
| Masque de la mort | — | oui | **non** |
| Masse d'armes | — | oui | **non** |
| Mercuriale | — | oui | **non** |
| Minegosh | — | oui | **non** |
| Miroir d'eau | — | oui | **non** |
| Miroir de la porte | — | oui | **non** |
| Mistortain | — | oui | **non** |
| Médaillon d'argent | — | oui | **non** |
| Médaillon d'or | — | oui | **non** |
| Médaillon de bronze | — | oui | **non** |
| Nobles bottes | — | oui | **non** |
| Nobles chaussures | — | oui | **non** |
| Os de guerrier sauvage | `0x004753D0`, `0x00446410` | oui | **non** |
| Panoramix | `0x00446790`, `0x00476350` | oui | **non** |
| Parfum d'impératrice | `0x00446790`, `0x00445E70` | oui | **non** |
| Parfum de convalescence | `0x00445E70` | oui | **non** |
| Parfum de feu | — | ? | **non** |
| Parfum de guérison | `0x00445E70` | oui | **non** |
| Pavois de chevalier | — | oui | **non** |
| Pavois fin | — | oui | **non** |
| Pendentif de princesse | — | oui | **non** |
| Pendentif vaporeux | — | oui | **non** |
| Pentacle de prince | — | oui | **non** |
| Pentacle magique | — | oui | **non** |
| Petite potion | `0x00446790` | oui | oui |
| Pierre magique | `0x004753D0`, `0x00446790` | oui | **non** |
| Pierre médicinale | `0x004753D0`, `0x00446790` | oui | **non** |
| Pique | — | oui | **non** |
| Plastron | — | oui | **non** |
| Plume | `0x00445F60` | oui | **non** |
| Plume dominicale | `0x004753D0`, `0x00476500` | oui | **non** |
| Poignard d'Orihalcon | — | oui | **non** |
| Poignard d'argent | — | oui | **non** |
| Poignard de brigand | — | oui | **non** |
| Poignard de sang | — | oui | **non** |
| Poignard des brouillards | — | oui | **non** |
| Pointe de sang | `0x00445F60` | oui | **non** |
| Porte-bonheur | — | oui | **non** |
| Potion med | `0x00446790` | oui | **non** |
| Potion olympique | `0x00446790`, `0x00446200` | oui | **non** |
| Poudre de fées | — | ? | **non** |
| Poudre de zombie | `0x004753D0`, `0x00476500` | oui | **non** |
| Poupée Dummy | `0x004753D0`, `0x00476500` | oui | **non** |
| Poupée dansante | `0x004753D0`, `0x00446790` | oui | **non** |
| Queue de dragon | — | oui | **non** |
| Rapière | — | oui | **non** |
| Rapière Misuril | — | oui | **non** |
| Rapière aux éclairs | — | oui | **non** |
| Rapière d'argent | — | oui | **non** |
| Rapière de Zeus | — | oui | **non** |
| Rapière du chaos | `0x00446F30` | oui | **non** |
| Rapière à poison | `0x00446F30` | oui | **non** |
| Renegeton | — | oui | **non** |
| Revatin | — | ? | **non** |
| Robe de Saulus | `0x00446DD0` | oui | **non** |
| Robe de fantôme | — | oui | **non** |
| Robe de l'ombre | — | oui | **non** |
| Robe de la terre | — | oui | **non** |
| Robe de reine | — | oui | **non** |
| Robe des anciens | — | oui | **non** |
| Robe des esprits | — | oui | **non** |
| Robe des runes | — | oui | **non** |
| Robe du démon | — | oui | **non** |
| Robe du gardien | `0x00446DD0` | oui | **non** |
| Robe fine | — | oui | **non** |
| Robe magique | — | oui | **non** |
| Robe médicinale | — | oui | **non** |
| Robe sacrée | — | oui | **non** |
| Robe épiscopale | — | oui | **non** |
| Ruban de force | — | oui | **non** |
| Rubis | `0x00446900` | oui | **non** |
| Réflector | — | oui | **non** |
| Sabre de la terre | — | oui | **non** |
| Saphir | `0x00446900` | oui | **non** |
| Sauge | — | oui | **non** |
| Sauvagerie | `0x004753D0`, `0x00446410` | oui | **non** |
| Sceptre de bataille | — | oui | **non** |
| Sceptre de la mort | — | oui | **non** |
| Simple bâton | — | oui | **non** |
| Solitaire | — | oui | **non** |
| Stylet de bague | — | oui | **non** |
| Symbole de force | — | oui | **non** |
| Symbole des ténébres | — | oui | **non** |
| Table de vision | — | oui | **non** |
| Table des anciens | — | oui | **non** |
| Tablier de cuir | — | oui | **non** |
| Talisman | — | oui | **non** |
| Talisman médicinal | — | oui | **non** |
| Tiare de la terre | `0x00446DD0` | oui | **non** |
| Tiare des étoiles | `0x00446DD0` | oui | **non** |
| Verveine | — | oui | **non** |
| Voile de beauté | — | oui | **non** |
| Voile des ombres | `0x00446900` | oui | **non** |
| Voile mystique | — | oui | **non** |
| Walkyrie | — | oui | **non** |
| Walmunk | — | oui | **non** |
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

**607 objets distincts**, dont **536** pleinement implementes et **3** deja employes
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
| `6d53696d706c65206172630000000000` | Simple arc |
| `6d426f75636c69657263756972000000` | Boucliercuir |
| `6d506f69676e61726400000000000000` | Poignard |
| `6d4261746f6e20646520626f69730000` | Baton de bois |
| `7741726d757265206375697200000000` | Armure cuir |
| `77426167756574746500000000000000` | Baguette |
| `77526f62650000000000000000000000` | Robe |
| `77436f75727465206570656500000000` | Courte epee |
| `7745706565206e6f726d616c65000000` | Epee normale |
| `6d546f6d616861776b00000000000000` | Tomahawk |
| `6d4d6173737565000000000000000000` | Massue |

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
