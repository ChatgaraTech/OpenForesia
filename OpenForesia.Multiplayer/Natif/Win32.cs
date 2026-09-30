// Les appels Win32 du multijoueur : fenetres, messages, premier plan, frappes simulees.
//
// Pas de WinForms (openspec/changes/add-multiplayer-streaming, D1) : comme l'installeur, des appels
// directs, en LibraryImport pour que l'elagage et une compilation native restent possibles.

using System.Runtime.InteropServices;

namespace OpenForesia.Multiplayer.Natif;

internal static unsafe partial class Win32
{
    // --- messages ---------------------------------------------------------------------------------
    internal const uint WM_DESTROY = 0x0002, WM_CLOSE = 0x0010, WM_KEYDOWN = 0x0100, WM_KEYUP = 0x0101,
                        WM_CHAR = 0x0102, WM_SYSKEYDOWN = 0x0104, WM_SYSKEYUP = 0x0105, WM_SYSCHAR = 0x0106;
    internal const uint PM_REMOVE = 1;

    // --- styles -----------------------------------------------------------------------------------
    internal const uint WS_OVERLAPPEDWINDOW = 0x00CF0000, WS_VISIBLE = 0x10000000;
    internal const int SW_SHOW = 5;

    // --- SendInput --------------------------------------------------------------------------------
    internal const uint INPUT_KEYBOARD = 1;
    internal const uint KEYEVENTF_EXTENDEDKEY = 0x0001, KEYEVENTF_KEYUP = 0x0002, KEYEVENTF_SCANCODE = 0x0008;

    [StructLayout(LayoutKind.Sequential)]
    internal struct WNDCLASSEXW
    {
        public uint cbSize, style;
        public delegate* unmanaged[Stdcall]<IntPtr, uint, IntPtr, IntPtr, IntPtr> lpfnWndProc;
        public int cbClsExtra, cbWndExtra;
        public IntPtr hInstance, hIcon, hCursor, hbrBackground;
        public char* lpszMenuName, lpszClassName;
        public IntPtr hIconSm;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam, lParam;
        public uint time;
        public int x, y;
    }

    /// <summary>
    /// INPUT d'un processus 32 bits : le genre (4 octets), puis l'union, dont le plus grand membre
    /// (MOUSEINPUT) fait 24 octets. KEYBDINPUT en occupe 16 ; la structure fait donc 28 octets.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 28)]
    internal struct INPUT
    {
        [FieldOffset(0)] public uint type;
        [FieldOffset(4)] public ushort wVk;
        [FieldOffset(6)] public ushort wScan;
        [FieldOffset(8)] public uint dwFlags;
        [FieldOffset(12)] public uint time;
        [FieldOffset(16)] public IntPtr dwExtraInfo;
    }

