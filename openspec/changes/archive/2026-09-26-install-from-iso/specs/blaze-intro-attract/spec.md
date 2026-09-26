## MODIFIED Requirements

### Requirement: Startup always reaches engine initialization

Once the game data check has passed, the startup sequence SHALL reach engine initialization on any
machine, regardless of whether its movie files or its title-screen bitmap can be read. No loop on the
startup path may iterate without at least one reachable exit condition. When the game data check
fails, startup SHALL stop before engine initialization with the message defined by the
`game-data-location` capability, and SHALL NOT wait for a disc.

#### Scenario: No game media present

- **WHEN** the application is started from a folder that does not contain the game data
- **THEN** a single message reports the missing data
- **AND** the process exits without reaching engine initialization
- **AND** the process does not spin or prompt for a disc

#### Scenario: Intro media partially present

- **WHEN** the data check passes but some intro assets cannot be read
- **THEN** each unreadable asset degrades only its own step
- **AND** the sequence still terminates and hands off to engine initialization

### Requirement: Title-screen wait fails closed when its asset is unavailable

The title-screen wait SHALL report a non-zero result — the original's "stop attracting" signal —
when its title bitmap cannot be loaded, and SHALL do so without displaying an error dialog to the
user.

#### Scenario: Title bitmap missing

- **WHEN** the title bitmap cannot be opened or read from the data folder
- **THEN** the wait reports a non-zero result
- **AND** no message box or error prompt is shown
- **AND** the attract loop exits

#### Scenario: Title bitmap load is attempted once

- **WHEN** the title bitmap has already been loaded successfully in this run
- **THEN** subsequent waits reuse it rather than re-reading it from disk
