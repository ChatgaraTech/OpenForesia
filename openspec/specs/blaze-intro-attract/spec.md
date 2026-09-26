# blaze-intro-attract Specification

## Purpose
Defines the behavior of the reconstructed application's startup intro/attract sequence: how the
attract loop terminates, what happens when its media assets are unavailable, and the guarantee that
startup always reaches engine initialization instead of hanging. Behavior of record is the original
binary as captured in the Ghidra decompilation.
## Requirements
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

### Requirement: Attract loop evaluates every exit condition of the original

The attract loop SHALL evaluate all four of the original's exit conditions on every iteration: the
result of each of its two title-screen waits, and the result of each of its two movie playbacks. An
exit condition SHALL NOT be removed on the grounds that its underlying step is currently degraded or
stubbed.

#### Scenario: Title-screen wait reports completion

- **WHEN** either title-screen wait in the loop body reports a non-zero result
- **THEN** the attract loop exits immediately without running the remaining steps of that iteration

#### Scenario: Movie playback reports completion

- **WHEN** either movie playback in the loop body reports a non-zero result
- **THEN** the attract loop exits immediately without running the remaining steps of that iteration

#### Scenario: All four steps report zero

- **WHEN** every step in an iteration reports zero
- **THEN** the loop begins another iteration

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

### Requirement: Degraded steps preserve control flow

Where a step of the intro sequence cannot be faithfully reconstructed and is reduced to a stub, the
stub SHALL preserve the step's contribution to control flow — its result SHALL still be produced and
consumed by the caller. Reducing a value-returning step to one that returns nothing, thereby
deleting a branch the original evaluates, is a defect regardless of whether the decompiler's own
declaration omits the return value.

#### Scenario: Decompiler omits a return value the call site reads

- **WHEN** the reference decompilation declares a function as returning nothing
- **AND** a call site in the reference decompilation reads that call's result
- **THEN** the reconstruction treats the call site as authoritative and preserves the result
- **AND** records the discrepancy as a traceability note

#### Scenario: Stubbed step inside a loop

- **WHEN** a step whose result is one of a loop's exit conditions is stubbed
- **THEN** the stub still returns a value
- **AND** the loop's exit condition on that value remains present in the reconstruction

### Requirement: Intro sequence hazards are recorded

Any loop on the startup path whose termination depends on a not-yet-reconstructed subsystem SHALL be
documented at the loop, stating what would clear its condition and what happens if the guarding stub
is later changed.

#### Scenario: Loop guarded by a stub

- **WHEN** a loop is currently unreachable only because a stub reports failure
- **THEN** the reconstruction carries a note identifying the stub, the condition that would need to
  be signalled, and the consequence of enabling the stub without it
