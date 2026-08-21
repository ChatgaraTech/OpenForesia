## 1. Recover and record the table

- [x] 1.1 Dump `0x0056ACA8` far enough to reach the `0x0000` terminator (80 bytes suffices) and
      transcribe all 37 `ushort` entries.
- [x] 1.2 Sanity-check the recovered entries against the code before using them: every low byte
      (index) must be below `0x3c`, since the fixed pass only relocates `header[4..63]`. An index at
      or above that would mean the reading is wrong.
- [x] 1.3 Correct `Documentation/Reference/DataSymbols.md`: replace the `(record kind, size)` hypothesis with
      the confirmed `(index, stride)` reading, note that `FUN_00532070`'s masking and shifting is
      what settles it, and record the full table.

## 2. Reconstruct the schema-driven pass

- [x] 2.1 Fill `s_ResourceFixupSchema_0056aca8` with the recovered entries. Keep the trailing
      `0x0000` out of the array and drive the loop off array length instead, or keep it in and
      terminate on it — either is faithful, but say which in a comment so the next reader does not
      have to infer it.
- [x] 2.2 Reconstruct the `while (uVar5 != 0)` loop: per entry, take `header[4 + (entry & 0xff)]`,
      skip when that pointer is null **or** when its first dword is zero (the original checks both),
      then walk the chain in `(entry >> 8)`-byte steps relocating each dword until it reads zero.
- [x] 2.3 Use the correct relocation formula for this pass: `blob + value + header[1] + header[0]`.
      The fixed pass above uses `blob + value + header[0]` — one term shorter. Mixing them up
      produces pointers that are wrong by a constant and look valid.
- [x] 2.4 Preserve the original's read-then-advance order exactly: it relocates `*p`, then reads the
      next link from `p + stride`, then advances. Reordering changes which dword terminates the walk.
- [x] 2.5 Remove the `TODO(REVERSE)` that says the pass is an unavoidable no-op, and replace the
      surrounding comment with what the schema means.

## 3. Verify

- [x] 3.1 Build: `$env:DOTNET_ROOT="C:\Users\Sagara\.dotnet"; dotnet build OpenForesia.sln` — 0 errors.
- [x] 3.2 Re-read the reconstructed loop against `Blaze.c:79417-79437` line by line. This function
      does pointer arithmetic on freshly-loaded file data; a subtle error here corrupts memory
      instead of throwing, so reading it back is worth more than a passing build.
- [x] 3.3 Run with the CD mounted and a **temporary, uncommitted** bypass of the attract-loop throw
      (TODO #34), as `fix-cd-data-paths` did. Confirm `LoadResourceEntry` no longer faults.
- [x] 3.4 Confirm the run now reaches `DecodeResourceChunk`'s `NotImplementedException` — the
      expected outcome. If it faults somewhere else instead, that is a new finding to record rather
      than a reason to keep adjusting this pass.
- [x] 3.5 Revert the bypass and confirm no trace remains (`grep` for the marker, and check the file
      length against a pre-experiment copy).

## 4. Update the record

- [x] 4.1 Mark TODO #20 resolved in `REVERSE_STATUS.md`, with the confirmed `(index, stride)`
      reading rather than the old hypothesis.
- [x] 4.2 Update TODO #35 with the outcome — resolved if the fault is gone, or re-scoped if it moved.
- [x] 4.3 Update TODO #19's status. It has been described as the blocker for this path since before
      the path could even be reached; record whether it is now genuinely exercised.
- [x] 4.4 Update the "Next area to explore" section with the new runtime stopping point.
