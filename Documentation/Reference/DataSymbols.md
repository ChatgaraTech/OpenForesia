# Recovered data symbols

Values read out of `Legacy/Binaries/Blaze.exe`'s initialised data, keyed by the **Ghidra virtual address**
`Blaze.c` uses. This is lookup material: an entry stops changing once its bytes are read.
`REVERSE_STATUS.md` keeps the narrative and the per-TODO status and links here.

Binary: `Legacy/Binaries/Blaze.exe`, 1,589,760 bytes, MD5 `46d90d1b4526f4a9a441f851c77179b3`.
Identity check — image base `0x400000` + entry RVA `0x147D40` = `0x00547D40`, which is exactly
`entry`'s address in `Blaze.c`. Same build, confirmed.

Read with `OpenForesia.Tools at Legacy/Binaries/Blaze.exe <va> [bytes]`.

**The same tool works on the helper DLLs**, which sit in `Legacy/Binaries/` — `at Legacy/Binaries/winsys3d.dll <va>`,
`at Legacy/Binaries/3dhelp.dll <va>`, and so on, using the very `@ 0xXXXXXXXX` addresses their `.c` files carry.
The "code-only decompilation" blocker described in `CLAUDE.md` is specific to `Blaze.c`; a DLL
symbol whose value lives in initialised data is **not** blocked, it is one command away. Findings
from a DLL belong in `Documentation/DLL/<dll>.md`, not in this file, which stays `Blaze.exe`-only.
Worked examples: the display-mode and scaling tables in `Documentation/DLL/winsys3d.md`, the
texture class vtable in `Documentation/DLL/3dhelp.md`.

## Section map

| Section | VA range | RVA | Size | File offset |
|---|---|---|---|---|
| `.text` | `0x00401000`–`0x0054C400` | `0x00001000` | `0x14B400` | `0x00000400` |
| `.rdata` | `0x0054D000`–`0x0054EE00` | `0x0014D000` | `0x001E00` | `0x0014B800` |
| `.data` | `0x0054F000`–`0x00607958` | `0x0014F000` | `0x0B8958` | `0x0014D600` |
| `.idata` | `0x00608000`–`0x00609200` | `0x00208000` | `0x001200` | `0x0016FA00` |
| `.rsrc` | `0x0060A000`–`0x0061D600` | `0x0020A000` | `0x013600` | `0x00170C00` |

`.data`'s virtual size exceeds its raw size, so addresses past roughly `0x005E9000` fall in an
uninitialised tail: zero at load, written only by code. The tool reports that case explicitly
instead of treating it as an invalid address — see TODO #21 below, where it is the answer.

## Convention: `@` is the drive-letter placeholder

Several path templates start with `@` (`0x40`, i.e. `'A' - 1`). The CD scan writes the detected
drive letter into index 0 and resets it to `@` when no drive matches — so `@` in a stored string
means "drive letter goes here", never a literal character.

---

## Paths and filenames

| VA | Raw bytes (as C string) | Notes |
|---|---|---|
| `0x005631E8` | `@:\Blaze&Blade` | CD presence marker. The 0-byte file `Blaze&Blade` exists at the CD root. |
| `0x005631F8` | `@:\FRENCH\` | **The data root.** Not the drive root — this is the bug behind `fix-cd-data-paths`. |
| `0x00563280` | `@:\cd.idx` | TODO #2, the CD-check filename. `cd.idx` (640 MiB) exists at the CD root. |
| `0x005684CC` | `DATA/Font.bmp` | TODO #17. Forward slash, mixed case — not `DATA\FONT.BMP`. |
| `0x005684DC` | `ANI01.AVI` | TODO #17. |
| `0x005684E8` | `OPENING.AVI` | TODO #17. |
| `0x005684F4` | `TEPS.AVI` | TODO #17. |
| `0x00568500` | `@:\snd\Base.snd` | TODO #22. Drive-patched **absolute** path — does not go through the data root. |
| `0x0056CCD4` | `%sDATA/TITLE640.BMP` | `wsprintfA` format for the attract title screen; `%s` takes the data root. |
| — | `BLAZE.ALL;1` | TODO #22. Note the ISO-9660 version suffix. |
| — | `SYSTEM.INI;1` | TODO #22. Same. |
| `0x005630C8` | `DATA/KEYBOARD.TXT` | Key-binding defaults; forward slash again. |

The `;1` suffixes are ISO-9660 level-1 file versions. Against a 1999 raw ISO mount that *was* the
filename; a modern Windows mount strips them. `fix-cd-data-paths` decides what to do about it.

## Options file (TODO #11)

| VA | Value |
|---|---|
| `0x00563080` | `Size` |
| `0x00563088` | `Small` |
| `0x00563090` | `Filter` |
| `0x00563098` | `Gamma` |
| `0x005630A0` | `CdVol` — the reconstruction's `UnknownIniKey_005630a0` placeholder |
| `0x005630A8` | `Options` (section name) |
| `0x005630B0` | `SoundVol` |
| `0x005630BC` | `.cfg` — **not `.ini`**; the real file is `BLAZE.cfg` |
| `0x005630C4` | `%d` (value format) |

Corroborated by the installed game's own `BLAZE.cfg`, whose `[Options]` section carries exactly
these keys.

## Messages and dialog strings

| VA | Value |
|---|---|
| `0x00563258` | `Please insert Blaze & Blade CD!` (TODO #4) |
| `0x00563278` | `Erreur` (TODO #4) |
| `0x00568510` | `Veuillez connecter la manette !` |

### `PTR_s_Blaze___Blade_00563208` — `Setup3DDevice`'s string table (TODO #3)

Five pointers, matching the slot meanings documented in `Documentation/DLL/winsys3d.md` exactly:

| Slot | Pointer | Value | Role |
|---|---|---|---|
| `[0]` | `0x005631D8` | `Blaze & Blade` | dialog title |
| `[1]` | `0x00563244` | `Selection Renderer` | static `0x3e9` |
| `[2]` | `0x0056322C` | `Définition de l'écran` | static `0x3ea` (cp1252) |
| `[3]` | `0x00563228` | `OK` | OK button, id 1 |
| `[4]` | `0x00563220` | `Annuler` | Cancel button, id 2 |

## Music filename table (TODO #30) — recovered in full, 2026-08-24

`PTR_s____Music_Mel00_01_WAV_00566cc8` — a pointer table indexed by `PlayMusicTrack`
(`FUN_00529ad0`). **40 entries**, `0x00566cc8`..`0x00566d67`; the strings themselves occupy
`0x005680d8`..`0x00568448` and are listed in descending pointer order. The table that follows at
`0x00566d68` is a different family — 0x40 spacing, pointing elsewhere — which is what fixes the
end of this one at index `0x27`.

| Index | Pointer | Value | | Index | Pointer | Value |
|---|---|---|---|---|---|---|
| `0x00` | `0x00568430` | `@:/Music/Mel00_01.WAV` | | `0x14` | `0x00568254` | `@:/Music/Mel10_01.WAV` |
| `0x01` | `0x00568418` | `@:/Music/Mel00_02.WAV` | | `0x15` | `0x0056823c` | `@:/Music/Mel10_02.WAV` |
| `0x02` | `0x00568400` | `@:/Music/Mel00_03.WAV` | | `0x16` | `0x00568224` | `@:/Music/Mel10_03.WAV` |
| `0x03` | `0x005683e8` | `@:/Music/Mel00_a.WAV` | | `0x17` | `0x0056820c` | `@:/Music/Mel05_03.WAV` |
| `0x04` | `0x005683d0` | `@:/Music/Mel00_b.WAV` | | `0x18` | `0x005681f4` | `@:/Music/Bos01_01.WAV` |
| `0x05` | `0x005683b8` | `@:/Music/Mel00_c.WAV` | | `0x19` | `0x005681dc` | `@:/Music/Bos02_01.WAV` |
| `0x06` | `0x005683a0` | `@:/Music/Title01.WAV` | | `0x1a` | `0x005681c4` | `@:/Music/Bos03_01.WAV` |
| `0x07` | `0x0056838c` | `@:/Music/End01.WAV` | | `0x1b` | `0x005681ac` | `@:/Music/Bos04_01.WAV` |
| `0x08` | `0x00568374` | `@:/Music/Mel01_01.WAV` | | `0x1c` | `0x00568194` | `@:/Music/Bos05_01.WAV` |
| `0x09` | `0x0056835c` | `@:/Music/Mel02_01.WAV` | | `0x1d` | `0x0056817c` | `@:/Music/Bos06_01.WAV` |
| `0x0a` | `0x00568344` | `@:/Music/Mel03_01.WAV` | | `0x1e` | `0x00568164` | `@:/Music/Bos07_01.WAV` |
| `0x0b` | `0x0056832c` | `@:/Music/Mel04_01.WAV` | | `0x1f` | `0x0056814c` | `@:/Music/Bos08_01.WAV` |
| `0x0c` | `0x00568314` | `@:/Music/Mel05_01.WAV` | | `0x20` | `0x00568134` | `@:/Music/Bos09_01.WAV` |
| `0x0d` | `0x005682fc` | `@:/Music/Mel05_02.WAV` | | `0x21` | `0x0056811c` | `@:/Music/Bos10_01.WAV` |
| `0x0e` | `0x005682e4` | `@:/Music/Mel06_01.WAV` | | `0x22` | `0x00572b40` | *(voir ci-dessous)* |
| `0x0f` | `0x005682cc` | `@:/Music/Mel06_02.WAV` | | `0x23` | `0x00572b40` | *(idem)* |
| `0x10` | `0x005682b4` | `@:/Music/Mel07_01.WAV` | | `0x24` | `0x00572b40` | *(idem)* |
| `0x11` | `0x0056829c` | `@:/Music/Mel07_02.WAV` | | `0x25` | `0x00568108` | `@:/Music/Menu.WAV` |
| `0x12` | `0x00568284` | `@:/Music/Mel08_01.WAV` | | `0x26` | `0x005680f0` | `@:/Music/GameOver.WAV` |
| `0x13` | `0x0056826c` | `@:/Music/Mel09_01.WAV` | | `0x27` | `0x005680d8` | `@:/Music/Bos99_01.WAV` |

