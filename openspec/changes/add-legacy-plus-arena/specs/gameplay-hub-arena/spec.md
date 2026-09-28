## Purpose

Offers, from the inn hub, a replayable boss fight in an arena room: a teleporter, a menu choosing the
mode and a difficulty from 1 to 10, the fight, and a way back to the hub whether the party wins or
falls — without ever touching the story or the save.

## ADDED Requirements

### Requirement: The arena exists whenever the hub exists

The arena, its teleporter and its menu SHALL be present whenever the inn hub is enabled
(`[Auberge] Hub = oui`), with no setting of their own. With the hub disabled, the game MUST behave
exactly as without this change.

#### Scenario: Hub disabled

- **WHEN** `[Auberge] Hub = non`
- **THEN** there is no teleporter anywhere, and the old tower room plays as in the original game

#### Scenario: Hub enabled

- **WHEN** `[Auberge] Hub = oui` and the party enters the hub
- **THEN** the arena teleporter stands in the hub

### Requirement: A teleporter in the hub asks before anything happens

The hub SHALL hold one teleporter, looking like the one at the entrance of the Labyrinth of the Dead,
at x = −766, z = 1235. When a standing party member comes near it, the game SHALL ask « Ce
téléporteur semble dangereux, on y va ? » with the choices Oui and Non, for that member only. Non, or
the menu button, SHALL close the question and leave the party where it is.

#### Scenario: Declining

- **WHEN** player 2 walks up to the teleporter and answers Non
- **THEN** the question closes, nothing else opens, and player 2 can walk away and come back to be asked again

### Requirement: The arena menu chooses a mode and a difficulty

After Oui, a menu SHALL open for the member who answered, in the look of the shop screen. It SHALL
list the available modes by name; after a mode is picked, it SHALL offer a difficulty from 1 to 10,
then a final confirmation. The menu button SHALL go back one step, and close the menu from the first
step. Only the member who touched the teleporter controls the menu.

#### Scenario: Choosing a fight

- **WHEN** player 1 answers Oui, picks the listed boss, picks difficulty 4 and confirms
- **THEN** the whole party is sent to the arena, where the boss fights at difficulty 4

#### Scenario: Backing out

- **WHEN** player 1 is on the difficulty step and presses the menu button twice
- **THEN** the menu returns to the mode list, then closes, and the party is still in the hub

### Requirement: Difficulty multiplies the boss's hit points and damage

At difficulty *n* (1 to 10), the arena boss SHALL be born with *n* times the hit points of its
original sheet and SHALL deal *n* times its original damage. Difficulty 1 SHALL be the original game.
Nothing else in the fight SHALL change, and no other creature SHALL be affected.

#### Scenario: Difficulty 1

- **WHEN** the boss is fought at difficulty 1
- **THEN** its hit points and damage are those of the original game

#### Scenario: Difficulty 10

- **WHEN** a boss whose sheet gives 910 hit points is fought at difficulty 10
- **THEN** it is born with 9 100 hit points and its blows deal ten times their original damage

### Requirement: The arena is the tower boss room, emptied

The arena SHALL be the boss room of the old tower (zone 4, sub-area 9) with its own creatures,
chest and scripted events removed, holding only what the current mode brings. Outside the arena
mode, that room MUST stay exactly as in the original game, story included.

#### Scenario: The real tower is untouched

- **WHEN** the party reaches the tower's boss room through the story, after having used the arena
- **THEN** its original boss, escort and chest are there, and the story proceeds as in the original game

### Requirement: Beating the boss plays the ending and opens the way back

When every creature of the mode is dead, the game SHALL play the tower's own end-of-boss cinematic,
then a portal SHALL stand at the centre of the room together with two chests. Walking into the
portal SHALL bring the whole party to the centre of the hub.

#### Scenario: Victory

- **WHEN** the arena boss dies
- **THEN** the camera shows the portal appearing, two chests stand at the centre, and entering the portal brings the party to the centre of the hub

### Requirement: A party wipe in the arena returns to the hub

When every party member has fallen in the arena, the game SHALL play its death fade and then, instead
of the game-over screen, SHALL bring every member back to life with all their hit points and send
the party to the hub,
showing « Ouille, nom d'un panda à la sauce tomate, j'ai mal à la tête ». Outside the arena, a party
wipe MUST end the game as in the original.

#### Scenario: Defeat

- **WHEN** the last standing member falls to the arena boss
- **THEN** the screen fades as for a death, every member wakes up in the hub with full hit points, and the message is shown

#### Scenario: Defeat elsewhere

- **WHEN** the party is wiped out in the forest
- **THEN** the game-over screen appears, as in the original game

### Requirement: The arena leaves no trace in the story or the save

Nothing that happens in the arena SHALL change a story flag, and the game SHALL NOT save in the arena:
a save made after an arena fight SHALL hold what it would have held without the fight, apart from
what the party carries and the members' state. Quitting during a fight SHALL resume from the last
save, as quitting anywhere does. The arena SHALL be replayable any number of times.

#### Scenario: Playing it again

- **WHEN** the party wins, comes back through the portal and uses the hub teleporter again
- **THEN** the same fight is offered and plays out the same way

#### Scenario: No save in the arena

- **WHEN** a member opens the game menu in the arena
- **THEN** saving is not possible there
