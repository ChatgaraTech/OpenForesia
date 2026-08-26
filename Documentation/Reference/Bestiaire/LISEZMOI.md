# La cartographie des ennemis

Relevé le **2026-08-26** par `OpenForesia.Tools bestiaire`, directement sur le CD d'origine. Rien
ici n'est saisi à la main : les cinq fichiers voisins sont régénérés d'une commande, et le jour où
une lecture d'offset change, ils changent avec elle.

```powershell
dotnet run --project OpenForesia.Tools -- bestiaire "E:\" --sortie Documentation\Reference\Bestiaire
```

`--zone N` restreint à une zone, `--regions` ajoute la carte des soixante pointeurs d'en-tête de
chaque répertoire et le relevé des fiches d'objets.

## D'où viennent ces chiffres

L'outil **rejoue le chargement du jeu**, il ne devine pas un format. `SYSTEM.INI` est le répertoire
racine (`LoadFirstLevelResources`), `BLAZE.ALL` l'archive adressée au secteur (`LoadResourceEntry` :
`entrée[0] << 11` pour l'offset, `entrée[1]` pour la taille), et chaque blob passe par la remise en
place de ses pointeurs (`FixupResourceHeader`). La chaîne a quatre niveaux, tous en `+0x48` :

```
SYSTEM.INI  --+0x48-->  état 3 (la carte du monde)
            --+0x48-->  zone (DAT_005de528)
            --+0x48-->  sous-zone (DAT_005de52c)
                        = le répertoire que ResolveEntityBlocks interroge
```

Les blobs sont chargés à l'adresse virtuelle **0**, si bien qu'un pointeur relocalisé devient un
simple offset dans le tableau. La substitution est exacte et non approchée : les pointeurs d'un blob
ne sortent jamais de leur propre blob.

**Les tables sont bornées par les pointeurs d'en-tête**, pas par un enregistrement nul. Les régions
d'un blob se suivent sans trou, donc la fin d'une table est le plus petit des soixante pointeurs qui
lui soit supérieur. Compter jusqu'à un zéro se trompe dès que la table voisine commence par un zéro —
c'est ce qui, au premier essai, a fait lire « Goblin » comme une sixième espèce.

## Les quatre tables

| champ | contenu | pas |
|---|---|---:|
| `+0x34` | les **formations** — ce que l'opcode 13 fait naître, et où | 0x20 |
| `+0x4c` | les **espèces** — l'aiguillage : quelle IA, quelle fiche, quelle classe de butin | 8 |
| `+0x98` | les **fiches** — le nom et les valeurs | 0x60 |
| `+0xb4` | les **tables de butin** — huit rangs par classe | 8 |

### La fiche, champ par champ

Ce sont les usages de l'**instance** qui nomment les champs, jamais une supposition sur le modèle :
`ResetEntityStats` recopie le modèle dans le bloc de statistiques, et c'est là que le reste du jeu
lit.

| modèle | instance | rôle | ce qui l'établit |
|---|---|---|---|
| `+0x00..0x0f` | `stats+0x00` | **le nom**, seize octets, Latin-1 | le code de texte `0x1d` insère le nom d'un locuteur à l'offset 0 de son bloc ; le premier octet du premier modèle lu est le `G` de « Goblin » |
| `+0x10` | `stats+0x44` | **le rang** | affiché par la surimpression de mise au point à côté des deux jauges (`WorldMapTick.cs:2084`), et comparé au niveau du personnage à la mise à mort |
| `+0x14` | `stats+0x48` max, `+0x4c` courant | **les points de vie** | la pétrification en draine cinq pour cent (`:3221`) |
| `+0x16` | `stats+0x4a` max, `+0x4e` courant | **les points de magie** | la régénération lente les remonte d'un seizième tous les cent tours (`:2931`) |
| `+0x22` | — | la valeur comparée à `rand() % 1000` pour le butin | `FUN_0051a740` |
| `+0x30..0x3f` | `stats+0x30` | **huit caractéristiques**, en `ushort` | recopiées telles quelles |
| `+0x28..0x2f` | `stats+0x18` et `+0x110` | huit octets, écrits deux fois | rôle non établi — `TODO(REVERSE)` de longue date |

