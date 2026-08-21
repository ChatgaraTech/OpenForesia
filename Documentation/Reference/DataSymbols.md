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

## Music filename table (TODO #30)

`PTR_s____Music_Mel00_01_WAV_00566cc8` — a pointer table indexed by `PlayMusicTrack`
(`FUN_00529ad0`), call sites reaching at least index `0x26`. Entries descend in memory:

| Index | Pointer | Value |
|---|---|---|
| 0 | `0x00568430` | `@:/Music/Mel00_01.WAV` |
| 1 | `0x00568418` | `@:/Music/Mel00_02.WAV` |
| … | … | … |
| 7 | `0x0056838C` | `@:/Music/End01.WAV` |

Forward slashes, and drive-patched absolute paths like the sound bank — the CD's `Music\` directory
is at the root, not under `FRENCH\`. Dereference the remaining entries before reconstructing
`PlayMusicTrack`.

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

**`AU_00564A05`** is the only remaining uninterpreted blob here; `DAT_0056ACA8` moved to the
confirmed section below once `FUN_00532070` settled its reading.

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
| `0x00571500` | `0x500` | EntityState | `EntityState(va)` | `Memory/GameMemory.cs` |
| `0x005de030` | `0x080` | CameraBlock | `Camera(va)` | `Memory/GameMemory.cs` |
| `0x005de100` | `0x460` | ItemMenuArena | `ItemMenuState(va)` / `ItemMenuBytes(va)` | `Memory/GameMemory.cs` |
| `0x005e85c0` | `0x340` | PartyList | — | `Memory/GameMemory.cs` |
| `0x005e9000` | `0x1600` | GameArena | `Arena(va)` | `Memory/GameMemory.cs` |
| `0x005eca00` | `0x14200` | EntityArena | `Entity(va)` | `Memory/GameMemory.cs` |
| `0x006067c0` | `0x040` | ViewBlock | `View(va)` | `Memory/GameMemory.cs` |

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
| `0x00551640` | `s_EntityParams_00551640` | 53864 | `Memory/GameMemory.cs` |
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
| `0x00564540` | `s_Unknown_00564540` | 184 | `Screens/State03/WorldMap.cs` |
| `0x005645f8` | `s_WorldMapStep_005645f8` | 4 | `Screens/State03/WorldMap.cs` |
| `0x005645fc` | `s_Unknown_005645fc` | 84 | `Screens/State03/WorldMap.cs` |
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
| `0x00566cb0` | `s_UiOffsetY_00566cb0` | 6120 | `Screens/State00/Options.cs` |
| `0x00568498` | `s_SaveNameTemplate_00568498` | 120 | `Screens/State04/CharacterCreation.cs` |
| `0x00568510` | `s_ControllerBannerText_00568510` | 10136 | `Engine/SaveData.cs` |
| `0x0056aca8` | `s_ResourceFixupSchema_0056aca8` | 160 | `Engine/Resources.cs` |
| `0x0056ad48` | `s_VtSource_0056ad48` | **0** | `Engine/Resources.cs` |
| `0x0056ad48` | `s_Vt_0056ad48` | 128 | `Engine/Resources.cs` |
| `0x0056adc8` | `s_VtSource_0056adc8` | **0** | `Engine/Resources.cs` |
| `0x0056adc8` | `s_Vt_0056adc8` | 256 | `Engine/Resources.cs` |
| `0x0056aec8` | `s_VtSource_0056aec8` | **0** | `Engine/Resources.cs` |
| `0x0056aec8` | `s_Vt_0056aec8` | 256 | `Engine/Resources.cs` |
| `0x0056afc8` | `s_VtSource_0056afc8` | **0** | `Engine/Resources.cs` |
| `0x0056afc8` | `s_Vt_0056afc8` | 256 | `Engine/Resources.cs` |
| `0x0056b0c8` | `s_VtSource_0056b0c8` | **0** | `Engine/Resources.cs` |
| `0x0056b0c8` | `s_Vt_0056b0c8` | 256 | `Engine/Resources.cs` |
| `0x0056b1c8` | `s_VtSource_0056b1c8` | **0** | `Engine/Resources.cs` |
| `0x0056b1c8` | `s_Vt_0056b1c8` | 256 | `Engine/Resources.cs` |
| `0x0056b2c8` | `s_VtSource_0056b2c8` | **0** | `Engine/Resources.cs` |
| `0x0056b2c8` | `s_Vt_0056b2c8` | 520 | `Engine/Resources.cs` |
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
| `0x0056d2c0` | `s_DifficultyBonus_0056d2c0` | 2712 | `Screens/State04/CharacterCreation.cs` |
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
