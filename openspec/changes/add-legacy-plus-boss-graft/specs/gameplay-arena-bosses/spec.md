## Purpose

Lets a boss from another zone of the game fight in the hub arena exactly as it does at home, starting
with the forest's boss fight — the Méchant ours and its two Grizzlys — so that later modes can chain
bosses from every zone.

## ADDED Requirements

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

#### Scenario: The breath, first lot

- **WHEN** the Méchant ours uses its breath before the breath is grafted
- **THEN** it makes the gesture and its sounds, and no effect is born

### Requirement: Grafted creatures drop nothing

A creature brought into the arena from another zone SHALL drop neither item nor coins when it dies;
its experience crystals SHALL drop as usual. The arena's reward is the purse paid at victory.

#### Scenario: The Méchant ours dies

- **WHEN** a grafted creature dies in the arena
- **THEN** no item and no coins appear, and its experience crystals do

### Requirement: The MECHANT OURS mode

The arena menu SHALL list a mode named MECHANT OURS next to ENCHANTERESSE. Choosing it SHALL pit the
party against the forest's boss fight — the Méchant ours and its two Grizzlys, in one wave — at the
chosen difficulty, with the arena's usual ending and defeat.

#### Scenario: Méchant ours at difficulty 3

- **WHEN** player 1 picks MECHANT OURS at difficulty 3 and confirms
- **THEN** the party faces the Méchant ours with 900 hit points and two Grizzlys with 240 each, all
  dealing three times their original damage

#### Scenario: Victory over the Méchant ours

- **WHEN** the Méchant ours and both Grizzlys are dead
- **THEN** the tower's ending cinematic plays, the portal appears and the party is paid 40 coins times the difficulty

### Requirement: The forest is untouched

Bringing forest creatures into the arena MUST NOT change the forest: its bear fight, its story and
its other creatures SHALL behave as in the original game, before and after any arena fight.

#### Scenario: The forest after the arena

- **WHEN** the party beats the Méchant ours in the arena, then walks into the forest's boss clearing through the story
- **THEN** the forest's own bear fight happens as in the original game

### Requirement: The BEHEMOTH mode

The arena menu SHALL list a mode named BEHEMOTH after MECHANT OURS. Choosing it SHALL pit the party
against the Behemoth of the underground cave (zone 3, sub-area 9), alone, in one wave, at the chosen
difficulty, with the arena's usual ending and defeat. It SHALL cast its spells as at home; the effect
its jump's landing spawns in its own sub-area SHALL be left out until it is grafted.

#### Scenario: Behemoth at difficulty 2

- **WHEN** player 1 picks BEHEMOTH at difficulty 2 and confirms
- **THEN** the party faces the Behemoth with 4 320 hit points, dealing twice its original damage

#### Scenario: The Behemoth lands its jump

- **WHEN** the Behemoth lands after its jump in the arena
- **THEN** no effect is born, and nothing from the tower appears
