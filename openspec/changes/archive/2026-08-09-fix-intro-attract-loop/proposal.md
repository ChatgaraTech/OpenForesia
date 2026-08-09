## Why

`RunMainLoop()` hangs at startup in an unbreakable `while (true)` (`Blaze.cs:2805`), spinning at
100% CPU with no message pump and no sleep. The application cannot reach engine initialization at
all, so nothing downstream of the intro sequence is observable.

The root cause is a fidelity regression, not a missing subsystem. The original attract loop
(`Blaze.c:74441-74457`) has **four** exit conditions; the reconstruction kept two, and both of the
survivors are dead in practice — `PlayMovie` always returns 0 while `TrySetupMovieStream` is a stub.
The two dropped exits were the return-value checks on `FUN_00542f30`, lost because Ghidra declares
that thunk `void` (`Blaze.c:90063`) even though it tail-calls `FUN_00542c40`, which is
`undefined4 __fastcall`, and the call site reads EAX. This is the already-catalogued
"Ghidra parameter/return recovery is unreliable in this binary" class of defect
(`REVERSE_STATUS.md`, Systemic blocker section) — and following the `void` declaration over the
call site silently removed an execution path, which `Strategy.md` explicitly forbids.

## What Changes

- Give `RewindMovieFallbackFrame` its real `int` return value and restore the two
  `if (… != 0) break;` conditions in `RunMainLoop`'s attract loop, matching `Blaze.c:74442-74451`.
- Reconstruct the prologue of `FUN_00542c40`: build `"<cdRoot>DATA\TITLE640.BMP"`, call
  `LoadBitmapFile`, and **return 1 on load failure**. This is the original binary's own graceful
  degradation path when the CD is absent — restoring it is what actually breaks the loop on
  iteration 1.
- Add the `wsyskern.dll` `LoadBitmapFile` P/Invoke (named export, ordinal 115, `@0x10010500`,
  `wsyskern.c:13885`). No invention required: it is a named export, callable by name exactly like
  the already-imported `LoadBitmapEx` (ordinal 114). With `mode = 0` — the value the call site
  passes — a missing file returns `NULL` silently, with no message box.
- Rename `RewindMovieFallbackFrame` → `ShowTitleScreenAndWait`, keeping the
  `// Ghidra: FUN_00542f30 @ 0x00542f30 -> FUN_00542c40` traceability comment. The current name is
  wrong: `FUN_00542c40` is the arcade-style attract-mode title screen (load `TITLE640.BMP`, blit it,
  wait 20s or until key/mouse/joystick input), not a frame rewind. Per `Strategy.md`'s renaming
  rule, the role is now established well enough to rename.
- Document the same latent defect in `PlayMoviePumpLoop` (`Blaze.cs:2581`): its
  `do { … } while (player.Playing)` is only unreachable because `TrySetupMovieStream` returns
  `false`. `player.Playing` is cleared solely by the unrecoverable stream vtable, so making that
  stub return `true` without a completion signal re-hangs the application in a tight `PeekMessage`
  spin.
- Update `REVERSE_STATUS.md` (per-function status, the movie sub-graph notes) and
  `Documentation/DLL/wsyskern.md` (ordinal 115).

**Explicitly out of scope** — the rest of `FUN_00542c40`: the `StretchDIBits` blit through the
DirectDraw-wrapper vtable and the 20-second `timeGetTime` attract wait. Both are reconstructible in
principle (the inner vtable pointer is null-checked in the original, so the blit degrades safely),
but they are useless until a real main window and `WNDPROC` exist — `REVERSE_STATUS.md` TODO #6 and
TODO #14, which sit behind the systemic `Blaze.exe`-missing blocker. Reconstructing them now would
produce a faithful loop that still never terminates, because no input event can ever arrive without
a window. The bitmap-load failure path is the correct and sufficient exit today.

## Capabilities

### New Capabilities
- `blaze-intro-attract`: the startup intro/attract sequence — movie-player lifetime, the attract
  loop's four exit conditions, the title-screen wait's success and failure paths, and the required
  hand-off to engine initialization. Behavior is derived from `Blaze.c` and the decompiled DLL
  sources, which are the specification of record for this project; the spec records it in
  verifiable form so a future reconstruction session cannot silently drop an execution path again.

### Modified Capabilities

None — `openspec/specs/` is currently empty; this is the first capability.

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — the movie/intro-video subsystem region (~2384-2620) and
  `RunMainLoop` (~2790-2815). No other call sites: `FUN_00542f30`'s only callers are the two inside
  the attract loop.
- New P/Invoke against `wsyskern.dll`, already copied to the build output by
  `OpenForesia.csproj`'s `src\DLL\*.dll` item group — no packaging change.
- `OpenForesia/src/Blaze/REVERSE_STATUS.md`, `Documentation/DLL/wsyskern.md`.
- Reference sources (`Blaze.c`, `wsyskern.c`) are read-only and untouched.
- Unblocks observation of everything `RunMainLoop` does after the intro: engine/device re-init, UI
  font load, viewport setup, `LoadFirstLevelResources`, `RunGameLoop` — which is expected to
  surface the next blocking point (`DecodeResourceChunk`, TODO #19) rather than run clean.
