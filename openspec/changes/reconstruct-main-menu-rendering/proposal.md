## Why

With the display-list arena fixed, the reconstruction renders frames but stops at
`FUN_005443a0`, and behind it `FUN_00545340`. These two are what actually put the main menu
on screen. They are also **not menu-specific**: `FUN_005443a0` has 49 call sites and
`FUN_00545340` has 17, spread across the whole game — they are the shared UI text and window
layer, so reconstructing them unblocks far more than game state 0.

## What Changes

### Stage 1 — the UI text scanner (`FUN_005443a0`)

- Reconstruct `FUN_005443a0` as what it actually is: a **two-pass text engine**, not the
  "menu background renderer" the current stub claims. Pass 1 scans the byte stream and
  collects `$`-escapes; pass 2 emits glyphs.
- Reconstruct both control-code dispatch tables, 32 entries each, read from
  `Legacy/Blaze.exe`: `PTR_LAB_0056e870` (pass 1) and `PTR_FUN_0056e728` (pass 2).
- Implement the handlers the main menu's own label stream reaches — control codes `0x0c`,
  `0x0d` and `0x09` — and make every other entry throw naming its address and code, so an
  unreconstructed handler is loud rather than silent.
- **BREAKING (internal)**: `MenuBackgroundStub` is removed. Its name was wrong and its
  signature dropped `FUN_005443a0`'s second argument (the descriptor).

### Stage 2 — the UI window renderer (`FUN_00545340`)

- Reconstruct `FUN_00545340` (1742 lines) and its dependency graph: 5 new `FUN_`
  (`0051bdb0`, `0051be20`, `00545160`, `00545220`, `00547008`) and roughly 30 new winsys3d
  ordinals, each signature taken from `winsys3d.c` rather than inferred from call sites.

### Also

- Fix the known infidelity in `PollInputAndAudio`'s index, now that `FUN_00545340`
  independently confirms the same expression: the outer index is
  `(_DAT_005de538 & 0xff) * 0x9c`, currently hardcoded to 0.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

None — this reconstructs behaviour that already exists in the binary rather than defining
new observable behaviour, so `.openspec.yaml` sets `skip_specs: true`, consistent with the
other reconstruction changes in this project.

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — the text engine, the two dispatch tables, the window
  renderer and its dependencies.
- `Documentation/DataSymbols.md` — both dispatch tables, recovered verbatim.
- `Documentation/DLL/winsys3d.md` — the ~30 newly used ordinals.
- `OpenForesia/src/Blaze/REVERSE_STATUS.md` — coverage, and the corrected role of
  `FUN_005443a0`.
- No change to `Blaze.c`, `winsys3d.c` or any `.dll`.
