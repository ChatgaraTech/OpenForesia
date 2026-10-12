// =====================================================================
// La réserve basse — la place sous 0x01000000, prise AVANT que le runtime .NET ne démarre.
//
// Écart legacy ++ (LB-056). La liste d'affichage est chaînée sur 24 bits (winsys3d.c
// ClearOTagR/DrawOTag), donc son arène de 0x280000 octets et une dizaine de descripteurs
// d'interface doivent vivre dans les 16 premiers Mo. En 1998, winsys3d.dll était chargée à son
// adresse préférée et l'arène était une globale en 0x00B6DBD8 : la place était là par
// construction. Le port, lui, la cherchait au démarrage, en concurrence avec le runtime et
// l'ASLR, et relançait jusqu'à trois fois un processus neuf quand il la perdait ; les trois
// essais pouvaient échouer (vu par Vincent le 2026-09-28 : plus grand intervalle libre 0x130000).
//
// Désormais le jeu se lance en deux temps :
//   1. le processus lancé par le joueur (le LANCEUR) crée le vrai jeu SUSPENDU — à cet instant
//      il ne porte que l'exécutable et ntdll, la fenêtre basse est presque vide ;
//   2. il y RÉSERVE l'arène (à 0x00B60000 si possible, la disposition de 1998) et un bassin pour
//      les descripteurs, publie les deux adresses dans une section nommée d'après le PID du jeu,
//      le réveille, puis l'attend et rend son code de sortie ;
//   3. le jeu lit la section au démarrage et ENGAGE ces réservations au lieu de chercher
//      (Blaze.EnsureDisplayListArena, Blaze.AllocLowZeroed).
// Le lanceur met le jeu dans un job « tué à la fermeture » : tuer l'un tue l'autre.
//
// Si la place manque, le jeu suspendu est tué et un neuf est créé, jusqu'à cent fois ; au-delà, un
// message explique au joueur qu'il faut relancer. Si c'est le système qui refuse (création du
// processus, section), le lanceur joue lui-même, par l'ancien chemin (recherche + relance).
// `OPENFORESIA_SANS_RESERVE=1` désactive la réserve, pour comparer.
// =====================================================================

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

internal static unsafe class ReserveBasse
{
    /// <summary>Posée par le lanceur dans l'environnement du jeu : « tu es l'enfant ».</summary>
    private const string VariableEnfant = "OPENFORESIA_RESERVE_ENFANT";
    private const string VariableSans = "OPENFORESIA_SANS_RESERVE";

    // Doit rester égal aux constantes de Blaze (Platform/Winsys3d.cs) : c'est la même arène.
    private const uint Plafond = 0x01000000;
    private const uint Plancher = 0x00010000;
    private const uint Granule = 0x10000;
    private const uint BaseOrigine = 0x00B60000;
    private const uint TailleOrigine = (0xDBD8 + 0x280000 + 0xFFF) & ~0xFFFu;   // MainMemOffsetInArena + ArenaSize
    private const uint TailleArene = 0x280000;

    /// <summary>Le bassin des descripteurs bas : ~0x16000 octets aujourd'hui, onze sites.</summary>
    internal const uint TailleBassin = 0x40000;

    /// <summary>
    /// Processus suspendus essayés avant d'abandonner. Un essai coûte 2,7 ms (mesuré le
    /// 2026-10-12 : création suspendue, réserve, fin) : cent font 0,3 s au pire.
    /// </summary>
    private const int EssaisDeReserve = 100;

    /// <summary>
    /// Le message du joueur quand la place sous 16 Mo est introuvable, à la place d'une exception
    /// .NET qui tuerait le processus sans rien dire d'utile.
    /// </summary>
    internal static void SignalerManqueDePlace()
    {
        MessageBoxW(IntPtr.Zero,
            "OpenForesia n'a pas pu démarrer.\n\n" +
            "Comme le jeu d'origine de 1998, il a besoin d'un bloc de mémoire d'un seul tenant " +
            "(2,5 Mo) placé dans les 16 premiers Mo de son espace mémoire, et Windows ne lui en a " +
            "pas laissé cette fois-ci. La place change à chaque lancement.\n\n" +
            "Relancez simplement le jeu. Si le message revient à chaque fois, redémarrez Windows.",
            "OpenForesia", MB_ICONERROR);
    }

    /// <summary>Ce que le jeu lit dans la section : l'arène, sa forme, le bassin.</summary>
    [StructLayout(LayoutKind.Sequential)]
    private struct Remise
    {
        public uint Arene, TailleArene, Origine, Bassin, TailleBassin;
    }

    // ---- côté jeu ---------------------------------------------------------------------------

    internal static bool EstEnfant => Environment.GetEnvironmentVariable(VariableEnfant) == "1";

