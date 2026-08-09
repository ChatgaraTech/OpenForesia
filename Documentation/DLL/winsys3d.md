# winsys3d.dll

Decompiled source: `winsys3d.c` (repo root, Ghidra output, **read-only**). Base load
address used throughout that file's `@ 0xXXXXXXXX` comments: `0x00a00000`
(i.e. `winsys3d.c address = 0x00a00000 + export RVA`).

Blaze & Blade's own 3D-engine helper DLL. Exports 830 ordinals; most are unnamed
(no export name — Ghidra shows these as `Ordinal_NNN` in `Blaze.c`). A relatively
small number are named exports, generally higher-level entry points.

Export table was resolved with a small pure-Python PE32 export-directory parser
(session scratchpad `pe_exports.py`, not committed) run directly against
`winsys3d.dll` sitting in the repo root, then cross-checked against the matching
function body in `winsys3d.c` by RVA.

## The libgpu family (ordinals 658 / 661 / 665 / 674) — **IDENTIFIED**

These are a port of the **PlayStation SDK's `libgpu`**, which is consistent with
the sibling DLL being named `playst.dll`. Blaze & Blade is a PSX title, and this
part of `winsys3d.dll` is its graphics layer carried over almost verbatim.

| Ordinal | winsys3d.c | libgpu equivalent | Confidence |
|--------:|------------|-------------------|------------|
| 658 | `FUN_00a05ac0` @ `0xa05ac0` | `SetDefDispEnv(DISPENV*, x, y, w, h)` | **certain** |
| 661 | `FUN_00a05b50` @ `0xa05b50` | `SetDefDrawEnv(DRAWENV*, x, y, w, h)` | **certain** |
| 674 | `FUN_00a05d50` @ `0xa05d50` | `LoadImage(RECT*, u_long* data)` | high |
| 665 | `FUN_00a05e90` @ `0xa05e90` | `ClearImage`-shaped, see caveat | partial |

Three independent lines of evidence pin 658 and 661:

1. **Field-for-field body match.** 658 writes `disp={x,y,w,h}`,
   `screen={0,0,0x100,0xf0}`, `isinter=0`, `isrgb24=0` — the documented
   `DISPENV` layout. 661 writes `clip={x,y,w,h}`, `ofs={x,y}`, `tw={0,0,0,0}`,
   `tpage=10`, `dtd=1`, `dfe=1`, `isbg=0`, `r0=g0=b0=0` — the documented
   `DRAWENV` prefix.
2. **Struct spacing proves the sizes.** The four globals `Blaze.exe` passes sit
   at `0x005e9880`, `0x005e98dc`, `0x005e98f0`, `0x005e994c` — alternating gaps
   of **92** and **20** bytes, exactly `sizeof(DRAWENV)` and `sizeof(DISPENV)`.
3. **The caller's follow-up stores all land on valid fields** of those layouts
   (`+0x08..+0x0e` = `DISPENV.screen`, `+0x16` = `DRAWENV.dtd`,
   `+0x18` = `DRAWENV.isbg`).

```
DISPENV (20) = { RECT disp; RECT screen; u_char isinter, isrgb24, pad0, pad1; }
DRAWENV (92) = { RECT clip; short ofs[2]; RECT tw; u_short tpage;
                 u_char dtd, dfe, isbg, r0, g0, b0; ... }
RECT     (8) = { short x, y, w, h; }
```

**Argument recovery caveat**: Ghidra does *not* render these calls' arguments as
arguments in `Blaze.c`. It spills them into unattributed stack slots
(`uStack_18`, `puStack_28`, …) and, for one call, emits only three of the five.
Read the slots back in **ascending address order** — the cdecl push order — to
recover `(env, x, y, w, h)`. Getting this wrong caused a null-pointer crash in
the reconstruction; see `REVERSE_STATUS.md` TODO #32.

`LoadImage` (674) early-outs on `rect.y > 0x1ff`, confirming the familiar
1024x512 VRAM addressing. **Caveat on 665**: it takes a `RECT` and floods VRAM
like `ClearImage`, but its colour math decompiles as
`((p4 & 0xff) << (p3 + 10)) << (p2 + 5)`, which does not match libgpu's plain
`(r, g, b)` parameters — left un-renamed in `Blaze.cs` until explained.

