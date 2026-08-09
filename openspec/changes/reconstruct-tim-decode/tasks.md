## 1. Decode the disassembly

- [x] 1.1 Reconstruct the stack frame from the prologue (`SUB ESP,0x14` plus `PUSH ESI`/`PUSH EDI`)
      to locate the parameters and locals relative to `ESP` after the pushes.
- [x] 1.2 Confirm `LEA ECX,[ESP+8]` hands `Ordinal_662` a 20-byte struct, matching both the `SUB`
      and the five dwords `FUN_00a05bd0` writes.
- [x] 1.3 Map each `[ESP+n]` reference to a `TIM_IMAGE` field or a parameter, and check the mapping
      against how each is used (a value passed to `LoadImage` as `rect` must be a RECT, as `data`
      must be a buffer).
- [x] 1.4 Confirm `Ordinal_677`/`Ordinal_662` are `OpenTIM`/`ReadTIM` by reading their bodies in
      `winsys3d.c`, not by inference from the name.

## 2. Reconstruct

- [x] 2.1 Add the `TimImage` struct and P/Invokes for ordinals 677 and 662.
- [x] 2.2 Correct the signature to `(byte* timData, ushort* slot, byte* meta)`.
- [x] 2.3 CLUT branch: guard on **`caddr`** (not `crect`), set the rect, and compute the rounded
      byte count *before* the width is overwritten — the multiply depends on the original width.
- [x] 2.4 Pixel branch: guard on `paddr` and the `meta[0] == 0x1c` skip; use the **byte** mask
      `& 0x10`, not the decompiler's `& 0xff10`.
- [x] 2.5 Transcribe the texture-page packing operation for operation, preserving the 16-bit `SAR`.
- [x] 2.6 Reproduce the tail copy that mirrors the CLUT into main video memory, including its
      `meta[1] < 0x10` and `caddr != null` guards.
- [x] 2.7 Replace the "not reconstructable even in principle" doc comment with what the function is
      and what the decompiler got wrong.

## 3. Fix the call-site stride

- [x] 3.1 Confirm from `Blaze.c` that both call sites use a byte pointer advancing 4 bytes.
- [x] 3.2 Change `destSlot += 16` to `+= 4` and explain why it was never caught.

## 4. Verify

- [x] 4.1 Build: 0 errors.
- [x] 4.2 Run with the CD mounted, no temporary patches. Confirm `LoadFirstLevelResources` completes
      instead of throwing.
- [x] 4.3 Record the new stopping point.
- [x] 4.4 Update `REVERSE_STATUS.md`: resolve TODO #19, correct the "unrecoverable" framing, and
      refresh the current-runtime-behaviour section.
