## 1. Native import

- [x] 1.1 Re-read `wsyskern.c:13885-13930` (`LoadBitmapFile @ 0x10010500`) and confirm the exact
      signature and the `mode` semantics: `mode == 1` calls `filenotfound`/`fileerror` (message box),
      `mode == 2` calls the `warn*` variants and returns 0, `mode == 0` fails silently. The call site
      at `Blaze.c:89909` passes 0.
- [x] 1.2 Add the `LoadBitmapFile` P/Invoke to the interop region of `Blaze.cs`, next to the existing
      `LoadBitmapEx` (ordinal 114) declaration. It is a named export, so declare it by name — no
      ordinal-resolution work needed. Returns a pointer to the raw `BITMAPFILEHEADER`-prefixed file
      image allocated by the DLL's own `Malloc`, or null on failure.
- [x] 1.3 Add the ordinal-115 entry to `Documentation/DLL/wsyskern.md`, separating confirmed
      behavior (the `0x4d42` "BM" signature check, the `Malloc` + full-file `ReadFile` path) from the
      unread `FUN_10003260` fallback branch for non-BMP-signature files.

## 2. Restore the dropped control-flow edges

- [x] 2.1 Change `RewindMovieFallbackFrame` to return `int` and rename it to
      `ShowTitleScreenAndWait`, keeping `// Ghidra: FUN_00542f30 @ 0x00542f30 -> FUN_00542c40`.
      Add a `TODO(REVERSE)`-style note recording that Ghidra declares `FUN_00542f30` as `void`
      (`Blaze.c:90063`) while its call site reads the result — the call site is authoritative.
- [x] 2.2 In `RunMainLoop`'s attract loop (`Blaze.cs:2805`), restore the two dropped exits so all
      four of `Blaze.c:74442-74451`'s conditions are present:
      `if (ShowTitleScreenAndWait() != 0) break;` before each `PlayMovie` call.
- [x] 2.3 Verify no other call site of `RewindMovieFallbackFrame` exists (`grep` confirms only the
      two inside the attract loop) and that the rename leaves the file compiling.

## 3. Reconstruct the title-screen wait's load prologue

- [x] 3.1 Reconstruct the first block of `FUN_00542c40` (`Blaze.c:89895-89915`) inside
      `ShowTitleScreenAndWait`: reset the result field, then — only when the bitmap is not already
      cached — build `"<s_CdDriveRootPath>DATA\TITLE640.BMP"` and call `LoadBitmapFile(path, 0)`.
      Reuse the existing CD-path building pattern (`BuildCdPath` / the `PlayMovie` path builder)
      rather than adding a third variant.
- [x] 3.2 Return 1 when the load fails, matching the original's early `return 1`. Add the caching
      fields (loaded flag at `+0xc`, bitmap pointer at `+8`) to `MoviePlayerState` with their Ghidra
      offsets in comments.
- [x] 3.3 Record the filename as a best guess: `Blaze.c` preserves only Ghidra's derived symbol name
      `s__sDATA_TITLE640_BMP_0056e6d4`, not the literal bytes — same category as
      `REVERSE_STATUS.md` TODOs #17/#22. Note the `%s` format: the path is built by `wsprintfA` from
      the CD root at `DAT_005631f8`.
- [x] 3.4 Stop there. Add a banner comment stating explicitly what of `FUN_00542c40` is deliberately
      not reconstructed — the `StretchDIBits` blit through the DirectDraw-wrapper vtable
      (offsets 0x44 GetDC / 0x68 ReleaseDC on the inner object, both null-checked in the original)
      and the 20-second `timeGetTime` wait with its key/mouse/joystick exit messages
      (0x100, 0x201, 0x204, 0x207, 0x3b5, 0x3b6) — and why: they cannot receive input without a real
      main window and `WNDPROC` (TODO #6 / #14).

## 4. Record the latent hazard

- [x] 4.1 Add a warning comment at `PlayMoviePumpLoop`'s `do { … } while (player.Playing)`
      (`Blaze.cs:2581`) naming `TrySetupMovieStream` as the only reason it is currently unreachable,
      `player.Playing` as the flag only the unrecoverable stream vtable clears, and a tight
      `PeekMessage` spin as the consequence of enabling the stub without a completion signal.
- [x] 4.2 Add the same hazard to `REVERSE_STATUS.md` as a numbered `TODO(REVERSE)` item so it is
      tracked alongside the existing 30.

## 5. Verify

- [x] 5.1 Build: `$env:DOTNET_ROOT="C:\Users\Sagara\.dotnet"; $env:PATH="C:\Users\Sagara\.dotnet;$env:PATH"; dotnet build OpenForesia.sln`
      — expect 0 errors and only the two known `NETSDK1189` warnings.
- [x] 5.2 Run `dotnet run --project OpenForesia` and confirm the attract loop exits on the first
      iteration instead of spinning, and that execution proceeds past `CloseMovieStreamIfOpen()` into
      the `Ordinal_129`/`Ordinal_41` engine re-init sequence.
- [x] 5.3 Confirm the run then stops at the *expected* next blocker rather than silently misbehaving:
      `DecodeResourceChunk` (TODO #19) via `LoadFirstLevelResources`, or an earlier throw from the
      not-yet-reconstructed device path. Record which one is actually hit — it is the new
      "next area to explore".
- [x] 5.4 Re-read the modified region against `Blaze.c:74441-74457` and `Blaze.c:89895-89915` side by
      side to confirm no other branch was dropped or added.

## 6. Update the reverse-engineering record

- [x] 6.1 Update `REVERSE_STATUS.md`: move `RewindMovieFallbackFrame` to its new name in the
      reconstructed-functions table, change its status from PARTIAL to reflect the reconstructed
      load prologue, and correct the movie sub-graph notes that describe it as a frame rewind.
- [x] 6.2 Update the "Next area to explore — current blocking point" section with the result of 5.3.
- [x] 6.3 Add a short note to the Systemic blocker section: this defect is a concrete instance of the
      already-documented unreliable-signature-recovery problem, and the mitigation is to trust call
      sites over Ghidra's declarations.
