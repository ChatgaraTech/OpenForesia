## ADDED Requirements

### Requirement: The installer provides a default display settings file

A successful installation SHALL leave an `Affichage.ini` beside the game executable, holding every
display setting at its default value with a short French comment per key listing the accepted
values. When an `Affichage.ini` already exists in the destination folder, the installer MUST leave
it untouched.

#### Scenario: Fresh installation

- **WHEN** the player installs into an empty folder
- **THEN** `Affichage.ini` exists beside the executable and lists each setting with its default value

#### Scenario: Reinstallation keeps the player's settings

- **WHEN** the player reinstalls into a folder whose `Affichage.ini` sets `ResolutionInterne = 2`
- **THEN** after installation that file still sets `ResolutionInterne = 2`
