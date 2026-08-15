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

## The display list is addressed on 24 bits — **CONFIRMED**, and it constrains the whole port

This is the single most load-bearing fact about this DLL. It is read directly out of
`winsys3d.c`, not inferred.

`Ordinal_688` (`FUN_00a06160` @ `0xa06160`, libgpu `ClearOTagR`) builds the ordering table
with **24-bit links and a zero top byte**:

```c
*puVar2 = ((uint)puVar1 ^ *puVar2 & 0xffffff) & 0xffffff ^ *puVar2 & 0xffffff;
*param_1 = 0xffffff;                       /* the end-of-list sentinel */
```

which reduces to `*puVar2 = (uint)puVar1 & 0xffffff`. `Ordinal_701`
(`FUN_00a061b0` @ `0xa061b0`, `DrawOTag`) walks it the same way, and reads the freed top
byte as the primitive's word count:

```c
if ((char)(*puVar2 >> 0x18) != '\0') { FUN_00a26b20(puVar2); }   /* top byte = length */
puVar2 = (uint *)(*puVar2 & 0xffffff);                           /* 24-bit link */
} while (puVar2 != (uint *)&DAT_00ffffff);
```

`Blaze.exe` builds its tags to match — `DrawFlatPrimitive` does
`*prim = *otEntry | 0x2000000`, packing the length into a top byte it assumes is free.
**Both sides of the interface depend on it**, so it cannot be worked around on either.

**Consequence:** every ordering table and every primitive buffer must live below
`0x01000000`. In the original this was automatic — `Blaze.exe` imports `winsys3d.dll`
*statically* (the name sits in its import table at file offset `0x1703AC`, the only one of
the five DLLs so imported), the loader mapped it at its preferred base `0x00A00000`, and
`winsys3d.c:568` set `MainMem = &DAT_00b6dbd8` = `0x00B6DBD8`.

### The `MainMem` arena

`MainMem` is a **plain exported data pointer**, and a genuine indirection: it occurs
exactly three times in 27 703 lines.

| `winsys3d.c` | code | role |
|---|---|---|
| 568 | `MainMem = &DAT_00b6dbd8;` | the sole initialisation, in `entry()` |
| 5655 | `DAT_00dedbd8 = iVar1 + 0x240000 + MainMem;` | `DrawOTag` — re-derived every frame |
| 5843 | `DAT_00dedbd8 = MainMem + 0x200000;` | `Ordinal_789`'s device init — derived **once** |

`DAT_00b6dbd8` itself is referenced only at line 568, and **no symbol between
`MainMem + 0x0` and `MainMem + 0x280000` is addressed by a fixed address** — the DLL's
globals resume exactly at `MainMem + 0x280000` (`DAT_00dedbd8` and 36 others). The arena is
therefore `0x280000` bytes wide and fully relocatable by rewriting the exported pointer.

Known usage inside it:

| offset | user |
|---|---|
| `+0x1c6000` / `+0x1c7000` | ordering tables, one per draw env (`Blaze.exe`, `FUN_0052a1e0`) |
| `+0x1c8000` / `+0x1e4000` | primitive buffers, one per draw env (`Blaze.exe`, `FUN_0052a1e0`) |
| `+0x200000` / `+0x240000` | winsys3d's double-buffered scratch (lines 5843 and 5655) |

`entry()` does **not** populate the arena: it sets the pointer, takes a separate 1 MB
`Malloc(0x100000)` into `EXT_00b6dbd0`, and loads font/KANJI resources into unrelated
globals. There is nothing in it to preserve across a relocation.

**Ordering matters**: line 5843 caches its derived pointer once and never recomputes it, so
the arena must be fixed before `Ordinal_789` is first called. Line 5655 re-derives every
frame and is self-correcting, which is a safety net, not a licence to relocate late.

See `REVERSE_STATUS.md` for how the reconstruction restores this on modern Windows.

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

