## 1. Unblock startup

- [x] 1.1 Re-read `FUN_00542800` (`Blaze.c:89660-89690`) and confirm the failure path: the result of
      `FUN_00512930(hwnd)` is stored at `player[0x1c]` and a null makes the function return 0.
- [x] 1.2 Make `OpenMovieStream` report failure, freeing the raw allocation rather than parking a
      half-built object where teardown would later treat it as real.
- [x] 1.3 Confirm `RunMainLoop`'s `if (streamOpened)` then skips both the intro movies and the
      attract loop, and that this is the original's behaviour rather than a bypass.
- [x] 1.4 Keep the diagnostic throw in `ShowTitleScreenAndWait` as a backstop for the case where a
      future change makes the open succeed without supplying window/input handling.

## 2. Wire the tick

- [x] 2.1 Add the `ReInitializeAllTextures` P/Invoke and resolve `Zoom8BitTextures` as an imported
      data symbol via `GetProcAddress`, following the existing `TexFilterPossible`/`MainMem` pattern.
- [x] 2.2 Reconstruct `FUN_00537ca0` as `RunGameStateTick`: prologue, then the switch on the game
      state, with each of the eleven handlers a stub naming its Ghidra function.
- [x] 2.3 Reproduce the `default:` case's deliberate store to address 0 as an exception, and say in
      a comment that it is the original's assert rather than a translation artifact.
- [x] 2.4 Point `s_MainLoopTickCallback` at it and remove `RunGameLoop`'s "no known assignment site"
      throw. Keep the indirection — the original dispatches through a `.data` slot.
- [x] 2.5 Record that all eleven handlers have bodies in `Blaze.c`, so they are uncovered code rather
      than blocked work.

## 3. Verify

- [x] 3.1 Build: 0 errors.
- [x] 3.2 Run with the CD mounted and **no temporary patches**. Confirm startup no longer stops in
      the attract sequence.
- [x] 3.3 Record the new stopping point. Expect `DecodeResourceChunk` (TODO #19) — which sits
      *before* `RunGameLoop`, so the tick is wired but not yet exercised. Do not present it as
      verified at runtime.

## 4. Update the record

- [x] 4.1 Mark TODO #15 resolved and wired in `REVERSE_STATUS.md`, noting it is not yet reachable.
- [x] 4.2 Mark TODO #34 resolved, and record *why* — the guard was correct but placed at the symptom
      rather than at the false success report upstream.
- [x] 4.3 Note that `FUN_0052a440` is not the per-state dispatcher after all; `FUN_00537ca0` is.
- [x] 4.4 Update the current-runtime-behaviour section with the untouched-run output.
