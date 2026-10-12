# gameplay-custom-combos Specification

## Purpose

Lets each player choose, for each of a character's spells, a short button sequence that casts it
through the game's combination mode, so that changing spells no longer requires a menu; the
sequences belong to the character and travel with their save. Disabled, the game is the 1998 game.

## Requirements
### Requirement: A switch in LegacyPlus.ini enables custom combinations

`LegacyPlus.ini` section `[Magie]` key `CombinaisonsLibres` SHALL accept `oui` to enable the tweak
and `non` to disable it, case-insensitively, and SHALL default to disabled. Any other value SHALL be
refused with one `[LEGACY++]` line in the log and the default kept. When disabled, combination mode
SHALL recognise the spell sheets' original combinations, the magic tab SHALL draw them, and SELECT
SHALL do nothing in the magic tab, as in the original game.

#### Scenario: Default

- **WHEN** `CombinaisonsLibres` is absent or set to `non`
- **THEN** holding SELECT and typing a spell's original combination casts it, as in the original game

#### Scenario: Refused value

- **WHEN** `CombinaisonsLibres = peut-etre`
- **THEN** the game logs one `[LEGACY++]` line and the tweak stays disabled

### Requirement: Each character has one shortcut per spell of their school, empty by default

When the tweak is enabled, each character SHALL have one shortcut per spell of their own school, of
zero to eight button presses, where a press is any non-empty combination of △ ○ ✕ □ held together.
A character who never defined a shortcut SHALL have every shortcut empty. Two characters of the same
school SHALL have independent shortcuts.

#### Scenario: A new or old save

- **WHEN** the tweak is enabled and a character from a save made before this change joins the party
- **THEN** all of the character's shortcuts are empty

#### Scenario: Sorcerer and Elf

- **WHEN** the tweak is enabled, the Sorcerer sets ✕ on Boule de feu and the Elf sets ✕ ✕ on
  Boule de feu
- **THEN** each one casts Boule de feu with their own shortcut, and not with the other's

### Requirement: Combination mode recognises only the caster's shortcuts

When the tweak is enabled and a character releases SELECT after typing a sequence in combination
mode, the spell cast SHALL be the spell of the character's school whose shortcut, for that
character, equals the typed sequence exactly (same length, same presses in order), if the character
has learned it and has the mana, as for an original combination. The spell sheets' original
combinations SHALL not be recognised. A sequence that matches no shortcut SHALL cast nothing, as an
unrecognised combination does in the original game. The memorised spell SHALL be unchanged.

#### Scenario: A one-press shortcut

- **WHEN** the tweak is enabled, the character's shortcut for Boule de feu is ✕, and the player
  holds SELECT, presses ✕ and releases SELECT
- **THEN** Boule de feu is cast, and the memorised spell is still the one it was

#### Scenario: An original combination no longer works

- **WHEN** the tweak is enabled, the character's shortcut for Balle de feu is empty, and the player
  types Balle de feu's original combination in combination mode
- **THEN** no spell is cast

### Requirement: Shortcuts are set from the magic tab of the START menu

When the tweak is enabled, in the magic tab of the START menu, with the cursor on a spell the
character has learned, pressing SELECT SHALL start entering a shortcut for that spell. While
entering, each new press of △ ○ ✕ □ SHALL be appended (eight at most; further presses are ignored),
SHALL be drawn at once where the spell's combination is drawn, and SHALL NOT act as a menu button
(no validation, no return, no memorisation). Pressing SELECT again SHALL end the entry: the typed
sequence becomes the spell's shortcut, and an empty sequence makes the shortcut empty. SELECT on a
spell the character has not learned SHALL do nothing.

#### Scenario: Setting a shortcut

- **WHEN** the tweak is enabled, the cursor is on Boule de feu, and the player presses SELECT, ✕, ✕,
  ✕, SELECT
- **THEN** Boule de feu's shortcut is ✕ ✕ ✕, and the magic tab draws ✕ ✕ ✕ for it

#### Scenario: Clearing a shortcut

- **WHEN** the tweak is enabled, Boule de feu's shortcut is ✕, and the player presses SELECT then
  SELECT on it
- **THEN** Boule de feu's shortcut is empty, and nothing is drawn for it

#### Scenario: Face buttons during entry

- **WHEN** the player is entering a shortcut and presses the button that validates in menus
- **THEN** it is appended to the shortcut, and the menu does not validate or memorise anything

### Requirement: A shortcut already used by another spell is taken from it

When a shortcut is validated, any other spell of the same character whose shortcut equals the new
sequence SHALL get an empty shortcut. Shortcuts of other characters SHALL not change.

#### Scenario: Conflict

- **WHEN** the tweak is enabled, Balle de feu's shortcut is ✕, and the player sets ✕ on Pieds
  mouillés for the same character
- **THEN** Pieds mouillés' shortcut is ✕ and Balle de feu's shortcut is empty

### Requirement: Shortcuts are kept in the character's save

A character's shortcuts SHALL be stored in their character record, in bytes the original game never
reads or writes, so that they are written with the character at every save, read back when the
character is loaded, and follow the character in every mode (solo, hosted multiplayer). A save
written with shortcuts SHALL stay readable by the game with the tweak disabled, which ignores them.
Shortcuts set after the last save SHALL be lost if the game is left without saving, like any other
change to the character.

#### Scenario: Save and reload

- **WHEN** the tweak is enabled, the player sets ✕ on Boule de feu, saves, quits and loads the save
- **THEN** Boule de feu's shortcut is ✕

#### Scenario: Tweak disabled on a save with shortcuts

- **WHEN** a save carrying shortcuts is loaded with `CombinaisonsLibres = non`
- **THEN** the game behaves as the original game, and the shortcuts are still there when the tweak
  is enabled again
