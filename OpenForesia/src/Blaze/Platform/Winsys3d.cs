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
    [DllImport("winsys3d.dll", EntryPoint = "#10")] private static extern int Ordinal_10(void* handle); // real: winsys3d.c FUN_00a04220 — **arme** le gestionnaire (état 0x1000 → 0x2000), ce n'est pas un désenregistrement (celui-là est l'ordinal 7, FUN_00a04130)
    [DllImport("winsys3d.dll", EntryPoint = "#40")] private static extern void Ordinal_40(); // real: winsys3d.c FUN_00a04fd0 — EnterCriticalSection(&DAT_00b6d6d8), la section que l'ordinal 41 relâche. Corps lu le 2026-08-18, RET sans argument
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

    [DllImport("kernel32.dll")] internal static extern void* VirtualAlloc(void* lpAddress, nuint dwSize, uint flAllocationType, uint flProtect);
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
    private static byte* s_GteVec799, s_GteVec800;
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

        // **799 et 800 identifiés le 2026-08-18**, par leur usage dans FUN_005330d0 : la fonction
        // y écrit deux vecteurs 3D de 32 bits (les deux aretes d'un triangle, sommet - sommet),
        // puis dépose leur produit vectoriel dans 801 (MAC1..3) champ par champ. Ce sont donc les
        // **deux vecteurs d'entrée du produit vectoriel**, pas des registres de projection. La
        // table du TODO ci-dessus les donnait comme non établis.
        s_GteVec799 = (byte*)GetProcAddress(module, (byte*)799);
        s_GteVec800 = (byte*)GetProcAddress(module, (byte*)800);

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
}
