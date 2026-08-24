# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this project is

OpenForesia is a **reverse-engineering reconstruction** of *Blaze & Blade* (`Blaze.exe`, a 1998
32-bit native Windows game) into readable C#. There is no product to ship: the deliverable is a
faithful, understandable C# transcription of Ghidra pseudocode, built up function by function
along the call graph.

`Documentation/Methode/Prompt.md` and `Strategy.md` (both in French) are the authoritative methodology
documents — read them before doing reconstruction work. The summary below is not a substitute.

## Build and run

**Whether the `dotnet` on PATH carries an SDK is machine-dependent — check before assuming.** On
the current machine, `C:\Program Files (x86)\dotnet` is on PATH and already ships an x86 SDK
(9.0.317), so a plain `dotnet build OpenForesia.sln` / `dotnet run --project OpenForesia` works
with no `DOTNET_ROOT` juggling. An earlier machine used for this project had only an x64
*runtime* on PATH, with the actual SDK installed separately under the user profile; on a machine
like that, every `dotnet` invocation needs to point at the real SDK first:

```powershell
$env:DOTNET_ROOT = "<path to the real SDK>"
$env:PATH = "<path to the real SDK>;$env:PATH"
dotnet build OpenForesia.sln
```

Verify with `dotnet --list-sdks`: an empty/failing result means PATH is runtime-only and the SDK
needs to be located and pointed at explicitly. `playtest.ps1`'s `-Build` switch already detects
this (tries a plain build first, falls back to a known SDK path if PATH has none) — extend that
fallback path if it's stale rather than hardcoding a new one elsewhere.

Two `NETSDK1189` warnings about `Prefer32Bit` are expected and harmless. Verification of the
transcribed logic is `dotnet build` plus reading the reconstruction against `Blaze.c`, and then the
screen (see "Running the game" below) — there is no behavioural test suite and there is not meant
to be one.

`OpenForesia.Tests` (`dotnet test OpenForesia.Tests`) is the one exception, and it tests **the
memory map, not behaviour**: six assertions that read the *sources* looking for the aliasing defect
— two names for one address of `.data`, modelled as two separate allocations. **Some of them are
red on purpose.** A red assertion there names a real defect the repo carries and has chosen not to
fix yet (fixing changes behaviour); the header of `MemoryLayoutTests.cs` says which and why. Do not
"make the tests pass" — read what they name, and fix it only as a deliberate change.

`dotnet run --project OpenForesia` can fail from the shell on machines where the SDK on PATH only
carries an x64 runtime while the game is x86 — in that case launch
`OpenForesia/bin/Debug/net9.0/OpenForesia.exe` directly, or use `playtest.ps1` below, which handles
it either way.

**Neither the SDK nor `node` is on the agent's PATH**, even when both are installed — same trap,
twice. `node` lives on `D:\Logiciels\nodejs` and is needed by the `openspec` CLI. Prefix once per
shell:

```powershell
$env:PATH = "C:\Program Files (x86)\dotnet;D:\Logiciels\nodejs;$env:PATH"
```

