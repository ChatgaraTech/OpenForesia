# Blaze reverse-engineering status

Tracks progress reconstructing `Blaze.c` (Ghidra pseudocode, read-only) into
`src/Blaze/Blaze.cs`. Read `Prompt.md` and `Strategy.md` (repo root) for the
methodology before continuing this work. Strategy.md now also covers the
decompiled DLL sources (`3dhelp.c`, `gldhelp.c`, `playst.c`, `winsys3d.c`,
`wsyskern.c`, repo root, **read-only**) and `Documentation/DLL/<dll>.md` as
the running memory of what's been learned about each — check there before
re-analyzing an external call, and update it when a new one is understood.

**The binary is now in the repo at `Legacy/Blaze.exe`** — the "systemic blocker"
section below is resolved, and `Documentation/DataSymbols.md` holds every value
recovered from its data sections. Read more with
`OpenForesia.Tools at Legacy/Blaze.exe <va>`.

Build: `dotnet build OpenForesia.sln` — builds clean as of this update.
Run: the app targets **x86** but the only SDK here is x64, so building and
running need *different* environments and cannot share one shell:

- **Build** needs `DOTNET_ROOT=C:\Users\Sagara\.dotnet` on the x64 SDK.
- **Run** needs that variable **unset**. Launch the built x86 apphost directly —
  `OpenForesia\bin\Debug\net9.0\OpenForesia.exe` — and it resolves against the
  x86 runtime in `C:\Program Files (x86)\dotnet` (Microsoft.NETCore.App 9.0.8,
  runtime-only). `dotnet run`, or any launch with `DOTNET_ROOT` still pointing
  at the x64 install, fails with `0x800700C1` (bad image format) because the
  x86 apphost tries to load the x64 `hostfxr.dll`.
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
6. **`DAT_005db85c`** (main window `HWND`) — **searched exhaustively across all six sources; not found anywhere.** Findings:
   - In `Blaze.c` it is **read twice and never written** (`FUN_00523120(0, DAT_005db85c, …)` and `DestroyWindow(DAT_005db85c)`).
   - `Blaze.c` contains exactly **one** `CreateWindowExA` call — the movie player's hidden `"VfwClass"` helper window (TODO #14), which is *not* it.
   - `winsys3d.c` contains **no** `CreateWindow`/`RegisterClass` at all.
   - `wsyskern.c`'s `"HugoXX"` window is a throwaway parent for an error `MessageBoxA`, immediately destroyed.
   - `3dhelp.c` **receives** an `HWND` from its caller (stores it at device+0x18 and validates it with `IsWindow`); it never creates one. Its `GetDesktopWindow` use is only for `GetDeviceCaps`.
   - **CORRECTED — this next claim is exactly backwards.** `Ordinal_789(0, 0)` is the *real* 3D device initialiser: passing 0 as the first argument is what **triggers** full init (it loads `3dhelp.dll`, resolves `Create3DDevice`, and on failure shows "3D-Graphics initialisation error" then `ExitProcess(3)` — `winsys3d.c:5966`). Observed firing for real once this reconstruction held DirectDraw exclusively and never released it. The non-zero values are the *teardown* paths (-1 / -2). Original claim, wrong: ~~`winsys3d.c`'s `Ordinal_789` device-init branch is never reached: both `Blaze.c` call sites pass `0` as its first argument.~~

   Conclusion: the main window's creation site is **not present in any available source**. Most likely it lives in a `Blaze.exe` code path Ghidra rendered as a bodiless `LAB_`, or is reached through one of the unresolved indirect calls. This is the main reason nothing can render yet — see the "Reality check" section.
