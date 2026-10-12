## Context

Voir `proposal.md` pour le pourquoi, et `specs/gameplay-fairy-support/spec.md` pour le contrat.

Ce que le code transcrit fait aujourd'hui, relevé pendant l'exploration du 2026-10-02 :

- **Chaque sort de l'école 2 pose son effet par UN appel direct**, dans
  `Spells/School2Elementalist.cs`, et **prête juste avant sa paire famille/indice au lanceur**
  (`*(ushort*)(lanceur + 6) = *(ushort*)(e + 6)` : `+0x06` l'indice, `+0x07` la famille). Les
  sites :

  | sort (indice) | corps qui pose | effet |
  |---|---|---|
  | Dodo (0) | `TickSleepBubble_00476CF0` | 11 `ApplyStatusEffect_0053B730` |
  | Gros sabots (1), Ralenti (12) | `TickHeavyHoofStomp_004770A0` — **le même sabot `0x7e`** | 20 `ApplyStatusEffect_0053BDA0` |
  | Vitesse (2), Turbo (13) | `TickHasteRing_00477420` — **le même anneau `0x81`** | 19 `ApplyStatusEffect_0053BD10` |
  | Matéria feu/terre/air/eau (3–6) | `Tick*Materia_00477…` | 3 à 6 |
  | Chant sirènes (7) | `TickSirenSongNote_00477A90` | 8 `ApplyStatusEffect_0053B4B0` |
  | Écrase (8) | `TickCrushOrb_00477E40` | 10 `ApplyStatusEffect_0053B660` |
  | Coussin d'air (10) | `TickAirCushion_004785F0` | 28 |
  | Superman (14) | son anneau `0x82` | 26 `ApplyStatusEffect_0053C160` |

- **Les quatre malus ont un masque d'immunité et un jet** : effet 8 masque `0xf3299500` jet B,
  effet 10 `0xf3099500` jet B, effet 11 `0xfbaf9500` jet B, effet 20 `0x80000000` jet A. Les
  **bonus n'en ont pas** : ils passent toujours.
- **Un coup réveille** : `ApplyDamage_00539A20` efface le bit `0x40` et la durée `+0x96` en tête.
- **Trois précédents « de groupe » dans le binaire** : Turbo (Vitesse en boucle), Coup de pouce
  (balaye `Inp.s_SlotFlags_005e93c0`, 4 × `0x9c`, membre retenu si `0x80000000` et pas
  `0x50000000`) et Zombie (balaye `0x005ecaa0`, `0x20` × `0x9c`, entité retenue si `0x80000000`
  et pas `0x40002000`, à moins de `0x5dc` du lanceur en trois dimensions, `Ordinal_442`).
- **Les objets lancent aussi des sorts de l'école 2** (opcode 14 : Superman 14 emplois, Dodo,
  Chant sirènes, Écrase, Ralenti, Turbo, Coussin d'air…) : la fiche seule ne dit pas qui lance.

## Goals / Non-Goals

**Goals :**
- Tout le tweak tient dans `src/LegacyPlus/SoutienFee.cs` ; `src/Blaze/` ne reçoit que des lignes
  de couture, identité par défaut.
- Les règles d'origine qui ne sont pas l'immunité restent : appareillage (`PrepareCombatPair_00538870`,
  donc mort, mourant, Blindé), durée qui ne fait que s'allonger, réveil au coup.

**Non-Goals :**
- Pas de nouvelle mise en scène pour les cibles supplémentaires : elles reçoivent l'effet et la
  marque que l'effet fait naître lui-même, pas le halo ni la marque d'Écrase de la victime touchée.
- Pas de liste de boss : le spec veut le malus à 100 % boss compris, aucune distinction n'est
  nécessaire.
- Rien sur Blindé, Champ magique, Escargot, ni les écoles 0, 1 et 3.

## Decisions

### D1. Qui est « la Fée » : la classe du lanceur, lue au moment de la pose

Le tweak agit quand le lanceur est **un membre du groupe** (`0x800000` du mot 0) dont la fiche
(`MainMem + (entité[8] + 0x78) * 0x2000`) porte la classe 7 (`fiche[0x150] & 7`), et que la paire
prêtée vaut **famille 2** (`+0x07`) avec un indice de la liste.

- *Plutôt que la fiche seule* : un objet de Superman utilisé par le Guerrier ne doit pas devenir un
  sort de groupe — le but est la plus-value de la Fée.
- *Conséquence acceptée* : un objet qui lance un sort de l'école 2, **utilisé par la Fée**, est
  amplifié aussi. Les distinguer demanderait de suivre la posture d'emploi d'objet ; à revoir si
  Vincent le souhaite.

### D2. Une couture par pose d'effet, qui reçoit l'effet à appliquer

Chacun des onze appels du tableau devient un appel à `Coutures.EffetDeFee(lanceur, victime, effet)`,
où `effet` est un pointeur de fonction vers l'`ApplyStatusEffect_…` d'origine. Défaut :
`effet(lanceur, victime, 0)`, et le résultat est rendu tel quel — trois sites s'en servent (le
sabot meurt si l'effet échoue ; Chant sirènes et Écrase n'engendrent leur suite que s'il passe).

Le tweak, quand D1 est vrai :
1. pose un **contexte** (statique : le jeu est mono-fil) — « malus forcé » pour les indices 0, 1,
   7, 8, 12 ; « durée ÷ 2 » pour les indices 1 et 2 ;
2. appelle l'effet sur la victime d'origine et garde son résultat ;
3. **diffuse** (D4) : bonus → chaque autre membre vivant ; malus → chaque autre ennemi en portée ;
   Turbo (13) ne diffuse pas, il est déjà de groupe ;
4. efface le contexte et rend le résultat de l'étape 2.

*Alternatives écartées* : une couture dans la table des 48 effets (`ApplyItemEffect_0056D4E0`) — les
sorts de l'école 2 ne passent pas par elle, ils appellent l'effet directement ; une couture dans
chaque `ApplyStatusEffect_…` — elle toucherait les pièges, l'équipement et les monstres, et ne
saurait pas quel sort l'appelle.

### D3. Le 100 % : une couture devant le masque et devant le jet, lue dans le contexte

Dans les effets 8, 10, 11 et 20 seulement :
`if (!Coutures.MalusForce() && (bloc[+0x14] & masque) != 0)` et
`if (!Coutures.MalusForce() && RollSavingThrow…(…) != 0)`. Défaut : `false`. Le tweak rend vrai
**seulement pendant son étape 2–3** (D2).

- Le court-circuit **évite le tirage** du jet : aucun nombre aléatoire n'est consommé à sa place
  (règle des tweaks).
- Le contexte borne l'effet à la Fée : un piège, une arme à effet ou un monstre qui posent ces
  mêmes effets gardent jet et immunité, même si la paire `+0x06` du porteur est restée sur un sort
  de l'école 2.
- `PrepareCombatPair_00538870` n'est pas touché : cible morte, mourante, invalide ou Blindée reste
  refusée.

### D4. Les cibles de la diffusion, sur les précédents du binaire

- **Bonus** : les quatre emplacements de `Inp.s_SlotFlags_005e93c0`, filtre de Coup de pouce
  (`0x80000000` posé, `0x50000000` absent), sans distance, la victime d'origine sautée.
- **Malus** : les `0x20` entités de `0x005ecaa0`, filtre de Zombie **sans** son critère d'espèce ni
  son `+0x74` (`0x80000000` posé, `0x40002000` absent), distance au **lanceur** en trois dimensions
  `< 0x5dc` calculée comme Zombie (`>> 0xc` puis `Ordinal_442`), la victime d'origine sautée.

### D5. La durée ÷ 2 : une couture sur la durée calculée des effets 19 et 20

`duree = Coutures.DureeDEtat(duree)` juste après le calcul (`+0x38 >> 3` + `0x28`), avant la
comparaison avec la durée en cours. Défaut : identité ; le tweak rend `duree / 2` quand le contexte
« durée ÷ 2 » est posé. Comme le sabot et l'anneau sont partagés entre Gros sabots/Ralenti et
Vitesse/Turbo, **c'est l'indice prêté (`+0x06`) qui décide**, pas le corps.

### D6. Les quatre projectiles : une couture à l'engendrement de la graine

Dans `TickSleep_00476C70`, `TickSirenSong_00477A20` et `TickCrush_00477D20`, l'appel unique à
`SpawnStatusEffectEntity_0047AC40(e, …)` devient `Coutures.ProjectileDeFee(e, engendrer)`. Défaut :
un engendrement. Le tweak (D1 vrai) engendre quatre fois, en tournant le cap de la graine `+0x32`
de `0x400` (un quart de tour) entre chaque, puis rend le cap d'origine. Chaque bulle hérite de son
cap par la recopie de l'enregistrement et cherche sa cible dans son propre cône
(`PickTargetInCone_00474F00`).

*Vérifié à l'implémentation* (tâche 3.1) : `SpawnStatusEffectEntity_0047AC40` recopie les `0x27`
dwords de la graine avant d'écraser la position, le sous-type et une dizaine de champs — `+0x32`
n'en fait pas partie, l'enfant part donc sur le cap tourné. Le verrou `0x80` que chaque bulle pose
sur le lanceur à sa première image est levé par ses **trois** fins (décor, quarante images,
victime) : la première bulle qui meurt libère la Fée, au plus tard quarante images après le lancer.

### D8. Après le premier essai (2026-10-02) : copier le sort plutôt que diffuser l'effet

Le premier essai de Vincent a validé l'effet (le buff s'applique à tous, Gros sabots ralentit tout
le monde) mais pas la mise en scène : la diffusion de D2 pose l'**effet** sur les autres cibles,
pas l'**animation** — les anneaux de Vitesse, le sabot qui tombe n'apparaissent que sur la cible
d'origine. Et Dodo, avec quatre bulles **et** la diffusion, endormait tout dès la première bulle.

