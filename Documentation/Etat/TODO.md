# TODO

Point d’entrée entre deux sessions. Mis à jour le **2026-09-18** (paliers 1 et 2 du château de
l’immortel : **la zone 6 passe de 31 à 125 sur 170**, et la grotte scellée de 48 à 51). La zone 5,
le Labyrinthe, a été fermée le même jour — **160 sur 160**, le septième ensemble entièrement
implémenté —, et la veille avaient fermé la mine, la montagne du dragon de feu et la vieille tour.
L'historique détaillé est dans `REVERSE_STATUS.md` (sections datées) ; ce fichier ne garde que
l'état courant et la suite à faire.

**Ne pas commit** — seul l'utilisateur commit, même après une tâche terminée.

**Les tests de navigation en jeu sont à Vincent.** Atteindre une pièce, changer de carte,
reproduire un symptôme localisé : c'est lui qui les exécute. Voir le partage dans
`Documentation/Tests/test-protocol.md`. Restent à l'agent le build et les lancements sans
déplacement.

---

# SEPT ENSEMBLES COMPLETS — LE LABYRINTHE DES MORTS FERMÉ (2026-09-18)

**La zone 5, le Labyrinthe des morts, est complète : 160 sur 160**, fermée le 2026-09-18 en treize
lots — ses dix-neuf routines d'intelligence la veille, tout son décor et ses effets ce jour-là. Les
six derniers lots (8 à 13) ont écrit 27 corps pour 17 lectures ; leur bilan de méthode est dans
`REVERSE_STATUS.md`, 2026-09-18 suite 9.

# SIX ENSEMBLES COMPLETS, ET TOUTE L'INTELLIGENCE DU LABYRINTHE (2026-09-17)

Les trois tables gravées dans `.data` — `0x00551c30`, `0x00551f30`, `0x00552230`, **274 sur 274** —,
la **zone 0, l’auberge, 48 sur 48**, la **forêt (zone 3, 136 sur 136)** bouclée le 2026-09-02, et
depuis le 2026-09-17 la **mine (zone 1, 157 sur 157)** et la **montagne du dragon de feu (zone 2,
42 sur 42)**, puis la **vieille tour (zone 4, 176 sur 176)** : six ensembles entièrement
implémentés. Le détail est dans `REVERSE_STATUS.md`, les sections du 2026-09-15 et du 2026-09-17.

**Et depuis la suite 10, les DIX-NEUF routines d'intelligence du Labyrinthe des morts** — les seize
qui restaient, écrites en dix lectures. La zone 5 n'a plus une seule bête à transcrire : sa catégorie
« E — intelligence » est vide, et il ne lui reste que du décor (B, C, D). C'est aussi la première
fois qu'une zone voit toutes ses bêtes écrites **sans être complète** : 58 sur 160.

La mine s’est fermée sur sept routines d’intelligence, dont **une n’a demandé aucune ligne** :
l’Ours gris `0x0048DB30` est le Grizzly, 660 instructions identiques sur 660. La montagne s’est fermée
sur le Dragon rouge et sur ses deux scènes — l’envol du dragon fait naître la scène muette —, au prix
d’une décision de plan mémoire : `ViewParams` étendue à `0x1b0`. **La vieille tour s’est fermée sur
seize routines d’intelligence pour onze lectures** : le diff contre les corps déjà écrits a désigné
les parentés avant toute transcription (voir `REVERSE_STATUS.md`, 2026-09-17 suite 13).

Les tables statiques ne sont pas une zone : **ce qui est écrit là vaut dans les onze**. C'est le
socle commun, et il est entier.

## Où on en est

| | |
|---|---|
| gestionnaires distincts atteignables | **1 430** |
| testés en jeu | **326** |
| implémentés, jamais appelés | **694** |
| à transcrire | **410** |

| zone | lieu | implémentés |
|---:|---|---|
| — | **tables statiques** (partout) | **274 / 274** |
| 0 | **L'auberge** | **48 / 48** |
| 3 | **La forêt** | **136 / 136** |
| 1 | **La mine abandonnée** | **157 / 157** |
| 2 | **La montagne du dragon de feu** | **42 / 42** |
| 4 | **La vieille tour** | **176 / 176** |
| 5 | **Le Labyrinthe des morts** | **160 / 160** |
| 6 | Le château de l’immortel | 125 / 170 |
| 7 | Les ruines souterraines | 19 / 35 |
| 8 | La vallée d'argent | 18 / 46 |
| 9 | Les ruines dans le lac | 18 / 41 |
| 10 | La grotte scellée | 51 / 349 |

Régénérer avec `sh OpenForesia.Tools/census-entity-handlers.sh` (ou `tests\feuille-de-route.bat`).

## La suite, par ordre de rendement

1. **Les 32 routines d'IA qui sont des copies exactes d'un frère non écrit** (relevé du 2026-09-02,
   `census-duplicates.sh`, revérifié le 2026-09-15). Les écrire **par représentant** et non par zone
   économise 32 corps entiers ; les six meilleurs groupes font **20 corps pour 6 lectures**, et
   chacune de ces lectures est une grosse routine. C'est le meilleur rendement disponible.
   - ~~5 corps, 1 179 instr : `0x004A7190` `0x004B22D0` `0x004E8DF0` `0x004EDB60` `0x004F8270`~~ —
     **fait le 2026-09-15** : le Gremlin (vieille tour). Les cinq ne different que par l'adresse de
     leur table de sorts, et les cinq tables portent les memes octets.
   - ~~3 corps, 1 212 : `0x004D1F70` `0x004E67C0` `0x004EB530`~~ — **fait le 2026-09-15** : le Pourpre
     preux (chateau de l'immortel). Copies conformes, sans donnee propre.
   - ~~3 corps, 1 110 : `0x004ACC20` `0x004CE120` `0x004DB610`~~ — **fait le 2026-09-15** : la
     Crapouille, la Gargouille et le Crapaud, trois especes pour une routine.
   - ~~3 corps, 1 008 : `0x004F3350` `0x004F5630` `0x004FC9B0`~~ — **fait le 2026-09-15** : le Gremlin
     de la grotte scellee, un terrestre sans rapport avec celui de la vieille tour.
   - 3 corps, 986 : `0x004E56C0` `0x004EA430` `0x004EEB20` — **le seul groupe qui reste**
   - ~~3 corps, 970 : `0x004BFE90` `0x004E7810` `0x004EC580`~~ — **fait le 2026-09-17 (suite 10)** :
     l'Ombre du Labyrinthe et ses deux copies de la grotte scellée. Les quatre corps (la quatrième
     est l'Ombre rouge, `0x004C5A30`) ne diffèrent que par **un mot** : celui que la variante 2 et
     au-delà lisent seize places après la fin de leur liste de sorts, par un registre non chargé.
2. **Les groupes de gestionnaires simples**, même principe : 144 corps couverts par 49 lectures.
   Le plus gros fait 7 corps pour 47 instructions.
3. **La zone 7 (19 / 35)**, la plus petite de celles qui restent. La zone 2 est complète depuis le
   2026-09-17.
4. ~~**Le décor du Labyrinthe des morts**~~ — **complet le 2026-09-18** (160 / 160) —
   plus un seul « B » ni « C ». Le tri est à jour dans `Zone5ParComplexite.md`. La
   table `0x0056adc8` n'a plus que son unique « D » (`0x00462A80`, 536 instr.), et `0x0056aec8` n'a
   plus aucun verbe à écrire. Trois lots faits le 2026-09-17, neuf le 2026-09-18 :
   - **lot 1** (suite 17) : les dix plus simples — sept décors de `0x0056adc8` dans le fichier neuf
     `Handlers/Labyrinth.cs`, trois **verbes de script** de `0x0056aec8` dans `WorldMapScript.cs` ;
   - **lot 2** (suite 18) : les **neuf paires miroir** de `0x0056b2c8` (bloc 5-8 = bloc 0-3 à
     `+0x2ff0`), 19 corps pour 8 lectures, treize planches relevées, cinq marges scellées ;
   - **lot 3** (suite 19) : le **banc des sous-zones 4 et 9**, 12 corps pour 4 lectures, **sur trois
     zones** — ses parents sont au château (6) et dans la grotte scellée (10) ;
   - **lot 4** (2026-09-18) : les **dix plus simples de `0x0056adc8`**, la table du décor. Sept dans
     `Handlers/Labyrinth.cs`, et **trois qui consultent le groupe** ont ouvert
     `Handlers/LabyrinthMecanismes.cs` — coupure de rôle, pas de taille, par le test
     d'appartenance d'`ARCHITECTURE.md`. Le piège du lot : le comptage d'emplacements inline de
     `0x004618B0` **n'est pas** `ActivePartySlotCount_00466D70` (masque interdit `0x50010000` contre
     `0x50000000`), et les deux comptes sont confrontés l'un à l'autre dans la même image ;
   - **lot 5** (2026-09-18) : les **sept derniers « B » de `0x0056adc8`** plus ses **trois « C » les
     plus simples**. Huit dans `Labyrinth.cs`, deux dans `LabyrinthMecanismes.cs` — le partage s'est
     fait en **comptant les appels dans les deux fichiers de la mine** (`StrikePartyMembers_0045DA40`
     est du décor, 6 contre 0 ; `RunEntityScript` est des mécanismes, 15 contre 4) plutôt qu'en
     lisant le sujet apparent. Trois relevés : les **trois tables de position** `0x005517a8` /
     `0x005517c8` / `0x005517e8` (la position d'un bloc poussé survit au changement de carte, voir
     `DataSymbols.md`), un **argument perdu par Ghidra** sur `0x00460C00` qui inverse le sens du site
     (`0` au lieu de la sentinelle `999`), et `0x00461F50` qui **empaquette quatre compteurs dans
     `+0x10` et `+0x16`** — son état n'est que l'octet bas de `+0x10` ;
   - **lot 6** (2026-09-18) : les **douze derniers « B »**, pris d'un coup plutôt que dix — neuf
     effets, deux secousses de caméra, et **un verbe de script** (`0x00444340`, table `0x0056aec8`),
     qui n'est pas un gestionnaire d'entité malgré sa ligne dans `GestionnairesEntites.md`. Deux
     relevés : **`0x00550360` est le premier cas où la donnée invite à transcrire trop LONG** (son
     cinquième enregistrement a la même forme que les quatre premiers ; c'est `+0x40`, qui meurt à
     4, qui borne), et deux corps **identiques instruction pour instruction** lisent **deux globales
     différentes** — `0x005e9062` en sous-zones 0 à 3, `0x005de538` en 5 à 8 ;
   - **lot 7** (2026-09-18) : les **neuf « C » de `0x0056adc8`**, qui vident cette table de la
     catégorie. Six décors, trois mécanismes. Le piège du lot : dans `0x00462330` et `0x004625B0`,
     **`+0x10` n'est pas l'état mais la vitesse de rotation** — l'état est `+0x16`. C'est le second
     corps de la zone à détourner ces champs après `0x00461F50` ; **vérifier sur quoi le corps
     dispatche avant de nommer les champs**. Deux autres relevés : `0x00463150` écrit l'état de ses
     **deux voisines** (`+0xac` et `+0x148`, soit `+0x10` à un et deux rangs de `0x9c`), et les
     deux sœurs `0x00462330` / `0x004625B0` ont des **gardes de caméra inverses** — elles ne
     tournent jamais ensemble ;
   - **lot 8** (2026-09-18, suite 4) : les **seize « C »**, pour douze lectures — deux verbes de
     script, dix effets, deux scènes. Deux corps n'ont demandé aucune ligne (`TickHostDeathBurst`),
     et `0x0041FCA0` assemble le nuage du lot et le **stroboscope de la forêt**, sorti en
     `StepCarrierStrobe`. Le piège du lot : **ce qu'un corps fait d'un porteur nul n'est pas une
     règle de bloc** — deux corps voisins des sous-zones 0 à 3 font l'un « continuer », l'autre
     « sortir » ; lire le `je` de tête. Et `0x00545A28`, l'« appelée inconnue », était
     `Ordinal_762` — un `ret 4` nu dans cette DLL.
   - **lot 9** (2026-09-18, suite 5) : **`0x004224F0` et son jumeau de la grotte scellée
     `0x0043F4E0`**, deux corps pour une lecture (`TickOwnerFrontSweep`). Ses « neuf appelées
     inconnues » étaient neuf corps identiques : la garde, puis un effet d'état déjà écrit.
   - **lot 10** (2026-09-18, suite 6) : **`0x0041C900` et `0x0041EDD0` sont `TickOwnerFrontSweep`**
     à leur repli près (un tampon nul sur la pile ; aucun repli). La « charpente commune mais pas des
     copies » de la suite 19 était vraie entre eux et fausse contre le modèle : **mesurer contre un
     corps écrit, pas seulement entre non-écrits**.
   - **lot 11** (2026-09-18, suite 7) : **les semis des nuages** — `0x0041D310` / `0x0041F760` /
     `0x0041FF40` / `0x00421DC0`, quatre corps pour une lecture (`TickCloudSeed`). Ce sont eux qui
     posent le `+0x44` que le nuage du lot 8 attend : la question qu'il laissait ouverte est close.
   - **lot 12** (2026-09-18, suite 8) : **la famille B**, `0x00420480` / `0x00422E50`, deux corps
     pour une lecture (`TickShockwaveOrSpray`) — l'onde qui frappe une fois chacun, ou sa gerbe.
   - **lot 13** (2026-09-18, suite 9) : **les deux derniers** — le projectile à tête chercheuse
     `0x00484190`, qui porte **LB-031** (il déréférence la cible qu'il vient d'effacer), et le
     battant `0x00462A80`. **La zone est complète.**
   Ce qui vaut pour la zone suivante :
   - **la méthode qui a payé trois fois** : normaliser le désassemblage en neutralisant *toute*
     constante hexadécimale à six chiffres (pas seulement les cibles de saut — c'est ce qui rend un
     échange de planche invisible), puis apparier par nombre d'instructions égal. Les parentés qui
     en sortent traversent les zones.
4. ~~**La vieille tour**~~ — **complète le 2026-09-17** (176 / 176). Pour la prochaine zone, la
   méthode qui a payé : `census-zone-complexity.sh <zone>`, puis **comparer chaque corps restant à
   tous les corps déjà écrits** (diff LOOSE de `census-duplicates.sh`, cache régénéré) avant d'en
   lire un seul. Les « appelées inconnues » du tri se lisent d'abord : `0x00514020` n'était qu'une
   enveloppe.
