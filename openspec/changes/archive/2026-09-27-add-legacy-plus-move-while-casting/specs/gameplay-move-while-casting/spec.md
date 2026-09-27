## Purpose

Lets a party member keep moving while casting a spell, at a fraction of the walking speed the player
chooses; 0 keeps the original game, where a caster cannot move.

## ADDED Requirements

### Requirement: A caster moves at a fraction of the walking speed

`LegacyPlus.ini` section `[Magie]` key `DeplacementEnIncantation` SHALL be a multiplier from 0 to 1,
default 0, written as an integer or a decimal with a point or a comma. While a party member is
casting a spell — from the start of the button combination until the spell leaves — the ground
speed the directional input gives SHALL be multiplied by that value, instead of being cancelled as
in the original game. The button combination, the spell cast and its cost SHALL be unchanged.

#### Scenario: Half speed

- **WHEN** `DeplacementEnIncantation = 0.5` and a player holds a direction while entering a spell combination
- **THEN** the caster moves in that direction at half the speed walking would give, and the spell leaves as usual

#### Scenario: Default

- **WHEN** `DeplacementEnIncantation` is absent or equals 0
- **THEN** a caster does not move while casting, as in the original game

#### Scenario: No direction held

- **WHEN** `DeplacementEnIncantation = 0.5` and a player casts without holding a direction
- **THEN** the caster stays in place
