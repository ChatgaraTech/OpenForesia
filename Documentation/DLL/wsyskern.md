# wsyskern.dll

Decompiled source: `Legacy/Decompiled/wsyskern.c` (Ghidra output, **read-only**). Base
load address used throughout that file's `@ 0xXXXXXXXX` comments: `0x10000000`
(i.e. `wsyskern.c address = 0x10000000 + export RVA`).

A "utility kernel" DLL with a large *named* export table (758 exports in the
adjacent `playst.dll` are PSX-SDK-style audio/graphics; `wsyskern.dll`'s own
758-ish exports skew toward Win32-GDI-adjacent helpers: bitmap/resource
loading, printer DC helpers, sprite sizing, hit-testing ("Hot*" functions),
array/file helpers). Three exports have been analyzed so far.

## Confirmed named exports used by Blaze.exe

### `LoadBitmapEx(HMODULE, LPCSTR, HPALETTE*)` (ordinal 114, RVA `0xbac0`, `wsyskern.c:9696`)

```c
HBITMAP LoadBitmapEx(HMODULE param_1, LPCSTR param_2, undefined4 *param_3)
{
  hResInfo = FindResourceA(param_1, param_2, (LPCSTR)0x2 /* RT_BITMAP */);
  if (hResInfo != NULL) {
    hResData = LoadResource(param_1, hResInfo);
    pbmi = LockResource(hResData);
    hdc = GetDC(NULL);
    plpal = (LOGPALETTE *)PaletteFromDIB(pbmi);
    *param_3 = CreatePalette(plpal);
    Free(plpal);
    if (*param_3 != NULL) { SelectPalette(hdc, *param_3, FALSE); RealizePalette(hdc); }
    local_28 = CreateDIBitmap(hdc, pbmi, CBM_INIT, /* DIB bits */, (BITMAPINFO*)pbmi, DIB_RGB_COLORS);
    ReleaseDC(NULL, hdc); FreeResource(hResData); // (FUN_1000bbac)
  }
  return local_28; // HBITMAP, NULL on failure
}
```

Loads an `RT_BITMAP` resource by ID/name from the given module, builds an
`HPALETTE` from the DIB's color table (via the DLL's own `PaletteFromDIB`
helper, not further analyzed), realizes it against the screen DC, then
creates and returns an `HBITMAP` via `CreateDIBitmap`.

**Call site** (`Blaze.c`, `RunApplication`/`FUN_00513900`):
`LoadBitmapEx(hInstance, MAKEINTRESOURCE(0x66), &hPalette)` — resource ID
`0x66` (102) is Blaze & Blade's splash-screen bitmap, loaded right after the
CD-check succeeds and immediately handed to `winsys3d.dll`'s
`Setup3DDevice(hBitmap, hPalette, ...)`.

`Blaze.cs` declares this with the confirmed signature
(`IntPtr resourceIdOrName` rather than a plain `int`, since it's genuinely an
`LPCSTR`/`MAKEINTRESOURCE` slot at the ABI level) rather than inferring it
from the call site alone.

### `LoadBitmapFile(LPCSTR, DWORD)` (ordinal 115, RVA `0x10500`, `wsyskern.c:13885`)

Loads a whole `.BMP` **file** (as opposed to `LoadBitmapEx`'s embedded
`RT_BITMAP` *resource*) into a wsyskern-heap buffer and returns a pointer to its
raw `BITMAPFILEHEADER`-prefixed image, or `NULL` on failure. No GDI object is
created — the caller blits it itself with `StretchDIBits`, reading the
`BITMAPINFO` at `+0xe` and the pixel bits at `+*(int*)(p+10)`.

**Confirmed** (whole body read):

- `CreateFileA(path, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL)`.
- On success: reads the first 4 bytes, rewinds, and tests the signature against
  `0x4d42` (`"BM"`). If it matches: `GetFileSize`, `Malloc(size)`, one full-file
  `ReadFile`, `CloseHandle`, return the buffer.
- `param_2` is a **failure-reporting mode**, not a flags word:

  | Value | Missing file | Bad/corrupt content |
  |------:|--------------|---------------------|
  | `0` | silent, returns `NULL` | silent, returns `NULL` |
  | `1` | `filenotfound(path)` — fatal message box | `fileerror(path)` |
  | `2` | `warnfilenotfound(path)`, returns `NULL` | `warnfileerror(path)` |

**Hypothesis / not yet read**: when the signature is *not* `"BM"`, the function
falls back to `FUN_10003260(hFile)` (`wsyskern.c:1254`) and accepts the result
only if it then starts with `0x4d42` — i.e. an unpacking/decompression path for
bitmaps stored in some container format. That helper's body has not been read;
the reconstruction does not depend on it, since a plain `.BMP` takes the
confirmed path.

**Call site** (`Blaze.c:89909`, `FUN_00542c40`, the attract-mode title screen):
`LoadBitmapFile("<cdRoot>DATA\TITLE640.BMP", 0)` — mode `0`, so a missing CD
must fail **silently**, and the caller treats that as a normal handled outcome
(it returns 1, which breaks the attract loop; see `REVERSE_STATUS.md`).

### `Malloc(uint)` / `Free(void*)` (ordinals 124 / 89, RVA `0xbbc0` / `0xbc30`, `wsyskern.c:9769` / `9818`)

The DLL's **own heap** pair, distinct from Blaze.exe's CRT `malloc`/`free`
chain (`FUN_00547220`/`FUN_00547210`). Ghidra resolved both by name from
Blaze.exe's import table, which is why they appear capitalized in `Blaze.c`.

- `Malloc` forwards to `FUN_10014270` and then **zero-fills** the block (dword
  loop plus a byte tail), calling `outofmemory(size)` on failure.
- `Free` wraps `FUN_100144f0` in an SEH frame. Passing `NULL` is safe, and
  `Blaze.c` itself calls it unconditionally on a possibly-null field.

**Why this matters**: any buffer returned by `LoadBitmapFile` lives on
*wsyskern's* heap and must be released with **this** `Free`. `Blaze.c` uses the
pair correctly — `FUN_00542f90` calls `Free(player+8)` on the title bitmap and
then the CRT `FUN_00547210(player)` on the struct itself. Mixing the two in the
C# reconstruction (e.g. releasing the bitmap through the `NativeMemory`-backed
CRT `Free`) would corrupt the heap, so `Blaze.cs` declares this one separately
as `WsysFree`.

Blaze.c calls the capitalized `Free` exactly twice (`Blaze.c:89940`, `90131`),
both on that title bitmap; it never calls the capitalized `Malloc` directly.

## Not yet analyzed

Everything else — `wsyskern.dll` has ~758 named exports and only the three above
have been read. Also unread: `PaletteFromDIB`, `FUN_10003260` (the non-`"BM"`
fallback noted above), and the `filenotfound`/`fileerror`/`warn*` reporters.
