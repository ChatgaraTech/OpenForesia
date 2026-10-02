## Purpose

Turns the Fairy into the party's support character: when the player enables it, her blessings play on
every party member, and her hexes always take hold and reach several enemies at once, so that she
brings something to the party that no other class does. Disabled, the game is the 1998 game.

## ADDED Requirements

### Requirement: A switch in LegacyPlus.ini enables the Fairy's support role

`LegacyPlus.ini` section `[Magie]` key `SoutienFee` SHALL accept `oui` or `true` to enable the
tweak and `non` or `false` to disable it, case-insensitively, and SHALL default to disabled. Any
other value SHALL be refused with one `[LEGACY++]` line in the log and the default kept. When
disabled, every spell SHALL behave as in the original game.

#### Scenario: Default

- **WHEN** `SoutienFee` is absent, or set to `non` or `false`
- **THEN** every Fairy spell reaches a single target, rolls its saving throw, respects species
  immunity and keeps its original duration, as in the original game

#### Scenario: Enabled

- **WHEN** `SoutienFee = true`
- **THEN** the Fairy's spells behave as described by the other requirements of this capability

#### Scenario: Refused value

- **WHEN** `SoutienFee = peut-etre`
- **THEN** the game logs one `[LEGACY++]` line and the tweak stays disabled

### Requirement: Only the Fairy's own school is affected

The tweak SHALL apply only to spells of the Fairy's school (school 2) cast by a party member whose
class is the Fairy. Spells of the Priest's and the Wizard's schools, spells cast by monsters, and a
school-2 spell cast through an item by a party member who is not the Fairy SHALL be unchanged.

#### Scenario: The Priest casts Renfort

- **WHEN** the tweak is enabled and the Priest casts Renfort
- **THEN** Renfort reaches a single ally, as in the original game

#### Scenario: Another class uses an item that casts Superman

- **WHEN** the tweak is enabled and the Warrior uses an item whose script casts Superman
- **THEN** Superman reaches a single target, as in the original game

### Requirement: The Fairy's blessings reach every party member

When the Fairy casts Vitesse, Matéria feu, Matéria terre, Matéria air, Matéria eau, Coussin d'air or
Superman, the spell SHALL play on its original target and, in full — its animation, its sounds and
its effect —, on every other living party member, whatever their distance to the Fairy. Turbo SHALL
be unchanged: it already reaches the whole party in the original game. A party member who refuses
the effect in the original game (Blindé active) SHALL still refuse it.

#### Scenario: Superman with four members

- **WHEN** the tweak is enabled, the party has four living members and the Fairy casts Superman
- **THEN** all four members get Superman's effect and its rings, including those far from the Fairy

#### Scenario: A dead member

- **WHEN** the tweak is enabled, one member is dead and the Fairy casts Vitesse
- **THEN** the three living members are hastened and the dead member gets nothing

#### Scenario: Turbo

- **WHEN** the tweak is enabled and the Fairy casts Turbo
- **THEN** Turbo hastens the party exactly as in the original game

### Requirement: The Fairy's hexes reach every nearby enemy

"In reach" means a living scene entity whose distance to the Fairy, in three dimensions, is less
than `0x5dc` world units (the reach of the original Zombie spell).

- **Gros sabots** SHALL drop a hoof on its original target and on every other enemy in reach; each
  hoof falls, plays and applies the slow to the enemy under it, as the original hoof does.
- **Ralenti**: when a hoof touches its victim, as in the original game, the effect SHALL be applied
  to that victim and to every other enemy in reach.
- **Dodo, Chant sirènes and Écrase**: each of their bubbles SHALL affect only the enemy it touches.

A projectile that touches no one SHALL apply nothing.

#### Scenario: Ralenti hits one of five goblins

- **WHEN** the tweak is enabled and a Ralenti hoof touches one of five goblins, all within `0x5dc` of
  the Fairy
- **THEN** all five goblins are slowed

#### Scenario: An enemy out of reach

- **WHEN** the tweak is enabled, a Ralenti hoof touches a goblin and another goblin stands farther
  than `0x5dc` from the Fairy
- **THEN** the far goblin is not slowed

#### Scenario: Gros sabots on a group

- **WHEN** the tweak is enabled and the Fairy casts Gros sabots with three goblins in reach
- **THEN** a hoof falls on each of the three goblins, and each is slowed

#### Scenario: One bubble, one enemy

- **WHEN** the tweak is enabled and one bubble of Dodo, Chant sirènes or Écrase touches a goblin while
  two other goblins stand in reach
- **THEN** only the touched goblin is affected

#### Scenario: A missed bubble

- **WHEN** the tweak is enabled and every bubble of a Dodo cast vanishes without touching anyone
- **THEN** no enemy falls asleep

### Requirement: The Fairy's hexes always take hold, bosses included

For the hexes named above, cast by the Fairy, the effect SHALL ignore the saving throw and the
species immunity of every enemy it is applied to, bosses included. The original rules that are not
immunity SHALL still apply: a dead, dying or invalid entity is not affected, and an effect already
running is only lengthened when the new duration is longer. Sleep SHALL still end as soon as the
sleeper takes a hit, as in the original game.

#### Scenario: Dodo on a boss

- **WHEN** the tweak is enabled and a Dodo bubble touches a boss whose species is immune to sleep in
  the original game
- **THEN** the boss falls asleep, and wakes up at the first hit it takes

#### Scenario: Confusion without a saving throw

- **WHEN** the tweak is enabled and a Chant sirènes note touches an enemy whose species resists confusion
- **THEN** that enemy is confused, without any saving throw

### Requirement: Dodo, Chant sirènes and Écrase launch four projectiles

When the Fairy casts Dodo, Chant sirènes or Écrase, the spell SHALL launch four projectiles instead
of one: one ahead of the Fairy, one behind, one to her left and one to her right, each behaving as
the original projectile does from its own heading (including seeking a target). The spell's mana
cost SHALL be unchanged.

#### Scenario: Dodo with enemies on both sides

- **WHEN** the tweak is enabled and the Fairy casts Dodo with an enemy ahead and an enemy behind her
- **THEN** four bubbles leave in the four directions, and the bubbles that find an enemy put to sleep
  every enemy within reach

### Requirement: A single-target version is halved where a group version exists

When a Fairy spell has a group or repeated counterpart in the original game, the duration of the
single-target version SHALL be half of its original duration, rounded down, for every target it is
applied to: Vitesse (counterpart Turbo) and Gros sabots (counterpart Ralenti). Turbo and Ralenti
SHALL keep their original duration.

#### Scenario: Vitesse and Turbo

- **WHEN** the tweak is enabled and the Fairy casts Vitesse, then later Turbo
- **THEN** Vitesse hastens every party member for half its original duration, and Turbo for its full
  original duration

#### Scenario: Gros sabots

- **WHEN** the tweak is enabled and a Gros sabots hoof touches an enemy
- **THEN** every enemy within reach is slowed for half the original duration

### Requirement: The tweak never draws a random number

The tweak's own code SHALL NOT draw from the game's random generator. Skipping a saving throw SHALL
not draw the random numbers that throw would have drawn. Random draws made by the original bodies
of the extra projectiles are part of the game's own behaviour and are allowed.

#### Scenario: Same seed, tweak disabled

- **WHEN** the tweak is disabled
- **THEN** the game draws exactly the same random numbers as without the tweak's code
