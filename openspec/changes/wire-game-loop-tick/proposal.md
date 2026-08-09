## Why

Two things blocked the reconstruction from running, and they turned out to be related only by
timing.

`RunGameLoop` threw on its indirect per-frame call because `PTR_FUN_0056c96c` had no known target
(TODO #15). That is now answered: the pointer holds `0x00537CA0`, and `FUN_00537ca0` already has a
decompiled body in `Blaze.c`.

Separately, startup dead-ended at a diagnostic throw in `ShowTitleScreenAndWait` (TODO #34) as soon
as the CD made the title bitmap loadable. That throw was a correct guard in the wrong place. The
actual defect is in `OpenMovieStream` (`FUN_00542800`): the original stores
`FUN_00512930(hwnd)` — the constructed DirectDraw wrapper — and **returns 0 when it is null**, at
which point `RunMainLoop` skips the intro movies and the attract loop outright. The reconstruction
never performs that construction (no window, TODO #14; unrecoverable wrapper vtable, TODO #13) and
returned `true` anyway, reporting success for work it had not done.

## What Changes

- `OpenMovieStream` reports **failure**, matching the original's own no-DirectDraw path, so the
  intro/attract block is skipped and startup proceeds. This is more faithful than the previous
  `return true`, not a workaround: it stops claiming an unperformed step succeeded.
- `s_MainLoopTickCallback` is initialised to `&RunGameStateTick`, keeping the original's indirection
  through a statically initialised `.data` slot rather than collapsing it into a direct call.
- `RunGameStateTick` reconstructs `FUN_00537ca0`: the real prologue (`Zoom8BitTextures = 0` through
  the winsys3d data export, then `ReInitializeAllTextures()`), then the switch on the game state.
- The eleven state handlers are named stubs. **All eleven have decompiled bodies in `Blaze.c`** —
  they are ordinary uncovered code, not blocked work — so each stub names its Ghidra function and
  the next state to reconstruct is readable straight off the exception.
- The `default:` case reproduces the original's `uRam00000000 = 1` (a deliberate store to address 0
  on an unknown state) as an exception rather than by actually faulting.

Worth recording: this displaces a long-standing assumption. `REVERSE_STATUS.md` had `FUN_0052a440`
down as the per-game-state dispatcher; the tick pointer reaches `FUN_00537ca0` instead.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

None. The `blaze-intro-attract` spec requires that startup always reach engine initialization and
that no loop iterate without a reachable exit — which the previous throw was *violating*. This
restores compliance rather than changing the contract, so `.openspec.yaml` sets `skip_specs: true`.

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — `OpenMovieStream`, `RunGameLoop`, the new `RunGameStateTick`,
  and two winsys3d imports (`ReInitializeAllTextures` as a function, `Zoom8BitTextures` as a data
  symbol resolved via `GetProcAddress`, following the existing `TexFilterPossible`/`MainMem` pattern).
- `REVERSE_STATUS.md` — resolve TODOs #15 and #34.
- The diagnostic guard in `ShowTitleScreenAndWait` is **kept** as a backstop: if a later change makes
  `OpenMovieStream` succeed without also supplying the window and input handling, the attract loop
  would again have no exit and should say so rather than spin.
- **The tick is wired but not yet reachable.** `LoadFirstLevelResources` throws at
  `DecodeResourceChunk` (TODO #19) before `RunGameLoop` is entered, so nothing exercises
  `RunGameStateTick` at runtime yet. That is the honest state, and #19 needs a Ghidra re-run rather
  than more reading.