Décision :
- **Bonus et Gros sabots : une copie du sort par cible supplémentaire.** Une couture de plus,
  `CibleDeFee(sort, cible)`, sur la recherche de cible de leur première image (huit sites). Le
  tweak y fait naître, dans la table des sorts (`0x005ff400`, liste libre `0x005fa5a0`, par
  `AllocEntitySlot`, rendue `internal`), une **copie exacte des `0x9c` octets** du sort — seul son
  indice `+0x08` change — et lui **réserve** une cible : un autre membre vivant (filtre de Coup de
  pouce) pour un bonus, un autre ennemi à portée pour Gros sabots. À sa première image, la copie
  passe par la même couture et prend la cible réservée (vérifiée : même lanceur, même sous-type,
  cible encore vivante). Chaque copie joue alors **son propre corps** — anneaux, flammes, sabot,
  sons — et pose l'effet sur **sa** cible. C'est la forme de Turbo (Vitesse en boucle) et de Bobos
  finis (Sang pur par membre).
  - La copie est prise **après** la recherche, donc après les deux seules écritures qui la
    précèdent (Matéria, Superman : `+0x36 |= 6`, `+0x2e = -64`), toutes deux idempotentes quand la
    copie les refait.
  - La passe `TickEntityTable_005ff400` rend l'emplacement par `record[8]` quand le corps éteint
    l'entité : la copie se libère comme un sort d'origine.
