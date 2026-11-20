# Le monde

Sources : `Documentation/Reference/Bestiaire/` (`lieux.csv`, `modeles.csv`, `formations.csv`,
`coffres.csv`, `decor.csv`), `Documentation/Etat/EffetsDesBoss.md`, `Documentation/Ennemies/Comportement.md`,
`Screens/State02/Destination.cs`.

## La structure (établi)

- **Le hub** — l'auberge (zone 0) : réception, chambres, salon, la salle du coffre. Elle existe en
  plusieurs jeux de cartes successifs (sous-zones 0-5, 8-12, 16-21, …) ; que ce soient les états de
  l'auberge au fil de l'histoire est une **lecture**.
- **La carte du monde** — un écran de choix de destination ; seules les destinations débloquées sont
  sélectionnables. Le déblocage tient dans un octet de progression.
- **Dix zones d'aventure**, chacune découpée en **sous-zones** : une sous-zone est une carte 3D
  chargée d'un bloc, avec ses monstres, son décor, ses coffres et ses comportements. On passe de
  l'une à l'autre par des sorties (portes, escaliers, téléporteurs).
- Dans une sous-zone, au plus **32 entités de scène** (monstres, objets animés) vivent en même temps
  — une limite de la machine, voir [07-Heritage-PS1](07-Heritage-PS1.md).

## Les zones

