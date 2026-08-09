# Blaze reverse-engineering status

Tracks progress reconstructing `Blaze.c` (Ghidra pseudocode, read-only) into
`src/Blaze/Blaze.cs`. Read `Prompt.md` and `Strategy.md` (repo root) for the
methodology before continuing this work.

Build: `dotnet build OpenForesia.sln` — builds clean as of this update.
Note for this machine: the .NET SDK is not on PATH / not in the usual install
locations; a working SDK was found at `C:\Users\Sagara\.dotnet` (prepend it to
`PATH` and set `DOTNET_ROOT` before calling `dotnet`).

## Call graph covered so far

```
entry (0x00547d40)                          [DONE]
├─ GetVersion / HeapInit / IoInit / SetupMultibyteCodePage      [DONE, abstracted]
├─ GetEnvironmentBlockAnsi / BuildEnviron / SetArgv / ParseCmdLine [DONE, faithful]
├─ RunStaticInitializers (FUN_00547740)      [STUB — TODO, see below]
└─ RunApplication (FUN_00513900, WinMain)    [DONE — control flow faithful]
   ├─ GetDriveTypeA CD-drive scan loop       [DONE]
   ├─ Ordinal_331 / Ordinal_336 (winsys3d.dll, unnamed ordinals) [DONE as opaque P/Invoke]
   ├─ LoadBitmapEx (wsyskern.dll)            [DONE as P/Invoke — confirmed named export]
   ├─ Setup3DDevice (winsys3d.dll ordinal 823) [DONE as P/Invoke — confirmed named export]
   ├─ CoInitialize / CoUninitialize          [DONE]
   ├─ SaveAndMaxWaveOutVolume (FUN_00513890) [DONE, faithful]
   ├─ RestoreWaveOutVolume (FUN_005138d0)    [DONE, faithful]
   ├─ Ordinal_337 / Ordinal_450 / Ordinal_451 / Ordinal_790 (winsys3d.dll) [DONE as opaque P/Invoke]
   ├─ MainEngineInit (FUN_0050f140)          [NOT STARTED — throws NotImplementedException]
   ├─ RunMainLoop (FUN_00529c10)             [NOT STARTED — likely the real game loop, throws]
   ├─ ShutdownStage1 (FUN_00504600)          [NOT STARTED — throws]
   ├─ ShutdownStage2 (thunk_FUN_0050f300)    [NOT STARTED — throws]
   ├─ ShutdownStage3 (FUN_00512ff0)          [NOT STARTED — throws]
   ├─ ShutdownStage4 (FUN_00542f50 -> FUN_005428d0) [NOT STARTED — throws]
   └─ ShutdownStage5 (FUN_00542f90)          [NOT STARTED — throws]
```

## Functions reconstructed (faithful logic, not just stubs)

| Ghidra name | Address | C# name | Notes |
|---|---|---|---|
| `entry` | 0x00547d40 | `entry` | PE entry point / `WinMainCRTStartup` |
| `FUN_00513900` | 0x00513900 | `RunApplication` | The real WinMain; Blaze & Blade's own code starts here |
| `FUN_00513890` | 0x00513890 | `SaveAndMaxWaveOutVolume` | |
| `FUN_005138d0` | 0x005138d0 | `RestoreWaveOutVolume` | |
| `FUN_00548af0` | 0x00548af0 | `HeapInit` | CRT `__heap_init`; SBH internals (FUN_00548b30) intentionally not transcribed, see below |
| `FUN_00547690`+`FUN_005476b0`+`FUN_00547700`+`FUN_00548ad0` | 0x00547690 area | `Malloc` | CRT `malloc` chain, collapsed onto `NativeMemory.Alloc` |
| `FUN_00547f10`+`FUN_00548dd0`+`FUN_00548e30` | 0x00547f10 area | `Free` | CRT `free` chain, collapsed onto `NativeMemory.Free` |
| `FUN_00547770` | 0x00547770 | `Exit` | CRT `exit` |
| `FUN_005477b0` | 0x005477b0 | `DoExit` | CRT `doexit` |
| `__amsg_exit` | 0x00547ee0 | `AmsgExit` | Message text approximated, see TODO |
| `FUN_0054a3d0` | 0x0054a3d0 | `GetEnvironmentBlockAnsi` | CRT env-block fetch (Unicode→ANSI) |
| `FUN_0054a030` | 0x0054a030 | `BuildEnviron` | CRT `_setenvp`, builds `environ[]` |
| `FUN_0054a120` | 0x0054a120 | `SetArgv` | CRT `_setargv` |
| `FUN_0054a1c0` | 0x0054a1c0 | `ParseCmdLine` | CRT `parse_cmdline`, direct port |
| `FUN_00549fd0`+`FUN_00549ff0` | 0x00549fd0 area | `IsLeadByte` | Replaced table lookup with `IsDBCSLeadByte` P/Invoke |

## Functions abstracted away (CRT-internal, no Blaze-specific behavior)

Per Prompt.md/Strategy.md, these are recognized MSVC 6 ("Visual Studio 1998")
CRT startup internals and are represented as thin/no-op C# methods rather than
transcribed, with Ghidra addresses kept in comments for traceability:

- `FUN_00548b30` (SBH/small-block-heap arena init) — folded into `HeapInit`'s comment; superseded by `NativeMemory`.
- `FUN_0054a810` (`_ioinit`, CRT file-descriptor table) — `IoInit()`, no-op; superseded by `System.IO`/`Console`.
- `FUN_0054a800` + `FUN_0054a530` (`__setmbcp` + DBCS table builder) — `SetupMultibyteCodePage()`, no-op; superseded by direct `IsDBCSLeadByte` calls at each use site.
- `FUN_00547740` (`_cinit`-style static-initializer runner) + `FUN_00547860` (`_initterm`) — `RunStaticInitializers()` / `RunStaticDestructors()`, currently empty. **Revisit**: the actual C/C++ static-initializer function-pointer tables are linker-generated data not present in Blaze.c's text; if global C++ objects are discovered later while reconstructing deeper subsystems, this is where their construction/destruction needs to be wired in.

## Known TODO(REVERSE) items

1. **Ordinal_331/336/337/450/451/790** (winsys3d.dll, called from `RunApplication`): Blaze.exe's own import table isn't available (only `Blaze.c`, the decompiled function bodies) — the DLL attribution is inferred from winsys3d.dll being the only nearby DLL with unnamed-ordinal exports, not confirmed against the real import table. Signatures are inferred purely from call-site usage. Confirm if `Blaze.exe` itself ever becomes available for a direct import-table dump.
2. **CD-check filename template** (`DAT_00563280`.._0056328c, 16 bytes forming a filename copied onto the found CD drive letter): the byte values are linker/data-section content not present in `Blaze.c`'s text output. `RunApplication`'s `cdCheckPath` buffer is currently a placeholder that only gets the correct drive letter substituted at index 0; the rest is unfilled.
3. **`PTR_s_Blaze___Blade_00563208`** (3rd arg to `Setup3DDevice`, presumably the app/window title pointer table): not recovered; `RunApplication` currently passes `driveRoot` as a placeholder pointer, which is almost certainly wrong and should be revisited once the string table is understood.
4. **`s_Please_insert_Blaze___Blade_CD__00563258` / `s_Erreur_00563278`** message strings: only Ghidra's auto-derived symbol names survive in `Blaze.c`, not the actual string bytes. `s_PleaseInsertCdMessage`/`s_ErrorCaption` in `Blaze.cs` are best-guess text reconstructed from the symbol name, not verified against the binary.
5. **`DAT_00600920` lookup table** (0x1000 entries, built from `Ordinal_451`/`Ordinal_450` in `RunApplication`): purpose unknown.
6. **`DAT_005db85c`** (main window `HWND`, passed to `DestroyWindow` after the main loop returns): not yet threaded through anywhere — window creation hasn't been located/reconstructed yet. `RunApplication`'s shutdown sequence currently skips the `DestroyWindow` call entirely; needs revisiting once a `CreateWindow`-equivalent is found (almost certainly inside `MainEngineInit`/`FUN_0050f140` or `RunMainLoop`/`FUN_00529c10`).
7. **atexit() registration**: `DoExit` walks a table (`DAT_00607950`..`DAT_0060794c`) that nothing currently populates — no `atexit()` call sites have been reconstructed yet in the reachable call graph. Revisit if/when one turns up.

## Next area to explore

The next, and by far the largest, unexplored subtree is everything reached
from `RunApplication`'s inner block:

- `FUN_0050f140` (`MainEngineInit`) — called once before the loop; likely
  window creation + Direct3D/OpenGL device bring-up (see `3dhelp.dll` /
  `gldhelp.dll`, both exporting a single `Create3DDevice`, suggesting a
  renderer-swap architecture selected here).
- `FUN_00529c10` (`RunMainLoop`) — this is almost certainly the actual game
  loop (message pump + per-frame update/render). This is where the bulk of
  "Blaze & Blade" gameplay-specific code will start showing up 60k+ lines
  away in `Blaze.c` (search for `/* FUN_00529c10 @` to find it — it's a very
  large function, likely worth breaking into several passes).
- The five shutdown stages (`FUN_00504600`, `thunk_FUN_0050f300`,
  `FUN_00512ff0`, `FUN_00542f50`→`FUN_005428d0`, `FUN_00542f90`).

Recommended order: `MainEngineInit` first (bounded, likely reveals the window
handle needed for TODO #6 above and the `playst.dll`/audio subsystem init),
then start breaking down `RunMainLoop` incrementally rather than all at once
given its size.

## Tooling note

A small PE export-table dumper was written to `scratchpad/pe_exports.py`
(session scratchpad, not committed) to resolve ordinal-only imports against
the actual DLLs shipped next to Blaze.exe (`playst.dll`, `winsys3d.dll`,
`wsyskern.dll`, `3dhelp.dll`, `gldhelp.dll`). Re-run it if more `Ordinal_NNN`
calls need identifying — it's a pure-Python PE32 export-directory parser, no
external dependencies.