## Coordinate scaling: `Size`, the lookup tables, and the display-mode table — **CONFIRMED**

Established 2026-08-12 while investigating task 4.5 of `replace-3d-backend-with-d3d11`
("geometry lands in a ~320x240 space inside a 960x720 target"). Values below were read out
of `winsys3d.dll` itself with `OpenForesia.Tools -- at winsys3d.dll <VA> <n>`, not inferred.

### One variable drives both the viewport and the vertex scale

`SetShift(x, y, Size, flag)` (ordinal 796, `winsys3d.c:4937`) does two things at once:

```c
device->vtable[0x50](base_x[mode] + x, base_y[mode] + y,
                     (Size + 9) * 0x20,          /* width  */
                     (Size + 9) * 0x18);         /* height */
DAT_00a2c498     = Size;
PTR_DAT_00a2c4a0 = &DAT_00a31138 + Size * 16000;   /* X lookup table */
PTR_DAT_00a2c4a4 = &DAT_00a31138 + Size * 16000;   /* Y lookup table */
```

So the viewport handed to `3dhelp` slot `+0x50` — the one `DeviceShim` records as the render
size — is `(Size+9)*32` by `(Size+9)*24`. `Size = 1` gives 320x240, `Size = 11` gives 640x480,
`Size = 21` gives **960x720**.

**Confirmed at runtime the same day.** `OpenForesia.cfg` reads `Resolution=4 Size=21`, and the
backend reports a 960x720 scene — `(21+9)*32 = 960`, `(21+9)*24 = 720`. The deduction above is
therefore measured, not just derived.

**Correction on the display-mode claim.** An earlier revision of this paragraph read the
reconstruction's own `[MODE] après init 3D: 1440x900` log line as proof that `3dhelp` switches the
desktop mode. That log is an **in-process** `GetDeviceCaps` reading and does not establish what the
desktop is doing. Polled from a separate process every 150 ms across a full startup (256 samples,
40 s), the desktop stayed at its native mode throughout. On the legacy startup path the same poll
does see real changes — 59 samples at 640x480, 3 at 1440x900 — so mode changes are genuinely
observable when they happen; the `3dhelp` one simply was not, on this machine, once the game no
longer holds DirectDraw exclusively. Whether it is shorter than the sampling interval or purely
in-process is not settled.

Vertices are **not** scaled inline. They are mapped through a float lookup table indexed by the
raw coordinate (`winsys3d.c:10328` and ~40 similar sites):

```c
x_final = ((float *)PTR_DAT_00a2c4a0)[coord_x] + (float)DAT_011be770;
```

`ShiftVertices` (ordinal 824, `winsys3d.c:4913`) states the factor the tables are meant to
encode:

```c
factor = (float)((DAT_00a2c498 + 9) * 0x20) * _DAT_00a2a06c;
```

`_DAT_00a2a06c` at VA `0x00a2a06c` (`.rdata`) is `0x3B4CCCCD` = **0.003125 = 1/320**. So
`factor = (Size+9)*32 / 320`: `Size=1` → x1.0, `Size=21` → **x3.0** — exactly the 320 → 960
ratio observed on screen.

### The tables are built at runtime, not stored

VA `0x00a31138` reads as all zeros on disk. Two consequences: the tables are filled in by code,
and their content can only be inspected in a live process. Note also that `SetShift` assigns the
**same** address to the X and Y table pointers, while the high-resolution branch below assigns
two addresses 160 000 bytes apart. One of the two readings is wrong — most likely Ghidra
mis-recovered one line of `SetShift`. Unresolved; needs a runtime dump, not more reading.

### Root cause of the mis-scaled menu — **found 2026-08-12, and it is neither earlier hypothesis**

Measured at runtime with the geometry dump, sampling the shift globals at the first draw and again
in the menu's steady state:

| | `Size` | table factor |
|---|---|---|
| first draw | 21 | 3.0 |
| draw 10 000 (menu steady state) | **11** | **2.0** |

So the engine **changes `Size` after the viewport has been set**, and the culprit is the guard in
`SetShift` itself:

```c
if (param_4 == 0) {                    /* only when this flag is zero */
    device->vtable[0x50](base_x[mode] + x, base_y[mode] + y,
                         (Size + 9) * 0x20, (Size + 9) * 0x18);   /* sets the viewport */
}
DAT_00a2c498     = Size;               /* but Size and the table change unconditionally */
PTR_DAT_00a2c4a0 = &DAT_00a31138 + Size * 16000;
```

**Called with a non-zero fourth argument, `SetShift` changes the vertex scale without touching the
viewport.** The render target therefore keeps the size implied by the *last* `Size` that did set a
viewport (21 → 960x720), while vertices are emitted for the *current* `Size` (11 → 640x480). Hence
an image confined to the upper-left of the target.

Everything checks out arithmetically: with factor 2 and an offset of `(32, 24)`, the measured
envelope `X = [32..352]`, `Y = [24..216]` corresponds to raw coordinates `0..160` by `0..96`, and
the sampled quad at `x = 80, 192` to raw `24` and `80`.

Two hypotheses are therefore **disproved** and should not be revisited: the lookup tables are
correct and correctly selected (`table[100] = 300` for `Size = 21`, `table[320] = 960`), and the
high-resolution branch below is **not** active — `resIndex` stays 4 and the table pointer always
matches `base + Size * 16000`.

**But the obvious fix does not work, and that is itself a finding.** Reading `DAT_00a2c498` at
`BeginScene` and resizing the render target accordingly was implemented and measured: it never
triggers. The value is **21 at every `BeginScene`** and 11 by the 10 000th draw of the same frame —
so `Size` changes *within* a frame and is reset before the next one. The engine therefore sets a
960x720 viewport each frame and then draws with factor 2, every frame, deliberately.

What that leaves open: whether the original device also received a 960x720 viewport and vertices in
a 640x480 space — in which case the mis-scaling is not a reconstruction defect at all and the
compensation belongs elsewhere — or whether slot `+0x50` does something a plain render-target size
does not capture. `+0x50` maps to `SetCurrentViewport` on the D3D3 device, which sets a viewport
*inside* a surface whose size is the display mode; that is not the same thing as choosing the size
of an off-screen target, and the difference is the most likely place the remaining factor hides.

**The full sequence, measured (`OPENFORESIA_TRACE_VP=1`).** Three facts, all stable across frames:

- **`+0x50` is called exactly once**, at startup: `SetViewport(32, 24, 960x720)`. Not per frame.
  And `(32, 24)` is precisely the composed offset — so the argument pair is a **position**, and the
  call describes a sub-rectangle inside a larger surface, not the size of a render target. On D3D3
  that is what `SetCurrentViewport` means: the surface is the display mode's size (1024x768 for
  `Resolution = 4`), and the viewport is a window into it.
- **Per-frame `Size` pattern**: 21 at `BeginScene`, then 11 two draws later, and 11 for the whole
  body of the frame. Every frame, identically. The 21 is transient.
- **The emitted content is a grid of 30-pixel cells** starting at `(64, 56)`, columns at 64, 94,
  124 …, rows at 56, 86, 116, 146, 176, 200 — envelope `320x192`, which is exactly a 320x240 PSX
  space with its used rows, never enlarged.

So the geometry arrives in PSX space, offset by the composed shift, and nothing scales it up.

**Next measurement, and it is the decisive one.** The menu's drawing was reconstructed in C#
(change `reconstruct-main-menu-rendering`), so the **raw display-list coordinates are available on
our side**. Logging them next to the `TLVERTEX` values that come out of `DrawPrimitive` gives the
factor actually applied, as a ratio rather than an inference — which settles between the two
readings that coordinate arithmetic alone cannot separate (factor 2 over raw 0..160, or factor 1
over raw 0..320).

