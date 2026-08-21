## Why

`FUN_005322f0` was the project's last hard blocker, and `REVERSE_STATUS.md` described it as
"genuinely unrecoverable, not just unread" — the one function that could not be reconstructed even
in principle, because Ghidra's own decompilation left inputs as `unaff_ESI`/`unaff_DI`/
`unaff_retaddr` and outputs as `extraout_var*`.

That was a correct observation turned into a wrong conclusion. Incomplete decompilation meant *read
the disassembly*, not *give up*. With the listing exported to `Legacy/Raw/Blaze/FUN_005322f0.txt`, the
function resolves completely — about 60 instructions, no ambiguity left.

## What the function actually is

It uploads a **TIM texture** into VRAM and records its texture-page / CLUT pair in a slot.
`Ordinal_677` and `Ordinal_662` are libgpu's `OpenTIM`/`ReadTIM`, extending the libgpu family
already identified in `winsys3d.dll` (658/661/665/674). The 20 bytes the prologue reserves with
`SUB ESP,0x14`, handed to `Ordinal_662` as `LEA ECX,[ESP+8]`, are a `TIM_IMAGE`:

```
[ESP+0x08] mode      [ESP+0x20] arg1 — the TIM chunk
[ESP+0x0C] crect     [ESP+0x24] arg2 — the 2-short slot
[ESP+0x10] caddr     [ESP+0x28] arg3 — the metadata triple
[ESP+0x14] prect
[ESP+0x18] paddr
```

## What the decompiler got wrong

Beyond missing names, three of its statements were actively misleading:

- **`unaff_retaddr` is not a return address.** It is parameter 2, the texture slot. Both call sites
  advance their slot pointer by exactly 4 bytes — two shorts — which corroborates it independently.
- **Every `param_1[0..2]` byte read comes from `EDI` = parameter 3**, the metadata triple, not from
  the chunk pointer the decompiler attributed them to.
- **`(*param_1 & 0xff10)` is an artifact.** The instruction is `AND AL,0x10`, a byte mask. Taking
  the decompiled form at face value would have corrupted the pixel rect's y coordinate — a bug that
  compiles, runs, and puts textures in the wrong place.

The `extraout_var*` values that made the function look unanalysable are just `Ordinal_674`'s ignored
return value, read three times.

## What Changes

- Reconstruct `DecodeResourceChunk` in full, with `TimImage` modelling libgpu's `TIM_IMAGE` and
  P/Invokes for ordinals 677/662.
- Correct the signature to `(byte* timData, ushort* slot, byte* meta)`.
- Transcribe the texture-page packing operation for operation. Its intermediate `SAR CX,4` is a
  **16-bit** shift and is not equivalent to a 32-bit one; simplifying it would change the result.
- Fix the directory loop's slot stride from 16 to **4**. Both call sites use a byte pointer advancing
  4 bytes; the 4x over-stride would have scattered the texture slots. It went unnoticed because
  `DecodeResourceChunk` threw before the loop could ever iterate.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

None — this reconstructs existing behaviour from the binary rather than defining new observable
behaviour, so `.openspec.yaml` sets `skip_specs: true`.

## Impact

- `OpenForesia/src/Blaze/Blaze.cs` — `DecodeResourceChunk`, the new `TimImage` struct, two winsys3d
  imports, and the stride fix in `LoadFirstLevelResources`.
- `REVERSE_STATUS.md` — resolve TODO #19 and record why the "unrecoverable" framing was wrong.
- **Runtime: startup now enters the game loop.** `LoadFirstLevelResources` completes, the tick wired
  in `wire-game-loop-tick` executes for the first time, and the process stops at `FUN_00542130`
  (game state 0) — ordinary uncovered code with a decompiled body, not a blocker.
- **Method note worth carrying forward**: TODOs #13, #14 and #23 are the same shape — bodies Ghidra
  never produced or analysed properly. They should be treated as "export the listing" tasks rather
  than as permanent gaps.
