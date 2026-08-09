## Purpose

Defines how the reconstruction locates the game's data files on the retail CD: detecting the drive,
resolving the data root, distinguishing the two path families the original uses, and guaranteeing
that a path that cannot be resolved fails visibly instead of silently pointing somewhere wrong.

## ADDED Requirements

### Requirement: Assets resolve against the data root, not the drive root

The system SHALL resolve data-root-relative assets against the game's data root — the drive letter
followed by the language directory — and not against the bare drive root. The data root's literal
value SHALL come from the binary's initialised data rather than being assumed.

#### Scenario: Retail CD mounted

- **WHEN** the CD is mounted and the drive scan identifies its letter
- **THEN** the data root is that letter followed by the language directory
- **AND** an asset named `DATA/TITLE640.BMP` resolves to a path under that directory
- **AND** the file is found and loaded

#### Scenario: Drive letter substitution

- **WHEN** the data root template carries a placeholder in its first character
- **THEN** only that first character is replaced with the detected drive letter
- **AND** the remainder of the template is preserved exactly, including its separators and case

### Requirement: Root-relative assets bypass the data root

Assets that live at the CD root rather than under the data root SHALL be built as drive-letter-patched
absolute paths, not by prefixing the data root. The streaming sound bank and the music tracks are
in this family.

#### Scenario: Sound bank path

- **WHEN** the base sound bank is requested
- **THEN** its path is the detected drive letter followed by the root-relative remainder of its own
  template
- **AND** the data root's language directory does not appear in the result

#### Scenario: Music track path

- **WHEN** a music track is requested by index
- **THEN** its path is built the same way, from that track's own template
- **AND** the result addresses the CD root

### Requirement: Unresolvable assets fail visibly

An asset that cannot be opened SHALL surface that fact through the caller's existing failure path.
The system SHALL NOT substitute a different location, silently skip the load, or report success with
no data.

#### Scenario: Asset missing from a correctly resolved path

- **WHEN** a path resolves correctly but the file is not present
- **THEN** the load reports failure to its caller
- **AND** the caller's documented degraded behaviour applies

#### Scenario: No CD present

- **WHEN** no drive is identified as holding the game media
- **THEN** asset loads fail rather than resolving against an empty or partial root
- **AND** startup still reaches engine initialization

### Requirement: Recovered path literals are preserved exactly

Path literals recovered from the binary SHALL be used as they appear there, including separator
characters and letter case, rather than normalised to a preferred convention. Where a literal cannot
be used verbatim on a modern system, the deviation SHALL be explicit at the point of use and the
original SHALL be attempted first.

#### Scenario: Mixed separators

- **WHEN** a recovered literal mixes forward and backward slashes
- **THEN** it is used unchanged

#### Scenario: Filesystem-version suffix

- **WHEN** a recovered literal carries an ISO-9660 version suffix that the mounted filesystem does
  not expose
- **THEN** the literal is attempted first
- **AND** on failure a suffix-free form is attempted
- **AND** the fallback is documented at the call site as a deliberate deviation

### Requirement: The options file is located by its real name

The system SHALL read and write its options file using the extension the binary actually uses, so
that settings written by the original game are read back rather than silently defaulted.

#### Scenario: Existing options file

- **WHEN** an options file written by the original game sits next to the executable
- **THEN** its `[Options]`, `[D3DDriver]` and `[Controls]` sections are read
- **AND** the values are applied rather than replaced by defaults