7. **atexit() registration**: `DoExit` walks a table (`DAT_00607950`..`DAT_0060794c`) that nothing currently populates — no `atexit()` call sites have been reconstructed yet in the reachable call graph. Revisit if/when one turns up.
8. **Ordinal 790's callback** (`Blaze.c`: `&LAB_00512ef0`, passed to `SetDeviceLostCallback`): no decompiled function body anywhere in `Blaze.c` — Ghidra only ever saw it address-taken. Cannot be reconstructed without further disassembly. `winsys3d.c` confirms the DLL does call it back later (see `Documentation/DLL/winsys3d.md`), so this matters once device-loss/reset handling comes up.
9. ~~**`DAT_005db900`/`DAT_005db758[50]`** interface identity~~ — **RESOLVED**. Reading the shutdown path (`FUN_00512ff0`/`FUN_00512ea0`) and the buffer-creation path (`FUN_00513030`, `FUN_005133xx`) exercises enough distinct vtable slots to pin both interfaces against the documented DirectX 3–5 COM layouts with no ambiguity: `DAT_005db8e4` is **`IDirectSound`** (0x08 Release, 0x0c CreateSoundBuffer) and `DAT_005db900`/`DAT_005db758[50]` are **`IDirectSoundBuffer`** (0x08 Release, 0x24 GetStatus, 0x2c Lock, 0x30 Play, 0x3c SetVolume, 0x48 Stop, 0x4c Unlock). This retroactively confirms the 0x3c=SetVolume reading in `ApplyCdVolume`/`ApplyAllSoundBufferVolumes`. The 50-slot count is confirmed independently by `FUN_00512ea0`'s loop bound (`0x005db758`..`0x005db81f`). *Update, this session*: the **buffer**-creation paths are now reconstructed (`AllocateSoundBuffer`, `StartMusicStream`), and a third interface was added to the set — `DAT_005db904` is **`IDirectSoundNotify`** (0x08 Release, 0x0c SetNotificationPositions), pinned by `StartMusicStream` obtaining it via `QueryInterface` on the streaming buffer and then using it only with the argument pair `(8, &DAT_005db8a0)` where `DAT_005db8a0`..`DAT_005db8df` has just been filled as exactly 8 `{ dwOffset, hEventNotify }` pairs. *Still open*: the **device** itself (`DAT_005db8e4`) still has no assignment site anywhere in the reconstructed graph — no `DirectSoundCreate` call has been located — so every entry point still takes its "no device" early-out at runtime. See TODO #26.
10. **AU_00564a05 blob**: `s_SmallTextureFlag` only reconstructs one 4-byte field (`._51_4_`) of what's clearly a larger structure/array (`AU_00564a05`) — the rest of that blob hasn't been characterized.
11. **RESOLVED** — recovered from `Legacy/Blaze.exe`, see `Documentation/DataSymbols.md`. The options-file extension (`DAT_005630bc`) is **`.cfg`, not `.ini`**, which is why `LoadOptionsIni` had never found a file; `DAT_005630a0` is `CdVol` (the former `UnknownIniKey_005630a0` placeholder); `DAT_005630c4` is `%d`. The full key set is `Size`, `Small`, `Filter`, `Gamma`, `CdVol`, `SoundVol` under section `Options`, corroborated by the installed game's own `BLAZE.cfg`. Original note kept below for context.

    *Update, this session*: this entry was marked RESOLVED but the finding had **never been carried into `Blaze.cs`**, which kept emitting the placeholder key `UNKNOWN_REVERSE_005630a0` — so the reconstruction wrote a duplicate of `Size` into the options file and read its own copy instead of the game's. Now applied (`SizeIniKey = "Size"`, `DAT_00563080` re-read from the binary to confirm). Two further corrections found while verifying: `s_Joy_d_00563064` is **`"Joy%d"` with no space** (the reconstruction used `"Joy %d"`, so every joystick setting silently fell back to its default), and the address in the old placeholder name was `CdVol`'s, not the key's. **Lesson: a RESOLVED entry here is not evidence the code applies it** — check the call site, not only the note.
    ~~**`DAT_00563080` ini key / `DAT_005630bc` ini extension / `DAT_005630c4` format string**: none of these three have a Ghidra-derived string name (unlike e.g. `s_SoundVol_005630b0`), so their exact text is a placeholder/guess in `Blaze.cs` (`UnknownIniKey_005630a0`, `IniFileExtension = ".ini"`) — see inline TODOs.~~
12. ~~**`DAT_00562e00` per-resolution clamp table**: content unknown; `s_ResolutionDetailLimit` in `Blaze.cs` is a placeholder filled with `int.MaxValue` (i.e. the clamp is currently a no-op).~~ — **RESOLVED**, read back from `Legacy/Blaze.exe` as `{ 1, 7, 11, 16, 23, 0 }` and applied. Cross-checked against the installed `BLAZE.cfg`, which pairs `Resolution=4` with `Size=23`: index 4 is 23, so the clamp is a no-op on a file the game itself wrote — exactly as expected. The bytes immediately after (`01 02 03 04 05 06 07 08`) are a different table, so the array ends at six entries. The trailing `0` is a **terminator, not a sixth entry**: running with `Resolution=5` makes `winsys3d`'s base-offset table (`DAT_00a2c49c`, indexed by the same value) return junk — viewport `(176,133) 288x216` against the clean progression `320x240 / 512x384 / 640x480 / 800x600 / 1024x768` for indices 0..4. Five valid entries, indexed 0..4.
13. **Vtables now fully identified — the subsystem is DirectShow, and none of it is unrecoverable.** All fifteen previously-undisassembled targets were forced in Ghidra and exported to `Legacy/Raw/*.c`. What they show:
    - **The movie player is DirectShow / ActiveMovie, not VfW.** `FUN_00543230` calls `CoCreateInstance` with the GUIDs at `DAT_0054e8a8` / `DAT_0054e8b8`, dumped from the binary as **`{E436EBB3-524F-11CE-9F53-0020AF0BA770}` = `CLSID_FilterGraph`** and **`{56A868A9-0AD4-11CE-B03A-0020AF0BA770}` = `IID_IGraphBuilder`**. That also explains `RunApplication`'s `CoInitialize`, and the objects at `+0x11c` whose vtable slots `0x1c`/`0x20`/`0x24` are used by `FUN_005438a0`/`FUN_005438f0`/`FUN_00543920` — an `IVideoWindow`-shaped interface.
    - **The overlay object (vtable `0x0054D028`) is a COM callback sink**: slots 0-2 are adjustor thunks (`FUN_00512E60`/`E80`/`E90`, all forwarding to `this - 8`, i.e. multiple inheritance), slot 3 is the real callback (`FUN_00512DD0`, returning `E_POINTER` on a null inner pointer), slot 4 a no-op, slot 5 posts `0x465` to the player window.
    - **`FUN_00547680` is `_purecall`** (`__amsg_exit(0x19)`). It fills every unimplemented vtable slot — 11 occurrences across two vtables — so hitting one aborts by design, and those slots need no reconstruction.
    - `FUN_00512a10`, `FUN_00512b40`, `FUN_00512c80` and `FUN_005431e0` all already had bodies in `Blaze.c`.
    **Correction to an earlier claim in this file and in session notes**: recovering the overlay vtable does *not* by itself make anything appear on screen. `FUN_00542c40`'s blit dereferences `*(wrapper + 4)`, which `FUN_00512930` explicitly sets to **0**; it is `FUN_00512a10` — the real DirectDraw surface/cooperative-level sequence — that populates it. The display path is `FUN_00512a10`, not these callbacks.
    Original note: ~~**Movie/intro-video subsystem's two custom C++ vtables** (`PTR_LAB_0054d078` — the DirectDraw-wrapper class constructed by `FUN_00512930`/`FUN_00512d10`, and `PTR_LAB_0054d0a0` — the video-stream class constructed by `FUN_005431e0`/`FUN_00542fc0`): every vtable slot these point at (`LAB_0054dxxx` targets) has no decompiled body anywhere in `Blaze.c` — same class of gap as ordinal 790's `&LAB_00512ef0` callback, just a whole class's worth at once. `TrySetupMovieStream`/`OpenMovieStream`/`RewindMovieFallbackFrame` in `Blaze.cs` stop exactly at these vtable calls; see the "Movie / intro-video subsystem" banner comment in `Blaze.cs` for the full writeup of what *is* independently confirmed (real DirectDraw vtable offsets matching the public `IDirectDraw`/`IDirectDrawSurface` v1 layout, real Win32 window/message-pump/joystick-capture behavior).~~
