## MODIFIED Requirements

### Requirement: The installer provides a default shop file

A successful installation SHALL leave, in a `LegacyPlus` folder beside the game executable, the four
merchant shop files — usable items, accessories, armour, weapons — each with its shop disabled and
its default catalogue. A shop file that already exists in that folder MUST be left untouched. The
installer SHALL NOT deposit `Boutique.ini` any more.

#### Scenario: Fresh installation

- **WHEN** the player installs into an empty folder
- **THEN** the `LegacyPlus` folder holds the four shop files, each with `Actif = non` and its catalogue, and no `Boutique.ini` is deposited

#### Scenario: Reinstallation keeps the player's shop

- **WHEN** the player reinstalls into a folder whose weapons shop file sets `Actif = oui` and a retouched price
- **THEN** after installation that file is unchanged
