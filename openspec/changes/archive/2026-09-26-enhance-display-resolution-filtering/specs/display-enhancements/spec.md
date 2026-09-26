## Purpose

Defines how the game's picture is rendered and presented on a modern display — internal
resolution, anti-aliasing, final scaling, 3D texture filtering and interface filtering — and how the
player controls each of them through a dedicated, read-only settings file, without touching the
game's own menus.

## ADDED Requirements

### Requirement: Display settings come from a dedicated file the game only reads

The game SHALL read its display settings from `Affichage.ini` in the executable's folder, once at
startup, before its window is created. The game MUST NOT create, modify or rewrite that file. A
missing file, a missing key or an unrecognised value SHALL fall back to that key's default value,
independently for each key, and MUST NOT prevent the game from starting. `OpenForesia.cfg` SHALL
keep its 1998 format and SHALL NOT receive any of these settings.

#### Scenario: No settings file

- **WHEN** the game starts and `Affichage.ini` does not exist
- **THEN** every display setting takes its default value
- **AND** the game starts normally and no `Affichage.ini` is created

#### Scenario: One invalid value

- **WHEN** `Affichage.ini` sets `Anticrenelage = 3` and a valid `ResolutionInterne = 2`
- **THEN** anti-aliasing takes its default value
- **AND** the internal resolution is 2

#### Scenario: The game's own settings are untouched

- **WHEN** the player changes a volume in the game's options menu
- **THEN** `OpenForesia.cfg` is rewritten as before
- **AND** `Affichage.ini` is unchanged

### Requirement: Defaults are the modern rendering

When no value is given, the settings SHALL be `ResolutionInterne = auto`, `Anticrenelage = 4`,
`MiseAEchelle = ajustee`, `Filtrage3D = jeu`, `FiltrageInterface = net`. Setting
`ResolutionInterne = 1`, `Anticrenelage = 1`, `Filtrage3D = proche` SHALL reproduce the rendering of
the current release: scene rendered at the game's logical size, no anti-aliasing, nearest-texel
sampling everywhere.

#### Scenario: Faithful rendering on request

- **WHEN** `Affichage.ini` sets `ResolutionInterne = 1`, `Anticrenelage = 1` and `Filtrage3D = proche`
- **THEN** a capture of the scene target matches, pixel for pixel, the capture of the same frame made
  before this change

### Requirement: The scene is rasterised at the chosen internal resolution

The scene SHALL be rasterised into a target whose size is the game's logical render size multiplied
by `ResolutionInterne` (an integer from 1 to 8), or, for `auto`, exactly the size at which the scene
is displayed in the window. The game's logic, its vertices and its logical render size MUST remain
unchanged: an internal resolution changes how finely primitives are rasterised, never where they
are.

#### Scenario: Integer factor

- **WHEN** the game renders at a logical size of 640x480 and `ResolutionInterne = 3`
- **THEN** the scene target is 1920x1440
- **AND** every primitive covers the same fraction of the picture as at factor 1

#### Scenario: Auto on a 1440p display

- **WHEN** the window's client area is 2560x1440, the logical size is 640x480 and `ResolutionInterne = auto`
- **THEN** the scene target is 1920x1440
- **AND** it is presented at that size with no further resampling

#### Scenario: The game changes its render size

- **WHEN** the game changes its logical render size at run time (its `Size` setting, F11/F12)
- **THEN** the scene target follows, keeping the chosen factor, and the window does not change

### Requirement: Anti-aliasing is selectable

The scene target SHALL be multisampled with the count given by `Anticrenelage` (1, 2, 4 or 8). When
the display adapter does not support the requested count, the highest supported count below it SHALL
be used and the fallback reported once in the log.

#### Scenario: Unsupported count

- **WHEN** `Anticrenelage = 8` and the adapter supports at most 4 samples for the scene format
- **THEN** the scene is rendered with 4 samples and one log line says so

### Requirement: Final scaling to the window is selectable

The scene SHALL be presented centred in the window, keeping its 4:3 aspect ratio, with black borders
elsewhere. With `MiseAEchelle = ajustee` it SHALL fill the largest 4:3 rectangle that fits and be
resampled smoothly. With `MiseAEchelle = entiere` it SHALL be scaled by the largest integer factor
that fits, nearest-pixel, so that every scene pixel has the same size on screen; when even factor 1
does not fit, it SHALL fall back to `ajustee`.

#### Scenario: Integer scaling

- **WHEN** the scene target is 640x480, the window is 2560x1440 and `MiseAEchelle = entiere`
- **THEN** the scene is shown at 1920x1440, each scene pixel exactly 3x3 screen pixels

### Requirement: 3D texture filtering follows the game or the player

`Filtrage3D` governs polygons. `jeu` SHALL filter a polygon's texture bilinearly exactly when the
original engine asked the device to filter it — its `Filter` setting and its per-texture decision
(8-bit pages it zooms stay unfiltered) both taken into account — and sample it nearest otherwise.
`proche` SHALL always sample polygons nearest. `bilineaire` SHALL filter every polygon bilinearly,
regardless of `Filter` and page depth. The F10 key toggles nothing: in the 1998 game its handler is
never polled, and this change does not revive it.

#### Scenario: The game's own setting is honoured

- **WHEN** `Filtrage3D = jeu` and `OpenForesia.cfg` has `Filter=0`
- **THEN** polygons are sampled nearest, as the 1998 game did with that setting

#### Scenario: F10 stays inert, as in 1998

- **WHEN** `Filtrage3D = jeu` and the player presses F10 in game
- **THEN** texture filtering does not change

#### Scenario: Forced filtering

- **WHEN** `Filtrage3D = bilineaire` and `OpenForesia.cfg` has `Filter=0`
- **THEN** 3D textures are filtered bilinearly

### Requirement: Filtering never produces colour-key halos or page bleeding

Whenever a texture is filtered, texels whose colour key marks them transparent MUST NOT contribute
their colour to a visible pixel, and a primitive MUST NOT sample texels outside the rectangle of
its texture page that its own texture coordinates span. Transparency SHALL keep its original
all-or-nothing meaning: a filtered pixel is either drawn or discarded, never blended with the
background because of filtering.

#### Scenario: Sprite edge

- **WHEN** a colour-keyed sprite is drawn with filtering enabled
- **THEN** its edge shows no dark or black fringe

#### Scenario: Neighbouring sprites in one page

- **WHEN** two sprites sit side by side in the same texture page and one is drawn filtered
- **THEN** no texel of the other appears along its border

### Requirement: Interface textures have their own filtering setting

Sprites — primitives the original engine binds as sprites (texts, icons, window frames), which it
never filtered — SHALL follow `FiltrageInterface`: `net` samples them nearest, `lisse` filters them
bilinearly under the same colour-key and page-bleeding guarantees. Every other textured primitive
(polygons: scenery, characters, portraits, backgrounds) SHALL follow `Filtrage3D`. Movies SHALL
follow `MiseAEchelle` only.

#### Scenario: Sharp interface with filtered 3D

- **WHEN** `Filtrage3D = bilineaire` and `FiltrageInterface = net`
- **THEN** the menus and texts keep hard pixel edges while 3D surfaces are filtered
