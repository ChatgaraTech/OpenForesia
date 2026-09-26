# game-data-location Specification

## Purpose
Defines where the running game finds its data and writes its saves, and what it does when the data
is missing — with no dependency on an optical drive, a drive letter or a copy-protection check.
## Requirements
### Requirement: Game data is read from a folder, never from a disc

The game SHALL read all of its data (archive, system file, interface pictures and texts, movies,
sound banks and music) from a data folder. It MUST NOT scan drives, test drive types, require a disc
to be present, or read the `CD.IDX` decoy.

#### Scenario: No optical drive at all

- **WHEN** the game is launched on a machine with no optical drive and no mounted image, from a
  folder containing the installed data
- **THEN** the game starts and plays normally

#### Scenario: Disc image mounted but no installed data

- **WHEN** a disc image is mounted as a drive but the data folder does not contain the game data
- **THEN** the game does not use the mounted drive and reports the missing data

### Requirement: The data folder defaults to the executable's folder

The data folder SHALL be the folder containing the game executable. The game SHALL make it its
working directory at startup, so that the data, the `save\` folder and the settings file always
resolve inside it, whatever folder the game was launched from.

#### Scenario: Launched from another working directory

- **WHEN** the game is started through a shortcut whose working directory is a different folder
- **THEN** data is read from, and saves are written to, the executable's folder

#### Scenario: Settings follow the executable

- **WHEN** the game is started from another working directory and its settings are changed in game
- **THEN** the settings file read and written is the one beside the executable

### Requirement: Missing or incomplete data stops startup with a clear message

Before creating its window, the game SHALL check that every file of the expected data list exists
with its expected size. If any check fails it MUST show a single message in French stating how many
files are missing or wrong and naming the first one, suggesting reinstalling, and then exit with a
non-zero code. It MUST NOT loop, retry or prompt for a disc.

#### Scenario: Data folder empty

- **WHEN** the game is launched from a folder that contains only the executable
- **THEN** one message box says the game data was not found and the process exits

#### Scenario: One file truncated

- **WHEN** one music file is shorter than its expected size
- **THEN** the message names that file and the game does not start

### Requirement: Installation paths with non-ASCII characters or spaces work

The game SHALL start and read its data from a folder whose path contains spaces or non-ASCII
characters (for example `D:\Jeux\Rétro & Co\OpenForesia`), independently of the system ANSI code page.

#### Scenario: Accented folder

- **WHEN** the game is installed under `C:\Users\Joueur\Téléchargements\OpenForesia` and launched
- **THEN** data loads and saves are written normally

### Requirement: Text display does not depend on a copy-protection value

Text rendering SHALL NOT depend on any value derived from the disc; every text previously rendered
correctly with the original disc mounted MUST render identically without it.

#### Scenario: Menu texts without a disc

- **WHEN** the main menu and the options page are displayed with no disc present
- **THEN** their texts are the same as with the original disc mounted