    /// <summary>
    /// Les réservations reçues du lanceur, lues une fois. <c>Arene == 0</c> : aucune — le jeu
    /// cherche lui-même, comme avant.
    /// </summary>
    internal static (uint Arene, uint TailleArene, bool Origine, uint Bassin, uint TailleBassin) Recue { get; private set; }

    /// <summary>Appelée par le jeu avant toute allocation basse.</summary>
    internal static void Lire()
    {
        if (!EstEnfant) return;
        IntPtr section = OpenFileMappingW(FILE_MAP_READ, false, Nom((uint)Environment.ProcessId));
        if (section == IntPtr.Zero)
        {
            Console.Error.WriteLine("[RESERVE] section introuvable — recherche ordinaire");
            return;
        }
        var vue = (Remise*)MapViewOfFile(section, FILE_MAP_READ, 0, 0, (nuint)sizeof(Remise));
        if (vue != null)
        {
            Recue = (vue->Arene, vue->TailleArene, vue->Origine != 0, vue->Bassin, vue->TailleBassin);
            UnmapViewOfFile(vue);
        }
        CloseHandle(section);
        Console.Error.WriteLine(
            $"[RESERVE] recue : arene 0x{Recue.Arene:X8} (0x{Recue.TailleArene:X}{(Recue.Origine ? ", disposition 1998" : "")}), " +
            $"bassin 0x{Recue.Bassin:X8} (0x{Recue.TailleBassin:X})");
    }

    // ---- côté lanceur -----------------------------------------------------------------------

    /// <summary>
    /// Vrai pour le processus lancé par le joueur : ni enfant, ni relance de l'ancien chemin, ni
    /// réserve désactivée — ni sous débogueur : le débogueur de Rider ou de Visual Studio
    /// s'attacherait au lanceur, et aucun point d'arrêt du jeu ne s'arrêterait plus.
    /// </summary>
    internal static bool EstLanceur =>
        !EstEnfant
        && !System.Diagnostics.Debugger.IsAttached
        && Environment.GetEnvironmentVariable(VariableSans) != "1"
        && Environment.GetEnvironmentVariable(global::Startup.RetryGuard) == null;

    /// <summary>
    /// Lance le jeu suspendu, lui réserve sa place basse, le réveille et l'attend. Rend son code de
    /// sortie, ou <c>null</c> si la réserve n'a pas pu être posée pour une raison système
    /// (CreateProcess, section) — l'appelant joue alors lui-même. Quand c'est la place qui manque
    /// en <see cref="EssaisDeReserve"/> essais, le joueur reçoit un message et le jeu s'arrête :
    /// l'ancien chemin perdrait à coup sûr, le runtime ayant déjà pris le bas de la fenêtre.
    /// </summary>
    internal static int? Lancer()
    {
        Environment.SetEnvironmentVariable(VariableEnfant, "1");   // hérité par le jeu
        var si = new STARTUPINFOW { cb = (uint)sizeof(STARTUPINFOW) };
        IntPtr sortie = GetStdHandle(STD_OUTPUT_HANDLE), erreur = GetStdHandle(STD_ERROR_HANDLE);
        if (sortie != IntPtr.Zero || erreur != IntPtr.Zero)
        {
            // Les journaux de playtest.ps1 : les redirections du lanceur passent au jeu.
            si.dwFlags = STARTF_USESTDHANDLES;
            si.hStdInput = GetStdHandle(STD_INPUT_HANDLE);
            si.hStdOutput = sortie;
            si.hStdError = erreur;
        }

        // Le jeu suspendu, et une réserve posée dedans. Un essai perdu se refait sur un processus
        // neuf : l'image de l'exe (8,5 Mo, runtime compris) tombe au même endroit pendant toute la
        // session Windows — en 0x00830000 le 2026-10-12, ce qui prend tout de 8 à 16 Mo —, mais la
        // pile et les premiers tas changent de place à chaque création. Mesuré ce jour-là : un
        // essai sur vingt sans place, et alors l'ancien chemin perdait à coup sûr, le runtime ayant
        // déjà pris le bas (« No free block of 0x280000 bytes … largest free region 0x1D0000 »,
        // depuis le lanceur, Vincent).
        PROCESS_INFORMATION pi = default;
        Remise remise = default;
        string ligne = Environment.CommandLine;
        for (int essai = 1; ; essai++)
        {
            fixed (char* l = ligne)
            {
                // CreateProcessW peut écrire dans la ligne de commande : une copie.
                char* copie = stackalloc char[ligne.Length + 1];
                new ReadOnlySpan<char>(l, ligne.Length).CopyTo(new Span<char>(copie, ligne.Length));
                copie[ligne.Length] = '\0';
                PROCESS_INFORMATION cree;
                if (!CreateProcessW(Environment.ProcessPath, copie, null, null, true, CREATE_SUSPENDED,
                                    null, Environment.CurrentDirectory, &si, &cree))
                {
                    Console.Error.WriteLine($"[RESERVE] CreateProcess a echoue ({Marshal.GetLastWin32Error()}) — le lanceur joue lui-meme");
                    Environment.SetEnvironmentVariable(VariableEnfant, null);
                    return null;
                }
                pi = cree;
            }

            remise = Reserver(pi.hProcess);
            if (remise.Arene != 0 && remise.Bassin != 0) break;

            TerminateProcess(pi.hProcess, 1);
            CloseHandle(pi.hThread);
            CloseHandle(pi.hProcess);
            if (essai == EssaisDeReserve)
            {
                Console.Error.WriteLine($"[RESERVE] aucune place sous 0x01000000 en {EssaisDeReserve} essais — abandon");
                SignalerManqueDePlace();
                return 1;
            }
            Console.Error.WriteLine($"[RESERVE] essai {essai} sans place sous 0x01000000 — processus neuf");
        }

        IntPtr section = IntPtr.Zero;
        try
        {
            section = CreateFileMappingW(INVALID_HANDLE_VALUE, null, PAGE_READWRITE, 0, (uint)sizeof(Remise), Nom(pi.dwProcessId));
            if (section == IntPtr.Zero) throw new InvalidOperationException($"CreateFileMapping ({Marshal.GetLastWin32Error()})");
            var vue = (Remise*)MapViewOfFile(section, FILE_MAP_WRITE, 0, 0, (nuint)sizeof(Remise));
            if (vue == null) throw new InvalidOperationException($"MapViewOfFile ({Marshal.GetLastWin32Error()})");
            *vue = remise;
            UnmapViewOfFile(vue);

            AttacherAuJob(pi.hProcess);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[RESERVE] {ex.Message} — le lanceur joue lui-meme");
            TerminateProcess(pi.hProcess, 1);
            CloseHandle(pi.hThread);
            CloseHandle(pi.hProcess);
            if (section != IntPtr.Zero) CloseHandle(section);
            Environment.SetEnvironmentVariable(VariableEnfant, null);
            return null;
        }

        // La section reste ouverte tant que le jeu tourne : il la lit à son démarrage.
        ResumeThread(pi.hThread);
        CloseHandle(pi.hThread);
        WaitForSingleObject(pi.hProcess, INFINITE);
        GetExitCodeProcess(pi.hProcess, out uint code);
        CloseHandle(pi.hProcess);
        CloseHandle(section);
        return (int)code;
    }