**Smart App Control blocks `OpenForesia.Tools`' Debug build.** This machine runs Smart App Control
in enforced mode (`HKLM:\SYSTEM\CurrentControlSet\Control\CI\Policy` →
`VerifiedAndReputablePolicyState = 1`), and it periodically refuses to load the freshly-built,
unsigned `OpenForesia.Tools.dll` out of `bin\Debug\`:

```
System.IO.FileLoadException: ... Une stratégie de contrôle d'application a bloqué ce fichier. (0x800711C7)
```

It is **not** Mark-of-the-Web — the file carries no `Zone.Identifier` stream, so `Unblock-File`
does nothing. The working answer is a **Release publish**, which SAC accepts:

```powershell
dotnet publish OpenForesia.Tools -c Release -o OpenForesia.Tools\bin\publish
OpenForesia.Tools\bin\publish\OpenForesia.Tools.exe disasm Legacy/Binaries/Blaze.exe 0x00516a7d 12
```

Do this rather than reaching for the security setting: **turning Smart App Control off is
irreversible** — Microsoft does not allow re-enabling it without reinstalling Windows. Never flip
it without the user asking for that specific change, in those terms.

If even the publish is refused, the fallback that needs no tooling at all is reading the binary's
bytes directly — the `.text` file offset is `VA - 0x400C00`:

```bash
od -An -tx1 -j $((0x115E7D)) -N 24 Legacy/Binaries/Blaze.exe   # VA 0x00516a7d
```

## Running the game — always via `test-protocol.md`

**Before exercising the game in any way — launching it, reaching a screen, reproducing a crash,
capturing a screenshot — read `Documentation/Tests/test-protocol.md` and drive the run with
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
dotnet run --project OpenForesia.Tools -- Legacy/Binaries/winsys3d.dll         # exports
dotnet run --project OpenForesia.Tools -- sections Legacy/Binaries/Blaze.exe           # VA ranges
dotnet run --project OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x005631f8 32   # bytes at a Ghidra VA
dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x004747f0 724  # x86-32
```

Both verbs take a **Ghidra virtual address** (hex, `0x` optional); byte counts are decimal.

`at` prints the resolved file offset and section alongside hex, C-string and dword renderings. An
address landing in `.data`'s uninitialised tail is reported as such — that means "zero at load,
written only by code", which is an answer, not an error.

`disasm` prints x86-32 instructions with internal jump targets labelled `LAB_…`. **This is what
makes a body Ghidra never decompiled reachable** — see "When `Blaze.c` doesn't have the answer"
below. Keep any dump you rely on under `Legacy/Raw/Blaze/`.

## Layout

**The repository root holds exactly two files** — `CLAUDE.md` and `OpenForesia.sln`. Everything
else lives under one of the directories below. Reorganised 2026-08-21; before that, the
decompilations, the DLL binaries and eleven `.md` files were all loose at the root.

```
Legacy/          tout ce qui vient de 1998, rien d'ecrit par nous
  Binaries/      Blaze.exe, Blaze.cfg, et les cinq DLL d'aide
  Decompiled/    Blaze.c et les cinq .c de DLL — sortie Ghidra, LECTURE SEULE
  Raw/           relevés bruts par fonction (desassemblage, exports Ghidra)

Documentation/   tout ce que le projet a appris
  ARCHITECTURE.md
  DLL/           un fichier par DLL d'aide
  Etat/          TODO.md, REVERSE_STATUS.md, HISTORY.md
  Methode/       Prompt.md, Strategy.md
  Reference/     DataSymbols.md, legacy-bug.md, OpenForesia.cfg.md
  Tests/         test-protocol.md, test-todo-later.md, et les analyses ponctuelles
```

