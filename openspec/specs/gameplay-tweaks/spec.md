# gameplay-tweaks Specification

## Purpose
Gameplay tweaks are optional departures from the 1998 game's balance and rules, chosen by the
player in a dedicated settings file. This capability fixes the contract every tweak obeys: how it
is configured, and what the game guarantees when no tweak is active.
## Requirements
### Requirement: Tweaks come from a dedicated file the game only reads

The game SHALL read its gameplay tweak settings from `LegacyPlus.ini` in the executable's folder,
once at startup. The game MUST NOT create, modify or rewrite that file. The settings MUST NOT go
through the game's own option menus or `OpenForesia.cfg`. A missing file, a missing key or a
rejected value SHALL fall back to that key's default and MUST NOT prevent the game from starting;
each rejected value SHALL produce one log line naming the key and the value.

#### Scenario: No settings file

- **WHEN** the game starts and `LegacyPlus.ini` does not exist
- **THEN** every tweak is inactive
- **AND** the game starts normally and no `LegacyPlus.ini` is created

#### Scenario: A value is rejected

- **WHEN** `LegacyPlus.ini` sets `PvMonstres = beaucoup`
- **THEN** that key takes its default value and a log line names the key and the rejected value
- **AND** the other keys of the file are applied

### Requirement: No active tweak means the 1998 game

When every tweak is at its default value, the game SHALL behave exactly as it does without any
tweak support: same damage, same hit points, same sequence of random numbers. A tweak MUST NOT
draw random numbers from the game's generator, so that enabling it changes nothing but its own
effect.

#### Scenario: Defaults reproduce the original fight

- **WHEN** `LegacyPlus.ini` holds every key at its default value
- **AND** the same fight is played with the same save and the same inputs
- **THEN** every hit deals the same damage and every monster has the same hit points as without the file

### Requirement: Tweaks stay out of the transcribed code

The transcribed game code SHALL NOT contain any condition on a tweak setting. It MAY only call a
declared seam at a fixed point, whose default is to leave the value unchanged; every such seam
SHALL be marked in place and listed in the tweak register `Documentation/LegacyPlus/legacy.md`.
Removing a tweak's code SHALL NOT require any change to the transcribed code.

#### Scenario: Reading the transcription against the decompilation

- **WHEN** a reader compares a transcribed function carrying a seam with `Blaze.c`
- **THEN** the only difference is the seam call, marked with its `LP-NNN` number
- **AND** no tweak or setting name appears in that function