**Correction : les deux jauges étaient inversées dans `REVERSE_STATUS.md`.** La note du 2026-08-26
donnait `+0x16` pour les points de vie. C'est `+0x14`. Les trois usages qu'elle citait portent tous
sur la **même** paire `(+0x4a, +0x4e)` et ne pouvaient donc pas départager les deux ; la paire
symétrique `(+0x48, +0x4c)` est celle que la pétrification draine, et une jauge qu'on pétrifie est
la vie. Les données concordent : le Goblin de rang 2 a 14 PV et 4 PM, la Coccinelle 8 et 1 — là où
la lecture inverse lui donnait 1 point de vie.

### Le butin

`FUN_0053a920(classe, plage)` tire `rand() % plage` et le compare à sept seuils — `0x18`, `0x29`,
`0x38`, `0x45`, `0x50`, `0x59`, `0x5f` — pour obtenir un rang de 0 à 7, puis rend l'octet
`table[classe * 8 + rang]`. Les probabilités s'en déduisent exactement :

| rang | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---|---:|---:|---:|---:|---:|---:|---:|---:|
| plage 100 | 25 % | 17 % | 15 % | 13 % | 11 % | 9 % | 6 % | 4 % |

À la mise à mort la plage n'est pas 100 : elle vaut `0x38 + écart`, où l'écart est le niveau du
personnage moins le rang du monstre, borné à `0x2c` — donc **56 à 100**. Une plage de 56 ne peut
jamais dépasser le rang 4, ce qui rend les trois derniers rangs inatteignables tant que le groupe
n'a pas pris de l'avance sur le monstre.

La **classe** vient de l'espèce (`espèce+5`), pas de la fiche : deux espèces partageant un modèle
peuvent donc ne pas lâcher les mêmes choses.

**L'octet rendu est un identifiant d'objet**, et c'est le code qui le dit : les trois autres
appelants de `FUN_0053a920` — dont le coffre — passent tous son résultat directement à
`FUN_00530f20`, la remise d'objet, qui l'utilise comme `catalogue + indice * 0x140`.

Une première lecture s'était trompée en suivant `FUN_0051a740`, qui écrit la valeur en `+7` de
l'entité de butin, et en concluant que `+7` était un index de liste parce que c'est ce qu'une autre
fonction y range. C'était prendre le **rangement temporaire** pour le sens de la valeur. Le sens se
lit chez les appelants qui la consomment, pas chez celui qui la met de côté.

### Les coffres

**Un coffre tire son contenu dans la même table `+0xb4` qu'un monstre.** `ChestGrantContents`
(`Screens/State03/WorldMapTick.cs`) appelle `PickWeightedTableEntry(+0x16, 100)` — c'est
`FUN_0053a920`, exactement l'appel de la mise à mort, avec les mêmes huit bandes de probabilité.
Ce qui change est la **rangée** : un monstre prend celle de son espèce (`espèce+5`), un coffre
prend celle que porte son enregistrement de placement. C'est pour cela que des objets n'apparaissent
que dans les coffres — **112 des 274** objets qu'ils contiennent ne sont lâchés par aucun monstre :
leurs rangées ne sont référencées par aucune espèce.

Les coffres sont des **entités de la table `+0x30`** — celle que `SpawnSubAreaEntities`
(`FUN_005350b0`) parcourt : une liste de listes d'enregistrements de `0x20` octets, terminée par
`kind == -1`, chacun devenant une entité de `0x9c` octets. La correspondance vient de son corps :

| dans l'enregistrement | dans l'entité | ce que c'est |
|---|---|---|
| `+0x04` / `+0x06` / `+0x08` | `slot[7]`, `slot[8]`, `slot[9]` | X, hauteur, Z |
| `+0x0a` | `+0x30` | **le numéro de coffre** |
| `+0x0e` | `+0x34` | **le mot de contenu**, octet bas et octet haut |
| `+0x18` | `+0x14` | l'index du drapeau, ou `-1` |
| `+0x1a` | `+0x16` | **la rangée de butin**, ou l'index du libellé |
| `+0x1d` | octet 5 | le **sous-type**, celui qui choisit le gestionnaire |

