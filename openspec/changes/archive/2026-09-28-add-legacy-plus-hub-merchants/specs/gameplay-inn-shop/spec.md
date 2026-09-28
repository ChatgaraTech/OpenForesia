## MODIFIED Requirements

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

## REMOVED Requirements

### Requirement: The shop is configured by a dedicated file the game only reads

**Reason**: the single shop becomes four merchants in the inn hub, each with its own file.
**Migration**: the four files of the `LegacyPlus` folder (capability `gameplay-hub-merchants`);
`Boutique.ini` is no longer read — its prices can be copied into them by hand.

### Requirement: The shop opens at the wardrobe of the inn's chest room

**Reason**: the wardrobe of map 82 was reachable only at the last story stage; the merchants of the
inn hub replace it, reachable from every reception.
**Migration**: open a shop at a merchant of the hub (capability `gameplay-hub-merchants`).
