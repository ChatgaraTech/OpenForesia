# `OpenForesia.cfg` — le fichier de configuration

`OpenForesia.cfg` est l'équivalent du `BLAZE.cfg` de 1998 : mêmes sections, mêmes clés, même
encodage des liaisons de touches. Le jeu le lit au démarrage (`LoadOptionsIni`, Ghidra
`FUN_0050f140`) et le réécrit à chaque sortie de la page d'options (`SaveOptionsIni`,
`FUN_0050f300`).

**La version par défaut est conservée ici : [`OpenForesia.cfg.default`](OpenForesia.cfg.default).**
C'est une copie octet pour octet du `BLAZE.cfg` installé par le jeu d'origine
(`Legacy/Binaries/Blaze.cfg`), et son contenu est **vérifié contre le binaire** — voir « D'où viennent les
valeurs par défaut » plus bas. Le fichier réellement utilisé vit à côté de l'exécutable et n'est
pas suivi par git ; c'est pour ça qu'une référence était nécessaire.

## Où le jeu le cherche

À côté de l'exécutable : son nom, extension remplacée par `.cfg`. Depuis le 2026-09-26 (abandon du
CD, `openspec/changes/install-from-iso`), le jeu se place au démarrage dans le dossier de son exe
et lit `.\OpenForesia.cfg` en **relatif**, côté jeu (`GetIniFilePath`) comme côté `winsys3d`
(`Setup3DDevice`). L'original bâtissait un chemin absolu avec `GetModuleFileNameA`, qu'un dossier
d'installation hors de la page de code ANSI aurait abîmé. Donc :

| lancement | fichier lu |
|---|---|
| `bin\Debug\net10.0\OpenForesia.exe`, Rider « Run », `dotnet run`, `playtest.ps1` | `bin\Debug\net10.0\OpenForesia.cfg` |
| jeu installé par `OpenForesia-Setup` | `OpenForesia.cfg` du dossier d'installation |

Le dossier `save\` et les données du jeu (`French\`, `Snd\`, `Music\`) se trouvent au même endroit.
S'il n'existe pas, sa première écriture (`WritePrivateProfileStringA`) le crée.

## Les trois sections

### `[D3DDriver]`

Écrite par la mise en place du périphérique, pas par les options. `Driver=display` et une
`Description` en clair. Sans intérêt pour la reconstruction, conservée pour rester fidèle au format.

### `[Options]`

| clé | défaut | rôle |
|---|---|---|
| `SoundVol` | 64 | volume des effets |
| `CdVol` | 64 | volume de la musique |
| `Gamma` | 64 | correction gamma, mise à l'échelle par `GammaIniScale` |
| `Filter` | 1 | filtrage de texture, si le périphérique l'accepte |
| `Small` | 0 | textures réduites (`AU_00564a05._51_4_`) |
| `Size` | 11 | niveau de rendu ; le viewport vaut `(Size + 9) * 0x20` par `(Size + 9) * 0x18` |
| `Resolution` | 2 | index dans la table des modes d'affichage |
| `Timing` | 1 | mode de cadencement passé à `SetTimingMode` **par la page « Timing » uniquement** — voir la note plus bas |
| `Joy0`..`Joy3` | 1 | manette activée pour le joueur N |

### `[Controls]` — l'encodage des liaisons

240 clés, nommées `K<joueur><table><action>` en hexadécimal :

```
K 0    2    0f
  │    │    └── action, 00..13 (20 actions)
  │    └─────── table,  0 = clavier principal, 1 = clavier secondaire, 2 = boutons de manette
  └──────────── joueur, 0..3
