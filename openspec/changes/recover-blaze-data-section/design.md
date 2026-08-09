## Context

See proposal.md — Why. Three facts shape the approach:

- **`Blaze.c` names data by virtual address.** Symbols read as `DAT_005631f8`, `PTR_FUN_0056c96c`,
  `s_..._0056e6d4`. The address is the join key between the decompilation and the binary, so
  VA→file-offset is the one primitive everything else is built on.
- **`OpenForesia.Tools` is already a PE reader**, and already contains a working
  `RvaToFileOffset(int rva, PEHeaders)` that walks section headers with a fallback for addresses
  inside the PE headers themselves. It was written for the export-table work but is exactly the
  helper this change needs. This is an extension, not a new tool.
- **The binary is verified.** Image base `0x400000` + entry RVA `0x147D40` = `0x00547D40`, matching
  `entry` in `Blaze.c`; MD5 `46d90d1b4526f4a9a441f851c77179b3`. Any future doubt about "is this the
  right build" is settled by re-checking that pair, which is cheap.

## Goals / Non-Goals

**Goals:**

- One repeatable command that turns a Ghidra VA into the bytes at that address.
- Recovered values recorded so they never have to be re-derived, keyed by the same VA `Blaze.c` uses.
- Every affected `TODO(REVERSE)` either resolved with a value, or explicitly reclassified as
  needing a Ghidra re-run rather than data.

**Non-Goals:**

- A general-purpose disassembler or a Ghidra replacement. Reading initialised data is the whole job;
  anything requiring instruction decoding stays out.
- Automating the update of `Blaze.cs`. Recovered constants are applied by hand, in other changes,
  where each one can be reviewed against its call site.
- Parsing `Blaze.c` to discover which symbols to look up. The list of blocked symbols is already
  enumerated in `REVERSE_STATUS.md`; a hand-written list is shorter and more honest than a parser.

## Decisions

### 1. Commit the binary under `Legacy/`, but keep the tool path-agnostic

Storage is settled in proposal.md (committed, `Legacy/Blaze.exe`). The technical consequence is
narrower than it sounds: the tool still takes the binary path as an **argument** and never assumes a
repo-relative location. That keeps it usable against the helper DLLs — which is its existing job —
and against any other build of the executable that turns up later, without a code change.

*Alternative considered*: hardcode `Legacy/Blaze.exe` as a default. Rejected — the same tool already
serves five DLLs at a different path, and a default that silently applies to the wrong file is worse
than an explicit argument.

### 2. Extend `OpenForesia.Tools` rather than write a scratchpad script

Two throwaway scripts already exist for this project (`pe_exports.py`, then `pe_exports.ps1`,
rewritten because no Python was on `PATH` that session). That is the argument: ad-hoc scripts have
already been lost once and rebuilt. `OpenForesia.Tools` is committed, builds with the solution, and
already has the PE plumbing.

*Alternative considered*: a third scratchpad script. Rejected — same cost, same fate.

### 3. Subcommands, with the existing behaviour kept as the default

The tool currently takes exactly one argument (a DLL path) and dumps exports. Adding a verb in front
would break the documented `pe_exports`-style workflow that `Documentation/DLL/*.md` describes.
So: no argument beyond a path keeps today's behaviour; new capabilities are opt-in verbs
(`sections`, `at <va>`). Any lookup accepts a VA and reports the section it resolved through, so a
wrong answer is visible rather than silent.

*Alternative considered*: always require a verb. Rejected — it invalidates written documentation for
no benefit.

### 4. Dump in three shapes, chosen by the caller

The blocked symbols are not one kind of thing: some are C strings (`DAT_005631f8`), some are
function pointers (`PTR_FUN_0056c96c`), some are numeric tables (the MS-ADPCM coefficients,
`DAT_00562e00`). One output format would force eyeballing hex for two of the three. Hex, C string,
and dword are all trivial over the same byte slice.

### 5. Record recovered data in a VA-keyed reference file, separate from `REVERSE_STATUS.md`

`REVERSE_STATUS.md` is already ~500 lines and is a *narrative* of progress. A table of recovered
constants is lookup material with a different lifetime — it stops changing once a symbol is read.
Mixing them makes both worse. `REVERSE_STATUS.md` keeps the per-TODO status and links out.

### 6. Distinguish "recovered" from "interpreted"

A dump gives bytes. Deciding that a 16-byte blob is "seven `short` coefficient pairs" is
interpretation, and interpretation can be wrong. `Documentation/DataSymbols.md` records the raw
bytes alongside any reading of them, so a later disagreement can be settled against the bytes rather
than against a previous guess. This is the same confirmed-vs-hypothesis discipline
`Documentation/DLL/*.md` already uses.

## Risks / Trade-offs

- **Recovered bytes get applied to the wrong symbol.** → Every entry records the VA, the computed
  file offset, and the section it resolved through. Spot-check with a symbol whose value is already
  known independently — `DAT_005631f8` is the natural canary, since exploration already established
  it reads `"@:\FRENCH\"` and the CD's `E:\French\` directory corroborates it.
- **The install's `Blaze.exe` is a different build from the DLLs in the repo.** → Its entry VA
  already matches `Blaze.c`, which is the binding constraint. Worth one explicit note if any
  recovered string contradicts the CD's actual contents.
- **Over-reach: dumping data and "fixing" `Blaze.cs` in the same pass.** → Excluded by scope
  (Non-Goals, and proposal.md). The temptation will be strongest for TODO #15, where the answer is
  one indirection from making the game loop iterate. Resist it there specifically.
- **TODO #15's target may not be in `Blaze.c`.** → Then this change still delivers the address, and
  the follow-up is a scoped Ghidra re-run rather than an open question. Either outcome is progress;
  neither is a failure of this change.
- **`;1` ISO-9660 suffixes** on `BLAZE.ALL;1` / `SYSTEM.INI;1` mean the faithful string and the
  string that opens a file on a modern Windows mount differ. → Out of scope here: record what the
  bytes say, and let `fix-cd-data-paths` decide what to do about it.

## Open Questions

- Whether to also recover the `.rsrc` section (dialog `0x66`, its splash bitmap, and the
  `Setup3DDevice` dialog template). It would confirm TODO #3's string table from a second direction,
  but nothing currently depends on it, and it does not change this design or the task breakdown.
