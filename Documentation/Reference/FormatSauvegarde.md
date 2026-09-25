# Le fichier de sauvegarde

Un fichier `OpenForesia\save\Blaze-Saving<code>` par personnage : **24 576 octets** (`0x6000`), en
quatre blocs. Relevé le 2026-09-25 en lisant les trois écrivains — `Saves.WriteCardSaveFile`
(`Engine/SaveData.cs`), la sauvegarde du menu (`WorldMapMenu.cs`) et celle du dépôt
(`WriteDepotSaveFile`, `WorldMapDepot.cs`) — puis vérifié octet pour octet sur quatre sauvegardes
réelles par `OpenForesia.Tools/Html/check-editeur-sauvegarde.mjs`.

C'est le format que lit et écrit l'**éditeur de sauvegarde** (`OpenForesia.Tools/Html/`).

| Plage du fichier | Contenu |
|---|---|
| `0x0000..0x1fff` | **La fiche du personnage**, les `0x2000` octets de `MainMem + 0xf0000 + i * 0x2000`. Ses `0x100` premiers octets sont écrasés par l'en-tête de carte mémoire. |
| `0x2000..0x3eff` | **Le dépôt de l'auberge** : douze emplacements d'objet de `0x280` octets. |
| `0x3f00..0x3fff` | **La copie** de `0x1f00..0x1fff` de la fiche, somme de contrôle comprise. |
| `0x4000..0x5fff` | La table des vignettes : `0x200` entrées de seize octets (les noms courts des objets). |

## La somme de contrôle

`0x1ffc` porte la **somme des `0x1ffc` premiers octets lus comme signés**, sur 32 bits. Le
chargement (`PartySetup.cs`) refuse une fiche dont la somme ne correspond pas.

**La copie de `0x3f00` compte aussi.** Le dépôt relit `0x2000..0x3fff` d'un bloc et compare
`0x3ffc` (la copie de la somme) à la somme du personnage chargé. Une copie périmée fait croire au
jeu que le dépôt est vide. Tout écrivain doit donc recalculer `0x1ffc`, **puis** recopier
`0x1f00..0x1fff` en `0x3f00`.

**Le nom du fichier ne dépend plus de la fiche une fois créé.** Il est calculé à la création (un
hachage du nom, des sept caractéristiques, de la classe, de l'élément et du style, voir
`legacy-bug.md` LB-002), rangé en `+0x1fd0`, et le jeu réécrit ensuite **toujours sous ce nom**.
Changer le nom ou les caractéristiques d'une fiche ne la détache donc pas de son fichier.

## L'en-tête de carte mémoire — `+0x000..+0x0ff`

Le format « title frame » de la PlayStation, rempli par `WriteCardSaveFile` :

| Décalage | Contenu |
|---|---|
| `+0x00` | `"SC"`, `0x11`, `0x03` |
| `+0x04..+0x43` | le titre en Shift-JIS pleine largeur : un intitulé fixe, le **nom** (`NameEncodeChar`), la classe |
| `+0x60..+0x7f` | la palette de l'icône, seize couleurs PlayStation (BGR555, `0` transparent) |
| `+0x80..+0xff` | l'icône : 16 × 16 en 4 bits par pixel, le portrait du personnage |

## La fiche — ce qui est établi

| Décalage | Taille | Rôle | Source |
|---|---|---|---|
| `+0x100` | 16 | nom, Latin-1, terminé par un nul | création |
| `+0x112` | 2 | `1 << classe` | `FinalizeCharacterStats` |
| `+0x114` | 4 | `1 << élément \| 0x800` | idem |
| `+0x120..+0x13f` | | copie de travail des caractéristiques et dérivées — **recalculée** à chaque ouverture du menu | `CommitStagedStatBlock` et suivantes |
| `+0x140` | 4 | états (poison, pétrification `0x100`…) | `Sorts.md` |
| `+0x144` | 2 | niveau « plancher », celui que le menu affiche | `MonteeDeNiveau.md` |
| `+0x146` | 2 | niveau courant, qui fixe le palier d'expérience | idem |
| `+0x148` / `+0x14a` | 2 + 2 | PV max / PM max | idem |
| `+0x14c` / `+0x14e` | 2 + 2 | PV / PM courants | idem |
| `+0x150` | 1 | identité : `genre << 3 \| classe` (genre 1 = femme) | `DebugParty.cs` |
| `+0x151` | 1 | élément | `MonteeDeNiveau.md` |
| `+0x158` | 4 | jauge d'expérience, remise à zéro à chaque niveau | idem |
| `+0x160..+0x177` | 3 × 8 | masques des sorts connus, écoles 0, 1 et 2, un bit par sort | `Sorts.md` |
| `+0x178..+0x17e` | 7 | **compteurs d'herbes** du Chasseur (école 3) | `StoreItemInInventory_005312D0` |
| `+0x180..+0x1ff` | | durées des états | le soin du menu les efface |
| `+0x26c` | 4 | or | onglet Statistiques |
| `+0x280..+0x28c` | 7 × 2 | les sept caractéristiques **actuelles** : FO, INT, VO, AGI, CON, PM, CH | `MonteeDeNiveau.md` |
| `+0x290..+0x29e` | 8 × 2 | dérivées — **recalculées** (`ComputeDerivedStats`) | |
| `+0x2a0..+0x2ac` | 7 × 2 | les mêmes, **figées à la création** : elles fixent gains et plafonds | `MonteeDeNiveau.md` |
| `+0x2b0..+0x2b4` | 5 | équipement : arme, armure, bouclier, deux accessoires — un numéro d'emplacement d'inventaire, `0xff` pour rien | `ApplyEquipmentStatBonuses` |
| `+0x2b5` | 1 | école de sorts | `Sorts.md` |
| `+0x2c0..` | | liste d'inventaire affichée — **reconstruite** par le menu | |
| `+0x2e0..+0x2ff` | 32 | progression (la banque SYSTEM) : bits 0 à 8 les neuf cristaux, bit 9 la dalle, bit 12 la page « Cristaux » du menu ; `+0x2fe` les destinations, voir ci-dessous | `mode-debug.md` |
| `+0x300..+0x1bff` | 10 × `0x280` | l'inventaire | ci-dessous |
| `+0x1d00..+0x1eff` | 8 × 64 | les huit banques d'état (FLAG, WORK, TEMP, ITEM, puis `0x005e9720`, `0x005e92c0`, `0x005e9980`, `0x005e9360`) | `RestorePartyGlobals` |
| `+0x1f00` | 1 | `0` : l'état partagé qui suit est présent | idem |
| `+0x1f01..+0x1f03` | 3 | lieu de reprise : zone, sous-zone, point d'entrée | idem |
| `+0x1f04..+0x1f43` | 8 × 8 | les repères de la carte | idem |
| `+0x1fd0` | | nom du fichier, `Blaze-Saving` + sept caractères | `SetCardFileName` |
| `+0x1ffc` | 4 | somme de contrôle | |