5. ~~**La zone 5, le Labyrinthe**~~ — **complète le 2026-09-18** (160 / 160), voir le point 4.
   **La zone 6, le château, est en cours : 125 / 170.** Le tri est dans `Zone6ParComplexite.md`
   (2026-09-18, suite 10) et le classement en cinq paliers dans `REVERSE_STATUS.md`.
   - ~~**palier 1**, les 32 corps déjà écrits ou presque~~ — **fait le 2026-09-18 (suite 11)**,
     plus deux gestionnaires de la grotte scellée en prime.
   - ~~**palier 2**, les 62 corps « simples » pour 49 formes~~ — **fait le 2026-09-18 (suite 12)**,
     plus un de la grotte. **Les catégories A et B du tri sont vides.**
   - **La suite est le palier 3** : les 23 « C » pour ~20 lectures — quatre paires miroir
     (`0x00426E80`/`0x004296F0`, `0x0042A920`/`0x0042B190`, `0x004644F0`/`0x00465E30`,
     `0x004646A0`/`0x00465FE0`), `0x0042D9B0` qui ferme aussi `0x00442AC0` (grotte), et 15 isolés.
     Les quatre nuages à effet 32 ont leurs feuilles écrites mais **ne sont pas**
     `TickHostStatusCloud` (73 lignes d'écart au plus proche). Puis ~8 lectures grosses et
     12 routines d'intelligence.
   - **Ce que les deux premiers paliers ont appris** :
     - la normalisation qui apparie deux corps **masque les cibles d'appel** — les résoudre à part,
       sauts internes retirés, avant de conclure qu'un corps est une copie ;
     - **comparer au bon parent** : `0x0042A540` faisait 22 lignes contre le modèle nu et une seule
       instruction contre la jumelle gardée ;
     - **le château se lit en deux blocs miroir au même numéro de créneau** (`0x00558e28` pour les
       sous-zones 0 à 4, `0x005594a8` pour 6 à 10). C'est ce qui apparie les corps sans rien lire,
       et la différence entre deux miroirs tient presque toujours en une constante ;
     - **« simple » ne veut pas dire « gratuit »** : aucune des 49 formes du palier 2 n'avait de
       parent dans le dépôt. Faire le relevé quand même — il a rendu un corps de la grotte scellée
       et évité 49 recherches à la main ;
     - **une ligne de `GestionnairesEntites.md` n'est pas toujours un gestionnaire d'entité** : les
       créneaux de `0x0056aec8` sont des **verbes de script**. Deuxième fois.
6. Le reste. La grotte scellée (349) est le dernier gros morceau.

**Un recouvrement latent a traiter a part** : `s_ForestSceneBlock_00571420` et
`s_EntityState_00571500` se recouvrent sur `0x00571500..0x00571667`. Aucune adresse n'est vue par les
deux noms au 2026-09-17, donc rien ne casse — mais `MemoryLayoutTests` ne le detecte pas. Le correctif
naturel est une seule arene `0x00571270..0x00571a38` (voir `REVERSE_STATUS.md`, 2026-09-17 suite 4),
a mener comme changement delibere.

**La marge a fondu le 2026-09-18** : l'enregistrement de camera du chateau de l'immortel est
`0x005714e0`, le cinquieme de la suite au pas de `0x30`, et il est le premier a FRANCHIR
`0x00571500` — son `+0x20` tombe dessus, son `+0x28` sur `0x00571508`. Les trois adresses
qu'`EntityState` sert reellement (`0x00571528`, `0x00571540`, `0x00571570`) restent hors de
portee, donc le recouvrement est toujours latent ; le suivant ne le sera peut-etre pas.

**La dette d’épreuve, elle, grandit** : 451 corps écrits n’ont jamais tourné, et **la mine et la
vieille tour y entrent presque entières** — 157 créneaux pour l’une, 176 pour l’autre, et 2 seulement
vus passer dans la tour, et le Labyrinthe (160) n’en a vu passer aucun. Sept ensembles sont complets. Une séance de
test sur l’auberge ou sur la mine se lirait désormais comme une mesure, pas comme une chasse au
plantage.

## Ce que ces deux séances ont appris, et qui vaut pour la suite

- **Un aiguillage qui vit hors du répartiteur principal doit être déclaré à
  `census-entity-handlers.sh` le jour où il est écrit.** C'est arrivé deux fois — `CallSubAreaMoveProbe`
  le 2026-09-01, `CallEntityHook_0056afe8` et `CallEntityHook_0056aff0` le 2026-09-15. Un faux
  « à transcrire » ne fait rien échouer : personne ne s'en aperçoit, et on compte comme dette du
  travail déjà fait.
- **Vérifier qu'un corps écrit est bien BRANCHÉ.** `AreaVerb_0053DEA0` et `AreaVerb_0053DF20`
  existaient depuis trois semaines, avec tout le panneau du coffre derrière eux, et le répartiteur
  levait sur les deux. Écrire sans brancher est indétectable jusqu'au premier plantage.
- **Un nom hors convention rend `check-descriptors.sh` muet, pas bruyant.** Le script reconnaît
  `s_Descriptors_00…` / `Descriptors00…()` et rien d'autre. Un vérificateur muet se lit comme un
  vérificateur satisfait.
- **Le même champ change de rôle d'un sous-type au suivant, dans une même famille.** `+0x9a` est une
  couche de dessin pour `0xa8` et un plafond d'échelle pour `0xa6`, voisins immédiats dans le
  binaire. Lire un offset « par analogie avec la sœur » est un piège.
- **Deux relais rendent le recensement structurellement aveugle.** `0x00476950` et `0x00476970`
  lisent le sous-type de leur enfant dans leur propre `+0x0b` : aucune analyse statique ne peut dire
  ce qu'ils atteignent. Seule la sonde le peut.
- **Le mécanisme d'incantation se recopie d'une zone à l'autre.** Le bloc du Behemoth est désormais
  relevé trois fois (Behemoth, Trent, `0x00487260`), dont une hors de la forêt, à quatre constantes
  près à chaque fois. Devant une routine qui appelle `CastSpell_0053A980`, chercher le bit `0x800`
  avant de lire quoi que ce soit d'autre.

---

# NON — ET IL N'Y A QUE DIX-HUIT CORPS À ÉCRIRE (2026-08-30)

**Non, tous les objets ne fonctionnent pas.** Mais l'intuition « chaque objet a son comportement »
est fausse, et la réponse exacte tient maintenant dans un fichier :
**`Documentation/Etat/ObjetsAtteignables.md`**, généré par
`sh OpenForesia.Tools/census-item-effects.sh`.

**On a arrêté de découvrir par plantage.** Les scripts d'objet sont de la *donnée*, et elle était
déjà dans le dépôt : `Documentation/Reference/Bestiaire/objets.csv` porte les `0x140` octets de
chaque fiche, scripts compris (`+0x80` l'emploi, `+0x82` le déclenchement). Le nouvel outil déroule
les deux scripts des **2 370 fiches** en suivant les sauts, et sort la liste exacte de ce qu'un
objet peut atteindre — **sans jouer une seule partie**.

## Le résultat

| | |
|---|---|
| gestionnaires d'entité atteignables par un objet | **21** |
| déjà écrits | **3** |
| **à transcrire** | **18** |

Vingt et un pour **tous les objets du jeu**, pas 225 et pas 1238 : les objets se partagent une
poignée d'effets visibles.

**Et le résultat le plus utile est ailleurs dans le compte** : sur les **1 440 naissances**
relevées, **zéro** emploie un sous-type `≥ 0xc0`. Tout passe par la table statique `0x00551c30`,
la même dans les onze zones — **un objet fait exactement la même chose où qu'on soit**. Écrire ces
dix-huit corps une fois les couvre partout ; il n'y a pas de variante par carte à redouter.

Le tableau est **trié par nombre d'emplois**, ce qui donne l'ordre de travail directement. Les
trois premiers restants :

- `0x00446DD0` (sous-type `0x3e`) — **189 emplois** ;
- `0x00445F60` (`0x44`) — 166 ;
- `0x004462B0` (`0x43`) — 162.

## Pourquoi on peut s'y fier

Le dérouleur a été vérifié avant d'être cru : la **Petite potion** se décode en opcode 17
(recherche de cible, mode 0), puis naissance du sous-type `0x40` → `0x00446790`. **Ce sont
exactement les deux levées rencontrées en jeu, dans cet ordre.** Il les avait toutes les deux sans
qu'on ait eu besoin de jouer.

## Ce qu'il a corrigé au passage

**Une potion ne soigne pas par la table des 48 effets.** Elle soigne par l'**opcode 6**, l'unité
arithmétique, sur le champ `+0x4c` du bloc de la cible. La table des 48 sert aux altérations d'état
et aux guérisons des sorts et des pièges. Seuls **13 des 48 genres** sont atteignables par un
objet — le tableau dit lesquels, donc lesquels valent un essai en jeu.

## La seule lacune, et elle est bornée

L'**opcode 14** désigne une fiche du répertoire des sorts, dont l'octet `+0x10` porte le sous-type
réellement engendré. Ce répertoire n'est pas dans `objets.csv` : les paires famille/indice sont
listées sans être résolues. Les résoudre demanderait d'étendre le verbe `bestiaire` au répertoire
des sorts — c'est le prochain pas si les 18 ne suffisent pas.

## Les deux tableaux, et leurs deux colonnes

`ObjetsAtteignables.md` porte maintenant **la même lecture en deux colonnes que
`GestionnairesEntites.md`**, sur les deux niveaux :

- **les 21 gestionnaires** — `implemente` (un `case` du répartiteur) et `teste` (la sonde des
  gestionnaires, `handlers-exerces.tsv`) ;
- **les 607 objets distincts, un par un** — `implemente` veut dire *tous* les corps que ses scripts
  font naître existent, `teste` veut dire que l'objet a réellement été employé en jeu.

**La sonde d'objets est nouvelle** (`Documentation/Etat/objets-exerces.tsv`) : elle s'accroche à
`RunItemScript_00523AA0`, donc elle voit les deux scripts — l'emploi et le déclenchement. Sa clé
est **l'hexa des seize premiers octets de la fiche**, pas le nom : les noms d'objet ne sont pas de
l'ASCII, et une clé lisible sortait en mojibake. Le recensement recalcule la même clé depuis
`objets.csv` pour retrouver le nom.

État de départ : **369 des 607 objets sont pleinement implémentés**, 0 exercé — le fichier de sonde
n'existe pas encore, il naîtra à la première partie.

`implemente = ?` marque les objets dont tout le résolvable est écrit **mais dont le script emploie
l'opcode 14** : leur sous-type engendré vit dans le répertoire des sorts et n'est pas résolu. Ceux-là
peuvent encore lever une fois.

## Le raccourci

**`tests\objets.bat`** — double-clic après une séance, le pendant de `feuille-de-route.bat`. Il
régénère `ObjetsAtteignables.md` et réaffiche le compte, les gestionnaires à écrire, la dette, et
le **contrôle croisé**.

## L'angle mort que le contrôle croisé a déjà trouvé

**`objets.csv` ne porte pas l'équipement de départ.** Dès la première partie, la sonde a relevé
`Epee normale`, `Armure cuir` et `Bouclier cuir` — absents du catalogue. Le verbe `bestiaire` dumpe
les catalogues **par sous-zone** (ce qu'on ramasse, ce que les coffres donnent), pas ce que la
création de personnage accorde.

Les 607 objets ne sont donc **pas** tout ce qui existe. Le rapport liste ces orphelins dans « Vus
en jeu, absents du catalogue » et le `.bat` les réaffiche : les deux sources se contrôlent l'une
l'autre. Si cette liste s'allonge beaucoup, c'est le signal qu'il faut étendre le verbe `bestiaire`
à l'équipement de départ.

## La stratégie, en trois lignes

1. Descendre le tableau des gestionnaires de haut en bas — le plus employé d'abord, c'est le plus
   probable en jeu.
2. Régénérer après chaque lot : les quatre colonnes se mettent à jour toutes seules.
3. Jouer normalement en gardant les deux sondes armées. Elles ne servent plus à *découvrir* mais à
   **prouver** : un objet ne passe à `teste = oui` que parce qu'on s'en est réellement servi.

---

# LA TABLE DES 48 EFFETS EST ÉCRITE — À EXERCER (2026-08-30)

**Prendre une potion dans le menu rapide a un chemin complet** : `0x00474ad0` (la recherche de
cible) et les 48 entrées de `0x0056d4e0` sont transcrites. Détail dans `REVERSE_STATUS.md`,
« suite 36 ».

**Écrit, presque rien d'exercé.** Cinq des 48 avaient déjà tourné par les pièges de coffre ; les 43
autres n'ont jamais tourné. Ce qui se vérifie facilement en jeu, dans l'ordre :

- **une potion de soin** — l'entrée 43 : les points de vie montent, un nombre monte au-dessus de la
  cible, une particule naît. Le nombre affiché est le montant **entier**, même si le plafond en a
  mangé une partie : c'est fidèle, ce n'est pas un défaut ;
- **une potion sur un compagnon** plutôt que sur soi — c'est le mode 0 de la recherche de cible, et
  le cône est large (0x100 d'ouverture, 0x96 de profondeur) : il faut être près ;
- **les altérations d'état**, par un sort ou un piège : la marque `0x29` ne naît **qu'à la première
  pose**, et une seconde application ne fait que rallonger la durée. Voir deux marques sur une même
  cible ne serait normal que pour l'effet 32, le seul qui s'empile (jusqu'à trois).

**Ce qui reste sur ce chemin** : l'opcode 22 du script d'objet, une levée volontaire — un défaut de
1998 jamais observé, décrit sur `ItemOpOpenItemMenu`.

---

# LES VERBES DE ZONE DE LA FORÊT — TABLE COMPLÈTE (2026-08-30)

**Les treize verbes distincts de la table `0x0056aec8` en zone 3 sont transcrits**, souches
comprises. Le dernier — `0x004433b0`, le verbe 13, 1150 octets — est le rassemblement du groupe sur
l'un des quatre emplacements des deux clairières ; détail dans `REVERSE_STATUS.md`, « suite 35 ».
Une arrivée sur une carte de forêt ne devrait plus lever de `verbe … pas encore reconstruit`.

**Écrit, jamais exercé.** Le corps compile et est branché ; aucune image de jeu ne l'a encore
traversé. Ce qu'il faut voir en jeu : arriver dans l'une des deux clairières, y **rassembler tout
le groupe** sur l'emplacement, et vérifier que l'énigme bascule. Un membre resté en arrière, en
l'air ou marqué `0x2000` suffit à faire échouer le test — c'est voulu, pas un défaut.

**Ce que le prochain plantage de verbe apprendra.** Les dix autres zones ont chacune leur table ;
`GestionnairesEntites.md` dit lesquelles sont couvertes. Le message de `ScriptOpAreaVerb` donne
déjà l'adresse et la ligne de `disasm` à taper.

---

# LES PIÈGES DE COFFRE — ÉCRITS EN ENTIER, TESTÉS EN PARTIE (2026-08-29)

**Les douze pièges `0xb4`..`0xbf` de la table statique `0x00551c30` sont transcrits, mais Vincent
n'a pas pu tous les déclencher en jeu.** Trois ont été vus (`0xb4`/`0xb5` au premier signalement,
`0xb6` puis `0xb9` ensuite) ; les autres n'ont jamais été atteints, les taux d'apparition étant ce
qu'ils sont dans le jeu d'origine.

**Ce qui reste donc à vérifier, quand l'occasion se présentera :**

- `0xb7` (`0x00475A30` → `0x40`), `0xb8` (`0x00475B90` → `0x5f`), `0xba` (`0x00476770`),
  `0xbb` (`0x00476730`, la secousse d'écran), `0xbc` (`0x00475170`, deux enfants),
  `0xbd` (`0x004753D0`) ;
- les trois applications d'effet `0x0053B4B0`, `0x0053B580`, `0x0053B660` et le second jet de
  sauvegarde `0x0053A790`, qui ne s'exercent que par ces pièges-là ;
- **un coffre dont l'octet de piège vaut `0x01`..`0x3f`** : le sous-type serait alors `0xc1`..`0xff`
  et résolu par la table d'entités de la **sous-zone**, donc n'importe quel corps de la zone. Rien
  ne garantit qu'il soit transcrit.

Le détail du mécanisme — le piège est nommé par l'octet haut de `+0x34` du coffre, plus `0xc0`
replié sur huit bits — est dans `REVERSE_STATUS.md`, section « suite 18 ».

**Symptôme à guetter** : une levée `FUN_0047aa70, table 0x005ff400 … gestionnaire d'entité 0x…`
juste après l'ouverture d'un coffre. Le sous-type est l'octet `+5` du relevé que le message affiche.

---


# LA FORÊT — LE COMBAT MARCHE, MAIS L'IA DES ESPÈCES EST UNE FAMILLE (2026-08-28)

Cinq plantages signalés en jeu le même jour, tous réglés :

| symptôme | cause | état |
|---|---|---|
| Select en forêt | sous-type `0xbd`, `FUN_004765D0` absente de `Blaze.c` | écrite |
| relâchement de Select | `FUN_0051a5d0`, la combinaison de directions | écrite |
| apparition d'un groupe (sous-type 2) | `FUN_0049A980` (2175 o.) absente de `Blaze.c` | écrite |
| le coup qui porte | `FUN_00535A90` (portée) puis `FUN_00539B80` (dégâts) | écrites |
| apparition d'un groupe (sous-type 0) | `FUN_00499DE0` (1377 o.) absente de `Blaze.c` | écrite |

**Le chemin de combat est complet** : apparition, décision, geste, portée, parade, dégâts, mort.

## Ce qui va se répéter — LE COMPTE EST FAIT

**Voir `Documentation/Ennemies/Comportement.md`**, écrit le 2026-08-28. Le relevé remplace
l'estimation « une quarantaine » qui figurait ici : elle était un plancher tiré d'un balayage trop
étroit.

| | |
|---|---|
| gestionnaires distincts installés sur `0x0056ad48` | **166** |
| dont routines d'intelligence complètes | **112** |
| écrites | **10** |
| pour la forêt (zone 3) seule | 22 d'IA, dont 9 écrites |

Ce n'est **pas une fonction par ennemi** : 555 espèces pour 112 comportements. Une routine est un
comportement partagé.

**Le prochain pas concret** : les deux routines restantes du bloc d’arrivée de la forêt
(sous-types 9 et 18 → `0x0049E190`, `0x0049E920`). **Le projectile du Goblin est écrit** — le
sous-système de tir (`0x0047A100`, `0x0047A270`, `0x00476320`) est complet. La recette — dont le
**tri de départ en quatre lectures** et le calque **à deux sœurs** — est dans `Comportement.md`.

## Lacunes secondaires, gardées et mortes en jeu normal

Les tracés de mise au point de `FUN_00535a90` (`0x00535d1d`..`0x00535ec8`, il lui manque le
descripteur `&DAT_0056aca0`) et de `FUN_00539b80`. Ils ne s'atteignent que par la touche de
débogage, elle-même non transcrite.

Détail dans `REVERSE_STATUS.md`, sections du 2026-08-28.

---

# L’UTILISATION DES OBJETS — ÉCRITE, JAMAIS EXERCÉE, CHANTIER GELÉ (2026-08-26)

`FUN_00523aa0` et ses 23 opcodes sont transcrits, **onze corps depuis le désassembleur** (Ghidra ne
les avait pas décompilés). Les deux entrées de la machine sont vivantes :

| entrée | script | quand |
|---|---|---|
| `RunItemScriptSaved_00523860` | `item + item[0x80]` | on se sert de l'objet (genre 5 du menu rapide) |
| `RunTriggeredItemScript_00523A20` | `item + item[0x82]` | un événement touche un objet équipé (`TriggerEquippedItems_005238E0`) |

Build vert, aucune régression sur le menu rapide.

**Décision du Responsable, 2026-08-26 : on laisse tomber l'utilisation des objets pour le moment.**
Le chantier est gelé en l'état — rien à reprendre dessus, ni les deux frontières ci-dessous, ni
l'opcode 22, tant que le premier donjon n'est pas atteint.

**Et ce test est au Responsable (Vincent), pas à l'agent.** Il faut jouer jusqu'au donjon : c'est un
test de navigation, donc du côté de Vincent selon le partage de `test-protocol.md`. L'agent ne
reprend ce chantier que sur ce que ce test aura montré.

## RIEN DE TOUT ÇA N'A TOURNÉ — ET ÇA ATTENDRA LE PREMIER DONJON

Le jeu refuse « se servir d'un objet » dans l'auberge, et aucun des sept sites de
`TriggerEquippedItems_005238E0` ne se déclenche dans la salle d'arrivée. **À exercer au premier
donjon** — c'est là que ça se verra :

- boire une potion (entrée `item[0x80]`, opcodes 0, 3/5, 12, 21 attendus) ;
- porter un équipement qui réagit — poison, coup reçu, entrée de zone (entrée `item[0x82]`).

## Les deux frontières, qui lèveront si on les touche

- **`PickTargetBySearch_00474AD0`** (opcode 17) — 47 lignes, mais elle ouvre le ciblage de combat :
  `FUN_00519af0`, `FUN_00519c30`, puis `FUN_00535a90`, `FUN_00538af0`.
- **`ApplyItemEffect_0056D4E0`** (opcode 18) — la table des **48 effets**, `0x0053afb0`..`0x0053cdf0`,
  partagée avec les sorts. C'est un sous-système à part entière.

Si une potion lève sur l'une des deux, c'est elle la suite du chantier — et on saura enfin laquelle
des deux le jeu emprunte pour soigner.

## Et un piège de 1998 qui dort

L'opcode 22 (ouvrir l'écran du menu objets depuis un script d'objet) rend un **curseur de liste
d'affichage** là où la boucle attend un curseur de script. La transcription fait les deux appels de
l'original puis lève, plutôt que d'inventer une valeur. Si cette levée sort un jour, c'est qu'un
script émet vraiment l'opcode 22 — et il faudra alors trancher ce que faisait la vraie machine.


# LE MENU RAPIDE FONCTIONNE (2026-08-26)

Espace ouvre l'anneau au-dessus du personnage, les flèches parcourent catégories et entrées, la
validation équipe et recalcule les statistiques, la croix referme. `FUN_00530070` est transcrite en
entier (`TickQuickMenu_00530070`) — le nom « le recul après un coup reçu » qu'elle portait était
faux, c'est le pilote du menu.

**Exercé par l'agent, capture à l'appui** (l'anneau s'ouvre sur place, donc hors du partage du
2026-08-24) :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 5 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT6,ENTER,WAIT5,ENTER,WAIT6,ENTER,WAIT2,ENTER,WAIT2,ESC,WAIT4,`
SPACE,WAIT2,DOWN,WAIT2,DOWN,WAIT2
```

Vu : `ARME` à l'ouverture, `BOUC` après deux `DOWN`, fermeture propre au second `SPACE`, aucun
plantage sur ouvrir → entrer → annuler → fermer.

## Ce qui reste

~~Un seul trou : `FUN_00523aa0`~~ — **écrite le 2026-08-26**, voir la section du dessus. Le menu
rapide n'a donc plus de levée à lui.

**Confirmé en jeu par Vincent le 2026-08-26** : équiper un bouclier ou une armure fonctionne.
**Toujours pas exercé** : les accessoires (il n'en avait pas), se servir d'un objet (le jeu
l'interdit dans l'auberge), le genre 6 (les sorts) et le genre 9 (passer la main). Au premier
donjon — **et c'est le Responsable (Vincent) qui l'exercera**, pas l'agent.

## Deux corrections de fond au passage

- **`stackalloc short[2]` pour un `ProjectPoint` qui écrit huit octets** : le témoin de pile du JIT
  tuait le processus par `0xC0000409` dans l'épilogue, **sans une ligne de journal**. Deux sites.
  La recette de diagnostic est dans `test-protocol.md`.
- **Recouvrement d'arène sur `0x005de154`/`_158`/`_15c`/`_160`** : la bulle se dessinait dans le coin
  haut gauche. Le `TODO(REVERSE)` d'`Engine/UiWindow.cs` disait « ne se manifeste pas » — il vient
  de se manifester. Le site fautif est corrigé ; le recouvrement, non.


# À VÉRIFIER EN JEU : la salle du barman

**Deux gestionnaires manquaient sur ce trajet, pas un.** Les deux sont transcrits, le build est
vert, mais **aucun des deux n'est exercé** : la salle du barman ne s'atteint pas en frappes
minutées. À refaire pour confirmer que le plantage a disparu.

| Passage | Branche | Sous-type | Cible | Transcrit |
|---|---|---|---|---|
| écran suivant, 1er essai | basse (`< 0xC0`) | `0x59` | `0x00403710`, l'effet de soin | ✔ |
| salle du barman | **haute** (`>= 0xC0`) | `0xe1` | `0x004055C0`, le 2e balayeur d'entités de scène | ✔ |

**Relever la ligne d'exception entière est ce qui a tranché.** Le premier essai n'en portait que le
début, et la déduction faite dessus était à moitié fausse — voir ci-dessous.

## La leçon du jour : symbole ≠ adresse

J'ai écrit le matin que `0x00403710` était **le seul** créneau atteignable de cette passe, parce que
`&DAT_005ff405` n'a qu'un seul écrivain dans `Blaze.c`. Vrai pour la **branche basse**, et muet sur
tout le reste : **six autres sites écrivent directement dans `DAT_005ff400`** — enregistrement posé
en bloc par le chargeur de zone — et y mettent des sous-types `>= 0xC0`.

Compter les écrivains d'un **symbole** ne compte pas les écrivains d'une **adresse**. Un `grep` sur
`DAT_005ff405` ne voit pas un `memcpy` vers `DAT_005ff400`. Le raisonnement était bon pour ce qu'il
couvrait ; l'erreur a été de ne pas borner sa portée en l'écrivant.

---

# LE COFFRE EST COMPLET — À EXERCER SUR UNE COPIE DE `save\`

`FUN_0053e0a0` ne porte plus aucune levée : ses deux modes, ses vingt états, son automate d'attente
de périphérique, son prologue et sa queue d'affichage sont transcrits. Build vert.

## AVANT DE TESTER

**Copie ton dossier `save\`.** Les états 1, 5 et 8 écrivent le fichier : `FUN_00504c20` y écrit
0x2000 + 0x1f00 + 0x100 octets, recalcule la somme de contrôle et réécrit le bloc d'icône. Ces
chemins n'ont **jamais** été exécutés.

## Ce qu'il faut regarder, dans cet ordre

1. **le panneau s'ouvre** et ressemble à `Tests\2. Coffre de stockage\legacy.jpg` ;
2. **la navigation** répond — cinq lignes, la fiche suit le curseur ;
3. **valider** bascule le drapeau « garde » (trois refus prévus : objet équipé, dix objets déjà
   tenus, rien à rendre) ;
4. **ressortir sans rien changer** → doit partir directement, sans écrire ;
5. **ressortir après un échange** → là seulement le fichier est écrit.

Si quelque chose cloche, `-Trace VERBS,SCRIPT,ENTITY` est toujours en place.

## Les trois points les plus risqués, si un défaut apparaît

- la **somme de contrôle** porte sur des octets **signés** ;
- les trois écritures sont combinées par **ou binaire**, testées une fois ;
- le **drapeau de succès** (`DAT_005dff20`) est posé en dernier — c'est lui qui décide si l'état 9
  remet les objets d'avant.

Détail dans `REVERSE_STATUS.md`, sections « suite 25 » à « suite 32 ».

---

## Vérifié en jeu le 2026-08-25

**Les trois verbes de l'homme qui expertise fonctionnent** — 56, 57 et 58, « suite 19 » et « suite 20 ».

**La chambre de l'homme qui expertise fonctionne** — les six balayeurs d'entités de scène sont
confirmés, « suite 18 ».

**Le changement de musique de la femme à la petite harpe fonctionne** — `FUN_00404e50` et le contenu
initial du bloc d'écran `0x00566c48` sont confirmés, « suite 17 ».

**La salle derrière le barman s'ouvre et ses PNJ répondent** : l'**opcode 17** (le changement de
salle) et le **troisième balayeur d'entités de scène** sont confirmés — « suite 15 » et « suite 16 ».

**Les dialogues de biais fonctionnent.** Cela confirme deux corrections qui tombaient toutes les
deux sur ce geste :

- **la racine de ressources 3** du moteur de texte (`FUN_00544a80`), qui levait sur toute boîte de
  dialogue passant par le répertoire de zone — `REVERSE_STATUS.md`, « suite 13 » ;
- **LB-013**, la clé de saut lue deux octets après la fin de sa table dans `DrawChunk`, qui ne
  plantait que dans l'octant 7 de cap de caméra — « suite 14 ».

Les deux avaient été exercées par sonde, pas par le jeu. C'est le trajet de Vincent qui les a
closes.

---

# LE MENU DE JEU EST ENTIÈREMENT TRANSCRIT

**Plus aucun corps ne manque à `FUN_0052a850`** : les six pages (la fiche de personnage, les objets de
quête, la dalle aux cristaux, l'organisation du groupe, la carte du monde et ses repères,
« Options ») et les six onglets de la fiche (statistiques, inventaire, équipement, sorts, herbes,
configuration de la manette). Les trois levées qui restent sont des gardes sur des cas que les
bornes rendent impossibles, pas des trous.

Chacun a été vérifié à l'écran. Deux vérifications valent d'être notées : la **sauvegarde** l'a été
sur le disque, avec un fichier de 0x6000 octets écrit dans `save\` ; et les **onglets 3 et 4**, que
le personnage de départ n'a pas, l'ont été avec une sonde d'une ligne sur la ligne de classe (voir
`test-protocol.md`).

## La suite

Le menu ne bloque plus rien. Ce qui reste au projet est ailleurs — les 120 corps décompilés des
tables de gestionnaires, les 152 désassemblages, et ce que le jeu nommera au prochain plantage. Voir
plus bas.

**Le menu n'a plus aucune dépendance manquante** : ses quarante-trois appelées sont transcrites.

**L'agent vérifie tout ça lui-même** — le menu s'ouvre sur place, sans conduire le personnage. Voir
`test-protocol.md`.

```powershell
# fiche → inventaire → équipement → choisir un emplacement → échanger
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 55 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT6,ENTER,WAIT5,ENTER,WAIT6,ENTER,WAIT2,ENTER,WAIT2,ESC,WAIT3,`
START,WAIT4,RIGHT,WAIT3,RIGHT,WAIT3,ENTER,WAIT3,ENTER,WAIT3,ENTER,WAIT8
```

## L'ordre à suivre — tout est fait

1. ~~**L'onglet 5**~~ — fait le 2026-08-25. `Blaze.c` 76446..76571, binaire
   0x0052cf0a..0x0052d2b4 ; vérifié à l'écran, les trois rangées répondent.
2. ~~**La page 1**~~ — faite le 2026-08-25. C'est la page « Objets », celle des objets de quête ;
   binaire 0x0052d2b9..0x0052d50b. Vérifiée à l'écran avec une sonde qui lève quatre drapeaux.
3. ~~**La page 2**~~ — faite le 2026-08-25. C'est la page « Table », la dalle aux cristaux ;
   binaire 0x0052d510..0x0052d60e. Vérifiée à l'écran, sonde posée **dans `FUN_00535710`** et non
   sur la banque (voir `test-protocol.md`).
4. ~~**La page 3**~~ — faite le 2026-08-25. C'est « Organisation du groupe » ; binaire
   0x0052d613..0x0052db32. Vérifiée à l'écran **sans sonde** : un `PGUP` depuis la page 0 y tombe
   directement sur une partie neuve.
5. ~~**La page 4**~~ — faite le 2026-08-25. C'est la carte du monde et ses huit repères ; binaire
   0x0052db5b..0x0052e33e. Vérifiée à l'écran : le défilement, la pose d'un repère, et le groupe en
   modèle 3d. Elle se pilote au `HOLD:`, pas au tapotement — voir `test-protocol.md`.
6. ~~**La page 5**~~ — faite le 2026-08-25. C'est « Options » : sauvegarder ou revenir au menu
   d'accueil ; binaire 0x0052e343..0x0052f568. Vérifiée à l'écran **et sur le disque** — la
   sauvegarde écrit un fichier de 0x6000 octets dans `save\`.
7. ~~**Les onglets 3 et 4**~~ — faits le 2026-08-25. Ce sont les sorts et les herbes. Vérifiés à
   l'écran avec une sonde d'une ligne sur `classRow`, la seule façon de les atteindre avec le
   personnage de départ.

```powershell
# fiche → l'onglet 5, entrer dedans et changer les trois valeurs
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 60 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT6,ENTER,WAIT5,ENTER,WAIT6,ENTER,WAIT2,ENTER,WAIT2,ESC,WAIT3,`
START,WAIT4,RIGHT,WAIT3,RIGHT,WAIT3,RIGHT,WAIT4,ENTER,WAIT3,RIGHT,WAIT2,DOWN,WAIT2,RIGHT,WAIT2,`
DOWN,WAIT2,LEFT,WAIT6
```

Les tableaux de découpage sont dans `REVERSE_STATUS.md`, section du 2026-08-25 (suite 2) ;
l'épilogue partagé dans la suite 3, l'inventaire dans la suite 4, l'équipement dans la suite 5.

## Ce qui reste à savoir

* **La fidélité du rendu n'est pas vérifiée.** Les captures sont cohérentes et lisibles, mais aucune
  image de référence ne leur a été comparée. Le fond notamment est l'image précédente restée dans le
  tampon — la branche courte ne redessine pas le monde — et il apparaît teinté. **C'est un test pour
  Vincent.**
* **Un recouvrement latent, relevé et non corrigé** : `DAT_005de0c8` (le tampon de nom d'objet) finit
  à `0x005de109` et mord de neuf octets sur `s_ItemMenuArena_005de100`. La divergence ne se voit
  qu'avec un nom de 63 caractères, ce qu'aucun nom du jeu n'atteint. Même TODO(REVERSE) que dans
  `Engine/UiWindow.cs`.
* ~~`FUN_0051aa30` (598 l.), l'autre moitié de la branche courte~~ — **transcrite le 2026-09-15** :
  c'est le menu de mise au point (`Screens/State03/WorldMapDebugMenu.cs`). Les cinq sites que sa ligne
  MONITOR allume sont transcrits le même jour.

---

# DEMAIN AUSSI : la suite des 121 corps décompilés

`FUN_00445860` — le premier de la liste, celui que le TODO de la veille désignait — est fait. Il
reste **120 transcriptions ordinaires** et 152 désassemblages sur les trois tables statiques.

Aucun autre créneau n'a de chemin reconstruit qui y mène de façon démontrée : les deux suivants se
choisissent donc au jugé, ou en attendant qu'un plantage en nomme un. La table complète, adresse
par adresse, est dans `REVERSE_STATUS.md`, sections du 2026-08-24 (suites 12 à 14).

**Un troisième balayeur d'entités de scène attend** : `FUN_00405a70`, qui réveille les types 0 à 4
avec encore d'autres corps (`FUN_00405b30`, `FUN_00405be0`, `FUN_00405cb0`, `FUN_00405d20`). Rien
ne démontre qu'il soit atteint — à faire quand un plantage le nommera, comme les deux autres.

**Deux TODO(REVERSE) ouverts le 2026-08-25**, tous deux avec assez de matière pour être repris :

* **`FUN_0053dcb0` recopie `+0x148` dans `+0x14c` à chaque image.** Avec la lecture désormais tenue
  par deux témoins — `+0x148`/`+0x14a` le maximum, `+0x14c`/`+0x14e` le courant — cela remettrait
  les points de vie au maximum en permanence. Les deux transcriptions sont fidèles au binaire, les
  deux désassemblages sont dans le dépôt : **il manque une pièce**, pas une correction.
* **Les huit octets `f0 80 0f 0f 01 00 11 00` en `0x00551640`** précèdent un flux d'étiquettes et
  ne sont écrits par personne. Un en-tête de boîte, vraisemblablement — le flux du développeur en
  `0x00551814` n'en a pas.

> ## LA CARTE DU MONDE TOURNE
>
> ```powershell
> OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 44 -WorkingDir OpenForesia -Shot -Then WAIT5,ENTER,WAIT2,START,WAIT6,ENTER,WAIT5,ENTER,WAIT20
> ```
>
> rend **« aucun plantage »**, ~2000 images, 557 primitives par image, et la capture montre
> **l'auberge** : murs de bois, escalier, sol de pierre, plante, porte, le personnage avec son épée,
> la rose des vents, et le panneau « MISEUR 16/16 2/2 ».
>
> C'est la première fois. Le jeu ne s'arrête plus.

# CE QUI A ÉTÉ FAIT LE 2026-08-25

* `FUN_00403710`, l'effet de soin — le seul créneau atteignable par la **branche basse** de
  `FUN_0047aa70`.
* `FUN_004055C0` et ses cinq réveils (types 3, 4, 5, 6, 7) — le second balayeur d'entités de scène,
  atteint par la **branche haute**. C'est le plantage de la salle du barman.
* `FUN_00445860`, l'afficheur d'énoncé — entrée `0x9d` de la table de `FUN_0047acf0`.
* `s_EntityParams_00551640` semée depuis le binaire : ses 0x200 octets ne sont **pas** nuls au
  chargement, et deux flux d'étiquettes inédits y dormaient (« 開かない！？ », « 遠くで音がした。 »).
* La **branche courte d'affichage** de `FUN_00516fc0` — le dernier trou de la boucle par image.
* **Le menu de jeu**, dans le nouveau `Screens/State03/WorldMapMenu.cs` : `FUN_0052a7d0`, le
  prologue et l'épilogue partagé de `FUN_0052a850`, la page 0 et ses onglets 0 (statistiques), 1
  (inventaire), 2 (équipement) et 5 (configuration de la manette), puis les pages 1 (« Objets ») et
  2 (« Table », la dalle aux cristaux) 3 (« Organisation du groupe »), 4 (la carte du monde) et 5 (« Options »), et les six onglets de la fiche. Plus les quatre appelées qui manquaient — `FUN_0052fab0`,
  `FUN_0051bd50`, `FUN_0052fba0`, `FUN_0052fdc0` : **le menu n'a plus aucune dépendance**.
* Les blocs de données du menu relevés depuis le binaire : la rangée de quatre descripteurs
  `0x00568530`, les trente-trois cellules de sprite `0x0056aae0`, les six flux d'étiquettes de
  `0x0056ac30`, les tables d'onglets et d'ordre, les trois listes de décorations et les deux chaînes
  d'objet non identifié.
* `Documentation/Etat/NOTE_DEVELOPPEUR.md` — nouveau, à la demande de Vincent : le registre des
  traces humaines restées dans le binaire. Six entrées pour commencer. **Y ajouter une entrée quand
  on en croise une.**

Détail dans `REVERSE_STATUS.md`, sections du 2026-08-25, et dans `DataSymbols.md` pour la plage.

# CE QUI A ÉTÉ FAIT LE 2026-08-24

## Les tables de gestionnaires d'entité — les trois branches basses câblées

Les trois passes `FUN_0047aa70` / `FUN_0047ab70` / `FUN_0047acf0` aiguillaient tout sous-type
`< 0xC0` vers une table statique du binaire, et cette branche entière levait une exception. Les
trois tables sont relevées et matérialisées.

| Table | Passe | Créneaux morts | Corps réels | dans `Blaze.c` | au désassembleur |
|---|---|---|---|---|---|
| `0x00551c30` | `FUN_0047aa70` | 69 / 192 | 123 | 63 | 60 |
| `0x00551f30` | `FUN_0047ab70` | 109 / 192 | 83 | 26 | 57 |
| `0x00552230` | `FUN_0047acf0` | 125 / 192 | 67 | 32 | 35 |
| **total** | | **303 / 576** | **273** | **121** | **152** |

Le 2026-08-28, **la première de ces 152 est faite** : `0x004765D0`, le sous-type `0xbd` de
`0x00551f30` — l'auréole d'incantation, et le plantage de Select en forêt. Voir
`REVERSE_STATUS.md`, section du 2026-08-28.

**303 créneaux sur 576** se comportent maintenant correctement, pour trois instructions : la souche
`0x0047ADC0` n'est pas un `ret` nu, elle **éteint** l'entité (`*enregistrement = 0`), et la passe
relit le mot d'état juste après pour rendre le créneau au pool.

Les trois tables sont **disjointes deux à deux** — leur seule cible commune est cette souche — et se
suivent sans trou de `0x00551c30` à `0x0055252f`, ce qui borne chacune sans avoir à deviner sa
taille.

**Deux pièges payés, à ne pas rejouer :**

* le message d'exception affirmait que ces gestionnaires ne prennent **aucun** argument, d'après le
  `(*(code *)…)()` de Ghidra. Faux : le `push` est fait **avant** le branchement, et le `add esp,4`
  nettoie les deux branches — `__cdecl`, un argument, l'enregistrement ;
* `PTR_LAB_` ne veut **pas** dire « absent de `Blaze.c` ». 121 des 273 corps y sont décompilés.
  L'affirmation contraire, écrite deux fois dans la journée, généralisait un échantillon de sept
  adresses.

## Le reste de la journée

Les douze gestionnaires d'entité de la table `0x0056adc8`, `FUN_00472e80` et sa queue, le bloc
d'étiquettes `0x00551814` (un message de développeur resté dans la version vendue), le double
`LAB_00474320`, et le correctif de `TestTriggerVolumes_00534CD0`. Détail dans `REVERSE_STATUS.md`,
sections du 2026-08-24 (suites 1 à 14), et lignes de progression dans `HISTORY.md`.

**Une correction non vérifiée** : `TestTriggerVolumes_00534CD0` lisait le compte des morceaux en
attente sur un `short` alors que le binaire fait `movzx ax,byte`. C'est la cause probable du
`0xC0000005` signalé dans la seconde grande pièce à droite de l'auberge, mais **le symptôme n'a pas
été reproduit** — à confirmer en jeu.

---

# L'ÉTAT D'AVANT (2026-08-24, matin)

**Le jeu a le son.** Musique et bruitages fonctionnent, du menu à l'auberge. Ce qui bloquait
n'était pas la pile audio — elle était complète — mais trois corps de fonction que Ghidra n'avait
jamais décompilés, dont celui qui crée le périphérique DirectSound *et* reçoit la fenêtre
principale. Détail et méthode dans `REVERSE_STATUS.md`, section du 2026-08-24.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 30 -WorkingDir OpenForesia -Shot -Then WAIT5,ENTER,WAIT2,START,WAIT6,ENTER,WAIT5,ENTER,WAIT10
```

donne trois lignes `[AUDIO] … statut 0x5` — `Title01.WAV`, `Menu.WAV`, `Mel00_01.WAV` — et une
fenêtre intitulée « Blaze & Blade ».

**La méthode à réutiliser** : quand une globale d'API paraît n'avoir aucun écrivain, ne pas
conclure depuis `Blaze.c`. Balayer les octets de `.text` à la recherche des `call` vers le thunk
d'import. Un trou du décompilateur et une absence réelle sont indiscernables dans le texte
décompilé, et se distinguent en une passe dans le binaire.

## Ce qui reste au son

* **Les identifiants `0x3c`..`0x4c` sont muets.** C'est le défaut d'aliasing de
  `s_SoundBankHeader_005e8520` (alloué à part alors que `0x005e8520 == 0x005e8340 + 0x1e0`, la
  queue du répertoire de sons). Le corriger change le comportement : c'est un changement à part
  entière, comme les autres défauts d'aliasing recensés.
* **Les entrées `0x22`..`0x24` de la table des morceaux** pointent sur `&DAT_00572b40`, que seul le
  code d'écran `0x00505xxx` remplit. Elles se rempliront avec lui.
* **LB-011** — au bouclage d'un morceau, la seconde passe de décodage écrit par-dessus la première.
  Défaut de 1998, à laisser tel quel.
* Un échafaudage de mesure subsiste dans `StartMusicStream` : la ligne `[AUDIO]` qui interroge
  `GetStatus`. À retirer quand le son sera routinier.

## Les entités de zone de la table 0x0056adc8 — terminées le 2026-08-24

*(Section conservée pour la méthode qu'elle porte ; la table elle-même n'a plus de lacune.)*

`NotImplementedException: FUN_0050e5e0, table 0x005fa5e0 …` : **ce n'est pas `FUN_0050e5e0` qui
manque**, elle est transcrite. C'est le gestionnaire d'entité que sa table de sauts (0x0056adc8)
désigne, et la **fin** du message le nomme — `gestionnaire d'entite 0x4XXXXX non transcrit`.

Quatre y ont été ajoutés le 2026-08-24, avec l'automate qu'ils partagent (`FUN_004727d0`). Le plus
important, `0x004734B0`, est la cible de **47 des 64 sous-types**.

**Ils compilent et se lisent contre `Blaze.c`, mais ne sont pas joués.** Une sonde au site de
dispatch montre que l'auberge n'anime que les sous-types 8, 9, 10 et 18, tous transcrits avant.
Le sous-type qui a planté n'apparaît pas sur ce parcours, et la ligne relevée était tronquée.

**La prochaine fois, relever la ligne d'exception entière** : elle porte le sous-type, l'adresse du
gestionnaire et les seize premiers octets de l'enregistrement. Le tableau complet de la table est
dans `REVERSE_STATUS.md`, section du 2026-08-24 (fin) — il mène de l'adresse au travail restant.

Les **trois** sous-types que `Blaze.c` couvrait encore — 12, 13 (le coffre) et 16 — puis les
**cinq** corps qui lui manquaient — 2, 6/7, 11, 14, 15 — ont été transcrits dans la foulée, ces
derniers depuis le désassemblage. Les 17 cibles distinctes de la table ont maintenant un cas dans
le répartiteur.

**La table n'a plus de lacune.** `0x004502E0` (sous-type 17), la dernière, a été transcrite le
2026-08-24 : 1042 octets exactement, un quasi-jumeau du coffre à cinq différences près, dont la
seule qui compte — au lieu de marquer un bit dans une banque de zone, il applique l'opération de
script `|=` au bloc de drapeaux de **chaque membre du groupe**. C'est un acquis permanent, pas du
butin. Avec lui, `FUN_0040e640`, la variante longue de `FUN_004742b0`.

`FUN_00472e80`, la queue de l'automate commun, a suivi le même jour : 1584 octets, six états
(200, 0xc9, 0xcd, 0xce, 0xd2, 0xd3), plus sa dépendance `FUN_00466d70` — onze lignes, alors qu'elle
était citée comme un obstacle. Ghidra avait perdu sa signature parce que l'état 0xcd **recycle les
emplacements d'arguments en variables locales** ; le troisième argument, lui, n'est jamais lu. Son
état 0xd2 rend une valeur jamais écrite : **LB-012**.

`LAB_00474320`, le rappel de fin de boîte du coffre, **n'était pas une lacune** : ses 87 octets
sont identiques à ceux de `0x00450700`, déjà transcrite, au seul déplacement relatif du `call`
près — et les deux visent `0x005312D0`. Le compilateur a émis deux fois le même corps. Les deux
adresses sont aiguillées sur la même transcription, et le coffre range réellement son contenu.

Le bloc d'étiquettes `0x00551814` est relevé, et il contenait **un message de développeur** :
« 片開きのドアは現在使用出来ません。今井まで連絡ください。 » — « la porte à un battant n'est pas
utilisable pour l'instant, contactez Imai ». 今井 est un nom propre. Les sous-types 6 et 7 sont donc
une porte **inachevée**, et le texte n'a jamais été traduit parce que personne n'était censé le
lire. Sa longueur ne se lisait pas dans un en-tête : un bloc d'étiquettes est un flux auto-terminant,
et deux `0x00` y sont des opérandes avant celui qui termine.

**La table 0x0056adc8 et tout ce qui gravite autour n'ont plus de lacune nommée.**

## La panne aléatoire de mémoire basse — corrigée le 2026-08-24

`No free block of 0x2000 bytes below 0x01000000`, signalée deux fois par Vincent, **ne tue plus une
session qu'exceptionnellement** — le 2026-08-24 au soir, un lancement a quand même échoué en
épuisant les **trois** tentatives de relance (journal : « relaunching (attempt 2 of 3) », puis
« (attempt 3 of 3) »). Le quatrième lancement, manuel, est passé. Le mécanisme fonctionne ; c'est
son budget qui peut s'épuiser un mauvais jour d'ASLR. Elle survenait sur cinq sites d'allocation basse pris paresseusement, donc en
cours de partie — dont trois pendant un chargement de zone, hors de portée de la relance en
processus fils qui rattrape la même panne au démarrage. Les cinq sont pré-réclamés.

Ce n'est pas rendre la panne impossible : la fenêtre de 16 Mo reste la fenêtre. C'est déplacer
toutes ses occurrences là où le programme sait s'en remettre. Analyse dans `REVERSE_STATUS.md`,
section du 2026-08-24 (suite).

Deux choses à savoir en lisant un journal :

* `[BAS] fenetre <0x01000000 apres reservations : libre 0x… , plus grand intervalle 0x…` — la marge
  basse, imprimée à chaque lancement. Elle varie de 3,4 à 11,5 Mo d'un lancement à l'autre ; c'est
  l'ASLR, et c'est le mécanisme de la panne.
* `[BAS] ATTENTION : <nom> alloue bas APRES le demarrage` — n'apparaît que si un **dixième** site
  d'allocation basse est ajouté sans rejoindre la liste pré-réclamée de
  `TryPrepareDisplayListArena`. Si elle apparaît, ajouter le site à la liste.

## Les pistes d'avant, toujours valables


Il n'y a plus de point de blocage : il y a un écran qui tourne et qu'on peut enfin **regarder**.
Trois pistes, par ordre de rendement.

## 1. ~~La caméra à l'auberge~~ — CORRIGÉE le 2026-08-23

`FUN_00401000` restitue `ViewAngle` depuis `SavedViewAngle` (`0x005712F8`) **avant** de semer le
but ; Ghidra avait supprimé ce bloc comme code mort, à tort — `DAT_005E9AE0` aliase la destination,
donc les lectures suivantes relisent ce que la première passe vient d'écrire. Élévation 597 et cap
1024 comme l'original, capture identique à `legacy.png`. Détail dans `REVERSE_STATUS.md`.

**Méthode à retenir** : c'est le point d'arrêt **matériel en écriture** de Vincent qui a tranché,
après quatre relevés de valeurs qui n'avaient rien donné. Quand deux implémentations partent du
même état et divergent sans raison visible, tracer les écrivains bat lire les valeurs.
## 1 bis. Continuer la comparaison à l'original, pas au compteur

La méthode vient de payer trois fois en une soirée. Points suivants à regarder :

* l'ordre de profondeur des faces de terrain (`DrawChunkCellsSorted` trie par face, ses deux sœurs
  non) ;
* les teintes des triangles plats de la deuxième liste — **LB-010** les réduit à deux valeurs,
  c'est volontaire mais visible ;
* les sept cellules que **LB-009** fait dessiner en trop aux quatre coins du carré 7×7.
## 2. ~~Refermer la porte de l'aliasing~~ — FAIT le 2026-08-23

`GameArenaSize` est passée de `0x1600` à `0x1560`, soit une arène qui s'arrête à `0x005ea560` et ne
recouvre plus `s_EntityStatBlocks_005ea560`. La borne est celle du balayage de `FUN_00516350` :
`undefined1 *`, 0x40 tours de pas 0x28 depuis `0x005e9b87`, donc dernier octet écrit en
`0x005ea55f` — l'ancienne borne comptait un enregistrement de trop (`0x40 * 0x28` au lieu de
`0x3f * 0x28`).

L'assertion `Aucune_allocation_propre_dans_l_etendue_d_une_arene` est **verte** : les tests de
disposition passent de trois rouges à deux. Playtest identique au précédent, image comprise — le
changement est neutre, comme attendu, puisque les deux seuls sites qui lisaient la table par
l'arène avaient déjà été corrigés.

## 3. Les lacunes que le jeu réclamera dès qu'on jouera vraiment

Elles se nomment toutes en levant ; aucune n'est sur le chemin au repos.

* **capturer le legacy sur le dialogue de l'herboriste et sur l'ouverture d'un coffre.** C'est
  devenu le contrôle le plus rentable : les deux marchent chez nous, la scène se peuple, plus aucune
  exception ne se lève sur ce chemin — et pourtant rien n'est *vérifié*. Aucune image de référence
  n'existe pour ces écrans, donc on ne sait ni si la conversation avance comme elle doit, ni si le
  cadrage de la caméra de scénette est le bon, ni **si l'icône de l'objet se pose au bon endroit** :
  les deux blits VRAM de `FUN_005312d0` sont la partie la plus facile à transcrire juste et à voir
  fausse. Seule l'arrivée à l'auberge a sa référence.
* l'entrée `0x46` de la table statique `0x00551c30` — le gestionnaire de l'effet « magie épuisée »,
  **absent de `Blaze.c`** : `OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x00551d48 4`, puis
  désassembler.
* le **sous-bloc d'occlusion** de `FUN_004011C0` (`0x00401913..0x00401b6d`). Il n'est **pas** sur
  le chemin au repos : `ProbeCameraOcclusion_004011C0` lève inconditionnellement et le playtest
  passe 1441 images sans exception, donc la garde `0x20` rend bien la main avant — chez nous comme
  chez Blaze, dont le relevé montre les mêmes drapeaux `0x60`. Il se réveillera quand le joueur
  prendra la caméra en main. Son blocage est **la donnée** : les tables `0x00604BA0` et
  `0x00604D30` ont une étendue que l'indexation apparente contredit, à trancher au désassembleur.
* `DAT_005eca94` nul dans `PublishCameraFrame_004022B0` : intermittent, **antérieur au 23 août**,
  et plus revu depuis la correction de l'aliasing — à surveiller plutôt qu'à chasser.

## Une fragilité à surveiller

Une exécution sur cinq a échoué dans `AllocLowZeroed` (« aucun bloc libre de 0x1000 octets sous
0x01000000 ») **au démarrage**. La fenêtre basse fait 16 Mo et chaque bloc y dépense un granule de
64 Ko. Ce n'est pas une régression, mais la marge est plus mince qu'il n'y paraît.

**Nouveau le 2026-08-23, et plus gênant** : Vincent a vu le même échec **pendant un chargement de
l'auberge**, pas au lancement. Aléatoire, une seule occurrence. Or la relance en processus fils qui
rattrape le cas du démarrage n'existe **que** là : une allocation basse qui échoue en cours de partie
tue la session. Six des neuf sites d'allocation basse ne sont pas pré-réclamés, dont trois qui
tombent exactement pendant un chargement de zone. Le détail, la correction candidate et surtout **ce
qu'il faut relever à la prochaine occurrence** sont dans `Documentation/Tests/test-protocol.md`,
§ « Le même échec pendant un chargement ». L'échantillon est de un : relever avant de corriger.

---

## Ce qui a été fait le 2026-08-23

**La moitié dessin, d'un trait.** `DrawWorldCells` (le balayage 7×7), `FUN_0051f940` (le voile plein
écran), `FUN_0051e700` (l'émission de modèle triée par face) et les trois listes de morceaux
`FUN_0051fa40` / `FUN_00521000` / `FUN_00522140`, ces trois-là réunies en un seul `DrawChunk`
paramétré. Les six lacunes que le TODO de la veille estimait à « deux séances ».

**La simulation, ensuite.** `FUN_0053aa10` (le pas d'états d'une entité de scène — elle n'anime
rien, malgré le nom que ce port lui avait donné d'avance), `FUN_0047ac40` (la troisième jumelle de
`SpawnEffectEntity`), `FUN_00404ef0` (le réveil des entités de scène), `FUN_00486ee0` (la bestiole
errante, 881 o., absente de `Blaze.c`) et `FUN_004db5a0` (l'avance d'image d'animation).

**Trois relevés** : `&DAT_00564548` et les huit tranches de `PTR_DAT_005645d8` (le balayage par
octant), et `&DAT_0056d340` (quarante-huit masques d'état). Tous dans `DataSymbols.md`.

**Deux défauts d'origine** : LB-009 (le balayage ne saute qu'une case sur huit) et LB-010 (un indice
de couleur réduit à un booléen), tous deux prouvés par comparaison avec le même code écrit deux fois
dans le binaire.

**Un défaut de reconstruction, celui qui bloquait tout** : `ResolveEntityBlocks` résolvait le bloc
de statistiques par `Mem.Arena(0x005ea560)` quand tout le reste de la simulation le lit par
`s_EntityStatBlocks_005ea560`. Deux noms, une adresse, deux blocs. Le détail — et la leçon sur les
assertions rouges « volontaires » qui cessent de l'être — est dans `REVERSE_STATUS.md`.


---

## Archive — les encadrés des séances précédentes

**État git au 2026-08-21.** Commité jusqu'à `2d5bb36` (« Refacto Audio.cs ») : les six premières
vraies classes, `OpenForesia.Tests/`, et les références figées `blaze-baseline.txt` /
`bodies-baseline.txt`.
**Non commité** : le travail du 23 août (la moitié dessin de la carte du monde en entier, deux défauts d'origine, deux gestionnaires d'entité relevés au désassembleur), et avant lui : les conversions de `Resources.cs`, `Model3d.cs` et `UiWindow.cs` — l'étape C est
terminée —, le rapatriement `.data` du son, le retrait intérieur des points d'entrée, la correction
des huit défauts d'aliasing (qui **change le comportement**), la transcription de `FUN_00533940`,
`FUN_00533450`, `FUN_004747F0` (celle-ci **absente de `Blaze.c`**, désassemblée sur place) et
`FUN_005350b0`, le verbe `disasm` d'`OpenForesia.Tools`, les vingt désassemblages d'opcodes de
`Legacy/Raw/Blaze/ScriptOps/`, **la réorganisation du dépôt**, et cette mise à jour de documentation.
S'y ajoute, le 2026-08-21 au soir : l'arène des paramètres de vue (étape 1), **tout
`Screens/State03/WorldMapScript.cs`** (étape 2), la correction du défaut de `RvaToFileOffset` dans
`OpenForesia.Tools`, la rétractation des notes qui en découlaient, la correction de l'arité de
`Winsys3d.Ordinal_45` et l'ajout d'`Ordinal_46`.

**Où en est le jeu (2026-08-22, tard).** Le chemin « Début du jeu → constitution du groupe → carte du
monde » va jusqu'au bout, exécute le prologue, l'ossature de la boucle par image, **treize des
quatorze appelées du cas 0** (`0x005176d5`..`0x0051776b`, le pas de simulation du monde) **et les
quatre gestionnaires d'entité que la zone de départ demande réellement**. Les six passes de tables
d'entités traversent maintenant sans lever.

## Archive — l'encadré « REPRENDRE ICI » du 2026-08-23, soldé

> **LE PAS DE SIMULATION DU MONDE S'EXÉCUTE EN ENTIER.** Les quatorze appelées du cas 0 tournent,
> `FUN_0050bf20` avec elles, pour **chaque membre du groupe** et à chaque image. L'exception a
> quitté la simulation : elle est remontée dans `RunWorldMapTick` lui-même.
>
> ---
>
> ## SOLDÉ le 2026-08-23 — `DrawWorldCells` et ses cinq appelées
>
> Le jeu s'arrête sur **`DrawWorldCells`** (`WorldMapTick.cs`, `0x00518e60`..`0x00519703`,
> ~200 l.). **C'est le dernier gros bloc**, et c'est lui qui fera enfin apparaître une image : tant
> qu'il lève, la liste d'affichage n'est jamais soumise et le débit reste à 1 primitive par image —
> ce qui veut dire que **rien de ce qui a été écrit ces deux jours n'a encore été vu à l'écran**.
> C'est le premier contrôle à faire une fois qu'il passe, et un débit qui monte ne suffira pas :
> il faudra regarder la capture.
>
> Ce qu'il fait : il parcourt 7×7 cellules à partir du coin que le cap désigne (`DAT_00564548`,
> relevé), saute celles qu'une table de visibilité comprimée marque (`PTR_DAT_005645d8`, relevé),
> et pour chaque occupant des deux grilles émet un modèle, une particule ou un sprite selon ses
> bits d'état. Suivent les trois listes de morceaux et le ciel.
>
> Appelées à écrire, **toutes dans `Blaze.c`** : `FUN_0051e700` (423 l.), `FUN_0051fa40` (441),
> `FUN_00521000` (365), `FUN_00522140` (315), `FUN_0051f940` (18, le voile de fondu plein écran).
> `FUN_0051c420` est faite. C'est le plus gros morceau restant de la carte du monde, ~1 500 lignes
> de pseudo-C — probablement deux séances.
>
> **Conseil de méthode, vérifié trois fois de suite ces deux jours :** avant d'écrire une de ces
> appelées, chercher si le dépôt n'en tient pas déjà la moitié. `FUN_0051c420` s'est écrite en une
> passe parce que `Engine/Model3d.cs` avait déjà tout son outillage GTE ; la disposition du bloc de
> sprite était dans `Screens/DebugParty.cs` ; `FUN_0051be90` était réputée « sans corps » par un
> commentaire périmé alors qu'elle est dans `Blaze.c`. **Relire ce qui tourne coûte moins cher que
> relever au désassembleur, qui coûte lui-même moins cher que raisonner sur la pseudo-C.**
>
> ## Deux lacunes secondaires, laissées derrière leur condition
>
> * `FUN_00538bc0` — FAIT le 2026-08-23. `DAT_0056d440` relevée : seize entrées de huit octets,
>   une hauteur de tête par classe. Le sous-bloc gardé par `record[0x3c] != 0` est écrit aussi.
> * `FUN_005309b0` — FAIT le 2026-08-23. Les trois tables sont relevées ; `DAT_0056aaa0` et
>   `DAT_0056aae0` ont dû être réunies en une seule allocation, l'index d'icône étant signé et son
>   entrée `-1` lisant la fin de la table voisine.

> ## Une fragilité à surveiller
>
> Une exécution sur cinq a échoué dans `AllocLowZeroed` (« aucun bloc libre de 0x1000 octets sous
> 0x01000000 »). La fenêtre basse fait 16 Mo et chaque bloc y dépense un granule de 64 Ko. Le bloc
> qui a échoué était alloué avant le nouveau, donc ce n'est pas une régression — mais la marge est
> plus mince qu'il n'y paraît, et `DrawWorldCells` peut demander d'autres blocs bas.
>
> ---
>
> ## Ce qui a été fait le 2026-08-22 (contexte)
>
> La **moitié dessin est rallumée** et s'exécute jusqu'à `DrawWorldCells` : la rose des vents, les
> nombres flottants, les quatre panneaux de membre avec leurs jauges, leurs pictogrammes d'état et
> leurs modèles animés. `FUN_0051c420` (rendu de modèle statique, dans `Engine/Model3d.cs`),
> `FUN_0051be90` (nombre décimal, dans `Engine/Primitives2d.cs`), `FUN_0051c250` et l'export de
> données `VSyncDelayed` sont transcrits.
>
> Ce qui la bloquait n'était pas du code mais trois erreurs de disposition, toutes tranchées au
> désassembleur et consignées dans `DataSymbols.md` : le bloc de sprite fait **sept champs sur
> 0x12 octets** (dont deux **échelles**, pas des demi-tailles), les descripteurs d'atlas
> **s'indexent en avant** donc se relèvent par plage et non un par un, et Ghidra place deux champs
> au mauvais décalage. **Sur ce bloc, la pseudo-C ne fait pas foi** — et ça vaut aussi pour les
> signatures : `FUN_0051c420` perd trois paramètres sur cinq et donne **deux noms à chacun**.
>
> La **tête** du bloc d'affichage est transcrite (mode de vue, défilement de textures, lissage de
> caméra, personnage suivi, centroïde du groupe, décalage monde, `SetGeomOffset`) **et tout le
> sous-système de caméra avec elle** (`Screens/State03/WorldMapCamera.cs`, `FUN_004011c0` et douze
> appelées).
>
> Du sous-système caméra il ne reste que le sous-bloc d'**occlusion**
> (`0x00401913`..`0x00401b6d`, plus `FUN_004029e0` et `FUN_00402d60`), qui **n'est pas sur le
> chemin au repos** — sa garde rend la main avant, playtest à l'appui. Son blocage est la donnée et
> non le code : les deux tables `0x00604ba0` / `0x00604d30` ont une étendue que l'indexation
> apparente contredit, à trancher au désassembleur (voir `DataSymbols.md`).
>
> Les descripteurs de sprite et les tables de panneaux **sont relevés et en place**, et le bloc de
> pile est établi (voir plus haut).
>
> **Les curseurs de primitives sont résolus** : `[esp+0x14]` est la base d'ordering table,
> `[esp+0x10]` le curseur, et les six noms que Ghidra leur donne (`unaff_EBX`, `unaff_ESI`,
> `unaff_EDI`, `unaff_EBP`, `local_60`, `piStack_74`) ne désignent que ces deux-là. Ne pas écrire
> une ligne de la moitié dessin sans cette table — un curseur mal chaîné plante **dans winsys3d**,
> sans pile utilisable.
>
> Quinze appelées ont été écrites le 2026-08-22 : `FUN_0050e140`, `FUN_0051a020`, `FUN_0047aae0`,
> `FUN_0051a670`, `FUN_00519f50`, `FUN_00519df0`, `FUN_00519ee0`, `FUN_00534330`, `FUN_005327b0`,
> `FUN_005197d0`, `FUN_00534cd0`, `FUN_00474eb0`, `FUN_00474db0`, `FUN_00474cd0`, plus les imports
> `winsys3d` 439, 788, 792 et 812.
>
> Des lacunes subsistent **hors du chemin au repos** — elles ne lèveront qu'en jeu réel : les
> gestionnaires de posture `0x0051A6B0`/`0x0051A700`/`0x0051A710` et de commande
> `0x0050DD00`/`0x0050DFF0` (tous **absents de `Blaze.c`**), et `FUN_005238e0`, `FUN_00530070`,
> `FUN_005397b0`, `FUN_00519940`, `FUN_00539190`, `FUN_005335e0`, `FUN_0051a5d0`, `FUN_0053a980`,
> `FUN_00530f20`, `FUN_0051a740`, `FUN_0053aa10` (tous dans `Blaze.c`). — `FUN_0051a5d0` est
> écrite le 2026-08-28, c'était le plantage au **relâchement** de Select en forêt.
>
> **Les quatre lectures que seule la machine donne sont résolues** — c'était le plus dur ; elles
> sont écrites en tête de `TickPartyMember` :
>
> | ce que Ghidra rend | ce que c'est | où on le lit |
> |---|---|---|
> | `iStack_4`, jamais affecté | l'entrée d'emplacement, `[esp+68h]` — le prologue **écrase la case du paramètre** avec | `mov [esp+68h],eax` en `0x0050bf52`, relu en `0x0050ce71` |
> | `unaff_EBP` | le bloc par personnage, `MainMem + 0xf0000 + membre * 0x2000` | posé en `0x0050bf61`, sauvé en `[esp+10h]` |
> | `unaff_EBX` | l'état de manette, `0x00604a20 + slotKind * 0x10` | posé en `0x0050bf63`, sauvé en `[esp+14h]` |
> | la cascade `__allmul`/`__allshr` de `0x0050cfb1` | **deux produits matrice-vecteur**, décalage `>> 12` | `mov ecx,0Ch` en `0x0050d02f` et ses cinq jumeaux |
>
> Le bloc matriciel est la transposée du repère de l'emplacement puis le repère du porteur —
> autrement dit « exprimer l'offset dans le repère local, puis dans celui de ce qui me porte ». Il
> est gardé par `s[0x78] != 0` (pas de porteur au repos), donc pas sur le chemin courant.
>
> **Onze appelées lèvent chacune en se nommant, toutes dans `Blaze.c`** — aucune ne demande de
> désassemblage. `FUN_0050e140` (le gestionnaire de commande d'indice 0) est celle que le jeu
> demande en premier. Les autres : `FUN_005238e0`, `FUN_00530070`, `FUN_0051a020`, `FUN_00474eb0`,
> `FUN_005397b0`, `FUN_00519df0`, `FUN_00519f50`, `FUN_005197d0`, `FUN_00519940`, `FUN_00519ee0`,
> `FUN_00534cd0`, `FUN_00539190`, `FUN_005335e0`.
>
> Après tout cela vient le **bloc d'affichage principal** (`0x00517aa7`..`0x00519703`, ~700 lignes),
> celui qui fera enfin apparaître quelque chose à l'écran.

### Les gestionnaires de `PTR_FUN_0056adc8` — SOLDÉS pour la zone de départ

**Quatre des dix-neuf suffisent, pas dix-sept.** L'estimation du matin comptait toute la table en
supposant que la zone posait une entité de chaque type — la lecture du chaînage de `DAT_005fa5e0`
disait le contraire, et le jeu l'a confirmé : les emplacements actifs demandent les sous-types
**8, 9, 10 et 18**, et rien d'autre.

| sous-type | adresse | dans `Blaze.c` ? | rôle |
|---:|---|---|---|
| 8 | `0x004508D0` | **non**, 197 o. | animation de 0x28 images, angle croissant, effet de caméra |
| 9 | `0x004509B0` | oui, 46 l. | sa jumelle en sens inverse, avec un son à l'ouverture |
| 10 | `0x00450AA0` | **non**, 273 o. | montée puis maintien d'un niveau, gardé par le bit de type |
| 18 | `0x00450D30` | **non**, 1 087 o. | **le coffre** : ouverture, clef, remise d'objet, fermeture |

Les quinze autres restent à faire **le jour où une zone les demandera**, pas avant — et la lacune
les nommera comme elle a nommé celles-ci. C'est la même règle que pour les verbes de zone : on ne
transcrit pas une table entière en spéculant sur ce qui sera appelé.

Appelées partagées écrites au passage, toutes réutilisables : `TryPublishCameraEffectNearEntity`
(`FUN_0041be60`), `TryPlaySoundNearEntity` (`FUN_00468080`), `PickWeightedTableEntry`
(`FUN_0053a920`) et l'accesseur `AreaEffectKindBits` (`DAT_00571540`).

**Trois lacunes nommées restent atteignables par le jeu, mais aucune n'est sur le chemin au repos** :
`FUN_00530f20` (la remise d'objet du coffre, la cascade déjà mise de côté pour l'opcode 27),
`FUN_0051a740` et `FUN_0053aa10` (à l'intérieur de `FUN_0050e680`, tous deux dans `Blaze.c`).
>
> Les trois autres trous de la boucle sont nommés sur place dans
> `Screens/State03/WorldMapTick.cs`, avec leur plage d'adresses : l'affichage de mise au point
> (inatteignable, `DAT_005de020` vaut 0 au chargement), le bloc d'affichage principal
> (`0x00517aa7`..`0x00519703`, ~700 lignes — c'est lui qui fera enfin apparaître quelque chose), et
> la branche courte `DAT_005de02c & 4`.

### Ce que la transcription du cas 0 a appris

**Six des quatorze appelées sont la même passe sur six tables différentes.** `FUN_0050e5e0`,
`FUN_0050e680`, `FUN_0050eb20`, `FUN_0047aa70`, `FUN_0047ab70` et `FUN_0047acf0` balaient
respectivement `0x005fa5e0`, `0x005ecaa0`, `0x005f7de0`, `0x005ff400`, `0x005edee0` et
`0x005f2d40`. Toutes portent des enregistrements de **0x9c octets** avec la même disposition de
tête — état à `+0x00`, sous-type à `+0x05`, indice d'emplacement à `+0x08`, position à
`+0x1c`/`+0x20`/`+0x24`, chaînage à `+0x68` — et font les mêmes trois gestes : ignorer l'inactif,
appeler le gestionnaire, raccrocher à la grille. **Ce qui les distingue tient dans le gestionnaire,
pas dans la passe.** Ça fait tomber « ~1 850 lignes » à beaucoup moins.

**Les deux pages de 4 Kio que le prologue efface sont deux grilles spatiales 32×32.** Les
constantes `0x3880` et `0x38a0` de la pseudo-C valent `0x1c4000` et `0x1c5000` une fois multipliées
par `0x20 * 4` — précisément les deux pages remises à zéro en tête d'image. Une entité y est
raccrochée par la cellule que déduit `((position >> 12) + 0x2000) >> 9`, en tête d'une liste
simplement chaînée. `FUN_0050e5e0` est la seule à alimenter la première grille.

**Les sept tables de sauts d'entités sont contiguës, et le jeu s'en sert.** `0x0056ad48` (0x20
entrées) puis six de 0x40 se suivent sans trou jusqu'à `0x0056b3c8`. Les trois passes
`FUN_0047aa70/ab70/acf0` indexent leur table avec un **octet entier** et débordent volontairement :
un indice de 0xc0 dépasse exactement de 0x300 octets, soit trois tables, et retombe sur l'entrée
`index - 0xc0` de la table trois rangs plus loin. C'est pour ça qu'elles testent `index < 0xc0` :
en dessous, la cible est dans une table **statique** du binaire (`PTR_LAB_00551c30` et ses deux
voisines) ; au-dessus, dans la table installée par zone. D'où
`Resources.ResolveEntityHandler(tableVa, index)`, qui résout **par adresse** au lieu d'indexer sept
tableaux séparés — le débordement devient lisible au lieu de lever un `IndexOutOfRangeException`.
Le même débordement était déjà documenté sur `ScriptOpAreaVerb` pour `0056aec8 → 0056afc8` ; il est
maintenant compris comme une règle générale.

**Un neuvième défaut d'aliasing, attrapé avant qu'il ne devienne vivant.**
`s_BackColourR_005e93a0` / `G_93a2` / `B_93a4` étaient trois `int` autonomes pour trois adresses
espacées de **deux** octets, à l'intérieur de GameArena. Le désassemblage de `FUN_00532bf0`
(`0x00532e38`) tranche : `movsx eax,word [5E93A0h]` à la lecture, `mov [5E93A0h],ax` à l'écriture —
ce sont des `short`, et la branche `DAT_005e9166 == 1` les recopie même par **dword**
(`mov eax,[5E9160h] ; mov [5E93A0h],eax`), ce qui n'est pas représentable en champs séparés.
Corrigés en vues, exactement comme `s_ScenePitch_005e9308` la veille. **La correction est neutre**
aujourd'hui : avant elle, aucun site n'écrivait ces trois symboles, ni par champ ni par arène, donc
les deux mémoires valaient zéro des deux côtés. `FUN_00532bf0` allait en être le premier écrivain,
donc le premier à rendre l'aliasing vivant. Le test 5 tombe de **42 à 39** champs signalés.

**Ghidra perd des paramètres par famille, pas par accident.** `FUN_0041be60` et `FUN_00468080` ont
exactement le même défaut : la pseudo-C montre trois paramètres et se sert de `param_1` **à la fois
comme entité** (`param_1 + 0x1c`) **et comme première valeur passée**, ce qui ne peut pas être. Les
deux sites d'appel empilent **six** arguments (`add esp,18h`) et les corps lisent les arguments 2, 3
et 4 ; les arguments 5 et 6 ne sont lus nulle part. Le contrôle est toujours le même : compter sur
le `add esp,N` qui suit le `call`, puis retrouver les décalages `[esp+N]` après les `push` du
prologue.

**Une surcharge homonyme rend l'oracle aveugle.** `compare-bodies.ps1` indexe les corps **par nom
seul**. Un `Blaze.CallEntityHandler` ajouté à côté de `Resources.CallEntityHandler` lui a fait
comparer l'un à l'autre et signaler une « logique changée » qui n'existait pas. Renommé
`DispatchEntityHandler`. Même famille de piège que la liste blanche insensible à la casse déjà
documentée plus bas : **le nom est l'index de l'outillage, pas une décoration.**

Recette de test :

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 50 -Shot -WorkingDir OpenForesia `
    -Then WAIT4,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT1,DOWN,WAIT1,ENTER,WAIT2,START,WAIT4,ENTER,WAIT4,ENTER,WAIT10
```

La recette de `test-protocol.md` (un seul `ENTER` avant `START`) ne suffit plus : la sauvegarde de
test porte quatre personnages, il faut les retenir un par un.

**Le dépôt a été réorganisé le 2026-08-21** — la racine ne porte plus que `CLAUDE.md` et
`OpenForesia.sln`. Ce fichier vit maintenant dans `Documentation/Etat/`. Les décompilations sont
dans `Legacy/Decompiled/`, les binaires dans `Legacy/Binaries/`. Tout est passé par `git mv`, donc
l'historique de chaque fichier suit. Détail dans `REVERSE_STATUS.md`, section du même jour.

**`bodies-baseline.txt` a été refigé** à **513 corps** le 2026-08-21, après les corrections
d'aliasing puis après `FUN_00533940` et les deux messages d'exception dont le chemin a changé.
L'ancienne référence d'avant les corrections reste dans l'historique, au commit `95344c7`.

Les deux fichiers de référence de `OpenForesia.Tools/` sont l'oracle — les perdre le perdrait.
Ils sont désormais dans l'historique, donc récupérables ; ce n'était pas le cas le 2026-08-19.

---

# Priorité 1 — **ÉTAPE C TERMINÉE**, et la moitié intérieure des points d'entrée retirée

Les neuf fichiers d'`Engine/` sont des classes d'instance, et **aucune n'atteint plus une autre par
`Subsystems.cs`** : les 28 qualifications croisées sont devenues des paramètres de constructeur.

```
UiWindow(GameMemory, UiText, Input, Audio)
Primitives2d(GameMemory, UiText, UiWindow)
Resources(GameMemory, Primitives2d, Input, Audio)
Video(GameMemory, Resources)
```

Les cinq autres gardent `(GameMemory mem)`. Les sept `Vid.` restants dans `Engine/Video.cs` sont des
**auto-références** depuis des membres statiques (`[UnmanagedCallersOnly]`, lambdas `static`) : elles
ne relèvent pas du retrait, elles sont la raison d'être du mécanisme.

**Il reste 665 lecteurs**, tous dans `Screens/` et `Platform/`. Ce sont encore des fichiers de
`partial class Blaze` : pas de constructeur, donc rien à injecter. `Subsystems.cs` ne disparaîtra
## Reprendre ici — objectif : **le jeu atteint et exécute le prologue de `FUN_00516fc0`**

Plan arrêté le 2026-08-21, en quatre étapes indépendamment vérifiables. **Cocher au fur et à
mesure**, et re-mesurer avant chaque étape — les estimations se sont révélées fausses **trois**
fois, dont une à l'étape (1) de ce plan même (voir « Cinq avertissements payés comptant »).

- [x] **(1) FAITE le 2026-08-21 — mais pas comme le plan le disait.** Le plan annonçait « étendre
      `EntityState` vers le bas sur `0x005710f0..0x005714ff`, 231 adresses ». **Trois quarts de
      cette région n'appartiennent pas au jeu**, et l'étendre ainsi aurait fait deux dégâts :
      * `0x005710f0..0x00571258` est du **CRT** — `DAT_00571258` est lu par `WideCharToMultiByte`
        (`FUN_0054B190`) et `DAT_005710F0` est sa table de pages de code, parcourue par paires.
        Une arène d'état de jeu l'aurait avalée.
      * ~~`0x00571400..0x00571500` est une **table initialisée** de valeurs `0x00208xxx`~~ —
        **faux, rétracté le même jour** : défaut de l'outil `at`, voir `REVERSE_STATUS.md`.
        `0x00571400` reste la bonne borne, mais parce que c'est la fin du contenu de `.data`
        présent dans le fichier.
      * `0x0057125c..0x0057126c` n'est cité nulle part.

      Le bloc réel est **`0x00571270..0x00571400`**, soit 0x190 octets et **69 symboles**, vérifié
      **intégralement nul au chargement** (384 octets relevés, zéro dword non nul). Créé sous
      `s_ViewParams_00571270` / `ViewParams(va)` — il alimente `s_ViewBlock_006067c0` par addition,
      reçoit la caméra en deux exemplaires parallèles, et porte des drapeaux à `0x0057139c`.
      **Neutre en comportement** : aucune des 123 adresses n'était un champ C#, donc rien à aliaser.
      Les deux tables flanquantes sont notées dans `Documentation/Reference/DataSymbols.md`.
      Vérifié : build 0 erreur, avertissements inchangés, **5 tests de disposition verts**,
      non-régression `-Entry 1` à 382,3.
- [x] **(2) L'interpréteur de script — FAITE le 2026-08-21, la machine est complète.**
      `OpenForesia/src/Blaze/Screens/State03/WorldMapScript.cs`, ~1 000 lignes.

      Écrit : la boucle (`FUN_00513dd0` + `FUN_00513e40`), le répartiteur, **22 opcodes sur 39**
      (1, 2, 3–10, 11, 12, 13, 16, 18, 19, 23–26, 28, 29) et **huit appelées**
      (`FUN_00532740`, `FUN_00538980`, `FUN_005389e0`, `FUN_0051b850`, `FUN_0047a9d0`,
      `FUN_005035c0`, `FUN_00535300`, `FUN_00535400`).

      **Les opcodes 1 et 2 sont la pièce qui rendait la boucle lisible** : saut, et saut
      conditionnel qui *remet le résultat à zéro*. C'est pour ça que la boucle laisse passer
      l'opcode 2 alors qu'un résultat est posé — sans lui, un test de script arrêterait le script
      pour de bon. Aucun des deux n'est dans `Blaze.c`.

      **Les 17 opcodes restants lèvent une lacune nommée**, pas un trou muet. Deux ont un blocage
      établi, écrit dans le message d'exception :

      * **opcode 0** — appelle `(&PTR_FUN_0056aec8)[script[1]]`. Les 256 entrées du binaire pointent
        toutes vers `FUN_00401fa0`, un `ret` nu, **mais ce n'est que le défaut** : `Blaze.c` l. 75143
        remplit la table à l'exécution, 0x40 entrées à la fois, depuis `(&PTR_PTR_00567318)[zone]`.
        Il faut ce chargeur et les jeux de fonctions par zone, pas une relecture du gestionnaire.
      * **opcode 27** — laissé de côté sciemment : tire `FUN_00530f20` (153 l.) et `FUN_005312d0`
        (109 l., criblée d'artefacts de pile), qui tirent à leur tour `FUN_00544350`, `FUN_0051bcc0`,
        `FUN_00530ed0`, `FUN_0053db20`, `FUN_00532710`. C'est la cascade d'affichage des nombres de
        dégâts, hors du chemin vers le prologue.

      Les quinze autres (11 écrit, restent 14, 15, 17, 20, 21, 22, 30–38) n'attendent qu'une reprise
      du désassemblage : les relevés banqués de `Legacy/Raw/Blaze/ScriptOps/` sont **des fenêtres de
      96 octets** et certains corps débordent — reprendre au verbe `disasm` avec une taille plus
      large.

      Trois arités de Ghidra ont été démenties au niveau machine avant transcription (`FUN_0053a920`
      en prend 2 et non 3, `FUN_00530f20` en prend 3 et non 2, `FUN_00532740` en reçoit 2 dont une
      morte), et un défaut d'aliasing a été évité de justesse : `s_PartySize_005e9020` est un
      `static int` **hors arène**, donc lire `0x005e9020` par adresse aurait rendu zéro en silence.
- [x] **(3) `FUN_00401000` et `FUN_00401080` — FAITE le 2026-08-21.** Transcrites dans
      `Screens/State03/WorldMap.cs` sous `SnapshotViewVector` et `PublishFrameView` : elles ne sont
      appelées que par le prologue de (4). Comme annoncé, purs brassages de globales une fois (1)
      faite — le seul calcul est `0x005de06c + 0xc8000`, appliqué deux fois.
- [x] **(4) Le prologue de `FUN_00516fc0` — FAITE le 2026-08-21. L'exception a disparu.**
      Écrit contre `Legacy/Raw/Blaze/FUN_00516FC0-prologue.asm`, pas contre la pseudo-C seule.
      **La clé `piStack_74` est maintenant confirmée au niveau machine** : la valeur posée avant un
      appel est bien le *dernier* argument, celui que Ghidra a perdu (`FUN_0052a1e0` en prend trois,
      `add esp,0Ch`) — sauf quand c'est une adresse de code, auquel cas c'est l'adresse de retour et
      l'appel ne prend rien.

      **La boucle par image reste un trou sonore**, comme prévu : le `do { … } while` de
      `0x00517163`, ~1 100 lignes.

### La frontière est maintenant la chaîne de verbes de l'opcode 0

Le prologue s'exécute et lance l'interpréteur, qui bute sur l'opcode 0 — l'appel de verbe de zone.
**Ce n'était pas le mur annoncé.** La note de l'étape (2) disait « il faut d'abord le chargeur et
les jeux de fonctions par zone » ; en réalité le chargeur existe déjà
(`InstallEntityVTables`, `Engine/Resources.cs`) et la table de l'index 0 y est relevée depuis
longtemps sous `s_VtSource_0056aec8`. Il ne manquait que le répartiteur, écrit sur le patron de
`CallAreaHandler`.

Le travail restant est **mécanique et répétitif** : le jeu nomme un verbe, on le transcrit, on
relance, il en nomme un autre.

- [x] verbe 3 = `FUN_00442F60` (336 o., **absente de `Blaze.c`**, désassemblée — dump dans
      `Legacy/Raw/Blaze/AreaVerbs/`). Décale d'un cran les bits du banc `0x005e9000` vers
      `0x005e9840`, choisit une piste musicale d'après `0x005e96fe` et la lance. Rend 0.
- [x] verbe 10 = `FUN_004441F0` — **souche** `xor eax,eax ; ret`. À ne pas confondre avec
      `FUN_00401FA0`, qui est un `ret` **nu** : celui-là laisse `EAX` intact, donc le résultat de
      condition reçoit l'indice du verbe, tandis que celui-ci rend 0. Les confondre donnait un
      script qui s'arrête là où il devrait continuer.
- [x] opcode 14 = `0x005158f0` — affichage d'un dialogue, absent de `Blaze.c`, transcrit.
- [x] verbe 0 = `FUN_00443100` — dégèle les trois tables d'effets (bit `0x04000000` effacé sur tout
      emplacement dont le mot d'état ne porte pas `0x10000000`). Rend 0. Demandé par le dialogue
      d'un PNJ.
- [x] verbe 14 = `FUN_00442D90` (27 o., **absente de `Blaze.c`**, désassemblée — dump dans
      `Legacy/Raw/Blaze/AreaVerbs/`). Écrit l'entité appelante dans le champ `+0x6c` de l'entité de
      scène d'indice donné, c'est-à-dire le porteur que `RunEntityScript` recevra. Rend 0. C'est ce
      que demande **l'accès à un coffre**.
- [ ] les onze autres verbes réels de l'index 0 (`0x00443260`, `0x004430B0`,
      `0x00442F20`, `0x00442E70`, `0x00442EA0`, `0x00442E20`, `0x00445840`, `0x004431F0`,
      `0x00442E10`, `0x00442DB0`, `0x004457F0`), plus `0x0053DD20/DEA0/DF20` aux
      indices 56–58. **Aucun n'est demandé pour l'instant** — le prologue va au bout sans eux.
      Un balayage a confirmé que seul `0x004441F0` est une souche ; les autres sont de
      vraies fonctions.

      Le désassemblage du 2026-08-23 en a montré cinq d'un coup, contigus après le verbe 14, et ils
      sont **tous courts** : `0x00442DB0` réveille les quatre membres du groupe, `0x00442E10` rend
      un bit de `0x00563c77`, `0x00442E20`, `0x00442E70`, `0x00442EA0` et `0x00442F20` interrogent
      ou écrivent le bloc de 0x2000 octets d'un personnage. Aucun ne dépasse la soixantaine
      d'octets. Les prendre en une passe quand l'un d'eux sera demandé coûtera moins cher qu'un par
      un.

**Le retour d'un verbe devient le résultat de condition**, donc il ne peut pas être deviné : un
verbe non transcrit lève en se nommant plutôt que de rendre une valeur plausible.

### (5) La boucle par image — le prochain chantier, et il est d'un autre ordre

**C'est la frontière depuis le 2026-08-21 au soir.** Le prologue s'exécute en entier, ses deux
lancements de script vont au bout, et l'exécution s'arrête sur la lacune posée en fin de prologue.

Forme : `Blaze.c` l. 63835–64929, **~1 095 lignes**, un seul `do { … } while(true)` avec **quatre
labels** (`0x00518473`, `0x005193ff`, `0x00519455`, `0x00517a64`) — donc à reconstruire en flot, pas
à translittérer. **Une seule sortie**, ligne 64096, gardée par `DAT_005de02c & 2` : elle relance
deux fois `FUN_0040a600`, pose `DAT_005e9898`/`DAT_005e9908` à 1 et rend la main.

L'arbre d'appelées de la seule tête de boucle :

| fonction | état | taille |
|---|---|---|
| `FUN_0040a600` | **no-op**, `ret` nu vérifié au désassembleur | — |
| `FUN_00533000` | déjà transcrite | — |
| `FUN_0053dc30` | à écrire | 34 l. (tire `FUN_0053db20`, 11 l.) |
| `FUN_0050e2b0` | à écrire | 107 l. |
| `FUN_0050e520` | à écrire | 41 l. |

Puis, plus loin dans le corps : `FUN_004011c0` (**358 l.**), `FUN_00532b40` (24 l.),
`FUN_005320f0` (48 l.), et `FUN_0051be90` — **absente de `Blaze.c`**, à désassembler.

**Découpage proposé** : (a) la tête de boucle et ses quatre appelées ; (b) l'affichage de mise au
point (~150 l., gardé par `DAT_005633a8 == 0 && DAT_005de020 != 0`) — vérifier d'abord s'il est
seulement atteint en jeu normal, auquel cas une lacune gardée suffit ; (c) la sortie et ce qui la
précède, pour que le tick puisse au moins rendre la main ; (d) le reste.

**Attention `FUN_0050e520`** : elle lit `DAT_005de034`, le symbole du défaut d'aliasing latent
signalé plus bas. Passer par `s_Unknown_005de034` comme `CharacterCreation.cs`, pas par
`Camera(0x005de034)`, tant que le doublon n'est pas résorbé.

### Ce que la vérification a déjà corrigé dans ce plan

J'espérais que le prologue saute l'interpréteur. **Faux, vérifié** : `DAT_005645f8` vaut **2** au
chargement (lu dans le binaire à `0x005645f8`) et `RunWorldMapState` le remet à 2, donc la branche
`== 2` est prise dès la première image et appelle `FUN_00513dd0` deux fois. L'interpréteur est
incontournable — d'où sa place en (2) et non en option.

### Et ce qui rend l'objectif plus intéressant qu'il n'en a l'air

Juste après le prologue, la boucle appelle `FUN_0040a600` (corps vide, vérifié), `FUN_00533000`
(déjà écrite), puis **`FUN_0053dc30` (34 l.)** et **`FUN_0050e2b0` (107 l.)**. Ce ne sont pas les
monstres de 999 et 591 lignes — ceux-là sont plus loin dans la boucle.

Une fois le prologue franchi, on est donc probablement à **~150 lignes des premières images de la
carte du monde**, pas à 5900. **L'estimation « ~5900 lignes » compte tout l'arbre, pas le chemin
chaud** — et l'expérience du 2026-08-21 montre que le chemin chaud est bien plus court (une seule
fonction, `FUN_00533940`, a débloqué trois étapes d'affilée).

### La clé de lecture, sans laquelle la pseudo-C ment

**`piStack_74 = X;` juste avant un appel est le DERNIER ARGUMENT EMPILÉ, pas une affectation.**
Vérifié au désassembleur : `FUN_00513dd0` reçoit **trois** arguments là où Ghidra en montre deux, et
`piStack_74 = (int *)0x190; Ordinal_379();` est en réalité `Ordinal_379(0x190)`.

Contrôler chaque appel précédé d'une telle affectation — le compte se lit sur le `add esp,N` qui
suit le `call` (`N/4` arguments, cdecl) :

```powershell
dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe <va du site> 60
```

Même chose pour les `unaff_EBX` / `unaff_ESI` / `unaff_EBP` / `unaff_EDI` (60 lignes) : ce sont les
**curseurs de primitives**, reconnaissables au motif `curseur = Draw(otEntry, curseur, …)`.
`local_60` est l'entrée d'ordering table ; `FUN_0052a1e0` (`FrameBegin`) prend **trois** paramètres
là où Ghidra en montre deux — précédent déjà documenté du dépôt.

### Reproduire l'arrêt

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 36 -WorkingDir OpenForesia -Shot `
  -Then WAIT5,ENTER,WAIT2,START,WAIT5,ENTER,WAIT4,ENTER,WAIT16
```

Aucun drapeau d'échafaudage. Si ça ne reproduit rien : chercher `relance detectee`, et vérifier le
**débit** — ~434 primitives/image veut dire qu'on est resté sur l'écran de groupe, l'état 3 atteint
donne ~1. Relancer suffit.

## Cinq avertissements payés comptant

**Vérifier sa propre note avant de planifier dessus.** Ce fichier annonçait que `Resources.cs`
« porte `ToAnsiZ` » et que sa conversion demanderait de décider où vit cette aide. C'était faux :
`ToAnsiZ` est dans `Platform/Startup.cs:310`, déjà `internal static`, et n'a jamais bougé. Une
difficulté annoncée qui n'existait pas a failli dicter l'ordre de travail.

**Le suffixe VA n'est pas la preuve, c'est l'ancre `// Ghidra: DAT_…` qui tranche.** La règle de
tri de 7.9 (« pas de suffixe VA → échafaudage ») a mal classé quatre symboles en deux jours :
`s_BaseSoundBank`, `s_LevelSoundBank`, `s_SoundDirectory` et `s_GameStateId` (`DAT_005de510`) sont
du `.data` pur dont le nom C# ne dit pas l'adresse. Les trois premiers sont maintenant dans
`GameMemory` ; `s_GameStateId` reste chez `GameStates.cs`, exposé `internal`.

Et ce n'est pas qu'une question de rangement : **un `.data` sans suffixe VA est hors de portée de
l'outillage.** Les tests de disposition n'indexent que par le suffixe du nom, donc ils étaient
verts sur un recouvrement bien réel (voir la priorité 1 bis). Le nom est l'index, pas une décoration.

**Un outil de mesure qui range mal fabrique un plan faux, et personne ne le relit.** Le script de
clôture n'avait pas de branche pour `Platform/` : ses symboles tombaient dans « autre Engine ».
`Model3d` a donc porté pendant trois jours l'étiquette « 18 emprunts à `Engine/`, à faire quand
ceux-ci seront convertis », alors que 17 des 18 étaient les registres GTE de `Platform/Winsys3d.cs`
— de l'interop vers `winsys3d.dll`, qui n'allait jamais devenir une classe. Le fichier était en
réalité le **plus fermé** des deux restants. Script corrigé ci-dessous ; la leçon générale est de
se méfier d'un seau fourre-tout dans un `case`, parce qu'il ne se signale jamais.

**Une référence datée ne vaut pas contrôle.** Le 2026-08-21, la conversion d'`UiWindow` a rendu
**5/16 puis 6/16** au taux de course, contre les 22–25 % documentés : alarme franche sur la mesure
même qui avait attrapé la régression du 19. Il n'y avait **aucune** régression — 32 essais plus
tard, le même code rendait **1/32 = 3 %**, contre un contrôle de l'arbre d'avant à **5/32 = 16 %**
mesuré juste à côté.

Le taux **dérive au fil d'une session**, même machine au repos. Le seul contrôle valable est
`git stash` + build + 32 essais, **adjacent dans le temps**. Procédure écrite dans
`test-protocol.md`. Et raisonner sur le mécanisme d'abord : une conversion qui rend des champs
paresseux ne peut que *réduire* le travail placé devant la course — quand la mesure dit l'inverse
de ce que le code impose, suspecter la mesure, mais la refaire plutôt que l'ignorer.

**Trois défauts d'outillage en une journée, et aucun ne produisait d'erreur** : le seau fourre-tout
du script de clôture, l'angle mort des tests de disposition (voir priorité 1 bis), et le filtre
« hors commentaire » qui prenait `*(uint*)…` pour une continuation de bloc. Chacun rendait un
chiffre plausible ou un test vert. **C'est la forme normale d'une panne d'outil dans ce dépôt.**

**Et le troisième a resservi le jour même, sur la décision la plus structurante.** La matrice de
dépendances entre classes, mesurée avant le retrait des points d'entrée, comptait d'abord une arête
`Input → Video` — qui fermait un cycle `Input → Video → Resources → Input`. Un cycle veut dire
« injection par constructeur impossible, garder les points d'entrée ». **Ce `Vid.` était dans un
commentaire.** Avec le filtre corrigé, l'arête disparaît et le graphe est un DAG propre.

Une conclusion architecturale entière tenait sur une ligne de commentaire mal classée. **Un chiffre
qui ferme une porte mérite d'être vérifié à la main avant qu'on renonce.**

## Mesurer les dépendances entre classes — la matrice

Écrite le 2026-08-21 pour le retrait des points d'entrée, et **réutilisable telle quelle à
l'étape D** : elle dit, pour chaque classe, laquelle elle atteint et combien de fois. Ce qui reste
sur la diagonale est une **auto-référence** — un membre statique qui rejoint son instance — et ne
relève pas du retrait.

```bash
cd OpenForesia/src/Blaze
POINTS="Ui Prims Saves Vid Inp Aud Res Mdl UiWin"
printf "%-16s" "depuis \\ vers"; for e in $POINTS; do printf "%-7s" "$e"; done; echo
for f in Engine/*.cs; do
  printf "%-16s" "$(basename $f .cs)"
  for e in $POINTS; do
    n=$(grep -nE "(^|[^A-Za-z0-9_.])$e\." "$f" \
        | grep -vE "^[0-9]+:\s*(//|///)" | grep -vE "^[0-9]+:\s*\*\s" | wc -l)
    [ "$n" = "0" ] && printf "%-7s" "." || printf "%-7s" "$n"
  done
  echo
done
```

**Le filtre à commentaires n'est pas décoratif ici.** Sans lui, la matrice comptait une arête
`Input → Video` qui fermait un cycle et faisait conclure « injection impossible ». Ce `Vid.` était
dans un commentaire. Toujours ouvrir les lignes d'une arête avant d'en tirer une conclusion
d'architecture — une seule ligne mal classée suffit à inverser la décision.

## Mesurer la clôture — la version corrigée du script

Refaire la mesure avant de partir : elle a changé à chaque conversion. **Utiliser cette version-ci**,
celle du 2026-08-21 avec les branches `Platform/` et `Engine/` séparées.

```bash
cd OpenForesia/src/Blaze
for f in Screens/State03/WorldMap.cs; do   # etape D : remplacer par le fichier vise
  gm=0; ic=0; en=0; pf=0; sc=0
  for s in $(grep -oE "\bs_[A-Za-z0-9_]+" $f | sort -u); do
    if grep -qE "^    (private|internal).*\b$s\b" $f; then ic=$((ic+1)); continue; fi
    src=$(grep -rlE "^    (private|internal).*\b$s\b" --include=*.cs . | head -1 | sed 's|^\./||')
    case "$src" in
      Memory/GameMemory.cs) gm=$((gm+1));;
      Platform/*|Interop/*)  pf=$((pf+1));;
      Screens/*)             sc=$((sc+1));;
      Engine/*)              en=$((en+1));;
    esac
  done
  printf "%-22s %5s | GameMem %-3s ici %-3s Engine %-3s Platform %-3s SCREENS %s\n" \
    "$f" "$(wc -l < $f)" "$gm" "$ic" "$en" "$pf" "$sc"
done
```

**Et le compte agrégé ment sur un point** : il inclut les symboles cités **en commentaire**. Lister
le détail avant de conclure (boucle identique, en imprimant `$s` et sa source au lieu de compter).

**Pour distinguer code et commentaire, ne pas filtrer sur `*` en début de ligne.** Le réflexe est
d'écrire `grep -vE "^[0-9]+:\s*(//|///|\*)"` pour retirer les lignes de commentaire, `*` visant les
continuations de bloc `/** … */`. Dans ce dépôt c'est **faux** : une ligne de code qui commence par
un déréférencement — `*(uint*)(atlas + …)` — est alors comptée comme un commentaire. Ça a fait
déclarer `s_FadeCounter_005e9960` « cité en commentaire seulement » alors qu'il est lu par
`DrawUiPanel` ; le compilateur l'a rattrapé, pas la mesure. Filtrer ainsi :

```bash
grep -vE "^[0-9]+:\s*(//|///)" | grep -vE "^[0-9]+:\s*\*\s"
```

`* ` **suivi d'une espace** est une continuation de commentaire ; `*(` est un déréférencement.
Dans du code à pointeurs, la nuance n'est pas cosmétique.

**La taille d'un fichier ne dit rien de son coût de conversion ; sa clôture, si — encore faut-il
la mesurer juste.** Les neuf fichiers d'`Engine/` sont convertis ; le script reste ici parce qu'il
sert tel quel à l'étape D, sur les douze fichiers de `Screens/`.

| classe | point d'entrée | membres | statiques | champs |
|---|---|---:|---:|---:|
| `UiText` | `Ui` | 55 | 0 | — |
| `Primitives2d` | `Prims` | 5 | 0 | — |
| `SaveData` | `Saves` | 33 | **8** (rappels natifs) | — |
| `Video` | `Vid` | 59 | 2 | — |
| `Input` | `Inp` | 61 | 1 | — |
| `Audio` | `Aud` | 76 | 1 (par **portée**) | — |
| `Resources` | `Res` | 67 | 0 | — |
| `Model3d` | `Mdl` | 7 | 0 | **0** |
| `UiWindow` | `UiWin` | 5 | 0 | **0** |

## La recette, éprouvée neuf fois — elle resservira à l'étape D

1. **Mesurer** : membres, `[UnmanagedCallersOnly]`, `delegate*`, et d'où vient chaque `s_*` lu.
2. **Trancher les globales empruntées.** La règle porte sur l'**ancre `// Ghidra:`**, pas sur le
   nom C# — corrigé le 2026-08-21 après quatre mauvais classements :
   * le commentaire ancre un `DAT_…`/`PTR_…` → c'est du `.data`, ça va dans `GameMemory`, **même
     si le nom C# n'a pas de suffixe VA**. Le suffixe est un indice commode, pas la preuve ;
     `s_BaseSoundBank` **est** `DAT_00604920` et `s_GameStateId` **est** `DAT_005de510` ;
   * aucune ancre `DAT_…` → c'est notre échafaudage, ça reste chez son propriétaire, exposé
     `internal` (les tables d'options `s_KeyBindings`, `s_JoystickSlot`, …) ;
   * **un symbole qui en aliase un autre déménage avec lui**, et un bloc déménage entier
     (les trois clés d'ini `s_SoundVolumeIni`/`s_CdVolumeIni`/`s_GammaIni` sont parties ensemble
     parce que `s_GammaIni` **est** `s_Unknown_005d9de0`) ;
   * **exception** : un symbole engagé dans un défaut d'aliasing recensé ne bouge pas — le
     déplacer obligerait à trancher l'alias, donc à changer le comportement. `0x00566c70` est
     dans ce cas.

   **Et si le nom manque son suffixe VA, ne pas le renommer en passant** : l'oracle compare du
   texte, un renommage y est indiscernable d'un changement de logique et ferait sonner ses lignes
   à chaque exécution future, sauf à refiger la référence. Le renommage va avec un changement qui
   refige de toute façon.
3. Déclaration de classe → `internal sealed unsafe class X(GameMemory mem)`, puis retirer `static`
   des membres. Le champ injecté s'appelle **`mem`, sans underscore** : le retour arrière négatif
   de l'oracle exclut `_`, donc `_mem.` serait lu comme un changement de logique.
4. Les membres appelés depuis l'extérieur passent `internal`. Vérifier que chaque membre exposé a un
   usage **réel** — plusieurs candidats n'étaient cités qu'en commentaire.
5. `Mem.` → `mem.`, sauf dans les membres restés statiques.
6. Ajouter `using static OpenForesia.Blaze.Blaze;` pour les aides encore dans `Blaze`.
7. Point d'entrée **paresseux** dans `Subsystems.cs` : `internal static X Xx => s_xx ??= new X(Mem);`
8. Qualifier les sites d'appel. **Une constante se qualifie par le nom de type**
   (`Input.s_GammaScale_0054d000`), pas par l'instance — sinon CS0176.
9. Vérifier : build, oracle, tests, playtest **et le taux de course** (voir plus bas).

## Choisir le nom du point d'entrée — ce n'est pas cosmétique

La liste blanche de `compare-bodies.ps1` est **insensible à la casse**. Y inscrire un nom qui
ressemble à une variable locale courante rend l'oracle aveugle à de vrais changements sur ces
lignes. `Prims` a été choisi plutôt que `Prim` parce que `prim` est un local dans 196 lignes du code
de dessin. Vérifier avant de nommer :

```bash
grep -rn --include=*.cs -iE "\bnomcandidat\b" OpenForesia/src | wc -l
```

Les occurrences **en commentaire** sont sans risque : l'oracle retire les commentaires avant de
normaliser. Noms pris : `Ui`, `Prims`, `Saves`, `Vid`, `Inp`, `Aud`, `Res`, `Mdl`, `UiWin`.

**Et ne jamais donner au point d'entrée le nom de son propre type.** `Audio` était disponible et
lisible ; il a été écarté au profit de `Aud` parce qu'une propriété homonyme de son type déclenche
la règle « Color Color » de C# — `Audio.PlaySoundEffect(...)` se résout alors tantôt comme un accès
statique, tantôt comme un accès d'instance, **selon la classe où la ligne est écrite**. Ça compile,
et c'est exactement le genre d'ambiguïté qu'une transcription vérifiée à la main ne doit pas avoir
à porter. Les abréviations ne sont donc pas qu'un confort de frappe.

## Un membre peut rester statique par PORTÉE, pas seulement par contrainte

La liste des trois motifs ci-dessus était incomplète ; `Audio` en a ajouté un quatrième, et c'est le
plus facile à manquer parce que rien ne le force :

* **Un membre qu'un autre sous-système appelle sans avoir rien à faire de celui-ci.**
  `Audio.InvokeComMethod` est une invocation COM générique de sept lignes. `Platform/Startup.cs`
  s'en sert pour libérer le **périphérique DirectInput** — qui n'a aucun rapport avec le son. Le
  rendre d'instance ferait construire tout le sous-système audio (quatre allocations natives) pour
  libérer une manette, et placerait ce travail devant la course pour la mémoire basse.
  Le garder `internal static` coûte une qualification par nom de type ; l'alternative coûte une
  dépendance fausse. **Se poser la question « qui l'appelle, et est-ce que ça a un sens qu'il
  dépende de ce sous-système ? » avant de retirer un `static` par réflexe.**

## Un déménagement de fonction est gratuit pour l'oracle — s'en servir

`ShutdownStage3` (`FUN_00512ff0`) vivait dans `Platform/Startup.cs` avec les trois autres étapes
d'extinction, et il était **le seul écrivain externe de `s_DirectSound`**. Deux issues :

* l'exposer `internal` et modifiable de loin — c'est-à-dire garder la fuite que la conversion existe
  pour supprimer ;
* le déménager dans `Audio`, où son adresse le place de toute façon (règle de routage 1).

Le deuxième choix a été pris, et il n'a **rien coûté** : l'oracle indexe par **nom seul**, un corps
inchangé qui change de classe ne sonne pas — vérifié, zéro méthode « absente de l'arbre courant ».
**Quand un membre doit être exposé uniquement pour une fonction extérieure, regarder d'abord si
c'est la fonction qui est mal placée.** C'est souvent le cas, et le déménagement est vérifiable.

## Ce qui reste statique, et pourquoi

Une classe mixte n'est pas un compromis raté. Trois motifs rencontrés, tous légitimes :

* **`[UnmanagedCallersOnly]`** exige une méthode statique. Si son corps est une une-ligne qui écrit
  une globale (`SaveData`), rien d'autre à faire ; si son corps appelle d'autres membres
  (`Video.MoviePlayerWindowProc`), il rejoint l'instance par le point d'entrée statique (`Vid.`) ;
  s'il n'utilise que ses paramètres (`Input.CountJoystickCallback`), il reste autonome.
* **Un lambda `static`** garde son mot-clé et passe par le point d'entrée : le `static` d'un lambda
  est une garantie de non-capture, on ne la brade pas pour économiser une qualification.
* **Une décision d'hôte** lue par `src/Host/` à l'initialisation de ses propres types
  (`Video.s_WindowedStartup`) reste statique : en faire un champ d'instance coupleraient cette init
  à la construction complète des sous-systèmes.

## Le piège qui a coûté le plus cher — à ne pas rouvrir

Les sous-systèmes de `Subsystems.cs` sont **paresseux**, `Mem` est **immédiat**. Ce n'est pas un
détail de style :

```
[ModuleInitializer] → TryPrepareDisplayListArena() → lit un champ statique de Blaze
                    → construit tout ce qui est immédiat      ← devant la course
                    → PUIS réclame l'arène basse              ← la course
```

La réclamation ne lit que `Mem`. Tant que les quatre points d'entrée étaient `static readonly`,
chaque conversion ajoutait son travail d'allocation devant une course non déterministe : mesuré
**5 échecs sur 8** contre 1 sur 4 documenté. Rendus paresseux : retour à 25 %.

**Toute nouvelle conversion doit donc être vérifiée sur le taux de course**, pas seulement sur le
build et le débit — ce dernier était vert pendant toute la régression. La boucle de mesure est dans
`test-protocol.md`, section « relance détectée ». **Seize essais minimum, machine au repos** : à ce
taux, huit essais ne distinguent pas 25 % de 40 %, et un build concurrent fausse le résultat
(mesuré 44 % sous charge, 12 % au repos sur le même binaire).

## Ce que l'étape C laisse ouvert

* **L'étape D** — `Screens/` en 12 classes (~13 800 l.), à ne commencer que quand les écrans
  cesseront de bouger. `Screens/State03/WorldMap.cs` est le front de la priorité 2.
* **`GameMemory` reste incomplet** : il modélise ~115 des 398 symboles `.data`. Le reste est
  dispersé dans les fichiers qui les utilisent. Ce n'est pas bloquant, mais chaque conversion en
  rapatrie une poignée.
* **Les défauts d'aliasing.** Cinq sont signalés par `OpenForesia.Tests` (3 tests rouges
  **attendus**) ; un **sixième** a été trouvé le 2026-08-21 et les tests étaient verts dessus —
  voir la priorité 1 bis. Les corriger change le comportement → changement séparé. `0x00566c70`
  doit être traité **avant** la reprise de la carte du monde, et `0x005e8520` va avec le
  renommage qui refige la référence de l'oracle.
* **Huit globales `.data` traversent encore la frontière d'une classe convertie** sans passer par
  `GameMemory`, exposées `internal` chez leur propriétaire :

  | symbole | vit dans | lue par |
  |---|---|---|
  | `s_GameStateId` (`DAT_005de510`) | `GameStates.cs` | `Resources` |
  | `s_MenuRenderBlock_00566c70` | `MainMenu.cs` | `Resources` |
  | `s_Unknown_005e97c8` | `MainMenu.cs` | `Resources` |
  | `s_Unknown_005e9818` | `MainMenu.cs` | `Model3d` |
  | `s_Unknown_005de52c` | `Destination.cs` | `Resources` |
  | `s_Unknown_005e92b0` | `DebugParty.cs` | `Resources` |
  | `s_Unknown_005de154/158/15c` | `MainMenu.cs` | `UiWindow` |
  | `s_FadeCounter_005e9960` | `MainMenu.cs` | `UiWindow` |

  `s_MenuRenderBlock_00566c70` est **bloquée** par le défaut d'aliasing de la même adresse ; les
  cinq autres relèvent de l'étape A ordinaire, à rapatrier en un seul geste vérifié plutôt qu'en
  marge d'une conversion — c'est ce qui a bien marché pour la dette du son (tâche 7.12).
* La réparation des **~870 lignes à encodage double** (`repair-double-encoded-comments`, ouvert,
  non commencé). Commentaires et journaux seulement, rien de visible en jeu.


---

# Priorité 1 bis — **SOLDÉE le 2026-08-21**

Les huit défauts d'aliasing sont corrigés et **les cinq tests de disposition passent**. Le détail
complet — chaque étendue, comment elle a été déduite, et ce que le binaire a démenti — est dans
`REVERSE_STATUS.md`, section du 2026-08-21. Résumé :

| # | défaut | correction |
|---|---|---|
| 1 | `0x00566c70` alloué deux fois | une allocation dans `GameMemory`, 0x14 |
| 2 | `s_MenuScreenBlock_00566c48` 0x40 pour un écart de 0x28 | 0x28 |
| 3 | `s_SoundBankHeader_005e8520` détaché du répertoire de sons | vue dans `s_SoundDirectory_005e8340` |
| 4 | `s_EntityList_005de558` avalé par `ItemMenuArena` | arène 0x460 → **0x410** |
| 5-7 | trois allocations propres dans `EntityArena` | devenues des vues |
| 8 | `s_CardPathDescriptor_00604940` 0x100 pour un écart de 0x20 | 0x20 |

**Deux de ces huit étaient invisibles aux tests**, et c'est le résultat le plus utile :
`s_SoundBankHeader_005e8520` (nom sans suffixe VA) et surtout `DAT_006008d0/d4/d8`, le décalage
monde — écrit par `WorldMap` **à travers l'arène**, lu par `Model3d` dans un **champ séparé**. Les
deux mémoires ne se rencontraient jamais.

La référence de l'oracle est **refigée à 511 corps**. L'ancienne est dans l'historique (`95344c7`).

## Ce que ça laisse ouvert — deux élargissements de l'outillage

Les deux valent mieux que de corriger ce que l'outil voit déjà : ils élargissent ce qu'il **peut**
voir.

1. **Étendre `Aucune_allocation_propre_dans_l_etendue_d_une_arene` aux champs de valeur.** Le test
   ne considère que `SymbolKind.OwnAllocation` ; un `int` à une adresse d'arène passe — c'est
   exactement `DAT_006008d0`. Attention : la première exécution signalera plusieurs cas, dont
   certains légitimes (un champ peut modéliser une adresse d'arène volontairement si aucun site ne
   l'atteint par l'arène). Vérifier chacun.
2. **Auditer les `.data` nommés sans suffixe VA.** Quatre ont été renommés parce qu'on avait établi
   que leur nom mentait ; un balayage systématique des déclarations ancrées `// Ghidra: DAT_…` mais
   nommées sans adresse en trouverait d'autres, et chacune est par construction invisible aux tests.

## Et un chantier de reconstruction, mesuré

**L'arène de carte mémoire.** `0x00604940..0x006049bc` est une région contiguë de globales de carte
— 9 symboles, **81 sites relevés** — qui mérite le traitement « un bloc, chaque symbole une vue »
de l'arène `0x005e9000`.

Tant qu'elle n'existe pas, un nom de fichier de plus de 26 octets déborderait sur le **tas** au lieu
de déborder sur `DAT_00604960` comme dans l'original. Le plus long nom du jeu de 1998 fait 19
octets, donc la marge tient — mais elle est plus mince qu'avant, et c'est le prix de la fidélité.

À faire séparément : ça touche le sous-système de sauvegarde, le plus difficile à vérifier à
l'écran.

---

# Priorité 2 — Finir l'état 3, la carte du monde

## Où on en est exactement

L'**état de jeu 3, la carte du monde**, est le front. Le chemin d'appel :

```
RunGameStateTick (état 3)
└─ RunWorldMapState            FUN_00516020   [FAIT]
   ├─ LoadWorldMapResources    FUN_005160e0   [FAIT]
   └─ boucle par image
      ├─ LoadWorldMapArea      FUN_00516250   [FAIT]
      │  └─ CallAreaHandler(8) → 0x004747F0   [FAIT le 2026-08-21 — absente de Blaze.c,
      │        RunArea0Handler8                  desassemblee sur place ; 724 octets]
      │        ├─ ApplyPartyScriptOp             [FAIT — FUN_00514000 + FUN_00515d70]
      │        └─ ApplyScriptOp                  [FAIT — FUN_00514040, l'ALU du script, 190 opcodes]
      ├─ RunWorldMapFrame      FUN_00516350   [corps FAIT]
      │  ├─ LoadWorldMapSubArea                [FAIT]
      │  │  ├─ InstallEntityVTables            [FAIT — 7 tables + 11 initialiseurs]
      │  │  └─ UploadRawAreaBlocks FUN_005355f0 [FAIT]
      │  └─ PlaceWorldMapParty                 [FAIT]
      │     ├─ TryPlaceEntity   FUN_005330d0   [FAIT]
      │     ├─ FinalizeEntityPlacement2       [FAIT le 2026-08-21 — FUN_00533940, 375 l.]
      │     │  └─ GroundHeightOnFace          [FAIT le 2026-08-21 — FUN_00533450, 52 l.]
      │     └─ SpawnSubAreaEntities FUN_005350b0 [FAIT le 2026-08-21 — le peuplement de
      │           sous-zone ; l'ancien nom ApplyAreaPalette etait faux]
      ├─ ReInitializeAllTextures / Ordinal_674 [FAIT]
      └─ RunWorldMapTick       FUN_00516fc0   [prologue + OSSATURE de la boucle FAITS 2026-08-22,
         │                                     dans Screens/State03/WorldMapTick.cs]
         ├─ TickDelayedSounds  FUN_0053dc30   [FAIT — + FUN_0053db20 / FUN_0053da20, Engine/Audio.cs]
         ├─ PrepareLeaderAndPartyFlags        [FAIT — FUN_0050e2b0]
         ├─ PassLeaderToNextEligible          [FAIT — FUN_0050e520]
         ├─ ResetPerCharacterFrameState       [FAIT — FUN_0053dcb0, absente de Blaze.c,
         │                                     desassemblee ; c'est PTR_FUN_0056afe8]
         └─ cas 0 du repartiteur DAT_005e9ae4  [13 des 14 appelees FAITES le 2026-08-22]
            ├─ SmoothSceneOrientationAndBackColour  [FAIT — FUN_00532bf0]
            ├─ RunLeaderScriptListEarly             [FAIT — FUN_00513ef0 + FUN_00513eb0]
            ├─ TickEntityTable_005fa5e0             [FAIT — FUN_0050e5e0]
            │  └─ gestionnaires de PTR_FUN_0056adc8 [les QUATRE que la zone demande sont FAITS]
            │     ├─ 0x004508D0  [FAIT — absente de Blaze.c, desassemblee]
            │     ├─ 0x004509B0  [FAIT — + FUN_00468080, arite corrigee au desassembleur]
            │     ├─ 0x00450AA0  [FAIT — absente de Blaze.c, desassemblee]
            │     └─ 0x00450D30  [FAIT — le coffre, 1087 o. absents de Blaze.c ; lacune interne
            │                     FUN_00530f20, hors du chemin au repos]
            ├─ TickEntityTable_005ecaa0             [FAIT — FUN_0050e680 ; deux lacunes internes,
            │                                        FUN_0051a740 et FUN_0053aa10, non atteintes]
            ├─ TickEntityTable_005f7de0             [FAIT — FUN_0050eb20]
            ├─ TickPartyMember                      [FAIT — FUN_0050bf20, 1002 l., en entier]
            │  ├─ TickPlayableMember                [FAIT]
            │  ├─ TickFollowingMember + ProbeGroundAhead  [FAIT]
            │  └─ TickPartyMemberTail               [FAIT — + ComputeCarrierOffset, FUN_00534330,
            │                                        FUN_005197d0, FUN_00534cd0, FUN_00474eb0]
            ├─ TickEntityTable_005ff400 / 005edee0 / 005f2d40
            │                                       [FAIT — FUN_0047aa70 / ab70 / acf0]
            ├─ TrySpawnRandomEncounter              [FAIT — FUN_0050ed70]
            ├─ TickEffectSlots_005e9b60             [FAIT — FUN_0050f0d0]
            ├─ RunLeaderScriptListLate              [FAIT — FUN_00513f20]
            ├─ ResetPendingChunkRecords             [FAIT — FUN_00534f90]
            └─ TickCutsceneCamera                   [FAIT — FUN_005036e0, boucle de verbes comprise ;
                                                     les 32 verbes de PTR_LAB_0055eb68 sont transcrits]

         [le cas 0 s'execute ENTIEREMENT depuis le 2026-08-22]
         └─ bloc d'affichage principal 0x00517aa7   ←←← ON S'ARRÊTE ICI (~700 l.)
```

## Comment reproduire l'arrêt en trente secondes

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;$env:PATH"     # le SDK n'est pas dans le PATH du shell
OpenForesia.Tools\playtest.ps1 -Entry 0 -Seconds 24 -WorkingDir OpenForesia `
  -Then WAIT4,ENTER,WAIT1,START,WAIT4,ENTER,WAIT3,ENTER,WAIT8
```

Séquence : « Début du jeu » → un personnage retenu (`ENTER`) → `START` → « Veux-tu jouer avec ce
groupe? » → `ENTER` → « Oui » → `ENTER`. L'exception nommée tombe avec la pile complète.

**Prérequis** : au moins une sauvegarde dans `OpenForesia\save\`. Il y en a quatre.

## `FUN_00533940` — **FAITE le 2026-08-21**, et ce qu'elle a appris

Transcrite sous le nom `FinalizeEntityPlacement2`, avec sa dépendance `FUN_00533450`
(`GroundHeightOnFace`). Le détail est dans `REVERSE_STATUS.md`. Trois choses valent d'être retenues
ici parce qu'elles resserviront :

**Le graphe n'était pas irréductible.** Ce fichier annonçait « cinq labels sautés depuis plusieurs
endroits : il faut reconstruire le flot ». C'est vrai, mais la reconstruction est simple une fois
qu'on voit ce que les labels sont : **une boucle sur les arêtes de la face**.

```
triangle : (i0,i2)  (i2,i1)  (i1,i0)
quad     : (i0,i2)  (i2,i1)  (i1,i3)  (i3,i0)
```

Chaque arête est testée par un `NormalClip` ; un signe négatif fait franchir **cette** arête vers la
face voisine, puis on reprend à l'arête suivante. `LAB_00533af7`, `LAB_00533b96` et `LAB_00533e9f`
sont exactement « reprendre à l'arête 1, 2, ou 2-quad ». La comparaison de clés d'arête qui suit la
marche est une **table de correspondance déguisée** : elle se résume à
`arête suivante = arête franchie + 1`.

**Une expression inlinée peut être une fonction déjà reconstruite.** Les six termes que Ghidra
répète six fois sont le `NormalClip` du SDK, déjà écrit dans `Platform/Winsys3d.cs` sous
`FUN_0051c280`. Recouper terme à terme avant d'appeler : Ghidra les émet dans un autre ordre.

**Un remplacement « plus fidèle » peut changer le sens.** Ghidra écrit `(… / N.y - y0) * 0x1000`.
Écrit `… - y0 << 0xc`, c'est juste. Remplacer `<< 0xc` par `* 0x1000` pour coller au texte de Ghidra
casse tout : `*` lie plus **fort** que `-`, `<<` plus lâche. **Vérifier la classe de précédence
avant de substituer un opérateur à un autre.**

### Ce que ce fichier annonçait et qui s'est révélé faux

> `_DAT_005de730` / `_DAT_005de734` … **Pas encore modélisés, et ils tombent hors de tout ce qui
> existe** … Il faudra une arène de plus.

**Non.** C'est un bloc de 8 octets, et la taille se déduit de trois côtés qui concordent : deux
écritures de dword mais des relectures en shorts (donc quatre shorts), un indice borné à 0..3 des
deux façons dont il est calculé, et le symbole suivant `s_Unknown_005de738` à exactement 8 octets.
Ajouté à `GameMemory` sous `s_FaceWalkHistory_005de730`.

**C'est la troisième note de ce fichier démentie par la vérification en trois jours.** Les deux
autres sont dans « Cinq avertissements payés comptant ».

### Le modèle à suivre

`TryPlaceEntity` (`FUN_005330d0`), transcrite aujourd'hui, fait **exactement la même géométrie** :
même disposition de sommet, même produit vectoriel dans 799/800/801, même saturation dans 813. La
relire d'abord — elle donne les bons types et les bons décalages, et elle est **vérifiée par le
comportement** (sa boucle de tirage aléatoire sort, donc elle trouve bien des faces praticables sur
les données réelles).

### Les ordinaux GTE

799 et 800 ont été **identifiés aujourd'hui** (les deux vecteurs d'entrée du produit vectoriel) et
sont résolus dans `EnsureGteRegisters`. 801 = `MAC1..3`, 808 = `MAC0` (où le NCLIP dépose son
signe), 813 = `IR1..3`, 805/806/807 = les trois sommets projetés. Voir
`Documentation/DLL/winsys3d.md`, section « The emulated GTE register file ».

---

## L'échafaudage `OPENFORESIA_SKIP_AREA_HANDLER` — **RETIRÉ le 2026-08-21**

Il n'existe plus, et rien ne le remplace : `FUN_004747F0` est transcrite. L'état 3 traverse la
transition de sous-zone pour de vrai.

**Ce que ce blocage a appris, et qui vaut au-delà de lui.** Ce fichier disait : « ça demande une
re-passe Ghidra ciblée — décoder 723 octets à la main serait déraisonnable ». Deux erreurs dans une
phrase :

1. **« Re-passe Ghidra » désignait une action hors du dépôt**, que personne ne pouvait faire depuis
   une session de travail. Le verrou était donc permanent en pratique, pas temporaire. Or le binaire
   est dans le dépôt : il ne manquait qu'un décodeur. `Iced` est ajouté à `OpenForesia.Tools`, et le
   verbe `disasm` existe désormais :

   ```powershell
   dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x004747f0 724
   ```

   **Toute future « re-passe Ghidra requise » est un travail interne.** Les corps que Ghidra n'a pas
   décompilés — procédures de fenêtre, corps seulement adressés, fentes de vtable — cessent d'être
   des impasses.

2. **723 octets ne mesurent pas une difficulté.** La fonction s'est révélée être une cascade
   parfaitement régulière : huit blocs identiques au bit près, et les « neuf sorties convergentes »
   sont neuf `return`. Transcription : 60 lignes. Ce qui mesure la difficulté, c'est le **nombre de
   formes distinctes** — ici deux.

Relevé conservé dans `Legacy/Raw/Blaze/FUN_004747F0.asm`.

---


## Le reste, par ordre de valeur

1. **`FUN_005350b0`** (95 lignes) — la palette de zone, appelée en fin de `PlaceWorldMapParty`.
   Petite, sur le chemin chaud.
2. **`FUN_00516fc0`** (1226 lignes) — **en cours** : prologue, ossature de la boucle et **treize des
   quatorze appelées du cas 0** faits le 2026-08-22, voir « Où en est le jeu » en tête de ce fichier
   pour ce qui reste. C'est elle qui fera enfin apparaître quelque chose à l'écran, mais pas avant
   le bloc d'affichage principal (`0x00517aa7`), qui vient après tout le cas 0.

2bis. **Les 39 champs de valeur simple qui doublent une arène** (42 avant la correction de la
   couleur de fond, le 2026-08-22), maintenant listés nominativement
   par le test 5 de `OpenForesia.Tests` (`dotnet test OpenForesia.Tests`). Le test est rouge
   exprès. Les corriger **change le comportement** : c'est un changement à part entière, et il vaut
   d'être fait avant le bloc d'affichage, qui touche largement `GameArena`.
3. **`FUN_00549380`** (424 lignes) — le vrai moteur `sprintf`. Aujourd'hui le seul format utilisé
   (`"q:\snd\Q%02d_%02d.snd"`) est posé en dur dans `LoadWorldMapSubArea`. À généraliser le jour où
   un second format apparaît, **pas avant**.
4. Les phases 9 et 10 de `FUN_00524880` et toute la boucle de sauvegarde du menu objets sont
   transcrites mais **injoignables dans le jeu de 1998** (voir `legacy-bug.md`, LB-004) : rien à y
   faire, ne pas les « réparer ».

---

## Rappels de méthode qui ont servi cette semaine

- **Mesurer avant d'écrire.** Un balayage transitif du sous-graphe (index des fonctions construit en
  une passe `awk` sur `Blaze.c`, croisé avec les `FUN_` déjà présents dans `Blaze.cs`) donne en
  quelques secondes « N fonctions manquantes, M lignes ». C'est ce qui a évité de partir sur les
  17 400 lignes de l'état 3 d'un bloc.
- **Quand la pseudo-C est ambiguë, lire le désassemblage** — pas renoncer. Trois « re-passe Ghidra
  requise » de cette semaine faisaient vingt octets. La distinction utile est la **taille**.
- **Une constante 64 bits dans du code 32 bits est un indice, pas un mur** :
  `pbVar14[-0xffffffff00000008]` est l'indice `-8`.
- **Un `DAT_` isolé au milieu d'une région modélisée est presque toujours un champ de cette
  région.** Le défaut d'aliasing s'est reproduit une vingtaine de fois ; le dernier en date
  (`DAT_005e9080`, « Au prix de: 0 ») ne s'est vu qu'à l'écran, le compte de primitives étant
  identique.
- **Vérifier contre l'écran, pas contre un nombre.** Voir la mémoire du projet à ce sujet.
- Le `save\` suit le **répertoire de travail**, le `.cfg` suit le chemin de l'**exe**. Un `save\`
  vide fait rebondir « Invertaire » et « Enchère » vers l'écran racine — c'est le comportement de
  l'original, pas un lien mort.
---

# EN ATTENTE — une corruption de tas intermittente, antérieure au 2026-08-26

**Mis de côté le 2026-08-26, décision du Responsable** — aucun chantier ouvert dessus pour le
moment. La section reste ici pour ne pas perdre ce qui a été daté ; on n'y revient pas tant que le
Responsable ne le demande pas.

**Vingt exécutions du Responsable le 2026-08-26, aucune n'a planté.** À un plantage sur six, vingt
essais propres d'affilée ont moins d'une chance sur trente de se produire : le taux mesuré la veille
ne tient plus. **L'hypothèse retenue** est que les six occurrences des 25 et 26/08 venaient de
**Smart App Control**, que le Responsable a désactivé le 25/08 parce qu'il refusait de charger une
des DLL du jeu. On garde ça en tête sans y travailler ; si le symptôme revient, on prendra des
dispositions à ce moment-là — et la première sera le tas de page.

**Environ une exécution sur six** meurt en silence sur `0xC0000374` (`STATUS_HEAP_CORRUPTION`,
détecté dans `ntdll`). Le journal des événements Windows en compte **cinq le 25/08 et une le
26/08** : ce n'est ni le menu rapide, ni le script d'objet — les deux ont été innocentés par une
trace fichier qui n'a jamais été écrite.

Jusqu'au 26/08 le verdict de `playtest.ps1` annonçait « aucun plantage » sur ces exécutions-là. Il
dit maintenant `SORTIE SILENCIEUSE`, ce qui est justement ce qui a rendu le défaut visible.

**Une exécution verte ne prouve rien** sur ce défaut : compter sur cinq à dix lancements.

## Ce que le vidage du 26/08 10:32 a déjà dit (lu le 2026-08-26, sans rouvrir le chantier)

Windows garde un **vidage complet** de chaque mort dans `%LOCALAPPDATA%\CrashDumps` ; onze y
dormaient. `OpenForesia.Tools\dump-info.ps1` les lit sans debogueur. Sur celui du `0xC0000374` :

- l'exécution avait **70 secondes** (10:31:01 → 10:32:11), tous les sous-systèmes chargés
  (`winsys3d`, `ddraw`, `dsound`, `dinput`, `d3d11`), **42 threads** ;
- le thread qui lève est un **thread COM/RPC** (`ole32`/`combase`/`rpcrt4`) : c'est celui qui
  allouait, donc le détecteur, pas le coupable ;
- le thread principal était dans la **boucle par image** de `winsys3d` — l'ordinal 701
  (`FUN_00a061b0`, le parcours de la liste d'affichage) et l'ordinal 132 (`FUN_00a053e0`, l'attente
  de synchronisation verticale). Donc **ni la constitution du groupe, ni un chargement**.

Ça ne nomme toujours pas l'écrivain — par construction, `0xC0000374` se détecte loin de la faute.
Seul le tas de page (`gflags`) le nommerait.

Pour l'attraper : `gflags /p /enable OpenForesia.exe /full` fait planter **à l'écriture** au lieu du
prochain `alloc`. Rien n'a encore été tenté. Recette et commandes de datation dans
`Documentation/Tests/test-protocol.md`.

