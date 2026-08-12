// OpenForesia — reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth — never modify it, see Prompt.md / Strategy.md).
//
// This file follows the call graph starting at the PE entry point `entry` and works
// outward. Progress, naming decisions and open questions are tracked in
// src/Blaze/REVERSE_STATUS.md — read that file before continuing this work.

using System;
using System.IO;
using System.Runtime.CompilerServices;
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

    // Ghidra: FUN_005129e0 -> DirectDrawCreate(NULL, &lpDD, NULL).
    [DllImport("ddraw.dll")] private static extern int DirectDrawCreate(void* lpGUID, void** lplpDD, void* pUnkOuter);

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

    // Game-state-0 ordinals. winsys3d.c declares all of these with no parameters, but that is the
    // same decompiler artifact seen throughout this project — the **call sites are authoritative**,
    // and the argument counts below are theirs (Blaze.c: FUN_00529a80 and FUN_00542160).
    [DllImport("winsys3d.dll", EntryPoint = "#97")] private static extern int Ordinal_97(int a, int b, int c);
    [DllImport("winsys3d.dll", EntryPoint = "#762")] private static extern void Ordinal_762(void* p);
    [DllImport("winsys3d.dll", EntryPoint = "#765")] private static extern int Ordinal_765(int a, uint rgb);
    [DllImport("winsys3d.dll", EntryPoint = "#766")] private static extern int Ordinal_766(void* p);
    [DllImport("winsys3d.dll", EntryPoint = "#767")] private static extern void Ordinal_767(int a, uint rgb);
    [DllImport("winsys3d.dll", EntryPoint = "#768")] private static extern int Ordinal_768(int a);
    [DllImport("winsys3d.dll", EntryPoint = "#769")] private static extern int Ordinal_769(int a);
    [DllImport("winsys3d.dll", EntryPoint = "#770")] private static extern int Ordinal_770(void* p);

    // Frame ordinals. 688 is libgpu `ClearOTagR(u_long *ot, int n)` — winsys3d.c FUN_00a06160 keeps
    // both parameters, unlike its neighbours. 657/660 take a DISPENV/DRAWENV (PutDispEnv/PutDrawEnv
    // shaped), 701 takes the ordering table (DrawOTag shaped).
    [DllImport("winsys3d.dll", EntryPoint = "#688")] private static extern uint* Ordinal_688(uint* ot, int count);
    [DllImport("winsys3d.dll", EntryPoint = "#657")] private static extern int Ordinal_657(void* env);
    [DllImport("winsys3d.dll", EntryPoint = "#660")] private static extern int Ordinal_660(void* env);
    [DllImport("winsys3d.dll", EntryPoint = "#701")] private static extern void Ordinal_701(uint* ot);

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

    // winsys3d.c FUN_00a05bb0 — libgpu `OpenTIM(u_long *addr)`. Parks a cursor at `addr + 4` in the
    // DLL global DAT_011be790; ReadTIM below consumes and clears it, so the pair is one-shot.
    [DllImport("winsys3d.dll", EntryPoint = "#677")] private static extern int OpenTim(void* timData);

    // winsys3d.c FUN_00a05bd0 — libgpu `ReadTIM(TIM_IMAGE*)`. Parses the TIM header the cursor
    // points at and fills the 5-dword TIM_IMAGE below. When the header's bit 3 is clear the image
    // has no CLUT and it writes mode=2, crect=null, caddr=null.
    [DllImport("winsys3d.dll", EntryPoint = "#662")] private static extern void ReadTim(TimImage* image);
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
    [DllImport("user32.dll", CharSet = CharSet.Ansi)] private static extern nint DefWindowProcA(IntPtr hWnd, uint msg, nuint wParam, nint lParam);
    [DllImport("user32.dll")] private static extern void PostQuitMessage(int nExitCode);
    [DllImport("user32.dll")] private static extern IntPtr SetFocus(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern short GetAsyncKeyState(int vKey);
    [DllImport("kernel32.dll")] private static extern void Sleep(uint milliseconds);
    [DllImport("winmm.dll")] private static extern uint timeGetTime();
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

    // =====================================================================
    // The winsys3d display-list arena (Ghidra: MainMem / DAT_00b6dbd8)
    //
    // Both binaries address the display list on **24 bits**:
    //   - winsys3d.c FUN_00a06160 (Ordinal_688, ClearOTagR) stores `(uint)entry & 0xffffff` into
    //     every link and terminates the list with the 0xffffff sentinel;
    //   - winsys3d.c FUN_00a061b0 (Ordinal_701, DrawOTag) follows `*p & 0xffffff` and reads the
    //     top byte as the primitive's word count;
    //   - Blaze.exe agrees — DrawFlatPrimitive builds its tag as `*prim = *otEntry | 0x2000000`,
    //     which is only correct while the address's top byte is zero.
    // So every ordering table and primitive buffer must live below 0x01000000.
    //
    // In 1998 that was automatic: Blaze.exe imports winsys3d.dll **statically** (the name sits in
    // its import table at file offset 0x1703AC, the only one of the five DLLs so imported), the
    // loader mapped it at its preferred base 0x00A00000, and winsys3d.c line 568 set
    // `MainMem = &DAT_00b6dbd8` = 0x00B6DBD8. On modern Windows the DLL is relocated out of low
    // memory — measured at 0x09… on this machine, because OpenForesia.exe's own ASLR base sits
    // inside winsys3d's preferred range — and DrawOTag faults on the very first frame.
    //
    // DEVIATION (environmental, see REVERSE_STATUS.md): the arena is allocated here, in low
    // memory, and its address written into the `MainMem` pointer winsys3d exports. The DLL file is
    // not modified and no Windows setting is changed. This is sound because MainMem is a genuine
    // indirection — it occurs exactly three times in 27703 lines of winsys3d.c:
    //     line  568  MainMem = &DAT_00b6dbd8;                     the sole initialisation, in entry()
    //     line 5655  DAT_00dedbd8 = iVar1 + 0x240000 + MainMem;   DrawOTag, re-derived every frame
    //     line 5843  DAT_00dedbd8 = MainMem + 0x200000;           Ordinal_789's device init, once
    // and DAT_00b6dbd8 itself is referenced only at line 568. Nothing between MainMem + 0x0 and
    // MainMem + 0x280000 is addressed by a fixed address — the globals resume exactly at
    // MainMem + 0x280000 — which is what makes the arena relocatable at all.
    // =====================================================================

    // Ghidra: winsys3d.c line 568 — MainMem points 0xDBD8 into the 64KB-aligned region that holds
    // DAT_00b6dbd8, so allocating at 0x00B60000 reproduces MainMem == 0x00B6DBD8 exactly.
    private const uint MainMemOffsetInArena = 0xDBD8;
    private const uint ArenaPreferredBase = 0x00B60000;
    private const uint ArenaOriginalMainMem = ArenaPreferredBase + MainMemOffsetInArena; // 0x00B6DBD8

    // Ghidra: winsys3d.c — the arena runs from MainMem to MainMem + 0x280000, where the DLL's
    // globals resume (DAT_00dedbd8 and 36 others). Known usage: Blaze.exe takes +0x1c6000..+0x1e4000
    // for its two ordering tables and primitive buffers, winsys3d takes +0x200000 and +0x240000 for
    // its double-buffered scratch (lines 5843 and 5655).
    private const uint ArenaSize = 0x280000;
    private const uint ArenaAllocationSize = MainMemOffsetInArena + ArenaSize; // rounded up below

    // The 24-bit link ceiling, and the address-space floor Windows permanently reserves.
    private const uint DisplayListCeiling = 0x01000000;
    private const uint ArenaScanFloor = 0x00010000;
    private const uint AllocationGranularity = 0x10000;

    private const uint MEM_COMMIT = 0x1000, MEM_RESERVE = 0x2000, MEM_FREE = 0x10000;
    private const uint PAGE_READWRITE = 0x04;

    [StructLayout(LayoutKind.Sequential)]
    private struct MEMORY_BASIC_INFORMATION
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public uint AllocationProtect;
        public IntPtr RegionSize;
        public uint State;
        public uint Protect;
        public uint Type;
    }

    [DllImport("kernel32.dll")] private static extern void* VirtualAlloc(void* lpAddress, nuint dwSize, uint flAllocationType, uint flProtect);
    [DllImport("kernel32.dll")] private static extern nuint VirtualQuery(void* lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, nuint dwLength);

    private static byte* s_DisplayListArena;   // the base of our allocation, not MainMem itself

    /// <summary>
    /// Places the display-list arena in low memory and points winsys3d's exported
    /// <c>MainMem</c> at it. Idempotent, and must run before any winsys3d entry point that
    /// derives a pointer from the arena — in practice before <c>Ordinal_789(0, 0)</c>, whose
    /// body (winsys3d.c line 5843) caches <c>MainMem + 0x200000</c> once and never re-derives it.
    /// </summary>
    /// <summary>
    /// Claims the low arena at the earliest point managed code runs. Called from a module
    /// initializer, which executes before <c>Main</c> and before any other static constructor —
    /// every allocation the program makes afterwards is one more competitor for the same
    /// 16 MB window, and the window is contended enough to lose the race otherwise.
    /// </summary>
    internal static void PrepareDisplayListArena() => EnsureDisplayListArena();

    /// <summary>
    /// Same, but reports failure instead of throwing, so the caller can decide whether a fresh
    /// process is worth trying. Address-space layout differs run to run, so a relaunch is a real
    /// second chance rather than a retry of the same conditions.
    /// </summary>
    internal static bool TryPrepareDisplayListArena()
    {
        try
        {
            EnsureDisplayListArena();

            // Claim every low allocation now, not on first use. These have to live below
            // 0x01000000 as well, and taking the menu descriptor lazily at the first menu frame
            // meant competing with a runtime that had had seconds to grow into the same window —
            // measured: the arena succeeded and the descriptor then failed.
            _ = s_MenuContext_0056dd58;
            return true;
        }
        catch (InvalidOperationException) { return false; }
    }

    private static void EnsureDisplayListArena()
    {
        if (s_DisplayListArena != null) return;

        // Reserve BEFORE loading winsys3d.dll. The DLL is an 8.3 MB image and, once relocated out
        // of its preferred base, is itself the strongest competitor for the same low window —
        // loading it first made this placement fail roughly one run in four.

        // 1. The original layout first: 0xDBD8 of lead-in so MainMem lands on 0x00B6DBD8 exactly
        //    and every address in winsys3d.c reads straight against the running process.
        nuint preferredSize = (ArenaAllocationSize + 0xFFFu) & ~0xFFFu;
        byte* arena = (byte*)VirtualAlloc((void*)ArenaPreferredBase, preferredSize,
                                          MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
        byte* mainMem = arena != null ? arena + MainMemOffsetInArena : null;

        // 2. Otherwise only the arena proper is needed — the 0xDBD8 lead-in exists solely to hit
        //    the original address, and demanding it in the fallback cost 56 KB of contiguous low
        //    memory for nothing. Measured: it was the difference between 0x250000 free and a
        //    0x28E000 request failing.
        uint largestFreeSeen = 0;
        if (arena == null)
        {
            nuint size = ArenaSize;
            uint maxBase = (DisplayListCeiling - (uint)size) & ~(AllocationGranularity - 1);
            arena = ScanForLowArena(size, maxBase, out largestFreeSeen);
            mainMem = arena;

            if (arena == null)
            {
                throw new InvalidOperationException(
                    $"The display list is addressed on 24 bits (winsys3d.c ClearOTagR/DrawOTag mask " +
                    $"by 0xffffff), so its arena must sit below 0x{DisplayListCeiling:X8}. No free " +
                    $"block of 0x{size:X} bytes was available below 0x{maxBase:X8} — the largest free " +
                    $"region there was 0x{largestFreeSeen:X} bytes. Without it every frame would fault " +
                    $"inside DrawOTag with no explanation — see REVERSE_STATUS.md, arena deviation.");
            }
        }

        s_DisplayListArena = arena;

        // Only now bring the library in, and point its exported pointer at the arena we secured.
        EnsureWinsys3dLoaded();
        if (s_MainMemExref == null) return;   // the DLL itself is unavailable; callers handle null
        *s_MainMemExref = mainMem;

        long delta = (long)(nint)mainMem - ArenaOriginalMainMem;
        Console.Error.WriteLine(delta == 0
            ? $"winsys3d arena at MainMem=0x{(nint)mainMem:X8} — original 1998 layout, addresses " +
              "match winsys3d.c one for one"
            : $"winsys3d arena at MainMem=0x{(nint)mainMem:X8} — 0x{ArenaOriginalMainMem:X8} was " +
              $"unavailable; {(delta < 0 ? "subtract" : "add")} 0x{Math.Abs(delta):X} " +
              $"{(delta < 0 ? "from" : "to")} any address read from winsys3d.c");
    }

    /// <summary>
    /// Walks the free regions between <see cref="ArenaScanFloor"/> and <paramref name="maxBase"/>
    /// and takes the **highest** base that fits. Highest rather than first for two reasons: it lands
    /// nearest the original 0x00B60000, keeping the reported delta small, and it avoids parking the
    /// arena at the very bottom of the address space where pointer values become suspiciously small.
    /// </summary>
    private static byte* ScanForLowArena(nuint size, uint maxBase, out uint largestFreeSeen)
    {
        nuint mbiSize = (nuint)sizeof(MEMORY_BASIC_INFORMATION);
        largestFreeSeen = 0;
        bool found = false;
        uint best = 0;

        for (uint address = ArenaScanFloor; address <= maxBase;)
        {
            if (VirtualQuery((void*)address, out MEMORY_BASIC_INFORMATION mbi, mbiSize) == 0) break;

            uint regionBase = (uint)(nint)mbi.BaseAddress;
            uint regionSize = (uint)(nint)mbi.RegionSize;
            if (regionSize == 0) break;

            if (mbi.State == MEM_FREE)
            {
                if (regionSize > largestFreeSeen) largestFreeSeen = regionSize;

                // VirtualAlloc needs a granularity-aligned base, which a free region rarely starts on.
                uint lowest = (regionBase + AllocationGranularity - 1) & ~(AllocationGranularity - 1);
                ulong regionEnd = (ulong)regionBase + regionSize;

                if (regionEnd >= (ulong)lowest + size)
                {
                    // The highest granularity-aligned base that still fits inside this region.
                    uint highest = (uint)((regionEnd - size) & ~(ulong)(AllocationGranularity - 1));
                    if (highest > maxBase) highest = maxBase;
                    if (highest >= lowest)
                    {
                        found = true;
                        best = highest;
                    }
                }
            }

            address = regionBase + regionSize;
        }

        if (!found) return null;

        // A region can be taken between the scan and the allocation; retry downward if so.
        for (uint candidate = best; candidate >= ArenaScanFloor; candidate -= AllocationGranularity)
        {
            byte* arena = (byte*)VirtualAlloc((void*)candidate, size, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            if (arena != null) return arena;
        }

        return null;
    }

    // Ghidra: MainMem_exref — Blaze.exe imports this pointer as DATA from winsys3d.dll (mirrors
    // TexFilterPossible_exref below: winsys3d.c's `entry` sets `MainMem = &DAT_00b6dbd8;`, its own
    // scratch arena). Resolved lazily via GetProcAddress, same pattern as GetTexFilterPossiblePtr.
    private static byte** s_MainMemExref;

    private static void EnsureWinsys3dLoaded()
    {
        if (s_MainMemExref != null) return;

        IntPtr module = LoadLibraryA(ToAnsiZ("winsys3d.dll"));
        if (module != IntPtr.Zero)
        {
            s_MainMemExref = (byte**)GetProcAddress(module, ToAnsiZ("MainMem"));
        }
    }

    private static byte* GetMainMemPtr()
    {
        EnsureDisplayListArena();
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
        // DEVIATION, and it must come first: Blaze.exe imports winsys3d.dll statically, so in the
        // original the library was mapped — and its arena fixed at 0x00B6DBD8 — before this entry
        // point ever ran. Loading it here and retargeting MainMem reproduces that ordering, which
        // Ordinal_789's device init depends on: winsys3d.c line 5843 caches MainMem + 0x200000 once
        // and never re-derives it. See EnsureDisplayListArena for the 24-bit constraint behind this.
        EnsureDisplayListArena();

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

    // Ghidra: &DAT_00563080 (Blaze.c:58266) — Ghidra gave it no string name, but the literal is
    // present in Legacy/Blaze.exe: "Size". It sits in the .data key block exactly where the call
    // order puts it, between "Small" (0x00563088) and "Resolution" (0x00563074), and the installed
    // game's BLAZE.cfg carries Size=23 alongside Resolution=4 — which is precisely
    // s_ResolutionDetailLimit[4]. Three independent confirmations; no longer a TODO.
    private const string SizeIniKey = "Size";

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

    // Ghidra: DAT_005de160, ini key "Size", default 0xb (11). Clamped in LoadOptionsIni against a
    // per-resolution table (DAT_00562e00).
    //
    // Role established 2026-08-12 (the earlier TODO(REVERSE) here is answered): this is the
    // engine's render-size level. `SetShift` (winsys3d.c:4937) hands the 3D device a viewport of
    // `(Size + 9) * 0x20` by `(Size + 9) * 0x18` and selects the vertex-scaling lookup table with
    // the same value. So Size 11 is 640x480 — the engine's logical reference space — and the
    // shipped `Size=21` is 960x720, which matches the render target measured at runtime. Full
    // derivation in `Documentation/DLL/winsys3d.md`, section "Coordinate scaling".
    private static int s_SizeSetting_005de160 = 0xb;

    /// <summary>Taille de rendu que le réglage <c>Size</c> implique, en pixels.</summary>
    private static (int Width, int Height) RenderSizeFromSizeSetting()
        => ((s_SizeSetting_005de160 + 9) * 0x20, (s_SizeSetting_005de160 + 9) * 0x18);

    // Ghidra: DAT_00566cc4 (ini "Resolution", default 2) / DAT_00562df8 (ini "Timing", default 1).
    private static uint s_ResolutionIndex;
    private static uint s_TimingMode;

    // Ghidra: &DAT_00564a58, 4 ints — per-joystick-slot assignment (-1 = unassigned). Ini keys
    // built from format string "Joy%d" — no space (Ghidra: s_Joy_d_00563064; literal read back
    // from Legacy/Blaze.exe at 0x00563064). An earlier reconstruction wrote "Joy %d", which reads
    // keys the real BLAZE.cfg does not contain: the joystick settings silently fell back to their
    // defaults instead of being loaded.
    private static readonly int[] s_JoystickSlot = new int[4];

    // Ghidra: &DAT_00562e00 — per-resolution clamp table for s_SizeSetting_005de160.
    // TODO(REVERSE): actual byte content not present in Blaze.c (data-only global); sized as a
    // guess and filled with int.MaxValue as a safe (non-clamping) placeholder rather than
    // inventing real limits.
    private static readonly int[] s_ResolutionDetailLimit = CreateUnknownResolutionLimitTable();

    // Ghidra: DAT_00562e00 — recovered from Legacy/Blaze.exe as the dwords
    // { 1, 7, 11, 16, 23, 0 }. These are real per-resolution detail clamps; the table was
    // previously filled with int.MaxValue on the assumption that its content was unrecoverable,
    // which turned the clamp into a silent no-op.
    // Entries 0..4 are confirmed: the installed BLAZE.cfg pairs Resolution=4 with Size=23, and
    // this table's index 4 is 23 — the clamp is a no-op there, which is what a settings file
    // written by the game itself must produce.
    // TODO(REVERSE): the trailing 0 is transcribed as read. It is plausibly a terminator rather
    // than a 6th entry meaning "clamp everything to 0"; the bytes that follow (01 02 03 04 05 06
    // 07 08) are visibly a different table, so 0x00562e00 ends here either way. Kept verbatim.
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
        s_SizeSetting_005de160 = (int)ReadIniInt(SizeIniKey, 0xb, iniPathAnsi);
        s_ResolutionIndex = ReadIniInt("Resolution", 2, iniPathAnsi);
        s_TimingMode = ReadIniInt("Timing", 1, iniPathAnsi);

        // ECHAFAUDAGE TEMPORAIRE : trace du fichier d'options réellement lu et des valeurs prises.
        LogDisplayMode("bureau, avant init 3D");
        Console.Error.WriteLine(
            $"[CFG] {Marshal.PtrToStringAnsi((IntPtr)iniPathAnsi)} -> Resolution={s_ResolutionIndex} " +
            $"Size={s_SizeSetting_005de160} Small={s_SmallTextureFlag} Timing={s_TimingMode}");

        int nextJoystickSlot = 0;
        for (int i = 0; i < s_JoystickSlot.Length; i++)
        {
            uint enabled = ReadIniInt($"Joy{i}", 1, iniPathAnsi);
            s_JoystickSlot[i] = enabled == 0 ? -1 : nextJoystickSlot++;
        }

        ApplyCdVolume();
        ApplyAllSoundBufferVolumes();
        CreateTranslationTables(s_GammaIni * GammaIniScale);

        if (s_ResolutionIndex < (uint)s_ResolutionDetailLimit.Length &&
            s_ResolutionDetailLimit[s_ResolutionIndex] < s_SizeSetting_005de160)
        {
            s_SizeSetting_005de160 = s_ResolutionDetailLimit[s_ResolutionIndex];
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
        WriteIniInt(SizeIniKey, (uint)s_SizeSetting_005de160, iniPathAnsi);
        WriteIniInt("Resolution", s_ResolutionIndex, iniPathAnsi);
        WriteIniInt("Timing", s_TimingMode, iniPathAnsi);

        for (int i = 0; i < s_JoystickSlot.Length; i++)
        {
            WriteIniInt($"Joy{i}", s_JoystickSlot[i] != -1 ? 1u : 0u, iniPathAnsi);
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
        public int AttractActive;           // offset 0x04 (param_1[1]) — set while the title screen is up;
                                            // FUN_00542970 tests it alongside Playing
        public int TimerArmed;              // offset 0x20 (param_1[8]) — nonzero once SetTimer(hwnd,1,...) ran
        public int Field24;                 // offset 0x24 — OpenMovieStream writes 2
        public int Field28;                 // offset 0x28 — OpenMovieStream writes 1; gates the WM_TIMER step
    }

    private static MoviePlayerState? s_MoviePlayer; // Ghidra: DAT_005dffa8

    private static readonly byte* s_VfwClassName = ToAnsiZ("VfwClass"); // Ghidra: s_VfwClass_0056e6b8 — TODO(REVERSE): exact bytes not recovered, name inferred from symbol
    private static readonly byte* s_MovieWindowTitle = ToAnsiZ("Blaze & Blade"); // Ghidra: s_Blaze___Blade_005631d8 — TODO(REVERSE): exact bytes not recovered

    // =====================================================================
    // DirectDraw surface setup (Ghidra: FUN_005129e0 + FUN_00512a10).
    //
    // The vtable offsets below are the documented IDirectDraw / IDirectDrawSurface v1 layouts, and
    // every one used here cross-checks against a decompiled call site:
    //   IDirectDraw:        0x18 CreateSurface, 0x4c RestoreDisplayMode,
    //                       0x50 SetCooperativeLevel, 0x54 SetDisplayMode
    //   IDirectDrawSurface: 0x30 GetAttachedSurface, 0x58 GetSurfaceDesc,
    //                       0x44 GetDC, 0x68 ReleaseDC  (the last two are what the attract blit uses)
    // =====================================================================

    private const int IDirectDraw_CreateSurface = 0x18 / 4;
    private const int IDirectDraw_RestoreDisplayMode = 0x4c / 4;
    private const int IDirectDraw_SetCooperativeLevel = 0x50 / 4;
    private const int IDirectDraw_SetDisplayMode = 0x54 / 4;
    private const int IDirectDrawSurface_GetAttachedSurface = 0x30 / 4;
    private const int IDirectDrawSurface_GetSurfaceDesc = 0x58 / 4;

    // DDSURFACEDESC is 0x6c bytes — the literal FUN_00512a10 writes into dwSize, corroborated by the
    // 0x1b-dword (108-byte) zeroing loop that precedes each use.
    /// <summary>
    /// Vrai quand la séquence de démarrage — écran-titre et attract — s'affiche dans la fenêtre du
    /// jeu au lieu de l'affichage exclusif du binaire d'origine.
    ///
    /// <para>
    /// C'est le drapeau de la **seule déviation de fidélité volontaire** de ce projet. Les trois
    /// autres écarts consignés dans <c>REVERSE_STATUS.md</c> sont subis — le refus du 16 bits par
    /// Windows moderne, les surfaces perdues, l'arène relocalisée par l'ASLR — et la reconstruction
    /// les contourne pour rester fonctionnelle. Celui-ci est demandé : le jeu ne doit plus prendre
    /// l'affichage en exclusif ni changer le mode, pour n'avoir qu'une seule fenêtre du lancement
    /// au menu.
    /// </para>
    /// <para>
    /// <c>OPENFORESIA_LEGACY_STARTUP=1</c> rétablit intégralement le comportement d'origine, sur le
    /// modèle d'<c>OPENFORESIA_LEGACY_3DHELP</c>. Ce qu'il gouverne : le niveau de coopération
    /// DirectDraw et le changement de mode dans <see cref="SetupDirectDrawSurfaces"/>, et les
    /// quatre sites qui affichent ou maximisent la fenêtre <c>VfwClass</c>.
    /// </para>
    /// </summary>
    private static readonly bool s_WindowedStartup =
        Environment.GetEnvironmentVariable("OPENFORESIA_LEGACY_STARTUP") != "1";

    private const int DdSurfaceDescSize = 0x6c;
    private const uint DDSD_CAPS = 0x0001;
    private const uint DDSD_HEIGHT = 0x0002;
    private const uint DDSD_WIDTH = 0x0004;
    private const uint DDSD_BACKBUFFERCOUNT = 0x0020;
    private const uint DDSCAPS_COMPLEX = 0x0008;
    private const uint DDSCAPS_FLIP = 0x0010;
    private const uint DDSCAPS_PRIMARYSURFACE = 0x0200;
    private const uint DDSCAPS_BACKBUFFER = 0x0004;

    /// <summary>
    /// Ghidra: FUN_00512a10 @ 0x00512a10 (`__thiscall`) — brings DirectDraw up and creates the
    /// flippable primary surface. **This is what populates `wrapper[1]`**, the surface the attract
    /// screen's blit reaches through GetDC/ReleaseDC; FUN_00512930 explicitly leaves it null, which
    /// is why recovering the overlay vtable alone never produced a picture.
    /// <para>
    /// Two spots in the decompiled body are decompiler noise, reconstructed from intent: the guard
    /// renders as <c>((uint)(param_1 + 2) &amp; 4) &amp;&amp; ((uint)(param_1 + 2) &amp; 2)</c>, which
    /// tests an *address*. 4 and 2 are `DDSD_WIDTH` and `DDSD_HEIGHT`, so the real test is on the
    /// surface description's flags, and the `SetRect` that follows takes its width and height from
    /// that description.
    /// </para>
    /// </summary>
    private static int SetupDirectDrawSurfaces(uint* wrapper, IntPtr hwnd)
    {
        // Ghidra: FUN_005129e0 — DirectDrawCreate(NULL, &wrapper[0], NULL), 0 on failure.
        void* directDraw;
        int hr = DirectDrawCreate(null, &directDraw, null);
        if (hr < 0 || directDraw == null) return unchecked((int)0x80004005); // E_FAIL — Ghidra's -0x7fffbffb
        wrapper[0] = (uint)(nuint)directDraw;

        void** ddVtable = *(void***)directDraw;

        // Ghidra: (**(*dd + 0x50))(dd, hwnd, 0x13)
        // 0x13 = DDSCL_FULLSCREEN | DDSCL_ALLOWREBOOT | DDSCL_EXCLUSIVE.
        var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)ddVtable[IDirectDraw_SetCooperativeLevel];

        // DEVIATION DELIBEREE ET CONDITIONNELLE — change `render-title-screen-in-window`.
        //
        // L'original prend l'affichage en `DDSCL_FULLSCREEN | DDSCL_EXCLUSIVE` puis le bascule en
        // 640x480, et c'est la moitié « affichage » de l'écran plein écran qu'on veut supprimer.
        // Tout cet appareillage n'a qu'un produit réellement consommé : le contexte de périphérique
        // que `BlitTitleScreen` obtenait par `GetDC` sur la surface primaire. Le backend le fournit
        // désormais (`StartupSurface`), donc plus rien ne le réclame ici :
        //
        //   * le back buffer obtenu par `GetAttachedSurface` n'était jamais stocké,
        //   * aucun `Flip` n'est jamais appelé sur cette chaîne,
        //   * les champs du wrapper renseignés ensuite ne sont lus que par le chemin vidéo
        //     DirectShow, laissé nul (TODO #13).
        //
        // On garde donc l'objet DirectDraw et son cycle de vie — `ReleaseDirectDrawSurfaces` le
        // libère toujours — et on ne retire que la prise d'affichage. `OPENFORESIA_LEGACY_STARTUP=1`
        // rétablit intégralement le chemin d'origine.
        if (!s_WindowedStartup)
        {
            hr = setCooperativeLevel(directDraw, hwnd, 0x13);
            if (hr < 0) return hr;
        }
        else
        {
            // 8 = DDSCL_NORMAL, la même valeur que la séquence de démontage d'origine emploie.
            hr = setCooperativeLevel(directDraw, hwnd, 8);
            if (hr < 0) return hr;

            // Ni changement de mode, ni chaîne primaire/flip : plus personne ne les consomme, et
            // c'est leur suppression qui fait disparaître le plein écran. Le wrapper garde les
            // valeurs de son constructeur ; les renseigner reviendrait à inventer une description
            // de surface qui n'existe plus.
            return 0;
        }

        // Ghidra: (**(*dd + 0x54))(dd, 0x280, 0x1e0, 0x10) — 640 x 480 x 16bpp.
        var setDisplayMode = (delegate* unmanaged[Stdcall]<void*, uint, uint, uint, int>)ddVtable[IDirectDraw_SetDisplayMode];
        hr = setDisplayMode(directDraw, 0x280, 0x1e0, 0x10);

        // ENVIRONMENTAL DEVIATION, measured rather than assumed. The original only ever asks for
        // 640x480x16, and that is what is attempted first. Modern Windows declines 16bpp mode
        // changes outright — measured here as E_NOTIMPL (0x80004001) — while the same 640x480 at
        // 32bpp succeeds and everything downstream (CreateSurface, GetDC, StretchDIBits) then works.
        // Falling back keeps the faithful call as the primary path and makes the deviation explicit
        // and conditional, rather than silently rewriting the original's request.
        if (hr == unchecked((int)0x80004001) /* E_NOTIMPL */)
        {
            hr = setDisplayMode(directDraw, 0x280, 0x1e0, 32);
        }

        if (hr < 0) return hr;

        // DEVIATION, deliberate and flagged. The original restores the display mode only through its
        // own teardown path. This reconstruction still throws on unreconstructed game states, and an
        // abrupt exit while holding an exclusive 640x480 mode leaves the user's desktop switched.
        // This restore-on-exit hook is a safety net, not fidelity — remove it once the shutdown path
        // is reliably reached.
        RegisterDisplayModeRestore(directDraw);

        // Ghidra: 0x1b-dword zeroing loop, then dwSize = 0x6c, dwFlags = 0x21, ddsCaps = 0x218.
        byte* desc = stackalloc byte[DdSurfaceDescSize];
        for (int i = 0; i < DdSurfaceDescSize; i++) desc[i] = 0;
        *(uint*)(desc + 0x00) = DdSurfaceDescSize;
        *(uint*)(desc + 0x04) = DDSD_CAPS | DDSD_BACKBUFFERCOUNT;                        // 0x21
        *(uint*)(desc + 0x14) = 1;                                                        // dwBackBufferCount
        *(uint*)(desc + 0x68) = DDSCAPS_PRIMARYSURFACE | DDSCAPS_FLIP | DDSCAPS_COMPLEX;  // 0x218

        void* primarySurface;
        var createSurface = (delegate* unmanaged[Stdcall]<void*, void*, void**, void*, int>)ddVtable[IDirectDraw_CreateSurface];
        hr = createSurface(directDraw, desc, &primarySurface, null);
        if (hr < 0) return hr;

        wrapper[1] = (uint)(nuint)primarySurface;   // the assignment the whole display path hinges on

        void** surfaceVtable = *(void***)primarySurface;

        // Ghidra: auStack_28[0] = 4 (DDSCAPS_BACKBUFFER); (**(*surface + 0x30))(surface, &caps, &out)
        uint* caps = stackalloc uint[4];
        caps[0] = DDSCAPS_BACKBUFFER; caps[1] = 0; caps[2] = 0; caps[3] = 0;
        void* backBuffer;
        var getAttachedSurface = (delegate* unmanaged[Stdcall]<void*, uint*, void**, int>)surfaceVtable[IDirectDrawSurface_GetAttachedSurface];
        hr = getAttachedSurface(primarySurface, caps, &backBuffer);
        if (hr < 0) return hr;

        // Ghidra: second 0x1b-dword zeroing loop, dwSize = 0x6c, then (**(*surface + 0x58))(...)
        byte* surfaceDesc = stackalloc byte[DdSurfaceDescSize];
        for (int i = 0; i < DdSurfaceDescSize; i++) surfaceDesc[i] = 0;
        *(uint*)(surfaceDesc + 0x00) = DdSurfaceDescSize;
        var getSurfaceDesc = (delegate* unmanaged[Stdcall]<void*, byte*, int>)surfaceVtable[IDirectDrawSurface_GetSurfaceDesc];
        hr = getSurfaceDesc(primarySurface, surfaceDesc);
        if (hr < 0) return hr;

        uint flags = *(uint*)(surfaceDesc + 0x04);
        uint height = *(uint*)(surfaceDesc + 0x08);
        uint width = *(uint*)(surfaceDesc + 0x0c);

        if ((flags & DDSD_WIDTH) != 0 && (flags & DDSD_HEIGHT) != 0)
        {
            // Win32 RECT here (four LONGs at wrapper[4..7]), not the four-short libgpu RECT used by
            // the winsys3d ordinals elsewhere in this file.
            SetRect((int*)(wrapper + 4), 0, 0, (int)width, (int)height);
            wrapper[0xe] = 0;   // Ghidra: param_1[0xe] = 0
            wrapper[0xf] = 1;   // Ghidra: param_1[0xf] = 1
            wrapper[3] = 1;     // Ghidra: param_1[3]  = 1
            hr = 0;
        }

        return hr;
    }

    /// <summary>
    /// Ghidra: FUN_00512b40 @ 0x00512b40 (`__thiscall`) — releases the surfaces and, crucially,
    /// calls <c>SetCooperativeLevel(hwnd, DDSCL_NORMAL)</c> to **drop the exclusive display mode**
    /// before releasing the DirectDraw object.
    /// <para>
    /// This is not optional bookkeeping. `winsys3d.dll`'s own 3D initialiser (`Ordinal_789(0, 0)`)
    /// runs later in RunMainLoop, loads `3dhelp.dll` and calls its `Create3DDevice`; if this
    /// reconstruction is still holding DirectDraw exclusively, that creation fails and the DLL
    /// shows "3D-Graphics initialisation error" and calls <c>ExitProcess(3)</c>
    /// (`winsys3d.c:5966`). Observed exactly that way before this was reconstructed.
    /// </para>
    /// </summary>
    private static int ReleaseDirectDrawSurfaces(uint* wrapper, IntPtr hwnd)
    {
        // Ghidra: param_1[2] then param_1[1] — Release each if non-null, in that order.
        for (int slot = 2; slot >= 1; slot--)
        {
            void* obj = (void*)(nuint)wrapper[slot];
            if (obj != null)
            {
                void** vt = *(void***)obj;
                var release = (delegate* unmanaged[Stdcall]<void*, uint>)vt[IUnknown_Release];
                release(obj);
                wrapper[slot] = 0;
            }
        }

        void* directDraw = (void*)(nuint)wrapper[0];
        if (directDraw != null)
        {
            void** ddVtable = *(void***)directDraw;

            // Ghidra: (**(*dd + 0x50))(dd, hwnd, 8) — 8 = DDSCL_NORMAL. On failure the original
            // shows its own message box and returns the HRESULT without releasing.
            var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)ddVtable[IDirectDraw_SetCooperativeLevel];
            int hr = setCooperativeLevel(directDraw, hwnd, 8);
            if (hr < 0)
            {
                MessageBoxA(hwnd, s_SetCooperativeLevelFailed, s_ErrorCaption, 0x10);
                return hr;
            }

            var release = (delegate* unmanaged[Stdcall]<void*, uint>)ddVtable[IUnknown_Release];
            release(directDraw);
            wrapper[0] = 0;

            // The exclusive mode is gone, so the safety-net hook has nothing left to restore.
            s_DisplayModeOwner = null;
        }

        wrapper[0xd] = 0xff00ff;                       // Ghidra: param_1[0xd] = 0xff00ff
        SetRect((int*)(wrapper + 4), 0, 0, 0, 0);
        SetRect((int*)(wrapper + 8), 0, 0, 0, 0);
        wrapper[3] = 0;
        return 0;
    }

    private const int IUnknown_Release = 0x08 / 4;
    private const int IDirectDrawSurface_GetDC = 0x44 / 4;
    private const int IDirectDrawSurface_Restore = 0x6c / 4;
    private const int IDirectDrawSurface_ReleaseDC = 0x68 / 4;

    /// <summary>
    /// Ghidra: the blit inside FUN_00542c40 (`Blaze.c:89916-89929`) — draws the loaded
    /// TITLE640.BMP onto the DirectDraw primary surface. Used both for the initial paint and for
    /// the WM_COMMAND(lParam == 1) repaint.
    /// <para>
    /// The bitmap is the raw .BMP file image `LoadBitmapFile` returned, so the two offsets the
    /// original indexes are BITMAPFILEHEADER fields: <c>+10</c> is `bfOffBits` (pixel data) and
    /// <c>+0xe</c> is the BITMAPINFOHEADER that immediately follows the 14-byte file header.
    /// </para>
    /// </summary>
    private static void BlitTitleScreen(MoviePlayerState player)
    {
        if (player.TitleBitmap == null) return;

        // Régime GDI de démarrage : la même image est écrite dans la surface hors écran du backend,
        // qui sera présentée dans la fenêtre du jeu.
        //
        // **Ajout, pas substitution.** Le chemin DirectDraw ci-dessous reste intact ; l'écran-titre
        // continue donc de s'afficher exactement comme avant, en plus d'alimenter notre surface.
        // C'est ce qui rend ce palier réversible : le retrait du chemin d'origine est un pas
        // séparé et assumé (change `render-title-screen-in-window`, groupe 6).
        BlitTitleScreenToStartupSurface(player);

        if (player.DirectDrawWrapper == null) return;

        // Ghidra: piVar1 = *(int **)(*(int *)(param_1 + 0x1c) + 4) — the wrapper's primary surface.
        // Null until FUN_00512a10 has run; the original null-checks it, and so does this.
        void* surface = (void*)(nuint)((uint*)player.DirectDrawWrapper)[1];
        if (surface == null) return;

        void** surfaceVtable = *(void***)surface;

        IntPtr hdc = IntPtr.Zero;
        var getDc = (delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)surfaceVtable[IDirectDrawSurface_GetDC];
        int hrDc = getDc(surface, &hdc);

        // A surface goes "lost" whenever the display mode changes or another application takes the
        // display; every later call on it then fails until it is restored. Measured happening here
        // between two attract-screen repaints. The original carries the same Restore-and-retry
        // pairing elsewhere in this cluster (vtable 0x6c), so this is its own handling, not a patch.
        if (hrDc == unchecked((int)0x887601c2) /* DDERR_SURFACELOST */)
        {
            var restore = (delegate* unmanaged[Stdcall]<void*, int>)surfaceVtable[IDirectDrawSurface_Restore];
            if (restore(surface) < 0) return;
            hrDc = getDc(surface, &hdc);
        }

        if (hrDc < 0 || hdc == IntPtr.Zero) return;

        byte* bmp = player.TitleBitmap;
        void* bits = bmp + *(int*)(bmp + 10);   // bfOffBits
        void* info = bmp + 0xe;                 // BITMAPINFOHEADER

        StretchDIBits(hdc, 0, 0, TitleScreenWidth, TitleScreenHeight,
                      0, 0, TitleScreenWidth, TitleScreenHeight,
                      bits, info, 0 /* DIB_RGB_COLORS */, 0xcc0020 /* SRCCOPY */);

        var releaseDc = (delegate* unmanaged[Stdcall]<void*, IntPtr, int>)surfaceVtable[IDirectDrawSurface_ReleaseDC];
        releaseDc(surface, hdc);
    }

    // Les dimensions que l'original emploie sur ce chemin, en source comme en destination : le
    // `StretchDIBits` de Blaze.c:89916-89929 et le `SetDisplayMode` de FUN_00512a10 portent les
    // mêmes littéraux. Ce ne sont donc pas des valeurs choisies par la reconstruction.
    private const int TitleScreenWidth = 0x280;    // 640
    private const int TitleScreenHeight = 0x1e0;   // 480

    /// <summary>
    /// Écrit l'écran-titre dans la surface hors écran du backend, en réutilisant le même
    /// <c>StretchDIBits</c> et les mêmes dimensions que le chemin d'origine.
    /// </summary>
    private static void BlitTitleScreenToStartupSurface(MoviePlayerState player)
    {
        IntPtr dc = StartupSurface.EnsureDc(TitleScreenWidth, TitleScreenHeight);
        if (dc == IntPtr.Zero) return;

        byte* bmp = player.TitleBitmap;
        void* bits = bmp + *(int*)(bmp + 10);   // bfOffBits
        void* info = bmp + 0xe;                 // BITMAPINFOHEADER

        int scanlines = StretchDIBits(dc, 0, 0, TitleScreenWidth, TitleScreenHeight,
                                      0, 0, TitleScreenWidth, TitleScreenHeight,
                                      bits, info, 0 /* DIB_RGB_COLORS */, 0xcc0020 /* SRCCOPY */);
        if (scanlines > 0) StartupSurface.MarkDrawn();

        // Présentation : cet appel est la frontière d'image du régime GDI, ce qu'EndScene est au
        // régime Direct3D. Un échec ne dégrade que ce redessin — journalisé une fois, jamais
        // propagé, jamais interruptif pour la boucle d'attract.
        //
        // **Aucune pompe de messages ici**, contrairement à ce que fait EndScene. La boucle
        // d'attract (`ShowTitleScreenAndWait`) est elle-même une pompe `PeekMessageA(NULL, …, PM_REMOVE)`
        // qui tourne en permanence et dispatche : les messages de notre fenêtre sont donc déjà
        // servis. Vider la file ici consommerait les `WM_KEYDOWN` qui terminent l'attract, et le
        // démarrage n'atteindrait plus jamais le menu.
        if (StartupSurface.HasContent && !PresentStartupSurface() && !s_StartupPresentReported)
        {
            s_StartupPresentReported = true;
            Console.Error.WriteLine("[GDI] présentation de l'écran-titre indisponible — redessin ignoré");
        }

    }

    private static bool s_StartupPresentReported;

    /// <summary>
    /// Présente la surface de démarrage dans la fenêtre du jeu. Rend faux si la fenêtre n'existe
    /// pas encore ou si le contexte de périphérique est indisponible — l'appelant se contente alors
    /// de sauter ce redessin.
    /// </summary>
    private static bool PresentStartupSurface()
    {
        IntPtr hwnd = D3D11Window.Handle;
        if (hwnd == IntPtr.Zero) return false;

        (int w, int h) = D3D11Window.ClientSize();
        return StartupSurface.PresentTo(hwnd, w, h);
    }

    /// <summary>
    /// Ghidra: FUN_00512930 @ 0x00512930 (`__thiscall`) — constructs the DirectDraw wrapper object.
    /// Every field the teardown and the blit later read is initialised **here**, which is why
    /// skipping it is not harmless: `Malloc` maps to <c>NativeMemory.Alloc</c> and does not zero,
    /// so `ReleaseDirectDrawSurfaces` read an uninitialised <c>wrapper[2]</c> as a COM pointer and
    /// dereferenced garbage. The original zeroes exactly these slots.
    /// </summary>
    private static void ConstructDirectDrawWrapper(uint* wrapper)
    {
        // Ghidra: SetRect(ESI + 0x10, 0,0,0,0) and SetRect(ESI + 0x20, 0,0,0,0) — two Win32 RECTs.
        SetRect((int*)(wrapper + 4), 0, 0, 0, 0);
        SetRect((int*)(wrapper + 8), 0, 0, 0, 0);

        wrapper[0] = 0;          // Ghidra: MOV [ESI],EDI       — IDirectDraw
        wrapper[1] = 0;          // Ghidra: MOV [ESI+0x4],EDI   — primary surface
        wrapper[2] = 0;          // Ghidra: MOV [ESI+0x8],EDI   — the slot that caused the crash
        wrapper[3] = 0;          // Ghidra: MOV [ESI+0xc],EDI
        wrapper[0xc] = 0;        // Ghidra: MOV [ESI+0x30],EDI
        wrapper[0xd] = 0xff00ff; // Ghidra: MOV [ESI+0x34],0xff00ff — the colour key
        wrapper[0xe] = 0;        // Ghidra: MOV [ESI+0x38],EDI
        wrapper[0xf] = 1;        // Ghidra: MOV [ESI+0x3c],1
        wrapper[0x10] = 0;       // Ghidra: MOV [ESI+0x40],EDI

        // Ghidra: sub = FUN_00547220(0x18); if (sub) FUN_00512d10(sub, this, hwnd, &local);
        //         this[0x44] = (result != 0) ? result + 0xc : 0;
        // The 0x18-byte object is the "Overlay Callback Object" — a COM sink whose vtable slots are
        // known (0x0054D028) but which nothing can ever call back into here, because no real
        // DirectDraw overlay is registered against it. Leaving this null is the original's own
        // allocation-failure path, not an invented one.
        wrapper[0x11] = 0;       // TODO(REVERSE): FUN_00512d10's overlay callback sink.
    }

    // Ghidra: s_SetCooperativeLevel___failed_to_g_00563178 — only the truncated symbol name
    // survives in Blaze.c; the reconstructed text is a best guess, same caveat as the other
    // message strings.
    private static readonly byte* s_SetCooperativeLevelFailed = ToAnsiZ("SetCooperativeLevel() failed to go back to normal mode.");

    // Safety net for the deviation noted above — not part of the original.
    private static void* s_DisplayModeOwner;
    private static void RegisterDisplayModeRestore(void* directDraw)
    {
        if (s_DisplayModeOwner != null) return;
        s_DisplayModeOwner = directDraw;
        AppDomain.CurrentDomain.ProcessExit += static (_, _) => RestoreDisplayModeIfHeld();
        AppDomain.CurrentDomain.UnhandledException += static (_, _) => RestoreDisplayModeIfHeld();
    }

    private static void RestoreDisplayModeIfHeld()
    {
        void* dd = s_DisplayModeOwner;
        if (dd == null) return;
        s_DisplayModeOwner = null;
        void** vtable = *(void***)dd;
        var restore = (delegate* unmanaged[Stdcall]<void*, int>)vtable[IDirectDraw_RestoreDisplayMode];
        restore(dd);
    }

    /// <summary>
    /// Ghidra: FUN_00542a00 @ 0x00542a00 (`__thiscall`) — **stops playback**. This is the function
    /// that was missing when the attract loop had no reachable exit: it clears the playing flag and
    /// publishes a result, which is what lets PlayMoviePumpLoop return and PlayMovie report non-zero.
    /// </summary>
    private static void StopMoviePlayback(MoviePlayerState player, IntPtr hwnd, int result)
    {
        // Ghidra: if (param_1[8] != 0) { KillTimer(param_2, 1); param_1[8] = 0; }
        if (player.TimerArmed != 0)
        {
            KillTimer(hwnd, 1);
            player.TimerArmed = 0;
        }

        // Ghidra: guarded by param_1[6] (the stream) and *(param_1[6] + 8). The body tears the
        // stream down through its own vtable (FUN_00512c80 / FUN_00543180 / slots 0x18 and 0x08).
        // Stream is null in this reconstruction — TODO(REVERSE) #13 — so the guard skips it exactly
        // as the original would with no stream open. Nothing is invented here.
        if (player.Stream != null)
        {
            // TODO(REVERSE): stream teardown via the unrecoverable vtable; unreachable while
            // TrySetupMovieStream is a stub.
        }

        player.Playing = false;   // Ghidra: *param_1 = 0
        player.Result = result;   // Ghidra: param_1[0xb] = param_3
    }

    /// <summary>Ghidra: FUN_00542970 @ 0x00542970 (`__thiscall`) — WM_ACTIVATEAPP handling.</summary>
    private static void OnMoviePlayerActivate(MoviePlayerState player, int activated)
    {
        if (player.Playing || player.AttractActive != 0)
        {
            if (activated != 0)
            {
                // DEVIATION CONDITIONNELLE — voir s_WindowedStartup. Ce couple SetFocus +
                // SW_MAXIMIZE rend la fenêtre VfwClass plein écran **à chaque activation de
                // l'application** pendant l'attract : la neutraliser à la création ne suffit pas,
                // l'original la rétablit ici. C'est le site le plus important des quatre.
                // Le PostMessage qui suit déclenche le redessin et reste actif dans les deux cas.
                if (!s_WindowedStartup)
                {
                    SetFocus(player.HWnd);
                    ShowWindow(player.HWnd, 3 /* SW_MAXIMIZE */);
                }
                PostMessageA(player.HWnd, 0x111 /* WM_COMMAND */, 1, 1);
                return;
            }
            StopMoviePlayback(player, player.HWnd, 1);
            ShowWindow(player.HWnd, 2 /* SW_SHOWMINIMIZED */);
        }
    }

    /// <summary>Ghidra: FUN_005429e0 @ 0x005429e0 (`__thiscall`) — WM_TIMER: step one movie frame.</summary>
    private static void OnMoviePlayerTimer(MoviePlayerState player, nuint timerId)
    {
        if (timerId == 1 && player.Field28 != 0)
        {
            // TODO(REVERSE): FUN_00512c80 — the DirectDraw wrapper's per-frame flip. Not reachable
            // while the wrapper's vtable is unreconstructed (#13); Field28 is only set once a
            // stream is really open.
        }
    }

    /// <summary>
    /// Ghidra: FUN_00542600 @ 0x00542600 — the `"VfwClass"` window procedure. Ghidra never
    /// disassembled it because the address is only ever *stored* into `WNDCLASSA.lpfnWndProc`,
    /// never called, so it produced no body until the bytes were forced to disassemble; the
    /// listing lives in `Legacy/Raw/FUN_00542600.txt`.
    /// <para>
    /// Its message set is exactly the one the attract-mode title screen waits on, which is what
    /// makes the attract loop terminable at all.
    /// </para>
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static nint MoviePlayerWindowProc(IntPtr hwnd, uint msg, nuint wParam, nint lParam)
    {
        // Ghidra: if (DAT_005dffa8 == 0) return DefWindowProcA(...);
        MoviePlayerState? player = s_MoviePlayer;
        if (player == null) return DefWindowProcA(hwnd, msg, wParam, lParam);

        switch (msg)
        {
            case 0x0002: // WM_DESTROY — falls through to `return 0` in the original, doing nothing
                return 0;

            case 0x0010: // WM_CLOSE
                PostQuitMessage(0);
                return 0;

            case 0x001c: // WM_ACTIVATEAPP
                OnMoviePlayerActivate(player, (int)wParam);
                return 0;

            case 0x0100: // WM_KEYDOWN — only Escape stops playback; other keys fall through to 0
                if (wParam == 0x1b /* VK_ESCAPE */)
                {
                    StopMoviePlayback(player, hwnd, 1);
                }
                return 0;

            case 0x0113: // WM_TIMER
                OnMoviePlayerTimer(player, wParam);
                return 0;

            case 0x0201: // WM_LBUTTONDOWN
            case 0x0204: // WM_RBUTTONDOWN
            case 0x0207: // WM_MBUTTONDOWN
            case 0x03b5: // MM_JOY1BUTTONDOWN
            case 0x03b6: // MM_JOY2BUTTONDOWN
                StopMoviePlayback(player, hwnd, 1);
                return 0;

            case 0x0464: // MM_MCINOTIFY
                // Ghidra: iVar2 = FUN_00542560(hwnd, wParam, lParam); if (iVar2 == 0) return 0;
                //         FUN_00542a00(hwnd, 0);
                // TODO(REVERSE): FUN_00542560 is in Blaze.c but only matters once MCI playback is
                // actually started, which this reconstruction never does. Treated as "not handled".
                return 0;

            default:
                return DefWindowProcA(hwnd, msg, wParam, lParam);
        }
    }

    /// <summary>Ghidra: FUN_00542740 @ 0x00542740 — creates the hidden window the movie player renders into.</summary>
    private static void CreateMoviePlayerWindow(MoviePlayerState player)
    {
        IntPtr hInstance = GetModuleHandleA(null);
        player.HInstance = hInstance;

        // Ghidra: SUB ESP,0x28 reserves exactly one WNDCLASSA, and the ten stores that follow are
        // its fields in order (Legacy/Raw/FUN_00542740.txt). Note the cursor comes from
        // wsyskern.dll's resource 10, not from a system cursor.
        var wc = new WNDCLASSA
        {
            style = 0,
            lpfnWndProc = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, uint, nuint, nint, nint>)&MoviePlayerWindowProc,
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = hInstance,
            hIcon = IntPtr.Zero,
            hCursor = LoadCursorA(GetModuleHandleA(ToAnsiZ("WSYSKERN.DLL")), (IntPtr)10),
            hbrBackground = GetStockObject(4 /* BLACK_BRUSH */),
            lpszMenuName = null,
            lpszClassName = s_VfwClassName,
        };

        RegisterClassA(&wc);

        // Ghidra: CreateWindowExA(8, "VfwClass", "Blaze & Blade", 0x80000000, 0, 0, 10, 10, 0, 0,
        //                          hInstance, 0) — WS_EX_TOPMOST + WS_POPUP, created tiny and
        // resized below, which is why this window is invisible in practice.
        player.HWnd = CreateWindowExA(
            8 /* WS_EX_TOPMOST */, s_VfwClassName, s_MovieWindowTitle, 0x80000000 /* WS_POPUP */,
            0, 0, 10, 10, IntPtr.Zero, IntPtr.Zero, hInstance, null);

        // Ghidra: *ESI = 0; *(ESI+0xc) = 0 — done before CreateWindowExA in the original; ordering
        // is immaterial since the window procedure early-outs while DAT_005dffa8 is null.
        player.Playing = false;
        player.TitleBitmapLoaded = 0;

        // DEVIATION CONDITIONNELLE — voir s_WindowedStartup.
        // Ces deux appels sont la moitié « fenêtre » du plein écran de démarrage : un popup
        // WS_EX_TOPMOST maximisé puis étendu à 2000x2000. La fenêtre reste créée — `joySetCapture`
        // et la procédure de fenêtre en dépendent — mais n'est plus montrée.
        // Mesuré (tâche 2.2) : les touches terminent toujours l'attract, la boucle qui les lit
        // faisant un `PeekMessageA(NULL, …)` à l'échelle du thread.
        if (s_WindowedStartup) return;

        ShowWindow(player.HWnd, 3 /* SW_MAXIMIZE */);
        MoveWindow(player.HWnd, 0, 0, 0x7d0, 0x7d0, 1);
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
        // FUN_00512930's disassembly (Legacy/Raw/FUN_00512930.txt) ends `MOV EAX,ESI; RET 0x4` — it
        // returns `this` unconditionally, even when its own internal Malloc fails. So the original's
        // `if (iVar1 == 0) return 0` only fires when the 0x48-byte allocation below fails, and the
        // open otherwise **succeeds**. A previous revision returned false here and justified it as
        // the original's no-DirectDraw path; that justification was wrong, and the deviation is now
        // unnecessary because the window procedure is reconstructed and the attract loop can end.
        void* ddrawWrapper = Malloc(0x48);
        if (ddrawWrapper == null) return false;

        // Ghidra: iVar1 = FUN_00512930(iVar1, param_1[0x10]) — the wrapper's constructor. It is not
        // optional: Malloc does not zero, and the teardown reads slots this initialises.
        ConstructDirectDrawWrapper((uint*)ddrawWrapper);
        player.DirectDrawWrapper = ddrawWrapper;

        player.Field24 = 2;   // Ghidra: *(param_1 + 0x24) = 2
        player.Field28 = 1;   // Ghidra: *(param_1 + 0x28) = 1

        void* stream = Malloc(0x120);
        player.Stream = null; // TODO(REVERSE): FUN_005431e0 builds a DirectShow graph object here
                              // (CLSID_FilterGraph / IID_IGraphBuilder, see #13). Left null so every
                              // stream-guarded branch takes the original's own "no stream" path.
        if (stream != null) Free(stream);

        // Ghidra: FUN_00512a10(*(param_1 + 0x10)) — the tail call of FUN_00542800, with the wrapper
        // in ECX and the window handle as its stack argument. This is what fills wrapper[1] with the
        // primary surface, so the attract screen's blit has something to draw into.
        SetupDirectDrawSurfaces((uint*)player.DirectDrawWrapper, player.HWnd);

        return true; // faithful: the original returns 1 once the wrapper allocation succeeded
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

        // DEVIATION CONDITIONNELLE — voir s_WindowedStartup.
        if (player.HWnd != IntPtr.Zero && !s_WindowedStartup)
        {
            MoveWindow(player.HWnd, 0, 0, 2000, 2000, 1);
        }

        if (player.DirectDrawWrapper != null)
        {
            // Ghidra: FUN_00512b40(param_1[4]) — the real teardown. Skipping it is not harmless:
            // it is what drops the exclusive display mode, and without it winsys3d.dll's own 3D
            // init (Ordinal_789) cannot acquire DirectDraw and aborts the process.
            ReleaseDirectDrawSurfaces((uint*)player.DirectDrawWrapper, player.HWnd);
            Free(player.DirectDrawWrapper);
        }
        player.DirectDrawWrapper = null;

        // DEVIATION CONDITIONNELLE — voir s_WindowedStartup.
        if (player.HWnd != IntPtr.Zero && !s_WindowedStartup)
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
        // Ghidra: the GetDC / StretchDIBits / ReleaseDC sequence at Blaze.c:89916-89929.
        BlitTitleScreen(player);
        // ---------------------------------------------------------------------------------------

        // Ghidra: DVar4 = timeGetTime(); *(param_1+4) = 1; joySetCapture x2; then a message pump
        // that exits with 1 on input and with 0 once 19999 ms have elapsed since the last redraw.
        uint lastDraw = timeGetTime();
        player.AttractActive = 1;
        joySetCapture(player.HWnd, 0, 0x32, 1);
        joySetCapture(player.HWnd, 1, 0x32, 1);

        MSG msg;
        while (true)
        {
            while (PeekMessageA(&msg, IntPtr.Zero, 0, 0, 1 /* PM_REMOVE */) != 0)
            {
                // Ghidra: the key/mouse/joystick set that ends the attract screen. Handled here
                // *and* in the window procedure — the original tests them in this loop directly,
                // which is what makes the wait end even when the message is not dispatched.
                if (msg.message is 0x100 or 0x201 or 0x204 or 0x207 or 0x3b5 or 0x3b6)
                {
                    WsysFree(player.TitleBitmap);
                    player.TitleBitmap = null;
                    player.TitleBitmapLoaded = 0;
                    player.AttractActive = 0;
                    joyReleaseCapture(0);
                    joyReleaseCapture(1);
                    return 1;
                }

                // Ghidra: WM_COMMAND with lParam == 1 re-blits and restarts the 20-second clock.
                if (msg.message == 0x111 && msg.lParam == 1)
                {
                    BlitTitleScreen(player);
                    lastDraw = timeGetTime();
                }

                if (msg.message == 0x12 /* WM_QUIT */)
                {
                    RaiseException(0xe0000001, 0, 0, null);
                }

                DispatchMessageA(&msg);
            }

            // Ghidra: if (19999 < timeGetTime() - DVar4) { *(param_1+4) = 0; joyRelease; return 0; }
            if (timeGetTime() - lastDraw > 19999)
            {
                player.AttractActive = 0;
                joyReleaseCapture(0);
                joyReleaseCapture(1);
                return 0;
            }
        }
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
    private const int DrawEnvSize = 92;   // 0x5C
    private const int DispEnvSize = 20;   // 0x14

    // Les quatre structures sont **contiguës** dans le binaire, et cette contiguïté est un contrat,
    // pas un hasard de placement :
    //
    //     0x005e9880  DrawEnv0  92 octets
    //     0x005e98dc  DispEnv0  20      <- DrawEnv0 + 0x5C
    //     0x005e98f0  DrawEnv1  92      <- DrawEnv0 + 0x70
    //     0x005e994c  DispEnv1  20      <- DrawEnv1 + 0x5C
    //     0x005e9960  (s_FadeCounter, la globale suivante — d'où la taille totale de 224)
    //
    // FrameEnd publie l'environnement d'affichage par `Ordinal_657(nextDrawEnv + 0x5c)` : il
    // *dépend* du fait que le DISPENV suive immédiatement son DRAWENV. Les allouer en quatre blocs
    // séparés faisait lire 20 octets de tas quelconque à la place du DISPENV — d'où un écran noir
    // intermittent, mesuré à 7 lancements sur 10, selon ce que le tas contenait à cet endroit.
    // Voir ANALYSE_ECRAN_NOIR_MENU.md pour la mesure qui a isolé la cause.
    private const int EnvBlockSize = 224;   // 0x005e9960 - 0x005e9880
    private static readonly byte* s_EnvBlock_005e9880 = (byte*)NativeMemory.AllocZeroed(EnvBlockSize);

    private static readonly byte* s_DrawEnv0_005e9880 = s_EnvBlock_005e9880 + 0x00;
    private static readonly byte* s_DispEnv0_005e98dc = s_EnvBlock_005e9880 + 0x5C;
    private static readonly byte* s_DrawEnv1_005e98f0 = s_EnvBlock_005e9880 + 0x70;
    private static readonly byte* s_DispEnv1_005e994c = s_EnvBlock_005e9880 + 0xCC;

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
        // La fenêtre du jeu naît ici, avant l'écran-titre — pas de device, il attend le premier
        // début de scène. Hors reconstruction : le binaire d'origine n'a pas de fenêtre à lui à ce
        // stade, il dessine dans l'affichage exclusif. Voir la change `render-title-screen-in-window`.
        CreateGameWindow();

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
        s_RandSeed_0056e9c0 = 1; // Ghidra: FUN_00547130(1) — srand(1); see CrtRand
        s_Unknown_005e9630 = Ordinal_116();
        Ordinal_771();
        InitTrigTables(); // Ordinal_337, re-run (idempotent — see winsys3d.md)
        // Ghidra: Ordinal_789(0, DAT_00566cc4) — Blaze.c line 79100. The second argument is **not**
        // inert, as an earlier note here claimed: winsys3d.c FUN_00a06420 opens with
        // `DAT_00a2c49c = param_2`, and that global indexes the base-offset tables SetShift uses to
        // position the viewport. Passing 0 selected the 320x240 entry (-160,-120), which is why the
        // scene rendered unscaled in a corner of a 640x480 screen.

        // `Ordinal_789` charge `3dhelp.dll` et lui fait créer son device, ce qui bascule le mode
        // d'affichage du bureau. Le device synthétique intercepte cette création. Voir
        // `SyntheticDevice.cs` ; opt-in tant qu'il n'est pas éprouvé.
        InstallSyntheticDevice();

        Ordinal_789(0, s_ResolutionIndexForClamp);
        InstallDeviceShim();   // ECHAFAUDAGE TEMPORAIRE — voir DeviceShim.cs
        LogDisplayMode("après init 3D"); // ECHAFAUDAGE TEMPORAIRE
        ShutdownMoviePlayer(); // Ghidra: FUN_00542f90()
        CheckDeviceCapabilityFlag(); // Ghidra: FUN_0050f120()
        Ordinal_110(0);
        Ordinal_681(0);
        SetShadingDefaults(); // Ghidra: FUN_00529e40()
        InitViewportSurfaces(); // Ghidra: FUN_00529eb0()
        // Ghidra: FUN_0040a600() — confirmed no-op (`return;` with an empty body).
        SetupJoystickHandlers(); // Ghidra: FUN_00504460()

        int detail = s_SizeSetting_005de160;
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
    private static uint s_RandSeed_0056e9c0; // Ghidra: DAT_0056e9c0 — the CRT rand seed
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
    /// <summary>
    /// libgpu's `TIM_IMAGE`, filled by ReadTIM. Exactly the 20 bytes FUN_005322f0 reserves with
    /// `SUB ESP,0x14` and hands to Ordinal_662 via `LEA ECX,[ESP+8]`.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct TimImage
    {
        public uint Mode;        // +0x00 — pixel mode; header & 7, or 2 when the TIM has no CLUT
        public short* ClutRect;  // +0x04 — RECT {x,y,w,h}; null when there is no CLUT
        public void* ClutData;   // +0x08 — palette bytes; null when there is no CLUT
        public short* PixelRect; // +0x0c — RECT {x,y,w,h}
        public void* PixelData;  // +0x10 — pixel bytes
    }

    /// <summary>
    /// Ghidra: FUN_005322f0 @ 0x005322f0 — uploads one TIM texture (loaded by LoadResourceEntry
    /// into a scratch buffer) into VRAM, and records its texture-page / CLUT pair in a slot.
    /// <para>
    /// Reconstructed from the raw disassembly in <c>Legacy/Raw/FUN_005322f0.txt</c>, because the
    /// decompiler's output for this function was not merely unnamed but **wrong**:
    /// </para>
    /// <list type="bullet">
    /// <item>Its `puStack_14`/`psStack_10`/`iStack_c` "indeterminate" locals are the fields of the
    /// libgpu <c>TIM_IMAGE</c> that <c>Ordinal_662</c> (ReadTIM) fills through the out-pointer
    /// `&amp;stack0xffffffe8`. Ghidra never linked the pointer to the slots it writes.</item>
    /// <item>`unaff_retaddr` is not a return address: it is **parameter 2**, the 2-short texture
    /// slot. Both call sites advance their slot pointer by exactly 4 bytes, which corroborates it.</item>
    /// <item>Every `param_1[0..2]` byte read in the decompiled body actually comes from
    /// **`EDI` = parameter 3**, the metadata triple — not from the chunk pointer.</item>
    /// <item>`(*param_1 &amp; 0xff10)` is a decompiler artifact. The instruction is `AND AL,0x10`,
    /// a byte mask; using `0xff10` would corrupt the pixel rect's y coordinate.</item>
    /// </list>
    /// <para>
    /// The `extraout_var*` values Ghidra invented for this function are simply Ordinal_674's
    /// ignored return value and have no role at all.
    /// </para>
    /// </summary>
    private static void DecodeResourceChunk(byte* timData, ushort* slot, byte* meta)
    {
        // Ghidra: PUSH arg1; CALL Ordinal_677; LEA ECX,[ESP+8]; PUSH ECX; CALL Ordinal_662.
        // The 20 bytes SUB ESP,0x14 reserves are exactly this struct.
        TimImage tim;
        OpenTim(timData);
        ReadTim(&tim);

        // ESI carries the CLUT's byte count from the branch below into the copy loop at the end.
        // It is only meaningful when a CLUT exists; the no-CLUT path loads arg1 into it (Ghidra:
        // `MOV ESI,[ESP+0x20]`) but that value is dead, because ReadTIM zeroes `caddr` in the same
        // case and `caddr` guards the loop.
        int clutByteCount = 0;

        // Ghidra: MOV EAX,[ESP+0x10]; TEST EAX,EAX; JZ — the test is on **caddr**, not crect.
        if (tim.ClutData != null)
        {
            short* clutRect = tim.ClutRect;

            clutRect[0] = 0;                                    // x = 0
            clutRect[1] = (short)(meta[1] + 0x1c0);             // y = meta[1] + 0x1c0

            // Read w and h *before* w is overwritten — the original does the same, and the
            // multiply below depends on the original width.
            int packed = clutRect[3] * clutRect[2];             // h * w
            packed = (packed + 0xff) & unchecked((int)0xffffff00);
            clutRect[2] = 0x100;                                // w = 256
            clutRect[3] = (short)(packed >> 8);                 // h = rounded / 256
            clutByteCount = packed;

            Ordinal_674(clutRect, tim.ClutData);                // LoadImage(crect, caddr)
        }

        // Ghidra: MOV EAX,[ESP+0x18] — guarded on **paddr**, plus a skip when meta[0] == 0x1c.
        if (tim.PixelData != null && meta[0] != 0x1c)
        {
            short* pixelRect = tim.PixelRect;

            // Ghidra: AND AL,0xf; SHL ECX,6.
            pixelRect[0] = (short)((meta[0] & 0x0f) << 6);

            // Ghidra: AND AL,0x10 — a *byte* mask. The decompiler rendered this as `& 0xff10`,
            // which is wrong and would have corrupted the y coordinate.
            pixelRect[1] = (short)((((meta[0] & 0x10) + meta[2] * 4)) << 4);

            Ordinal_674(pixelRect, tim.PixelData);              // LoadImage(prect, paddr)
        }

        // Ghidra: 005323bf-005323f2. Packs the texture-page word out of the pixel rect and the TIM
        // mode — libgpu's GetTPage shape. Transcribed operation for operation rather than
        // simplified, since the intermediate 16-bit SAR is not equivalent to a 32-bit one.
        int x = tim.PixelRect[0];
        int y = tim.PixelRect[1];
        int tpage = ((x >> 2) & 0xf0) | (y & 0x100);
        tpage |= 0x2000;                                        // Ghidra: OR CH,0x20
        tpage = (short)tpage >> 4;                              // Ghidra: SAR CX,4 — 16-bit
        tpage |= ((int)tim.Mode & 3) << 7;
        slot[0] = (ushort)tpage;

        // Ghidra: (meta[1] + 0x1c0) << 6 — libgpu's GetClut for x=0, matching the crect y above.
        slot[1] = (ushort)((meta[1] + 0x1c0) << 6);

        // Ghidra: 00532407-0053243c. Mirrors the CLUT into main video memory for the low banks.
        if (meta[1] < 0x10 && tim.ClutData != null)
        {
            byte* mainMem = GetMainMemPtr();
            if (mainMem != null)
            {
                uint* source = (uint*)tim.ClutData;
                uint* dest = (uint*)(mainMem + ((meta[1] + 0xe10) << 9));
                for (int remaining = clutByteCount >> 1; remaining != 0; remaining--)
                {
                    *dest++ = *source++;
                }
            }
        }
    }

    // Ghidra: DAT_005e8fc0 — **not a second table**. It is DAT_005e8f00 offset by 0xC0, i.e. the
    // same tpage/clut array starting 48 entries in, and Ghidra simply named two points inside it.
    //
    // Modelling them as separate allocations is what left the main menu black: its panel
    // descriptor asks for entry 0x31 of DAT_005e8f00 = 0x31 - 0x30 = entry 1 of DAT_005e8fc0,
    // which LoadFirstLevelResources fills. LoadMenuResources only writes entries 0..3 (measured:
    // the menu's directory has exactly four), so entry 0x31 came back zero and every panel
    // primitive sampled texture page 0 — an empty region of VRAM, hence a black screen with
    // otherwise perfectly formed geometry.
    //
    // Same shape of bug as DAT_0056e61f being menuContext[0x8c7]: suspect any DAT_ that lands
    // inside a known structure's extent.
    private static byte* s_EntityDecodeTable_005e8fc0 => s_EntityDecodeTable_005e8f00 + 0xC0;
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
                    DecodeResourceChunk(scratch, (ushort*)destSlot, (byte*)metadata);
                    entryPtrValue = directory[2];
                    directory += 2;
                    // Ghidra: `undefined *puVar13; ... puVar13 = puVar13 + 4;` — a **byte** pointer
                    // advancing 4 bytes, i.e. one tpage/clut pair per entry. This was 16, a 4x
                    // over-stride that would have scattered the texture slots; it went unnoticed
                    // because DecodeResourceChunk threw before the loop could ever iterate.
                    destSlot += 4;
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

    // =====================================================================
    // Game state 0 — the main menu (Ghidra: FUN_00542130 and its subtree).
    //
    // Rendering follows the PlayStation display-list model the rest of winsys3d already showed:
    // FrameBegin prepares a primitive buffer and an ordering table, each renderer links its
    // primitive into the OT and returns the advanced buffer cursor, and FrameEnd walks the OT and
    // swaps buffers. The OT pointer is threaded through return values — that threading *is* the
    // display list, so it is modelled as written rather than hidden behind a field.
    // =====================================================================

    // Ghidra: DAT_005e9300 — which of the two draw envs is current, i.e. the back/front selector.
    private static byte* s_CurrentDrawEnv;
    private static int s_Unknown_005e8310;
    private static int s_Unknown_005e9818;
    private static uint s_FadeCounter_005e9960;      // Ghidra: DAT_005e9960
    private static uint s_Unknown_005e9814;
    private static int s_Unknown_005de154, s_Unknown_005de158, s_Unknown_005de15c;
    private static int s_Unknown_005de51c;
    private static int s_MenuSelection_005e909f;     // Ghidra: DAT_005e909f — the selected entry
    // Ghidra names DAT_0056e61f as its own symbol, but it is **inside** the menu context:
    // 0x0056e61f - 0x0056dd58 = 0x8c7, the descriptor's flags byte. Modelling it separately made
    // the confirm gate always open, so the menu selected an entry on its very first frame. Bit 2
    // is the one Handler_00544b10 raises when the text stream ends.
    private static byte s_Unknown_0056e61f => s_MenuContext_0056dd58[0x8c7];
    private static int s_Unknown_005d9de0;
    private static int s_Unknown_00564a48, s_Unknown_00571a3c;

    /// <summary>
    /// Ghidra: FUN_0052a1e0 @ 0x0052a1e0 — begins a frame: picks the scratch bases out of MainMem
    /// according to which draw env is current, clears a 1024-entry ordering table
    /// (<c>Ordinal_688</c>, libgpu's ClearOTagR) and resets the primitive cursor.
    /// <para>
    /// Ghidra recovers only two parameters where the call sites pass three, and renders both
    /// writes as <c>*param_1</c>. **Confirmed against the disassembly**
    /// (`Legacy/Raw/FUN_0052a1e0.asm`): the function has no prologue, so its arguments sit at
    /// `[ESP+4]`, `[ESP+8]` and `[ESP+0xc]`, and `0052a257 MOV ECX,[ESP+0xc]` shows the second
    /// write targets **arg3** — the primitive buffer. Plain `RET` with the caller's `ADD ESP,0xc`
    /// makes it cdecl with three arguments.
    /// </para>
    /// </summary>
    private static void FrameBegin(uint** otBase, uint** otEntry, uint** primCursor)
    {
        // Every caller uses all three out-parameters immediately, so they must be written on every
        // path. Returning early without setting them left uninitialised stack in the caller and
        // took DrawOTag down with a null-page access — a defect in this reconstruction, not in the
        // original, which has no such early exit.
        *otBase = null;
        *otEntry = null;
        *primCursor = null;

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "FUN_0052a1e0: winsys3d.dll's MainMem export resolves to null, so the frame's " +
                "ordering table and primitive buffer have nowhere to live — see REVERSE_STATUS.md " +
                "TODO #33. Every frame is built inside that arena, so this cannot degrade quietly.");
        }

        bool firstEnv = s_CurrentDrawEnv == s_DrawEnv0_005e9880;

        uint* ot = (uint*)(mainMem + (firstEnv ? 0x1c7000 : 0x1c6000));
        *otBase = ot;
        *otEntry = (uint*)((byte*)ot + 0xffc);   // Ghidra: *param_2 = iVar1 + 0xffc
        Ordinal_688(ot, 0x400);                   // ClearOTagR(ot, 1024)
        s_Unknown_005e8310 = 0;

        *primCursor = (uint*)(mainMem + (firstEnv ? 0x1e4000 : 0x1c8000));
        s_Unknown_005e9818 = 0x9c4;

        // Ghidra: FUN_00533000(DAT_005e97c0) — palette animation, once per frame.
        AnimatePalettes(s_MainResourceBuffer_005e97c0);
    }

    /// <summary>
    /// Ghidra: FUN_0052a280 @ 0x0052a280 — ends a frame: advances the frame counter, swaps the
    /// draw/display env pair, walks the ordering table and immediately begins the next frame.
    /// <para>
    /// Ghidra recovers **no** parameters for this function, yet every call site passes five. The
    /// mapping below is **confirmed against the disassembly** (`Legacy/Raw/FUN_0052a280.asm`):
    /// tracing `SUB ESP,0x14` plus two `PUSH`es puts the arguments at `[ESP+0x20]`..`[ESP+0x30]`,
    /// giving `Ordinal_701(*arg2)`, `Ordinal_132(arg4)`, the guard on `arg5`, and the tail call
    /// `FUN_0052a1e0(arg1, arg2, arg3)` — cdecl, three arguments, `ADD ESP,0xc`.
    /// </para>
    /// </summary>

    private static void FrameEnd(uint** otBase, uint** otEntry, uint** primCursor, int mode, int flag)
    {
        CrtRand();                             // Ghidra: FUN_00547140() — result discarded here

        s_FadeCounter_005e9960++;

        int cosine = GetCosTableEntry((int)(s_FadeCounter_005e9960 * 0x200));
        sbyte level = (sbyte)((cosine >> 6) - 0x80);
        s_Unknown_005e9814 = (uint)(((byte)level << 16) | ((byte)level << 8) | (byte)level);

        Ordinal_667(0);

        // Ghidra: puVar3 = &DAT_005e9880; if (DAT_005e9300 == &DAT_005e9880) puVar3 = &DAT_005e98f0;
        // — the buffer swap: pick the *other* draw env, then publish its matching disp env at +0x5c.
        byte* nextDrawEnv = s_CurrentDrawEnv == s_DrawEnv0_005e9880
            ? s_DrawEnv1_005e98f0
            : s_DrawEnv0_005e9880;
        s_CurrentDrawEnv = nextDrawEnv;

        Ordinal_657(nextDrawEnv + 0x5c);       // the DISPENV sits 0x5c past the DRAWENV
        Ordinal_660(nextDrawEnv);

        FlushDeferredImageUploads();           // Ghidra: FUN_00545140()

        Ordinal_701(*otEntry);                 // DrawOTag — asm: Ordinal_701(*arg2)

        // asm 0052a314: MOV ECX,[ESP+0x2c] — that slot is **arg4**, not arg5. The `if` below tests
        // [ESP+0x30], which is arg5. An earlier reading passed the same value to both.
        Ordinal_132(mode);

        if (flag != 0)
        {
            PollInputAndAudio();               // Ghidra: FUN_00512520 / FUN_005048f0 /
                                               //         FUN_00504a30 / FUN_005357a0
        }

        FrameBegin(otBase, otEntry, primCursor);

        // Ghidra: if (DAT_00571a3c != 0) draw the "Veuillez connecter la manette !" banner.
        if (s_Unknown_00571a3c != 0)
        {
            ControllerBannerStub();            // Ghidra: FUN_0051c120(...)
        }
    }

    /// <summary>Ghidra: FUN_00529a80 @ 0x00529a80 — screen/viewport setup for the menu state.</summary>
    private static void SetupMenuScreen()
    {
        // Ghidra: a small struct at DAT_00566c48 filled with 0xcf / 0x3fff clip values, then two
        // ordinal calls. Modelled as a raw block because none of its fields are individually named.
        uint* block = s_MenuScreenBlock_00566c48;
        block[0] = 0xcf;
        *(ushort*)((byte*)block + 4) = 0x3fff;
        *(ushort*)((byte*)block + 6) = 0x3fff;
        *(ushort*)((byte*)block + 8) = 0;
        *(ushort*)((byte*)block + 0x0a) = 0;
        *(ushort*)((byte*)block + 0x10) = 0x3fff;
        *(ushort*)((byte*)block + 0x12) = 0x3fff;

        Ordinal_762(block);
        Ordinal_97(0xc, 0, 0);
    }

    // Ghidra: &DAT_005e8f00 — the menu state's own tpage/clut slot table, distinct from the
    // 0x005e8fc0 one LoadFirstLevelResources fills. Same 4-byte-per-entry layout.
    private static readonly byte* s_EntityDecodeTable_005e8f00 = (byte*)NativeMemory.AllocZeroed(0x1000);

    // Ghidra: DAT_005e97c8 — the tail pointer LoadResourceEntry returns for the state's own load.
    private static byte* s_Unknown_005e97c8;

    // winsys3d.dll named exports (ordinal 776 and its setter) — the viewport shift the options page
    // saves and restores around its own screen.
    [DllImport("winsys3d.dll")] private static extern void GetAbsShift(int* x, int* y);
    [DllImport("winsys3d.dll")] private static extern void SetAbsShift(int x, int y);

    // Ghidra: _DAT_0054d074 — the float the gamma value is scaled by before CreateTranslationTables.
    // TODO(REVERSE): .rdata content; 1.0f keeps the product equal to the raw setting.
    private const float s_GammaScale_0054d074 = 1.0f;

    // Ghidra: PTR_s____Music_Mel00_01_WAV_00566cc8 — the music filename table. Entries recovered
    // from Legacy/Blaze.exe are CD-root absolute templates; see Documentation/DataSymbols.md.
    // TODO(REVERSE): only the sampled entries are transcribed. Call sites index up to at least
    // 0x26, so the remaining slots stay null and PlayMusicTrack returns without starting anything
    // rather than indexing past the table.
    private static readonly byte*[] s_MusicTrackPaths = BuildMusicTrackTable();

    private static byte*[] BuildMusicTrackTable()
    {
        var table = new byte*[0x30];
        table[0] = ToAnsiZ("@:/Music/Mel00_01.WAV");
        table[1] = ToAnsiZ("@:/Music/Mel00_02.WAV");
        table[7] = ToAnsiZ("@:/Music/End01.WAV");
        return table;
    }

    private static readonly uint* s_MenuScreenBlock_00566c48 = (uint*)NativeMemory.AllocZeroed(0x40);
    private static readonly uint* s_MenuRenderBlock_00566c70 = (uint*)NativeMemory.AllocZeroed(0x40);

    /// <summary>
    /// Ghidra: FUN_00542160 @ 0x00542160 — reloads the state's resources. Its directory loop is the
    /// same one <see cref="LoadFirstLevelResources"/> already runs, so it reuses the same three
    /// reconstructed helpers rather than duplicating them.
    /// </summary>
    private static void LoadMenuResources()
    {
        s_Unknown_005e97c8 = LoadResourceEntry((int*)(nint)s_Unknown_005e92b0, s_MainResourceDirectory_005e97c4);
        FixupResourceHeader(s_MainResourceDirectory_005e97c4);

        if (s_MainResourceDirectory_005e97c4 != null)
        {
            int* directory = *(int**)(s_MainResourceDirectory_005e97c4 + 0x10);
            int entryPtrValue = *directory;
            if (entryPtrValue != 0)
            {
                int* metadata = directory + 1;
                byte* destSlot = s_EntityDecodeTable_005e8f00;
                byte* mainMem = GetMainMemPtr();
                do
                {
                    byte* scratch = mainMem != null ? mainMem + 0x1b0000 : null;
                    LoadResourceEntry((int*)(nint)entryPtrValue, scratch);
                    DecodeResourceChunk(scratch, (ushort*)destSlot, (byte*)metadata);
                    entryPtrValue = directory[2];
                    directory += 2;
                    destSlot += 4;      // same 4-byte tpage/clut stride as the other call site
                    metadata += 2;
                } while (entryPtrValue != 0);
            }
        }

        // Ghidra: _DAT_00566c74 = 0x100; _DAT_00566c78 = 0; _DAT_00566c7a = 0; then six ordinals.
        uint* block = s_MenuRenderBlock_00566c70;
        *(uint*)((byte*)block + 4) = 0x100;
        *(ushort*)((byte*)block + 8) = 0;
        *(ushort*)((byte*)block + 0x0a) = 0;

        Ordinal_770(block);
        Ordinal_769(1);
        Ordinal_768(1);
        Ordinal_767(0, 0xffffff);
        Ordinal_766(block);
        Ordinal_765(1, 0xffffff);
    }

    /// <summary>Ghidra: FUN_00542130 @ 0x00542130 — game state 0's entry point: the main menu.</summary>
    private static void RunMainMenuState()
    {
        LoadMenuResources();      // Ghidra: FUN_00542160()
        SetupMenuScreen();        // Ghidra: FUN_00529a80()
        s_FadeCounter_005e9960 = 0;

        // Ghidra: while (DAT_005de510 == 0) FUN_00542250();
        while (s_GameStateId == 0)
        {
            RunMainMenuFrameLoop();
        }
    }

    /// <summary>Ghidra: FUN_00529ad0 @ 0x00529ad0 — starts a music track by table index.</summary>
    private static void PlayMusicTrack(int trackIndex)
    {
        // Ghidra: patches the path's first character with the CD drive letter, then starts the
        // stream. The table entries are CD-**root** absolute templates ("@:/Music/Mel00_01.WAV"),
        // recovered at PTR_s____Music_Mel00_01_WAV_00566cc8 — see Documentation/DataSymbols.md.
        if (trackIndex < 0 || trackIndex >= s_MusicTrackPaths.Length) return;
        byte* path = s_MusicTrackPaths[trackIndex];
        if (path == null) return;
        path[0] = s_CdDataRootPath[0];
        StartMusicStream(path);
    }

    /// <summary>
    /// Ghidra: FUN_00542250 @ 0x00542250 — the main menu's frame loop. Runs until a menu entry is
    /// chosen, then sets the next game state (or exits) and returns.
    /// </summary>


    private static void RunMainMenuFrameLoop()
    {
        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);

        PlayMusicTrack(6);

        // Ghidra: DAT_005e9898 = 1; DAT_005e9908 = 1 — the two draw envs' `isbg`.
        *(s_DrawEnv0_005e9880 + 0x18) = 1;
        *(s_DrawEnv1_005e98f0 + 0x18) = 1;

        JoystickResetState(false);

        // Ghidra: the entity-slot table init. The table it reads from is selected by two joystick
        // axis comparisons against -0x8000 (a disconnected/centred sentinel), producing an index
        // 0..3 into a four-entry pointer table at 0x0056e660.
        // TODO(REVERSE): that table is .data content and its four entries are not modelled, so the
        // per-slot byte writes into DAT_00604a2a are skipped. The slot table itself is initialised
        // exactly as the original does.
        InitialiseEntitySlots();

        s_Unknown_005de538 = 0;

        // Ghidra: FUN_00544350(&DAT_0056e670, 0, &DAT_0056dd58, 0)
        BindMenuLabels(s_MenuLabels_0056e670, 0, s_MenuContext_0056dd58, 0);

        while (true)
        {
            s_Unknown_005de15c = 1;

            // Ghidra: the fade. uVar3 is the grey level; it walks down while the low nibble of the
            // frame counter is not 8 and the level is still above 0x7f.
            uint level = (s_FadeCounter_005e9960 & 0xf) * 0x10;
            s_Unknown_005de154 = (0xb - s_SizeSetting_005de160) * 0x10;
            s_Unknown_005de158 = (0xb - s_SizeSetting_005de160) * 0x0c;

            if ((s_FadeCounter_005e9960 & 0xf) != 8 && level > 0x7f)
            {
                s_FadeCounter_005e9960--;
                level = 0x80;

                primCursor = RunTextEngine(primCursor, s_MenuContext_0056dd58);  // Ghidra: FUN_005443a0
                primCursor = DrawUiPanel(otEntry, primCursor, s_MenuContext_0056dd58);  // Ghidra: FUN_00545340

                int gammaBefore = s_Unknown_005d9de0;

                if ((s_Unknown_0056e61f & 4) == 0)
                {
                    switch (s_MenuSelection_005e909f)
                    {
                        case 0: s_GameStateId = 1; break;
                        case 1: s_GameStateId = 4; break;
                        case 2: s_GameStateId = 6; break;

                        case 3:
                            // The options page. Drains the input queue, rebuilds textures, runs the
                            // options UI, writes the settings back and rebuilds the gamma tables if
                            // the value changed.
                            while (InputDrainStub() != 0) { }
                            ReInitializeAllTextures();
                            int shiftX, shiftY;
                            GetAbsShift(&shiftX, &shiftY);
                            SetAbsShift(0, 0);
                            if (s_SizeSetting_005de160 < 7) s_SmallTextureFlag = 1;
                            OptionsScreenStub();                        // Ghidra: FUN_00512320
                            SetAbsShift(shiftX, shiftY);
                            SaveOptionsIni();
                            if (gammaBefore != s_Unknown_005d9de0)
                            {
                                CreateTranslationTables(s_Unknown_005d9de0 * s_GammaScale_0054d074);
                            }
                            break;

                        case 4:
                            Exit(0);   // Ghidra: FUN_00547770(0)
                            break;
                    }

                    if (s_GameStateId != 0)
                    {
                        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
                        s_Unknown_005de15c = 0;
                        s_Unknown_005de51c = 0;
                        return;
                    }
                }
            }

            s_Unknown_005de15c = 0;

            // Ghidra: the per-frame draw. local_10 is the grey fill (level * 0x10101), and the
            // literals below are the sprite's position/size block the two renderers read.
            uint* drawBlock = s_MenuDrawBlock;
            drawBlock[0] = level * 0x10101;
            *(ushort*)((byte*)drawBlock + 4) = 0xa0;
            *(ushort*)((byte*)drawBlock + 6) = 0x78;
            *(ushort*)((byte*)drawBlock + 8) = 0x1420;
            *(ushort*)((byte*)drawBlock + 0x0a) = 0x1149;
            *(ushort*)((byte*)drawBlock + 0x0c) = 0;

            // Ghidra: local_14 = *(*(DAT_005e97c4 + 0x94) + 4)
            int* sprite = s_MenuSpriteRef;
            if (s_MainResourceDirectory_005e97c4 != null)
            {
                int* table = *(int**)(s_MainResourceDirectory_005e97c4 + 0x94);
                if (table != null) sprite[0] = table[1];
            }

            primCursor = DrawSpritePrimitive(otEntry, primCursor, sprite, 0, 0);
            primCursor = DrawFlatPrimitive(otEntry, primCursor, sprite);
            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        }
    }

    private static readonly uint* s_MenuDrawBlock = (uint*)NativeMemory.AllocZeroed(0x20);
    private static readonly int* s_MenuSpriteRef = (int*)NativeMemory.AllocZeroed(0x20);

    /// <summary>
    /// Ghidra: the entity-slot initialisation loop at the top of FUN_00542250 — four 0x4c-byte
    /// slots, each stamped with its index and a leading 8.
    /// </summary>
    private static void InitialiseEntitySlots()
    {
        byte* slot = s_EntitySlotTable_005e9180;
        for (int i = 0; i < 4; i++, slot += 0x4c)
        {
            slot[0] = 8;
            slot[1] = (byte)i;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0051f9c0 @ 0x0051f9c0 — links a two-word flat primitive into the ordering table.
    /// The tag word carries the primitive length in its top byte (0x02000000), which is the
    /// PlayStation display-list convention.
    /// </summary>
    private static uint* DrawFlatPrimitive(uint* otEntry, uint* prim, int* sprite)
    {
        uint attributes = *(uint*)(*(int*)sprite + 4 + (short)sprite[4] * 8);

        if (s_Unknown_00564a48 != 0 && (attributes & 0x200) != 0)
        {
            if (s_SmallTextureFlag == 0) return prim;
            attributes &= 0xfffffdff;
        }

        prim[1] = (uint)*(ushort*)(s_EntityDecodeTable_005e8f00 + ((attributes >> 0x10) & 0xff) * 4)
                  | ((attributes & 0xff) << 5) | 0xe1000000;
        prim[2] = 0;
        *prim = *otEntry | 0x2000000;
        *otEntry = (uint)(nuint)prim;
        return prim + 8;
    }

    /// <summary>
    /// Ghidra: FUN_00521f20 @ 0x00521f20 — links a ten-word textured sprite primitive into the
    /// ordering table (tag length 0x09000000). The bit packing is transcribed operation for
    /// operation; the shifts mix a colour, a texture page and four corner coordinates.
    /// </summary>
    private static uint* DrawSpritePrimitive(uint* otEntry, uint* prim, int* sprite, uint flags, uint extra)
    {
        uint* source = (uint*)(*(int*)sprite + (short)sprite[4] * 8);
        uint posX = (uint)sprite[2];
        short halfW = (short)sprite[3];
        short halfH = *(short*)((byte*)sprite + 0xe);
        if (halfW < 0) halfW = (short)-halfW;
        if (halfH < 0) halfH = (short)-halfH;

        uint baseWord = (uint)sprite[1];
        uint colour = source[0];
        uint size = (uint)(halfH * 0x10000 + halfW);
        uint attributes = source[1];

        if (s_Unknown_00564a48 != 0)
        {
            if ((attributes & 0x200) != 0)
            {
                if (s_SmallTextureFlag == 0) return prim;
                attributes &= 0xfffffdff;
            }
            if ((flags & 2) != 0)
            {
                if (s_SmallTextureFlag == 0) return prim;
                flags = (byte)flags & 0xfdu;
            }
        }

        uint alpha = (colour >> 0x10) & 0xff;
        if (alpha == 0) alpha = 0xff;

        uint scaledW = alpha * (uint)(short)size;
        uint scaledH = (size >> 0x10) * (colour >> 0x18);
        uint clut = *(uint*)(s_EntityDecodeTable_005e8f00 + ((attributes >> 0x10) & 0xff) * 4);

        *prim = *otEntry | 0x9000000;
        *otEntry = (uint)(nuint)prim;

        prim[1] = ((attributes & 0xffffff00) | (((flags & 0xff) | 0x2c) << 8)) << 0x10 | (baseWord & 0x3ffffff);

        uint alphaSum = alpha + (colour & 0xff);
        uint packed = (colour >> 0x18) * 0x100 + (colour & 0xff00);
        uint left = (posX - (scaledW >> 0xd)) & 0xffff;
        uint top = (posX & 0xffff0000) + (scaledH >> 0xd) * 0xffff0000;
        uint right = (left + (scaledW >> 0xc)) & 0xffff;
        uint bottom = (scaledH >> 0xc) * 0x10000 + top;

        prim[2] = top | left;
        prim[3] = (uint)(((((ushort)((short)extra >> 2)) & 0xffc0) + (extra & 0xff)) * 0x10000)
                  + (clut & 0xffff0000) + ((attributes >> 8) & 0xff0000) | (colour & 0xffff);
        prim[4] = top | right;
        prim[5] = (((attributes & 0xff) << 5) | clut) << 0x10 | alphaSum | (colour & 0xff00);
        prim[6] = left | bottom;
        prim[8] = bottom | right;
        prim[7] = packed | (colour & 0xff);
        prim[9] = packed | alphaSum;

        return prim + 10;
    }

    // =====================================================================
    // The UI text engine (Ghidra: FUN_005443a0 and its subtree)
    //
    // Shared by the whole game, not just the menu: FUN_005443a0 has 49 call sites. It runs one
    // byte stream **twice** over a descriptor, dispatching every byte below 0x20 through a
    // different 32-entry table per pass:
    //
    //   pass 1  scan    PTR_LAB_0056e870   measures and skips; collects '$'-escapes, draws nothing
    //   pass 2  render  PTR_FUN_0056e728   emits glyphs
    //
    // Both tables are recovered verbatim in Documentation/DataSymbols.md, along with the five
    // handler bodies Ghidra never emitted (they are reached only through these tables) which were
    // decoded by hand from Legacy/Blaze.exe.
    //
    // What it produces is **not** ordinary primitives. Glyphs are rasterised into a staging buffer
    // (DAT_005e0300, 4 bits per pixel), and each finished line is enqueued as a VRAM upload node
    // onto the same list FlushDeferredImageUploads drains. The cursor threaded in and out of
    // FUN_005443a0 is that node cursor.
    //
    // Descriptor layout, from the offsets these functions touch:
    //   +0x00  ushort  tpage/clut slot, indexes DAT_005e8f00
    //   +0x0a  short   line height budget
    //   +0x0c  uint    packed line state; byte +0x0d is the line index
    //   +0x0e  sbyte   pen X accumulator
    //   +0x10  short   glyph count; byte +0x11 is the line index again
    //   +0x12  byte    line width, in units of 8 pixels
    //   +0x14  byte    staging bank
    //   +0x15  sbyte   nested-text stack depth
    //   +0x68  16-entry ordering table (ClearOTagR'd by control code 0x0c)
    //   +0xa4  ordering-table cursor base
    //   +0x828 sprite primitive base
    //   +0x8ac text cursor      +0x8a8 completion callback     +0x8b0 callback user data
    //   +0x8b4 nested-text return stack
    //   +0x8c6 byte    frame countdown       +0x8c7 byte  flags
    // =====================================================================

    // Ghidra: &DAT_005e0300 — the 4bpp glyph staging arena, addressed as (bank + line) * 0x800.
    // The next named global is DAT_005e8300, which puts its size at 0x8000 — sixteen banks.
    private static readonly byte* s_GlyphStaging_005e0300 = (byte*)NativeMemory.AllocZeroed(0x8000);

    /// <summary>
    /// The 4bpp glyph staging bank for one line, bounds-checked. The buffer holds 16 banks of
    /// 0x800 bytes. Its extent in the original is inferred from where the next named global sits,
    /// so an index past it is a defect in this reconstruction rather than original behaviour — and
    /// has to say so rather than quietly scribble over another allocation.
    /// </summary>
    private static byte* StagingBank(uint bank, uint line)
    {
        uint index = bank + line;
        if (index >= 0x10)
        {
            throw new InvalidOperationException(
                $"UI text staging bank {index} (bank {bank} + line {line}) is past the 16-bank " +
                "buffer at DAT_005e0300. The line counter is a byte, so a descriptor laying out " +
                "more than 16 lines would corrupt unrelated memory here.");
        }
        return s_GlyphStaging_005e0300 + index * 0x800;
    }

    /// <summary>
    /// One tpage/clut slot of the table at DAT_005e8f00, bounds-checked. A descriptor carries its
    /// slot as a full ushort, so a wrong or unpopulated descriptor can index far past the table.
    /// </summary>
    private static uint* DecodeSlot(uint index)
    {
        if (index >= 0x400)
        {
            throw new InvalidOperationException(
                $"tpage/clut slot {index} is past the 1024-entry table at DAT_005e8f00 — the " +
                "descriptor asking for it is not the one this table was filled for.");
        }
        return (uint*)(s_EntityDecodeTable_005e8f00 + index * 4);
    }

    private static byte* s_StagingWrite_005e8304;   // Ghidra: DAT_005e8304 — staging write pointer
    private static int s_TextState_005e8308;        // Ghidra: DAT_005e8308 — 0 / 1 / 2 / -1, see below
    private static byte* s_TrimAnchor_005e830c;     // Ghidra: DAT_005e830c — leading-space trim anchor
    private static uint* s_OtCursor_005e8314;       // Ghidra: DAT_005e8314 — OT cursor in the descriptor
    private static int s_LineIndex_005e8318;        // Ghidra: DAT_005e8318 — lines emitted this page
    private static uint* s_UploadCursor_005e831c;   // Ghidra: DAT_005e831c — the node cursor, in and out
    private static uint s_PenX_005e8320;            // Ghidra: DAT_005e8320 — pen X, in 4bpp columns
    private static uint* s_SpriteCursor_005e8324;   // Ghidra: DAT_005e8324 — sprite cursor in the descriptor
    private static uint s_BreakPen_005e8328;        // Ghidra: DAT_005e8328 — pen saved at the last break
    private static uint s_BreakColumn_005e8300;     // Ghidra: _DAT_005e8300 — column saved at the last break
    private static byte* s_BreakStaging_005e02fc;   // Ghidra: DAT_005e02fc — staging ptr at the last break
    private static int s_GlyphMode_005dffd0;        // Ghidra: DAT_005dffd0 — 0 none / 1 latin / 2 DBCS
    private static int s_Unknown_005dffe8;          // Ghidra: DAT_005dffe8

    /// <summary>
    /// Ghidra: &amp;DAT_0056e7b0 — proportional glyph widths, one byte per folded slot.
    /// <para>
    /// **Taken from `FUN_00507890`, not from the `.data` image.** The two differ, and the `.data`
    /// copy is the stale one: `FUN_00507890` builds the real table as 186 stack literals and copies
    /// it over (`FUN_00507870` is its fixed-width counterpart, every entry 8). The `.data` copy
    /// gives `I`=6/`J`=2 and `M`=5/`N`=8 — impossible for a proportional font, and the direct cause
    /// of the uneven spacing: narrow glyphs advanced too far, wide ones not far enough. This table
    /// gives `I`=2, `M`=8, `i`=2, `l`=2, `m`=8, `t`=4, `Q`=8.
    /// </para>
    /// The folding that indexes it is confirmed against the machine code at 0x00543EE3:
    /// `CMP SI,0x41 / LEA EAX,[ESI-0x22] / JAE / LEA EAX,[ESI-0x21]`, then
    /// `MOVSX AX,[ESI + 0x0056E7B0]` — so both the folding and the base are faithful as written.
    /// <para>
    /// TODO(REVERSE): neither setter has a call site in `Blaze.c` — both are reached only through
    /// an address-taken table, so which one runs when is not established. The proportional one is
    /// installed here because the original demonstrably renders proportional text.
    /// </para>
    /// </summary>
    private static readonly byte[] s_GlyphWidths_0056e7b0 =
    [
        0x02,0x04,0x08,0x06,0x0a,0x08,0x02,0x04,0x04,0x06,0x06,0x02,0x04,0x02,0x04,0x06,
        0x04,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x02,0x02,0x04,0x06,0x04,0x06,0x06,
        0x06,0x06,0x06,0x05,0x05,0x07,0x06,0x02,0x06,0x07,0x05,0x08,0x07,0x07,0x06,0x08,
        0x06,0x06,0x06,0x06,0x07,0x08,0x06,0x06,0x07,0x03,0x04,0x03,0x06,0x08,0x05,0x05,
        0x05,0x05,0x05,0x04,0x05,0x05,0x02,0x03,0x06,0x02,0x08,0x05,0x06,0x05,0x05,0x04,
        0x06,0x04,0x05,0x06,0x06,0x06,0x06,0x06,0x04,0x02,0x04,0x07,0x07,0x02,0x06,0x06,
        0x06,0x06,0x02,0x07,0x04,0x0a,0x06,0x06,0x0a,0x04,0x0a,0x08,0x04,0x06,0x05,0x05,
        0x03,0x06,0x07,0x02,0x04,0x03,0x06,0x06,0x08,0x0a,0x0a,0x06,0x06,0x06,0x06,0x06,
        0x06,0x0a,0x07,0x05,0x05,0x06,0x06,0x03,0x03,0x04,0x04,0x08,0x07,0x08,0x08,0x08,
        0x08,0x08,0x06,0x07,0x06,0x06,0x06,0x06,0x06,0x07,0x06,0x06,0x06,0x06,0x05,0x06,
        0x08,0x05,0x05,0x05,0x06,0x06,0x03,0x03,0x04,0x04,0x07,0x05,0x06,0x06,0x06,0x06,
        0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,
    ];

    // Ghidra: memmove — FUN_00547a00 is 420 lines of unrolled MSVC block copy. Per the project's
    // rule, a recognised CRT routine is called rather than transcribed.
    [DllImport("msvcrt.dll", EntryPoint = "memmove", CallingConvention = CallingConvention.Cdecl)]
    private static extern void* CrtMemMove(void* dest, void* src, nuint count);

    /// <summary>
    /// Ghidra: FUN_0051c0e0 @ 0x0051c0e0 — advances a cheat-code matcher over every character the
    /// text engine draws, against the string at <c>DAT_00564a40</c>.
    /// </summary>
    private static void CheatCodeProbe(byte ch)
    {
        // TODO(REVERSE): DAT_00564a40 is the expected sequence and DAT_005633a8 the latch it
        // clears. Both live in initialised .data; recovering them only changes whether a cheat
        // can be entered, never whether text draws, so the matcher is kept structurally faithful
        // with an empty sequence — which simply never matches.
        if (s_CheatSequence_00564a40.Length == 0 ||
            s_CheatSequence_00564a40[s_CheatIndex_005de164] == 0)
        {
            s_Unknown_005633a8 = 0;
            s_CheatIndex_005de164 = 0;
            return;
        }

        if (ch == s_CheatSequence_00564a40[s_CheatIndex_005de164])
        {
            s_CheatIndex_005de164++;
            return;
        }

        s_CheatIndex_005de164 = 0;
    }

    private static readonly byte[] s_CheatSequence_00564a40 = [0];  // TODO(REVERSE): .data content
    private static int s_CheatIndex_005de164;   // Ghidra: DAT_005de164
    private static int s_Unknown_005633a8;      // Ghidra: DAT_005633a8

    /// <summary>
    /// Ghidra: FUN_00543980 @ 0x00543980 — enqueues the finished line's staging bank as a VRAM
    /// upload node, onto the same list <see cref="FlushDeferredImageUploads"/> drains.
    /// </summary>
    private static void EnqueueGlyphUpload(byte* descriptor)
    {
        int clut = (int)*DecodeSlot(*(ushort*)descriptor);
        uint packed = *(uint*)(descriptor + 0x0c);
        ushort bank = *(ushort*)(descriptor + 0x14);

        uint* node = s_UploadCursor_005e831c;
        node[1] = (uint)(nuint)StagingBank((byte)bank, (packed >> 8) & 0xff);
        node[3] = 0x100040;
        node[2] = (uint)((byte)bank * 0x100000 +
                         ((((packed >> 4) & 0xf0) + (uint)((((clut + 1) & 0x10) + 2) * 0x10)) * 0x10000
                          | (uint)((clut + 1) & 0xf) << 6));

        node[0] = (uint)(nuint)s_Unknown_005e8310;
        s_Unknown_005e8310 = (int)(nuint)node;
        s_UploadCursor_005e831c = node + 4;
    }

    /// <summary>
    /// Ghidra: FUN_00543a10 @ 0x00543a10 — links the finished line's textured sprite into the
    /// descriptor's own 16-entry ordering table.
    /// </summary>
    private static void EmitTextSprite(byte* descriptor)
    {
        ushort bank = *(ushort*)(descriptor + 0x14);
        uint packed = *(uint*)(descriptor + 0x0c);
        int clut = (int)*DecodeSlot(*(ushort*)descriptor);
        uint rect = *(uint*)(descriptor + 0x20);

        uint* prim = s_SpriteCursor_005e8324;
        prim[2] = *(uint*)(descriptor + 0x24) | 0x64000000;
        prim[1] = (rect >> 0x10) & 0x60 | (uint)(clut + 1) & 0xffff | 0xe1000000;
        prim[3] = (((rect >> 8) & 0xff) + (uint)(ushort)((sbyte)((packed >> 8) & 0xf) * 0x0c)) * 0x10000
                  | ((rect & 0xff) + (packed & 0xff));
        prim[4] = rect & 0xf0000 | (uint)(clut + 1) & 0xffff0000
                  | ((((uint)(byte)bank * 0x100 + packed) & 0xf00) * 0x10 + 0x2000 + (packed & 0xff)) & 0xffff;
        prim[5] = (packed >> 0x10) & 0xff | (packed >> 8) & 0xff0000;

        prim[0] = *s_OtCursor_005e8314 | 0x5000000;
        *s_OtCursor_005e8314 = (uint)(nuint)prim;
        s_SpriteCursor_005e8324 = prim + 6;
    }

    /// <summary>
    /// Ghidra: FUN_00544cb0 @ 0x00544cb0 — closes the current line: trims the leading run the
    /// engine had provisionally drawn, then grows the descriptor's height budget by one line.
    /// </summary>
    private static void FlushLine(byte* descriptor)
    {
        if (s_TextState_005e8308 == 2)
        {
            uint trim = (((uint)descriptor[0x12] * 8 - s_PenX_005e8320) - 10) >> 1;
            if ((ushort)trim < 0x100)
            {
                uint span = (uint)((int)((trim & 0xffff) + 1) >> 1);
                for (int row = 0xf; row != 0; row--)
                {
                    CrtMemMove(s_TrimAnchor_005e830c + span, s_TrimAnchor_005e830c,
                               (nuint)((s_PenX_005e8320 + 1) >> 1));

                    byte* fill = s_TrimAnchor_005e830c;
                    for (uint w = span >> 2; w != 0; w--) { *(uint*)fill = 0xffffffff; fill += 4; }
                    for (uint b = span & 3; b != 0; b--) { *fill = 0xff; fill++; }

                    s_TrimAnchor_005e830c += 0x80;   // Ghidra: DAT_005e830c + 0x20 on a uint*
                }

                s_PenX_005e8320 += trim & 0xffff;
                descriptor[0x0e] = (byte)(descriptor[0x0e] + (byte)trim);
            }
        }

        s_LineIndex_005e8318++;
        int height = s_LineIndex_005e8318 * 0x0c + 6;
        if (*(short*)(descriptor + 0x0a) < height && (descriptor[0x8c7] & 2) != 0)
        {
            *(short*)(descriptor + 0x0a) = (short)height;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00544c50 @ 0x00544c50 — pass-2 handler for control code <c>0x0c</c>: begins a
    /// page. Clears the descriptor's own 16-entry ordering table and re-bases every cursor.
    /// </summary>
    private static byte* Handler_00544c50(byte* text, byte* descriptor)
    {
        Ordinal_688((uint*)(descriptor + 0x68), 0x10);   // ClearOTagR on the descriptor's own OT
        *(ushort*)(descriptor + 0x10) = 0;
        *(uint*)(descriptor + 0x0c) = 0xf000000;
        s_OtCursor_005e8314 = (uint*)(descriptor + 0xa4);
        s_SpriteCursor_005e8324 = (uint*)(descriptor + 0x828);
        s_PenX_005e8320 = 0;                            // Ghidra: DAT_005e8320 = 0
        s_GlyphMode_005dffd0 = 0;
        s_StagingWrite_005e8304 = StagingBank(descriptor[0x14], 0);
        return text;   // Ghidra returns unaff_ESI — the caller's cursor, unchanged
    }

    /// <summary>
    /// Ghidra: FUN_00544d90 @ 0x00544d90 — pass-2 handler for control code <c>0x0d</c>: ends the
    /// line, uploads it, emits its sprite, and re-points every cursor at the next line's slots.
    /// </summary>
    private static byte* Handler_00544d90(byte* text, byte* descriptor) { NewLine(0, descriptor); return text; }

    private static void NewLine(int reset, byte* descriptor)
    {
        FlushLine(descriptor);
        if (s_PenX_005e8320 != 0) EnqueueGlyphUpload(descriptor);
        if ((sbyte)descriptor[0x0e] != 0) EmitTextSprite(descriptor);

        uint packed = (*(uint*)(descriptor + 0x0c) + 0x100) & 0xff00ff00;
        *(uint*)(descriptor + 0x0c) = packed;
        *(short*)(descriptor + 0x10) = (short)packed;

        uint line = (packed >> 8) & 0xff;
        s_OtCursor_005e8314 = (uint*)(descriptor + (0x29 - line) * 4);
        s_SpriteCursor_005e8324 = (uint*)(descriptor + (0x828 - line * 0x80));
        s_StagingWrite_005e8304 = StagingBank(descriptor[0x14], line);
        s_PenX_005e8320 = 0;                            // Ghidra: DAT_005e8320 = 0
        s_BreakStaging_005e02fc = null;

        if (reset != 0) s_TextState_005e8308 = 0;
        if (s_TextState_005e8308 == 2) s_TextState_005e8308 = 1;
        s_GlyphMode_005dffd0 = 0;
    }

    /// <summary>
    /// Pass-2 handler for control code <c>0x09</c> — end of page / wait. **Decoded by hand from
    /// Legacy/Blaze.exe**: Ghidra never emitted this body, since it is reached only through
    /// PTR_FUN_0056e728. Bytes and full listing in Documentation/DataSymbols.md.
    /// <code>
    /// 8B 4C 24 08  MOV ECX,[ESP+8]     8B 44 24 04  MOV EAX,[ESP+4]
    /// 8A 81 ..     MOV AL,[ECX+0x8C7]  8A 10        MOV DL,[EAX]
    /// C6 41 3B 01  MOV [ECX+0x3B],1    40           INC EAX
    /// 0C 04        OR  AL,4            88 91 ..     MOV [ECX+0x8C5],DL
    /// C6 81 ..     MOV [ECX+0x8C4],0   89 81 ..     MOV [ECX+0x8AC],EAX
    /// 88 81 ..     MOV [ECX+0x8C7],AL  C3           RET
    /// </code>
    /// Raising flag bit 2 is what makes every later <see cref="RunTextEngine"/> call return at
    /// once, so a finished page costs nothing on subsequent frames.
    /// </summary>
    private static byte* Handler_00544b10(byte* text, byte* descriptor)
    {
        byte flags = descriptor[0x8c7];
        descriptor[0x3b] = 1;
        flags |= 4;
        descriptor[0x8c4] = 0;
        descriptor[0x8c7] = flags;

        descriptor[0x8c5] = *text;          // the operand byte — '?' for the main menu
        text++;
        *(byte**)(descriptor + 0x8ac) = text;
        return text;
    }

    // The four pass-1 stubs, decoded from the binary. They are not semantic handlers: they declare
    // how many operand bytes a control code consumes so the scan can skip without drawing.
    private static byte* Advance0(byte* p, byte* descriptor) => p;        // 0x00543940  8B 44 24 04 C3
    private static byte* Advance1(byte* p, byte* descriptor) => p + 1;    // 0x00543950  ... 40 C3
    private static byte* Advance2(byte* p, byte* descriptor) => p + 2;    // 0x00543960  ... 83 C0 02 C3
    private static byte* Advance3(byte* p, byte* descriptor) => p + 3;    // 0x00543970  ... 83 C0 03 C3

    private delegate byte* TextControlHandler(byte* text, byte* descriptor);

    /// <summary>
    /// Builds one of the two dispatch tables. Both are modelled as tables rather than a switch
    /// because that is what the original is, and because the two differ only by substitution —
    /// a switch would hide that <c>0x00543950</c> alone covers six codes.
    /// </summary>
    private static TextControlHandler[] BuildTextTable(bool renderPass)
    {
        var t = new TextControlHandler[0x20];

        // Slots with no reconstructed body. Each names its address and code rather than no-opping,
        // so an unreached corner of the text format announces itself the first time it is used.
        for (int i = 0; i < 0x20; i++)
        {
            int code = i;
            uint address = (renderPass ? s_RenderTable_0056e728 : s_ScanTable_0056e870)[i];
            t[i] = (text, descriptor) => throw new NotImplementedException(
                $"UI text control code 0x{code:x2} dispatches to FUN_{address:x8}, which Ghidra " +
                $"never emitted (reached only through {(renderPass ? "PTR_FUN_0056e728" : "PTR_LAB_0056e870")}). " +
                "It needs a scoped Ghidra re-run at that address — see Documentation/DataSymbols.md.");
        }

        if (!renderPass)
        {
            // Pass 1: every slot whose recovered address is one of the four stubs.
            for (int i = 0; i < 0x20; i++)
            {
                t[i] = s_ScanTable_0056e870[i] switch
                {
                    0x00543940 => Advance0,
                    0x00543950 => Advance1,
                    0x00543960 => Advance2,
                    0x00543970 => Advance3,
                    _ => t[i],
                };
            }
        }
        else
        {
            t[0x09] = Handler_00544b10;   // 0x00544b10, decoded here
            t[0x0c] = Handler_00544c50;   // 0x00544c50, decompiled
            t[0x0d] = Handler_00544d90;   // 0x00544d90, decompiled
        }

        return t;
    }

    // Ghidra: PTR_LAB_0056e870 and PTR_FUN_0056e728, both recovered verbatim from Legacy/Blaze.exe.
    private static readonly uint[] s_ScanTable_0056e870 =
    [
        0x543950, 0x543970, 0x543960, 0x543960, 0x543960, 0x543950, 0x543960, 0x544A80,
        0x543950, 0x543950, 0x543960, 0x544C10, 0x543940, 0x543940, 0x543950, 0x544E50,
        0x544F70, 0x544F80, 0x544F90, 0x544FA0, 0x543950, 0x545010, 0x545020, 0x545030,
        0x545040, 0x545070, 0x5450A0, 0x5450D0, 0x544E80, 0x544F10, 0x545100, 0x545120,
    ];

    private static readonly uint[] s_RenderTable_0056e728 =
    [
        0x544FB0, 0x544810, 0x544970, 0x544990, 0x5449B0, 0x5449D0, 0x5449F0, 0x544A80,
        0x544AB0, 0x544B10, 0x544BC0, 0x544C10, 0x544C50, 0x544D90, 0x544B50, 0x544E50,
        0x544F70, 0x544F80, 0x544F90, 0x544FA0, 0x544FB0, 0x545010, 0x545020, 0x545030,
        0x545040, 0x545070, 0x5450A0, 0x5450D0, 0x544E80, 0x544F10, 0x545100, 0x545120,
    ];

    private static readonly TextControlHandler[] s_ScanHandlers = BuildTextTable(false);
    private static readonly TextControlHandler[] s_RenderHandlers = BuildTextTable(true);

    // winsys3d ordinal 57 (winsys3d.c FUN_00a05060) — maps a Shift-JIS code to a 16x16 1bpp glyph
    // inside the KANJI resource the DLL locks at startup, or to a blank when it is out of range.
    [DllImport("winsys3d.dll", EntryPoint = "#57")] private static extern ushort* Ordinal_57(uint sjis);

    // Ghidra: DAT_0056e9b4 / _b8 / _bc — recovered from Legacy/Blaze.exe. Full-width Shift-JIS
    // punctuation that this renderer squeezes to half width.
    private static readonly uint[] s_HalfWidthCodes_0056e9b4 = [0x8381, 0x7981, 0x6981];

    // Ghidra: &DAT_005dffd8 — the 15-byte row scratch the spreading pass fills.
    private static readonly byte* s_DbcsRowScratch_005dffd8 = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>
    /// Ghidra: FUN_00544150 @ 0x00544150 — the DBCS/kanji glyph renderer. Fetches a 16x16 1bpp
    /// glyph, spreads each row's bits and unpacks it into the 4bpp staging buffer.
    /// <para>
    /// **Signature confirmed against `Legacy/Raw/FUN_00544150.asm`.** Ghidra renders the descriptor
    /// as <c>unaff_retaddr</c> because the function reuses arg2's stack slot as scratch
    /// (<c>[ESP+0x18]</c>). The listing settles it: at <c>00544318</c>, after four pushes,
    /// <c>MOV EAX,[ESP+0x14]</c> loads **arg1** and it is arg1 that receives <c>+0xe</c> and
    /// <c>+0x10</c> — so the call sites' <c>(descriptor, codepoint)</c> is right and the
    /// pseudocode is wrong. The fifth time this project has been saved by reading the listing.
    /// </para>
    /// </summary>
    private static void RenderDbcsGlyph(byte* descriptor, uint codepoint)
    {
        if (s_GlyphMode_005dffd0 == 1) return;

        uint code = codepoint & 0xffff;
        uint swapped = (code >> 8) | (code << 8);
        s_GlyphMode_005dffd0 = 2;

        bool halfWidth = swapped == s_HalfWidthCodes_0056e9b4[2]
                      || swapped == s_HalfWidthCodes_0056e9b4[1]
                      || swapped == s_HalfWidthCodes_0056e9b4[0];

        ushort* rows = Ordinal_57(code);

        uint spread = 0;
        byte* scratch = s_DbcsRowScratch_005dffd8;
        for (int row = 0xf; row != 0; row--)
        {
            // CONCAT11(lo, hi) — the row is byte-swapped out of the resource.
            ushort r = (ushort)(((*rows & 0xff) << 8) | (*rows >> 8));
            if (halfWidth) r >>= 1;

            byte carry = (byte)((byte)(r >> 1) | (byte)(r & 1));
            uint v = (uint)((r >> 1) & 0xfffc) >> 1;
            spread = (v & 0xe000) >> 6
                   | ((v & 0xf800) >> 5) & 0xff
                   | ((v & 0xfe00) >> 4) & 0x7f
                   | ((v & 0xff80) >> 3) & 0x3f
                   | ((v & 0xffe0) >> 2) & 0x1f
                   | ((v & 0xfff8) >> 1) & 0xf
                   | (v & 7)
                   | (uint)(carry & 3);

            byte packed = (byte)spread;
            if (row == 4 || row == 8 || row == 0x0c)
            {
                scratch--;
                packed |= *scratch;
            }
            *scratch = packed;

            scratch++;
            rows++;
        }

        byte* dest = s_StagingWrite_005e8304;
        scratch = s_DbcsRowScratch_005dffd8;
        for (uint shade = 0xf; shade != 0; shade--)
        {
            s_StagingWrite_005e8304 = dest;

            uint bits = *scratch;
            for (int i = 8; i != 0; i--)
            {
                spread <<= 4;
                if ((bits & 1) != 0) spread |= shade;
                bits >>= 1;
            }

            *(uint*)s_StagingWrite_005e8304 = ~spread;
            scratch++;
            dest = s_StagingWrite_005e8304 + 0x80;   // Ghidra: DAT_005e8304 + 0x20 on a uint*
        }

        // Ghidra: DAT_005e8304 + -0x1bf on a uint* = -0x6fc bytes from start+0x700, i.e. +4.
        // The listing agrees by a different route: start+0x780 then SUB EBP,0x77c.
        s_StagingWrite_005e8304 -= 0x6fc;

        descriptor[0x0e] = (byte)(descriptor[0x0e] + 8);
        s_PenX_005e8320 += 8;
        descriptor[0x10]++;
    }

    /// <summary>
    /// Ghidra: FUN_00543b20 @ 0x00543b20 — rasterises one character into the 4bpp staging buffer
    /// and advances the pen, implementing word wrap by backtracking to the last break.
    /// </summary>


    private static void RasterizeGlyph(byte* descriptor, uint character, int oddAlign)
    {
        CheatCodeProbe((byte)character);

        ushort ch = (ushort)character;
        if (s_TextState_005e8308 == 0 && ch == 0x20)
        {
            s_TrimAnchor_005e830c = s_StagingWrite_005e8304;
            s_TextState_005e8308 = 1;
        }

        if (s_TextState_005e8308 == 1 && ch == 0x20) return;   // leading space, provisional

        short width;
        int advance;

        if (ch == 0x5e)   // '^' — a solid rule to the next 8-column boundary
        {
            advance = 8 - (int)(s_PenX_005e8320 & 7);
            width = (short)advance;
            FillStagingRun((short)advance);
            descriptor[0x0e] = (byte)(descriptor[0x0e] + (byte)advance);
            s_PenX_005e8320 += (uint)advance;
            descriptor[0x10]++;
            return;
        }

        s_GlyphMode_005dffd0 = 1;

        if ((ch < 0x21 && ch != 0x14) || 0xff < ch)
        {
            // A space or an unprintable: draw a blank and remember it as the wrap point.
            if ((uint)descriptor[0x12] * 8 - 10 < s_PenX_005e8320 && s_BreakStaging_005e02fc != null)
            {
                NewLine(0, descriptor);
                return;
            }

            width = 4;
            if (oddAlign != 0 && (descriptor[0x0e] & 1) != 0) width = 5;
            advance = width;

            uint penBefore = s_PenX_005e8320;
            FillStagingRun(width);

            s_BreakColumn_005e8300 = penBefore + (uint)advance;

            // Ghidra applies `+ ((pen & 1) + width) / 2 - 0x700` **once**, after the 15-row loop —
            // and FillStagingRun already does exactly that as its last statement. Repeating it here
            // advanced the staging pointer twice per space, which drifted every glyph after the
            // first space of a line into the wrong columns and eventually the wrong bank.
            s_BreakStaging_005e02fc = s_StagingWrite_005e8304;
            s_BreakPen_005e8328 = (uint)descriptor[0x0e] + (uint)advance;
        }
        else
        {
            if ((uint)descriptor[0x12] * 8 - 10 < s_PenX_005e8320 && s_BreakStaging_005e02fc != null)
            {
                ReflowPartialWord(descriptor);
            }

            // Ghidra: DAT_005e8308 = ((0 < DAT_005e8308) - 1 & 0xfffffffd) + 2 — a SETcc idiom.
            // Positive state becomes 2 (a leading space was trimmed); anything else becomes -1.
            s_TextState_005e8308 =
                (((s_TextState_005e8308 > 0 ? 1 : 0) - 1) & unchecked((int)0xfffffffd)) + 2;

            switch (character & 0xffff)
            {
                case 0x40: case 0x80: case 0x8a: case 0x9a: case 0xc0: case 0xe0:
                    character = 0xa5;
                    break;
            }

            ch = (ushort)character;
            short slot;
            if (ch < 0xe1)
            {
                if (ch < 0xc1)
                {
                    if (ch < 0xa1)
                    {
                        slot = ch < 0x61 ? (ch < 0x41 ? (short)(ch - 0x21) : (short)(ch - 0x22))
                                         : (short)(ch - 0x23);
                    }
                    else slot = (short)(ch - 0x44);
                }
                else slot = (short)(ch - 0x45);
            }
            else slot = (short)(ch - 0x46);

            int index = slot;
            width = index >= 0 && index < s_GlyphWidths_0056e7b0.Length
                ? s_GlyphWidths_0056e7b0[index]
                : (short)0;
            if (oddAlign != 0 && (((ushort)descriptor[0x0e] + width) & 1) != 0) width++;

            int bitOffset = (index % 0x1f) * 10;
            byte* rows = GetMainMemPtr() + (bitOffset >> 3) + (index / 0x1f) * 0x230 + 0x1ad8f0;
            advance = width;
            BlitGlyphRows(rows, bitOffset, advance);
        }

        descriptor[0x0e] = (byte)(descriptor[0x0e] + (byte)width);
        s_PenX_005e8320 += (uint)advance;
        descriptor[0x10]++;
    }

    /// <summary>The 15-row blank fill shared by the <c>'^'</c> rule and the space path.</summary>
    private static void FillStagingRun(short width)
    {
        byte* row = s_StagingWrite_005e8304;
        uint pen = s_PenX_005e8320;

        for (int r = 0xf; r != 0; r--)
        {
            s_StagingWrite_005e8304 = row;
            uint column = pen & 1;
            uint bit = pen;
            for (int i = width + 1; i != 0; i--)
            {
                byte* cell = s_StagingWrite_005e8304 + ((int)column >> 1);
                if ((bit & 1) == 0) *cell = 0x0f;
                else *cell |= 0xf0;
                bit++;
                column++;
                pen = s_PenX_005e8320;
            }
            row = s_StagingWrite_005e8304 + 0x80;   // Ghidra: DAT_005e8304 + 0x20 on a uint*
        }

        s_StagingWrite_005e8304 += (int)(((pen & 1) + (uint)width) >> 1) - 0x700;
    }

    /// <summary>
    /// The 15-row glyph blit: one 12-bit slice per row out of the font bitmap, unpacked into
    /// 4bpp cells. Row 1 is forced solid, which is what gives the glyph its baseline.
    /// </summary>
    private static void BlitGlyphRows(byte* rows, int bitOffset, int advance)
    {
        byte* row = s_StagingWrite_005e8304;
        uint pen = s_PenX_005e8320;
        int shift = (bitOffset + (((bitOffset + ((bitOffset >> 31) & 7)) >> 3) * -8)) & 0x1f;

        for (int r = 0xf; r != 0; r--)
        {
            s_StagingWrite_005e8304 = row;

            uint slice = ((((uint)rows[1] + (uint)rows[0] * 0x100) * 0x100 + rows[2]) << shift) >> 0xc;
            if (r == 1) slice = 0xffffffff;

            uint column = pen & 1;
            uint bit = pen;
            for (int i = advance; i != 0; i--)
            {
                int cell = (int)column >> 1;
                if ((bit & 1) == 0)
                {
                    if ((slice & 0x800) == 0) *(sbyte*)(s_StagingWrite_005e8304 + cell) = (sbyte)r;
                    else s_StagingWrite_005e8304[cell] = 0x0f;
                }
                else if ((slice & 0x800) == 0)
                {
                    s_StagingWrite_005e8304[cell] =
                        (byte)((r << 4) | (s_StagingWrite_005e8304[cell] & 0x0f));
                }
                else s_StagingWrite_005e8304[cell] |= 0xf0;

                bit++;
                column++;
                slice <<= 1;
                pen = s_PenX_005e8320;
            }

            rows += 0x28;
            row = s_StagingWrite_005e8304 + 0x80;
        }

        s_StagingWrite_005e8304 += (int)(((pen & 1) + (uint)advance) >> 1) - 0x700;
    }

    /// <summary>
    /// The word-wrap backtrack: rewinds the pen to the last break, starts a new line, and copies
    /// the partial word's staged pixels onto it — byte-aligned or nibble-shifted by parity.
    /// </summary>
    private static void ReflowPartialWord(byte* descriptor)
    {
        uint carried = s_PenX_005e8320 - s_BreakColumn_005e8300;
        sbyte penBefore = (sbyte)descriptor[0x0e];
        sbyte penAtBreak = (sbyte)s_BreakPen_005e8328;

        s_PenX_005e8320 = s_BreakColumn_005e8300;
        descriptor[0x0e] = (byte)penAtBreak;
        byte* source = s_BreakStaging_005e02fc;

        NewLine(0, descriptor);

        int span = (int)(carried + 1) >> 1;

        if ((s_BreakColumn_005e8300 & 1) == 0)
        {
            for (int r = 0xf; r != 0; r--)
            {
                byte* src = source;
                byte* dst = s_StagingWrite_005e8304;
                for (uint w = (uint)span >> 2; w != 0; w--) { *(uint*)dst = *(uint*)src; src += 4; dst += 4; }
                source += 0x80;
                for (uint b = (uint)span & 3; b != 0; b--) { *dst = *src; src++; dst++; }
                s_StagingWrite_005e8304 += 0x80;
            }
        }
        else
        {
            for (int r = 0xf; r != 0; r--)
            {
                for (int i = 0; i < span; i++)
                {
                    s_StagingWrite_005e8304[i] =
                        (byte)((sbyte)source[i + 1] * 0x10 + (source[i] >> 4));
                }
                source += 0x80;
                s_StagingWrite_005e8304 += 0x80;
            }
        }

        s_StagingWrite_005e8304 += ((int)carried >> 1) - 0x780;
        s_PenX_005e8320 = carried;
        descriptor[0x0e] = (byte)(penBefore - penAtBreak);
        s_BreakStaging_005e02fc = null;
    }

    /// <summary>
    /// Allocates below <c>0x01000000</c>. Text descriptors are masked to 24 bits by
    /// <see cref="RunTextEngine"/> exactly as the original does — where it is the identity, since
    /// every descriptor is a <c>.data</c> global of an image based at <c>0x00400000</c>. Keeping
    /// them low here keeps it the identity, instead of turning a faithful mask into corruption.
    /// </summary>
    private static byte* AllocLowZeroed(uint size)
    {
        // The arena needs 2.6 MB contiguous and gets first pick. Static field initialisers run
        // before entry()'s body, so without this a 64 KB descriptor allocated at the very bottom
        // could split the only region large enough and starve it — measured, not hypothetical.
        EnsureDisplayListArena();

        nuint rounded = (size + 0xFFFu) & ~0xFFFu;

        // Downward from the top of the window, for the same reason: keep the low span contiguous.
        for (uint address = (DisplayListCeiling - AllocationGranularity) & ~(AllocationGranularity - 1);
             address >= ArenaScanFloor; address -= AllocationGranularity)
        {
            if (address + rounded >= DisplayListCeiling) continue;
            byte* block = (byte*)VirtualAlloc((void*)address, rounded, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            if (block != null) return block;
        }

        throw new InvalidOperationException(
            $"No free block of 0x{rounded:X} bytes below 0x{DisplayListCeiling:X8}. UI text " +
            "descriptors are masked to 24 bits by FUN_005443a0, so they cannot live higher.");
    }

    /// <summary>
    /// Ghidra: FUN_005443a0 @ 0x005443a0 — the UI text engine. Runs the descriptor's byte stream
    /// twice: pass 1 scans and collects <c>$</c>-escapes, pass 2 emits glyphs. Returns the advanced
    /// VRAM-upload node cursor.
    /// </summary>
    private static uint* RunTextEngine(uint* uploadCursor, byte* descriptorArg)
    {
        // Ghidra: puVar11 = (undefined4 *)(param_2 & 0xffffff). The identity in the original; a
        // reconstruction bug here rather than original behaviour if it ever bites.
        byte* descriptor = (byte*)((nuint)descriptorArg & 0xffffff);
        if (descriptor != descriptorArg)
        {
            throw new InvalidOperationException(
                $"FUN_005443a0 masks its descriptor to 24 bits, but this one is at " +
                $"0x{(nint)descriptorArg:X8}. Allocate it with AllocLowZeroed.");
        }

        // Ghidra: local_8d4[8]. The '&' path can index it at -1 (for the literal "&0"), so the
        // buffer is offset by one rather than silently clamping an index the original allows.
        byte* escapeBuffer = stackalloc byte[8];
        for (int i = 0; i < 8; i++) escapeBuffer[i] = 0;
        byte* escapes = escapeBuffer + 1;

        byte flags = descriptor[0x8c7];
        if ((flags & 4) != 0) return uploadCursor;
        if ((flags & 2) != 0) return uploadCursor;
        if ((flags & 0x40) != 0) return uploadCursor;
        if ((flags & 0x80) != 0) return uploadCursor;

        if ((sbyte)descriptor[0x8c6] != 0)
        {
            descriptor[0x8c6] = (byte)((sbyte)descriptor[0x8c6] - 1);
            return uploadCursor;
        }

        byte* text = *(byte**)(descriptor + 0x8ac);
        if (text == null) return uploadCursor;

        s_TextState_005e8308 = 0;
        s_BreakStaging_005e02fc = null;
        s_LineIndex_005e8318 = 1;
        if (s_Unknown_005dffe8 == 0) s_Unknown_005dffe8 = 1;

        s_UploadCursor_005e831c = uploadCursor;

        uint line = (*(uint*)(descriptor + 0x10) >> 8) & 0xff;
        *(uint*)(descriptor + 0x0c) = (*(uint*)(descriptor + 0x10) & 0xffff) | 0xf000000;
        s_OtCursor_005e8314 = (uint*)(descriptor + (0x29 - line) * 4);
        s_SpriteCursor_005e8324 = (uint*)(descriptor + (0x20a - line * 0x20) * 4);
        s_StagingWrite_005e8304 = StagingBank(descriptor[0x14], line);
        s_PenX_005e8320 = 0;

        // Ghidra: the 0x232-dword block copy into local_8c8 — pass 1 must leave no trace.
        const int SaveBytes = 0x232 * 4;
        byte* saved = stackalloc byte[SaveBytes];
        Buffer.MemoryCopy(descriptor, saved, SaveBytes, SaveBytes);

        // ---- pass 1: scan ----
        byte* escapeWrite = escapes;
        byte* cursor = text;
        bool terminated = false;

        // Neither pass has a length bound in the original: the stream is trusted to be
        // NUL-terminated .data. A cursor that walks off would read arbitrary memory here, so both
        // passes stop and say so instead. 0x4000 is far past any real page of text.
        int scanned = 0;
        const int ScanLimit = 0x4000;

        while (!terminated)
        {
            while (true)
            {
                if (++scanned > ScanLimit)
                {
                    throw new InvalidOperationException(
                        $"FUN_005443a0: scanned over 0x{ScanLimit:X} bytes without reaching a " +
                        "terminator. The descriptor text pointer at +0x8ac is not a NUL-terminated " +
                        "stream, so this walk is reading memory that is not text.");
                }

                byte b = *cursor;
                byte* next = cursor + 1;
                if (b == 0) break;

                if (b < 0x20)
                {
                    cursor = s_ScanHandlers[b](next, descriptor);
                    if (b == 9 || b == 0x0e || b == 0x0f) { terminated = true; break; }
                }
                else
                {
                    if (b > 0x7f && b < 0xa0) next = cursor + 2;   // DBCS lead byte
                    cursor = next;
                    // The original writes here unbounded into its 8-byte stack array, which is
                    // safe only because its own text never carries more escapes than that. Ours
                    // would corrupt the stack — the classic shape of a random CLR fatal error —
                    // so the write stops at the six slots the function initialises.
                    if (next[-1] == 0x24 && escapeWrite < escapes + 6)
                    {
                        *escapeWrite = *next;
                        escapeWrite++;
                    }
                }
            }
            if (terminated) break;

            if ((sbyte)descriptor[0x15] == 0)
            {
                *(uint*)(descriptor + 0x8ac) = 0;
                descriptor[0x8c7] &= 0xfe;
                break;
            }

            byte depth = (byte)((sbyte)descriptor[0x15] - 1);
            descriptor[0x15] = depth;
            cursor = *(byte**)(descriptor + 0x8b4 + depth * 4);
            *(uint*)(descriptor + 0x8b4 + depth * 4) = 0;
        }

        // ---- pass 2: render ----
        Buffer.MemoryCopy(saved, descriptor, SaveBytes, SaveBytes);
        cursor = text;
        bool skipping = false;
        scanned = 0;

        while (true)
        {
            bool reachedTail = false;

            while (true)
            {
                if (++scanned > ScanLimit)
                {
                    throw new InvalidOperationException(
                        $"FUN_005443a0: rendered over 0x{ScanLimit:X} bytes without reaching a " +
                        "terminator — same cause as the scan pass, see above.");
                }

                ushort u = *cursor;
                byte* next = cursor + 1;
                if (u == 0) break;

                if (u < 0x20)
                {
                    skipping = false;
                    cursor = s_RenderHandlers[u](next, descriptor);
                    if (u == 9 || u == 0x0e || u == 0x0f) { reachedTail = true; break; }
                }
                else if (u < 0x80 || 0x9f < u)
                {
                    if (*cursor == 0x24)          // '$' — an escape, already collected in pass 1
                    {
                        cursor += 2;
                    }
                    else
                    {
                        if (*cursor == 0x26)      // '&' — conditional on a collected escape
                        {
                            byte selector = *next;
                            int slot = 0;
                            if (selector > 0x2f && selector < 0x36)
                            {
                                slot = selector - 0x31;
                                next = cursor + 2;
                            }
                            byte want = escapes[slot];
                            if (*next != want) skipping = true;
                            if (*next == 0x78 && (want == 0x6d || want == 0x73)) skipping = false;
                            if (next[1] == 0x5b) next += 3;
                            else skipping = false;
                        }

                        cursor = next;
                        if (next[-1] == 0x5d) skipping = false;      // ']' closes the conditional
                        else if (!skipping)
                        {
                            RasterizeGlyph(descriptor, next[-1], *next < 0x20 ? 1 : 0);
                        }
                    }
                }
                else if (u < 0x83 && s_GlyphMode_005dffd0 != 2)
                {
                    // Half-width kana: look the two-byte code up in the table the resource
                    // directory points at, and fall back to the DBCS renderer on a miss.
                    uint code = u | ((uint)*next << 8);
                    short* table = *(short**)(*(byte**)(s_MainResourceBuffer_005e97c0 + 0x70));
                    int index = 0;
                    while (*table != (short)code)
                    {
                        if (0xfe < (short)index) { index = -1; break; }
                        table++;
                        index++;
                    }

                    if (index >= 0 && (short)index < 0xff && ((short)index > 0 || s_GlyphMode_005dffd0 == 1))
                    {
                        RasterizeGlyph(descriptor, (uint)index, 1);
                    }
                    else
                    {
                        RenderDbcsGlyph(descriptor, *next + code * 0x100);
                    }
                    cursor += 2;
                }
                else
                {
                    RenderDbcsGlyph(descriptor, (uint)((*cursor << 8) | *next));
                    cursor += 2;
                }
            }

            if (!reachedTail)
            {
                skipping = false;
                if ((sbyte)descriptor[0x15] != 0)
                {
                    byte depth = (byte)((sbyte)descriptor[0x15] - 1);
                    descriptor[0x15] = depth;
                    cursor = *(byte**)(descriptor + 0x8b4 + depth * 4);
                    *(uint*)(descriptor + 0x8b4 + depth * 4) = 0;
                    continue;
                }

                descriptor[0x8c7] &= 0xfe;
                *(uint*)(descriptor + 0x8ac) = 0;

                uint completion = *(uint*)(descriptor + 0x8a8);
                if (completion != 0)
                {
                    // TODO(REVERSE): the original calls this through the pointer at +0x8a8 with
                    // the user data at +0x8b0. No call site assigns it yet, so reaching it means a
                    // caller now does and the signature has to be established.
                    throw new NotImplementedException(
                        $"FUN_005443a0: text completion callback at 0x{completion:X8} — no " +
                        "assignment site is reconstructed yet, so its signature is unknown");
                }
            }

            FlushLine(descriptor);
            if (s_PenX_005e8320 != 0) EnqueueGlyphUpload(descriptor);
            if ((sbyte)descriptor[0x0e] != 0) EmitTextSprite(descriptor);
            s_TextState_005e8308 = 0;
            return s_UploadCursor_005e831c;
        }
    }

    // =====================================================================
    // The UI window renderer (Ghidra: FUN_00545340 and its four primitive emitters)
    //
    // Draws one UI panel: its scroll arrows, its frame corners and edges, the text display list
    // FUN_005443a0 already built inside the descriptor, and its cursor. The panel's own ordering
    // table is copied into the frame's, offset by the panel's screen position — which is why the
    // text engine builds a self-contained list rather than writing into the frame directly.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0051bdb0 @ 0x0051bdb0 — links a screen-space clip rectangle, chosen by the
    /// small-texture flag and the detail level.
    /// </summary>
    private static void EmitClipRect(uint* otEntry, uint** prim)
    {
        if (s_Unknown_005de15c == 0) return;

        uint* p = *prim;
        p[1] = (s_SmallTextureFlag == 0 && 7 < s_SizeSetting_005de160)
            ? 0xfb00000b
            : 0xfb000001;
        p[2] = (uint)s_Unknown_005de154;
        p[3] = (uint)s_Unknown_005de158;
        p[0] = *otEntry | 0x3000000;
        *otEntry = (uint)(nuint)p;
        *prim = p + 4;                       // Ghidra: *param_2 + 0x10 bytes
    }

    /// <summary>
    /// Ghidra: FUN_0051be20 @ 0x0051be20 — links the panel's viewport rectangle, derived from the
    /// detail level the options page and the F11/F12 hotkeys set.
    /// </summary>
    private static void EmitViewportRect(uint* otEntry, uint** prim)
    {
        if (s_Unknown_005de15c == 0) return;

        uint* p = *prim;
        p[1] = (uint)(s_SizeSetting_005de160 - 0x5000000);
        p[2] = (uint)(s_SizeSetting_005de160 * -0x10 + 0xb0);
        p[3] = (uint)(s_SizeSetting_005de160 * -0x0c + 0x84);
        p[0] = *otEntry | 0x3000000;
        *otEntry = (uint)(nuint)p;
        *prim = p + 4;
    }

    /// <summary>
    /// Ghidra: FUN_00545160 @ 0x00545160 — a six-word textured sprite (tag 0x05000000).
    /// </summary>
    private static uint* EmitSprite(uint* otEntry, uint* prim, uint tpage, uint position,
                                    uint offset, uint clut, uint size)
    {
        prim[1] = (offset >> 0x10) & 0x60 | tpage & 0xffff | 0xe1000000;
        prim[2] = clut | 0x64000000;
        prim[3] = ((position >> 0x10) + ((offset >> 8) & 0xff)) * 0x10000
                  | (position + (offset & 0xff)) & 0xffff;
        prim[4] = offset & 0xf0000 | tpage & 0xffff0000 | size & 0xffff;
        prim[5] = (size >> 8) & 0xff0000 | (size >> 0x10) & 0xff;
        prim[0] = *otEntry | 0x5000000;
        *otEntry = (uint)(nuint)prim;
        return prim + 6;
    }

    /// <summary>
    /// Ghidra: FUN_00545220 @ 0x00545220 — an eight-word tinted sprite (tag 0x07000000), the
    /// frame edges. Its colour word is built by packing three 5-bit channels.
    /// </summary>
    private static uint* EmitTintedSprite(uint* otEntry, uint* prim, uint tpage, uint position,
                                          uint offset, uint clut, uint tint, uint size)
    {
        prim[1] = (offset >> 0x10) & 0x60 | tpage & 0xffff | 0xe1000000;
        prim[2] = ((((((tint >> 8) & 0xff) >> 3 | 0xffffc400) << 5 | (tint & 0xff) >> 3) << 5
                    | (uint)((byte)(-(sbyte)(tint >> 0x18)) >> 3)) << 5)
                  | (uint)((byte)(-(sbyte)(tint >> 0x10)) >> 3);
        if (s_Unknown_00564a48 != 0) clut &= 0xfdffffff;
        prim[3] = clut | 0x64000000;
        prim[4] = ((position >> 0x10) + ((offset >> 8) & 0xff)) * 0x10000
                  | (position + (offset & 0xff)) & 0xffff;
        prim[6] = size;
        prim[7] = 0xe2000000;
        prim[5] = offset & 0xf0000 | tpage & 0xffff0000 | tint & 0xffff;
        prim[0] = *otEntry | 0x7000000;
        *otEntry = (uint)(nuint)prim;
        return prim + 8;
    }

    // Ghidra: &DAT_005e9060 — the per-panel selection index the scroll handling writes back.
    private static readonly byte* s_PanelSelection_005e9060 = (byte*)NativeMemory.AllocZeroed(0x100);

    /// <summary>
    /// Ghidra: FUN_00545340 @ 0x00545340 — draws one UI panel and returns the advanced primitive
    /// cursor. 17 call sites across the game; this is the shared window layer, not menu-specific.
    /// </summary>
    private static uint* DrawUiPanel(uint* otEntry, uint* prim, byte* descriptor)
    {
        EmitViewportRect(otEntry, &prim);

        // Which player slot drives this panel: taken from the owner object when it has one, and
        // from the active slot otherwise.
        uint slot;
        uint* owner = *(uint**)(descriptor + 0x8b0);
        bool resolved = false;
        if (owner != null)
        {
            uint kind = *owner & 0xfc0000;
            if (kind == 0x200000)
            {
                if (owner[0x1b] != 0) { slot = *(byte*)((nuint)owner[0x1b] + 8); resolved = true; }
                else slot = 0;
            }
            else if (kind == 0x400000) { slot = *((byte*)owner + 9); resolved = true; }
            else if (kind == 0x800000) { slot = (byte)owner[2]; resolved = true; }
            else slot = 0;
        }
        else slot = 0;

        if (!resolved) slot = s_SlotArea_005e9180[0x248 + (uint)s_Unknown_005de538 * 0x9c];
        // The original indexes four player slots with a stride of 0x9c/0x4c. `slot` comes from a
        // byte in .data, so a table this reconstruction has not populated yields 0..255 and walks
        // far past both arrays — and the value read then becomes a *write* offset below. This was
        // corrupting the managed heap (Fatal error 0x80131506), so it fails loudly instead.
        if (slot >= 4)
        {
            throw new InvalidOperationException(
                $"FUN_00545340: player slot {slot} is outside the four the slot tables hold. It " +
                "comes from DAT_005e93c8 via DAT_005de538; one of those is not initialised the way " +
                "the original has it.");
        }

        if ((*(uint*)(s_SlotFlags_005e93c0 + slot * 0x9c) & 0x10000) != 0)
        {
            slot = s_SlotArea_005e9180[0x248 + (uint)s_Unknown_005de538 * 0x9c];
        }

        byte slotIndex1 = s_PlayerSlots_005e9188[slot * 0x4c + 1];
        // The original reaches this only once the resource directory is loaded. Guarding is not
        // padding: a null here faults natively inside the emitters below, several frames later,
        // with nothing naming the cause.
        byte** atlasSlot = s_MainResourceBuffer_005e97c0 != null
            ? *(byte***)(s_MainResourceBuffer_005e97c0 + 0x94)
            : null;
        if (atlasSlot == null || *atlasSlot == null)
        {
            throw new InvalidOperationException(
                "FUN_00545340: the UI atlas at DAT_005e97c0+0x94 is not loaded, so the panel has " +
                "no texture metadata to draw from. LoadFirstLevelResources must run first.");
        }
        byte* atlas = *atlasSlot;

        uint tpage = *DecodeSlot(*(ushort*)descriptor);
        uint extent = *(uint*)(descriptor + 8);
        // Second bound, on the value rather than the index: `buttons` addresses the per-controller
        // input table (0x140 bytes, 0x10 per entry) and is used for writes, so it must land inside
        // it. 8 is the original's "no controller" sentinel and is a legitimate value here.
        int controller = s_PlayerSlots_005e9188[slot * 0x4c];
        if (controller * 0x10 + 0x10 > 0x140)
        {
            throw new InvalidOperationException(
                $"FUN_00545340: controller index {controller} for slot {slot} is past the input " +
                "state table at DAT_00604a20. Writing there would corrupt unrelated memory.");
        }
        int buttons = controller * 0x10;
        uint scrollFlags = *(uint*)(descriptor + 0x38);
        int position = *(int*)(descriptor + 4);

        // ---- the scroll arrows, four directions, each with its own repeat rule ----
        if ((scrollFlags & 0x1000000) != 0 && (descriptor[0x8c7] & 0x20) == 0)
        {
            byte* buttonState = s_InputStateBase_00604a20 + buttons;

            if ((sbyte)descriptor[0x8c4] != 0 && (buttonState[6] & 0x10) != 0
                && (descriptor[0x8c7] & 0x40) != 0)
            {
                descriptor[0x8c4] = (byte)((sbyte)descriptor[0x8c4] - 2);
                PlayUiSound3A();
            }
            if ((sbyte)descriptor[0x8c4] != 0 && (buttonState[6] & 0x10) != 0
                && (descriptor[0x8c7] & 0x80) != 0)
            {
                descriptor[0x8c4] = (byte)((sbyte)descriptor[0x8c4] - 3);
                PlayUiSound3A();
            }
            if ((sbyte)descriptor[0x8c4] != 0 && (buttonState[6] & 0x10) != 0
                && (descriptor[0x8c7] & 0xc0) == 0)
            {
                descriptor[0x8c4] = (byte)((sbyte)descriptor[0x8c4] - 1);
                PlayUiSound3A();
            }
            if ((int)(uint)descriptor[0x8c4] < (int)(descriptor[0x11] - 2)
                && (buttonState[6] & 0x40) != 0 && (descriptor[0x8c7] & 0x40) != 0)
            {
                descriptor[0x8c4] = (byte)(descriptor[0x8c4] + 2);
                PlayUiSound3A();
            }
            if ((int)(uint)descriptor[0x8c4] < (int)(descriptor[0x11] - 3)
                && (buttonState[6] & 0x40) != 0 && (descriptor[0x8c7] & 0x80) != 0)
            {
                descriptor[0x8c4] = (byte)(descriptor[0x8c4] + 3);
                PlayUiSound3A();
            }
            if ((int)(uint)descriptor[0x8c4] < (int)(descriptor[0x11] - 1)
                && (buttonState[6] & 0x40) != 0 && (descriptor[0x8c7] & 0xc0) == 0)
            {
                descriptor[0x8c4] = (byte)(descriptor[0x8c4] + 1);
                PlayUiSound3A();
            }

            // Confirm: latch the selection back into the per-panel table and clear the edge.
            ushort edge = *(ushort*)(s_InputStateBase_00604a20 + buttons + 2);
            if ((edge & 0x8000) != 0)
            {
                *(ushort*)(s_InputStateBase_00604a20 + buttons + 2) = (ushort)(edge & 0x7fff);
                byte flags = descriptor[0x8c7];
                descriptor[0x3b] = 0;
                descriptor[0x8c7] = (byte)(flags & 0xfb);

                if ((flags & 0x40) != 0)
                    s_PanelSelection_005e9060[descriptor[0x8c5]] = (byte)(descriptor[0x8c4] >> 1);
                if ((descriptor[0x8c7] & 0x80) != 0)
                    s_PanelSelection_005e9060[descriptor[0x8c5]] = (byte)(descriptor[0x8c4] / 3);
                if ((descriptor[0x8c7] & 0xc0) == 0)
                    s_PanelSelection_005e9060[descriptor[0x8c5]] = descriptor[0x8c4];

                PlayUiSound38();
                descriptor[0x8c7] &= 0x3f;
            }

            uint clut = *(uint*)(descriptor + 0x3c);
            uint arrow = scrollFlags + (uint)((descriptor[0x8c4] + 0x155555) * 0xc00);
            prim = EmitSprite(otEntry, prim, tpage, (uint)position, arrow, clut,
                              *(uint*)(atlas + 0xd8 + (uint)slotIndex1 * 8));
            prim = EmitSprite(otEntry, prim, tpage, (uint)position, arrow + 0x400, clut,
                              *(uint*)(atlas + 0xc0));
        }

        // ---- the confirm cursor ----
        uint cursorFlags = *(uint*)(descriptor + 0x30);
        if ((cursorFlags & 0x1000000) != 0 && (descriptor[0x8c7] & 0x20) == 0)
        {
            ushort edge = *(ushort*)(s_InputStateBase_00604a20 + buttons + 2);
            if ((edge & 0x8000) != 0)
            {
                *(ushort*)(s_InputStateBase_00604a20 + buttons + 2) = (ushort)(edge & 0x7fff);
                descriptor[0x33] = 0;
                descriptor[0x8c7] &= 0xfd;
                PlayUiSound38();
            }

            uint clut = *(uint*)(descriptor + 0x34);
            uint packed = (cursorFlags & 0xffff0000) | (byte)cursorFlags | (((extent >> 0x10) & 0xff) << 8);
            prim = EmitSprite(otEntry, prim, tpage, (uint)position, packed + 0x40c, clut,
                              *(uint*)(atlas + 0xd8 + (uint)slotIndex1 * 8));
            prim = EmitSprite(otEntry, prim, tpage, (uint)position, packed, clut,
                              *(uint*)(atlas + (s_FadeCounter_005e9960 >> 1 & 3) * 8));
        }

        // ---- up to four decorations ----
        uint* cursor = prim;
        byte* slotDesc = descriptor + 0x48;
        byte* slotAtlas = atlas + 0x20;
        for (int i = 4; i != 0; i--)
        {
            uint flags = *(uint*)slotDesc;
            if ((flags & 0x1000000) != 0)
            {
                cursor = EmitSprite(otEntry, cursor, tpage, (uint)position, flags,
                                    *(uint*)(slotDesc + 4), *(uint*)slotAtlas);
                prim = cursor;
            }
            slotAtlas += 8;
            slotDesc += 8;
        }

        // ---- the frame: four corners then four edges ----
        uint frame = *(uint*)(descriptor + 0x40);
        if ((frame & 0x1000000) != 0)
        {
            uint clut = *(uint*)(descriptor + 0x44);
            prim = EmitSprite(otEntry, cursor, tpage, (uint)position, frame, clut, *(uint*)(atlas + 0x60));
            uint width = extent & 0xffff;
            prim = EmitSprite(otEntry, prim, tpage, width + (uint)position, frame, clut, *(uint*)(atlas + 0x68));
            uint height = extent & 0xffff0000;
            uint farCorner = width + (uint)position + height;
            prim = EmitSprite(otEntry, prim, tpage, farCorner, frame, clut, *(uint*)(atlas + 0x78));
            prim = EmitSprite(otEntry, prim, tpage, farCorner - width, frame, clut, *(uint*)(atlas + 0x70));

            uint horizontal = (extent - 0x10) & 0xffff | 0x100000;
            prim = EmitTintedSprite(otEntry, prim, tpage, (uint)position + 0x10, frame, clut,
                                    *(uint*)(atlas + 0x40), horizontal);
            prim = EmitTintedSprite(otEntry, prim, tpage, (uint)position + 0x10 + height, frame, clut,
                                    *(uint*)(atlas + 0x48), horizontal);
            uint vertical = height - 0x100000 | 0x10;
            prim = EmitTintedSprite(otEntry, prim, tpage, (uint)position + 0x100000, frame, clut,
                                    *(uint*)(atlas + 0x50), vertical);
            cursor = EmitTintedSprite(otEntry, prim, tpage, (uint)position + 0x100000 + width, frame, clut,
                                      *(uint*)(atlas + 0x58), vertical);
            prim = cursor;
        }

        // ---- the text: copy the display list FUN_005443a0 built inside the descriptor,
        //      offsetting each sprite by the panel's position. The 24-bit link mask again.
        if ((*(uint*)(descriptor + 0x20) & 0x1000000) != 0)
        {
            uint link = *(uint*)(descriptor + 0xa4);
            int guard = 0;
            for (uint* node = (uint*)(link & 0xffffff); node != (uint*)0xffffff; node = (uint*)(link & 0xffffff))
            {
                // The original cannot reach a null link: ClearOTagR terminates the descriptor's
                // own ordering table with the 0xffffff sentinel, so the walk always ends. Reaching
                // null here means this reconstruction let the panel be drawn before the text engine
                // ever cleared that table — a defect on our side, and one that would otherwise show
                // up as an unexplained native fault.
                if (node == null || ++guard > 0x400)
                {
                    throw new InvalidOperationException(
                        $"FUN_00545340: the panel's ordering table at descriptor+0xa4 is not a " +
                        $"terminated list ({(node == null ? "null link" : "over 1024 nodes")}). It is " +
                        "cleared by control code 0x0c in FUN_005443a0 — the text engine must have " +
                        "laid the page out at least once before the panel is drawn.");
                }

                if ((*node & 0xff000000) != 0)
                {
                    cursor[1] = node[1];
                    prim[2] = node[2];
                    prim[3] = node[3] + (uint)position;
                    prim[4] = node[4];
                    prim[5] = node[5];
                    prim[0] = *otEntry | 0x5000000;
                    *otEntry = (uint)(nuint)prim;
                    cursor = prim + 6;
                    prim = cursor;
                }
                link = *node;
            }
        }

        // ---- the panel background ----
        if ((*(uint*)(descriptor + 0x18) & 0x1000000) != 0)
        {
            prim = EmitTintedSprite(otEntry, cursor, tpage, (uint)position,
                                    *(uint*)(descriptor + 0x18), *(uint*)(descriptor + 0x1c),
                                    *(uint*)(atlas + 0x80), extent);
        }

        EmitClipRect(otEntry, &prim);
        return prim;
    }

    // ---------------------------------------------------------------------------------------
    // Deliberately deferred. Each has a decompiled body in Blaze.c and is ordinary uncovered
    // code, not blocked work; they are stubbed so the frame structure above can be exercised and
    // the runtime can name the next target. See the change proposal for why.
    // ---------------------------------------------------------------------------------------


    private static void OptionsScreenStub()
        => throw new NotImplementedException("FUN_00512320: the options page (80 lines)");

    private static int InputDrainStub()
        => throw new NotImplementedException("FUN_005048f0: joystick state processing (76 lines)");

    // Ghidra: &DAT_0056e670 — the menu's label table, recovered verbatim from Legacy/Blaze.exe.
    // A 0x0c length byte, then five cp1252 labels separated by carriage returns, ending 0x09 '?'.
    // They match the five switch cases in RunMainMenuFrameLoop one for one and in order:
    //   "Début du jeu"                -> game state 1
    //   "Sélection des personnages"   -> game state 4
    //   "Menu objets"                 -> game state 6
    //   "Options"                     -> the options page
    //   "Quitter"                     -> Exit(0)
    private static readonly byte* s_MenuLabels_0056e670 = BuildMenuLabels();

    private static byte* BuildMenuLabels()
    {
        ReadOnlySpan<byte> bytes =
        [
            0x0c, 0x44, 0xe9, 0x62, 0x75, 0x74, 0x20, 0x64, 0x75, 0x20, 0x6a, 0x65, 0x75, 0x0d,
            0x53, 0xe9, 0x6c, 0x65, 0x63, 0x74, 0x69, 0x6f, 0x6e, 0x20, 0x64, 0x65, 0x73, 0x20,
            0x70, 0x65, 0x72, 0x73, 0x6f, 0x6e, 0x6e, 0x61, 0x67, 0x65, 0x73, 0x0d,
            0x4d, 0x65, 0x6e, 0x75, 0x20, 0x6f, 0x62, 0x6a, 0x65, 0x74, 0x73, 0x0d,
            0x4f, 0x70, 0x74, 0x69, 0x6f, 0x6e, 0x73, 0x0d,
            0x51, 0x75, 0x69, 0x74, 0x74, 0x65, 0x72, 0x0d,
            0x09, 0x3f, 0x00, 0x00,
        ];

        byte* buffer = (byte*)NativeMemory.AllocZeroed((nuint)bytes.Length);
        for (int i = 0; i < bytes.Length; i++) buffer[i] = bytes[i];
        return buffer;
    }

    // Ghidra: &DAT_0056dd58 — the menu context struct FUN_00544350 writes into. Its highest touched
    // offset is 0x8c7, and the next named global sits at 0x0056e670, so it spans 0x918 bytes.
    /// <summary>
    /// Ghidra: &amp;DAT_0056dd58 — the main menu's panel descriptor, **statically initialised in
    /// `.data`** and recovered verbatim from `Legacy/Blaze.exe`. Only 35 of its bytes are
    /// non-zero; the rest is the scratch the text engine and the renderer fill in.
    /// <para>
    /// Leaving it zeroed is what produced a black screen: every feature test in
    /// <see cref="DrawUiPanel"/> is <c>field &amp; 0x1000000</c>, so a zeroed descriptor draws
    /// nothing at all — no background, no frame, no text.
    /// </para>
    /// Allocated low because <see cref="RunTextEngine"/> masks it to 24 bits.
    /// </summary>
    // Lazy, not a field initializer: field initializers run inside the type constructor, so a
    // failure to secure low memory here would surface as TypeInitializationException — outside the
    // reach of the module initializer's try/catch, and therefore unrecoverable by relaunching.
    private static byte* s_MenuContextStorage;
    private static byte* s_MenuContext_0056dd58
        => s_MenuContextStorage != null ? s_MenuContextStorage : (s_MenuContextStorage = BuildMenuContext());

    private static byte* BuildMenuContext()
    {
        // The allocation keeps 0x918 bytes of slack, but the descriptor proper ends at +0x8c7:
        // the bytes beyond it in the original belong to neighbouring globals, not to this struct
        // (0x0056e620 is the controller remap table, 0x0056e660 its four-entry pointer table).
        byte* ctx = AllocLowZeroed(0x918);

        // (offset, value) pairs, read straight out of the binary. Key fields:
        //   +0x00  0x0031            tpage/clut slot, indexes DAT_005e8f00
        //   +0x04  0x00080008        panel position (8, 8)
        //   +0x08  0x00480088        panel extent (0x88 x 0x48)
        //   +0x18  0x010F0808        background   — bit 0x1000000 set, so it draws
        //   +0x20  0x01070C10        text         — set
        //   +0x30  0x000F2874        cursor       — clear
        //   +0x38  0x000F0C00        scroll arrows— clear
        //   +0x40  0x010F0000        frame        — set
        ReadOnlySpan<(short Offset, byte Value)> initial =
        [
            (0x000,0x31), (0x004,0x08), (0x006,0x08), (0x008,0x88), (0x00A,0x48), (0x012,0x1A),
            (0x013,0x0C), (0x018,0x08), (0x019,0x08), (0x01A,0x0F), (0x01B,0x01), (0x01C,0x20),
            (0x01D,0x10), (0x01E,0xCF), (0x01F,0x02), (0x020,0x10), (0x021,0x0C), (0x022,0x07),
            (0x023,0x01), (0x027,0x01), (0x02F,0x01), (0x030,0x74), (0x031,0x28), (0x032,0x0F),
            (0x037,0x01), (0x039,0x0C), (0x03A,0x0F), (0x03F,0x01), (0x042,0x0F), (0x043,0x01),
            (0x047,0x01), (0x04F,0x01), (0x057,0x01), (0x05F,0x01), (0x067,0x01),
        ];

        foreach ((short offset, byte value) in initial) ctx[offset] = value;
        return ctx;
    }

    /// <summary>
    /// Ghidra: FUN_00544350 @ 0x00544350 — binds the label table and its flags into the menu
    /// context. The call site is <c>FUN_00544350(&amp;DAT_0056e670, 0, &amp;DAT_0056dd58, 0)</c>.
    /// </summary>
    private static void BindMenuLabels(byte* labels, uint flags, byte* context, uint extra)
    {
        *(uint*)(context + 0x8b0) = flags;
        context[0x8c7] |= 1;
        *(byte**)(context + 0x8ac) = labels;
        *(uint*)(context + 0x8a8) = extra;

        // Ghidra: if (*param_1 == 8) FUN_00535300(0). The label table's first short is 0x440c
        // (recovered from the binary), so this never fires at this call site — but the branch is
        // kept because other callers may pass a table that does.
        if (*(short*)labels == 8)
        {
            throw new NotImplementedException(
                "FUN_00535300: reached only when the bound table's first short is 8 — " +
                "not the case for the main menu's label table");
        }
    }

    /// <summary>
    /// Ghidra: FUN_00547140 @ 0x00547140 — the CRT `rand()`. The constants are MSVC's linear
    /// congruential generator, which also identifies its neighbour: `FUN_00547130`, already called
    /// by RunMainLoop with 1, is `srand`, and the global they share (`DAT_0056e9c0`) is the seed
    /// rather than the flag an earlier comment called it.
    /// </summary>
    private static int CrtRand()
    {
        s_RandSeed_0056e9c0 = s_RandSeed_0056e9c0 * 0x343fd + 0x269ec3;
        return (int)((s_RandSeed_0056e9c0 >> 0x10) & 0x7fff);
    }

    /// <summary>
    /// Ghidra: FUN_00545140 @ 0x00545140 — flushes the deferred VRAM upload queue: walks the
    /// singly-linked list whose head <see cref="FrameBegin"/> resets each frame, calling
    /// <c>Ordinal_674</c> (libgpu LoadImage) with each node's rect at +8 and its data at +4.
    /// </summary>
    private static void FlushDeferredImageUploads()
    {
        for (uint* node = (uint*)(nuint)s_Unknown_005e8310;
             node != null;
             node = (uint*)(nuint)node[0])
        {
            Ordinal_674(node + 2, (void*)(nuint)node[1]);
        }
    }

    // =====================================================================
    // Per-frame input polling (Ghidra: FUN_00512520 / FUN_005048f0 / FUN_00504a30 / FUN_005357a0).
    //
    // These share three global regions whose *relative* offsets the code depends on, so each is
    // modelled as one block with derived pointers rather than as separate allocations:
    //   0x00604a20  the per-button input state table (0x10 bytes per entry)
    //   0x00604b00  the two joystick axis/state blocks
    //   0x005e9180  the player-slot table and its parallel per-slot array at +0x240
    // =====================================================================

    private static readonly byte* s_InputStateBase_00604a20 = (byte*)NativeMemory.AllocZeroed(0x140);
    private static byte* s_ButtonStates_00604a2a => s_InputStateBase_00604a20 + 0x0a;
    private static ushort* s_JoyAxis1_00604b0a => (ushort*)(s_InputStateBase_00604a20 + 0xea);
    private static uint* s_JoyBlock1_00604b0c => (uint*)(s_InputStateBase_00604a20 + 0xec);
    private static ushort* s_JoyAxis2_00604b2e => (ushort*)(s_InputStateBase_00604a20 + 0x10e);

    private static readonly byte* s_SlotArea_005e9180 = (byte*)NativeMemory.AllocZeroed(0x500);
    private static byte* s_PlayerSlots_005e9188 => s_SlotArea_005e9180 + 8;
    private static byte* s_SlotFlags_005e93c0 => s_SlotArea_005e9180 + 0x240;

    // Ghidra: the four 16-byte controller→player remap rows at 0x0056acf8, reached through the
    // pointer table at 0x0056ad38. Recovered verbatim from Legacy/Blaze.exe. The row is chosen by
    // which of the two joystick axes reads the -0x8000 "absent" sentinel.
    private static readonly byte[][] s_ControllerRemapRows =
    [
        [0x00, 0x04, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0x08, 0, 0, 0, 0, 0, 0, 0],
        [0x00, 0x01, 0x02, 0x03, 0x04, 0x08, 0x08, 0x08, 0x08, 0, 0, 0, 0, 0, 0, 0],
        [0x00, 0x04, 0x05, 0x06, 0x07, 0x08, 0x08, 0x08, 0x08, 0, 0, 0, 0, 0, 0, 0],
        [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0, 0, 0, 0, 0, 0, 0],
    ];

    /// <summary>
    /// Ghidra: the index computation shared by FUN_00542250 and FUN_005357a0 — one bit per joystick,
    /// set when its axis reads the <c>-0x8000</c> sentinel.
    /// </summary>
    private static int ControllerRemapIndex()
    {
        int index = *(short*)s_JoyAxis1_00604b0a == -0x8000 ? 1 : 0;
        if (*(short*)s_JoyAxis2_00604b2e == -0x8000) index += 2;
        return index;
    }

    /// <summary>
    /// Ghidra: FUN_00512520 @ 0x00512520 — the debug hotkey handler, polled once per frame:
    /// F3 opens the options page, F11/F12 step the detail level, F9 toggles the small-texture flag.
    /// Returns 1 when the options page ran, 0 otherwise.
    /// </summary>
    private static int PollDebugHotkeys(byte* context)
    {
        s_Unknown_00579270 = context;

        if (s_Unknown_005de554 == 0 && GetAsyncKeyState(0x72 /* VK_F3 */) < 0)
        {
            int gammaBefore = s_Unknown_005d9de0;
            ReInitializeAllTextures();
            int shiftX, shiftY;
            GetAbsShift(&shiftX, &shiftY);
            SetAbsShift(0, 0);
            if (s_SizeSetting_005de160 < 7) s_SmallTextureFlag = 1;
            OptionsScreenStub();
            SetAbsShift(shiftX, shiftY);
            SaveOptionsIni();
            if (gammaBefore != s_Unknown_005d9de0)
            {
                // Note: this site scales by _DAT_0054d000, *not* the _DAT_0054d074 the menu's
                // options case uses. Two distinct constants; kept distinct.
                CreateTranslationTables(s_Unknown_005d9de0 * s_GammaScale_0054d000);
            }
            return 1;
        }

        if (GetAsyncKeyState(0x7a /* VK_F11 */) != 0)
        {
            if (s_SizeSetting_005de160 != 0)
            {
                s_SizeSetting_005de160--;
                SetShift((0xb - s_SizeSetting_005de160) * 0x10,
                         (0xb - s_SizeSetting_005de160) * 0x0c,
                         s_SizeSetting_005de160, 0);
                if (s_SizeSetting_005de160 < 7) s_SmallTextureFlag = 1;
                SaveOptionsIni();
                do { Sleep(100); } while (GetAsyncKeyState(0x7a) < 0);
                return 0;
            }
        }
        else if (GetAsyncKeyState(0x7b /* VK_F12 */) != 0)
        {
            // The clamp table here is DAT_00562e00 — recovered as { 1, 7, 11, 16, 23, 0 }. F12
            // raises Size up to the ceiling for the current Resolution; at Resolution=4 that
            // ceiling is 23, which is what the installed BLAZE.cfg holds.
            int limit = s_ResolutionIndexForClamp < s_ResolutionDetailLimit.Length
                ? s_ResolutionDetailLimit[s_ResolutionIndexForClamp]
                : int.MaxValue;
            if (s_SizeSetting_005de160 < limit)
            {
                s_SizeSetting_005de160++;
                SetShift((0xb - s_SizeSetting_005de160) * 0x10,
                         (0xb - s_SizeSetting_005de160) * 0x0c,
                         s_SizeSetting_005de160, 0);
                SaveOptionsIni();
                do { Sleep(100); } while (GetAsyncKeyState(0x7b) < 0);
                return 0;
            }
        }
        else if (GetAsyncKeyState(0x78 /* VK_F9 */) != 0 && s_SizeSetting_005de160 > 6)
        {
            s_SmallTextureFlag = s_SmallTextureFlag == 0 ? 1u : 0u;
            SaveOptionsIni();
            do { Sleep(100); } while (GetAsyncKeyState(0x78) < 0);
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00504a30 @ 0x00504a30 — button edge detection and auto-repeat over two blocks of
    /// four slots. `0x4100` marks a live entry; the XOR pair yields pressed/released masks, and the
    /// counter at +0xa runs 0xc frames before repeating every 2.
    /// </summary>
    private static void UpdateButtonEdges()
    {
        ushort* state = (ushort*)s_InputStateBase_00604a20;
        uint* joyBlock = s_JoyBlock1_00604b0c;

        for (int block = 0; block < 2; block++)
        {
            uint* entry = joyBlock;
            for (int i = 0; i < 4; i++)
            {
                uint raw = *entry;
                if ((short)raw == 0x4100)
                {
                    state[3] = 0;
                    ushort current = (ushort)~(ushort)(raw >> 0x10);
                    state[4] = state[0];
                    ushort changed = (ushort)(state[0] ^ current);
                    state[0] = current;
                    state[1] = changed;

                    if ((changed & 0xf0) == 0)
                    {
                        byte countdown = (byte)(((byte*)(state + 5))[0] - 1);
                        ((byte*)(state + 5))[0] = countdown;
                        if (countdown == 0)
                        {
                            ((byte*)(state + 5))[0] = 2;
                            if (((byte)state[0] & 0xf0) != 0) state[3] = (ushort)((byte)state[0] & 0xf0);
                        }
                    }
                    else
                    {
                        ((byte*)(state + 5))[0] = 0xc;
                        state[3] = (ushort)((byte)state[0] & 0xf0);
                    }

                    state[2] = (ushort)(state[4] & state[1]);
                    ushort held = (ushort)(state[0] & state[1]);
                    state[1] = held;
                    ((byte*)state)[0xb] |= 0x80;
                    ((byte*)state)[0xb] = (((byte)state[2] | (byte)held) & 0xf0) != 0
                        ? (byte)(((byte*)state)[0xb] | 2)
                        : ((byte*)state)[0xb];
                }
                else
                {
                    ((byte*)state)[0xb] &= 0x7e;
                }

                entry += 2;
                state += 8;
            }
            joyBlock += 9;
        }
    }

    /// <summary>
    /// Ghidra: FUN_005357a0 @ 0x005357a0 — assigns controllers to the four player slots through the
    /// remap row selected by <see cref="ControllerRemapIndex"/>.
    /// </summary>
    private static void AssignControllersToSlots()
    {
        byte[] row = s_ControllerRemapRows[ControllerRemapIndex()];

        byte* slotFlags = s_SlotFlags_005e93c0;
        byte* slot = s_PlayerSlots_005e9188;

        for (int i = 0; i < 4; i++)
        {
            byte mapped = row[slot[1]];
            int mappedOffset = (sbyte)mapped * 0x10;
            byte* current = s_ButtonStates_00604a2a + slot[0] * 0x10 + 1;
            byte currentFlags = *current;

            if ((currentFlags & 0x80) == 0)
            {
                byte* target = s_ButtonStates_00604a2a + mappedOffset + 1;
                byte targetFlags = *target;
                if ((targetFlags & 0x80) == 0)
                {
                    *current = (byte)(currentFlags & 0xfe);
                    *target = (byte)(*target & 0xfe);
                    slot[0] = 8;
                }
                else if ((targetFlags & 1) == 0)
                {
                    *target = (byte)(targetFlags | 1);
                    slot[0] = mapped;
                }
            }
            else if ((slotFlags[2] & 1) != 0 && slot[0] != (sbyte)mapped)
            {
                *current = (byte)(currentFlags & 0x7e);
                slot[0] = 8;
            }

            slotFlags += 0x9c;
            slot += 0x4c;
        }
    }

    /// <summary>
    /// Ghidra: FUN_005048f0 @ 0x005048f0 — polls the DirectInput joystick device and folds its
    /// state into the per-slot tables.
    /// <para>
    /// TODO(REVERSE): its first two calls operate on the device object `FUN_00523120` creates, and
    /// `SetupJoystickHandlers` still skips that creation — so the device pointer is null here and
    /// dereferencing it would fault. The guard below skips exactly those two calls and lets the
    /// rest of the polling run; it is a deviation, and it disappears once the device exists.
    /// </para>
    /// </summary>
    private static int PollJoystickState()
    {
        if (s_JoystickDevice == null)
        {
            return 0;
        }

        throw new NotImplementedException(
            "FUN_005048f0: reached with a live DirectInput device — FUN_005230b0/FUN_00523660 " +
            "still need reconstructing");
    }

    private static void* s_JoystickDevice;      // Ghidra: DAT_00604b54 / DAT_00604b50
    private static byte* s_Unknown_00579270;    // Ghidra: DAT_00579270
    private static int s_Unknown_005de554;      // Ghidra: DAT_005de554
    // Ghidra: DAT_00566cc4 — **the same global as s_ResolutionIndex**, not a second one. Blaze.c
    // line 58267 fills it from the ini's Resolution key, exactly as LoadOptionsIni does here, and
    // the reconstruction had split one global into two fields with only one of them ever assigned.
    // Third instance of this shape today, after DAT_0056e61f and DAT_005e8fc0.
    private static int s_ResolutionIndexForClamp => (int)s_ResolutionIndex;

    // Ghidra: _DAT_0054d000 — the gamma scale this hotkey path uses. Distinct from _DAT_0054d074.
    // TODO(REVERSE): .rdata content; 1.0f leaves the product equal to the raw setting.
    private const float s_GammaScale_0054d000 = 1.0f;

    /// <summary>Ghidra: the four calls guarded by arg5 in FUN_0052a280.</summary>
    private static void PollInputAndAudio()
    {
        // Ghidra: FUN_00512520(&DAT_00604a20 +
        //   (byte)(&DAT_005e9188)[(byte)(&DAT_005e93c8)[(_DAT_005de538 & 0xff) * 0x9c] * 0x4c] * 4)
        // The outer index had been hardcoded to 0 instead of reading _DAT_005de538. Equivalent
        // while that global is 0, but not faithful — and FUN_00545340 uses the same expression,
        // which is what settled it.
        int slotIndex = s_PlayerSlots_005e9188[
            s_SlotArea_005e9180[0x248 + (uint)(s_Unknown_005de538 & 0xff) * 0x9c] * 0x4c];
        PollDebugHotkeys(s_InputStateBase_00604a20 + slotIndex * 4);

        PollJoystickState();
        UpdateButtonEdges();
        AssignControllersToSlots();
    }

    private static void ControllerBannerStub()
        => throw new NotImplementedException(
            "FUN_0051c120: the \"Veuillez connecter la manette !\" banner");

    /// <summary>
    /// Ghidra: FUN_00533000 @ 0x00533000 — per-frame **palette animation** (CLUT cycling). Walks a
    /// list of animation descriptors and, for each one whose counter has reached its period,
    /// rotates a run of colour entries by one and resets the counter.
    /// <para>
    /// The CLUTs it rotates are the ones <see cref="DecodeResourceChunk"/> mirrors into main video
    /// memory: both use the same address formula, <c>(bank + 0xe10) * 0x200 + MainMem</c>. That
    /// mirror exists precisely so this can animate them — the two functions are two halves of one
    /// mechanism, which is worth knowing before either is changed.
    /// </para>
    /// <para>
    /// Each descriptor is one packed dword:
    /// <c>[7:0]</c> CLUT slot within the bank (32 bytes each), <c>[15:8]</c> how many 16-bit colour
    /// entries take part in the rotation, <c>[23:16]</c> the period in frames, <c>[31:24]</c> the
    /// running frame counter. A descriptor array is terminated by a zero word; the list itself is
    /// (descriptor array, bank index) pairs terminated by a null pointer.
    /// </para>
    /// </summary>
    private static void AnimatePalettes(byte* resourceHeader)
    {
        if (resourceHeader == null) return;

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return;

        // Ghidra: puVar12 = *(undefined4 **)(param_1 + 100)
        uint** list = *(uint***)(resourceHeader + 0x64);
        if (list == null) return;

        while (true)
        {
            uint* descriptors = list[0];
            if (descriptors == null) break;

            uint bank = (uint)(nuint)list[1];

            uint descriptor = *descriptors;
            while (descriptor != 0)
            {
                // Ghidra: uVar7 = uVar7 + 0x1000000 — tick the counter in the top byte.
                descriptor += 0x1000000;
                uint updated = descriptor;

                // Ghidra: counter byte == period byte — this descriptor steps this frame.
                if ((byte)(descriptor >> 0x18) == (byte)(descriptor >> 0x10))
                {
                    updated = descriptor & 0xffffff;   // reset the counter, keep the rest

                    // Same CLUT address formula as DecodeResourceChunk's mirror.
                    ushort* clut = (ushort*)(mainMem
                        + (descriptor & 0xff) * 0x20
                        + ((bank & 0xff) + 0xe10) * 0x200);

                    int count = (byte)(updated >> 8);   // entries taking part
                    ushort first = clut[0];

                    // Rotate left by one: entries [1..count-1] move down, the old first goes last.
                    // The original open-codes this as a dword copy plus a 16-bit tail; the loop
                    // below is the same movement, and the count-1 == 0 case leaves the single
                    // entry untouched exactly as the original's guard does.
                    for (int i = 1; i < count; i++)
                    {
                        clut[i - 1] = clut[i];
                    }
                    if (count >= 1) clut[count - 1] = first;
                }

                *descriptors = updated;
                descriptors++;
                descriptor = *descriptors;
            }

            list += 2;
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
                RunMainMenuState();
                return;
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
