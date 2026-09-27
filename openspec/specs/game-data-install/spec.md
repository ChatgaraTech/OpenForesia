# game-data-install Specification

## Purpose
Installs OpenForesia from the player's own disc image: recognises the edition, extracts only the game
data the reconstruction reads, and lays out a self-contained, playable game folder — without the
project ever distributing the original data.
## Requirements
### Requirement: The installer recognises a supported disc image before extracting

The installer SHALL accept an ISO 9660 image with 2048-byte sectors and SHALL identify its edition
from the volume label and the checksum of the main game archive. It MUST refuse an image that matches
no known edition, before writing anything to disk, with a message that names what was expected.

#### Scenario: Reference edition

- **WHEN** the player selects the French retail image (volume label `BLAZE_BLAZE_F`, `FRENCH\BLAZE.ALL`
  matching the recorded checksum)
- **THEN** the installer reports the edition as recognised and allows installation to proceed

#### Scenario: Unknown or damaged image

- **WHEN** the player selects an image whose label or archive checksum matches no known edition
- **THEN** the installer refuses it and nothing is written to the destination folder

#### Scenario: Unsupported image format

- **WHEN** the selected file is not an ISO 9660 image with 2048-byte sectors (for example a raw
  `.bin` or an unrelated file)
- **THEN** the installer refuses it with a message stating that an `.iso` image is required

### Requirement: Only the data the game reads is extracted, at its real size

The installer SHALL copy exactly the files listed for the recognised edition — the game data under
`FRENCH\`, `SND\` and `MUSIC\` — and SHALL NOT copy the copy-protection decoy `CD.IDX`, the duplicate
`FRENCH2\` tree, the 1998 setup, launcher and DirectX files, or any executable or DLL from the disc.
A file whose directory entry declares a size larger than its content (the padded movies) MUST be
written at its real content size.

#### Scenario: Padded movie

- **WHEN** `FRENCH\OPENING.AVI` is extracted, whose directory entry declares 463 194 112 bytes while
  its RIFF header declares 28 101 316
- **THEN** the installed file is 28 101 316 bytes long and plays identically

#### Scenario: Decoy and duplicates are skipped

- **WHEN** installation completes
- **THEN** the destination contains no `CD.IDX`, no `FRENCH2\` folder and no file from `DIRECTX\`,
  `DXMEDIA\`, `SYSTEM\` or the root `DATA\` folder

### Requirement: Every installed file is verified

After extraction the installer SHALL verify each installed data file against the edition's recorded
size and checksum, and MUST report the installation as failed if any file does not match.

#### Scenario: Corrupted extraction

- **WHEN** an extracted file's checksum differs from the recorded one
- **THEN** the installer reports the failure, naming the file, and does not report success

### Requirement: The destination folder is safe for a game that writes next to itself

The installer SHALL propose a per-user folder that needs no administrator rights by default. It MUST
warn when the chosen folder is under a protected system location where the game could not write its
saves, MUST refuse a folder path longer than 200 characters, and MUST check free space before
extracting.

#### Scenario: Default destination

- **WHEN** the installer opens the destination step
- **THEN** it proposes `%LOCALAPPDATA%\Programs\OpenForesia`

#### Scenario: Protected location

- **WHEN** the player chooses a folder under `Program Files`
- **THEN** the installer warns that saves could not be written there and asks for confirmation

#### Scenario: Path too long

- **WHEN** the chosen folder path exceeds 200 characters
- **THEN** the installer refuses it and explains the limit

#### Scenario: Not enough space

- **WHEN** the destination drive has less free space than the installation needs
- **THEN** the installer refuses to start extracting and states how much space is required

### Requirement: The installed folder is a complete, playable game

A successful installation SHALL leave in the destination folder the game executable (published so
that no separate runtime installation is needed), the extracted data, and an empty `save\` folder.
The installer SHALL create a desktop shortcut and a Start-menu entry that launch the game, and a
Start-menu entry that uninstalls it.

#### Scenario: First launch after installation

- **WHEN** the player launches the game from the desktop shortcut right after installing
- **THEN** the game starts without asking for a disc or a runtime, and `save\` exists beside the
  executable

### Requirement: The game can be uninstalled cleanly

The game SHALL appear in the Windows list of installed applications for the current user, and
uninstalling SHALL remove the installed files, the shortcuts and that entry. Before removal it MUST
ask whether to keep the `save\` folder.

#### Scenario: Keep saves

- **WHEN** the player uninstalls and chooses to keep saves
- **THEN** everything is removed except the `save\` folder and its contents

#### Scenario: Remove everything

- **WHEN** the player uninstalls and chooses not to keep saves
- **THEN** the installation folder, the shortcuts and the installed-applications entry are all gone

### Requirement: A failed or cancelled installation leaves nothing behind

If extraction fails or the player cancels, the installer SHALL remove what it wrote in this run and
MUST NOT create shortcuts or the installed-applications entry.

#### Scenario: Cancel during extraction

- **WHEN** the player cancels while files are being extracted
- **THEN** the files written so far are removed and no shortcut exists

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
