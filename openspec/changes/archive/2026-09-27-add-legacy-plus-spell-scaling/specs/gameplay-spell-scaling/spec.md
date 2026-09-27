## Purpose

Makes the damage of the party's spells grow with the caster's magic power, so that low-level spells
stay useful late in the game; a setting of 0 is the original game.

## ADDED Requirements

### Requirement: A party spell's flat amount grows with the caster's magic power

`LegacyPlus.ini` section `[Magie]` key `ScalingMagique` SHALL be a multiplier from 0 to 5, default
0, written as an integer or a decimal with a point or a comma and rounded to the nearest hundredth.
When a spell cast by a party member hits and the original game adds the spell sheet's flat amount
to the damage, the amount added SHALL be `amount × (1 + ScalingMagique × MAG / 100)`, rounded down,
where MAG is the caster's magic power as the game computes it for that hit, equipment included.
Every other part of the damage formula SHALL be unchanged, including the rule that skips the amount
when the victim already has the spell's element.

#### Scenario: A basic spell at a high level

- **WHEN** `ScalingMagique = 1` and a party member with MAG 130 hits with a spell whose amount is 10
- **THEN** the amount added to the damage is 23

#### Scenario: Default

- **WHEN** `ScalingMagique` is absent or equals 0
- **THEN** every spell adds the amount of its sheet, as in the original game

#### Scenario: The element rule still applies

- **WHEN** `ScalingMagique = 1` and a fire spell hits a monster whose dominant element is fire
- **THEN** no amount is added, as in the original game

### Requirement: Monster spells are not scaled

A spell cast by a scene entity SHALL add the amount of its sheet unchanged, whatever
`ScalingMagique` is set to.

#### Scenario: A monster casts a spell

- **WHEN** `ScalingMagique = 2` and a monster's spell hits a party member
- **THEN** the amount added is the amount of the spell sheet

### Requirement: Scaled damage stays within the game's range

The scaled amount SHALL be capped so that the damage of the hit never exceeds 32767. The game stores
damage in a signed 16-bit field, and a larger value would be read as negative.

#### Scenario: Extreme setting

- **WHEN** `ScalingMagique = 5` and a party member with MAG 999 hits with a spell whose amount is 200
- **THEN** the hit deals positive damage, at most 32767
