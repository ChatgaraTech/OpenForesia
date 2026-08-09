## 1. Bring the binary into the repo

- [x] 1.1 Create a real `Legacy/` directory and copy `Blaze.exe` into it from
      `F:\Open Foresia Project\Official game\Blaze.exe`. `Legacy` is currently only a virtual
      solution folder, so the directory does not exist on disk yet.
- [x] 1.2 Verify the copy is byte-identical: MD5 must be `46d90d1b4526f4a9a441f851c77179b3`
      (1,589,760 bytes).
- [x] 1.3 Register `Legacy/Blaze.exe` as a solution item under the existing `Legacy` solution folder
      in `OpenForesia.sln`, alongside how `Blaze.c` is registered under `Legacy/Decompiled`. Do not
      move `Blaze.c` — its repo-root path is referenced throughout the documentation.
- [x] 1.4 Confirm `.gitignore` does not exclude it. The `[Bb]in/` and `[Oo]bj/` rules are the ones to
      check against a path like `Legacy/Blaze.exe`; no `*.exe` rule currently exists.
- [x] 1.5 Record in `CLAUDE.md` and `REVERSE_STATUS.md` that the binary is now in the repo at
      `Legacy/Blaze.exe`, with its MD5 and the identity check that proves it matches `Blaze.c`
      (image base `0x400000` + entry RVA `0x147D40` = `0x00547D40` = `entry`).

## 2. Extend OpenForesia.Tools

- [x] 2.1 Preserve today's behaviour: `OpenForesia.Tools <path>` with no verb still dumps the export
      table. `Documentation/DLL/winsys3d.md` and `wsyskern.md` describe that workflow and must keep
      working.
- [x] 2.2 Add `sections <path>`: print each section's name, virtual address range, raw file offset
      and size. This is what makes a wrong VA lookup diagnosable.
- [x] 2.3 Add `at <path> <va> [count]`: translate a Ghidra virtual address to a file offset and dump
      `count` bytes (default a sensible small window). Reuse the existing `RvaToFileOffset` helper —
      subtract the image base from the VA to get the RVA first. Print the VA, the RVA, the resolved
      file offset and the containing section name alongside the bytes.
- [x] 2.4 Render each dump three ways: hex, as a NUL-terminated C string, and as little-endian
      dwords. The blocked symbols include strings, function pointers and numeric tables.
- [x] 2.5 Fail loudly and specifically when a VA resolves outside every section, rather than
      returning a plausible-looking offset.
