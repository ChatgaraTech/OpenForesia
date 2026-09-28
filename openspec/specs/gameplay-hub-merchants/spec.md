# gameplay-hub-merchants Specification

## Purpose
Fills the inn hub with four specialised merchants, each wearing the look of a playable class and
selling one kind of item from a catalogue the player controls; disabled, the hub stays empty.
## Requirements
### Requirement: Each merchant is configured by its own file the game only reads

The game SHALL read four shop files from a `LegacyPlus` folder beside the executable, once, and MUST
NOT write them: one for usable items, one for accessories, one for armour and shields, one for
weapons. In each, section `[Catalogue]` SHALL list items as `name = price`. Every merchant SHALL be
present whenever the hub is enabled; a missing or unreadable file SHALL leave that merchant with an
empty catalogue. A `[Boutique] Actif` key left in an older file SHALL be ignored.

#### Scenario: No shop files

- **WHEN** the hub is enabled and the `LegacyPlus` folder holds no shop file
- **THEN** the five merchants stand in the hub, each with nothing to sell

#### Scenario: An older file still says Actif = non

- **WHEN** the hub is enabled and the weapons file still carries `[Boutique] Actif = non`
- **THEN** the weapons merchant stands in the hub and sells its catalogue

### Requirement: Four merchants stand in the hub, idle, as playable classes

Each merchant SHALL stand in the hub along its west wall, facing into the room, playing the
idle animation of the playable class it wears, without moving: the usable-items merchant as the
Lutin, the accessories merchant as the Voleur, the armour merchant as the Kobold, the weapons
merchant as the Sylphide. The merchants SHALL be visible whatever the classes in the player's party,
and SHALL NOT change how the party members look.

#### Scenario: Merchants whatever the party

- **WHEN** a party with no Lutin, Voleur, Kobold nor Sylphide enters the hub with all four shops enabled
- **THEN** the four merchants stand in the hub with the looks of those four classes, and the party members look as usual

### Requirement: A merchant sells only its kind of item

Each merchant SHALL offer only the items of its catalogue whose kind, read from the item's own
record, matches its speciality: weapons; armour and shields; accessories; or usable items (none of
the four equipment kinds). An item listed in the wrong file SHALL NOT be offered.

#### Scenario: A sword in the armour file

- **WHEN** the armour file lists a sword
- **THEN** the armour merchant does not offer it

### Requirement: Items are offered only to the classes that can use them

Every merchant SHALL offer, to the party member who opened the shop, only the items whose own record
lets that member's class equip or use them. An item the member's class
cannot equip SHALL NOT be shown nor bought. When nothing remains, the shop screen SHALL open with a
message saying there is nothing for that member.

#### Scenario: A warrior at the weapons merchant

- **WHEN** a warrior opens the weapons shop
- **THEN** only weapons a warrior can equip are listed

#### Scenario: Another class, another list

- **WHEN** a wizard then opens the same weapons shop
- **THEN** the list shows only the weapons a wizard can equip

#### Scenario: Accessories reserved to some classes

- **WHEN** a wizard opens the accessories shop
- **THEN** the iron gauntlets, which only a warrior or a kobold can wear, are not listed

#### Scenario: The hunter's herbs

- **WHEN** a warrior, then a hunter, opens the usable-items shop
- **THEN** the herbs are listed for the hunter only

### Requirement: The Voleuse buys back what the player carries and stores

With the hub enabled, a fifth merchant, the
Voleuse (the playable Voleur class, female), SHALL stand in the hub and, when spoken to, SHALL list
the items of the speaker's inventory that are not equipped and the items of the speaker's storage
chest. An item SHALL be listed only if one of the four shop files sells it, at `[Vente] Taux` percent
(50 by default) of that price, and a stack SHALL be sold whole. A sale SHALL add the price to the
speaker's purse and remove the item; a sale from the chest SHALL be written to the speaker's save
at once, and the chest SHALL still open normally afterwards. A sale that would take the purse above
99 999 SHALL be refused.

#### Scenario: Selling a stack from the inventory

- **WHEN** a player carrying 12 Petites potions, sold 60 by the usable-items merchant, sells them to the Voleuse at 50 %
- **THEN** the purse gains 360 and the slot is empty

#### Scenario: Equipped items stay

- **WHEN** a player speaks to the Voleuse
- **THEN** the weapon, armour, shield and accessories the player wears are not listed

#### Scenario: Selling from the chest

- **WHEN** a player sells an item listed from the chest, then opens the chest
- **THEN** the chest shows its other items, without the sold one, and loading the save later keeps the sale

### Requirement: A merchant opens its shop at the action button

When a standing party member stands in front of a merchant, faces it within 45 degrees and presses
their action button, and no text box is open, the game SHALL open that merchant's shop screen, and
that button press SHALL NOT also trigger the member's attack or action.

#### Scenario: Opening the weapons shop

- **WHEN** player 1 stands in front of the Sylphide, facing it, and presses the action button
- **THEN** the shop screen opens on the weapons catalogue, the world is frozen, and the player gets control back when leaving it
