# gameplay-inn-shop Specification

## Purpose
Gives the gold players collect a use: the shop screen and the purchase that the merchants of the
inn hub open (capability `gameplay-hub-merchants`); the wardrobe shop of the chest room that first
carried them is retired.
## Requirements
### Requirement: The shop screen lists the catalogue with prices and the buyer's purse

The shop screen SHALL list the catalogue of the merchant that opened it, in file order, ten items per
page, each with its price, and SHALL show the gold in the purse of the member who opened it. The
buyer's controller SHALL move the cursor (up and down), change page (left and right), attempt a
purchase (action button) and leave (the button that opens the game's menu). A purchase the purse
does not cover SHALL be refused with a message.

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