    /// <summary>Les deux réservations dans le jeu suspendu : l'arène d'abord, le bassin ensuite.</summary>
    private static Remise Reserver(IntPtr processus)
    {
        var r = new Remise();

        // La disposition de 1998 d'abord : MainMem tombe alors sur 0x00B6DBD8 exactement.
        if (VirtualAllocEx(processus, (void*)BaseOrigine, TailleOrigine, MEM_RESERVE, PAGE_READWRITE) != null)
        {
            r.Arene = BaseOrigine;
            r.TailleArene = TailleOrigine;
            r.Origine = 1;
        }
        else
        {
            r.Arene = ReserverHaut(processus, TailleArene);
            r.TailleArene = TailleArene;
        }
        if (r.Arene == 0) return r;

        r.Bassin = ReserverHaut(processus, TailleBassin);
        r.TailleBassin = TailleBassin;
        return r;
    }

    /// <summary>Le plus haut intervalle libre sous le plafond qui tienne <paramref name="taille"/> ; 0 sinon.</summary>
    private static uint ReserverHaut(IntPtr processus, uint taille)
    {
        uint meilleur = 0;
        uint maxBase = (Plafond - taille) & ~(Granule - 1);
        for (uint adresse = Plancher; adresse <= maxBase;)
        {
            if (VirtualQueryEx(processus, (void*)adresse, out Blaze.MEMORY_BASIC_INFORMATION info,
                               (nuint)sizeof(Blaze.MEMORY_BASIC_INFORMATION)) == 0) break;
            uint debut = (uint)(nint)info.BaseAddress, longueur = (uint)(nint)info.RegionSize;
            if (longueur == 0) break;
            if (info.State == MEM_FREE)
            {
                uint bas = (debut + Granule - 1) & ~(Granule - 1);
                ulong fin = (ulong)debut + longueur;
                if (fin >= (ulong)bas + taille)
                {
                    uint haut = (uint)((fin - taille) & ~(ulong)(Granule - 1));
                    if (haut > maxBase) haut = maxBase;
                    if (haut >= bas) meilleur = haut;
                }
            }
            adresse = debut + longueur;
        }

        for (uint candidat = meilleur; meilleur != 0 && candidat >= Plancher; candidat -= Granule)
            if (VirtualAllocEx(processus, (void*)candidat, taille, MEM_RESERVE, PAGE_READWRITE) != null) return candidat;
        return 0;
    }