    [LibraryImport("user32.dll", EntryPoint = "RegisterClassExW")] internal static partial ushort RegisterClassEx(WNDCLASSEXW* classe);
    [LibraryImport("user32.dll", EntryPoint = "CreateWindowExW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr CreateWindowEx(uint exStyle, string classe, string? texte, uint style,
        int x, int y, int largeur, int hauteur, IntPtr parent, IntPtr menu, IntPtr instance, IntPtr param);
    [LibraryImport("user32.dll", EntryPoint = "DefWindowProcW")] internal static partial IntPtr DefWindowProc(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);
    [LibraryImport("user32.dll", EntryPoint = "PeekMessageW")]
    [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool PeekMessage(MSG* msg, IntPtr hwnd, uint min, uint max, uint retrait);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool TranslateMessage(MSG* msg);
    [LibraryImport("user32.dll", EntryPoint = "DispatchMessageW")] internal static partial IntPtr DispatchMessage(MSG* msg);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool ShowWindow(IntPtr hwnd, int commande);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool DestroyWindow(IntPtr hwnd);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool SetForegroundWindow(IntPtr hwnd);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool IsWindow(IntPtr hwnd);
    [LibraryImport("user32.dll")] internal static partial IntPtr GetForegroundWindow();
    [LibraryImport("user32.dll")] internal static partial uint GetWindowThreadProcessId(IntPtr hwnd, out uint processus);
    [LibraryImport("user32.dll", EntryPoint = "LoadCursorW")] internal static partial IntPtr LoadCursor(IntPtr instance, IntPtr nom);
    [LibraryImport("user32.dll", SetLastError = true)] internal static partial uint SendInput(uint nombre, INPUT* entrees, int taille);
    [LibraryImport("user32.dll", EntryPoint = "GetKeyboardLayout")] internal static partial IntPtr GetKeyboardLayout(uint thread);
    [LibraryImport("user32.dll", EntryPoint = "MapVirtualKeyW")] internal static partial uint MapVirtualKey(uint code, uint genre);
    [LibraryImport("user32.dll", EntryPoint = "GetKeyNameTextW")] internal static partial int GetKeyNameText(int parametre, char* nom, int taille);

    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW")] internal static partial IntPtr GetModuleHandle(IntPtr nom);

    // Sans elle, Thread.Sleep(1) dort 15,6 ms : la diffusion tombait à 31 images par seconde.
    [LibraryImport("winmm.dll")] internal static partial uint timeBeginPeriod(uint ms);
    [LibraryImport("winmm.dll")] internal static partial uint timeEndPeriod(uint ms);

    // --- la table des processus (Toolhelp) --------------------------------------------------------
    private const uint TH32CS_SNAPPROCESS = 2;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct PROCESSENTRY32W
    {
        public uint dwSize, cntUsage, th32ProcessID;
        public IntPtr th32DefaultHeapID;
        public uint th32ModuleID, cntThreads, th32ParentProcessID;
        public int pcPriClassBase;
        public uint dwFlags;
        public fixed char szExeFile[260];
    }

    [LibraryImport("kernel32.dll")] private static partial IntPtr CreateToolhelp32Snapshot(uint drapeaux, uint processus);
    [LibraryImport("kernel32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool Process32FirstW(IntPtr instantane, PROCESSENTRY32W* entree);
    [LibraryImport("kernel32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool Process32NextW(IntPtr instantane, PROCESSENTRY32W* entree);
    [LibraryImport("kernel32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool CloseHandle(IntPtr objet);

    /// <summary>Les processus enfants directs de <paramref name="pid"/>.</summary>
    internal static List<uint> Enfants(uint pid)
    {
        var enfants = new List<uint>();
        IntPtr instantane = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
        if (instantane == -1) return enfants;
        try
        {
            PROCESSENTRY32W e;
            e.dwSize = (uint)sizeof(PROCESSENTRY32W);
            for (bool ok = Process32FirstW(instantane, &e); ok; ok = Process32NextW(instantane, &e))
                if (e.th32ParentProcessID == pid) enfants.Add(e.th32ProcessID);
            return enfants;
        }
        finally { CloseHandle(instantane); }
    }

    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool EnumWindows(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, int> rappel, IntPtr parametre);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool IsWindowVisible(IntPtr hwnd);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool GetClientRect(IntPtr hwnd, int* r);

    private static HashSet<uint>? s_ProcessusCherches;
    private static IntPtr s_Trouvee;
    private static long s_Aire;

    /// <summary>La plus grande fenêtre visible (par sa zone client) de l'un des <paramref name="processus"/>, ou zéro.</summary>
    internal static IntPtr PlusGrandeFenetre(HashSet<uint> processus)
    {
        s_ProcessusCherches = processus;
        s_Trouvee = IntPtr.Zero;
        s_Aire = 0;
        EnumWindows(&Examiner, IntPtr.Zero);
        s_ProcessusCherches = null;
        return s_Trouvee;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static int Examiner(IntPtr hwnd, IntPtr parametre)
    {
        if (!IsWindowVisible(hwnd)) return 1;
        GetWindowThreadProcessId(hwnd, out uint pid);
        if (s_ProcessusCherches == null || !s_ProcessusCherches.Contains(pid)) return 1;
        int* r = stackalloc int[4];
        if (!GetClientRect(hwnd, r)) return 1;
        long aire = (long)r[2] * r[3];
        if (aire > s_Aire) { s_Aire = aire; s_Trouvee = hwnd; }
        return 1;
    }

    /// <summary>Le processus parent de <paramref name="pid"/>, ou 0 s'il est introuvable.</summary>
    internal static uint Parent(uint pid)
    {
        IntPtr instantane = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
        if (instantane == -1) return 0;
        try
        {
            PROCESSENTRY32W e;
            e.dwSize = (uint)sizeof(PROCESSENTRY32W);
            for (bool ok = Process32FirstW(instantane, &e); ok; ok = Process32NextW(instantane, &e))
                if (e.th32ProcessID == pid) return e.th32ParentProcessID;
            return 0;
        }
        finally { CloseHandle(instantane); }
    }

    /// <summary>
    /// Une touche par son code de balayage, enfoncee ou relachee — sans touche virtuelle : c'est le
    /// code que DirectInput lit (un code etendu, prefixe E0, porte KEYEVENTF_EXTENDEDKEY).
    /// </summary>
    internal static bool Frapper(ushort code, bool etendu, bool relache)
    {
        INPUT entree = default;
        entree.type = INPUT_KEYBOARD;
        entree.wScan = code;
        entree.dwFlags = KEYEVENTF_SCANCODE | (etendu ? KEYEVENTF_EXTENDEDKEY : 0) | (relache ? KEYEVENTF_KEYUP : 0);
        return SendInput(1, &entree, sizeof(INPUT)) == 1;
    }
}