**Les destinations — `+0x2fe`, un bit chacune** (`Destination.cs`, `navMask`) : bit 0 la mine
abandonnée, 1 la montagne du dragon de feu, 2 la vieille tour, 3 le Labyrinthe des morts, 4 le
château de l’immortel, 5 les ruines souterraines, 6 la vallée d’argent, 7 les ruines dans le lac.
L’auberge et la forêt sont toujours accessibles ; la grotte scellée ne se choisit pas sur la carte.
Le jeu lit le OU de cet octet sur tout le groupe.

**Les banques et le lieu de reprise ne sont relus que sur la fiche du premier membre du groupe**
(`RestorePartyGlobals`). Les autres fiches les portent aussi, mais le chargement ne les lit pas.

## Un emplacement d'objet — `0x280` octets

Le même format dans l'inventaire (`+0x300 + i * 0x280`) et dans le dépôt (`0x2000 + i * 0x280`) :

| Décalage | Contenu |
|---|---|
| `+0x000..+0x13f` | **la fiche de l'objet**, recopiée depuis la table du CD : les mêmes `0x140` octets que la colonne `octets` de `Bestiaire/objets.csv` |
| `+0x000` | nom court (seize octets, dont un pictogramme de tête `w`, `m` ou `s`) ; **un premier octet nul veut dire « emplacement libre »** |
| `+0x010` | nombre d'exemplaires (plafonné à 99) — remplace les deux octets de la fiche |
| `+0x012` | octet bas : les classes qui peuvent s'en servir (`1 << classe`) ; octet haut : l'emplacement d'équipement — `0x100` arme, `0x200` armure, `0x400` bouclier, `0x800` accessoire |
| `+0x013` | `& 0x30` : empilable |
| `+0x020..+0x03f` | seize bonus ajoutés à `+0x120..` quand l'objet est porté |
| `+0x041..` | « nom long/description », en Latin-1 |
| `+0x140..+0x15f` | la palette de la vignette |
| `+0x160..+0x27f` | la vignette : 24 × 24 en 4 bits par pixel |

**La vignette passe par la mémoire vidéo** : `StoreItemInInventory_005312D0` la capture au moment
où l'objet est rangé, et le chargement la renvoie en mémoire vidéo depuis la fiche
(`DebugParty.cs`). Un objet écrit dans une sauvegarde sans sa vignette s'affiche sans image, mais
fonctionne normalement.

**Mais sa source est sur le CD**, juste après la fiche dans le catalogue d'objets de la sous-zone :
les `0x140` octets qui suivent chaque fiche sont exactement ceux que la capture rend. Le verbe
`bestiaire` les écrit dans `Bestiaire/vignettes.csv` ; la vignette de la Petite potion y est
identique, octet pour octet, à celle des quatre sauvegardes relevées (2026-09-25).

**Le jeu vide un emplacement en mettant son premier octet à zéro**, et rien d'autre
(`WorldMapMenu.cs`, `ItemScript.cs`, `Inventory.cs`). Un emplacement libre garde donc souvent les
restes de l'objet précédent. Le jeu refuse de jeter un objet porté.

## Ce qui reste à établir

- Le sens de la plupart des bits des banques et de la progression.
- Les octets `+0x110`, `+0x116..+0x11f`, `+0x154..+0x157`, `+0x200..+0x26b`, `+0x270..+0x27f`.
- Le rôle exact de la table des vignettes de `0x4000`, que le dépôt complète.
- L'intitulé fixe du titre (`LabelAt(0x134)`) et les tables d'accents de `NameEncodeChar`.