L'état 0 du gestionnaire vide `+0x30` et `+0x34` dans `+0x9a`, `+0x44` et `+0x48`, puis les remet à
zéro : **la fiche du coffre voyage dans les champs de hauteur et de rotation de l'entité**, et n'est
lue qu'à la première image. Le numéro de coffre indexe le champ de bits « déjà ouvert »
(`DAT_005e96e0`) ; un numéro **négatif** ne change que le sens d'ouverture, et la valeur absolue
reste l'index.

Trois rôles, dans l'ordre où le gestionnaire les teste : `+0x48` non nul, le coffre fait naître une
entité d'effet et ne donne rien ; sinon `+0x14 == -1`, il **remet un objet** ; sinon il affiche le
libellé que `+0x16` désigne.

**Le piège, et il est complet : un numéro de sous-type ne veut rien dire tout seul.** La passe
`FUN_0050e5e0` appelle `(&PTR_…_0056adc8)[entité[5]]`, et cette table de `0x40` entrées est
**rechargée à chaque sous-zone** — la branche `DAT_00600904` de `FUN_0052a660` choisit sept blocs
indexés par `zone * 0x10 + sous-zone`. Le premier relevé filtrait sur « sous-type 13, 17 ou 18 »,
les numéros observés dans la sous-zone où les gestionnaires avaient été lus, et sortait des données
absurdes : identifiants tous nuls, contenus tous nuls, 36 coffres. Le coffre de la zone 1 est en
réalité le **sous-type 22**. L'outil résout donc la table de la sous-zone dans `Blaze.exe` — c'est
le même relevé que `dump-entity-vtables.sh` — et retient les entrées qui valent `0x00473ED0`,
`0x004502E0` ou `0x00450D30`, les trois gestionnaires qui remettent un objet. Le compte passe de 36
à **161 coffres**.

Ces trois gestionnaires portent le même automate à cinq différences près ; `EntityHandler_00450D30`
et `FUN_004502E0` étaient absents de `Blaze.c` et ont été transcrits depuis le désassemblage.

### Le catalogue d'objets

Il est **par sous-zone**, et `UploadRawAreaBlocks` (`FUN_005355f0`) le construit à partir de la
liste `areaDir + 0xb0` : chaque entrée est un blob d'archive contenant un compte suivi d'offsets
relatifs, et chaque morceau ainsi désigné commence par les **0x140 octets de la fiche d'objet** (la
palette et l'icône 24×24 suivent en `+0x140` et `+0x160`, et partent en VRAM). L'indice est
cumulatif sur toute la liste.

**Le nom affiché est le nom long de `+0x41`**, terminé par une barre oblique et non par un NUL. Le
nom court de `+0x00` n'est qu'un repli, tronqué à seize octets — c'est lui qui donne les
« Casstetechain » là où le nom long dit « Casse-tête à chaîne ».

**Le champ ne porte pas deux chaînes mais une seule, et la barre oblique est facultative.** Quand
elle est là, le champ est `« nom/description »` — `Armure du dragon/Armure cuirassée d'écailles de
dragon.` — et c'est exactement ce que `FUN_0051bd50`, « le nom d'objet coupé à la barre oblique »,
jette. **Quand elle manque, le champ entier est la description**, et le nom retombe sur le nom
court : `« Potion med »` porte `« Petite Potion »`, et le jeu affiche bien l'un en nom et l'autre en
description. Avec cette règle, **les 1 618 fiches** en ont une ; à ne lire que ce qui suit une barre
oblique, 242 en manquaient.

### Les valeurs d'un objet

**C'est l'écran d'objet du jeu qui les nomme, pas une déduction.** `DrawItemStatRow`
(`Screens/State06/Inventory.cs`) dessine, dans cet ordre : les **huit octets signés de `+0x18`** sur
deux rangées de quatre, puis **deux valeurs longues** prises à `+0x30` et `+0x32` — l'**ATT** et la
**DEF**. `DrawItemElementRow` allume ensuite huit témoins depuis les bits de `+0x12`.

