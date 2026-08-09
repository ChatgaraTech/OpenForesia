## MODIFIED Requirements

### Requirement: Title-screen wait fails closed when its asset is unavailable

The title-screen wait SHALL report a non-zero result — the original's "stop attracting" signal —
when its title bitmap cannot be loaded, and SHALL do so without displaying an error dialog to the
user.

When the bitmap **does** load, the wait cannot complete without the input handling the original
performs. In that state the attract loop has no reachable exit, so the system SHALL fail with a
diagnostic naming the missing capability rather than looping without progress. Spinning without a
terminating condition is never an acceptable outcome, whether the asset is present or absent.

#### Scenario: Title bitmap missing

- **WHEN** the title bitmap cannot be opened or read from the resolved data root
- **THEN** the wait reports a non-zero result
- **AND** no message box or error prompt is shown
- **AND** the attract loop exits
- **AND** startup proceeds to engine initialization

#### Scenario: Title bitmap present but input handling unavailable

- **WHEN** the title bitmap loads successfully
- **AND** the wait's timeout and input handling are not yet reconstructed
- **THEN** the system reports a diagnostic identifying the missing capability
- **AND** it does not spin without a terminating condition
- **AND** the diagnostic names the other exits it checked, so the reason no exit is reachable is
  visible without re-deriving it

#### Scenario: Title bitmap load is attempted once

- **WHEN** the title bitmap has already been loaded successfully in this run
- **THEN** subsequent waits reuse it rather than re-reading it from disk