- **Plus de diffusion pour les bonus, Gros sabots et les trois sorts à bulles.** La diffusion de D2
  ne reste que pour Ralenti (dont le sabot, répété toutes les huit images, ne se copie pas
  simplement).
- **Dodo, puis Chant sirènes et Écrase** (second essai, même jour) : chaque bulle n'agit que sur ce
  qu'elle touche — toujours sans jet ni immunité. Avec quatre bulles **et** la diffusion, la
  première bulle qui touchait affectait tout le monde.

### D7. L'interrupteur

`[Magie] SoutienFee`, lu dans `ReglagesLegacyPlus` comme `RamassageAuto` et `Hub`, en acceptant en
plus `true` / `false` ; `SoutienFee.Installer()` ne s'empile sur les coutures que s'il est actif.

## Risks / Trade-offs

- [Une entité de scène qui n'est pas un ennemi — marchand du hub, PNJ scripté — dans la portée
  serait endormie ou ralentie] → les lister dans le hub et l'arène (tâche 5.3) ; si besoin, les
  exclure par leur entité ou leur sous-zone, sans changer le spec.
- [Certains boss pilotent leur propre mouvement : sommeil et lenteur peuvent n'y rien changer de
  visible] → comportement de leur corps d'origine, pas un défaut du tweak ; noté dans `legacy.md`
  après l'essai.
- [Les trois bulles supplémentaires tirent des nombres aléatoires (leur éclaboussure par image)] →
  c'est le corps du jeu qui tire, comme les monstres de plus de LP-007 ; le code du tweak n'en tire
  aucun.
- [Pool des effets de statut (`0x005f2d40`) plein : une bulle peut ne pas naître] → l'engendrement
  d'origine échoue déjà en silence ; aucune garde à ajouter.
- [Équilibre : Dodo à 2 PM endort tout ce qui est en portée, boss compris] → voulu par Vincent ; le
  réveil au premier coup le borne.

## Migration Plan

Aucune donnée ni sauvegarde touchée. Interrupteur à `non` ou absent : le jeu de 1998, coutures
comprises. Retour en arrière : retirer la clé.