| offset | forme | ce que c'est |
|---|---|---|
| `+0x30` | `short` | **ATT** — non nul sur les armes |
| `+0x32` | `short` | **DEF** — non nul sur les armures |
| `+0x18` | 8 octets **signés** | **les huit éléments**, affichés dans l'ordre `s_StatSlotOrder_00566ad0 = [2,1,0,3,4,5,6,7]` |
| `+0x12` | masque 8 bits | **les classes qui peuvent l'équiper** |
| `+0x20` | 16 `short` | ce qu'`ApplyEquipmentStatBonuses` ajoute aux statistiques du personnage — les indices 0 à 7 vont aux caractéristiques, les indices **8, 9, 12 et 13 sont l'ATT, la DEF, la MATT et la MDEF**, et les indices 10, 11, 14 et 15 ne s'affichent que derrière le verrou de mise au point — voir plus bas |
| `+0x8a` | `short` | ajouté à la valeur bornée à `[0, 99]` de `+0x156` |

Les huit octets de `+0x18` ne s'appliquent qu'aux **deux derniers emplacements** d'équipement.

**`+0x12` n'est pas un masque d'éléments**, malgré le nom que `DrawItemElementRow` porte encore dans
la reconstruction. `GrantChestContents_00530F20` le documentait déjà correctement — « celles
auxquelles il est destiné » — et les données tranchent : le « Simple bâton » n'allume qu'un bit, le
« Manteau de cuir » les huit, l'« Armure du dragon » deux. Ce sont les huit classes jouables. Le nom
`…ElementRow` est un nom posé sur une hypothèse, et il a empêché de relire — exactement le piège que
`CLAUDE.md` signale.

**Quel bit désigne quelle classe** — la page nomme les huit, et les noms sortent du binaire plutôt
que d'une déduction. `&PTR_s_Fighter_0056c7c0` (`Screens/DebugParty.cs`) est un tableau de huit
pointeurs dans le bloc `0x0056c894`, indexé par l'identifiant de classe, c'est-à-dire les trois
bits bas de l'octet d'identité `+0x150` du personnage :

| bit de `+0x12` | nom dans le binaire | en français | ce que le bit porte, dans les données |
|---|---|---|---|
| 0 | `Fighter` | Guerrier | les épées longues et larges, le manteau de croisé |
| 1 | `Priest` | Prêtre | les masses, les crosses, le marteau de prêtre |
| 2 | `Rogue` | Voleur | les poignards, le stylet, l'insigne de brigand, le Glaive |
| 3 | `Sorcerer` | Sorcier | les bâtons et les baguettes de mage |
| 4 | `Hunter` | Chasseur | les arcs, les flèches, la casquette de chasseur, les herbes |
| 5 | `Elf` | Elfe | les rapières, les bottes et le chapeau d'elfe |
| 6 | `Dwarf` | Nain | **toutes** les haches, dont Gulwick |
| 7 | `Fairy` | Fée | les baguettes de fée |

Trois relevés indépendants concordent, ce qui fixe l'ordre : les pointeurs ci-dessus ;
`SetClassBaseHpMp` (`Screens/State04/CharacterCreation.cs`), qui donne 16 PV / 2 PM aux classes
0 et 6, 14/8 aux classes 1, 3 et 7, 15/4 aux classes 2, 4 et 5 — soit les paires
Guerrier/Nain, Prêtre/Sorcier/Fée et Voleur/Chasseur/Elfe ; et la dernière colonne du tableau,
qui est le contenu du jeu lui-même, obtenu en listant les objets dont `+0x12` n'allume qu'un bit.

#### Où vont les seize mots de `+0x20`

`ApplyEquipmentStatBonuses` (`FUN_00538e20`) les ajoute **tous au même tableau** :
`primary[k] += bonus[k]`, avec `primary = character + 0x120`. Les indices 0 à 7 tombent donc sur les
caractéristiques, les indices 8 à 15 sur les **huit valeurs dérivées** de `+0x130` que
`ComputeDerivedStats` vient d'y écrire.

