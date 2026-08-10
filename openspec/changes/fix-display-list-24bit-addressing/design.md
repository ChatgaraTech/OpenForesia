## Context

See `proposal.md` — Why. This section records only the evidence the approach rests on, all
of it read out of `winsys3d.c` or measured, none of it inferred.

**The 24-bit contract is in both binaries, not just the DLL.** `Ordinal_688` (ClearOTagR,
`FUN_00a06160`) writes `(uint)puVar1 & 0xffffff` into each entry and terminates the list
with `0xffffff`; `Ordinal_701` (DrawOTag, `FUN_00a061b0`) walks it with
`puVar2 = (uint *)(*puVar2 & 0xffffff)` and reads the top byte as the primitive length.
`Blaze.exe` agrees: `DrawFlatPrimitive` builds its tag as `*prim = *otEntry | 0x2000000`,
which is only correct while the address's top byte is zero. Neither side can be fixed
without breaking fidelity — the arena has to be low.

**`MainMem` is a genuine indirection.** It occurs three times in 27 703 lines:

| line | code | role |
|---|---|---|
| 568 | `MainMem = &DAT_00b6dbd8;` | the one initialisation, in `entry()` |
| 5655 | `DAT_00dedbd8 = iVar1 + 0x240000 + MainMem;` | read, in `DrawOTag`, every frame |
| 5843 | `DAT_00dedbd8 = MainMem + 0x200000;` | read, in device initialisation |

`DAT_00b6dbd8` itself is referenced only at line 568. Nothing else in the DLL hardcodes an
arena address, so redirecting the exported pointer redirects every user of the arena — the
game's, through `MainMem_exref`, and the library's own two sites.

**The arena is a clean 0x280000-byte hole.** Every symbol winsys3d addresses by fixed
address between the arena base and 2.5 MB above it was enumerated: there are none between
`MainMem + 0x0` and `MainMem + 0x280000`, and the globals resume exactly at
`MainMem + 0x280000` (`DAT_00dedbd8` and 36 others). Known usage fits inside: the game
takes `+0x1c6000`…`+0x1e4000` for its two ordering tables and primitive buffers, the
library takes `+0x200000` and `+0x240000` for its double-buffered scratch.

**`entry()` does not populate the arena.** It sets the pointer, allocates a separate 1 MB
buffer (`EXT_00b6dbd0 = Malloc(0x100000)`), and loads font/KANJI resources into unrelated
globals. There is nothing in the arena to preserve across a retarget.

**Measured, on this machine.** `winsys3d.dll`'s preferred base is `0x00A00000` and its
image spans `0x0084E400`; it is relocated to `0x09…` because `OpenForesia.exe` (ASLR,
stable per boot at `0x00EC0000`) sits inside that range and the CLR reserves the rest.
Sampling the largest free block from `0x00010000` over five runs gave 6.9, 2.8, 4.3,
**1.9** and 13.1 MB — so a low block is usually but not always available at the very
bottom, and the fallback scan is the normal path rather than a precaution.

## Goals / Non-Goals

**Goals:**

- Restore the address-space arrangement the original got for free from a fixed DLL base.
- Keep runtime addresses equal to decompiled addresses in the nominal case, so
  `winsys3d.c` and `Blaze.c` stay directly usable while reading the reconstruction.
- Make the failure mode legible: a named constraint, not an access violation.

**Non-Goals:**

- Controlling where `winsys3d.dll` itself loads. After retargeting, its base is irrelevant.
- Owning any winsys3d behaviour beyond the arena's location.

## Decisions

### Retarget the exported `MainMem` pointer rather than relocating the DLL

The blocker is the arena's address, not the module's. Writing the pointer reaches both
consumers, costs about twenty lines, modifies no binary, and needs no Windows setting.

*Alternatives considered, all rejected:*

- **Clear `DYNAMICBASE` on `OpenForesia.exe`.** Measurement kills it: the CLR anchors a
  ~37 MB reservation just after the executable, so moving the executable to `0x00400000`
  moves that reservation on top of `0x00A00000`.
- **Reserve every free region except a target window, forcing `LoadLibrary`'s hand.**
  Works, but manipulates the loader instead of using an interface the DLL exposes, and is
  sensitive to loader search behaviour.
- **A native launcher that loads `winsys3d.dll` before hosting the CLR** — conceptually
  the most faithful, since `Blaze.exe` imports the DLL statically (the string sits in its
  import table at file offset `0x1703AC`, the only one of the five DLLs so imported). No
  C/C++ toolchain is installed on this machine, and the retarget makes it unnecessary.
- **Rebasing `winsys3d.dll`** — excluded on principle: the DLLs are reverse-engineering
  references and must stay byte-identical.
- **Windows Exploit Protection settings** — not reproducible from the repository.

### Allocate `0x280000` bytes at `0x00B60000` by preference

64 KB allocation granularity means requesting `0x00B60000` puts `MainMem` at
`0x00B6DBD8` — its 1998 value. Then `winsys3d.c` line numbers and Ghidra addresses read
directly against a running process, which is worth real effort on a project whose output
is understanding rather than a product.

The constraint the fallback must satisfy is `base + 0x280000 < 0x01000000`, i.e. any base
below `0x00D80000`. Scan for the first free block of the required size and log the delta;
the low 64 KB is permanently reserved by Windows and is never a candidate.

### Retarget immediately after `LoadLibrary`, before any winsys3d call

Line 5843 derives three pointers from `MainMem` during device initialisation. If it runs
first, the library works from the old arena while the game writes into the new one. The
load already happens in one place (`GetMainMemPtr`), which is the natural home for this.

That `DrawOTag` re-derives its scratch pointer from `MainMem` every frame is a safety net,
not a licence to retarget late: the initialisation-time pointers are not re-derived.

## Risks / Trade-offs

- **No free low block at startup** → the spec requires a descriptive failure. Measured at
  worst 1.9 MB free in the first hole out of five runs, but other holes exist higher in the
  window; the scan covers the whole range below `0x00D80000` before giving up.
- **The arena is 0x280000 bytes because that is where winsys3d's globals resume** — an
  upper bound inferred from symbol layout, not a size read from the binary. Known usage
  reaches `+0x240000`, leaving `0x40000` of margin. If a later subsystem addresses beyond
  `+0x280000`, it would silently overlap what were DLL globals in the original layout →
  worth a bounds assertion when a new `MainMem + offset` site is reconstructed.
- **The fallback delta breaks 1:1 traceability** when it fires → log it once at startup so
  any address read from `winsys3d.c` can be translated by a single constant.
- **Writing through an exported data symbol is unusual** and will look surprising to a
  future reader → the deviation goes in `REVERSE_STATUS.md` next to the two existing ones,
  with the three `MainMem` occurrences quoted as justification.
- **This restores a 1998 assumption rather than removing it.** The reconstruction stays
  x86-32 and stays dependent on winsys3d's memory model. That is deliberate for now; a
  later move to a modern renderer is the thing that would retire it.

## Migration Plan

Not applicable — no persisted state, no external interface. Rollback is reverting the
change. The two diagnostic patches currently in the working tree
(`OpenForesia/src/Blaze/Blaze.cs`, `OpenForesia/Program.cs`) are reverted as part of this
work, not carried forward.

## Open Questions

- Whether any winsys3d entry point other than lines 5655 and 5843 reads the arena through
  a pointer derived earlier and cached elsewhere. The symbol enumeration says no such
  cached pointer lives inside the arena, but it does not prove none lives outside it. This
  is verifiable by running: if the frame draws, the pointers agree.
