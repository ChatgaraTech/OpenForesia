## MODIFIED Requirements

### Requirement: Beating the boss plays the ending and opens the way back

When every creature of the mode is dead, the game SHALL play the tower's own end-of-boss cinematic,
then a portal SHALL stand at the centre of the room, and the mode's purse SHALL be paid to the party:
its gold coins times the chosen difficulty, split evenly between every party member, standing or
fallen, any remainder going one coin each to the first members, and no purse exceeding 99 999. A
message SHALL tell the party how much it won. No chest SHALL appear. Walking into the portal SHALL
bring the whole party to the centre of the hub.

The purse follows the boss's strength, from 40 coins for MECHANT OURS to 1 200 for DRAGON ROUGE; each
mode's purse is listed with the modes themselves (`gameplay-arena-bosses`).

#### Scenario: Victory

- **WHEN** the arena boss dies
- **THEN** the camera shows the portal appearing, no chest stands in the room, and entering the portal brings the party to the centre of the hub

#### Scenario: The Méchant ours's purse, four members

- **WHEN** a party of four beats MECHANT OURS at difficulty 1
- **THEN** each member's purse grows by 10 gold coins, and the message says the party won 40 coins

#### Scenario: A split with a remainder

- **WHEN** a party of three beats MECHANT OURS at difficulty 1
- **THEN** the first member gets 14 coins and the two others 13 each
