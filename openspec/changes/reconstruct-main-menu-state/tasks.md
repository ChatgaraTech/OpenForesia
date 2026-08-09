## 0. Clear the undocumented debt first

- [x] 0.1 Record in `REVERSE_STATUS.md` that the display path works end to end: `FUN_00512a10` fills
      `wrapper[1]`, and the attract screen's blit draws `TITLE640.BMP` (measured: `StretchDIBits`
      returned 480 scanlines, and the image was confirmed on screen).
- [x] 0.2 Record both environmental deviations, with the measurements behind them: the
      `SetDisplayMode(640,480,16)` → `E_NOTIMPL` → 32bpp fallback, gated on that exact HRESULT so it
      never fires on period hardware; and the `DDERR_SURFACELOST` → `Restore` → retry in the blit.
- [x] 0.3 Correct the `Ordinal_789` entry: the status file states its device-init branch is never
      reached because both call sites pass 0. The opposite is true — passing 0 is what *triggers*
      full initialisation, which is why it aborted with "3D-Graphics initialisation error" when
      DirectDraw was still held exclusively.
- [x] 0.4 Mark TODOs #13, #14 and #23 resolved with what the forced disassembly produced, and record
      that the movie subsystem is DirectShow (`CLSID_FilterGraph` / `IID_IGraphBuilder`).

## 1. Shared frame infrastructure

Do this before the menu itself — every game state uses it.

- [x] 1.1 Reconstruct `FUN_0052a1e0` (33 lines). It takes three out-parameters and prepares the
      per-frame primitive buffer and ordering table. Name them for what they hold, not `local_20`.
- [x] 1.2 Reconstruct `FUN_0052a280` (71 lines) — the flush and buffer swap. Note it is one of the
      functions `REVERSE_STATUS.md` flags for unreliable Ghidra parameter recovery: its call sites
      pass 5 arguments. Check the call sites against the declaration before trusting either.
- [x] 1.3 Model the ordering-table pointer the way the original does: threaded through each
      renderer's return value and reassigned. Do not "simplify" it into a field — the threading is
      how the display list is built.

## 2. State entry

- [x] 2.1 Declare the eight winsys3d ordinals: 97, 762, 765, 766, 767, 768, 769, 770. Read each
      body in `winsys3d.c` before writing its signature, rather than inferring from the call site.
- [x] 2.2 Reconstruct `FUN_00529a80` (23 lines) — the global writes plus `Ordinal_762`/`Ordinal_97`.
- [x] 2.3 Reconstruct `FUN_00542160` (45 lines). Its directory loop is the one already written in
      `LoadFirstLevelResources`; reuse `LoadResourceEntry`, `FixupResourceHeader` and
      `DecodeResourceChunk` rather than writing a second copy, and keep the 4-byte slot stride.
- [x] 2.4 Reconstruct `FUN_00542130` (8 lines) and point game state 0 at it in `RunGameStateTick`,
      replacing the stub.

## 3. The frame loop

- [x] 3.1 Reconstruct `FUN_00542250`'s entry sequence: `FUN_0052a1e0`, `PlayMusicTrack(6)`, the two
      draw-env `isbg` writes, `JoystickResetState`, and the entity-slot table initialisation loop.
      That loop indexes a table selected by two joystick-state comparisons against `-0x8000`;
      transcribe the selection faithfully rather than assuming a default.
- [x] 3.2 Reconstruct the fade: `(DAT_005e9960 & 0xf) * 0x10`, the decrement, the `0x80` floor and
      the `!= 8` guard. `uVar3 * 0x10101` is the grey level it feeds to the renderers.
- [x] 3.3 Reconstruct the menu switch with **all five cases**, including case 3's options sequence
      (`FUN_00512320`, `SaveOptionsIni`, and the conditional `CreateTranslationTables` rebuild when
      the gamma value changed) and case 4's `Exit(0)`.
- [x] 3.4 Reconstruct the per-frame render tail: the `local_14` fetch from the resource header at
      `+0x94`, then `FUN_00521f20`, `FUN_0051f9c0` and `FUN_0052a280`, preserving the OT threading.
- [x] 3.5 Reconstruct the small renderers `FUN_00521f20` (78), `FUN_0051f9c0` (26) and
      `FUN_00544350` (19).
- [ ] 3.6 Reconstruct `FUN_005048f0` (76 lines) — the loop in case 3 drains it until it returns 0,
      so its return value carries meaning; do not reduce it to a void call.

## 4. Music

- [x] 4.1 Reconstruct `PlayMusicTrack` (`FUN_00529ad0`, 14 lines) using the filename table recovered
      at `PTR_s____Music_Mel00_01_WAV_00566cc8`. Remember the paths are the CD-root absolute form
      (`@:/Music/...`), patched at index 0 — not data-root relative.
- [ ] 4.2 Dereference the whole table, not just the entries already sampled: call sites index it up
      to at least `0x26`.
- [x] 4.3 Resolve TODO #30 in `REVERSE_STATUS.md`, and note explicitly that nothing is audible until
      the `IDirectSound` device exists (#26), so silence here is expected rather than a defect.

## 5. Stubs

- [x] 5.1 Leave `FUN_00545340` (1742 lines), `FUN_005443a0` (233 lines) and `FUN_00512320` (80
      lines) as stubs that throw, each naming its Ghidra function and its role.
- [x] 5.2 Record in `REVERSE_STATUS.md` what `FUN_00545340` appears to be — the menu/UI engine, 3
      parameters, 25 calls — and that it is deliberately deferred as its own piece of work.

## 6. Verify

- [x] 6.1 Build: 0 errors.
- [x] 6.2 Run with the CD mounted. Confirm the attract screen still displays the title bitmap —
      this change touches the shared frame path, so it could regress what was just proven to work.
- [x] 6.3 Confirm execution now enters state 0's frame loop and throws on a **named** function
      rather than on "game state 0". Record which one.
- [x] 6.4 If it faults natively instead of throwing, treat that as the finding: the OT threading or
      the ordinal signatures are the first suspects, and the pointer arithmetic in the entity-slot
      loop the second.
- [x] 6.5 Update the current-runtime-behaviour section of `REVERSE_STATUS.md` with the new stopping
      point.
