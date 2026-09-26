## MODIFIED Requirements

### Requirement: Display settings come from a dedicated file the game only reads

The game SHALL read its display settings from `Affichage.ini` in the executable's folder, once at
startup, before its window is created. The game MUST NOT create, modify or rewrite that file. A
missing file, a missing key or an unrecognised value SHALL fall back to that key's default value,
independently for each key, and MUST NOT prevent the game from starting. `OpenForesia.cfg` SHALL
keep its 1998 format and SHALL NOT receive any of these settings. The recognised keys SHALL be
`[Affichage] Rendu`, `Mode`, `Fenetre` and `MiseAEchelle`.

#### Scenario: No settings file

- **WHEN** the game starts and `Affichage.ini` does not exist
- **THEN** every display setting takes its default value
- **AND** the game starts normally and no `Affichage.ini` is created

#### Scenario: One invalid value

- **WHEN** `Affichage.ini` sets `Fenetre = grande` and a valid `Mode = plein-ecran`
- **THEN** the window size takes its default value
- **AND** the game opens in full screen

#### Scenario: The game's own settings are untouched

- **WHEN** the player changes a volume in the game's options menu
- **THEN** `OpenForesia.cfg` is rewritten as before
- **AND** `Affichage.ini` is unchanged

### Requirement: Defaults are the modern rendering

When no value is given, the settings SHALL be `Rendu = moderne`, `Mode = fenetre`, `Fenetre = auto`,
`MiseAEchelle = ajustee`. `Rendu = origine` SHALL reproduce the rendering of the port before
`Affichage.ini` existed: scene rendered at the game's logical size, no anti-aliasing, nearest-texel
sampling for every primitive whatever `Filter=` says.

#### Scenario: Faithful rendering on request

- **WHEN** `Affichage.ini` sets `Rendu = origine`
- **THEN** a capture of the scene target matches, pixel for pixel, the capture of the same frame made
  before `enhance-display-resolution-filtering`

#### Scenario: Modern rendering by default

- **WHEN** `Affichage.ini` does not exist
- **THEN** the scene is rasterised at its displayed size, with 4 samples when the adapter supports
  them, and polygon textures filtered as `Filter=` asks

## ADDED Requirements

### Requirement: The scene is rasterised at its displayed size

With `Rendu = moderne`, the scene SHALL be rasterised into a target exactly the size at which it is
displayed in the window — never smaller than the game's logical render size — and multisampled with
4 samples, or the highest supported count below 4, the fallback reported once in the log. When the
window changes size, the target SHALL follow. With `Rendu = origine`, the target SHALL be the logical
render size with a single sample. The game's logic, its vertices and its logical render size MUST
remain unchanged: the target size changes how finely primitives are rasterised, never where they
are.

#### Scenario: A 1440p display

- **WHEN** the window's client area is 2560x1440, the logical size is 640x480 and `Rendu = moderne`
- **THEN** the scene target is 1920x1440
- **AND** it is presented at that size with no further resampling

#### Scenario: A window smaller than the logical size

- **WHEN** the window's client area is 400x300 and the logical size is 640x480
- **THEN** the scene target is 640x480

#### Scenario: The game changes its render size

- **WHEN** the game changes its logical render size at run time (its `Size` setting, F11/F12)
- **THEN** the scene target follows and the window does not change

#### Scenario: Unsupported sample count

- **WHEN** the adapter supports at most 2 samples for the scene format
- **THEN** the scene is rendered with 2 samples and one log line says so

### Requirement: Texture filtering follows the game

With `Rendu = moderne`, a polygon's texture SHALL be filtered bilinearly exactly when the original
engine asked the device to filter it — its `Filter` setting and its per-texture decision (8-bit pages
it zooms stay unfiltered) both taken into account — and sampled nearest otherwise. Sprites —
primitives the engine binds as sprites (texts, icons, window frames) — SHALL always be sampled
nearest. With `Rendu = origine`, every primitive SHALL be sampled nearest. The F10 key toggles
nothing: in the 1998 game its handler is never polled, and this change does not revive it.

#### Scenario: The game's own setting is honoured

- **WHEN** `Rendu = moderne` and `OpenForesia.cfg` has `Filter=0`
- **THEN** polygons are sampled nearest, as the 1998 game did with that setting

#### Scenario: Filter on

- **WHEN** `Rendu = moderne` and `OpenForesia.cfg` has `Filter=1`
- **THEN** polygons whose page the engine filters are filtered bilinearly
- **AND** menus and texts keep hard pixel edges

#### Scenario: F10 stays inert, as in 1998

- **WHEN** the player presses F10 in game
- **THEN** texture filtering does not change

### Requirement: The window opens in the chosen mode

The game window SHALL open in the mode given by `Mode`: `fenetre` — a normal window with a frame
and title bar, resizable; `fenetre-sans-bordure` — a window without frame or title bar, of the size
given by `Fenetre` (the whole monitor for `auto`), centred on the primary monitor, which stays on
screen when the player uses another monitor; `plein-ecran` — **exclusive** full screen on the
monitor, at the desktop's resolution, which the game leaves when it loses focus and takes back when
it regains it. Before the game's first Direct3D frame (title screen, attract), `plein-ecran` SHALL
show as a frameless window covering the monitor. `fenetre` and `fenetre-sans-bordure` SHALL NOT
change the display mode of the monitor. Alt+Enter SHALL switch between the starting mode and
`plein-ecran`; when the starting mode is `plein-ecran`, it SHALL switch to `fenetre` at the size
given by `Fenetre`. The chosen mode SHALL NOT be written back anywhere.

