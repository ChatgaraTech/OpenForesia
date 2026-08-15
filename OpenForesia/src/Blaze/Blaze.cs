// OpenForesia â€” reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth â€” never modify it, see Prompt.md / Strategy.md).
//
// This file follows the call graph starting at the PE entry point `entry` and works
// outward. Progress, naming decisions and open questions are tracked in
// src/Blaze/REVERSE_STATUS.md â€” read that file before continuing this work.

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
    // wsyskern.c:9769), so it must be released with wsyskern's `Free` below â€” NOT with this file's
    // CRT-derived Free, which maps to NativeMemory and would corrupt the heap.
    [DllImport("wsyskern.dll")]
    private static extern byte* LoadBitmapFile(byte* fileName, uint mode);

    // wsyskern.dll ordinal 89 (named export, wsyskern.c:9818 @ 0x1000bc30). The matching deallocator
    // for anything wsyskern's `Malloc` produced. Blaze.c calls it exactly twice, both on the attract
    // screen's title bitmap (Blaze.c:89940 and 90131) â€” note it is the capitalized `Free`/`Malloc`
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
    // resource 0x66 â€” the same resource id whose RT_BITMAP LoadBitmapEx loads, i.e. hBitmap /
    // hPalette are that dialog's splash image â€” and writes the chosen driver back to the ini.
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
    //     (fills DAT_011cb420[0x1000] via fsin()) â€” no arguments, run once.
    //   - ordinals 450/451 (winsys3d.c: FUN_00a25c20/FUN_00a25c40) are simple reads from that
    //     same 0x1000-entry ring buffer, 450 phase-shifted by 0x400 (a quarter period) relative
    //     to 451 â€” i.e. a classic packed cos()/sin() lookup table pair.
    //   - ordinal 790 (winsys3d.c: FUN_00a06410) just stores the pointer into a DLL-private
    //     global (DAT_011be798) that winsys3d.dll later calls indirectly as a callback
    //     (confirmed via cross-reference in winsys3d.c); likely a device-lost/reset notification
    //     hook. TODO(REVERSE): the callback passed at the RunApplication call site
    //     (Ghidra: &LAB_00512ef0) has no decompiled function body in Blaze.c (Ghidra only saw it
    //     addressed-of, never as a call target), so it cannot be reconstructed yet.
    [DllImport("winsys3d.dll", EntryPoint = "#337")] private static extern void InitTrigTables();
    // libgte rcos/rsin. Bodies: winsys3d.c FUN_00a25c20 / FUN_00a25c40 â€” both plain lookups into the
    // same 0x1000-entry table at DAT_011cb420, rcos being rsin shifted a quarter turn
    // (`table[(a + 0x400) & 0xfff]`). 0x1000 steps per full turn, the PlayStation convention, which
    // is what makes `i << 8` sixteen evenly spaced positions in the character carousel.
    // Ordinals 452/453 are aliases of these same two entries.
    [DllImport("winsys3d.dll", EntryPoint = "#450")] private static extern int GetCosTableEntry(int index);
    [DllImport("winsys3d.dll", EntryPoint = "#451")] private static extern uint GetSinTableEntry(int index);
    [DllImport("winsys3d.dll", EntryPoint = "#790")] private static extern void SetDeviceLostCallback(void* callback);

    // The following winsys3d.dll ordinals are used directly by RunMainLoop (Ghidra: FUN_00529c10)
    // and its immediate dependency graph. Signatures/behavior were confirmed against winsys3d.c â€”
    // see Documentation/DLL/winsys3d.md for the full per-ordinal writeup. Several are literal
    // no-op stubs in the shipped DLL (`return 0;` / `return;` with no body) â€” they are still
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
    [DllImport("winsys3d.dll", EntryPoint = "#132")] private static extern int Ordinal_132(int a); // real: winsys3d.c FUN_00a053e0, VSync/frame-pacing wait â€” DLL-internal timing state
    [DllImport("winsys3d.dll", EntryPoint = "#136")] private static extern int Ordinal_136(int a); // real: winsys3d.c FUN_00a05770, get/set DAT_011be6a4 ("frame time offset"?), returns previous value
    [DllImport("winsys3d.dll", EntryPoint = "#138")] private static extern void Ordinal_138(); // confirmed no-op (FUN_00a26470)
    [DllImport("winsys3d.dll", EntryPoint = "#368")] private static extern void Ordinal_368(void* eightDwordStruct); // real: winsys3d.c FUN_00a20ed0, copies 8 dwords into DLL-internal "shading defaults" state (EXT_011d0828); exact field semantics TODO(REVERSE)
    [DllImport("winsys3d.dll", EntryPoint = "#378")] private static extern void Ordinal_378(int a, int b); // real: winsys3d.c FUN_00a21020, stores (a,b) into DLL-internal state (EXT_011cb414/EXT_011cb410); exact semantics TODO(REVERSE)
    // Ordinals 376/379/457 are winsys3d.dll's port of the PlayStation SDK's **libgte** (the
    // geometry engine), the sibling of the libgpu ordinals below. Identified from their bodies:
    [DllImport("winsys3d.dll", EntryPoint = "#376")] private static extern void Ordinal_376(byte r, byte g, byte b); // real: winsys3d.c FUN_00a20fe0 â€” libgte SetBackColor: three bytes into DAT_011f0898
    [DllImport("winsys3d.dll", EntryPoint = "#379")] private static extern void Ordinal_379(int h); // real: winsys3d.c FUN_00a21040 â€” libgte SetGeomScreen: stores the projection distance, then rebuilds the 0x10000-entry reciprocal table at EXT_011d0898
    [DllImport("winsys3d.dll", EntryPoint = "#457")] private static extern int Ordinal_457(int y, int x); // real: winsys3d.c FUN_00a25d10 â€” libgte ratan2
    // The five entry points FUN_0051d1f0 (the animated-model draw) needs. Identified from their
    // bodies; see Documentation/DLL/winsys3d.md. Declared ahead of that reconstruction so the
    // identification is not lost.
    [DllImport("winsys3d.dll", EntryPoint = "#366")] private static extern void Ordinal_366(void* matrix); // real: winsys3d.c FUN_00a20e70 â€” libgte SetRotMatrix: copies the nine rotation shorts into the MATRIX register (ordinal 814, offset 0)
    [DllImport("winsys3d.dll", EntryPoint = "#369")] private static extern void Ordinal_369(void* matrix); // real: winsys3d.c FUN_00a20ef0 â€” libgte SetTransMatrix: copies the three translations from the matrix's +0x14/+0x18/+0x1c into the same register's translation row
    [DllImport("winsys3d.dll", EntryPoint = "#360")] private static extern void Ordinal_360(void* destMatrix, void* a, void* b); // real: winsys3d.c FUN_00a20630 â€” composes two matrices (libgte CompMatrix family), with its own MMX/scalar split
    [DllImport("winsys3d.dll", EntryPoint = "#418")] private static extern void Ordinal_418(void* vertices, void* outScreenXy, void* outDepth, void* outFlags, uint count); // real: winsys3d.c FUN_00a22010 â€” batch RotTransPers: transforms `count` vertices through the MATRIX register (ordinal 814) into screen XY and depth arrays. MMX/scalar split inside.
    [DllImport("winsys3d.dll", EntryPoint = "#793")] private static extern void Ordinal_793(void* normals, void* outColours, uint triangleCount); // real: winsys3d.c FUN_00a03e70 â€” batch lighting. Gated on the DLL's `Elo_NoLighting`, which when set writes zeroes instead. Consumes EXT_00b6d198, i.e. data export ordinal 817.
    [DllImport("winsys3d.dll", EntryPoint = "#794")] private static extern void Ordinal_794(void* matrix, void* scale); // real: winsys3d.c FUN_00a20540 â€” libgte ScaleMatrix: scales each of the matrix's three columns by one component of `scale`.
    // real: winsys3d.c FUN_00a226c0 â€” the depth-less sibling of ordinal 418, taken when the model's
    // 0x400 flag is set. **Three** parameters, not two: Ghidra names them (param_1, param_2,
    // param_3) and then swaps two of them inside the scalar path (`param_3 = param_2; param_2 =
    // iVar4`), which reads as a two-argument function if the swap is skipped. Both paths agree that
    // the third is the count: the scalar loop ends on `while (param_2 != 0)` after the swap, and the
    // MMX path runs `for (iVar4 = (int)param_3 * 3; iVar4 != 0; iVar4--)` without swapping. Same
    // count as ordinal 418, and three vertices consumed per unit in both.
    [DllImport("winsys3d.dll", EntryPoint = "#795")] private static extern void Ordinal_795(void* vertices, void* outScreenXy, uint count);

    // =====================================================================
    // La file de caractères tapés — **reconstruite plutôt qu'appelée dans la DLL**, et c'est le
    // seul endroit du portage où l'on s'écarte de « appeler winsys3d quand winsys3d le fait ».
    //
    // Les trois pièces sont EnableKBInput (0x00a03ef0, ordinal 775), GetKBChar (0x00a03f10,
    // ordinal 778) et l'écrivain FUN_00a03f50. Les deux premières sont exportées, la troisième
    // **ne l'est pas** : la table d'exports de winsys3d.dll ne contient nulle part son RVA
    // 0x3f50. Elle n'est appelée que par la procédure de fenêtre interne de la DLL, sur WM_CHAR.
    //
    // Or dans ce portage la fenêtre est la nôtre (D3D11Window) et cette procédure ne tourne
    // jamais. Appeler les deux exports laisserait donc GetKBChar rendre 0 pour toujours — et
    // l'écran de saisie du nom, qui coupe la traduction manette→clavier par DAT_00564a50, n'aurait
    // alors **plus aucune entrée**. Mesuré : le nom restait vide et aucune touche ne passait.
    //
    // La file fait huit lignes et se lit entièrement dans winsys3d.c ; la transcrire ici, alimentée
    // par notre WM_CHAR, reproduit exactement ce que fait la DLL. La solution de rechange aurait
    // été d'appeler FUN_00a03f50 à `base + 0x3f50`, ce qui suppose une adresse interne stable.
    // =====================================================================
    private static readonly byte[] s_TypedRing = new byte[0x400];   // &DAT_00b6d1a0
    private static int s_TypedRingRead;                             // DAT_00b6d5a0
    private static int s_TypedRingWrite;                            // DAT_00b6d5a4
    private static uint s_TypedRingArmed;                           // DAT_00b6dbcc

    /// <summary>winsys3d.c @ 0x00a03ef0 — arme ou désarme la file et remet ses deux curseurs.</summary>
    private static uint EnableKBInput(uint enable)
    {
        uint previous = s_TypedRingArmed;
        s_TypedRingRead = 0;
        s_TypedRingWrite = 0;
        s_TypedRingArmed = enable;
        return previous;
    }

    /// <summary>winsys3d.c @ 0x00a03f10 — dépile un caractère, ou 0 si désarmée ou vide.</summary>
    private static byte GetKBChar()
    {
        if (s_TypedRingArmed == 0) return 0;
        if (s_TypedRingRead == s_TypedRingWrite) return 0;
        byte value = s_TypedRing[s_TypedRingRead];
        s_TypedRingRead = s_TypedRingRead + 1 & 0x3ff;
        return value;
    }

    /// <summary>winsys3d.c @ 0x00a03f50 — l'écrivain, appelé par notre WM_CHAR.</summary>
    internal static void PushTypedCharacter(byte value)
    {
        if (s_TypedRingArmed == 0) return;
        s_TypedRing[s_TypedRingWrite] = value;
        s_TypedRingWrite = s_TypedRingWrite + 1 & 0x3ff;
    }
    // Ordinals 658/661/665/674 are winsys3d.dll's port of the PlayStation SDK's **libgpu** â€” which
    // is what the sibling `playst.dll` ("PlayStation") is named after, and unsurprising for a PSX
    // game ported to Windows. The two below are identified beyond doubt by their bodies: they write
    // exactly the documented DISPENV and DRAWENV field layouts, and the four global structs Blaze.c
    // passes them are spaced 20 / 92 bytes apart, matching sizeof(DISPENV) / sizeof(DRAWENV) exactly.
    // Both take (env, x, y, w, h) â€” a *screen rectangle*, not four opaque shorts.

    // winsys3d.c FUN_00a05ac0 â€” libgpu `SetDefDispEnv(DISPENV*, x, y, w, h)`. Writes disp={x,y,w,h},
    // screen={0,0,0x100,0xf0}, isinter=0, isrgb24=0 (18 of the struct's 20 bytes).
    [DllImport("winsys3d.dll", EntryPoint = "#658")] private static extern void SetDefDispEnv(void* dispEnv, short x, short y, short w, short h);

    // winsys3d.c FUN_00a05b50 â€” libgpu `SetDefDrawEnv(DRAWENV*, x, y, w, h)`. Writes clip={x,y,w,h},
    // ofs={x,y}, tw={0,0,0,0}, tpage=10, dtd=1, dfe=1, isbg=0, r0=g0=b0=0 (first 28 bytes of 92).
    [DllImport("winsys3d.dll", EntryPoint = "#661")] private static extern void SetDefDrawEnv(void* drawEnv, short x, short y, short w, short h);

    // Game-state-0 ordinals. winsys3d.c declares all of these with no parameters, but that is the
    // same decompiler artifact seen throughout this project â€” the **call sites are authoritative**,
    // and the argument counts below are theirs (Blaze.c: FUN_00529a80 and FUN_00542160).
    [DllImport("winsys3d.dll", EntryPoint = "#97")] private static extern int Ordinal_97(int a, int b, int c);
    [DllImport("winsys3d.dll", EntryPoint = "#762")] private static extern void Ordinal_762(void* p);
    [DllImport("winsys3d.dll", EntryPoint = "#765")] private static extern int Ordinal_765(int a, uint rgb);
    [DllImport("winsys3d.dll", EntryPoint = "#766")] private static extern int Ordinal_766(void* p);
    [DllImport("winsys3d.dll", EntryPoint = "#767")] private static extern void Ordinal_767(int a, uint rgb);
    [DllImport("winsys3d.dll", EntryPoint = "#768")] private static extern int Ordinal_768(int a);
    [DllImport("winsys3d.dll", EntryPoint = "#769")] private static extern int Ordinal_769(int a);
    [DllImport("winsys3d.dll", EntryPoint = "#770")] private static extern int Ordinal_770(void* p);

    // Frame ordinals. 688 is libgpu `ClearOTagR(u_long *ot, int n)` â€” winsys3d.c FUN_00a06160 keeps
    // both parameters, unlike its neighbours. 657/660 take a DISPENV/DRAWENV (PutDispEnv/PutDrawEnv
    // shaped), 701 takes the ordering table (DrawOTag shaped).
    [DllImport("winsys3d.dll", EntryPoint = "#688")] private static extern uint* Ordinal_688(uint* ot, int count);
    [DllImport("winsys3d.dll", EntryPoint = "#657")] private static extern int Ordinal_657(void* env);
    [DllImport("winsys3d.dll", EntryPoint = "#660")] private static extern int Ordinal_660(void* env);
    [DllImport("winsys3d.dll", EntryPoint = "#701")] private static extern void Ordinal_701(uint* ot);

    // libgpu MoveImage(RECT* src, int dstX, int dstY) â€” copies a rectangle inside the emulated
    // VRAM. Body confirmed at winsys3d.c:5348 (`/* 0x5df0  676 */`): it walks `src->h` rows of
    // `src->w` texels from the arena and writes them at (dstX, dstY).
    [DllImport("winsys3d.dll", EntryPoint = "#676")] private static extern void Ordinal_676(ushort* sourceRect, int destinationX, int destinationY);

    // real: winsys3d.c FUN_00a05e90 â€” libgpu `ClearImage(RECT*, r, g, b)`-shaped (takes a 4-short
    // RECT and floods VRAM), but its colour math decompiles as
    // `((p4 & 0xff) << (p3 + 10)) << (p2 + 5)`, which does not match ClearImage's plain (r,g,b).
    // TODO(REVERSE): left un-renamed until that is explained rather than assumed.
    [DllImport("winsys3d.dll", EntryPoint = "#665")] private static extern int Ordinal_665(void* rect, byte r, byte g, uint b);
    [DllImport("winsys3d.dll", EntryPoint = "#667")] private static extern void Ordinal_667(int a); // confirmed no-op (FUN_00a02e20)
    [DllImport("winsys3d.dll", EntryPoint = "#668")] private static extern int Ordinal_668(int a, int b, int c, int d, int e, int f); // real: winsys3d.c FUN_00a020a0, registers a debug/console text channel slot (unused feature in the shipped build)
    // real: winsys3d.c FUN_00a05d50 â€” libgpu `LoadImage(RECT*, u_long* data)`: copies `data` row by
    // row into the VRAM rectangle. `rect` is a 4-short RECT {x,y,w,h}, and the body early-outs on
    // rect.y > 0x1ff, confirming a 1024x512 VRAM addressing scheme.
    [DllImport("winsys3d.dll", EntryPoint = "#674")] private static extern int Ordinal_674(void* rect, void* srcBuffer);

    // winsys3d.c FUN_00a05bb0 â€” libgpu `OpenTIM(u_long *addr)`. Parks a cursor at `addr + 4` in the
    // DLL global DAT_011be790; ReadTIM below consumes and clears it, so the pair is one-shot.
    [DllImport("winsys3d.dll", EntryPoint = "#677")] private static extern int OpenTim(void* timData);

    // winsys3d.c FUN_00a05bd0 â€” libgpu `ReadTIM(TIM_IMAGE*)`. Parses the TIM header the cursor
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
    // (de)registration bookkeeping â€” see winsys3d.c FUN_00a040d0/FUN_00a04220 and neighbors).
    [DllImport("winsys3d.dll", EntryPoint = "#6")] private static extern void* Ordinal_6(uint messageId, uint p2, uint p3, void* callback); // real: winsys3d.c FUN_00a040d0, registers a handler, returns an opaque handle
    [DllImport("winsys3d.dll", EntryPoint = "#10")] private static extern int Ordinal_10(void* handle); // real: winsys3d.c FUN_00a04220, unregisters a handler returned by ordinal 6
    [DllImport("winsys3d.dll", EntryPoint = "#40")] private static extern void Ordinal_40(); // real: winsys3d.c FUN_00a04fd0 â€” TODO(REVERSE): body not read, opaque
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

    /// <summary>
    /// Ghidra: <c>RaiseQuitException()</c> — l'exception logicielle par laquelle
    /// le jeu abandonne la boucle en cours pour rejoindre sa sortie. Trois sites la lèvent :
    /// « Quitter le jeu » dans la page d'options, et les deux pompes de messages du lecteur de
    /// vidéo quand elles voient <c>WM_QUIT</c>.
    /// <para>
    /// Dans l'original, un gestionnaire SEH la rattrape en haut de pile et enchaîne sur la séquence
    /// d'arrêt. La lever telle quelle ici ne fait qu'abattre le processus : rien côté managé ne la
    /// rattrape, et l'arrêt propre ne s'exécute pas. On la représente donc par une exception
    /// managée, que <see cref="RunApplication"/> rattrape à l'endroit exact où l'original reprend
    /// la main — devant le <c>finally</c> qui porte déjà les cinq étapes d'arrêt.
    /// </para>
    /// </summary>
    private sealed class BlazeQuitException : Exception
    {
        internal BlazeQuitException() : base("Ghidra: RaiseException(0xE0000001) — sortie du jeu") { }
    }

    /// <summary>Ghidra: RaiseQuitException().</summary>
    private static void RaiseQuitException() => throw new BlazeQuitException();
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
    //   - Blaze.exe agrees â€” DrawFlatPrimitive builds its tag as `*prim = *otEntry | 0x2000000`,
    //     which is only correct while the address's top byte is zero.
    // So every ordering table and primitive buffer must live below 0x01000000.
    //
    // In 1998 that was automatic: Blaze.exe imports winsys3d.dll **statically** (the name sits in
    // its import table at file offset 0x1703AC, the only one of the five DLLs so imported), the
    // loader mapped it at its preferred base 0x00A00000, and winsys3d.c line 568 set
    // `MainMem = &DAT_00b6dbd8` = 0x00B6DBD8. On modern Windows the DLL is relocated out of low
    // memory â€” measured at 0x09â€¦ on this machine, because OpenForesia.exe's own ASLR base sits
    // inside winsys3d's preferred range â€” and DrawOTag faults on the very first frame.
    //
    // DEVIATION (environmental, see REVERSE_STATUS.md): the arena is allocated here, in low
    // memory, and its address written into the `MainMem` pointer winsys3d exports. The DLL file is
    // not modified and no Windows setting is changed. This is sound because MainMem is a genuine
    // indirection â€” it occurs exactly three times in 27703 lines of winsys3d.c:
    //     line  568  MainMem = &DAT_00b6dbd8;                     the sole initialisation, in entry()
    //     line 5655  DAT_00dedbd8 = iVar1 + 0x240000 + MainMem;   DrawOTag, re-derived every frame
    //     line 5843  DAT_00dedbd8 = MainMem + 0x200000;           Ordinal_789's device init, once
    // and DAT_00b6dbd8 itself is referenced only at line 568. Nothing between MainMem + 0x0 and
    // MainMem + 0x280000 is addressed by a fixed address â€” the globals resume exactly at
    // MainMem + 0x280000 â€” which is what makes the arena relocatable at all.
    // =====================================================================

    // Ghidra: winsys3d.c line 568 â€” MainMem points 0xDBD8 into the 64KB-aligned region that holds
    // DAT_00b6dbd8, so allocating at 0x00B60000 reproduces MainMem == 0x00B6DBD8 exactly.
    private const uint MainMemOffsetInArena = 0xDBD8;
    private const uint ArenaPreferredBase = 0x00B60000;
    private const uint ArenaOriginalMainMem = ArenaPreferredBase + MainMemOffsetInArena; // 0x00B6DBD8

    // Ghidra: winsys3d.c â€” the arena runs from MainMem to MainMem + 0x280000, where the DLL's
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
    /// derives a pointer from the arena â€” in practice before <c>Ordinal_789(0, 0)</c>, whose
    /// body (winsys3d.c line 5843) caches <c>MainMem + 0x200000</c> once and never re-derives it.
    /// </summary>
    /// <summary>
    /// Claims the low arena at the earliest point managed code runs. Called from a module
    /// initializer, which executes before <c>Main</c> and before any other static constructor â€”
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
            // meant competing with a runtime that had had seconds to grow into the same window â€”
            // measured: the arena succeeded and the descriptor then failed.
            _ = s_MenuContext_0056dd58;

            // Les descripteurs de la sélection des personnages relèvent de la même contrainte :
            // chacun porte son ordering table à +0xa4, que ClearOTagR masque à 0xffffff.
            _ = s_UiDescriptors_00560ff0;
            _ = s_StatusDescriptor_00560728;
            _ = s_SelectSlots_00573518;
            return true;
        }
        catch (InvalidOperationException) { return false; }
    }

    private static void EnsureDisplayListArena()
    {
        if (s_DisplayListArena != null) return;

        // Reserve BEFORE loading winsys3d.dll. The DLL is an 8.3 MB image and, once relocated out
        // of its preferred base, is itself the strongest competitor for the same low window â€”
        // loading it first made this placement fail roughly one run in four.

        // 1. The original layout first: 0xDBD8 of lead-in so MainMem lands on 0x00B6DBD8 exactly
        //    and every address in winsys3d.c reads straight against the running process.
        nuint preferredSize = (ArenaAllocationSize + 0xFFFu) & ~0xFFFu;
        byte* arena = (byte*)VirtualAlloc((void*)ArenaPreferredBase, preferredSize,
                                          MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
        byte* mainMem = arena != null ? arena + MainMemOffsetInArena : null;

        // 2. Otherwise only the arena proper is needed â€” the 0xDBD8 lead-in exists solely to hit
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
                    $"block of 0x{size:X} bytes was available below 0x{maxBase:X8} â€” the largest free " +
                    $"region there was 0x{largestFreeSeen:X} bytes. Without it every frame would fault " +
                    $"inside DrawOTag with no explanation â€” see REVERSE_STATUS.md, arena deviation.");
            }
        }

        s_DisplayListArena = arena;

        // Only now bring the library in, and point its exported pointer at the arena we secured.
        EnsureWinsys3dLoaded();
        if (s_MainMemExref == null) return;   // the DLL itself is unavailable; callers handle null
        *s_MainMemExref = mainMem;

        long delta = (long)(nint)mainMem - ArenaOriginalMainMem;
        Console.Error.WriteLine(delta == 0
            ? $"winsys3d arena at MainMem=0x{(nint)mainMem:X8} â€” original 1998 layout, addresses " +
              "match winsys3d.c one for one"
            : $"winsys3d arena at MainMem=0x{(nint)mainMem:X8} â€” 0x{ArenaOriginalMainMem:X8} was " +
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

    // Ghidra: MainMem_exref â€” Blaze.exe imports this pointer as DATA from winsys3d.dll (mirrors
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
    // Le banc de registres GTE de winsys3d.dll
    //
    // La DLL n'expose pas seulement des fonctions : les **registres du GTE émulé** — le
    // coprocesseur géométrique de la PlayStation — sortent en exports de **données**, ordinaux
    // 799 à 821. Blaze.exe fait donc sa géométrie en écrivant et relisant ces adresses
    // directement, sans passer par un appel. C'est pour ça que Ghidra montre des
    // `Ordinal_814_exref` partout dans le code de rendu au lieu d'appels reconnaissables.
    //
    // Les adresses viennent de la table d'exports de winsys3d.dll (base 0x00a00000) :
    //
    //   801 -> 0x011f0e98   MAC1..MAC3, les accumulateurs 32 bits
    //   802 -> 0x011f0c80   le vecteur d'entrée (SVECTOR)
    //   805 -> 0x011f0ea8   SXY0 |
    //   806 -> 0x011f0eac   SXY1 |  les trois sommets projetés à l'écran
    //   807 -> 0x011f0eb0   SXY2 |
    //   808 -> 0x011cb418   MAC0, où NCLIP dépose son produit vectoriel
    //   813 -> 0x011d0848   IR1..IR3, les MAC saturés sur 16 bits
    //   814 -> 0x011d0870   la MATRIX courante : short m[3][3] puis long t[3], 0x20 octets
    //   816 -> 0x011d0828   le bloc de 8 dwords que l'ordinal 368 écrit
    //   819 -> 0x011cb410 | le décalage de projection posé par SetGeomOffset (ordinal 378)
    //   820 -> 0x011cb414 |
    //   821 -> 0x011d0898   la table de réciproques que SetGeomScreen (ordinal 379) reconstruit
    //
    // Recoupements qui confirment la lecture : 819/820 sont exactement les deux cibles du corps
    // de l'ordinal 378 dans winsys3d.c, 821 celle de l'ordinal 379, et 816 celle de l'ordinal
    // 368 — trois identifications indépendantes qui tombent juste.
    //
    // TODO(REVERSE): 799, 800, 803, 804, 809, 810, 811, 812, 815, 817, 818 sont exportés au même
    // endroit mais leur rôle n'est pas établi. FUN_0051d1f0 lit 805..811, 815, 817, 819..821.
    // =====================================================================

    private static byte* s_GteVector802, s_GteVector803, s_GteMac801, s_GteSxy805, s_GteSxy806, s_GteSxy807;
    private static byte* s_GteMac0808, s_GteIr813, s_GteMatrix814;
    private static byte* s_GteSxy809, s_GteSz810, s_GteSz811, s_GteLightMatrix815;
    private static byte* s_GteOfx819, s_GteOfy820, s_GteRecip821, s_GteLightColour817;
    private static bool s_GteResolved;

    private static void EnsureGteRegisters()
    {
        if (s_GteResolved) return;

        IntPtr module = LoadLibraryA(ToAnsiZ("winsys3d.dll"));
        if (module == IntPtr.Zero)
        {
            throw new NotImplementedException(
                "EnsureGteRegisters: winsys3d.dll could not be loaded, so the emulated GTE " +
                "register file (data exports 801..814) cannot be resolved.");
        }

        // GetProcAddress accepte un ordinal à la place d'un nom quand le pointeur tient dans son
        // mot bas — la convention MAKEINTRESOURCE. Ces exports n'ont pas de nom.
        s_GteMac801 = (byte*)GetProcAddress(module, (byte*)801);
        s_GteVector802 = (byte*)GetProcAddress(module, (byte*)802);
        s_GteVector803 = (byte*)GetProcAddress(module, (byte*)803);   // le second vecteur d'entrée, celui que FUN_0051f650 utilise
        s_GteSxy805 = (byte*)GetProcAddress(module, (byte*)805);
        s_GteSxy806 = (byte*)GetProcAddress(module, (byte*)806);
        s_GteSxy807 = (byte*)GetProcAddress(module, (byte*)807);
        s_GteMac0808 = (byte*)GetProcAddress(module, (byte*)808);
        s_GteIr813 = (byte*)GetProcAddress(module, (byte*)813);
        s_GteMatrix814 = (byte*)GetProcAddress(module, (byte*)814);

        // Les registres du chemin de projection, utilisés par le cull par partie de
        // FUN_0051d1f0. 809 reçoit le (x | y<<16) projeté, 811 la profondeur, 810 sa version
        // divisée par 4 qui sert d'emplacement d'OT, 815 la matrice de normales, 819/820 le
        // décalage écran posé par SetGeomOffset et 821 la table de réciproques de SetGeomScreen.
        s_GteSxy809 = (byte*)GetProcAddress(module, (byte*)809);
        s_GteSz810 = (byte*)GetProcAddress(module, (byte*)810);
        s_GteSz811 = (byte*)GetProcAddress(module, (byte*)811);
        s_GteLightMatrix815 = (byte*)GetProcAddress(module, (byte*)815);
        s_GteOfx819 = (byte*)GetProcAddress(module, (byte*)819);
        s_GteOfy820 = (byte*)GetProcAddress(module, (byte*)820);
        s_GteRecip821 = (byte*)GetProcAddress(module, (byte*)821);

        // Ghidra: EXT_00b6d198, la couleur que la passe d'éclairage (ordinal 793) consomme.
        // Identifié par le corps de l'ordinal 793 lui-même, qui la passe à FUN_00a24000.
        s_GteLightColour817 = (byte*)GetProcAddress(module, (byte*)817);

        if (s_GteMatrix814 == null || s_GteMac0808 == null)
        {
            throw new NotImplementedException(
                "EnsureGteRegisters: winsys3d.dll resolved but its GTE data exports did not — " +
                "ordinals 808/814 came back null.");
        }

        s_GteResolved = true;
    }

    /// <summary>
    /// Ghidra: FUN_0051c300 @ 0x0051c300 — le <c>RotMatrix</c> du SDK PlayStation : trois angles
    /// vers une matrice de rotation 3×3 de shorts.
    ///
    /// <para><paramref name="angles"/> pointe trois shorts (X, Y, Z) et <paramref name="matrix"/>
    /// reçoit neuf shorts. Chaque angle indexe <see cref="s_LookupTable00600920"/>, la table de
    /// 0x1000 entrées bâtie au démarrage, qui empaquette <c>rsin</c> dans le mot bas et
    /// <c>rcos</c> dans le mot haut — d'où les deux extractions par entrée.</para>
    ///
    /// <para>L'original indexe par <c>(angle &lt;&lt; 2) &amp; 0x3ffc</c>, soit une indexation en
    /// octets déjà multipliée par 4 : c'est le même <c>&amp; 0xfff</c> sur l'indice, écrit
    /// autrement. Transcrit ici en indices d'entrée.</para>
    /// </summary>
    private static void RotMatrix(short* angles, short* matrix)
    {
        uint packedX = s_LookupTable00600920[angles[0] & 0xfff];
        uint packedY = s_LookupTable00600920[angles[1] & 0xfff];
        uint packedZ = s_LookupTable00600920[angles[2] & 0xfff];

        int sinX = (short)packedX;
        int cosX = (short)(packedX >> 0x10);
        int sinY = (short)packedY;
        int cosY = (short)(packedY >> 0x10);
        int sinZ = (short)packedZ;
        int cosZ = (short)(packedZ >> 0x10);

        matrix[0] = (short)(cosY * cosZ >> 0xc);
        matrix[1] = (short)(-(sinZ * cosY) >> 0xc);
        matrix[2] = (short)sinY;

        int sinXsinY = sinX * sinY;
        matrix[3] = (short)(sinZ * cosX + (sinXsinY >> 0xc) * cosZ >> 0xc);
        matrix[4] = (short)((-sinXsinY >> 0xc) * sinZ + cosX * cosZ >> 0xc);
        matrix[5] = (short)(-(sinX * cosY) >> 0xc);

        int cosXsinY = sinY * cosX;
        matrix[6] = (short)(sinX * sinZ + (-cosXsinY >> 0xc) * cosZ >> 0xc);
        matrix[7] = (short)(sinX * cosZ + (cosXsinY >> 0xc) * sinZ >> 0xc);
        matrix[8] = (short)(cosX * cosY >> 0xc);
    }

    /// <summary>
    /// Ghidra: FUN_0051c280 @ 0x0051c280 — le <c>NormalClip</c> du SDK : le produit vectoriel 2D
    /// des trois sommets projetés (SXY0/1/2, ordinaux 805/806/807), déposé dans MAC0 (ordinal 808)
    /// et rendu. Son signe donne l'orientation de la face, d'où l'élimination des faces arrière.
    ///
    /// <para>Les six termes ont été vérifiés un à un contre la formule du SDK
    /// (<c>SX0*SY1 + SX1*SY2 + SX2*SY0 - SX0*SY2 - SX1*SY0 - SX2*SY1</c>) : Ghidra les émet dans
    /// un ordre différent et avec un regroupement de soustractions qui masque la symétrie, mais
    /// l'expression est la même.</para>
    /// </summary>
    private static int NormalClip()
    {
        EnsureGteRegisters();

        int x0 = *(short*)s_GteSxy805, y0 = *(short*)(s_GteSxy805 + 2);
        int x1 = *(short*)s_GteSxy806, y1 = *(short*)(s_GteSxy806 + 2);
        int x2 = *(short*)s_GteSxy807, y2 = *(short*)(s_GteSxy807 + 2);

        int result = x0 * y1 + x1 * y2 + x2 * y0
                   - x0 * y2 - x1 * y0 - x2 * y1;

        *(int*)s_GteMac0808 = result;
        return result;
    }

    /// <summary>Ghidra: FUN_0051c2f0 @ 0x0051c2f0 — relit MAC0 sans le recalculer.</summary>
    private static int ReadMac0()
    {
        EnsureGteRegisters();
        return *(int*)s_GteMac0808;
    }

    /// <summary>
    /// Ghidra: FUN_005327b0 @ 0x005327b0 — un <c>ScaleMatrix</c> fait à la main : chaque colonne de
    /// la matrice est mise à l'échelle par une composante du vecteur.
    ///
    /// <para>C'est la même opération que l'ordinal 794 de winsys3d, à deux détails près qui font
    /// qu'on ne peut pas substituer l'un à l'autre : celui-ci passe par les registres du GTE (donc
    /// il sature en 16 bits à chaque étape via IR) et il lit le facteur **non signé**
    /// (<c>(uint)(ushort)</c>), là où l'ordinal 794 le lit signé. Pour des facteurs positifs les
    /// deux coïncident.</para>
    ///
    /// <para>Il identifie au passage l'ordinal 812 (<c>0x011f0c70</c>) comme le registre de
    /// facteur d'échelle du GTE émulé.</para>
    /// </summary>
    private static void ScaleMatrixSaturating(short* matrix, ushort* scale)
    {
        EnsureGteRegisters();

        short* ir = (short*)s_GteIr813;
        int* mac = (int*)s_GteMac801;

        for (int column = 0; column < 3; column++)
        {
            uint factor = scale[column];

            ir[0] = matrix[column];
            ir[1] = matrix[column + 3];
            ir[2] = matrix[column + 6];

            mac[0] = (int)(factor * ir[0]) >> 0xc; ir[0] = (short)mac[0];
            mac[1] = (int)(ir[1] * factor) >> 0xc; ir[1] = (short)mac[1];
            mac[2] = (int)(ir[2] * factor) >> 0xc; ir[2] = (short)mac[2];

            matrix[column] = ir[0];
            matrix[column + 3] = ir[1];
            matrix[column + 6] = ir[2];
        }
    }

    // =====================================================================
    // FUN_0051d1f0 — le rendu de modèle animé
    //
    // La plus grosse fonction reconstruite à ce jour. C'est elle qui dessine un personnage : elle
    // compose la pose de caméra avec la pose de l'entité, déroule le squelette de l'animation en
    // matrices d'os, transforme et éclaire les sommets par lot côté DLL, puis émet les primitives
    // dans la liste d'affichage.
    //
    // Deux régimes complets, commandés par le bit 0x400 du champ +0x3c du modèle :
    //   dégagé — transformation perspective (ordinal 418, qui produit une profondeur par sommet),
    //            puis **tri en profondeur par polygone** : l'emplacement d'OT est le maximum des
    //            quatre profondeurs, borné par `slot - 0x50 < 0xec0`.
    //   posé   — transformation sans profondeur (ordinal 795) et **un emplacement fixe** pour tout
    //            le modèle. C'est le chemin des géométries plates ou en panneau.
    //
    // C'est ce drapeau qui donne à l'original son air d'avoir quatre boucles d'émission presque
    // identiques : il en a deux, écrites deux fois.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0051d1f0 @ 0x0051d1f0.
    ///
    /// <para>Les sept paramètres, d'après les deux sites d'appel (<c>FUN_00506880</c> et
    /// <c>FUN_00507010</c>) : la base de l'OT, le curseur de primitives, l'entrée d'animation,
    /// l'entrée de la table de modèles, le bloc de transformation de l'entité, l'indice de frame
    /// et le mot d'attributs supplémentaire. Rend le curseur avancé.</para>
    /// </summary>
    private static uint* DrawAnimatedModel(
        uint* otBase, uint* prim, byte* animation, int* modelBundle,
        int* transform, int frameIndex, uint extra)
    {
        EnsureGteRegisters();

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "DrawAnimatedModel: winsys3d.dll's MainMem export resolves to null, so the bone " +
                "matrix arena at MainMem + 0x1ba000 cannot be placed.");
        }

        short* matrix814 = (short*)s_GteMatrix814;
        short* vector802 = (short*)s_GteVector802;
        int* mac801 = (int*)s_GteMac801;
        short* ir813 = (short*)s_GteIr813;

        // Ghidra: la matrice de caméra entre dans le registre MATRIX du GTE — les neuf shorts de
        // rotation puis les trois translations, d'un bloc.
        for (int i = 0; i < 9; i++) matrix814[i] = ((short*)s_CameraMatrix_006008e0)[i];
        *(int*)(s_GteMatrix814 + 0x14) = CameraTranslation[0];
        *(int*)(s_GteMatrix814 + 0x18) = CameraTranslation[1];
        *(int*)(s_GteMatrix814 + 0x1c) = CameraTranslation[2];

        // Ghidra: la position de l'entité, décalée par DAT_006008d0..d8 et ramenée en unités de
        // 1/0x1000. Noter que Y est **soustrait** — l'axe est inversé par rapport aux deux autres.
        vector802[0] = (short)((transform[0] + s_Unknown_006008d0) >> 0xc);
        vector802[1] = (short)((s_Unknown_006008d4 - transform[1]) >> 0xc);
        vector802[2] = (short)((transform[2] + s_Unknown_006008d8) >> 0xc);

        // Ghidra: le produit matrice-vecteur écrit à la main, accumulateurs 32 bits (MAC) puis
        // versions saturées 16 bits (IR) — exactement ce que ferait RotTrans sur la console.
        mac801[0] = (vector802[1] * matrix814[1] + vector802[2] * matrix814[2]
                     + matrix814[0] * vector802[0] >> 0xc) + *(int*)(s_GteMatrix814 + 0x14);
        ir813[0] = (short)mac801[0];
        mac801[1] = (vector802[1] * matrix814[4] + vector802[2] * matrix814[5]
                     + matrix814[3] * vector802[0] >> 0xc) + *(int*)(s_GteMatrix814 + 0x18);
        ir813[1] = (short)mac801[1];
        mac801[2] = (vector802[1] * matrix814[7] + vector802[2] * matrix814[8]
                     + matrix814[6] * vector802[0] >> 0xc) + *(int*)(s_GteMatrix814 + 0x1c);
        ir813[2] = (short)mac801[2];

        // Ghidra: local_3c / sStack_3a / sStack_38 — l'orientation de l'entité, décalée par la pose
        // de caméra DAT_00604928. sStack_38 sert aussi d'emplacement d'OT dans le régime « fixe ».
        short* entityAngles = stackalloc short[3];
        entityAngles[0] = (short)((short)transform[5] + *(short*)(s_CameraVector_00604928 + 0));
        entityAngles[1] = (short)(*(short*)((byte*)transform + 0x16) + *(short*)(s_CameraVector_00604928 + 2));
        entityAngles[2] = (short)((short)transform[6] + *(short*)(s_CameraVector_00604928 + 4));

        // Ghidra: local_bc / local_b8 / local_b4 — la position transformée, mise de côté.
        int translatedX = mac801[0], translatedY = mac801[1], translatedZ = mac801[2];

        // Ghidra: local_d0 — la matrice de l'entité : rotation depuis ses angles, puis mise à
        // l'échelle par transform[3..5] (les trois ushorts à +0xc).
        byte* entityMatrix = stackalloc byte[0x20];
        NativeMemory.Clear(entityMatrix, 0x20);
        RotMatrix(entityAngles, (short*)entityMatrix);
        ScaleMatrixSaturating((short*)entityMatrix, (ushort*)(transform + 3));

        *(int*)(entityMatrix + 0x14) = translatedX;
        *(int*)(entityMatrix + 0x18) = translatedY;
        *(int*)(entityMatrix + 0x1c) = translatedZ;

        // Ghidra: local_68.. — la rotation de scène, celle que le pilotage libre du sous-état 0
        // fait tourner.
        short* sceneAngles = stackalloc short[3];
        sceneAngles[0] = (short)s_ScenePitch_005e9308;
        sceneAngles[1] = (short)s_SceneYaw_005e930a;
        sceneAngles[2] = (short)s_SceneRoll_005e930c;
        short* sceneMatrix = stackalloc short[9];
        RotMatrix(sceneAngles, sceneMatrix);

        // === Les constantes de page de texture, calculées une fois ===
        //
        // Quirk de l'original, transcrit tel quel : elles sont dérivées du **premier** modèle du
        // paquet, alors que la boucle de parties plus bas relit les attributs de chaque partie.
        // Les parties suivantes réutilisent donc la page de la première.
        byte* firstModel = (byte*)(nint)modelBundle[0];
        uint modelAttributes = *(uint*)(firstModel + 0x3c);

        if (s_Unknown_00564a48 != 0 && (modelAttributes & 0x200) != 0)
        {
            if (s_Unknown_00564a4c == 0) return prim;
            modelAttributes &= 0xfffffdff;
        }

        ushort* skipCounters = (ushort*)(firstModel + 0x10);
        int skipped = skipCounters[0] + skipCounters[1] + skipCounters[2] + skipCounters[3];
        byte* firstRecords = *(byte**)(firstModel + 0x28);

        uint clut = *(uint*)(s_EntityDecodeTable_005e8f00 + ((modelAttributes >> 0x10) & 0xff) * 4);

        uint tpageWord = ((*(ushort*)(firstRecords + skipped * 0x10 + 8) & 0xfffff800u)
                          | ((((modelAttributes & 0xff) | 0x1c0) << 5))
                          | clut) << 0x10;

        uint uv = (clut >> 0x10)
                + (uint)(((short)extra >> 2) & unchecked((int)0xffffffc0))
                + (extra & 0xff);
        if (uv > 0x7ec0) uv = 0x7ec0;
        uint uvWord = uv << 0x10;

        // === La boucle d'os : le squelette de la frame devient des matrices monde ===
        //
        // Chaque os prend sa pose dans la frame courante, la tourne, la met à l'échelle (uniforme)
        // et la compose avec celle de son parent. Le résultat s'empile dans MainMem + 0x1ba000,
        // un slot de 0x20 octets par os — que l'original adresse par `(index + 0xdd00) * 0x20`,
        // ce qui est la même chose.
        int boneCount = *(ushort*)(animation + 0x12);
        byte* poseBase = *(byte**)(animation + 0x20);
        ushort* boneIndices = *(ushort**)(animation + 0x24);
        ushort* poseIndices = (ushort*)(*(byte**)(animation + 0x28) + frameIndex * boneCount * 2);

        byte* boneSlot = mainMem + 0x1ba000;
        byte* parentMatrix = entityMatrix;
        byte* boneMatrix = stackalloc byte[0x20];
        ushort* uniformScale = stackalloc ushort[3];

        // local_50 dans l'original : là où le curseur s'arrête. Ce n'est pas une lecture hors
        // bornes — la table d'animation +0x24 est en deux parties, les parents des os suivis des
        // indices d'os par partie, et la boucle laisse le curseur pile sur la seconde.
        ushort* partBones = boneIndices;

        if (boneCount != 0)
        {
            for (int remaining = boneCount; remaining != 0; remaining--)
            {
                byte* pose = poseBase + *poseIndices * 0x10;

                NativeMemory.Clear(boneMatrix, 0x20);
                RotMatrix((short*)pose, (short*)boneMatrix);

                // Ghidra: local_48 / local_46 / la moitié basse de local_44 reçoivent toutes le
                // même short — l'échelle est uniforme, pas un vecteur.
                ushort scale = *(ushort*)(pose + 0xe);
                uniformScale[0] = scale;
                uniformScale[1] = scale;
                uniformScale[2] = scale;
                Ordinal_794(boneMatrix, uniformScale);

                *(int*)(boneMatrix + 0x14) = *(short*)(pose + 8);
                *(int*)(boneMatrix + 0x18) = *(short*)(pose + 10);
                *(int*)(boneMatrix + 0x1c) = *(short*)(pose + 0xc);

                Ordinal_360(parentMatrix, boneMatrix, boneSlot);

                boneIndices++;
                poseIndices++;
                boneSlot += 0x20;
                parentMatrix = mainMem + (*boneIndices + 0xdd00) * 0x20;
            }

            partBones = boneIndices;
        }

        // === La boucle de parties ===
        int transformFlags = *(short*)((byte*)transform + 0x1a);
        int partCount = *(ushort*)(animation + 0x14);
        if (partCount == 0) return prim;

        bool cullParts = (transformFlags & 1) != 0;
        bool skipLighting = (transformFlags & 4) != 0;

        short* composed = stackalloc short[9];
        short otSlotFixed = entityAngles[2];   // sStack_38, réécrit par le cull quand il tourne

        for (; partCount != 0; partCount--, modelBundle++, partBones++)
        {
            short* partMatrix = (short*)(mainMem + (*partBones + 0xdd00) * 0x20);

            // Ghidra: deux chemins sur MMXInstalled_exref — neuf produits scalaires, ou trois
            // triplets de `pmaddwd` sur les mêmes opérandes.
            //
            // DÉVIATION ASSUMÉE : seul le chemin scalaire est transcrit. Le branchement de
            // l'original est une optimisation sur capacité CPU, donc les deux chemins doivent
            // calculer la même chose — mais cette équivalence n'a **pas** été vérifiée opérande
            // par opérande. TODO(REVERSE): la vérifier, ou transcrire le chemin MMX.
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    composed[row * 3 + col] =
                        (short)((sceneMatrix[row * 3 + 0] * partMatrix[0 * 3 + col]
                               + sceneMatrix[row * 3 + 1] * partMatrix[1 * 3 + col]
                               + sceneMatrix[row * 3 + 2] * partMatrix[2 * 3 + col]) >> 0xc);
                }
            }

            // Ghidra: la matrice composée part dans le registre de normales (815), la matrice de
            // l'os — translation comprise — dans le registre de transformation (814).
            //
            // L'original copie 0x20 octets vers 815 depuis local_88, dont la rangée de translation
            // n'est **pas** réécrite par le produit ci-dessus : elle garde ce que la boucle d'os y
            // a laissé au dernier tour. Reproduit tel quel, c'est sans effet — l'éclairage n'a
            // besoin que de la rotation.
            for (int i = 0; i < 9; i++) ((short*)s_GteLightMatrix815)[i] = composed[i];
            *(int*)(s_GteLightMatrix815 + 0x14) = *(int*)(boneMatrix + 0x14);
            *(int*)(s_GteLightMatrix815 + 0x18) = *(int*)(boneMatrix + 0x18);
            *(int*)(s_GteLightMatrix815 + 0x1c) = *(int*)(boneMatrix + 0x1c);

            for (int i = 0; i < 8; i++) ((uint*)s_GteMatrix814)[i] = ((uint*)partMatrix)[i];

            if (cullParts && !PartIsOnScreen(ref otSlotFixed)) continue;

            prim = DrawModelPart(otBase, prim, (byte*)(nint)modelBundle[0], mainMem,
                                 transform, tpageWord, uvWord, skipLighting, otSlotFixed);
        }

        return prim;
    }

    /// <summary>
    /// Ghidra: la branche <c>local_94 != 0</c> de FUN_0051d1f0 — le rejet des parties hors écran.
    ///
    /// <para>Elle projette l'origine de la partie, c'est-à-dire le vecteur nul : tous les produits
    /// par la rotation s'annulent et il ne reste que la translation de la matrice courante, lue en
    /// <c>short</c>. La division perspective passe par la table de réciproques de l'ordinal 821,
    /// indexée par la profondeur, puis le décalage écran des ordinaux 819/820 est ajouté.</para>
    ///
    /// <para>Effet de bord voulu : la profondeur projetée, divisée par quatre, devient
    /// l'emplacement d'OT du régime « fixe ». D'où le <c>ref</c>.</para>
    /// </summary>
    private static bool PartIsOnScreen(ref short otSlot)
    {
        ProjectOriginThroughMatrix();

        short x = (short)*(uint*)s_GteSxy809;
        short y = (short)(*(uint*)s_GteSxy809 >> 0x10);
        short z = (short)*(int*)s_GteSz810;

        otSlot = z;

        return (ushort)(x + 0x18) < 0x170
            && (ushort)(y + 0x18) < 0x120
            && (uint)(z - 0x58) < 0x3a0;
    }

    /// <summary>
    /// Ghidra: le cœur de projection partagé par la branche de cull de FUN_0051d1f0 et par
    /// <see cref="ProjectPoint"/> — les deux l'écrivent en toutes lettres, à l'identique.
    ///
    /// <para>Le vecteur d'entrée est mis à zéro, donc tous les produits par la rotation
    /// s'annulent : seule la translation de la matrice courante survit. Ce que l'original écrit
    /// quand même en entier, et qu'on garde ici sous la forme courte — la version longue ne ferait
    /// qu'ajouter des zéros. La division perspective passe par la table de réciproques de
    /// l'ordinal 821, indexée par la profondeur, puis le décalage écran des ordinaux 819/820.</para>
    /// </summary>
    private static void ProjectOriginThroughMatrix()
    {
        short* vector = (short*)s_GteVector802;
        vector[0] = 0;
        vector[1] = 0;
        vector[2] = 0;

        short depth = *(short*)(s_GteMatrix814 + 0x1c);
        *(int*)s_GteSz811 = depth;

        int reciprocal = *(short*)(s_GteRecip821 + (ushort)depth * 2);
        short screenY = *(short*)(s_GteMatrix814 + 0x18);
        short screenX = *(short*)(s_GteMatrix814 + 0x14);

        *(uint*)s_GteSxy809 = (uint)(
              ((reciprocal * screenY & unchecked((int)0xfffff000)) + *(int*)s_GteOfx819 * 0x1000) * 0x10
            + ((reciprocal * screenX) >> 0xc) + *(int*)s_GteOfy820);

        *(int*)s_GteSz810 = *(int*)s_GteSz811 >> 2;
    }

    /// <summary>
    /// Ghidra: FUN_0051f650 @ 0x0051f650 — projette une position monde et dit si elle est à
    /// l'écran. C'est le filtre que le carrousel du sous-état 0 applique à chacun de ses seize
    /// emplacements : un retour nul écarte l'emplacement.
    ///
    /// <para>Elle charge la matrice de caméra dans le registre du GTE par
    /// <c>SetRotMatrix</c>/<c>SetTransMatrix</c> (ordinaux 366 et 369), transforme la position, en
    /// fait la nouvelle translation de la matrice, puis projette l'origine — le même tour que la
    /// branche de cull de FUN_0051d1f0.</para>
    ///
    /// <para>Les deux paramètres viennent de l'assembleur, Ghidra ayant perdu le cadre de pile et
    /// rendu la sortie en <c>unaff_retaddr</c> : arg1 en <c>[ESP+0x1c]</c> (la position), arg2 en
    /// <c>[ESP+0x20]</c> (le tampon de sortie, écrit en <c>0x0051f8e5</c>).</para>
    /// </summary>
    private static int ProjectPoint(int* position, short* outScreen)
    {
        EnsureGteRegisters();

        Ordinal_366(s_CameraMatrix_006008e0);
        Ordinal_369(s_CameraMatrix_006008e0);

        short* input = (short*)s_GteVector803;
        input[0] = (short)((position[0] + s_Unknown_006008d0) >> 0xc);
        input[1] = (short)((s_Unknown_006008d4 - position[1]) >> 0xc);
        input[2] = (short)((position[2] + s_Unknown_006008d8) >> 0xc);

        short* m = (short*)s_GteMatrix814;
        int* mac = (int*)s_GteMac801;
        short* ir = (short*)s_GteIr813;

        mac[0] = (input[1] * m[1] + input[2] * m[2] + m[0] * input[0] >> 0xc)
               + *(int*)(s_GteMatrix814 + 0x14);
        mac[1] = (input[1] * m[4] + input[2] * m[5] + m[3] * input[0] >> 0xc)
               + *(int*)(s_GteMatrix814 + 0x18);
        mac[2] = (input[1] * m[7] + input[2] * m[8] + m[6] * input[0] >> 0xc)
               + *(int*)(s_GteMatrix814 + 0x1c);

        ir[0] = (short)mac[0];
        ir[1] = (short)mac[1];
        ir[2] = (short)mac[2];

        // La position transformée devient la translation de la matrice — c'est ce qui fait que la
        // projection de l'origine, juste après, projette bien ce point-là.
        *(int*)(s_GteMatrix814 + 0x14) = mac[0];
        *(int*)(s_GteMatrix814 + 0x18) = mac[1];
        *(int*)(s_GteMatrix814 + 0x1c) = mac[2];

        ProjectOriginThroughMatrix();

        *(uint*)outScreen = *(uint*)s_GteSxy809;
        *(uint*)(outScreen + 2) = (uint)*(int*)s_GteSz810;

        return (ushort)(outScreen[0] + 0x18) < 0x170
            && (ushort)(outScreen[1] + 0x18) < 0x120
            && (uint)(outScreen[2] - 0x58) < 0x3a0 ? 1 : 0;
    }

    /// <summary>
    /// Ghidra: LAB_0051db61 — le corps d'une partie : transformation et éclairage des sommets côté
    /// DLL, puis émission des primitives.
    ///
    /// <para>Le bit <c>0x400</c> des attributs commande les deux régimes. Dégagé : transformation
    /// perspective par l'ordinal 418, qui produit une profondeur par sommet, et **tri en profondeur
    /// par polygone**. Posé : ordinal 795, sans profondeur, et **un emplacement d'OT fixe** pour
    /// toute la partie.</para>
    /// </summary>
    private static uint* DrawModelPart(
        uint* otBase, uint* prim, byte* model, byte* mainMem, int* transform,
        uint tpageWord, uint uvWord, bool skipLighting, short fixedOtSlot)
    {
        uint attributes = *(uint*)(model + 0x3c);

        if (s_Unknown_00564a48 != 0 && (attributes & 0x2000000) != 0)
        {
            if (s_Unknown_00564a4c == 0) return prim;
            attributes &= 0xfdffffff;
        }

        uint vertexCount = *(uint*)(model + 8) >> 0x10;
        bool depthSorted = (attributes & 0x400) == 0;

        // Ghidra: la transformation par lot. Le chemin avec profondeur remplit aussi le tableau
        // à MainMem + 0x1b4000, que le tri par polygone relit.
        if (depthSorted)
        {
            Ordinal_418(model + 0x40, mainMem + 0x1b0000, mainMem + 0x1b4000, mainMem,
                        *(uint*)(model + 8) & 0xffff);
        }
        else
        {
            // Même compteur que la branche avec profondeur : les deux ordinaux consomment le
            // champ +8 du modèle sur ses seize bits bas.
            Ordinal_795(model + 0x40, mainMem + 0x1b0000, *(uint*)(model + 8) & 0xffff);
        }

        // Ghidra: l'éclairage — soit la passe de la DLL, soit une couleur plate recopiée trois fois
        // par triangle quand le bit 4 du bloc de transformation est posé.
        uint* colours = (uint*)(mainMem + 0x1b8000);
        uint flatColour = (uint)transform[7] & 0x3ffffff ^ (attributes & 0x300) << 0x10;

        if (skipLighting)
        {
            // Curseur distinct : `colours` sert encore à l'émission plus bas. L'original a le
            // même découplage (puVar15 est réassigné, pas la base).
            uint* fill = colours;
            for (uint i = vertexCount * 3; i != 0; i--) *fill++ = flatColour;
        }
        else
        {
            *(uint*)s_GteLightColour817 = flatColour;
            Ordinal_793(*(void**)(model + 0x24), colours, vertexCount);
        }

        byte* records = *(byte**)(model + 0x28);
        ushort* counters = (ushort*)(model + 0x10);
        int skipped = counters[0] + counters[1] + counters[2] + counters[3];
        uint* record = (uint*)(records + skipped * 0x10);

        uint* screen = (uint*)(mainMem + 0x1b0000);
        int* depths = (int*)(mainMem + 0x1b4000);

        // Ghidra: les deux seaux, comptés en +0x1a puis +0x1e. Le premier émet des quads texturés
        // de 10 mots, le second des quads texturés dégradés de 13.
        prim = EmitModelQuads(otBase, prim, ref record, *(ushort*)(model + 0x1a),
                              screen, depths, colours, tpageWord, uvWord,
                              depthSorted, fixedOtSlot, gouraud: false);

        prim = EmitModelQuads(otBase, prim, ref record, *(ushort*)(model + 0x1e),
                              screen, depths, colours, tpageWord, uvWord,
                              depthSorted, fixedOtSlot, gouraud: true);

        return prim;
    }

    /// <summary>
    /// Ghidra: les quatre boucles d'émission de FUN_0051d1f0, qui n'en sont que deux écrites deux
    /// fois — le régime de tri ne change que le calcul de l'emplacement d'OT.
    ///
    /// <para>Chaque enregistrement de 16 octets porte quatre indices de sommet empaquetés dans son
    /// premier mot. Deux garde-fous bornent le travail : un quad n'est considéré que si l'un de ses
    /// quatre X écran tombe sous <c>0x140</c>, et le budget global <c>DAT_005e9818</c> décroît à
    /// chaque primitive émise et arrête la boucle à zéro.</para>
    ///
    /// <para>Le rejet des faces arrière est **inliné** dans l'original — la même expression à six
    /// termes que <see cref="NormalClip"/>, recopiée à la main. Le premier test porte sur les trois
    /// premiers sommets ; s'il passe, le quatrième remplace SXY2 et le test est refait, cette fois
    /// par un vrai appel.</para>
    /// </summary>
    private static uint* EmitModelQuads(
        uint* otBase, uint* prim, ref uint* record, int count,
        uint* screen, int* depths, uint* colours,
        uint tpageWord, uint uvWord, bool depthSorted, short fixedOtSlot, bool gouraud)
    {
        int stride = gouraud ? 0xd : 10;

        for (; count != 0 && s_Unknown_005e9818 != 0; count--, record += 4)
        {
            uint packed = record[0];
            int i0 = (int)(packed & 0xff);
            int i1 = (int)(packed >> 8 & 0xff);
            int i2 = (int)(packed >> 0x10 & 0xff);
            int i3 = (int)(packed >> 0x18);

            uint v0 = screen[i0], v1 = screen[i1], v2 = screen[i2], v3 = screen[i3];

            *(uint*)s_GteSxy805 = v0;
            *(uint*)s_GteSxy806 = v1;
            *(uint*)s_GteSxy807 = v2;

            if ((ushort)v0 >= 0x140 && (ushort)v1 >= 0x140
                && (ushort)v2 >= 0x140 && (ushort)v3 >= 0x140) continue;

            // Ghidra: le produit vectoriel écrit à la main, identique à NormalClip.
            int x0 = *(short*)s_GteSxy805, y0 = *(short*)(s_GteSxy805 + 2);
            int x1 = *(short*)s_GteSxy806, y1 = *(short*)(s_GteSxy806 + 2);
            int x2 = *(short*)s_GteSxy807, y2 = *(short*)(s_GteSxy807 + 2);
            *(int*)s_GteMac0808 = x0 * y1 + x1 * y2 + x2 * y0
                                - x0 * y2 - x1 * y0 - x2 * y1;

            if ((v0 & 0xffff0000) >= 0xf00000 && (v1 & 0xffff0000) >= 0xf00000
                && (v2 & 0xffff0000) >= 0xf00000 && (v3 & 0xffff0000) >= 0xf00000) continue;

            if (ReadMac0() >= 0)
            {
                *(uint*)s_GteSxy807 = v3;
                if (NormalClip() >= 0) continue;
            }

            uint* slot;
            if (depthSorted)
            {
                int zmax = depths[i0];
                if (zmax < depths[i1]) zmax = depths[i1];
                if (zmax < depths[i2]) zmax = depths[i2];
                if (zmax < depths[i3]) zmax = depths[i3];

                // Ghidra rend ce test en arithmétique de pointeur (`puVar20 + -0x50` sur un
                // `uint *`). L'assembleur tranche : `LEA ECX,[EAX + 0xfffffec0]` puis
                // `CMP ECX,0xec0` non signé — c'est bien 0x140 octets, pas 0x50.
                if ((uint)(zmax - 0x140) >= 0xec0) continue;

                slot = (uint*)((byte*)otBase + ((uint)zmax & 0xfffc));
            }
            else
            {
                slot = otBase + fixedOtSlot;
            }

            uint previous = *slot;
            *slot = (uint)(nuint)prim;

            if (!gouraud)
            {
                uint colour = colours[record[1] & 0xff];
                if (s_Unknown_00564a48 != 0) colour &= 0xfdffffff;

                prim[0] = previous | 0x9000000;
                prim[1] = colour | 0x2c000000;
                prim[2] = v1;
                prim[3] = record[2] >> 0x10 | uvWord;
                prim[4] = v0;
                prim[5] = tpageWord | record[2] & 0xffff;
                prim[6] = v2;
                prim[7] = record[3] & 0xffff;
                prim[8] = v3;
                prim[9] = record[3] >> 0x10;
            }
            else
            {
                uint indices = record[1];
                uint c0 = colours[indices & 0xff];
                uint c1 = colours[indices >> 8 & 0xff];
                uint c2 = colours[indices >> 0x10 & 0xff];
                uint c3 = colours[indices >> 0x18];
                if (s_Unknown_00564a48 != 0) c1 &= 0xfdffffff;

                prim[0] = previous | 0xc000000;
                prim[1] = c1 | 0x3c000000;
                prim[2] = v1;
                prim[3] = record[2] >> 0x10 | uvWord;
                prim[4] = c0;
                prim[5] = v0;
                prim[6] = tpageWord | record[2] & 0xffff;
                prim[7] = c2;
                prim[8] = v2;
                prim[9] = record[3] & 0xffff;
                prim[10] = c3;
                prim[11] = v3;
                prim[12] = record[3] >> 0x10;
            }

            prim += stride;
            s_Unknown_005e9818--;
        }

        return prim;
    }


    // =====================================================================
    // CRT support (MSVC 6 / "Visual Studio 1998" runtime startup, confirmed by several
    // functions Ghidra matched to known library symbols, e.g. __amsg_exit, __exit).
    //
    // None of this is Blaze-specific game logic â€” it is the C runtime's process bring-up
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
    /// TODO(REVERSE): same as RunStaticDestructors â€” the initializer table contents are
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
    /// Ghidra: entry @ 0x00547d40 â€” the raw PE entry point, i.e. MSVC's `WinMainCRTStartup`.
    /// Performs C-runtime bring-up (heap, argv/environ, static initializers), extracts the
    /// WinMain-style command line, then hands off to <see cref="RunApplication"/>
    /// (Ghidra: FUN_00513900), which is where Blaze & Blade's own code begins.
    /// </summary>
    public static void entry()
    {
        // DEVIATION, and it must come first: Blaze.exe imports winsys3d.dll statically, so in the
        // original the library was mapped â€” and its arena fixed at 0x00B6DBD8 â€” before this entry
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
    // RunApplication â€” Blaze & Blade's own entry point (this is where "our" game code starts;
    // everything above is C-runtime/CRT startup plumbing).
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00513900 @ 0x00513900 â€” the WinMain-equivalent passed to `entry`. Ghidra only
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
        // Ghidra: s____Blaze_Blade_005631e8 @ 0x005631e8 â€” a 4-byte "X:\\\0" drive-root buffer
        // whose first byte is swapped for each candidate drive letter.
        byte* driveRoot = stackalloc byte[4];
        driveRoot[1] = (byte)':';
        driveRoot[2] = (byte)'\\';
        driveRoot[3] = 0;

        // TODO(REVERSE): DAT_00563280.._0056328c (the CD-check filename template copied into
        // local_2c/local_28/local_24/local_20 in the original) are 16 bytes of initialized data
        // not present in Blaze.c's text â€” only Ghidra's derived symbol names survive. The drive
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
                // wrote into the image. Patch index 0 only â€” do not rebuild the string from parts,
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
                // in the high word â€” see Documentation/DLL/winsys3d.md. Packed sin/cos table,
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
                catch (BlazeQuitException)
                {
                    // Ghidra: l'exception logicielle 0xE0000001 que le jeu lève pour abandonner la
                    // boucle en cours et rejoindre sa sortie — « Quitter le jeu » dans les options,
                    // et WM_QUIT dans les pompes du lecteur de vidéo. Le `finally` ci-dessous est
                    // l'équivalent du gestionnaire SEH de l'original : la séquence d'arrêt tourne
                    // quand même.
                }
                finally
                {
                    ShutdownStage1(); // Ghidra: FUN_00504600 @ 0x00504600
                    SaveOptionsIni(); // Ghidra: thunk_FUN_0050f300 @ 0x0050f520 -> FUN_0050f300
                    ShutdownStage3(); // Ghidra: FUN_00512ff0 @ 0x00512ff0
                    // Ghidra: DestroyWindow(DAT_005db85c) â€” main window handle global not yet reconstructed.
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

    // Ghidra: &PTR_s_Blaze___Blade_00563208 â€” the 5-entry ANSI string-pointer table passed as
    // Setup3DDevice's 3rd argument (see the P/Invoke declaration above for how winsys3d.dll's
    // dialog proc consumes each slot).
    //
    // This previously passed `driveRoot` as a placeholder, which was actively wrong: the DLL would
    // have read a 4-byte "X:\\\0" buffer as an array of five pointers and dereferenced whatever
    // followed it on the stack. Entry [0] is well founded â€” Ghidra's own symbol name for the table
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

    // Ghidra: DAT_00604920 / DAT_00600900 â€” the two raw 8-bit-PCM sample banks, allocated in
    // RunApplication and filled straight from disk. Their role is settled by PlaySoundEffect
    // (FUN_0053cea0), which adds a directory entry's offset to one of these two bases to get the
    // sample's address: the "base" bank holds sound ids 0..0x3b (loaded from "\SND\BASE.SND"
    // alongside the first 0x1e0 bytes of the directory), the "level" bank holds ids 0x3c..0x4c
    // (loaded by FUN_00512xxx alongside the remaining 0x88 bytes of the directory).
    private static void* s_BaseSoundBank;   // 790000 bytes
    private static void* s_LevelSoundBank;  // 260000 bytes

    // Ghidra: s_Please_insert_Blaze___Blade_CD__00563258 / s_Erreur_00563278 â€” symbol names were
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

    /// <summary>Ghidra: FUN_005138d0 @ 0x005138d0 â€” restores the volumes saved above.</summary>
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
    // Ghidra: DAT_005630bc â€” recovered from Legacy/Blaze.exe as ".cfg", not ".ini". The installed
    // game's own options file is BLAZE.cfg, and its [Options]/[D3DDriver]/[Controls] sections match
    // what LoadOptionsIni/LoadKeyBindings already read â€” so those two were correct all along and
    // had simply never located their file.
    private const string IniFileExtension = ".cfg";

    // Ghidra: &DAT_00563080 (Blaze.c:58266) â€” Ghidra gave it no string name, but the literal is
    // present in Legacy/Blaze.exe: "Size". It sits in the .data key block exactly where the call
    // order puts it, between "Small" (0x00563088) and "Resolution" (0x00563074), and the installed
    // game's BLAZE.cfg carries Size=23 alongside Resolution=4 â€” which is precisely
    // s_ResolutionDetailLimit[4]. Three independent confirmations; no longer a TODO.
    private const string SizeIniKey = "Size";

    // Ghidra: DAT_0054d000. TODO(REVERSE): no literal value in Blaze.c (data-only global); this
    // is inferred as 1/64 from cross-referencing winsys3d.c's own default
    // `CreateTranslationTables(1.0f)` call against the ini's default Gamma value of 0x40 (64) â€”
    // 64 * (1/64) == 1.0, matching the DLL's default. See Documentation/DLL/winsys3d.md.
    private const float GammaIniScale = 1.0f / 64.0f;

    // Ghidra: DAT_00589c70 (ini "SoundVol") / DAT_0057925c (ini "CdVol") / DAT_005d9de0 (ini "Gamma").
    // All three default to 0x40 (64) in GetPrivateProfileIntA.
    private static uint s_SoundVolumeIni;
    private static uint s_CdVolumeIni;
    private static uint s_GammaIni;

    // Ghidra: AU_00564a05._51_4_ â€” a 4-byte field at offset 0x33 inside a larger, not-yet-
    // reconstructed blob (AU_00564a05). Ini key "Small", default 0.
    private static uint s_SmallTextureFlag;

    // Ghidra: DAT_005de160, ini key "Size", default 0xb (11). Clamped in LoadOptionsIni against a
    // per-resolution table (DAT_00562e00).
    //
    // Role established 2026-08-12 (the earlier TODO(REVERSE) here is answered): this is the
    // engine's render-size level. `SetShift` (winsys3d.c:4937) hands the 3D device a viewport of
    // `(Size + 9) * 0x20` by `(Size + 9) * 0x18` and selects the vertex-scaling lookup table with
    // the same value. So Size 11 is 640x480 â€” the engine's logical reference space â€” and the
    // shipped `Size=21` is 960x720, which matches the render target measured at runtime. Full
    // derivation in `Documentation/DLL/winsys3d.md`, section "Coordinate scaling".
    private static int s_SizeSetting_005de160 = 0xb;

    /// <summary>Taille de rendu que le rÃ©glage <c>Size</c> implique, en pixels.</summary>
    private static (int Width, int Height) RenderSizeFromSizeSetting()
        => ((s_SizeSetting_005de160 + 9) * 0x20, (s_SizeSetting_005de160 + 9) * 0x18);

    // Ghidra: DAT_00566cc4 (ini "Resolution", default 2) / DAT_00562df8 (ini "Timing", default 1).
    private static uint s_ResolutionIndex;
    private static uint s_TimingMode;

    // Ghidra: &DAT_00564a58, 4 ints â€” per-joystick-slot assignment (-1 = unassigned). Ini keys
    // built from format string "Joy%d" â€” no space (Ghidra: s_Joy_d_00563064; literal read back
    // from Legacy/Blaze.exe at 0x00563064). An earlier reconstruction wrote "Joy %d", which reads
    // keys the real BLAZE.cfg does not contain: the joystick settings silently fell back to their
    // defaults instead of being loaded.
    private static readonly int[] s_JoystickSlot = new int[4];

    // Ghidra: &DAT_00562e00 â€” per-resolution clamp table for s_SizeSetting_005de160.
    // TODO(REVERSE): actual byte content not present in Blaze.c (data-only global); sized as a
    // guess and filled with int.MaxValue as a safe (non-clamping) placeholder rather than
    // inventing real limits.
    private static readonly int[] s_ResolutionDetailLimit = CreateUnknownResolutionLimitTable();

    // Ghidra: DAT_00562e00 â€” recovered from Legacy/Blaze.exe as the dwords
    // { 1, 7, 11, 16, 23, 0 }. These are real per-resolution detail clamps; the table was
    // previously filled with int.MaxValue on the assumption that its content was unrecoverable,
    // which turned the clamp into a silent no-op.
    // Entries 0..4 are confirmed: the installed BLAZE.cfg pairs Resolution=4 with Size=23, and
    // this table's index 4 is 23 â€” the clamp is a no-op there, which is what a settings file
    // written by the game itself must produce.
    // TODO(REVERSE): the trailing 0 is transcribed as read. It is plausibly a terminator rather
    // than a 6th entry meaning "clamp everything to 0"; the bytes that follow (01 02 03 04 05 06
    // 07 08) are visibly a different table, so 0x00562e00 ends here either way. Kept verbatim.
    private static int[] CreateUnknownResolutionLimitTable()
        => [1, 7, 11, 16, 23, 0];

    // Ghidra: &DAT_00564b58, 240 ints (4 groups x 3 categories x 20 actions) â€” key-binding table.
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
    // slot 0x0c with the argument pair (8, &DAT_005db8a0) â€” and DAT_005db8a0..DAT_005db8df is
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

    // Ghidra: DAT_005db8e4 â€” the IDirectSound device object.
    // TODO(REVERSE): nothing in the reconstructed call graph assigns it â€” no DirectSoundCreate
    // call site has been located yet, so it is still null at runtime and every entry point below
    // takes its "no device" early-out. Same category of coverage gap as DAT_005db85c (TODO #6).
    private static void* s_DirectSound;

    // Ghidra: DAT_005db754 â€” a second COM object released just before the IDirectSound device in
    // FUN_00512ff0. TODO(REVERSE): only ever seen through Release (vtable 0x08), so its concrete
    // type isn't pinned down; position in the teardown order suggests the primary/listener buffer.
    private static void* s_DirectSoundPrimary;

    // Ghidra: DAT_005db900 â€” the streaming-music IDirectSoundBuffer created by FUN_005131d0.
    // (The ini key that drives its volume is "CdVol", hence ApplyCdVolume's name, but the buffer
    // itself is the ADPCM music stream, not a CD-audio object â€” FUN_005131d0 fills it from a
    // .WAV file on disk.)
    private static void* s_MusicStreamBuffer;

    // Ghidra: DAT_005db904 â€” the IDirectSoundNotify of the buffer above.
    private static void* s_MusicStreamNotify;

    // Ghidra: &DAT_005db758, 50 IDirectSoundBuffer pointers. The count is confirmed by
    // FUN_00512ea0's loop bound (`while ((int)piVar3 <= 0x5db81f)` starting at 0x005db758 â€”
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
    /// allocated (Ghidra: the `goto LAB_00512edf` path) â€” reproduced exactly.
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

    // Ghidra: TexFilterPossible_exref â€” Blaze.exe imports this `uint` as DATA (not a function)
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
    /// Ghidra: winsys3d.c FUN_00a261e0 â€” the path normalizer every winsys3d file-open call runs its
    /// argument through first. It does two things, both of which matter here:
    /// <list type="bullet">
    /// <item>skips one leading backslash, so a template like <c>"\SND\BASE.SND"</c> is relative</item>
    /// <item><b>truncates at the first <c>';'</c></b>, which is what makes the ISO-9660 version
    /// suffixes in <c>"BLAZE.ALL;1"</c> and <c>"SYSTEM.INI;1"</c> work â€” the DLL opens
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
    /// DSBVOLUME_MIN/DSBVOLUME_MAX. The interface identity â€” an open question when those two
    /// functions were first reconstructed â€” is now confirmed; see the DirectSound banner above.
    /// </summary>
    private static void InvokeSetVolume(void* comObject, int attenuationHundredthDb)
    {
        if (comObject == null) return;
        void** vtable = *(void***)comObject;
        var setVolume = (delegate* unmanaged[Stdcall]<void*, int, int>)vtable[IDirectSoundBuffer_SetVolume];
        setVolume(comObject, attenuationHundredthDb);
    }

    // Ghidra: DAT_005db8ec â€” the current music-stream attenuation, computed by FUN_005137b0 and
    // read back by FUN_005131d0 when it starts a new stream.
    private static int s_MusicAttenuation;

    // Ghidra: DAT_005db8e8 â€” the current sound-effect attenuation, computed by FUN_00513800 and
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
    /// TODO(REVERSE): Ghidra's register attribution is wrong in the tail of this function â€” it
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
    /// Ghidra: FUN_00513110 @ 0x00513110 â€” <c>IDirectSoundBuffer::Lock</c> over the whole buffer
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

    /// <summary>Ghidra: FUN_00513150 @ 0x00513150 â€” <c>IDirectSoundBuffer::Unlock</c>.</summary>
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
    // Everything above is reconstructed. The worker thread itself is not â€” see the TODO(REVERSE)
    // at the CreateThread site in StartMusicStream.
    // =====================================================================

    // Ghidra: DAT_005631d4 â€” nonzero while music is enabled (toggled by FUN_00513860, and read
    // by the options UI at 0x0050f2xx). Recovered from Legacy/Blaze.exe @ 0x005631d4 as **1**:
    // music ships **enabled**. This previously defaulted to 0 on the assumption that the value was
    // unrecoverable, which silently made StartMusicStream take its early-out forever.
    private static uint s_MusicEnabled = 1;

    // Ghidra: DAT_005631d0 â€” nonzero while no stream is running. Note the inverted sense: it is
    // *cleared* at the end of a successful start and *set* at the top of a stop. Left at 0 (its
    // .bss default): a first StartMusicStream always takes the "different track" path, which
    // calls StopMusicStream and so sets the flag before it is tested.
    private static uint s_MusicStreamStopped;

    // Ghidra: DAT_005db8f0 â€” the whole-file scratch buffer. Its size is the literal 0x004c4b40
    // (5,000,000), which Ghidra rendered as `&LAB_004c4b40` because the constant happens to fall
    // inside the .text range; it is used identically as a plain size in the tail-zeroing loop.
    private const uint MusicFileBufferSize = 0x004c4b40;
    private static byte* s_MusicFileBuffer;

    // Ghidra: DAT_005db8f4 / DAT_005db8f8 / DAT_005db8fc â€” the `fmt ` chunk body, the `data`
    // chunk body and the `data` chunk size, all pointing into s_MusicFileBuffer.
    private static ADPCMWAVEFORMAT* s_MusicFormat;
    private static byte* s_MusicData;
    private static int s_MusicDataSize;

    // Ghidra: DAT_005db820 â€” the PCM WAVEFORMATEX describing the *decoded* stream. Kept in native
    // memory because the original is a .bss global whose address is handed to DirectSound.
    private static readonly WAVEFORMATEX* s_MusicOutputFormat = (WAVEFORMATEX*)NativeMemory.AllocZeroed((nuint)sizeof(WAVEFORMATEX));

    // Ghidra: DAT_005db8a0 (DSBPOSITIONNOTIFY[8]) and DAT_005db838 (the same 8 event handles,
    // kept separately so FUN_005135d0 can close them without walking the notify structs).
    private const int MusicNotifyCount = 8;
    private static readonly DSBPOSITIONNOTIFY* s_MusicNotifyPositions = (DSBPOSITIONNOTIFY*)NativeMemory.AllocZeroed(MusicNotifyCount * (nuint)sizeof(DSBPOSITIONNOTIFY));
    private static readonly IntPtr[] s_MusicNotifyEvents = new IntPtr[MusicNotifyCount];

    // Ghidra: DAT_005db858 â€” the "please exit" event signalled by FUN_005135d0.
    private static IntPtr s_MusicStopEvent;

    // Ghidra: DAT_005db880 / DAT_005db8e0 â€” the streaming worker thread and its thread id.
    private static IntPtr s_MusicStreamThread;
    private static uint s_MusicStreamThreadId;

    // Ghidra: DAT_005db860 / DAT_005db750 / DAT_005db89c. The first two are the streaming
    // worker's cursor (source block index reached so far) and its end marker (total number of
    // source blocks); the third is a flag set as playback starts.
    // TODO(REVERSE): all three are only ever *written* in the reconstructed graph â€” their readers
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
    // symbols), but the interface identity is pinned by how the returned object is used â€” see the
    // DirectSound banner above. The value below is the documented IID_IDirectSoundNotify.
    private static readonly Guid IID_IDirectSoundNotify = new Guid("b0210783-89cd-11d0-af08-00a0c925cd16");

    // Ghidra: DAT_0056ac60 (16 shorts), DAT_0056ac80 and DAT_0056ac90 (7 shorts each) â€” read by
    // FUN_00531bb0 and FUN_00531f70 respectively.
    //
    // These are data-only globals, so their bytes are not in Blaze.c either; the values below are
    // the standard Microsoft-ADPCM tables. The identification is structural rather than a guess:
    // FUN_00531f70 only accepts a `fmt ` chunk with wFormatTag == 2 (WAVE_FORMAT_ADPCM),
    // wBitsPerSample == 4 and wNumCoef == 7, and then compares aCoef[0..6].iCoef1/iCoef2 against
    // DAT_0056ac80/DAT_0056ac90 â€” i.e. it verifies the file uses the standard coefficient set so
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
    /// The block layout it walks â€” per channel: 1 predictor index, then a 16-bit iDelta, iSamp1
    /// and iSamp2 â€” plus the nibble-per-sample body with channels interleaved inside each byte,
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
        // Ghidra: `(((destBits & 0xff) >> 3) << (channels >> 1 & 0x1f)) * decodedSamples` â€” bytes
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
                // TODO(REVERSE): the original subtracts unconditionally â€” a file smaller than
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

        // Ghidra: __strcmpi â€” a different track than the one currently in the scratch buffer has
        // to stop the current stream and reload before it can be started.
        if (lstrcmpiA(path, s_LoadedMusicPath) != 0)
        {
            StopMusicStream();
            // Ghidra shows `FUN_00513470()` with no argument, but the callee's only use of its
            // parameter is the CreateFileA path and this is its sole call site â€” another instance
            // of the parameter-recovery weakness noted in REVERSE_STATUS.md.
            if (!LoadMusicWaveFile(path)) return 0;
        }

        if (s_MusicStreamStopped == 0) return 1;
        if (s_DirectSound == null) return -1;

        // The decoded stream is always described as 16-bit stereo at the source sample rate, even
        // for a mono source â€” preserved as decompiled.
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
        // `&LAB_00513660` to CreateThread but contains no function at that address â€” Ghidra only
        // ever saw it address-taken, the same class of gap as the movie WndProc (TODO #14) and
        // the ordinal-790 device-lost callback (TODO #8). Calling CreateThread with an address we
        // cannot reconstruct would jump into nothing, so the call is skipped exactly like
        // CreateMoviePlayerWindow skips CreateWindowExA, and the handle stays zero.
        //
        // Consequence: everything above is real, so a track would start playing, but nothing ever
        // refills the buffer â€” the primed 80 blocks would loop forever instead of streaming on.
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
            // this returns WAIT_FAILED immediately instead of blocking â€” kept as decompiled.
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
    // â€” around 200 call sites all over Blaze.c. It looks the sample up in the sound directory,
    // then picks one of five hand-written resamplers by pitch band. Each resampler allocates a
    // DirectSound buffer sized for its output, converts the mono 8-bit source into 8-bit stereo
    // while applying the two gains, pads the remainder with silence and plays it.
    //
    // `pitch` is 1.10 fixed point with 0x400 as unity: 0x800 plays an octave up, 0x200 an octave
    // down. That reading is not a guess â€” it falls out of the dispatcher's own thresholds matching
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
    /// sample, i.e. plays back at ratio (n+1)/n â€” the fine pitch-up steps between unity and
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
    /// Ghidra: FUN_0053d420 @ 0x0053d420. Per group of n+1 source samples emits n+2 frames â€” the
    /// first sample, an interpolated frame midway to the next, then n more â€” i.e. plays back at
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
    /// Ghidra: FUN_0053d640 @ 0x0053d640. Per group of n source samples emits 2n-1 frames â€” one
    /// plain frame, then n-1 pairs of (sample, interpolated midpoint) â€” i.e. ratio n/(2n-1), the
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
    /// interpolated midpoint) â€” exactly half speed, one octave down.
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
        // No null check on the bank here â€” the original has none either, and none is needed: the
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
        // dropped â€” a register-attribution miss, not a different signature: it is the last arm of
        // the same ladder and every sibling arm passes the identical (src, length, gains) set.
        PlaySampleDoubledExceptOneIn(9, src, srcLength, gainLeft, gainRight);
    }

    /// <summary>Ghidra: FUN_0053db80 @ 0x0053db80 â€” fixed UI sound, id 0x38.</summary>
    private static void PlayUiSound38() => PlaySoundEffect(0x38, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dba0 @ 0x0053dba0 â€” fixed UI sound, id 0x39.</summary>
    private static void PlayUiSound39() => PlaySoundEffect(0x39, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053db60 @ 0x0053db60 â€” fixed UI sound, id 0x3a.</summary>
    private static void PlayUiSound3A() => PlaySoundEffect(0x3a, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dbc0 @ 0x0053dbc0 â€” fixed UI sound, id 0x3b.</summary>
    private static void PlayUiSound3B() => PlaySoundEffect(0x3b, 0x400, 0x2000, 0x2000);

    // TODO(REVERSE): FUN_0053da20 @ 0x0053da20 (positional gain calculation: distance/angle ->
    // left/right gain pair via the cos table, ordinal 450) and its caller FUN_0053db20 are the
    // remaining members of this family and are deliberately *not* reconstructed yet. Ghidra
    // declares FUN_0053da20 with 2 parameters but FUN_0053db20 calls it with 3, and its body reads
    // `unaff_SI` and `unaff_retaddr` â€” register-passed values Ghidra never tied to a parameter.
    // Reconstructing it would mean inventing a signature, so it waits for a Ghidra re-run with the
    // calling convention corrected (same class of problem as the cases listed in REVERSE_STATUS.md).

    /// <summary>Ghidra: FUN_005235d0 @ 0x005235d0 â€” loads the [Controls] key-binding table.</summary>
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

    /// <summary>Ghidra: FUN_00523540 @ 0x00523540 â€” saves the [Controls] key-binding table.</summary>
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

        // ECHAFAUDAGE TEMPORAIRE : trace du fichier d'options rÃ©ellement lu et des valeurs prises.
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
    /// thunk_FUN_0050f300 @ 0x0050f520 (identical body â€” both map onto this one method). Mirror
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
    // COM layout exactly: 0x50=SetCooperativeLevel â€” confirmed by the sibling error
    // string "SetCooperativeLevel() failed to g[et]..."; 0x54=SetDisplayMode called
    // with 640x480x16; 0x18=CreateSurface; 0x2c=Flip/0x6c=Restore in a device-lost
    // retry loop) plus a *second*, Blaze-owned C++ "stream" object (vtable
    // PTR_LAB_0054d0a0, constructed by FUN_005431e0) used to actually decode and
    // step video frames (open/pause/stop calls at vtable offsets 0x1c/0x20/0x24,
    // matching FUN_00543100/140/180; a 100ms SetTimer drives per-frame stepping).
    //
    // TODO(REVERSE): the Blaze-owned "stream" object's vtable slots point at
    // LAB_xxxxxxxx addresses with *no* decompiled body anywhere in Blaze.c â€” Ghidra
    // never saw their instructions, only their address taken (same situation as the
    // ordinal-790 device-lost callback documented above). The exact decoder backing
    // it (VfW vs. an internal codec) can't be determined without further disassembly
    // of Blaze.exe outside what Blaze.c provides. Rather than invent a specific COM
    // interface/IID (the two IID byte blobs `DAT_0054e8c8`/`DAT_0054e8d8` referenced
    // deeper in this cluster, e.g. FUN_00543320, aren't reproduced in Blaze.c's text
    // either), this reconstruction implements every part that *is* independently
    // verifiable (window lifetime, DirectDraw surface lifetime, joystick capture,
    // the message pump, the exe-relative movie path building) faithfully, and stops
    // exactly at the "step one frame" vtable call â€” TrySetupMovieStream below always
    // reports failure, which the original's own code already treats as a normal,
    // handled case (RunMainLoop keeps going without the intro movies playing).
    // =====================================================================

    // Ghidra: DAT_005dffa8, a 0x30-byte "movie player" struct; single global instance.
    private sealed class MoviePlayerState
    {
        public IntPtr HWnd;                 // offset 0x10 (param_1[4])
        public IntPtr HInstance;            // offset 0x14 (param_1[5])
        public void* DirectDrawWrapper;     // offset 0x1c (param_1[7]) â€” Ghidra: FUN_00512930 result
        public void* Stream;                // offset 0x18 (param_1[6]) â€” Ghidra: FUN_005431e0 result; TODO(REVERSE) vtable unrecoverable
        public bool Playing;                // offset 0x00 (*param_1)
        public int Result;                  // offset 0x2c (param_1[0xb])
        public byte* TitleBitmap;           // offset 0x08 (param_1[2]) â€” the attract screen's TITLE640.BMP
                                            // image. Allocated on *wsyskern's* heap inside LoadBitmapFile,
                                            // so it is released with WsysFree, never the CRT Free.
        public int TitleBitmapLoaded;       // offset 0x0c (param_1[3]) â€” nonzero once TitleBitmap is cached
        public int AttractActive;           // offset 0x04 (param_1[1]) â€” set while the title screen is up;
                                            // FUN_00542970 tests it alongside Playing
        public int TimerArmed;              // offset 0x20 (param_1[8]) â€” nonzero once SetTimer(hwnd,1,...) ran
        public int Field24;                 // offset 0x24 â€” OpenMovieStream writes 2
        public int Field28;                 // offset 0x28 â€” OpenMovieStream writes 1; gates the WM_TIMER step
    }

    private static MoviePlayerState? s_MoviePlayer; // Ghidra: DAT_005dffa8

    private static readonly byte* s_VfwClassName = ToAnsiZ("VfwClass"); // Ghidra: s_VfwClass_0056e6b8 â€” TODO(REVERSE): exact bytes not recovered, name inferred from symbol
    private static readonly byte* s_MovieWindowTitle = ToAnsiZ("Blaze & Blade"); // Ghidra: s_Blaze___Blade_005631d8 â€” TODO(REVERSE): exact bytes not recovered

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

    // DDSURFACEDESC is 0x6c bytes â€” the literal FUN_00512a10 writes into dwSize, corroborated by the
    // 0x1b-dword (108-byte) zeroing loop that precedes each use.
    /// <summary>
    /// Vrai quand la sÃ©quence de dÃ©marrage â€” Ã©cran-titre et attract â€” s'affiche dans la fenÃªtre du
    /// jeu au lieu de l'affichage exclusif du binaire d'origine.
    ///
    /// <para>
    /// C'est le drapeau de la **seule dÃ©viation de fidÃ©litÃ© volontaire** de ce projet. Les trois
    /// autres Ã©carts consignÃ©s dans <c>REVERSE_STATUS.md</c> sont subis â€” le refus du 16 bits par
    /// Windows moderne, les surfaces perdues, l'arÃ¨ne relocalisÃ©e par l'ASLR â€” et la reconstruction
    /// les contourne pour rester fonctionnelle. Celui-ci est demandÃ© : le jeu ne doit plus prendre
    /// l'affichage en exclusif ni changer le mode, pour n'avoir qu'une seule fenÃªtre du lancement
    /// au menu.
    /// </para>
    /// <para>
    /// <c>OPENFORESIA_LEGACY_STARTUP=1</c> rÃ©tablit intÃ©gralement le comportement d'origine, sur le
    /// modÃ¨le d'<c>OPENFORESIA_LEGACY_3DHELP</c>. Ce qu'il gouverne : le niveau de coopÃ©ration
    /// DirectDraw et le changement de mode dans <see cref="SetupDirectDrawSurfaces"/>, et les
    /// quatre sites qui affichent ou maximisent la fenÃªtre <c>VfwClass</c>.
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
    /// Ghidra: FUN_00512a10 @ 0x00512a10 (`__thiscall`) â€” brings DirectDraw up and creates the
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
        // Ghidra: FUN_005129e0 â€” DirectDrawCreate(NULL, &wrapper[0], NULL), 0 on failure.
        void* directDraw;
        int hr = DirectDrawCreate(null, &directDraw, null);
        if (hr < 0 || directDraw == null) return unchecked((int)0x80004005); // E_FAIL â€” Ghidra's -0x7fffbffb
        wrapper[0] = (uint)(nuint)directDraw;

        void** ddVtable = *(void***)directDraw;

        // Ghidra: (**(*dd + 0x50))(dd, hwnd, 0x13)
        // 0x13 = DDSCL_FULLSCREEN | DDSCL_ALLOWREBOOT | DDSCL_EXCLUSIVE.
        var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)ddVtable[IDirectDraw_SetCooperativeLevel];

        // DEVIATION DELIBEREE ET CONDITIONNELLE â€” change `render-title-screen-in-window`.
        //
        // L'original prend l'affichage en `DDSCL_FULLSCREEN | DDSCL_EXCLUSIVE` puis le bascule en
        // 640x480, et c'est la moitiÃ© Â« affichage Â» de l'Ã©cran plein Ã©cran qu'on veut supprimer.
        // Tout cet appareillage n'a qu'un produit rÃ©ellement consommÃ© : le contexte de pÃ©riphÃ©rique
        // que `BlitTitleScreen` obtenait par `GetDC` sur la surface primaire. Le backend le fournit
        // dÃ©sormais (`StartupSurface`), donc plus rien ne le rÃ©clame ici :
        //
        //   * le back buffer obtenu par `GetAttachedSurface` n'Ã©tait jamais stockÃ©,
        //   * aucun `Flip` n'est jamais appelÃ© sur cette chaÃ®ne,
        //   * les champs du wrapper renseignÃ©s ensuite ne sont lus que par le chemin vidÃ©o
        //     DirectShow, laissÃ© nul (TODO #13).
        //
        // On garde donc l'objet DirectDraw et son cycle de vie â€” `ReleaseDirectDrawSurfaces` le
        // libÃ¨re toujours â€” et on ne retire que la prise d'affichage. `OPENFORESIA_LEGACY_STARTUP=1`
        // rÃ©tablit intÃ©gralement le chemin d'origine.
        if (!s_WindowedStartup)
        {
            hr = setCooperativeLevel(directDraw, hwnd, 0x13);
            if (hr < 0) return hr;
        }
        else
        {
            // 8 = DDSCL_NORMAL, la mÃªme valeur que la sÃ©quence de dÃ©montage d'origine emploie.
            hr = setCooperativeLevel(directDraw, hwnd, 8);
            if (hr < 0) return hr;

            // Ni changement de mode, ni chaÃ®ne primaire/flip : plus personne ne les consomme, et
            // c'est leur suppression qui fait disparaÃ®tre le plein Ã©cran. Le wrapper garde les
            // valeurs de son constructeur ; les renseigner reviendrait Ã  inventer une description
            // de surface qui n'existe plus.
            return 0;
        }

        // Ghidra: (**(*dd + 0x54))(dd, 0x280, 0x1e0, 0x10) â€” 640 x 480 x 16bpp.
        var setDisplayMode = (delegate* unmanaged[Stdcall]<void*, uint, uint, uint, int>)ddVtable[IDirectDraw_SetDisplayMode];
        hr = setDisplayMode(directDraw, 0x280, 0x1e0, 0x10);

        // ENVIRONMENTAL DEVIATION, measured rather than assumed. The original only ever asks for
        // 640x480x16, and that is what is attempted first. Modern Windows declines 16bpp mode
        // changes outright â€” measured here as E_NOTIMPL (0x80004001) â€” while the same 640x480 at
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
        // This restore-on-exit hook is a safety net, not fidelity â€” remove it once the shutdown path
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
    /// Ghidra: FUN_00512b40 @ 0x00512b40 (`__thiscall`) â€” releases the surfaces and, crucially,
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
        // Ghidra: param_1[2] then param_1[1] â€” Release each if non-null, in that order.
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

            // Ghidra: (**(*dd + 0x50))(dd, hwnd, 8) â€” 8 = DDSCL_NORMAL. On failure the original
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
    /// Ghidra: the blit inside FUN_00542c40 (`Blaze.c:89916-89929`) â€” draws the loaded
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

        // RÃ©gime GDI de dÃ©marrage : la mÃªme image est Ã©crite dans la surface hors Ã©cran du backend,
        // qui sera prÃ©sentÃ©e dans la fenÃªtre du jeu.
        //
        // **Ajout, pas substitution.** Le chemin DirectDraw ci-dessous reste intact ; l'Ã©cran-titre
        // continue donc de s'afficher exactement comme avant, en plus d'alimenter notre surface.
        // C'est ce qui rend ce palier rÃ©versible : le retrait du chemin d'origine est un pas
        // sÃ©parÃ© et assumÃ© (change `render-title-screen-in-window`, groupe 6).
        BlitTitleScreenToStartupSurface(player);

        if (player.DirectDrawWrapper == null) return;

        // Ghidra: piVar1 = *(int **)(*(int *)(param_1 + 0x1c) + 4) â€” the wrapper's primary surface.
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
    // mÃªmes littÃ©raux. Ce ne sont donc pas des valeurs choisies par la reconstruction.
    private const int TitleScreenWidth = 0x280;    // 640
    private const int TitleScreenHeight = 0x1e0;   // 480

    /// <summary>
    /// Ã‰crit l'Ã©cran-titre dans la surface hors Ã©cran du backend, en rÃ©utilisant le mÃªme
    /// <c>StretchDIBits</c> et les mÃªmes dimensions que le chemin d'origine.
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

        // PrÃ©sentation : cet appel est la frontiÃ¨re d'image du rÃ©gime GDI, ce qu'EndScene est au
        // rÃ©gime Direct3D. Un Ã©chec ne dÃ©grade que ce redessin â€” journalisÃ© une fois, jamais
        // propagÃ©, jamais interruptif pour la boucle d'attract.
        //
        // **Aucune pompe de messages ici**, contrairement Ã  ce que fait EndScene. La boucle
        // d'attract (`ShowTitleScreenAndWait`) est elle-mÃªme une pompe `PeekMessageA(NULL, â€¦, PM_REMOVE)`
        // qui tourne en permanence et dispatche : les messages de notre fenÃªtre sont donc dÃ©jÃ 
        // servis. Vider la file ici consommerait les `WM_KEYDOWN` qui terminent l'attract, et le
        // dÃ©marrage n'atteindrait plus jamais le menu.
        if (StartupSurface.HasContent && !PresentStartupSurface() && !s_StartupPresentReported)
        {
            s_StartupPresentReported = true;
            Console.Error.WriteLine("[GDI] prÃ©sentation de l'Ã©cran-titre indisponible â€” redessin ignorÃ©");
        }

    }

    private static bool s_StartupPresentReported;

    /// <summary>
    /// PrÃ©sente la surface de dÃ©marrage dans la fenÃªtre du jeu. Rend faux si la fenÃªtre n'existe
    /// pas encore ou si le contexte de pÃ©riphÃ©rique est indisponible â€” l'appelant se contente alors
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
    /// Ghidra: FUN_00512930 @ 0x00512930 (`__thiscall`) â€” constructs the DirectDraw wrapper object.
    /// Every field the teardown and the blit later read is initialised **here**, which is why
    /// skipping it is not harmless: `Malloc` maps to <c>NativeMemory.Alloc</c> and does not zero,
    /// so `ReleaseDirectDrawSurfaces` read an uninitialised <c>wrapper[2]</c> as a COM pointer and
    /// dereferenced garbage. The original zeroes exactly these slots.
    /// </summary>
    private static void ConstructDirectDrawWrapper(uint* wrapper)
    {
        // Ghidra: SetRect(ESI + 0x10, 0,0,0,0) and SetRect(ESI + 0x20, 0,0,0,0) â€” two Win32 RECTs.
        SetRect((int*)(wrapper + 4), 0, 0, 0, 0);
        SetRect((int*)(wrapper + 8), 0, 0, 0, 0);

        wrapper[0] = 0;          // Ghidra: MOV [ESI],EDI       â€” IDirectDraw
        wrapper[1] = 0;          // Ghidra: MOV [ESI+0x4],EDI   â€” primary surface
        wrapper[2] = 0;          // Ghidra: MOV [ESI+0x8],EDI   â€” the slot that caused the crash
        wrapper[3] = 0;          // Ghidra: MOV [ESI+0xc],EDI
        wrapper[0xc] = 0;        // Ghidra: MOV [ESI+0x30],EDI
        wrapper[0xd] = 0xff00ff; // Ghidra: MOV [ESI+0x34],0xff00ff â€” the colour key
        wrapper[0xe] = 0;        // Ghidra: MOV [ESI+0x38],EDI
        wrapper[0xf] = 1;        // Ghidra: MOV [ESI+0x3c],1
        wrapper[0x10] = 0;       // Ghidra: MOV [ESI+0x40],EDI

        // Ghidra: sub = FUN_00547220(0x18); if (sub) FUN_00512d10(sub, this, hwnd, &local);
        //         this[0x44] = (result != 0) ? result + 0xc : 0;
        // The 0x18-byte object is the "Overlay Callback Object" â€” a COM sink whose vtable slots are
        // known (0x0054D028) but which nothing can ever call back into here, because no real
        // DirectDraw overlay is registered against it. Leaving this null is the original's own
        // allocation-failure path, not an invented one.
        wrapper[0x11] = 0;       // TODO(REVERSE): FUN_00512d10's overlay callback sink.
    }

    // Ghidra: s_SetCooperativeLevel___failed_to_g_00563178 â€” only the truncated symbol name
    // survives in Blaze.c; the reconstructed text is a best guess, same caveat as the other
    // message strings.
    private static readonly byte* s_SetCooperativeLevelFailed = ToAnsiZ("SetCooperativeLevel() failed to go back to normal mode.");

    // Safety net for the deviation noted above â€” not part of the original.
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
    /// Ghidra: FUN_00542a00 @ 0x00542a00 (`__thiscall`) â€” **stops playback**. This is the function
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
        // Stream is null in this reconstruction â€” TODO(REVERSE) #13 â€” so the guard skips it exactly
        // as the original would with no stream open. Nothing is invented here.
        if (player.Stream != null)
        {
            // TODO(REVERSE): stream teardown via the unrecoverable vtable; unreachable while
            // TrySetupMovieStream is a stub.
        }

        player.Playing = false;   // Ghidra: *param_1 = 0
        player.Result = result;   // Ghidra: param_1[0xb] = param_3
    }

    /// <summary>Ghidra: FUN_00542970 @ 0x00542970 (`__thiscall`) â€” WM_ACTIVATEAPP handling.</summary>
    private static void OnMoviePlayerActivate(MoviePlayerState player, int activated)
    {
        if (player.Playing || player.AttractActive != 0)
        {
            if (activated != 0)
            {
                // DEVIATION CONDITIONNELLE â€” voir s_WindowedStartup. Ce couple SetFocus +
                // SW_MAXIMIZE rend la fenÃªtre VfwClass plein Ã©cran **Ã  chaque activation de
                // l'application** pendant l'attract : la neutraliser Ã  la crÃ©ation ne suffit pas,
                // l'original la rÃ©tablit ici. C'est le site le plus important des quatre.
                // Le PostMessage qui suit dÃ©clenche le redessin et reste actif dans les deux cas.
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

    /// <summary>Ghidra: FUN_005429e0 @ 0x005429e0 (`__thiscall`) â€” WM_TIMER: step one movie frame.</summary>
    private static void OnMoviePlayerTimer(MoviePlayerState player, nuint timerId)
    {
        if (timerId == 1 && player.Field28 != 0)
        {
            // TODO(REVERSE): FUN_00512c80 â€” the DirectDraw wrapper's per-frame flip. Not reachable
            // while the wrapper's vtable is unreconstructed (#13); Field28 is only set once a
            // stream is really open.
        }
    }

    /// <summary>
    /// Ghidra: FUN_00542600 @ 0x00542600 â€” the `"VfwClass"` window procedure. Ghidra never
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
            case 0x0002: // WM_DESTROY â€” falls through to `return 0` in the original, doing nothing
                return 0;

            case 0x0010: // WM_CLOSE
                PostQuitMessage(0);
                return 0;

            case 0x001c: // WM_ACTIVATEAPP
                OnMoviePlayerActivate(player, (int)wParam);
                return 0;

            case 0x0100: // WM_KEYDOWN â€” only Escape stops playback; other keys fall through to 0
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

    /// <summary>Ghidra: FUN_00542740 @ 0x00542740 â€” creates the hidden window the movie player renders into.</summary>
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
        //                          hInstance, 0) â€” WS_EX_TOPMOST + WS_POPUP, created tiny and
        // resized below, which is why this window is invisible in practice.
        player.HWnd = CreateWindowExA(
            8 /* WS_EX_TOPMOST */, s_VfwClassName, s_MovieWindowTitle, 0x80000000 /* WS_POPUP */,
            0, 0, 10, 10, IntPtr.Zero, IntPtr.Zero, hInstance, null);

        // Ghidra: *ESI = 0; *(ESI+0xc) = 0 â€” done before CreateWindowExA in the original; ordering
        // is immaterial since the window procedure early-outs while DAT_005dffa8 is null.
        player.Playing = false;
        player.TitleBitmapLoaded = 0;

        // DEVIATION CONDITIONNELLE â€” voir s_WindowedStartup.
        // Ces deux appels sont la moitiÃ© Â« fenÃªtre Â» du plein Ã©cran de dÃ©marrage : un popup
        // WS_EX_TOPMOST maximisÃ© puis Ã©tendu Ã  2000x2000. La fenÃªtre reste crÃ©Ã©e â€” `joySetCapture`
        // et la procÃ©dure de fenÃªtre en dÃ©pendent â€” mais n'est plus montrÃ©e.
        // MesurÃ© (tÃ¢che 2.2) : les touches terminent toujours l'attract, la boucle qui les lit
        // faisant un `PeekMessageA(NULL, â€¦)` Ã  l'Ã©chelle du thread.
        if (s_WindowedStartup) return;

        ShowWindow(player.HWnd, 3 /* SW_MAXIMIZE */);
        MoveWindow(player.HWnd, 0, 0, 0x7d0, 0x7d0, 1);
    }

    /// <summary>Ghidra: FUN_00542940 @ 0x00542940 â€” destroys the movie player's hidden window.</summary>
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
    /// succeeds â€” the stream object's own construction can silently leave its fields zeroed
    /// without failing the overall open. TODO(REVERSE): see the subsystem banner above â€” the
    /// stream object's vtable can't be faithfully constructed, so `player.Stream` stays null.
    /// </summary>
    private static bool OpenMovieStream(MoviePlayerState player)
    {
        // Ghidra: iVar1 = FUN_00547220(0x48); then, only if that succeeded,
        //         iVar1 = FUN_00512930(*(param_1 + 0x10));   <- the HWnd
        //         *(param_1 + 0x1c) = iVar1;
        //         if (iVar1 == 0) return 0;
        //
        // FUN_00512930's disassembly (Legacy/Raw/FUN_00512930.txt) ends `MOV EAX,ESI; RET 0x4` â€” it
        // returns `this` unconditionally, even when its own internal Malloc fails. So the original's
        // `if (iVar1 == 0) return 0` only fires when the 0x48-byte allocation below fails, and the
        // open otherwise **succeeds**. A previous revision returned false here and justified it as
        // the original's no-DirectDraw path; that justification was wrong, and the deviation is now
        // unnecessary because the window procedure is reconstructed and the attract loop can end.
        void* ddrawWrapper = Malloc(0x48);
        if (ddrawWrapper == null) return false;

        // Ghidra: iVar1 = FUN_00512930(iVar1, param_1[0x10]) â€” the wrapper's constructor. It is not
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

        // Ghidra: FUN_00512a10(*(param_1 + 0x10)) â€” the tail call of FUN_00542800, with the wrapper
        // in ECX and the window handle as its stack argument. This is what fills wrapper[1] with the
        // primary surface, so the attract screen's blit has something to draw into.
        SetupDirectDrawSurfaces((uint*)player.DirectDrawWrapper, player.HWnd);

        return true; // faithful: the original returns 1 once the wrapper allocation succeeded
    }

    /// <summary>Ghidra: FUN_005428d0 @ 0x005428d0 â€” releases the sub-objects allocated by OpenMovieStream.</summary>
    private static void CloseMovieStream(MoviePlayerState player)
    {
        if (player.Stream != null)
        {
            // Ghidra: FUN_00543000() [reset stream vtable] + FUN_00547210(iVar1) [[Free]].
            Free(player.Stream);
        }
        player.Stream = null;

        // DEVIATION CONDITIONNELLE â€” voir s_WindowedStartup.
        if (player.HWnd != IntPtr.Zero && !s_WindowedStartup)
        {
            MoveWindow(player.HWnd, 0, 0, 2000, 2000, 1);
        }

        if (player.DirectDrawWrapper != null)
        {
            // Ghidra: FUN_00512b40(param_1[4]) â€” the real teardown. Skipping it is not harmless:
            // it is what drops the exclusive display mode, and without it winsys3d.dll's own 3D
            // init (Ordinal_789) cannot acquire DirectDraw and aborts the process.
            ReleaseDirectDrawSurfaces((uint*)player.DirectDrawWrapper, player.HWnd);
            Free(player.DirectDrawWrapper);
        }
        player.DirectDrawWrapper = null;

        // DEVIATION CONDITIONNELLE â€” voir s_WindowedStartup.
        if (player.HWnd != IntPtr.Zero && !s_WindowedStartup)
        {
            ShowWindow(player.HWnd, 3 /* SW_MAXIMIZE, per Ghidra literal */);
        }
    }

    /// <summary>Ghidra: FUN_00542f60 @ 0x00542f60 â€” allocates the movie-player struct and creates its window.</summary>
    private static void InitMoviePlayer()
    {
        // Ghidra: the allocation is followed by an explicit *(p+0xc) = 0; *(p+8) = 0 â€” the title
        // bitmap cache starts empty. A fresh MoviePlayerState is already zeroed, so this mirrors
        // the original rather than adding behavior.
        s_MoviePlayer = new MoviePlayerState { TitleBitmap = null, TitleBitmapLoaded = 0 };
        CreateMoviePlayerWindow(s_MoviePlayer);
    }

    /// <summary>Ghidra: FUN_00542f90 @ 0x00542f90 â€” destroys the window and frees the movie-player struct.</summary>
    private static void ShutdownMoviePlayer()
    {
        if (s_MoviePlayer == null) return;
        DestroyMoviePlayerWindow(s_MoviePlayer);
        if (s_MoviePlayer.Stream != null || s_MoviePlayer.DirectDrawWrapper != null)
        {
            CloseMovieStream(s_MoviePlayer);
        }

        // Ghidra: Free(*(DAT_005dffa8 + 8)) then FUN_00547210(iVar1) â€” note the two *different*
        // heaps, used correctly by the original: the title bitmap goes back to wsyskern (which
        // allocated it in LoadBitmapFile), the player struct to the CRT. The original calls this
        // unconditionally; wsyskern's Free tolerates null. Our struct is GC-managed, so only the
        // bitmap needs releasing here.
        WsysFree(s_MoviePlayer.TitleBitmap);
        s_MoviePlayer.TitleBitmap = null;
        s_MoviePlayer.TitleBitmapLoaded = 0;

        s_MoviePlayer = null;
    }

    /// <summary>Ghidra: FUN_00542f40 @ 0x00542f40 -> FUN_00542800 â€” tries to open the movie stream.</summary>
    private static bool TryOpenMovieStream()
        => s_MoviePlayer != null && OpenMovieStream(s_MoviePlayer);

    // Ghidra: s__sDATA_TITLE640_BMP_0056e6d4 â€” the wsprintfA format string is "%sDATA\TITLE640.BMP",
    // its "%s" filled with the CD drive root (DAT_005631f8). Only Ghidra's derived symbol name
    // survives in Blaze.c, not the literal bytes, so the text below is a best guess reconstructed
    // from that name â€” same caveat as the movie filenames above and REVERSE_STATUS.md TODOs #17/#22.
    private static readonly byte* s_TitleBitmapFileName = ToAnsiZ("DATA/TITLE640.BMP");

    /// <summary>
    /// Ghidra: FUN_00542f30 @ 0x00542f30 -> FUN_00542c40 â€” the attract-mode title screen: shows
    /// TITLE640.BMP and waits, returning non-zero to tell the attract loop to stop attracting.
    /// <para>
    /// Returns 1 when the caller should leave the attract loop (title bitmap unavailable, or â€” in
    /// the original â€” the user pressed something), 0 when the attract loop should continue to the
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

        // Ghidra: if (*(param_1+0xc) == 0) { ... } â€” load the title bitmap once, then cache it.
        if (player.TitleBitmapLoaded == 0)
        {
            byte* path = BuildCdPath(s_TitleBitmapFileName);
            player.TitleBitmap = LoadBitmapFile(path, 0 /* silent on failure â€” see wsyskern.md */);
            Free(path);

            // Ghidra: if (iVar3 == 0) return 1; â€” the original's own graceful degradation when the
            // CD (or the file) is absent. Reporting 1 is what breaks FUN_00529c10's attract loop,
            // letting startup continue to engine initialization instead of attracting forever.
            if (player.TitleBitmap == null) return 1;

            player.TitleBitmapLoaded = 1;
        }

        // ---------------------------------------------------------------------------------------
        // Deliberately not reconstructed â€” the rest of FUN_00542c40 (Blaze.c:89916-89975):
        //
        //   * `*(param_1+4) = 1` then a StretchDIBits blit of the 640x480 bitmap through the
        //     DirectDraw wrapper's own vtable â€” GetDC at offset 0x44, ReleaseDC at 0x68 on the
        //     inner object at *(param_1+0x1c)+4. Both calls are null-checked in the original, so
        //     this part would in fact degrade safely.
        //   * joySetCapture on both joysticks, then a message-pump loop that exits with 1 on
        //     WM_KEYDOWN (0x100), WM_LBUTTONDOWN (0x201), WM_RBUTTONDOWN (0x204),
        //     WM_MBUTTONDOWN (0x207), MM_JOY1BUTTONDOWN (0x3b5) or MM_JOY2BUTTONDOWN (0x3b6) â€”
        //     freeing the bitmap on the way out â€” re-blits on WM_TIMER (0x111, lParam 1), raises
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
                // *and* in the window procedure â€” the original tests them in this loop directly,
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
                    RaiseQuitException();
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

    /// <summary>Ghidra: FUN_00542f50 @ 0x00542f50 -> FUN_005428d0 â€” closes the stream if one is open.</summary>
    private static void CloseMovieStreamIfOpen()
    {
        if (s_MoviePlayer != null) CloseMovieStream(s_MoviePlayer);
    }

    /// <summary>
    /// Ghidra: FUN_00542a70 @ 0x00542a70 â€” opens the underlying video stream on the just-created
    /// window and arms the 100ms per-frame timer. TODO(REVERSE): entirely built out of calls
    /// through the unrecoverable custom "stream"/DirectDraw-wrapper vtables (see banner above);
    /// always reports failure here, matching how the original already handles a failed open.
    /// </summary>
    private static bool TrySetupMovieStream(MoviePlayerState player)
        => false;

    /// <summary>
    /// Ghidra: FUN_00542b70 @ 0x00542b70 â€” plays the stream opened by TrySetupMovieStream, pumping
    /// window messages (and raising the original's WM_QUIT-triggered fatal exception faithfully)
    /// until playback finishes, with joystick input captured for skip/cancel for the duration.
    /// </summary>
    private static int PlayMoviePumpLoop(MoviePlayerState player, byte* path)
    {
        // Ghidra: lstrcpyA(param_1[6] + 0xc, param_2) â€” copies into the (unrecoverable) stream
        // object's own filename buffer; nothing to copy into without that object, so skipped.
        player.Result = 0;

        joySetCapture(player.HWnd, 0, 0x32, 1);
        joySetCapture(player.HWnd, 1, 0x32, 1);

        if (TrySetupMovieStream(player))
        {
            // HAZARD â€” do not enable TrySetupMovieStream without also signalling completion.
            // This loop terminates only when `player.Playing` is cleared, and the sole thing that
            // clears it in the original is a callback through the movie stream object's vtable,
            // whose slots have no decompiled body anywhere in Blaze.c (REVERSE_STATUS.md TODO #13).
            // The loop is currently unreachable *only* because TrySetupMovieStream is a stub
            // returning false. Making that stub return true before the completion signal exists
            // re-hangs startup â€” this time in a tight PeekMessage spin rather than a bare one.
            // This is the same defect class that TODO #31 records for the attract loop.
            player.Playing = true;
            MSG msg;
            do
            {
                while (PeekMessageA(&msg, IntPtr.Zero, 0, 0, 1 /* PM_REMOVE */) != 0)
                {
                    if (msg.message == 0x12 /* WM_QUIT */)
                    {
                        RaiseQuitException();
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

    // Ghidra: s_TEPS_AVI_005684f4 / s_OPENING_AVI_005684e8 / s_ANI01_AVI_005684dc â€” only Ghidra's
    // auto-derived symbol names survive in Blaze.c, not the literal bytes; best-guess filenames.
    private static readonly byte* s_MovieNameTeps = ToAnsiZ("TEPS.AVI");
    private static readonly byte* s_MovieNameOpening = ToAnsiZ("OPENING.AVI");
    private static readonly byte* s_MovieNameAni01 = ToAnsiZ("ANI01.AVI");

    // =====================================================================
    // RunMainLoop and its remaining direct dependencies
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00504460 @ 0x00504460. Registers, then immediately unregisters, a set of 8
    /// winsys3d.dll "handler" slots (Ghidra: Ordinal_6/Ordinal_10 â€” see winsys3d.c FUN_00a040d0/
    /// FUN_00a04220, a generic handler-table used elsewhere for DPMI/legacy-interrupt-style
    /// callbacks), then sets up a joystick device via FUN_00523120 before resetting joystick
    /// state (FUN_00504b10). TODO(REVERSE): the 8 callback targets (&LAB_00504610.._680) have no
    /// decompiled bodies in Blaze.c, matching the ordinal-790/&LAB_00512ef0 situation elsewhere;
    /// passed through as null. FUN_00523120 (DirectInput joystick device creation, tied to the
    /// main window handle) has its own unread dependency chain (FUN_00522dc0/eb0/e40) â€” next
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

        // Ghidra: DAT_00604b54 = FUN_00523120(0, DAT_005db85c, &DAT_00604b50, 0, 0)
        //
        // `DAT_005db85c` is the main window handle that TODO #6 searched for exhaustively and never
        // found. It does not block this call, for two reasons established by reading the chain:
        // the first argument is an `HINSTANCE` whose null case the constructor handles explicitly
        // (`GetModuleHandleA(NULL)`), and the handle itself is only ever used as the window of
        // `SetCooperativeLevel`. The reconstruction now owns a real window â€” the Direct3D 11
        // backend's â€” so there is a legitimate handle to give, which was not true when TODO #6 was
        // declared blocked.
        fixed (byte** keyboardState = &s_KeyboardState_00604b50)
            s_JoystickDevice_00604b54 = CreateInputDevice(IntPtr.Zero, D3D11Window.Handle,
                                                         keyboardState, null, null);

        Ordinal_63(0);
        Ordinal_64();
        Ordinal_66();
        JoystickResetState(true);
    }

    // =====================================================================
    // DirectInput bring-up â€” Ghidra: FUN_00523120 and its init chain
    //
    // The game's entire input path hangs off one 0x8c4-byte object. `SetupJoystickHandlers`
    // creates it; `PollJoystickState` (FUN_005048f0) reads it every frame; `ShutdownStage1`
    // destroys it. Its layout, recovered from the constructor's zeroing runs and from the three
    // out-pointers FUN_00523120 hands back:
    //
    //     +0x000  IDirectInputA*
    //     +0x004  HWND â€” the window given to SetCooperativeLevel
    //     +0x008  keyboard device cell (a pointer to a pointer, allocated separately)
    //     +0x00c  16 joystick device cells
    //     +0x04c  mouse device cell
    //     +0x050  keyboard state area, 0x360 bytes   <- out-pointer 1 (DAT_00604b50)
    //     +0x3b0  joystick state area, 0x500 bytes   <- out-pointer 2
    //     +0x8b0  number of attached joysticks
    //     +0x8b4  mouse state area                   <- out-pointer 3
    //
    // Everything the DLL side needs was recovered from `Legacy/Blaze.exe` rather than assumed:
    // the two device GUIDs (`GUID_SysKeyboard` at 0x0054db58, `GUID_SysMouse` at 0x0054db48),
    // both DIDATAFORMATs (0x00546f60 = the 256-object keyboard format, 0x00546ff0 = the 7-object
    // mouse format) and the keyboard object table at 0x00545f60, whose entries are exactly
    // `{ &GUID_Key, i, DIDFT_OPTIONAL | DIDFT_BUTTON | (i << 8), 0 }`.
    // =====================================================================

    [DllImport("dinput.dll", ExactSpelling = true)]
    private static extern int DirectInputCreateA(IntPtr hinst, uint version, void** ppDI, void* unkOuter);

    // IDirectInputA â€” byte offsets divided by 4, checked against the call sites in Blaze.c.
    private const int IDirectInput_CreateDevice = 0x0c / 4;
    private const int IDirectInput_EnumDevices = 0x10 / 4;

    // IDirectInputDeviceA â€” likewise; SetDataFormat at +0x2c, SetCooperativeLevel at +0x34 and
    // Acquire at +0x1c are what FUN_005233c0/FUN_00523470 call, in that order.
    private const int IDirectInputDevice_Acquire = 0x1c / 4;
    private const int IDirectInputDevice_GetDeviceState = 0x24 / 4;
    private const int IDirectInputDevice_SetDataFormat = 0x2c / 4;
    private const int IDirectInputDevice_SetCooperativeLevel = 0x34 / 4;

    /// <summary>DISCL_FOREGROUND | DISCL_NONEXCLUSIVE â€” the literal 6 both device setups pass.</summary>
    private const uint DiscLevel_ForegroundNonExclusive = 6;

    private static readonly Guid s_GuidSysKeyboard =            // Ghidra: DAT_0054db58
        new(0x6F1D2B61, 0xD5A0, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private static readonly Guid s_GuidSysMouse =               // Ghidra: DAT_0054db48
        new(0x6F1D2B60, 0xD5A0, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private static readonly Guid s_GuidKey =                    // Ghidra: DAT_0054db18
        new(0x55728220, 0xD33C, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);

    [StructLayout(LayoutKind.Sequential)]
    private struct DIDATAFORMAT
    {
        public uint Size, ObjSize, Flags, DataSize, NumObjs;
        public void* Rgodf;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DIOBJECTDATAFORMAT
    {
        public Guid* Guid;
        public uint Ofs, Type, Flags;
    }

    /// <summary>
    /// Ghidra: the keyboard DIDATAFORMAT at 0x00546f60 and its object table at 0x00545f60, rebuilt
    /// in this process's memory. The table cannot be copied verbatim: every entry holds a pointer
    /// to a GUID at `Blaze.exe`'s own image base, which is not mapped here â€” so the bytes are
    /// reproduced field for field with our copy of `GUID_Key` substituted.
    /// </summary>
    private static DIDATAFORMAT* BuildKeyboardDataFormat()
    {
        const int objects = 0x100;
        var format = (DIDATAFORMAT*)NativeMemory.AllocZeroed((nuint)sizeof(DIDATAFORMAT));
        var table = (DIOBJECTDATAFORMAT*)NativeMemory.AllocZeroed(objects, (nuint)sizeof(DIOBJECTDATAFORMAT));
        var guid = (Guid*)NativeMemory.Alloc((nuint)sizeof(Guid));
        *guid = s_GuidKey;

        for (int i = 0; i < objects; i++)
        {
            table[i].Guid = guid;
            table[i].Ofs = (uint)i;
            table[i].Type = 0x8000000C | ((uint)i << 8);   // DIDFT_OPTIONAL | DIDFT_BUTTON | instance
            table[i].Flags = 0;
        }

        format->Size = 0x18;          // relevÃ© verbatim Ã  0x00546f60
        format->ObjSize = 0x10;
        format->Flags = 2;            // DIDF_ABSAXIS
        format->DataSize = objects;   // 256 octets d'Ã©tat clavier
        format->NumObjs = objects;
        format->Rgodf = table;
        return format;
    }

    private static DIDATAFORMAT* s_KeyboardDataFormat;

    private static readonly Guid s_GuidXAxis =                  // Ghidra: DAT_0054da98
        new(0xA36D02E0, 0xC9F3, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private static readonly Guid s_GuidYAxis =                  // Ghidra: DAT_0054daa8
        new(0xA36D02E1, 0xC9F3, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);
    private static readonly Guid s_GuidZAxis =                  // Ghidra: DAT_0054dab8
        new(0xA36D02E2, 0xC9F3, 0x11CF, 0xBF, 0xC7, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);

    /// <summary>
    /// Ghidra: the mouse DIDATAFORMAT at 0x00546ff0 and its 7-object table at 0x00546f80, rebuilt
    /// the same way as the keyboard's. The table is transcribed entry for entry from the binary:
    /// three axes carrying the X/Y/Z axis GUIDs, then four buttons with a null GUID, the Z axis and
    /// the last two buttons flagged optional.
    /// </summary>
    private static DIDATAFORMAT* BuildMouseDataFormat()
    {
        const int objects = 7;
        var format = (DIDATAFORMAT*)NativeMemory.AllocZeroed((nuint)sizeof(DIDATAFORMAT));
        var table = (DIOBJECTDATAFORMAT*)NativeMemory.AllocZeroed(objects, (nuint)sizeof(DIOBJECTDATAFORMAT));
        var axes = (Guid*)NativeMemory.Alloc(3, (nuint)sizeof(Guid));
        axes[0] = s_GuidXAxis; axes[1] = s_GuidYAxis; axes[2] = s_GuidZAxis;

        // (GUID, dwOfs, dwType) relevÃ©s Ã  0x00546f80 ; 0x03 = DIDFT_AXIS, 0x0c = DIDFT_BUTTON,
        // 0xffff00 = DIDFT_ANYINSTANCE, 0x80000000 = DIDFT_OPTIONAL.
        table[0] = new DIOBJECTDATAFORMAT { Guid = &axes[0], Ofs = 0x00, Type = 0x00FFFF03 };
        table[1] = new DIOBJECTDATAFORMAT { Guid = &axes[1], Ofs = 0x04, Type = 0x00FFFF03 };
        table[2] = new DIOBJECTDATAFORMAT { Guid = &axes[2], Ofs = 0x08, Type = 0x80FFFF03 };
        table[3] = new DIOBJECTDATAFORMAT { Guid = null, Ofs = 0x0C, Type = 0x00FFFF0C };
        table[4] = new DIOBJECTDATAFORMAT { Guid = null, Ofs = 0x0D, Type = 0x00FFFF0C };
        table[5] = new DIOBJECTDATAFORMAT { Guid = null, Ofs = 0x0E, Type = 0x80FFFF0C };
        table[6] = new DIOBJECTDATAFORMAT { Guid = null, Ofs = 0x0F, Type = 0x80FFFF0C };

        format->Size = 0x18;          // relevÃ© verbatim Ã  0x00546ff0
        format->ObjSize = 0x10;
        format->Flags = 2;            // DIDF_ABSAXIS
        format->DataSize = 0x10;      // DIMOUSESTATE
        format->NumObjs = objects;
        format->Rgodf = table;
        return format;
    }

    private static DIDATAFORMAT* s_MouseDataFormat;

    /// <summary>
    /// Ghidra: FUN_00523120 @ 0x00523120 â€” allocates the input-device object, constructs it and
    /// runs its initialisation, handing back three pointers into it. Returns null if either step
    /// fails, exactly as the original does (it frees the object and returns 0).
    /// </summary>
    private static byte* CreateInputDevice(IntPtr instance, IntPtr window,
                                           byte** keyboardState, byte** joystickState, byte** mouseState)
    {
        // Ghidra: iVar1 = FUN_00547220(0x8c4) â€” the CRT operator new. A null return is carried
        // through rather than short-circuited, matching the original.
        var device = (byte*)Malloc(0x8c4);
        if (device != null) InputDeviceConstruct(device, instance, window);

        int hr = InputDeviceInitialise(device);
        if (hr < 0)
        {
            if (device != null)
            {
                DestroyJoystickDevice((void**)device);
                Free(device);
            }
            return null;
        }

        if (keyboardState != null) *keyboardState = device + 0x50;
        if (joystickState != null) *joystickState = device + 0x3b0;
        if (mouseState != null) *mouseState = device + 0x8b4;
        return device;
    }

    /// <summary>
    /// Ghidra: FUN_00522dc0 @ 0x00522dc0 â€” the constructor. Creates the DirectInput object and
    /// zeroes the three state areas.
    /// </summary>
    private static void InputDeviceConstruct(byte* device, IntPtr instance, IntPtr window)
    {
        // Ghidra: if (param_2 == 0) param_2 = GetModuleHandleA(NULL) â€” which is why the never-found
        // DAT_005db85c never blocked this call.
        if (instance == IntPtr.Zero) instance = GetModuleHandleA(null);

        // Ghidra: DirectInputCreateA(hInstance, 0x500, this, 0) â€” version 5, the object receiving
        // the interface pointer at its own offset 0.
        int hr = DirectInputCreateA(instance, 0x500, (void**)device, null);
        if (hr < 0)
            Console.Error.WriteLine($"[INPUT] DirectInputCreateA -> 0x{hr:X8}");

        *(IntPtr*)(device + 4) = window;
        *(void**)(device + 8) = null;
        NativeMemory.Fill(device + 0x0c, 0x10 * sizeof(uint), 0);     // 16 joystick cells
        NativeMemory.Fill(device + 0x50, 0xd8 * sizeof(uint), 0);     // keyboard state area
        NativeMemory.Fill(device + 0x3b0, 0x140 * sizeof(uint), 0);   // joystick state area
        *(uint*)(device + 0x8b0) = 0;
        *(uint*)(device + 0x8b4) = 0;
        *(uint*)(device + 0x8b8) = 0;
        *(uint*)(device + 0x8bc) = 0;
        *(uint*)(device + 0x8c0) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00522eb0 @ 0x00522eb0 â€” creates the keyboard device, then the mouse device
    /// (whose failure is tolerated and simply clears its cell), then enumerates attached joysticks.
    /// </summary>
    private static int InputDeviceInitialise(byte* device)
    {
        if (device == null || *(void**)device == null) return -1;

        void* directInput = *(void**)device;
        IntPtr window = *(IntPtr*)(device + 4);

        // Ghidra: a 4-byte cell is allocated for each device pointer and stored in the object; the
        // device interfaces themselves live one indirection further out.
        var keyboardCell = (void**)Malloc(4);
        if (keyboardCell != null) *keyboardCell = null;
        *(void***)(device + 8) = keyboardCell;

        int hr = CreateKeyboardDevice(keyboardCell, directInput, window);
        if (hr < 0) return hr;

        var mouseCell = (void**)Malloc(4);
        if (mouseCell != null) *mouseCell = null;
        *(void***)(device + 0x4c) = mouseCell;

        // Ghidra: a failing mouse releases its cell and carries on â€” the mouse is optional.
        if (CreateMouseDevice(mouseCell, directInput, window) < 0)
        {
            if (mouseCell != null) { ReleaseComObject(*mouseCell); Free(mouseCell); }
            *(void***)(device + 0x4c) = null;
        }

        // Ghidra: (*directInput)[0x10](4, &LAB_00522d80, &count, 1) â€” EnumDevices over
        // DIDEVTYPE_JOYSTICK with DIEDFL_ATTACHEDONLY, whose callback counts what is attached.
        int joystickCount = 0;
        var enumDevices = (delegate* unmanaged[Stdcall]<void*, uint, void*, void*, uint, int>)
            (*(void***)directInput)[IDirectInput_EnumDevices];
        enumDevices(directInput, 4, (delegate* unmanaged[Stdcall]<void*, void*, int>)&CountJoystickCallback,
                    &joystickCount, 1);

        // TODO(REVERSE): FUN_00523230 creates one device per enumerated joystick. Ghidra's
        // decompilation of it is unusable â€” the whole frame is rendered as `puStack_`/`piStack_`
        // temporaries and every vtable call has lost its arguments â€” so it belongs to the
        // "read the disassembly" class rather than the reading class. Until it is recovered the
        // 16 joystick cells stay null, which every consumer already tests for (FUN_005230b0 skips
        // null cells), so the keyboard path is unaffected.
        *(int*)(device + 0x8b0) = joystickCount;

        Console.Error.WriteLine(
            $"[INPUT] DirectInput prÃªt â€” clavier acquis, {joystickCount} manette(s) dÃ©tectÃ©e(s), " +
            "crÃ©ation des manettes non reconstruite (FUN_00523230)");
        return 0;
    }

    /// <summary>
    /// Ghidra: &LAB_00522d80 â€” the enumeration callback, address-taken only and with no decompiled
    /// body anywhere. Its role is fixed by its use: the count it accumulates in the caller's
    /// `pvRef` is what the loop below it iterates over. Returning DIENUM_CONTINUE (1) is what makes
    /// the enumeration visit every attached device.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int CountJoystickCallback(void* instance, void* context)
    {
        if (context != null) (*(int*)context)++;
        return 1;   // DIENUM_CONTINUE
    }

    /// <summary>
    /// Ghidra: FUN_005233c0 @ 0x005233c0 â€” CreateDevice(GUID_SysKeyboard), SetDataFormat,
    /// SetCooperativeLevel(window, 6), Acquire. Ghidra recovered two parameters where the call site
    /// passes three: the window it hands to SetCooperativeLevel is the one it dropped.
    /// </summary>
    private static int CreateKeyboardDevice(void** cell, void* directInput, IntPtr window)
    {
        if (cell == null) return -1;

        var createDevice = (delegate* unmanaged[Stdcall]<void*, Guid*, void**, void*, int>)
            (*(void***)directInput)[IDirectInput_CreateDevice];

        Guid guid = s_GuidSysKeyboard;
        int hr = createDevice(directInput, &guid, cell, null);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] CreateDevice(clavier) -> 0x{hr:X8}"); return hr; }

        if (s_KeyboardDataFormat == null) s_KeyboardDataFormat = BuildKeyboardDataFormat();

        void* keyboard = *cell;
        void** vtable = *(void***)keyboard;

        var setDataFormat = (delegate* unmanaged[Stdcall]<void*, DIDATAFORMAT*, int>)
            vtable[IDirectInputDevice_SetDataFormat];
        hr = setDataFormat(keyboard, s_KeyboardDataFormat);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] SetDataFormat(clavier) -> 0x{hr:X8}"); return hr; }

        var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)
            vtable[IDirectInputDevice_SetCooperativeLevel];
        hr = setCooperativeLevel(keyboard, window, DiscLevel_ForegroundNonExclusive);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] SetCooperativeLevel(clavier) -> 0x{hr:X8}"); return hr; }

        var acquire = (delegate* unmanaged[Stdcall]<void*, int>)vtable[IDirectInputDevice_Acquire];
        acquire(keyboard);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00523470 @ 0x00523470 â€” the same sequence for GUID_SysMouse, with the 7-object
    /// mouse data format at 0x00546ff0. A failure at any step is returned to the caller, which
    /// treats a missing mouse as normal.
    /// </summary>
    private static int CreateMouseDevice(void** cell, void* directInput, IntPtr window)
    {
        if (cell == null) return -1;

        var createDevice = (delegate* unmanaged[Stdcall]<void*, Guid*, void**, void*, int>)
            (*(void***)directInput)[IDirectInput_CreateDevice];

        Guid guid = s_GuidSysMouse;
        int hr = createDevice(directInput, &guid, cell, null);
        if (hr < 0) return hr;

        if (s_MouseDataFormat == null) s_MouseDataFormat = BuildMouseDataFormat();

        void* mouse = *cell;
        void** vtable = *(void***)mouse;

        var setDataFormat = (delegate* unmanaged[Stdcall]<void*, DIDATAFORMAT*, int>)
            vtable[IDirectInputDevice_SetDataFormat];
        hr = setDataFormat(mouse, s_MouseDataFormat);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] SetDataFormat(souris) -> 0x{hr:X8}"); return hr; }

        var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)
            vtable[IDirectInputDevice_SetCooperativeLevel];
        hr = setCooperativeLevel(mouse, window, DiscLevel_ForegroundNonExclusive);
        if (hr < 0) { Console.Error.WriteLine($"[INPUT] SetCooperativeLevel(souris) -> 0x{hr:X8}"); return hr; }

        var acquire = (delegate* unmanaged[Stdcall]<void*, int>)vtable[IDirectInputDevice_Acquire];
        acquire(mouse);
        return 0;
    }

    /// <summary>Ghidra: the IUnknown::Release calls the device teardown paths make.</summary>
    private static void ReleaseComObject(void* obj)
    {
        if (obj == null) return;
        var release = (delegate* unmanaged[Stdcall]<void*, uint>)(*(void***)obj)[IUnknown_Release];
        release(obj);
    }

    // Ghidra: DAT_00604b50 â€” the keyboard state area inside the device object, handed back as
    // FUN_00523120's third argument. `DAT_00604b54`, the object itself, is declared with the
    // teardown code that already owned it.
    private static byte* s_KeyboardState_00604b50;

    /// <summary>
    /// Ghidra: FUN_00504b10 @ 0x00504b10 â€” resets the 9-slot joystick binding/state table.
    /// `clearName` mirrors the original's `param_1 != 0` guard around one extra field
    /// (`puVar1[1] = 0`) that's cleared only on some call sites â€” exact field meaning unconfirmed.
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
        public ushort Name; // Ghidra: puVar1[1] â€” TODO(REVERSE) exact field meaning unconfirmed
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

    // Ghidra: DAT_00564a48 â€” boolean capability flag set by CheckDeviceCapabilityFlag.
    private static bool s_DeviceCapFlag_00564a48;

    /// <summary>
    /// Ghidra: FUN_00529e40 @ 0x00529e40 â€” pushes a fixed "shading defaults" struct into
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
                            // faithful placeholder â€” see winsys3d.md.
        Ordinal_368(buf);
    }

    /// <summary>
    /// Ghidra: FUN_00529eb0 @ 0x00529eb0 â€” initializes several on-screen viewport/surface
    /// descriptors (via ordinals 658/661) and either fills the main render buffer with a
    /// checkerboard pattern + tiled blit (ordinals 674/667), or clears it to black (ordinals
    /// 665/667), depending on DAT_005633a8 (an unrecovered global â€” TODO(REVERSE) exact meaning).
    /// </summary>
    private static void InitViewportSurfaces()
    {
        // Ghidra renders these four calls with their arguments spilled to unattributed stack slots
        // (uStack_18..puStack_50) rather than as call arguments, and gives the fourth only three of
        // its five. Reading the slots back in ascending address order â€” the cdecl push order â€” and
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
        // instead of the four globals, and passed **null** for the fourth â€” which is what made
        // winsys3d.dll write through a null pointer and take down the process.
        SetDefDispEnv(s_DispEnv0_005e98dc, 0, 0xf0, 0x140, 0xf0);
        SetDefDispEnv(s_DispEnv1_005e994c, 0, 0, 0x140, 0xf0);
        SetDefDrawEnv(s_DrawEnv0_005e9880, 0, 0, 0x140, 0xf0);
        SetDefDrawEnv(s_DrawEnv1_005e98f0, 0, 0xf0, 0x140, 0xf0);

        // Ghidra: the 12 global stores that follow the calls. Each one lands inside one of the four
        // structs above at an offset that matches the DISPENV/DRAWENV layout exactly â€” which is the
        // independent confirmation that the identification is right. They override some of what the
        // two DLL helpers just defaulted.
        //
        // _DAT_005e98e4/_e6/_e8/_ea = disp env 0's `screen` rect  (0,0,320,240) â€” replaces the
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
        // (dither, offset 0x16 â€” already 1 from SetDefDrawEnv, so this store is a no-op) and `isbg`
        // (offset 0x18 â€” flipped from 0 to 1, i.e. clear the framebuffer to the background colour
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
            // Ghidra: two runs of `*(undefined4*)(mainMem + 4 + off) = 0x80008000;` â€” a checkerboard fill.
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

    // Les quatre structures sont **contiguÃ«s** dans le binaire, et cette contiguÃ¯tÃ© est un contrat,
    // pas un hasard de placement :
    //
    //     0x005e9880  DrawEnv0  92 octets
    //     0x005e98dc  DispEnv0  20      <- DrawEnv0 + 0x5C
    //     0x005e98f0  DrawEnv1  92      <- DrawEnv0 + 0x70
    //     0x005e994c  DispEnv1  20      <- DrawEnv1 + 0x5C
    //     0x005e9960  (s_FadeCounter, la globale suivante â€” d'oÃ¹ la taille totale de 224)
    //
    // FrameEnd publie l'environnement d'affichage par `Ordinal_657(nextDrawEnv + 0x5c)` : il
    // *dÃ©pend* du fait que le DISPENV suive immÃ©diatement son DRAWENV. Les allouer en quatre blocs
    // sÃ©parÃ©s faisait lire 20 octets de tas quelconque Ã  la place du DISPENV â€” d'oÃ¹ un Ã©cran noir
    // intermittent, mesurÃ© Ã  7 lancements sur 10, selon ce que le tas contenait Ã  cet endroit.
    // Voir ANALYSE_ECRAN_NOIR_MENU.md pour la mesure qui a isolÃ© la cause.
    private const int EnvBlockSize = 224;   // 0x005e9960 - 0x005e9880
    private static readonly byte* s_EnvBlock_005e9880 = (byte*)NativeMemory.AllocZeroed(EnvBlockSize);

    private static readonly byte* s_DrawEnv0_005e9880 = s_EnvBlock_005e9880 + 0x00;
    private static readonly byte* s_DispEnv0_005e98dc = s_EnvBlock_005e9880 + 0x5C;
    private static readonly byte* s_DrawEnv1_005e98f0 = s_EnvBlock_005e9880 + 0x70;
    private static readonly byte* s_DispEnv1_005e994c = s_EnvBlock_005e9880 + 0xCC;

    // Ghidra: DAT_005633a8 â€” read pervasively as a mode flag (movie-intro skip / safe-mode?);
    // TODO(REVERSE): exact meaning not confirmed, no assignment site reached yet from RunMainLoop.
    private static int s_UnknownModeFlag_005633a8;

    // Ghidra: DAT_005631f8 â€” the game's **data root** on the CD. Recovered from Legacy/Blaze.exe at
    // VA 0x005631f8: the bytes are "@:\FRENCH\", NOT the bare drive root this was previously assumed
    // to be. See Documentation/DataSymbols.md.
    //
    // '@' (0x40, 'A'-1) is the drive-letter placeholder; RunApplication's scan writes the detected
    // letter into index 0 and resets it to '@' when nothing matches. Only index 0 is ever patched â€”
    // the language directory is static data, which is exactly why Blaze.c's text (code only, no
    // .data) showed a single-byte store and gave no hint that the rest of the string existed.
    //
    // This is a distinct buffer from the CD *presence marker* path 16 bytes earlier
    // (s____Blaze_Blade_005631e8 = "@:\Blaze&Blade"). Conflating the two is what made every
    // CD-relative path resolve one directory too high and find nothing.
    private static readonly byte* s_CdDataRootPath = ToAnsiZ("@:\\FRENCH\\");

    /// <summary>
    /// Ghidra: FUN_00529c10 @ 0x00529c10 â€” Blaze &amp; Blade's own "RunMainLoop": plays the four
    /// intro movies (gracefully skipped if the movie subsystem can't be opened, exactly like the
    /// original's own handling of a failed open), re-initializes engine/device state, loads the
    /// UI font from the CD, sets up the viewport, and finally hands off to the real per-frame game
    /// loop (FUN_0052a3d0).
    /// </summary>
    private static void RunMainLoop()
    {
        // La fenÃªtre du jeu naÃ®t ici, avant l'Ã©cran-titre â€” pas de device, il attend le premier
        // dÃ©but de scÃ¨ne. Hors reconstruction : le binaire d'origine n'a pas de fenÃªtre Ã  lui Ã  ce
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

            // Ghidra: Blaze.c:74441-74451. The attract loop has FOUR exit conditions, one per step â€”
            // title screen, ANI01, title screen, OPENING â€” and every one of them must stay, even
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
        Ordinal_45(null, s_CdDataRootPath); // Ghidra: apcStack_54[1]="j" â€” TODO(REVERSE): odd literal, but Ordinal_45 is a confirmed no-op regardless
        Ordinal_136(0);
        s_RandSeed_0056e9c0 = 1; // Ghidra: FUN_00547130(1) â€” srand(1); see CrtRand
        s_Unknown_005e9630 = Ordinal_116();
        Ordinal_771();
        InitTrigTables(); // Ordinal_337, re-run (idempotent â€” see winsys3d.md)
        // Ghidra: Ordinal_789(0, DAT_00566cc4) â€” Blaze.c line 79100. The second argument is **not**
        // inert, as an earlier note here claimed: winsys3d.c FUN_00a06420 opens with
        // `DAT_00a2c49c = param_2`, and that global indexes the base-offset tables SetShift uses to
        // position the viewport. Passing 0 selected the 320x240 entry (-160,-120), which is why the
        // scene rendered unscaled in a corner of a 640x480 screen.

        // `Ordinal_789` charge `3dhelp.dll` et lui fait crÃ©er son device, ce qui bascule le mode
        // d'affichage du bureau. Le device synthÃ©tique intercepte cette crÃ©ation. Voir
        // `SyntheticDevice.cs` ; opt-in tant qu'il n'est pas Ã©prouvÃ©.
        InstallSyntheticDevice();

        Ordinal_789(0, s_ResolutionIndexForClamp);
        InstallDeviceShim();   // ECHAFAUDAGE TEMPORAIRE â€” voir DeviceShim.cs
        LogDisplayMode("aprÃ¨s init 3D"); // ECHAFAUDAGE TEMPORAIRE
        ShutdownMoviePlayer(); // Ghidra: FUN_00542f90()
        CheckDeviceCapabilityFlag(); // Ghidra: FUN_0050f120()
        Ordinal_110(0);
        Ordinal_681(0);
        SetShadingDefaults(); // Ghidra: FUN_00529e40()
        InitViewportSurfaces(); // Ghidra: FUN_00529eb0()
        // Ghidra: FUN_0040a600() â€” confirmed no-op (`return;` with an empty body).
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
    // DAT_005e97e0 (screen stride/pitch?) / _DAT_005e9820 â€” all read again by not-yet-reconstructed
    // rendering code.
    private static uint s_RandSeed_0056e9c0; // Ghidra: DAT_0056e9c0 â€” the CRT rand seed
    private static uint s_Unknown_005e9630;
    private static uint s_Unknown_005e97f0;
    private static uint s_ScreenStride_005e97e0;
    // Ghidra: DAT_005e97c0 â€” renamed from an earlier "screen/back-buffer base" guess once
    // FUN_0052a030 (LoadFirstLevelResources) showed it's really the destination buffer for the
    // top-level "<cd-drive-root>SYSTEM.INI"-equivalent resource-file load, immediately after
    // RunMainLoop sets it to `mainMem + 0xe0000` here â€” a fixed scratch address inside winsys3d's
    // arena reserved for that blob (LoadFirstLevelResources then treats it as a struct: offsets
    // 0x10/0x48/0x6c/100 are read back as sub-tables/fields once loaded).
    private static byte* s_MainResourceBuffer_005e97c0;
    private static int s_Unknown_005e9820;

    /// <summary>
    /// Ghidra: FUN_00547130 @ 0x00547130 â€” trivial setter, folded directly into RunMainLoop above
    /// (`s_Flag_0056e9c0 = 1`) rather than kept as a separate one-line method.
    /// </summary>

    /// <summary>
    /// Ghidra: the font-loading sequence inlined at the end of FUN_00529c10 (no separate function â€”
    /// builds "&lt;cd-drive-root&gt;DATA\FONT.BMP"-style path via string concatenation, opens it with
    /// Ordinal_331/333/334/336, which are the same thin CreateFileA/SetFilePointer/ReadFile/
    /// CloseHandle wrappers already confirmed for OpenExistingFileReadOnly elsewhere â€” called via
    /// real Win32 here for consistency with that precedent â€” reads a chunk into the engine's main
    /// memory arena, then builds a font/glyph table via ordinals 92/104/132/712/720 and the two
    /// FUN_0052a030/FUN_0052a3d0 hand-off below).
    /// </summary>
    private static void LoadUiFontFromCd()
    {
        // Ghidra: s_DATA_Font_bmp_005684cc â€” only the derived symbol name survives; best guess below.
        byte* fontFileName = ToAnsiZ("DATA/Font.bmp"); // recovered verbatim from Legacy/Blaze.exe @ 0x005684cc â€” forward slash, mixed case
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
    // from "BLAZE.ALL", a monolithic resource archive on the CD (sector-addressed â€” offsets are in
    // 2048-byte units, confirmed by the `<< 0xb` shift in LoadResourceEntry), plus a couple of
    // standalone files ("SYSTEM.INI"-equivalent, "BASE.SND"-equivalent) loaded whole.
    // =====================================================================

    // Ghidra: PTR_s_BLAZE_ALL_1_00566bbc -> 0x005684b0, PTR_s_SYSTEM_INI_1_00566bb8 -> 0x005684bc.
    // Recovered verbatim from Legacy/Blaze.exe â€” the `;1` is an ISO-9660 file version, and Ghidra's
    // "_1_" in the symbol names was echoing it all along. Keep the suffix: NormalizePath truncates
    // at ';' before the open, exactly as winsys3d.dll does. See Documentation/DataSymbols.md.
    private static readonly byte* s_ArchiveFileName = ToAnsiZ("BLAZE.ALL;1");
    private static readonly byte* s_SystemFileName = ToAnsiZ("SYSTEM.INI;1");

    // Ghidra: s____snd_Base_snd_00568500, recovered as "@:\snd\Base.snd". A CD-**root** absolute
    // template with its own '@' drive-letter placeholder â€” it does NOT go through the data root.
    // Lowercase "snd", matching the disc's E:\Snd\ directory (Windows is case-insensitive here).
    private static readonly byte* s_SoundArchiveTemplate = ToAnsiZ("@:\\snd\\Base.snd");

    /// <summary>
    /// Builds a **data-root-relative** path: the data root (Ghidra DAT_005631f8, "@:\FRENCH\" with
    /// its drive letter patched) followed by a relative filename. This is the family that covers
    /// BLAZE.ALL;1, SYSTEM.INI;1, DATA/Font.bmp, DATA/TITLE640.BMP and the three intro movies.
    /// <para>
    /// It is <b>not</b> the only family â€” see <see cref="BuildCdRootPath"/> for templates that are
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
    /// drive-letter placeholder â€” e.g. "@:\snd\Base.snd" (Ghidra: s____snd_Base_snd_00568500) and
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
    /// Ghidra: FUN_0052a660 @ 0x0052a660 â€” checks whether `dest` is one of three well-known
    /// "class override table" destinations; if so, substitutes hardcoded native function-pointer
    /// tables instead of loading resource-file bytes into it. TODO(REVERSE): none of the three
    /// comparison targets (Ghidra: DAT_006008c8/DAT_00604930/DAT_00600904) have an assignment site
    /// anywhere in the reconstructed call graph yet, so they're always null here and every real
    /// call site's `dest` (always a valid non-null scratch-buffer pointer) can never match â€” this
    /// always returns false in the current reconstruction. The three true-branch bodies (the
    /// DAT_00600904 one alone pulls in 11 further FUN_ calls, presumably per-entity-class native
    /// vtable installers) are out of scope for this pass and stubbed loudly rather than guessed.
    /// </summary>
    private static bool IsKnownOverrideDestination(void* dest)
    {
        if (dest == s_OverrideDestA_006008c8) return true; // Ghidra: no body at all for this case â€” just `return 1;`
        if (dest == s_OverrideDestB_00604930)
            throw new NotImplementedException("FUN_0052a660: DAT_00604930 override-table branch not reconstructed â€” see REVERSE_STATUS.md");
        if (dest == s_OverrideDestC_00600904)
            throw new NotImplementedException("FUN_0052a660: DAT_00600904 override-table branch (+11 dependent FUN_ calls) not reconstructed â€” see REVERSE_STATUS.md");
        return false;
    }

    // Ghidra: DAT_006008c8 / DAT_00604930 / DAT_00600904 â€” never assigned anywhere reached yet.
    private static void* s_OverrideDestA_006008c8;
    private static void* s_OverrideDestB_00604930;
    private static void* s_OverrideDestC_00600904;

    /// <summary>
    /// Ghidra: FUN_00529b00 @ 0x00529b00. Opens `path`, reads the whole file into `dest`, closes
    /// it, and returns the next dword-aligned address past the loaded data (bump-allocator style â€”
    /// matches LoadResourceEntry's own return convention below). TODO(REVERSE): Ghidra's own
    /// decompilation of this function is incomplete â€” the failure path returns `unaff_ESI` (a
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
        if ((int)handle < 0) return null; // TODO(REVERSE): see summary above â€” original's real failure value unrecoverable

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
            // Ghidra: `/* WARNING: Do nothing block with infinite loop */ do {} while (true);` â€”
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

    // Ghidra: DAT_0056aca8 â€” a ushort-driven schema table describing which fields of a loaded
    // resource blob need base-relative-to-absolute pointer fixups (see FixupResourceHeader below).
    // TODO(REVERSE): data-only global, byte content not present in Blaze.c's text; left empty, so
    // the schema-driven second fixup pass below is a safe (but incomplete) no-op â€” same category of
    // gap as s_ResolutionDetailLimit elsewhere in this file.
    // Ghidra: DAT_0056aca8 â€” recovered from Legacy/Blaze.exe, 37 ushorts ending at the 0x0000 the
    // original's loop terminates on. The terminator is NOT stored here; the loop below runs to the
    // array's length instead, which is equivalent and keeps the sentinel out of the data.
    //
    // Each entry packs two fields, and this is confirmed by FUN_00532070's own masking rather than
    // inferred: the **low byte is an index** into the header's already-relocated pointer array
    // (`header[4 + index]`), and the **high byte is the byte stride** used to walk that list.
    // The earlier "(record kind, size)" reading in an older revision of DataSymbols.md was wrong.
    //
    // Every index is below 0x3c, matching the 60 dwords the fixed pass relocates â€” an internal
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
    /// The two passes use **different** relocation formulas â€” the fixed pass adds
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

            // Ghidra checks both the pointer and its first dword â€” an empty list is stored as a
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
        public uint Mode;        // +0x00 â€” pixel mode; header & 7, or 2 when the TIM has no CLUT
        public short* ClutRect;  // +0x04 â€” RECT {x,y,w,h}; null when there is no CLUT
        public void* ClutData;   // +0x08 â€” palette bytes; null when there is no CLUT
        public short* PixelRect; // +0x0c â€” RECT {x,y,w,h}
        public void* PixelData;  // +0x10 â€” pixel bytes
    }

    /// <summary>
    /// Ghidra: FUN_005322f0 @ 0x005322f0 â€” uploads one TIM texture (loaded by LoadResourceEntry
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
    /// **`EDI` = parameter 3**, the metadata triple â€” not from the chunk pointer.</item>
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

        // Ghidra: MOV EAX,[ESP+0x10]; TEST EAX,EAX; JZ â€” the test is on **caddr**, not crect.
        if (tim.ClutData != null)
        {
            short* clutRect = tim.ClutRect;

            clutRect[0] = 0;                                    // x = 0
            clutRect[1] = (short)(meta[1] + 0x1c0);             // y = meta[1] + 0x1c0

            // Read w and h *before* w is overwritten â€” the original does the same, and the
            // multiply below depends on the original width.
            int packed = clutRect[3] * clutRect[2];             // h * w
            packed = (packed + 0xff) & unchecked((int)0xffffff00);
            clutRect[2] = 0x100;                                // w = 256
            clutRect[3] = (short)(packed >> 8);                 // h = rounded / 256
            clutByteCount = packed;

            Ordinal_674(clutRect, tim.ClutData);                // LoadImage(crect, caddr)
        }

        // Ghidra: MOV EAX,[ESP+0x18] â€” guarded on **paddr**, plus a skip when meta[0] == 0x1c.
        if (tim.PixelData != null && meta[0] != 0x1c)
        {
            short* pixelRect = tim.PixelRect;

            // Ghidra: AND AL,0xf; SHL ECX,6.
            pixelRect[0] = (short)((meta[0] & 0x0f) << 6);

            // Ghidra: AND AL,0x10 â€” a *byte* mask. The decompiler rendered this as `& 0xff10`,
            // which is wrong and would have corrupted the y coordinate.
            pixelRect[1] = (short)((((meta[0] & 0x10) + meta[2] * 4)) << 4);

            Ordinal_674(pixelRect, tim.PixelData);              // LoadImage(prect, paddr)
        }

        // Ghidra: 005323bf-005323f2. Packs the texture-page word out of the pixel rect and the TIM
        // mode â€” libgpu's GetTPage shape. Transcribed operation for operation rather than
        // simplified, since the intermediate 16-bit SAR is not equivalent to a 32-bit one.
        int x = tim.PixelRect[0];
        int y = tim.PixelRect[1];
        int tpage = ((x >> 2) & 0xf0) | (y & 0x100);
        tpage |= 0x2000;                                        // Ghidra: OR CH,0x20
        tpage = (short)tpage >> 4;                              // Ghidra: SAR CX,4 â€” 16-bit
        tpage |= ((int)tim.Mode & 3) << 7;
        slot[0] = (ushort)tpage;

        // Ghidra: (meta[1] + 0x1c0) << 6 â€” libgpu's GetClut for x=0, matching the crect y above.
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

    // Ghidra: DAT_005e8fc0 â€” **not a second table**. It is DAT_005e8f00 offset by 0xC0, i.e. the
    // same tpage/clut array starting 48 entries in, and Ghidra simply named two points inside it.
    //
    // Modelling them as separate allocations is what left the main menu black: its panel
    // descriptor asks for entry 0x31 of DAT_005e8f00 = 0x31 - 0x30 = entry 1 of DAT_005e8fc0,
    // which LoadFirstLevelResources fills. LoadMenuResources only writes entries 0..3 (measured:
    // the menu's directory has exactly four), so entry 0x31 came back zero and every panel
    // primitive sampled texture page 0 â€” an empty region of VRAM, hence a black screen with
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
    // entries of 8 bytes, and 0x4c is exactly the last valid index â€” the sizes corroborate.
    private const int SoundDirectoryBaseCount = 0x1e0 / 8;  // 60 entries, ids 0x00..0x3b
    private const int SoundDirectoryCount = 0x268 / 8;      // 77 entries, ids 0x00..0x4c
    private static readonly SoundDirectoryEntry* s_SoundDirectory =
        (SoundDirectoryEntry*)NativeMemory.AllocZeroed(SoundDirectoryCount * (nuint)sizeof(SoundDirectoryEntry));
    // Ghidra: DAT_005e97e4 / _DAT_005e9810 / _DAT_005e9824 / DAT_005de538 â€” all read again by
    // not-yet-reconstructed code.
    private static uint s_Unknown_005e97e4;
    private static int s_Unknown_005e9810;
    private static IntPtr s_Unknown_005e9824;
    private static int s_Unknown_005de538;
    // Ghidra: &DAT_005e9180 â€” small per-slot table reset at the end of FUN_0052a030 (4 entries of
    // 0x4c/76 bytes: 0x13 dwords zeroed, byte[-1]=8, byte[0]=incrementing slot id). Also touched
    // (not yet reconstructed) by FUN_00542160's near-identical loop elsewhere.
    private static readonly byte* s_EntitySlotTable_005e9180 = (byte*)NativeMemory.AllocZeroed(0x140);

    /// <summary>
    /// Ghidra: FUN_0052a030 @ 0x0052a030. Loads the "&lt;cd-drive-root&gt;SYSTEM.INI"-equivalent
    /// level/base resource file, fixes up its internal pointers, decodes each directory entry it
    /// lists (throws at the first one â€” see DecodeResourceChunk above), loads the base sound
    /// archive's header + big scratch buffer, and resets the small entity-slot table.
    /// </summary>
    private static void LoadFirstLevelResources()
    {
        // Ghidra: FUN_0040a600() â€” confirmed no-op.
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
                    // Ghidra: `undefined *puVar13; ... puVar13 = puVar13 + 4;` â€” a **byte** pointer
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
        // Ghidra doesn't guard the open failing here either â€” ReadFile/CloseHandle on an invalid
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

    // Ghidra: DAT_005e97c4 â€” the loaded resource file's own "next free address" bookkeeping value;
    // not yet used further by any reconstructed code past LoadFirstLevelResources itself.
    private static byte* s_MainResourceDirectory_005e97c4;

    /// <summary>
    /// Ghidra: FUN_0052a3d0 @ 0x0052a3d0 â€” the real, never-returning per-frame game loop. Ticks via
    /// an indirect call through a Blaze-owned function pointer (Ghidra: PTR_FUN_0056c96c) whose
    /// assignment site hasn't been found yet anywhere in the reconstructed call graph, then
    /// dispatches to FUN_0052a440 whenever the game-state id (Ghidra: DAT_005de510) changes away
    /// from 0. The only way out, in the original, is process termination via Exit()/ExitProcess â€”
    /// there is no normal `return` path, so RunApplication's post-RunMainLoop shutdown sequence is
    /// dead code in practice (still preserved faithfully in RunApplication's `finally` block).
    /// TODO(REVERSE): PTR_FUN_0056c96c's assignment site not found; FUN_0052a440 (the per-state
    /// dispatcher) not yet reconstructed â€” next frontier after LoadFirstLevelResources above.
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
                    // Ghidra: FUN_00529b50(entry, DAT_006008c8) â€” this destination is always
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
                RunLoadingScreen();     // Ghidra: FUN_0052a440
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0052a440 @ 0x0052a440 â€” **the inter-state loading screen**, and not the
    /// "per-game-state dispatcher" an earlier revision of this file called it. Two call sites fix
    /// its role beyond doubt: <see cref="RunGameLoop"/> runs it whenever the game leaves a state
    /// for one other than 7, 9, 2 or 3, and <c>FUN_00516250</c> runs it when the current area index
    /// changes, immediately before that area's resources are loaded.
    /// <para>
    /// What it does: load one resource entry into the scratch at <c>MainMem + 0x1b0000</c>, decode
    /// it, then show it for 150 frames. It is skippable â€” pressing START or SQUARE plays a UI sound
    /// and the screen ends when that press is <em>released</em>, which is also why a button already
    /// held on entry cannot skip it.
    /// </para>
    /// <para>
    /// **Ghidra did not recover this function's stack frame.** Several calls are rendered with
    /// their arguments as free-standing locals (<c>piStack_50</c>/<c>puStack_54</c> are argument
    /// slots, not variables), and one slot the code reads is never written anywhere in the
    /// pseudocode. Two consequences are marked TODO(REVERSE) below. The argument order of the two
    /// resource calls is not among them: <c>FUN_00516250</c> calls the same
    /// <c>FUN_00529b50(entry, destination)</c> with both arguments intact, which settles it.
    /// </para>
    /// </summary>
    private static void RunLoadingScreen()
    {
        // Ghidra: FUN_0040a600() â€” an empty body in Blaze.c (`void FUN_0040a600(void) { return; }`).
        // Called here and at the top of every game-loop iteration; nothing to reproduce.

        byte* mainMem = GetMainMemPtr();
        if (s_MainResourceBuffer_005e97c0 == null || mainMem == null) return;

        // Ghidra: piVar2 = *(int **)(DAT_005e97c0 + 0x84) â€” a table of (entry, metadata) pairs.
        int* pairs = *(int**)(s_MainResourceBuffer_005e97c0 + 0x84);
        if (pairs == null) return;

        // Ghidra: the walk to the end of the pair list. **Its result is never used** â€” transcribed
        // rather than dropped, because a discarded traversal usually means Ghidra lost the use, not
        // that the original wrote a dead loop.
        int* walk = pairs;
        while (*walk != 0) walk += 2;

        // Ghidra: game state 3 picks its entry from DAT_005de528; every other state uses 0xb.
        uint entryIndex = s_GameStateId == 3 ? (uint)(s_Unknown_005de528 & 0xff) : 0xb;

        byte* scratch = mainMem + 0x1b0000;
        LoadResourceEntry((int*)(nint)pairs[entryIndex * 2], scratch);
        DecodeResourceChunk(scratch,
                            (ushort*)(s_EntityDecodeTable_005e8f00 + 0xfc),   // Ghidra: &DAT_005e8ffc
                            (byte*)(pairs + entryIndex * 2 + 1));

        // Ghidra: two Ordinal_676 calls â€” winsys3d's MoveImage(RECT* src, int dstX, int dstY),
        // confirmed at winsys3d.c:5348, which copies a VRAM rectangle to another VRAM position.
        // The two rectangles are the eight ushorts the function opens with.
        //
        // TODO(REVERSE): the destinations are what the lost stack frame costs here. The first call
        // is preceded by `piStack_50 = 0; puStack_54 = 0`, which are its two pushed arguments, so
        // (0, 0) is well supported; the second is rendered as `Ordinal_676(&uStack_28, 0, 0)`,
        // whose source address is four bytes below the first rectangle and cannot be right. Both
        // are transcribed with the rectangles as read; a Ghidra listing of 0x0052a440 settles the
        // second one in minutes, the way it settled FUN_0052a1e0 and FUN_0052a280.
        ushort* rect = stackalloc ushort[8];
        rect[0] = 0x380; rect[1] = 0; rect[2] = 0x80; rect[3] = 0xe0;
        rect[4] = 0; rect[5] = 0x1ff; rect[6] = 0x100; rect[7] = 1;
        Ordinal_676(rect, 0, 0);
        Ordinal_676(rect + 4, 0, 0);

        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);

        // Ghidra: the 20-byte sprite descriptor, the same shape as the main menu's â€” pointer,
        // colour, x/y, tpage/clut, index â€” and passed by address to both renderers.
        byte* sprite = s_LoadingScreenSprite;
        *(uint*)(sprite + 0x04) = 0x1000000;
        *(ushort*)(sprite + 0x08) = 0xa0;
        *(ushort*)(sprite + 0x0a) = 0x80;
        *(ushort*)(sprite + 0x0c) = 0x1400;
        *(ushort*)(sprite + 0x0e) = 0x1249;
        *(uint*)(sprite + 0x10) = 0;

        // TODO(REVERSE): the descriptor's pointer field. The original sets it to the address of a
        // stack slot that **nothing in the pseudocode ever writes** â€” the second symptom of the
        // lost frame. It has to be an array of 8-byte texture records, since DrawSpritePrimitive
        // indexes it as `*(int*)sprite + sprite[4] * 8`; the menu's equivalent comes from the
        // resource directory (`*(*(DAT_005e97c4 + 0x94) + 4)`). Left null rather than guessed, and
        // the two draws are skipped while it is: a blank screen for the 150 frames is a visible,
        // harmless degradation, whereas an invented pointer would fault or draw garbage.
        bool spriteResolved = *(int*)sprite != 0;

        // Ghidra: iVar5 is a three-state gate â€” -1 until nothing is held (so a button carried in
        // from the previous screen cannot skip this one), 0 armed, 1 pressed and acknowledged.
        int phase = -1;
        int framesLeft = 0x96;

        while (framesLeft != 0 || phase != 0)
        {
            if (spriteResolved)
            {
                primCursor = DrawSpritePrimitive(otEntry, primCursor, (int*)sprite, 0, 0);
                primCursor = DrawFlatPrimitive(otEntry, primCursor, (int*)sprite);
            }
            FrameEnd(&otBase, &otEntry, &primCursor, 1, 1);

            // Ghidra: START (0x0008) or SQUARE (0x8000) of the current player's pad word. The word
            // at DAT_00604a20 is **active high** â€” UpdateButtonEdges inverts the active-low word
            // FUN_00523660 builds â€” so a set bit is a held button.
            bool held = (CurrentPadWord() & 0x8008) != 0;

            if (!held && phase == -1) phase = 0;

            if (!held)
            {
                if (phase == 1) return;
            }
            else if (phase == 0)
            {
                phase = 1;
                PlayUiSound38();        // Ghidra: FUN_0053db80
            }

            framesLeft--;
        }
    }

    /// <summary>
    /// Ghidra: the pad-state expression shared by FUN_00512520, FUN_00545340, FUN_0052a440 and
    /// FUN_00506880. Factored because it had already been transcribed twice and drifted once: one
    /// copy hardcoded the outer index to 0 instead of reading DAT_005de538.
    ///
    /// <para><b>The stride is 0x10, not 4.</b> Ghidra's pseudo-C renders the final scaling as
    /// <c>&amp;DAT_00604a20 + slot * 4</c>, which is wrong — the decompiler lost the shift. All three
    /// call sites disassemble to <c>SHL EAX,0x4</c> followed by <c>ADD EAX,0x604a20</c>:
    /// <c>FUN_0052a440</c> at <c>0x0052a60b</c>, <c>FUN_00545340</c> at <c>0x00545423</c>, and
    /// <c>FUN_00506880</c> at <c>0x005069fa</c> (see <c>Legacy/Raw/Blaze/*.asm</c>). Each pad slot
    /// is a 16-byte record. The earlier <c>* 4</c> here was a latent 4× address error: it only
    /// agreed with the original at slot 0, which is the single-controller case everything has been
    /// exercised with so far.</para>
    /// </summary>
    private static byte* CurrentPadState()
    {
        int slot = s_PlayerSlots_005e9188[
            s_SlotArea_005e9180[0x248 + (uint)(s_Unknown_005de538 & 0xff) * 0x9c] * 0x4c];
        return s_InputStateBase_00604a20 + slot * 0x10;
    }

    /// <summary>
    /// Ghidra: the halfword at offset 0 of the current pad record — the form FUN_0052a440 reads
    /// (<c>MOV AX, word ptr [EAX]</c> at <c>0x0052a613</c>, then <c>AND AX,0x8008</c>).
    /// </summary>
    private static ushort CurrentPadWord() => *(ushort*)CurrentPadState();

    /// <summary>
    /// Ghidra: the loading screen's sprite descriptor â€” 20 bytes, **one contiguous block**. Split
    /// into two allocations it would draw nothing at all, which is exactly what happened to the
    /// main menu's background; see REVERSE_STATUS.md, 2026-08-14.
    /// </summary>
    private static readonly byte* s_LoadingScreenSprite = (byte*)NativeMemory.AllocZeroed(0x20);

    // Ghidra: DAT_005de528 â€” the area index game state 3 loads its screen from. Never assigned in
    // the reconstructed graph yet, so it reads 0.
    private static int s_Unknown_005de528;

    // =====================================================================
    // Game state 0 â€” the main menu (Ghidra: FUN_00542130 and its subtree).
    //
    // Rendering follows the PlayStation display-list model the rest of winsys3d already showed:
    // FrameBegin prepares a primitive buffer and an ordering table, each renderer links its
    // primitive into the OT and returns the advanced buffer cursor, and FrameEnd walks the OT and
    // swaps buffers. The OT pointer is threaded through return values â€” that threading *is* the
    // display list, so it is modelled as written rather than hidden behind a field.
    // =====================================================================

    // Ghidra: DAT_005e9300 â€” which of the two draw envs is current, i.e. the back/front selector.
    private static byte* s_CurrentDrawEnv;
    private static int s_Unknown_005e8310;
    // Ghidra: DAT_005e9818 — le **budget de primitives par frame**, identifié en reconstruisant
    // FUN_0051d1f0. FrameBegin le recharge à 0x9c4 (2500) et chaque primitive émise le décrémente ;
    // à zéro, les boucles d'émission s'arrêtent net. Il n'est écrit nulle part ailleurs, et son
    // adresse tombe dans la queue non initialisée de .data — donc sans la remise à 2500 de
    // FrameBegin, plus rien ne se dessinerait du tout.
    private static int s_Unknown_005e9818;
    private static uint s_FadeCounter_005e9960;      // Ghidra: DAT_005e9960
    private static uint s_Unknown_005e9814;
    private static int s_Unknown_005de154, s_Unknown_005de158, s_Unknown_005de15c;
    private static int s_Unknown_005de51c;
    /// <summary>
    /// The menu's selected entry â€” <c>menuContext[0x8c4]</c>, i.e. address 0x0056e61c.
    /// <para>
    /// **Ghidra names `DAT_005e909f` as the switch selector here, and that is wrong.** Two facts
    /// rule it out: that global is written in exactly two places in all of `Blaze.c`, both `= 0`
    /// and both in unrelated entity/sound code, so the switch could only ever pick case 0; and a
    /// per-frame diff of the descriptor (`OPENFORESIA_TRACE_MENU=1`) shows **offset 0x8c4** stepping
    /// 0 â†’ 1 â†’ 2 in exact time with the arrow presses that move the cursor on screen.
    /// </para>
    /// <para>
    /// 0x0056e61c is three bytes before `DAT_0056e61f`, the flags byte the same `if` reads and
    /// which was itself found to be `menuContext[0x8c7]` â€” one dword, two fields. Ghidra never
    /// named 0x0056e61c at all, because nothing reaches it as an absolute address; it is only ever
    /// `descriptor + 0x8c4`. Same shape of defect as the four before it, and the sixth overall.
    /// </para>
    /// </summary>
    private static int s_MenuSelection_0056e61c => s_MenuContext_0056dd58[0x8c4];
    // Ghidra names DAT_0056e61f as its own symbol, but it is **inside** the menu context:
    // 0x0056e61f - 0x0056dd58 = 0x8c7, the descriptor's flags byte. Modelling it separately made
    // the confirm gate always open, so the menu selected an entry on its very first frame. Bit 2
    // is the one Handler_00544b10 raises when the text stream ends.
    private static byte s_Unknown_0056e61f => s_MenuContext_0056dd58[0x8c7];
    /// <summary>
    /// Ghidra: DAT_005d9de0 â€” **le mÃªme global que <see cref="s_GammaIni"/>**, la clÃ© Â« Gamma Â» de
    /// l'ini, et non un second. Le commentaire de LoadOptionsIni le disait dÃ©jÃ  ; il avait quand
    /// mÃªme Ã©tÃ© redÃ©clarÃ© ici, et n'Ã©tant jamais Ã©crit il valait zÃ©ro. ConsÃ©quence visible : la
    /// page d'options passait ses palettes dans <c>Gamma(valeur, 0.0)</c>, ce qui les rendait
    /// noires â€” fond et cadre invisibles, libellÃ©s intacts parce qu'eux n'y passent pas.
    /// SeptiÃ¨me occurrence d'un global reconstruit sous deux noms.
    /// </summary>
    private static int s_Unknown_005d9de0 => (int)s_GammaIni;

    private static int s_Unknown_00564a48, s_Unknown_00571a3c;

    // Ghidra: DAT_00564a4c — le second drapeau des deux émetteurs de sprite. Jamais écrit par le
    // code reconstruit pour l'instant : FUN_00537ca0 le pose autour de FUN_00537da0 (état 2), qui
    // n'est pas reconstruit. Voir DrawSpriteVariant pour la divergence avec s_SmallTextureFlag.
    private static int s_Unknown_00564a4c;

    /// <summary>
    /// Ghidra: FUN_0052a1e0 @ 0x0052a1e0 â€” begins a frame: picks the scratch bases out of MainMem
    /// according to which draw env is current, clears a 1024-entry ordering table
    /// (<c>Ordinal_688</c>, libgpu's ClearOTagR) and resets the primitive cursor.
    /// <para>
    /// Ghidra recovers only two parameters where the call sites pass three, and renders both
    /// writes as <c>*param_1</c>. **Confirmed against the disassembly**
    /// (`Legacy/Raw/FUN_0052a1e0.asm`): the function has no prologue, so its arguments sit at
    /// `[ESP+4]`, `[ESP+8]` and `[ESP+0xc]`, and `0052a257 MOV ECX,[ESP+0xc]` shows the second
    /// write targets **arg3** â€” the primitive buffer. Plain `RET` with the caller's `ADD ESP,0xc`
    /// makes it cdecl with three arguments.
    /// </para>
    /// </summary>
    private static void FrameBegin(uint** otBase, uint** otEntry, uint** primCursor)
    {
        // Every caller uses all three out-parameters immediately, so they must be written on every
        // path. Returning early without setting them left uninitialised stack in the caller and
        // took DrawOTag down with a null-page access â€” a defect in this reconstruction, not in the
        // original, which has no such early exit.
        *otBase = null;
        *otEntry = null;
        *primCursor = null;

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "FUN_0052a1e0: winsys3d.dll's MainMem export resolves to null, so the frame's " +
                "ordering table and primitive buffer have nowhere to live â€” see REVERSE_STATUS.md " +
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

        // Ghidra: FUN_00533000(DAT_005e97c0) â€” palette animation, once per frame.
        AnimatePalettes(s_MainResourceBuffer_005e97c0);
    }

    /// <summary>
    /// Ghidra: FUN_0052a280 @ 0x0052a280 â€” ends a frame: advances the frame counter, swaps the
    /// draw/display env pair, walks the ordering table and immediately begins the next frame.
    /// <para>
    /// Ghidra recovers **no** parameters for this function, yet every call site passes five. The
    /// mapping below is **confirmed against the disassembly** (`Legacy/Raw/FUN_0052a280.asm`):
    /// tracing `SUB ESP,0x14` plus two `PUSH`es puts the arguments at `[ESP+0x20]`..`[ESP+0x30]`,
    /// giving `Ordinal_701(*arg2)`, `Ordinal_132(arg4)`, the guard on `arg5`, and the tail call
    /// `FUN_0052a1e0(arg1, arg2, arg3)` â€” cdecl, three arguments, `ADD ESP,0xc`.
    /// </para>
    /// </summary>

    private static void FrameEnd(uint** otBase, uint** otEntry, uint** primCursor, int mode, int flag)
    {
        CrtRand();                             // Ghidra: FUN_00547140() â€” result discarded here

        s_FadeCounter_005e9960++;

        int cosine = GetCosTableEntry((int)(s_FadeCounter_005e9960 * 0x200));
        sbyte level = (sbyte)((cosine >> 6) - 0x80);
        s_Unknown_005e9814 = (uint)(((byte)level << 16) | ((byte)level << 8) | (byte)level);

        Ordinal_667(0);

        // Ghidra: puVar3 = &DAT_005e9880; if (DAT_005e9300 == &DAT_005e9880) puVar3 = &DAT_005e98f0;
        // â€” the buffer swap: pick the *other* draw env, then publish its matching disp env at +0x5c.
        byte* nextDrawEnv = s_CurrentDrawEnv == s_DrawEnv0_005e9880
            ? s_DrawEnv1_005e98f0
            : s_DrawEnv0_005e9880;
        s_CurrentDrawEnv = nextDrawEnv;

        Ordinal_657(nextDrawEnv + 0x5c);       // the DISPENV sits 0x5c past the DRAWENV
        Ordinal_660(nextDrawEnv);

        FlushDeferredImageUploads();           // Ghidra: FUN_00545140()

        Ordinal_701(*otEntry);                 // DrawOTag â€” asm: Ordinal_701(*arg2)

        // asm 0052a314: MOV ECX,[ESP+0x2c] â€” that slot is **arg4**, not arg5. The `if` below tests
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

    /// <summary>Ghidra: FUN_00529a80 @ 0x00529a80 â€” screen/viewport setup for the menu state.</summary>
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

    // Ghidra: &DAT_005e8f00 â€” the menu state's own tpage/clut slot table, distinct from the
    // 0x005e8fc0 one LoadFirstLevelResources fills. Same 4-byte-per-entry layout.
    private static readonly byte* s_EntityDecodeTable_005e8f00 = (byte*)NativeMemory.AllocZeroed(0x1000);

    // Ghidra: DAT_005e97c8 â€” the tail pointer LoadResourceEntry returns for the state's own load.
    private static byte* s_Unknown_005e97c8;

    // winsys3d.dll named exports (ordinal 776 and its setter) â€” the viewport shift the options page
    // saves and restores around its own screen.
    [DllImport("winsys3d.dll")] private static extern void GetAbsShift(int* x, int* y);
    [DllImport("winsys3d.dll")] private static extern void SetAbsShift(int x, int y);

    // Ghidra: _DAT_0054d074 â€” the float the gamma value is scaled by before CreateTranslationTables.
    // TODO(REVERSE): .rdata content; 1.0f keeps the product equal to the raw setting.
    private const float s_GammaScale_0054d074 = 1.0f;

    // Ghidra: PTR_s____Music_Mel00_01_WAV_00566cc8 â€” the music filename table. Entries recovered
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

    // =====================================================================
    // Game state 4 — la sélection des personnages (Ghidra: FUN_005066d0)
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_005066e0 @ 0x005066e0 — le chargement des ressources de l'écran. Même entame que
    /// <see cref="LoadMenuResources"/> — l'entrée de l'état puis la relocalisation de son en-tête —
    /// suivie de **quatre** listes du répertoire au lieu d'une :
    ///
    /// <list type="bullet">
    /// <item><c>+0x10</c> : les TIM, décodées vers la VRAM émulée, comme partout ailleurs.</item>
    /// <item><c>+0x2c</c> : des paquets de modèles, relocalisés puis repaginés.</item>
    /// <item><c>+0x7c</c> : des paquets d'animations, relocalisés.</item>
    /// <item><c>+0xb0</c> : des blocs bruts, chargés bout à bout tous les 0x2000 octets.</item>
    /// </list>
    ///
    /// Écart assumé avec l'original : les gardes <c>null</c> sur le répertoire et sur
    /// <c>MainMem</c> ci-dessous sont un ajout. <c>FUN_005066e0</c> déréférence les deux sans filet.
    ///
    /// Écart assumé n°2 : <c>FUN_0040a600()</c>, appelé entre la mise à 1 des drapeaux et le
    /// chargement de l'en-tête, n'est pas transcrit. Son corps décompilé est **vide**
    /// (<c>Blaze.c:4319</c>), donc l'omission est comportementalement neutre — mais la signature
    /// que Ghidra lui recompose est fausse, puisque d'autres sites l'appellent avec des arguments
    /// (<c>FUN_0040a600(0x25)</c>, <c>FUN_0040a600(4)</c>). Vraisemblablement un stub libgpu vidé
    /// au portage depuis la PlayStation.
    /// </summary>
    private static void LoadCharacterSelectResources()
    {
        for (int i = 0; i < 9; i++) s_SelectState_00578de8[i] = 0;
        for (int i = 0; i < 3; i++) s_SelectFlags_00562a48[i] = 1;

        s_Unknown_005e97c8 = LoadResourceEntry((int*)(nint)s_Unknown_005e92b0, s_MainResourceDirectory_005e97c4);
        FixupResourceHeader(s_MainResourceDirectory_005e97c4);

        byte* directoryBase = s_MainResourceDirectory_005e97c4;
        byte* mainMem = GetMainMemPtr();
        if (directoryBase == null || mainMem == null) return;

        // Ghidra: la liste +0x10 — chaque entrée est chargée dans le tampon de travail puis décodée
        // dans son emplacement de la table de pages.
        int* textures = *(int**)(directoryBase + 0x10);
        if (textures != null)
        {
            byte* scratch = mainMem + 0x1b0000;
            byte* slot = s_EntityDecodeTable_005e8f00;
            for (int* entry = textures; *entry != 0; entry += 2, slot += 4)
            {
                LoadResourceEntry((int*)(nint)(*entry), scratch);
                DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
            }
        }

        // Ghidra: la liste +0x2c — les paquets de modèles. Chaque chargement rend la fin du bloc,
        // qui devient la destination du suivant : ils s'empilent sans trou.
        byte* cursor = mainMem + 0x138000;
        int* models = *(int**)(directoryBase + 0x2c);
        if (models != null)
        {
            byte** table = (byte**)s_ModelTable_005e96a0;
            for (int* entry = models; *entry != 0; entry += 2, table++)
            {
                byte* next = LoadResourceEntry((int*)(nint)(*entry), cursor);
                RelocateModelBundle((int*)cursor, (byte*)entry, table);
                cursor = next;
            }
        }

        // Ghidra: la liste +0x7c — les paquets d'animations, empilés à la suite des précédents.
        // Le curseur est partagé avec la boucle ci-dessus : si la liste +0x2c était vide, la
        // première animation atterrit bien à MainMem + 0x138000.
        int* animations = *(int**)(directoryBase + 0x7c);
        if (animations != null)
        {
            byte** table = (byte**)s_AnimationTable_005e9640;
            for (int* entry = animations; *entry != 0; entry++, table++)
            {
                byte* next = LoadResourceEntry((int*)(nint)(*entry), cursor);
                RelocateAnimationBundle((int*)cursor, table);
                cursor = next;
            }
        }

        // Ghidra: la liste +0xb0 — des blocs bruts, un tous les 0x2000 octets, sans relocalisation.
        int* raw = *(int**)(directoryBase + 0xb0);
        if (raw != null)
        {
            byte* destination = mainMem + 0xf8000;
            for (int* entry = raw; *entry != 0; entry++, destination += 0x2000)
                LoadResourceEntry((int*)(nint)(*entry), destination);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00532640 @ 0x00532640 — un paquet de modèles : un compte, puis autant d'offsets
    /// relatifs suivis d'autant de descripteurs de 8 octets. Chaque offset devient un pointeur
    /// absolu, et chaque entrée reçoit ensuite sa relocalisation propre et sa repagination.
    ///
    /// Le pointeur publié dans <paramref name="result"/> vise le **tableau d'offsets**, pas le
    /// compte : c'est ce qui rend le compte lisible à <c>[-4]</c>, comme le fait le code de rendu
    /// en <c>Blaze.c:79042</c>.
    /// </summary>
    private static int RelocateModelBundle(int* bundle, byte* metadata, byte** result)
    {
        uint pageBits = *(uint*)(metadata + 4);
        int count = bundle[0];
        int* offsets = bundle + 1;
        int* descriptors = bundle + count;

        *result = (byte*)offsets;

        for (int i = count; i != 0; i--, offsets++, descriptors++)
        {
            int absolute = (int)(nint)bundle + *offsets + count * 8 + 4;
            *offsets = absolute;
            descriptors[1] |= (int)pageBits;
            RelocateModelEntry((ushort*)(nint)absolute, (byte*)descriptors);
            AdjustModelTexturePages((byte*)(nint)absolute);
        }

        return count;
    }

    /// <summary>
    /// Ghidra: FUN_00532450 @ 0x00532450 — une entrée de modèle. Trois pointeurs simples, deux
    /// tables d'entiers (l'une de taille variable, l'autre de 0x400) et une liste terminée par -1
    /// dont la longueur est rangée dans l'entrée. Les deux derniers champs sont divisés par trois —
    /// la multiplication par 0x55555556 de Ghidra.
    ///
    /// Cette division par trois est l'un des deux indices qui font de cette structure un modèle et
    /// non un paquet de sprites : trois sommets par triangle. L'autre est la repagination de
    /// <see cref="AdjustModelTexturePages"/>.
    ///
    /// Le <c>0x55555556</c> de Ghidra est l'idiome de division signée par 3, dont la correction de
    /// signe est ici inutile : <c>entry[4]</c> et <c>entry[5]</c> sont des <c>ushort</c>, donc
    /// <c>n + 2</c> est toujours positif et l'idiome se réduit exactement à <c>/ 3</c>.
    ///
    /// Attention à l'échelle des indices : Ghidra type ce paramètre <c>ushort*</c> ici et <c>int</c>
    /// dans <c>FUN_00532530</c>, alors que les deux reçoivent le **même** pointeur. Les décalages
    /// ci-dessous sont donc en <c>ushort</c> (<c>entry + 0x12</c> = octet 0x24), ceux de
    /// <see cref="AdjustModelTexturePages"/> en octets.
    /// </summary>
    private static void RelocateModelEntry(ushort* entry, byte* descriptor)
    {
        byte* baseAddress = (byte*)entry;

        *(int*)(entry + 0x12) += (int)(nint)baseAddress;
        *(uint*)(entry + 0x1e) = *(uint*)(descriptor + 4);
        *(int*)(entry + 0x14) += (int)(nint)baseAddress;
        *(int*)(entry + 0x16) += (int)(nint)baseAddress;

        if (*(int*)(entry + 0x18) != 0)
        {
            int* table = (int*)(nint)(*(int*)(entry + 0x18) + (int)(nint)baseAddress);
            *(int**)(entry + 0x18) = table;
            for (uint n = entry[0]; n != 0; n--, table++) *table += *(int*)(entry + 0x18);
        }

        if (*(int*)(entry + 0x1a) != 0)
        {
            int* table = (int*)(nint)(*(int*)(entry + 0x1a) + (int)(nint)baseAddress);
            *(int**)(entry + 0x1a) = table;
            for (int n = 0x400; n != 0; n--, table++) *table += *(int*)(entry + 0x1a);
        }

        ushort length = 0;
        if (*(int*)(entry + 0x1c) != 0)
        {
            int* list = (int*)(nint)((int)(nint)baseAddress + *(int*)(entry + 0x1c));
            *(int**)(entry + 0x1c) = list;
            for (; *list != -1; list++, length++)
                if (*list != 0) *list += *(int*)(entry + 0x1c);
        }

        entry[7] = length;
        entry[4] = (ushort)((entry[4] + 2) / 3);
        entry[5] = (ushort)((entry[5] + 2) / 3);
    }

    /// <summary>
    /// Ghidra: FUN_00532530 @ 0x00532530 — repagine une entrée : le décalage de page, pris dans les
    /// bits 8..15 du champ 0x3c, est ajouté à deux octets de chaque enregistrement de 16 octets. Le
    /// nombre d'enregistrements et leur origine se lisent dans les huit compteurs de l'en-tête —
    /// quatre à sauter (0x10..0x16), quatre à traiter (0x18..0x1e), un par type de primitive.
    ///
    /// Des enregistrements de 16 octets rangés par type de primitive, dont deux octets portent une
    /// page de texture : c'est la disposition d'un modèle PSX, ce qui confirme le rôle de la table
    /// que <see cref="RelocateModelBundle"/> remplit.
    ///
    /// Le champ 0x3c est celui que <see cref="RelocateModelEntry"/> vient d'écrire depuis le
    /// descripteur, après que <see cref="RelocateModelBundle"/> y a fusionné les bits de page de la
    /// liste. Les trois fonctions se referment sur la même convention.
    /// </summary>
    private static void AdjustModelTexturePages(byte* entry)
    {
        uint adjust = (*(uint*)(entry + 0x3c) >> 8) & 0xc0;

        int count = *(ushort*)(entry + 0x1e) + *(ushort*)(entry + 0x1c)
                  + *(ushort*)(entry + 0x1a) + *(ushort*)(entry + 0x18);
        if (count == 0) return;

        int skipped = *(ushort*)(entry + 0x16) + *(ushort*)(entry + 0x14)
                    + *(ushort*)(entry + 0x12) + *(ushort*)(entry + 0x10);

        uint* record = (uint*)(nint)(skipped * 0x10 + *(int*)(entry + 0x28) + 0xc);
        for (; count != 0; count--, record += 4)
        {
            uint high = record[0];
            uint low = record[-1];
            record[-1] = (((((low >> 0x18) + adjust) & 0xff) << 0x10)
                          | (((low >> 8) + adjust) & 0xff)) << 8 | (low & 0xff00ff);
            record[0] = (((((high >> 0x18) + adjust) & 0xff) << 0x10)
                         | ((((high >> 8) & 0xff) + adjust) & 0xff)) << 8 | (high & 0xff00ff);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005326d0 @ 0x005326d0 — un paquet d'animations : même forme que celui des
    /// modèles, sans descripteurs, et huit pointeurs à relocaliser par entrée.
    ///
    /// Comme pour les modèles, le pointeur publié vise le tableau d'offsets et non le compte, que
    /// <c>Blaze.c:79042</c> relit à <c>[-4]</c>.
    /// </summary>
    private static void RelocateAnimationBundle(int* bundle, byte** result)
    {
        int count = bundle[0];
        int stride = count * 4;
        int* offsets = bundle + 1;

        *result = (byte*)offsets;

        for (; count != 0; count--, offsets++)
        {
            int absolute = (int)(nint)bundle + *offsets + stride + 4;
            *offsets = absolute;
            RelocateAnimationEntry((int*)(nint)absolute);
        }
    }

    /// <summary>Ghidra: FUN_005326b0 @ 0x005326b0 — huit pointeurs, tous relatifs à l'entrée.</summary>
    private static void RelocateAnimationEntry(int* entry)
    {
        int origin = entry[0];
        int* slot = entry + 8;
        for (int i = 8; i != 0; i--, slot++) *slot += (int)(nint)entry + origin;
    }

    // Ghidra: DAT_00578de8..DAT_00578e08 — neuf entiers **contigus**, remis à zéro d'un bloc au
    // début de FUN_005066e0 ; et DAT_00562a48..DAT_00562a50, trois autres mis à 1. Déclarés comme
    // les blocs qu'ils sont, plutôt qu'en douze champs isolés.
    private static readonly int* s_SelectState_00578de8 = (int*)NativeMemory.AllocZeroed(9, sizeof(int));
    private static readonly int* s_SelectFlags_00562a48 = (int*)NativeMemory.AllocZeroed(3, sizeof(int));

    // Ghidra: &DAT_005e96a0 et &DAT_005e9640 — les deux tables de pointeurs que les relocalisations
    // ci-dessus remplissent, une entrée par paquet chargé.
    //
    // Ces tables ne sont **pas** propres à la sélection des personnages, malgré le fait que
    // FUN_005066e0 soit le premier site qui les remplisse à être reconstruit. FUN_005069e0 les
    // remplit exactement de la même manière (Blaze.c:78876), et une trentaine de sites les indexent
    // dans tout Blaze.c — 2043, 14900, 19190, 23424, 25455, 52332, 64765, 79042 — le plus souvent
    // par un octet pris dans une entité (`*(byte*)(param_1 + 4) * 4`). D'où le nom générique.
    //
    // Leur rôle respectif, établi par les sites de lecture :
    //   005e9640 — animations. En Blaze.c:55840 un compteur de frame est incrémenté puis comparé au
    //              champ +0x16 de l'entrée et remis à zéro : c'est la longueur de l'animation.
    //   005e96a0 — modèles. FUN_0051d1f0 reçoit les deux ensemble (Blaze.c:55845) : l'entrée
    //              d'animation indexée par la frame, et le paquet de modèles en regard.
    //
    // Capacité : 0x005e96a0 - 0x005e9640 = 0x60 octets, soit 24 pointeurs en 32 bits. Les deux
    // symboles tombent dans la queue non initialisée de .data, donc il n'y a aucun contenu à
    // récupérer du binaire — seulement cette borne à respecter.
    private const int AssetTableCapacity = 24;
    private static readonly byte* s_ModelTable_005e96a0 = (byte*)NativeMemory.AllocZeroed((nuint)AssetTableCapacity, (nuint)sizeof(nint));
    private static readonly byte* s_AnimationTable_005e9640 = (byte*)NativeMemory.AllocZeroed((nuint)AssetTableCapacity, (nuint)sizeof(nint));

    /// <summary>
    /// Ghidra: FUN_005066d0 @ 0x005066d0 — le point d'entrée de l'état 4, la sélection des
    /// personnages. Deux moitiés, comme <see cref="RunMainMenuState"/> : le chargement puis la
    /// boucle de frame.
    /// </summary>
    private static void RunCharacterSelectState()
    {
        LoadCharacterSelectResources();   // Ghidra: FUN_005066e0()
        RunCharacterSelectFrameLoop();    // Ghidra: FUN_00506880()
    }

    /// <summary>
    /// Ghidra: FUN_00506880 @ 0x00506880 — la boucle de frame de l'écran. Le prologue ci-dessous est
    /// intégralement déterminable et transcrit tel quel ; la boucle elle-même ne l'est pas encore,
    /// pour deux raisons distinctes qu'il faut garder séparées :
    ///
    /// <para><b>1. Les huit sous-états ne sont pas dans <c>Blaze.c</c>.</b> La boucle répartit
    /// chaque frame par <c>(*(code *)(&amp;PTR_LAB_00560708)[DAT_00578de8])(...)</c>. Les huit
    /// cibles ont été relevées dans <c>Legacy/Blaze.exe</c> (décalage fichier <c>0x15ED08</c>) et
    /// sont déclarées en talons ci-dessous — mais aucun de leurs corps n'a été décompilé : la
    /// décompilation saute de <c>FUN_00506880</c> à <c>FUN_00507870</c>. Rien ne les appelle
    /// directement, seule cette table les désigne, donc Ghidra n'a jamais désassemblé ces régions.
    /// C'est la classe « corps uniquement adressés » : il faut un re-run Ghidra ciblé sur les huit
    /// adresses, pas une relecture de <c>Blaze.c</c>.</para>
    ///
    /// <para><b>2. Ghidra a raté les paramètres de <c>FUN_00506880</c> elle-même.</b> Son corps
    /// utilise <c>unaff_EBX</c>, <c>unaff_EBP</c> et <c>unaff_ESI</c>, et les passe tels quels à
    /// <c>FUN_0051d1f0</c>, <c>FUN_0051be20</c>, <c>FUN_00545340</c>, <c>FUN_00521f20</c> et
    /// d'autres. Ces sites d'appel ne sont pas transcriptibles sans re-run : inventer une signature
    /// violerait la règle du projet.</para>
    ///
    /// <para>Reste, indépendamment du re-run, une liste de fonctions de rendu non reconstruites que
    /// la boucle appelle : <c>FUN_0051d1f0</c> (le rendu de modèle animé), <c>FUN_0051be20</c> /
    /// <c>FUN_0051bdb0</c>, <c>FUN_00521df0</c> / <c>FUN_00521f20</c>, <c>FUN_0051f9c0</c>,
    /// <c>FUN_00545340</c>, <c>FUN_00544350</c>, <c>FUN_00520ae0</c>.</para>
    /// </summary>
    private static void RunCharacterSelectFrameLoop()
    {
        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);   // Ghidra: FUN_0052a1e0(&local_1c,&local_18,&local_20)

        // Ghidra: Ordinal_379(400); Ordinal_378(0xa0,0x78) — libgte SetGeomScreen / SetGeomOffset.
        // (0xa0, 0x78) = (160, 120), le centre d'un écran 320x240 : la projection est recentrée sur
        // l'écran avant que quoi que ce soit ne soit projeté.
        Ordinal_379(400);
        Ordinal_378(0xa0, 0x78);

        PlayMusicTrack(4);   // Ghidra: FUN_00529ad0(4)

        InitialiseCharacterSelectSlots();

        // Ghidra: DAT_00573338 = MainMem + 0xf0000, puis 0x2000 octets remis à zéro d'un bloc.
        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "RunCharacterSelectFrameLoop: winsys3d.dll's MainMem export resolves to null, so " +
                "the state's 0x2000-byte scratch block at MainMem + 0xf0000 cannot be placed.");
        }
        s_SelectScratch_00573338 = mainMem + 0xf0000;
        NativeMemory.Clear(s_SelectScratch_00573338, 0x2000);

        JoystickResetState(false);   // Ghidra: FUN_00504b10(0)

        // Ghidra: la sortie normale passe par `DAT_005de510 = 0` avant LAB_00506fe7 ; la sortie par
        // sous-état -1 y saute directement (`goto LAB_00506fe7`) et laisse donc l'état du jeu
        // intact. Le drapeau reproduit cette différence, que le `goto` de l'original masque.
        bool resetGameState = true;

        while (true)
        {
            s_Unknown_005de15c = 1;

            if (s_SmallTextureFlag == 0 && s_SizeSetting_005de160 > 7)
            {
                s_Unknown_005de158 = 0;
                s_Unknown_005de154 = 0;
            }
            else
            {
                s_Unknown_005de154 = 0xa0;
                s_Unknown_005de158 = (0xb - s_SizeSetting_005de160) * 0xc;
            }

            // Ghidra: FUN_0040a600(4) — corps vide, voir LoadCharacterSelectResources.

            byte* pad = CurrentPadState();

            // Ghidra: le test de sortie. Les deux bits 9 = 0b1001 du champ +2, sauf au sous-état 7
            // tant que DAT_00562b68 est positif.
            if ((pad[2] & 9) == 9
                && (s_SelectState_00578de8[0] != 7 || s_Unknown_00562b68 < 0))
            {
                break;
            }

            if (s_SelectState_00578de8[1] != s_SelectState_00578de8[0])
            {
                s_SelectState_00578de8[1] = s_SelectState_00578de8[0];
                s_SelectFlags_00562a48[0] = 1;
                s_SelectFlags_00562a48[1] = 1;
            }

            primCursor = InvokeCharacterSelectSubState(
                s_SelectState_00578de8[0], otBase, otEntry, primCursor, pad);

            if (s_SelectState_00578de8[0] == -1)
            {
                resetGameState = false;
                break;
            }

            s_SelectFlags_00562a48[1] = 0;

            // =========================================================================
            // Le bloc « un personnage est en scène », sous-états 1 à 6.
            //
            // Les globales que Ghidra nomme DAT_00572b4c, 52, 60, 62, 64..6c et 70..74 ne sont
            // pas un bloc à part : ce sont les champs de s_SceneEntity_00572b48, à +0x04, +0x0a,
            // +0x18, +0x1a, +0x1c..+0x24 et +0x28..+0x2c. Six sur six correspondent au même
            // enregistrement de 0x9c octets qu'un emplacement du carrousel — ce qui est attendu,
            // puisque la validation y recopie l'emplacement choisi.
            // =========================================================================
            if (s_SelectState_00578de8[0] != 0 && s_SelectState_00578de8[0] < 7)
            {
                byte* entity = s_SceneEntity_00572b48;
                int* scenePos = (int*)(entity + 0x1c);     // DAT_00572b64 / 68 / 6c
                short* sceneGlow = (short*)(entity + 0x28); // DAT_00572b70 / 72 / 74

                // Les particules et la teinte de fond ne concernent que les sous-états 5 et 6.
                if (s_SelectState_00578de8[0] > 4)
                {
                    // Ghidra: &DAT_00562a58 indexé par l'octet +0x151 du bloc de travail.
                    short* tone = (short*)(s_SceneToneTable_00562a58
                                           + s_SelectScratch_00573338[0x151] * SceneToneStride);
                    s_BackColourR_005e93a0 += (short)((tone[0] - s_BackColourR_005e93a0) >> 2);
                    s_BackColourG_005e93a2 += (short)((tone[1] - s_BackColourG_005e93a2) >> 2);
                    s_BackColourB_005e93a4 += (short)((tone[2] - s_BackColourB_005e93a4) >> 2);
                    Ordinal_376((byte)s_BackColourR_005e93a0,
                                (byte)s_BackColourG_005e93a2,
                                (byte)s_BackColourB_005e93a4);

                    primCursor = EmitSceneParticles(otBase, primCursor);

                    // Ghidra: Ordinal_457 est l'atan2 de libgte. La scène se tourne vers la
                    // première particule, l'angle étant pris entre elle et l'entité.
                    s_ScenePitch_005e9308 = 0;
                    // DAT_00572c00 et DAT_00572c08 sont le x et le z de la **position** du premier
                    // record, donc sa base + 0x1c.
                    int* firstParticle = (int*)(s_SceneParticles_00572be4 + 0x1c);
                    short dz = (short)((scenePos[2] - firstParticle[2]) >> 0xc);
                    short dx = (short)((scenePos[0] - firstParticle[0]) >> 0xc);
                    s_SceneYaw_005e930a = -Ordinal_457(dx, dz);
                    s_SceneRoll_005e930c = 0;
                }

                // L'éclat de l'entité remonte vers 0x1e00, sa position retombe vers (0, -0x64000, 0)
                // — toujours par quarts. Sous 0x1000 en valeur absolue, x et z sont collés à zéro
                // pour que l'amortissement finisse net plutôt qu'asymptotiquement.
                sceneGlow[0] += (short)((0x1e00 - sceneGlow[0]) >> 2);
                sceneGlow[1] += (short)((0x1e00 - sceneGlow[1]) >> 2);
                sceneGlow[2] += (short)((0x1e00 - sceneGlow[2]) >> 2);

                scenePos[0] += -scenePos[0] >> 2;
                scenePos[1] += -0x64000 - scenePos[1] >> 2;
                scenePos[2] += -scenePos[2] >> 2;

                if ((scenePos[0] < 0 ? -scenePos[0] : scenePos[0]) < 0x1000) scenePos[0] = 0;
                if ((scenePos[2] < 0 ? -scenePos[2] : scenePos[2]) < 0x1000) scenePos[2] = 0;

                // Avance de frame, exactement comme au carrousel : le compteur boucle sur la
                // longueur de l'animation, lue à +0x16 de son entrée.
                byte assetIndex = entity[0x04];                     // DAT_00572b4c
                short* sceneAnimIndex = (short*)(entity + 0x18);    // DAT_00572b60
                short* sceneFrame = (short*)(entity + 0x1a);        // DAT_00572b62

                byte* bundle = ((byte**)s_AnimationTable_005e9640)[assetIndex];
                byte* animEntry = *(byte**)(bundle + *sceneAnimIndex * 4);

                short advanced = (short)(*sceneFrame + 1);
                *sceneFrame = advanced;
                if (*(ushort*)(animEntry + 0x16) <= advanced) *sceneFrame = 0;

                // Ghidra: CONCAT22(sVar3 >> 0xf, DAT_00572b52) — la moitié haute est le signe du
                // compteur **avant** bouclage, la basse le champ +0x0a. Même panachage qu'au
                // carrousel, transcrit tel quel.
                uint sceneExtra = (uint)((advanced >> 0xf) << 0x10) | *(ushort*)(entity + 0x0a);

                primCursor = DrawAnimatedModel(
                    otBase, primCursor, animEntry,
                    (int*)(((byte**)s_ModelTable_005e96a0)[assetIndex]),
                    scenePos, *sceneFrame, sceneExtra);
            }

            // Ghidra: DAT_006008fc — la translation Z de la matrice de caméra, amortie vers 800.
            CameraTranslation[2] += (800 - CameraTranslation[2]) >> 2;

            // Ghidra: le descripteur de sprite de 20 octets, monté sur la pile — un seul bloc
            // contigu, comme s_LoadingScreenSprite. Le champ 0 vient du répertoire de ressources.
            byte* sprite = stackalloc byte[20];
            NativeMemory.Clear(sprite, 20);
            *(uint**)sprite = *(uint**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x94) + 0x30);
            *(uint*)(sprite + 4) = 0x1000000;

            // Ghidra: la rangée de descripteurs d'interface — DAT_00560ff0 et ses suivants, pas
            // 0x8c8 octets. Le compte est DAT_00562a50, que les handlers ajustent (3 au maximum
            // observé). Les coordonnées se lisent dans le descripteur et sont décalées de (0xc, 4).
            for (int i = 0; i < s_SelectFlags_00562a48[2]; i++)
            {
                byte* descriptor = s_UiDescriptors_00560ff0 + i * UiDescriptorStride;

                *(short*)(sprite + 8) = (short)(*(short*)(descriptor + 4) + 0xc);
                *(short*)(sprite + 0xa) = (short)(*(short*)(descriptor + 6) + 4);
                *(short*)(sprite + 0x10) =
                    (sbyte)s_SelectSubStateBytes_00562a98[s_SelectState_00578de8[0] * 4 + i];

                EmitViewportRect(otBase, &primCursor);                              // FUN_0051be20
                primCursor = DrawSpriteVariant(otBase, primCursor, (int*)sprite, 0, 0);  // FUN_00521df0
                primCursor = DrawFlatPrimitive(otBase, primCursor, (int*)sprite); // FUN_0051f9c0
                EmitClipRect(otBase, &primCursor);                                  // FUN_0051bdb0
                primCursor = DrawUiPanel(otBase, primCursor, descriptor);           // FUN_00545340
            }

            if (s_SmallTextureFlag == 0 && s_SizeSetting_005de160 > 7)
            {
                s_Unknown_005de158 = s_SizeSetting_005de160 * 0xc - 0x70;
            }
            else if (s_SizeSetting_005de160 < 1)
            {
                s_Unknown_005de158 -= 8;
            }
            else
            {
                s_Unknown_005de158 = (s_SizeSetting_005de160 + 9) * 0xc;
            }

            if (s_SelectState_00578de8[2] >= 0)
            {
                // Ghidra: FUN_00544350(directory[0x70][DAT_00578df0], 0, &DAT_00560728, 0)
                BindMenuLabels(
                    *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70)
                              + s_SelectState_00578de8[2] * 4),
                    0, s_StatusDescriptor_00560728, 0);
            }

            primCursor = RunTextEngine(primCursor, s_StatusDescriptor_00560728);       // FUN_005443a0
            primCursor = DrawUiPanel(otBase, primCursor, s_StatusDescriptor_00560728);

            s_Unknown_005de15c = 0;

            // Ghidra: le plein écran de fin de frame — (0xa0, 0x78) est le centre, (0x1420, 0x1149)
            // les deux mots de texture, et le champ 0 vient cette fois du premier slot de +0x94.
            *(short*)(sprite + 8) = 0xa0;
            *(short*)(sprite + 0xa) = 0x78;
            *(ushort*)(sprite + 0xc) = 0x1420;
            *(ushort*)(sprite + 0xe) = 0x1149;
            *(short*)(sprite + 0x10) = 0;
            *(uint**)sprite = **(uint***)(s_MainResourceDirectory_005e97c4 + 0x94);
            *(uint*)(sprite + 4) = 0x1000000;

            primCursor = DrawSpritePrimitive(otEntry, primCursor, (int*)sprite, 0, 0);
            DrawFlatPrimitive(otEntry, primCursor, (int*)sprite);

            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        }

        if (resetGameState) s_GameStateId = 0;

        // Ghidra: LAB_00506fe7 — les deux sorties se rejoignent ici.
        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        s_Unknown_005de15c = 0;
    }

    /// <summary>
    /// Ghidra: la boucle de particules de FUN_00506880, atteinte aux seuls sous-états 5 et 6.
    /// Chaque record subit une marche aléatoire bornée : trois axes, chacun poussé de ±0x2000 vers
    /// l'entité de scène et saturé à ±0x19000, puis la position intègre la vitesse.
    ///
    /// <para><b>Le corps est transcrit, l'émission ne l'est pas.</b> Le tirage, la saturation et
    /// l'intégration sont entièrement lisibles ; ce qui manque est l'appel de dessin,
    /// <c>FUN_00520ae0</c>, dont la signature n'est pas établie — voir le <c>throw</c> ci-dessous.
    /// Écrire la boucle sans son émission donnerait un rendu silencieusement faux, ce que le projet
    /// interdit ; l'écrire avec un trou nommé situe la lacune exactement.</para>
    /// </summary>
    private static uint* EmitSceneParticles(uint* otBase, uint* primCursor)
    {
        for (int i = 0; i < SceneParticleCount; i++)
        {
            // La boucle travaille sur le champ de position (+0x1c), pas sur la base du record.
            int* particle = (int*)(s_SceneParticles_00572be4 + i * SceneParticleStride + 0x1c);
            int* velocity = particle + 0xd;         // +0x50 du record, la vitesse des trois axes
            int* target = (int*)(s_SceneEntity_00572b48 + 0x1c);

            // Ghidra: trois blocs identiques, l'un par axe, à deux détails près — le second se
            // compare à -0x88001 au lieu de +0x3fff. Le tirage (FUN_00547140 = rand) décide du
            // sens, la borne 0x19000 de l'amplitude.
            ReadOnlySpan<int> bias = [0x3fff, -0x88001, 0x3fff];
            for (int axis = 0; axis < 3; axis++)
            {
                int roll = CrtRand();
                int v = velocity[axis];
                if (particle[axis] - target[axis] + bias[axis] - roll < 0)
                {
                    if (v < 0x19000) velocity[axis] = v + 0x2000;
                }
                else if (-0x19000 < v)
                {
                    velocity[axis] = v - 0x2000;
                }
            }

            particle[0] += velocity[0];
            particle[1] += velocity[1];
            particle[2] += velocity[2];

            // Ghidra: trois shorts à 0x640 puis un octet cyclique de période 6, pris sur le
            // compteur de frames décalé d'un bit.
            *(short*)((byte*)particle + 0x0c) = 0x640;
            *(short*)((byte*)particle + 0x0e) = 0x640;
            *(short*)((byte*)particle + 0x10) = 0x640;
            *((byte*)particle + 0x24) = (byte)((s_FadeCounter_005e9960 >> 1) % 6);

            throw new NotImplementedException(
                "FUN_00520ae0 (la passe de particules) : signature RÉSOLUE, corps non transcrit.\n" +
                "  uint* FUN_00520ae0(uint* otEntry, uint* prim, int* particle, uint flags, uint extra)\n" +
                "cdecl, cinq arguments — établi depuis l'assembleur : le site d'appel empile cinq " +
                "valeurs et nettoie par ADD ESP,0x14 ; le prologue (SUB ESP,0x18 + quatre PUSH = " +
                "0x28) les place en [ESP+0x2C..0x3C], et les cinq emplacements sont lus. " +
                "0x00520ea8 charge [ESP+0x30] et écrit la primitive à travers (c'est le curseur), " +
                "0x00520ec3 charge [ESP+0x38] et l'OR avec 0x2c (l'octet de commande GPU, donc " +
                "`flags`), 0x00520f5b charge [ESP+0x3C] dans l'idiome `((short)x >> 2) & 0xffc0 + " +
                "(x & 0xff)` (donc `extra`). C'est la même famille que DrawSpritePrimitive : une " +
                "primitive de dix dwords liée dans l'OT.\n" +
                "Re-décompilé avec la signature forcée par ~/ghidra_scripts/" +
                "ForceDecompileParticlePass.java, résultat dans Legacy/Raw/Blaze/FUN_00520ae0.c " +
                "(+ .asm). ATTENTION : même avec la signature forcée, Ghidra attribue mal les " +
                "variables dans le corps — il nomme `particle` ce qui est `extra` dans prim[3], " +
                "`primCursor` ce qui est `flags` dans prim[1], et rend otEntry en local " +
                "`iStack_4`. La transcription demande donc un recoupement ligne à ligne avec le " +
                "listing. Chemin atteint aux seuls sous-états 5 et 6.");
        }

        return primCursor;
    }

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_LAB_00560708)[DAT_00578de8])(...)</c> @ 0x00506a52.
    ///
    /// <para>La signature est <b>reconstruite depuis l'assembleur</b>, parce que le pseudo-C de
    /// Ghidra est faux ici : il rend les trois premiers arguments en <c>unaff_EBP</c>,
    /// <c>unaff_EBX</c>, <c>unaff_ESI</c>, alors que l'assembleur empile quatre valeurs et restaure
    /// par <c>ADD ESP,0x10</c> — donc cdecl à quatre arguments, et les trois premiers sont des
    /// <b>locaux de pile</b>, pas des registres. En suivant les décalages : <c>[E+0x14]</c>,
    /// <c>[E+0x18]</c> et <c>[E+0x10]</c>, qui sont exactement les trois out-params de
    /// <c>FUN_0052a1e0</c> — <see cref="FrameBegin"/>. La valeur de retour est réécrite dans
    /// <c>[E+0x10]</c>, le curseur de primitives.</para>
    ///
    /// <para>Le quatrième argument est le même pointeur de manette que le test de sortie vient
    /// d'utiliser (<c>PUSH EAX</c> en 0x00506a46). Le pseudo-C le rend <c>&amp;DAT_00604a20 + i * 4</c>
    /// alors que l'assembleur fait <c>SHL EAX,0x4</c> : voir <see cref="CurrentPadState"/>.</para>
    /// </summary>
    private static uint* InvokeCharacterSelectSubState(
        int index, uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        if ((uint)index >= (uint)s_CharacterSelectSubStateAddresses.Length)
        {
            throw new NotImplementedException(
                $"Character-select sub-state index {index} is outside PTR_LAB_00560708's eight " +
                "entries; the original would call through whatever the table's neighbour holds.");
        }

        if (index == 0) return CharacterSelectSubState0(otBase, otEntry, primCursor, pad);
        if (index == 1) return CharacterSelectSubState1(otBase, otEntry, primCursor, pad);
        if (index == 3) return CharacterSelectSubState3(otBase, otEntry, primCursor, pad);
        if (index == 4) return CharacterSelectSubState4(otBase, otEntry, primCursor, pad);

        throw new NotImplementedException(
            $"Character-select sub-state {index} (0x{s_CharacterSelectSubStateAddresses[index]:x8}) " +
            "is decompiled but not yet transcribed — see Legacy/Raw/Blaze/FUN_" +
            $"{s_CharacterSelectSubStateAddresses[index]:x8}.c. Signature: " +
            "uint* handler(uint* otBase, uint* otEntry, uint* primCursor, byte* pad).");
    }

    /// <summary>
    /// Ghidra: FUN_00507010 @ 0x00507010 — <c>PTR_LAB_00560708[0]</c>, le sous-état d'entrée : le
    /// carrousel de personnages.
    ///
    /// <para>Attention aux deux échelles sur le pointeur de manette. Ghidra le type <c>ushort *</c>,
    /// donc <c>in_stack_00000010[1]</c> et <c>[3]</c> sont les mots aux octets 2 et 6, tandis que
    /// <c>*(byte *)((int)in_stack_00000010 + 3)</c> est l'octet 3 — la moitié haute du premier. Les
    /// deux formes coexistent dans la même fonction ; elles sont distinguées ci-dessous par
    /// <c>padWords</c> et <c>pad</c>.</para>
    /// </summary>
    /// <summary>
    /// Ghidra: FUN_00507cb0 @ 0x00507cb0 — <c>PTR_LAB_00560708[1]</c>, la **saisie du nom** : un
    /// clavier virtuel de huit lignes que la manette parcourt, doublé de la frappe réelle que
    /// winsys3d met en file.
    ///
    /// <para><b>Signature reconstruite depuis l'assembleur</b>, comme pour la table de répartition.
    /// Ghidra ne récupère qu'un paramètre et rend les deux autres en <c>unaff_retaddr</c> et
    /// <c>uStack_8</c>. Le prologue est <c>SUB ESP,0x28</c> puis quatre <c>PUSH</c>, soit 0x38 :
    /// les arguments sont donc en <c>[ESP+0x3C..0x48]</c>, et les décalages observés dans le corps
    /// tombent juste — <c>[ESP+0x3C]</c> est <c>uStack_8</c> (otBase), <c>[ESP+0x44]</c> est
    /// <c>unaff_retaddr</c> (le curseur de primitives, huit accès), <c>[ESP+0x48]</c> est
    /// <c>param_1</c> (la manette). Le <c>RET</c> nu confirme cdecl.</para>
    ///
    /// <para>Les globales que Ghidra nomme <c>DAT_00578df4</c>, <c>df8</c>, <c>dfc</c> et
    /// <c>e00</c> sont les entrées 3 à 6 de <see cref="s_SelectState_00578de8"/> ; celles en
    /// <c>DAT_0057351c..3c</c> sont les champs du **premier** descripteur de
    /// <see cref="s_SelectSlots_00573518"/> ; <c>DAT_00572b7a</c> est le lacet de l'entité de
    /// scène. Aucune n'est un nouveau bloc.</para>
    /// </summary>
    private static uint* CharacterSelectSubState1(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = s_SelectScratch_00573338;
        int* state = s_SelectState_00578de8;   // [3] page, [4] colonne, [5] ligne, [6] longueur

        // Première frame du sous-état : on arme la frappe réelle, on coupe la traduction
        // manette→clavier, et on installe le descripteur de la boîte de saisie.
        if (s_SelectFlags_00562a48[0] != 0)
        {
            EnableKBInput(1);
            DisablePadKeyboard();              // Ghidra: FUN_00523530
            s_SelectFlags_00562a48[0] = 0;
            state[4] = 0;
            state[5] = 0;
            state[3] = 0;

            byte* box = s_SelectSlots_00573518;
            *(short*)(box + 0x04) = 0x28;      // DAT_0057351c
            *(short*)(box + 0x06) = 0x38;      // DAT_0057351e
            *(short*)(box + 0x08) = 0xe0;      // DAT_00573520
            *(short*)(box + 0x0a) = 0x68;      // DAT_00573522
            box[0x12] = 0x20;                  // DAT_00573528 + 2
            *(uint*)(box + 0x20) = 0x1070c28;  // DAT_00573538 — le mot de fonction « texte »
            *(uint*)(box + 0x24) = 0x1000000;  // DAT_0057353c — son activation
            s_SelectFlags_00562a48[2] = 2;
        }

        SettleSceneCamera(scratch);

        // Ghidra: `if (DAT_00562a4c != 0) goto LAB_0050820a` — pendant le fondu d'entrée du
        // sous-état, aucune entrée n'est lue ; on va directement au dessin.
        if (s_SelectFlags_00562a48[1] == 0)
        {
            int typed = GetKBChar();

            // Les deux chemins de sortie de l'original se rejoignent juste avant EnableKBInput(0) :
            // le drapeau rendu ici tient lieu de ce point de jonction, que le `goto` masquait.
            bool leaving = typed == 0
                ? HandlePadNameEntry(pad, scratch, state)
                : HandleTypedNameEntry(typed, scratch, state);

            if (leaving) EnableKBInput(0);
        }

        return DrawNameEntryScreen(otBase, primCursor, scratch, state);
    }

    /// <summary>
    /// Le bloc d'ouverture que **tous** les sous-états « un personnage est en scène » répètent mot
    /// pour mot : la caméra rejoint l'angle du personnage courant, la teinte de fond revient au
    /// gris 0x60, la pose devient une matrice, et l'entité de scène tourne vers le joueur.
    ///
    /// <para>Extrait parce que c'est une unité indépendante répétée telle quelle d'un sous-état à
    /// l'autre, pas pour découper un corps unique.</para>
    /// </summary>
    private static void SettleSceneCamera(byte* scratch)
    {
        // Un seizième de tour par cran, par le chemin court : au-delà d'un demi-tour on tourne
        // dans l'autre sens plutôt que de traverser tout le cercle.
        ushort delta = (ushort)(((scratch[0x150] - 8) * 0x100
                                 - *(short*)(s_CameraVector_00604928 + 2)) & 0xfff);
        short step = delta < 0x800
            ? (short)((short)delta >> 2)
            : (short)-((short)(0x1000 - (short)delta) >> 2);
        *(short*)(s_CameraVector_00604928 + 2) += step;

        s_BackColourR_005e93a0 += (short)((0x60 - s_BackColourR_005e93a0) >> 2);
        s_BackColourG_005e93a2 += (short)((0x60 - s_BackColourG_005e93a2) >> 2);
        s_BackColourB_005e93a4 += (short)((0x60 - s_BackColourB_005e93a4) >> 2);

        // Ghidra: FUN_0051c300(&DAT_00604928, &DAT_006008e0) — la pose de caméra devient la matrice.
        RotMatrix((short*)s_CameraVector_00604928, (short*)s_CameraMatrix_006008e0);
        Ordinal_376((byte)s_BackColourR_005e93a0,
                    (byte)s_BackColourG_005e93a2,
                    (byte)s_BackColourB_005e93a4);

        // DAT_00572b7a — le lacet de l'entité de scène, à +0x32, amené vers l'angle du personnage.
        short* sceneYaw = (short*)(s_SceneEntity_00572b48 + 0x32);
        *sceneYaw += (short)((scratch[0x150] * -0x100 - *sceneYaw) >> 2);
    }

    // Ghidra: &DAT_00562b70 — seize octets relevés au décalage fichier 0x161170, un par personnage.
    // Le sous-état 3 s'en sert pour préarmer scratch[0x153] selon la classe choisie. La table
    // s'arrête net sur celle de 0x00562b80, ce qui en fixe la taille.
    private static readonly byte[] s_ClassDefault_00562b70 =
    [
        0x08, 0x02, 0x00, 0x04, 0x0a, 0x02, 0x04, 0x06,
        0x09, 0x03, 0x0f, 0x0d, 0x01, 0x03, 0x05, 0x07,
    ];

    // Ghidra: &DAT_00572e58 — huit enregistrements de 0x9c octets, du même type que l'entité de
    // scène et que les emplacements du carrousel : les huit icônes que le sous-état 4 dispose en
    // cercle. Ghidra les désigne par DAT_00572e78 (+0x20) et DAT_00572e80 (+0x28) selon la boucle.
    // Elles suivent le tableau de particules (0x00572be4 + 4 * 0x9c = 0x00572e54, puis 0x00572e58).
    private const int SceneIconCount = 8;
    private const int SceneIconStride = 0x9c;
    private static readonly byte* s_SceneIcons_00572e58 =
        (byte*)NativeMemory.AllocZeroed(SceneIconCount, SceneIconStride);

    /// <summary>
    /// Ghidra: FUN_00508e00 @ 0x00508e00 — <c>PTR_LAB_00560708[4]</c>, le carrousel des huit
    /// éléments, disposés sur un cercle de rayon <c>0x78</c> et choisis par <c>scratch[0x151]</c>.
    ///
    /// <para><b>Signature reprise au prologue</b> : <c>SUB ESP,0x44</c> plus quatre <c>PUSH</c> =
    /// <c>0x54</c>, donc les arguments sont en <c>[ESP+0x58..0x64]</c>. <c>EBP</c> reçoit le
    /// curseur de primitives depuis <c>[ESP+0x60]</c> avant la boucle de dessin (d'où le
    /// <c>unaff_retaddr</c> de Ghidra), et l'épilogue renvoie le résultat du dernier
    /// <c>FUN_005443a0</c>.</para>
    ///
    /// <para>Détail à ne pas rater : après la phase d'entrée, l'original **réutilise l'emplacement
    /// de son quatrième argument comme compteur de boucle** (<c>INC EBX ; MOV [ESP+0x64],EBX</c>),
    /// ce que Ghidra rend en <c>uStack00000004</c>. Ici c'est une variable locale.</para>
    /// </summary>
    private static uint* CharacterSelectSubState4(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = s_SelectScratch_00573338;
        int* state = s_SelectState_00578de8;

        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;

            // La caméra saute directement à l'angle de l'élément courant, sans amortissement.
            ushort turn = (ushort)((scratch[0x151] - 4) & 7);
            *(short*)(s_CameraVector_00604928 + 0) = 0x15e;
            *(short*)(s_CameraVector_00604928 + 2) = (short)(turn * 0x200);
            *(short*)(s_CameraVector_00604928 + 4) = 0;

            *(short*)(s_SceneEntity_00572b48 + 0x30) = 0;                          // DAT_00572b78
            *(short*)(s_SceneEntity_00572b48 + 0x32) = (short)(turn * -0x200 + 0x800); // DAT_00572b7a
            *(short*)(s_SceneEntity_00572b48 + 0x34) = 0;                          // DAT_00572b7c

            // Les huit icônes sur un cercle : même disposition qu'au carrousel des personnages,
            // avec un pas de 0x200 (huit positions) et un rayon de 0x78 au lieu de 0x96.
            for (int i = 0; i < SceneIconCount; i++)
            {
                byte* icon = s_SceneIcons_00572e58 + i * SceneIconStride;
                *(int*)(icon + 0x1c) = (int)GetSinTableEntry(i << 9) * -0x78;
                *(int*)(icon + 0x20) = 0;
                *(int*)(icon + 0x24) = GetCosTableEntry(i << 9) * 0x78;
                *(ushort*)(icon + 0x28) = 0;
                *(ushort*)(icon + 0x2a) = 0;
                *(ushort*)(icon + 0x2c) = 0;
                *(ushort*)(icon + 0x36) = 1;
                *(uint*)(icon + 0x38) = 0x1000000;
            }

            s_SelectFlags_00562a48[2] = 3;
        }

        if (s_SelectFlags_00562a48[1] == 0) StepSubState4Phase(pad, scratch, state);

        // L'amortissement propre à ce sous-état : il vise scratch[0x151] par pas de 0x200 (huit
        // crans), redresse l'orientation de scène vers (0x200, 0x380, 0) et prend sa teinte de fond
        // dans la table des huit tons.
        ushort delta = (ushort)(((scratch[0x151] - 4) * 0x200
                                 - *(short*)(s_CameraVector_00604928 + 2)) & 0xfff);
        short step = delta < 0x800
            ? (short)((short)delta >> 2)
            : (short)-((short)(0x1000 - (short)delta) >> 2);
        *(short*)(s_CameraVector_00604928 + 2) += step;

        s_ScenePitch_005e9308 += (short)((0x200 - s_ScenePitch_005e9308) >> 2);
        s_SceneYaw_005e930a += (short)((0x380 - s_SceneYaw_005e930a) >> 2);
        s_SceneRoll_005e930c += (short)(-s_SceneRoll_005e930c >> 2);

        short* tone = (short*)(s_SceneToneTable_00562a58 + scratch[0x151] * SceneToneStride);
        s_BackColourR_005e93a0 += (short)((tone[0] - s_BackColourR_005e93a0) >> 2);
        s_BackColourG_005e93a2 += (short)((tone[1] - s_BackColourG_005e93a2) >> 2);
        s_BackColourB_005e93a4 += (short)((tone[2] - s_BackColourB_005e93a4) >> 2);

        RotMatrix((short*)s_CameraVector_00604928, (short*)s_CameraMatrix_006008e0);
        Ordinal_376((byte)s_BackColourR_005e93a0,
                    (byte)s_BackColourG_005e93a2,
                    (byte)s_BackColourB_005e93a4);

        *(short*)(s_SceneEntity_00572b48 + 0x32) =
            (short)(0x800 - *(short*)(s_CameraVector_00604928 + 2));

        return DrawSubState4Icons(otBase, primCursor, scratch);
    }

    /// <summary>
    /// La machine à phases du sous-état 4 : <c>0x2e</c> pendant le choix, <c>0x2f</c> et
    /// <c>0x30</c> le temps de la boîte de confirmation.
    /// </summary>
    private static void StepSubState4Phase(byte* pad, byte* scratch, int* state)
    {
        if (state[2] == 0x2e)
        {
            ushort buttons = *(ushort*)(pad + 2);

            if ((buttons & 0x8000) != 0)          // validation
            {
                *(ushort*)(pad + 2) = (ushort)(buttons & 0x7fff);
                PlayUiSound38();
                state[2]++;
                return;
            }

            if ((buttons & 0x4000) != 0)          // annulation : retour au sous-état 3
            {
                short angle = (short)(scratch[0x150] + 8);
                *(short*)(s_CameraVector_00604928 + 2) = (short)(angle * 0x100);
                *(short*)(s_SceneEntity_00572b48 + 0x32) = (short)(angle * -0x100 + 0x800);
                state[2] = 0x5f;
                PlayUiSound39();
                state[0]--;
                return;
            }

            // Le choix tourne en boucle sur huit crans — le masque & 7 s'en charge, sans borne.
            sbyte before = (sbyte)scratch[0x151];
            if ((pad[6] & 0x80) != 0) scratch[0x151] = (byte)(before - 1);
            if ((pad[6] & 0x20) != 0) scratch[0x151]++;
            scratch[0x151] &= 7;
            if (before != (sbyte)scratch[0x151]) PlayUiSound3A();
            return;
        }

        if (state[2] == 0x2f)
        {
            if ((s_Unknown_00560fef & 2) == 0)
            {
                *(ushort*)(pad + 2) &= 0x7fff;
                state[2]++;
            }
            else if ((pad[3] & 0x40) != 0)
            {
                s_Unknown_0056075b = 0;
                s_Unknown_00560fef &= 0xfd;
                PlayUiSound39();
                state[2] = 0x2e;
            }
            return;
        }

        if (state[2] != 0x30) return;

        if ((s_Unknown_00560fef & 4) != 0)
        {
            if ((pad[3] & 0x40) == 0) return;
            s_Unknown_00560763 = 0;
            s_Unknown_00560fef &= 0xfb;
            PlayUiSound39();
            state[2] = 0x2e;
            return;
        }

        if (s_Unknown_005e909f != 0) { state[2] = 0x2e; return; }

        // Confirmé : l'icône choisie est recopiée dans chacune des quatre particules, qui partent
        // ensuite d'une position et d'une vitesse tirées au sort.
        state[2] = 0x39;
        for (int i = 0; i < SceneParticleCount; i++)
        {
            byte* particle = s_SceneParticles_00572be4 + i * SceneParticleStride;

            CopyDwords((uint*)(s_SceneIcons_00572e58 + scratch[0x151] * SceneIconStride),
                       (uint*)particle, 0x27);

            // Ghidra: puVar6 pointe la base + 0x3c, d'où les indices 0, 5, 6, 7 ci-dessous.
            uint* fields = (uint*)(particle + 0x3c);
            fields[0] = *(uint*)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x94)
                                 + 8 + scratch[0x151] * 4);

            short spread = (short)CrtRand();
            fields[5] = (uint)(GetSinTableEntry(spread) * 0x1e);
            fields[6] = (uint)(CrtRand() * 8 - 0x1fff8);
            fields[7] = (uint)(GetCosTableEntry(spread) * 0x1e);
        }
        state[0]++;
    }

    /// <summary>
    /// Le dessin des huit icônes. Chacune est projetée depuis une copie de dix dwords de sa
    /// position, relevée de <c>0x28000</c>, et n'est dessinée que si la projection tombe à l'écran.
    /// L'emplacement d'OT est la profondeur rendue par <see cref="ProjectPoint"/>.
    /// </summary>
    private static uint* DrawSubState4Icons(uint* otBase, uint* primCursor, byte* scratch)
    {
        for (int i = 0; i < SceneIconCount; i++)
        {
            byte* icon = s_SceneIcons_00572e58 + i * SceneIconStride;
            short* glow = (short*)(icon + 0x28);

            byte* copy = stackalloc byte[40];
            CopyDwords((uint*)(icon + 0x1c), (uint*)copy, 10);
            *(int*)(copy + 4) += 0x28000;

            // L'icône choisie brille plus fort et tourne moins vite que les autres.
            *(short*)(icon + 0x38) = 0;
            if (i == scratch[0x151])
            {
                *(short*)(icon + 0x3a) = 0x100;
                glow[0] += (short)((0x1800 - glow[0]) >> 2);
                glow[1] += (short)((0x1800 - glow[1]) >> 2);
            }
            else
            {
                *(short*)(icon + 0x3a) = 0x300;
                glow[0] += (short)((0x1000 - glow[0]) >> 2);
                glow[1] += (short)((0x1000 - glow[1]) >> 2);
            }

            short* projected = stackalloc short[4];
            if (ProjectPoint((int*)copy, projected) == 0) continue;

            byte* atlas = ((byte**)*(byte**)(s_MainResourceDirectory_005e97c4 + 0x94))[2 + i];
            short frame = (short)((s_FadeCounter_005e9960 >> 1) % 6);

            byte* sprite = stackalloc byte[20];
            NativeMemory.Clear(sprite, 20);
            *(byte**)sprite = atlas;
            // Ghidra: MOV ECX,[ESI + 0x10] — mais ESI vaut icône + 0x28, donc c'est **icône + 0x38**,
            // le mot d'attribut posé à 0x1000000 à l'initialisation. Le lire à +0x10 donne zéro, et
            // le sprite sort en disque noir : mesuré.
            *(uint*)(sprite + 4) = *(uint*)(icon + 0x38);
            *(short*)(sprite + 8) = projected[0];
            *(short*)(sprite + 0x0a) = projected[1];
            *(short*)(sprite + 0x0c) = glow[0];
            *(short*)(sprite + 0x0e) = glow[1];
            *(short*)(sprite + 0x10) = frame;

            // Le même décalage d'un pixel que le carrousel applique le temps du dessin.
            atlas[frame * 8 + 2]--;
            atlas[frame * 8 + 3]--;
            primCursor = DrawSpritePrimitive(otBase + projected[2], primCursor, (int*)sprite, 0, 0);
            atlas[frame * 8 + 2]++;
            atlas[frame * 8 + 3]++;
        }

        // Le libellé de l'élément courant, dans le troisième descripteur d'interface.
        byte* label = s_UiDescriptors_00560ff0 + 2 * UiDescriptorStride;   // &DAT_00562180
        BindMenuLabels(
            *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70)
                      + 0xc4 + scratch[0x151] * 4),
            0, label, 0);
        return RunTextEngine(primCursor, label);
    }

    /// <summary>
    /// Ghidra: FUN_00508860 @ 0x00508860 — <c>PTR_LAB_00560708[3]</c>, le choix sur une grille de
    /// quatre par deux, dont le résultat est empaqueté dans <c>scratch[0x153]</c>.
    ///
    /// <para><b>Signature reprise au prologue</b>, comme les précédentes : <c>SUB ESP,0x14</c> plus
    /// trois <c>PUSH</c> = <c>0x20</c>, donc les arguments sont en <c>[ESP+0x24..0x30]</c> —
    /// <c>[ESP+0x24]</c> otBase (Ghidra: <c>uStack_8</c>), <c>[ESP+0x2C]</c> le curseur de
    /// primitives (<c>unaff_retaddr</c>), <c>[ESP+0x30]</c> la manette (<c>param_1</c>). Le
    /// pseudo-C rend la fonction <c>void</c>, mais l'épilogue laisse dans <c>EAX</c> le résultat du
    /// dernier <c>FUN_00545340</c> : la valeur de retour existe, Ghidra l'a perdue.</para>
    ///
    /// <para><c>DAT_00573de0</c> est <c>DAT_00573518 + 0x8c8</c> : le **deuxième** descripteur du
    /// même tableau, pas un nouveau symbole.</para>
    /// </summary>
    private static uint* CharacterSelectSubState3(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = s_SelectScratch_00573338;
        int* state = s_SelectState_00578de8;
        byte* box = s_SelectSlots_00573518;                                  // DAT_00573518
        byte* panel = s_SelectSlots_00573518 + CharacterSelectSlotStride;    // DAT_00573de0

        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;

            // Écrit ici dans l'ordre des adresses ; Ghidra les rend dans un ordre arbitraire.
            *(uint*)(box + 0x04) = 0x380028;      // DAT_0057351c — position (0x28, 0x38)
            *(short*)(box + 0x08) = 0xe0;         // DAT_00573520
            *(short*)(box + 0x0a) = 0x28;         // DAT_00573522
            *(uint*)(box + 0x20) = 0x1070c24;     // DAT_00573538 — texte, bit d'activation posé
            *(uint*)(box + 0x24) = 0x1000000;     // DAT_0057353c

            *(short*)(panel + 0x04) = 0x68;       // DAT_00573de4
            *(short*)(panel + 0x06) = 0x68;       // DAT_00573de6
            *(short*)(panel + 0x08) = 0x60;       // DAT_00573de8
            *(short*)(panel + 0x0a) = 0x18;       // DAT_00573dea
            panel[0x12] = 0x0b;                   // DAT_00573df0._2_1_
            panel[0x14] = 0x05;                   // DAT_00573df4._0_1_
            *(uint*)(panel + 0x20) = 0x1070c10;   // DAT_00573e00
            *(uint*)(panel + 0x24) = 0x1000000;   // DAT_00573e04

            state[4] = 0;
            state[5] = 0;
            s_SelectFlags_00562a48[2] = 2;
            scratch[0x153] = s_ClassDefault_00562b70[scratch[0x150]];
        }

        SettleSceneCamera(scratch);

        if (s_SelectFlags_00562a48[1] == 0) StepSubState3Phase(pad, scratch, state);

        return DrawSubState3(otBase, primCursor, state, box, panel);
    }

    /// <summary>
    /// La machine à phases du sous-état 3. <c>state[2]</c> vaut <c>0x5f</c> pendant le choix, puis
    /// <c>0x60</c>, <c>0x61</c>, <c>0x62</c> le temps d'une boîte de dialogue de confirmation.
    /// </summary>
    private static void StepSubState3Phase(byte* pad, byte* scratch, int* state)
    {
        switch (state[2])
        {
            case 0x5f:
            {
                ushort buttons = *(ushort*)(pad + 2);

                if ((buttons & 8) != 0)          // saut direct à la confirmation
                {
                    PlayUiSound38();
                    state[2] = 0x61;
                    break;
                }

                if ((buttons & 0x4000) != 0)     // annulation : retour au sous-état 1
                {
                    PlayUiSound39();
                    state[2] = 0x13;
                    state[0] -= 2;
                    break;
                }

                if ((buttons & 0x8000) != 0)     // validation
                {
                    *(ushort*)(pad + 2) = (ushort)(buttons & 0x7fff);
                    PlayUiSound38();
                    state[2] = 0x61;
                    // Le choix s'empaquette sur les bits 1..3, le bit 0 étant conservé.
                    scratch[0x153] = (byte)((((byte)state[5] << 2 | (byte)state[4]) << 1)
                                            | (scratch[0x153] & 1));
                    break;
                }

                // Déplacement dans la grille. Les quatre bits testés sont ceux de
                // s_PadButtonMasks_00564f18 ; le son ne part que si la position a bougé.
                uint before = (uint)(state[5] << 8 | state[4]);

                if ((pad[6] & 0x10) != 0 && state[5] != 0) state[5]--;
                if ((pad[6] & 0x40) != 0 && state[5] < 1) state[5]++;

                if ((pad[6] & 0x80) != 0)
                {
                    if (state[4] == 0)
                    {
                        if (state[5] != 0) { state[5]--; state[4] = 3; }
                    }
                    else state[4]--;
                }

                if ((pad[6] & 0x20) != 0)
                {
                    if (state[4] < 3) state[4]++;
                    else if (state[5] < 1) { state[5]++; state[4] = 0; }
                }

                if (before != (uint)(state[5] << 8 | state[4])) PlayUiSound3A();
                break;
            }

            case 0x60:
                // TODO(REVERSE): DAT_00560fef et ses voisins sont posés par le code de boîte de
                // dialogue, non reconstruit, donc ils lisent 0 et seule la première branche est
                // atteignable pour l'instant.
                if ((s_Unknown_00560fef & 4) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2]++;
                    scratch[0x153] = (byte)((((byte)state[5] << 2 | (byte)state[4]) << 1)
                                            | s_Unknown_005e909f);
                }
                else if ((pad[3] & 0x40) != 0)
                {
                    s_Unknown_00560763 = 0;
                    s_Unknown_00560fef &= 0xfb;
                    PlayUiSound39();
                    state[2]--;
                }
                break;

            case 0x61:
                if ((s_Unknown_00560fef & 2) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2]++;
                    break;
                }
                if ((pad[3] & 0x40) == 0) break;
                s_Unknown_0056075b = 0;
                s_Unknown_00560fef &= 0xfd;
                PlayUiSound39();
                state[2] = 0x5f;
                break;

            case 0x62:
                if ((s_Unknown_00560fef & 4) != 0)
                {
                    if ((pad[3] & 0x40) == 0) break;
                    s_Unknown_00560763 = 0;
                    s_Unknown_00560fef &= 0xfb;
                    PlayUiSound39();
                    state[2] = 0x5f;
                    break;
                }
                if (s_Unknown_005e909f == 0)
                {
                    // Confirmé : on passe au sous-état suivant.
                    *(byte*)(s_SelectSlots_00573518 + CharacterSelectSlotStride + 0x14) = 4;
                    state[0]++;
                    state[2] = 0x2e;
                    break;
                }
                state[2] = 0x5f;
                break;
        }
    }

    /// <summary>
    /// Le dessin du sous-état 3. Le curseur de grille est relevé dans l'assembleur :
    /// <c>x = boîte+0x04 + colonne * 48 + 0x14</c>, <c>y = boîte+0x06 + ligne * 16 + 0x0a</c>.
    /// <para>Le second panneau reçoit <c>&amp;DAT_00572b48</c> **en guise de drapeaux** — c'est-à-dire
    /// que l'entité de scène devient le « propriétaire » que <see cref="DrawUiPanel"/> relit à
    /// <c>+0x8b0</c>. Son champ 0 valant <c>0x800000</c>, le panneau prend son emplacement joueur
    /// dans <c>owner[2]</c> : c'est le chemin `kind == 0x800000` de DrawUiPanel, jusqu'ici jamais
    /// emprunté.</para>
    /// </summary>
    private static uint* DrawSubState3(uint* otBase, uint* primCursor, int* state, byte* box, byte* panel)
    {
        byte* sprite = stackalloc byte[20];
        NativeMemory.Clear(sprite, 20);
        *(uint**)sprite = **(uint***)(s_MainResourceBuffer_005e97c0 + 0x94);
        *(uint*)(sprite + 4) = 0x1000000;
        *(short*)(sprite + 8) = (short)(*(short*)(box + 0x04) + state[4] * 48 + 0x14);
        *(short*)(sprite + 0x0a) = (short)(*(short*)(box + 0x06) + state[5] * 16 + 0x0a);
        *(short*)(sprite + 0x10) = 0x18;

        EmitViewportRect(otBase, &primCursor);
        primCursor = DrawSpriteVariant(otBase, primCursor, (int*)sprite, 0, 0);
        primCursor = DrawFlatPrimitive(otBase, primCursor, (int*)sprite);
        EmitClipRect(otBase, &primCursor);

        BindMenuLabels(s_Unknown_005606f8, 0, box, 0);
        primCursor = RunTextEngine(primCursor, box);

        for (int offset = 0x500; offset < 0x508; offset += 4)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70) + offset),
                0, box, 0);
            primCursor = RunTextEngine(primCursor, box);
        }
        primCursor = DrawUiPanel(otBase, primCursor, box);

        if (state[2] != 0x2e)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70) + 0x18c),
                (uint)(nuint)s_SceneEntity_00572b48, panel, 0);
            primCursor = RunTextEngine(primCursor, panel);
            primCursor = DrawUiPanel(otBase, primCursor, panel);
        }

        return primCursor;
    }

    /// <summary>
    /// Le nom par défaut de la classe choisie, copié dans le tampon de saisie quand le joueur
    /// valide sans avoir rien tapé. Ghidra: <c>directory[0x70][0x208 + scratch[0x150] * 4]</c>.
    /// La longueur retenue est **la moitié** du nombre d'octets : les noms sont en Shift-JIS sur
    /// deux octets, et le compteur compte des caractères.
    /// </summary>
    private static void LoadDefaultName(byte* scratch, int* state)
    {
        byte* source = *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70)
                                 + 0x208 + scratch[0x150] * 4);
        int length = 0;
        while (source[length] != 0)
        {
            scratch[0x100 + length] = source[length];
            length++;
        }
        scratch[0x100 + length] = 0;
        state[6] = length >> 1;
    }

    /// <summary>
    /// Le clavier virtuel à la manette. <c>state[3]</c> est la page, <c>state[4]</c> la colonne
    /// (0..9), <c>state[5]</c> la ligne (0..7) — la ligne 7 étant la colonne « fin de saisie » —
    /// et <c>state[6]</c> le nombre de caractères déjà posés.
    /// <para>Retourne <c>true</c> quand le sous-état se termine, ce qui fait désarmer la file de
    /// frappe par l'appelant — l'original y arrive par deux chemins qui se rejoignent avant
    /// <c>EnableKBInput(0)</c>.</para>
    /// </summary>
    private static bool HandlePadNameEntry(byte* pad, byte* scratch, int* state)
    {
        // Ghidra relit la même composition avant et après pour décider du clic de déplacement.
        uint before = (uint)((state[3] << 8 | state[5]) << 8 | state[4]);

        if ((pad[2] & 8) != 0) { state[4] = 0; state[5] = 7; }

        if ((pad[6] & 0x80) != 0)          // haut
        {
            if (state[4] == 0)
            {
                // En haut de colonne : on remonte en bas, mais seulement s'il y a où aller —
                // et l'original **repose le bit gauche** pour que la frappe se propage.
                if (state[5] != 0 || state[3] != 0) { state[4] = 9; pad[6] |= 0x10; }
            }
            else state[4]--;
        }

        if ((pad[6] & 0x20) != 0)          // bas
        {
            if (state[4] < 9) state[4]++;
            else if (state[5] < 7 || state[3] < 7)
            {
                state[4] = 0;
                *(ushort*)(pad + 6) |= 0x40;   // idem, en reposant le bit droit
            }
        }

        if ((pad[6] & 0x10) != 0)          // gauche
        {
            if (state[5] == 0) { if (state[3] != 0) state[3]--; }
            else state[5]--;
        }

        if ((pad[6] & 0x40) != 0 && state[5] < 7) state[5]++;   // droite

        if (state[5] == 7) state[4] = 0;

        if (before != (uint)((state[3] << 8 | state[5]) << 8 | state[4])) PlayUiSound3A();

        if ((*(ushort*)(pad + 2) & 0x8000) == 0)
        {
            if ((*(ushort*)(pad + 2) & 0x4000) == 0) return false;

            // Annulation : efface le dernier caractère, ou quitte si le tampon est déjà vide et
            // que le curseur est sur la case de fin.
            if (state[6] != 0)
            {
                state[6]--;
                scratch[0x100 + state[6]] = 0;
                PlayUiSound39();
                return false;
            }
            if (state[5] != 7 || state[3] != 7) return false;

            PlayUiSound39();
            state[2] = 0;
            state[0]--;
            return true;
        }

        if (state[5] != 7)
        {
            // Validation d'une case : le caractère se lit dans la table de la page courante.
            if (state[6] > 7) return false;

            byte* row = *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70)
                                  + 0x50 + (state[5] + state[3]) * 4);
            byte glyph = row[state[4] * 3 + (((state[4] < 5 ? 1 : 0) - 1) & 2)];

            // TODO(REVERSE): le test de l'original compare un **octet** à DAT_00562c9c & 0xffff,
            // soit 0x4081 — le caractère Shift-JIS 0x8140 (espace idéographique) relu en
            // petit-boutien. Un octet ne peut pas valoir 0x4081, donc cette branche ne se
            // déclenche jamais telle qu'écrite ; c'est probablement une lecture sur deux octets
            // que Ghidra a rétrécie. Transcrit tel quel plutôt que corrigé au jugé.
            if (state[6] == 0 && glyph == (s_IdeographicSpace_00562c9c & 0xffff))
            {
                PlayUiSound3B();
                return false;
            }

            PlayUiSound38();
            scratch[0x100 + state[6]] = glyph;
            state[6]++;
            scratch[0x100 + state[6]] = 0;
            if (state[6] == 8) { state[4] = 0; state[5] = 7; }
            return false;
        }

        // Case « fin » : on confirme, en retombant sur le nom par défaut si rien n'a été saisi.
        if (state[6] == 0) LoadDefaultName(scratch, state);
        PlayUiSound38();
        state[2] = 0x5f;
        state[0] += 2;
        return true;
    }

    /// <summary>La frappe réelle, dépilée de la file de winsys3d par <c>GetKBChar</c>.</summary>
    private static bool HandleTypedNameEntry(int typed, byte* scratch, int* state)
    {
        if (typed == 0x0d)                 // Entrée : confirme, comme la case « fin »
        {
            state[5] = 7;
            if (state[6] == 0) LoadDefaultName(scratch, state);
            PlayUiSound38();
            state[0] += 2;
            state[2] = 0x5f;
            EnableKBInput(0);
            EnablePadKeyboard();           // Ghidra: FUN_00523520
            return false;                  // le désarmement est déjà fait ici
        }

        if (typed == 0x08)                 // retour arrière
        {
            if (state[6] != 0)
            {
                state[6]--;
                scratch[0x100 + state[6]] = 0;
                PlayUiSound39();
            }
            return false;
        }

        if (typed != 0x1b)
        {
            // Les codes de contrôle et la plage 0x80..0x9f sont ignorés, et le tampon est plein
            // à huit caractères.
            if (typed < 0x20 || (typed > 0x7f && typed < 0xa0) || state[6] > 7) return false;

            PlayUiSound38();
            scratch[0x100 + state[6]] = (byte)typed;
            state[6]++;
            scratch[0x100 + state[6]] = 0;
            if (state[6] == 8) { state[4] = 0; state[5] = 7; }
            return false;
        }

        PlayUiSound39();                   // Échap : retour au sous-état précédent
        state[2] = 0;
        state[0]--;
        return true;
    }

    /// <summary>
    /// Le dessin de l'écran : la boîte de saisie, le curseur sur la grille, puis les huit lignes
    /// du clavier virtuel.
    /// <para>Coordonnées du curseur, relevées dans l'assembleur plutôt que dans le pseudo-C, qui
    /// avait perdu le cadre de pile : <c>x = 0x40 + colonne * 16 + (colonne &lt; 5 ? 0 : 8)</c> et
    /// <c>y = boîte+0x06 + (ligne + 1) * 12</c>. Le décrochage de 8 pixels au-delà de la
    /// cinquième colonne est le même que celui du choix du caractère, deux lignes plus haut.</para>
    /// </summary>
    private static uint* DrawNameEntryScreen(uint* otBase, uint* primCursor, byte* scratch, int* state)
    {
        byte* box = s_UiDescriptors_00560ff0 + 1 * UiDescriptorStride;   // &DAT_005618b8

        BindMenuLabels(s_Unknown_005606f8, 0, box, 0);
        primCursor = RunTextEngine(primCursor, box);

        // Le nom en cours, recopié sur la pile puis suffixé d'un « _ » tant que le curseur n'est
        // pas sur la case de fin. L'original recopie de la même façon, dans un local de 24 octets.
        byte* typedName = stackalloc byte[24];
        int length = 0;
        while (scratch[0x100 + length] != 0) { typedName[length] = scratch[0x100 + length]; length++; }
        typedName[length] = 0;

        if (state[5] != 7)
        {
            typedName[length] = 0x5f;      // Ghidra: &DAT_00562c98, la chaîne "_"
            typedName[length + 1] = 0;
        }

        BindMenuLabels(typedName, 0, box, 0);
        primCursor = RunTextEngine(primCursor, box);

        // Le curseur de la grille : un sprite de 20 octets monté sur la pile, comme partout.
        byte* cursor = stackalloc byte[20];
        NativeMemory.Clear(cursor, 20);
        *(uint**)cursor = **(uint***)(s_MainResourceDirectory_005e97c4 + 0x94);
        *(uint*)(cursor + 4) = 0x1000000;

        byte* grid = s_SelectSlots_00573518;
        *(short*)(cursor + 8) =
            (short)(*(short*)(grid + 0x04) + state[4] * 16 + (state[4] < 5 ? 0 : 8) + 0x18);
        *(short*)(cursor + 0x0a) = (short)(*(short*)(grid + 0x06) + (state[5] + 1) * 12);
        *(short*)(cursor + 0x10) = 0x18;

        EmitViewportRect(otBase, &primCursor);
        primCursor = DrawSpriteVariant(otBase, primCursor, (int*)cursor, 0, 0);
        primCursor = DrawFlatPrimitive(otBase, primCursor, (int*)cursor);
        EmitClipRect(otBase, &primCursor);

        // Les huit lignes du clavier virtuel. La table de largeurs passe en **fixe** le temps de
        // la grille — c'est ce qui aligne les colonnes — et revient au proportionnel après.
        BindMenuLabels(s_Unknown_005606f8, 0, grid, 0);
        primCursor = RunTextEngine(primCursor, grid);

        UseFixedGlyphWidths();             // Ghidra: FUN_00507870
        for (int i = 0; i < 8; i++)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70)
                          + 0x50 + (i + state[3]) * 4),
                0, grid, 0);
            primCursor = RunTextEngine(primCursor, grid);
        }
        primCursor = DrawUiPanel(otBase, primCursor, grid);
        UseProportionalGlyphWidths();      // Ghidra: FUN_00507890

        return primCursor;
    }

    // Ghidra: DAT_00562c9c — deux octets 81 40 relevés au décalage fichier 0x16129c : le caractère
    // Shift-JIS 0x8140, l'espace idéographique. Le code le relit en dword, d'où le 0x4081.
    private const uint s_IdeographicSpace_00562c9c = 0x00004081;

    private static uint* CharacterSelectSubState0(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        ushort* padWords = (ushort*)pad;
        byte* scratch = s_SelectScratch_00573338;

        // Ghidra: le pilotage libre, actif tant que DAT_005633a8 vaut 0. Le bit 1 du premier mot
        // choisit la cible : couleur de fond au pas de 8 (bornée à 8 bits), sinon rotation de la
        // scène au pas de 0x40 (bornée au tour complet de 0x1000).
        if (s_UnknownModeFlag_005633a8 == 0)
        {
            ushort buttons = padWords[0];
            if ((buttons & 1) == 0)
            {
                if ((buttons & 0x400) != 0) s_SceneYaw_005e930a -= 0x40;
                if ((padWords[0] & 0x800) != 0) s_SceneYaw_005e930a += 0x40;
                if ((padWords[0] & 0x100) != 0) s_SceneRoll_005e930c -= 0x40;
                if ((padWords[0] & 0x200) != 0) s_SceneRoll_005e930c += 0x40;
                if ((padWords[0] & 0x1000) != 0) s_ScenePitch_005e9308 += 0x40;
                if ((padWords[0] & 0x4000) != 0) s_ScenePitch_005e9308 -= 0x40;
                s_ScenePitch_005e9308 &= 0xfff;
                s_SceneYaw_005e930a &= 0xfff;
                s_SceneRoll_005e930c &= 0xfff;
            }
            else
            {
                if ((buttons & 0x400) != 0) s_BackColourG_005e93a2 -= 8;
                if ((padWords[0] & 0x800) != 0) s_BackColourG_005e93a2 += 8;
                if ((padWords[0] & 0x100) != 0) s_BackColourB_005e93a4 -= 8;
                if ((padWords[0] & 0x200) != 0) s_BackColourB_005e93a4 += 8;
                if ((padWords[0] & 0x1000) != 0) s_BackColourR_005e93a0 += 8;
                if ((padWords[0] & 0x4000) != 0) s_BackColourR_005e93a0 -= 8;
                s_BackColourR_005e93a0 &= 0xff;
                s_BackColourG_005e93a2 &= 0xff;
                s_BackColourB_005e93a4 &= 0xff;
            }
        }

        // Ghidra: la première frame du sous-état — le drapeau que FUN_00506880 relève.
        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;

            s_BackColourR_005e93a0 = 0x60;
            s_BackColourG_005e93a2 = 0x60;
            s_BackColourB_005e93a4 = 0x60;
            s_ScenePitch_005e9308 = 0x200;
            s_SceneYaw_005e930a = 0x380;
            s_SceneRoll_005e930c = 0;

            // Ghidra: DAT_00604928 — un vecteur de trois shorts. Le troisième cadre la caméra sur
            // le personnage courant : (index - 8) * 0x100, soit un seizième de tour par cran.
            *(short*)(s_CameraVector_00604928 + 0) = 0x15e;
            *(short*)(s_CameraVector_00604928 + 4) = 0;
            *(short*)(s_CameraVector_00604928 + 2) = (short)((scratch[0x150] - 8 & 0xf) << 8);

            s_Unknown_006008d0 = 0;
            s_Unknown_006008d4 = 0;
            s_Unknown_006008d8 = 0;
            CameraTranslation[0] = 0;
            CameraTranslation[1] = 0;
            CameraTranslation[2] = 0;

            // Ghidra: les seize emplacements de personnage, posés sur un cercle. Ordinal_451/450
            // sont rsin/rcos en 0x1000 pas par tour, donc `i << 8` répartit exactement seize
            // positions, et 0x96 = 150 est le rayon.
            for (int i = 0; i < CharacterSlotCount; i++)
            {
                byte* slot = s_CharacterSlots_00578420 + i * CharacterSlotStride;

                *(uint*)(slot + 0x00) = 0x800000;
                slot[0x04] = (byte)i;
                slot[0x08] = 0;
                slot[0x0a] = 0;
                slot[0x0b] = (byte)(i & 3);
                *(ushort*)(slot + 0x10) = 0;
                *(ushort*)(slot + 0x12) = 0;
                *(ushort*)(slot + 0x18) = 0;
                *(ushort*)(slot + 0x1a) = 0;
                *(int*)(slot + 0x1c) = (int)GetSinTableEntry(i << 8) * -0x96;
                *(int*)(slot + 0x20) = 0;
                *(int*)(slot + 0x24) = GetCosTableEntry(i << 8) * 0x96;
                *(ushort*)(slot + 0x28) = 0;
                *(ushort*)(slot + 0x2a) = 0;
                *(ushort*)(slot + 0x2c) = 0;
                *(ushort*)(slot + 0x30) = 0;
                *(ushort*)(slot + 0x34) = 0;
                *(ushort*)(slot + 0x36) = 1;
                *(uint*)(slot + 0x38) = 0xffffff;
            }

            s_SelectFlags_00562a48[2] = 1;
        }

        // Ghidra: la machine à phases sur DAT_00578df0, sautée entièrement quand DAT_00562a4c est
        // encore posé. L'original la referme par un maillage de `goto` (LAB_00507438, LAB_0050743d,
        // joined_r0x00507423) ; il se réduit à deux variantes d'annulation qui ne diffèrent que par
        // l'octet remis à zéro et le bit effacé de DAT_00560fef.
        if (s_SelectFlags_00562a48[1] == 0)
        {
            bool sharedCancel = false;

            switch (s_SelectState_00578de8[2])
            {
                case 0:
                {
                    sbyte before = (sbyte)scratch[0x150];
                    if ((padWords[3] & 0x80) != 0) scratch[0x150] = (byte)(before - 1);
                    if ((padWords[3] & 0x20) != 0) scratch[0x150]++;
                    scratch[0x150] &= 0xf;
                    if (before != (sbyte)scratch[0x150]) PlayUiSound3A();   // FUN_0053db60

                    ushort word = padWords[1];
                    if ((word & 0x8000) == 0)
                    {
                        if ((word & 0x4000) != 0)
                        {
                            padWords[1] = (ushort)(word & 0xbfff);
                            PlayUiSound39();                                // FUN_0053dba0
                            s_SelectState_00578de8[2] = 0x77;
                        }
                    }
                    else
                    {
                        padWords[1] = (ushort)(word & 0x7fff);
                        PlayUiSound38();                                     // FUN_0053db80
                        s_SelectState_00578de8[2]++;
                    }
                    break;
                }

                case 1:
                    if ((s_Unknown_00560fef & 2) == 0)
                    {
                        padWords[1] &= 0x7fff;
                        s_SelectState_00578de8[2]++;
                    }
                    else sharedCancel = true;
                    break;

                case 2:
                    if ((s_Unknown_00560fef & 4) == 0)
                    {
                        if (s_Unknown_005e909f == 0)
                        {
                            // Ghidra: FUN_005352e0(&DAT_00578420 + index * 0x27, &DAT_00572b48, 0x27)
                            // — 0x27 dwords = 0x9c octets, soit l'emplacement entier recopié vers le
                            // bloc de scène. Le sous-état suivant travaille sur cette copie.
                            CopyDwords(
                                (uint*)(s_CharacterSlots_00578420 + scratch[0x150] * CharacterSlotStride),
                                (uint*)s_SceneEntity_00572b48, 0x27);

                            s_SelectState_00578de8[0]++;
                            s_SelectState_00578de8[2] = 0x13;
                            scratch[0x100] = 0;
                            s_SelectState_00578de8[6] = 0;
                        }
                        else s_SelectState_00578de8[2] = 0;
                    }
                    else if ((pad[3] & 0x40) != 0)
                    {
                        s_Unknown_00560763 = 0;
                        s_Unknown_00560fef &= 0xfb;
                        PlayUiSound39();
                        s_SelectState_00578de8[2] = 0;
                    }
                    break;

                case 0x59:
                    if ((s_Unknown_00560fef & 4) != 0)
                    {
                        if ((pad[3] & 0x40) != 0)
                        {
                            s_Unknown_00560763 = 0;
                            s_Unknown_00560fef &= 0xfb;
                            PlayUiSound39();
                            s_SelectState_00578de8[2] = 0;
                        }
                    }
                    else if (s_Unknown_005e909f == 0)
                    {
                        // La sortie de l'écran : le sous-état -1 fait sortir FUN_00506880 par son
                        // `goto`, qui saute la remise à zéro de l'état de jeu — celle-ci est faite
                        // ici à la place.
                        s_SelectState_00578de8[0] = -1;
                        s_GameStateId = 0;
                    }
                    else s_SelectState_00578de8[2] = 0;
                    break;

                case 0x77:
                    if ((s_Unknown_00560fef & 2) == 0)
                    {
                        padWords[1] &= 0x7fff;
                        s_SelectState_00578de8[2] = 0x59;
                    }
                    else sharedCancel = true;
                    break;
            }

            if (sharedCancel && (pad[3] & 0x40) != 0)
            {
                s_Unknown_0056075b = 0;
                s_Unknown_00560fef &= 0xfd;
                PlayUiSound39();
                s_SelectState_00578de8[2] = 0;
            }
        }

        // Ghidra: l'amortissement de la rotation vers le personnage courant, par quarts, sur le
        // chemin le plus court du tour de 0x1000.
        ushort delta = (ushort)(((scratch[0x150] - 8) * 0x100
                                 - *(short*)(s_CameraVector_00604928 + 2)) & 0xfff);
        short step = delta < 0x800
            ? (short)((short)delta >> 2)
            : (short)-(short)((0x1000 - (short)delta) >> 2);
        *(short*)(s_CameraVector_00604928 + 2) += step;

        // Ghidra: FUN_0051c300(&DAT_00604928, &DAT_006008e0) — la pose de caméra devient la matrice
        // que FUN_0051d1f0 recopiera ensuite dans le registre MATRIX du GTE.
        RotMatrix((short*)s_CameraVector_00604928, (short*)s_CameraMatrix_006008e0);

        // Ghidra: Ordinal_376 — libgte SetBackColor.
        Ordinal_376((byte)s_BackColourR_005e93a0,
                    (byte)s_BackColourG_005e93a2,
                    (byte)s_BackColourB_005e93a4);

        // === Le carrousel : seize emplacements amortis, dessinés puis étiquetés ===
        //
        // Les décalages ci-dessous sont ceux de l'emplacement (base 0x00578420), pas ceux de
        // `psVar11` : l'original promène un `short *` calé 0x2a octets après la base, ce qui rend
        // tous ses indices négatifs. Recalés ici sur la base, où ils recoupent exactement le bloc
        // d'initialisation de la première frame plus haut.
        short* projected = stackalloc short[4];
        byte* slotCopy = stackalloc byte[40];
        int angleStep = 0;
        int lift = 0x2000;

        for (int i = 0; i < CharacterSlotCount; i++, angleStep += 0x100, lift -= 0x100)
        {
            byte* slot = s_CharacterSlots_00578420 + i * CharacterSlotStride;

            short* yaw = (short*)(slot + 0x32);
            short* glowR = (short*)(slot + 0x28);
            short* glowG = (short*)(slot + 0x2a);
            short* glowB = (short*)(slot + 0x2c);
            short* tiltX = (short*)(slot + 0x10);
            short* tiltY = (short*)(slot + 0x12);

            // L'emplacement choisi se redresse et s'illumine ; les autres s'inclinent et
            // s'éteignent. Toujours par quarts — l'amortissement standard du jeu.
            if (i == scratch[0x150])
            {
                *yaw += (short)(-(*yaw + angleStep) >> 2);
                *glowR += (short)((0x1800 - *glowR) >> 2);
                *glowG += (short)((0x1800 - *glowG) >> 2);
                *glowB += (short)((0x1800 - *glowB) >> 2);
                *tiltX += (short)(-*tiltX >> 2);
                *tiltY += (short)(-*tiltY >> 2);
            }
            else
            {
                *yaw += (short)((lift - *yaw) >> 2);
                *glowR += (short)(-*glowR >> 2);
                *glowG += (short)(-*glowG >> 2);
                *glowB += (short)(-*glowB >> 2);
                *tiltX += (short)((0x1000 - *tiltX) >> 2);
                *tiltY += (short)((0x1000 - *tiltY) >> 2);
            }

            // Sous 0x100 de luminosité, l'emplacement n'est pas dessiné du tout.
            if (*glowR > 0xff)
            {
                byte assetIndex = slot[0x04];
                short* animIndex = (short*)(slot + 0x18);
                short* frame = (short*)(slot + 0x1a);

                byte* bundle = ((byte**)s_AnimationTable_005e9640)[assetIndex];
                byte* animEntry = *(byte**)(bundle + *animIndex * 4);

                // Avance de frame, bouclée sur la longueur de l'animation (+0x16).
                (*frame)++;
                if (*(ushort*)(animEntry + 0x16) <= *frame) *frame = 0;

                // Ghidra: CONCAT22(psVar11[-9] >> 0xf, psVar11[-0x10]) — la moitié haute est le
                // signe de l'indice d'animation, la basse un champ de l'emplacement.
                // TODO(REVERSE): ce panachage sent l'artefact de reconstruction ; transcrit tel
                // quel faute de mieux.
                uint modelExtra = (uint)((*animIndex >> 0xf) << 0x10)
                                | *(ushort*)(slot + 0x0a);

                primCursor = DrawAnimatedModel(
                    otBase, primCursor, animEntry,
                    (int*)(((byte**)s_ModelTable_005e96a0)[assetIndex]),
                    (int*)(slot + 0x1c), *frame, modelExtra);
            }

            // L'étiquette : on projette la position de l'emplacement, relevée de 0x3c000, et on
            // ne dessine que si elle tombe à l'écran.
            CopyDwords((uint*)(slot + 0x1c), (uint*)slotCopy, 10);
            *(int*)(slotCopy + 4) += 0x3c000;

            if (ProjectPoint((int*)slotCopy, projected) == 0) continue;

            byte* labelSprite = stackalloc byte[20];
            NativeMemory.Clear(labelSprite, 20);

            byte* atlas = *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x94) + 0x28);
            *(byte**)labelSprite = atlas;
            *(uint*)(labelSprite + 4) = 0x1000000;
            *(short*)(labelSprite + 8) = projected[0];
            *(short*)(labelSprite + 0xa) = projected[1];
            *(short*)(labelSprite + 0xc) = *(short*)(slot + 0x10);
            *(short*)(labelSprite + 0xe) = *(short*)(slot + 0x12);
            *(short*)(labelSprite + 0x10) = (short)i;

            // Ghidra décrémente deux octets de l'entrée d'atlas, dessine, puis les restaure — un
            // décalage d'un pixel appliqué le temps des deux passes.
            atlas[i * 8 + 2]--;
            atlas[i * 8 + 3]--;

            // Deux passes décalées d'un pixel en X : l'ombre puis le texte.
            uint* labelOt = otBase + projected[2] + 0x14;
            primCursor = DrawSpritePrimitive(labelOt, primCursor, (int*)labelSprite, 2, 0);
            *(short*)(labelSprite + 8) -= 1;
            primCursor = DrawSpritePrimitive(labelOt, primCursor, (int*)labelSprite, 2, 0);

            atlas[i * 8 + 2]++;
            atlas[i * 8 + 3]++;
        }

        // Ghidra: les deux passes de texte finales — le nom de la classe puis celui du
        // personnage courant, toutes deux dans le premier descripteur d'interface.
        BindMenuLabels(s_Unknown_005606f8, 0, s_UiDescriptors_00560ff0, 0);
        primCursor = RunTextEngine(primCursor, s_UiDescriptors_00560ff0);

        BindMenuLabels(
            *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70) + 0xc + scratch[0x150] * 4),
            0, s_UiDescriptors_00560ff0, 0);
        primCursor = RunTextEngine(primCursor, s_UiDescriptors_00560ff0);

        return primCursor;
    }

    /// <summary>
    /// Ghidra: la boucle d'initialisation en tête de FUN_00506880 — neuf structures de 0x8c8 octets
    /// à partir de 0x00573518, dont seuls les 0x68 premiers octets sont écrits.
    ///
    /// <para>Le type est identifiable : <c>DAT_00560728</c> et <c>DAT_00560ff0</c> sont deux
    /// instances **statiques** du même type, lisibles dans <c>Legacy/Blaze.exe</c>, et 14 des 26
    /// premiers dwords y sont identiques à ce que cette boucle écrit — dont le <c>0x31</c> de tête
    /// et l'alternance de <c>0x01000000</c>. <c>FUN_005443a0</c>, le moteur de texte de l'interface,
    /// reçoit <c>DAT_00560728</c> (<c>Blaze.c:55881</c>), donc ces structures sont les descripteurs
    /// que ce moteur consomme. Leur disposition champ par champ reste à établir, d'où les
    /// constantes brutes ci-dessous : les transcrire telles quelles est fidèle, les nommer serait
    /// inventer.</para>
    /// </summary>
    private static void InitialiseCharacterSelectSlots()
    {
        for (int slot = 0; slot < CharacterSelectSlotCount; slot++)
        {
            uint* entry = (uint*)(s_SelectSlots_00573518 + slot * CharacterSelectSlotStride);

            // Ghidra: puVar4 pointe 8 octets après la base, d'où les indices négatifs de l'original.
            // Réécrits ici en décalages depuis la base, dans l'ordre des octets plutôt que dans
            // l'ordre — arbitraire — où Ghidra les a émis.
            entry[0] = 0x31;
            entry[1] = 0;
            entry[2] = 0;
            entry[3] = 0;
            entry[4] = 0x10180000;
            entry[5] = (uint)(slot + 3);
            entry[6] = 0x010f0808;
            entry[7] = 0x02cf1020;
            entry[8] = 0x01070c10;
            entry[9] = 0x01000000;
            entry[10] = 0;
            entry[11] = 0x01000000;
            entry[12] = 0x000f0000;
            entry[13] = 0x01000000;
            entry[14] = 0x000f0c00;
            entry[15] = 0x01000000;
            entry[16] = 0x010f0000;
            entry[17] = 0x01000000;

            // Ghidra: la queue — quatre paires (0x000f0000, 0x01000000) consécutives.
            for (int i = 0; i < 4; i++)
            {
                entry[18 + i * 2] = 0x000f0000;
                entry[19 + i * 2] = 0x01000000;
            }
        }
    }

    // Ghidra: &DAT_00573518 — neuf structures contiguës de 0x8c8 octets (le pas 0x232 dwords de la
    // boucle). L'original les prend dans .data ; ici c'est un bloc alloué, comme les autres.
    private const int CharacterSelectSlotCount = 9;
    private const int CharacterSelectSlotStride = 0x8c8;

    // Alloué bas : ce sont neuf instances du type de s_UiDescriptors_00560ff0, en-tête compris
    // (InitialiseCharacterSelectSlots écrit les mêmes 26 dwords). Aucun des sous-états déjà
    // transcrits ne les passe encore au moteur de texte, mais dès qu'un le fera l'ordering
    // table de +0xa4 y sera masquée à 0xffffff comme sur les autres. Vingt kilo-octets de la
    // fenêtre basse, contre une panne à retrouver plus tard.
    private static byte* s_SelectSlotsStorage;
    private static byte* s_SelectSlots_00573518
        => s_SelectSlotsStorage != null
            ? s_SelectSlotsStorage
            : (s_SelectSlotsStorage =
                   AllocLowZeroed((uint)(CharacterSelectSlotCount * CharacterSelectSlotStride)));

    // Ghidra: DAT_00573338 — le bloc de 0x2000 octets que la boucle place à MainMem + 0xf0000.
    // C'est la même adresse que la destination de la liste +0xb0 de LoadCharacterSelectResources :
    // le chargement y dépose des blocs bruts, la boucle de frame l'efface puis le relit
    // (`*(byte *)(DAT_00573338 + 0x151)` sert d'index dans la table de couleurs DAT_00562a58).
    private static byte* s_SelectScratch_00573338;

    // =====================================================================
    // Les huit sous-états de la sélection des personnages
    //
    // Ghidra: PTR_LAB_00560708 @ 0x00560708 — table de huit pointeurs relevée dans
    // Legacy/Blaze.exe au décalage fichier 0x15ED08. FUN_00506880 y répartit chaque frame par
    // DAT_00578de8. Aucun de ces huit corps n'existe dans Blaze.c : seule cette table les désigne,
    // donc Ghidra ne les a jamais désassemblés. Un re-run ciblé sur ces huit adresses les produit
    // d'un coup.
    //
    // Vérifié dans le binaire : les huit sont de vrais prologues de fonction, et **sept sur huit**
    // ouvrent sur la même instruction, `mov eax, [0x00562A48]` — le drapeau « première frame de ce
    // sous-état » que LoadCharacterSelectResources met à 1 et que FUN_00506880 relève à chaque
    // changement de sous-état. Le premier lit `[0x005633A8]` à la place.
    // =====================================================================

    // Ghidra: &DAT_00560708 — la table elle-même, relevée dans Legacy/Blaze.exe au décalage
    // fichier 0x15ED08. Les huit corps sont désormais décompilés et rangés dans
    // Legacy/Raw/Blaze/FUN_<adresse>.c ; il reste à les transcrire.
    private static readonly int[] s_CharacterSelectSubStateAddresses =
    {
        0x00507010, 0x00507cb0, 0x00508410, 0x00508860,
        0x00508e00, 0x00509470, 0x005098f0, 0x0050a910,
    };

    // Ghidra: &DAT_00560ff0 et suivants — la rangée de descripteurs d'interface que la boucle de
    // frame dessine, pas 0x8c8 octets. Trois instances statiques, relevées dans Legacy/Blaze.exe
    // aux décalages 0x15F5F0, 0x15FEB8 et 0x160780 ; FUN_00509470 confirme la troisième en
    // référençant 0x00562180 = 0x00560ff0 + 2 * 0x8c8. Seuls les 0x68 premiers octets de chaque
    // sont initialisés — la queue est nulle dans le fichier, vérifié.
    //
    // Le champ +4 est une paire (x, y) empaquetée : les trois boîtes sont à x = 0x28, 0x78, 0xd0
    // pour un même y = 0x18, et le champ +0x14 vaut leur index 0, 1, 2.
    private const int UiDescriptorStride = 0x8c8;
    private const int UiDescriptorCount = 3;
    // Alloué bas. Ces descripteurs portent leur **propre** ordering table à +0xa4, que
    // ClearOTagR remplit de liens masqués à 0xffffff (voir la marche de liens dans
    // DrawUiPanel) : au-dessus de 0x01000000 cette table se relit sur des adresses tronquées.
    // Le masque de RunTextEngine n'est que le premier symptôme du même problème. Dans
    // l'original la question ne se pose pas, ces structures sont des globales de .data.
    //
    // Paresseux plutôt qu'initialiseur de champ, pour la raison exposée sur
    // s_MenuContext_0056dd58 : un échec d'allocation basse doit rester rattrapable par relance,
    // pas se présenter en TypeInitializationException.
    private static byte* s_UiDescriptorsStorage;
    private static byte* s_UiDescriptors_00560ff0
        => s_UiDescriptorsStorage != null
            ? s_UiDescriptorsStorage
            : (s_UiDescriptorsStorage = BuildUiDescriptors());

    private static byte* BuildUiDescriptors()
    {
        byte* block = AllocLowZeroed((uint)(UiDescriptorCount * UiDescriptorStride));

        // Les 0x68 octets initialisés de chaque instance, relevés dans Legacy/Blaze.exe aux
        // décalages 0x15F5F0, 0x15FEB8 et 0x160780 — 26 dwords, pas 8. Les mots de fonction
        // sont testés par DrawUiPanel en `champ & 0x1000000` : sans f8 (texte) ni f16 (cadre),
        // le panneau se dessine vide, exactement comme le descripteur du menu principal quand il
        // était laissé à zéro.
        //   f5   l'index de la boîte (0, 1, 2)      f12  curseur      — bit 0x1000000 absent
        //   f8   texte        — bit présent         f14  flèches      — bit absent
        //   f16  cadre        — bit présent
        const int UiDescriptorHeaderDwords = 26;
        ReadOnlySpan<uint> headers = new uint[]
        {
            // 0x00560ff0
            0x00000031, 0x00180028, 0x00180048, 0x00000000,
            0x01080000, 0x00000000, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
            // 0x005618b8
            0x00000031, 0x00180078, 0x00180050, 0x00000000,
            0x01080000, 0x00000001, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
            // 0x00562180
            0x00000031, 0x001800d0, 0x00180038, 0x00000000,
            0x021a0000, 0x00000002, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
        };

        for (int i = 0; i < UiDescriptorCount; i++)
        {
            uint* dest = (uint*)(block + i * UiDescriptorStride);
            for (int f = 0; f < UiDescriptorHeaderDwords; f++)
            {
                dest[f] = headers[i * UiDescriptorHeaderDwords + f];
            }
        }

        return block;
    }

    // Ghidra: &DAT_00560728 — une neuvième instance du même type, celle que la boucle de frame
    // passe au moteur de texte (FUN_005443a0) puis dessine. Relevée au décalage fichier 0x15ED28.
    //
    // Alloué bas et paresseux pour la même raison que s_UiDescriptors_00560ff0 : il traverse
    // RunTextEngine puis DrawUiPanel, donc il porte une ordering table à +0xa4.
    private static byte* s_StatusDescriptorStorage;
    private static byte* s_StatusDescriptor_00560728
        => s_StatusDescriptorStorage != null
            ? s_StatusDescriptorStorage
            : (s_StatusDescriptorStorage = BuildStatusDescriptor());

    private static byte* BuildStatusDescriptor()
    {
        byte* block = AllocLowZeroed((uint)UiDescriptorStride);
        uint* f = (uint*)block;

        // Les 26 dwords du décalage fichier 0x15ED28, queue comprise : f25 manquait.
        f[0] = 0x00000031; f[1] = 0x00a80028; f[2] = 0x002800e0; f[3] = 0x00000000;
        f[4] = 0x021a0000; f[5] = 0x0000000c; f[6] = 0x010f0808; f[7] = 0x02cf1020;
        f[8] = 0x01070c10; f[9] = 0x01000000; f[10] = 0x00000000; f[11] = 0x01000000;
        f[12] = 0x000f28d4; f[13] = 0x01000000; f[14] = 0x000f0a00; f[15] = 0x01000000;
        f[16] = 0x010f0000; f[17] = 0x01000000; f[19] = 0x01000000; f[21] = 0x01000000;
        f[23] = 0x01000000; f[25] = 0x01000000;

        return block;
    }

    // Ghidra: &DAT_00562a98 — quatre octets par sous-état, relevés au décalage fichier 0x161098.
    // Seul le troisième varie (0x02 pour les sous-états 0 à 4, 0x03 pour 5 à 7).
    private static readonly byte* s_SelectSubStateBytes_00562a98 = BuildSelectSubStateBytes();

    private static byte* BuildSelectSubStateBytes()
    {
        byte* table = (byte*)NativeMemory.AllocZeroed(8, 4);
        for (int row = 0; row < 8; row++)
        {
            table[row * 4 + 0] = 0x00;
            table[row * 4 + 1] = 0x01;
            table[row * 4 + 2] = (byte)(row < 5 ? 0x02 : 0x03);
            table[row * 4 + 3] = 0x00;
        }
        return table;
    }

    // Ghidra: DAT_00562b68 — le compteur que le test de sortie consulte au sous-état 7. Jamais
    // écrit par le code reconstruit pour l'instant, donc il lit 0.
    private static int s_Unknown_00562b68;

    // Ghidra: &DAT_005606f8 — le descripteur d'étiquette que le carrousel lie avant sa première
    // passe de texte. Seize octets relevés dans Legacy/Blaze.exe au décalage fichier 0x15ECF8 :
    // {0x0000000c, 0, 0x005066c0, 0}. Il précède immédiatement PTR_LAB_00560708.
    //
    // TODO(REVERSE): le troisième champ est un **pointeur de code**, 0x005066c0 — juste avant
    // FUN_005066d0, et absent de Blaze.c comme les huit sous-états l'étaient. Si BindMenuLabels
    // appelle à travers, ça se verra à l'exécution ; il faudra alors un re-run Ghidra ciblé de
    // plus sur cette adresse.
    private static readonly byte* s_Unknown_005606f8 = BuildLabelDescriptor005606f8();

    private static byte* BuildLabelDescriptor005606f8()
    {
        byte* block = (byte*)NativeMemory.AllocZeroed(0x10);
        ((uint*)block)[0] = 0x0000000c;
        ((uint*)block)[1] = 0;
        ((uint*)block)[2] = 0x005066c0;
        ((uint*)block)[3] = 0;
        return block;
    }


    // Ghidra: DAT_005e9308 / 930a / 930c — l'orientation de la scène, en pas de 0x1000 par tour
    // (la convention des tables rsin/rcos de winsys3d, ordinaux 450/451).
    private static int s_ScenePitch_005e9308, s_SceneYaw_005e930a, s_SceneRoll_005e930c;

    // Ghidra: DAT_005e93a0 / 93a2 / 93a4 — la couleur de fond passée à Ordinal_376 (SetBackColor).
    private static int s_BackColourR_005e93a0, s_BackColourG_005e93a2, s_BackColourB_005e93a4;

    // Ghidra: DAT_00604928 — trois shorts contigus, la pose de caméra. Le troisième (+2) est
    // l'angle amorti vers le personnage courant ; FUN_0051c300 consomme le tout.
    private static readonly byte* s_CameraVector_00604928 = (byte*)NativeMemory.AllocZeroed(8);

    // Ghidra: DAT_006008d0 / d4 / d8 — le décalage monde appliqué à la position de l'entité avant
    // projection. Remis à zéro à l'entrée du sous-état 0.
    private static int s_Unknown_006008d0, s_Unknown_006008d4, s_Unknown_006008d8;

    // Ghidra: &DAT_006008e0 — la matrice de caméra, **un seul bloc de 0x20 octets**, exactement la
    // MATRIX du SDK PlayStation : neuf shorts de rotation (0x006008e0..0x006008f1) puis les trois
    // translations à +0x14, +0x18 et +0x1c, que Ghidra nomme DAT_006008f4, DAT_006008f8 et
    // **DAT_006008fc**.
    //
    // Ce dernier point mérite d'être explicite : DAT_006008fc n'est pas une variable isolée. C'est
    // la translation Z de cette matrice, et la boucle de frame l'amortit vers 800 à chaque image
    // (`DAT_006008fc = DAT_006008fc + (800 - DAT_006008fc >> 2)`) — autrement dit elle fait avancer
    // la caméra. Déclarer les trois translations à part de la rotation couperait la structure en
    // deux et FUN_0051d1f0 recopierait une matrice à moitié fausse dans le registre du GTE.
    private static readonly byte* s_CameraMatrix_006008e0 = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>Ghidra: DAT_006008f4 / f8 / fc — la rangée de translation de la matrice ci-dessus.</summary>
    private static int* CameraTranslation => (int*)(s_CameraMatrix_006008e0 + 0x14);

    // Ghidra: &DAT_00578420 — les seize emplacements de personnage du carrousel, pas 0x9c octets
    // (le 0x4e du pas `ushort *` de Ghidra, et le 0x27 de la copie par dwords en case 2). C'est le
    // même pas 0x9c que la table DAT_005e93c8 que CurrentPadState indexe.
    private const int CharacterSlotCount = 16;
    private const int CharacterSlotStride = 0x9c;
    private static readonly byte* s_CharacterSlots_00578420 =
        (byte*)NativeMemory.AllocZeroed(CharacterSlotCount, CharacterSlotStride);

    // Ghidra: &DAT_00572b48 — la destination de la copie faite à la validation : l'emplacement
    // choisi devient l'entité de scène que le sous-état suivant anime.
    //
    // Tout ce que FUN_00506880 nomme DAT_00572b4c / 52 / 60 / 62 / 64..6c / 70..74 tombe dedans :
    // ce sont ses champs +0x04, +0x0a, +0x18, +0x1a, +0x1c..+0x24 et +0x28..+0x2c. Il n'y a donc
    // aucun « bloc de globales 0x00572b40 » à modéliser à côté — c'est le même enregistrement de
    // 0x9c octets qu'un emplacement du carrousel, et les six décalages le confirment un à un.
    private static readonly byte* s_SceneEntity_00572b48 =
        (byte*)NativeMemory.AllocZeroed((nuint)CharacterSlotStride, 1);

    // Ghidra: &DAT_00562a58 — huit teintes de fond, trois shorts et un mot de bourrage chacune,
    // relevées au décalage fichier 0x161058. La table s'arrête net sur DAT_00562a98, huit entrées
    // plus loin, ce qui en donne la taille sans avoir à la deviner.
    private const int SceneToneStride = 8;
    private static readonly byte* s_SceneToneTable_00562a58 = BuildSceneToneTable();

    private static byte* BuildSceneToneTable()
    {
        ReadOnlySpan<short> tones =
        [
            192,  96,   0, 0,      0,  96, 192, 0,
            128,  96,  64, 0,     64, 128,  96, 0,
            192, 192, 192, 0,      0,   0,   0, 0,
            128, 128,  96, 0,     64,  32, 128, 0,
        ];

        byte* block = (byte*)NativeMemory.AllocZeroed(8, SceneToneStride);
        for (int i = 0; i < tones.Length; i++) ((short*)block)[i] = tones[i];
        return block;
    }

    // Ghidra: &DAT_00572be4 — les particules de la scène : quatre enregistrements de 0x9c octets,
    // du même type que l'entité ci-dessus, et **contigus avec elle** (0x00572b48 + 0x9c = 0x00572be4).
    //
    // La base a demandé deux lectures croisées, parce que les deux boucles qui parcourent ce
    // tableau ne le désignent pas au même endroit :
    //   FUN_00506880  part de &DAT_00572c34, pas 0x27 ints, borne 0x572ea4, et travaille sur
    //                 `piVar10 - 0xd` — soit 0x572c00, 0x572c9c, 0x572d38, 0x572dd4 ;
    //   FUN_00508e00  part de &DAT_00572c20, même pas, borne 0x572e90, et recopie 0x27 dwords vers
    //                 `puVar6 - 0xf` — soit 0x572be4, 0x572c80, 0x572d1c, 0x572db8.
    // Les secondes valent les premières moins 0x1c : la boucle de frame manipule le **champ de
    // position** (+0x1c), pas la base. Quatre records dans les deux cas, et la contiguïté avec
    // l'entité confirme le tout.
    private const int SceneParticleCount = 4;
    private const int SceneParticleStride = 0x9c;
    private static readonly byte* s_SceneParticles_00572be4 =
        (byte*)NativeMemory.AllocZeroed(SceneParticleCount, SceneParticleStride);

    // Ghidra: DAT_00560fef, DAT_0056075b, DAT_00560763 — l'état des boîtes de dialogue modales que
    // le sous-état 0 ouvre puis attend. TODO(REVERSE): elles sont posées par le code de dialogue,
    // non reconstruit, donc elles lisent 0 et les branches d'annulation ne sont pas encore
    // atteignables.
    private static byte s_Unknown_00560fef, s_Unknown_0056075b, s_Unknown_00560763;

    // Ghidra: DAT_005e909f — testé contre 0 avant chaque transition. Écrit par FUN_00509470
    // (sous-état 5), non reconstruit.
    private static byte s_Unknown_005e909f;

    /// <summary>
    /// Ghidra: FUN_005352e0 @ 0x005352e0 — la recopie par dwords que le sous-état 0 utilise pour
    /// figer l'emplacement choisi. TODO(REVERSE): le corps de FUN_005352e0 n'est pas lu ; il est
    /// pris ici pour un `memcpy` de `count` dwords parce que les deux sites d'appel du sous-état 0
    /// passent 0x27 (= 0x9c octets, l'emplacement entier) et 10. À confirmer avant de s'y fier.
    /// </summary>
    private static void CopyDwords(uint* source, uint* destination, int count)
    {
        for (int i = 0; i < count; i++) destination[i] = source[i];
    }

    /// <summary>
    /// Ghidra: FUN_00542160 @ 0x00542160 â€” reloads the state's resources. Its directory loop is the
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

    /// <summary>Ghidra: FUN_00542130 @ 0x00542130 â€” game state 0's entry point: the main menu.</summary>
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

    /// <summary>Ghidra: FUN_00529ad0 @ 0x00529ad0 â€” starts a music track by table index.</summary>
    private static void PlayMusicTrack(int trackIndex)
    {
        // Ghidra: patches the path's first character with the CD drive letter, then starts the
        // stream. The table entries are CD-**root** absolute templates ("@:/Music/Mel00_01.WAV"),
        // recovered at PTR_s____Music_Mel00_01_WAV_00566cc8 â€” see Documentation/DataSymbols.md.
        if (trackIndex < 0 || trackIndex >= s_MusicTrackPaths.Length) return;
        byte* path = s_MusicTrackPaths[trackIndex];
        if (path == null) return;
        path[0] = s_CdDataRootPath[0];
        StartMusicStream(path);
    }

    /// <summary>
    /// Ghidra: FUN_00542250 @ 0x00542250 â€” the main menu's frame loop. Runs until a menu entry is
    /// chosen, then sets the next game state (or exits) and returns.
    /// </summary>


    private static void RunMainMenuFrameLoop()
    {
        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);

        PlayMusicTrack(6);

        // Ghidra: DAT_005e9898 = 1; DAT_005e9908 = 1 â€” the two draw envs' `isbg`.
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

            // ECHAFAUDAGE TEMPORAIRE (OPENFORESIA_TRACE_MENU=1) : pourquoi la validation ouvre
            // l'entrÃ©e 0 mais pas l'entrÃ©e 3. On relÃ¨ve ce que la boucle voit rÃ©ellement.
            if (s_TraceMenu) TraceMenuDescriptorChanges();

            if ((s_FadeCounter_005e9960 & 0xf) != 8 && level > 0x7f)
            {
                s_FadeCounter_005e9960--;
                level = 0x80;

                primCursor = RunTextEngine(primCursor, s_MenuContext_0056dd58);  // Ghidra: FUN_005443a0
                primCursor = DrawUiPanel(otEntry, primCursor, s_MenuContext_0056dd58);  // Ghidra: FUN_00545340

                int gammaBefore = s_Unknown_005d9de0;

                if ((s_Unknown_0056e61f & 4) == 0)
                {
                    switch (s_MenuSelection_0056e61c)
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

                            // DEVIATION, une ligne, et mesurée. La page d'options se quitte par
                            // Échap, qui est lié au bouton 0x4000 de la manette. Au retour, la
                            // première image du menu voit ce bouton **encore enfoncé**, son panneau
                            // le prend pour une validation et rouvre aussitôt la page : à l'écran,
                            // un aller-retour instantané au menu principal.
                            //
                            // Le jeu a son propre idiome pour ça — la boucle de vidange juste
                            // au-dessus, `while (FUN_005048f0() != 0)`, qui attend le relâchement
                            // avant d'entrer dans la page. On l'applique symétriquement à la
                            // sortie.
                            //
                            // TODO(REVERSE): l'original n'a pas cette seconde vidange, ce qui veut
                            // dire que son panneau distingue une touche *nouvellement* enfoncée
                            // d'une touche maintenue — un front — là où cette reconstruction lit
                            // l'état maintenu (`CurrentPadWord`, le mot d'état, pas le mot de
                            // fronts que `UpdateButtonEdges` calcule juste à côté). C'est là qu'est
                            // le correctif propre ; celui-ci en tient lieu en attendant, et se
                            // retire d'une ligne le jour où la sémantique du panneau est établie.
                            //
                            // La boucle est **cadencée** par Ordinal_132, contrairement à celle du
                            // dessus : sans elle, elle tourne à vide sans laisser la pompe de
                            // messages s'exécuter, et l'état DirectInput ne se rafraîchit jamais —
                            // mesuré, le jeu restait figé dans la page. C'est la même attente que
                            // l'éditeur de touches utilise entre deux captures.
                            UiPageTrace("options : retour au menu, vidange…");
                            while (InputDrainStub() != 0) { Ordinal_132(0); }

                            // …et on referme la garde de validation. Sans cela le `switch`
                            // ci-dessus se redéclenche à l'image suivante sur la même entrée :
                            // mesuré, `Clear(TARGET)` reste figé pendant que `BeginScene` continue
                            // de compter, signe que le menu n'a jamais repris la main.
                            //
                            // Le bit 2 est celui que `Handler_00544b10` lève quand le flux de texte
                            // d'une page se termine, et qui referme la garde jusqu'à la validation
                            // suivante. Le rétablir ici, c'est remettre le menu dans l'état où il
                            // était avant d'ouvrir la page.
                            //
                            // **Les deux champs, pas un seul.** L'état interactif du menu est le
                            // couple `[0x3b] == 1` et bit 2 levé — c'est ce que le relevé par image
                            // montre en régime stable, et la validation les fait tomber ensemble
                            // (`+0x03B:1->0` et `+0x8C7:05->01`). Ne relever que le bit 2 referme
                            // la garde sans marquer la page terminée : le menu redevient visible
                            // mais son curseur ne répond plus.
                            //
                            // `Handler_00544b10` remet en plus le curseur à zéro (`[0x8c4] = 0`) ;
                            // on ne le fait pas, pour laisser la sélection là où le joueur l'avait.
                            s_MenuContext_0056dd58[0x3b] = 1;
                            s_MenuContext_0056dd58[0x8c7] |= 4;
                            UiPageTrace("options : vidange terminée, garde refermée");

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

            // Ghidra: the per-frame draw of the menu **background sprite**.
            //
            // `local_14` .. `local_4` are ONE contiguous 20-byte structure on the original's
            // stack, and it is `&local_14` that both renderers receive:
            //
            //     +0x00  local_14  descriptor pointer, from the resource directory
            //     +0x04  local_10  colour â€” the fade's grey, level * 0x10101
            //     +0x08  local_c   x = 0xa0 (160)      +0x0a  local_a  y = 0x78 (120)
            //     +0x0c  local_8   tpage = 0x1420      +0x0e  local_6  clut  = 0x1149
            //     +0x10  local_4   0
            //
            // This had been reconstructed as **two separate heap blocks**, so the sprite reached
            // the renderers with a null position, size and texture page â€” which is why no
            // background was drawn on either rendering path. Same shape of bug as the four
            // DRAWENV/DISPENV structures, whose contiguity is likewise a contract; it is the
            // fifth occurrence in this project, and the second where the evidence was "a `DAT_`
            // or a local that is really a field of a larger structure".
            byte* sprite = s_MenuBackgroundSprite;
            *(uint*)(sprite + 0x04) = level * 0x10101;
            *(ushort*)(sprite + 0x08) = 0xa0;
            *(ushort*)(sprite + 0x0a) = 0x78;
            *(ushort*)(sprite + 0x0c) = 0x1420;
            *(ushort*)(sprite + 0x0e) = 0x1149;
            *(uint*)(sprite + 0x10) = 0;

            // Ghidra: local_14 = *(*(DAT_005e97c4 + 0x94) + 4)
            if (s_MainResourceDirectory_005e97c4 != null)
            {
                int* table = *(int**)(s_MainResourceDirectory_005e97c4 + 0x94);
                if (table != null) *(int*)sprite = table[1];
            }

            primCursor = DrawSpritePrimitive(otEntry, primCursor, (int*)sprite, 0, 0);
            primCursor = DrawFlatPrimitive(otEntry, primCursor, (int*)sprite);
            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        }
    }

    /// <summary>
    /// Ghidra: `&local_14` in FUN_00542250 â€” the menu background sprite's descriptor, 20 bytes,
    /// **one contiguous block**. See the layout comment at its only use site; splitting it is what
    /// kept the background from being drawn.
    /// </summary>
    private static readonly byte* s_MenuBackgroundSprite = (byte*)NativeMemory.AllocZeroed(0x20);

    private static readonly bool s_TraceMenu =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_MENU") == "1";

    /// <summary>
    /// ECHAFAUDAGE TEMPORAIRE (<c>OPENFORESIA_TRACE_MENU=1</c>). Reports which bytes of the menu
    /// descriptor change from one frame to the next, alongside the pad word that caused it. The
    /// cursor visibly moves, so whatever holds its position must show up here â€” the same method
    /// that identified <c>menuContext[0x8c7]</c>.
    /// </summary>
    private static void TraceMenuDescriptorChanges()
    {
        const int extent = 0x918;
        byte* context = s_MenuContext_0056dd58;
        s_MenuTraceSnapshot ??= new byte[extent];

        ushort pad = CurrentPadWord();
        var changes = new System.Text.StringBuilder();
        int changed = 0;

        for (int i = 0; i < extent; i++)
        {
            if (context[i] == s_MenuTraceSnapshot[i]) continue;
            if (++changed <= 12)
                changes.Append($" +0x{i:X3}:{s_MenuTraceSnapshot[i]:X2}->{context[i]:X2}");
            s_MenuTraceSnapshot[i] = context[i];
        }

        if (changed != 0 || pad != 0)
            Console.Error.WriteLine(
                $"[MENU] pad=0x{pad:X4} fade={s_FadeCounter_005e9960} " +
                $"drapeaux=0x{s_Unknown_0056e61f:X2} changÃ©s={changed}{changes}");
    }

    private static byte[]? s_MenuTraceSnapshot;

    /// <summary>
    /// Ghidra: the entity-slot initialisation loop at the top of FUN_00542250 â€” four 0x4c-byte
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
    /// Ghidra: FUN_0051f9c0 @ 0x0051f9c0 â€” links a two-word flat primitive into the ordering table.
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
    /// Ghidra: FUN_00521f20 @ 0x00521f20 â€” links a ten-word textured sprite primitive into the
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

    /// <summary>
    /// Ghidra: FUN_00521df0 @ 0x00521df0 — la sœur courte de <see cref="DrawSpritePrimitive"/> :
    /// une primitive de **cinq** mots (longueur de tag 0x04000000) au lieu de dix. Même en-tête —
    /// même lecture du couple couleur/attributs à <c>sprite[0] + (short)sprite[4] * 8</c>, même
    /// garde sur les deux drapeaux — mais elle ne pose qu'un seul coin au lieu de quatre.
    ///
    /// <para>Le champ d'échelle se lit à <c>(colour >> 0x10) &amp; 0xff</c> et **0 y signifie
    /// 0x100**, pas 0xff : la sœur longue teste la même chose mais retombe sur 0xff. L'écart est
    /// dans l'original, transcrit tel quel.</para>
    ///
    /// <para>TODO(REVERSE) : le second drapeau est <c>DAT_00564a4c</c>, qui n'a pas de symbole
    /// reconstruit. <see cref="DrawSpritePrimitive"/> lui substitue <c>s_SmallTextureFlag</c>, mais
    /// celui-ci est <c>DAT_00564a38</c> (soit <c>AU_00564a05._51_4_</c>) — deux adresses
    /// différentes. Plutôt que de propager un rapprochement douteux, on déclare ici le symbole
    /// propre ; la divergence est à trancher, et elle touche aussi la sœur longue.</para>
    /// </summary>
    private static uint* DrawSpriteVariant(uint* otEntry, uint* prim, int* sprite, uint flags, uint extra)
    {
        uint* source = (uint*)(*(int*)sprite + (short)sprite[4] * 8);
        uint colour = source[0];
        uint posX = (uint)sprite[2];
        uint attributes = source[1];

        if (s_Unknown_00564a48 != 0)
        {
            if ((attributes & 0x200) != 0)
            {
                if (s_Unknown_00564a4c == 0) return prim;
                attributes &= 0xfffffdff;
            }
            if ((flags & 2) != 0)
            {
                if (s_Unknown_00564a4c == 0) return prim;
                flags = (byte)flags & 0xfdu;
            }
        }

        uint scale = (colour >> 0x10) & 0xff;
        if ((short)scale == 0) scale = 0x100;

        // Ghidra: &DAT_005e8f02 — la moitié haute de l'entrée de la table de pages, d'où le +2 sur
        // la même table que DrawSpritePrimitive indexe à &DAT_005e8f00.
        ushort page = *(ushort*)(s_EntityDecodeTable_005e8f00 + 2 + ((attributes >> 0x10) & 0xff) * 4);

        prim[1] = ((attributes >> 8) | (flags & 0xff) | 100) << 0x18 | ((uint)sprite[1] & 0x3ffffff);
        prim[3] = (uint)(((((ushort)((short)extra >> 2)) & 0xffc0) | (extra & 0xff)) + page + (attributes >> 0x18))
                  * 0x10000 | (colour & 0xffff);
        prim[2] = posX;
        prim[4] = (colour >> 0x18) << 0x10 | scale;

        *prim = *otEntry | 0x4000000;
        *otEntry = (uint)(nuint)prim;

        return prim + 5;
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

    // Ghidra: &DAT_005e0300 â€” the 4bpp glyph staging arena, addressed as (bank + line) * 0x800.
    // The next named global is DAT_005e8300, which puts its size at 0x8000 â€” sixteen banks.
    private static readonly byte* s_GlyphStaging_005e0300 = (byte*)NativeMemory.AllocZeroed(0x8000);

    /// <summary>
    /// The 4bpp glyph staging bank for one line, bounds-checked. The buffer holds 16 banks of
    /// 0x800 bytes. Its extent in the original is inferred from where the next named global sits,
    /// so an index past it is a defect in this reconstruction rather than original behaviour â€” and
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
                $"tpage/clut slot {index} is past the 1024-entry table at DAT_005e8f00 â€” the " +
                "descriptor asking for it is not the one this table was filled for.");
        }
        return (uint*)(s_EntityDecodeTable_005e8f00 + index * 4);
    }

    private static byte* s_StagingWrite_005e8304;   // Ghidra: DAT_005e8304 â€” staging write pointer
    private static int s_TextState_005e8308;        // Ghidra: DAT_005e8308 â€” 0 / 1 / 2 / -1, see below
    private static byte* s_TrimAnchor_005e830c;     // Ghidra: DAT_005e830c â€” leading-space trim anchor
    private static uint* s_OtCursor_005e8314;       // Ghidra: DAT_005e8314 â€” OT cursor in the descriptor
    private static int s_LineIndex_005e8318;        // Ghidra: DAT_005e8318 â€” lines emitted this page
    private static uint* s_UploadCursor_005e831c;   // Ghidra: DAT_005e831c â€” the node cursor, in and out
    private static uint s_PenX_005e8320;            // Ghidra: DAT_005e8320 â€” pen X, in 4bpp columns
    private static uint* s_SpriteCursor_005e8324;   // Ghidra: DAT_005e8324 â€” sprite cursor in the descriptor
    private static uint s_BreakPen_005e8328;        // Ghidra: DAT_005e8328 â€” pen saved at the last break
    private static uint s_BreakColumn_005e8300;     // Ghidra: _DAT_005e8300 â€” column saved at the last break
    private static byte* s_BreakStaging_005e02fc;   // Ghidra: DAT_005e02fc â€” staging ptr at the last break
    private static int s_GlyphMode_005dffd0;        // Ghidra: DAT_005dffd0 â€” 0 none / 1 latin / 2 DBCS
    private static int s_Unknown_005dffe8;          // Ghidra: DAT_005dffe8

    /// <summary>
    /// Ghidra: &amp;DAT_0056e7b0 â€” proportional glyph widths, one byte per folded slot.
    /// <para>
    /// **Taken from `FUN_00507890`, not from the `.data` image.** The two differ, and the `.data`
    /// copy is the stale one: `FUN_00507890` builds the real table as 186 stack literals and copies
    /// it over (`FUN_00507870` is its fixed-width counterpart, every entry 8). The `.data` copy
    /// gives `I`=6/`J`=2 and `M`=5/`N`=8 â€” impossible for a proportional font, and the direct cause
    /// of the uneven spacing: narrow glyphs advanced too far, wide ones not far enough. This table
    /// gives `I`=2, `M`=8, `i`=2, `l`=2, `m`=8, `t`=4, `Q`=8.
    /// </para>
    /// The folding that indexes it is confirmed against the machine code at 0x00543EE3:
    /// `CMP SI,0x41 / LEA EAX,[ESI-0x22] / JAE / LEA EAX,[ESI-0x21]`, then
    /// `MOVSX AX,[ESI + 0x0056E7B0]` â€” so both the folding and the base are faithful as written.
    /// <para>
    /// TODO(REVERSE): neither setter has a call site in `Blaze.c` â€” both are reached only through
    /// an address-taken table, so which one runs when is not established. The proportional one is
    /// installed here because the original demonstrably renders proportional text.
    /// </para>
    /// <para>
    /// **Both setters now have a call site**, which settles the TODO above: the name-entry
    /// sub-state (<c>FUN_00507cb0</c>) calls <c>FUN_00507870</c> before laying out its
    /// eight-row character grid and <c>FUN_00507890</c> immediately after. The grid needs every
    /// cell the same width to line up, so the screen switches to the fixed-width table for the
    /// grid and back to the proportional one for everything else. The table is therefore mutable,
    /// not a constant.
    /// </para>
    /// </summary>
    private static readonly byte[] s_GlyphWidthsProportional =
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

    // La table réellement consultée. Déclarée **après** la table proportionnelle : les
    // initialiseurs de champs statiques s'exécutent dans l'ordre du texte, et l'inverse donnerait
    // un Clone() sur null au constructeur de type.
    private static readonly byte[] s_GlyphWidths_0056e7b0 = (byte[])s_GlyphWidthsProportional.Clone();

    /// <summary>Ghidra: FUN_00507870 @ 0x00507870 — installe la table fixe, toutes les entrées à 8.</summary>
    private static void UseFixedGlyphWidths()
    {
        for (int i = 0; i < s_GlyphWidths_0056e7b0.Length; i++) s_GlyphWidths_0056e7b0[i] = 8;
    }

    /// <summary>Ghidra: FUN_00507890 @ 0x00507890 — restaure la table proportionnelle.</summary>
    private static void UseProportionalGlyphWidths()
        => s_GlyphWidthsProportional.CopyTo(s_GlyphWidths_0056e7b0, 0);

    // Ghidra: memmove â€” FUN_00547a00 is 420 lines of unrolled MSVC block copy. Per the project's
    // rule, a recognised CRT routine is called rather than transcribed.
    [DllImport("msvcrt.dll", EntryPoint = "memmove", CallingConvention = CallingConvention.Cdecl)]
    private static extern void* CrtMemMove(void* dest, void* src, nuint count);

    /// <summary>
    /// Ghidra: FUN_0051c0e0 @ 0x0051c0e0 â€” advances a cheat-code matcher over every character the
    /// text engine draws, against the string at <c>DAT_00564a40</c>.
    /// </summary>
    private static void CheatCodeProbe(byte ch)
    {
        // TODO(REVERSE): DAT_00564a40 is the expected sequence and DAT_005633a8 the latch it
        // clears. Both live in initialised .data; recovering them only changes whether a cheat
        // can be entered, never whether text draws, so the matcher is kept structurally faithful
        // with an empty sequence â€” which simply never matches.
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
    /// Ghidra: FUN_00543980 @ 0x00543980 â€” enqueues the finished line's staging bank as a VRAM
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
    /// Ghidra: FUN_00543a10 @ 0x00543a10 â€” links the finished line's textured sprite into the
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
    /// Ghidra: FUN_00544cb0 @ 0x00544cb0 â€” closes the current line: trims the leading run the
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
    /// Ghidra: FUN_00544c50 @ 0x00544c50 â€” pass-2 handler for control code <c>0x0c</c>: begins a
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
        return text;   // Ghidra returns unaff_ESI â€” the caller's cursor, unchanged
    }

    /// <summary>
    /// Ghidra: FUN_00544d90 @ 0x00544d90 â€” pass-2 handler for control code <c>0x0d</c>: ends the
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
    /// Pass-2 handler for control code <c>0x09</c> â€” end of page / wait. **Decoded by hand from
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

        descriptor[0x8c5] = *text;          // the operand byte â€” '?' for the main menu
        text++;
        *(byte**)(descriptor + 0x8ac) = text;
        return text;
    }

    /// <summary>
    /// Handler for control code <c>0x0f</c>, in **both** passes â€” the table has <c>0x00544e50</c>
    /// at slot 15 on the scan side as well as the render side. **Decoded by hand from
    /// Legacy/Blaze.exe** (file offset <c>0x144250</c>, 33 bytes then <c>0x90</c> padding); Ghidra
    /// never emitted it, since it is reached only through the two tables.
    /// <code>
    /// 8B 4C 24 08        MOV ECX,[ESP+8]      ; descriptor
    /// 8A 81 C7 08 00 00  MOV AL,[ECX+0x8C7]   ; flags
    /// C6 41 33 01        MOV byte [ECX+0x33],1
    /// 0C 02              OR  AL,2
    /// 88 81 C7 08 00 00  MOV [ECX+0x8C7],AL
    /// 8B 44 24 04        MOV EAX,[ESP+4]      ; text cursor, returned unchanged
    /// 89 81 AC 08 00 00  MOV [ECX+0x8AC],EAX
    /// C3                 RET
    /// </code>
    /// <para>Sibling of <see cref="Handler_00544b10"/> (code <c>0x09</c>) and it reads best against
    /// it. Three differences: it raises flag bit **1** rather than bit 2, it writes <c>+0x33</c>
    /// rather than <c>+0x3b</c>, and it consumes **no operand byte** â€” the returned cursor is the
    /// one it was handed.</para>
    /// <para><b>What the two byte writes are.</b> Neither is a loose flag: they are the **top byte
    /// of a feature dword** in the descriptor header, i.e. its <c>0x1000000</c> enable bit. The
    /// header is a row of (parameter, enable) pairs â€” see Documentation/DataSymbols.md â€” with the
    /// cursor's parameter at <c>+0x30</c> and the scroll arrows' at <c>+0x38</c>. So
    /// <c>[ECX+0x33] = 1</c> turns <c>0x000F28D4</c> into <c>0x010F28D4</c>: it **switches the
    /// cursor on**, exactly as code <c>0x09</c> switches the scroll arrows on through
    /// <c>+0x3b</c>.</para>
    /// <para>Flag bit 1 is tested at the top of <see cref="RunTextEngine"/>, which returns at once
    /// when it is set. Code <c>0x0f</c> therefore means <b>stop here and show the cursor</b> â€” a
    /// prompt awaiting a choice, which is what the character-select screen reaches on validating a
    /// character.</para>
    /// </summary>
    private static byte* Handler_00544e50(byte* text, byte* descriptor)
    {
        byte flags = descriptor[0x8c7];
        descriptor[0x33] = 1;               // le bit 0x1000000 du dword de curseur, à +0x30
        flags |= 2;
        descriptor[0x8c7] = flags;

        *(byte**)(descriptor + 0x8ac) = text;
        return text;                        // aucun octet d'opérande consommé
    }

    /// <summary>
    /// Handler for control code <c>0x1c</c>, in both passes — <c>0x00544e80</c>, decoded by hand
    /// from Legacy/Blaze.exe (file offset <c>0x144280</c>). It is a **subroutine jump inside the
    /// text stream**: it pushes a return pointer onto the descriptor's stack at <c>+0x8b4</c> and
    /// returns a different string, chosen by a byte of the player's saved block.
    ///
    /// <para>The matching **pop** was already reconstructed — <see cref="RunTextEngine"/> unwinds
    /// <c>descriptor[0x15]</c> and resumes from <c>+0x8b4 + depth * 4</c> when it meets a NUL. Only
    /// the push side was missing, which is why the stack looked write-only until now.</para>
    ///
    /// <para><b>The substituted value.</b> <c>[0x006087b4]</c> is an IAT slot, so
    /// <c>*(*(0x006087b4))</c> is winsys3d's exported <c>MainMem</c> — the same pointer
    /// <see cref="GetMainMemPtr"/> returns. The offset <c>slot * 0x2000 + 0xF0153</c> is then
    /// exactly <c>DAT_00573338[0x153]</c> for that player, i.e. the byte sub-state 3 writes: the
    /// arena's per-player 0x2000 block at <c>MainMem + 0xf0000</c>.</para>
    ///
    /// <para>Operand: three bytes. <c>text[0]</c> picks the resource root, <c>text[1..2]</c> a
    /// little-endian index into its <c>+0x70</c> label table, to which the substituted value is
    /// added — so one control code selects one of a run of consecutive strings.</para>
    /// </summary>
    private static byte* Handler_00544e80(byte* text, byte* descriptor)
    {
        byte value = 0;
        uint* owner = *(uint**)(descriptor + 0x8b0);
        if (owner != null && (*owner & 0xfc0000) == 0x800000)
        {
            byte* mainMem = GetMainMemPtr();
            if (mainMem == null)
            {
                throw new NotImplementedException(
                    "FUN_00544e80: winsys3d's MainMem resolves to null, so the per-player block " +
                    "this control code substitutes from cannot be located.");
            }
            value = mainMem[*((byte*)owner + 8) * 0x2000 + 0xF0153];
        }

        // Ghidra: descriptor[0x8b4 + descriptor[0x15] * 4] = text + 3, puis le compteur avance.
        byte depth = descriptor[0x15];
        *(byte**)(descriptor + 0x8b4 + depth * 4) = text + 3;
        descriptor[0x15] = (byte)(depth + 1);

        uint index = (uint)((text[2] << 8) | text[1]) + value;

        // Ghidra: *(int *)(&DAT_005e97c0 + text[0] * 4). Les deux racines sont des champs séparés
        // ici, pas un tableau, donc le choix est explicite plutôt qu'indexé.
        byte* root = text[0] switch
        {
            0 => s_MainResourceBuffer_005e97c0,
            1 => s_MainResourceDirectory_005e97c4,
            _ => throw new NotImplementedException(
                     $"FUN_00544e80: resource root {text[0]} — the original indexes " +
                     "&DAT_005e97c0 by four bytes, and only slots 0 and 1 are modelled."),
        };

        return *(byte**)(*(byte**)(root + 0x70) + index * 4);
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
    /// because that is what the original is, and because the two differ only by substitution â€”
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
                "It needs a scoped Ghidra re-run at that address â€” see Documentation/DataSymbols.md.");
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

        // Les deux tables portent le même corps à ces deux emplacements : il sert aux deux passes,
        // contrairement aux codes dont la passe 1 substitue un talon d'avance.
        t[0x0f] = Handler_00544e50;   // 0x00544e50
        t[0x1c] = Handler_00544e80;   // 0x00544e80

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

    // winsys3d ordinal 57 (winsys3d.c FUN_00a05060) â€” maps a Shift-JIS code to a 16x16 1bpp glyph
    // inside the KANJI resource the DLL locks at startup, or to a blank when it is out of range.
    [DllImport("winsys3d.dll", EntryPoint = "#57")] private static extern ushort* Ordinal_57(uint sjis);

    // Ghidra: DAT_0056e9b4 / _b8 / _bc â€” recovered from Legacy/Blaze.exe. Full-width Shift-JIS
    // punctuation that this renderer squeezes to half width.
    private static readonly uint[] s_HalfWidthCodes_0056e9b4 = [0x8381, 0x7981, 0x6981];

    // Ghidra: &DAT_005dffd8 â€” the 15-byte row scratch the spreading pass fills.
    private static readonly byte* s_DbcsRowScratch_005dffd8 = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>
    /// Ghidra: FUN_00544150 @ 0x00544150 â€” the DBCS/kanji glyph renderer. Fetches a 16x16 1bpp
    /// glyph, spreads each row's bits and unpacks it into the 4bpp staging buffer.
    /// <para>
    /// **Signature confirmed against `Legacy/Raw/FUN_00544150.asm`.** Ghidra renders the descriptor
    /// as <c>unaff_retaddr</c> because the function reuses arg2's stack slot as scratch
    /// (<c>[ESP+0x18]</c>). The listing settles it: at <c>00544318</c>, after four pushes,
    /// <c>MOV EAX,[ESP+0x14]</c> loads **arg1** and it is arg1 that receives <c>+0xe</c> and
    /// <c>+0x10</c> â€” so the call sites' <c>(descriptor, codepoint)</c> is right and the
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
            // CONCAT11(lo, hi) â€” the row is byte-swapped out of the resource.
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
    /// Ghidra: FUN_00543b20 @ 0x00543b20 â€” rasterises one character into the 4bpp staging buffer
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

        if (ch == 0x5e)   // '^' â€” a solid rule to the next 8-column boundary
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

            // Ghidra applies `+ ((pen & 1) + width) / 2 - 0x700` **once**, after the 15-row loop â€”
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

            // Ghidra: DAT_005e8308 = ((0 < DAT_005e8308) - 1 & 0xfffffffd) + 2 â€” a SETcc idiom.
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
    /// the partial word's staged pixels onto it â€” byte-aligned or nibble-shifted by parity.
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
    /// <see cref="RunTextEngine"/> exactly as the original does â€” where it is the identity, since
    /// every descriptor is a <c>.data</c> global of an image based at <c>0x00400000</c>. Keeping
    /// them low here keeps it the identity, instead of turning a faithful mask into corruption.
    /// </summary>
    private static byte* AllocLowZeroed(uint size)
    {
        // The arena needs 2.6 MB contiguous and gets first pick. Static field initialisers run
        // before entry()'s body, so without this a 64 KB descriptor allocated at the very bottom
        // could split the only region large enough and starve it â€” measured, not hypothetical.
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
    /// Ghidra: FUN_005443a0 @ 0x005443a0 â€” the UI text engine. Runs the descriptor's byte stream
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

        // Ghidra: the 0x232-dword block copy into local_8c8 â€” pass 1 must leave no trace.
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
                    // would corrupt the stack â€” the classic shape of a random CLR fatal error â€”
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
                        "terminator â€” same cause as the scan pass, see above.");
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
                    if (*cursor == 0x24)          // '$' â€” an escape, already collected in pass 1
                    {
                        cursor += 2;
                    }
                    else
                    {
                        if (*cursor == 0x26)      // '&' â€” conditional on a collected escape
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
                        $"FUN_005443a0: text completion callback at 0x{completion:X8} â€” no " +
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
    // table is copied into the frame's, offset by the panel's screen position â€” which is why the
    // text engine builds a self-contained list rather than writing into the frame directly.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0051bdb0 @ 0x0051bdb0 â€” links a screen-space clip rectangle, chosen by the
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
    /// Ghidra: FUN_0051be20 @ 0x0051be20 â€” links the panel's viewport rectangle, derived from the
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
    /// Ghidra: FUN_00545160 @ 0x00545160 â€” a six-word textured sprite (tag 0x05000000).
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
    /// Ghidra: FUN_00545220 @ 0x00545220 â€” an eight-word tinted sprite (tag 0x07000000), the
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

    // Ghidra: &DAT_005e9060 â€” the per-panel selection index the scroll handling writes back.
    private static readonly byte* s_PanelSelection_005e9060 = (byte*)NativeMemory.AllocZeroed(0x100);

    /// <summary>
    /// Ghidra: FUN_00545340 @ 0x00545340 â€” draws one UI panel and returns the advanced primitive
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
        // far past both arrays â€” and the value read then becomes a *write* offset below. This was
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
                // ever cleared that table â€” a defect on our side, and one that would otherwise show
                // up as an unexplained native fault.
                if (node == null || ++guard > 0x400)
                {
                    throw new InvalidOperationException(
                        $"FUN_00545340: the panel's ordering table at descriptor+0xa4 is not a " +
                        $"terminated list ({(node == null ? "null link" : "over 1024 nodes")}). It is " +
                        "cleared by control code 0x0c in FUN_005443a0 â€” the text engine must have " +
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

    // =====================================================================
    // The options page's drawing layer (Ghidra: the FUN_00510xxx family)
    //
    // A second, self-contained UI layer, unrelated to the display-list renderer the main menu
    // uses. It talks to the 3D device **directly**: every widget ends up in FUN_00510500 or
    // FUN_00510330, which build four `D3DTLVERTEX` on the stack and submit them as one
    // `DrawPrimitive(D3DPT_TRIANGLEFAN, D3DVT_TLVERTEX, â€¦, 4, 0)`. The device is the one
    // `Ordinal_750` returns â€” `DAT_011be760`, the same object DeviceShim faces â€” so this whole
    // layer renders through the Direct3D 11 backend without any further plumbing.
    //
    // **On the vertex order, which Ghidra does not give directly.** In both builders the address
    // passed to `DrawPrimitive` disagrees with the one passed to the scaling helper by 16 bytes,
    // so the slot naming cannot be trusted on its own. Grouping the assignments by the 32-byte
    // `TLVERTEX` stride resolves it without guesswork, and the two agree: the array starts at the
    // address the scaling helper receives, and its four vertices are
    // `(x1,y1) (x2,y1) (x2,y2) (x1,y2)` â€” the standard quad fan, with `z` and `rhw` both 0.9f.
    // =====================================================================

    /// <summary>Ghidra: DAT_00578e28 â€” the device, fetched once per use through Ordinal_750.</summary>
    private static void** UiDeviceVtable => *(void***)Ordinal_750();

    private const int Device_BindTexture = 0x64 / 4;
    private const int Device_AlphaBlendEnable = 0x98 / 4;
    private const int Device_StippleState = 0x9c / 4;
    private const int Device_SrcBlend = 0xa0 / 4;
    private const int Device_DestBlend = 0xa4 / 4;
    private const int Device_DrawPrimitive = 0xbc / 4;
    private const int Device_GetDeviceDesc = 0x4c / 4;

    private static void UiSetState(int slot, uint value)
    {
        var method = (delegate* unmanaged[Thiscall]<void*, uint, void>)UiDeviceVtable[slot];
        method(Ordinal_750(), value);
    }

    private static void UiDrawFan(TlVertex* vertices)
    {
        var draw = (delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)
            UiDeviceVtable[Device_DrawPrimitive];
        draw(Ordinal_750(), 6, 3, vertices, 4, 0);   // D3DPT_TRIANGLEFAN, D3DVT_TLVERTEX
    }

    /// <summary>
    /// Ghidra: FUN_00510110 @ 0x00510110 â€” maps the layer's own 320x240-ish coordinates onto the
    /// current resolution, in place, for <paramref name="count"/> vertices.
    /// <para>
    /// The two offset tables were read out of <c>Legacy/Blaze.exe</c>: X at 0x00566c98 is
    /// <c>{-160, -64, 0, 80, 192}</c> and Y at 0x00566cb0 is <c>{-120, -48, 0, 60, 144}</c>,
    /// indexed by the ini's Resolution. Entry 4 is `(192, 144)` â€” exactly the "base" the
    /// `[SHIFT]` runtime trace reports, which is what confirms the tables are the right ones.
    /// </para>
    /// </summary>
    private static void UiScaleVertices(TlVertex* vertices, int count)
    {
        int scale, offsetX, offsetY;
        if (s_SmallTextureFlag == 0 && s_SizeSetting_005de160 > 7)
        {
            scale = 2; offsetX = 0; offsetY = 0;
        }
        else
        {
            scale = 1; offsetX = 0xa0; offsetY = 0x78;
        }

        uint resolution = s_ResolutionIndex < (uint)s_UiOffsetX_00566c98.Length ? s_ResolutionIndex : 0;

        for (int i = 0; i < count; i++)
        {
            vertices[i].X = vertices[i].X * scale + s_UiOffsetX_00566c98[resolution] + offsetX;
            vertices[i].Y = vertices[i].Y * scale + s_UiOffsetY_00566cb0[resolution] + offsetY;
        }
    }

    // Ghidra: the tables at 0x00566c98 and 0x00566cb0, read verbatim from Legacy/Blaze.exe.
    private static readonly int[] s_UiOffsetX_00566c98 = [-160, -64, 0, 80, 192];
    private static readonly int[] s_UiOffsetY_00566cb0 = [-120, -48, 0, 60, 144];

    // Ghidra: _DAT_0054d020 = -0.1f and _DAT_0054d024 = 1/256f â€” the half-texel bias and the
    // reciprocal of the atlas width, read from the binary's .rdata.
    private const float UiTexelBias = -0.1f;
    private const float UiTexelScale = 1.0f / 256.0f;

    /// <summary>Ghidra: the 0.9f both `z` and `rhw` carry on every vertex this layer builds.</summary>
    private const float UiDepth = 0.9f;

    /// <summary>
    /// Ghidra: FUN_00510330 @ 0x00510330 â€” a flat filled rectangle, drawn untextured (it binds
    /// texture 0) and alpha-blended.
    /// </summary>
    private static void UiFillRect(int x, int y, int width, int height, uint colour)
    {
        void* deviceDesc = ((delegate* unmanaged[Thiscall]<void*, void*>)
            UiDeviceVtable[Device_GetDeviceDesc])(Ordinal_750());

        TlVertex* quad = stackalloc TlVertex[4];
        UiQuad(quad, x, y, width, height, colour);
        UiScaleVertices(quad, 4);

        UiSetState(Device_BindTexture, 0);

        // Ghidra: the capability word at deviceDesc + 0x80 chooses between two blend setups â€”
        // bit 0x1000 leaves the state alone, bit 0x2000 raises the 0x9c pair instead.
        uint caps = deviceDesc != null ? *(uint*)((byte*)deviceDesc + 0x80) : 0;
        if ((caps & 0x1000) == 0)
        {
            if ((caps & 0x2000) != 0) UiSetState(Device_StippleState, 1);
        }
        else
        {
            UiSetState(Device_AlphaBlendEnable, 1);
        }

        UiSetState(Device_SrcBlend, 5);     // D3DBLEND_SRCALPHA
        UiSetState(Device_DestBlend, 6);    // D3DBLEND_INVSRCALPHA
        UiDrawFan(quad);
        UiSetState(Device_StippleState, 0);
    }

    /// <summary>
    /// Ghidra: FUN_00510500 @ 0x00510500 â€” one textured quad out of an atlas: the destination
    /// rectangle, the source position and size in texels, the texture object and a modulating
    /// colour. Opaque: it forces alpha off and ONE/ZERO blending.
    /// </summary>
    private static void UiBlit(int x, int y, int width, int height,
                               int sourceX, int sourceY, void* texture,
                               int sourceWidth, int sourceHeight, uint colour)
    {
        TlVertex* quad = stackalloc TlVertex[4];
        UiQuad(quad, x, y, width, height, colour | 0xff000000);

        float u1 = (sourceX - UiTexelBias) * UiTexelScale;
        float v1 = (sourceY - UiTexelBias) * UiTexelScale;
        float u2 = (sourceWidth - UiTexelBias + sourceX) * UiTexelScale;
        float v2 = (sourceHeight - UiTexelBias + sourceY) * UiTexelScale;

        quad[0].U = u1; quad[0].V = v1;
        quad[1].U = u2; quad[1].V = v1;
        quad[2].U = u2; quad[2].V = v2;
        quad[3].U = u1; quad[3].V = v2;

        UiScaleVertices(quad, 4);

        var bind = (delegate* unmanaged[Thiscall]<void*, void*, void>)UiDeviceVtable[Device_BindTexture];
        bind(Ordinal_750(), texture);

        UiSetState(Device_AlphaBlendEnable, 0);
        UiSetState(Device_SrcBlend, 2);     // D3DBLEND_ONE
        UiSetState(Device_DestBlend, 1);    // D3DBLEND_ZERO
        UiDrawFan(quad);
    }

    /// <summary>Fills the four vertices both builders share, less their texture coordinates.</summary>
    private static void UiQuad(TlVertex* quad, int x, int y, int width, int height, uint colour)
    {
        float x1 = x, y1 = y, x2 = x + width, y2 = y + height;
        quad[0] = new TlVertex { X = x1, Y = y1, Z = UiDepth, Rhw = UiDepth, Diffuse = colour };
        quad[1] = new TlVertex { X = x2, Y = y1, Z = UiDepth, Rhw = UiDepth, Diffuse = colour };
        quad[2] = new TlVertex { X = x2, Y = y2, Z = UiDepth, Rhw = UiDepth, Diffuse = colour };
        quad[3] = new TlVertex { X = x1, Y = y2, Z = UiDepth, Rhw = UiDepth, Diffuse = colour };
    }

    /// <summary>
    /// Ghidra: FUN_005108f0 @ 0x005108f0 â€” draws one pre-rendered label strip. The label index
    /// selects both its width (from the runtime-built table at DAT_00589758) and which of four
    /// atlases holds it: 0..0x11 in the first, 0x12..0x23 in the second, 0x24..0x35 in the third,
    /// and negative indices in the fourth, counted backwards. Rows are 14 texels tall.
    /// <paramref name="centred"/> is the original's `param_4 == 1`, which shifts left by half the
    /// label's width.
    /// </summary>
    private static void UiDrawLabel(int x, int y, int label, int centred, uint colour)
    {
        // La table est indexÃ©e des deux cÃ´tÃ©s : les libellÃ©s d'ACTION.TXT ont des identifiants
        // nÃ©gatifs et leurs largeurs sont Ã©crites en descendant sous la base. Un garde qui ramenait
        // ces index Ã  0 donnait Ã  toutes les actions la largeur du libellÃ© 0, ce qui les tronquait
        // toutes Ã  la mÃªme longueur.
        int width = s_UiLabelWidths_00589758[label];
        if (centred == 1) x -= width >> 1;

        if (label < 0)
        {
            UiBlit(x, y, width, 0xe, 0, (-1 - label) * 0xe, s_UiAtlasNegative_00589708,
                   width, 0xe, colour);
            return;
        }

        void* atlas = s_UiAtlas0_00589c6c;
        if (label > 0x11)
        {
            if (label < 0x24) { label -= 0x12; atlas = s_UiAtlas1_00589830; }
            else if (label > 0x35) return;
            else { label -= 0x24; atlas = s_UiAtlas2_00589834; }
        }

        UiBlit(x, y, width, 0xe, 0, label * 0xe, atlas, width, 0xe, colour);
    }

    /// <summary>
    /// Ghidra: FUN_00510740 @ 0x00510740 â€” the page's window frame: a translucent fill, then the
    /// eight pieces of a nine-patch border taken from the 16x16 cells of the frame atlas.
    /// </summary>
    private static void UiDrawFrame(int x, int y, int width, int height)
    {
        void* atlas = s_UiFrameAtlas_00599ddc;
        UiFillRect(x + 8, y + 8, width - 0x10, height - 0x10, 0x7f080437);

        int right = x + width - 0x10;
        int bottom = y + height - 0x10;

        UiBlit(x, y, 0x10, 0x10, 0x40, 0, atlas, 0x10, 0x10, 0xffffff);
        UiBlit(x + 0x10, y, width - 0x1f, 0x10, 0, 0, atlas, 0xf, 0x10, 0xffffff);
        UiBlit(right, y, 0x10, 0x10, 0x50, 0, atlas, 0x10, 0x10, 0xffffff);
        UiBlit(x, y + 0x10, 0x10, height - 0x1f, 0x20, 0, atlas, 0x10, 0xf, 0xffffff);
        UiBlit(right, y + 0x10, 0x10, height - 0x1f, 0x30, 0, atlas, 0x10, 0xf, 0xffffff);
        UiBlit(x, bottom, 0x10, 0x10, 0x60, 0, atlas, 0x10, 0x10, 0xffffff);
        UiBlit(x + 0x10, bottom, width - 0x1f, 0x10, 0x10, 0, atlas, 0xf, 0x10, 0xffffff);
        UiBlit(right, bottom, 0x10, 0x10, 0x70, 0, atlas, 0x10, 0x10, 0xffffff);
    }

    /// <summary>Ghidra: FUN_00510a70 @ 0x00510a70 â€” the selection cursor, one 15x16 cell.</summary>
    private static void UiDrawCursor(int x, int y)
        => UiBlit(x, y, 0xf, 0x10, 0xf0, 0, s_UiFrameAtlas_00599ddc, 0xf, 0x10, 0xffffff);

    // winsys3d, exports nommÃ©s. `ShiftVertices` (ordinal 824, winsys3d.c:4913) applique
    // `x = dÃ©calage + (Size + 9) * 32 / 320 * x` â€” le facteur documentÃ© dans la section
    // Â« Coordinate scaling Â». Le fond de page passe par lui ; les widgets, eux, par la mise Ã 
    // l'Ã©chelle que le jeu fait lui-mÃªme (FUN_00510110). Les deux coexistent, c'est voulu.
    [DllImport("winsys3d.dll", ExactSpelling = true)]
    private static extern void ShiftVertices(TlVertex* vertices, int count);

    [DllImport("winsys3d.dll", ExactSpelling = true)]
    private static extern void SetTextureFilterMode(int mode);

    [DllImport("winsys3d.dll", ExactSpelling = true)]
    private static extern int SetTimingMode(int mode);

    // winsys3d ordinal 749 (winsys3d.c:6010) : demande au device le mode d'affichage d'index
    // `resolution` par son slot `+0xE8`, et ne retient le changement que si le device l'accepte.
    [DllImport("winsys3d.dll", ExactSpelling = true)]
    private static extern int ChangeResolution(int resolution);

    /// <summary>
    /// Ghidra: FUN_0050fe90 @ 0x0050fe90 â€” dÃ©but d'image : chacune des huit textures de la page est
    /// interrogÃ©e par son slot <c>[8]</c> (<c>IDirectDrawSurface::IsLost</c> chez 3dhelp) et
    /// recrÃ©Ã©e depuis sa source si elle a Ã©tÃ© perdue.
    /// <para>
    /// Sur notre chemin le slot rend toujours Â« pas perdue Â» â€” une texture Direct3D 11 ne se perd
    /// pas comme une surface DirectDraw â€” donc cette passe ne fait rien. Elle est transcrite
    /// entiÃ¨re malgrÃ© tout : c'est elle qui prouve que le slot 8 existe, et le jour oÃ¹ une perte de
    /// device sera gÃ©rÃ©e, la reconstruction est dÃ©jÃ  en place.
    /// </para>
    /// </summary>
    private static void RestoreLostUiTextures()
    {
        byte* background = s_UiBackgroundFile_00579264;
        byte* frame = s_UiFrameFile_00589838;

        RestoreIfLost(ref s_UiBackgroundTexture_0057926c, s_UiBackgroundInfo_00578e30,
                      background != null ? background + *(int*)(background + 10) : null, null);
        RestoreIfLost(ref s_UiFrameAtlas_00599ddc, s_UiFrameInfo_00589840,
                      frame != null ? frame + *(int*)(frame + 10) : null, s_UiFrameColourKey_005d9df8);

        RestoreIfLost(ref s_UiAtlas0_00589c6c, s_UiTargetInfo_00579278, s_UiAtlas0Bits_00589c80, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlas1_00589830, s_UiTargetInfo_00579278, s_UiAtlas1Bits_005b9de0, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlas2_00589834, s_UiTargetInfo_00579278, s_UiAtlas2Bits_005c9de0, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlasNegative_00589708, s_UiTargetInfo_00579278, s_UiAtlasNegativeBits_00579708, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlasKeyboard_00589c78, s_UiTargetInfo_00579278, s_UiKeyboardBits_005a9de0, s_UiRestoreColourKey_005d9df8);
        RestoreIfLost(ref s_UiAtlasJoystick_00599dd8, s_UiTargetInfo_00579278, s_UiAtlasJoystickBits_00599de0, s_UiRestoreColourKey_005d9df8);
    }

    private static void RestoreIfLost(ref void* texture, byte* bitmapInfo, void* bits, uint* colourKey)
    {
        if (texture == null || bits == null) return;

        void** vtable = *(void***)texture;
        int lost = ((delegate* unmanaged[Thiscall]<void*, int>)vtable[8])(texture);
        if (lost == 0) return;

        ((delegate* unmanaged[Thiscall]<void*, uint, int>)vtable[0])(texture, 1);
        texture = UiCreateTexture(bitmapInfo, bits, colourKey);
    }

    /// <summary>
    /// Ghidra: FUN_005101c0 @ 0x005101c0 â€” le fond de page : un seul quad `(0,0)-(320,241)` en
    /// coordonnÃ©es du jeu, texturÃ© par BACKGRND.PIC, opaque, filtrage activÃ© le temps du dessin.
    /// Les coordonnÃ©es de texture sont les constantes du binaire, `0.5/256` et `255/256` en X,
    /// `224.5/256` en Y.
    /// </summary>
    private static void DrawUiBackground()
    {
        TlVertex* quad = stackalloc TlVertex[4];
        const float u1 = 0.001953125f, u2 = 0.99609375f;   // 0x3B000000, 0x3F7F8000
        const float v2 = 0.876953125f;                     // 0x3F608000

        quad[0] = UiBackgroundVertex(0f, 0f, u1, u1);
        quad[1] = UiBackgroundVertex(320f, 0f, u2, u1);
        quad[2] = UiBackgroundVertex(320f, 241f, u2, v2);
        quad[3] = UiBackgroundVertex(0f, 241f, u1, v2);

        // Ghidra perd l'argument du premier appel ; le second est SetTextureFilterMode(0), et le
        // filtrage n'aurait pas de sens laissÃ© actif aprÃ¨s un fond plein Ã©cran.
        SetTextureFilterMode(1);
        ShiftVertices(quad, 4);

        var bind = (delegate* unmanaged[Thiscall]<void*, void*, void>)UiDeviceVtable[Device_BindTexture];
        bind(Ordinal_750(), s_UiBackgroundTexture_0057926c);

        UiSetState(Device_AlphaBlendEnable, 0);
        UiSetState(Device_SrcBlend, 2);     // D3DBLEND_ONE
        UiSetState(Device_DestBlend, 1);    // D3DBLEND_ZERO
        UiDrawFan(quad);
        SetTextureFilterMode(0);
    }

    private static TlVertex UiBackgroundVertex(float x, float y, float u, float v)
        => new() { X = x, Y = y, Z = UiDepth, Rhw = UiDepth, Diffuse = 0xffffffff, Specular = 0, U = u, V = v };

    /// <summary>
    /// Ghidra: FUN_00510aa0 @ 0x00510aa0 â€” la sÃ©lection. Fait avancer <paramref name="selection"/>
    /// entre <paramref name="min"/> et <paramref name="max"/>, et rend un masque de ce qui a Ã©tÃ©
    /// demandÃ© : 1 valider, 2 gauche, 4 droite, 8 page prÃ©cÃ©dente, 0x10 page suivante, et
    /// <c>0xffffffff</c> pour annuler.
    /// <para>
    /// Deux sources d'entrÃ©e, lues sÃ©parÃ©ment. Le clavier est pris **directement dans le tampon
    /// DirectInput** (`DAT_00604b50`), par scancode : 0xC8 haut, 0xD0 bas, 0xCB gauche, 0xCD
    /// droite, 0x1C entrÃ©e, 0x01 Ã©chap, 0xC9 et 0xD1 page prÃ©cÃ©dente/suivante. La manette passe par
    /// le bloc d'Ã©tat que <c>PollInputAndAudio</c> dÃ©signe (`DAT_00579270`). La rÃ©pÃ©tition tient en
    /// deux compteurs : 12 images avant la premiÃ¨re rÃ©pÃ©tition, 2 ensuite.
    /// </para>
    /// </summary>
    private static uint UiSelect(int* selection, int min, int max)
    {
        uint result = 0;
        uint held = 0;

        PollJoystickState();            // Ghidra: FUN_005048f0
        UpdateButtonEdges();            // Ghidra: FUN_00504a30
        AssignControllersToSlots();     // Ghidra: FUN_005357a0

        byte* keys = s_KeyboardState_00604b50;
        if (keys == null) return 0;

        // Ghidra: haut et bas partagent le mÃªme compteur de rÃ©pÃ©tition, et s'excluent â€” le bas
        // n'est mÃªme pas consultÃ© tant que le haut est enfoncÃ©.
        if (keys[0xc8] != 0)
        {
            held = UiRepeat();
            if ((held & 2) != 0 && *selection > min) { (*selection)--; PlayUiSound3A(); }
        }
        else if (keys[0xd0] != 0)
        {
            held = UiRepeat();
            if ((held & 2) != 0 && *selection < max) { (*selection)++; PlayUiSound3A(); }
        }

        if (keys[0xcb] != 0)
        {
            held = UiRepeat();
            if ((held & 2) != 0) { result = 2; PlayUiSound3A(); }
        }

        if (keys[0xcd] != 0)
        {
            held = UiRepeat();
            if ((held & 2) != 0) { result |= 4; PlayUiSound3A(); }
        }

        if (held == 0) { s_UiRepeatCounter_005d9e08 = 0; s_UiRepeatActive_005d9e00 = 0; }

        // Les quatre touches Ã  front : entrÃ©e, Ã©chap, page prÃ©cÃ©dente, page suivante. L'Ã©tat
        // prÃ©cÃ©dent tient dans les quatre bits de DAT_005d9e04.
        uint edges = s_UiKeyEdges_005d9e04;
        edges = UiEdge(keys[0x1c] != 0, edges, 1, ref result, 1);
        if (keys[0x01] != 0 && (edges & 2) == 0) result = 0xffffffff;
        edges = keys[0x01] != 0 ? edges | 2 : edges & ~2u;
        edges = UiEdge(keys[0xc9] != 0, edges, 4, ref result, 8);
        edges = UiEdge(keys[0xd1] != 0, edges, 8, ref result, 0x10);
        s_UiKeyEdges_005d9e04 = edges;

        if (s_TraceMenu && (result != 0 || held != 0 || keys[0x01] != 0))
            Console.Error.WriteLine(
                $"[UI] sÃ©lection -> 0x{result:X8} (maintien={held} fronts=0x{edges:X} " +
                $"entrÃ©e={keys[0x1c]:X2} Ã©chap={keys[0x01]:X2})");

        if (held != 0 || edges != 0) return result;

        // La manette, seulement si le clavier ne dit rien.
        byte* pad = s_Unknown_00579270;
        if (pad == null) return result;

        ushort directions = *(ushort*)(pad + 6);
        if ((directions & 0x10) != 0 && *selection > min) { (*selection)--; PlayUiSound3A(); }
        else if ((directions & 0x40) != 0 && *selection < max) { (*selection)++; PlayUiSound3A(); }

        if ((directions & 0x80) != 0) result |= 2;
        if ((directions & 0x20) != 0) result |= 4;

        ushort buttons = *(ushort*)(pad + 2);
        if ((buttons & 0x8000) != 0) result |= 1;
        if ((buttons & 0x0800) != 0) result |= 8;
        if ((buttons & 0x0400) != 0) result |= 0x10;
        if ((buttons & 0x4000) != 0) return 0xffffffff;
        return result;
    }

    /// <summary>Ghidra: le motif de rÃ©pÃ©tition rÃ©pÃ©tÃ© cinq fois dans FUN_00510aa0.</summary>
    private static uint UiRepeat()
    {
        if (s_UiRepeatActive_005d9e00 == 0)
        {
            s_UiRepeatActive_005d9e00 = 1;
            s_UiRepeatCounter_005d9e08 = 0xc;
            return 3;
        }

        s_UiRepeatCounter_005d9e08--;
        if (s_UiRepeatCounter_005d9e08 != 0) return 1;

        s_UiRepeatCounter_005d9e08 = 2;
        return 3;
    }

    /// <summary>Ghidra: le motif de dÃ©tection de front des quatre touches d'action.</summary>
    private static uint UiEdge(bool down, uint edges, uint bit, ref uint result, uint resultBit)
    {
        if (!down) return edges & ~bit;
        if ((edges & bit) == 0) result |= resultBit;
        return edges | bit;
    }

    private static int s_UiRepeatActive_005d9e00;    // Ghidra: DAT_005d9e00
    private static uint s_UiKeyEdges_005d9e04;       // Ghidra: DAT_005d9e04
    private static int s_UiRepeatCounter_005d9e08;   // Ghidra: DAT_005d9e08
    private static int s_UiOptionsSelection_005d9e48;// Ghidra: DAT_005d9e48

    // Ghidra: DAT_005d9df8 â€” la couleur-clÃ© que la restauration rÃ©applique. Comme ses voisines,
    // elle n'est Ã©crite nulle part dans le graphe reconstruit et vaut donc zÃ©ro.
    private static readonly uint* s_UiRestoreColourKey_005d9df8 = (uint*)NativeMemory.AllocZeroed(2, sizeof(uint));
    private static uint* s_UiFrameColourKey_005d9df8 => s_UiRestoreColourKey_005d9df8;

    // =====================================================================
    // The options page's setup (Ghidra: FUN_0050fab0 and its chain)
    //
    // Everything this page shows is built once, on first entry, out of files on the CD:
    //
    //     DATA/FONT.PIC       the 8-bit font sheet, 31 glyphs per row, cells 10 x 14
    //     DATA/CONFIG.TXT     the option labels          -> three 256x256 atlases
    //     DATA/ACTION.TXT     the action names           -> one atlas, negative label ids
    //     DATA/JOYSTICK.TXT   the joystick button names  -> one atlas
    //     DATA/KEYBOARD.TXT   the key names              -> one atlas, packed into rows
    //     DATA/BACKGRND.PIC   the page background        -> one texture
    //     DATA/FRAMEMOD.PIC   the window frame and cursor sheet
    //
    // The labels are **rasterised in software** â€” glyph by glyph, through wsyskern's DIBCopyBlock,
    // into 256x256 8-bit buffers â€” and only then handed to the device as textures via the texture
    // class's `[2]` slot. That is the path this file wrongly recorded as never taken; see
    // Documentation/DLL/3dhelp.md, correction of 2026-08-14.
    // =====================================================================

    // wsyskern exports, all by name, on the same footing as LoadBitmapFile.
    [DllImport("wsyskern.dll", ExactSpelling = true)]
    private static extern int DIBCopyBlock(void* destinationBits, void* destinationInfo,
                                           int destinationX, int destinationY, uint width, uint height,
                                           void* sourceBits, void* sourceInfo,
                                           int sourceX, int sourceY);

    /// <summary>wsyskern ordinal 91. Ghidra types it `void`; it returns the adjusted byte in AL.</summary>
    [DllImport("wsyskern.dll", ExactSpelling = true)]
    private static extern byte Gamma(int value, double gamma);

    [DllImport("wsyskern.dll", ExactSpelling = true)] private static extern void* CreateArray(int kind, void* capacity);
    [DllImport("wsyskern.dll", ExactSpelling = true)] private static extern void* LoadArray(void* array, byte* path, int startIndex, int maxLines, int skip, int flags);
    [DllImport("wsyskern.dll", ExactSpelling = true)] private static extern void ArrayToString(void* array, int index, byte* buffer, int size);
    [DllImport("wsyskern.dll", ExactSpelling = true)] private static extern void DeleteArray(void* array);

    // =====================================================================
    // DEVIATION, mesurÃ©e et bornÃ©e : la lecture des fichiers texte ne passe plus par l'API de
    // tableaux de `wsyskern`.
    //
    // Pourquoi. `LoadArray` **corrompt le tas** dans ce processus, de faÃ§on cumulative avec le
    // nombre de lignes lues : 1 ligne passe, 4 lignes font tomber le processus plus loin, le
    // fichier entier le fait tomber tout de suite â€” et toujours Ã  la premiÃ¨re allocation qui suit,
    // signature d'une corruption causÃ©e ailleurs. Ce qui a Ã©tÃ© Ã©cartÃ© par la mesure, et n'est donc
    // pas Ã  rouvrir :
    //
    //   - l'appel est conforme : `RET 0x18` dans la DLL, donc 6 arguments stdcall, et le sens de
    //     chacun est lu dans `FUN_10004060` (index de dÃ©part, plafond de lignes, lignes Ã  sauter,
    //     drapeaux) ; `ArrayToString` de mÃªme, `RET 0x10` ;
    //   - l'objet est valide : `CreateArray` pose le tag Â« ARAY Â» que le validateur exige, et
    //     `LoadArray` sortirait proprement s'il ne l'Ã©tait pas â€” ce qu'il fait avec un fichier
    //     absent, cas oÃ¹ **toute la mise en place s'exÃ©cute jusqu'au bout** ;
    //   - ce n'est ni la capacitÃ© initiale (0 comme 0x80 Ã©chouent), ni la croissance (le drapeau
    //     Â« ne pas agrandir Â» n'y change rien), ni le chemin (correct, fichier prÃ©sent).
    //
    // Ce que le tableau apporte ici se rÃ©duit Ã  Â« rendre la ligne i d'un fichier texte Â». On le
    // fait donc nous-mÃªmes. C'est la mÃªme nature de dÃ©viation que le remplacement de DirectDraw
    // par Direct3D 11, en beaucoup plus petit, et elle ne touche aucune logique du jeu.
    //
    // `OPENFORESIA_WSYSKERN_ARRAYS=1` rÃ©tablit le chemin d'origine, pour qui voudra reprendre le
    // diagnostic cÃ´tÃ© DLL.
    // =====================================================================

    private sealed class UiTextFile
    {
        internal readonly List<byte[]> Lines = [];
    }

    private static readonly bool s_UseWsyskernArrays =
        Environment.GetEnvironmentVariable("OPENFORESIA_WSYSKERN_ARRAYS") == "1";

    /// <summary>Ghidra: LoadArray â€” lit tout le fichier et le dÃ©coupe en lignes.</summary>
    private static void ReadUiTextFile(UiTextFile file, string name)
    {
        file.Lines.Clear();

        byte* fileName = ToAnsiZ(name);
        byte* pathAnsi = BuildCdPath(fileName);
        // Le tampon fait 64 octets et n'est zÃ©ro que jusqu'au terminateur : couper au premier zÃ©ro,
        // pas Ã  la fin, sinon la queue non initialisÃ©e se retrouve dans le chemin.
        int length = 0;
        while (length < 64 && pathAnsi[length] != 0) length++;
        string path = System.Text.Encoding.Latin1.GetString(pathAnsi, length);
        Free(pathAnsi);
        Free(fileName);

        byte[] bytes;
        try { bytes = System.IO.File.ReadAllBytes(path); }
        catch (System.IO.IOException) { return; }
        catch (UnauthorizedAccessException) { return; }

        int start = 0;
        for (int i = 0; i <= bytes.Length; i++)
        {
            if (i != bytes.Length && bytes[i] != (byte)'\n') continue;

            int end = i;
            if (end > start && bytes[end - 1] == (byte)'\r') end--;
            if (end > start || i != bytes.Length) file.Lines.Add(bytes[start..end]);
            start = i + 1;
        }
    }

    /// <summary>
    /// Ghidra: ArrayToString â€” copie la ligne demandÃ©e dans un tampon ANSI terminÃ© par zÃ©ro. Un
    /// index hors du fichier rend une chaÃ®ne vide, ce que la boucle appelante traite dÃ©jÃ  comme
    /// Â« rien Ã  dessiner Â».
    /// </summary>
    private static void UiLineToBuffer(UiTextFile file, int index, byte* buffer, int size)
    {
        buffer[0] = 0;
        if ((uint)index >= (uint)file.Lines.Count) return;

        byte[] line = file.Lines[index];
        int n = Math.Min(line.Length, size - 1);
        for (int i = 0; i < n; i++) buffer[i] = line[i];
        buffer[n] = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0050f560 @ 0x0050f560 â€” character to glyph index. The five ranges skip the
    /// punctuation gaps of the cp1252 layout the font sheet is laid out in; six accented capitals
    /// and the currency sign all fold onto index 0xa5.
    /// </summary>
    private static int GlyphIndex(int character)
    {
        switch (character)
        {
            case 0x40: case 0x80: case 0x8a: case 0x9a: case 0xc0: case 0xe0:
                character = 0xa5;
                break;
        }

        if (character > 0xe0) return character - 0x46;
        if (character > 0xc0) return character - 0x45;
        if (character > 0xa0) return character - 0x44;
        if (character > 0x60) return character - 0x23;
        if (character > 0x40) return character - 0x22;
        return character - 0x21;
    }

    /// <summary>
    /// Ghidra: FUN_0050f770 @ 0x0050f770 â€” the width a string will occupy, in the same terms
    /// FUN_0050f680 advances by: a space is 4, anything else its glyph's width, control characters
    /// nothing.
    /// </summary>
    private static int MeasureLabel(byte* text)
    {
        int width = 0;
        for (int i = 0; text[i] != 0; i++)
        {
            byte c = text[i];
            // Ghidra rend le caractÃ¨re en `char` **signÃ©**, ce qui est un artefact : `FUN_0050f560`
            // compare son paramÃ¨tre Ã  0x80, 0x8a, 0x9a, 0xc0 et 0xe0, valeurs qu'un char signÃ© ne
            // peut jamais prendre. Le test des caractÃ¨res de contrÃ´le, lui, est bien signÃ© dans
            // l'original â€” les deux formulations y sont Ã©quivalentes.
            if ((sbyte)c is > 0 and < 0x20) continue;
            if (c == ' ') { width += 4; continue; }

            int glyph = GlyphIndex(c);
            if ((uint)glyph >= (uint)s_GlyphWidths_0056e7b0.Length) continue;
            width += s_GlyphWidths_0056e7b0[glyph];
        }
        return width;
    }

    /// <summary>
    /// Ghidra: FUN_0050f680 @ 0x0050f680 â€” draws one string into a DIB at row
    /// <paramref name="row"/> (rows are 14 texels tall), starting at <paramref name="x"/>, and
    /// returns the pen position it ended at. Each glyph is one DIBCopyBlock out of the font sheet,
    /// whose cells are 10 wide and 14 tall, 31 to a row.
    /// </summary>
    private static int RasteriseLabel(void* destinationBits, void* destinationInfo,
                                      int row, byte* text, int x)
    {
        byte* font = s_UiFontFile_00589c74;
        if (font == null) return x;

        void* fontBits = font + *(int*)(font + 10);   // BITMAPFILEHEADER::bfOffBits
        void* fontInfo = font + 0xe;                  // the BITMAPINFO that follows it

        for (int i = 0; text[i] != 0; i++)
        {
            byte c = text[i];
            if ((sbyte)c is > 0 and < 0x20) continue;

            if (c == ' ') { x += 4; continue; }

            int glyph = GlyphIndex(c);
            if ((uint)glyph >= (uint)s_GlyphWidths_0056e7b0.Length)
            {
                UiStage($"    glyphe hors table : car=0x{(byte)c:X2} index={glyph} " +
                        $"(table={s_GlyphWidths_0056e7b0.Length})");
                continue;
            }

            int width = s_GlyphWidths_0056e7b0[glyph];

            // GARDE-FOU, et instrument. La cible fait 256x256 en 8 bits, soit exactement les
            // 64 Kio du tampon. `DIBCopyBlock` ne clippe pas de maniÃ¨re sÃ»re : un blit qui sort
            // Ã©crit hors du tampon et corrompt le tas â€” le symptÃ´me observÃ©. On refuse le blit et
            // on le signale, ce qui dit du mÃªme coup s'il arrive vraiment.
            if (x < 0 || row < 0 || x + width > 0x100 || row * 0xe + 0xe > 0x100)
            {
                UiStage($"    blit REFUSE hors cible : ({x},{row * 0xe}) l={width} " +
                        $"car=0x{(byte)c:X2} glyphe={glyph}");
                x += width;
                continue;
            }

            if (s_TraceGlyphs)
                UiStage($"    blit car=0x{(byte)c:X2} glyphe={glyph} l={width} -> ({x},{row * 0xe}) " +
                        $"source=({(glyph % 0x1f) * 10},{(glyph / 0x1f) * 0xe})");
            DIBCopyBlock(destinationBits, destinationInfo, x, row * 0xe, (uint)width, 0xe,
                         fontBits, fontInfo, (glyph % 0x1f) * 10, (glyph / 0x1f) * 0xe);
            x += width;
        }

        return x;
    }

    /// <summary>
    /// Ghidra: FUN_0050f9b0 @ 0x0050f9b0 â€” the key names, which unlike the other label sets do not
    /// get one row each: they are packed left to right and wrapped at 253 texels, each entry's
    /// (x, row, width) recorded in the placement table at DAT_00599c80.
    /// </summary>
    private static void RasteriseKeyLabels(UiTextFile array)
    {
        NativeMemory.Fill(s_UiKeyboardBits_005a9de0, UiAtlasBytes, 0);

        byte* placement = s_UiKeyPlacement_00599c80;
        byte* text = stackalloc byte[0x20];
        int x = 0, row = 0;

        for (int i = 0; i < UiKeyLabelCount; i++, placement += 4)
        {
            UiLineToBuffer(array, i, text, 0x20);
            int width = MeasureLabel(text);

            if (width + x > 0xfd) { x = 0; row++; }

            placement[0] = (byte)x;
            placement[1] = (byte)row;
            *(short*)(placement + 2) = (short)width;

            RasteriseLabel(s_UiKeyboardBits_005a9de0, s_UiTargetInfo_00579278, row, text, x);
            x += width;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050f7d0 @ 0x0050f7d0 â€” the two .PIC-backed textures. Both bitmaps' headers are
    /// copied out of the loaded file, their 256-entry palettes passed through wsyskern's
    /// <c>Gamma</c> at the ini's gamma setting, and the result handed to a texture object.
    /// </summary>
    private static void BuildPictureTextures()
    {
        byte* background = s_UiBackgroundFile_00579264;
        byte* frame = s_UiFrameFile_00589838;
        if (background == null || frame == null) return;

        // Ghidra: 10 dwords â€” the BITMAPINFOHEADER alone; the palette that follows is rebuilt.
        NativeMemory.Copy(background + 0xe, s_UiBackgroundInfo_00578e30, 40);
        NativeMemory.Copy(frame + 0xe, s_UiFrameInfo_00589840, 40);

        double gamma = (float)s_Unknown_005d9de0 * s_GammaScale_0054d000;

        // Ghidra: the palettes start at file + 0x36 (0x0e + 0x28) and are walked as B, G, R with
        // the fourth byte forced to zero. The three reads are at +0x36/+0x37/+0x38 of the file,
        // which is that same triple.
        for (int i = 0; i < 0x400; i += 4)
        {
            s_UiBackgroundInfo_00578e30[0x28 + i + 2] = Gamma(background[0x38 + i], gamma);
            s_UiBackgroundInfo_00578e30[0x28 + i + 1] = Gamma(background[0x37 + i], gamma);
            s_UiBackgroundInfo_00578e30[0x28 + i + 0] = Gamma(background[0x36 + i], gamma);
            s_UiBackgroundInfo_00578e30[0x28 + i + 3] = 0;

            s_UiFrameInfo_00589840[0x28 + i + 2] = Gamma(frame[0x38 + i], gamma);
            s_UiFrameInfo_00589840[0x28 + i + 1] = Gamma(frame[0x37 + i], gamma);
            s_UiFrameInfo_00589840[0x28 + i + 0] = Gamma(frame[0x36 + i], gamma);
            s_UiFrameInfo_00589840[0x28 + i + 3] = 0;
        }

        s_UiBackgroundTexture_0057926c = UiCreateTexture(
            s_UiBackgroundInfo_00578e30, background + *(int*)(background + 10), null);
        s_UiFrameAtlas_00599ddc = UiCreateTexture(
            s_UiFrameInfo_00589840, frame + *(int*)(frame + 10), s_UiFrameColourKey_005d9de8);
    }

    /// <summary>
    /// Ghidra: the three-call idiom this page repeats for every texture â€” create through the
    /// device's `+0x10` factory, load through the texture class's `[2]` slot, and, when it has one,
    /// set a colour key through `[6]`.
    /// </summary>
    private static void* UiCreateTexture(byte* bitmapInfo, void* bits, uint* colourKey)
    {
        void* device = Ordinal_750();
        void* texture = ((delegate* unmanaged[Thiscall]<void*, void*>)UiDeviceVtable[0x10 / 4])(device);
        if (texture == null) return null;

        void** vtable = *(void***)texture;
        ((delegate* unmanaged[Thiscall]<void*, void*, void*, int>)vtable[2])(texture, bitmapInfo, bits);
        if (colourKey != null)
            ((delegate* unmanaged[Thiscall]<void*, void*, int>)vtable[6])(texture, colourKey);
        return texture;
    }

    /// <summary>
    /// Ghidra: FUN_0050fab0 @ 0x0050fab0 â€” the options page's one-time setup, guarded by
    /// DAT_005d9de4 so it runs on first entry only, followed by the texture creation that runs
    /// every time (the gamma may have changed).
    /// </summary>
    private static void OptionsSetup()
    {
        if (s_UiSetupDone_005d9de4 == 0)
        {
            s_UiSetupDone_005d9de4 = 1;

            s_UiBackgroundFile_00579264 = LoadUiFile(s_UiFileBackground, silent: true);
            s_UiFrameFile_00589838 = LoadUiFile(s_UiFileFrame, silent: true);
            UiStage($"BACKGRND.PIC=0x{(nint)s_UiBackgroundFile_00579264:X8} " +
                    $"FRAMEMOD.PIC=0x{(nint)s_UiFrameFile_00589838:X8}");

            UiStage("lecture CONFIG.TXT");
            var array = new UiTextFile();
            ReadUiTextFile(array, s_UiFileConfigText);
            UiStage($"  {array.Lines.Count} lignes");

            UiStage("chargement FONT.PIC");
            s_UiFontFile_00589c74 = LoadUiFile(s_UiFileFont, silent: true);

            // Ghidra: 0x10a dwords out of the font file at +0xe â€” a BITMAPINFOHEADER plus its
            // 256-entry palette, 1064 bytes â€” reused as the header of every rasterisation target,
            // with the dimensions forced to 256x256. Measured on the disc's FONT.PIC: 312x84, 8
            // bits, so the copy covers a real 256-colour palette and the target is exactly the
            // 64 KiB the buffers provide.
            if (s_UiFontFile_00589c74 != null)
                NativeMemory.Copy(s_UiFontFile_00589c74 + 0xe, s_UiTargetInfo_00579278, 0x10a * 4);

            *(int*)(s_UiTargetInfo_00579278 + 4) = 0x100;    // biWidth
            *(int*)(s_UiTargetInfo_00579278 + 8) = 0x100;    // biHeight, positive: bottom-up

            UiStage("rastÃ©risation CONFIG.TXT (3 atlas)");
            RasteriseLabelSet(array, s_UiAtlas0Bits_00589c80, 0, 0, 0x12);
            RasteriseLabelSet(array, s_UiAtlas1Bits_005b9de0, 0x12, 0x12, 0x12);
            RasteriseLabelSet(array, s_UiAtlas2Bits_005c9de0, 0x24, 0x24, 0x12);

            // Ghidra: ACTION.TXT's widths are written **downward** from DAT_00589754, which is what
            // makes FUN_005108f0's negative label ids resolve.
            UiStage("rastÃ©risation ACTION.TXT");
            ReadUiTextFile(array, s_UiFileActionText);
            NativeMemory.Fill(s_UiAtlasNegativeBits_00579708, UiAtlasBytes, 0);
            byte* text = stackalloc byte[0x20];
            for (int i = 0; i < 0x12; i++)
            {
                UiLineToBuffer(array, i, text, 0x20);
                s_UiLabelWidths_00589758[-1 - i] =
                    RasteriseLabel(s_UiAtlasNegativeBits_00579708, s_UiTargetInfo_00579278, i, text, 0);
            }

            // Ghidra: the joystick set is the only one drawn twice â€” 0x24 entries folded onto 0x12
            // rows, the second half offset by 0x80. Its widths are not recorded.
            UiStage("rastÃ©risation JOYSTICK.TXT");
            ReadUiTextFile(array, s_UiFileJoystickText);
            NativeMemory.Fill(s_UiAtlasJoystickBits_00599de0, UiAtlasBytes, 0);
            for (int i = 0; i < 0x24; i++)
            {
                UiLineToBuffer(array, i, text, 0x20);
                RasteriseLabel(s_UiAtlasJoystickBits_00599de0, s_UiTargetInfo_00579278,
                               i % 0x12, text, i < 0x12 ? 0 : 0x80);
            }

            UiStage("rastÃ©risation KEYBOARD.TXT");
            ReadUiTextFile(array, s_UiFileKeyboardText);
            RasteriseKeyLabels(array);
        }

        UiStage("textures BACKGRND/FRAMEMOD");
        BuildPictureTextures();
        UiStage("textures des atlas");

        s_UiAtlas0_00589c6c = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlas0Bits_00589c80, s_UiAtlasColourKey_005d9df0);
        s_UiAtlas1_00589830 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlas1Bits_005b9de0, s_UiAtlasColourKey_005d9df0);
        s_UiAtlas2_00589834 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlas2Bits_005c9de0, s_UiAtlasColourKey_005d9df0);
        s_UiAtlasNegative_00589708 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlasNegativeBits_00579708, s_UiAtlasColourKey_005d9df0);
        s_UiAtlasJoystick_00599dd8 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiAtlasJoystickBits_00599de0, s_UiAtlasColourKey_005d9df0);
        s_UiAtlasKeyboard_00589c78 = UiCreateTexture(s_UiTargetInfo_00579278, s_UiKeyboardBits_005a9de0, s_UiAtlasColourKey_005d9df0);
    }

    /// <summary>One of the three CONFIG.TXT label runs: clear the atlas, draw 18 rows, record 18 widths.</summary>
    private static void RasteriseLabelSet(UiTextFile array, void* bits, int firstEntry, int firstWidth, int count)
    {
        NativeMemory.Fill(bits, UiAtlasBytes, 0);
        byte* text = stackalloc byte[0x20];

        for (int i = 0; i < count; i++)
        {
            UiLineToBuffer(array, firstEntry + i, text, 0x20);
            s_UiLabelWidths_00589758[firstWidth + i] =
                RasteriseLabel(bits, s_UiTargetInfo_00579278, i, text, 0);
        }
    }

    /// <summary>
    /// ECHAFAUDAGE TEMPORAIRE. Jalonne la mise en place : le tas s'est corrompu quelque part dans
    /// cette chaÃ®ne au premier essai, et une corruption de tas ne lÃ¨ve pas lÃ  oÃ¹ elle est causÃ©e.
    /// La derniÃ¨re Ã©tape journalisÃ©e avant la mort nomme la coupable.
    /// </summary>
    private static void UiStage(string stage)
    {
        if (s_RunOptionsSetup) Console.Error.WriteLine($"[UI] {stage}");
    }

    /// <summary>ECHAFAUDAGE : entrÃ©e et sortie de chaque page, sous OPENFORESIA_TRACE_MENU=1.</summary>
    private static void UiPageTrace(string page)
    {
        if (s_TraceMenu) Console.Error.WriteLine($"[UI] {page}");
    }

    /// <summary>Ghidra: FUN_0050fa50 â€” the CD path, then LoadBitmapFile in its silent mode.</summary>
    private static byte* LoadUiFile(string name, bool silent)
    {
        byte* fileName = ToAnsiZ(name);
        byte* path = BuildCdPath(fileName);
        byte* file = LoadBitmapFile(path, silent ? 0u : 1u);
        Free(path);
        Free(fileName);
        return file;
    }

    private static void LoadUiArray(void* array, string name)
    {
        byte* fileName = ToAnsiZ(name);
        byte* path = BuildCdPath(fileName);
        // ECHAFAUDAGE : OPENFORESIA_OPTIONS_BADPATH=1 remplace le chemin par un fichier absent,
        // ce qui fait sortir le chargeur juste aprÃ¨s l'ouverture ratÃ©e. SÃ©pare Â« la faute est dans
        // l'ouverture Â» de Â« la faute est dans la lecture des lignes Â».
        if (Environment.GetEnvironmentVariable("OPENFORESIA_OPTIONS_BADPATH") == "1")
        {
            byte* absent = ToAnsiZ("Z:/pas-de-fichier.txt");
            UiStage("  LoadArray(chemin absent)");
            LoadArray(array, absent, 0, 0, 0, 0);
            UiStage("  LoadArray revenu (chemin absent)");
            Free(absent);
            Free(path);
            Free(fileName);
            return;
        }

        // ECHAFAUDAGE : OPENFORESIA_OPTIONS_NOGROW=1 met le drapeau 8 (Â« ne pas agrandir Â»), ce qui
        // court-circuite l'appel de croissance du chargeur. SÃ©pare Â« la croissance corrompt Â» de
        // Â« la lecture/stockage des lignes corrompt Â».
        int flags = Environment.GetEnvironmentVariable("OPENFORESIA_OPTIONS_NOGROW") == "1" ? 8 : 0;

        // ECHAFAUDAGE : le 4e argument du chargeur est un **plafond de lignes** (0 = illimitÃ©).
        // OPENFORESIA_OPTIONS_MAXLINES borne la lecture, pour trouver Ã  quelle ligne Ã§a casse.
        _ = int.TryParse(Environment.GetEnvironmentVariable("OPENFORESIA_OPTIONS_MAXLINES"), out int maxLines);

        UiStage($"  LoadArray(\"{System.Text.Encoding.Latin1.GetString(path, 40).TrimEnd('\0', ' ')}\") lignes<={maxLines} drapeaux={flags}");
        LoadArray(array, path, 0, maxLines, 0, flags);
        UiStage("  LoadArray revenu");
        Free(path);
        Free(fileName);
        UiStage("  libÃ©rÃ©");
    }

    // Ghidra: the seven file names at 0x005630c8..0x00563134, read verbatim from Legacy/Blaze.exe â€”
    // forward slashes, like every other CD path this reconstruction builds.
    private const string s_UiFileKeyboardText = "DATA/KEYBOARD.TXT";
    private const string s_UiFileJoystickText = "DATA/JOYSTICK.TXT";
    private const string s_UiFileActionText = "DATA/ACTION.TXT";
    private const string s_UiFileFont = "DATA/FONT.PIC";
    private const string s_UiFileConfigText = "DATA/CONFIG.TXT";
    private const string s_UiFileFrame = "DATA/FRAMEMOD.PIC";
    private const string s_UiFileBackground = "DATA/BACKGRND.PIC";

    /// <summary>Ghidra: the 0x4000-dword clears â€” 64 KB, one 256x256 8-bit page.</summary>
    private const nuint UiAtlasBytes = 0x4000 * 4;

    /// <summary>
    /// Ghidra: la borne de boucle de FUN_0050f9b0 â€” de `0x599c81` Ã  `0x599dd6` par pas de 4, soit
    /// **86** entrÃ©es. Les 85 premiÃ¨res sont les noms de touches de KEYBOARD.TXT ; la 86áµ‰ est
    /// l'invite que l'Ã©diteur affiche Ã  la place d'une touche pendant la capture.
    /// </summary>
    private const int UiKeyLabelCount = 0x56;

    private static int s_UiSetupDone_005d9de4;                  // Ghidra: DAT_005d9de4
    private static byte* s_UiBackgroundFile_00579264;           // Ghidra: DAT_00579264
    private static byte* s_UiFrameFile_00589838;                // Ghidra: DAT_00589838
    private static byte* s_UiFontFile_00589c74;                 // Ghidra: DAT_00589c74

    private static readonly byte* s_UiTargetInfo_00579278 = (byte*)NativeMemory.AllocZeroed(0x10a * 4);
    private static readonly byte* s_UiBackgroundInfo_00578e30 = (byte*)NativeMemory.AllocZeroed(0x10a * 4);
    private static readonly byte* s_UiFrameInfo_00589840 = (byte*)NativeMemory.AllocZeroed(0x10a * 4);

    private static readonly void* s_UiAtlas0Bits_00589c80 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiAtlas1Bits_005b9de0 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiAtlas2Bits_005c9de0 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiAtlasNegativeBits_00579708 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiAtlasJoystickBits_00599de0 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly void* s_UiKeyboardBits_005a9de0 = NativeMemory.AllocZeroed(UiAtlasBytes);
    private static readonly byte* s_UiKeyPlacement_00599c80 = (byte*)NativeMemory.AllocZeroed(UiKeyLabelCount * 4 + 8);

    // Ghidra: DAT_005d9de8 and DAT_005d9df0 â€” the two colour keys, DDCOLORKEY pairs of palette
    // indices. TODO(REVERSE): neither is written anywhere in the reconstructed graph and both read
    // as uninitialised data in the binary, so they are zero â€” index 0 transparent, which is the
    // usual convention for these sheets but is not confirmed.
    private static readonly uint* s_UiFrameColourKey_005d9de8 = (uint*)NativeMemory.AllocZeroed(2, sizeof(uint));
    private static readonly uint* s_UiAtlasColourKey_005d9df0 = (uint*)NativeMemory.AllocZeroed(2, sizeof(uint));

    // Ghidra: &DAT_0056e7b0, the glyph widths, is **already reconstructed** further up this file
    // for the UI text engine. A second copy transcribed here from the same hex dump carried one
    // 0x06 too many â€” the duplicate-symbol error is what caught it. One table, one definition.

    // Ghidra: the texture objects this layer draws from, and the label-width table. All are built
    // at runtime by FUN_0050fab0 â€” 0x00589758 reads as uninitialised data in the binary, which is
    // an answer rather than a gap: nothing but code can fill it.
    private static void* s_UiAtlas0_00589c6c;
    private static void* s_UiAtlas1_00589830;
    private static void* s_UiAtlas2_00589834;
    private static void* s_UiAtlasNegative_00589708;
    private static void* s_UiAtlasJoystick_00599dd8;
    private static void* s_UiAtlasKeyboard_00589c78;
    private static void* s_UiBackgroundTexture_0057926c;
    private static void* s_UiFrameAtlas_00599ddc;

    /// <summary>
    /// Ghidra: &amp;DAT_00589758 â€” the label widths, **indexed on both sides of that address**.
    /// FUN_0050fab0 writes three contiguous 18-entry runs upward from it (0x00589758, 0x005897a0,
    /// 0x005897e8) for label ids 0..0x35, and a fourth run **downward** from 0x00589754 for the
    /// ACTION.TXT labels, which FUN_005108f0 reaches with negative ids. One block with the base
    /// pointer placed inside it reproduces that exactly; two arrays would not.
    /// </summary>
    private static readonly int* s_UiLabelWidths_00589758 =
        (int*)NativeMemory.AllocZeroed(0x100, sizeof(int)) + 0x20;

    // ---------------------------------------------------------------------------------------
    // Deliberately deferred. Each has a decompiled body in Blaze.c and is ordinary uncovered
    // code, not blocked work; they are stubbed so the frame structure above can be exercised and
    // the runtime can name the next target. See the change proposal for why.
    // ---------------------------------------------------------------------------------------


    /// <summary>
    /// Ghidra: FUN_00512320 @ 0x00512320 â€” the options page. Its first statement is
    /// <c>FUN_0050fab0()</c>, which is reconstructed, so the setup runs for real here; the page's
    /// own loop is not, and says so.
    /// </summary>
    private static void OptionsScreenStub()
    {
        OptionsSetup();     // Ghidra: FUN_0050fab0

        // Ghidra: la boucle de page. Quatre entrÃ©es, sÃ©lectionnÃ©es entre 0 et 3 ; la quatriÃ¨me
        // quitte par une exception logicielle, Ã©chap quitte en libÃ©rant les huit textures.
        while (true)
        {
            RestoreLostUiTextures();                                    // FUN_0050fe90
            UiBeginScene();                                             // (*device + 0xb0)
            DrawUiBackground();                                         // FUN_005101c0

            UiDrawFrame(0x4c, 0x20, 0xa8, 100);                         // FUN_00510740
            UiDrawLabel(0xa0, 0x32, 0, 1, 0xffffff);                    // FUN_005108f0
            UiDrawLabel(0x5c, 0x48, 1, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x54, 2, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x60, 3, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x6c, 0x10, 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiOptionsSelection_005d9e48 + 6) * 0xc);   // FUN_00510a70

            UiEndScene();                                               // (*device + 0xb4)
            UiPresent();                                                // (*device + 0xb8)

            uint action;
            fixed (int* selection = &s_UiOptionsSelection_005d9e48)
                action = UiSelect(selection, 0, 3);                     // FUN_00510aa0

            if (action == 0xffffffff)
            {
                ReleaseUiTextures();
                return;
            }

            if ((action & 1) != 0)
            {
                switch (s_UiOptionsSelection_005d9e48)
                {
                    case 0: PlayUiSound38(); DisplayOptionsPage(); break;    // FUN_005110b0
                    case 1: PlayUiSound38(); SoundOptionsPage(); break;      // FUN_005113d0
                    case 2: PlayUiSound38(); ControlOptionsPage(); break;    // FUN_00512220
                    case 3:
                        UiPageTrace("Quitter le jeu");
                        PlayUiSound38();
                        // Ghidra: RaiseQuitException() â€” la sortie du jeu depuis
                        // la page d'options, la mÃªme exception logicielle que le lecteur de vidÃ©o
                        // utilise pour WM_QUIT.
                        RaiseQuitException();
                        break;
                }
            }

            Ordinal_132(2);
        }
    }

    /// <summary>Ghidra: les trois slots de scÃ¨ne que la page emprunte, sur le mÃªme device.</summary>
    private static void UiBeginScene()
        => ((delegate* unmanaged[Thiscall]<void*, void>)UiDeviceVtable[0xb0 / 4])(Ordinal_750());

    private static void UiEndScene()
        => ((delegate* unmanaged[Thiscall]<void*, int>)UiDeviceVtable[0xb4 / 4])(Ordinal_750());

    private static void UiPresent()
        => ((delegate* unmanaged[Thiscall]<void*, int>)UiDeviceVtable[0xb8 / 4])(Ordinal_750());

    /// <summary>
    /// Ghidra: la sortie par Ã©chap libÃ¨re les huit textures de la page â€” chacune par le slot
    /// <c>[0]</c> de sa classe, avec le drapeau 1 des destructeurs C++ de l'Ã©poque.
    /// </summary>
    private static void ReleaseUiTextures()
    {
        ReleaseUiTexture(ref s_UiAtlas0_00589c6c);
        ReleaseUiTexture(ref s_UiAtlas1_00589830);
        ReleaseUiTexture(ref s_UiAtlasJoystick_00599dd8);
        ReleaseUiTexture(ref s_UiBackgroundTexture_0057926c);
        ReleaseUiTexture(ref s_UiAtlas2_00589834);
        ReleaseUiTexture(ref s_UiAtlasNegative_00589708);
        ReleaseUiTexture(ref s_UiFrameAtlas_00599ddc);
        ReleaseUiTexture(ref s_UiAtlasKeyboard_00589c78);
    }

    private static void ReleaseUiTexture(ref void* texture)
    {
        if (texture == null) return;
        ((delegate* unmanaged[Thiscall]<void*, uint, int>)(*(void***)texture)[0])(texture, 1);
        texture = null;
    }

    /// <summary>
    /// Ghidra: FUN_00510a00 @ 0x00510a00 â€” une jauge : la glissiÃ¨re (0x86 x 10, prise en
    /// <c>(0, 0x20)</c> de la planche du cadre) puis le curseur (8 x 10, pris en <c>(0x8e, 0x20)</c>)
    /// posÃ© Ã  <c>x + valeur + 1</c>.
    /// </summary>
    private static void UiDrawGauge(int x, int y, int value)
    {
        UiBlit(x, y, 0x86, 10, 0, 0x20, s_UiFrameAtlas_00599ddc, 0x86, 10, 0xffffff);
        UiBlit(x + value + 1, y, 8, 10, 0x8e, 0x20, s_UiFrameAtlas_00599ddc, 8, 10, 0xffffff);
    }

    /// <summary>
    /// Ghidra: FUN_0050f980 @ 0x0050f980 â€” rÃ©applique le gamma : dÃ©truit les deux textures issues
    /// des `.PIC` et les refabrique, palettes repassÃ©es dans <c>Gamma</c> au nouveau rÃ©glage.
    /// </summary>
    private static void RebuildGammaTextures()
    {
        ReleaseUiTexture(ref s_UiBackgroundTexture_0057926c);
        ReleaseUiTexture(ref s_UiFrameAtlas_00599ddc);
        BuildPictureTextures();     // Ghidra: FUN_0050f7d0
    }

    /// <summary>
    /// Ghidra: FUN_005110b0 @ 0x005110b0 â€” la page Â« Affichage Â». Cinq rÃ©glages, six quand le
    /// device annonce la capacitÃ© que son slot <c>+0xE0</c> rapporte : taille de l'Ã©cran, gamma,
    /// filtrage de texture, affichage de statut, puis deux pages filles.
    /// </summary>
    private static void DisplayOptionsPage()
    {
        UiPageTrace("entrée page Affichage");
        // Ghidra: le slot +0xE0 dÃ©cide si la sixiÃ¨me ligne existe. Non portÃ© par le device
        // synthÃ©tique, il rend zÃ©ro â€” donc cinq lignes, ce qui est le cas nominal.
        int extra = ((delegate* unmanaged[Thiscall]<void*, int>)UiDeviceVtable[0xe0 / 4])(Ordinal_750()) != 0 ? 1 : 0;

        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, extra * 0xc + 0x88);
            UiDrawLabel(0xa0, 0x32, 4, 1, 0xffffff);
            UiDrawLabel(0x5c, 0x48, 5, 0, 0xffffff);
            UiDrawGauge(100, 0x55, (int)(s_SizeSetting_005de160 * 0x79) / UiDetailLimit + 4);
            UiDrawLabel(0x5c, 0x60, 7, 0, 0xffffff);
            UiDrawGauge(100, 0x6d, s_Unknown_005d9de0 - 8);
            UiDrawLabel(0x5c, 0x78, 9 - (*GetTexFilterPossiblePtr() != 0 ? 1 : 0), 0, 0xffffff);
            UiDrawLabel(0x5c, 0x84, 0xb - (s_SmallTextureFlag != 0 ? 1 : 0), 0, 0xffffff);
            UiDrawLabel(0x5c, 0x90, 0x25, 0, 0xffffff);
            if (extra != 0) UiDrawLabel(0x5c, 0x9c, 0x24, 0, 0xffffff);

            // Ghidra: la position du curseur saute une ligne au-delÃ  de l'entrÃ©e 1 â€” les deux
            // premiÃ¨res portent une jauge, donc occupent deux crans chacune.
            int line = (s_UiDisplaySelection_005d9e10 > 1 ? 1 : 0) + 6 + s_UiDisplaySelection_005d9e10
                       + (s_UiDisplaySelection_005d9e10 > 0 ? 1 : 0);
            UiDrawCursor(0x4b, line * 0xc);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiDisplaySelection_005d9e10)
                action = UiSelect(selection, 0, 4 + extra);

            if (action == 0xffffffff) return;

            if ((action & 1) != 0)
            {
                if (s_UiDisplaySelection_005d9e10 == 4) TimingPage();        // FUN_00510d40
                if (s_UiDisplaySelection_005d9e10 == 5) ResolutionPage();    // FUN_00510e60
            }

            if ((action & 7) != 0)
            {
                if (s_UiDisplaySelection_005d9e10 == 2)
                {
                    PlayUiSound38();
                    uint* filter = GetTexFilterPossiblePtr();
                    *filter = *filter == 0 ? 1u : 0u;
                }
                else if (s_UiDisplaySelection_005d9e10 == 3 && s_SizeSetting_005de160 > 6)
                {
                    PlayUiSound38();
                    s_SmallTextureFlag = s_SmallTextureFlag == 0 ? 1u : 0u;
                }
            }

            if ((action & 2) != 0)
            {
                if (s_UiDisplaySelection_005d9e10 == 0)
                {
                    if (s_SizeSetting_005de160 != 0)
                    {
                        PlayUiSound3A();
                        s_SizeSetting_005de160--;
                        SetShift((int)((0xb - s_SizeSetting_005de160) * 0x10),
                                 (int)((0xb - s_SizeSetting_005de160) * 0xc),
                                 (int)s_SizeSetting_005de160, 0);
                        if (s_SizeSetting_005de160 < 8) s_SmallTextureFlag = 1;
                    }
                }
                else if (s_UiDisplaySelection_005d9e10 == 1 && s_Unknown_005d9de0 > 8)
                {
                    PlayUiSound3A();
                    s_GammaIni -= 8;
                    RebuildGammaTextures();
                }
            }

            if ((action & 4) != 0)
            {
                if (s_UiDisplaySelection_005d9e10 == 0)
                {
                    if (s_SizeSetting_005de160 < UiDetailLimit)
                    {
                        PlayUiSound3A();
                        s_SizeSetting_005de160++;
                        SetShift((int)((0xb - s_SizeSetting_005de160) * 0x10),
                                 (int)((0xb - s_SizeSetting_005de160) * 0xc),
                                 (int)s_SizeSetting_005de160, 0);
                    }
                }
                else if (s_UiDisplaySelection_005d9e10 == 1 && s_Unknown_005d9de0 < 0x88)
                {
                    PlayUiSound3A();
                    s_GammaIni += 8;
                    RebuildGammaTextures();
                }
            }

            Ordinal_132(2);
        }
    }

    /// <summary>Ghidra: `*(int *)(&DAT_00562e00 + DAT_00566cc4 * 4)` â€” le plafond de dÃ©tail de la rÃ©solution courante.</summary>
    private static int UiDetailLimit
        => s_ResolutionIndex < (uint)s_ResolutionDetailLimit.Length
            ? s_ResolutionDetailLimit[s_ResolutionIndex]
            : 1;

    /// <summary>
    /// Ghidra: FUN_005113d0 @ 0x005113d0 â€” la page Â« Son Â» : volume CD, volume des effets, et
    /// l'interrupteur de musique. Les deux volumes bougent par pas de 8 entre 0 et 0x80, et chaque
    /// changement est appliquÃ© immÃ©diatement.
    /// </summary>
    private static void SoundOptionsPage()
    {
        UiPageTrace("entrée page Son");
        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, 0x70);
            UiDrawLabel(0xa0, 0x32, 0xc, 1, 0xffffff);
            UiDrawLabel(0x5c, 0x48, 0xd, 0, 0xffffff);
            UiDrawGauge(100, 0x55, (int)s_CdVolumeIni);
            UiDrawLabel(0x5c, 0x60, 0xe, 0, 0xffffff);
            UiDrawGauge(100, 0x6d, (int)s_SoundVolumeIni);
            UiDrawLabel(0x5c, 0x78, 0x28 - (s_MusicEnabled != 0 ? 1 : 0), 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiSoundSelection_005d9e14 + 3) * 0x18);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiSoundSelection_005d9e14)
                action = UiSelect(selection, 0, 2);

            if (action == 0xffffffff) return;

            if ((action & 7) != 0 && s_UiSoundSelection_005d9e14 == 2)
            {
                PlayUiSound38();
                ToggleMusicEnabled();       // Ghidra: FUN_00513860
            }

            if ((action & 2) != 0)
            {
                if (s_UiSoundSelection_005d9e14 == 0 && s_CdVolumeIni != 0)
                {
                    PlayUiSound3A();
                    s_CdVolumeIni -= 8;
                    ApplyCdVolume();
                }
                else if (s_UiSoundSelection_005d9e14 == 1 && s_SoundVolumeIni != 0)
                {
                    PlayUiSound3A();
                    s_SoundVolumeIni -= 8;
                    ApplyAllSoundBufferVolumes();
                }
            }

            if ((action & 4) != 0)
            {
                if (s_UiSoundSelection_005d9e14 == 0 && s_CdVolumeIni < 0x80)
                {
                    PlayUiSound3A();
                    s_CdVolumeIni += 8;
                    ApplyCdVolume();
                }
                else if (s_UiSoundSelection_005d9e14 == 1 && s_SoundVolumeIni < 0x80)
                {
                    PlayUiSound3A();
                    s_SoundVolumeIni += 8;
                    ApplyAllSoundBufferVolumes();
                }
            }

            Ordinal_132(2);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00512220 @ 0x00512220 â€” la page Â« ContrÃ´le Â», deux entrÃ©es qui ouvrent chacune
    /// une page de configuration.
    /// </summary>
    private static void ControlOptionsPage()
    {
        UiPageTrace("entrée page Contrôle");
        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, 0x4c);
            UiDrawLabel(0xa0, 0x32, 0xf, 1, 0xffffff);
            UiDrawLabel(0x5c, 0x48, 0x11, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x54, 0x12, 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiControlSelection_005d9e44 + 6) * 0xc);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiControlSelection_005d9e44)
                action = UiSelect(selection, 0, 1);

            if (action == 0xffffffff) return;

            if ((action & 1) != 0)
            {
                if (s_UiControlSelection_005d9e44 == 0) { PlayUiSound38(); KeyConfigPage(); }
                else if (s_UiControlSelection_005d9e44 == 1) { PlayUiSound38(); JoystickAssignmentPage(); }
            }

            Ordinal_132(2);
        }
    }

    private static int s_UiDisplaySelection_005d9e10;   // Ghidra: DAT_005d9e10
    private static int s_UiSoundSelection_005d9e14;     // Ghidra: DAT_005d9e14
    private static int s_UiControlSelection_005d9e44;   // Ghidra: DAT_005d9e44

    // Les quatre pages filles. Toutes ont un corps dÃ©compilÃ© et aucune dÃ©pendance bloquÃ©e ; elles
    // sont nommÃ©es pour que le runtime dÃ©signe la suivante.
    /// <summary>
    /// Ghidra: FUN_00510d40 @ 0x00510d40 â€” la page **Timing**, quatre choix. La sÃ©lection est
    /// initialisÃ©e depuis le rÃ©glage courant Ã  la premiÃ¨re ouverture seulement (bit 0 de
    /// DAT_00579260), et n'est appliquÃ©e qu'Ã  la validation.
    /// </summary>
    private static void TimingPage()
    {
        if ((s_UiPageFlags_00579260 & 1) == 0)
        {
            s_UiPageFlags_00579260 |= 1;
            s_UiTimingSelection_00579268 = (int)s_TimingMode;
        }

        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, 100);
            UiDrawLabel(0xa0, 0x32, 0x29, 1, 0xffffff);
            UiDrawLabel(0x5c, 0x48, 0x32, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x54, 0x2a, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x60, 0x2b, 0, 0xffffff);
            UiDrawLabel(0x5c, 0x6c, 0x31, 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiTimingSelection_00579268 + 6) * 0xc);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiTimingSelection_00579268)
                action = UiSelect(selection, 0, 3);

            if (action == 0xffffffff) return;

            if ((action & 1) != 0)
            {
                s_TimingMode = (uint)s_UiTimingSelection_00579268;
                SetTimingMode(s_UiTimingSelection_00579268);
                return;
            }

            Ordinal_132(2);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00510e60 @ 0x00510e60 â€” la page **rÃ©solution**. Le device annonce les modes
    /// qu'il accepte par un masque de bits (slot <c>+0x1c</c>), la page en fait une liste, et une
    /// validation qui change rÃ©ellement de mode dÃ©truit les huit textures, refait la mise en place
    /// et recale le zoom.
    /// <para>
    /// TODO(REVERSE): le device synthÃ©tique ne porte ni <c>+0x1c</c> (le masque des modes) ni
    /// <c>+0xE8</c> (le changement effectif, qui est ce que <c>ChangeResolution</c> appelle). Le
    /// masque revient donc Ã  zÃ©ro, la liste est vide, et la page n'affiche que son titre. C'est
    /// une dÃ©gradation visible et sans risque : le garde ci-dessous empÃªche d'indexer une liste
    /// vide, ce que l'original n'a pas Ã  faire puisque son device annonce toujours au moins un
    /// mode.
    /// </para>
    /// </summary>
    private static void ResolutionPage()
    {
        int* modes = stackalloc int[32];
        int count = 0;

        uint mask = (uint)((delegate* unmanaged[Thiscall]<void*, int>)UiDeviceVtable[0x1c / 4])(Ordinal_750());
        for (int bit = 0; mask != 0; mask >>= 1, bit++)
            if ((mask & 1) != 0) modes[count++] = bit;

        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, count * 0xc + 0x38);
            UiDrawLabel(0xa0, 0x32, 0x26, 1, 0xffffff);
            for (int i = 0, y = 0x48; i < count; i++, y += 0xc)
                UiDrawLabel(0x5c, y, modes[i] + 0x2c, 0, 0xffffff);
            UiDrawCursor(0x4b, (s_UiResolutionSelection_005d9e0c + 6) * 0xc);

            UiEndScene();
            UiPresent();

            uint action;
            fixed (int* selection = &s_UiResolutionSelection_005d9e0c)
                action = UiSelect(selection, 0, count - 1);

            if (action == 0xffffffff) return;

            // Le garde : sans mode annoncÃ© il n'y a rien Ã  choisir.
            if ((action & 1) != 0 && count > 0
                && (uint)s_UiResolutionSelection_005d9e0c < (uint)count
                && modes[s_UiResolutionSelection_005d9e0c] != (int)s_ResolutionIndex)
            {
                ReleaseUiTextures();
                ReInitializeAllTextures();

                s_ResolutionIndex = (uint)modes[s_UiResolutionSelection_005d9e0c];
                ChangeResolution((int)s_ResolutionIndex);

                // Ghidra: la file est vidÃ©e de ses WM_QUIT (0x12) avant de continuer.
                MSG message;
                while (PeekMessageA(&message, IntPtr.Zero, 0x12, 0x12, 1) != 0) { }

                s_SizeSetting_005de160 = UiDetailLimit;
                SetShift((int)((0xb - s_SizeSetting_005de160) * 0x10),
                         (int)((0xb - s_SizeSetting_005de160) * 0xc),
                         (int)s_SizeSetting_005de160, 0);
                if (s_SizeSetting_005de160 < 8) s_SmallTextureFlag = 1;

                OptionsSetup();     // Ghidra: DAT_00578e28 = Ordinal_750(); FUN_0050fab0();
            }

            Ordinal_132(2);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005119c0 @ 0x005119c0 â€” l'affectation des manettes. Quatre lignes Â« joueur N Â»
    /// suivies du numÃ©ro de manette attribuÃ©, puis quatre lignes Â« configurer Â», grisÃ©es
    /// (0x5f5f5f au lieu de 0xffffff) tant que le joueur correspondant n'a pas de manette.
    /// <para>
    /// L'affectation est un rang compact : attribuer Ã  un joueur lui donne le rang suivant libre et
    /// dÃ©cale ceux d'aprÃ¨s ; le retirer les dÃ©cale en arriÃ¨re. Les quatre lignes du bas sont
    /// sautÃ©es Ã  la navigation quand elles sont inactives.
    /// </para>
    /// </summary>
    private static void JoystickAssignmentPage()
    {
        UiPageTrace("entrée page Manettes");
        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x4c, 0x20, 0xa8, 0x94);
            UiDrawLabel(0xa0, 0x32, 0x20, 1, 0xffffff);

            for (int player = 0; player < 4; player++)
            {
                int y = 0x48 + player * 0xc;
                int label = 0x13 + player;
                UiDrawLabel(0x5c, y, label, 0, 0xffffff);
                // Ghidra: DAT_005897a4 + player*4 â€” la largeur du libellÃ© qu'on vient de poser,
                // prise dans la table des largeurs, sert de dÃ©calage pour la valeur.
                UiDrawLabel(s_UiLabelWidths_00589758[label] + 0x5c, y,
                            s_JoystickSlot[player] + 0x18, 0, 0xffffff);
            }

            for (int player = 0; player < 4; player++)
            {
                uint colour = s_JoystickSlot[player] != -1 ? 0xffffffu : 0x5f5f5fu;
                UiDrawLabel(0x5c, 0x78 + player * 0xc, 0x1c + player, 0, colour);
            }

            UiDrawCursor(0x4b, (s_UiJoystickSelection_005d9e38 + 6) * 0xc);
            UiEndScene();
            UiPresent();

            int before = s_UiJoystickSelection_005d9e38;
            uint action;
            fixed (int* selection = &s_UiJoystickSelection_005d9e38)
                action = UiSelect(selection, 0, 7);

            SkipInactiveJoystickRows(before);
            if (action == 0xffffffff) return;

            int row = s_UiJoystickSelection_005d9e38;

            if ((action & 7) != 0 && row is >= 0 and < 4)
            {
                PlayUiSound3A();
                ToggleJoystickAssignment(row);
            }

            if ((action & 1) != 0 && row is >= 4 and <= 7)
            {
                if (s_JoystickSlot[row - 4] < 0) PlayUiSoundError();
                else { PlayUiSound38(); JoystickButtonsPageStub(row - 4); }
            }

            Ordinal_132(2);
        }
    }

    /// <summary>
    /// Ghidra: la boucle qui repousse le curseur hors des quatre lignes Â« configurer Â» inactives â€”
    /// vers le haut si on y arrivait en descendant... et l'inverse, selon le sens du mouvement.
    /// </summary>
    private static void SkipInactiveJoystickRows(int before)
    {
        int row = s_UiJoystickSelection_005d9e38;
        if (row < 4 || s_JoystickSlot[row - 4] != -1) return;

        if (row < before)
        {
            int candidate = row;
            while (candidate > 3 && s_JoystickSlot[candidate - 4] == -1) candidate--;
            s_UiJoystickSelection_005d9e38 = candidate;
            return;
        }

        while (row < 8 && s_JoystickSlot[row - 4] == -1) row++;
        s_UiJoystickSelection_005d9e38 = row == 8 ? before : row;
    }

    /// <summary>
    /// Ghidra: attribuer donne au joueur le rang Ã©gal au nombre de joueurs dÃ©jÃ  servis avant lui et
    /// incrÃ©mente ceux d'aprÃ¨s ; retirer met -1 et dÃ©crÃ©mente ceux d'aprÃ¨s. Les rangs restent donc
    /// toujours compacts.
    /// </summary>
    private static void ToggleJoystickAssignment(int player)
    {
        if (s_JoystickSlot[player] == -1)
        {
            int rank = 0;
            for (int i = 0; i < player; i++) if (s_JoystickSlot[i] >= 0) rank++;
            s_JoystickSlot[player] = rank;
            for (int i = player + 1; i < 4; i++) if (s_JoystickSlot[i] != -1) s_JoystickSlot[i]++;
        }
        else
        {
            s_JoystickSlot[player] = -1;
            for (int i = player + 1; i < 4; i++) if (s_JoystickSlot[i] != -1) s_JoystickSlot[i]--;
        }
    }

    private static int s_UiPageFlags_00579260;            // Ghidra: DAT_00579260
    private static int s_UiTimingSelection_00579268;      // Ghidra: DAT_00579268
    private static int s_UiResolutionSelection_005d9e0c;  // Ghidra: DAT_005d9e0c
    private static int s_UiJoystickSelection_005d9e38;    // Ghidra: DAT_005d9e38

    /// <summary>Ghidra: FUN_0053dbc0 â€” le bip d'action refusÃ©e.</summary>
    private static void PlayUiSoundError() => PlaySoundEffect(0x3b, 0x400, 0x2000, 0x2000);

    private static void JoystickButtonsPageStub(int player)
        => throw new NotImplementedException(
            $"FUN_005115c0 : les boutons de la manette du joueur {player + 1} (168 lignes)");

    /// <summary>
    /// Ghidra: FUN_005115a0 @ 0x005115a0 â€” retrouve l'index du nom d'une touche Ã  partir de son
    /// scancode, en cherchant dans la table des 85 scancodes. -1 si la touche n'est pas listÃ©e.
    /// </summary>
    private static int KeyLabelForScancode(int scancode)
    {
        for (int i = 0; i < 0x55; i++)
            if (s_KeyScancodes_00562e18[i] == scancode) return i;
        return -1;
    }

    /// <summary>
    /// Ghidra: FUN_005109c0 @ 0x005109c0 puis FUN_005108b0 â€” dessine un nom de touche depuis
    /// l'atlas clavier, en lisant sa position dans la table de placement que
    /// <see cref="RasteriseKeyLabels"/> a remplie : x en <c>+0</c>, ligne en <c>+1</c>, largeur en
    /// <c>+2</c>.
    /// </summary>
    private static void DrawKeyName(int x, int y, int keyIndex, uint colour)
    {
        byte* placement = s_UiKeyPlacement_00599c80 + keyIndex * 4;
        int sourceX = placement[0];
        int row = placement[1];
        int width = *(short*)(placement + 2);
        UiBlit(x, y, width, 0xe, sourceX, row * 0xe, s_UiAtlasKeyboard_00589c78, width, 0xe, colour);
    }

    /// <summary>
    /// Ghidra: FUN_00511d40 @ 0x00511d40 â€” l'Ã©diteur de touches. Une liste de 0x31 lignes dÃ©filant
    /// par fenÃªtre de onze, bÃ¢tie sur la table de descripteurs `DAT_00562ed8` : huit actions
    /// globales, puis quatre blocs Â« Joueur N Â» (un en-tÃªte colorÃ© et neuf actions), et une
    /// derniÃ¨re ligne qui remet les liaisons par dÃ©faut.
    /// <para>
    /// Chaque ligne d'action montre jusqu'Ã  deux touches. Ce sont les catÃ©gories 0 et 1 de la mÃªme
    /// table de liaisons â€” `DAT_00564ba8` est `DAT_00564b58` dÃ©calÃ© de vingt entrÃ©es â€” c'est-Ã -dire
    /// les deux jeux clavier que <see cref="FoldKeyboardIntoPadState"/> lit dÃ©jÃ . Assigner une
    /// touche pousse l'ancienne en second et efface ce scancode partout ailleurs.
    /// </para>
    /// </summary>
    private static void KeyConfigPage()
    {
        UiPageTrace("entrée éditeur de touches");
        int capturing = -1;
        bool drainAfterCapture = false;

        while (true)
        {
            RestoreLostUiTextures();
            UiBeginScene();
            DrawUiBackground();

            UiDrawFrame(0x10, 0x20, 0x120, 0xb8);
            UiDrawLabel(0xa0, 0x32, 0x22, 1, 0xffffff);

            for (int slot = 0, y = 0x48; y < 0xcc; slot++, y += 0xc)
            {
                int row = slot + s_UiKeyScroll_005d9e40;
                if ((uint)row >= KeyRowCount) continue;

                byte* desc = s_KeyRowTable_00562ed8 + row * 8;
                uint colour = *(uint*)desc;
                int kind = (sbyte)desc[4];

                // Ghidra: un `kind` nÃ©gatif est une ligne d'en-tÃªte â€” elle ne porte qu'un libellÃ©.
                if (kind < 0) { UiDrawLabel(0x20, y, (sbyte)desc[6], 0, colour); continue; }

                UiDrawLabel(0x20, y, -1 - kind, 0, colour);

                // Ghidra: les quatre directions portent une flÃ¨che prise dans la planche du cadre.
                if (kind is > 3 and < 8)
                {
                    UiBlit(s_UiLabelWidths_00589758[-kind] + 0x20, y + 2, 8, 9,
                           s_KeyArrowColumn_00563060[kind - 4] * 8 + 0x98, 0x20,
                           s_UiFrameAtlas_00599ddc, 8, 9, 0xffffff);
                }

                int group = (sbyte)desc[5];
                if (group == -1) group = 0;
                int binding = s_KeyActionIndex_00562ec0[kind] + group * 0x3c;

                int x = 0xb9;
                int primary = KeyLabelForScancode(s_KeyBindings[binding]);
                if (primary != -1) { DrawKeyName(0xb9, y, primary, colour); x = 0xf3; }

                if (row != capturing || x == 0xb9)
                {
                    int secondary = KeyLabelForScancode(s_KeyBindings[binding + 20]);
                    if (secondary != -1) { DrawKeyName(x, y, secondary, colour); x += 0x3a; }
                }

                if (row == capturing) DrawKeyName(x, y, 0x55, colour);
                else if (x == 0xb9) UiDrawLabel(0xb9, y, 6, 0, colour);
            }

            UiDrawCursor(0xf, (s_UiKeySelection_005d9e3c - s_UiKeyScroll_005d9e40 + 6) * 0xc);
            UiEndScene();
            UiPresent();

            if (drainAfterCapture) { WaitForAllKeysReleased(); drainAfterCapture = false; }

            int before = s_UiKeySelection_005d9e3c;
            uint action;
            fixed (int* selection = &s_UiKeySelection_005d9e3c)
                action = UiSelect(selection, 0, 0x30);

            // Ghidra: page prÃ©cÃ©dente / page suivante dÃ©placent de dix, curseur et fenÃªtre ensemble.
            if ((action & 8) != 0)
            {
                s_UiKeySelection_005d9e3c -= 10;
                s_UiKeyScroll_005d9e40 -= 10;
                if (s_UiKeySelection_005d9e3c < 0) s_UiKeySelection_005d9e3c = 0;
                if (s_UiKeyScroll_005d9e40 < 0) s_UiKeyScroll_005d9e40 = 0;
            }
            if ((action & 0x10) != 0)
            {
                s_UiKeySelection_005d9e3c += 10;
                s_UiKeyScroll_005d9e40 += 10;
                if (s_UiKeySelection_005d9e3c > 0x30) s_UiKeySelection_005d9e3c = 0x30;
                if (s_UiKeyScroll_005d9e40 > 0x26) s_UiKeyScroll_005d9e40 = 0x26;
            }

            FollowKeyCursor();

            // Ghidra: une ligne d'en-tÃªte (kind == -1, mais pas -2) est franchie dans le sens du
            // dÃ©placement. La derniÃ¨re ligne, de type -2, reste sÃ©lectionnable.
            if ((sbyte)s_KeyRowTable_00562ed8[s_UiKeySelection_005d9e3c * 8 + 4] == -1)
                s_UiKeySelection_005d9e3c += s_UiKeySelection_005d9e3c > before ? 1 : -1;

            FollowKeyCursor();

            if (capturing == -1)
            {
                if ((action & 1) != 0)
                {
                    if (s_UiKeySelection_005d9e3c < 0x30)
                    {
                        PlayUiSound38();
                        capturing = s_UiKeySelection_005d9e3c;
                        WaitForAllKeysReleased();
                    }
                    else if (s_UiKeySelection_005d9e3c == 0x30)
                    {
                        PlayUiSound38();
                        ResetKeyBindings();
                    }
                }

                if (action == 0xffffffff) return;
                Ordinal_132(2);
                continue;
            }

            // Mode capture : la premiÃ¨re touche listÃ©e qu'on trouve enfoncÃ©e devient la liaison.
            PollJoystickState();
            int pressed = -1;
            for (int i = 0; i < 0x55; i++)
            {
                if ((s_KeyboardState_00604b50[s_KeyScancodes_00562e18[i]] & 0x80) == 0) continue;
                pressed = i;
                break;
            }

            if (pressed == -1) { Ordinal_132(2); continue; }

            int scancode = s_KeyScancodes_00562e18[pressed];
            ClearScancodeEverywhere(scancode);
            PlayUiSound38();

            byte* captured = s_KeyRowTable_00562ed8 + capturing * 8;
            int capturedGroup = (sbyte)captured[5];
            if (capturedGroup == -1) capturedGroup = 0;
            int target = s_KeyActionIndex_00562ec0[(sbyte)captured[4]] + capturedGroup * 0x3c;

            capturing = -1;
            drainAfterCapture = true;

            // L'ancienne liaison devient la seconde ; la nouvelle prend la premiÃ¨re place.
            s_KeyBindings[target + 20] = s_KeyBindings[target];
            s_KeyBindings[target] = scancode;
            Ordinal_132(2);
        }
    }

    /// <summary>Ghidra: la fenÃªtre suit le curseur, une ligne Ã  la fois, deux fois par tour.</summary>
    private static void FollowKeyCursor()
    {
        if (s_UiKeySelection_005d9e3c < s_UiKeyScroll_005d9e40) s_UiKeyScroll_005d9e40--;
        else if (s_UiKeyScroll_005d9e40 + 10 < s_UiKeySelection_005d9e3c) s_UiKeyScroll_005d9e40++;
    }

    /// <summary>
    /// Ghidra: la boucle d'attente qui suit une validation â€” tant qu'une touche listÃ©e est
    /// enfoncÃ©e, on repolle. Elle empÃªche la touche de validation d'Ãªtre capturÃ©e aussitÃ´t.
    /// </summary>
    private static void WaitForAllKeysReleased()
    {
        while (true)
        {
            PollJoystickState();
            byte any = 0;
            for (int i = 0; i < 0x55; i++)
                any |= (byte)(s_KeyboardState_00604b50[s_KeyScancodes_00562e18[i]] & 0x80);
            Ordinal_132(0);
            if (any == 0) return;
        }
    }

    /// <summary>
    /// Ghidra: le scancode retenu est effacÃ© de **toutes** les liaisons clavier â€” les deux
    /// catÃ©gories des quatre groupes, soit 2 x 20 entiers par groupe de 0x3c. La troisiÃ¨me
    /// catÃ©gorie, celle de la manette, n'est pas touchÃ©e.
    /// </summary>
    private static void ClearScancodeEverywhere(int scancode)
    {
        for (int group = 0; group < 4; group++)
            for (int i = 0; i < 40; i++)
                if (s_KeyBindings[group * 0x3c + i] == scancode) s_KeyBindings[group * 0x3c + i] = 0;
    }

    /// <summary>
    /// Ghidra: la derniÃ¨re ligne rÃ©tablit les liaisons du joueur 1 depuis la table de dÃ©fauts et
    /// efface celles des trois autres.
    /// </summary>
    private static void ResetKeyBindings()
    {
        for (int i = 0; i < 0x28; i++) s_KeyBindings[i] = s_KeyBindingDefaults_00564ab8[i];
        for (int group = 1; group < 4; group++)
            for (int i = 0; i < 0x28; i++) s_KeyBindings[group * 0x3c + i] = 0;
    }

    private static int s_UiKeySelection_005d9e3c;   // Ghidra: DAT_005d9e3c
    private static int s_UiKeyScroll_005d9e40;      // Ghidra: DAT_005d9e40

    private const int KeyRowCount = 0x31;

    // =====================================================================
    // Les cinq tables de l'Ã©diteur de touches, relevÃ©es dans Legacy/Blaze.exe.
    // =====================================================================

    /// <summary>
    /// Ghidra: &amp;DAT_00562e18 â€” les 85 scancodes DirectInput que l'Ã©diteur sait nommer, dans le
    /// mÃªme ordre que les lignes de KEYBOARD.TXT. Le 0xff qui suit est un terminateur.
    /// </summary>
    private static readonly byte[] s_KeyScancodes_00562e18 =
    [
        0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08, 0x09,0x0a,0x0b,0x0c,0x0d,0x0e,0x0f,0x10,
        0x11,0x12,0x13,0x14,0x15,0x16,0x17,0x18, 0x19,0x1a,0x1b,0x1c,0x1d,0x1e,0x1f,0x20,
        0x21,0x22,0x23,0x24,0x25,0x26,0x27,0x28, 0x29,0x2a,0x2b,0x2c,0x2d,0x2e,0x2f,0x30,
        0x31,0x32,0x33,0x34,0x35,0x36,0x37,0x38, 0x39,0x47,0x48,0x49,0x4a,0x4b,0x4c,0x4d,
        0x4e,0x4f,0x50,0x51,0x52,0x53,0x9d,0x56, 0xb5,0xb8,0xc7,0xc8,0xc9,0xcb,0xcd,0xcf,
        0xd0,0xd1,0xd2,0xd3,0x3b,
    ];

    /// <summary>
    /// Ghidra: &amp;DAT_00562ec0 â€” l'action d'une ligne vers son rang dans le bloc de vingt liaisons.
    /// </summary>
    private static readonly byte[] s_KeyActionIndex_00562ec0 =
    [
        0x0c,0x0e,0x0f,0x0d,0x07,0x05,0x06,0x04, 0x03,0x02,0x01,0x00,0x0b,0x08,0x12,0x13,
        0x10,0x11,0x00,0x00,0x03,0x01,0x02,0x00,
    ];

    /// <summary>
    /// Ghidra: `(&amp;DAT_0056305c)[kind]` pour kind 4..7 â€” la colonne de la flÃ¨che de direction dans
    /// la planche du cadre. Les quatre octets utiles sont ceux de 0x00563060.
    /// </summary>
    private static readonly byte[] s_KeyArrowColumn_00563060 = [0x03, 0x01, 0x02, 0x00];

    /// <summary>
    /// Ghidra: &amp;DAT_00564ab8 â€” les liaisons par dÃ©faut du joueur 1, quarante entiers : les deux
    /// jeux clavier. Ils correspondent exactement aux clÃ©s `K0000`..`K0113` du BLAZE.cfg installÃ©,
    /// ce qui les confirme.
    /// </summary>
    private static readonly int[] s_KeyBindingDefaults_00564ab8 =
    [
        0x47, 0x4f, 0x51, 0x49, 0x39, 0x2a, 0x38, 0x1d, 0x3b, 0x00,
        0x00, 0x3c, 0x48, 0x4d, 0x50, 0x4b, 0x00, 0x00, 0x0c, 0x0d,
        0xc7, 0xcf, 0xd1, 0xc9, 0x00, 0x00, 0x01, 0x1c, 0x00, 0x00,
        0x00, 0x00, 0xc8, 0xcd, 0xd0, 0xcb, 0xd2, 0xd3, 0x4a, 0x4e,
    ];

    /// <summary>
    /// Ghidra: &amp;DAT_00562ed8 â€” les 0x31 lignes de l'Ã©diteur, huit octets chacune : couleur sur
    /// quatre octets, puis l'action, le groupe et le libellÃ©, tous trois signÃ©s.
    /// <para>
    /// Transcrites en octets bruts plutÃ´t qu'interprÃ©tÃ©es : la table se relit alors ligne Ã  ligne
    /// contre le vidage du binaire, ce qu'une reconstruction structurÃ©e ne permet pas. La structure
    /// s'y lit quand mÃªme â€” huit actions globales en blanc, puis quatre blocs Â« Joueur N Â» dans les
    /// quatre teintes `FFBFBF`, `BFFFBF`, `BFBFFF`, `FFFFBF`, chacun un en-tÃªte (action -1) et neuf
    /// actions, et une derniÃ¨re ligne d'action -2 : la remise aux valeurs par dÃ©faut.
    /// </para>
    /// </summary>
    private static readonly byte* s_KeyRowTable_00562ed8 = BuildKeyRowTable();

    private static byte* BuildKeyRowTable()
    {
        ReadOnlySpan<byte> rows =
        [
            0xff,0xff,0xff,0x00, 0x08,0xff,0x00,0x00,   0xff,0xff,0xff,0x00, 0x09,0xff,0x00,0x00,
            0xff,0xff,0xff,0x00, 0x0a,0xff,0x00,0x00,   0xff,0xff,0xff,0x00, 0x0b,0xff,0x00,0x00,
            0xff,0xff,0xff,0x00, 0x0e,0xff,0x00,0x00,   0xff,0xff,0xff,0x00, 0x0f,0xff,0x00,0x00,
            0xff,0xff,0xff,0x00, 0x10,0xff,0x00,0x00,   0xff,0xff,0xff,0x00, 0x11,0xff,0x00,0x00,

            0xbf,0xbf,0xff,0x00, 0xff,0xff,0x13,0x00,   0xbf,0xbf,0xff,0x00, 0x00,0x00,0x00,0x00,
            0xbf,0xbf,0xff,0x00, 0x01,0x00,0x00,0x00,   0xbf,0xbf,0xff,0x00, 0x02,0x00,0x00,0x00,
            0xbf,0xbf,0xff,0x00, 0x03,0x00,0x00,0x00,   0xbf,0xbf,0xff,0x00, 0x04,0x00,0x00,0x00,
            0xbf,0xbf,0xff,0x00, 0x05,0x00,0x00,0x00,   0xbf,0xbf,0xff,0x00, 0x06,0x00,0x00,0x00,
            0xbf,0xbf,0xff,0x00, 0x07,0x00,0x00,0x00,   0xbf,0xbf,0xff,0x00, 0x0d,0x00,0x00,0x00,

            0xbf,0xff,0xbf,0x00, 0xff,0xff,0x14,0x00,   0xbf,0xff,0xbf,0x00, 0x00,0x01,0x00,0x00,
            0xbf,0xff,0xbf,0x00, 0x01,0x01,0x00,0x00,   0xbf,0xff,0xbf,0x00, 0x02,0x01,0x00,0x00,
            0xbf,0xff,0xbf,0x00, 0x03,0x01,0x00,0x00,   0xbf,0xff,0xbf,0x00, 0x04,0x01,0x00,0x00,
            0xbf,0xff,0xbf,0x00, 0x05,0x01,0x00,0x00,   0xbf,0xff,0xbf,0x00, 0x06,0x01,0x00,0x00,
            0xbf,0xff,0xbf,0x00, 0x07,0x01,0x00,0x00,   0xbf,0xff,0xbf,0x00, 0x0d,0x01,0x00,0x00,

            0xff,0xbf,0xbf,0x00, 0xff,0xff,0x15,0x00,   0xff,0xbf,0xbf,0x00, 0x00,0x02,0x00,0x00,
            0xff,0xbf,0xbf,0x00, 0x01,0x02,0x00,0x00,   0xff,0xbf,0xbf,0x00, 0x02,0x02,0x00,0x00,
            0xff,0xbf,0xbf,0x00, 0x03,0x02,0x00,0x00,   0xff,0xbf,0xbf,0x00, 0x04,0x02,0x00,0x00,
            0xff,0xbf,0xbf,0x00, 0x05,0x02,0x00,0x00,   0xff,0xbf,0xbf,0x00, 0x06,0x02,0x00,0x00,
            0xff,0xbf,0xbf,0x00, 0x07,0x02,0x00,0x00,   0xff,0xbf,0xbf,0x00, 0x0d,0x02,0x00,0x00,

            0xbf,0xff,0xff,0x00, 0xff,0xff,0x16,0x00,   0xbf,0xff,0xff,0x00, 0x00,0x03,0x00,0x00,
            0xbf,0xff,0xff,0x00, 0x01,0x03,0x00,0x00,   0xbf,0xff,0xff,0x00, 0x02,0x03,0x00,0x00,
            0xbf,0xff,0xff,0x00, 0x03,0x03,0x00,0x00,   0xbf,0xff,0xff,0x00, 0x04,0x03,0x00,0x00,
            0xbf,0xff,0xff,0x00, 0x05,0x03,0x00,0x00,   0xbf,0xff,0xff,0x00, 0x06,0x03,0x00,0x00,
            0xbf,0xff,0xff,0x00, 0x07,0x03,0x00,0x00,   0xbf,0xff,0xff,0x00, 0x0d,0x03,0x00,0x00,

            0xff,0xff,0xff,0x00, 0xfe,0xff,0x23,0x00,
        ];

        byte* table = (byte*)NativeMemory.AllocZeroed((nuint)rows.Length);
        for (int i = 0; i < rows.Length; i++) table[i] = rows[i];
        return table;
    }

    /// <summary>Journalisation des Ã©tapes de la mise en place : <c>OPENFORESIA_OPTIONS_SETUP=1</c>.</summary>
    private static readonly bool s_RunOptionsSetup =
        Environment.GetEnvironmentVariable("OPENFORESIA_OPTIONS_SETUP") == "1";

    private static readonly bool s_TraceGlyphs =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_GLYPHS") == "1";

    /// <summary>
    /// Ghidra: the menu's options branch drains input with `while (FUN_005048f0() != 0)` â€” it waits
    /// until nothing is held before opening the page, so the keypress that chose "Options" is not
    /// still down inside it. `FUN_005048f0` is <see cref="PollJoystickState"/>, reconstructed on
    /// 2026-08-14; this stub predates it and was never anything else.
    /// </summary>
    private static int InputDrainStub() => PollJoystickState();

    // Ghidra: &DAT_0056e670 â€” the menu's label table, recovered verbatim from Legacy/Blaze.exe.
    // A 0x0c length byte, then five cp1252 labels separated by carriage returns, ending 0x09 '?'.
    // They match the five switch cases in RunMainMenuFrameLoop one for one and in order:
    //   "DÃ©but du jeu"                -> game state 1
    //   "SÃ©lection des personnages"   -> game state 4
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

    // Ghidra: &DAT_0056dd58 â€” the menu context struct FUN_00544350 writes into. Its highest touched
    // offset is 0x8c7, and the next named global sits at 0x0056e670, so it spans 0x918 bytes.
    /// <summary>
    /// Ghidra: &amp;DAT_0056dd58 â€” the main menu's panel descriptor, **statically initialised in
    /// `.data`** and recovered verbatim from `Legacy/Blaze.exe`. Only 35 of its bytes are
    /// non-zero; the rest is the scratch the text engine and the renderer fill in.
    /// <para>
    /// Leaving it zeroed is what produced a black screen: every feature test in
    /// <see cref="DrawUiPanel"/> is <c>field &amp; 0x1000000</c>, so a zeroed descriptor draws
    /// nothing at all â€” no background, no frame, no text.
    /// </para>
    /// Allocated low because <see cref="RunTextEngine"/> masks it to 24 bits.
    /// </summary>
    // Lazy, not a field initializer: field initializers run inside the type constructor, so a
    // failure to secure low memory here would surface as TypeInitializationException â€” outside the
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
        //   +0x18  0x010F0808        background   â€” bit 0x1000000 set, so it draws
        //   +0x20  0x01070C10        text         â€” set
        //   +0x30  0x000F2874        cursor       â€” clear
        //   +0x38  0x000F0C00        scroll arrowsâ€” clear
        //   +0x40  0x010F0000        frame        â€” set
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
    /// Ghidra: FUN_00544350 @ 0x00544350 â€” binds the label table and its flags into the menu
    /// context. The call site is <c>FUN_00544350(&amp;DAT_0056e670, 0, &amp;DAT_0056dd58, 0)</c>.
    /// </summary>
    private static void BindMenuLabels(byte* labels, uint flags, byte* context, uint extra)
    {
        *(uint*)(context + 0x8b0) = flags;
        context[0x8c7] |= 1;
        *(byte**)(context + 0x8ac) = labels;
        *(uint*)(context + 0x8a8) = extra;

        // Ghidra: if (*param_1 == 8) FUN_00535300(0). The label table's first short is 0x440c
        // (recovered from the binary), so this never fires at this call site â€” but the branch is
        // kept because other callers may pass a table that does.
        if (*(short*)labels == 8)
        {
            throw new NotImplementedException(
                "FUN_00535300: reached only when the bound table's first short is 8 â€” " +
                "not the case for the main menu's label table");
        }
    }

    /// <summary>
    /// Ghidra: FUN_00547140 @ 0x00547140 â€” the CRT `rand()`. The constants are MSVC's linear
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
    /// Ghidra: FUN_00545140 @ 0x00545140 â€” flushes the deferred VRAM upload queue: walks the
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

    // Ghidra: the four 16-byte controllerâ†’player remap rows at 0x0056acf8, reached through the
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
    /// Ghidra: the index computation shared by FUN_00542250 and FUN_005357a0 â€” one bit per joystick,
    /// set when its axis reads the <c>-0x8000</c> sentinel.
    /// </summary>
    private static int ControllerRemapIndex()
    {
        int index = *(short*)s_JoyAxis1_00604b0a == -0x8000 ? 1 : 0;
        if (*(short*)s_JoyAxis2_00604b2e == -0x8000) index += 2;
        return index;
    }

    /// <summary>
    /// Ghidra: FUN_00512520 @ 0x00512520 â€” the debug hotkey handler, polled once per frame:
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
            // The clamp table here is DAT_00562e00 â€” recovered as { 1, 7, 11, 16, 23, 0 }. F12
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
    /// Ghidra: FUN_00504a30 @ 0x00504a30 â€” button edge detection and auto-repeat over two blocks of
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
    /// Ghidra: FUN_005357a0 @ 0x005357a0 â€” assigns controllers to the four player slots through the
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
    /// Ghidra: FUN_005048f0 @ 0x005048f0 â€” polls the DirectInput joystick device and folds its
    /// state into the per-slot tables.
    /// <para>
    /// Three parts, all reconstructed: `FUN_005231f0`/`FUN_005230b0` read every acquired device,
    /// `FUN_00523660` folds the keyboard into the four players' pad states through the
    /// `[Controls]` binding table, and the loop below distributes those states across the slot
    /// table the rest of the game reads. Verified at runtime: the menu cursor moves one entry per
    /// arrow press and the confirm key leaves the menu.
    /// </para>
    /// </summary>
    private static int PollJoystickState()
    {
        if (s_JoystickDevice_00604b54 == null) return 0;

        // Ghidra: FUN_005231f0(DAT_00604b54) â€” a one-line forwarder to FUN_005230b0.
        PollInputDevices(s_JoystickDevice_00604b54);

        // Ghidra: FUN_00523660(DAT_00604b50, &DAT_00604ac0)
        int anyInput = FoldKeyboardIntoPadState(s_KeyboardState_00604b50,
                                                s_InputStateBase_00604a20 + 0xa0);

        // Ghidra: the two-block folding loop that closes FUN_005048f0. Its cursor starts at
        // `&DAT_00604b0c` and advances by 9 dwords (0x24 bytes); every access below is written as
        // the byte offset that cursor computes, so the pointer arithmetic stays checkable against
        // the pseudocode instead of being re-expressed.
        byte* state = s_InputStateBase_00604a20;
        int cursor = 0xec;
        for (int block = 0; block < 2; block++, cursor += 0x24)
        {
            uint packed = *(uint*)(state + cursor - 0x4c);       // &DAT_00604ac0 for the first block
            uint live = (packed & 0x7777) << 1;

            if ((byte)(packed >> 0x10) == 0)
            {
                if ((packed & 0x80000000) == 0)                  // (packed >> 0x10 & 0x8000)
                {
                    live |= 1;
                    *(uint*)(state + cursor) =
                        *(ushort*)(state + cursor - 0x4a) | (*(uint*)(state + cursor - 0x48) << 0x10);

                    if ((packed & 0x30000000) != 0)              // (packed >> 0x10 & 0x3000)
                    {
                        *(uint*)(state + cursor + 4) =
                            (*(uint*)(state + cursor - 0x44) << 0x10)
                            | (*(uint*)(state + cursor - 0x48) >> 0x10);
                    }
                }
                else
                {
                    uint bit = 1;
                    int source = cursor - 0x48;
                    for (int i = 0; i < 4; i++, source += 8, bit <<= 4)
                    {
                        uint entry = *(uint*)(state + source);
                        if ((byte)entry != 0) continue;

                        live |= bit & 0xffff;
                        *(uint*)(state + source + 0x48) = entry;
                        if ((entry & 0x3000) != 0)
                            *(uint*)(state + source + 0x4c) = *(uint*)(state + source + 4);
                    }
                }
            }

            // Ghidra: any of the four sub-slots the folding above did not mark live is stamped
            // with the "nothing pressed" pattern â€” 0xffff41ff / 0xffffffff, the same active-low
            // convention the button masks use.
            uint slotMask = 0xf;
            int slot = cursor;
            for (int i = 0; i < 4; i++, slot += 8, slotMask <<= 4)
            {
                if ((slotMask & 0xffff & live) != 0) continue;
                *(uint*)(state + slot) = 0xffff41ff;
                *(uint*)(state + slot + 4) = 0xffffffff;
            }

            *(short*)(state + cursor - 0x4c) = (short)live;
            if ((short)live != 0)
                *(ushort*)(state + cursor - 2) = *(ushort*)(state + cursor - 0x4a);
        }

        return anyInput;
    }

    /// <summary>
    /// Ghidra: FUN_005230b0 @ 0x005230b0 â€” reads the current state of every acquired device into
    /// the object's three state areas.
    /// </summary>
    private static void PollInputDevices(byte* device)
    {
        if (device == null) return;

        // Ghidra: FUN_00523410(device + 0x50) â€” a thiscall whose `this` is the keyboard cell;
        // Ghidra shows only the stack argument. 0x100 bytes of keyboard state, cleared as 0x40
        // dwords when the device is gone.
        if (*(void**)(device + 8) != null)
            ReadDeviceState(*(void***)(device + 8), device + 0x50, 0x100, 0x40);

        // Ghidra: FUN_005234c0(device + 0x8b4) â€” same shape for the mouse: 0x10 bytes, 4 dwords.
        if (*(void**)(device + 0x4c) != null)
            ReadDeviceState(*(void***)(device + 0x4c), device + 0x8b4, 0x10, 4);

        *(uint*)(device + 0x14c) = *(uint*)(device + 0x8c0);

        // Ghidra: FUN_00523340 / FUN_00522fe0 per attached joystick. Every cell is null while
        // FUN_00523230 is unreconstructed, so this cannot fire today; it is left loud rather than
        // silently skipped so the day a stick is created the gap names itself.
        for (int i = 0; i < 0x10; i++)
        {
            if (*(void**)(device + 0x0c + i * 4) != null)
                throw new NotImplementedException(
                    "FUN_005230b0: FUN_00523340/FUN_00522fe0 (lecture d'une manette) non reconstruites");
        }
    }

    /// <summary>
    /// Ghidra: FUN_00523410 @ 0x00523410 and FUN_005234c0 @ 0x005234c0 â€” the same function twice,
    /// differing only in the state size they request and the number of dwords they clear on
    /// failure. `IDirectInputDevice::GetDeviceState` is retried after re-acquiring, which is what
    /// makes input survive the window losing and regaining the foreground.
    /// </summary>
    private static void ReadDeviceState(void** cell, byte* destination, uint size, int zeroDwords)
    {
        void* device = cell != null ? *cell : null;
        if (device == null)
        {
            NativeMemory.Fill(destination, (nuint)(zeroDwords * sizeof(uint)), 0);
            return;
        }

        void** vtable = *(void***)device;
        var getDeviceState = (delegate* unmanaged[Stdcall]<void*, uint, void*, int>)
            vtable[IDirectInputDevice_GetDeviceState];
        var acquire = (delegate* unmanaged[Stdcall]<void*, int>)vtable[IDirectInputDevice_Acquire];

        while (getDeviceState(device, size, destination) < 0)
        {
            if (acquire(device) < 0)
            {
                NativeMemory.Fill(destination, (nuint)(zeroDwords * sizeof(uint)), 0);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00523660 @ 0x00523660 â€” folds the raw keyboard (and joystick) state into the
    /// four players' pad states, through the `[Controls]` binding table.
    /// <para>
    /// The convention is **active low**: a player's button word starts at 0xffff and every bound
    /// key that is down clears its bit. The bit each binding clears comes from the 20-entry mask
    /// table at 0x00564f18, recovered from `Legacy/Blaze.exe` and unmistakably a PlayStation pad
    /// layout â€” 0x0010 up, 0x0020 right, 0x0040 down, 0x0080 left, 0x0008 start, 0x0001 select,
    /// the four face buttons at 0x1000..0x8000, the shoulders at 0x0100..0x0800, and four
    /// two-button combinations closing the table.
    /// </para>
    /// <para>
    /// Returns non-zero if anything at all was pressed â€” the value FUN_005048f0 passes on.
    /// </para>
    /// </summary>
    private static int FoldKeyboardIntoPadState(byte* deviceState, byte* padState)
    {
        if (deviceState == null || padState == null) return 0;

        int anyInput = 0;

        // Ghidra: if (DAT_00564a50 == 0) the raw state is zeroed before use â€” a keyboard-enable
        // gate. Recovered from Legacy/Blaze.exe as 1, so the state is kept.
        if (s_KeyboardEnabled_00564a50 == 0)
            NativeMemory.Fill(deviceState, 0x3f * sizeof(uint), 0);

        // Ghidra: state[0x1c] |= state[0x9c]; state[0x9c] = 0. Those two DirectInput scancodes are
        // DIK_RETURN and DIK_NUMPADENTER: the keypad's Enter is folded into the main one.
        deviceState[0x1c] |= deviceState[0x9c];
        deviceState[0x9c] = 0;

        // Ghidra: nine pairs of ushorts cleared from +0x24, then three fixed bytes.
        NativeMemory.Fill(padState + 0x24, 9 * 4, 0);
        padState[0x27] = 0x41;
        padState[0x02] = 0;
        padState[0x03] = 0x80;

        for (int player = 0; player < 4; player++)
        {
            int bindings = player * 60;     // Ghidra: the 0x3c-int stride of DAT_00564b58
            int entry = player * 8;         // Ghidra: iVar6, the pad-state stride

            // Ghidra: scancode 0x3e is DIK_F4 â€” held down, it silences the keyboard entirely. The
            // second test lets only player 0 through while DAT_005de168 is set.
            if ((deviceState[0x3e] & 0x80) == 0 && (player == 0 || s_Unknown_005de168 == 0))
            {
                ushort buttons = 0xffff;
                padState[entry + 4] = 0;
                padState[entry + 5] = 0x41;

                // Ghidra: two passes of 20 over binding categories 0 and 1 â€” the primary and
                // alternate keyboard layouts. The binding cursor runs on across both.
                for (int category = 0; category < 2; category++)
                {
                    for (int action = 0; action < 20; action++)
                    {
                        int scancode = s_KeyBindings[bindings + category * 20 + action];
                        if (scancode == 0) continue;
                        if ((deviceState[scancode] & 0x80) == 0) continue;

                        anyInput = 1;
                        buttons &= (ushort)~s_PadButtonMasks_00564f18[action];
                    }
                }

                // Ghidra: the third pass reads category 2 â€” the joystick bindings â€” from the
                // joystick button area at state + 0x100, indexed by this player's assigned stick.
                ushort accumulated = buttons;
                for (int action = 0; action < 20; action++)
                {
                    int scancode = s_KeyBindings[bindings + 40 + action];
                    int stick = s_JoystickSlot[player];
                    if (scancode == -1 || stick == -1) continue;

                    // Transcribed in the original's order: the assignment happens whether or not
                    // the button test passes, which is why it is inside the guard and not after.
                    buttons = accumulated;
                    if ((deviceState[0x100 + stick * 0x26 + scancode] & 0x80) == 0) continue;

                    anyInput = 1;
                    accumulated &= (ushort)~s_PadButtonMasks_00564f18[action];
                    buttons = accumulated;
                }

                *(ushort*)(padState + entry + 6) = buttons;
            }
            else
            {
                ushort clear = (ushort)~(0xf << (player * 4));
                *(ushort*)padState &= clear;
                *(ushort*)(padState + 0x48) &= clear;
                padState[entry + 4] = 0xff;
                padState[entry + 5] = 0;
                *(ushort*)(padState + entry + 6) = 0;
            }
        }

        return anyInput;
    }

    /// <summary>
    /// Ghidra: &amp;DAT_00564f18 â€” 20 PlayStation pad button masks, one per binding slot, read
    /// verbatim out of <c>Legacy/Blaze.exe</c>. Slots 9 and 10 are genuinely zero; the last four
    /// are two-button combinations.
    /// </summary>
    private static readonly ushort[] s_PadButtonMasks_00564f18 =
    [
        0x0100, 0x0200, 0x0400, 0x0800,     // L2, R2, L1, R1
        0x1000, 0x2000, 0x4000, 0x8000,     // triangle, circle, cross, square
        0x0001, 0x0000, 0x0000, 0x0008,     // select, --, --, start
        0x0010, 0x0020, 0x0040, 0x0080,     // up, right, down, left
        0x0500, 0x0a00, 0x0c00, 0x0300,     // L2+L1, R2+R1, L1+R1, L2+R2
    ];

    /// <summary>
    /// Ghidra: DAT_00564a50 â€” read from Legacy/Blaze.exe as 1. **Not read-only**: the two
    /// one-line functions <c>FUN_00523520</c> and <c>FUN_00523530</c> exist for nothing else than
    /// to set it to 1 and 0, and the name-entry sub-state calls both. It gates
    /// <see cref="FoldKeyboardIntoPadState"/>, so clearing it silences the pad mapping while raw
    /// characters are being typed.
    /// </summary>
    private static int s_KeyboardEnabled_00564a50 = 1;

    /// <summary>Ghidra: FUN_00523520 @ 0x00523520 â€” <c>DAT_00564a50 = 1</c>, nothing else.</summary>
    private static void EnablePadKeyboard() => s_KeyboardEnabled_00564a50 = 1;

    /// <summary>Ghidra: FUN_00523530 @ 0x00523530 â€” <c>DAT_00564a50 = 0</c>, nothing else.</summary>
    private static void DisablePadKeyboard() => s_KeyboardEnabled_00564a50 = 0;

    // Ghidra: DAT_005de168 â€” gates players 2..4 in FUN_00523660. Never assigned anywhere in the
    // reconstructed graph yet, so it stays 0 and all four players are polled, as the original does
    // on a fresh start.
    private static int s_Unknown_005de168;
    private static byte* s_Unknown_00579270;    // Ghidra: DAT_00579270
    private static int s_Unknown_005de554;      // Ghidra: DAT_005de554
    // Ghidra: DAT_00566cc4 â€” **the same global as s_ResolutionIndex**, not a second one. Blaze.c
    // line 58267 fills it from the ini's Resolution key, exactly as LoadOptionsIni does here, and
    // the reconstruction had split one global into two fields with only one of them ever assigned.
    // Third instance of this shape today, after DAT_0056e61f and DAT_005e8fc0.
    private static int s_ResolutionIndexForClamp => (int)s_ResolutionIndex;

    // Ghidra: _DAT_0054d000 â€” **lu dans Legacy/Blaze.exe : 0x3C800000, soit 1/64**, et non 1.0f
    // comme un TODO le supposait ici. C'est aussi le mÃªme symbole que `GammaIniScale` dÃ©clarÃ© plus
    // haut dans ce fichier, qui portait dÃ©jÃ  la bonne valeur par dÃ©duction ; huitiÃ¨me global
    // reconstruit sous deux noms.
    //
    // La valeur compte : la page d'options passe ses palettes dans `Gamma(valeur, rÃ©glage * ce
    // facteur)`. Ã€ 1.0f le produit valait 64 et rendait toutes les palettes blanches â€” fond et
    // cadre invisibles. Ã€ 1/64 le rÃ©glage par dÃ©faut de l'ini (64) donne 1.0, le gamma neutre.
    private const float s_GammaScale_0054d000 = 1.0f / 64.0f;

    /// <summary>Ghidra: the four calls guarded by arg5 in FUN_0052a280.</summary>
    private static void PollInputAndAudio()
    {
        // Ghidra: FUN_00512520(&DAT_00604a20 +
        //   (byte)(&DAT_005e9188)[(byte)(&DAT_005e93c8)[(_DAT_005de538 & 0xff) * 0x9c] * 0x4c] * 4)
        // The outer index had been hardcoded to 0 instead of reading _DAT_005de538. Equivalent
        // while that global is 0, but not faithful â€” and FUN_00545340 uses the same expression,
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
    /// Ghidra: FUN_00533000 @ 0x00533000 â€” per-frame **palette animation** (CLUT cycling). Walks a
    /// list of animation descriptors and, for each one whose counter has reached its period,
    /// rotates a run of colour entries by one and resets the counter.
    /// <para>
    /// The CLUTs it rotates are the ones <see cref="DecodeResourceChunk"/> mirrors into main video
    /// memory: both use the same address formula, <c>(bank + 0xe10) * 0x200 + MainMem</c>. That
    /// mirror exists precisely so this can animate them â€” the two functions are two halves of one
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
                // Ghidra: uVar7 = uVar7 + 0x1000000 â€” tick the counter in the top byte.
                descriptor += 0x1000000;
                uint updated = descriptor;

                // Ghidra: counter byte == period byte â€” this descriptor steps this frame.
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

    // Ghidra: DAT_005de510 â€” current game-state id, shared between RunGameLoop and the tick below.
    private static int s_GameStateId;

    // Ghidra: PTR_FUN_0056c96c â€” the per-frame tick RunGameLoop calls indirectly. **Resolved**: the
    // pointer's initialised value was read out of Legacy/Blaze.exe as 0x00537CA0, and FUN_00537ca0
    // already has a decompiled body in Blaze.c (line 83364), so no Ghidra re-run was needed. See
    // Documentation/DataSymbols.md.
    //
    // Kept as a function pointer rather than a direct call: the original really does dispatch
    // through a statically initialised .data slot, and preserving that indirection keeps the shape
    // of the code honest. A managed function pointer is enough â€” there is no native caller.
    private static delegate*<void> s_MainLoopTickCallback = &RunGameStateTick;

    // winsys3d.dll: ReInitializeAllTextures is a named export (called by winsys3d.c itself at
    // winsys3d.c:4119). Zoom8BitTextures is an imported *data* symbol, same category as
    // TexFilterPossible/MainMem â€” resolved lazily via GetProcAddress, since P/Invoke has no
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
    /// Ghidra: FUN_00537ca0 @ 0x00537ca0 â€” the per-frame tick, and the game's real **per-state
    /// dispatcher**. Note this displaces a long-standing assumption in REVERSE_STATUS.md that
    /// FUN_0052a440 played that role; the tick pointer reaches this function instead.
    /// <para>
    /// Every one of the eleven state handlers below **has a decompiled body in Blaze.c** â€” they are
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
                // Ghidra: FUN_005066d0() â€” le chargement est reconstruit, la boucle de frame
                // s'arrête sur ses huit sous-états non décompilés. Voir REVERSE_STATUS.md.
                RunCharacterSelectState();
                break;
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
                // Ghidra: `uRam00000000 = 1;` â€” a store to address 0. Not a translation artifact:
                // the original deliberately faults on an unknown state, i.e. this is its assert.
                throw new InvalidOperationException(
                    $"FUN_00537ca0: unknown game state {s_GameStateId}. The original stores to " +
                    "address 0 here, deliberately crashing â€” reproduced as an exception rather " +
                    "than by writing to a null pointer.");
        }
    }
    // Ghidra: DAT_005e92b0 â€” per-game-state value read from the loaded resource file's own table
    // at offset 0x48; read again by not-yet-reconstructed code (e.g. FUN_00542160).
    private static int s_Unknown_005e92b0;

    // =====================================================================
    // Not yet reconstructed â€” large subsystems reached directly from RunApplication.
    // These throw rather than silently no-op so the gap is loud instead of hidden.
    // See REVERSE_STATUS.md for the "next area to explore" notes on each.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00522e40 @ 0x00522e40 â€” destructor for the 0x8c4-byte joystick/DirectInput
    /// device struct allocated by FUN_00523120 (the 0x8c4 size matches that allocation exactly,
    /// and the final `for (0x231 dwords) *p++ = 0;` zero-fill covers the same 0x8c4 bytes).
    /// Frees 18 owned sub-objects (fields [2], [0x13], and [3..0x12]), releases the underlying
    /// COM device (vtable 0x08 = Release), then zeroes the struct.
    /// </summary>
    private static void DestroyJoystickDevice(void** device)
    {
        // Ghidra: each sub-object is destructed via FUN_005233a0 then freed via FUN_00547210.
        // TODO(REVERSE): FUN_005233a0 (the per-sub-object destructor) hasn't been read yet, so
        // only the Free half is reproduced â€” the sub-objects hold no COM references that the
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
    /// Ghidra: FUN_00504600 @ 0x00504600 -> FUN_00523200 @ 0x00523200 â€” post-loop teardown,
    /// stage 1: destroys and frees the joystick/DirectInput device created by FUN_00523120.
    /// </summary>
    private static void ShutdownStage1()
    {
        // Ghidra: FUN_00523200(DAT_00604b54) â€” `if (p != 0) { FUN_00522e40(p); FUN_00547210(p); }`
        if (s_JoystickDevice_00604b54 == null) return;
        DestroyJoystickDevice((void**)s_JoystickDevice_00604b54);
        Free(s_JoystickDevice_00604b54);
        s_JoystickDevice_00604b54 = null;
    }

    // Ghidra: DAT_00604b54 â€” the input-device object. Created by FUN_00523120, which
    // SetupJoystickHandlers now calls; ShutdownStage1 destroys it.
    //
    // **This global had been reconstructed under three different names** â€” `s_JoystickDevice`,
    // `s_JoystickDevice_00604b54` and, briefly, an `s_InputDevice_00604b54` added while
    // reconstructing the creation chain. One address, one field: the same shape of defect as
    // DAT_00566cc4 / s_ResolutionIndex, and the reason to check for an existing name before
    // declaring one.
    private static byte* s_JoystickDevice_00604b54;

    /// <summary>
    /// Ghidra: FUN_00512ff0 @ 0x00512ff0 â€” post-loop teardown, stage 3: the DirectSound shutdown.
    /// Releases the primary/listener object, then every sound buffer (stopping any that are still
    /// playing), then the IDirectSound device itself.
    /// </summary>
    private static void ShutdownStage3()
    {
        if (s_DirectSound == null) return;

        if (s_DirectSoundPrimary != null)
        {
            InvokeComMethod(s_DirectSoundPrimary, IDirectSoundBuffer_Release);
            // Ghidra does not null DAT_005db754 here â€” preserved as-is.
        }

        ReleaseAllSoundBuffers(stopPlayingBuffers: true);

        InvokeComMethod(s_DirectSound, IDirectSound_Release);
        s_DirectSound = null;
    }

    /// <summary>Ghidra: FUN_00542f50 @ 0x00542f50 (-> FUN_005428d0) â€” post-loop teardown, stage 4; same function RunMainLoop uses mid-sequence, see CloseMovieStreamIfOpen.</summary>
    private static void ShutdownStage4() => CloseMovieStreamIfOpen();

    /// <summary>Ghidra: FUN_00542f90 @ 0x00542f90 â€” post-loop teardown, stage 5; same function RunMainLoop uses mid-sequence, see ShutdownMoviePlayer.</summary>
    private static void ShutdownStage5() => ShutdownMoviePlayer();
}
