# winsys3d.dll

Decompiled source: `Legacy/Decompiled/winsys3d.c` (Ghidra output, **read-only**). Base load
address used throughout that file's `@ 0xXXXXXXXX` comments: `0x00a00000`
(i.e. `winsys3d.c address = 0x00a00000 + export RVA`).

Blaze & Blade's own 3D-engine helper DLL. Exports 830 ordinals; most are unnamed
(no export name — Ghidra shows these as `Ordinal_NNN` in `Blaze.c`). A relatively
small number are named exports, generally higher-level entry points.

Export table was resolved with a small pure-Python PE32 export-directory parser
(session scratchpad `pe_exports.py`, not committed) run directly against
`winsys3d.dll` in `Legacy/Binaries/`, then cross-checked against the matching
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
of `winsys3d.dll` itself with `OpenForesia.Tools -- at Legacy/Binaries/winsys3d.dll <VA> <n>`, not inferred.

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

**Question ouverte — RÉSOLUE le 2026-08-24.** Le rappel que `Blaze.exe` enregistre par l'ordinal
790 (`Blaze.c` : `&LAB_00512ef0`) n'a pas de corps dans `Blaze.c` : Ghidra ne l'a vu que pris en
adresse. Il est désormais transcrit depuis le binaire — c'est **l'initialisation du son**, et elle
répond du même coup à deux lacunes que `REVERSE_STATUS.md` tenait pour bloquées :

* `DAT_005db8e4`, le périphérique DirectSound (#26) : `LAB_00512ef0` appelle `DirectSoundCreate`
  en `0x00512f20`, et c'est **l'unique** site d'appel de tout le binaire (balayage de `.text` à la
  recherche des `call rel32` et des `call [IAT]` vers le thunk `0x00545c68`) ;
