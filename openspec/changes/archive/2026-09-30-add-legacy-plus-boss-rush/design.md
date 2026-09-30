## Context

Voir `proposal.md` pour le pourquoi, et `specs/gameplay-arena-boss-rush/spec.md` pour le contrat.

L'arène a déjà tout ce qu'un challenge demande, sauf la place :

- **Le directeur** (`AreneDirecteur`) joue un programme vague après vague : une vague finit quand
  chacun de ses emplacements suivis a perdu le bit `0x80000000` (le corps a disparu, pas seulement
  « mort »), puis la suivante naît `Delai` = 30 images plus tard. Aucun programme d'aujourd'hui n'a
  plus d'une vague.
- **La greffe** (`AreneGreffe`) installe, au peuplement de 4/9, **toutes** les espèces du programme :
  en mémoire (rang d'espèce 16 à 31, fiche, créneau d'IA, maillage 3 à 23 et animations, sons de
  niveau) et en VRAM (`Placer` : cinq bandes de 128 × 64 mots, palettes lignes 288 à 319, entrées de
  décodage `0xe8` et suivantes). Le maillage est relogé depuis l'archive quand sa bande ou son entrée
  change (`ChargerModele`, `ModelePour`).

Les bandes demandées, boss seuls : Boss Rush 1, 2 et Ultimate en veulent 5 au total, Boss Rush 3 en
veut 8 (Eifridt 2, Belzébuth 3). Par vague, jamais plus de 3.

## Goals / Non-Goals

**Goals:**
- Un challenge est un programme comme les autres : des vagues, une pause, une bourse.
- Chaque vague dispose des cinq bandes de VRAM.
- Les combats d'un seul boss se comportent exactement comme avant.

**Non-Goals:**
- Pas d'escorte dans les challenges ; pas de nouveau boss greffé (tous existent déjà).
- Pas de sauvegarde de la progression dans un challenge : une défaite ramène au hub, comme
  aujourd'hui, et le challenge recommence au début.
- Pas de couture nouvelle dans `src/Blaze/`.

## Decisions

### D1 — Un challenge est un `Programme` à plusieurs vagues

Les quatre challenges s'ajoutent à `AreneDirecteur.Programmes`, après les dix-neuf combats. Chaque
vague reprend la `Creature` du boss dans son combat seul (formation, rang, espèce greffée), sans les
escortes, à la place du boss (100, −2900). Deux boss d'une même vague sont écartés de part et
d'autre : Loup-garou et Tigre comme dans leur combat ; les deux Dragons zombies en (−150) et (350),
les deux Dragons rouges en (−150) et (350), 500 unités entre eux.

`Programme` gagne deux champs à valeur par défaut : `Pause` (millisecondes entre la disparition
d'une vague et la suivante, 500 par défaut ; 5 000 pour un challenge) et `Challenge` (le menu ouvre
une page avant le premier). La première vague garde le délai d'arrivée de 30 images.

La pause se compte **en temps réel**, pas en images : le premier essai (300 images) durait plus de
cinq secondes en jeu (Vincent, 2026-09-30). Le journal `[ARENE]` donne, à chaque vague, le temps
entre la dernière mort et la disparition, et la pause réellement écoulée.

*Alternative écartée* : une liste de challenges à part, avec son propre directeur — un second
chemin pour la même mécanique.

### D2 — La greffe en deux temps : la mémoire à l'entrée, la VRAM par vague

- **Au peuplement de 4/9** (`Preparer`, inchangé dans son déclenchement) : tables recopiées,
  répertoires relus, espèces chargées et installées en mémoire — rang, fiche, créneau d'IA, rang de
  maillage (retenu dans l'espèce), animations, sons de niveau. Rien en VRAM.
- **Avant chaque vague** (nouveau, appelé par le directeur juste avant `SpawnEntityFormation`) :
  `Placer` sur les seules espèces greffées de CETTE vague, puis pour chacune le maillage relogé si
  sa place a changé, la texture et la palette envoyées, l'entrée de décodage écrite, et le pointeur
  de la table des maillages (`0x005e96a0`) mis à jour.

Un combat d'une seule vague fait donc la même chose qu'avant, un peu plus tard (au moment de naître
au lieu du peuplement) — la VRAM de 4/9 est la même à ces deux moments.

*Alternative écartée* : tout placer à l'entrée et refuser les programmes qui dépassent — Boss
Rush 3 serait impossible.

### D3 — Le saut de page dans l'écran de menu

`EcranMenu.Ligne` gagne `NouvellePage` (faux par défaut). La pagination n'est plus « dix lignes par
page » mais une liste de débuts de page : une page s'ouvre toutes les dix lignes, ou sur une ligne
`NouvellePage`. Gauche / droite passent à la page précédente / suivante, à la même hauteur si elle
existe. La page des modes marque le premier challenge. Les autres écrans (boutiques, rachat) n'ont
aucune ligne marquée : leur pagination ne change pas.

### D4 — Les noms longs

La liste coupe un nom à 20 caractères pour laisser la colonne de droite (prix). Quand la colonne de
droite d'une ligne est vide, le nom a toute la largeur (34 caractères) : « [CHALLENGE] - Boss Rush 1 »
(25) tient.

### D5 — Les bourses

Somme des bourses des combats seuls des boss du challenge (escorte comprise, telle que fixée pour
chaque combat), + 25 %, arrondie à la dizaine quand elle ne tombe pas juste : 600 → 750 ;
1 110 → 1 387,5 → 1 390 ; 1 830 → 2 287,5 → 2 290 ; 4 700 → 5 875. Écrites en dur dans chaque
programme, avec le calcul en commentaire.

## Risks / Trade-offs

- [Un effet du boss précédent encore vivant à la bascule de VRAM, dessiné avec la texture du
  suivant] → la vague ne finit qu'à la disparition du corps, puis cinq secondes passent ; le journal
  `[GREFFE]` écrit à chaque bascule le nombre d'entités de lignée encore vivantes.
- [L'Enchanteresse, boss natif de 4/9, au milieu du Boss Rush 1 : sa mort pourrait réveiller la
  cinématique de fin de la tour avant la dernière vague] → l'arène tait déjà scripts et volumes de
  déclenchement ; à vérifier en jeu en priorité (tâche dédiée).
- [Relogement d'un maillage depuis l'archive au moment de la vague : une lecture de disque dans une
  image] → pendant la pause, dans une salle vide ; acceptable.
- [Sons de niveau : 8 identifiants libres dans 4/9, partagés par les cinq boss] → greffés à la
  demande et coupés s'il n'y a plus de place (sans plantage) ; le journal le dit.
- [Créneaux d'IA de `0x0056ad48`] → cinq espèces au plus par challenge, déjà atteint par le
  Roi-zombie et sa cour (quatre) ; l'erreur « aucun créneau » est journalisée et abandonne le
  programme proprement.
