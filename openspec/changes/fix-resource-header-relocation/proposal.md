## Why

`LoadFirstLevelResources` faults against real game data. With the CD mounted it loads
`SYSTEM.INI;1`, enters its directory-entry loop, and dereferences a value read straight out of the
file as if it were a pointer — a null/AV crash inside `LoadResourceEntry`, before
`DecodeResourceChunk` is ever reached (`REVERSE_STATUS.md` TODO #35).

The cause is one pass upstream. `FixupResourceHeader` (`FUN_00532070`) has two jobs: relocate a
fixed run of 60 header dwords, then walk a set of chained lists and relocate those too. Only the
first is reconstructed. The second is a no-op because its schema table,
`s_ResourceFixupSchema_0056aca8`, is an empty array — the bytes lived in `.data` and were
unrecoverable while the binary was missing. So the header's pointer array is relocated but the
records it points at are not, and the first traversal into them fails.

The binary is now in the repo, and the table is recovered.

## What Changes

- Fill `s_ResourceFixupSchema_0056aca8` with the 37 `ushort` entries recovered from
  `Legacy/Binaries/Blaze.exe` at VA `0x0056ACA8`, terminated by the `0x0000` that ends the original's loop.
- Reconstruct `FUN_00532070`'s schema-driven pass faithfully, replacing the documented no-op.
- Correct the recorded reading of the table. `Documentation/Reference/DataSymbols.md` currently offers
  `(record kind, size)` as a hypothesis. Reading `FUN_00532070` disproves it: each entry is
  **`(low byte = index, high byte = stride)`**, and it is not a hypothesis any more — the code
  masks and shifts exactly that way.

### What the table actually says

The original iterates `ushort`s until it hits zero. Per entry it takes `header[4 + (entry & 0xff)]`
— a pointer the fixed pass already relocated — and, if that list is non-empty, walks it in
`(entry >> 8)`-byte steps, relocating each dword until it reads a zero.

The 37 entries carry ascending indices `0x00`..`0x31` with a per-list stride:

| Stride | Indices |
|---|---|
| `0x08` | `00`–`07`, `15`, `1D`, `27` |
| `0x04` | `08`, `09`, `0A`, `0E`, `10`, `11`, `14`, `17`, `18`, `19`, `1B`, `1C`, `1E`, `1F`, `21`, `23`, `25`, `26`, `28`, `2A`, `2B`, `2D`, `2F`, `30`, `31` |
| `0x14` | `0B` |

Every index is below `0x3c`, which is exactly the count the fixed pass relocates — an internal
consistency check the recovered bytes pass.

Note the two passes use **different** relocation formulas, and the difference is not incidental:
the fixed pass adds `header[0]`, the schema pass adds `header[0] + header[1]`. Getting this wrong
would produce pointers that look plausible and are silently off by a constant.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

None. `openspec/specs/` holds only `blaze-intro-attract`, which this does not touch. This change
restores a subsystem to what the original binary does rather than defining new observable
behaviour, so `.openspec.yaml` sets `skip_specs: true`.

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — `s_ResourceFixupSchema_0056aca8` and `FixupResourceHeader`.
- `Documentation/Reference/DataSymbols.md` — replace the `(kind, size)` hypothesis with the confirmed
  `(index, stride)` reading, and record the full table.
- `REVERSE_STATUS.md` — resolve TODO #20, update TODO #35.
- **Expected outcome is a new failure, not a clean run.** Clearing this should let the directory
  walk reach `DecodeResourceChunk` (TODO #19) — the hard blocker that has never once been exercised
  at runtime, because execution has never got this far. Reaching its `NotImplementedException` is
  success here.
- Reaching that point still requires bypassing the attract loop, which throws by design once the CD
  is present (TODO #34). Verification therefore needs a temporary local bypass, exactly as the
  measurement in `fix-cd-data-paths` did — and that bypass must not be committed.