| Path | Role |
|---|---|
| `Legacy/Decompiled/Blaze.c` (98k lines) | Ghidra decompilation of `Blaze.exe`. **Source of truth. Never modify.** |
| `Legacy/Binaries/Blaze.exe` | The game binary itself (MD5 `46d90d1b4526f4a9a441f851c77179b3`). Same build as `Blaze.c`: image base `0x400000` + entry RVA `0x147D40` = `0x00547D40` = `entry`. |
| `Legacy/Decompiled/winsys3d.c`, `wsyskern.c`, `playst.c`, `3dhelp.c`, `gldhelp.c` | Ghidra decompilations of the five helper DLLs. **Secondary references, read-only.** |
| `Legacy/Binaries/*.dll` | The actual DLL binaries — this is what makes ordinal-only imports resolvable. |
| `Documentation/ARCHITECTURE.md` | **Read this before adding or moving code.** The directory map, the routing rule that decides where a function goes, and the state rule that protects against the aliasing defect. |
| `Documentation/Etat/REVERSE_STATUS.md` | **Read this first.** Call-graph coverage, per-function status, numbered `TODO(REVERSE)` items, current blocking point. |
| `Documentation/Etat/TODO.md` | The between-sessions entry point: current state and what to do next. |
| `Documentation/Etat/HISTORY.md` | **Update it at the end of every work session — see below.** One line per change, newest first. |
| `Documentation/Methode/Prompt.md`, `Strategy.md` | The authoritative methodology documents, both in French. |
| `Documentation/Reference/DataSymbols.md` | Values recovered from the binary's data sections, keyed by Ghidra VA. Also carries the **map of reconstructed symbols**: every VA-suffixed C# symbol, sorted by address, with its gap to the next and the file it lives in. |
| `Documentation/Reference/legacy-bug.md` | **Bugs of the 1998 game itself**, observed in play and traced in the decompilation. Check here before "fixing" an oddity: it may be a faithful transcription of an original defect, and the project reproduces those rather than repairing them. |
| `Documentation/Tests/test-protocol.md` | How to exercise the game. Read it before any run — see "Running the game" above. |
| `Documentation/DLL/<dll>.md` | Accumulated knowledge per DLL (ordinals, signatures, confirmed vs. hypothesis). |
| `OpenForesia/src/Blaze/**` | The reconstruction, split into `Platform/`, `Memory/`, `Engine/`, `Screens/` — **see `Documentation/ARCHITECTURE.md`**. |
| `OpenForesia/src/Host/**` | Our 2026 scaffolding — the Direct3D 11 replacement for the 1998 display chain. **Not bound by fidelity.** |
| `OpenForesia.Tools/` | PE export-table dumper, **x86-32 disassembler** (`disasm`, via Iced), `playtest.ps1`, and the split tooling (`split-manifest.awk`, `apply-split.ps1`, `verify-split.ps1`, `blaze-baseline.txt`, `bodies-baseline.txt`). |
| `OpenForesia/src/DLL/*.dll` | Copies of the helper DLLs, flattened into the build output for P/Invoke. |
| `Analyse/` | Notes d'observation du jeu original (comportement, sessions x32dbg). |

`openspec/` holds the change workflow: several completed changes, one archived, and the
`blaze-intro-attract` spec. Use `openspec list` to see the current state.

## Closing a session — always update `HISTORY.md`

**At the end of every work session, add your entries to `Documentation/Etat/HISTORY.md`.** This is
not optional and does not need to be asked for. It is the one file that shows overall progress at a
glance: `REVERSE_STATUS.md` is too detailed for that, and `TODO.md` only describes the current
state.

Keep it **simple** — one line per change, prefixed by its kind:

```markdown
## 2026-08-23

- feat : la carte du monde s'affiche et tient — l'auberge, 2101 images sans plantage
- fix : `ResolveEntityBlocks` lisait les statistiques d'entité par une vue d'arène quand le reste de
  la simulation les lit par leur allocation propre — toute entité de scène naissait à 0 PV
```

`feat :` for an addition, `fix :` for a correction, `doc :` for something learned and written down.
Newest date at the top. No analysis paragraphs — the reasoning belongs in `REVERSE_STATUS.md`;
`HISTORY.md` keeps only the line of progress.

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

## Layout of the reconstruction

`Blaze.cs` was split on 2026-08-19 into 30 files under `OpenForesia/src/Blaze/`, plus
`OpenForesia/src/Host/` for the scaffolding. **`ARCHITECTURE.md` is the authority** — read it before
adding or moving code. The short version:

```
   Platform/   ce qui n'est pas le jeu : interop OS, CRT, winsys3d, demarrage/extinction
   Memory/     l'espace d'adressage : les huit arenes, les tables .data (GameMemory)
   Engine/     appele par plusieurs ecrans : Audio, Video, Input, Resources, SaveData,
               Model3d, UiText, UiWindow, Primitives2d
   Screens/    un repertoire par etat de jeu (State00 .. State06), plus le repartiteur
```

**Where a newly-named function goes** — the rule, in order:

1. its VA falls in a known engine address range → that `Engine/` file;
2. a **single** screen calls it → that `Screens/StateNN/` file;
3. **several** screens call it → promote to `Engine/` and give it a subsystem name;
4. otherwise → **it stays with its caller** until the role is established. There is no dumping
   ground file, and deliberately so.

