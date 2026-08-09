# wsyskern.dll

Decompiled source: `wsyskern.c` (repo root, Ghidra output, **read-only**). Base
load address used throughout that file's `@ 0xXXXXXXXX` comments: `0x10000000`
(i.e. `wsyskern.c address = 0x10000000 + export RVA`).

A "utility kernel" DLL with a large *named* export table (758 exports in the
adjacent `playst.dll` are PSX-SDK-style audio/graphics; `wsyskern.dll`'s own
758-ish exports skew toward Win32-GDI-adjacent helpers: bitmap/resource
loading, printer DC helpers, sprite sizing, hit-testing ("Hot*" functions),
array/file helpers). Only one export has been analyzed so far.

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

## Not yet analyzed

Everything else — `wsyskern.dll` has ~758 named exports and only one has been
read so far. No other calls into this DLL have been reached yet in the
reconstructed call graph (see `REVERSE_STATUS.md`).
