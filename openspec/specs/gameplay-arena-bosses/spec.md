# gameplay-arena-bosses Specification

## Purpose

Lets a boss from another zone of the game fight in the hub arena exactly as it does at home — its
look, its moves, its sounds and the effects of its own zone — so that every boss fight of the game can
be replayed there, one mode per fight.

## Requirements

### Requirement: A boss from another zone fights in the arena as at home

A boss brought into the arena from another zone SHALL look, move, attack, sound and produce its
effects as it does in its own zone, and SHALL be defeated in the same way. None of its sounds or
effects SHALL be replaced by those of the arena's zone; a sound or effect that cannot be reproduced
SHALL be left out rather than substituted.

#### Scenario: The Grizzly's grab

- **WHEN** a Grizzly grabs a party member in the arena
- **THEN** the grab plays the same sound and the same visual effect as in the forest

#### Scenario: The Méchant ours roars

- **WHEN** the Méchant ours attacks in the arena
- **THEN** it plays the forest's sound, not the tower's sound of the same number

#### Scenario: Nothing from the tower leaks in

- **WHEN** a grafted creature fights in the arena
- **THEN** no creature, sound or effect belonging to the tower appears as a result of its actions

#### Scenario: An effect of the boss's own zone

- **WHEN** a grafted boss, or an effect it produced, gives birth to an effect that belongs to its own zone
- **THEN** that effect is the one of the boss's zone, and it plays, sounds and strikes as it does there

#### Scenario: The Méchant ours breathes

- **WHEN** the Méchant ours uses its breath in the arena
- **THEN** the screen flashes red, the rings spread out and strike the party, and the breath's sound plays, as in the forest

### Requirement: Grafted creatures drop nothing

A creature brought into the arena from another zone SHALL drop neither item nor coins when it dies;
its experience crystals SHALL drop as usual. The arena's reward is the purse paid at victory.

#### Scenario: The Méchant ours dies

- **WHEN** a grafted creature dies in the arena
- **THEN** no item and no coins appear, and its experience crystals do

### Requirement: The arena offers every boss fight of the game

The arena menu SHALL list one mode per boss fight below, each pitting the party against that fight —
the boss and its escort from the fight's formation, in one wave — at the chosen difficulty, with the
arena's usual ending and defeat. The menu SHALL list the modes by the total hit points of the fight at
difficulty 1, boss and escort together, in increasing order; ENCHANTERESSE, the tower's own boss, takes
its place in that order. Each mode pays its purse at victory.

| Mode | Fight (zone / sub-area) | Hit points at difficulty 1 | Purse |
|---|---|---|---|
| MECHANT OURS | the Méchant ours and two Grizzlys (3/5) | 460 | 40 |
| JEUNE DRAGON | the Jeune dragon (1/4) | 700 | 100 |
| ENCHANTERESSE | the Enchanteresse, native to the arena (4/9) | 910 | 150 |
| LOUP-GAROU ET TIGRE | the Loup-garou and the Tigre (6/5) | 968 | 140 |
| NOCTURIO | the Nocturio and two Sacubas (6/11) | 1 220 | 170 |
| SYLPHIDE | the Sylphide and the Wybarn (4/7) | 1 263 | 180 |
| FOUGRE | the Fougre and two Grifons (8/1) | 1 272 | 180 |
| DURAHAN | the Durahan and three Sanguinaires (5/4) | 1 531 | 220 |
| TROLL | the Troll and two Géants verts (1/9) | 1 650 | 230 |
| BEHEMOTH | the Behemoth (3/9) | 2 160 | 300 |
| GROS POILU | le gros poilu (10/12) | 2 300 | 330 |
| ANGE NOIR | l'Ange noir (10/14) | 2 391 | 340 |
| EIFRIDT | Eifridt (10/11) | 2 400 | 340 |
| ROI-ZOMBIE | the Roi-zombie, two Durahans noirs, two Revenants and two Ombres rouges (5/9) | 2 726 | 330 |
| LORDEMON | the Lordémon (10/13) | 2 763 | 400 |
| CERBERE | the Cerbère and four Groklebs (10/9) | 2 812 | 400 |
| BELZEBUTH | Belzébuth (10/15) | 3 000 | 430 |
| DRAGON ZOMBIE | the Dragon zombie and two Maudites (10/4) | 5 438 | 780 |
| DRAGON ROUGE | the Dragon rouge and two Salamandres feu (2/0) | 8 480 | 1 200 |

The Kraaken of the lake ruins is not offered.

#### Scenario: Méchant ours at difficulty 3

- **WHEN** player 1 picks MECHANT OURS at difficulty 3 and confirms
- **THEN** the party faces the Méchant ours with 900 hit points and two Grizzlys with 240 each, all
  dealing three times their original damage

#### Scenario: Victory over the Méchant ours

- **WHEN** the Méchant ours and both Grizzlys are dead
- **THEN** the tower's ending cinematic plays, the portal appears and the party is paid 40 coins times the difficulty

#### Scenario: Behemoth at difficulty 2

- **WHEN** player 1 picks BEHEMOTH at difficulty 2 and confirms
- **THEN** the party faces the Behemoth with 4 320 hit points, dealing twice its original damage

#### Scenario: The Behemoth lands its jump

- **WHEN** the Behemoth lands after its jump in the arena
- **THEN** the effect of its landing is the underground cave's, and nothing from the tower appears

#### Scenario: A boss too strong for its hit points

- **WHEN** player 1 picks DRAGON ROUGE at difficulty 10
- **THEN** the Dragon rouge has 65 535 hit points, the most a creature can hold, not 80 000

#### Scenario: The menu order

- **WHEN** player 1 opens the arena menu
- **THEN** MECHANT OURS comes first, JEUNE DRAGON second, ENCHANTERESSE third, and DRAGON ROUGE last

### Requirement: The forest is untouched

Bringing creatures of another zone into the arena MUST NOT change that zone: its boss fight, its
story and its other creatures SHALL behave as in the original game, before and after any arena fight.

#### Scenario: The forest after the arena

- **WHEN** the party beats the Méchant ours in the arena, then walks into the forest's boss clearing through the story
- **THEN** the forest's own bear fight happens as in the original game
