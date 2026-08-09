## Why

The reconstruction resolves every CD-relative path one directory too high, so it finds none of the
game's data even with the retail CD mounted. `RunApplication`'s drive scan works — `E:` is detected
as `DRIVE_CDROM` — but it then sets the data root to `"E:\"`, while the real root is `"E:\FRENCH\"`.
Reading `Blaze.exe` at VA `0x005631f8` shows the actual initialised value of `DAT_005631f8`:

```
001617f8: 40 3a 5c 46 52 45 4e 43 48 5c 00    @:\FRENCH\.
```

`@` (0x40, `'A'-1`) is the drive-letter placeholder the scan patches, exactly as it does for the
`"@:\Blaze&Blade"` marker path 16 bytes earlier. The CD corroborates it: the game's data lives in
`E:\French\`, not at the root.

This silently degrades everything downstream. The intro attract loop exits immediately because
`TITLE640.BMP` "does not exist"; `LoadFirstLevelResources` finds no `SYSTEM.INI` and completes
having loaded nothing. Both were previously mis-read as "no CD present" and recorded that way in
`REVERSE_STATUS.md`, which made a path bug look like an environment fact — the most expensive kind of
wrong note to leave behind.

## What Changes

- Correct the CD data root to the recovered `"@:\FRENCH\"`, with the drive letter patched at index 0
  as the original does. **BREAKING for `s_CdDriveRootPath`'s meaning**: it stops being a 4-byte
  `"X:\"` buffer and becomes the full data-root string, so every current consumer must be re-checked.
- Model the two path families that exploration showed are **not** the same:
  - **Data-root-relative**, built by prefixing `DAT_005631f8`: `BLAZE.ALL;1`, `SYSTEM.INI;1`,
    `DATA/Font.bmp`, `DATA/TITLE640.BMP` (via the format string `"%sDATA/TITLE640.BMP"`),
    `TEPS.AVI`, `OPENING.AVI`, `ANI01.AVI`.
  - **Drive-letter-patched absolute paths** that live at the CD *root* and never pass through the
    data root: `"@:\snd\Base.snd"` and `"@:/Music/Mel00_01.WAV"`. The reconstruction currently runs
    the sound path through its CD-path helper, which is wrong in both prefix and shape.
- Correct the options file extension from `".ini"` to `".cfg"` (TODO #11). The real file is
  `BLAZE.cfg`, and its contents match the `[Options]` / `[D3DDriver]` / `[Controls]` sections the
  reconstruction already reads — so `LoadOptionsIni` and `LoadKeyBindings` are correct and have
  simply never found their file.
- Replace the guessed filename literals with the bytes actually in the binary, including their
  original separators (`DATA/Font.bmp` uses a forward slash; `snd` and `Music` are lowercase and
  mixed-case respectively).
- Decide and implement the handling of the **`;1` ISO-9660 version suffixes** — see below.

## The `;1` question

`BLAZE.ALL;1` and `SYSTEM.INI;1` carry ISO-9660 level-1 version suffixes. In 1999, against a raw
ISO-9660 mount, that *was* the file's name. On a modern Windows mount the version is stripped, so
opening `BLAZE.ALL;1` fails while `BLAZE.ALL` succeeds. Faithfulness to the binary and actually
working now point in opposite directions — the first such conflict in this project, so how it is
resolved sets a precedent.

Three options, to be settled during implementation:

| Option | Trade-off |
|---|---|
| Keep `;1`, let it fail | Maximally faithful, and useless — nothing loads. |
| Strip `;1` unconditionally | Works today; silently diverges from the binary. |
| **Try the literal name first, retry without the suffix** (recommended) | Preserves the original as the primary path, degrades explicitly, and the retry is self-documenting at the call site. |

Worth checking first: `Ordinal_331` (winsys3d's `CreateFileA` wrapper) is documented as applying a
`NormalizePath`, which may already handle this. If it does, the whole question is moot and the
literal strings can stand — that check is cheap and belongs before the decision.

## Capabilities

### New Capabilities

- `blaze-cd-assets`: how the reconstruction locates the game's data on the CD — drive detection, the
  data root, the two path families, and the requirement that a missing asset degrade visibly rather
  than silently resolving to a wrong location.

### Modified Capabilities

None yet — but see the sequencing note below, which is a real prerequisite rather than a formality.

## Sequencing note — `blaze-intro-attract`

`openspec/specs/` is still empty: the `blaze-intro-attract` capability exists only inside the
unarchived `fix-intro-attract-loop` change, so there is no main spec for this change to write a
`MODIFIED` delta against.

That capability does need one requirement revisited. Its "title-screen wait fails closed when its
asset is unavailable" requirement stands, but its scenarios were written assuming a missing asset
means *no CD present* — which this change disproves. With the CD mounted and the path corrected, the
attract loop should run rather than exit on iteration 1, and the capability needs a scenario
separating "asset genuinely absent" from "asset present at the correctly resolved path".

Resolution: **archive `fix-intro-attract-loop` first**, then add the `MODIFIED` delta here against
the resulting `openspec/specs/blaze-intro-attract/spec.md`. Task 6.1 carries this.

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — `s_CdDriveRootPath` and its initialisation in
  `RunApplication`; `BuildCdPath`; the filename literals; `ShowTitleScreenAndWait`; the sound and
  music path builders; the options-file extension.
- Depends on `recover-blaze-data-section` for the authoritative byte values, though the two
  highest-impact ones (`@:\FRENCH\`, `.cfg`) are already established and this change can proceed on
  them alone.
- Expected to make the intro attract sequence actually run for the first time, and to push
  `LoadFirstLevelResources` into real data — which will surface `DecodeResourceChunk` (TODO #19), a
  known hard blocker that has never yet been reached at runtime. That is progress, not regression,
  but it means "the game runs further" and "the game crashes in a new place" arrive together.
- `REVERSE_STATUS.md` — correct the "no game CD" framing in the end-to-end run notes, and mark
  TODO #11 resolved.
