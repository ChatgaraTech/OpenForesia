## Why

The reconstruction reaches the main menu's first frame and then dies with a native
`0xC0000005` inside `Ordinal_701` (`DrawOTag`). The cause is not in `Blaze.cs`: the
PlayStation display list that `Blaze.exe` and `winsys3d.dll` share stores its links on
**24 bits**, so every ordering-table entry and every primitive must live below
`0x01000000`. On modern Windows `winsys3d.dll` is relocated out of the low address space,
its `MainMem` arena lands at `0x09…`, and the truncated links point at unmapped memory.

Nothing further can be reconstructed until this is fixed — every game state draws through
the same path, so this blocks the menu engine (`FUN_00545340`) and all eleven other state
handlers behind it.

## What Changes

- **Retarget the `MainMem` arena into low memory at startup.** Allocate `0x280000` bytes
  with an explicit base address and write that pointer into the `MainMem` data symbol
  `winsys3d.dll` exports, immediately after `LoadLibrary` and before any winsys3d
  initialisation call. The DLL keeps using its own pointer; only the address it holds
  changes.
- **Prefer the original address.** Try `0x00B60000` first so `MainMem` resolves to
  `0x00B6DBD8` — its 1998 value — and every address in `winsys3d.c` and every
  `MainMem + offset` in `Blaze.c` matches the runtime one for one.
- **Fall back by scanning, and record the delta.** When the preferred base is taken, scan
  for any free `0x280000` block below `0x00D80000` and log the constant offset from the
  original layout.
- **Fail loudly when no low block exists.** Throw a descriptive exception naming the
  24-bit constraint rather than letting `DrawOTag` fault natively.
- **Document the deviation** in `REVERSE_STATUS.md` alongside the two existing
  environmental deviations, and record the 24-bit display-list constraint in
  `Documentation/DLL/winsys3d.md` as confirmed behaviour.
- **Revert the two diagnostic patches** currently in the working tree (the neutralised
  `PollDebugHotkeys` call plus the `FrameEnd` trace in `Blaze.cs`, and the address-space
  probe in `Program.cs`).

No binary is modified: `winsys3d.dll` on disk is untouched, and no Windows security
setting is changed.

### Non-goals

- Reconstructing `winsys3d.dll` in C#. Its lower layer is a 1998 DirectDraw rasteriser;
  it stays a black box. Reading it as a source of truth continues.
- Forcing `winsys3d.dll` to load at its preferred base (PE surgery, a native launcher, or
  Windows Exploit Protection settings). All three were investigated and are unnecessary
  once the arena is retargeted.
- Changing `OpenForesia.exe`'s image base or its `DYNAMICBASE` flag.

## Capabilities

### New Capabilities

- `blaze-display-list-memory`: where the shared PlayStation display-list arena lives and
  what the reconstruction guarantees about it — the 24-bit addressing constraint the
  original imposes, the requirement that the arena be secured before winsys3d derives any
  pointer from it, and the requirement that an unsatisfiable layout fail visibly rather
  than as a native access violation.

### Modified Capabilities

None. `openspec/specs/` holds only `blaze-intro-attract`, which this change does not
touch — the crash happens well after the attract sequence has handed off.

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — `GetMainMemPtr()` and the winsys3d module load gain
  the arena allocation and retargeting; the two diagnostic patches come out.
- `OpenForesia/Program.cs` — the address-space probe comes out.
- `OpenForesia/src/Blaze/REVERSE_STATUS.md` — a third environmental deviation, and the
  correction of the current blocking-point section (the crash is the first frame, not the
  second, and the input-polling functions are not implicated).
- `Documentation/DLL/winsys3d.md` — the 24-bit display-list contract, confirmed from
  `winsys3d.c` rather than hypothesised.
- `TODO.md` — its regression section is superseded.
- No change to `Blaze.c`, `winsys3d.c`, any `.dll`, the build configuration, or the
  machine's Windows settings.