Forward slashes, and drive-patched absolute paths like the sound bank — the CD's `Music\` directory
is at the root, not under `FRENCH\`. Every name above exists on the disc, `MEL10_03.wav` excepted.

**Les trois entrées `0x22`..`0x24` ne sont pas des littéraux** : elles pointent toutes sur
`&DAT_00572b40`, huit octets de la queue **non initialisée** de `.data`, déjà modélisés ailleurs
sous le nom `s_EmptyLabel_00572b40`. `FUN_00512900` les remplit au vol depuis le code d'écran
`0x00505xxx`, que la reconstruction n'a pas encore atteint. `MainMenu.cs` les laisse nulles et le
dit sur place : les pointer sur ce tampon ferait écrire la lettre de lecteur dedans.

**L'entrée `0x06` est le thème du menu**, et c'était la lacune qui rendait le menu muet : la table
n'avait que trois entrées transcrites (`0`, `1`, `7`) et `RunMainMenuFrameLoop` demande `6`.
## MS-ADPCM tables (TODO #24) — standard set, now confirmed

The reconstruction filled these from the published MS-ADPCM standard. That assumption is now
verified byte for byte.

`DAT_0056AC60`, adaptation table, 16 shorts:

```
230, 230, 230, 230, 307, 409, 512, 614, 768, 614, 512, 409, 307, 230, 230, 230
```

`DAT_0056AC80`, coefficient 1, 7 shorts: `256, 512, 0, 192, 240, 460, 392`
`DAT_0056AC90`, coefficient 2, 7 shorts: `0, -256, 0, 64, 0, -208, -232`

## Tables still needing interpretation

Raw bytes recovered; the reading of them is not yet established. Recorded separately so a later
disagreement is settled against the bytes rather than against a guess.

**`DAT_00562E00`** (TODO #12), per-resolution clamp table — dwords:
`0x00000001, 0x00000007, 0x0000000B, 0x00000010, 0x00000017, 0x00000000`, i.e. `1, 7, 11, 16, 23`
then a zero. The reconstruction currently fills this with `int.MaxValue`, making the clamp a no-op;
these are real limits and the no-op is wrong.

**`AU_00564A05`** est l'autre blob non interprete ici ; `DAT_0056ACA8` est passe dans la section
confirmee ci-dessous une fois sa lecture etablie par `FUN_00532070`.

**`DAT_005710F0`..`DAT_00571258`** (releve le 2026-08-21) — **ce n'est pas du jeu, c'est du CRT.**
`FUN_0054B190` lit `DAT_00571258` comme argument de `WideCharToMultiByte`, et `DAT_005710F0` est la
table de pages de code qui va avec, parcourue **par paires de dwords** jusqu'a `&DAT_00571258` :
`(1, 0x16), (2, 2), (3, 2), (4, 0x18), ...`. En-tete a `0x00571258` : `1, 0x2E, 1`.
A recuperer entierement le jour ou le CRT multi-octets sera transcrit. **Ne pas l'englober dans une
arene d'etat de jeu** : c'est ce que la delimitation de `s_ViewParams_00571270` a evite de peu.

**`DAT_00571400` — RETRACTATION DU 2026-08-21, le meme jour que le releve.**

Une entree de ce fichier annoncait ici une « table **initialisee** de valeurs de la forme
`0x00208xxx` » a `0x00571400..0x00571500`, avec ses premiers dwords releves. **Cette table n'existe
pas.** Elle etait le produit d'un defaut de l'outil `at` : `RvaToFileOffset` ne bornait pas la
conversion a la `SizeOfRawData` de la section, de sorte qu'une adresse tombant dans la queue **non
initialisee** de `.data` recevait quand meme un offset — lequel atterrissait dans les octets bruts
de `.idata`. D'ou les valeurs en `0x00208xxx` : c'est la RVA de `.idata` (`0x00208000`), pas une
donnee du jeu. Le garde-fou existant ne testait que `offset >= file.Length`, et cet offset-la est
parfaitement dans le fichier. Corrige dans `OpenForesia.Tools/Program.cs`.

Ce qui reste vrai, et qui est **la** frontiere a retenir pour tout le reste de `.data` :

> le contenu de `.data` present dans le fichier s'arrete a `PointerToRawData 0x0014D600 +
> SizeOfRawData 0x22400 = 0x0016FA00`, soit **VA `0x00571400`**. Au-dessus, la section n'existe
> qu'en memoire et vaut zero au chargement.

La borne haute de `s_ViewParams_00571270` tient donc toujours — mais parce que c'est cette
frontiere-la, pas parce qu'une table s'y trouvait. `Blaze.c` cite des adresses jusqu'a `0x005714FC` :
elles sont toutes ecrites par le code, jamais par la section.

**Lecon d'outillage** : un dump qui rend des octets plausibles n'est pas une preuve qu'ils
appartiennent a l'adresse demandee. Comparer l'`Offset` affiche aux bornes de la section dans
`sections` avant de tirer une conclusion d'un releve situe haut dans `.data`.

## `DAT_0056ACA8` — resource-fixup schema (TODO #20), reading CONFIRMED

37 `ushort` entries at `0x0056ACA8`, terminated by `0x0000` at `0x0056ACFC`.

An earlier revision of this file offered `(record kind, size)` as a hypothesis. **That was wrong.**
Reading `FUN_00532070` (`Blaze.c:79417-79437`) settles it from the code rather than from the shape
of the bytes — the function masks with `& 0xff` and shifts with `>> 8`, so each entry is:

- **low byte** — an index into the header's pointer array, i.e. `header[4 + index]`, a pointer the
  fixed 60-dword pass has already relocated
- **high byte** — the **byte stride** used to walk the chained list that pointer heads

Per entry the original takes that list, skips it when the pointer is null **or** its first dword is
zero (an empty list is stored as a valid pointer to a zero, not as null), then relocates each dword
in turn, stepping by the stride, until it reads a zero.

| Stride | Indices |
|---|---|
| `0x08` | `00`–`07`, `15`, `1D`, `27` |
| `0x04` | `08`, `09`, `0A`, `0E`, `10`, `11`, `14`, `17`, `18`, `19`, `1B`, `1C`, `1E`, `1F`, `21`, `23`, `25`, `26`, `28`, `2A`, `2B`, `2D`, `2F`, `30`, `31` |
| `0x14` | `0B` |

Raw, as stored (little-endian, so `00 08` is the `ushort` `0x0800`):

```
00 08  01 08  02 08  03 08  04 08  05 08  06 08  07 08
08 04  09 04  0a 04  0b 14  0e 04  10 04  11 04  14 04
15 08  17 04  18 04  19 04  1b 04  1c 04  1d 08  1e 04
1f 04  21 04  23 04  25 04  26 04  27 08  28 04  2a 04
2b 04  2d 04  2f 04  30 04  31 04  00 00
```

Every index is below `0x3c`, matching the 60 dwords the fixed pass relocates — a consistency check
the recovered bytes pass.

**Relocation formulas differ between the two passes**, and the difference is easy to miss:

| Pass | Formula |
|---|---|
| Fixed, 60 header dwords | `blob + value + header[0]` |
| Schema-driven, list contents | `blob + value + header[1] + header[0]` |

Using the shorter form in the schema pass produces pointers that are wrong by a constant and look
entirely plausible.

**`AU_00564A05`** (TODO #10) — begins `.snd`, then `2F 00 00 00 0C 0F 00 00 48 50 00 00 2D 00 00 00`,
then the tags `CHG`, `DPT`, `L…`. A mix of extension strings and numbers; not characterised.

## Uninitialised at load (TODO #21)

`DAT_006008C8`, `DAT_00604930`, `DAT_00600904` all resolve into `.data`'s uninitialised tail. They
are **zero at startup** and can only be written by code. This confirms the status file's suspicion
that they have no assignment site in the reconstructed graph yet, and means no data recovery can
help: the answer has to come from whatever code populates them.

## The game loop's tick callback (TODO #15) — resolved

`PTR_FUN_0056C96C` = **`0x00537CA0`**, and the following dword `0x0056C970` = `0x00537D50`.

`FUN_00537ca0` **is already present in `Blaze.c`** (line 83364) — no Ghidra re-run needed. It is the
**per-game-state dispatcher**: it zeroes `Zoom8BitTextures`, calls `ReInitializeAllTextures()`, then
switches on `DAT_005de510` (the game state) across at least 7 cases, dispatching to `FUN_00542130`,
`thunk_FUN_00536180`, `FUN_00542160`/`FUN_00537da0`, `thunk_FUN_00516020`, `FUN_005066d0`,
`FUN_00504c90`, `FUN_00524180`.

`FUN_00537d50` is **not** in `Blaze.c` and would need a scoped Ghidra re-run at `0x00537d50` if it
turns out to matter.

Wiring this into `RunGameLoop` is deliberately out of scope here — see `REVERSE_STATUS.md`.

## The UI text engine's two dispatch tables (`FUN_005443a0`)

`FUN_005443a0` runs the same byte stream twice over its descriptor and dispatches every byte
below `0x20` through a **different table per pass**:

| pass | table | role |
|---|---|---|
| 1 (scan) | `PTR_LAB_0056e870` | measure/skip — collects `$`-escapes, draws nothing |
| 2 (render) | `PTR_FUN_0056e728` | emit glyphs |

Both are 32 entries of 4 bytes, recovered verbatim from `Legacy/Binaries/Blaze.exe`. **They are the same
table except where pass 1 substitutes a cursor-advance stub** for a handler that would draw.

| code | pass 1 | pass 2 | code | pass 1 | pass 2 |
|---|---|---|---|---|---|
| `00` | `543950` | `544FB0` | `10` | `544F70` | `544F70` |
| `01` | `543970` | `544810` | `11` | `544F80` | `544F80` |
| `02` | `543960` | `544970` | `12` | `544F90` | `544F90` |
| `03` | `543960` | `544990` | `13` | `544FA0` | `544FA0` |
| `04` | `543960` | `5449B0` | `14` | `543950` | `544FB0` |
| `05` | `543950` | `5449D0` | `15` | `545010` | `545010` |
| `06` | `543960` | `5449F0` | `16` | `545020` | `545020` |
| `07` | `544A80` | `544A80` | `17` | `545030` | `545030` |
| `08` | `543950` | `544AB0` | `18` | `545040` | `545040` |
| `09` | `543950` | `544B10` | `19` | `545070` | `545070` |
| `0a` | `543960` | `544BC0` | `1a` | `5450A0` | `5450A0` |
| `0b` | `544C10` | `544C10` | `1b` | `5450D0` | `5450D0` |
| `0c` | `543940` | `544C50` | `1c` | `544E80` | `544E80` |
| `0d` | `543940` | `544D90` | `1d` | `544F10` | `544F10` |
| `0e` | `543950` | `544B50` | `1e` | `545100` | `545100` |
| `0f` | `544E50` | `544E50` | `1f` | `545120` | `545120` |

**Only 5 of the 31 distinct pass-2 handlers exist in `Blaze.c`** (`544810`, `544C10`, `544C50`,
`544D90`, `544FB0`). The rest are reached only through these tables, so Ghidra never emitted them
as functions — the "address-taken-only bodies" class, which needs a scoped Ghidra re-run at those
addresses rather than more reading.

### Bodies decoded by hand from the binary

The four pass-1 stubs are 16 bytes each and are **not** semantic handlers: they declare how many
operand bytes each control code consumes, so the scan can skip the stream without drawing.

| address | bytes | meaning |
|---|---|---|
| `0x00543940` | `8B 44 24 04 C3` | `return p` (no operand) |
| `0x00543950` | `8B 44 24 04 40 C3` | `return p + 1` |
| `0x00543960` | `8B 44 24 04 83 C0 02 C3` | `return p + 2` |
| `0x00543970` | `8B 44 24 04 83 C0 03 C3` | `return p + 3` |

`0x00544b10` — pass-2 handler for control code `0x09`, 49 bytes, the end-of-page/wait code:

```
8B 4C 24 08          MOV ECX,[ESP+8]        ; descriptor
8A 81 C7 08 00 00    MOV AL,[ECX+0x8C7]     ; flags
C6 41 3B 01          MOV byte [ECX+0x3B],1
0C 04                OR  AL,4               ; the bit FUN_005443a0 tests on entry
C6 81 C4 08 00 00 00 MOV byte [ECX+0x8C4],0
88 81 C7 08 00 00    MOV [ECX+0x8C7],AL
8B 44 24 04          MOV EAX,[ESP+4]        ; text cursor
8A 10                MOV DL,[EAX]           ; operand byte
40                   INC EAX
88 91 C5 08 00 00    MOV [ECX+0x8C5],DL
89 81 AC 08 00 00    MOV [ECX+0x8AC],EAX    ; descriptor->text = p+1
C3                   RET
```

Raising flag bit 2 makes every later `FUN_005443a0` call return immediately — which closes the
loop with the main menu's label stream, ending `0x09 0x3f` (code 9, operand `?`).

### What the main menu actually reaches

Its label table `DAT_0056e670` contains exactly three control codes: `0x0c` at the head, `0x0d`
five times as separators, `0x09` as terminator. Their handlers are `543940`/`544C50`,
`543940`/`544D90` and `543950`/`544B10` — **all five bodies are available**, three decompiled and
two decoded above. The main menu therefore needs no Ghidra re-run.


### `0x00544e50` — handler for control code `0x0f`, decoded by hand (2026-08-15)

Slot 15 of **both** tables, so the same body serves the scan and the render pass. 33 bytes at file
offset `0x144250`, followed by `0x90` padding.

```
8B 4C 24 08          MOV ECX,[ESP+8]        ; descriptor
8A 81 C7 08 00 00    MOV AL,[ECX+0x8C7]     ; flags
C6 41 33 01          MOV byte [ECX+0x33],1
0C 02                OR  AL,2
88 81 C7 08 00 00    MOV [ECX+0x8C7],AL
8B 44 24 04          MOV EAX,[ESP+4]        ; text cursor, returned unchanged
89 81 AC 08 00 00    MOV [ECX+0x8AC],EAX
C3                   RET
```

It is the sibling of `0x00544b10` (code `0x09`) and reads best against it. Three differences: flag
bit **1** rather than bit 2, byte `+0x33` rather than `+0x3b`, and **no operand byte consumed** —
the returned cursor is the one it was handed.

**The two byte writes are enable bits, not loose flags.** This closes a loop with the descriptor
header documented above: the header is a row of *(parameter, enable)* pairs whose enable is the
`0x1000000` bit, i.e. the dword's **top byte**. The cursor's parameter is at `+0x30` and the scroll
arrows' at `+0x38`. So:

| handler | write | effect on the header |
|---|---|---|
| `544e50` (code `0x0f`) | `[+0x33] = 1` | `0x000F28D4` → `0x010F28D4` — **switches the cursor on** |
| `544b10` (code `0x09`) | `[+0x3b] = 1` | `0x000F0A00` → `0x010F0A00` — switches the scroll arrows on |

Flag bit 1 is tested at the top of `FUN_005443a0`, which returns immediately when it is set. Code
`0x0f` therefore means **stop here and show the cursor**: a prompt awaiting a choice. It is what the
character-select screen reaches the moment a character is validated, and it needed no Ghidra re-run
after all — the body was short enough to read straight out of the binary, like the four scan stubs
and `544b10` before it.

Twenty-five of the thirty-one distinct handlers remain unreconstructed. The three now available on
the render side are `544b10`, `544c50`, `544d90`, plus `544810` and `544fb0` from `Blaze.c`, and
`544e50` here.

#### Converting a `.text` address to a file offset

Needed to decode these bodies and not written down anywhere until now. `Legacy/Binaries/Blaze.exe` has five
sections; `.text` runs `VA 0x00401000..0x0054C270` from file offset `0x400`, so for any address in
it:

```
offset = VA - 0x00401000 + 0x400
```

`0x00544e50` → `0x144250`. The other sections: `.rdata` `0x0054D000` → `0x14B800`, `.data`
`0x0054F000` → `0x14D600`, `.idata` `0x00608000` → `0x16FA00`, `.rsrc` `0x0060A000` → `0x170C00`.
## `DAT_0056e7b0` — proportional glyph widths

186 bytes, one per glyph slot, indexed by the folded character code `FUN_00543b20` computes
(subtracting `0x21`/`0x22`/`0x23`/`0x44`/`0x45`/`0x46` by range). Values are pixel widths, 2 to 10.

```
0204 0806 0a08 0204 0406 0602 0402 0406  0406 0606 0606 0606 0602 0204 0604 0606
0606 0605 0507 0602 0607 0508 0707 0608  0606 0606 0708 0606 0703 0403 0608 0505
0505 0504 0505 0203 0602 0805 0605 0504  0604 0506 0606 0606 0402 0407 0702 0606
0606 0207 040a 0606 0a04 0a04 0406 0505  0306 0702 0403 0606 080a 0a06 0606 0606
060a 0705 0506 0603 0304 0408 0708 0808  0808 0607 0606 0606 0607 0606 0606 0506
0805 0505 0606 0303 0404 0705 0606 0606  0606 0606 0606 0606 0606 0000 0000 0000
```

## The font bitmap lives inside the `MainMem` arena

`FUN_00543b20` fetches glyph rows with

```c
pbVar5 = (byte *)((iVar15 >> 3) + (iVar9 / 0x1f) * 0x230 + 0x1ad8f0 + *(int *)MainMem_exref);
```

i.e. **`MainMem + 0x1ad8f0`**, rows `0x28` bytes apart, 31 glyphs per `0x230`-byte band, 1 bit per
pixel unpacked into a 4bpp staging buffer. This sits inside the `0x280000` arena and is a further
reason the arena has to be placed correctly before anything draws.

## `PTR_LAB_00560708` — the character-select sub-state table (state 4)

`FUN_00506880`, the frame loop of game state 4, dispatches every frame through

```c
(*(code *)(&PTR_LAB_00560708)[DAT_00578de8])(...)
```

Read verbatim from `Legacy/Binaries/Blaze.exe` at **file offset `0x15ED08`** (`.data`: VA `0x0054F000` →
raw `0x0014D600`, so `0x00560708 - 0x0054F000 + 0x0014D600`):

| index | target | index | target |
|---|---|---|---|
| 0 | `0x00507010` | 4 | `0x00508E00` |
| 1 | `0x00507CB0` | 5 | `0x00509470` |
| 2 | `0x00508410` | 6 | `0x005098F0` |
| 3 | `0x00508860` | 7 | `0x0050A910` |

**Exactly eight entries.** The table stops at `0x00560728`, where `DAT_00560728` begins — a
primitive descriptor whose first dword is `0x00000031`, the very value `FUN_00506880` writes as
`puVar4[-2] = 0x31`. Eight is corroborated by the loop's own tests (`DAT_00578de8 < 7`, `!= 7`,
and `== -1` for "leave the state").

**None of the eight exists in `Blaze.c`.** The decompilation jumps from `FUN_00506880` (ending
around `0x00506FE7`) straight to `FUN_00507870`. Nothing calls them directly — only this table
points at them — so Ghidra never disassembled those regions. This is the "address-taken-only
bodies" class: it needs a scoped Ghidra re-run at the eight addresses above, not more reading of
`Blaze.c`.

All eight are real functions. Their first bytes, and a notable pattern — **seven of the eight open
on the same instruction**:

| address | first bytes | disassembly |
|---|---|---|
| `0x00507010` | `a1 a8 33 56 00` | `mov eax, [0x005633A8]` |
| `0x00507CB0` | `a1 48 2a 56 00` | `mov eax, [0x00562A48]` |
| `0x00508410` | `a1 48 2a 56 00` | `mov eax, [0x00562A48]` |
| `0x00508860` | `a1 48 2a 56 00` | `mov eax, [0x00562A48]` |
| `0x00508E00` | `a1 48 2a 56 00` | `mov eax, [0x00562A48]` |
| `0x00509470` | `a1 48 2a 56 00` | `mov eax, [0x00562A48]` |
| `0x005098F0` | `a1 48 2a 56 00` | `mov eax, [0x00562A48]` |
| `0x0050A910` | `a1 48 2a 56 00` | `mov eax, [0x00562A48]` |

`DAT_00562A48` is one of the three flags `FUN_005066e0` sets to 1 on entering the state, and which
`FUN_00506880` sets back to 1 whenever the sub-state changes:

```c
if (DAT_00578dec != DAT_00578de8) { DAT_00578dec = DAT_00578de8; DAT_00562a48 = 1; DAT_00562a4c = 1; }
```

So it is the **"first frame in this sub-state"** flag, and each handler tests it on entry to do its
one-off setup. `FUN_00506880` clears `DAT_00562a4c` right after each dispatch, so that one is
"first frame" as seen by the loop; `DAT_00562a48` is cleared by the handlers themselves.

## `DAT_00562a98` — four bytes per character-select sub-state

Indexed `(&DAT_00562a98)[DAT_00578de8 * 4 + i]` for `i` in `0 .. DAT_00562a50-1`, the value being
sign-extended into a field of the struct `FUN_00506880` passes to `FUN_0051be20`. Read at file
offset `0x161098`:

| sub-state | bytes |
|---|---|
| 0 | `00 01 02 00` |
| 1 | `00 01 02 00` |
| 2 | `00 01 02 00` |
| 3 | `00 01 02 00` |
| 4 | `00 01 02 00` |
| 5 | `00 01 03 00` |
| 6 | `00 01 03 00` |
| 7 | `00 01 03 00` |

`DAT_00562a50` — the third flag `FUN_005066e0` sets to 1 — is the row length actually consumed, so
only the first column is read until something raises it. Only the third byte varies across
sub-states (`02` for 0–4, `03` for 5–7). Its meaning is not yet established.

The bytes immediately following are separate symbols, not a ninth row: `03 00 00 02 01 02 03 06
05 04 07 08` (a permutation) then `01 00 00 80 00 40 00 20 00 10 00 08 00 04 00 02` (a bitmask
ladder).

## `DAT_005e96a0` and `DAT_005e9640` — the global model and animation tables

Both sit in `.data`'s uninitialised tail, so there is **nothing to recover from the binary** — they
are filled at load time. What matters is their scope and the layout they imply.

They are **not** specific to the character-select screen, even though `FUN_005066e0` is the first
filler to be reconstructed. `FUN_005069e0` fills them identically (`Blaze.c:78876`), and around
thirty sites index them across `Blaze.c` — 2043, 14900, 19190, 23424, 25455, 52332, 64765, 79042 —
usually by a byte taken from an entity (`*(byte *)(param_1 + 4) * 4`).

**Capacity is 24 entries**: `0x005e96a0 - 0x005e9640 = 0x60` bytes = 24 pointers at 32 bits.

Their roles, established from the read sites rather than from the fillers:

- **`DAT_005e9640` — animations.** `Blaze.c:55840` increments a frame counter, compares it against
  field `+0x16` of the entry and wraps to zero: that field is the animation length.
  `Blaze.c:79042` reads the published pointer at `[-4]`, which is exactly the bundle's `count` —
  confirming that `FUN_005326d0` publishes a pointer to the *offset array*, not to the count.
- **`DAT_005e96a0` — models.** `FUN_0051d1f0` (which returns a `uint *` OT pointer, i.e. the
  animated-model renderer) receives both together at `Blaze.c:55845`: the animation entry indexed
  by the current frame, and the model bundle alongside it.

### Model entry layout (from `FUN_00532450` + `FUN_00532530`)

Beware the scale: Ghidra types the same pointer `ushort *` in `FUN_00532450` and `int` in
`FUN_00532530`, so `param_1 + 0x12` means byte `0x24` in the first and byte `0x12` in the second.
Byte offsets below.

| offset | kind | role |
|---|---|---|
| `0x00` | `ushort` | entry count of the table at `0x30` |
| `0x08`, `0x0a` | `ushort` | vertex counts — **divided by 3** on relocation (→ triangles) |
| `0x0e` | `ushort` | written back: measured length of the `-1`-terminated list at `0x38` |
| `0x10`–`0x16` | 4 × `ushort` | records to **skip**, one counter per primitive type |
| `0x18`–`0x1e` | 4 × `ushort` | records to **repage**, one counter per primitive type |
| `0x24`, `0x28`, `0x2c` | ptr | relocated by adding the entry base; `0x28` is the record array |
| `0x30` | ptr | table of `[0x00]` ints, each relocated against the table base |
| `0x34` | ptr | table of `0x400` ints, each relocated against the table base |
| `0x38` | ptr | `-1`-terminated int list; non-zero elements relocated; length → `0x0e` |
| `0x3c` | `uint` | texture-page bits, copied from the directory descriptor's `+4` |

`FUN_00532530` then walks 16-byte records starting at `*(int *)(entry + 0x28) + 0xc`, skipping
`sum(0x10..0x16)` of them and adjusting `sum(0x18..0x1e)`, adding `(field_0x3c >> 8) & 0xc0` to two
bytes of each. 16-byte records bucketed by primitive type, each carrying a texture page, is the
layout of a PSX model — which is what pins down `DAT_005e96a0`'s role.

The `0x55555556` multiplications on `0x08`/`0x0a` are the signed divide-by-3 idiom. Its sign
correction is absent and unnecessary here: both fields are `ushort`, so `n + 2` is always positive
and the idiom reduces exactly to `/ 3`.

### Animation bundle layout (from `FUN_005326d0` + `FUN_005326b0`)

Same shape as the model bundle — a count, then that many relative offsets — but with no descriptor
array, and each entry carries **eight pointers** at `entry + 0x20`, each relocated against
`entry + entry[0]`.

## `FUN_0040a600` is empty

`Blaze.c:4319` decompiles it as `void FUN_0040a600(void) { return; }`. It is called from ~20 sites,
several of which pass arguments (`FUN_0040a600(0x25)`, `FUN_0040a600(4)`,
`FUN_0040a600(DAT_005de534)`), so Ghidra's recovered signature is wrong — but the body really is
empty in the image. Almost certainly a PSX libgpu stub (`VSync`-like) gutted during the port.
Omitting it from a reconstruction is behaviourally neutral; it should still be noted rather than
silently dropped.

## `DAT_00560728` and `DAT_00560ff0` — two static instances of the UI descriptor struct

`FUN_00506880`'s prologue initialises **nine** structures of `0x8c8` bytes starting at
`0x00573518` (Ghidra writes them through `&DAT_00573520`, which is the base plus 8 — hence its
negative indices). Only the first `0x68` bytes of each are written.

Two statically initialised instances of the **same type** live in `.data` and are readable from
`Legacy/Binaries/Blaze.exe`: `DAT_00560728` (file offset `0x15ED28`) and `DAT_00560ff0` (`0x15F5F0`).

| offset | `FUN_00506880` writes | `DAT_00560728` | `DAT_00560ff0` |
|---|---|---|---|
| `0x00` | `0x00000031` | `0x00000031` | `0x00000031` |
| `0x04` | `0` | `0x00A80028` | `0x00180028` |
| `0x08` | `0` | `0x002800E0` | `0x00180048` |
| `0x0C` | `0` | `0x00000000` | `0x00000000` |
| `0x10` | `0x10180000` | `0x021A0000` | `0x01080000` |
| `0x14` | `slot + 3` | `0x0000000C` | `0x00000000` |
| `0x18` | `0x010F0808` | `0x010F0808` | `0x010F0808` |
| `0x1C` | `0x02CF1020` | `0x02CF1020` | `0x02CF1020` |
| `0x20` | `0x01070C10` | `0x01070C10` | `0x01070C10` |
| `0x24` | `0x01000000` | `0x01000000` | `0x01000000` |
| `0x28` | `0` | `0x00000000` | `0x00000000` |
| `0x2C` | `0x01000000` | `0x01000000` | `0x01000000` |
| `0x30` | `0x000F0000` | `0x000F28D4` | `0x000F00D0` |
| `0x34` | `0x01000000` | `0x01000000` | `0x01000000` |
| `0x38` | `0x000F0C00` | `0x000F0A00` | `0x000F0A00` |
| `0x3C` | `0x01000000` | `0x01000000` | `0x01000000` |

Fourteen of the first twenty-six dwords match exactly, including the `0x31` header and the
alternating `0x01000000` at every other slot. The type is the same beyond reasonable doubt.

**What the struct is**: `FUN_005443a0` — the UI text engine already documented above — is called
with `&DAT_00560728` (`Blaze.c:55881`), as is `FUN_00545340` (`Blaze.c:55882`). So these are the
descriptors that engine consumes.

**Field `0x04` is a packed `(x, y)` pair.** `FUN_00506880` walks a second array from
`&DAT_00560ff6` with a stride of `0x464` shorts = `0x8c8` bytes — the same stride — and reads
`psVar11[-1] + 0xc` and `*psVar11 + 4` as two screen coordinates. Those are the low and high
shorts of field `0x04`: `0x0028` and `0x0018`, i.e. `(0x28, 0x18)` → `(0x34, 0x1c)`. Field `0x08`
is plausibly the matching size pair (`0x48 × 0x18` for `DAT_00560ff0`, `0xE0 × 0x28` for
`DAT_00560728`) but that is a hypothesis, not confirmed.

The remaining fields are not interpreted. The reconstruction transcribes the raw constants rather
than naming them, since naming them would be inventing.

### The full `0x68`-byte header — all four static instances (2026-08-15)

The table above stops at `0x3C`; the initialised prefix is `0x68` bytes, i.e. **26 dwords**. Read
back from `Legacy/Binaries/Blaze.exe` at the four file offsets, they are:

| dword | offset | `DAT_00560728` @`0x15ED28` | `DAT_00560ff0` @`0x15F5F0` | `DAT_005618b8` @`0x15FEB8` | `DAT_00562180` @`0x160780` |
|---|---|---|---|---|---|
| f0  | `0x00` | `0x00000031` | `0x00000031` | `0x00000031` | `0x00000031` |
| f1  | `0x04` | `0x00A80028` | `0x00180028` | `0x00180078` | `0x001800D0` |
| f2  | `0x08` | `0x002800E0` | `0x00180048` | `0x00180050` | `0x00180038` |
| f3  | `0x0C` | `0x00000000` | `0x00000000` | `0x00000000` | `0x00000000` |
| f4  | `0x10` | `0x021A0000` | `0x01080000` | `0x01080000` | `0x021A0000` |
| f5  | `0x14` | `0x0000000C` | `0x00000000` | `0x00000001` | `0x00000002` |
| f6  | `0x18` | `0x010F0808` | `0x010F0808` | `0x010F0808` | `0x010F0808` |
| f7  | `0x1C` | `0x02CF1020` | `0x02CF1020` | `0x02CF1020` | `0x02CF1020` |
| f8  | `0x20` | `0x01070C10` | `0x01070C10` | `0x01070C10` | `0x01070C10` |
| f9  | `0x24` | `0x01000000` | `0x01000000` | `0x01000000` | `0x01000000` |
| f10 | `0x28` | `0x00000000` | `0x00000000` | `0x00000000` | `0x00000000` |
| f11 | `0x2C` | `0x01000000` | `0x01000000` | `0x01000000` | `0x01000000` |
| f12 | `0x30` | `0x000F28D4` | `0x000F00D0` | `0x000F00D0` | `0x000F00D0` |
| f13 | `0x34` | `0x01000000` | `0x01000000` | `0x01000000` | `0x01000000` |
| f14 | `0x38` | `0x000F0A00` | `0x000F0A00` | `0x000F0A00` | `0x000F0A00` |
| f15 | `0x3C` | `0x01000000` | `0x01000000` | `0x01000000` | `0x01000000` |
| f16 | `0x40` | `0x010F0000` | `0x010F0000` | `0x010F0000` | `0x010F0000` |
| f17 | `0x44` | `0x01000000` | `0x01000000` | `0x01000000` | `0x01000000` |
| f18, f20, f22, f24 | `0x48`..`0x60` | `0x00000000` | `0x00000000` | `0x00000000` | `0x00000000` |
| f19, f21, f23, f25 | `0x4C`..`0x64` | `0x01000000` | `0x01000000` | `0x01000000` | `0x01000000` |

`DAT_005618b8` and `DAT_00562180` are `DAT_00560ff0 + 1 * 0x8c8` and `+ 2 * 0x8c8`: the three form
the contiguous row the frame loop walks, and f5 is their index `0, 1, 2`. `FUN_00509470` confirms
the third by referencing `0x00562180` directly.

Note that the earlier table's `0x30`/`0x38` column for `DAT_00560ff0` read `0x000F00D0` and
`0x000F0A00`; both are confirmed here against the binary.

**The odd dwords are not padding.** Every second dword from f9 on is `0x01000000`, and every
feature test in `FUN_00545340` (`DrawUiPanel`) is `field & 0x1000000`. The header is therefore a
row of *(parameter, enable)* pairs:

| pair | what `FUN_00545340` draws from it | state in the four instances |
|---|---|---|
| f8 / f9   | text                | enabled, parameter `0x01070C10` |
| f10 / f11 | — (zero parameter)  | enabled, parameter zero |
| f12 / f13 | cursor              | enabled; parameter `0x000F…`, whose own `0x1000000` bit is clear |
| f14 / f15 | scroll arrows       | same shape as the cursor pair |
| f16 / f17 | frame               | enabled, parameter `0x010F0000` |
| f18..f25  | four unused pairs   | parameter zero, enable set |

Consequence for the reconstruction: an instance built with only the first eight dwords has f8 and
f16 zero and **draws nothing at all** — no text, no frame. That is the same silent failure already
recorded for `DAT_0056dd58`, the main menu's descriptor.

### These structures must be allocated below `0x01000000` (2026-08-15)

`FUN_005443a0` masks its descriptor argument with `& 0xffffff`, which is the identity in the
original because the whole image is based at `0x00400000`. That mask is only the visible half of
the constraint. The real reason is that **the descriptor carries its own ordering table at
`+0xa4`**: `FUN_005443a0` clears it with `ClearOTagR` (control code `0x0c` at the head of the
label stream), which writes every link as `address & 0xffffff`, and `FUN_00545340` later walks it
under the same mask until the `0xffffff` sentinel.

So any reconstruction that models one of these `.data` globals as a heap allocation has to place
it below `0x01000000`, or the internal ordering table relinks itself onto truncated addresses.
This applies to `DAT_00560728`, the `DAT_00560ff0` row and the nine instances at `DAT_00573518`.

It does **not** apply to the label tables they point at through `+0x8ac`: those are read back as
full 32-bit pointers (`Blaze.c:91094`, and `Blaze.cs`'s `RunTextEngine`), which is why
`DAT_0056e670` has always worked as an ordinary allocation.

## Two small symbols behind the name-entry screen (2026-08-15)

| symbol | file offset | bytes | reading |
|---|---|---|---|
| `DAT_00562c98` | `0x161298` | `5F 00` | the string `"_"`, appended to the name in progress while the grid cursor is off the `FIN` cell |
| `DAT_00562c9c` | `0x16129c` | `81 40` | Shift-JIS `0x8140`, the ideographic space; the code reads it as a dword, giving `0x4081` |

`FUN_00507cb0` compares a **byte** against `DAT_00562c9c & 0xffff`, which no byte can equal, so
that branch is unreachable as decompiled. Most likely a two-byte read that Ghidra narrowed.

## `DAT_00562a58` — eight scene tones (2026-08-15)

Eight entries of three shorts plus a padding word, at file offset `0x161058`, ending exactly where
`DAT_00562a98` begins — which is what fixes the count at eight. `FUN_00506880` damps the scene's
background colour toward the entry selected by the byte at `DAT_00573338 + 0x151`, but only at
sub-states 5 and 6.

```
[0] 192  96   0     [4] 192 192 192
[1]   0  96 192     [5]   0   0   0
[2] 128  96  64     [6] 128 128  96
[3]  64 128  96     [7]  64  32 128
```

## A `DAT_` cluster inside a known record is a field, not a global (2026-08-15)

Twice in one session a list of "globals still to model" dissolved on subtraction:

| cluster | actually |
|---|---|
| `DAT_00572b4c / 52 / 60 / 62 / 64..6c / 70..74` | `DAT_00572b48 + 0x04 / 0x0a / 0x18 / 0x1a / 0x1c..0x24 / 0x28..0x2c` — the 0x9c-byte entity record |
| `DAT_00578df0 / df4 / df8 / dfc / e00` | `DAT_00578de8[2..6]` |
| `DAT_0057351c / 1e / 20 / 22 / 28 / 38 / 3c` | fields of the first `0x8c8` descriptor at `DAT_00573518` |
| `DAT_0060492a` | `DAT_00604928 + 2` |

Subtracting the nearest already-declared base before declaring anything new is the cheap check,
and it is worth doing before writing "globals still to model" into a plan.

### Same trap, with a jump table: `PTR_FUN_0056afe8` and `PTR_FUN_0056aff0` (2026-08-22)

The world-map frame loop (`FUN_00516fc0`) calls two function pointers that look like isolated
globals. They are not. `0x0056afc8` is the fourth of the **seven entity jump tables**
`InstallEntityVTables` copies at area load, and the two "globals" are entries in it:

| `PTR_…` | index | value at area index 0 | in `Blaze.c`? |
|---|---:|---|---|
| `PTR_FUN_0056afe8` | `(0x0056afe8 − 0x0056afc8)/4` = **8** | `0x0053DCB0` | **no** |
| `PTR_FUN_0056aff0` | **10** | `0x0053DF90` | **no** |

Both are real functions, not the `0x00401FA0` stub, and both are missing from `Blaze.c` because
Ghidra does not decompile address-taken-only code. Dumps in `Legacy/Raw/Blaze/`.

The check that would have found this in seconds: **subtract the nearest declared table base**, the
same arithmetic as the record-field case above. A `PTR_FUN_` name is not evidence that the address
stands alone.

## The debug party-select screen's tables (2026-08-17)

Everything `FUN_00536270` (`RunDebugPartySelect`) reads out of `.data`, recovered with
`OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe`. None of it is inferred.

### `0x0056c894`..`0x0056c8d3` — the eight class names, 64 contiguous bytes

Padding is uneven, so the block is reproduced verbatim rather than as eight independent strings:

```
0x56c894  "Fairy\0\0\0"        0x56c8a8  "Hunter\0\0"
0x56c89c  "Dwarf\0\0\0"        0x56c8b0  "Sorcerer\0\0\0\0"
0x56c8a4  "Elf\0"              0x56c8bc  "Rogue\0\0\0"
                               0x56c8c4  "Priest\0\0"
                               0x56c8cc  "Fighter\0"