Mais la fiche de personnage n'en dessine que **quatre**, et pas dans l'ordre : elle boucle
`i = 0..3` sur `character + 0x130 + s_StatOrder_0056a8d0[i] * 2`, et cette table vaut
`[0, 1, 4, 5, 2, 3, 6, 7]`.

| indice | offset dans l'objet | emplacement de `+0x130` | ce que c'est |
|---:|---|---:|---|
| 8 | `+0x30` | 0 | **ATT** |
| 9 | `+0x32` | 1 | **DEF** |
| 10 | `+0x34` | 2 | non affichée en jeu normal |
| 11 | `+0x36` | 3 | non affichée en jeu normal |
| 12 | `+0x38` | 4 | **MATT** |
| 13 | `+0x3a` | 5 | **MDEF** |
| 14 | `+0x3c` | 6 | non affichée en jeu normal |
| 15 | `+0x3e` | 7 | non affichée en jeu normal |

**Les noms viennent de l'écran, pas d'une déduction** : relevés en jeu par Vincent le 2026-08-26 en
équipant des objets aux valeurs distinctes et en regardant lesquelles bougent. Une mesure sous sonde,
le même jour, le confirme côté mémoire : le Bâton d'Apollon (indices 8 et 12 à +121 et +82) fait
passer les emplacements 0 et 4 de `10` et `2` à `131` et `84`.

**Ce qui fixe les quatre noms, côté code** : le système d'états ne touche *que* les emplacements
0, 1, 4 et 5. `FinalizeEquipmentEffects` lit huit bits de `+0x140` — `0x400`/`0x800` pour
l'emplacement 0, `0x1000`/`0x2000` pour le 1, `0x10000`/`0x20000` pour le 4,
`0x40000`/`0x80000` pour le 5 — une paire hausse/baisse chacune, plus un bit `0x200` qui monte le 0
et descend le 1, la rage. **Les emplacements 2, 3, 6 et 7 n'ont aucun bit d'état.** Ce sont donc bien
les quatre autres qui portent les statistiques de combat, indépendamment du relevé à l'écran.

**Les huit se répondent deux à deux.** `ComputeDerivedStats` les tire des sept caractéristiques et
du niveau (`+0x144`), et chaque paire sort des mêmes sources :

| emplacement | rang | nom | tirée de |
|---:|---:|---|---|
| 0 | 0 | **ATT** | niveau, carac. 1 |
| 2 | 4 | contrepartie de l'ATT | niveau, carac. 1 et 4 |
| 1 | 1 | **DEF** | niveau, carac. 4 et 5 |
| 3 | 5 | contrepartie de la DEF | niveau, carac. 4 et 5 |
| 4 | 2 | **MATT** | niveau, carac. 6 |
| 6 | 6 | contrepartie de la MATT | niveau, carac. 3 et 6 |
| 5 | 3 | **MDEF** | niveau, carac. 2 et 3 |
| 7 | 7 | contrepartie de la MDEF | niveau, carac. 2 et 3 |

**Les quatre contreparties sont probablement des résistances** — et c'est le **contenu du jeu** qui
le dit, pas le code. Quatre relevés concordent :

- les objets qui ne posent *que* ces quatre-là sont tous des objets de protection, et leurs
  descriptions le disent : « Renforce le système immunitaire », « Augmente la résistance de
  l'esprit », « Protection anti-sortilèges », « une formule sacrée qui sanctifie » ;
- la série **Médaillon de bronze / d'argent / d'or** les pose à 6, 12 puis 24 sur les indices 11
  **et** 15 à la fois, et sur rien d'autre — trois paliers du même objet ;
- sur les 128 objets qui en posent au moins une, **66 posent toutes les leurs à la même valeur** ;
- une arme pure n'y touche jamais : le Glaive et la Longue épée ne posent que l'indice 8.

**Ça reste une hypothèse.** Aucun lecteur n'est identifié — le combat n'est pas reconstruit — et le
jeu ne leur donne aucun libellé, même en mise au point. À écrire comme telle jusqu'à preuve.

