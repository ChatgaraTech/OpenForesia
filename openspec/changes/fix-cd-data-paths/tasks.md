## 1. Confirm the literals

- [x] 1.1 Dump `DAT_005631f8` and confirm it reads `@:\FRENCH\` (established during exploration at
      file offset `0x1617f8`; re-confirm with the tooling from `recover-blaze-data-section` once it
      exists, or with `xxd` directly if this change runs first).
- [x] 1.2 Dump the asset filename literals and record them exactly, separators and case included:
      `BLAZE.ALL;1`, `SYSTEM.INI;1`, `DATA/Font.bmp`, `%sDATA/TITLE640.BMP`, `ANI01.AVI`,
      `OPENING.AVI`, `TEPS.AVI`, `@:\snd\Base.snd`, `@:/Music/Mel00_01.WAV`.
- [x] 1.3 Confirm the options-file extension is `.cfg`. Corroborated already by the real
      `BLAZE.cfg` next to the installed `Blaze.exe`, whose sections match what `LoadOptionsIni`
      reads.
- [x] 1.4 Read `Ordinal_331`'s body in `winsys3d.c` and determine whether its documented
      `NormalizePath` step already strips ISO-9660 `;1` suffixes. **Do this before task 4** — a
      positive answer removes that decision entirely.

## 2. Data root

- [x] 2.1 Widen `s_CdDriveRootPath` from the 4-byte `"X:\"` buffer to hold the full data-root
      template, and initialise it from the recovered `@:\FRENCH\` literal.
- [x] 2.2 In `RunApplication`, patch only index 0 with the detected drive letter, mirroring how the
      original patches `s____Blaze_Blade_005631e8[0]`. Do not rebuild the string from parts.
- [x] 2.3 Audit every existing reader of `s_CdDriveRootPath` — this is a breaking change to its
      meaning. `RunMainLoop`, `PlayMovie`, `BuildCdPath`, `ShowTitleScreenAndWait` and the resource
      loader all consume it; confirm each still composes a correct path.
- [x] 2.4 Keep the `"X:\"`-shaped marker path used by the CD-presence scan separate from the data
      root. They are two distinct globals in the original (`s____Blaze_Blade_005631e8` vs
      `DAT_005631f8`, 16 bytes apart) and conflating them is what caused this bug.

## 3. The two path families

- [x] 3.1 Keep `BuildCdPath` for data-root-relative assets and update its callers to the exact
      recovered filenames, including the forward slash in `DATA/Font.bmp`.
- [x] 3.2 Add a distinct builder for drive-letter-patched absolute paths, and route the sound bank
      (`@:\snd\Base.snd`) through it. It currently goes through `BuildCdPath`, which is wrong in
      both prefix and shape.
- [x] 3.3 Note at the music-track site that its table (TODO #30) uses the same absolute form
      (`@:/Music/Mel00_01.WAV`). Do not reconstruct the table here — that is
      `recover-blaze-data-section`'s job, and `PlayMusicTrack` stays untranslated until it lands.
- [x] 3.4 Update `ShowTitleScreenAndWait` to use the recovered `%sDATA/TITLE640.BMP` form rather
      than the guessed `DATA\TITLE640.BMP`.

## 4. ISO-9660 suffix handling

- [x] 4.1 If task 1.4 showed `NormalizePath` already handles `;1`, use the literals verbatim and
      record that finding. Otherwise implement the recommended behaviour: attempt the literal name
      first, then retry without the suffix.
- [x] 4.2 Whichever path is taken, document the deviation at the call site — the spec requires the
      original be attempted first and any fallback be explicit.

## 5. Options file

- [x] 5.1 Change the options-file extension to `.cfg` and confirm `LoadOptionsIni` reads the real
      `BLAZE.cfg`'s `[Options]` values (`Resolution=4`, `Size=23`, `SoundVol=64`, `CdVol=64`,
      `Gamma=64`, `Filter=1`, `Timing=1`, `Joy0..3`) instead of defaulting.
- [x] 5.2 Verify `LoadKeyBindings` reads the `[Controls]` `K….` entries from the same file.
- [x] 5.3 Do not overwrite the user's real `BLAZE.cfg` while testing — work against a copy, since
      `SaveOptionsIni` writes back.

## 6. Specs and status

- [x] 6.1 Archive `fix-intro-attract-loop` so `openspec/specs/blaze-intro-attract/spec.md` exists,
      then add the `MODIFIED` delta described in proposal.md — a scenario separating "asset
      genuinely absent" from "asset present at the correctly resolved path". Copy the full existing
      requirement block into the delta; a partial `MODIFIED` loses detail at archive time.
- [x] 6.2 Correct `REVERSE_STATUS.md`'s end-to-end run notes. They currently attribute the observed
      behaviour to "a machine with no game CD"; the CD was mounted and detected, and the paths were
      wrong. Leaving that framing tells a future session an environment fact where there was a bug.
- [x] 6.3 Mark TODO #11 resolved, and correct TODO #17 / #22's guessed filenames to the recovered
      literals.
- [x] 6.4 Note explicitly that `LoadFirstLevelResources` previously completed *because* it found
      nothing, so TODO #19 (`DecodeResourceChunk`) was never actually exercised — it is untested,
      not passed.

## 7. Verify

- [x] 7.1 Build: `$env:DOTNET_ROOT="C:\Users\Sagara\.dotnet"; dotnet build OpenForesia.sln` — 0 errors.
- [x] 7.2 Run with the CD mounted (`DOTNET_ROOT` unset, launch the x86 apphost directly — see
      `REVERSE_STATUS.md`). Confirm `TITLE640.BMP` now loads: the attract loop should no longer exit
      on iteration 1.
- [x] 7.3 Confirm the attract loop's new behaviour is understood before calling it correct. With the
      bitmap loading, `ShowTitleScreenAndWait` returns 0 and `PlayMovie` returns 0, so the loop will
      spin — the 20-second timeout and input handling that would break it are still unreconstructed
      (out of scope, gated on TODO #6/#14). Decide deliberately whether that is acceptable for this
      change or whether it needs a guard, and record the decision.
- [x] 7.4 Confirm `LoadFirstLevelResources` now finds `SYSTEM.INI` and reaches real directory
      entries. Hitting `DecodeResourceChunk`'s `NotImplementedException` is the **expected** outcome
      and counts as success for this change.
- [x] 7.5 Record the new runtime stopping point in `REVERSE_STATUS.md`.