```

`PTR_s_Fighter_0056c7c0` is the eight pointers into it, in class-id order (the low three bits of the
identity byte at record `+0x150`):

| class | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---|---|---|---|---|---|---|---|---|
| name | Fighter | Priest | Rogue | Sorcerer | Hunter | Elf | Dwarf | Fairy |
| offset from `0x56c894` | 0x38 | 0x30 | 0x28 | 0x1c | 0x14 | 0x10 | 0x08 | 0x00 |

### `0x0056c8d4`..`0x0056c96b` — sixteen default character names, then three labels

Sixteen 8-byte records in **descending** identity order (`0x56c8d4` is identity 15), immediately
followed by the screen's three labels. `PTR_DAT_0056c780` holds the sixteen pointers.

| identity | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |
|---|---|---|---|---|---|---|---|---|
| male | Elo | Hansi | Hugo | Werner | Willi | Egon | Matze | Tommi |

| identity | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 |
|---|---|---|---|---|---|---|---|---|
| female | Bella | Süßi | Mimi | Nicola | Tussi | Erna | Dora | Dina |

Identity 9 is `53 FC DF 69` — "Süßi" in Latin-1. The names are German throughout.

The three labels, at fixed offsets from `0x56c8d4`:

| offset | address | string |
|---|---|---|
| `+0x7c` | `0x56c950` | `"BUFFER OVER"` |
| `+0x88` | `0x56c95c` | `"FEMALE"` |
| `+0x90` | `0x56c964` | `"MALE"` |

**The name copy reads sixteen bytes out of eight-byte records.** For identity 0 that means
`"Elo\0"` followed by the first twelve bytes of `"BUFFER OVER\0"`. Harmless (every consumer stops at
the first NUL) but the reason the block must be reproduced contiguously rather than string by string.

### `0x0056c7e0` — eight rows of eight bytes, the per-class starting stats

Bytes 0..6 go to record `+0x280` *and* `+0x2a0` (the staged/current pair); byte 7 is the element,
and lands in `+0x151`.

```
class 0 Fighter   18 08 08 10 10 08 10 | 00
class 1 Priest    08 10 18 08 08 10 10 | 06
class 2 Rogue     08 08 08 18 10 08 18 | 05
class 3 Sorcerer  08 10 10 08 08 18 10 | 04
class 4 Hunter    10 08 08 14 14 08 10 | 03
class 5 Elf       08 14 10 08 08 14 10 | 01
class 6 Dwarf     14 08 08 10 14 08 10 | 02
class 7 Fairy     08 14 14 08 08 10 10 | 07
```

### `0x0056c820` / `0x0056c828` / `0x0056c830` — duplicates of an already-known set

Byte for byte identical to `s_ClassStartIcon_00562ab8` / `s_StatBitRemap_00562ac0` /
`s_StatBitTable_00562ac8`, which serve the same computation in `FinalizeCharacterStats`. The binary
carries both sets at two addresses. Declared separately on the C# side so the Ghidra-address
traceability survives — if a patch ever makes them diverge, that will be visible.

```
0x56c820  00 01 00 00 03 00 00 02                         (per class)
0x56c828  01 02 03 06 05 04 07 08                         (per class, a remap)
0x56c830  0001 8000 4000 2000 1000 0800 0400 0200 0100 0000 0000 0000   (ushorts)
```

### `0x0056c848` / `0x0056c858` — sixteen bytes each, indexed by the full identity

Gender included, unlike the tables above. They feed record `+0x153` and `+0x152` — the two fields
character-creation sub-state 3 composes by hand.

```
0x56c848  08 02 00 04 0a 02 04 06 09 03 0f 0d 01 03 05 07   -> +0x153
0x56c858  0c 0f 09 02 0d 0e 04 0f 00 0f 0e 10 0b 0d 04 0f   -> +0x152
```

### Small ones

| address | bytes | meaning |
|---|---|---|
| `0x00564a2c` | `3E 00 00 00` | the string `">"` — this screen's selection cursor |
| `0x00564a40` | `47 75 72 6b 68 61 00 00` | **`"Gurkha"`** — the cheat sequence `CheatCodeProbe` matches (see below) |
| `0x005633a8` | `01 00 00 00` | `DAT_005633a8` at load: **1**. Width confirmed by `0x00536218 CMP dword [0x5633a8],EBX` |
| `0x00566c84`..`0x94` | `ff`, `ff`, `ff`, `01`, `01` (4 bytes apart) | exactly what `FUN_00536270` writes back — a reset to load state, not a configuration |
| `0x00566c28` | `00 30 80 40 / 00 00 30 02` | the shared 8x8 glyph atlas descriptor (origin Y=0x30, cell 8x8; attributes `0x02300000`) |
| `0x0056315c` | `60 31 56 00` | a pointer to `s_SaveDeviceName_00563160` |
| `0x00563158` | `70 31 56 00` | a pointer to `s_SaveFolderName_00563170` — what `FUN_005128e0` dereferences |
| `0x00566bc0` | `98 84 56 00` | a pointer to the save-filename template at `0x00568498` — see below |
| `0x00568498` | `"Blaze-Saving        "` | the save-filename template: 12-byte literal prefix + eight spaces + NUL |

`"Gurkha"` sits immediately after the three debug labels `"DPT"`/`"LOC"`/`"ANG"` at `0x00564a20`,
which is corroborating: the whole neighbourhood is developer material.

### `0x00566bc0` is a pointer, and it settles the save-filename question (2026-08-18)

`FUN_005098f0`'s finalisation writes the seven-character character code through
`pcRam00566bc0[0xc..0x12]`, then copies **from byte 0** into `DAT_00573338+0x1fd0`, the buffer
`SetCardFileName` turns into the save path. A previous session read the bytes *at* `0x00566bc0`,
found a pointer-table shape rather than a string, and correctly refused to use them.

The missing step was one more dereference. All eight `.text` sites that touch the address load it
(`8B 35 / 8B 0D / 8B 15  C0 6B 56 00` = `MOV reg,[0x566BC0]`) before writing through it
(`88 54 30 0C` = `MOV [EAX+ESI+0xC],DL`), and **none of them ever writes the address itself** — no
`A3 C0 6B 56 00`, no `89 xx C0 6B 56 00` anywhere in `.text` — so the pointer keeps its load-time
value for the whole run. It points at `0x00568498`, `"Blaze-Saving"` followed by eight spaces: the
first twelve bytes are the literal prefix the copy picks up, and the seven code characters land in
the spaces. Hence the shape of real 1998 save filenames, `Blaze-Saving@MVB@R_` = 12 + 7.

The neighbourhood corroborates it: `0x00566bb8` and `0x00566bbc` hold the pointers to
`"SYSTEM.INI"` and `"BLAZE.ALL;1"`, so `0x00566bc0` is simply the next entry of the same
filename-pointer table.

## The save-load screen's tables (2026-08-17)

Read while analysing `FUN_00536ea0` — the party-constitution screen, **transcribed 2026-08-18** as
`RunPartyConstitution`. Every table below is now wired to a call site.

| address | contents |
|---|---|
| `0x00563148` | the row sprite's 2-dword texture record: `0x10102000`, `0x003e0000`. The `0x3e` selects table entry `0x5e8f00 + 0x3e*4 = 0x5e8ff8`, which the same code writes just before drawing |
| `0x005630c4` | `"%d"` — the format `FUN_00547960` (the CRT `sprintf`) is called with. The one call site can therefore be written as a plain decimal format, with no variadic engine |
| `0x00566b8c` | `"--------"` (eight dashes) — the placeholder row for a save file that fails to open |
| `0x0056c868` | the class abbreviations, stride 5: `Gue.` `Pre.` `Vol.` `Mag.` `Cha.` `Elf ` `Nai.` `Lut.` (French: Guerrier, Prêtre, Voleur, Magicien, Chasseur, Elfe, Nain, Lutin) |
| `0x0056c890` | `'N'` — the `Niveau` prefix printed before the level number |

### `0x0056b558` and `0x0056be20` — two more `0x8c8` UI descriptors

Contiguous (`0x56b558 + 0x8c8 = 0x56be20`), and `FUN_00536ea0`'s render loop bounds itself at
`0x56c6e8 = 0x56b558 + 2 * 0x8c8`, which settles the count at two. First 26 dwords initialised, tail
zero — the same shape as `DAT_00560ff0` and `DAT_00560728`.

```
0x56b558  00000031 00a80028 002800e0 00000000  021a0000 0000000c 010f0808 02cf1020
          01070c10 01000000 00000000 01000000  000f28d4 01000000 000f0c00 01000000
          010f0000 01000000 00000000 01000000  00000000 01000000 00000000 01000000
          00000000 01000000

0x56be20  00000031 00100028 009000e0 00000000  081a0000 00000004 010f0808 02cf1020
          01071018 01000000 00000000 01000000  00000000 01000000 00000000 01000000
          010f0000 01000000 00000000 01000000  00000000 01000000 00000000 01000000
          00000000 01000000
