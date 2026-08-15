# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this project is

OpenForesia is a **reverse-engineering reconstruction** of *Blaze & Blade* (`Blaze.exe`, a 1998
32-bit native Windows game) into readable C#. There is no product to ship: the deliverable is a
faithful, understandable C# transcription of Ghidra pseudocode, built up function by function
along the call graph.

`Prompt.md` and `Strategy.md` (repo root, both in French) are the authoritative methodology
documents — read them before doing reconstruction work. The summary below is not a substitute.

## Build and run

**The `dotnet` on PATH (`C:\Program Files\dotnet`) is runtime-only — it has no SDK.** The only SDK
on this machine is at `C:\Users\Sagara\.dotnet` (10.0.302). Every `dotnet` invocation must point at
it, or it fails with "No .NET SDKs were found":

```powershell
$env:DOTNET_ROOT = "C:\Users\Sagara\.dotnet"
$env:PATH = "C:\Users\Sagara\.dotnet;$env:PATH"
dotnet build OpenForesia.sln
dotnet run --project OpenForesia          # runs Blaze.entry()
```

Two `NETSDK1189` warnings about `Prefer32Bit` are expected and harmless. There is no test project;
verification is `dotnet build` plus reading the reconstruction against `Blaze.c`.

`dotnet run --project OpenForesia` **fails from the shell** — the app is x86 and the SDK on PATH
only carries the x64 runtime. Launch `OpenForesia/bin/Debug/net9.0/OpenForesia.exe` directly, or use
`playtest.ps1` below, which handles it.

## Running the game — always via `test-protocol.md`

**Before exercising the game in any way — launching it, reaching a screen, reproducing a crash,
capturing a screenshot — read `test-protocol.md` (repo root) and drive the run with
`OpenForesia.Tools/playtest.ps1`.** This is not optional and does not need to be asked for.

```powershell
OpenForesia.Tools\playtest.ps1 -Entry 1 -Seconds 25 -Shot   # character select, with a capture
```

The game's keyboard goes through DirectInput and its menu cannot be driven by timed keystrokes, so
every ad-hoc launcher written from scratch has had to rediscover the same half-dozen traps.
`playtest.ps1` navigates the menu in closed loop, confirms the validation was seen, captures the
window and prints a verdict (crash with its stack, or primitives-per-frame). Read the capture it
writes to `scratchpad/`.

If a run needs something the script does not do, **extend the script and note it in
`test-protocol.md`** rather than working around it. Anything a test session learns about *how to
test* belongs in that file, the same way findings about the binary belong in `Documentation/`.

PE inspector. A bare path keeps the original export-table dump (ordinal → RVA → name); the two
verbs read the binary's data sections:

```powershell
dotnet run --project OpenForesia.Tools -- winsys3d.dll                        # exports
dotnet run --project OpenForesia.Tools -- sections Legacy/Blaze.exe           # VA ranges
dotnet run --project OpenForesia.Tools -- at Legacy/Blaze.exe 0x005631f8 32   # bytes at a Ghidra VA
```

`at` takes a **Ghidra virtual address** (hex, `0x` optional) and prints the resolved file offset and
section alongside hex, C-string and dword renderings. Byte counts are decimal. An address landing in
`.data`'s uninitialised tail is reported as such — that means "zero at load, written only by code",
which is an answer, not an error.

## Layout

| Path | Role |
|---|---|
| `Blaze.c` (98k lines) | Ghidra decompilation of `Blaze.exe`. **Source of truth. Never modify.** |
| `Legacy/Blaze.exe` | The game binary itself (MD5 `46d90d1b4526f4a9a441f851c77179b3`). Same build as `Blaze.c`: image base `0x400000` + entry RVA `0x147D40` = `0x00547D40` = `entry`. |
| `Documentation/DataSymbols.md` | Values recovered from the binary's data sections, keyed by Ghidra VA. |
| `legacy-bug.md` | **Bugs of the 1998 game itself**, observed in play and traced in the decompilation. Check here before "fixing" an oddity: it may be a faithful transcription of an original defect, and the project reproduces those rather than repairing them. |
| `test-protocol.md` | How to exercise the game. Read it before any run — see "Running the game" above. |
| `winsys3d.c`, `wsyskern.c`, `playst.c`, `3dhelp.c`, `gldhelp.c` | Ghidra decompilations of the five helper DLLs. **Secondary references, read-only.** |
| `*.dll` (repo root) | The actual DLL binaries — this is what makes ordinal-only imports resolvable. |
| `OpenForesia/src/Blaze/Blaze.cs` | The entire reconstruction (one file, by design — see "Phase 1"). |
| `OpenForesia/src/Blaze/REVERSE_STATUS.md` | **Read this first.** Call-graph coverage, per-function status, 30 numbered `TODO(REVERSE)` items, current blocking point. |
| `Documentation/DLL/<dll>.md` | Accumulated knowledge per DLL (ordinals, signatures, confirmed vs. hypothesis). |
| `OpenForesia.Tools/` | Standalone PE export-table dumper. |
| `OpenForesia/src/DLL/*.dll` | Copies of the helper DLLs, flattened into the build output for P/Invoke. |