Elles **sont dessinées**  — mais seulement quand le verrou `DAT_005633a8` est
tombé, c'est-à-dire après « Gurkha » : une colonne nue en bas à droite, en `x = 0xd4`, **sans
libellé**. Les développeurs de 1998 ne les nommaient pas non plus.

**Rien d'autre ne les lit.** Dans tout `Blaze.c`, `+0x130` apparaît quatre fois — les deux boucles
d'affichage, la recopie depuis `+0x290`, et la remise à l'échelle par la difficulté ; les copies de
travail `+0x292`, `+0x296`, `+0x29a` et `+0x29e` apparaissent **une seule fois chacune**, leur
propre écriture ; et seules deux fonctions sont jamais appelées avec la base `+0x100`
(`FUN_00538f50` et `FUN_00538fa0`), les deux passes de recalcul. Réserve à garder : `Blaze.c` est
une décompilation du code seul et Ghidra a laissé des corps de côté ; le combat n'est pas
reconstruit, donc « absent de `Blaze.c` » ne veut pas dire « absent du binaire ».

**Les malus ne se voient jamais.** `FinalizeEquipmentEffects` termine par un écrêtage sur seize
`short` à partir de `+0x120` : `< 0 → 0`, `> 999 → 999`. Les six valeurs négatives du Bâton
d'Apollon écrasent donc leur emplacement à zéro au lieu de le décrémenter. C'est le jeu de 1998, pas
une erreur de lecture.

**L'indice 7 ne sert à rien.** Il tombe sur `character + 0x12e`, la **huitième** case d'un tableau
que le jeu ne remplit qu'à **sept** : `DrawStatsTab` boucle `i < 7`, `ComputeDerivedStats` ne lit que
`0x120` à `0x12c`, l'écran de création n'ouvre que sept lignes, et la huitième colonne de
`s_ClassBaseStats_0056c7e0` est l'élément du personnage, pas une caractéristique. La chaîne `0x12e`
apparaît **une seule fois** dans les 98 000 lignes de `Blaze.c`, et c'est une écriture. Vérifié en
jeu : la Cuirasse de maître y met `+5` et aucune valeur de la fiche ne bouge.

Des sept caractéristiques, **seule la troisième est nommée** à ce jour — **Volonté**, relevée à
l'écran. Les six autres restent numérotées dans la page plutôt que devinées.

### Les huit éléments

Les huit octets de `+0x18` sont **les huit éléments**, et ce ne sont pas des noms devinés : le jeu
porte une **« Chaîne de X » par emplacement**, chacune décrite « Protection des élémentaux de X », et
chacune ne remplit que son propre octet.

| emplacement | élément | témoin |
|---:|---|---|
| 0 | **Feu** | Chaîne du feu · Cape d'été · Épée de feu |
| 1 | **Eau** | Chaîne de l'eau · Cape d'hiver · Bouclier de glace |
| 2 | **Terre** | Chaîne de la terre · Cape du printemps · **Gulwick**, 25 |
| 3 | **Air** | Chaîne de l'air · Cape d'automne · Épée du tonnerre |
| 4 | **Lumière** | Chaîne de la lumière · Lame du soleil |
| 5 | **Ténèbres** | Chaîne des ténèbres · Armure noire |
| 6 | **Sacré** | Chaîne sacrée · Bouclier sacré · Gants sacrés |
| 7 | **Mal** | Chaîne du mal · Anneau de Satan · La dévoreuse |

Trois recoupements indépendants les fixent : les huit chaînes, les **quatre capes saisonnières** —
été, hiver, printemps, automne remplissant exactement les emplacements 0 à 3 — et **Gulwick**, « hache
dorée abritant l'âme de la terre », qui porte 25 en emplacement 2 là où la FAQ GameFAQs du jeu
annonce *25 Earth*.

**Et les monstres portent les mêmes huit**, à `modèle+0x28` — le bloc dont le rôle était un
`TODO(REVERSE)` de longue date. Le contrôle est immédiat : Triton glace et Varan glace en Eau,
Grifon en Air, Fantôme en Ténèbres et Mal, Archange noir en Ténèbres. Sur 281 fiches, **214** ont au
moins une affinité.