### The high-resolution branch bypasses the whole mechanism

`FUN_00a05a00` (`winsys3d.c:5042`, in substance `SetDispEnv`) copies a 5-dword DISPENV then
branches on its width:

```c
if (dispenv.width <= 320) {                 /* normal path */
    SetAbsShift((320 - w) / 2, (240 - h) / 2);
    /* Size preserved, tables = base + Size * 16000 */
} else {                                    /* high-resolution path */
    DAT_00a2c498     = 0;                   /* overwrites the Size set by SetShift */
    DAT_011be778     = 0;   DAT_011be77c = 0;
    PTR_DAT_00a2c4a0 = &DAT_00a34fb8;       /* two fixed addresses, outside */
    PTR_DAT_00a2c4a4 = &DAT_00a5c0b8;       /* the Size-indexed family      */
    DAT_011be770     = 0;   DAT_011be774 = 0;
}
```

This branch **decouples the viewport from the coordinate mapping**: the viewport keeps whatever
`SetShift` last pushed, while the vertex tables fall back to fixed ones. It is the leading
candidate for the symptom recorded in `TODO.md`, and it is discriminable by one runtime
measurement — read `PTR_DAT_00a2c4a0` and a few entries of the table it points at, and compare
`table[100] / 100` against the expected factor.

### The display-mode table — **CONFIRMED, read from the binary**

`DAT_00a2c49c` is a display-mode index, set by `ChangeResolution` (ordinal 749,
`winsys3d.c:6010`). The four arrays it indexes:

| index | width `0xa2c4a8` | height `0xa2c4c0` | `base_x` `0xa2c4d8` | `base_y` `0xa2c4f0` |
|---|---|---|---|---|
| 0 | 320 | 240 | −160 | −120 |
| 1 | 512 | 384 | −64 | −48 |
| 2 | 640 | 480 | 0 | 0 |
| 3 | 800 | 600 | +80 | +60 |
| 4 | 1024 | 768 | +192 | +144 |

The offsets are exactly `(width − 640) / 2` and `(height − 480) / 2`. **The engine's logical
reference space is 640x480**, and `base_x`/`base_y` centre that image inside the real display
mode. This had not been recorded anywhere in the repo before.

Both arrays are terminated by `0x00000000` / `0xFFFFFFFF` past index 4, so the valid range is
0..4 — the bounds check `(uint)resIndex < 8` in `DeviceShim.LogShiftGlobals` is looser than the
data warrants.

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

## The libgte family (ordinals 376 / 377 / 378 / 379 / 457) — **IDENTIFIED**

Sibling of the libgpu family above: where 658/661/665/674 are the PlayStation SDK's **libgpu**,
these are its **libgte** (geometry engine). Identified from their bodies in `winsys3d.c`, all
found by grepping the ordinal with surrounding spaces as the DLL embeds it in a comment.

| ordinal | `winsys3d.c` | libgte name | body |
|---|---|---|---|
| 376 | `FUN_00a20fe0` @ `0x20fe0` | `SetBackColor(r,g,b)` | three bytes into `DAT_011f0898` |
| 377 | `FUN_00a21000` @ `0x21000` | `SetFarColor(r,g,b)` | three bytes into `DAT_011f0ec8` — same shape, different target |
| 378 | `FUN_00a21020` @ `0x21020` | `SetGeomOffset(x,y)` | stores the pair into `EXT_011cb414`/`EXT_011cb410` |
| 379 | `FUN_00a21040` @ `0x21040` | `SetGeomScreen(h)` | stores `h`, then rebuilds the reciprocal table at `EXT_011d0898` for every divisor in `-0x8000 .. 0x7fff`, saturating to `0x7fff`; then calls `FUN_00a1dbc0(h)` |
| 457 | `FUN_00a25d10` @ `0x25d10` | `ratan2(y,x)` | `fpatan` then `__ftol` |