`winsys3d.md` at the repo root is an **older, superseded** version of
`Documentation/DLL/winsys3d.md`; prefer the latter.

`openspec/` contains an initialized-but-empty OpenSpec workspace (no changes or specs yet).

## Working rules (condensed from Strategy.md)

- **Never modify** `Blaze.c` or any decompiled `.c` DLL source. Never treat an earlier C#
  translation as more reliable than `Blaze.c`.
- **Fidelity beats compilability.** Never substitute `0`, `null`, `false`, or an empty body just to
  make the compiler happy; never delete a hard-to-translate branch or an unknown call. When
  something is genuinely unknown, write `// TODO(REVERSE): <precisely what remains to determine>`
  and record it in `REVERSE_STATUS.md`.
- **Keep Ghidra traceability** on every reconstructed function: `// Ghidra: FUN_00513900 @ 0x00513900`.
  Rename to a human name only when the role is actually established; otherwise keep `FUN_…` or
  `Unknown_00513900`. Same for `DAT_…`/`PTR_…` globals.
- **Reconstruct control flow, don't transliterate it.** `LAB_…`/`goto`/`joined_r0…` are artifacts —
  rebuild them as `if`/`switch`/`while`/`for`/`break`/`return`. Do not mechanically turn a label
  into a method; extract a method only for a genuinely independent unit.
- **Respect x86-32 semantics**: pointer arithmetic, signed/unsigned, `byte`/`short`/`ushort`/`int`/`uint`,
  struct layouts, vtables, calling conventions. A cast that feels wrong usually means a wrongly
  reconstructed *type*, not a missing cast — fix the root cause.
- Use P/Invoke for recognized Win32/CRT calls rather than transcribing runtime assembly, keeping a
  comment naming the original. For DLL calls, read the DLL's decompiled body before inferring a
  signature from call sites.
- **Read the DLL's own decompilation before reconstructing a call into it — not only for the
  signature, but for the data conventions it implies.** `winsys3d.c`, `wsyskern.c` etc. are sources
  of truth on the same footing as `Blaze.c`; they are just not reconstructed. A worked example:
  `DrawFlatPrimitive`'s `*prim = *otEntry | 0x2000000` was transcribed faithfully and still produced
  a native crash, because the convention it belongs to — the display list is addressed on 24 bits —
  lives in `ClearOTagR`/`DrawOTag` on the DLL side. Reading those two functions first would have
  caught it immediately; a day of debugging did not.
- Build in coherent groups of functions, rebuilding often. If a change causes error counts to
  explode or demands casts everywhere, revert and re-examine the type decision instead of patching.
- Gaps throw a descriptive `NotImplementedException` rather than silently no-op'ing, so a missing
  subsystem is loud at runtime.

## Phase 1 constraint

All reconstruction stays in the single `Blaze.cs` file, organized by `// ====` banner regions
(interop, CRT, entry/RunApplication, options ini, DirectSound, sound-effect layer, movie player,
resource loader, not-yet-reconstructed). **Do not create `GraphicsService`/`AudioManager`/etc. yet** —
subsystem splitting is a deliberate second phase, once enough behavior is understood. Identified
subsystems get noted in `REVERSE_STATUS.md` instead.

## Resolving an ordinal-only DLL import

`Blaze.c` calls unnamed DLL exports as `Ordinal_NNN(...)`. To find the implementation:

1. Check `Documentation/DLL/<dll>.md` first — it may already be documented.
2. Fastest path for `winsys3d.c`: it embeds the ordinal in a comment inside each function body
   (e.g. `/* 0x2490  129 */`), so grep for the number *with surrounding spaces*.
3. Otherwise dump the export table (`OpenForesia.Tools`), then add the DLL's base address to the
   export RVA to get the `@ 0x…` address used in the decompiled `.c`:
   `winsys3d.dll` → `0x00a00000`, `wsyskern.dll` → `0x10000000`.
4. Write up whatever is learned back into `Documentation/DLL/<dll>.md`, separating confirmed
   behavior from hypotheses.

## The systemic blocker

`Blaze.exe` itself is **not in the repo** — only `Blaze.c`, which is a *code-only* decompilation.
Ghidra emitted function bodies but not the `.data`/`.rdata` sections, so every symbol whose value
lives in initialized data (string literals, lookup tables, statically initialized function pointers
such as the game loop's per-frame tick `PTR_FUN_0056c96c`) is **unrecoverable no matter how
carefully `Blaze.c` is re-read**. `REVERSE_STATUS.md` lists exactly which TODOs are blocked this
way. Do not "solve" one of them by inventing plausible content; adding the binary (or a Ghidra
data-section export) unblocks them all at once.

A related class of gaps needs a **Ghidra re-run**, not more reading: address-taken-only bodies
(window procedures, thread procedures, C++ vtable slots that appear only as `LAB_…`) and functions
whose parameters Ghidra failed to recover (`unaff_ESI`, `extraout_var`, fewer parameters than call
sites pass). Flag these inline rather than guessing a signature.
