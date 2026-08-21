## Why

`REVERSE_STATUS.md` names one blocker as "the single highest-leverage thing that can change": the
repo has `Blaze.c` but not `Blaze.exe`, and `Blaze.c` is a **code-only** decompilation — Ghidra
emitted function bodies, not `.data`/`.rdata`. Every symbol whose value lives in initialised data is
therefore unrecoverable by re-reading `Blaze.c`, no matter how carefully. Fourteen catalogued
`TODO(REVERSE)` items are blocked on exactly this, including **#15, the game loop's per-frame tick
function pointer**, which is the reconstruction's current runtime stopping point.

The binary is not actually missing. It is on this machine at
`F:\Open Foresia Project\Official game\Blaze.exe` (1,589,760 bytes, 1999-03-03,
MD5 `46d90d1b4526f4a9a441f851c77179b3`), and it is **provably the same build** `Blaze.c` came from:
its PE header reports image base `0x400000` and entry RVA `0x147D40`, i.e. VA `0x00547D40` — exactly
the address of `entry` in `Blaze.c`.

This was demonstrated ad hoc during exploration: reading offsets `0x1617e8`..`0x1617f8` recovered
`"@:\Blaze&Blade"` and `"@:\FRENCH\"`, resolving a data symbol (`DAT_005631f8`) that the status file
had listed as permanently blocked, and explaining a real runtime failure. What is missing is not the
data but a **repeatable way to get at it**.

## What Changes

- Bring `Blaze.exe` into reach of the reconstruction work, and settle how it is stored — see the
  open decision below, which is the main thing this change needs a human answer on.
- Extend `OpenForesia.Tools` from a DLL-export dumper into a small PE inspector that can, for this
  binary: list section headers, translate a **virtual address to a file offset**, and dump the bytes
  at a VA as hex, as a C string, or as a little-endian dword. VA→offset is the operation every
  blocked TODO needs, since `Blaze.c` names its data symbols by VA (`DAT_005631f8`, `PTR_FUN_0056c96c`).
- Resolve the data-blocked TODOs and record the recovered values as **reference documentation**,
  not as code edits: a new `Documentation/Reference/DataSymbols.md` keyed by Ghidra VA, plus the corresponding
  `REVERSE_STATUS.md` updates marking each item resolved and correcting the "Systemic blocker"
  section, which is factually wrong as written.
- Specifically resolve **TODO #15** (`PTR_FUN_0056c96c`): read the function pointer, and check
  whether its target is one of the 1426 `FUN_*` bodies `Blaze.c` already contains. If it is, the
  game loop's tick is recoverable with no Ghidra re-run at all.

**Out of scope, deliberately**: applying any recovered value to `Blaze.cs`. This change produces
knowledge and tooling; the behavioural fixes that consume it belong to their own changes — the CD
path constants go to `fix-cd-data-paths`, and wiring up the tick callback is a separate change once
its target is known. Keeping the split means this change can be reviewed as "is the recovered data
correct?" without also arguing about reconstruction semantics.

## Decision — how `Blaze.exe` is stored

**Settled: commit it, under `Legacy/`.** The project owner's call, on the basis that the 1999 title
is abandonware. This is consistent with how the five helper DLLs (`winsys3d.dll`, `wsyskern.dll`,
`playst.dll`, `3dhelp.dll`, `gldhelp.dll`) are already committed at the repo root, and it makes the
recovery work reproducible for anyone with the repository rather than only on this machine.

Note a wrinkle in the destination: `Legacy` currently exists only as a **virtual solution folder**
in `OpenForesia.sln` (project type GUID `2150E333-8FDC-42A3-9474-1A3956D46DE8`), with `Blaze.c`
nested under `Legacy/Decompiled` as a solution item while physically living at the repo root. So
this change creates a real `Legacy/` directory on disk and registers the binary as a solution item
there. Existing files are not moved — `Blaze.c`'s path is referenced throughout the documentation
and relocating it is out of scope.

> **Note du 2026-08-21** : les deux paragraphes ci-dessus décrivent la disposition d'alors et sont
> conservés tels quels comme trace du raisonnement. Depuis, le dépôt a été réorganisé : `Blaze.c` et
> les cinq `.c` de DLL vivent dans `Legacy/Decompiled/`, les binaires dans `Legacy/Binaries/`, et le
> dossier virtuel du `.sln` coïncide donc enfin avec le disque. La réserve « relocating it is out of
> scope » n'a plus cours ; le déplacement s'est révélé peu coûteux parce que les 371 mentions de
> `Blaze.c` sont toutes en prose, jamais des chemins.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

None. This change adds tooling and reference documentation and does not alter any observable
behaviour of the reconstruction — `Blaze.cs` is not touched. `.openspec.yaml` therefore sets
`skip_specs: true`. The behaviour changes that *use* this data are specced in their own changes.

## Impact

- `OpenForesia.Tools/Program.cs` — grows a small subcommand surface. Its existing export-dump path
  must keep working; it is how the ordinal-only DLL imports were resolved and is still needed.
- New `Documentation/Reference/DataSymbols.md`.
- `OpenForesia/src/Blaze/REVERSE_STATUS.md` — the "Systemic blocker" section is rewritten from
  "blocked, needs the binary" to "resolved, here is where the binary is and what it yielded"; the
  14 affected TODOs are updated individually.
- `.gitignore` — one entry, under option A.
- Unblocks, at minimum: TODOs #2, #3, #4, #10, #11, #12, #15, #17, #20, #21, #22, #24, #27, #30.
- Does **not** unblock #13, #14, #19, #23 — those need a Ghidra re-run (address-taken-only bodies,
  and one function whose own decompilation is incomplete), not data. The status file already
  separates these two categories correctly and that distinction must survive the rewrite.