* `DAT_005db85c`, la fenêtre principale (#6) : `Blaze.exe` ne la crée pas, **winsys3d la lui
  rend**. Le rappel la reçoit en paramètre et la range, puis lui pose le titre « Blaze & Blade »
  par `SetWindowTextA`.

Le rappel est `void __stdcall (HWND)`. `DAT_011be798` est appelé à deux endroits de
`FUN_00a06420` (l'ordinal 789), et les **deux** empilent `DAT_011be79c`, la fenêtre du DLL —
Ghidra a raté l'argument au premier des deux sites, le désassemblage montre `push ecx` avant
`call eax` en `0x00a06485` comme en `0x00a065f8`. Ce n'est donc pas un rappel de « device perdu »
au sens strict : il est déclenché à la création du périphérique 3D, et de nouveau à chaque
réinitialisation. Le nom `SetDeviceLostCallback` reste, faute d'un meilleur, mais son rôle réel
est « prévenir Blaze.exe que le périphérique et sa fenêtre existent ».

**Piège de notre côté** : avec le device synthétique de `Host/SyntheticDevice.cs`, `3dhelp` n'est
jamais initialisé et `DAT_011be79c` reste nul. Le rappel arrive alors avec une poignée nulle et
`SetCooperativeLevel` rend `E_INVALIDARG` (0x80070057). `Engine/Audio.cs` substitue la fenêtre
Direct3D 11 de l'hôte dans ce cas, au trampoline et nulle part ailleurs.

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
| `FUN_00a02870` | `return;` (`ret 4`) | 712, 720, 63, **762**, and many more unused ones |
| `FUN_00a02e20` | `return 0;` | 45, 104, 110, 681, 766, 768, 769, 770, and many more unused ones |
| `FUN_00a02e40` | `return;` | 767, and many more unused ones |
| `FUN_00a26470` | `return;` | 138, 771, 66, and many more unused ones |

These are declared as real P/Invokes in `Blaze.cs` and called for real (not specially
skipped) — "does nothing" is the DLL's actual, confirmed behavior at these ordinals, not
an assumption.

**Ordinal 762 added 2026-09-18.** Its export resolves to `0x00a02870` — the bare `c2 04 00`,
one stack argument. Every Blaze-side caller hands it the screen block `0x00566c48` after
writing `0xc0` into its head (`SetupMenuScreen`, `MusicFadeStep_0041AF50`,
`EntityHandler_00404E50`, and the labyrinth script verb `AreaVerb_004443D0`): **publishing
that block does nothing in this build**. The countdown on its `+0x10`/`+0x12` shorts still
matters, because Blaze itself reads them back. And since the stub leaves EAX alone, a caller
that returns straight after it (as `AreaVerb_004443D0` does) returns whatever EAX held before
the call.

#### A stub still has an arity, and getting it wrong leaks the stack (2026-08-21)

"It does nothing" is not the whole signature. These stubs are not all the same shape, and the
`ret` instruction says which:

| ordinal | body at | machine code | convention | arguments |
|---|---|---|---|---|
| 45 | `0x00a02e20` | `xor eax,eax ; ret 4` | **stdcall** | **exactly one** |
| 46 | `0x00a02490` | `xor eax,eax ; ret` | **cdecl** | none |

`Ordinal_45` was declared here with **two** parameters until 2026-08-21. Under stdcall the caller
pushed 8 bytes and the callee popped 4, leaving 4 bytes on the stack at every call. Corrected in
`Interop/Winsys3d.cs`, along with the one call site in `RunMainLoop` (`Platform/Startup.cs`).

`Ordinal_46` was not declared at all; added for `FUN_00513dd0`, which tests `Ordinal_46() < 0` to
decide whether the DLL allocator has anything worth preserving. It returns 0, so **that test is
always false in this build** and the interpreter always takes the save/restore path.

Read the `ret` before writing a `[DllImport]` for an ordinal-only export. `dotnet run --project
OpenForesia.Tools -- disasm Legacy/Binaries/winsys3d.dll 0x00a02e20 8` answers it in one line, and
the export table maps ordinal → RVA (`+ 0x00a00000`) to find the body.

#### L'autre moitié : lire le **site d'appel**, et ce que la convention décale (2026-08-24)

Le `ret` du corps donne la convention. Le **site d'appel** la donne aussi — l'absence d'un
`add esp,N` après le `call` — et c'est cette moitié-là qui compte quand on reconstruit la fonction
*appelante* plutôt que l'interop.

`Ordinal_442` (la racine carrée entière, `0x00545A10` côté `Blaze.exe`) est **stdcall** : aucun de
ses sites d'appel ne nettoie la pile.

```
004680cc  push esi
004680cd  call 00545A10h        ; pas de `add esp,4` : le callee a dépilé
004680d2  xor  ecx,ecx
```

Cela **décale tout ce qui suit**. Dans `FUN_00468080`, les trois arguments lus après l'appel le
sont à `[esp+14h]`, `[esp+18h]` et `[esp+1ch]` : en stdcall ce sont les arguments **2, 3 et 4** ; en
cdecl, l'argument poussé resterait sur la pile et les mêmes offsets désigneraient les arguments
**1, 2 et 3**. Un argument d'écart sur toute la famille.

C'est ce qui rend récupérable la signature des trois sœurs `FUN_00468080` / `FUN_00468110` /
`FUN_0041be60`, que Ghidra rate toutes les trois de la même façon (`param_1` employé à la fois
comme pointeur et comme scalaire). Vérifié au désassembleur le 2026-08-24 ; les trois
transcriptions étaient justes.

Côté C#, `[DllImport]` sans `CallingConvention` vaut `Winapi`, soit `StdCall` sur x86 : la
déclaration d'`Ordinal_442` dans `Interop/Winsys3d.cs` est donc correcte telle quelle.

### Confirmed real behavior

| Ordinal | RVA | `winsys3d.c` symbol | Behavior |
|---|---|---|---|
| 41 | `0x4fe0` | `FUN_00a04fe0` | `LeaveCriticalSection(&DAT_00b6d6d8)` — releases the DLL's own internal device lock. Its `EnterCriticalSection` partner is **ordinal 40**, not `FUN_00a04ff0` (that one is the one-time `InitializeCriticalSection`); `FUN_00504460` calls 40 then 41 around the eight registrations |
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
| 6 | `0x40d0` | `FUN_00a040d0` | Allocates and registers a "handler" node into a DLL-internal linked list; layout `[0]=id, [1]=state, [2]=mode, [3]=arg3, [4]=callback, [5]=prev, [6]=next`. Created with state `0x1000` (**registered but not armed**); returns the node as an opaque handle |
| 7 | `0x4130` | `FUN_00a04130` | The real **unregister**: unlinks the node and `Free`s it |
| 9 | `0x41d0` | `FUN_00a041d0` | Re-arms a node (state → `0x2000`) if it had fired (`0x4000`), or unconditionally for the two card channels (`id ∈ {0xf4000001, 0xf0000011}` with `mode == 4`) |
| 10 | `0x4220` | `FUN_00a04220` | **Arms** a node: state `0x1000` → `0x2000`. *Not* an unregister — see the correction below |
| 12 | `0x4270` | `FUN_00a04270` | The **dispatcher**. Walks the list for `id == p1 && mode == p2 && state == 0x2000`; on a match, either sets state `0x4000` + `SetEvent` (when `[3] == 0x2000`) or **calls the node's callback** `[4]` with no arguments. Ordinals 67/68/69 are thin wrappers over it |
| 20 | `0x45e0` | `FUN_00a045e0` | `DeleteFileA` on the resolved path descriptor. **Inverted return**: `1` when the delete FAILED, `0` when it succeeded (`NEG EAX; SBB EAX,EAX; INC EAX`) |
| 40 | `0x4fd0` | `FUN_00a04fd0` | `EnterCriticalSection(&DAT_00b6d6d8)` — the partner of ordinal 41. Zero arguments, `RET` |
| 67 / 69 | `0x50f0` | `FUN_00a050f0` | `FUN_00a04270(0xf4000001, 4)` then `return 1`. **`RET 4`** — one (ignored) argument |
| 68 | `0x50d0` | `FUN_00a050d0` | `FUN_00a04270(0xf0000011, 4)` then `return 1`. `RET 4` |
| 750 | `0x1e30` | `FUN_00a01e30` | `return DAT_011be760;` — the DLL's main device-interface pointer |

### Correction (2026-08-18): ordinal 10 arms, it does not unregister — and the 8 callbacks are real

Both halves of the earlier note above were wrong, and together they caused a `0xC0000005` in
`Blaze.cs` whenever a memory-card transfer was polled:

- **Ordinal 10 is an arm, not an unregister.** `FUN_00a04220` only flips state `0x1000` → `0x2000`,
  which is exactly the state the dispatcher `FUN_00a04270` requires before it will fire a node. So
  `FUN_00504460`'s eight `Ordinal_10` calls leave all eight handlers **live for the whole session**.
  The genuine unregister is ordinal 7 (`FUN_00a04130`), never called from the traced graph.
- **The 8 callback targets are readable after all.** They have no decompiled body in `Blaze.c`
  (address-taken-only), but they are nine-byte stubs in `Legacy/Binaries/Blaze.exe`, read directly on
  2026-08-18: each is one `MOV dword [<global>], <imm>` followed by `RET`.

  | stub | effect | | stub | effect |
  |---|---|---|---|---|
  | `0x00504610` | `DAT_00604ab0 = 0` | | `0x00504650` | `DAT_00604a00 = 0` |
  | `0x00504620` | `DAT_00604ab0 = 1` | | `0x00504660` | `DAT_00604a00 = 1` |
  | `0x00504630` | `DAT_00604ab0 = 2` | | `0x00504670` | `DAT_00604a00 = 2` |
  | `0x00504640` | `DAT_00604ab0 = 3` | | `0x00504680` | `DAT_00604a00 = 3` |

  `DAT_00604ab0` is the write-transfer status and `DAT_00604a00` the read-transfer status —
  precisely the two globals `FUN_005046c0`/`FUN_00504770` poll, and `0..3` precisely the values
  `FUN_00504820` decodes. This is the mechanism by which a transfer status ever leaves its idle
  value of 4.

Passing `null` for those callbacks (what `Blaze.cs` did until 2026-08-18) therefore had the DLL
call address 0 the first time `Ordinal_67` ran. Fixed by transcribing the eight stubs as
`[UnmanagedCallersOnly]` methods and passing their function pointers.

`FUN_00504460` also stores the eight handles in `DAT_00604b68/6c/70/74` (write) and
`DAT_00604b58/5c/60/64` (read), and `FUN_00504730`/`FUN_005047e0` pass **those very handles** back
to ordinal 9. They are not caller-allocated structures.

## Ordinals 6–25 are PSY-Q `libapi`, in the library's own order — **CONFIRMED**, 2026-08-18

This is the single most useful thing to know about the low-numbered ordinals: `winsys3d.dll` is a
Win32 reimplementation of the PlayStation BIOS API that the original PS1 build linked against, and
its ordinal numbering follows `libapi`'s own declaration order. Two contiguous runs:

| Ordinal | `libapi` | RVA | Win32 realisation |
|---|---|---|---|
| 6 | `OpenEvent(class, spec, mode, func)` | `0x40d0` | mallocs a node, links it, state `EvStWAIT` |
| 7 | `CloseEvent(ev)` | `0x4130` | unlinks and frees |
| 8 | `WaitEvent(ev)` | `0x4180` | `WaitForSingleObject` on the DLL's shared event |
| 9 | `TestEvent(ev)` | `0x41d0` | `EvStALREADY` → `EvStACTIVE`, returns 1 |
| 10 | `EnableEvent(ev)` | `0x4220` | `EvStWAIT` → `EvStACTIVE` |
| 11 | `DisableEvent(ev)` | `0x4250` | → `EvStWAIT` |
| 12 | `DeliverEvent(class, spec)` | `0x4270` | dispatch: set `EvStALREADY` + `SetEvent`, **or** call the node's `func` |
| 13 | `UndeliverEvent(class, spec)` | `0x42d0` | `ResetEvent` + `EvStALREADY` → `EvStACTIVE` |
| 14–16 | `OpenTh` / `CloseTh` / `ChangeTh` | `0x4320`+ | thread-control block over Win32 threads |
| 18 | `firstfile(name, dir)` | `0x44c0` | `FindFirstFileA` + fills the caller's directory entry |
| 19 | `nextfile(dir)` | `0x4560` | `FindNextFileA` |
| 20 | `erase(name)` | `0x45e0` | `DeleteFileA` (inverted return, see above) |
| 21 | `open(name, flags)` | `0x4600` | `CreateFileA` |
| 22 | `close(fd)` | `0x4650` | `CloseHandle` |
| 23 | `lseek(fd, off, whence)` | `0x4660` | `SetFilePointer` |
| 24 | `read(fd, buf, n)` | `0x4680` | `ReadFile` |
| 25 | `write(fd, buf, n)` | `0x46b0` | `WriteFile` |

The constants used at the call sites are `libapi`'s, one for one:

| Constant | Value | Where it shows up |
|---|---|---|
| `SwCARD` | `0xf4000001` | the software memory-card event class — the write channel |
| `HwCARD` | `0xf0000011` | the hardware memory-card event class — the read channel |
| `EvSpIOE` | `0x0004` | end of I/O — the mode ordinals 67/68/69 deliver |
| `EvSpERROR` | `0x8000` | error |
| `EvSpTIMOUT` | `0x0100` | timeout |
| `EvSpNEW` | `0x2000` | new device |
| `EvMdINTR` | `0x1000` | callback mode — the third argument of all eight `OpenEvent` calls |
| `EvStWAIT` / `EvStACTIVE` / `EvStALREADY` | `0x1000` / `0x2000` / `0x4000` | the node states ordinals 9/10/11/12 shuffle |

Practical consequences when reading `Blaze.c`:

- A "slot" argument like `DAT_00604980` is a PlayStation **`chan`**: `port * 16 + card`. That is why
  `FUN_0050a910` decomposes it as `(x & 3) + (x & 4) * 4` in some places and `(x & 4) << 2` (the port
  alone) in others.
- The `"Blaze-Saving*"` string is a `firstfile` glob, not a device name.
- The eight `&LAB_00504610..680` stubs are `OpenEvent` callbacks — ordinary event handlers, which is
  why they are three instructions long.
- The produced save file is a **genuine PS1 memory-card save**, title frame and all: `"SC"`, icon
  flag `0x11..0x13`, block count, Shift-JIS title at `+0x04`, 16-colour CLUT at `+0x60`, 16×16 4bpp
  icon frames from `+0x80`. `WriteCardSaveFile` composes exactly that, and `FUN_005126c0` parses it
  back.

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

## A primitive's RGB is **doubled and clamped** — `0x80` is full white, not grey — **CONFIRMED**, 2026-08-30

Every per-primitive handler in the DrawOTag dispatcher (`FUN_00a13400`) begins its colour work the
same way. `FUN_00a0f090`, the flat textured triangle (primitive code `0x24`), is representative:

```c
uVar1 = (uint)*(byte *)(param_1 + 4) * 2;   if (0xfe < uVar1) uVar1 = 0xff;   /* R */
uVar2 = (uint)*(byte *)(param_1 + 5) * 2;   if (0xfe < uVar2) uVar2 = 0xff;   /* G */
uVar3 = (uint)*(byte *)(param_1 + 6) * 2;   if (0xfe < uVar3) uVar3 = 0xff;   /* B */
_DAT_011cb148 = ((uVar1 + 0xff00) * 0x100 + uVar2) * 0x100 + uVar3;           /* 0xFFRRGGBB */
```

The same three-clamp block appears at `0x00a0e580`, `0x00a0f090`, and a dozen more sites — one per
primitive shape. The alpha byte is **always** `0xff`; transparency is a render state, never the
vertex colour.

**This is the PlayStation convention, kept.** On the PSX GPU a texture is modulated by the primitive
colour with `128` as the neutral multiplier, so `0x808080` means "the texel unchanged" and `0xffffff`
means "twice as bright, clamped". winsys3d reproduces it by doubling into a `0..255` D3D colour
before the vertex leaves the DLL.

**Why it matters for reading `Blaze.c`.** A tint of `0x808080` on an entity (`+0x38`) is **white**,
not 50 % grey, and a tint of `0xffffff` is a deliberate over-brighten. The distinction decides
whether an effect that renders dark is a bug or a faithful transcription. Two examples in the
reconstruction: `EntityHandler_00476770` gives the warrior's shockwave `0x808080` and subtracts
`0x080808` per frame — sixteen frames from **white** to black — while `WorldMap.cs` gives an ordinary
scene entity `0xffffff`.

**Consequence for the D3D11 backend.** The doubling happens inside the DLL, so anything that reaches
`DeviceShim` already carries a `0..255` colour and must **not** be doubled again. If a primitive ever
bypassed winsys3d and were built directly, it would have to double for itself.

## `FUN_00a03c80` — les **cinq** couples de mélange, dont trois additifs — **CONFIRMED**, 2026-08-30

`FUN_00a03c80(tpage)` traduit le mode de semi-transparence de la page de texture en un couple
`SRCBLEND`/`DESTBLEND` de Direct3D 3, posé par les slots `+0xA0` et `+0xA4` du périphérique
`3dhelp` :

| cas | `SRCBLEND` | `DESTBLEND` | effet |
|---|---|---|---|
| posé par `FUN_00a03db0` | `2` ONE | `1` ZERO | opaque |
| 0 et 2 | `5` SRCALPHA | `6` INVSRCALPHA | fondu ordinaire |
| 1 | `2` ONE | `2` ONE | **additif** |
| 3 | `5` SRCALPHA | `2` ONE | additif pondéré par l'alpha |
| 4 | `3` SRCCOLOR | `2` ONE | additif pondéré par la couleur |

Le mode vient de `FUN_00a03400((tpage >> 5) & 3)`, c'est-à-dire des deux bits `ABR` de la page —
la sémantique PlayStation, conservée.

**Deux garde-fous avant la traduction.** `DAT_00a2f454` porte les capacités du périphérique : sans
le bit `0x1000` (mélange alpha) mais avec `0x2000`, la DLL bascule sur le **tramage**
(`vtable[0x9C]`, `STIPPLEENABLE`) ; sans ni l'un ni l'autre, `FUN_00a03c80` rend faux et la
primitive est **abandonnée**. Un backend qui déclare le mélange alpha n'emprunte jamais ces deux
chemins.

**Ce que ça implique pour un backend de remplacement.** Porter le seul couple `(5, 6)` ne suffit
pas : les trois modes additifs sont ceux des effets lumineux — nuages de la carte du monde, onde de
choc, auréoles. Rendus opaques, ils peignent leur planche en aplat au lieu de s'ajouter au fond, et
une planche en dégradé y devient une tache sombre. C'est exactement le défaut relevé le 2026-08-30.

## La famille de fichiers (ordinaux 18–25, 29, 30) est **PORTÉE** — 2026-09-22

Les huit ordinaux `libapi` de la section précédente vivent maintenant dans
`OpenForesia/src/Libraries/winsys3d/CarteMemoire.cs`, avec leur pivot `FUN_00a03f80` et les
ordinaux 29 (`rename`) et 30 (`chdir`) que la composante lui soude. Ce qu'il a fallu établir, et
qui ne se relit pas dans `Blaze.c` :

### La convention `~` — le chiffrement de pauvre d'un nom de carte mémoire

Un nom de fichier PlayStation contient un `:` (`BASLUS-XXXXX:nom`), interdit sous Windows. Le
portage le remplace, et **les deux bouts sont dans deux corps différents** :

- à l'**écriture**, `FUN_00a03f80` copie le descripteur puis change en `~` le premier `:` trouvé
  **à partir de l'octet 2** — à partir de 2 pour ne pas manger le `C:` d'un chemin absolu ;
- à la **lecture**, `FUN_00a044c0` (18) et `FUN_00a04560` (19) regardent `cFileName[4]` puis
  `cFileName[5]` : si l'un vaut `~`, le nom utile commence juste après (indice 5 ou 6).

Les deux corps tronquent ensuite le nom à **19 caractères**, par un `'\0'` écrit d'office à
l'indice `0x13` — avant même d'en mesurer la longueur. La fiche que l'appelant reçoit fait au
moins 0x1c octets : le nom NUL-terminé à l'offset 0, la taille du fichier à l'offset 0x18.

### Les drapeaux de l'ordinal 21 ne sont pas ceux de Win32

`FUN_00a04600` calcule l'accès par `((drapeaux * 4) | (drapeaux & 2)) << 29` : le bit 0 du
paramètre devient `GENERIC_READ`, le bit 1 `GENERIC_WRITE`. La disposition est `OPEN_EXISTING`,
ou `OPEN_ALWAYS` si le bit `0x200` est posé. Partage, sécurité, attributs et modèle sont nuls.
C'est ce qui explique les valeurs vues aux sites d'appel : `1` = lecture, `2` = écriture,
`3` = les deux, `0x30200` = écriture + création.

### Deux ordinaux de plus dans la table des souches

`FUN_00a046e0` (`xor eax,eax ; ret 0Ch`) et `FUN_00a046f0` (`mov eax,1 ; ret 4`) sont une
**cinquième et une sixième souche vide**, au milieu du `libapi` et non avec les quatre recensées
le 2026-08-21. Neuf ordinaux aboutissent à la première (26, 33, 34, 97, 102, 105, 107, 126, 249),
deux à la seconde (28, 73). Le `ret 0Ch` confirme au passage les trois arguments avec lesquels
`Interop/Winsys3d.cs` déclarait l'ordinal 97.

### `winsys3d` importe cinq ordinaux de `wsyskern`, pas seulement des exports nommés

Sa table d'imports (section `WSYSKERN.dll`) nomme les ordinaux 1, 2, 3, 4, 5 en plus de
`ProjectionMatrix`, `Strdup`, `Free`, `Malloc`, `Gamma` et `ScaleMatrix`. Les ordinaux 4 et 5 sont
`_splitpath` et `_makepath` (`FUN_10013160` et `FUN_10012ef0`), que `FUN_00a03f80` emploie sur son
chemin `cdrom:`. **Aucun n'est importé par `Blaze.exe`**, donc `DLLAtteignables.md` ne les voyait
pas : ils sont transcrits dans `Libraries/wsyskern/Chemins.cs`.

### L'ordinal 29 est cassé dans le binaire vendu

`FUN_00a04700` résout ses deux descripteurs dans le même casier statique `DAT_00dedc58` et passe
donc deux fois le nom d'arrivée à `MoveFileA`. Voir **LB-035** dans
`Documentation/Reference/legacy-bug.md`.

## `OpenTIM` / `ReadTIM` (ordinaux 677 et 662) sont **PORTÉS** — 2026-09-22

`src/Libraries/winsys3d/Tim.cs`. Les deux corps ne lisent aucun fichier : un TIM est déjà en
mémoire quand ils le voient, et tout leur travail est de l'arithmétique de pointeurs.

`OpenTIM` retient `données + 4` dans `DAT_011be790` — il saute le mot d'identification sans le
lire — et **rend toujours 0** sans rien vérifier. `ReadTIM` remplit les cinq mots du `TIM_IMAGE`
puis remet `DAT_011be790` à zéro : **l'état est à un seul appel de profondeur**. Deux `ReadTIM`
de suite et le second sort par son premier test sans rien écrire, laissant la structure de
l'appelant avec les valeurs du TIM précédent — pas remise à zéro, laissée telle quelle.

La carte d'un TIM telle que ces deux corps la lisent, en mots de 32 bits depuis `p` :

| | |
|---|---|
| `p[0]` | le drapeau : mode de pixel sur 3 bits, bit 3 posé s'il y a une CLUT |
| `p[1]` | la longueur **en octets** du bloc qui suit, son propre mot compris |
| `p+2` | le rectangle du bloc — x, y, w, h, quatre shorts |
| `p+4` | les données du bloc |

Avec CLUT, le bloc d'image commence à `p + p[1]/4 + 1` ; sans, il n'y a qu'un bloc à `p + 1`. Le
corps calcule les deux cas avec la même expression en posant `saut = 1` dans le second, et le mode
forcé à 2 n'est pas un défaut prudent : c'est `TIM_16BIT`, un TIM sans palette étant 16 bits
directs.

## Il y a **huit** corps de souche, pas quatre — 2026-09-22

Le relevé du 2026-08-21 en comptait quatre pour 149 exports. Il en manquait quatre, et le total
est **223 exports sur 830** :

| adresse | forme | rend / arité | ordinaux | importé |
|---|---|---|---:|---|
| `0x00a02490` | `xor eax,eax ; ret` | 0, aucun argument | 22 | 46, 64, 116, 129 |
| `0x00a02870` | `ret 4` | rien, un argument | 41 | 63, 712, 720, 762 |
| `0x00a02e10` | `xor eax,eax ; ret 8` | 0, **deux** arguments | 22 | 765 |
| `0x00a02e20` | `xor eax,eax ; ret 4` | 0, un argument | 53 | dix ordinaux |
| `0x00a02e40` | `ret 8` | rien, **deux** arguments | 41 | 767 |
| `0x00a26470` | `ret` | rien, aucun argument | 33 | 66, 138, 771 |
| `0x00a046e0` | `xor eax,eax ; ret 0Ch` | 0, **trois** arguments | 9 | 97 |
| `0x00a046f0` | `mov eax,1 ; ret 4` | **1**, un argument | 2 | aucun |

Ghidra décompile les huit exactement pareil — `return 0;` ou `return;`, sans paramètre —, donc
**seul le `ret N` donne l'arité**. `0x00a02e10` et `0x00a02e40` sont les jumelles à deux arguments
de `0x00a02e20` et `0x00a02870`, à trente octets de distance ; `0x00a046e0` et `0x00a046f0` vivent
au milieu du `libapi`, juste après l'ordinal 25.

**Comment on les trouve toutes** : compter les corps, pas les ordinaux. `grep -c "0x2e10  *[0-9]"
winsys3d.c` donne les 22 ordinaux de la troisième ligne, et le même grep sur chaque adresse donne
la colonne. Le relevé d'origine avait cherché par l'ordinal importé, ce qui ne pouvait pas voir
une souche dont aucun ordinal importé ne dépendait encore.

## La `libcd` survit en une seule fonction — ordinal 92 = `CdSearchFile` — 2026-09-22

Le jeu de 1998 lisait ses données par le lecteur CD de la PlayStation. Le portage PC lit un
système de fichiers ordinaire, et la famille `libcd` a donc été vidée. Ce qu'il en reste :

| ordinal | corps | ce qu'il en reste |
|---:|---|---|
| 92 | `FUN_00a05160` | rend `&DAT_00dedd58` **en ignorant ses deux arguments** |
| 93 | `FUN_00a02e10` | souche : rend 0, deux arguments |
| 94 | `FUN_00a05170` | `CdComstr` : rend `"CdlNop"`, `"CdlSetloc"`, `"CdlPlay"`, `"CdlForward"`, `"CdlBackward"` — pas importé |
| 95, 104 | `FUN_00a02e20` | souche : rend 0, un argument |

**C'est l'ordinal 94 qui nomme la famille** : son `switch` rend les commandes de la `libcd`, mot
pour mot. Personne ne l'importe, mais il établit que 92–104 sont ce bloc-là.

Et l'ordinal 92 est `CdSearchFile`, par trois preuves concordantes :

1. **la taille du bloc rendu.** `DAT_00dedd58` fait 0x18 octets (`DAT_00dedd70` suit) — soit
   exactement une `CdlFILE` : `CdlLOC pos` (4), `u_long size` (4), `char name[16]` ;
2. **la signature.** `CdSearchFile(CdlFILE *fp, char *name)` rend un `CdlFILE*` — le corps prend
   bien deux arguments (`ret 8`) et rend bien un pointeur ;
3. **le site d'appel.** `Blaze.c:74553` : `Ordinal_92(&DAT_005e9040, PTR_s_BLAZE_ALL_1_00566bbc)`
   — un tampon de sortie et le nom d'un fichier du CD, `BLAZE_ALL.1`.

La `CdlFILE` rendue **n'est écrite par personne** : `DAT_00dedd58` a une seule référence dans les
27 000 lignes de `winsys3d.c`, celle qui en prend l'adresse. Ce sont 24 octets de zéros — position
nulle, taille nulle, nom vide — et le jeu ne s'en aperçoit pas : son unique site jette le retour.

## Ordinaux 794, 10 et 135/136 — **PORTÉS**, 2026-09-22

- **794** (`FUN_00a20540`, libgte `ScaleMatrix`) : multiplie chaque **colonne** de la matrice 3×3
  par une composante de l'échelle, en virgule fixe **4.12** (`sar 0Ch`, `ONE` = 4096). Opérandes
  sign-étendus (`movsx`) et décalage arithmétique : traiter la matrice en non signé la
  retournerait. C'est le **seul des neuf ordinaux libgte importés qui ne touche aucun registre
  émulé du GTE**, d'où son portage isolé. `Libraries/winsys3d/Matrices.cs`.
- **10** (`FUN_00a04220`, libapi `EnableEvent`) : arme un nœud, `EvStWAIT` → `EvStACTIVE`, et rend
  1 seulement s'il était bien en attente. Il ne parcourt pas la liste chaînée — c'est ce qui le
  détache des sept autres. Le nœud vient de l'ordinal 6, donc du tas de `wsyskern` : une seule
  mémoire, vue des deux côtés. `Libraries/winsys3d/Evenements.cs`.
- **135/136** (`FUN_00a05760`/`FUN_00a05770`) : un casier `DAT_011be6a4`, son lecteur et son
  échangeur, **et rien d'autre dans la DLL ne le touche** (trois références en tout). Le jeu
  appelle 136 une fois au démarrage avec 0 et jette la valeur rendue. Le rôle reste **non
  établi** — la forme (échange rendant l'ancienne valeur, appelé une fois avec 0) évoque le
  `SetGraphDebug` de la `libgpu`, rien ne le prouve. `Libraries/winsys3d/Cadence.cs`.

## La carte du chemin d'affichage : deux familles disjointes — 2026-09-22

Mesuré sur `winsys3d.c`, en classant les corps par la globale qu'ils touchent :

| famille | reconnue à | corps | lignes | squelettes de contrôle distincts |
|---|---|---:|---:|---:|
| les **producteurs** | ils écrivent `DAT_011be7dc` | 38 | 4 886 | **11** |
| les **appelants de device** | ils lisent `DAT_011be760` | 73 | 4 696 | 36 |
| *recouvrement* | | **0** | | |

**Zéro recouvrement.** Aucun des 38 producteurs n'appelle le device ; aucun des 73 appelants
n'écrit le tampon. C'est la structure du portage de 1998 : une moitié traduit les primitives
PlayStation en un flux de commandes, l'autre parle à Direct3D.

### Le tampon de commandes

`FUN_00a0c480` l'alloue — `Malloc(n * 0x94)` — et remet tout l'état d'un coup :

```
DAT_011be7ac = Malloc(n * 0x94)       la base
DAT_011be7dc = 2                      le curseur d'écriture (en dwords)
DAT_011be7a8 = DAT_011be7ac + n*0x14  la zone de sommets, dans le même bloc
DAT_00a2c480 = 8 ; DAT_00a2c484/488/48c = -1 ; DAT_00a2c578/57c = -1
```

Les commandes occupent les `n * 0x14` premiers dwords, les sommets le reste. Chaque producteur
avance `DAT_011be7a8` de `0x18` ou `0x20` **floats** — soit 3 ou 4 `TLVERTEX` de 32 octets : un
triangle ou un quad.

Les mots de commande repérés : `0xfffffffb`, `0xfffffffd`, `0xfffffffe` en tête de bloc, `0xffffffff`
en fin, et des opcodes positifs (`1`, `4`, `6`) pour les primitives.

### Le cache d'état, et pourquoi il interdit de couper

`DAT_00a2c480/484/488/48c` et `DAT_00a2c578/57c` mémorisent **la dernière commande d'état émise**.
Un producteur n'écrit une commande d'état que si le cache dit qu'elle a changé :

```c
if (DAT_00a2c488 != -2) {                       /* la page de texture liee */
    if ((DAT_00a2c578 != -5) || (DAT_00a2c57c != 0)) { ...émettre... }
    DAT_00a2c488 = -2;
}
```

Porter la moitié des producteurs donnerait **deux caches** : la moitié portée croirait l'état déjà
posé parce que la moitié native l'a posé, et sauterait la commande. La primitive sortirait avec le
mélange de la précédente, par intermittence. C'est le pire cas de la règle « ne pas couper une
représentation en deux ».

### `DAT_011be7ac` est un symbole réutilisé — attention aux comptes

La même adresse sert de pointeur de travail à des corps sans rapport avec l'affichage :
`winsys3d.c:8718` s'en sert comme nœud de liste chaînée puis le `Free`. Ghidra confond sous un seul
nom ce que le compilateur a réutilisé. **Les comptes de soudure de `DLLGlobales.md` sont donc
surévalués pour cette globale**, et probablement pour d'autres : avant de bâtir un lot sur un
chiffre de ce tableau, lire les sites.

### `DAT_011be760` n'est pas une soudure, et n'est pas un levier

Le pointeur de device est touché par 73 corps et écrit par un seul (`FUN_00a06420`, ordinal 789),
ce qui en faisait le candidat évident pour une coupure. **Mesure faite : non.** En relançant
`census-dll-globals.awk` avec cette globale retirée de la carte, la masse passe de 425 corps à
419 — le seul corps qui se détache est l'**ordinal 750** (`FUN_00a01e30`), qui est littéralement
`return DAT_011be760;`.

Et ce n'en a jamais été une, de soudure : la valeur est **une poignée vers un objet que la
reconstruction possède déjà**. `Host/SyntheticDevice.cs` détourne `GetProcAddress` dans la table
d'imports de `winsys3d`, donc le `DAT_011be760` de la DLL native pointe sur un objet C# ; et
`Screens/State00/Options.cs` appelle sa vtable directement depuis `src/Blaze/` depuis le
2026-08-12. Un corps porté n'a pas besoin d'une copie : il relit la globale par l'ordinal 750, ce
que l'original fait aussi — il ne la met jamais en cache.

**Corollaire : ne pas porter l'ordinal 750.** Notre copie serait vide, l'écrivain (789) étant
natif. Le `[DllImport]` de 750 *est* la fenêtre sur la globale native ; le remplacer par une
lecture du `.data` de la DLL chargée serait le même travail, déguisé en portage.

## Le consommateur du tampon, et la chaîne complète — 2026-09-22

**`FUN_00a0cd20`, 46 lignes.** C'est le seul corps de la DLL qui lise un paquet de commandes, et
il se trouve en remontant les appelants depuis `DAT_011be7ac` : les deux seuls corps qui touchent
la base du tampon sans jamais écrire le curseur sont `FUN_00a0dde0` (le constructeur) et
`FUN_00a0cd20` (le lecteur).

```
  CONSTRUCTION, pendant l'image
    FUN_00a0dde0 (ordinal 752)
      -> FUN_00a0c480(n)                Malloc(n * 0x94), curseur à 2, mémos à -1
      -> switch (genre de primitive) -> l'un des ~31 producteurs
      -> *noeud = paquet | 0x80000000   lie le paquet dans l'OT du jeu

  AFFICHAGE, à DrawOTag
    FUN_00a061b0 (701)  device[0x60|0x5c](0,0) ; device[0xb0]()      BeginScene
      parcours de l'OT (liens 24 bits, sentinelle 0x00ffffff)
        FUN_00a13400 -> FUN_00a0cdd0 -> DAT_011be7ac = *noeud -> FUN_00a0cd20()
      device[0xb4]()                                                  EndScene
```

`DrawOTag` fait **deux** parcours de l'OT : le premier appelle `FUN_00a26b20` sur chaque nœud
(la passe de textures, précédée de `FUN_00a26490` et suivie de `FUN_00a027a0`/`FUN_00a26f20`), le
second rejoue les paquets.

### Le format du paquet

En-tête de 8 octets (`[0]` lien OT, `[1]` longueur), puis des enregistrements de **0x10 octets**
depuis l'offset 8 — d'où le `DAT_011be7dc = 2` de l'initialisation. La boucle s'arrête quand le mot
d'opcode **suivant** vaut `0xffffffff` ; chaque producteur pose cette sentinelle sans avancer le
curseur, et le producteur d'après l'écrase.

| opcode | action du consommateur |
|---|---|
| `0xfffffffb` | `device[0x64](0)` — délie la texture |
| `0xfffffffc` | `FUN_00a27880(arg0, arg2, 0, 0)` |
| `0xfffffffd` | rien |
| `0xfffffffe` | `device[0x8c](arg0)` |
| **≥ 0** | `device[0xbc](opcode, arg0, arg1, arg2, 0)` — `DrawPrimitive` |

L'enregistrement **est** un `DrawPrimitive` sérialisé :
`[type de primitive][type de sommet][pointeur de sommets][nombre]`. Le slot `+0xbc` est celui que
`3dhelp.md` documente et qu'`Options.cs` appelle déjà directement depuis C#.

### Le mémo d'état sert à GROUPER, pas seulement à dédoublonner

Quand l'état n'a pas changé, un producteur n'omet pas sa commande : **il allonge la précédente**.

```c
DAT_011be7ac[DAT_011be7dc + -1] = DAT_011be7ac[DAT_011be7dc + -1] + uVar8;   /* nombre += n */
```

Le mot « nombre » du dernier enregistrement est incrémenté sur place et les sommets se rangent à la
suite dans la zone partagée : deux primitives consécutives de même état deviennent **un seul
`DrawPrimitive`**. C'est ce qui rend les 38 producteurs indivisibles — porter la moitié ferait
allonger par l'un un enregistrement écrit par l'autre.

### CORRECTION — `DAT_011be7ac` n'est pas un symbole réutilisé

Une section antérieure de ce document l'affirmait, sur la foi de `winsys3d.c:8718` qui s'en sert
comme nœud de liste puis le `Free`. **C'est faux** : ce corps est `FUN_00a0c440` (ordinal 754), qui
**libère un paquet** — il lit son en-tête (`[0]` le lien, `[1]` la longueur) pour recoudre l'OT
avant de rendre la mémoire. La globale a un sens unique et cohérent : *le paquet courant*, qu'on le
remplisse, qu'on le rejoue ou qu'on le libère.

### Le devis, mesuré

| lot | corps | lignes | remarque |
|---|---:|---:|---|
| `FUN_00a0cd20` seul | 1 | 46 | ne touche ni le curseur ni les mémos — mais non exporté |
| le rejeu (`FUN_00a13400` et dessous) | 97 | 7 022 | lit un format connu, appelle notre device |
| `DrawOTag` entier (701) | 134 | 9 091 | rejeu + passe de textures |
| les producteurs | 38 | 4 886 | 11 formes ; à faire **après** le rejeu |

**L'ordre : le rejeu d'abord.** C'est le seul lot qui franchisse une frontière déjà matérielle —
le paquet en mémoire, format ci-dessus — au lieu d'une frontière inventée. Tant que le rejeu est
natif, un paquet construit en C# devrait être identique au bit près ; l'inverse ne coûte rien, un
paquet natif se relisant aussi bien depuis C#.

## Les paquets de modèles sont des TMD PlayStation — ordinaux 755, 678, 563 **PORTÉS**, 2026-09-23

**CONFIRMÉ.** Le second chemin qui remplit le tampon de commandes, `FUN_00a0c4f0` (755), convertit
une **entrée de la table d'objets d'un TMD** — le format de modèle de la libgs. Les preuves :
`FUN_00a06a40` (678) refuse un fichier dont le premier mot n'est pas `0x41`, l'identifiant TMD ;
l'entrée qu'il lit fait sept mots (`vert_top`, `n_vert`, `normal_top`, `n_normal`,
`primitive_top`, `n_primitive`, `scale`) ; `FUN_00a1cff0` (563) change les décalages en adresses
une seule fois, selon le mot de drapeaux ; et les dix-huit codes du `switch` de `c4f0`
(`mode & 0xfd`) sont les modes de primitive TMD, avec leurs tailles exactes.

| mode | primitive | producteur | sommets |
|---|---|---|---|
| `0x20` / `0x21` | F3 éclairé / sans lumière | `078c0` / `07d80` | `D3DVERTEX` / `D3DLVERTEX` |
| `0x24` / `0x25` | FT3 | `08850` / `08c80` | idem |
| `0x28` / `0x29` | F4 | `08080` / `08510` | idem |
| `0x2c` / `0x2d` | FT4 | `094d0` / `09070` | idem |
| `0x30` / `0x31` | G3 | `09980` / `09e60` | idem |
| `0x34` / `0x35` | GT3 | `0a1a0` / `0a670` | idem |
| `0x38` / `0x39` | G4 | `0aa90` / `0b000` | idem |
| `0x3c` / `0x3d` | GT4 | `0b390` / `0b930` | idem |
| `0x40` / `0x50` | LF2 / LG2 | `0bdf0` / `0c110` | `D3DVERTEX`, sans normale |

Le bit 1 du mode (la semi-transparence TMD) est **ignoré** : masqué avant l'aiguillage.

L'**état de rendu** `DAT_00a2c480` se lit alors en bits : bit 0 Gouraud, bit 1 texturé, bit 2
sans lumière, 8 = paquet neuf. C'est ce que `c4f0` compare pour regrouper.

Les huit producteurs sans lumière sont aussi ceux que les adaptateurs de l'ordinal 752 appellent :
un paquet construit par 752 et un TMD converti par 755 sortent donc des mêmes corps.

**Deux pièges de Ghidra sur ces producteurs** (voir `ConstructionPaquets.cs`) : la couleur, qu'il
écrit `(float)(rgb)`, est rangée en entier ; et les `u v`, qu'il écrit `0.0`, sont une division
entière par ~140 millions — nulles en effet (**LB-042**). Et un défaut du binaire : **LB-041**.
