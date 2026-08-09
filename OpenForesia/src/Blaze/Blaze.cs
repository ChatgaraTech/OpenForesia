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
    // Ghidra recovered its name directly from the import table.
    [DllImport("wsyskern.dll", CharSet = CharSet.Ansi)]
    private static extern IntPtr LoadBitmapEx(IntPtr hInstance, int resourceId, out IntPtr phPalette);

    // winsys3d.dll: named export, confirmed at ordinal 823 in the DLL's export table.
    [DllImport("winsys3d.dll")]
    private static extern int Setup3DDevice(IntPtr hBitmap, IntPtr hPalette, void* titlePtr);

    // The following winsys3d.dll entry points are exported by ordinal only (no name in the
    // DLL's export table), so Ghidra could not recover a name for them either. Blaze.exe's own
    // import table isn't available to us (only Blaze.c), so the source DLL below is inferred
    // from winsys3d.dll being the only DLL with unnamed ordinals in this call site's range —
    // signatures are inferred purely from call-site usage in RunApplication.
    // TODO(REVERSE): confirm source DLL/signatures once Blaze.exe's import table can be inspected directly.
    [DllImport("winsys3d.dll", EntryPoint = "#331")] private static extern IntPtr Ordinal_331(byte* path, int param2, int param3);
    [DllImport("winsys3d.dll", EntryPoint = "#336")] private static extern void Ordinal_336(IntPtr handle);
    [DllImport("winsys3d.dll", EntryPoint = "#337")] private static extern void Ordinal_337();
    [DllImport("winsys3d.dll", EntryPoint = "#450")] private static extern int Ordinal_450(int index);
    [DllImport("winsys3d.dll", EntryPoint = "#451")] private static extern uint Ordinal_451(int index);
    [DllImport("winsys3d.dll", EntryPoint = "#790")] private static extern void Ordinal_790(void* callback);

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
                IntPtr handle = Ordinal_331(driveRoot, 0, 0);
                if ((int)handle < 0)
                {
                    foundLetter = '@';
                }
                else
                {
                    Ordinal_336(handle);
                    foundLetter = 'P';
                }
            }

            if (foundLetter != '@')
            {
                cdCheckPath[0] = (byte)foundLetter;
                IntPtr hFile = Ordinal_331(cdCheckPath, 0, 0);
                uint fileSize = GetFileSize(hFile, null);

                IntPtr hBitmap = LoadBitmapEx(hInstance, 0x66, out IntPtr hPalette);
                Setup3DDevice(hBitmap, hPalette, driveRoot /* TODO(REVERSE): PTR_s_Blaze___Blade_00563208 title string pointer, not recovered */);
                DeleteObject(hBitmap);
                DeleteObject(hPalette);

                CoInitialize(null);
                SaveAndMaxWaveOutVolume();
                Ordinal_337();

                // Ghidra: builds a 0x1000-entry lookup table combining Ordinal_451's low 16
                // bits with Ordinal_450's value shifted into the high word.
                // TODO(REVERSE): purpose of this table (DAT_00600920) not yet identified.
                for (int i = 0; i < 0x1000; i++)
                {
                    uint low = Ordinal_451(i) & 0xffff;
                    int high = Ordinal_450(i);
                    s_LookupTable00600920[i] = low + (uint)(high << 16);
                }

                s_Buffer00604920 = Malloc(790000);
                s_Buffer00600900 = Malloc(260000);

                // TODO(REVERSE): Ordinal_790 receives a function pointer (Ghidra: &LAB_00512ef0);
                // likely registers a callback (idle/frame/crash handler) with the 3D engine.
                Ordinal_790(null);

                MainEngineInit(); // Ghidra: FUN_0050f140 @ 0x0050f140

                try
                {
                    RunMainLoop(); // Ghidra: FUN_00529c10 @ 0x00529c10
                }
                finally
                {
                    ShutdownStage1(); // Ghidra: FUN_00504600 @ 0x00504600
                    ShutdownStage2(); // Ghidra: thunk_FUN_0050f300 @ 0x0050f520
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

    // Ghidra: DAT_00600920, a 0x1000-entry (0x4000-byte) table populated in RunApplication.
    private static readonly uint[] s_LookupTable00600920 = new uint[0x1000];

    // Ghidra: DAT_00604920 (790000-byte scratch buffer) / DAT_00600900 (260000-byte scratch buffer).
    private static void* s_Buffer00604920;
    private static void* s_Buffer00600900;

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
    // Not yet reconstructed — large subsystems reached directly from RunApplication.
    // These throw rather than silently no-op so the gap is loud instead of hidden.
    // See REVERSE_STATUS.md for the "next area to explore" notes on each.
    // =====================================================================

    /// <summary>Ghidra: FUN_0050f140 @ 0x0050f140 — engine/device init, called once before the main loop.</summary>
    private static void MainEngineInit()
        => throw new NotImplementedException("FUN_0050f140 (engine init) not yet reconstructed — see REVERSE_STATUS.md");

    /// <summary>Ghidra: FUN_00529c10 @ 0x00529c10 — appears to be the main application/game loop.</summary>
    private static void RunMainLoop()
        => throw new NotImplementedException("FUN_00529c10 (main loop) not yet reconstructed — see REVERSE_STATUS.md");

    /// <summary>Ghidra: FUN_00504600 @ 0x00504600 — post-loop teardown, stage 1.</summary>
    private static void ShutdownStage1()
        => throw new NotImplementedException("FUN_00504600 (shutdown stage 1) not yet reconstructed — see REVERSE_STATUS.md");

    /// <summary>Ghidra: thunk_FUN_0050f300 @ 0x0050f520 — post-loop teardown, stage 2.</summary>
    private static void ShutdownStage2()
        => throw new NotImplementedException("thunk_FUN_0050f300 (shutdown stage 2) not yet reconstructed — see REVERSE_STATUS.md");

    /// <summary>Ghidra: FUN_00512ff0 @ 0x00512ff0 — post-loop teardown, stage 3.</summary>
    private static void ShutdownStage3()
        => throw new NotImplementedException("FUN_00512ff0 (shutdown stage 3) not yet reconstructed — see REVERSE_STATUS.md");

    /// <summary>Ghidra: FUN_00542f50 @ 0x00542f50 (-> FUN_005428d0) — post-loop teardown, stage 4.</summary>
    private static void ShutdownStage4()
        => throw new NotImplementedException("FUN_00542f50 (shutdown stage 4) not yet reconstructed — see REVERSE_STATUS.md");

    /// <summary>Ghidra: FUN_00542f90 @ 0x00542f90 — post-loop teardown, stage 5.</summary>
    private static void ShutdownStage5()
        => throw new NotImplementedException("FUN_00542f90 (shutdown stage 5) not yet reconstructed — see REVERSE_STATUS.md");
}
