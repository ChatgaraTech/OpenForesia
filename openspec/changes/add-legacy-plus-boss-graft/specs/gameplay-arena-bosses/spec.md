## Purpose

Lets a boss from another zone of the game fight in the hub arena exactly as it does at home, starting
with the Grizzly of the forest, so that later modes can chain bosses from every zone.

## ADDED Requirements

### Requirement: A boss from another zone fights in the arena as at home

A boss brought into the arena from another zone SHALL look, move, attack, sound and produce its
effects as it does in its own zone, and SHALL be defeated in the same way. None of its sounds or
effects SHALL be replaced by those of the arena's zone; a sound or effect that cannot be reproduced
SHALL be left out rather than substituted.

#### Scenario: The Grizzly's grab

- **WHEN** the Grizzly grabs a party member in the arena
- **THEN** the grab plays the same sound and the same visual effect as in the forest

#### Scenario: Nothing from the tower leaks in

- **WHEN** the Grizzly fights in the arena
- **THEN** no creature, sound or effect belonging to the tower appears as a result of its actions

### Requirement: The GRIZZLY mode

The arena menu SHALL list a mode named GRIZZLY. Choosing it SHALL pit the party against the
forest's Grizzly boss alone, at the chosen difficulty, with the arena's usual ending and defeat.

#### Scenario: Grizzly at difficulty 3

- **WHEN** player 1 picks GRIZZLY at difficulty 3 and confirms
- **THEN** the party faces the Grizzly boss with 240 hit points, dealing three times its original damage

#### Scenario: Victory over the Grizzly

- **WHEN** the Grizzly dies in the arena
- **THEN** the tower's ending cinematic plays, and the portal and two chests appear at the centre

### Requirement: The forest is untouched

Bringing the Grizzly into the arena MUST NOT change the forest: its Grizzly, its story and its other
creatures SHALL behave as in the original game, before and after any arena fight.

#### Scenario: The forest after the arena

- **WHEN** the party beats the Grizzly in the arena, then walks into the forest's boss clearing through the story
- **THEN** the forest's own Grizzly fight happens as in the original game
