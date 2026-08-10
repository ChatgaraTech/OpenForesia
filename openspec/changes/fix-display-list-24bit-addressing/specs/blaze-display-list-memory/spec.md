## Purpose

Defines where the shared PlayStation display-list arena lives and what the reconstruction
guarantees about it, so that the ordering tables and primitives the game builds every
frame remain reachable through the 24-bit links the original hardware model uses.

## ADDED Requirements

### Requirement: Display-list memory SHALL be addressable on 24 bits

The original stores every display-list link in the low 24 bits of a 32-bit word and
reserves the top byte for the primitive's length. The reconstruction SHALL therefore
guarantee that the ordering tables and the primitive buffers of every frame resolve to
addresses strictly below `0x01000000`.

#### Scenario: Frame memory is reachable through a truncated link

- **WHEN** a frame's ordering table or primitive buffer is placed in memory
- **THEN** masking its address to its low 24 bits SHALL yield the same address

#### Scenario: A link's length byte stays free

- **WHEN** a primitive is linked into the ordering table
- **THEN** the top byte of the link word SHALL carry only the primitive length, with no
  address bits displaced into it

### Requirement: The arena SHALL be secured before any derived pointer is taken

The graphics library derives working pointers from the arena base during its device
initialisation. The reconstruction SHALL fix the arena's location before that
initialisation runs, so that the game and the library never work from two different
arenas.

#### Scenario: Arena is fixed before initialisation

- **WHEN** the graphics library is loaded
- **THEN** the arena location SHALL be established before any of the library's
  initialisation entry points is called

#### Scenario: Game and library agree on the arena

- **WHEN** the library derives a working pointer from the arena base
- **THEN** that pointer SHALL fall inside the same arena the game writes its frames into

### Requirement: The arena SHALL prefer the original layout

The reconstruction SHALL place the arena at the address the 1998 process used, so that
addresses quoted in the decompiled sources correspond to runtime addresses without an
offset. When that address is unavailable, the reconstruction SHALL still place the arena
somewhere satisfying the 24-bit constraint, and SHALL report the resulting offset from the
original layout.

#### Scenario: Original address is available

- **WHEN** the original arena address is free at startup
- **THEN** the arena SHALL be placed there, and no offset SHALL be reported

#### Scenario: Original address is taken

- **WHEN** the original arena address is unavailable
- **THEN** the arena SHALL be placed at another address satisfying the 24-bit constraint
- **AND** the offset between the runtime layout and the original layout SHALL be reported
  so that decompiled addresses remain translatable

### Requirement: An unsatisfiable layout SHALL fail visibly

A layout that cannot satisfy the 24-bit constraint makes every subsequent frame fault in
native code, where the failure carries no explanation. The reconstruction SHALL detect
this before drawing and SHALL stop with a description naming the constraint, rather than
proceeding.

#### Scenario: No suitable region exists

- **WHEN** no region satisfying the 24-bit constraint can be obtained at startup
- **THEN** the reconstruction SHALL stop with an error naming the 24-bit display-list
  constraint and the size required
- **AND** it SHALL NOT reach the first frame

#### Scenario: Failure is not a native fault

- **WHEN** the arena cannot be secured
- **THEN** the failure SHALL be a descriptive managed error, not an access violation
