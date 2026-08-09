# Blaze reverse-engineering status

Tracks progress reconstructing `Blaze.c` (Ghidra pseudocode, read-only) into
`src/Blaze/Blaze.cs`. Read `Prompt.md` and `Strategy.md` (repo root) for the
methodology before continuing this work. Strategy.md now also covers the
decompiled DLL sources (`3dhelp.c`, `gldhelp.c`, `playst.c`, `winsys3d.c`,
`wsyskern.c`, repo root, **read-only**) and `Documentation/DLL/<dll>.md` as
the running memory of what's been learned about each — check there before
re-analyzing an external call, and update it when a new one is understood.

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
│  ├─ RewindMovieFallbackFrame (FUN_00542f30 -> FUN_00542c40) [PARTIAL — only the one confirmed
│  │                                             field reset (*(param+0x2c)=0); the DirectDraw-
│  │                                             wrapper-vtable-based fallback-bitmap blit is
│  │                                             TODO(REVERSE), same root cause as above]
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
| `FUN_00542f30` | 0x00542f30 | `RewindMovieFallbackFrame` | Partial, see call-graph notes above |
| `FUN_00542a70` | 0x00542a70 | `TrySetupMovieStream` | Stub (always false), see call-graph notes above |
| `FUN_00542b70`/`FUN_00542eb0` | 0x00542b70/0x00542eb0 | `PlayMoviePumpLoop`/`PlayMovie` | Joystick capture + message pump, faithful |
| `FUN_00504460` | 0x00504460 | `SetupJoystickHandlers` | Partial, see call-graph notes above |
| `FUN_00504b10` | 0x00504b10 | `JoystickResetState` | Faithful |
| `FUN_0050f120` | 0x0050f120 | `CheckDeviceCapabilityFlag` | Faithful structure, opaque vtable call |
| `FUN_00529e40` | 0x00529e40 | `SetShadingDefaults` | Faithful |
| `FUN_00529eb0` | 0x00529eb0 | `InitViewportSurfaces` | Faithful |
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
   - `winsys3d.c`'s `Ordinal_789` device-init branch is never reached: both `Blaze.c` call sites pass `0` as its first argument.

   Conclusion: the main window's creation site is **not present in any available source**. Most likely it lives in a `Blaze.exe` code path Ghidra rendered as a bodiless `LAB_`, or is reached through one of the unresolved indirect calls. This is the main reason nothing can render yet — see the "Reality check" section.
