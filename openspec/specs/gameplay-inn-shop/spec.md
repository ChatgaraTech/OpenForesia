# gameplay-inn-shop Specification

## Purpose
Gives the gold players collect a use: a shop in the inn, at the wardrobe of the chest room, that
sells equipment from a catalogue the player controls; disabled, the game is the original one.
## Requirements
### Requirement: The shop is configured by a dedicated file the game only reads

The game SHALL read the shop settings from `Boutique.ini` in the executable's folder, once, and MUST
NOT write it. Section `[Boutique]` key `Actif` SHALL accept `oui` or `non`, default `non`. A missing
file, or `Actif = non`, SHALL leave the game exactly as the original.

#### Scenario: No shop file

- **WHEN** the game starts and `Boutique.ini` does not exist
- **THEN** the wardrobe of the inn's chest room behaves as in the original game

### Requirement: The shop opens at the wardrobe of the inn's chest room

With `Actif = oui`, when a standing party member in quest 0, map 82, is within about 120 units of
the wardrobe, faces it within 45 degrees and presses their action button, and no text box is
already open, the game SHALL open the shop, and that button press SHALL NOT also trigger the
member's attack or action. Anywhere else, the action button SHALL behave as in the original game.

#### Scenario: Opening the shop

- **WHEN** `Actif = oui` and player 1 stands in front of the wardrobe in map 82, facing it, and presses the action button
- **THEN** the shop screen opens, the world is frozen while it is open, and the player gets control back when leaving it

#### Scenario: Facing away

- **WHEN** `Actif = oui` and player 1 stands next to the wardrobe with their back to it and presses the action button
- **THEN** the player performs their usual action and no shop opens

#### Scenario: Another map

- **WHEN** `Actif = oui` and a player presses the action button in any other map
- **THEN** the button behaves as in the original game

### Requirement: The shop screen lists the catalogue with prices and the buyer's purse

The shop screen SHALL list the catalogue of `Boutique.ini` in file order, ten items per page, each
with its price, and SHALL show the gold in the purse of the member who opened it. The buyer's
controller SHALL move the cursor (up and down), change page (left and right), attempt a purchase
(action button) and leave (the button that opens the game's menu). A purchase the purse does not
cover SHALL be refused with a message.

#### Scenario: Browsing

- **WHEN** the shop is open and the buyer presses down twice, then right
- **THEN** the cursor moves two items down, then to the next page

#### Scenario: Not enough gold

- **WHEN** the buyer's purse holds 50 gold and the buyer tries to buy an item priced 95
- **THEN** the purchase is refused with a message and the purse still holds 50 gold

### Requirement: Buying puts the item in the buyer's inventory

When the buyer's purse covers the price, a purchase SHALL deduct the price from that purse and put
the item in the buyer's inventory exactly where a chest would put it — stacked on the same item when
the item stacks, otherwise in the last free slot — with its icon, and identified. When the ten slots
are taken, the purchase SHALL be refused with a message and nothing SHALL be deducted.

#### Scenario: A purchase

- **WHEN** the buyer holds 666 gold and buys an item priced 95
- **THEN** the purse holds 571 gold and the item appears in the buyer's inventory with its icon and its name

#### Scenario: Full inventory

- **WHEN** the buyer's ten inventory slots are taken and the item does not stack on one of them
- **THEN** the purchase is refused with a message and the purse is unchanged

#### Scenario: Saving

- **WHEN** the buyer saves the game after a purchase and loads it again
- **THEN** the bought item is still in the inventory, with its icon
