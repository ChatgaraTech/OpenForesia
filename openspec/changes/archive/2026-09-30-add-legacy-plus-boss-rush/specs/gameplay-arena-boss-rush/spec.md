## Purpose

Offers, in the hub arena, challenges that chain several boss fights in one visit — each boss alone,
one after the other, with a short pause between them — at the arena's usual difficulties and for a
larger purse.

## ADDED Requirements

### Requirement: Four challenges are offered on their own page

The arena menu SHALL list four challenges after the single fights, starting a page of their own —
page 3 with the nineteen single fights of today —, in this order, their names shown in full:
`[CHALLENGE] - Boss Rush 1`, `[CHALLENGE] - Boss Rush 2`, `[CHALLENGE] - Boss Rush 3`,
`[CHALLENGE] - Ultimate`. Picking one SHALL lead to the same difficulty step as a single fight.

#### Scenario: Reaching the challenges

- **WHEN** player 1 opens the arena menu and moves to page 3
- **THEN** the four challenges are listed there, in that order, and pages 1 and 2 hold only the single fights

### Requirement: A challenge is a sequence of bosses without escort

Each challenge SHALL bring its bosses one wave at a time, without any of their escorts, in this order:

| Challenge | Waves |
|---|---|
| Boss Rush 1 | Méchant ours; Jeune dragon; Enchanteresse; Loup-garou and Tigre together; Nocturio |
| Boss Rush 2 | Sylphide; Fougre; Durahan; Troll; Behemoth |
| Boss Rush 3 | Gros poilu; Roi-zombie; Cerbère; Eifridt; Belzébuth |
| Ultimate | Lordémon; Ange noir; two Dragons zombies together; two Dragons rouges together |

Every boss SHALL look, move, attack and sound as in its single fight.

#### Scenario: No escort

- **WHEN** the Troll's wave of Boss Rush 2 appears
- **THEN** the Troll stands alone, without its two Géants verts

#### Scenario: Two bosses at once

- **WHEN** the last wave of Ultimate appears
- **THEN** two Dragons rouges stand in the arena at the same time, each with its own hit points

### Requirement: The next boss comes five seconds after the previous one has vanished

A wave SHALL end when every creature it brought has died and its body has disappeared from the room.
The next wave SHALL appear five seconds later. After the last wave, the arena SHALL end as for a
single fight: the tower's cinematic, the purse, the portal.

#### Scenario: Between two bosses

- **WHEN** the Méchant ours of Boss Rush 1 dies and its body disappears
- **THEN** nothing appears for five seconds, then the Jeune dragon appears

#### Scenario: End of a challenge

- **WHEN** the Behemoth of Boss Rush 2 dies and its body disappears
- **THEN** the tower's cinematic plays, the purse is paid once and the portal opens

### Requirement: A challenge uses the arena difficulties

The four arena difficulties SHALL apply to every creature of every wave exactly as in a single fight:
hit points times 1, 2, 3 or 4, never above 30 000; damage dealt to the party times 1, 1.5, 2 or 2.5.

#### Scenario: Ultimate at Impossible

- **WHEN** Ultimate is fought at Impossible
- **THEN** each of the two Dragons rouges has 30 000 hit points and deals 2.5 times its original damage

### Requirement: A challenge pays the sum of its fights plus a quarter

A challenge's purse at Normal SHALL be the sum of the purses of the single fights of its bosses, plus
25 %, rounded to the nearest ten when it is not a whole number: 750 for Boss Rush 1, 1 390 for Boss Rush 2, 2 290 for Boss Rush 3,
5 875 for Ultimate. It SHALL be multiplied by the difficulty and paid as for a single fight.

#### Scenario: Boss Rush 1 at Difficile

- **WHEN** a party beats Boss Rush 1 at Difficile
- **THEN** the party is paid 1 500 gold coins, split as for a single fight
