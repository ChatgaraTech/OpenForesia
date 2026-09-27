# gameplay-encounters Specification

## Purpose
Lets the player make random encounters more frequent, overlap with an ongoing fight, and bring
several formations at once; every default keeps the original game's encounters.
## Requirements
### Requirement: Encounters come more often

`LegacyPlus.ini` section `[Rencontres]` key `Frequence` SHALL be a multiplier from 0.1 to 10,
default 1. Every delay the game sets before the next random encounter SHALL be divided by that value,
at least one frame.

#### Scenario: Twice as often

- **WHEN** `Frequence = 2` and the party clears an encounter
- **THEN** the next encounter comes after 100 to 150 frames instead of 200 to 300

#### Scenario: Default

- **WHEN** `Frequence` is absent or equals 1
- **THEN** the delays are those of the original game

### Requirement: Encounters may overlap a fight

`[Rencontres]` key `MonstresMax` SHALL be an integer from 0 to 12, default 0. With 0, a random
encounter SHALL come only when no scene entity is alive, as in the original game. With a value N
above 0, a random encounter SHALL also be attempted while scene entities are alive, once the
encounter delay has run out, as long as fewer than N scene entities are alive; the formation SHALL
be chosen and placed exactly as for an original encounter.

#### Scenario: The fight goes on

- **WHEN** `MonstresMax = 12` and three monsters are still alive when the encounter delay runs out
- **THEN** a new formation appears around the party

#### Scenario: The cap is reached

- **WHEN** `MonstresMax = 12` and twelve scene entities are alive
- **THEN** no new encounter is attempted until some of them die

### Requirement: An encounter may bring several formations

`[Rencontres]` key `Formations` SHALL be an integer from 1 to 4, default 1. When a random encounter
brings a formation, the game SHALL attempt that many formations in the same frame, each chosen among
the sub-area's encounter formations and placed on its own, as the original game does for one. A
formation that cannot be placed, or that no longer fits in the scene entity table, SHALL simply not
appear, without error. No additional formation SHALL be started once twelve scene entities are
alive, whatever `MonstresMax` is.

#### Scenario: The rendering limit

- **WHEN** `Formations = 4` and a random encounter brings formations of five monsters
- **THEN** no further formation is started once twelve scene entities are alive

#### Scenario: Two formations

- **WHEN** `Formations = 2` and a random encounter comes
- **THEN** two formations appear, each at its own place around the party

### Requirement: Scripted monsters are untouched

Monsters placed by the sub-area scripts, bosses and scene monsters SHALL appear exactly as in the
original game, whatever the `[Rencontres]` settings.

#### Scenario: A boss room

- **WHEN** `Formations = 3` and the party enters a boss room whose boss is placed by script
- **THEN** the boss appears once, as in the original game