7. **atexit() registration**: `DoExit` walks a table (`DAT_00607950`..`DAT_0060794c`) that nothing currently populates — no `atexit()` call sites have been reconstructed yet in the reachable call graph. Revisit if/when one turns up.
8. **Ordinal 790's callback** (`Blaze.c`: `&LAB_00512ef0`, passed to `SetDeviceLostCallback`): no decompiled function body anywhere in `Blaze.c` — Ghidra only ever saw it address-taken. Cannot be reconstructed without further disassembly. `winsys3d.c` confirms the DLL does call it back later (see `Documentation/DLL/winsys3d.md`), so this matters once device-loss/reset handling comes up.
9. ~~**`DAT_005db900`/`DAT_005db758[50]`** interface identity~~ — **RESOLVED**. Reading the shutdown path (`FUN_00512ff0`/`FUN_00512ea0`) and the buffer-creation path (`FUN_00513030`, `FUN_005133xx`) exercises enough distinct vtable slots to pin both interfaces against the documented DirectX 3–5 COM layouts with no ambiguity: `DAT_005db8e4` is **`IDirectSound`** (0x08 Release, 0x0c CreateSoundBuffer) and `DAT_005db900`/`DAT_005db758[50]` are **`IDirectSoundBuffer`** (0x08 Release, 0x24 GetStatus, 0x2c Lock, 0x30 Play, 0x3c SetVolume, 0x48 Stop, 0x4c Unlock). This retroactively confirms the 0x3c=SetVolume reading in `ApplyCdVolume`/`ApplyAllSoundBufferVolumes`. The 50-slot count is confirmed independently by `FUN_00512ea0`'s loop bound (`0x005db758`..`0x005db81f`). *Update, this session*: the **buffer**-creation paths are now reconstructed (`AllocateSoundBuffer`, `StartMusicStream`), and a third interface was added to the set — `DAT_005db904` is **`IDirectSoundNotify`** (0x08 Release, 0x0c SetNotificationPositions), pinned by `StartMusicStream` obtaining it via `QueryInterface` on the streaming buffer and then using it only with the argument pair `(8, &DAT_005db8a0)` where `DAT_005db8a0`..`DAT_005db8df` has just been filled as exactly 8 `{ dwOffset, hEventNotify }` pairs. *Still open*: the **device** itself (`DAT_005db8e4`) still has no assignment site anywhere in the reconstructed graph — no `DirectSoundCreate` call has been located — so every entry point still takes its "no device" early-out at runtime. See TODO #26.
10. **AU_00564a05 blob**: `s_SmallTextureFlag` only reconstructs one 4-byte field (`._51_4_`) of what's clearly a larger structure/array (`AU_00564a05`) — the rest of that blob hasn't been characterized.
11. **`DAT_00563080` ini key / `DAT_005630bc` ini extension / `DAT_005630c4` format string**: none of these three have a Ghidra-derived string name (unlike e.g. `s_SoundVol_005630b0`), so their exact text is a placeholder/guess in `Blaze.cs` (`UnknownIniKey_005630a0`, `IniFileExtension = ".ini"`) — see inline TODOs.
12. **`DAT_00562e00` per-resolution clamp table**: content unknown; `s_ResolutionDetailLimit` in `Blaze.cs` is a placeholder filled with `int.MaxValue` (i.e. the clamp is currently a no-op).
13. **Movie/intro-video subsystem's two custom C++ vtables** (`PTR_LAB_0054d078` — the DirectDraw-wrapper class constructed by `FUN_00512930`/`FUN_00512d10`, and `PTR_LAB_0054d0a0` — the video-stream class constructed by `FUN_005431e0`/`FUN_00542fc0`): every vtable slot these point at (`LAB_0054dxxx` targets) has no decompiled body anywhere in `Blaze.c` — same class of gap as ordinal 790's `&LAB_00512ef0` callback, just a whole class's worth at once. `TrySetupMovieStream`/`OpenMovieStream`/`RewindMovieFallbackFrame` in `Blaze.cs` stop exactly at these vtable calls; see the "Movie / intro-video subsystem" banner comment in `Blaze.cs` for the full writeup of what *is* independently confirmed (real DirectDraw vtable offsets matching the public `IDirectDraw`/`IDirectDrawSurface` v1 layout, real Win32 window/message-pump/joystick-capture behavior).
14. **Movie window procedure** (`Blaze.c`: `&LAB_00542600`, the `WNDPROC` for window class `"VfwClass"`): no decompiled body. `CreateMoviePlayerWindow` in `Blaze.cs` therefore does not actually call `RegisterClassA`/`CreateWindowExA` (would crash on the first dispatched message with no real `WndProc`); `HWnd` stays `IntPtr.Zero`. This is also *why* `DAT_005db85c` (TODO #6 above) still can't be threaded through — the movie window isn't the main window anyway (it's a separate 2000x2000 hidden helper window), so the real main-window creation is presumably elsewhere, still unlocated.
15. **`PTR_FUN_0056c96c`** (per-frame "tick" function pointer, called indirectly by `FUN_0052a3d0`/`RunGameLoop`) — **investigated and closed as "requires the binary"**. `0x0056c96c` occurs exactly once in all 98k lines of `Blaze.c`: at the call site itself. It is therefore not a runtime-assigned variable at all but a **statically initialised `.data` function pointer** — the linker wrote the target address into the image, and Ghidra's `PTR_<KIND>_<address-of-pointer>` naming (compare the confirmed `PTR_s_Blaze___Blade_00563208`, `PTR_PTR_00566d68`, `PTR_FUN_0056b4d0`) tells us the slot holds an unnamed function pointer but not *which* function. Its neighbours (`s_FEMALE_0056c95c`, `DAT_0056c964`, `DAT_0056c978`) confirm it sits in initialised data. **No amount of further reading of `Blaze.c` can recover it** — see the "Systemic blocker" section below. Guessing a plausible target would violate Strategy.md's "ne jamais inventer une signature/un comportement", so `RunGameLoop` throws rather than silently skipping the tick.
16. **`DAT_005633a8`** (mode flag read pervasively, including by `FUN_00529eb0`/`InitViewportSurfaces`): no assignment site reached yet either; always 0 (its default) in the current reconstruction.
17. **Font-loading filename** (`Blaze.c`: `s_DATA_Font_bmp_005684cc`) and the three intro-movie filenames (`s_TEPS_AVI_005684f4`/`s_OPENING_AVI_005684e8`/`s_ANI01_AVI_005684dc`): only Ghidra's auto-derived symbol names survive, not the literal bytes — `"DATA\FONT.BMP"`/`"TEPS.AVI"`/`"OPENING.AVI"`/`"ANI01.AVI"` in `Blaze.cs` are best guesses from the symbol names, same caveat as TODO #4's message strings.
18. **`FUN_00529b00`/`LoadWholeFileToBuffer`'s incomplete Ghidra analysis**: the failure path returns `unaff_ESI` (a register Ghidra never attributed a value to at function entry — genuinely indeterminate, not just unnamed) and the read call shows a literal `2` (`Ordinal_334(iVar1,2,iVar2)`) exactly where a destination-buffer argument should be, one parameter short of the two arguments both real call sites actually pass. Reconstructed using the evident 2-argument intent (`dest` as that argument, `0` as an explicit safe failure return) — flagged rather than presented as certain.
19. **`FUN_005322f0`/`DecodeResourceChunk` — genuinely unrecoverable, not just unread**: several of its inputs are `unaff_ESI`/`unaff_DI`/`unaff_retaddr` and outputs `extraout_var`/`extraout_var_00`/`extraout_var_01` in Ghidra's own decompilation — register-allocated values Ghidra could never tie to a defining instruction or a real variable. This is a stronger category of gap than the usual "not yet read": Ghidra's *own* static analysis of this function is incomplete, so no amount of re-reading `Blaze.c` will resolve it without disassembling the raw instructions. **Current blocking point** for `LoadFirstLevelResources` (thrown on the first directory entry, if the loaded "SYSTEM.INI"-equivalent file lists any).
20. **`DAT_0056aca8`** (resource-fixup schema table read by `FixupResourceHeader`/`FUN_00532070`): data-only global, byte content not present in `Blaze.c`'s text (same category as `DAT_00562e00`/TODO #12) — `s_ResourceFixupSchema_0056aca8` is an empty array, so the schema-driven fixup pass is a safe no-op rather than a guess.
21. **`DAT_006008c8`/`DAT_00604930`/`DAT_00600904`** (the 3 comparison targets in `IsKnownOverrideDestination`/`FUN_0052a660`): none have an assignment site anywhere in the reconstructed call graph yet, so they're always null/zero; the 2 true-branch bodies (native override-table installers, one pulling in 11 further `FUN_` calls) are stubbed rather than translated.
22. **`PTR_s_BLAZE_ALL_1_00566bbc`/`PTR_s_SYSTEM_INI_1_00566bb8`/`s____snd_Base_snd_00568500`** filenames used by the resource-loader subsystem: same category as TODO #17 — only Ghidra's derived symbol names survive; `"BLAZE.ALL"`/`"SYSTEM.INI"`/`"\SND\BASE.SND"` in `Blaze.cs` are best guesses.
23. **Music-streaming worker thread** (`Blaze.c`: `&LAB_00513660`, passed to `CreateThread` by `FUN_005131d0`): no decompiled body anywhere in `Blaze.c` — address-taken only, exactly the same class of gap as the movie `WNDPROC` (#14) and ordinal 790's callback (#8). `StartMusicStream` therefore does **not** call `CreateThread` (jumping to an address we cannot reconstruct would crash), same precedent as `CreateMoviePlayerWindow` skipping `CreateWindowExA`; `s_MusicStreamThread` stays `IntPtr.Zero`. Consequence: everything else in the start path is real, so a track *would* begin playing, but nothing ever refills the buffer — the pre-decoded 80 blocks would loop forever instead of streaming on. This is also why `DAT_005db860`/`DAT_005db750`/`DAT_005db89c` (source-block cursor, total block count, playing flag) and the 8 notification events are written but never read in the reconstruction: the worker is their only consumer. Resolvable by re-running Ghidra with `0x00513660` forced to "disassemble as function".
24. **`DAT_0056ac60` (16 shorts) / `DAT_0056ac80` and `DAT_0056ac90` (7 shorts each)** — the MS-ADPCM adaptation table and the two coefficient tables. Data-only globals, so the bytes aren't in `Blaze.c` (same systemic blocker as #12/#20), but unlike those two the *identity* here is not a guess: `FUN_00531f70` only accepts a `fmt ` chunk with `wFormatTag == 2` (WAVE_FORMAT_ADPCM), `wBitsPerSample == 4` and `wNumCoef == 7`, then compares `aCoef[0..6]` against `DAT_0056ac80`/`DAT_0056ac90` — i.e. it verifies the file uses the *standard* coefficient set so the decoder's hardcoded tables apply. The addresses corroborate the sizes exactly (16 shorts at `0x0056ac60` end at `0x0056ac80`; 7 shorts at `0x0056ac80` end at `0x0056ac8e`, padded to `0x0056ac90`). `Blaze.cs` uses the documented standard MS-ADPCM values. Still worth confirming byte-for-byte if the binary appears.
25. **`FUN_00513030`'s register-attribution bug**: Ghidra writes `piVar3` (loaded from `DAT_005db8e4`, the *device*) into the slot table and releases that same pointer when the table is full, while the newly created buffer sits unused in `CreateSoundBuffer`'s `local_2c` output parameter. Storing the device in a buffer slot would make `ReleaseAllSoundBuffers` call `IDirectSoundBuffer` methods on an `IDirectSound` and release the device up to 50 times, so the decompiled text cannot be what the binary does. `AllocateSoundBuffer` transcribes the evident intent (the *created buffer* is stored and released) — flagged rather than presented as certain.
26. **`DAT_005db8e4` (the `IDirectSound` device) has no creation site**: read in four places, assigned nowhere in all of `Blaze.c`'s reconstructed graph, and no `DirectSoundCreate` call site has been located. Same shape as `DAT_005db85c` (#6, the main window handle) and quite possibly the same root cause — both would plausibly be created by whatever code also creates the main window, which #6 established is not present in any available source. Until it is found, the whole audio subsystem is correct but inert: every entry point takes its "no device" early-out.
27. **`DAT_005631d4` initial value** (music enabled/disabled at startup): data-section content, so unavailable; `s_MusicEnabled` defaults to 0, meaning music starts disabled and `StartMusicStream` early-outs until `ToggleMusicEnabled` flips it. Note `DAT_005631d0` (the "no stream running" flag) needed no such assumption — a first `StartMusicStream` always routes through `StopMusicStream`, which sets the flag before it is tested.
28. **`FUN_0053da20`/`FUN_0053db20` (positional sound gain)** — the remaining members of the sound-effect family, deliberately left untranslated. `FUN_0053da20` computes a left/right gain pair from a world position via the cos table (ordinal 450) and `FUN_0053db20` feeds the result to `PlaySoundEffect`, but Ghidra declares `FUN_0053da20` with **2** parameters while `FUN_0053db20` calls it with **3**, and its body reads `unaff_SI` and `unaff_retaddr` — register-passed values never tied to a parameter. Reconstructing it would mean inventing a signature, which Strategy.md forbids; it needs a Ghidra re-run with the calling convention corrected (same class as the `__fastcall`/`__thiscall` cases noted under the systemic blocker).
29. **Sound-directory second half (`DAT_005e8520`, entries `0x3c`..`0x4c`)** is loaded by `FUN_00512xxx` (`Blaze.c:63354`), which is not reconstructed — so those 17 entries stay zero and sound ids `0x3c`..`0x4c` (which include the four fixed UI sounds `PlayUiSound38`..`3B`) currently resolve to a zero-length sample and play silence. Not a data-availability problem: it is ordinary uncovered code, and the same call site also fills the level sample bank `DAT_00600900`.
30. **`PTR_s____Music_Mel00_01_WAV_00566cc8`** — the music-filename pointer table indexed by `FUN_00529ad0`/`PlayMusicTrack`, the only caller of `StartMusicStream`. Call sites pass indices up to at least `0x26`, and `FUN_00529ad0` patches each path's first character with the CD drive letter (`s____Blaze_Blade_005631e8[0]`) before playing. Data-only table, so it is not reconstructed and `FUN_00529ad0` is deliberately left untranslated rather than filled with invented filenames — same category as #17/#22, but a whole table rather than single strings.

## Systemic blocker — `Blaze.exe` itself is missing from the repo

**This is the single highest-leverage thing that can change.** The repo root contains
`Blaze.c` and the five helper DLLs *together with their binaries*
(`winsys3d.dll`, `wsyskern.dll`, `playst.dll`, `3dhelp.dll`, `gldhelp.dll`) — but
**not `Blaze.exe`**. (`Prompt.md` refers to a `Legacy/Decompiled` solution folder;
no such folder exists here.)

Having the DLL *binaries* is exactly why ordinal-only imports could be resolved
(`pe_exports.ps1` reads their export tables directly). Not having `Blaze.exe`
is exactly why a whole *class* of TODOs is stuck, because `Blaze.c` is a
**code-only decompilation**: Ghidra emitted function bodies, not the `.data`
/`.rdata` sections. Every symbol whose value lives in initialised data is
therefore unrecoverable by construction, no matter how carefully `Blaze.c` is
re-read.

Currently blocked *solely* by this, and all unblockable at once by adding the
binary (or a Ghidra data-section export):

| TODO | Symbol(s) | What's missing |
|---|---|---|
| #15 | `PTR_FUN_0056c96c` | the game loop's per-frame tick function pointer |
| #2 | `DAT_00563280`..`_0056328c` | CD-check filename template |
| #3 | `PTR_s_Blaze___Blade_00563208` | `Setup3DDevice`'s title/string table |
| #4, #17, #22 | all the `s_*` message/filename strings | literal bytes (currently best-guess text) |
| #10 | `AU_00564a05` | structure/array content |
| #11 | ini key/extension/format strings | literal bytes |
| #12 | `DAT_00562e00` | per-resolution clamp table |
| #20 | `DAT_0056aca8` | resource-fixup schema table |
| #21 | `DAT_006008c8`/`DAT_00604930`/`DAT_00600904` | override-table base pointers |
| #24 | `DAT_0056ac60`/`DAT_0056ac80`/`DAT_0056ac90` | MS-ADPCM adaptation + coefficient tables (values reconstructed from the standard, worth verifying) |
| #27 | `DAT_005631d4` | whether music is enabled at startup |
| #30 | `PTR_s____Music_Mel00_01_WAV_00566cc8` | the music-filename pointer table |

Two further blockers are *not* fixed by the binary alone but by **re-running
Ghidra** (or a disassembler) on it:

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

- **`DecodeResourceChunk` (Ghidra: `FUN_005322f0`)** — **the current blocking
  point.** Reached from `LoadFirstLevelResources`'s directory-entry loop
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
- **`FUN_0052a440`** (per-game-state dispatcher, ~100 lines, already read,
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