```

**Eight more `DAT_` clusters dissolve on subtraction** — the same check as the 2026-08-15 entry
above, applied once the descriptor size is known:

| cluster | actually |
|---|---|
| `DAT_0056b58b` / `DAT_0056b593` | `0x56b558 + 0x33` / `+ 0x3b` |
| `DAT_0056be1c` / `1d` / `1f` | `0x56b558 + 0x8c4` / `+ 0x8c5` / `+ 0x8c7` |
| `DAT_0056be5b` | `0x56be20 + 0x3b` |
| `DAT_0056c6e4` / `e5` / `e7` | `0x56be20 + 0x8c4` / `+ 0x8c5` / `+ 0x8c7` |

Four fields per descriptor, at the offsets already named on the other instances
(`s_Unknown_00560763` = `+0x3b`, `s_MenuSelection_0056e61c` = `+0x8c4`, `s_Unknown_0056e61f` =
`+0x8c7`). The symmetry is what makes it certain.

## Le sous-écran « Invertaire » du menu objets (`FUN_00524880`) — relevé le 2026-08-18

| VA | contenu | rôle |
|---|---|---|
| `0x00562C9C` | `0x00004081` (dword) | comparé au glyphe de la case de clavier ; voir `legacy-bug.md` LB-005 |
| `0x00564A0C` | `"/"` | le séparateur du compteur « n / 403 » |
| `0x00564A10` | `0C 0F 00 00` | deux codes de contrôle : début de page, puis attente |
| `0x00566170` | en-tête de descripteur, 26 dwords | **troisième** descripteur d'interface du menu objets ; `+4` = `0x00640014` |
| `0x00566A38` | `00 04 00 00 …` | identifiants d'emplacement de carte, branchement 0 |
| `0x00566A40` | `00 01 02 03 04` | idem, branchement 1 |
| `0x00566A48` | `00 04 05 06 07` | idem, branchement 2 |
| `0x00566A50` | `00 01 02 03 04 05 06 07` | idem, branchement 3 |
| `0x00566A58` | 4 pointeurs → `0x566a38/40/48/50` | `PTR_DAT_00566a58` |
| `0x00566A68` | `02 00 01 …` | liste d'emplacements, branchement 0 ; **le premier octet est le compte** |
| `0x00566A70` | `05 02 03 04 05 01` | idem, branchement 1 |
| `0x00566A78` | `05 00 06 07 08 09` | idem, branchement 2 |
| `0x00566A80` | `08 02 03 04 05 06 07 08 09` | idem, branchement 3 |
| `0x00566A90` | 4 pointeurs → `0x566a68/70/78/80` | `PTR_DAT_00566a90` |
| `0x00566AC8` | `FF FF FF FF FF FF FF FF` | `DAT_00566ac8` / `DAT_00566acc`, tous deux à −1 au chargement |
| `0x00566AD0` | `02 01 00 03 04 05 06 07` | l'ordre d'affichage des huit caractéristiques d'un objet |
| `0x00566AD8` | 10 quadruplets + dword nul | la rangée d'icônes du cadre : `{descripteur, variante, dx, dy}` |
| `0x00566B08` | 8 × 8 octets | les enregistrements de sprite des huit éléments (`80/90/a0/…/f0 E0 10 08 00 00 30 00`) |
| `0x00566B48` | `00 00 18 18 00 00 3E 00` | l'enregistrement de sprite d'une icône d'objet ; ses deux premiers octets sont réécrits à chaque tracé |
| `0x00566B80` | `"???"` | ce qu'affiche une caractéristique d'objet non identifié |
| `0x00566B84` | `"????"` | ce qu'affiche le nom d'un objet non identifié |

## L'état de jeu 3, la carte du monde (`FUN_00516020`) — relevé le 2026-08-18

| VA | contenu | rôle |
|---|---|---|
| `0x0055EB68` | 32 pointeurs → `0x005037d0`, `0x005037e0`, `0x00503850`, `0x005038a0`, `0x00503990`, `0x00503b50`, `0x00503d30`, `0x00503d70`, `0x00503d80`, `0x00503d90`, `0x00503db0`, `0x00503dd0`, `0x00503df0`, `0x00503e10`, `0x00503e50`, `0x00503e80`, `0x00503f10`, `0x00503fa0`, `0x00503fd0`, `0x00504020`, `0x00504070`, `0x005040a0`, `0x00504110`, `0x00504170`, `0x005041a0`, `0x005041d0`, `0x00504200`, `0x00504270`, `0x00504290`, `0x005042b0`, `0x00504310`, `0x005043c0` ; puis `0x00504c80`, puis un dword nul | `PTR_LAB_0055eb68` — les verbes de la caméra de scénette, tous **absents de `Blaze.c`**, transcrits le 2026-08-23 dans `Screens/State03/WorldMapTick.cs`. `0x00504c80` **n'est pas un verbe** : son corps ne suit pas la convention `(curseur, &continuer)` |
| `0x00564F80` | 23 dwords : `00523B00 00523BB0 00523BD0 00523C00 00523C30 00523C70 00523CB0 00523D20 00523D60 00523DD0 00523E10 00523E50 00524060 00523E80 00523ED0 00523F00 00523F10 00523F80 00523FC0 00524060 005240D0 00524140 00524170`, puis un dword nul | `PTR_LAB_00564f80` — **les gestionnaires de la machine a opcodes des objets** (`FUN_00523aa0`). 23 entrees, **22 corps distincts** : 12 et 19 partagent `0x00524060`. **Onze sont absents de `Blaze.c`** — Ghidra ne les a vus que pris en adresse. Etendue deduite du dword nul qui suit |
| `0x0052403C` | `00 00 01 01 01 02 02 03 03 03 03 04 05 05 05 06 06 07 07 07 07 08` | `DAT_0052403c` — la **famille de l'evenement d'objet**, indexee par `item[0x84]` (0 a 0x15). Lue par l'opcode 18, qui la reduit ensuite a un seul bit par la table de sauts `0x00524014` : 1 pour les familles impaires, 0 sinon |
| `0x00524014` | 9 dwords : `00523FE9 00523FE2` en alternance, puis `00523FE9 00523FE9` | la table de sauts que l'opcode 18 emprunte apres `DAT_0052403c`. Ses neuf bras ne portent que deux valeurs, `eax = 0` (`0x00523FE9`) et `eax = 1` (`0x00523FE2`) — un `switch` etale sur ce qui tient en une comparaison de parite |
| `0x0056D4E0` | 48 dwords, de `0x0053AFB0` a `0x0053CDF0` ; le dword suivant vaut `1` | `DAT_0056d4e0` — **la table des 48 effets**, celle que l'opcode 18 du script d'objet appelle et que les sorts empruntent aussi. Etendue **deduite** : ce qui suit est une table de masques de bits (`1, 2, 4, 8, …`), pas des adresses. Non transcrite |
| `0x0056A850` | 9 rangées de 9 octets : `07 / 00 01 02 03 04 05 09 08` · `07 / 00 01 03 04 05 06 09 08` · `06 / 00 01 03 04 05 09 08 FF` · `07 / 00 01 03 04 05 06 09 08` · `07 / 00 01 03 04 05 07 09 08` · `07 / 00 01 03 04 05 06 09 08` · `07 / 00 01 02 03 04 05 09 08` · `07 / 00 01 03 04 05 06 09 08` · `04 / 00 01 02 05 FF 05 00 01` | `DAT_0056a850` + `DAT_0056a851` — **la disposition du menu rapide**, une rangée par classe (`perso[0x150] & 7`). Premier octet : le **nombre de catégories**. Les huit suivants : le **genre** de chaque catégorie (0–5 les cinq emplacements d'équipement, 6 les sorts, 7 et 8 deux choix fixes, 9 le passage de la main). Une seule allocation dans la reconstruction, `s_MenuLayout_0056a850` : les deux symboles de Ghidra sont une seule table, décalée d'un octet |
| `0x00564600` | `1d 18 14 01 01 01 26 00` | `DAT_00564600` — **le nombre de sorts par école**, indexé `perso[0x2b5]`. **Trois écoles** seulement : 29, 24, 20. La preuve est double — `PTR_DAT_00564640` porte trois pointeurs puis un mot nul, et le voisin `DAT_00564608` (`00 1d 35 49`) est la somme courante de ces trois-là. Ce qui suit dans la rangée n'est donc pas un compte de sorts |
| `0x0056AAA0` | 16 dwords : `08000000 06001200 04000500 03000300 05000800 06000900 06000B00 06001200 06000400 06000900 03000000 06001200 05000800 07000500 06000400 06000900` | `DAT_0056aaa0` — une **réaction par classe de personnage** (paire animation / image de départ), écrite d'un bloc sur `+0x18` de l'emplacement. Fin relevée : `0x0056AAE0` change de nature et est déjà un autre symbole |
| `0x0056AAE0` | 33 entrées de 8 octets, `u v w h 00 00 clut clut`, jusqu'à `0x0056ABE8` où commence une chaîne | `DAT_0056aae0` — les descripteurs de sprite de la surimpression. **Contiguë à `0x0056AAA0` et il faut le rester** : l’index `-1` de `DAT_0056a851` lirait les huit derniers octets de la table des réactions, dont la hauteur nulle vaut « pas d’icône ». Cet index `-1` est **inatteignable** (2026-08-26) — l’octet de compte de `0x0056A850` borne l’indice avant lui — mais l’adjacence décrit l’indexation du binaire et se garde. Entrées nommées : 10 = `0x0056AB30` (le cadre), 11 = `0x0056AB38` (les chiffres), 18 = `0x0056AB70` (le repère 8x8) |
| `0x0056D440` | 16 entrées de 8 octets : `2E00 8200 1E00 0000 / 2800 7F00 1800 0000 / 2600 7D00 1A00 0000 / 2100 7C00 2000 0000 / 2600 8200 1A00 0000 / 2800 7D00 1E00 0000 / 4000 7600 2800 0000 / 1E00 7100 1400 0000 / 2600 7D00 1900 0000 / 1E00 7B00 1C00 0000 / 1C00 7500 1800 0000 / 1F00 7800 1800 0000 / 2400 7800 1800 0000 / 2400 7800 1A00 0000 / 3200 7200 2800 0000 / 1E00 6E00 1400 0000` | `DAT_0056d440` — une entrée par **classe de personnage** : la **boîte du corps**. `+0` et `+4` sont ses deux demi-étendues au sol (établi le 2026-08-28 par `FUN_00535a90`, le test de portée), `+2` la hauteur de la tête où flotte la bulle d’état, `+6` nul partout. Le **même enregistrement** vit à `stat + 0x58` pour une entité de scène — `FUN_00538c10` rend l’un ou l’autre. Fin relevée : `0x0056D4C0` commence une table de pointeurs |
bloc
sur
de
lemplacement. Fin relevée : `0x0056AAE0` change de nature et est déjà un autre symbole |
| `0x005633b0` | en-tête de descripteur, 26 dwords ; `+4` = `0xFFB00030` | le descripteur d'interface de la carte du monde |
| `0x005645F8` | `02 00 00 00 FF 00 00 00 …` | `DAT_005645f8` = 2 : le pas de boucle ; `DAT_005645fc` = 0xff |
| `0x00564650` | `7C 01 00 00 00 00 00 00 00 00 00 00 50 00 00 00 80 02 00 00` | six champs de caméra remis à neuf à chaque tour |
| `0x00566C10` | `00 00 C0 01 00 01 10 00` | le rectangle VRAM d'envoi par image : (0, 0x1c0) 0x100×0x10 |
| `0x00566D68` | 11 pointeurs → `0x55e8a8`, `0x55e8e8`, … `0x55eb28`, puis un dword nul | `PTR_PTR_00566d68` : une table de sauts par zone, espacées de 0x40 |
| `0x0056B4F0` | quatre dwords, tous `0x00401FA0` | `PTR_FUN_0056b4f0` — l'entrée 8 de `PTR_FUN_0056b4d0` ; `FUN_00401fa0` a un **corps vide** |
| `0x00604928` | (queue non initialisée de `.data`) | zéro au chargement, écrit par le code seulement |
| `0x00604930` | (queue non initialisée) | posé à **2** par `RunApplication` — une sentinelle de destination, pas un pointeur |
| `0x006008C8` / `0x00600904` | (queue non initialisée) | posés à **1** et **3** par `RunApplication`, mêmes sentinelles |

---

## L'onglet 5 du menu de jeu, la configuration de la manette — relevé le 2026-08-25

Le corps est le `case 5` de `FUN_0052a850` (0x0052cf0a..0x0052d2b4). Ses quatre tables occupent
`0x0056aa18`..`0x0056aa9f` et se referment des deux côtés sans qu'on ait à deviner : en dessous,
`0x0056aa00`..`0x0056aa17` est nul dans le binaire ; au-dessus, `0x0056aaa0` est déjà la table des
réactions par classe. Aucune n'est indexée au-delà de sa propre fin.

| VA | contenu | rôle |
|---|---|---|
| `0x0056AA18` | `04 00 08 04` | le décalage horizontal des trois cadres de rangée, ajouté à `0x60`. Quatre octets, trois lus |
| `0x0056AA20` | 4 pointeurs → `0x56ac18`, `0x56ac10`, `0x56ac08`, `0x56ac00` | `PTR_s_Type_A_0056aa20` — les noms de type de manette, A, B, C, D. **Le quatrième n'est jamais indexé** : le code borne le choix à 2 |
| `0x0056AA30` | 2 pointeurs → `0x56abf4`, `0x56abe8` | `PTR_DAT_0056aa30` — les étiquettes des deux bascules : 0 = « ␣␣␣Activé », 1 = « Désactivé » |
| `0x0056AA38` | 24 entrées de 4 octets | les **vingt-quatre permutations de {0,1,2,3}** — exactement les 24 arrangements, ce qui confirme la borne `0x17` du code sans la deviner. Chaque octet indexe `0x0056AA98` |
| `0x0056AA98` | `00 10 00 20 00 40 00 80` | les quatre masques de bouton d'action : `0x1000`, `0x2000`, `0x4000`, `0x8000` |
| `0x0056ABE8` | `"Désactivé"`, cp1252, 12 octets | la borne haute de `DAT_0056aae0`, déjà relevée comme « où commence une chaîne » |
| `0x0056ABF4` | `"   Activé"` — **trois espaces de tête**, 12 octets | c'est ce qui aligne la colonne des valeurs sur « Type A », qui n'en a pas besoin |
| `0x0056AC00` / `08` / `10` / `18` | `"Type D"` / `"Type C"` / `"Type B"` / `"Type A"` | 8 octets chacun ; le bloc se termine à `0x0056AC20`, où commence « Sauvegar… » |

Les tables de permutation et de masques n'appartiennent qu'au `case 3` du saut indexé de
`0x0052fa4c`, et **ce cas est inatteignable** : le curseur de rangée est ramené dans 0..2 quelques
instructions avant le saut. C'est le remappage des quatre boutons d'action, écrit dans le personnage
à `+0x1fc0`, `+0x1fc2`, `+0x1fc4` et — le quatrième saute deux mots — `+0x1fca`.

---

## Les onglets 3 et 4 de la fiche — relevé le 2026-08-25

L'onglet des sorts et celui des herbes. Trois écoles, six pages chacune ; le compte d'écoles n'est
pas deviné, c'est le **quatrième pointeur nul** de `0x00564640` qui le donne.

| VA | contenu | rôle |
|---|---|---|
| `0x00564608` | `00 1d 35 49` | `DAT_00564608` — le rang du premier libellé de chaque école dans la table de ressources ; la quatrième valeur borne la troisième |
| `0x00564610` | `00 05 05 05 0A 05 0F 05 14 06 1A 03` puis quatre octets nuls | école 0 : six couples (premier, compte), un par page |
| `0x00564620` | `00 05 05 06 0B 05 10 03 13 03 16 02` + nuls | école 1 |
| `0x00564630` | `00 03 03 04 07 02 09 03 0C 04 10 04` + nuls | école 2 |
| `0x00564640` | 3 pointeurs → `0x564610/20/30`, puis **un dword nul** | `PTR_DAT_00564640` |
| `0x0056A978` | `0x1C2C2402 0x1C680502 0x50342502 0x74442602 0x00000000` | les quatre décorations de l'onglet 3, ancrées sur le descripteur 2 ; la plage se referme sur `0x0056A98C` |

Le bloc de huit octets `+0x160 + école * 8` du personnage est lu **en bits** par l'onglet 3 (l'état
« appris » de chaque sort) et **en octets** par l'onglet 4 (le compte de chaque herbe). C'est la
classe qui décide, et `s_ClassTabs_0056a898` fait qu'un personnage n'a jamais les deux onglets.

---

## La page 5 du menu de jeu, « Options » — relevé le 2026-08-25

Les emplacements de carte mémoire, `0x0056a98c`..`0x0056aa18`. La plage se referme par le haut sur
`s_ConfigRowOffsets_0056aa18`, relevée le matin même pour l'onglet 5 : dix entrées de quatre octets
tombent exactement entre les deux bornes.

| VA | contenu | rôle |
|---|---|---|
| `0x0056A98C` | `00 04` | l'identifiant de port des emplacements, sans multitap |
| `0x0056A990` | `00 01 02 03 04` | idem, un multitap sur le port 1 |
| `0x0056A998` | `00 04 05 06 07` | idem, un multitap sur le port 2 |
| `0x0056A9A0` | `00 01 02 03 04 05 06 07` | idem, deux multitaps |
| `0x0056A9A8` | 4 pointeurs → `0x56a98c/90/98/a0` | `PTR_DAT_0056a9a8`, indexé par `DAT_005de6f4` |
| `0x0056A9B8` | `02 00 01` | **le premier octet est le compte** : deux emplacements, aux positions 0 et 1 |
| `0x0056A9C0` | `05 02 03 04 05 01` | cinq emplacements |
| `0x0056A9C8` | `05 00 06 07 08 09` | cinq emplacements, l'autre port |
| `0x0056A9D0` | `08 02 03 04 05 06 07 08 09` | huit emplacements |
| `0x0056A9E0` | 4 pointeurs → `0x56a9b8/c0/c8/d0` | `PTR_DAT_0056a9e0` |
| `0x0056A9F0` | 10 couples de `short` : `(0x11,0x2c)`, `(0x1c,0x2c)`, `(-0x11,0x43)`, `(-0x0a,0x49)`, `(0x04,0x49)`, `(0x0b,0x43)`, `(0x23,0x43)`, `(0x2a,0x49)`, `(0x38,0x49)`, `(0x3f,0x43)` | les positions à l'écran, relatives à `(0x8c, 0x48)` |
| `0x0056AC20` | `"Sauvegarde"` | la chaîne du petit menu, écrite en dur ; « Quitter » est à `0x00566B64`, partagée avec le menu objets |
| `0x0056AC2C` | `0x00006B82` | `DAT_0056ac2c` — le **mot sentinelle** qui marque la fin du titre d'un fichier de sauvegarde ; seul le mot bas est lu |

`DAT_005de6f4` vaut 0 à 3 selon que `DAT_00604b0a` et `DAT_00604b2e` lisent la sentinelle `-0x8000`
— la même dérivation que `Input.ControllerRemapIndex`, sur une table différente.

---

## Carte des symboles reconstruits — où chaque adresse vit dans le port

Ajoutée le **2026-08-19**, au découpage de `Blaze.cs` (`openspec/changes/split-blaze-into-subsystems`).

Les deux tables de ce fichier ont **la même clé, l'adresse virtuelle Ghidra**, et se lisent
**ensemble** :

| | répond à |
|---|---|
| les sections précédentes | *que contient le binaire à cette adresse ?* |
| celle-ci | *où l'avons-nous modélisée, et qui est son voisin ?* |

### À quoi elle sert : le contrôle d'adjacence

Le défaut d'aliasing a été corrigé **seize fois** dans ce dépôt. Il naît toujours du même geste —
séparer ce que le binaire garde ensemble. Deux symboles voisins d'adresse déclarés séparément ne
partagent plus leur mémoire ; une écriture faite par un bout n'est jamais lue par l'autre. Aucune
erreur, aucun plantage : une valeur qui reste à zéro.

Avant de déclarer un `DAT_` isolé, **chercher son adresse ici** :

- s'il tombe dans l'étendue d'une arène (colonne « fichier » en `Memory/`), il est déjà couvert —
  l'adresser par l'accesseur de l'arène, pas par un champ neuf ;
- s'il a un voisin à quelques octets, vérifier que la taille déclarée du voisin ne le recouvre pas ;
- s'il n'a pas de voisin proche, un champ isolé est légitime.

### Pourquoi elle est nécessaire depuis le découpage

Mesuré au moment du découpage : **398 déclarateurs sur 375 adresses**, dont **187 ont un voisin à
4 octets ou moins**, et surtout —

> **107 paires de symboles adjacents (écart ≤ 64 octets) atterrissent dans deux fichiers
> différents.**

Tant que tout vivait dans `Blaze.cs`, cette adjacence se voyait en faisant défiler le fichier.
Elle ne se voit plus. Cette table est ce qui la rend encore consultable.

### Les blocs adressés par décalage

Huit blocs sont modélisés comme une allocation unique dont les symboles sont des décalages. Un
symbole qui tombe dans l'une de ces étendues **ne doit jamais recevoir sa propre allocation**.

| Base | Taille | Nom | Accesseur | Fichier |
|---|---|---|---|---|
| `0x00551640` | `0x200` | EntityParams | `EntityParam(va)` | `Memory/GameMemory.cs` |
| `0x00571270` | `0x190` | ViewParams | `ViewParams(va)` | `Memory/GameMemory.cs` |
| `0x00571500` | `0x538` | EntityState | `EntityState(va)` / `EntityStateBytes(va)` | `Memory/GameMemory.cs` |
| `0x005de030` | `0x084` | CameraBlock | `Camera(va)` | `Memory/GameMemory.cs` |
| `0x005de100` | `0x410` | ItemMenuArena | `ItemMenuState(va)` / `ItemMenuBytes(va)` | `Memory/GameMemory.cs` |
| `0x005e85c0` | `0x340` | PartyList | — | `Memory/GameMemory.cs` |
| `0x005e9000` | `0x1600` | GameArena | `Arena(va)` | `Memory/GameMemory.cs` |
| `0x005eca00` | `0x14200` | EntityArena | `Entity(va)` | `Memory/GameMemory.cs` |
| `0x006067c0` | `0x040` | ViewBlock | `View(va)` | `Memory/GameMemory.cs` |

**Mis à jour le 2026-08-21** : `ViewParams` ajoutée (étape 1 du plan « atteindre le prologue »),
`EntityState` étendue de `0x500` à `0x538` pour `FUN_005035c0`, et `ItemMenuArena` ramenée de
`0x460` à `0x410`. Trois symboles simples se sont ajoutés hors arène le même jour, tous nuls au
chargement : `s_WorldFlags_005de02c` (`Memory/GameMemory.cs`, quatre octets juste avant
`CameraBlock`), `s_CutsceneActive_00604b78` et `s_SavedViewMode_00604b7c` (`Engine/SaveData.cs`,
immédiatement après `s_CardWriteEventHandles_00604b68` qui s'arrête pile à `0x00604b78`).

**Avertissement sur cette table** : elle ne recense que les étendues d'arène. Un `static int`
ordinaire dont l'adresse tombe dans l'une d'elles n'y apparaît pas — c'est le cas de
`s_PartySize_005e9020` (`Screens/State04/SaveSlots.cs`), qui double `GameArena`. Lire `0x005e9020`
par `Arena(va)` rend donc zéro en silence. Avant d'écrire `Arena(0x005e9…)` pour une adresse
nouvelle, chercher un champ nommé à cette VA.

> **Depuis le 2026-08-22, le test le voit.** `OpenForesia.Tests`, test 5
> (`Aucun_champ_simple_dans_l_etendue_d_une_arene`) liste ces champs par nom, fichier et ligne :
> `dotnet test OpenForesia.Tests`. Il en compte **42** et il est rouge exprès. C'est devenu le moyen
> le plus rapide de faire la vérification recommandée au paragraphe ci-dessus.

**Quatre allocations indépendantes tombent dans l'étendue d'une arène** — constatées au découpage,
**non corrigées** parce que les corriger changerait le comportement :

| Symbole | Adresse | Arène qui la couvre |
|---|---|---|
| `s_EntityList_005de558` | `0x005de558` | `ItemMenuArena` |
| `s_ModelResultB_005f7b40` | `0x005f7b40` | `EntityArena` |
| `s_ModelResultA_005fa588` | `0x005fa588` | `EntityArena` |
| `s_CameraMatrix_006008e0` | `0x006008e0` | `EntityArena` |

Les trois dernières sont dans le voisinage que la carte du monde doit lire (`DAT_005f7bc0`,
`DAT_005f7cc0`, `DAT_005fa508`) : à traiter avant de reprendre `FUN_00533940`.

### L'index

La colonne **écart** est la distance jusqu'au symbole d'adresse suivant. Un écart plus petit que la
taille déclarée du symbole est un recouvrement, donc un défaut.

| VA | symbole C# | écart | fichier |
|---|---|---:|---|
| `0x0054d000` | `s_GammaScale_0054d000` | 116 | `Engine/Input.cs` |
| `0x0054d074` | `s_GammaScale_0054d074` | 17868 | `Screens/State00/MainMenu.cs` |
| `0x00551640` | `s_EntityParams_00551640` | 7240 | `Memory/GameMemory.cs` |
| `0x00553288` | `s_VtBlock` — les 197 blocs de sauts d'entites, un pool contigu | 46624 | `Engine/EntityVTables.cs` |
| `0x0055e8a8` | `s_AreaHandlerSource_0055e8a8` | 7760 | `Engine/Resources.cs` |
| `0x005606f8` | `s_Unknown_005606f8` | 48 | `Memory/GameMemory.cs` |
| `0x00560728` | `s_StatusDescriptor_00560728` | 51 | `Memory/GameMemory.cs` |
| `0x0056075b` | `s_Unknown_0056075b` | 8 | `Memory/GameMemory.cs` |
| `0x00560763` | `s_Unknown_00560763` | 2161 | `Memory/GameMemory.cs` |
| `0x00560fd4` | `s_Unknown_00560fd4` | 27 | `Screens/State04/SaveSlots.cs` |
| `0x00560fef` | `s_Unknown_00560fef` | 1 | `Memory/GameMemory.cs` |
| `0x00560ff0` | `s_UiDescriptors_00560ff0` | 6760 | `Memory/GameMemory.cs` |
| `0x00562a58` | `s_SceneToneTable_00562a58` | 64 | `Memory/GameMemory.cs` |
| `0x00562a98` | `s_SelectSubStateBytes_00562a98` | 32 | `Memory/GameMemory.cs` |
| `0x00562ab8` | `s_ClassStartIcon_00562ab8` | 8 | `Screens/State04/CharacterCreation.cs` |
| `0x00562ac0` | `s_StatBitRemap_00562ac0` | 8 | `Screens/State04/CharacterCreation.cs` |
| `0x00562ac8` | `s_StatBitTable_00562ac8` | 20 | `Screens/State04/CharacterCreation.cs` |
| `0x00562adc` | `s_CardSlotFlags_00562adc` | 44 | `Screens/State04/CharacterCreation.cs` |
| `0x00562b08` | `s_CardDirectorySize_00562b08` | 74 | `Screens/State04/CharacterCreation.cs` |
| `0x00562b52` | `s_NameEncodeTable_00562b52` | 22 | `Engine/SaveData.cs` |
| `0x00562b68` | `s_Unknown_00562b68` | 8 | `Memory/GameMemory.cs` |
| `0x00562b70` | `s_ClassDefault_00562b70` | 104 | `Screens/State04/CharacterCreation.cs` |
| `0x00562bd8` | `s_AccentSourceTable_00562bd8` | 64 | `Engine/SaveData.cs` |
| `0x00562c18` | `s_AccentTargetTable_00562c18` | 132 | `Engine/SaveData.cs` |
| `0x00562c9c` | `s_IdeographicSpace_00562c9c` | **0** | `Screens/State04/SaveSlots.cs` |
| `0x00562c9c` | `s_Unknown_00562c9c` | 380 | `Screens/State06/Inventory.cs` |
| `0x00562e18` | `s_KeyScancodes_00562e18` | 88 | `Screens/State00/Options.cs` |
| `0x00562e70` | `s_JoyButtonScan_00562e70` | 40 | `Screens/State00/Options.cs` |
| `0x00562e98` | `s_JoyButtonLabelStorage_00562e98` | 1 | `Screens/State00/Options.cs` |
| `0x00562e99` | `s_JoyButtonLabel_00562e99` | 39 | `Screens/State00/Options.cs` |
| `0x00562ec0` | `s_KeyActionIndex_00562ec0` | 24 | `Screens/State00/Options.cs` |
| `0x00562ed8` | `s_KeyRowTable_00562ed8` | 392 | `Screens/State00/Options.cs` |
| `0x00563060` | `s_KeyArrowColumn_00563060` | 232 | `Screens/State00/Options.cs` |
| `0x00563148` | `s_RowSpriteRecord_00563148` | 24 | `Screens/State01/PartySetup.cs` |
| `0x00563160` | `s_SaveDeviceName_00563160` | 16 | `Engine/SaveData.cs` |
| `0x00563170` | `s_SaveFolderName_00563170` | 172 | `Engine/SaveData.cs` |
| `0x0056321c` | `s_CdCheckFileSize_0056321c` | 396 | `Memory/GameMemory.cs` |
| `0x005633a8` | `s_DebugModeLatch_005633a8` | 8 | `Memory/GameMemory.cs` |
| `0x005633b0` | `s_WorldMapDescriptor_005633b0` | 2220 | `Screens/State03/WorldMap.cs` |
| `0x00563c5c` | `s_Unknown_00563c5c` | 27 | `Screens/State03/WorldMap.cs` |
| `0x00563c77` | `s_Unknown_00563c77` | 2249 | `Screens/State03/WorldMap.cs` |
| `0x00564540` | `s_Unknown_00564540` | 8 | `Screens/State03/WorldMap.cs` |
| `0x00564548` | `s_CellSweepCorner_00564548` | 16 | `Memory/GameMemory.cs` |
| `0x00564558` | `s_CellSkipTables_00564558` | 160 | `Memory/GameMemory.cs` |
| `0x005645f8` | `s_WorldMapStep_005645f8` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005645fc` | `s_Unknown_005645fc` | 4 | `Screens/State03/WorldMap.cs` |
| `0x00564600` | `s_SpellCounts_00564600` | 80 | `Screens/State03/WorldMapTick.cs` |
| `0x00564650` | `s_WorldMapCamera_00564650` | 956 | `Screens/State03/WorldMap.cs` |
| `0x00564a0c` | `s_SlashLabel_00564a0c` | 4 | `Screens/State06/Inventory.cs` |
| `0x00564a10` | `s_PageWaitCommand_00564a10` | 28 | `Screens/State06/Inventory.cs` |
| `0x00564a2c` | `s_CursorGlyph_00564a2c` | 20 | `Screens/DebugParty.cs` |
| `0x00564a40` | `s_CheatSequence_00564a40` | 8 | `Engine/UiText.cs` |
| `0x00564a48` | `s_DeviceCapFlag_00564a48` | 4 | `Engine/Input.cs` |
| `0x00564a4c` | `s_Unknown_00564a4c` | 4 | `Screens/State00/MainMenu.cs` |
| `0x00564a50` | `s_KeyboardEnabled_00564a50` | 24 | `Engine/Input.cs` |
| `0x00564a68` | `s_JoyBindingDefaults_00564a68` | 80 | `Screens/State00/Options.cs` |
| `0x00564ab8` | `s_KeyBindingDefaults_00564ab8` | 1120 | `Screens/State00/Options.cs` |
| `0x00564f18` | `s_PadButtonMasks_00564f18` | 200 | `Engine/Input.cs` |
| `0x00564fe0` | `s_ItemMenuDescriptors_00564fe0` | 51 | `Screens/State06/ItemMenu.cs` |
| `0x00565013` | `s_Unknown_00565013` | 8 | `Screens/State06/Inventory.cs` |
| `0x0056501b` | `s_Unknown_0056501b` | 2188 | `Screens/State06/Inventory.cs` |
| `0x005658a7` | `s_ItemMenuDialogGate_005658a7` | 1 | `Screens/State06/Inventory.cs` |
| `0x005658a8` | `s_ItemMenuTitleDescriptor_005658a8` | 2248 | `Screens/State06/ItemMenu.cs` |
| `0x00566170` | `s_ItemMenuEditDescriptor_00566170` | 2280 | `Screens/State06/ItemMenu.cs` |
| `0x00566a58` | `s_CardSlotIds_00566a58` | 56 | `Screens/State06/Inventory.cs` |
| `0x00566a90` | `s_CardSlotLists_00566a90` | 56 | `Screens/State06/Inventory.cs` |
| `0x00566ac8` | `s_ItemMenuSubState_00566ac8` | 4 | `Screens/State06/ItemMenu.cs` |
| `0x00566acc` | `s_Unknown_00566acc` | 4 | `Screens/State06/ItemMenu.cs` |
| `0x00566ad0` | `s_StatSlotOrder_00566ad0` | 8 | `Screens/State06/Inventory.cs` |
| `0x00566ad8` | `s_ItemPanelIcons_00566ad8` | 48 | `Screens/State06/Inventory.cs` |
| `0x00566b08` | `s_ElementIcons_00566b08` | 64 | `Screens/State06/Inventory.cs` |
| `0x00566b48` | `s_ItemIconSprite_00566b48` | 8 | `Screens/State06/Inventory.cs` |
| `0x00566b50` | `s_EquippedMarker_00566b50` | 8 | `Screens/State06/Inventory.cs` |
| `0x00566b58` | `s_InventoryLabels_00566b58` | 40 | `Screens/State06/ItemMenu.cs` |
| `0x00566b80` | `s_UnknownStatLabel_00566b80` | 4 | `Screens/State06/Inventory.cs` |
| `0x00566b84` | `s_UnknownItemLabel_00566b84` | 8 | `Screens/State06/Inventory.cs` |
| `0x00566b8c` | `s_DashesLabel_00566b8c` | 92 | `Screens/State01/PartySetup.cs` |
| `0x00566be8` | `s_DecimalDivisors_00566be8` | 40 | `Engine/UiText.cs` |
| `0x00566c10` | `s_WorldMapUploadRect_00566c10` | 24 | `Screens/State03/WorldMap.cs` |
| `0x00566c28` | `s_GlyphAtlas_00566c28` | 8 | `Screens/State00/MainMenu.cs` |
| `0x00566c30` | `s_NumeralTileRecord_00566c30` | 24 | `Screens/State04/SaveSlots.cs` |
| `0x00566c48` | `s_MenuScreenBlock_00566c48` | 40 | `Screens/State00/MainMenu.cs` |
| `0x00566c70` | `s_MenuRenderBlock_00566c70` | **0** | `Screens/State00/MainMenu.cs` |
| `0x00566c70` | `s_ViewRect_00566c70` | 20 | `Screens/State03/WorldMap.cs` |
| `0x00566c84` | `s_Unknown_00566c84` | 4 | `Screens/DebugParty.cs` |
| `0x00566c88` | `s_Unknown_00566c88` | 4 | `Screens/DebugParty.cs` |
| `0x00566c8c` | `s_Unknown_00566c8c` | 4 | `Screens/DebugParty.cs` |
| `0x00566c90` | `s_Unknown_00566c90` | 4 | `Screens/DebugParty.cs` |
| `0x00566c94` | `s_Unknown_00566c94` | 4 | `Screens/DebugParty.cs` |
| `0x00566c98` | `s_UiOffsetX_00566c98` | 24 | `Screens/State00/Options.cs` |
| `0x00566cb0` | `s_UiOffsetY_00566cb0` | 232 | `Screens/State00/Options.cs` |
| `0x00566d98` | `s_VtPtr_00566d98` | 704 | `Engine/EntityVTables.cs` |
| `0x00567058` | `s_VtPtr_00567058` | 704 | `Engine/EntityVTables.cs` |
| `0x00567318` | `s_VtPtr_00567318` | 704 | `Engine/EntityVTables.cs` |
| `0x005675d8` | `s_VtPtr_005675d8` | 704 | `Engine/EntityVTables.cs` |
| `0x00567898` | `s_VtPtr_00567898` | 704 | `Engine/EntityVTables.cs` |
| `0x00567b58` | `s_VtPtr_00567b58` | 704 | `Engine/EntityVTables.cs` |
| `0x00567e18` | `s_VtPtr_00567e18` | 1664 | `Engine/EntityVTables.cs` |
| `0x00568498` | `s_SaveNameTemplate_00568498` | 120 | `Screens/State04/CharacterCreation.cs` |
| `0x00568510` | `s_ControllerBannerText_00568510` | 9024 | `Engine/SaveData.cs` |
| `0x0056a850` | `s_MenuLayout_0056a850` | 592 | `Screens/State03/WorldMapTick.cs` |
| `0x0056aca8` | `s_ResourceFixupSchema_0056aca8` | 160 | `Engine/Resources.cs` |
| `0x0056ad48` | `s_Vt_0056ad48` | 128 | `Engine/Resources.cs` |
| `0x0056adc8` | `s_Vt_0056adc8` | 256 | `Engine/Resources.cs` |
| `0x0056aec8` | `s_Vt_0056aec8` | 256 | `Engine/Resources.cs` |
| `0x0056afc8` | `s_Vt_0056afc8` | 256 | `Engine/Resources.cs` |
| `0x0056b0c8` | `s_Vt_0056b0c8` | 256 | `Engine/Resources.cs` |
| `0x0056b1c8` | `s_Vt_0056b1c8` | 256 | `Engine/Resources.cs` |
| `0x0056b2c8` | `s_Vt_0056b2c8` | 256 | `Engine/Resources.cs` |
| `0x0056b3c8` | `s_VtBlock` — le bloc d'arret fatal (64 x `FUN_00536170`) | 264 | `Engine/EntityVTables.cs` |
| `0x0056b4d0` | `s_AreaHandlerTable_0056b4d0` | 136 | `Engine/Resources.cs` |
| `0x0056b558` | `s_LoadScreenDescriptors_0056b558` | 51 | `Memory/GameMemory.cs` |
| `0x0056b58b` | `s_Unknown_0056b58b` | 8 | `Memory/GameMemory.cs` |
| `0x0056b593` | `s_Unknown_0056b593` | 2185 | `Memory/GameMemory.cs` |
| `0x0056be1c` | `s_Unknown_0056be1c` | 1 | `Memory/GameMemory.cs` |
| `0x0056be1d` | `s_Unknown_0056be1d` | 2 | `Memory/GameMemory.cs` |
| `0x0056be1f` | `s_Unknown_0056be1f` | 1 | `Memory/GameMemory.cs` |
| `0x0056be20` | `s_LoadScreenDescriptor2_0056be20` | 59 | `Memory/GameMemory.cs` |
| `0x0056be5b` | `s_Unknown_0056be5b` | 2185 | `Memory/GameMemory.cs` |
| `0x0056c6e4` | `s_Unknown_0056c6e4` | 1 | `Memory/GameMemory.cs` |
| `0x0056c6e5` | `s_Unknown_0056c6e5` | 2 | `Memory/GameMemory.cs` |
| `0x0056c6e7` | `s_Unknown_0056c6e7` | 153 | `Memory/GameMemory.cs` |
| `0x0056c780` | `s_DebugPartyNameOffsets_0056c780` | 64 | `Screens/DebugParty.cs` |
| `0x0056c7c0` | `s_ClassNameOffsets_0056c7c0` | 32 | `Screens/DebugParty.cs` |
| `0x0056c7e0` | `s_ClassBaseStats_0056c7e0` | 64 | `Screens/DebugParty.cs` |
| `0x0056c820` | `s_ClassStartIcon_0056c820` | 8 | `Screens/DebugParty.cs` |
| `0x0056c828` | `s_StatBitRemap_0056c828` | 8 | `Screens/DebugParty.cs` |
| `0x0056c830` | `s_StatBitTable_0056c830` | 24 | `Screens/DebugParty.cs` |
| `0x0056c848` | `s_DebugAppearanceA_0056c848` | 16 | `Screens/DebugParty.cs` |
| `0x0056c858` | `s_DebugAppearanceB_0056c858` | 16 | `Screens/DebugParty.cs` |
| `0x0056c868` | `s_ClassAbbrev_0056c868` | 44 | `Screens/State01/PartySetup.cs` |
| `0x0056c894` | `s_ClassNameBlock_0056c894` | 64 | `Screens/DebugParty.cs` |
| `0x0056c8d4` | `s_DebugNameBlock_0056c8d4` | 124 | `Screens/DebugParty.cs` |
| `0x0056c950` | `s_BufferOverLabel_0056c950` | 12 | `Screens/DebugParty.cs` |
| `0x0056c95c` | `s_FemaleLabel_0056c95c` | 8 | `Screens/DebugParty.cs` |
| `0x0056c964` | `s_MaleLabel_0056c964` | 20 | `Screens/DebugParty.cs` |
| `0x0056c978` | `s_DestinationDescriptor_0056c978` | 4 | `Screens/State02/Destination.cs` |
| `0x0056c97c` | `s_PanelX_0056c97c` | 4 | `Screens/State02/Destination.cs` |
| `0x0056c980` | `s_PanelW_0056c980` | 2240 | `Screens/State02/Destination.cs` |
| `0x0056d240` | `s_DestinationIconPos_0056d240` | 48 | `Screens/State02/Destination.cs` |
| `0x0056d270` | `s_DestinationCamX_0056d270` | 2 | `Screens/State02/Destination.cs` |
| `0x0056d272` | `s_DestinationCamY_0056d272` | 46 | `Screens/State02/Destination.cs` |
| `0x0056d2a0` | `s_AreaToMenuIndex_0056d2a0` | 16 | `Screens/State02/Destination.cs` |
| `0x0056d2b0` | `s_MenuIndexToArea_0056d2b0` | 16 | `Screens/State02/Destination.cs` |
| `0x0056d2c0` | `s_DifficultyBonus_0056d2c0` | 128 | `Screens/State04/CharacterCreation.cs` |
| `0x0056d340` | `s_StatusTimerMasks_0056d340` | 2584 | `Screens/State03/WorldMapTick.cs` |
| `0x0056dd58` | `s_MenuContext_0056dd58` | 2244 | `Screens/State00/Options.cs` |
| `0x0056e61c` | `s_MenuSelection_0056e61c` | 3 | `Screens/State00/MainMenu.cs` |
| `0x0056e61f` | `s_Unknown_0056e61f` | 81 | `Screens/State00/MainMenu.cs` |
| `0x0056e670` | `s_MenuLabels_0056e670` | 184 | `Screens/State00/Options.cs` |
| `0x0056e728` | `s_RenderTable_0056e728` | 136 | `Engine/UiText.cs` |
| `0x0056e7b0` | `s_GlyphWidths_0056e7b0` | 192 | `Engine/UiText.cs` |
| `0x0056e870` | `s_ScanTable_0056e870` | 324 | `Engine/UiText.cs` |
| `0x0056e9b4` | `s_HalfWidthCodes_0056e9b4` | 12 | `Engine/UiText.cs` |
| `0x0056e9c0` | `s_RandSeed_0056e9c0` | 11072 | `Memory/GameMemory.cs` |
| `0x00571500` | `s_EntityState_00571500` | 1340 | `Memory/GameMemory.cs` |
| `0x00571a3c` | `s_Unknown_00571a3c` | 4356 | `Screens/State00/MainMenu.cs` |
| `0x00572b40` | `s_EmptyLabel_00572b40` | 8 | `Screens/State04/CharacterCreation.cs` |
| `0x00572b48` | `s_SceneEntity_00572b48` | 156 | `Memory/GameMemory.cs` |
| `0x00572be4` | `s_SceneParticles_00572be4` | 628 | `Memory/GameMemory.cs` |
| `0x00572e58` | `s_SceneIcons_00572e58` | 1248 | `Screens/State04/SaveSlots.cs` |
| `0x00573338` | `s_SelectScratch_00573338` | 24 | `Screens/State04/SaveSlots.cs` |
| `0x00573350` | `s_Dice_00573350` | 456 | `Screens/State04/CharacterCreation.cs` |
| `0x00573518` | `s_SelectSlots_00573518` | 20232 | `Screens/State04/SaveSlots.cs` |
| `0x00578420` | `s_CharacterSlots_00578420` | 2496 | `Memory/GameMemory.cs` |
| `0x00578de0` | `s_CardLoadResult_00578de0` | 4 | `Screens/State04/CharacterCreation.cs` |
| `0x00578de4` | `s_StatSource_00578de4` | 32 | `Screens/State04/CharacterCreation.cs` |
| `0x00578e04` | `s_DiceRolled_00578e04` | 8 | `Screens/State04/CharacterCreation.cs` |
| `0x00578e0c` | `s_CardPhase_00578e0c` | 4 | `Screens/State04/CharacterCreation.cs` |
| `0x00578e10` | `s_CardPrevPhase_00578e10` | 4 | `Screens/State04/CharacterCreation.cs` |
| `0x00578e14` | `s_CardPhaseChanged_00578e14` | 4 | `Screens/State04/CharacterCreation.cs` |
| `0x00578e18` | `s_CardCursor_00578e18` | 8 | `Screens/State04/CharacterCreation.cs` |
| `0x00578e20` | `s_CardPort_00578e20` | 16 | `Screens/State04/CharacterCreation.cs` |
| `0x00578e30` | `s_UiBackgroundInfo_00578e30` | 1072 | `Screens/State00/Options.cs` |
| `0x00579260` | `s_UiPageFlags_00579260` | 4 | `Screens/State00/Options.cs` |
| `0x00579264` | `s_UiBackgroundFile_00579264` | 4 | `Screens/State00/Options.cs` |
| `0x00579268` | `s_UiTimingSelection_00579268` | 4 | `Screens/State00/Options.cs` |
| `0x0057926c` | `s_UiBackgroundTexture_0057926c` | 4 | `Screens/State00/Options.cs` |
| `0x00579270` | `s_Unknown_00579270` | 8 | `Engine/Input.cs` |
| `0x00579278` | `s_UiTargetInfo_00579278` | 1168 | `Screens/State00/Options.cs` |
| `0x00579708` | `s_UiAtlasNegativeBits_00579708` | 65536 | `Screens/State00/Options.cs` |
| `0x00589708` | `s_UiAtlasNegative_00589708` | 80 | `Screens/State00/Options.cs` |
| `0x00589758` | `s_UiLabelWidths_00589758` | 216 | `Screens/State00/Options.cs` |
| `0x00589830` | `s_UiAtlas1_00589830` | 4 | `Screens/State00/Options.cs` |
| `0x00589834` | `s_UiAtlas2_00589834` | 4 | `Screens/State00/Options.cs` |
| `0x00589838` | `s_UiFrameFile_00589838` | 8 | `Screens/State00/Options.cs` |
| `0x00589840` | `s_UiFrameInfo_00589840` | 1068 | `Screens/State00/Options.cs` |
| `0x00589c6c` | `s_UiAtlas0_00589c6c` | 8 | `Screens/State00/Options.cs` |
| `0x00589c74` | `s_UiFontFile_00589c74` | 4 | `Screens/State00/Options.cs` |
| `0x00589c78` | `s_UiAtlasKeyboard_00589c78` | 8 | `Screens/State00/Options.cs` |
| `0x00589c80` | `s_UiAtlas0Bits_00589c80` | 65536 | `Screens/State00/Options.cs` |
| `0x00599c80` | `s_UiKeyPlacement_00599c80` | 344 | `Screens/State00/Options.cs` |
| `0x00599dd8` | `s_UiAtlasJoystick_00599dd8` | 4 | `Screens/State00/Options.cs` |
| `0x00599ddc` | `s_UiFrameAtlas_00599ddc` | 4 | `Screens/State00/Options.cs` |
| `0x00599de0` | `s_UiAtlasJoystickBits_00599de0` | 65536 | `Screens/State00/Options.cs` |
| `0x005a9de0` | `s_UiKeyboardBits_005a9de0` | 65536 | `Screens/State00/Options.cs` |
| `0x005b9de0` | `s_UiAtlas1Bits_005b9de0` | 65536 | `Screens/State00/Options.cs` |
| `0x005c9de0` | `s_UiAtlas2Bits_005c9de0` | 65536 | `Screens/State00/Options.cs` |
| `0x005d9de0` | `s_Unknown_005d9de0` | 4 | `Screens/State00/MainMenu.cs` |
| `0x005d9de4` | `s_UiSetupDone_005d9de4` | 20 | `Screens/State00/Options.cs` |
| `0x005d9df8` | `s_UiFrameColourKey_005d9df8` | 8 | `Screens/State00/Options.cs` |
| `0x005d9e00` | `s_UiRepeatActive_005d9e00` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e04` | `s_UiKeyEdges_005d9e04` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e08` | `s_UiRepeatCounter_005d9e08` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e0c` | `s_UiResolutionSelection_005d9e0c` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e10` | `s_UiDisplaySelection_005d9e10` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e14` | `s_UiSoundSelection_005d9e14` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e18` | `s_UiJoySelection_005d9e18` | 16 | `Screens/State00/Options.cs` |
| `0x005d9e28` | `s_UiJoyScroll_005d9e28` | 16 | `Screens/State00/Options.cs` |
| `0x005d9e38` | `s_UiJoystickSelection_005d9e38` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e3c` | `s_UiKeySelection_005d9e3c` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e40` | `s_UiKeyScroll_005d9e40` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e44` | `s_UiControlSelection_005d9e44` | 4 | `Screens/State00/Options.cs` |
| `0x005d9e48` | `s_UiOptionsSelection_005d9e48` | 7712 | `Screens/State00/Options.cs` |
| `0x005dbc68` | `s_WorldPanels_005dbc68` | 9160 | `Screens/State03/WorldMap.cs` |
| `0x005de030` | `s_CameraBlock_005de030` | 4 | `Memory/GameMemory.cs` |
| `0x005de034` | `s_Unknown_005de034` | 204 | `Screens/State04/CharacterCreation.cs` |
| `0x005de100` | `s_ItemMenuArena_005de100` | 16 | `Memory/GameMemory.cs` |
| `0x005de110` | `s_ItemNameScratch_005de110` | 68 | `Screens/State06/Inventory.cs` |
| `0x005de154` | `s_Unknown_005de154` | 4 | `Screens/State00/MainMenu.cs` |
| `0x005de158` | `s_Unknown_005de158` | 4 | `Screens/State00/MainMenu.cs` |
| `0x005de15c` | `s_Unknown_005de15c` | 4 | `Screens/State00/MainMenu.cs` |
| `0x005de160` | `s_SizeSetting_005de160` | 4 | `Screens/State00/Options.cs` |
| `0x005de164` | `s_CheatIndex_005de164` | 4 | `Engine/UiText.cs` |
| `0x005de168` | `s_Unknown_005de168` | 360 | `Engine/Input.cs` |
| `0x005de2d0` | `s_ItemValue_005de2d0` | 8 | `Screens/State06/Inventory.cs` |
| `0x005de2d8` | `s_ItemHeld_005de2d8` | 24 | `Screens/State06/Inventory.cs` |
| `0x005de2f0` | `s_Unknown_005de2f0` | 160 | `Screens/State06/Inventory.cs` |
| `0x005de390` | `s_CardMessage_005de390` | 128 | `Screens/State06/Inventory.cs` |
| `0x005de410` | `s_ItemMenuRecord_005de410` | 24 | `Screens/State06/ItemMenu.cs` |
| `0x005de428` | `s_KeyCellAddress_005de428` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de42c` | `s_ItemMenuChosenEntry_005de42c` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de430` | `s_ItemCount_005de430` | 24 | `Screens/State06/Inventory.cs` |
| `0x005de448` | `s_Unknown_005de448` | 28 | `Screens/State06/Inventory.cs` |
| `0x005de464` | `s_Unknown_005de464` | 4 | `Screens/State06/ItemMenu.cs` |
| `0x005de468` | `s_CardDialogStep_005de468` | 28 | `Screens/State06/Inventory.cs` |
| `0x005de484` | `s_CardDialogResult_005de484` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de488` | `s_ItemIndex_005de488` | 24 | `Screens/State06/Inventory.cs` |
| `0x005de4a0` | `s_ItemNameEdit_005de4a0` | 36 | `Screens/State06/Inventory.cs` |
| `0x005de4c4` | `s_ItemMenuScroll_005de4c4` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4c8` | `s_ItemMenuSelection_005de4c8` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4cc` | `s_ItemMenuChosen_005de4cc` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4d0` | `s_ItemMenuPhase_005de4d0` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4d4` | `s_Unknown_005de4d4` | 4 | `Screens/State06/ItemMenu.cs` |
| `0x005de4d8` | `s_ItemMenuPrevPhase_005de4d8` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4dc` | `s_ItemMenuEntered_005de4dc` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4e0` | `s_ItemMenuCursor_005de4e0` | 4 | `Screens/State06/ItemMenu.cs` |
| `0x005de4e4` | `s_Unknown_005de4e4` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4e8` | `s_ItemMenuCardSlot_005de4e8` | **0** | `Screens/State06/Inventory.cs` |
| `0x005de4e8` | `s_ItemMenuLoaded_005de4e8` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4ec` | `s_ItemGridScroll_005de4ec` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4f0` | `s_ItemMenuVisible_005de4f0` | **0** | `Screens/State06/Inventory.cs` |
| `0x005de4f0` | `s_ItemMenuTarget_005de4f0` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4f4` | `s_Unknown_005de4f4` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4f8` | `s_KeyPage_005de4f8` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de4fc` | `s_KeyColumn_005de4fc` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de500` | `s_KeyRow_005de500` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de504` | `s_ItemNameLength_005de504` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de508` | `s_ItemRenamed_005de508` | 4 | `Screens/State06/Inventory.cs` |
| `0x005de50c` | `s_Unknown_005de50c` | 16 | `Screens/State06/ItemMenu.cs` |
| `0x005de51c` | `s_Unknown_005de51c` | 12 | `Screens/State00/MainMenu.cs` |
| `0x005de528` | `s_Unknown_005de528` | 4 | `Engine/Resources.cs` |
| `0x005de52c` | `s_Unknown_005de52c` | 4 | `Screens/State02/Destination.cs` |
| `0x005de530` | `s_Unknown_005de530` | 4 | `Screens/State02/Destination.cs` |
| `0x005de534` | `s_Unknown_005de534` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005de538` | `s_Unknown_005de538` | 4 | `Engine/Resources.cs` |
| `0x005de53c` | `s_Unknown_005de53c` | 16 | `Screens/DebugParty.cs` |
| `0x005de54c` | `s_Unknown_005de54c` | 8 | `Screens/State03/WorldMap.cs` |
| `0x005de554` | `s_Unknown_005de554` | 4 | `Engine/Input.cs` |
| `0x005de558` | `s_EntityList_005de558` | 480 | `Engine/Resources.cs` |
| `0x005de738` | `s_Unknown_005de738` | 176 | `Screens/DebugParty.cs` |