Rang = le « niveau » du monstre (comparé au niveau du personnage pour l'expérience et le butin).
Les noms de lieu sont ceux que le jeu affiche.

| # | zone | lieux | monstres (rang) | boss |
|---|---|---|---|---|
| 1 | **La mine abandonnée** | la mine, les ruines souterraines | Goblin, Goblin-sorcier, Chef-Goblin, Chauve-souris, Vipère, Scorpion, Araignée, Mille-patte, Ours gris, Crabe, Poisson tueur, Maudite, Belle bleue, Salamandre, Ogre, Géant vert (15 à 100) | **Jeune dragon** (40, 700 PV), **Troll** (130, 850 PV) |
| 2 | **La montagne du dragon de feu** | — | Salamandre, Salamandre feu (115-155) | **Dragon rouge** (306, 8 000 PV) |
| 3 | **La forêt** | la forêt aux ruines, le pré au bord du lac, le hangar à bateaux, l'île aux ruines, les chutes d'eau de Maldo, le terrain marécageux, la cabane et sa cave, le lieu de coupe, le sentier pierreux, la vallée ensablée, la grotte souterraine | Goblin, Coccinelle, Fourmi géante, Poisson volant, Crabe géant, Crotale, Grizzly, Abeille, Abeille tueuse, Homme-lézard, Trent, Fleur du mal, Lézard noir, Willie, Varan, Wyrm, Géant (2 à 95) | **Méchant ours** (38, 300 PV), **Behemoth** (150, 2 160 PV) |
| 4 | **La vieille tour** | annexe, tour principale, entrepôt des cristaux, étage supérieur, point culminant | Goblin, Goblin volant, Gremlin, Belle verte, Garde golem, Lézard rouge, Crapouille, Sorcier, Homme-lézard, Golem-squelette, Rampant, Chevalier noir, Sabre volant, Armure volante, Archange noir, Gros dur, Platinum, Ténébreux (26 à 120) | **Sylphide et Wybarn** (100 / 96), **Enchanteresse** (140, 910 PV), Cristal maudit (255, 900 PV) |
| 5 | **Le labyrinthe des morts** | la salle de purification de l'eau bénite | Tas d'os, Zombie, Dunœud, Persil, Belle rouge, Mort-vivant, Sanguinaire, Momie, Sanglante, Agonisant, Ombre, Revenant, Belle jaune, Vieille momie, Reine momie, Durahan noir, Ombre rouge (35 à 126) | **Durahan** (80, 808 PV), **Roi-zombie** (150, 1 350 PV) |
| 6 | **Le château de l'immortel** | ailes est et ouest (trois niveaux, salle de bal), tour de l'horloge, toit, salle du trésor, bâtiment principal, combles et salle d'audience | Zombie, Harpie, Loup, Loup gris, Fantôme, Sanguinaire, Crapouille, Gargouille, Petit vampire, Vampire, Ombre, Pourpre preux, Chimère, Sacuba (35 à 125) | **Loup-garou et Tigre** (85), **Nocturio** (150, 840 PV) |
| 7 | **Les ruines** | les ruines souterraines, de la surface, le Laboratoire | Crapaud, Loup, Chimère, Balle métal, Sosie (38 à 140) | — |
| 8 | **La vallée d'argent** | le nid des monstres | Ours blanc, Loup blanc, Grifon (42 à 65) | **Fougre** (180, 1 030 PV) |
| 9 | **Les ruines dans le lac** | les ruines dans les eaux | Lézard noir, Wyrm | **Kraaken** (bras, pieds et corps, 82 à 200) |
| 10 | **La grotte scellée** | la Porte des Sages, le lieu maudit, la grotte sous la glace, la région chaude, la forteresse du vent, la salle des démons, le pays des tempêtes, le dernier étage | Sale rampant, Ombre rouge, Maudite, Bras gris, Gremlin, Mort volante, Noir goblin, Shamane, Preux mort, Sanpeur, Infernal, Basirisk, Groklebs, Sacuba, Triton glace, Varan glace, Salamandres, Gorgon (87 à 168) | **Dragon zombie** (180, 4 400 PV), **Cerbère** (200, 2 100), **Eifridt** (212, 2 400), **Le gros poilu** (218, 2 300), **Lordémon** (235, 2 763), **L'Ange noir** (238, 2 391), **Belzébuth** (320, 3 000) |

L'ordre des zones est leur **numéro interne**, pas forcément l'ordre de jeu ; l'ordre de déblocage
est **à établir** (il se lit dans l'octet de progression et les scènes qui l'écrivent). Les rangs
suggèrent une montée forêt → mine → tour → labyrinthe → château → vallée/lac → grotte scellée,
avec la montagne du dragon comme défi à part : **lecture**.

La grotte scellée concentre sept boss : c'est vraisemblablement le donjon final, découpé en régions
élémentaires (glace, chaleur, vent, tempêtes, démons) — **lecture** d'après les noms de lieu.

## Le contenu d'une sous-zone

Chaque sous-zone porte (**établi**, ce que l'outil `bestiaire` relit) :

| quoi | où dans les exports |
|---|---|
| les **formations** de monstres : quelle espèce, où (X, Y, Z), orientation, variante | `formations.csv` |
| les **espèces** : quelle IA, quelle fiche, quelle table de butin | `especes.csv` |
| les **fiches** : nom, rang, PV, PM, caractéristiques, affinités | `modeles.csv` |
| le **décor actif** : coffres, portes, leviers, déclencheurs, sons, effets — et sa position | `decor.csv`, `coffres.csv` |
| les **textes** : noms de lieu, panneaux, dialogues | (dans les données ; l'exporteur de scénario les sortira) |

La **géométrie** de la carte (sols, murs, collisions) et les **modèles 3D** sont dans les données du
jeu et lus par la reconstruction ; leur export vers un format moderne est l'objet des exporteurs à
venir.

## Les boss

Dix-neuf combats de boss recensés dans `EffetsDesBoss.md`, avec, pour chacun, les effets et les sons
propres à sa sous-zone. Le portage les a déjà sortis de leur carte pour une arène (LP-013, LP-014),
ce qui prouve qu'un boss se décrit entièrement par : sa fiche, son IA, ses effets et ses sons.

> **Pour un jeu neuf.** La structure hub → destination → zone en cartes successives est un
> *level streaming* naturel sous Unreal (un niveau persistant par zone, un sous-niveau par carte).
> La limite de 32 entités disparaît ; elle a pourtant façonné le rythme des combats (petits groupes),
> qu'il faudra garder **par choix** si on l'aime. Le hub qui change au fil de l'histoire est une
> bonne idée à reprendre.
