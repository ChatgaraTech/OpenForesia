## Why

Startup now runs end to end and **draws its first frame** — the CD's `TITLE640.BMP` appears during
the attract sequence. Execution then enters the game loop, the tick dispatches on the game state,
and stops at the first one: `FUN_00542130 (game state 0) not yet reconstructed`.

Game state 0 is the **main menu**. That is not an inference from its position; the switch at the end
of its frame loop says so directly:

```c
switch (DAT_005e909f) {              // selected menu entry
  case 0: DAT_005de510 = 1; break;   // -> game state 1
  case 1: DAT_005de510 = 4; break;   // -> game state 4
  case 2: DAT_005de510 = 6; break;   // -> game state 6
  case 3: ... FUN_00512320(); SaveOptionsIni(); CreateTranslationTables(gamma); break;
  case 4: Exit(0);                   // quit
}
```

Five entries: three that hand off to other game states, an options page that rewrites `BLAZE.cfg`
and rebuilds the gamma tables, and a quit that calls the CRT `exit` already reconstructed. The
`(DAT_005e9960 & 0xf) * 0x10` counter with its decrement and `0x80` floor is a **fade**, and
`uVar3 * 0x10101` is its grey level.

Reconstructing it is the natural next step, and it also delivers something with leverage beyond the
menu — see below.

## The shape of the work

```
FUN_00542130  (game state 0)                            8 lines
├─ FUN_00542160     resource init                      45 lines
│    └─ its loop is LoadFirstLevelResources' loop, using LoadResourceEntry,
│       FixupResourceHeader and DecodeResourceChunk — all already reconstructed
├─ FUN_00529a80     screen setup                       23 lines
└─ FUN_00542250     the state's frame loop            122 lines
     ├─ FUN_0052a1e0     33 l.  ─┐ shared frame infrastructure,
     ├─ FUN_0052a280     71 l.  ─┘ not menu-specific
     ├─ FUN_00544350     19 l.
     ├─ FUN_0051f9c0     26 l.
     ├─ FUN_005048f0     76 l.
     ├─ FUN_00521f20     78 l.
     ├─ FUN_00512320     80 l.   the options page
     ├─ FUN_005443a0    233 l.
     ├─ PlayMusicTrack(6)
     └─ FUN_00545340   1742 l.   the menu/UI engine — a project of its own
```

**`FUN_0052a1e0` and `FUN_0052a280` are the highest-value part.** They bracket every frame: one
prepares the primitive buffer and ordering table, the other flushes it and swaps buffers. The
rendering functions thread the OT pointer through their return values, PlayStation-style. Every
other game state will use the same pair, so reconstructing them here pays forward.

## What Changes

- Reconstruct `FUN_00542130`, `FUN_00542160` and `FUN_00529a80`.
- Reconstruct `FUN_00542250`'s full control flow: the entry setup, the entity-slot table loop, the
  fade counter, the menu switch with all five cases, and the per-frame render/flush sequence.
- Reconstruct the shared frame pair `FUN_0052a1e0` / `FUN_0052a280` and the small renderers
  `FUN_00521f20`, `FUN_0051f9c0`, `FUN_00544350`.
- Declare the eight winsys3d ordinals these need: 97, 762, 765, 766, 767, 768, 769, 770.
- Reconstruct `PlayMusicTrack` (`FUN_00529ad0`), now that its filename table has been recovered from
  the binary (TODO #30).
- Leave `FUN_00545340` (1742 lines, the menu/UI engine), `FUN_005443a0` (233 lines) and
  `FUN_00512320` (the options page) as **named stubs**.

### Why stubs rather than going deeper

The same approach that resolved the tick callback: convert one unknown into a list of knowns, and
let the runtime name the next target. With the structure in place the application enters state 0's
frame loop and throws on a specific function instead of on "game state 0". Attempting
`FUN_00545340` in the same pass would mean writing 1742 lines with no way to observe whether the
surrounding order is right.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

None — this reconstructs behaviour that already exists in the binary rather than defining new
observable behaviour, so `.openspec.yaml` sets `skip_specs: true`, consistent with the other
reconstruction changes in this project.

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — a new game-state-0 region, the shared frame pair, and eight
  ordinal declarations.
- `REVERSE_STATUS.md` — record what game state 0 is, and resolve TODO #30.
- **Expected outcome is a new, more specific failure.** Reaching a `FUN_00545340` stub is success.
- `PlayMusicTrack(6)` runs on entry, but **nothing will be audible**: no `DirectSoundCreate` call
  site has ever been located (TODO #26), so `StartMusicStream` takes its no-device early-out. Worth
  stating so silence is not mistaken for a bug in this change.

## Debt this change should also clear

Two things from the display work are **not yet recorded anywhere**, and they are exactly the kind of
context that evaporates between sessions:

- The display path works: `SetupDirectDrawSurfaces` fills `wrapper[1]`, and `BlitTitleScreen` draws
  the title bitmap (measured at 480 scanlines copied).
- Two **environmental deviations**, both measured rather than assumed: modern Windows returns
  `E_NOTIMPL` for the original's `SetDisplayMode(640, 480, 16)`, so a 32bpp fallback is applied only
  on that exact error code; and DirectDraw surfaces are lost across repaints, so `GetDC` failing
  with `DDERR_SURFACELOST` triggers `Restore` and one retry.

These should be written into `REVERSE_STATUS.md` as part of this change if they have not been by the
time it starts.
