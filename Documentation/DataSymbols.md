# Recovered data symbols

Values read out of `Legacy/Blaze.exe`'s initialised data, keyed by the **Ghidra virtual address**
`Blaze.c` uses. This is lookup material: an entry stops changing once its bytes are read.
`REVERSE_STATUS.md` keeps the narrative and the per-TODO status and links here.

Binary: `Legacy/Blaze.exe`, 1,589,760 bytes, MD5 `46d90d1b4526f4a9a441f851c77179b3`.
Identity check — image base `0x400000` + entry RVA `0x147D40` = `0x00547D40`, which is exactly
`entry`'s address in `Blaze.c`. Same build, confirmed.

Read with `OpenForesia.Tools at Legacy/Blaze.exe <va> [bytes]`.

**The same tool works on the helper DLLs**, which sit in the repo root — `at winsys3d.dll <va>`,
`at 3dhelp.dll <va>`, and so on, using the very `@ 0xXXXXXXXX` addresses their `.c` files carry.
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

Both are 32 entries of 4 bytes, recovered verbatim from `Legacy/Blaze.exe`. **They are the same
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

Needed to decode these bodies and not written down anywhere until now. `Legacy/Blaze.exe` has five
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

Read verbatim from `Legacy/Blaze.exe` at **file offset `0x15ED08`** (`.data`: VA `0x0054F000` →
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
`Legacy/Blaze.exe`: `DAT_00560728` (file offset `0x15ED28`) and `DAT_00560ff0` (`0x15F5F0`).

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
back from `Legacy/Blaze.exe` at the four file offsets, they are:

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
`OpenForesia.Tools -- at Legacy/Blaze.exe`. None of it is inferred.

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

Read while analysing `FUN_00536ea0`; the function itself is not transcribed yet.

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