> **Correction locale, 2026-08-25.** L'écart de 480 déclaré sur `0x005de558` est celui d'un index
> qui n'a pas suivi : la bande `0x005de5f8`..`0x005de6f0` porte depuis plusieurs séances des
> symboles que la table ci-dessous n'a jamais listés. Relevé pour cette bande seule, dans l'ordre :
>
> | VA | symbole C# | taille | fichier |
> |---|---|---:|---|
> | `0x005de5f8` | `s_MenuSprite_005de5f8` | 20 | `Screens/State03/WorldMapMenu.cs` |
> | `0x005de620` | `s_ItemMessageScratch_005de620` | 128 | `Screens/State03/WorldMapTick.cs` |
> | `0x005de6a8` | `s_KeyItemIndex_005de6a8` | 32 | `Screens/State03/WorldMapMenu.cs` |
> | `0x005de6f0` | `s_MenuPage_005de6f0` | 4 | `Screens/State03/WorldMapMenu.cs` |
>
> Les trois écarts internes se referment sans recouvrement (`0x005de60c` → `0x005de620`,
> `0x005de6a0` → `0x005de6a8`, `0x005de6c8` → `0x005de6f0`). Le reste de l'index n'a pas été
> re-vérifié : le prendre comme un état daté, pas comme une garantie.
| `0x005de7e8` | `s_Unknown_005de7e8` | 4 | `Screens/State01/PartySetup.cs` |
| `0x005de7ec` | `s_Unknown_005de7ec` | 4 | `Screens/State01/PartySetup.cs` |
| `0x005de7f0` | `s_Unknown_005de7f0` | 4 | `Screens/State01/PartySetup.cs` |
| `0x005de7f4` | `s_Unknown_005de7f4` | 48 | `Screens/State01/PartySetup.cs` |
| `0x005de824` | `s_Particles_005de824` | 6060 | `Screens/State02/Destination.cs` |
| `0x005dffd0` | `s_GlyphMode_005dffd0` | 8 | `Engine/UiText.cs` |
| `0x005dffd8` | `s_DbcsRowScratch_005dffd8` | 16 | `Engine/UiText.cs` |
| `0x005dffe8` | `s_Unknown_005dffe8` | 788 | `Engine/UiText.cs` |
| `0x005e02fc` | `s_BreakStaging_005e02fc` | 4 | `Engine/UiText.cs` |
| `0x005e0300` | `s_GlyphStaging_005e0300` | 32768 | `Engine/UiText.cs` |
| `0x005e8300` | `s_BreakColumn_005e8300` | 4 | `Engine/UiText.cs` |
| `0x005e8304` | `s_StagingWrite_005e8304` | 4 | `Engine/UiText.cs` |
| `0x005e8308` | `s_TextState_005e8308` | 4 | `Engine/UiText.cs` |
| `0x005e830c` | `s_TrimAnchor_005e830c` | 4 | `Engine/UiText.cs` |
| `0x005e8310` | `s_Unknown_005e8310` | 4 | `Screens/State00/MainMenu.cs` |
| `0x005e8314` | `s_OtCursor_005e8314` | 4 | `Engine/UiText.cs` |
| `0x005e8318` | `s_LineIndex_005e8318` | 4 | `Engine/UiText.cs` |
| `0x005e831c` | `s_UploadCursor_005e831c` | 4 | `Engine/UiText.cs` |
| `0x005e8320` | `s_PenX_005e8320` | 4 | `Engine/UiText.cs` |
| `0x005e8324` | `s_SpriteCursor_005e8324` | 4 | `Engine/UiText.cs` |
| `0x005e8328` | `s_BreakPen_005e8328` | 504 | `Engine/UiText.cs` |
| `0x005e8520` | `s_SoundBankHeader_005e8520` | 160 | `Screens/State03/WorldMap.cs` |
| `0x005e85c0` | `s_PartyListArena_005e85c0` | **0** | `Memory/GameMemory.cs` |
| `0x005e85c0` | `s_PartySelectionOrder_005e85c0` | 128 | `Screens/State01/PartySetup.cs` |
| `0x005e8640` | `s_PartyRowText_005e8640` | 2240 | `Screens/State01/PartySetup.cs` |
| `0x005e8f00` | `s_EntityDecodeTable_005e8f00` | 192 | `Screens/State00/MainMenu.cs` |
| `0x005e8fc0` | `s_EntityDecodeTable_005e8fc0` | 64 | `Engine/Resources.cs` |
| `0x005e9000` | `s_GameArena_005e9000` | **0** | `Memory/GameMemory.cs` |
| `0x005e9000` | `s_PartyAbilityMask_005e9000` | 32 | `Screens/State04/SaveSlots.cs` |
| `0x005e9020` | `s_PartySize_005e9020` | 64 | `Screens/State04/SaveSlots.cs` |
| `0x005e9060` | `s_StateBank2_005e9060` | **0** | `Memory/GameMemory.cs` |
| `0x005e9060` | `s_PanelSelection_005e9060` | 10 | `Memory/GameMemory.cs` |
| `0x005e906a` | `s_StatTotal_005e906a` | 22 | `Screens/State04/CharacterCreation.cs` |
| `0x005e9080` | `s_Unknown_005e9080` | 26 | `Screens/State06/Inventory.cs` |
| `0x005e909a` | `s_Unknown_005e909a` | 1 | `Screens/State04/SaveSlots.cs` |
| `0x005e909b` | `s_Unknown_005e909b` | 1 | `Screens/State04/SaveSlots.cs` |
| `0x005e909c` | `s_Unknown_005e909c` | 1 | `Screens/State04/SaveSlots.cs` |
| `0x005e909d` | `s_Unknown_005e909d` | 1 | `Screens/State04/SaveSlots.cs` |
| `0x005e909e` | `s_Unknown_005e909e` | 1 | `Screens/State04/SaveSlots.cs` |
| `0x005e909f` | `s_Unknown_005e909f` | 225 | `Memory/GameMemory.cs` |
| `0x005e9180` | `s_EntitySlotTable_005e9180` | **0** | `Engine/Resources.cs` |
| `0x005e9180` | `s_SlotArea_005e9180` | 8 | `Engine/Input.cs` |
| `0x005e9188` | `s_PlayerSlots_005e9188` | 296 | `Engine/Input.cs` |
| `0x005e92b0` | `s_Unknown_005e92b0` | 16 | `Screens/DebugParty.cs` |
| `0x005e92c0` | `s_StateBank5_005e92c0` | **0** | `Memory/GameMemory.cs` |
| `0x005e92c0` | `s_Bank_005e92c0` | 72 | `Screens/State01/PartySetup.cs` |
| `0x005e9308` | `s_ScenePitch_005e9308` | 2 | `Memory/GameMemory.cs` |
| `0x005e930a` | `s_SceneYaw_005e930a` | 2 | `Memory/GameMemory.cs` |
| `0x005e930c` | `s_SceneRoll_005e930c` | 20 | `Memory/GameMemory.cs` |
| `0x005e9320` | `s_EquipPairs_005e9320` | 64 | `Screens/State01/PartySetup.cs` |
| `0x005e9360` | `s_StateBank7_005e9360` | **0** | `Memory/GameMemory.cs` |
| `0x005e9360` | `s_Bank_005e9360` | 64 | `Screens/State01/PartySetup.cs` |
| `0x005e93a0` | `s_BackColourR_005e93a0` | 2 | `Memory/GameMemory.cs` |
| `0x005e93a2` | `s_BackColourG_005e93a2` | 2 | `Memory/GameMemory.cs` |
| `0x005e93a4` | `s_BackColourB_005e93a4` | 28 | `Memory/GameMemory.cs` |
| `0x005e93c0` | `s_SlotFlags_005e93c0` | 624 | `Engine/Input.cs` |
| `0x005e9630` | `s_Unknown_005e9630` | 16 | `Memory/GameMemory.cs` |
| `0x005e9640` | `s_AnimationTable_005e9640` | 64 | `Screens/State04/CharacterSelect.cs` |
| `0x005e9680` | `s_PartyAnimationResult_005e9680` | 32 | `Screens/DebugParty.cs` |
| `0x005e96a0` | `s_ModelTable_005e96a0` | 64 | `Screens/State04/CharacterSelect.cs` |
| `0x005e96e0` | `s_StateBank0_005e96e0` | **0** | `Memory/GameMemory.cs` |
| `0x005e96e0` | `s_Bank_005e96e0` | 64 | `Screens/State01/PartySetup.cs` |
| `0x005e9720` | `s_StateBank4_005e9720` | **0** | `Memory/GameMemory.cs` |
| `0x005e9720` | `s_Bank_005e9720` | 64 | `Screens/State01/PartySetup.cs` |
| `0x005e9760` | `s_Unknown_005e9760` | 32 | `Screens/State03/WorldMap.cs` |
| `0x005e9780` | `s_StateBank1_005e9780` | **0** | `Memory/GameMemory.cs` |
| `0x005e9780` | `s_Bank_005e9780` | 64 | `Screens/State01/PartySetup.cs` |
| `0x005e97c0` | `s_MainResourceBuffer_005e97c0` | 4 | `Memory/GameMemory.cs` |
| `0x005e97c4` | `s_MainResourceDirectory_005e97c4` | 4 | `Engine/Resources.cs` |
| `0x005e97c8` | `s_Unknown_005e97c8` | 4 | `Screens/State00/MainMenu.cs` |
| `0x005e97cc` | `s_Unknown_005e97cc` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005e97d0` | `s_Unknown_005e97d0` | 4 | `Screens/State00/MainMenu.cs` |
| `0x005e97d4` | `s_Unknown_005e97d4` | 12 | `Screens/State03/WorldMap.cs` |
| `0x005e97e0` | `s_ScreenStride_005e97e0` | 4 | `Memory/GameMemory.cs` |
| `0x005e97e4` | `s_Unknown_005e97e4` | 4 | `Engine/Resources.cs` |
| `0x005e97e8` | `s_Unknown_005e97e8` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005e97ec` | `s_Unknown_005e97ec` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005e97f0` | `s_Unknown_005e97f0` | 16 | `Memory/GameMemory.cs` |
| `0x005e9800` | `s_PartyModelResult_005e9800` | 16 | `Screens/DebugParty.cs` |
| `0x005e9810` | `s_Unknown_005e9810` | 4 | `Engine/Resources.cs` |
| `0x005e9814` | `s_Unknown_005e9814` | 4 | `Screens/State00/MainMenu.cs` |
| `0x005e9818` | `s_Unknown_005e9818` | 8 | `Screens/State00/MainMenu.cs` |
| `0x005e9820` | `s_Unknown_005e9820` | 4 | `Memory/GameMemory.cs` |
| `0x005e9824` | `s_Unknown_005e9824` | 4 | `Engine/Resources.cs` |
| `0x005e9828` | `s_Unknown_005e9828` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005e982c` | `s_Unknown_005e982c` | 20 | `Screens/State03/WorldMap.cs` |
| `0x005e9840` | `s_StateBank3_005e9840` | **0** | `Memory/GameMemory.cs` |
| `0x005e9840` | `s_Bank_005e9840` | 64 | `Screens/State01/PartySetup.cs` |
| `0x005e9880` | `s_EnvBlock_005e9880` | **0** | `Memory/GameMemory.cs` |
| `0x005e9880` | `s_DrawEnv0_005e9880` | 92 | `Memory/GameMemory.cs` |
| `0x005e98dc` | `s_DispEnv0_005e98dc` | 20 | `Memory/GameMemory.cs` |
| `0x005e98f0` | `s_DrawEnv1_005e98f0` | 92 | `Memory/GameMemory.cs` |
| `0x005e994c` | `s_DispEnv1_005e994c` | 20 | `Memory/GameMemory.cs` |
| `0x005e9960` | `s_FadeCounter_005e9960` | 32 | `Screens/State00/MainMenu.cs` |
| `0x005e9980` | `s_StateBank6_005e9980` | **0** | `Memory/GameMemory.cs` |
| `0x005e9980` | `s_Bank_005e9980` | 348 | `Screens/State01/PartySetup.cs` |
| `0x005e9adc` | `s_Unknown_005e9adc` | 2692 | `Screens/State03/WorldMap.cs` |
| `0x005ea560` | `s_EntityStatBlocks_005ea560` | 9376 | `Screens/State04/CharacterCreation.cs` |
| `0x005eca00` | `s_EntityArena_005eca00` | 5316 | `Memory/GameMemory.cs` |
| `0x005edec4` | `s_Unknown_005edec4` | 19996 | `Screens/State03/WorldMap.cs` |
| `0x005f2ce0` | `s_Unknown_005f2ce0` | 20064 | `Screens/State03/WorldMap.cs` |
| `0x005f7b40` | `s_ModelResultB_005f7b40` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005f7b44` | `s_Unknown_005f7b44` | 10816 | `Screens/State03/WorldMap.cs` |
| `0x005fa584` | `s_Unknown_005fa584` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005fa588` | `s_ModelResultA_005fa588` | 25408 | `Screens/State03/WorldMap.cs` |
| `0x006008c8` | `s_OverrideDestA_006008c8` | 8 | `Engine/Resources.cs` |
| `0x006008d0` | `s_Unknown_006008d0` | 4 | `Memory/GameMemory.cs` |
| `0x006008d4` | `s_Unknown_006008d4` | 4 | `Memory/GameMemory.cs` |
| `0x006008d8` | `s_Unknown_006008d8` | 8 | `Memory/GameMemory.cs` |
| `0x006008e0` | `s_CameraMatrix_006008e0` | 36 | `Memory/GameMemory.cs` |
| `0x00600904` | `s_OverrideDestC_00600904` | 16420 | `Engine/Resources.cs` |
| `0x00604928` | `s_CameraVector_00604928` | 8 | `Memory/GameMemory.cs` |
| `0x00604930` | `s_OverrideDestB_00604930` | 16 | `Engine/Resources.cs` |
| `0x00604940` | `s_CardPathDescriptor_00604940` | 32 | `Engine/SaveData.cs` |
| `0x00604960` | `s_Unknown_00604960` | 32 | `Screens/State04/SaveSlots.cs` |
| `0x00604980` | `s_CardSlotIndex_00604980` | 1 | `Screens/State04/SaveSlots.cs` |
| `0x00604981` | `s_CardEntryCount_00604981` | 1 | `Screens/State04/SaveSlots.cs` |
| `0x00604982` | `s_Unknown_00604982` | 2 | `Screens/State04/SaveSlots.cs` |
| `0x00604984` | `s_CardCursorSlot_00604984` | 6 | `Screens/State04/SaveSlots.cs` |
| `0x0060498a` | `s_Unknown_0060498a` | 2 | `Screens/State04/SaveSlots.cs` |
| `0x0060498c` | `s_CardIconFrameCount_0060498c` | 48 | `Screens/State04/SaveSlots.cs` |
| `0x006049bc` | `s_CardDirectoryEntries_006049bc` | 68 | `Screens/State04/SaveSlots.cs` |
| `0x00604a00` | `s_CardReadStatus_00604a00` | 32 | `Engine/SaveData.cs` |
| `0x00604a20` | `s_InputStateBase_00604a20` | 10 | `Engine/Input.cs` |
| `0x00604a2a` | `s_ButtonStates_00604a2a` | 134 | `Engine/Input.cs` |
| `0x00604ab0` | `s_CardWriteStatus_00604ab0` | 90 | `Engine/SaveData.cs` |
| `0x00604b0a` | `s_JoyAxis1_00604b0a` | 2 | `Engine/Input.cs` |
| `0x00604b0c` | `s_JoyBlock1_00604b0c` | 34 | `Engine/Input.cs` |
| `0x00604b2e` | `s_JoyAxis2_00604b2e` | 34 | `Engine/Input.cs` |
| `0x00604b50` | `s_KeyboardState_00604b50` | 4 | `Engine/Input.cs` |
| `0x00604b54` | `s_JoystickDevice_00604b54` | 4 | `Platform/Startup.cs` |
| `0x00604b58` | `s_CardReadEventHandles_00604b58` | 16 | `Engine/SaveData.cs` |
| `0x00604b68` | `s_CardWriteEventHandles_00604b68` | 7256 | `Engine/SaveData.cs` |
| `0x006067c0` | `s_ViewBlock_006067c0` | — | `Memory/GameMemory.cs` |


