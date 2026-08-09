// OpenForesia — reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth — never modify it, see Prompt.md / Strategy.md).
//
// This file follows the call graph starting at the PE entry point `entry` and works
// outward. Progress, naming decisions and open questions are tracked in
// src/Blaze/REVERSE_STATUS.md — read that file before continuing this work.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // Win32 / native interop
    //
    // Ghidra recovered several game-engine helper DLLs sitting next to Blaze.exe
    // (wsyskern.dll, winsys3d.dll, playst.dll, 3dhelp.dll, gldhelp.dll). Their export
    // tables were inspected directly (see scratchpad/pe_exports.py) to resolve names
    // for the few ordinal-only imports Blaze.c calls out of `entry`'s subtree.
    // =====================================================================

    [StructLayout(LayoutKind.Sequential)]
    private struct STARTUPINFOA
    {
        public uint cb;
        public byte* lpReserved;
        public byte* lpDesktop;
        public byte* lpTitle;
        public uint dwX;
        public uint dwY;
        public uint dwXSize;
        public uint dwYSize;
        public uint dwXCountChars;
        public uint dwYCountChars;
        public uint dwFillAttribute;
        public uint dwFlags;
        public ushort wShowWindow;
        public ushort cbReserved2;
        public byte* lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    private const uint STARTF_USESHOWWINDOW = 1;
    private const int SW_SHOWDEFAULT = 10;

    [DllImport("kernel32.dll")] private static extern uint GetVersion();
    [DllImport("kernel32.dll")] private static extern byte* GetCommandLineA();
    [DllImport("kernel32.dll")] private static extern void GetStartupInfoA(STARTUPINFOA* lpStartupInfo);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern IntPtr GetModuleHandleA(byte* lpModuleName);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern uint GetModuleFileNameA(IntPtr hModule, byte* lpFilename, uint nSize);

    [DllImport("kernel32.dll")] private static extern IntPtr HeapCreate(uint flOptions, nuint dwInitialSize, nuint dwMaximumSize);

    [DllImport("kernel32.dll")] private static extern IntPtr GetCurrentProcess();
    [DllImport("kernel32.dll")] private static extern int TerminateProcess(IntPtr hProcess, uint uExitCode);
    [DllImport("kernel32.dll")] private static extern void ExitProcess(uint uExitCode);

    [DllImport("kernel32.dll")] private static extern char* GetEnvironmentStringsW();
    [DllImport("kernel32.dll")] private static extern int FreeEnvironmentStringsW(char* lpszEnvironmentBlock);
    [DllImport("kernel32.dll")] private static extern int WideCharToMultiByte(uint CodePage, uint dwFlags, char* lpWideCharStr, int cchWideChar, byte* lpMultiByteStr, int cbMultiByte, byte* lpDefaultChar, int* lpUsedDefaultChar);

    [DllImport("kernel32.dll")] private static extern int IsDBCSLeadByte(byte TestChar);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern uint GetDriveTypeA(byte* lpRootPathName);
    [DllImport("kernel32.dll")] private static extern uint GetFileSize(IntPtr hFile, uint* lpFileSizeHigh);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr CreateFileA(byte* lpFileName, uint dwDesiredAccess, uint dwShareMode, void* lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
    [DllImport("kernel32.dll")] private static extern int CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern uint GetPrivateProfileIntA(byte* lpAppName, byte* lpKeyName, int nDefault, byte* lpFileName);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern int WritePrivateProfileStringA(byte* lpAppName, byte* lpKeyName, byte* lpString, byte* lpFileName);

    [DllImport("ole32.dll")] private static extern int CoInitialize(void* pvReserved);
    [DllImport("ole32.dll")] private static extern void CoUninitialize();

    [DllImport("user32.dll", CharSet = CharSet.Ansi)] private static extern int MessageBoxA(IntPtr hWnd, byte* lpText, byte* lpCaption, uint uType);
    [DllImport("user32.dll")] private static extern int DestroyWindow(IntPtr hWnd);
    [DllImport("gdi32.dll")] private static extern int DeleteObject(IntPtr hObject);

    [DllImport("winmm.dll")] private static extern uint waveOutGetNumDevs();
    [DllImport("winmm.dll")] private static extern int waveOutGetVolume(IntPtr hwo, uint* pdwVolume);
    [DllImport("winmm.dll")] private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

    private const uint DRIVE_CDROM = 5;

    // wsyskern.dll: named "utility kernel" export table (bitmap/resource/hot-region helpers).
    // Ghidra recovered its name directly from the import table. Signature confirmed against
    // wsyskern.c (decompiled DLL source, ordinal 114): loads an RT_BITMAP resource, builds and
    // realizes an HPALETTE from its DIB header, and returns the created HBITMAP.
    // See Documentation/DLL/wsyskern.md.
    [DllImport("wsyskern.dll")]
    private static extern IntPtr LoadBitmapEx(IntPtr hInstance, IntPtr resourceIdOrName, out IntPtr phPalette);

    // wsyskern.dll ordinal 115 (named export, wsyskern.c:13885 @ 0x10010500). Reads a whole .BMP
    // file into a wsyskern-heap buffer and returns a pointer to its raw BITMAPFILEHEADER-prefixed
    // image, or null on failure. `mode` selects the failure reporting: 0 = silent, 1 = fatal
    // `filenotfound`/`fileerror` message box, 2 = `warnfilenotfound`/`warnfileerror` warning.
    // The only Blaze.c call site (0x00542c40, the attract title screen) passes 0, i.e. a missing
    // file must fail silently and is a normal, handled outcome.
    //
    // IMPORTANT: the returned buffer comes from wsyskern's own heap (its `Malloc`, ordinal 124,
    // wsyskern.c:9769), so it must be released with wsyskern's `Free` below — NOT with this file's
    // CRT-derived Free, which maps to NativeMemory and would corrupt the heap.
    [DllImport("wsyskern.dll")]
    private static extern byte* LoadBitmapFile(byte* fileName, uint mode);

    // wsyskern.dll ordinal 89 (named export, wsyskern.c:9818 @ 0x1000bc30). The matching deallocator
    // for anything wsyskern's `Malloc` produced. Blaze.c calls it exactly twice, both on the attract
    // screen's title bitmap (Blaze.c:89940 and 90131) — note it is the capitalized `Free`/`Malloc`
    // pair Ghidra resolved from the import table, a different heap from the lowercase CRT
    // malloc/free chain (FUN_00547220/FUN_00547210) this file maps onto NativeMemory.
    // Passing null is safe: it forwards to a CRT free, and Blaze.c itself calls it unconditionally.
    [DllImport("wsyskern.dll", EntryPoint = "Free")]
    private static extern void WsysFree(byte* block);

    // winsys3d.dll ordinal 823 (named export). Role now fully confirmed by reading its body and
    // its callee chain in winsys3d.c (FUN_00a010d0 -> dialog proc FUN_00a012f0): despite the name,
    // this is the game's **renderer-selection dialog / launcher**, not a device creation call.
    // It enumerates DirectDraw drivers (DirectDrawEnumerateA), probes for a 3Dfx card by
    // LoadLibrary("GLIDE2X.DLL") + GetProcAddress("_grSstQueryBoards@4"), then shows modal dialog
    // resource 0x66 — the same resource id whose RT_BITMAP LoadBitmapEx loads, i.e. hBitmap /
    // hPalette are that dialog's splash image — and writes the chosen driver back to the ini.
    // See Documentation/DLL/winsys3d.md for the full analysis and struct layout.
    //
    // `titleStrings` is a pointer to an array of 5 ANSI string pointers, consumed by the dialog
    // proc's WM_INITDIALOG handler (winsys3d.c:218-223):
    //   [0] dialog window title   [1] static 0x3e9   [2] static 0x3ea
    //   [3] OK button (id 1)      [4] Cancel button (id 2)
    // The DLL null-checks the array pointer itself but then dereferences all five entries
    // unconditionally, so a non-null array must have five valid strings.
    [DllImport("winsys3d.dll")]
    private static extern int Setup3DDevice(IntPtr hBitmap, IntPtr hPalette, byte** titleStrings);

    // winsys3d.dll ordinal 756, confirmed against winsys3d.c (decompiled DLL source): builds
    // gamma-correction / RGB translation tables from a float multiplier (default 1.0f = 0x3f800000).
    [DllImport("winsys3d.dll")]
    private static extern void CreateTranslationTables(float gamma);

    // The following winsys3d.dll entry points are exported by ordinal only (no name in the DLL's
    // own export table). Their RVAs were resolved against winsys3d.dll's export directory and
    // cross-checked directly in winsys3d.c (decompiled DLL source, see
    // Documentation/DLL/winsys3d.md for the full analysis):
    //   - ordinal 337 (winsys3d.c: FUN_00a1df00) builds the engine's trig lookup tables
    //     (fills DAT_011cb420[0x1000] via fsin()) — no arguments, run once.
    //   - ordinals 450/451 (winsys3d.c: FUN_00a25c20/FUN_00a25c40) are simple reads from that
    //     same 0x1000-entry ring buffer, 450 phase-shifted by 0x400 (a quarter period) relative
    //     to 451 — i.e. a classic packed cos()/sin() lookup table pair.
    //   - ordinal 790 (winsys3d.c: FUN_00a06410) just stores the pointer into a DLL-private
    //     global (DAT_011be798) that winsys3d.dll later calls indirectly as a callback
    //     (confirmed via cross-reference in winsys3d.c); likely a device-lost/reset notification
    //     hook. TODO(REVERSE): the callback passed at the RunApplication call site
    //     (Ghidra: &LAB_00512ef0) has no decompiled function body in Blaze.c (Ghidra only saw it
    //     addressed-of, never as a call target), so it cannot be reconstructed yet.
    [DllImport("winsys3d.dll", EntryPoint = "#337")] private static extern void InitTrigTables();
    [DllImport("winsys3d.dll", EntryPoint = "#450")] private static extern int GetCosTableEntry(int index);
    [DllImport("winsys3d.dll", EntryPoint = "#451")] private static extern uint GetSinTableEntry(int index);
    [DllImport("winsys3d.dll", EntryPoint = "#790")] private static extern void SetDeviceLostCallback(void* callback);

    // The following winsys3d.dll ordinals are used directly by RunMainLoop (Ghidra: FUN_00529c10)
    // and its immediate dependency graph. Signatures/behavior were confirmed against winsys3d.c —
    // see Documentation/DLL/winsys3d.md for the full per-ordinal writeup. Several are literal
    // no-op stubs in the shipped DLL (`return 0;` / `return;` with no body) — they are still
    // declared and called for real (rather than special-cased away) so the call graph stays
    // traceable to Blaze.c 1:1, and because "does nothing" is itself the DLL's real, confirmed
    // behavior here, not an assumption.
    [DllImport("winsys3d.dll", EntryPoint = "#41")] private static extern void Ordinal_41(); // real: LeaveCriticalSection on the DLL's internal device lock
    [DllImport("winsys3d.dll", EntryPoint = "#45")] private static extern int Ordinal_45(void* a, byte* b); // confirmed no-op (winsys3d.c: FUN_00a02e20)
    [DllImport("winsys3d.dll", EntryPoint = "#92")] private static extern void* Ordinal_92(void* a, void* b); // confirmed no-op (FUN_00a05160 ignores its args)
    [DllImport("winsys3d.dll", EntryPoint = "#104")] private static extern uint Ordinal_104(void* a); // confirmed no-op, returns 0 (FUN_00a02e20)
    [DllImport("winsys3d.dll", EntryPoint = "#110")] private static extern void Ordinal_110(int a); // confirmed no-op (FUN_00a02e20)
    [DllImport("winsys3d.dll", EntryPoint = "#116")] private static extern uint Ordinal_116(); // confirmed no-op, returns 0 (FUN_00a02490)
    [DllImport("winsys3d.dll", EntryPoint = "#129")] private static extern void Ordinal_129(); // confirmed no-op (FUN_00a02490)
    [DllImport("winsys3d.dll", EntryPoint = "#132")] private static extern int Ordinal_132(int a); // real: winsys3d.c FUN_00a053e0, VSync/frame-pacing wait — DLL-internal timing state
    [DllImport("winsys3d.dll", EntryPoint = "#136")] private static extern int Ordinal_136(int a); // real: winsys3d.c FUN_00a05770, get/set DAT_011be6a4 ("frame time offset"?), returns previous value
    [DllImport("winsys3d.dll", EntryPoint = "#138")] private static extern void Ordinal_138(); // confirmed no-op (FUN_00a26470)
    [DllImport("winsys3d.dll", EntryPoint = "#368")] private static extern void Ordinal_368(void* eightDwordStruct); // real: winsys3d.c FUN_00a20ed0, copies 8 dwords into DLL-internal "shading defaults" state (EXT_011d0828); exact field semantics TODO(REVERSE)
    [DllImport("winsys3d.dll", EntryPoint = "#378")] private static extern void Ordinal_378(int a, int b); // real: winsys3d.c FUN_00a21020, stores (a,b) into DLL-internal state (EXT_011cb414/EXT_011cb410); exact semantics TODO(REVERSE)
    // Ordinals 658/661/665/674 are winsys3d.dll's port of the PlayStation SDK's **libgpu** — which
    // is what the sibling `playst.dll` ("PlayStation") is named after, and unsurprising for a PSX
    // game ported to Windows. The two below are identified beyond doubt by their bodies: they write
    // exactly the documented DISPENV and DRAWENV field layouts, and the four global structs Blaze.c
    // passes them are spaced 20 / 92 bytes apart, matching sizeof(DISPENV) / sizeof(DRAWENV) exactly.
    // Both take (env, x, y, w, h) — a *screen rectangle*, not four opaque shorts.

    // winsys3d.c FUN_00a05ac0 — libgpu `SetDefDispEnv(DISPENV*, x, y, w, h)`. Writes disp={x,y,w,h},
    // screen={0,0,0x100,0xf0}, isinter=0, isrgb24=0 (18 of the struct's 20 bytes).
    [DllImport("winsys3d.dll", EntryPoint = "#658")] private static extern void SetDefDispEnv(void* dispEnv, short x, short y, short w, short h);

    // winsys3d.c FUN_00a05b50 — libgpu `SetDefDrawEnv(DRAWENV*, x, y, w, h)`. Writes clip={x,y,w,h},
    // ofs={x,y}, tw={0,0,0,0}, tpage=10, dtd=1, dfe=1, isbg=0, r0=g0=b0=0 (first 28 bytes of 92).
    [DllImport("winsys3d.dll", EntryPoint = "#661")] private static extern void SetDefDrawEnv(void* drawEnv, short x, short y, short w, short h);

    // real: winsys3d.c FUN_00a05e90 — libgpu `ClearImage(RECT*, r, g, b)`-shaped (takes a 4-short
    // RECT and floods VRAM), but its colour math decompiles as
    // `((p4 & 0xff) << (p3 + 10)) << (p2 + 5)`, which does not match ClearImage's plain (r,g,b).
    // TODO(REVERSE): left un-renamed until that is explained rather than assumed.
    [DllImport("winsys3d.dll", EntryPoint = "#665")] private static extern int Ordinal_665(void* rect, byte r, byte g, uint b);
    [DllImport("winsys3d.dll", EntryPoint = "#667")] private static extern void Ordinal_667(int a); // confirmed no-op (FUN_00a02e20)
    [DllImport("winsys3d.dll", EntryPoint = "#668")] private static extern int Ordinal_668(int a, int b, int c, int d, int e, int f); // real: winsys3d.c FUN_00a020a0, registers a debug/console text channel slot (unused feature in the shipped build)
    // real: winsys3d.c FUN_00a05d50 — libgpu `LoadImage(RECT*, u_long* data)`: copies `data` row by
    // row into the VRAM rectangle. `rect` is a 4-short RECT {x,y,w,h}, and the body early-outs on
    // rect.y > 0x1ff, confirming a 1024x512 VRAM addressing scheme.
    [DllImport("winsys3d.dll", EntryPoint = "#674")] private static extern int Ordinal_674(void* rect, void* srcBuffer);
    [DllImport("winsys3d.dll", EntryPoint = "#681")] private static extern void Ordinal_681(int a); // confirmed no-op (FUN_00a02e20)
    [DllImport("winsys3d.dll", EntryPoint = "#710")] private static extern void Ordinal_710(int width, int height); // real: winsys3d.c FUN_00a026e0, (re)creates the DLL's text/console overlay bitmap at the given size
    [DllImport("winsys3d.dll", EntryPoint = "#712")] private static extern void Ordinal_712(int a); // confirmed no-op (FUN_00a02870)
    [DllImport("winsys3d.dll", EntryPoint = "#720")] private static extern void Ordinal_720(nint a); // confirmed no-op (FUN_00a02870)
    [DllImport("winsys3d.dll", EntryPoint = "#771")] private static extern void Ordinal_771(); // confirmed no-op (FUN_00a26470)
    [DllImport("winsys3d.dll", EntryPoint = "#789")] private static extern int Ordinal_789(int a, int b); // real: winsys3d.c FUN_00a06420, device (re)init/teardown dispatcher; inert when a==0 besides storing b
    [DllImport("winsys3d.dll", EntryPoint = "SetShift")] private static extern void SetShift(int x, int y, int zoomLevel, int flag); // real, named export (ordinal 796): camera/view shift+zoom, winsys3d.c confirmed

    // winsys3d.dll ordinals used by FUN_00504460 (joystick/interrupt-style event-handler
    // (de)registration bookkeeping — see winsys3d.c FUN_00a040d0/FUN_00a04220 and neighbors).
    [DllImport("winsys3d.dll", EntryPoint = "#6")] private static extern void* Ordinal_6(uint messageId, uint p2, uint p3, void* callback); // real: winsys3d.c FUN_00a040d0, registers a handler, returns an opaque handle
    [DllImport("winsys3d.dll", EntryPoint = "#10")] private static extern int Ordinal_10(void* handle); // real: winsys3d.c FUN_00a04220, unregisters a handler returned by ordinal 6
    [DllImport("winsys3d.dll", EntryPoint = "#40")] private static extern void Ordinal_40(); // real: winsys3d.c FUN_00a04fd0 — TODO(REVERSE): body not read, opaque
    [DllImport("winsys3d.dll", EntryPoint = "#63")] private static extern void Ordinal_63(int a); // confirmed no-op (FUN_00a02870)
    [DllImport("winsys3d.dll", EntryPoint = "#64")] private static extern int Ordinal_64(); // confirmed no-op, returns 0 (FUN_00a02490)
    [DllImport("winsys3d.dll", EntryPoint = "#66")] private static extern void Ordinal_66(); // confirmed no-op (FUN_00a26470)
    [DllImport("winsys3d.dll", EntryPoint = "#750")] private static extern void* Ordinal_750(); // real: winsys3d.c FUN_00a01e30, returns the DLL's main device-interface pointer (DAT_011be760)

    // Win32 APIs needed by RunMainLoop's own dependency graph (movie/intro-video playback window,
    // resource-file font loading, and the joystick/critical-section plumbing in FUN_00504460).
    [DllImport("user32.dll", CharSet = CharSet.Ansi)] private static extern ushort RegisterClassA(WNDCLASSA* lpWndClass);
    [DllImport("user32.dll")] private static extern int UnregisterClassA(byte* lpClassName, IntPtr hInstance);
    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern IntPtr CreateWindowExA(uint dwExStyle, byte* lpClassName, byte* lpWindowName, uint dwStyle,
        int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, void* lpParam);
    [DllImport("user32.dll")] private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")] private static extern int MoveWindow(IntPtr hWnd, int x, int y, int w, int h, int bRepaint);
    [DllImport("user32.dll", CharSet = CharSet.Ansi)] private static extern IntPtr LoadCursorA(IntPtr hInstance, IntPtr lpCursorName);
    [DllImport("gdi32.dll")] private static extern IntPtr GetStockObject(int fnObject);
    [StructLayout(LayoutKind.Sequential)]
    private struct WNDCLASSA
    {
        public uint style;
        public IntPtr lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public byte* lpszMenuName;
        public byte* lpszClassName;
    }

    [DllImport("user32.dll")] private static extern int PeekMessageA(MSG* lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);
    [DllImport("user32.dll")] private static extern int DispatchMessageA(MSG* lpMsg);
    [DllImport("user32.dll")] private static extern IntPtr SetTimer(IntPtr hWnd, nuint nIDEvent, uint uElapse, IntPtr lpTimerFunc);
    [DllImport("user32.dll")] private static extern int KillTimer(IntPtr hWnd, nuint uIDEvent);
    [DllImport("user32.dll")] private static extern int PostMessageA(IntPtr hWnd, uint msg, nuint wParam, nint lParam);
    [DllImport("kernel32.dll")] private static extern void RaiseException(uint dwExceptionCode, uint dwExceptionFlags, uint nNumberOfArguments, nint* lpArguments);
    [DllImport("winmm.dll")] private static extern uint joySetCapture(IntPtr hWnd, uint uJoyID, uint uPeriod, int fChanged);
    [DllImport("winmm.dll")] private static extern uint joyReleaseCapture(uint uJoyID);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern byte* lstrcpyA(byte* lpString1, byte* lpString2);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern int lstrcmpiA(byte* lpString1, byte* lpString2);
    [DllImport("gdi32.dll")] private static extern int StretchDIBits(IntPtr hdc, int xDest, int yDest, int destW, int destH,
        int xSrc, int ySrc, int srcW, int srcH, void* lpBits, void* lpBitsInfo, uint iUsage, uint rop);
    [DllImport("user32.dll")] private static extern int SetRect(int* lprc, int left, int top, int right, int bottom);
    [DllImport("ddraw.dll")] private static extern int DirectDrawCreate(void* lpGuid, out IntPtr lplpDD, void* pUnkOuter);

    [StructLayout(LayoutKind.Sequential)]
    private struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public nuint wParam;
        public nint lParam;
        public uint time;
        public int ptX;
        public int ptY;
    }

    [DllImport("kernel32.dll")] private static extern uint SetFilePointer(IntPtr hFile, int lDistanceToMove, void* lpDistanceToMoveHigh, uint dwMoveMethod);
    [DllImport("kernel32.dll")] private static extern int ReadFile(IntPtr hFile, void* lpBuffer, uint nNumberOfBytesToRead, uint* lpNumberOfBytesRead, void* lpOverlapped);

    // Used by the music-streaming subsystem (Ghidra: FUN_005131d0 / FUN_005135d0).
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern IntPtr CreateEventA(void* lpEventAttributes, int bManualReset, int bInitialState, byte* lpName);
    [DllImport("kernel32.dll")] private static extern int SetEvent(IntPtr hEvent);
    [DllImport("kernel32.dll")] private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
    [DllImport("kernel32.dll")] private static extern IntPtr CreateThread(void* lpThreadAttributes, nuint dwStackSize, IntPtr lpStartAddress, void* lpParameter, uint dwCreationFlags, uint* lpThreadId);

    // Ghidra: MainMem_exref — Blaze.exe imports this pointer as DATA from winsys3d.dll (mirrors
    // TexFilterPossible_exref below: winsys3d.c's `entry` sets `MainMem = &DAT_00b6dbd8;`, its own
    // ~1MB scratch arena). Resolved lazily via GetProcAddress, same pattern as GetTexFilterPossiblePtr.
    private static byte** s_MainMemExref;
    private static byte* GetMainMemPtr()
    {
        if (s_MainMemExref == null)
        {
            IntPtr module = LoadLibraryA(ToAnsiZ("winsys3d.dll"));
            if (module != IntPtr.Zero)
            {
                s_MainMemExref = (byte**)GetProcAddress(module, ToAnsiZ("MainMem"));
            }
        }
        return s_MainMemExref != null ? *s_MainMemExref : null;
    }

    // =====================================================================
    // CRT support (MSVC 6 / "Visual Studio 1998" runtime startup, confirmed by several
    // functions Ghidra matched to known library symbols, e.g. __amsg_exit, __exit).
    //
    // None of this is Blaze-specific game logic — it is the C runtime's process bring-up
    // (heap init, small-block allocator, argv/environ construction, atexit/static-destructor
    // plumbing). Per Prompt.md/Strategy.md guidance, standard CRT internals are represented
    // with a C# abstraction (the .NET native allocator, structured argv/environ parsing)
    // rather than transcribing 1998 x86 heap bookkeeping. Traceability to the original Ghidra
    // symbols is kept in comments throughout.
    // =====================================================================

    // Ghidra: _DAT_005e000c/_DAT_005e0008/_DAT_005e0004/_DAT_005e0000, set from GetVersion() in `entry`.
    private static byte s_WinMinor;
    private static byte s_WinMajor;
    private static ushort s_WinVersion;
    private static ushort s_Platform;

    // Ghidra: DAT_00607944 (CRT private heap handle, from __heap_init @ FUN_00548af0)
    private static IntPtr s_CrtHeap;

    // Ghidra: DAT_00607948 (raw GetCommandLineA() pointer) / DAT_005e0040 (ANSI environment block)
    private static byte* s_RawCommandLine;
    private static byte* s_EnvironmentBlock;

    // Ghidra: _DAT_005e001c (environ, char**) / _DAT_005e0014 (argv, char**) / _DAT_005e0010 (argc - 1)
    private static byte** s_Environ;
    private static byte** s_Argv;
    private static int s_ArgvCount;

    // Ghidra: DAT_005e003c ("already terminating" guard used by FUN_005477b0/doexit)
    private static bool s_CrtTerminated;

    /// <summary>
    /// Ghidra: FUN_00548af0 @ 0x00548af0 (CRT `__heap_init`).
    /// The original also built the VC6 "small block heap" free-list/bucket allocator on top of
    /// a VirtualAlloc'd arena (FUN_00548b30 @ 0x00548b30) as a fast path in front of HeapAlloc.
    /// TODO(REVERSE): that bucket allocator is pure CRT-internal bookkeeping with no
    /// Blaze-specific behavior; Malloc/Free below use the .NET native allocator instead, so it
    /// is intentionally not transcribed.
    /// </summary>
    private static bool HeapInit()
    {
        s_CrtHeap = HeapCreate(1 /* HEAP_NO_SERIALIZE */, 0x1000, 0);
        return s_CrtHeap != IntPtr.Zero;
    }

    /// <summary>
    /// Ghidra: FUN_00547690/FUN_005476b0/FUN_00547700/FUN_00548ad0 (CRT `malloc` + the
    /// small-block-heap fast path + the C++ new-handler retry loop), collapsed into one
    /// allocator backed by the .NET native heap. Returns null on failure, matching the
    /// original malloc() contract that every call site in Blaze.c checks against.
    /// </summary>
    private static void* Malloc(nuint size)
    {
        if (size == 0) size = 1;
        try { return NativeMemory.Alloc(size); }
        catch (OutOfMemoryException) { return null; }
    }

    /// <summary>Ghidra: FUN_00547f10/FUN_00548dd0/FUN_00548e30 (CRT `free` + SBH bookkeeping).</summary>
    private static void Free(void* block)
    {
        if (block != null) NativeMemory.Free(block);
    }

    /// <summary>
    /// Ghidra: __amsg_exit @ 0x00547ee0. Prints a CRT runtime-error message and terminates
    /// with exit code 0xff. The original formats "R60xx" style messages via a private buffer
    /// (FUN_0054aa10/FUN_0054aa50 @ DAT_005e0048 check); reproduced here as a plain stderr
    /// message since the on-disk message table isn't present in Blaze.c.
    /// </summary>
    private static void AmsgExit(int errorCode)
    {
        Console.Error.WriteLine($"runtime error R60{errorCode:D2}");
        Exit(0xff);
    }

    /// <summary>Ghidra: FUN_00547770 @ 0x00547770 (CRT `exit`).</summary>
    private static void Exit(uint exitCode) => DoExit(exitCode, skipAtExit: false, returnInsteadOfTerminate: false);

    /// <summary>
    /// Ghidra: FUN_005477b0 @ 0x005477b0 (CRT `doexit`). Runs registered atexit() callbacks and
    /// C++ static-destructor tables, then calls ExitProcess (unless returnInsteadOfTerminate).
    /// </summary>
    private static void DoExit(uint exitCode, bool skipAtExit, bool returnInsteadOfTerminate)
    {
        if (s_CrtTerminated)
        {
            TerminateProcess(GetCurrentProcess(), exitCode);
        }

        if (!skipAtExit)
        {
            // TODO(REVERSE): original walks the atexit() registration table
            // (DAT_00607950..DAT_0060794c). No atexit() registration call sites have been
            // reconstructed yet, so there is nothing to run here.
            RunStaticDestructors();
        }
        RunStaticDestructors();

        if (!returnInsteadOfTerminate)
        {
            s_CrtTerminated = true;
            ExitProcess(exitCode);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00547860 @ 0x00547860 (CRT `_initterm`) as invoked by `doexit` over the C++
    /// static-destructor function-pointer table (DAT_0054f024..DAT_0054f038).
    /// TODO(REVERSE): the destructor table's contents are linker-generated data not present in
    /// Blaze.c; no global C++ objects have been identified/reconstructed yet to run here.
    /// </summary>
    private static void RunStaticDestructors()
    {
    }

    /// <summary>
    /// Ghidra: FUN_00547740 @ 0x00547740, called once from `entry` before argv parsing.
    /// Runs the C then C++ static-initializer tables (via `_initterm` @ FUN_00547860) plus an
    /// optional floating-point-related callback (DAT_00607954).
    /// TODO(REVERSE): same as RunStaticDestructors — the initializer table contents are
    /// linker-generated data outside Blaze.c; revisit once specific global objects are found.
    /// </summary>
    private static void RunStaticInitializers()
    {
    }

    /// <summary>
    /// Ghidra: FUN_0054a810 @ 0x0054a810 (CRT `_ioinit`), builds the 32-entry low-level file
    /// descriptor table (stdin/stdout/stderr + inherited handles from STARTUPINFO).
    /// TODO(REVERSE): superseded by Console/System.IO; no reconstructed code currently needs
    /// the raw CRT file-descriptor table, so this is a documented no-op.
    /// </summary>
    private static void IoInit()
    {
    }

    /// <summary>
    /// Ghidra: FUN_0054a800 @ 0x0054a800 (calls FUN_0054a530(-3), CRT `__setmbcp`), which built
    /// the process-wide DBCS lead-byte lookup table used by FUN_00549fd0/FUN_0054a1c0.
    /// Superseded here by direct calls to Win32 IsDBCSLeadByte at each use site, so there is no
    /// table to build.
    /// </summary>
    private static void SetupMultibyteCodePage()
    {
    }

    /// <summary>Ghidra: FUN_00549fd0 -> FUN_00549ff0 @ 0x00549fd0/0x00549ff0 (`_ismbblead`-style check).</summary>
    private static bool IsLeadByte(byte b) => IsDBCSLeadByte(b) != 0;

    /// <summary>
    /// Ghidra: FUN_0054a3d0 @ 0x0054a3d0 (CRT `_setenvp` block fetch). Retrieves the process
    /// environment block and converts it to ANSI, matching the original's Unicode-first /
    /// ANSI-fallback strategy.
    /// </summary>
    private static byte* GetEnvironmentBlockAnsi()
    {
        char* wide = GetEnvironmentStringsW();
        if (wide == null) return null;

        char* end = wide;
        while (*end != '\0')
        {
            while (*end != '\0') end++;
            end++;
        }
        int charCountWithTerminator = (int)(end - wide) + 1;

        int bytesNeeded = WideCharToMultiByte(0, 0, wide, charCountWithTerminator, null, 0, null, null);
        if (bytesNeeded == 0)
        {
            FreeEnvironmentStringsW(wide);
            return null;
        }

        byte* ansi = (byte*)Malloc((nuint)bytesNeeded);
        if (ansi == null)
        {
            FreeEnvironmentStringsW(wide);
            return null;
        }

        int written = WideCharToMultiByte(0, 0, wide, charCountWithTerminator, ansi, bytesNeeded, null, null);
        FreeEnvironmentStringsW(wide);
        if (written == 0)
        {
            Free(ansi);
            return null;
        }
        return ansi;
    }

    /// <summary>
    /// Ghidra: FUN_0054a030 @ 0x0054a030 (CRT `_setenvp`). Splits the flat "NAME=VALUE\0...\0\0"
    /// environment block obtained above into the environ[] pointer array.
    /// </summary>
    private static void BuildEnviron()
    {
        s_EnvironmentBlock = GetEnvironmentBlockAnsi();

        int count = 0;
        if (s_EnvironmentBlock != null)
        {
            byte* p = s_EnvironmentBlock;
            while (*p != 0)
            {
                if (*p != (byte)'=') count++;
                while (*p != 0) p++;
                p++;
            }
        }

        byte** environ = (byte**)Malloc((nuint)((count + 1) * sizeof(byte*)));
        s_Environ = environ;
        if (environ == null)
        {
            AmsgExit(9);
            return;
        }

        if (s_EnvironmentBlock == null)
        {
            environ[0] = null;
            return;
        }

        byte* cursor = s_EnvironmentBlock;
        int slot = 0;
        while (*cursor != 0)
        {
            byte* entryStart = cursor;
            while (*cursor != 0) cursor++;
            int len = (int)(cursor - entryStart) + 1;
            cursor++;

            if (*entryStart == (byte)'=')
            {
                // CRT skips drive-letter pseudo-variables like "=C:=C:\path" here.
                continue;
            }

            byte* copy = (byte*)Malloc((nuint)len);
            if (copy == null) { AmsgExit(9); return; }
            new ReadOnlySpan<byte>(entryStart, len).CopyTo(new Span<byte>(copy, len));
            environ[slot++] = copy;
        }
        environ[slot] = null;
    }

    /// <summary>
    /// Ghidra: FUN_0054a1c0 @ 0x0054a1c0 (CRT `parse_cmdline`). Splits a Win32 command line into
    /// whitespace-delimited, quote-aware tokens, matching the classic MSVC argv-parsing rules:
    ///  - the first token (program name) is parsed with simpler quote handling than the rest;
    ///  - `\"` sequences inside later tokens: a run of N backslashes followed by a quote emits
    ///    N/2 backslashes, and an odd run additionally escapes the quote as a literal `"`.
    /// `argv`/`argStorage` may be null on the counting pass (argCount/charCount only).
    /// </summary>
    private static void ParseCmdLine(byte* cmdLine, byte** argv, byte* argStorage, out int argCount, out int charCount)
    {
        int nArgs = 1;
        int nChars = 0;
        byte* dst = argStorage;
        byte* src = cmdLine;

        void Emit(byte b)
        {
            nChars++;
            if (dst != null) *dst++ = b;
        }

        if (argv != null) *argv++ = argStorage;

        if (*src == (byte)'"')
        {
            byte* p = src + 1;
            while (*p != (byte)'"' && *p != 0)
            {
                if (IsLeadByte(*p) && p[1] != 0)
                {
                    Emit(*p); p++;
                }
                Emit(*p); p++;
            }
            Emit(0);
            src = *p == (byte)'"' ? p + 1 : p;
        }
        else
        {
            while (*src > (byte)' ')
            {
                if (IsLeadByte(*src) && src[1] != 0)
                {
                    Emit(*src); src++;
                }
                Emit(*src);
                src++;
            }
            if (dst != null) dst--;
            nChars--;
        }

        while (*src == ' ' || *src == '\t') src++;

        bool inQuotes = false;
        while (*src != 0)
        {
            if (argv != null) *argv++ = argStorage + nChars;
            nArgs++;

            while (true)
            {
                int backslashes = 0;
                while (*src == '\\') { backslashes++; src++; }

                if (*src == (byte)'"')
                {
                    if ((backslashes & 1) == 0)
                    {
                        if (inQuotes && src[1] == (byte)'"') src++;
                        else inQuotes = !inQuotes;
                    }
                    backslashes /= 2;
                }

                for (; backslashes != 0; backslashes--) Emit((byte)'\\');

                if (*src == 0 || (!inQuotes && (*src == ' ' || *src == '\t'))) break;

                if (IsLeadByte(*src) && src[1] != 0)
                {
                    Emit(*src); src++;
                }
                Emit(*src);
                src++;
            }

            Emit(0);
            while (*src == ' ' || *src == '\t') src++;
        }

        if (argv != null) *argv = null;
        nArgs++;

        argCount = nArgs;
        charCount = nChars;
    }

    /// <summary>Ghidra: FUN_0054a120 @ 0x0054a120 (CRT `_setargv`).</summary>
    private static void SetArgv()
    {
        byte* moduleFileName = stackalloc byte[0x104];
        GetModuleFileNameA(IntPtr.Zero, moduleFileName, 0x104);

        byte* cmdLineForArgv = (s_RawCommandLine != null && *s_RawCommandLine != 0)
            ? s_RawCommandLine
            : moduleFileName;

        ParseCmdLine(cmdLineForArgv, null, null, out int argCount, out int charCount);

        byte* block = (byte*)Malloc((nuint)(charCount + argCount * sizeof(byte*)));
        if (block == null) { AmsgExit(8); return; }

        byte** argv = (byte**)block;
        byte* storage = block + argCount * sizeof(byte*);
        ParseCmdLine(cmdLineForArgv, argv, storage, out argCount, out charCount);

        s_Argv = argv;
        s_ArgvCount = argCount - 1;
    }

    // =====================================================================
    // Process entry point
    // =====================================================================

    /// <summary>
    /// Ghidra: entry @ 0x00547d40 — the raw PE entry point, i.e. MSVC's `WinMainCRTStartup`.
    /// Performs C-runtime bring-up (heap, argv/environ, static initializers), extracts the
    /// WinMain-style command line, then hands off to <see cref="RunApplication"/>
    /// (Ghidra: FUN_00513900), which is where Blaze & Blade's own code begins.
    /// </summary>
    public static void entry()
    {
        uint version = GetVersion();
        s_WinMinor = (byte)((version >> 8) & 0xff);
        s_WinMajor = (byte)(version & 0xff);
        s_WinVersion = (ushort)(s_WinMajor * 0x100 + s_WinMinor);
        s_Platform = (ushort)(version >> 0x10);

        if (!HeapInit())
        {
            AmsgExit(0x1c);
            return;
        }

        IoInit();
        SetupMultibyteCodePage();

        s_RawCommandLine = GetCommandLineA();
        s_EnvironmentBlock = GetEnvironmentBlockAnsi();
        if (s_EnvironmentBlock == null || s_RawCommandLine == null)
        {
            Exit(0xffffffff);
            return;
        }

        SetArgv();
        BuildEnviron();
        RunStaticInitializers();

        // Skip the program-name token to find the WinMain-style lpCmdLine, honoring quoting
        // and DBCS lead bytes exactly like the CRT startup code does.
        byte* p = s_RawCommandLine;
        if (*p == (byte)'"')
        {
            byte* prev;
            byte b;
            do
            {
                prev = p;
                p = prev + 1;
                b = *p;
                if (b == (byte)'"' || b == 0) break;
                if (IsLeadByte(b)) p = prev + 2;
            } while (true);
            if (*p == (byte)'"') p = prev + 2;
        }
        else
        {
            while (*p > (byte)' ') p++;
        }
        while (*p != 0 && *p < (byte)'!') p++;

        var startupInfo = new STARTUPINFOA { dwFlags = 0 };
        GetStartupInfoA(&startupInfo);
        int nCmdShow = (startupInfo.dwFlags & STARTF_USESHOWWINDOW) == 0
            ? SW_SHOWDEFAULT
            : startupInfo.wShowWindow;

        IntPtr hInstance = GetModuleHandleA(null);
        uint exitCode = RunApplication(hInstance, IntPtr.Zero, p, (uint)nCmdShow);
        Exit(exitCode);
    }

    // =====================================================================
    // RunApplication — Blaze & Blade's own entry point (this is where "our" game code starts;
    // everything above is C-runtime/CRT startup plumbing).
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00513900 @ 0x00513900 — the WinMain-equivalent passed to `entry`. Ghidra only
    /// recovered the first parameter (hInstance); the other three are reconstructed from the
    /// call site in `entry` (hInstance, hPrevInstance, lpCmdLine, nCmdShow), matching the
    /// standard WinMain signature.
    ///
    /// Behavior: loops over drive letters C..Z looking for the Blaze &amp; Blade CD-ROM (special-
    /// casing drives X and Z, then P as a fallback), shows a "please insert the CD" prompt if
    /// none is found, then on success loads a splash bitmap resource, sets up the 3D device,
    /// initializes COM and the audio volume/table setup, allocates the two big scratch buffers
    /// used by the rest of the engine, and runs the main application loop before tearing
    /// everything back down.
    /// </summary>
    private static uint RunApplication(IntPtr hInstance, IntPtr hPrevInstance, byte* lpCmdLine, uint nCmdShow)
    {
        // Ghidra: s____Blaze_Blade_005631e8 @ 0x005631e8 — a 4-byte "X:\\\0" drive-root buffer
        // whose first byte is swapped for each candidate drive letter.
        byte* driveRoot = stackalloc byte[4];
        driveRoot[1] = (byte)':';
        driveRoot[2] = (byte)'\\';
        driveRoot[3] = 0;

        // TODO(REVERSE): DAT_00563280.._0056328c (the CD-check filename template copied into
        // local_2c/local_28/local_24/local_20 in the original) are 16 bytes of initialized data
        // not present in Blaze.c's text — only Ghidra's derived symbol names survive. The drive
        // letter substitution below is faithful; the rest of the template is a placeholder.
        byte* cdCheckPath = stackalloc byte[16];

        bool foundDriveX = false;
        bool foundDriveZ = false;
        char foundLetter = '@';

        while (true)
        {
            foundLetter = '@';
            for (char letter = 'C'; letter < '['; letter++)
            {
                driveRoot[0] = (byte)letter;

                // Check if drive is type DRIVE_CDROM and keep letter found
                if (GetDriveTypeA(driveRoot) == DRIVE_CDROM)
                {
                    foundLetter = letter;
                    break;
                }
            }

            if (foundLetter == '@' && foundDriveX && foundDriveZ)
            {
                driveRoot[0] = (byte)'P';
                IntPtr handle = OpenExistingFileReadOnly(driveRoot);
                if ((int)handle < 0)
                {
                    foundLetter = '@';
                }
                else
                {
                    CloseHandle(handle);
                    foundLetter = 'P';
                }
            }

            if (foundLetter != '@')
            {
                // Ghidra: DAT_005631f8 = s____Blaze_Blade_005631e8[0]. A single-byte store, and
                // that is genuinely all it is: the rest of "@:\FRENCH\" is static data the linker
                // wrote into the image. Patch index 0 only — do not rebuild the string from parts,
                // or the language directory is lost again.
                s_CdDataRootPath[0] = (byte)foundLetter;

                cdCheckPath[0] = (byte)foundLetter;
                IntPtr hFile = OpenExistingFileReadOnly(cdCheckPath);
                uint fileSize = GetFileSize(hFile, null);

                IntPtr hBitmap = LoadBitmapEx(hInstance, (IntPtr)0x66 /* MAKEINTRESOURCE */, out IntPtr hPalette);
                Setup3DDevice(hBitmap, hPalette, GetRendererDialogStrings());
                DeleteObject(hBitmap);
                DeleteObject(hPalette);

                CoInitialize(null);
                SaveAndMaxWaveOutVolume();
                InitTrigTables();

                // Ghidra: builds a 0x1000-entry lookup table combining the sin table (ordinal 451)
                // low 16 bits with the cos table (ordinal 450, phase-shifted by a quarter period)
                // in the high word — see Documentation/DLL/winsys3d.md. Packed sin/cos table,
                // consumed by the not-yet-reconstructed rendering code.
                for (int i = 0; i < 0x1000; i++)
                {
                    uint low = GetSinTableEntry(i) & 0xffff;
                    int high = GetCosTableEntry(i);
                    s_LookupTable00600920[i] = low + (uint)(high << 16);
                }

                s_BaseSoundBank = Malloc(790000);
                s_LevelSoundBank = Malloc(260000);

                // TODO(REVERSE): the callback (Ghidra: &LAB_00512ef0) has no decompiled body in
                // Blaze.c (see Documentation/DLL/winsys3d.md for what SetDeviceLostCallback does
                // with it inside winsys3d.dll).
                SetDeviceLostCallback(null);

                LoadOptionsIni(); // Ghidra: FUN_0050f140 @ 0x0050f140

                try
                {
                    RunMainLoop(); // Ghidra: FUN_00529c10 @ 0x00529c10
                }
                finally
                {
                    ShutdownStage1(); // Ghidra: FUN_00504600 @ 0x00504600
                    SaveOptionsIni(); // Ghidra: thunk_FUN_0050f300 @ 0x0050f520 -> FUN_0050f300
                    ShutdownStage3(); // Ghidra: FUN_00512ff0 @ 0x00512ff0
                    // Ghidra: DestroyWindow(DAT_005db85c) — main window handle global not yet reconstructed.
                    // TODO(REVERSE): DAT_005db85c (main HWND) needs to be threaded through from window creation.
                    ShutdownStage4(); // Ghidra: FUN_00542f50 @ 0x00542f50
                    ShutdownStage5(); // Ghidra: FUN_00542f90 @ 0x00542f90
                    RestoreWaveOutVolume(); // Ghidra: FUN_005138d0 @ 0x005138d0
                    CoUninitialize();
                }

                return 0;
            }

            int choice = MessageBoxA(IntPtr.Zero, s_PleaseInsertCdMessage, s_ErrorCaption, 0x2001);
            if (choice == 2 /* IDCANCEL */)
            {
                Exit(1);
            }
        }
    }

    // Ghidra: &PTR_s_Blaze___Blade_00563208 — the 5-entry ANSI string-pointer table passed as
    // Setup3DDevice's 3rd argument (see the P/Invoke declaration above for how winsys3d.dll's
    // dialog proc consumes each slot).
    //
    // This previously passed `driveRoot` as a placeholder, which was actively wrong: the DLL would
    // have read a 4-byte "X:\\\0" buffer as an array of five pointers and dereferenced whatever
    // followed it on the stack. Entry [0] is well founded — Ghidra's own symbol name for the table
    // (`PTR_s_Blaze___Blade_...`) records that its first entry points at the "Blaze & Blade" string,
    // the same literal used as the movie window's title (s_Blaze___Blade_005631d8).
    //
    // TODO(REVERSE): entries [1]..[4] are separate .data string pointers whose bytes are not in
    // Blaze.c's text (see the "Systemic blocker" section of REVERSE_STATUS.md). They are filled
    // with explicit placeholder text rather than null, because the DLL dereferences all five
    // unconditionally once the array pointer itself is non-null.
    private static byte** s_RendererDialogStrings;

    private static byte** GetRendererDialogStrings()
    {
        if (s_RendererDialogStrings == null)
        {
            byte** table = (byte**)NativeMemory.Alloc(5 * (nuint)sizeof(byte*));
            table[0] = ToAnsiZ("Blaze & Blade");
            table[1] = ToAnsiZ("UNKNOWN_REVERSE_0056320c");
            table[2] = ToAnsiZ("UNKNOWN_REVERSE_00563210");
            table[3] = ToAnsiZ("UNKNOWN_REVERSE_00563214");
            table[4] = ToAnsiZ("UNKNOWN_REVERSE_00563218");
            s_RendererDialogStrings = table;
        }
        return s_RendererDialogStrings;
    }

    // Ghidra: DAT_00600920, a 0x1000-entry (0x4000-byte) table populated in RunApplication.
    private static readonly uint[] s_LookupTable00600920 = new uint[0x1000];

    // Ghidra: DAT_00604920 / DAT_00600900 — the two raw 8-bit-PCM sample banks, allocated in
    // RunApplication and filled straight from disk. Their role is settled by PlaySoundEffect
    // (FUN_0053cea0), which adds a directory entry's offset to one of these two bases to get the
    // sample's address: the "base" bank holds sound ids 0..0x3b (loaded from "\SND\BASE.SND"
    // alongside the first 0x1e0 bytes of the directory), the "level" bank holds ids 0x3c..0x4c
    // (loaded by FUN_00512xxx alongside the remaining 0x88 bytes of the directory).
    private static void* s_BaseSoundBank;   // 790000 bytes
    private static void* s_LevelSoundBank;  // 260000 bytes

    // Ghidra: s_Please_insert_Blaze___Blade_CD__00563258 / s_Erreur_00563278 — symbol names were
    // derived by Ghidra from the actual string bytes, but those bytes aren't reproduced in
    // Blaze.c's text. TODO(REVERSE): confirm exact original text/encoding from the binary.
    private static readonly byte* s_PleaseInsertCdMessage = ToAnsiZ("Please insert the Blaze & Blade CD-ROM.");
    private static readonly byte* s_ErrorCaption = ToAnsiZ("Erreur");

    private static byte* ToAnsiZ(string s)
    {
        byte[] bytes = System.Text.Encoding.Latin1.GetBytes(s + "\0");
        byte* p = (byte*)NativeMemory.Alloc((nuint)bytes.Length);
        for (int i = 0; i < bytes.Length; i++) p[i] = bytes[i];
        return p;
    }

    /// <summary>
    /// Ghidra: FUN_00513890 @ 0x00513890. Saves the current volume of every waveOut device and
    /// forces it to maximum (0xffffffff = full left/right) for the duration of the game.
    /// </summary>
    private static void SaveAndMaxWaveOutVolume()
    {
        uint deviceCount = waveOutGetNumDevs();
        for (uint device = 0; device < deviceCount; device++)
        {
            uint savedVolume;
            waveOutGetVolume((IntPtr)device, &savedVolume);
            s_SavedWaveOutVolumes[device] = savedVolume;
            waveOutSetVolume((IntPtr)device, 0xffffffff);
        }
    }

    /// <summary>Ghidra: FUN_005138d0 @ 0x005138d0 — restores the volumes saved above.</summary>
    private static void RestoreWaveOutVolume()
    {
        uint deviceCount = waveOutGetNumDevs();
        for (uint device = 0; device < deviceCount; device++)
        {
            waveOutSetVolume((IntPtr)device, s_SavedWaveOutVolumes[device]);
        }
    }

    // Ghidra: DAT_006008a0, an array of saved per-device waveOut volumes.
    private static readonly uint[] s_SavedWaveOutVolumes = new uint[32];

    // =====================================================================
    // Options .ini persistence (Ghidra: FUN_0050f140 "load" -> LoadOptionsIni,
    // FUN_0050f300/thunk_FUN_0050f300 "save" -> SaveOptionsIni, FUN_005235d0/FUN_00523540
    // "key bindings load/save" -> LoadKeyBindings/SaveKeyBindings).
    //
    // Section/key name strings below are reconstructed from Ghidra's auto-derived symbol names
    // (e.g. `s_SoundVol_005630b0` -> "SoundVol"), which mirror the literal string bytes closely
    // enough to trust directly. Two exceptions have no derived name (Ghidra didn't recognize
    // them as printable text) and are marked TODO(REVERSE) below.
    // =====================================================================

    private const string IniSection = "Options"; // Ghidra: s_Options_005630a8
    private const string ControlsIniSection = "Controls"; // Ghidra: s_Controls_00564f60

    // TODO(REVERSE): DAT_005630bc has no Ghidra-derived string name (short/undecoded literal);
    // ".ini" is a strong but unconfirmed guess for the settings-file extension.
    // Ghidra: DAT_005630bc — recovered from Legacy/Blaze.exe as ".cfg", not ".ini". The installed
    // game's own options file is BLAZE.cfg, and its [Options]/[D3DDriver]/[Controls] sections match
    // what LoadOptionsIni/LoadKeyBindings already read — so those two were correct all along and
    // had simply never located their file.
    private const string IniFileExtension = ".cfg";

    // TODO(REVERSE): ini key text unknown — DAT_00563080 (the key name argument for
    // DAT_005de160 below) has no Ghidra-derived string name either.
    private const string UnknownIniKey_005630a0 = "UNKNOWN_REVERSE_005630a0";

    // Ghidra: DAT_0054d000. TODO(REVERSE): no literal value in Blaze.c (data-only global); this
    // is inferred as 1/64 from cross-referencing winsys3d.c's own default
    // `CreateTranslationTables(1.0f)` call against the ini's default Gamma value of 0x40 (64) —
    // 64 * (1/64) == 1.0, matching the DLL's default. See Documentation/DLL/winsys3d.md.
    private const float GammaIniScale = 1.0f / 64.0f;

    // Ghidra: DAT_00589c70 (ini "SoundVol") / DAT_0057925c (ini "CdVol") / DAT_005d9de0 (ini "Gamma").
    // All three default to 0x40 (64) in GetPrivateProfileIntA.
    private static uint s_SoundVolumeIni;
    private static uint s_CdVolumeIni;
    private static uint s_GammaIni;

    // Ghidra: AU_00564a05._51_4_ — a 4-byte field at offset 0x33 inside a larger, not-yet-
    // reconstructed blob (AU_00564a05). Ini key "Small", default 0.
    private static uint s_SmallTextureFlag;

    // Ghidra: DAT_005de160, ini key UnknownIniKey_005630a0, default 0xb (11). Clamped in
    // LoadOptionsIni against a per-resolution table (DAT_00562e00). Exact purpose unknown.
    private static int s_UnknownDetailSetting_005de160 = 0xb;

    // Ghidra: DAT_00566cc4 (ini "Resolution", default 2) / DAT_00562df8 (ini "Timing", default 1).
    private static uint s_ResolutionIndex;
    private static uint s_TimingMode;

    // Ghidra: &DAT_00564a58, 4 ints — per-joystick-slot assignment (-1 = unassigned). Ini keys
    // built from format string reconstructed as "Joy %d" (Ghidra: s_Joy_d_00563064).
    private static readonly int[] s_JoystickSlot = new int[4];

    // Ghidra: &DAT_00562e00 — per-resolution clamp table for s_UnknownDetailSetting_005de160.
    // TODO(REVERSE): actual byte content not present in Blaze.c (data-only global); sized as a
    // guess and filled with int.MaxValue as a safe (non-clamping) placeholder rather than
    // inventing real limits.
    private static readonly int[] s_ResolutionDetailLimit = CreateUnknownResolutionLimitTable();

    // Ghidra: DAT_00562e00 — recovered from Legacy/Blaze.exe as the dwords
    // { 1, 7, 11, 16, 23, 0 }. These are real per-resolution detail clamps; the table was
    // previously filled with int.MaxValue on the assumption that its content was unrecoverable,
    // which turned the clamp into a silent no-op.
    // TODO(REVERSE): the trailing 0 is transcribed as read. It is plausibly a terminator rather
    // than a 6th entry meaning "clamp everything to 0", and no call site has been reconstructed
    // that indexes past 4 — s_ResolutionIndex comes from the ini's Resolution key (4 in the real
    // BLAZE.cfg). Kept verbatim rather than trimmed to a guess.
    private static int[] CreateUnknownResolutionLimitTable()
        => [1, 7, 11, 16, 23, 0];

    // Ghidra: &DAT_00564b58, 240 ints (4 groups x 3 categories x 20 actions) — key-binding table.
    // Key format reconstructed as "K%01x%01x%02x" (Ghidra: s_K_01x_01x_02x_00564f6c).
    private static readonly int[] s_KeyBindings = new int[4 * 3 * 20];

    // =====================================================================
    // DirectSound (confirmed). The interface identities below were open questions until the
    // shutdown path (FUN_00512ff0/FUN_00512ea0) and the buffer-creation path (FUN_00513030/
    // FUN_005133xx) were read: together they exercise enough distinct vtable slots to pin both
    // interfaces against the documented DirectX 3-5 COM layouts with no ambiguity left.
    //
    //   DAT_005db8e4 -> IDirectSound      : 0x08 Release, 0x0c CreateSoundBuffer
    //   DAT_005db900 -> IDirectSoundBuffer: 0x08 Release, 0x24 GetStatus, 0x2c Lock, 0x30 Play,
    //                                       0x3c SetVolume, 0x48 Stop, 0x4c Unlock
    //   DAT_005db904 -> IDirectSoundNotify: 0x08 Release, 0x0c SetNotificationPositions
    //
    // This retroactively confirms the earlier "vtable offset 0x3c is probably DirectSound
    // SetVolume" hypothesis in ApplyCdVolume/ApplyAllSoundBufferVolumes (was TODO(REVERSE) #9).
    //
    // The IDirectSoundNotify identity comes out of FUN_005131d0: the object is obtained by
    // QueryInterface (vtable slot 0) on the streaming buffer and is then only ever used through
    // slot 0x0c with the argument pair (8, &DAT_005db8a0) — and DAT_005db8a0..DAT_005db8df is
    // filled in as exactly 8 { dwOffset, hEventNotify } pairs just before that call. That is
    // IDirectSoundNotify::SetNotificationPositions(dwPositionNotifies, pcPositionNotifies) with
    // a DSBPOSITIONNOTIFY[8]; no other DirectSound interface has that shape.
    // =====================================================================

    private const int IUnknown_QueryInterface = 0x00 / 4;
    private const int IDirectSound_Release = 0x08 / 4;
    private const int IDirectSound_CreateSoundBuffer = 0x0c / 4;
    private const int IDirectSoundBuffer_Release = 0x08 / 4;
    private const int IDirectSoundBuffer_GetStatus = 0x24 / 4;
    private const int IDirectSoundBuffer_Lock = 0x2c / 4;
    private const int IDirectSoundBuffer_Play = 0x30 / 4;
    private const int IDirectSoundBuffer_SetVolume = 0x3c / 4;
    private const int IDirectSoundBuffer_Stop = 0x48 / 4;
    private const int IDirectSoundBuffer_Unlock = 0x4c / 4;
    private const int IDirectSoundNotify_Release = 0x08 / 4;
    private const int IDirectSoundNotify_SetNotificationPositions = 0x0c / 4;

    private const uint DSBSTATUS_PLAYING = 1;

    // DSBCAPS_* flag values used by the two DSBUFFERDESC literals in Blaze.c.
    private const uint DSBCAPS_LOCSOFTWARE = 0x00000008;
    private const uint DSBCAPS_CTRLFREQUENCY = 0x00000020;
    private const uint DSBCAPS_CTRLPAN = 0x00000040;
    private const uint DSBCAPS_CTRLVOLUME = 0x00000080;
    private const uint DSBCAPS_CTRLPOSITIONNOTIFY = 0x00000100;

    private const uint DSBLOCK_ENTIREBUFFER = 0x00000002;
    private const uint DSBPLAY_LOOPING = 0x00000001;

    private const ushort WAVE_FORMAT_PCM = 1;
    private const ushort WAVE_FORMAT_ADPCM = 2; // Microsoft ADPCM

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct WAVEFORMATEX
    {
        public ushort wFormatTag;
        public ushort nChannels;
        public uint nSamplesPerSec;
        public uint nAvgBytesPerSec;
        public ushort nBlockAlign;
        public ushort wBitsPerSample;
        public ushort cbSize;
    }

    /// <summary>
    /// The MS-ADPCM `fmt ` chunk body: a <see cref="WAVEFORMATEX"/> followed by wSamplesPerBlock
    /// (+0x12), wNumCoef (+0x14) and aCoef[wNumCoef] (+0x16). Every field offset here is confirmed
    /// by the accesses in FUN_00531bb0/FUN_00531f70/FUN_005131d0.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ADPCMWAVEFORMAT
    {
        public WAVEFORMATEX wfx;      // 0x00..0x11
        public ushort wSamplesPerBlock; // 0x12
        public ushort wNumCoef;         // 0x14
        // aCoef[] follows at 0x16 as pairs of shorts (iCoef1, iCoef2).
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DSBUFFERDESC
    {
        public uint dwSize;
        public uint dwFlags;
        public uint dwBufferBytes;
        public uint dwReserved;
        public WAVEFORMATEX* lpwfxFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DSBPOSITIONNOTIFY
    {
        public uint dwOffset;
        public IntPtr hEventNotify;
    }

    // Ghidra: DAT_005db8e4 — the IDirectSound device object.
    // TODO(REVERSE): nothing in the reconstructed call graph assigns it — no DirectSoundCreate
    // call site has been located yet, so it is still null at runtime and every entry point below
    // takes its "no device" early-out. Same category of coverage gap as DAT_005db85c (TODO #6).
    private static void* s_DirectSound;

    // Ghidra: DAT_005db754 — a second COM object released just before the IDirectSound device in
    // FUN_00512ff0. TODO(REVERSE): only ever seen through Release (vtable 0x08), so its concrete
    // type isn't pinned down; position in the teardown order suggests the primary/listener buffer.
    private static void* s_DirectSoundPrimary;

    // Ghidra: DAT_005db900 — the streaming-music IDirectSoundBuffer created by FUN_005131d0.
    // (The ini key that drives its volume is "CdVol", hence ApplyCdVolume's name, but the buffer
    // itself is the ADPCM music stream, not a CD-audio object — FUN_005131d0 fills it from a
    // .WAV file on disk.)
    private static void* s_MusicStreamBuffer;

    // Ghidra: DAT_005db904 — the IDirectSoundNotify of the buffer above.
    private static void* s_MusicStreamNotify;

    // Ghidra: &DAT_005db758, 50 IDirectSoundBuffer pointers. The count is confirmed by
    // FUN_00512ea0's loop bound (`while ((int)piVar3 <= 0x5db81f)` starting at 0x005db758 —
    // exactly 50 dword slots).
    private static readonly void*[] s_SoundBuffers = new void*[50];

    /// <summary>Invokes a no-argument <c>HRESULT</c> method at <paramref name="vtableSlot"/> on a COM object.</summary>
    private static int InvokeComMethod(void* comObject, int vtableSlot)
    {
        void** vtable = *(void***)comObject;
        var method = (delegate* unmanaged[Stdcall]<void*, int>)vtable[vtableSlot];
        return method(comObject);
    }

    /// <summary>
    /// Ghidra: FUN_00512ea0 @ 0x00512ea0. Releases all 50 sound buffers. When
    /// <paramref name="stopPlayingBuffers"/> is set, a buffer that is still playing is stopped and
    /// then released; when it is clear, a still-playing buffer is skipped entirely and left
    /// allocated (Ghidra: the `goto LAB_00512edf` path) — reproduced exactly.
    /// </summary>
    private static void ReleaseAllSoundBuffers(bool stopPlayingBuffers)
    {
        for (int i = 0; i < s_SoundBuffers.Length; i++)
        {
            void* buffer = s_SoundBuffers[i];
            if (buffer == null) continue;

            uint status;
            void** vtable = *(void***)buffer;
            var getStatus = (delegate* unmanaged[Stdcall]<void*, uint*, int>)vtable[IDirectSoundBuffer_GetStatus];
            getStatus(buffer, &status);

            if ((status & DSBSTATUS_PLAYING) != 0)
            {
                if (!stopPlayingBuffers) continue; // leave a playing buffer alone, allocated
                InvokeComMethod(buffer, IDirectSoundBuffer_Stop);
            }

            InvokeComMethod(buffer, IDirectSoundBuffer_Release);
            s_SoundBuffers[i] = null;
        }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern IntPtr LoadLibraryA(byte* lpLibFileName);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] private static extern IntPtr GetProcAddress(IntPtr hModule, byte* lpProcName);

    // Ghidra: TexFilterPossible_exref — Blaze.exe imports this `uint` as DATA (not a function)
    // from winsys3d.dll. Confirmed via winsys3d.c: it defines and reads/writes a plain global
    // `TexFilterPossible` (see Documentation/DLL/winsys3d.md). P/Invoke has no direct mechanism
    // for an imported data symbol, so it's resolved lazily via GetProcAddress instead.
    private static uint* s_TexFilterPossible;

    private static uint* GetTexFilterPossiblePtr()
    {
        if (s_TexFilterPossible == null)
        {
            IntPtr module = LoadLibraryA(ToAnsiZ("winsys3d.dll"));
            if (module != IntPtr.Zero)
            {
                s_TexFilterPossible = (uint*)GetProcAddress(module, ToAnsiZ("TexFilterPossible"));
            }
        }
        return s_TexFilterPossible;
    }

    private static string GetIniFilePath()
    {
        byte* buffer = stackalloc byte[0x104];
        GetModuleFileNameA(IntPtr.Zero, buffer, 0x104);
        string exePath = Marshal.PtrToStringAnsi((IntPtr)buffer) ?? string.Empty;
        int dot = exePath.LastIndexOf('.');
        if (dot >= 0) exePath = exePath[..dot];
        return exePath + IniFileExtension;
    }

    private static uint ReadIniInt(string key, int defaultValue, byte* iniPathAnsi)
    {
        byte* sectionAnsi = ToAnsiZ(IniSection);
        byte* keyAnsi = ToAnsiZ(key);
        uint result = GetPrivateProfileIntA(sectionAnsi, keyAnsi, defaultValue, iniPathAnsi);
        Free(sectionAnsi);
        Free(keyAnsi);
        return result;
    }

    private static void WriteIniInt(string key, uint value, byte* iniPathAnsi)
    {
        byte* sectionAnsi = ToAnsiZ(IniSection);
        byte* keyAnsi = ToAnsiZ(key);
        byte* valueAnsi = ToAnsiZ(value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        WritePrivateProfileStringA(sectionAnsi, keyAnsi, valueAnsi, iniPathAnsi);
        Free(sectionAnsi);
        Free(keyAnsi);
        Free(valueAnsi);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a261e0 — the path normalizer every winsys3d file-open call runs its
    /// argument through first. It does two things, both of which matter here:
    /// <list type="bullet">
    /// <item>skips one leading backslash, so a template like <c>"\SND\BASE.SND"</c> is relative</item>
    /// <item><b>truncates at the first <c>';'</c></b>, which is what makes the ISO-9660 version
    /// suffixes in <c>"BLAZE.ALL;1"</c> and <c>"SYSTEM.INI;1"</c> work — the DLL opens
    /// <c>"BLAZE.ALL"</c>. On a 1999 raw ISO-9660 mount the <c>;1</c> was part of the real
    /// filename; on a modern mount it is stripped by the filesystem. Either way this truncation
    /// makes the stored literal correct, so the literals are kept verbatim rather than edited.</item>
    /// </list>
    /// This was previously missed: <c>Ordinal_331</c> was recorded as a "thin CreateFileA wrapper"
    /// and called directly, which silently dropped the normalization.
    /// </summary>
    private static byte* NormalizePath(byte* path)
    {
        byte* source = path;
        if (*source == (byte)'\\') source++;

        byte* buffer = (byte*)Malloc(260 /* MAX_PATH */);
        int i = 0;
        while (*source != 0 && *source != (byte)';' && i < 259)
        {
            buffer[i++] = *source++;
        }
        buffer[i] = 0;
        return buffer;
    }

    /// <summary>
    /// Ghidra: winsys3d.dll ordinal 331 (winsys3d.c: FUN_00a26220). All three RunApplication call
    /// sites pass mode 0 (read-only):
    /// CreateFileA(NormalizePath(path), GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING,
    /// FILE_ATTRIBUTE_NORMAL, NULL).
    /// </summary>
    private static IntPtr OpenExistingFileReadOnly(byte* path)
    {
        byte* normalized = NormalizePath(path);
        IntPtr handle = CreateFileA(normalized, 0x80000000 /* GENERIC_READ */, 1 /* FILE_SHARE_READ */, null, 3 /* OPEN_EXISTING */, 0x80 /* FILE_ATTRIBUTE_NORMAL */, IntPtr.Zero);
        Free(normalized);
        return handle;
    }

    /// <summary>
    /// Ghidra: <c>IDirectSoundBuffer::SetVolume</c> (vtable offset 0x3c), called from FUN_005137b0
    /// and FUN_00513800. The value range (-10000..0, hundredths of a decibel) matches
    /// DSBVOLUME_MIN/DSBVOLUME_MAX. The interface identity — an open question when those two
    /// functions were first reconstructed — is now confirmed; see the DirectSound banner above.
    /// </summary>
    private static void InvokeSetVolume(void* comObject, int attenuationHundredthDb)
    {
        if (comObject == null) return;
        void** vtable = *(void***)comObject;
        var setVolume = (delegate* unmanaged[Stdcall]<void*, int, int>)vtable[IDirectSoundBuffer_SetVolume];
        setVolume(comObject, attenuationHundredthDb);
    }

    // Ghidra: DAT_005db8ec — the current music-stream attenuation, computed by FUN_005137b0 and
    // read back by FUN_005131d0 when it starts a new stream.
    private static int s_MusicAttenuation;

    // Ghidra: DAT_005db8e8 — the current sound-effect attenuation, computed by FUN_00513800 and
    // read back by FUN_00513180 every time an effect is played.
    private static int s_SoundEffectAttenuation;

    /// <summary>
    /// Ghidra: FUN_005137b0 @ 0x005137b0. Converts the "CdVol" ini setting (0..128-ish) into a
    /// DirectSound-style attenuation, stores it as the current music attenuation and applies it
    /// to the streaming-music buffer, if one exists yet.
    /// </summary>
    private static void ApplyCdVolume()
    {
        if (s_CdVolumeIni == 0) return;
        s_MusicAttenuation = (int)(s_CdVolumeIni * 2500) / 128 - 2500;
        if (s_MusicStreamBuffer != null)
        {
            // Ghidra special-cases exactly -2000 (not this function's own -2500 floor) to force
            // full mute (-10000). Looks like it may be a copy/paste of FUN_00513800's -2000
            // constant in the original binary, but is preserved exactly as decompiled.
            int applied = s_MusicAttenuation == -2000 ? -10000 : s_MusicAttenuation;
            InvokeSetVolume(s_MusicStreamBuffer, applied);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00513800 @ 0x00513800. Converts the sound-volume ini setting into a
    /// DirectSound-style attenuation, stores it as the current effect attenuation and applies it
    /// to every allocated sound buffer.
    /// </summary>
    private static void ApplyAllSoundBufferVolumes()
    {
        s_SoundEffectAttenuation = (int)(s_SoundVolumeIni * 2000) / 128 - 2000;
        foreach (void* buffer in s_SoundBuffers)
        {
            if (buffer == null) continue;
            int applied = s_SoundEffectAttenuation == -2000 ? -10000 : s_SoundEffectAttenuation;
            InvokeSetVolume(buffer, applied);
        }
    }

    // =====================================================================
    // Sound-effect buffers (Ghidra: FUN_00513030 / FUN_00513110 / FUN_00513150 / FUN_00513180).
    //
    // The four functions form the complete "one-shot sample" API used by the sound-effect code at
    // 0x00537xxx: allocate a slot -> lock it -> fill it with 8-bit stereo PCM -> unlock -> play.
    // The five call sites read back (`FUN_00513110(slot, &ptr, &bytes)`, then writing pairs of
    // unsigned bytes with 0x80 as silence) independently confirm the buffer format literal below.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00513030 @ 0x00513030. Creates a <paramref name="bufferBytes"/>-sized
    /// 11025 Hz / 8-bit / stereo secondary buffer and parks it in the first free slot of
    /// <see cref="s_SoundBuffers"/>, returning that slot index (-1 on any failure).
    ///
    /// TODO(REVERSE): Ghidra's register attribution is wrong in the tail of this function — it
    /// writes `piVar3` (which it had just loaded from DAT_005db8e4, the *device*) into the slot
    /// table and releases the same `piVar3` when the table is full, while the newly created
    /// buffer sits unused in the `local_2c` output parameter of CreateSoundBuffer. Storing the
    /// device in a buffer slot would make ReleaseAllSoundBuffers call IDirectSoundBuffer methods
    /// on an IDirectSound and would release the device 50 times over, so the decompiled text
    /// cannot be what the binary does. Transcribed as the evident intent: the *created buffer*
    /// is what gets stored and released.
    /// </summary>
    private static int AllocateSoundBuffer(uint bufferBytes)
    {
        if (s_DirectSound == null) return -1;

        // Frees every slot whose buffer has finished playing; a still-playing one is kept.
        ReleaseAllSoundBuffers(stopPlayingBuffers: false);

        WAVEFORMATEX format;
        format.wFormatTag = WAVE_FORMAT_PCM;
        format.nChannels = 2;
        format.nSamplesPerSec = 0x2b11; // 11025
        format.nAvgBytesPerSec = 0x5622; // 22050 == 11025 * 2 channels * 1 byte
        format.nBlockAlign = 2;
        format.wBitsPerSample = 8;
        format.cbSize = 0;

        DSBUFFERDESC desc;
        desc.dwSize = 0x14; // Ghidra literal; == sizeof(DSBUFFERDESC) in the original 32-bit layout
        desc.dwFlags = DSBCAPS_CTRLFREQUENCY | DSBCAPS_CTRLPAN | DSBCAPS_CTRLVOLUME; // 0xe0 == DSBCAPS_CTRLDEFAULT
        desc.dwBufferBytes = bufferBytes;
        desc.dwReserved = 0;
        desc.lpwfxFormat = &format;

        void* buffer = null;
        void** vtable = *(void***)s_DirectSound;
        var createSoundBuffer = (delegate* unmanaged[Stdcall]<void*, DSBUFFERDESC*, void**, void*, int>)vtable[IDirectSound_CreateSoundBuffer];
        if (createSoundBuffer(s_DirectSound, &desc, &buffer, null) < 0) return -1;

        for (int slot = 0; slot < s_SoundBuffers.Length; slot++)
        {
            if (s_SoundBuffers[slot] == null)
            {
                s_SoundBuffers[slot] = buffer;
                return slot;
            }
        }

        // All 50 slots busy: throw the buffer away again.
        InvokeComMethod(buffer, IDirectSoundBuffer_Release);
        return -1;
    }

    /// <summary>
    /// Ghidra: FUN_00513110 @ 0x00513110 — <c>IDirectSoundBuffer::Lock</c> over the whole buffer
    /// (dwFlags 2 == DSBLOCK_ENTIREBUFFER; the second pointer/size pair is passed as NULL, which
    /// is legal precisely because the entire buffer is locked from offset 0).
    /// </summary>
    private static bool LockSoundBuffer(int slot, void** audioPtr, uint* audioBytes)
    {
        void* buffer = s_SoundBuffers[slot];
        if (buffer == null) return false;

        void** vtable = *(void***)buffer;
        var lockBuffer = (delegate* unmanaged[Stdcall]<void*, uint, uint, void**, uint*, void**, uint*, uint, int>)vtable[IDirectSoundBuffer_Lock];
        return lockBuffer(buffer, 0, 0, audioPtr, audioBytes, null, null, DSBLOCK_ENTIREBUFFER) >= 0;
    }

    /// <summary>Ghidra: FUN_00513150 @ 0x00513150 — <c>IDirectSoundBuffer::Unlock</c>.</summary>
    private static void UnlockSoundBuffer(int slot, void* audioPtr, uint audioBytes)
    {
        void* buffer = s_SoundBuffers[slot];
        if (buffer == null) return;

        void** vtable = *(void***)buffer;
        var unlockBuffer = (delegate* unmanaged[Stdcall]<void*, void*, uint, void*, uint, int>)vtable[IDirectSoundBuffer_Unlock];
        unlockBuffer(buffer, audioPtr, audioBytes, null, 0);
    }

    /// <summary>
    /// Ghidra: FUN_00513180 @ 0x00513180. Applies the current effect volume and starts playback.
    /// The `-2000 &lt; DAT_005db8e8` guard means a fully attenuated effect is never even started.
    ///
    /// Ghidra decompiled the looping flag as `unaff_retaddr != 0` (a register it failed to
    /// attribute to a parameter); all five call sites pass a literal second argument of 0, which
    /// is what identifies it as a real parameter rather than garbage.
    /// </summary>
    private static void PlaySoundBuffer(int slot, bool looping)
    {
        void* buffer = s_SoundBuffers[slot];
        if (buffer == null || s_SoundEffectAttenuation <= -2000) return;

        InvokeSetVolume(buffer, s_SoundEffectAttenuation);

        void** vtable = *(void***)buffer;
        var play = (delegate* unmanaged[Stdcall]<void*, uint, uint, uint, int>)vtable[IDirectSoundBuffer_Play];
        play(buffer, 0, 0, looping ? DSBPLAY_LOOPING : 0);
    }

    // =====================================================================
    // Streaming music (Ghidra: FUN_005131d0 "start", FUN_00513470 "load", FUN_005135d0 "stop",
    // FUN_00513860 "toggle", plus the MS-ADPCM codec at FUN_00531bb0/FUN_00531f70).
    //
    // What the subsystem does, end to end: a .WAV file holding Microsoft-ADPCM data is read whole
    // into a 5 MB scratch buffer; a 16-bit stereo DirectSound buffer exactly 8 notification
    // segments long is created; the first 80 source blocks are decoded straight into it; playback
    // starts looping and a worker thread then refills whichever segment has just been passed.
    //
    // Everything above is reconstructed. The worker thread itself is not — see the TODO(REVERSE)
    // at the CreateThread site in StartMusicStream.
    // =====================================================================

    // Ghidra: DAT_005631d4 — nonzero while music is enabled (toggled by FUN_00513860, and read
    // by the options UI at 0x0050f2xx). Recovered from Legacy/Blaze.exe @ 0x005631d4 as **1**:
    // music ships **enabled**. This previously defaulted to 0 on the assumption that the value was
    // unrecoverable, which silently made StartMusicStream take its early-out forever.
    private static uint s_MusicEnabled = 1;

    // Ghidra: DAT_005631d0 — nonzero while no stream is running. Note the inverted sense: it is
    // *cleared* at the end of a successful start and *set* at the top of a stop. Left at 0 (its
    // .bss default): a first StartMusicStream always takes the "different track" path, which
    // calls StopMusicStream and so sets the flag before it is tested.
    private static uint s_MusicStreamStopped;

    // Ghidra: DAT_005db8f0 — the whole-file scratch buffer. Its size is the literal 0x004c4b40
    // (5,000,000), which Ghidra rendered as `&LAB_004c4b40` because the constant happens to fall
    // inside the .text range; it is used identically as a plain size in the tail-zeroing loop.
    private const uint MusicFileBufferSize = 0x004c4b40;
    private static byte* s_MusicFileBuffer;

    // Ghidra: DAT_005db8f4 / DAT_005db8f8 / DAT_005db8fc — the `fmt ` chunk body, the `data`
    // chunk body and the `data` chunk size, all pointing into s_MusicFileBuffer.
    private static ADPCMWAVEFORMAT* s_MusicFormat;
    private static byte* s_MusicData;
    private static int s_MusicDataSize;

    // Ghidra: DAT_005db820 — the PCM WAVEFORMATEX describing the *decoded* stream. Kept in native
    // memory because the original is a .bss global whose address is handed to DirectSound.
    private static readonly WAVEFORMATEX* s_MusicOutputFormat = (WAVEFORMATEX*)NativeMemory.AllocZeroed((nuint)sizeof(WAVEFORMATEX));

    // Ghidra: DAT_005db8a0 (DSBPOSITIONNOTIFY[8]) and DAT_005db838 (the same 8 event handles,
    // kept separately so FUN_005135d0 can close them without walking the notify structs).
    private const int MusicNotifyCount = 8;
    private static readonly DSBPOSITIONNOTIFY* s_MusicNotifyPositions = (DSBPOSITIONNOTIFY*)NativeMemory.AllocZeroed(MusicNotifyCount * (nuint)sizeof(DSBPOSITIONNOTIFY));
    private static readonly IntPtr[] s_MusicNotifyEvents = new IntPtr[MusicNotifyCount];

    // Ghidra: DAT_005db858 — the "please exit" event signalled by FUN_005135d0.
    private static IntPtr s_MusicStopEvent;

    // Ghidra: DAT_005db880 / DAT_005db8e0 — the streaming worker thread and its thread id.
    private static IntPtr s_MusicStreamThread;
    private static uint s_MusicStreamThreadId;

    // Ghidra: DAT_005db860 / DAT_005db750 / DAT_005db89c. The first two are the streaming
    // worker's cursor (source block index reached so far) and its end marker (total number of
    // source blocks); the third is a flag set as playback starts.
    // TODO(REVERSE): all three are only ever *written* in the reconstructed graph — their readers
    // live in the missing worker thread body (LAB_00513660), so their exact roles above are read
    // off the values written (0x50 == the 80 blocks pre-decoded, and a block count derived from
    // the data-chunk size / nBlockAlign) rather than from consuming code.
    private static int s_MusicSourceBlockCursor;
    private static int s_MusicTotalBlocks;
    private static int s_MusicPlaying;

    // Ghidra: DAT_005db908 (the file currently loaded into s_MusicFileBuffer) and DAT_005dba08
    // (the most recently requested file, replayed when FUN_005131d0 is called with a null path).
    // 0x100 is the exact gap between DAT_005db908 and the next referenced global, DAT_005dba08.
    private const nuint MusicPathBufferSize = 0x100;
    private static readonly byte* s_LoadedMusicPath = (byte*)NativeMemory.AllocZeroed(MusicPathBufferSize);
    private static readonly byte* s_RequestedMusicPath = (byte*)NativeMemory.AllocZeroed(MusicPathBufferSize);

    // Ghidra: DAT_0054ded8, the riid handed to QueryInterface in FUN_005131d0. The GUID *bytes*
    // are .rdata and therefore absent from Blaze.c (same systemic blocker as the other data-only
    // symbols), but the interface identity is pinned by how the returned object is used — see the
    // DirectSound banner above. The value below is the documented IID_IDirectSoundNotify.
    private static readonly Guid IID_IDirectSoundNotify = new Guid("b0210783-89cd-11d0-af08-00a0c925cd16");

    // Ghidra: DAT_0056ac60 (16 shorts), DAT_0056ac80 and DAT_0056ac90 (7 shorts each) — read by
    // FUN_00531bb0 and FUN_00531f70 respectively.
    //
    // These are data-only globals, so their bytes are not in Blaze.c either; the values below are
    // the standard Microsoft-ADPCM tables. The identification is structural rather than a guess:
    // FUN_00531f70 only accepts a `fmt ` chunk with wFormatTag == 2 (WAVE_FORMAT_ADPCM),
    // wBitsPerSample == 4 and wNumCoef == 7, and then compares aCoef[0..6].iCoef1/iCoef2 against
    // DAT_0056ac80/DAT_0056ac90 — i.e. it verifies the file uses the standard coefficient set so
    // that the decoder's own hardcoded tables apply. The addresses corroborate the sizes exactly:
    // 16 shorts at 0x0056ac60 end at 0x0056ac80, and 7 shorts at 0x0056ac80 end at 0x0056ac8e,
    // padded to the next entry at 0x0056ac90.
    // TODO(REVERSE): confirm the byte values against the binary if it ever becomes available.
    private static readonly short[] MsAdpcmAdaptationTable =
    {
        230, 230, 230, 230, 307, 409, 512, 614,
        768, 614, 512, 409, 307, 230, 230, 230,
    };
    private static readonly short[] MsAdpcmCoefficient1 = { 256, 512, 0, 192, 240, 460, 392 };
    private static readonly short[] MsAdpcmCoefficient2 = { 0, -256, 0, 64, 0, -208, -232 };

    /// <summary>
    /// Ghidra: FUN_00531f70 @ 0x00531f70. Accepts only a `fmt ` chunk that is mono/stereo
    /// 4-bit Microsoft ADPCM using the standard 7-entry coefficient set.
    /// </summary>
    private static bool IsStandardMsAdpcmFormat(ADPCMWAVEFORMAT* format)
    {
        if (format == null) return false;
        if (format->wfx.wFormatTag != WAVE_FORMAT_ADPCM) return false;
        if (format->wfx.wBitsPerSample != 4) return false;
        if (format->wfx.nChannels == 0 || format->wfx.nChannels >= 3) return false;
        if (format->wNumCoef != 7) return false;

        short* aCoef = (short*)((byte*)format + 0x16);
        for (int i = 0; i < 7; i++)
        {
            if (aCoef[i * 2] != MsAdpcmCoefficient1[i]) return false;
            if (aCoef[i * 2 + 1] != MsAdpcmCoefficient2[i]) return false;
        }
        return true;
    }

    /// <summary>
    /// Writes one sample in the destination format and returns the advanced cursor. Ghidra emits
    /// this inline three times (initial sample2, initial sample1, decoded sample); the 8-bit form
    /// `(char)(sample >> 8) + -0x80` is the usual signed-16 to unsigned-8 rebias
    /// (0x8000 -&gt; 0x00, 0x0000 -&gt; 0x80, 0x7fff -&gt; 0xff).
    /// </summary>
    private static byte* WriteDecodedSample(byte* dest, short sample, int destBitsPerSample)
    {
        if (destBitsPerSample == 8)
        {
            *dest = (byte)((byte)((uint)(ushort)sample >> 8) - 0x80);
            return dest + 1;
        }
        *(short*)dest = sample;
        return dest + 2;
    }

    /// <summary>
    /// Ghidra: FUN_00531bb0 @ 0x00531bb0. Decodes Microsoft-ADPCM blocks from
    /// <paramref name="src"/> into <paramref name="dest"/> until <paramref name="srcByteBudget"/>
    /// source bytes have been consumed, and returns the number of bytes written.
    ///
    /// The block layout it walks — per channel: 1 predictor index, then a 16-bit iDelta, iSamp1
    /// and iSamp2 — plus the nibble-per-sample body with channels interleaved inside each byte,
    /// is standard MS-ADPCM, which is what makes the hardcoded tables above identifiable.
    ///
    /// The original keeps iDelta/iSamp1 in one `short[4]` (deltas at [0..1], samples at [2..3])
    /// and iSamp2 in a `uint[2]` shared with the byte counter. Because the format check restricts
    /// this to at most 2 channels those regions never overlap, so they are split into separate
    /// per-channel arrays here without changing behaviour.
    /// </summary>
    private static int DecodeMsAdpcm(ADPCMWAVEFORMAT* srcFormat, byte* src, WAVEFORMATEX* destFormat, byte* dest, uint srcByteBudget)
    {
        int numCoef = srcFormat->wNumCoef & 0xff;      // Ghidra reads only the low byte
        int channels = srcFormat->wfx.nChannels & 0xff;
        int destBits = destFormat->wBitsPerSample & 0xff;
        int samplesPerBlock = srcFormat->wSamplesPerBlock;
        short* aCoef = (short*)((byte*)srcFormat + 0x16);

        int decodedSamples = 0;
        if (srcByteBudget == 0) return 0;

        short* delta = stackalloc short[2];
        short* sample1 = stackalloc short[2];
        short* sample2 = stackalloc short[2];
        short* coef1 = stackalloc short[2];
        short* coef2 = stackalloc short[2];

        uint consumed = 0;
        uint blockHeaderBytes = (uint)(channels * 7);
        byte* s = src;
        byte* d = dest;

        while (true)
        {
            consumed += blockHeaderBytes;
            if (consumed > srcByteBudget) break;

            for (int ch = 0; ch < channels; ch++)
            {
                byte predictorIndex = *s++;
                if (predictorIndex >= numCoef)
                {
                    // Ghidra explicitly zeroes the sample count before bailing out here, unlike
                    // the "ran out of source" exit further down, which keeps what it decoded.
                    decodedSamples = 0;
                    goto finished;
                }
                coef1[ch] = aCoef[predictorIndex * 2];
                coef2[ch] = aCoef[predictorIndex * 2 + 1];
            }
            for (int ch = 0; ch < channels; ch++) { delta[ch] = *(short*)s; s += 2; }
            for (int ch = 0; ch < channels; ch++) { sample1[ch] = *(short*)s; s += 2; }
            for (int ch = 0; ch < channels; ch++) { sample2[ch] = *(short*)s; s += 2; }

            // The two samples carried in the block header are emitted before anything is decoded,
            // iSamp2 first (it is the older of the two).
            for (int ch = 0; ch < channels; ch++) d = WriteDecodedSample(d, sample2[ch], destBits);
            for (int ch = 0; ch < channels; ch++) d = WriteDecodedSample(d, sample1[ch], destBits);
            decodedSamples += 2;

            bool readNewByte = true;
            int pendingLowNibble = 0;
            for (int sampleIndex = 2; sampleIndex < samplesPerBlock; sampleIndex++)
            {
                for (int ch = 0; ch < channels; ch++)
                {
                    int nibble;
                    if (readNewByte)
                    {
                        consumed++;
                        if (consumed > srcByteBudget) goto finished;
                        int packed = (sbyte)*s++;
                        nibble = (short)packed >> 4;               // high nibble, sign-extended
                        pendingLowNibble = (packed << 0x1c) >> 0x1c; // low nibble, sign-extended
                        readNewByte = false;
                    }
                    else
                    {
                        nibble = pendingLowNibble;
                        readNewByte = true;
                    }

                    short oldDelta = delta[ch];
                    short newDelta = (short)(int)((uint)(MsAdpcmAdaptationTable[nibble & 0xf] * oldDelta) >> 8);
                    delta[ch] = newDelta < 0x10 ? (short)0x10 : newDelta;

                    int predicted = ((coef2[ch] * sample2[ch] + coef1[ch] * sample1[ch]) >> 8) + oldDelta * nibble;
                    if (predicted >= 0x8000) predicted = 0x7fff;
                    else if (predicted < -0x8000) predicted = -0x8000;

                    d = WriteDecodedSample(d, (short)predicted, destBits);
                    sample2[ch] = sample1[ch];
                    sample1[ch] = (short)predicted;
                }
                decodedSamples++;
            }

            if (consumed >= srcByteBudget) break;
        }

    finished:
        // Ghidra: `(((destBits & 0xff) >> 3) << (channels >> 1 & 0x1f)) * decodedSamples` — bytes
        // per sample, doubled for stereo, times the number of sample frames emitted.
        return ((destBits >> 3) << (channels >> 1)) * decodedSamples;
    }

    /// <summary>
    /// Ghidra: FUN_00513470 @ 0x00513470. Reads a Microsoft-ADPCM .WAV whole into the 5 MB
    /// scratch buffer and locates its `fmt ` and `data` chunks. The header is read first (0x400
    /// bytes) so the format can be rejected before paying for the rest of the file.
    /// </summary>
    private static bool LoadMusicWaveFile(byte* path)
    {
        if (s_MusicFileBuffer == null) s_MusicFileBuffer = (byte*)Malloc(MusicFileBufferSize);

        IntPtr file = CreateFileA(path, 0x80000000 /* GENERIC_READ */, 1 /* FILE_SHARE_READ */, null,
            3 /* OPEN_EXISTING */, 0x80 /* FILE_ATTRIBUTE_NORMAL */, IntPtr.Zero);
        if (file == (IntPtr)(-1)) return false;

        lstrcpyA(s_LoadedMusicPath, path);

        uint fileSize = GetFileSize(file, null);
        uint bytesRead;
        ReadFile(file, s_MusicFileBuffer, 0x400, &bytesRead, null);

        // "RIFF" / "WAVE" / "fmt " (little-endian dwords, exactly as compared in Blaze.c).
        if (*(uint*)s_MusicFileBuffer == 0x46464952 &&
            *(uint*)(s_MusicFileBuffer + 8) == 0x45564157 &&
            *(uint*)(s_MusicFileBuffer + 12) == 0x20746d66)
        {
            s_MusicFormat = (ADPCMWAVEFORMAT*)(s_MusicFileBuffer + 20);
            if (IsStandardMsAdpcmFormat(s_MusicFormat))
            {
                // Continues from the current file position (0x400), so this reads the remainder.
                // TODO(REVERSE): the original subtracts unconditionally — a file smaller than
                // 0x400 bytes underflows the length, and one larger than 5 MB overruns the
                // buffer. Both are original behaviour and are left as-is rather than "fixed",
                // but they are why the tail-zeroing below is guarded.
                ReadFile(file, s_MusicFileBuffer + 0x400, fileSize - 0x400, &bytesRead, null);

                // Walk the chunk list from `fmt ` until "data".
                byte* chunk = s_MusicFileBuffer + 12;
                while (*(uint*)chunk != 0x61746164) chunk += *(uint*)(chunk + 4) + 8;
                s_MusicData = chunk + 8;
                s_MusicDataSize = *(int*)(chunk + 4);

                CloseHandle(file);

                // Clears everything past the file so a shorter track cannot play back the tail of
                // a longer previous one. The original does this unguarded; see the TODO above.
                if (fileSize < MusicFileBufferSize)
                {
                    NativeMemory.Clear(s_MusicFileBuffer + fileSize, MusicFileBufferSize - fileSize);
                }
                return true;
            }
        }

        CloseHandle(file);
        return false;
    }

    /// <summary>
    /// Ghidra: FUN_005131d0 @ 0x005131d0. Starts (or restarts) the streaming music.
    /// A null <paramref name="path"/> means "replay whatever was last requested".
    ///
    /// Return values follow the original exactly: 1 when nothing needed doing (music disabled, or
    /// the requested track is already streaming), 0 when a stream was set up or the file failed to
    /// load, and -1 when there is no DirectSound device.
    /// </summary>
    private static int StartMusicStream(byte* path)
    {
        if (path == null) path = s_RequestedMusicPath;
        else lstrcpyA(s_RequestedMusicPath, path);

        if (s_MusicEnabled == 0) return 1;

        // Ghidra: __strcmpi — a different track than the one currently in the scratch buffer has
        // to stop the current stream and reload before it can be started.
        if (lstrcmpiA(path, s_LoadedMusicPath) != 0)
        {
            StopMusicStream();
            // Ghidra shows `FUN_00513470()` with no argument, but the callee's only use of its
            // parameter is the CreateFileA path and this is its sole call site — another instance
            // of the parameter-recovery weakness noted in REVERSE_STATUS.md.
            if (!LoadMusicWaveFile(path)) return 0;
        }

        if (s_MusicStreamStopped == 0) return 1;
        if (s_DirectSound == null) return -1;

        // The decoded stream is always described as 16-bit stereo at the source sample rate, even
        // for a mono source — preserved as decompiled.
        s_MusicOutputFormat->wFormatTag = WAVE_FORMAT_PCM;
        s_MusicOutputFormat->nChannels = 2;
        s_MusicOutputFormat->nSamplesPerSec = s_MusicFormat->wfx.nSamplesPerSec;
        s_MusicOutputFormat->nAvgBytesPerSec = s_MusicFormat->wfx.nSamplesPerSec << 2;
        s_MusicOutputFormat->nBlockAlign = 4;
        s_MusicOutputFormat->wBitsPerSample = 0x10;
        s_MusicOutputFormat->cbSize = 0;

        int samplesPerBlock = s_MusicFormat->wSamplesPerBlock;
        int sourceBlockAlign = s_MusicFormat->wfx.nBlockAlign;

        DSBUFFERDESC desc;
        desc.dwSize = 0x14;
        desc.dwFlags = DSBCAPS_LOCSOFTWARE | DSBCAPS_CTRLFREQUENCY | DSBCAPS_CTRLPAN |
                       DSBCAPS_CTRLVOLUME | DSBCAPS_CTRLPOSITIONNOTIFY; // 0x1e8
        // 0x140 == 8 notification segments x 0x28 bytes per sample-per-block, i.e. exactly the
        // 80 source blocks pre-decoded below (each block decodes to samplesPerBlock * 4 bytes).
        desc.dwBufferBytes = (uint)(samplesPerBlock * 0x140);
        desc.dwReserved = 0;
        desc.lpwfxFormat = s_MusicOutputFormat;

        void* streamBuffer = null;
        void** deviceVtable = *(void***)s_DirectSound;
        var createSoundBuffer = (delegate* unmanaged[Stdcall]<void*, DSBUFFERDESC*, void**, void*, int>)deviceVtable[IDirectSound_CreateSoundBuffer];
        if (createSoundBuffer(s_DirectSound, &desc, &streamBuffer, null) < 0) return 0;

        s_MusicStreamBuffer = streamBuffer;
        void** bufferVtable = *(void***)streamBuffer;

        void* notify = null;
        Guid iid = IID_IDirectSoundNotify;
        var queryInterface = (delegate* unmanaged[Stdcall]<void*, Guid*, void**, int>)bufferVtable[IUnknown_QueryInterface];
        queryInterface(streamBuffer, &iid, &notify);
        s_MusicStreamNotify = notify;

        for (int i = 0; i < MusicNotifyCount; i++)
        {
            IntPtr signal = CreateEventA(null, 0, 0, null);
            s_MusicNotifyPositions[i].hEventNotify = signal;
            s_MusicNotifyEvents[i] = signal;
            s_MusicNotifyPositions[i].dwOffset = (uint)(samplesPerBlock * i * 0x28);
        }
        s_MusicStopEvent = CreateEventA(null, 0, 0, null);

        void** notifyVtable = *(void***)notify;
        var setNotificationPositions = (delegate* unmanaged[Stdcall]<void*, uint, DSBPOSITIONNOTIFY*, int>)notifyVtable[IDirectSoundNotify_SetNotificationPositions];
        setNotificationPositions(notify, MusicNotifyCount, s_MusicNotifyPositions);

        void* audioPtr = null;
        uint audioBytes = 0;
        var lockBuffer = (delegate* unmanaged[Stdcall]<void*, uint, uint, void**, uint*, void**, uint*, uint, int>)bufferVtable[IDirectSoundBuffer_Lock];
        lockBuffer(streamBuffer, 0, 0, &audioPtr, &audioBytes, null, null, DSBLOCK_ENTIREBUFFER);

        // Prime the whole buffer with the first 80 source blocks.
        DecodeMsAdpcm(s_MusicFormat, s_MusicData, s_MusicOutputFormat, (byte*)audioPtr, (uint)(sourceBlockAlign * 0x50));
        s_MusicSourceBlockCursor = 0x50;
        s_MusicTotalBlocks = (s_MusicDataSize - 1) / sourceBlockAlign + 1;

        var unlockBuffer = (delegate* unmanaged[Stdcall]<void*, void*, uint, void*, uint, int>)bufferVtable[IDirectSoundBuffer_Unlock];
        unlockBuffer(streamBuffer, audioPtr, audioBytes, null, 0);

        s_MusicPlaying = 1;
        InvokeSetVolume(streamBuffer, s_MusicAttenuation);

        var play = (delegate* unmanaged[Stdcall]<void*, uint, uint, uint, int>)bufferVtable[IDirectSoundBuffer_Play];
        play(streamBuffer, 0, 0, DSBPLAY_LOOPING);

        s_MusicStreamStopped = 0;

        // TODO(REVERSE): the streaming worker thread's body is missing. Blaze.c passes
        // `&LAB_00513660` to CreateThread but contains no function at that address — Ghidra only
        // ever saw it address-taken, the same class of gap as the movie WndProc (TODO #14) and
        // the ordinal-790 device-lost callback (TODO #8). Calling CreateThread with an address we
        // cannot reconstruct would jump into nothing, so the call is skipped exactly like
        // CreateMoviePlayerWindow skips CreateWindowExA, and the handle stays zero.
        //
        // Consequence: everything above is real, so a track would start playing, but nothing ever
        // refills the buffer — the primed 80 blocks would loop forever instead of streaming on.
        // s_MusicSourceBlockCursor / s_MusicTotalBlocks / s_MusicPlaying / s_MusicNotifyEvents /
        // s_MusicStopEvent exist for that thread and currently have no reader.
        s_MusicStreamThread = IntPtr.Zero;
        s_MusicStreamThreadId = 0;

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_005135d0 @ 0x005135d0. Signals the worker thread to exit, waits for it, then
    /// tears down the streaming buffer, its notify interface and the 8 notification events.
    /// </summary>
    private static int StopMusicStream()
    {
        if (s_MusicStreamStopped == 0)
        {
            s_MusicStreamStopped = 1;
            SetEvent(s_MusicStopEvent);

            // With the worker thread not created (see StartMusicStream), the handle is zero and
            // this returns WAIT_FAILED immediately instead of blocking — kept as decompiled.
            WaitForSingleObject(s_MusicStreamThread, 0xffffffff);
            s_MusicStreamThread = IntPtr.Zero;

            if (s_MusicStreamBuffer != null) InvokeComMethod(s_MusicStreamBuffer, IDirectSoundBuffer_Release);
            if (s_MusicStreamNotify != null) InvokeComMethod(s_MusicStreamNotify, IDirectSoundNotify_Release);
            s_MusicStreamBuffer = null;
            s_MusicStreamNotify = null;

            // Only the 8 notification events are closed (Ghidra's loop bound is 0x5db85c, i.e. it
            // stops just before DAT_005db858): the stop event itself is leaked on every restart.
            // Preserved rather than corrected.
            for (int i = 0; i < MusicNotifyCount; i++) CloseHandle(s_MusicNotifyEvents[i]);
        }
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_00513860 @ 0x00513860. Flips the music-enabled flag and either stops the
    /// current stream or restarts the last requested track.
    /// </summary>
    private static void ToggleMusicEnabled()
    {
        s_MusicEnabled = s_MusicEnabled == 0 ? 1u : 0u;
        if (s_MusicEnabled == 0) StopMusicStream();
        else StartMusicStream(null);
    }

    // =====================================================================
    // Sound-effect layer (Ghidra: FUN_0053cea0 dispatcher + the five FUN_0053dxxx resamplers).
    //
    // PlaySoundEffect(soundId, pitch, gainLeft, gainRight) is the game's single sound entry point
    // — around 200 call sites all over Blaze.c. It looks the sample up in the sound directory,
    // then picks one of five hand-written resamplers by pitch band. Each resampler allocates a
    // DirectSound buffer sized for its output, converts the mono 8-bit source into 8-bit stereo
    // while applying the two gains, pads the remainder with silence and plays it.
    //
    // `pitch` is 1.10 fixed point with 0x400 as unity: 0x800 plays an octave up, 0x200 an octave
    // down. That reading is not a guess — it falls out of the dispatcher's own thresholds matching
    // each resampler's exact rate ratio, e.g. 0x800 -> Decimated(step 2), 0xc00 -> Decimated(3),
    // 0x600 -> DroppingOneIn(2) (ratio 3/2), 0x555 -> DroppingOneIn(3) (4/3), 0x200 -> Doubled
    // (1/2). Every band lines up.
    //
    // `gainLeft`/`gainRight` are 1.12 fixed point with 0x1000 as unity, halved by the dispatcher
    // before use; call sites pass unequal values to pan and values above 0x1000 to over-drive.
    // =====================================================================

    /// <summary>
    /// One entry of the sound directory at Ghidra `DAT_005e8340`: a byte offset into the sample
    /// bank selected by the sound id, and the sample's length in bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SoundDirectoryEntry
    {
        public int Offset;
        public int Length;
    }

    private const int SoundGainUnity = 0x1000;

    /// <summary>
    /// Applies a 1.12 fixed-point gain to one unsigned 8-bit PCM sample. Ghidra emits this inline
    /// a dozen times across the five resamplers, always with the same asymmetric clamp to
    /// [-0x7f, +0x7f] (not [-0x80, +0x7f]) before re-biasing by 0x80.
    /// </summary>
    private static byte ApplySoundGain(byte sample, int gain)
    {
        int value = (sample - 0x80) * gain >> 0xc;
        if (value > 0x7f) value = 0x7f;
        if (value < -0x7f) value = -0x7f;
        return (byte)(value + 0x80);
    }

    /// <summary>
    /// The tail shared by all five resamplers: pad whatever is left of the buffer with silence
    /// (0x80 is the unsigned-8-bit zero level), unlock and play.
    ///
    /// Note the guard, which is original behaviour and not a defensive addition: if the converted
    /// audio reached or passed the end of the buffer, the original skips Unlock *and* Play
    /// entirely, leaving the buffer locked and silent. The size formulas all carry a +100 sample
    /// slack that normally keeps this from happening.
    /// </summary>
    private static void FinishAndPlayResampledBuffer(int slot, byte* dest, byte* cursor, int bufferBytes)
    {
        if (cursor >= dest + bufferBytes) return;

        do { *cursor++ = 0x80; } while (cursor < dest + bufferBytes);
        UnlockSoundBuffer(slot, dest, (uint)bufferBytes);
        PlaySoundBuffer(slot, looping: false);
    }

    /// <summary>
    /// Ghidra: FUN_0053d1a0 @ 0x0053d1a0. Keeps every <c>stepMinusOne + 1</c>-th source sample,
    /// i.e. plays the sample back <c>step</c> times faster. `step == 1` is the identity path used
    /// for unity pitch.
    ///
    /// Ghidra types the first two parameters as <c>byte*</c> because the compiler reused their
    /// stack slots for the Lock output pointer and byte count; only <paramref name="src"/> is
    /// really a pointer.
    /// </summary>
    private static void PlaySampleDecimated(int stepMinusOne, byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int step = stepMinusOne + 1;
        int bufferBytes = (int)((srcLength / step + 100U) & 0xffffffe) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 4);

        if (gainLeft == SoundGainUnity && gainRight == SoundGainUnity)
        {
            for (; src < end; src += step)
            {
                cursor[0] = *src;
                cursor[1] = *src;
                cursor += 2;
            }
        }
        else
        {
            for (; src < end; src += step)
            {
                cursor[0] = ApplySoundGain(*src, gainLeft);
                cursor[1] = ApplySoundGain(*src, gainRight);
                cursor += 2;
            }
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053d2c0 @ 0x0053d2c0. Emits <paramref name="n"/> frames then skips one source
    /// sample, i.e. plays back at ratio (n+1)/n — the fine pitch-up steps between unity and
    /// double speed.
    ///
    /// The inner loop reproduces Ghidra's `for (; src = src + 1, i != 0; i--)` exactly: the source
    /// pointer advances as part of the *condition*, so each emitted frame reads the already
    /// advanced sample and the loop leaves the pointer one past the n frames it wrote.
    /// </summary>
    private static void PlaySampleDroppingOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int bufferBytes = (int)((uint)(srcLength + (100 - srcLength / (n + 1))) & 0xffffffe) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 5 - n);
        bool unityGain = gainLeft == SoundGainUnity && gainRight == SoundGainUnity;

        while (src < end)
        {
            int remaining = n;
            while (true)
            {
                src++;
                if (remaining == 0) break;

                if (unityGain)
                {
                    cursor[0] = *src;
                    cursor[1] = *src;
                }
                else
                {
                    cursor[0] = ApplySoundGain(*src, gainLeft);
                    cursor[1] = ApplySoundGain(*src, gainRight);
                }
                cursor += 2;
                remaining--;
            }
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053d420 @ 0x0053d420. Per group of n+1 source samples emits n+2 frames — the
    /// first sample, an interpolated frame midway to the next, then n more — i.e. plays back at
    /// ratio (n+1)/(n+2), the fine pitch-*down* steps between unity and half speed.
    /// </summary>
    private static void PlaySampleInsertingOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int bufferBytes = (int)((uint)(srcLength + srcLength / (n + 1) + 100) & 0xffffffe) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 5 - n);
        bool unityGain = gainLeft == SoundGainUnity && gainRight == SoundGainUnity;

        while (src < end)
        {
            byte midpoint = (byte)(((uint)*src + src[1]) / 2);
            if (unityGain)
            {
                cursor[0] = *src;
                cursor[1] = *src;
                cursor[2] = midpoint;
                cursor[3] = midpoint;
            }
            else
            {
                cursor[0] = ApplySoundGain(*src, gainLeft);
                cursor[1] = ApplySoundGain(*src, gainRight);
                cursor[2] = ApplySoundGain(midpoint, gainLeft);
                cursor[3] = ApplySoundGain(midpoint, gainRight);
            }
            cursor += 4;

            int remaining = n;
            while (true)
            {
                src++;
                if (remaining == 0) break;

                if (unityGain)
                {
                    cursor[0] = *src;
                    cursor[1] = *src;
                }
                else
                {
                    cursor[0] = ApplySoundGain(*src, gainLeft);
                    cursor[1] = ApplySoundGain(*src, gainRight);
                }
                cursor += 2;
                remaining--;
            }
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053d640 @ 0x0053d640. Per group of n source samples emits 2n-1 frames — one
    /// plain frame, then n-1 pairs of (sample, interpolated midpoint) — i.e. ratio n/(2n-1), the
    /// fine steps just above half speed.
    /// </summary>
    private static void PlaySampleDoubledExceptOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int bufferBytes = (int)((uint)(srcLength * 2 + 100 - srcLength / (n + 1)) & 0xffffffe) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 5 - n);
        bool unityGain = gainLeft == SoundGainUnity && gainRight == SoundGainUnity;

        while (src < end)
        {
            if (unityGain)
            {
                cursor[0] = *src;
                cursor[1] = *src;
            }
            else
            {
                cursor[0] = ApplySoundGain(*src, gainLeft);
                cursor[1] = ApplySoundGain(*src, gainRight);
            }
            cursor += 2;

            int remaining = n - 1;
            while (true)
            {
                src++;
                if (remaining == 0) break;

                byte midpoint = (byte)(((uint)*src + src[1]) / 2);
                if (unityGain)
                {
                    cursor[0] = *src;
                    cursor[1] = *src;
                    cursor[2] = midpoint;
                    cursor[3] = midpoint;
                }
                else
                {
                    cursor[0] = ApplySoundGain(*src, gainLeft);
                    cursor[1] = ApplySoundGain(*src, gainRight);
                    cursor[2] = ApplySoundGain(midpoint, gainLeft);
                    cursor[3] = ApplySoundGain(midpoint, gainRight);
                }
                cursor += 4;
                remaining--;
            }
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053d880 @ 0x0053d880. Emits two frames per source sample (the sample and an
    /// interpolated midpoint) — exactly half speed, one octave down.
    /// </summary>
    private static void PlaySampleDoubled(byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int bufferBytes = (srcLength * 2 + 100) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 4);
        bool unityGain = gainLeft == SoundGainUnity && gainRight == SoundGainUnity;

        for (; src < end; src++)
        {
            byte midpoint = (byte)(((uint)*src + src[1]) / 2);
            if (unityGain)
            {
                cursor[0] = *src;
                cursor[1] = *src;
                cursor[2] = midpoint;
                cursor[3] = midpoint;
            }
            else
            {
                cursor[0] = ApplySoundGain(*src, gainLeft);
                cursor[1] = ApplySoundGain(*src, gainRight);
                cursor[2] = ApplySoundGain(midpoint, gainLeft);
                cursor[3] = ApplySoundGain(midpoint, gainRight);
            }
            cursor += 4;
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053cea0 @ 0x0053cea0. The game's sound entry point: resolves
    /// <paramref name="soundId"/> in the sound directory and dispatches to the resampler whose
    /// rate ratio matches <paramref name="pitch"/> (1.10 fixed point, 0x400 == unity).
    /// </summary>
    private static void PlaySoundEffect(int soundId, int pitch, int gainLeft, int gainRight)
    {
        gainLeft >>= 1;
        gainRight >>= 1;

        void* bank;
        if (soundId < SoundDirectoryBaseCount) bank = s_BaseSoundBank;
        else if (soundId <= 0x4c) bank = s_LevelSoundBank;
        else return; // ids past the end of the directory are silently ignored

        // Both halves of the directory are indexed off the same base; only the bank differs.
        // No null check on the bank here — the original has none either, and none is needed: the
        // pointer is only ever dereferenced after a sound buffer has been allocated successfully,
        // which cannot happen before RunApplication has allocated both banks.
        SoundDirectoryEntry entry = s_SoundDirectory[soundId];
        byte* src = (byte*)bank + entry.Offset;
        int srcLength = entry.Length;

        // The bands below are transcribed one-for-one from Blaze.c's if-ladder. Each threshold is
        // the lower bound of the band whose resampler ratio approximates that pitch.
        if (pitch > 0x1bff) { PlaySampleDecimated(6, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x17ff) { PlaySampleDecimated(5, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x13ff) { PlaySampleDecimated(4, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0fff) { PlaySampleDecimated(3, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0bff) { PlaySampleDecimated(2, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x07ff) { PlaySampleDecimated(1, src, srcLength, gainLeft, gainRight); return; }

        if (pitch > 0x05ff) { PlaySampleDroppingOneIn(2, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0554) { PlaySampleDroppingOneIn(3, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x04ff) { PlaySampleDroppingOneIn(4, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x04d0) { PlaySampleDroppingOneIn(5, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x04b3) { PlaySampleDroppingOneIn(6, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0498) { PlaySampleDroppingOneIn(7, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x048a) { PlaySampleDroppingOneIn(8, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0471) { PlaySampleDroppingOneIn(9, src, srcLength, gainLeft, gainRight); return; }

        if (pitch >= 0x0400)
        {
            // Unity pitch: decimation by 1, i.e. a straight mono -> stereo copy.
            PlaySampleDecimated(0, src, srcLength, gainLeft, gainRight);
            return;
        }

        if (pitch > 0x03ce) { PlaySampleInsertingOneIn(0x14, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x03a1) { PlaySampleInsertingOneIn(10, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0371) { PlaySampleInsertingOneIn(6, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0354) { PlaySampleInsertingOneIn(5, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0332) { PlaySampleInsertingOneIn(4, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0301) { PlaySampleInsertingOneIn(3, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x02a9) { PlaySampleInsertingOneIn(2, src, srcLength, gainLeft, gainRight); return; }

        if (pitch >= 0x0268) { PlaySampleDoubledExceptOneIn(2, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0248) { PlaySampleDoubledExceptOneIn(3, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0237) { PlaySampleDoubledExceptOneIn(4, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0228) { PlaySampleDoubledExceptOneIn(6, src, srcLength, gainLeft, gainRight); return; }
        if (pitch < 0x021a) { PlaySampleDoubled(src, srcLength, gainLeft, gainRight); return; }

        // Ghidra renders this one call as `FUN_0053d640(9)` with its four remaining arguments
        // dropped — a register-attribution miss, not a different signature: it is the last arm of
        // the same ladder and every sibling arm passes the identical (src, length, gains) set.
        PlaySampleDoubledExceptOneIn(9, src, srcLength, gainLeft, gainRight);
    }

    /// <summary>Ghidra: FUN_0053db80 @ 0x0053db80 — fixed UI sound, id 0x38.</summary>
    private static void PlayUiSound38() => PlaySoundEffect(0x38, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dba0 @ 0x0053dba0 — fixed UI sound, id 0x39.</summary>
    private static void PlayUiSound39() => PlaySoundEffect(0x39, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053db60 @ 0x0053db60 — fixed UI sound, id 0x3a.</summary>
    private static void PlayUiSound3A() => PlaySoundEffect(0x3a, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dbc0 @ 0x0053dbc0 — fixed UI sound, id 0x3b.</summary>
    private static void PlayUiSound3B() => PlaySoundEffect(0x3b, 0x400, 0x2000, 0x2000);

    // TODO(REVERSE): FUN_0053da20 @ 0x0053da20 (positional gain calculation: distance/angle ->
    // left/right gain pair via the cos table, ordinal 450) and its caller FUN_0053db20 are the
    // remaining members of this family and are deliberately *not* reconstructed yet. Ghidra
    // declares FUN_0053da20 with 2 parameters but FUN_0053db20 calls it with 3, and its body reads
    // `unaff_SI` and `unaff_retaddr` — register-passed values Ghidra never tied to a parameter.
    // Reconstructing it would mean inventing a signature, so it waits for a Ghidra re-run with the
    // calling convention corrected (same class of problem as the cases listed in REVERSE_STATUS.md).

    /// <summary>Ghidra: FUN_005235d0 @ 0x005235d0 — loads the [Controls] key-binding table.</summary>
    private static void LoadKeyBindings(byte* iniPathAnsi)
    {
        byte* sectionAnsi = ToAnsiZ(ControlsIniSection);
        int index = 0;
        for (int group = 0; group < 4; group++)
        {
            for (int category = 0; category < 3; category++)
            {
                for (int action = 0; action < 20; action++)
                {
                    byte* keyAnsi = ToAnsiZ($"K{group:x1}{category:x1}{action:x2}");
                    uint value = GetPrivateProfileIntA(sectionAnsi, keyAnsi, -1, iniPathAnsi);
                    Free(keyAnsi);
                    if (value != 0xffffffff) s_KeyBindings[index] = (int)value;
                    index++;
                }
            }
        }
        Free(sectionAnsi);
    }

    /// <summary>Ghidra: FUN_00523540 @ 0x00523540 — saves the [Controls] key-binding table.</summary>
    private static void SaveKeyBindings(byte* iniPathAnsi)
    {
        byte* sectionAnsi = ToAnsiZ(ControlsIniSection);
        int index = 0;
        for (int group = 0; group < 4; group++)
        {
            for (int category = 0; category < 3; category++)
            {
                for (int action = 0; action < 20; action++)
                {
                    byte* keyAnsi = ToAnsiZ($"K{group:x1}{category:x1}{action:x2}");
                    byte* valueAnsi = ToAnsiZ(s_KeyBindings[index].ToString(System.Globalization.CultureInfo.InvariantCulture));
                    WritePrivateProfileStringA(sectionAnsi, keyAnsi, valueAnsi, iniPathAnsi);
                    Free(keyAnsi);
                    Free(valueAnsi);
                    index++;
                }
            }
        }
        Free(sectionAnsi);
    }

    /// <summary>
    /// Ghidra: FUN_0050f140 @ 0x0050f140. Loads Blaze.ini (built from the exe's own path with
    /// its extension replaced), applies the sound/CD volume and gamma settings immediately, and
    /// loads the key-binding table.
    /// </summary>
    private static void LoadOptionsIni()
    {
        byte* iniPathAnsi = ToAnsiZ(GetIniFilePath());

        s_SoundVolumeIni = ReadIniInt("SoundVol", 0x40, iniPathAnsi);
        s_CdVolumeIni = ReadIniInt("CdVol", 0x40, iniPathAnsi);
        s_GammaIni = ReadIniInt("Gamma", 0x40, iniPathAnsi);

        uint filter = ReadIniInt("Filter", 1, iniPathAnsi);
        uint* texFilterPossible = GetTexFilterPossiblePtr();
        if (texFilterPossible != null) *texFilterPossible = filter;

        s_SmallTextureFlag = ReadIniInt("Small", 0, iniPathAnsi);
        s_UnknownDetailSetting_005de160 = (int)ReadIniInt(UnknownIniKey_005630a0, 0xb, iniPathAnsi);
        s_ResolutionIndex = ReadIniInt("Resolution", 2, iniPathAnsi);
        s_TimingMode = ReadIniInt("Timing", 1, iniPathAnsi);

        int nextJoystickSlot = 0;
        for (int i = 0; i < s_JoystickSlot.Length; i++)
        {
            uint enabled = ReadIniInt($"Joy {i}", 1, iniPathAnsi);
            s_JoystickSlot[i] = enabled == 0 ? -1 : nextJoystickSlot++;
        }

        ApplyCdVolume();
        ApplyAllSoundBufferVolumes();
        CreateTranslationTables(s_GammaIni * GammaIniScale);

        if (s_ResolutionIndex < (uint)s_ResolutionDetailLimit.Length &&
            s_ResolutionDetailLimit[s_ResolutionIndex] < s_UnknownDetailSetting_005de160)
        {
            s_UnknownDetailSetting_005de160 = s_ResolutionDetailLimit[s_ResolutionIndex];
        }

        LoadKeyBindings(iniPathAnsi);
        Free(iniPathAnsi);
    }

    /// <summary>
    /// Ghidra: FUN_0050f300 @ 0x0050f300, duplicated by the linker/Ghidra as
    /// thunk_FUN_0050f300 @ 0x0050f520 (identical body — both map onto this one method). Mirror
    /// of <see cref="LoadOptionsIni"/>: writes every setting back to the ini file.
    /// </summary>
    private static void SaveOptionsIni()
    {
        byte* iniPathAnsi = ToAnsiZ(GetIniFilePath());

        WriteIniInt("SoundVol", s_SoundVolumeIni, iniPathAnsi);
        WriteIniInt("CdVol", s_CdVolumeIni, iniPathAnsi);
        WriteIniInt("Gamma", s_GammaIni, iniPathAnsi);

        uint* texFilterPossible = GetTexFilterPossiblePtr();
        WriteIniInt("Filter", texFilterPossible != null ? *texFilterPossible : 0, iniPathAnsi);

        WriteIniInt("Small", s_SmallTextureFlag, iniPathAnsi);
        WriteIniInt(UnknownIniKey_005630a0, (uint)s_UnknownDetailSetting_005de160, iniPathAnsi);
        WriteIniInt("Resolution", s_ResolutionIndex, iniPathAnsi);
        WriteIniInt("Timing", s_TimingMode, iniPathAnsi);

        for (int i = 0; i < s_JoystickSlot.Length; i++)
        {
            WriteIniInt($"Joy {i}", s_JoystickSlot[i] != -1 ? 1u : 0u, iniPathAnsi);
        }

        SaveKeyBindings(iniPathAnsi);
        Free(iniPathAnsi);
    }

    // =====================================================================
    // Movie / intro-video subsystem (Ghidra: FUN_00542xxx cluster, ~30 functions).
    //
    // This is Blaze & Blade's own intro-movie player: a hidden VfW-style window
    // (window class "VfwClass") wrapping what strongly appears to be a DirectDraw
    // output surface (FUN_00512930/FUN_00512a10/FUN_00512b40/FUN_00512c80 use vtable
    // offsets that match the public, documented IDirectDraw/IDirectDrawSurface (v1)
    // COM layout exactly: 0x50=SetCooperativeLevel — confirmed by the sibling error
    // string "SetCooperativeLevel() failed to g[et]..."; 0x54=SetDisplayMode called
    // with 640x480x16; 0x18=CreateSurface; 0x2c=Flip/0x6c=Restore in a device-lost
    // retry loop) plus a *second*, Blaze-owned C++ "stream" object (vtable
    // PTR_LAB_0054d0a0, constructed by FUN_005431e0) used to actually decode and
    // step video frames (open/pause/stop calls at vtable offsets 0x1c/0x20/0x24,
    // matching FUN_00543100/140/180; a 100ms SetTimer drives per-frame stepping).
    //
    // TODO(REVERSE): the Blaze-owned "stream" object's vtable slots point at
    // LAB_xxxxxxxx addresses with *no* decompiled body anywhere in Blaze.c — Ghidra
    // never saw their instructions, only their address taken (same situation as the
    // ordinal-790 device-lost callback documented above). The exact decoder backing
    // it (VfW vs. an internal codec) can't be determined without further disassembly
    // of Blaze.exe outside what Blaze.c provides. Rather than invent a specific COM
    // interface/IID (the two IID byte blobs `DAT_0054e8c8`/`DAT_0054e8d8` referenced
    // deeper in this cluster, e.g. FUN_00543320, aren't reproduced in Blaze.c's text
    // either), this reconstruction implements every part that *is* independently
    // verifiable (window lifetime, DirectDraw surface lifetime, joystick capture,
    // the message pump, the exe-relative movie path building) faithfully, and stops
    // exactly at the "step one frame" vtable call — TrySetupMovieStream below always
    // reports failure, which the original's own code already treats as a normal,
    // handled case (RunMainLoop keeps going without the intro movies playing).
    // =====================================================================

    // Ghidra: DAT_005dffa8, a 0x30-byte "movie player" struct; single global instance.
    private sealed class MoviePlayerState
    {
        public IntPtr HWnd;                 // offset 0x10 (param_1[4])
        public IntPtr HInstance;            // offset 0x14 (param_1[5])
        public void* DirectDrawWrapper;     // offset 0x1c (param_1[7]) — Ghidra: FUN_00512930 result
        public void* Stream;                // offset 0x18 (param_1[6]) — Ghidra: FUN_005431e0 result; TODO(REVERSE) vtable unrecoverable
        public bool Playing;                // offset 0x00 (*param_1)
        public int Result;                  // offset 0x2c (param_1[0xb])
        public byte* TitleBitmap;           // offset 0x08 (param_1[2]) — the attract screen's TITLE640.BMP
                                            // image. Allocated on *wsyskern's* heap inside LoadBitmapFile,
                                            // so it is released with WsysFree, never the CRT Free.
        public int TitleBitmapLoaded;       // offset 0x0c (param_1[3]) — nonzero once TitleBitmap is cached
    }

    private static MoviePlayerState? s_MoviePlayer; // Ghidra: DAT_005dffa8

    private static readonly byte* s_VfwClassName = ToAnsiZ("VfwClass"); // Ghidra: s_VfwClass_0056e6b8 — TODO(REVERSE): exact bytes not recovered, name inferred from symbol
    private static readonly byte* s_MovieWindowTitle = ToAnsiZ("Blaze & Blade"); // Ghidra: s_Blaze___Blade_005631d8 — TODO(REVERSE): exact bytes not recovered

    /// <summary>Ghidra: FUN_00542740 @ 0x00542740 — creates the hidden window the movie player renders into.</summary>
    private static void CreateMoviePlayerWindow(MoviePlayerState player)
    {
        IntPtr hInstance = GetModuleHandleA(null);
        player.HInstance = hInstance;

        var wc = new WNDCLASSA
        {
            style = 0,
            lpfnWndProc = IntPtr.Zero, // TODO(REVERSE): Ghidra: &LAB_00542600, window-proc body not decompiled in Blaze.c; DefWindowProcA-equivalent behavior assumed by leaving 0 unsafe here would crash, so real registration is skipped below.
            cbWndExtra = 0,
            cbClsExtra = 0,
            hInstance = hInstance,
            hIcon = IntPtr.Zero,
            hbrBackground = GetStockObject(4 /* BLACK_BRUSH */),
            lpszMenuName = null,
            lpszClassName = s_VfwClassName,
        };

        // TODO(REVERSE): the original's window procedure (&LAB_00542600) has no decompiled body in
        // Blaze.c, so a faithful RegisterClassA/CreateWindowExA call can't safely be made here (an
        // unset lpfnWndProc would crash on the first message). The rest of the movie player degrades
        // gracefully with HWnd left Zero — see TrySetupMovieStream.
        player.HWnd = IntPtr.Zero;
    }

    /// <summary>Ghidra: FUN_00542940 @ 0x00542940 — destroys the movie player's hidden window.</summary>
    private static void DestroyMoviePlayerWindow(MoviePlayerState player)
    {
        if (player.HWnd != IntPtr.Zero)
        {
            DestroyWindow(player.HWnd);
        }
        player.HWnd = IntPtr.Zero;
        UnregisterClassA(s_VfwClassName, player.HInstance);
    }

    /// <summary>
    /// Ghidra: FUN_00542800 @ 0x00542800. Allocates the DirectDraw-wrapper and custom stream
    /// sub-objects. In the original, this always returns success (1) once memory allocation
    /// succeeds — the stream object's own construction can silently leave its fields zeroed
    /// without failing the overall open. TODO(REVERSE): see the subsystem banner above — the
    /// stream object's vtable can't be faithfully constructed, so `player.Stream` stays null.
    /// </summary>
    private static bool OpenMovieStream(MoviePlayerState player)
    {
        // Ghidra: iVar1 = FUN_00547220(0x48); then, only if that succeeded,
        //         iVar1 = FUN_00512930(*(param_1 + 0x10));   <- the HWnd
        //         *(param_1 + 0x1c) = iVar1;
        //         if (iVar1 == 0) return 0;
        //
        // The second step is the one that matters and it is NOT optional: the original stores
        // FUN_00512930's result — the constructed DirectDraw wrapper — and **returns failure when
        // it is null**. FUN_00512930 builds that wrapper against the movie player's window, and
        // neither exists here: the window is never created (its WNDPROC has no decompiled body,
        // TODO #14) and the wrapper's own class vtable is unrecoverable (TODO #13).
        //
        // So the faithful result of this function, in the current reconstruction, is **failure** —
        // not success. An earlier revision allocated the buffer, skipped the construction entirely,
        // and still returned true, claiming a step it had never performed. That is what pushed
        // RunMainLoop into an attract sequence the original would have skipped outright, and it is
        // why startup dead-ended once the CD made the title bitmap loadable (TODO #34).
        //
        // The original treats a failed open as an ordinary, handled outcome: RunMainLoop's
        // `if (streamOpened)` skips the intro movies and the attract loop and carries straight on
        // to engine initialization. Reporting failure here is therefore the *original's own*
        // no-DirectDraw path, not a workaround bolted on to get past a blocker.
        void* ddrawWrapper = Malloc(0x48);
        if (ddrawWrapper == null) return false;

        // TODO(REVERSE): FUN_00512930 goes here. Until it exists the wrapper is null by
        // construction, so free the raw allocation rather than parking a half-built object in
        // player.DirectDrawWrapper where teardown would later treat it as real.
        Free(ddrawWrapper);
        player.DirectDrawWrapper = null;
        player.Stream = null;
        return false;
    }

    /// <summary>Ghidra: FUN_005428d0 @ 0x005428d0 — releases the sub-objects allocated by OpenMovieStream.</summary>
    private static void CloseMovieStream(MoviePlayerState player)
    {
        if (player.Stream != null)
        {
            // Ghidra: FUN_00543000() [reset stream vtable] + FUN_00547210(iVar1) [[Free]].
            Free(player.Stream);
        }
        player.Stream = null;

        if (player.HWnd != IntPtr.Zero)
        {
            MoveWindow(player.HWnd, 0, 0, 2000, 2000, 1);
        }

        if (player.DirectDrawWrapper != null)
        {
            // Ghidra: FUN_00512b40(param_1[4]) — real DirectDraw surface/clipper teardown; TODO(REVERSE)
            // once FUN_00512930's construction above is wired to a real IDirectDraw object.
            Free(player.DirectDrawWrapper);
        }
        player.DirectDrawWrapper = null;

        if (player.HWnd != IntPtr.Zero)
        {
            ShowWindow(player.HWnd, 3 /* SW_MAXIMIZE, per Ghidra literal */);
        }
    }

    /// <summary>Ghidra: FUN_00542f60 @ 0x00542f60 — allocates the movie-player struct and creates its window.</summary>
    private static void InitMoviePlayer()
    {
        // Ghidra: the allocation is followed by an explicit *(p+0xc) = 0; *(p+8) = 0 — the title
        // bitmap cache starts empty. A fresh MoviePlayerState is already zeroed, so this mirrors
        // the original rather than adding behavior.
        s_MoviePlayer = new MoviePlayerState { TitleBitmap = null, TitleBitmapLoaded = 0 };
        CreateMoviePlayerWindow(s_MoviePlayer);
    }

    /// <summary>Ghidra: FUN_00542f90 @ 0x00542f90 — destroys the window and frees the movie-player struct.</summary>
    private static void ShutdownMoviePlayer()
    {
        if (s_MoviePlayer == null) return;
        DestroyMoviePlayerWindow(s_MoviePlayer);
        if (s_MoviePlayer.Stream != null || s_MoviePlayer.DirectDrawWrapper != null)
        {
            CloseMovieStream(s_MoviePlayer);
        }

        // Ghidra: Free(*(DAT_005dffa8 + 8)) then FUN_00547210(iVar1) — note the two *different*
        // heaps, used correctly by the original: the title bitmap goes back to wsyskern (which
        // allocated it in LoadBitmapFile), the player struct to the CRT. The original calls this
        // unconditionally; wsyskern's Free tolerates null. Our struct is GC-managed, so only the
        // bitmap needs releasing here.
        WsysFree(s_MoviePlayer.TitleBitmap);
        s_MoviePlayer.TitleBitmap = null;
        s_MoviePlayer.TitleBitmapLoaded = 0;

        s_MoviePlayer = null;
    }

    /// <summary>Ghidra: FUN_00542f40 @ 0x00542f40 -> FUN_00542800 — tries to open the movie stream.</summary>
    private static bool TryOpenMovieStream()
        => s_MoviePlayer != null && OpenMovieStream(s_MoviePlayer);

    // Ghidra: s__sDATA_TITLE640_BMP_0056e6d4 — the wsprintfA format string is "%sDATA\TITLE640.BMP",
    // its "%s" filled with the CD drive root (DAT_005631f8). Only Ghidra's derived symbol name
    // survives in Blaze.c, not the literal bytes, so the text below is a best guess reconstructed
    // from that name — same caveat as the movie filenames above and REVERSE_STATUS.md TODOs #17/#22.
    private static readonly byte* s_TitleBitmapFileName = ToAnsiZ("DATA/TITLE640.BMP");

    /// <summary>
    /// Ghidra: FUN_00542f30 @ 0x00542f30 -> FUN_00542c40 — the attract-mode title screen: shows
    /// TITLE640.BMP and waits, returning non-zero to tell the attract loop to stop attracting.
    /// <para>
    /// Returns 1 when the caller should leave the attract loop (title bitmap unavailable, or — in
    /// the original — the user pressed something), 0 when the attract loop should continue to the
    /// next movie.
    /// </para>
    /// <para>
    /// TODO(REVERSE): Ghidra declares the FUN_00542f30 thunk as <c>void</c> (Blaze.c:90063) even
    /// though it tail-calls FUN_00542c40, which it types <c>undefined4 __fastcall</c>, and the call
    /// site in FUN_00529c10 reads the result (Blaze.c:74442, 74450). The call site is authoritative:
    /// this is the same unreliable-signature-recovery problem catalogued in REVERSE_STATUS.md, and
    /// following the <c>void</c> declaration here previously deleted two of the attract loop's four
    /// exits, hanging startup.
    /// </para>
    /// </summary>
    private static int ShowTitleScreenAndWait()
    {
        if (s_MoviePlayer == null) return 1;
        MoviePlayerState player = s_MoviePlayer;

        // Ghidra: *(param_1+0x2c) = 0
        player.Result = 0;

        // Ghidra: if (*(param_1+0xc) == 0) { ... } — load the title bitmap once, then cache it.
        if (player.TitleBitmapLoaded == 0)
        {
            byte* path = BuildCdPath(s_TitleBitmapFileName);
            player.TitleBitmap = LoadBitmapFile(path, 0 /* silent on failure — see wsyskern.md */);
            Free(path);

            // Ghidra: if (iVar3 == 0) return 1; — the original's own graceful degradation when the
            // CD (or the file) is absent. Reporting 1 is what breaks FUN_00529c10's attract loop,
            // letting startup continue to engine initialization instead of attracting forever.
            if (player.TitleBitmap == null) return 1;

            player.TitleBitmapLoaded = 1;
        }

        // ---------------------------------------------------------------------------------------
        // Deliberately not reconstructed — the rest of FUN_00542c40 (Blaze.c:89916-89975):
        //
        //   * `*(param_1+4) = 1` then a StretchDIBits blit of the 640x480 bitmap through the
        //     DirectDraw wrapper's own vtable — GetDC at offset 0x44, ReleaseDC at 0x68 on the
        //     inner object at *(param_1+0x1c)+4. Both calls are null-checked in the original, so
        //     this part would in fact degrade safely.
        //   * joySetCapture on both joysticks, then a message-pump loop that exits with 1 on
        //     WM_KEYDOWN (0x100), WM_LBUTTONDOWN (0x201), WM_RBUTTONDOWN (0x204),
        //     WM_MBUTTONDOWN (0x207), MM_JOY1BUTTONDOWN (0x3b5) or MM_JOY2BUTTONDOWN (0x3b6) —
        //     freeing the bitmap on the way out — re-blits on WM_TIMER (0x111, lParam 1), raises
        //     the fatal exception on WM_QUIT (0x12), and exits with 0 once timeGetTime shows more
        //     than 19999 ms elapsed since the last blit.
        //
        // Reason: none of that can receive input without a real main window and WNDPROC, which do
        // not exist yet (REVERSE_STATUS.md TODO #6 and #14).
        //
        // Why this throws instead of returning 0: reaching here means the title bitmap loaded, and
        // at that point the attract loop in RunMainLoop has **no reachable exit at all**. Its four
        // conditions are this function's two calls — which can only report "stop attracting" on a
        // load failure or on user input — and two PlayMovie results, which are pinned at 0 while
        // TrySetupMovieStream is a stub. Returning 0 spins the process at 100% CPU forever with no
        // message pump; that is precisely the silent misbehaviour this codebase throws to avoid,
        // and it is how the pre-CD version of this bug presented.
        //
        // Note the degradation is targeted: with no CD the load fails, this function returns 1
        // above, the loop exits on iteration 1 and startup proceeds normally. Only the
        // CD-present path — where the original would show the title screen and wait for a
        // keypress — stops here, which is exactly where the missing capability actually bites.
        // ---------------------------------------------------------------------------------------
        throw new NotImplementedException(
            "FUN_00542c40: the attract-mode title screen loaded DATA/TITLE640.BMP but cannot wait " +
            "for input — the 20s timeGetTime wait and its key/mouse/joystick handling need a real " +
            "main window and WNDPROC (REVERSE_STATUS.md TODO #6/#14). Without them the attract " +
            "loop in RunMainLoop has no reachable exit, since its other two exits depend on " +
            "PlayMovie, stubbed at 0 by TrySetupMovieStream (TODO #13).");
    }

    /// <summary>Ghidra: FUN_00542f50 @ 0x00542f50 -> FUN_005428d0 — closes the stream if one is open.</summary>
    private static void CloseMovieStreamIfOpen()
    {
        if (s_MoviePlayer != null) CloseMovieStream(s_MoviePlayer);
    }

    /// <summary>
    /// Ghidra: FUN_00542a70 @ 0x00542a70 — opens the underlying video stream on the just-created
    /// window and arms the 100ms per-frame timer. TODO(REVERSE): entirely built out of calls
    /// through the unrecoverable custom "stream"/DirectDraw-wrapper vtables (see banner above);
    /// always reports failure here, matching how the original already handles a failed open.
    /// </summary>
    private static bool TrySetupMovieStream(MoviePlayerState player)
        => false;

    /// <summary>
    /// Ghidra: FUN_00542b70 @ 0x00542b70 — plays the stream opened by TrySetupMovieStream, pumping
    /// window messages (and raising the original's WM_QUIT-triggered fatal exception faithfully)
    /// until playback finishes, with joystick input captured for skip/cancel for the duration.
    /// </summary>
    private static int PlayMoviePumpLoop(MoviePlayerState player, byte* path)
    {
        // Ghidra: lstrcpyA(param_1[6] + 0xc, param_2) — copies into the (unrecoverable) stream
        // object's own filename buffer; nothing to copy into without that object, so skipped.
        player.Result = 0;

        joySetCapture(player.HWnd, 0, 0x32, 1);
        joySetCapture(player.HWnd, 1, 0x32, 1);

        if (TrySetupMovieStream(player))
        {
            // HAZARD — do not enable TrySetupMovieStream without also signalling completion.
            // This loop terminates only when `player.Playing` is cleared, and the sole thing that
            // clears it in the original is a callback through the movie stream object's vtable,
            // whose slots have no decompiled body anywhere in Blaze.c (REVERSE_STATUS.md TODO #13).
            // The loop is currently unreachable *only* because TrySetupMovieStream is a stub
            // returning false. Making that stub return true before the completion signal exists
            // re-hangs startup — this time in a tight PeekMessage spin rather than a bare one.
            // This is the same defect class that TODO #31 records for the attract loop.
            player.Playing = true;
            MSG msg;
            do
            {
                while (PeekMessageA(&msg, IntPtr.Zero, 0, 0, 1 /* PM_REMOVE */) != 0)
                {
                    if (msg.message == 0x12 /* WM_QUIT */)
                    {
                        RaiseException(0xe0000001, 0, 0, null);
                    }
                    DispatchMessageA(&msg);
                }
            } while (player.Playing);
        }

        joyReleaseCapture(0);
        joyReleaseCapture(1);
        return player.Result;
    }

    /// <summary>
    /// Ghidra: FUN_00542eb0 @ 0x00542eb0. Builds "&lt;cd-drive-root&gt;&lt;movieFileName&gt;" and plays it.
    /// `flag` is passed through from RunMainLoop but only reaches the (unrecoverable) stream vtable.
    /// </summary>
    private static int PlayMovie(byte* movieFileName, int flag)
    {
        if (s_MoviePlayer == null) return 0;

        byte* path = stackalloc byte[64];
        int i = 0;
        for (byte* p = s_CdDataRootPath; *p != 0 && i < 60; p++, i++) path[i] = *p;
        for (byte* p = movieFileName; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;

        return PlayMoviePumpLoop(s_MoviePlayer, path);
    }

    // Ghidra: s_TEPS_AVI_005684f4 / s_OPENING_AVI_005684e8 / s_ANI01_AVI_005684dc — only Ghidra's
    // auto-derived symbol names survive in Blaze.c, not the literal bytes; best-guess filenames.
    private static readonly byte* s_MovieNameTeps = ToAnsiZ("TEPS.AVI");
    private static readonly byte* s_MovieNameOpening = ToAnsiZ("OPENING.AVI");
    private static readonly byte* s_MovieNameAni01 = ToAnsiZ("ANI01.AVI");

    // =====================================================================
    // RunMainLoop and its remaining direct dependencies
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00504460 @ 0x00504460. Registers, then immediately unregisters, a set of 8
    /// winsys3d.dll "handler" slots (Ghidra: Ordinal_6/Ordinal_10 — see winsys3d.c FUN_00a040d0/
    /// FUN_00a04220, a generic handler-table used elsewhere for DPMI/legacy-interrupt-style
    /// callbacks), then sets up a joystick device via FUN_00523120 before resetting joystick
    /// state (FUN_00504b10). TODO(REVERSE): the 8 callback targets (&LAB_00504610.._680) have no
    /// decompiled bodies in Blaze.c, matching the ordinal-790/&LAB_00512ef0 situation elsewhere;
    /// passed through as null. FUN_00523120 (DirectInput joystick device creation, tied to the
    /// main window handle) has its own unread dependency chain (FUN_00522dc0/eb0/e40) — next
    /// frontier, see REVERSE_STATUS.md.
    /// </summary>
    private static void SetupJoystickHandlers()
    {
        void* h1 = Ordinal_6(0xf4000001, 4, 0x1000, null);
        void* h2 = Ordinal_6(0xf4000001, 0x8000, 0x1000, null);
        void* h3 = Ordinal_6(0xf4000001, 0x100, 0x1000, null);
        void* h4 = Ordinal_6(0xf4000001, 0x2000, 0x1000, null);
        void* h5 = Ordinal_6(0xf0000011, 4, 0x1000, null);
        void* h6 = Ordinal_6(0xf0000011, 0x8000, 0x1000, null);
        void* h7 = Ordinal_6(0xf0000011, 0x100, 0x1000, null);
        void* h8 = Ordinal_6(0xf0000011, 0x2000, 0x1000, null);
        Ordinal_41();
        Ordinal_10(h1); Ordinal_10(h2); Ordinal_10(h3); Ordinal_10(h4);
        Ordinal_10(h5); Ordinal_10(h6); Ordinal_10(h7); Ordinal_10(h8);

        // Ghidra: DAT_00604b54 = FUN_00523120(0, DAT_005db85c, &DAT_00604b50, 0, 0); DAT_005db85c
        // is the not-yet-reconstructed main window HWND (see REVERSE_STATUS.md item on it).
        // TODO(REVERSE): FUN_00523120 (DirectInput joystick device creation) not yet reconstructed —
        // next frontier. Skipped here rather than guessed.
        Ordinal_63(0);
        Ordinal_64();
        Ordinal_66();
        JoystickResetState(true);
    }

    /// <summary>
    /// Ghidra: FUN_00504b10 @ 0x00504b10 — resets the 9-slot joystick binding/state table.
    /// `clearName` mirrors the original's `param_1 != 0` guard around one extra field
    /// (`puVar1[1] = 0`) that's cleared only on some call sites — exact field meaning unconfirmed.
    /// </summary>
    private static void JoystickResetState(bool clearName)
    {
        for (int i = 0; i < s_JoystickState.Length; i++)
        {
            s_JoystickState[i] = new JoystickSlotState
            {
                AxisOrButton = 0xffff,
                Name = clearName ? (ushort)0 : s_JoystickState[i].Name,
            };
        }
    }

    private struct JoystickSlotState
    {
        public ushort AxisOrButton;
        public ushort Name; // Ghidra: puVar1[1] — TODO(REVERSE) exact field meaning unconfirmed
    }

    // Ghidra: &DAT_00604a28-relative 9-entry table walked by FUN_00504b10.
    private static readonly JoystickSlotState[] s_JoystickState = new JoystickSlotState[9];

    /// <summary>
    /// Ghidra: FUN_0050f120 @ 0x0050f120. Reads a capability flag off the main device interface
    /// (Ordinal_750) via a vtable call at offset 0x40. TODO(REVERSE): exact meaning of that vtable
    /// slot not confirmed (likely a "hardware texture filtering supported"-style caps query).
    /// </summary>
    private static void CheckDeviceCapabilityFlag()
    {
        void* device = Ordinal_750();
        if (device == null) { s_DeviceCapFlag_00564a48 = true; return; }
        void** vtable = *(void***)device;
        var query = (delegate* unmanaged[Stdcall]<void*, int>)vtable[0x10];
        int result = query(device);
        s_DeviceCapFlag_00564a48 = result == 0;
    }

    // Ghidra: DAT_00564a48 — boolean capability flag set by CheckDeviceCapabilityFlag.
    private static bool s_DeviceCapFlag_00564a48;

    /// <summary>
    /// Ghidra: FUN_00529e40 @ 0x00529e40 — pushes a fixed "shading defaults" struct into
    /// winsys3d.dll via ordinals 378/368. Field semantics beyond their literal values are unknown.
    /// </summary>
    private static void SetShadingDefaults()
    {
        // Ghidra: uStack_28=0xa0, uStack_24=0x78, then 5 (short,short) pairs of (64000, 0x600),
        // laid out as 8 little-endian dwords consumed by Ordinal_368 below.
        uint* buf = stackalloc uint[8];
        buf[0] = 0xa0;
        buf[1] = 0x78;
        buf[2] = 0x0600fa00; // (local_1e=0x600, local_20=64000=0xfa00)
        buf[3] = 0x0600fa00; // (local_1a, local_1c)
        buf[4] = 0x0600fa00; // (local_16, local_18)
        buf[5] = 0x0600fa00; // (local_12, local_14)
        buf[6] = 0x0000fa00; // (padding, local_10)
        buf[7] = 0;          // local_c

        Ordinal_378(0, 0); // TODO(REVERSE): Ordinal_378 takes no visible args at this call site in
                            // Blaze.c beyond an implicit/optimized-out pair; passed (0,0) as a
                            // faithful placeholder — see winsys3d.md.
        Ordinal_368(buf);
    }

    /// <summary>
    /// Ghidra: FUN_00529eb0 @ 0x00529eb0 — initializes several on-screen viewport/surface
    /// descriptors (via ordinals 658/661) and either fills the main render buffer with a
    /// checkerboard pattern + tiled blit (ordinals 674/667), or clears it to black (ordinals
    /// 665/667), depending on DAT_005633a8 (an unrecovered global — TODO(REVERSE) exact meaning).
    /// </summary>
    private static void InitViewportSurfaces()
    {
        // Ghidra renders these four calls with their arguments spilled to unattributed stack slots
        // (uStack_18..puStack_50) rather than as call arguments, and gives the fourth only three of
        // its five. Reading the slots back in ascending address order — the cdecl push order — and
        // cross-checking against the DLL bodies recovers all four completely:
        //
        //   Ordinal_658(&DAT_005e98dc, 0, 0xf0, 0x140, 0xf0)   disp env 0 : (0,240,320,240)
        //   Ordinal_658(&DAT_005e994c, 0, 0,    0x140, 0xf0)   disp env 1 : (0,  0,320,240)
        //   Ordinal_661(&DAT_005e9880, 0, 0,    0x140, 0xf0)   draw env 0 : (0,  0,320,240)
        //   Ordinal_661(&DAT_005e98f0, 0, 0xf0, 0x140, 0xf0)   draw env 1 : (0,240,320,240)
        //
        // i.e. the standard PSX double-buffer setup: while one buffer is displayed at y=0 the other
        // is drawn at y=240, and vice versa. 0x140/0xf0 are 320x240.
        //
        // The previous reconstruction had the argument order rotated, passed stack scratch buffers
        // instead of the four globals, and passed **null** for the fourth — which is what made
        // winsys3d.dll write through a null pointer and take down the process.
        SetDefDispEnv(s_DispEnv0_005e98dc, 0, 0xf0, 0x140, 0xf0);
        SetDefDispEnv(s_DispEnv1_005e994c, 0, 0, 0x140, 0xf0);
        SetDefDrawEnv(s_DrawEnv0_005e9880, 0, 0, 0x140, 0xf0);
        SetDefDrawEnv(s_DrawEnv1_005e98f0, 0, 0xf0, 0x140, 0xf0);

        // Ghidra: the 12 global stores that follow the calls. Each one lands inside one of the four
        // structs above at an offset that matches the DISPENV/DRAWENV layout exactly — which is the
        // independent confirmation that the identification is right. They override some of what the
        // two DLL helpers just defaulted.
        //
        // _DAT_005e98e4/_e6/_e8/_ea = disp env 0's `screen` rect  (0,0,320,240) — replaces the
        // 0x100-wide default SetDefDispEnv writes.
        *(short*)(s_DispEnv0_005e98dc + 0x08) = 0;
        *(short*)(s_DispEnv0_005e98dc + 0x0a) = 0;
        *(short*)(s_DispEnv0_005e98dc + 0x0c) = 0x140;
        *(short*)(s_DispEnv0_005e98dc + 0x0e) = 0xf0;

        // _DAT_005e9954/_56/_58/_5a = disp env 1's `screen` rect, same values.
        *(short*)(s_DispEnv1_005e994c + 0x08) = 0;
        *(short*)(s_DispEnv1_005e994c + 0x0a) = 0;
        *(short*)(s_DispEnv1_005e994c + 0x0c) = 0x140;
        *(short*)(s_DispEnv1_005e994c + 0x0e) = 0xf0;

        // DAT_005e9896 / _DAT_005e9898 and DAT_005e9906 / _DAT_005e9908 = each draw env's `dtd`
        // (dither, offset 0x16 — already 1 from SetDefDrawEnv, so this store is a no-op) and `isbg`
        // (offset 0x18 — flipped from 0 to 1, i.e. clear the framebuffer to the background colour
        // before drawing). Ghidra shows the isbg stores as 2-byte writes overlapping the adjacent
        // `r0` byte; r0 was just zeroed by SetDefDrawEnv, so a byte store is equivalent here.
        *(s_DrawEnv0_005e9880 + 0x16) = 1;
        *(s_DrawEnv0_005e9880 + 0x18) = 1;
        *(s_DrawEnv1_005e98f0 + 0x16) = 1;
        *(s_DrawEnv1_005e98f0 + 0x18) = 1;

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return; // TODO(REVERSE): winsys3d.dll's MainMem not yet resolvable in this environment

        if (s_UnknownModeFlag_005633a8 == 0)
        {
            // Ghidra: two runs of `*(undefined4*)(mainMem + 4 + off) = 0x80008000;` — a checkerboard fill.
            for (int off = 0x1c3ffc - 4; off >= 0x1c1ffc; off -= 4)
                *(uint*)(mainMem + 4 + off) = 0x80008000;
            for (int off = 0x1b7ffc - 4; off >= 0x1afffc; off -= 4)
                *(uint*)(mainMem + 4 + off) = 0x80008000;

            uint frameOffset = 0;
            short* tileRect = stackalloc short[14];
            for (int i = 0; i < 0x20; i++)
            {
                Ordinal_674(tileRect, mainMem + 0x1b0000);
                Ordinal_667(0);
                frameOffset += 0x40;
                if (frameOffset == 0x400) frameOffset = 0;
            }
        }
        else
        {
            short* rect = stackalloc short[14];
            Ordinal_665(rect, 0, 0, 0);
            Ordinal_667(0);
        }
    }

    // The four global libgpu environment structs InitViewportSurfaces sets up (Ghidra:
    // DAT_005e9880 / DAT_005e98dc / DAT_005e98f0 / DAT_005e994c). Their **addresses prove their
    // sizes**: consecutive structs sit 92 and 20 bytes apart, alternating, which is exactly
    // sizeof(DRAWENV) and sizeof(DISPENV) in the PlayStation SDK's libgpu:
    //
    //   0x005e9880  DRAWENV  92 bytes   draw env 0
    //   0x005e98dc  DISPENV  20 bytes   disp env 0
    //   0x005e98f0  DRAWENV  92 bytes   draw env 1
    //   0x005e994c  DISPENV  20 bytes   disp env 1   (ends 0x005e9960)
    //
    // DISPENV = { RECT disp; RECT screen; u_char isinter, isrgb24, pad0, pad1; }
    // DRAWENV = { RECT clip; short ofs[2]; RECT tw; u_short tpage; u_char dtd, dfe, isbg,
    //             r0, g0, b0; ... }  with RECT = { short x, y, w, h; }
    //
    // Allocated zeroed, matching how this file models other DAT_ globals that are really structs.
    // They must be process-lifetime globals, not stack buffers: the not-yet-reconstructed rendering
    // code reads them back on every frame.
    private const int DrawEnvSize = 92;
    private const int DispEnvSize = 20;
    private static readonly byte* s_DrawEnv0_005e9880 = (byte*)NativeMemory.AllocZeroed(DrawEnvSize);
    private static readonly byte* s_DispEnv0_005e98dc = (byte*)NativeMemory.AllocZeroed(DispEnvSize);
    private static readonly byte* s_DrawEnv1_005e98f0 = (byte*)NativeMemory.AllocZeroed(DrawEnvSize);
    private static readonly byte* s_DispEnv1_005e994c = (byte*)NativeMemory.AllocZeroed(DispEnvSize);

    // Ghidra: DAT_005633a8 — read pervasively as a mode flag (movie-intro skip / safe-mode?);
    // TODO(REVERSE): exact meaning not confirmed, no assignment site reached yet from RunMainLoop.
    private static int s_UnknownModeFlag_005633a8;

    // Ghidra: DAT_005631f8 — the game's **data root** on the CD. Recovered from Legacy/Blaze.exe at
    // VA 0x005631f8: the bytes are "@:\FRENCH\", NOT the bare drive root this was previously assumed
    // to be. See Documentation/DataSymbols.md.
    //
    // '@' (0x40, 'A'-1) is the drive-letter placeholder; RunApplication's scan writes the detected
    // letter into index 0 and resets it to '@' when nothing matches. Only index 0 is ever patched —
    // the language directory is static data, which is exactly why Blaze.c's text (code only, no
    // .data) showed a single-byte store and gave no hint that the rest of the string existed.
    //
    // This is a distinct buffer from the CD *presence marker* path 16 bytes earlier
    // (s____Blaze_Blade_005631e8 = "@:\Blaze&Blade"). Conflating the two is what made every
    // CD-relative path resolve one directory too high and find nothing.
    private static readonly byte* s_CdDataRootPath = ToAnsiZ("@:\\FRENCH\\");

    /// <summary>
    /// Ghidra: FUN_00529c10 @ 0x00529c10 — Blaze &amp; Blade's own "RunMainLoop": plays the four
    /// intro movies (gracefully skipped if the movie subsystem can't be opened, exactly like the
    /// original's own handling of a failed open), re-initializes engine/device state, loads the
    /// UI font from the CD, sets up the viewport, and finally hands off to the real per-frame game
    /// loop (FUN_0052a3d0).
    /// </summary>
    private static void RunMainLoop()
    {
        InitMoviePlayer();
        bool streamOpened = TryOpenMovieStream();
        if (streamOpened)
        {
            if (PlayMovie(s_MovieNameTeps, 1) == 0)
            {
                PlayMovie(s_MovieNameOpening, 3);
            }

            // Ghidra: Blaze.c:74441-74451. The attract loop has FOUR exit conditions, one per step —
            // title screen, ANI01, title screen, OPENING — and every one of them must stay, even
            // while a step is degraded. Dropping the two ShowTitleScreenAndWait checks (Ghidra types
            // that thunk `void`, but the call site reads its result) is what previously made this
            // loop unbreakable, since PlayMovie always reports 0 while TrySetupMovieStream is a stub.
            while (true)
            {
                if (ShowTitleScreenAndWait() != 0) break;
                if (PlayMovie(s_MovieNameAni01, 3) != 0) break;
                if (ShowTitleScreenAndWait() != 0) break;
                if (PlayMovie(s_MovieNameOpening, 3) != 0) break;
            }
        }
        CloseMovieStreamIfOpen();

        Ordinal_129();
        Ordinal_41();
        Ordinal_45(null, s_CdDataRootPath); // Ghidra: apcStack_54[1]="j" — TODO(REVERSE): odd literal, but Ordinal_45 is a confirmed no-op regardless
        Ordinal_136(0);
        s_Flag_0056e9c0 = 1; // Ghidra: FUN_00547130(1)
        s_Unknown_005e9630 = Ordinal_116();
        Ordinal_771();
        InitTrigTables(); // Ordinal_337, re-run (idempotent — see winsys3d.md)
        Ordinal_789(0, 0); // Ghidra: Ordinal_789(0); 2nd arg TODO(REVERSE), inert when 1st arg is 0
        ShutdownMoviePlayer(); // Ghidra: FUN_00542f90()
        CheckDeviceCapabilityFlag(); // Ghidra: FUN_0050f120()
        Ordinal_110(0);
        Ordinal_681(0);
        SetShadingDefaults(); // Ghidra: FUN_00529e40()
        InitViewportSurfaces(); // Ghidra: FUN_00529eb0()
        // Ghidra: FUN_0040a600() — confirmed no-op (`return;` with an empty body).
        SetupJoystickHandlers(); // Ghidra: FUN_00504460()

        int detail = s_UnknownDetailSetting_005de160;
        SetShift((0xb - detail) * 0x10, (0xb - detail) * 0xc, detail, 0);

        LoadUiFontFromCd();

        Ordinal_710(0x3c0, 0);
        int consoleChannel = Ordinal_668(8, 0x20, 0xe0, 200, 0, 0x200);
        Ordinal_720(consoleChannel);
        Ordinal_92(null, null);
        s_Unknown_005e97f0 = Ordinal_104(null);
        s_ScreenStride_005e97e0 = 0x1010;
        byte* mainMem = GetMainMemPtr();
        s_MainResourceBuffer_005e97c0 = mainMem != null ? mainMem + 0xe0000 : null;
        s_Unknown_005e9820 = 0;

        Ordinal_132(0);
        Ordinal_712(1);

        LoadFirstLevelResources(); // Ghidra: FUN_0052a030()
        RunGameLoop();            // Ghidra: FUN_0052a3d0()

        Ordinal_138();
    }

    // Ghidra: DAT_0056e9c0 (set by FUN_00547130) / _DAT_005e9630 (set by the confirmed-no-op
    // Ordinal_116, always 0) / DAT_005e97f0 (set by the confirmed-no-op Ordinal_104, always 0) /
    // DAT_005e97e0 (screen stride/pitch?) / _DAT_005e9820 — all read again by not-yet-reconstructed
    // rendering code.
    private static int s_Flag_0056e9c0;
    private static uint s_Unknown_005e9630;
    private static uint s_Unknown_005e97f0;
    private static uint s_ScreenStride_005e97e0;
    // Ghidra: DAT_005e97c0 — renamed from an earlier "screen/back-buffer base" guess once
    // FUN_0052a030 (LoadFirstLevelResources) showed it's really the destination buffer for the
    // top-level "<cd-drive-root>SYSTEM.INI"-equivalent resource-file load, immediately after
    // RunMainLoop sets it to `mainMem + 0xe0000` here — a fixed scratch address inside winsys3d's
    // arena reserved for that blob (LoadFirstLevelResources then treats it as a struct: offsets
    // 0x10/0x48/0x6c/100 are read back as sub-tables/fields once loaded).
    private static byte* s_MainResourceBuffer_005e97c0;
    private static int s_Unknown_005e9820;

    /// <summary>
    /// Ghidra: FUN_00547130 @ 0x00547130 — trivial setter, folded directly into RunMainLoop above
    /// (`s_Flag_0056e9c0 = 1`) rather than kept as a separate one-line method.
    /// </summary>

    /// <summary>
    /// Ghidra: the font-loading sequence inlined at the end of FUN_00529c10 (no separate function —
    /// builds "&lt;cd-drive-root&gt;DATA\FONT.BMP"-style path via string concatenation, opens it with
    /// Ordinal_331/333/334/336, which are the same thin CreateFileA/SetFilePointer/ReadFile/
    /// CloseHandle wrappers already confirmed for OpenExistingFileReadOnly elsewhere — called via
    /// real Win32 here for consistency with that precedent — reads a chunk into the engine's main
    /// memory arena, then builds a font/glyph table via ordinals 92/104/132/712/720 and the two
    /// FUN_0052a030/FUN_0052a3d0 hand-off below).
    /// </summary>
    private static void LoadUiFontFromCd()
    {
        // Ghidra: s_DATA_Font_bmp_005684cc — only the derived symbol name survives; best guess below.
        byte* fontFileName = ToAnsiZ("DATA/Font.bmp"); // recovered verbatim from Legacy/Blaze.exe @ 0x005684cc — forward slash, mixed case
        byte* path = stackalloc byte[64];
        int i = 0;
        for (byte* p = s_CdDataRootPath; *p != 0 && i < 60; p++, i++) path[i] = *p;
        for (byte* p = fontFileName; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;

        IntPtr hFile = OpenExistingFileReadOnly(path);
        if ((int)hFile < 0) return; // TODO(REVERSE): original doesn't appear to guard this open failing either

        SetFilePointer(hFile, 0x3e, null, 0 /* FILE_BEGIN */);
        byte* mainMem = GetMainMemPtr();
        if (mainMem != null)
        {
            uint bytesRead;
            ReadFile(hFile, mainMem + 0x1ad8f0, 0xd48, &bytesRead, null);
        }
        CloseHandle(hFile);
    }

    // =====================================================================
    // Resource-file loader subsystem (Ghidra: FUN_00529b00/FUN_00529b50/FUN_00532070/FUN_005322f0/
    // FUN_0052a660), used by LoadFirstLevelResources (FUN_0052a030) below. Everything here loads
    // from "BLAZE.ALL", a monolithic resource archive on the CD (sector-addressed — offsets are in
    // 2048-byte units, confirmed by the `<< 0xb` shift in LoadResourceEntry), plus a couple of
    // standalone files ("SYSTEM.INI"-equivalent, "BASE.SND"-equivalent) loaded whole.
    // =====================================================================

    // Ghidra: PTR_s_BLAZE_ALL_1_00566bbc -> 0x005684b0, PTR_s_SYSTEM_INI_1_00566bb8 -> 0x005684bc.
    // Recovered verbatim from Legacy/Blaze.exe — the `;1` is an ISO-9660 file version, and Ghidra's
    // "_1_" in the symbol names was echoing it all along. Keep the suffix: NormalizePath truncates
    // at ';' before the open, exactly as winsys3d.dll does. See Documentation/DataSymbols.md.
    private static readonly byte* s_ArchiveFileName = ToAnsiZ("BLAZE.ALL;1");
    private static readonly byte* s_SystemFileName = ToAnsiZ("SYSTEM.INI;1");

    // Ghidra: s____snd_Base_snd_00568500, recovered as "@:\snd\Base.snd". A CD-**root** absolute
    // template with its own '@' drive-letter placeholder — it does NOT go through the data root.
    // Lowercase "snd", matching the disc's E:\Snd\ directory (Windows is case-insensitive here).
    private static readonly byte* s_SoundArchiveTemplate = ToAnsiZ("@:\\snd\\Base.snd");

    /// <summary>
    /// Builds a **data-root-relative** path: the data root (Ghidra DAT_005631f8, "@:\FRENCH\" with
    /// its drive letter patched) followed by a relative filename. This is the family that covers
    /// BLAZE.ALL;1, SYSTEM.INI;1, DATA/Font.bmp, DATA/TITLE640.BMP and the three intro movies.
    /// <para>
    /// It is <b>not</b> the only family — see <see cref="BuildCdRootPath"/> for templates that are
    /// already absolute and address the CD root instead.
    /// </para>
    /// </summary>
    private static byte* BuildCdPath(byte* fileName)
    {
        byte* path = (byte*)Malloc(64);
        int i = 0;
        for (byte* p = s_CdDataRootPath; *p != 0 && i < 60; p++, i++) path[i] = *p;
        for (byte* p = fileName; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;
        return path;
    }

    /// <summary>
    /// Builds a **CD-root** path from a template that is already absolute and carries its own '@'
    /// drive-letter placeholder — e.g. "@:\snd\Base.snd" (Ghidra: s____snd_Base_snd_00568500) and
    /// the music table's "@:/Music/Mel00_01.WAV" entries. Only index 0 is substituted; the rest of
    /// the template is copied verbatim, separators and case included.
    /// <para>
    /// These live at the CD root, not under the language directory, which the disc layout confirms:
    /// <c>E:\snd\</c> and <c>E:\Music\</c> sit beside <c>E:\French\</c> rather than inside it. Running
    /// them through <see cref="BuildCdPath"/> would prepend the data root and never resolve.
    /// </para>
    /// </summary>
    private static byte* BuildCdRootPath(byte* absoluteTemplate)
    {
        byte* path = (byte*)Malloc(64);
        int i = 0;
        for (byte* p = absoluteTemplate; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;
        if (i > 0) path[0] = s_CdDataRootPath[0]; // the detected drive letter
        return path;
    }

    /// <summary>
    /// Ghidra: FUN_0052a660 @ 0x0052a660 — checks whether `dest` is one of three well-known
    /// "class override table" destinations; if so, substitutes hardcoded native function-pointer
    /// tables instead of loading resource-file bytes into it. TODO(REVERSE): none of the three
    /// comparison targets (Ghidra: DAT_006008c8/DAT_00604930/DAT_00600904) have an assignment site
    /// anywhere in the reconstructed call graph yet, so they're always null here and every real
    /// call site's `dest` (always a valid non-null scratch-buffer pointer) can never match — this
    /// always returns false in the current reconstruction. The three true-branch bodies (the
    /// DAT_00600904 one alone pulls in 11 further FUN_ calls, presumably per-entity-class native
    /// vtable installers) are out of scope for this pass and stubbed loudly rather than guessed.
    /// </summary>
    private static bool IsKnownOverrideDestination(void* dest)
    {
        if (dest == s_OverrideDestA_006008c8) return true; // Ghidra: no body at all for this case — just `return 1;`
        if (dest == s_OverrideDestB_00604930)
            throw new NotImplementedException("FUN_0052a660: DAT_00604930 override-table branch not reconstructed — see REVERSE_STATUS.md");
        if (dest == s_OverrideDestC_00600904)
            throw new NotImplementedException("FUN_0052a660: DAT_00600904 override-table branch (+11 dependent FUN_ calls) not reconstructed — see REVERSE_STATUS.md");
        return false;
    }

    // Ghidra: DAT_006008c8 / DAT_00604930 / DAT_00600904 — never assigned anywhere reached yet.
    private static void* s_OverrideDestA_006008c8;
    private static void* s_OverrideDestB_00604930;
    private static void* s_OverrideDestC_00600904;

    /// <summary>
    /// Ghidra: FUN_00529b00 @ 0x00529b00. Opens `path`, reads the whole file into `dest`, closes
    /// it, and returns the next dword-aligned address past the loaded data (bump-allocator style —
    /// matches LoadResourceEntry's own return convention below). TODO(REVERSE): Ghidra's own
    /// decompilation of this function is incomplete — the failure path returns `unaff_ESI` (a
    /// register Ghidra could not attribute a value to at function entry) and the read call shows a
    /// literal `2` where a destination-buffer argument would be expected
    /// (`Ordinal_334(iVar1,2,iVar2)`), one parameter short of the two arguments every real call
    /// site actually passes. Reconstructed here using `dest` as that argument (the only reading
    /// consistent with both call sites) and 0 as a safe/explicit failure return, not Ghidra's
    /// unrecoverable `unaff_ESI`.
    /// </summary>
    private static byte* LoadWholeFileToBuffer(byte* path, byte* dest)
    {
        IntPtr handle = OpenExistingFileReadOnly(path);
        if ((int)handle < 0) return null; // TODO(REVERSE): see summary above — original's real failure value unrecoverable

        uint size = SetFilePointer(handle, 0, null, 2 /* FILE_END */);
        SetFilePointer(handle, 0, null, 0 /* FILE_BEGIN */);
        if (dest != null)
        {
            uint bytesRead;
            ReadFile(handle, dest, size, &bytesRead, null);
        }
        CloseHandle(handle);

        return (byte*)(((nint)dest + size + 5) & ~3);
    }

    /// <summary>
    /// Ghidra: FUN_00529b50 @ 0x00529b50. Loads one sector-addressed chunk (`entryPtr[0]` = sector
    /// offset, `entryPtr[1]` = byte size) out of BLAZE.ALL into `dest`, unless `dest` is one of the
    /// IsKnownOverrideDestination special cases. Returns the next dword-aligned address past the
    /// loaded data.
    /// </summary>
    private static byte* LoadResourceEntry(int* entryPtr, byte* dest)
    {
        if (IsKnownOverrideDestination(dest)) return dest;

        byte* path = BuildCdPath(s_ArchiveFileName);
        IntPtr handle = OpenExistingFileReadOnly(path);
        Free(path);
        if ((int)handle < 0)
        {
            // Ghidra: `/* WARNING: Do nothing block with infinite loop */ do {} while (true);` —
            // preserved faithfully rather than silently changed to a graceful failure; the original
            // hangs here instead of continuing with an invalid handle.
            while (true) { }
        }

        SetFilePointer(handle, entryPtr[0] << 0xb, null, 0 /* FILE_BEGIN */);
        int size = entryPtr[1];
        uint bytesRead;
        ReadFile(handle, dest, (uint)size, &bytesRead, null);
        CloseHandle(handle);

        return (byte*)(((nint)dest + size + 3) & ~3);
    }

    // Ghidra: DAT_0056aca8 — a ushort-driven schema table describing which fields of a loaded
    // resource blob need base-relative-to-absolute pointer fixups (see FixupResourceHeader below).
    // TODO(REVERSE): data-only global, byte content not present in Blaze.c's text; left empty, so
    // the schema-driven second fixup pass below is a safe (but incomplete) no-op — same category of
    // gap as s_ResolutionDetailLimit elsewhere in this file.
    // Ghidra: DAT_0056aca8 — recovered from Legacy/Blaze.exe, 37 ushorts ending at the 0x0000 the
    // original's loop terminates on. The terminator is NOT stored here; the loop below runs to the
    // array's length instead, which is equivalent and keeps the sentinel out of the data.
    //
    // Each entry packs two fields, and this is confirmed by FUN_00532070's own masking rather than
    // inferred: the **low byte is an index** into the header's already-relocated pointer array
    // (`header[4 + index]`), and the **high byte is the byte stride** used to walk that list.
    // The earlier "(record kind, size)" reading in an older revision of DataSymbols.md was wrong.
    //
    // Every index is below 0x3c, matching the 60 dwords the fixed pass relocates — an internal
    // consistency check the recovered bytes pass.
    private static readonly ushort[] s_ResourceFixupSchema_0056aca8 =
    [
        0x0800, 0x0801, 0x0802, 0x0803, 0x0804, 0x0805, 0x0806, 0x0807,
        0x0408, 0x0409, 0x040a, 0x140b, 0x040e, 0x0410, 0x0411, 0x0414,
        0x0815, 0x0417, 0x0418, 0x0419, 0x041b, 0x041c, 0x081d, 0x041e,
        0x041f, 0x0421, 0x0423, 0x0425, 0x0426, 0x0827, 0x0428, 0x042a,
        0x042b, 0x042d, 0x042f, 0x0430, 0x0431,
    ];

    /// <summary>
    /// Ghidra: FUN_00532070 @ 0x00532070. Turns a just-loaded resource blob's stored
    /// "base-relative" offsets into real pointers, now that its load address is known. Two passes:
    /// a fixed run of 60 header dwords, then a schema-driven walk over the chained lists those
    /// dwords point at.
    /// <para>
    /// The two passes use **different** relocation formulas — the fixed pass adds
    /// <c>header[0]</c>, the schema pass adds <c>header[0] + header[1]</c>. That extra term is not
    /// incidental; dropping it yields pointers that look valid and are wrong by a constant.
    /// </para>
    /// </summary>
    private static void FixupResourceHeader(byte* blob)
    {
        if (blob == null) return;

        // Ghidra: iVar1 = *param_1; the fixed 60-dword pass over blob[4..0x40).
        int origin = *(int*)blob;
        int* field = (int*)blob + 4;
        for (int i = 0; i < 0x3c; i++, field++)
        {
            *field = (int)(nint)blob + *field + origin;
        }

        // Ghidra: iVar3 = param_1[1]; the schema pass's extra relocation term.
        int recordOrigin = ((int*)blob)[1];

        // Ghidra: the `while (uVar5 != 0)` walk over DAT_0056aca8. Each schema entry names one of
        // the pointers the pass above just relocated, plus the stride to walk the list it heads.
        foreach (ushort entry in s_ResourceFixupSchema_0056aca8)
        {
            int listIndex = entry & 0xff;
            int stride = entry >> 8;

            int* node = (int*)(nint)((int*)blob)[4 + listIndex];

            // Ghidra checks both the pointer and its first dword — an empty list is stored as a
            // valid pointer to a zero, not as a null.
            if (node == null) continue;
            int value = *node;
            if (value == 0) continue;

            // Read-then-advance, in the original's order: relocate the current dword, then take the
            // next link from `node + stride`, then step. Reordering these changes which dword ends
            // the walk.
            do
            {
                *node = (int)(nint)blob + value + recordOrigin + origin;
                value = *(int*)((byte*)node + stride);
                node = (int*)((byte*)node + stride);
            } while (value != 0);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005322f0 @ 0x005322f0 — decodes/unpacks one resource chunk (loaded by
    /// LoadResourceEntry into a scratch buffer) into its final destination slot. TODO(REVERSE):
    /// unlike every other function in this file, this one is *not* reconstructable even in
    /// principle from Blaze.c's text — Ghidra's own decompilation leaves several of its inputs as
    /// `unaff_ESI`/`unaff_DI`/`unaff_retaddr` (register-allocated values whose defining
    /// instructions Ghidra never attributed to this function) and its outputs as
    /// `extraout_var`/`extraout_var_00`/`extraout_var_01` (register return values from callees
    /// Ghidra couldn't tie to a real variable). Both real call sites (here and the not-yet-reached
    /// FUN_00542160) pass 3 arguments despite Ghidra recovering only a 1-parameter signature,
    /// confirming the analysis is genuinely incomplete, not just unnamed. Signature widened to the
    /// 3 arguments both call sites actually pass; body intentionally left throwing.
    /// </summary>
    private static void DecodeResourceChunk(byte* scratch, byte* destSlot, int* metadata)
    {
        throw new NotImplementedException(
            "FUN_005322f0: Ghidra's own decompilation is incomplete for this function " +
            "(unaff_ESI/unaff_DI/unaff_retaddr register inputs never resolved) — see REVERSE_STATUS.md");
    }

    // Ghidra: DAT_005e8fc0 — parallel per-entry decode-destination table walked alongside the
    // directory linked list in LoadFirstLevelResources; size unconfirmed, sized generously.
    private static readonly byte* s_EntityDecodeTable_005e8fc0 = (byte*)NativeMemory.AllocZeroed(0x1000);
    // Ghidra: DAT_005e8340 (0x1e0 bytes, read from "BASE.SND" here) immediately followed by
    // DAT_005e8520 (0x88 bytes, read from the level file by FUN_00512xxx @ 0x006335x). They are
    // one contiguous array: 0x005e8520 - 0x005e8340 == 0x1e0 exactly, and PlaySoundEffect indexes
    // *both* halves off &DAT_005e8340 (`&DAT_005e8340 + soundId * 8` for ids up to 0x4c), only
    // switching which sample bank the entry's offset is added to. 0x1e0 + 0x88 == 0x268 == 77
    // entries of 8 bytes, and 0x4c is exactly the last valid index — the sizes corroborate.
    private const int SoundDirectoryBaseCount = 0x1e0 / 8;  // 60 entries, ids 0x00..0x3b
    private const int SoundDirectoryCount = 0x268 / 8;      // 77 entries, ids 0x00..0x4c
    private static readonly SoundDirectoryEntry* s_SoundDirectory =
        (SoundDirectoryEntry*)NativeMemory.AllocZeroed(SoundDirectoryCount * (nuint)sizeof(SoundDirectoryEntry));
    // Ghidra: DAT_005e97e4 / _DAT_005e9810 / _DAT_005e9824 / DAT_005de538 — all read again by
    // not-yet-reconstructed code.
    private static uint s_Unknown_005e97e4;
    private static int s_Unknown_005e9810;
    private static IntPtr s_Unknown_005e9824;
    private static int s_Unknown_005de538;
    // Ghidra: &DAT_005e9180 — small per-slot table reset at the end of FUN_0052a030 (4 entries of
    // 0x4c/76 bytes: 0x13 dwords zeroed, byte[-1]=8, byte[0]=incrementing slot id). Also touched
    // (not yet reconstructed) by FUN_00542160's near-identical loop elsewhere.
    private static readonly byte* s_EntitySlotTable_005e9180 = (byte*)NativeMemory.AllocZeroed(0x140);

    /// <summary>
    /// Ghidra: FUN_0052a030 @ 0x0052a030. Loads the "&lt;cd-drive-root&gt;SYSTEM.INI"-equivalent
    /// level/base resource file, fixes up its internal pointers, decodes each directory entry it
    /// lists (throws at the first one — see DecodeResourceChunk above), loads the base sound
    /// archive's header + big scratch buffer, and resets the small entity-slot table.
    /// </summary>
    private static void LoadFirstLevelResources()
    {
        // Ghidra: FUN_0040a600() — confirmed no-op.
        byte* path = BuildCdPath(s_SystemFileName);
        s_MainResourceDirectory_005e97c4 = LoadWholeFileToBuffer(path, s_MainResourceBuffer_005e97c0);
        Free(path);
        FixupResourceHeader(s_MainResourceBuffer_005e97c0);

        if (s_MainResourceBuffer_005e97c0 != null)
        {
            int* directory = *(int**)(s_MainResourceBuffer_005e97c0 + 0x10);
            int entryPtrValue = *directory;
            if (entryPtrValue != 0)
            {
                int* metadata = directory + 1;
                byte* destSlot = s_EntityDecodeTable_005e8fc0;
                byte* mainMem = GetMainMemPtr();
                do
                {
                    byte* scratch = mainMem != null ? mainMem + 0x1b0000 : null;
                    LoadResourceEntry((int*)(nint)entryPtrValue, scratch);
                    DecodeResourceChunk(scratch, destSlot, metadata); // throws — see summary above
                    entryPtrValue = directory[2];
                    directory += 2;
                    destSlot += 16;
                    metadata += 2;
                } while (entryPtrValue != 0);
            }
        }

        uint savedStride = s_ScreenStride_005e97e0;

        // CD-root family, not data-root: the sound bank lives at E:\Snd\, beside E:\French\ rather
        // than inside it. This previously went through BuildCdPath, which prefixed the data root
        // onto an already-absolute template and could never resolve.
        byte* sndPath = BuildCdRootPath(s_SoundArchiveTemplate);
        IntPtr sndHandle = OpenExistingFileReadOnly(sndPath);
        Free(sndPath);
        // Ghidra doesn't guard the open failing here either — ReadFile/CloseHandle on an invalid
        // handle simply fail harmlessly, matching the original's unconditional calls.
        uint sndBytesRead;
        // The first 60 directory entries, then the whole base sample bank they point into.
        ReadFile(sndHandle, s_SoundDirectory, 0x1e0, &sndBytesRead, null);
        if (s_BaseSoundBank != null)
        {
            ReadFile(sndHandle, s_BaseSoundBank, 790000, &sndBytesRead, null);
        }
        CloseHandle(sndHandle);

        s_Unknown_005e97e4 = savedStride;
        if (s_MainResourceBuffer_005e97c0 != null)
        {
            s_Unknown_005e9810 = *(int*)(s_MainResourceBuffer_005e97c0 + 100);
        }
        s_Unknown_005e9824 = sndHandle;

        byte entryId = 0;
        byte* slotIdByte = s_EntitySlotTable_005e9180 + 9; // Ghidra: &DAT_005e9189
        uint* slotBase = (uint*)s_EntitySlotTable_005e9180; // Ghidra: &DAT_005e9180
        byte* tableEnd = s_EntitySlotTable_005e9180 + (0x5e92b9 - 0x5e9180);
        do
        {
            uint* p = slotBase;
            for (int k = 0; k < 0x13; k++, p++) *p = 0;
            slotIdByte[-1] = 8;
            *slotIdByte = entryId;
            slotIdByte += 0x4c;
            entryId++;
            slotBase += 0x13;
        } while (slotIdByte < tableEnd);

        s_Unknown_005de538 = 0;
    }

    // Ghidra: DAT_005e97c4 — the loaded resource file's own "next free address" bookkeeping value;
    // not yet used further by any reconstructed code past LoadFirstLevelResources itself.
    private static byte* s_MainResourceDirectory_005e97c4;

    /// <summary>
    /// Ghidra: FUN_0052a3d0 @ 0x0052a3d0 — the real, never-returning per-frame game loop. Ticks via
    /// an indirect call through a Blaze-owned function pointer (Ghidra: PTR_FUN_0056c96c) whose
    /// assignment site hasn't been found yet anywhere in the reconstructed call graph, then
    /// dispatches to FUN_0052a440 whenever the game-state id (Ghidra: DAT_005de510) changes away
    /// from 0. The only way out, in the original, is process termination via Exit()/ExitProcess —
    /// there is no normal `return` path, so RunApplication's post-RunMainLoop shutdown sequence is
    /// dead code in practice (still preserved faithfully in RunApplication's `finally` block).
    /// TODO(REVERSE): PTR_FUN_0056c96c's assignment site not found; FUN_0052a440 (the per-state
    /// dispatcher) not yet reconstructed — next frontier after LoadFirstLevelResources above.
    /// </summary>
    private static void RunGameLoop()
    {
        s_GameStateId = 0;
        while (true)
        {
            do
            {
                if (s_MainResourceBuffer_005e97c0 != null)
                {
                    int* perStateTable1 = *(int**)(s_MainResourceBuffer_005e97c0 + 0x48);
                    s_Unknown_005e92b0 = perStateTable1[s_GameStateId];
                    int* perStateTable2 = *(int**)(s_MainResourceBuffer_005e97c0 + 0x6c);
                    int entryPtrValue = perStateTable2[s_GameStateId];
                    // Ghidra: FUN_00529b50(entry, DAT_006008c8) — this destination is always
                    // IsKnownOverrideDestination's first case *by construction* (the call site
                    // passes DAT_006008c8 as both the value being compared and the comparand), so
                    // it's always a no-op here regardless of DAT_006008c8's real (never assigned
                    // in this reconstruction) value.
                    LoadResourceEntry((int*)(nint)entryPtrValue, (byte*)s_OverrideDestA_006008c8);
                }

                s_MainLoopTickCallback();
            } while (s_GameStateId == 0);

            if (s_GameStateId != 7 && s_GameStateId != 9 && s_GameStateId != 2 && s_GameStateId != 3)
            {
                throw new NotImplementedException(
                    "FUN_0052a440 (per-game-state dispatcher) not yet reconstructed — see REVERSE_STATUS.md");
            }
        }
    }

    // Ghidra: DAT_005de510 — current game-state id, shared between RunGameLoop and the tick below.
    private static int s_GameStateId;

    // Ghidra: PTR_FUN_0056c96c — the per-frame tick RunGameLoop calls indirectly. **Resolved**: the
    // pointer's initialised value was read out of Legacy/Blaze.exe as 0x00537CA0, and FUN_00537ca0
    // already has a decompiled body in Blaze.c (line 83364), so no Ghidra re-run was needed. See
    // Documentation/DataSymbols.md.
    //
    // Kept as a function pointer rather than a direct call: the original really does dispatch
    // through a statically initialised .data slot, and preserving that indirection keeps the shape
    // of the code honest. A managed function pointer is enough — there is no native caller.
    private static delegate*<void> s_MainLoopTickCallback = &RunGameStateTick;

    // winsys3d.dll: ReInitializeAllTextures is a named export (called by winsys3d.c itself at
    // winsys3d.c:4119). Zoom8BitTextures is an imported *data* symbol, same category as
    // TexFilterPossible/MainMem — resolved lazily via GetProcAddress, since P/Invoke has no
    // mechanism for imported data.
    [DllImport("winsys3d.dll")] private static extern void ReInitializeAllTextures();

    private static uint* s_Zoom8BitTextures;
    private static uint* GetZoom8BitTexturesPtr()
    {
        if (s_Zoom8BitTextures == null)
        {
            IntPtr module = LoadLibraryA(ToAnsiZ("winsys3d.dll"));
            if (module != IntPtr.Zero)
            {
                s_Zoom8BitTextures = (uint*)GetProcAddress(module, ToAnsiZ("Zoom8BitTextures"));
            }
        }
        return s_Zoom8BitTextures;
    }

    /// <summary>
    /// Ghidra: FUN_00537ca0 @ 0x00537ca0 — the per-frame tick, and the game's real **per-state
    /// dispatcher**. Note this displaces a long-standing assumption in REVERSE_STATUS.md that
    /// FUN_0052a440 played that role; the tick pointer reaches this function instead.
    /// <para>
    /// Every one of the eleven state handlers below **has a decompiled body in Blaze.c** — they are
    /// ordinary uncovered code, not blocked on anything, so each stub names its Ghidra function so
    /// the next state to reconstruct is obvious from the exception alone.
    /// </para>
    /// </summary>
    private static void RunGameStateTick()
    {
        // Ghidra: *(undefined4 *)Zoom8BitTextures_exref = 0;
        uint* zoom = GetZoom8BitTexturesPtr();
        if (zoom != null) *zoom = 0;

        ReInitializeAllTextures();

        switch (s_GameStateId)
        {
            case 0:
                throw new NotImplementedException("FUN_00542130 (game state 0) not yet reconstructed");
            case 1:
                throw new NotImplementedException("thunk_FUN_00536180 (game state 1) not yet reconstructed");
            case 2:
                // Ghidra: FUN_00542160(); DAT_00564a4c = 0; FUN_00537da0(); DAT_00564a4c = 1;
                //         DAT_005de510 = 3;   <- the one case that advances the state itself
                throw new NotImplementedException("FUN_00542160/FUN_00537da0 (game state 2) not yet reconstructed");
            case 3:
                // Ghidra: sets Zoom8BitTextures back to 1 before dispatching.
                throw new NotImplementedException("thunk_FUN_00516020 (game state 3) not yet reconstructed");
            case 4:
                throw new NotImplementedException("FUN_005066d0 (game state 4) not yet reconstructed");
            case 5:
                throw new NotImplementedException("FUN_00504c90 (game state 5) not yet reconstructed");
            case 6:
                throw new NotImplementedException("FUN_00524180 (game state 6) not yet reconstructed");
            case 7:
                throw new NotImplementedException("FUN_00531b40 (game state 7) not yet reconstructed");
            case 8:
                throw new NotImplementedException("FUN_005315d0 (game state 8) not yet reconstructed");
            case 9:
                throw new NotImplementedException("FUN_0051bb00 (game state 9) not yet reconstructed");
            case 10:
                throw new NotImplementedException("FUN_00513ba0 (game state 10) not yet reconstructed");
            default:
                // Ghidra: `uRam00000000 = 1;` — a store to address 0. Not a translation artifact:
                // the original deliberately faults on an unknown state, i.e. this is its assert.
                throw new InvalidOperationException(
                    $"FUN_00537ca0: unknown game state {s_GameStateId}. The original stores to " +
                    "address 0 here, deliberately crashing — reproduced as an exception rather " +
                    "than by writing to a null pointer.");
        }
    }
    // Ghidra: DAT_005e92b0 — per-game-state value read from the loaded resource file's own table
    // at offset 0x48; read again by not-yet-reconstructed code (e.g. FUN_00542160).
    private static int s_Unknown_005e92b0;

    // =====================================================================
    // Not yet reconstructed — large subsystems reached directly from RunApplication.
    // These throw rather than silently no-op so the gap is loud instead of hidden.
    // See REVERSE_STATUS.md for the "next area to explore" notes on each.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00522e40 @ 0x00522e40 — destructor for the 0x8c4-byte joystick/DirectInput
    /// device struct allocated by FUN_00523120 (the 0x8c4 size matches that allocation exactly,
    /// and the final `for (0x231 dwords) *p++ = 0;` zero-fill covers the same 0x8c4 bytes).
    /// Frees 18 owned sub-objects (fields [2], [0x13], and [3..0x12]), releases the underlying
    /// COM device (vtable 0x08 = Release), then zeroes the struct.
    /// </summary>
    private static void DestroyJoystickDevice(void** device)
    {
        // Ghidra: each sub-object is destructed via FUN_005233a0 then freed via FUN_00547210.
        // TODO(REVERSE): FUN_005233a0 (the per-sub-object destructor) hasn't been read yet, so
        // only the Free half is reproduced — the sub-objects hold no COM references that the
        // reconstructed graph knows about, but this is not yet proven.
        void* field2 = device[2];
        if (field2 != null) Free(field2);

        void* field13 = device[0x13];
        if (field13 != null) Free(field13);

        for (int i = 3; i < 3 + 0x10; i++)
        {
            void* sub = device[i];
            if (sub != null) Free(sub);
        }

        if (device[0] != null) InvokeComMethod(device[0], IDirectSound_Release /* 0x08, generic COM Release */);

        for (int i = 0; i < 0x231; i++) device[i] = null;
    }

    /// <summary>
    /// Ghidra: FUN_00504600 @ 0x00504600 -> FUN_00523200 @ 0x00523200 — post-loop teardown,
    /// stage 1: destroys and frees the joystick/DirectInput device created by FUN_00523120.
    /// </summary>
    private static void ShutdownStage1()
    {
        // Ghidra: FUN_00523200(DAT_00604b54) — `if (p != 0) { FUN_00522e40(p); FUN_00547210(p); }`
        if (s_JoystickDevice_00604b54 == null) return;
        DestroyJoystickDevice((void**)s_JoystickDevice_00604b54);
        Free(s_JoystickDevice_00604b54);
        s_JoystickDevice_00604b54 = null;
    }

    // Ghidra: DAT_00604b54 — the joystick/DirectInput device object. Created by FUN_00523120,
    // which SetupJoystickHandlers does not call yet (see REVERSE_STATUS.md), so it stays null and
    // ShutdownStage1 is currently a well-defined no-op rather than a crash.
    private static void* s_JoystickDevice_00604b54;

    /// <summary>
    /// Ghidra: FUN_00512ff0 @ 0x00512ff0 — post-loop teardown, stage 3: the DirectSound shutdown.
    /// Releases the primary/listener object, then every sound buffer (stopping any that are still
    /// playing), then the IDirectSound device itself.
    /// </summary>
    private static void ShutdownStage3()
    {
        if (s_DirectSound == null) return;

        if (s_DirectSoundPrimary != null)
        {
            InvokeComMethod(s_DirectSoundPrimary, IDirectSoundBuffer_Release);
            // Ghidra does not null DAT_005db754 here — preserved as-is.
        }

        ReleaseAllSoundBuffers(stopPlayingBuffers: true);

        InvokeComMethod(s_DirectSound, IDirectSound_Release);
        s_DirectSound = null;
    }

    /// <summary>Ghidra: FUN_00542f50 @ 0x00542f50 (-> FUN_005428d0) — post-loop teardown, stage 4; same function RunMainLoop uses mid-sequence, see CloseMovieStreamIfOpen.</summary>
    private static void ShutdownStage4() => CloseMovieStreamIfOpen();

    /// <summary>Ghidra: FUN_00542f90 @ 0x00542f90 — post-loop teardown, stage 5; same function RunMainLoop uses mid-sequence, see ShutdownMoviePlayer.</summary>
    private static void ShutdownStage5() => ShutdownMoviePlayer();
}
