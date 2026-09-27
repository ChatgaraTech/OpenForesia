## Purpose

Lets several attackers hit the same enemy in quick succession, as a four-player party expects,
while keeping every single attack from hitting the same enemy more than once per invulnerability
window; `oui` keeps the original shared lock.

## ADDED Requirements

### Requirement: The enemy invulnerability window is kept per attacker

`LegacyPlus.ini` section `[Combat]` key `InvulnerabiliteEnnemis` SHALL accept `oui` (default) or
`non`. With `non`, when a party attack looks for an enemy to hit, an enemy whose invulnerability
window is running SHALL be refused only to the same attack that hit it:

- a projectile or spell entity SHALL be refused until the window its own hit opened, lasting as
  long as the original lock that hit set, has run out;
- a melee gesture SHALL be refused only while the same gesture is still held on its striking frame;
  the member's next gesture SHALL hit.

Any other attack SHALL hit it.

#### Scenario: Four players strike together

- **WHEN** `InvulnerabiliteEnnemis = non` and player 1 hits an enemy, then player 2 strikes it five frames later
- **THEN** player 2's hit lands and deals damage

#### Scenario: A melee fighter chains swings

- **WHEN** `InvulnerabiliteEnnemis = non` and a warrior swings twice at an enemy, the second swing landing while the enemy still blinks from the first
- **THEN** both swings deal damage

#### Scenario: One attack never counts twice

- **WHEN** `InvulnerabiliteEnnemis = non` and a projectile that lasts several frames hits an enemy
- **THEN** that same projectile does not hit that enemy again before the window its hit opened has run out

#### Scenario: Default

- **WHEN** `InvulnerabiliteEnnemis` is absent or equals `oui`
- **THEN** a blinking enemy refuses every party hit, as in the original game

### Requirement: The visible reaction to a hit is unchanged

Each landed hit SHALL still make the enemy blink, recoil and be interrupted as in the original game.
The party's own invulnerability after being hit by a monster SHALL be unchanged.

#### Scenario: A monster hits a player

- **WHEN** `InvulnerabiliteEnnemis = non` and a monster hits a player
- **THEN** the player blinks and cannot be hit again during the blink, as in the original game