## Confirmed named exports used by Blaze.exe

### `Setup3DDevice` (ordinal 823, RVA `0x1910`, `winsys3d.c:386`) — **ANALYZED**

**The name is misleading: this does not create a 3D device.** It is the game's
**renderer-selection dialog / launcher**, shown once at startup.

Body (`winsys3d.c:389-500`):

1. `GetModuleFileNameA` + `_strrchr('.')` + `lstrcatA` → builds the ini path
   (same `<exe>.ini` convention `LoadOptionsIni` uses on the Blaze.exe side).
2. Reads `[D3DDriver] Driver` and `[D3DDriver] Description`, plus
   `[Options] Resolution` (default 2), from that ini.
3. If `hBitmap != NULL`, calls `FUN_00a010d0` (below) — the actual dialog.
   If it returns −1 the user cancelled: `DeleteObject` both GDI objects and
   `ExitProcess(1)`.
4. Sets two DLL globals from the chosen driver name:
   `DAT_00a2c738 = (driver == "GLIDE")`, `DAT_00a2c73c = (driver == "SOFTWARE")`.
5. Writes the chosen `Driver`/`Description` back to the ini.

`FUN_00a010d0` (`winsys3d.c:11`) is where the real work happens:

- Seeds the list with a `"Software"` / `"Direct3D Software Emulation"` entry.
- `FUN_00a017d0` → `DirectDrawCreate(NULL)` + `EnumDisplayModes` (vtable 0x20)
  + `Release` — display-mode probe, **not** device creation.
- `DirectDrawEnumerateA` → appends each hardware DirectDraw driver.
- `LoadLibraryA("GLIDE2X.DLL")` + `GetProcAddress("_grSstQueryBoards@4")` →
  if a 3Dfx board is present, appends a `"Glide"` / `"3Dfx Glide"` entry.
- `DialogBoxParamA(hInstance, MAKEINTRESOURCE(0x66), parentHwnd, FUN_00a012f0, …)`.

**Resource 0x66 is the same id `RunApplication` passes to `wsyskern.dll`'s
`LoadBitmapEx`** — confirming that `hBitmap`/`hPalette` are that dialog's
splash image, which is why they are `DeleteObject`'d immediately afterwards.

#### Argument struct layout (built on `Setup3DDevice`'s stack, passed to `FUN_00a010d0`)

Cross-checked against every dereference in both functions:

| Offset | Contents | Evidence |
|---|---|---|
| `0x000` | parent `HWND` (always 0 here) | `DialogBoxParamA(..., *(HWND*)param_1, ...)` |
| `0x004` | driver name, 20 bytes | `local_324[20]` |
| `0x018` | description, 256 bytes | `pcVar8 = param_1 + 0x18` writes `"unknown"` |
| `0x118` | resolution index | dialog proc writes it at `+0x118` |
| `0x11c` | `hBitmap` | `DeleteObject(*(HGDIOBJ*)(param_1 + 0x11c))` |
| `0x120` | `hPalette` | `DeleteObject(*(HGDIOBJ*)(param_1 + 0x120))` |
| `0x124` | **`char**` — 5 UI strings** | dialog proc, below |

#### `Setup3DDevice`'s 3rd argument, resolved

The dialog proc's `WM_INITDIALOG` handler (`winsys3d.c:218-223`) dereferences
the `+0x124` pointer as an array of five ANSI string pointers:

```c
SetWindowTextA   (dlg,       strings[0]);  // dialog title
SetDlgItemTextA  (dlg, 0x3e9, strings[1]); // static text
SetDlgItemTextA  (dlg, 0x3ea, strings[2]); // static text
SetDlgItemTextA  (dlg, 1,     strings[3]); // OK button
SetDlgItemTextA  (dlg, 2,     strings[4]); // Cancel button
```

So `Blaze.c`'s `&PTR_s_Blaze___Blade_00563208` is a **5-entry string-pointer
table** whose first entry is the `"Blaze & Blade"` title. The array pointer
itself is null-checked, but all five entries are then dereferenced
unconditionally — a non-null array must supply five valid strings.

