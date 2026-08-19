// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

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
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)] internal static extern IntPtr GetModuleHandleA(byte* lpModuleName);
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
    // La sous-couche carte mémoire (winsys3d.c FUN_00a041d0/04600/04650/04660/04680/046b0/050d0/
    // 050f0/02e20) : de vrais CreateFileA/CloseHandle/SetFilePointer/ReadFile/WriteFile sur un
    // dossier "save\" — pas d'émulation de carte mémoire PS1, malgré le nom. Chaque arité relue
    // directement dans winsys3d.dll (RET n) plutôt que déduite de Ghidra : les ordinaux 67/68/69/72
    // partagent tous un corps qui ignore son argument mais le retire quand même de la pile
    // (RET 4), alors que Ghidra les avait décompilés en (void) — la même classe de piège que
    // documente déjà test-protocol.md, attrapée ici avant d'écrire le P/Invoke plutôt qu'après un
    // plantage natif.
    [DllImport("winsys3d.dll", EntryPoint = "#9")] private static extern int Ordinal_9(int* transferState); // real: FUN_00a041d0, ResetEvent + un contrôle d'état sur *param_1
    [DllImport("winsys3d.dll", EntryPoint = "#18")] private static extern byte* Ordinal_18(void* pathDescriptor, byte* dest); // real: FUN_00a044c0, FindFirstFileA + copie du nom tronqué et de la taille dans *dest ; null si aucune entrée
    // real: FUN_00a04560, FindNextFileA + même copie. Ghidra le décompile `void`, mais FUN_005126c0
    // (Legacy/Raw/Blaze/FUN_005126c0.asm:36, `TEST EAX,EAX` juste après le CALL) teste bien EAX au
    // retour : le premier `return;` de la fonction laisse BVar2(=le FindNextFileA FALSE) dans EAX par
    // convention de code-gen, donc EAX vaut 0 exactement quand il n'y a plus d'entrée — déclaré `int`
    // ici pour pouvoir lire cette valeur, même si le vrai prototype ne la nomme pas.
    [DllImport("winsys3d.dll", EntryPoint = "#19")] private static extern int Ordinal_19(byte* dest);
    [DllImport("winsys3d.dll", EntryPoint = "#20")] private static extern int Ordinal_20(void* pathDescriptor); // real: FUN_00a045e0, DeleteFileA — **polarité inversée** : rend 1 si la suppression a ÉCHOUÉ, 0 si elle a réussi (`NEG EAX; SBB EAX,EAX; INC EAX` en fin de corps)
    [DllImport("winsys3d.dll", EntryPoint = "#21")] private static extern IntPtr Ordinal_21(void* pathDescriptor, uint flags); // real: FUN_00a04600, CreateFileA
    [DllImport("winsys3d.dll", EntryPoint = "#22")] private static extern int Ordinal_22(IntPtr handle); // real: FUN_00a04650, CloseHandle
    [DllImport("winsys3d.dll", EntryPoint = "#23")] private static extern int Ordinal_23(IntPtr handle, int offset, uint method); // real: FUN_00a04660, SetFilePointer
    [DllImport("winsys3d.dll", EntryPoint = "#24")] private static extern uint Ordinal_24(IntPtr handle, void* buffer, uint size); // real: FUN_00a04680, ReadFile (0xffffffff sur échec)
    [DllImport("winsys3d.dll", EntryPoint = "#25")] private static extern uint Ordinal_25(IntPtr handle, void* buffer, uint size); // real: FUN_00a046b0, WriteFile (0xffffffff sur échec)
    [DllImport("winsys3d.dll", EntryPoint = "#67")] private static extern int Ordinal_67(int unused); // real: FUN_00a050f0, RET 4 confirmé — un paramètre ignoré ; rend toujours 1
    [DllImport("winsys3d.dll", EntryPoint = "#68")] private static extern int Ordinal_68(int unused); // real: FUN_00a050d0, même famille qu'Ordinal_67/69
    [DllImport("winsys3d.dll", EntryPoint = "#69")] private static extern int Ordinal_69(int unused); // real: FUN_00a050f0 — même corps qu'Ordinal_67, deux ordinaux pour la même adresse
    [DllImport("winsys3d.dll", EntryPoint = "#72")] private static extern uint Ordinal_72(int unused); // real: FUN_00a02e20, le talon partagé par ~50 ordinaux ; rend toujours 0
    [DllImport("winsys3d.dll", EntryPoint = "#684")] private static extern uint Ordinal_684(short* rect, void* destination); // real: FUN_00a05f50, copie rectangulaire VRAM→mémoire (icône de sauvegarde) ; rect = {x,y,w,h}

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
}