    /// <summary>Un job « tué à la fermeture » : le lanceur tué (gestionnaire des tâches), le jeu part avec.</summary>
    private static void AttacherAuJob(IntPtr processus)
    {
        IntPtr job = CreateJobObjectW(null, null);
        if (job == IntPtr.Zero) return;                                  // sans job : on joue quand même
        var limites = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION();
        limites.BasicLimitInformation.LimitFlags = JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE;
        if (!SetInformationJobObject(job, JobObjectExtendedLimitInformation, &limites, (uint)sizeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION))
            || !AssignProcessToJobObject(job, processus))
        {
            CloseHandle(job);
        }
        // Le handle du job n'est jamais fermé : il se ferme avec le lanceur, et c'est ce qui tue le jeu.
    }

    private static string Nom(uint pid) => $"Local\\OpenForesia-ReserveBasse-{pid}";

    // ---- Win32 ------------------------------------------------------------------------------

    private const uint CREATE_SUSPENDED = 0x4, STARTF_USESTDHANDLES = 0x100, INFINITE = 0xFFFFFFFF;
    private const uint MEM_RESERVE = 0x2000, MEM_FREE = 0x10000, PAGE_READWRITE = 0x04;
    private const uint FILE_MAP_WRITE = 0x2, FILE_MAP_READ = 0x4;
    private const int STD_INPUT_HANDLE = -10, STD_OUTPUT_HANDLE = -11, STD_ERROR_HANDLE = -12;
    private const uint JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x2000;
    private const int JobObjectExtendedLimitInformation = 9;
    private static readonly IntPtr INVALID_HANDLE_VALUE = new(-1);

    [StructLayout(LayoutKind.Sequential)]
    private struct STARTUPINFOW
    {
        public uint cb;
        public char* lpReserved, lpDesktop, lpTitle;
        public uint dwX, dwY, dwXSize, dwYSize, dwXCountChars, dwYCountChars, dwFillAttribute, dwFlags;
        public ushort wShowWindow, cbReserved2;
        public byte* lpReserved2;
        public IntPtr hStdInput, hStdOutput, hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESS_INFORMATION
    {
        public IntPtr hProcess, hThread;
        public uint dwProcessId, dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct JOBOBJECT_BASIC_LIMIT_INFORMATION
    {
        public long PerProcessUserTimeLimit, PerJobUserTimeLimit;
        public uint LimitFlags;
        public nuint MinimumWorkingSetSize, MaximumWorkingSetSize;
        public uint ActiveProcessLimit;
        public nuint Affinity;
        public uint PriorityClass, SchedulingClass;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
    {
        public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
        public ulong ReadOperationCount, WriteOperationCount, OtherOperationCount,
                     ReadTransferCount, WriteTransferCount, OtherTransferCount;   // IO_COUNTERS
        public nuint ProcessMemoryLimit, JobMemoryLimit, PeakProcessMemoryUsed, PeakJobMemoryUsed;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool CreateProcessW(string? lpApplicationName, char* lpCommandLine, void* lpProcessAttributes,
        void* lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, void* lpEnvironment,
        string? lpCurrentDirectory, STARTUPINFOW* lpStartupInfo, PROCESS_INFORMATION* lpProcessInformation);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern void* VirtualAllocEx(IntPtr hProcess, void* lpAddress, nuint dwSize, uint flAllocationType, uint flProtect);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern nuint VirtualQueryEx(IntPtr hProcess, void* lpAddress, out Blaze.MEMORY_BASIC_INFORMATION lpBuffer, nuint dwLength);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern IntPtr CreateFileMappingW(IntPtr hFile, void* lpAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern IntPtr OpenFileMappingW(uint dwDesiredAccess, bool bInheritHandle, string lpName);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern void* MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, nuint dwNumberOfBytesToMap);
    [DllImport("kernel32.dll")] private static extern bool UnmapViewOfFile(void* lpBaseAddress);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr CreateJobObjectW(void* lpJobAttributes, string? lpName);
    [DllImport("kernel32.dll")] private static extern bool SetInformationJobObject(IntPtr hJob, int infoClass, void* lpInfo, uint cbInfoLength);
    [DllImport("kernel32.dll")] private static extern bool AssignProcessToJobObject(IntPtr hJob, IntPtr hProcess);
    [DllImport("kernel32.dll")] private static extern uint ResumeThread(IntPtr hThread);
    [DllImport("kernel32.dll")] private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
    [DllImport("kernel32.dll")] private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
    [DllImport("kernel32.dll")] private static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);
    [DllImport("kernel32.dll")] private static extern bool CloseHandle(IntPtr hObject);
    [DllImport("kernel32.dll")] private static extern IntPtr GetStdHandle(int nStdHandle);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);
    private const uint MB_ICONERROR = 0x10;
}
