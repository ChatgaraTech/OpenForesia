## ADDED Requirements

### Requirement: The installer provides a default gameplay tweaks file

A successful installation SHALL leave a `LegacyPlus.ini` beside the game executable, holding every
tweak at its default (inactive) value with a short French comment per key giving its effect and the
accepted values. When a `LegacyPlus.ini` already exists in the destination folder, the installer
MUST leave it untouched.

#### Scenario: Fresh installation

- **WHEN** the player installs into an empty folder
- **THEN** `LegacyPlus.ini` exists beside the executable and every key in it is at its default value

#### Scenario: Reinstallation keeps the player's tweaks

- **WHEN** the player reinstalls into a folder whose `LegacyPlus.ini` sets `PvMonstres = 2`
- **THEN** after installation that file still sets `PvMonstres = 2`
