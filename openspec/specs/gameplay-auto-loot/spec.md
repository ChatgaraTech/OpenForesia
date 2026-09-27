# gameplay-auto-loot Specification

## Purpose
Collects the coins and experience crystals that defeated monsters drop without the players having
to walk over them, leaving chests to be opened by hand; `non` keeps the original pick-up by contact.
## Requirements
### Requirement: Coins and crystals are collected once they have landed

`LegacyPlus.ini` section `[Butin]` key `RamassageAuto` SHALL accept `non` (default) or `oui`. With
`oui`, a coin or an experience crystal dropped by a monster SHALL be collected once it has stopped
bouncing, without any party member touching it, and the reward SHALL be exactly what touching it
gives in the original game: a crystal's experience shared among the members still standing, coins
added to the collecting member's purse, and the pick-up sound.

#### Scenario: A monster dies far from the party

- **WHEN** `RamassageAuto = oui` and a monster killed by an arrow drops a crystal and two coins far from every player
- **THEN** once they have landed, the crystal's experience is shared among the standing members and the coins are added to a purse, without anyone walking to them

#### Scenario: Default

- **WHEN** `RamassageAuto` is absent or equals `non`
- **THEN** coins and crystals are collected only by walking over them, as in the original game

### Requirement: Coins are shared in turn among the standing members

With `RamassageAuto = oui`, coins SHALL be collected in turn by the members currently standing, one
coin each in party order, whatever the party size from one to four. A knocked-out member SHALL be
skipped and SHALL rejoin the rotation once revived. A coin that the member whose turn it is has not
collected by the time it starts to fade SHALL be collected by any standing member. A crystal MAY be
collected by any standing member, its experience being shared by the original game.

#### Scenario: Four players

- **WHEN** `RamassageAuto = oui` with four standing members and eight coins land
- **THEN** each member's purse receives two coins

#### Scenario: A member is knocked out

- **WHEN** `RamassageAuto = oui` with three members, one of them knocked out, and four coins land
- **THEN** the two standing members receive two coins each

### Requirement: Chests are never collected automatically

A chest dropped by a monster SHALL NOT be collected, opened or moved by the tweak; it SHALL behave
exactly as in the original game.

#### Scenario: A monster drops a chest

- **WHEN** `RamassageAuto = oui` and a monster drops a chest
- **THEN** the chest stays where it fell until a player opens it