Ordinal 378's semantics were previously marked `TODO(REVERSE)` in `Blaze.cs`. The call site settles
it: `FUN_00506880` calls `Ordinal_379(400); Ordinal_378(0xa0, 0x78);` back to back, and
`(0xa0, 0x78)` is `(160, 120)` — the centre of a 320×240 screen. Projection distance then
projection centre, the standard libgte setup pair.

Ordinal 457's return value is **dropped by Ghidra in the DLL body** (`fpatan` leaves its result on
the x87 stack and `__ftol` converts it, but the decompiler typed the function `void`). `Blaze.c`
assigns it — `sVar3 = Ordinal_457((int)sVar3, (int)sVar6)` at `Blaze.c:55814` — so it does return
one. Declared `int` on the C# side.

## The emulated GTE register file is exported as DATA (ordinals 799–821) — **IDENTIFIED**

This is the third and largest PlayStation SDK family in `winsys3d.dll`, after libgpu
(658/661/665/674) and libgte's function half (376–379, 450–457). It is not a function family at
all: **the registers of the emulated GTE — the PlayStation's geometry coprocessor — are exported
as data**, and `Blaze.exe` does its geometry by writing and reading those addresses directly
rather than by calling anything.

That is why the decompilation of every 3D routine is littered with `Ordinal_814_exref`,
`Ordinal_802_exref` and friends instead of recognisable calls, and why those routines look
impenetrable until the register map is known.

Addresses from `winsys3d.dll`'s export table (image base `0x00a00000`; all of these land in
`.data`, so they are live state, not code):

| ordinal | RVA | address | role |
|---|---|---|---|
| 801 | `0x007f0e98` | `0x011f0e98` | `MAC1..MAC3` — the 32-bit accumulators |
| 802 | `0x007f0c80` | `0x011f0c80` | the input vector (`SVECTOR`) |
| 805 | `0x007f0ea8` | `0x011f0ea8` | `SXY0` — projected screen XY, vertex 0 |
| 806 | `0x007f0eac` | `0x011f0eac` | `SXY1` |
| 807 | `0x007f0eb0` | `0x011f0eb0` | `SXY2` |
| 808 | `0x007cb418` | `0x011cb418` | `MAC0` — where `NCLIP` leaves its cross product |
| 813 | `0x007d0848` | `0x011d0848` | `IR1..IR3` — the MACs saturated to 16 bits |
| 814 | `0x007d0870` | `0x011d0870` | the current `MATRIX`: `short m[3][3]` then `long t[3]` |
| 816 | `0x007d0828` | `0x011d0828` | the 8-dword block ordinal 368 writes |
| 819 | `0x007cb410` | `0x011cb410` | projection offset, set by `SetGeomOffset` |
| 820 | `0x007cb414` | `0x011cb414` | projection offset, second component |
| 821 | `0x007d0898` | `0x011d0898` | the reciprocal table `SetGeomScreen` rebuilds |

**Three independent cross-checks land exactly**, which is what makes this reading safe rather
than plausible:

- Ordinal 378's body (`SetGeomOffset`) writes `EXT_011cb414` and `EXT_011cb410` — ordinals 820
  and 819.
- Ordinal 379's body (`SetGeomScreen`) rebuilds the table at `EXT_011d0898` — ordinal 821.
- Ordinal 368's body copies eight dwords into `EXT_011d0828` — ordinal 816.

A fourth confirmation comes from the shape of the writes rather than from the DLL: `FUN_0051d1f0`
fills ordinal 814 with four dwords, one short at `+0x10`, then three dwords at `+0x14`/`+0x18`/
`+0x1c`. That is `short m[3][3]` (18 bytes, padded to 20) followed by `long t[3]` — the PlayStation
SDK's `MATRIX`, 32 bytes, field for field.

