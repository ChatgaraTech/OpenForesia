## ADDED Requirements

### Requirement: The installer provides a default shop file

A successful installation SHALL leave a `Boutique.ini` beside the game executable, with the shop
disabled and the default catalogue. When a `Boutique.ini` already exists in the destination folder,
the installer MUST leave it untouched.

#### Scenario: Fresh installation

- **WHEN** the player installs into an empty folder
- **THEN** `Boutique.ini` exists beside the executable, with `Actif = non` and the catalogue

#### Scenario: Reinstallation keeps the player's shop

- **WHEN** the player reinstalls into a folder whose `Boutique.ini` sets `Actif = oui` and a retouched price
- **THEN** after installation that file is unchanged
