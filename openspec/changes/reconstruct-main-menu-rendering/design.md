## Context

See `proposal.md` — Why. This records the structure established by reading, and the
evidence for the parts Ghidra did not decompile.

**`FUN_005443a0` is a two-pass text engine.** Its body (`Blaze.c` 91041–91266) runs the same
byte stream twice over the descriptor `param_2`:

```
  pass 1  (91121-91151)   scan     dispatch through PTR_LAB_0056e870
                                   collects '$'-escapes into local_8d4[6]
                                   descriptor saved to local_8c8 and restored after
  pass 2  (91163-91265)   render   dispatch through PTR_FUN_0056e728
                                   emits glyphs via FUN_00543b20 / FUN_00544150
```

Both passes read a byte, advance past it, and dispatch when it is below `0x20`; codes `9`,
`0xe` and `0xf` end the pass. Bytes `0x80..0x9f` are DBCS lead bytes (Shift-JIS heritage),
`0x24` (`$`) marks an escape, `0x26` (`&`) a conditional, `0x5b`/`0x5d` its brackets.

**Both dispatch tables were read from `Legacy/Binaries/Blaze.exe`**, 32 entries each. They share most
handlers; pass 1 substitutes trivial cursor-advance stubs wherever pass 2 would draw. Only
five of pass 2's 31 distinct handlers are decompiled in `Blaze.c` — the rest are reached only
through these tables, so Ghidra never emitted them as functions.

**The four pass-1 stubs were decoded from the binary**, being 16 bytes each:

| address | bytes | meaning |
|---|---|---|
| `0x00543940` | `8B 44 24 04 C3` | `return p` — code takes no operand |
| `0x00543950` | `8B 44 24 04 40 C3` | `return p + 1` |
| `0x00543960` | `8B 44 24 04 83 C0 02 C3` | `return p + 2` |
| `0x00543970` | `8B 44 24 04 83 C0 03 C3` | `return p + 3` |

They are not semantic handlers: they declare how many operand bytes each control code
consumes, so pass 1 can skip the stream without drawing.

**Handler `0x00544b10` (control code `0x09`) was decoded too** — 49 bytes, and the only one
the menu needs that Ghidra lacks:

```asm
MOV ECX,[ESP+8]            ; descriptor
MOV AL,[ECX+0x8C7]         ; flags
MOV byte [ECX+0x3B], 1
OR  AL, 4                  ; the bit FUN_005443a0 tests on entry to return early
MOV byte [ECX+0x8C4], 0
MOV [ECX+0x8C7], AL
MOV EAX,[ESP+4]            ; text cursor
MOV DL,[EAX]               ; the operand byte
INC EAX
MOV [ECX+0x8C5], DL
MOV [ECX+0x8AC], EAX       ; descriptor->text = p + 1
RET
```

It is the end-of-page / wait code: it latches the operand and raises flag bit 2, which makes
every later call to `FUN_005443a0` return immediately. That closes the loop with the menu's
own label stream, which ends `0x09 0x3f` — code 9 with operand `'?'`.

**What the menu actually exercises.** Its label table (`DAT_0056e670`, already recovered
verbatim) contains exactly three control codes:

| code | pass 1 | pass 2 | status |
|---|---|---|---|
| `0x0c` (stream head) | `0x00543940` (+0) | `0x00544c50` | decoded / decompiled |
| `0x0d` (×5, separators) | `0x00543940` (+0) | `0x00544d90` | decoded / decompiled |
| `0x09` (terminator) | `0x00543950` (+1) | `0x00544b10` | decoded / **decoded here** |

So stage 1 needs no Ghidra re-run.

**`FUN_00544150` has a broken Ghidra signature and is out of stage 1's path.** It is the
DBCS/kanji glyph renderer: it resolves a 16×16 1bpp bitmap via `Ordinal_57(codepoint)`, applies
a bit-spreading transform and unpacks it into the 4bpp staging buffer. Ghidra lost its frame —
it reuses `param_1` as a scratch variable and then reads the descriptor from `unaff_retaddr`,
while every call site passes `FUN_00544150(descriptor, codepoint)`. That is the "parameters not
recovered" class this project has already been burned by four times, so it needs a disassembly
export before it can be reconstructed faithfully.

It is reached only from the two DBCS branches of pass 2 (`uVar10` in `0x80..0x9f`). The French
label table is single-byte cp1252, every byte below `0x80`, so **the main menu never reaches
it** — a descriptive throw is the correct stage-1 treatment.

**`FUN_00543b20` is a software glyph rasteriser**, 253 lines: it writes 4-bit pixels into the
staging buffer at `DAT_005e8304`, tracks the pen in `DAT_005e8320`, and implements word wrap by
backtracking to the last space (`DAT_005e02fc`) and re-flowing the partial word onto the next
line, with two copy paths depending on nibble parity. Its glyph widths come from `DAT_0056e7b0`
and its bitmap from `MainMem + 0x1ad8f0`. Both are recovered in `Documentation/Reference/DataSymbols.md`.

## Goals / Non-Goals

**Goals:**

- Reconstruct the text engine faithfully as two passes over one dispatch mechanism.
- Make the 26 unreconstructed pass-2 handlers loud, individually identifiable, and trivially
  fillable later — each is a table slot, not a code change.

**Non-Goals:**

- Reconstructing the other 26 handlers now. They belong to dialogue, item lists and battle
  text, none of which the main menu reaches.
- Reconstructing the fonts or glyph rasterisation below `FUN_00543b20` / `FUN_00544150`.

## Decisions

### Model both tables as arrays of delegates, not a `switch`

The original *is* a data table, both tables are recovered verbatim, and they overlap. An
array keeps the correspondence to `Documentation/Reference/DataSymbols.md` visible and makes an
unimplemented slot a one-line fill. A `switch` would hide which slots share a handler —
`0x00543950` alone covers six codes.

*Alternative considered:* a `switch` on the control code. Rejected — it discards the fact
that the two tables differ only by substitution, which is the clearest evidence for what
pass 1 is for.

### Name the handlers by address, not by guessed role

`Handler_00544b10` rather than `EndOfPage`. Only three of the 31 are understood well enough
to name; inventing names for the rest would violate the project's traceability rule. The one
decoded here gets its behaviour documented in a comment instead.

### Replace `MenuBackgroundStub` outright

Its name asserts a role the function does not have, and its signature drops the descriptor
argument. Leaving it renamed-but-present would preserve a wrong call shape at the one site
that matters.

## Risks / Trade-offs

- **The decoded handlers are hand-read x86**, not Ghidra output → each is transcribed with
  its bytes quoted in a comment so the decoding can be rechecked without the binary. All are
  under 50 bytes and use no branches.
- **Pass 1 saves and restores 562 dwords of the descriptor** (`local_8c8`); if the copy
  length or the field offsets are wrong the corruption would be silent → the copy is
  transcribed as a fixed-size block move, not as field-by-field assignment.
- **The `0x8c4`/`0x8c5`/`0x8c7` offsets exceed the descriptor size currently allocated for
  the menu context** (`0x918` bytes at `DAT_0056dd58`) → within bounds, but the descriptor
  layout is inferred from touched offsets only; a bounds assertion is cheap insurance.
- **Stage 2 is large and may not finish in one session.** Stage 1 is written to stand alone:
  after it, the run either draws or names the next missing handler.

## Open Questions

- Whether `FUN_005443a0`'s pass 2 is reached at all for the menu, or whether flag bit 2 from
  a prior frame short-circuits it. Answered by running stage 1.
