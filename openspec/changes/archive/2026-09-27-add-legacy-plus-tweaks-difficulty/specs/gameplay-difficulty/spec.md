## Purpose

Lets the player make monsters tougher or weaker than in the 1998 game, by multiplying their hit
points and the damage they deal; a multiplier of 1 is the original game.

## ADDED Requirements

### Requirement: Monster hit points are multiplied at birth

`LegacyPlus.ini` section `[Difficulte]` key `PvMonstres` SHALL be a multiplier from 0.1 to 10,
default 1, written as an integer or a decimal with a point or a comma (`2`, `1.5`, `1,5`), and
rounded to the nearest hundredth. When a scene entity's statistics are created, its maximum and
current hit points SHALL both be the original value times `PvMonstres`, at least 1, and at most
32767. Healing an entity back to full restores it to that multiplied maximum.

#### Scenario: Doubled hit points

- **WHEN** `PvMonstres = 2` and a monster whose sheet gives 150 HP appears
- **THEN** it appears with 300 HP out of 300

#### Scenario: Decimal multiplier

- **WHEN** `PvMonstres = 1,5` and a monster whose sheet gives 150 HP appears
- **THEN** it appears with 225 HP out of 225

#### Scenario: The cap protects the signed field

- **WHEN** `PvMonstres = 10` and a monster whose sheet gives 5000 HP appears
- **THEN** it appears with 32767 HP, not with a value the game would read as negative

#### Scenario: Default

- **WHEN** `PvMonstres` is absent or equals 1
- **THEN** every monster appears with the hit points of its sheet

### Requirement: Damage dealt to the party is multiplied

`[Difficulte]` key `DegatsMonstres` SHALL be a multiplier from 0.1 to 10, default 1, written as for
`PvMonstres`. Every melee or spell hit whose victim is a party member SHALL deal the original damage
times `DegatsMonstres`, at least 1 (the original game never deals zero), and at most 32767. Hits on
monsters, and damage expressed as a percentage of hit points (poison, traps, drains), SHALL be
unchanged.

#### Scenario: Monsters hit harder

- **WHEN** `DegatsMonstres = 1.5` and a monster's hit would deal 20 damage to a party member
- **THEN** the party member loses 30 HP and the damage number shown is 30

#### Scenario: The party's own hits are untouched

- **WHEN** `DegatsMonstres = 1.5` and a party member's hit would deal 20 damage to a monster
- **THEN** the monster loses 20 HP

#### Scenario: Never zero

- **WHEN** `DegatsMonstres = 0.1` and a monster's hit would deal 3 damage to a party member
- **THEN** the party member loses 1 HP
