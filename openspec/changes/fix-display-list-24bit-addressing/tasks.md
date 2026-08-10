## 1. Clean the working tree

- [x] 1.1 Revert the A′ diagnostic patch in `OpenForesia/src/Blaze/Blaze.cs`: restore the
      `PollDebugHotkeys(...)` call in `PollInputAndAudio`, and remove the `FrameEnd` trace
      and the `s_DiagFrameCounter` field
- [x] 1.2 Revert the address-space probe in `OpenForesia/Program.cs` back to its three
      original lines
- [x] 1.3 Rebuild and confirm the tree is back to commit `e9579ee` behaviour — the native
      `0xC0000005` in `Ordinal_701` on the first `FrameEnd`

## 2. Reconstruct the arena placement

- [x] 2.1 Add the `VirtualAlloc`/`VirtualFree` P/Invokes alongside the existing kernel32
      declarations in `Blaze.cs`
      — `VirtualAlloc` + `VirtualQuery` (the scan needs the latter; `VirtualFree` has no
      caller, since the arena lives for the process lifetime as the original's `.data` did)
- [x] 2.2 Add the named constants for the arena: size `0x280000`, preferred base
      `0x00B60000`, `MainMem` offset `0xDBD8` within it, and the ceiling `0x00D80000`
      derived from the 24-bit constraint — each with a comment citing where in
      `winsys3d.c` it comes from
      — the ceiling is computed rather than hardcoded: `0x01000000 - allocationSize`
      aligned down to granularity, which lands on `0x00D70000`, not the `0x00D80000`
      estimated in design.md
- [x] 2.3 Implement the placement: try the preferred base first; on failure scan upward
      from `0x00010000` for the first free block of the required size below the ceiling
- [x] 2.4 Throw a descriptive exception naming the 24-bit display-list constraint and the
      required size when no region qualifies — never return a high address

## 3. Retarget `MainMem`

- [x] 3.1 In `GetMainMemPtr()`, after `LoadLibraryA("winsys3d.dll")` resolves the `MainMem`
      export and before any other winsys3d entry point is called, write the placed arena's
      address into the exported pointer
      — split into `EnsureWinsys3dLoaded` / `EnsureDisplayListArena` / `GetMainMemPtr`, and
      called unconditionally from the top of `entry()`: the ordering requirement is against
      `Ordinal_789`, which the lazy accessor alone does not guarantee to precede
- [x] 3.2 Make the retarget happen exactly once and be observable — log the chosen base,
      whether it is the original layout, and the delta when it is not
- [x] 3.3 Add the Ghidra traceability comment quoting the three `winsys3d.c` occurrences of
      `MainMem` (lines 568, 5655, 5843) that justify the retarget

## 4. Verify

- [x] 4.1 Build with the SDK environment, run without it, confirm `MainMem` resolves to
      `0x00B6DBD8` in the nominal case
      — observed once in six runs; the original layout is reachable but not the common case
- [x] 4.2 Confirm the ordering table and primitive buffer addresses reported for the first
      frame are unchanged when masked to 24 bits
      — verified behaviourally: `DrawOTag` follows `*p & 0xffffff` on every entry, so a
      truncated address would fault immediately; it now completes on every frame
- [x] 4.3 Confirm `Ordinal_701` no longer faults and the frame loop iterates past frame 1 —
      the next stop should be a named `NotImplementedException`, not an access violation
      — stops at `NotImplementedException: FUN_005443a0: menu background renderer`
- [x] 4.4 Run several times to exercise the fallback scan; confirm a non-preferred base is
      reported with its delta and still draws
      — 6/6 runs placed the arena; deltas `-0x800000`, `-0x8A0000`, `0`, `-0x800000`,
      `-0x8E0000`, `-0x800000`
- [x] 4.5 Confirm the previously reconstructed input polling runs — `PollInputAndAudio` is
      reached for the first time, since it sits after `DrawOTag` in `FrameEnd`
      — reached, and the frame loop went round enough times for the fade counter to enter
      its branch, so polling ran on every one of those frames without incident

## 5. Document

- [x] 5.1 Record the 24-bit display-list contract in `Documentation/DLL/winsys3d.md` as
      confirmed behaviour, quoting `ClearOTagR` and `DrawOTag`, and note that the arena
      spans `MainMem + 0x0` to `+0x280000` with no fixed-address symbols inside
- [x] 5.2 Add the arena retarget to `REVERSE_STATUS.md` as a third environmental deviation,
      next to the `SetDisplayMode` fallback and the `DDERR_SURFACELOST` restore
- [x] 5.3 Correct the blocking-point section of `REVERSE_STATUS.md`: the crash was the
      first frame rather than the second, and the four input-polling functions were never
      implicated
      — also recorded the one real infidelity found while clearing them (the hardcoded
      outer index in `PollInputAndAudio`), which is left unfixed and flagged
- [x] 5.4 Rewrite the superseded regression section of `TODO.md` and refresh its "where the
      project is" summary
- [x] 5.5 Note in `CLAUDE.md`'s working rules that a DLL's own decompilation is to be read
      before reconstructing a call into it — the `| 0x2000000` tag would have been caught
      immediately by reading `ClearOTagR`