**Ce qui reste sans nom** : les huit caractéristiques du personnage (indices 0 à 7 de `+0x20`) et les
six derniers shorts. Leurs libellés vivent dans une ressource de texte que le relevé ne lit pas
encore, et leur donner des noms plausibles serait exactement ce que `Strategy.md` interdit.

**Et l'octet de tête du nom court n'est pas une lettre.** `BuildItemDisplayName` le dit textuellement : quand
l'octet 0 dépasse `0x5f` alors que l'octet 1 reste en dessous, le jeu l'émet comme la séquence de
contrôle `0x24 <octet>` — un pictogramme — et le nom commence en `+1`. Sur 1 618 fiches l'octet 0 ne
prend que quatre valeurs : `w`, `m`, `s`… et le `L` de « La dévoreuse », qui ne dépasse pas `0x5f`
et reste donc une vraie lettre. Une lecture naïve colle les trois premiers devant chaque nom.

## Les huit fichiers

| fichier | contenu |
|---|---|
| `bestiaire.md` | le rapport lisible, zone par zone |
| `modeles.csv` | une ligne par fiche : nom, rang, PV, PM, seuil, huit caractéristiques, et les 0x60 octets bruts |
| `especes.csv` | l'aiguillage : IA, modèle, classe de butin, drapeaux |
| `formations.csv` | un placement de monstre par ligne : formation, espèce, X/Y/Z, orientation |
| `butin.csv` | une ligne par rang : classe, rang, identifiant d'objet, nom, probabilité |
| `objets.csv` | le catalogue par sous-zone : identifiant, nom, **description**, **bonus**, et les 0x140 octets bruts |
| `butin_par_objet.csv` | **l'index inverse** : un objet, et tout ce qui le lâche — zone, monstre, rang, chance, nombre de placements |
| `lieux.csv` | les **noms de lieu** relevés dans les textes de chaque sous-zone |
| `coffres.csv` | **un coffre par ligne et par rang** : zone, numéro, rôle, gestionnaire résolu, position, rangée de butin, objet et chance |

### Les noms de zone

Le jeu écrit ses noms de lieu dans ses textes sous la forme `« - Le labyrinthe des morts - »`, suivie
du sous-niveau (`« - 1er sous-sol - »`). Le relevé les cherche par ce motif dans les octets de chaque
répertoire de sous-zone : ce n'est pas la lecture d'une table déclarée, mais c'est ce qui donne
l'**association nom ↔ indice de zone**, la seule chose qu'on ne puisse pas deviner de l'extérieur.

| zone | nom, orthographe du jeu |
|---:|---|
| 1 | La mine abandonnée |
| 2 | La montagne du dragon de feu |
| 3 | La forêt |
| 4 | La vieille tour |
| 5 | Le labyrinthe des morts |
| 6 | Le château de l'immortel |
| 7 | Les ruines |
| 8 | La vallée d'argent |
| 9 | Les ruines dans le lac |
| 10 | La grotte scellée |

Le nom dominant d'une zone se dégage nettement — dix occurrences pour « La mine abandonnée » en
zone 1, treize pour le château en zone 6 — parce qu'il est répété dans chacune de ses sous-zones,
là où les noms de salle n'apparaissent qu'une fois. Les zones **7 et 9** n'ont pas de forme courte
dans les textes : elles ne portent que leurs variantes (« Les ruines souterraines », « de la
surface », « dans le lac »), et la page retient celle qui les distingue.

**Les rangs se cumulent.** Un même objet occupe souvent plusieurs des huit rangs d'une classe, et
`butin_par_objet.csv` garde alors une ligne par rang — c'est la donnée fidèle. Pour la chance réelle
d'un monstre sur un objet, il faut **additionner** : l'Abeille lâche l'Aiguille médicinale aux rangs
0, 1 et 2, soit 25 + 17 + 15 = **57 %**, et non 25 %. La page fait ce regroupement ; une lecture
directe du CSV doit le faire aussi.

## La page consultable

