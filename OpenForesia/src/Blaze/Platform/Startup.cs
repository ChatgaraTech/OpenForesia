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

        uint version = Win32.GetVersion();
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

        s_RawCommandLine = Win32.GetCommandLineA();
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
        Win32.GetStartupInfoA(&startupInfo);
        int nCmdShow = (startupInfo.dwFlags & STARTF_USESHOWWINDOW) == 0
            ? SW_SHOWDEFAULT
            : startupInfo.wShowWindow;

        IntPtr hInstance = Win32.GetModuleHandleA(null);
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

        // Ghidra: DAT_00563280.._0056328c, copied into local_2c/local_28/local_24/local_20 in the
        // original. Read directly from Legacy/Blaze.exe's .data (file offset 0x161880): "@:\cd.idx".
        // Confirmed against the real disc, too â€” its root has a `cd.idx` file whose size (671088640
        // bytes = 0x28000000 = 640 MiB) is exactly what Handler_00544810's TODO(REVERSE) predicted
        // s_CdCheckFileSize_0056321c needs to be for that handler's return-value multiply to reduce
        // to identity. Only the drive letter at index 0 is substituted per candidate, same as
        // driveRoot above.
        byte* cdCheckPath = stackalloc byte[16];
        cdCheckPath[1] = (byte)':'; cdCheckPath[2] = (byte)'\\';
        cdCheckPath[3] = (byte)'c'; cdCheckPath[4] = (byte)'d'; cdCheckPath[5] = (byte)'.';
        cdCheckPath[6] = (byte)'i'; cdCheckPath[7] = (byte)'d'; cdCheckPath[8] = (byte)'x';
        cdCheckPath[9] = 0;

        // Ghidra: `DAT_006008c8 = 1; DAT_00604930 = 2; DAT_00600904 = 3;` juste avant la boucle de
        // balayage (Blaze.c:61733). **Ce ne sont pas des pointeurs**, ce sont trois sentinelles de
        // destination : LoadResourceEntry compare son argument `dest` à ces trois valeurs, et une
        // égalité veut dire « installe une table de sauts native » au lieu de « écris les octets
        // du fichier de ressources ici ». Voir ApplyResourceDestinationOverride.
        s_OverrideDestA_006008c8 = (void*)1;
        s_OverrideDestB_00604930 = (void*)2;
        s_OverrideDestC_00600904 = (void*)3;

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
                if (Win32.GetDriveTypeA(driveRoot) == DRIVE_CDROM)
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
                    Win32.CloseHandle(handle);
                    foundLetter = 'P';
                }
            }

            if (foundLetter != '@')
            {
                // Ghidra: DAT_005631f8 = s____Blaze_Blade_005631e8[0]. A single-byte store, and
                // that is genuinely all it is: the rest of "@:\FRENCH\" is static data the linker
                // wrote into the image. Patch index 0 only â€” do not rebuild the string from parts,
                // or the language directory is lost again.
                Mem.s_CdDataRootPath[0] = (byte)foundLetter;

                cdCheckPath[0] = (byte)foundLetter;
                IntPtr hFile = OpenExistingFileReadOnly(cdCheckPath);
                uint fileSize = Win32.GetFileSize(hFile, null);
                Mem.s_CdCheckFileSize_0056321c = fileSize;   // Ghidra: DAT_0056321c = fileSize

                IntPtr hBitmap = Wsyskern.LoadBitmapEx(hInstance, (IntPtr)0x66 /* MAKEINTRESOURCE */, out IntPtr hPalette);
                Winsys3d.Setup3DDevice(hBitmap, hPalette, GetRendererDialogStrings());
                Win32.DeleteObject(hBitmap);
                Win32.DeleteObject(hPalette);

                Win32.CoInitialize(null);
                SaveAndMaxWaveOutVolume();
                Winsys3d.InitTrigTables();

                // Ghidra: builds a 0x1000-entry lookup table combining the sin table (ordinal 451)
                // low 16 bits with the cos table (ordinal 450, phase-shifted by a quarter period)
                // in the high word â€” see Documentation/DLL/winsys3d.md. Packed sin/cos table,
                // consumed by the not-yet-reconstructed rendering code.
                for (int i = 0; i < 0x1000; i++)
                {
                    uint low = Winsys3d.GetSinTableEntry(i) & 0xffff;
                    int high = Winsys3d.GetCosTableEntry(i);
                    s_LookupTable00600920[i] = low + (uint)(high << 16);
                }

                s_BaseSoundBank = Malloc(790000);
                s_LevelSoundBank = Malloc(260000);

                // TODO(REVERSE): the callback (Ghidra: &LAB_00512ef0) has no decompiled body in
                // Blaze.c (see Documentation/DLL/winsys3d.md for what SetDeviceLostCallback does
                // with it inside winsys3d.dll).
                Winsys3d.SetDeviceLostCallback(null);

                LoadOptionsIni(); // Ghidra: FUN_0050f140 @ 0x0050f140
                Mem.s_CdCheckFileSize_0056321c += 0xd8000000;   // Ghidra: DAT_0056321c = DAT_0056321c + 0xd8000000

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
                    Aud.ShutdownStage3(); // Ghidra: FUN_00512ff0 @ 0x00512ff0
                    // Ghidra: DestroyWindow(DAT_005db85c) â€” main window handle global not yet reconstructed.
                    // TODO(REVERSE): DAT_005db85c (main HWND) needs to be threaded through from window creation.
                    ShutdownStage4(); // Ghidra: FUN_00542f50 @ 0x00542f50
                    ShutdownStage5(); // Ghidra: FUN_00542f90 @ 0x00542f90
                    RestoreWaveOutVolume(); // Ghidra: FUN_005138d0 @ 0x005138d0
                    Win32.CoUninitialize();
                }

                return 0;
            }

            int choice = Win32.MessageBoxA(IntPtr.Zero, s_PleaseInsertCdMessage, s_ErrorCaption, 0x2001);
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
    internal static void* s_BaseSoundBank;   // 790000 bytes
    internal static void* s_LevelSoundBank;  // 260000 bytes

    // Ghidra: s_Please_insert_Blaze___Blade_CD__00563258 / s_Erreur_00563278 â€” symbol names were
    // derived by Ghidra from the actual string bytes, but those bytes aren't reproduced in
    // Blaze.c's text. TODO(REVERSE): confirm exact original text/encoding from the binary.
    private static readonly byte* s_PleaseInsertCdMessage = ToAnsiZ("Please insert the Blaze & Blade CD-ROM.");
    internal static readonly byte* s_ErrorCaption = ToAnsiZ("Erreur");

    // `internal` : appele depuis src/Host/ (DeviceShim, SyntheticDevice).
    internal static byte* ToAnsiZ(string s)
    {
        byte[] bytes = System.Text.Encoding.Latin1.GetBytes(s + "\0");
        byte* p = (byte*)NativeMemory.Alloc((nuint)bytes.Length);
        for (int i = 0; i < bytes.Length; i++) p[i] = bytes[i];
        return p;
    }

    /// <summary>Une table d'octets relevee dans <c>.data</c>, posee en memoire native pour que les
    /// sites qui la parcourent au pointeur puissent le faire comme dans l'original.</summary>
    private static byte* AllocBytes(byte[] bytes)
    {
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
        uint deviceCount = Win32.waveOutGetNumDevs();
        for (uint device = 0; device < deviceCount; device++)
        {
            uint savedVolume;
            Win32.waveOutGetVolume((IntPtr)device, &savedVolume);
            s_SavedWaveOutVolumes[device] = savedVolume;
            Win32.waveOutSetVolume((IntPtr)device, 0xffffffff);
        }
    }

    /// <summary>Ghidra: FUN_005138d0 @ 0x005138d0 â€” restores the volumes saved above.</summary>
    private static void RestoreWaveOutVolume()
    {
        uint deviceCount = Win32.waveOutGetNumDevs();
        for (uint device = 0; device < deviceCount; device++)
        {
            Win32.waveOutSetVolume((IntPtr)device, s_SavedWaveOutVolumes[device]);
        }
    }

    // Ghidra: DAT_006008a0, an array of saved per-device waveOut volumes.
    private static readonly uint[] s_SavedWaveOutVolumes = new uint[32];

    // =====================================================================
    // RunMainLoop and its remaining direct dependencies
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00504460 @ 0x00504460. Enregistre huit « gestionnaires » auprès de winsys3d.dll
    /// (Ordinal_6, <c>FUN_00a040d0</c>), les **active** (Ordinal_10, <c>FUN_00a04220</c>), puis met
    /// en place le périphérique joystick via FUN_00523120 avant de réinitialiser son état
    /// (FUN_00504b10). FUN_00523120 (création du périphérique DirectInput, liée au handle de la
    /// fenêtre principale) a sa propre chaîne de dépendances non lue (FUN_00522dc0/eb0/e40).
    ///
    /// <para><b>Deux corrections le 2026-08-18</b>, toutes deux causes directes du 0xC0000005 dans
    /// <c>Saves.ResetCardTransferStatus</c>/<c>Ordinal_67</c> quand un fichier de sauvegarde existait
    /// déjà :</para>
    /// <list type="number">
    /// <item><c>Ordinal_10</c> n'est <b>pas</b> un désenregistrement, contrairement à ce que le
    /// commentaire précédent affirmait : <c>FUN_00a04220</c> fait passer l'état du nœud de 0x1000 à
    /// 0x2000, c'est-à-dire qu'il l'<b>arme</b>. Les huit gestionnaires restent donc actifs pour
    /// toute la partie, et la DLL les appelle vraiment (<c>FUN_00a04270</c>, le répartiteur
    /// qu'Ordinal_67/68/69 déclenchent).</item>
    /// <item>Les huit cibles étaient passées à <c>null</c> faute de corps décompilés. La DLL
    /// appelait donc l'adresse 0. Leurs corps sont désormais relus dans le binaire et transcrits —
    /// voir <see cref="CardWriteStatusHandler0"/> et le commentaire qui les précède.</item>
    /// </list>
    /// </summary>
    private static void SetupJoystickHandlers()
    {
        Winsys3d.Ordinal_40();

        // Ghidra: DAT_00604b68/6c/70/74 puis DAT_00604b58/5c/60/64 — l'ordre compte, chaque
        // gestionnaire publie l'état correspondant à son mode (4, 0x8000, 0x100, 0x2000 → 0..3).
        void* h1 = Winsys3d.Ordinal_6(0xf4000001, 4, 0x1000, (void*)(delegate* unmanaged[Stdcall]<void>)&SaveData.CardWriteStatusHandler0);
        void* h2 = Winsys3d.Ordinal_6(0xf4000001, 0x8000, 0x1000, (void*)(delegate* unmanaged[Stdcall]<void>)&SaveData.CardWriteStatusHandler1);
        void* h3 = Winsys3d.Ordinal_6(0xf4000001, 0x100, 0x1000, (void*)(delegate* unmanaged[Stdcall]<void>)&SaveData.CardWriteStatusHandler2);
        void* h4 = Winsys3d.Ordinal_6(0xf4000001, 0x2000, 0x1000, (void*)(delegate* unmanaged[Stdcall]<void>)&SaveData.CardWriteStatusHandler3);
        void* h5 = Winsys3d.Ordinal_6(0xf0000011, 4, 0x1000, (void*)(delegate* unmanaged[Stdcall]<void>)&SaveData.CardReadStatusHandler0);
        void* h6 = Winsys3d.Ordinal_6(0xf0000011, 0x8000, 0x1000, (void*)(delegate* unmanaged[Stdcall]<void>)&SaveData.CardReadStatusHandler1);
        void* h7 = Winsys3d.Ordinal_6(0xf0000011, 0x100, 0x1000, (void*)(delegate* unmanaged[Stdcall]<void>)&SaveData.CardReadStatusHandler2);
        void* h8 = Winsys3d.Ordinal_6(0xf0000011, 0x2000, 0x1000, (void*)(delegate* unmanaged[Stdcall]<void>)&SaveData.CardReadStatusHandler3);

        Saves.s_CardWriteEventHandles_00604b68[0] = h1;
        Saves.s_CardWriteEventHandles_00604b68[1] = h2;
        Saves.s_CardWriteEventHandles_00604b68[2] = h3;
        Saves.s_CardWriteEventHandles_00604b68[3] = h4;
        Saves.s_CardReadEventHandles_00604b58[0] = h5;
        Saves.s_CardReadEventHandles_00604b58[1] = h6;
        Saves.s_CardReadEventHandles_00604b58[2] = h7;
        Saves.s_CardReadEventHandles_00604b58[3] = h8;

        Winsys3d.Ordinal_41();
        Winsys3d.Ordinal_10(h1); Winsys3d.Ordinal_10(h2); Winsys3d.Ordinal_10(h3); Winsys3d.Ordinal_10(h4);
        Winsys3d.Ordinal_10(h5); Winsys3d.Ordinal_10(h6); Winsys3d.Ordinal_10(h7); Winsys3d.Ordinal_10(h8);

        // Ghidra: DAT_00604b54 = FUN_00523120(0, DAT_005db85c, &DAT_00604b50, 0, 0)
        //
        // `DAT_005db85c` is the main window handle that TODO #6 searched for exhaustively and never
        // found. It does not block this call, for two reasons established by reading the chain:
        // the first argument is an `HINSTANCE` whose null case the constructor handles explicitly
        // (`GetModuleHandleA(NULL)`), and the handle itself is only ever used as the window of
        // `SetCooperativeLevel`. The reconstruction now owns a real window â€” the Direct3D 11
        // backend's â€” so there is a legitimate handle to give, which was not true when TODO #6 was
        // declared blocked.
        fixed (byte** keyboardState = &Inp.s_KeyboardState_00604b50)
            Mem.s_JoystickDevice_00604b54 = Inp.CreateInputDevice(IntPtr.Zero, D3D11Window.Handle,
                                                         keyboardState, null, null);

        Winsys3d.Ordinal_63(0);
        Winsys3d.Ordinal_64();
        Winsys3d.Ordinal_66();
        Inp.JoystickResetState(true);
    }

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
    internal static void DestroyJoystickDevice(void** device)
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

        if (device[0] != null) Audio.InvokeComMethod(device[0], Audio.IDirectSound_Release /* 0x08, generic COM Release */);

        for (int i = 0; i < 0x231; i++) device[i] = null;
    }

    /// <summary>
    /// Ghidra: FUN_00504600 @ 0x00504600 -> FUN_00523200 @ 0x00523200 â€” post-loop teardown,
    /// stage 1: destroys and frees the joystick/DirectInput device created by FUN_00523120.
    /// </summary>
    private static void ShutdownStage1()
    {
        // Ghidra: FUN_00523200(DAT_00604b54) â€” `if (p != 0) { FUN_00522e40(p); FUN_00547210(p); }`
        if (Mem.s_JoystickDevice_00604b54 == null) return;
        DestroyJoystickDevice((void**)Mem.s_JoystickDevice_00604b54);
        Free(Mem.s_JoystickDevice_00604b54);
        Mem.s_JoystickDevice_00604b54 = null;
    }

    // Deplace dans GameMemory le 2026-08-19 : globale .data lue par Engine/Input.cs.

    // Ghidra: FUN_00512ff0 @ 0x00512ff0 - l'etape 3 de l'extinction (l'arret de DirectSound) a
    // rejoint Engine/Audio.cs le 2026-08-21, a la conversion du son en vraie classe. Son adresse
    // tombe dans la plage audio et elle etait le seul ecrivain externe de s_DirectSound. Le site
    // d'appel de la sequence d'extinction est reste ici : voir `Aud.ShutdownStage3()` plus haut.

    /// <summary>Ghidra: FUN_00542f50 @ 0x00542f50 (-> FUN_005428d0) â€” post-loop teardown, stage 4; same function RunMainLoop uses mid-sequence, see Vid.CloseMovieStreamIfOpen.</summary>
    private static void ShutdownStage4() => Vid.CloseMovieStreamIfOpen();

    /// <summary>Ghidra: FUN_00542f90 @ 0x00542f90 â€” post-loop teardown, stage 5; same function RunMainLoop uses mid-sequence, see Vid.ShutdownMoviePlayer.</summary>
    private static void ShutdownStage5() => Vid.ShutdownMoviePlayer();

    // ---------------------------------------------------------------------------------------
    // Deplacees depuis Memory/Arena.GameState.cs le 2026-08-19 (extract-real-classes-from-partial).
    // Elles y avaient atterri par un point de coupe trop large : ce sont des COMPORTEMENTS, et
    // GameMemory ne doit contenir que le modele de .data. Leur place est ici, la sequence de
    // demarrage, aux cotes de RunApplication qui est leur appelant.
    //
    // A noter : la regle de routage (1) d'ARCHITECTURE.md aurait dit Engine/Resources.cs, parce
    // que FUN_00529c10 tombe dans la plage 0x00529. La regle a tort ici - la proximite d'adresse
    // est un artefact de l'editeur de liens sur le code de demarrage, pas une appartenance.
    // ---------------------------------------------------------------------------------------

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
        DeviceShim.CreateGameWindow();

        Vid.InitMoviePlayer();
        bool streamOpened = Vid.TryOpenMovieStream();
        if (streamOpened)
        {
            if (Vid.PlayMovie(Vid.s_MovieNameTeps, 1) == 0)
            {
                Vid.PlayMovie(Vid.s_MovieNameOpening, 3);
            }

            // Ghidra: Blaze.c:74441-74451. The attract loop has FOUR exit conditions, one per step â€”
            // title screen, ANI01, title screen, OPENING â€” and every one of them must stay, even
            // while a step is degraded. Dropping the two Vid.ShowTitleScreenAndWait checks (Ghidra types
            // that thunk `void`, but the call site reads its result) is what previously made this
            // loop unbreakable, since Vid.PlayMovie always reports 0 while TrySetupMovieStream is a stub.
            while (true)
            {
                if (Vid.ShowTitleScreenAndWait() != 0) break;
                if (Vid.PlayMovie(Vid.s_MovieNameAni01, 3) != 0) break;
                if (Vid.ShowTitleScreenAndWait() != 0) break;
                if (Vid.PlayMovie(Vid.s_MovieNameOpening, 3) != 0) break;
            }
        }
        Vid.CloseMovieStreamIfOpen();

        Winsys3d.Ordinal_129();
        Winsys3d.Ordinal_41();
        Winsys3d.Ordinal_45(null, Mem.s_CdDataRootPath); // Ghidra: apcStack_54[1]="j" â€” TODO(REVERSE): odd literal, but Ordinal_45 is a confirmed no-op regardless
        Winsys3d.Ordinal_136(0);
        Mem.s_RandSeed_0056e9c0 = 1; // Ghidra: FUN_00547130(1) â€” srand(1); see CrtRand
        Mem.s_Unknown_005e9630 = Winsys3d.Ordinal_116();
        Winsys3d.Ordinal_771();
        Winsys3d.InitTrigTables(); // Ordinal_337, re-run (idempotent â€” see winsys3d.md)
        // Ghidra: Ordinal_789(0, DAT_00566cc4) â€” Blaze.c line 79100. The second argument is **not**
        // inert, as an earlier note here claimed: winsys3d.c FUN_00a06420 opens with
        // `DAT_00a2c49c = param_2`, and that global indexes the base-offset tables SetShift uses to
        // position the viewport. Passing 0 selected the 320x240 entry (-160,-120), which is why the
        // scene rendered unscaled in a corner of a 640x480 screen.

        // `Ordinal_789` charge `3dhelp.dll` et lui fait crÃ©er son device, ce qui bascule le mode
        // d'affichage du bureau. Le device synthÃ©tique intercepte cette crÃ©ation. Voir
        // `SyntheticDevice.cs` ; opt-in tant qu'il n'est pas Ã©prouvÃ©.
        SyntheticDevice.InstallSyntheticDevice();

        Winsys3d.Ordinal_789(0, Inp.s_ResolutionIndexForClamp);
        DeviceShim.InstallDeviceShim();   // ECHAFAUDAGE TEMPORAIRE â€” voir DeviceShim.cs
        DeviceShim.LogDisplayMode("aprÃ¨s init 3D"); // ECHAFAUDAGE TEMPORAIRE
        Vid.ShutdownMoviePlayer(); // Ghidra: FUN_00542f90()
        CheckDeviceCapabilityFlag(); // Ghidra: FUN_0050f120()
        Winsys3d.Ordinal_110(0);
        Winsys3d.Ordinal_681(0);
        Vid.SetShadingDefaults(); // Ghidra: FUN_00529e40()
        Vid.InitViewportSurfaces(); // Ghidra: FUN_00529eb0()
        // Ghidra: FUN_0040a600() â€” confirmed no-op (`return;` with an empty body).
        SetupJoystickHandlers(); // Ghidra: FUN_00504460()

        int detail = Mem.s_SizeSetting_005de160;
        Winsys3d.SetShift((0xb - detail) * 0x10, (0xb - detail) * 0xc, detail, 0);

        LoadUiFontFromCd();

        Winsys3d.Ordinal_710(0x3c0, 0);
        int consoleChannel = Winsys3d.Ordinal_668(8, 0x20, 0xe0, 200, 0, 0x200);
        Winsys3d.Ordinal_720(consoleChannel);
        Winsys3d.Ordinal_92(null, null);
        Mem.s_Unknown_005e97f0 = Winsys3d.Ordinal_104(null);
        Mem.s_ScreenStride_005e97e0 = 0x1010;
        byte* mainMem = GetMainMemPtr();
        Mem.s_MainResourceBuffer_005e97c0 = mainMem != null ? mainMem + 0xe0000 : null;
        Mem.s_Unknown_005e9820 = 0;

        Winsys3d.Ordinal_132(0);
        Winsys3d.Ordinal_712(1);

        LoadFirstLevelResources(); // Ghidra: FUN_0052a030()
        RunGameLoop();            // Ghidra: FUN_0052a3d0()

        Winsys3d.Ordinal_138();
    }

    // Ghidra: DAT_0056e9c0 (set by FUN_00547130) / _DAT_005e9630 (set by the confirmed-no-op
    // Ordinal_116, always 0) / DAT_005e97f0 (set by the confirmed-no-op Ordinal_104, always 0) /
    // DAT_005e97e0 (screen stride/pitch?) / _DAT_005e9820 â€” all read again by not-yet-reconstructed
    // rendering code.

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
        for (byte* p = Mem.s_CdDataRootPath; *p != 0 && i < 60; p++, i++) path[i] = *p;
        for (byte* p = fontFileName; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;

        IntPtr hFile = OpenExistingFileReadOnly(path);
        if ((int)hFile < 0) return; // TODO(REVERSE): original doesn't appear to guard this open failing either

        Win32.SetFilePointer(hFile, 0x3e, null, 0 /* FILE_BEGIN */);
        byte* mainMem = GetMainMemPtr();
        if (mainMem != null)
        {
            uint bytesRead;
            Win32.ReadFile(hFile, mainMem + 0x1ad8f0, 0xd48, &bytesRead, null);
        }
        Win32.CloseHandle(hFile);
    }
}