14. **RESOLVED.** `0x00542600` was never *called*, only stored into `WNDCLASSA.lpfnWndProc`, so Ghidra labelled it and left the bytes undisassembled. Forcing disassembly (`G` → address → `D` → `F`) produced a clean decompilation immediately — see `Legacy/Raw/FUN_00542600.txt`. It is an ordinary window procedure, and its message set (WM_KEYDOWN/Esc, the three mouse buttons, both joystick buttons, WM_TIMER, WM_ACTIVATEAPP, WM_CLOSE, MM_MCINOTIFY) is exactly the set the attract screen waits on. All four of its handlers — `FUN_00542970`, `FUN_005429e0`, `FUN_00542a00`, `FUN_00542560` — already had bodies in `Blaze.c`. `CreateMoviePlayerWindow` now registers the class and creates the window for real; the full `FUN_00542740` listing supplied the cursor (wsyskern resource 10), `WS_EX_TOPMOST`/`WS_POPUP`, and the `ShowWindow`/`MoveWindow` tail. Original note: ~~**Movie window procedure** (`Blaze.c`: `&LAB_00542600`, the `WNDPROC` for window class `"VfwClass"`): no decompiled body. `CreateMoviePlayerWindow` in `Blaze.cs` therefore does not actually call `RegisterClassA`/`CreateWindowExA` (would crash on the first dispatched message with no real `WndProc`); `HWnd` stays `IntPtr.Zero`. This is also *why* `DAT_005db85c` (TODO #6 above) still can't be threaded through — the movie window isn't the main window anyway (it's a separate 2000x2000 hidden helper window), so the real main-window creation is presumably elsewhere, still unlocated.~~
    *Still true and worth keeping*: this is the **movie player's** window, not the main game window. `DAT_005db85c` (#6) remains unlocated.
15. **RESOLVED AND WIRED.** `s_MainLoopTickCallback` now points at `RunGameStateTick`, the reconstruction of `FUN_00537ca0`, so `RunGameLoop` no longer throws on its indirect call. The tick's prologue is real (`Zoom8BitTextures = 0` via the winsys3d data export, then `ReInitializeAllTextures()`), and its eleven state handlers are named stubs — **all eleven have decompiled bodies in `Blaze.c`**, so they are ordinary uncovered code rather than blocked work, and each stub names its Ghidra function so the next state to reconstruct is obvious from the exception text. The `default:` case reproduces the original's deliberate store to address 0 as an exception rather than by faulting. *Not yet reachable at runtime*: `LoadFirstLevelResources` throws at `DecodeResourceChunk` (#19) before `RunGameLoop` is entered. Original note: ~~**`PTR_FUN_0056c96c`** — **The pointer holds `0x00537CA0`, and `FUN_00537ca0` is already decompiled in `Blaze.c` at line 83364** — so the game loop's tick needs no Ghidra re-run at all. It is the **per-game-state dispatcher**: it clears `Zoom8BitTextures`, calls `ReInitializeAllTextures()`, then switches on `DAT_005de510` across at least 7 states (`FUN_00542130`, `thunk_FUN_00536180`, `FUN_00542160`+`FUN_00537da0`, `thunk_FUN_00516020`, `FUN_005066d0`, `FUN_00504c90`, `FUN_00524180`). Note this makes `FUN_0052a440` — long assumed to be the state dispatcher — not the one the tick actually reaches. The adjacent pointer at `0x0056c970` holds `0x00537d50`, which is **not** in `Blaze.c` and would need a scoped re-run if it matters. Wiring the tick into `RunGameLoop` is a separate change; `RunGameLoop` still throws until then. Historical reasoning that led here, kept because it was sound: `0x0056c96c` occurs exactly once in all 98k lines of `Blaze.c`: at the call site itself. It is therefore not a runtime-assigned variable at all but a **statically initialised `.data` function pointer** — the linker wrote the target address into the image, and Ghidra's `PTR_<KIND>_<address-of-pointer>` naming (compare the confirmed `PTR_s_Blaze___Blade_00563208`, `PTR_PTR_00566d68`, `PTR_FUN_0056b4d0`) tells us the slot holds an unnamed function pointer but not *which* function. Its neighbours (`s_FEMALE_0056c95c`, `DAT_0056c964`, `DAT_0056c978`) confirm it sits in initialised data. **No amount of further reading of `Blaze.c` can recover it** — see the "Systemic blocker" section below. Guessing a plausible target would violate Strategy.md's "ne jamais inventer une signature/un comportement", so `RunGameLoop` throws rather than silently skipping the tick.~~
16. **`DAT_005633a8`** (mode flag read pervasively, including by `FUN_00529eb0`/`InitViewportSurfaces`): no assignment site reached yet either; always 0 (its default) in the current reconstruction.
17. **Font-loading filename** (`Blaze.c`: `s_DATA_Font_bmp_005684cc`) and the three intro-movie filenames (`s_TEPS_AVI_005684f4`/`s_OPENING_AVI_005684e8`/`s_ANI01_AVI_005684dc`): only Ghidra's auto-derived symbol names survive, not the literal bytes — `"DATA\FONT.BMP"`/`"TEPS.AVI"`/`"OPENING.AVI"`/`"ANI01.AVI"` in `Blaze.cs` are best guesses from the symbol names, same caveat as TODO #4's message strings.
18. **`FUN_00529b00`/`LoadWholeFileToBuffer`'s incomplete Ghidra analysis**: the failure path returns `unaff_ESI` (a register Ghidra never attributed a value to at function entry — genuinely indeterminate, not just unnamed) and the read call shows a literal `2` (`Ordinal_334(iVar1,2,iVar2)`) exactly where a destination-buffer argument should be, one parameter short of the two arguments both real call sites actually pass. Reconstructed using the evident 2-argument intent (`dest` as that argument, `0` as an explicit safe failure return) — flagged rather than presented as certain.
19. **RESOLVED — and the "genuinely unrecoverable" claim below was wrong.** Reconstructed in full from the raw disassembly (`Legacy/Raw/FUN_005322f0.txt`, exported from Ghidra). The function uploads a **TIM texture** to VRAM and records its texture-page / CLUT pair in a slot: `Ordinal_677`/`Ordinal_662` are libgpu's `OpenTIM`/`ReadTIM`, and the 20 bytes the prologue reserves (`SUB ESP,0x14`, handed over as `LEA ECX,[ESP+8]`) are a `TIM_IMAGE` — which is what the "indeterminate" `puStack_14`/`psStack_10`/`iStack_c` locals really were.
    **What the decompiler got wrong, beyond naming**: `unaff_retaddr` is parameter 2 (the 2-short slot, corroborated by both call sites' 4-byte stride); every `param_1[0..2]` byte read actually comes from `EDI` = parameter 3, the metadata triple, *not* the chunk pointer; and `(*param_1 & 0xff10)` is an artifact of a `AND AL,0x10` byte mask — using it would corrupt the pixel rect's y coordinate. The `extraout_var*` values were just `Ordinal_674`'s ignored return.
    **Lesson**: "Ghidra's own analysis is incomplete" was a correct observation turned into a wrong conclusion. Incomplete decompilation meant *read the disassembly*, not *give up*. Three of this file's remaining blockers (#13/#14/#23) are the same shape and should be treated the same way. Original note: ~~**`FUN_005322f0`/`DecodeResourceChunk` — genuinely unrecoverable, not just unread**: several of its inputs are `unaff_ESI`/`unaff_DI`/`unaff_retaddr` and outputs `extraout_var`/`extraout_var_00`/`extraout_var_01` in Ghidra's own decompilation — register-allocated values Ghidra could never tie to a defining instruction or a real variable. This is a stronger category of gap than the usual "not yet read": Ghidra's *own* static analysis of this function is incomplete, so no amount of re-reading `Blaze.c` will resolve it without disassembling the raw instructions. **Current blocking point** for `LoadFirstLevelResources` (thrown on the first directory entry, if the loaded "SYSTEM.INI"-equivalent file lists any).~~
20. **RESOLVED — schema recovered *and* its reading confirmed.** `DAT_0056aca8` holds 37 `ushort`s ending at a `0x0000` terminator. Each entry is **`(low byte = index, high byte = stride)`**: the low byte selects `header[4 + index]` (a pointer the fixed pass already relocated) and the high byte is the byte stride for walking the chained list it heads. This is settled by `FUN_00532070`'s own `& 0xff` / `>> 8`, not inferred from the byte pattern — an earlier `(kind, size)` guess was wrong. The schema-driven pass is now reconstructed and is no longer a no-op. **Watch the two relocation formulas**: the fixed pass adds `header[0]`, the schema pass adds `header[0] + header[1]`; using the shorter one yields plausible-looking pointers that are off by a constant. Full table and analysis in `Documentation/DataSymbols.md`. Original note: ~~**`DAT_0056aca8`** (resource-fixup schema table read by `FixupResourceHeader`/`FUN_00532070`): data-only global, byte content not present in `Blaze.c`'s text (same category as `DAT_00562e00`/TODO #12) — `s_ResourceFixupSchema_0056aca8` is an empty array, so the schema-driven fixup pass is a safe no-op rather than a guess.~~
21. **`DAT_006008c8`/`DAT_00604930`/`DAT_00600904`** (the 3 comparison targets in `IsKnownOverrideDestination`/`FUN_0052a660`): none have an assignment site anywhere in the reconstructed call graph yet, so they're always null/zero; the 2 true-branch bodies (native override-table installers, one pulling in 11 further `FUN_` calls) are stubbed rather than translated.
22. **`PTR_s_BLAZE_ALL_1_00566bbc`/`PTR_s_SYSTEM_INI_1_00566bb8`/`s____snd_Base_snd_00568500`** filenames used by the resource-loader subsystem: same category as TODO #17 — only Ghidra's derived symbol names survive; `"BLAZE.ALL"`/`"SYSTEM.INI"`/`"\SND\BASE.SND"` in `Blaze.cs` are best guesses.
23. **Body recovered — reconstructible whenever wanted.** `0x00513660` was forced in Ghidra and decompiled cleanly to `Legacy/Raw/FUN_00513660.c`. It is a straightforward streaming pump: `WaitForMultipleObjects(9, &DAT_005db838, FALSE, INFINITE)` over the 8 notification events plus the stop event (index 8 breaks and `ExitThread`s), then `IDirectSoundBuffer::Lock` (vtable `0x2c`), a decode of 10 MS-ADPCM blocks through `FUN_00531bb0` — **already reconstructed as `DecodeMsAdpcm`** — and `Unlock` (vtable `0x4c`), with explicit wraparound handling when fewer than 10 blocks remain. Every dependency exists. *Note*: reconstructing it would have no observable effect yet, because `StartMusicStream` early-outs without an `IDirectSound` device (#26). Original note: ~~**Music-streaming worker thread** (`Blaze.c`: `&LAB_00513660`, passed to `CreateThread` by `FUN_005131d0`): no decompiled body anywhere in `Blaze.c` — address-taken only, exactly the same class of gap as the movie `WNDPROC` (#14) and ordinal 790's callback (#8). `StartMusicStream` therefore does **not** call `CreateThread` (jumping to an address we cannot reconstruct would crash), same precedent as `CreateMoviePlayerWindow` skipping `CreateWindowExA`; `s_MusicStreamThread` stays `IntPtr.Zero`. Consequence: everything else in the start path is real, so a track *would* begin playing, but nothing ever refills the buffer — the pre-decoded 80 blocks would loop forever instead of streaming on. This is also why `DAT_005db860`/`DAT_005db750`/`DAT_005db89c` (source-block cursor, total block count, playing flag) and the 8 notification events are written but never read in the reconstruction: the worker is their only consumer. Resolvable by re-running Ghidra with `0x00513660` forced to "disassemble as function".~~ — which is exactly what was done.
24. **`DAT_0056ac60` (16 shorts) / `DAT_0056ac80` and `DAT_0056ac90` (7 shorts each)** — the MS-ADPCM adaptation table and the two coefficient tables. Data-only globals, so the bytes aren't in `Blaze.c` (same systemic blocker as #12/#20), but unlike those two the *identity* here is not a guess: `FUN_00531f70` only accepts a `fmt ` chunk with `wFormatTag == 2` (WAVE_FORMAT_ADPCM), `wBitsPerSample == 4` and `wNumCoef == 7`, then compares `aCoef[0..6]` against `DAT_0056ac80`/`DAT_0056ac90` — i.e. it verifies the file uses the *standard* coefficient set so the decoder's hardcoded tables apply. The addresses corroborate the sizes exactly (16 shorts at `0x0056ac60` end at `0x0056ac80`; 7 shorts at `0x0056ac80` end at `0x0056ac8e`, padded to `0x0056ac90`). `Blaze.cs` uses the documented standard MS-ADPCM values. Still worth confirming byte-for-byte if the binary appears.
25. **`FUN_00513030`'s register-attribution bug**: Ghidra writes `piVar3` (loaded from `DAT_005db8e4`, the *device*) into the slot table and releases that same pointer when the table is full, while the newly created buffer sits unused in `CreateSoundBuffer`'s `local_2c` output parameter. Storing the device in a buffer slot would make `ReleaseAllSoundBuffers` call `IDirectSoundBuffer` methods on an `IDirectSound` and release the device up to 50 times, so the decompiled text cannot be what the binary does. `AllocateSoundBuffer` transcribes the evident intent (the *created buffer* is stored and released) — flagged rather than presented as certain.
26. **`DAT_005db8e4` (the `IDirectSound` device) has no creation site**: read in four places, assigned nowhere in all of `Blaze.c`'s reconstructed graph, and no `DirectSoundCreate` call site has been located. Same shape as `DAT_005db85c` (#6, the main window handle) and quite possibly the same root cause — both would plausibly be created by whatever code also creates the main window, which #6 established is not present in any available source. Until it is found, the whole audio subsystem is correct but inert: every entry point takes its "no device" early-out.
27. **`DAT_005631d4` initial value** (music enabled/disabled at startup): data-section content, so unavailable; `s_MusicEnabled` defaults to 0, meaning music starts disabled and `StartMusicStream` early-outs until `ToggleMusicEnabled` flips it. Note `DAT_005631d0` (the "no stream running" flag) needed no such assumption — a first `StartMusicStream` always routes through `StopMusicStream`, which sets the flag before it is tested.
28. **`FUN_0053da20`/`FUN_0053db20` (positional sound gain)** — the remaining members of the sound-effect family, deliberately left untranslated. `FUN_0053da20` computes a left/right gain pair from a world position via the cos table (ordinal 450) and `FUN_0053db20` feeds the result to `PlaySoundEffect`, but Ghidra declares `FUN_0053da20` with **2** parameters while `FUN_0053db20` calls it with **3**, and its body reads `unaff_SI` and `unaff_retaddr` — register-passed values never tied to a parameter. Reconstructing it would mean inventing a signature, which Strategy.md forbids; it needs a Ghidra re-run with the calling convention corrected (same class as the `__fastcall`/`__thiscall` cases noted under the systemic blocker).
29. **Sound-directory second half (`DAT_005e8520`, entries `0x3c`..`0x4c`)** is loaded by `FUN_00512xxx` (`Blaze.c:63354`), which is not reconstructed — so those 17 entries stay zero and sound ids `0x3c`..`0x4c` (which include the four fixed UI sounds `PlayUiSound38`..`3B`) currently resolve to a zero-length sample and play silence. Not a data-availability problem: it is ordinary uncovered code, and the same call site also fills the level sample bank `DAT_00600900`.
30. **RESOLVED — table recovered and `PlayMusicTrack` reconstructed.** The entries are CD-**root** absolute templates (`@:/Music/Mel00_01.WAV`), drive-letter patched at index 0, recorded in `Documentation/DataSymbols.md`. Only the sampled entries are transcribed so far; call sites index up to at least `0x26`, and unsampled slots stay null so `PlayMusicTrack` returns rather than reading past the table. **Nothing is audible yet**: no `DirectSoundCreate` site exists (#26), so `StartMusicStream` takes its no-device early-out. Silence here is expected, not a defect. Original note: ~~**`PTR_s____Music_Mel00_01_WAV_00566cc8`** — the music-filename pointer table indexed by `FUN_00529ad0`/`PlayMusicTrack`, the only caller of `StartMusicStream`. Call sites pass indices up to at least `0x26`, and `FUN_00529ad0` patches each path's first character with the CD drive letter (`s____Blaze_Blade_005631e8[0]`) before playing. Data-only table, so it is not reconstructed and `FUN_00529ad0` is deliberately left untranslated rather than filled with invented filenames — same category as #17/#22, but a whole table rather than single strings.~~

36. **`FUN_00545340` — the menu/UI engine, deliberately deferred.** 1742 lines, three parameters, 25 calls, reached twice per menu frame alongside `FUN_005443a0` (233 lines). It is ordinary uncovered code with a full decompiled body, not blocked work, but it is a project in its own right and reconstructing it blind — with no way to observe whether the surrounding frame order is right — would have been the wrong order of operations. Stubbed along with `FUN_005443a0`, `FUN_00512320` (the options page) and `FUN_005048f0` (joystick state processing, which pulls in `FUN_005231f0`/`FUN_00523660`).

31. **Dropped return values silently delete control-flow edges — a startup hang, now fixed.** `FUN_00542f30` is declared `void` by Ghidra (`Blaze.c:90063`) even though it tail-calls `FUN_00542c40`, which Ghidra itself types `undefined4 __fastcall`, and both call sites in `FUN_00529c10` read the result (`Blaze.c:74442`, `74450`). The reconstruction had followed the `void` declaration, which removed **two of the attract loop's four exit conditions**; combined with the other two being dead (`PlayMovie` always reports 0 while `TrySetupMovieStream` is a stub), `RunMainLoop` spun forever at 100% CPU and startup never reached engine init. Fixed by trusting the call site over the declaration and reconstructing `FUN_00542c40`'s bitmap-load prologue, whose `return 1` on a missing `TITLE640.BMP` is the original's own way out of the loop when there is no CD.
    **Rule this establishes**: when Ghidra's declaration and a call site disagree about a return value or a parameter, the *call site* wins. A stub must still return a value if its result is one of a loop's exit conditions.
    **The same defect is still latent** in `PlayMoviePumpLoop` (`FUN_00542b70`): its `do { … } while (*param_1 != 0)` terminates only when the movie-stream vtable callback clears the playing flag (TODO #13), and is currently unreachable *only* because `TrySetupMovieStream` returns false. Enabling that stub without a completion signal re-hangs startup in a tight `PeekMessage` spin. A warning comment now sits at that loop in `Blaze.cs`.

32. **`FUN_00529eb0`/`InitViewportSurfaces` was writing through a null pointer — fixed, and the ordinals are now identified.** The reconstruction passed `null` to `Ordinal_661` (under a `TODO(REVERSE): exact 3rd struct target not modeled`), passed stack scratch buffers instead of the four global structs, and had the argument order rotated on all four calls — a native `0xC0000005` that killed the process the moment the attract-loop fix made this function reachable. Root cause: Ghidra spills these calls' arguments into unattributed stack slots (`uStack_18`..`puStack_50`) instead of rendering them as arguments, and gives the fourth call only three of its five. Reading the slots back in ascending-address (cdecl push) order recovers all four exactly.
    **Identification, now certain**: winsys3d.dll ordinals 658/661/665/674 are a port of the PlayStation SDK's **libgpu** — fitting, since the sibling DLL is literally named `playst.dll`. Ordinal 658 is `SetDefDispEnv(DISPENV*, x, y, w, h)` and 661 is `SetDefDrawEnv(DRAWENV*, x, y, w, h)`; their bodies write the documented DISPENV/DRAWENV field layouts field for field. The four globals `Blaze.c` passes them (`DAT_005e9880`, `DAT_005e98dc`, `DAT_005e98f0`, `DAT_005e994c`) are spaced **92 / 20 / 92 / 20 bytes** apart — exactly `sizeof(DRAWENV)` / `sizeof(DISPENV)` — and all 12 of the follow-up global stores land on valid fields of those layouts. Three independent confirmations, no guessing. The setup is standard PSX double buffering at 320x240 (`0x140`x`0xf0`): display one buffer at y=0 while drawing the other at y=240, and swap.
    Ordinal 674 is `LoadImage(RECT*, data)` (its `rect.y > 0x1ff` early-out confirms 1024x512 VRAM). Ordinal 665 is `ClearImage`-shaped but its colour math decompiles as `((p4 & 0xff) << (p3 + 10)) << (p2 + 5)`, which does not match libgpu's plain `(r,g,b)` — deliberately left un-renamed until that is explained.
    *Still open in this function*: the `DAT_005633a8 != 0` branch calls `Ordinal_674` with a RECT (`uStack_58`) whose `x` advances by 0x40 per iteration and wraps at 0x400; the reconstruction's `tileRect` is never initialized and its `frameOffset` is tracked separately from the rect it belongs to. That branch is currently unreachable (`GetMainMemPtr()` returns null, TODO #33), so it is flagged rather than fixed.

33. **`GetMainMemPtr()` returns null** — winsys3d.dll's `MainMem` export is not yet resolved, so the entire second half of `InitViewportSurfaces` (the framebuffer checkerboard fill and the 0x20-iteration `LoadImage` tile upload loop) is skipped by an early return, and any other code reading main video memory will do the same. Now that startup runs end to end this is the most visible remaining gap below the game loop itself.

34. **RESOLVED — properly this time, and the deviation is gone.** With `FUN_00542600` disassembled (`Legacy/Raw/FUN_00542600.txt`) the movie player's window procedure is reconstructed, so `CreateMoviePlayerWindow` now performs the real `RegisterClassA`/`CreateWindowExA` and the attract screen can receive input. The piece that had been missing all along is `FUN_00542a00`: on WM_KEYDOWN(Esc), any mouse button, or either joystick button it sets `*player = 0` (Playing) and `player[0xb] = 1` (Result) — which is precisely what lets `PlayMoviePumpLoop` return and `PlayMovie` report non-zero, giving the attract loop its exits. `ShowTitleScreenAndWait`'s 20-second `timeGetTime` wait and message pump are reconstructed too. `OpenMovieStream` is back to its faithful `return true`.
    Measured: the attract sequence runs for real and terminates, and startup proceeds into the game loop. *Not yet determined*: which of the exits fires in practice — a joystick event (`joySetCapture` now succeeds against a real window) or the 20 s timeout. Worth instrumenting if the intro timing ever matters.
    Superseded note: ~~**Startup unblocked, but by a deliberate deviation — and the earlier justification here was wrong.** `OpenMovieStream` now reports failure, which makes `RunMainLoop` skip the intro movies and the attract loop, so startup proceeds. An earlier revision of this entry claimed that *was* the original's own no-DirectDraw path. **It is not.** `FUN_00512930`'s disassembly (`Legacy/Raw/FUN_00512930.txt`) ends `MOV EAX,ESI; RET 0x4` — it returns `this` unconditionally, even when its internal `Malloc` fails — so `FUN_00542800`'s `if (iVar1 == 0) return 0` only fires when the 0x48-byte allocation fails. In the original, the open succeeds.~~
    So this is a **known deviation**, kept because it is the only workable behaviour while the movie window cannot be created. It disappears once #13/#14 are disassembled. The diagnostic throw in `ShowTitleScreenAndWait` is retained as a backstop for exactly that moment: if the open is made to succeed without the window and input handling, the attract loop again has no exit and must say so rather than spin. Original note: ~~**The attract loop has no reachable exit once the CD is present — now a loud throw.** With the data root corrected, `ShowTitleScreenAndWait` loads `DATA/TITLE640.BMP` successfully and returns 0, and `PlayMovie` is pinned at 0 while `TrySetupMovieStream` is stubbed (#13). All four of the loop's exits therefore depend on unreconstructed subsystems, and the process spins at 100% CPU with no message pump — the same silent-misbehaviour shape as #31, arriving from the opposite direction. `ShowTitleScreenAndWait` now throws a descriptive `NotImplementedException` at the point where the original's 20-second `timeGetTime` wait and its key/mouse/joystick handling would be, rather than returning 0. The degradation is targeted: with no CD the bitmap load still fails, the function returns 1, the loop exits on iteration 1 and startup proceeds. **To clear this**, the real fix is the main window + `WNDPROC` (#6/#14) so input can actually arrive; reconstructing the 20 s timeout alone would produce a faithfully-infinite loop, which is no better.~~
    The guard itself is kept in `ShowTitleScreenAndWait` as a backstop: if a future change makes `OpenMovieStream` succeed again without also supplying the window and input handling, the attract loop would once more have no exit, and it should say so rather than spin.

35. **RESOLVED** — the schema-driven relocation pass is reconstructed (TODO #20), `LoadResourceEntry` no longer faults, and the directory walk now reaches `DecodeResourceChunk`. Original diagnosis kept below; it was correct. ~~**`LoadResourceEntry` faults on unrelocated directory pointers against real game data.** Measured with the CD mounted and the attract loop temporarily bypassed: `LoadFirstLevelResources` loads `SYSTEM.INI;1`, enters its directory-entry loop, and dereferences `entryPtrValue` — a raw value read straight out of the file — as a pointer, giving a `NullReferenceException`/AV in `LoadResourceEntry`. Cause is upstream and already documented: `FixupResourceHeader`'s schema-driven relocation pass is a no-op because `s_ResourceFixupSchema_0056aca8` is an empty array, so the file's internal offsets are never turned into real pointers. **This is now fixable** — `DAT_0056aca8`'s bytes were recovered and are recorded in `Documentation/DataSymbols.md` (#20); the `(kind, size)` reading of them is still a hypothesis and needs confirming against `FUN_00532070`'s use of the table. Until then `DecodeResourceChunk` (#19) cannot be reached, so it stays untested.~~

## ~~Systemic blocker~~ — RESOLVED: `Blaze.exe` is now in the repo

**This section previously said a whole class of work was impossible. It was wrong, and it
was the most misleading thing in this file** — it told every future session not to try.

`Blaze.exe` is now committed at **`Legacy/Blaze.exe`** (1,589,760 bytes, MD5
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
OpenForesia.Tools sections Legacy/Blaze.exe
OpenForesia.Tools at Legacy/Blaze.exe 0x005631f8 [bytes]
```

All recovered values are recorded in **`Documentation/DataSymbols.md`**, keyed by
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
- **Audio is complete but inert.** The whole stack is reconstructed now —
  gameplay call site → `PlaySoundEffect` → resampler → `IDirectSoundBuffer::Play`
  for effects, and file load → MS-ADPCM decode → streaming buffer for music —
  but nothing creates the `IDirectSound` *device* they all hang off (TODO #26),
  so every entry point early-outs. Even with a device, streaming music would
  loop its first 80 blocks forever until the worker thread (TODO #23) is
  recovered, and sound ids 0x3c..0x4c would be silent until the level-load code
  that fills the second half of the directory is reconstructed (TODO #29).
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
  tables are recovered verbatim in `Documentation/DataSymbols.md`. Only 5 of the
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
(`Legacy/Raw/FUN_0052a1e0.asm`, `FUN_0052a280.asm`). Ghidra gives `FUN_0052a1e0`
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
  Recovering it needs a **Ghidra re-run** on `Legacy/Blaze.exe` with corrected
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
  none is blocked on anything: `FUN_00542130` (state 0), `thunk_FUN_00536180`
  (1), `FUN_00542160`+`FUN_00537da0` (2), `thunk_FUN_00516020` (3),
  `FUN_005066d0` (4), `FUN_00504c90` (5), `FUN_00524180` (6), `FUN_00531b40`
  (7), `FUN_005315d0` (8), `FUN_0051bb00` (9), `FUN_00513ba0` (10). This is the
  largest body of straightforwardly readable code reachable from the spine.
  Note it is gated behind #19 at runtime, but not for reading.
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
- `FUN_0052a440` (per-game-state dispatcher) and the rendering/input functions
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

Everything the DLL side needed came out of `Legacy/Blaze.exe` rather than out of guesswork:
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
`FUN_0052a440`, the per-game-state dispatcher, as a descriptive exception. **That is the new
blocking point, and it is ordinary uncovered code.**

Three things worth carrying forward from `FUN_00523660`:

- **The button convention is active low.** A player's word starts at `0xffff` and every bound key
  that is down clears its bit. The 20 masks come from `DAT_00564f18`, read out of
  `Legacy/Blaze.exe`: unmistakably a PlayStation pad — `0x0010` up, `0x0020` right, `0x0040` down,
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
`Legacy/Blaze.exe` — `at winsys3d.dll <va>`, `at 3dhelp.dll <va>`, using the
very `@ 0xXXXXXXXX` addresses those `.c` files carry. Unlike `Blaze.c`, the DLLs
ship with their data sections in the repo, so a DLL symbol whose value lives in
initialised data is **never** blocked the way a `Blaze.c` one used to be: it is
one command away. This is how the display-mode table and the `3dhelp` vtables
were recovered on 2026-08-12 rather than guessed. Two caveats found in passing:
build the tool with the x64 SDK but **run the `net9.0` apphost** (the `net10.0`
output hits the same `0x800700C1` bad-image-format wall described at the top of
this file), and an address landing in a `.data` region that reads as zero on disk
means the table is built at runtime — an answer, not a failure.
