# Blaze reverse-engineering status

Tracks progress reconstructing `Blaze.c` (Ghidra pseudocode, read-only) into
`OpenForesia/src/Blaze/` — split on 2026-08-19 into `Platform/`, `Memory/`,
`Engine/` and `Screens/`, all one `partial class Blaze`. **`ARCHITECTURE.md`
(`Documentation/`) is the authority on where a function goes**; read it before adding
or moving code. Read `Prompt.md` and `Strategy.md` (`Documentation/Methode/`) for the
methodology before continuing this work. Strategy.md now also covers the
decompiled DLL sources (`3dhelp.c`, `gldhelp.c`, `playst.c`, `winsys3d.c`,
`wsyskern.c`, `Legacy/Decompiled/`, **read-only**) and `Documentation/DLL/<dll>.md` as
the running memory of what's been learned about each — check there before
re-analyzing an external call, and update it when a new one is understood.

**The binary is now in the repo at `Legacy/Binaries/Blaze.exe`** — the "systemic blocker"
section below is resolved, and `Documentation/Reference/DataSymbols.md` holds every value
recovered from its data sections. Read more with
`OpenForesia.Tools at Legacy/Binaries/Blaze.exe <va>`.

Build: `dotnet build OpenForesia.sln` — builds clean as of this update.
Run: the app targets **x86**. Whether a plain `dotnet build`/`dotnet run` works
depends on the machine — check `dotnet --list-sdks` first:

- If the `dotnet` on PATH already has an SDK (true on the machine this was last
  run on: `C:\Program Files (x86)\dotnet`, SDK 9.0.317, **x86**), `dotnet build`
  and `dotnet run --project OpenForesia` both work directly, no environment
  juggling needed.
- If PATH only carries a runtime (true on an earlier machine used for this
  project, x64-only), building and running need *different* environments:
  build with `DOTNET_ROOT` pointed at a real SDK, then **unset it** before
  launching — `OpenForesia\bin\Debug\net9.0\OpenForesia.exe` directly, or via
  `playtest.ps1`. A leftover x64 `DOTNET_ROOT` makes the x86 apphost fail with
  `0x800700C1` trying to load the x64 `hostfxr.dll`.

`playtest.ps1 -Build` already detects which case applies — see its "construction"
section — so prefer it over reproducing this logic by hand.

## Call graph covered so far

```
entry (0x00547d40)                          [DONE]
├─ GetVersion / HeapInit / IoInit / SetupMultibyteCodePage      [DONE, abstracted]
├─ GetEnvironmentBlockAnsi / BuildEnviron / SetArgv / ParseCmdLine [DONE, faithful]
├─ RunStaticInitializers (FUN_00547740)      [STUB — TODO, see below]
└─ RunApplication (FUN_00513900, WinMain)    [DONE — control flow faithful]
   ├─ GetDriveTypeA CD-drive scan loop       [DONE — simplified by hand after the first pass;
   │                                           the X/Z-drive special-casing and the inline
   │                                           CD-check open/close inside the scan loop are no
   │                                           longer present. Left as-is per current file state.
   │                                           This session: now also persists the found drive
   │                                           root into s_CdDriveRootPath (Ghidra: DAT_005631f8),
   │                                           which RunMainLoop/PlayMovie read back.]
   ├─ OpenExistingFileReadOnly/CloseHandle (winsys3d.dll ordinals 331/336, confirmed as thin
   │  CreateFileA/CloseHandle wrappers in winsys3d.c — now called directly) [DONE]
   ├─ LoadBitmapEx (wsyskern.dll, ordinal 114) [DONE — signature confirmed against wsyskern.c]
   ├─ Setup3DDevice (winsys3d.dll ordinal 823) [DONE as opaque P/Invoke — body not yet read]
   ├─ CoInitialize / CoUninitialize          [DONE]
   ├─ SaveAndMaxWaveOutVolume (FUN_00513890) [DONE, faithful]
   ├─ RestoreWaveOutVolume (FUN_005138d0)    [DONE, faithful]
   ├─ InitTrigTables / GetCosTableEntry / GetSinTableEntry / SetDeviceLostCallback
   │  (winsys3d.dll ordinals 337/450/451/790) [DONE — confirmed + renamed against winsys3d.c]
   ├─ LoadOptionsIni (FUN_0050f140)          [DONE — see below for full sub-graph]
   ├─ RunMainLoop (FUN_00529c10)             [DONE this session — see full sub-graph below;
   │                                           **runs its own logic through to the real per-frame
   │                                           game loop**, which throws at the first genuinely
   │                                           unrecoverable dependency — new blocking point,
   │                                           see "Next area to explore" below]
   ├─ ShutdownStage1 (FUN_00504600)          [DONE — joystick/DirectInput device teardown via
   │                                           FUN_00523200 -> FUN_00522e40]
   ├─ SaveOptionsIni (thunk_FUN_0050f300)    [DONE — mirrors LoadOptionsIni]
   ├─ ShutdownStage3 (FUN_00512ff0)          [DONE — the DirectSound shutdown; confirmed the
   │                                           IDirectSound/IDirectSoundBuffer identities, see below]
   │   (the rest of the DirectSound subsystem, FUN_00513xxx, is reconstructed this session but is
   │    not reachable from RunApplication — see its own sub-graph below)
   ├─ ShutdownStage4 (FUN_00542f50 -> FUN_005428d0) [DONE this session — same function RunMainLoop
   │                                           itself calls mid-sequence, see CloseMovieStreamIfOpen]
   └─ ShutdownStage5 (FUN_00542f90)          [DONE this session — same function RunMainLoop
                                               itself calls mid-sequence, see ShutdownMoviePlayer]

LoadOptionsIni (FUN_0050f140) sub-graph      [DONE, earlier session]
├─ GetIniFilePath (GetModuleFileNameA + strrchr + ".ini")   [DONE]
├─ ReadIniInt / WriteIniInt (GetPrivateProfileIntA / WritePrivateProfileStringA) [DONE]
├─ ApplyCdVolume (FUN_005137b0)              [DONE, faithful]
├─ ApplyAllSoundBufferVolumes (FUN_00513800) [DONE, faithful]
├─ CreateTranslationTables (winsys3d.dll ordinal 756) [DONE — confirmed against winsys3d.c]
├─ TexFilterPossible (winsys3d.dll imported *data* symbol) [DONE — resolved via GetProcAddress]
├─ LoadKeyBindings (FUN_005235d0)            [DONE, faithful]
└─ SaveKeyBindings (FUN_00523540, via SaveOptionsIni) [DONE, faithful]

RunMainLoop (FUN_00529c10) sub-graph          [DONE this session]
├─ Movie/intro-video subsystem (Ghidra: FUN_00542xxx cluster, ~30 functions; C#: the
│  "Movie / intro-video subsystem" region in Blaze.cs)
│  ├─ InitMoviePlayer/ShutdownMoviePlayer (FUN_00542f60/FUN_00542f90)        [DONE, faithful]
│  ├─ CreateMoviePlayerWindow (FUN_00542740)   [PARTIAL — window class/creation logic faithful,
│  │                                             but RegisterClassA/CreateWindowExA calls are
│  │                                             skipped: the window procedure (&LAB_00542600)
│  │                                             has no decompiled body anywhere in Blaze.c, so
│  │                                             calling CreateWindowExA with no real WndProc
│  │                                             would crash on the first message. HWnd stays
│  │                                             Zero — see TODO(REVERSE) at the call site.]
│  ├─ DestroyMoviePlayerWindow (FUN_00542940)  [DONE, faithful — real DestroyWindow/UnregisterClassA]
│  ├─ OpenMovieStream/CloseMovieStream (FUN_00542800/FUN_005428d0) [PARTIAL — real Malloc/Free
│  │                                             of the two sub-objects, matches the original's
│  │                                             "always succeeds given memory" behavior; the
│  │                                             DirectDraw-wrapper and custom "stream" objects'
│  │                                             own construction (FUN_00512930/FUN_005431e0) is
│  │                                             TODO(REVERSE) — see banner comment in Blaze.cs]
│  ├─ TryOpenMovieStream/CloseMovieStreamIfOpen (FUN_00542f40/FUN_00542f50) [DONE, thin dispatchers]
│  ├─ ShowTitleScreenAndWait (FUN_00542f30 -> FUN_00542c40) [PARTIAL — **renamed this session**
│  │                                             from the misleading `RewindMovieFallbackFrame`:
│  │                                             this is the arcade-style attract-mode title screen
│  │                                             (load DATA\TITLE640.BMP, blit it, wait 20 s or
│  │                                             until key/mouse/joystick input), not a frame
│  │                                             rewind. The field reset and the **whole bitmap-load
│  │                                             prologue** are now faithful, including the
│  │                                             `return 1` on load failure — that return value is
│  │                                             what breaks the attract loop when no CD is present.
│  │                                             Still TODO(REVERSE): the StretchDIBits blit through
│  │                                             the DirectDraw-wrapper vtable and the 20 s wait,
│  │                                             both useless until a real window/WNDPROC exists
│  │                                             (#6/#14). **Fixed a startup hang while doing this**
│  │                                             — see TODO #31.]
│  ├─ TrySetupMovieStream (FUN_00542a70)       [STUB, returns false — TODO(REVERSE): entirely
│  │                                             built from calls through the unrecoverable custom
│  │                                             "stream"/DirectDraw-wrapper vtables; the original
│  │                                             already treats a failed open as a normal, handled
│  │                                             case, so this is a faithful degraded path, not an
│  │                                             invented one]
│  └─ PlayMoviePumpLoop/PlayMovie (FUN_00542b70/FUN_00542eb0) [DONE, faithful — real joystick
│                                                capture, message pump, WM_QUIT→RaiseException;
│                                                gracefully no-ops the play loop body since
│                                                TrySetupMovieStream always reports failure]
├─ SetupJoystickHandlers (FUN_00504460)        [PARTIAL — real Ordinal_6/10/41/63/64/66 P/Invokes
│                                                 and JoystickResetState (FUN_00504b10, faithful);
│                                                 FUN_00523120 (DirectInput joystick device
│                                                 creation) skipped, not yet reconstructed — see
│                                                 "Next area to explore" below]
├─ CheckDeviceCapabilityFlag (FUN_0050f120)    [DONE — real Ordinal_750 P/Invoke + opaque vtable
│                                                 call at a confirmed-real offset; exact semantic
│                                                 meaning of the capability bit is TODO(REVERSE)]
├─ SetShadingDefaults (FUN_00529e40)           [DONE — real Ordinal_378/368 P/Invokes with the
│                                                 literal struct values from Blaze.c; per-field
│                                                 semantics beyond the literals are TODO(REVERSE)]
├─ InitViewportSurfaces (FUN_00529eb0)         [DONE — real Ordinal_658/661/665/667/674 P/Invokes,
│                                                 faithful branch on DAT_005633a8 (meaning TODO)]
├─ LoadUiFontFromCd (inlined font-loading sequence at the end of FUN_00529c10) [DONE — real
│                                                 CreateFileA/SetFilePointer/ReadFile/CloseHandle,
│                                                 same precedent as OpenExistingFileReadOnly]
├─ LoadFirstLevelResources (FUN_0052a030)      [DONE this session — see full sub-graph below;
│                                                 runs to completion (loads "SYSTEM.INI"-equivalent
│                                                 + "BASE.SND"-equivalent, resets the entity-slot
│                                                 table) but throws mid-way through its directory-
│                                                 entry loop at the one genuinely Ghidra-unrecoverable
│                                                 dependency, DecodeResourceChunk/FUN_005322f0]
└─ RunGameLoop (FUN_0052a3d0)                  [DONE control-flow, including the per-state resource
                                                  lookup at the top of its tick loop (now wired up
                                                  to LoadResourceEntry); still throws at
                                                  PTR_FUN_0056c96c, see "Next area to explore" below]

LoadFirstLevelResources (FUN_0052a030) sub-graph [DONE this session]
├─ BuildCdPath (shared helper, factored out of the font-loading/movie-path-building patterns
│  already used elsewhere in RunMainLoop)         [DONE]
├─ IsKnownOverrideDestination (FUN_0052a660)    [PARTIAL — the 3-way comparison itself is faithful,
│                                                 but all 3 comparison targets are never assigned
│                                                 anywhere in the reconstructed graph yet, so it
│                                                 always returns false in practice; the 2 true-branch
│                                                 bodies that copy hardcoded native override tables
│                                                 (11+ further FUN_ calls in one of them) are stubbed,
│                                                 out of scope for this pass]
├─ LoadWholeFileToBuffer (FUN_00529b00)         [DONE, with a noted TODO(REVERSE): Ghidra's own
│                                                 analysis of this function is incomplete
│                                                 (unaff_ESI failure-path return, a suspicious literal
│                                                 argument where a 2nd parameter should be)]
├─ LoadResourceEntry (FUN_00529b50)             [DONE, faithful — including the intentional
│                                                 infinite-hang-on-open-failure branch, preserved
│                                                 per Ghidra's own "do nothing block with infinite
│                                                 loop" annotation rather than silently changed]
├─ FixupResourceHeader (FUN_00532070)           [PARTIAL — the fixed 60-dword header relocation pass
│                                                 is fully faithful; the schema-driven per-record pass
│                                                 degrades to a safe no-op since its schema table
│                                                 (DAT_0056aca8) is data-only content unavailable in
│                                                 Blaze.c's text]
└─ DecodeResourceChunk (FUN_005322f0)           [STUB, throws — genuinely unrecoverable even in
                                                  principle: Ghidra's own decompilation leaves
                                                  several inputs as unaff_ESI/unaff_DI/unaff_retaddr
                                                  and outputs as extraout_var*, meaning Ghidra itself
                                                  never determined this function's real parameters/
                                                  behavior. **Current blocking point** — see below]

DirectSound subsystem (FUN_00513xxx family)      [DONE this session]
Reachable from ShutdownStage3 only for its teardown half; the creation/playback half below is
called from the sound-effect code (0x00537xxx) and from PlayMusicTrack (FUN_00529ad0), neither of
which is reconstructed yet — so this subsystem is complete in itself but not yet wired to a caller.
├─ Sound effects (the "one-shot sample" API used by the 0x00537xxx effect code)
│  ├─ AllocateSoundBuffer (FUN_00513030)      [DONE — CreateSoundBuffer of an 11025 Hz / 8-bit /
│  │                                            stereo secondary buffer + first-free-slot parking
│  │                                            in the 50-entry table; Ghidra's register-attribution
│  │                                            bug (storing/releasing the *device* instead of the
│  │                                            created buffer) transcribed as its evident intent]
│  ├─ LockSoundBuffer / UnlockSoundBuffer (FUN_00513110 / FUN_00513150) [DONE, faithful —
│  │                                            IDirectSoundBuffer::Lock over DSBLOCK_ENTIREBUFFER
│  │                                            and the matching Unlock]
│  └─ PlaySoundBuffer (FUN_00513180)          [DONE, faithful — SetVolume + Play; the `looping`
│                                               parameter Ghidra lost (`unaff_retaddr`) is proven
│                                               real by all 5 call sites passing a literal 0]
└─ Streaming music
   ├─ StartMusicStream (FUN_005131d0)         [DONE, faithful — builds the 16-bit stereo streaming
   │                                            buffer (8 notification segments), acquires
   │                                            IDirectSoundNotify, creates the 8 position events,
   │                                            pre-decodes the first 80 ADPCM blocks into it and
   │                                            starts looping playback. **Only the CreateThread of
   │                                            the streaming worker is skipped** — see TODO #23]
   ├─ LoadMusicWaveFile (FUN_00513470)        [DONE, faithful — reads a whole MS-ADPCM .WAV into a
   │                                            5 MB scratch buffer, walks RIFF/WAVE chunks to find
   │                                            `fmt `/`data`. Note: `FUN_00547220(&LAB_004c4b40)`
   │                                            is Ghidra mistaking the constant 5,000,000 for a
   │                                            .text label — it is a plain allocation size, as the
   │                                            tail-zeroing loop's identical use of it confirms]
   ├─ StopMusicStream (FUN_005135d0)          [DONE, faithful — including the leaked stop-event
   │                                            handle (its loop bound stops just short of it)]
   ├─ ToggleMusicEnabled (FUN_00513860)       [DONE, faithful]
   ├─ IsStandardMsAdpcmFormat (FUN_00531f70)  [DONE — rejects anything that is not mono/stereo
   │                                            4-bit MS-ADPCM with the standard 7 coefficient
   │                                            pairs; see TODO #24 for the tables]
   └─ DecodeMsAdpcm (FUN_00531bb0)            [DONE, faithful — full MS-ADPCM block decoder
                                                (per-channel predictor/iDelta/iSamp1/iSamp2 header,
                                                nibble body with channels interleaved per byte),
                                                8- or 16-bit PCM output, returns bytes written]

Sound-effect layer (FUN_0053cea0 + the FUN_0053dxxx resamplers) [DONE this session]
The game's single sound entry point, ~200 call sites throughout Blaze.c. Sits directly on top of
the FUN_00513xxx effect API above.
├─ PlaySoundEffect (FUN_0053cea0)            [DONE, faithful — halves both gains, resolves the
│                                              sound id in the directory, dispatches by pitch band]
├─ PlaySampleDecimated (FUN_0053d1a0)        [DONE, faithful — keeps every step-th sample
│                                              (pitch x step); step 1 is the unity-pitch identity]
├─ PlaySampleDroppingOneIn (FUN_0053d2c0)    [DONE, faithful — ratio (n+1)/n, fine pitch-up steps]
├─ PlaySampleInsertingOneIn (FUN_0053d420)   [DONE, faithful — ratio (n+1)/(n+2), fine pitch-down]
├─ PlaySampleDoubledExceptOneIn (FUN_0053d640) [DONE, faithful — ratio n/(2n-1)]
├─ PlaySampleDoubled (FUN_0053d880)          [DONE, faithful — exactly half speed]
├─ PlayUiSound38/39/3A/3B (FUN_0053db80/dba0/db60/dbc0) [DONE — trivial fixed-id wrappers]
└─ FUN_0053da20 / FUN_0053db20 (positional gain calculation) [NOT DONE — deliberately; see TODO #28]
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
| `FUN_0050f140` | 0x0050f140 | `LoadOptionsIni` | Loads Blaze.ini; renamed from the earlier placeholder `MainEngineInit` now that its role (ini settings load) is confirmed — it is not engine/device init |
| `FUN_0050f300`+`thunk_FUN_0050f300` | 0x0050f300 / 0x0050f520 | `SaveOptionsIni` | Both Ghidra symbols are the same body (linker-duplicated); mirror of `LoadOptionsIni` |
| `FUN_005137b0` | 0x005137b0 | `ApplyCdVolume` | CD-volume → DirectSound-style attenuation |
| `FUN_00513800` | 0x00513800 | `ApplyAllSoundBufferVolumes` | Sound-volume → DirectSound-style attenuation, applied to all 50 buffer slots |
| `FUN_005235d0` | 0x005235d0 | `LoadKeyBindings` | `[Controls]` key-binding table load |
| `FUN_00523540` | 0x00523540 | `SaveKeyBindings` | `[Controls]` key-binding table save |
| `FUN_00529c10` | 0x00529c10 | `RunMainLoop` | Blaze & Blade's own main-loop entry point; runs intro movies (gracefully degraded, see below), re-inits engine/device state, loads the UI font, sets up the viewport, hands off to `RunGameLoop` |
| `FUN_00542f60`/`FUN_00542f90` | 0x00542f60/0x00542f90 | `InitMoviePlayer`/`ShutdownMoviePlayer` | Movie-player struct + window lifetime; also reused verbatim as `ShutdownStage5`/(via `CloseMovieStreamIfOpen`) `ShutdownStage4` |
| `FUN_00542740`/`FUN_00542940` | 0x00542740/0x00542940 | `CreateMoviePlayerWindow`/`DestroyMoviePlayerWindow` | Hidden VfW-class window; creation is a documented partial (see call-graph notes above) |
| `FUN_00542800`/`FUN_005428d0` | 0x00542800/0x005428d0 | `OpenMovieStream`/`CloseMovieStream` | Sub-object alloc/free; partial, see call-graph notes above |
| `FUN_00542f40`/`FUN_00542f50` | 0x00542f40/0x00542f50 | `TryOpenMovieStream`/`CloseMovieStreamIfOpen` | Thin dispatchers |
| `FUN_00542f30` | 0x00542f30 | `ShowTitleScreenAndWait` | Attract-mode title screen; bitmap-load prologue + its `return 1` failure path faithful, blit/20 s wait still TODO. Renamed from `RewindMovieFallbackFrame` |
| `LoadBitmapFile` | wsyskern ord. 115 | `LoadBitmapFile` | Whole-file .BMP loader, P/Invoked by name; `mode 0` = silent failure. See `Documentation/DLL/wsyskern.md` |
| `Free` | wsyskern ord. 89 | `WsysFree` | wsyskern's own deallocator — **a different heap** from the CRT `Free`; required for anything `LoadBitmapFile` returns |
| `FUN_00542a70` | 0x00542a70 | `TrySetupMovieStream` | Stub (always false), see call-graph notes above |
| `FUN_00542b70`/`FUN_00542eb0` | 0x00542b70/0x00542eb0 | `PlayMoviePumpLoop`/`PlayMovie` | Joystick capture + message pump, faithful |
| `FUN_00504460` | 0x00504460 | `SetupJoystickHandlers` | Partial, see call-graph notes above |
| `FUN_00504b10` | 0x00504b10 | `JoystickResetState` | Faithful |
| `FUN_0050f120` | 0x0050f120 | `CheckDeviceCapabilityFlag` | Faithful structure, opaque vtable call |
| `FUN_00529e40` | 0x00529e40 | `SetShadingDefaults` | Faithful |
| `FUN_00529eb0` | 0x00529eb0 | `InitViewportSurfaces` | First half faithful (4 libgpu env structs + 12 field stores); second half skipped while `GetMainMemPtr()` is null. Fixed a null-pointer crash here, see TODO #32 |
| (inlined in `FUN_00529c10`) | — | `LoadUiFontFromCd` | Faithful |
| `FUN_0052a3d0` | 0x0052a3d0 | `RunGameLoop` | Faithful control-flow, including the per-state resource lookup; throws at its remaining unresolved dependency (see "Next area to explore") |
| `FUN_0052a030` | 0x0052a030 | `LoadFirstLevelResources` | Faithful; throws mid-loop at `DecodeResourceChunk` |
| `FUN_00529b00` | 0x00529b00 | `LoadWholeFileToBuffer` | Faithful, with a noted Ghidra-incompleteness caveat (see TODO #18) |
| `FUN_00529b50` | 0x00529b50 | `LoadResourceEntry` | Faithful, including the intentional infinite-hang-on-failure branch |
| `FUN_00532070` | 0x00532070 | `FixupResourceHeader` | Faithful fixed-header pass; schema-driven pass degrades to a no-op (schema table data unavailable) |
| `FUN_0052a660` | 0x0052a660 | `IsKnownOverrideDestination` | Faithful 3-way comparison; both true-branch bodies stubbed |
| `FUN_00504600`+`FUN_00523200` | 0x00504600/0x00523200 | `ShutdownStage1` | Joystick/DirectInput device teardown |
| `FUN_00522e40` | 0x00522e40 | `DestroyJoystickDevice` | 0x8c4-byte device-struct destructor (size cross-checked against FUN_00523120's allocation) |
| `FUN_00512ff0` | 0x00512ff0 | `ShutdownStage3` | DirectSound shutdown |
| `FUN_00512ea0` | 0x00512ea0 | `ReleaseAllSoundBuffers` | Faithful, including the "skip a still-playing buffer" branch |
| `FUN_00513030` | 0x00513030 | `AllocateSoundBuffer` | Faithful intent; Ghidra's register-attribution bug corrected (see TODO #25) |
| `FUN_00513110` | 0x00513110 | `LockSoundBuffer` | Faithful — `IDirectSoundBuffer::Lock`, DSBLOCK_ENTIREBUFFER |
| `FUN_00513150` | 0x00513150 | `UnlockSoundBuffer` | Faithful — `IDirectSoundBuffer::Unlock` |
| `FUN_00513180` | 0x00513180 | `PlaySoundBuffer` | Faithful — SetVolume + Play; `looping` param recovered from the call sites |
| `FUN_005131d0` | 0x005131d0 | `StartMusicStream` | Faithful except the skipped worker-thread creation (TODO #23) |
| `FUN_00513470` | 0x00513470 | `LoadMusicWaveFile` | Faithful — MS-ADPCM .WAV load + RIFF chunk walk |
| `FUN_005135d0` | 0x005135d0 | `StopMusicStream` | Faithful, including the leaked stop-event handle |
| `FUN_00513860` | 0x00513860 | `ToggleMusicEnabled` | Faithful |
| `FUN_00531f70` | 0x00531f70 | `IsStandardMsAdpcmFormat` | Faithful; the coefficient tables it compares against are TODO #24 |
| `FUN_00531bb0` | 0x00531bb0 | `DecodeMsAdpcm` | Faithful full MS-ADPCM block decoder, 8/16-bit PCM output |
| `FUN_0053cea0` | 0x0053cea0 | `PlaySoundEffect` | The game's sound entry point (~200 call sites); pitch-band dispatcher |
| `FUN_0053d1a0` | 0x0053d1a0 | `PlaySampleDecimated` | Faithful; pitch x step |
| `FUN_0053d2c0` | 0x0053d2c0 | `PlaySampleDroppingOneIn` | Faithful; ratio (n+1)/n |
| `FUN_0053d420` | 0x0053d420 | `PlaySampleInsertingOneIn` | Faithful; ratio (n+1)/(n+2) |
| `FUN_0053d640` | 0x0053d640 | `PlaySampleDoubledExceptOneIn` | Faithful; ratio n/(2n-1) |
| `FUN_0053d880` | 0x0053d880 | `PlaySampleDoubled` | Faithful; exactly half speed |
| `FUN_0053db60`/`db80`/`dba0`/`dbc0` | 0x0053db60 area | `PlayUiSound3A`/`38`/`39`/`3B` | Fixed-id UI wrappers |

## Functions abstracted away (CRT-internal, no Blaze-specific behavior)

Per Prompt.md/Strategy.md, these are recognized MSVC 6 ("Visual Studio 1998")
CRT startup internals and are represented as thin/no-op C# methods rather than
transcribed, with Ghidra addresses kept in comments for traceability:

- `FUN_00548b30` (SBH/small-block-heap arena init) — folded into `HeapInit`'s comment; superseded by `NativeMemory`.
- `FUN_0054a810` (`_ioinit`, CRT file-descriptor table) — `IoInit()`, no-op; superseded by `System.IO`/`Console`.
- `FUN_0054a800` + `FUN_0054a530` (`__setmbcp` + DBCS table builder) — `SetupMultibyteCodePage()`, no-op; superseded by direct `IsDBCSLeadByte` calls at each use site.
- `FUN_00547740` (`_cinit`-style static-initializer runner) + `FUN_00547860` (`_initterm`) — `RunStaticInitializers()` / `RunStaticDestructors()`, currently empty. **Revisit**: the actual C/C++ static-initializer function-pointer tables are linker-generated data not present in Blaze.c's text; if global C++ objects are discovered later while reconstructing deeper subsystems, this is where their construction/destruction needs to be wired in.

## Known TODO(REVERSE) items

1. ~~**`Setup3DDevice` body**~~ — **RESOLVED**. Fully analyzed; see `Documentation/DLL/winsys3d.md`. Despite its name it creates no device: it is the **renderer-selection dialog/launcher** (enumerates DirectDraw drivers, probes for 3Dfx via `GLIDE2X.DLL`, shows modal dialog resource `0x66` — the same resource id whose bitmap `LoadBitmapEx` loads, i.e. the splash image — then writes the chosen driver back to the ini). The full argument-struct layout is documented there too.
2. **CD-check filename template** (`DAT_00563280`.._0056328c, 16 bytes forming a filename copied onto the found CD drive letter): the byte values are linker/data-section content not present in `Blaze.c`'s text output. `RunApplication`'s `cdCheckPath` buffer is currently a placeholder that only gets the correct drive letter substituted at index 0; the rest is unfilled. Note: the CD-drive-scan loop in `RunApplication` was hand-simplified after the initial pass (X/Z special-casing and the inline open/close check removed) — current behavior only checks `GetDriveTypeA`, so this placeholder may matter less than before.
3. **`PTR_s_Blaze___Blade_00563208`** — **structure resolved, contents partly open.** The dialog proc (`winsys3d.c:218-223`) proves it is a **5-entry ANSI string-pointer table**: `[0]` dialog title, `[1]`/`[2]` static labels, `[3]` OK caption, `[4]` Cancel caption. Entry `[0]` is `"Blaze & Blade"` (recorded in Ghidra's own symbol name for the table, and the same literal used as the movie window title). **Fixed a real defect while resolving this**: `RunApplication` had been passing its 4-byte `"X:\"` `driveRoot` buffer here, which would make the DLL read five garbage pointers off the stack and dereference them; it now passes a proper 5-entry table. *Still open*: entries `[1]`..`[4]` are `.data` string pointers (see "Systemic blocker") and currently hold explicit placeholder text — deliberately not null, because the DLL null-checks only the array pointer and then dereferences all five entries unconditionally.
4. **`s_Please_insert_Blaze___Blade_CD__00563258` / `s_Erreur_00563278`** message strings: only Ghidra's auto-derived symbol names survive in `Blaze.c`, not the actual string bytes. `s_PleaseInsertCdMessage`/`s_ErrorCaption` in `Blaze.cs` are best-guess text reconstructed from the symbol name, not verified against the binary.
5. **`DAT_00600920` lookup table**: purpose now narrowed (see `Documentation/DLL/winsys3d.md`) — it's a packed cos/sin table (`GetCosTableEntry`/`GetSinTableEntry`, ordinals 450/451, confirmed against `winsys3d.c`: both read the same `DAT_011cb420[0x1000]` sin table built by ordinal 337, 450 phase-shifted a quarter period). What *consumes* `DAT_00600920` is still unknown — presumably the not-yet-reconstructed rendering code.
6. **RESOLVED, 2026-08-24 — and the conclusion below was right about the *shape* of the answer, wrong about it being unreachable.** `DAT_005db85c` is written in exactly one place: `LAB_00512ef0`, the ordinal-790 callback (#8), at `0x00512eff` — `mov [5DB85Ch],esi`, `esi` being the callback's only parameter. **`Blaze.exe` never creates the main window; `winsys3d.dll` hands it over.** `FUN_00a06420` (ordinal 789) calls the registered callback with `DAT_011be79c`, its own `HWND`, both when the 3D device is first created and on every re-init. The very next instruction is `SetWindowTextA(hwnd, "Blaze & Blade")`, which is why the window's title changing is a free, visible proof that audio init ran. `DestroyWindow(DAT_005db85c)` at shutdown is restored. The old findings below all remain true — none of the six sources creates the window, because none of them is where it happens. Original note: ~~**`DAT_005db85c`** (main window `HWND`) — **searched exhaustively across all six sources; not found anywhere.** Findings:~~
   - In `Blaze.c` it is **read twice and never written** (`FUN_00523120(0, DAT_005db85c, …)` and `DestroyWindow(DAT_005db85c)`).
   - `Blaze.c` contains exactly **one** `CreateWindowExA` call — the movie player's hidden `"VfwClass"` helper window (TODO #14), which is *not* it.
   - `winsys3d.c` contains **no** `CreateWindow`/`RegisterClass` at all.
   - `wsyskern.c`'s `"HugoXX"` window is a throwaway parent for an error `MessageBoxA`, immediately destroyed.
   - `3dhelp.c` **receives** an `HWND` from its caller (stores it at device+0x18 and validates it with `IsWindow`); it never creates one. Its `GetDesktopWindow` use is only for `GetDeviceCaps`.
   - **CORRECTED — this next claim is exactly backwards.** `Ordinal_789(0, 0)` is the *real* 3D device initialiser: passing 0 as the first argument is what **triggers** full init (it loads `3dhelp.dll`, resolves `Create3DDevice`, and on failure shows "3D-Graphics initialisation error" then `ExitProcess(3)` — `winsys3d.c:5966`). Observed firing for real once this reconstruction held DirectDraw exclusively and never released it. The non-zero values are the *teardown* paths (-1 / -2). Original claim, wrong: ~~`winsys3d.c`'s `Ordinal_789` device-init branch is never reached: both `Blaze.c` call sites pass `0` as its first argument.~~

   **Caveat for our build**: with the synthetic device (`Host/SyntheticDevice.cs`), `3dhelp` is never initialised and `DAT_011be79c` stays null, so the callback arrives with a null handle and `SetCooperativeLevel` returns `E_INVALIDARG`. `Audio.DeviceInitCallback` substitutes the Direct3D 11 host window there — at the trampoline, nowhere deeper. Original conclusion: ~~the main window's creation site is **not present in any available source**. Most likely it lives in a `Blaze.exe` code path Ghidra rendered as a bodiless `LAB_`, or is reached through one of the unresolved indirect calls.~~ The first half of that guess was right.
7. **atexit() registration**: `DoExit` walks a table (`DAT_00607950`..`DAT_0060794c`) that nothing currently populates — no `atexit()` call sites have been reconstructed yet in the reachable call graph. Revisit if/when one turns up.
8. **RESOLVED, 2026-08-24 — transcribed from the binary as `Audio.InitAudioDevice`.** `LAB_00512ef0` is the **audio initialiser**, not a device-lost handler: it stores the `HWND` winsys3d passes it (#6), titles the window, zeroes the 50 sound-buffer slots, calls `DirectSoundCreate` (#26), sets `DSSCL_PRIORITY` cooperative level, and creates the primary buffer with a 22050 Hz / 16-bit / stereo `WAVEFORMATEX`. 248 bytes, one shape, no branching worth the name — the *third* time a body written off as needing "further disassembly" turned out to be an afternoon's work. Dump kept at `Legacy/Raw/Blaze/FUN_00512EF0.asm`. Signature settled as `void __stdcall (HWND)`: `ret 4`, and both winsys3d call sites (`0x00a06485`, `0x00a065f8`) `push ecx` — the DLL's own `HWND` — before `call eax`, even though Ghidra shows the first one taking no argument. Original note: ~~**Ordinal 790's callback** (`Blaze.c`: `&LAB_00512ef0`, passed to `SetDeviceLostCallback`): no decompiled function body anywhere in `Blaze.c` — Ghidra only ever saw it address-taken. Cannot be reconstructed without further disassembly. `winsys3d.c` confirms the DLL does call it back later (see `Documentation/DLL/winsys3d.md`), so this matters once device-loss/reset handling comes up.~~
9. ~~**`DAT_005db900`/`DAT_005db758[50]`** interface identity~~ — **RESOLVED**. Reading the shutdown path (`FUN_00512ff0`/`FUN_00512ea0`) and the buffer-creation path (`FUN_00513030`, `FUN_005133xx`) exercises enough distinct vtable slots to pin both interfaces against the documented DirectX 3–5 COM layouts with no ambiguity: `DAT_005db8e4` is **`IDirectSound`** (0x08 Release, 0x0c CreateSoundBuffer) and `DAT_005db900`/`DAT_005db758[50]` are **`IDirectSoundBuffer`** (0x08 Release, 0x24 GetStatus, 0x2c Lock, 0x30 Play, 0x3c SetVolume, 0x48 Stop, 0x4c Unlock). This retroactively confirms the 0x3c=SetVolume reading in `ApplyCdVolume`/`ApplyAllSoundBufferVolumes`. The 50-slot count is confirmed independently by `FUN_00512ea0`'s loop bound (`0x005db758`..`0x005db81f`). *Update, this session*: the **buffer**-creation paths are now reconstructed (`AllocateSoundBuffer`, `StartMusicStream`), and a third interface was added to the set — `DAT_005db904` is **`IDirectSoundNotify`** (0x08 Release, 0x0c SetNotificationPositions), pinned by `StartMusicStream` obtaining it via `QueryInterface` on the streaming buffer and then using it only with the argument pair `(8, &DAT_005db8a0)` where `DAT_005db8a0`..`DAT_005db8df` has just been filled as exactly 8 `{ dwOffset, hEventNotify }` pairs. *Still open*: the **device** itself (`DAT_005db8e4`) still has no assignment site anywhere in the reconstructed graph — no `DirectSoundCreate` call has been located — so every entry point still takes its "no device" early-out at runtime. See TODO #26.
10. **AU_00564a05 blob**: `s_SmallTextureFlag` only reconstructs one 4-byte field (`._51_4_`) of what's clearly a larger structure/array (`AU_00564a05`) — the rest of that blob hasn't been characterized.
11. **RESOLVED** — recovered from `Legacy/Binaries/Blaze.exe`, see `Documentation/Reference/DataSymbols.md`. The options-file extension (`DAT_005630bc`) is **`.cfg`, not `.ini`**, which is why `LoadOptionsIni` had never found a file; `DAT_005630a0` is `CdVol` (the former `UnknownIniKey_005630a0` placeholder); `DAT_005630c4` is `%d`. The full key set is `Size`, `Small`, `Filter`, `Gamma`, `CdVol`, `SoundVol` under section `Options`, corroborated by the installed game's own `BLAZE.cfg`. Original note kept below for context.

    *Update, this session*: this entry was marked RESOLVED but the finding had **never been carried into `Blaze.cs`**, which kept emitting the placeholder key `UNKNOWN_REVERSE_005630a0` — so the reconstruction wrote a duplicate of `Size` into the options file and read its own copy instead of the game's. Now applied (`SizeIniKey = "Size"`, `DAT_00563080` re-read from the binary to confirm). Two further corrections found while verifying: `s_Joy_d_00563064` is **`"Joy%d"` with no space** (the reconstruction used `"Joy %d"`, so every joystick setting silently fell back to its default), and the address in the old placeholder name was `CdVol`'s, not the key's. **Lesson: a RESOLVED entry here is not evidence the code applies it** — check the call site, not only the note.
    ~~**`DAT_00563080` ini key / `DAT_005630bc` ini extension / `DAT_005630c4` format string**: none of these three have a Ghidra-derived string name (unlike e.g. `s_SoundVol_005630b0`), so their exact text is a placeholder/guess in `Blaze.cs` (`UnknownIniKey_005630a0`, `IniFileExtension = ".ini"`) — see inline TODOs.~~
12. ~~**`DAT_00562e00` per-resolution clamp table**: content unknown; `s_ResolutionDetailLimit` in `Blaze.cs` is a placeholder filled with `int.MaxValue` (i.e. the clamp is currently a no-op).~~ — **RESOLVED**, read back from `Legacy/Binaries/Blaze.exe` as `{ 1, 7, 11, 16, 23, 0 }` and applied. Cross-checked against the installed `BLAZE.cfg`, which pairs `Resolution=4` with `Size=23`: index 4 is 23, so the clamp is a no-op on a file the game itself wrote — exactly as expected. The bytes immediately after (`01 02 03 04 05 06 07 08`) are a different table, so the array ends at six entries. The trailing `0` is a **terminator, not a sixth entry**: running with `Resolution=5` makes `winsys3d`'s base-offset table (`DAT_00a2c49c`, indexed by the same value) return junk — viewport `(176,133) 288x216` against the clean progression `320x240 / 512x384 / 640x480 / 800x600 / 1024x768` for indices 0..4. Five valid entries, indexed 0..4.
13. **Vtables now fully identified — the subsystem is DirectShow, and none of it is unrecoverable.** All fifteen previously-undisassembled targets were forced in Ghidra and exported to `Legacy/Raw/*.c`. What they show:
    - **The movie player is DirectShow / ActiveMovie, not VfW.** `FUN_00543230` calls `CoCreateInstance` with the GUIDs at `DAT_0054e8a8` / `DAT_0054e8b8`, dumped from the binary as **`{E436EBB3-524F-11CE-9F53-0020AF0BA770}` = `CLSID_FilterGraph`** and **`{56A868A9-0AD4-11CE-B03A-0020AF0BA770}` = `IID_IGraphBuilder`**. That also explains `RunApplication`'s `CoInitialize`, and the objects at `+0x11c` whose vtable slots `0x1c`/`0x20`/`0x24` are used by `FUN_005438a0`/`FUN_005438f0`/`FUN_00543920` — an `IVideoWindow`-shaped interface.
    - **The overlay object (vtable `0x0054D028`) is a COM callback sink**: slots 0-2 are adjustor thunks (`FUN_00512E60`/`E80`/`E90`, all forwarding to `this - 8`, i.e. multiple inheritance), slot 3 is the real callback (`FUN_00512DD0`, returning `E_POINTER` on a null inner pointer), slot 4 a no-op, slot 5 posts `0x465` to the player window.
    - **`FUN_00547680` is `_purecall`** (`__amsg_exit(0x19)`). It fills every unimplemented vtable slot — 11 occurrences across two vtables — so hitting one aborts by design, and those slots need no reconstruction.
    - `FUN_00512a10`, `FUN_00512b40`, `FUN_00512c80` and `FUN_005431e0` all already had bodies in `Blaze.c`.
    **Correction to an earlier claim in this file and in session notes**: recovering the overlay vtable does *not* by itself make anything appear on screen. `FUN_00542c40`'s blit dereferences `*(wrapper + 4)`, which `FUN_00512930` explicitly sets to **0**; it is `FUN_00512a10` — the real DirectDraw surface/cooperative-level sequence — that populates it. The display path is `FUN_00512a10`, not these callbacks.
    Original note: ~~**Movie/intro-video subsystem's two custom C++ vtables** (`PTR_LAB_0054d078` — the DirectDraw-wrapper class constructed by `FUN_00512930`/`FUN_00512d10`, and `PTR_LAB_0054d0a0` — the video-stream class constructed by `FUN_005431e0`/`FUN_00542fc0`): every vtable slot these point at (`LAB_0054dxxx` targets) has no decompiled body anywhere in `Blaze.c` — same class of gap as ordinal 790's `&LAB_00512ef0` callback, just a whole class's worth at once. `TrySetupMovieStream`/`OpenMovieStream`/`RewindMovieFallbackFrame` in `Blaze.cs` stop exactly at these vtable calls; see the "Movie / intro-video subsystem" banner comment in `Blaze.cs` for the full writeup of what *is* independently confirmed (real DirectDraw vtable offsets matching the public `IDirectDraw`/`IDirectDrawSurface` v1 layout, real Win32 window/message-pump/joystick-capture behavior).~~
14. **RESOLVED.** `0x00542600` was never *called*, only stored into `WNDCLASSA.lpfnWndProc`, so Ghidra labelled it and left the bytes undisassembled. Forcing disassembly (`G` → address → `D` → `F`) produced a clean decompilation immediately — see `Legacy/Raw/Blaze/FUN_00542600.txt`. It is an ordinary window procedure, and its message set (WM_KEYDOWN/Esc, the three mouse buttons, both joystick buttons, WM_TIMER, WM_ACTIVATEAPP, WM_CLOSE, MM_MCINOTIFY) is exactly the set the attract screen waits on. All four of its handlers — `FUN_00542970`, `FUN_005429e0`, `FUN_00542a00`, `FUN_00542560` — already had bodies in `Blaze.c`. `CreateMoviePlayerWindow` now registers the class and creates the window for real; the full `FUN_00542740` listing supplied the cursor (wsyskern resource 10), `WS_EX_TOPMOST`/`WS_POPUP`, and the `ShowWindow`/`MoveWindow` tail. Original note: ~~**Movie window procedure** (`Blaze.c`: `&LAB_00542600`, the `WNDPROC` for window class `"VfwClass"`): no decompiled body. `CreateMoviePlayerWindow` in `Blaze.cs` therefore does not actually call `RegisterClassA`/`CreateWindowExA` (would crash on the first dispatched message with no real `WndProc`); `HWnd` stays `IntPtr.Zero`. This is also *why* `DAT_005db85c` (TODO #6 above) still can't be threaded through — the movie window isn't the main window anyway (it's a separate 2000x2000 hidden helper window), so the real main-window creation is presumably elsewhere, still unlocated.~~
    *Still true and worth keeping*: this is the **movie player's** window, not the main game window. `DAT_005db85c` (#6) remains unlocated.
15. **RESOLVED AND WIRED.** `s_MainLoopTickCallback` now points at `RunGameStateTick`, the reconstruction of `FUN_00537ca0`, so `RunGameLoop` no longer throws on its indirect call. The tick's prologue is real (`Zoom8BitTextures = 0` via the winsys3d data export, then `ReInitializeAllTextures()`), and its eleven state handlers are named stubs — **all eleven have decompiled bodies in `Blaze.c`**, so they are ordinary uncovered code rather than blocked work, and each stub names its Ghidra function so the next state to reconstruct is obvious from the exception text. The `default:` case reproduces the original's deliberate store to address 0 as an exception rather than by faulting. *Not yet reachable at runtime*: `LoadFirstLevelResources` throws at `DecodeResourceChunk` (#19) before `RunGameLoop` is entered. Original note: ~~**`PTR_FUN_0056c96c`** — **The pointer holds `0x00537CA0`, and `FUN_00537ca0` is already decompiled in `Blaze.c` at line 83364** — so the game loop's tick needs no Ghidra re-run at all. It is the **per-game-state dispatcher**: it clears `Zoom8BitTextures`, calls `ReInitializeAllTextures()`, then switches on `DAT_005de510` across at least 7 states (`FUN_00542130`, `thunk_FUN_00536180`, `FUN_00542160`+`FUN_00537da0`, `thunk_FUN_00516020`, `FUN_005066d0`, `FUN_00504c90`, `FUN_00524180`). Note this makes `FUN_0052a440` — long assumed to be the state dispatcher — not the one the tick actually reaches. The adjacent pointer at `0x0056c970` holds `0x00537d50`, which is **not** in `Blaze.c` and would need a scoped re-run if it matters. Wiring the tick into `RunGameLoop` is a separate change; `RunGameLoop` still throws until then. Historical reasoning that led here, kept because it was sound: `0x0056c96c` occurs exactly once in all 98k lines of `Blaze.c`: at the call site itself. It is therefore not a runtime-assigned variable at all but a **statically initialised `.data` function pointer** — the linker wrote the target address into the image, and Ghidra's `PTR_<KIND>_<address-of-pointer>` naming (compare the confirmed `PTR_s_Blaze___Blade_00563208`, `PTR_PTR_00566d68`, `PTR_FUN_0056b4d0`) tells us the slot holds an unnamed function pointer but not *which* function. Its neighbours (`s_FEMALE_0056c95c`, `DAT_0056c964`, `DAT_0056c978`) confirm it sits in initialised data. **No amount of further reading of `Blaze.c` can recover it** — see the "Systemic blocker" section below. Guessing a plausible target would violate Strategy.md's "ne jamais inventer une signature/un comportement", so `RunGameLoop` throws rather than silently skipping the tick.~~
16. **`DAT_005633a8`** (mode flag read pervasively, including by `FUN_00529eb0`/`InitViewportSurfaces`): no assignment site reached yet either; always 0 (its default) in the current reconstruction.
17. **Font-loading filename** (`Blaze.c`: `s_DATA_Font_bmp_005684cc`) and the three intro-movie filenames (`s_TEPS_AVI_005684f4`/`s_OPENING_AVI_005684e8`/`s_ANI01_AVI_005684dc`): only Ghidra's auto-derived symbol names survive, not the literal bytes — `"DATA\FONT.BMP"`/`"TEPS.AVI"`/`"OPENING.AVI"`/`"ANI01.AVI"` in `Blaze.cs` are best guesses from the symbol names, same caveat as TODO #4's message strings.
18. **`FUN_00529b00`/`LoadWholeFileToBuffer`'s incomplete Ghidra analysis**: the failure path returns `unaff_ESI` (a register Ghidra never attributed a value to at function entry — genuinely indeterminate, not just unnamed) and the read call shows a literal `2` (`Ordinal_334(iVar1,2,iVar2)`) exactly where a destination-buffer argument should be, one parameter short of the two arguments both real call sites actually pass. Reconstructed using the evident 2-argument intent (`dest` as that argument, `0` as an explicit safe failure return) — flagged rather than presented as certain.
19. **RESOLVED — and the "genuinely unrecoverable" claim below was wrong.** Reconstructed in full from the raw disassembly (`Legacy/Raw/Blaze/FUN_005322f0.txt`, exported from Ghidra). The function uploads a **TIM texture** to VRAM and records its texture-page / CLUT pair in a slot: `Ordinal_677`/`Ordinal_662` are libgpu's `OpenTIM`/`ReadTIM`, and the 20 bytes the prologue reserves (`SUB ESP,0x14`, handed over as `LEA ECX,[ESP+8]`) are a `TIM_IMAGE` — which is what the "indeterminate" `puStack_14`/`psStack_10`/`iStack_c` locals really were.
    **What the decompiler got wrong, beyond naming**: `unaff_retaddr` is parameter 2 (the 2-short slot, corroborated by both call sites' 4-byte stride); every `param_1[0..2]` byte read actually comes from `EDI` = parameter 3, the metadata triple, *not* the chunk pointer; and `(*param_1 & 0xff10)` is an artifact of a `AND AL,0x10` byte mask — using it would corrupt the pixel rect's y coordinate. The `extraout_var*` values were just `Ordinal_674`'s ignored return.
    **Lesson**: "Ghidra's own analysis is incomplete" was a correct observation turned into a wrong conclusion. Incomplete decompilation meant *read the disassembly*, not *give up*. Three of this file's remaining blockers (#13/#14/#23) are the same shape and should be treated the same way. Original note: ~~**`FUN_005322f0`/`DecodeResourceChunk` — genuinely unrecoverable, not just unread**: several of its inputs are `unaff_ESI`/`unaff_DI`/`unaff_retaddr` and outputs `extraout_var`/`extraout_var_00`/`extraout_var_01` in Ghidra's own decompilation — register-allocated values Ghidra could never tie to a defining instruction or a real variable. This is a stronger category of gap than the usual "not yet read": Ghidra's *own* static analysis of this function is incomplete, so no amount of re-reading `Blaze.c` will resolve it without disassembling the raw instructions. **Current blocking point** for `LoadFirstLevelResources` (thrown on the first directory entry, if the loaded "SYSTEM.INI"-equivalent file lists any).~~
20. **RESOLVED — schema recovered *and* its reading confirmed.** `DAT_0056aca8` holds 37 `ushort`s ending at a `0x0000` terminator. Each entry is **`(low byte = index, high byte = stride)`**: the low byte selects `header[4 + index]` (a pointer the fixed pass already relocated) and the high byte is the byte stride for walking the chained list it heads. This is settled by `FUN_00532070`'s own `& 0xff` / `>> 8`, not inferred from the byte pattern — an earlier `(kind, size)` guess was wrong. The schema-driven pass is now reconstructed and is no longer a no-op. **Watch the two relocation formulas**: the fixed pass adds `header[0]`, the schema pass adds `header[0] + header[1]`; using the shorter one yields plausible-looking pointers that are off by a constant. Full table and analysis in `Documentation/Reference/DataSymbols.md`. Original note: ~~**`DAT_0056aca8`** (resource-fixup schema table read by `FixupResourceHeader`/`FUN_00532070`): data-only global, byte content not present in `Blaze.c`'s text (same category as `DAT_00562e00`/TODO #12) — `s_ResourceFixupSchema_0056aca8` is an empty array, so the schema-driven fixup pass is a safe no-op rather than a guess.~~
21. **`DAT_006008c8`/`DAT_00604930`/`DAT_00600904`** (the 3 comparison targets in `IsKnownOverrideDestination`/`FUN_0052a660`): none have an assignment site anywhere in the reconstructed call graph yet, so they're always null/zero; the 2 true-branch bodies (native override-table installers, one pulling in 11 further `FUN_` calls) are stubbed rather than translated.
22. **`PTR_s_BLAZE_ALL_1_00566bbc`/`PTR_s_SYSTEM_INI_1_00566bb8`/`s____snd_Base_snd_00568500`** filenames used by the resource-loader subsystem: same category as TODO #17 — only Ghidra's derived symbol names survive; `"BLAZE.ALL"`/`"SYSTEM.INI"`/`"\SND\BASE.SND"` in `Blaze.cs` are best guesses.
23. **RESOLVED, 2026-08-24 — reconstructed as `Audio.RunMusicStreamWorker`, and `CreateThread` is called for real.** The body had already been recovered to `Legacy/Raw/Blaze/FUN_00513660.c`; the transcription was checked against **both** that decompilation and a fresh disassembly (`Legacy/Raw/Blaze/LAB_00513660.asm`), and the two disagree on one point that matters. Ghidra models the two stack locals as arrays (`local_8`, `local_4`), so it passes *addresses* where the machine code passes *values*: its `puVar3` is handed to the decoder as the destination and to `Unlock` as `lpvAudioPtr1`, with a byte count of `uVar4 = 0`. In the disassembly the destination is `mov esi,[esp+14h]` — a **load** of the locked pointer — and `Unlock` gets the real byte count from the other local. The reconstruction follows the disassembly. This also settles the layout of `DAT_005db838`: **nine** contiguous handles, the eight notification events plus the stop event at `DAT_005db858`, which `0x005db838 + 9*4 == 0x005db85c` corroborates to the byte against the window handle that follows. See LB-011 for the wraparound defect the body carries. Original note: ~~**Body recovered — reconstructible whenever wanted.** … *Note*: reconstructing it would have no observable effect yet, because `StartMusicStream` early-outs without an `IDirectSound` device (#26).~~ — both halves are now done.
24. **`DAT_0056ac60` (16 shorts) / `DAT_0056ac80` and `DAT_0056ac90` (7 shorts each)** — the MS-ADPCM adaptation table and the two coefficient tables. Data-only globals, so the bytes aren't in `Blaze.c` (same systemic blocker as #12/#20), but unlike those two the *identity* here is not a guess: `FUN_00531f70` only accepts a `fmt ` chunk with `wFormatTag == 2` (WAVE_FORMAT_ADPCM), `wBitsPerSample == 4` and `wNumCoef == 7`, then compares `aCoef[0..6]` against `DAT_0056ac80`/`DAT_0056ac90` — i.e. it verifies the file uses the *standard* coefficient set so the decoder's hardcoded tables apply. The addresses corroborate the sizes exactly (16 shorts at `0x0056ac60` end at `0x0056ac80`; 7 shorts at `0x0056ac80` end at `0x0056ac8e`, padded to `0x0056ac90`). `Blaze.cs` uses the documented standard MS-ADPCM values. Still worth confirming byte-for-byte if the binary appears.
25. **`FUN_00513030`'s register-attribution bug**: Ghidra writes `piVar3` (loaded from `DAT_005db8e4`, the *device*) into the slot table and releases that same pointer when the table is full, while the newly created buffer sits unused in `CreateSoundBuffer`'s `local_2c` output parameter. Storing the device in a buffer slot would make `ReleaseAllSoundBuffers` call `IDirectSoundBuffer` methods on an `IDirectSound` and release the device up to 50 times, so the decompiled text cannot be what the binary does. `AllocateSoundBuffer` transcribes the evident intent (the *created buffer* is stored and released) — flagged rather than presented as certain.
26. **RESOLVED, 2026-08-24 — there is exactly one creation site, and the search that concluded otherwise was looking in the wrong text.** `DirectSoundCreate(0, &DAT_005db8e4, 0)` is called at `0x00512f20`, inside `LAB_00512ef0` (#8) — a body `Blaze.c` does not contain, which is precisely why grepping `Blaze.c` found nothing. **Method worth reusing**: scan `.text` for `call rel32` whose target is the import thunk (`0x00545c68`) and for `call [IAT slot]` (`0x006084b8`). One pass over 1.35 MB, one hit, and the hit is inside a `LAB_` — a decompiler gap and a "no call site exists" conclusion look identical from `Blaze.c` alone. The audio subsystem is no longer inert: measured `S_OK` on all four init calls, `DSBSTATUS_PLAYING|LOOPING` on the music buffer, and non-zero sample offsets on the UI effects. Original note: ~~**`DAT_005db8e4` (the `IDirectSound` device) has no creation site**: read in four places, assigned nowhere in all of `Blaze.c`'s reconstructed graph, and no `DirectSoundCreate` call site has been located. … Until it is found, the whole audio subsystem is correct but inert: every entry point takes its "no device" early-out.~~
27. **`DAT_005631d4` initial value** (music enabled/disabled at startup): **recovered from the binary as `1`** — music ships enabled. The note below assumed the value unrecoverable and defaulted `s_MusicEnabled` to 0, which silently made `StartMusicStream` early-out forever; that was corrected when the binary was committed. Original note: ~~data-section content, so unavailable; `s_MusicEnabled` defaults to 0, meaning music starts disabled and `StartMusicStream` early-outs until `ToggleMusicEnabled` flips it.~~ Note `DAT_005631d0` (the "no stream running" flag) needed no such assumption — a first `StartMusicStream` always routes through `StopMusicStream`, which sets the flag before it is tested.
28. **`FUN_0053da20`/`FUN_0053db20` (positional sound gain)** — the remaining members of the sound-effect family, deliberately left untranslated. `FUN_0053da20` computes a left/right gain pair from a world position via the cos table (ordinal 450) and `FUN_0053db20` feeds the result to `PlaySoundEffect`, but Ghidra declares `FUN_0053da20` with **2** parameters while `FUN_0053db20` calls it with **3**, and its body reads `unaff_SI` and `unaff_retaddr` — register-passed values never tied to a parameter. Reconstructing it would mean inventing a signature, which Strategy.md forbids; it needs a Ghidra re-run with the calling convention corrected (same class as the `__fastcall`/`__thiscall` cases noted under the systemic blocker).
29. **Sound-directory second half (`DAT_005e8520`, entries `0x3c`..`0x4c`)** is loaded by `FUN_00512xxx` (`Blaze.c:63354`), which is not reconstructed — so those 17 entries stay zero and sound ids `0x3c`..`0x4c` (which include the four fixed UI sounds `PlayUiSound38`..`3B`) currently resolve to a zero-length sample and play silence. Not a data-availability problem: it is ordinary uncovered code, and the same call site also fills the level sample bank `DAT_00600900`.
30. **RESOLVED — table recovered *in full*, 2026-08-24.** 40 entries, `0x00566cc8`..`0x00566d67`, listed one by one in `Documentation/Reference/DataSymbols.md`. CD-**root** absolute templates (`@:/Music/Mel00_01.WAV`), drive-letter patched at index 0. The end of the table is fixed by what follows it at `0x00566d68` — a different family, 0x40 spacing. **Entry `0x06` is `Title01.WAV`, the menu theme**, and its absence was the second reason the menu was silent: only `0`, `1` and `7` had been sampled, and `RunMainMenuFrameLoop` asks for `6`, so `PlayMusicTrack` returned on a null slot every frame. Entries `0x22`..`0x24` are **not** literals — all three point at `&DAT_00572b40`, eight uninitialised bytes filled at run time by `FUN_00512900` from the not-yet-reconstructed `0x00505xxx` screen code; they stay null, and `MainMenu.cs` says why on the spot. Original note: ~~Only the sampled entries are transcribed so far … **Nothing is audible yet**: no `DirectSoundCreate` site exists (#26), so `StartMusicStream` takes its no-device early-out.~~

36. **`FUN_00545340` — the menu/UI engine, deliberately deferred.** 1742 lines, three parameters, 25 calls, reached twice per menu frame alongside `FUN_005443a0` (233 lines). It is ordinary uncovered code with a full decompiled body, not blocked work, but it is a project in its own right and reconstructing it blind — with no way to observe whether the surrounding frame order is right — would have been the wrong order of operations. Stubbed along with `FUN_005443a0`, `FUN_00512320` (the options page) and `FUN_005048f0` (joystick state processing, which pulls in `FUN_005231f0`/`FUN_00523660`).

31. **Dropped return values silently delete control-flow edges — a startup hang, now fixed.** `FUN_00542f30` is declared `void` by Ghidra (`Blaze.c:90063`) even though it tail-calls `FUN_00542c40`, which Ghidra itself types `undefined4 __fastcall`, and both call sites in `FUN_00529c10` read the result (`Blaze.c:74442`, `74450`). The reconstruction had followed the `void` declaration, which removed **two of the attract loop's four exit conditions**; combined with the other two being dead (`PlayMovie` always reports 0 while `TrySetupMovieStream` is a stub), `RunMainLoop` spun forever at 100% CPU and startup never reached engine init. Fixed by trusting the call site over the declaration and reconstructing `FUN_00542c40`'s bitmap-load prologue, whose `return 1` on a missing `TITLE640.BMP` is the original's own way out of the loop when there is no CD.
    **Rule this establishes**: when Ghidra's declaration and a call site disagree about a return value or a parameter, the *call site* wins. A stub must still return a value if its result is one of a loop's exit conditions.
    **The same defect is still latent** in `PlayMoviePumpLoop` (`FUN_00542b70`): its `do { … } while (*param_1 != 0)` terminates only when the movie-stream vtable callback clears the playing flag (TODO #13), and is currently unreachable *only* because `TrySetupMovieStream` returns false. Enabling that stub without a completion signal re-hangs startup in a tight `PeekMessage` spin. A warning comment now sits at that loop in `Blaze.cs`.

32. **`FUN_00529eb0`/`InitViewportSurfaces` was writing through a null pointer — fixed, and the ordinals are now identified.** The reconstruction passed `null` to `Ordinal_661` (under a `TODO(REVERSE): exact 3rd struct target not modeled`), passed stack scratch buffers instead of the four global structs, and had the argument order rotated on all four calls — a native `0xC0000005` that killed the process the moment the attract-loop fix made this function reachable. Root cause: Ghidra spills these calls' arguments into unattributed stack slots (`uStack_18`..`puStack_50`) instead of rendering them as arguments, and gives the fourth call only three of its five. Reading the slots back in ascending-address (cdecl push) order recovers all four exactly.
    **Identification, now certain**: winsys3d.dll ordinals 658/661/665/674 are a port of the PlayStation SDK's **libgpu** — fitting, since the sibling DLL is literally named `playst.dll`. Ordinal 658 is `SetDefDispEnv(DISPENV*, x, y, w, h)` and 661 is `SetDefDrawEnv(DRAWENV*, x, y, w, h)`; their bodies write the documented DISPENV/DRAWENV field layouts field for field. The four globals `Blaze.c` passes them (`DAT_005e9880`, `DAT_005e98dc`, `DAT_005e98f0`, `DAT_005e994c`) are spaced **92 / 20 / 92 / 20 bytes** apart — exactly `sizeof(DRAWENV)` / `sizeof(DISPENV)` — and all 12 of the follow-up global stores land on valid fields of those layouts. Three independent confirmations, no guessing. The setup is standard PSX double buffering at 320x240 (`0x140`x`0xf0`): display one buffer at y=0 while drawing the other at y=240, and swap.
    Ordinal 674 is `LoadImage(RECT*, data)` (its `rect.y > 0x1ff` early-out confirms 1024x512 VRAM). Ordinal 665 is `ClearImage`-shaped but its colour math decompiles as `((p4 & 0xff) << (p3 + 10)) << (p2 + 5)`, which does not match libgpu's plain `(r,g,b)` — deliberately left un-renamed until that is explained.
    *Still open in this function*: the `DAT_005633a8 != 0` branch calls `Ordinal_674` with a RECT (`uStack_58`) whose `x` advances by 0x40 per iteration and wraps at 0x400; the reconstruction's `tileRect` is never initialized and its `frameOffset` is tracked separately from the rect it belongs to. That branch is currently unreachable (`GetMainMemPtr()` returns null, TODO #33), so it is flagged rather than fixed.

33. **`GetMainMemPtr()` returns null** — winsys3d.dll's `MainMem` export is not yet resolved, so the entire second half of `InitViewportSurfaces` (the framebuffer checkerboard fill and the 0x20-iteration `LoadImage` tile upload loop) is skipped by an early return, and any other code reading main video memory will do the same. Now that startup runs end to end this is the most visible remaining gap below the game loop itself.

34. **RESOLVED — properly this time, and the deviation is gone.** With `FUN_00542600` disassembled (`Legacy/Raw/Blaze/FUN_00542600.txt`) the movie player's window procedure is reconstructed, so `CreateMoviePlayerWindow` now performs the real `RegisterClassA`/`CreateWindowExA` and the attract screen can receive input. The piece that had been missing all along is `FUN_00542a00`: on WM_KEYDOWN(Esc), any mouse button, or either joystick button it sets `*player = 0` (Playing) and `player[0xb] = 1` (Result) — which is precisely what lets `PlayMoviePumpLoop` return and `PlayMovie` report non-zero, giving the attract loop its exits. `ShowTitleScreenAndWait`'s 20-second `timeGetTime` wait and message pump are reconstructed too. `OpenMovieStream` is back to its faithful `return true`.
    Measured: the attract sequence runs for real and terminates, and startup proceeds into the game loop. *Not yet determined*: which of the exits fires in practice — a joystick event (`joySetCapture` now succeeds against a real window) or the 20 s timeout. Worth instrumenting if the intro timing ever matters.
    Superseded note: ~~**Startup unblocked, but by a deliberate deviation — and the earlier justification here was wrong.** `OpenMovieStream` now reports failure, which makes `RunMainLoop` skip the intro movies and the attract loop, so startup proceeds. An earlier revision of this entry claimed that *was* the original's own no-DirectDraw path. **It is not.** `FUN_00512930`'s disassembly (`Legacy/Raw/Blaze/FUN_00512930.txt`) ends `MOV EAX,ESI; RET 0x4` — it returns `this` unconditionally, even when its internal `Malloc` fails — so `FUN_00542800`'s `if (iVar1 == 0) return 0` only fires when the 0x48-byte allocation fails. In the original, the open succeeds.~~
    So this is a **known deviation**, kept because it is the only workable behaviour while the movie window cannot be created. It disappears once #13/#14 are disassembled. The diagnostic throw in `ShowTitleScreenAndWait` is retained as a backstop for exactly that moment: if the open is made to succeed without the window and input handling, the attract loop again has no exit and must say so rather than spin. Original note: ~~**The attract loop has no reachable exit once the CD is present — now a loud throw.** With the data root corrected, `ShowTitleScreenAndWait` loads `DATA/TITLE640.BMP` successfully and returns 0, and `PlayMovie` is pinned at 0 while `TrySetupMovieStream` is stubbed (#13). All four of the loop's exits therefore depend on unreconstructed subsystems, and the process spins at 100% CPU with no message pump — the same silent-misbehaviour shape as #31, arriving from the opposite direction. `ShowTitleScreenAndWait` now throws a descriptive `NotImplementedException` at the point where the original's 20-second `timeGetTime` wait and its key/mouse/joystick handling would be, rather than returning 0. The degradation is targeted: with no CD the bitmap load still fails, the function returns 1, the loop exits on iteration 1 and startup proceeds. **To clear this**, the real fix is the main window + `WNDPROC` (#6/#14) so input can actually arrive; reconstructing the 20 s timeout alone would produce a faithfully-infinite loop, which is no better.~~
    The guard itself is kept in `ShowTitleScreenAndWait` as a backstop: if a future change makes `OpenMovieStream` succeed again without also supplying the window and input handling, the attract loop would once more have no exit, and it should say so rather than spin.

35. **RESOLVED** — the schema-driven relocation pass is reconstructed (TODO #20), `LoadResourceEntry` no longer faults, and the directory walk now reaches `DecodeResourceChunk`. Original diagnosis kept below; it was correct. ~~**`LoadResourceEntry` faults on unrelocated directory pointers against real game data.** Measured with the CD mounted and the attract loop temporarily bypassed: `LoadFirstLevelResources` loads `SYSTEM.INI;1`, enters its directory-entry loop, and dereferences `entryPtrValue` — a raw value read straight out of the file — as a pointer, giving a `NullReferenceException`/AV in `LoadResourceEntry`. Cause is upstream and already documented: `FixupResourceHeader`'s schema-driven relocation pass is a no-op because `s_ResourceFixupSchema_0056aca8` is an empty array, so the file's internal offsets are never turned into real pointers. **This is now fixable** — `DAT_0056aca8`'s bytes were recovered and are recorded in `Documentation/Reference/DataSymbols.md` (#20); the `(kind, size)` reading of them is still a hypothesis and needs confirming against `FUN_00532070`'s use of the table. Until then `DecodeResourceChunk` (#19) cannot be reached, so it stays untested.~~

## ~~Systemic blocker~~ — RESOLVED: `Blaze.exe` is now in the repo

**This section previously said a whole class of work was impossible. It was wrong, and it
was the most misleading thing in this file** — it told every future session not to try.

`Blaze.exe` is now committed at **`Legacy/Binaries/Blaze.exe`** (1,589,760 bytes, MD5
`46d90d1b4526f4a9a441f851c77179b3`), registered as a solution item under the
existing `Legacy` solution folder. It is provably the same build `Blaze.c` came
from: image base `0x400000` + entry RVA `0x147D40` = `0x00547D40`, exactly
`entry`'s address.

The reasoning that led here still holds and is worth keeping: `Blaze.c` is a
**code-only decompilation** — Ghidra emitted function bodies, not `.data`
/`.rdata`. Any symbol whose value lives in initialised data is unrecoverable by
re-reading `Blaze.c`, no matter how carefully. The fix was never more analysis;
it was getting the bytes.

Read them with:

```
OpenForesia.Tools sections Legacy/Binaries/Blaze.exe
OpenForesia.Tools at Legacy/Binaries/Blaze.exe 0x005631f8 [bytes]
```

All recovered values are recorded in **`Documentation/Reference/DataSymbols.md`**, keyed by
Ghidra VA. Every TODO below that was blocked on data is now resolved there:

| TODO | Symbol(s) | Outcome |
|---|---|---|
| #15 | `PTR_FUN_0056c96c` | **`0x00537CA0`** — and `FUN_00537ca0` is already in `Blaze.c` (line 83364). It is the per-game-state dispatcher. No Ghidra re-run needed. |
| #2 | `DAT_00563280` | `"@:\cd.idx"` — and `cd.idx` exists at the CD root |
| #3 | `PTR_s_Blaze___Blade_00563208` | all 5 strings recovered; slot meanings match `winsys3d.md` exactly |
| #4 | message strings | `"Please insert Blaze & Blade CD!"`, `"Erreur"` |
| #11 | ini keys/extension | extension is **`.cfg`, not `.ini`** — the reconstruction has never found its options file |
| #12 | `DAT_00562e00` | `1, 7, 11, 16, 23` — real clamps; the current `int.MaxValue` no-op is wrong |
| #17, #22 | filenames | recovered exactly, incl. forward slashes (`DATA/Font.bmp`) and ISO-9660 `;1` suffixes |
| #20 | `DAT_0056aca8` | bytes recovered; `(kind, size)` reading still a hypothesis |
| #21 | `DAT_006008c8`/`DAT_00604930`/`DAT_00600904` | **uninitialised `.data` tail** — zero at load, written only by code. No data recovery can help; confirms they have no assignment site yet. |
| #24 | MS-ADPCM tables | standard values **confirmed byte-for-byte** — the earlier assumption was right |
| #27 | `DAT_005631d4` | **`1`** — music is *enabled* at startup; the reconstruction defaults it to 0 |
| #30 | music filename table | `"@:/Music/Mel00_01.WAV"` etc., drive-patched absolute paths |
| — | `DAT_005631f8` | `"@:\FRENCH\"` — the data root, not the drive root. Cause of a live path bug; see `fix-cd-data-paths`. |

Applying these values to `Blaze.cs` is deliberately **not** part of that recovery
work — each one gets reviewed against its call site in its own change.

Two further blockers were never about the data, and are **still open**. They need
**re-running Ghidra** on the binary — which is now possible, since the binary is here:

- **#19 `FUN_005322f0`** — Ghidra's own analysis is incomplete
  (`unaff_ESI`/`unaff_DI`/`unaff_retaddr` inputs, `extraout_var*` outputs).
- **#13/#14/#23** — the movie subsystem's two C++ vtables, the `"VfwClass"`
  window procedure and the music-streaming worker thread (`LAB_0054dxxx`,
  `LAB_00542600`, `LAB_00513660`): address-taken only, so Ghidra never produced
  bodies for them. A re-run with those addresses forced to "disassemble as
  function" would produce them. `LAB_00513660` is the cheapest of the three and
  the highest-value: it is a single self-contained thread procedure whose
  inputs (`DAT_005db860`, `DAT_005db750`, `DAT_005db89c`, the 8 notification
  events, the stop event) are all already identified.

**This is not only a fidelity concern — it has already produced a shipped bug.**
TODO #31 records the first confirmed case: following Ghidra's `void`
declaration for `FUN_00542f30` over its call sites deleted two of an
attract loop's four exit conditions and hung startup outright. Treat every
`void`-declared function whose result a call site reads, and every function
called with more arguments than Ghidra declares, as a defect waiting to
happen rather than a cosmetic annotation. The call site is authoritative.

A recurring, *separate* quality issue also worth noting: Ghidra's **parameter
recovery is unreliable throughout this binary** — `FUN_00529b00`,
`FUN_005322f0`, `FUN_0052a1e0`, `FUN_0052a280` and others are all decompiled
with fewer parameters than their real call sites pass (register-passed
arguments Ghidra didn't attribute). Each such case is flagged inline in
`Blaze.cs`; re-running Ghidra with corrected calling conventions
(`__fastcall`/`__thiscall`) on these would improve fidelity considerably.

## Reality check — how far is this from "the game runs"?

Deliberately blunt, because the gap is large and easy to under-estimate from
the call-graph section above:

- `Blaze.c` defines **1426** `FUN_*` functions. **~67** are reconstructed
  (~5%). The reconstructed set is the *startup spine* (CRT bring-up → WinMain →
  CD check → options → main loop → resource load → shutdown) plus the complete
  audio stack (DirectSound wrappers, MS-ADPCM codec, sound-effect resamplers),
  which is the right skeleton to have first, but it is still a thin slice.
- **Nothing renders yet.** No reconstructed code creates a main window
  (`DAT_005db85c` is still unlocated, TODO #6), creates a Direct3D/DirectDraw
  device beyond `Setup3DDevice`'s ini parsing, or draws a frame.
- ~~**Audio is complete but inert.**~~ **Le son marche depuis le 2026-08-24.** Le
  périphérique DirectSound naît dans `LAB_00512ef0` (TODO #8/#26), le thread de
  flux est transcrit (#23), et la table des morceaux est complète (#30). Mesuré :
  `Title01.WAV` au menu, `Menu.WAV` à la constitution du groupe, `Mel00_01.WAV` à
  la carte du monde, chacun `DSBSTATUS_PLAYING|LOOPING` ; les bruitages d'interface
  `0x38`/`0x3a` lisent des échantillons réels de la banque `Snd\`. Ce qui reste
  muet : les identifiants `0x3c`..`0x4c`, jusqu'à ce que le code de chargement de
  niveau remplisse la seconde moitié du répertoire (TODO #29) — et c'est aussi le
  défaut d'aliasing de `s_SoundBankHeader_005e8520`.
- **The game loop cannot iterate** — its tick pointer is TODO #15 above.
- The whole of gameplay (`FUN_0052a440` and everything below it: rendering
  pipeline, entity/AI, menus, battle, save/load) is untouched.

Realistically the startup path alone still needs the window/device creation
chain plus TODO #15 before anything can be *observed* running; a playable
reconstruction is a project measured in many hundreds of functions beyond
that. The current build compiles cleanly and every gap throws a descriptive
`NotImplementedException` rather than silently misbehaving, which is the
right state to keep extending from — but it does not execute as a game today.

## Next area to explore — current blocking point

`RunMainLoop` (Ghidra: `FUN_00529c10`) is fully reconstructed and runs its own
logic — intro movies (gracefully degraded, see TODO #13), engine/device
re-init, UI font load, viewport setup, `LoadFirstLevelResources` — all the
way through to handing off to the real per-frame game loop, which itself now
runs its per-state resource lookup before hitting its own blocking point.

**The CD is present and detected — earlier notes here saying otherwise were wrong.**
The retail disc mounts at `E:`, the drive scan finds it, and the failures
previously attributed to "no game CD" were a path bug: the data root is
`"@:\FRENCH\"` (Ghidra `DAT_005631f8`), not the bare drive root. Everything
CD-relative resolved one directory too high and found nothing. Fixed in
`fix-cd-data-paths`; the disc's real layout is `E:\French\` for `BLAZE.ALL`,
`SYSTEM.INI`, `DATA/`, and the movies, with `E:\Snd\` and `E:\Music\` at the
root reached by their own `@:`-prefixed absolute templates.

**THE RECONSTRUCTION DRAWS.** The attract screen displays the CD's `TITLE640.BMP` —
confirmed on screen, and measured as `StretchDIBits` returning 480 scanlines. The
path is complete end to end: CD detection → `E:\FRENCH\DATA/TITLE640.BMP` →
`LoadBitmapFile` → `FUN_00512a10` creating the DirectDraw primary surface into
`wrapper[1]` → `GetDC`/`StretchDIBits`/`ReleaseDC`.

**Three environmental deviations, all measured rather than assumed.** None is a
reconstruction shortcut; all three are the modern platform refusing what the 1999
binary asks for:

| Original | Modern Windows | Handling |
|---|---|---|
| `SetDisplayMode(640, 480, 16)` | returns `E_NOTIMPL` (0x80004001) — 16bpp mode changes are gone | the faithful call is attempted first; a 32bpp retry fires **only** on that exact HRESULT, so it would never trigger on period hardware |
| surfaces stay valid | `GetDC` fails with `DDERR_SURFACELOST` (0x887601c2) between repaints | `Restore` (vtable 0x6c) then one retry — the same pairing the original uses elsewhere in this cluster |
| `winsys3d.dll` maps at its preferred base `0x00A00000`, putting the `MainMem` display-list arena at `0x00B6DBD8` | ASLR relocates it out of low memory (measured at `0x09…`), because `OpenForesia.exe`'s own image base sits inside winsys3d's preferred range and the CLR reserves the rest | the arena is allocated in low memory and its address written into the `MainMem` pointer winsys3d exports — see below |

### A fourth deviation — deliberate, not environmental (`s_WindowedStartup`)

Added 2026-08-12 by the change `render-title-screen-in-window`. The three above are **subtractions
the platform forces**; this one is **a behaviour change the project asks for**, and it is the only
one of its kind so far. It must not be mistaken for the others.

**What it changes.** The startup sequence — title screen and attract — no longer takes the display.
`SetupDirectDrawSurfaces` requests `DDSCL_NORMAL` instead of
`DDSCL_FULLSCREEN | DDSCL_EXCLUSIVE`, skips `SetDisplayMode(640, 480)` and skips the
primary/flip surface chain; the four sites that show or maximise the `VfwClass` window stop doing
so. The title screen is drawn into a DIB section owned by the backend (`StartupSurface`) and
presented into the game window instead.

**Why it is safe to drop all of that.** The whole DirectDraw apparatus had exactly one consumed
product: the `HDC` that `BlitTitleScreen` obtained by `GetDC` on the primary surface. The back
buffer fetched by `GetAttachedSurface` was never stored, no `Flip` was ever called on the chain,
and the wrapper fields filled afterwards are read only by the DirectShow video path, left null
(TODO #13). The DirectDraw object itself and its release sequence are untouched.

**Measured effect.** Polling the desktop mode from a separate process every 150 ms across a full
startup: on the original path, 59 samples at 640x480 and 3 at 1440x900 — roughly nine seconds of
switched desktop. On the new path, 256 samples out of 256 at the native mode. The `VfwClass`
window is no longer visible, `Ordinal_789` still initialises `3dhelp` without `ExitProcess(3)`, and
the menu is still reached.

**Reversal.** `OPENFORESIA_LEGACY_STARTUP=1` restores the original path in full, on the model of
`OPENFORESIA_LEGACY_3DHELP`. Verified: the legacy run reproduces the mode changes above and still
reaches the menu.

**What it does not fix.** `3dhelp` still switches the desktop mode for 200–600 ms inside
`Ordinal_789`, between the title screen and the menu. Three independent attempts to suppress that —
swapping the vtable of `ddraw`'s `IDirectDraw` objects, and two ways of neutering
`ChangeDisplaySettingsExA` in `ddraw`'s import table — each removed the mode change and each left
`3dhelp` unable to finish initialising. Two durable findings came out of it: **`ddraw` does not
tolerate having its objects' vtable pointer replaced** (unlike `3dhelp`'s, which `DeviceShim` swaps
freely), and **`Create3DDevice` requires a real exclusive-mode switch**. The remaining route is a
synthetic device — `winsys3d` loads `3dhelp` dynamically, so `GetProcAddress` can be intercepted —
which amounts to porting the vtable slots rather than patching one. Full plan in
`Documentation/DLL/3dhelp.md` and in the change's `tasks.md`, group 9.

### The display-list arena (`EnsureDisplayListArena`)

**The whole display list is addressed on 24 bits.** `Ordinal_688` (ClearOTagR) stores
`(uint)entry & 0xffffff` in every link and terminates with `0xffffff`; `Ordinal_701`
(DrawOTag) follows `*p & 0xffffff` and reads the top byte as the primitive's word count;
and `Blaze.exe` itself packs that length in with `*prim = *otEntry | 0x2000000`. Both sides
of the interface depend on it, so it cannot be worked around on either — the arena has to
sit below `0x01000000`. Full derivation in `Documentation/DLL/winsys3d.md`.

This deviation is safe because `MainMem` is a genuine indirection: three occurrences in
27 703 lines of `winsys3d.c` (line 568 initialises, lines 5655 and 5843 read), and nothing
between `MainMem + 0x0` and `MainMem + 0x280000` is addressed by a fixed address. The DLL
file is never modified and no Windows setting is changed.

Two ordering facts make the implementation what it is, both learned by measuring:

- **Reserve before loading.** `winsys3d.dll` is an 8.3 MB image and, once relocated, is the
  strongest competitor for the same low window. Loading it first made the placement fail
  roughly one run in four; allocating first made it succeed 6/6.
- **Place before `Ordinal_789`.** `winsys3d.c:5843` caches `MainMem + 0x200000` once and
  never recomputes it, so the arena is fixed at the top of `entry()` — which also matches
  the original, where the static import mapped the DLL before the CRT entry point ran.

The preferred base `0x00B60000` reproduces `MainMem == 0x00B6DBD8` exactly, so decompiled
addresses match runtime ones. When it is taken, the highest fitting block below the ceiling
is used instead and the delta is logged at startup; when nothing fits, startup fails with a
descriptive exception naming the constraint rather than letting `DrawOTag` fault natively.

**Method note that earned its keep**: three consecutive hypotheses about why nothing
displayed were wrong (bitmap not loading, `GetDC` failing, blit skipped). What
settled it was writing the whole path's state to a log file and reading it. The
actual cause was in none of the three — and one of them, "the blit is a faithful
no-op", was a claim about code that had simply never been written.

**The main menu's whole frame path is now reconstructed.** The UI text engine
(`FUN_005443a0`) and the UI window renderer (`FUN_00545340`) both run, and a full
run completes **without throwing at all** — the first time in this project.

Three things worth carrying forward from that work:

- **The UI text engine is two passes over one dispatch mechanism.** Pass 1 scans
  through `PTR_LAB_0056e870`, pass 2 renders through `PTR_FUN_0056e728`; both
  tables are recovered verbatim in `Documentation/Reference/DataSymbols.md`. Only 5 of the
  31 distinct pass-2 handlers exist in `Blaze.c` — the rest are address-taken-only
  and need a scoped Ghidra re-run. **The main menu reaches none of them**: its
  three control codes are covered, two decompiled and one decoded by hand.
- **`FUN_005443a0` was mis-named `MenuBackgroundStub`.** It is neither a background
  nor menu-specific — 49 call sites. What it threads in and out is a **VRAM upload
  node cursor**, not a primitive cursor: `FUN_00543980` enqueues onto the same list
  `FlushDeferredImageUploads` drains.
- **A Ghidra symbol that was really a struct field.** `DAT_0056e61f` is
  `menuContext[0x8c7]` — `0x0056e61f - 0x0056dd58 = 0x8c7`, the descriptor's flags
  byte. Modelled as its own global, the menu's confirm gate was permanently open
  and it selected an entry on its first frame. Worth suspecting whenever a `DAT_`
  lands inside a known structure's extent.

**`FUN_00544150`'s signature is confirmed against disassembly** (`Legacy/Raw/
FUN_00544150.asm`, exported for this). Ghidra renders the descriptor as
`unaff_retaddr` because the function reuses arg2's stack slot as scratch; the
listing shows `MOV EAX,[ESP+0x14]` loading **arg1** after four pushes, and arg1 is
what receives `+0xe`/`+0x10`. The call sites' `(descriptor, codepoint)` is right and
the pseudocode is wrong — **the fifth time reading the listing has settled a
question that resisted careful reading of the pseudocode.**

**Earlier runtime behaviour, kept for the sequence.** Startup runs the
whole spine, loads and decodes real game resources, **enters the game loop, and
now renders frames** — `DrawOTag` completes on every one of them. Execution then
stopped on the first unreconstructed renderer:

```
=== OpenForesia ===
winsys3d arena at MainMem=0x0083DBD8 — 0x00B6DBD8 was unavailable;
subtract 0x330000 from any address read from winsys3d.c
Unhandled exception. System.NotImplementedException: FUN_005443a0: menu
background renderer (233 lines)
   at Blaze.MenuBackgroundStub(UInt32*)
   at Blaze.RunMainMenuFrameLoop()
   at Blaze.RunMainMenuState()
```

`MenuBackgroundStub` sits inside the fade branch, which the frame counter only
enters after several `FrameEnd`s — so this is reached by *iterating*, not on the
first frame. The blocker is now ordinary uncovered code with a decompiled body.

**Correction to an earlier reading of this crash.** The native `0xC0000005` in
`Ordinal_701` was previously attributed to a regression introduced by the four
input-polling functions, and believed to strike on the *second* frame. Both were
wrong, and instrumentation settled it in one run: the fault was on the **first**
`FrameEnd`, and `PollInputAndAudio` — which sits *after* `DrawOTag` in `FrameEnd`
— had never executed at all. It could not have been the cause. The polling
functions were not a regression; they had merely unblocked the frame path far
enough to expose the arena problem behind it.

One real infidelity did surface while checking them, and is worth fixing when
that area is next touched: `PollInputAndAudio`'s index is
`&DAT_00604a20 + DAT_005e9188[DAT_005e93c8[(_DAT_005de538 & 0xff) * 0x9c] * 0x4c] * 4`,
and the reconstruction hardcodes the outer index to 0 instead of reading
`_DAT_005de538`. Equivalent today (that global is 0), but not faithful. The
pointer it computes is a dead store in the current reconstruction — `DAT_00579270`
is read only by `FUN_00510ce8`, which is not yet on any executed path.

**Game state 0 is the main menu**, and it is now reconstructed. Its resource load
and screen setup complete, and execution reaches the frame loop, where the first
missing piece is `FUN_00533000` — a *named* function with a decompiled body,
rather than "game state 0".

What the menu is, from the switch at the end of its frame loop: five entries
selected by `DAT_005e909f` — three that hand off to game states 1, 4 and 6, an
options page that rewrites `BLAZE.cfg` and rebuilds the gamma tables, and a quit
that calls the CRT `exit`. The `(DAT_005e9960 & 0xf) * 0x10` counter with its
`0x80` floor is a fade; `level * 0x10101` is its grey.

**Shared frame infrastructure, reconstructed here but used by every state**:
`FUN_0052a1e0` prepares a primitive buffer and a 1024-entry ordering table
(`Ordinal_688` = libgpu `ClearOTagR`), each renderer links its primitive into the
OT and returns the advanced cursor, and `FUN_0052a280` walks the OT
(`Ordinal_701`) and swaps the draw/display env pair. The OT pointer is threaded
through return values — that threading *is* the display list.

**Both frame functions' parameters are now confirmed against disassembly**
(`Legacy/Raw/Blaze/FUN_0052a1e0.asm`, `FUN_0052a280.asm`). Ghidra gives `FUN_0052a1e0`
two parameters where the call sites pass three, and `FUN_0052a280` **none** where
they pass five. The listings settle both, and found one real error the reasoning
had missed:

- `FUN_0052a1e0` — no prologue, so args are at `[ESP+4/8/0xc]`. Its two
  apparently-identical `*param_1` writes are really `*arg1` (OT base) and
  `*arg3` (primitive buffer); `0052a257 MOV ECX,[ESP+0xc]` proves it. cdecl, 3 args.
- `FUN_0052a280` — `SUB ESP,0x14` plus two pushes puts args at `[ESP+0x20..0x30]`,
  giving `Ordinal_701(*arg2)`, `Ordinal_132(**arg4**)` and the guard on `arg5`.
  **The reconstruction had been passing arg5 to both**, so `Ordinal_132` received
  1 instead of 3 — which corrupted winsys3d's state and made `DrawOTag` fault
  natively. Neither value is distinguishable in the pseudocode: they are adjacent
  stack slots holding small integers, and Ghidra names them alike.

**Method, now proven four times over** (`FUN_005322f0`, `FUN_00542600`,
`FUN_00512930`, `FUN_0052a280`): when Ghidra loses parameters, **read the
disassembly**. Every one of these was settled in minutes by a listing after
resisting careful reasoning — and in two cases the pseudocode was not merely
incomplete but actively wrong.

That is a qualitative change from every earlier run. `LoadFirstLevelResources`
now completes — `DecodeResourceChunk` (#19) uploads the TIM textures for real —
`RunGameLoop` is entered, and the tick wired in #15 executes. The blocker is no
longer a gap in what can be *known*: `FUN_00542130` has a decompiled body in
`Blaze.c`, as do the other ten state handlers.

The intro/attract sequence is **skipped**, which is the original's own behaviour
when its DirectDraw wrapper cannot be constructed (#34).

**Earlier measurements, kept for the sequence they establish:**

- The attract-mode title screen **loads** `DATA/TITLE640.BMP` — the path fix is
  proven. It then throws, deliberately: see TODO #34.
- With the attract loop bypassed by a temporary local patch (not committed),
  `LoadFirstLevelResources` **finds `SYSTEM.INI;1`, walks real directory entries,
  relocates them, and reaches `DecodeResourceChunk`** — which throws its
  documented `NotImplementedException`.

  **TODO #19 is therefore exercised for the first time.** Every previous run
  either never got here or got here for the wrong reason: before the CD paths
  were fixed the loader found no entries at all, and before the relocation pass
  was reconstructed (#20/#35) it faulted on an unrelocated pointer one call
  earlier. The resource path is now genuinely blocked on `FUN_005322f0` itself,
  which is the hard blocker the analysis predicted all along.

**First end-to-end run (before the CD paths were fixed).** After the attract-loop
hang (TODO #31) and the viewport-init crash (TODO #32) were fixed, the
reconstruction ran the **entire startup spine** and stopped where the analysis
said it would — at the game loop's missing tick callback, as a descriptive
managed exception rather than a crash:

```
=== OpenForesia ===
Unhandled exception. System.NotImplementedException: FUN_0052a3d0: per-frame
tick callback (Ghidra: PTR_FUN_0056c96c) has no known assignment site yet
   at Blaze.RunGameLoop()
   at Blaze.RunMainLoop()
   at Blaze.RunApplication(...)
   at Blaze.entry()
```

Everything upstream executes on a machine with **no game CD**: CRT bring-up,
CD scan, `LoadOptionsIni`, the full intro/attract sequence (exits on iteration
1 as designed), engine/device re-init, `ShutdownMoviePlayer`,
`CheckDeviceCapabilityFlag`, `SetShadingDefaults`, `InitViewportSurfaces`,
`SetupJoystickHandlers`, `LoadUiFontFromCd`, the `Ordinal_710/668/720/92/104`
sequence, and `LoadFirstLevelResources`.

Note `LoadFirstLevelResources` completes *without* reaching
`DecodeResourceChunk` (TODO #19): with no CD, the "SYSTEM.INI"-equivalent it
loads lists no directory entries, so the loop body that would hit that stub
never runs. TODO #19 therefore remains untested rather than resolved — it will
reappear the moment real game data is present.

So the empirical blocking point is now **TODO #15, `PTR_FUN_0056c96c`** — the
same one the static analysis already identified as the game loop's blocker,
and it is gated behind the systemic missing-`Blaze.exe` problem rather than
being a reading task.

- **`DecodeResourceChunk` (Ghidra: `FUN_005322f0`)** — **now genuinely reached
  at runtime** against real game data, and the blocker on the resource path.
  Recovering it needs a **Ghidra re-run** on `Legacy/Binaries/Blaze.exe` with corrected
  calling conventions, which is possible now that the binary is in the repo —
  it is no longer gated on anything external. Reached from
  `LoadFirstLevelResources`'s directory-entry loop
  (thrown on the first entry, if the loaded "SYSTEM.INI"-equivalent file
  lists any — untested against real game data, since none is available in
  this environment). Unlike every other gap so far, this one is genuinely
  unrecoverable from `Blaze.c`'s text alone, not just unread: see TODO #19.
  Un-jamming this one likely requires disassembling `0x005322f0` directly
  (outside Ghidra's existing decompilation) rather than re-reading `Blaze.c`.
- **`PTR_FUN_0056c96c`** (`RunGameLoop`'s per-frame tick callback) — still no
  assignment site found anywhere in the reconstructed call graph (TODO #15).
  Since `LoadFirstLevelResources` now runs for real, the per-state resource
  lookup at the top of the tick loop is wired up too — only the indirect call
  itself remains blocked.
- **`FUN_00537ca0`'s eleven state handlers** — the real per-state work, now that
  the tick is wired (#15). All eleven have decompiled bodies in `Blaze.c` and
  none is blocked at the top level: `FUN_00542130` (state 0),
  `thunk_FUN_00536180` (1), `FUN_00542160`+`FUN_00537da0` (2),
  `thunk_FUN_00516020` (3), `FUN_005066d0` (4), `FUN_00504c90` (5),
  `FUN_00524180` (6), `FUN_00531b40` (7), `FUN_005315d0` (8), `FUN_0051bb00`
  (9), `FUN_00513ba0` (10). This is the largest body of straightforwardly
  readable code reachable from the spine. Note it is gated behind #19 at
  runtime, but not for reading. **Correction (2026-08-14):** "none is blocked"
  held only one level deep. State 4 (`FUN_005066d0`) splits into a loader
  (`FUN_005066e0`, now reconstructed) and a frame loop (`FUN_00506880`) whose
  eight sub-state handlers are *not* in `Blaze.c` — see the dated section below.
  The other ten have not been checked one level down.
- **`FUN_0052a440`** — **not the per-game-state dispatcher** after all; the tick
  pointer reaches `FUN_00537ca0` instead (#15). Kept here because it is still
  reached from `RunGameLoop`'s own state handling. (~100 lines, already read,
  not yet transcribed) pulls in further rendering-pipeline functions
  (`FUN_00521f20`, `FUN_0051f9c0`, `FUN_0053db80`) and input/audio functions
  (`FUN_00512520`, `FUN_005048f0`, `FUN_00504a30`, `FUN_005357a0`) — next
  after the tick-callback question is resolved.

The DirectSound subsystem (`FUN_00513xxx` + its MS-ADPCM codec) was completed
this session and hit **no hard blocker** — it is reconstructed end to end. Its
two remaining gaps are of the already-catalogued kinds rather than new ones: an
address-taken-only thread body (TODO #23) and missing `.data` content
(TODOs #24/#27/#30). Its sound-effect half now *does* have its caller: the
`FUN_0053cea0`/`FUN_0053dxxx` layer was completed this session too, so the path
from a gameplay call site down to `IDirectSoundBuffer::Play` is unbroken.
`PlayMusicTrack`/`FUN_00529ad0` still sits unreconstructed above the music half.

All four of `RunApplication`'s post-loop teardown stages are now reconstructed
(`ShutdownStage1`/`3` this session, `4`/`5` earlier) — that whole branch is done.

**Window/device creation was investigated and is now a closed question with a
negative result** — see TODO #6: the main window's creation site does not exist
in any of the six available sources. (Two hypotheses from the previous session
were checked and disproved: `FUN_00513030` is `IDirectSound::CreateSoundBuffer`,
not window creation — it takes a buffer size, not an `HWND`; and `Setup3DDevice`
creates no device, it is the renderer-selection dialog, TODO #1.) Recovering it
requires re-running Ghidra on `Blaze.exe`, so it is gated behind the systemic
blocker above rather than being a reading task.

That makes the remaining *reading*-only work fairly bounded. The most productive
unblocked directions now:
- `FUN_0052a440` (the inter-state loading screen — **done**, see 2026-08-14) and the rendering/input functions
  below it — the largest body of still-readable code reachable from the spine.
- ~~The `FUN_00513xxx` DirectSound family~~ — **DONE this session**, together
  with its MS-ADPCM codec dependencies (`FUN_00531bb0`/`FUN_00531f70`). The
  register-attribution bug in `FUN_00513030` flagged here previously was
  confirmed and transcribed as intent; see TODO #25.
- ~~The sound-effect layer~~ — **DONE this session**. (It lives at
  **`FUN_0053dxxx`**, not `FUN_00537xxx` as an earlier revision of this file
  said; `FUN_00537xxx` is texture/screen code.) The five resamplers plus the
  `FUN_0053cea0` dispatcher are reconstructed; only the positional-gain pair is
  left, and deliberately so (TODO #28).
- **`FUN_00512xxx` @ `Blaze.c:63354`** — the level-load path that fills the
  second half of the sound directory and the level sample bank (TODO #29). Small,
  and it would complete the sound-effect data path.

Smaller in-place gaps worth picking off opportunistically:
- `FUN_00523120`/`FUN_00523220` (DirectInput joystick device creation, tied to
  the still-unlocated main window handle `DAT_005db85c`) — skipped inside
  `SetupJoystickHandlers`.
- `Ordinal_40` (winsys3d.dll ordinal 40, `FUN_00a04fd0`) — body not read yet.
- `IsKnownOverrideDestination`'s 2 stubbed true-branch bodies (native
  per-entity-class override tables) — only matter once something actually
  assigns `DAT_00604930`/`DAT_00600904`.

Recommended order: `DecodeResourceChunk` first if a disassembler becomes
available (it's the only *hard* blocker left in this immediate path); failing
that, the `PTR_FUN_0056c96c` tick-callback question is the next most direct
lead (finding its assignment site would let `RunGameLoop` actually loop
instead of throwing on iteration 1), then `FUN_0052a440` and its
rendering/input dependencies. The smaller gaps above are independent and can be
picked off any time; of them, the level-load path at `Blaze.c:63354` (TODO #29)
is the cheapest genuinely useful one, since it is what makes the reconstructed
sound-effect layer able to reach its second sample bank.

## 2026-08-14 — the menu background, and a fifth structure split in two

**The main menu's background image was missing, and the cause was in the reconstruction, not the
backend.** `FUN_00542250`'s per-frame draw builds a 20-byte sprite descriptor on its stack and
passes **its address** to both renderers:

```c
local_10 = uVar3 * 0x10101;                          /* +0x04  colour, the fade's grey  */
local_c  = 0xa0;  local_a = 0x78;                    /* +0x08  x = 160, y = 120         */
local_8  = 0x1420; local_6 = 0x1149;                 /* +0x0c  tpage, +0x0e  clut       */
local_14 = *(*(DAT_005e97c4 + 0x94) + 4);            /* +0x00  descriptor pointer       */
FUN_00521f20(local_24, local_28, &local_14, 0, 0);   /*        ← &local_14              */
```

`local_14` .. `local_4` are **one contiguous structure**; the reconstruction had allocated
`&local_14` and `&local_10` as two separate heap blocks, so the sprite reached the renderers with a
null position, size and texture page and drew nothing.

**This is the fifth occurrence of the same shape of bug**, after `DAT_0056e61f` = `menuContext[0x8c7]`,
`DAT_00566cc4` = `s_ResolutionIndex`, `DAT_005e8fc0`, and the four `DRAWENV`/`DISPENV` structures
whose contiguity the random-black-screen bug turned on. The lesson has now earned a rule: **a `DAT_`
or a stack local whose address is taken is a suspect** — check what lies immediately after it before
modelling it as an independent entity.

Fixed; the background renders. Verified against the original renderer
(`OPENFORESIA_LEGACY_3DHELP=1`) on the same build: same image, mean difference **4/255 per channel**
once the deliberate viewport-origin difference is accounted for — the alignment search finds the
best match at exactly `(32, 24)`, the origin `SetViewport(32, 24, 960x720)` poses and which
`DeviceShim` drops on purpose.

**A consequence for the coordinate-scale question (task 4.5).** With the background present, the
menu panel is visibly smaller, relative to it, than in the retail 640x480 screenshot (30 % of the
width against 45 %). That is **not** a porting defect: `3dhelp`, run on the same build at the same
`Resolution=4`, produces the same proportions. The game's UI does not scale with the resolution
while its background does. Any change here would be a deliberate improvement, not a fidelity fix.

## 2026-08-14 — the menu's confirm is wired to the wrong variable (measured)

Trying to reach the options page turned up a defect in the main menu that the earlier "confirm
works" check had hidden. Instrumenting the frame loop (`OPENFORESIA_TRACE_MENU=1`) gives, per
frame:

```
[MENU] pad=0x0040 fade=9 sélection=0 drapeaux=0x05
```

Three facts, and each one matters:

- **`pad=0x0040` is DOWN** — input reaches the menu correctly, and the cursor does move on screen.
- **`sélection` never leaves 0**, even as the cursor walks down the entries. `DAT_005e909f`, the
  variable the frame loop's `switch` reads, is **never written to a non-zero value anywhere in
  `Blaze.c`** — 18 reads, two writes, both `= 0`. So the switch can only ever pick case 0. The
  cursor's real position lives elsewhere, almost certainly in the menu context descriptor, exactly
  as `DAT_0056e61f` turned out to be `menuContext[0x8c7]`. **This is the fifth instance of that
  shape**, and the reason "Options" cannot be opened.
- **`drapeaux` goes 0x01 → 0x05** after a few frames — bit 2 rises when the text stream ends — and
  the switch is gated on `(flags & 4) == 0`. So confirm only ever fires during the first frames,
  which is why one earlier test appeared to work: it pressed confirm inside that window and got
  case 0, "Début du jeu", regardless of where the cursor was.

Consequence for the record: the earlier note "the confirm key leaves the menu" is true but
misleading. Confirm left the menu **to game state 1 only**, whatever the cursor showed.

### Answered the same day: the cursor index is `menuContext[0x8c4]`

Found by diffing the whole descriptor between frames (`OPENFORESIA_TRACE_MENU=1`), which is the
same instrument that identified `menuContext[0x8c7]`:

```
[MENU] pad=0x0040 ... changés=1 +0x8C4:00->01
[MENU] pad=0x0040 ... changés=1 +0x8C4:01->02
[MENU] pad=0x0040 ... changés=1 +0x8C4:02->03
```

One byte moves, in exact step with the arrow presses that move the cursor on screen, and no other.
The selector is therefore `menuContext[0x8c4]` — address **0x0056e61c**, three bytes before the
flags byte the same `if` reads. One dword, two fields.

**Ghidra never named 0x0056e61c**, because nothing in the program reaches it as an absolute
address: it is only ever `descriptor + 0x8c4`. That is why the decompiler attributed the switch to
`DAT_005e909f`, an unrelated global that happens to be live nearby — the same failure mode as
`unaff_retaddr` in `FUN_00544150`, and the sixth instance in this project of a `DAT_` that is
really a structure field.

**Verified end to end**: with the selector corrected, walking the cursor to "Options" and confirming
now dispatches to the **Options** case instead of case 0. The menu is genuinely navigable.

## 2026-08-14 — the options page: its drawing layer is in, the rest is mapped

The main menu's "Options" entry opens a **second UI layer**, unrelated to the display-list renderer
every other screen uses. It talks to the 3D device directly: each widget ends in `FUN_00510500`
(textured quad) or `FUN_00510330` (flat fill), which build four `D3DTLVERTEX` on the stack and
submit one `DrawPrimitive(TRIANGLEFAN, TLVERTEX, …, 4, 0)`. The device is what `Ordinal_750`
returns — `DAT_011be760`, the object `DeviceShim` already faces — so **this layer renders through
the Direct3D 11 backend with no further plumbing**.

Reconstructed and compiling: `FUN_00510110` (resolution mapping), `FUN_00510330`, `FUN_00510500`,
`FUN_005108f0` (label strip), `FUN_00510740` (nine-patch frame), `FUN_00510a70` (cursor).

Two things settled by reading rather than assumed:

- **The vertex order.** In both builders the address passed to `DrawPrimitive` disagrees by 16
  bytes with the one passed to the scaling helper, so the slot names cannot be trusted alone.
  Grouping the assignments by the 32-byte `TLVERTEX` stride resolves it: the array starts where the
  scaling helper points, and the four vertices are `(x1,y1) (x2,y1) (x2,y2) (x1,y2)`.
- **The resolution tables** at `0x00566c98` / `0x00566cb0` are `{-160,-64,0,80,192}` and
  `{-120,-48,0,60,144}`. Entry 4 is `(192, 144)` — exactly the "base" the `[SHIFT]` runtime trace
  prints, which is what confirms they are the right tables.

**A correction to this morning's note on the texture class.** `Documentation/DLL/3dhelp.md` was
told that slot `[2]` — the `BITMAPINFO` loader — is never called. That holds for the main menu and
is false in general: **the options page uses it**, for four 256x256 8-bit atlases it rasterises
itself from `FONT.PIC` and the CD's `CONFIG.TXT` / `ACTION.TXT` / `JOYSTICK.TXT`, followed by a
colour key through slot `[6]`. Both slots are now implemented on the synthetic device (bottom-up
DIB, 4- and 8-bit palettised, colour key applied by re-expanding the kept indices). The general
lesson is the one `+0xDC` already taught: *zero calls measured* means in the measured phase, never
*never*.

**The setup chain is reconstructed too** (second pass, same day): `FUN_0050fab0`, `FUN_0050f7d0`,
`FUN_0050f9b0`, `FUN_0050f680` (the software text rasteriser), `FUN_0050f770`, `FUN_0050f560`, and
`FUN_0050fa50` — which turned out to be `BuildCdPath`, already present. Everything it needs from
`wsyskern` is a **named export**: `DIBCopyBlock`, `Gamma`, `CreateArray`, `LoadArray`,
`ArrayToString`, `DeleteArray`, alongside `LoadBitmapFile`.

What it does, worth knowing before touching it: the page's labels are **rasterised in software**,
glyph by glyph, out of `DATA/FONT.PIC` (cells 10x14, 31 to a row) into six 256x256 8-bit buffers,
from four text files on the CD — `CONFIG.TXT`, `ACTION.TXT`, `JOYSTICK.TXT`, `KEYBOARD.TXT`. Only
then are those buffers handed to the device as textures.

One structural detail that a straight reading would get wrong: the three 18-entry label-width runs
are **contiguous** from `0x00589758`, and `ACTION.TXT`'s widths are written **downward** from
`0x00589754` — which is what makes `FUN_005108f0`'s negative label ids resolve. One block with the
base pointer placed inside it reproduces that; two arrays would not.

**It runs to completion.** Getting there took two distinct bugs, one in `wsyskern` and one of ours.

### Bug 1 — `wsyskern`'s `LoadArray` corrupts the heap here, and is now bypassed

Symptom: the process dies with `0xC0000374` (heap corruption), **cumulatively with the number of
lines read** — 1 line is fine, 4 lines kill it a few stages later, the whole 51-line file kills it
immediately — and always at the next allocation, which is what a corruption *detection* looks like.

Ruled out by measurement, so not worth reopening:

- the call is correct: `LoadArray` ends in `RET 0x18`, so **6 stdcall arguments**, and each one's
  meaning was read from `FUN_10004060` (start index, line cap, lines to skip, flags);
  `ArrayToString` likewise, `RET 0x10`, 4 arguments;
- the object is valid: `CreateArray` writes the `"ARAY"` tag (`0x59415241`) that the validator
  `FUN_10003f10` demands, and `LoadArray` returns *cleanly* when it is not — which is exactly what
  it does with a missing file, a case where **the entire setup then runs to the end**;
- it is neither the initial capacity (0 and 0x80 both fail), nor the growth path (the "do not grow"
  flag changes nothing), nor the path (correct, file present).

Since what the array provides here is only "give me line *i* of a text file", the reconstruction now
reads the files itself. That is the same nature of deviation as replacing DirectDraw with Direct3D
11, several orders of magnitude smaller, and it touches no game logic.
`OPENFORESIA_WSYSKERN_ARRAYS=1` restores the original path for whoever wants to resume the
DLL-side diagnosis.

### Bug 2 — the glyph index was signed, and should not be

Ours, and the same shape as several before it. `FUN_0050f560` takes the character as an `int`;
Ghidra types the caller's variable as `char`, and transcribing that literally makes `'ô'` (0xF4)
arrive as **-12**, so the index comes out **-45** and every accented label indexes before the width
table.

**The pseudocode itself proves the parameter is unsigned**: the function compares it against
`0x80`, `0x8a`, `0x9a`, `0xc0` and `0xe0` — values a signed `char` can never hold. Read as
unsigned, `0xF4` gives glyph 174 and `0xE9` gives 167, both inside the 186-entry table and inside
the 312x84 font sheet's 31x6 grid. The control-character test in the caller genuinely is signed;
the two are not the same test.

Measured after the fix: all four label sets rasterise, the six atlases and the two `.PIC` textures
are created (`256x256`, 8-bit, bottom-up, through the texture class's `[2]` slot), and execution
stops on the page loop's own `NotImplementedException`. The setup is **on by default** again.

### Leaving the options page put the menu in a loop, and the fade counter is a frame counter

Second half of the same report: Escape from the options page flashed the main menu and came
straight back. Instrumented, the mechanism is clear and has two parts.

**Part one — the key is still held on return.** Escape is bound to pad mask `0x4000`, so the menu's
first frame after the page returns sees it down, treats it as a confirm, and re-opens the same
entry. The game's own idiom for this is the drain loop the options branch already runs *before*
opening the page (`while (FUN_005048f0() != 0)`); it is now applied symmetrically on the way out.
It must be **paced** — spinning without letting the message pump run leaves the DirectInput state
frozen and the loop never ends, which is what the first attempt did.

**Part two — the confirm gate stays open.** Draining is not enough: the switch is gated on
`menuContext[0x8c7] & 4`, and that bit stays clear after the page returns, so the very next frame
fires the same case again. The measurement that shows it plainly is `Clear(TARGET)` frozen while
`BeginScene` keeps counting — the menu never draws a frame, because it re-enters the page every
time.

**Part three — the interactive state is a pair, not a bit.** Raising bit 2 alone brought the menu
back but left it dead: visible, and no cursor. The per-frame descriptor diff had already shown the
answer and it was misread the first time — a confirm drops **two** fields together
(`+0x03B:1->0` and `+0x8C7:05->01`), so the steady interactive state is `[0x3b] == 1` **and** bit 2
raised. Restoring both is what `Handler_00544b10` does at end of page; it also resets the cursor to
0, which is deliberately not reproduced here so the player's selection survives the round trip.
Verified: the menu returns, stays, and its cursor moves.

**Both are deviations, and both point at the same unfinished reading.** The original needs neither,
which means its panel distinguishes a *newly pressed* button from a held one, where this
reconstruction reads the held word (`CurrentPadWord`, the state word, not the edge word that
`UpdateButtonEdges` computes right beside it). That is where the clean fix lives; these two lines
stand in until the panel's input semantics are established.

**A useful fact recovered on the way**: `DAT_005e9960` is not a fade counter but a **global frame
counter**, incremented by `FUN_0052a280` — `FrameEnd` — on every frame. The menu's
`(counter & 0xf) != 8 && level > 0x7f` test with its `counter--` is therefore a *hold*, not a
countdown: the menu pins the counter inside a band for as long as it is the active screen.

### Escape was quitting the game, and the culprit was our own window

Reported as "the back key behaves strangely in the options". Measured: pressing Escape anywhere in
the options killed the process with exception code **0xE0000001** — which is the game's own
software-exception code, so the first reading was that "Quitter le jeu" had somehow run. Tracing
showed it had not: `UiSelect` never even saw the key.

The cause is in `D3D11Window`'s window procedure, which had

```csharp
if ((int)wParam == VkEscape) CloseRequested = true;
```

a convenience from when that window was a test harness with no game input behind it. Escape asked
the window to close, which becomes `WM_QUIT`, which the game's own message pump turns into
`RaiseException(0xE0000001)` — quitting instead of going back. Removed: the window closes on
`WM_CLOSE`, like any window. Escape now returns one page at a time, verified two levels deep.

Worth generalising: **a development affordance in the host layer can impersonate a game behaviour**
so exactly that the evidence (the game's own exception code) points at the game.

### "Quitter le jeu" now exits instead of crashing

`RaiseException(0xE0000001)` was transcribed literally at all three sites — the options' quit entry
and the movie player's two message pumps. Faithful, and useless here: nothing on the managed side
catches it, so it terminated the process through Windows error reporting and none of the five
shutdown stages ran.

It is now a managed `BlazeQuitException`, caught in `RunApplication` immediately before the
`finally` that already carries those stages — which is exactly where the original's SEH handler
resumes. Verified: the entry exits the game, and no crash is recorded in the event log.

### The key editor

`FUN_00511d40` is reconstructed, with its two helpers (`FUN_005115a0` scancode→name index,
`FUN_005109c0`/`FUN_005108b0` draw a key name) and its five data tables, all read out of
`Legacy/Binaries/Blaze.exe`:

| table | size | content |
|---|---|---|
| `DAT_00562e18` | 85 bytes | the DirectInput scancodes the editor can name, in KEYBOARD.TXT's order |
| `DAT_00562ec0` | 24 bytes | action → its rank in the 20-entry binding block |
| `DAT_0056305c+4` | 4 bytes | the direction arrow's column, for actions 4..7 |
| `DAT_00564ab8` | 40 ints | player 1's default bindings — they match `K0000`..`K0113` in the installed BLAZE.cfg exactly, which confirms them |
| `DAT_00562ed8` | 49 x 8 bytes | the rows: colour, action, group, label |

The row table is transcribed as raw bytes rather than as an interpreted structure, so it can be
re-read line by line against the dump. Its shape is still legible: eight global actions in white,
then four "Joueur N" blocks in `FFBFBF` / `BFFFBF` / `BFBFFF` / `FFFFBF`, each a header plus nine
actions, and a last row of action −2 — the reset to defaults.

**The "second key" is not a second table.** `DAT_00564ba8` is `DAT_00564b58` offset by twenty
entries: it is **category 1 of the same binding table**, i.e. the alternate keyboard layout that
`FUN_00523660` already reads alongside category 0. Assigning a key pushes the old one into that
slot and clears the new scancode from every keyboard binding of all four groups — the joystick
category is deliberately left alone.

Verified on screen: the eight global actions with both their keys ("NumPad 9" + "Pg up", …), the
coloured "Joueur 1" header, the scrolling window and the cursor.

**One defect of ours, found by looking at the screen.** `UiDrawLabel` still clamped negative label
ids to 0 when reading the width table — a leftover from when that table was an `int[]` rather than
the pointer placed inside its block. Every ACTION.TXT label was therefore drawn with label 0's
width and truncated to the same short length. The clamp is gone; the labels render in full. Worth
remembering as a category: **a guard written for an older representation outlives it silently**,
because it never throws.

### Three of the four leaves, and what the fourth really costs

`FUN_00510d40` (Timing), `FUN_00510e60` (résolution) and `FUN_005119c0` (joystick assignment) are
reconstructed. The joystick page was verified on screen: four "Joueur N: Joystick N" rows — the
value positioned by the width of the label just drawn, which is what the width table is for — then
four "Configuration joueur N" rows, white because all four players are assigned and grey when they
are not.

Two things worth recording:

- **`FUN_00510d40` is the Timing page, not a screen-adjust page** as its stub had guessed from the
  call site. It writes the ini's `Timing` key and calls `SetTimingMode`.
- **The resolution page cannot do anything yet, and says so.** It needs two device slots the
  synthetic device does not port: `+0x1c`, the bitmask of accepted display modes, and `+0xE8`, the
  actual mode change that `ChangeResolution` drives. The mask comes back zero, so the list is empty
  and only the title draws. A guard was added that the original does not need — its device always
  advertises at least one mode — and it is marked as such.

**The fourth leaf is bigger than its line count suggests.** `FUN_00511d40`, the key editor, is 230
lines *plus* a scrolling list over the descriptor table at `DAT_00562ed8` (8 bytes per row), a
"press a key" capture mode, two helpers (`FUN_005115a0` scancode→label, `FUN_005109c0` draw a key
name) and four `.data` tables to recover (`DAT_00562ed8`, `DAT_00562ec0`, `DAT_00562e18`,
`DAT_0056305c`). Reading it also surfaced a fifth leaf that was not on the list: `FUN_005115c0`,
the per-player joystick button config, 168 lines, reached from the page above. Both are stubbed
with their dependencies named.

### The three submenus run too

`FUN_005110b0` (Affichage), `FUN_005113d0` (Son) and `FUN_00512220` (Contrôle) are reconstructed,
plus the gauge widget `FUN_00510a00` and the gamma reapply `FUN_0050f980`. All three draw and
navigate; verified on screen:

- **Son** — "Volume musique" and "Volume effets" with working gauges, "Musique activé". Right
  twice on the first entry moves its knob and leaves the second alone, i.e. the ±8 step, the
  0..0x80 clamp and the immediate `ApplyCdVolume` all behave.
- **Affichage** — "Dimensions de l'écran" and "Clarté" with their gauges (the first near maximum at
  `Size=21`, the second mid-range at the ini's gamma of 64), then the filter, status-size and
  timing lines. Two Downs land the cursor on the filter line, which exercises the awkward part:
  the cursor's row is `(sel > 1) + 6 + sel + (sel > 0)`, because the two gauge entries occupy two
  rows each.
- **Contrôle** — its two entries, each opening a page that is still a named stub.

Everything they depend on was already reconstructed — `ApplyCdVolume`, `ApplyAllSoundBufferVolumes`,
`ToggleMusicEnabled`, `SetShift`, the detail-limit table, `TexFilterPossible`. Four leaves remain
stubbed, all with decompiled bodies and no blocked dependency: `FUN_00510d40` (36), `FUN_00510e60`
(95), `FUN_00511d40` (230, the key config) and `FUN_005119c0` (122, the joystick config).

### The page runs

`FUN_0050fe90`, `FUN_005101c0`, `FUN_00510aa0` and the loop `FUN_00512320` are reconstructed, and
the page draws: background picture, framed panel, the "OPTIONS" title, its four entries with their
accents, and a cursor that moves with the arrow keys (verified by frame diff — the change is
confined to the cursor column). Only the three submenus remain stubs (`FUN_005110b0`,
`FUN_005113d0`, `FUN_00512220`, 90+64+39 lines).

Three defects had to be cleared along the way, and all three are worth remembering:

- **The texture class has more slots than this file recorded.** Re-read at `0x0FFE61F8`, its vtable
  carries at least eleven entries, not six. `FUN_0050fe90` calls slot **8** once per texture per
  frame — `mov eax,[ecx+8]` then the surface's `+0x60`, i.e.
  `IDirectDrawSurface::IsLost` — and the synthetic class only had eight slots, so the call would
  have jumped past the end of its own vtable. It now has twelve and answers "not lost", a Direct3D
  11 texture having no equivalent loss.
- **`DAT_005d9de0` is `s_GammaIni`**, the ini's "Gamma" key, already reconstructed — the comment on
  `LoadOptionsIni` said so, and it had been redeclared anyway. Never assigned, it read 0.
- **`_DAT_0054d000` is `1/64`, read from the binary** (`0x3C800000` at `0x0054d000`), not the
  `1.0f` a TODO had assumed — and it is the same symbol as `GammaIniScale`, which already carried
  the right value by inference. Together those two made the page compute `Gamma(value, 64.0)`, which
  whitens every palette: the background and the frame were drawn pure white and looked absent,
  while the labels — which do not pass through `Gamma` — were fine. Setting the scale to `1/64`
  turns the ini's default 64 into a neutral gamma of 1.0 and the picture appears.

That is the seventh and eighth global reconstructed under two names. The pattern is stable enough
to be worth stating as a rule: **before declaring a `DAT_` field, grep the file for its address** —
half of them are already there under another name, and the older one is not always the wrong one.

## 2026-08-14 — `FUN_0052a440` is the loading screen, and its frame is lost

Reconstructed and running. **It is not the "per-game-state dispatcher"** this file used to call it;
two call sites fix its role: `RunGameLoop` runs it whenever the game leaves a state for one other
than 7, 9, 2 or 3, and `FUN_00516250` runs it when the area index changes, right before that area's
resources load. It loads one resource entry into `MainMem + 0x1b0000`, decodes it, and shows it for
150 frames — skippable with START or SQUARE, the skip taking effect on **release**, which is what
stops a button held from the previous screen from eating the screen.

Measured after the fix: confirming an entry in the menu now runs two further 60-frame blocks with
`DrawPrimitive` almost flat — the loading screen's frames, drawn empty for the reason below — and
execution then reaches game state 1 and stops at `thunk_FUN_00536180`. **That is the new blocking
point.**

**Ghidra did not recover this function's stack frame**, and it costs exactly two things. Both are
marked TODO(REVERSE) in the code rather than guessed:

- **the second `MoveImage`'s source rectangle.** The eight ushorts the function opens with are two
  VRAM rectangles, `{0x380, 0, 0x80, 0xe0}` and `{0, 0x1ff, 0x100, 1}`. The first call's
  destination is well supported — the two zeroes assigned immediately before it are its pushed
  arguments — but the second is rendered with a source address four bytes below the first
  rectangle, which cannot be right.
- **the sprite descriptor's pointer field.** The original sets it to the address of a stack slot
  that *nothing in the pseudocode ever writes*. It must be an array of 8-byte texture records,
  since `DrawSpritePrimitive` indexes it as `*(int*)sprite + sprite[4] * 8`, and the menu's
  equivalent comes from the resource directory. Left null, and the two draws are skipped while it
  is: **the loading screen is currently blank**. A visible, harmless degradation; an invented
  pointer would fault or draw garbage.

A listing of `0x0052a440` closes both in minutes — the same move that settled `FUN_0052a1e0`,
`FUN_0052a280`, `FUN_00544150`, `FUN_005322f0` and `FUN_00512930`. No disassembler is available in
this environment, so it needs a Ghidra export like the ones already in `Legacy/Raw/Blaze/`.

What the frame did *not* cost, worth recording so it is not re-litigated: the argument order of
`FUN_00529b50(entry, destination)` and `FUN_005322f0(timData, slot, metadata)`. `FUN_00516250`
calls the former with both arguments intact, and both ran against real data here without faulting.

## 2026-08-14 — DirectInput bring-up, and TODO #6 stops mattering

`FUN_00523120` and its whole init chain are reconstructed: `FUN_00522dc0` (the constructor,
`DirectInputCreateA` version 5), `FUN_00522eb0` (init), `FUN_005233c0` (keyboard) and
`FUN_00523470` (mouse). Measured at runtime: **the keyboard is created, formatted, cooperative and
acquired**, the mouse likewise, no failing HRESULT, and the menu renders exactly as before.

**`DAT_005db85c` (TODO #6) is no longer a blocker, and never was one for this call.** The handle
that was searched for exhaustively and never found is used for exactly two things, both read out of
the chain rather than assumed:

- it is the `HINSTANCE` argument of `DirectInputCreateA` — and `FUN_00522dc0` **handles the null
  case explicitly**, substituting `GetModuleHandleA(NULL)`, which is what the single call site
  passes anyway (`FUN_00523120(0, DAT_005db85c, …)` passes 0 as the instance);
- it is the window given to `SetCooperativeLevel`.

The reconstruction now owns a real window — the Direct3D 11 backend's — which did not exist when
TODO #6 was written and declared blocked behind a Ghidra re-run. There is therefore a legitimate
handle to pass, and it works.

Everything the DLL side needed came out of `Legacy/Binaries/Blaze.exe` rather than out of guesswork:
`GUID_SysKeyboard` (0x0054db58), `GUID_SysMouse` (0x0054db48), `GUID_Key` (0x0054db18), the three
axis GUIDs (0x0054da98..b8), and both `DIDATAFORMAT`s with their object tables (0x00546f60 /
0x00545f60 for the keyboard, 0x00546ff0 / 0x00546f80 for the mouse). The tables are rebuilt field
for field rather than copied, because every entry holds a pointer into `Blaze.exe`'s image, which is
not mapped in this process.

**The menu is navigable.** `FUN_005048f0` and everything under it are reconstructed too —
`FUN_005231f0`/`FUN_005230b0` (read every acquired device), `FUN_00523410`/`FUN_005234c0`
(`GetDeviceState` with a re-`Acquire` retry, the same body twice with different sizes),
`FUN_00523660` (keyboard → pad state) and the two-block folding loop that closes `FUN_005048f0`.

Verified at runtime by injecting real key events: the cursor moves exactly one entry per arrow
press and back up, the difference between frames confined to the cursor column (`x=[24..52]`,
the y-band stepping down and back), and the confirm key leaves the menu — landing on
`FUN_0052a440` as a descriptive exception — since reconstructed, so the frontier has moved on to
game state 1. **That was the then-new blocking point, and it was ordinary uncovered code.**

Three things worth carrying forward from `FUN_00523660`:

- **The button convention is active low.** A player's word starts at `0xffff` and every bound key
  that is down clears its bit. The 20 masks come from `DAT_00564f18`, read out of
  `Legacy/Binaries/Blaze.exe`: unmistakably a PlayStation pad — `0x0010` up, `0x0020` right, `0x0040` down,
  `0x0080` left, `0x0008` start, `0x0001` select, the face buttons at `0x1000..0x8000`, the
  shoulders at `0x0100..0x0800`, and four two-button combinations closing the table.
- **`state[0x1c] |= state[0x9c]`** is `DIK_RETURN |= DIK_NUMPADENTER` — the keypad's Enter folded
  into the main one. A one-line operation that reads as noise until the scancodes are named.
- **Scancode `0x3e` is `DIK_F4`**: held down, it silences the keyboard for every player. Worth
  knowing before chasing an input bug.

**Two gaps left on this path, both named:**

- `FUN_00523230` (per-joystick device creation) — Ghidra's decompilation is unusable: the entire
  frame is `puStack_`/`piStack_` temporaries and every vtable call has lost its arguments. This is
  the "read the disassembly" class, not the reading class. The enumeration still runs and counts
  attached sticks; the 16 device cells stay null, which every consumer already tests for.
- `FUN_00523340` / `FUN_00522fe0` (reading one joystick's state) — unreachable while no joystick
  device is created, and left as a loud throw rather than a silent skip for the day one is.

Also fixed here: `DAT_00604b54` had been reconstructed under **three** names
(`s_JoystickDevice`, `s_JoystickDevice_00604b54`, and a third added while writing the creation
chain). One address, one field — the same defect as `DAT_00566cc4` / `s_ResolutionIndex`.

## 2026-08-12 — DLL-side findings behind the two open rendering tasks

Nothing here changes a numbered `TODO(REVERSE)` or the current blocking point:
this is knowledge about `3dhelp` and `winsys3d`, not about `Blaze.cs`. It is
recorded because it removes two things this file and `TODO.md` previously
treated as unknown, and because both were established by **reading the DLL
binaries' data sections**, a move that is easy to forget is available.

**Textures (task 3.5 of `replace-3d-backend-with-d3d11`).** The argument passed
to device slot `+0x64` is an **object pointer**, not an opaque
`D3DTEXTUREHANDLE` — earlier notes said the opposite and concluded the pixels
were unreachable. The object is a 32-byte `3dhelp` texture (vtable
`0x0FFE61F8`) holding two `IDirectDrawSurface` pointers, a palette, and the
handle at `+0x10`; its pixels originate as a **plain GDI DIB** blitted in with
`StretchDIBits`. Full chain, layouts and the three candidate hook points:
`Documentation/DLL/3dhelp.md`, section "Les textures — du DIB au handle".

**Coordinate scaling (task 4.5).** `Size` drives the viewport *and* the vertex
lookup tables at once, with factor `(Size+9)*32 / 320`; a high-resolution branch
in `SetDispEnv` decouples the two, which is the leading explanation for geometry
landing in a 320x240 space inside a 960x720 target. Also established, and new to
the repo: **the engine's logical reference space is 640x480**, and
`DAT_00a2c49c` indexes a five-entry display-mode table whose per-mode offsets are
exactly `(mode − 640x480) / 2`. See `Documentation/DLL/winsys3d.md`, section
"Coordinate scaling".

**Two things deliberately left as hypotheses**, both settled by measurement
rather than further reading:

- whether `3dhelp`'s DirectDraw surfaces are still alive after task 4.3 stopped
  initialising its display path — this decides between the two cheap hook points;
- `SetShift` assigns the same address to the X and Y table pointers while the
  high-resolution branch assigns two addresses 160 000 bytes apart. One reading
  is wrong, most likely a Ghidra mis-recovery on one line.

**Consequence for `TODO.md`**: the "one window only" request is **not** gated on
task 3.5. The title screen and attract draw through device slots `+0xC4` /
`+0xCC` / `+0xD0`, which `DeviceShim` already intercepts, and porting them needs
no DirectDraw surface read at all.

## Tooling note

A small PE export-table dumper was written to `scratchpad/pe_exports.py`
(session scratchpad, not committed) to resolve ordinal-only imports against
the actual DLLs shipped next to Blaze.exe (`playst.dll`, `winsys3d.dll`,
`wsyskern.dll`, `3dhelp.dll`, `gldhelp.dll`). A PowerShell port,
`scratchpad/pe_exports.ps1`, was added this session (same pure PE32
export-directory-parsing logic, no external dependencies) since no working
`python` was on `PATH` this time around — prefer it going forward on this
machine: `./pe_exports.ps1 -Path <dll> -Wanted 331,336,...`. Once an
ordinal's RVA is known, add `winsys3d.dll`'s base address (`0x00a00000`,
confirmed via `CreateTranslationTables`) or `wsyskern.dll`'s (`0x10000000`,
confirmed via `LoadBitmapEx`) to find the matching function in that DLL's
decompiled `.c` file. **Faster alternative discovered this session**:
`winsys3d.c` embeds the ordinal number directly in a comment inside each
function body (e.g. `/* 0x2490  129 */`), so grepping for
`  <ordinal>  ` (with the surrounding spaces, to avoid matching substrings of
other numbers) inside `winsys3d.c` directly often finds the implementation
faster than round-tripping through the PE export table at all — see
`Documentation/DLL/winsys3d.md` and `Documentation/DLL/wsyskern.md` for
worked examples of both workflows.

**`OpenForesia.Tools at` works on the helper DLLs too**, not just
`Legacy/Binaries/Blaze.exe` — `at winsys3d.dll <va>`, `at 3dhelp.dll <va>`, using the
very `@ 0xXXXXXXXX` addresses those `.c` files carry. Unlike `Blaze.c`, the DLLs
ship with their data sections in the repo, so a DLL symbol whose value lives in
initialised data is **never** blocked the way a `Blaze.c` one used to be: it is
one command away. This is how the display-mode table and the `3dhelp` vtables
were recovered on 2026-08-12 rather than guessed. Two caveats found in passing:
build the tool with the x64 SDK but **run the `net9.0` apphost** (the `net10.0`
output hits the same `0x800700C1` bad-image-format wall described at the top of
this file), and an address landing in a `.data` region that reads as zero on disk
means the table is built at runtime — an answer, not a failure.

## 2026-08-14 — game state 4: the loader is in, the frame loop is a hole

`FUN_005066d0` (state 4, character select) is two halves:

```
FUN_005066d0
  ├── FUN_005066e0   the resource load          → reconstructed
  └── FUN_00506880   the frame loop             → not reconstructed
        └── (*PTR_LAB_00560708[DAT_00578de8])() → eight handlers, none in Blaze.c
```

### The loader — `LoadCharacterSelectResources`

Faithful transcription of `FUN_005066e0`. Same opening as `LoadMenuResources` (state entry, then
header relocation), followed by **four** directory lists instead of one: `+0x10` TIMs decoded into
emulated VRAM, `+0x2c` model bundles, `+0x7c` animation bundles, `+0xb0` raw blocks every `0x2000`
bytes. The `+0x2c` and `+0x7c` loops share one cursor from `MainMem + 0x138000`, so the bundles
stack without gaps.

Two deviations from the original, both commented at the call sites rather than left implicit:

- `null` guards on the directory and on `MainMem` that the original does not have.
- `FUN_0040a600()` is not transcribed. Its decompiled body is **empty** (`Blaze.c:4319`), so this
  is behaviourally neutral — but Ghidra's signature for it is wrong (other sites pass arguments).
  Recorded in `Documentation/Reference/DataSymbols.md`.

### Two globals were about to be misnamed

`DAT_005e96a0` and `DAT_005e9640` were first written as `s_SelectSpriteTable_*` /
`s_SelectPaletteTable_*`, as if they belonged to this screen. They do not: `FUN_005069e0` fills
them identically (`Blaze.c:78876`) and ~30 sites read them across `Blaze.c`. They are the global
**model** and **animation** tables — `DAT_005e9640` holds animations (frame counter wraps on the
entry's `+0x16` field, `Blaze.c:55840`; count read at `[-4]`, `Blaze.c:79042`), `DAT_005e96a0`
holds models (16-byte primitive records bucketed by type, vertex counts divided by three).
Renamed to `s_ModelTable_005e96a0` / `s_AnimationTable_005e9640` before the thirty read sites
arrive. Capacity is 24, not `0x40`: the two symbols are `0x60` bytes apart. Full layout in
`Documentation/Reference/DataSymbols.md`.

### The frame loop's eight handlers need a Ghidra re-run

`FUN_00506880` dispatches through `PTR_LAB_00560708`, an eight-entry table read out of
`Legacy/Binaries/Blaze.exe`:

| index | target | index | target |
|---|---|---|---|
| 0 | `0x00507010` | 4 | `0x00508E00` |
| 1 | `0x00507CB0` | 5 | `0x00509470` |
| 2 | `0x00508410` | 6 | `0x005098F0` |
| 3 | `0x00508860` | 7 | `0x0050A910` |

**The `PTR_LAB_…` blocker no longer applies** — the binary is in the repo and the section map is in
`DataSymbols.md`, so the table resolves by hand (file offset `0x15ED08`). What remains is the
*other* blocker class: none of the eight bodies is in `Blaze.c`. The decompilation jumps from
`FUN_00506880` to `FUN_00507870`, skipping them entirely, because nothing calls them directly.
A scoped Ghidra re-run at those eight addresses produces all of them at once.

Verified from the binary: all eight are genuine function prologues, and seven of the eight open on
`mov eax, [0x00562A48]` — the "first frame in this sub-state" flag that `FUN_005066e0` sets to 1
and that `FUN_00506880` re-raises on every sub-state change.

`FUN_00506880` itself is reconstructible today, with the eight handlers as named stubs, but two
things about it need flagging:

- Ghidra failed to recover its parameters: `unaff_EBX`, `unaff_EBP`, `unaff_ESI` are passed
  straight through to `FUN_0051d1f0`, `FUN_0051be20`, `FUN_00545340`, `FUN_00521f20` and others.
  Same re-run would fix this.
- It calls a long tail of not-yet-reconstructed rendering functions (`FUN_0051d1f0`,
  `FUN_0051be20`/`FUN_0051bdb0`, `FUN_00521df0`/`FUN_00521f20`, `FUN_0051f9c0`, `FUN_00545340`,
  `FUN_005443a0`, `FUN_00544350`, `FUN_0052a1e0`/`FUN_0052a280`, `FUN_00547140`, `FUN_00520ae0`,
  `FUN_00504b10`) plus `winsys3d` ordinals 376/378/379/457.

## 2026-08-14 — the Ghidra re-run happened, and it moved three walls at once

### How to re-run (the recipe, because it changed)

Ghidra is at `F:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC`, project
`F:\Open Foresia Project\Ghidra Project\Open Foresia`.

**The `.py` scripts in `~/ghidra_scripts` no longer run.** Ghidra 12 ships PyGhidra and demotes
Jython to an *uninstalled* extension zip (`Extensions/Ghidra/…_Jython.zip` is present but never
extracted). `ForceFunctions.py` and `ExportFunction.py` are therefore dead until Jython is
installed or they are ported. The replacement is a Java `GhidraScript`, which headless always
supports: `~/ghidra_scripts/ForceDecompileCharacterSelect.java`.

```powershell
$gh = "F:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
& $gh "F:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
      -noanalysis -readOnly `
      -scriptPath "C:\Users\Sagara\ghidra_scripts" `
      -postScript ForceDecompileCharacterSelect.java
```

Two things matter in that invocation:

- **Invoke it from PowerShell, not Git Bash.** The `.bat` mangles its own path when bash launches
  it through a directory containing spaces (`'F:\Open' n'est pas reconnu`).
- **`-readOnly` keeps the project untouched.** Functions are created in memory, decompiled, and
  discarded. Drop the flag only when the created functions are wanted permanently.

The script's own shape matters as much as the flags: it creates **all** the missing functions
first, then calls `analyzeChanges(currentProgram)`, and only then decompiles. Skipping the
`analyzeChanges` step is not cosmetic — on the first attempt the bodies came out riddled with
`func_0x0053db80(…)` (unresolved call targets) and several were 2–5× too short, because the newly
disassembled code's outgoing references had never been analysed. With the analysis pass:
`FUN_005098f0` went 713 → 3660 bytes, `FUN_0050a910` 2522 → 5425, and `func_0x…` dropped to zero.

Output lands in `Legacy/Raw/Blaze/FUN_<addr>.c` and `.asm`.

### The eight sub-states are decompiled

All eight entries of `PTR_LAB_00560708` now have bodies and listings in `Legacy/Raw/Blaze/`:

| index | address | pseudo-C lines |
|---|---|---|
| 0 | `0x00507010` | 303 |
| 1 | `0x00507CB0` | 285 |
| 2 | `0x00508410` | 143 |
| 3 | `0x00508860` | 178 |
| 4 | `0x00508E00` | 203 |
| 5 | `0x00509470` | 177 |
| 6 | `0x005098F0` | 542 |
| 7 | `0x0050A910` | 711 |

None is transcribed into `Blaze.cs` yet. `InvokeCharacterSelectSubState` throws with the address
and the file name so the next one to do is obvious.

### The handler signature, recovered from the disassembly

Ghidra's pseudo-C for the dispatch is **wrong** and would have produced a broken reconstruction:

```c
(**(code **)(&DAT_00560708 + DAT_00578de8 * 4))
    (unaff_EBP, unaff_EBX, unaff_ESI, &DAT_00604a20 + i * 4);
```

The listing at `0x00506a42`–`0x00506a52` shows four `PUSH`es and `ADD ESP,0x10` — cdecl, four
arguments — and the first three are **stack locals**, not registers. Tracing the offsets against
the frame (`SUB ESP,0x20` then four register pushes, so locals start at `E+0x10`):

| arg | slot | what it is |
|---|---|---|
| 1 | `[E+0x14]` | `FUN_0052a1e0`'s 1st out-param — `otBase` |
| 2 | `[E+0x18]` | its 2nd out-param — `otEntry` |
| 3 | `[E+0x10]` | its 3rd out-param — `primCursor` |
| 4 | `EAX` | the pad record the exit test just read |

and the return value is written back to `[E+0x10]`. So:

```
uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
```

The handlers corroborate it independently: `FUN_00509470` reads `*(ushort *)(in_stack_00000010 + 2)`
and masks button bits — `in_stack_00000010` is argument 4 at `[ESP+0x10]`.

This also decodes the `unaff_*` names inside the body, confirmed at two call sites each:
**`unaff_EBP` is `otBase`** (`FUN_00545340` at `0x00506e9d` and `0x00506f44` both take `[E+0x14]`)
and **`unaff_EBX` is `otEntry`** (`FUN_00521f20` at `0x00506f9e` takes `[E+0x18]`). `EBP` itself is
zeroed at `0x00506953` and used as a zero register for the rest of the function, which is what
confused the decompiler.

### A latent 4× addressing bug in already-shipped code

`CurrentPadWord()` computed `s_InputStateBase_00604a20 + slot * 4`. **The stride is `0x10`.** Every
call site disassembles to `SHL EAX,0x4` then `ADD EAX,0x604a20`:

| function | address |
|---|---|
| `FUN_0052a440` | `0x0052a60b` |
| `FUN_00545340` | `0x00545423` |
| `FUN_00506880` | `0x005069fa` |

Ghidra's pseudo-C loses the shift and prints `* 4` in all of them, which is where the reconstruction
got it. The bug is **latent**: at slot 0 — the single-controller case everything has been exercised
with — `0 * 4` and `0 * 0x10` are the same address, so nothing ever misbehaved. It would have
surfaced the moment a second pad slot was used. Now `CurrentPadState()` returns the record pointer
and `CurrentPadWord()` reads the halfword at its offset 0, which is the form `FUN_0052a440` uses
(`MOV AX, word ptr [EAX]`, then `AND AX,0x8008`). `FUN_00506880` instead reads the **byte at +2**
and masks `9`.

### What now runs, and what still stops

`RunCharacterSelectFrameLoop` is reconstructed end to end except for one branch. Reaching the
screen needs sub-state 0, and the `DAT_00578de8 != 0 && < 7` block — camera damping, the particle
loop through `FUN_00520ae0`, and the animated-model draw through `FUN_0051d1f0` — is **skipped at
sub-state 0**, so it is not on the critical path. It throws with an inventory rather than being
silently dropped.

`FUN_00521df0` was the last missing helper on that path and is now reconstructed as
`DrawSpriteVariant`: the five-word sister of `DrawSpritePrimitive` (tag `0x04000000` against
`0x09000000`), same header decode, one corner instead of four. Note its scale field treats 0 as
`0x100` where the long sister falls back to `0xff` — that asymmetry is in the original.

**Open question it surfaced**: `DrawSpritePrimitive` uses `s_SmallTextureFlag` where Ghidra has
`DAT_00564a4c`, but `s_SmallTextureFlag` is `DAT_00564a38` (`AU_00564a05._51_4_`) — two different
addresses. `DrawSpriteVariant` declares the proper `s_Unknown_00564a4c` rather than propagate the
conflation. Which of the two is right needs settling, and it affects the long sister too. There is
a second, unrelated duplication at the same address block: both `s_DeviceCapFlag_00564a48` (bool)
and `s_Unknown_00564a48` (int) exist for `DAT_00564a48`.

### The route to the screen

`RunGameStateTick`'s state 4 no longer throws — it calls `RunCharacterSelectState`. Two ways in:

- **From the main menu, entry index 1**: the already-reconstructed menu does `case 1: s_GameStateId = 4;`.
- `FUN_00536ea0` sets `DAT_005de510 = 4` when `DAT_00604980._1_1_ == 0` — no saved games, so the
  save-slot screen falls straight through to character creation.

The remaining work to actually see it is transcribing `FUN_00507010` (sub-state 0), whose
decompilation is now sitting in `Legacy/Raw/Blaze/FUN_00507010.c`.

## 2026-08-14 — sub-state 0: input, setup and the phase machine are in

`CharacterSelectSubState0` (`FUN_00507010`) is reconstructed up to its render pass.

**The carousel is confirmed geometry, not a guess.** The first-frame block places sixteen slots
with `Ordinal_451(i << 8) * -0x96` and `Ordinal_450(i << 8) * 0x96`. Ordinals 450/451 are libgte
`rcos`/`rsin` over a `0x1000`-step turn (`winsys3d.c` `FUN_00a25c20`/`FUN_00a25c40`, plain table
lookups at `DAT_011cb420`, with 452/453 aliasing the same entries). So `i << 8` is exactly one
sixteenth of a turn per slot and `0x96` = 150 is the radius: sixteen characters on a circle.

The slot stride is `0x9c`, corroborated three ways: Ghidra's `ushort *` step of `0x4e`, the
`0x27`-dword copy at the confirm branch, and the `* 0x9c` that `CurrentPadState` already uses on
`DAT_005e93c8`.

**The goto mesh collapses.** The phase machine's `LAB_00507438` / `LAB_0050743d` /
`joined_r0x00507423` tangle reduces to two cancel variants that differ only in which byte they zero
(`DAT_0056075b` vs `DAT_00560763`) and which bit of `DAT_00560fef` they clear (`0xfd` vs `0xfb`).
Rebuilt as a `switch` plus one shared tail flag.

**Two pad scales in one function.** Ghidra types the pad pointer `ushort *`, so
`in_stack_00000010[1]` and `[3]` are the words at byte 2 and byte 6, while
`*(byte *)((int)in_stack_00000010 + 3)` is byte 3 — the high half of the first. Both forms appear
in `FUN_00507010`. The reconstruction keeps them apart as `padWords[…]` and `pad[3]`.

### What sub-state 0 still needs

The render pass, from the `FUN_0051c300` call onward — the sixteen-slot damping loop, the model
draw and the two sprite passes. Four helpers, none reconstructed:

| function | role |
|---|---|
| `FUN_0051c300` | camera matrix from `DAT_00604928` into `DAT_006008e0` |
| `FUN_005352e0` | the copy used for both the slot freeze (`0x27`) and the transform (`10`) |
| `FUN_0051f650` | projection; its zero return culls the slot |
| `FUN_0051d1f0` | the animated model draw — also the blocker for `FUN_00506880`'s in-scene block |

`FUN_0051d1f0` is the one that matters: it is the single remaining dependency shared by both the
frame loop's in-scene branch and sub-state 0's carousel. `FUN_005352e0` is currently reconstructed
as `CopyDwords` **on the strength of its call sites only** — its body has not been read, and that
assumption is flagged inline.

### Two more symbol duplications found

Same class as the `DAT_00564a48` pair noted above: `DAT_005633a8` has both
`s_Unknown_005633a8` and `s_UnknownModeFlag_005633a8`. Sub-state 0 uses the latter. These want a
sweep at some point — two names for one address is a drift waiting to happen.

## 2026-08-14 — `FUN_0051d1f0`: the blocker was the GTE register map, and it is now mapped

`FUN_0051d1f0` is 689 lines and is **not** reconstructed. What stopped anyone reading it was not
its length: its dependency surface is **sixteen distinct data exports** of `winsys3d.dll`
(`Ordinal_801_exref` … `Ordinal_821_exref`), none of which was documented, plus five functions.
Without knowing what those addresses are, the body is unreadable.

They are now identified — the emulated **GTE register file**, exported as data. Full map, three
independent cross-checks and the C# access convention are in
`Documentation/DLL/winsys3d.md`, section "The emulated GTE register file is exported as DATA".

### Getting the export table when `OpenForesia.Tools` will not run

`OpenForesia.Tools` is pinned to `PlatformTarget=x86`/`Prefer32Bit=true` and **cannot run on this
machine** — only the x64 .NET 10 runtime is installed, so the apphost fails with
`hostfxr` `0x800700C1`. Overriding `-p:PlatformTarget=x64` on the command line does not help:
`Prefer32Bit` still stamps the 32-bit flag on the assembly.

Rather than change the project, the export table was dumped with a standalone PowerShell PE
parser, kept at `scratchpad/exports.ps1`:

```powershell
& exports.ps1 -Path winsys3d.dll -From 790 -To 825 -ImageBase 0x00a00000
```

It prints ordinal, RVA, VA and section, and resolves names where they exist. Worth folding into
`OpenForesia.Tools` — or fixing that project's architecture — at some point.

### Reconstructed on the way

Three small consumers, all complete:

| C# | Ghidra | note |
|---|---|---|
| `RotMatrix` | `FUN_0051c300` | nine coefficients verified one by one against the pseudo-C |
| `NormalClip` | `FUN_0051c280` | six terms verified against the SDK formula |
| `ReadMac0` | `FUN_0051c2f0` | trivial |

`RotMatrix` matters beyond `FUN_0051d1f0`: sub-state 0 calls it too, so
`CharacterSelectSubState0` now runs one step further — through the camera matrix and
`SetBackColor` — before throwing at its sixteen-slot render loop.

### A duplicate P/Invoke, caught before it landed

Ordinals 450/451 were already declared as `GetCosTableEntry`/`GetSinTableEntry`. A second pair of
`Ordinal_450`/`Ordinal_451` declarations was added while reading `FUN_00507010` and then removed;
the libgte identification was folded into the existing declarations instead. This is the same
drift class as the `DAT_00564a48` and `DAT_005633a8` duplicate pairs already noted — the file is
big enough now that grepping for the address before declaring a symbol is worth doing every time.

### What `FUN_0051d1f0` still needs

The register map removes the blocker; the work itself remains. Its function dependencies are
`FUN_005327b0`, `FUN_0051e700` and the three above, and it touches ordinals 805–811, 815, 817 and
819–821 — so transcribing it should also pin down most of the eleven still-unidentified exports.
It stays the single shared dependency of both the frame loop's in-scene branch and sub-state 0's
carousel.

## 2026-08-14 — `FUN_0051d1f0` fully mapped, not yet written

Every dependency is now identified and every branch understood. The transcription itself has not
been started, deliberately — see the closing note.

### The seven winsys3d entry points it needs

| ordinal | `winsys3d.c` | role |
|---|---|---|
| 360 | `FUN_00a20630` | composes two matrices; own MMX/scalar split |
| 418 | `FUN_00a22010` | batch `RotTransPers`: N vertices through the MATRIX register into screen-XY and depth arrays |
| 793 | `FUN_00a03e70` | batch lighting; gated on the DLL's `Elo_NoLighting`, writes zeroes when set. Consumes `EXT_00b6d198` = data export **817** |
| 794 | `FUN_00a20540` | libgte `ScaleMatrix` — scales each matrix column by one scale component |
| 795 | `FUN_00a226c0` | the depth-less sibling of 418 |

Plus the GTE register file (801/802/805–808/813/814/815/817) already documented.

### Structure

```
FUN_0051d1f0(otBase, prim, model, entity, transform, boneIndices, colourArg)
  |
  |-- prologue: copy DAT_006008e0.. into the MATRIX register (814), build the input
  |   vector (802), do the 3x3 multiply by hand into MAC (801) and IR (813),
  |   RotMatrix the entity pose, ScaleMatrix it (794)
  |
  |-- per-bone loop, `*(ushort*)(model + 0x12)` iterations:
  |     RotMatrix each bone, ScaleMatrix (794), compose into MainMem + 0x1ba000 (360)
  |
  `-- main loop, `*(ushort*)(model + 0x14)` iterations:
        |-- matrix concat, TWO PATHS on MMXInstalled_exref:
        |     scalar: nine explicit `>> 0xc` dot products
        |     MMX:    three `pmaddwd` triples over the same operands
        |-- load result into MATRIX (814)
        |-- transform + light, on the model's 0x400 flag (field +0x3c):
        |     clear -> Ordinal_418 (perspective, emits per-vertex depth) + Ordinal_793
        |     set   -> Ordinal_795 (no depth)
        `-- emit primitives from two buckets, counts at model +0x1a and +0x1e:
              +0x1a -> 10-word textured quad, tag 0x9000000, subtag 0x2c000000
              +0x1e -> 13-word Gouraud quad,  tag 0xc000000, subtag 0x3c000000
```

**The 0x400 flag drives two complete regimes**, which is what makes the function look like it has
four near-identical loops. It does not: it has two, written twice.

- **clear** — per-polygon depth sort. The OT slot is the max of the four vertices' depths read
  from `MainMem + 0x1b4000`, clamped by `slot - 0x50 < 0xec0`, linked at `otBase + (slot & 0xfffc)`.
- **set** — one fixed slot for the whole model, `otBase + sStack_38 * 4`, computed once. No max, no
  clamp. Billboard/flat geometry.

Both regimes run the same backface test first: the six-term cross product is **hand-inlined**,
character for character the same expression as `FUN_0051c280`, then `FUN_0051c2f0` re-reads MAC0.
That inlining is independent confirmation that the `NormalClip` reconstruction is right.

Two guards bound the work per call: a vertex is skipped unless one of the four screen X values is
below `0x140`, and the global budget `DAT_005e9818` decrements per emitted primitive and stops the
loop at zero.

### Two things to settle before trusting a transcription

1. **The MMX path.** `MMXInstalled_exref` selects between nine scalar dot products and three
   `pmaddwd` triples. A reconstruction that only takes the scalar path is behaviour-preserving
   *if* the two agree — which is the point of a CPU-capability branch — but that has not been
   verified operand by operand, and it is a stated deviation either way. Writing the MMX path in
   C# intrinsics would be no more faithful and much harder to check.
2. **`FUN_005352e0`.** Still reconstructed as `CopyDwords` from its call sites alone; its body has
   not been read. It is used by sub-state 0 as well.

### Why it is not written yet

This is the largest function in the reconstruction so far — ~690 lines of pseudo-C emitting
PlayStation display-list primitives with exact bit packing, on top of seven DLL entry points that
were undocumented at the start of this session. The project already has one worked example of what
rushing exactly this kind of code costs: `DrawFlatPrimitive`'s `*prim = *otEntry | 0x2000000` was
transcribed faithfully and still crashed, because the convention it belonged to lived on the DLL
side. A day went to it.

The map above is the part that could not be recovered by reading harder, and it is now recorded.
The transcription is a focused sitting of its own, and it is no longer blocked on anything.

## 2026-08-14 — `FUN_0051d1f0` is written: `DrawAnimatedModel`

The animated-model draw is reconstructed end to end, in four pieces:

| C# | covers |
|---|---|
| `DrawAnimatedModel` | prologue, bone loop, part loop, matrix concat |
| `PartIsOnScreen` | the `local_94` branch — per-part frustum cull |
| `DrawModelPart` | `LAB_0051db61` — transform, lighting, bucket dispatch |
| `EmitModelQuads` | the four emission loops, which are two written twice |
| `ScaleMatrixSaturating` | `FUN_005327b0`, the hand-rolled `ScaleMatrix` |

### What the asm settled that the pseudo-C could not

- **The depth clamp.** Ghidra renders it as `puVar20 + -0x50 < (uint *)0xec0` — pointer arithmetic
  on a `uint *`. Taking `0x50` literally would have been wrong by a factor of four. The listing at
  `0x0051e32a` is `LEA ECX,[EAX + 0xfffffec0]` then `CMP ECX,0xec0` unsigned, so the test is
  `(uint)(zmax - 0x140) < 0xec0`. The OT slot immediately after is `otBase + (zmax & 0xfffc)` as a
  **byte** offset, which is what the pseudo-C shows.
- **`FUN_005352e0` is confirmed.** Its 32-byte body is a plain dword copy,
  `(source, destination, count)` — the `CopyDwords` assumption from the previous session was
  right, and is now a fact rather than an inference.

### Two findings that reach back into existing code

**`DAT_005e9818` is the per-frame primitive budget, and `FrameBegin` already resets it to
`0x9c4`.** It is never assigned anywhere in `Blaze.c`; its address is in `.data`'s uninitialised
tail, so it loads as zero. Had `FrameBegin`'s reconstruction not already carried that line, every
emission loop here would have exited immediately and nothing would ever have drawn. Now labelled.

**`DAT_006008fc` is not a standalone variable.** It is the Z translation of the camera matrix at
`DAT_006008e0` — a single 0x20-byte `MATRIX`, nine rotation shorts then three translations at
`+0x14`/`+0x18`/`+0x1c`. The frame loop's `DAT_006008fc = DAT_006008fc + (800 - DAT_006008fc >> 2)`
is the camera dollying in. It had been declared as an isolated `int`, which would have made
`DrawAnimatedModel` copy a half-wrong matrix into the GTE register. Same structure-split class as
the menu background bug. Now one block with a `CameraTranslation` accessor.

### Structure worth remembering

- The **0x400** bit of the model's `+0x3c` picks the regime: clear → perspective transform
  (ordinal 418, emits per-vertex depth) and per-polygon depth sort; set → ordinal 795 and one
  fixed OT slot for the whole part.
- **Bit 0** of the transform block's `+0x1a` is not that — it enables the **per-part cull**, which
  projects the part origin (a zero vector, so only the translation survives) through the
  reciprocal table at ordinal 821 and tests screen bounds. Side effect: the projected depth,
  divided by four, becomes the fixed OT slot.
- **Bit 2** of the same field replaces lighting with a flat colour written three times per triangle.
- `param_4` advances per part: **each part has its own model entry**. But the texture-page words
  are computed once from the *first* part's model and reused — an original quirk, transcribed.
- The per-part matrix goes into register 814 (transform) while the scene×bone product goes into
  815 (normals). 815's translation row is never rewritten by that product and keeps whatever the
  bone loop last left; harmless, lighting only needs the rotation. Transcribed as-is.

### Stated deviation

Only the **scalar** matrix-concat path is transcribed. The original branches on
`MMXInstalled_exref` between nine dot products and three `pmaddwd` triples. A CPU-capability
branch should compute the same thing, but that has **not** been verified operand by operand.
Marked `TODO(REVERSE)` at the site.

### A third duplicate symbol, caught the same way as the previous two

`s_PrimitiveBudget_005e9818` was declared before noticing `s_Unknown_005e9818` already existed;
removed. That is three in three sessions (`DAT_00564a48`, ordinals 450/451, now this). The file is
past the size where a new symbol should be declared without grepping its address first.

### What is left for the character-select screen

Neither render path is fully open yet, and the remainders are now small and named:

- **Sub-state 0's carousel** needs `FUN_0051f650`, the per-slot projection whose zero return culls
  the slot. It is decompiled (`Legacy/Raw/Blaze/FUN_0051f650.c`), has the same lost-parameter
  problem, and pulls in two more unidentified ordinals, **366** and **369**.
- **The frame loop's in-scene branch** needs `FUN_00520ae0` (the particle pass) and the
  `0x00572b40` globals.

Nothing has been run. No test of the game has been attempted this session.

## 2026-08-14 — `FUN_0051f650` and the carousel: sub-state 0 no longer throws

### `ProjectPoint` (`FUN_0051f650`)

Loads the camera matrix into the GTE register through **ordinal 366 (`SetRotMatrix`)** and
**ordinal 369 (`SetTransMatrix`)** — both write into the MATRIX register at ordinal 814, the
rotation at +0 and the translation row at +0x14, which is what confirms them. It then transforms
a world position, makes that the matrix's translation, and projects the origin.

That last step is character-for-character the projection `FUN_0051d1f0`'s cull branch already
does, so the two now share `ProjectOriginThroughMatrix`.

Parameters came from the listing, Ghidra having lost the frame and rendered the output as
`unaff_retaddr`: arg1 at `[ESP+0x1c]` is the position, arg2 at `[ESP+0x20]` is the output buffer
(written at `0x0051f8e5`). Returns 1 when the projected point is on screen.

Ordinal 803 is identified in passing as the second input vector register.

### The carousel is wired

`CharacterSelectSubState0` now has **no `throw` left**. Its sixteen-slot loop damps each slot
(the selected one straightens and lights up, the others tilt away and dim — all by quarters),
advances the animation frame with wraparound on the animation's `+0x16` length, draws the model
through `DrawAnimatedModel`, then projects the slot position raised by `0x3c000` and, if it lands
on screen, draws its label twice one pixel apart.

Every offset in that loop was verified against `FUN_00507010.asm` rather than taken from the
pseudo-C, because Ghidra walks the slot through a `short *` parked 0x2a bytes past the base and so
renders every field as a negative index. Recentred on the slot base, they line up exactly with the
first-frame init block written in the previous session — which is a good independent check that
both readings are right. In particular the transform passed to the model draw is `slot + 0x1c`,
which is precisely where that init writes the circle position.

Two details the listing settled and the pseudo-C could not:

- The label's screen position is the projection output's x **and** y (slot struct `+8` and `+0xa`),
  written from `[ESP+0x1c]` and `[ESP+0x22]`. Ghidra folds both into one `unaff_EBP`.
- The label's OT slot is `otBase + projectedZ + 0x14` — from `LEA EDX,[EBX + ECX*0x4 + 0x50]`,
  where `EBX` is the handler's first argument.

### `DAT_005606f8`

Sixteen bytes at file offset `0x15ECF8`: `{0x0000000c, 0, 0x005066c0, 0}`, sitting immediately
before `PTR_LAB_00560708`. The third field is a **code pointer**, `0x005066c0` — right before
`FUN_005066d0` and, like the eight sub-states were, absent from `Blaze.c`. Modelled with the
recovered bytes. If `BindMenuLabels` ever calls through it, that will surface at runtime and want
one more scoped Ghidra re-run.

### State of the path to the screen

```
RunGameStateTick case 4
  -> RunCharacterSelectState
       -> LoadCharacterSelectResources      complete
       -> RunCharacterSelectFrameLoop       complete except the in-scene block
            -> CharacterSelectSubState0     complete, no throws
                 -> DrawAnimatedModel       complete (scalar matrix path only)
                 -> ProjectPoint            complete
```

The two `throw`s left in the frame loop are the null-`MainMem` guard and the in-scene block, which
only sub-states 1–6 reach. Sub-state 0 is the entry sub-state, so **the screen is reachable on
paper for the first time**.

Nothing has been run. This is the point at which running the game is worth doing: main menu,
entry 1.

## 2026-08-15 — the first run of state 4, and what it actually said

The screen was reached and it threw. Two defects behind it, both now fixed, and one reported
symptom that turned out not to be a defect at all.

### The UI descriptors carry an ordering table, so they must live low

The exception was the guard in `RunTextEngine`:

```
FUN_005443a0 masks its descriptor to 24 bits, but this one is at 0x02D2C3B8.
```

The obvious reading — "the parameter is masked, so keep the argument low" — is true but shallow,
and taken alone it invites fixing only the symbol that happens to throw. The real constraint is
one level down: **the descriptor contains its own ordering table at `+0xa4`**. `FUN_005443a0`
clears it with `ClearOTagR` on control code `0x0c`, which stores every link as
`address & 0xffffff`; `FUN_00545340` (`DrawUiPanel`) then walks it under the same mask until the
`0xffffff` sentinel. A descriptor above `0x01000000` therefore relinks its own text list onto
truncated addresses, and the parameter mask is just the first place that shows.

In the original the question never arises: these are `.data` globals of an image based at
`0x00400000`. It only arises here because the reconstruction models `.data` globals as
allocations.

Converted to `AllocLowZeroed`, as lazy properties rather than field initialisers — the reason is
the one already written on `s_MenuContext_0056dd58`: a field initialiser turns a lost race for low
memory into `TypeInitializationException`, outside the module initializer's `try`/`catch` and so
not recoverable by relaunching. They are also claimed eagerly in `TryPrepareDisplayListArena`,
next to the menu descriptor, for the same measured reason recorded there.

- `s_UiDescriptors_00560ff0` — three instances, passed to `RunTextEngine` and `DrawUiPanel`.
- `s_StatusDescriptor_00560728` — same, from the frame loop.
- `s_SelectSlots_00573518` — nine instances of the same type. No transcribed sub-state passes one
  to the text engine yet, but they are the same struct with the same `+0xa4`; twenty kilobytes of
  the low window now beats finding this again from sub-state 3.

### What did *not* need converting

The evening's note listed nine symbols "to convert by coherence". Six of them do not need it, and
converting them would have spent low memory on nothing:

| symbol | why not |
|---|---|
| `s_Unknown_005606f8` | a label table. Stored at `descriptor + 0x8ac` and read back as a **full 32-bit pointer** (`Blaze.c:91094`). `s_MenuLabels_0056e670` has always been an ordinary allocation and the main menu draws. |
| `s_CameraMatrix_006008e0` | copied **by value** into the GTE matrix register; its address is never stored anywhere. |
| `s_CharacterSlots_00578420` | `slot + 0x1c` reaches `DrawAnimatedModel` as a vector source, not as a link. |
| `s_ModelTable_005e96a0`, `s_AnimationTable_005e9640` | only their contents are dereferenced. |
| `s_SelectSubStateBytes_00562a98`, `s_CameraVector_00604928` | plain data. |

The usable rule is not "does this block end up near display-list code" but **"is this block a
`0x8c8` descriptor that traverses `FUN_005443a0` / `FUN_00545340`"**.

### `BuildUiDescriptors` was writing 8 of its 26 dwords

Found while checking the first defect, and it would have produced a blank screen right after
fixing it — the same failure mode already recorded for `DAT_0056dd58`.

The initialised prefix of one of these structures is `0x68` bytes = 26 dwords, and the builder
carried only the first 8. Everything from `f8` on was zero, including:

- `f8` = `0x01070C10`, the **text** feature word;
- `f16` = `0x010F0000`, the **frame** feature word.

Every feature test in `DrawUiPanel` is `field & 0x1000000`, so a descriptor missing those draws
neither text nor frame. `BuildStatusDescriptor` had the same shape of gap, one dword wide: `f25`.

All four instances are now written from the binary, and the transcription was verified
mechanically rather than by eye — the dwords in `Blaze.cs` were extracted and diffed against
`od` output at file offsets `0x15ED28`, `0x15F5F0`, `0x15FEB8` and `0x160780`: 78/78 and 26/26
identical. The full table is now in `Documentation/Reference/DataSymbols.md`, along with the reading of the
odd dwords as *enable* halves of *(parameter, enable)* pairs.

### The frozen `DrawPrimitive` counter was a misreading

The evening's note flagged this as a second defect to investigate:

```
[SHIM] bilan : ... BeginScene=361 EndScene=360 DrawPrimitive=34545
[SHIM] bilan : ... BeginScene=421 EndScene=420 DrawPrimitive=34545
```

Sixty frames with nothing drawn. But `ShimReport` fires every 60 `Flip`s (`DeviceShim.cs`), and
the stack trace shows the loop dying in the **first** frame of sub-state 0 — it never ran sixty
times. Those sixty silent frames are the resource-loading phase, not the render loop. Putting a
trace on `EmitModelQuads` and `s_Unknown_005e9818` before re-running would have been instrumenting
a hypothesis that cannot be true.

To re-measure after the fix, not before.

### State

`dotnet build` is green. The startup low-memory claim now includes the three descriptor blocks and
succeeds (no relaunch on the run tested). What has **not** been verified is the screen itself:
reaching it needs manual navigation through the main menu, which no environment switch shortcuts.
That is the next thing to do, and the `[SHIM] bilan` line is the measurement to read once there.

## 2026-08-15 — the character-select screen draws

It runs. Reached from the main menu, 2400+ frames without a fault, and the capture shows the
carousel of sixteen portraits, the animated model, the "Classe / Guerrier" panel and the
"Sélectionne un personnage." banner. Both text panels rendering is the direct confirmation that the
26-dword header fix was the right one — a descriptor missing `f8`/`f16` would have drawn neither.

Three defects stood between the first run and this, and only the first was known in advance.

### 1 and 2 — the descriptors (see the entry above)

Fixed before the run: low allocation, and the truncated `0x68`-byte header.

### 3 — ordinal 795 was declared with two arguments and takes three

With the descriptors fixed the loop got as far as the carousel's first model draw and took a native
access violation:

```
Fatal error. 0xC0000005
   at Ordinal_795(Void*, Void*)
   at DrawModelPart(...)
   at DrawAnimatedModel(...)
   at CharacterSelectSubState0(...)
```

`FUN_00a226c0` is `void f(vertices, outScreenXy, count)`. The P/Invoke had only the first two, so
cdecl handed the DLL whatever the caller had left on the stack as its count, and it wrote through
it. The full reading, including why the pseudo-C reads as a two-argument function if you miss the
swap in its scalar path, is now in `Documentation/DLL/winsys3d.md`.

This is the third time Ghidra's parameter recovery has been wrong on a call this screen makes —
`FUN_0052a1e0` and `FUN_0052a280` were the other two, both settled from the disassembly. The
pattern is worth naming: **a native fault in a winsys3d call is an arity question before it is
anything else.**

### The `DrawPrimitive` counter, re-measured

The previous entry predicted that the frozen counter was a loading-phase artifact. Confirmed:

```
[SHIM] bilan : ... BeginScene=241  DrawPrimitive=23330
[SHIM] bilan : ... BeginScene=301  DrawPrimitive=46270
[SHIM] bilan : ... BeginScene=361  DrawPrimitive=69210
```

About 382 primitives per frame, steady. No investigation of `EmitModelQuads` or the
`s_Unknown_005e9818` budget was needed.

### Driving the game without hands, for future runs

Reaching state 4 from a script is not obvious and cost several attempts, so the working recipe:

- The keyboard goes through **DirectInput**, so `SendKeys` (window messages) is invisible to it.
  `SendInput` with `KEYEVENTF_SCANCODE` works.
- The default bindings that matter are the **arrow keys** (`DIK_DOWN` `0xd0`) and `DIK_RETURN`
  (`0x1c`). The numpad half of the default table is not live — `OpenForesia.cfg` overrides it.
- The menu runs at roughly 6 frames per second here, and it polls once per frame. A press shorter
  than ~200 ms falls between two polls and is lost; a press longer than ~450 ms auto-repeats and
  the cursor runs away. **The first press after `SetForegroundWindow` is always swallowed.**
- Timing the press is therefore hopeless. What works is closed-loop: run with
  `OPENFORESIA_TRACE_MENU=1`, tap, then read `+0x8C4:xx->yy` back out of the redirected stderr
  (opened `FileShare.ReadWrite`, the game still holds it) and tap again until the cursor is on the
  wanted entry. Entry 1 is "Sélection des personnages".
- `CurrentPadWord()` keeps returning the last non-zero word after the key is released, so the pad
  value in the trace is not a reliable "is it held" signal — the `+0x8C4` transitions are.

## 2026-08-15 — validating a character: control code `0x0f`

Pressing Enter on the carousel threw the text engine's "unreconstructed control code" guard:

```
UI text control code 0x0f dispatches to FUN_00544e50, which Ghidra never emitted
   at BuildTextTable lambda -> RunTextEngine -> RunCharacterSelectFrameLoop
```

Validating a character rebinds the status descriptor to a different label stream, and that one
opens with control code `0x0f`.

### It did not need a Ghidra re-run

The guard's message says "needs a scoped Ghidra re-run", and that is the right default for this
class — but it is worth checking the body's size first. `0x00544e50` is **33 bytes**, decodable by
hand exactly like the four scan stubs and `0x00544b10` before it. Full listing in
`Documentation/Reference/DataSymbols.md`; the reconstruction is `Handler_00544e50`.

Registered in **both** tables: slot 15 holds `0x00544e50` on the scan side as well as the render
side, unlike the codes whose pass 1 substitutes an advance stub.

### It confirms the (parameter, enable) reading of the header

The two byte writes in these handlers are not loose flags. `[+0x33] = 1` and `[+0x3b] = 1` are the
**top byte of a feature dword** — `0x000F28D4` → `0x010F28D4` — i.e. the `0x1000000` enable bit that
every `DrawUiPanel` test reads. So `0x0f` **switches the cursor on** and `0x09` switches the scroll
arrows on, which is the same structure the 26-dword header established this morning, arrived at
from the opposite direction. Two independent readings agreeing is worth more than either alone.

Semantically `0x0f` means **stop here and show the cursor**: flag bit 1 makes every later
`FUN_005443a0` call return at once, so the descriptor freezes with a selection cursor displayed —
a prompt awaiting a choice, which is exactly what validating a character opens.

### Where the screen stops now

Sub-state 0 hands over correctly; the loop advances to **sub-state 1** and stops on the gap that was
already documented:

```
RunCharacterSelectFrameLoop: sub-state 1 needs the in-scene block of FUN_00506880.
What remains is FUN_00520ae0 (the particle pass) and the 0x00572b40 globals.
```

Three pieces, all readable, none blocked on a re-run:

1. **The in-scene block of `FUN_00506880`** — camera damping on `DAT_00572b64..74`, the particle
   loop, then the model draw (already `DrawAnimatedModel`). Readable in
   `Legacy/Raw/Blaze/FUN_00506880.c` around lines 128–210, plus the `0x00572b40` globals to model.
2. **`FUN_00520ae0`** — the particle pass. Its body **is** in `Blaze.c` at line 69030, but Ghidra
   gives it **three** parameters where every call site passes **five**
   (`FUN_00520ae0(iVar13 + 0x14, uVar12, piVar1, 0, 0)`). Same arity class as ordinal 795 and as
   `FUN_0052a1e0`/`FUN_0052a280`: settle it from the disassembly before transcribing, not from the
   pseudo-C.
3. **Sub-state 1** — `Legacy/Raw/Blaze/FUN_00507cb0.c`, 284 lines.

### Tooling

`playtest.ps1` gained `-Then`, a key sequence played once the screen is reached
(`-Then ENTER` reproduces this crash in one command). Writing it also caught a real trap: `-Build`
was leaving `DOTNET_ROOT` pointing at the x64 SDK, and the **x86** game then died on
`Failed to load hostfxr.dll … 0x800700C1` before opening its window. The build environment is now
restored before launch. Both are recorded in `test-protocol.md`.

## 2026-08-15 — the in-scene block and sub-state 1: the name-entry screen draws

Three pieces were outstanding. Two are done and running; the third is scoped down to a single
function that genuinely needs a Ghidra re-run.

### The "0x00572b40 globals" do not exist

`FUN_00506880`'s in-scene block reads `DAT_00572b4c`, `52`, `60`, `62`, `64..6c` and `70..74`, and
the previous entry listed modelling them as work. They are all fields of
**`s_SceneEntity_00572b48`**, at `+0x04`, `+0x0a`, `+0x18`, `+0x1a`, `+0x1c..+0x24` and
`+0x28..+0x2c`. Six offsets, six matches against the 0x9c-byte record a carousel slot already uses —
which is exactly right, since validating a character copies the chosen slot into this one. Nothing
new to declare.

The same held for sub-state 1: `DAT_00578df4/df8/dfc/e00` are `s_SelectState_00578de8[3..6]`,
`DAT_0057351c..3c` are fields of the **first** descriptor of `s_SelectSlots_00573518`,
`DAT_00572b7a` is the scene entity's yaw at `+0x32`, and `DAT_0060492a` is
`s_CameraVector_00604928 + 2`. Only two genuinely new symbols turned up, both tiny.

Worth generalising: **a `DAT_` cluster inside a known record's span is a field, not a global.**
Subtracting the nearest declared base first would have saved the detour both times.

### The particle pass is gated to sub-states 5 and 6

`FUN_00520ae0` sits inside `if (4 < DAT_00578de8)`. Sub-state 1 never reaches it, so it never
blocked the path the way the previous entry assumed. The loop is transcribed —the bounded random
walk on three axes, ±0x2000 per step saturated at ±0x19000, then position integrating velocity —
and only the draw call throws.

### `FUN_00520ae0` needs a re-run with its signature forced

Settled from the disassembly, not from the pseudo-C:

- The call site pushes **five** arguments (`PUSH EBP, PUSH EBP, PUSH EDI, PUSH EDX, PUSH EAX`) and
  cleans with `ADD ESP,0x14`. cdecl, so `(otBase + 0x14, primCursor, particle, 0, 0)`.
- The prologue is `SUB ESP,0x18` plus four `PUSH`es = `0x28`, putting the arguments at
  `[ESP+0x2C..0x3C]`. A byte scan for `MOV r32,[ESP+disp8]` shows **all five slots are read**,
  including `[ESP+0x38]` and `[ESP+0x3C]`.
- Ghidra declares three parameters and renders the primitive cursor as `unaff_retaddr`, writing a
  10-dword primitive through it and returning `+10`.

So the body is not transcribable as it stands: the two extra parameters are live and unattributed.
This is the one place in the three pieces where a scoped Ghidra re-run, with the signature forced to
five arguments, is the right tool rather than more reading.

### Sub-state 1 — `FUN_00507cb0`, the name-entry screen

Runs. The capture shows the virtual keyboard (upper case, lower case, accented rows, `FIN`), the
"Nom" box with its `_` cursor, and the status banner. `DrawPrimitive` goes from ~382 to ~593 per
frame, which is the eight grid rows arriving.

Its signature was lost by Ghidra the same way the dispatch table's was, and recovered the same way:
prologue `SUB ESP,0x28` plus four `PUSH`es = `0x38`, so arguments sit at `[ESP+0x3C..0x48]`.
`[ESP+0x3C]` is Ghidra's `uStack_8` (otBase), `[ESP+0x44]` its `unaff_retaddr` (the primitive
cursor, eight accesses), `[ESP+0x48]` its `param_1` (the pad). Bare `RET` confirms cdecl.

**It closes an open TODO.** `FUN_00507870` and `FUN_00507890` — the fixed-width and proportional
glyph-width table setters — were recorded as having no call site anywhere in `Blaze.c`, so which one
ran when was unknown. This sub-state calls **both**: fixed-width immediately before laying out the
eight-row grid, proportional immediately after. The grid needs equal cells to line up. The table is
therefore mutable, not the constant it had been modelled as.

Two new symbols, both recovered from the binary:

- `DAT_00562c98` — the string `"_"`, appended to the name while the cursor is not on the `FIN` cell.
- `DAT_00562c9c` — the bytes `81 40`, i.e. Shift-JIS `0x8140`, the ideographic space, read back as
  the dword `0x4081`.

`TODO(REVERSE)` left on the second: the original compares a **byte** against
`DAT_00562c9c & 0xffff` = `0x4081`, which a byte cannot equal, so that branch never fires as
written. It is most likely a two-byte read Ghidra narrowed. Transcribed as-is rather than repaired
on a guess.

Also settled: `FUN_00523520` and `FUN_00523530` are one-liners setting `DAT_00564a50` to 1 and 0 —
the gate on `FoldKeyboardIntoPadState`. The screen clears it while raw characters are being typed so
the pad mapping does not fight the keyboard, and restores it on the way out. `EnableKBInput` and
`GetKBChar` are winsys3d named exports (ordinals 775 and 778), a ring buffer of typed characters;
`GetKBChar` returns `undefined1`, i.e. a byte.

### One thing to check on the capture

The grid cursor sprite draws as a dark disc over the first cell, hiding the `A`. Its position is
transcribed straight from the disassembly —
`x = box+0x04 + column * 16 + (column < 5 ? 0 : 8) + 0x18`, `y = box+0x06 + (row + 1) * 12` — and
its texture is `**(directory + 0x94)`, its `+0x10` field `0x18`, both as the machine code builds
them. Whether that is the intended marker or a wrong sub-image is not established; it is the first
thing to look at if the grid highlight turns out wrong.

## 2026-08-15 — sub-state 3, and the typed-character queue that was blocking everything after it

Sub-state 3 runs: the speech-style grid (Neutre / Courtois / Suranné / Enfantin over Excité /
Flegmatique / Direct / Taciturne), the name box showing the loaded default, the substituted panel
under it, and the banner "Sélectionne le style de langage.". Two prerequisites had to be solved
first, and both are more interesting than the sub-state itself.

### The name screen had no input at all, and it was architectural

Sub-state 1 disables the pad→keyboard mapping (`DAT_00564a50 = 0`) and reads only `GetKBChar`. That
queue is filled by `FUN_00a03f50` in winsys3d — and **that function is not exported**: a scan of all
830 export RVAs finds no entry for its RVA `0x3f50`. It is reached only from winsys3d's own window
procedure, on `WM_CHAR`.

In this port the window belongs to `D3D11Window`, so that procedure never runs. Calling the two
exported halves (`EnableKBInput`, `GetKBChar`) therefore left the queue permanently empty, and with
the pad mapping switched off the name screen accepted **nothing**. Measured: the name box stayed
empty and no key advanced the state.

The queue is eight lines and fully readable, so it is now reconstructed in `Blaze.cs` and fed from
our own `WM_CHAR`. That is a deliberate deviation from "call winsys3d where winsys3d does", and the
only one of its kind; the alternative was calling an internal address at `base + 0x3f50`, which
assumes a stable non-exported entry point. Both halves and the writer are transcribed verbatim from
`winsys3d.c`.

This is worth generalising: **a winsys3d function with no export and no caller in its own
decompilation is fed by the DLL's window procedure**, which this port does not run. Any other
feature that depends on one will fail the same silent way.

### Control code `0x1c` — a subroutine jump inside the text stream

`FUN_00544e80`, 0x82 bytes at file offset `0x144280`, decoded by hand like `0x0f` before it.

It pushes `text + 3` onto the descriptor's stack at `+0x8b4`, bumps the depth counter at `+0x15`,
and returns a **different** string. The matching pop was already reconstructed — `RunTextEngine`
unwinds that stack on a NUL — so only the push side was missing, which is why the stack had looked
write-only.

The string is chosen by `text[0]` (which resource root), `text[1..2]` (a little-endian index into
its `+0x70` label table) **plus a substituted value**:

```
MOV ECX,[0x006087B4]   ; an IAT slot, so *(*(…)) is winsys3d's exported MainMem
SHL EDX,0xD            ; owner[8] * 0x2000
MOV DL,[EDX+ECX+0xF0153]
```

`MainMem + 0xf0000` is `DAT_00573338`, and `+0x153` is exactly the byte sub-state 3 writes. So the
code substitutes the chosen speech style from the player's own 0x2000 block, selected through the
panel's owner. That owner path (`*owner & 0xfc0000 == 0x800000`) is the one `DrawUiPanel` had
modelled but never exercised — sub-state 3 is the first caller to pass `&DAT_00572b48` as
`BindMenuLabels`' flags argument, which is what puts the scene entity at `descriptor + 0x8b0`.

### Sub-state 3 itself

`FUN_00508860`. Signature recovered at the prologue as before (`SUB ESP,0x14` + three `PUSH` =
`0x20`, so arguments at `[ESP+0x24..0x30]`). Ghidra declares it `void`; the epilogue leaves the last
`FUN_00545340` result in `EAX`, so it does return the primitive cursor.

`DAT_00573de0` is `DAT_00573518 + 0x8c8` — the second descriptor of the same array, not a new
symbol. `DAT_00562b70` is sixteen bytes at file offset `0x161170`, one per character, pre-arming
`scratch[0x153]`.

The opening block — camera settling toward the current character, background fading to grey 0x60,
`RotMatrix`, scene yaw — is identical in sub-states 1 and 3 and is now
`SettleSceneCamera`. Extracted because it is a repeated independent unit, not to slice one body.

### A correction to yesterday's particle array

Reading sub-state 4 turned up an error in what was written earlier today. The particle array's base
is **`0x00572be4`**, not `0x00572c00`. Two loops walk it and they disagree by `0x1c`:

| loop | starts at | works on | records |
|---|---|---|---|
| `FUN_00506880` | `&DAT_00572c34` | `piVar10 - 0xd` = `0x572c00, 0x572c9c, 0x572d38, 0x572dd4` | 4 |
| `FUN_00508e00` | `&DAT_00572c20` | `puVar6 - 0xf` = `0x572be4, 0x572c80, 0x572d1c, 0x572db8` | 4 |

The second set is the first minus `0x1c`: the frame loop manipulates the **position field**, not the
record base. And `0x00572b48 + 0x9c = 0x00572be4` — the array begins exactly where the scene entity
ends, which settles it. Corrected, and `EmitSceneParticles` now offsets by `+0x1c` explicitly.

### Tooling

Two robustness bugs in `playtest.ps1`, both found by being bitten:

- **The relaunch was not handled.** When the game loses the race for low memory it re-executes
  itself as a child (about one run in four) and the parent then waits, holding no window. The script
  drove the parent, focus silently failed, and thirty taps moved nothing. It now picks the
  `OpenForesia` process that actually owns a window, and kills the whole tree rather than the parent
  alone.
- `-Then` added earlier now covers the sequences these sub-states need.

### Where this stops

Sub-states **2, 4, 5, 6 and 7** are still not transcribed. Sub-state 4 is read and understood — it
is the eight-item element carousel on a circle of radius 0x78, keyed on `scratch[0x151]`, using the
`DAT_00562a58` tone table already recovered, with its own array of eight `0x9c` records at
`0x00572e58`. Its Ghidra signature is lost the usual way (`unaff_retaddr`, `iStack_8`, `unaff_BP`,
`in_stack_00000010`) and its prologue is `SUB ESP,0x44` plus four `PUSH`es = `0x54`, so its arguments
sit at `[ESP+0x58..0x64]`. Sub-states 6 and 7 are 545 and 713 lines and will not be one sitting.

## 2026-08-15 — sub-state 4: the element carousel

`FUN_00508e00` runs. Eight medallions on a circle of radius `0x78` around the character, the front
one lit with its glyph, the "Elé. / Feu" panel following the selection. The chain is now
0 → 1 → 3 → 4.

### Signature, again from the prologue

`SUB ESP,0x44` plus four `PUSH`es = `0x54`, so arguments sit at `[ESP+0x58..0x64]`. `EBP` takes the
primitive cursor from `[ESP+0x60]` before the draw loop — that is Ghidra's `unaff_retaddr` — and the
epilogue returns the last `FUN_005443a0` result.

One trap worth recording: after the input phase the original **reuses its fourth argument's stack
slot as the loop counter** (`INC EBX ; MOV [ESP+0x64],EBX`), which is what Ghidra renders as
`uStack00000004`. Reading that slot as "the pad" past the input phase would be wrong.

### The icon array

`DAT_00572e58`, eight records of `0x9c` — the same record type as the scene entity and the carousel
slots, laid out on a circle exactly as sub-state 0 lays out its sixteen characters, with a step of
`0x200` and radius `0x78` instead of `0x100` and `0x96`. It follows the particle array:
`0x00572be4 + 4 * 0x9c = 0x00572e54`, then `0x00572e58`.

Ghidra names it `DAT_00572e78` in the init loop and `DAT_00572e80` in the draw loop, because each
walks it from a different field — `+0x20` and `+0x28`. Same array.

### Two mistakes of mine, both caught by looking

- **`MOV ECX,[ESI + 0x10]` is not `icon + 0x10`.** `ESI` walks the array at `base + 0x28`, so that
  read is `icon + 0x38` — the attribute word set to `0x1000000` at init. Taken as `+0x10` it reads
  zero, and every medallion drew as a **black disc**. The screenshot is what caught it; the numbers
  were unchanged, since the primitives were emitted either way.
- The particle array base was corrected the same session (`0x00572be4`, see the previous entry),
  which is what made this sub-state's copy loop line up at all.

Both share a lesson: when a loop walks a record array from an interior field, every `[reg + n]` in
its body is offset by that field, and Ghidra's `DAT_` name for the base hides it.

### Tooling: the window title matters

`playtest.ps1`'s relaunch handling picked the wrong window. A relaunched game also carries a
**console** window whose title is the exe path, and taking that one sent every keystroke nowhere —
the capture showed a black console and the menu never moved. It now requires
`MainWindowTitle -eq 'OpenForesia'`.

### Remaining

Sub-states **2, 5, 6, 7**. Sub-state 2 (143 lines) is reached only as sub-state 3's back-step.
Sub-states 6 and 7 are 545 and 713 lines.

## 2026-08-15 — `FUN_00520ae0`: signature resolved by a forced-signature Ghidra re-run

Sub-state 5 cannot be seen without this: the frame loop runs the particle pass for every sub-state
above 4, before anything sub-state 5 draws. So the particle emitter had to be unblocked first.

### The re-run

`~/ghidra_scripts/ForceDecompileParticlePass.java`, a variant of `ForceDecompileCharacterSelect`
that **sets the function's prototype before decompiling** rather than only creating it:

```java
fn.setCallingConvention("__cdecl");
fn.updateFunction("__cdecl", new ReturnParameterImpl(uintPtr, currentProgram), params,
                  Function.FunctionUpdateType.DYNAMIC_STORAGE_FORMAL_PARAMS, true,
                  SourceType.USER_DEFINED);
```

Run the same way as the other one (`-noanalysis -readOnly`, from PowerShell). It reported
`undefined FUN_00520ae0()` before and the five-parameter prototype after, and rewrote
`Legacy/Raw/Blaze/FUN_00520ae0.c` and `.asm`.

### The signature

```
uint* FUN_00520ae0(uint* otEntry, uint* prim, int* particle, uint flags, uint extra)
```

Every parameter is pinned to an instruction rather than inferred:

| arg | slot | evidence |
|---|---|---|
| 1 `otEntry` | `[ESP+0x2C]` | `0x520eb8 MOV ECX,[ESP+0x2c]` then `MOV [ECX+EBP*4-0x28],EAX` — the OT link write |
| 2 `prim` | `[ESP+0x30]` | `0x520ea8 MOV EAX,[ESP+0x30]`, then `MOV [EAX],ECX` writes `otSlot \| 0x9000000`; also what the early exits return |
| 3 `particle` | `[ESP+0x34]` | `0x520ae7 MOV EDI,[ESP+0x34]`, then `[EDI+0x20]` = the `particle[8]` guard |
| 4 `flags` | `[ESP+0x38]` | `0x520ec3 MOV EBP,[ESP+0x38]` then `OR EBP,0x2c` — the GPU command byte |
| 5 `extra` | `[ESP+0x3C]` | `0x520f5b`, feeding `((short)x >> 2) & 0xffc0 + (x & 0xff)` — the project's known `extra` idiom |

So it is the same family as `DrawSpritePrimitive` and `DrawFlatPrimitive`: a ten-dword primitive
linked into the ordering table.

### The pseudo-C is still not trustworthy line by line

This is the part worth remembering. Forcing the signature fixed the *prototype* but **not** the
variable attribution inside the body. In the freshly decompiled output:

- `prim[1] = ((uint)primCursor & 0xff | 0x2c | …)` — that `& 0xff` term is **arg4**, not arg2. The
  assembly reads `[ESP+0x38]`.
- `prim[3] = (((ushort)((short)particle >> 2) & 0xffc0) + ((uint)particle & 0xff)) * 0x10000 …` —
  that is **arg5**, not arg3. The assembly reads `[ESP+0x3C]`.
- `iStack_4`, used as `iStack_4 - 0x28 + iVar19 * 4` for the OT link, is **arg1**, rendered as a
  local.
- `unaff_retaddr`, through which the whole primitive is written and which the function returns `+10`,
  is **arg2**.

A forced signature makes the body *readable*; it does not make it *copyable*. Transcribing this one
needs a line-by-line cross-check against `FUN_00520ae0.asm`, which the script now writes alongside.

### State

The signature and the evidence are recorded in the `EmitSceneParticles` throw message, so the next
session starts from the answer rather than the question. The body itself is not transcribed.

Remaining on the screen: the body of `FUN_00520ae0`, then sub-states **5** (175 lines), **2** (143,
reached only as sub-state 3's back-step), **6** (545) and **7** (713).

## 2026-08-15 — the particle emitter, forced signatures for all eight handlers, and sub-state 5

The chain now runs 0 → 1 → 3 → 4 → 5. Sub-state 5 is the **bonus draw**: three cards, turned over
one at a time, with a "Bonus" panel counting the total and the banner "Tu reçois un bonus.".

### `FUN_00520ae0` is transcribed — `EmitParticle`

Written from the forced-signature re-run plus the listing. It is the same shape as
`DrawSpritePrimitive`: transform the position, cull, then emit a ten-dword primitive linked into the
ordering table, scaled by perspective (`0x190000 / otSlot`).

Two things settled that the pseudo-C could not give:

- **The OT pointer the caller passes is `otBase + 0x14` bytes, not `otEntry`.** Settled by range:
  the slot written is `arg1 - 0x28 + otSlot * 4` with `otSlot ∈ [0x58, 0x3f7]` from the cull test.
  With `otBase` every write lands inside the 0x400-entry table; with `otEntry` (`otBase + 0xffc`) it
  would run about a thousand bytes past the end.
- **All four argument slots are reused as scratch** once emission starts — overwritten at
  `0x520e89`, `0x520efa`, `0x520f08`, `0x520f26`. That is why Ghidra calls `flags` "primCursor",
  `extra` "particle", `otEntry` a local `iStack_4`, and `particle+0x1c/0x24`
  "otEntry[7]/otEntry[9]". A forced signature fixes the *prototype*; it does not fix attribution
  inside the body.

`TODO(REVERSE)` left in place: the attributes are read at `atlas[1]`, i.e. **frame 0**, while the
colour is read at the current frame. Same shape as the texture-page quirk in `DrawAnimatedModel`.

**Not yet exercised at runtime.** The frame loop's in-scene block runs *after* the handler dispatch,
and sub-states 6 and 7 still throw there, so the particle path has not actually executed. Said
plainly rather than counted as done.

### `LoadPointAsMatrixTranslation` extracted

`ProjectPoint` and `FUN_00520ae0` share the preamble instruction for instruction — load the camera
matrix, transform the point, make the result the matrix translation. Extracted on that basis. The
carousel still renders at 433.1 primitives/frame after the change, unchanged.

### All eight handlers re-decompiled with forced signatures

`~/ghidra_scripts/ForceDecompileSubStates.java`, the batch form of the particle script. It sets

```
uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
```

on all eight before decompiling. Worth knowing what it does and does not buy:

- **It confirms the hand-derived signature** — `pad[3]` now appears as written, where before it was
  `*(byte *)(in_stack_00000010 + 3)`.
- **It does not recover stack locals.** The 20-byte sprite descriptors still come out as
  `&stack0xffffffdc`. Those still need the listing.

### Sub-state 5 — `FUN_00509470`, the bonus draw

Three cards on a `0x9c` stride at `DAT_00573350` — the same record type as everything else on this
screen. Each turns from angle 0 to `0x800` in steps of `0x100`, its face drawn at `0x400`, and the
three faces add into `DAT_005e906a` (starting at 15, capped at 99). Three identical faces with the
total still under 99 earn a re-draw; that is the whole purpose of phase `0x3a`.

The three records run to `0x573524` and so **overlap `DAT_00573518` by twelve bytes**. Not a
mis-read: the `0x9c` stride is confirmed by the three addresses the code compares
(`0x573380`, `0x57341c`, `0x5734b8`), and no field touched goes past `+0x30`. The overlap is in the
original and is harmless.

**A push-accounting error nearly cost a wrong transcription.** Reading `MOV [ESP+0x24],AX` after one
`PUSH`, the address is `(ESP-4) + 0x24 = clean + 0x20` — not `clean + 0x28`. Getting that wrong made
the sprite look like it overlapped the argument area and made one store look dead. Counting the four
`POP`s in the epilogue fixed the frame, and with the arithmetic done properly every write lands
inside the twenty bytes at `clean + 0x10`, with no dead store at all.

### Control code `0x06` — print a number in decimal

`FUN_005449f0`, decoded by hand like `0x0f` and `0x1c`. Two operand bytes: `text[0]` is a byte offset
into `DAT_005e9060`, the player-stat block, and `text[1]` the width to read (2 word, 4 dword, else
byte). The value is walked against a ten-entry divisor table at `DAT_00566be8` (1e9 down to 1) and
each digit emitted through `RasterizeGlyph`. Leading zeros are suppressed by a latch that also fires
on the last divisor, so zero still prints one `'0'` — which is exactly what the "Bonus" panel shows
before the first card is turned.

Handlers now available on the render side: `544810`, `5449f0`, `544b10`, `544c50`, `544d90`,
`544e50`, `544e80`, `544fb0`.

### Remaining

Sub-states **2** (143 lines, reached only as sub-state 3's back-step), **6** (545) and **7** (713).
Reaching 6 will finally exercise `EmitParticle`.

## 2026-08-15 — new machine, then `FUN_005098f0`: sub-state 6, the stat-allocation screen

Picked up on a fresh machine (see the top of this file and `CLAUDE.md` for what changed: the
`dotnet` on this PATH already carries an x86 SDK, no `DOTNET_ROOT` juggling needed; `playtest.ps1`
now detects which case applies instead of hardcoding the previous machine's paths). Ghidra itself
is present (`D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC`) but `~/ghidra_scripts` is empty —
the two force-decompile scripts were never committed, only their source is preserved in this file's
"The re-run" / "All eight handlers re-decompiled" sections above. Not needed this session: the
forced-signature body for `FUN_005098f0` was already committed from an earlier session.

### The screen: seven stats fanned around the character, plus two extra slots

`FUN_005098f0` is the point-allocation screen reached right after the dice roll. Seven stat rows
(Force, ...) laid out on `s_SelectSlots_00573518` (already allocated at nine records of `0x8c8` —
clearly sized for this function by an earlier session, before it was transcribed) via
`GetCosTableEntry`/`GetSinTableEntry` at `i * 0x155`, a fan covering a bit over a third of a turn —
same shape as sub-state 0's full-circle carousel, smaller step. State fields:

- `state[2]` (`DAT_00578df0`) — the phase, same convention as sub-state 5's `StepSubState5Phase`.
- `state[8]` (`DAT_00578e08`) — the row cursor, 0..6. Already implicit in the nine-`int` sizing of
  `s_SelectState_00578de8`, just not read through the array anywhere until now (contrast
  `s_DiceRolled_00578e04`, which duplicates `state[7]` as its own field instead — an existing
  inconsistency, left alone rather than refactored out of scope).

### Two data-table reads confirmed at the binary, and one resolved by cross-reference instead of the `.asm`

`FUN_005098f0.asm` turned out to be **incomplete** — Ghidra's linear disassembly stops right after
the two-level jump table dispatch (`JMP [ECX*4 + 0x50a75c]`) and resumes at `LAB_0050a5e0`, roughly
0xB40 bytes later; none of the individual case bodies have corresponding assembly in the file. That
made the usual "count the pushes, read the listing" cross-check unavailable for this function's
switch body.

One place this mattered: the C renders `uStack_14 = **(undefined4 **)(_DAT_005e97c0 + 0x94)`, four
bytes off `s_MainResourceDirectory_005e97c4` used identically three lines later in the shared tail —
looked exactly like the kind of decompiler aliasing glitch documented elsewhere in this file. It
isn't one. `grep -c DAT_005e97c0 Blaze.c` returns 40 hits across the whole game, and line 74556
(`DAT_005e97c0 = *(int *)MainMem_exref + 0xe0000;`) then line 74793
(`DAT_005e97c4 = FUN_00529b00(&local_3c,DAT_005e97c0);`) shows they're two genuinely distinct
globals — `s_MainResourceBuffer_005e97c0` (already declared, from `RunMainLoop`) feeds the
construction of `s_MainResourceDirectory_005e97c4`. Lesson: a "these two DAT_ symbols look like the
same thing four bytes apart" hunch is exactly the case that needs a whole-file `grep`, not a
shrug — Ghidra was right and the hunch was wrong.

The other two tables (`&DAT_00562ab8`, eight bytes; `&DAT_00562ac0`/`&DAT_00562ac8`, eight bytes
and nine words) had no `.asm` to check against at all, so they were read straight from
`Legacy/Binaries/Blaze.exe` with `OpenForesia.Tools -- at` and recorded as `s_ClassStartIcon_00562ab8`,
`s_StatBitRemap_00562ac0`, `s_StatBitTable_00562ac8`.

### What's transcribed, and where it stops

Cursor movement, point spend/refund (capped at 99, floored at the class's rolled baseline via
`s_StatSource_00578de4`), the row header icon + description panel, the nine-row tail render, and
the phase state machine (including the dialog-gate branches that are dead under the always-closed
`s_Unknown_00560fef`/`s_Unknown_005e909f` gates, transcribed anyway per the project's own
precedent in `StepSubState5Phase`) are all done and confirmed working at runtime (see below).

**`FinalizeCharacterStats` stops at five unresolved calls.** When the pool reaches zero and the
confirm button is pressed, the original resets a large swath of `scratch` (character record
fields, a texture copy from `MainMem + (class+0x7c)*0x2000` into `scratch+0x300`, flag tables) and
then calls `FUN_00538ab0`, `FUN_00538c50`, `FUN_00538e20`, `FUN_00538f50`, `FUN_00538fa0` — none of
which have decompiled bodies at those addresses in `Blaze.c`. Everything before those calls is
transcribed and throws precisely there, naming all five, rather than guessing what a seven-character
checksum-code generator that never runs would need. The **cancel** path (`case 0x5d`'s
`DAT_005e909f == 0` branch) doesn't depend on those functions and is fully transcribed — it writes a
"0000000" placeholder code and exits via `state[0] = -1`, which `RunCharacterSelectFrameLoop`
already handles correctly (that check predates this session, evidently written in anticipation of
this exact function).

One quirk kept rather than resolved: the "animated header tint" written to `s_PanelSelection_005e9060[7]`/`[8]`
(a `CONCAT13`-style two-tap blend of two stat bytes, offset by `scratch[0x148]`/`[0x14a]`) has no
reader anywhere in this function. The render loop happens to print it as if it were a stat value for
the two extra rows, which is why they showed plausible small numbers in testing rather than reading
as clearly a "total" or "confirm" label — that assumption from the row count turned out unconfirmed;
transcribed as read, not renamed to fit the guess.

### Confirmed at runtime

`playtest.ps1 -Entry 1` with nine `ENTER`s (name entry, language, element, dice roll, confirm) reaches
the screen cleanly: no crash, 644.1 `DrawPrimitive`/frame (well above every prior screen, consistent
with `EmitParticle` finally running per the dispatcher wiring already in place), all seven stats and
the two extra rows rendered with plausible values, header icon and description panel tracking the
cursor. A follow-up run added `DOWN,DOWN,RIGHT,RIGHT,UP,LEFT` after arrival: cursor moved (icon
repositioned, description text changed to match), one stat's value changed — `EditCharacterStats`'s
input handling confirmed working, no crash.

One unrelated crash surfaced by chance during this testing, **not a regression**: control code
`0x14` in the shared UI text engine throws its generic "never emitted, needs a scoped Ghidra re-run"
message (`Blaze.cs` around line 10267) when the always-on "Bonus" panel's text happens to contain
that escape — pre-existing (confirmed via `git show HEAD:...`, present before this session's 516
added lines), timing-dependent on the dice roll's RNG, and orthogonal to character-select work.

### Remaining

Sub-state **2** (143 lines, reached only as sub-state 3's back-step) and **7** (713 lines, next on
the path — `state[2] = 0x6e` when sub-state 6 hands off via `state[0]++`). `FinalizeCharacterStats`'s
five missing functions block only the real confirm-and-continue path, not reaching or using the
screen.

## 2026-08-15 — `FUN_0050a910`: sub-state 7, the memory-card save screen

Continued the same session (see above for the machine notes). This is a different kind of
sub-state from 0–6: not just UI over the character record, but a save/load screen built on a
PS1-BIOS-flavoured memory-card layer — `Ordinal_9/21/22/23/24/25/67/68/69/72/684` wrapped in retry
loops (`FUN_00504690/6c0/730/770/7e0/820`, `FUN_00504b40/70/90/c20`, `FUN_00512900`,
`FUN_005128e0`), none of it read this session. That subsystem is its own coherent unit of work —
see "What's stubbed" below.

### The two-level jump table was, again, just a compiler artifact

`FUN_0050a910`'s C renders `switch((&UNK_0050bea8)[uVar5])` with `/* WARNING: Could not find
normalized switch variable to match jumptable */`, the same red flag as sub-state 6's incomplete
`.asm`. This time the `.asm` **was** complete (688 lines against 719 of C, ending on the function's
own `RET`), and it showed a genuine two-level indirection: `MOV BL,[EAX+0x50bea8]` then
`JMP [EBX*4+0x50be44]`. Read `&DAT_0050bea8` directly from the binary (105 bytes,
`OpenForesia.Tools -- at`) rather than guess: it's the identity map `phase+5 → phase+5` for every
case that's actually spelled out in the C (`-5..18` and `99`), collapsed to one shared filler value
`0x18` for the ~80 unused phase numbers, with `-3` and `-2` sharing a slot (matching the C's own
`case 0xfffffffd: case 0xfffffffe:`). **The C's case structure is exactly the real control flow** —
the compiler's sparse-switch lowering just looked like a hostile jump table to Ghidra. A plain C#
`switch` on the phase int reproduces it with no remap table needed in the port at all.

### Two more static tables, at addresses that turned out not to be pointers-into-nowhere

`_DAT_00578e18 = **(char**)(port*4 + 0x562b30) - 1` looked at first like another "pointer into
memory the unresolved I/O layer would populate" — until dumping `0x562b30`'s four pointers and then
the bytes *at* those targets showed real, non-zero, baked-in demo data: port 0 has two pre-existing
save slots. Recorded as `s_CardDirectorySize_00562b08` and `s_CardSlotFlags_00562adc` (both
`byte[][]`, one row per port). This means the idle render of this screen shows a small existing
save directory, not an empty one — worth knowing before reading a future capture of it.

### `FUN_0051be90` is the one real blocker, and it's a `.asm`-can't-help-either case

Distinct from sub-state 6's blockers: `FUN_0051be90` has **no decompiled body anywhere in
`Blaze.c`**, only call sites (confirmed by grep — same class of gap as an unnamed DLL ordinal, but
this one's an internal `Blaze.exe` function Ghidra never recovered at all, most likely because none
of its callers gave it a resolvable prototype either). Six cdecl parameters are visible from every
call site: `(otEntry, prim, sprite, value, mode, extra)`, `mode` always `0x82` in this function. It
sits in the same family as `DrawSpriteVariant`/`DrawFlatPrimitive` (same 20-byte sprite descriptor)
and is called with a single-byte `value` pulled from the same stat-value buffer sub-state 6 uses —
strongly suggestive of a numeric-tile primitive, but CLAUDE.md is explicit that this class of gap
"needs a Ghidra re-run, not more reading," so it's stubbed (`EmitCardGlyphTile`) rather than
guessed. **It sits in the unconditional part of the render tail** (the two "current stats" glyph
rows run every frame, not just when browsing cards), so unlike sub-state 6's blockers, this one
blocks *all* rendering of this screen, confirmed by test (below).

### What's stubbed, and why as one unit rather than piecemeal

`ThrowSaveCardUnresolved(context)` is the single throw point for every phase that touches the card
layer (cases `1`, `4`, `6`, `0xd`, `0x10`, `-5`, `-3`/`-2`, plus the every-frame transfer-poll that
precedes the switch). Cases that only touch already-known state (`0`, `2`, `3`, `5`, `7`, `8`, `9`,
`0xa`, `0xb`, `0xc`'s dead branch aside, `0xe`, `0xf`, `0x11`, `0x12`, `99`, `-4`, `-1`) are fully
transcribed, including the dialog-gate branches that are dead under the always-closed
`s_Unknown_00560fef`/`s_Unknown_005e909f` gates (same convention as sub-states 5 and 6). Case `0`
turned up its own never-set gate, `iRam00560fd4` → `s_Unknown_00560fd4`, a new address, not a
rename of an existing one — checked before declaring it, per the standing rule.

The `uVar17` idiom (several cases just set a label pointer and `break` into a shared
`BindMenuLabels` + phase-increment tail) is reproduced with a `pendingLabel` local rather than
duplicating the pair of calls at every site — first-draft duplicated it inline and **missed the
phase increment at four of the six sites**, caught by re-diffing each case against the raw C
line-by-line rather than trusting the first pass. Worth remembering: merging near-identical cases
by eye is exactly where a shared field (here, the phase-advance) silently drops out.

### Confirmed at runtime — with a new tool for reaching a not-yet-connected sub-state

Sub-state 7 cannot be reached through the normal chain: sub-state 6's `FinalizeCharacterStats`
throws before it ever sets `state[0]++`. Added a **temporary, clearly marked** hook —
`OPENFORESIA_FORCE_SUBSTATE=N` in `LoadCharacterSelectResources` — to set `state[0]` directly for
testing a sub-state in isolation, the same scaffolding pattern already used elsewhere
(`OPENFORESIA_TRACE_MENU`, `OPENFORESIA_OPTIONS_BADPATH`, etc.).

`OPENFORESIA_FORCE_SUBSTATE=7` plus a single `ENTER` into the character-select menu: **no crash**
through the init block, the port/cursor computation, and the full phase state machine — confirmed
the two-level-table reasoning and the stubs are wired correctly. Rendering gets past
`EmitViewportRect`/`DrawUiPanel`/the first `BindMenuLabels` call and throws inside `RunTextEngine`
on **control code `0x04`**, a pre-existing gap (confirmed via `git show HEAD:...`, present before
this session) in the same "never emitted, needs a scoped Ghidra re-run" family as the `0x14` gap
sub-state 6's testing turned up — unrelated to anything written this session, just the next
unresolved text control code down the line. `FUN_0051be90` was never reached because this earlier,
unrelated gap fires first; it remains the real blocker for this screen once `0x04` is dealt with.

### Remaining

Sub-state **2** (143 lines, back-step only). For sub-state 7: the text-engine control code `0x04`
gap (quick — same shape as the already-fixed ones, needs its handler read from
`Legacy/Binaries/Blaze.exe`/`Documentation/Reference/DataSymbols.md`), then `FUN_0051be90` (needs a forced-signature
Ghidra re-run, six params, see above), then the memory-card subsystem itself if save/load is ever
to actually work. None of these block sub-state 6 from being finished first instead, via its own
five missing functions — the two blockers are independent.

## 2026-08-15 — `FinalizeCharacterStats`'s five functions, one at a time

Same day, continued after a commit. The user asked for the five blockers to be closed one function
at a time, committing between each — so this section covers all five together but each was written,
built, and (where possible) checked before moving to the next.

### `FUN_00538ab0` — `CommitStagedStatBlock`

Trivial: four dwords copied from `param+0x280` to `param+0x120`. Called at dozens of sites across
`Blaze.c` on all sorts of records (not just this one), so named on its apparent role — "commit the
staged stat block" — rather than on this single call site; flagged in its doc comment as open to
renaming if another site turns out to use it differently.

### `FUN_00538f50` — `ScaleStatBlockByDifficulty`

Also trivial: eight shorts at `record+0x20`, each multiplied by `(0x1000 - factor*0x200)/0x1000`
and clamped to `[0, 999]`, where `factor` comes from `record+0xec`. Called with `scratch+0x100`, so
`factor` reads `scratch+0x1ec` — never written by any reconstructed code, always 0, which makes the
whole scaling formula the identity (`0x1000 * x >> 12 == x`). Verified that identity by hand rather
than assuming it, since silently trusting "it's probably fine" is exactly how the wrong half of a
shift expression slips through unnoticed.

### `FUN_00538c50` — `ComputeDerivedStats`

The first one worth real care: eight derived stats computed from the eight primary stats
`CommitStagedStatBlock` just wrote, each formula a `>>`/`+` combination whose grouping depends on
`>>` binding *looser* than `+` in both C and C# — same precedence table, so the original's implicit
parenthesization carries over, made explicit here rather than relied on silently. Clamped to 999,
then copied back with a single `CopyDwords(source: +0x290, dest: +0x130, count: 4)` — the original
does this as four dword copies, and since the four dwords are contiguous on both ends it collapses
to one call without changing behavior.

### `FUN_00538e20` — `ApplyEquipmentStatBonuses`

Iterates five equipment-slot bytes at `record+0x2b0..0x2b4` (`0xff` = empty slot). Each populated
slot indexes `record+0x300+id*0x280` — which is **the same texture blob**
`FinalizeCharacterStats` already copies in via `CopyDwords` from `MainMem+(class+0x7c)*0x2000` —
confirming that blob isn't just a portrait texture, it's a `0x280`-byte-stride array of item
records with embedded stat bonuses at `+0x20`. With the current default equipment
(`scratch[0x2b0..1]` always active, `[0x2b2]` class-gated, `[0x2b3..4]` always `0xff`), two of the
five slots and the branch gated on them stay dead — transcribed anyway, per the project's own
precedent elsewhere. Also introduced `DivideRoundedBy100`, the signed-division-by-100 idiom (magic
multiply + sign correction) Ghidra reconstructs from the compiler's optimized division — transcribed
term for term rather than simplified, since nothing here proves the simplification would be bit-
identical for negative inputs. New table read from the binary: `s_DifficultyBonus_0056d2c0` (16
bytes), indexed by `DAT_005de034` (always 0 — no reconstructed code sets a difficulty tier yet).

### `FUN_00538fa0` — `FinalizeEquipmentEffects`

The last and largest: called on `scratch+0x100`, so its offsets land on `scratch+0x108..0x156`, a
region nothing else in this session's work touches — confirmed by checking, not assumed. Picks a
4-byte fill pattern (`0x00`, `0x01010101`, or `0xffffffff`) from two flag bits, seeds two counter
dwords with it, nudges eight individual counter bytes up or down from further flag bits, then uses
those counters to scale (`×1.5` or `×0.5`) the eight derived stats `ApplyEquipmentStatBonuses` just
computed. Every source byte it reads (`record+0x40`, `+0x86..0x8a`, `+0xba`) is zero in this port's
current state — `scratch+0x100..0x1ff` stays untouched by anything reconstructed so far — so the
function is presently a pure `[0, 999]` clamp of sixteen stats, but transcribed branch for branch
rather than reduced to that, since nothing here guarantees it stays that way once equipment or
another sub-state writes into that range.

### The checksum tail that was hiding behind the throw

With all five functions in, the code that builds the seven-character save code (previously
unreachable, so never transcribed) had to be written too — `FinalizeCharacterStats` doesn't exit
the screen on its own, it sets `state[2] = 0x81` and lets the phase state machine (already wired)
advance `state[0]` to sub-state 7 on a later frame. Two things worth remembering from writing it:

- **A four-level nested `(a + (b + (c + (d + x) * 2) * 2) * 2) * 2` needs a stack-based paren count,
  not a glance.** First reading dropped the outermost `* 2`, which would have silently produced a
  checksum half the intended magnitude. Caught by mechanically parsing the parens with a stack
  before writing any C#, not by re-reading the C faster.
- **A dead check silently overwritten one line later is safe to elide, not just safe to keep.**
  `pcRam00566bc0[iVar13+0xd]` is tested against `'\\'` and conditionally rewritten to `'7'`, then
  unconditionally overwritten with `0` on the very next line — the test's result is provably never
  observed. Elided with a comment saying so, rather than transcribed for the sake of transcribing;
  this is different from the "dead under always-closed dialog gates" branches kept elsewhere,
  because here the deadness is a *local, provable* write-then-clobber, not a runtime condition that
  could change.

Reused the existing `WriteCharacterCode` helper (written for the sub-state 5/6 cancel path) for the
final copy — same "copies from byte 0 of a buffer that's only ever written from byte 0xc onward, so
currently produces an empty string" quirk noted back when that helper was first written applies
here too, now confirmed by a second call site rather than just inferred.

### Confirmed at runtime — the sub-state 6 → 7 transition now works for real

Added one more piece of temporary scaffolding, `OPENFORESIA_FORCE_CONFIRM_STATS=1`: calls
`FinalizeCharacterStats` directly on sub-state 6's first frame, sidestepping two unknowns that
aren't this task's concern — which physical key drives `pad+2 & 0x8000` (the confirm bit;
`FoldKeyboardIntoPadState`'s exact bit-packing wasn't traced) and how many points the dice roll
leaves in the pool to spend before confirming is even allowed. A `playtest.ps1` run with this set,
nine `ENTER`s and nothing else, produced a crash — but the stack trace is the actual finding: it
shows `RunCharacterSelectFrameLoop → InvokeCharacterSelectSubState → CharacterSelectSubState7`,
i.e. the **normal dispatcher reached sub-state 7 on its own**, not through the
`OPENFORESIA_FORCE_SUBSTATE` hook. All five functions and the checksum tail ran with no exception;
the crash itself is the *same* pre-existing control-code `0x04` gap already documented above,
confirming sub-state 7's earlier testing found the real next blocker, not an artifact of the
force-substate hook used at the time.

### Remaining

Sub-state **6 is now fully transcribed**, confirm path included. Sub-state **7**'s two blockers
(control code `0x04`, then `FUN_0051be90`) are unchanged by this session — see the entry above.
Sub-state **2** (143 lines, back-step only) is still open. The keyboard-to-`pad+2`-bit mapping for
sub-state 6's confirm button was deliberately not traced (out of scope here); worth doing before
relying on `OPENFORESIA_FORCE_CONFIRM_STATS` becomes inconvenient.

## 2026-08-16 — control codes `0x04` and `0x01`, and a genuine copy-protection tripwire

New session, continuing straight from the `0x04` gap TODO.md left as "quick, same shape as the
already-fixed ones."

### Control code `0x04` — `Handler_005449b0`, as expected

Decoded by hand from `Legacy/Binaries/Blaze.exe` (file offset `0x143db0`, 21 bytes to the `RET`), same
recipe as `0x06`/`0x09`/`0x1c`. Two operand bytes, writing verbatim into the low word of the
descriptor's `+0x20` "rect" dword that `EmitTextSprite` reads back to place the finished line's
sprite — `text[0]` becomes `rect & 0xff`, `text[1]` becomes `(rect >> 8) & 0xff`. The dword's upper
word (including the `0x1000000` enable bit `DrawUiPanel` tests before copying this descriptor's
text into a panel) is left untouched. Confirmed by build; wired into `BuildTextTable`'s render pass.

### Control code `0x01` was the *next* gap down, not a stub — a lesson in checking `Blaze.c` first

Fixing `0x04` let `playtest.ps1 -Trace` (via `OPENFORESIA_FORCE_SUBSTATE=7`) advance further into
the same text stream and crash on control code `0x01` instead, dispatching to `FUN_00544810`.
**Wasted real effort hand-disassembling it from `Legacy/Binaries/Blaze.exe` before thinking to grep
`Blaze.c` for the address** — it was fully decompiled all along (`Blaze.c:91274`), unlike
`FUN_0051be90`. **Lesson for next time: always grep `Blaze.c` for the target `FUN_` address before
reaching for the binary.** The half-finished hand-disassembly did turn out useful for one thing: it
independently confirmed byte-for-byte that the C's odd final line really is the machine code (see
below), which is the kind of claim worth double-checking before trusting Ghidra on a stack-slot
tail read.

`Handler_00544810` draws an **inline icon glyph** — a second, independent icon-in-text path from
`FUN_0051be90`, and one that Ghidra could actually decompile. Three operand bytes select
(category, sub-category, index), each remapped through `s_PanelSelection_005e9060` exactly like
`Handler_005449f0`'s single operand, then chase the same nested atlas `DrawUiPanel` and
`Handler_00544e80` already use: `root[selector0]` (slot 0 or 1) → `+0x94` (atlas) → `[selector1]`
(sub-table pointer) → `[selector2 * 8]` (an 8-byte `(uv, flags)` record). `flags`' byte at bits
16-23 indexes `s_EntityDecodeTable_005e8f00` — already known in this port as the 1024-entry
tpage/clut table `DecodeSlot` reads elsewhere; safe here since the index is an explicit `& 0xff`.
The five stores that follow are the same six-word sprite primitive `EmitTextSprite` builds, fed
from this record instead of the descriptor's own line state. Wired to the correctly-live
`s_DeviceCapFlag_00564a48` (not the dead duplicate — see below) for the conditional `flags &=
0xfffffdff`.

**Found, and left alone: `s_Unknown_00564a48` is a dead duplicate of `DAT_00564a48`.** Two separate
C# fields exist for the same Ghidra global — `s_DeviceCapFlag_00564a48` (`bool`, correctly wired,
set by `CheckDeviceCapabilityFlag`) and `s_Unknown_00564a48` (`int`, declared standalone, never
assigned by anything, so always `0`). The second one gates real rendering paths in at least nine
call sites (`DrawAnimatedModel`, `EmitParticle`, `DrawUiPanel` neighbours, lines 983/1255/1397/
1417/6849/10499/10532/10601/11915) — every one of those `if (s_Unknown_00564a48 != 0)` branches is
silently dead in the current port, always taking the "false" side. `DAT_00564a48`'s *compiled*
`.data` value is `1` (confirmed by reading `Legacy/Binaries/Blaze.exe` directly), so in the original this
gate was live far more often than our port currently reproduces. **Not fixed this session** — nine
call sites is real scope, and merging the two needs re-checking each site's branch against what the
capability bit is actually supposed to mean (still TODO(REVERSE) itself, per `CheckDeviceCapabilityFlag`'s
own entry above) rather than just renaming. Flagged here per the "grep the whole file before
declaring a symbol" rule this project already keeps failing to fully apply — worth a dedicated pass,
and worth checking first if any "geometry right, image wrong" report ever comes in (§7 of `TODO.md`).

### The return value is a real integer multiply, confirmed down to the byte, and looks like copy protection

`Blaze.c:91310`: `return (DAT_0056321c + 1) * (int)(param_1 + 3);` — every sibling text-control
handler just returns the advanced cursor; this one multiplies it. Confirmed byte-for-byte against
`Legacy/Binaries/Blaze.exe` (file offset `0x143c10`..`0x143d6b`, no branch after the load, so this really is
unconditional): `MOV EAX,[DAT_0056321c]; INC EAX; IMUL EAX,[ESP+4]; RET`. The `[ESP+4]` read really
does resolve to `param_1 + 3` — the function spills its updated cursor into its own incoming
argument's stack slot mid-body (`MOV [ESP+0x14], EAX` while 4 callee-saved registers are still
pushed, which is the *same* absolute stack address as `[ESP+4]` after they're popped back off in
the epilogue) specifically so it can read it back here. This is exactly the "arguments reused as
scratch, trust nothing near the tail without counting pushes" trap `TODO.md` §6 already warns
about — except here it turned out to be *correct*, not a trap, which is its own lesson: the
warning means "verify," not "assume it's wrong."

Since `RunTextEngine` feeds this return value straight back in as its next parse cursor, the
multiplier has to be exactly `1` for *any* text pointer to survive — i.e. `DAT_0056321c` must be
exactly `0`. `DAT_0056321c` is set in `RunApplication` (`Blaze.c:61774/61795`, now wired up in this
session as `s_CdCheckFileSize_0056321c`, previously a dropped local — see below) to `GetFileSize`
of the CD-check file, plus a fixed `0xd8000000`. For the sum to land back on exactly `0`, the file
has to be **exactly 0x28000000 bytes (640 MiB)** — an oddly specific, round number for this era's
"corrupt something subtle later instead of refusing to run" style of copy protection, rather than a
coincidence. Left as a hypothesis, not fact — recorded as a TODO(REVERSE) at the call site rather
than asserted, since it can't be confirmed without the real CD-check filename (`TODO.md` #2/#4,
still a data-section gap) or the real disc.

**Also fixed in passing**: `RunApplication`'s `uint fileSize = GetFileSize(hFile, null);` computed
the value and then threw it away — neither `Blaze.c` write to `DAT_0056321c` (the initial set, or
the later `+= 0xd8000000`) had ever been transcribed. Both are now wired to the new
`s_CdCheckFileSize_0056321c` field, faithfully, independent of whatever the multiply in
`FUN_00544810` turns out to mean.

### Confirmed at runtime — same shape of finding as `0x04`, one level deeper

`OPENFORESIA_FORCE_SUBSTATE=7` plus one `ENTER`: no managed exception this time — a native
`0xC0000005`, with the stack topping out inside `RunTextEngine` itself rather than inside the new
handler. That matches the theory exactly: `Handler_00544810` runs to completion and returns a
corrupted cursor (our `cdCheckPath` is still the TODO #2 placeholder, so the CD-check file never
opens, `GetFileSize` returns `0xFFFFFFFF`, and `s_CdCheckFileSize_0056321c` ends up nowhere near
`0`); the crash then happens one `RunTextEngine` loop iteration later, dereferencing that garbage
pointer. Not a new independent blocker so much as TODO #2 finally being reachable in a place that
bites.

### Remaining

Sub-state 7's blocker list grows by one, in order: the `DAT_0056321c`/`0x28000000` mystery (needs
either the real CD-check filename+size from a `.data`-section source, or a decision to special-case
it — not attempted this session, per "never substitute to make things compile"), then `FUN_0051be90`
(unchanged, still needs a forced-signature Ghidra re-run). The dead `s_Unknown_00564a48` duplicate
(above) is independent of both and can be fixed any time — nine call sites, nothing sub-state-7-
specific about it.

## 2026-08-16 — same day, continued: the copy-protection hypothesis confirmed, both remaining loose ends closed

User mounted the real CD (drive `E:`, label `BLAZE_BLAZE_F`) between sessions and asked for the
`s_Unknown_00564a48` duplicate to be fixed. Both closed the same sitting.

### `s_Unknown_00564a48` merged into `s_DeviceCapFlag_00564a48`

Same fix shape as the `s_Unknown_005d9de0`/`s_GammaIni` precedent above, but simpler: rather than
turning the dead name into a forwarding property, all nine call sites (`983`, `1255`, `1397`,
`1417`, `6856`, `10506`, `10539`, `10608`, `12001`) were repointed straight at
`s_DeviceCapFlag_00564a48` (a global find/replace of `s_Unknown_00564a48 != 0` →
`s_DeviceCapFlag_00564a48`, since every occurrence had that exact `!= 0` suffix — checked first),
and the dead `int` field's declaration removed from its shared line with `s_Unknown_00571a3c`
(which stays; still genuinely unresolved). No behavioural precedent to preserve here, unlike
`s_GammaIni`, since the "wrong" field was never correct to begin with.

### The 640 MiB was never a coincidence — `cd.idx` on the real disc

With the CD mounted, `Get-ChildItem E:\` turned up `cd.idx`, size **671,088,640 bytes — exactly
`0x28000000`**, exactly the value the previous session's write-up predicted from
`Handler_00544810`'s return-value multiply without being able to check it. Reading
`Legacy/Binaries/Blaze.exe`'s `.data` directly at `DAT_00563280` (file offset `0x161880`) confirmed the
template text itself: **`"@:\cd.idx"`** — TODO #2 is fully resolved, not just narrowed. This was
1998-era copy protection: an authentic disc's `cd.idx` is exactly 640 MiB, which makes
`DAT_0056321c` land on exactly `0` and the text-cursor multiply in `Handler_00544810` reduce to
identity; anything else corrupts the cursor several control codes downstream of the actual check,
which is what made this so opaque without the disc in hand.

`RunApplication`'s `cdCheckPath` now fills bytes 1-9 with the confirmed template (only the drive
letter at index 0 is still substituted per candidate, same convention as `driveRoot`) instead of
being left as an unfilled placeholder buffer.

### Confirmed at runtime — clean hand-off to the next, already-known blocker

`OPENFORESIA_FORCE_SUBSTATE=7` plus one `ENTER`, same as both prior sessions' tests: **no native
crash this time.** `Handler_00544810` now returns a correct cursor, `RunTextEngine` keeps parsing
normally, and rendering reaches — and cleanly throws on — `FUN_0051be90` via `EmitCardGlyphTile`,
exactly the blocker documented since the very first sub-state-7 session, now reached through the
*real* dispatch path instead of a forced jump past a crash. This is the intended kind of "gap":
loud, a managed `NotImplementedException` naming the exact function and its six-parameter
signature, not a wild pointer three call frames away from its cause.

### Remaining

Sub-state 7 has exactly **one** blocker left: `FUN_0051be90` (needs a forced-signature Ghidra
re-run — six cdecl params, see `TODO.md` §5 for the script). Nothing else in this screen's render
path is currently known to be broken. Sub-state 2 (143 lines, back-step only) and the memory-card
subsystem remain open but are unrelated, pre-existing scope.

## 2026-08-16 — same day, third pass: `FUN_0051be90` needed no Ghidra re-run at all, and one more raw-VA gap

User asked to launch the forced-signature Ghidra re-run this entry recommends. **Before doing
that, grepped `Blaze.c` for the address first** (lesson from the previous pass, finally applied) —
and the body was there all along, at line 66710, complete and correct. The "never decompiled by
Ghidra" claim in every prior write-up (this file, `TODO.md`, the `EmitCardGlyphTile` stub's own
exception message) was wrong: a search miss somewhere in a 98,000-line file, not a real gap. No
re-run needed; the two other unresolved TODO-script entries in `TODO.md` §5
(`ForceDecompileSubStates.java`/`ForceDecompileParticlePass.java`) were never even invoked this
session.

### `FUN_0051be90` — `EmitCardGlyphTile`, transcribed

Draws a decimal number as a row of numeral-tile sprites (five words each, linked into `otEntry`) —
not through the text engine at all, a separate primitive-emission path. Everything it calls was
already reconstructed: `FUN_0051be20` → `EmitViewportRect`, `FUN_0051f9c0` → `DrawFlatPrimitive`,
`FUN_0051bdb0` → `EmitClipRect`. So once read, this was a same-shot transcription, no cascading
gaps.

Two things worth remembering from the read:

- **`mode`'s low nibble is a digit count, and it only skips *processing* the leading divisors, not
  the accumulated value.** The ten-entry decimal divisor table (`s_DecimalDivisors_00566be8`, the
  same one `Handler_005449f0` uses) is always walked start to finish, but the loop body — the
  actual `value / divisor` — only executes once the remaining-iterations counter drops to `mode &
  0xf`. For `mode = 0x82` (two digits) that means the *tens* divisor absorbs whatever accumulated
  in the seven skipped leading divisors, so a value at or above 100 truncates into whatever the
  tens/units math happens to produce rather than overflowing cleanly into more tiles. Every call
  site in this screen keeps its values under 100, so this is not expected to bite — flagged rather
  than "fixed," since fixing an original truncation quirk that never fires isn't this project's job.
- **`sprite[0]` here is read with no per-frame offset**, unlike `DrawSpriteVariant`/
  `DrawFlatPrimitive`, which both add `(short)sprite[4] * 8`. Ghidra's literal `*(uint *)*param_3`
  has no such term. Assumed-by-analogy would have been wrong; read, not guessed.

### A second raw-VA-as-pointer gap, same shape as the CD-check multiply — found by actually running it

Transcribing `EmitCardGlyphTile` alone still crashed natively, *inside* it this time. Cause:
`RenderSaveScreen`'s three call sites (pre-existing code, not written this session) do `*(uint*)box
= 0x566c30;` — a **literal absolute address**, `DAT_00566c30`, baked into the original because that
binary loads at a fixed base and can afford to. Our port doesn't map anything at `0x566c30`;
dereferencing it as `EmitCardGlyphTile`'s `sprite[0]` faulted immediately. Same species of bug as
last entry's `cdCheckPath` placeholder, different manifestation: a hardcoded `.data` address used
as a live pointer rather than a filename template.

Read the real 8 bytes straight from `Legacy/Binaries/Blaze.exe` (file offset `0x165230`): `{ 0x08504800,
0x02300000 }` — a plausible (colour, attributes) pair (digit column width computes to 8px, texture
page index 0x30, both sane). Allocated a real 8-byte buffer,
`s_NumeralTileRecord_00566c30`, and repointed all three call sites at it instead of the literal.
**Worth a grep pass for other bare `0x56xxxx`/`0x60xxxx`-shaped literals assigned into a `sprite[0]`
slot elsewhere in the file** — this pattern is easy to miss by reading alone (it looks like an
ordinary immediate, not a bug) and only announces itself by crashing once actually exercised.

### Confirmed at runtime — the full render path now completes

`OPENFORESIA_FORCE_SUBSTATE=7`, first frame: `RenderSaveScreen` runs start to finish, no exception,
no native fault — the entire chain fixed across this session's three passes (`0x04`, `0x01`,
`DAT_0056321c`/the CD file, `s_Unknown_00564a48`, `DAT_00566c30`) actually renders now. What was
not anticipated: `CharacterSelectSubState7` calls `StepSubState7Phase` **before**
`RenderSaveScreen` every frame, and the phase counter (`s_CardPhase_00578e0c`) advances from `0` to
`1` on that very first frame — so the *second* frame lands on `StepSubState7Phase`'s `case 1`,
which throws `ThrowSaveCardUnresolved`, the pre-existing, deliberately-out-of-scope memory-card
gate documented since the very first sub-state-7 session. Confirmed twice: once via
`OPENFORESIA_FORCE_SUBSTATE=7`, once via the full nine-`ENTER` normal chain (781 frames, 429.1
primitives/frame average before the throw) — same throw, same case, stable. This is not a new
blocker, just the render path finally being complete enough to reach the *next* one.

### An unrelated visible change, caused by this session's `s_Unknown_00564a48` fix — first mis-read, then actually fixed

The character-select carousel (and any other screen drawing multiple `DrawAnimatedModel`/
`EmitParticle` entities) started showing only the **selected** entity, where earlier sessions'
captures showed all of them. Traced to the duplicate-global fix earlier this session:
`DrawAnimatedModel` has a culling branch — `if (s_DeviceCapFlag_00564a48 && (attributes & 0x200) !=
0) { if (s_Unknown_00564a4c == 0) return prim; ... }` (`Blaze.cs` ~line 983) — that was dead code
while `s_DeviceCapFlag_00564a48` read from the never-written duplicate. With the duplicate merged
away, the flag is live (true in this port's normal no-3D-device case), exposing this branch for the
first time.

**First pass got this wrong**: assumed the resulting cull was intentional and recorded it as
"confirmed correct by the user." It was not — the user came back and called it a real regression.
Re-reading `Blaze.c` found the actual bug, in the *second* condition of that same branch:
`s_Unknown_00564a4c`. Every one of its write sites in `Blaze.c` (at least four: three around
`FUN_00520ae0`/`EmitParticle`, one around `FUN_00537da0` in `FUN_00542ab0`'s `case 2`) follows the
identical shape `DAT_00564a4c = 0; <reentrant call>; DAT_00564a4c = 1;` — a **reentrancy guard**,
not a persistent flag, whose steady-state value is `1`. None of those call sites are wired up in
this port, so the field just needed the right default. It had been declared as a bare `int`
(C# default `0`) — backwards from the original's steady state — so the branch's inner check `if
(s_Unknown_00564a4c == 0) return prim;` always took the early-return (skip drawing) instead of the
far more common fallthrough (`attributes &= 0xfffffdff;`, clear the bit, draw normally). Fixed with
one line: `private static int s_Unknown_00564a4c = 1;`.

**Confirmed by primitive count, not just by re-reading.** `REVERSE_STATUS.md`'s original carousel
write-up (2026-08-14, long before either bug existed) already documents the intended design: all
sixteen slots call `DrawAnimatedModel` every frame, "the selected one straightens and lights up,
the others tilt away and dim." A model rotated near edge-on toward a fixed camera contributes
almost no visible pixels while still genuinely drawing — so a static screenshot showing one
prominent character is not itself evidence of anything. What is evidence: primitive count. Post-fix
measurement on the carousel: **~350/frame**, against the pre-regression historical baseline of
**~382/frame** (`TODO.md`) — consistent with all sixteen slots drawing again, not just one. Also
confirmed interactively: pressing `RIGHT` three times changes the displayed character (Guerrier →
Magicien), so slot selection and animation were never broken, only this cull branch was.

**The primitive-count match was a false all-clear — the user's reference screenshot caught what it
missed.** Matching the historical ~382/frame baseline felt like sufficient proof at the time; it
was not. The user pushed back a second time with a real screenshot of the original 1998 game
showing something this port still didn't: a scattered wall of the other fifteen characters'
**portrait cards** behind the 3D model. A single prominent character is not, on its own, evidence
of correctness — matching a historical number is not the same as matching the reference image, and
this session had the reference image the whole time and didn't check against it first.

### The portrait cards — a second, unrelated bug, traced by elimination

`CharacterSelectSubState0`'s per-slot loop (`Blaze.cs` ~9894-9944) already draws one: a
`DrawSpritePrimitive` call per slot, `atlas+0x28` indexed by character `i` (a genuine 16-entry,
8-byte-stride portrait table — confirmed distinct, non-zero UV data per slot via a temporary trace),
positioned by the same `ProjectPoint` the name label uses, sized from the slot's own `tiltX`/`tiltY`
reinterpreted as `halfWidth`/`halfHeight` (a real repurposing, not a bug — bigger for
non-selected/tilted-away slots, ~0 for the selected one, whose 3D model already covers that spot).
None of this was missing code; it was already there, unconditional on the model's own glow gate,
and firing every frame.

Instrumented step by step (`OPENFORESIA_TRACE_CAROUSEL`, temporary, removed after) to rule things
out in order: real atlas data (not null, not zeroed) → plausible on-screen positions and sizes for
several slots per frame → confirmed `DrawSpritePrimitive` actually gets called. Even an extreme
forced test (fixed size well past anything naturally occurring, a guaranteed-in-bounds OT slot, a
guaranteed-on-screen position) still drew nothing. That ruled out geometry, OT range, and resource
loading all at once and pointed at the primitive-building code itself.

Found there: `DrawSpritePrimitive` (`FUN_00521f20`) and `DrawFlatPrimitive` (`FUN_0051f9c0`) both
gate their attribute-clearing branch behind a "second flag," and both had it wired to
`s_SmallTextureFlag` — the ini `"Small"` texture-quality setting, `0` unless the user's `Blaze.ini`
sets it. Cross-checked byte for byte against `Blaze.c:69625-69638` and `:68565-68569`: the real
global in both is `DAT_00564a4c`, i.e. `s_Unknown_00564a4c` — the *same* reentrancy guard fixed
earlier this session, now defaulting to `1`. `DrawSpriteVariant`, the third sibling, already had
this right; its own doc comment even flagged the divergence ("`DrawSpritePrimitive` lui substitue
`s_SmallTextureFlag`... deux adresses différentes") without anyone following up on which side was
wrong. With `s_SmallTextureFlag` defaulting to `0`, every sprite that went through either wrong
branch — the portrait cards among them — silently returned without drawing. Fixed by replacing
`s_SmallTextureFlag` with `s_Unknown_00564a4c` in both functions' guards (three call sites total).

Confirmed visually, not just by primitive count this time: the capture now shows all fifteen
non-selected characters' portrait cards scattered behind the model, matching the user's reference
screenshot. Primitive count also landed exactly on the historical baseline (382.3/frame), but that
number is corroborating evidence now, not the sole proof.

### Remaining

Sub-state 7's **entire render path is done**, and so — as best confirmed — is the character
carousel's. Sub-state 2 (143 lines, back-step only) is still open and unrelated. Worth a sweep of
other `DrawSpritePrimitive`/`DrawFlatPrimitive` call sites now that the guard is fixed — anything
else that silently no-op'd behind `s_SmallTextureFlag` should start drawing too. The memory-card
subsystem started this same day — see the next entry.

## 2026-08-16 — the memory-card subsystem: not PS1 hardware, just Win32 file I/O — case 1 done

User committed the day's rendering fixes and asked to keep going on the save screen. The one
remaining blocker was the memory-card subsystem behind `ThrowSaveCardUnresolved`, previously scoped
as "its own dedicated session." Started that session.

### It isn't a memory-card emulator — it's `CreateFileA` on a `save\` folder

The name ("PS1-BIOS-flavoured") oversold it. Reading `winsys3d.c`'s actual bodies for ordinals
9/21/22/23/24/25/67/68/69/72 (`FUN_00a041d0`/`04600`/`04650`/`04660`/`04680`/`046b0`/`050d0`/
`050f0`/`02e20`) shows ordinary `CreateFileA`/`CloseHandle`/`SetFilePointer`/`ReadFile`/`WriteFile`
wrappers, plus a device-path builder (`FUN_00a03f80`) that reads two static ANSI strings —
`"save\"` and `"Blaze-Saving*"` — recovered directly from `Legacy/Binaries/Blaze.exe` (`DAT_00563158`, file
offset `0x161758`). This PC port never emulates real memory-card hardware; it just writes ordinary
files to a `save\` folder next to the executable.

### A real arity trap, caught before it became a native crash

Ordinals 67, 68, 69 and 72 all share one body (`FUN_00a050f0`/`00a050d0`/`00a02e20`) that Ghidra
decompiled as taking **no parameters**, even though every call site passes one. Before writing the
P/Invoke declarations, read the raw bytes at each address in `winsys3d.dll` directly
(`OpenForesia.Tools -- at Legacy/Binaries/winsys3d.dll <va>`) and found `RET 4` on all four — genuinely one
stdcall parameter, silently ignored by the body (which just pushes hardcoded constants to a shared
dispatcher, `FUN_00a04270`, and returns a fixed value). This is exactly the "Ghidra arity" trap
`test-protocol.md` already warns about, caught here at declaration time instead of as a
`0xC0000005` after the fact. Ordinals 9/21/22/23/24/25's Ghidra-declared arities were all
cross-checked the same way and found correct.

Also confirmed from the same reads: ordinals 67/68/69 always return `1`, and ordinal 72 (shared by
~50 unrelated ordinals as a generic stub) always returns `0`. This degenerates the whole
async-transfer-status machinery in `FUN_00504690`/`FUN_005046c0`/`FUN_00504820` to something that
always reports "idle" — the real async worker thread (`FUN_00a04320`/`4390` in `winsys3d.c`) is
never created anywhere in the reconstructed call graph, so its completion callbacks never fire.
Transcribed the polling logic faithfully anyway (`PollCardWriteStatus`/`PollCardReadStatus`,
`PollCardTransfer` — Ghidra: `FUN_005046c0`/`FUN_00504770`/`FUN_00504820`), including the genuine
infinite-loop shape of the original when called in blocking mode with a status that never changes —
but guarded it behind a clear `NotImplementedException` instead of actually hanging, since nothing
currently exercises that path (case 1 always polls non-blocking) and a silent hang would be far
worse to debug later than a named exception now.

### A real, pre-existing bug found while wiring case 1: `DAT_00604980` vs `DAT_00604984`

Case 0 (already transcribed, working, committed in an earlier session) writes
`s_CardCursorSlot_00604984` — but the raw source (`Legacy/Raw/Blaze/FUN_0050a910.c:131-133`) writes
`_DAT_00604980`, a **different address**, four bytes earlier. Confirmed genuinely different fields,
not a decompiler aliasing artifact: case 1 (`Blaze.c`/raw line 138: `_DAT_00604984 = 0;`) zeroes
`DAT_00604984` at its very start, then separately reads `DAT_00604980` later
(`(DAT_00604980 & 3) + (DAT_00604980 & 4) * 4`) to select the save slot for the file open — if
these were the same field, that read would always see `0`. Case 0's existing write was simply
aliased to the wrong one of the two, unnoticed until this session needed the real
`DAT_00604980` value for case 1. Fixed by adding a proper `s_CardSlotIndex_00604980` field and
repointing case 0 at it; `s_CardCursorSlot_00604984` (case 0xa's port-cursor navigation, and case
1's own zeroing) is untouched.

### Case 1 — transcribed and confirmed working in-game

`SetCardPathDeviceFields`/`SetCardFileName` (Ghidra: `FUN_005128e0`/`FUN_00512900`) build the path
descriptor; `RetryOpenCardFile`/`RetryCloseCardFile` (`FUN_00504b40`/`FUN_00504b70`, ten attempts
each) open/close it. Both retry wrappers are declared `void` by Ghidra but their call sites use the
return value (`iVar4 = func_0x00504b40(...)`) — the same "EAX survives past an unwritten return
statement" pattern seen elsewhere in this project; declared `IntPtr`/`int` here instead of trusting
the `void`.

Confirmed at runtime, `OPENFORESIA_FORCE_SUBSTATE=7`: no crash, no exception from case 1 itself —
it correctly detects that no save file exists yet (expected: this port's `save\` folder is empty)
and transitions cleanly to case 6 (**not yet implemented** — a new, clean `NotImplementedException`
naming it precisely), rather than the old blanket case-1 throw. This is the intended kind of
progress: the *next* real gap, reached through the real dispatcher.

### Remaining (superseded — see 2026-08-16 case 6 entry below)

Case 6 (`FUN_0050a910.c:198-338`) is the write path — building a full save-file blob (a 4-byte
`"SC\x11\x03"` header, name, a Shift-JIS-converted title via `func_0x0050a810`, a class-name string
padded to `0x40` bytes, a save-icon thumbnail captured from VRAM via `func_0x00545ac4`/`abe`
— presumably wrapping ordinal 684, `FUN_00a05f50`, already read and confirmed to be a VRAM
rectangle copy, not file I/O — a checksum summed over `0x1ffc` bytes, then four
`FUN_00504c20` seek+write+verify calls) — a substantially bigger, self-contained unit than case 1,
deliberately not started this pass. Cases 4, 0xd, 0x10, -5, -3/-2 remain behind
`ThrowSaveCardUnresolved` too, each smaller than case 6 but each with its own sequence to read.

## 2026-08-16 — case 6 (save write): transcribed, and a real end-to-end save file confirmed on disk

Continued straight from case 1. Transcribed `WriteCardSaveFile` (Ghidra: the `case 6` body of
`FUN_0050a910`, `Legacy/Raw/Blaze/FUN_0050a910.c:198-338`) faithfully: header, `LabelAt(0x134)`
title, up to 16 Shift-JIS-encoded name characters, class name via `LabelAt(0x138 + (class&7)*4)`,
zero-pad to `0x40`, `0x1c` zero bytes at `+0x44`, the VRAM icon capture (`Ordinal_684`, twice, same
rect both times — see the `TODO(REVERSE)` on the function about the second call's stack-address
discrepancy in Ghidra's forced decompile), `0x2000` zero bytes at `+0x2000`, the icon-frame
compaction loop at `+0x4000` (degenerates to an all-zero table here since nothing populates
`+0x300` yet — transcribed as-is, not invented), a signed-byte checksum over the first `0x1ffc`
bytes, then four `RetrySeekAndWriteCardFile` calls and a close, branching to phase 99 (error) or
phase `0x11` (success).

Also transcribed `NameEncodeChar` (Ghidra: `FUN_0050a810`) — the ASCII/Latin-1 → Shift-JIS
half/full-width encoder for the on-disk name field — using the three data tables read directly out
of `Legacy/Binaries/Blaze.exe` (`s_NameEncodeTable_00562b52` at file offset `0x161152`, plus the two accent
tables). Hand-verified against known Shift-JIS full-width digit codes (`'0'` → `0x4F82`, `'1'` →
`0x5082`) before trusting it.

### The `Ordinal_21` "mystery" — not a bug, a test-harness artifact

First pass at testing case 6 via `OPENFORESIA_FORCE_SUBSTATE=7` showed `Ordinal_21` (the
`CreateFileA` wrapper `FUN_00a04600` in `winsys3d.c`) consistently failing (`handle=0xffffffff`)
for the save-write open, while an equivalent direct `CreateFileA` P/Invoke with the identical path
and flags succeeded reliably. Spent a long stretch on this: re-verified `SetCardPathDeviceFields`
by disassembling `FUN_005128e0` directly at file offset `0x111ce0` (confirming it copies the
**literal bytes** `"save\"` into the descriptor, not pointers — the earlier implementation had it
backwards and got a garbage path; fixed), ruled out a stale/mismatched `winsys3d.dll` copy
(`md5sum` on all three copies in the repo: identical, `c2f82301d9b68f010770eb25ec6f27b8`), and ruled
out the specific `access=0`/`CREATE_ALWAYS` flag combination being rejected by modern Windows
(direct `CreateFileA` with that exact combination succeeded).

The actual answer was in `FUN_00a04600` itself, read more carefully:
`CreateFileA(path, (flags*4 | flags&2) << 0x1d, 0, 0, ((flags & 0x200) != 0) + 3, 0, 0)`. The
disposition term only ever evaluates to **3 (`OPEN_EXISTING`)** or **4 (`OPEN_ALWAYS`)**, never
`CREATE_ALWAYS`/`CREATE_NEW`. Case 6 calls `RetryOpenCardFile(descriptor, 2)` — bit `0x200` clear,
so `OPEN_EXISTING` — meaning it **requires the file to already exist**; it is case 1 that creates it
first (`RetryOpenCardFile(descriptor, 0x30200)`, bit `0x200` set → `OPEN_ALWAYS`) when its own
read-open fails, immediately before falling through to phase 6. This is exactly the original
PS1-derived flow: reserve/create the slot, then write into it. `Ordinal_21` and
`WriteCardSaveFile` were both already correct.

The reason it looked broken under test: the `OPENFORESIA_FORCE_CARD_NAME` scaffolding (needed
because the forced-substate test path skips real name entry, leaving the name field all zeros) was
injecting its test name **inside `WriteCardSaveFile`**, i.e. after case 1 had already pre-created a
file under the *old* (empty) name. Case 6 then correctly failed to `OPEN_EXISTING` a file that, under
the corrected name, had never been created. Moved the injection to fire once, before case 1 builds
the path descriptor (`Blaze.cs`, start of `case 1:`), so both phases agree on the same name — after
which a full run (`OPENFORESIA_FORCE_SUBSTATE=7`, `OPENFORESIA_FORCE_CONFIRM_STATS=1`,
`OPENFORESIA_FORCE_CARD_NAME=1`) produced a real 24576-byte (`0x6000`) file in `save\TEST`, exactly
matching the sum of the four write extents (`0x2000+0x1f00+0x100+0x2000`), with the expected
`"SC\x11\x03"` header and a non-zero computed checksum at `+0x1ffc`. All temporary trace/diagnostic
scaffolding from this investigation (`GetLastError`/`DirectCreateFileA` P/Invokes, the isolated-
descriptor probes, the hex-dump and path/handle/result traces) has been removed; only the
(permanent-style) `OPENFORESIA_FORCE_CARD_NAME` name injection remains, alongside
`OPENFORESIA_FORCE_SUBSTATE`/`OPENFORESIA_FORCE_CONFIRM_STATS`.

### Remaining

Cases 4, 0xd, 0x10, -5, -3/-2 remain behind `ThrowSaveCardUnresolved`, each smaller than case 6 but
each with its own sequence to read.

## 2026-08-16 — traced "stuck after saving" to the missing modal-confirm subsystem; reached a real new boundary (game state 1)

User reported the game stays stuck right after a save completes, and asked whether the save
filename is coherent with the character name. Both led somewhere useful.

### The filename is not the typed name — it's a 7-character stat-derived code, and that part is correct

`SetCardFileName`'s source (`s_SelectScratch_00573338+0x1fd0`) is populated by `WriteCharacterCode`
(Ghidra: the tail of `FUN_005098f0`, `case 0x5d`/`FinalizeCharacterStats`'s finalization path,
`Legacy/Raw/Blaze/FUN_005098f0.c:277-340`), **not** by whatever the player typed at the name-entry
screen. It's a checksum-style fold over the character's stats/class/element/style (`+= value; *= 2`
accumulator, then 5 bits at a time into 7 chars in `['@'..'_']`-ish range, `\` mapped to `'7'`),
written into `s_CharCodeScratch_00566bc0+0xc..+0x13`. Confirmed byte-identical control flow against
the raw decompile. So: two characters with identical stats/class/element/style **would** collide on
the same save file (a variant of LB-002, arguably less likely in practice than a name collision
since it also requires an identical build) — noted, not fixed, matches original.

**A real, still-open gap found while checking this**: `WriteCharacterCode` copies from
`s_CharCodeScratch_00566bc0[0]`, not `[0xc]` — matching the raw decompile exactly
(`cVar3 = *pcRam00566bc0;`, not `pcRam00566bc0+0xc`). Since nothing anywhere in the 98k-line
`Blaze.c` writes bytes `0..0xb` of this buffer, and our port zero-initializes it, the copy loop
exits immediately on the leading zero, before ever reaching the real 7-char code — the effective
filename is currently always empty in this port. Read the real on-disk bytes at that address
directly from `Legacy/Binaries/Blaze.exe` (file offset `0x165bc0`) to check whether they'd fix it: they're
**not zero** — `98 84 56 00 00 00 00 00 88 84 56 00 78 84 56 00 68 84 56 00 58 84 56 00 48 84 56 00
01 00 00 00`, a shape that reads as seven dwords `0x568498, 0, 0x568488, 0x568478, 0x568468,
0x568458, 0x568448` (spaced `0x10` apart — pointer-table-shaped) plus a trailing `1`. **Deliberately
not used as a fix**: `DAT_00566bc0` appears nowhere else in `Blaze.c`, so nothing confirms this
load-time snapshot is still what's there once gameplay actually reaches this code — the
pointer-table shape suggests an unrelated global sitting next-door in memory, whose real runtime
content at this point would come from code we haven't found. Using the disk bytes anyway would just
trade a known-wrong answer for a plausible-looking wrong one. Left at zero; documented in the field's
comment in `Blaze.cs` for whoever finds the real writer next.

### The actual "stuck" — a cross-cutting missing subsystem, not a save bug

Traced `case 0x12` of `StepSubState7Phase` (right after the one-frame `case 0x11` success message —
confirmed that one auto-advances every frame via the shared `pendingLabel != null` → phase-increment
tail, so it is *not* the stuck point). `case 0x12` waits for `confirmGate`
(`s_Unknown_005e909f`) to go nonzero before setting `s_GameStateId = 1` (leave character-select for
real gameplay). Searched all of `Blaze.c` for writes to `DAT_005e909f`: **every single one sets it
to 0**; nothing anywhere sets it nonzero. Same for `DAT_00560fef` (`dialogGate`) — checked in
`FUN_0050a910`'s own raw decompile, only ever bit-cleared, never bit-set, and the symbol doesn't
even appear in the normal `Blaze.c` pass at all (raw-redecompile-only symbol). This is the *exact*
same root cause already flagged in `TODO.md` §1 for the stat-allocation confirm screen
(`OPENFORESIA_FORCE_CONFIRM_STATS`'s reason for existing) — a generic modal "OK dialog" subsystem
that sets these two flags from real input is used all over the game (`s_Unknown_005e909f` alone has
~10 read sites across unrelated substates) and has not been located anywhere in the decompiled call
graph. Likely candidate for the "Ghidra re-run" bucket (an address-taken-only function, matching
`REVERSE_STATUS.md`'s systemic-blocker pattern) rather than something sitting unread in `Blaze.c`.

**Added a scoped test hook** rather than inventing behavior: `OPENFORESIA_FORCE_CARD_CONFIRM=1`
treats `confirmGate` as nonzero *only* inside this one `case 0x12` check, without touching the
global `s_Unknown_005e909f` (which many unrelated substates also read — forcing it globally would
have side effects well beyond this screen). Confirmed in game: with the hook set, the phase
progresses past `0x12`, `s_GameStateId` becomes `1`, and the game throws a new, clean, precisely
named exception — `RunGameStateTick` reaches `case 1` and throws
`"thunk_FUN_00536180 (game state 1) not yet reconstructed"` (`Blaze.cs:15656`). Per
`RunGameStateTick`'s own doc comment, every one of its eleven state handlers already has a
decompiled body in `Blaze.c` — genuinely just not transcribed yet, not blocked on anything. This is
real dungeon-crawling gameplay, a separate subsystem entirely from character-select/save, and a
natural next call-graph target whenever that's the priority.

Detail on the save-write investigation this same session: see the case 6 entry directly above.

## 2026-08-16 — hunting the generic "OK dialog" subsystem: real ground covered, real dead end

User asked to keep pursuing the missing modal-confirm subsystem (`OPENFORESIA_FORCE_CARD_CONFIRM`'s
reason for existing), since it gates several screens at once, not just the save screen. This section
is the full trail — worth reading before re-attempting any of it, so a future session doesn't
re-walk the same dead ends.

### Environment fix: the Ghidra project needed re-owning

`D:\Open Foresia Project\Ghidra Project\Open Foresia` still had `OWNER="Sagara"` in
`Open Foresia.rep\project.prp` (a plain XML property file, not analysis data) from the machine this
project was created on. `analyzeHeadless.bat` refuses to open a project it doesn't own
(`NotOwnerException`) even read-only. Fixed by editing that one attribute to `VALUE="Vincent"` — low
risk (plaintext metadata, not the disassembly database), confirmed working immediately after.

### Ghidra is runnable headless from this session, and a new re-run script now exists

`C:\Users\Vincent\ghidra_scripts\ForceDecompileConfirmDialog.java` — not committed (matches the
project convention that these scripts live outside the repo; only their source and recipe are
preserved here), takes a list of target addresses, disassembles + creates a function at each,
runs `analyzeChanges` so outgoing references resolve, decompiles, and writes `.c`/`.asm` to
`Legacy/Raw/Blaze/`. Invocation matches `TODO.md` §5's existing recipe:

```powershell
$gh = "D:\Open Foresia Project\Tool\ghidra_12.1.2_PUBLIC\support\analyzeHeadless.bat"
& $gh "D:\Open Foresia Project\Ghidra Project" "Open Foresia" -process "Blaze.exe" `
      -noanalysis -readOnly -scriptPath "C:\Users\Vincent\ghidra_scripts" `
      -postScript ForceDecompileConfirmDialog.java
```

### Finding `FUN_0053dd20`: the only real write to confirmGate anywhere in the binary

Scanned the whole `.text` section (`0x401000`-`0x54c400`) for every instruction referencing
`DAT_005e909f`'s address (35 total) and classified each by its preceding opcode bytes (read vs.
write, and — for writes — the literal value). Exactly one writes a nonzero value:
`C6 05 9F 90 5E 00 01` (`MOV byte ptr [0x5e909f],1`) at VA `0x53DE43`, sitting in a gap between two
already-known functions (`FUN_0053dc30` ends well before it, `FUN_0053e0a0` starts at `0x53e0a0`) —
never disassembled as code in the normal pass. Manually found the real function start at `0x53DD20`
(a clean `PUSH EBX; MOV ECX,[...]; PUSH EBP; PUSH ESI; PUSH EDI` prologue) and force-decompiled it
with the script above. Also found and decompiled its two siblings, `FUN_0053dea0`/`FUN_0053df20`,
via a second raw-byte scan: the same three addresses (`0x53DD20`/`0x53DEA0`/`0x53DF20`) appear
twice, 0x20 bytes apart, inside a much larger function-pointer table in `.data` at `0x553480`+
(mostly filled with a shared default handler, `0x00401fa0`).

**What the three actually do, and why none of them is the fix**: `FUN_0053dd20` scans up to 10
records of `0x280` bytes starting at `MainMem+0xf0000+index*0x2000+0x300` (index 0 lands exactly on
`s_SelectScratch_00573338+0x300`, the same icon-frame table `WriteCardSaveFile` compacts) for one
matching, claims it, formats its name via `FUN_0051bd30`/`FUN_0051bcc0` (small, self-contained,
not yet transcribed) into a label slot at `*(int*)(DAT_005e97cc+0x70)+4`, fills
`s_Unknown_005e909a..e`, and only then sets confirmGate. `FUN_0053dea0`/`FUN_0053df20` are its two
"entry mode" initializers (`DAT_005de548=0` or `1`), consumed by the huge `FUN_0053e0a0` — read in
full and confirmed to be a genuine, pad-driven, unrelated menu handler (up/down navigation with
`FUN_0053db60` sounds, item-slot browsing) that has nothing to do with character-select's own state
fields (`s_CardPhase_00578e0c`/`s_SelectState_00578de8` never appear in it). Two independent reasons
this cannot be the save-screen's mechanism even if wired in: **(1)** `DAT_005e97cc` — a *second*
resource directory FUN_0053dd20 needs — is never initialized anywhere in this port (only referenced
by real-gameplay code, ~11 call sites, likely populated by game-state-1 init,
`thunk_FUN_00536180`, itself not reconstructed); calling through it today is a null-pointer crash,
not a fix. **(2)** `RenderSaveScreen` already writes `s_Unknown_005e909a..e` unconditionally every
frame with values derived from the current character, so anything `FUN_0053dd20` computed there
would be overwritten immediately after. Concluded: real code, real understanding gained, but the
wrong subsystem — a coincidental reuse of the same global byte, 1998-memory-economy style. Not
called from anywhere in `Blaze.cs`.

### Two exhaustive dead-end scans: neither confirmGate nor dialogGate has a real "set" site by literal address

Repeated the same byte-scan-and-classify technique for `DAT_00560fef` (dialogGate): 63 occurrences
in `.text`, every one either a read or the already-known `AND al,0xFD/0xFB/0xF9` mask-clear pattern
(matches `case -4` and the dead `iRam00560fd4` branch, both already correctly transcribed). No
occurrence sets a bit. Combined with confirmGate's own scan, this means: **searching for literal
4-byte address references in `.text` — which covers direct/absolute addressing, `MOV reg,imm32`,
and `LEA reg,[imm32]` alike — finds no code anywhere that arms either flag.**

### The actual reason the literal scan was blind: both flags are struct fields, not just bare globals

User's instinct (work from the raw machine code, not Ghidra's inferred C) pointed at the right
question and immediately paid off. `DAT_00560fef` is exactly `0x560728 + 0x8c7`, and `0x560728` is
`s_StatusDescriptor_00560728` — the descriptor already passed everywhere via `BindMenuLabels`. Once
a compiler (or Ghidra) treats an access as `struct_base_register + 0x8c7` rather than a folded
constant, the literal bytes `EF 0F 56 00` never appear in the instruction at all, so no amount of
literal scanning can find it. This immediately explained a second thing: `FUN_00544350`
(`BindMenuLabels`, already transcribed) itself contains a real, direct write —
`*(byte*)(param_3+0x8c7) = *(byte*)(param_3+0x8c7) | 1;` — setting **bit 0** of dialogGate on every
single label bind. Confirmed already present, correctly transcribed, in `BindMenuLabels`.

### `DrawUiPanel` (`FUN_00545340`) has a real, pad-driven "confirm cursor" — and it's already wired to the right descriptor

Re-read `DrawUiPanel`'s already-transcribed body (`Blaze.cs:12679`) end to end rather than skimming
it, and found the "---- the confirm cursor ----" block (`Blaze.cs:12821-12832`, already commented
as such): gated on `cursorFlags = descriptor[0x30]` having bit `0x1000000` set, it reads a **real
pad-edge check** — `*(ushort*)(s_InputStateBase_00604a20 + buttons + 2) & 0x8000` — the genuine
controller input table, not a stand-in — and on a real press clears **bit 1** of `descriptor[0x8c7]`
(`&= 0xfd`) plus plays `PlayUiSound38()`. Bit 1 is exactly what `case 0x11`/`case 4` test
(`bVar1 & 2`). Checked every call site of `FUN_00545340` in `Blaze.c`: one of them
(`FUN_00506880`, line `55882`) calls it with `&DAT_00560728` — the real dialogGate descriptor —
and `FUN_00506880` is `RunCharacterSelectFrameLoop`, already reconstructed. Confirmed the
equivalent call is **already present and unconditional, every frame**, in our port
(`Blaze.cs:6831`: `primCursor = DrawUiPanel(otBase, primCursor, s_StatusDescriptor_00560728);`).

**The dead end**: the confirm-cursor branch only runs if `s_StatusDescriptor_00560728[0x30]` has bit
`0x1000000` set, and nothing sets it. Read the real static bytes at that exact address
(`Legacy/Binaries/Blaze.exe`, VA `0x560758`) directly: `0x000F28D4` — bit 24 clear. Grepped both `Blaze.c`
and every `Legacy/Raw/Blaze/*.c` for any write to `0x560758`: none. So even on its real,
already-correctly-wired path, this specific descriptor's confirm cursor cannot currently activate —
neither the code nor the static data arms it. Whatever *does* arm it in the original (almost
certainly the same not-yet-located subsystem, or a sibling of `FUN_0053dd20`'s never-analyzed gap)
remains unfound.

### Where this leaves things

The generic "OK dialog" subsystem is now understood in far more detail than before — confirmed to
be a real, pad-driven, otherwise-fully-wired mechanism (`BindMenuLabels` arms bit 0, `DrawUiPanel`'s
confirm cursor clears bit 1 on a real button edge, both already correctly transcribed and connected
to the right descriptor) — but its *activation switch*
(`s_StatusDescriptor_00560728+0x30`'s bit `0x1000000`) has no discoverable writer anywhere in
`Blaze.c`, any existing `Legacy/Raw/Blaze/*.c`, or a from-scratch `.text` literal-address scan.
`OPENFORESIA_FORCE_CARD_CONFIRM` remains the pragmatic way to test past `case 0x12`. A future
attempt should assume the writer is in another Ghidra-unanalyzed gap (same shape as
`FUN_0053dd20`'s), and would need either a live debugger (to catch the actual write at runtime) or a
full (not `-noanalysis`) Ghidra re-analysis pass to have a chance of surfacing it — static
literal-address scanning has now been tried twice and is exhausted for this specific question.

## 2026-08-16 — a real hardware breakpoint (x32dbg, user's Windows 10 machine) resolves the whole mechanism, and reframes the "stuck" screen entirely

`Legacy/Binaries/Blaze.exe` does not run on this machine's Windows 11 (untested territory, still unresolved),
but does on the user's other, Windows 10 machine. Set a hardware write breakpoint on
`0x00560758` (`s_StatusDescriptor_00560728+0x30`) there with x32dbg and drove the real 1998 game
through character creation to the save screen. Two runs, both archived under
`Analyse/x32dbg/POINT_ARRET_0x00560758/` (first attempt, then "Tentative 2").

### First hit: a save/restore round-trip inside `RunTextEngine` itself — a red herring, but a confirming one

The breakpoint's first, most frequent hit was `0x005445B2`, a `rep movsd` copying `0x232` dwords
(`0x8c8` bytes — exactly the descriptor's size) from a stack-local buffer into
`s_StatusDescriptor_00560728`. Traced this address to inside `FUN_005443a0` (`RunTextEngine`,
already transcribed) via `Blaze.c:91112-91118` (a snapshot of the *live* descriptor into
`local_8c8`, taken at the function's own entry) and `:91155-91162` (the exact matching restore,
right before the label the breakpoint caught). Since the source of this specific write is the
function's *own* entry-time snapshot of the *same* field, this hit doesn't introduce a new value —
it just proves `RunTextEngine`'s save/restore shape is exactly what the decompiled `Blaze.c` already
showed, and explains why it fires so often (once per label processed, many per frame).

### Second hit, after conditioning the breakpoint (`bphwcond 560758,eip!=5445B2`): the real writer

A second, rarer hit landed at `0x00544E5E` (`or al,2`), inside a **6-line, never-before-seen
function at `0x00544E50`** — sitting, like `FUN_0053dd20` before it, in an unanalyzed gap between
two already-known functions (`FUN_00544d90` ends at `0x544E4D`, `FUN_00544fb0` starts at
`0x544fb0`). Force-decompiled it with the same Ghidra headless recipe:

```c
void FUN_00544e50(undefined4 param_1,int param_2)
{
  *(undefined1 *)(param_2 + 0x33) = 1;
  *(byte *)(param_2 + 0x8c7) = *(byte *)(param_2 + 0x8c7) | 2;
  *(undefined4 *)(param_2 + 0x8ac) = param_1;
  return;
}
```

`param_2+0x33` is the **top byte of the dword at `+0x30`** — writing `1` there turns
`0x000f28d4` into `0x010f28d4`, i.e. sets exactly the `0x1000000` bit `DrawUiPanel`'s confirm-cursor
block requires. `param_2+0x8c7 |= 2` sets **bit 1** of dialogGate — the same bit `case 0x11`/`case 4`
test. Confirmed via a raw-pointer scan of the whole file that this function is referenced, twice, as
data — at index **15 (`0x0f`)** in *both* `PTR_LAB_0056e870` and `PTR_FUN_0056e728`, the two
32-entry control-code dispatch tables `RunTextEngine` already indexes by an in-band byte (`< 0x20`)
found while walking a label's text. So the whole mechanism is **data-driven**: a label string
carrying byte `0x0f` embedded in it is what arms the confirm cursor, not a separate call from the
screen's own logic.

**Already fully and correctly transcribed, from an earlier session, before any of today's
investigation**: `Blaze.cs`'s `BuildTextTable` already has `t[0x0f] = Handler_00544e50;`
(`Blaze.cs:11954`), and `Handler_00544e50` (`Blaze.cs:11672`) matches the real disassembly exactly,
down to a doc comment already stating "code `0x0f` therefore means stop here and show the cursor —
a prompt awaiting a choice, which is what the character-select screen reaches on validating a
character." Combined with `BindMenuLabels` (arms bit 0) and `DrawUiPanel`'s confirm-cursor (already
wired to `s_StatusDescriptor_00560728`, every frame, `Blaze.cs:6831`) — **every piece of this
mechanism was already correctly built.** Today's contribution is knowing what the pieces are *for*,
plus one thing that was still missing: the physical button. Cross-checked `OpenForesia.cfg`'s
`[Controls]` section — action slot 7 ("square", pad mask `0x8000`, the bit `DrawUiPanel` tests) is
bound to scancode `29` = **Left Ctrl**. Added an `LCTRL` token to `playtest.ps1`'s `-Then` (`0x1D`,
non-extended) since only `ENTER`/`ESC`/arrows existed before.

### Testing it exposed the real remaining gap — and it reframes "stuck" entirely

Pressing the *correct* button (`LCTRL`) still didn't reach game state 1. Added two temporary traces
(`OPENFORESIA_TRACE_CARDPHASE`, in `StepSubState7Phase` and `RunCharacterSelectFrameLoop`) and found
`s_StatusDescriptor_00560728+0x30` (`cursorFlags`) **never changes from its static default
`0x000f28d4`** across the whole run — meaning `Handler_00544e50` never actually fires, meaning the
real resource text for the label shown at `case 0x11` (`LabelAt(0x1d8)`) apparently does not carry
the `0x0f` byte in this port (or `case 0x11`'s one-frame lifetime, see the case-6 entry above, never
gives `RunTextEngine` a chance to reach it — not yet distinguished).

**But the far more important finding**: after a few frames stuck at phase `0x12` with
`confirmGate` staying `0`, `s_SelectState_00578de8[0]` (the *top-level* substate index) silently
becomes `1` — i.e. `CharacterSelectSubState7`'s own `case 0x12` (`Blaze.cs:9109-9130`) is doing
exactly what its code says: `if (!confirmed) { state[0] = 0; state[2] = 0; }` resets the whole
screen back to the character carousel every single frame `confirmGate` is zero. Given yesterday's
exhaustive scan already proved `confirmGate` (`DAT_005e909f`) has **no other writer anywhere in the
game reachable from character creation** (its one real setter, `FUN_0053dd20`, belongs to an
unrelated item-menu subsystem — see the entry above), **the original, shipped 1998 game almost
certainly takes this exact same reset-to-carousel branch too.** This may not be a bug or a gap at
all: returning to the carousel after saving, rather than continuing straight into gameplay, could be
the actual, intended design — with actually *starting* the game a separate action taken from
substate 0 (the carousel) on an already-saved character slot, a path this session never exercised.

### Where this leaves things

- The generic "OK dialog" mechanism (`BindMenuLabels`, `Handler_00544e50`, `DrawUiPanel`'s confirm
  cursor) is now **fully understood and confirmed already correctly transcribed** — nothing left to
  fix there.
- `OPENFORESIA_FORCE_CARD_CONFIRM` was masking the real behavior rather than working around a gap:
  forcing `confirmGate` nonzero made the save screen jump straight to game state 1, which the real
  game may never do from this exact point.
- **Next real question, not yet investigated**: does substate 0 (the character carousel) have its
  own path from "select an already-saved character slot" to `s_GameStateId = 1`? That would be the
  actual, correct way to reach real gameplay, and is a different call graph entirely from anything
  this session traced.
- A genuine, separate, still-open bug surfaced as a side effect of this testing: reaching `case 1`
  of `StepSubState7Phase` when a save file *already exists* for the current name (the "load" branch,
  `ResetCardTransferStatus` → `Ordinal_67`) crashes with `0xC0000005` — never exercised before today
  since every prior test used a fresh, empty `save\` folder. Not investigated further this session;
  reproduce with a pre-existing `save\TEST` file and `OPENFORESIA_FORCE_CARD_NAME=1`.
- Two temporary diagnostics remain in `Blaze.cs`, gated behind `OPENFORESIA_TRACE_CARDPHASE`
  (`StepSubState7Phase` and `RunCharacterSelectFrameLoop`) — remove once the carousel-path question
  above is settled, per the project's scaffolding convention.

## 2026-08-16 — root cause found and fixed: five "globals" were never aliased to the real descriptor memory

User pushed back on the "maybe it's intended design" theory with a concrete, first-hand behavioral
reference (`Analyse/Comportement Legacy/Selection des personnages.md` — every dialog box's exact
text and the input flow through the whole character-creation sequence, recorded from the real 1998
game). Section VI (the save screen) confirms `case 0x11`/`case 0x12` is a genuine **two-line Oui/Non
choice** ("Définir de nouveaux personnages" / "Constituer un groupe"), not a plain
press-to-continue — which reopened the question properly rather than closing it as "working as
intended".

### Second hardware breakpoint (write to `0x005E909F`, `confirmGate` itself) finds the real writer — and the real bug

`Analyse/x32dbg/POINT_ARRET_0x005E909F/` — breakpoint hit inside `DrawUiPanel`
(`FUN_00545340`, VA `0x5455F4`, `mov byte ptr [edx+5E9060],al` with `edx=0x3f`), in a section of
that function **already transcribed** in `Blaze.cs` but that a first read had skipped over: the
"scroll arrows" widget's confirm-commit step (`Blaze.cs:12804-12822` before this session's fix),
already correctly writing `s_PanelSelection_005e9060[descriptor[0x8c5]] = descriptor[0x8c4]` (three
variants depending on flag bits — halved, divided by three, or verbatim). `Handler_00544b10`
(control code `0x09`, also already correctly transcribed, `Blaze.cs:11658`) is what arms this
widget and sets `descriptor[0x8c5]` from an operand byte in the label text — for a prompt whose
result should land on `confirmGate`, that operand byte is `0x3f`.

**The actual bug**: `0x5e909f - 0x5e9060 = 0x3f` — `DAT_005e9060` and `DAT_005e909f` are **the same
contiguous memory** in the original (a 0x100-byte table; `confirmGate` is just entry `0x3f`). This
port had them as two *separate* allocations — `s_PanelSelection_005e9060` (a real `NativeMemory`
buffer, already correctly written by `DrawUiPanel`) and `s_Unknown_005e909f` (an independent `byte`
field that `StepSubState7Phase` and its four sibling substates read). Writing one never touched the
other. Every `if (confirmGate == ...)` check in the whole character-select flow was reading a byte
that the real widget code could never reach — a genuine aliasing bug, not a missing mechanism.

**Checked for siblings rather than patching just this one field**: `s_Unknown_00560fef`
(`dialogGate`) and two more single-byte fields, `s_Unknown_0056075b`/`s_Unknown_00560763`, had the
exact same problem. `0x560fef - 0x560728 = 0x8c7`, `0x56075b - 0x560728 = 0x33`,
`0x560763 - 0x560728 = 0x3b` — all three are fields of `s_StatusDescriptor_00560728` (the descriptor
`BindMenuLabels`/`RunTextEngine`/`DrawUiPanel` already operate on correctly), declared a second time
as standalone globals that the *reading* side (all the character-select substates) used instead.
Same for `s_Unknown_005e909a..e` (`0x5e909a..e - 0x5e9060 = 0x3a..0x3e`) — the same
`DAT_005e9060` table, adjacent entries to `confirmGate`.

### The fix

All six fields are now `ref`-returning properties into the real backing storage instead of
standalone `byte`s:

```csharp
private static ref byte s_Unknown_00560fef => ref s_StatusDescriptor_00560728[0x8c7];
private static ref byte s_Unknown_0056075b => ref s_StatusDescriptor_00560728[0x33];
private static ref byte s_Unknown_00560763 => ref s_StatusDescriptor_00560728[0x3b];
private static ref byte s_Unknown_005e909a => ref s_PanelSelection_005e9060[0x3a];
// ...905e909b..e likewise...
private static ref byte s_Unknown_005e909f => ref s_PanelSelection_005e9060[0x3f];
```

Every existing call site (~40 across `StepSubState7Phase` and the substate-1/3/4/6 siblings) needed
no changes at all — reads and `= value` writes both work transparently through a `ref`-returning
property in an unsafe context, since the right-hand side is a real pointer dereference either way.
Removed `OPENFORESIA_FORCE_CARD_CONFIRM` and the `OPENFORESIA_TRACE_CARDPHASE` diagnostics that were
scaffolded around the *symptom* now that the *cause* is fixed.

### Confirmed in game

Rebuilt, cleared `save\`, ran the full create-character-and-save flow with no force hooks beyond the
existing `OPENFORESIA_FORCE_SUBSTATE`/`OPENFORESIA_FORCE_CONFIRM_STATS`/`OPENFORESIA_FORCE_CARD_NAME`
(none of which touch this mechanism) — the save screen now reaches `case 0x12`, reads a real
(non-stale) `confirmGate`, and transitions to `s_GameStateId = 1`, landing on the clean
`"thunk_FUN_00536180 (game state 1) not yet reconstructed"` exception. Reproduced twice.

This is a systemic fix, not a save-screen-only one: `dialogGate`, `confirmGate`, and the three
adjacent fields gate the "OK"/"Oui-Non" dialog flow on **every** character-select substate (0, 1,
3, 4, 5, 6, 7) — the desynchronized dialog boxes the user had been observing (text/state not
matching what the game was actually doing) should be fixed everywhere this pattern occurs, not just
on the save screen. Worth a full pass replaying `Analyse/Comportement Legacy/Selection des
personnages.md`'s documented flow end to end to confirm each screen now matches.

## 2026-08-16 — same aliasing bug, two more instances, found by replaying the reference document

User committed the fix above, then replayed `Analyse/Comportement Legacy/Selection des
personnages.md`'s flow and reported the "Bonus" screen's point counter stayed at `0` even though
points could still be spent afterward — the same *symptom shape* as the confirmGate bug (logic
works, display doesn't), so checked for the same *cause* first.

### `s_StatTotal_005e906a` — the bonus total

`0x5e906a - 0x5e9060 = 0xa`. Same table as `confirmGate`. `Handler_005449f0` (control code `0x06`,
decimal number display, already correctly transcribed) reads
`s_PanelSelection_005e9060 + text[0]` — so any label whose embedded operand byte is `0xa` was
already reading the right memory. The dice-roll accumulation (`Blaze.cs`, sub-state 5) wrote to a
standalone `sbyte s_StatTotal_005e906a` field instead, never touching the table. Point allocation
later in sub-state 6 read/wrote that same standalone field consistently with itself, which is why
spending points worked — it just never showed a value the display could see.

Fixed the same way: `private static ref byte s_StatTotal_005e906a => ref
s_PanelSelection_005e9060[0xa];`. One call site (`Blaze.cs:7359`) needed a cast
(`s_StatTotal_005e906a = (byte)(s_StatTotal_005e906a + sum);`) since the local accumulator `sum` is
`sbyte` and compound `+=` against a `ref byte`-returning property doesn't implicitly narrow the way
a plain field did.

**Confirmed visually** (`playtest.ps1 -Shot`, natural flow, `-WorkingDir OpenForesia`): "Bonus"
reads `15` on first reaching the screen (the dice-roll starting value), and `24` after revealing
three cards summing to `9`.

### Control code `0x14` → `FUN_00544fb0`: a genuine, previously-unimplemented gap, not aliasing

Separately, reaching the three-identical-cards "Bonus spécial !" case threw
`System.NotImplementedException: UI text control code 0x14 dispatches to FUN_00544fb0`.
Unlike most of its `0x544xxx` neighbours, `FUN_00544fb0` has a **complete, normal Ghidra
decompilation** already sitting in `Blaze.c:91445` — no re-run or hand-disassembly needed this
time:

```c
undefined1 * FUN_00544fb0(undefined1 *param_1,int param_2)
{
  char cVar1;
  if (*(char *)(param_2 + 0xe) != '\0') {
    FUN_00543a10(param_2);                                       // EmitTextSprite
    cVar1 = *(char *)(param_2 + 0xe);
    *(undefined1 *)(param_2 + 0xe) = 0;
    *(char *)(param_2 + 0xc) = *(char *)(param_2 + 0xc) + cVar1;
    DAT_005e02fc = DAT_005e8304;                                  // break-staging snapshot
    _DAT_005e8300 = DAT_005e8320;
    DAT_005e8328 = (uint)*(byte *)(param_2 + 0xe);                // always 0, just cleared above
  }
  *(undefined1 *)(param_2 + 0x22) = *param_1;
  return param_1 + 1;
}
```

Every global it touches already had an established symbol from earlier text-engine work
(`EmitTextSprite`, `s_BreakStaging_005e02fc`, `s_StagingWrite_005e8304`, `s_PenX_005e8320`,
`s_BreakColumn_005e8300`, `s_BreakPen_005e8328` — the same break-point bookkeeping `FlushLine`
reads back on a word-wrap), so this was a mechanical transcription, not new investigation. Registered
as `Handler_00544fb0`, render-pass-only table slot `0x14` (the scan pass already used the generic
"skip one operand byte" stub there, matching this handler consuming exactly one).

`descriptor[0x22]` is the upper word of the same `+0x20` "rect" dword `Handler_005449b0` (code
`0x04`) writes the lower word of, and `EmitTextSprite` reads whole — bits 0-3 fold into the
sprite's page term, bits 5-6 into its blend-mode term. Consistent with this code appearing
specifically in text the reference document notes as rendered in gold.

**Confirmed visually**: added a scoped, temporary test hook
(`OPENFORESIA_FORCE_TRIPLE`, at the `f0==f1==f2` check in sub-state 5) to force the triple-match
branch without depending on the frame-timing-seeded dice RNG landing on it by chance; captured a
screenshot showing "Bonus spécial !" in gold followed by "Tu peux retourner les cartes encore une
fois." — byte-for-byte the same text the user's reference document recorded from the real game,
**literal mid-sentence `n` included** (so that is a faithful transcription of the original's own
text data, not a line-break bug). Removed the test hook once confirmed, per the project's
scaffolding convention.

## 2026-08-16 — third visual bug fixed (name-entry cursor icon), then the first step into game state 1

### The name-entry keyboard cursor showed the wrong sprite

User spotted an earth-element icon sitting over the "A" key on the name-entry virtual keyboard.
`DrawNameEntryScreen`'s cursor sprite (`Blaze.cs`, ~line 10101) built its atlas pointer from
`s_MainResourceDirectory_005e97c4 + 0x94` — but the real disassembly (`FUN_00507cb0.asm:403-408`,
`MOV EDX,[0x005e97c0]` then `+0x94` then one more dereference) reads from
`s_MainResourceBuffer_005e97c0` instead, the same symbol `DrawUiPanel` already uses correctly for
the identical `+0x94` lookup. Two separate, real globals 4 bytes apart — using the wrong one landed
the read on an adjacent atlas entry. One-line fix (swap the symbol), confirmed visually: the cursor
now shows the expected pointer-hand icon and the "A" is visible.

### First step into game state 1 (real gameplay) — RunGameState1Entry

User asked to continue past the save screen into game state 1 itself (`thunk_FUN_00536180`, never
touched before this project). Traced the call graph with two research agents before writing
anything:

- `thunk_FUN_00536180` (`0x536260`) is byte-for-byte identical to `FUN_00536180` (`0x536180`) — the
  usual linker-folded duplicate.
- Its two direct helper calls, `FUN_00504ca0` and `FUN_00529a80`, turned out to already be **fully
  available**: `FUN_00529a80` is already transcribed as `SetupMenuScreen`, and `FUN_00504ca0` is
  **byte-for-byte the same directory-reload prefix** `LoadMenuResources` (`FUN_00542160`) already
  runs, just without that function's menu-specific tail. Extracted the shared prefix into
  `ReloadStateResources`, added `ReloadGameplayResources` as the thin wrapper `FUN_00504ca0` needs,
  and left `LoadMenuResources` calling the same shared method for its own use.
- Also discovered along the way: `DecodeResourceChunk` (`FUN_005322f0`) — long tracked in this
  file's older entries as the project's one deliberately-unrecoverable "systemic blocker" — has in
  fact been **fully reconstructed** in an intervening session not covered by this one's context.
  Worth updating any stale "STUB, throws" notes elsewhere in this file the next time they're
  touched; it is not a blocker for anything anymore.
- The branch between `FUN_00536270`/`FUN_00536ea0` needed one more not-yet-decoded idiom: the
  "current player slot" pattern already settled for `PollInputAndAudio`
  (`s_SlotArea_005e9180[0x248 + slot*0x9c]` then `s_PlayerSlots_005e9188[...]`) reused verbatim
  here, plus `s_UnknownModeFlag_005633a8` (already known from the sub-state-0 free-camera flag).

Transcribed `RunGameState1Entry` faithfully: the block of sixteen miscellaneous globals reset to
idle values (first encountered here, roles not yet established, declared as `s_Unknown_*` rather
than guessed), the two now-available helper calls, the `DAT_005de51c` branch (kept as a named
`NotImplementedException` for `FUN_00537a20` — currently unreachable in practice since every known
writer of that flag sets it to `0` immediately before entering this state), a `0x8000`-byte clear at
`s_SelectScratch_00573338` (note: **0x2000 dwords, not the 0x2000 bytes this project has assumed
that scratch block's extent to be elsewhere** — the base address is confirmed the same, just a
larger true extent than previously needed), and finally the branch to `FUN_00536270` or
`FUN_00536ea0`.

**Confirmed in game**: the full create-character-and-save flow now runs past the save screen,
through `RunGameState1Entry` end to end with no crash, and lands cleanly on
`"thunk_FUN_00536180: FUN_00536ea0 (game state 1, path B) not yet reconstructed"` — a new, one
function deeper, precisely named boundary.

### Scope of what's next

Surveyed (not transcribed) `FUN_00536270` (361 lines, `Blaze.c:82277-82637`) and `FUN_00536ea0`
(494 lines, `Blaze.c:82764-83257`) with research agents. Every function either one calls resolves to
a real line in `Blaze.c` — no Ghidra gaps detected in this immediate scope — and the two share many
call targets. Most of those targets are **already reconstructed**: all five `FinalizeCharacterStats`
helpers, `BindMenuLabels`, `RunTextEngine`, `DrawUiPanel`, the `PlayUiSound*` family,
`EmitViewportRect`/`EmitClipRect`, `DrawSpriteVariant`/`DrawFlatPrimitive`/`DrawSpritePrimitive`,
`LoadResourceEntry`, `JoystickResetState`, `SetCardFileName`/`SetCardPathDeviceFields`, the
`Retry*CardFile` family, `ControllerBannerStub`. The genuinely new pieces are `FUN_00536ba0`
(shared by both), `FUN_00535710` (shared by both), and — `FUN_00536ea0` only —
`FUN_00529ad0`, `FUN_005126c0`, `FUN_00547960`. A substantial but tractable next session.

## 2026-08-16 — `FUN_0051c120` was never "the controller banner": it's a ~50-call-site text primitive

Before touching `FUN_00536270`/`FUN_00536ea0` for real, ran the systematic check the previous entry
recommends (grep every claimed dependency against `Blaze.cs` before doing fresh disassembly). Most
of them checked out. `ControllerBannerStub` did not: it was a `NotImplementedException` placeholder
covering `FUN_0051c120`, written after reading only `FrameEnd`'s one call site. Grepping `Blaze.c`
for `FUN_0051c120(` turns up **~50 call sites** — menus, the party screen, `FUN_00536270` itself —
so it needed a real implementation before any of those orchestrators could be transcribed honestly.

Ghidra decompiles the body cleanly as pseudo-C (`Blaze.c:66809-66862`), but the "sprite" struct it
passes to `DrawSpriteVariant`/`DrawFlatPrimitive` (`local_14 = &local_1c`) is exactly the kind of
stack-shape pseudo-C tends to get subtly wrong, and this primitive is reused too widely to guess.
Force-exported the raw disassembly (`ForceDecompileConfirmDialog.java`, retargeted to
`0x0051c120` this run → `Legacy/Raw/Blaze/FUN_0051c120.asm`) and walked every `[ESP+N]` by hand.
Result: the pointer actually passed to `DrawSpriteVariant`/`DrawFlatPrimitive` sits **8 bytes below**
where Ghidra's `local_14` naming suggests, and the slot in between holds a self-reference back to
the struct's own address — i.e. pseudo-C's "`local_14` = a pointer variable, itself passed by
address" is a real pointer-to-pointer in memory, not a naming quirk. Reproduced faithfully in C# with
a 7-`uint` `frame` buffer and `frame[2] = (uint)(nuint)frame` as the self-reference.

Also resolved the bit-packing of the "atlas" dword (`packing = *cursor[0]`) against a real call site:
`FrameEnd`'s banner passes `&DAT_00566c28` as that pointer. **The binary is in the repo now**
(`REVERSE_STATUS.md` line 11 already says so — this was the first time this session actually needed
it), so `DAT_00566c28`'s bytes aren't a systemic-blocker case at all:
`dotnet run --project OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x00566c28 32` reads `0x40803000` /
`0x02300000` straight off disk, which decodes to a clean 8×8 glyph cell (origin Y=0x30, cell 8×8) —
confirming the derived bit layout (`origin X/Y` in the low two bytes, cell `W`/`H` at `>>0x14`/`>>0x1b`)
independently of the asm reading.

Named the result `DrawTextRun`, wired `FrameEnd`'s controller-banner call site to it for real (was
the only call site with a concrete addressable argument available before `FUN_00536270`/`FUN_00536ea0`
are transcribed), and removed `ControllerBannerStub`. Builds clean. **Not yet visually confirmed**:
`s_Unknown_00571a3c` (the banner's gate) needs a controller connected-then-disconnected mid-session
to go non-zero, which `playtest.ps1` has no lever for yet — TODO next session, either add one or
verify by other means before trusting this path beyond the disassembly-level cross-check above.

Every other `FUN_0051c120` call site (the party screen included) can now call `DrawTextRun` directly
once its own `cursor`/text arguments are transcribed — no more placeholder blocking that path.

### `FUN_00535710` → `RefreshPartyAbilityMask`, the first of the two shared genuinely-new pieces

Transcribed directly from the full body a research agent retrieved earlier this session (35 lines,
no sub-calls — the simplest of the remaining unknowns in `FUN_00536270`/`FUN_00536ea0`'s dependency
list). OR-accumulates each active party member's 32-byte flag block into a shared party-wide mask.
Declared two new globals it needs (`s_PartySize_005e9020`, `s_PartyAbilityMask_005e9000`) — both
widely cross-referenced elsewhere in `Blaze.c` (`DAT_005e9020` alone at ~60 sites) but only given the
conservative role this one function requires; **not** a full reconstruction of either global's
complete behavior, flagged as such in their doc comments. Builds clean; confirmed no regression via
`playtest.ps1` (this function isn't reachable yet from any transcribed call site, so there's nothing
to visually confirm until `FUN_00536270`/`FUN_00536ea0` actually call it).

Remaining before those two orchestrators: `FUN_00536ba0` (110 lines, dense bit-packing, shared by
both — full body already retrieved via agent), then `FUN_00529ad0`/`FUN_005126c0`/`FUN_00547960`
(`FUN_00536ea0`-only, not yet read), then the two orchestrators themselves.

### `FUN_00536ba0` → `LoadPartyMemberResources`

This one earned the raw-disassembly treatment `FUN_0051c120` got two entries back: Ghidra's
pseudo-C types several genuine pointers as `undefined4`, and at least one pseudo-C variable name
doesn't line up with what the real stack offsets show. Delegated the mechanical trace (a forked
agent, same context, kept the tedious instruction-by-instruction ESP-delta bookkeeping out of the
main conversation) after finding the trap myself first: **stack cleanup after a cdecl `CALL` is
sometimes deferred several instructions** (the `DecodeResourceChunk` call at asm `0x536c9c` isn't
cleaned up until `0x536cba`, five instructions later — any `[ESP+n]` read in that window is off by
the pending push count from a naïve post-prologue offset). The fork found a second trap in the same
family: the two winsys3d calls this function makes directly (`OpenTim`/`ReadTim`, called here
through their raw thunk addresses rather than the `Ordinal_677`/`Ordinal_662` names the full-analysis
`Blaze.c` gives them) are **stdcall** — the callee pops its own arguments, so no `ADD ESP` follows
and a naïve trace silently misreads the next variable.

Cross-checked a meaningful chunk of the fork's output by hand (the two texture-loading blocks, where
I'd already partially traced the asm myself before delegating) — it matched independently derived
bit-packing exactly, including a subtle one (`pixelRect[1]`'s 16-bit truncated `LEA` with garbage in
the upper bytes of `ECX` that turns out not to matter because the final store only keeps the low 16
bits). Confirmed `s_SlotFlags_005e93c0` (the `DAT_005e93c0`/stride-`0x9c` per-slot record table this
function writes into) was **already declared** — reused rather than redeclared.

Per-party-slot (fixed 4 slots, **not** gated by `s_PartySize_005e9020` — confirmed by reading the
whole function, a real TODO(REVERSE) left in the docstring): reads the slot's character identity from
`s_SelectScratch_00573338[0x150 + i*0x2000]`, checks it against every earlier slot for a duplicate
(skips the heavy load if found, just records which slot to mirror), then loads and decodes two
textures, a model bundle and an animation bundle, each indexed by that identity into
`s_MainResourceDirectory_005e97c4`'s per-character tables — the same `LoadResourceEntry` +
`Relocate*Bundle` idiom `LoadCharacterSelectResources` already uses for its own directory-list-driven
loading, just indexed directly instead of walked as a list. Builds clean (two new `CA2014`
stackalloc-in-loop warnings, same pre-existing pattern as five other spots in this file).

Tail call `FUN_00535500` read (pseudo-C is clean, no traps apparent) but not transcribed — stubbed as
`FinalizePartyResourceUpload` with a named exception and the exact `Blaze.c:81583` line to resume
from. Looks like a portrait-sprite VRAM upload pass (loops over `mainMem+0xf0440`, `Ordinal_667`/
`Ordinal_674` pairs), not obviously coupled to anything already established — left for the next
function rather than guessed at, per the same discipline as `FUN_00537a20` in `RunGameState1Entry`.

Not yet reachable from any call site (same situation as `RefreshPartyAbilityMask`): both remaining
orchestrators still throw before reaching this function, so nothing to visually confirm yet beyond
the regression check (`playtest.ps1`, no crash, primitive count unchanged).

### The three `FUN_00536ea0`-only dependencies, closed out

`FUN_00529ad0` was already `PlayMusicTrack` (confirmed earlier this arc). The other two:

**`FUN_005126c0` → `LoadSaveDirectory`.** Turned into its own side quest: this function enumerates
the `save\` folder for `Blaze-Saving*` files (up to 32), opens and reads each one's header, and — this
is the actually valuable part — it's the **previously-unknown writer** of `DAT_00604981`, a field
`RenderSaveScreen`'s case `0xa` (the memory-card port cursor) already referenced but could only read
as a hardcoded `0` with a comment saying "never set by any reconstructed code." Found by recognizing
the `puVar6 = puVar6 + 0x28` stride matching a `TODO` already sitting in `s_CardDirectoryEntries_006049bc`'s
doc comment. Wired the fix into that existing case-0xa code too, correcting the stale comment there.

Needed the same raw-disassembly treatment as `FUN_00536ba0` — Ghidra wraps this one in SEH
try/except-shaped pseudo-C (`puStack_20`, literal continuation addresses like `0x5126f1` standing in
for real locals), which makes argument order to its handful of calls actively misleading to read
directly. The asm is completely ordinary underneath (no real exception handling in the machine code,
just the decompiler's standard artifact for `/EHa`-compiled functions) and resolved cleanly. Two
findings worth keeping in mind for the next SEH-shaped function:
- The very first instruction, before the prologue, loads `[0x56315c]` and that value gets pushed as
  the sole argument to what's already `SetCardFileName` — confirmed by reading the raw bytes at
  `0x56315c` (`at Legacy/Binaries/Blaze.exe`), which hold exactly `0x563160`, the address of the already-declared
  `s_SaveDeviceName_00563160` ("Blaze-Saving*"). A `PTR_xxx` name in Ghidra's pseudo-C is worth
  resolving the same way `DAT_` globals already are — it may just be a pointer *to* something already
  named.
- Two `Ordinal_674` (LoadImage) calls near the end each build a 4-field rect, and a first pass at
  tracing them by hand seemed to leave the first two fields of one rect unexplained. Recomputing more
  carefully found the gap was a **self-inflicted version of the deferred-cleanup trap**: a `PUSH`
  between a field write and the `LEA` that computes the struct's own address shifts what `[ESP+0x18]`
  means by 4 bytes, so a write that looked like it was 4 bytes *before* the struct was actually its
  first field. Both rects turned out fully accounted for once re-derived against the post-push `ESP`.
- Along the way, declared two new `winsys3d.dll` ordinals (`Ordinal_18`/`Ordinal_19` — `FindFirstFileA`/
  `FindNextFileA` wrappers, confirmed by reading `winsys3d.c` directly). `Ordinal_19` is declared
  `void` in `winsys3d.c` but its caller here tests `EAX` after the call anyway — traced why that's not
  a mistake: the wrapper's own early `return;` leaves `FindNextFileA`'s `BOOL` result sitting in `EAX`
  by ordinary x86 code-gen, so the caller's test is reading a real (if undeclared) success flag. Kept
  the P/Invoke declared as returning `int` with a comment explaining the discrepancy, same resolution
  style as the `FrameBegin`/`FrameEnd` arity notes.

Not wired into any caller yet, so `s_CardEntryCount_00604981` stays `0` until `FUN_00536ea0` (or
whatever else calls `LoadSaveDirectory`) actually runs — the case-0xa fix is correct but currently
inert. Builds clean; regression-checked with `playtest.ps1`.

**`FUN_00547960`**: read, not transcribed. It's the CRT's variadic `sprintf`-equivalent (calls into
`FUN_00549380`, the real varargs formatting engine, with a raw `&stack0x0000000c` pointer as the args
list) — a generic printf-family function, not game logic. Implementing a faithful C varargs engine
generically would be real scope for no immediate benefit; the one call site that needs it is inside
`FUN_00536ea0` itself, not yet read. Plan: when `FUN_00536ea0`'s body is transcribed and that call
site's actual format string and arguments are known, write the specific `string.Format`/manual
formatting equivalent for *that* call rather than a general-purpose sprintf.

### Where this leaves `FUN_00536270`/`FUN_00536ea0`

Every dependency either function calls has now been resolved one way or another (already done,
newly transcribed this arc, or explicitly deferred with a plan). Next step is the two orchestrators
themselves — 361 and 494 lines, not yet read in full by me directly (only surveyed for call targets
via research agents earlier this arc, plus the first 140 lines of `FUN_00536270` read directly).

---

## 2026-08-17 — `FUN_00536270` transcrite et confirmée en jeu : c'est un écran de développeur, et la clé qui l'ouvre est le mot « Gurkha »

Objectif de la séance : les deux orchestrateurs restants de l'état de jeu 1. Le premier
(`FUN_00536270`, chemin A) est fait, vérifié à l'écran, et a entraîné cinq corrections de fond
ailleurs dans la reconstruction. Le second (`FUN_00536ea0`, chemin B) est intégralement analysé mais
pas encore écrit — l'analyse est consignée plus bas pour que la prochaine séance n'ait pas à la
refaire, ainsi que le seul obstacle réel qui reste devant lui.

### Ce qui décide entre les deux chemins : `DAT_005633a8`, et il valait 0 par accident

`RunGameState1Entry` branche sur `DAT_005633a8 == 0 && manette_connectée`. Trois choses étaient
fausses d'un coup, et elles se répondent :

- **La valeur au chargement est 1**, relevée dans le binaire (`at Legacy/Binaries/Blaze.exe 0x005633a8`,
  `.data`). La reconstruction la laissait au zéro par défaut de C#, donc le chemin A était le chemin
  pris — l'inverse de la réalité. Largeur confirmée par le code machine
  (`0x00536218 39 1D A8 33 56 00 CMP dword [0x5633a8],EBX`).
- **Le symbole était déclaré deux fois** : `s_UnknownModeFlag_005633a8` portait les trois lectures,
  `s_Unknown_005633a8` recevait l'unique écriture. Exactement la classe de défaut d'aliasing trouvée
  six fois le 2026-08-16. Fusionnés en `s_DebugModeLatch_005633a8`, initialisé à 1.
- **`DAT_00564a40`, la séquence de triche, est lisible dans le binaire** : `47 75 72 6b 68 61 00`,
  soit **« Gurkha »**, logée juste après les trois étiquettes de mise au point « DPT »/« LOC »/« ANG »
  à 0x564a20. Elle était déclarée vide avec un `TODO(REVERSE)` disant que la retrouver ne changerait
  rien d'observable ; c'est faux, elle ouvre un écran entier.

Le mécanisme complet, une fois les trois recollés : `CheatCodeProbe` (`FUN_0051c0e0`) est appelée sur
chaque caractère **dessiné** — par `DrawTextRun` et par `RasterizeGlyph`, donc par les deux moteurs de
texte — et non sur une saisie. Quand le filage atteint le terminateur de « Gurkha », elle met le
verrou à 0. Autrement dit : **faire afficher le mot « Gurkha » quelque part** (nommer un personnage
ainsi, l'écran de saisie du nom le dessine) allume le mode développeur. Les trois lecteurs du verrou
sont cohérents avec cette lecture — damier de mise au point dans le tampon de rendu
(`InitViewportSurfaces`), pilotage libre de la caméra, et l'écran de composition de groupe ci-dessous.

Corrigé au passage dans `CheatCodeProbe` : un `return` que la reconstruction avait ajouté dans la
première branche et que l'original n'a pas (`Blaze.c` 66791-66799). Sans lui, après la remise à zéro
la fonction recompare aussitôt le caractère courant à la première lettre ; avec lui, le filage ne
pouvait jamais repartir sur ce caractère-là.

### `FUN_00536270` → `RunDebugPartySelect` : composition de groupe de mise au point

Deux moitiés. La première est une boucle d'image : colonne « MALE / FEMALE », colonne des huit
classes, un curseur « > » sur chacune, la liste du groupe composé en bas. Carré ajoute, Croix retire,
Start valide, Select+Start abandonne vers le menu principal. Le groupe s'écrit directement dans les
octets d'identité `MainMem+0xf0150 + n*0x2000`, les mêmes que la création de personnage normale.

La seconde moitié initialise les quatre enregistrements de personnage d'un coup
(`InitialiseDebugPartyRecord`). C'est le pendant expéditif de `FinalizeCharacterStats` : mêmes
champs, mêmes cinq fonctions de calcul en fin de parcours, mais les statistiques viennent d'une table
par classe et **les PV/PM sont écrasés par 9000 / 200**. Puis le garde-fou de dépassement de tampon,
et l'état de jeu 2.

Trois particularités transcrites telles quelles parce qu'elles sont dans l'original :

- `SetClassBaseHpMp` est appelée puis **immédiatement écrasée** par les 9000/200. Ce n'est pas une
  erreur de lecture, et c'est précisément cette paire qui a permis de renommer la fonction (voir plus
  bas).
- Dix octets isolés remis à zéro à `+0x300 + i*0x280` sont intégralement recouverts par la copie de
  portrait de la ligne suivante. Mort dans l'original.
- La copie du nom par défaut lit **seize** octets dans des enregistrements qui n'en font que **huit**.
  Inoffensif (tout affichage s'arrête au premier nul), mais le bloc `.data` est reproduit tel quel
  côté C# pour que le débordement tombe sur les mêmes octets.

### Les données `.data` relevées pour cet écran

Toutes lues avec `OpenForesia.Tools -- at`, aucune devinée.

| adresse | contenu |
|---|---|
| `0x0056c894`..`0x0056c8d3` | les huit noms de classe, 64 octets contigus : Fighter, Priest, Rogue, Sorcerer, Hunter, Elf, Dwarf, Fairy |
| `0x0056c7c0` | les huit pointeurs dedans (`PTR_s_Fighter_0056c7c0`) |
| `0x0056c8d4`..`0x0056c96b` | seize noms de personnage par défaut de 8 octets (index décroissant), puis « BUFFER OVER », « FEMALE », « MALE » |
| `0x0056c780` | les seize pointeurs vers ces noms |
| `0x0056c7e0` | 8 lignes de 8 octets : sept statistiques de départ + l'élément, par classe |
| `0x0056c820`/`828`/`830` | **identiques** à `s_ClassStartIcon_00562ab8`/`s_StatBitRemap_00562ac0`/`s_StatBitTable_00562ac8` — le binaire porte les deux jeux à deux adresses |
| `0x0056c848`/`858` | 16 octets chacun, indexés par l'identité complète (genre compris) → `+0x153` / `+0x152` |
| `0x00564a2c` | `3E 00 00 00`, la chaîne « > » : le curseur de sélection |
| `0x00566c84`..`0x00566c94` | les cinq champs que la fin de fonction repose à 0xff/0xff/0xff/1/1 — **exactement** leurs valeurs au chargement, donc une remise à l'état initial |

Les seize noms par défaut sont allemands (Elo, Hansi, Hugo, Werner, Willi, Egon, Matze, Tommi pour
les masculins ; Bella, Süßi, Mimi, Nicola, Tussi, Erna, Dora, Dina pour les féminins) — cohérent avec
l'édition européenne. L'identité 9 (`53 FC DF 69`) est « Süßi » en Latin-1.

### Confirmé à l'écran, les deux moitiés

`playtest.ps1 -Entry 1 -Then WAIT14,DOWN,WAIT1,DOWN,WAIT1,ENTER,WAIT2,RIGHT,WAIT1,ENTER` sur
l'échafaudage décrit plus bas : les huit classes s'affichent dans l'ordre exact de la table, le
curseur suit les flèches, Carré ajoute bien « Rogue MALE » puis « Rogue FEMALE » à la liste du bas,
Croix retire, et Start sort de la boucle. Débit : 58 primitives par image à vide, ~10 de plus par
ligne de groupe. Aucune régression sur l'écran de sélection des personnages (382.3 par image, capture
identique à la référence).

Une nuance sur les directions : le pseudo-code fait bien reculer le genre sur DROITE et l'avancer sur
GAUCHE, l'inverse de `CharacterSelectSubState7` qui lit les mêmes bits. Transcrit tel quel — et
c'est **inobservable**, parce qu'un cycle à deux valeurs donne le même résultat dans les deux sens.
La liste des classes, elle, a huit valeurs et va bien dans le bon sens.

### `SetClassHighlightColour` → `SetClassBaseHpMp`

Le nom précédent n'était qu'une supposition. Trois recoupements le fixent :
`FinalizeCharacterStats` recopie `+0x148`/`+0x14a` dans `+0x14c`/`+0x14e` (la paire
courant/maximum) ; l'accumulateur qui les prépare calcule `(stat + stat) / 2 + base` ; et le chemin
de mise au point écrase les quatre par 9000/9000/200/200. Les valeurs par classe sont cohérentes :
16 PV / 2 PM pour le Guerrier et le Nain, 14/8 pour le Prêtre, le Sorcier et la Fée, 15/4 pour le
Voleur, le Chasseur et l'Elfe.

### Deux pas de manette encore à 4 au lieu de 0x10

Le piège documenté sur `CurrentPadState` (la pseudo-C écrit `&DAT_00604a20 + n * 4` parce que le
symbole est typé `undefined4`, ce qui vaut n*0x10 en octets) subsistait à deux endroits. Les deux
confirmés par le code machine, pas par analogie :

- `PollInputAndAudio` : `Legacy/Raw/Blaze/FUN_0052a280.asm` 0x0052a34c, `SHL ECX,0x4` puis
  `ADD ECX,0x604a20`.
- `RunGameState1Entry` : octets lus directement, `0x0053623f C1 E0 04 SHL EAX,0x4` puis
  `0x00536242 F6 80 20 4A 60 00 01 TEST byte [EAX + 0x604a20],0x1`.

Latents jusqu'ici : le décalage n'était juste que pour l'emplacement 0, le seul exercé.

### `FUN_005352e0` : hypothèse close

Le `TODO(REVERSE)` sur `CopyDwords` (« pris pour un memcpy de count dwords, à confirmer ») est levé.
`Legacy/Raw/Blaze/FUN_005352e0.asm` fait 32 octets en tout : sortie immédiate si le compteur est nul,
puis `MOV ESI,[EAX] / ADD EAX,4 / MOV [ECX],ESI / ADD ECX,4 / DEC EDX / JNZ`. L'ordre des arguments
(source, destination, nombre) est confirmé par les `[ESP+4]`/`[ESP+8]`/`[ESP+0xc]`.

### Un vrai bug dans `LoadPartyMemberResources`, trouvé en l'exécutant pour la première fois

`FUN_00536ba0` avait été transcrite le 2026-08-16 mais n'était atteignable depuis aucun site
d'appel — la note de l'époque le disait explicitement. `RunDebugPartySelect` l'appelle, donc elle a
tourné pour la première fois cette séance, et elle a fauté aussitôt (`0xC0000005` dans
`DecodeResourceChunk`).

Cause : **un déréférencement manquant**. La table de textures se lit `*(int*)(directoryBase + 0x84)`
puis `+ identité * 8` ; la reconstruction lisait la table **à** `directoryBase + 0x84`. Le
désassemblage est sans ambiguïté (`0x00536c42 MOV ESI,[EAX+0x84]` puis `MOV EAX,[ESI + EDI*1]`), et
la pseudo-C disait déjà la même chose. Le paquet de modèle, dix lignes plus bas, déréférençait bien
son `*(int*)(directoryBase + 0x2c)` — c'est l'incohérence entre les deux qui saute aux yeux une fois
qu'on la cherche.

Corrigé, `LoadPartyMemberResources` traverse maintenant ses quatre emplacements en entier (deux
textures, un modèle, une animation chacun) et s'arrête sur une borne **nommée**, pas sur une faute :
`FUN_00535500`, pas encore reconstruite.

**Ce que ça dit de la méthode** : une fonction transcrite avec soin depuis l'assembleur, relue,
documentée, et qui contenait quand même un défaut d'une ligne — invisible tant qu'elle n'était pas
exécutée. Le fait d'ouvrir un chemin d'appel vaut autant qu'une relecture.

### `FUN_00535500` — la borne suivante du chemin A, et pourquoi elle n'est pas faite

Trente-cinq lignes, deux boucles imbriquées qui téléversent en VRAM des bandes de 16x1 pixels
(`Ordinal_674`) pour 4 emplacements × 10 lignes, puis posent `DAT_005e8fa0 = 0x7c400216`. Le corps
est simple **sauf** la disposition de pile : la pseudo-C passe `&local_c` au premier `Ordinal_674`
alors que les quatre champs du rectangle sont écrits dans `local_8`/`local_6`/`local_4`/`local_2`
(décalage de 4), et le second appel reçoit un `&stack0xffffffe8` que rien dans la pseudo-C ne
remplit. Deux rectangles, un seul visible. C'est exactement le cas où le projet exige un
désassemblage brut plutôt qu'une lecture — la recette Ghidra headless est dans `TODO.md`.

### `FUN_00536ea0` (chemin B) — analyse complète, transcription à faire

C'est **le chemin normal** : le verrou étant à 1, c'est lui que le jeu prend. Toutes ses dépendances
existent déjà côté C# (vérifié une par une) sauf `FUN_00547960`, le `sprintf` du CRT, dont le format
est maintenant connu : `DAT_005630c4` vaut `"%d"`, donc le site d'appel se transcrit en un simple
formatage décimal, pas en moteur variadique.

**Structure.** Un prologue qui énumère les sauvegardes, puis une boucle d'image avec une petite
machine à états sur `local_38` (0 = parcours de la liste, 1 = confirmation d'abandon, 2/5 = boîte
modale, 3 = sortie, 4 = confirmation de départ).

Prologue : `PlayMusicTrack(0x25)`, `JoystickResetState`, `LoadSaveDirectory`, puis pour chaque entrée
du répertoire — ouverture, lecture de 0x2000 octets, et composition d'une ligne d'affichage de
**0x16 octets** à `DAT_005e8640 + n*0x16` :

| plage | contenu |
|---|---|
| `[0x00..0x0a]` | le nom du personnage (lu à `+0x100` du fichier), complété à 11 par des espaces |
| `[0x0b..0x0e]` | l'abréviation de classe, table `0x0056c868` de pas 5 : `Gue.` `Pre.` `Vol.` `Mag.` `Cha.` `Elf ` `Nai.` `Lut.` |
| `[0x0f]` | `DAT_0056c890` = `'N'` (Niveau) |
| `[0x10..0x15]` | le niveau (`+0x144` du fichier), formaté `"%d"` |

Si l'ouverture échoue, la ligne reçoit `s__________00566b8c` = `"--------"` (huit tirets). S'il n'y a
aucune sauvegarde, la fonction pose l'état de jeu 4 (sélection des personnages) et rend la main.

Boucle : jusqu'à six lignes visibles, défilement par `local_34`, sélection numérotée de 1 à 4 dans
`DAT_005e85c0` (Carré bascule, la désélection renumérote les suivantes). Les lignes non sélectionnées
sont dessinées avec `puStack_10 = 0x404040` au lieu de `0x1000000` — Ghidra rend ça
`&LAB_00404040 + (-(sel != 0) & 0xbfbfc0)`, ce qui est simplement la constante 0x404040 lue comme une
étiquette, plus le complément qui la ramène à 0x1000000. Un gris pour « non retenu ».

Sortie : Start avec au moins un personnage retenu → état 4 → Carré → `local_38 = 5` → `DAT_005de510 = 3`
→ l'itération suivante prend le bloc de chargement, qui relit chaque fichier retenu, vérifie une somme
de contrôle par octets sur 0x1ffc octets, restaure l'état du monde depuis le premier, puis appelle
`RefreshPartyAbilityMask` / `LoadPartyMemberResources` et finit **exactement** comme le chemin A
(mêmes drapeaux d'emplacement, même garde-fou, même boucle « BUFFER OVER »), à ceci près qu'il laisse
l'état de jeu à **3** là où le chemin A pose **2**.

### Deux descripteurs d'interface de plus, et huit champs qui étaient encore des octets isolés

En lisant la boucle de rendu de `FUN_00536ea0`
(`do { RunTextEngine; DrawUiPanel; p += 0x8c8; } while (p < 0x56c6e8)`), il devient évident que
`DAT_0056b558` et `DAT_0056be20` sont **deux instances contiguës** du type de descripteur déjà connu
(0x56b558 + 0x8c8 = 0x56be20, + 0x8c8 = 0x56c6e8, la borne exacte de la boucle). Leurs 26 dwords
initialisés sont relevés dans le binaire et déclarés.

Et alors huit « globales » de la liste de remise à zéro de `RunGameState1Entry` tombent en place,
avec une symétrie parfaite de quatre champs par descripteur, aux mêmes décalages que ceux déjà
nommés sur les autres instances (`+0x3b`, `+0x8c4`, `+0x8c5`, `+0x8c7`) :

```
0x56b593 = descripteur 1 + 0x3b      0x56be5b = descripteur 2 + 0x3b
0x56be1c = descripteur 1 + 0x8c4     0x56c6e4 = descripteur 2 + 0x8c4
0x56be1d = descripteur 1 + 0x8c5     0x56c6e5 = descripteur 2 + 0x8c5
0x56be1f = descripteur 1 + 0x8c7     0x56c6e7 = descripteur 2 + 0x8c7
```

Septième à quatorzième instances du même défaut d'aliasing. Le symptôme était visible depuis le
début sans être lu : les huit produisaient un avertissement CS0414 « assigné mais jamais utilisé ».
Recâblés en propriétés `ref byte`, ils disparaissent.

### Le vrai obstacle devant `FUN_00536ea0` : la région 0x5e9060–0x5e99c0 est découpée en morceaux

Le bloc de restauration d'état du monde recopie huit banques de 0x40 octets de la sauvegarde vers
`DAT_005e96e0`, `005e9780`, `005e9060`, `005e9840`, `005e9720`, `005e92c0`, `005e9980`, `005e9360` —
les huit mêmes que `InitialiseDebugPartyRecord` efface à `+0x1d00..0x1eff`. Quatre d'entre elles
tombent à l'intérieur d'allocations existantes (`s_PanelSelection_005e9060`,
`s_SlotArea_005e9180 + 0x140 / +0x1a0 / +0x1e0`), les quatre autres dans la zone que
`s_PartyAnimationResult_005e9680` / `s_PartyModelResult_005e9800` occupent nominalement.

Dans l'original c'est **une seule région contiguë** ; ici elle est éparpillée en allocations
indépendantes. Écrire le bloc de restauration en déclarant huit nouveaux blocs isolés reproduirait
exactement le défaut d'aliasing corrigé quatorze fois. **La bonne étape préalable est de poser un
seul bloc de fond pour 0x5e9060–0x5e99c0 et de réexprimer les symboles existants comme des décalages
dedans** — comme `s_SlotFlags_005e93c0 => s_SlotArea_005e9180 + 0x240` le fait déjà. C'est un
remaniement mesuré, à faire avant `FUN_00536ea0` et pas pendant.

### Outillage

- `playtest.ps1 -Build` : le repli SDK codé en dur (`C:\Users\Sagara\.dotnet`) n'existe plus sur
  cette machine. Remplacé par une **liste** de candidats essayés dans l'ordre, `C:\Program Files
  (x86)\dotnet` en tête (SDK x86, donc sans le problème hostfxr habituel).
- Trois jetons `-Then` ajoutés, lus dans `s_PadButtonMasks_00564f18` croisée avec la section
  `[Controls]` d'`OpenForesia.cfg` : `START` (action 11, masque 0x0008, K000b=60 = F2),
  `SELECT` (action 8, 0x0001, K0008=59 = F1), `LALT` (action 6, croix, 0x4000, K0006=56).
- Échafaudage `OPENFORESIA_FORCE_DEBUG_PARTY=1` : les deux conditions réelles du chemin A sont
  hors de portée du script (il faut que « Gurkha » ait été dessiné, **et** que SELECT soit maintenu à
  l'entrée de l'état 1 — or ce sont des conditions d'état, pas de frappe ponctuelle).

### Reste à faire

1. Poser le bloc de fond 0x5e9060–0x5e99c0 (voir ci-dessus) — préalable à `FUN_00536ea0`.
2. `FUN_00536ea0`, avec l'analyse ci-dessus.
3. `FUN_00535500`, après désassemblage brut (disposition de pile ambiguë).
4. `FUN_00537a20`, la branche `DAT_005de51c != 0` de `RunGameState1Entry`, jamais lue.
5. L'état de jeu 2 (`FUN_00542160`/`FUN_00537da0`), où le chemin A débouche maintenant.

### Le déclencheur « Gurkha », vérifié en jeu (même journée, après coup)

La question « comment y accède-t-on vraiment ? » méritait mieux qu'une déduction. Les deux
conditions du chemin A ont été mesurées séparément dans le jeu qui tourne, avec deux traces
temporaires depuis retirées.

**1. Taper « Gurkha » bascule bien le verrou.** Chemin exact : écran de sélection des personnages →
valider un portrait (trois validations : le portrait, la question « Ton choix se porte-t-il sur ce
personnage ? », puis « Oui ») → écran de saisie du nom → taper les six lettres. La trace posée dans
`CheatCodeProbe` tombe immédiatement : `DAT_005633a8 -> 0`.

Le mécanisme se referme proprement : `DrawNameEntryScreen` lie le nom tapé puis appelle
`RunTextEngine`, donc chaque caractère passe par `RasterizeGlyph` → `CheatCodeProbe`, dans l'ordre et
sans rien d'intercalé. Et l'octet `0x5f` (le curseur `_`) que l'écran écrit **juste après** le nom
est exactement le « caractère suivant » dont la sonde a besoin pour franchir le terminateur et poser
le verrou. Rien à confirmer, rien à valider : la bascule a lieu pendant la frappe.

Deux détails qui auraient pu tout empêcher, et qui tiennent :
- le clavier virtuel de l'écran porte les minuscules **et** les majuscules, donc « Gurkha » est
  saisissable à la manette seule ;
- l'écran lit aussi la frappe réelle (file `WM_CHAR` de winsys3d via `GetKBChar`), ce qui a permis de
  le tester au clavier.

**2. Le verrou est à sens unique.** `DAT_005633a8` n'a qu'un seul écrivain dans les 98 000 lignes de
`Blaze.c` — la ligne 66792, qui le met à 0 — contre **22 lecteurs** répartis dans tout le jeu
(rendu, caméra, combat, terrain), presque toujours appariés à `DAT_005de020`. Ce n'est donc pas le
verrou d'un écran : c'est **le** mode de mise au point du jeu, et une fois « Gurkha » affiché il
reste actif jusqu'à la fermeture.

**3. SELECT maintenu, lu au branchement même.** Le bit 0 du mot « tenu » à `0x604a20` est bien
SELECT (masque 0x0001, action 8 de `s_PadButtonMasks_00564f18`). Mesuré de deux façons : une trace
sur le mot complet donne `0x0001` avec F1 enfoncé, `0x0008` avec F2, `0x8000` avec Entrée,
`0x0040` avec Bas ; et une trace posée sur le branchement de `RunGameState1Entry` lit `pad0=0x01`
quand F1 est maintenu depuis la prise de focus. C'est un **état tenu**, pas un front : un appui
ponctuel ne suffit pas.

**Ce qui n'a pas été fait** : réunir les deux dans un même run scripté. La création de personnage
complète passe par le dé de bonus puis la répartition d'une vingtaine de points, soit une trentaine
d'appuis à enchaîner dans un `-Then`, pour prouver un `&&` de deux lignes dont les deux opérandes
sont déjà vérifiés au bon endroit. À la main, l'utilisateur y arrive en quelques secondes.

Trois pièges de pilotage trouvés en chemin, tous consignés dans `test-protocol.md` :
- `-Then` ne démarre **qu'après** la validation du menu, or la chaîne forcée
  (`OPENFORESIA_FORCE_SUBSTATE=7`) atteint l'état de jeu 1 en quelques images — d'où le nouveau
  paramètre `-Hold`, qui enfonce une touche dès la prise de focus.
- Le « premier appui après `SetForegroundWindow` est avalé » vaut aussi pour un maintien : `-Hold`
  dépense donc volontairement un appui avant d'enfoncer pour de bon. Sans ça, la trace lisait
  obstinément `pad0=0x00`.
- La détection de validation cherchait `pad=0x8000` **exact** ; avec `-Hold SELECT` le jeu trace
  `pad=0x8001` et la validation n'était jamais vue. Passée en test de bit.

Ajouts durables à `playtest.ps1` : `-Hold`, les jetons `HOLD:`/`FREE:`, et la frappe de texte
`TYPE:`/`KBENTER` par `KEYEVENTF_UNICODE` (et non par scancode : un scancode dépend de la
disposition clavier, or l'écran de saisie du nom veut un vrai caractère).

---

## 2026-08-17 — suite : `FUN_00535500`, `FUN_00538780`, et l'arène 0x5e9000–0x5e9a00

Trois unités, chacune construite et testée avant de passer à la suivante.

### `FUN_00535500` → `UploadPartyIconTextures`

Le mur immédiat du chemin ouvert plus tôt dans la journée. Trente-cinq lignes, mais il fallait le
désassemblage brut : la pseudo-C passe `&local_c` au premier `Ordinal_674` alors que les quatre
champs du rectangle sont écrits dans `local_8`/`local_6`/`local_4`/`local_2`, et donne au second
appel un `&stack0xffffffe8` que rien ne remplit. Deux artefacts, **une seule cause** : le `PUSH 0x0`
de l'appel à `Ordinal_667` est émis *avant* les quatre écritures, si bien que les `[ESP+0x18..0x1e]`
de l'assembleur et le `LEA EAX,[ESP+0x14]` d'après l'appel désignent la même structure. C'est le
piège déjà rencontré dans `FUN_005126c0`, et les deux rectangles réutilisent en plus le **même**
emplacement de pile — ce que la pseudo-C perd complètement.

Une fois l'assembleur lu, la fonction se referme sur elle-même. Elle téléverse, pour chacun des
quatre emplacements du groupe et chacune de ses dix vignettes, une palette de seize couleurs
(16x1 à `x = vignette*16`, `y = 0x1f1 + emplacement`) et une image de 24x24 en 4 bits par pixel
(6 unités de 16 bits sur 24 lignes, à `x = 0x180 + vignette*6`, `y = 0x100 + emplacement*0x18`).

Trois recoupements indépendants la confirment :
- la disposition d'un sous-bloc tombe juste **sans reste** : 0x140 d'en-tête + 0x20 de palette +
  0x120 de pixels = 0x280, et dix sous-blocs font les 0x640 dwords de la région `+0x300` de
  l'enregistrement — celle que `InitialiseDebugPartyRecord` et `FinalizeCharacterStats` remplissent ;
- le couple posé en sortie se recalcule : `0x7c40` est exactement l'identifiant de palette de
  l'emplacement 0 (`(0x1f1 << 6) | (0 >> 4)`), et `0x216` porte la page de texture de la zone pixels
  (`(0x180 >> 6) | ((0x100 & 0x100) >> 4)` = 0x16) ;
- l'octet testé en tête de boucle est celui que `record[0x313 + i*0x280] |= 0x80` voisine.

Confirmé en jeu : le chemin de mise au point traverse maintenant `RunDebugPartySelect` en entier et
s'arrête sur l'**état de jeu 2**, la borne nommée suivante. Aucune faute native.

TODO(REVERSE) : ce que représentent les dix vignettes par personnage (objets équipés ? sorts ?) reste
à établir — seul leur format l'est.

### `FUN_00538780` → `GetEntityStatBlock`

Quatre lignes, trois sites d'appel ailleurs dans le jeu. Le bit `0x800000` du mot d'état d'une entité
distingue un **membre du groupe**, dont le bloc de statistiques vit dans son enregistrement de 0x2000
octets (`MainMem + 0xf0100 + index * 0x2000`), de toute autre entité, logée dans une table compacte à
`DAT_005ea560`, 0x120 octets par entrée.

Recoupement : `FUN_005387c0` écrit la même adresse sous la forme `(index + 0x78) * 0x2000 + MainMem`,
et 0x78 * 0x2000 vaut précisément 0xf0000.

La table fait **32 entrées**, borné deux fois : les symboles `.data` vont de 0x5ea560 à 0x5ea663 —
tous dans la première entrée, 0x5ea663 étant son décalage +0x103, celui que `FUN_00538830` écrit —
et le groupe suivant ne reprend qu'à 0x5eca80, où 32 * 0x120 = 0x2400 tient exactement ; par
ailleurs `FUN_00537da0` parcourt 0x20 emplacements d'entité en tête de fonction.

### L'arène 0x5e9000–0x5e9a00, et les quatre défauts qu'elle met au jour

C'était l'obstacle annoncé devant `FUN_00536ea0`, et il s'est avéré bloquer aussi `FUN_00537da0` :
les deux écrivent dans les huit banques de 0x40 octets dispersées dans cette région. Dans le binaire
c'est une seule zone contiguë ; ce portage l'avait découpée en une dizaine d'allocations
indépendantes, la fabrique même du défaut d'aliasing corrigé quatorze fois ce jour-là.

**La disposition n'a pas été choisie, elle a été déduite** : chaque symbole s'arrête là où commence
son voisin d'adresse. Le levier a été les huit banques de `FUN_00536ea0` (0x5e9060, 0x5e92c0,
0x5e9360, 0x5e96e0, 0x5e9720, 0x5e9780, 0x5e9840, 0x5e9980) : **sept butent exactement** sur un
symbole déjà connu, et la huitième ne tombait juste qu'en corrigeant une borne — laquelle en a
entraîné deux autres. Trois erreurs qui se résolvent ensemble, c'est ce qui vaut confirmation.

Les quatre défauts trouvés en posant l'arène :

| défaut | conséquence |
|---|---|
| `s_EntitySlotTable_005e9180` et `s_SlotArea_005e9180` : **deux allocations pour la même adresse** | `InitialiseEntitySlots` écrivait dans l'une, `CurrentPadState` lisait l'autre — l'initialisation des emplacements manette n'atteignait jamais son lecteur |
| `s_PartyAbilityMask_005e9000` déclaré 0x100 | débordait sur `s_PanelSelection_005e9060` |
| `AssetTableCapacity = 24` | la table de modèles débordait sur la banque 0x5e96e0 ; la table d'animations recouvrait `s_PartyAnimationResult_005e9680`. La vraie capacité est **16** pour les deux — la borne « 0x60 = 24 pointeurs » ignorait que `DAT_005e9680` se loge entre les deux tables |
| `s_SlotArea_005e9180` déclaré 0x500 | déborde sur la table d'animations ; 0x4c0 suffit et couvre le plus grand décalage lu (0x240 + 4 * 0x9c = 0x4bc) |

Conséquence assumée du regroupement : un index hors borne déborde désormais **sur le voisin réel**,
exactement comme dans l'original, au lieu de rester confiné à une allocation isolée. C'est plus
fidèle, pas moins sûr.

Les scalaires isolés de la région (0x5e97c0..0x5e9824, 0x5e9960) restent des champs C# : aucune
banque ni aucun bloc ne les recouvre, donc ils ne peuvent pas produire ce défaut. À ramener dans
l'arène le jour où un site les lira par adresse.

Non-régression vérifiée sur les trois chemins exerçables — menu principal, sélection des personnages
(382.3 primitives par image, capture identique à la référence), et le chemin de mise au point
(68 par image, même borne à l'état de jeu 2).

### État du front

`FUN_00537da0` (l'état de jeu 2, 360 lignes) est lu en entier et son unique dépendance manquante
(`FUN_00538780`) est faite. C'est l'écran de choix de destination : 32 particules animées, une caméra
qui glisse vers la destination visée, un masque de destinations débloquées construit à partir de
`DAT_005e901e`, le modèle 3D du groupe, et la sortie qui pose `DAT_005de528` puis nettoie quatre des
huit banques. Il lui reste à récupérer un paquet de tables `.data` : `DAT_0056d240` (positions des
icônes), `DAT_0056d270`/`272` (caméra par zone), `DAT_0056d2a0` (zone → index de menu),
`DAT_0056d2b0` (index → zone), `DAT_0056c978` (un descripteur d'interface de plus), et le bloc de
particules à `DAT_005de824` (32 entrées de 0x9c).

### `FUN_00537da0` → `RunDestinationSelect` : l'état de jeu 2, confirmé à l'écran

La carte du monde. Le chemin de mise au point y débouche directement, donc elle a pu être vérifiée
tout de suite : le fond de carte s'affiche, le modèle 3D du groupe se tient dessus, le panneau
annonce « L'auberge », et une pression à droite fait glisser la caméra jusqu'à « La mine
abandonnée » en repositionnant le modèle. 231 primitives par image.

Ce que la vérification prouve d'un coup : le masque de navigation, la table index de menu → zone, le
lissage de caméra par quarts, la recherche du libellé dans la table `+0x70` du répertoire de
ressources, et le rendu de modèle animé. Sans progression `DAT_005e901e` vaut 0, donc le masque de
navigation vaut exactement 9 — seuls les index 0 et 3 sont atteignables, et c'est bien ce que fait
le curseur.

**Le désassemblage était nécessaire pour un point précis.** Ghidra rend l'appel au rendu de modèle
`unaff_EBP = FUN_0051d1f0(local_40, unaff_EBP, ...)`, ce qui laisse croire à un second curseur de
primitives qui ne reviendrait jamais dans le vrai. L'assembleur dit autre chose : les deux derniers
`PUSH` avant le `CALL` sont `[ESP+0x30]` et `[ESP+0x24]`, soit otBase et primCursor — les deux
emplacements sont bien distants de 0xc, ce que le cadre de pile confirme — et le
`MOV [ESP+0x14],EAX` qui suit le `ADD ESP,0x1c` réécrit le résultat dans primCursor. Convention
habituelle, rien de spécial ; c'est la pseudo-C qui se trompait.

**Un piège de lecture que j'ai d'abord raté**, et qui aurait donné des particules figées : la boucle
d'initialisation part de `&DAT_005de828` (base+4) et celle de mise à jour de `&DAT_005de82c`
(base+8). Les mêmes indices `piVar11[n]` y désignent donc des champs différents, et j'avais pris le
compteur avancé de 0x4000 par image pour une durée de vie alors que c'est le **Z** — les particules
dérivent en profondeur et sont resemées au-delà de 0x7fffff. La couleur est en `+0x1c`, pas `+0x18`.
Corrigé avant le premier lancement, mais c'est exactement le genre d'écart qu'un pseudo-code à deux
bases voisines fabrique.

Les deux corrections d'aspect appliquées aux icônes sont de simples divisions entières une fois le
codage MSVC démêlé : `x = (x - 0x80) * 320 / 256 + 0x80` (que Ghidra rend avec son correctif de
signe `+ (v >> 31 & 0xff)`) et `y = (y - 0x70) * 242 / 224 + 0x70` (rendu par une multiplication
magique par 0x92492493). Elles ramènent une carte cadrée en 256x224 au 320x240 de l'écran — et la
fonction rétrécit d'ailleurs les deux DISPENV à 256x224 le temps de l'envoi de chaque image avant de
les remettre à 320x240.

Tables `.data` relevées pour l'occasion : `DAT_0056d240` (12 positions d'icône empaquetées),
`DAT_0056d270`/`272` (pose de caméra par zone), `DAT_0056d2a0`/`DAT_0056d2b0` (zone ↔ index de menu,
**vérifiées réciproques** sur 0..9, ce qui les confirme l'une l'autre), et `DAT_0056c978` — une
instance de plus du descripteur d'interface à 0x8c8 octets, dont `DAT_0056c97c` et `DAT_0056c980` ne
sont pas des globales isolées mais ses champs `+4` (x du panneau) et `+8` (sa largeur) : la boucle
les lit et les réécrit chaque image pour faire glisser le panneau du bord opposé à la caméra.
Quinzième et seizième instances du défaut d'aliasing, évitées d'emblée cette fois.

**Un sens enfin tranché** : la sortie vers la zone 0 recopie `+0x148` → `+0x14c` et `+0x14a` →
`+0x14e` pour chaque membre du groupe. `FinalizeCharacterStats` donnait la même valeur aux deux
paires, ce qui laissait indécidable laquelle était le maximum ; ici c'est le soin au retour en ville,
donc **`+0x148`/`+0x14a` sont le maximum et `+0x14c`/`+0x14e` le courant**.

La borne suivante est l'état de jeu 3 (`thunk_FUN_00516020`).

Restent non confirmées visuellement : les icônes de destination elles-mêmes. Avec une progression
nulle, les deux seules dessinées (index 0 et 3) ont la même entrée `0x00000000` dans la table de
positions, donc elles se superposent au même endroit — il faudra une sauvegarde avancée, ou forcer
`DAT_005e901e`, pour les voir séparément.

## 2026-08-18 — le nom de fichier de sauvegarde : `DAT_00566bc0` était un pointeur, pas un tampon

Vincent a signalé que la sauvegarde produisait toujours un fichier `TEST`, alors que le jeu de 1998
écrit des noms de la forme `Blaze-Saving@MVB@R_`, et a demandé de pouvoir créer plusieurs
personnages différents avec un fichier par sauvegarde. Les deux demandes tenaient dans le même
défaut de portage.

### Ce qui bloquait

`WriteCharacterCode` recopie le nom de fichier depuis l'octet 0 de `pcRam00566bc0`, alors que la
finalisation n'écrit les sept caractères de code qu'à partir de l'octet `0xc`. Notre port
initialisait ce symbole à un tampon de 32 octets à zéro, donc la copie s'arrêtait sur le premier
octet nul : nom vide, d'où l'échafaudage `OPENFORESIA_FORCE_CARD_NAME=1` qui injectait « TEST ».

La séance du 2026-08-16 avait lu les octets *à* `0x00566bc0`, trouvé une forme de table de
pointeurs plutôt qu'une chaîne, et refusé — à raison — de s'en servir tels quels.

### Ce qui manquait : une déréférence

`pcRam00566bc0` est un **pointeur logé à cette adresse**, pas le tampon lui-même. Tranché en
désassemblant les huit sites de `.text` qui la touchent (recherche des motifs d'octets directement
dans `Legacy/Binaries/Blaze.exe`) : tous chargent l'adresse — `8B 35 / 8B 0D / 8B 15  C0 6B 56 00`, soit
`MOV reg,[0x566BC0]` — avant d'écrire à travers (`88 54 30 0C` = `MOV [EAX+ESI+0xC],DL`). Et aucun
ne l'écrit jamais : pas un seul `A3 C0 6B 56 00` ni `89 xx C0 6B 56 00` dans tout `.text`, donc le
pointeur garde sa valeur de chargement pour toute la partie.

Il vaut `0x00568498`, où se trouve `"Blaze-Saving"` suivi de huit espaces et d'un NUL. Les douze
premiers octets, dont le rôle était noté « non établi », sont donc simplement le **préfixe
littéral** ; les sept caractères de code atterrissent dans les espaces, et le NUL se pose à `+0x13`.
`12 + 7` — exactement la forme que Vincent décrivait. Corroboration de voisinage : `0x00566bb8` et
`0x00566bbc` portent les pointeurs de `"SYSTEM.INI"` et `"BLAZE.ALL;1"`, `0x00566bc0` est l'entrée
suivante de la même table de noms de fichiers. Consigné dans `Documentation/Reference/DataSymbols.md`.

Même piège, à trois symboles d'écart, que celui déjà corrigé dans `SetCardPathDeviceFields` : la
pseudo-C de Ghidra montre une déréférence de moins que l'assembleur. Quand un symbole `DAT_`/`pcRam`
« contient » ce qui ressemble à une adresse plausible du binaire, la question à poser n'est pas
« que valent ces octets ? » mais « combien de déréférences le code machine fait-il ? ».

### Le nom tapé entre bien dans le code

Un commentaire périmé affirmait que les seize octets de `scratch+0x100` que le hachage replie
étaient « toujours nuls, aucun code reconstruit ne les écrit ». C'est le **nom tapé** : la saisie
(`HandlePadNameEntry`/`HandleTypedNameEntry`) les y écrit un par un, et `WriteCardSaveFile` les
relit au même endroit pour l'en-tête du fichier. Deux personnages ne partagent donc leur fichier que
s'ils ont à la fois le même nom, la même classe, le même élément, le même style et exactement la
même répartition de points. `legacy-bug.md` LB-002 a été récrit en conséquence : son énoncé d'alors
(« le nom tapé est le nom de fichier ») décrivait le défaut de portage, pas le jeu de 1998.

### Vérifié en jeu, et le parcours réel est maintenant scriptable

`OPENFORESIA_FORCE_SUBSTATE=7` ne convient plus pour tester la sauvegarde : il saute la
finalisation. `=6` non plus (le moteur de texte n'a pas encore mis la page en page :
`InvalidOperationException` dans `DrawUiPanel`). Le vrai parcours de création tient en fait dans un
seul `-Then` — carrousel, saisie du nom au clavier, style, élément, dé de bonus, puis la répartition
validée d'office par `OPENFORESIA_FORCE_CONFIRM_STATS=1`. Recette complète dans `test-protocol.md`.

Deux runs successifs, « VINCENT » puis « MARIE », sans plantage :

```
OpenForesia/save/Blaze-Saving@MJAXY_   24576 octets
OpenForesia/save/Blaze-Saving@MJAX]A   24576 octets
```

En-tête `53 43 11 03` (`"SC"`), puis le nom encodé sur deux octets par caractère à partir de
l'offset 4 (`82 6C 82 60 82 71 82 68 82 64` = `MARIE`). Un personnage, un fichier — comme
l'original.

### Nettoyage

- `OPENFORESIA_FORCE_CARD_NAME` retiré de `Blaze.cs` et de toutes les recettes qui s'en servaient
  (`test-protocol.md`, `test-todo-later.md`).
- `OpenForesia/save/TEST` — suivi par git depuis le commit « Trouvé un code triche Gurkha » —
  supprimé de l'arbre de travail : plus rien ne le produit, et sa seule conséquence restante était
  le plantage au deuxième lancement. La suppression reste à valider par un commit.
- L'entrée 1 de `test-todo-later.md` (création de bout en bout sans échafaudage) est réduite à ce
  qui reste vraiment ouvert : les appuis exacts de l'écran de répartition des points.

Le plantage `0xC0000005` dans `ResetCardTransferStatus`/`Ordinal_67` quand le fichier existe déjà
(branche « chargement » du `case 1`) reste ouvert, inchangé — il est maintenant simplement plus
facile à éviter, un nom différent suffisant à changer le fichier.

## 2026-08-18 — le 0xC0000005 « fichier déjà présent » : huit gestionnaires enregistrés avec un pointeur nul

Suite directe de l'entrée précédente. Vincent a demandé de corriger le plantage sur fichier
existant, et signalé que la sauvegarde semblait ne plus rien écrire dans `save\`.

### Le fichier était bien écrit — dans l'autre `save\`

`OpenForesia/bin/Debug/net9.0/save/Blaze-Saving@MZB@R_`, 24576 octets, horodaté de son propre
lancement. Le chemin du jeu est relatif (`save\<nom>`), donc il suit le **répertoire de travail du
processus** : un exe lancé directement écrit dans `bin/Debug/net9.0/save/`, Rider et `dotnet run`
dans `OpenForesia/save/`. Rien de cassé, un décalage de dossier. Consigné en tableau dans
`test-protocol.md` — c'est le genre de faux négatif qui se represente.

### Le plantage : `Ordinal_10` arme, il ne désenregistre pas

`FUN_00504460` enregistre huit gestionnaires auprès de winsys3d.dll (`Ordinal_6`) puis appelle
`Ordinal_10` sur chacun. La note du port disait « registers, then immediately unregisters » — d'où
la conclusion, jusqu'ici, que ces gestionnaires ne servaient à rien et que leurs cibles pouvaient
rester à `null`.

`FUN_00a04220` (l'ordinal 10) fait pourtant une seule chose : `état 0x1000 → 0x2000`. C'est un
**armement**. Le vrai désenregistrement est l'ordinal 7 (`FUN_00a04130`, qui délie et `Free`), jamais
appelé. Les huit nœuds restent donc vivants toute la partie, et le répartiteur `FUN_00a04270` — que
`Ordinal_67/68/69` déclenchent — appelle le pointeur du nœud trouvé. Avec `null`, c'est un appel à
l'adresse 0.

Ce qui explique la forme exacte du symptôme : le plantage n'arrivait que sur la branche « fichier
présent » du `case 1`, la seule à appeler `ResetCardTransferStatus` → `Ordinal_67`.

### Les huit cibles sont lisibles : neuf octets chacune

Aucun corps dans `Blaze.c` (fonctions uniquement prises par adresse — catégorie « re-run Ghidra »).
Mais lues directement dans `Legacy/Binaries/Blaze.exe`, ce sont huit talons `C7 05 <globale> <imm32>` + `C3`,
soit une affectation et un retour :

```
0x00504610  MOV dword [0x00604AB0],0      0x00504650  MOV dword [0x00604A00],0
0x00504620  MOV dword [0x00604AB0],1      0x00504660  MOV dword [0x00604A00],1
0x00504630  MOV dword [0x00604AB0],2      0x00504670  MOV dword [0x00604A00],2
0x00504640  MOV dword [0x00604AB0],3      0x00504680  MOV dword [0x00604A00],3
```

`DAT_00604ab0` est le statut d'écriture, `DAT_00604a00` celui de lecture — exactement les deux
globales que `PollCardWriteStatus`/`PollCardReadStatus` sondent, et `0..3` exactement les valeurs
que `PollCardTransfer` décode. C'est le chaînon qui manquait pour qu'un statut puisse quitter sa
valeur d'inactivité, 4. Transcrits en huit méthodes `[UnmanagedCallersOnly]`.

**Une adresse « sans corps décompilé » n'est pas une impasse quand elle est prise par adresse dans
du code déjà lu** : neuf octets suffisaient. Le réflexe « ça demande un re-run Ghidra » a coûté ici
plus cher que la lecture directe. À tenter avant de classer, au moins quand les adresses sont
proches et régulièrement espacées — l'espacement de 0x10 disait déjà que les corps étaient minuscules.

### Deux défauts d'aliasing de plus, et une section critique dépareillée

- `DAT_00604b58/5c/60/64` et `DAT_00604b68/6c/70/74` ne sont pas huit petites structures à allouer :
  ce sont **les poignées rendues par `Ordinal_6`**, que `FUN_005047e0`/`FUN_00504730` repassent à
  `Ordinal_9`. Le port allouait huit blocs de 12 octets à zéro. Dix-septième à vingt-quatrième
  instances du même défaut.
- `SetupJoystickHandlers` appelait `Ordinal_41` (`LeaveCriticalSection`) sans son `Ordinal_40`
  (`EnterCriticalSection`) — omis parce que le corps de l'ordinal 40 était noté « non lu ». Il fait
  six octets. Rétabli.

### `case 0x10` n'était pas un « formatage » mais la suppression, et c'est la réécriture

Une fois le plantage levé, le parcours atteint `case 0x10`, jusque-là derrière
`ThrowSaveCardUnresolved`. Sa lecture (`Legacy/Raw/Blaze/FUN_0050a910.c:487-511`) le rend
entièrement : il supprime la sauvegarde existante (`Ordinal_20` = `DeleteFileA`) puis remet la phase
à 0 — le `case 1` ne trouve alors plus de fichier et l'écriture normale reprend. **C'est comme ça
que l'original réécrit une sauvegarde.**

Deux détails transcrits tels quels plutôt que « corrigés » :

- `Ordinal_20` rend **1 sur échec**, 0 sur succès (`NEG EAX; SBB EAX,EAX; INC EAX`).
- la boucle de dix tentatives sort dès que l'appel rend non nul, donc c'est la *seconde* tentative —
  sur un fichier désormais absent — qui la termine. La branche d'erreur ne se déclencherait que si
  dix suppressions consécutives réussissaient : inatteignable.

### Vérifié en jeu

- Personnage déjà sauvegardé, rejoué à l'identique : aucun plantage, « Enregistrement achevé. » à
  l'écran, fichier réécrit sur place (horodatage à jour, contenu à jour).
- Personnage neuf dans la foulée : second fichier créé, le premier intact.

```
OpenForesia/save/Blaze-Saving@MJA@[_   24576   (LEA, réécrit)
OpenForesia/save/Blaze-Saving@MJA@]A   24576   (PAUL, nouveau)
```

Restent derrière `ThrowSaveCardUnresolved` : les cas 4, 0xd, -5 et -3/-2.

## 2026-08-18 — la sous-couche carte mémoire est du PlayStation littéral ; les six cases restantes sont transcrites

Vincent a demandé les cas 4, 0xd, -5 et -3/-2, et avant ça d'où venait l'impression que « cette
fonction a des noms de carte mémoire PS1 ». La réponse à la seconde question a rendu la première
beaucoup plus simple.

### Ce n'est pas une ressemblance : winsys3d.dll est une réimplémentation de PSY-Q `libapi`

Les ordinaux bas de la DLL suivent l'ordre de déclaration de `libapi`, sur deux séries contiguës :
6..13 sont les huit fonctions d'événement (`OpenEvent`, `CloseEvent`, `WaitEvent`, `TestEvent`,
`EnableEvent`, `DisableEvent`, `DeliverEvent`, `UndeliverEvent`), 14..16 les trois fonctions de
thread, 18..25 les huit fonctions de fichier (`firstfile`, `nextfile`, `erase`, `open`, `close`,
`lseek`, `read`, `write`). Chaque corps est le Win32 correspondant. Tableau complet dans
`Documentation/DLL/winsys3d.md`.

Les constantes le confirment une à une, sans place pour la coïncidence : `0xf4000001` = `SwCARD`,
`0xf0000011` = `HwCARD`, les modes `4 / 0x8000 / 0x100 / 0x2000` = `EvSpIOE / EvSpERROR / EvSpTIMOUT
/ EvSpNEW`, le troisième argument `0x1000` = `EvMdINTR`, et les états de nœud `0x1000 / 0x2000 /
0x4000` = `EvStWAIT / EvStACTIVE / EvStALREADY`.

Trois choses qui semblaient arbitraires deviennent lisibles d'un coup :

- **`DAT_00604980` est un `chan` PlayStation**, `port * 16 + carte`. D'où `(x & 3) + (x & 4) * 4`
  (le canal complet) à certains sites et `(x & 4) << 2` (le port seul) à d'autres — deux extractions
  du même encodage, pas deux formules concurrentes.
- **`"Blaze-Saving*"` est un motif de `firstfile`**, pas un nom de périphérique.
- **Les huit talons `&LAB_00504610..680` sont des rappels d'`OpenEvent`** — ce qui explique qu'ils
  fassent trois instructions.

### Et le fichier de sauvegarde est un vrai fichier de carte mémoire PS1

`WriteCardSaveFile` compose la « title frame » standard sans jamais la nommer, et tout ce qu'on
avait relevé au fil des séances s'y range :

```
+0x00  "SC"                 signature
+0x02  0x11                 drapeau d'icône ; nibble bas = nombre d'images (1..3)
+0x03  0x03                 nombre de blocs de 8 Kio → 3 * 0x2000 = 0x6000 = la taille observée
+0x04  titre Shift-JIS      pleine chasse, deux octets par caractère → c'est ça, NameEncodeChar
+0x60  CLUT 16 couleurs     capturée en VRAM par Ordinal_684
+0x80  icône 16x16 en 4 bpp 128 octets par image
```

Deux corrections de lecture en découlent, toutes deux dans `LoadSaveDirectory` :

- `DAT_0060498c` n'est **pas** un « nombre de personnages » du fichier mais le **nombre d'images de
  l'icône** — renommé `s_CardIconFrameCount_0060498c`. Les `(n-1)*0x80` octets lus ensuite sont les
  images 2 et 3, et la hauteur du rectangle VRAM (`n * 16` lignes) le confirme. Son autre usage
  connu dans `Blaze.c` (diviseur d'une animation cyclique) corrobore : c'est le cycle d'animation de
  l'icône.
- La boucle qui pose le bit `0x8000` sur seize ushorts à `+0x60` n'est pas un « marqueur de case
  d'inventaire » : c'est le correctif **STP** habituel sur une CLUT PS1 (couleur 15 bits + bit de
  semi-transparence ; `0x0000` = transparent, toute autre couleur doit avoir le bit 15 levé).

### Les six cases restantes, transcrites

Toutes lues dans `Legacy/Raw/Blaze/FUN_0050a910.c`. `ThrowSaveCardUnresolved` n'a plus aucun site
d'appel et a été retiré.

| case | ce que c'est |
|---|---|
| 4 | attente de fin de transfert + réarmement, puis intitulé vide ou drapeau de sortie selon la réponse |
| 0xd | **la même suppression que 0x10, mais sur l'entrée choisie dans le navigateur** (`DAT_006049bc + curseur * 0x28`), pas sur le personnage courant |
| 0x10 | suppression de la sauvegarde du personnage courant, puis retour phase 0 (fait la veille) |
| -5 | attente + réarmement + relecture de `dialogGate`, puis **chute délibérée sans `break` dans le case -4** |
| -3 / -2 | sur résultat -3 on repart à la phase 0 ; sinon attente + réarmement + drapeau de sortie |

La boucle de dix suppressions apparaissait à l'identique dans 0xd et 0x10 : extraite en
`DeleteCardFileRetrying`, au même titre que les `Retry…CardFile` voisines.

`PollCardWriteStatus(..., wait: true)` est appelée par quatre de ces cinq cases et **ne touche plus
sa garde** : depuis que les huit gestionnaires publient vraiment un statut, la valeur a déjà quitté
4 quand on arrive là. La garde ne subsiste que pour le cas réellement bloquant.

### Vérifié en jeu

Personnage neuf puis le même rejoué à l'identique : aucun plantage dans les deux cas, fichier créé
puis réécrit sur place. Aucune régression du parcours normal.

### Ce qui reste ouvert sur cet écran

Un seul point, et ce n'est plus une case : **rien n'appelle `LoadSaveDirectory`** (FUN_005126c0).
Aucun appelant dans `Blaze.c` ni dans les re-décompilations brutes — la fonction est probablement
prise par adresse depuis du code non reconstruit. Conséquence visible : la table des répertoires
reste à zéro, donc le navigateur de sauvegardes (phases 10..13, cases 0xa/0xc) s'affiche vide au
lieu de lister les fichiers réellement présents dans `save\`. C'est aussi ce qui rend le cas 0xd
inatteignable en pratique pour l'instant, bien qu'il soit transcrit.

## 2026-08-18 — l'écran de constitution du groupe (FUN_00536ea0), et « Début du jeu » branché

Vincent a proposé de repartir du menu principal et de brancher l'entrée « Début du jeu », en
pariant que `LoadSaveDirectory` y serait utilisée. **Le pari était bon** : `FUN_00536ea0` appelle
`FUN_005126c0` à sa quatrième ligne, et c'est son **seul appelant dans tout le binaire**. Le point
laissé ouvert à la fin de la séance précédente se referme donc de lui-même : la table des
répertoires n'était vide que parce que l'écran qui la remplit n'était pas transcrit.

### Où ça se branche

L'état de jeu 1 a deux chemins, et les deux entrées que Vincent a repérées sur le jeu de 1998 —
« Début du jeu » au menu principal et « Constituer un groupe » à la fin de la création — y mènent
toutes les deux :

```
menu principal, entrée 0 ──┐
                           ├──► état de jeu 1 ──┬─► chemin A : RunDebugPartySelect  (Gurkha + SELECT tenu)
« Constituer un groupe » ──┘                    └─► chemin B : RunPartyConstitution (le cas normal)
```

Le chemin B était le dernier `NotImplementedException` de `RunGameState1Entry`. Brancher plus tard
« Constituer un groupe » sur l'écran de sélection des personnages ne demandera donc plus que de
poser `s_GameStateId = 1` au bon endroit.

### Ce que fait l'écran

Trois temps. **Un** : `LoadSaveDirectory` énumère `save\Blaze-Saving*`, puis une ligne de 0x16
octets est composée par fichier — nom sur 11 caractères complété d'espaces, abréviation de classe
sur 4 (`DAT_0056c868`, pas de 5), `'N'`, puis le niveau en décimal. Un fichier qui ne s'ouvre pas
donne une ligne de huit tirets. **Deux** : une boucle d'image avec une machine à six sous-phases
(navigation, deux boîtes de dialogue de confirmation) et une fenêtre de six lignes qui défile.
**Trois** : à la sortie, chaque membre retenu est relu depuis son fichier, sa somme de contrôle
vérifiée, et le premier d'entre eux restaure l'état partagé du groupe.

**Aucune sauvegarde du tout** : la fonction pose `s_GameStateId = 4` et revient aussitôt — l'écran
de sélection des personnages. C'est le parcours « premier lancement », et il explique pourquoi
« Début du jeu » mène directement à la création tant que `save\` est vide. Vérifié en jeu.

### `RestorePartyGlobals` : l'autre moitié du format de sauvegarde

Le bloc `DAT_005e9020 == 0` restaure, depuis le seul enregistrement du premier membre, huit banques
de 0x40 octets prises à `+0x1d00..+0x1efc`, trois compteurs à `+0x1f01..+0x1f03`, et huit paires de
mots à `+0x1f04`. Ce sont **exactement** les zones que `FinalizeCharacterStats` remet à zéro à la
création, dans le même ordre et avec les mêmes tailles — les deux fonctions sont les deux moitiés du
même format, et se confirment mutuellement. Les huit banques tombent toutes dans l'arène 0x5e9000
déjà modélisée, donc pas une seule nouvelle allocation à risque d'aliasing.

### Un défaut de portage exposé par le nouveau chemin

`s_SelectScratch_00573338` était un **champ affecté une seule fois**, par
`LoadCharacterSelectResources`. Atteindre l'état de jeu 1 sans être passé par l'écran de sélection
des personnages — ce que fait précisément « Début du jeu » — le laissait donc nul, et le
`NativeMemory.Clear` de `RunGameState1Entry` partait en `NullReferenceException` à la première
image. L'original n'a pas ce problème : il recalcule `*MainMem + 0xf0000` sur place à chaque site
(visible tel quel dans `FUN_00536ea0`). Converti en propriété dérivée de MainMem.

C'est la forme la plus courante de défaut que ce projet produit : **une valeur que l'original
recalcule et que le port met en cache**. Le cache est correct tant qu'un seul chemin y mène ; le
second chemin le prend en défaut.

### Vérifié en jeu, de bout en bout

Menu principal → « Début du jeu » → la liste, montrant `ARTHUR      Gue. N1` sous le titre
« - Organisation du groupe - » → CARRÉ pour retenir le personnage → START → « Veux-tu jouer avec ce
groupe? » → CARRÉ → la boîte Oui/Non → Oui → le fichier est relu, le groupe monté, et le jeu avance
jusqu'à `thunk_FUN_00516020 (game state 3) not yet reconstructed`.

Deux choses valent d'être notées au passage :

- la boîte **Oui/Non répond bien** sur cet écran, sans échafaudage : le widget de `DrawUiPanel`
  dépose la réponse là où `confirmGate` la lit. Le blocage de l'écran de sauvegarde (case 0x12) n'est
  donc pas « le sous-système de dialogue manque » en général — il faudra le reprendre avec ce
  contre-exemple en main ;
- l'état de jeu 3 (`thunk_FUN_00516020`) est la nouvelle frontière, et elle est atteinte par le
  parcours normal du jeu, pas par un raccourci.

### Ce qui reste ouvert ici

L'icône de sauvegarde de chaque ligne s'affiche mais son contenu est un aplat.

> **Réglé le jour même** — ce n'était pas une capture « pas encore peuplée » mais un rectangle de
> capture mal deviné dans `WriteCardSaveFile`. Voir l'entrée suivante.

## 2026-08-18 — les icônes de personnage : un rectangle de capture deviné, et un TODO qui avait vu juste

Vincent a signalé que les icônes de l'écran de constitution du groupe sortaient buggées — un aplat
rouge de 16x16 à la place de chaque portrait.

### Le TODO du port avait annoncé exactement ce cas

`WriteCardSaveFile` capture deux fois depuis la VRAM (`Ordinal_684`), vers `+0x60` (la CLUT) puis
`+0x80` (l'icône). La pseudo-C ne montre **qu'un seul rectangle**, jamais réécrit entre les deux
appels, avec seulement deux adresses de pile différentes (`&stack0xffffffd4` contre
`&stack0xffffffe0`). Une séance antérieure avait transcrit la réutilisation du même rectangle, en
notant : « à revoir si l'icône affichée dans un futur navigateur de sauvegardes sort visiblement
fausse ». C'est arrivé, mot pour mot.

### Le désassemblage montre la seconde construction, entièrement fondue par Ghidra

`Legacy/Binaries/Blaze.exe`, `0x0050B197..0x0050B1E6` — trouvé en cherchant les octets `16 8F 5E 00` dans
`.text` (les deux seules lectures de `DAT_005e8f16` du binaire tombent dans ce bloc) :

```
MOV CL,[0x5E8F14]          ; l'autre moitié de l'entrée 5 de la table de pages
AND ECX,ESI                ; ESI = 0x10
MOV BL,[EAX+0x150]         ; l'octet d'identité du personnage
AND EDX,0x0F   AND BL,0x03
LEA EDX,[EDI + EDX*4]   SHL EDX,4   MOV [ESP+0x14],DX     ; x
MOV word [ESP+0x18],4                                      ; largeur 4
ADD ECX,EDX             SHL ECX,4   MOV [ESP+0x16],CX     ; y
MOV [ESP+0x1A],SI                                          ; hauteur 0x10
```

**Largeur 4, hauteur 0x10** — très exactement les 4 mots de 16 bits sur 16 lignes qu'une icône
16x16 en 4 bpp occupe, soit les 128 octets que la « title frame » attend à `+0x80`. L'ancienne
transcription écrivait un rectangle `0x10 x 1`, la forme de la **CLUT** : 32 octets seulement,
laissant `+0xa0..+0xff` à zéro. D'où l'aplat.

Elle lit aussi `DAT_005e8f14` et non `DAT_005e8f16` — les deux moitiés de la même entrée de table,
que la pseudo-C avait confondues en gardant la seconde.

### Vérifié en jeu

Un fichier produit après correction porte 112 octets non nuls sur 128 à `+0x80`, avec une structure
de nibbles visiblement figurative. À l'écran, deux personnages de classes différentes donnent bien
deux portraits différents et corrects (ARTHUR/Guerrier et MERLIN/Voleur) — ce qui vérifie non
seulement que quelque chose est capturé, mais que la dérivation `x = ((identité & 3) + (tuile & 0xf)
* 4) << 4` / `y = ((tuile & 0x10) + (identité & 0xc)) << 4` vise la bonne case.

**Une sauvegarde écrite avant ce correctif garde son icône vide** : le défaut était à l'écriture,
pas à l'affichage. Il faut recréer le personnage pour voir la correction.

### La leçon, qui vaut au-delà de ce cas

Quand la pseudo-C montre **deux appels avec deux adresses de pile différentes mais une seule
structure construite**, ce n'est pas une réutilisation : c'est une seconde construction que le
décompilateur a fondue dans la première. Le décalage entre les deux adresses (ici 0xC) est
l'indice — il n'y aurait aucune raison de passer une adresse différente pour la même donnée. Aller
directement à l'assembleur ; c'est vingt lignes à lire.

## 2026-08-18 — le choix final de l'écran de sauvegarde : une seule ligne manquait

Vincent a demandé de terminer le choix final de l'écran de sélection des personnages : « Définir de
nouveaux personnages » (retour au début de l'écran) et « Constituer un groupe » (vers l'écran
d'organisation du groupe fait plus tôt dans la journée).

**Le code des deux branches était déjà là et correct** (`case 0x12` de `StepSubState7Phase`, à la
virgule près de l'original). Ce qui manquait était une seule ligne, ailleurs, et elle empêchait le
choix de jamais s'afficher.

### Le symptôme, et pourquoi il trompait

L'écran passait de « Enregistrement achevé. » directement au carrousel, sans montrer la boîte à deux
lignes. La lecture naïve accusait le sous-système de dialogue modal — le suspect habituel de ce
projet, `confirmGate` restant à zéro. C'était faux.

### La trace, et ce qu'elle a montré

Un échafaudage temporaire relevant la phase, les drapeaux du descripteur et le pointeur de flux du
moteur de texte, image par image, a donné la chaîne complète :

1. le libellé `LabelAt(0x1d8)` est **exactement le bon** :
   `<0C>Définir de nouveaux personnages<0D>Constituer un groupe<0D><09>?<00>` — le `<09>` est le code
   de contrôle « choix », son opérande `?` (0x3F) désigne l'entrée où déposer la réponse, et
   `s_PanelSelection_005e9060[0x3f]` **est** `confirmGate` ;
2. la phase 0x11 le liait bien (trace `[CARDBIND]`) ;
3. mais à l'image suivante le flux du moteur de texte pointait sur la fin de
   « Enregistrement achevé.<0F> », pas sur le nouveau libellé, et le drapeau levé était le bit 1
   (attente d'appui) au lieu du bit 2 (choix armé).

Quelque chose réécrivait le libellé entre les deux.

### La cause : `state[2]` n'était jamais remis à -1

La boucle d'image de l'écran de sélection relie le descripteur de statut **à chaque image**, tant que
`state[2]` (`DAT_00578df0`) est positif :

```c
if (-1 < DAT_00578df0)
    FUN_00544350(directory[0x70][DAT_00578df0 * 4], 0, &DAT_00560728, 0);
```

Le sous-état 6 entre dans le sous-état 7 avec `state[2] = 0x6e`, soit `LabelAt(0x1b8)`,
« Enregistrement achevé. ». Et le bloc d'entrée du sous-état 7 remet `state[2]` à -1 pour reprendre
la main — `_DAT_00578df0 = 0xffffffff;`, `Legacy/Raw/Blaze/FUN_0050a910.c:59`. **Cette ligne
manquait.** Sans elle, la boucle d'image réécrasait « Enregistrement achevé. » par-dessus tout ce que
la machine à phases liait, à chaque image, et la phase 0x12 trouvait son drapeau de choix à zéro :
elle partait sur la première branche sans que rien ne s'affiche.

Corrigé en même temps : le même bloc posait `s_CardPhase_00578e0c = -1` là où l'original pose `0`
(sans conséquence observable — le bloc suivant le remet à 0 dans la même image — mais c'est un écart).

### Ce que la correction débloque, au-delà du choix

Le panneau de **récapitulatif du personnage** (portrait, nom, classe, élément, les huit statistiques)
apparaît enfin sur l'écran de sauvegarde. Il était lié puis écrasé lui aussi, chaque image, par la
même ligne manquante — personne ne l'avait relié au même défaut. C'est l'étape VI.1 de
`Analyse/Comportement Legacy/Selection des personnages.md`.

### Vérifié en jeu, les deux branches

- **« Définir de nouveaux personnages »** (ligne 1, curseur par défaut) → retour au carrousel,
  « Sélectionne un personnage. » — l'étape I de la création.
- **« Constituer un groupe »** (BAS puis validation, `desc8c4` passe bien à 01) → « Organisation du
  groupe », listant les deux personnages sauvegardés.

La boîte Oui/Non répond donc correctement ici aussi, sans échafaudage. Combiné au constat de l'écran
d'organisation du groupe, cela **retire le sous-système de dialogue modal de la liste des blocages** :
il fonctionne partout où le libellé porte son code de contrôle et où le descripteur n'est pas
réécrasé. Le blocage historique du `case 0x12` était ce réécrasement, pas le dialogue.

### La leçon

Quand un widget d'interface « ne s'arme jamais », vérifier d'abord **qui d'autre écrit dans son
descripteur pendant la même image**, avant de soupçonner le mécanisme d'armement. Ici la machine à
phases et la boucle d'image liaient toutes deux le même descripteur, et la seconde gagnait.

L'échafaudage `OPENFORESIA_TRACE_CARDPHASE` de cette enquête a été retiré, comme le veut la règle du
projet.

## 2026-08-18 — l'écran de transition entre états : deux défauts superposés, dont un de cadence

Vincent a signalé qu'entre le menu principal et la sélection des personnages, « il y a une image
qui est censée apparaître ». Elle ne l'était pas, pour **deux raisons indépendantes** qui se
masquaient l'une l'autre.

### Défaut 1 — l'image n'était pas dessinée du tout

`RunLoadingScreen` (FUN_0052a440) chargeait et décodait bien la ressource, mais le champ 0 du
descripteur de sprite — le pointeur vers l'enregistrement de texture — était laissé à `null`, avec
un `TODO(REVERSE)` : la pseudo-C le fait pointer sur un emplacement de pile que **rien n'écrit**,
symptôme du cadre de pile que Ghidra n'a pas récupéré sur cette fonction. Les deux tracés étaient
donc sautés, et le port affichait 150 images vides.

Résolu en désassemblant directement `Legacy/Binaries/Blaze.exe`. L'original monte l'enregistrement octet par
octet juste avant la boucle (`0x0052A51B..0x0052A53D`), puis relie son adresse au descripteur à
chaque tour (`0x0052A57E`, `LEA EAX,[ESP+0x20]` → `MOV [ESP+0x3C],EAX`) :

```
0xE0000000   0x003F0100
```

Le second dword se recoupe tout seul : son octet 2 vaut `0x3F`, l'index de la table de pages, soit
`s_EntityDecodeTable_005e8f00 + 0x3F * 4 = 0x005e8ffc` — **exactement** l'adresse où
`DecodeResourceChunk` vient d'écrire quelques lignes plus haut. Le premier porte la hauteur,
`0xE0` = 224 lignes, ce qui concorde avec le rectangle source `{0x380, 0, 0x80, 0xE0}` de la
première recopie VRAM.

Le second `TODO(REVERSE)` du même site — la destination de la seconde recopie VRAM — est levé par
le même désassemblage, et **la transcription était déjà correcte** : le `LEA EDX,[ESP+0x34]` se lit
avec un `PUSH` déjà passé, donc son décalage effectif est 0x30, soit `rect + 4`.

### Défaut 2 — et surtout : le jeu n'avait aucune cadence

Corriger le tracé ne suffisait pas : les 150 images passaient en **moins d'une seconde**, donc
l'image restait invisible.

La chaîne, remontée jusqu'au bout : le moteur n'attend jamais lui-même. `Ordinal_132`
(winsys3d, `FUN_00a053e0`) est sa seule attente d'image, et son corps s'ouvre sur
`switch (DAT_00a2c3b0)` dont le `case 0` **rend la main immédiatement**. Ce mode de cadencement
n'est posé que par `SetTimingMode`, appelée depuis un seul endroit du binaire — la page « Timing »
du menu de mise en route (`FUN_00510d40`) — jamais sur le chemin de démarrage. Le jeu de 1998
tournait donc en mode 0, et **toute sa cadence venait du `Flip()` DirectDraw**, bloquant sur le
retour de balayage.

Notre `D3D11Backend.Present` présentait avec un intervalle de synchronisation de **0**. Passé à 1 :
150 images mesurées à **2608 ms**, soit les 2,5 s attendues à 60 Hz.

C'est un correctif d'un caractère dont la portée dépasse largement cet écran : **toutes** les
animations du jeu tournaient à la vitesse que la carte graphique voulait bien leur donner. L'écran
de transition est simplement celui où ça se voyait, parce que sa durée est comptée en images.

Réserve assumée, notée dans le code : sur un écran à plus de 60 Hz le jeu tournera d'autant plus
vite, là où l'original était calé sur le 60 Hz de son mode d'affichage. Un vrai limiteur à 60 images
par seconde serait plus fidèle encore ; on se contente ici de restituer le comportement du `Flip`
d'origine.

### Vérifié en jeu

Capture prise pendant la transition : l'illustration sépia du groupe (une fée, un guerrier, une
voleuse, un chevalier sur des pavés) s'affiche plein cadre, pendant 2,6 s, entre le menu principal
et le carrousel de sélection.

### Comment capturer une transition (dans `test-protocol.md`)

`playtest.ps1` capture à la fin de sa fenêtre d'observation, et le script met environ une seconde
entre la validation détectée et le début de cette fenêtre. Pour une transition de 2,6 s, il faut
donc **`-Seconds 0`** ; à `-Seconds 2` on est déjà sur l'écran suivant. Le repère fiable est le
débit de primitives dans le verdict : **1 par image** est la signature de l'écran de transition,
contre ~380 pour la sélection des personnages.

### La leçon

Deux défauts sur le même symptôme, dont le second masquait la correction du premier. Après avoir
corrigé le tracé, le compteur de primitives disait « 1 par image pendant 150 images » — donc *ça
dessinait* — alors que l'écran ne montrait rien. C'est cet écart entre « le rendu émet » et « on ne
voit rien » qui a mené à la cadence. Un compteur qui dit oui quand l'œil dit non désigne le temps,
pas la géométrie.

## 2026-08-18 — `FUN_005115c0`, les boutons de manette, et un fichier de configuration de référence

Deux demandes de Vincent : garder une trace de la version par défaut d'`OpenForesia.cfg`, et
reconstruire la page des boutons de manette du joueur 1 — « il devrait y avoir la même chose pour
joueur 2 / 3 / 4 ».

### La page est paramétrée par le joueur, donc les quatre viennent ensemble

`FUN_005115c0` prend le numéro de joueur en argument et indexe tout par `iVar1 * 0x3c` : son curseur
(`DAT_005d9e18`), son haut de fenêtre (`DAT_005d9e28`) et son bloc de vingt liaisons. Une seule
transcription couvre donc les manettes 1 à 4 — c'est ainsi que l'original est écrit, et l'écran
« OPTIONS JOYSTICK » propose bien ses quatre entrées « Configuration joueur N ».

### Deux détails que seul le désassemblage donne

La pseudo-C perd l'arité de `FUN_005108b0` (sept paramètres, six affichés au site d'appel) :

- la couleur du glyphe de bouton est `0xFFFFFF` — `0x00511682  PUSH 0x00FFFFFF`, le septième
  argument, poussé bien avant les six autres et donc invisible dans le rendu de Ghidra. Le
  `ADD ESP,0x1C` après l'appel (28 octets = sept arguments) le confirmait déjà par le compte ;
- l'ordonnée dans la planche est 0 pour les dix-huit premiers boutons et `0x80` au-delà, via
  l'idiome `SETGE / DEC / AND 0x80 / ADD 0x80` à `0x005116B3`.

### `-1` veut dire « aucun », et la table de libellés l'encode par son octet précédent

La table manette utilise `-1` pour « pas de liaison », pas `0` — le bouton 0 est valide. Le code
lit `(char)(&DAT_00562e99)[liaison]`, et **l'octet qui précède la table vaut `0xff`** : l'index -1
tombe dessus et rend -1 une fois lu en octet signé, ce que la ligne suivante teste pour afficher
« libre ». C'est délibéré, pas un débordement — même idiome que le tampon d'échappements décalé
d'une case du moteur de texte. Reproduit tel quel.

### Le défaut que ça a mis au jour : la table de liaisons n'était pas initialisée

`s_KeyBindings` était un `new int[240]` à zéro. Or `DAT_00564b58` est **pré-rempli à l'édition de
liens**, et `LoadKeyBindings` ne fait qu'écraser les entrées dont la clé existe dans le fichier.
Sans fichier de configuration, les vingt liaisons manette de chaque joueur — qui ont de vraies
valeurs par défaut — restaient donc vides, et `0` y est un bouton valide : l'écran aurait affiché
« gauche » partout au lieu de « libre ».

Les 960 octets sont relus dans `Legacy/Binaries/Blaze.exe` (`.data`, VA `0x00564B58`) et correspondent **clé
pour clé** aux 240 lignes `[Controls]` du BLAZE.cfg installé. Les deux sources se confirment
mutuellement.

Piège d'ordre d'initialisation rencontré au passage : les initialiseurs de champs statiques C#
s'exécutent dans l'ordre de déclaration, et les deux tables de valeurs par défaut sont déclarées
bien plus bas dans le fichier. Le premier jet plantait donc en `TypeInitializationException` au
démarrage. Passé au motif paresseux déjà utilisé pour `s_LoadScreenDescriptors_0056b558`.

### Un décalage d'une case corrigé dans l'éditeur de touches, au passage

`KeyConfigPage` plaçait la flèche de direction avec `s_UiLabelWidths_00589758[-kind]`. L'original
lit `(&DAT_00589754)[-kind]`, et `DAT_00589754` vaut `DAT_00589758` **moins un dword** : c'est donc
`[-1 - kind]`, la largeur du libellé que la même ligne vient de dessiner. Corrigé ; la page des
boutons de manette, écrite d'emblée avec la bonne expression, servait de contre-exemple.

### Le fichier de configuration de référence

`Documentation/Reference/OpenForesia.cfg.default` — copie octet pour octet du BLAZE.cfg installé, dont le
contenu est vérifié contre le binaire comme ci-dessus. `Documentation/Reference/OpenForesia.cfg.md` documente
le format : l'encodage `K<joueur><table><action>`, les vingt actions avec leur masque manette
(recoupé avec le tableau vérifié en jeu de `test-protocol.md`), et les pièges — clés périmées qui
survivent parce que `WritePrivateProfileString` n'efface jamais, `Timing` sans effet hors de sa
page, `Size` plafonné par `Resolution`.

Le fichier réel vit à côté de l'exécutable et n'est pas suivi par git : c'est précisément pour ça
qu'une référence manquait.

### Vérifié en jeu

Options → Contrôle → Manettes → « Configuration joueur 1 » : la liste s'affiche avec ses dix-huit
actions par fenêtre de dix, les symboles PlayStation posés après le libellé pour les quatre
directions, les liaisons par défaut correctes (`Touche 1` pour Attaque/□, `haut`/`bas`/`gauche`/
`droite` pour le pavé), le défilement jusqu'à « Rétablir standard », et **« libre »** sur les quatre
actions sans liaison.

Ce dernier point n'est apparu qu'après avoir remplacé le `OpenForesia.cfg` de test, qui portait
encore des `0` là où l'original a `-1` — écrit par une version du port antérieure aux valeurs par
défaut manette. Première utilité concrète du fichier de référence, le jour même de sa création.

## 2026-08-18 — relecture de l'écran de configuration clavier : rien ne manque

Vincent a demandé s'il restait des choses à terminer sur l'écran de configuration clavier.
`FUN_00511d40` a été relue **en entier** contre la transcription, ligne à ligne. Réponse : non, et
la vérification en jeu le confirme.

### Ce qui a été recoupé

| point | verdict |
|---|---|
| fenêtre de onze lignes, table de descripteurs `DAT_00562ed8` | conforme |
| lignes d'en-tête (`kind < 0`) : libellé seul, rien d'autre | conforme |
| liaison principale puis secondaire (`DAT_00564b58` / `DAT_00564ba8`, soit +0 et +20) | conforme |
| court-circuit `(ligne != capture) || (x == 0xb9)` avant la secondaire | conforme, structure comprise |
| « aucun » quand aucune des deux n'est liée | conforme |
| franchissement d'un en-tête dans le sens du déplacement | conforme (`sel <= avant ? -1 : +1`) |
| capture : la première touche listée trouvée enfoncée | conforme |
| l'ancienne principale glisse en secondaire | conforme |
| le scancode retenu est effacé partout ailleurs — **tables 0 et 1 seulement**, sur les quatre joueurs | conforme (bornes `0x28` et `0x564f18`) |
| « rétablir standard » : joueur 1 depuis les défauts, joueurs 2-4 à zéro, **tables manette intactes** | conforme |
| attente de relâchement après validation et après capture | conforme |

### Un décalage d'une case, corrigé

Seul écart trouvé : la flèche de direction était placée avec
`s_UiLabelWidths_00589758[-kind]`. L'original lit `(&DAT_00589754)[-kind]`, et `DAT_00589754` vaut
`DAT_00589758` **moins un entier** — donc `[-1 - kind]`, la largeur du libellé que la même ligne
vient de dessiner. La table est indexée des deux côtés de son adresse de base (les libellés
d'ACTION.TXT descendent depuis `[-1]`), ce qui rend l'erreur silencieuse : elle lisait une largeur
voisine, plausible, jamais absurde. Corrigé aux deux sites.

### Vérifié en jeu

Options → Contrôle → Clavier : les huit actions globales avec leurs deux liaisons, l'en-tête
« Joueur 1 » dans sa teinte, le défilement. Puis une capture réelle — validation sur la première
ligne, appui sur F1 : la ligne passe de `NumPad 9 / Pg up` à **`F1 / NumPad 9`**, l'ancienne
principale ayant glissé en seconde place et l'ancienne seconde étant tombée. C'est exactement
`[cible+20] = [cible] ; [cible] = scancode`.

### Nettoyage

`OptionsScreenStub` renommée `OptionsPage`, et sa note « the page's own loop is not
[reconstructed] » retirée : les deux dataient du premier jet, où seule la mise en place était
transcrite. La boucle et ses quatre sous-pages le sont depuis longtemps. Un nom en « Stub » sur du
code complet envoie chercher un trou qui n'existe plus.

### Ce qui reste dans cette zone, et qui n'est pas du clavier

La **page résolution** (`FUN_00510e60`) est la seule encore dégradée : le device synthétique
n'expose ni le masque des modes (`+0x1c`) ni le changement effectif (`+0xE8`), donc la liste est
vide et la page n'affiche que son titre. C'est une limite du shim graphique, pas un manque de
transcription — le TODO est posé à cet endroit.

## 2026-08-18 — la page « Définition de l'écran », et une branche de redimensionnement morte depuis toujours

Vincent a demandé de terminer la page résolution — et où elle se trouve. Elle est sous
**Options → Affichage → « Définition de l'écran »**, en sixième ligne. Cette ligne n'existe que si
le device annonce sa capacité, ce qui est la première raison pour laquelle personne ne l'avait vue.

### La page était transcrite en entier ; il manquait trois capacités du device

`FUN_00510e60` était déjà là, fidèle jusqu'aux détails (la libération des huit textures, la vidange
des `WM_QUIT`, le plafonnement de `Size` par la table `DAT_00562e00`). Ce qui manquait était côté
`SyntheticDevice`, dont les slots non implémentés rendent zéro :

| slot | rôle | ce qu'on rend, et pourquoi |
|---|---|---|
| `+0x1c` | masque des modes acceptés | `0x1f` — les cinq modes de la table de winsys3d (320x240 à 1024x768). Le rendu va dans une cible hors écran que `Present` met à l'échelle : tous sont rendables, quel que soit l'écran réel |
| `+0xE0` | « le mode est-il changeable ? » | non nul — c'est ce que la page « Affichage » consulte pour faire exister sa sixième ligne |
| `+0xE8(l, h)` | le changement effectif | non nul. **Doit** l'être : sur zéro, `ChangeResolution` (winsys3d, ordinal 749) abandonne sans poser `DAT_00a2c49c`, donc sans que le nouvel index prenne effet |

Aucun mode d'affichage physique ne change chez nous, et c'est un progrès sur l'original plutôt qu'un
écart : même image, sans reprise de l'affichage.

### Le vrai défaut, que la page a révélé

Une fois les trois slots posés, la liste s'affichait et le changement était accepté — mais l'écran
se **dédoublait** : le jeu dessinait sa nouvelle image 320x240 dans le coin d'une cible restée en
960x720, par-dessus l'image précédente.

`Shim_BeginScene` n'appelait `EnsureBackend` que **si le backend n'était pas encore vivant** :

```csharp
if (!s_BackendLive) Guarded("Initialize", EnsureBackend);
```

Or la seconde branche d'`EnsureBackend` — `EnsureSceneTarget(s_SceneWidth, s_SceneHeight)`, le
redimensionnement de la cible sur le viewport courant — n'est atteignable que quand le backend
**est** vivant. Elle était donc **morte depuis qu'elle avait été écrite** : la cible de rendu gardait
pour toujours la taille du tout premier viewport. Rien ne le montrait tant qu'aucun chemin ne
changeait de résolution en cours de partie. Corrigé en appelant `EnsureBackend` à chaque image ; le
coût est de deux comparaisons d'entiers, la fonction sortant aussitôt si la taille n'a pas bougé.

**Une branche que sa propre garde rend inatteignable ne se voit pas à la lecture** — elle se lit
comme du code normal. Ce qui l'a trouvée, c'est un chemin nouveau qui en avait besoin.

### Vérifié en jeu

Options → Affichage : la ligne « Définition de l'écran » apparaît. Elle ouvre la liste des cinq
modes, `320 x 240` à `1024 x 768`, conforme à la table de `Documentation/DLL/winsys3d.md`. Passage
en 320x240 : l'interface est rendue en 320x240 et mise à l'échelle plein cadre, sans dédoublement.
Passage en 640x480 puis sortie : retour au menu principal, rendu correct, et le fichier de
configuration porte bien `Resolution=2` / `Size=11` — la valeur de `Size` étant celle que la table
`DAT_00562e00` impose pour cette résolution.

### Où en est le menu Options

Toutes les pages sont désormais complètes et exercées : Affichage (et ses deux filles Timing et
Définition de l'écran), Son, Contrôle (Clavier et Manettes, avec la page des boutons par joueur).
Il ne reste aucun `TODO(REVERSE)` ni `NotImplementedException` dans cette zone.

## 2026-08-18 — le menu objets (état de jeu 6) : l'entrée, la boucle et l'écran racine

Vincent a demandé de passer au menu objets, après remise de la configuration d'affichage à ses
valeurs par défaut (`Documentation/Reference/OpenForesia.cfg.default` recopiée ; `Small` était resté à 1
après l'excursion en 320x240 de la séance précédente).

### Le découpage, et le choix de portée

`FUN_00524180` est un chargement (`FUN_00524210`) suivi d'une boucle d'image (`FUN_00524320`) qui
déroule une machine à cinq sous-écrans sur `DAT_00566ac8` :

| sous-état | fonction | taille |
|---|---|---|
| -1, l'écran racine | `FUN_00524680` | 82 lignes |
| 0, « Invertaire » | `FUN_00524880` | **1377 lignes** |
| 1, « Enchère » | `FUN_005277b0` | 267 lignes |
| 3 | `FUN_00527e30` | 683 lignes |
| 4, la fermeture | `FUN_005298c0` | 84 lignes |

Environ 2600 lignes de pseudo-C en tout — l'ordre de grandeur de l'écran de sélection des
personnages, qui a demandé plusieurs séances. Transcrits cette fois : l'entrée, le chargement, la
boucle et l'écran racine. Les quatre sous-écrans sont nommés et gardés, chacun avec sa taille dans
le message, pour que le prochain choix de cible soit immédiat.

### Trois défauts, et ce qui les a trouvés

Le menu se figeait à sa première image, écran noir, sans plantage — donc sans pile à lire. Une
trace posée étape par étape dans l'image l'a cerné en trois runs, et a mis au jour trois erreurs
distinctes :

1. **Deux pas différents dans une même boucle.** L'initialisation des quatre emplacements avance de
   `0x9c` sur un pointeur et de `0x26` sur un `undefined2*` — soit **0x4c octets**, pas 0x9c. Les
   avoir écrits identiques écrasait des champs voisins de l'arène 0x5e9000, dont la table
   d'emplacements que `CurrentPadState` indexe. La pseudo-C cache ce genre d'écart derrière ses
   types de pointeur : deux `+= constante` qui se ressemblent n'ont pas la même unité.

2. **`FUN_00524680` est typée `void` par Ghidra, mais son appelant utilise son retour**
   (`local_20 = FUN_00524680(...)`). EAX porte à la sortie le résultat du dernier appel, celui de
   `FUN_005443a0` — le curseur de primitives que le moteur de texte a avancé. Rendre l'ancien
   curseur laissait les dessins suivants réécrire par-dessus les primitives du bandeau de titre, ce
   qui casse la table d'ordonnancement : **`DrawOTag` ne revenait jamais**, et le gel se produisait
   dans `FrameEnd`, loin de sa cause. Même défaut de type de retour que `RetryOpenCardFile`.

3. **La boucle de vidange d'entrée sans cadencement.** `while (FUN_005048f0() != 0)` ne se termine
   pas chez nous, la pompe de messages ne tournant pas — piège déjà documenté sur la page d'options,
   et retombé dedans. Corrigé de la même façon, `Ordinal_132(0)` dans la boucle.

Le premier gel a coûté trois runs de plus qu'il n'aurait fallu, faute d'avoir tracé tout de suite :
**un écran noir sans exception ne se diagnostique pas à la lecture**, il se cerne en posant des
jalons dans l'image.

### Un piège d'outillage, à ne pas refaire

Le bloc a d'abord été ajouté au fichier par un aller-retour PowerShell
`Get-Content` / `Set-Content -Encoding UTF8`. En Windows PowerShell 5.1, **`Get-Content` lit dans la
page de code ANSI**, pas en UTF-8 : tous les accents de la région ajoutée se sont retrouvés
doublement encodés, ce qui s'est vu à l'écran — « Enchère » sortait mojibaké, puisque la chaîne
elle-même était touchée. Réparé en ré-encodant la seule région concernée (`git diff --stat` a servi
à vérifier que le reste du fichier était resté identique au commit).

Pour ajouter un bloc à un fichier source, passer par `[IO.File]::ReadAllLines/WriteAllLines` avec un
encodage explicite, ou par les outils d'édition — jamais par `Get-Content`/`Set-Content`.

### Ce que l'écran fait

Trois lignes tirées de `PTR_s_Invertaire_00566b58` — **« Invertaire »** (la faute de frappe est dans
le binaire, voir `legacy-bug.md` LB-003), « Enchère », « Quitter » — un curseur qui boucle de 0 à 2,
et la validation qui ouvre le sous-écran correspondant ; la troisième repose `s_GameStateId` à 0 et
rend la main au menu principal. Le bandeau de titre passe par `FUN_00524190`, dont la disposition de
pile a demandé le désassembleur : les quatre octets de tête sont `0C 04 18 10`, et le quatrième
**écrase le premier octet du libellé copié** — lequel commence lui-même par `0x0C`, que ce préfixe
remplace.

Deux descripteurs d'interface contigus de plus relevés dans `.data` (`DAT_00564fe0` et
`DAT_005658a8`, 0x564fe0 + 0x8c8), et le bloc d'état `DAT_005de410`..`DAT_005de50c` modélisé d'un
seul tenant plutôt qu'en vingt champs — l'application directe de la leçon d'aliasing du projet.

### Vérifié en jeu

Menu principal → « Menu objets » : le titre « - Utilisation des objets - », les trois lignes avec
leur curseur, et le panneau du bas « Sélectionne un menu. ». Environ 387 primitives par image.

## 2026-08-18 — menu objets : la fermeture (FUN_005298c0) et « Enchère » (FUN_005277b0)

Suite de l'entrée précédente. Deux des quatre sous-écrans restants sont transcrits ; il reste
`FUN_00524880` (1377 lignes) et `FUN_00527e30` (683).

### « Enchère » est l'écran d'organisation du groupe, redessiné ailleurs

`FUN_005277b0` et `FUN_00536ea0` partagent **la même construction de lignes, à la virgule près** :
même table `DAT_005e8640`, même ordre de sélection `DAT_005e85c0`, même format « nom sur 11
caractères, abréviation de classe, `N`, niveau ». Extraite en `BuildSaveDirectoryRows` et appelée
des deux côtés, comme les `Retry…CardFile` l'avaient été.

Ce qui diffère tient en trois points : les coordonnées (x 0x28 au lieu de 0x48, y +0x26 au lieu de
+0x32, curseur en 0x14 au lieu de 0x34 — la page du menu objets est cadrée en 256x224) ; un drapeau
de chargement unique (`DAT_005de4e8`) qui évite de relire le répertoire à chaque image ; et la
sortie, qui va vers le sous-écran 3 au lieu d'un état de jeu, CROIX ramenant à l'écran racine.

Sans aucune sauvegarde, l'écran repasse aussitôt à la racine — le pendant du `s_GameStateId = 4` de
son jumeau.

### La fermeture est un ré-enregistrement des quatre membres

`FUN_005298c0` rouvre le fichier de chaque membre (le nom vient de son propre enregistrement, à
`+0x1fd0`), relit deux extents dans le tampon de travail, **fusionne les objets**, recalcule la
somme de contrôle et réécrit trois extents.

La fusion : dix emplacements à `+0x300`, pas de 0x280 octets, retenus si leur mot de drapeaux à
`+0x12` porte 0x8000 sans 0x4000. Chacun est cherché dans une liste d'enregistrements de 0x10 octets
terminée par un premier octet nul, par comparaison des quatre premiers dwords ; absent, il est
ajouté sur place, à l'octet terminateur.

Deux bizarreries de l'original transcrites telles quelles :

- les **deux lectures visent le même tampon** — celle de `0x3f00` sur 0x100 octets est aussitôt
  écrasée par celle de `0x4000` sur 0x2000. Le résultat de la première est donc perdu ;
- `DAT_005de4e0`, qui est le curseur de l'écran racine, sert ici de compteur de membre et ressort à
  4. L'écran racine le ramène à 0 par son propre plafonnement, à la première image suivante.

**`CloseItemMenu` n'est pas encore atteignable** : le sous-état 4 n'est posé que par les deux
sous-écrans non transcrits. Elle est écrite et compile, mais rien ne l'exerce — dit ici plutôt que
laissé croire l'inverse.

### Vérifié en jeu

Menu objets → « Enchère » : la liste des sauvegardes s'affiche avec portraits, noms, classes et
niveaux (`ARTHUR Gue. N1`, `MERLIN Vol. N1`), curseur compris, aux coordonnées propres à cet écran.
~398 primitives par image, aucun plantage. L'écran d'organisation du groupe (entrée « Début du jeu »)
est revérifié après l'extraction du helper.

## 2026-08-18 — menu objets : `FUN_00527e30`, l'écran d'utilisation d'objet (partiel, et pourquoi)

Demandé après « Enchère » et la fermeture. **Transcrit en partie** : les phases d'entrée, de
transfert et de dialogue ; les deux phases centrales restent gardées. Le découpage n'est pas un
choix de confort, il suit une différence réelle de lisibilité de la pseudo-C.

### Ce qui est fait

| phase | rôle | état |
|---|---|---|
| prologue | reconstruit l'index des emplacements occupés des quatre membres (`+0x2c0`, `+0x2c1`) | fait |
| 0 | choix du membre, entrée dans sa grille, les deux sorties | fait |
| 3 | le transfert effectif d'un objet vers un autre membre | fait |
| -1, -2 | les deux boîtes de confirmation | fait |
| 1 | la grille d'objets d'un membre (165 lignes) | **gardé** |
| 2 | le choix de la cible du transfert (330 lignes) | **gardé** |

Avec `FUN_00529590` (121 lignes), le bandeau du groupe que trois phases dessinent : par membre, son
portrait, son or et l'emplacement d'objet de plus grande valeur qu'il porte.

Deux extractions au passage, chacune recopiée à l'identique à trois endroits de l'original :
`ComputeItemValue` (le repliement « somme des valeurs absolues doublées » sur un emplacement) et
`RebuildItemIndex`.

**Le trio de champs de descripteur, pour la troisième fois.** `DAT_005658a7`, `DAT_0056501b` et
`DAT_00565013` sont les champs `+0x8c7`, `+0x3b` et `+0x33` du **premier** descripteur du menu
objets (0x564fe0 + ces décalages). Exactement le même trio que sur l'écran de sauvegarde
(`DAT_00560728`) et sur l'écran d'organisation du groupe (`DAT_0056b558`). Quand trois adresses
« isolées » se présentent ensemble avec ces trois écarts, ce sont des champs de descripteur.

### Pourquoi les cas 1 et 2 sont gardés plutôt que transcrits

Le cas 2 n'est **pas relisible en l'état** : Ghidra y a perdu l'adressage et rend des indices en
constantes 64 bits — `pbVar14[-0xffffffff00000008] = 1;` par exemple, qui n'a pas de sens dans un
programme 32 bits. Il y a plusieurs occurrences de cette forme, plus une boucle de sélection de
cible où le décompilateur a mélangé quatre variables de travail. Le transcrire demande une passe de
désassemblage soutenue, du même ordre que celles qui ont tranché aujourd'hui le préfixe `0C 04 18 10`
ou le pas de 0x4c — mais sur trois cents lignes, pas sur trois.

Le cas 1 est lisible, mais il appelle `FUN_005276c0`, `FUN_00529070` et `FUN_00529290` (248 lignes),
dont `FUN_00529070` souffre du même défaut d'adressage, et il tire une demi-douzaine de symboles
`.data` encore à relever.

**Et surtout, rien de tout cela n'est exerçable aujourd'hui** : la grille d'objets suppose un
inventaire, que seul `FUN_00524880` (1377 lignes, non transcrit) peuple. Transcrire mille lignes
qu'aucun run ne peut traverser, c'est exactement la façon dont sont nés les deux défauts de cette
journée — le pas de 0x9c au lieu de 0x4c et le type de retour perdu — tous deux dans du code qu'on
ne voyait pas tourner. Ce qui est livré ici est ce qui a pu être vérifié à l'écran.

### Vérifié en jeu

Menu objets → « Enchère » → sélection d'un personnage → START : l'écran d'utilisation s'affiche avec
le portrait du membre, son or, son meilleur objet et sa valeur, et l'invite « Sélectionne le
propriétaire. » — soit le bandeau de `FUN_00529590` et la phase 0 au complet. ~368 primitives par
image.

---

## 2026-08-18 — `FUN_00524880`, le sous-écran « Invertaire » (1377 lignes)

Transcrit **en entier**, les 26 phases. `ItemMenuInventoryScreen`, plus trois aides :
`BuildItemDisplayName` (`FUN_0051bcc0`), `ItemDisplayName` (`FUN_0051bd30`) et `DrawPanelIconRow`
(`FUN_005276c0`) — ce dernier était l'un des trois helpers gardés de la veille.

### Ce que l'écran fait

Il choisit une sauvegarde, charge son inventaire et l'affiche. La disposition d'un inventaire, qui
n'était pas connue jusqu'ici : **vingt-deux emplacements de 0x280 octets**, `+0x00` la fiche (nom
court sur 0x10, nom long en `+0x41` terminé par `'/'`, quantité en `+0x10`, attributs en `+0x12` et
`+0x13`), `+0x140` une palette de seize couleurs, `+0x160` une icône 24×24 en 4 bits. Les **dix**
premiers vivent dans le premier bloc du fichier de sauvegarde à `+0x300`, les **douze** suivants
dans son second bloc — d'où les trois lectures de la phase 5 et les deux boucles de comptage.

**La somme de contrôle est stockée deux fois dans le fichier**, à `0x1ffc` et à `0x3ffc` : la phase
0x11 réécrit `record[0x1f00..0x1fff]` à l'offset `0x3f00`, ce qui y recopie le même dword. La phase
5 compare donc d'abord les deux exemplaires, puis recalcule la somme sur les `0x1ffc` premiers
octets. La garde qui ressemblait à un artefact Ghidra (`*(record+0x1ffc) == *(MainMem+0x1b38fc)`)
est en fait cohérente avec l'écriture — les deux se recoupent exactement.

### Trois choses que la pseudo-C cachait

**Le tampon de pile de 188 octets du prologue est deux locales fusionnées.** Ghidra montre un
`builtin_strncpy` de 186 octets qui sont, à l'octet près, la table des largeurs de glyphes
proportionnelles — et le même tampon sert ensuite de tampon de commande texte aux phases 8 et à la
queue. Ce n'est pas une contradiction : la phase 10 passe la table globale `DAT_0056e7b0` en chasse
fixe (0x08 partout) pour aligner sa grille de caractères, et la **restaure depuis cette copie** en
fin de phase. Le prologue la refaisant à chaque image, les deux usages ne sont jamais vivants
ensemble. Sans cette lecture de la phase 10, le `strncpy` du prologue restait inexplicable.

**Un troisième descripteur d'interface.** `0x564fe0 + 0x8c8 = 0x5658a8 + 0x8c8 = 0x566170`. La
boucle de rendu de la page n'en parcourt que deux ; le troisième n'appartient qu'à ce sous-écran,
qui le lie et le dessine lui-même. Son en-tête est relevé dans `Legacy/Binaries/Blaze.exe`, et son `+4`
vaut `0x00640014` — exactement ce que la phase 9 y réécrit, ce qui confirme le relevé.

**`DAT_005de4e8` porte deux rôles.** Drapeau « liste déjà chargée » dans « Enchère »
(`FUN_005277b0`), curseur d'emplacement de carte mémoire ici. Les deux sous-écrans ne coexistent
jamais et `FUN_00524210` remet la plage à zéro à l'entrée du menu. Deux accesseurs nommés plutôt
qu'un nom faux sur l'un des deux sites.

L'arène d'état descend de `0x005de410` à **`0x005de100`** (0x420 octets) : cet écran atteint
`DAT_005de110`, `DAT_005de2d8`, `DAT_005de390` et `DAT_005de428`.

### Deux défauts du binaire, notés dans `legacy-bug.md`

* **LB-004** — les phases 9 (fiche détaillée) et 10 (clavier de renommage) n'ont **aucun
  prédécesseur**. Vérifié à l'octet sur les 46 instructions qui référencent `0x005de4d4` : aucun
  site n'y écrit 9 ni 10. La phase 8 sort vers l'écran racine sur CARRÉ comme sur CROIX. Toute la
  boucle de réécriture du fichier (phases 7, 0xb..0x12, 0x5e..0x62, négatives) est morte par
  ricochet, puisqu'elle exige `DAT_005de508`, que seul le renommage lève.
* **LB-005** — le refus de premier caractère du renommage compare un glyphe sur un octet à
  `0x4081`. Désassemblé à `0x005266A7` : `XOR EBX,EBX / MOV BL,[EAX]`, puis `CMP EBX,EDX` avec
  `EDX = [0x562c9c] & 0xffff`. Jamais vrai.

Tout est transcrit malgré tout — le projet reconstruit le binaire, pas seulement ses chemins
vivants — mais **rien de la phase 9 vers le bas n'est exerçable**, donc rien de tout cela n'a pu
être vérifié à l'écran. C'est le seul gros bloc de la journée dans ce cas, et il l'est pour une
raison établie, pas par manque de temps.

### Vérifié en jeu

Menu objets → « Invertaire » → la liste des sauvegardes (portrait, nom, classe, niveau, curseur) →
CARRÉ sur une ligne → le fichier est lu, les deux gardes de somme de contrôle passent, et la liste
d'objets s'affiche : « Epée normale / Armure de cuir / Bouclier de cuir / Petite potion », le
portrait et le nom du personnage, le compteur « 4/403 ». CARRÉ ou CROIX ramène à l'écran racine, et
« Enchère » n'a pas régressé. ~374 primitives par image sur la liste d'objets, ~416 sur la liste des
sauvegardes.

Le prérequis est qu'il existe au moins une sauvegarde dans le `save\` du répertoire de travail —
sans quoi la phase 0 rebondit aussitôt vers l'écran racine, exactement comme l'original.

### Une observation laissée ouverte

Sur la liste d'objets, un petit témoin d'attente subsiste en bas à droite de la boîte de message.
Il vient du couple `0x0c 0x0f` (« début de page », « attente ») que la phase 5 lie dans le premier
descripteur **dans l'image même où elle passe à la phase 8** : le joueur n'a jamais l'occasion de
le consommer. La séquence d'appels du port est identique à celle de l'original (`FUN_00544350`
avec `0x564a10`, puis `&DAT_005606f8` à l'entrée de la phase 8), et la boîte est propre sur
« Enchère », qui ne lie pas de page en attente. Ce n'est donc pas une erreur de transcription ;
savoir si le jeu de 1998 affiche le même témoin demanderait une capture de référence.

---

## 2026-08-18 — `FUN_00527e30`, les cas 1 et 2 (les deux qui restaient gardés)

Le sous-écran « Utilisation » est désormais complet. Avec eux, quatre fonctions de plus :
`DrawMemberItemGrid` (`FUN_00529070`), `DrawItemDetailCard` (`FUN_00529290`), et deux extractions,
`DrawItemStatRow` et `DrawItemElementRow`.

### Les deux blocages de la veille sont tombés, et aucun n'était ce qu'on croyait

**« FUN_005276c0, FUN_00529070 et FUN_00529290 non transcrites »** — la première l'a été en
transcrivant `FUN_00524880`. Les deux autres se sont révélées être, pour l'essentiel, du code déjà
écrit : `FUN_00529290` est **mot pour mot** la fiche d'objet de la phase 9 de `FUN_00524880`, plus
un bloc de nom au milieu. Les deux moitiés communes sont extraites en `DrawItemStatRow` et
`DrawItemElementRow`, et la phase 9 les appelle maintenant au lieu de les recopier.

**« Le cas 2 perd l'adressage, il faut une passe de désassemblage »** — non. `Ghidra` rend
`pbVar14[-0xffffffff00000008]` : c'est une constante **64 bits** dont les 32 bits bas valent
`0xFFFFFFF8`, soit l'indice `-8`. Le même champ est relu vingt lignes plus haut sous la forme
`*(short *)(pbVar14 + -8)`, ce qui lève l'ambiguïté sans ouvrir le binaire. Il suffisait de lire les
deux formes côte à côte. Même chose pour le pas de `0x4e` de la boucle de remise à zéro : `puVar5`
est un `undefined2 *`, donc 0x4e mots font bien les 0x9c octets attendus.

Ce qui **a** demandé le désassembleur, en revanche : les deux `&DAT_00604a20 + slot * 4` du cas 1 et
du cas 2. `0x005281D6` et `0x00528944` donnent tous deux `SHL EAX,0x4` puis `ADD EAX,0x604a20` — le
pas est de **0x10**, pas de 4. C'est la quatrième et la cinquième occurrence de cette perte de
décalage, après les trois déjà notées sur `CurrentPadState` ; il faut désormais la considérer comme
systématique sur cette table.

### Ce que les deux cas font

**Cas 1** — la grille d'objets du membre : cinq colonnes de 0x1c, une icône par emplacement, la
quantité sur les objets empilables, une pastille sur ceux qu'il porte, et la fiche détaillée en
dessous. Chaque membre répond à **sa propre manette**, pas à celle du joueur courant.

**Cas 2** — l'enchère. Chaque autre membre mise par pas de 100 avec sa manette, entre le prix de
départ (la valeur calculée de l'objet) et son or ; GAUCHE et DROITE **passent la manette** au membre
voisin qui n'en a pas ; CARRÉ confirme, CROIX revient sur sa décision. Le vendeur ne mise pas : sa
manette est cédée d'office à un emplacement libre. Quand tous ceux qui tiennent une manette ont
confirmé, la meilleure mise l'emporte — **une égalité ne départage pas**, le gagnant redevient le
vendeur, ce que la résolution traite comme un refus. Refus aussi si le gagnant porte déjà dix objets.

### Un défaut d'aliasing de plus, attrapé à l'écran

`DAT_005e9080` (la valeur de l'objet, que le bandeau affiche en « Au prix de: ») a d'abord été
déclaré en champ C# isolé. `0x5e9080 = 0x5e9060 + 0x20` : c'est un champ de l'arène déjà modélisée
par `s_PanelSelection_005e9060`. Écrit ailleurs qu'où le moteur de texte le lit, le bandeau affichait
« Au prix de: 0 » quelle que soit la valeur. Corrigé en `Arena(0x005e9080)` : « Au prix de: 24 ».
C'est exactement le défaut que ce projet a déjà rencontré une vingtaine de fois, et il ne s'est vu
qu'en regardant l'écran — le compte de primitives, lui, était identique.

### Vérifié en jeu

Menu objets → « Enchère » → deux personnages retenus → START → l'écran d'utilisation → CARRÉ :
**le cas 1** affiche la grille d'ARTHUR (épée, armure, bouclier avec leur pastille d'équipement,
potion avec sa quantité 5), la fiche « Epée normale / ATT 7 / DEF 0 », les huit témoins d'élément
avec leurs libellés de classe, le portrait, et « Sélectionne l'objet. / Au prix de: 24 ».

**Le cas 2** demande un équipement **non porté** : un personnage neuf n'en a pas. Vérifié dans le
fichier de sauvegarde — les trois pièces ont bien `0x8000` et un bit de `0xf00`, mais sont portées
(`+0x2b0..0x2b2 = 00 01 02`), et la potion a `0xf00 == 0`. Ce n'est pas un défaut de transcription
mais la règle du jeu. En déséquipant le bouclier sur une sauvegarde de test (et en recalculant les
deux sommes de contrôle), le cas 2 s'atteint : le lot glisse vers le gagnant courant, le vendeur est
grisé, l'enchérisseur en clair avec son marteau et ses deux compteurs, et « Fais une offre. ».
~403 primitives par image au cas 1, ~368 au cas 2.

**L'enchère est vérifiée jusqu'à l'adjudication**, avec un enchérisseur pourvu d'or (5000 posés à
`+0x26c` sur une sauvegarde de test) : le premier appui cale la mise sur la valeur calculée de
l'objet (46 pour le bouclier), les suivants ajoutent 100, l'écran affiche `246` / `4754` — dont la
somme fait exactement les 5000 de départ. CARRÉ adjuge, le cas 3 transfère, et l'écran revient au
choix du propriétaire avec `246` chez le vendeur, `4754` chez l'acheteur, et le meilleur objet passé
de l'un à l'autre (36 contre 46). Les trois arithmétiques — montée de la mise, or restant, transfert
— se recoupent, ce qui est une vérification bien plus forte qu'un simple « ça s'affiche ».

---

## 2026-08-18 — L'état de jeu 3 (la carte du monde) : entrée ouverte, nouveau point de blocage

Le menu objets étant terminé, le graphe d'appel reprend à l'**état de jeu 3**, atteint directement
par « Début du jeu » → constitution du groupe → lancement (`RunPartyConstitution` pose
`s_GameStateId = 3`), et aussi depuis l'état 2 qui y avance de lui-même.

### La taille du morceau, mesurée avant d'écrire

Un balayage transitif du sous-graphe depuis les quatre appelées de `FUN_00516020` :
**516 fonctions atteintes, 164 manquantes, ~17 400 lignes**. C'est tout le moteur de jeu. Il n'est
donc pas question de le prendre d'un bloc ; ce tour ouvre l'entrée et le socle, et situe précisément
le premier vrai obstacle.

Transcrit : `RunWorldMapState` (`FUN_00516020`, les deux boucles imbriquées),
`LoadWorldMapResources` (`FUN_005160e0`) et `LoadWorldMapArea` (`FUN_00516250`) — **aucune de leurs
dépendances ne manquait**. Laissées en exception nommée : `FUN_00516350` (455 lignes, la mise en
place d'image) et `FUN_00516fc0` (1226 lignes, la logique par image, celle qui pose `DAT_005645f8`
et décide de la sortie).

### Une note du fichier qui était fausse, et ce qu'elle cachait

`IsKnownOverrideDestination` (`FUN_0052a660`) portait : *« none of the three comparison targets
(DAT_006008c8/DAT_00604930/DAT_00600904) have an assignment site anywhere in the reconstructed call
graph yet »*. C'est faux. `RunApplication` — reconstruite de longue date — les assigne juste avant
le balayage des lecteurs (`Blaze.c:61733`) :

```c
DAT_006008c8 = 1;
DAT_00604930 = 2;
DAT_00600904 = 3;
```

**Ce ne sont pas des pointeurs, ce sont trois sentinelles entières.** `LoadResourceEntry` compare son
argument `dest` à ces valeurs, et une égalité veut dire « installe une table de sauts native » au
lieu de « écris les octets du fichier ici ». Tant qu'elles restaient nulles côté port, l'unique site
qui les utilisait (`LoadFirstLevelResources`, avec la sentinelle A) fonctionnait **par accident** :
`null == null`. Le comportement était le bon, la raison ne l'était pas — et le second site, celui de
l'état 3, aurait matché la mauvaise branche.

### Ce qu'est vraiment la branche 0x00604930

Lue à l'occasion : elle copie **0x20 dwords** depuis `(&PTR_PTR_00566d68)[zone]` vers
`&PTR_FUN_0056b4d0`. La table d'entrée compte **onze** zones, relevées à 0x00566d68 : 0x55e8a8,
0x55e8e8, 0x55e928 … 0x55eb28, puis un dword nul. `PTR_FUN_0056b4f0` — l'appel indirect que
`FUN_00516250` fait juste après — est **l'entrée 8 de la table de destination**, et vaut
`FUN_00401fa0`, un corps vide, avant toute installation.

**TODO(REVERSE)** : les onze tables sont espacées de **0x40** octets alors que la copie en transfère
**0x80**. Chaque zone déborde donc sur la suivante. Défaut d'origine ou tables volontairement à
moitié partagées — non tranché, il faut lire le contenu de 0x0055e8a8 avant de conclure.

L'installation n'est **pas** transcrite, délibérément : les 32 valeurs sont des adresses de code, et
les rendre appelables demande un mécanisme de table de sauts que rien de reconstruit n'exerce
aujourd'hui. L'inventer maintenant serait de l'échafaudage spéculatif — exactement ce que
`Strategy.md` proscrit. C'est la première fois que ce projet bute sur ce besoin ; il faudra le
concevoir explicitement, pas en passant.

### Vérifié en jeu

« Début du jeu » → un personnage retenu → START → « Veux-tu jouer avec ce groupe? » → Oui : l'état 3
démarre, `LoadWorldMapResources` s'exécute en entier, et l'arrêt tombe exactement où il est prévu —

```
NotImplementedException: FUN_0052a660, branche DAT_00604930 : l'installation de la table de sauts
de la zone courante ... Atteinte depuis FUN_00516250, l'etat de jeu 3
  at IsKnownOverrideDestination -> LoadResourceEntry -> LoadWorldMapArea -> RunWorldMapState
  -> RunGameStateTick -> RunGameLoop -> RunMainLoop -> RunApplication -> entry
```

Non-régression contrôlée sur le menu objets après la correction des sentinelles : ~374 primitives
par image, identique à avant.

### Le prochain pas

Deux chemins possibles, dans cet ordre de coût :

1. **Concevoir le mécanisme de table de sauts** et transcrire la branche 0x00604930. C'est le
   blocage dur, et il reviendra pour la branche 0x00600904 (trois tables, +11 fonctions).
2. Transcrire `FUN_00516350` (455 lignes) sans attendre : elle est en aval du blocage, donc
   invérifiable pour l'instant, mais ses dépendances sont peut-être déjà là — à mesurer avant de
   s'y mettre, comme on l'a fait ici.

---

## 2026-08-18 (suite) — La table de sauts de zone est levée ; le blocage est maintenant nommé

Le tour précédent s'arrêtait sur « il faudra concevoir un mécanisme de table de sauts, pas en
passant ». **Il n'y avait pas de mécanisme à concevoir** : il suffisait de lire les données.

### Ce que le relevé a donné

`0x0056b4d0` contient seize dwords à `FUN_00401FA0` puis seize à zéro : la table de destination fait
**seize entrées**, sans ambiguïté. Les onze tables sources sont espacées de 0x40, donc seize dwords
chacune. Et sur les 192 dwords du bloc complet (`0x0055e8a8`..`0x0055eba7`), **174 valent
`FUN_00401FA0`**, dont le corps est vide dans `Blaze.c`. Les dix-huit autres :

| zone | entrée | cible |
|---|---|---|
| 0 | 8 | `0x004747F0` |
| 1 | 8 | `0x0040A600` — **corps vide** lui aussi |
| 10 | 16..31 | `0x5037D0`…`0x503E80`, seize fonctions qui ne sont dans aucune table |

Autrement dit : les zones 1 à 9 n'installent **rien** d'autre que des no-ops, et le seul vrai
handler du jeu est celui de la zone 0. Le « mécanisme de vtable » se réduit à une table de seize
adresses, une copie, et un point d'appel qui reconnaît les deux corps vides.

Transcrit : `InstallAreaHandlerTable`, `CallAreaHandler`, `s_AreaHandlerSource_0055e8a8` (le bloc
plat de 192 dwords) et `s_AreaHandlerTable_0056b4d0`. Le débordement de la copie est reproduit tel
quel — voir `legacy-bug.md`, **LB-006**.

### Un effet de bord que le port ignorait, sur un chemin très chaud

En relisant `FUN_0052a660` pour cette branche : le nettoyage de **trente-neuf dwords** à
`0x005de558`, suivi du curseur `DAT_005de5c4` qui repointe sur leur début, **n'est pas dans une
branche**. Il court à chaque appel de `LoadResourceEntry` dont la destination n'est pas la
sentinelle A — c'est-à-dire à chaque chargement de ressource du jeu. Le port ne le faisait pas du
tout. Ajouté (`s_EntityList_005de558`). Une douzaine de lecteurs existent dans `Blaze.c`, tous dans
du code de jeu non encore transcrit, donc l'effet est pour l'instant inerte — mais c'est exactement
le genre d'omission qui se paie trois sous-systèmes plus tard.

Au passage, `ApplyResourceDestinationOverride` remplace `IsKnownOverrideDestination` : la fonction a
maintenant des effets de bord, et l'ancien nom mentait.

### Le nouveau blocage, et il est borné

`CallAreaHandler(8)` pour la zone 0 tombe sur `0x004747F0`, qui **n'est pas décompilée par Ghidra** :
il n'y a aucune fonction entre `FUN_004742b0` et `FUN_00474ad0` dans `Blaze.c`. Corps uniquement
adressé, jamais disassemblé comme fonction.

La zone vaut bien 0, et ce n'est pas un défaut de reconstruction : `RunPartyConstitution` la lit
dans la sauvegarde (`record[0x1f01]`, l'octet `record[0x1f00]` valant zéro), le port le transcrit
déjà, et un personnage neuf a bien `00` partout à `+0x1f00`.

**Mesuré plutôt que supposé**, en désassemblant les bornes :

* `FUN_004747F0` fait **242 octets**, du `SUB ESP,8` d'entrée au premier `RET` à `0x004748E2`.
* Elle n'appelle qu'une seule fonction, `0x00514000`, qui est décompilée (12 lignes) et se réduit à
  `FUN_00515d70(DAT_005e97cc, 0, a, b)` — un appel d'événement *dans le contexte de la zone*, ce que
  la présence de `DAT_005e97cc` (le répertoire de zone, ajouté ce tour) confirme.
* Le sous-arbre derrière `FUN_00515d70` : **4 fonctions atteintes, 2 manquantes, 567 lignes**
  (`FUN_00514040` et `FUN_00535710`).
* Son début est lisible : elle éteint le bit 8 de `DAT_005e9841`, monte un descripteur de quatre
  octets sur la pile (`00 01 04 EF`) et le passe à `0x00514000` si le bit 2 de `DAT_005e9001` est
  éteint, puis enchaîne sur `DAT_005e9006` et `DAT_005e9840`.

### Pourquoi ce tour s'arrête là

Transcrire les 242 octets à la main **plus** les 567 lignes du sous-arbre produirait ~600 lignes que
rien ne peut exercer : `FUN_00516350` (455 lignes) et `FUN_00516fc0` (1226 lignes) sont toujours des
exceptions, donc la carte du monde ne dessine rien de toute façon. C'est très exactement le mode
d'échec que ce fichier documente déjà deux fois (le pas de 0x9c, le type de retour perdu), et qu'il
s'était promis de ne plus reproduire.

Le prochain tour a un choix propre, avec les deux coûts connus :

1. **Le chemin de l'affichage** — `FUN_00516350` (455 lignes) puis `FUN_00516fc0` (1226). C'est ce
   qui fait apparaître quelque chose à l'écran, donc ce qui redevient vérifiable. Mesurer d'abord
   leurs dépendances manquantes, comme on l'a fait ici.
2. **Le chemin du handler** — les 242 octets à la main plus le sous-arbre d'événements de 567
   lignes. Bornée, mais aveugle tant que (1) n'existe pas.

Le (1) est le bon ordre : il rend le (2) vérifiable au lieu de l'inverse.

---

## 2026-08-18 (suite) — La branche sentinelle C, et une correction de mesure

### Fait : les sept tables de sauts d'entités

`FUN_0052a660`, branche `DAT_00600904`, est transcrite. Ce n'était pas « trois tables + 11 appels »
comme la note le disait, mais **sept** copies (0x20 + 0x40 × 6) suivies de onze initialiseurs.

Les sept tables de l'index 0 — le seul que la carte du monde atteigne, l'index valant
`zone * 0x10 + sous-zone` et étant forcé à 0 quand la zone est 0 — sont relevées dans
`Legacy/Binaries/Blaze.exe` et **générées mécaniquement**, pas recopiées à la main. Répartition :

| table | entrées | non vides |
|---|---|---|
| `0056ad48` | 32 | 2 |
| `0056adc8` | 64 | 64 (19 distinctes puis `0x004734B0` répété — un défaut par défaut) |
| `0056aec8` | 64 | 18 |
| `0056afc8` | 64 | 2 (entrées 8 et 10) |
| `0056b0c8` | 64 | 8 |
| `0056b1c8` | 64 | 0 |
| `0056b2c8` | 64 | 1 |

Les onze initialiseurs sont tous des feuilles (266 lignes en tout) qui remettent à neuf deux plages
de paramètres par classe d'entité. Modélisées en deux arènes, `s_EntityParams_00551640` et
`s_EntityState_00571500` — une allocation chacune, pas cent champs séparés.

L'appel indirect que `FUN_00516350` fait juste après (`PTR_FUN_0056afc8`) vise l'entrée **0** de la
table `0056afc8`, qui vaut `FUN_00401FA0` : un no-op vérifié, pas une supposition.

### Correction : `FUN_004747F0` fait ~723 octets, pas 242

La note précédente donnait 242 octets, « du `SUB ESP,8` d'entrée au premier `RET` ». **C'est faux** :
ce `RET` est l'une des sorties, pas la fin. Neuf sauts `Jcc rel32` répartis sur
`0x00474872`..`0x00474A33` convergent tous vers un épilogue commun en `0x00474ABE`, et la dernière
sortie est en `0x00474AC3`. La fonction couvre donc **`0x004747F0`..`0x00474AC3`, soit ~723 octets**,
et les « petites fonctions » que j'avais cru voir après le premier `RET` sont ses blocs de queue.

Cela change la recommandation. 242 octets linéaires se décodent à la main ; 723 octets avec neuf
branches convergentes et une logique de drapeaux dense, non — c'est exactement le travail d'un
décompilateur. **C'est une demande de re-passe Ghidra ciblée sur `0x004747F0`**, pas un exercice de
lecture.

Ce que le début révèle quand même : la fonction éteint des bits dans `DAT_005E9840/41`,
`DAT_005E9000/01/06`, compte les bits levés de `DAT_005E9000` dans ESI, et compare
`DAT_005DE52C`/`DAT_005DE530` (sous-zone et sa voisine) à zéro — c'est une **machine de transition
de sous-zone**, pas un rendu.

### `FUN_00516350` n'est pas livrée, et pourquoi

Elle est en aval de `FUN_004747F0` dans la boucle (`FUN_00516250` d'abord, elle ensuite), donc
inexerçable aujourd'hui. Mesuré avant de renoncer : six aides directes manquantes — `FUN_00532770`
(27), `FUN_005352c0` (15), `FUN_005355f0` (56), `FUN_005330d0` (113), `FUN_00533940` (375),
`FUN_00535050` (33) — soit **619 lignes**, plus les 455 de la fonction, plus une trentaine de
symboles d'arène nouveaux (`0x5f2xxx`, `0x5f7xxx`, `0x5faxxx`, `0x600xxx`, `0x5ecxxx`, `0x5edxxx`,
`0x5ddxxx`). `FUN_005355f0` a en prime un défaut Ghidra propre : sa borne de boucle est rendue
`0x180006` là où le compte d'entrées est attendu, et son second rectangle d'envoi VRAM est un
`&stack0xfffffff0` non résolu.

L'écrire à moitié aurait été pire que ne pas l'écrire. Deux chemins propres pour la suite :

1. **Re-passe Ghidra sur `0x004747F0`** — lève le verrou pour de bon et rend `FUN_00516350`
   vérifiable dès qu'elle est écrite. C'est le bon ordre.
2. **Un échafaudage explicitement étiqueté** qui fait de `CallAreaHandler` un no-op tracé, le temps
   d'écrire et d'exercer `FUN_00516350`. Défendable — le projet a le précédent de
   `OPENFORESIA_FORCE_SUBSTATE` — mais c'est une déviation de comportement, à décider, pas à
   prendre en passant.

---

## 2026-08-18 (suite) — L'échafaudage, et la première moitié de `FUN_00516350`

### L'échafaudage `OPENFORESIA_SKIP_AREA_HANDLER`

`CallAreaHandler` accepte `OPENFORESIA_SKIP_AREA_HANDLER=1`, qui fait de l'appel un **no-op tracé**
au lieu de lever. C'est **une déviation de comportement**, pas une omission forcée par la
plateforme comme les quatre documentées plus haut : elle est opt-in, le défaut reste fidèle, elle
écrit sur stderr à chaque fois, et elle est à retirer dès que `0x004747F0` est décompilée.

Ce qu'elle coûte, dit franchement : `FUN_004747F0` est une machine de transition de sous-zone (elle
éteint des bits dans `DAT_005E9840/41` et `DAT_005E9000/01/06`, compte les bits levés de
`DAT_005E9000`, compare `DAT_005DE52C`/`DAT_005DE530` à zéro). La sauter laisse ces drapeaux tels
que le chargement les a posés. **Tout ce qu'on observe sous ce drapeau est à re-vérifier** une fois
la re-passe Ghidra faite.

### Ce qui tourne maintenant

`FUN_00516350` est écrite jusqu'au bout de son chargement, et **tout s'exécute sur des données
réelles** : les six listes d'index identité et les six effacements d'entités, le rechargement de
sous-zone, l'installation des sept tables de sauts (la sentinelle C, écrite au tour précédent —
elle est donc maintenant exercée, pas seulement compilée), les onze initialiseurs, les textures, le
**banc de sons** de la zone, les quatre listes de morceaux, les modèles, les animations, et l'envoi
des blocs bruts.

Nouvelles arènes, chacune d'une seule pièce : `s_EntityArena_005eca00` (0x14200 octets —
0x5eca00..0x600c00 est **une région continue**, ce que prouvent les six `FUN_005352c0` dont chaque
bloc finit exactement où le suivant commence), `s_CameraBlock_005de030`, `s_ViewBlock_006067c0`,
`s_ViewRect_00566c70`, plus `GameArenaSize` étendue à 0x1600 et l'arène du menu objets à 0x460.

### `FUN_005355f0` : les deux défauts de la pseudo-C étaient des artefacts

La note du tour précédent la donnait comme « désassemblage requis ». Fait, et les deux points
tombent :

* La borne de boucle rendue `0x180006` est un artefact. À `0x00535619` c'est `TEST EAX,EAX / JLE`
  sur le compte lu en tête du bloc — **la borne est le compte**.
* Le second rectangle, `&stack0xfffffff0`, n'est pas une seconde adresse : c'est **le même
  emplacement de pile que le premier**, réécrit entre les deux appels. Les deux `PUSH` intercalés
  décalent les `[ESP+n]` de huit octets, d'où l'illusion. Ses champs sont montés à
  `0x00535671`..`0x005356B5` : `{ (i%10)*6 + 0x180, (i/10)*0x18 + 0x160, 6, 0x18 }`.

Troisième fois ce mois-ci qu'un « il faut une re-passe Ghidra » se révèle être un « il faut lire
vingt octets de désassemblage ». La distinction utile reste la taille : ces deux-là faisaient
quelques instructions, `FUN_004747F0` en fait 723 avec neuf sorties convergentes.

### Le reste

La seconde moitié de `FUN_00516350` — le placement du groupe et la dérivation de l'état de vue,
~200 lignes — lève une exception nommée. Elle dépend de `FUN_005330d0` (113 lignes) et
`FUN_00533940` (375), toutes deux sur le chemin chaud : **688 lignes** pour finir la fonction.

Un détail à ne pas oublier au passage : `FUN_00547960` n'est **pas** le `FormatDecimal` à deux
arguments déjà transcrit, c'est un `sprintf` variadique dont le moteur est `FUN_00549380` (424
lignes, non transcrit). Le seul format utilisé ici, `"q:\snd\Q%02d_%02d.snd"` (relevé à
0x005649f4), est posé directement plutôt que de tirer tout le moteur pour deux entiers — à
remplacer le jour où un autre format apparaît.

---

## 2026-08-18 (suite) — La seconde moitié de `FUN_00516350`

**Le corps de la fonction est transcrit en entier.** Avec elle, `FUN_005330d0` (113 lignes), qui
était l'une des deux grosses dépendances.

### `FUN_005330d0` — « sur quelle face du terrain tombe cette position ? »

Elle parcourt la liste de faces du morceau et teste le point (x, z) de l'entité par trois produits
en croix dans le plan horizontal, un quatrième pour les faces à quatre sommets. Une face retenue
voit sa normale calculée dans le banc GTE, puis un produit scalaire décide du côté du plan. Le
numéro de face et ses attributs partent dans l'entité à `+0x80 + type * 4` ; le compteur rendu ne
compte que les faces dont les attributs ne rencontrent pas le masque — c'est lui que l'appelant
boucle jusqu'à obtenir non nul.

Disposition déduite des accès : **un sommet fait huit octets** (`x` et `y` dans le dword de tête,
bas et haut ; `z` dans le short de `+4`), **une face en fait seize** (trois index de sommet répartis
sur les deux premiers dwords, un index de matière au troisième, les attributs au quatrième).

**Ordinaux 799 et 800 identifiés.** La table du fichier les donnait comme non établis. Cette
fonction y écrit deux vecteurs 3D de 32 bits — les deux arêtes d'un triangle — puis dépose leur
produit vectoriel dans 801 (`MAC1..3`) champ par champ : ce sont les **deux vecteurs d'entrée du
produit vectoriel**, pas des registres de projection. Ajoutés à `EnsureGteRegisters`.

### La seconde moitié de `FUN_00516350`

Le placement du groupe au point d'entrée de la sous-zone (position imposée, ou tirage aléatoire
autour du point jusqu'à trouver une face praticable), la recopie du membre actif sur les quatre
emplacements, et la dérivation de tout l'état de vue depuis la fiche de sous-zone.

**Le piège de ce bloc, et il est sérieux** : un emplacement de personnage fait **0x9c octets** à
partir de 0x005e93c0, et Ghidra l'adresse de **trois** façons selon le type du symbole qu'il a
choisi — `[n * 0x27]` sur un tableau de dwords, `[n * 0x4e]` sur un tableau de shorts, et
`+ n * 0x9c` en octets. Les trois désignent le même pas. Tout passe donc ici par un unique pointeur
`slot` et des décalages explicites, plutôt que par la forme de l'original. C'est la même famille
d'erreur que le 0x9c/0x4c qui avait coûté une journée sur le menu objets.

Nouvelles arènes : `s_EntityArena_005eca00`, `s_CameraBlock_005de030`, `s_ViewBlock_006067c0`,
`s_ViewRect_00566c70`, `s_WorldPanels_005dbc68` (quatre descripteurs d'interface, identifiés par le
pas de boucle de 0x464 mots = 0x8c8 octets).

### Vérifié en jeu

L'état 3 traverse maintenant **tout** `FUN_00516350` jusqu'à `FUN_00533940`. En particulier la
boucle `do { position aléatoire ; TryPlaceEntity } while (rien)` **en sort** : la transcription
géométrique trouve bien une face praticable sur les données réelles du terrain. Une erreur de pas
ou de signe l'aurait fait tourner indéfiniment — c'est une vérification par le comportement, pas
seulement par la compilation.

(Sous `OPENFORESIA_SKIP_AREA_HANDLER=1` — tout ceci reste à re-vérifier une fois `0x004747F0`
décompilée.)

### Ce qui reste pour finir l'écran

* `FUN_00533940` (375 lignes) + `FUN_00533450` (52) — la finalisation de placement. **Cinq labels
  entrelacés** (`LAB_005339a8`, `LAB_005339b6`, `LAB_00533af7`, `LAB_00533c2a`, `LAB_00534018`)
  sautés depuis plusieurs endroits, avec les registres GTE en scratch : c'est une reconstruction de
  flot, pas une translittération, et ça se fait à tête reposée.
* `FUN_005350b0` (95 lignes) — la mise en place de palette de zone.
* Puis `FUN_00516fc0` (1226 lignes), la logique par image, qui reste le gros morceau de l'état 3.

---

## 2026-08-19 — `Blaze.cs` est découpé, et le découpage a trouvé quatre choses

`openspec/changes/split-blaze-into-subsystems`. Le fichier unique de **23 680 lignes** est réparti
en 30 fichiers sous `OpenForesia/src/Blaze/` (`Platform/`, `Memory/`, `Engine/`, `Screens/`), plus
5 fichiers d'échafaudage sortis dans `OpenForesia/src/Host/`. Tout `src/Blaze/` reste **une seule
classe partielle**. `Documentation/ARCHITECTURE.md` fait autorité sur la carte et sur la règle qui
décide où va une fonction.

**Prouvé sans perte** : les 28 corps réassemblés dans l'ordre du manifeste sont identiques à la
référence figée sur **23 663 lignes**, la seule différence étant l'accolade fermante de la classe,
repositionnée en postambule. Chaque fichier est en outre vérifié octet à octet
(`OpenForesia.Tools/verify-split.ps1`). Build propre à chaque étape ; 382,3 et 387 primitives par
image, captures identiques aux références d'avant découpage.

### 1. Trois bandeaux sur 37 mentaient

Les bandeaux `// ====` servaient de sommaire. Le taux d'erreur mesuré est de **8 %** :

| Bandeau | Annonçait | Contenait vraiment |
|---|---|---|
| « Not yet reconstructed » | des sous-systèmes à faire | **l'extinction**, reconstruite depuis longtemps → `Platform/Startup.cs` |
| « Les huit sous-états » | les sous-états de l'état 4 | quatre choses sans rapport : tables `.data`, chargement de ressources, **l'état 0**, et les **émetteurs de primitives 2D** |
| « Sous-couche carte mémoire » | l'E/S carte mémoire | l'E/S (595 l.) **plus les sous-états 0, 3, 4 et 7 de l'état 4** (2 082 l.) |

Deux rangements en découlent : `Screens/GameStates.cs` extrait le répartiteur `RunGameStateTick`
(`FUN_00537ca0`) de la queue de la scrutation d'entrées où il était classé, et
`Engine/Primitives2d.cs` naît — `DrawFlatPrimitive`, `DrawSpritePrimitive`, `DrawSpriteVariant`,
`DrawTextRun` sont appelés par tous les écrans et étaient enfouis sous un bandeau qui annonçait
autre chose.

### 2. Le découpage en fichiers n'est pas sémantiquement neutre

Les initialiseurs de champs statiques d'une classe partielle s'exécutent dans l'ordre où le
compilateur reçoit les **fichiers**, ordre non spécifié. Tant que tout vivait dans `Blaze.cs`,
l'ordre textuel garantissait que l'arène `0x005e9000` (L5373) était allouée avant
`s_SlotArea_005e9180 = Arena(0x005e9180)` (L16663). Une fois `Arena()` parti dans
`Memory/Arena.GameState.cs`, `Blaze.cs` s'initialise d'abord : `Arena()` rendait `null + décalage`,
et `CurrentPadState()` levait une `NullReferenceException` dès la première image du menu.

**Huit champs** de la forme `static readonly … = Arena(0x…)` sont devenus des propriétés
`=> Arena(0x…)`. La base d'arène est allouée une fois et ne change jamais, donc réévaluer
`base + décalage` rend toujours le même pointeur — et le nombre de lignes ne bouge pas.

C'est le même mécanisme que celui déjà écrit sur `s_MenuContext_0056dd58` et sur les descripteurs
d'interface : **un initialiseur de champ transforme un problème rattrapable en problème fatal**.

> **Règle** : un champ `static readonly` dont l'initialiseur lit un membre statique vivant dans un
> **autre** fichier doit devenir une propriété calculée. Les dépendances intra-fichier sont sûres.

Les 41 champs initialisés par un appel de méthode ont été croisés avec le manifeste : **chaque
méthode atterrit dans le même fichier que son champ**, donc aucun autre cas. `ToAnsiZ`,
`ToAnsiBlock` et `AllocBytes`, appelés depuis 26 initialiseurs répartis partout, ne lisent aucun
statique.

### 3. La frontière avec `Host/` est bidirectionnelle, et c'est inhérent

Le compilateur a donné la vraie surface après extraction : **25 symboles**, pas les 4 estimés.

* **Host → Blaze, 5** : `ToAnsiZ`, `RenderSizeFromSizeSetting`, `s_WindowedStartup`,
  `GetModuleHandleA`, `VirtualAlloc` — passés `internal`, importés par `using static`.
* **Blaze → Host, 4** : `InstallSyntheticDevice`, `InstallDeviceShim`, `CreateGameWindow`,
  `LogDisplayMode`. **Une façade s'installe nécessairement depuis le chemin qu'elle détourne.**
  Qualifiés explicitement au site d'appel pour rester visibles.
* **Host ↔ Host, 16** : les thunks `Shim_*`, `Guarded`, `DeviceVtableSlots`, `s_SyntheticDevice`.

La propriété à viser n'était donc pas « sens unique » mais **« couture étroite »** : les 4 appels
de `Blaze` vers `Host` sont tous des points d'installation.

`s_WindowedStartup` **n'a pas migré** vers `Host/` comme prévu : huit de ses dix sites de lecture
sont dans la transcription (coopération DirectDraw, les quatre sites `VfwClass`). Le déplacer aurait
inversé la dépendance.

### 4. Un défaut d'aliasing dormant, et quatre allocations dans une arène

L'index des globales trié par adresse — désormais en fin de `Documentation/Reference/DataSymbols.md` — a été
construit pour rendre le contrôle d'adjacence encore possible après le découpage : **107 paires de
symboles voisins (écart ≤ 64 octets) vivent maintenant dans deux fichiers différents**, sur
398 déclarateurs répartis sur 375 adresses.

Il a trouvé **un vrai défaut, non corrigé** :

```
L6929   s_MenuRenderBlock_00566c70 = (uint*)NativeMemory.AllocZeroed(0x40);   // 64 octets
L17622  s_ViewRect_00566c70        = (byte*)NativeMemory.AllocZeroed(0x10);   // 16 octets
```

**Deux allocations indépendantes pour une seule adresse du binaire.** Le menu principal écrit dans
l'une, la carte du monde lit dans l'autre. En prime, `ViewRect` est entièrement contenu dans
l'étendue de `MenuRenderBlock` : au moins une des deux tailles est fausse en plus du partage
manquant. **Dormant** parce que la carte du monde n'affiche rien encore — il mordra exactement quand
la priorité 2 reprendra. À traiter **avant** `FUN_00533940`.

Deux autres adresses portent deux déclarations, sans conséquence : `0x005e9180`
(`s_EntitySlotTable` / `s_SlotArea`, tous deux `Arena(…)`, donc la mémoire est bien partagée) et
`0x00562c9c` (deux constantes de même valeur, aucune mémoire en jeu).

**Quatre allocations indépendantes tombent dans l'étendue d'une arène**, constatées et non
corrigées :

| Symbole | Adresse | Arène qui la couvre |
|---|---|---|
| `s_EntityList_005de558` | `0x005de558` | `ItemMenuArena` |
| `s_ModelResultB_005f7b40` | `0x005f7b40` | `EntityArena` |
| `s_ModelResultA_005fa588` | `0x005fa588` | `EntityArena` |
| `s_CameraMatrix_006008e0` | `0x006008e0` | `EntityArena` |

Les trois dernières sont dans le voisinage que `FUN_00533940` doit lire (`DAT_005f7bc0`,
`DAT_005f7cc0`, `DAT_005fa508`). *(Note : `s_CameraMatrix_006008e0` est signalé plus haut dans ce
fichier comme n'ayant pas besoin d'être bas, parce qu'il est copié par valeur dans le registre
matrice du GTE. Les deux constats ne se contredisent pas : l'un porte sur la fenêtre basse, l'autre
sur le recouvrement d'arène.)*

### Ce que le découpage n'a pas fait

* **La réparation des 871 lignes à encodage double** — commentaires et journaux uniquement, aucun
  texte visible en jeu. Explicitement hors périmètre : mêler une réparation de contenu à un
  déplacement rend les deux invérifiables. Changement séparé.
* **La correction du défaut `0x00566c70`** et des quatre allocations en arène — les corriger change
  le comportement, ce que ce changement s'interdisait.
* **Aucune reconstruction.** `FUN_00533940` et `FUN_00516fc0` sont dans
  `Screens/State03/WorldMap.cs`, exactement dans l'état où ils étaient.

### Outillage laissé derrière

`OpenForesia.Tools/` : `split-manifest.awk` (construction du manifeste, attribution ligne par
ligne), `split-manifest.tsv` (56 plages), `blaze-baseline.txt` (la référence figée),
`apply-split.ps1` (écrit les fichiers en **redérivant depuis la référence**, jamais d'édition en
place), `verify-split.ps1` (comparaison octet à octet). Tout lit et écrit en ISO-8859-1 via
`ReadAllBytes`/`WriteAllBytes` — `Get-Content` casse sur la région à encodage double, mesuré :
19 221 lignes rendues au lieu de 21 858.

**La référence figée n'existe qu'en dehors de git tant que ce changement n'est pas commité.** Elle a
été détruite une fois pendant le travail, par une seule commande PowerShell maladroite, et n'a pu
être restaurée que grâce à une copie de circonstance.

---

## 2026-08-19 (suite) — `GameMemory`, et les tests de disposition trouvent deux défauts inconnus

`openspec/changes/extract-real-classes-from-partial`, étapes A et A′.

### `GameMemory` : le modèle de `.data` devient un objet

Les quatre fichiers de `Memory/` fusionnent en `Memory/GameMemory.cs`, **97 membres** (79 d'instance
+ 18 `const`). `Arena.GameState.cs`, `Arenas.cs` et `DataTables.cs` sont supprimés. Le reste de
`src/Blaze/` y accède par `Mem.X` (membres d'instance) et `GameMemory.X` (constantes) — **675 lignes
qualifiées, 21 fichiers**.

**Un seul objet, parce que le binaire a un seul segment `.data`.** Ce n'est pas un pis-aller : les
~870 références croisées mesurées au découpage montrent qu'aucune frontière par sous-système ne
tient. Ce que la classe apporte, c'est une **signature** — un sous-système qui prendra `GameMemory`
en paramètre déclarera qu'il touche à `.data` ; jusqu'ici il le faisait en silence.

Le pont statique temporaire prévu par la conception **n'a pas été écrit** : l'oracle de comparaison
de corps normalise les qualifications, donc écrire `Mem.X` au site d'appel lui est invisible, alors
qu'un pont aurait changé 94 corps d'accesseurs. Il ne reste qu'un champ statique, `Blaze.Mem`, avec
sa condition de retrait écrite (déplacé dans `Subsystems.cs` le 2026-08-19, voir plus bas).

**Trois choses que le compilateur a imposées**, toutes consignées dans `GameMemory.cs` :

* les quatre vues `s_DrawEnv*`/`s_DispEnv*` deviennent des propriétés `=>` — un initialiseur de champ
  ne peut pas lire un membre d'instance ;
* les trois tables construites par appel montent dans le **constructeur** — en faire des propriétés
  `=>` les reconstruirait à chaque accès, ce qui serait un changement de comportement ;
* `s_PanelSelection_005e9060` est rapatrié depuis `Engine/UiWindow.cs` : `0x005e9060` tombe dans
  l'étendue de `GameArena`, c'est donc une **vue** et non un stockage propre.

**`RunMainLoop()` et `LoadUiFontFromCd()` ont quitté `Memory/`** pour `Platform/Startup.cs` : elles y
avaient atterri par un point de coupe trop large au découpage. Le VA `0x00529c10` aurait envoyé
`RunMainLoop` dans `Engine/Resources.cs` selon la règle de routage (1) — la règle a tort ici, la
proximité d'adresse est un artefact de l'éditeur de liens sur le code de démarrage.

**Vérifié** : build 0 erreur ; l'oracle rend « aucune logique modifiée » (509 → 510 corps, le nouveau
étant le constructeur) ; 382,3 et 387 primitives par image, captures identiques.

### Les premiers tests du dépôt, et ce qu'ils ont trouvé

`OpenForesia.Tests`, cinq assertions de **disposition mémoire**. Elles ne testent aucun comportement :
l'oracle de la logique transcrite reste l'écran. Elles lisent les **sources** plutôt que l'exécution,
pour deux raisons : le défaut d'aliasing est un défaut de *déclaration*, entièrement visible dans le
texte ; et toucher aux statiques de `Blaze` déclencherait `AllocLowZeroed`, qui dispute une fenêtre
mémoire basse et échoue par intermittence.

**Deux échecs étaient attendus, et tombent exactement sur les symboles prédits au découpage :**

* aucune allocation propre dans l'étendue d'une arène → **4 symboles** : `s_EntityList_005de558`
  (dans `ItemMenuArena`), `s_ModelResultB_005f7b40`, `s_ModelResultA_005fa588`,
  `s_CameraMatrix_006008e0` (dans `EntityArena`) ;
* deux symboles à la même adresse partagent leur mémoire → **1 adresse**, `0x00566c70`.

**Un troisième échec n'était pas attendu, et c'est la vraie prise.** L'assertion « la taille déclarée
ne déborde pas sur le voisin d'adresse » relève **deux recouvrements que personne n'avait vus** :

| Symbole | Déclare | Écart réel au voisin | Recouvrement |
|---|---|---|---|
| `s_MenuScreenBlock_00566c48` (`MainMenu.cs:269`) | `0x40` | `0x28` vers `s_MenuRenderBlock_00566c70` | **0x18** |
| `s_CardPathDescriptor_00604940` (`SaveData.cs:88`) | `0x100` | `0x4c` vers `s_CardIconFrameCount_0060498c` | **0xb4** |

Le premier est deux lignes voisines du même fichier, la même taille `0x40` recopiée sans vérifier
l'écart. Le second est plus sérieux : le descripteur de chemin **avale entièrement** le compteur
d'images d'icône, et les deux vivent dans des fichiers différents (`Engine/SaveData.cs` et
`Screens/State04/SaveSlots.cs`) — la forme exacte du défaut corrigé seize fois.

**Aucun des trois n'est corrigé** : les corriger change le comportement, ce qui relève d'un
changement séparé. Ils sont documentés comme échecs attendus dans le projet de tests.

Ce qu'il faut en retenir : ces quatre assertions ont trouvé **deux défauts inconnus en une seconde,
sans lancer le jeu**, alors que les seize précédents avaient tous été vus à l'écran, après coup. La
carte des symboles reconstruits de `Documentation/Reference/DataSymbols.md` et ces tests forment ensemble le
contrôle que le projet n'avait pas.

### Trois pièges PowerShell, payés et documentés

Ils sont écrits en tête des scripts qui les ont rencontrés, chacun avec le faux positif qui l'a
révélé :

* **`-replace` est insensible à la casse.** Le nom de méthode `View` mordait sur la variable locale
  `view` de `Host/D3D11Backend.cs`. Sans l'essai à blanc, un fichier de `Host/` était corrompu.
* **La virgule lie plus fort que `+`.** `@("a" + $x + "b", "c")` devient `"a" + $x + ("b","c")`, soit
  `string + tableau` = **tableau**. Cent-quatre règles de remplacement se sont retrouvées imbriquées :
  zéro correspondance, zéro message d'erreur.
* **`^\s*\*` saute les déréférencements.** Un garde anti-commentaire ignorait les 922 lignes
  commençant par `*(uint*)…`, laissant 194 erreurs dans `WorldMap.cs` à la première passe.

---

## 2026-08-19 (fin) — l'interop sort de la classe, et trois tâches s'annulent d'elles-mêmes

Fin du socle de `extract-real-classes-from-partial` : étapes B et E.

### B — les 160 P/Invoke quittent `partial class Blaze`

`src/Blaze/Interop/` accueille `Winsys3d.cs` (85 déclarations), `Win32.cs` (66, huit DLL système)
et `Wsyskern.cs` (9). **381 lignes qualifiées** dans 22 fichiers. Les commentaires d'ordinal partent
avec leur déclaration : ils sont la trace vers `winsys3d.c`, au même titre qu'un `// Ghidra:` l'est
vers `Blaze.c`.

Au site d'appel, `Winsys3d.Ordinal_688(…)` dit désormais ce que `Ordinal_688(…)` taisait.

Relevé au passage : **`Platform/Win32.cs` contenait 42 P/Invoke `winsys3d.dll`**. Le fichier était
mal nommé pour son contenu depuis le découpage — l'inventaire ne l'avait pas vu parce qu'il classait
par bandeau, pas par DLL. `Win32.cs` ne fait plus que 72 lignes, ses structures d'interop.

### Aucune interface n'était justifiée, `IWinsys3d` comprise

La tâche qui devait *vérifier* que `SyntheticDevice` est une seconde implémentation d'`IWinsys3d` a
été faite **en premier**, et elle a annulé les deux tâches qui construisaient dessus.

`SyntheticDevice` détourne `GetProcAddress` **dans la table d'imports de winsys3d**, pour remplacer
`3dhelp.dll::Create3DDevice`. Il substitue le **device 3D de 3dhelp**, jamais winsys3d — qui reste
toujours la vraie DLL, appelée par 173 `Ordinal_NNN(...)` directs.

La couture substituable est donc **entre winsys3d et 3dhelp**, et elle est déjà traitée par
`SyntheticDevice` et `DeviceShim`, deux vraies classes de `src/Host/`. Le critère du projet est
« une interface là où un double existe déjà » : **l'appliquer correctement consistait ici à ne pas
créer l'interface.**

### E — les trois tâches ont été mesurées, aucune n'a été écrite

**Le pont statique pour les rappels natifs n'est pas nécessaire.** `CountJoystickCallback` utilise
son paramètre `context` et ne touche aucun statique ; les huit gestionnaires de carte écrivent deux
`private static` de `Engine/SaveData.cs` qui restent statiques. La question ne se posera qu'à
l'étape C.

**Le composition root a été écrit, a fait planter le jeu, et a été reverté.** La réclamation de
l'arène de liste d'affichage, dans le module initializer, touche **délibérément**
`Mem.s_UiDescriptors_00560ff0` et `Mem.s_StatusDescriptor_00560728` : ces descripteurs portent leur
ordering table à `+0xa4`, masquée sur 24 bits, donc ils doivent être pris sous `0x01000000` au même
instant précoce que l'arène. `GameMemory` doit donc naître **pendant** l'initialisation statique de
`Blaze`.

*La justification qui m'avait poussé à le déplacer était fausse* : j'avais avancé que les 16
allocations de `GameMemory` précédant la réclamation étaient une régression du refactor. Non — avant
lui, ces mêmes allocations étaient déjà des initialiseurs de champ de `Blaze` et précédaient la
réclamation exactement de la même façon. `Program.cs` porte maintenant la contrainte en commentaire.

**`IDisposable` sur `src/Host/` n'est ni implémentable, ni nécessaire.** Les cinq classes y sont
`internal static`, et une classe statique ne peut pas implémenter une interface. La libération
existe déjà sous une forme mieux adaptée : `StartupSurface.Retire()` et
`D3D11Window.ReleaseLegacyDisplay()` sont appelées **en cours de partie**, quand le chemin Direct3D
11 prend le relais — un événement de cycle de vie que `Dispose()` ne modélise pas.
`D3D11Backend.Shutdown()` n'est appelé que par l'autotest, jamais à la sortie : c'est correct, le
système reclame GPU, fenêtre et DIB section à la fin du processus.

### Ce que le socle laisse

```
  src/Blaze/Interop/      3 fichiers    539 l.   160 P/Invoke, hors de la classe
  src/Blaze/Memory/       2 fichiers    694 l.   GameMemory + le point d'acces Mem
  OpenForesia.Tests/      5 assertions           de disposition, 3 rouges attendus
  OpenForesia.Tools/compare-bodies.ps1           l'oracle « forme vs logique »
```

Vérifié à chaque étape : build 0 erreur, oracle « aucune logique modifiée », **382,3 et 387**
primitives par image, captures identiques aux références d'avant découpage.

### La leçon de méthode de ce changement

**Trois fois** une tâche planifiée de bonne foi s'est révélée inutile ou nuisible une fois mesurée :
le pont statique que l'outillage rendait superflu, les interfaces sans second implémenteur, le
`IDisposable` sur des classes qui ne peuvent pas en porter. À chaque fois l'hypothèse était
plausible, et à chaque fois le code a répondu autre chose.

Le plan a autant servi à **empêcher du travail** qu'à en produire. Les tâches formulées « mesurer
d'abord » ont toutes payé — c'est une forme à reprendre.

Écrit dans `Strategy.md` et `Prompt.md` pour que la leçon serve ailleurs.

### Ce qui reste ouvert

* **C** — `Engine/` en 9 classes d'instance (~7 700 l.), fichier par fichier, oracle à chaque
  fichier. Ordre suggéré du plus fermé au moins fermé.
* **D** — `Screens/` en 12 classes (~13 800 l.), **en dernier**, quand les écrans cesseront de
  bouger. `Screens/State03/WorldMap.cs` est le front de la priorité 2 : on n'y touche pas tant que
  `FUN_00533940` n'est pas terminée.
* Les **cinq défauts d'aliasing** que les tests signalent, dont deux inconnus jusqu'ici. Leur
  correction change le comportement, donc elle relève d'un changement séparé — et
  `0x00566c70` doit être traité **avant** la reprise de la carte du monde.
* La réparation des **868 lignes à encodage double** (`repair-double-encoded-comments`, ouvert et
  non commencé).

---

## 2026-08-19 (étape C, 3 fichiers sur 9) — trois vraies classes, et un piège d'ordre refermé

`UiText`, `Primitives2d` et `SaveData` ne sont plus des parts de `partial class Blaze`. Ce sont des
classes d'instance, construites une fois dans `src/Blaze/Subsystems.cs`.

```
  UiText(GameMemory)                    1 344 l.   55 membres, 0 static
  Primitives2d(GameMemory, UiText)        284 l.    5 membres, 0 static
  SaveData(GameMemory)                    623 l.   33 membres, 8 static — voulus
```

### Ce que la conversion a rendu visible

`Primitives2d` **dépend du moteur de texte** : `DrawTextRun` fait passer chaque octet par
`CheatCodeProbe`. Ce lien existait dans le binaire ; il était invisible tant que tout vivait dans
une seule classe. Il est maintenant dans la signature, et l'ordre de construction de
`Subsystems.cs` le reflète — `Ui` avant `Prims`.

### Une classe mixte, et pourquoi ce n'est pas un compromis

`SaveData` garde **huit membres `static`** : les gestionnaires `Card{Write,Read}StatusHandler0..3`,
porteurs de `[UnmanagedCallersOnly]`. L'attribut **exige** une méthode statique — la DLL les appelle
par simple adresse de code, sans `this` à leur passer. Les rendre d'instance est impossible, pas
inélégant.

C'est ce qui a **tranché concrètement la question laissée ouverte par la tâche 4.1** (« le pont
statique pour les rappels natifs — la question ne se posera qu'à l'étape C »). La réponse n'est pas
un pont : c'est que les deux états publiés par ces rappels, `s_CardWriteStatus_00604ab0` et
`s_CardReadStatus_00604a00`, sont des globales `.data` et devaient donc rejoindre `GameMemory`.
**Une globale lue à la fois par un rappel natif et par du code d'instance doit vivre là où les deux
peuvent la voir.** Cinq globales ont été rapatriées au total (les deux ci-dessus plus
`s_CardPhase_00578e0c`, `s_SelectScratch_00573338`, `s_EntitySlotTable_005e9180`,
`s_Unknown_00564a4c`) : `GameMemory` passe de 97 à 102 membres.

Au site d'appel, les rappels s'atteignent par le **nom de type** (`SaveData.CardWriteStatusHandler0`)
et le reste par l'instance (`Saves.PollCardTransfer`). La distinction se lit.

### Le piège d'ordre, refermé pour de bon

`Memory/Globals.cs` est **supprimé**, son champ `Mem` replié en tête de `Subsystems.cs`.

Motif : `Saves = new SaveData(Mem)` et `Mem` étaient des champs statiques de `Blaze` déclarés dans
**deux fichiers différents**. Or C# ne garantit l'ordre d'initialisation des champs statiques
qu'**à l'intérieur d'un même fichier** ; entre fichiers d'une classe partielle, il n'est pas
spécifié. C'est exactement le défaut qui a fait planter le jeu au découpage (huit champs
`Arena(0x…)` devenus `null + offset`), et l'en-tête de `Subsystems.cs` affirmait alors un ordre
« textuel, donc garanti » qui reposait en réalité sur la chance.

Les quatre champs sont désormais dans un seul fichier, dans l'ordre : `Mem`, `Ui`, `Prims`, `Saves`.
L'ordre est lisible **et** garanti. La contrainte d'initialisation précoce (l'arène de liste
d'affichage sous `0x01000000`) est préservée : `Subsystems.cs` reste dans l'initialisation statique
de `Blaze`, pas dans un composition root appelé depuis `Program.cs`.

### Le nom du point d'entrée est une décision, pas une évidence

`Primitives2d` s'atteint par `Prims`, **pas** `Prim`. L'oracle `compare-bodies.ps1` neutralise les
qualifications d'après une liste blanche, et il compare **sans tenir compte de la casse** : inscrire
`prim` y aurait fait avaler silencieusement tout `prim.` futur — or `prim` est un local dans 196
lignes du code de dessin. Un caractère de plus supprime la classe de faux négatifs.
Même contrôle pour `Saves` (3 occurrences, toutes en commentaire, que l'oracle retire de toute façon).

### Vérification

Build solution 0 erreur · oracle **« aucune logique modifiée »** sur 511 corps · tests de disposition
inchangés (3 rouges attendus / 2 verts) · `-Entry 1` **382,3** et `-Entry 2` **387** primitives par
image, soit les débits de référence · captures relues : carrousel, modèle 3D, panneaux et menu objets
conformes. Fins de ligne vérifiées CRLF uniformes, région à encodage double intacte.

### Reste de l'étape C

`Audio`, `Resources`, `Video`, `Input`, `Model3d`, `UiWindow` — six fichiers, ~5 400 lignes.
`UiWindow` a dû exposer `EmitClipRect`/`EmitViewportRect`, et `SaveSlots`/`Options` exposer
`LabelAt`/`BindMenuLabels` : ces promotions `private` → `internal` marquent les coutures que les
prochaines conversions auront à trancher.

---

## 2026-08-19 (étape C, 4 fichiers sur 9) — `Video`, et une régression cumulative trouvée par mesure

`Engine/Video.cs` → `Video(GameMemory)`, 1 143 l., 59 membres dont **2 statiques**.

### Le choix du fichier a été mesuré, pas déduit de sa taille

Le plan disait « du plus petit au plus grand », donc `UiWindow.cs` (368 l.). La mesure de **clôture**
a dit l'inverse :

```
                lignes | déjà dans GameMemory · à lui · autre Engine · SCREENS
  UiWindow.cs      368 |      4      0      5      6      <- le PIRE
  Model3d.cs       583 |     10      0     18      1
  Input.cs       1 017 |      0     27      2      7
  Video.cs       1 112 |      6     13      1      0      <- le MEILLEUR
  Resources.cs   1 110 |      5     34      4      6
  Audio.cs       1 253 |      0     25      3      2
```

`UiWindow` ne possède **aucune** globale : c'est un pur consommateur, et six de ses onze emprunts
pointent vers des fichiers d'écran. `Video` n'en emprunte qu'**un** hors `GameMemory`.

*Précision apportée après coup* : rapatrier une globale `.data` vers `GameMemory` n'est **pas**
l'étape D — c'est de l'étape A inachevée, légitime à tout moment. Le vrai coût de `UiWindow` est le
brassage qu'il impose et le risque d'aliasing qui va avec, pas un franchissement d'étape.

**La taille d'un fichier ne dit rien de son coût de conversion ; sa clôture, si.**

### Deux membres restent statiques, pour deux raisons différentes

`MoviePlayerWindowProc` porte `[UnmanagedCallersOnly]`. Contrairement aux huit une-lignes de
`SaveData`, son corps **appelle d'autres membres** — il rejoint donc l'instance par le point d'entrée
statique (`Vid.OnMoviePlayerActivate`, `Vid.StopMoviePlayback`…). Les deux lambdas de
`RegisterDisplayModeRestore` ont gardé leur mot-clé `static` par le même chemin : le `static` d'un
lambda est une garantie de non-capture, et on ne la brade pas pour économiser une qualification.

`s_WindowedStartup` reste statique **délibérément**. Ce n'est pas une globale `.data` du jeu : c'est
une décision d'hôte lue une fois dans l'environnement, que `Host/DeviceShim.cs` et
`Host/SyntheticDevice.cs` lisent pendant l'initialisation de leurs propres types. En faire un champ
d'instance ferait dépendre cette initialisation de la construction complète des sous-systèmes, sur
le chemin de démarrage le plus fragile du projet.

### La régression cumulative, et pourquoi elle n'était pas propre à `Video`

`playtest.ps1` a signalé « relance détectée » trois fois sur trois, alors que les exécutions
précédentes de la séance n'en montraient aucune. Le journal a donné la cause :
`display-list arena: lost the race for low memory, relaunching`.

Mesuré, 8 lancements : **5 courses perdues sur 8**, contre « environ une sur quatre » documenté.

Le mécanisme n'est pas dans `Video` mais dans la forme du composition root :

```
  [ModuleInitializer] -> TryPrepareDisplayListArena()
                      -> lit un champ statique de Blaze
                      -> construit Mem, Ui, Prims, Saves, Vid       <-- tout ça d'abord
                      -> PUIS réclame l'arène basse                 <-- la course
```

Or la réclamation ne lit que `Mem` (`s_UiDescriptors_00560ff0`, `s_StatusDescriptor_00560728`).
**Les quatre sous-systèmes étaient construits avant la course sans que rien ne l'exige** — les
tables de `UiText`, les huit chaînes de `Video`, autant de travail d'allocation placé au pire
endroit possible. Chaque conversion aggravait le taux ; `Video` a été celle qui l'a rendu visible.

Correction : les quatre points d'entrée deviennent **paresseux** (`Ui => s_ui ??= new UiText(Mem)`).
`Mem` reste immédiat, puisque lui seul porte la contrainte de mémoire basse.

```
  avant correction, 8 essais  : 5 perdues  (62 %)
  après correction, 16 essais : 4 perdues  (25 %)   = le taux documenté
```

Cette paresse **ne contredit pas** la leçon « pris paresseusement, le descripteur échouait » :
celle-ci porte sur les *descripteurs de mémoire basse*, qui restent pris immédiatement dans
`TryPrepareDisplayListArena`.

**Ce que l'épisode enseigne** : le banc d'essai a signalé la régression par une ligne d'information
que rien n'obligeait à lire — « relance détectée » — et non par un plantage ni par un débit
dégradé. Les deux verdicts habituels (aucun plantage, 382,3 et 387 primitives par image) étaient
**verts pendant toute la régression**. Un indicateur vert ne prouve que ce qu'il mesure.

### Vérification

Build solution 0 erreur · oracle « aucune logique modifiée » sur 511 corps · tests de disposition
inchangés (3 rouges attendus / 2 verts) · `-Entry 1` **382,3** et `-Entry 2` **387** primitives par
image · captures relues et identiques aux références.

### Promotions d'accessibilité entraînées

`private` → `internal` : `RaiseQuitException`, `Malloc`, `Free`, `BuildCdPath`, `s_ErrorCaption`,
et `Video.IUnknown_Release` — une constante de `Video` qu'`Input.cs` lisait sans que rien ne le dise.
Chacune marque une couture que les conversions suivantes auront à trancher.

---

## 2026-08-19 (étape C, 5 fichiers sur 9) — `Input`, et la règle de tri des globales empruntées

`Engine/Input.cs` → `Input(GameMemory)`, 1 044 l., 61 membres dont **1 statique**.

`CountJoystickCallback` reste `static` et, contrairement au rappel de `Video`, **n'a pas besoin de
rejoindre l'instance** : il ne touche que son paramètre `context`. C'est exactement ce que la
tâche 4.1 avait mesuré cinq étapes plus tôt, confirmé sur pièces.

### La règle de tri qui s'est dégagée

`Input` empruntait dix symboles à cinq fichiers. Les traiter d'un seul geste aurait été une erreur —
ils ne sont pas de même nature :

* **Suffixe VA** (`s_SizeSetting_005de160`, `s_JoystickDevice_00604b54`, `s_AnimationTable_005e9640`,
  `s_Unknown_005d9de0`, `s_Unknown_005de538`) → ce sont des globales `.data`, elles rejoignent
  `GameMemory`. Les atteindre via `using static Blaze` aurait masqué précisément la dépendance que
  ce refactor cherche à rendre visible.
* **Pas de suffixe VA** (`s_KeyBindings`, `s_JoystickSlot`, `s_ResolutionDetailLimit`) → c'est notre
  propre échafaudage de configuration, pas du `.data`. Reste chez son propriétaire, exposé
  `internal`. Deviendra un paramètre de constructeur quand la page d'options sera une classe.
* **`s_SmallTextureFlag`** porte une référence Ghidra — `AU_00564a05._51_4_`, un champ **à
  l'intérieur d'un blob** non modélisé. Il ne bouge pas : le déplacer seul, détaché de son blob,
  est exactement le geste qui fabrique le défaut d'aliasing.

### Un alias rattrapé au vol

`s_Unknown_005d9de0` est déclaré `=> (int)s_GammaIni` — son propre commentaire dit qu'il **est** le
même global (`DAT_005d9de0`, la clé « Gamma » de l'ini), septième occurrence d'un global reconstruit
sous deux noms. Le déplacer seul vers `GameMemory` aurait séparé la paire.

`s_GammaIni` l'a donc suivi, **avec ses deux voisines de déclaration** `s_SoundVolumeIni`
(`DAT_00589c70`) et `s_CdVolumeIni` (`DAT_0057925c`) : un bloc déménage entier. Vérifié à l'écran,
pas au compteur — la page d'options affiche son fond et son cadre, ce qui est précisément le
symptôme que la rupture de cet alias produisait (palettes passées dans `Gamma(valeur, 0.0)`, donc
noires).

`GameMemory` passe de 102 à ~110 membres.

### Ce que la mesure du taux de course a appris de plus

Première mesure après conversion : **7 échecs sur 16 (44 %)**, alarmante. Elle était **fausse** —
build et tests tournaient en parallèle. Reprise machine au repos : **7 sur 32 (22 %)**, conforme à
la référence de 25 %.

**Un banc d'essai non déterministe se mesure au repos.** C'est maintenant écrit dans
`test-protocol.md` à côté de la boucle, avec le contre-exemple chiffré : 44 % sous charge contre
12 % au repos sur le même binaire.

### Promotions d'accessibilité entraînées

`private` → `internal` : `DestroyJoystickDevice` (Startup), `ControlsIniSection`, `OptionsPage`,
`SaveOptionsIni`, `s_ResolutionIndex` (Options). Et un rappel de C# qui a coûté une passe de build :
**une constante se qualifie par le nom de type**, `Input.s_GammaScale_0054d000` et non
`Inp.s_GammaScale_0054d000` — sinon CS0176.

### Vérification

Build solution 0 erreur · oracle « aucune logique modifiée » sur 511 corps · tests de disposition
inchangés (3 rouges attendus / 2 verts) · `-Entry 1` **382,3**, `-Entry 2` **387** primitives par
image · `-Entry 3` (options) relu à l'écran · taux de course 22 % sur 32 essais au repos.

---

## 2026-08-21 (étape C, 6 fichiers sur 9) — `Audio`, un statique de portée et une fonction mal placée

`Engine/Audio.cs` → `Audio(GameMemory)`, 1 253 → 1 316 l., 76 membres dont **1 statique**.

Choisi par clôture mesurée, comme les cinq précédents : 25 globales à lui, 3 empruntées hors
`GameMemory`, et surtout **0 emprunt à `Screens/`** — le seul des quatre fichiers restants dans ce
cas. C'est aussi la **première conversion sans un seul `[UnmanagedCallersOnly]`** : le son du jeu
n'a aucun rappel natif, ce qui a retiré d'un coup le motif le plus coûteux des étapes précédentes.

### Un quatrième motif de `static`, et il ne repose sur aucune contrainte

Les trois motifs connus étaient tous subis : `[UnmanagedCallersOnly]`, un lambda `static`, une
décision d'hôte lue avant la construction. `InvokeComMethod` en révèle un quatrième, **choisi** :

C'est une invocation COM générique de sept lignes, qui ne touche que ses paramètres. Elle est
appelée depuis `Platform/Startup.cs` — mais pour libérer le **périphérique DirectInput**, dans
`DestroyJoystickDevice`. Rien à voir avec le son : la fonction ne partage avec l'audio que le fait
d'avoir été écrite dans le même fichier, parce que c'est là qu'un vtable COM a été déplié en
premier.

La rendre membre d'instance aurait donc fait construire tout le sous-système audio — quatre
allocations natives — **pour libérer une manette**, et aurait placé ce travail devant la course pour
la mémoire basse que l'étape 7.6 venait tout juste de dégager. Elle reste `internal static`, appelée
par nom de type (`Audio.InvokeComMethod`).

**La leçon générale** : retirer `static` n'est pas l'objectif, c'est le moyen. La question à poser
devant chaque membre est « qui l'appelle, et est-ce que ça a un sens qu'il dépende de ce
sous-système ? ». Une dépendance fausse coûte plus cher qu'une qualification.

### Quand un membre doit être exposé, c'est souvent la *fonction* qui est mal placée

`ShutdownStage3` (`FUN_00512ff0`) vivait dans `Platform/Startup.cs`, aux côtés des trois autres
étapes numérotées d'extinction. Son corps ne touche que des membres d'`Audio`, et il est **le seul
écrivain externe de `s_DirectSound`**.

Le geste réflexe — exposer `s_DirectSound` en `internal` — aurait laissé un champ COM modifiable
depuis un autre fichier, c'est-à-dire conservé exactement la fuite que la conversion existe pour
supprimer. Le VA `0x00512ff0` tombe dans la plage audio, donc la règle de routage (1)
d'`ARCHITECTURE.md` place la fonction dans `Engine/Audio.cs` : elle y a déménagé, corps inchangé.

`s_DirectSound`, `s_DirectSoundPrimary`, `ReleaseAllSoundBuffers` et deux constantes de vtable sont
restés **privés** en conséquence. Le site d'appel de la séquence d'extinction, lui, n'a pas bougé —
`Startup.cs` appelle `Aud.ShutdownStage3()` et garde un commentaire disant où la fonction est
partie.

**Le déménagement est gratuit pour l'oracle**, et c'est ce qui rend la manœuvre praticable :
`compare-bodies.ps1` indexe par **nom de méthode seul**, jamais par fichier+nom. Vérifié sur pièces
— aucune méthode signalée « absente de l'arbre courant ».

### Le nom du point d'entrée : `Aud`, pas `Audio`

`Audio` était disponible et se lisait bien. Il a été écarté : une propriété qui porte le nom de son
propre type déclenche la règle **« Color Color »** de C#, où `Audio.PlaySoundEffect(...)` se résout
tantôt comme un accès statique, tantôt comme un accès d'instance **selon la classe où la ligne est
écrite**. Ça compile, et c'est une ambiguïté qu'une transcription relue à la main contre `Blaze.c`
n'a aucune raison de porter.

`aud` est entré dans la liste blanche de l'oracle après la vérification que `TODO.md` exige :
**0 occurrence** hors commentaire dans tout `OpenForesia/src`. Il ne peut donc masquer aucune ligne
de logique existante.

### Une dette laissée ouverte, et pourquoi elle n'a pas été soldée ici

Trois symboles restent chez leur propriétaire en `internal` : `s_BaseSoundBank`, `s_LevelSoundBank`
(`Platform/Startup.cs`) et `s_SoundDirectory` (`Engine/Resources.cs`).

La règle de tri dégagée en 7.9 les classe « échafaudage » parce qu'ils n'ont **pas de suffixe VA**.
C'est faux : leurs commentaires les ancrent à `DAT_00604920`, `DAT_00600900` et `DAT_005e8340` —
c'est du `.data` réel, et `s_SoundDirectory` porte même une réparation d'aliasing déjà faite
(`DAT_005e8340` et `DAT_005e8520` fusionnés en un seul tableau de 77 entrées).

**Correction à apporter à la règle** : *le suffixe VA est un indice, pas la preuve ; l'ancre
`// Ghidra: DAT_…` du commentaire tranche.* Ils n'ont pas été rapatriés vers `GameMemory` dans cette
étape parce que le déménagement touche aussi `Startup.cs`, `Resources.cs` et
`Screens/State03/WorldMap.cs` : mêler un rapatriement `.data` à une conversion de classe rendrait
les deux invérifiables séparément. À faire pendant la conversion de `Resources`.

### Vérification

Build solution 0 erreur, et **tableau d'avertissements identique avant/après** (20 CA2014,
2 CS0169, 38 CS0414, 10 CS0649, 2 CS0652, 2 CS0675, 2 CS8618, 4 NETSDK1189) — comparé par
`git stash` + reconstruction complète, parce qu'un avertissement qui disparaît est un signal autant
qu'un qui apparaît · oracle « aucune logique modifiée » sur 511 corps, 0 méthode absente · tests de
disposition inchangés (3 rouges attendus / 2 verts) · `-Entry 1` sans plantage à **382,3**
primitives par image, capture relue : carrousel de portraits, modèle 3D et fenêtre de classe à
l'écran · **taux de course 3/16 = 19 %**, machine au repos, sous la référence de 22–25 %.

---

## 2026-08-21 (suite) — `Resources`, et un défaut d'aliasing que les tests ne pouvaient pas voir

`Engine/Resources.cs` → `Resources(GameMemory)`, 1 110 → 1 152 l., 67 membres, **0 statique**.
C'est la première classe entièrement d'instance : aucun rappel natif, aucun pointeur de fonction,
aucune décision d'hôte à préserver.

### La difficulté annoncée n'existait pas

`TODO.md` prévenait que ce fichier « porte `ToAnsiZ` », appelé par toutes les classes converties, et
que sa conversion demanderait de décider où vit cette aide. **C'était faux** : `ToAnsiZ` est dans
`Platform/Startup.cs:310`, déjà `internal static`, et n'a jamais été ailleurs. Trente secondes de
`grep` ont annulé une difficulté qui figurait dans le plan depuis deux jours.

**Vérifier sa propre note avant de planifier dessus** — c'est la deuxième fois en deux jours qu'une
affirmation de `TODO.md` se révèle fausse, après la règle de tri des globales.

### Ce que la conversion a réparé sans le chercher

`s_ArchiveFileName`, `s_SystemFileName` et `s_SoundArchiveTemplate` étaient des champs
`static readonly` initialisés par `ToAnsiZ(...)` — une méthode vivant dans un **autre fichier**.
C'est exactement la forme du défaut qui a coûté un plantage au démarrage le 2026-08-19 (tâche 7.4) :
entre fichiers, l'ordre d'initialisation des champs statiques d'une classe partielle n'est pas
spécifié. Ils marchaient par chance.

Devenus champs d'instance, ils sont construits au premier accès à `Res`, donc nécessairement après
l'initialisation statique de `Blaze`. Le risque **disparaît** au lieu d'être documenté. C'est un
bénéfice de la conversion qui n'était pas dans ses objectifs.

Trois méthodes `Build*` sont **repassées `static`** dans le mouvement inverse : ce sont des
initialiseurs de champ, et C# interdit à un initialiseur d'appeler une méthode d'instance (CS0236).
Elles sont pures — allocation plus littéraux — donc `static` est le bon correctif et non un
contournement de compilateur.

### Le rapatriement `.data` du son, soldé

La dette ouverte la veille par `Audio` est fermée : `s_BaseSoundBank`, `s_LevelSoundBank`
(`Platform/Startup.cs`), `s_SoundDirectory` et la structure `SoundDirectoryEntry`
(`Engine/Resources.cs`) rejoignent `GameMemory`. Deux classes converties se les partageaient, plus
`Startup` et la carte du monde.

Deux précautions qui comptent :

* les banques restent **allouées par `RunApplication`** (`Mem.s_BaseSoundBank = Malloc(790000)`) :
  seul le champ qui les reçoit a déménagé, l'allocation a lieu au même instant qu'avant ;
* la structure suit son tableau. La laisser dans `Audio` aurait fait dépendre `Memory/` d'`Engine/`.

Fait en **étape séparée** de la conversion, chacune vérifiée entièrement — c'est la raison pour
laquelle la dette avait été laissée ouverte la veille plutôt que soldée en marge.

### Le défaut d'aliasing trouvé en chemin — et pourquoi les tests l'avaient manqué

En requalifiant les sites, `Screens/State03/WorldMap.cs:328` est apparu :

```csharp
private static readonly byte* s_SoundBankHeader_005e8520 = (byte*)NativeMemory.AllocZeroed(0x88);
```

Or `0x005e8520 == 0x005e8340 + 0x1e0`. C'est la **queue** du répertoire de sons, allouée à part.
Vérifié contre la source de vérité plutôt que déduit — `Blaze.c` 74824 charge `&DAT_005e8340` sur
0x1e0 octets depuis `BASE.SND`, et 63353 charge `&DAT_005e8520` sur 0x88 depuis le fichier de zone :
un seul tableau de 77 entrées de 8 octets, chargé en deux fois, que `PlaySoundEffect` indexe depuis
`&DAT_005e8340` pour **tous** les ids jusqu'à `0x4c`.

Conséquence attendue en jeu : les effets sonores de niveau (`0x3c..0x4c`) lisent un décalage et une
longueur nuls — silencieux — alors que leurs échantillons sont bien chargés juste en dessous.
**Non corrigé** : la correction change le comportement, et appartient au changement qui traite les
cinq autres défauts d'aliasing. Consigné aux deux endroits où il se lit, `Audio.cs` et `WorldMap.cs`.

**Le point vraiment instructif est que les quatre tests de disposition étaient verts dessus.** Ils
n'indexent que par le **suffixe VA du nom C#** : un `.data` nommé sans son adresse leur est
totalement invisible — il ne peut ni entrer en collision, ni déborder, ni être signalé.
`s_SoundDirectory`, nommé sans suffixe, a masqué un recouvrement réel dont les tailles se
corroborent au bit près.

**Un `.data` sans suffixe VA n'est donc pas seulement mal nommé : il est hors de portée de
l'outillage.** Le nom est l'index, pas une décoration. Écrit dans `OpenForesia.Tests/README.md`.

Le renommage qui réparerait ça (`s_SoundDirectory` → `s_SoundDirectory_005e8340`, et les banques en
`s_BaseSoundBank_00604920` / `s_LevelSoundBank_00600900` — adresses pinées dans `Blaze.c` 61791-2)
**n'a pas été fait** : l'oracle compare du texte normalisé, où un renommage est indiscernable d'un
changement de logique. Il ferait sonner sept méthodes à chaque exécution future, sauf à refiger la
référence — ce qui effacerait sa mémoire. Le renommage va donc avec la correction, qui refige de
toute façon.

### Vérification

Les deux étapes ont été vérifiées séparément et intégralement.

| | conversion | rapatriement `.data` |
|---|---|---|
| build | 0 erreur | 0 erreur |
| avertissements | identiques avant/après (`git stash` + reconstruction) | identiques |
| oracle | « aucune logique modifiée », 0 méthode absente | idem |
| tests | 3 rouges attendus / 2 verts | idem |
| `-Entry 1` | **382,3** primitives/image, capture relue | **382,3**, capture relue |
| `-Entry 2` | **387** primitives/image, capture relue | **387** |
| taux de course | **4/16 = 25 %** | **1/16** |

`-Entry 2` a été ajouté au protocole de cette conversion parce que `Resources` porte l'écran de
chargement et les ressources de menu : conclure sur un seul écran aurait laissé ce chemin non
exercé.

---

## 2026-08-21 (fin) — `Model3d`, et un outil de mesure qui fabriquait un faux plan

`Engine/Model3d.cs` → `Model3d(GameMemory)`, 583 → 622 l., **7 membres, 0 statique, 0 champ**.
Huitième vraie classe sur neuf.

C'est la seule classe de l'étape C qui ne possède **aucune** globale. Sa conversion n'a donc rien
déménagé : elle a rendu visible ce qu'elle lit ailleurs — 11 symboles de `GameMemory`, 17 registres
GTE, un compteur d'écran.

### Le vrai sujet : le script de clôture rangeait mal, et le plan s'en est trouvé faux

`TODO.md` classait `Model3d` « à faire en dernier » depuis trois jours, au motif qu'il empruntait
**18 symboles à d'autres fichiers `Engine/`**, donc qu'il fallait attendre leur conversion.

C'était faux. **17 des 18 sont les registres GTE de `Platform/Winsys3d.cs`** — des pointeurs dans le
segment de données de `winsys3d.dll`, résolus par `GetProcAddress` sur des ordinaux sans nom. Ce ne
sont ni des globales de `Blaze.exe`, ni du code destiné à devenir une classe. Le fichier était en
réalité le **plus fermé** des deux restants, et sa conversion a tenu en une passe.

L'erreur venait du script de mesure, dont le `case` n'avait de branche que pour `GameMemory` et
`Screens/` :

```bash
case "$src" in Memory/GameMemory.cs) gm=…;; Screens/*) sc=…;; "") ;; *) en=…;; esac
#                                                                     ^^^ tout le reste, y
#                                                                     compris Platform/ et Interop/
```

Le seau fourre-tout `*)` absorbait `Platform/` et `Interop/` sous l'étiquette « autre Engine ».

**Un seau fourre-tout dans un `case` ne se signale jamais** : il ne produit ni erreur, ni valeur
aberrante, seulement un chiffre plausible dans la mauvaise colonne. Et un chiffre plausible produit
par un outil ne se relit pas. Corrigé dans `TODO.md`, avec les branches `Platform/*|Interop/*` et
`Engine/*` séparées — et `UiWindow`, le dernier fichier, a été remesuré avec la bonne version.

Deuxième défaut du même script, plus bénin : le compte agrégé **inclut les symboles cités en
commentaire**. Deux des « 5 emprunts à `Screens/` » de `UiWindow` sont dans ce cas. Lister le détail
avant de conclure, pas seulement compter.

### Pourquoi les 17 registres GTE restent dans `Platform/`

Ce n'est pas un compromis mais l'application de la règle corrigée le matin même : **aucune ancre
`// Ghidra: DAT_…` ne les désigne**, donc ils ne sont pas du `.data` de `Blaze.exe` et n'ont rien à
faire dans `GameMemory`. Ce sont les registres du GTE émulé par la DLL, documentés dans
`Documentation/DLL/winsys3d.md`. Trois autres fichiers les lisent (`Model3d`, `WorldMap`,
`CharacterCreation`) : c'est de l'interop partagée, pas l'état privé d'un rendu.

Ils passent `internal`, avec cinq aides du même fichier (`EnsureGteRegisters`, `RotMatrix`,
`ScaleMatrixSaturating`, `NormalClip`, `ReadMac0`).

### Le nom du point d'entrée, encore une fois décisif

`Mdl`, et **non `Model`** : `model` est un paramètre vivant dans dix lignes de `Model3d.cs`
lui-même. La liste blanche de l'oracle étant insensible à la casse, inscrire `model` aurait rendu
l'outil aveugle à ces dix lignes — exactement le piège que `Prims` (contre `Prim`) avait déjà évité.
`mdl` vérifié à 0 occurrence avant inscription.

C'est la troisième fois que ce contrôle change le nom retenu. Il vaut ses trente secondes.

### Une dette laissée ouverte

`s_Unknown_005e9818` (`DAT_005e9818`, le budget de primitives par image) reste chez
`Screens/State00/MainMenu.cs`, exposé `internal`. C'est du `.data` et il devrait rejoindre
`GameMemory` ; comme les cinq de `Resources`, ce rapatriement est de l'étape A et se fera
séparément.

### Vérification

Build 0 erreur, **tableau d'avertissements identique** avant/après (`git stash` + reconstruction
complète) · oracle « aucune logique modifiée » sur 511 corps, 0 méthode absente · auto-test de
l'oracle 9/9 · tests de disposition inchangés (3 rouges attendus / 2 verts) · `-Entry 1` sans
plantage à **382,3** primitives par image, **capture relue — le modèle 3D animé du personnage est
bien à l'écran**, ce qui est le contrôle qui compte ici puisque `DrawAnimatedModel` est précisément
ce qui le dessine · `-Entry 2` à **387** · **taux de course 3/16 = 19 %**, machine au repos, sous la
référence de 22–25 %.

---

## 2026-08-21 — `UiWindow` : l'étape C est terminée, et une fausse alerte instructive

`Engine/UiWindow.cs` → `UiWindow(GameMemory)`, 368 → 411 l., **5 membres, 0 statique, 0 champ**.
Neuvième et dernière classe de l'étape C. **Tout `Engine/` est désormais en classes d'instance.**

Comme `Model3d`, ce fichier ne possède aucune globale : sa conversion ne déplace pas d'état, elle
rend visible ce qu'il lit. Six symboles de `GameMemory` (déjà via `mem.`), quatre d'`Input` (déjà
via `Inp.`), quatre d'écrans.

### La mesure « hors commentaire » avait un défaut, et le compilateur l'a rattrapée

Le relevé d'emprunts annonçait `s_FadeCounter_005e9960` « cité en commentaire seulement ». Faux :
`DrawUiPanel` le lit. Le filtre en cause :

```bash
grep -vE "^[0-9]+:\s*(//|///|\*)"   # le `\*` vise les continuations de bloc /** … */
```

Dans du code à pointeurs, `*` en début de ligne est aussi un **déréférencement** — ici
`*(uint*)(atlas + (s_FadeCounter_005e9960 >> 1 & 3) * 8))`. Le filtre l'a pris pour un commentaire.
Corrigé dans `TODO.md` : `* ` suivi d'une espace est une continuation, `*(` est du code.

C'est le troisième défaut d'outillage de la journée, après le seau fourre-tout du script de clôture
et l'angle mort des tests de disposition. **Aucun des trois ne produisait d'erreur** — seulement un
chiffre plausible, ou un test vert.

### Le nom du point d'entrée, quatrième fois que la mesure décide

`UiWin`. `Panel` était le nom naturel — le membre principal est `DrawUiPanel` — mais `panel` est un
local dans **58 lignes**. `Win` et `Wnd` mesuraient zéro occurrence et ont quand même été écartés :
dans un dépôt qui fait beaucoup de Win32, ils se lisent comme de l'interop.

Sur les quatre conversions de la journée, **trois fois la mesure a changé le nom retenu**
(`mdl` contre `model`, `uiWin` contre `panel`, et `res` retenu contre toute attente).

### Un recouvrement d'arène relevé, latent, non corrigé

Les trois dwords `s_Unknown_005de154/158/15c` que cette classe lit vivent en champs séparés dans
`MainMenu.cs`, alors que leurs adresses tombent **à l'intérieur** de
`GameMemory.s_ItemMenuArena_005de100` (0x460 octets, donc jusqu'à `0x005de560`). Idem pour
`s_CheatIndex_005de164` (`UiText.cs`) et `s_Unknown_005de168` (`Input.cs`).

**Vérifié : aucun site n'adresse l'arène entre `0x005de154` et `0x005de170`.** Le recouvrement ne
se manifeste donc pas aujourd'hui — c'est un risque de structure, pas un défaut observable.

Deux raisons de ne pas y toucher : le corriger changerait le comportement, et **l'étendue de
l'arène est elle-même douteuse** — son commentaire annonce 0x420 octets, son allocation en fait
0x460. Ces 0x40 d'écart sont exactement ce qui rend vrai ou faux le signalement de
`s_EntityList_005de558` par les tests. Trancher demande de retrouver l'étendue réelle dans
`Blaze.c` : c'est de la reconstruction, pas du refactor. Consigné dans l'en-tête d'`UiWindow.cs`.

### La fausse alerte sur le taux de course — et ce qu'elle apprend

Deux lots de 16 essais ont rendu **5/16 puis 6/16 (31 % et 37 %)**, contre les 22–25 % documentés.
Alarme franche, sur la mesure même qui avait attrapé la régression du 2026-08-19.

**Il n'y avait pas de régression.** Le raisonnement mécanique le disait déjà : cette conversion, et
celles de la journée, ne font que *retirer* du travail du chemin pré-course — les chaînes `ToAnsiZ`,
les sept tables `s_Vt_*`, les tables de zone de `Resources` passent d'une initialisation statique de
`Blaze` (avant la course) à des champs d'instance paresseux (après). Le taux ne pouvait pas monter
de ce fait.

Le contrôle l'a confirmé : **arbre d'avant, 32 essais, 5 perdues = 16 %** ; **arbre modifié mesuré
juste après, 32 essais, 1 perdue = 3 %**. Le sens était inverse de l'alarme.

**La leçon : la référence documentée est datée, donc elle ne vaut pas contrôle.** Le taux dérive au
fil d'une session — 31 %, 37 % puis 3 % sur le même code. Le seul contrôle valable est l'arbre
d'avant mesuré **adjacent dans le temps** (`git stash` + build + 32 essais). Écrit dans
`test-protocol.md`, avec le contre-exemple chiffré.

Corollaire : **raisonner sur le mécanisme avant de lancer une enquête.** Quand la mesure contredit
ce que le code impose, suspecter la mesure — mais la refaire, pas l'ignorer.

### Ce que l'étape C laisse en place, avec sa condition de retrait

Les neuf sous-systèmes sont des classes ; les **points d'entrée de `Subsystems.cs` sont maintenant
le dernier échafaudage**. L'en-tête de ce fichier dit depuis le début qu'ils disparaissent « quand
un sous-système appelant devient lui-même une classe d'instance ». C'est désormais vrai de tous :
les qualifications croisées entre classes (`Aud.`, `Inp.`, `Ui.`, `UiWin.` à l'intérieur d'une
classe) peuvent devenir des paramètres de constructeur.

Non fait ici, délibérément : ce serait une dixième conversion déguisée en neuvième, et elle mérite
d'être mesurée et vérifiée pour elle-même.

### Vérification

Build 0 erreur, **tableau d'avertissements identique** avant/après · oracle « aucune logique
modifiée » sur 511 corps, 0 méthode absente · auto-test 9/9 · tests de disposition inchangés
(3 rouges attendus / 2 verts) · `-Entry 1` **382,3** et `-Entry 2` **387** primitives par image,
**les deux captures relues — les cadres de panneau, qui sont précisément ce que `DrawUiPanel`
émet, sont corrects** · taux de course **1/32 = 3 %** contre un contrôle adjacent à **5/32 = 16 %**.

---

## 2026-08-21 — le retrait des points d'entrée, moitié intérieure

Les 28 qualifications croisées **entre classes converties** sont devenues des paramètres de
constructeur. Plus aucun sous-système n'atteint un autre par `Subsystems.cs`.

```
UiWindow(GameMemory, UiText, Input, Audio)
Primitives2d(GameMemory, UiText, UiWindow)
Resources(GameMemory, Primitives2d, Input, Audio)
Video(GameMemory, Resources)
```

Les cinq autres classes ne dépendent d'aucune sœur et gardent `(GameMemory mem)`.

### La mesure d'abord : une matrice, et le graphe qu'elle révèle

Compter les qualifications par paire, `depuis` × `vers`, a suffi à cadrer tout le travail — et à
éviter deux erreurs.

**Première version de la matrice : fausse.** Elle comptait `Input → Video`, qui aurait fermé un
cycle `Input → Video → Resources → Input` et rendu l'injection impossible. Ce `Vid.` était **dans un
commentaire**. Avec le filtre corrigé le matin même (`* ` avec espace = continuation, `*(` = code),
l'arête disparaît et le graphe devient un **DAG propre** :

```
Video → Resources → { Primitives2d → UiWindow, Input, Audio }
                      UiWindow → { UiText, Input, Audio }
```

**Le filtre à commentaires corrigé le matin a donc servi le jour même**, sur la décision la plus
structurante de la journée. Un cycle aurait signifié « injection impossible, garder les points
d'entrée » — conclusion fausse tirée d'un commentaire.

**Deuxième chose que la matrice a montrée** : les sept `Vid.` de `Engine/Video.cs` ne sont pas des
dépendances mais des **auto-références**, depuis deux lambdas `static` et depuis
`MoviePlayerWindowProc` que `[UnmanagedCallersOnly]` force à rester statique. Une méthode statique
ne peut atteindre son instance que par un point d'entrée : elles ne relèvent pas du retrait, elles
sont la raison d'être du mécanisme.

### Ce que ce retrait ne fait PAS, et il faut le dire

**665 lecteurs subsistent**, tous dans `Screens/` et `Platform/`. Ce sont encore des fichiers de
`partial class Blaze` : ils n'ont pas de constructeur, donc rien à injecter. `Subsystems.cs` ne
disparaît pas, il maigrit de 28 usages sur ~693.

Le gain n'est donc pas la taille du fichier. C'est que **chaque signature de classe déclare
maintenant ses vraies dépendances** — l'objectif énoncé du refactor depuis le début. `Resources`
dit qu'il touche les primitives, les entrées et le son ; avant, il les atteignait en silence.

### Un commentaire devenu faux, corrigé

`Subsystems.cs` affirmait : « `Prims` construit APRÈS `Ui`, dont il dépend : **l'ordre textuel de
ce fichier est ce qui garantit l'ordre d'initialisation** ». C'était vrai quand ces points d'entrée
étaient des champs `static readonly` ; depuis la tâche 7.6 ce sont des **propriétés paresseuses**,
et l'ordre textuel ne garantit plus rien.

La preuve est dans le code d'aujourd'hui : `Prims` référence `UiWin`, **déclaré tout en bas du
fichier**, et cela fonctionne — la résolution a lieu au premier appel, pas à l'initialisation
statique. Si le commentaire avait dit vrai, cette ligne serait cassée.

Corrigé, avec la conséquence pratique écrite : **ne pas réintroduire de champ immédiat ici** pour
« clarifier l'ordre ». Ce serait remettre le graphe à la merci de l'ordre des fichiers que le
compilateur reçoit — le défaut qui a coûté un plantage le 2026-08-19 — et remettre leur travail
d'allocation devant la course pour la mémoire basse.

### Vérification

Build 0 erreur, tableau d'avertissements identique · oracle « aucune logique modifiée », 0 méthode
absente — attendu, `Aud.` et `aud.` sont normalisés pareil, mais c'est justement ce qui rend le
geste vérifiable · tests 3 rouges attendus · `-Entry 1` **382,3** et `-Entry 2` **387**, captures
relues.

Taux de course avec la procédure écrite le matin : **arbre modifié 2/32 = 6 %**, **contrôle de
l'arbre d'avant mesuré adjacent 6/32 = 19 %**. Pas de régression. Le mécanisme le prédisait : tout
reste paresseux, donc rien de neuf ne passe devant la course ; toucher `Res` construit désormais
aussi `Prims`, `UiWin`, `Ui`, `Inp` et `Aud`, mais au premier usage, bien après.

---

## 2026-08-21 — les défauts d'aliasing corrigés, et deux que les tests ne voyaient pas

**Premier changement de la série à modifier le comportement.** Les cinq conversions et le retrait
des points d'entrée prouvaient « la forme a changé, pas la logique » ; celui-ci change la logique
délibérément, et se vérifie donc à l'écran, pas au compteur.

**Les cinq tests de disposition passent** — pour la première fois depuis leur écriture.

### Les huit corrections

| # | défaut | ce que le binaire dit | correction |
|---|---|---|---|
| 1 | `0x00566c70` alloué **deux fois** : `s_MenuRenderBlock_00566c70` (0x40, MainMenu) et `s_ViewRect_00566c70` (0x10, WorldMap) | `Blaze.c` 63665/63669 et 89424/89428 écrivent les **mêmes** champs puis passent `&DAT_00566c70` aux **mêmes** ordinaux | une allocation, dans `GameMemory`, **0x14** |
| 2 | `s_MenuScreenBlock_00566c48` déclarait 0x40 | écritures jusqu'à `+0x13`, voisin à `+0x28` | 0x28 |
| 3 | `s_SoundBankHeader_005e8520` alloué à part | `0x005e8520 == 0x005e8340 + 0x1e0` : la queue du répertoire de sons | vue : `s_SoundDirectory_005e8340 + SoundDirectoryBaseCount` |
| 4 | `s_EntityList_005de558` dans `ItemMenuArena` | le commentaire de l'arène raisonnait « juste avant `DAT_005de510` » = 0x410 ; l'allocation demandait 0x460 | arène ramenée à **0x410** |
| 5-7 | `s_CameraMatrix_006008e0`, `s_ModelResultA_005fa588`, `s_ModelResultB_005f7b40` : allocations propres dans `EntityArena` | l'arène 0x005eca00..0x00600c00 les couvre | devenues des **vues** |
| 8 | `s_CardPathDescriptor_00604940` déclarait 0x100 | voisin réel `DAT_00604960`, soit **0x20** | 0x20 |

**La taille n'a jamais été choisie, toujours déduite** — par les écritures observées d'un côté, par
le symbole voisin de l'autre. Deux fois, le binaire a contredit ce que le code affirmait : `#4`
(l'allocation contredisait son propre commentaire) et `#8`.

Pour `#1`, une vérification a compté : `Ordinal_766` et `Ordinal_770` sont des **souches
`return 0;`** côté `winsys3d.dll` (table des souches dans `Documentation/DLL/winsys3d.md`), donc la
DLL ne contraint pas la taille. Sans ce contrôle, on aurait pu croire qu'elle imposait une
structure.

### Le défaut le plus intéressant, et les tests ne le voyaient pas

`DAT_006008d0/d4/d8` — le décalage monde appliqué à la position d'une entité **avant projection**.

* `Screens/State03/WorldMap.cs` l'**écrivait** par l'arène : `*(int*)Mem.Entity(0x006008d0) = …`
* `Engine/Model3d.cs` le **lisait** dans un champ séparé : `mem.s_Unknown_006008d0`
* `Destination.cs` et `SaveSlots.cs` remettaient à zéro le champ séparé

Les deux mémoires ne se rencontraient jamais. Le décalage posé par la carte du monde était donc
invisible à la projection qui devait s'en servir.

**Pourquoi les tests étaient verts dessus** : `Aucune_allocation_propre_dans_l_etendue_d_une_arene`
ne considère que les symboles de type `OwnAllocation`. Un simple champ `int` à une adresse qu'une
arène couvre ne déclenche rien. **Second angle mort de l'analyseur**, écrit dans
`OpenForesia.Tests/README.md` avec la correction proposée : étendre l'assertion aux champs de
valeur, probablement le meilleur rapport travail/valeur qui reste à ces tests.

Corrigé en **propriétés adossées à l'arène** plutôt qu'en champs, pour que les douze sites d'appel
ne bougent pas : ils lisent et écrivent comme avant, mais dans la mémoire de l'arène.

### Le renommage, et pourquoi il attendait ce changement-ci

Quatre `.data` dont le nom C# ne portait pas l'adresse — donc **hors de portée des tests**, qui
n'indexent que par le suffixe VA :

```
s_SoundDirectory  -> s_SoundDirectory_005e8340     (Blaze.c 74824)
s_BaseSoundBank   -> s_BaseSoundBank_00604920      (Blaze.c 61791, 790000 octets)
s_LevelSoundBank  -> s_LevelSoundBank_00600900     (Blaze.c 61792, 260000 octets)
s_GameStateId     -> s_GameStateId_005de510
```

Un renommage est indiscernable d'un changement de logique pour l'oracle : il aurait sonné à chaque
exécution future, sauf à refiger la référence. **Ce changement refigeait de toute façon** — d'où le
groupement, décidé deux jours plus tôt et tenu.

### Ce que l'oracle a prouvé, et comment il a été relu

L'oracle a signalé **20 corps** : 19 renommages purs et **une** vraie modification,
`LoadMenuResources`, qui est précisément la correction `#1`.

Mais l'oracle ne rapporte que la **première** ligne divergente par méthode : une seconde
modification cachée plus bas serait passée. Contrôle fait au niveau du diff — toutes les lignes de
code modifiées, commentaires et renommages exclus — et le résultat est **exactement les huit
corrections, rien d'autre**. C'est ce contrôle-là qui autorisait à refiger.

Référence refigée : **511 corps**. L'ancienne reste dans l'historique (`95344c7`), donc aucune copie
d'archive n'a été gardée.

### Vérification — à l'écran, pas au compteur

Build 0 erreur, avertissements identiques · **les 5 tests de disposition verts** · `-Entry 0` (liste
de sauvegardes) **470** primitives/image, `-Entry 1` **382,3**, `-Entry 2` **387**, **les trois
captures relues**.

`-Entry 0` était le contrôle qui comptait : il exerce le chargement des sauvegardes, donc le
descripteur de carte dont la taille passe de 0x100 à 0x20. Les six noms de personnage s'affichent
correctement.

Taux de course **7/32 = 22 %** contre un contrôle adjacent à **6/32 = 19 %** — indistinguable.

### Ce qui reste ouvert, et c'est délibéré

**L'arène de carte mémoire.** `0x00604940..0x006049bc` est une région contiguë de globales de carte
(9 symboles, 81 sites relevés) qui mérite le traitement « un bloc, chaque symbole une vue » de
l'arène `0x005e9000`. Tant qu'elle n'existe pas, un nom de fichier de plus de 26 octets déborderait
sur le **tas** au lieu de déborder sur `DAT_00604960` comme dans l'original. Le plus long nom du
jeu de 1998 fait 19 octets, donc la marge tient — mais elle est plus mince qu'avant, et c'est le
prix de la fidélité. Chantier séparé : il touche le sous-système de sauvegarde, le plus difficile à
vérifier à l'écran.

---

## 2026-08-21 — `FUN_00533940` et `FUN_00533450` : le front de la carte du monde avance

Deux fonctions transcrites, et **le point d'arrêt de l'état 3 s'est déplacé** : la pile ne s'arrête
plus sur `FUN_00533940` mais sur `FUN_005350b0`, la palette de zone.

### `FUN_00533450` → `GroundHeightOnFace` (52 lignes)

La hauteur du sol sous un point (x, z), par interpolation du plan de la face. Écrite en premier
parce qu'elle est indépendante et qu'elle sert de contrôle, exactement comme le prévoyait `TODO.md`.

Reprend à l'identique les conventions de `TryPlaceEntity` — sommet de 8 octets (x et y dans les
deux moitiés du dword de tête, z dans le short de `+4`), les deux arêtes dans les vecteurs GTE 799
et 800, leur produit vectoriel dans 801 avec **le même ordre de facteurs**, la troncature 16 bits
dans 813.

**Une erreur de précédence attrapée en la relisant, et elle vaut d'être notée.** Ghidra écrit
`(… / N.y - y0) * 0x1000`. Écrit d'abord `… / mac801[1] - y0 << 0xc`, c'était juste — `<<` lie plus
lâche que `-`. En remplaçant `<< 0xc` par `* 0x1000` pour coller à Ghidra, la précédence bascule :
`*` lie plus **fort** que `-`, donc l'expression devenait `A/B - (y0 * 0x1000)`. Corrigé par une
variable intermédiaire et un commentaire sur place. **Un remplacement « plus fidèle » peut changer
le sens** quand il change la classe de précédence de l'opérateur.

### `FUN_00533940` → `FinalizeEntityPlacement2` (375 lignes)

Partant des quatre références de face de l'emplacement, elle marche de proche en proche à travers
le maillage en franchissant les arêtes partagées, jusqu'à trouver la face qui contient la position.

**Le graphe n'est pas irréductible, contrairement à ce qu'il paraît.** Ghidra en rend sept labels
sautés depuis plusieurs endroits plus un `joined_r0x005342c7`. En les suivant un à un, la structure
se révèle simple : **c'est une boucle sur les arêtes de la face courante**.

```
triangle : (i0,i2)  (i2,i1)  (i1,i0)
quad     : (i0,i2)  (i2,i1)  (i1,i3)  (i3,i0)
```

Chaque arête est testée par un `NormalClip` ; un signe négatif veut dire « le point est de l'autre
côté », donc on franchit **cette** arête vers la face voisine et on reprend à l'arête suivante.

Ce qui a débloqué la lecture : les trois destinations de saut `LAB_00533af7`, `LAB_00533b96` et
`LAB_00533e9f` sont exactement « reprendre à l'arête 1, 2, ou 2-quad ». Le code compare la clé de
l'arête franchie pour savoir laquelle — mais la comparaison est une **table de correspondance
déguisée**, et elle se résume à `arête suivante = arête franchie + 1`. Une fois ça vu, les sept
labels deviennent un `while` sur un compteur d'arête.

**L'expression à six termes inlinée six fois est le `NormalClip` du SDK**, déjà reconstruit sous ce
nom dans `Platform/Winsys3d.cs` (FUN_0051c280). Les six termes ont été recoupés un à un contre la
version inlinée avant de l'appeler — Ghidra les émet dans un ordre différent, mais c'est la même
expression sur les mêmes registres 805/806/807/808.

### Trois décisions de transcription

**Les deux boucles restent dans la même méthode.** La seconde lit trois locales que la première
laisse derrière elle (`local_30`, `local_40`, `local_1c`). Les extraire aurait demandé de les
passer en paramètres, donc une signature mensongère : ce ne sont pas des entrées, ce sont des
restes.

**Deux locales que l'original lit sans les avoir écrites.** `local_1c` n'est assigné que par la
branche « face à quatre sommets » de la première boucle, et `local_30` y garde le sommet 2 de la
dernière face examinée — ce qui n'a aucun sens comme hauteur. C# exige une affectation définie ;
0 est retenu, avec un `TODO(REVERSE)` sur place. Les deux ne sont lus qu'à travers
`previousFace != 0`, faux au premier tour, donc sans effet observable — mais l'indétermination est
notée plutôt que masquée.

**Une extension de signe rendue explicite.** Ghidra écrit `uVar20 << 0x10 | (int)sVar4` ; la boucle
garantit `sVar4 >= 0`, donc c'est inoffensif, mais ça valait un CS0675. Remplacé par
`(uint)(ushort)other`, qui calcule exactement la même chose — **un avertissement de signe qu'on
apprend à ignorer est le début d'un vrai bug de signe.**

### La globale qui manquait, et pourquoi elle n'a pas demandé d'arène

`TODO.md` annonçait que `_DAT_005de730` / `_DAT_005de734` « tombent hors de tout ce qui existe » et
qu'« il faudra une arène de plus ». Vérification faite : **non**. C'est un bloc de 8 octets, et la
taille est déduite de trois côtés qui concordent :

* l'original initialise par **deux écritures de dword** mais relit par
  `(&DAT_005de730)[local_50]` en **shorts** — donc 8 octets vus comme quatre shorts ;
* l'indice `local_50` vaut `4 - local_4c` (avec `local_4c` dans [1,4]) ou le type de face qui
  indexe `slot + 0x80 + type * 4`, lui aussi borné à 4 entrées : **0..3** dans les deux cas ;
* le symbole suivant, `s_Unknown_005de738`, est à **exactement 8 octets**.

Ajouté à `GameMemory` sous le nom `s_FaceWalkHistory_005de730`, avec un `TODO(REVERSE)` : la région
0x005de700..0x005de72f est référencée par `Blaze.c` mais pas encore reconstruite ; si un site venait
à l'indexer d'un bout à l'autre, ce bloc devrait la rejoindre.

### Vérification — le front a bougé, et c'est la preuve

Build 0 erreur, avertissements inchangés (le CS0675 introduit a été résorbé) · oracle : **un seul
corps signalé**, `FinalizeEntityPlacement2`, le stub devenu implémentation — donc rien d'autre n'a
bougé · tests de disposition 5 verts.

**Le contrôle qui compte** : la reproduction documentée (`OPENFORESIA_SKIP_AREA_HANDLER=1`, entrée 0,
`WAIT4,ENTER,WAIT1,START,WAIT4,ENTER,WAIT3,ENTER,WAIT12`) ne s'arrête plus sur `FUN_00533940`. La
pile pointe maintenant sur :

```
NotImplementedException: FUN_005350b0 : la mise en place de palette de zone (95 lignes)
   at ApplyAreaPalette(Byte) ... WorldMap.cs:1300
   at PlaceWorldMapParty(Byte*) ... WorldMap.cs:1548
   at RunWorldMapFrame() ... RunWorldMapState() ... RunGameStateTick()
```

C'est-à-dire **exactement l'étape suivante que `TODO.md` annonçait**. La marche fait seize pas
d'arithmétique de pointeurs sur un maillage réel : une foulée ou un index faux fauterait ou
boucherait, donc passer au travers est un signal, même s'il ne prouve pas la justesse numérique.

**Un piège du banc d'essai, à connaître** : la première tentative n'a rien reproduit du tout et
s'est arrêtée à l'écran d'organisation du groupe. Cause : `(relance detectee)` — le jeu avait perdu
la course pour la mémoire basse et s'était relancé en processus fils, ce qui **décale toute la
séquence de touches** de `-Then`. Relancer suffit. À ne pas confondre avec « la séquence est
fausse ».

---

## 2026-08-21 — reorganisation du depot : la racine ne porte plus que deux fichiers

Aucun changement de code, aucun changement de comportement. La racine portait six decompilations
Ghidra (6,9 Mo), cinq DLL et onze fichiers `.md` en vrac ; elle ne porte plus que `CLAUDE.md` et
`OpenForesia.sln`.

```
Legacy/Binaries/     Blaze.exe  Blaze.cfg  + les cinq DLL d'aide
Legacy/Decompiled/   Blaze.c  winsys3d.c  wsyskern.c  playst.c  3dhelp.c  gldhelp.c
Legacy/Raw/          (inchange)

Documentation/ARCHITECTURE.md
Documentation/Etat/        TODO.md  REVERSE_STATUS.md  HISTORY.md
Documentation/Methode/     Prompt.md  Strategy.md
Documentation/Reference/   DataSymbols.md  legacy-bug.md  OpenForesia.cfg.md  .cfg.default
Documentation/Tests/       test-protocol.md  test-todo-later.md  ANALYSE_ECRAN_NOIR_MENU.md
Documentation/DLL/         (inchange)
```

Tout est passe par `git mv`, donc l'historique de chaque fichier suit.

### Ce qui a rendu le deplacement de `Blaze.c` peu couteux

Sa taille (3,1 Mo) et ses **371 mentions** dans le depot le faisaient paraitre intouchable — c'est
d'ailleurs ce qu'affirmait la proposition `recover-blaze-data-section` : « relocating it is out of
scope ».

Mesure faite avant d'agir : **les 371 mentions sont toutes en prose** (« `Blaze.c` 74824 »), aucune
n'est un chemin. Et **aucun outil ne l'ouvre** — ni `playtest.ps1`, ni `compare-bodies.ps1`, ni
`split-manifest.awk`. Le seul endroit qui portait un chemin etait `OpenForesia.sln`.

**Une taille de fichier n'est pas une mesure de couplage.** Ce qui compte est le nombre de
references *par chemin*, et ici il valait un.

### `Legacy/Decompiled` etait deja le nom prevu

Le `.sln` rangeait `Blaze.c` sous un dossier virtuel `Legacy/Decompiled` **tout en le laissant
physiquement a la racine** — l'ecart etait explicitement note dans
`openspec/changes/recover-blaze-data-section/proposal.md`. Le disque coincide desormais avec la
solution, et le nom n'a pas eu a etre invente.

### Trois references cassees trouvees en chemin, anterieures a ce rangement

Le controle « chaque chemin cite existe-t-il ? » a leve trois erreurs qui n'avaient rien a voir
avec ce changement :

* les trois items `Legacy\Raw\FUN_*.txt` du `.sln` pointaient a cote — ces fichiers avaient
  emmenage dans `Legacy/Raw/Blaze/` sans que la solution suive ;
* les memes chemins etaient perimes dans `REVERSE_STATUS.md` et deux changements openspec ;
* `winsys3d.md` a la racine etait documente comme « ancienne version remplacee » depuis un moment.
  Supprime — git le garde.

**Un rangement est une occasion de verifier les chemins, pas seulement de les changer.** Le controle
tient en une ligne et vaut d'etre refait apres tout deplacement :

```bash
grep -rhoE "(Documentation|Legacy|OpenForesia|openspec)[A-Za-z0-9_/.-]*\.(md|exe|dll|cfg|txt)" \
  --include=*.md . | sort -u | while IFS= read -r p; do [ -e "$p" ] || echo "MANQUANT $p"; done
```

### La portee a debordee sur le code, et c'etait justifie

La demande portait sur les liens des `.md`. Mais l'ancien chemin du binaire apparaissait aussi dans **47
fichiers** dont des commentaires et deux **messages d'exception** C#. Un message d'execution qui
nomme un fichier inexistant est un defaut du message, pas de la documentation : les deux ont ete
corriges (`BuildTextTable`, `InstallEntityVTables`).

Consequence assumee : l'oracle a signale ces deux corps, a juste titre. Verifie ligne a ligne — ce
sont bien les seuls, et seul le chemin change dans la chaine. Reference refigee a **513 corps**.

### Verification

Build 0 erreur · oracle « aucune logique modifiee » apres refreeze · tests de disposition 5 verts ·
`-Entry 1` **382,3** primitives par image, capture relue · **tous les items de solution du `.sln`
resolvent** et l'outil PE fonctionne sur les nouveaux chemins
(`at Legacy/Binaries/Blaze.exe 0x005631f8`).

---

## 2026-08-21 — le verrou `FUN_004747F0` est leve, et le depot sait desassembler

**`OPENFORESIA_SKIP_AREA_HANDLER` est retire.** L'etat 3 traverse desormais la transition de
sous-zone sans aucun echafaudage, et s'arrete sur `FUN_005350b0` — le meme point qu'avec
l'echafaudage, mais cette fois honnetement.

### Ce qui a change la donne : `OpenForesia.Tools disasm`

Le blocage tenait a une phrase ecrite trois jours plus tot : « c'est une demande de re-passe Ghidra
ciblee, pas un exercice de lecture ». Elle designait une action **hors du depot**, que personne ne
pouvait faire depuis une session de travail — donc le verrou etait permanent en pratique.

Or le binaire est dans le depot, la table des sections aussi. **Il ne manquait qu'un decodeur.**
`Iced` (NuGet, 1.21.0) est ajoute a `OpenForesia.Tools`, et le verbe `disasm` rend le
desassemblage x86-32 a une VA Ghidra, avec les cibles de saut internes relevees en premiere passe :

```
dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe 0x004747f0 724
```

Il ne remplace pas Ghidra — pas de flot reconstruit, pas de types, pas de noms. Il rend ce qui
suffit a transcrire a la main quand la pseudo-C manque. **Toute future « re-passe Ghidra requise »
est desormais un travail interne.**

### L'estimation qui avait fait renoncer etait fausse

La note disait : « 723 octets avec neuf branches convergentes et une logique de drapeaux dense,
c'est le travail d'un decompilateur ».

Le desassemblage montre l'inverse : une **cascade parfaitement reguliere**. Huit blocs identiques au
bit pres, chacun « si telle condition, poser un code de destination et sortir ». Les « neuf sorties
convergentes » sont neuf `return`, pas un graphe emmele. La transcription tient en 60 lignes.

**Une taille en octets ne mesure pas une difficulte.** Ce qui la mesure, c'est le nombre de formes
distinctes — ici deux. C'est la meme lecon que « la taille d'un fichier ne dit rien de son cout de
conversion, sa cloture si », rencontree a l'etape C.

### Ce que fait `FUN_004747F0`

Elle choisit la sous-zone suivante de la zone 0 selon le nombre de capacites que le groupe possede
en commun : elle compte les bits leves de `DAT_005e9000` (le masque de groupe, recalcule par
`RefreshPartyAbilityMask`) plus le bit 0 de `DAT_005e9001`, et pose un code dans `DAT_005de52c` —
0, 8, 0x10, 0x18, 0x20, 0x28, 0x30, 0x38, 0x40, 0x48 ou 0x50.

Deux verifications qui evitent du faux travail :

* **`FUN_00401FA0`, appele par chaque branche, est un `ret` nu** — desassemble, pas suppose. Les
  neuf appels sont donc sans effet et ne sont pas transcrits. C'est coherent avec la table de sauts,
  que `BuildAreaHandlerTable` remplit deja de cette adresse.
* **La garde de sortie precoce est le point important** : si `DAT_005de52c` ou `DAT_005de530` est
  deja non nul, la fonction ne fait rien. Une transition en cours n'est jamais ecrasee.

Releve conserve dans `Legacy/Raw/Blaze/FUN_004747F0.asm`.

### La chaine qu'elle a tiree derriere elle

`FUN_004747F0` appelle `FUN_00514000`, qui delegue a `FUN_00515d70`, qui appelle `FUN_00514040` et
`FUN_00535710`. Les trois premieres sont dans `Blaze.c` ; la quatriere etait deja reconstruite
(`RefreshPartyAbilityMask`).

* **`FUN_00514000` et `FUN_00515d70` sont fondues** en `ApplyPartyScriptOp`. La premiere ne fait que
  passer `DAT_005e97cc` et 0 a la seconde, et **ces deux parametres n'y sont jamais lus** : les
  garder aurait donne une signature qui ment sur ce que la fonction touche.
* **`FUN_00514040` est l'ALU du moteur de script** — 190 opcodes, trois groupes de largeur
  (octet / mot / double mot) et six familles de douze dans chacun. Transcrite **integralement**,
  cas par cas, dans l'ordre de Ghidra.

**Pourquoi ne pas l'avoir repliee en boucle calculee**, alors que la structure y invite : les cas ne
sont pas interchangeables. Ghidra emet des largeurs et des signes differents d'un cas a l'autre —
`0x01` additionne en `char` signe la ou `0x07` divise en `byte` non signe, `0x13` passe par un
64 bits la ou `0x07` reste en 32. Replier aurait ete une re-derivation, pas une transcription, et
la divergence n'aurait sonne que le jour ou l'opcode concerne serait tombe.

### Ou vit l'ALU, et quand elle demenagera

Elle reste dans `Screens/State03/WorldMap.cs`, son seul appelant reconstruit — regle 4
d'`ARCHITECTURE.md`. Mais **le binaire l'appelle depuis 17 fonctions distinctes**, sur deux bandes
d'adresses (`0x00515xxx` et `0x00523xxx`) : c'est une primitive partagee. Le jour ou un second
appelant est reconstruit, elle part dans `Engine/` avec `ApplyPartyScriptOp`. La condition est
ecrite sur place.

### Verification

Build 0 erreur · oracle : **un seul corps signale**, `CallAreaHandler`, le rebranchement voulu ·
tests de disposition 5 verts · **la reproduction tourne sans `OPENFORESIA_SKIP_AREA_HANDLER`** et
s'arrete sur `FUN_005350b0`, l'etape suivante.

**Un signal de plus, non cherche** : les avertissements `CS0414` passent de 38 a 34.
`s_Unknown_00566c90` — un drapeau que `DebugParty` ecrivait sans qu'aucun code ne le lise — est
maintenant consomme par le gestionnaire de zone, exactement comme dans le binaire. **Un champ
« assigne mais jamais utilise » est souvent le bout pendant d'une fonction non encore transcrite**,
pas du code mort.

---

## 2026-08-21 — `FUN_005350b0` : `PlaceWorldMapParty` est complete de bout en bout

Transcrite sous le nom **`SpawnSubAreaEntities`**. Le front passe a `FUN_00516fc0`, la logique par
image — la derniere piece avant que la carte du monde dessine quelque chose.

### Le nom precedent etait faux, et il faisait croire a autre chose

Le stub s'appelait `ApplyAreaPalette` et son message parlait de « mise en place de palette de zone ».
Le corps ne touche a aucune palette : il **peuple la sous-zone**. Il parcourt une liste
d'enregistrements de 0x20 octets (prise dans le repertoire de zone, table `+0x30`, indexee par la
sous-zone), en derive une entite de 0x9c octets par enregistrement, et pose chacune sur le terrain
via `TryPlaceEntity` puis `FinalizeEntityPlacement2`.

Le nom datait du moment ou seule la **taille** de la fonction etait connue. Renomme des que le corps
a ete lu — c'est ce que `Strategy.md` demande, et c'est le second nom trompeur corrige ce jour-la
apres les quatre `.data` dont le nom taisait l'adresse.

### Un defaut probable de l'original, transcrit tel quel

L'ecriture finale du compte d'entites est **hors du bloc garde**, alors que la variable qui la porte
n'est initialisee qu'a l'interieur :

```c
if ((puVar9 != 0) && (param_1 != 0xff)) {
   param_1 = DAT_005e9ae8;          // le compte
   for (...) { ...; param_1 = param_1 + 1; }
}
DAT_005e9ae8 = param_1;             // <- hors du if
```

Si la liste d'apparitions est nulle, ou si la sous-zone vaut `0xff`, le compte d'entites recoit donc
l'**index de sous-zone**. Non corrige : le projet reproduit les defauts de 1998. Marque
`TODO(REVERSE)` sur place, et a verser dans `legacy-bug.md` le jour ou on l'observe en jeu — on ne
sait pas encore si le cas est atteignable.

### Ce que ce pas a vraiment exerce

`SpawnSubAreaEntities` fait tourner `TryPlaceEntity` **et** `FinalizeEntityPlacement2` sur jusqu'a
**128 entites**, pas seulement sur les quatre du groupe. Les deux fonctions ecrites plus tot dans la
journee — dont celle que Ghidra n'avait pas decompilee — sont donc bien plus eprouvees qu'a leur
premiere verification, et rien ne faute.

### La mesure de `FUN_00516fc0`, faite avant de s'y engager

`TODO.md` impose de mesurer les dependances manquantes avant d'attaquer une grosse fonction. Fait :
**1225 lignes pour elle-meme, et ~5900 lignes d'appelees directes non ecrites**, dont
`FUN_0050bf20` (999), `FUN_0051aa30` (591), `FUN_0051fa40` (438), `FUN_0051e700` (420),
`FUN_0051c420` (371), `FUN_004011c0` (360), `FUN_00521000` (362), `FUN_00522140` (312).

C'est un chantier a part entiere, pas la suite naturelle d'une seance. **Mesurer a evite de le
commencer par le milieu.**

### Deux « fonctions absentes de Blaze.c » qui n'en etaient pas

La mesure signalait `FUN_0056aff0` et `FUN_0056afe8` comme absentes du fichier, donc candidates au
desassemblage. Trente secondes de `at` ont montre que ce ne sont **pas des fonctions** : ce sont les
entrees 8 et 10 de la table de sauts `s_Vt_0056afc8`, toutes deux garnies de `0x00401FA0` — le `ret`
nu deja verifie. Ghidra les rend `(*(code *)PTR_FUN_0056afe8)()`, un appel indirect, et le prefixe
`FUN_` d'un `PTR_FUN_` trompe le comptage.

**Un `FUN_xxxxxxxx` absent de `Blaze.c` n'est pas forcement du code manquant** : verifier a quoi
l'adresse correspond avant de conclure. Ici la table est deja modelisee par `CallEntityHandler`.

### Verification

Build 0 erreur, avertissements inchanges · oracle : **deux corps signales**, tous deux voulus
(`CallAreaHandler` rebranche, et le renommage au site d'appel dans `PlaceWorldMapParty`), plus une
methode « absente » qui est l'ancien nom du stub · tests de disposition 5 verts · la reproduction
tourne sans echafaudage et s'arrete sur `RunWorldMapTick`.

---

## 2026-08-21 — `FUN_00516fc0` : la clé de lecture, et une dépendance que la mesure avait ratée

**Aucune ligne de `FUN_00516fc0` n'est transcrite, et c'est délibéré.** Ce qui a été fait est ce
sans quoi la transcription aurait été fausse : établir comment lire sa pseudo-C, et refaire sa
mesure de dépendances — la précédente en manquait une famille entière.

### La clé de lecture : `piStack_74 = X;` avant un appel est un ARGUMENT PERDU

Ghidra rend le corps ainsi :

```c
piStack_74 = (int *)(DAT_005de048 & 0xffff);
FUN_00513dd0(DAT_005e97cc, &DAT_005e93c0 + uVar24 * 0x27);
```

Ça se lit naturellement comme « affecter une variable locale, puis appeler avec deux arguments ».
**C'est faux.** Le désassemblage du site (`0x005170d5`) :

```
push eax        ; DAT_005de048 & 0xffff
push esi        ; l'emplacement d'entite
push ecx        ; DAT_005e97cc
call 00513DD0h
add esp, 0Ch    ; trois dwords, cdecl
```

Trois arguments. `piStack_74` est la fente de pile où Ghidra a vu le troisième `push` atterrir, et
il l'a modélisée comme une variable. Le motif revient partout dans cette fonction —
`piStack_74 = (int *)0x190; Ordinal_379();` est en réalité `Ordinal_379(0x190)`.

**Une transcription faite sur la pseudo-C seule aurait perdu un argument par appel concerné, en
silence.** Le contrôle tient en une commande, et il faut le faire pour chaque appel précédé d'une
affectation à `piStack_74` :

```powershell
dotnet run --project OpenForesia.Tools -- disasm Legacy/Binaries/Blaze.exe <va du site> 60
```

Même famille de défaut que les `unaff_EBX` / `unaff_ESI` / `unaff_EBP` / `unaff_EDI` que la fonction
déclare (60 lignes concernées) : ce sont les **curseurs de primitives** que Ghidra n'a pas su
rattacher. Le motif `curseur = Draw(otEntry, curseur, ...)` les identifie sans ambiguïté, et
`local_60` est l'entrée d'ordering table — `FUN_0052a1e0` est déjà connu du projet comme un cas
d'arité mal récupérée (`FrameBegin` prend **trois** paramètres, Ghidra en montre deux).

### Une dépendance entière que la mesure précédente ne voyait pas

Le relevé de la veille donnait « ~5900 lignes d'appelées directes ». Il comptait les `FUN_` cités
dans le texte — donc il ratait tout ce qui passe par une **table de pointeurs**.

Le prologue appelle `FUN_00513dd0` → `FUN_00513e40`, qui dispatche sur
`(&PTR_LAB_00563298)[opcode]` : **l'interpréteur de script**. C'est une famille de fonctions, pas
une ligne.

Mesure faite, table lue dans le binaire :

* la table a **39 entrées** — elle s'arrête à `+0x98`, le dword suivant est nul ;
* **19 des cibles sont dans `Blaze.c`** (274 lignes au total) ;
* **20 n'y sont pas.** Ghidra a nommé la table `PTR_LAB_`, donc il n'a pas reconnu ces corps comme
  des fonctions du tout.

**Et ces 20 sont minuscules** : 8 à 17 instructions chacune. Exemple complet, `FUN_005154c0` :

```
mov eax,[esp+0Ch]      ; le pointeur de script
mov edx,[esp+4]
xor ecx,ecx
mov cl,[eax+1]         ; index sur deux octets
mov ch,[eax+2]
mov eax,[edx+8Ch]      ; la table de scripts de l'objet
mov eax,[eax+ecx*4]
ret                    ; rend le nouveau pointeur de script
```

Les vingt désassemblages sont conservés dans `Legacy/Raw/Blaze/ScriptOps/`.

**Le « trou » est donc bien plus petit qu'il n'en avait l'air** — ~240 instructions au total — mais
il n'existait pas du tout dans l'estimation précédente. C'est le prix d'une mesure qui compte les
noms cités plutôt que les appels réels.

### Pourquoi la transcription n'a pas été commencée

`TODO.md` dit, de ma propre main la veille : « ne pas la commencer par le milieu ; descendre par les
petites appelées, en exerçant à chaque fois ». Le problème est que **rien ne s'exerce tant que
`FUN_00516fc0` elle-même n'existe pas** : ses appelées ne sont atteignables que par elle.

Écrire 1225 lignes d'un coup, sur une pseudo-C dont on vient d'établir qu'elle perd des arguments,
sans pouvoir en exercer une seule ligne, aurait produit exactement le genre de transcription
plausible et fausse que ce projet passe son temps à rattraper. La règle du dépôt est explicite :
« L'écrire à moitié aurait été pire que ne pas l'écrire. »

Ce qui est livré à la place est ce qui rend la prochaine passe sûre : la clé de lecture, la mesure
corrigée, et les vingt désassemblages banqués.

---

## 2026-08-21 — étape (1) du plan « atteindre le prologue » : l'arène des paramètres de vue

Faite, mais **pas comme le plan l'annonçait** — et l'écart valait la mesure.

### Ce que le plan disait, et pourquoi c'était faux

« Étendre `EntityState` vers le bas sur `0x005710f0..0x005714ff` ; `Blaze.c` y cite 231 adresses. »
Le chiffre était juste, la conclusion non : **trois quarts de cette région n'appartiennent pas au
jeu.**

* **`0x005710f0..0x00571258` est du CRT.** `FUN_0054B190` lit `DAT_00571258` comme argument de
  `WideCharToMultiByte`, et `DAT_005710F0` est la table de pages de code qui va avec — parcourue
  **par paires de dwords** jusqu'à `&DAT_00571258`, ce que le code de la boucle dit littéralement.
  L'englober dans une arène d'état de jeu aurait mélangé deux propriétaires.
* ~~**`0x00571400..0x00571500` est une table initialisée** de valeurs `0x00208xxx`~~ — **FAUX,
  rétracté le même jour.** Cette table n'existe pas : c'était un défaut de l'outil `at`. Voir
  « L'outil `at` rendait les octets d'une autre section » plus bas. Ce qui subsiste est que
  `0x00571400` est la **fin du contenu de `.data` présent dans le fichier**, donc une vraie borne.
* `0x0057125c..0x0057126c` n'est cité nulle part.

Le bloc réel est **`0x00571270..0x00571400`** : 0x190 octets, **69 symboles**, et la première
adresse du jeu est `0x00571270` parce que `FUN_004011c0` l'ajoute à `s_ViewBlock_006067c0`.

### Les deux bornes sont déduites, pas choisies

* borne basse — la dernière adresse CRT (`0x00571258`) puis un trou non cité ;
* borne haute — `0x00571400`, qui est la **fin du contenu de `.data` présent dans le fichier**
  (`PointerToRawData 0x0014D600 + SizeOfRawData 0x22400 = 0x0016FA00`). Le chiffre est le bon ;
  la justification d'origine (« le premier dword non nul de la table ») ne l'était pas ;
* et le bloc entre les deux est **intégralement nul au chargement** : 384 octets relevés en trois
  passes, zéro dword non nul. Une `AllocZeroed` est donc fidèle, sans valeur à semer.

### Neutre en comportement, et vérifié comme tel

**Aucune des 123 adresses de la région n'était un champ C#.** Il n'y avait donc rien à aliaser :
créer l'arène ne peut pas changer ce que le code fait aujourd'hui. C'est le contrôle qui manquait
au rapatriement du son, et il a été fait avant d'écrire cette fois.

Créée sous `s_ViewParams_00571270` / `ViewParams(va)`. Le nom dit ce qui est observé et pas plus :
le bloc alimente `s_ViewBlock_006067c0` par addition, reçoit la caméra (`0x005de068/06c/070`) en
deux exemplaires parallèles, et porte des bits de drapeau à `0x0057139c`. Le détail de ses 69 champs
n'est pas tracé.

### La leçon, qui est la même que les deux précédentes

**Un compte d'adresses ne dit pas à qui elles appartiennent.** « 231 adresses dans 0x00571xxx » est
un fait ; « donc c'est une région d'état de jeu » est une inférence, et elle était fausse. Ce qui a
tranché, c'est de regarder **qui lit chaque adresse** (une ligne de `grep` par borne) et **ce que le
binaire y contient** (une commande `at` par borne).

Troisième estimation démentie par la vérification en deux jours, après « `Resources.cs` porte
`ToAnsiZ` » et « 18 emprunts à `Engine/` ».

### Vérification

Build 0 erreur · avertissements inchangés · **les 5 tests de disposition restent verts** — c'était
le contrôle attendu de cette étape, puisqu'ils signalent précisément les recouvrements d'arène ·
oracle sans changement inattendu · non-régression `-Entry 1` à **382,3** primitives par image.

---

## 2026-08-21 — l'outil `at` rendait les octets d'une autre section

**Le pire genre de défaut d'outillage : il ne plantait pas, il répondait.**

`RvaToFileOffset` (`OpenForesia.Tools/Program.cs`) convertissait une RVA en offset fichier par
`PointerToRawData + (rva - VirtualAddress)`, **sans jamais la borner à la `SizeOfRawData` de la
section**. Or la `VirtualSize` d'une section peut largement dépasser sa taille brute : `.data` de
`Blaze.exe` occupe `0xB8958` octets en mémoire pour `0x22400` dans le fichier. Toute adresse dans
cette queue recevait donc un offset — qui tombait droit dans les octets bruts de la section
**suivante**.

Le garde-fou existant ne rattrapait rien : il ne testait que `offset >= file.Length`, et cet
offset-là est parfaitement dans le fichier.

### Ce que ça a coûté

`at Blaze.exe 0x00571400` rendait une table dense et crédible de valeurs `0x00208xxx`. C'était
`.idata` — dont la RVA est justement `0x00208000`. Trois notes du dépôt, écrites **le même jour**,
en ont tiré une « table initialisée à `0x00571400..0x00571500` » et s'en sont servi pour justifier
la borne haute de `s_ViewParams_00571270` (`DataSymbols.md`, `TODO.md`, et le bloc de commentaire de
`GameMemory.cs`). Toutes trois sont rétractées.

Le défaut a été trouvé en essayant d'étendre `s_EntityState_00571500` vers le haut : le dump
prétendait que l'arène **déjà en place** recouvrait une table initialisée, ce qui aurait été un
défaut sérieux et ancien. La contradiction — une arène validée par les tests de disposition posée
sur des données du fichier — est ce qui a mené à l'outil plutôt qu'au code.

### La correction, et la frontière qui reste

`RvaToFileOffset` rend maintenant `-1` quand `rva - VirtualAddress >= SizeOfRawData`, ce qui fait
dire à l'appelant ce qui est vrai : *données non initialisées, zéro au chargement*.

Ce qu'il faut retenir de `.data` :

| | valeur |
|---|---|
| VA de début | `0x0054F000` |
| offset fichier | `0x0014D600` |
| taille brute | `0x22400` |
| **dernière VA présente dans le fichier** | **`0x00571400`** |
| VA de fin (mémoire) | `0x00607958` |

**Au-dessus de `0x00571400`, aucune valeur de `.data` ne vient de la section.** Cela couvre
l'essentiel des `DAT_005e…`, `DAT_005f…` et `DAT_006…` du jeu : ils sont tous à zéro au chargement
et écrits par le code. Un `at` qui répond « non initialisé » sur l'un d'eux est donc la réponse
normale, pas un échec.

Le chiffre `0x00571400` restait par ailleurs la bonne borne haute pour `s_ViewParams_00571270` —
mais parce que c'est cette frontière-là, pas parce qu'une table s'y trouvait.

### Leçon

Un dump qui rend des octets plausibles ne prouve pas qu'ils appartiennent à l'adresse demandée.
Avant de tirer une conclusion d'un relevé situé haut dans `.data`, comparer l'`Offset` affiché aux
bornes de section que donne `sections`. C'est le pendant exact de la leçon déjà inscrite pour le
jeu : *vérifier contre la référence, pas contre un nombre qui a l'air juste*.

---

## 2026-08-21 — étape (2) : l'interpréteur de script

`OpenForesia/src/Blaze/Screens/State03/WorldMapScript.cs`, ~1 000 lignes. La machine est complète :
boucle, répartiteur, **22 opcodes sur 39** et huit appelées. Le jeu va maintenant jusqu'à l'état 3
et s'arrête **exactement sur `FUN_00516fc0`**, c'est-à-dire l'étape (4).

### Les opcodes 1 et 2 étaient la pièce manquante de la boucle

Aucun des deux n'est dans `Blaze.c` — Ghidra a nommé la table `PTR_LAB_00563298` et n'a donc pas
reconnu ces corps comme des fonctions. Relevés au désassembleur, ils font six et neuf instructions :

* **opcode 1** — saut inconditionnel vers `scripts[ushort(script[1], script[2])]` ;
* **opcode 2** — saut conditionnel, **et il remet le résultat à zéro**.

C'est ce `mov dword [ecx],0` qui explique la condition d'arrêt de `RunScript`, écrite plus tôt sans
qu'on sache pourquoi elle traitait l'opcode 2 à part : tous les autres opcodes arrêtent la boucle
dès qu'ils posent un résultat, celui-ci est le seul à la reprendre. Sans lui, un test de script
arrêterait le script pour de bon.

### Trois arités de Ghidra démenties au niveau machine

Le comptage se lit sur l'`add esp,N` qui suit l'appel (cdecl, N/4 arguments). Chaque fois, la
pseudo-C disait autre chose :

| fonction | Ghidra | réel | ce que ça change |
|---|---|---|---|
| `FUN_0053a920` | 3 arguments | **2** (`add esp,8`) | le `push 5312D0h` n'était pas le sien |
| `FUN_00530f20` | 2 arguments | **3** (`add esp,0Ch`) | …il appartenait à celle-ci — un pointeur de fonction |
| `FUN_00532740` | 1 argument | **2 poussés, 1 lu** | la capacité passée en second est morte |

Le cas `FUN_00530f20` est le plus instructif : l'argument perdu par Ghidra était poussé **avant**
l'appel intermédiaire et récupéré après, une forme que la décompilation ne sait pas rendre. C'est le
même défaut que le `piStack_74 = X` de `FUN_00516fc0`, sous un autre déguisement.

### Un défaut d'aliasing évité de justesse

`FreezeAllEntities` (`FUN_00535300`) balaie sept tables d'entités, dont la première a un compte
variable : `DAT_005e9020`. Écrit d'abord `*(int*)Mem.Arena(0x005e9020)` — ce qui **aurait rendu
zéro en silence** et fait sauter le gel du groupe entier, parce que cette adresse est doublée par
`s_PartySize_005e9020`, un `static int` ordinaire déclaré dans `Screens/State04/SaveSlots.cs`, hors
de l'arène.

Les cinq tests de disposition **ne l'auraient pas vu** : ils comparent des symboles à suffixe de VA
entre eux, pas un champ simple à une étendue d'arène. C'est exactement le manque déjà noté « étendre
le test de disposition aux champs de valeur simples » — il vient de coûter une vérification
manuelle, et il en coûtera d'autres.

À l'inverse, `0x005e93c0` est **sans danger** : `Inp.s_SlotFlags_005e93c0` n'est pas une allocation
propre mais une vue (`mem.Arena(0x005e9180) + 0x240`). La différence entre les deux cas ne se voit
qu'en lisant la déclaration ; l'adjacence ne suffit pas à conclure.

### Deux opcodes bloqués pour des raisons établies

Le message d'exception les porte, pour ne pas refaire l'enquête :

* **opcode 0** — appelle `(&PTR_FUN_0056aec8)[script[1]]`. Les 256 entrées du binaire pointent
  toutes vers `FUN_00401fa0`, un `ret` nu. **Ce n'est que le contenu par défaut** : `Blaze.c`
  l. 75143 remplit la table à l'exécution, 0x40 entrées à la fois, depuis
  `(&PTR_PTR_00567318)[zone]` — et quatre autres tables voisines avec elle. Il faut ce chargeur et
  les jeux de fonctions par zone. *A failli être transcrit comme « appelle un no-op »* : la lecture
  du binaire seule donnait cette réponse, et elle était fausse.
* **opcode 27** — laissé de côté sciemment. Cascade d'affichage des nombres de dégâts
  (`FUN_00530f20`, `FUN_005312d0` et cinq sous-appelées), hors du chemin vers le prologue.

### Deux corrections d'interop

* **`Winsys3d.Ordinal_45` était déclaré avec deux arguments.** Son corps est
  `xor eax,eax ; ret 4` : le `ret 4` dit **un seul** argument, en stdcall. Deux laissaient quatre
  octets sur la pile à chaque appel. Corrigé, ainsi que le site d'appel de `RunMainLoop`.
* **`Ordinal_46` ajouté** : `xor eax,eax ; ret`, cdecl sans argument. Il rend donc toujours 0, ce
  qui rend le test `Ordinal_46() < 0` de `FUN_00513dd0` toujours faux dans cette version de la DLL.

### Reste à faire sur l'interpréteur

Quinze opcodes (14, 15, 17, 20, 21, 22, 30–38) n'attendent qu'une reprise du désassemblage. Les
relevés banqués de `Legacy/Raw/Blaze/ScriptOps/` sont **des fenêtres de 96 octets** et certains
corps débordent — reprendre au verbe `disasm` avec une taille plus large, sinon le dernier
mnémonique lu est un `(bad)`.

### Contrôles

Build 0 erreur · **les 5 tests de disposition restent verts**, y compris après l'extension de
`s_EntityState_00571500` de 0x500 à 0x538 · oracle refigé à 550 corps, les deux seuls changements de
logique étant les corrections faites sciemment · non-régression en jeu : même frontière
`FUN_00516fc0`, atteinte trois fois de suite.

---

## 2026-08-21 — étapes (3) et (4) : `FUN_00516fc0` ne lève plus

Le prologue de `FUN_00516fc0` est transcrit, et avec lui les deux fonctions de l'étape (3)
(`SnapshotViewVector` / `PublishFrameView`, ex-`FUN_00401000` / `FUN_00401080`). L'exception qui
bloquait la carte du monde depuis le début a disparu ; le jeu exécute maintenant le prologue, lance
l'interpréteur de script, et s'arrête plus loin.

### La clé `piStack_74` est confirmée, et elle a une exception

Écrit contre `Legacy/Raw/Blaze/FUN_00516FC0-prologue.asm` plutôt que contre la pseudo-C. La règle,
vérifiée au niveau machine :

> `piStack_74 = X;` avant un appel est le **dernier argument**, celui que Ghidra a perdu.

`FUN_0052a1e0` le montre sans ambiguïté : `add esp,0Ch` après l'appel, trois `push`, et la pseudo-C
n'en montre que deux. Le troisième est bien `piStack_74`. *Le même défaut avait déjà été trouvé et
documenté sur cette fonction en la transcrivant, dans l'autre sens.*

**L'exception** : quand la valeur posée est une **adresse de code** (`0x5170bb`, `0x5170d0`), ce
n'est pas un argument mais l'adresse de retour du `call`, et la fonction ne prend rien. Les deux
appels concernés — `FUN_00401080` et `FUN_00401000` — n'ont effectivement aucun paramètre. Sans
cette distinction on leur en inventait un.

### Ce que le prologue fait

Instantané de mise au point, ouverture d'image (`FrameBegin`), remise à zéro d'une dizaine de mots
et de **deux pages de 4 Kio** dans MainMem (`+0x1c5000` et `+0x1c4000`), changement de musique si la
zone a changé, puis le pas de boucle `DAT_005645f8` — qui vaut **2** au chargement, donc la première
image publie la vue et lance **deux** scripts —, et enfin la purge de la liste d'attente.

Cette purge répond à une question laissée ouverte le matin même : **c'est elle qui consomme la pile
que l'opcode 11 remplit** (`0x005fa560`). Le `TODO(REVERSE)` posé sur `ScriptOpPushPending` a donc
son consommateur, trouvé sans le chercher.

### La frontière suivante n'était pas celle qu'on avait annoncée

L'interpréteur bute sur l'opcode 0, l'appel de verbe de zone. La note écrite le matin disait :

> « Transcrire cet opcode demande d'abord ce chargeur et les jeux de fonctions par zone. »

**C'était trop pessimiste, et vérifiable en trente secondes.** Le chargeur existait déjà
(`InstallEntityVTables`, `Engine/Resources.cs`), et la table de l'index 0 était relevée dans le
dépôt depuis longtemps sous `s_VtSource_0056aec8`. Il ne manquait que le répartiteur, écrit sur le
patron exact de `CallAreaHandler` : dispatch sur la **valeur d'adresse**, souche vérifiée
court-circuitée, tout le reste lève en se nommant.

La leçon est symétrique de celle du matin sur l'outil `at` : une note du dépôt — y compris une note
qu'on vient d'écrire soi-même — n'est pas une preuve. Celle-ci a été écrite en lisant le binaire
(les 256 entrées y sont bien des souches) sans vérifier ce que le port contenait déjà.

### Un reste de registre, et pourquoi il est transcriptible

L'opcode 0 fait `xor eax,eax ; mov al,[esi+1]` pour construire l'indice, puis
`call dword [eax*4+56AEC8h]` et `mov [ecx],eax`. Ni le `call` ni un corps `ret` nu ne touchent
`EAX` : quand le verbe est la souche `FUN_00401FA0`, **le résultat de condition reçoit l'indice du
verbe lui-même**. Ce n'est pas une reconstruction hasardeuse mais ce que le processeur fait, et la
boucle s'arrête ou non selon que cet indice est nul. Transcrit comme tel, avec le raisonnement écrit
sur place.

### Le verbe 3, premier de la chaîne

`FUN_00442F60`, 336 octets, **absente de `Blaze.c`** — désassemblée, dump conservé dans
`Legacy/Raw/Blaze/AreaVerbs/`. Deux moitiés : un décalage d'un cran des bits du banc `0x005e9000`
vers `0x005e9840` (celui de l'opcode 7), puis le choix d'une piste musicale d'après `0x005e96fe`.

Deux formes sans branchement rendues par ce qu'elles calculent, pas par leur forme :
`setne / dec / and 2` est l'identité bornée à {0,1,2}, et onze `cmp al,N ; je` successifs testent
« multiple de 8 jusqu'à 0x50 ».

Le mur est maintenant le **verbe 10** (`FUN_004441F0`, également absent de `Blaze.c`). Le travail
est devenu mécanique : le jeu nomme un verbe, on le transcrit, on relance.

### Deux places mémoire, et un défaut latent trouvé au passage

* `CameraBlock` étendu de `0x80` à `0x84` — le prologue écrit `DAT_005de0b0`, qui tombait
  exactement sur l'ancienne borne. Rien ne suit jusqu'à `ItemMenuArena`.
* `DAT_005de024` et `DAT_005de028` ajoutés à côté de `s_WorldFlags_005de02c`.

**Défaut d'aliasing latent, à résorber** : `s_Unknown_005de034`
(`Screens/State04/CharacterCreation.cs`) est un `static int` ordinaire dont l'adresse tombe **dans**
`CameraBlock`. Il ne fait pas de dégât aujourd'hui — aucun site n'écrit `Camera(0x005de034)` — mais
le premier qui le fera lira une autre mémoire. C'est le **deuxième** cas du même genre trouvé dans
la journée, après `s_PartySize_005e9020`, et les deux fois par lecture manuelle : les tests de
disposition ne comparent pas un champ de valeur à une étendue d'arène. **Étendre le test est
maintenant la correction la plus rentable du dépôt.**

### Contrôles

Build 0 erreur · 5 tests de disposition verts · oracle refigé à 554 corps, les deux seuls changements
de logique étant ceux faits sciemment · en jeu, la frontière a reculé **trois fois** en une session :
`FUN_00516fc0` → opcode 0 → verbe 3 → verbe 10.

---

## 2026-08-21 — la chaîne de verbes est bouclée : le prologue s'exécute en entier

Trois maillons ont suffi, pas la quinzaine annoncée :

| maillon | verdict |
|---|---|
| verbe 3 = `FUN_00442F60` | 336 o., absente de `Blaze.c`, transcrite |
| verbe 10 = `FUN_004441F0` | **souche** `xor eax,eax ; ret` |
| opcode 14 = `0x005158f0` | absent de `Blaze.c`, transcrit |

Les **deux** lancements de script du prologue vont maintenant au bout, et l'exécution retombe sur la
lacune posée en fin de prologue : la boucle par image. C'est le résultat attendu.

### Deux souches, deux comportements — la distinction compte

`FUN_00401FA0` est un `ret` **nu** : il ne touche pas `EAX`, qui contient encore l'indice du verbe au
moment du `mov [ecx],eax`. `FUN_004441F0` est `xor eax,eax ; ret` : il rend **0**.

Les confondre — « ce sont deux fonctions vides » — aurait donné un résultat de condition faux dans un
cas sur deux, et donc un script qui s'arrête là où il devrait continuer. La différence ne se voit pas
dans une décompilation : `Blaze.c` rendrait les deux par `return;` ou `return 0;` sans que la
distinction porte à conséquence. Elle ne se voit qu'au niveau machine, parce que l'appelant lit un
registre que le callee n'a pas écrit.

Un balayage des seize adresses distinctes de `s_VtSource_0056aec8` a montré que **seule**
`0x004441F0` est une souche : les quinze autres sont de vraies fonctions. Le balayage a coûté une
minute et a évité de le redécouvrir une par une.

### Le choix du locuteur de l'opcode 14 se lit à l'envers

L'entité courante n'est remplacée **que si elle est déjà un locuteur valide**. Nulle, ou d'un genre
autre que `0x800000`, elle est gardée telle quelle ; quand elle l'est, la fonction va chercher le
premier membre du groupe disponible et le prend à sa place. C'est ce que disent les trois
`jne/je 00515945` du prologue, et c'est le contraire de ce qu'on écrit spontanément.

### Deux corrections dans du code existant

* **`BindMenuLabels` avait un paramètre mal nommé.** Son deuxième argument s'appelait `flags` parce
  que le seul site connu y passait 0. L'opcode 14 et `FUN_00530f20` y passent tous deux un
  **pointeur d'entité** — le locuteur auquel la boîte est attachée. Renommé `subject`.
* **Une lacune de `BindMenuLabels` est comblée.** Sa branche `if (*labels == 8)` levait une
  `NotImplementedException` faute de `FUN_00535300` ; celle-ci est transcrite depuis le matin
  (`FreezeAllEntities`). La branche est maintenant écrite. Le menu principal ne l'atteint pas — sa
  table commence par `0x440c` — mais l'opcode 14 passe des tables de dialogue quelconques.

Ce deuxième point est le troisième cas de la journée où **le dépôt contenait déjà de quoi combler un
trou qu'il déclarait ouvert** (après le chargeur de tables de verbes et la table `s_VtSource_0056aec8`
elle-même). Vérifier ce qui existe avant de déclarer un blocage est manifestement rentable.

### La frontière

`FUN_00516fc0`, boucle par image : le `do { … } while` de `0x00517163`, ~1 100 lignes. Elle commence
par `FUN_0040a600` / `FUN_00533000` / `FUN_0053dc30` / `FUN_0050e2b0`, puis l'affichage de mise au
point gardé par `DAT_005633a8`. C'est un autre ordre de grandeur que tout ce qui précède.

Build 0 erreur · 5 tests verts · oracle refigé à 555 corps.

---

## 2026-08-22 — l'ossature de la boucle par image, et trois recouvrements d'arène résorbés

`FUN_00516fc0` a maintenant son **squelette complet**. La fonction a déménagé dans
`Screens/State03/WorldMapTick.cs`, le fichier que le découpage lui réservait (design.md, D9), et le
désassemblage entier des 10 256 octets est conservé sous
`Legacy/Raw/Blaze/FUN_00516FC0-full.asm`.

Ce qui est transcrit : l'entrée de boucle (quatre appels), le passage de meneur, les **quatre cas**
du répartiteur `DAT_005e9ae4 & 3`, la sortie par `DAT_005de02c & 2`, et la queue de boucle
(`FrameEnd` plus les quatre mots d'écran). Quatre trous restent, chacun nommé sur place avec sa
plage d'adresses.

En jeu, la frontière a reculé **deux fois** : `FUN_00516fc0` → cas 0 du répartiteur → suite du cas 0.

### Ghidra nomme les mêmes emplacements de pile différemment à deux endroits de la même fonction

`FUN_0052a1e0` et `FUN_0052a280` reçoivent, au niveau machine, **exactement les trois mêmes
emplacements dans le même ordre** — `lea [esp+0x14] / [esp+0x20] / [esp+0x10]` aux deux sites. La
pseudo-C écrit pourtant `FUN_0052a1e0(&local_5c,&local_50)` d'un côté et
`FUN_0052a280(&local_60,&uStack_54,&stack0xffffff9c,…)` de l'autre : **un des deux jeux de noms est
décalé de quatre octets.** Les deux sont internement cohérents (0xC d'écart entre les deux nommés
dans les deux cas), donc rien ne les départage dans la pseudo-C seule.

C'est tranché par l'usage : le corps d'affichage passe `local_60` en premier argument de
`FUN_0051c120` et se sert de `local_5c` comme compteur de boucle. `local_60` est donc la table
d'ordonnancement, et c'est le rendu de `FUN_0052a280` qui est juste. **Un nom de variable Ghidra
n'est pas une donnée** — celui-ci aurait fait écrire la queue de boucle sur les mauvais
emplacements.

### `PTR_FUN_0056afe8` n'était pas une globale isolée

Les deux crochets indirects de la boucle sont les entrées **8 et 10 de la table `0x0056afc8`**,
c'est-à-dire la quatrième des sept tables de sauts d'entités que `InstallEntityVTables` recopie déjà
au chargement de zone : `(0x0056afe8 − 0x0056afc8) / 4 = 8`. Le dépôt relevait donc leur contenu
depuis longtemps, sous un autre nom.

Pour l'index 0 elles valent `0x0053DCB0` et `0x0053DF90` — **deux vraies fonctions, pas la souche
`0x00401FA0`**, et toutes deux **absentes de `Blaze.c`** (Ghidra ne décompile pas ce dont seule
l'adresse est prise). `FUN_0053dcb0` est transcrite depuis son désassemblage : elle ouvre le pas de
simulation en faisant glisser l'instantané « image précédente » de chaque membre du groupe. C'est le
**quatrième** cas où le dépôt contenait déjà de quoi combler un trou qu'il déclarait ouvert.

Note à garder pour `FUN_0053df90` : si un jour le crochet tombe sur la souche `ret` nu, `EAX` porte
encore l'adresse du dernier argument empilé et l'appelant en fait le **curseur de primitives**. Même
forme que le reste-de-registre déjà documenté sur `ScriptOpAreaVerb`.

### La couche son gagne une dépendance, et c'est le binaire qui la pose

`FUN_0053dc30` (la file des sons différés, appelée une fois par image) mène à `FUN_0053db20` puis à
`FUN_0053da20`, qui **projette la position du son avec la caméra courante** avant de calculer le
panoramique. `Engine/Audio.cs` prend donc `(GameMemory mem, Model3d mdl)` et cesse d'être une
feuille du graphe. Le graphe reste un DAG.

`FUN_0053da20` est un cas de plus où la pseudo-C perd le cadre de pile (`unaff_SI`,
`unaff_retaddr`, deux paramètres au lieu de trois) et où le désassemblage tranche : `unaff_SI` est
l'atténuation calculée juste au-dessus, `unaff_retaddr` le gain de base. Le panoramique est une
paire de cosinus pris à l'écart horizontal, mis à l'échelle **en miroir** selon le côté — `x*8 / x*4`
à droite du centre, `x*4 / x*8` à gauche.

### Trois recouvrements d'arène résorbés, dont un qui n'était plus latent

* `s_Unknown_005de034` (`Screens/State04/CharacterCreation.cs`) tombait dans `CameraBlock`. Il était
  latent jusqu'à ce que `PrepareLeaderAndPartyFlags` (`FUN_0050e2b0`) l'écrive à chaque image.
  Bénéfice inattendu : la table de primes qu'il indexe (0, 0, 10, 20, 30 puis des zéros) se lit
  maintenant comme une **prime de groupe** selon le nombre de membres encore jouables, ce qui est
  cohérent avec ce que la fonction par image y met.
* `s_ScenePitch_005e9308` / `s_SceneYaw_005e930a` / `s_SceneRoll_005e930c` (`Memory/GameMemory.cs`)
  étaient trois `int` autonomes à des adresses espacées de **deux** octets. Ce sont des `short`, et
  ils doublaient `GameArena`. Celui-là **n'était pas latent** : `Screens/State03/WorldMap.cs` remet
  déjà l'orientation à zéro par `*(uint*)Arena(0x005e9308) = 0` pendant que `Engine/Model3d.cs` lit
  les champs. Le cas 2 du répartiteur en aurait fait un troisième écrivain.

### Le test de disposition voit enfin les champs simples — et il est rouge

Le manque nommé ici depuis le 2026-08-19 est comblé. `OpenForesia.Tests` passe à **six** assertions,
et l'analyseur a été rendu plus juste sur quatre points, tous découverts en le regardant échouer :

| correction | ce qu'elle débloque |
|---|---|
| distinguer le **nom déclaré** d'une simple mention | `internal int* CameraTranslation => (int*)(s_CameraMatrix_006008e0 + 0x14)` se déclarait comme une seconde déclaration de `s_CameraMatrix_006008e0` |
| suivre une déclaration sur **plusieurs lignes** | tout `s_X_00y =\n    NativeMemory.Alloc…` se comptait en champ simple |
| résoudre les vues posées sur **une autre vue** | `s_PlayerSlots_005e9188 => s_SlotArea_005e9180 + 8` et les quatre `DrawEnv`/`DispEnv` — une trentaine de faux positifs |
| la surcharge `Alloc(compte, taille)` | `AllocZeroed(0x100, sizeof(int))` se mesurait au quart de sa taille |

État : **trois verts, trois rouges**, et aucun des trois échecs n'est un défaut nouveau — ce sont
des défauts que l'analyseur ne savait pas voir.

* test 2 : `s_EntityStatBlocks_005ea560` s'alloue 0x2400 octets dans `GameArena` ;
* test 3 : `s_UiLabelWidths_00589758` couvre 0x400 octets et déborde sur `s_UiFrameInfo_00589840` —
  sa déclaration dit que le recouvrement est **voulu**, donc c'est le modèle qu'il faut trancher ;
* test 5 : **42** champs de valeur simple doublent une arène. Le chiffre est le résultat le plus
  utile de la journée : ce qui était « à résorber avec les autres » est maintenant une liste
  nominative avec fichier et ligne.

Les corriger change le comportement et relève d'un changement séparé, comme les seize corrections
d'aliasing qui ont précédé.

### Contrôles

Build 0 erreur · 3 tests de disposition verts, 3 rouges **délibérément** (voir ci-dessus) · oracle
refigé à 563 corps, les deux seuls changements de logique étant ceux faits sciemment · en jeu,
la frontière recule deux fois : `FUN_00516fc0` → cas 0 → suite du cas 0. 1 primitive par image sur
la carte du monde, inchangé : rien n'y est encore dessiné.

### La frontière

La suite du cas 0, `0x005176d5` à `0x0051776b` : quatorze appelées, ~1 850 lignes de pseudo-C au
total, dont `FUN_0050bf20` (1 002 lignes, la mise à jour d'un personnage) à elle seule. La première,
`FUN_00532bf0` (146 lignes), est le lissage de l'orientation de scène et de la couleur de fond —
elle ne dépend que de ce qui est déjà là. `FUN_0050e5e0` (36 lignes) est la plus petite mais mène à
la table `PTR_FUN_0056adc8`, la seule des sept qui soit pleine : dix-neuf gestionnaires distincts.

---

## 2026-08-22 (suite) — le pas de simulation du monde : treize appelées sur quatorze

La suite du cas 0 (`0x005176d5`..`0x0051776b`) est écrite. Le jeu traverse maintenant le lissage de
la vue, les scripts du meneur, et s'arrête **à l'intérieur** de la première des six passes de
tables d'entités, sur un gestionnaire que la table `PTR_FUN_0056adc8` désigne.

Écrites, toutes dans `Screens/State03/WorldMapTick.cs` sauf indication contraire :

| Ghidra | nom | note |
|---|---|---|
| `FUN_00532bf0` | `SmoothSceneOrientationAndBackColour` | 146 l. |
| `FUN_00513eb0/ef0/f20` | `RunEntityScriptList`, `RunLeaderScriptListEarly/Late` | `WorldMapScript.cs` |
| `FUN_00532710` | `FreeEntitySlot` | `WorldMapScript.cs`, jumelle d'`AllocEntitySlot` |
| `FUN_0050e5e0` | `TickEntityTable_005fa5e0` | |
| `FUN_0050e680` | `TickEntityTable_005ecaa0` | + `PublishSceneEntityToGlobals` / `ReadSceneEntityBackFromGlobals` |
| `FUN_0050eb20` | `TickEntityTable_005f7de0` | |
| `FUN_0047aa70/ab70/acf0` | `TickEntityTable_005ff400/005edee0/005f2d40` | |
| `FUN_0050ed70` | `TrySpawnRandomEncounter` | |
| `FUN_0050f0d0` | `TickEffectSlots_005e9b60` | |
| `FUN_00534f90` | `ResetPendingChunkRecords` | |
| `FUN_005036e0` | `TickCutsceneCamera` | |
| `FUN_004508D0`, `FUN_004509B0` | `EntityHandler_…` | **absentes de `Blaze.c`**, désassemblées |
| `FUN_0041be60`, `FUN_00468080` | `TryPublishCameraEffectNearEntity`, `TryPlaySoundNearEntity` | arités corrigées au désassembleur |

Ajouts d'outillage : `Winsys3d.Ordinal_442` (libgte `SquareRoot0`) et
`Resources.ResolveEntityHandler`.

### Six fonctions, une seule passe

`FUN_0050e5e0`, `FUN_0050e680`, `FUN_0050eb20`, `FUN_0047aa70`, `FUN_0047ab70` et `FUN_0047acf0`
balaient six tables différentes — `0x005fa5e0`, `0x005ecaa0`, `0x005f7de0`, `0x005ff400`,
`0x005edee0`, `0x005f2d40` — mais ce sont **la même passe**. Toutes portent des enregistrements de
0x9c octets avec la même tête : mot d'état à `+0x00`, sous-type à `+0x05`, indice d'emplacement à
`+0x08`, position à `+0x1c`/`+0x20`/`+0x24`, chaînage de grille à `+0x68`. Toutes font les mêmes
trois gestes : ignorer l'inactif, appeler le gestionnaire par table de sauts, raccrocher à la
grille. **Ce qui les distingue tient dans le gestionnaire, pas dans la passe** — l'estimation
« ~1 850 lignes » comptait six fois le même code.

### Les deux pages de 4 Kio du prologue sont deux grilles spatiales

Le prologue et le cas 0 remettent à zéro `MainMem + 0x1c4000` et `MainMem + 0x1c5000`, 0x400 dwords
chacune. Les passes y raccrochent leurs entités par les constantes `0x3880` et `0x38a0` :
`0x3880 * 0x20 * 4 = 0x1c4000` et `0x38a0 * 0x20 * 4 = 0x1c5000`. Ce sont donc **deux grilles
32×32** de listes simplement chaînées, indexées par `((position >> 12) + 0x2000) >> 9` — la position
en 20.12 ramenée à 32 cases de 512 unités, le test d'appartenance étant fait en **non signé** pour
rejeter d'un coup les cellules négatives. `FUN_0050e5e0` est la seule à alimenter la première. Le
rapprochement ne se lit pas dans la pseudo-C, qui n'écrit que les deux constantes. Rendu par
`LinkIntoWorldGrid`.

Le chaînage stocke une **adresse dans une case de quatre octets**. C'est exact parce que la
reconstruction est bâtie en x86 (`PlatformTarget x86`), donc un pointeur y fait bien quatre octets,
comme en 1998.

### Les sept tables de sauts d'entités sont contiguës, et le débordement est volontaire

`0x0056ad48` (0x20 entrées) puis six tables de 0x40 se suivent sans trou jusqu'à `0x0056b3c8`. Les
trois passes `FUN_0047aa70/ab70/acf0` indexent leur table avec un **octet entier** : un indice de
0xc0 dépasse exactement de 0x300 octets, soit trois tables, et retombe sur l'entrée `index - 0xc0`
de la table trois rangs plus loin — `0056adc8 → 0056b0c8`, `0056aec8 → 0056b1c8`,
`0056afc8 → 0056b2c8`. C'est pour ça que ces trois passes testent `index < 0xc0` : en dessous, la
cible est dans une table **statique** du binaire (`PTR_LAB_00551c30` / `00551f30` / `00552230`) ;
au-dessus, dans la table installée par zone.

D'où `Resources.ResolveEntityHandler(tableVa, index)`, qui résout **par adresse** au lieu d'indexer
sept tableaux séparés : le débordement devient lisible au lieu de lever un
`IndexOutOfRangeException`. Le même débordement était déjà documenté sur `ScriptOpAreaVerb` pour
`0056aec8 → 0056afc8` ; il est maintenant compris comme une règle générale plutôt que comme une
curiosité locale.

### Un neuvième défaut d'aliasing, attrapé avant qu'il ne devienne vivant

`s_BackColourR_005e93a0` / `G_93a2` / `B_93a4` étaient trois `int` autonomes pour trois adresses
espacées de **deux** octets, à l'intérieur de GameArena. Le désassemblage de `FUN_00532bf0`
(`0x00532e38`) tranche : `movsx eax,word [5E93A0h]` à la lecture, `mov [5E93A0h],ax` à l'écriture, et
la branche `DAT_005e9166 == 1` les recopie par **dword** (`mov eax,[5E9160h] ; mov [5E93A0h],eax`) —
ce qui n'est pas représentable en champs séparés. Corrigés en vues, exactement comme
`s_ScenePitch_005e9308` la veille et dans le même fichier.

**La correction est neutre en comportement aujourd'hui** : avant elle, aucun site n'écrivait ces
trois symboles, ni par champ ni par arène ; les deux mémoires valaient zéro des deux côtés.
`FUN_00532bf0` allait en devenir le premier écrivain, donc le premier à rendre l'aliasing vivant —
et le lissage aurait alors écrit dans une mémoire pendant que `Ordinal_376` lisait l'autre. Le test
5 d'`OpenForesia.Tests` tombe de **42 à 39** champs signalés.

C'est la deuxième fois en deux jours que le même piège se présente **au moment où l'on écrit le
premier écrivain d'un symbole**. La règle pratique qui s'en dégage : quand une transcription
s'apprête à écrire pour la première fois un `.data` déclaré en champ, vérifier son adresse contre
les arènes avant d'écrire la première ligne, pas après.

### Ghidra perd des paramètres par famille, pas par accident

`FUN_0041be60` et `FUN_00468080` portent exactement le même défaut : la pseudo-C montre trois
paramètres et se sert de `param_1` **à la fois comme entité** (`param_1 + 0x1c`) **et comme première
valeur passée**, ce qui ne peut pas être. Les deux sites d'appel empilent **six** arguments
(`add esp,18h`) et les corps lisent les arguments 2, 3 et 4 ; les arguments 5 et 6 ne sont lus nulle
part dans les deux corps.

Le contrôle est toujours le même, et il tient en deux gestes : compter les arguments sur le
`add esp,N` qui suit le `call` (`N/4`, cdecl), puis retrouver les décalages `[esp+N]` du corps en
soustrayant les `push` du prologue. Les deux fonctions sont d'ailleurs jumelles au-delà de la
signature : même calcul de distance à la caméra `DAT_005edebc`, l'une publie trois mots dans
`DAT_005de0a8`/`ac`/`b0` sous 1500 unités, l'autre joue un son sous 2000.

`FUN_004509b0` a livré un troisième cas de la même famille, plus discret : Ghidra rend
l'identifiant du son comme `CONCAT22((short)(DAT_005e97cc >> 0x10), …)`, ce qui n'a aucun sens
comme valeur. C'est sa façon de modéliser un `movzx ax, byte […]` qui laisse la moitié haute d'EAX
périmée — l'appelé ne lit que `movsx word`, donc seule la moitié basse compte. **Un `CONCAT22` dont
l'une des moitiés vient d'un symbole sans rapport est le signe d'un registre partiellement écrit,
pas d'un vrai calcul.**

### Une surcharge homonyme rend l'oracle aveugle

`compare-bodies.ps1` indexe les corps **par nom seul**. Un `Blaze.CallEntityHandler` ajouté à côté
de `Resources.CallEntityHandler` lui a fait comparer l'un à l'autre et signaler une « logique
changée » qui n'existait pas. Renommé `DispatchEntityHandler`, et le rapport est redevenu exact.

Même famille de piège que la liste blanche insensible à la casse déjà documentée dans `TODO.md` :
**le nom est l'index de l'outillage, pas une décoration.** Le corollaire pratique : ne jamais
introduire une surcharge d'un nom déjà porté par une autre classe du dépôt.

### La lacune qui porte sa donnée

Les gestionnaires d'entité manquants lèvent maintenant en imprimant **les seize premiers octets de
l'enregistrement** en plus de leur adresse. Ce n'est pas décoratif : le premier arrêt sur
`0x004508D0` a fait douter que l'entité soit légitime — les emplacements 8, 9 et 10 portaient les
sous-types 8, 9 et 10, une coïncidence trop nette. Le vidage a tranché en un seul essai :
`00 20 20 c0 | 0e 0a 00 02 | 0a 00 0e 00 | 02 ff 00 00` est exactement ce que
`SpawnSubAreaEntities` écrit (état `0xc0202000`, indice `+0x08 = 10`, genre `+0x07 = 2`, dword de
genre `+0x0c = 0x0000ff02`), et le sous-type vient bien de la donnée de zone. La zone de départ pose
simplement une entité de chaque type, dans l'ordre.

Sans ce vidage, la seule façon d'en être sûr était d'instrumenter le jeu pour un essai, ou de
transcrire onze gestionnaires en espérant qu'ils ne soient pas du code mort.

### Ce qui reste, et c'est mesuré

**(A) La famille des dix-neuf gestionnaires de `PTR_FUN_0056adc8`** — c'est le blocage courant. Deux
faites, dix-sept restantes, dont **douze absentes de `Blaze.c`** (Ghidra ne décompile pas ce dont
seule l'adresse est prise). Le tableau ordonné, avec la taille et la disponibilité de chacune, est
en tête de `TODO.md`. Travail mécanique et répétitif : le jeu nomme, on transcrit, on relance.

**(B) `FUN_0050bf20`, le pas par personnage** — 1 002 lignes, la seule des quatorze appelées du cas
0 qui manque. **Sa moitié basse n'est pas exploitable dans la pseudo-C** : à partir de `0x0050cca1`
environ, Ghidra rend une cascade de `__allmul`/`__allshr` dont il a perdu les résultats, plus un
`iStack_4` jamais affecté et des `unaff_EBX`/`unaff_EBP`. Classe de blocage qui demande le
désassemblage. Appelées manquantes déjà repérées : `FUN_005238e0`, `FUN_00530070`, `FUN_0051a020`,
et les tables statiques `PTR_FUN_00562ce0` / `PTR_FUN_00562ca0`.

Deux petites lacunes restent aussi à l'intérieur de `FUN_0050e680`, toutes deux dans `Blaze.c` :
`FUN_0051a740` (mise à mort d'une entité de scène) et `FUN_0053aa10` (pas d'animation).

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : **un seul corps signalé**, `RunWorldMapTick`,
et c'est le changement voulu ; 22 méthodes nouvelles. Tests de disposition : les trois rouges
attendus, dont le test 5 qui passe de 42 à 39 champs. Playtest `-Entry 0` : le jeu atteint l'état 3,
exécute le pas de simulation et s'arrête sur la lacune nommée du gestionnaire `0x00450AA0`.

---

## 2026-08-22 (fin) — les gestionnaires d'entité : quatre suffisaient, pas dix-sept

Les six passes de tables d'entités du cas 0 traversent maintenant sans lever. Le front est passé
à `FUN_0050bf20`, le pas par personnage — le dernier trou du pas de simulation du monde.

### La mesure du matin était pessimiste d'un facteur quatre

Le relevé de la veille annonçait « dix-sept gestionnaires restants, dont douze absentes de
`Blaze.c` », en supposant que la zone de départ posait une entité de chaque type parce que les
emplacements 8, 9 et 10 portaient les sous-types 8, 9 et 10. **Faux.** Dès l'emplacement 11, la
suite se casse : les emplacements actifs demandent les sous-types **8, 9, 10 et 18**, et rien
d'autre. Quatre gestionnaires, pas dix-sept.

| sous-type | adresse | dans `Blaze.c` ? | taille | rôle |
|---:|---|---|---:|---|
| 8 | `0x004508D0` | **non** | 197 o. | animation de 0x28 images, angle croissant, effet de caméra |
| 9 | `0x004509B0` | oui | 46 l. | sa jumelle en sens inverse, avec un son à l'ouverture |
| 10 | `0x00450AA0` | **non** | 273 o. | montée puis maintien d'un niveau, gardé par le bit de type |
| 18 | `0x00450D30` | **non** | 1 087 o. | le coffre |

**La leçon est celle des verbes de zone, re-vérifiée** : on ne transcrit pas une table de sauts en
spéculant sur ce qui sera appelé. La lacune nomme, on transcrit, on relance. Le coût réel a été
quatre passes de désassemblage au lieu des dix-sept annoncées, et les quinze gestionnaires restants
attendront qu'une zone les demande.

### `DAT_00571540` : le cycle est complet

Trois des quatre gestionnaires partagent un registre de cinq bits, un par type d'effet de zone,
dans l'octet bas de `DAT_00571540` — que `Init_004502d0` remet à zéro au chargement de zone.
`0x004508D0` et `0x004509B0` **allument** le bit de leur type au premier tour ; `0x00450AA0` s'en
sert comme feu vert pour avancer, puis l'**éteint** quand son effet s'achève. Rendu par
l'accesseur `AreaEffectKindBits`.

Les trois passent par la **même table de sauts à cinq entrées**, dont les cibles ne diffèrent que
par la constante — `or 1/2/4/8/0x10` d'un côté, `and 0FEh/0FDh/0FBh/0F7h/0EFh` de l'autre. C'est
`1 << (type - 1)`, et c'est écrit ainsi plutôt qu'en cinq branches identiques.

`0x00450AA0` en donne une variante instructive : ses cinq gardes s'écrivent
`cmp cx,N ; jne ; test dl,M ; je`, sauf la deuxième, qui est `test cl,dl`. Ce n'est **pas** un cas
particulier — le compilateur a réutilisé `CL`, qui porte déjà le 2 de la comparaison. Les cinq
gardes se réduisent à une ligne. **Une instruction qui a l'air différente des quatre autres mérite
qu'on regarde ce que le registre contenait avant de conclure à une exception.**

### Le coffre, et les deux registres qu'il faut suivre

`FUN_00450D30` (1 087 octets, absente de `Blaze.c`) est une machine à états dont les jalons sont
espacés — `0`, `0xFFFF`, `0xFFFE`, `1`, `2`, `0x14`, `0x28`, `0x64`, `0xC8`, `0xC9`, `0xCA`,
`0x12C`, `0x3E7` — et dont chaque état est un `if` séparé qui **tombe dans le suivant** : plusieurs
transitions s'enchaînent dans la même image. Transcrit tel quel plutôt qu'en `switch`, parce que le
`switch` supprimerait précisément cet enchaînement.

**Deux registres persistants portent des valeurs qui changent en cours de route, et une lecture
naïve du désassemblage se trompe dessus :**

* **`EDI`** vaut zéro à l'entrée et sert de « zéro » à plusieurs comparaisons — mais `0x00451097`
  le recharge à `0xC8`, et cette instruction est sur **toutes** les routes qui mènent à l'état
  `0x12C`. Le `mov [esi+10h],di` de `0x0045115d` pose donc `0xC8`, pas zéro : après le message de
  refus, le coffre repart par la séquence de fermeture. Un zéro l'aurait fait boucler sur son
  amorçage.
* **`EBX`** vaut 1 en tête et `0x28` à partir de `0x00450e79`. Les `mov [esi+10h],bx` des deux
  côtés ne posent pas la même valeur.

**La règle qui s'en dégage** : dans un corps sans variables locales, un registre chargé d'une
constante loin du point d'usage est un piège à part entière. Avant de transcrire un
`mov [x],reg`, remonter à la dernière écriture du registre **sur chaque route** qui atteint la
ligne, pas seulement sur la plus proche.

### Un neuvième défaut de 1998, contradictoire avec lui-même

`FUN_00450D30` se garde explicitement d'un `+0x6c` nul — `cmp eax,edi ; jne ; mov eax,5DE558h` —
puis, trente instructions plus loin, écrit `mov dword [ebx+6Ch],0` par le registre **non replié**,
et relit deux fois `[esi+6Ch]` sans repli du tout. Les deux moitiés se contredisent dans la même
fonction : le repli n'existe que parce que l'auteur savait que le champ peut être nul.

Latent, pas observé — l'état `0x28` n'est atteint qu'après un contact qui écrit `+0x6c`. Écrit dans
`legacy-bug.md` sous **LB-007**, et transcrit tel quel : étendre le repli rendrait le code plus sûr
que l'original et masquerait la contradiction.

### Ghidra et les registres partiellement écrits, troisième et quatrième cas

`FUN_004509b0` rend l'identifiant du son comme
`CONCAT22((short)(DAT_005e97cc >> 0x10), (ushort)*(byte *)…)`, ce qui n'a aucun sens comme valeur.
C'est un `movzx ax, byte […]` suivi d'un `push eax` : la moitié haute d'EAX est périmée, et l'appelé
ne lit que `movsx word`. **Un `CONCAT22` dont l'une des moitiés vient d'un symbole sans rapport est
la signature d'un registre partiellement écrit, pas d'un calcul.**

Même famille dans `FUN_00450D30`, où `mov eax,[esi+44h]` puis `mov al,[eax+5E9860h]` laisse les
trois octets hauts d'EAX porter l'index ; le `and eax,0FFh` de la branche suivante confirme la
lecture.

### Deux petites transcriptions au passage

`FUN_0053a920` (`PickWeightedTableEntry`) est un tirage pondéré : il compte combien de sept seuils
`rand() % bound` dépasse, ce qui donne une colonne de 0 à 7 dont la probabilité décroît, puis lit
l'octet **signé** de cette colonne dans une table de huit octets par ligne prise à `ctx + 0xb4`.
Sept comparaisons indépendantes plutôt qu'une table cumulative.

`FUN_00468080` avait déjà été écrite dans la passe précédente ; le coffre l'appelle avec le même
double déréférencement que `FUN_004509b0`, à `+8` près sur l'octet lu.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : **un seul corps signalé**, `RunWorldMapTick`,
et c'est le changement voulu ; 25 méthodes nouvelles. Tests de disposition : les trois rouges
attendus, 39 champs (inchangé depuis la correction de la couleur de fond). Playtest `-Entry 0` : les
six passes de tables traversent, et l'arrêt est désormais sur `FUN_0050bf20`, membre 0.

---

## 2026-08-22 (nuit) — `FUN_0050bf20` : les quatre lectures que la pseudo-C ne donne pas

Le pas par personnage est transcrit **jusqu'à `LAB_0050caf3`** : le prologue, l'animation,
l'aiguillage principal, la branche du membre jouable, celle du membre suivi par l'IA et la sonde de
terrain. Le jeu les exécute et s'arrête maintenant sur `FUN_0050e140`, le premier gestionnaire de
commande. Reste la queue commune, ~500 lignes.

La fonction fait 8 Ko de code pour 1 002 lignes de pseudo-C. **Le coût n'est pas là où la taille le
laisse croire** : l'essentiel du travail a été de rendre lisibles quatre endroits où Ghidra ment,
et c'est ce qui est consigné ici parce que c'est réutilisable.

### `iStack_4` : Ghidra a perdu le paramètre parce que la fonction l'écrase

La pseudo-C se sert d'`iStack_4` une quinzaine de fois — `*(int *)(iStack_4 + 0x1c)`,
`FUN_00519f50(puVar3, iStack_4)` — sans jamais l'affecter. C'est `[esp+68h]`, et `[esp+68h]` est
**l'emplacement du paramètre entrant**, que le prologue écrase :

```
0050bf25  mov  ebp,[esp+60h]         ; ebp = param_1 (membre)
0050bf47  imul eax,4Ch
0050bf4a  add  eax,5E9180h           ; eax = &DAT_005e9180 + membre * 0x4c
0050bf52  mov  [esp+68h],eax         ; ... et ecrase la case du parametre avec
```

Confirmé au premier usage : `0050ce71  mov edi,[esp+68h] ; movsx ebx,word [edi+44h]` est exactement
le `*(short *)(iStack_4 + 0x44)` de la pseudo-C. C'est donc **le même objet que `puVar11`**, que
Ghidra nomme correctement ailleurs dans la même fonction — un objet sous deux noms, dont l'un est
perdu.

**La leçon générale** : quand une variable de pile est lue sans jamais être écrite, regarder d'abord
si son décalage tombe sur la zone des paramètres. Une fonction qui réutilise la case de son
paramètre comme locale est courante en 1998, et c'est exactement le cas que le décompilateur ne sait
pas rendre.

### `unaff_EBP` et `unaff_EBX` : deux registres sauvés en pile dès le prologue

`unaff_EBP` est le bloc par personnage (`MainMem + 0xf0000 + membre * 0x2000`), posé en `0x0050bf61`
et sauvé en `[esp+10h]`. `unaff_EBX` est l'état de manette (`0x00604a20 + slotKind * 0x10`), posé en
`0x0050bf63` et sauvé en `[esp+14h]`. Les deux sont les `iVar23` et `puVar21` que la pseudo-C nomme
correctement en tête ; Ghidra les perd parce que `0x0050cee9` (`mov ebp,[esi+78h]`) réutilise le
registre en cours de route.

**Le prologue suffit à identifier les trois.** Il tient en quarante instructions et il pose tout ce
que la fonction relira ensuite. Sur un corps de cette taille, le lire d'abord coûte deux minutes et
évite de deviner pendant une heure.

### Le bloc de matrices : `mov ecx,0Ch` répété six fois

De `0x0050cfb1` à `0x0050d290`, Ghidra rend une cascade de `__allmul`/`__allshr` dont il a perdu
tous les résultats — une quarantaine de lignes illisibles. Au désassembleur, c'est **deux produits
matrice-vecteur parfaitement réguliers** : trois `__allmul` par ligne, une addition 64 bits, puis
`mov ecx,0Ch ; call __allshr`. Le décalage est **12**, six fois de suite.

La composition a un sens géométrique clair, et il se lit dans les deux `Ordinal_366` :
`FUN_0051c300` bâtit la matrice du repère de l'emplacement, le code la **transpose** à la main
(0x005e9b00 reçoit 0x005e9b20 dans l'ordre 0,3,6 / 1,4,7 / 2,5,8), puis bâtit celle du porteur.
Le premier produit exprime l'offset dans le repère local de l'emplacement, le second le ré-exprime
dans celui de ce qui le porte. C'est un attachement père-fils standard.

**Ghidra brouille l'ordre des opérandes dans chaque triple**, ce qui donne l'illusion que les
composantes sont permutées entre les deux passes. Elles ne le sont pas : la somme est commutative,
et ce qui compte est quel `m[i]` va avec quel emplacement de pile. En suivant les décalages
`[esp+N]` à travers les `push`, les deux passes sont `row_i = (m[3i]*x + m[3i+1]*y + m[3i+2]*z) >> 12`,
sans surprise.

### La sonde de terrain : deux axes, un seul emplacement de pile

`ProbeGroundAhead` avance l'entité de quatre pas le long de son cap. La pseudo-C ajoute **deux fois
la même valeur** à X et à Z, parce que Ghidra a fait tenir le sinus et le cosinus dans le même
emplacement. Le binaire (`0x0050c512`..`0x0050c57b`) donne `sinus << 4` sur X et `cosinus << 4` sur
Z — la même convention que `TrySpawnRandomEncounter`. **Vérifié, pas supposé** : un axe interverti
aurait envoyé la sonde de côté, et rien à l'écran ne l'aurait signalé.

### Deux labels qui n'en font qu'un

La branche du membre suivi par l'IA porte `LAB_0050c4d6` et `LAB_0050c781`, sautés depuis trois
endroits — l'allure d'un graphe irréductible. Il ne l'est pas : **toutes** les routes qui atteignent
`LAB_0050c781` le font avec une distance restante nulle ou négative, et toutes celles qui atteignent
le bloc de sonde le font avec une distance d'au moins 1. Les deux labels se ramènent à un seul
`if (distance >= 1)`, et le drapeau « entrer en approche » que la lecture naïve suggère est inutile.

C'est le même constat que sur `FUN_00533940` en août : un graphe de labels n'est presque jamais
irréductible, il faut chercher l'invariant qui les sépare.

### Deux tables statiques relevées dans le binaire

`&DAT_00562cc0` porte huit entiers — `{0x13, 0x13, 0x15, 0x12, 0x14, 0x14, 0x12, 0x15}` — indexés
par les trois bits bas de `bloc + 0x150`, la posture : c'est la **portée de détection** que le
membre suivi compare à sa distance au meneur.

`&PTR_FUN_00562ca0` porte huit gestionnaires de posture, dont **quatre sont `0x0040A600`**, le corps
vide déjà vérifié au désassembleur : les postures 1, 2, 3 et 5 ne font rien. `&PTR_FUN_00562ce0`
porte quatre gestionnaires de commande, tous réels.

### `ReleaseTexture` était un import, pas une fonction du jeu

Le thunk `0x00545ad6` est un `jmp dword [608804h]`, et le nom vient de la table d'imports : c'est
**l'ordinal 788 de `winsys3d.dll`** (`winsys3d.c` l. 25890, un renvoi d'une ligne vers
`FUN_00a27700`). Ajouté aux P/Invoke. Le dépôt portait déjà un `ReleaseTexture` **sans rapport**
dans `src/Host/D3D11Backend.cs` — s'y raccrocher aurait compilé et fait n'importe quoi. Un nom qui
existe déjà des deux côtés de la couture `Blaze`/`Host` mérite d'être vérifié avant d'être lié.

### Ce qui reste

La queue commune, `0x0050caf3`..`0x0050dc0d`. Onze appelées lèvent en se nommant, **toutes dans
`Blaze.c`** — aucune ne demande de désassemblage. Le jeu réclame `FUN_0050e140` en premier.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : **un seul corps signalé**, `RunWorldMapTick`,
et c'est le changement voulu. Tests de disposition : inchangés, 39 champs. Playtest `-Entry 0` : le
jeu entre dans le pas par personnage, exécute le prologue, l'animation, l'aiguillage et la branche
jouable, et s'arrête sur la lacune nommée du gestionnaire de commande.

---

## 2026-08-22 (fin) — la queue commune de `FUN_0050bf20` : la fonction est entière

Les ~500 lignes de `LAB_0050caf3` sont écrites. **`FUN_0050bf20` est transcrite en entier**, ses
1 002 lignes, et le jeu l'exécute jusqu'au premier appel indirect : il réclame maintenant
`FUN_0050e140`, l'entrée 0 de la table de commande `PTR_FUN_00562ce0` — 68 lignes dans `Blaze.c`.

La queue se lit en huit temps : restitution d'état, publication vers les globales, intégration de
la vitesse, offset du porteur, deux demi-pas de placement, amortissement et rebond, sol et plafond,
puis la cascade de fin (coup reçu, pas sonore, ombre, éclairage, changement de sous-zone,
immobilité). Découpée en `TickPartyMemberTail` et `ComputeCarrierOffset` ; le reste est en flot.

### Le même piège de pile, une troisième fois — et cette fois il portait sur un axe

La pseudo-C de la queue écrit `uStack_14 = Ordinal_451(cap)` puis `iStack_10 = Ordinal_450(cap)`,
précédé d'un `iStack_10 = 0` qui a tout l'air d'un magasin mort. Les trois noms sont attachés aux
mauvais emplacements. Le binaire (`0x0050cd63`..`0x0050cd7e`) est sans ambiguïté :

```
0050cd63  mov  [esp+54h],eax          ; sinus
0050cd67  mov  dword [esp+58h],0      ; le Y, mis a zero
0050cd7e  mov  [esp+5Ch],eax          ; cosinus
```

Le triplet est donc **(sinus, 0, cosinus)** — un cap horizontal, avec un Y nul que Ghidra rend
comme une affectation morte. Le recoupement décisif vient d'ailleurs dans la même fonction : le
bloc `& 0x100` ajoute `entry+0x44`, `entry+0x46` et `entry+0x48` à ces trois emplacements dans
l'ordre, et `0x0050ce71` montre `[esp+54h] ↔ +0x44`, `[esp+58h] ↔ +0x46`, `[esp+5Ch] ↔ +0x48`.
X, Y, Z.

**C'est le troisième emplacement de pile que Ghidra confond dans cette seule fonction** (après
`iStack_4` et le couple sinus/cosinus de la sonde de terrain). La règle pratique qui s'en dégage :
dans un corps où le compilateur a fait tenir plusieurs valeurs dans le même emplacement, **ne
jamais faire confiance au nom que Ghidra donne à une variable de pile — seulement à son décalage**.
Un axe interverti ici aurait fait marcher le groupe en crabe, et rien à l'écran ne l'aurait dit.

### `Ordinal_439` : la bonne des deux variantes

Le cap est ensuite basculé sur la pente par `Ordinal_439`, le `VectorNormal` de libgte. La DLL en
porte **deux variantes à l'octet près** : l'ordinal 439 (`winsys3d.c` FUN_00a25910) écrit trois
`int`, l'ordinal 440 (FUN_00a259d0) écrit trois `short`. La distinction n'est pas cosmétique : la
sortie écrase ici trois emplacements de pile de quatre octets, et se tromper de variante n'aurait
rempli que six octets sur douze, laissant le Z du cap à sa valeur précédente.

### Deux tables statiques de plus, et une borne qui se lit dans la donnée

`&DAT_00562cf0` porte les jeux de paramètres de pas sonore, **0xc octets (six shorts) par entrée**.
La table compte **dix-huit entrées**, ni seize ni vingt : le dword de `0x00562dc8` vaut
`0x02030100`, une suite d'octets croissants qui n'a plus la forme des six shorts. **La borne se lit
dans la donnée, pas dans les sites d'appel** — et c'est ce qui la rend fiable, parce qu'aucun site
d'appel ne la donne. Quatre entrées (4, 7, 12, 15) sont entièrement nulles : les postures sans pas
sonore.

L'indice est l'octet de posture **entier**, pas ses trois bits bas ; seule l'entrée 6 de la posture
0 est détournée vers `(posture >> 3) + 0x10`, ce qui tombe exactement sur les deux dernières
entrées. Un indice hors des dix-huit lève en se nommant plutôt que de rendre des octets inventés.

### Le bloc de matrices, écrit

`ComputeCarrierOffset` rend les deux produits matrice-vecteur de `0x0050cfb1` : la transposée du
repère de l'emplacement, puis le repère du porteur, décalage `>> 12` chacun. Le résultat est la
différence entre la position ainsi transportée et la position d'origine — l'offset qu'impose ce qui
porte le personnage. C'est un attachement père-fils standard, et il ne se voyait pas du tout dans
la cascade de `__allmul` que Ghidra rend.

### Une adresse voisine qui n'est pas la même

La queue lit `0x005ff3e4` dans la branche de restitution et écrit `0x005ff3e8` dans la
publication. Ghidra préfixe la seconde d'un `_`, ce qui signale un recouvrement de symboles et
invite à croire à une faute de frappe. Le désassembleur tranche : `mov [5FF3E8h],eax` en
`0x0050cd43`. Ce sont bien **deux cases voisines et distinctes**, et les confondre aurait fait
relire à la restitution ce que la publication venait d'écrire.

### Ce qui reste

Les quatre gestionnaires de commande de `PTR_FUN_00562ce0` (deux dans `Blaze.c`, deux absentes),
les quatre gestionnaires de posture réels de `PTR_FUN_00562ca0`, et treize appelées nommées —
**toutes dans `Blaze.c`**, aucune ne demandant de désassemblage. Le jeu réclame `FUN_0050e140` en
premier.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : **un seul corps signalé**, `RunWorldMapTick`,
et c'est le changement voulu ; 30 méthodes nouvelles. Tests de disposition : les trois rouges
attendus, 39 champs. Playtest `-Entry 0` : le jeu traverse le pas par personnage jusqu'au
gestionnaire de commande.

---

## 2026-08-22 (nuit) — sept appelées de `FUN_0050bf20`, et un paramètre perdu qui aurait tourné en silence

Le chemin traverse maintenant la queue commune. Écrites dans cette passe :

| Ghidra | nom | note |
|---|---|---|
| `FUN_0050e140` | `HandleDirectionalInput_0050E140` | la marche à la croix directionnelle |
| `FUN_0051a020` | `ApplyGroundMotion_0051A020` | 221 l., cinq postures |
| `FUN_0047aae0` | `SpawnCastEntity_0047AAE0` | jumelle de `SpawnEffectEntity` |
| `FUN_0051a670` | `PostureHandler_0051A670` | 14 l. |
| `FUN_00519f50` | `PullEntityTowardCameraTarget_00519F50` | **paramètre perdu, voir plus bas** |
| `FUN_00519df0` | `ApplyEntityMotion_00519DF0` | balayage 3×3 de la grille de `0x1c4000` |
| `FUN_00519ee0` | `UpdateEntityShadow_00519EE0` | le clignotement d'invulnérabilité |

Le front est `FUN_00534330` (277 l., dans `Blaze.c`) — le contact entre deux entités voisines.

### Le paramètre perdu le plus dangereux des cinq

`FUN_00519f50` est déclarée `void FUN_00519f50(uint *param_1)` alors que son site d'appel en empile
deux. Jusque-là, rien de nouveau — c'est le cinquième cas de cet arbre. **Ce qui change, c'est où
Ghidra fait atterrir l'écriture** :

```c
uVar2 = Ordinal_442(iVar3 * iVar3 + iVar5 * iVar5);
*(undefined2 *)param_1 = uVar2;        // dans la moitie basse du mot d'etat de l'entite ?
```

Écrire la distance à l'offset 0 de l'entité écraserait **seize drapeaux** à chaque image. Le
désassemblage tranche :

```
00519f96  call 00545A10h            ; Ordinal_442, stdcall
00519f9b  mov  ecx,[esp+18h]        ; quatre push au prologue -> [esp+18h] = ARG 2
00519f9f  mov  [ecx],ax             ; la distance va dans l'entree d'emplacement
```

**Les quatre parametres perdus précédents se signalaient** : la pseudo-C lisait une variable jamais
écrite (`iStack_4`) ou un registre non attribué (`unaff_EBP`, `unaff_EBX`), et le compilateur C#
aurait refusé de compiler une transcription naïve. **Celui-ci compile et tourne.** Il ne se serait
manifesté que par un personnage dont les drapeaux se réinitialisent tout seuls, des heures plus
tard, sans rien qui pointe vers cette fonction.

La règle qui s'en dégage : **quand Ghidra déclare moins de paramètres que le site d'appel n'en
empile, ne pas se contenter de rajouter les manquants — vérifier aussi que les écritures du corps
atterrissent bien où il le dit.** Un paramètre perdu n'est pas seulement absent de la signature ; il
est confondu avec un autre, et toutes ses déréférences le sont avec lui.

### La table `0x00562dc8`, ou comment lire seize octets

`FUN_0050e140` indexe une table de seize octets par le quartet haut de `manette[0]`. Le contenu
paraît arbitraire — `0, 1, 3, 2, 5, 0, 4, 0, 7, 8, 0, 0, 6, 0, 0, 0` — jusqu'à ce qu'on remarque que
les quatre bits sont haut, droite, bas, gauche dans cet ordre :

```
1 (haut)          -> 1        5  (haut+bas)     -> 0
3 (haut+droite)   -> 2        10 (gauche+droite)-> 0
2 (droite)        -> 3        7, 11, 13, 14, 15 -> 0
6 (droite+bas)    -> 4
```

Les huit secteurs dans le sens horaire, diagonales comprises, et zéro pour tout ce qui s'annule.
**Une table de constantes se lit en cherchant la structure, pas en la recopiant** : sans ce
recoupement, les quatre zéros du milieu auraient eu l'air d'un trou dans la donnée.

### Le rôle de `DAT_00562cc0`, établi par son deuxième lecteur

La table avait été nommée `s_LeaderReachByPosture` en transcrivant `TickFollowingMember`, où elle
sert de distance d'arrêt. `FUN_0050e140` la décale de douze bits pour en faire **directement la
vitesse du personnage** : c'est une vitesse, et le membre suivi s'arrête à une distance égale à un
pas de son meneur. Renommée `s_PostureSpeed_00562cc0`.

**Un seul site d'appel ne suffit pas à nommer une table de constantes.** Le premier lecteur donne
une interprétation plausible ; c'est le deuxième qui la confirme ou la corrige.

### Deux vues incompatibles de la même région, conservées

`FUN_0051a020` lit `bloc + 0x160 + famille * 8` de deux façons selon la posture : comme un **masque
de bits** de 64 bits dans la branche d'incantation (`1 << (index & 0x1f)` sur le dword
`(index >> 5)`), et comme **huit compteurs d'un octet** dans la branche d'objet consommable. Les
deux vues se recouvrent. Transcrites telles quelles avec un `TODO(REVERSE)` : soit les deux
`famille` ne se croisent jamais, soit c'est un défaut de 1998 — et rien dans la fonction ne permet
de trancher.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : **un seul corps signalé**, `RunWorldMapTick`,
et c'est le changement voulu ; 37 méthodes nouvelles. Tests de disposition : inchangés, 39 champs.
Playtest `-Entry 0` : le jeu traverse le pas par personnage et la queue commune jusqu'à
`FUN_00534330`.

---

## 2026-08-22 (tard) — `FUN_00534330` : quand la pseudo-C confond ses deux opérandes

`FUN_00534330` n'est **pas** transcrite. Ce qui est fait, et c'est le verrou, c'est la
correspondance entre les noms de Ghidra et les vrais objets — sans elle le corps est intranscriptible,
avec elle il redevient mécanique.

### Pourquoi celle-ci n'est pas comme les précédentes

Elle fait **2 464 octets** (0x00534330..0x00534cd0) pour 277 lignes de pseudo-C : une boucle sur les
faces du maillage de collision d'une entité voisine, avec par face un produit vectoriel dans les
registres GTE émulés puis jusqu'à trois `NormalClip` pour le test d'appartenance au triangle.

Ghidra la déclare `undefined4 FUN_00534330(uint *param_1)` — **un** paramètre — alors que le site
d'appel en empile trois (`add esp,0Ch` en 0x00519e83). Jusque-là c'est le sixième cas de cette
famille dans le même arbre. Ce qui la distingue : Ghidra ne se contente pas d'omettre les deux
autres, **il fait porter à un même nom des accès qui visent des objets différents**.

| ce que Ghidra écrit | ce que c'est |
|---|---|
| `param_1`, `puStack_18` | **argument 1**, l'occupant — `[esp+74h]`. Les deux noms sont le même objet (`puStack_18 = param_1` en tête de boucle, `param_1 = puStack_18` en queue) |
| `local_14` *vu comme une entité* | **argument 2**, le personnage qui se déplace — `[esp+78h]` |
| `local_14` *passé à `FUN_0051c300` / `FUN_005327b0` / `Ordinal_366`* | un **tampon local de neuf shorts** à `[esp+5Ch]`, la matrice de l'occupant — rien à voir |
| `iStack_10` | **argument 3**, l'entrée d'emplacement — `[esp+7Ch]` |
| `puStack_54` | le bloc de modèle de l'occupant, mis de côté et restauré à chaque tour |

Le recoupement décisif est `0x00534778` :

```
00534778  mov  edx,[edi+1Ch]        ; edi = argument 2
0053477e  mov  [edi+78h],esi        ; esi = argument 1  ->  entite[0x78] = occupant
00534781  mov  ecx,[esi+1Ch]
00534784  sub  edx,ecx
00534786  mov  ecx,[esp+7Ch]        ; argument 3
0053478a  mov  [ecx+1Ch],edx        ; entree[0x1c] = entite.x - occupant.x
```

que la pseudo-C rend en `local_14[0x1e] = puStack_18` et
`*(uint *)(iStack_10 + 0x1c) = local_14[7] - puStack_18[7]`. Les trois arguments apparaissent dans
six instructions, et les trois noms de Ghidra s'y raccrochent sans ambiguïté.

### Ce que ce cas ajoute aux cinq précédents

Les cinq paramètres perdus rencontrés jusqu'ici se rangeaient en deux classes : ceux qui **ne
compilent pas** (une variable lue sans être écrite, `iStack_4`) et celui qui **compile et corrompt**
(`FUN_00519f50`, où l'écriture atterrissait sur le mauvais objet). Celui-ci est une troisième
classe, la pire : **deux objets du même type sous un seul nom**.

Intervertir l'occupant et le personnage compile, tourne, et rend une collision qui pousse le décor
au lieu de pousser le personnage. Aucun compteur de primitives ne le dirait, aucune capture d'écran
non plus tant que le décor ne bouge pas visiblement. **Le seul garde-fou est de vérifier chaque
accès contre la table ci-dessus avant de l'écrire**, et de ne jamais déduire l'objet du nom seul.

C'est aussi pourquoi la table est gravée dans le résumé XML de la méthode plutôt que seulement ici :
elle doit être sous les yeux de qui écrira le corps.

### Le piège suivant est déjà connu

Les six termes du `NormalClip` sont **inlinés** dans le corps, et le dépôt en porte déjà la version
reconstruite (`FUN_0051c280`, `Platform/Winsys3d.cs`). `FinalizeEntityPlacement2` a payé ce
précédent en août : Ghidra émet les termes dans un autre ordre, il faut les recouper terme à terme
avant d'appeler la fonction existante.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : un seul corps signalé, `RunWorldMapTick`,
le changement voulu. Aucun changement de comportement : seule la documentation de la lacune a
changé.

---

## 2026-08-22 (très tard) — `FUN_00534330` écrite : la pseudo-C avait la logique juste, pas les noms

La résolution de contact est transcrite et **le jeu l'exécute** : il traverse maintenant le pas par
personnage, la queue commune et cette fonction, et s'arrête dans `FUN_005197d0` (62 l.), la collecte
du coup reçu.

Écrites au passage : `FUN_005327b0` (`ScaleMatrix`), et les imports `winsys3d` **792** (`RotTrans`
par lot) et **812** (le registre de facteur d'échelle du GTE).

### La correspondance a tenu, et c'est le résultat le plus utile

La table établie au tour précédent — `param_1`/`puStack_18` = argument 1 (l'occupant), `local_14` =
argument 2 (le personnage) sauf quand il est passé à `RotMatrix`/`ScaleMatrix`/`SetRotMatrix` où
c'est un tampon local, `iStack_10` = argument 3 (l'entrée d'emplacement) — s'est vérifiée sur
**chacun** des accès du corps. Aucune exception, aucun cas ambigu.

**Ghidra avait la logique entièrement juste ; seuls les noms étaient faux.** C'est la conclusion
pratique à retenir : sur une fonction dont le décompilateur confond les opérandes, il ne faut pas
jeter la pseudo-C — il faut établir la table de correspondance au désassembleur, puis l'appliquer
mécaniquement. L'analyse coûte une heure ; la transcription qui suit est directe.

### L'asymétrie qui avait l'air d'un bug n'en est pas un

Les trois deltas d'entrée sont pris `entité − occupant` sur X et Z, mais `occupant − entité` sur Y.
Assez anormal pour suspecter Ghidra. Le désassemblage (0x005343ef..0x0053440a) donne :

```
005343ef  mov eax,[edi+1Ch]     ; edi = entite
005343f2  sub eax,ecx           ; - occupant.x
005343f4  mov ecx,[esi+20h]     ; esi = occupant
005343f7  sub ecx,[edi+20h]     ; - entite.y      <-- vraiment inverse
00534407  mov ecx,[edi+24h]
0053440a  sub ecx,[esi+24h]     ; entite.z - occupant.z
```

L'asymétrie est dans le binaire. Elle se comprend une fois qu'on voit à quoi Y sert : X et Z
servent au test d'appartenance en projection, Y à savoir de quel côté du plan on est — deux
questions opposées, deux signes opposés. **Conservée telle quelle.**

### Le `NormalClip` inliné, recoupé terme à terme

Les six termes que le compilateur a inlinés sont, une fois développés,
`x0*y1 + x1*y2 + x2*y0 − x0*y2 − x1*y0 − x2*y1` — **exactement** ceux de `Winsys3d.NormalClip`,
déjà reconstruit en août. Recoupés avant d'appeler, comme `FinalizeEntityPlacement2` l'avait exigé :
Ghidra les émet dans un ordre différent et avec un regroupement de soustractions qui masque la
symétrie.

Le test d'appartenance se fait **en projection XZ**, vue de dessus : trois `NormalClip` pour un
triangle, quatre pour un quad (bit 0 des attributs, quatrième sommet dans l'octet haut du mot de
coins).

### Deux blocs identiques, factorisés une fois

Les treize écritures qui publient le contact dans l'entrée d'emplacement — le vecteur de séparation,
la pose complète de l'occupant, les deux mots de normale — apparaissent **deux fois à l'identique**
dans le corps, aux deux points de contact. Extraites en `PublishContact` : c'est une unité
réellement indépendante, pas un `LAB_` promu en méthode.

### `FUN_005327b0` = `ScaleMatrix`, et l'ordinal 812

La fonction multiplie chaque **colonne** de la matrice 3×3 par un facteur en 1.12 — indices 0/3/6,
puis 1/4/7, puis 2/5/8. L'original passe par les registres GTE : le facteur dans **812**, la colonne
dans IR1..3, le produit dans MAC1..3. Transcrite telle quelle plutôt qu'en arithmétique directe :
ces registres sont observables depuis le reste du moteur.

**812 identifié par ce corps** : c'est le registre de facteur scalaire, lu en `ushort`. Ajouté à
`EnsureGteRegisters` — d'où le second corps que l'oracle signale, un ajout voulu.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : deux corps signalés, `RunWorldMapTick` (le
changement en cours) et `EnsureGteRegisters` (l'ajout de 812) — les deux voulus ; 41 méthodes
nouvelles. Tests de disposition : inchangés, 39 champs. Playtest `-Entry 0` : le contact s'exécute
pour chaque occupant des neuf cellules, et l'arrêt est sur `FUN_005197d0`.

---

## 2026-08-22 (fin de journée) — le pas de simulation du monde s'exécute en entier

**L'exception a quitté la simulation.** Les quatorze appelées du cas 0 tournent, `FUN_0050bf20`
avec elles, pour chaque membre du groupe et à chaque image. Ce qui lève maintenant est la lacune du
**bloc d'affichage principal** (`0x00517aa7`..`0x00519703`), posée dans `RunWorldMapTick` lui-même
il y a deux jours — c'est-à-dire que tout ce qui séparait le jeu de cette lacune est écrit.

Cinq appelées de plus dans cette passe : `FUN_005197d0`, `FUN_00534cd0`, `FUN_00474eb0`,
`FUN_00474db0`, `FUN_00474cd0`.

### Deux noms corrigés, et ce que ça dit sur la méthode

`FUN_00534cd0` s'appelait `UpdateEntityLighting` dans ce dépôt, et `FUN_00474eb0`
`FindNearbyObstacle`. Les deux noms venaient d'une **supposition faite au site d'appel**, avant
lecture du corps. Les deux étaient faux :

* `FUN_00534cd0` est le **test des volumes de déclenchement** : elle parcourt la liste de morceaux
  en attente et teste l'entité contre une suite de boîtes alignées sur les axes, chacune portant un
  type et un index de script. Rien à voir avec l'éclairage.
* `FUN_00474eb0` cherche **l'adversaire le plus proche** : selon le bit `0x800000`, un membre du
  groupe balaie la table des entités de scène et une entité de scène balaie celle du groupe. Ce
  n'est pas un obstacle, c'est une cible.

**La règle que `Strategy.md` énonce déjà — ne renommer que quand le rôle est établi — a une
contrepartie qui manquait : un nom posé trop tôt ne se signale jamais tout seul.** Il se lit comme
un fait acquis pendant des semaines. Les deux ici n'ont été corrigés que parce qu'il a fallu ouvrir
les corps pour les transcrire.

### L'empaquetage des volumes de déclenchement

Les boîtes tiennent dans 0x10 octets et leur empaquetage est irrégulier, ce qui vaut d'être écrit
une fois : mot 1 = `{centre X, centre Y}`, mot 2 = `{centre Z, demi-étendue X}`, mot 3 =
`{demi-étendue Y, demi-étendue Z}`. Le mot 0 porte l'état sur son octet bas, le type et ses
drapeaux sur le suivant, l'index de script sur sa moitié haute.

Cinq types se distinguent par les quatre bits bas du second octet : **1** compte les passages,
**2** lance son script en sortant, **3** le lance à la première sortie seulement, **8** change la
couleur de fond et pose le bit 3 du monde, **9** fait naître l'effet 0x59 quand les points de vie
ou de magie ont bougé. Tout le reste lance son script en **entrant**.

Le test `(*entité & 0xfc0000) == 0x800000` — « est-ce un membre du groupe » — revient six fois et
conditionne la plupart des effets : une entité quelconque traverse la plupart des volumes sans rien
déclencher.

### Une constante de grille qui replie son décalage

`FUN_005197d0` balaie 3×3 cellules comme `FUN_00519df0`, mais avec la constante **`0x389f`** au
lieu de `0x38a0` : le `-1` du balayage est replié dedans plutôt qu'écrit à part. La conséquence est
réelle et conservée — pour un personnage dans la rangée 0, la première des trois rangées balayées
est `0x389f`, c'est-à-dire **la dernière rangée de l'autre grille**, les deux pages étant adjacentes
(0x1c4000 et 0x1c5000). Le garde `row < 0x71800` ne borne que le haut.

### Un `void` qui rend quelque chose

Ghidra déclare `FUN_00474eb0` en `void` alors que son appelant lit son résultat. Ce n'est pas un
paramètre perdu de plus : ses deux branches sont des appels **terminaux**, et le résultat de
l'appelée traverse tel quel. Un `void` de Ghidra sur une fonction dont toutes les sorties sont des
appels terminaux ne prouve rien.

### Où en est le jeu

Le débit est encore à **1 primitive par image** : la simulation tourne, mais rien n'est encore
émis. C'est attendu — le bloc d'affichage est précisément ce qui manque, et c'est le prochain
chantier. Il commence par `FUN_004011c0`, `FUN_00532b40` et deux `FUN_00533000`.

Des lacunes subsistent **hors du chemin au repos** et ne lèveront qu'en jeu réel : les gestionnaires
de posture `0x0051A6B0`/`0x0051A700`/`0x0051A710` et de commande `0x0050DD00`/`0x0050DFF0` (tous
absents de `Blaze.c`), plus une douzaine d'appelées qui, elles, y sont.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : deux corps signalés, `RunWorldMapTick` (le
changement en cours) et `EnsureGteRegisters` (l'ajout de l'ordinal 812) — les deux voulus ;
46 méthodes nouvelles. Tests de disposition : inchangés, 39 champs.

---

## 2026-08-22 (fin) — le bloc d'affichage : les curseurs de primitives sont résolus

Le bloc d'affichage principal **n'est pas transcrit**. Ce qui est fait, c'est la lecture que
`CLAUDE.md` signale comme la plus chère de tout le dépôt, et sans laquelle il ne faut pas en écrire
une ligne.

### Six noms, deux objets

Ghidra répand dans ce bloc des `unaff_EBX`, `unaff_ESI`, `unaff_EDI`, un `local_60` et un
`piStack_74`. Ce ne sont que **deux** objets, et ce sont les deux emplacements de pile que
`FrameBegin` a remplis en tête de la fonction :

| emplacement | rôle | noms que Ghidra lui donne |
|---|---|---|
| `[esp+0x14]` | la base d'ordering table | `unaff_EBP`, `unaff_EDI`, `local_60` |
| `[esp+0x10]` | le curseur de primitives | `unaff_ESI`, `unaff_EBX`, `piStack_74` |

Le premier appel de dessin le montre en six instructions :

```
00517e95  mov  ecx,[esp+10h]        ; le curseur
00517e99  mov  edx,[esp+14h]        ; la base d'OT
00517eac  push ecx                  ; arg2 = curseur
00517ead  push edx                  ; arg1 = base
00517eae  call 0051F940h
00517eb6  mov  [esp+10h],eax        ; le resultat REVIENT dans le curseur
```

Le motif `mov [esp+10h],eax` après l'appel se répète à tous les sites de dessin du bloc, et la base
est systématiquement passée en `base + 8`.

### Pourquoi ça valait de s'arrêter là

C'est exactement le piège que `CLAUDE.md` décrit comme ayant déjà coûté une journée :
`DrawFlatPrimitive` transcrit fidèlement plantait quand même, parce que la **convention** de la
liste d'affichage vit du côté DLL. Un curseur mal chaîné ne lève pas une exception managée avec une
pile lisible — il produit une violation d'accès **dans winsys3d**, sans rien qui pointe vers la
ligne fautive.

Écrire 700 lignes d'émission de primitives sans cette table sous les yeux, c'est se préparer cette
séance de débogage. La table est donc gravée dans le pavé qui précède la lacune, là où celui qui
écrira le corps la verra.

### L'inventaire, mesuré

Quinze appelées du bloc sont **déjà dans le dépôt** — le moteur de dessin a été transcrit plus tôt
(`FUN_0051c120`, `FUN_00521df0`, `FUN_0051f9c0`, `FUN_00545340`, `FUN_005443a0`, `FUN_0051d1f0`,
`FUN_0051f650`, `FUN_00520ae0`, `FUN_00521f20`, `FUN_0051be20`, `FUN_0051bdb0`, `FUN_00533000`,
`FUN_0051c300`, `FUN_00544350`, `FUN_0052a280`). C'est ce qui rend ce bloc abordable malgré sa
taille.

Seize manquent, **toutes dans `Blaze.c`** — aucune ne demande de désassemblage :

| | | |
|---|---|---|
| `FUN_0051aa30` 594 l. | `FUN_0051fa40` 441 l. | `FUN_0051e700` 423 l. |
| `FUN_0051c420` 374 l. | `FUN_00521000` 365 l. | `FUN_004011c0` 363 l. |
| `FUN_00522140` 315 l. | `FUN_005309b0` 147 l. | `FUN_0051be90` 74 l. |
| `FUN_005320f0` 51 l. | `FUN_005321f0` 51 l. | `FUN_00532b40` 29 l. |
| `FUN_0052a7d0` 20 l. | `FUN_0051f940` 18 l. | `FUN_00538bc0` 13 l. |
| `FUN_0051c250` 10 l. | | |

Le sous-bloc de mise au point (~150 l. sur les 700) est gardé par `DAT_005de020`, nul au
chargement : à transcrire en dernier, et probablement jamais atteint en jeu normal.

**La méthode qui a marché tout au long de cette carte du monde s'applique telle quelle** : écrire le
corps, poser une lacune nommée par appelée, et laisser le jeu dire lesquelles il emprunte
réellement. Sur les gestionnaires d'entité, quatre ont suffi là où dix-sept étaient annoncées.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : deux corps signalés, les deux voulus et
inchangés depuis la passe précédente. **Aucun changement de comportement** : seule la documentation
de la lacune a bougé.

---

## 2026-08-22 (nuit) — la tête du bloc d'affichage, et une supposition démentie par la mesure

Le jeu **entre dans le bloc d'affichage**. Sa tête est transcrite — tout l'état de vue, jusqu'à la
première primitive — et l'arrêt est maintenant sur `FUN_004011c0`, à l'intérieur du bloc.

Écrites : la tête (~130 l.), `FUN_00532b40`, `FUN_005320f0`, `FUN_005321f0`, `SmoothCameraStep`.

### Ce que la tête fait

Mode de vue, défilement de textures sur les deux tranches de la table de morceaux, lissage de la
pose de caméra vers `DAT_005e9ae0` (un quart de l'écart, contre un huitième pour l'orientation de
scène), construction de la matrice de caméra, choix du **personnage suivi** — celui dont le
compteur est le plus haut — puis du **plus éloigné de celui-là**, dont le milieu devient la cible
de caméra. Enfin le centroïde du groupe, le décalage monde et `SetGeomOffset(160, 120)`.

Le décalage monde nie X et Z mais **pas Y** — asymétrie de l'original, conservée, et de la même
famille que celle relevée dans `FUN_00534330`.

### Le mode de vue au repos est 1, pas 0

Le message de lacune que je venais d'écrire annonçait « le mode 0 est la seule branche que la carte
du monde au repos emprunte ». **Faux** : le jeu a levé avec `mode 1`. Corrigé sur place.

C'est un cas d'école du travers que ce fichier documente depuis août — *une difficulté annoncée qui
n'existait pas a failli dicter l'ordre de travail*. Ici la supposition était moins coûteuse mais de
même nature : elle aurait fait écrire d'abord la branche courte (une trentaine de lignes) au lieu de
la branche longue, le **pilotage manuel de la caméra**, qui est ce dont le jeu a besoin. La lacune
porte maintenant la mesure, pas la supposition.

### Trois empaquetages de `FUN_00532b40`, tous vérifiés au désassembleur

Le mouvement de scène scripté lit un flux d'enregistrements de 0x10 octets et ajoute des deltas aux
**consignes** que `SmoothSceneOrientationAndBackColour` poursuit — la boucle se referme. Trois de
ses quatre écritures ont une forme inattendue, et il aurait été tentant de les prendre pour des
artefacts :

* `0x005e9310` reçoit **deux additions 16 bits indépendantes**, sans retenue de l'une vers l'autre.
* `0x005e9314` et `0x005e92bc` sont lus et écrits en **dword** puis masqués à 16 bits : leur moitié
  haute est donc **remise à zéro à chaque tour**.
* `0x005e92b8` voit ses deux moitiés **croisées** — la nouvelle moitié basse vaut l'ancienne moitié
  haute plus celle du flux, et réciproquement. `shr edi,10h ; add eax,edi ; shl edx,10h ; or eax,edx`
  ne laisse aucun doute.

**Ghidra avait les trois justes.** C'est le pendant utile des six cas où il avait tort : sa
pseudo-C n'est pas suspecte partout, elle est suspecte aux endroits que le désassemblage sait
départager — et il faut y aller pour le savoir, dans les deux sens.

### Ce qui reste du bloc

La **moitié dessin**, à partir de `0x00517e7c`. En plus des seize appelées déjà inventoriées, elle
demande de relever une vingtaine de descripteurs de sprite dans le binaire (`0x00564788`,
`0x005646b8`, `0x00566c18`..`0x00566c38`, `0x00564900`..`0x00564958`, `0x00563348`..`0x00563360`)
et de reconstruire les blocs de trois entiers que l'appelant bâtit sur sa pile — Ghidra les rend en
écritures `CONCAT22` éparpillées, sans jamais montrer la structure.

Le premier obstacle concret est `FUN_004011c0` en mode 1 (le pilotage manuel), qui tire
`FUN_00402410`, `FUN_004022b0`, `FUN_00401fb0` et `FUN_00401c90`.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : deux corps signalés, les deux voulus et
inchangés ; 48 méthodes nouvelles. Tests de disposition : inchangés, 39 champs. Playtest `-Entry 0` :
le jeu entre dans le bloc d'affichage et s'arrête sur `FUN_004011c0`, mode 1.

---

## 2026-08-22 (nuit) — `FUN_004011c0` mesurée : c'est un sous-système, pas une fonction

`FUN_004011c0` **n'est pas transcrite**. Ce tour a mesuré ce qu'elle demande, relevé ses deux tables
de constantes, et établi que son état est déjà modélisé — ce qui change son coût, pas sa taille.

### Douze appelées, toutes petites, toutes groupées

Les appelées du mode 1 tiennent entre `0x00401bf0` et `0x00402d60` : c'est **un sous-système de
caméra contigu**, pas une dispersion.

| | | | |
|---|---|---|---|
| `FUN_00401bf0` 38 l. | `FUN_00401c90` 121 l. | `FUN_00401fa0` 8 l. | `FUN_00401fb0` 68 l. |
| `FUN_004022b0` 42 l. | `FUN_00402410` 66 l. | `FUN_004025a0` 16 l. | `FUN_00402610` 15 l. |
| `FUN_00402670` 24 l. | `FUN_00402710` 40 l. | `FUN_004029e0` 75 l. | `FUN_00402d60` 24 l. |

Aucune ne demande de désassemblage. Mais **les deux premières du chemin mode 1 sont
`FUN_00402410` et `FUN_004022b0`** : elles sont appelées à la deuxième et troisième instruction de
la branche, donc rien ne bouge sans elles. Écrire `FUN_004011c0` seule ne ferait pas avancer le jeu
d'une image — c'est pour ça que ce tour s'arrête sur la mesure plutôt que sur un corps à moitié
utile.

Le total réel du morceau est de l'ordre de **900 lignes** : les 363 de `FUN_004011c0` plus les ~540
de ses appelées.

### L'état est déjà là, et c'est ce qui rend le morceau abordable

Les globales que ce sous-système remue vivent presque toutes dans deux blocs **déjà modélisés** :

* `0x005712xx`..`0x005713xx` → `GameMemory.ViewParams`, créé à l'étape (1) du plan du 2026-08-21.
  La note de l'époque disait « il alimente `s_ViewBlock_006067c0` par addition, reçoit la caméra en
  deux exemplaires parallèles, et porte des drapeaux à `0x0057139c` » — c'est exactement ce que
  `FUN_004011c0` fait. **La description avait été écrite sans avoir lu la fonction qui s'en sert**,
  et elle tombe juste.
* `0x006067xx` → `s_ViewBlock_006067c0`.

Deux tables restent à modéliser : `0x00604ba0` et `0x00604d30`, parallèles et espacées de `0x190`,
indexées par `(cellule * 200 + n) * 4` — le cache de maillage que la caméra interroge pour ne pas
traverser le décor. **Leur étendue n'est pas établie** et l'indexation apparente déborderait l'écart
entre les deux bases : à trancher au désassembleur avant de les allouer, plutôt que de reproduire un
débordement qui n'existe peut-être pas.

### Deux tables de constantes relevées, et pourquoi elles étaient illisibles

Écrites dans `DataSymbols.md`. `DAT_0054f040` = 1600 et `DAT_0054f044` = 900 sont les bornes haute
et basse de la hauteur de caméra.

Les deux tableaux de `0x0054f048` et `0x0054f070` sont plus intéressants : neuf `short` **à pas de
quatre octets** chacun, valant `{-1,0,1,-1,0,1,-1,0,1}` et `{-1,-1,-1,0,0,0,1,1,1}`. Pris ensemble,
ce sont les **neuf décalages d'un voisinage 3×3**.

**Le pas de quatre octets pour des valeurs de deux est ce qui les rendait illisibles.** Lus en
dwords — le réflexe quand on dumpe une zone — ils donnent `0xffff, 0, 1, 0xffff…`, ce qui ressemble
à des sentinelles. C'est la lecture en `short`, celle que fait le code, qui révèle la grille. Un
relevé de table doit se faire à la largeur que l'instruction utilise, pas à celle du dump.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : deux corps signalés, les deux voulus et
inchangés. **Aucun changement de comportement** : le jeu s'arrête au même endroit, seules la
documentation de la lacune et `DataSymbols.md` ont bougé.

---

## 2026-08-22 (fin) — le sous-système de caméra, écrit d'un bloc

`Screens/State03/WorldMapCamera.cs`, nouveau fichier : `FUN_004011c0` et douze de ses quatorze
appelées. **Le jeu l'exécute en entier**, et l'arrêt est revenu sur la lacune délibérée de la moitié
dessin. Toute la tête du bloc d'affichage tourne donc, caméra comprise.

### Pourquoi un fichier à part

Les quatorze fonctions tiennent entre `0x00401bf0` et `0x00402d60` : c'est une **unité de
compilation de 1998** prise telle quelle. Un seul écran l'appelle, donc la règle de routage (2)
d'`ARCHITECTURE.md` la place dans `Screens/State03/`, et sa cohésion justifie son fichier plutôt
qu'un ajout de 900 lignes à `WorldMapTick.cs`.

### Le bloc de vue porte quatre poses parallèles

C'est la lecture qui rend le reste évident, et elle ne se voit pas dans les noms de Ghidra
(`DAT_006067c0`, `d0`, `e0`, `e8`, `f0` — cinq symboles sans rapport apparent) :

```
+0x00  c0/c4/c8   la position visee     (« oeil »)
+0x10  d0/d4/d8   la position courante  (« cible »)
+0x20  e0/e2/e4   l'orientation visee
+0x28  e8/ea/ec   l'orientation courante
+0x30  f0/f4/f8   la position d'arrivee du glissement
```

`PublishCameraFrame_004022B0` fait glisser courant → précédent une fois par image ; c'est ce
décalage qui donne aux autres fonctions un « avant » et un « après ». Nommées `ViewEye`,
`ViewTarget`, `ViewAngle`, `ViewAngleGoal`, `ViewTargetGoal`, les quinze symboles deviennent cinq
vecteurs.

### Une cascade de labels de plus qui n'était pas irréductible

`FUN_004027f0` classe le déplacement du groupe en huit secteurs. Ghidra en rend huit `goto`
entrelacés plus un `unaff_DI` jamais initialisé — l'allure d'un graphe impossible. Ce n'en est pas
un : chaque branche assigne son secteur puis saute au maillon suivant, **qui échoue toujours**. Le
seul effet net est le secteur, et les huit plages couvrent la ligne entière, donc `unaff_DI` est
toujours écrit avant d'être lu. Rendu en `if / else if`.

C'est le troisième cas de cette forme (après `FinalizeEntityPlacement2` et `TickFollowingMember`).
**Un `unaff_` de Ghidra n'est pas toujours un registre perdu** : c'est parfois simplement une
variable que toutes les branches écrivent, et que le décompilateur n'a pas su relier.

### Un bug que j'ai introduit, et ce qu'il apprend

La garde de `AdvanceScriptedSceneMotion_00532B40` était écrite
`*(sbyte*)Mem.Entity(0x005645fc) == -1`. **`0x005645fc` n'est pas dans `EntityArena`** (qui commence
à `0x005eca00`) : `Mem.Entity` en a fait un décalage **négatif**, la garde a lu de la mémoire
quelconque, elle est passée à tort, et la fonction a déréférencé un pointeur nul.

Le symbole existait déjà dans le dépôt sous `s_Unknown_005645fc`, et c'est un **octet** comparé à
`0xff`, pas un `sbyte` comparé à `-1`.

Deux leçons, et la seconde est la vraie : les accesseurs d'arène **ne vérifient pas leurs bornes**,
donc une adresse hors plage rend un pointeur valide sur autre chose. Et surtout — **chercher le
symbole dans le dépôt avant d'inventer un accès**. Il était là, correctement typé, à trois lignes
de `grep`.

### Ce qui reste du sous-système

Le sous-bloc d'**occlusion** (`0x00401913`..`0x00401b6d`) et ses deux appelées `FUN_004029e0` /
`FUN_00402d60`. Le blocage n'est pas le code — c'est la **donnée** : les deux tables parallèles
`0x00604ba0` et `0x00604d30`, espacées de `0x190` et indexées par `(cellule * 200 + n) * 4`, ont une
étendue que l'indexation apparente contredit. À trancher au désassembleur avant de les allouer.

**Il n'est pas sur le chemin au repos** : la garde `DAT_00571348 == 0 && DAT_00571380 == 0` rend la
main avant, et le playtest le confirme — la lacune n'a pas été atteinte.

### Ce que la vérification vaut, et ce qu'elle ne vaut pas

Build 0 erreur, 42 avertissements (inchangé). Oracle : deux corps signalés, les deux voulus et
inchangés ; 64 méthodes nouvelles. Tests de disposition : inchangés, 39 champs. Playtest : la tête
du bloc d'affichage traverse en entier.

**Mais rien ne s'affiche encore**, donc il n'y a aucun contrôle à l'écran : on sait que ce code
*tourne*, pas qu'il est *juste*. Les largeurs des quarante champs de `ViewParams` sont déduites de
l'usage de la pseudo-C, pas d'une passe de désassemblage — une largeur fausse écrirait chez le
voisin sans rien casser de visible aujourd'hui. C'est le premier contrôle à refaire quand la moitié
dessin donnera une image.

---

## 2026-08-22 (fin) — la moitié dessin : les deux préalables levés, le corps reste

La moitié dessin **n'est pas transcrite**. Ce tour a levé les deux obstacles que le tour précédent
avait nommés, et ils étaient de la donnée et de la structure, pas du code.

### Les descripteurs sont relevés

Onze descripteurs d'atlas de 8 octets et cinq tables de la barre d'état, tous dans
`DataSymbols.md`. La plus parlante est `0x00564900` : neuf dwords `{1, 2, 4, 8, 0x10, 0x20, 0x40,
0x100, 0x200}` que la barre teste contre `bloc + 0x140`, **un pictogramme par bit d'état**. La borne
de la boucle (`< 0x564924`) confirme les neuf entrées sans qu'on ait à deviner.

Les trois tables de `0x00564928`, `0x00564948` et `0x00564958` sont trois dispositions de X pour les
panneaux des quatre membres, choisies par `DAT_005de160` — la position du menu dans son animation
d'ouverture. Les panneaux se réarrangent pendant que le menu glisse.

### Le bloc de trois entiers de la pile

C'est le second préalable, et le plus important parce qu'il ne se voit nulle part dans la pseudo-C.
Toutes les fonctions de dessin prennent en troisième argument l'adresse de **trois emplacements de
pile consécutifs** :

```
bloc[0]  pointeur vers un descripteur d'atlas de 8 octets
bloc[1]  transmis verbatim comme dword de texture/drapeaux
bloc[2]  la position ecran, X dans le mot bas et Y dans le mot haut
```

Ghidra rend les trois écritures séparément, sous les noms de leurs décalages — `uStack_48`,
`uStack_44`, `uStack_40` pour un bloc, puis `uStack_40`, `uStack_3c`, `uStack_38` pour le suivant —
et la position par des `CONCAT22` sur les deux moitiés de `bloc[2]`. **Reconnaître que trois
`uStack_NN` consécutifs dont le premier reçoit une adresse de descripteur forment un bloc** est ce
qui rend ce code transcriptible ; sans cela chaque appel a l'air de prendre un pointeur vers rien.

La disposition elle-même n'est pas neuve : elle avait été résolue en août sur le désassemblage de
`FUN_0051c120` et est documentée sur `Primitives2d.EmitTextSprite`. **Ce qui manquait était de
faire le lien** entre cette documentation et les `uStack_NN` du bloc d'affichage — deux endroits du
dépôt qui décrivaient la même chose sans se citer.

### Ce qui reste, et pourquoi ce tour s'arrête là

Le corps (~570 l.) et cinq appelées du chemin principal : `FUN_0051c420` (374 l.),
`FUN_0051be90` (74), `FUN_0051f940` (18), `FUN_00538bc0` (13), `FUN_0051c250` (10). Les onze autres
appelées sont **déjà dans le dépôt** — le moteur de dessin a été transcrit plus tôt, et c'est ce qui
rend ce bloc abordable malgré sa taille.

Le total est du même ordre que le sous-système de caméra écrit juste avant : environ 1 100 lignes.
La différence est le mode de défaillance. Un curseur de primitives mal chaîné ne lève pas une
exception managée — il produit une violation d'accès **dans winsys3d**, sans pile utilisable, et le
dépôt a déjà payé une journée pour ça sur `DrawFlatPrimitive`. C'est le seul endroit de cette carte
du monde où « écrire vite et voir » ne marche pas.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : deux corps signalés, les deux voulus et
inchangés. **Aucun changement de comportement** : seules `DataSymbols.md` et la documentation de la
lacune ont bougé.

---

## 2026-08-22 (nuit) — la moitié dessin : écrite, essayée, désactivée

Le corps de la moitié dessin est écrit — la première section, la rose des vents, et les lacunes
nommées des trois autres — puis **essayé, et désactivé**. Il corrompt la mémoire.

### Ce qui s'est passé

À l'exécution, le jeu a levé une `NullReferenceException` **dans la caméra**, deux appels et une
image plus loin que la faute : `PublishCameraFrame_004022B0` a trouvé un pointeur nul à
`0x005eca94`, dans l'arène d'entités. La moitié dessin n'y écrit rien.

L'explication tient en une ligne : les fonctions de dessin reçoivent l'**adresse d'un bloc de pile**
dont elles lisent plusieurs champs, et je n'avais établi la disposition que d'un seul —
`DrawTextRun` (`FUN_0051c120`), résolue au désassembleur en août. Pour `DrawSpriteVariant`,
`DrawFlatPrimitive` et `FUN_0051c420`, j'ai rempli les champs que la pseudo-C montre et laissé les
autres à zéro. La fonction calcule alors une adresse sur ces zéros et **écrit hors de la liste
d'affichage**.

### Pourquoi c'est le pire endroit pour improviser

Ici la faute s'est vue : elle est tombée dans une de nos arènes, et le CLR a levé. **Dans le jeu
réel elle part dans `winsys3d`**, sans pile utilisable — exactement ce que `CLAUDE.md` décrit sur
`DrawFlatPrimitive`, où « une transcription fidèle plantait quand même parce que la convention de la
liste d'affichage vit du côté DLL ».

J'avais écrit cet avertissement moi-même deux tours plus tôt, en expliquant pourquoi je ne voulais
pas écrire ce bloc à la légère. Il s'est vérifié au premier essai.

### Ce qui manque n'est pas du code

Le corps se lit et se relit ; il est conservé dans `DrawWorldMapFrame` et il est **appelé nulle
part**, avec un `throw` qui dit pourquoi. Ce qui manque est la **disposition des blocs de pile**,
fonction par fonction :

| fonction | disposition |
|---|---|
| `FUN_0051c120` `DrawTextRun` | **établie** (août, désassemblage) — sert de modèle |
| `FUN_00521df0` `DrawSpriteVariant` | à relever |
| `FUN_0051f9c0` `DrawFlatPrimitive` | à relever |
| `FUN_0051c420` | à relever, et la fonction elle-même à écrire (374 l.) |

Chacune se relève comme `FUN_0051c120` l'a été : `disasm` sur le corps, suivre les `[param_3 + N]`.
C'est du travail borné et sûr, et c'est le vrai préalable — les descripteurs et les tables de
panneaux, eux, sont relevés et en place.

### La leçon, et elle n'est pas nouvelle

Le dépôt savait déjà que la convention de la liste d'affichage vit côté DLL et qu'une transcription
fidèle ne suffit pas. Ce que cet essai ajoute : **ça vaut aussi pour les blocs que l'appelant bâtit
sur sa pile**. Un champ laissé à zéro n'est pas « une valeur par défaut », c'est une adresse.

Reconnaître le bloc de trois entiers ne suffisait donc pas — il fallait aussi savoir combien de
champs chaque appelée lit. Les deux préalables annoncés au tour précédent en cachaient un
troisième.

### Vérification

Build 0 erreur, 42 avertissements (inchangé). Oracle : deux corps signalés, les deux voulus et
inchangés. Tests de disposition : inchangés, 39 champs. Playtest : le jeu s'arrête sur la lacune
nommée, **sans corruption** — l'état d'avant l'essai est rétabli.

---

## 2026-08-22 (nuit, suite) — la moitié dessin est rallumée : trois défauts, tous de disposition

La moitié dessin, écrite puis désactivée quelques heures plus tôt parce qu'elle corrompait la
mémoire, **s'exécute maintenant** et s'arrête sur une lacune nommée. Ce qui la bloquait n'était pas
du code manquant : c'étaient trois erreurs de disposition, toutes tranchées au désassembleur.

### 1. Le bloc de sprite fait sept champs, pas trois

Le dépôt décrivait « trois entiers consécutifs », relevés en août sur `FUN_0051c120` — qui se
trouve être la seule de la famille à n'en lire que trois. Les autres en lisent sept, sur `0x12`
octets : descripteur, mot de base, position X/Y, **échelle** X/Y, et un indice.

Deux conséquences. `+0x0c`/`+0x0e` sont des échelles en virgule fixe (`0x1000` = 1.0), pas des
demi-tailles comme le nom que `Primitives2d` leur donnait ; et allouer le bloc sur trois entiers
faisait lire `+0x0c`..`+0x10` **hors de l'allocation**. `Screens/DebugParty.cs` documentait déjà la
disposition complète, sur du code qui tourne — elle n'avait simplement jamais été rapprochée de
celle de `DrawTextRun`.

### 2. Un descripteur d'atlas ne se relève pas seul

Le champ `+0x10` est un indice qui s'ajoute au descripteur passé : la fonction lit
`bloc[0] + indice * 8`. La rose des vents passe `&DAT_005646c0` avec un indice 0..3 et lit donc
quatre descripteurs **voisins dans le binaire**.

Le relevé d'origine rangeait onze descripteurs isolés dans un tableau de onze entrées, dans l'ordre
où je les avais rencontrés. Indexer en avant y donnait le mauvais descripteur dès l'indice 1 et
sortait du tampon de 88 octets à partir du quatrième — d'où le couple couleur/attributs pris dans
le tas, et la corruption. `SpriteAtlas` relève désormais **deux plages contiguës**,
`0x00564680..0x00564900` et `0x00566c18..0x00566c40`, adressées par VA.

Au passage, une ligne du relevé d'août était fausse : `0x00566c20` portait `0x28180000 /
0x02300008`, décalage d'un octet. Le vidage aligné donne `0x08281800 / 0x02300000`.

### 3. Ghidra place deux champs au mauvais décalage

Dans le seul bloc d'affichage de `FUN_00516fc0` :

| site | pseudo-C | désassemblage |
|---|---|---|
| rose des vents | `piStack_4c = CONCAT22(…, X)` → `+0x04` | `mov [esp+3Ch],cx`, bloc à `esp+34h` → **`+0x08`** |
| indicateur d'écran | `uStack_40 = CONCAT22(…, pouls)` → `+0x08` | `[esp+40h]` et `[esp+46h]` → **`+0x0c` et `+0x0e`** |

La transcription initiale avait fidèlement copié les deux erreurs. Dans les deux cas le résultat
est absurde à la lecture — un X dans le mot de couleur, une position d'environ `0x1000` sur un
écran de 320 pixels — et **c'est l'absurdité qui est le signal**, pas un doute sur Ghidra en
général. La règle qui en sort : sur un bloc de pile passé par adresse, la pseudo-C ne fait pas foi.

La boucle de la rose des vents fait par ailleurs **quatre** tours, pas huit : `0` à `0x1000` par
pas de `0x400`, les quatre points cardinaux. Le commentaire disait huit.

### Ce que ça a coûté, et ce que ça enseigne

Le tour précédent avait conclu « ce qui manque n'est pas du code, c'est la disposition ». C'était
juste, mais il manquait de voir que **le dépôt tenait déjà la réponse à deux endroits** :
`DebugParty.cs` décrivait les sept champs, et la contiguïté se déduisait de
`bloc[0] + indice * 8`, une expression déjà transcrite dans `Primitives2d`. Chercher au
désassembleur était la bonne idée ; commencer par relire ce qui tourne déjà l'aurait été davantage.

Un défaut latent de 1998 est ressorti du relevé : **LB-008**, l'indicateur d'écran émet son mot de
base depuis une pile jamais écrite. Transcrit tel quel, non comblé.

### Vérification

Build 0 erreur. Oracle : trois corps signalés — les deux voulus, plus `DrawSpritePrimitive` dont
seuls deux noms de variables changent (`halfW`/`halfH` → `scaleX`/`scaleY`, la lecture correcte).
Playtest `-Entry 0` avec la séquence de formation du groupe : la carte du monde est atteinte
(1 primitive par image, la signature de l'état 3), la moitié dessin s'exécute, et le jeu s'arrête
sur `FUN_0051c420 (0x0051c420) : non transcrite` — une lacune franche, plus aucune corruption, et
la caméra ne lève plus.

### Ce qui reste sur le chemin

Cinq appelées, toutes dans `Blaze.c` : `FUN_0051c420` (374 l., celle qui bloque maintenant),
`FUN_0051e700` (423), `FUN_0051fa40` (441), `FUN_00521000` (365), `FUN_00522140` (315), plus les
courtes `FUN_005309b0` (147), `FUN_0051be90` (74), `FUN_0051f940` (18), `FUN_00538bc0` (13),
`FUN_0051c250` (10). Et les deux corps encore à écrire : `DrawPartyPanels` et `DrawWorldCells`,
environ 200 lignes chacun.

---

## 2026-08-22 (nuit, suite) — FUN_0051c420 : le rendu de modèle statique

`FUN_0051c420` est transcrite, dans `Engine/Model3d.cs` à côté de sa sœur `FUN_0051d1f0`. La carte
du monde pose maintenant son décor et sa rose des vents, et s'arrête sur la lacune suivante,
`DrawPartyPanels`.

### Le placement : une fonction d'`Engine/`, pas d'un écran

Elle va en `Engine/Model3d.cs` par les deux premières règles d'`ARCHITECTURE.md` à la fois : son VA
tombe dans la plage de rendu de modèles, et cinq sites d'appel la sollicitent depuis plusieurs
écrans. Elle y trouve tout ce dont elle a besoin — `LoadPointAsMatrixTranslation`,
`ProjectOriginThroughMatrix`, `NormalClip`, `ReadMac0`, `ScaleMatrixSaturating` et les dix-sept
registres GTE existaient déjà pour la sœur animée.

C'est ce qui explique que 374 lignes de pseudo-C tiennent en une méthode et deux boucles : **la
moitié du travail avait été faite en écrivant `FUN_0051d1f0`**, et personne ne l'avait remarqué
parce que les deux fonctions ne se citent nulle part.

### Ghidra a perdu trois paramètres sur cinq, et coupé chacun en deux

Il déclare `FUN_0051c420(int param_1, int *param_2)` là où les cinq appelants poussent cinq
arguments (`add esp,14h`). Le prologue — `sub esp,50h` puis quatre `push` — place les arguments à
`[esp+64h..74h]`, ce qui donne :

| réel | `[esp+…]` | noms Ghidra |
|---|---|---|
| `otEntry` | `64h` | `puStack_10` **et** `iStack_4` |
| `prim` | `68h` | `unaff_retaddr` **et** `puStack_c` |
| `model` | `6Ch` | `param_1` **et** `uStack_8` |
| `placement` | `70h` | `param_2` |
| `flags` | `74h` | aucun |

**Le dédoublement est plus dangereux que la perte.** Un paramètre absent ne compile pas ; un
paramètre sous deux noms compile et ment. On lit `uStack_8 + 0x18` à côté de `param_1 + 0x3c` et on
croit à deux structures. Pire : les emplacements d'arguments servent de brouillon une fois leur
valeur consommée, si bien que `iStack_4` désigne **l'entrée d'OT** au début de la fonction et un
**temporaire du produit vectoriel** dans les boucles. Les traduire par une seule variable aurait
détruit le pointeur d'OT à la première face émise.

Deux constantes de primitive, `uStack_48` et `uStack_44`, ne sont **jamais affectées** dans la
pseudo-C. Elles se relèvent en `0x0051c82a..0x0051c871` : le mot de CLUT et le mot de page de
texture, plus simples que ceux de la sœur (ni terme `0x1c0`, ni relecture du premier
enregistrement, ni borne `0x7ec0`).

### Ce qui reste faux dans la moitié dessin — et ce n'était pas dans cette fonction

Le premier essai a planté dans `EmitStaticTriangles`. La faute n'était pas dans la transcription
mais au **site d'appel** : `*(byte**)s_ModelResultB_005f7b40` ne déréférence qu'une fois, alors que
`RelocateModelBundle` range dans ce slot le **tableau d'offsets** du paquet, dont le modèle est
l'élément 0.

Ghidra écrit `*DAT_005f7b40` en typant le symbole comme un pointeur — il le passe par adresse en
`0x00532640` et le relit `DAT_005f7b40[1]` / `[3]` ailleurs — ce qui masque un niveau. Un seul
déréférencement donne le tableau lui-même, et son « en-tête » se lit alors comme un modèle dont
`+0x08` est un pointeur et `+0x28` un petit entier.

**C'est le vidage qui l'a tranché, pas la relecture.** Deux passes de relecture n'avaient rien
donné ; un `File.WriteAllText` d'une ligne — l'en-tête du modèle, ses quatre compteurs et ses
quatre seaux — a montré `+0x24 = 0x00010001` et `+0x28 = 0x00010002` là où il fallait des
pointeurs, et la réponse était immédiate. Sur du pointeur, imprimer coûte moins cher que raisonner.

### Un piège de protocole qui a failli coûter cher

Deux exécutions intermédiaires ont levé un `NullReferenceException` dans
`PublishCameraFrame_004022B0` — **exactement la signature de la corruption de la veille**. J'ai
d'abord cru à une régression de la fonction neuve. C'était l'échec de minutage que
`test-protocol.md` documente déjà : la validation du groupe rate, et le débit de 1 primitive par
image ne suffit pas à le détecter puisque l'état 3 est bien atteint. Des attentes plus longues
(`WAIT6` / `WAIT5` / `WAIT20`) ont rendu la séquence fiable, et le vidage de diagnostic n'était
simplement pas écrit — ce qui était le vrai indice : la fonction soupçonnée n'avait pas tourné.

**Un plantage identique à un plantage connu n'a pas forcément la même cause.** Le contrôle qui
tranche est de vérifier que le code soupçonné s'est exécuté.

### Vérification

Build 0 erreur, compile du premier coup. Oracle : trois corps signalés, les mêmes que le tour
précédent (les deux voulus, plus le renommage `halfW` → `scaleX`) ; `DrawStaticModel` et ses deux
émetteurs comptent parmi les corps neufs. Tests de disposition inchangés, 3 rouges volontaires.
Playtest : la rose des vents s'exécute, et le jeu s'arrête sur `DrawPartyPanels`.

### La frontière

`DrawPartyPanels` (~200 l., `0x005185f0..0x00518e60`), qui demande `FUN_0051be90` (74 l.),
`FUN_0051c250` (10), `FUN_00538bc0` (13) et `FUN_005309b0` (147). Puis `DrawWorldCells` (~200 l.)
et ses appelées `FUN_0051e700` (423), `FUN_0051fa40` (441), `FUN_00521000` (365), `FUN_00522140`
(315), `FUN_0051f940` (18).

---

## 2026-08-22 (nuit, fin) — DrawPartyPanels : les panneaux de membre s'exécutent

Le bloc des panneaux (`Blaze.c` 64485..64699, `0x005185f0`..`0x0051904e`) est transcrit et tourne.
La frontière de la carte du monde est passée à `DrawWorldCells`, le dernier gros bloc de la moitié
dessin.

### Ce que le bloc fait

Deux passes. D'abord les **nombres flottants** : soixante-quatre emplacements de `0x28` octets à
`0x005e9b60` — ceux que `TickEffectSlots_005e9b60` fait vivre — dont chacun porte une position
monde, une valeur et un témoin. Chacun est projeté et imprimé en décimal.

Puis, **un panneau par membre** : les deux jauges (valeur courante teintée par son rapport au
maximum, maximum en blanc), le nom, deux barres obliques, les neuf pictogrammes d'état, le
descripteur de fenêtre, puis le membre lui-même dans le monde — son modèle animé, son ombre portée,
son disque au sol et sa lueur.

### Trois fonctions écrites au passage

`FUN_0051be90` — **un nombre en décimal**, un sprite de cinq mots par chiffre, écrit dans
`Engine/Primitives2d.cs`. Il ne passe par aucun moteur de texte : il découpe l'atlas de chiffres à
la main, la largeur d'un chiffre étant la largeur de cellule divisée par dix. Son argument de
chiffres porte deux choses : quatre bits de rang, et un bit `0x80` qui décide si un zéro de tête
avance la position (cadrage à gauche) ou est entièrement sauté (le nombre se resserre à droite).

`UiText.cs` affirmait que cette fonction « n'a jamais eu de corps » ; c'était faux, elle est bien
dans `Blaze.c` à la ligne 66712. Le commentaire datait d'avant, et personne ne l'avait revérifié.

`FUN_0051c250` — **un simple renvoi** vers `FUN_0051c120`. Un appel et un `ret`. Gardé sous son nom
parce que les sites d'appel distinguent les deux.

`VSyncDelayed` — un second export de **données nommé** de `winsys3d.dll`, résolu comme `MainMem`.
Le bloc le teste avant de dessiner les membres dans le monde : non nul, l'image est en retard et
tout le rendu 3d des membres est sauté.

### Ce que la pseudo-C ne montrait pas

`local_50` avance de `0x26` sur un `short*`, soit **0x4c octets** — pas 0x98 comme le donnerait la
lecture naturelle en `int*`. La différence n'est pas cosmétique : à 0x98 les quatre entrées
recouvrent les enregistrements de membre de `0x005e93c0`, que le même bloc lit par ailleurs. Le
désassembleur tranche en une ligne (`add ebp,4Ch` en `0x0051903e`), et confirme au passage que le
bloc va jusqu'à `0x0051904e` et non `0x00518e60` comme l'estimation le disait.

Les quatre `psVar17` sont des **descripteurs de fenêtre** de `0x8c8` octets à `0x005dbc68` — le pas
exact de `GameMemory.UiDescriptorStride`. Le bloc est nul au chargement (l'adresse tombe dans la
queue non initialisée de `.data`), donc tout ce qu'il porte vient du code ; le bloc des panneaux
n'en écrit que le coin, la teinte et la couleur de jauge.

### Deux lacunes laissées, toutes deux derrière une condition

`FUN_00538bc0` (13 l.) — la hauteur de la bulle d'état, prise dans l'une de deux tables selon un bit
de l'enregistrement. Triviale en soi, mais `DAT_0056d440` est une table de pas 8 dans `.data` de
l'exe qu'il faut relever, et le sous-bloc est gardé par `record[0x3c] != 0`.

`FUN_005309b0` (147 l.) — la surimpression de dialogue, gardée par le bit `0x8000000`. Elle demande
trois tables non relevées.

Les deux lèvent sous leur nom, avec ce qui manque écrit dedans.

### Une fragilité indépendante, vue en passant

Une exécution a échoué dans `AllocLowZeroed` : « aucun bloc libre de 0x1000 octets sous
0x01000000 ». La fenêtre basse fait 16 Mo et chaque descripteur y dépense un granule de 64 Ko ; le
bloc qui a échoué était alloué **avant** le mien, donc la cause n'est pas ce tour, mais la marge
est plus mince qu'on ne croit. L'exécution suivante est passée sans rien changer. À surveiller si
d'autres blocs bas apparaissent.

### Ce qu'on ne peut pas encore voir

**Rien ne s'affiche à l'écran, et c'est normal** : `DrawWorldCells` lève avant la fin de l'image,
donc la liste d'affichage n'est jamais soumise et le débit reste à 1 primitive par image. La
vérification visuelle des panneaux n'est possible qu'une fois la moitié dessin complète — ce que
`DrawWorldCells` conditionne à lui seul.

### Vérification

Build 0 erreur. Oracle : trois corps signalés, les mêmes que les deux tours précédents ; les
quatre corps neufs comptent parmi les nouveaux. Tests de disposition inchangés, 3 rouges
volontaires. Playtest : les panneaux s'exécutent, arrêt sur `DrawWorldCells`.


## 2026-08-23 — la moitié dessin de la carte du monde est terminée

Les six lacunes que le TODO de la veille listait comme « le plus gros morceau restant, ~1 500 lignes
de pseudo-C, probablement deux séances » sont transcrites en une : `DrawWorldCells`,
`FUN_0051f940`, `FUN_0051e700`, `FUN_0051fa40`, `FUN_00521000`, `FUN_00522140`.
`DrawWorldMapFrame` va maintenant de bout en bout sans lever, et l'exception a **quitté la moitié
dessin** — elle est remontée dans le pas de simulation, une ou deux images plus loin.

### L'estimation était fausse, et pour une raison instructive

Les trois listes de morceaux comptaient 441, 365 et 315 lignes de pseudo-C. Ce sont **la même passe
à trois réglages près** :

| | source des faces | profondeur | emplacement d'OT |
|---|---|---|---|
| `FUN_0051fa40` | grille de cellules | ordinal **418** | **par face**, `(z & 0xfffc)` |
| `FUN_00521000` | grille de cellules | ordinal 795 | fixe, celui de l'appelant |
| `FUN_00522140` | faces à la file | ordinal 795 | fixe, celui de l'appelant |

Elles sont un seul `DrawChunk` dans `Engine/Model3d.cs`, avec deux émetteurs partagés
(`EmitChunkTriangles`, `EmitChunkQuads`) et trois points d'entrée nommés. De même, `FUN_0051e700`
(423 l.) est la **synthèse des deux émetteurs de modèle déjà écrits** : le prologue et les quatre
seaux de `FUN_0051c420`, le tri par face de `FUN_0051d1f0`. Elle s'est écrite en une passe parce que
les deux sœurs étaient là — troisième confirmation d'affilée du conseil « relire ce qui tourne coûte
moins cher que relever au désassembleur ».

**Ce qui prédit le coût n'est ni le nombre de lignes ni le nombre d'octets, mais le nombre de
*formes distinctes*.** Ici : deux (triangle, quad), déclinées plate/dégradée, déjà écrites deux fois
dans le dépôt.

### Le morceau de décor n'est pas un modèle

Un « morceau » porte une grille 32×32 de cellules à `+0x34` **en plus** de son tableau de faces à
`+0x28`, et les deux premières listes refont **à l'intérieur** le même balayage 7×7 par octant que
`FUN_00516fc0`. Chaque cellule pointe sur un enregistrement dont les deux premiers mots portent
chacun quatre compteurs : ceux du premier sont sautés (ils appartiennent aux autres passes) et
décalent seulement le curseur d'indices, ceux du second donnent les quatre seaux de la passe.

Une face fait 0x20 octets. Les deux moitiés d'un mot de sommets **ne se lisent pas de la même
façon** : la basse est un indice (`(w & 0xffff) * 4`), la haute un décalage d'octets déjà calculé
(`w >> 0xe`), sans remasquage des deux bits perdus.

### Les deux tables du balayage, relevées

`&DAT_00564548` (huit demi-mots : le coin de départ, X dans l'octet bas, Z dans le haut) et les huit
tranches que `PTR_DAT_005645d8` désigne (huit clés de seize bits `rang << 8 | colonne` chacune, les
coins hors du cône de vue). Elles sont **contiguës de `0x00564548` à `0x005645d8`**, la table de
pointeurs comprise — même situation que les descripteurs d'atlas : on relève la plage, pas l'entrée.
Détail dans `DataSymbols.md`. Elles vivent dans `GameMemory` et non chez un lecteur, parce qu'il y
en a trois répartis sur deux fichiers.

### Deux défauts d'origine, tous deux prouvés par comparaison interne

* **LB-009** — `FUN_00516fc0` lit les clés de saut suivantes **sur un octet** (`movzx ax, byte
  [edi]` en `0x00519143`) alors que le curseur avance de deux et que la table porte des clés de
  seize bits. La clé devient ≤ 0xff et ne peut plus égaler `rang << 8 | colonne` ≥ 0x0101 : **une
  seule cellule est sautée par image au lieu de huit**. `FUN_0051fa40`, qui refait le même balayage
  sur la même table, lit correctement seize bits — c'est ce qui transforme l'hypothèse en preuve.
* **LB-010** — le seau des triangles plats de `FUN_00521000`, et lui seul, remplace son indice de
  couleur par le prédicat « le champ est non nul » (`test ebx,ebx / setne dl`). Les trois autres
  seaux de la même fonction, et les seaux homologues des deux autres listes, indexent normalement.

### Un gestionnaire d'entité de plus, absent de `Blaze.c`

`FUN_00486ee0` (881 o., `Legacy/Raw/Blaze/FUN_00486EE0.asm`) — le sous-type **0** de la table
`0x005ecaa0`, la bestiole errante. Deux moitiés : la **détection** (distance par l'ordinal 442,
angle par le 457, un bit par membre dans `entité[9]`, et le script `+0x16` qui démarre quand le
masque passe de vide à non vide) et la **promenade** (cap et vitesse tirés au hasard, **essayés sur
une copie** de l'enregistrement à `0x005e9a40`, adoptés seulement si `FinalizeEntityPlacement2` ne
signale pas les bits `0x882`). Plus `FUN_004db5a0` (112 o.), l'avance d'image d'animation qu'elle
partage : le bit `0x1000000` choisit entre boucler et s'arrêter en posant `0x2000000`.

### Quatre thunks d'ordinal identifiés au passage

`0x00545A10` → 442 (`SquareRoot0`), `0x00545A16` → 450 (cosinus), `0x00545A1C` → 451 (sinus),
`0x00545A22` → 457 (`ratan2`). Le partage sinus/cosinus a été tranché en retrouvant le même idiome
dans `Blaze.c` (`param_1[7] = Ordinal_451(...)`, `param_1[9] = Ordinal_450(...)`, soit X par le
sinus et Z par le cosinus). L'ordinal **367** (`SetLightMatrix`, huit dwords) est déclaré pour la
première fois — c'est lui qui explique pourquoi `FUN_0051c420` recopie 0x20 octets à la main.

### État à la fin de la séance

Build 0 erreur. Playtest : la moitié dessin passe en entier ; l'arrêt survient ensuite dans le pas
de simulation, avec **trois sorties possibles selon le minutage** — `FUN_0053aa10` non transcrite
(la plus fréquente), `DAT_005eca94` nul dans `PublishCameraFrame_004022B0` (intermittent, et
**antérieur au travail du jour** : constaté à la première exécution de la séance, avant toute
modification), ou quelques images de plus avant l'une des deux.

**La première image de la carte du monde n'a toujours pas été vue.** La liste d'affichage se
construit maintenant, mais le jeu meurt une ou deux images après le premier `Flip` et la capture
n'attrape qu'une fenêtre morte. Un échafaudage temporaire dans `WorldMapCamera.cs` imprime le
voisinage de `0x005eca94` au moment où il lève : le lancer une fois répondra plus vite que
raisonner.


## 2026-08-23 (suite) — `FUN_0053aa10`, et ce qu'elle révèle

`FUN_0053aa10` est écrite, ainsi que les trois choses qu'elle réclamait : `FUN_0047ac40` (la
troisième jumelle de `SpawnEffectEntity`, sur la table `DAT_005f2d40`), la table `&DAT_0056d340`
(quarante-huit masques d'état, un par minuteur, relevée dans le binaire) et `FUN_00404ef0` (le
gestionnaire `0x404EF0` de la table des effets). Le pas de simulation va deux fonctions plus loin.

**Le nom que ce port lui avait donné d'avance était faux.** `AdvanceSceneEntityAnimation_0053aa10`
n'anime rien : elle fait vivre le **bloc de statistiques** que `GetEntityStatBlock` désigne. Trois
cadences — à chaque image la teinte `+0x0b` de l'entité ; toutes les trente images les deux poisons
et le drain de pétrification, chacun à trois pour cent du maximum et **au moins un point** ; toutes
les vingt images les quarante-huit minuteurs d'état, dont trois portent une régénération au lieu
d'un simple décompte — puis les deux seuils de zéro. Le nom est conservé pour ne pas casser les
renvois, mais la documentation sur place dit ce qu'elle fait vraiment.

### Une supposition démentie avant même d'être testée

Le TODO du matin annonçait un problème de carte mémoire : `&DAT_005ea560` s'indexe au pas `0x120`
alors que `GameArena` s'arrête à `0x005ea600`, donc « l'arène ne tient qu'une entrée ». Faux. Le bloc
a **déjà** sa propre allocation — `s_EntityStatBlocks_005ea560`, 0x20 × 0x120, dans
`Screens/State04/CharacterCreation.cs` — et son accesseur `GetEntityStatBlock`, écrits pour
`FUN_00538780` avec les deux bornes concordantes qui donnent 32 entrées. Chercher le symbole avant
de conclure à un trou dans l'arène : même leçon que « relire ce qui tourne avant de relever au
désassembleur », appliquée aux données.

### Ce que la transcription a mis au jour : personne ne remplit ces blocs

`s_EntityStatBlocks_005ea560` est alloué à zéro et **aucun site du dépôt n'y écrit**. Les entités de
scène — celles de `0x005ecaa0` qui ne portent pas le bit `0x800000` — naissent donc à 0 point de vie
et 0 point de magie. `FUN_0053aa10` est le premier lecteur qui *agit* sur ce contenu, et le fait
fidèlement : à la première image, chaque entité de scène est marquée morte (`0x40000000` dans le
bloc **et** dans le mot d'état), puis fait naître une entité de sous-type `0x29` dans
`DAT_005f2d40` et une copie de sous-type `0x46` dans la table des effets.

C'est cette copie qui bloque maintenant, sur l'entrée 0x46 de la table statique `0x00551c30` —
absente de `Blaze.c`. **Il ne faut pas transcrire cette cascade** : elle est le symptôme. Les trois
fonctions de remplissage sont identifiées (`FUN_00538af0`, `FUN_00538f50`, `FUN_00538fa0`, toutes
dans `Blaze.c`, appelées ensemble par `FUN_00538830` et `FUN_00519c30`) ; ce qui reste à trouver est
**qui les appelle au chargement de zone**.

C'est le genre de trouvaille qu'une lacune franche produit et qu'un no-op silencieux aurait cachée :
sans le `throw` nommé, les entités de scène seraient simplement mortes à l'écran sans que rien ne le
dise.

### État à la fin de la séance

Build 0 erreur ; tests de disposition inchangés (3 rouges volontaires) ; oracle `compare-bodies`
inchangé sur les trois corps déjà signalés. Playtest reproductible deux fois de suite : arrêt sur
l'entrée 0x46 de la table statique des effets. La première image de la carte du monde n'a **toujours
pas** été vue.


## 2026-08-23 (fin) — LA CARTE DU MONDE TOURNE. Le remplisseur existait ; c'était un aliasing

**`OpenForesia.Tools\playtest.ps1 -Entry 0 … -Shot` rend « aucun plantage », 2101 images, 257 à 452
primitives par image, et la capture montre l'auberge** : les murs de bois, l'escalier, le sol de
pierre, la plante, la porte, le personnage avec son épée, la rose des vents et le panneau de membre
« MISEUR 16/16 2/2 ». Deux exécutions de suite, identiques.

### Ce qu'on cherchait

Le TODO du matin annonçait : « personne ne remplit les blocs de statistiques des entités de scène,
c'est la vraie tâche suivante ». La recherche a donné le contraire de ce qu'elle annonçait.

**Le remplisseur existe, il est transcrit, et il tournait.** C'est `FUN_005389e0`
(`ResetEntityStats`, dans `Screens/State03/WorldMapScript.cs`), et il n'a **qu'un seul appelant dans
tout le binaire** : `FUN_0051b850` (`SpawnEntityFormation`), lui-même atteint depuis un volume de
déclenchement — `TickPartyMember` → `TestTriggerVolumes_00534CD0` → `RunEntityScript` →
l'opcode de formation. Un échafaudage posé sur le site d'appel l'a montré sortant avec **PV 1/1 et
PM 1/1**, donc bien rempli.

### Le vrai défaut : deux noms pour une adresse

`ResolveEntityBlocks` (`FUN_00538980`) résolvait le bloc de statistiques par
**`Mem.Arena(0x005ea560)`** — une vue sur `GameArena` — alors que `GetEntityStatBlock`
(`FUN_00538780`), lecteur de toute la simulation, le résout par **`s_EntityStatBlocks_005ea560`**,
une allocation **distincte**. L'écriture partait dans la queue de `GameArena`, la lecture revenait
de l'autre bloc : zéro.

Conséquence en chaîne, entièrement fidèle une fois la donnée fausse admise : toute entité de scène
naissait à 0 point de vie, `FUN_0053aa10` la marquait morte à la première image, puis faisait naître
l'effet « magie épuisée » de sous-type `0x46` dont le gestionnaire n'est pas transcrit. **Trois
fonctions de symptôme avaient été mises en file d'attente derrière une seule ligne fausse.**

Le défaut était doublement fautif : `GameArena` s'arrête à `0x005ea600`, donc dès l'emplacement 1
l'écriture sortait de l'arène.

### La leçon, et elle est désagréable

**Le test de disposition nommait ce symbole, mot pour mot, depuis le début** :

```
1 symbole(s) s'allouent leur propre memoire a une adresse qu'une arene couvre deja.
L'arene et le symbole designent deux blocs distincts : une ecriture par l'un
n'est jamais lue par l'autre.
  s_EntityStatBlocks_005ea560 -> dans GameArena (0x005ea560, …CharacterCreation.cs:985)
```

C'est l'une des trois assertions rouges « volontaires » de `MemoryLayoutTests`. Elle était classée
latente ; elle ne l'était pas. **Une assertion rouge volontaire cesse d'être volontaire dès qu'un
site vivant l'active** — et rien ne signale ce basculement. Relire les trois messages rouges quand
un symptôme inexplicable apparaît coûte une minute et aurait fait gagner la matinée.

### L'étendue de l'arène, refermée dans la foulée

`GameArenaSize` est passée de `0x1600` à `0x1560` : l'arène s'arrête désormais à `0x005ea560` et ne
recouvre plus la table des blocs de statistiques. La borne vient du seul site qui la fixe, le
balayage de `FUN_00516350` — `undefined1 *puVar9 = &DAT_005e9b87`, 0x40 tours de pas 0x28, **un
octet** par tour, donc dernier écrit en `0x005e9b87 + 0x3f * 0x28 = 0x005ea55f`, soit l'index
`0x155f`. L'ancienne borne de `0x1600` venait d'un compte à un enregistrement près (`0x40 * 0x28`
au lieu de `0x3f * 0x28`) ; la table de 0x40 enregistrements de 0x28 octets qui commence en
`0x005e9b60` s'arrête exactement au même endroit.

Le changement est **neutre à la mesure** — playtest identique, image comprise — parce que les deux
seuls sites qui lisaient la table par l'arène venaient d'être corrigés. Ce qu'il apporte est de
rendre le défaut irreproductible : un `Mem.Arena(0x005ea5..)` écrit par distraction lèvera
maintenant au lieu de repartir en silence dans le mauvais bloc. L'assertion
`Aucune_allocation_propre_dans_l_etendue_d_une_arene` est verte, et les tests de disposition
passent de trois rouges à deux.
### État à la fin de la séance

Build 0 erreur. Tests de disposition : 3 rouges, les mêmes (dont celle-ci, qui reste rouge tant que
l'étendue de `GameArena` n'est pas réduite). Oracle `compare-bodies` : quatre corps signalés, les
trois habituels plus `ResolveEntityBlocks` — c'est la correction elle-même, et l'outil a bien fait
de la voir.


## 2026-08-23 (soir) — trois écarts relevés par Vincent contre le jeu d'origine, deux réglés

Première comparaison à l'original depuis que l'auberge s'affiche. Vincent relève trois différences.
Deux avaient la même cause de fond — **un symbole modélisé deux fois** — et la troisième reste
ouverte.

### La boîte « - La réception - » ne s'ouvrait pas

`DAT_00563c5c` et `DAT_00563c77` **ne sont pas des globales** : ce sont les champs `+0x8ac` (le
curseur de texte, un pointeur) et `+0x8c7` (les drapeaux du moteur de texte) du descripteur
d'interface de la carte du monde, à `0x005633b0`. Ghidra les rend en globales isolées parce qu'il
ignore que le bloc fait `0x8c8` octets — le piège déjà catalogué sous « un `DAT_` cluster dans un
enregistrement connu est un champ, pas une globale », et **le même décalage `0x8c7` que
`DAT_0056e61f`** dans le contexte de menu.

`BindMenuLabels` écrivait donc le descripteur pendant que le bloc d'affichage lisait deux
`static byte` que rien n'atteignait : l'opcode 14 liait bien son texte, et il ne s'affichait jamais.
Les deux largeurs viennent du désassembleur et non de la pseudo-C — `mov [563C77h],bl` est un octet,
`mov [563C5Ch],ebx` un dword (`0x00516fa1`, `0x00516fa7`).

Une fois les deux champs remis dans le descripteur, le moteur de texte a tourné pour la première
fois sur ce bloc et a réclamé **deux codes de contrôle absents de `Blaze.c`**, décodés à la main :

* **`0x1f` → `FUN_00545120`** (29 o.) : deux octets du flux deviennent l'**étendue** du panneau, en
  `+0x08` — `(texte[1] << 0x10) | texte[0]`. C'est ainsi qu'un message dimensionne sa propre boîte.
* **`0x08` → `FUN_00544ab0`** (77 o.) : un sous-code d'un octet sur une table de sauts de quatre
  entrées relevée en `0x00544afc` — `FreezeAllEntities`, `UnfreezeAllEntities`, puis le bit 0 de
  `DAT_00604b78` effacé ou posé. **C'est le code qui immobilise le joueur pendant un dialogue**, et
  il explique la seconde moitié de l'observation : on ne peut pas bouger tant que la boîte est là.

### Les jauges s'affichaient sans cadre

Même famille, deux fois de suite. D'abord `&DAT_005dbc68` portait **deux noms** :
`s_WorldPanels_005dbc68`, que la fin de `FUN_00516350` monte au chargement, et
`s_PartyPanelDescriptors_005dbc68`, une allocation séparée que le bloc d'affichage lisait. Le second
est devenu une vue sur le premier.

Cela n'a pas suffi, et le second défaut est plus instructif : **la boucle d'initialisation elle-même
avait des décalages faux**. Ghidra rend son curseur `puVar15 = &DAT_005dbc72` en `short *` puis
mélange indices de short et décalages d'octets (`puVar15 + 7` à côté de `(int)puVar15 + 0xf`) ; la
transcription en avait tiré des offsets décalés de deux au début et de sept à la fin. Le
désassembleur (`0x00516f25..0x00516f9d`) donne la base sans ambiguïté — `eax = 0x005dbc72`, soit
**descripteur + 0x0a** — et tout se lit relativement à elle :

```
mov [eax-2],bp        -> +0x08 = 0x54        mov [eax-0Ah],si  -> +0x00 = 0x31 (WORD, pas dword)
mov word [eax],1Ch    -> +0x0a = 0x1c        mov [eax+12h],edi -> +0x1c (dword)
```

L'écart décisif est `+0x08` : c'est là que `DrawUiPanel` lit l'**étendue** de la fenêtre, et
l'ancienne version ne l'écrivait jamais. Les quatre panneaux sortaient donc dégénérés, d'où des
jauges en texte nu au lieu du cadre bleu de l'original.

**Troisième fois dans la journée que la pseudo-C ment sur un bloc de descripteur.** La règle déjà
écrite pour le bloc de sprite vaut ici mot pour mot : sur ces blocs, on relève au désassembleur.

### Ce qui reste : la caméra

Vincent la place, sur l'original, **juste au-dessus de la porte** à l'entrée de l'auberge ; la nôtre
est ailleurs. Non diagnostiqué. Ce qui est établi : le bloc d'initialisation existe et s'exécute
(`WorldMap.cs`, la branche qui écrit `Mem.View(0x006067e8..ec)` depuis `s_WorldMapCamera_00564650`
et le point d'entrée). Les pistes, par ordre de vraisemblance :

1. la lecture du point d'entrée — position **et cap** — dans la fiche de sous-zone ;
2. les constantes de `&DAT_00564650`, relevées mais dont l'usage n'a pas été recoupé à l'écran ;
3. le sous-bloc d'**occlusion** du sous-système de caméra (`0x00401913..0x00401b6d`, plus
   `FUN_004029e0` et `FUN_00402d60`), toujours pas transcrit — il est réputé hors du chemin au
   repos, ce qui reste à vérifier maintenant qu'on a une image à comparer.

### État

Build 0 erreur. Playtest : aucun plantage, 2101 images, 388 primitives par image, boîte de dialogue
et cadre de jauges conformes. Tests de disposition : 2 rouges, inchangés. Oracle `compare-bodies` :
six corps signalés — les trois habituels plus `ResolveEntityBlocks`, `PlaceWorldMapParty` et
`BuildTextTable`, c'est-à-dire exactement les trois corrections du jour.

## 2026-08-23 (nuit) — la caméra : un défaut réel corrigé, et qui n'était pas le bon

Troisième écart relevé par Vincent contre le jeu d'origine : à l'entrée de l'auberge la caméra doit
se poser **juste au-dessus de la porte**, la nôtre reste en arrière.

**Un défaut a été trouvé et corrigé** : les trois décalages de caméra `&DAT_00564650 + 8`, `+0xc` et
`+0x10` sont des **entiers**, et la reconstruction les lisait sur seize bits dans **trois** endroits
— `PlaceWorldMapParty` (au chargement de zone), la remise à neuf en tête de `RunWorldMapState`, et
`ApplyPadHeightLimits_004021E0` (à chaque image). Le désassembleur ne laisse pas de place au doute :

```
0040223d  8b1558465600   mov edx,[564658h]      005169b5  8b355c465600  mov esi,[56465Ch]
00402250  8b355c465600   mov esi,[56465Ch]      00516a7d  891560465600  mov [564660h],edx
```

Ghidra rendait ces symboles sans cast, ce qui aurait dû suffire à les typer en `int` ; c'est la
même famille d'erreur que les décalages du bloc de panneaux plus tôt dans la journée.

**Et l'image n'a pas bougé d'un pixel.** Le défaut est réel mais **latent à ce point d'entrée** :
les valeurs y tiennent sur seize bits, donc la troncature n'y changeait rien. C'est un rappel utile
— corriger ce qu'on trouve en cherchant une cause ne veut pas dire qu'on a trouvé *la* cause, et
seule la capture le dit.

### Deux pistes écartées par la mesure, pas par l'intuition

* **Le sous-bloc d'occlusion n'est pas en cause.** Sa garde
  (`ViewParams(0x00571348) == 0 && ViewParams(0x00571380) != 0`) rend la main à chaque image, et la
  preuve est solide : `ProbeCameraOcclusion_004011C0` est encore une lacune franche, elle lèverait
  si on l'atteignait. Le jeu tourne 2101 images sans lever.
* **La lecture du point d'entrée est fidèle** — `views = *(byte**)(areaDir + 0xc0)`,
  `index = (spawn[6] * 2) & 0xffff`, `view[0]` / `view[1]` — recoupée mot pour mot avec la pseudo-C.

### Ce qu'il faut faire ensuite

Comparer les **valeurs**, pas le code. Relever sur le jeu d'origine, à l'image où l'auberge
apparaît, ce que valent `DAT_00604928` (le cap), `DAT_00564650..60` (le bloc de constantes) et
`DAT_005de078/7c/80` (la cible), puis les confronter aux nôtres. C'est le seul moyen de séparer trois
hypothèses qui produisent le même symptôme : une vue mal lue, une cible mal placée, ou un lissage
qui ne converge pas au même endroit.

## 2026-08-23 (nuit) — la caméra, localisée à un seul bloc : `ViewAngleGoal`

Le relevé x32dbg de Vincent (`Analyse/x32dbg/AUBERGE_CAMERA/camera_x32dbd_recap.md`) permet de
réduire sa table de sept lignes à **deux nombres**, puis à **un bloc mémoire**.

### Toutes les divergences découlent du lacet et du tangage

| | Blaze | OpenForesia |
|---|---:|---:|
| `00604928` | cap 597, tangage **1024** | cap 297, tangage **3** |
| `006067e8` | (600, **1024**, 0) | (300, **0**, 0) |
| `005de08c` | 77 | 114 |
| `005de090` | 606 | 334 |

Les deux dernières lignes ne sont pas des divergences indépendantes, ce sont des **conséquences
arithmétiques** des deux premières :

* `005de08c = cos(cap) >> 5`. `cos(597)/4096 = 0.601` → `0.601 × 4096 / 32 = 77` ✓ ;
  `cos(297)/4096 = 0.891` → `114` ✓.
* `005de090` converge vers `1.5 × DAT_005de05c`, lui-même `sin(cap) >> 3` + l'écartement du groupe.

Autrement dit : **la cible et la distance sont déjà justes** (`547` et
`(3686400, 409600, -2867200)` des deux côtés), seul l'angle diffère, et tout le reste suit.

### Le mécanisme, mesuré image par image

Un échafaudage qui imprime la pose à chaque image montre le lacet monter **de +10 par image** depuis
0, et s'arrêter net à **300**. Ce n'est pas un lissage : c'est la borne basse du couple
`DAT_005e9b34`, dont nos deux valeurs sont **(300, 600)** — les bonnes. Nous convergeons vers le
**minimum**, l'original se pose sur le **maximum**.

### D'où vient le 600 de l'original — et pourquoi nous le perdons

`PublishFrameView` (`FUN_00401080`) fait, dans cet ordre :

```
ViewAngle[0] = range[1]          // 600
0x005712f8   = ViewAngle         // sauvegarde
ViewAngle    = ViewAngleGoal     // 0x006067e0
```

Notre sauvegarde `0x005712f8` vaut **(600, 1024, 0)** — exactement les valeurs de régime établi de
Blaze. **Le calcul est donc juste.** Ce qui l'écrase est la troisième ligne : notre
`ViewAngleGoal` (`0x006067e0`) vaut `(0, 0, 0)` à l'image 1, puis `(300, 0, 0)`.

**La divergence est entièrement dans `0x006067e0`.** Ses deux seuls semeurs sont
`SnapshotViewVector` (`FUN_00401000`, gardé par le verrou `DAT_00566c94`) et la branche **mode 0**
d'`ApplyViewMode_004011C0` — et le mode vaut 1 dès le chargement, donc cette branche ne s'exécute
jamais.

### Ce qui a été corrigé au passage, et qui n'était pas la cause

`MeasureCameraToParty_00402710` comptait **`dx * dx` deux fois** dans la distance caméra-groupe :
Ghidra rend la somme `dy² + [esp+0x10] + ebp`, et `[esp+0x10]` avait été pris pour `flat` alors
qu'il ne porte que `dz²` — il est rangé en `0x00402752`, une instruction **avant** que `eax` ne
devienne `dz² + dx²`. Corrigé. L'image n'a pas bougé : à ce point d'entrée la fonction ne pilote pas
la caméra.

Vérifié au passage et **fidèle** : `StartAngleGlide_004025A0` (une première lecture du prologue
avait conclu à tort à un décalage d'arguments — elle ne tenait pas compte des `push edi` / `pop`
intermédiaires), et le lissage de pose en tête du bloc d'affichage.

### La mesure qui reste

Au point d'arrêt `0x004021E0`, sur le jeu d'origine : **`0x006067E0`** (trois shorts, le but
d'orientation) et **`0x00566C94`** (un octet, le verrou de réamorçage). Si Blaze montre
`0x006067E0 = (600, 1024, 0)`, le semeur tourne chez lui et pas chez nous ; s'il montre `(0, 0, 0)`
comme nous, alors c'est `PublishFrameView` qui ne doit pas écraser, et l'ordre des trois lignes est
à revoir au désassembleur.

## 2026-08-23 (nuit, suite) — la caméra localisée au bit 0x20, et une note du dépôt démentie

Second relevé x32dbg de Vincent : `ViewAngleGoal` (`0x006067E0`) = **(600, 1024, 0)** sur l'original,
contre **(300, 0, 0)** chez nous ; verrou `0x00566C94` à 0 des deux côtés.

**Notre calcul est juste, c'est son résultat qui est écrasé.** La sauvegarde `0x005712F8` vaut chez
nous **(600, 1024, 0)** — exactement les valeurs de Blaze. `PublishFrameView` les y range, puis fait
`ViewAngle = ViewAngleGoal`, et c'est ce dernier qui dérive vers 300 chez nous.

**Pourquoi il dérive :** `ApplyViewMode` part dans la **branche de rotation** (bit `0x20` de
`DAT_0057139C`, posé par `SnapshotViewVector`) et n'atteint jamais la branche de recadrage, celle qui
recharge le lacet depuis `0x005712F8`. Le lacet est alors poussé de +10 par image jusqu'à la borne
**basse** du couple `DAT_005E9B34` (relevé : `(300, 600)`, les bonnes valeurs), là où l'original se
pose sur la borne **haute**.

### L'expérience, et ce qu'elle démentit

En neutralisant le bit `0x20`, le flot descend jusqu'à `ProbeCameraOcclusion_004011C0` et lève.
Donc :

1. le bit `0x20` est bien ce qui nous détourne ;
2. ~~le sous-bloc d'occlusion est sur le chemin au repos à l'auberge.~~ **RÉTRACTÉ le
   2026-08-23 au soir** — voir la section « l'occlusion, remise à sa place » en fin de fichier.
   L'expérience effaçait `0x20`, or `0x20` est précisément la garde qui protège l'occlusion :
   l'atteindre en l'effaçant ne prouvait rien. La note d'origine (« sa garde rend la main
   avant ») était juste.

La caméra « juste au-dessus de la porte » ne venait pas de l'occlusion non plus : elle vient de la
restitution de `SavedViewAngle` dans `FUN_00401000`, corrigée le même soir.

**Leçon de méthode, elle aussi corrigée** : l'expérience de l'après-midi concluait qu'une garde
« qui rend la main avant » ne prouve rien tant qu'on n'a pas vérifié la condition. C'est vrai —
mais l'expérience choisie était circulaire, puisqu'elle effaçait précisément cette condition. La
bonne leçon est plus sévère : **une expérience qui force la garde d'un bloc ne dit rien sur
l'atteignabilité de ce bloc.**

### Il reste un dword à lire

`0x0057139C` au point d'arrêt `0x004021E0` sur l'original. S'il n'a pas le bit `0x20`, c'est notre
pose du bit qu'il faut expliquer ; s'il l'a, les deux jeux prennent la même branche et l'écart est
**dans** `ManualCameraControl_00401C90`.

## 2026-08-23 (fin de nuit) — le tangage résolu, le lacet localisé à une graine

Troisième relevé de Vincent : `0x0057139C` = **0x60** sur l'original, **identique au nôtre**. Le bit
`0x20` est donc légitime, les deux jeux prennent la même branche, et l'écart est ailleurs.

### Trois transcriptions vérifiées et déclarées fidèles

* `ManualCameraControl_00401C90` — le rappel ±10 vers les bornes, le pas de dérive du tangage
  (`((sbyte)(ViewAngle[1] >> 8) * 0x100 - ViewAngle[1]) >> 3`, un amortisseur qui colle le tangage
  au multiple de 0x100 le plus proche, ce qui explique qu'il **tienne** à 1024 chez Blaze), la
  lecture de manette à `+0` (et non `+2` comme dans `ReadCameraPad`).
* `PublishFrameView` — l'hypothèse d'un `DAT_005e9b34 + 2` valant +4 octets sur un `short *` était
  **fausse** : `mov cx,[eax+2]` en 0x0040108c, c'est bien +2, donc `range[1]`.
* Le verrou `DAT_00566C94` n'est armé qu'au changement de zone, comme chez nous.

### Ce que l'expérience a prouvé

En semant `ViewAngleGoal` depuis `ViewAngle` à la fin du bloc caméra du chargement de zone :

* le tangage passe à **1024**, exactement la valeur de Blaze ;
* la vue change du tout au tout — le comptoir de la réception au premier plan, deux PNJ, les
  plantes, les lampes, l'étage ;
* le débit passe de **388 à 1037 primitives par image**.

**La divergence est donc entièrement la valeur initiale de `ViewAngleGoal`** (`0x006067E0`), que
`PublishFrameView` consomme (`ViewAngle = ViewAngleGoal`) et qui n'est jamais semée chez nous.

Le lacet reste à 297 contre 597 : notre graine vaut 0, donc le rappel ±10 le pousse vers la borne
**basse** (300) au lieu de la **haute** (600).

### Ce qui reste — une séquence, pas une valeur

`ViewAngleGoal` n'a que trois écrivains : `SnapshotViewVector` (`FUN_00401000`, verrou consommé une
fois), la branche **mode 0** d'`ApplyViewMode` (jamais prise, le mode vaut 1 dès le chargement), et
`PublishCameraFrame` (qui le recopie depuis `ViewAngle`, donc n'amorce rien). Sur le papier, la
séquence du premier tour de carte du monde donne `(0,0,0)` **dans les deux jeux** :

```
PublishFrameView : ViewAngle[0] = 600 ; sauvegarde ; ViewAngle <- ViewAngleGoal (0,0,0)
SnapshotViewVector : ViewAngleGoal <- ViewAngle (0,0,0)
```

Or Blaze mesure `(600, 1024, 0)`. Il manque donc un passage que la lecture statique ne montre pas.
**Mesure suivante** : un point d'arrêt sur `0x00401000` (12 instructions) — est-il seulement atteint
après l'entrée dans l'auberge, et si oui que vaut `0x006067E8` à ce moment ? Plus `0x006067E0` au
**premier** passage en `0x00401080`.

L'expérience reste dans le dépôt derrière `OPENFORESIA_SEED_GOAL=1`, **désactivée par défaut** et
marquée « NE PAS GARDER TELLE QUELLE » : elle écrit ce que l'original écrit sûrement ailleurs, et
tant qu'on ne sait pas où, ce n'est pas une transcription.

## 2026-08-23 (fin) — le lacet : Blaze part du même état que nous, donc le modèle est faux quelque part

Quatrième relevé : `FUN_00401000` (`SnapshotViewVector`) est atteint **pendant l'écran de
chargement**, avec `ViewAngle = (0,0,0)`, et au **premier** passage de `FUN_00401080`
`ViewAngleGoal = (0,0,0)`.

**Conclusion forte : l'original démarre exactement dans notre état.** Le semeur ne tourne pas avec
une orientation déjà bonne, et il n'y a pas de quatrième écrivain caché avant la carte du monde.
La divergence naît donc **pendant** les premières images, à code identique et depuis un état
identique — ce qui ne laisse qu'une explication : le **nombre d'itérations** n'est pas le même.

### L'indice qui reste, et il est net

Le compteur `DAT_005713AC` monte d'une unité à chaque passage dans `ManualCameraControl_00401C90`,
et au-delà de `0x32` il efface le bit `0x40` pour poser `0x80` — définitivement, rien ne le remet à
zéro sans entrée manette.

* chez nous : drapeaux `0x60` à l'image 1, **`0xa0`** à l'image 60 → plus de cinquante passages ;
* chez Blaze : **`0x60`** au moment du relevé, la boîte de dialogue affichée → **moins de cinquante
  passages depuis le début**.

Autrement dit, à ce moment-là l'original a exécuté ce corps **beaucoup moins souvent que nous**. Et
comme c'est lui qui applique le rappel de ±10 vers les bornes, cinquante itérations de plus suffisent
à emmener notre lacet jusqu'à la borne basse (300) là où l'original n'a pas eu le temps de bouger.

Le gel du monde n'explique pas l'écart : `ApplyViewMode_004011C0` est appelée sans garde dans la
moitié dessin, chez nous comme dans l'original (le bit `0x10` de `DAT_005de02c` ne garde que des
morceaux de la moitié simulation). La cause est ailleurs — un chemin qui, chez l'original, rend la
main **avant** la branche `0x20` la plupart des images.

### Mesure suivante, deux lectures de la même adresse

`0x006067E8` (trois shorts) au **premier** passage en `0x004021E0` après l'apparition de l'auberge,
puis de nouveau cinq secondes plus tard.

* si le lacet vaut **600 dès le premier passage et n'a pas bougé** cinq secondes après : l'original
  ne dérive pas du tout, et c'est notre appel répété de `ManualCameraControl` qu'il faut expliquer ;
* s'il **monte progressivement** vers 600 : les deux jeux dérivent, mais depuis des bornes
  différentes, et il faut relire le couple `DAT_005E9B34` au moment exact du premier passage.

### Rappel de l'acquis

Le tangage, lui, est **résolu** : semer `ViewAngleGoal` donne 1024 comme l'original, la vue devient
celle du comptoir de la réception et le débit passe de 388 à 1037 primitives par image. Reste à
trouver **où** l'original le sème — pas à le semer nous-mêmes.

## 2026-08-23 (fin) — les deux captures de Vincent tranchent : c'était le CAP, et j'avais inversé deux noms

Vincent fournit `legacy.png` et `new.png` (`Tests/1. New Game/Arrivé Auberge/`) et note « je pense
que la caméra est à 90° de la version legacy ». Les **roses des vents** le confirment sans
ambiguïté :

| | haut | droite | bas | gauche |
|---|---|---|---|---|
| legacy | O | **N** | E | S |
| nous | **N** | E | S | O |

Un quart de tour. Et 90° dans ce moteur valent `4096 / 4 = 1024` — exactement la valeur qui
manquait.

### Le correctif de nommage, qui explique deux jours de fausses pistes

`DAT_00604928` n'est pas `(cap, tangage, roulis)`. C'est :

```
+0  elevation      +2  CAP      +4  roulis
```

C'est `+2` que la rose des vents emploie (`DrawCompassRose` place ses graduations avec
`pose[1]`) **et** que le balayage 7×7 emploie pour choisir son octant
(`((ushort)pose[1] + 0x100) >> 9 & 7`). Les deux lecteurs les plus visibles du jeu pointent le même
champ, et ce n'est pas celui que ce document appelait « cap » depuis le premier relevé.

Relu avec les bons noms, tout se remet en place :

* **cap** : legacy 1024 (90°), nous 0 → l'écart de 90° que Vincent voit ;
* **élévation** : legacy 597, nous 297 → la caméra basse chez nous, haute chez lui ;
* `DAT_005E9B34` = (300, 600) est la paire de bornes d'**élévation**, pas de cap : 26° et 53°.
  Le legacy se pose sur la borne haute, nous sur la basse.

### Ce que l'expérience de graine règle vraiment

Semer `ViewAngleGoal` porte le **cap** à 1024 : la rose des vents devient identique au legacy, la
scène est la bonne pièce vue du bon côté, et le débit passe de 388 à 1037 primitives par image.
**Les 90° sont donc réglés** — reste l'élévation.

### L'élévation, et pourquoi elle converge du mauvais côté

`PublishFrameView` écrit `ViewAngle[0] = range[1]` (**600**, la borne haute) puis, deux lignes plus
bas, `ViewAngle = ViewAngleGoal` l'écrase. Sur une image où `PublishFrameView` tourne, l'élévation
repart donc du but ; sur une image où il ne tourne pas, le rappel de ±10 de
`ManualCameraControl` l'accumule. Notre élévation grimpe de 0 à 300 en trente images, donc
**`PublishFrameView` ne tourne pas à chaque image chez nous** — sinon le but la ramènerait.

La question suivante est donc : sur quelles images le pas de boucle `DAT_005645F8` vaut-il 2 chez
l'original ? C'est lui qui décide si `FUN_00401080` s'exécute, et donc si l'élévation est rappelée à
600 ou laissée dériver vers 300.

## 2026-08-23 (fin) — LA CAMÉRA EST CORRIGÉE. Ghidra avait supprimé un tiers de FUN_00401000

La trace dynamique de Vincent (`Analyse/x32dbg/AUBERGE_CAMERA/openforesia_camera_dynamic_trace.md`,
point d'arrêt **matériel en écriture** sur `0x006067E8`) donne la réponse que quatre relevés
statiques n'avaient pas pu donner.

### Ce que Ghidra avait effacé

`FUN_00401000` commence en réalité par **restituer `ViewAngle` depuis `SavedViewAngle`** :

```
00401000  mov ax,[5712F8h]      00401014  mov [6067E8h],ax    <- ViewAngle <- SavedViewAngle
00401006  mov cx,[5712FAh]      0040101F  mov [6067EAh],cx
0040100D  mov dx,[5712FCh]      00401026  mov [6067ECh],dx
0040101A  mov eax,[5E9AE0h]     0040102D  mov cx,[eax]        <- puis le but depuis le pointeur
```

Ghidra tient ces trois écritures pour **mortes**, puisque les trois suivantes réécrivent les mêmes
adresses depuis `*DAT_005E9AE0`. Elles ne le sont pas : la trace confirme `EAX = 0x006067E8` à cet
instant, donc **le pointeur vise la destination elle-même** et les lectures `[eax]`, `[eax+2]`,
`[eax+4]` relisent ce que la première passe vient d'écrire. La chaîne réelle est :

```
SavedViewAngle (600, 1024, 0)  ->  ViewAngle  ->  ViewAngleGoal
```

**Une élimination de code mort par le décompilateur est fausse dès qu'un pointeur peut aliaser la
destination.** C'est le même piège que les doubles noms d'adresse rencontrés toute la journée, vu du
côté du flot au lieu du côté des données — et il est bien plus difficile à voir, parce que le code
supprimé ne laisse aucune trace dans la pseudo-C.

### Le résultat

Une fois le bloc restitué, sans rien d'autre :

| | Blaze | OpenForesia avant | OpenForesia après |
|---|---:|---:|---:|
| élévation | 597 | 297 | **597** |
| cap | 1024 | 3 | **1024** |
| primitives/image | — | 388 | **557** |

Et la capture est **identique à `legacy.png`** : le personnage vu de dos au niveau de la porte, la
plante, la boîte « - La réception - », les poutres, les deux arêtes de mur convergentes, la rose des
vents (O en haut, N à droite) et jusqu'aux deux petites flèches grises latérales.

L'expérience `OPENFORESIA_SEED_GOAL` est retirée : elle n'a plus lieu d'être, et elle écrivait au
mauvais endroit.

### Ce que cette enquête a coûté et appris

Cinq allers-retours de mesure. Ce qui a fini par trancher n'est aucun des quatre relevés ponctuels
mais le **point d'arrêt matériel en écriture** : là où lire des valeurs ne dit que l'état, tracer les
écrivains donne la séquence. À retenir pour la prochaine divergence de ce type — et à préférer
d'emblée quand deux implémentations partent du même état et divergent sans raison visible.

À retenir aussi : les captures comparées ont donné en une image (les deux roses des vents, un quart
de tour d'écart = 1024 unités) ce que trois relevés mémoire n'avaient pas isolé, **et** elles ont
révélé une erreur de nommage — `DAT_00604928` est `(élévation, cap, roulis)`.

## 2026-08-23 (clôture) — l'occlusion, remise à sa place

Question de Vincent après la correction de la caméra : « y a-t-il quelque chose à modifier concernant
le sous-bloc d'occlusion ? » **Non, et il faut rétracter ce que ce fichier en disait l'après-midi.**

### La mesure

`ProbeCameraOcclusion_004011C0` lève **inconditionnellement**. Le playtest de clôture passe
**1441 images sans une seule exception**. Le bloc n'est donc pas atteint, preuve directe et non
déduite.

### Pourquoi l'affirmation de l'après-midi était fausse

L'expérience concluait « le sous-bloc d'occlusion est sur le chemin au repos à l'auberge » parce
qu'en neutralisant le bit `0x20` le flot y descendait et levait. Mais `0x20` **est** la garde qui
protège l'occlusion :

```
if ((*CameraFlags & 0x20) != 0) { ManualCameraControl(); RaiseCameraOverParty(); return; }
...
if ((*CameraFlags & 0x78) != 0) { RaiseCameraOverParty(); return; }   // 0x78 contient 0x20
...
ProbeCameraOcclusion_004011C0();
```

Effacer la garde puis constater qu'on franchit la garde ne démontre rien. Et le relevé de Vincent a
montré depuis que **Blaze aussi porte `0x20`** (drapeaux `0x60`) : la garde rend bien la main avant,
dans les deux jeux. La note d'origine était juste, c'est ma rétractation qui était fausse.

### Où en est réellement le bloc

Inchangé, et correctement traité : une lacune franche qui se nomme en levant, derrière une garde
fidèle. Son blocage reste **la donnée** — les deux tables parallèles `0x00604BA0` et `0x00604D30`,
espacées de `0x190` et indexées par `(cellule * 200 + n) * 4`, ont une étendue que l'indexation
apparente contredit ; il faut la trancher au désassembleur avant d'allouer quoi que ce soit.

Il se réveillera quand le joueur prendra la caméra en main (bits `0x40`/`0x80` sans `0x20`) ou dans
les états de caméra que l'auberge au repos n'emprunte pas. **À écrire le jour où il lèvera, pas
avant** — c'est la même règle que pour les quinze gestionnaires d'entité laissés de côté.

## 2026-08-23 (nuit) — le verbe 0 de zone, et la cascade de reveil qui suit

Le dialogue avec un PNJ levait sur l'**opcode 0, verbe 0 = `0x00443100`**, absente de `Blaze.c`.
Relevee au desassembleur (108 octets, dump dans `Legacy/Raw/Blaze/AreaVerbs/FUN_00443100.asm`), elle
est courte et sans surprise : trois balayages identiques qui **effacent le bit de gel
`0x04000000`** sur les enregistrements encore presents de trois tables — `0x005ff400` (0x20),
`0x005f2d40` (0x80) et `0x005edee0` (0x80).

**Ce qui est instructif, c'est ce qu'elle ne touche pas.** Ni le groupe (`0x005e93c0`), ni les
entites de zone (`0x005fa5e0`), ni celles de scene (`0x005ecaa0`), ni les projectiles
(`0x005f7de0`). Un dialogue relache donc les effets, les incantations et les sorts d'etat, mais
laisse le monde fige — ce qui est exactement le comportement attendu tant que la boite reste
affichee, et ce qui confirme au passage la lecture du code de controle `0x08` faite plus tot.

### La cascade de reveil, debloquee dans la foulee

Le verbe passe, et le jeu reclame aussitot `FUN_00404f70` — l'une des trois cibles de
`FUN_00404ef0` laissees en lacunes nommees le matin. Elle et ses trois appelees sont transcrites :

* **`FUN_004051d0`** — arme l'etat d'animation depuis les champs de consigne. Le mot `+0x5c` porte
  trois choses : ses trente bits bas sont le numero d'etat, `0x40000000` dit « consigne deja lue »,
  `0x80000000` « l'etat vient de changer » — ce dernier efface en sortant, donc valable un tour.
* **`FUN_00404fe0`** — le pas d'animation qui va jusqu'au bout de la sequence, puis lance le script
  accroche a l'entite. Le champ `+0x48` est un compteur **a double usage** : au-dessus de 9 c'est un
  delai qui decompte, en dessous un numero de phase (0 a amorcer, 1 en cours, 2 fini et gele) ; le
  test `< 10` fait le passage de l'un a l'autre.
* **`FUN_004050d0`** — la jumelle qui declenche des la **premiere** image au lieu de la derniere, et
  qui ne dereference l'entree d'animation que dans la branche qui fige.

`FUN_00404f70` relit `+0x5c` avant chacun de ses trois tests, et c'est voulu : les deux pas ci-dessus
ecrivent ce meme mot, donc un etat peut en declencher un autre dans le meme tour.

### Ce que le jeu fait maintenant

La boite « - La reception - » se passe, le monde se degele, **un PNJ apparait au comptoir** et
s'anime. 1741 images sans plantage, 562 primitives par image.

Marcher vers lui declenche ensuite une **camera de scenette** : `TickCutsceneCamera` reclame le
verbe **27** de `PTR_LAB_0055eb68`, dont l'entree vaut **`0x00504270`** (relevee dans le binaire).
Absente de `Blaze.c` comme toute cette table — c'est la prochaine marche.

---

## 2026-08-23 (suite) — la caméra de scénette au complet

`TickCutsceneCamera` réclamait le verbe 27 de `PTR_LAB_0055eb68`. Le relevé de la table a montré
qu'il n'y avait pas un verbe à écrire mais **trente-deux**, tous absents de `Blaze.c` : Ghidra a
nommé la table `PTR_LAB_`, donc il n'a reconnu aucune de ses cibles comme une fonction et n'en a
décompilé aucune. Les trente-deux sont maintenant transcrits, ainsi que la boucle qui les enchaîne.

### La boucle, et ce qu'elle dit du rythme d'une scénette

`FUN_005036e0` tient en trois lignes une fois l'échappement du joueur écarté :

```c
while (continuer != 0)
    curseur = table[*curseur](curseur, &continuer);
DAT_005719b4 = curseur;
```

Un pas de scénette n'est donc **pas « un verbe par image »** mais « tout ce qui se fait sans
attendre, puis on rend la main ». Seuls trois verbes posent `continuer` à zéro — les trois verbes de
pas, 3, 4 et 5 —, plus le verbe 0 qui termine. Tous les autres enchaînent dans la même image : une
suite de dix consignes suivie d'un glissement s'exécute d'un bloc, puis la caméra glisse sur autant
d'images qu'il faut.

### Les six blocs, et le contresens qu'ils corrigent

`BeginScriptedCamera` sauvegardait, croyait-on, les trois blocs de caméra avant de les remplacer par
des copies de travail. Les verbes montrent que la moitié de cette « sauvegarde » n'en est pas une :

| travail (ce que la caméra montre) | consigne (ce que les verbes visent) | contenu |
|---|---|---|
| `0x005719e8` | `0x005719f0` | trois angles, `short` |
| `0x00571a18` | `0x005719f8` | position, `int` en virgule fixe 20.12 |
| `0x00571a08` | `0x00571a28` | cible, `int` sans décalage |

La colonne de droite est une **consigne**, initialisée à la valeur courante pour qu'une scénette qui
n'écrit rien ne bouge rien. Dix-sept verbes ne font qu'écrire dedans, sous toutes les formes :
depuis une entité choisie (15, 16) ou l'entité porteuse (13, 14), en littéral absolu (17, 18, 19) ou
relatif (20, 21, 22), en rappelant la caméra libre (23, 24, 25), ou par tirage au hasard (29, 30,
31 — la secousse de caméra).

Deux verbes vont plus loin que la consigne : 11 et 12 branchent le pointeur que le moteur suit
**directement** sur le triplet d'une entité, retenu au passage par les verbes 13 à 16 dans
`DAT_005719b0` et `DAT_00571a14`. La caméra cesse alors de viser une valeur figée et suit l'entité
image par image, sans plus repasser par les verbes. Ces deux adresses n'ont aucun lecteur dans
`Blaze.c` : leurs seuls lecteurs étaient ces verbes-là.

### Les trois régimes de mouvement

- **verbe 3** — la consigne est recopiée telle quelle, et l'on tient N images. C'est le seul des
  trois à écrire aussi les registres que le moteur lit sans passer par les pointeurs : le triplet
  d'angles `DAT_00604928` et le vecteur de translation `DAT_006008d0`, dont deux composantes sur
  trois partent niées.
- **verbe 4** — chaque composante avance d'un quart de son écart à chaque image. Le verbe rend la
  main tant qu'un seul des huit pas atteint 4 en valeur absolue.
- **verbe 5** — l'écart est divisé une fois par la durée, et le pas ainsi obtenu est ajouté à chaque
  image. Huit emplacements de pas lui sont réservés (`0x005719a8`, `0x005719b8`, `0x005719c8`).

Les deux composantes d'angle des verbes 4 et 5 prennent le **chemin le plus court** : l'écart est
ramené sur douze bits — un tour complet vaut `0x1000` — et au-delà d'un demi-tour c'est le
complément qui est pris, en négatif.

### Deux acquis de méthode

**Un écart entre deux entrées d'une table de pointeurs ne mesure pas la fonction.** Le verbe 31
était annoncé à 2240 octets par l'écart jusqu'à l'entrée suivante ; il en fait 158, et le code qui
suit — une initialisation qui alloue huit tampons et ouvre un fichier — ne lui appartient pas. La
règle rejoint celle déjà écrite pour `FUN_004747F0` : ce qui prédit le coût est le nombre de formes
distinctes, pas la taille.

**La dernière entrée d'une table n'en fait pas forcément partie.** Le mot `0x00504c80`, trente-
troisième de la table, ne suit pas la convention des verbes : son corps ne lit ni curseur ni
drapeau, il enchaîne un appel et un saut vers un tout autre sous-programme. La table compte
trente-deux verbes ; le mot nul qui suit `0x00504c80` est sa vraie fin.

### Un blocage latent du jeu de 1998

Le verbe 6 — « attendre N images » — pose son compteur **sans** rendre la main, comptant sur le tour
suivant de la boucle pour le décrémenter. Une durée de zéro le fait donc repasser indéfiniment sur
lui-même à l'intérieur d'une seule image. Aucun script connu n'écrit ce zéro ; le comportement est
transcrit tel quel plutôt que gardé.

### Le point de blocage suivant

La scénette est franchie. Le blocage suivant est ailleurs : l'**opcode 22 de l'interpréteur de
scripts d'entité**, `FUN_00515b40`, lui aussi absent de `Blaze.c` et pour la même raison. On y
arrive par `TickEntityTable_005ff400` → `EntityHandler_00404EF0` → `WakeSceneEntityKind0_00404F70`
→ `AdvanceSceneEntityState_00404FE0`, c'est-à-dire quand l'animation d'une entité de scène atteint
sa dernière image et lance le script qui y est accroché — rien qui passe par la caméra de scénette.
Le chemin au repos de l'auberge y arrive tout seul vers la 1681ᵉ image, une fois la boîte de
dialogue écartée et le monde dégelé. Son désassemblage est déjà relevé dans
`Legacy/Raw/Blaze/ScriptOps/FUN_00515b40.asm`.

---

## 2026-08-23 (suite) — l'opcode 22, et le premier dialogue de PNJ

`FUN_00515b40`, absent de `Blaze.c` comme les 38 autres entrées de `PTR_LAB_00563298`, tient en une
trentaine d'instructions :

```c
if ((*(uint *)entite & 0xc00000) == 0) { *resultat = 1; return script + 4; }
octet *stats = FUN_00538780(entite);                     // reecrit en ligne
*resultat = FUN_00514040(stats + script[1], script[2], script[3]);
return script + 4;
```

C'est l'opération arithmétique de l'opcode 3, mais portée sur le **bloc de statistiques** de
l'entité au lieu de son enregistrement de 0x9c octets. C'est par là qu'un script lit ou change des
points de vie, un niveau, une résistance.

### Trois choses que le corps apprend

**Une entité sans bloc rend vrai.** Si le mot d'état ne porte ni `0x400000` ni `0x800000`, l'entité
n'a pas de statistiques du tout : le résultat de condition reçoit `1` et rien n'est écrit. Ce n'est
pas une garde ajoutée à la transcription mais la première branche de l'original, et le `1` est
exactement ce que rendrait une comparaison satisfaite — un script qui teste une statistique sur une
entité qui n'en a pas voit donc son test **passer**.

**La résolution du bloc est `FUN_00538780` réécrite en ligne par le compilateur**, à l'identique :
le bit `0x800000` distingue le membre du groupe, dont le bloc vit dans son enregistrement de 0x2000
octets (`MainMem + 0xf0100 + indice * 0x2000`), de toute autre entité, logée dans
`s_EntityStatBlocks_005ea560` au pas de 0x120. La transcription **réutilise `GetEntityStatBlock`**
plutôt que de recopier le calcul : faire résoudre la même adresse par deux chemins est précisément
le défaut d'aliasing que `ResolveEntityBlocks` a déjà coûté une journée le matin même.

**Un résidu de registre dans le deuxième argument.** L'original charge l'indice d'entité dans `ECX`,
s'en sert pour calculer l'adresse du bloc, puis n'écrit que l'octet bas `CL` avec `script[2]` avant
de pousser `ECX` entier. Sur la branche du membre de groupe, où `ECX` vaut `indice << 13`, les bits
hauts poussés ne sont pas nuls. C'est sans effet — `FUN_00514040` déclare son deuxième paramètre sur
un octet (`undefined1` chez Ghidra) et ne lit jamais au-delà —, et cela explique le `xor` que le
compilateur a omis là et fait pour le troisième argument. Le noter compte : sans la signature de
`FUN_00514040` sous les yeux, on aurait pu prendre ce résidu pour un défaut de 1998 à reproduire.

### Le résultat à l'écran

Le jeu affiche le **premier dialogue de personnage non joueur** de la reconstruction : à l'auberge,
« L'herboriste : Bonjour ! », dans une boîte au bon endroit, avec la caméra de scénette placée
derrière le joueur et le PNJ visible. 2221 images sans plantage, 956 primitives par image — presque
le double du régime au repos, la scénette et le PNJ tirant le reste.

**Aucune image de référence du jeu original n'existe pour cet écran** : la seule comparaison
disponible (`Tests/1. New Game/Arrivé Auberge/legacy.png`) porte sur l'arrivée, pas sur ce dialogue.
La conformité du cadrage et du texte n'est donc **pas vérifiée**, seulement la stabilité. Une capture
du legacy à ce moment-là serait le prochain contrôle utile.

### Le point de blocage suivant

Avancer dans le dialogue (une validation de plus) atteint le **code de contrôle de texte `0x0a`**,
`FUN_00544bc0`, dans la table de rendu `PTR_FUN_0056e728`. Même classe de lacune : Ghidra n'a pas
émis le corps parce qu'il n'y accède que par la table.

Le message que levait ce trou conseillait encore « une re-passe Ghidra ciblée » — l'avis d'avant le
2026-08-21, c'est-à-dire *bloqué pour toujours*, alors que le verbe `disasm` résout ce cas depuis.
Le message renvoie maintenant à `disasm` et rappelle la signature `(texte, descripteur) → nouveau
curseur`. Les autres messages de lacune du dépôt méritent le même coup d'œil.

---

## 2026-08-23 (suite) — le code de contrôle de texte `0x0a`

`FUN_00544bc0`, 74 octets, absent de `Blaze.c` parce que Ghidra n'y accède que par
`PTR_FUN_0056e728`. **C'est le code par lequel un dialogue lance un script d'entité** :

```c
octet *avant = descripteur[0x8ac];
FUN_00513dd0(DAT_005e97cc, descripteur[0x8b0], texte[0] | texte[1] << 8);
return descripteur[0x8ac] == avant ? texte + 2 : descripteur[0x8ac];
```

Les deux valeurs viennent du descripteur, et les deux étaient déjà nommées : `+0x8b0` est l'entité
qui porte le message — le champ que `Handler_00544e80` interroge pour savoir si le message
appartient à un membre du groupe — et `+0x8ac` est le curseur de texte lui-même.

**Le script peut détourner le flux de texte.** Le corps relit `+0x8ac` après l'appel et le compare à
sa valeur d'avant ; si le script l'a déplacé, c'est cette nouvelle adresse qui devient le curseur et
les deux octets d'opérande ne sont pas consommés. C'est le troisième endroit du binaire où l'on
rencontre cet idiome « avant / après » — `TickCutsceneCamera` et le verbe 2 de la caméra de scénette
le font sur `DAT_005719b4`. Chez ce compilateur, c'est la seule façon pour l'appelé de rendre la
main ailleurs, et le reconnaître fait gagner du temps : sans lui on prendrait la relecture pour une
redondance.

**Un recoupement gratuit sur la longueur de l'opérande.** La table de balayage `PTR_LAB_0056e870`
porte `0x00543960` au même indice, c'est-à-dire le talon `Advance2`. La passe 1 saute donc exactement
deux octets là où la passe 2 en consomme deux. Les deux tables se confirment l'une l'autre, et cela
vaut d'être systématisé : **pour tout code de contrôle, la table de balayage donne la taille de
l'opérande sans qu'on ait à lire le corps de rendu.**

Une conséquence de plomberie : `s_Unknown_005e97cc` (le répertoire de zone) passe de `private` à
`internal` sur la classe `Blaze`, parce que `UiText` en a maintenant besoin. `RunEntityScript`
l'était déjà.

### Où en est le chemin de dialogue

L'auberge tourne **2341 images sans plantage** (2281 sur une seconde exécution), à 1017 primitives
par image contre 562 au repos. La boîte « L'herboriste : Bonjour ! » s'affiche, et la scène s'est
peuplée : deux personnages non joueurs visibles, le comptoir, le mobilier.

**Aucun point de blocage nommé ne reste sur ce chemin** — c'est la première fois depuis le début du
dialogue. Mais l'absence d'exception n'est pas une preuve de conformité : il n'existe toujours pas
d'image de référence du jeu original pour cet écran, et l'on ne sait donc pas si la conversation
avance comme elle doit, ni si le cadrage est le bon. Capturer le legacy sur ce dialogue est le
prochain contrôle utile, et il ne coûte que quelques minutes de jeu.

---

## 2026-08-23 (suite) — les réveils d'entité de type 1 et 2, et une signature de Ghidra à jeter

`FUN_00405240` et `FUN_00405510` étaient les deux dernières cibles de `FUN_00404ef0`. Même forme que
le type 0 déjà transcrit — armer l'état, puis cinq branches sur les trente bits bas de `+0x5c`, chacune
relisant le mot parce que la précédente a pu le changer. Elles amènent avec elles deux pas
d'animation qui n'étaient pas encore écrits : `FUN_004052f0` et `FUN_00405420`.

### La signature que Ghidra donne pour `FUN_004052f0` est fausse

Ghidra la déclare :

```c
uint __fastcall FUN_004052f0(undefined4 param_1, undefined4 param_2, int param_3,
                             undefined2 param_4, short param_5, undefined2 param_6)
```

Six paramètres en `__fastcall`, alors que les **deux** sites d'appel en poussent quatre. Le
désassemblage tranche sans ambiguïté :

```
00405561  push edx          ; rand % 10
00405562  push 5
00405564  push 3
00405566  push esi          ; l'enregistrement
00405567  call 004052F0h
0040556c  add esp,10h       ; <- cdecl, quatre arguments
```

et le prologue de l'appelée lit l'enregistrement en `[esp+0x0c]` puis le premier numéro d'animation
en `[esp+0x14]`, ce qui, compte tenu des trois `push` de registres, sont bien les arguments 1 et 2.
Les `param_1` et `param_2` de Ghidra sont des **registres fantômes** ; ses `param_3` à `param_6` sont
les quatre vrais arguments, dans l'ordre.

C'est exactement la classe que `CLAUDE.md` signale comme « encore véritablement bloquée » — les
fonctions dont Ghidra a raté les paramètres. Elle ne l'était pas ici, et la raison mérite d'être
retenue : **quand les sites d'appel s'accordent sur un nombre d'arguments et que le `add esp,N` qui
suit le confirme, la convention se lit en trois lignes de désassemblage.** Ce qui reste réellement
bloquant, c'est le cas inverse — des sites d'appel discordants, ou une convention à registres qu'il
faut reconstruire depuis le trafic. Le décalage systématique des `param_N` de Ghidra, lui, se corrige
en comptant.

### Ce que font les deux pas d'animation

**`FUN_004052f0` parcourt une plage d'animations en marquant une pause sur chacune** : c'est
l'inactivité d'un personnage non joueur. Il joue l'animation `first`, se fige quelques images sur sa
dernière image, passe à la suivante, et recommence à `first` une fois `last` dépassée. Le champ
`+0x9a`, qui n'apparaît nulle part ailleurs, est le **reste de pause**. La durée de pause est tirée
au hasard **par l'appelant** (`CrtRand() % 10`), une fois par passage, si bien que deux entités de la
même scène ne respirent pas en cadence.

**`FUN_00405420` décompte au lieu d'avancer.** Le champ `+0x1a` part du nombre d'images de la
séquence et descend, là où les trois autres pas d'animation du fichier le font monter — le champ ne
désigne donc plus l'image courante mais ce qui reste à tenir. C'est la seule différence de fond entre
les deux réveils : à l'état 3, le type 1 décompte, le type 2 avance normalement.

### Ce que cela vérifie, et ce que cela ne vérifie pas

Le jeu tourne, et le chemin de l'auberge ne lève plus rien. Mais **mon parcours scripté ne réveille
pas d'entité de type 1 ou 2** : Vincent avait atteint `FUN_00405510` dans sa propre partie, plus loin
que ce que le script rejoue. Les deux branches sont donc écrites et compilées, pas exercées. C'est sa
prochaine session qui les mettra à l'épreuve.

---

## 2026-08-23 (suite) — le verbe 14 de la table de zone, celui du coffre

`FUN_00442d90`, vingt-sept octets, absente de `Blaze.c` comme toute la table de verbes :

```c
*(octet **)(0x005ecaa0 + index * 0x9c + 0x6c) = entite;
return 0;
```

Le champ `+0x6c` d'une entité de scène est celui que `RunEntityScript` reçoit en deuxième argument
partout ailleurs — `AdvanceSceneEntityState_00404FE0`, `CycleSceneEntityAnimations_004052F0`,
`CountDownSceneEntityState_00405420` le lisent toutes. Le verbe répond donc à la question « **pour le
compte de qui ce décor joue-t-il son script ?** », et c'est exactement ce qu'il faut à un coffre : le
script du coffre doit savoir qui l'ouvre pour lui remettre son contenu.

L'original écrit `[ecx*4 + 0x005ecb0c]` avec `ecx = 39 * index` ; c'est la même table d'entités de
scène que partout, prise à son champ `+0x6c`. La transcription l'adresse par son début et son
décalage plutôt que par l'adresse composée, pour rester comparable aux autres accès à cette table.

**L'indice est signé** (`movsx eax, word [esp+8]`) et aucune borne n'est vérifiée : un indice négatif
ou au-delà de 0x20 écrit hors de la table. Transcrit tel quel.

La convention d'appel des verbes de zone est reconfirmée au désassembleur du gestionnaire d'opcode 0
(`0x00515490`) : `push ecx` (le `ushort` formé de `script[2]` et `script[3]`), `push edx`
(l'entité), `call dword [eax*4+56AEC8h]`, `add esp,8`. Cdecl, deux arguments, dans cet ordre.

### Cinq voisins relevés au passage

La fenêtre de 464 octets prise pour ce verbe en a montré cinq autres, contigus, et **tous courts** —
dump complet dans `Legacy/Raw/Blaze/AreaVerbs/FUN_00442D90_et_voisins.asm` :

| adresse | ce qu'il fait, en une ligne |
|---|---|
| `0x00442DB0` | réveille les quatre membres du groupe : pour chacun dont le mot d'état porte `0x80000000`, remet `+0x6c` à zéro et enchaîne `0x0053A920` / `0x00530F20` / `0x005312D0` |
| `0x00442E10` | rend le bit 0 de `0x00563c77` |
| `0x00442E20` | balaie les dix emplacements de 0x280 octets d'un personnage à la recherche du mot `0x2EFB` en `+0x86` |
| `0x00442E70` | pose la variable `0x005e9060` à 1 ou 2 selon que le mot `+0x144` du bloc de personnage est sous `0xB4` |
| `0x00442EA0` | échelonne la même variable de 0 à 5 par seuils de 20 sur ce même mot `+0x144` |
| `0x00442F20` | force à 5 les sept octets de `+0x178` qui étaient en dessous, et rend 1 si aucun ne l'était |

Aucun ne dépasse la soixantaine d'octets. **Les prendre en une passe le jour où l'un d'eux sera
demandé coûtera moins cher que de les reprendre un par un** — chaque verbe isolé coûte un
désassemblage, une relecture du contexte et un playtest, et la fenêtre est déjà relevée.

### Ce que cela vérifie

Le chemin au repos ne régresse pas : 1921 images, aucune exception. **Le coffre lui-même n'est pas
rejoué par le parcours scripté** — c'est la partie de Vincent qui le vérifiera, comme pour les
réveils d'entité de type 1 et 2 écrits juste avant.

---

## 2026-08-23 (suite) — la remise d'un objet au groupe

`FUN_00530f20`, 130 lignes dans `Blaze.c`, plus deux compagnons et une signature de rappel qui
manquait depuis longtemps.

### Ce que fait la fonction

Quatre temps. Le monde se fige ; chaque membre du groupe capable de se servir de l'objet prend une
**animation de réaction** choisie par sa classe, après que son état d'animation courant a été mis de
côté en `+0x40` / `+0x42` ; l'inventaire du porteur — dix emplacements de `0x280` octets à
`MainMem + 0xf0000 + indice * 0x2000 + 0x300` — est parcouru ; et une phrase est composée puis
confiée au moteur de texte.

Un emplacement dont les seize premiers octets (le nom court) égalent ceux de l'objet est un
**empilement** : si l'objet porte l'un des bits `0x30` en `+0x13`, la remise se fait là sans chercher
plus loin.

**Le retour dit si l'objet est passé** : zéro s'il l'est, `quantité + 1` sinon — inventaire plein, ou
destinataire qui n'est pas un membre du groupe. Les deux appelants ne lisent que `AX` (`neg ax`) ou
`AL` ; la transcription rend un `int` et les sites rétrécissent explicitement, là où le processeur le
fait.

**L'objet n'est pas rangé par cette fonction.** Elle ne fait que vider les deux mots `+0x310` et
`+0x312` de l'emplacement libre. Le rangement est dans le rappel de fin de boîte, passé en troisième
argument — c'est une surprise pour qui lit le nom de la fonction, et cela vaut d'être su avant d'y
chercher une copie de la fiche d'objet.

### La signature du rappel de fin de texte, enfin établie

`RunTextEngine` portait depuis longtemps un `TODO(REVERSE)` sur le pointeur de `+0x8a8` : « aucun
site d'affectation n'est reconstruit, donc sa signature est inconnue ». `FUN_00530f20` en est un, et
`Blaze.c` donne l'appel :

```c
if ((code *)puVar11[0x22a] != (code *)0x0) {
  (*(code *)puVar11[0x22a])(puVar11[0x22c]);
}
```

Un seul argument, la donnée d'utilisateur de `+0x8b0`. Le trou est comblé et remplacé par un
répartiteur par adresse, sur le modèle des autres tables du dépôt : `0x00530ed0` est branché, tout
autre rappel lève en se nommant.

**La leçon est de méthode** : un `TODO(REVERSE)` sur une signature inconnue se résout quand le
premier site d'affectation apparaît, pas avant — et il faut penser à revenir le fermer à ce
moment-là plutôt que d'attendre de buter dessus.

### Deux acquis de données

`DAT_0056aaa0` est une table de **seize dwords**, une par classe de personnage, lue par l'octet
`MainMem + 0xf0150`. Chaque dword est écrit d'un bloc sur `+0x18` de l'emplacement, donc c'est une
paire (numéro d'animation, image de départ). L'étendue est relevée, pas supposée : à `0x0056aae0` le
contenu change de nature et cette adresse est déjà connue comme un autre symbole.

`DAT_005de620` est **hors de `ItemMenuArena`**, qui s'arrête à `0x005de510` : c'est une allocation
propre de `0x80` octets (le symbole suivant est `DAT_005de6a0`), pas une vue d'arène. Le distinguer
évitait de rouvrir le défaut d'aliasing qui a déjà coûté une journée.

### Ce qui reste pour que le coffre marche de bout en bout

La boîte s'affiche maintenant. En la refermant, le rappel du coffre `0x00450700` se déclenche et
lèvera : il est **absent de `Blaze.c`**, mais court (87 octets) et déjà désassemblé dans
`Legacy/Raw/Blaze/Chest/`. Son corps est clair :

```c
octet indice = entite[0x41];
entite[0x6c] = 0;  DAT_005e9060 = 0;
octet *coffre = 0x005fa5e0 + indice * 0x9c;
int r = FUN_005312d0(entite);
coffre[0x10] = r != 0 ? 0xC8 : 0x64;
DAT_005e9860[coffre[0x44]] = r != 0 ? (octet)r : 0;
```

Il tire `FUN_005312d0`, **le rangement proprement dit** (109 lignes). Celle-là est de la classe que
`CLAUDE.md` signale comme difficile : Ghidra y a perdu la convention de `FUN_0053db20` et de
`FUN_00532710`, qu'il déclare sans paramètre, si bien que leurs arguments apparaissent comme des
affectations à des variables locales (`puStack_20`, `uStack_24`) et leurs adresses de retour comme
des valeurs (`iStack_30 = 0x53134e`). **Ce n'est pas bloquant** : le désassemblage est déjà relevé
dans `Legacy/Raw/Blaze/Chest/FUN_005312D0_rangement.asm` et le prologue s'y lit sans peine
(`sub esp,0Ch` puis quatre `push` de registres). Il faut juste le lire, plutôt que suivre `Blaze.c`.

### Vérification

Pas de régression : 1921 images, aucune exception sur le chemin habituel. **Le coffre lui-même n'est
pas rejouable par le parcours scripté** — c'est la partie de Vincent qui le vérifiera, et le prochain
arrêt attendu est le rappel `0x00450700`.

---

## 2026-08-23 (suite) — le coffre bout en bout

Trois fonctions, et la chaîne du coffre est complète : `FUN_005312d0` (le rangement),
`FUN_00450700` (le rappel de fin de boîte) et `FUN_00544c10` (le code de contrôle `0x0b`).

### `FUN_005312d0` : ne pas suivre `Blaze.c`

C'est le cas d'école de la fonction qu'il faut lire au désassembleur *malgré* sa présence dans
`Blaze.c`. Ghidra y a perdu la convention de `FUN_0053db20` et de `FUN_00532710`, qu'il déclare sans
paramètre ; leurs arguments apparaissent alors comme des affectations à des locales
(`puStack_20 = param_1 + 7`), et leurs adresses de retour comme des valeurs
(`iStack_30 = 0x53134e`). Les quatre appels d'ordinaux subissent le même sort. `Blaze.c` n'a servi
que pour la charpente : les branches, les comparaisons, la structure. Tout le reste vient du
désassemblage.

**La bonne nouvelle, et elle vaut d'être retenue** : une fois le désassemblage sous les yeux, les
conventions se lisent en une ligne chacune — `add esp,0x10` après l'appel dit quatre arguments,
`add esp,0xC` en dit trois — et **toutes les appelées étaient déjà transcrites** sous d'autres noms :

| Ghidra | ce que c'était déjà chez nous |
|---|---|
| `FUN_00530ed0` | `RestorePartyReactions_00530ED0`, écrite la veille |
| `FUN_0053db20` | `Audio.PlayPositionalSound` — le son est le jingle « objet obtenu » |
| `FUN_00532710` | `FreeEntitySlot` — au même octet près, c'est le même corps |
| ordinaux 667 / 676 / 684 | déjà déclarés dans `Interop/Winsys3d.cs` |

La leçon : **avant d'estimer le coût d'une fonction, vérifier ce que ses appelées sont devenues.**
Celle-ci a été portée au TODO comme « difficile » deux fois, alors que quatre de ses cinq
dépendances étaient déjà là.

### Ce que la fonction fait

Elle recopie les `0x50` dwords de la fiche d'objet dans l'emplacement d'inventaire, découpe l'icône
de la VRAM en deux passes (un rectangle `0x10 × 1` pour la palette, un `6 × 0x18` pour l'image) et la
repose à la place qui revient au personnage, puis remonte le compteur d'exemplaires — qui plafonne à
99.

**La recopie écrase le compteur, et l'original le sait** : les `0x50` dwords recouvrent `+0x310` et
`+0x312` de l'emplacement, donc le compteur et le bit `0x8000` sont relevés avant et remis après.
Transcrire la boucle sans voir ça aurait remis chaque objet à un exemplaire.

**Un recoupement gratuit.** La famille `0x32c9..0x32cf` ne passe pas par l'inventaire : elle
incrémente `holder[famille - 0x3151]`, soit `holder[0x178 + (famille - 0x32c9)]` — **sept compteurs
contigus à `+0x178`**. Ce sont exactement les sept octets que le verbe de zone `0x00442F20`, relevé
la veille sans qu'on sache à quoi ils servaient, remonte à 5 quand ils sont en dessous. Deux relevés
indépendants qui se confirment.

### `FUN_00450700`, et le rappel enfin bouclé

Quatre-vingt-sept octets, absente de `Blaze.c`. Elle range l'objet puis **écrit le verdict à deux
endroits** : l'état du coffre en `+0x10` (`0xC8` s'il est passé, `0x64` sinon, les deux valeurs que
`TickEntityTable` aiguille) et la table de butin `0x005e9860`, qui retient ce qui reste à donner
pour que le coffre puisse être rouvert. Le coffre est retrouvé par `entite[0x41]`, l'octet que le
script y range avant d'appeler la remise.

Elle est branchée sur le répartiteur de rappels de fin de texte ouvert la veille — qui compte
maintenant deux entrées.

### `FUN_00544c10` : le code de contrôle `0x0b`

Un **saut de sous-chaîne** : le jumeau dépouillé de `Handler_00544e80`, sans la valeur substituée. Il
empile `texte + 3` sur la pile de retour du descripteur et rend la chaîne désignée par ses trois
octets d'opérande. Inscrit dans les **deux** tables, comme les codes `0x0f`, `0x1c` et `0x1f` : la
passe de balayage doit suivre le même saut que la passe de rendu, sinon elle mesure la mauvaise
chaîne.

C'est ce qui manquait pour que la phrase du coffre — composée de libellés qui s'appellent entre eux —
s'affiche entière.

### Vérification

Pas de régression : 1921 images, aucune exception. **Le coffre reste non rejouable par le parcours
scripté** ; c'est la partie de Vincent qui dira si l'objet arrive vraiment dans l'inventaire, et
surtout si son icône se pose au bon endroit — les deux blits VRAM sont la partie la plus facile à
transcrire juste et à voir fausse.

---

## 2026-08-23 (suite) — la table des codes de contrôle de texte, en entier

Le code `0x03` manquait. Le relevé du bloc a montré qu'il en manquait **dix-huit**, tous minuscules,
et qu'ils allaient tomber un par un au fil des parties. Ils sont tous écrits. La table des trente-deux
codes est complète pour la première fois.

### Ce que le relevé d'un bloc apprend qu'un relevé isolé n'apprend pas

Quinze des dix-huit sont de **simples setters de champ du descripteur** : un octet vers un octet
(`0x10`–`0x13`, `0x15`–`0x17`), quatre octets vers un dword (`0x18`–`0x1b`), deux octets vers les
deux moitiés d'un dword (`0x1e`). Le binaire en fait quinze fonctions distinctes de seize à trente
octets qui ne diffèrent que par le champ visé. Elles sont repliées ici en trois corps paramétrés, sur
le précédent de `ScriptOpAluArena` — la régularité est vérifiée instruction par instruction, pas
supposée.

Les trois autres ont chacun un rôle :

- **`0x07`** est un **saut de chaîne sans retour** — il n'empile rien, le flux part et ne revient
  pas. Son index est *calculé* : `texte[1]` désigne une variable de la banque `DAT_005e9060` dont la
  valeur s'ajoute à `texte[2]`. C'est le « selon que… » du scénariste, une chaîne qui choisit parmi
  une suite de libellés consécutifs selon l'état du jeu.
- **`0x0e`** est l'**attente de fin de page** : le code qui arrête le défilement et rend la main au
  joueur. Le quartet bas de son opérande est un index de variable, le quartet haut choisit un bit de
  drapeau supplémentaire.
- **`0x1d`** **insère le nom du locuteur** : le flux saute dans le bloc de 0x2000 octets du membre du
  groupe désigné par `+0x8b0`, à la même adresse que `GetEntityStatBlock` rend. Sans opérande — et
  c'est l'adresse empilée qui le prouve, `texte` tel quel et non `texte + n` comme aux codes `0x0b`
  et `0x1c`.

### Un piège de largeur, attrapé de justesse

Le code `0x05` choisit la banque de composition des glyphes. L'écriture évidente serait
`descripteur[0x14] = texte[0]`. L'original fait `movzx cx, byte [eax]` puis `mov [edx+14h],cx` : il
écrit **deux** octets là où l'opérande n'en fournit qu'un, et le second — `+0x15`, la profondeur de
la pile de sous-chaînes que les codes `0x0b`, `0x1c` et `0x1d` empilent — reçoit le zéro de
l'extension. **Ce code remet donc la pile de retour à plat.** Transcrire le `mov` évident aurait
laissé une sous-chaîne ouverte revenir là où il ne fallait pas, avec un symptôme (du texte qui
reprend au mauvais endroit, plus tard) impossible à rattacher à sa cause.

C'est le rappel que `Strategy.md` fait sur les largeurs : ce n'est pas une subtilité théorique, elle
se paye en heures de recherche.

### Sur le choix d'avoir tout pris d'un coup

Vincent n'avait demandé que le code `0x03`. Les dix-huit ont été faits ensemble parce que la
situation cochait exactement le cas noté la veille dans `TODO.md` à propos des verbes de zone : **une
famille de fonctions courtes, contiguës, relevées par un seul désassemblage, et qui se manifestent
une par une**. Le coût marginal des dix-sept autres était une lecture de plus dans un dump déjà
ouvert ; les prendre séparément aurait coûté dix-sept allers-retours de partie.

Le critère n'est pas « c'est facile », c'est **contiguïté + relevé déjà fait + apparition en série**.
Sans ces trois-là, la règle reste de s'en tenir à ce qui est demandé.

### Vérification

2101 images, aucune exception, et la boîte de dialogue se ferme désormais : la capture montre le
joueur qui circule librement à l'étage de l'auberge, un décor et un personnage non joueur que le jeu
n'avait encore jamais atteints. Toujours pas d'image de référence pour comparer.

---

## 2026-08-23 (suite) — la bulle d'état au-dessus du membre

`FUN_00538bc0` rend **la hauteur de la tête d'une entité**, prise dans l'une de deux tables selon ce
qu'elle est : un membre du groupe la tient de sa classe, toute autre entité de son propre bloc de
statistiques à `+0x5a`.

La seconde branche est un piège déjà connu : `&DAT_005ea5ba + indice * 0x120` est la table des blocs
de statistiques, celle qui a coûté une journée en août. Elle passe par
`s_EntityStatBlocks_005ea560`, l'allocation propre, et non par une vue d'arène — sans quoi toutes les
entités de scène auraient eu une hauteur nulle et leur bulle se serait affichée dans leurs pieds.

`DAT_0056d440` est une table de **seize entrées de huit octets**, une par classe, dont seul le
`short` de `+2` a un rôle établi. L'étendue est relevée : à `0x0056d4c0` commence une table de
pointeurs. Seize entrées — le même compte que `DAT_0056aaa0`, la table des réactions relevée le même
jour, et pour la même raison.

### Le sous-bloc était décompilé depuis le début

La souche affirmait que le corps se réduisait à « un `EmitParticle` à l'emplacement `0x10001000` ».
C'était juste, mais incomplet : le sous-bloc est bel et bien dans `Blaze.c`, à la ligne 64663, à
l'intérieur de `FUN_00516fc0`. Il ne manquait que son unique appelée.

**La leçon est de recherche.** Les cinq sites d'appel de `FUN_00538bc0` que `grep` remonte sont tous
en dehors de `FUN_00516fc0`, ce qui laisse croire que le sous-bloc n'est pas décompilé. Il l'est ; le
`grep` échouait parce que le pointeur d'enregistrement y est exprimé en négatif
(`puVar19 + -0x5a` pour `record + 0x3c`, `puVar19 + -0x96` pour `record`). **Chercher un décalage de
champ dans du pseudo-code Ghidra suppose de savoir depuis quelle base la boucle l'exprime**, et cette
base peut être au milieu de la structure.

Une fois trouvé, le corps est le jumeau de `DrawGroundDisc` : la transformation du membre est
recopiée, une composante est décalée — la hauteur ici au lieu de la profondeur —, et le tout part en
particule. La bulle part sur `otBase` et non `otBase + 5` comme la lueur : elle se dessine donc
devant, ce qui est bien ce qu'on attend d'une icône d'état.

### Ce qui reste dans ce voisinage

`FUN_005309b0` (147 l., la surimpression de dialogue, gardée par le bit `0x8000000`) est toujours une
souche. Elle demande trois tables de `.data` non relevées : `DAT_0056a851`, `DAT_0056aae0` et
`DAT_0056ab30`. Les deux dernières sont adjacentes à `DAT_0056aaa0`, déjà relevée — leur étendue se
lira dans le même dump.

---

## 2026-08-23 (suite) — la surimpression de dialogue

`FUN_005309b0` est écrite. C'est la bulle et son icône au-dessus d'un membre du groupe, gardée par le
bit `0x8000000` de son enregistrement. Toutes ses appelées étaient déjà transcrites — `ProjectPoint`,
`EmitViewportRect`, `EmitClipRect`, `DrawSpriteVariant`, `DrawFlatPrimitive` — et le travail était
donc dans les données et dans trois détails faciles à manquer.

### `&LAB_00404040` n'est pas du code

Ghidra l'étiquette comme un label parce que l'adresse tombe dans `.text`. C'est la **couleur
`0x404040`**, le gris éteint : `local_38` est le champ de teinte du bloc de sprite, et son autre
valeur possible est `DAT_005e9814`, la teinte globale déjà modélisée en `uint`. `PartySetup` avait
tranché le même cas en août (`&LAB_00404040 + (-(sel != 0) & 0xbfbfc0)`), ce qui a évité de le
reprendre à zéro.

**La règle générale** : dans ce binaire, un `&LAB_00xxxxxx` utilisé comme *valeur* est presque
toujours une constante que Ghidra a prise pour une adresse. Vérifier avant d'en faire un pointeur.

### Le bit `0x80` fait plus que changer une couleur

`personnage[0x2be] & 0x80` choisit la teinte de la première moitié, puis **les deux teintes se
croisent** pour la seconde. Et dans la branche où le bit est posé, l'original glisse un
`iVar1 = (int)param_1` au milieu de la branche de couleur : l'emplacement d'ordonnancement passe de
`otBase + 1` à `otBase`, donc toute la fin se dessine un plan plus près. Une ligne discrète au milieu
d'un `if` de couleur, et elle change ce que le joueur voit.

### Une table qui s'indexe en négatif, et ce qu'elle impose

`DAT_0056a851` donne l'index d'icône, en **octets signés**, et deux de ses entrées valent `-1`. Or
l'index sert tel quel : `DAT_0056aae0 + (-1) * 8` lit les huit derniers octets de… `DAT_0056aaa0`,
la table des réactions par classe relevée le même jour.

Les deux tables ont donc été réunies en **une seule allocation** (`s_IconTables_0056aaa0`,
0x148 octets), dont les cinq symboles connus sont des vues. Les garder séparées aurait fait lire du
tas quelconque à l'entrée `-1` — le défaut d'aliasing d'`ARCHITECTURE.md`, mais dans l'autre sens :
non pas deux allocations pour une adresse, mais deux allocations là où le binaire compte sur
l'adjacence.

L'entrée `-1` ainsi lue a une **hauteur nulle**, donc elle ne dessine rien. C'est vraisemblablement
la façon dont le jeu de 1998 écrit « pas d'icône » — mais la conclusion tient seulement parce que la
mémoire est disposée comme dans le binaire.

### Ce qui reste incertain

Le pas de `DAT_0056a851` et l'étendue de son second index ne s'accordent pas : le premier facteur
avance de neuf, le second va jusqu'à quinze, donc les rangées se chevauchent. Soit la table est
volontairement dentelée, soit `personnage[0x2be] & 0xf` ne dépasse jamais huit en pratique.
L'étendue retenue (0x50 octets) est celle que l'indexation *peut* atteindre, pas une fin observée.
Un `TODO(REVERSE)` le dit sur place.

### Vérification

Pas de régression : 1921 images, aucune exception. La surimpression est gardée par un bit que le
parcours scripté ne lève pas, donc elle est **écrite et compilée, pas exercée**.

---

## 2026-08-24 — le son, et la leçon de méthode qui l'a débloqué

Le jeu était muet. La pile audio, elle, était complète depuis des semaines : enveloppes DirectSound,
codec MS-ADPCM, les cinq rééchantillonneurs, le son positionnel, la file des sons différés. Tout
existait, rien ne sonnait. Trois lacunes se tenaient en travers, et **les trois étaient la même
lacune** : un corps de fonction que Ghidra n'a jamais décompilé.

### Ce que la journée a réellement appris

**Une conclusion « ce site d'appel n'existe pas » tirée de `Blaze.c` seul ne vaut rien.** TODO #26
disait, mot pour mot, qu'aucun appel à `DirectSoundCreate` n'avait pu être localisé, et en déduisait
que le périphérique devait naître dans du code absent de toute source disponible. C'était faux : il
y a exactement un site, en `0x00512f20`. Il était invisible parce qu'il vit dans un `LAB_` sans
corps — et depuis `Blaze.c`, **un trou du décompilateur et une absence réelle se ressemblent
exactement**.

La mesure qui tranche coûte une passe sur 1,35 Mo :

```powershell
# pour chaque octet de .text : E8 dont la cible est le thunk, ou FF15/FF25 vers l'entrée d'IAT
# thunk DirectSoundCreate = 0x00545c68, entrée d'IAT = 0x006084b8
```

Un seul résultat, et il est dans un `LAB_`. **À refaire chaque fois qu'une globale d'API paraît
n'avoir aucun écrivain.** Le balayage linéaire du désassembleur ne remplace pas ce test : lancé sur
tout `.text` depuis `0x00401000`, il se désynchronise au bout de 120 Ko sur le premier îlot de
données. C'est un test d'octets, pas un test d'instructions.

### `LAB_00512ef0` répond à deux questions d'un coup

L'initialisation du son écrit aussi `DAT_005db85c`. TODO #6 avait cherché la création de la fenêtre
principale dans les six sources et conclu qu'elle n'y était pas — vrai, et pour une raison qu'aucune
recherche dans les six sources ne pouvait donner : **`Blaze.exe` ne crée pas sa fenêtre, winsys3d la
lui rend**, en argument du rappel de l'ordinal 790. La chaîne complète :

```
RunApplication → Ordinal_790(&LAB_00512ef0)      range le rappel dans DAT_011be798
              → Ordinal_789(0, résolution)       crée le périphérique 3D
                 └→ (*DAT_011be798)(DAT_011be79c)  ← la HWND arrive ici
                     └→ DAT_005db85c = hwnd ; SetWindowTextA ; DirectSoundCreate ; …
```

Ghidra montre le premier des deux sites d'appel de winsys3d comme prenant zéro argument
(`(*DAT_011be798)()`). Le désassemblage montre `push ecx` juste avant `call eax` aux **deux** sites.
La signature est `void __stdcall (HWND)` — le `ret 4` du rappel le confirme de son côté.

### Deux erreurs de lecture antérieures, corrigées au passage

**`FUN_005135d0` fermait huit poignées sur neuf.** La note en place disait « la borne de Ghidra est
`0x5db85c`, donc la boucle s'arrête juste avant `DAT_005db858` : l'événement d'arrêt fuit à chaque
redémarrage ». La borne `0x5db85c` **inclut** `0x5db858` : neuf tours, pas huit. Il n'y avait pas de
fuite, il y avait une poignée non fermée que nous ajoutions. Le thread de flux le prouve de
l'extérieur : il attend `WaitForMultipleObjects(9, &DAT_005db838, …)`. `0x005db838 + 9*4 ==
0x005db85c`, l'adresse exacte de la fenêtre principale qui suit — les deux mesures se recoupent à
l'octet.

**Le décompilateur et le désassembleur ne disent pas la même chose du thread de flux**, et c'est le
désassembleur qui a raison. `FUN_00513660.c` modélise les deux locaux de pile en tableaux
(`local_8`, `local_4`), donc passe des **adresses** là où le code machine passe des **valeurs** : sa
destination de décodage est `puVar3`, l'adresse d'un local, et son `Unlock` reçoit un compte
d'octets `uVar4 = 0`. En `0x00513706` le binaire fait `mov esi,[esp+14h]` — une *lecture* du
pointeur verrouillé. Suivre le texte décompilé aurait décodé la musique par-dessus le cadre de pile
du thread.

### Ce qui est mesuré, et comment

Le son ne laisse aucune trace dans une capture. La méthode de vérification est écrite dans
`Documentation/Tests/test-protocol.md` ; en résumé, `IDirectSoundBuffer::GetStatus` rendant `0x5`
(`PLAYING|LOOPING`) est la seule preuve qui vaille, et le titre de la fenêtre passant de
`OpenForesia` à `Blaze & Blade` est la preuve gratuite que l'initialisation a eu lieu.

Relevé : `Title01.WAV` au menu, `Menu.WAV` à la constitution du groupe, `Mel00_01.WAV` à l'auberge ;
tampon de 323 840 octets, atténuation −1250 pour `CdVol=64` ; bruitages `0x38` (décalage 769 440,
longueur 8 152) et `0x3a` (780 028 / 1 308). Aucun plantage, 1 621 images, 557 primitives par image
sur le chemin complet.

### Ce que ça laisse ouvert

* Les identifiants de son `0x3c`..`0x4c` restent muets — c'est le défaut d'aliasing de
  `s_SoundBankHeader_005e8520`, recensé et volontairement non corrigé.
* Les entrées `0x22`..`0x24` de la table des morceaux pointent sur un tampon que seul le code
  d'écran `0x00505xxx` remplit.
* LB-011 : au bouclage d'un morceau, la seconde passe de décodage écrit par-dessus la première.
  Fidèle, et à laisser tel quel.

---

## 2026-08-24 (suite) — la panne aléatoire de mémoire basse, et pourquoi elle tuait la session

Vincent a signalé pour la seconde fois `No free block of 0x2000 bytes below 0x01000000`, cette
fois avec le journal. La panne était connue et documentée depuis le 2026-08-19 ; ce qui manquait,
c'était la distinction entre ses deux formes.

### Le fond : la fenêtre basse est petite, disputée, et différente à chaque lancement

Les descripteurs d'interface portent leur propre ordering table à `+0xa4`, que `ClearOTagR` relie
en masquant sur 24 bits. Au-dessus de `0x01000000` un descripteur relie sa liste sur des adresses
tronquées. Ils doivent donc vivre sous cette barre — dans 16 Mo que se disputent aussi le runtime
.NET, `winsys3d.dll` (image de 8,3 Mo) et l'arène de liste d'affichage (2,6 Mo d'un tenant).

Le relevé ajouté ce jour chiffre la dispute. Trois lancements consécutifs, rien de changé, marge
restante après toutes nos réservations :

| lancement | libre total | plus grand intervalle |
|---|---|---|
| 1 | 0x7D1000 (8,2 Mo) | 0x690000 (6,8 Mo) |
| 2 | 0xB0B000 (11,5 Mo) | 0xA50000 (10,6 Mo) |
| 3 | **0x33B000 (3,4 Mo)** | **0x1A0000 (1,6 Mo)** |

C'est l'ASLR, et l'amplitude est le mécanisme même de la panne. Sur le troisième lancement, l'arène
n'aurait pas tenu dans le plus grand intervalle restant si elle n'avait pas été servie en premier.

### Ce qui distinguait vraiment les deux formes

Il y a **neuf** sites d'allocation basse. Quatre étaient pré-réclamés par
`TryPrepareDisplayListArena`, **cinq étaient pris paresseusement**. Cette différence est tout :

* un échec **au démarrage** est levé à l'intérieur du `try` de `TryPrepareDisplayListArena`, rendu
  comme `false`, et `Program.cs` relance un processus neuf — jusqu'à trois fois. La disposition de
  l'espace d'adressage étant différente à chaque lancement, c'est une vraie seconde chance ;
* un échec **en cours de partie** n'a rien pour le rattraper. Il traverse toute la pile et tue la
  session.

Or trois des cinq sites paresseux — l'écran de chargement, les quatre panneaux du monde, le
descripteur de carte — sont pris **exactement pendant un chargement de zone**, c'est-à-dire au pire
moment : le runtime a eu plusieurs secondes pour grandir dans la même fenêtre. Le raisonnement qui
avait fait remonter les quatre premiers s'appliquait mot pour mot aux cinq autres et n'avait pas été
poussé jusqu'au bout.

**Correction** : les cinq rejoignent la liste pré-réclamée. Après quoi plus aucune `AllocLowZeroed`
n'a lieu après le démarrage — les neuf sites sont des singletons paresseux, tous forcés. Vérifié sur
les trois chemins (menu, menu objets, carte du monde) : aucune allocation basse tardive.

Ce n'est **pas** rendre la panne impossible. La fenêtre reste la fenêtre. C'est déplacer toutes ses
occurrences là où la relance sait les rattraper.

### Une garde, parce que la liste peut se démoder

Un dixième site ajouté un jour sans rejoindre la liste réintroduirait le problème en silence.
`AllocLowZeroed` imprime donc `[BAS] ATTENTION : <nom> alloue bas APRES le demarrage` si elle est
appelée une fois les réservations faites. Elle n'interrompt rien — l'allocation a réussi — mais la
prochaine personne le verra dans le journal du premier test.

### Le message d'échec ne désignait pas le coupable

`No free block of 0x2000 bytes` ne dit pas quel descripteur. La note de test s'était construit une
table taille → site pour combler ce trou, et **cette table était fausse** : elle donnait `0x4000`
pour l'écran de chargement, qui fait 2 × `0x8c8` = `0x1190`, donc `0x2000` ; et **trois** sites
distincts arrondissent à `0x2000`. Une occurrence ne pouvait donc pas être attribuée.

`AllocLowZeroed` prend maintenant un `[CallerMemberName]` — zéro modification aux neuf sites — et
son message nomme le membre appelant, puis le total libre et le plus grand intervalle sous le
plafond. Les deux chiffres séparent « fenêtre pleine » de « fenêtre fragmentée », qui n'appellent
pas la même suite.

**Leçon** : quand un message d'erreur oblige la documentation à tenir une table de correspondance
pour être exploitable, c'est le message qu'il faut corriger, pas la table. Celle-ci a vécu cinq
jours en étant fausse sans que personne s'en aperçoive, faute d'occurrence à y confronter.

---

## 2026-08-24 (fin) — la table de sauts d'entites 0x0056adc8, et ce qu'un plantage y designe

`NotImplementedException: FUN_0050e5e0, table 0x005fa5e0 …` signale par Vincent. **`FUN_0050e5e0`
n'est pas en cause** : elle est transcrite depuis longtemps (`TickEntityTable_005fa5e0`). Ce qui
manque est toujours le **gestionnaire d'entite** qu'elle atteint par la table de sauts 0x0056adc8,
et c'est la fin du message — `gestionnaire d'entite 0x4XXXXX non transcrit` — qui le nomme.

### La table entiere, pour l'index de zone 0

Relevee dans `Resources.s_VtSource_0056adc8`. C'est la carte a consulter la prochaine fois : elle
mene de l'adresse du message au sous-type, et du sous-type au travail restant.

| sous-types | cible | etat |
|---|---|---|
| 0, 1, **19..63** | `0x004734B0` | **transcrit le 2026-08-24** — le saut sur place, 47 creneaux sur 64 |
| 2 | `0x004736D0` | **transcrit le 2026-08-24** — depuis le desassemblage |
| 3 | `0x004738A0` | **transcrit** — la chute avec butee |
| 4 | `0x00473A90` | **transcrit** — le glissement sur le cap |
| 5 | `0x00473CB0` | **transcrit** — son miroir |
| 6, 7 | `0x00473A70` | **transcrit le 2026-08-24** — l'enonce fixe |
| 8, 9, 10, 18 | `0x4508D0`, `0x4509B0`, `0x450AA0`, `0x450D30` | transcrits anterieurement |
| 11 | `0x00450BD0` | **transcrit le 2026-08-24** — le scintillement |
| 12 | `0x00450C60` | **transcrit le 2026-08-24** — l'entite qui palpite |
| 13 | `0x00473ED0` | **transcrit le 2026-08-24** — le coffre |
| 14 | `0x004746C0` | **transcrit le 2026-08-24** |
| 15 | `0x004508B0` | **transcrit le 2026-08-24** |
| 16 | `0x00450760` | **transcrit le 2026-08-24** — le declencheur de script |
| 17 | `0x004502E0` | **transcrit le 2026-08-24 (suite 4)** — le quasi-jumeau du coffre |

### Ce qui a ete fait

`FUN_004727d0` (248 l.), **l'automate commun** des quatre gestionnaires transcrits, avec sa
dependance `FUN_00472770` et `FUN_005037e0`. Restent en lacune nommee : `FUN_00472e80` (244 l.,
huit dependances) pour les etats que l'automate ne traite pas lui-meme.

**Deux pieges de lecture, tous deux payes comptant :**

**Un.** `if (sVar3 != 99)` entre dans le bloc commun ; le bloc de contact n'est atteint que dans
l'etat 99. La premiere transcription avait le test **a l'envers**, ce qui aurait envoye tous les
etats ordinaires dans le chemin de contact.

**Deux.** Lire un corps par tranches fait manquer des branches. `FUN_004734b0` a d'abord ete lu
jusqu'a l'etat `0xcb` et transcrit comme s'il s'arretait la ; il a **six** groupes d'etats, dont un
— `0xb`/`0xc` — qui n'est pas un saut mais une **chute avec butee** (hauteur decroissante, sortie
sur la distance cumulee passant 0xc0, puis replacement exact a 0xc1). La regle : mesurer la
longueur du corps *avant* de le lire, et verifier que la derniere accolade est dans la tranche.

### Ce qui n'est pas verifie, et pourquoi

**Les quatre gestionnaires n'ont pas ete exerces.** Une sonde posee au site de dispatch montre que
l'auberge n'anime que les sous-types **8, 9, 10 et 18** — tous transcrits avant aujourd'hui. Le
plantage de Vincent vient donc d'un sous-type qui n'apparait pas sur ce parcours, et rien ne dit
lequel : le message complet le nommait, mais seule sa premiere ligne a ete relevee.

Ces quatre corps sont donc **lus contre `Blaze.c` et compiles, pas joues**. C'est la meme situation
que la surimpression de dialogue du 2026-08-23, et elle se note de la meme facon plutot que de se
presenter comme une correction confirmee.

**Methode pour la prochaine fois** : relever la ligne d'exception **entiere**. Elle porte le
sous-type, l'adresse du gestionnaire et les seize premiers octets de l'enregistrement — de quoi
aller directement a la bonne ligne du tableau ci-dessus.

---

## 2026-08-24 (suite 2) — les trois derniers gestionnaires que `Blaze.c` contenait

La table 0x0056adc8 comptait encore neuf sous-types decouverts. **Trois avaient un corps dans
`Blaze.c`** ; les six autres n'en ont pas et relevent du desassemblage. Les trois sont transcrits.

| sous-type | cible | ce que c'est |
|---|---|---|
| 12 | `0x00450C60` | une entite qui **palpite** : les trois facteurs d'echelle oscillent autour de 0x1000, un son d'ambiance tous les 0x3c pas |
| 16 | `0x00450760` | une entite qui **declenche un script sur contact**, avec descente et remontee de trente pas |
| 13 | `0x00473ED0` | **le coffre** — le plus fourni de la famille, et le seul a remettre un objet au groupe |

Avec eux, quatre dependances : `FUN_00460fc0` (11 l.), `FUN_004742b0` (38 l.), et la resolution de
deux symboles qui n'etaient pas des fonctions.

### Deux valeurs lues dans le binaire, et ce qu'elles ont evite

**`PTR_FUN_0056afac` vaut `0x00401FA0`.** C'est la souche `ret` nu que la table de sauts d'entites
utilise partout ailleurs comme creneau vide. L'appel indirect de l'etat 4 de `FUN_00450760` est
donc un **no-op verifie**, pas une lacune — il est transcrit comme rien du tout, avec la mesure en
commentaire. Sans cette lecture il aurait fallu poser un `NotImplementedException` sur un chemin
qui ne fait rien.

**`DAT_00571990` n'est pas une globale isolee** : elle tombe dans `s_EntityState_00571500`
(0x00571500..0x00571a3c), qui a deja son accesseur `Mem.EntityState(va)`. Rien a allouer. C'est
exactement le cas que la carte des symboles de `DataSymbols.md` existe pour attraper — la
consulter avant de declarer un `DAT_` isole a coute une minute et economise une allocation
aliasante de plus.

### Le coffre : deux champs de bits d'arene comme memoire persistante

`FUN_00473ed0` porte l'etat dans +0x10, avec des valeurs **negatives** (`-0xff`, `-2`, `-1`) que
Ghidra rend en `undefined2` non signe (`0xffff`, `0xfffe`). Elles sont lues en `short` signe, ce que
fait le binaire.

Deux champs de bits retiennent ce qui a deja eu lieu, tous deux ecrits par le meme motif que Ghidra
obscurcit en `bVar2 - (bVar2 & 0xf8)` — c'est `n & 7` :

* `DAT_005e96e0` (banque d'etat 0), indexe par le numero de coffre de +0x9a : **le coffre a deja
  ete ouvert**. Teste a l'ouverture, pose a la fermeture.
* `DAT_005e9840`, indexe par +0x14 : le contenu a ete **refuse**.

Le calcul de sortie `(-(ushort)(sVar4 != 0) & 0xfce1) + 999` vaut 999 quand l'objet est passe et
200 quand il ne l'est pas ; il est ecrit pour ce qu'il vaut plutot que transcrit en arithmetique.

**Une lacune reste, et elle ne bloque pas ce corps** : `LAB_00474320`, le rappel de fin de boite,
n'est pas appele ici — il est **range** dans le descripteur par `GrantChestContents_00530F20` et
rejoue plus tard. Ce corps-ci tourne sans lui ; c'est le rangement effectif du contenu qui en
depend.

### Verification : toujours pas exerces

Une sonde relevant chaque sous-type **distinct** sur un parcours long et interactif de l'auberge —
dialogue avec deux PNJ, deplacement dans trois directions — ne voit que **8, 9, 10 et 18**, comme
la mesure precedente. Les sept gestionnaires ajoutes aujourd'hui sont donc **lus contre `Blaze.c`
et compiles, pas joues**.

Ce n'est pas une reserve de forme : la zone de depart n'anime que quatre sous-types sur soixante-
quatre, et rien de ce qui est ajoute ici ne se verifiera avant qu'une autre zone soit atteinte.

### Ce qui reste

Six sous-types — 2, 6, 7, 11, 14, 15, 17 — dont le corps est **absent de `Blaze.c`** :
`0x004736D0`, `0x00473A70`, `0x00450BD0`, `0x004746C0`, `0x004508B0`, `0x004502E0`. Voie du
desassemblage, comme `LAB_00512ef0` et `LAB_00513660` ce matin. Plus `FUN_00472e80` (244 l., huit
dependances) pour les etats que l'automate commun ne traite pas lui-meme.

---

## 2026-08-24 (suite 3) — les corps absents de `Blaze.c`, tires du desassemblage

Six sous-types de la table 0x0056adc8 n'avaient **aucun corps dans `Blaze.c`**, pour la meme raison
que `LAB_00512ef0` et `LAB_00513660` ce matin : Ghidra ne les a vus que pris en adresse dans une
table de sauts, jamais appeles directement, donc jamais promus en fonction. **Cinq sont
transcrits** ; le sixieme est nomme et chiffre.

| sous-type | cible | taille | ce que c'est |
|---|---|---|---|
| 15 | `0x004508B0` | 18 o. | pose une echelle de 0x320 et allume le bit 0x4000 |
| 6, 7 | `0x00473A70` | 23 o. | affiche un enonce fixe et **ignore son entite** |
| 11 | `0x00450BD0` | 130 o. | l'entite qui **scintille** — un gris sinusoidal bruite |
| 14 | `0x004746C0` | 45 o. | une fois sur deux, fait naitre un effet d'etat sur soi |
| 2 | `0x004736D0` | 450 o. | la vitesse amortie qui pousse le cap |
| 17 | `0x004502E0` | 1042 o. | **transcrit** (suite 4) — le coffre, mais l'acquis est du groupe |

Avec eux, `FUN_00468110` (19 l. dans `Blaze.c`, jamais reconstruite).

### Le piege du « c'est le miroir de son frere »

`FUN_004736D0` ressemble trait pour trait a `FUN_004738A0` : meme automate commun, memes quatre
groupes d'etats, meme amortissement d'un huitieme. Les signes de `+0x32` sont bien inverses branche
par branche. **Mais le drapeau `0x40000000` ne se pose pas aux memes etats** : chez `0x4736D0` seul
l'etat 0x16 le pose ; chez `0x4738A0` les etats 2 et 0xca le posent aussi. Et la forme differe :
chez `0x4736D0` les etats 1 et 0xc9 **rendent la main** apres avoir arme la branche suivante, chez
le frere ils tombent dedans dans le meme pas.

Les deux corps sont donc ecrits separement. Les partager derriere un parametre de signe aurait
efface exactement la difference qui compte — et se serait vu comme une simplification elegante.

**La regle** : deux corps qui se ressemblent au desassemblage se transcrivent separement d'abord ;
la factorisation, si elle a lieu, vient apres verification qu'elle ne mange rien.

### Ghidra rate la meme signature pour la troisieme fois

`FUN_00468110` est la **troisieme sœur** de `TryPlaySoundNearEntity` (`FUN_00468080`) et
`TryPublishCameraEffectNearEntity` (`FUN_0041be60`) : meme calcul de distance a la camera, rayon de
1800 au lieu de 2000 et 1500, et une naissance d'entite a la place d'un son ou de trois mots.

Et Ghidra s'y trompe de la meme facon : sa pseudo-C declare trois parametres et ecrit
`(short)param_1 + 0xc0`, ou `param_1` est un pointeur. Le desassemblage donne quatre parametres —
apres trois `push`, le corps lit `[esp+14h]`, `[esp+18h]` et `[esp+1ch]` — et c'est **`param_2`**
qui recoit le `+ 0xC0`. Le site d'appel empile six arguments (`add esp,18h`) ; les deux derniers ne
sont lus nulle part.

**Trois fois le meme motif, trois fois la meme erreur de Ghidra, trois fois le desassemblage qui
tranche.** Quand une fonction se sert de `param_1` a la fois comme pointeur et comme scalaire, ce
n'est jamais du code bizarre : c'est une attribution de registre ratee.

### Deux details de lecture qui inversent le sens

**`or ch,40h` pose 0x4000, pas 0x40** — c'est le deuxieme octet de `ecx`. Dans `FUN_004508B0`, tout
le corps tient dans cette ligne ; s'y tromper aurait pose le mauvais bit d'etat.

**Le modulo signe de MSVC** (`cdq ; xor eax,edx ; sub eax,edx ; and eax,N-1 ; xor eax,edx ; sub
eax,edx`) est `% N` sur un entier **signe**, pas un masque. `FUN_00450BD0` et `FUN_004746C0`
l'emploient tous les deux, pour `% 16` et `% 2`.

### Une lacune assumee : l'enonce japonais

`FUN_00473A70` affiche le bloc d'etiquettes de `0x00551814`. Ses octets sont du **Shift-JIS** — la
chaine japonaise est restee dans le binaire francais. Le bloc n'est pas transcrit : sa **longueur**
n'est pas etablie, et `BindMenuLabels` en a besoin.

Le symbole existe donc, alloue a zero. Un bloc vide n'affiche rien — visible et sans danger — la ou
un pointeur invente aurait fait lire au hasard dans `.data`. C'est la meme decision que pour les
entrees 0x22..0x24 de la table des morceaux : **nommer la lacune plutot que la combler au
jugé**.

### Etat de la table

Les 17 cibles distinctes de la table 0x0056adc8 ont maintenant un cas dans le repartiteur. Seize
sont des corps reels ; `0x004502E0` est une lacune nommee qui se decrit elle-meme. *(Elle a ete
comblee le meme jour — voir la section « suite 4 » plus bas.)*

Les sept gestionnaires d'hier et les cinq d'aujourd'hui restent **non exerces** : la sonde au site
de dispatch ne voit toujours que les sous-types 8, 9, 10 et 18 dans l'auberge.

---

## 2026-08-24 (suite 4) — `FUN_004502E0`, la derniere lacune de la table 0x0056adc8

Le sous-type 17 est transcrit. La table 0x0056adc8 n'a plus de creneau sans corps reel : ses **17
cibles distinctes** sont toutes reconstruites.

### Mesurer avant de lire, une fois de plus

La lacune etait chiffree « ~1030 octets, meme forme que le coffre ». Les deux moities de cette
phrase se sont averees justes, mais elles ne disaient pas la seule chose qui comptait.

La longueur exacte est **1042 octets**, 0x004502e0..0x004506f1 : le `ret` final, puis quatorze
`nop` de bourrage, puis `FUN_00450700` a l'adresse ronde suivante. C'est la regle du 2026-08-24 —
mesurer le corps *avant* de le lire, et verifier que la derniere accolade tient dans la tranche —
appliquee d'entree, et elle a evite de relire le debut d'une autre fonction comme si c'etait la
queue de celle-ci.

Le releve est dans `Legacy/Raw/Blaze/FUN_004502E0.asm`.

### Ce que c'est : le coffre, moins le coffre

`FUN_004502E0` est un **quasi-jumeau** de `FUN_00473ED0`. Meme automate a etats disperses (-0xff,
-2, -1, 0, 1, 2, 0x14, 0x28, 100, 200, 0xc9, 0xca, 300, 999), meme ecriture de +0x9a / +0x98 a
l'entree, meme descente de couvercle de 0x23 sur trente pas, meme remise d'objet par
`FUN_00530f20`, meme calcul sans branchement `(-(ushort)(x != 0) & 0xfce1) + 999` en sortie.

**Cinq differences, pas une de plus** :

| | coffre `0x00473ED0` | sous-type 17 `0x004502E0` |
|---|---|---|
| memoire du « deja pris » | `DAT_005e96e0`, banque d'etat 0 | **`s_PartyAbilityMask_005e9000`** |
| pose du bit | ecriture directe `\|= 1 << (n & 7)` | **`FUN_00514000`, opcode de script 3** |
| valeur lissee de l'ouverture | `DAT_00571990` | **`DAT_00571528`** |
| rappel de fin de boite | `0x00474320` (lacune) | **`0x00450700`** (deja transcrite) |
| passe de fin de pas | `FUN_004742b0` | **`FUN_0040e640`** |

La deuxieme ligne est celle qui donne son sens au sous-type. La ou le coffre marque un bit dans une
banque de zone, celui-ci passe par `ApplyPartyScriptOp` avec l'operande `{ n>>3, 3, 1<<(n&7) }` :
l'opcode 3 est un `|=`, applique au bloc de drapeaux de **chaque membre du groupe** puis reflete
dans le masque commun. Ce n'est pas un coffre a butin, c'est un **acquis permanent du groupe**, et
le champ de bits qu'il teste a l'ouverture est le meme que celui qu'il ecrit a la fermeture.

D'ou le nom donne aux auxiliaires — `StepAbilityChestOpening`, `AbilityChestDispatchTail`,
`AbilityChestGrantContents`. Il vient du symbole `s_PartyAbilityMask`, pas d'une observation en
jeu ; le sous-type n'a pas ete vu tourner.

**La quatrieme ligne a economise le travail** : `0x00450700` etait deja reconstruite depuis le
2026-08-23 sous le nom `ChestPickupDone_00450700`, et le repartiteur de rappels de `UiText` la
connait deja. La lacune `LAB_00474320` que le coffre traine ne se reproduit donc pas ici : ce corps
range reellement son contenu.

### `FUN_0040e640`, le grand frere de `FUN_004742b0`

La cinquieme difference n'etait pas visible dans le nom. Les deux fonctions balayent les **memes
trois tables d'entites** (0x005ff400 sur 0x20, 0x005f2d40 et 0x005edee0 sur 0x80) pour eteindre le
bit 0x04000000 des enregistrements non epingles. `FUN_0040e640` en fait deux de plus :

* sur la **premiere** table seulement, les sous-types `0x1c` et `0x17` recoivent le bit
  `0x40000000` — c'est pourquoi cette passe ne peut pas reutiliser `ClearWakeBitOnTable` ;
* une **quatrieme** boucle remet les quatre fiches d'emplacement du groupe a blanc : trois octets
  de couleur a 0xff en +0x38 et le bit 2 efface en +0x36.

Ghidra ecrit cette derniere boucle en decalages **negatifs** autour de `&DAT_005e93f9`
(`puVar2[-1]`, `*puVar2`, `puVar2[1]`, `*(ushort *)(puVar2 - 3)`), ce qui masque completement la
base. Le pas de 0x9c et l'adresse 0x005e93f6 la donnent : c'est `s_SlotFlags_005e93c0 + 0x36`, la
meme table que `UiWindow` interroge. **Une boucle ecrite autour d'un point milieu ne nomme pas sa
base ; c'est le pas qui la nomme.**

### Un detail de pile a ne pas transcrire betement

L'etat 100 appelle `FUN_00514000(&operande, &param_1)` — `lea ecx,[esp+18h]`, c'est-a-dire
**l'adresse de son propre emplacement d'argument**. La fonction ecrase donc le pointeur d'entite
qu'elle a recu, pour y ranger un resultat que personne ne lit. C'est sans consequence : a partir de
la, le corps ne travaille plus que par ESI. Transcrit par une variable locale morte plutot que par
une reproduction de l'ecrasement, avec la mesure en commentaire.

Meme famille : l'operande de quatre octets est initialisee **a l'entree de la fonction** a
`{ 0, 1, 4, 0xEF }`, et ses trois derniers octets sont reecrits avant le seul site qui la lit. Ces
valeurs d'entree sont mortes. Elles sont ecrites quand meme — le cout est nul et c'est ce que fait
le binaire.

### Verification

`dotnet build` : 0 erreur. Le parcours long de l'auberge (`-Entry 0 … -Then WAIT5,ENTER,WAIT2,
START,WAIT6,ENTER,WAIT5,ENTER,WAIT10`) rend **1621 images, aucun plantage, 557 primitives par
image**, et la capture montre l'auberge avec la boite « - La réception - ».

C'est une **non-regression, pas une confirmation** : comme les onze gestionnaires precedents, le
sous-type 17 n'apparait pas dans la zone de depart. Le corps est lu contre le desassemblage et
compile, pas joue. Il ne se verifiera qu'avec une autre zone.

---

## 2026-08-24 (suite 5) — `FUN_00472e80`, et ce qu'un registre ecrase

La queue de l'automate commun des entites de zone est transcrite, avec sa dependance
`FUN_00466d70`. Il ne reste plus, autour de la table 0x0056adc8, que `LAB_00474320` (le rappel de
fin de boite du coffre) et le bloc d'etiquettes Shift-JIS de `0x00551814`.

### La signature perdue, et la ligne du binaire qui l'explique

`Blaze.c` declare `uint FUN_00472e80(uint *param_1, short param_2)` alors que le site d'appel
empile **trois** arguments, puis reaffecte `param_1` en compteur de boucle au milieu du corps.
C'etait la raison invoquee pour ne pas la transcrire : « parametres non recuperes par Ghidra ».

Le desassemblage tranche en une lecture. La branche de l'etat 0xcd **reutilise les emplacements
d'arguments comme variables locales** :

```
00472f5c  mov dword [esp+18h],0     ; l'emplacement de param_2 devient un compteur
00472f69  mov dword [esp+14h],4     ; celui de param_1 devient le pas de boucle
```

Les deux ont ete recopies en registres (`esi`, puis `ecx` via `movsx ecx,word [esp+18h]`) avant
d'etre ecrases. Ghidra, qui suit les emplacements, voit `param_1 = 4` et perd le fil.

Le troisieme argument, lui, **n'est jamais lu** : `[esp+1ch]` n'apparait nulle part dans les 1584
octets du corps. Le site d'appel en empile pourtant trois. `soundB` est donc mort, et garde dans la
signature C# pour que l'appel reste comparable a `Blaze.c`.

**La regle** : « Ghidra a perdu les parametres » ne veut pas dire « la convention d'appel est a
reconstruire depuis le trafic de registres ». Trois fois sur quatre, cela veut dire qu'un
emplacement d'argument a ete recycle en local, et le prologue suffit a le voir.

### Le retour est un `short`, et Ghidra le dit de six facons

`CONCAT22(…, 1)`, `& 0xffff0000`, `<< 0x10`, `extraout_var`, `extraout_var_00`, `in_EAX` : six
formes differentes pour la meme chose. Le binaire est univoque — chaque `ret` est precede de
`mov ax,1` ou de `xor ax,ax`. Le reste est du bruit de suivi de registre 16/32 bits.

Sauf deux sorties, qui ne posent AX ni d'une facon ni de l'autre : **LB-012**.

### `extraout_AX` avait un nom

Le test que Ghidra ecrit `if ((sVar10 == extraout_AX) && …)` est simplement
`if (near == FUN_00466d70() && …)`. La fonction est appelee **inconditionnellement** juste avant, et
`FUN_00466d70` fait onze lignes : elle compte les emplacements du groupe dont le masque
`0x50000000` est vide, et rend -1 quand il n'y en a aucun.

Elle etait citee comme « dependance non transcrite » ; elle etait en realite la plus petite du lot.

### Trois comptages, trois masques, et pourquoi ils ne se factorisent pas

L'etat 0xcd balaye **trois fois** les quatre fiches d'emplacement de `0x005e93c0`, et les trois
passes n'ont pas le meme masque :

| passe | masque exige nul | ce qu'elle compte |
|---|---|---|
| 1 | `0x50000001` | les membres assez **eloignes** de l'entite — rayon 500, ou 300 si +0x98 vaut 1 |
| 2 | `0x50010000` | les emplacements **vivants** |
| 3 | `0x50010000` + bit `0x04000000` pose | ceux d'entre eux que la passe 1 a **marques** |

Et `FUN_00466d70`, appelee ensuite, en emploie un quatrieme : `0x50000000`. Quatre comptages, quatre
masques, deux issues concurrentes vers l'etat 0xce. Les replier aurait efface exactement la
distinction qui decide laquelle des deux se declenche.

La passe 1 a un effet de bord : quand +0x98 vaut 2, chaque membre assez loin recoit le bit
`0x04000000` **et** voit naitre l'effet d'etat 0x9d sur lui. C'est ce qui rend la passe 3 non vide.

### L'etat 0xce est le miroir exact d'un bloc deja reconstruit

`ResetCameraAndWakeSceneEntities`, extraite le 2026-08-24 de `EntityAnimCommon` pour l'etat 0x10,
rebranche les trois globales de camera (`DAT_005eca94`, `DAT_005e9ae0`, `DAT_005edebc`) sur les
tables ordinaires et reveille les entites que le masque `DAT_00604b84` designe.

L'etat 0xce est sa moitie **active** : il recopie ce que les tables portaient dans
`0x00571980..0x005719a0`, y detourne les trois globales, **construit** le masque, gele les entites,
et eteint le bit d'eveil des entites de zone de sous-type 1..7. Les deux blocs se lisent en vis-a-vis.

Deux details qui ne se voient que dans le binaire :

* **`DAT_00571990` est ecrit deux fois** — d'abord avec la distance courante recopiee de la table,
  puis avec `rand() % 200 + 1000`. La premiere ecriture est morte. Transcrite quand meme.
* **L'entite est detruite en cours de bloc** : `xor esi,esi` a 0x00473216 recycle le registre qui la
  portait en compteur de bits. Rien ne la relit ensuite, et la branche rend 1 immediatement. C'est
  cette destruction qui rend le corps illisible dans `Blaze.c`, ou `puVar9` change de sens au
  milieu d'une fonction.

### Une asymetrie de sortie a ne pas lisser

L'etat 0xd3 libere le sujet par deux chemins. Celui de la famille d'objets `(classe & 7) == 2`
(`LAB_00473454`) remet **aussi** `+0x16` a zero ; celui de l'autre famille (`LAB_00473490`) ne le
fait pas. Meme trois instructions, une de moins. C'est le motif du 2026-08-24 sur `FUN_004736D0` :
deux corps qui se ressemblent au desassemblage se transcrivent separement d'abord.

Ici les deux sont partages derriere `ReleaseSubjectPosture(e, subject, clearEvent)` — le drapeau
**est** la difference, il ne l'efface pas.

### Verification

`dotnet build` : 0 erreur. Le parcours long de l'auberge rend **1621 images, aucun plantage, 557
primitives par image** — chiffres **identiques** a ceux du run precedent, ce qui est en soi la
mesure : la queue n'a pas ete atteinte une seule fois. Comme les douze gestionnaires ajoutes
aujourd'hui, elle est lue contre le desassemblage et compilee, pas jouee.

---

## 2026-08-24 (suite 6) — `LAB_00474320` n'existait pas

La derniere lacune de la famille du coffre est close, et elle n'a rien coute : **il n'y avait rien
a transcrire**.

`LAB_00474320` est le rappel de fin de boite que `FUN_00473ED0` range dans le descripteur de texte.
Il figurait depuis deux jours comme une lacune nommee — « un bloc dont Ghidra n'a vu que l'adresse
prise », « le rangement effectif du contenu en depend ». Le desassemblage rend 87 octets qui
ressemblaient furieusement a ceux de `FUN_00450700`, deja transcrite sous le nom
`ChestPickupDone_00450700`.

Ils ne se ressemblent pas : ils sont **les memes**. Comparaison des octets bruts, sur les deux
decalages fichier (`VA - 0x400C00`) :

```
od -An -tx1 -j $((0x4FB00)) -N 87 Legacy/Binaries/Blaze.exe   # 0x00450700
od -An -tx1 -j $((0x73720)) -N 87 Legacy/Binaries/Blaze.exe   # 0x00474320
```

**Une seule difference sur 87 octets** : les quatre octets de deplacement du `call`, qui est relatif
et doit donc differer. Les deux visent la meme cible :

* `0x0045072D + 0x000E0BA3 = 0x005312D0`
* `0x0047434D + 0x000BCF83 = 0x005312D0`

Le compilateur a emis deux fois le meme corps sans les replier — pas de repliement de COMDAT
identiques sur cette compilation. Les deux adresses sont donc aiguillees sur la meme transcription
dans le repartiteur de rappels de `Engine/UiText.cs`, ce qui est **fidele** et non une
approximation.

### Ce que ca change

Le coffre range reellement son contenu. La reserve portee par `EntityHandler_00473ED0` — « ce
corps-ci tourne sans lui ; c'est le rangement effectif du contenu qui en depend » — tombe.

### La lecon

**Avant de transcrire un corps absent de `Blaze.c`, verifier qu'il n'est pas le double d'un corps
deja transcrit.** Le test tient en deux `od` et une soustraction, et il aurait fallu le faire le
2026-08-23, quand `FUN_00450700` a ete transcrite et que `LAB_00474320` figurait deja au tableau
des lacunes a trois lignes de distance. Deux adresses, meme taille, meme famille de code : le
soupcon etait la, et il n'a pas ete leve.

Le signe qui aurait du alerter : `GrantChestContents_00530F20` recevait `0x00450700` de deux
appelants et `0x00474320` d'un troisieme, pour un role identique. Un rappel qui fait la meme chose
depuis deux adresses est presque toujours le meme rappel.

### Verification

`dotnet build` : 0 erreur. Parcours long de l'auberge : **1621 images, aucun plantage, 557
primitives par image**, inchange. Le chemin ne se verifiera qu'en ouvrant un coffre : il n'y en a
pas dans la zone de depart, et le sous-type 13 n'y est pas anime.

### Etat de la famille du coffre

Il ne reste qu'une lacune autour de la table 0x0056adc8 : le bloc d'etiquettes `0x00551814`, du
Shift-JIS reste dans le binaire francais, dont la **longueur** n'est pas etablie.

---

## 2026-08-24 (suite 7) — le bloc 0x00551814, et pourquoi il etait en japonais

La derniere lacune de la famille des entites de zone est comblee. Elle contenait **un message de
developpeur**.

Le bloc d'etiquettes de `0x00551814`, affiche par `FUN_00473A70` pour les sous-types 6 et 7, fait
**66 octets** jusqu'a son terminateur. Decode en Shift-JIS, son texte est :

> 片開きのドアは現在使用出来ません。今井まで連絡ください。
>
> « La porte a un battant n'est pas utilisable pour l'instant. Contactez Imai. »

**今井 est un nom propre**, celui d'un membre de l'equipe. Ce n'etait donc pas une phrase de jeu
oubliee a la traduction : c'est une **note interne** laissee a qui construisait les niveaux, et
expediee telle quelle dans la version vendue. Les sous-types 6 et 7 sont une porte **inachevee**.

Cela repond a la question que la note du 2026-08-24 laissait ouverte — « la chaine japonaise est
restee dans le binaire francais » — et elle avait une meilleure reponse que l'oubli : le texte n'a
jamais eu de traduction parce que personne n'etait cense le lire.

### La longueur ne se lisait pas dans un en-tete

La lacune tenait a une hypothese fausse, ecrite noir sur blanc dans le commentaire du symbole :
« un en-tete de huit octets suivi du texte, comme les autres blocs d'etiquettes ». Il n'y a pas
d'en-tete, et il n'y en a dans aucun bloc d'etiquettes.

Un bloc d'etiquettes est un **flux** : octet ≥ 0x20 = texte, octet < 0x20 = code de controle avec
son propre nombre d'operandes, fin au premier 0x00 **qui n'est pas une operande**. La longueur ne
se lit pas, elle se **mesure en parcourant**.

Et l'outil pour la mesurer etait deja dans le depot depuis le 2026-08-15 : la table de la passe 1,
`s_ScanTable_0056e870` dans `Engine/UiText.cs`, dont quatre entrees sont des talons d'avance pure
(`0x00543940`..`0x00543970` = 0 a 3 operandes). Le compte de chaque code s'y lit d'un coup d'œil.

Ici **deux `0x00` sont des operandes** avant celui qui termine. S'arreter au premier zero — le
reflexe naturel — aurait donne cinq octets au lieu de soixante-six.

**La lecon** : quand une lacune porte « la longueur n'est pas etablie », verifier d'abord si la
donnee a vraiment une longueur declaree. Un flux auto-terminant n'en a pas, et le decodeur qui le
consomme est la specification de sa fin. Le releve complet est dans `DataSymbols.md`.

### Une scenette, pas un libelle

Le flux fait plus qu'afficher du texte : `1f` place la boite en (0xd0, 0x20), `08 00` gele le monde,
`0c` ouvre la boite, puis les 28 paires Shift-JIS, puis `0f` attend le joueur en montrant le
curseur, `08 01` degele, `00` termine. Les huit gestionnaires que ces codes atteignent sont tous
reconstruits, dans les deux passes : le bloc traverse le moteur de texte sans lacune.

### Verification

`dotnet build` : 0 erreur. Parcours de l'auberge : **1441 images, aucun plantage, 557 primitives
par image**, inchange. Le bloc ne s'affichera qu'a une porte de sous-type 6 ou 7, qui n'existe pas
dans la zone de depart — mais le chemin est desormais complet de bout en bout, ce qui n'etait pas
le cas quand le symbole etait un tampon de seize zeros.

### Etat de la famille

La table 0x0056adc8 et tout ce qui gravite autour n'ont plus de lacune nommee.

---

## 2026-08-24 (suite 8) — `FUN_00472770` etait deja transcrite ; sa verification a trouve autre chose

`FUN_00472770` n'est pas une lacune : elle a ete reconstruite le 2026-08-24 sous le nom
`ActivePartySlot_00472770`, en meme temps que l'automate commun `FUN_004727d0`. Le passage au
desassembleur avait un autre objet — confirmer la seule lecture **interpretative** de ce corps, le
retour que Ghidra rend par `CONCAT31((int3)(uVar1 >> 8), DAT_005de538) & 0xffff00ff`.

### La lecture du retour est confirmee

```
00472797  6633c0    xor  ax,ax
0047279a  8ac1      mov  al,cl
0047279c  c3        ret
```

AX vaut l'octet de `DAT_005de538` ; les seize bits hauts d'EAX gardent les drapeaux lus juste
avant, ce qui est exactement le `uVar1 >> 8` que Ghidra recompose. L'appelant ne lit qu'un `short`,
donc la valeur utile est l'octet. La transcription disait vrai.

La boucle aussi : `inc eax ; cmp ax,3 ; jle` rend bien **4** quand aucun des quatre emplacements ne
convient, et l'appelant s'en sert tel quel.

### Ce que la verification a trouve en passant

Le prologue lit le global **sur un octet** :

```
00472770  8a0d38e55d00  mov  cl,[5DE538h]
00472776  33c0          xor  eax,eax
00472778  8ac1          mov  al,cl
```

Or la transcription indexait la table avec `Mem.s_Unknown_005de538` **non masque**, tout en masquant
le retour par `& 0xff` trois lignes plus bas. Deux lectures du meme global dans la meme fonction,
de deux largeurs differentes : l'une des deux etait fausse.

Le masque est ajoute a l'indice. **Le defaut etait latent, pas vivant** : les cinq ecrivains
reconstruits (`Engine/Resources.cs`, `Screens/State00/MainMenu.cs`, trois sites de
`Screens/State03/WorldMapTick.cs`) ecrivent tous un octet ou zero, comme les huit ecrivains de
`Blaze.c`. La valeur n'a jamais depasse 255, et le playtest le confirme — chiffres identiques.

### Pourquoi ne pas simplement declarer le champ en `byte`

C'etait la correction de fond evidente, et elle serait fausse. **Le binaire lit ce global a trois
largeurs differentes selon le site** :

| Forme | Sites reconstruits |
|---|---|
| octet (`mov cl,[…]`) | `FUN_00472770` |
| double mot masque (`… & 0xff`) | `Engine/Input.cs`, `Engine/Resources.cs`, `Screens/State02/Destination.cs` |
| double mot nu | deux sites d'`Engine/UiWindow.cs` |

Unifier derriere un champ `byte` rendrait les trois formes indistinguables et effacerait
l'information. C'est **le binaire qui varie**, pas la reconstruction : chaque site se transcrit tel
qu'il est ecrit, et le commentaire du symbole dit maintenant pourquoi les trois formes cohabitent.

**La regle** : quand le meme global se lit a deux largeurs dans le depot, ce n'est pas
necessairement une incoherence a resorber — verifier d'abord si le binaire fait la meme chose. Ici
oui. Dans la meme fonction, en revanche, deux largeurs pour un meme global sont toujours suspectes.

### Verification

`dotnet build` : 0 erreur. Parcours de l'auberge : **1441 images, aucun plantage, 557 primitives
par image**, inchange — ce qui est le resultat attendu pour un defaut latent.

---

## 2026-08-24 (suite 9) — `FUN_004727d0` verifiee contre le binaire, et trois lectures d'octet

`FUN_004727d0` etait deja transcrite (`EntityAnimTick_004727D0`, avec sa suite extraite
`EntityAnimCommon`). Comme pour `FUN_00472770`, le travail restant etait la **verification** — et
elle se justifiait : c'est le corps sur lequel deux pieges avaient ete payes le 2026-08-24, dont un
test inverse.

### Ce qui a ete verifie ligne a ligne

| Bloc | Verdict |
|---|---|
| bloc d'age 0 (+0x9a, +0x30, +0x44, +0x4c, +0x48, l'aiguillage de +0x16) | conforme |
| boucle des quatre emplacements et sa sortie `LAB_00472E3D` | conforme |
| compte a rebours de +0x9a, avec son `cmp bx,bx` | conforme |
| pas d'age, bascule 200 → 99 et bornage a 0x10 | conforme |
| les deux chemins de contact de l'etat 99 | conforme |
| tete d'`EntityAnimCommon` (LAB_004729b4) et etat 0 | conforme |
| cascade d'etats 1, 5, 10, 0xb, 0xf et leurs cibles | conforme |

Le test inverse corrige le 2026-08-24 est bien dans le bon sens : `cmp cx,di` puis
`jne near 004729B4` — le bloc de contact n'est atteint que dans l'etat 99.

Deux details qui auraient pu se retourner et qui sont justes :

* `or ch,80h` a `0x00472e6a` pose **0x8000**, pas 0x80 — meme famille que le `or ch,40h` de
  `FUN_004508B0`, et transcrit correctement des l'origine ;
* `cmp byte [esi+0Dh],0FFh` est une comparaison d'**octet** a 0xFF, rendue par
  `(sbyte)e[0xd] == -1`.

### Ce que la verification a trouve : trois lectures d'octet non masquees

`DAT_005de538` est lu **sur un octet** a trois sites de ce fichier, tous de la meme forme
`xor eax,eax ; mov al,[5DE538h]` :

| Adresse | Site | Etat avant |
|---|---|---|
| `0x00472a09` | `EntityAnimCommon`, etat 0 | non masque |
| `0x00472eab` | `EntityAnimTail_00472E80`, etat 200 | non masque |
| `0x00472770` | `ActivePartySlot_00472770` | non masque (corrige en suite 8) |

Les trois indexent la table des emplacements au pas de 0x9c. Les trois portent desormais le
`(byte)` que le reste du depot emploie deja a huit autres sites.

**Le defaut reste latent** : tous les ecrivains du global, reconstruits comme dans `Blaze.c`,
ecrivent un octet ou zero. Le playtest le confirme — 1441 images, chiffres identiques.

**La lecon, et elle vaut au-dela de ce global** : quand un correctif de largeur est applique a un
site, balayer immediatement les autres sites du meme global. Le premier a ete trouve en verifiant
`FUN_00472770` ; les deux autres ne se sont montres qu'en verifiant la fonction voisine, et l'un
d'eux venait d'etre ecrit le jour meme, dans cette meme session, en recopiant l'idiome fautif.

### Une observation de test, notee au passage

Un lancement sur les deux de cette verification est mort au demarrage sur
`No free block of 0x280000 bytes below 0x00D80000`. **Le mecanisme de relance a bien tourne** — le
journal porte « relaunching (attempt 2 of 3) » puis « (attempt 3 of 3) » — mais les **trois**
tentatives ont perdu la course a la memoire basse. Le quatrieme lancement, manuel, a reussi.

La formulation de `TODO.md` — « ne peut plus tuer une session » — est donc un cran trop forte : le
budget de trois relances peut s'epuiser. Ce n'est pas une regression, c'est la fenetre de 16 Mo qui
reste la fenetre, comme la section du 2026-08-24 (suite) le disait deja.

---

## 2026-08-24 (suite 10) — `FUN_00468080` verifiee, et la convention qui la rend lisible

`FUN_00468080` (`TryPlaySoundNearEntity`) etait deja transcrite. La verification au desassembleur
la confirme mot pour mot — rayon 2000, gain double par `shl edx,1`, position sonore prise en
`+0x1c`, retour 1 ou 0 dans AX — et livre au passage le **fait qui manquait au dossier**.

### `Ordinal_442` est stdcall, et c'est le pivot

La signature de cette famille repose entierement sur une affirmation que la documentation posait
sans la justifier : « le corps lit `[esp+14h]`, `[esp+18h]` et `[esp+1ch]`, soit les arguments 2, 3
et 4 ». Cette lecture n'est vraie **que si** la somme des carres empilee juste avant a ete depilee
par la callee.

Elle l'est :

```
004680cc  push esi
004680cd  call 00545A10h        ; aucun `add esp,4`
004680d2  xor  ecx,ecx
```

Trois autres sites d'appel d'`Ordinal_442` — deux dans `FUN_00472e80`, un dans `FUN_00468110` — ne
nettoient pas davantage. La convention est **stdcall**.

En cdecl, l'argument pousse resterait sur la pile et les memes offsets designeraient les arguments
**1, 2 et 3**. Un cran d'ecart sur les trois sœurs a la fois.

**La lecon** : le `ret` du corps donne la convention quand on ecrit l'interop ; le **site d'appel**
la donne quand on reconstruit l'appelant, et c'est la moitie qui manquait a
`Documentation/DLL/winsys3d.md`. Une convention non etablie ne se voit pas comme une lacune — elle
se voit comme une signature qui marche, jusqu'a ce qu'elle ne marche plus.

### Les deux sœurs verifiees dans la foulee

`FUN_00468110` (`SpawnEffectNearCamera_00468110`), transcrite le 2026-08-24 depuis le
desassemblage, est confirmee sur ses trois points sensibles :

* rayon **1800** — `cmp eax,708h ; jge` ;
* le `+ 0xC0` porte bien sur **param_2** — `add ecx,0C0h` ou `ecx = [esp+14h]` ;
* l'ordre d'empilement donne `(entite, &entite[0x1c], 0, param_2 + 0xC0, param_3, param_4)`.

Cote interop, `[DllImport]` sans `CallingConvention` vaut `Winapi`, soit `StdCall` sur x86 : la
declaration d'`Ordinal_442` dans `Interop/Winsys3d.cs` etait deja correcte.

### Verification

`dotnet build` : 0 erreur. Aucun changement de comportement — cette passe n'a rien corrige dans le
code, seulement etabli et ecrit ce qui le rendait vrai.

---

## 2026-08-24 (suite 11) — le plantage de `FUN_00534cd0`, et `FUN_0041be60`

### Le plantage signale par Vincent

`Fatal error. 0xC0000005 at TestTriggerVolumes_00534CD0`, en entrant dans la seconde grande piece a
droite de l'auberge.

**Un defaut de largeur, et Ghidra le presentait a l'envers.** La pseudo-C ecrit

```c
uVar5 = (ushort)DAT_005fa560;
if ((short)uVar5 != 0) { ... }
```

ce qui se lit comme un mot de seize bits. Le binaire dit autre chose :

```
00534d0a  movzx ax,byte [5FA560h]
00534d12  movsx edi,ax
```

Le compte des morceaux en attente tient sur **un octet**, et l'octet suivant est deja la premiere
entree de la liste. La transcription lisait un `short` : un compte de 2 suivi d'une entree 5 donnait
**0x0502 = 1282 tours**. Passe la fin de la liste, `pending[slot]` rend des octets quelconques,
l'indice de morceau qui en decoule designe n'importe quoi, et la marche sur les boites dereference
un pointeur invente. C'est la violation d'acces.

**La fonction sœur le disait deja.** `FUN_00534f90` (`ResetPendingChunkRecords`) parcourt la **meme
liste** et etait transcrite correctement, `*pending` sur un octet. Deux fonctions, une liste, deux
largeurs : l'incoherence etait visible dans le depot avant d'etre visible a l'ecran.

### Ce qui n'est pas etabli

**Le plantage n'a pas ete reproduit.** Les frappes minutees n'atteignent pas une piece choisie : le
meme trajet donne 1051 primitives par image une fois et 698 la fois suivante. Deux executions avec
la ligne fautive **remise en place** n'ont pas plante non plus — ce qui ne disculpe rien, cela dit
seulement que ces trajets-la n'ont pas rempli la liste des morceaux en attente.

La correction est donc etablie **par le binaire et par la fonction sœur**, pas par l'observation.
C'est suffisant pour la faire — la lecture d'un `short` la ou le binaire lit un octet est fausse
dans tous les cas — mais pas pour affirmer que le symptome de Vincent a disparu.

### Un garde-fou, faute de reproduction

La marche sur les boites n'a **aucune borne** dans l'original : elle fait confiance a la sentinelle
`0xffffffff`. Une liste atteinte par un mauvais indice n'en a pas, et le parcours part alors dans de
la memoire quelconque — c'est-a-dire un `0xC0000005` sans la moindre indication de sa cause, ce que
Vincent a recu.

Une borne de 0x400 boites transforme ce plantage muet en un message qui nomme le morceau, le
creneau et le compte. Elle ne change rien a un parcours normal (les listes font quelques dizaines
d'entrees). Meme raison d'etre que la borne de balayage de `RunTextEngine` et que `StagingBank` :
une deviation assumee, qui rend visible un defaut de reconstruction au lieu de le laisser corrompre
la memoire en silence.

### `FUN_0041be60`, la troisieme sœur

Verifiee contre le binaire, conforme : rayon **1500** (`cmp eax,5DCh`), trois valeurs comparees a
999 puis publiees dans `0x005de0a8` / `ac` / `b0`, retour 1 ou 0 dans AX.

Une difference de forme avec `FUN_00468080` merite d'etre notee : ici les trois arguments sont lus
en **double mot** (`mov eax,[esp+10h]`), la ou la sœur sonore les lit en `movsx word`. Les deux
fonctions se ressemblent trait pour trait et ne partagent pas les largeurs de leurs arguments.

Et le decompte d'offsets ne tient, pour les trois, que parce qu'`Ordinal_442` est **stdcall** —
etabli en suite 10. Ici le corps ne fait que **deux** `push` avant l'appel au lieu de trois, d'ou
`[esp+10h]` pour l'argument 2 au lieu de `[esp+14h]`.

Les trois sœurs sont maintenant verifiees : rayons 1500, 1800 et 2000.

---

## 2026-08-24 (suite 12) — `FUN_0047aa70` : la branche basse de son repartiteur

`FUN_0047aa70` (`TickEntityTable_005ff400`) etait transcrite, mais **la moitie de son repartiteur
etait un mur** : tout sous-type `< 0xC0` levait une exception au lieu d'appeler quoi que ce soit.
Cette passe est maintenant cartographiee, et un cinquieme de ses creneaux se comporte correctement.

### Trois faits releves dans le binaire

**Un — la table.** `PTR_LAB_00551c30` compte **192 entrees** et **124 cibles distinctes**, toutes
absentes de `Blaze.c`. Relevee verbatim et materialisee ; le detail est dans `DataSymbols.md`.

**Deux — le creneau « inconnu » agit.** Soixante-neuf des 192 creneaux pointent sur `0x0047ADC0`,
trois instructions qui **eteignent l'entite** (`*enregistrement = 0`), et non un `ret` nu. La passe
relit le mot d'etat juste apres et rend le creneau au pool. Transcrire ces trois instructions fait
passer **69 creneaux sur 192** d'un plantage a un comportement juste.

**Trois — la convention etait documentee a l'envers.** Le message d'exception affirmait « le
gestionnaire ne prend AUCUN argument (l'appel est `(*(code *)…)()`) ». Faux : le `push esi` est
fait **avant** le branchement, et le `add esp,4` qui suit nettoie les deux branches. La table basse
a exactement la convention de la haute — `__cdecl`, un argument, l'enregistrement.

C'est le **troisieme** argument perdu par Ghidra rencontre dans la journee, apres `FUN_00472e80`
(emplacements d'arguments recycles en locales) et la famille `FUN_00468080` (offsets decales par
une convention stdcall non etablie). Le motif est stable : quand Ghidra montre un appel indirect
sans argument, verifier le `push` avant le branchement.

**Et celui-ci etait pire que les deux autres** : il ne s'est pas manifeste par un plantage mais par
une phrase juste-a-cote-de-vraie dans un message d'erreur, ecrite pour guider quiconque
transcrirait ces 123 corps. Une lacune qui donne de mauvaises instructions coute plus qu'une lacune
muette.

### Ce qui reste, chiffre

**123 corps reels** a relever au desassembleur pour cette table seule. Les deux passes sœurs —
`FUN_0047ab70` vers `0x00551f30`, `FUN_0047acf0` vers `0x00552230` — ont la meme forme et ne sont
pas relevees ; leur message d'exception dit maintenant comment le faire, et ne ment plus sur la
convention.

### Verification

`dotnet build` : 0 erreur. Lancement sans deplacement : 1441 images, aucun plantage, 557 primitives
par image, inchange. La branche basse n'est pas exercee a l'auberge — comme le reste de cette
famille, elle attend une autre zone.

---

## 2026-08-24 (suite 13) — `FUN_0047ab70` : la deuxieme table statique

Meme travail que la suite 12, sur la passe sœur. `PTR_LAB_00551f30` est relevee et materialisee ;
**109 de ses 192 creneaux** deviennent corrects d'un coup, contre 69 pour la premiere table.

### Trois faits, dont deux nouveaux

**La borne des tables se lit dans leur contiguite.** `0x00551f30` commence exactement ou
`0x00551c30` s'acheve, et `0x00552230` exactement ou celle-ci s'acheve : trois blocs de 0x300
octets qui se suivent. Chacune borne la precedente, ce qui evite d'avoir a deviner leur taille.

**Les deux tables sont disjointes.** Une seule cible commune — la souche `0x0047ADC0` — et **pas un
seul gestionnaire reel** partage. Ce ne sont pas deux vues d'un meme jeu de comportements : ce sont
deux familles d'entites distinctes, ce que la passe confirme (celle-ci raccroche ses entites a la
grille spatiale, `FUN_0047aa70` est la seule des six a ne rien y raccrocher).

**La table est beaucoup plus creuse.** 109 creneaux sur 192 eteignent ce qui s'y presente, et les
**soixante premiers le sont d'un bloc** : cette famille numerote vraisemblablement ses sous-types a
partir de 0x3c.

### Un dividende de verification

Le prologue biaise son pointeur — `esi = 0x005edee0 + 0x36`, l'enregistrement etant `esi - 0x36` —
d'ou `and byte [esi],7Fh` pour `record[0x36] &= 0x7f`. La transcription l'avait juste. C'est la
meme figure que la quatrieme boucle de `FUN_0040e640` autour de `&DAT_005e93f9`, relevee en suite 4 :
**une boucle ecrite autour d'un point milieu ne nomme pas sa base**, et il faut la retrouver par le
pas ou par le biais.

### L'etat des trois tables

| Table | Passe | Creneaux vides | Corps reels | Etat |
|---|---|---|---|---|
| `0x00551c30` | `FUN_0047aa70` | 69 / 192 | 123 | relevee (suite 12) |
| `0x00551f30` | `FUN_0047ab70` | 109 / 192 | 83 | **relevee** |
| `0x00552230` | `FUN_0047acf0` | ? | ? | non relevee |

Deux cent six corps reels restent a relever pour les deux premieres. Le message d'exception de la
troisieme dit comment la relever et ne ment plus sur la convention.

### Verification

`dotnet build` : 0 erreur. Lancement sans deplacement : 1441 images, aucun plantage, 557 primitives
par image, inchange. Les branches basses ne sont pas exercees a l'auberge.

---

## 2026-08-24 (suite 14) — `FUN_0047acf0`, et une affirmation a corriger

La troisieme et derniere table statique est relevee. Les trois passes de `0x0047axxx` ont
maintenant leur branche basse cablee, et **303 des 576 creneaux** — plus de la moitie — se
comportent correctement pour trois instructions.

### Le bilan

| Table | Passe | Creneaux morts | Corps reels | dans `Blaze.c` | au desassembleur |
|---|---|---|---|---|---|
| `0x00551c30` | `FUN_0047aa70` | 69 / 192 | 123 | 63 | 60 |
| `0x00551f30` | `FUN_0047ab70` | 109 / 192 | 83 | 26 | 57 |
| `0x00552230` | `FUN_0047acf0` | 125 / 192 | 67 | 32 | 35 |
| **total** | | **303 / 576** | **273** | **121** | **152** |

Les trois sont disjointes deux a deux : leur seule cible commune est la souche.

### La correction : `PTR_LAB_` ne veut pas dire « absent de `Blaze.c` »

**Ce que j'ai ecrit en suite 12 et 13 etait faux.** « Aucune de ses cibles n'est dans `Blaze.c` »,
« tous absents de `Blaze.c` » : c'etait une generalisation d'un **echantillon de sept adresses**
verifiees a la main, etendue a 273.

Le compte exact dit autre chose : **121 des 273 corps sont decompiles**. Le prefixe `PTR_LAB_` dit
seulement que Ghidra n'a pas reconnu les cibles comme des fonctions **depuis cette table** ; la
plupart le sont depuis d'autres appelants.

Cela change la nature du travail restant. Ce n'etait pas « 273 corps au desassembleur », c'est
**121 transcriptions ordinaires et 152 desassemblages**. Le premier tiers est de la route pavee.

**La lecon**, et c'est la meme que celle du carrousel de selection : sept echantillons concordants
ne valent pas une mesure. Le compte tenait en deux commandes, et je ne les ai lancees qu'a la
troisieme table — apres avoir ecrit deux fois l'affirmation dans le code et dans la documentation.
Quand une phrase porte un quantificateur universel (« aucune », « tous »), la mesurer avant de
l'ecrire, pas apres.

### La seule des trois qui soit atteinte

`0x00552230` est la seule dont un creneau soit atteint par un chemin deja reconstruit :
`SpawnStatusEffectEntity_0047AC40` pose son sous-type en `+0x05`, l'octet que cette passe aiguille,
et l'effet de dispersion du groupe d'`EvaluatePartyDispersal` y entre avec `0x9d` — soit
`0x00445860`, un corps decompile et non transcrit. C'est le premier des 121 a faire, puisqu'un
chemin vivant y mene.

### Verification

`dotnet build` : 0 erreur. Lancement sans deplacement : 1441 images, aucun plantage, 557 primitives
par image, inchange.

## 2026-08-25 — le plantage de l'auberge : `FUN_0047aa70` n'avait qu'un seul creneau atteignable

Vincent signale, en passant a l'ecran suivant dans l'auberge :

```
Unhandled exception. System.NotImplementedException: FUN_0047aa70
```

La ligne est tronquee, et la lecon du 2026-08-24 disait de relever l'exception entiere. Elle n'a
pas ete necessaire : **la table 0x005ff400 n'a qu'un seul sous-type possible**, et deux `grep` le
demontrent sans avoir a rejouer le trajet.

### Comment le creneau se deduit au lieu de se mesurer

`FUN_0047aa70` aiguille sur l'octet `+0x05` de l'enregistrement. Dans tout `Blaze.c`,
**`&DAT_005ff405` n'a qu'un seul ecrivain** — `FUN_0047a9d0`, l'engendreur de cette table, qui le
pose depuis son `param_2`. Et ses **six** sites d'appel passent tous la meme valeur : `0x59`.

`0x59 = 89 < 0xC0`, donc la branche basse, donc l'entree 89 de `PTR_LAB_00551c30`. Relevee dans le
binaire (`at 0x00551d88 32`, decalage `+0x0c`) : **`0x00403710`**.

Le sous-type ne pouvant pas etre autre chose, la ligne complete n'apportait rien. C'est le contraire
du cas du 2026-08-24, ou la table 0x0056adc8 avait 64 sous-types possibles et ou seule l'exception
pouvait dire lequel : **le nombre d'ecrivains du champ d'aiguillage decide si un symptome doit etre
reproduit ou peut etre deduit.**

### `FUN_00403710` — l'effet de soin

Corps de 480 octets, decompile dans `Blaze.c`, verifie ligne a ligne au desassembleur. C'est une
entite satellite accrochee a son porteur (`+0x6c`), dont elle recopie la position a chaque image :

* `+0x16` est le compteur de scenette — bruitage `0x30` a l'image 0, `0x2f` a l'image 1, extinction
  a 40 (le bit `0x80000000` de l'entite tombe, et le bit `8` du porteur avec lui) ;
* `+0x1a` est le **pourcentage** soigne. A 100, et seulement a 100, il pose le bit `8` du porteur et
  lance une gerbe d'entites `0x59` en spirale (angle `+0x12`, pas de `rand % 400 + 800`, rayon
  `60 * sin`) ;
* le soin lui-meme n'a lieu qu'a l'image 1 : `+0x14c` et `+0x14e` du bloc de personnage recoivent
  le pourcentage de `+0x148` et `+0x14a`, bornes par eux.

**Ce bornage tranche le sens de la paire** : `+0x148`/`+0x14a` sont le maximum, `+0x14c`/`+0x14e`
le courant. C'est la meme lecture que `Destination.cs` (retour en ville), et elle est maintenant
tenue par deux temoins independants.

Une bizarrerie a signaler sans la resoudre : `ResetPerCharacterFrameState` (`FUN_0053dcb0`) recopie
`+0x148` dans `+0x14c` **a chaque image**, ce qui, avec la lecture ci-dessus, remettrait les points
de vie au maximum en permanence. Les deux transcriptions sont fideles au binaire — les deux
desassemblages sont dans ce depot. **Il reste donc une piece manquante**, pas une erreur de
transcription. TODO(REVERSE) : trouver ce qui distingue les deux sites.

### Le piege de Ghidra sur la division par 100

`Blaze.c` ecrit la division comme une somme de trois termes :

```c
(short)(iVar6 / 100) + (short)(iVar6 >> 0x1f) - (short)((longlong)iVar6 * 0x51eb851f >> 0x3f)
```

Les deux derniers termes valent tous deux `-1` pour un operande negatif et `0` sinon : ils
s'annulent. Le binaire porte l'idiome habituel (`imul 0x51EB851F ; sar edx,5 ; shr eax,31 ; add`),
une division signee tronquee vers zero. C'est `/ 100` en C#, sans plus.

### `FUN_00445860` — le premier des 121, transcrit

L'item designe par le TODO de la veille. Corps de 180 octets, entree `0x9d` de `PTR_LAB_00552230`.
C'est un **afficheur d'enonce** accroche a son porteur : il pose `&DAT_00551640` en `+0x3c`, avance
l'image d'animation du porteur en la bouclant sur le nombre d'images de sa planche
(`**(&DAT_005e9680)[porteur[+4]]`, champ `+0x16`), remet les trois octets de teinte a `0xff`, et
fait pulser son echelle `+0x28`/`+0x2a` a `0x1000 + sin(angle)/4`.

Deux points que seul le binaire tranche :

* **le porteur nul retombe sur `&DAT_005de558`**, l'entite factice — `test eax,eax ; jne ; mov
  eax,5DE558h`. Ce n'est pas une garde defensive ajoutee par la transcription : tout le corps
  travaille ensuite sur ce substitut, avance d'animation comprise ;
* `Ordinal_451` est appele **deux fois** avec le meme angle, pour `+0x28` puis `+0x2a`. Table de
  sinus pure, donc deux fois la meme valeur ; les deux appels sont conserves.

### Ce que `FUN_00445860` a mis au jour : `0x00551640` n'etait pas semee

`param_1[0xf] = &DAT_00551640` pose un **pointeur de donnee**, pas un drapeau. Or
`s_EntityParams_00551640` etait une `AllocZeroed(0x200)` : l'entite recevait un pointeur vers 512
octets nuls.

La plage est sous la frontiere `0x00571400`, donc **reellement presente dans le fichier**, et ses
0x200 octets y sont initialises. Les onze initialiseurs (`Init_0045a220` et sa famille) reecrivent
les champs numeriques avec exactement les memes valeurs — `Init_0045a220` pose `0x9c4` en
`0x00551660`, et le binaire y porte deja `c4 09 00 00` — mais ils ne touchent pas aux **trois flux
d'etiquettes** noyes dans la plage :

| Adresse | Flux | Sens |
|---|---|---|
| `0x00551648` | `1f d0 20 · 08 00 · 0c · 開かない！？ · 0f · 08 01 · 00` | « ça ne s'ouvre pas ! » |
| `0x00551720` | `08 00 · 0c · 遠くで音がした。 · 0f · 08 01 · 00` | « un bruit, au loin » |
| `0x00551814` | la note de developpeur, relevee le 2026-08-24 | « contactez Imai » |

Les 0x200 octets sont maintenant semes verbatim depuis le binaire (`BuildEntityParams_00551640`,
`Memory/GameMemory.cs`). Les huit premiers octets (`f0 80 0f 0f 01 00 11 00`) precedent le flux de
`0x00551648` et ne sont ecrits par personne — TODO(REVERSE) : leur role n'est pas etabli. Un
en-tete de boite, vraisemblablement, puisque le flux du developpeur en `0x00551814` n'en a pas et
se lit directement.

**La lecon generale** : une plage sous `0x00571400` modelisee par `AllocZeroed` est une hypothese,
pas un fait. Elle ne tient que si le binaire y porte reellement des zeros — ce que `at` dit en une
commande. Deux des huit blocs adresses par decalage portent la mention « verifiee nulle » ; les
autres ne l'ont jamais ete.

### Verification

`dotnet build` : 0 erreur. `dotnet test` : 4 vertes, 2 rouges — les deux rouges connues (test 3 et
test 5), inchangees. Lancement sans deplacement jusqu'a l'auberge : aucun plantage, 557 primitives
par image, identique avant et apres.

**Le plantage lui-meme n'est pas exerce** : il demande de conduire le personnage jusqu'a l'ecran
suivant, ce qui est a Vincent depuis le 2026-08-24.

## 2026-08-25 (suite) — la salle du barman, et la deduction qui etait a moitie fausse

La ligne d'exception entiere, cette fois :

```
FUN_0047aa70, table 0x005ff400 entree 0 [00 08 90 80 00 e1 00 00 00 00 00 00 00 00 00 00]
 : gestionnaire d'entite 0x4055C0 non transcrit
```

### La correction

J'ai ecrit ce matin que `0x00403710` etait **le seul** creneau que cette passe puisse atteindre.
C'est faux, et l'octet `+0x05` de l'enregistrement le dit : il vaut **`0xe1`**, pas `0x59`.

Ce qui etait juste : `FUN_0047a9d0` est bien l'unique ecrivain de `&DAT_005ff405`, et ses six sites
d'appel passent bien tous `0x59`. Ce qui etait faux, c'est d'en conclure quoi que ce soit sur
**l'ensemble** des sous-types possibles. `FUN_0047a9d0` n'est pas le seul chemin vers cette table :
**six autres sites de `Blaze.c` ecrivent directement dans `DAT_005ff400`** (lignes 6165, 30034,
30297, 45382, 81493, 81556), et le chargement de zone y pose des enregistrements complets. Le
symbole `DAT_005ff405` ne compte que les ecritures **nommees par ce symbole-la**.

`0xe1 >= 0xC0`, donc la branche **haute** : `0x0056adc8 + 0xe1 * 4 = 0x0056b14c`, soit l'entree 33
de `s_Vt_0056b0c8`, la table installee par zone. Cible : `0x004055C0`.

**La lecon**, et c'est la troisieme fois qu'elle se paie dans ce depot : compter les ecrivains d'un
**symbole** ne compte pas les ecrivains d'une **adresse**. Un `grep` sur `DAT_005ff405` ne voit pas
un `memcpy` vers `DAT_005ff400`, ni un enregistrement pose en bloc par le chargeur de zone. Le
raisonnement etait bon pour ce qu'il couvrait — la branche basse — et l'erreur a ete de ne pas
borner sa portee en l'ecrivant.

`RestoreVitalsEntity_00403710` reste juste et reste utile : c'est bien la cible de la branche basse.
Sa documentation borne maintenant ce qu'elle affirme.

### `FUN_004055C0` — le second balayeur d'entites de scene

Jumeau de `FUN_00404ef0`, deja transcrit : meme forme, meme table `0x005ecaa0`, meme
auto-desarmement en sortant. La difference tient aux types reveilles — **3, 4, 5, 7 et 6**, dans cet
ordre — la ou son jumeau reveille 0, 1 et 2.

Il y a d'ailleurs un **troisieme** balayeur, `FUN_00405a70`, qui reveille 0, 1, 2, 3, 4 avec encore
d'autres corps. Non transcrit : rien ne demontre qu'il soit atteint. Son type 0 est
`FUN_00405680`, deja fait ici.

Les cinq reveils partagent la meme charpente que les trois deja transcrits — `ArmSceneEntityState`
puis cinq ou six tests sur les trente bits bas de `+0x5c` — et ne different que par leurs numeros
d'animation :

| Type | Corps | Etat 1 | Etat 2 (inactivite) | Etat 3 | Etats 4-6 |
|---|---|---|---|---|---|
| 3 | `0x00405680` | avance 1 **+ decale `+0x32`** | cycle 2..4, pause `rand%10` | decompte 1 **+ decale `+0x32`** | 2, 3, 4 |
| 4 | `0x00405770` | avance 1 | cycle 2..4, pause `rand%10` | decompte 1 | 2, 3, 4 |
| 5 | `0x00405840` | avance 1 | cycle 1..2, pause `rand%10` | decompte 2 | 1, 2 |
| 6 | `0x004058f0` | avance 4 | cycle 2..3, pause `rand%10 + 10` | decompte 4 | 0, 5 |
| 7 | `0x004059a0` | avance 1 | cycle 3..4, pause `rand%10` | decompte 1 | 3, 4, 2 |

Deux choses meritent d'etre notees :

* **le type 3 est le seul des huit qui deplace son entite.** Aux etats 1 et 3, si le pas d'animation
  vient d'atteindre sa fin (`+0x48 == 1`) et que le bit « l'etat vient de changer » est retombe, il
  decale `+0x32` de `-0x22` puis de `+0x22`. Aller-retour de 34 unites sur ce seul champ. Les deux
  additions sont **16 bits** dans le binaire (`add word [esi+32h],0FFDEh`), ce qui se verifie au
  desassembleur et pas dans `Blaze.c` ;
* **le type 4 est exactement le type 3 sans ces deux decalages.** Memes six etats, memes numeros
  d'animation. Ce qui distingue les deux types dans le jeu est donc le deplacement, et rien
  d'autre — un objet de decor qui bouge et le meme qui ne bouge pas.

Les cinq tests du balayeur **relisent** le mot d'etat et l'octet de type a chaque fois : un reveil
peut changer l'un comme l'autre, et le binaire recharge `[esi]` avant chaque comparaison. Transcrit
tel quel.

### Verification

`dotnet build` : 0 erreur. Le trajet lui-meme est a Vincent : la salle du barman ne s'atteint pas en
frappes minutees.

## 2026-08-25 (suite 2) — la branche courte de `FUN_00516fc0`, et l'entree du menu de jeu

La touche de menu (F2) plantait sur le dernier trou de la boucle par image :

```
FUN_00516fc0 : la branche courte d'affichage (0x005179da a 0x00517a77) ... Il lui manque
FUN_0051aa30 (594 lignes) et FUN_0052a7d0 (20 lignes).
```

### La branche elle-meme — transcrite

Vingt lignes de C#, verifiees au desassembleur. Quand `DAT_005de02c` porte le bit 4, le monde n'est
**pas dessine du tout** : un seul appel remplace les ~700 lignes du bloc d'affichage principal, la
fenetre de rendu se retrecit de 320x240 a **256x224** le temps du menu, et la queue de boucle la
remet a 320x240 juste apres.

Un piege releve au passage : la chaine qui trouve l'emplacement de manette n'est **pas** la meme
ici et au site de `PTR_FUN_0056aff0`, vingt lignes plus haut.

| Site | Chaine |
|---|---|
| `PTR_FUN_0056aff0` (0x0051762b) | `DAT_005e9188[DAT_005de540 * 0x4c]` |
| branche courte (0x005179de) | `DAT_005e9188[DAT_005e93c8[DAT_005de540 * 0x9c] * 0x4c]` |

Une indirection de plus ici : l'emplacement se lit a travers l'index de personnage porte par la
fiche (`+8`), pas directement. Les deux sont transcrites telles qu'elles sont ecrites.

`FUN_0051aa30` (598 l.) reste une lacune nommee. Elle n'est atteinte que par le mode de mise au
point (`DAT_005633a8 == 0` **et** bit 1 de `DAT_005de02c`), donc hors du chemin du joueur.

### `FUN_0052a7d0` — l'enveloppe, transcrite

Elle arme la fenetre de decoupe de l'interface (`DAT_005de154`/`_158`/`_15c`, ceux que
`UiWindow.EmitClipRect` relit), appelle le menu, la desarme. Le calcul est mot pour mot celui de
`Screens/State01/PartySetup.cs`.

**Ghidra la declare `void` et c'est faux.** Le site d'appel range son `eax` dans l'emplacement du
curseur de primitives, et rien apres l'appel a `FUN_0052a850` ne touche `eax` — elle rend donc le
curseur. Verifie a 0x0052a841.

### `FUN_0052a850` — la charpente transcrite, les six pages nommees

2533 lignes dans `Blaze.c`. Ce qui est fait : le prologue (0x0052a850..0x0052aa1d), verifie ligne a
ligne au desassembleur.

* changement de page sur les deux gachettes (`[pad+3] & 4` et `& 8`) ;
* **deux pages se sautent selon l'etat de la partie** — la 1 tant que `DAT_005e9840` est nul, la 2
  tant que `DAT_005e9001` ne porte pas le bit `0x10` — et les deux sauts s'enchainent, donc un recul
  depuis la page 3 peut atterrir directement sur la 0 ;
* remise a neuf de l'etat de page et recadrage de la carte sur le meneur, au changement ;
* fermeture sur `[pad+2] & 8` ;
* **deux verrous « operation en cours »** qui court-circuitent toute la navigation, fermeture
  comprise : la page 5 tant que `DAT_005de710` n'est pas negatif, la page 0 tant que
  `DAT_005de70c` ne l'est pas.

**Deux pieges que `Blaze.c` ne montre pas :**

* `DAT_005e9840` est compare en **dword** (`cmp [5E9840h],edi` a 0x0052a8bb). `DAT_005e9001`, lui,
  est bien un octet. La pseudo-C ecrit les deux de la meme facon ;
* Ghidra reutilise `iVar29` pour la page precedente avant le switch **et pour tout autre chose**
  dans la page 0. Le binaire recharge `esi` depuis la pile en 0x0052aa0b, ou il vaut `param_1`. Ne
  pas faire circuler la variable de Ghidra a travers le switch.

### La carte des six pages, pour la suite

| Page | `Blaze.c` | Taille | Ce qu'on en sait |
|---|---|---|---|
| 0 | 75336..76574 | **1238 l.** | la fiche de personnage. Elle-meme un switch de **six onglets** |
| 1 | 76574..76672 | 98 l. | sautee quand `DAT_005e9840` est nul |
| 2 | 76672..76697 | 25 l. | sautee sans le bit `0x10` de `DAT_005e9001` |
| 3 | 76697..76833 | 136 l. | |
| 4 | 76833..77060 | 227 l. | |
| 5 | 77060..77698 | 638 l. | la sauvegarde — elle appelle les quatre corps de carte memoire `FUN_00504xxx` |

Le decoupage de la page 0, releve au meme titre :

| Onglet | `Blaze.c` | Taille |
|---|---|---|
| 0 | 75488..75674 | 187 l. |
| 1 | 75675..75887 | 213 l. |
| 2 | 75888..76123 | 236 l. |
| 3 | 76124..76340 | 217 l. |
| 4 | 76341..76445 | 105 l. |
| 5 | 76446..76571 | 126 l. |

L'onglet affiche n'est pas l'indice de position : `DAT_0056a898` porte **huit lignes de six octets**,
une par classe (`DAT_005e9150 & 7`), dont le premier octet est le nombre d'onglets et les suivants
leurs identifiants. Relevee dans le binaire :

```
classe 0  04 | 00 01 02 05      classe 4  05 | 00 01 02 04 05
classe 1  05 | 00 01 02 03 05   classe 5  05 | 00 01 02 03 05
classe 2  04 | 00 01 02 05      classe 6  04 | 00 01 02 05
classe 3  05 | 00 01 02 03 05   classe 7  05 | 00 01 02 03 05
```

Toutes commencent par l'onglet 0, donc **le menu s'ouvre toujours sur l'onglet 0 de la page 0** —
`DAT_005de6f0` et `DAT_005de704` valent tous deux zero au chargement, le premier releve dans la
queue non initialisee de `.data`.

`DAT_0056a8c8` (`00 01 46 02 03 47`) traduit l'identifiant d'onglet en indice de pictogramme.

### Ce qui manque encore au dépôt pour finir le menu

**Quatre appelees seulement**, toutes courtes : `FUN_0052fab0` (39 l.), `FUN_0052fba0` (79 l.),
`FUN_0052fdc0` (134 l.), `FUN_0051bd50` (35 l.). Les **trente-neuf** autres sont deja transcrites —
tout le moteur de texte, les fenetres, les sprites, les cinq passes de statistiques
(`FUN_00538ab0`..`FUN_00538fa0`) et les quatre corps de carte memoire. Le menu n'est donc pas un
sous-systeme a batir, c'est un corps a transcrire au-dessus d'un socle qui existe.

Un bloc de donnees reste a modeliser : **`DAT_005de5f8`**, le descripteur de sprite de brouillon que
toute la page 0 remplit avant chaque `FUN_00521df0` / `FUN_0051f9c0`. Cinq dwords a 0x005de5f8 —
atlas, couleur, position (deux `short` a `+8`), indice de tuile a `+0x10`. Il ne recouvre pas
`s_EntityList_005de558`, qui s'arrete a 0x005de5f4 : quatre octets d'ecart, verifies.

### Verification

`dotnet build` : 0 erreur. En jeu, **par l'agent** (voir la nouvelle section de
`test-protocol.md`) : la branche courte, `FUN_0052a7d0` et le prologue s'executent, et l'exception
nomme desormais `FUN_0052a850, page 0` avec sa plage de lignes. Le chemin entier est donc exerce.

## 2026-08-25 (suite 3) — la fiche de personnage s'affiche

La page 0 du menu et son onglet 0 sont transcrits, plus l'epilogue partage que les six pages se
donnent. **L'ecran s'ouvre, se dessine, change d'onglet et se referme** — capture a l'appui :
portrait, nom, classe, element, niveau, or, PV/PM, les sept caracteristiques, les quatre
resistances, l'experience et le reste a gagner.

### L'epilogue partage, qui manquait a la carte

Le decoupage releve hier disait « six pages » et s'arretait la. Il manquait la piece qui les rend
visibles : **0x0052f82c..0x0052f9c2**, ou les six pages retombent toutes. C'est lui qui appelle
`FUN_00545340` (le dessin de panneau) et qui pose le curseur d'ecran.

Le nombre de panneaux dessines se lit dans un idiome de booleen etendu que Ghidra rend en
arithmetique :

```
uVar23 = -(uint)(page != 4) & (-(uint)(page != 0) & 0xfffffffe) + 3
```

soit, developpe : **page 4 → un panneau, page 0 → quatre, les autres → deux**. Ils partent du
dernier et redescendent, chacun s'empilant devant le precedent.

Et les deux derniers appels prennent **`param_2`** (otEntry) et non `param_1` — seul endroit du
corps ou la difference se voie. Le binaire le dit en une instruction : `mov esi,[esp+20h]` en
0x0052f915, juste avant.

### Le troisieme piege de Ghidra dans cette fonction

```c
if ((DAT_005de704 == 1) || (DAT_005696e3 = 0, DAT_005de704 == 2)) { DAT_005696e3 = 1; }
```

**Ce n'est pas un `if/else`.** L'operateur virgule cache que la remise a zero n'a lieu que sur le
chemin `tab != 1` :

```
0052f831  cmp eax,1 ; je 0052F842
0052f836  cmp eax,2 ; mov byte [5696E3h],0 ; jne 0052F849
0052f842  mov byte [5696E3h],1
```

Transcrit en deux `if` successifs, ce qui reproduit exactement les deux chemins. C'est le
**troisieme** piege de Ghidra dans ce seul corps, apres le `iVar29` reutilise et le `cmp dword` lu
comme un octet.

### La rangee de descripteurs 0x00568530 — quatre, et le compte se deduit

Quatre descripteurs d'interface contigus au pas `0x8c8`, comme la rangee de `0x00560ff0`. Leurs
**0x68 premiers octets** sont initialises dans le binaire, la queue est nulle — verifie aux quatre
adresses.

Le compte ne se devine pas : `FUN_0052fab0` indexe depuis `0x00568530` au pas `0x8c8`, le plus grand
indice rencontre est 3, et la premiere donnee qui n'est pas un descripteur est `DAT_0056a898` —
juste au-dela de `0x00568530 + 4 * 0x8c8 = 0x0056a850`. Les deux bornes se rejoignent.

| Indice | VA | Role observe |
|---|---|---|
| 0 | `0x00568530` | le panneau racine, dessine en dernier ; l'epilogue deplace sa boite selon la page |
| 1 | `0x00568df8` | la boite de titre, deplacee elle aussi |
| 2 | `0x005696c0` | le pave de statistiques — l'ancre des vingt decorations de l'onglet 0 |
| 3 | `0x00569f88` | la fiche de personnage, que la page 0 remplit quatre fois |

### Ce que la page 0 apprend

* **Les cinq passes de statistiques sont faites deux fois par image** — une fois dans la tete de
  page, une fois dans l'onglet 0. C'est dans le binaire aux deux endroits, pas une maladresse de
  transcription. La fiche affiche donc un total **recalcule**, jamais un total range.
* **Le bloc `+0x100` du personnage est un flux d'etiquettes**, pas une structure de nombres : il
  part tel quel dans `FUN_00544350` puis le moteur de texte. C'est ainsi que les statistiques
  arrivent a l'ecran.
* **`DAT_005de708` est signe** (`test eax,eax ; jge` en 0x0052aa68), et c'est ce qui fait boucler la
  selection de membre sur le dernier. Le depot le declarait `uint` — corrige.
* **L'onglet affiché n'est pas la position du curseur.** `DAT_0056a898` porte huit lignes de six
  octets, une par classe : le nombre d'onglets puis leurs identifiants. La classe du guerrier en a
  quatre (0, 1, 2, 5) ; d'autres en ont cinq. Verifie en jeu — une pression a droite depuis
  l'onglet 0 mene bien a l'onglet 1 et non a une position vide.
* **La teinte des jauges** vient d'un rapport sur 255 : sous la moitie le rouge est plein et le vert
  suit, au-dessus l'inverse. Les deux assemblages que Ghidra rend en `CONCAT31` sont
  `(rapport << 9) & 0xffffff00 | 0xff` et `(rapport + 0x7f80) * 0x20000 | 0xffff`.
* **`DAT_005de5f8`** est le bloc de sprite de brouillon — cinq dwords, atlas / couleur / position /
  indice de tuile — que toute la page remplit avant chaque emission. Il ne recouvre pas
  `s_EntityList_005de558`, qui s'arrete a `0x005de5f4` : quatre octets d'ecart, verifies avant de
  l'allouer a part.

### `FUN_0052fab0` — la liste de decorations

Trente-neuf lignes, la premiere des quatre appelees qui manquaient. Un dword par decoration :
indice du descripteur qui sert d'ancre, indice de tuile, puis deux decalages en octets ; la liste
s'arrete au premier dword nul. Son bloc de sprite est **local**, pas `DAT_005de5f8` — meme
disposition, autre emplacement.

Deux listes relevees : `DAT_0056a8e0` (deux decorations, ancrees sur le descripteur 3) et
`DAT_0056a8f0` (vingt, sur le descripteur 2).

### Verification

`dotnet build` : 0 erreur. **En jeu, par l'agent**, trois runs :

| Sequence | Resultat |
|---|---|
| `…,START,WAIT10` | aucun plantage, 509 primitives par image, **la fiche s'affiche** |
| `…,START,WAIT4,RIGHT,WAIT6` | plantage attendu : « page 0, onglet 1 » — le selecteur d'onglets marche |
| `…,START,WAIT5,START,WAIT8` | aucun plantage — le menu se referme et le monde reprend a 1105 primitives par image |

**Ce qui n'est PAS verifie** : la fidelite du rendu contre le jeu d'origine. La capture est
coherente et lisible, mais aucune image de reference ne lui a ete comparee — notamment le fond, qui
est l'image precedente restee dans le tampon (la branche courte ne redessine pas le monde) et qui
apparait teintee. Dire si cette teinte est celle de 1998 demande le jeu original.

## 2026-08-25 (suite 4) — l'inventaire, et les quatre dernieres appelees du menu

L'onglet 1 de la fiche est transcrit, avec ses trois appelees. **Le menu n'a plus aucune dependance
manquante** : ses quarante-trois appelees sont toutes dans le depot, et ce qui reste est du corps a
transcrire.

Verifie a l'ecran : la grille de quatre objets, les marqueurs « equipe », la quantite, le curseur de
case, la fiche de l'objet vise (« Epee normale », ATT 7 / DEF 0), les huit pictogrammes d'element
teintes selon le masque de l'objet, et la description (« Fabriquee en cuir solide. ») quand le
curseur bouge.

### Les trois etats de l'onglet, et le curseur a 99

Deux verrous, pas un :

* **`DAT_005de718` negatif** — la grille n'est pas entree. Un seul libelle s'affiche, rien ne
  repond. C'est l'etat ou l'onglet arrive, parce que l'onglet 0 pose `-1` en sortant ;
* **`DAT_005de70c`** mene une machine a quatre etats une fois la grille entree : `-1` la selection
  libre, puis 0 a 3 pour la confirmation d'utilisation, chacun avec sa question et son annulation
  par Croix.

**Le curseur vaut 99 quand rien n'est vise**, et ce n'est pas une sentinelle inventee : les quatre
directions y reviennent des qu'elles sortent de la liste, et 99 est teste partout comme « aucun
objet ». La grille fait cinq colonnes, d'ou les pas de 1 et de 5.

Depuis 99, les quatre directions **entrent** dans la liste — trois par la fin, une par le debut — et
elles ne s'excluent pas : le binaire les teste l'une apres l'autre, donc une diagonale prend la
derniere.

### Six chemins, un seul appel

`Blaze.c` rend l'onglet avec un `goto LAB_0052ba95` et trois variables recyclees, ce qui donne six
chemins apparents. Les trois derniers arguments sont **identiques partout** — la fiche, le
descripteur racine, zero — et seul le flux d'etiquettes change. Reconstruit en une variable et un
appel, ce qui fait tomber les six chemins a une cascade lisible.

Le point de chute `LAB_0052b82d` est ecrit deux fois par Ghidra, et les deux aboutissent au meme
test : la branche « validation » y saute apres avoir pose `DAT_005de70c = 0`, ce qui ne change pas
le test qui suit.

### `FUN_0051bd50` — le nom coupe a la barre oblique, et son indice 1

Le nom de l'objet est recopie depuis `+0x40` jusqu'au premier `'/'`, puis la suite est recopiee — et
**elle repart de l'indice 1**, pas de 0 ni de la position de la barre. Le binaire est formel :
`mov edx,1` en 0x0051bd7c, sans condition.

Le format attendu est donc `<un caractere>/<le reste>` : le premier caractere survit, la barre
disparait, le reste se colle derriere. Un nom dont la barre serait ailleurs qu'en position 1 serait
mutile. Ce n'est pas un defaut a signaler dans `legacy-bug.md` — c'est une convention de donnees,
et les noms du jeu la respectent (« Epee normale » sort entier).

**Ghidra ne lui recupere qu'un parametre alors que le site en pousse quatre.** Ce n'est pas une
signature ratee : la fonction est `__cdecl`, l'appelant nettoie, et les trois autres sont ignores —
le site les pousse parce qu'il partage sa forme avec les cinq autres chemins de l'onglet, qui
appellent tous `FUN_00544350`.

### `FUN_0052fba0` — la grille

Cinq colonnes de cases de `0x1c`. Ghidra ecrit l'indice de boucle en arithmetique de pointeurs
(`pbVar8 = pbVar9 + iVar7` avec `iVar7 = -0x2c1 - param_4`), ce qui n'est que `i`.

**Le pictogramme se decoupe en ecrivant dans l'atlas** : les deux premiers octets du descripteur
partage recoivent `entree * 0x18` et `personnage * 0x18` avant chaque emission. Le meme couple
repart en `extra`, empaquete sur seize bits.

Son `param_3` est bien la fiche du membre choisi, et non une variable recyclee : verifie au site
d'appel (0x0052bc58, `mov edi,[esp+18h]`, l'emplacement ou la tete de page a range `ebx` en
0x0052aad9).

### `FUN_0052fdc0` — la fiche d'objet

Huit rangs de valeurs, deux rangs de portee, le nom, huit pictogrammes d'element. Un objet non
identifie (`+0x13` sans le bit `0x80`) rend `« ??? »` a chaque rang et `« ???? »` comme nom — les
deux chaines sont a `0x00566b80`, relevees.

**Le nom part dans un flux d'etiquettes monte sur la pile** : `0c 04 <x> <y>` puis le nom recopie
octet par octet par l'idiome `rep movsd` habituel. Les deux decalages se prennent par rapport au
descripteur 2, donc ils suivent la boite si elle bouge. Le descripteur garde ensuite un pointeur
vers un cadre mort et personne ne le relit — c'est ce que fait l'original, avec la meme duree de
vie.

### Deux blocs de donnees releves

* **`0x0056aae0`..`0x0056abe8`** — trente-trois cellules de sprite de huit octets. **Les deux bornes
  sont mesurees** : en dessous commence une table de mots de seize bits, au-dessus une chaine cp1252
  (« Desactiv… »). C'est la lecon du carrousel de selection — un descripteur ne se releve pas seul,
  parce que `DrawSpriteVariant` indexe **en avant** depuis sa base.
* **`0x0056a8d8`** — l'ordre des huit rangs de la fiche d'objet. Il occupe exactement le trou de
  huit octets laisse entre `DAT_0056a8d0` et `DAT_0056a8e0`, ce qui referme la plage.

### Un recouvrement latent, releve et NON corrige

`DAT_005de0c8`, le tampon de nom, finit a `0x005de109` dans le binaire et mord donc de neuf octets
sur `s_ItemMenuArena_005de100`. Ici il s'alloue a part, ce qui diverge de l'original **uniquement si
un nom atteint 63 caracteres** — ce qu'aucun nom du jeu ne fait. Le corriger demande de trancher
l'etendue de l'arene, deja douteuse (meme TODO(REVERSE) dans `Engine/UiWindow.cs`).

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent :

| Sequence | Resultat |
|---|---|
| `…,START,WAIT4,RIGHT,WAIT8` | aucun plantage, 498 primitives par image, **la grille et la fiche d'objet s'affichent** |
| `…,RIGHT,WAIT4,ENTER,WAIT4,RIGHT,WAIT8` | aucun plantage, 499 par image — le curseur entre dans la grille et se deplace, la fiche suit |

Meme reserve qu'hier : **la fidelite du rendu contre le jeu d'origine n'est pas verifiee.**

## 2026-08-25 (suite 5) — l'equipement

L'onglet 2 de la fiche est transcrit. Verifie a l'ecran de bout en bout : les cinq rangees avec
leurs pictogrammes et leurs noms, le choix d'un emplacement, l'ouverture de la grille d'objets, et
**l'echange lui-meme** — valider l'epee deja portee la retire, et la rangee repasse a
« ­ARME­ » avec son marqueur vide.

### Trois etats sur un seul verrou

Contrairement a l'onglet 1, qui en utilise deux, celui-ci tient sur `DAT_005de718` seul :

| Valeur | Etat |
|---|---|
| `-1` | l'onglet n'est pas entre — un libelle, rien ne repond |
| `0` | on choisit un emplacement (haut/bas sur `pad[3]`) |
| `1` | on choisit l'objet a y mettre (la grille de l'onglet 1, empruntee telle quelle) |

Les trois partagent le meme dessin final, ce que Ghidra rend par un `goto LAB_0052c017`.

### Le trou des classes a trois emplacements

**Le nombre d'emplacements depend de la classe** : quatre pour les classes 0 et 6, trois pour les
autres. Ghidra ecrit ce choix avec sa virgule habituelle —
`if ((cls == 0) || (uVar23 = 3, cls == 6)) { uVar23 = 4; }` — qui n'est ici qu'un ternaire, la
valeur n'ayant pas d'effet de bord.

Ce qui compte davantage : **l'emplacement retenu n'est pas la position du curseur.** Au-dela de la
deuxieme ligne, une classe a trois emplacements saute d'un cran
(`DAT_005de710 = DAT_005de6fc + 1`). Et le meme trou se retrouve dans la boucle de dessin, qui saute
la troisieme rangee **et n'avance pas son indice de libelle** — le `param_4++` est a l'interieur du
`if`. Les deux se repondent : sans cela, les rangees suivantes porteraient le mauvais nom.

### Poser un objet est refuse par deux tests, pas un

Le mot de drapeaux de l'objet (`+0x12` de sa fiche) doit porter le bit que l'emplacement demande :

| Emplacement | Bit exige |
|---|---|
| 0, 1, 2 | `0x100`, `0x200`, `0x400` |
| 3, 4 (les deux mains) | `0x800`, **et seulement si l'autre main ne porte pas deja cet objet** |

**Et** le personnage doit porter ce meme bit dans son propre mot de `+0x112`. Les deux tests sont
lies par un `||` de refus, donc il suffit qu'un seul echoue. Un refus joue le bruitage d'erreur.

Poser un objet deja porte a cet emplacement le **retire** (`0xff`) : la validation est une bascule,
pas une pose. Les cinq passes de statistiques repassent juste apres, puisque le total change — c'est
la troisieme fois qu'elles apparaissent dans cette seule page.

### Les six flux d'etiquettes de `0x0056ac30`

Six blocs de **quatre octets**, releves verbatim. Cinq sont `04 2a <y> 00` — un code de placement,
ses deux operandes, la fin — et le sixieme (`0d 00 00 00`) referme la rangee.

La boucle les prend **a l'envers**, depuis `0x0056ac44` : la rangee 0 est la plus haute
(`y = 0x1e`), la rangee 4 la plus basse (`y = 0x54`). Bornes : au-dessous deux dwords sans rapport,
au-dessus une paire de pointeurs de code (`0x005315b0`, `0x00531b90`).

Le pictogramme d'emplacement vide se prend lui aussi a l'envers, dans le bloc de sprites deja
releve : `&DAT_0056abe8 + compteur * -8`, soit les cinq dernieres cellules de
`s_ItemSprites_0056aae0`.

### Un cas par defaut que le binaire n'a pas

Le `switch(DAT_005de710)` qui choisit le bit exige n'a **pas** de branche par defaut : au-dela de 4,
le registre garderait sa valeur precedente. `DAT_005de710` est borne a 4 par le choix
d'emplacement, donc le cas ne se produit pas — mais la transcription leve plutot que d'inventer une
valeur, ce qui rendrait le jour ou il se produirait visible au lieu de silencieux.

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent, trois runs :

| Sequence | Resultat |
|---|---|
| `…,RIGHT,WAIT3,RIGHT,WAIT8` | 470 primitives par image — les cinq rangees, trois portees et deux vides |
| `…,RIGHT,RIGHT,ENTER,DOWN,ENTER` | 498 — la grille s'ouvre sur « Choisis ce que tu aimerais echanger. » |
| `…,RIGHT,RIGHT,ENTER,ENTER,ENTER` | 471 — **l'epee est retiree**, la rangee repasse a « ­ARME­ » |

Meme reserve que les jours precedents : **la fidelite du rendu contre le jeu d'origine n'est pas
verifiee.**

---

## 2026-08-25 (suite 6) — l'onglet 5, la configuration de la manette

`FUN_0052a850`, page 0, `case 5` : Blaze.c 76446..76571, binaire **0x0052cf0a..0x0052d2b4**. C'est
le quatrieme et dernier onglet du Guerrier, et le seul des trois restants qui soit atteignable avec
le personnage de depart — trois `RIGHT` depuis l'onglet 0 y menent.

Trois rangees, dessinees a gauche par un cadre pictogramme (« CONTROLE », « DIRECTION »,
« JOUEUR ») et a droite par leur valeur :

| Rangee | Octet du personnage | Valeurs | Etiquettes |
|---|---|---|---|
| 0 | `+0x2b7` | cycle 0..2 | `PTR_s_Type_A_0056aa20` — « Type A/B/C » |
| 1 | `+0x2ba` | bascule 0/1 | `PTR_DAT_0056aa30` — « ␣␣␣Activé » / « Désactivé » |
| 2 | `+0x2b9` | bascule 0/1 | idem |

### Le curseur de rangee est `DAT_005de6f8`, pas un curseur de grille

Les onglets 1 et 2 se servent de `DAT_005de6f8` comme d'un index dans une liste ; ici il **est** la
rangee, borne a 0..2, et il sert directement de coordonnee : le curseur se pose a
`(0x58, rangee * 0x1a + 0x24)`. C'est aussi lui que le saut indexe de `0x0052fa4c` consulte.

### Quatre touches, trois mots de manette differents

C'est le point ou une lecture rapide se trompe, parce que `Blaze.c` ecrit les quatre acces de la
meme facon alors que le binaire lit trois mots differents :

| Geste | Pseudo-C | Binaire | Touche |
|---|---|---|---|
| rangee precedente / suivante | `param_5[3] & 0x10` / `& 0x40` | `test byte [edi+6]` | haut / bas |
| sortir de l'onglet | `puVar27[1] & 0x40` sur l'octet 3 | `test byte [edi+3],40h` | croix (Echap) |
| type de manette, en arriere | `puVar27[1] & 0x80` | `test byte [edi+2],80h` | gauche |
| type de manette, en avant | `puVar27[3] & 0x20` | `test byte [edi+6],20h` | droite |
| basculer | `puVar27[1] & 0xa0` | `test byte [edi+2],0A0h` | gauche **ou** droite |

Le mot 1 est l'etat presse, le mot 3 l'etat repete : la rangee change au repete, le type de manette
recule au presse et avance au repete. L'asymetrie est dans le binaire, elle est transcrite telle
quelle.

### Le `case 3` du saut indexe est inatteignable, et il est transcrit quand meme

La table de `0x0052fa4c` porte quatre entrees ; `DAT_005de6f8` est ramene dans 0..2 quelques
instructions avant le saut. Le `case 3` est donc mort — mais c'est le **remappage des quatre boutons
d'action**, et il donne son sens a deux tables voisines : `0x0056aa38` porte les vingt-quatre
permutations de {0,1,2,3} (exactement les 24 arrangements, ce qui confirme la borne `0x17` du code)
et `0x0056aa98` les quatre masques `0x1000`/`0x2000`/`0x4000`/`0x8000`. Les quatre resultats vont
dans le personnage a `+0x1fc0`, `+0x1fc2`, `+0x1fc4` et — le quatrieme **saute deux mots** —
`+0x1fca`.

### Deux racines de ressources dans le meme corps

Le curseur de rangee prend son atlas dans `DAT_005e97c0`, les trois cadres dans `DAT_005e97c4`.
`Blaze.c` ecrit les deux pareil, ce qui donne toutes les chances de recopier l'une pour l'autre ; le
desassembleur tranche — `mov ecx,[5E97C0h]` en 0x0052d0a1 contre `mov eax,[5E97C4h]` en 0x0052d151.

### La queue est commune aux deux branches

`if (DAT_005de718 < 0)` ne choisit que la **tete** : onglet non entre, un bloc de libelles est pose
sur le descripteur racine (c'est le panneau « Fenêtre Contrôle » du bas) ; onglet entre, les touches
sont lues et le curseur est dessine. Les trois cadres et les trois etiquettes s'executent dans les
deux cas — le `}` de Ghidra se referme avant, et c'est facile a lire de travers.

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent, trois runs (2641, 3121 et 3181 images, ~488
primitives par image, aucun plantage) :

| Sequence | Resultat |
|---|---|
| `START,WAIT4,RIGHT,RIGHT,RIGHT,WAIT10` | l'onglet s'affiche : trois pictogrammes, « Type A », « Activé », « Activé », et le panneau « Fenêtre Contrôle » |
| `…,ENTER,DOWN,DOWN` | le curseur apparait et descend jusqu'a la rangee « JOUEUR » — la branche « onglet entre » |
| `…,ENTER,RIGHT,DOWN,RIGHT,DOWN,LEFT` | « Type B », et les **deux bascules passent a « Désactivé »** |

Meme reserve que les jours precedents : **la fidelite du rendu contre le jeu d'origine n'est pas
verifiee.**

---

## 2026-08-25 (suite 7) — la page 1, « Objets »

`FUN_0052a850`, `case 1` du switch de pages : Blaze.c 76574..76672, binaire
**0x0052d2b9..0x0052d50b**. C'est la page des **objets de quete**.

### Le role est etabli a l'ecran, pas deduit

La page parcourt les trente-deux premiers bits de `DAT_005e9840` — la banque de drapeaux
d'evenement — et prend, pour chaque bit leve, un enregistrement de huit octets dans
`DAT_005e97c8 + 0x70` : un nom en `+0`, une description en `+4`. Rien dans le depot ne dit ce que
ces bits representent : le titre de la page vit dans le fichier de ressources, pas dans le binaire.

Une **sonde temporaire** — `*(int*)0x005e9840 |= 0x0f` en tete du menu, retiree apres — a tranche en
un run : le titre est « - Objets - », la liste dit « Clé de l'aubergiste, Pierre triangulaire,
Pierre-faucille, Pierre pentagonale », et la description de la troisieme est « Pierre étrange qui
émet une lumière blafarde ». La table de `DAT_005e97c8 + 0x70` est donc bien peuplee, et ces
trente-deux bits sont les objets de quete du groupe.

C'est la lecon a retenir de la journee : **quand une page ne s'atteint pas parce que l'etat de la
partie ne le permet pas, forcer l'etat le temps d'un run est moins cher que de raisonner dessus** —
et c'est la seule chose qui aurait pu nommer la page.

### Tout se dessine par une seule chaine

Les noms sont concatenes, separes par `", "`, dans un tampon de brouillon a `MainMem + 0x1b0000`,
puis le tampon entier part dans le moteur de texte. Il n'y a **pas** de curseur dessine : l'entree
selectionnee est encadree par deux codes de controle, `0x14 0x06` avant et `0x14 0x07` apres,
c'est-a-dire un changement de couleur. La capture le confirme — l'entree visee est jaune.

Consequence : `DAT_005de6a8`, trente-deux octets, garde la correspondance **rang affiche → numero de
bit**. Sans elle, `DAT_005de6fc` (un rang dans la liste courte) ne dirait rien du bit qu'il
represente, et la description ne se retrouverait pas.

### Le defilement est un remplacement, pas un cadrage

Au-dela de douze entrees, les `compte - 12` premieres sont ecrites `"..."` au lieu de leur nom.
Elles gardent leur place dans la chaine et dans la numerotation : rien ne bouge, le texte
raccourcit. C'est tout le defilement qu'il y a.

### Deux etats, portes par `DAT_005de70c` seul

Negatif, la liste n'est pas entree et le panneau du bas montre le libelle fixe de `+0x2e4`
(« Page Objets ») ; a zero ou plus, gauche/droite deplacent `DAT_005de6fc` et le panneau montre la
description de l'entree visee. Entrer demande le carre **et** au moins une entree ; sortir est la
croix. Les deux bornes du deplacement ne sont pas symetriques — la gauche s'arrete a zero par un
test d'egalite, la droite compare a `compte - 1`.

### Une ecriture d'un octet avant le tampon, transcrite telle quelle

`text[-2] = 0` enleve le dernier `", "`. Sans aucune entree, il n'y en a pas eu, et l'ecriture tombe
a `MainMem + 0x1AFFFE`. Le prologue rend le cas inatteignable (on n'entre pas sur la page quand le
dword est nul) et l'adresse reste dans l'arene : transcrit tel quel plutot que garde.

### `playtest.ps1` sait changer de page

Deux jetons ajoutes, `PGUP` et `PGDN` : les gachettes hautes, que `FUN_0052a850` lit en `0x0800`
(avancer) et `0x0400` (reculer). Leur liaison secondaire par defaut est PageUp pour R1 et PageDown
pour L1 — donc **`PGUP` avance et `PGDN` recule**, l'inverse de ce que les noms suggerent.

Un `PGUP` depuis la page 0 sur une partie neuve tombe sur la **page 3** : les pages 1 et 2 sont
toutes les deux sautees, ce qui mesure d'un coup que `DAT_005e9840` est nul et que `DAT_005e9001`
n'a pas son bit `0x10`.

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent, quatre runs :

| Sequence | Resultat |
|---|---|
| `START,WAIT4,PGUP,WAIT10` (sans sonde) | `PLANTAGE : … page 3` — les deux pages intermediaires se sautent, le jeton marche |
| `START,WAIT4,PGUP,WAIT10` (avec sonde) | aucun plantage, 383 primitives par image, « - Objets - » et ses quatre entrees |
| `…,PGUP,ENTER,RIGHT,RIGHT` (avec sonde) | 384 — la troisieme entree passe au jaune, le panneau donne sa description |
| `START,WAIT10` (sonde retiree) | 509 — la fiche de personnage, comme avant : pas de regression |

Meme reserve que les jours precedents : **la fidelite du rendu contre le jeu d'origine n'est pas
verifiee.**

---

## 2026-08-25 (suite 8) — la page 2, « Table » : la dalle aux cristaux

`FUN_0052a850`, `case 2` : Blaze.c 76672..76697, binaire **0x0052d510..0x0052d60e**. Vingt-cinq
lignes, et **aucune touche lue** — c'est la seule page d'affichage pur des six.

### Ce qu'elle fait

Dix images, une par bit leve parmi les dix premiers de `DAT_005e9000`, posees aux dix positions de
`0x00564748`. La tuile est **l'indice du bit**, pas le rang d'affichage : le compteur avance a
chaque tour de boucle, meme quand le bit est absent. Un cristal manquant laisse donc son trou, et
les autres ne se decalent pas.

### Le role, etabli a l'ecran

Titre « - Table - », panneau du bas « Page Cristaux », et une dalle de pierre gravee portant neuf
cristaux. La **dixieme tuile est la dalle elle-meme** — un descripteur de 0x80 par 0x70 en
`0x00564740`, pose en dernier et donc derriere les neuf autres. Le bit 9 n'est pas un cristal de
plus, c'est le support ; sans lui les cristaux flotteraient sur le fond.

Les deux tables voisines sont maintenant nommees dans `DataSymbols.md` : neuf descripteurs de
cristal en `0x005646f8`..`0x00564738`, celui de la dalle en `0x00564740`, dix positions en
`0x00564748`..`0x0056476c`. Elles etaient deja dans le depot, relevees d'un bloc en 2026-08-22 pour
la carte du monde — la lecon « un descripteur ne se releve pas seul » a paye ici sans qu'on ait rien
a faire.

### La sonde qui ne tenait pas — et pourquoi

Le premier essai posait `*(int*)0x005e9000 |= 0x13ff` en tete du menu. Sans effet : le run tombait
toujours sur la page 3. **`DAT_005e9000` n'est pas une banque de la partie, c'est un cumul
recalcule** — `FUN_00535710` (`RefreshPartyAbilityMask`) remet ses huit dwords a zero et les
reconstruit par OU des blocs de trente-deux octets de `+0x2e0` de chaque membre. Et le prologue
l'appelle **juste apres avoir change de page, avant de tester le saut**. La sonde etait donc effacee
entre son ecriture et sa lecture.

La sonde qui marche se pose dans `FUN_00535710` lui-meme, apres la remise a zero. C'est la lecon a
garder : **avant de sonder une adresse, chercher qui la reecrit dans la meme image** — un cumul
recalcule par frame ne se force pas depuis l'exterieur.

Au passage, cela dit ou vivent ces bits : dans les personnages, pas dans la partie. Les cristaux se
portent.

### Le dword de couleur n'est pas repose

Rien dans ce corps n'ecrit `DAT_005de5fc` : la page se sert de ce que la precedente a laisse dans le
bloc de brouillon. Verifie au desassembleur — aucun acces a `0x005de5fc` entre 0x0052d510 et
0x0052d60e. Transcrit tel quel.

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent, trois runs :

| Sequence | Resultat |
|---|---|
| `START,WAIT4,PGUP,WAIT8` (sonde sur la banque) | `PLANTAGE : … page 3` — la sonde ne tient pas, voir ci-dessus |
| `START,WAIT4,PGUP,WAIT8` (sonde dans `FUN_00535710`) | aucun plantage, 391 primitives par image, la dalle et ses neuf cristaux |
| `START,WAIT10` (sonde retiree) | 509 — la fiche de personnage, comme avant : pas de regression |

Meme reserve que les jours precedents : **la fidelite du rendu contre le jeu d'origine n'est pas
verifiee.**

---

## 2026-08-25 (suite 9) — la page 3, « Organisation du groupe »

`FUN_0052a850`, `case 3` : Blaze.c 76697..76833, binaire **0x0052d613..0x0052db32**. C'est la
premiere page du menu qui s'atteint **sans sonde** : sur une partie neuve, un `PGUP` depuis la
page 0 y tombe directement, les pages 1 et 2 se sautant.

### Ce qu'elle fait

Gauche/droite choisissent un membre, haut/bas changent sa **couleur**, le carre lui donne le role de
meneur, la croix le met de cote. Puis une colonne par membre : marque de meneur, deux pictogrammes
d'etat, le numero de couleur, l'image de la manette, et le portrait.

### `+0x2bd` est une couleur, pas un rang de formation

C'est la correction que l'ecran a apportee. En lisant seul le desassembleur, un octet borne a 0..3
par un `& 3`, modifie par haut/bas sur une page d'organisation de groupe, se lit naturellement comme
une **rangee de formation**. Le panneau dit « Couleur 0 », et deux appuis sur haut font passer les
cheveux du portrait de bleu a brun.

Ce n'etait meme pas une inconnue : la fiche de personnage passe deja ce meme octet en `extra` au
portrait, c'est-a-dire comme selecteur de palette. La lecon est la meme que pour `s_SizeSetting` en
2026-08-22 — **le nom existait dans le depot, il suffisait de regarder l'autre site d'appel avant
d'ecrire une interpretation**.

### Donner le role de meneur echange deux sieges

Quand le membre choisi etait ecarte (bit `0x10000` de ses drapeaux), il reprend le siege de l'ancien
meneur : les octets `+8` et `+9` de l'entree de `DAT_005e9180` — emplacement de manette et index de
personnage — sont **echanges**, pas copies, et c'est l'ancien meneur qui herite de la mise a l'ecart.
Le meme geste que `PassLeaderToNextEligible` (`FUN_0050e520`) fait de son cote ; la croix appelle
d'ailleurs cette fonction-la.

### Deux refus, deux raisons

Le carre est refuse si le membre vise est deja meneur, ou si son bloc `+0x140` porte l'un des bits
`0x400001c0`. La croix est refusee tant que `DAT_005de034` est sous 2 — verifie en jeu avec un
groupe d'un seul membre : la croix ne fait rien et ne plante pas. Les deux jouent le meme bip de
refus (`FUN_0053dbc0`).

### Trois indexations dans une seule boucle

C'est le piege du corps, et il n'apparait qu'au desassembleur :

| Ce qui est lu | Indexe par | Pas |
|---|---|---|
| l'abscisse de la colonne, `x * 0x35 + 0x18` (et `+ 0x1c` pour le portrait) | l'**index de personnage**, `DAT_005e93c8` | — |
| l'emplacement de manette et son image | ce meme index de personnage | `0x4c` |
| la couleur, `+0x2bd` | le **rang de la boucle** | `0x2000` |
| les drapeaux et le portrait | le rang de la boucle | `0x9c` |

### Le `CONCAT22` du dernier appel

Le dernier `FUN_00521df0` recoit un `extra` que Ghidra ecrit
`CONCAT22((short)(DAT_005de6f8 >> 0x10), couleur)`. Le binaire fait `movzx ax, byte [ebx]` sur un
`eax` qui portait encore `DAT_005de6f8` : seul le mot bas est remplace. Le mot haut est
**demontrablement nul** — quinze instructions plus haut, ce meme `DAT_005de6f8` est borne dans
`[0, DAT_005e9020)`. Transcrit comme la seule couleur, avec la borne citee en commentaire : un
artefact de registre partiel n'est pas une valeur.

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent, deux runs, 388 primitives par image, aucun plantage :

| Sequence | Resultat |
|---|---|
| `START,WAIT4,PGUP,WAIT8` | « - Organisation du groupe - », le portrait, la marque de meneur, « ◀1P▶ », « Couleur 0 » |
| `…,PGUP,UP,UP,ESC` | « Couleur 2 » et **les cheveux du portrait changent** ; la croix est refusee, groupe d'un seul membre |

Meme reserve que les jours precedents : **la fidelite du rendu contre le jeu d'origine n'est pas
verifiee.**

---

## 2026-08-25 (suite 10) — la page 4, la carte du monde et ses reperes

`FUN_0052a850`, `case 4` : Blaze.c 76833..77060, binaire **0x0052db5b..0x0052e33e**. La plus grosse
page apres la fiche, et la seule qui sorte de l'interface : image de carte qui defile, huit reperes
poses par le joueur, et le groupe dessine en **modele 3d**.

### C'est le jumeau de la rose des vents

Le bloc central — mettre la camera de cote, poser une vue fixe a 800, emettre le modele, restituer —
est celui de `DrawCompassRose` dans `WorldMapTick.cs`, instruction pour instruction : memes
emplacements de sauvegarde (`0x005fa520`..`0x005fa538` pour le decalage monde et la pose,
`0x005e9b00` pour les huit dwords de matrice), meme `Ordinal_378` avant et apres. Deux differences
seulement :

* le cap est **inverse** le temps du rendu (`neg ax` sur `pose[1]`) — la carte se lit du dessus ;
* le modele est **l'element 2** du tableau d'offsets du paquet, la rose prenant l'element 0.

Avoir la rose deja transcrite a fait la moitie du travail. C'est l'argument pour transcrire les
jumeaux dans la foulee plutot que de les laisser pour plus tard.

### Le defilement se fait dans le descripteur d'atlas

Ce n'est pas le sprite qui bouge : les **deux premiers octets** de `**(DAT_005e97cc + 0x94)` —
l'origine u et v de la cellule — sont reecrits a chaque image depuis le decalage de camera, et le
sprite reste plante a (0x80, 0x70). C'est la fenetre lue dans la texture qui se deplace.

Verifie a l'ecran : trois secondes de droite maintenue et le continent sort du cadre par la gauche,
le modele du groupe disparaissant avec lui — la borne `0x14 < x < 0xec` le supprime proprement.

### Les quatre directions sont lues sur le mot 0

C'est la **seule** page qui lise l'etat *tenu* (`param_5[0]`) et non un front. Quatre unites par
image tant que la touche est enfoncee : c'est ce qui fait un defilement et non un pas a pas. Toutes
les autres pages lisent le mot 1 (presse) ou le mot 3 (repete).

### Un repere fait huit octets, et l'efface en ecrit quatre

Deux `short` de position monde reduite au quart, puis un mot d'etiquette qui empaquette
`DAT_00566c84`, `DAT_00566c88` et la zone courante. Un repere vide se reconnait a son `short` de
`+4` **negatif** — mais l'effacement ecrit un `dword` de `0xffffffff` (`mov [eax+4],ebp`, ebp = -1).
Le test lit deux octets, l'ecriture en pose quatre ; transcrit tel quel.

Le repere n'est dessine que si son etiquette correspond a la zone affichee, et **l'octet de zone est
ignore** quand le terrain courant n'a pas le bit `0x10` : les deux cotes de la comparaison perdent
alors leur octet bas.

### Le recadrage sur le meneur, quatre fois en toute lettre

Le meme bloc de huit instructions apparait dans le prologue (changement de page) et **trois fois**
dans la page 4 : a l'ouverture de la liste, a la croix, et apres avoir pose un repere. Extrait en
`RecentreMapOnLeader` — une unite qui se tient seule, pas un raccourci d'ecriture.

### Deux ecritures de seize bits la ou la rose en fait trente-deux

Le bloc de placement de `0x005de6c8` a exactement la forme du cadre de pile de la rose : trois
entiers de position, deux d'echelle, un mot de reserve, deux `short`, l'etiquette. Mais `+0x10` et
`+0x18` sont ecrits en **mot** et non en dword (`mov [5DE6D8h],ax`, `mov [5DE6E0h],di`) : le mot
haut garde ce qu'il avait. Sur un bloc statique reutilise d'une image a l'autre, cela compte.

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent, quatre runs, aucun plantage, ~314 primitives par
image (la page en dessine moins que la fiche, elle n'a ni portrait ni texte) :

| Sequence | Resultat |
|---|---|
| `START,WAIT4,PGUP,WAIT3,PGUP,WAIT8` | la carte, le continent, le groupe en fleche rouge, et la rangee des huit emplacements |
| `…,HOLD:RIGHT,WAIT3,FREE:RIGHT` | **le continent sort du cadre** et la fleche du groupe avec lui — le defilement et la borne |
| `…,ENTER,ENTER,ENTER` | le curseur de liste apparait, un repere **orange** — la couleur de l'emplacement 0 — se pose a la position du groupe |
| `…,ENTER,RIGHT,ENTER,ENTER` | idem sur l'emplacement 1, et le repere est **bleu** : la teinte suit l'emplacement |

Les deux derniers disent aussi que les huit emplacements partent **vides** : le premier carre pose,
il n'efface pas.

Meme reserve que les jours precedents : **la fidelite du rendu contre le jeu d'origine n'est pas
verifiee.**

---

## 2026-08-25 (suite 11) — la page 5, « Options » : LE MENU EST COMPLET

`FUN_0052a850`, `case 5` : Blaze.c 77060..77698, binaire **0x0052e343..0x0052f568**. **638 lignes**,
la plus grosse page du menu. Avec elle, **les six pages de `FUN_0052a850` sont transcrites**.

### Trois niveaux emboites, quatre verrous

`DAT_005de710` dit quelle entree du petit menu (Sauvegarde / Quitter) a ete validee,
`DAT_005de70c` quel membre porte la sauvegarde, `DAT_005de714` quel emplacement de carte est
retenu, et `DAT_005de71c` mene la machine a etats de la carte — 0 a 8 pour la marche normale, des
numeros negatifs pour les echecs. `DAT_005de720`/`DAT_005de724` ajoutent un battement d'une image
apres chaque changement d'etat, le temps que la boite de dialogue soit a l'ecran avant que l'etat
suivant n'agisse.

Le `case -5` **tombe volontairement dans le `case -4`** : le binaire n'a pas de saut entre les deux,
le desarmement du port n'est qu'un prologue au meme nettoyage.

### La sauvegarde ecrit pour de bon — et c'est verifie sur le disque

Ce port remplace la carte memoire par des fichiers de `save\`, si bien que le chemin complet
s'exerce. Le `case 7` ne se contente pas d'ecrire :

1. il relit les 0x100 octets d'en-tete du fichier existant et compare sa somme de controle a celle
   du bloc du personnage — trois issues, echec de lecture, somme differente (c'est la sauvegarde
   d'un autre personnage), ou la suite ;
2. il relit la table d'icones et y **fusionne** les dix entrees du personnage, chacune de seize
   octets, ajoutee seulement si aucune ligne identique n'existe deja ;
3. il colle le niveau en chiffres a la fin du titre ;
4. il recopie huit banques globales (`0x005e96e0`, `0x005e9780`, `0x005e9060`, `0x005e9840`,
   `0x005e9720`, `0x005e92c0`, `0x005e9980`, `0x005e9360`) et les huit reperes de la page 4 dans le
   bloc du personnage ;
5. il capture la palette depuis la VRAM ;
6. il recalcule la somme sur 0x1ffc octets et ecrit **en trois morceaux**.

En jeu : « Mémorisation achevée. », et un fichier neuf de **0x6000 octets** dans
`OpenForesia/save/`, commencant par `53 43 11 03` — le « SC » des « title frames » PlayStation. Ce
n'est pas une capture qui ressemble a ce qu'il faut, c'est un artefact verifiable sur le disque.

### Le titre se termine par un mot sentinelle

La boucle qui colle le niveau remonte depuis `+0x44` jusqu'a trouver `0x6b82` — quatre octets
releves en `0x0056ac2c`, entre la chaine « Sauvegarde » et les flux d'etiquettes de `0x0056ac30`,
dont seul le mot bas est lu. Les chiffres qui suivent **ne sont pas de l'ASCII** : ce sont les
glyphes de seize bits du moteur de texte, pris dans la table de la ressource principale a
`+0x70 → +4 → +0x20`.

### Un `byte` qui cachait un `short`, et son voisin

`DAT_0060498a` etait declare `byte` dans `SaveSlots.cs` avec la note « aucun lecteur trouve dans
`Blaze.c` ; le role reste a etablir ». Le lecteur est cette page : elle lit **les deux moities de
`0x00604988` en seize bits** — l'angle et le rayon de l'icone de carte qui tourne — et les amortit,
l'angle vers le douzieme de tour suivant et le rayon vers `0x1800`. Elargi en un bloc de quatre
octets avec deux vues.

C'est le meme piege que d'habitude, dans l'autre sens : ici ce n'est pas une adresse vue de deux
cotes, c'est **une largeur choisie sur un seul site d'ecriture**. Une note « aucun lecteur trouve »
est une invitation a revenir, pas une conclusion.

### Les tables d'emplacements de carte

Relevees et bornees, `0x0056a98c`..`0x0056aa18` :

| VA | contenu | role |
|---|---|---|
| `0x0056a98c`..`0x0056a9a8` | 4 rangees d'octets | l'identifiant de port de chaque emplacement, par configuration de multitap |
| `0x0056a9a8` | 4 pointeurs | `PTR_DAT_0056a9a8` |
| `0x0056a9b8`..`0x0056a9e0` | 4 rangees, **premier octet = le compte** | le nombre d'emplacements et leurs indices de position |
| `0x0056a9e0` | 4 pointeurs | `PTR_DAT_0056a9e0` |
| `0x0056a9f0`..`0x0056aa18` | 10 couples de `short` | les positions, relatives a `(0x8c, 0x48)` |

Deux emplacements sans multitap, cinq avec un seul, huit avec les deux. La plage se referme par le
haut sur `s_ConfigRowOffsets_0056aa18`, relevee le matin meme pour l'onglet 5 : dix entrees de
quatre octets tombent exactement entre les deux bornes.

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent, quatre runs (407 primitives par image, aucun
plantage) :

| Sequence | Resultat |
|---|---|
| `START,PGUP,PGUP,PGUP` | « - Options - », « Sauvegarde », « Quitter », le curseur, le portrait et le nom du membre, « Page Options » |
| `…,DOWN,ENTER` | « Retour au menu d'accueil ? » et le curseur oui/non — le curseur du petit menu disparait, comme le code le dit |
| `…,ENTER,ENTER` | « Mémorisation achevée. » **et un fichier neuf de 0x6000 octets dans `save\`** |
| `START,WAIT10` (regression) | 509 — la fiche de personnage, inchangee |

**Un run a ete perdu** et vaut d'etre note : le verdict disait « aucun plantage, 509 primitives par
image » alors que la sequence demandait trois `PGUP`. 509 est le compte de la **page 0** — la
sequence n'avait donc jamais ete vue par le jeu. Le piege est deja dans `test-protocol.md` (« une
relance perdue invalide une sequence `-Then` ») ; ce qui l'a tranche ici est le **nombre de
primitives**, qui identifie la page a lui seul.

Meme reserve que les jours precedents : **la fidelite du rendu contre le jeu d'origine n'est pas
verifiee.**

---

## 2026-08-25 (suite 12) — les onglets 3 et 4 : LE MENU N'A PLUS AUCUNE LEVEE

`FUN_0052a850`, page 0, `case 3` (Blaze.c 76124..76340, 217 l.) et `case 4` (76341..76445, 105 l.).
Avec eux, **les six pages et les six onglets du menu sont transcrits** : plus une seule
`NotImplementedException` dans tout `FUN_0052a850`.

### L'onglet 3 : les sorts

Six pages (quatre pour la classe 5), chacune une tranche du repertoire de l'ecole du personnage.
Haut/bas changent de page, gauche/droite parcourent la tranche, le carre retient le sort dans
`+0x2b6`. La tranche se lit dans `PTR_DAT_00564640` — un couple (premier, compte) par page — et
l'etat « appris » dans le bloc de bits de `+0x160 + ecole * 8`.

**La liste courte encode l'etat dans le signe.** Un sort appris entre dans `DAT_005de6a0` par son
numero, un sort inconnu par son **oppose**, et tout le reste du corps teste `< 0x80`. Le numero 0 est
donc ambigu — son oppose vaut aussi zero — et se lira toujours comme appris.

Ce n'est pas une deduction de lecture : **l'ecran le montre**. Sur un Guerrier sonde en classe 1, la
page 1 (numeros 0 a 4) affiche « Balle de feu » en premier et quatre « ? », tandis que la page 2
(numeros 5 a 9) n'affiche que des « ? ». La seule entree qui paraisse apprise est celle dont le
numero est zero. Transcrit tel quel, sans correction.

**Le verrou de mise au point n'affiche pas une triche, il ecrit.** Quand `DAT_005633a8` vaut zero —
il vaut 1 au chargement, et passe a zero apres « Gurkha » — la boucle **leve** le bit de chaque sort
qu'elle parcourt avant de le tester. C'est une ecriture dans le personnage, pas un affichage.

**Le nom du sort est reecrit sur place** : les zeros des quatorze premiers octets de
l'enregistrement de ressource deviennent des espaces, dans la ressource elle-meme. Transcrit tel
quel.

### L'onglet 4 : les herbes

Sept entrees fixes, sans page ni tranche. Le carre retient l'entree visee dans `+0x2b6` — **le meme
octet que l'onglet 3**. Sept libelles pris d'un bloc contigu de la table de ressources (`+0x398` a
`+0x3b4`) et sept nombres.

**Les memes huit octets, lus de deux facons.** Les nombres viennent de `+0x160 + ecole * 8`, un
octet par entree — exactement le bloc que l'onglet 3 lit **en bits**. Rien ne les reconcilie : c'est
la classe qui decide de la lecture, et `s_ClassTabs_0056a898` s'arrange pour qu'un personnage n'ait
jamais les deux onglets. C'est le genre de recouvrement que ce depot cherche d'habitude a debusquer ;
ici il est deliberé et il faut le laisser.

**La borne de la boucle de libelles est un decalage, pas un compte** : le binaire va de `0x398` a
`0x3b4` par pas de quatre, ce qui fait sept entrees et le dit sans avoir a les compter.

### Les tables relevees

| VA | contenu | role |
|---|---|---|
| `0x00564608` | `00 1d 35 49` | le rang du premier libelle de chaque ecole ; le quatrieme borne le troisieme |
| `0x00564610` | `00 05 05 05 0a 05 0f 05 14 06 1a 03` | ecole 0 : six couples (premier, compte) |
| `0x00564620` | `00 05 05 06 0b 05 10 03 13 03 16 02` | ecole 1 |
| `0x00564630` | `00 03 03 04 07 02 09 03 0c 04 10 04` | ecole 2 |
| `0x00564640` | 3 pointeurs → `0x564610/20/30`, puis **un dword nul** | `PTR_DAT_00564640` ; le nul borne le nombre d'ecoles a trois sans qu'on ait a le deviner |
| `0x0056a978` | 4 dwords + terminateur | les decorations de l'onglet 3, ancrees sur le descripteur 2 ; la plage se referme sur `0x0056a98c`, la table de ports de carte de la page 5 |
| `0x005de6a0` | 8 octets | `DAT_005de6a0`, la tranche affichee ; borne par `s_KeyItemIndex_005de6a8` |

### Comment les deux onglets ont ete atteints

Le Guerrier de depart n'a que les onglets 0, 1, 2 et 5. Une **sonde d'une ligne** — `classRow = 1`
puis `classRow = 4` dans la barre d'onglets, retiree apres — suffit a les afficher, et n'a pas eu
besoin de toucher a l'ecole ni au bloc de bits : le personnage reste un Guerrier, seul le choix
d'onglet change. C'est ce qui rend la sonde honnete ici, la ou forcer l'onglet directement aurait
melange deux jeux de donnees.

### Verification

`dotnet build` : 0 erreur. En jeu, par l'agent, quatre runs, aucun plantage :

| Sequence | Resultat |
|---|---|
| `…,RIGHT,RIGHT,RIGHT` (sonde classe 1) | 490 primitives par image — « Niveau Magie 1/6 », cinq pictogrammes, « Balle de feu », « MA 2 », trois pictogrammes d'element, « Fenêtre Spécialité » |
| `…,ENTER,UP,RIGHT` (sonde classe 1) | 484 — « 2/6 », cinq « ? », le curseur sur la deuxieme, « ???? » partout et « Cette capacité n'a pas encore été développée » |
| `…,ENTER,DOWN,ENTER` (sonde classe 4) | 531 — les sept herbes avec leur compte, le marqueur sur la deuxieme, et sa description |
| `…,RIGHT,RIGHT,RIGHT` (sonde retiree) | 487 — l'onglet 5 du Guerrier, comme avant : pas de regression |

**Un run a ete perdu**, la seconde fois de la journee, avec la meme signature : « aucun plantage,
509 par image » sur une sequence qui demandait trois `RIGHT`, et un `relance detectee` dans le
journal. La table des signatures ajoutee a `test-protocol.md` a suffi a le voir tout de suite.

Meme reserve que les jours precedents : **la fidelite du rendu contre le jeu d'origine n'est pas
verifiee.**

---

## 2026-08-25 (suite 13) — les racines de ressources du moteur de texte : il y en a cinq, pas deux

**Le plantage.** Une boite de dialogue leve
`FUN_00544a80 : racine de ressources 3 — l'original indexe &DAT_005e97c0 de quatre en quatre, et
seuls les emplacements 0 et 1 sont modelises.` Le message disait exactement ce qu'il fallait : la
garde etait un aveu de modelisation incomplete, pas une inconnue.

**Ce que le desassemblage tranche.** `0x00544a9c` porte `mov eax,[ecx*4+5E97C0h]`, avec `ecx` charge
d'un octet du flux de texte (`mov cl,[eax]`). L'indexation est donc sur **256 emplacements
possibles**, pas sur deux. La question n'etait pas « faut-il ajouter la racine 3 » mais « ou s'arrete
la suite de globales qui en sont ».

**Elle s'arrete a cinq.** Les cinq dwords a partir de `0x005e97c0` recoivent tous le retour du meme
chargeur, `FUN_00529b50` :

| n | VA | role | ecrivains dans `Blaze.c` |
|---|---|---|---|
| 0 | `0x005e97c0` | le tampon de ressources principal | `MainMem + 0xe0000` (ligne 74556) |
| 1 | `0x005e97c4` | son repertoire | ligne 74793 |
| 2 | `0x005e97c8` | le repertoire de l'etat de jeu courant | lignes 54703, 55577, 63141, 78867, 89405 |
| 3 | `0x005e97cc` | le repertoire de la zone | ligne 63216 |
| 4 | `0x005e97d0` | le repertoire du menu objets | lignes 63322, 71243 |

Le sixieme, `0x005e97d4`, n'en est pas une : son **seul** ecrivain (`Blaze.c` 81679,
`_DAT_005e97d4 = param_1`) y range un parametre, pas un retour de chargeur. La levee s'arrete donc a
5, et non a 256 — une borne tiree de la nature des voisins, pas d'une commodite.

**Les cinq etaient deja transcrites**, chacune sous son nom, dans trois fichiers differents
(`GameMemory.cs`, `Resources.cs`, `MainMenu.cs`, `WorldMap.cs`). Rien n'etait a decouvrir : ce qui
manquait etait le **lien** entre l'octet d'operande et ces globales. C'est le genre de trou que la
carte des symboles de `DataSymbols.md` rend visible en une ligne — les cinq y sont contigus, 4 octets
chacun — et qu'un `grep` sur un seul symbole ne montre jamais.

**Quatre copies du meme calcul.** `Handler_00544e80` (code `0x1c`), `Handler_00544810` (`0x01`),
`Handler_00544c10` (`0x0b`) et `Handler_00544a80` (`0x07`) portaient chacun leur propre `switch` a
deux branches et leur propre message de levee. Elles sont remontees dans une aide unique,
`UiText.ResourceRoot(slot, caller)` : le calcul est bien **un** dans le binaire — la meme sequence de
trois instructions, quatre fois — donc une seule reconstruction le dit mieux que quatre.

**Verification a l'execution.** Une sonde jetable dans `ResourceRoot`, sur le trajet de l'auberge
(`-Entry 0 … ENTER×4,ESC,START`), a imprime les cinq pointeurs et la premiere entree de leur table
`+0x70` :

```
racine 0  root=0x00ad0000  +0x70=0x00ad0194  [0] = 81 40 82 ED 82 F1 82 EE
racine 1  root=0x00ad08b0  +0x70=0x00ad0a90  [0] = « Tire sur… »
racine 2  root=0x00ad504c  +0x70=0x00ad5158  [0] = « Cle de l… »
racine 3  root=0x00ad5d50  +0x70=0x00ad609c  [0] = 1F D0 1D 08 00 0C 20 2D
racine 4  root=0x00ad975c  +0x70=0x00000000
```

C'est **la racine 3 qui est la preuve** : son entree 0 ne commence pas par du texte lisible mais par
`0x1f`, `0x1d` — deux codes de controle du moteur lui-meme. C'est donc bien une chaine de dialogue de
la zone, et non un libelle d'interface. Les racines 1 et 2 rendent du texte lisible, ce qui borne le
faux positif. La racine 4 a une table nulle tant que le menu objets n'est pas charge, exactement
comme l'original.

**Ce que la sonde a appris au passage, et qui ne se devinait pas.** Les 2 358 appels a
`ResourceRoot` du trajet viennent **du menu de jeu**, pas de la boite « - La reception - » : la
sequence sans `START` final n'en produit aucun. La boite de reception traverse donc le moteur de
texte sans passer par une racine indexee. A retenir avant de conclure d'un run qu'un code de
controle a ete exerce.

**Ce qui reste non exerce.** La racine 3 est confirmee **valide**, pas **empruntee** : aucun run de
l'agent n'atteint le dialogue qui levait. La confirmation que le plantage a disparu demande le trajet
de Vincent — voir le partage du 2026-08-24 dans `test-protocol.md`.

---

## 2026-08-25 (suite 14) — le plantage en parlant de biais : une lecture deux octets trop loin

**Le symptome, tel que Vincent l'a decrit, portait deja la reponse.** « Les PNJ de la deuxieme
piece, seulement quand je leur parle **de cote** — bas et droite ensemble ; en face a face, aucun
probleme. » Une condition qui depend de l'**orientation** et de rien d'autre : dans ce moteur, ce qui
depend de l'orientation, c'est l'**octant de cap de la camera**.

```
System.IndexOutOfRangeException  at Model3d.DrawChunk … Model3d.cs:line 1520
```

### Ce que la ligne 1520 faisait

Le chemin « balayage par cellules » de `FUN_0051fa40` parcourt les 49 cellules d'une grille 7x7 en
sautant celles que l'octant met hors du cone de vue. La liste des sauts est une **tranche de seize
octets** — huit cles de 16 bits `rang << 8 | colonne` — et le binaire y avance un `short*` **sans
aucune borne** (`Blaze.c` 68683-68687) : chaque correspondance consomme une cle et **lit d'avance la
suivante**.

### La simulation tranche en une seconde ce que le raisonnement laissait flou

Les huit tranches sont contigues, `0x00564558`..`0x005645d7`. Rejouer les huit balayages hors du jeu,
sur les octets releves dans le binaire, donne :

```
octant 0 : 8 sauts, dernier curseur lu 16    (dans la table)
octant 1 : 8 sauts, dernier curseur lu 32    (dans la table)
…
octant 6 : 8 sauts, dernier curseur lu 112   (dans la table)
octant 7 : 8 sauts, dernier curseur lu 128   (DEBORDEMENT)
```

Les huit octants font **tous** huit sauts, et chacun lit d'avance la premiere cle de la tranche
**suivante**. Pour les sept premiers c'est inoffensif — la tranche suivante existe. Pour le
huitieme, il n'y en a pas : `0x005645d8` est le debut de `PTR_DAT_005645d8`, la table de pointeurs
qui designe les tranches elles-memes.

**Ce que la simulation a corrige dans mon raisonnement.** J'avais deduit a la main que seules
certaines tranches finissaient sur la derniere cellule visitee, et donc que le nombre de sauts
variait d'un octant a l'autre. Faux : il est de huit partout. La conclusion — un seul octant
deborde — etait juste, la demonstration ne l'etait pas. Vingt lignes de simulation valaient mieux
que la lecture a l'oeil de huit lignes de table.

### Pourquoi le jeu de 1998 ne bronchait pas

Les deux octets lus en `0x005645d8` sont `58 45`, la moitie basse du premier pointeur
(`0x00564558`) : la cle vaut `0x4558`. Elle n'est **jamais consultee** — elle est lue en `(1,1)`, la
derniere cellule, et les deux boucles se terminent juste apres. Et meme consultee, `0x4558` ne peut
egaler aucun `rang << 8 | colonne`, tous deux bornes a 7. Sur x86, deux octets lus chez le voisin ne
coutent rien.

**C'est le premier defaut du catalogue qui plante la reconstruction sans avoir jamais rien casse
dans l'original** : `ReadOnlySpan<byte>` leve la ou une lecture x86 passait. Consigne en
[LB-013](../Reference/legacy-bug.md).

### La correction

`GameMemory.NextCellSkipKey(cursor)` restitue la lecture telle qu'elle est : dans la table tant qu'on
y est, et **les vrais octets de `PTR_DAT_005645d8`** au-dela — huit pointeurs de quatre octets vers
les huit tranches, espaces de `0x10`, releves dans le binaire. Le debordement est **reproduit, pas
garde** : ni zero de commodite, ni `if` qui evite la lecture. C'est la difference entre modeliser
l'espace d'adressage et se proteger de lui.

### La verification, et comment elle a ete obtenue

Le trajet de l'auberge ne suffisait pas : deux runs successifs montrent que le chemin `cellSweep`
tourne bien (~4 400 appels) mais **toujours dans l'octant 2**. Conduire la camera jusqu'a l'octant 7
au clavier n'est pas fiable — c'est exactement ce que le partage du 2026-08-24 constate.

Une **sonde d'une ligne** l'a donne directement : `octant = 7;` juste apres le calcul. Deux
verdicts :

| sonde | resultat |
|---|---|
| `octant = 7` force, compteur d'appels | ~2 800 appels, **aucun plantage** |
| trace dans la branche de debordement | **2 314** lectures, toutes `curseur 128, over 0` |

La seconde est celle qui prouve quelque chose : elle montre que la branche corrigee est **empruntee
2 314 fois**, la ou l'ancien code levait a la premiere. Les sondes retirees, le run de controle rend
509 primitives par image — la valeur de reference.

**Ce qui reste a Vincent** : refaire le dialogue de biais. Forcer l'octant prouve que le chemin ne
plante plus ; il ne prouve pas que c'est bien cet octant que la camera prenait.

### L'autre balayage n'est pas concerne, et c'est un defaut qui en masque un autre

`DrawWorldCells` (`FUN_00516fc0`) lit la meme table, mais sa lecture de cle suivante est celle de
**LB-009** : sur **un seul octet**. La cle devient `<= 0xff`, ne peut plus egaler `rang << 8 |
colonne`, et le curseur s'arrete apres une seule correspondance — bien avant la fin de la tranche. Le
bug de 1998 protege le code du bug de 1998.

---

## 2026-08-25 (suite 15) — opcode 17 : le changement de salle

**Le plantage.** La salle derriere le barman leve
`Interpreteur de script : opcode 17 = FUN_00515a10, pas encore transcrit.` Le message portait deja
tout ce qu'il fallait, y compris le chemin du desassemblage.

**Le releve etait tronque.** `FUN_00515a10.asm` s'arretait sur `(bad)` — la fenetre de 96 octets du
premier passage coupait le corps, qui en fait 110. La coupure tombait au milieu de
`mov [5DE53Ch],al`, dont le premier octet seul se lit comme invalide. Le README du repertoire le
disait deja pour `FUN_00515b40` ; c'est la deuxieme fois, et la deuxieme fois qu'un dump repris plus
large rend le corps entier d'un coup.

### Ce que fait l'opcode

```
mov eax,[esp+8]         ; l'entite (2e argument)
and edx,0FC0000h        ; genre
cmp edx,800000h         ; membre du groupe ?
mov edx,[esp+0Ch]       ; le curseur de script (3e argument)
jne 00515A7A            ; sinon : rien, on avance de 4
…
lea eax,[edx+4]         ; les DEUX chemins rendent script + 4
ret
```

Sur une entite du bon genre — le meme garde que `ScriptOpShowMessage` — il fait deux choses.

**Il marque l'entite comme partante** : `+0x98` recoit `+0x32`, les drapeaux perdent `0x08000200` et
gagnent `0x2000`, et le compteur `+0x12` avance d'un. Ce `+0x12` est exactement le champ
« occupee » que `ScriptOpShowMessage` consulte pour ne pas choisir un locuteur en transit.

**Il note la destination** dans quatre globales, puis pose le pas de boucle :

| globale | source | 0xff = ne pas changer |
|---|---|---|
| `DAT_005de528` — la zone | `script[1]` | oui |
| `DAT_005de52c` — la sous-zone | `script[2]` | oui |
| `DAT_005de530` — le point d'entree | `script[3]` | **non** |
| `DAT_005de53c` — le membre declencheur | `entite[8]` | — |
| `DAT_005645f8` — le pas de boucle | `2` | — |

**L'asymetrie du `0xff` est dans le binaire**, pas une simplification : deux `cmp cl,0FFh ; je` et
pas de troisieme. Elle a un sens — on peut rester dans la meme zone en changeant de porte, on ne
peut pas arriver sans point d'arrivee.

### Le pas de boucle a 2, et pourquoi c'est le detail qui compte

`Blaze.c` 57354-57363 fait **le meme geste** pour la sortie *marchee* : memes quatre champs
d'entite, memes globales de destination. Une seule difference — cette version-la pose le pas a
**1**, celle-ci a **2**.

Ce n'est pas cosmetique. `PlaceWorldMapParty` ne replace le groupe au point d'entree que
`if ((s_WorldMapStep_005645f8 & 0xff) == 2)`, et c'est la qu'il lit `DAT_005de530` et
`DAT_005de53c` — les deux globales que cet opcode vient d'ecrire. **Un transfert scripte repose donc
le groupe a la porte d'arrivee ; une sortie marchee le laisse ou son elan l'a mis.** Les deux
ecritures ne se comprennent que l'une par l'autre.

### Une largeur de store preservee

`DAT_005de528` est un `int` dans la reconstruction, la ou l'original y ecrit un octet
(`mov [5DE528h],cl`). Le store masque les trois octets hauts. Tous ses lecteurs connus le masquent
par `0xff`, donc la difference est invisible aujourd'hui — mais reproduire la largeur coute une ligne
et ne suppose rien sur les lecteurs a venir.

### Non exerce, et pourquoi

Une sonde posee sur l'opcode ne s'allume **pas** sur le trajet de l'auberge, herboriste comprise :
ce passage-la n'emprunte pas l'opcode 17. C'est coherent avec la lecture ci-dessus — c'est une sortie
marchee, celle de `Blaze.c` 57354. Atteindre un declencheur scripte demande la salle du barman, donc
Vincent.

**Reserve a dire franchement** : cet opcode ne fait que *noter* la destination. Le travail reel
arrive a l'image suivante, quand `LoadWorldMapArea` recharge la zone demandee — et ce chemin-la peut
tres bien buter sur autre chose qui n'est pas encore transcrit. Corriger l'opcode 17 ne garantit pas
que la salle s'ouvre ; il garantit que le jeu ne s'arrete plus **ici**.

---

## 2026-08-25 (suite 16) — la salle derriere le barman : le troisieme balayeur d'entites de scene

**L'opcode 17 a tenu.** Le jeu affiche la salle, puis leve ailleurs :

```
FUN_0047aa70, table 0x005ff400 entree 0 [00 08 90 80 00 e2 00 00 …] :
gestionnaire d'entite 0x405A70 non transcrit.
```

C'est exactement la reserve ecrite la veille au soir : « corriger l'opcode 17 ne garantit pas que la
salle s'ouvre ; il garantit que le jeu ne s'arrete plus ici ». Elle s'est verifiee en une session.

**Le corps etait dans `Blaze.c`** — ligne 2598 — contrairement a la famille des opcodes de script.
Le message de la levee proposait le desassembleur « s'il est absent de Blaze.c » ; il ne l'etait pas,
et un `grep` de dix secondes l'a montre. Lire la condition du message avant de suivre sa suggestion.

### Ce qui manquait

`FUN_00405a70` est le **troisieme** balayeur d'entites de scene, apres `FUN_00404ef0` et
`FUN_004055c0` deja transcrits. Meme forme exacte : 0x20 emplacements de 0x9c octets a partir de
`0x005ecaa0`, cinq tests par emplacement sur l'octet de type `+0x0c`, chacun garde par « vivante et
eveillee », puis `*entite &= 0x7fffffff` en sortant.

Sur ses cinq corps de reveil, **un etait deja la** et quatre manquaient :

| type | corps | etat |
|---|---|---|
| 0 | `FUN_00405680` | deja transcrit — c'est le type **3** du second balayeur |
| 1 | `FUN_00405b30` | ecrit |
| 2 | `FUN_00405be0` | ecrit |
| 3 | `FUN_00405cb0` | ecrit |
| 4 | `FUN_00405d20` | ecrit |

Toutes leurs primitives (`FUN_004051d0`, `FUN_00404fe0`, `FUN_004052f0`, `FUN_00405420`,
`FUN_00468080`) etaient deja en place. Il ne manquait que quatre corps de quinze lignes.

### Le numero de type est relatif au balayeur, pas au corps

`FUN_00405680` est le type **3** du second balayeur et le type **0** du troisieme. Le nom
`WakeSceneEntityKind3_00405680` porte donc un numero qui n'est vrai que pour un de ses deux
appelants. Les quatre nouveaux corps sont nommes `WakeSceneEntity_<adresse>` **sans numero** : il n'y
en a pas un seul a porter, et le suffixe d'adresse est la seule identite stable.

Le nom existant n'a pas ete change — son commentaire disait deja « il sert aussi de type 0 au
troisieme balayeur » — mais la lecon vaut d'etre retenue : **un numero d'index dans un nom suppose
qu'il n'y a qu'une table.**

### Un corps emis deux fois, verifie et non suppose

`FUN_00405be0` est mot pour mot `FUN_00405770` (le type 4 du second balayeur). La regle du depot
demande de verifier avant de transcrire, et c'est ce qui a ete fait :

- meme taille, 208 octets ;
- le desassemblage des deux ne differe que par les `jne short` internes, relogeables par nature ;
- **aucun `call` ne differe** — memes cibles, meme ordre.

Le compilateur a emis le corps deux fois. Les deux adresses restent distinctes parce que la table de
sauts les distingue, donc les deux fonctions restent distinctes ici aussi.

### Le seul des douze corps de reveil qui fasse du bruit

`FUN_00405b30` joue un son a l'etat 1, mais seulement si l'animation courante est **exactement** la 2
et son compteur d'images **exactement** 0x12. Une image precise d'une animation precise : c'est un
bruitage cale sur un geste — un pas, un objet pose. Les deux champs sont des `short` (`+0x1a` le
compteur, `+0x18` le numero), ce que Ghidra rend par `(&DAT_005ecaba)[i * 0x4e]`.

### Non exerce

La salle du barman ne s'atteint pas sans conduire le personnage. Le run de controle rend 509
primitives par image, la valeur de reference, et le build est vert — c'est tout ce que l'agent peut
etablir. **Meme reserve qu'hier**, et elle vaut d'etre repetee parce qu'elle s'est deja realisee
une fois : une salle qui s'ouvre reveille des entites qui n'ont peut-etre jamais tourne. Le prochain
plantage, s'il vient, se nommera.

---

## 2026-08-25 (suite 17) — la femme a la petite harpe, et un bloc `.data` alloue a zero

**La salle marche, les PNJ aussi.** Un seul geste levait : accepter la proposition de la femme a la
petite harpe de changer de musique.

```
FUN_0047aa70, table 0x005ff400 entree 1 [00 08 90 80 00 ff 00 00 01 00 00 00 …] :
gestionnaire d'entite 0x404E50 non transcrit.
```

Absent de `Blaze.c` cette fois — contrairement a `FUN_00405a70` la veille. Le message dit quoi faire
dans les deux cas ; c'est sa condition qu'il faut lire, pas sa suggestion.

### Ce que fait le gestionnaire

151 octets, deux moities, et **la seconde attend la premiere**.

A chaque image il decompte **300** sur les deux `short` de `+0x10` et `+0x12` du bloc d'ecran
`0x00566c48`, en les bornant a zero par le bas, remet le mot de tete a `0xc0` et republie le bloc par
`Ordinal_762`. Tant que l'un des deux est non nul, il s'arrete la : l'entite **reste armee** et
repasse a l'image suivante. Quand les deux sont a zero, il choisit la piste, la lance, et se desarme
(`and dword [eax],7FFFFFFFh`).

`0x3fff / 300` fait une cinquantaine d'images. Le changement de musique n'est donc pas instantane :
il y a une transition, et c'est elle que ce gestionnaire pilote.

### Deux recoupements qui n'ont rien coute

**La premiere moitie existe seule dans `Blaze.c`** — `FUN_0041af50`, ligne 11043 : le meme decompte,
sans entite, sans musique, sans desarmement. Elle n'est pas transcrite (rien ne l'appelle encore),
mais elle confirme la lecture du bloc et des deux champs sans avoir a les deduire.

**Le choix de piste est un calcul deja rencontre, ecrit autrement.** Ici, une cascade
`cmp al,1` / `cmp al,2` ; dans `ScriptOpAreaVerbMusic`, un `setne / dec / and 2` sans branchement.
Les deux calculent la meme identite bornee — 0, 1 et 2 se rendent eux-memes, toute autre valeur rend
0. Deux codegens pour une regle, ce qui la confirme mieux qu'une lecture seule.

### Le vrai enseignement : un bloc `.data` alloue a zero

`s_MenuScreenBlock_00566c48` etait `NativeMemory.AllocZeroed(0x28)`. C'etait juste tant que son
**unique** lecteur, `SetupMenuScreen`, ecrivait tous les champs avant de publier le bloc. Ce
gestionnaire-ci est le premier a **lire** `+0x10` et `+0x12` sans les avoir ecrits — et sur un bloc a
zero, son decompte serait deja fini a la premiere image : la transition disparaitrait, la musique
changerait d'un coup.

Le contenu initial est releve et modelise (voir `DataSymbols.md`). **La lecon se generalise** : un
bloc `.data` alloue a zero n'est faux que le jour ou quelqu'un le lit avant de l'ecrire, et rien ne
le signale avant ce jour-la. Meme famille de dette silencieuse que les huit champs `= Arena(0x…)`
du decoupage du 2026-08-19.

Verifie a l'ecran : la fiche de personnage est pixel pour pixel celle d'avant le changement, et le
debit reste a 509 primitives par image.

### Ce qui n'est pas etabli

`Ordinal_762` reste opaque. Elle est declaree, deja appelee par `SetupMenuScreen` sur ce meme bloc,
et sa decompilation dans `winsys3d.c` tombe sur un corps partage par des dizaines d'ordinaux — ce qui
ne dit rien. Les deux champs decomptes sont donc transcrits **pour ce qu'ils sont**, deux `short` du
bloc, sans leur preter un role d'affichage qu'on ne peut pas prouver.

### Non exerce

La salle du barman demande de conduire le personnage. Build vert, run de controle a 509 primitives
par image, capture identique a la precedente.

---

## 2026-08-25 (suite 18) — les six balayeurs d'entites de scene, fermes d'un bloc

**Le troisieme plantage de suite au meme endroit du graphe.** La chambre de l'analyseur d'objets
leve sur `0x00405D60`, apres `0x00405A70` (la salle du barman) et `0x00404E50` (la harpiste). Trois
allers-retours pour trois voisins immediats : c'etait le signal qu'il fallait arreter de les prendre
un par un.

### Le binaire les range lui-meme

`s_VtSource_0056b0c8`, entrees **32 a 37** — donc les sous-types `0xe0` a `0xe5` de la table
installee par zone :

| entree | sous-type | adresse | types reveilles | etat avant |
|---|---|---|---|---|
| 32 | `0xe0` | `0x00404EF0` | 1, 0, 2 | transcrit |
| 33 | `0xe1` | `0x004055C0` | 3, 4, 5, 7, 6 | transcrit |
| 34 | `0xe2` | `0x00405A70` | 0, 1, 2, 3, 4 | transcrit la veille |
| 35 | `0xe3` | `0x00405D60` | 0 | **manquant** |
| 36 | `0xe4` | `0x00405E70` | 0, 1 | **manquant** |
| 37 | `0xe5` | `0x00405FE0` | 5, 1, 2, 3, 4, 0 | **manquant** |

Les entrees 38 et 39 retombent sur la souche vide : le bloc se referme la. **Six balayeurs, pas
sept**, et la borne vient de la table, pas d'une supposition.

Cote corps de reveil, la famille est finie elle aussi : **quinze**, dont douze etaient deja ecrits.
Manquaient `FUN_00405da0`, `FUN_00405ed0` et `FUN_00405f50`. Trois sweepers et trois corps, tous
courts, toutes leurs primitives deja en place : le bloc entier valait moins qu'un des allers-retours
qu'il evite.

### Un seul est absent de `Blaze.c`, et il fallait le voir

`0x00405D60` n'a pas de nom Ghidra : il n'est atteint que par la table. Pire, il se cache **dans**
ce que la lecture rapide prend pour la fin de `FUN_00405d20` — ce dernier s'arrete en `0x00405d58`,
suivi de sept `nop` d'alignement, et le balayeur commence en `0x00405d60`. Releve au desassembleur,
64 octets, garde dans `Legacy/Raw/Blaze/FUN_00405D60.asm`.

C'est le plus depouille de la famille : un seul test par emplacement, le type 0.

### Verifie par extraction, pas a l'oeil

Les trois corps de reveil sont des cascades de quatre a sept `if (etat == n) primitive(record, m)`.
Les relire a l'oeil est exactement le genre de tache ou l'on intervertit deux nombres sans le voir.
Les triplets `(etat, primitive, argument)` ont donc ete extraits **des deux cotes** — de `Blaze.c` et
du C# — et compares :

```
--- 405da0 (Blaze.c) ---        --- 00405DA0 (C#) ---
== 1  FUN_00404fe0(puVar1,0)    == 1  AdvanceSceneEntityState_00404FE0(record, 0)
== 2  FUN_00404fe0(puVar1,1)    == 2  AdvanceSceneEntityState_00404FE0(record, 1)
…                               …
== 7  FUN_00405420(puVar1,1)    == 7  CountDownSceneEntityState_00405420(record, 1)
```

Les quinze lignes des trois corps concordent. **Ca coute deux commandes et ca remplace une relecture
qui n'aurait rien prouve.**

### Ce que la famille dit une fois complete

`FUN_00405f50` est le **seul des neuf** appelants de `FUN_004052f0` a passer une pause d'inactivite
**fixe** (`2, 3, 1`) ; les huit autres passent `rand() % 10`, un avec un `+ 10`. Ce decor-la ne
respire pas au hasard — et on ne pouvait pas le savoir avant d'avoir les neuf sous les yeux.

`FUN_00405fe0` est `FUN_00405a70` plus le type 5, et il commence par lui : l'ordre du binaire est
5, 1, 2, 3, 4, 0. Conserve tel quel — un reveil peut changer l'octet de type, et le binaire le relit
avant chaque test.

### Non exerce

La chambre demande de conduire le personnage. Build vert, run de controle a 509 primitives par
image. L'oracle d'extraction ci-dessus est ce qui remplace, pour ces trois corps, une verification a
l'ecran qui n'est pas a ma portee.

---

## 2026-08-25 (suite 19) — le verbe 56 : l'expertise d'un objet

**Le second dialogue de l'homme de la chambre** appelle le verbe 56 de la table de zone,
`FUN_0053dd20`. Absente de `Blaze.c`.

**Le relevé etait deja au depot depuis le 2026-08-16** — `Legacy/Raw/Blaze/FUN_0053dd20.asm` et sa
decompilation ciblee `FUN_0053dd20.c`. Neuf jours d'avance sur le besoin. Reflexe a garder :
**regarder dans `Legacy/Raw/` avant de relancer un dump**, la moitie du travail y est parfois deja
faite.

### Trois temps

**Trouver.** Les dix emplacements d'objet du personnage (`MainMem + (joueur + 0x78) * 0x2000 +
0x300`, pas de `0x280`) sont parcourus jusqu'au premier occupe **et** non identifie — octet de tete
non nul, bit `0x8000` de `+0x12` eteint. Les dix epuises : `DAT_005e909f = 0` et le verbe rend
**1**. C'est sa seule sortie non nulle, donc la seule qui fasse brancher le script — « je n'ai rien
a expertiser ».

**Expertiser.** Le bit est pose, le nom complet de l'objet est **recopie octet par octet** dans le
libelle 1 de la zone (`repertoire + 0x70`, entree 1) — c'est comme cela que la phrase du personnage
nomme l'objet — et cinq champs du bloc de panneau sont remplis. Deux entrees de la table de decodage
aussi, dont l'une porte le numero du joueur.

**Propager.** Les dix emplacements sont reparcourus : tout objet occupe encore non identifie dont
les **seize premiers octets** sont identiques a celui qu'on vient d'expertiser est marque identifie a
son tour. Une expertise vaut pour toute la pile. C'est le **meme predicat d'identite d'objet** que le
transfert entre membres du menu « Objets », qui y propage deja `0x8000` dans l'autre sens : deux
sites, une regle.

### Trois endroits ou la pseudo-C dit moins que le binaire

**`CONCAT22` cache deux ecritures de largeurs differentes.** Ghidra rend
`_DAT_005e8ffc = CONCAT22(joueur * 0x40 + DAT_005e8fa2, DAT_005e8fa0)`. Le binaire fait
`mov edx,[5e8fa0] ; mov [5e8ffc],edx` — une copie de **dword** — puis
`movzx ax,[edi+8] ; shl eax,6 ; add ax,[5e8fa2] ; mov [5e8ffe],ax` — une reecriture de la moitie
haute en **seize bits**. C'est cette forme-la qui est transcrite.

**Le drapeau `DAT_005e909a` est pose puis retracte.** L'ordre du binaire est
`cmp eax,0x4000400 ; mov [5e909a],dl ; jnz ; mov [5e909a],0` : il ecrit 1, puis revient dessus si la
valeur exacte tombe. Ce n'est pas la meme chose qu'un test compose, et la difference se verrait si
quoi que ce soit lisait entre les deux.

**Les trois seuils de valeur sont NON SIGNES** (`jbe`, pas `jle`) : plus de 0x15e, plus de 1000, plus
de 5000, sur le champ `+0x88` — celui dont `ComputeItemValue` part.

**Et une astuce de pointeur conservee** : la boucle de propagation tient l'ecart en octets jusqu'a
l'objet expertise et le decremente de `0x280` par tour, si bien que `curseur[ecart]` le designe
toujours sans recalcul. Transcrite telle quelle.

### Les deux verbes voisins : prets a ecrire, mais PAS ecrits

`s_VtSource_0056aec8` porte `0x0053DD20, 0x0053DEA0, 0x0053DF20` puis la souche vide : les verbes
**56, 57 et 58** forment un petit bloc, exactement comme les six balayeurs de la veille. Leurs
relevés sont la eux aussi, et leurs corps sont **triviaux** — une douzaine de stores chacun, ouvrant
le meme panneau avec `DAT_005de548` a 0 pour l'un et 1 pour l'autre.

**Je me suis arrete la, et pas par prudence de principe.** Le meme raisonnement qu'hier — « le
binaire les range ensemble, ferme le bloc » — ne s'applique pas : hier, tout ce que les trois corps
touchaient existait deja. Ici, les deux voisins ont besoin de **dix globales nouvelles**, et la carte
des symboles dit ou elles tombent :

| adresse | ou elle tombe |
|---|---|
| `0x005dff28`, `0x005dff5c` .. `0x005dff7c` | **a l'interieur de `s_Particles_005de824`** (6060 octets, `0x005de824`..`0x005dffd0`) |
| `0x005de548` | dans la fenetre de `s_Unknown_005de53c`, aux cotes de `s_Unknown_005de540` deja declare — scalaire, sans difficulte |
| `0x0056dc90` | rien de declare autour — scalaire, sans difficulte |

Les neuf premieres sont **le piege que le depot surveille** : les declarer en globales propres
donnerait deux noms pour une adresse, l'exact defaut que `MemoryLayoutTests` existe pour nommer. Il
faut d'abord trancher laquelle des trois lectures est la bonne — la taille de `s_Particles_005de824`
est fausse, ou ces champs vivent vraiment dans le bloc de particules, ou le nom du bloc est faux — et
c'est une decision deliberee, pas un raccourci de cinq minutes.

**Ce qu'il reste a faire quand ils tomberont** : trancher cette question, puis les deux corps
s'ecrivent en dix minutes. Les deux rendent **0** (`xor eax,eax` avant la sortie, verifie au
desassembleur pour les deux), donc le script continue.

### Non exerce

La chambre demande de conduire le personnage. Build vert, run de controle a 509 primitives par image.
Le corps a ete recoupe instruction par instruction contre `FUN_0053dd20.asm`, ce qui est l'oracle
disponible ici.

---

## 2026-08-25 (suite 20) — `s_Particles_005de824` tranche, et une erreur de lecture a corriger

### D'abord la correction : ce que j'ai affirme hier soir etait faux

J'ai ecrit que neuf des dix globales des verbes 57 et 58 tombaient « **a l'interieur** de
`s_Particles_005de824` (6060 octets) » et que les declarer recreerait le defaut d'aliasing. **Non.**

Le 6060 vient de la carte des symboles de `DataSymbols.md`, dont la colonne est
**l'ecart jusqu'au symbole suivant**, pas la taille de l'allocation. `CLAUDE.md` le dit en toutes
lettres — « avec son ecart au suivant » — et je l'ai lu comme une taille.

L'allocation reelle est **0x20 particules de 0x9c**, soit 4992 octets : `0x005de824`..`0x005dfba4`.
Les adresses en cause, `0x005dff28` et `0x005dff5c`..`0x005dff7c`, sont **900 octets plus haut**.
Aucun recouvrement, rien qui bloquait.

**La lecon, et elle vaut plus que l'erreur** : la colonne d'ecart de la carte ne prouve **jamais**
une appartenance. Elle dit seulement ou commence le voisin declare. Pour savoir si une adresse tombe
dans une allocation, il faut la **taille reelle de cette allocation**, cote C#. La verification tient
en quatre lignes et a ete faite cette fois pour les quatre adresses :

| adresse | symbole en dessous | taille reelle | distance | verdict |
|---|---|---|---|---|
| `0x005de548` | `s_Unknown_005de53c` | 1 (un `byte`) | 12 | dehors |
| `0x005dff28` | `s_Particles_005de824` | 4992 | 5892 | dehors |
| `0x005dff5c` | `s_Particles_005de824` | 4992 | 5944 | dehors |
| `0x0056dc90` | `s_StatusTimerMasks_0056d340` | 192 (0x30 dwords) | 2384 | dehors |

### Ce que la question a quand meme trouve : un vrai recouvrement, ailleurs

En verifiant l'etendue des particules, une autre paire s'est presentee. `FUN_0053dbe0` et
`FUN_0053dc30` (`Blaze.c` 87916 et 87938) tiennent une **file de sons differes** : un compteur en
`0x005dfb8c` qui reboucle a 0x20, et **32 enregistrements de 0x14 octets a partir de
`0x005dfb90`**. Trois confirmations independantes de sa taille — le masque `0xffffff1f` du compteur,
le `local_4 = 0x20` de la boucle de service, et le pas `* 0x14`.

Or `0x005dfb90` est **0x14 octets avant la fin nominale des particules** (`0x005dfba4`). Les deux
etendues se chevauchent d'exactement un enregistrement.

**Et c'est sans consequence, ce qui est le vrai resultat.** Le code des particules ne touche que les
offsets `0x00`..`0x27` de chaque entree de `0x9c` — verifie sur les deux boucles (`Blaze.c` 83468 et
83627, qui ecrivent au plus `piVar11[8]`) et sur les deux appelees qui recoivent un enregistrement
(`ProjectPoint` lit les trois entiers de position, `EmitParticle` lit `+0x20` et rien d'autre). Le
dernier octet qu'une particule ecrit est donc `0x005dfb2f`, **97 octets avant la file de sons**. Les
0x74 octets de queue de chaque entree sont du remplissage mort.

Reste que les deux **objets** se chevauchent sur le papier, ce qu'un editeur de liens ne fait pas :
l'une des deux lectures est donc imprecise — le plus probable etant que le tableau de particules
n'est pas un objet C de 32 x 0x9c mais une boucle qui parcourt 32 entites au pas d'entite dans une
zone plus courte. **Non tranche, et sans urgence** : rien ne casse tant que personne ne modelise la
file de sons comme une vue dans les particules. C'est precisement ce que la colonne d'ecart de 6060
inviterait a faire, d'ou la note ajoutee a `DataSymbols.md`.

### Les deux verbes

`FUN_0053dea0` (57) et `FUN_0053df20` (58) sont transcrits. Ils **posent un etat et levent un
drapeau**, rien de plus : le membre concerne, le mode, le bit `0x40` de `DAT_005de02c`, et la remise
a neuf de neuf dwords de curseur. Les deux rendent 0.

**Deux differences entre les jumeaux, et pas une de plus** : `DAT_005de548` vaut 0 pour l'un et 1
pour l'autre — c'est le mode que `FUN_0053e0a0` teste avant tout —, et `DAT_005dff64` recoit chez le
57 l'octet `+0x2bc` du bloc du personnage la ou le 58 y met zero. L'ordre des stores differe aussi,
sans consequence : aucun de ces champs n'est relu entre-temps.

Les onze globales restent dans `WorldMapScript.cs`, chez leur seul ecrivain transcrit, par la regle
de routage (4). Leur proprietaire reel est `FUN_0053e0a0`, qui les lit toutes et n'est pas
transcrite.

### Ce que le prochain plantage dira, et c'est previsible

Le bit `0x40` est exactement ce que le pas de monde teste (`WorldMapTick`, ligne 189) pour appeler le
crochet `PTR_FUN_0056aff0`. Ce crochet vaut `0x0053DF90` pour la zone d'index 0, il n'est pas
transcrit, et il leve deja en se nommant — son relevé est au depot
(`Legacy/Raw/Blaze/FUN_0053DF90.asm`). **Ouvrir ce panneau menera donc a lui, puis a
`FUN_0053e0a0`.** Ce n'est pas une regression : c'est la marche suivante, et elle etait deja ecrite
dans le message de levee avant aujourd'hui.

### Non exerce

Build vert, run de controle a 509 primitives par image, et les deux assertions rouges de
`MemoryLayoutTests` sont **les memes qu'avant** — aucune des onze globales nouvelles n'y apparait.

---

## 2026-08-25 (suite 21) — le crochet du coffre, et `FUN_0053e0a0` qui est **deux ecrans**

### Le crochet

`0x0053DF90` (260 octets, absent de `Blaze.c`) encadre l'appel au panneau : il pose le rectangle de
decoupe, appelle `FUN_0053e0a0`, puis dessine le curseur d'ecran par dessus. Sa queue est **mot pour
mot** celle de `DrawWorldMapMenuCursor` — memes huit octets de descripteur montes a la main
(`00 00 00 df 00 01 3f 00`), memes `0x80` / `0x70` / `0x1400` / `0x1249`. Seule difference : le
descripteur de sprite est ici une locale de pile.

Son seul appele manquant, `FUN_005412f0`, est transcrit avec lui : une liste d'icones dont chaque
entree est un dword de **quatre octets signes** — panneau de reference, image, decalage x, decalage
y. Les coordonnees se prennent sur le descripteur du panneau, ce qui fait suivre les icones quand il
bouge.

### `FUN_0053e0a0` n'est pas une fonction, c'est deux ecrans

1359 lignes de pseudo-C, et le premier test du corps est `DAT_005de548` — **le mode**, que les verbes
57 et 58 posent :

| mode | verbe | ecran | lignes | etat |
|---|---|---|---|---|
| 1 | 58 | **le coffre** — reprendre un objet depose | 195 | **transcrit** |
| 0 | 57 | **la carte memoire** — sauvegarder, charger, formater | 1150 | non transcrit, leve en se nommant |

Le mode 0 a son propre automate de sous-etats, ouvre des fichiers (`FUN_00504b40`), attend la carte
(`Ordinal_67` en boucle), et recopie trois chaines dans un tampon de message. C'est un ecran a part
entiere, du meme ordre que `SaveSlots.cs`. Le decouper du coffre n'est pas un arbitrage de confort :
c'est le corps lui-meme qui les separe des sa premiere ligne.

**Le coffre noir de la chambre est le mode 1.** C'est celui qui etait demande, et c'est celui qui est
fait.

### Onze globales declarees, onze qui ne devaient PAS l'etre

Le corps touche 39 adresses. La verification contre la carte des symboles — celle que la lecon de la
« suite 20 » impose de faire sur les **tailles reelles**, pas sur la colonne d'ecart — en a ecarte
onze d'un coup :

| adresse | ce que c'est vraiment |
|---|---|
| `0x005633b6` | `s_WorldMapDescriptor_005633b0 + 6` |
| `0x00568563` · `0x0056856b` · `0x00568ddc` · `0x00568df7` | `MenuDescriptor(0) + 0x33` · `+0x3b` · `+0x8ac` · `+0x8c7` |
| `0x00568dfc` · `0x00568e00` · `0x00568e02` · `0x00568e1b` | `MenuDescriptor(1) + 4` · `+8` · `+0x0a` · `+0x23` |
| `0x005696e3` | `MenuDescriptor(2) + 0x23` |

Ce ne sont pas des globales : ce sont des **champs des descripteurs d'interface deja modelises**, au
pas de `0x8c8`. Et les quatre offsets du descripteur 0 sont ceux que le moteur de texte nomme deja
ailleurs — `+0x33` le curseur, `+0x3b` les fleches de defilement, `+0x8ac` le curseur de texte,
`+0x8c7` les drapeaux. Les declarer aurait donne onze paires « deux noms pour une adresse ».

**La regle qui en sort** : quand une adresse tombe a moins de `0x8c8` au-dessus de `0x00568530`,
`0x005633b0` ou d'un autre descripteur, ce n'est presque jamais une globale — c'est un champ. Le
reflexe doit etre de soustraire avant de declarer.

Les onze restantes (`0x005dfe20` et sa plage, `0x0056dd34`) sont bien neuves ; aucune ne tombe dans
une allocation existante.

### Trois points ou la pseudo-C ne suffisait pas

**Les pas du prologue.** Ghidra rend trois indexations sur trois types de tableau differents. Le
binaire tranche : `imul eax,4Ch` en `0x0053e0e5` pour la vue d'emplacement (Ghidra disait `0x26`, sur
des `short`), `0x9c` pour l'emplacement de personnage, `shl edx,0Dh` pour le bloc du personnage.

**Un appel decoupe au mauvais endroit.** Ghidra rend
`FUN_0051bd50(fiche, personnage, panneau, 0)` puis `FUN_00544350(resultat)`. Le binaire empile les
quatre valeurs, appelle `FUN_0051bd50` avec **une seule** (`add esp,4` en `0x0053e323`), puis empile
le nom rendu et appelle `FUN_00544350` avec quatre (`add esp,10h`). Les trois arguments appartiennent
au **second** appel. Le commentaire de `BuildItemDisplayName_0051BD50` decrivait deja ce piege pour
son autre site ; c'est la deuxieme fois qu'il se presente.

**`0x0056dd34` est une chaine, pas un nombre.** Les octets `2a 00` releves dans le binaire, passes en
texte a `FUN_0051c120` : la chaine `"*"` d'un caractere que la liste intercale entre le nom de
l'objet et son prix.

### Un saut d'un mode a l'autre

La branche du coffre fait `goto LAB_0053e9fa`, dont la cible vit **dans le code du mode 0**. Trois
lignes — le champ `+6` du descripteur de carte a `0xffb0`, le bit `0x40` des drapeaux de monde
eteint, un bip. Elles sont recopiees sur place plutot que rendues par un saut entre deux ecrans que
tout le reste separe.

### Non exerce, et une question a poser

Build vert, run de controle a 509 primitives par image, `MemoryLayoutTests` inchange (les deux memes
assertions rouges, aucune des onze globales nouvelles n'y apparait).

**Le symptome signale — « le coffre n'ouvre pas l'interface » — n'est pas explique par ce qui
precede, et il faut le dire.** Si le script du coffre appelait bien le verbe 58, la version d'hier
aurait **plante** dans le crochet, pas « ne rien faire ». Deux lectures possibles : le trajet a ete
essaye sur une version anterieure aux verbes 57/58, ou le coffre passe par un tout autre chemin. Le
prochain essai tranche : soit le panneau s'ouvre, soit la levee nomme `FUN_0053e0a0, mode 0` — et
dans les deux cas on saura que le chemin est le bon.

---

## 2026-08-25 (suite 22) — le cliche du jeu d'origine dit que le coffre n'est PAS `FUN_0053e0a0`

Vincent fournit une capture du jeu officiel : `D:\Open Foresia Project\Tests\2. Coffre de stockage\legacy.jpg`.

### Ce que la capture montre

Le cadre habituel a trois panneaux — portrait / Nom / Classe / Element a gauche, une liste a droite,
une description en bas. Mais le panneau de droite porte **trois choses que rien de transcrit ne
dessine ensemble** :

1. **quatre lignes d'objet** avec pictogramme, dont une avec une quantite (« Petite potion 5 ») ;
2. **`ATT 7   DEF 0`** ;
3. **une rangee de huit colonnes** de pastilles chiffrees, etiquetees des huit **classes**
   (Gue / Pre / Vol / Mag / Cha / Elf / …) — donc « qui peut se servir de cet objet », et non des
   resistances elementaires.

Puis la description de l'objet designe : « Facile a manier pour un debutant. »

C'est donc **le contenu du coffre plus la fiche de l'objet choisi**. Un ecran a part entiere.

### Ce que ce n'est pas

**Ce n'est pas le mode 1 de `FUN_0053e0a0`**, que j'ai transcrit hier en le nommant « le coffre ».
Deux raisons, et la seconde suffit :

- **La mise en page ne colle pas.** Le mode 1 dessine **cinq** rangees, et chacune met le
  pictogramme, puis le nom, puis — a `+0x88` et `+8` plus loin, donc contre le bord droit — une
  etoile et un nombre a deux chiffres. La capture n'a rien contre le bord droit, et quatre lignes.
- **La logique ne colle pas.** Le mode 1 lit un **prix** dans `repertoire + 0xa8`, verifie que
  l'emplacement marque par la cle `0x2efb` en contient assez, le decremente, et **donne** l'objet par
  `GrantChestContents_00530F20`. On paie une monnaie pour recevoir un objet : c'est un **achat**,
  pas un depot.

Le corps est juste ; c'est son nom qui etait faux. Il faudra le renommer quand son ecran sera
identifie a l'ecran — pas avant, pour ne pas remplacer une erreur par une autre.

**Ce n'est pas non plus l'onglet d'equipement du menu de jeu**, verifie par comparaison directe :
notre onglet rend cinq emplacements (`- ARME -`, Armure, Bouclier, `-ACCESSOIRE1-`,
`-ACCESSOIRE2-`) et la phrase « Choisis ce que tu aimerais echanger. », sans `ATT`/`DEF` ni rangee de
classes. Meme cadre, contenu different.

### La lecon : le voisinage dans une table n'est pas une preuve de role

`FUN_0053e0a0` a ete pris pour le coffre parce que les verbes 56, 57 et 58 se suivent dans
`s_VtSource_0056aec8` et que le 56 est l'expertise d'objet — un voisinage, pas un fait. Le corps a
ete transcrit correctement ; l'erreur est d'avoir laisse le nom affirmer ce que rien n'avait etabli.
**Nommer d'apres la position dans une table est la meme faute que compter les ecrivains d'un symbole
au lieu d'une adresse** (2026-08-25, la lecon du matin).

### L'instrument qui tranche, et pourquoi il est permanent

Un script de zone ne se lit **ni dans `Blaze.c` ni au desassembleur** : les tables sont installees
par zone et les scripts vivent dans les donnees du disque. La seule facon d'aller du geste au corps
est de regarder le jeu le faire.

`ScriptOpAreaVerb` porte donc desormais une trace, sous `OPENFORESIA_TRACE_VERBS=1`
(`playtest.ps1 -Trace VERBS`) :

```
[VERBE] 3 -> 0x442F60  operande 0x0000
[VERBE] 10 -> 0x4441F0  operande 0x0000
```

Le trajet d'arrivee de l'auberge en allume exactement deux. **Ouvrir le coffre avec la trace posee
nomme la fonction a transcrire en une ligne** — c'est ce qui manque et c'est un run, pas une
enquete. La trace reste dans le code : la question reviendra a chaque decor nouveau.

### Ce qui est acquis malgre tout

Le crochet `0x0053DF90`, `FUN_005412f0` et le mode 1 de `FUN_0053e0a0` sont transcrits et le build
est vert. Le chemin verbe → drapeau `0x40` → crochet → panneau fonctionne de bout en bout ; c'est
seulement le panneau au bout qui n'est pas celui du coffre.

---

## 2026-08-25 (suite 23) — l'ecran du coffre est identifie ; le declencheur, non

### L'ecran : c'est l'onglet 1 de la fiche de personnage

Comparaison directe, capture contre capture, plutot que raisonnement :

| | cliche du jeu d'origine | notre onglet 1 (`playtest -Then …,START,RIGHT`) |
|---|---|---|
| cadre | portrait / Nom / Classe / Element, liste a droite, description en bas | **identique** |
| objets | Epee normale, Armure de cuir, Bouclier de cuir, Petite potion 5 | **les memes quatre** |
| valeurs | `ATT 7` `DEF 0` | **identiques** |
| rangee du bas | huit colonnes `Gue Pre Vol Mag Cha Elf Nai Lut` | **les memes huit** |

Ce sont donc bien **les huit classes** — qui peut se servir de l'objet — et non des resistances
elementaires, comme la premiere lecture du cliche le supposait.

L'ecran du coffre est donc l'onglet 1 du menu de jeu (`FUN_0052a850`, page 0), deja transcrit et deja
verifie a l'ecran. `FUN_0052fdc0`, qui dessine la moitie basse (valeurs, portees, nom, pictogrammes),
n'a que **deux sites d'appel dans tout `Blaze.c`**, tous deux dans le menu de jeu : il n'existe pas de
second dessinateur de cette fiche.

### Deux differences a garder en vue

1. **La liste.** Le cliche empile quatre lignes « pictogramme + nom » ; le notre pose quatre
   pictogrammes en rangee horizontale avec le nom de l'objet vise en dessous. Meme fiche, deux
   dispositions — donc deux etats du meme onglet, ou un etat que nous ne produisons pas.
2. **La description.** Le cliche affiche le texte de l'objet (« Facile a manier pour un
   debutant. ») ; le notre affiche le libelle de remplissage « Fenetre Objets ». C'est un defaut de
   notre rendu, independant du coffre, et il valait d'etre note.

### Le declencheur : toujours inconnu, et je ne l'invente pas

Vincent retente : **rien**. Ni panneau, ni levee. Ce « rien » se lit :

- ce n'est **pas** un verbe non transcrit (les onze de la table d'index 0 leveraient) ;
- ce n'est **pas** le verbe 57 (le mode 0 de `FUN_0053e0a0` leve) ;
- ce n'est **pas** une zone differente (`InstallEntityVTables` leve pour tout index non nul, et
  l'index est force a 0 tant que la zone est 0 — ce qui est le cas puisque rien ne leve) ;
- ce n'est **pas** un opcode de script non transcrit (ils levent aussi).

Restent trois possibilites, et aucune ne se departage par la lecture :

1. le script du coffre ne tourne pas du tout — l'interaction n'atteint pas l'interpreteur ;
2. il tourne et n'appelle que des opcodes deja transcrits, sans effet visible ;
3. il appelle un verbe **souche** (`0x00401FA0`), ce qui ne ferait rien dans l'original non plus —
   donc improbable, mais a exclure.

### L'instrument, complete

`RunScript` porte desormais sa propre trace, sous `OPENFORESIA_TRACE_SCRIPT=1`
(`playtest.ps1 -Trace SCRIPT`), a cote de `-Trace VERBS` :

```
[SCRIPT] index 8 entite 0x… genre 0x800000
[OPCODE] 4
[OPCODE] 16
```

Mesure de reference sur le trajet d'arrivee : 1 446 lancements, les opcodes 4, 16 et 3 en masse, et
**deux** appels a l'opcode 0. Les trois issues ci-dessus se distinguent donc en **un** run :

| ce que le journal montre au moment du geste | conclusion |
|---|---|
| aucune ligne `[SCRIPT]` nouvelle | l'interaction n'atteint pas l'interpreteur — chercher du cote du declenchement, pas du script |
| `[SCRIPT]` mais pas d'`[OPCODE] 0` | le script tourne ; lire quels opcodes pour savoir ce qu'il tente |
| `[OPCODE] 0` puis une ligne `[VERBE]` | le verbe est nomme, et avec lui la fonction a transcrire |

**Je n'ai pas cable le coffre sur l'onglet 1, et c'est deliberé.** Rien n'etablit encore que c'est
par la que l'original y va ; poser un lien maintenant reviendrait a coder le symptome attendu plutot
que le mecanisme, ce que la methode du depot exclut. L'ecran est identifie — le chemin qui y mene est
a une seule execution de distance.

---

## 2026-08-25 (suite 24) — le coffre : un appel supprime sur une supposition

### Le defaut

`EntityHandler_00450760` — le gestionnaire du coffre — portait ceci a son etat 4 :

```csharp
// Ghidra: (*(code *)PTR_FUN_0056afac)(param_1[0x1b], 0) - souche `ret` nu. Rien a emettre.
```

**L'appel avait ete supprime sur la foi d'une souche.** Il n'en est pas une :
`0x0056afac - 0x0056aec8 = 0xe4`, soit l'entree **57** de la table de verbes de zone, et pour la
zone 0 cette entree vaut `0x0053DEA0` — le verbe 57, celui qui leve le drapeau `0x40` et fait ouvrir
le panneau.

Le coffre s'ouvrait donc visuellement (le couvercle descend de `0x23` pendant 30 images), le compte
arrivait a son terme, et a l'instant precis ou le panneau devait apparaitre, on ne faisait rien.

**La regle qui manquait** : une entree de table n'est une souche que **pour une zone donnee**. Ces
tables sont installees par zone ; ecrire « souche `ret` nu, rien a emettre » fige pour toutes les
zones ce qui n'a ete verifie que pour une. La correction reprend la discipline des autres tables —
un cas par fonction transcrite, une levee nommee pour le reste.

### Comment il a ete trouve : cinq sondes, chacune posee parce que la precedente ne suffisait pas

Aucune lecture statique ne pouvait donner ce resultat : le declencheur est un script du disque, la
table est installee par zone, et le symptome etait « rien ne se passe ». Il a fallu instrumenter, et
chaque sonde a corrige une conclusion trop rapide :

| sonde | ce qu'elle a montre | l'erreur qu'elle a corrigee |
|---|---|---|
| `-Trace VERBS` | les verbes appeles par l'opcode 0 | « le coffre appelle un verbe » — il n'en appelle aucun |
| `-Trace SCRIPT` | chaque script et chaque opcode | « le monde est gele » — non, il tourne |
| `[ETAT]` dans la boucle | pas, drapeaux, position | « aucun script = simulation arretee » — une salle sans script donne le meme silence |
| `[TOUCHE]` | le mot de boutons a chaque changement | « l'appui n'est pas vu » — il l'est, et la boite s'ouvre |
| `[CHOIX]`, `[ALU]`, `[SAUT]` | le choix latche, le test, le verdict | « la reponse Oui ne passe pas » — elle passe, le calcul est juste |

**La sonde `[ETAT]` a d'abord echoue silencieusement**, et c'est instructif : elle comptait les
**appels** a `RunWorldMapTick`. Or cette fonction n'est pas appelee une fois par image — elle
contient elle-meme la boucle d'images et n'est entree qu'une fois par chargement de zone. Une sonde
a son entree ne s'allume qu'une fois. Elle est desormais **dans** la boucle.

### Et une etiquette a inverser

`FUN_0053e0a0` a deux modes. J'avais nomme le mode 0 « carte memoire » parce qu'il appelle des
primitives de fichier (`FUN_00504b40`, `FUN_00504c20`, `FUN_00512900`), et le mode 1 « le coffre »
parce que les verbes 56/57/58 se suivent. **Les deux etaient faux, et le jeu l'a tranche** :

| mode | verbe | ce que c'est |
|---|---|---|
| 0 | 57 | **le coffre de stockage** — c'est la que le coffre noir arrive. La persistance sur fichier est son magasin, pas une sauvegarde de partie ; il recopie des blocs d'objets par `FUN_005352e0` |
| 1 | 58 | **une boutique** — cinq rangees avec un prix contre le bord droit, prelevé sur l'emplacement `0x2efb`, l'objet rendu par `GrantChestContents_00530F20` |

**J'ai conclu deux fois sur ce que le corps APPELLE sans regarder ce qu'il DESSINE.** Les primitives
de fichier ne disent pas « sauvegarde » ; elles disent « quelque chose est range quelque part ».

### Etat

Le chemin va maintenant de bout en bout et s'arrete en se nommant :

```
coffre → etat 4 → PTR_FUN_0056afac → verbe 57 → drapeau 0x40 → crochet 0x0053DF90
       → FUN_0053e0a0 mode 0 → leve
```

Reste a transcrire le mode 0 : `Blaze.c` 88221..89333, 1150 lignes. Son prologue, ses onze globales
et ses onze vues de descripteur sont deja en place — c'est le corps qui manque.

### Note d'allocation relevee au passage

Le journal signale : `BuildMenuDescriptors alloue bas APRES le demarrage, hors de la portee de la
relance`. Les quatre descripteurs d'interface sont alloues paresseusement ; a ajouter a la liste
pre-reclamee de `TryPrepareDisplayListArena`.

---

## 2026-08-25 (suite 25) — le mode 0 : le socle est pose, le corps ne l'est pas

Le mode 0 de `FUN_0053e0a0` a ete attaque d'un bloc, comme demande. **Le socle est en place et
compile ; le corps ne l'est pas, et je ne le declare pas fait.**

### Ce qui est pose

**Les trente-neuf globales et les huit tables de donnees.** Chaque table est relevee dans le binaire,
pas devinee :

| symbole | contenu |
|---|---|
| `PTR_DAT_0056dc20` | quatre tables d'octets — l'identifiant de peripherique par (peripherique, ligne) |
| table de `0x0056dc58` | quatre listes dont le **premier octet est le compte**, ce qui borne le curseur |
| `DAT_0056dc68` | dix positions d'ecran, deux `short` chacune |
| `DAT_0056dcb0` | dix icones de cadre, quatre octets signes chacune |
| `DAT_0056dca0` | deux icones de bandeau |
| `DAT_0056dce0`, `DAT_0056dce8` | deux enregistrements d'atlas de huit octets |
| `DAT_00566be8` | les dix puissances de dix, de 1e9 a 1 |

**Quatre adresses n'ont pas ete declarees** — meme verification que pour le mode 1 : `0x005e8f92`
tombe dans `s_EntityDecodeTable_005e8f00` (0x1000 octets), `0x005e9322` et `0x005fafb0` sont des vues
d'arene, et `0x00568536` / `0x0056853a` / `0x005696c4` / `0x005696c6` sont des champs de
descripteurs.

### Une branche morte, etablie au desassemblage

Le prologue teste `DAT_0056dc90 < 0` **trois fois**, et le premier bloc met cette globale a zero
(`mov [56DC90h],esi` en `0x0053e963`, avec `esi` a 0 — verifie par les `cmp` voisins). Les deux tests
suivants, et le `goto switchD_0053eaef_caseD_a` qui en depend, sont donc **morts dans tous les cas** :
si le premier bloc a tourne la valeur vaut 0, et s'il n'a pas tourne elle etait deja positive.

C'est une simplification reelle pour la suite — mais elle n'a ete acquise qu'au desassemblage, et
c'est le point suivant.

### Pourquoi le corps n'est pas ecrit

Les 1110 lignes restantes sont un automate de vingt etats avec des sauts d'un cas a l'autre
(`goto LAB_005402b5`, `joined_r0x005402d1`, `goto LAB_00540dab`), trois chemins qui **ecrivent le
fichier de sauvegarde** (`FUN_00504c20` y ecrit 0x2000 + 0x1f00 + 0x100 octets, recalcule la somme de
controle de `+0x1ffc` et reecrit le bloc d'icone de `0x4000`), et une large duplication de ce que
`SaveSlots.cs` fait deja.

La branche morte ci-dessus montre le probleme : **la pseudo-C ne suffit pas ici**. Il a fallu le
desassemblage pour trancher un seul point de flot sur les soixante premieres lignes. Transcrire les
1110 restantes a la meme rigueur est le travail d'une session, pas d'une passe — et livrer 1300
lignes qui compilent sans cette verification, sur des chemins qui **ecrivent la sauvegarde du
joueur**, serait exactement ce que `Strategy.md` interdit.

### Ce qu'il reste, concretement

L'ordre conseille, du plus sur au plus risque :

1. **la queue d'affichage** (apres `switchD_0053eaef_caseD_a`, ~50 lignes) — elle dessine le curseur
   de peripherique, la vignette du personnage, les deux listes d'icones et les quatre panneaux.
   Elle seule ferait **apparaitre** l'interface ;
2. **le cas 3** (~300 lignes) — la liste des objets ranges, sa fenetre de cinq lignes et sa fiche ;
3. **les cas 0, 2, 4, 6, 7, 9, 0x5f a 0x62, 99 et les negatifs** — la navigation et les messages ;
4. **les cas 1, 5, 8 en dernier** — les trois chemins de fichier, a verifier au desassemblage et a
   exercer sur une copie de `save\`.

Le prologue commun, les onze vues de descripteur du mode 1 et les trente-neuf globales de ce
paragraphe sont deja en place : le point 1 s'attaque directement.

---

## 2026-08-25 (suite 26) — la queue d'affichage du coffre

Le prologue de navigation et la **queue d'affichage** du mode 0 sont transcrits. L'automate des
vingt etats ne l'est pas : chacun leve en se nommant.

### Ce que la queue dessine

C'est elle qui produit le cadre du cliche de reference : le curseur de peripherique, le portrait, la
liste d'icones du bandeau, quatre lignes de texte et **quatre panneaux**.

**Le quatrieme descripteur n'est pas une globale.** `DAT_00569f88` est a `0x1a58` de
`0x00568530`, soit exactement **trois** descripteurs de `0x8c8` : c'est `MenuDescriptor(3)`. La
boucle finale les dessine tous les quatre, du dernier au premier — la ou le mode 1 n'en dessine que
deux. C'est la difference de cadre la plus visible entre les deux ecrans, et elle explique pourquoi
le cliche a trois cadres imbriques.

**Le curseur de peripherique ne se dessine qu'a l'etat 4.** La condition de l'original est
`(DAT_0056dc90 < 0) || (DAT_005dff7c == 4)`, et son premier terme est celui dont la branche morte a
ete etablie a la suite 25. Sa position se prend en deux temps : la liste de lignes du peripherique
donne un indice, cet indice donne un couple (x, y) dans la table de `0x0056dc68`, decale de `0x86`
et `0x48`.

**Le quatrieme libelle est anime**, ce qui ne se voit pas dans la pseudo-C au premier regard :
`DAT_005e909e` recoit `(compteur d'images / 4) % 3` ajoute a `+0x151` fois trois. Trois images qui
tournent, pas un index fixe.

### Deux points de type

`DAT_005dff7c` est **signe** : l'automate a des etats `-1` a `-5` en plus de `0` a `99`. La globale
etant declaree `uint` avec les huit autres dwords de curseur, le `switch` porte sur `(int)`.

`DAT_005dff80` et `DAT_005dff84` sont deux globales nouvelles, distinctes des neuf dwords de curseur
qui s'arretent a `0x005dff7c` : la valeur de l'etat a l'image precedente, et le drapeau « l'etat vient
de changer » que leur comparaison leve.

### L'automate, etat par etat

Il leve avec le numero de l'etat, et l'ordre conseille est dans le message lui-meme. **Le premier a
ecrire est l'etat 2** : c'est celui que le prologue pose en entrant, et c'est la lecture du magasin.

Une precision utile : **l'original n'a pas de `default`** dans ce `switch`. Une valeur hors de sa
liste ne fait rien du tout, et la transcription le reproduit — la levee ne couvre que les vingt etats
qu'il traite vraiment.

---

## 2026-08-25 (suite 27) — l'etat 2 du coffre : la lecture du magasin

C'est l'etat que le prologue pose en entrant, donc le premier que le panneau execute.
`Blaze.c` 88423..88543, transcrit en quatre corps : l'etat lui-meme et ses trois etapes.

### Ce qu'il fait

1. **Il met les objets du joueur de cote** a `MainMem + 0x1bc000`. C'est cette copie que l'etat 9
   remet en place si on ressort sans rien ranger — le seul filet de securite du panneau, et il faut
   le savoir avant de toucher aux etats de sortie.
2. **Il lit le fichier du coffre** et ne le prend que si sa somme de controle correspond a celle du
   bloc du joueur (`+0x1ffc` contre `MainMem + 0x1b38fc`). Sinon tout est saute et on tombe sur le
   message « coffre vide », etat 9.
3. **Il fusionne les piles identiques** dans l'inventaire du joueur.
4. **Il envoie vingt-deux vignettes en VRAM**, deux transferts par emplacement.
5. **Il recense les deux cotes** et passe a l'etat 3 — sauf coffre vide.

### Le predicat d'identite, pour la troisieme fois

La fusion cherche un objet dont les **seize premiers octets** et la cle de `+0x86` concordent, et
dont l'octet de `+0x13` porte `0x30`. Les seize octets sont **exactement** le predicat que
l'expertise (verbe 56) et le transfert entre membres du menu « Objets » emploient deja. Trois sites,
une regle — et c'est maintenant assez pour dire que c'est *la* definition d'« objet identique » du
jeu, pas une coincidence.

Le total fusionne est **borne a 99 par deux ecritures d'octet** (`99` puis `0`), pas par une
saturation de `short`. La difference se verrait sur une pile de plus de 255.

### Deux ecarts declares

**Les deux rectangles VRAM sont des locales de pile dans l'original**, dont l'adresse est rangee dans
deux globales (`DAT_005dfef4`, `DAT_005dfefc`). Ils sont ici deux blocs propres de huit octets : meme
comportement observable, sans le pointeur pendant que l'original laisse derriere lui en sortant.
Reproduire le pendouillant serait du comportement indefini en C#, pas de la fidelite.

**La poignee de fichier** est un `int` compare a -1 dans l'original ; `RetryOpenCardFile` rend un
`IntPtr`, donc c'est ce type qui est garde et le test devient `(nint) >= 0`.

### Un croisement de listes, conserve

La derniere boucle parcourt la table des emplacements du **coffre** (`DAT_005dff40`) mais son compte
de tours est celui de l'inventaire du **joueur** (`+0x2c0`). Les deux listes n'ont aucune raison
d'avoir la meme longueur, et rien ne borne la lecture. C'est ce que dit le binaire ; conserve comme
les autres defauts d'origine, et a verser dans `legacy-bug.md` le jour ou une partie le fait sortir
de ses seize octets.

### Ce qui reste

L'etat 3 — la liste des objets ranges, sa fenetre de cinq lignes et sa fiche, ~300 lignes. C'est le
plus gros des dix-neuf restants, et c'est lui qui remplit le cadre que la queue d'affichage dessine
deja.

---

## 2026-08-25 (suite 28) — l'etat 3 du coffre : la liste et la fiche

Le plus gros des vingt etats, `Blaze.c` 88545..88889. C'est lui qui **remplit** le cadre que la queue
d'affichage dessine deja : quatre lignes « pictogramme + nom », les valeurs, les deux portees et la
rangee des huit classes — exactement ce que montre le cliche du jeu d'origine.

### Un `CONCAT31` qui n'existe pas

Les quatre emissions de sprite de la boucle recoivent un `extra` que Ghidra rend par

```c
CONCAT31(uVar13, bVar3) & 0xffff000f | (CONCAT31(..., bVar3) & 0xffff0010) << 4
```

Le desassemblage en `0x0053f93a` montre la vraie sequence, et elle est propre :

```
mov al,[ecx+5DFF40h]     ; l'emplacement
xor cx,cx                ; efface SEIZE bits — la moitie haute reste
mov dl,al
and al,10h
and dl,0Fh
mov cl,dl                ; (emplacement & 0x0f)
xor dx,dx                ; idem
mov dl,al
shl edx,4                ; (emplacement & 0x10) << 4
or  ecx,edx
```

Les deux `xor cx,cx` / `xor dx,dx` de **seize bits** laissent la moitie haute des registres intacte,
et c'est cette moitie-la que Ghidra a rendue en `CONCAT31`. Elle ne participe pas :
`DrawSpriteVariant` ne lit que les seize bits bas de son `extra`. Le calcul reel est donc
`(emplacement & 0xf) | ((emplacement & 0x10) << 4)`.

**C'est la troisieme fois de la journee que la pseudo-C fabrique un artefact qu'un `xor` de seize
bits explique.** Le reflexe a garder : un `CONCAT` sur une valeur qui devrait etre un simple octet
signale presque toujours un effacement partiel de registre, pas un calcul.

### Trois idiomes de MSVC rendus par ce qu'ils font

Le nom de chaque rangee est compose sur la pile, en un flux d'etiquettes
`0c 04 <x> <y> 14 <couleur>` suivi du texte — le meme idiome que `DrawItemDetail_0052FDC0`. Ghidra
en rend trois boucles cryptiques : ce sont un `strlen`, un `strcpy` et un `strcat` **mis en ligne**
par le compilateur (le `uVar7 = 0xffffffff ; do … while (c != 0) ; uVar7 = ~uVar7` est la signature
du `strlen` de MSVC). Ils sont transcrits par leur effet, pas par leur forme.

La couleur de la ligne suit la meme logique : `(-(garde != 0) & 6) + 1` est une facon sans
branchement de choisir entre 1 et 7.

Et la ligne visee est dessinee **quatre fois**, decalee d'un pixel a chaque tour : c'est le liseré
qui l'entoure. La condition est `DAT_005dfe2c == 0x800`, soit la moitie basse du mot de couleur que
la branche « non visee » vient de poser — un test sur une valeur que le code a lui-meme ecrite deux
lignes plus haut.

### La sortie

Valider bascule le drapeau « garde » de la ligne visee, avec **trois refus** : l'objet est deja
equipe (il figure dans les cinq octets de `+0x2b0`), on tient deja dix objets, ou l'on essaie de
rendre alors qu'on n'en tient aucun.

Annuler ne quitte que si l'echange tient en douze emplacements, et **va directement a l'etat 9 si
rien n'a change** — meme compte et memes quantites que la copie de sauvegarde posee par l'etat 2.
C'est ce qui evite de reecrire le fichier pour rien.

### Ce qui reste

Dix-huit etats, dont les trois d'ecriture de fichier (1, 5, 8). Le panneau devrait maintenant
s'afficher et se naviguer ; il ne peut pas encore **valider** l'echange, l'etat 4 qui suit
l'annulation n'etant pas ecrit.

---

## 2026-08-25 (suite 29) — l'etat 4 du coffre : trois affectations

Le plus court des vingt : `DAT_005dff7c = 8`, `DAT_005dff84 = 0`, `DAT_005dff18 = 0`.

C'est **l'aiguillage vers l'ecriture**. L'etat 3 y arrive en annulant — quand l'echange a change
quelque chose — et il repart aussitot vers l'etat 8, celui qui ecrit le fichier.

**La remise a zero des deux drapeaux n'est pas decorative.** `DAT_005dff84` est le drapeau « l'etat
vient de changer » que le prologue leve a chaque transition ; l'etat 8 le teste en entrant et, s'il
le trouve leve, se contente de le consommer sans rien faire. En le posant a zero ici, l'etat 4
garantit que l'etat 8 entrera par sa branche de travail des le premier tour, et non un tour plus
tard. Meme raisonnement pour `DAT_005dff18`, qui remet l'attente de peripherique a l'arret.

C'est le genre de detail qu'on aurait pu prendre pour du bruit : trois affectations sans logique.
Elles sont la synchronisation de deux automates.

### Etat des vingt

| ecrit | reste |
|---|---|
| 2, 3, 4 | 0, 1, 5, 6, 7, 8, 9, 0x5f, 0x60, 0x61, 0x62, 99, -1, -2, -3, -4, -5 |

Le panneau s'affiche et se navigue ; il ne peut pas encore conclure, l'etat 8 (l'ecriture) n'etant
pas ecrit. L'ordre conseille reste : la navigation et les messages d'abord, les trois etats de
fichier (1, 5, 8) en dernier et sur une copie du dossier de sauvegarde.

---

## 2026-08-25 (suite 30) — la panne d'allocation basse du coffre, et six etats de plus

### La panne, signalee en jeu

```
No free block of 0x3000 bytes below 0x01000000 for BuildMenuDescriptors.
Libre sous le plafond : 0x19B000 octets au total, plus grand intervalle 0xF000.
```

**C'est la sixieme fois que la meme lecon se presente**, et le journal l'annoncait deja : les quatre
descripteurs d'interface (`0x00568530`, 4 x `0x8c8`) etaient pris **paresseusement**. Tant qu'on
ouvrait le menu de jeu tot dans la partie, ils tombaient dans une fenetre encore large ; le coffre,
lui, s'atteint apres plusieurs minutes, et la fenetre a eu le temps de se fragmenter — 0x19B000
libres mais seulement 0xF000 d'un tenant.

Ajoutes a la liste pre-reclamee de `TryPrepareDisplayListArena`. Apres correction, le releve de
demarrage passe de `0xF000` a **`0x640000`** de plus grand intervalle.

**La regle, une bonne fois** : *le moment ou une allocation basse est prise fait partie de sa
definition.* Une `AllocLowZeroed` qui echoue au demarrage rend `false` et `Program.cs` relance un
processus neuf ; la meme, en cours de partie, traverse toute la pile et tue la session.

### Six etats

| etat | ce qu'il fait |
|---|---|
| 6 | pose le premier message de confirmation, puis surveille l'annulation |
| 7 | idem, mais ne parle que si `DAT_005e909f` est nul — sinon il termine le transfert et file a l'etat 4 |
| 9 | **la sortie** — rend la main au monde et remet les objets si rien n'a ete range |
| 0x5f | l'abandon **avec** fin de transfert |
| 0x60 | l'abandon sans |
| 99 | la reprise apres echec : remet le magasin d'avant la tentative et repart a l'etat 4 |

**Trois queues partagees, pas trois etats.** Ghidra rend les cas 6 et 7 avec `LAB_005402b5`,
`LAB_005402bd` et `joined_r0x005402d1` entrelaces. Ce sont deux corps distincts qui partagent une
queue d'annulation ; ecrits comme tels, ils tiennent en trois methodes lisibles au lieu d'un
enchevetrement de sauts.

**Le `0x5f` tombe dans le `0x60`**, `break` manquant compris. Ce n'est pas un oubli : les deux etats
existent precisement pour distinguer « il y avait un transfert en cours » de « il n'y en avait
pas », et le premier ne fait qu'ajouter la fin de transfert avant de faire le travail du second.

**L'etat 7 est le seul a s'arreter tant que l'etat n'a PAS change** (`DAT_005dff84 == 0`), a l'envers
de tous les autres. Transcrit tel quel.

### Le filet de securite se tend a l'etat 9

`DAT_005dff20` est leve par l'etat 8 apres une ecriture reussie. Tant qu'il est a zero, l'etat 9
recopie par-dessus l'inventaire la sauvegarde posee par l'etat 2 a `MainMem + 0x1bc000`. C'est **le**
mecanisme qui garantit qu'un coffre referme sans rien ranger ne mange pas les objets du joueur, et il
tient a ce seul octet.

### Etat des vingt

| ecrit | reste |
|---|---|
| 2, 3, 4, 6, 7, 9, 0x5f, 0x60, 99 | 0, 1, 5, 8, 0x61, 0x62, -1 a -5 |

Les trois etats de fichier (1, 5, 8) restent pour la fin, et sur une copie du dossier de sauvegarde.

---

## 2026-08-25 (suite 31) — les etats 0x61, 0x62 et les cinq negatifs

Sept etats de plus, tous courts, tous consacres au **peripherique** : l'attendre, le voir echouer,
revenir a son choix.

### Les deux automates se parlent par une seule globale

Les etats `0x61` / `0x62` et `-3` / `-2` testent tous `DAT_005dff24 == -3`. Ce n'est pas un code
d'erreur invente : c'est ce que rend `FUN_00504820`, et l'automate d'attente de peripherique
(`DAT_005dff18`) le range dans cette globale. **Les deux automates de la fonction communiquent par ce
seul entier**, et c'est ce qui les rend lisibles une fois qu'on l'a vu — sans ca, ces quatre etats
semblent tester une valeur qui vient de nulle part.

### `DAT_0056dc90` a -1 est le seul chemin de retour

Trois de ces etats (`-4`, `-2`, `-1`) reposent `DAT_0056dc90` a `-1`. C'est tout leur objet : le
prologue de `RunDepotMode0` ne s'execute que lorsque cette globale est negative, donc la remettre a
-1 **rearme le choix de peripherique au tour suivant**. C'est le seul chemin de retour vers ce choix
une fois qu'on en est sorti, et il eclaire retrospectivement la branche morte de la suite 25 : le
prologue est bien un etat a part, entre et quitte par cette valeur.

### Deux enchainements sans `break`, et le meme motif

`-5` tombe dans `-4` exactement comme `0x5f` tombe dans `0x60` : deux etats pour distinguer « il y
avait un transfert en cours » de « il n'y en avait pas », le premier n'ajoutant que les deux appels
de fin de transfert. **Le meme motif, deux fois dans la meme fonction** — ce n'est pas un oubli de
`break`, c'est une facon de faire.

L'etat `-2` rearme pourtant le choix **sans** passer par `-4` : il refait les deux appels lui-meme.
Le code est duplique dans l'original, pas factorise, et il l'est reste ici.

### Etat des vingt

| ecrit | reste |
|---|---|
| 2, 3, 4, 6, 7, 9, 0x5f, 0x60, 0x61, 0x62, 99, -1, -2, -3, -4, -5 | **0, 1, 5, 8** |

Les quatre qui restent sont l'entree (0) et les **trois ecritures de fichier**. Ce sont les seuls a
toucher le disque, et ils sont gardes pour la fin, a exercer sur une copie du dossier de sauvegarde.

---

## 2026-08-25 (suite 32) — les etats 0, 1, 5 et 8 : LE COFFRE EST COMPLET

Les quatre derniers etats, dont les **trois qui touchent le disque**. `FUN_0053e0a0` ne porte plus
aucune levee : ses deux modes, ses vingt etats et son automate d'attente de peripherique sont
transcrits.

### Trois choses a ne pas se tromper dans l'ecriture

**La somme de controle porte sur des octets SIGNES.** Ghidra la rend `(int)(char)*p` sur `0x1ffc`
octets. Un octet a `0x80` y compte pour **-128**, pas 128. Se tromper la produirait un fichier que
le jeu refuserait de relire, sans que rien n'indique pourquoi.

**Les trois ecritures sont combinees par OU binaire** et testees une seule fois : il suffit qu'un bit
de signe apparaisse dans l'un des trois retours pour que l'ensemble soit declare en echec. Ce n'est
pas trois tests, c'est un.

**Le drapeau de succes est pose en dernier.** `DAT_005dff20 = 1` n'arrive qu'apres la reussite des
quatre acces disque, et c'est ce drapeau que l'etat 9 consulte pour savoir s'il doit remettre les
objets d'avant. Le poser plus tot rendrait la sortie destructrice — c'est le point le plus dangereux
de la fonction, et il tient a l'ordre de deux lignes.

### La correction d'equipement, qui se serait vue tout de suite

Le compactage vers l'inventaire ne se contente pas de deplacer les objets : il reecrit les cinq
octets de `+0x2b0` qui designaient leurs **anciens** indices. Sans cette correction, refermer le
coffre desequiperait ce que le joueur portait. C'est un des rares endroits ou un oubli se serait vu
immediatement en jeu plutot que des mois plus tard.

### Deux enchainements de plus sans `break`

Le cas `0` de l'etat 5 tombe dans son cas `-1` quand l'ouverture echoue — troisieme occurrence du
motif apres `0x5f`/`0x60` et `-5`/`-4`. **Trois fois dans la meme fonction** : c'est la facon de
faire de ce code, pas un accident.

Et le message d'absence de peripherique est ecrit **deux fois a l'identique** dans l'original (cas 0
et cas -1 de l'etat 1). Factorise ici en `ComposeDepotDeviceMessage`, parce que c'est une unite
reellement independante — trois chaines de glyphes de seize bits collees bout a bout — et non un
`LAB_` promu en methode.

### Une duplication assumee

La moitie basse de l'etat 8 est **la meme sequence que l'ecran de sauvegarde du menu**
(`BuildAndWriteSaveBlock`) : meme mot sentinelle `0x6b82`, memes huit banques, memes huit reperes,
meme capture d'icone, meme ecriture en trois morceaux. Le compilateur l'a emise deux fois ; les deux
corps restent distincts ici parce qu'ils le sont dans le binaire et que leurs globales de travail
different.

### Etat

| ecrit | reste |
|---|---|
| les vingt etats, l'automate d'attente, le prologue, la queue | **rien** |

**Non exerce.** Le panneau n'a jamais tourne : aucun run de l'agent n'atteint le coffre. Et surtout,
**les chemins d'ecriture n'ont jamais ete executes**. Le premier essai doit se faire sur une copie du
dossier `save\` — c'est la seule precaution qui compte ici.

---

## 2026-08-25 (suite 33) — `& 0xfffffffN` suivi d'un `+ N'` n'est pas un masque

Signale en jeu : la pastille de la classe autorisee sort en **rectangle blanc avec un libelle
bariole**, au lieu de sa couleur — dans la fenetre d'objets (F1) **comme** dans celle du coffre.

### Le calcul

Ghidra rend la teinte des huit pastilles de classe sans branchement :

```c
(-(uint)((1 << (i & 0x1f) & flags) != 0) & 0xfffffff9) + 9
```

Lu vite, `& 0xfffffff9` ressemble a un masque et `+ 9` a un decalage, ce qui donne « 0 quand le bit
est leve, 9 sinon ». **C'est faux.** `0xfffffff9` vaut **-7**, et l'expression est une soustraction
deguisee :

| bit | valeur |
|---|---|
| leve | `0xfffffff9 + 9` = **2** |
| eteint | `0 + 9` = **9** |

Deux, pas zero. Cette valeur est le champ d'attributs que `DrawSpriteVariant` transforme en page
d'atlas ; avec 0 la pastille allait chercher ses pixels ailleurs.

### Deux sites, une seule faute

Le meme calcul existe **deux fois** dans le jeu : `FUN_0052fdc0` (la fiche d'objet du menu, ecrite le
2026-08-25 au matin) et `case 3` du coffre (ecrit le soir). Les deux portaient la meme lecture
fautive, la seconde parce qu'elle a repris la premiere. Corriger l'une sans l'autre aurait laisse le
defaut visible dans un ecran sur deux — c'est le signalement de Vincent, qui l'avait vu **aux deux
endroits**, qui a evite ca.

### Le reflexe

**Un `& 0xfffffffN` suivi d'un `+ N'` n'est jamais un masque : c'est un `SETcc` deguise en
arithmetique.** Calculer les deux cas avant d'ecrire le ternaire, systematiquement. Le meme idiome
existe ailleurs dans le depot (`UiText.cs`, `((0 < x) - 1 & 0xfffffffd) + 2`) ; celui-la est
transcrit **litteralement**, sans simplification, et se trouve donc juste — la simplification est
exactement ce qui introduit l'erreur.

### Verifie a l'ecran

Onglet 1 de la fiche de personnage, epee du Guerrier : `Gue` sort en blanc franc, les sept autres en
gris, et le rectangle blanc a disparu. Conforme au cliche du jeu d'origine
(`Tests. Coffre de stockageegacy.jpg`), ou la classe autorisee est la seule en clair.

---

## 2026-08-26 — le menu rapide : `FUN_00530070`, ses deux etages et ses deux plantages

`TickQuickMenu_00530070` (`Screens/State03/WorldMapTick.cs`) transcrit `FUN_00530070` en entier.
C'est le pilote de **l'anneau au-dessus du personnage** que `DrawSpeechOverlay_005309B0` dessinait
deja : espace l'ouvre, les fleches parcourent, la validation agit.

### Ce que la fonction est vraiment

Le nom qu'elle portait — « le recul apres un coup recu » — etait faux. Il venait de sa position dans
`FUN_0050bf20`, entre deux traitements de degats. Son corps ne parle que de menu.

Trois etages, dans l'ordre du corps :

1. **ouvrir / fermer / valider l'etage courant**, selon le bit `0x80` de `personnage[0x2be]` ;
2. **le curseur d'entree** et l'action, par un aiguillage sur le **genre** de la categorie ;
3. **la queue commune, atteinte par tous les chemins** : le curseur de categorie, puis le recalcul
   de la liste d'entrees que la nouvelle categorie propose.

L'etat tient dans quatre octets du bloc par personnage : `+0x2be` (categorie en bas de quartet,
`0x80` = « anneau ouvert sur une categorie »), `+0x2bf` (entree choisie), `+0x2d0` (nombre
d'entrees) et `+0x2d1…` (la liste elle-meme, au plus dix indices d'objet porte).

Les dix genres viennent de `DAT_0056a851` : 0–5 les cinq emplacements d'equipement, 6 les sorts,
7 et 8 deux choix fixes, 9 le passage de la main a un autre membre.

### `DAT_0056a850` et `DAT_0056a851` sont **une seule table**

Le `TODO(REVERSE)` pose le 2026-08-22 disait : « le pas 9 et un second index allant jusqu'a 15 se
contredisent, les rangees se chevauchent — non tranche ». **Il est tranche, et il n'y a pas de
chevauchement.** La table fait neuf rangees de neuf octets ; le premier octet de chaque rangee est
le **nombre de categories** de la classe (7, 7, 6, 7, 7, 7, 7, 7), les huit suivants sont les
genres. `DAT_0056a850` est ce compte, `DAT_0056a851` est le premier genre — deux symboles Ghidra
pour une table.

Consequence directe : la queue ramene `0x2be & 0xf` sous le compte a chaque changement, donc
l'index reste sous 7 et **une rangee ne deborde jamais sur la suivante**. Consequence indirecte :
les deux `0xff` de la table sont **inatteignables** (celui de la rangee 2 est au huitieme genre
alors que le compte vaut 6 ; la rangee 8 est hors de portee de `classe & 7`). La note d'adjacence
de `s_IconTables_0056aaa0` reste juste comme description de l'indexation, mais son cas `-1` ne se
produit pas.

Allouees ensemble, sous `s_MenuLayout_0056a850` (81 octets) ; `s_SpeechIconIndex_0056a851` est
devenu une **propriete calculee** `=> base + 1`, pour que l'octet de compte garde une adresse.

### Deux fidelites qui se lisent mal, gardees telles quelles

**Le test « le curseur a-t-il bouge » du genre 6 lit un registre ecrase.** Ghidra charge `puVar11`
avec l'index d'avant, puis les deux boucles de recherche l'ecrasent avec leur compteur d'essais
restants ; `cmp ebx,edx` en `0x00530376` le confirme au binaire. Le son de deplacement se declenche
donc des qu'une recherche a tourne, meme revenue sur le meme sort. Transcrit tel quel.

**Le masque de la queue n'est pas initialise pour un genre negatif.** Le `ja` non signe saute
par-dessus les cinq `mov ebx`, puis le `jg` signe laisse entrer la boucle avec `ebx` = le pointeur
de manette. Zero est pose a la place, avec le fait dit sur place : c'est inatteignable par le
paragraphe precedent, et rendre la liste vide vaut mieux qu'aleatoire.

### Ghidra s'arrete avant la fin de la fonction

`Blaze.c` rend un corps qui se termine au `ret` de `0x00530714` — mais les genres 8 et 9 vivent
**apres** lui (`0x00530715` et `0x00530761`), atteints par la table de sauts `0x00530948`. La
fonction fait 2 262 octets, pas 1 701. Trois tables de sauts (`0x00530948` les dix genres,
`0x00530970` les defauts de categorie, `0x00530998` les cinq masques) ne se lisent que dans le
binaire. Desassemblage complet garde dans `Legacy/Raw/Blaze/FUN_00530070.disasm.txt`.

`DAT_00564600`, lu par le genre 6, donne **trois** ecoles de sorts (29, 24, 20) : `PTR_DAT_00564640`
porte trois pointeurs puis un mot nul, et `DAT_00564608` (`00 1d 35 49`) est leur somme courante.

### Le seul trou restant

**TODO(REVERSE) — `FUN_00523aa0`, la seconde machine a opcodes.** Le genre 5 valide (« se servir de
l'objet ») descend `UseCarriedItem_00523820` → `RunItemScriptSaved_00523860` →
`RunItemScript_00523AA0`. Les deux premieres sont transcrites ; la troisieme leve. C'est la meme
boucle que `RunScript` (arret sur `0xff`, ou des qu'un gestionnaire pose un resultat, sauf l'opcode
2) mais sur `PTR_LAB_00564f80` : **23 gestionnaires**, `0x00523b00` a `0x00524170`. Rien
d'autre du menu rapide n'est manquant.

> **Correction du 2026-08-26** : ce paragraphe disait « tous presents dans `Blaze.c` ». **Faux** —
> onze des vingt-deux corps distincts sont des `LAB_`, que Ghidra n'a vus que pris en adresse dans
> la table. L'affirmation venait de ce que la table de pointeurs affiche des adresses en
> `0x00523xxx`, une bande ou `Blaze.c` a beaucoup de fonctions ; elle n'avait pas ete verifiee
> nom par nom. Le reflexe : compter les corps presents avec un `grep` par adresse avant d'annoncer
> qu'un sous-arbre est decompile.

`FUN_00523860` porte le defaut de recuperation habituel de Ghidra : sa seconde branche appelle
`FUN_00523aa0(unaff_retaddr, param_1, param_2, …)`, decalee d'un cran, en prenant le mot de retour
pour un argument. Les deux branches passent les memes trois arguments ; `FUN_00539190` porte le
meme defaut sur le meme squelette.

### La chaine de recalcul etait deja la

Les cinq appels que la validation d'equipement enchaine — `FUN_00538ab0`, `FUN_00538f50`,
`FUN_00538c50`, `FUN_00538e20`, `FUN_00538fa0` — ont ete **retranscrits puis jetes** : ils
existaient deja sous `CommitStagedStatBlock` / `ScaleStatBlockByDifficulty` / `ComputeDerivedStats`
/ `ApplyEquipmentStatBonuses` / `FinalizeEquipmentEffects` dans `Screens/State04/CharacterCreation.cs`,
et trois ecrans les appellent deja dans cet ordre exact. La regle de `CLAUDE.md` — « chercher un
doublon avant de transcrire » — vaut aussi pour les fonctions **deja reconstruites**, pas seulement
pour deux adresses du binaire.

**TODO(REVERSE)** : ces cinq-la satisfont desormais la regle (3) d'`ARCHITECTURE.md` — quatre ecrans
les appellent (State03 x2, State04, DebugParty). Elles restent dans `State04/CharacterCreation.cs`
faute d'un `Engine/` evident ; leurs douze sites d'appel dans `Blaze.c` s'etalent de `0x00516350` a
`0x00539b80`, ce qui plaide pour un sous-systeme « statistiques derivees ». La promotion n'a pas ete
faite dans le meme changement qu'une correction de comportement.

### Deux plantages, tous deux anterieurs a cette transcription

**`0xC0000409` sans une ligne de journal : un `stackalloc` deborde.**
`DrawSpeechOverlay_005309B0` donnait un `stackalloc short[2]` a `Model3d.ProjectPoint`, qui ecrit
**huit octets** — le couple ecran en `+0`, la profondeur en `+4`. Le JIT pose un temoin de pile sur
toute methode contenant un `stackalloc` et le verifie dans l'**epilogue** : le processus mourait
donc *au retour* de la fonction, sans message, et le verdict de `playtest.ps1` annoncait « aucun
plantage ». Le cadre de l'original reserve bien huit octets a cet endroit (`local_44` suivi de
quatre octets muets avant `local_3c`). Deuxieme site corrige au passage : le balayage de cellules de
`WorldMapTick`. Cinq autres appelants avaient deja la bonne taille.

**La bulle se dessinait dans le coin haut gauche : recouvrement d'arene.**
`DrawSpeechOverlay_005309B0` posait son origine par `Mem.ItemMenuState(0x005de154 / _158 / _15c)` et
lisait son zoom par `Mem.ItemMenuState(0x005de160)` — l'arene — alors que le lecteur
(`UiWindow.EmitViewportRect`) lit les champs simples `s_Unknown_005de154…` de
`Screens/State00/MainMenu.cs`, et que le zoom vit dans `GameMemory.s_SizeSetting_005de160`.
L'ecriture n'atteignait jamais le lecteur, l'origine restait a zero. Ramene sur les champs.

Le `TODO(REVERSE)` de `Engine/UiWindow.cs` disait depuis le 2026-08-21 : « aucun site n'adresse
l'arene entre `0x005de154` et `0x005de170`, donc le recouvrement ne se manifeste pas ». **C'etait
vrai jusqu'a ce qu'un site nouveau l'adresse.** Le recouvrement lui-meme n'est pas corrige — le
corriger changerait le comportement, et l'etendue de l'arene reste douteuse — mais la note dit
maintenant qu'il a mordu une fois.

---

## 2026-08-26 (suite) — `FUN_00523aa0` : la seconde machine a opcodes, ses 23 opcodes et ses deux entrees

`RunItemScript_00523AA0` transcrit la machine a opcodes des objets, et ses vingt-trois
gestionnaires avec elle. Sa boucle est celle de `RunScript` a la ligne pres — arret sur `0xff`, ou
des qu'un gestionnaire pose un resultat, sauf l'opcode 2 qui est la branche. Seule la table change :
`PTR_LAB_00564f80`, 23 entrees, 22 corps distincts (12 et 19 partagent le meme).

### Onze corps sur vingt-deux sont absents de `Blaze.c`

Ghidra ne les a vus que pris en adresse dans la table, d'ou le prefixe `PTR_LAB_`. Les onze :
`0x00523b00`, `0x00523bb0`, `0x00523bd0`, `0x00523cb0`, `0x00523d60`, `0x00523e10`, `0x00523f00`,
`0x00523f10`, `0x00523f80`, `0x00523fc0`, `0x00524170`. Tous transcrits depuis le desassemblage,
conserve dans `Legacy/Raw/Blaze/FUN_00523aa0-handlers.disasm.txt`.

**Un byte count reste un mauvais estimateur.** Les onze pesent 712 octets a eux tous et se sont
ecrits en une passe : neuf sont des variantes a une constante pres de corps deja decompiles (l'unite
arithmetique sur une banque differente, la particule sur la cible plutot que sur soi). Ce sont les
*formes distinctes* qui comptent, et il n'y en avait que quatre nouvelles.

### Les quatre arguments, etablis par recoupement

Ghidra donne la signature des onze corps decompiles ; les autres se lisent aux decalages de pile.
Tous recoivent la meme chose :

| argument | ce que c'est |
|---|---|
| `slot` | l'enregistrement d'entite de 0x9c octets de celui qui se sert de l'objet ; `slot[0x70]` est sa cible |
| `record` | son bloc par personnage de 0x2000 octets |
| `item` | le bloc de 0x280 octets de l'objet, dans `record + 0x300 + n * 0x280` |
| `s` | le curseur de script — qui vit **dans** le bloc d'objet |

Ce dernier point explique les opcodes 1 et 2 : leurs sauts sont des decalages de seize bits
**depuis `item`**, pas depuis le curseur.

### Deux scripts par objet, deux entrees dans la machine

| entree | script | appelee par |
|---|---|---|
| `FUN_00523860` → `RunItemScriptSaved_00523860` | `item + item[0x80]` | `FUN_00523820`, « se servir de l'objet » (genre 5 du menu rapide) |
| `FUN_00523a20` → `RunTriggeredItemScript_00523A20` | `item + item[0x82]` | `FUN_005238e0`, l'evenement d'objet equipe |

`FUN_005238e0` etait une souche sous le nom **faux** de `SetEntityAnimationState_005238E0` — rien
dans son corps ne touche a une animation. Elle est transcrite sous
`TriggerEquippedItems_005238E0` : elle balaie les cinq emplacements de `+0x2b0` et fait tourner le
script declenche de chaque objet dont `item[0x84]` vaut l'evenement. Ses sept sites d'appel dans la
reconstruction passent 0, 1, 5, 6 et 0x15 comme evenements.

**Trois protections encadrent l'appel, et elles disent que ce script peut en declencher un autre** :
un masque de cinq bits (`DAT_005e99c0`) empeche un emplacement de se re-declencher, la banque de
variables de script est empilee dans `0x005de170` au pas de 0x40, et la cible `slot[0x70]` dans
`0x005de2b8`.

**La pile de sauvegarde touche son propre compteur.** `0x005de170 + 5 * 0x40` vaut exactement
`0x005de2b0`, l'octet de profondeur. Cinq places pour cinq emplacements d'equipement : la
disposition de 1998 est juste au dernier octet pres, et c'est ce qui justifie de les loger dans la
meme allocation (`ItemMenuArena`) plutot qu'en champs separes.

### `item[0x84]` est un identifiant d'evenement, pas une categorie

Deux sites le lisent et se recoupent : `TriggerEquippedItems_005238E0` compare `item[0x84]` a
l'evenement recu, et l'opcode 18 l'indexe dans `DAT_0052403c` (22 entrees, donc evenements 0 a
0x15). La table rend une famille 0 a 8, et une table de sauts a neuf entrees
(`0x00524014`) la reduit a **un seul bit** : 1 pour les familles impaires, 0 sinon. Le compilateur a
etale un `switch` sur ce qui tient en une comparaison de parite ; la transcription garde
l'enumeration, qui est ce que la table dit.

### Ce que chaque opcode fait

| op | corps | role | taille |
|---:|---|---|---:|
| 0 | `LAB_00523b00` | consommer l'objet, une chance sur cent de `s[1]` ; a zero, vider l'emplacement, refaire la liste d'inventaire `+0x2c0` et deshabiller | 2 |
| 1 | `LAB_00523bb0` | saut, decalage 16 bits depuis `item` | 3 |
| 2 | `LAB_00523bd0` | saut conditionnel, et remise a zero de la condition | 3 |
| 3 | `FUN_00523c00` | unite arithmetique sur l'enregistrement d'entite | 4 |
| 4 | `FUN_00523c30` | idem, sur celui de la cible | 4 |
| 5 | `FUN_00523c70` | idem, sur `record + 0x100` | 4 |
| 6 | `LAB_00523cb0` | idem, sur le bloc de statistiques de la cible | 4 |
| 7 | `FUN_00523d20` | idem, sur `record + 0x200` | 4 |
| 8 | `LAB_00523d60` | idem, sur le bloc de statistiques de la cible `+0x100` | 4 |
| 9 | `FUN_00523dd0` | engendrer un effet sur soi | 4 |
| 10 | `LAB_00523e10` | idem, sur la cible | 4 |
| 11 | `FUN_00523e50` | unite arithmetique sur la banque de variables | 4 |
| 12 | `FUN_00524060` | particule a sa propre position | 4 |
| 13 | `FUN_00523e80` | unite arithmetique sur le bloc de l'objet equipe `s[1]` | **5** |
| 14 | `FUN_00523ed0` | declencher un sort ou un effet (`FUN_0053a980`) | **5** |
| 15 | `LAB_00523f00` | le son de refus, et rien d'autre | **1** |
| 16 | `LAB_00523f10` | choisir la cible par indice, groupe ou scene | 3 |
| 17 | `LAB_00523f80` | choisir la cible par recherche (`FUN_00474ad0`) | 2 |
| 18 | `LAB_00523fc0` | appliquer un effet de la table de 48 de `0x0056d4e0` | 2 |
| 19 | `FUN_00524060` | le meme corps que 12 | 4 |
| 20 | `FUN_005240d0` | particule a la position de la cible | 4 |
| 21 | `FUN_00524140` | jouer un son | **6** |
| 22 | `LAB_00524170` | ouvrir l'ecran du menu objets | — |

### Trois fidelites que la relecture voudra questionner

**L'opcode 12 passe le curseur de script comme parametre quand la largeur est inconnue.** `s[1]`
dit 0 octet, 1 mot, 2 double mot ; toute autre valeur laisse le registre non initialise, et il se
trouve contenir le curseur (`mov eax,[esp+14h]` en `0x005240a1`). Ghidra le rend exactement ainsi
(`uVar2 = param_4`). Transcrit tel quel.

**L'opcode 21 lit deux fois le meme mot** pour le gain gauche et le gain droit. Ce n'est pas une
coquille de transcription : `FUN_00524140` lit `s+4` deux fois.

**L'opcode 22 rend une valeur qui n'est pas un curseur de script.** `LAB_00524170` est
`FUN_00524180` — l'etat de jeu 6 — duplique par le compilateur, avec le second appel en saut de
queue. La valeur rendue est donc celle de `FUN_00524320`, dont le dernier appel est `FUN_00524190`,
c'est-a-dire `DrawItemMenuTitle`, qui rend un **curseur de liste d'affichage**. La boucle
dereference ensuite ce pointeur comme un opcode.

C'est un defaut de 1998, **latent** : il ne se manifeste que si un script d'objet emet reellement
l'opcode 22. Il n'est donc pas verse dans `legacy-bug.md`, qui ne recense que l'observe ; un
`TODO(REVERSE)` le porte sur place. La transcription fait les deux appels de l'original puis leve,
plutot que d'inventer une valeur de retour : le jour ou le jeu y passe, la levee dira ou regarder.

### `CONCAT31` de Ghidra a failli couter une regression

`FUN_0053a980` ecrit `*(short *)(param_1 + 6) = (short)CONCAT31((int3)param_2,(char)param_3);`.
Lu vite, cela ressemble a « ranger l'indice » ; c'est en fait **les deux arguments empiles dans un
mot**, la famille en octet haut et l'indice en octet bas. La premiere transcription avait garde le
seul indice. Le binaire tranche sans ambiguite — `mov dh,cl ; mov dl,al ; mov [esi+6],dx` en
`0x0053a991` — et c'est la seule facon de le voir.

**La regle** : un `CONCAT` de Ghidra tronque par un cast n'est jamais evident. Le desassembler
coute trois lignes ; le deviner coute un champ silencieusement faux.

### Les deux frontieres, nommees et bruyantes

**`FUN_00474ad0`, la recherche de cible** (opcode 17). 47 lignes seulement, mais elle ouvre le
sous-arbre de ciblage de combat : `FUN_00519af0` (52 l., le balayage des entites de scene par
cellule) et `FUN_00519c30` (63 l., le balayage du groupe), qui tirent `FUN_00535a90`,
`FUN_00538af0` et `FUN_005238e0`. C'est le ciblage, pas le script d'objet.

**La table de 48 effets de `0x0056d4e0`** (opcode 18), `0x0053afb0` a `0x0053cdf0`. L'etendue est
deduite : le dword qui suit la 48e entree vaut 1, debut d'une table de masques de bits. C'est le
sous-systeme des effets, atteint aussi par les sorts.

### Non exerce

Le menu rapide refuse « se servir d'un objet » dans l'auberge (le jeu lui-meme l'interdit), et
aucun des sept sites de `TriggerEquippedItems_005238E0` ne se declenche dans la piece d'arrivee.
**La machine est transcrite et compilee, elle n'a pas tourne.** Le premier donjon la dira.

---

## 2026-08-26 (suite 2) — opcode 30 : sortir du batiment

**Le plantage.** Quitter l'auberge leve
`Interpreteur de script : opcode 30 = FUN_00515e20, pas encore transcrit.` Le message portait le
chemin du desassemblage, et cette fois le relevé de 96 octets **suffisait** : le corps s'arrete au
`ret` de 0x00515e70, soit 81 octets, suivi de quinze `nop` de bourrage. Aucune reprise plus large
n'etait necessaire — un dump tronque se reconnait a son `(bad)` final, celui-ci finit sur des `nop`.

### Un jumeau de l'opcode 17, a la fin pres

La comparaison instruction par instruction avec `ScriptOpTransferArea` (opcode 17, suite 15) donne
la fonction en entier :

| | opcode 17 | opcode 30 |
|---|---|---|
| garde de genre | `& 0xfc0000 == 0x800000` | **identique** |
| marquage de l'entite partante | `+0x98 ← +0x32`, drapeaux `& 0xf7fffdff \| 0x2000`, `+0x12` +1 | **identique** |
| `DAT_005de53c` | `entite[8]` | **identique** |
| destination (`005de528`/`52c`/`530`) | trois operandes, `0xff` = ne pas changer | **rien** |
| pas de boucle `DAT_005645f8` | `2` | **`0xfe`** |
| etat de jeu `DAT_005de510` | — | **`2`** |
| avancee du curseur | `script + 4` | **`script + 1`** |

L'avancee d'un seul octet (`lea eax,[ecx+1]`, pas `[ecx+4]`) est la preuve mecanique qu'il n'y a
aucune operande a lire : la destination n'est pas omise du relevé, elle n'existe pas.

### Ce que les deux valeurs de fin enchainent

`0xfe` est `-2` signe, la valeur sur laquelle `RunWorldMapState` fait `return` au lieu de reboucler
— l'etat 3 rend la main. `DAT_005de510 = 2` designe l'etat suivant, et le cas 2 de
`RunGameStateTick` est `Res.LoadMenuResources()` puis `RunDestinationSelect()` puis `etat = 3`.
Autrement dit : **on sort du batiment par la carte du monde, on choisit ou aller, on rentre en
jeu**. La ou l'opcode 17 recharge une zone sur place sans quitter l'etat 3, l'opcode 30 remonte d'un
etage.

Les deux ecritures ne se comprennent que l'une par l'autre, exactement comme le couple `pas = 1` /
`pas = 2` de la suite 15 : le pas dit *comment* on repart, l'etat dit *ou*.

### Non exerce

Transcrit et compile, pas execute — les tests sont a la charge du Responsable depuis le 2026-08-26.

**Reserve a dire franchement**, la meme qu'a la suite 15 : cet opcode ne fait que *poser* l'etat. Le
travail arrive apres, dans `RunDestinationSelect` et dans le rechargement qui suit, et ce chemin-la
peut tres bien buter sur autre chose qui n'est pas encore transcrit. Corriger l'opcode 30 garantit
que le jeu ne s'arrete plus **ici**.

---

## 2026-08-26 (suite 3) — les sept tables de sauts d'entites, pour les onze zones

**Le plantage.** Entrer dans la foret depuis la carte du monde leve
`Tables de sauts d'entites, index 48 (zone * 0x10 + sous-zone) : seules les valeurs de l'index 0
sont relevees.` Index 48 = zone 3, sous-zone 0.

### Ce que le relevé a montré, et pourquoi il valait mieux tout prendre d'un coup

`FUN_0052a660` (Blaze.c 75118) choisit sept blocs dans **sept tables de pointeurs contigues** et les
recopie sur les sept tables actives `0x0056ad48 .. 0x0056b2c8`. Ces tables de pointeurs sont
`PTR_PTR_00566d98`, `00567058`, `00567318`, `005675d8`, `00567898`, `00567b58`, `00567e18` —
espacees de `0x2C0` exactement, soit **176 pointeurs chacune : onze zones de seize sous-zones**.

Le dépôt en avait relevé **un seul index sur 176**, à la main. La suite aurait été 175 allers-retours
d'une session chacun. Trois faits, tous vérifiables en une commande, ont montré que ce n'était pas
nécessaire :

- les sept tables sont contigues et de taille connue → un seul `od` de 4 928 octets les donne toutes ;
- elles ne citent que **197 blocs distincts** (29 par colonne, dont `0x0056B3C8` partagé par les
  sept) — pas 1 232 ;
- les blocs pointés forment un pool **contigu** de `0x00553288` à `0x0055E8A8`, juste avant
  `s_AreaHandlerSource_0055e8a8` qui était déjà relevé.

D'où `OpenForesia.Tools/dump-entity-vtables.sh`, qui régénère
`OpenForesia/src/Blaze/Engine/EntityVTables.cs` : 7 × 176 adresses + 11 712 entrées de bloc. **Aucune
valeur n'est écrite à la main.**

### Le contrôle qui rend le relevé sûr

Les sept blocs de l'index 0 relevés mécaniquement sont **identiques, entrée pour entrée**, aux sept
tables `s_VtSource_*` qui étaient écrites à la main dans `Resources.cs` depuis des mois. Sept `diff`
vides. Le générateur est donc validé contre du connu avant d'être cru sur les 27 autres jeux, et
l'index 0 ne peut pas avoir régressé.

### Deux choses que le relevé apprend au passage

**`0x0056B3C8` n'est pas la souche vide.** C'est le bloc que les sept tables donnent à toute
sous-zone inexistante, et il porte soixante-quatre fois `FUN_00536170` — **l'arret fatal**. Une
sous-zone qui n'existe pas ne se traverse pas en silence : elle tue le processus. C'est le pendant du
`if (0xf < sous-zone) FUN_00536170()` déjà transcrit, pour le cas où l'index est dans la table mais
la zone vide.

**La carte des zones est complete** : onze zones (0..10) ont un contenu, et le nombre de sous-zones
par zone se lit directement dans la table — zone 0 : 1 ; zones 1, 3, 5 : 10 ; zone 4 : 11 ; zone 6 :
12 ; zone 7 : 3 ; zones 8, 9 : 2 ; zone 10 : **16**, la seule pleine.

### Ce que la forêt va exercer, et ce qu'elle va lever

Les tables de la zone 3 ne ressemblent pas à celles de la zone 0 : **25 des 64 gestionnaires
d'entité** (`0x0056adc8`) et **15 des 64 verbes** (`0x0056aec8`) changent. Ces adresses-là ne sont
pour la plupart pas transcrites, et `CallEntityHandler` lèvera en les nommant — ce qui est le
comportement voulu.

Un détail rassure sur le chemin par image : dans `0x0056afc8`, les entrées 8 et 10 — les deux que
`RunWorldMapTick` appelle à chaque image — valent `0x00401FA0` (corps vide) pour la zone 3, là où la
zone 0 y mettait `0x0053DCB0` et `0x0053DF90`. Elles ne lèveront pas.

**Ce que cette suite corrige dans ce qui précède** : l'affirmation de la suite du 2026-08-25
(« `InstallEntityVTables` leve pour tout index non nul ») n'est plus vraie. Les 176 index sont
relevés ; seuls les *gestionnaires* qu'ils désignent restent à transcrire.

### Non exerce

Transcrit, généré, compilé, contrôlé contre l'index 0 — pas exécuté. Les tests sont à la charge du
Responsable.

---

## 2026-08-26 (suite 4) — les verbes de la foret : le premier, et la carte du reste

**Le plantage.** `Interpreteur de script, opcode 0 : verbe 6 = 0x443260, pas encore reconstruit.`

Premiere chose a verifier, et elle rassure : `0x00443260` **est** l'entree 6 de la table de la
zone 3 (`0x005554a8`), et c'est l'entree **1** de la zone 0. Le releve de la suite 3 tient donc au
runtime, et la forme du dispatch — un `switch` sur l'**adresse**, pas sur l'indice — encaisse le
remappage sans rien changer. C'etait le pari de cette forme ; il est gagne.

### `FUN_00443260` — le « tout effacer » de la memoire de script

53 octets, absente de `Blaze.c`, relevee au desassembleur
(`Legacy/Raw/Blaze/AreaVerbs/FUN_00443260.asm`). Quatre `rep stosd` de `0x10` mots sur
`0x005e9840`, `0x005e9060`, `0x005e9780`, `0x005e96e0` — les banques **3, 2, 1 et 0**, c'est-a-dire
exactement les quatre bancs que servent les opcodes 4 a 7 de l'interpreteur. Ce verbe remet a zero
toute la memoire de script de la zone.

Elle rend **0** : le `xor eax,eax` de `0x00443266` sert de valeur aux `stosd`, et `rep stosd` ne
touche pas EAX.

### Deux voisines prises dans la foulee

Le desassemblage de la plage entiere etait deja tire ; deux verbes de la meme table y tenaient en
quelques instructions, transcrits dans le meme passage :

- **`FUN_00442E10`** (11 octets) — rend le bit 0 de `0x00563c77`. C'est un verbe de **test**.
  L'adresse est un piege deja tranche le 2026-08-23 : ce n'est pas une globale mais le champ
  `+0x8c7` du descripteur `0x005633b0`, et la boite de dialogue etait restee invisible pour l'avoir
  modelisee en `static byte`. La transcription passe par `s_Unknown_00563c77`.
- **`FUN_004432E0`** (19 octets) — si `0x005e9001` porte `0x10`, efface le bit 0 de
  `s_StateBank3_005e9840 + 1`. Rend 0 par les deux chemins.

### Ce qui reste dans cette table, et ce que ca coute

La zone 3 cite **13 verbes distincts** hors souche ; cinq sont maintenant couverts (les trois
ci-dessus, plus `0x004441F0` la souche a zero et `0x00401FA0` la souche nue). Les huit qui restent,
mesures au desassembleur — la taille est jusqu'au premier `ret`, donc un minorant :

| verbe | indice en zone 3 | taille | appels |
|---|---:|---:|---:|
| `0x004457F0` | 63 | 28 | 1 |
| `0x00445840` | 8 | 28 | 1 |
| `0x00445480` | 62 | 61 | 0 |
| `0x004439A0` | 10 | 83 | 0 |
| `0x00443830` | 9 | 108 | 0 |
| `0x004431F0` | 12 | 109 | 0 |
| `0x004438A0` | 5 | 179 | 0 |
| `0x00443300` | 14 | 171 | 1 |
| `0x004433B0` | 13 | ≥ 1150 | 0 |

Le desassemblage de toute la plage est disponible sans nouvel outillage :
`disasm Legacy/Binaries/Blaze.exe 0x00442e10 10992`. **Rappel de la suite 3** : une taille n'est pas
une difficulte — `FUN_004747F0` faisait 723 octets et est sortie en une passe.

### Non exerce

Compile, pas execute. Quel verbe la foret appelle ensuite, seul le jeu le dit — c'est exactement ce
pour quoi `OPENFORESIA_TRACE_VERBS=1` existe.

---

## 2026-08-26 (suite 5) — le premier gestionnaire propre a une zone, et le chantier qui s'ouvre

**Le plantage.** `FUN_0050e5e0, table 0x005fa5e0 entree 58, sous-type 32 : gestionnaire d'entite
0x456730 non transcrit.`

**C'est la confirmation que le releve des onze zones tient au runtime.** Le sous-type 32 renvoie a
`0x004734B0`, le gestionnaire par defaut, dans la zone 0 — et a `0x00456730` dans le bloc
`0x005553a8`, celui de la zone 3. La valeur lue par le jeu est celle du releve, entree pour entree.
C'est le premier gestionnaire **propre a une zone** que le depot exerce.

### `FUN_00456730` — vingt-deux octets

Absente de `Blaze.c`, relevee au desassembleur (`Legacy/Raw/Blaze/FUN_00456730.asm`) :

```
mov eax,[esp+4] ; mov ecx,0C00h ; mov [eax+28h],cx ; mov [eax+2Ah],cx ; mov [eax+2Ch],cx ; ret
```

`+0x28`, `+0x2a`, `+0x2c` sont l'echelle X, Y, Z de l'enregistrement, `0x1000` valant l'unite — c'est
ce que `SpawnEntityFormation` y ecrit pour une entite a taille normale. `0x0C00` est donc **trois
quarts** : ce decor de foret nait plus petit que son modele. Rien d'autre n'est touche.

### Ce qui reste dans la table de la foret, et pourquoi je m'arrete la

La table `0x0056adc8` de la zone 3 compte **18 gestionnaires encore non transcrits**, et — a la
difference des verbes de la suite 4 — **aucun n'est trivial**. Mesure au desassembleur (taille
jusqu'au premier `ret`, donc un minorant ; le compte d'appels est ce qui predit vraiment le cout) :

| gestionnaire | sous-type(s) | taille | appels |
|---|---:|---:|---:|
| `0x004571D0` | 22 | 103 | 3 |
| `0x004566C0` | 33 | 110 | 1 |
| `0x00457240` | 19 | 122 | 0 |
| `0x00458170` | 31 | 133 | 1 |
| `0x00457140` | 23 | 142 | 4 |
| `0x00457E20` | 8 | 152 | 3 |
| `0x00457FC0` | 34 | 172 | 4 |
| `0x00456E80` | 25 | 183 | 3 |
| `0x00457770` | 17 | 197 | 1 |
| `0x00456F40` | 24 | 234 | 5 |
| `0x004746F0` | 26 | 242 | 1 |
| `0x00457EC0` | 9 | 256 | 2 |
| `0x00456CF0` | 27 | 395 | 5 |
| `0x00457BF0` | 16 | 503 | 8 |
| `0x00456A30` | 29 | 666 | 6 |
| `0x00456750` | 30 | 734 | 10 |
| `0x00457370` | 18 | 946 | 10 |
| `0x00474380` | 15 | non mesure | — |

Le plus petit fait 103 octets avec trois appels ; la moitie en portent quatre ou plus, donc chacun
tire des appelees qui peuvent elles-memes manquer. **Les transcrire d'avance serait un chantier de
plusieurs jours pour des sous-types dont rien ne dit qu'ils apparaissent** : la foret n'en a
exerce qu'un jusqu'ici.

La bonne facon d'y aller est celle que le depot s'est donnee : laisser le jeu nommer le suivant.
`OPENFORESIA_TRACE_SCRIPT=1` et `OPENFORESIA_TRACE_VERBS=1` sont faits pour ca, et le message de
lacune porte deja les seize premiers octets de l'enregistrement.

Le desassemblage de la plage entiere se retire sans nouvel outillage :
`disasm Legacy/Binaries/Blaze.exe 0x004566c0 7000`.

### Non exerce

Compile, pas execute.

---

## 2026-08-26 (suite 6) — la trace de script en jeu, et le sous-type 31

**Deux runs `playtest.ps1`, a la demande de Vincent.** Le premier avec `-Trace SCRIPT`, le second
avec `-Trace SCRIPT,VERBS`.

### Ce que la trace etablit

Quinze opcodes distincts tournent sur le trajet auberge → foret, et **tous les quinze sont deja
transcrits**. La repartition est tres inegale :

| opcode | executions | |
|---:|---:|---|
| 4, 6, 16, 3 | 564, 522, 463, 317 | 97 % du total |
| 9 | 48 | |
| 0, 1, 2, 13, 14, 18, 19, 28, 29 | 1 a 7 | |
| **30** | **1** | la sortie de l'auberge |

**L'opcode 30 tourne une fois de tout le run** — c'est la confirmation en jeu de la transcription de
la suite 2, et la preuve qu'un opcode rare n'est pas un opcode secondaire : celui-la est le seul
chemin hors du batiment.

Les lanceurs de script sont eux aussi comptes : `RunEntityScriptList` (index 8 et 9) et
`TestTriggerVolumes_00534CD0` (index 0 et 3) font l'essentiel ; `RunWorldMapTick`,
`ResolveEntityContact_00534330`, `Handler_00544bc0` et trois autres lancent chacun un script isole.

Cote verbes, la salle d'arrivee n'en appelle que deux (3 → `0x00442F60`, 10 → `0x004441F0`), tous
deux couverts. **La table de verbes n'est donc pas ce qui bloque** ; les gestionnaires d'entite le
sont.

### `FUN_00458170` — le sous-type 31, un decor qui parle

133 octets, absente de `Blaze.c` (`Legacy/Raw/Blaze/FUN_00458170.asm`). Une machine a trois etats
sur `+0x10` :

- **etat 0** — repos. Se declenche quand les quatre bits bas de `+0x9` (le champ de contact) sont
  poses : lie le texte, passe a l'etat 2 avec un compte de 3 en `+0x12` ;
- **etat 2** — compte a rebours de trois images vers l'etat 1 ;
- **etat 1** — attend que le contact cesse pour revenir a 0.

C'est ce qui empeche le decor de reparler a chaque image tant qu'on reste appuye contre lui.

Le sujet du texte a un **repli** : `+0x70` designe l'entite concernee, et quand il est nul c'est
`s_EntityList_005de558`, l'enregistrement neutre remis a zero a chaque chargement de zone. Le bit
`0x10000` (« ecarte ») de son mot de tete annule l'affichage **sans annuler le changement d'etat** —
le decor consomme le contact meme quand il ne dit rien. Le bloc d'etiquettes vient de la table
`+0x70` du repertoire de zone, indexee par `+0x16` lu en `movsx`, donc **signe**.

### Ce que les deux runs n'ont pas etabli

Le second run n'a pas atteint la foret : 557 primitives par image et une capture montrant
« - La reception - », c'est-a-dire la salle d'arrivee. Sortir de l'auberge demande de conduire le
personnage, ce qui reste le partage du 2026-08-24. **La transcription de `FUN_00458170` n'est donc
pas exercee** ; elle compile, et c'est tout ce qui est acquis.

Il reste **17 gestionnaires** dans la table de la foret (carte des tailles en suite 5).

---

## 2026-08-26 (suite 7) — huit gestionnaires de la foret, et ou le bloc s'arrete

Vincent a demande les dix-sept d'un bloc. **Huit sont transcrits, un est bloque, huit restent.** Le
detail de ce partage compte plus que le compte : ils ne sont pas restes pour la meme raison.

### Ce que la lecture des dix-sept a d'abord appris

**Quatre des cinq « appelees manquantes » n'en etaient pas.** `0x00545A10`, `0x00545A16` et
`0x00545A1C` sont des **thunks d'import** — `jmp dword [IAT]` — et les slots resolvent aux ordinaux
**442, 450 et 451** de `winsys3d`, c'est-a-dire `SquareRoot0`, `GetCosTableEntry` et
`GetSinTableEntry`, tous trois deja lies dans `Interop/Winsys3d.cs`. Une adresse dans `.text` qui ne
ressemble a rien peut n'etre qu'un saut vers une DLL ; le verifier coute un `od` sur `.idata`.

**`FUN_004580E0` est la troisieme sœur** de `TryPlaySoundNearEntity` et
`TryPublishCameraEffectNearEntity` : meme calcul de distance a la camera, memes six arguments, meme
convention `stdcall` d'`Ordinal_442`. Deux differences seulement — la portee est un **argument** au
lieu d'une constante, et le gain **decroit lineairement avec la distance** avant de partir sur les
deux voies, donc le son est **non positionnel**. Le patron etait deja au depot ; il n'a fallu que le
relire.

### Les huit transcrits

| sous-type | fonction | ce qu'il fait |
|---:|---|---|
| 33 | `FUN_004566C0` | chuchote toutes les vingt images, a portee 3000 |
| 22 | `FUN_004571D0` | balance : `+0x34 = sin(phase) >> 5`, phase et vitesse tirees au hasard |
| 23 | `FUN_00457140` | respire : la meme phase module l'**echelle** `+0x28`/`+0x2c` autour de `0xED8` |
| 19 | `FUN_00457240` | tombe, rebondit, puis flotte — le bit `0x40000000` separe les deux moities |
| 25 | `FUN_00456E80` | lance un script sur le meneur a chaque image, s'eteint au trentieme tour |
| 9 | `FUN_00457EC0` | **dalle de pression** : pose un bit dans le banc `0x005e96e0` |
| 34 | `FUN_00457FC0` | tremble sur place autour d'une position memorisee en `+0x44` |
| 31 | `FUN_00458170` | parle au contact (suite 6) |

plus les deux appelees `CountPartyMembersMatching` (`FUN_00535000`) et `TryPlayDistanceSound`
(`FUN_004580E0`).

**Le plus interessant des huit est la dalle.** C'est le premier decor transcrit qui **ecrit dans la
memoire de script** : `+0x16` est un numero de bit global, l'octet est `+0x16 / 8` et le bit
`+0x16 % 8`, dans le banc que sert l'opcode 4 de l'interpreteur. Et elle **n'efface jamais le
bit** — la relachee ne fait que remonter la dalle. C'est un declencheur a sens unique, ce qui
explique comment un script sait qu'on est passe quelque part.

**Deux idiomes reviennent partout** et se transcrivent mal si on les lit vite :

- `cdq ; xor eax,edx ; sub eax,edx ; and eax,N-1 ; xor ; sub` n'est **pas** un masque, c'est le
  **reste signe** `% N` pour N puissance de deux. Le masque seul changerait le resultat sur un
  tirage negatif ;
- `cdq ; and edx,7 ; add eax,edx ; sar eax,3` est la **division signee par 8** avec arrondi vers
  zero.

### Le bloque, et pourquoi il l'est

**Sous-type 8, `FUN_00457E20`** (152 octets) — son corps est lu et compris : il teste les vitesses
`+0x50` et `+0x58` contre `0x2000`, joue un son au franchissement avec un verrou de quinze images
en `+0x98`, puis appelle `TestTriggerVolumes_00534CD0` et remet les trois vitesses a zero. Mais sa
**premiere instruction utile est `FUN_00455B30(entite, 0x88C2)`**, une fonction de 200 octets et
plus qui monte un cadre de pile d'une quinzaine de champs pour appeler `FUN_005330D0`. La
transcrire est un chantier a part entiere, et l'ecrire au jugé donnerait un decor qui bouge faux
sans rien dire. **Laisse en lacune nommee.**

### Les huit qui restent, par cout croissant

| sous-type | fonction | taille | appelees encore absentes |
|---:|---|---:|---|
| 26 | `FUN_004746F0` | 242 | — |
| 24 | `FUN_00456F40` | 234 | — |
| 15 | `FUN_00474380` | non mesuree | — |
| 27 | `FUN_00456CF0` | 395 | `FUN_0045DA40` |
| 16 | `FUN_00457BF0` | 503 | — |
| 29 | `FUN_00456A30` | 666 | `FUN_00455B30` |
| 30 | `FUN_00456750` | 734 | `FUN_00468110` ✓, `FUN_00533940` ✓ |
| 18 | `FUN_00457370` | 946 | — |

Trois d'entre eux passent 500 octets et deux tirent des appelees non transcrites. **Ce n'est pas un
reste de bloc, c'est un second bloc.** Le dire plutot que de livrer huit transcriptions rapides :
une erreur dans un de ces corps ne se voit pas a la compilation et coute une session a retrouver.

### Non exerce

Les huit compilent ; **aucun n'a tourne**. `FUN_00457EC0` en particulier ecrit dans la memoire de
script, donc son effet se verra dans le comportement d'un script, pas a l'ecran.

---

## 2026-08-26 (suite 8) — la foret au complet, moins trois

Vincent : « prends les huit restants aussi ». **Cinq des huit sont transcrits ; les trois autres
butent sur la meme chose, une fonction de collision de terrain.** Total de la journee sur cette
table : **quatorze gestionnaires sur dix-sept**, plus six appelees.

### Les cinq de ce tour

| sous-type | fonction | taille | ce que c'est |
|---:|---|---:|---|
| 24 | `FUN_00456F40` | 234 | quelque chose qui traverse, tombe, **eclate en neuf effets**, et recommence |
| 26 | `FUN_004746F0` | 242 | un decor qui **attend un bit de script** et pulse en attendant |
| 15 | `FUN_00474380` | 400 | la **scenette de sortie** : detourne la camera, fond au blanc, relance la musique |
| 16 | `FUN_00457BF0` | 503 | un projectile en cloche qui **s'eteint en cinq echos** |
| 30 | `FUN_00456750` | 734 | le **radeau** |
| 18 | `FUN_00457370` | 946 | l'**ascenseur** |

(Six lignes pour cinq entrees : `FUN_00457370` etait le huitieme, `FUN_00457FC0` ayant ete pris a la
suite 7.)

### Ce que le lot apprend, et qui vaut plus que les fonctions elles-memes

**Le banc de script est un vrai bus, et on en voit maintenant les deux bouts.** La dalle
`FUN_00457EC0` (suite 7) **ecrit** un bit dans `0x005e96e0` ; `FUN_004746F0` en **lit** un dans
`0x005e9840` avec exactement la meme arithmetique ; `FUN_00457BF0` ne fait **rien du tout** tant que
le bit 0 de `0x005e96fb` n'est pas pose ; et `FUN_00456750` exige deux bits de `0x005e96ff`. Un decor
de foret n'est pas une machine isolee : c'est un abonne d'un bus de drapeaux que les scripts
pilotent.

**Le radeau et l'ascenseur comptent le groupe de deux facons differentes**, et la difference est un
seul bit : `CountPartyMembersMatching(entite, 0x10, …)` pour la dalle et le radeau, `0x20` pour
l'ascenseur. Deux manieres distinctes d'etre « pose sur » quelque chose, jamais confondues par le
binaire.

**Tout est double dans l'ascenseur.** `+0x44` porte le sens, et les cinq decisions de la fonction
sont ecrites **deux fois** dans le binaire, une par sens, avec deux jeux de constantes
(`0x14000`/`0x258000` contre `0xFFF60000`/`0xDC000`). La reconstruction choisit les constantes une
fois et n'ecrit le corps qu'une fois — verifie instruction par instruction que les deux branches ne
different que par la constante. C'est ce que `Strategy.md` appelle reconstruire le flot.

**Une table de sauts qui n'est qu'une constante.** Les six branches de `FUN_00457BF0`
(`0x00457df0`) ne different que par le **gain** : `0x1800 → 0x1000 → 0x0B00 → 0x0800 → 0x400`, la
sixieme etant muette. Rendues en tableau de six entiers plutot qu'en six blocs — le seul contenu de
la table etait la constante.

**Le trajet du radeau est ecrit, pas navigue.** Quatre plages disjointes sur le compteur d'images
(`0x19..0x21`, `0x23..0x2b`, `0x6c..0x75`, `0x78..0x81`) changent le cap de -4, +4, +3, -3. Deux
virages a l'aller, deux au retour, et la traversee s'arrete a la 200e image exactement.

**Un `movsx` change une borne.** `FUN_00457BF0` teste `+0x98 > 12` avec `movsx` puis `ja` : la
comparaison est **non signee**, donc une valeur negative depasse la table au lieu de tomber dessous.
Transcrit tel quel.

### Les trois qui restent, et la seule raison qui les retient

Ce ne sont plus des tailles, c'est **une dependance unique** :

| sous-type | fonction | bloque par | qui appelle |
|---:|---|---|---|
| 8 | `FUN_00457E20` (152 o.) | `FUN_00455B30` (200 o.) | `FUN_005330D0` |
| 29 | `FUN_00456A30` (666 o.) | `FUN_00455B30` | `FUN_005330D0` |
| 27 | `FUN_00456CF0` (395 o.) | `FUN_0045DA40` (300 o.) | `FUN_005397B0` |

**`FUN_005330D0` est le vrai obstacle.** Elle est dans `Blaze.c` (ligne 80202) et c'est une
**recherche de sol dans un maillage triangulaire** : produits vectoriels sur trois sommets,
interpolation barycentrique, plus de 150 lignes de pseudo-C truffee de temporaires. Ce n'est pas un
gestionnaire de decor, c'est de la geometrie de collision — un chantier a part entiere, et le
transcrire au jugé donnerait des entites qui traversent le sol sans que rien ne le dise.

`FUN_005397B0` (37 lignes, deja stubee sous `SetEntityPose_005397B0`) est en revanche a portee de
main : c'est elle qui debloquerait le sous-type 27, le moins cher des trois.

### Non exerce

**Aucun des quatorze n'a tourne.** Ils compilent, et c'est tout ce qui est acquis. Plusieurs
touchent des choses visibles — la camera, la musique, le fondu — donc un defaut s'y verra ; d'autres
n'ecrivent que dans le banc de script, et leur effet ne se lira que dans le comportement d'un
script.

---

## 2026-08-26 (suite 9) — le sous-type 27, et un nom qui etait faux depuis le debut

La chaine demandee : `FUN_005397B0` → `FUN_0045DA40` → `FUN_00456CF0`. Les trois sont transcrites.
**Quinze gestionnaires sur dix-sept.**

### `SetEntityPose_005397B0` n'a jamais pose de pose

Le stub portait ce nom depuis sa creation, avec la signature `(entity, pose, mode)` et le resume
« pose une pose d'entite ». **Il n'y a aucune pose dans ce corps.** `FUN_005397B0` applique des
**degats en pourcentage des points de vie maximaux**, et son corps etait dans `Blaze.c` depuis le
debut — trente-sept lignes que personne n'avait ouvertes parce que le nom laissait croire que le
role etait connu.

Renommee `ApplyPercentDamage_005397B0(entite, pourcentage, parametre d'effet)`. **Les deux appels
deja transcrits passaient `(slot, 2, 0)`** : ce n'etait pas « pose 2, mode 0 » mais **2 % des points
de vie**. Ils n'ont pas change de forme, seulement de sens.

**La lecon vaut d'etre ecrite** : un nom pose sur une hypothese est plus couteux qu'un `FUN_…`,
parce qu'il empeche de relire. `Strategy.md` le dit deja — « renommer seulement quand le role est
etabli » — et voici le cas ou la regle a ete enfreinte, et ce que ca a coute : deux sites d'appel
lus a l'envers pendant des semaines.

### Ce que le corps apprend

`+0x96` est un **verrou d'invulnerabilite** : non nul, le coup ne passe pas ; la fonction le pose a
`0x10` des qu'elle frappe. Les deux appelants deja transcrits le confirment sans le savoir — ils le
mettent a zero juste avant l'appel et le remettent a `0x10` juste apres, pour forcer un coup a
travers.

Les degats ont un **plancher a 1** : un pourcentage trop petit pour valoir un point en vaut quand
meme un. Et **deux champs sont sauves et rendus** autour de la naissance de l'effet `0x50`
(`+0x18` et `+0x0a`) : l'effet herite d'une copie de l'entite, donc le binaire y glisse ses valeurs
le temps de l'appel. Sans la restitution, une entite frappee garderait la teinte de son propre
effet de degats.

### `FUN_0045DA40` — le selecteur qui sert de masque

`+0x9` de la source porte **deux masques de quatre bits**, un par demi-octet, et l'argument
`selector` dit lequel prendre : 0 le bas, 1 le haut. **Toute autre valeur est prise telle quelle
comme masque.** Cela ne se voit que sur la pile : l'argument et le masque partagent la case
`[esp+0x20]`, et les deux branches ne font que l'ecraser. La pseudo-C ne l'aurait pas montre.

Chaque membre touche gagne le bit `0x40` et un verrou de `stunFrames` images — ou **0x3c** quand
l'argument est nul. L'octet de contact est remis a zero en sortant, et le retour est le masque des
membres reellement touches.

### `FUN_00456CF0` — le sous-type 27, le seul decor de la foret qui blesse

Trois machines a etats **dont deux ne sont jamais armees par la fonction elle-meme** :

- `+0x10` : 0 amorcage, 1 attente (compte les membres poses dessus), **2 declenchement** — pose par
  le script, jamais ici ;
- `+0x16` : **1 impact** (4 % des points de vie, demi-octet haut du contact, verrou de deux images,
  son 3, effet d'ecran `0xfa, 0x3c, 0x3c, 1`), **2 restitution** — poses par le script eux aussi.

Ce decor est donc **pilote de bout en bout par son script**. L'amorcage retient trois shorts de
l'arene (`0x005e92b8`, `ba`, `bc`) dans `+0x44`/`+0x48`/`+0x4c` pour les rendre a l'etat 2 de
`+0x16` : l'effet d'ecran est emprunte, puis rendu.

### Les deux qui restent

Sous-types **8** et **29**, tous deux bloques par `FUN_00455B30` → `FUN_005330D0`, la recherche de
sol dans un maillage triangulaire (`Blaze.c` 80202, 150+ lignes de geometrie). Inchange depuis la
suite 8 : c'est un chantier de collision, pas de decor.

### Non exerce

Aucune des quinze n'a tourne.

---

## 2026-08-26 (suite 10) — le crochet de contact au sol, et un script fabrique sur la pile

**Le plantage.** `FUN_0050bf20, membre 0 : le crochet de contact au sol PTR_FUN_0056b048 :
gestionnaire d'entite 0x541440 non transcrit.`

**Ce n'etait pas la table 0x0056adc8** — celle des dix-sept — mais la **quatrieme** des sept,
`0x0056afc8`, entree 32. La zone 0 y met la souche vide ; la foret y met `FUN_00541440`. Autrement
dit : le releve des onze zones continue de rendre ce que le jeu demande, table par table, et la
foret ne remappe pas que les gestionnaires d'entite.

### Deux moities, aucun etat

271 octets, absente de `Blaze.c`. La fonction est **rejouee en entier a chaque image et ne retient
rien** — pas de `+0x10`, pas de compteur.

**La poussiere du pas.** L'animation courante du membre est prise dans `0x005e9680` (les quatre
bundles d'animation, indexes par `+0x4`), puis par le clip `+0x18`. Son champ `+0x16` **divise par
deux** donne la periode, et quand l'image courante `+0x1a` tombe dessus, un effet `0xFE` nait. **La
cadence de la poussiere vient du clip, pas d'une constante** : elle suit le cycle de marche.

La division est **non signee** (`movsx` puis `div`, pas `idiv`) : une image negative devient un tres
grand nombre au lieu de rester negative. Transcrit tel quel.

**Le sol qui brule.** Quand les **trois bits bas** de `+0x150` du bloc de personnage valent **4** —
le type de terrain — un effet `0x2A` nait. Et `+0x0a` est mis a zero le temps de la naissance puis
rendu : `SpawnStatusEffectEntity_0047AC40` recopie l'entite entiere, donc l'effet heriterait de son
orientation. **Meme geste que `ApplyPercentDamage_005397B0` (suite 9), et pour la meme raison** —
c'est un motif, pas une coincidence.

### Le jeu fabrique une instruction de script sur la pile

C'est la trouvaille de ce corps. Sept octets sont ecrits dans un tampon local —
`{0, 0, 0, porteur[0x0a], 0x0d, 4, 0xf7}` — puis passes a `FUN_00513FE0`, qui n'est **qu'une
enveloppe** :

```c
void FUN_00513fe0(a, b) { FUN_00515790(DAT_005e97cc, 0, a, b); }
```

`FUN_00515790` est le **gestionnaire de l'opcode 10** de l'interpreteur, deja transcrit sous
`ScriptOpAluChunkRecords`. Le jeu ne lance donc pas un script du disque : il **assemble une
instruction en memoire et appelle son gestionnaire directement**, avec le contexte de zone et une
entite nulle.

Cela change la facon de lire les gestionnaires d'opcode : ce ne sont pas seulement les 39 cibles
d'une table de sauts, ce sont des **primitives appelables**. Le huitieme octet du tampon n'est ni
ecrit ni lu — `ScriptOpAluChunkRecords` s'arrete a `s[6]`, ce qui confirme au passage la longueur
de sept octets deja transcrite.

### Non exerce

Compile, pas execute.