## 0x0054f040 — les constantes de la caméra de carte du monde

Relevées le 2026-08-22 en mesurant `FUN_004011c0` (`at Legacy/Binaries/Blaze.exe 0x0054f040 96`).

| VA | valeur | rôle |
|---|---|---|
| `0x0054f040` | `short` 1600 | la hauteur **maximale** de la caméra : au-delà, `FUN_004011c0` la ramène d'un huitième de l'excès par image |
| `0x0054f044` | `short` 900 | la hauteur **minimale**, ramenée de la même façon |

Deux tableaux de **neuf `short` à pas de quatre octets** suivent — c'est-à-dire neuf entrées dont
seule la moitié basse est lue :

| VA | contenu |
|---|---|
| `0x0054f048` | `{-1, 0, 1, -1, 0, 1, -1, 0, 1}` |
| `0x0054f070` | `{-1, -1, -1, 0, 0, 0, 1, 1, 1}` |

Pris ensemble, ce sont les **neuf décalages d'un voisinage 3×3** : `(dx, dz)` parcourant les neuf
cellules autour de la caméra. `FUN_004011c0` les consomme par paires dans
`FUN_00402d60(dx, dz, n)`, dont le `n` est l'indice de cellule.

**Le pas de quatre octets pour des valeurs de deux est ce qui rend le contenu illisible au premier
coup d'œil** : lu en dwords, le premier tableau donne `0xffff, 0, 1, 0xffff…`, ce qui ressemble à
des sentinelles plutôt qu'à des offsets. C'est la lecture en `short` — celle que fait le code — qui
révèle la grille.

## 0x00604ba0 et 0x00604d30 — le cache de maillage de la caméra (non modélisé)

Deux tableaux **parallèles** de pointeurs, espacés de `0x190`, indexés par
`(cellule * 200 + n) * 4` dans `FUN_004011c0`. Le premier donne un enregistrement de face, le
second la table de sommets qui va avec. Le compte par cellule vit dans `DAT_005712a8[cellule]`.

**Leur étendue n'est pas établie** : l'indexation par `cellule * 200` suggère 9 × 200 entrées, ce
qui déborderait largement l'écart de 0x190 entre les deux bases. Soit le compte réel par cellule est
petit, soit l'une des deux bases est mal identifiée. À trancher au désassembleur avant de les
modéliser — les allouer sur la foi de l'indexation apparente reproduirait un débordement qui
n'existe peut-être pas.


## Les descripteurs de sprite de la moitié dessin (0x00564680, 0x00566c18)

Relevés le 2026-08-22 en préparant la moitié dessin du bloc d'affichage de `FUN_00516fc0`. Ce sont
des **descripteurs d'atlas de 8 octets** : `dword0` empaquette l'origine X (octet bas), l'origine Y
(octet 1), la largeur de cellule (octet 2) et la hauteur (5 bits hauts) ; `dword1` part tel quel
comme moitié « attributs » du sprite. La disposition est déjà documentée sur
`Primitives2d.EmitTextSprite` (`FUN_0051c120`), qui les consomme.