- [x] 2.6 Verify against a known answer before trusting the tool anywhere else: `at Blaze.exe
      0x005631f8` must print `@:\FRENCH\`, and `0x005631e8` must print `@:\Blaze&Blade`. Both were
      established during exploration by direct `xxd` at file offsets `0x1617f8` / `0x1617e8`, so this
      also confirms the VA→offset arithmetic end to end.

## 3. Recover the blocked data symbols

Work from the "Systemic blocker" table in `REVERSE_STATUS.md`. For each, dump the bytes and record
them; do **not** edit `Blaze.cs`.

- [x] 3.1 `DAT_005631f8` (CD data root) and `s____Blaze_Blade_005631e8` (CD marker path) — already
      known to read `@:\FRENCH\` and `@:\Blaze&Blade`; capture them formally as the canary entries.
- [x] 3.2 TODO #2 — `DAT_00563280`..`_0056328c`, the 16-byte CD-check filename template.
- [x] 3.3 TODO #3 — `PTR_s_Blaze___Blade_00563208`, the 5-entry string-pointer table for
      `Setup3DDevice`'s dialog. Dereference all five pointers, not just the array.
- [x] 3.4 TODO #4 / #17 / #22 — the message and filename strings
      (`s_Please_insert...`, `s_Erreur...`, the font/movie/archive names). Several were guessed from
      Ghidra's derived symbol names; exploration already showed the guesses are close but not exact
      (`DATA/Font.bmp` with a forward slash, `BLAZE.ALL;1` with an ISO-9660 suffix). Record the
      literal bytes.
- [x] 3.5 TODO #11 — the ini key / extension / format strings. Exploration found the real file is
      `BLAZE.cfg`, so the reconstruction's `".ini"` is wrong; confirm against the bytes.
- [x] 3.6 TODO #12 — `DAT_00562e00`, the per-resolution clamp table.
- [x] 3.7 TODO #20 — `DAT_0056aca8`, the resource-fixup schema table.
- [x] 3.8 TODO #21 — `DAT_006008c8` / `DAT_00604930` / `DAT_00600904` override-table base pointers.
- [x] 3.9 TODO #24 — `DAT_0056ac60` (16 shorts) and `DAT_0056ac80` / `DAT_0056ac90` (7 shorts each),
      the MS-ADPCM adaptation and coefficient tables. The reconstruction filled these from the
      published standard; this is the chance to confirm byte-for-byte rather than assume.
- [x] 3.10 TODO #27 — `DAT_005631d4`, whether music is enabled at startup.
- [x] 3.11 TODO #30 — `PTR_s____Music_Mel00_01_WAV_00566cc8`, the music filename pointer table.
      Call sites index it up to at least `0x26`, so dump the whole table and dereference each entry.
- [x] 3.12 TODO #10 — `AU_00564a05`, the uncharacterised blob.

## 4. Resolve TODO #15 — the game loop's tick callback

- [x] 4.1 Dump `PTR_FUN_0056c96c` as a little-endian dword; that value is the tick function's VA.
- [x] 4.2 Search `Blaze.c` for a `FUN_<addr>` matching it. `Blaze.c` defines 1426 functions, so
      there is a good chance the body is already available and no Ghidra re-run is needed.
- [x] 4.3 Record the outcome either way — the function name if found, or an explicit "target is not
      among `Blaze.c`'s function bodies, needs a scoped Ghidra re-run at `0x…`" if not.
- [x] 4.4 Do **not** wire it into `RunGameLoop`. That is a separate change; note it as the obvious
      next one.

## 5. Document

- [x] 5.1 Create `Documentation/DataSymbols.md`, keyed by Ghidra VA. Per entry: VA, file offset,
      containing section, raw bytes, and — separately labelled — any interpretation of those bytes.
      Keep raw and interpreted distinct, as `Documentation/DLL/*.md` already does for confirmed vs
      hypothesised behaviour.
- [x] 5.2 Rewrite `REVERSE_STATUS.md`'s "Systemic blocker — `Blaze.exe` itself is missing from the
      repo" section. As written it is now factually wrong and is the single most misleading thing in
      the file: it tells a future session that a whole class of work is impossible.
- [x] 5.3 Update each resolved TODO in place with its value and a link to `DataSymbols.md`.
- [x] 5.4 Preserve the distinction the status file already draws correctly: TODOs #13, #14, #19 and
      #23 are **not** fixed by having the binary. They need a Ghidra re-run — address-taken-only
      bodies (`LAB_0054dxxx`, `LAB_00542600`, `LAB_00513660`) and `FUN_005322f0`, whose own
      decompilation is incomplete. Keep them clearly separated from the data-blocked group, and note
      that having the binary now makes that re-run possible too.
- [x] 5.5 Add the tool's new verbs to `CLAUDE.md` next to the existing ordinal-resolution workflow.

## 6. Verify

- [x] 6.1 Build: `$env:DOTNET_ROOT="C:\Users\Sagara\.dotnet"; dotnet build OpenForesia.sln` — 0 errors.
- [x] 6.2 Re-run the export dump against `winsys3d.dll` and confirm the output is unchanged from the
      documented workflow (regression check for task 2.1).
- [x] 6.3 Confirm every entry in `DataSymbols.md` records a file offset that falls inside the section
      it claims.
- [x] 6.4 Cross-check at least two recovered strings against the actual CD in `E:` — for example
      `@:\FRENCH\` against `E:\French\`, and a movie or archive filename against the files on disc.
      Independent corroboration, not just internal consistency.
- [x] 6.5 Confirm `Blaze.cs` is untouched by this change (`git diff --stat` should show no
      `Blaze.cs`), since applying values is explicitly out of scope.
