## Purpose

Export the game's maps (sub-areas) — their textured display geometry, their collision geometry and
the placement of what lives in them — from the player's own installed data into open formats, as a
visual and spatial base for a remake.

## ADDED Requirements

### Requirement: Export a sub-area from the player's installed data

The tools SHALL provide an `exporter-carte` command taking the installed game folder, a zone, a
sub-area (or every sub-area of a zone, or every zone) and an output folder. It SHALL read only the
player's own game files, offline, without launching the game. A missing game folder, an unknown zone
or sub-area SHALL stop with a message naming what is missing, and SHALL write nothing.

#### Scenario: One sub-area
- **WHEN** the command is run for zone 3, sub-area 0 and an output folder
- **THEN** the output folder holds one folder for the forest, named after the zone as the game
  names it and prefixed by its number (e.g. `03 - La forêt`), containing that sub-area's model,
  textures, placements, top view and visit page, and nothing else is written

#### Scenario: One folder per zone
- **WHEN** several zones are exported
- **THEN** each zone has its own folder (`00 - L'auberge`, `01 - La mine abandonnée`, …), and
  each sub-area its own sub-folder named by its number and, when the game names it, its place name

#### Scenario: Whole game
- **WHEN** the command is run with no zone
- **THEN** every sub-area of every zone that loads is exported, one folder per zone, and a summary
  lists each sub-area with its face, texture and placement counts, and any sub-area that failed with
  the reason

#### Scenario: Missing data
- **WHEN** the game folder does not contain the game data
- **THEN** the command stops with a message naming the missing file and writes nothing

### Requirement: Textured display geometry in glTF 2.0

Each exported sub-area SHALL produce a valid glTF 2.0 model holding every face the game draws for
that sub-area, at the position the game draws it, with its texture coordinates and vertex colours.
Textures SHALL be reconstructed from the game's own images and palettes as the game displays them,
written as PNG, one material per distinct texture page and palette. Fully transparent texels SHALL
stay transparent. The model SHALL open in Blender and in Unreal Engine without error.

#### Scenario: Opening the model
- **WHEN** the exported model is opened in a glTF viewer
- **THEN** the sub-area appears textured, recognisable against a screenshot of the same place in
  the game, with no face missing and no face out of place

#### Scenario: Transparency
- **WHEN** a face uses a texel the game treats as transparent
- **THEN** that texel is transparent in the exported texture

### Requirement: Collision geometry as a separate layer

The model SHALL also hold the sub-area's collision faces as a separate node, hidden from the
textured view by being distinct, each face keeping whether it is a floor and its kind as the game
reads it.

#### Scenario: Floors and walls
- **WHEN** the collision node is shown alone
- **THEN** floors and other faces can be told apart, and they line up with the textured geometry

### Requirement: Placements in JSON

Each exported sub-area SHALL produce a JSON file listing, in the same coordinates as the model: the
monster formations (species name, position, orientation, variant), the active scenery records
(sub-type, resolved behaviour, position) including chests (number, content), and the sub-area's
entry points. Every value SHALL come from the game data; a field the game data does not establish
SHALL be absent rather than guessed.

#### Scenario: A known chest
- **WHEN** the placements of a sub-area holding a chest listed in `coffres.csv` are exported
- **THEN** the JSON holds that chest at the same position and with the same content as the CSV

### Requirement: A visit page per sub-area

Each exported sub-area SHALL come with a standalone HTML page that shows the textured model in 3D,
lets the viewer orbit and move around it, toggle the collision layer and the placement markers, and
name a marker on hover. The page SHALL work offline from the output folder.

#### Scenario: Visiting a map
- **WHEN** the visit page is opened from the output folder
- **THEN** the sub-area is shown textured, can be turned around, and the collision layer and the
  markers can each be shown or hidden

### Requirement: Nothing from the game enters the repository

The command SHALL refuse an output folder inside the repository, and the repository SHALL hold no
exported file.

#### Scenario: Output inside the repository
- **WHEN** the output folder is inside the repository
- **THEN** the command stops with a message and writes nothing

### Requirement: A complete top view per sub-area

Each exported sub-area SHALL come with a PNG image showing the whole sub-area seen from above,
textured, orthographic, framed on the sub-area's extent, and not mirrored: the game's +X to the right and
the game's +Z up (glTF -Z, the usual "forward"). The game's Y axis points down, so this is the only
unmirrored orientation with +X to the right. A second
PNG SHALL show the same view with the placement markers (monsters, chests, entries) drawn and
labelled.

#### Scenario: Looking at a map from above
- **WHEN** the top view of a sub-area is opened
- **THEN** the entire sub-area is visible, textured, with nothing cropped, and the annotated image
  shows each chest, entry and monster formation at its position