> **Un descripteur ne se relève jamais seul — on relève la plage.** Le champ `+0x10` du bloc de
> pile (voir plus bas) est un **indice qui s'ajoute au descripteur passé** : la fonction lit
> `bloc[0] + indice * 8`. La rose des vents s'en sert pour ses quatre points cardinaux — elle
> passe `&DAT_005646c0` et un indice 0..3, donc lit `0x005646c0`, `c8`, `d0`, `d8`. Une table
> qui range des descripteurs isolés dans l'ordre où on les a rencontrés donne le mauvais voisin
> dès l'indice 1 et **sort du tampon** peu après. C'est ce qui a corrompu la mémoire le
> 2026-08-22, et le symptôme est apparu une image plus loin, dans la caméra.
>
> Les deux plages effectivement relevées, contiguës, sont `0x00564680..0x00564900` et
> `0x00566c18..0x00566c40` (`0x00566c40` vaut `0x007f007f` et n'est pas un descripteur). Elles
> sont transcrites telles quelles dans `SpriteAtlas`, qui les adresse par VA.
>
> La ligne `0x00566c20` du tableau ci-dessous portait `0x28180000 / 0x02300008` jusqu'au
> 2026-08-22 : un décalage d'un octet dans le relevé d'origine. Le vidage aligné sur 16 octets
> donne `0x08281800 / 0x02300000`, et les quatre autres lignes de la plage concordent.

| VA | dword0 | dword1 |
|---|---|---|
| `0x00566c18` | `0x40400000` | `0x02300100` |
| `0x00566c20` | `0x08281800` | `0x02300000` |
| `0x00566c28` | `0x40803000` | `0x02300000` |
| `0x00566c30` | `0x08504800` | `0x02300000` |
| `0x00566c38` | `0x0a640880` | `0x02300100` |
| `0x00564690` | `0x10181800` | `0x01100302` |
| `0x005646b8` | `0x3030a830` | `0x0e110001` |
| `0x005646c0` | `0x0808f8a0` | `0x0e110001` |
| `0x00564788` | `0x0f0f80c0` | `0x0d110000` |
| `0x00564790` | `0x0f0f80d0` | `0x0d110000` |
| `0x005648b8` | `0x0808f800` | `0x06110000` |

### Ce que `0x005646f8`..`0x0056476f` s'est révélé être — le 2026-08-25

Deux tables voisines de cette même plage, nommées en transcrivant la page 2 du menu de jeu
(« - Table - » / « Page Cristaux ») et **vérifiées à l'écran** :

| VA | contenu | rôle |
|---|---|---|
| `0x005646f8`..`0x00564738` | 9 descripteurs de 0x20 × 0x20 : `(0,0x70)`, `(0x20,0x70)`, `(0x40,0x70)`, `(0x60,0x70)`, `(0,0x90)`, `(0x20,0x90)`, `(0x40,0x90)`, `(0x60,0x90)`, `(0x40,0xb0)` | **les neuf cristaux** de la dalle |
| `0x00564740` | un descripteur de 0x80 × 0x70, `0x70800000 / 0x00120001` | **la dalle elle-même**, tuile 9 — posée en dernier, donc derrière les cristaux |
| `0x00564748`..`0x0056476c` | 10 dwords, X dans le mot bas et Y dans le mot haut : `(0x68,0x2c)`, `(0x88,0x3c)`, `(0x98,0x4c)`, `(0x90,0x6c)`, `(0x68,0x6c)`, `(0x48,0x6c)`, `(0x40,0x4c)`, `(0x50,0x3c)`, `(0x68,0x54)`, `(0x40,0x2c)` | leurs positions ; les huit premières font un anneau, la neuvième est le centre et la dixième le coin de la dalle |

La borne de boucle du binaire est l'**adresse** `0x00564770`, pas un compte : c'est elle qui fixe la
table à dix entrées. Le relevé de 2026-08-22 les décrivait comme « des couples de coordonnées » sans
plus ; c'était juste, et ce sont bien dix positions et non des couples.

## Les quatre tables de la barre d'état (0x00564900)

| VA | contenu | rôle |
|---|---|---|
| `0x00564900` | `{1, 2, 4, 8, 0x10, 0x20, 0x40, 0x100, 0x200}` | les **neuf bits d'état** que la barre teste contre `bloc + 0x140`, un pictogramme par bit. La boucle s'arrête sur `< 0x564924`, ce qui donne bien neuf entrées. |
| `0x00564928` | `{0x32, -50, 0x172, 0x10e}` | X du panneau, par membre du groupe |
| `0x00564938` | `{0x5a, -20, 0x154, 0xe6}` | seconde disposition de X |
| `0x00564948` | `{0xa0, 0xa0, 0xa0, 0xa0}` | X uniforme |
| `0x00564958` | `{0xb4, 0x8c, 0xb4, 0x8c}` | X alterné |

Les trois dernières sont choisies par `DAT_005de160` — que le dépôt porte déjà sous
`s_SizeSetting_005de160`, le **réglage de taille de rendu** (11 = 640x480, 21 = 960x720, dérivation
dans `Documentation/DLL/winsys3d.md`). Les panneaux se réarrangent donc selon la **résolution**, et
non selon une animation de menu comme ce relevé l'avait d'abord supposé. Le nom existait dans le
dépôt ; il suffisait de le chercher avant d'écrire une interprétation.

## 0x00563340 — le bloc de l'affichage de mise au point

| VA | contenu |
|---|---|
| `0x00563348` | `0x0002000c` |
| `0x0056334c` | `0x000e0003` |
| `0x00563350` | trois pointeurs de chaîne : `0x005649e8`, `0x005649dc`, `0x005649d0` |
| `0x00563360` | deux pointeurs de chaîne : `0x005649cc`, `0x005649c8` |

Ce bloc ne sert qu'au sous-bloc de mise au point, gardé par `DAT_005de020` qui vaut zéro au
chargement — relevé ici parce qu'il était sur le chemin de la mesure, pas parce qu'il est utile
tout de suite.

## Le bloc de sprite que l'appelant bâtit sur sa pile — sept champs, 0x12 octets

Ce n'est pas une donnée du binaire, mais c'est la lecture sans laquelle les relevés ci-dessus ne
servent à rien. Toutes les fonctions de dessin de cette famille — `FUN_0051c120`, `FUN_00521f20`,
`FUN_00521df0`, `FUN_0051f9c0`, `FUN_0051be90` — prennent en troisième argument l'adresse d'un
même bloc :

```
+0x00  int    pointeur vers un descripteur d'atlas de 8 octets (le tableau ci-dessus)
+0x04  int    mot de base : part dans prim[1] sur 26 bits (couleur / liste d'affichage)
+0x08  short  position X            +0x0a  short  position Y
+0x0c  short  echelle X             +0x0e  short  echelle Y      (0x1000 = 1.0)
+0x10  short  indice dans la suite de descripteurs qui commence a +0x00
```

**Ghidra ne montre jamais cette structure** : il rend chaque écriture séparément, sous le nom de
son décalage (`uStack_48`, `uStack_44`, `uStack_40`…), et les demi-mots par des `CONCAT22`.
Reconnaître que des `uStack_NN` consécutifs dont le premier reçoit une adresse de descripteur
forment un bloc est ce qui rend la moitié dessin transcriptible.

### Ce que la première lecture avait manqué

Jusqu'au 2026-08-22 cette section décrivait « trois entiers consécutifs » — les trois premiers
champs seulement, tirés de `FUN_0051c120` qui est la seule de la famille à n'en lire que trois.
Les quatre autres champs manquaient, et les conséquences n'étaient pas cosmétiques :

* `+0x0c`/`+0x0e` ne sont pas des demi-tailles mais des **échelles** en virgule fixe : la largeur
  émise vaut `largeurDeCellule * échelleX >> 12`. Les laisser à zéro donne un sprite de taille
  nulle, et les remplir avec autre chose donne n'importe quoi.
* `+0x10` **indexe en avant** dans les descripteurs — voir l'encadré plus haut.
* le bloc fait `0x12` octets : l'allouer sur trois entiers (12 octets) fait lire `+0x0c`..`+0x10`
  hors de l'allocation.

### La pseudo-C ne fait pas foi sur ce bloc

Deux erreurs de champ relevées au désassembleur dans le seul bloc d'affichage de `FUN_00516fc0` :

| site | ce que Ghidra écrit | ce que dit le désassemblage |
|---|---|---|
| rose des vents, `0x005186cf` | `piStack_4c = CONCAT22(…, X)` → `+0x04` | `mov [esp+3Ch],cx` avec le bloc à `esp+34h` → **`+0x08`** |
| indicateur d'écran, `0x00518002` | `uStack_40 = CONCAT22(…, pouls)` → `+0x08` | `mov [esp+40h],ax` → **`+0x0c`**, et `[esp+46h]` → `+0x0e` |

Dans les deux cas Ghidra décale d'un champ, et dans les deux cas le résultat est absurde à la
lecture : un X dans le mot de couleur, ou une position d'environ `0x1000` sur un écran de 320
pixels. **L'absurdité du résultat est le signal** ; le désassembleur tranche.

## `0x00564548` et `0x00564558` — les deux tables du balayage 7×7 par octant (2026-08-23)

Relevées avec `OpenForesia.Tools -- at`, dans `.data`. Elles se suivent sans trou, et la table de
pointeurs qui désigne la seconde vient juste après : **`0x00564548` .. `0x005645d8` d'un tenant**.

### `&DAT_00564548` — le coin de départ, huit demi-mots

```
0x00564548: 0304 0204 0203 0202 0302 0402 0403 0404
```

soit, en petit-boutiste : `0x0403 0x0402 0x0302 0x0202 0x0203 0x0204 0x0304 0x0404`.

L'indice est l'octant du cap de la caméra, `((DAT_0060492a + 0x100) >> 9) & 7`. L'**octet bas** est
le décalage en X du coin de départ, l'**octet haut** son décalage en Z, tous deux comptés en
cellules à partir de la cellule où se trouve la caméra :

```
cellX = ((-DAT_006008d0 >> 12) + 0x2000 >> 9) + (table[octant] & 0xff)
cellZ = ((-DAT_006008d8 >> 12) + 0x2000 >> 9) + (table[octant] >> 8)
```

Le balayage descend ensuite sept rangs de sept colonnes, **en décroissant sur les deux axes** ; le
coin est donc celui du plus grand X et du plus grand Z.

### `PTR_DAT_005645d8` — huit pointeurs vers huit tranches contiguës

```
0x005645D8: 5845 5600 6845 5600 7845 5600 8845 5600
0x005645E8: 9845 5600 a845 5600 b845 5600 c845 5600
```

soit `0x00564558`, `68`, `78`, `88`, `98`, `a8`, `b8`, `c8` — **huit tranches de 0x10 octets qui
s'arrêtent pile où commence la table de pointeurs elle-même**. C'est la même situation que les
descripteurs d'atlas : on ne peut pas relever une tranche isolée, il faut relever la plage, parce
que le code y avance au pointeur et pourrait déborder d'une tranche sur la suivante.

```
0x00564558: 0703 0103 0702 0102 0701 0601 0201 0101
0x00564568: 0707 0107 0706 0702 0701 0601 0201 0101
0x00564578: 0707 0607 0507 0706 0702 0701 0601 0501
0x00564588: 0707 0607 0207 0107 0706 0702 0701 0101
0x00564598: 0707 0607 0207 0107 0706 0106 0705 0105
0x005645A8: 0707 0607 0207 0107 0106 0102 0701 0101
0x005645B8: 0307 0207 0107 0106 0102 0301 0201 0101
0x005645C8: 0707 0107 0106 0102 0701 0601 0201 0101
```

Chaque tranche est une suite de **huit clés de seize bits** `rang << 8 | colonne`, dans l'ordre
exact où le balayage visite ses 49 cellules (rang 7 puis 6 …, colonne 7 puis 6 … à l'intérieur).
Ce sont les cellules que l'octant de cap met hors du cône de vue — les coins du carré, taillés
différemment selon l'orientation.

### Trois lecteurs, dont un défectueux

`FUN_00516fc0` (le balayage du décor), `FUN_0051fa40` et `FUN_00521000` (deux des trois listes de
morceaux, qui refont le balayage **à l'intérieur** de chaque morceau) lisent tous les trois ces
deux tables. Les deux derniers consomment leurs huit clés ; le premier n'en consomme qu'une, parce
qu'il lit les suivantes sur un octet au lieu de deux. Voir **LB-009**.

### La huitième clé de la huitième tranche tombe dans la table de pointeurs (2026-08-25)

Les huit octants consomment **tous** huit clés, et chacun lit d'avance la première clé de la tranche
suivante. Pour les sept premiers, la tranche suivante existe. Pour le huitième — `0x005645c8` — il
n'y en a pas : les deux octets lus sont ceux de `0x005645d8`, c'est-à-dire `58 45`, la moitié basse
du premier pointeur. La clé débordée vaut donc **`0x4558`**, et elle n'est jamais consultée.

C'est ce que `GameMemory.NextCellSkipKey` restitue, et c'est ce qui rend la plage indissociable :
**tout lecteur de ces tranches doit passer par cette aide**, y compris `FUN_00521000` quand il sera
transcrit — il consomme ses huit clés comme `FUN_0051fa40` et débordera pareil. Voir **LB-013**.

### Dans OpenForesia

`GameMemory.s_CellSweepCorner_00564548` et `GameMemory.s_CellSkipTables_00564558` — deux vues
statiques, dans `Memory/GameMemory.cs` plutôt que chez l'un des lecteurs, précisément parce qu'il y
en a trois répartis sur deux fichiers.

## `0x0056d340` — quarante-huit masques d'état, un par minuteur (2026-08-23)

Relevés avec `OpenForesia.Tools -- at`, dans `.data`. Quarante-huit dwords, `0x0056d340` à
`0x0056d400` — la borne vient du code, pas d'une supposition : `FUN_0053aa10` avance de quatre en
quatre **tant que l'adresse reste sous `0x0056d400`**.

Ils sont lus en parallèle des quarante-huit `short` que le bloc de statistiques porte à partir de
`+0x80`. Quand le minuteur d'indice *i* retombe à zéro, `masques[i]` est effacé du mot d'état
`+0x40` du même bloc.

```
+0x00  08000000  02000000  04000000  00000000
+0x10  00000000  00000000  00000000  00000004
+0x20  00000008  00000010  00000020  00000040
+0x30  00000080  00000100  00000200  00000400
+0x40  00000800  00001000  00002000  00004000
+0x50  00008000  00010000  00020000  00040000
+0x60  00080000  00100000  00200000  10000000
+0x70  00400000  00000000  00000000  00000000
+0x80  00000000  01000000  00000000  00000000
+0x90  … (zéro jusqu'à la fin)
```

Onze entrées valent zéro : leur minuteur décompte sans rien lever à l'échéance. Trois d'entre elles
sont précisément les minuteurs que la boucle traite à part parce qu'ils agissent **tant qu'ils
tournent** — `+0xbc` et `+0xbe` ajoutent le pas de régénération de `+0xfc`/`+0xfe` aux points de vie
et de magie, `+0xc2` en ajoute vingt à taux fixe.

### Dans OpenForesia

`s_StatusTimerMasks_0056d340`, dans `Screens/State03/WorldMapTick.cs` — un seul lecteur, donc chez
lui.

---

## `0x00551814` — le message de développeur resté dans le binaire (2026-08-24)

Le bloc d'étiquettes que `FUN_00473a70` (sous-types 6 et 7 de la table d'entités 0x0056adc8)
affiche. Relevé **verbatim** dans `Legacy/Binaries/Blaze.exe`, `.data`, décalage fichier
`0x0014FE14`. **Soixante-six octets** jusqu'au terminateur, plus deux de bourrage jusqu'à la
frontière de quatre.

```
1f d0 20 08 00 0c 95 d0  8a 4a 82 ab 82 cc 83 68
83 41 82 cd 8c bb 8d dd  8e 67 97 70 8f 6f 97 88
82 dc 82 b9 82 f1 81 42  8d a1 88 e4 82 dc 82 c5
98 41 97 8d 82 ad 82 be  82 b3 82 a2 81 42 0f 08
01 00                    (00 00)
```

Décodé en Shift-JIS :

> 片開きのドアは現在使用出来ません。今井まで連絡ください。

« La porte à un battant n'est pas utilisable pour l'instant. Contactez Imai. »

**今井 est un nom propre** — celui d'un membre de l'équipe. Ce n'est pas une phrase de jeu oubliée
en japonais : c'est une **note interne** laissée à qui construisait les niveaux, expédiée telle
quelle dans la version vendue. Les sous-types 6 et 7 sont donc une porte **inachevée**, et le texte
n'a jamais été traduit parce que personne n'était censé le lire. Cela répond aussi à la question
posée le 2026-08-24 : « pourquoi du Shift-JIS dans le binaire français ? »

### La longueur ne se lit pas dans un en-tête

C'est l'erreur qui a laissé ce bloc en lacune deux jours, sur la foi de ses huit premiers octets
pris pour « un en-tête, comme les autres blocs d'étiquettes ». Il n'y a pas d'en-tête. Un bloc
d'étiquettes est un **flux** :

* tout octet ≥ `0x20` est du texte (cp1252 ou Shift-JIS selon le jeu de caractères) ;
* tout octet < `0x20` est un code de contrôle, qui consomme un nombre d'opérandes propre ;
* le flux s'arrête au premier `0x00` **qui n'est pas une opérande**.

La longueur se mesure donc en **parcourant** le flux. Le nombre d'opérandes de chaque code se lit
directement dans la table de la passe 1, `PTR_LAB_0056e870`, dont quatre entrées sont des talons
d'avance pure :

| Talon | Opérandes |
|---|---|
| `0x00543940` | 0 |
| `0x00543950` | 1 |
| `0x00543960` | 2 |
| `0x00543970` | 3 |

Les codes qui ont un vrai corps portent leur compte dans ce corps — `0x0f` (`0x00544e50`) n'en
consomme aucune, `0x1f` (`0x00545120`) en consomme deux.

Ici, **deux `0x00` sont des opérandes** avant celui qui termine : s'arrêter au premier aurait donné
cinq octets au lieu de soixante-six.

### Ce que le flux fait, code par code

| Octets | Code | Effet |
|---|---|---|
| `1f d0 20` | `0x1f` | pose le champ de `+0x08` à deux `short` : la boîte se place en (0xd0, 0x20) |
| `08 00` | `0x08` | **gèle** le monde |
| `0c` | `0x0c` | ouvre la boîte — c'est lui qui `ClearOTagR` la table de `+0x68` |
| 56 octets | — | 28 paires Shift-JIS |
| `0f` | `0x0f` | **attend le joueur** en montrant le curseur |
| `08 01` | `0x08` | **dégèle** |
| `00` | — | fin de flux |

Une scénette complète, donc, et non un simple libellé. Les huit gestionnaires que ces codes
atteignent sont tous reconstruits, dans les deux passes : le bloc traverse le moteur de texte sans
lacune.

---

## `0x00551640` — la plage EntityParams n'était pas nulle au chargement (2026-08-25)

Les 0x200 octets de `0x00551640` étaient modélisés par une `AllocZeroed`, sur l'hypothèse que les
onze initialiseurs (`Init_0045a220` et sa famille) les remplissaient entièrement. **Faux** : la
plage est sous la frontière `0x00571400`, donc réellement présente dans le fichier
(décalage `0x0014FC40`), et le binaire y porte du contenu.

Les initialiseurs réécrivent bien les champs numériques — et avec exactement les mêmes valeurs :
`Init_0045a220` pose `0x9c4` en `0x00551660`, et le binaire y porte déjà `c4 09 00 00`. Mais ils ne
touchent pas aux **trois flux d'étiquettes** noyés dans la plage.

| Adresse | Flux | Shift-JIS | Sens |
|---|---|---|---|
| `0x00551648` | `1f d0 20 · 08 00 · 0c · … · 0f · 08 01 · 00` | 開かない！？ | « ça ne s'ouvre pas ! » |
| `0x00551720` | `08 00 · 0c · … · 0f · 08 01 · 00` | 遠くで音がした。 | « un bruit, au loin » |
| `0x00551814` | voir la section dédiée ci-dessus | 片開きのドア… | la note à Imai |

Les deux premiers sont inédits. Ils ont la même grammaire que celui de `0x00551814` — placement,
gel, ouverture de boîte, texte, attente du joueur, dégel — et le second n'a pas de code `0x1f`,
donc pas de placement explicite : la boîte reste où elle était.

### Ce qui l'a mis au jour

`FUN_00445860` pose `&DAT_00551640` en `+0x3c` de son entité. C'est un **pointeur de donnée**, pas
un drapeau, et il désignait 512 octets nuls.

### Les huit octets d'en-tête

```
f0 80 0f 0f  01 00 11 00
```

Ils précèdent le flux de `0x00551648` et **ne sont écrits par personne**. TODO(REVERSE) : leur rôle
n'est pas établi. Un en-tête de boîte, vraisemblablement — le flux du développeur en `0x00551814`
n'en a pas et se lit directement, donc la différence tient à qui pointe où.

### La leçon

Une plage sous `0x00571400` modélisée par `AllocZeroed` est une **hypothèse**, pas un fait. Elle ne
tient que si le binaire y porte réellement des zéros, ce que `at` dit en une commande. Deux des huit
blocs adressés par décalage portent la mention « vérifiée nulle » (`s_EntityState_00571500`,
`s_ViewParams_00571270`) ; les autres ne l'ont jamais été.

### Dans OpenForesia

`BuildEntityParams_00551640`, dans `Memory/GameMemory.cs` — les 0x200 octets semés verbatim.

---

## `0x005de538` — un global lu à trois largeurs (2026-08-24)

L'indice du membre de groupe « actif ». **Un octet** dans le binaire : les huit écrivains de
`Blaze.c` y écrivent tous un `byte` ou zéro, et la carte des symboles lui donne quatre octets
seulement par alignement.

Le code le **lit** en revanche de trois façons différentes, et la reconstruction transcrit chacune
telle quelle plutôt que de les unifier derrière un champ `byte` :

| Forme dans le binaire | Sites reconstruits |
|---|---|
| octet — `mov cl,[5DE538h]` puis `xor eax,eax ; mov al,cl` | `FUN_00472770` (`ActivePartySlot_00472770`) |
| double mot masqué — `… & 0xff` | `Engine/Input.cs`, `Engine/Resources.cs`, `Screens/State02/Destination.cs` |
| double mot nu | deux sites d'`Engine/UiWindow.cs` |

Les trois donnent le même résultat tant que la valeur reste sous 256, ce qui est toujours le cas.
Elles se distinguent quand même dans la transcription : c'est le binaire qui varie, et l'unification
effacerait l'information.

---

## `PTR_LAB_00551c30` — la table statique des gestionnaires d'entité de `FUN_0047aa70` (2026-08-24)

**192 entrées de quatre octets**, relevées verbatim dans `Legacy/Binaries/Blaze.exe` (`.data`,
`at 0x00551c30 768`). C'est la table que le répartiteur de `FUN_0047aa70` emprunte pour tout
sous-type `< 0xC0` ; au-dessus, il passe par la table installée par zone `0x0056adc8`.

**Aucune de ses cibles n'est dans `Blaze.c`** — Ghidra ne les a vues que prises en adresse ici,
d'où le préfixe `PTR_LAB_`. Elles se répartissent en deux bandes, `0x0044xxxx` et
`0x00475xxx`..`0x00482xxx`, et comptent **124 adresses distinctes**.

### Le créneau « sous-type inconnu » n'est pas vide

**Soixante-neuf des 192 créneaux pointent sur `0x0047ADC0`** :

```
0047adc0  8b442404      mov   eax,[esp+4]      ; l'enregistrement
0047adc4  c70000000000  mov   dword [eax],0    ; son mot d'état part à zéro
0047adca  c3            ret
```

Ce n'est **pas** un `ret` nu comme la souche `0x00401FA0` de l'autre table : effacer le mot d'état
éteint le bit `0x80000000`, que la passe relit juste après pour rendre le créneau au pool. C'est un
**désengendrement**. Le transcrire fait passer 69 créneaux sur 192 d'un plantage à un comportement
juste, pour trois instructions.

### La convention d'appel, que Ghidra donne à l'envers

La pseudo-C écrit `(*(code *)…)()`, sans argument, et ce dépôt l'a répété. C'est faux. Le `push` est
fait **avant** le branchement, donc les deux tables reçoivent le même unique argument :

```
0047aa8e  push esi                      ; l'enregistrement
0047aa92  cmp  eax,0C0h
0047aa97  jge  0047AAA2
0047aa99  call dword [eax*4+551C30h]    ; table basse
0047aaa2  call dword [eax*4+56ADC8h]    ; table haute
0047aaa9  add  esp,4                    ; __cdecl, un argument, commun aux deux
```

La branche basse a donc exactement la convention de la branche haute. C'est le troisième argument
perdu par Ghidra rencontré ce jour-là, après `FUN_00472e80` et la famille `FUN_00468080`.

### Ce qui reste

**123 corps réels**, tous absents de `Blaze.c`, à relever au désassembleur. C'est un mur, pas une
lacune : il se franchit un corps à la fois, et cette table est la carte qui dit lequel. Les deux
passes sœurs (`FUN_0047ab70` → `0x00551f30`, `FUN_0047acf0` → `0x00552230`) ont la même forme et
n'ont pas encore été relevées.

---

## `PTR_LAB_00551f30` — la table statique de `FUN_0047ab70` (2026-08-24)

**192 entrées de quatre octets**, relevées verbatim (`at 0x00551f30 768`). Elle commence exactement
où `PTR_LAB_00551c30` s'achève : les trois tables de 0x300 octets — `0x00551c30`, `0x00551f30`,
`0x00552230` — **se suivent sans trou**, ce qui donne au passage la borne de chacune.

**84 cibles distinctes**, toutes absentes de `Blaze.c`.

### Les deux tables sont disjointes

Elles n'ont **qu'une seule cible en commun**, la souche `0x0047ADC0`, et **pas un seul gestionnaire
réel**. Ce ne sont donc pas deux vues d'un même jeu de comportements indexé différemment : ce sont
deux familles d'entités distinctes. La passe elle-même le confirme — `FUN_0047ab70` raccroche ses
entités à la grille spatiale, `FUN_0047aa70` est la seule des six à ne rien y raccrocher.

### Une table beaucoup plus creuse que sa sœur

**109 des 192 créneaux** pointent sur la souche de désengendrement, contre 69 dans `0x00551c30` :
plus de la moitié de l'espace d'indices est vide et éteint ce qui s'y présente. Les 60 premiers
créneaux le sont d'un bloc, ce qui laisse penser que cette famille numérote ses sous-types à partir
de 0x3c.

Restent **83 corps réels**.

### Le répartiteur a la même forme

```
0047ab95  and  byte [esi],7Fh              ; esi = 0x005edee0 + 0x36 → record[0x36] &= 0x7f
0047ab9d  push edi                          ; l'enregistrement, AVANT le branchement
0047ab9e  cmp  eax,0C0h
0047aba5  call dword [eax*4+551F30h]        ; table basse
0047abae  call dword [eax*4+56AEC8h]        ; table haute
0047abb5  add  esp,4                        ; __cdecl, un argument
```

Le `lea edi,[esi-36h]` du prologue vaut confirmation au passage du `record[0x36] &= 0x7f` de la
transcription : Ghidra le rendait par un décalage négatif autour d'un pointeur biaisé, la même
figure que la boucle de `FUN_0040e640` autour de `&DAT_005e93f9`.

---

## `PTR_LAB_00552230` — la table statique de `FUN_0047acf0`, et le bilan des trois (2026-08-24)

**192 entrées de quatre octets** (`at 0x00552230 768`), la troisième et dernière du bloc contigu.
**68 cibles distinctes**, dont la souche. La plus creuse des trois : **125 créneaux sur 192**
éteignent ce qui s'y présente.

Le répartiteur a la forme des deux autres, avec le même pointeur biaisé
(`esi = 0x005f2d40 + 0x36`) :

```
0047ad15  and  byte [esi],7Fh
0047ad1d  push edi                          ; l'enregistrement, AVANT le branchement
0047ad1e  cmp  eax,0C0h
0047ad25  call dword [eax*4+552230h]
```

C'est la **seule des trois dont un créneau soit atteint par un chemin déjà reconstruit** :
`SpawnStatusEffectEntity_0047AC40` engendre dans `0x005f2d40` en posant son sous-type en `+0x05`,
l'octet même que cette passe aiguille. L'effet de dispersion du groupe d'`EvaluatePartyDispersal` y
entre avec `0x9d`, ce qui désigne `0x00445860` — un corps réel, décompilé dans `Blaze.c`, non
transcrit.

### Bilan des trois tables

| Table | Passe | Créneaux morts | Corps réels | dans `Blaze.c` | au désassembleur |
|---|---|---|---|---|---|
| `0x00551c30` | `FUN_0047aa70` | 69 / 192 | 123 | 63 | 60 |
| `0x00551f30` | `FUN_0047ab70` | 109 / 192 | 83 | 26 | 57 |
| `0x00552230` | `FUN_0047acf0` | 125 / 192 | 67 | 32 | 35 |
| **total** | | **303 / 576** | **273** | **121** | **152** |

Les trois sont **disjointes deux à deux** : leur seule cible commune est la souche `0x0047ADC0`.
303 des 576 créneaux — plus de la moitié — sont des trous qui éteignent ce qui s'y présente.

### `PTR_LAB_` ne veut pas dire « absent de `Blaze.c` »

C'est la correction à retenir. Le préfixe dit seulement que **Ghidra n'a pas reconnu les cibles
comme des fonctions depuis cette table** ; la plupart sont décompilées quand même, atteintes par
d'autres appelants. **121 des 273 corps y sont**, et relèvent de la transcription ordinaire.

L'affirmation contraire, écrite ici le même jour, venait d'un **échantillon de sept adresses**
généralisé à 273. Le compte exact tient en deux commandes :

```bash
grep -oE "^/\* FUN_[0-9a-f]{8} @" Legacy/Decompiled/Blaze.c | sed 's|^/\* FUN_||; s| @$||' | sort -u > have.txt
comm -12 cibles.txt have.txt | wc -l
```

---

## `0x00566c48` — le bloc d'ecran, et son contenu **initial** (2026-08-25)

Relevé avec `OpenForesia.Tools -- at`. Quarante octets, jusqu'a `DAT_00566c70` qui suit :

```
0x00566C48: 0000 0000 ff3f ff3f 0000 0000 ff3f ff3f
0x00566C58: ff3f ff3f 0100 0000 0100 0000 0000 0000
0x00566C68: 0000 0000 0000 0000
```

| champ | valeur initiale |
|---|---|
| `+0x00` | 0 |
| `+0x04` | `0x3fff3fff` |
| `+0x08` | 0 |
| `+0x0c` | `0x3fff3fff` |
| `+0x10` et `+0x12` | `0x3fff` chacun — deux `short`, c'est-a-dire `DAT_00566c58` et `DAT_00566c5a` |
| `+0x14`, `+0x18` | 1 |
| `+0x1c` .. `+0x24` | 0 |

**Pourquoi ce releve a fini par compter.** Le bloc etait alloue **a zero** dans la reconstruction, ce
qui etait juste tant que son unique lecteur, `SetupMenuScreen`, ecrivait tous ses champs avant de le
publier. `EntityHandler_00404E50` (2026-08-25) en **lit** deux : il decompte 300 par image sur
`+0x10` et `+0x12` et n'agit que quand les deux atteignent zero. Sur un bloc a zero, ce decompte
serait deja fini a la premiere image — le changement de musique serait instantane au lieu de prendre
la cinquantaine d'images que `0x3fff / 300` demande.

**La lecon generale** : un bloc `.data` alloue a zero n'est faux que le jour ou quelqu'un le **lit**
avant de l'ecrire. Tant qu'il n'y a qu'un ecrivain complet, rien ne le montre. C'est le meme genre de
dette silencieuse que les huit champs `= Arena(0x…)` du decoupage de 2026-08-19.

---

## La colonne d'ecart n'est PAS une taille (2026-08-25)

**Piege paye une fois, note ici pour ne pas l'etre deux.** La troisieme colonne de la carte des
symboles reconstruits est **l'ecart jusqu'au symbole declare suivant**, pas la taille de
l'allocation. `s_Particles_005de824` y porte `6060` alors qu'elle alloue **4992** octets
(0x20 particules de 0x9c) : l'ecart couvre 900 octets de globales qui ne sont simplement pas encore
declarees.

En lisant cette colonne comme une taille, on conclut qu'une adresse tombe dans une allocation alors
qu'elle est dehors — et on renonce a un travail pour une mauvaise raison. **Pour trancher une
appartenance, il faut la taille reelle cote C#**, pas l'ecart :

| adresse | symbole en dessous | ecart affiche | taille reelle | verdict |
|---|---|---|---|---|
| `0x005dff28` | `s_Particles_005de824` | 6060 | 4992 | **dehors** |
| `0x0056dc90` | `s_StatusTimerMasks_0056d340` | 2584 | 192 | **dehors** |

## `0x005dfb8c` — la file de sons differes, et son chevauchement nominal

`FUN_0053dbe0` empile et `FUN_0053dc30` sert une file de sons differes : un compteur en
`0x005dfb8c` qui reboucle a 0x20, puis **32 enregistrements de 0x14 octets a partir de
`0x005dfb90`**, soit `0x005dfb90`..`0x005dfe10`. Taille confirmee trois fois — le masque
`0xffffff1f` du compteur, la boucle de service (`local_4 = 0x20`), et le pas `* 0x14`.

**Elle commence 0x14 octets avant la fin nominale de `s_Particles_005de824`** (`0x005dfba4`). Les
deux etendues se chevauchent donc d'un enregistrement sur le papier.

**Sans consequence en pratique** : le code des particules ne touche que les offsets `0x00`..`0x27`
de chaque entree de `0x9c` — les deux boucles n'ecrivent pas au-dela de `piVar11[8]`, `ProjectPoint`
ne lit que les trois entiers de position et `EmitParticle` que `+0x20`. Le dernier octet ecrit par
une particule est `0x005dfb2f`, 97 octets avant la file.

**Ce qu'il ne faut surtout pas faire** : modeliser la file de sons comme une vue dans
`s_Particles_005de824`. Ce serait deux noms pour une adresse — le defaut que `MemoryLayoutTests`
existe pour nommer. Elle prend sa propre allocation le jour ou elle est transcrite.

## `0x005de548`, `0x005dff28`, `0x005dff5c`..`0x005dff7c`, `0x0056dc90` — l'etat du panneau des verbes 57/58

Onze globales posees par `FUN_0053dea0` et `FUN_0053df20`, lues par `FUN_0053e0a0` (non transcrite).
Declarees dans `Screens/State03/WorldMapScript.cs` chez leur seul ecrivain transcrit.

| adresse | largeur | role |
|---|---|---|
| `0x005de548` | octet | le **mode** du panneau : 0 (verbe 57) ou 1 (verbe 58) ; toute autre valeur fait sortir `FUN_0053e0a0` aussitot |
| `0x005dff28` | octet | l'index de membre du groupe sur lequel il porte |
| `0x005dff5c`..`0x005dff7c` | 9 dwords | l'etat de curseur, remis a zero d'un bloc a l'ouverture |
| `0x0056dc90` | dword | le sous-etat, **-1** a l'ouverture |

`0x005dff64` est le seul des neuf traite a part : le verbe 57 y met l'octet `+0x2bc` du bloc du
personnage (`MainMem + membre * 0x2000 + 0xf02bc`), le verbe 58 y met zero.

## `0x00552930`..`0x00552978` — des gabarits de portee d'ennemi, en dur dans `.data` (2026-08-29)

Cinq enregistrements de six `short`, de la meme forme que
`s_MeleeProbes_00562cf0` — le gabarit que `TestMeleeReach_00535A90` croise avec chaque membre du
groupe. Releves au verbe `at` en transcrivant `FUN_0049B840`, le Grizzly.

| adresse | `[0]` | `[1]` | `[2]` | `[3]` | `[4]` | `[5]` | attribution |
|---|---:|---:|---:|---:|---:|---:|---|
| `0x00552930` | 0 | `0x32` | `0x78` | `0x96` | `0x0100` | `0xfa` | non attribuee |
| `0x0055293c` | 0 | `0x32` | `0x5a` | `0x96` | 0 | `0xc8` | non attribuee |
| `0x00552948` | 0 | `0x32` | `0x5a` | `0x96` | 0 | `0xc8` | non attribuee |
| `0x00552960` | 0 | `0x1e` | `0x46` | `0x96` | `-0x100` | `0x50` | **`FUN_0049B840`, la griffe** |
| `0x0055296c` | 0 | `0x1e` | `0x32` | `0x96` | 0 | `0x50` | **`FUN_0049B840`, le bond** |

Le cinquieme terme est un **decalage vertical** : negatif, le gabarit se leve — la griffe du Grizzly
s'abat d'en haut, son bond frappe au ras du sol.

**Deux bornes, lues dans la donnee et non dans les sites d'appel.** `0x00552954` porte
`00 00 00 00 0e 02 14 00 1b 00 00 00`, qui n'a plus la forme d'une sonde : la serie de trois
s'arrete donc a `0x00552954`. De meme `0x00552978` (`03 00 08 00 0a 00 10 00 0c 02 03 00`) ferme la
paire du Grizzly. Les trois premieres appartiennent a des routines du meme bloc non encore
transcrites, et **il ne faut pas les deviner** : celle qui les lira les nommera.

**Ce que ce releve apprend sur la famille.** Les six routines d'intelligence ecrites avant le
Grizzly construisaient toutes leur sonde **sur la pile**, a chaque image. Le Grizzly est le premier
a la lire dans `.data`. Une routine d'entite de scene peut donc faire l'un ou l'autre, et un
`push <adresse>` juste avant `call 00519C30` est le signe qu'il faut sortir le verbe `at` plutot que
de chercher six `mov word [esp+…]` qui n'existent pas.

## `0x00551a08` — les sept images de l'éclaboussure de tir (2026-08-29)

Sept enregistrements de huit octets, même forme que `s_LootDescriptors_005646E0` : le descripteur
d'atlas que `FUN_0047a270`, la trajectoire du projectile, passe à `FUN_0047ac40` en faisant naître
l'effet d'impact `0xbd`.

| adresse | octets |
|---|---|
| `0x00551a08` | `c0 b8 18 18 01 02 10 00` |
| `0x00551a10` | `d8 b8 18 18 01 02 10 00` |
| `0x00551a18` | `60 d0 18 18 01 02 10 00` |
| `0x00551a20` | `78 d0 18 18 01 02 10 00` |
| `0x00551a28` | `90 d0 18 18 01 02 10 00` |
| `0x00551a30` | `a8 d0 18 18 01 02 10 00` |
| `0x00551a38` | `c0 d0 18 18 01 02 10 00` |

**La borne se lit dans le CODE, pas seulement dans la donnée.** `FUN_00476320` compte `+0x40`
jusqu'à 7 avant de s'éteindre, et `FUN_0047ac40` pose ce champ à zéro : il faut donc exactement
sept entrées. La donnée le confirme — la huitième (`0x00551a40`) a un cinquième octet à 0 et un
huitième à 1, une autre forme.

C'est la deuxième fois que la longueur d'une table de descripteurs se déduit d'un compteur du corps
plutôt que de la mise en page ; l'autre est `0x005515a0`, l'étincelle, dont le `+0x40` cycle sur
trois. Le grain de poussière de `0x0054f118`, lui, n'incrémente jamais son indice : une seule
entrée.

## `0x0054f0e0` — les sept images de la retombée sombre, et une correction sur `0x0054f118` (2026-08-29)

Sept enregistrements de huit octets, que `FUN_00404310` (sous-type `0x34`) et `FUN_00403d60`
(sous-type `0x32`) posent tous deux en `+0x3c`. La borne se lit dans le code, comme pour
`0x00551a08` : les deux corps comptent `+0x40` jusqu'à 7 avant de s'éteindre.

| adresse | octets |
|---|---|
| `0x0054f0e0` | `c0 b8 17 17 01 02 10 00` |
| `0x0054f0e8` | `d8 b8 17 17 01 02 10 00` |
| `0x0054f0f0` | `60 d0 17 17 01 02 10 00` |
| `0x0054f0f8` | `78 d0 17 17 01 02 10 00` |
| `0x0054f100` | `90 d0 17 17 01 02 10 00` |
| `0x0054f108` | `a8 d0 17 17 01 02 10 00` |
| `0x0054f110` | `c0 d0 17 17 01 02 10 00` |

**C'est la même séquence que `0x00551a08`, dans une autre planche.** Les sept déplacements sont
identiques — `c0b8`, `d8b8`, `60d0`, `78d0`, `90d0`, `a8d0`, `c0d0` — et seule la moitié haute
change : `1717` ici, `1818` pour l'éclaboussure de tir. Le jeu réutilise une même animation de sept
images sur deux planches de texture.

### La correction sur `0x0054f118`

La table s'arrête exactement où commence celle du grain de poussière, et cette adjacence a fait
apparaître une **erreur de relevé** dans `s_DustDescriptors_0054F118` : elle portait
`18 a8 17 17 01 02 10 00`, qui sont les octets de `0x0054f120`, pas de `0x0054f118`. Le binaire dit :

```
0x0054f118 : 00 a8 17 17 01 02 10 00
0x0054f120 : 18 a8 17 17 01 02 10 00
```

Vérifié deux fois, au verbe `at` puis à `od` sur l'offset fichier `0x0014d718`. Corrigé le
2026-08-29. Le grain de poussière pointait donc une image trop loin depuis sa transcription — un
décalage d'une seule case, invisible à la compilation, et que rien n'aurait signalé sans la lecture
de la table voisine.

**À retenir :** un descripteur d'atlas relevé isolément n'a aucun garde-fou. Lire la table qui le
précède ou le suit est le seul contrôle bon marché.

## `0x00551a60` et `0x00551b00` — deux atlas de plus (2026-08-29)

Passés en troisième argument de `FUN_0047ac40` par les deux naissances `FUN_004758c0` et
`FUN_00483450`.

`0x00551a60` — **trois** enregistrements, et la borne se lit dans le corps du consommateur :
`FUN_00475910` pose `+0x40 = rand % 3`. La donnée le confirme, le quatrième mot change de planche.

| adresse | octets |
|---|---|
| `0x00551a60` | `00 30 18 18 01 02 10 00` |
| `0x00551a68` | `18 30 18 18 01 02 10 00` |
| `0x00551a70` | `30 30 18 18 01 02 10 00` |

`0x00551b00` — **cinq** enregistrements, bornés autrement : ici le consommateur est résolu par
sous-zone (`0xc0 + n`), donc **aucun compteur du code ne donne la longueur**. Ce qui la donne est le
changement de planche à `0x00551b28` (`a8 18 18 18`). À reprendre si le gestionnaire réel se révèle
en compter plus.

| adresse | octets |
|---|---|
| `0x00551b00` | `10 68 10 10 00 00 10 00` |
| `0x00551b08` | `20 68 10 10 00 00 10 00` |
| `0x00551b10` | `00 68 10 10 01 02 10 00` |
| `0x00551b18` | `10 68 10 10 01 02 10 00` |
| `0x00551b20` | `20 68 10 10 01 02 10 00` |

Les deux premiers ont `00 00` en cinquième et sixième octets là où les trois suivants ont `01 02` :
deux groupes dans une même table, ce qui est une raison de plus de ne pas trancher la longueur sans
le corps consommateur.

### `0x00551af8` — la borne gauche manquait d'une ligne (2026-08-31)

**Stalagtite** (`FUN_00480600`, sous-type `0x17`) passe le même genre d'atlas à `FUN_0047ac40`, et
sa base est `0x00551af8`, pas `0x00551b00` :

```
00480711  mov  ecx,3
00480716  idiv ecx
00480718  lea  edx,[edx*8+551AF8h]
```

Trois enregistrements, la longueur lue dans le corps comme pour `0x00551a60` — mais **les deux
derniers sont les deux premiers de `0x00551b00`** :

| adresse | octets |
|---|---|
| `0x00551af8` | `00 68 10 10 00 00 10 00` |
| `0x00551b00` | `10 68 10 10 00 00 10 00` |
| `0x00551b08` | `20 68 10 10 00 00 10 00` |

Le groupe `00 00` compte donc **trois** lignes et commence une ligne plus haut que le relevé du
2026-08-29 ne pouvait le voir : `0x00551b00` n'était pas un début de table, c'était le milieu d'un
groupe. Les deux consommateurs entrent dans le même bloc de `.data` à deux bases distantes de huit
octets, l'un prenant les trois `00 00`, l'autre les deux derniers `00 00` plus les trois `01 02`.

**Ce que ça change pour la méthode** : deux bases différentes dans un même bloc contigu ne sont pas
une contradiction à arbitrer, et un « début de table » établi par le seul changement de planche
reste une borne *droite* — la gauche ne se prouve que par un `lea` d'appelant.

### `0x00551b88` — le même cas, une deuxième fois (2026-08-31)

Le démon de **Satanas** (`FUN_00481250`, sous-type `0x88`) tire lui aussi `rand % 3` dans un atlas,
et sa base tombe au milieu d'un groupe de quatre lignes identiques de forme :

| adresse | octets | |
|---|---|---|
| `0x00551b80` | `d8 d0 0f 0f 00 00 10 00` | même planche, **aucun consommateur connu** |
| `0x00551b88` | `d8 e0 0f 0f 00 00 10 00` | ← la base du démon |
| `0x00551b90` | `d8 f0 0f 0f 00 00 10 00` | |
| `0x00551b98` | `e8 f0 0f 0f 00 00 10 00` | |

Deux relevés indépendants, deux fois le même piège : la borne gauche d'un atlas ne se lit pas dans
la donnée. Ici c'est la ligne `0x00551b80` qui reste orpheline — la chercher plutôt que la rattacher.

## `0x00552588` — le sixième script de sons différés (2026-08-31)

Mis en file par `FUN_00480b90` (**Satanas**) à la fin de sa première image. Même format que les
cinq précédents : huit octets par pas, l'octet `+1` est le délai, un délai nul termine.

| adresse | octets | |
|---|---|---|
| `0x00552588` | `31 01 01 00 94 01 00 12` | son `0x31`, une image d'attente |
| `0x00552590` | `12 00 01 00 53 06 00 0f` | son `0x12`, délai nul : fin |

## `0x00552598` — le septième script, et le plus long (2026-08-31)

Mis en file par `FUN_00481720`, la spirale d'**Au sec**, à sa première image. **Cinq pas**, contre
deux ou trois pour les six précédents :

| adresse | octets | son | délai | hauteur |
|---|---|---:|---:|---:|
| `0x00552598` | `30 20 01 00 29 03 00 12` | `0x30` | 0x20 | `0x0329` |
| `0x005525a0` | `31 10 01 00 00 01 00 12` | `0x31` | 0x10 | `0x0100` |
| `0x005525a8` | `12 02 01 00 00 02 00 12` | `0x12` | 2 | `0x0200` |
| `0x005525b0` | `12 02 01 00 29 03 00 12` | `0x12` | 2 | `0x0329` |
| `0x005525b8` | `12 00 01 00 00 04 00 12` | `0x12` | **0** : fin | `0x0400` |

La file contiguë depuis `0x00552530` compte donc sept scripts relevés, jusqu'à `0x005525bf` ; le
huitième (`0x005525c0`, `1a 01 01 00 00 02 00 12`) reste à rencontrer.


## `0x00551ab8` — l'atlas de la gerbe `0x3c`, et une borne par champ (2026-08-29)

Passé en troisième argument de `FUN_0047ac40` par `FUN_004754c0`, le piège de coffre `0xb4`.

**Quatre** enregistrements. La borne se lit d'abord dans le corps du consommateur, comme pour
`0x005519d0` : `FUN_004755c0` pose `+0x40 = compteur / 2` et s'éteint dès que cet indice dépasse 3.

| adresse | octets |
|---|---|
| `0x00551ab8` | `40 88 20 20 01 02 10 02` |
| `0x00551ac0` | `60 88 20 20 01 02 10 02` |
| `0x00551ac8` | `80 88 20 20 01 02 10 04` |
| `0x00551ad0` | `a0 88 20 20 01 02 10 0b` |

**La donnée confirme la borne par le bas d'une façon nouvelle.** Les quatre enregistrements
reprennent exactement les mêmes tuiles `0x20 × 0x20` (`4088`, `6088`, `8088`, `a088`) que la plage
`0x00551a98` qui les précède sans trou : ni la planche ni la taille ne séparent les deux tables. Ce
qui les sépare est le **dernier octet**, nul dans toute la plage précédente et `02 02 04 0b` ici.

**À retenir :** une plage de descripteurs peut se borner par un champ qui change de valeur, pas
seulement par un changement de planche. Quand deux tables partagent leurs tuiles, comparer les huit
octets colonne par colonne — la colonne qui bouge est la frontière.

## `0x005515b8`, `0x005515c0`, `0x005519b8`, `0x00551868` — quatre relevés de la branche `0x99` / `0xbf` (2026-08-29)

### Les deux atlas de `FUN_004459e0`

Cette fonction a deux corps exclusifs, choisis par `+0x44` de l'entité, et chacun pose son propre
atlas en `+0x3c`.

| adresse | octets | borne |
|---|---|---|
| `0x005515b8` | `a0 a8 0f 0f 01 02 10 00` | **1** — le corps `+0x44 == 0` pose `+0x40 = 0` et n'y touche plus |
| `0x005515c0` | `70 78 0f 0f 01 02 10 00` | **4** — voir ci-dessous |
| `0x005515c8` | `80 78 0f 0f 01 02 10 00` | |
| `0x005515d0` | `90 78 0f 0f 01 02 10 00` | |
| `0x005515d8` | `a0 78 0f 0f 01 02 10 00` | |

`0x005515b8` est **seul de sa planche** : la plage qui le précède (`0x005515a0`) est en `d0`, celle
qui le suit en `78`. La donnée et le code disent donc la même chose.

**`0x005515c0` introduit une borne d'un genre nouveau : le plafond.** Le corps `+0x44 == 1` avance
`+0x40` d'un cran toutes les six sous-étapes, puis exécute `si l'incrémentation donne 4, remettre 3`.
Ce n'est pas un cycle — l'animation s'arrête sur sa dernière image et y reste. Quatre
enregistrements, et les quatre tuiles `7078`, `8078`, `9078`, `a078` se suivent exactement.

**À retenir :** un plafond borne une table aussi sûrement qu'un modulo, et se repère au motif
`inc ; cmp N ; si égal, écrire N-1`.

### `0x005519b8` — la borne écrite dans l'engendreur

| adresse | octets |
|---|---|
| `0x005519b8` | `20 c0 10 10 01 02 10 00` |
| `0x005519c0` | `00 d0 10 10 01 02 10 00` |
| `0x005519c8` | `10 d0 10 10 01 02 10 00` |

**Trois**, et pour la première fois la longueur n'est ni dans le consommateur ni dans un changement
de planche : `FUN_00475d40` pose `+0x1a = 3` sur l'entité **avant** l'appel d'engendrement, et le
consommateur (`FUN_00475fc0`) parcourt `+0x1a` enregistrements. Le même engendreur passe `+0x1a = 1`
pour son premier appel, sur l'atlas `0x00551ba0`.

La donnée suit : ce sont les trois derniers de la plage de six tuiles `0x10 × 0x10` commençant à
`0x005519a0`, et `0x005519d0` derrière change de planche.

### `0x00551868` — le son du coup qui ne passe pas

| adresse | octets |
|---|---|
| `0x00551868` | `03 00 01 00 00 01 00 12` |

Même forme que le script de piège `0x00551858` (identifiant en `+0`, délai du lot suivant en `+1`,
nombre de sons en `+2`, hauteur en `+4`, gain en `+6`) mais **un seul enregistrement** : son délai de
lot suivant est nul, ce qui termine le script dès la première lecture. C'est ce qui le sépare du
script de deux enregistrements qui le précède, et non un changement de valeur.

## Correction — `0x005519b8` était décalé d'un enregistrement (2026-08-29)

Le relevé publié plus haut le même jour donnait `20c0`, `00d0`, `10d0`. **C'est faux.** La lecture à
l'adresse exacte donne :

| adresse | octets |
|---|---|
| `0x005519b8` | `00 d0 10 10 01 02 10 00` |
| `0x005519c0` | `10 d0 10 10 01 02 10 00` |
| `0x005519c8` | `20 d0 10 10 01 02 10 00` |

L'erreur vient d'avoir compté les enregistrements à l'œil sur un affichage de seize octets par ligne
au lieu de demander le relevé **à l'adresse du descripteur**. La vérification coûte une commande :

```powershell
dotnet run --project OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x005519b8 24
```

**À retenir :** relever à l'adresse citée par le code, jamais à celle d'une plage qui la contient.
Le décalage ne se voit ni à la compilation ni dans la forme des données — les huit octets restent
plausibles.

## `0x005519a0`, `0x00551a40`, `0x00551a78` — les trois atlas de la famille de pièges (2026-08-29)

### `0x005519a0` — trois images, et une plage partagée

| adresse | octets |
|---|---|
| `0x005519a0` | `00 c0 10 10 01 02 10 00` |
| `0x005519a8` | `10 c0 10 10 01 02 10 00` |
| `0x005519b0` | `20 c0 10 10 01 02 10 00` |

Borné par `FUN_0047a880`, qui avance `+0x40` d'un cran par image et le remet à zéro dès qu'il atteint
3.

**Cette table et `0x005519b8` se partagent une seule plage contiguë** de six tuiles `0x10 × 0x10`
allant de `0x005519a0` à `0x005519d0`. Ni la planche, ni la taille, ni la queue des huit octets ne
les sépare : **seuls leurs deux consommateurs les bornent**, l'un à trois enregistrements par un
modulo, l'autre à trois par le `+0x1a` que son engendreur pose. C'est le cas de figure où relever un
descripteur isolément est le plus dangereux — et c'est précisément là que l'erreur ci-dessus s'est
produite.

### `0x00551a40` — quatre images, bornées deux fois

| adresse | octets |
|---|---|
| `0x00551a40` | `60 b8 18 18 00 02 10 01` |
| `0x00551a48` | `78 b8 18 18 00 02 10 01` |
| `0x00551a50` | `90 b8 18 18 00 02 10 01` |
| `0x00551a58` | `a8 b8 18 18 00 02 10 01` |

`FUN_004757a0` pose `+0x40 = (compteur / 2) & 3`. La donnée le confirme **deux fois** : la table
suivante (`0x00551a60`) change de tuile *et* de queue (`01 02 10 00` au lieu de `00 02 10 01`).
Quand les deux critères concordent, la borne ne fait pas de doute.

### `0x00551a78` — quatre images, et deux engendreurs

| adresse | octets |
|---|---|
| `0x00551a78` | `98 00 10 10 01 02 10 00` |
| `0x00551a80` | `a8 00 10 10 01 02 10 00` |
| `0x00551a88` | `b8 00 10 10 01 02 10 00` |
| `0x00551a90` | `c8 00 10 10 01 02 10 00` |

**Le seul atlas de la série que deux engendreurs indépendants se partagent** : `FUN_00475a30` le
donne à son enfant `0x40`, et `FUN_0053b580` au `0xbb` qu'il fait naître sur la victime. Les deux
consommateurs (`FUN_00475a80` et `FUN_004768a0`) posent tous deux `+0x40 = rand() & 3`, ce qui
donne la même borne par deux chemins. `0x00551a98` derrière passe en `0x20 × 0x20`.

## `0x0054f158` — un descripteur qui est aussi un séparateur (2026-08-29)

| adresse | octets |
|---|---|
| `0x0054f158` | `00 00 08 08 01 02 10 00` |

**Un seul enregistrement**, et c'est le cas où la borne se démontre au lieu de s'estimer :
`FUN_0047ac40` pose `+0x40 = 0` sur toute entité qu'elle fait naître, et `FUN_004038f0` — son
unique consommateur — n'écrit jamais ce champ. L'indice d'atlas reste nul pour la vie entière de
l'entité, donc aucun autre enregistrement n'est atteignable, quoi que contienne la suite.

La donnée est cohérente avec ça : la plage qui précède (`0x0054f138`..`0x0054f150`) est en
`0x0f × 0x0f`, celle qui suit (`0x0054f160`..`0x0054f178`) en `0x18 × 0x20`.

**Et le même motif reparaît à `0x0054f180`**, juste après cette seconde plage. Une tuile `8 × 8`
prise à l'origine de la planche, posée entre deux animations : `00 00 08 08 01 02 10 00` sert de
**séparateur** dans cette zone de `.data`. C'est une borne exploitable pour les plages voisines,
au même titre qu'un changement de planche.

## `0x0056d308` et `0x0056d310` — les deux tables de la montée de niveau (2026-08-29)

Relevées de longue date dans `WorldMapTick.cs`, portées ici parce que la première a changé de sens.
L'exploitation complète est dans `Documentation/Reference/MonteeDeNiveau.md`.

### `0x0056d308` — la caractéristique favorisée, **par élément**

Six octets : `00 01 04 03 02 05`, une permutation de 0..5. La caractéristique désignée voit son
plafond de progression augmenté d'un cinquième.

**L'indexeur est `+0x151`, qui porte l'élément et non une sous-classe.** Le symbole s'appelait
`s_SubClassFavouredStat_0056D308` jusqu'à ce jour ; le nom était une supposition. Ce qui tranche est
le remplissage du champ : `DebugParty` fait `record[0x151] = s_ClassBaseStats_0056c7e0[classe * 8 + 7]`
avec le commentaire « l'élément », et `LISEZMOI.md` du bestiaire avait déjà identifié cette huitième
colonne comme l'élément du personnage. Les éléments 6 et 7 ne sont pas dans la table : le code les
traite à part, en augmentant les six plafonds d'un vingtième.

**À retenir :** un nom de symbole qui contient une hypothèse doit être vérifié contre le
*remplissage* du champ, pas seulement contre son usage. Ici l'usage — « choisit une caractéristique
favorisée » — était compatible avec les deux lectures.

### `0x0056d310` — les chances de progression, huit dwords

| classe | dword | physique | mentale |
|---|---|---|---|
| 0 Guerrier, 6 Nain | `0x04280241` | `chance / 2 + 65` | `chance / 4 + 40` |
| 1 Prêtre, 3 Sorcier, 7 Fée | `0x023c0423` | `chance / 4 + 35` | `chance / 2 + 60` |
| 2 Voleur, 4 Chasseur, 5 Elfe | `0x03320332` | `chance / 3 + 50` | `chance / 3 + 50` |

Chaque dword se lit **quatre octets deux par deux** : `(base physique, diviseur physique, base
mentale, diviseur mental)`.

**Trois valeurs pour huit classes**, et le regroupement n'est pas arbitraire — les deux classes
purement physiques, les trois lanceurs de sorts, les trois hybrides. Le même partage apparaît dans
les masques d'équipement et dans les tables du bestiaire, ce qui le confirme de trois côtés.

## `0x0054f7a8` — les quatre images de l'éclaboussure du pas dans l'eau (2026-08-30)

Quatre enregistrements de huit octets, posés en `+0x3c` par `0x0040f7f0` — le gestionnaire du
sous-type `0xfe`, absent de `Blaze.c` et relevé au désassembleur le même jour
(`Legacy/Raw/Blaze/FUN_0040F7F0.asm`).

| adresse | octets | u, v, l, h |
|---|---|---|
| `0x0054f7a8` | `08 08 08 08 01 03 10 00` | 8, 8, 8, 8 |
| `0x0054f7b0` | `10 08 08 08 01 03 10 00` | 16, 8, 8, 8 |
| `0x0054f7b8` | `08 10 08 08 01 03 10 00` | 8, 16, 8, 8 |
| `0x0054f7c0` | `10 10 08 08 01 03 10 00` | 16, 16, 8, 8 |

**La borne se lit des deux côtés, ce qui est le cas le plus confortable.** Le corps compte `+0x40`
jusqu'à 4 avant d'éteindre l'entité — quatre images. Et la mise en page est d'accord : la plage qui
précède (`0x0054f778`..`0x0054f7a8`, les six images de `FUN_0040f8e0`) est en `0x18 x 0x18`, et
`DAT_0054f7c8`, symbole distinct dans `Blaze.c`, ouvre la suivante en `0x10 x 0x10`.

Les quatre tuiles sont les quatre coins d'un carré de `0x10` de côté à partir de `(8, 8)` : le jeu
lit le carré en Z, un coin par paire d'images. La sixième et la septième colonne (`01 03`, `10 00`)
sont celles de toute la famille — voir `0x0054f0e0` ci-dessus, qui porte `01 02`.