**Naming**: neutral, descriptive, all `static`. **Never** `Manager`, `Service`, `Controller`,
`Provider` — those names promise an object with a lifecycle that the transcription does not have,
and invite drift towards a design the source of truth does not contain.

**No maximum file size.** `Screens/State06/Inventory.cs` is 2 937 lines for three sub-screens of one
menu, and that is coherent. The metric is coherence, not size.

**Never move work in progress.** A finished piece gets filed; if nothing fits, it stays with its
caller.

### Two traps the split itself uncovered

**A global never moves to make a file self-contained.** It moves only when the address map says so;
arenas move as one block. Consult the map of reconstructed symbols in `Documentation/Reference/DataSymbols.md`
before declaring an isolated `DAT_`: 107 pairs of address-adjacent symbols now live in two different
files, and that adjacency is no longer visible in the code.

**Splitting a partial class across files is not semantically neutral.** Static field initialisers run
in the order the compiler receives the *files*, which is unspecified. A `static readonly` field whose
initialiser reads a static member living in **another** file must become a computed `=>` property.
Intra-file dependencies are safe — textual order is guaranteed within one file. This one bit: eight
fields of the form `= Arena(0x…)` returned `null + offset` and the game threw on the first frame.

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

## When `Blaze.c` doesn't have the answer — read the binary

`Blaze.c` is a **code-only** decompilation: Ghidra emitted function bodies but not the
`.data`/`.rdata` sections, and it left some bodies undecompiled entirely. Two gap classes follow,
and **both are now solvable inside the repo** — the binary is committed at
`Legacy/Binaries/Blaze.exe`.

**Values that live in initialized data** (string literals, lookup tables, statically initialized
function pointers such as `PTR_FUN_0056c96c`) are not in `Blaze.c` at any price. Read them out of
the binary:

```powershell
dotnet run --project OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x005631f8 32
```

Record what you recover in `Documentation/Reference/DataSymbols.md`, keyed by VA. **Never invent
plausible content** for one of these — an address that lands in `.data`'s uninitialised tail is
reported as such, and that is an answer.

**Bodies Ghidra did not decompile** — address-taken-only code (window procedures, thread procedures,
vtable slots appearing only as `LAB_…`) and whole functions missing from `Blaze.c`. Until
2026-08-21 the standing advice was "this needs a targeted Ghidra re-run", which meant an action
outside the repository — so in practice it meant *blocked forever*. It no longer does:

```powershell
dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x004747f0 724
```

`disasm` prints x86-32 with internal jump targets labelled. It is **not** a decompiler — no
reconstructed control flow, no types, no names — but it is enough to transcribe a function by hand,
and `FUN_004747F0` (723 bytes, absent from `Blaze.c`, blocking game state 3 for three days) came out
of it in one pass. Keep the dump under `Legacy/Raw/Blaze/` next to the transcription.

**A byte count is not a difficulty estimate.** That same function had been written off as
"723 bytes with nine convergent branches — that's a decompiler's job". It turned out to be a
perfectly regular cascade of eight near-identical blocks, and the nine "convergent exits" were nine
`return`s. What predicts the cost is the number of *distinct shapes*, not the size.

**Check for a duplicate before transcribing.** `LAB_00474320` sat on the gap list for two days as
"the callback that actually stores a chest's contents". Its 87 bytes turn out to be *identical* to
already-transcribed `FUN_00450700` — one difference, the `call`'s relative displacement, and both
resolve to the same target. The compiler emitted the same body twice. Two `od` dumps and a
subtraction settle it, so run them whenever two addresses play the same role:

```bash
od -An -tx1 -j $((VA - 0x400C00)) -N <len> Legacy/Binaries/Blaze.exe
```

**What is still genuinely blocked**: functions whose parameters Ghidra failed to recover
(`unaff_ESI`, `extraout_var`, fewer parameters than call sites pass). Disassembly resolves those too
in principle, but reconstructing a calling convention from register traffic is real work — flag it
inline rather than guessing a signature.
