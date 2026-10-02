## Purpose

Lets up to three friends join the host's game of Blaze & Blade over the network: the host plays
player 1 on the PC while the game's picture and sound are streamed to the friends, whose controllers
or keyboards drive players 2 to 4 — turnkey, without third-party software, without a server of our
own, and without modifying the game.

## ADDED Requirements

### Requirement: One program hosts and joins

A single program, `OpenForesiaMultiplayer.exe`, installed next to the game, SHALL offer two choices:
Host and Join. It SHALL need nothing beyond Windows (10 version 2004 or later) and the files the
installer lays down.

#### Scenario: Hosting

- **WHEN** the host starts OpenForesiaMultiplayer and chooses Host
- **THEN** the game starts, the host plays player 1 as usual, and an invitation code is shown to share with friends

#### Scenario: Joining

- **WHEN** a friend starts OpenForesiaMultiplayer, chooses Join and enters the code
- **THEN** the friend sees and hears the host's game in a window and controls the next free player

### Requirement: The game is not modified

Hosting SHALL drive players 2 to 4 only through the game's own keyboard settings: the program SHALL
assign keys to players 2 to 4 in `OpenForesia.cfg` and simulate their presses. It SHALL never change
the keys of player 1. The keys it assigns SHALL be keys no French keyboard carries and that type no
character, so that they collide neither with player 1's keyboard nor with the character name entry.

#### Scenario: Player 1's keys untouched

- **WHEN** the host has customised player 1's keys and then hosts a session
- **THEN** player 1's keys are unchanged in `OpenForesia.cfg` and in the game

#### Scenario: Typing a name while friends press buttons

- **WHEN** the host types a character's name while a friend presses buttons
- **THEN** no character is added to the name

### Requirement: A friend's controller maps to a PlayStation pad

A joining friend SHALL configure their controller or keyboard once, button by button, for the
fourteen PlayStation pad inputs (cross, circle, triangle, square, L1, R1, L2, R2, start, select and
the four directions), the way a PlayStation emulator does; the configuration SHALL be kept for the
next sessions. Any controller Windows recognises, and the keyboard, SHALL be usable. Only the state
of those fourteen inputs SHALL travel to the host.

#### Scenario: First session

- **WHEN** a friend joins for the first time
- **THEN** they are asked to press the button they want for each of the fourteen inputs before the game is shown

#### Scenario: Pressing cross

- **WHEN** a friend playing player 3 presses the button configured as cross
- **THEN** player 3 acts in the game exactly as with a pad's cross button

### Requirement: Up to three friends, one player each

A session SHALL accept at most three friends. Each friend SHALL take the lowest free player among 2,
3 and 4, keep it until they leave, and free it when they leave. A fourth friend SHALL be refused with
a message.

#### Scenario: A friend leaves

- **WHEN** the friend playing player 2 closes their window
- **THEN** player 2 stops receiving input, and the next friend to join becomes player 2

#### Scenario: Session full

- **WHEN** a fourth friend tries to join
- **THEN** they are told the session is full and nothing changes for the others

### Requirement: Friends see and hear the game with low delay

The host SHALL stream the game window's picture and the game's own sound — not the rest of the
host's screen or sound — to every friend. On a home connection with enough upload, the delay between
a friend pressing a button and seeing its effect SHALL stay around a tenth of a second.

#### Scenario: Other windows stay private

- **WHEN** the host opens another window or plays a video beside the game
- **THEN** friends see and hear only the game

### Requirement: Joining over the internet without a server

The invitation code SHALL carry what a friend needs to reach the host: address, port and a session
key. The host SHALL open its port automatically when its router allows it, and otherwise explain the
port to redirect. All traffic of a session SHALL be encrypted with the session key; a friend without
the code SHALL not be able to join or watch.

#### Scenario: Router without automatic opening

- **WHEN** the host's router refuses to open the port automatically
- **THEN** the host is shown which port to redirect, and friends can join once it is done

#### Scenario: Without the code

- **WHEN** someone connects to the host's port without the session key
- **THEN** they receive nothing and control nothing

### Requirement: The game window must stay in front on the host

Friends' input SHALL reach the game only while the game window is in front on the host's PC; the
program SHALL tell the host so when the session starts, and tell friends when input is paused for
that reason.

#### Scenario: The host switches window

- **WHEN** the host switches to another window during a session
- **THEN** friends are told their input is paused, and it resumes when the game window is back in front