Dialog control ids, for reference: `1000` = driver combo box, `0x3eb` = display-mode
combo, `0x3e9`/`0x3ea` = static labels, `1`/`2` = OK/Cancel.

This resolved two long-standing `REVERSE_STATUS.md` TODOs (#1 and #3) and fixed a
real defect: `Blaze.cs` had been passing its 4-byte `"X:\"` drive-root buffer as
this argument, which would have made the DLL read five garbage pointers off the
stack and dereference them.

### `CreateTranslationTables(float gamma)` (ordinal 756, RVA `0x2f50`, `winsys3d.c:2373`)

```c
void CreateTranslationTables(float param_1)
{
  if (param_1 != _DAT_00a2c228) {
    _DAT_00a2c228 = param_1;
    // ... builds an 8-bit gamma LUT via Gamma(i, param_1) for i in 0..255,
    // then builds several 16-bit RGB565/RGB555-ish packed-color translation
    // tables (DAT_00a8d07a/DAT_00aad07a/DAT_00acd07a/DAT_00add07a) from it.
  }
}
```

- Pure DLL-internal state (writes only to `winsys3d.dll`'s own private globals);
  from `Blaze.exe`'s perspective it's a fire-and-forget call with a single
  `float` gamma multiplier.
- Caches on `_DAT_00a2c228`: calling again with the same `gamma` value is a
  no-op, so it's safe to call from both load and any future gamma-change code
  path.
- **Confirmed default**: `winsys3d.c:2484` itself calls
  `CreateTranslationTables(0x3f800000)` (i.e. `1.0f`) as part of its own device
  setup (`FUN_00a03110`). This is what let us infer `Blaze.c`'s
  `DAT_0054d000` gamma-scale constant (used as
  `CreateTranslationTables((float)(int)DAT_005d9de0 * _DAT_0054d000)`) — the
  ini's default `Gamma` value is `0x40` (64), and `64 * (1/64) == 1.0`, which
  matches winsys3d.dll's own default. `Blaze.cs` uses `GammaIniScale = 1/64f`
  based on this cross-reference. **Confidence: high, but not proven** — no
  literal float bit pattern for `DAT_0054d000` survives in `Blaze.c`'s text.

**Used by**: `LoadOptionsIni` (Ghidra: `FUN_0050f140`) in `Blaze.cs`.

### `TexFilterPossible` — imported **data**, not a function (no ordinal; plain export)

`Blaze.c` references it as `TexFilterPossible_exref` (Ghidra's naming
convention for an imported data symbol, as opposed to `Ordinal_NNN`/named
function imports). Confirmed in `winsys3d.c`: a plain `uint` global, read/write
in several places (e.g. `winsys3d.c:4111`, `:5762`, `:5936`) that gates
whether texture filtering is applied.

`Blaze.cs` resolves this at runtime via `LoadLibraryA("winsys3d.dll")` +
`GetProcAddress(..., "TexFilterPossible")` (P/Invoke has no direct mechanism
for binding an imported *data* symbol the way the PE loader does), then reads
it as a live `uint*`. Written from the ini's `Filter` setting in
`LoadOptionsIni`.

## Confirmed ordinal-only exports used by Blaze.exe

All four below are called from `RunApplication` and were resolved by RVA
lookup + reading the matching `winsys3d.c` function body.

| Ordinal | RVA | `winsys3d.c` symbol | Behavior | `Blaze.cs` name |
|---|---|---|---|---|
| 331 | `0x26220` | `FUN_00a26220` @ `0x00a26220` | Thin `CreateFileA` wrapper. `param_2` selects access mode (0=`GENERIC_READ`, 1=`GENERIC_WRITE`, 2=both); always `FILE_SHARE_READ`, `OPEN_EXISTING`, `FILE_ATTRIBUTE_NORMAL`. All 3 Blaze.c call sites pass mode 0. | Not called directly anymore — `Blaze.cs` calls `CreateFileA` itself (`OpenExistingFileReadOnly` helper), since the DLL layer adds nothing. |
| 336 | `0x26300` | `FUN_00a26300` @ `0x00a26300` | Literally `CloseHandle(param_1); return 0;` | Same — `Blaze.cs` calls `CloseHandle` directly. |
| 337 | `0x1df00` | `FUN_00a1df00` @ `0x00a1df00` | Builds the engine's trig lookup table: fills a 0x1000-entry table (`DAT_011cb420`) via `fsin()`, plus several related derived tables. No parameters, called once. | `InitTrigTables()` |
| 450 | `0x25c20` | `FUN_00a25c20` @ `0x00a25c20` | `return DAT_011cb420[(index + 0x400) & 0xfff];` — reads the same 0x1000-entry table built by ordinal 337, phase-shifted by a quarter period (`0x400` of `0x1000`). Given ordinal 451 below is the same table unshifted, this pair is a classic `cos(x) = sin(x + period/4)` lookup. | `GetCosTableEntry(int index)` |
| 451 | `0x25c40` | `FUN_00a25c40` @ `0x00a25c40` | `return DAT_011cb420[index & 0xfff];` — direct read from the sin table. | `GetSinTableEntry(int index)` |
| 790 | `0x6410` | `FUN_00a06410` @ `0x00a06410` | `DAT_011be798 = param_1; return;` — stores the pointer into a DLL-private global. Confirmed elsewhere in `winsys3d.c` (near `0x00a06420`/`FUN_00a03110`) that `DAT_011be798` is later called indirectly as a callback (`if (DAT_011be798 != NULL) (*DAT_011be798)();` and a second call site passing an extra argument `DAT_011be79c`). Likely a device-lost/reset or per-frame notification hook, but not confirmed. | `SetDeviceLostCallback(void* callback)` |

**Open question**: the callback Blaze.exe registers via ordinal 790
(`Blaze.c`: `&LAB_00512ef0`) has no decompiled function body anywhere in
`Blaze.c` — Ghidra only saw it address-taken, never called directly, so it
never got promoted to a standalone `FUN_`/`LAB_` block with a body. Cannot be
reconstructed without further disassembly at `0x00512ef0`.

## Ordinals used by RunMainLoop (Ghidra: FUN_00529c10) and its dependency graph

Resolved this session while reconstructing `RunMainLoop`. Method: winsys3d.c embeds the
ordinal number directly in a comment inside each function body (e.g. `/* 0x2490  129 */`),
so ordinals were matched to their implementation by grepping those comments directly —
faster and more reliable than re-deriving RVAs from the PE export table by hand (still
useful as a cross-check; `pe_exports.ps1`, a PowerShell port of the earlier session's
`pe_exports.py`, was added to the scratchpad since no working `python` was on PATH this
session).

### Confirmed literal no-ops (real DLL behavior — not a Blaze.cs simplification)

Several ordinals fold (via linker COMDAT/identical-code folding) onto one of a handful of
trivial stub bodies in the shipped `winsys3d.dll`. Confirmed by reading each stub body:

| Stub body | Behavior | Ordinals observed folded onto it (subset actually used from `Blaze.c`) |
|---|---|---|
| `FUN_00a02490` | `return 0;` | 116, 129, 64, and many more unused ones |
| `FUN_00a02870` | `return;` | 712, 720, 63, and many more unused ones |
| `FUN_00a02e20` | `return 0;` | 45, 104, 110, 681, 766, 768, 769, 770, and many more unused ones |
| `FUN_00a02e40` | `return;` | 767, and many more unused ones |
| `FUN_00a26470` | `return;` | 138, 771, 66, and many more unused ones |

These are declared as real P/Invokes in `Blaze.cs` and called for real (not specially
skipped) — "does nothing" is the DLL's actual, confirmed behavior at these ordinals, not
an assumption.

### Confirmed real behavior

| Ordinal | RVA | `winsys3d.c` symbol | Behavior |
|---|---|---|---|
| 41 | `0x4fe0` | `FUN_00a04fe0` | `LeaveCriticalSection(&DAT_00b6d6d8)` — releases the DLL's own internal device lock (paired with an `EnterCriticalSection` at `FUN_00a04ff0`, not itself called from the traced graph) |
| 132 | `0x53e0` | `FUN_00a053e0` | VSync/frame-pacing wait, keyed off a DLL-internal timing-mode global (`DAT_00a2c3b0`); pumps Win32 messages internally while waiting |
| 136 | `0x5770` | `FUN_00a05770` | Get-and-set on `DAT_011be6a4` ("frame time offset"? unconfirmed), returns the previous value |
| 368 | `0x20ed0` | `FUN_00a20ed0` | Copies 8 dwords from the caller into DLL-internal "shading defaults" state (`EXT_011d0828`); exact per-field meaning unconfirmed |
| 378 | `0x21020` | `FUN_00a21020` | Stores two dwords into DLL-internal state (`EXT_011cb414`/`EXT_011cb410`); exact meaning unconfirmed |
| 658 | `0x5ac0` | `FUN_00a05ac0` | Fills a caller-owned "surface descriptor" struct (position/size fields; rest defaulted) |
| 661 | `0x5b50` | `FUN_00a05b50` | Fills a richer caller-owned "surface descriptor" variant (adds default clip/flag fields) |
| 665 | `0x5e90` | `FUN_00a05e90` | Fills the video-memory region described by a rect with a flat RGB color |
| 668 | `0x20a0` | `FUN_00a020a0` | Registers a debug/console text-output channel (position, size, capacity); up to 8 slots. Appears to back an on-screen debug console feature not exercised by the shipped build's normal boot path |
| 674 | `0x5d50` | `FUN_00a05d50` | Blits a source buffer into the video-memory region described by a rect |
| 710 | `0x26e0` | `FUN_00a026e0` | (Re)creates the DLL's text/console overlay bitmap at a given size |
| 789 | `0x6420` | `FUN_00a06420` | Device (re)init/teardown dispatcher keyed off its first argument; storing the 2nd argument into `DAT_00a2c49c` is its only effect when the 1st argument is 0 (the only value any traced call site passes) |
| 796 (`SetShift`, named export) | `0x5810` | `SetShift` | Camera/view pixel-offset + zoom-level setter; with `flag==0` also calls `SetCooperativeLevel`-adjacent code on the DLL's main device interface (`DAT_011be760`) |
| 6 | `0x40d0` | `FUN_00a040d0` | Allocates and registers a generic "handler" node (message/id, 2 extra words, callback pointer) into a DLL-internal linked list; returns an opaque handle |
| 10 | `0x4220` | `FUN_00a04220` | Unregisters a handler node returned by ordinal 6 (marks it inactive; does not free it — see `FUN_00a04270`/`FUN_00a042d0` for the actual dispatch/free side, not yet read) |
| 750 | `0x1e30` | `FUN_00a01e30` | `return DAT_011be760;` — the DLL's main device-interface pointer |
| 40 | `0x4fd0` | `FUN_00a04fd0` | Body not read this session (out of scope — only reached via `FUN_00504460`, which stops mattering once its own `FUN_00523120` dependency throws first) |

**Open question (`Ordinal_6`'s callback targets)**: `FUN_00504460` (Ghidra) registers 8
handlers via ordinal 6, passing `&LAB_00504610` .. `&LAB_00504680` as callbacks — none of
these have a decompiled body anywhere in `Blaze.c` (same situation as ordinal 790's
`&LAB_00512ef0`, see above). `Blaze.cs`'s `SetupJoystickHandlers` passes `null` for all 8
and documents this with a TODO(REVERSE).

## Confirmed unnamed pair (both DLLs), noted for context

`3dhelp.dll` and `gldhelp.dll` each export exactly one function, both named
`Create3DDevice` (ordinal 1 in both). Neither is called from any part of
`Blaze.c` reconstructed so far — this strongly suggests a renderer-swap
architecture (Direct3D helper vs. OpenGL helper) selected somewhere in the
not-yet-reconstructed engine/device init, not from `Setup3DDevice` itself
(which lives in `winsys3d.dll`, not either helper DLL). Revisit once
device/window creation is reconstructed.
