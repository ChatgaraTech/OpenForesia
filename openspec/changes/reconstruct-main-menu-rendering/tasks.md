## 1. Recover the dispatch tables

- [x] 1.1 Record both 32-entry tables verbatim in `Documentation/DataSymbols.md`:
      `PTR_LAB_0056e870` (pass 1) and `PTR_FUN_0056e728` (pass 2), with the note that they
      differ only where pass 1 substitutes a cursor-advance stub
- [x] 1.2 Record the five decoded bodies (`0x00543940/50/60/70`, `0x00544b10`) with their
      bytes, so the hand-decoding can be rechecked
      — also recorded `DAT_0056e7b0` (proportional glyph widths) and the font bitmap's
      location at `MainMem + 0x1ad8f0`, both found while reading `FUN_00543b20`

## 2. Stage 1 — the UI text scanner

- [x] 2.1 Remove `MenuBackgroundStub` and give the call site in `RunMainMenuFrameLoop` the
      real two-argument shape `FUN_005443a0(primCursor, descriptor)`
- [x] 2.2 Reconstruct the four pass-1 cursor-advance stubs and handler `0x00544b10`, each
      with its decoded bytes quoted
- [x] 2.3 Reconstruct the two dispatch tables as delegate arrays, every unimplemented slot
      throwing with its address and control code
- [x] 2.4 Reconstruct `FUN_005443a0` pass 1: the descriptor save/restore, the scan loop, the
      DBCS lead-byte handling and the `$`-escape collection
- [x] 2.5 Reconstruct `FUN_005443a0` pass 2: the render loop, the `&`-conditional, the
      `0x5b`/`0x5d` bracket handling and the tail (`FUN_00544cb0`, `FUN_00543980`,
      `FUN_00543a10`)
      — the descriptor is masked to 24 bits here too (`param_2 & 0xffffff`), an identity in
      the original since every descriptor is `.data` of an image based at `0x00400000`.
      Added `AllocLowZeroed` and moved the menu context onto it so the mask stays an
      identity, with a throw if it ever is not
- [x] 2.6 Reconstruct the line/page helpers `FUN_00543980` (28 l.), `FUN_00543a10` (33 l.),
      `FUN_00544cb0` (45 l.), and the two decompiled handlers `FUN_00544c50` (22 l.) and
      `FUN_00544d90` (37 l.)
      — `FUN_00543980` turned out to enqueue onto the same VRAM upload list
      `FlushDeferredImageUploads` already drained, which is what identifies the cursor
      threaded through `FUN_005443a0` as an upload-node cursor rather than a primitive one
- [x] 2.7 Reconstruct `FUN_00543b20` (253 l.), the software glyph rasteriser, using the
      recovered `DAT_0056e7b0` widths and the `MainMem + 0x1ad8f0` font bitmap
      — split into `RasterizeGlyph` / `FillStagingRun` / `BlitGlyphRows` / `ReflowPartialWord`;
      `FUN_00547a00` is MSVC's unrolled `memmove` and is P/Invoked, not transcribed
- [x] 2.8 Leave `FUN_00544150` (DBCS/kanji glyphs) as a descriptive throw — its Ghidra
      signature is broken (`unaff_retaddr`) and the French menu never reaches it
- [x] 2.9 Build, run, and record what the frame loop reaches next
      — runs clean through both passes and stops at `FUN_00545340`, the stage-2 target. No
      handler slot threw, so the menu's `0x0c`/`0x0d`/`0x09` are all covered and the glyph
      path ran for all five labels, `é` (0xe9) included

## 3. Stage 2 — the UI window renderer

- [x] 3.1 Declare the ~30 new winsys3d ordinals, each signature read from `winsys3d.c`
      — **not needed: there are none.** The "~30 ordinals / 1742 lines" scope in `proposal.md`
      and `design.md` came from a mis-delimited slice of `Blaze.c`: the boundary scan only
      looked for `/* FUN_… */` headers, so it ran past the end of `FUN_00545340` and swallowed
      the trailing `Ordinal_NNN` stub section. `FUN_00545340` is **212 lines** and calls no
      ordinal directly
- [x] 3.2 Reconstruct `FUN_0051bdb0` and `FUN_0051be20`
- [x] 3.3 Reconstruct `FUN_00545160`, `FUN_00545220` and `FUN_00547008`
      — `FUN_00547008` is not a dependency either; same mis-delimitation. The real set is
      `FUN_00545160` and `FUN_00545220`, both reconstructed
- [x] 3.4 Reconstruct `FUN_00545340` itself
- [x] 3.5 Build and run; confirm the main menu draws its five labels
      — runs the whole menu frame loop with no exception, a first for this project. On-screen
      confirmation still pending
      — found and fixed a real aliasing bug doing this: `DAT_0056e61f` is not its own global,
      it is `menuContext[0x8c7]` (`0x0056e61f - 0x0056dd58 = 0x8c7`). Modelled separately, the
      confirm gate was always open and the menu picked an entry on its first frame

## 4. Needs a Ghidra export (for the user)

- [x] 4.0 `FUN_00544150` — export its disassembly to `Legacy/Raw/FUN_00544150.asm`. Ghidra
      lost its stack frame, so the argument mapping cannot be trusted from the pseudocode.
      Not on the main menu's path, so this does not block stages 1 or 2.

## 5. Corrections carried by this change

- [x] 5.1 Fix `PollInputAndAudio`'s hardcoded outer index to read `_DAT_005de538`, now that
      `FUN_00545340` line 91585 confirms the same expression independently

## 6. Document

- [x] 6.1 Record the newly used ordinals in `Documentation/DLL/winsys3d.md`
      — none were needed (see 3.1). The 24-bit display-list contract this change relies on was
      already recorded there by `fix-display-list-24bit-addressing`
- [x] 6.2 Update `REVERSE_STATUS.md`: coverage, the corrected role of `FUN_005443a0`, and
      the remaining unreconstructed handler slots as a numbered TODO
- [x] 6.3 Refresh `TODO.md`