```

C'est exactement la disposition du tableau `DAT_00564b58` en mémoire : un `int` par entrée, pas de
20 entiers par table, pas de **60** entiers par joueur (`0x3c`, la constante qu'on retrouve partout
dans le code de l'éditeur de touches). Côté port, c'est `s_KeyBindings`, un `int[4 * 3 * 20]`.

Les 20 actions, dans l'ordre, avec le masque manette que `s_PadButtonMasks_00564f18`
(`DAT_00564f18`) leur associe :

| action | masque | bouton | action | masque | bouton |
|---|---|---|---|---|---|
| `00` | `0x0100` | L2 | `0a` | `0x0000` | — |
| `01` | `0x0200` | R2 | `0b` | `0x0008` | START |
| `02` | `0x0400` | L1 | `0c` | `0x0010` | haut |
| `03` | `0x0800` | R1 | `0d` | `0x0020` | droite |
| `04` | `0x1000` | triangle | `0e` | `0x0040` | bas |
| `05` | `0x2000` | rond | `0f` | `0x0080` | gauche |
| `06` | `0x4000` | croix | `10` | `0x0500` | L2+L1 |
| `07` | `0x8000` | carré | `11` | `0x0a00` | R2+R1 |
| `08` | `0x0001` | SELECT | `12` | `0x0c00` | L1+R1 |
| `09` | `0x0000` | — | `13` | `0x0300` | L2+R2 |

Les quatre dernières sont des **combinaisons**, pas des boutons : une seule liaison y suffit à poser
deux bits d'un coup.

Recoupement, qui vaut vérification des deux côtés : la table 0 du joueur 1 donne action `06` = 56
(`0x38`, Alt gauche), `07` = 29 (`0x1D`, Ctrl gauche), `08` = 59 (F1), `0b` = 60 (F2) et
`0c`..`0f` = 72/77/80/75 (les flèches) — exactement les correspondances relevées en jeu et
consignées dans `test-protocol.md`.

- **tables 0 et 1** : des **scancodes DirectInput**. `0` signifie « pas de liaison ». Les deux
  tables sont deux liaisons clavier par action, la seconde servant de secours ; les deux sont lues
  par `FoldKeyboardIntoPadState`.
- **table 2** : des **numéros de bouton de manette**, et ici **`-1` signifie « pas de liaison »**,
  pas `0` — `0` est un bouton valide. Le code s'appuie sur cette convention : l'octet qui précède la
  table de libellés `DAT_00562e99` vaut `0xff`, si bien que `DAT_00562e99[-1]` rend `-1` et la ligne
  s'affiche « aucun ». C'est délibéré, pas un débordement.

Le tableau des correspondances jeton `playtest.ps1` → action → touche est dans `test-protocol.md`.

## D'où viennent les valeurs par défaut

**Elles ne sont pas dans du code, elles sont dans `.data`.** Le tableau `DAT_00564b58` est
pré-initialisé à l'édition de liens avec les 240 valeurs, et `LoadKeyBindings` ne fait que les
écraser pour les clés effectivement présentes dans le fichier (`GetPrivateProfileIntA` avec `-1`
comme valeur de repli, et le résultat n'est retenu que s'il diffère de `-1`).

Lues directement dans `Legacy/Binaries/Blaze.exe` le 2026-08-18 (`.data`, VA `0x00564B58`, 960 octets) :

```
J0 T0 : 71, 79, 81, 73, 57, 42, 56, 29, 59, 0, 0, 60, 72, 77, 80, 75, 0, 0, 12, 13
J0 T1 : 199, 207, 209, 201, 0, 0, 1, 28, 0, 0, 0, 0, 200, 205, 208, 203, 210, 211, 74, 78
J0 T2 : 11, 8, 13, 12, 10, 9, 7, 6, 15, -1, -1, 14, 2, 1, 3, 0, -1, -1, -1, -1
J1..J3 T0/T1 : tout à 0        (aucun clavier pour les joueurs 2 à 4)
J1..J3 T2    : identique à J0 T2
```

Ces 240 valeurs correspondent **clé pour clé** aux 240 lignes `[Controls]` du `BLAZE.cfg` installé.
Les deux sources se confirment l'une l'autre, ce qui donne au fichier de référence une autorité que
n'aurait pas une simple copie.

Deux autres tables du binaire portent les mêmes valeurs, pour la remise aux valeurs par défaut :

- `DAT_00564ab8`, 40 entiers — les deux tables clavier du joueur 1 (`s_KeyBindingDefaults_00564ab8`) ;
- `DAT_00564a68`, 20 entiers — la table manette, celle que la page de configuration des boutons
  recopie sur sa dernière ligne (`s_JoyBindingDefaults_00564a68`).

## Pièges connus

- **Le fichier réellement écrit contient des clés périmées.** `WritePrivateProfileString` ajoute et
  met à jour, mais ne supprime jamais. Un `OpenForesia.cfg` qui a vécu peut donc encore porter des
  clés d'une version antérieure du port — on a observé `UNKNOWN_REVERSE_005630a0` (l'ancien nom de
  `CdVol`) et un jeu de `Joy 0`..`Joy 3` **avec une espace**, doublons inertes d'un écrivain corrigé
  depuis. Ils ne gênent pas la lecture ; en cas de doute, repartir de la référence.
- **`Timing` n'a d'effet que si on passe par la page « Timing ».** La valeur est lue et réécrite,
  mais `SetTimingMode` n'est appelée que depuis cette page (`FUN_00510d40`) — jamais au démarrage,
  ni dans le jeu d'origine. Le mode de cadencement de la DLL reste donc à 0, et la cadence vient
  entièrement de la présentation ; voir `REVERSE_STATUS.md`, 2026-08-18.
- **`Size` est plafonné** au chargement par la table `DAT_00562e00`, selon `Resolution`. Une valeur
  trop grande dans le fichier est donc silencieusement ramenée au maximum du mode courant.