`bestiaire.html` est une page autonome — deux vues, monstres et objets, avec la fiche de 0x60 octets
affichée champ par champ. Elle s'ouvre directement dans un navigateur, sans serveur.

**La ligne est le monstre, pas la fiche.** Le même Goblin a jusqu'à **neuf** fiches — une par
sous-zone où il apparaît, avec des valeurs qui montent d'une zone à l'autre (rang 2 en forêt,
rang 30 plus loin). Les lister à plat remplissait le tableau de doublons ; le nom fait donc la
ligne, les colonnes donnent l'étendue, et le dépliage montre chaque variante avec son rang, ses
jauges, son seuil, son numéro d'IA et son butin. Filtrer par zone réduit aussi la liste des
variantes, pas seulement celle des lignes.

Elle est **générée**, pas tenue à la main :

```powershell
dotnet run --project OpenForesia.Tools -- bestiaire "E:\" --sortie Documentation\Reference\Bestiaire
OpenForesia.Tools\build-bestiaire-page.ps1
```

Le premier appel refait les six CSV depuis le CD, le second les joint et les injecte dans
`bestiaire-page.template.html`. **Modifier `bestiaire.html` à la main est perdu au prochain
appel** : c'est le gabarit qu'il faut toucher.

### Vérifier la page

```powershell
node OpenForesia.Tools\check-bestiaire-page.mjs
```

Le contrôle **exécute le rendu** dans un DOM minimal — les deux vues, liste et dépliage, plus chaque
filtre de zone — puis relit ce qui a été produit et échoue sur tout `undefined`, `NaN`,
`[object Object]` ou tableau vide. **Le lancer après toute modification du gabarit.**

Ce n'est pas un détail de mise en œuvre. Une vérification qui relit la *charge utile* passe alors que
la page affiche `undefined` partout, parce que le défaut est dans le chemin qui construit la ligne,
pas dans les données. Trois défauts sont sortis comme ça, tous de la même famille — **un champ lu sur
une entrée qui ne le porte plus depuis le regroupement par nom** :

- la ligne d'objet ne transportait ni description ni ATT/DEF jusqu'au dépliage ;
- les maxima des barres de PV se lisaient encore sur les fiches, d'où des largeurs en `NaN` ;
- la liste des zones aussi, d'où une puce unique « Zundefined » qui vidait les deux vues.

Le troisième a échappé aux deux premières versions du contrôle parce que **les puces sont construites
par `createElement`, pas par `innerHTML`** : il ne suffit pas de relire le tableau, il faut aplatir
aussi ce que le script a créé élément par élément. C'est ce que le contrôle fait maintenant.

## Ce que ça donne

**281 fiches dans les zones 1 à 10, 121 noms distincts** — du Goblin au Dragon rouge (8 000 PV), en
passant par Belzébuth, Cerbère et Behemoth. **913 formations et 3 018 placements** de monstres,
chacun avec ses coordonnées. **386 objets au catalogue, dont 265 obtenables sur un monstre**
et **274 dans un coffre** — dont 112 que nul monstre ne lâche. Ce qui reste vient des marchands, qui
ne passent pas par cette table. **161 coffres** dans les zones 1 à 10. Les bornes de la
lecture sont dans `bestiaire.md` zone par zone.

**La zone 0 est à écarter.** Ses 102 lignes portent des noms illisibles et se répètent avec une
période de huit sous-zones : sa liste de sous-zones est vraisemblablement mal parcourue, ou la
zone 0 — le monde extérieur — n'a tout simplement pas de table de fiches à `+0x98`. À vérifier avant
de s'en servir ; le reste du relevé ne dépend pas d'elle.

## Ce qui n'est pas ici, et pourquoi

**Les compétences des monstres ne sont pas des données.** Elles vivent dans les fonctions d'IA — les
onze de la forêt, les 166 du jeu — et se lisent une par une dans le binaire. La colonne « IA » de
`especes.csv` est le pont : elle donne, pour chaque monstre nommé, le numéro qui l'aiguille dans la
table de sauts `0x0056ad48`. C'est la première fois que le chantier des 166 gestionnaires a des
**noms** en face de ses numéros.
