# gameplay-hub-merchants Specification

## Purpose
Fills the inn hub with four specialised merchants, each wearing the look of a playable class and
selling one kind of item from a catalogue the player controls; disabled, the hub stays empty.
## Requirements
### Requirement: Each merchant is configured by its own file the game only reads

The game SHALL read four shop files from a `LegacyPlus` folder beside the executable, once, and MUST
NOT write them: one for usable items, one for accessories, one for armour and shields, one for
weapons. In each, section `[Boutique]` key `Actif` SHALL accept `oui` or `non`, default `non`, and
section `[Catalogue]` SHALL list items as `name = price`. A merchant SHALL be present only when the
hub is enabled and its file exists with `Actif = oui`.

#### Scenario: No shop files

- **WHEN** the hub is enabled and the `LegacyPlus` folder holds no shop file
- **THEN** the hub is empty, as without this change

#### Scenario: One merchant enabled

- **WHEN** the hub is enabled and only the weapons file sets `Actif = oui`
- **THEN** only the weapons merchant stands in the hub

### Requirement: Four merchants stand in the hub, idle, as playable classes

Each enabled merchant SHALL stand in the hub along its west wall, facing into the room, playing the
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

### Requirement: A merchant opens its shop at the action button

When a standing party member stands in front of a merchant, faces it within 45 degrees and presses
their action button, and no text box is open, the game SHALL open that merchant's shop screen, and
that button press SHALL NOT also trigger the member's attack or action.

#### Scenario: Opening the weapons shop

- **WHEN** player 1 stands in front of the Sylphide, facing it, and presses the action button
- **THEN** the shop screen opens on the weapons catalogue, the world is frozen, and the player gets control back when leaving it