#### Scenario: Exclusive full screen at startup

- **WHEN** `Mode = plein-ecran` on a 2560x1440 monitor
- **THEN** the game covers the whole monitor, without frame, from its first visible frame
- **AND** from its first Direct3D frame it holds the monitor in exclusive full screen at 2560x1440

#### Scenario: Leaving to a second monitor in full screen

- **WHEN** `Mode = plein-ecran` and the player clicks on another monitor
- **THEN** the game leaves exclusive full screen
- **AND** takes it back when the player returns to it

#### Scenario: A second monitor next to a borderless window

- **WHEN** `Mode = fenetre-sans-bordure` and the player clicks on another monitor
- **THEN** the game window stays in place on its monitor and the display mode never changes

#### Scenario: Borderless window

- **WHEN** `Mode = fenetre-sans-bordure` and `Fenetre = 1280x960`
- **THEN** the game opens as a 1280x960 window without frame or title bar, centred on screen

#### Scenario: Alt+Enter from full screen

- **WHEN** the game started with `Mode = plein-ecran` and the player presses Alt+Enter
- **THEN** the game shows in a framed window whose client area is the `Fenetre` size

### Requirement: The window size is chosen by the player

`Fenetre` SHALL give the client-area size of the window in `fenetre` and `fenetre-sans-bordure`
modes, as `<largeur>x<hauteur>` or `auto`. In `fenetre-sans-bordure`, sizes are measured against the
whole monitor, taskbar included, and `auto` SHALL be the whole monitor. Otherwise they are measured
against the monitor's work area, frame included, and `auto` SHALL be the largest rectangle with the
proportions of the game's logical render size that fits — or, with `MiseAEchelle = entiere`, the
largest integer multiple of it that fits —, and at least the logical size. A size larger than the
area it is measured against SHALL be reduced to fit, keeping its proportions, with one log line. The
setting SHALL NOT be called « résolution »: it does not change the game's logical render size, which
stays governed by `Resolution=`/`Size` in `OpenForesia.cfg`.

#### Scenario: Automatic size

- **WHEN** `Mode = fenetre`, `Fenetre = auto`, the logical size is 1024x768 and the work area leaves
  1904x993 inside the frame
- **THEN** the window's client area is 1324x993

#### Scenario: Automatic size, integer scaling

- **WHEN** `Mode = fenetre`, `Fenetre = auto`, `MiseAEchelle = entiere`, the logical size is 640x480
  and the work area is 2560x1400
- **THEN** the window's client area is 1280x960

#### Scenario: Borderless takes the whole screen

- **WHEN** `Mode = fenetre-sans-bordure` and `Fenetre = auto` or `Fenetre = 1920x1080` on a
  1920x1080 monitor with a taskbar
- **THEN** the window covers the whole monitor, taskbar included

#### Scenario: Explicit size

- **WHEN** `Fenetre = 1600x1200` on a 2560x1440 monitor
- **THEN** the window's client area is 1600x1200

#### Scenario: Too large

- **WHEN** `Mode = fenetre` and `Fenetre = 4000x3000` on a 1920x1080 monitor
- **THEN** the window is reduced to fit the work area and one log line says so

### Requirement: Retired keys are ignored quietly

The keys `ResolutionInterne`, `Anticrenelage`, `Filtrage3D` and `FiltrageInterface` SHALL be
recognised as retired: when present, each SHALL be ignored with one log line naming it as retired,
and MUST NOT affect rendering or prevent the game from starting.

#### Scenario: An old settings file

- **WHEN** `Affichage.ini` still carries `ResolutionInterne = 8` and `Anticrenelage = 8` from an
  earlier install
- **THEN** the game renders with the modern defaults
- **AND** the log names both keys as retired

## REMOVED Requirements

### Requirement: The scene is rasterised at the chosen internal resolution

**Reason**: An integer factor above the displayed size only supersamples a picture the window shrinks
again — no visible gain for a large GPU cost — and it never sharpens textures.
**Migration**: Replaced by « The scene is rasterised at its displayed size » (the former `auto`);
`ResolutionInterne = 1` becomes `Rendu = origine`.

### Requirement: Anti-aliasing is selectable

**Reason**: Redundant with rendering at the displayed size; a fixed 4 samples covers the 3D edges
where it shows.
**Migration**: Fixed at 4 samples in `Rendu = moderne`, 1 in `Rendu = origine`.

### Requirement: 3D texture filtering follows the game or the player

**Reason**: The game already has its own setting (`Filter=`), and its behaviour is the one wanted.
**Migration**: Replaced by « Texture filtering follows the game »; `Filtrage3D = proche` becomes
`Rendu = origine`, `Filtrage3D = bilineaire` has no replacement (use `Filter=1`).

### Requirement: Interface textures have their own filtering setting

**Reason**: Smoothing pixel-art menus and texts was never wanted; `net` was the default.
**Migration**: Sprites are always sampled nearest.