**Still unidentified**: 799, 800, 803, 804, 809, 810, 811, 812, 815, 817, 818. `FUN_0051d1f0`
touches 805–811, 815, 817 and 819–821, so reconstructing it will pin most of them down.

### The three consumers reconstructed so far

| Ghidra | SDK name | what it does |
|---|---|---|
| `FUN_0051c300` | `RotMatrix` | three angles → a 3×3 short matrix |
| `FUN_0051c280` | `NormalClip` | 2D cross product of SXY0/1/2 → MAC0; its sign is the facing test |
| `FUN_0051c2f0` | — | re-reads MAC0 without recomputing |

`RotMatrix` does not call `rsin`/`rcos` per angle. It indexes `DAT_00600920`, the 0x1000-entry
table the startup path builds once, each entry packing `rsin(i)` in the low half and `rcos(i)` in
the high half. Ghidra renders the indexing as `(angle << 2) & 0x3ffc` — a byte offset already
multiplied by four — which is `angle & 0xfff` on the entry index, written differently.

`NormalClip`'s six terms were checked one by one against the SDK formula
`SX0*SY1 + SX1*SY2 + SX2*SY0 - SX0*SY2 - SX1*SY0 - SX2*SY1`. Ghidra emits them in a different
order and folds three of the subtractions together, which hides the symmetry, but the expression
is identical.

### Reaching them from C#

These exports have no names, so `GetProcAddress` is called with the ordinal in the low word of the
name pointer — the `MAKEINTRESOURCE` convention. See `EnsureGteRegisters` in `Blaze.cs`.

## Ordinal 795 (`FUN_00a226c0`) takes **three** arguments — **CONFIRMED**, 2026-08-15

The depth-less sibling of ordinal 418, taken when a model's `0x400` attribute bit is set. It was
first declared with two parameters, which faulted natively (`0xC0000005`) the moment the
character-select carousel drew its first model: the missing third argument was read off the
caller's stack as a count and the DLL wrote through it.

```c
void FUN_00a226c0(ulonglong *param_1, int *param_2, int *param_3)   /* 0x226c0  795 */
```

| parameter | role |
|---|---|
| `param_1` | source vertices, three per unit |
| `param_2` | destination screen-XY array, three ints written per unit |
| `param_3` | **the count** |

Reading this off the pseudo-C needs one caution. The scalar path opens with a swap:

```c
iVar4 = (int)param_3;
if (MMXInstalled == 0) {
  if (param_3 != (int *)0x0) {
    param_3 = param_2;            /* <- from here on, param_3 is the OUTPUT */
    param_2 = (int *)iVar4;       /* <- and param_2 is the COUNT */
    do { ... param_3 = param_3 + 3; param_2 = (int *)((int)param_2 + -1);
    } while (param_2 != (int *)0x0);
```

Skip the swap and the function reads as though it took two arguments and ran an unbounded loop.
The MMX path settles it without any swap at all:

```c
for (iVar4 = (int)param_3 * 3; iVar4 != 0; iVar4 = iVar4 + -1) {
    ... param_2[local_8] = ... ; local_8 = local_8 + 1;
}
```

`param_3` used directly as a count, `param_2` directly as the output. The two paths agree, and the
`* 3` confirms that one unit of the count is three vertices — the same unit ordinal 418 uses, so
the call site passes the same expression:

```csharp
Ordinal_795(model + 0x40, mainMem + 0x1b0000, *(uint*)(model + 8) & 0xffff);
```

**The lesson generalises.** Ghidra's parameter recovery for this DLL is not trustworthy on its own:
ordinal 795 has a real argument it dropped, and `FUN_0052a1e0`/`FUN_0052a280` on the Blaze side had
the same problem. When a winsys3d call faults natively rather than misbehaving, an arity mismatch
is the first thing to check — cdecl means a missing argument is silently read from whatever the
caller left on the stack.
