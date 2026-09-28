## Purpose

Gives the project a room of its own inside the inn: a door in the reception leads to an empty copy
of the chest room, the future home of project-specific features; disabled, the game is the original.

## ADDED Requirements

### Requirement: The hub is switched by a LegacyPlus key, off by default

The game SHALL enable the inn hub only when `LegacyPlus.ini` carries `[Auberge] Hub = oui`. The key
SHALL accept `oui` or `non`, default `non`. A missing file, a missing key or `Hub = non` SHALL leave
the inn exactly as in the original game: no extra door, no extra room.

#### Scenario: Hub disabled

- **WHEN** the game starts without `[Auberge] Hub = oui`
- **THEN** the inn's reception shows no extra door and every map of the inn behaves as in the original game

### Requirement: A door in the reception of every story stage

With the hub enabled, the reception of the inn (quest 0, maps whose number is a multiple of 8, from
0 to 80) SHALL show an extra door against a fixed wall of the main hall. The door SHALL NOT swing
when the party touches it. When a standing party member stands in front of that door, faces the
wall within 45 degrees and presses their action button, and no text box is open, the party SHALL
move to the hub. That button press SHALL NOT also trigger the member's attack or action.

#### Scenario: Entering the hub from the last stage

- **WHEN** the hub is enabled and player 1, in map 80, stands in front of the extra door facing the wall and presses the action button
- **THEN** the party arrives in the hub

#### Scenario: Entering the hub from an early stage

- **WHEN** the hub is enabled and player 1, in map 8, stands in front of the extra door facing the wall and presses the action button
- **THEN** the party arrives in the same hub

#### Scenario: Facing away from the door

- **WHEN** the hub is enabled and player 1 stands next to the extra door with their back to it and presses the action button
- **THEN** the player performs their usual action and nothing else happens

### Requirement: The hub is an empty copy of the chest room

The hub SHALL have the walls, floor and lighting of the inn's chest room (map 82), read from the
player's installed game data, with none of its furniture: only its door and its storage chest SHALL
be present, the chest standing against the wall opposite the door. The storage chest SHALL behave as
the original storage chest. The party SHALL be able to walk the whole floor without hitting
invisible obstacles where furniture used to stand. Arriving in the hub SHALL NOT show the chest
room's title box, and SHALL place the party near the hub's door. Map 82 itself SHALL remain
unchanged.

#### Scenario: The hub is empty

- **WHEN** the party arrives in the hub
- **THEN** no title box opens, the party stands near the hub's door, and the room shows its door and its storage chest and no other furniture

#### Scenario: The storage chest works in the hub

- **WHEN** a player opens the storage chest in the hub, deposits an item, leaves and comes back
- **THEN** the item is still in the storage chest, as with the original chest

#### Scenario: Map 82 is untouched

- **WHEN** the hub is enabled and the party enters map 82 through the inn's upstairs corridor
- **THEN** map 82 shows all its original furniture

### Requirement: The hub's door leads back to the reception the party came from

Leaving the hub through its door SHALL bring the party back to the reception of the same story stage
they entered from, on the ground floor of the main hall, near the extra door and not on the raised
platform. It SHALL NOT lead to the upstairs corridor nor to another story stage.

#### Scenario: Round trip from an early stage

- **WHEN** the party enters the hub from map 16 and walks out through the hub's door
- **THEN** the party is in map 16, on the ground floor of the main hall, near the extra door

### Requirement: A save never points at the hub

A game saved while the party is in the hub, from the menu or by using the storage chest, SHALL
record the reception the party entered the hub from, so that it reloads there whether or not the
hub is enabled when the save is loaded.

#### Scenario: Saving in the hub, loading without the tweak

- **WHEN** a player saves in the hub, then disables the hub and loads that save
- **THEN** the game loads without error in the reception the party came from
