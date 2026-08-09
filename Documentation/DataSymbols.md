# Recovered data symbols

Values read out of `Legacy/Blaze.exe`'s initialised data, keyed by the **Ghidra virtual address**
`Blaze.c` uses. This is lookup material: an entry stops changing once its bytes are read.
`REVERSE_STATUS.md` keeps the narrative and the per-TODO status and links here.

Binary: `Legacy/Blaze.exe`, 1,589,760 bytes, MD5 `46d90d1b4526f4a9a441f851c77179b3`.
Identity check — image base `0x400000` + entry RVA `0x147D40` = `0x00547D40`, which is exactly
`entry`'s address in `Blaze.c`. Same build, confirmed.

Read with `OpenForesia.Tools at Legacy/Blaze.exe <va> [bytes]`.

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
