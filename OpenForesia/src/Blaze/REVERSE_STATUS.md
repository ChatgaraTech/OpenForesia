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
`Legacy/Blaze.exe`:

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
`FUN_0052a440` as a descriptive exception — since reconstructed, so the frontier has moved on to
game state 1. **That was the then-new blocking point, and it was ordinary uncovered code.**

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
  Recorded in `Documentation/DataSymbols.md`.

### Two globals were about to be misnamed

`DAT_005e96a0` and `DAT_005e9640` were first written as `s_SelectSpriteTable_*` /
`s_SelectPaletteTable_*`, as if they belonged to this screen. They do not: `FUN_005069e0` fills
them identically (`Blaze.c:78876`) and ~30 sites read them across `Blaze.c`. They are the global
**model** and **animation** tables — `DAT_005e9640` holds animations (frame counter wraps on the
entry's `+0x16` field, `Blaze.c:55840`; count read at `[-4]`, `Blaze.c:79042`), `DAT_005e96a0`
holds models (16-byte primitive records bucketed by type, vertex counts divided by three).
Renamed to `s_ModelTable_005e96a0` / `s_AnimationTable_005e9640` before the thirty read sites
arrive. Capacity is 24, not `0x40`: the two symbols are `0x60` bytes apart. Full layout in
`Documentation/DataSymbols.md`.

### The frame loop's eight handlers need a Ghidra re-run

`FUN_00506880` dispatches through `PTR_LAB_00560708`, an eight-entry table read out of
`Legacy/Blaze.exe`:

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
identical. The full table is now in `Documentation/DataSymbols.md`, along with the reading of the
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
`Documentation/DataSymbols.md`; the reconstruction is `Handler_00544e50`.

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
`Legacy/Blaze.exe` with `OpenForesia.Tools -- at` and recorded as `s_ClassStartIcon_00562ab8`,
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
`Legacy/Blaze.exe`/`Documentation/DataSymbols.md`), then `FUN_0051be90` (needs a forced-signature
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

Decoded by hand from `Legacy/Blaze.exe` (file offset `0x143db0`, 21 bytes to the `RET`), same
recipe as `0x06`/`0x09`/`0x1c`. Two operand bytes, writing verbatim into the low word of the
descriptor's `+0x20` "rect" dword that `EmitTextSprite` reads back to place the finished line's
sprite — `text[0]` becomes `rect & 0xff`, `text[1]` becomes `(rect >> 8) & 0xff`. The dword's upper
word (including the `0x1000000` enable bit `DrawUiPanel` tests before copying this descriptor's
text into a panel) is left untouched. Confirmed by build; wired into `BuildTextTable`'s render pass.

### Control code `0x01` was the *next* gap down, not a stub — a lesson in checking `Blaze.c` first

Fixing `0x04` let `playtest.ps1 -Trace` (via `OPENFORESIA_FORCE_SUBSTATE=7`) advance further into
the same text stream and crash on control code `0x01` instead, dispatching to `FUN_00544810`.
**Wasted real effort hand-disassembling it from `Legacy/Blaze.exe` before thinking to grep
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
`.data` value is `1` (confirmed by reading `Legacy/Blaze.exe` directly), so in the original this
gate was live far more often than our port currently reproduces. **Not fixed this session** — nine
call sites is real scope, and merging the two needs re-checking each site's branch against what the
capability bit is actually supposed to mean (still TODO(REVERSE) itself, per `CheckDeviceCapabilityFlag`'s
own entry above) rather than just renaming. Flagged here per the "grep the whole file before
declaring a symbol" rule this project already keeps failing to fully apply — worth a dedicated pass,
and worth checking first if any "geometry right, image wrong" report ever comes in (§7 of `TODO.md`).

### The return value is a real integer multiply, confirmed down to the byte, and looks like copy protection

`Blaze.c:91310`: `return (DAT_0056321c + 1) * (int)(param_1 + 3);` — every sibling text-control
handler just returns the advanced cursor; this one multiplies it. Confirmed byte-for-byte against
`Legacy/Blaze.exe` (file offset `0x143c10`..`0x143d6b`, no branch after the load, so this really is
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
`Legacy/Blaze.exe`'s `.data` directly at `DAT_00563280` (file offset `0x161880`) confirmed the
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

Read the real 8 bytes straight from `Legacy/Blaze.exe` (file offset `0x165230`): `{ 0x08504800,
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
`"save\"` and `"Blaze-Saving*"` — recovered directly from `Legacy/Blaze.exe` (`DAT_00563158`, file
offset `0x161758`). This PC port never emulates real memory-card hardware; it just writes ordinary
files to a `save\` folder next to the executable.

### A real arity trap, caught before it became a native crash

Ordinals 67, 68, 69 and 72 all share one body (`FUN_00a050f0`/`00a050d0`/`00a02e20`) that Ghidra
decompiled as taking **no parameters**, even though every call site passes one. Before writing the
P/Invoke declarations, read the raw bytes at each address in `winsys3d.dll` directly
(`OpenForesia.Tools -- at winsys3d.dll <va>`) and found `RET 4` on all four — genuinely one
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
of `Legacy/Blaze.exe` (`s_NameEncodeTable_00562b52` at file offset `0x161152`, plus the two accent
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
directly from `Legacy/Blaze.exe` (file offset `0x165bc0`) to check whether they'd fix it: they're
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
(`Legacy/Blaze.exe`, VA `0x560758`) directly: `0x000F28D4` — bit 24 clear. Grepped both `Blaze.c`
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

`Legacy/Blaze.exe` does not run on this machine's Windows 11 (untested territory, still unresolved),
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
`dotnet run --project OpenForesia.Tools -- at Legacy/Blaze.exe 0x00566c28 32` reads `0x40803000` /
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
  `0x56315c` (`at Legacy/Blaze.exe`), which hold exactly `0x563160`, the address of the already-declared
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

- **La valeur au chargement est 1**, relevée dans le binaire (`at Legacy/Blaze.exe 0x005633a8`,
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
dans `Legacy/Blaze.exe`) : tous chargent l'adresse — `8B 35 / 8B 0D / 8B 15  C0 6B 56 00`, soit
`MOV reg,[0x566BC0]` — avant d'écrire à travers (`88 54 30 0C` = `MOV [EAX+ESI+0xC],DL`). Et aucun
ne l'écrit jamais : pas un seul `A3 C0 6B 56 00` ni `89 xx C0 6B 56 00` dans tout `.text`, donc le
pointeur garde sa valeur de chargement pour toute la partie.

Il vaut `0x00568498`, où se trouve `"Blaze-Saving"` suivi de huit espaces et d'un NUL. Les douze
premiers octets, dont le rôle était noté « non établi », sont donc simplement le **préfixe
littéral** ; les sept caractères de code atterrissent dans les espaces, et le NUL se pose à `+0x13`.
`12 + 7` — exactement la forme que Vincent décrivait. Corroboration de voisinage : `0x00566bb8` et
`0x00566bbc` portent les pointeurs de `"SYSTEM.INI"` et `"BLAZE.ALL;1"`, `0x00566bc0` est l'entrée
suivante de la même table de noms de fichiers. Consigné dans `Documentation/DataSymbols.md`.

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
Mais lues directement dans `Legacy/Blaze.exe`, ce sont huit talons `C7 05 <globale> <imm32>` + `C3`,
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

`Legacy/Blaze.exe`, `0x0050B197..0x0050B1E6` — trouvé en cherchant les octets `16 8F 5E 00` dans
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

Résolu en désassemblant directement `Legacy/Blaze.exe`. L'original monte l'enregistrement octet par
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

Les 960 octets sont relus dans `Legacy/Blaze.exe` (`.data`, VA `0x00564B58`) et correspondent **clé
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

`Documentation/OpenForesia.cfg.default` — copie octet pour octet du BLAZE.cfg installé, dont le
contenu est vérifié contre le binaire comme ci-dessus. `Documentation/OpenForesia.cfg.md` documente
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
valeurs par défaut (`Documentation/OpenForesia.cfg.default` recopiée ; `Small` était resté à 1
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
qui le lie et le dessine lui-même. Son en-tête est relevé dans `Legacy/Blaze.exe`, et son `+4`
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
