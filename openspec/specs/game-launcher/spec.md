# game-launcher Specification

## Purpose

Gives the player one program to start Blaze & Blade — alone, hosting, or joining a friend — and lets
player 1 play with any controller Windows knows, configured on a visual screen, without modifying
the game.

## Requirements

### Requirement: One launcher starts every kind of game

A single program, `OpenForesiaLanceur.exe`, installed next to the game, SHALL offer four choices from
its home window: Play alone, Host, Join (with an invitation code), and Configure my controller. It
SHALL replace `OpenForesiaMultiplayer.exe` and keep every hosting and joining behaviour that program
had.

#### Scenario: Playing alone

- **WHEN** the player starts the launcher and chooses Play alone
- **THEN** the game starts, and a launcher window stays open for as long as the game runs

#### Scenario: Hosting and joining unchanged

- **WHEN** the player chooses Host, or Join with a code
- **THEN** the session behaves as it did with `OpenForesiaMultiplayer.exe`

### Requirement: The launcher alone is enough to join

The launcher SHALL work as a single file, without the game, its data or an installation. When
`OpenForesia.exe` is not in the launcher's own folder, the home window SHALL offer only Join and
Configure my controller; Play alone and Host SHALL NOT be shown. The release SHALL provide the
launcher as a separate download next to the installer, and that file SHALL contain no game data.

#### Scenario: A friend without the game

- **WHEN** a friend who does not own the game starts the launcher on its own
- **THEN** the home window offers Join and Configure my controller only, and joining works

#### Scenario: Launcher next to the game

- **WHEN** the launcher is started from the game's installation folder
- **THEN** the home window offers Play alone, Host, Join and Configure my controller

### Requirement: Player 1 plays with a controller

When the game is started from the launcher, alone or hosting, player 1 SHALL be driven by the
controller configured for this PC, whether it is an XInput controller or any other controller
Windows lists as a joystick, including its directional pad, triggers and both sticks where
configured. Player 1's keyboard SHALL keep working at the same time, with the player's own key
bindings.

#### Scenario: Controller and keyboard together

- **WHEN** player 1 presses a controller button bound to Cross, then a key bound to Cross in the game's options
- **THEN** both act as Cross in the game

#### Scenario: No controller configured

- **WHEN** the game is started from the launcher and no controller has been configured
- **THEN** player 1 plays with the keyboard exactly as when the game is started directly

#### Scenario: Hosting with a controller

- **WHEN** the host plays player 1 with a controller while friends play players 2 to 4
- **THEN** each player is driven only by their own controller

### Requirement: Configuring my controller

The launcher SHALL offer a controller configuration screen showing a PlayStation pad with one box
per button. Clicking a box SHALL wait for the next input pressed and bind it to that button, taking
it away from any other button. Inputs pressed SHALL light up live. Every change SHALL be saved at
once and kept between sessions. The same configuration SHALL serve player 1 locally and the player
when they join a friend's game from this PC. The screen SHALL be reachable from the home window,
from the solo game window and from the host window, while a game is running.

#### Scenario: Rebinding during a game

- **WHEN** the player opens the configuration screen during a game, binds a new button to Triangle and closes the screen
- **THEN** the new button acts as Triangle in the game from then on, without restarting it

#### Scenario: Same configuration everywhere

- **WHEN** the player configures their controller from the home window, then joins a friend's game
- **THEN** the controller works with the same bindings in the friend's game

### Requirement: The controller acts only on the game

The controller SHALL drive player 1 only while the game window is in the foreground and the
configuration screen is closed. Whenever either condition stops holding, every button of player 1's
controller SHALL be released in the game.

#### Scenario: Configuration screen open

- **WHEN** the configuration screen is open and the player presses controller buttons
- **THEN** the buttons light up on the screen and the game receives none of them

#### Scenario: Game in the background

- **WHEN** the player switches to another window while holding a controller button
- **THEN** the button is released in the game

### Requirement: The game and its settings are left as they were

The launcher SHALL NOT modify the game program. Any change it makes to the game's settings file to
relay the controller SHALL be undone once the game has closed, and, if the launcher stopped before
undoing it, the next time the launcher starts. The game started directly SHALL keep working with the
keyboard alone.

#### Scenario: Settings restored after a game

- **WHEN** a game started from the launcher ends
- **THEN** the game's settings file holds player 1's own key bindings again

#### Scenario: Launcher stopped abruptly

- **WHEN** the launcher was killed during a game and is started again
- **THEN** player 1's own key bindings are restored before anything else

### Requirement: Installation points at the launcher

The installer SHALL lay down the launcher next to the game, with two shortcuts: « OpenForesia »
opening the game directly and « OpenForesia Lanceur » opening the launcher. Installing over an
earlier version SHALL remove `OpenForesiaMultiplayer.exe` and its « OpenForesia Multijoueur »
shortcuts. Uninstalling SHALL remove the launcher and its shortcuts.

#### Scenario: Updating an installation

- **WHEN** the player installs over a version that had `OpenForesiaMultiplayer.exe`
- **THEN** the game and the launcher remain, the « OpenForesia » shortcut opens the game and the « OpenForesia Lanceur » shortcut opens the launcher
