// Les appels Win32 de la fenetre d'installation.
//
// L'installeur n'emploie plus WinForms (2026-09-26) : WinForms ne peut pas etre elague, et il pesait
// a lui seul une quarantaine de Mo dans l'installeur publie. La fenetre est faite des memes
// controles natifs que WinForms enveloppait — STATIC, EDIT, BUTTON, la barre de progression de
// comctl32 —, donc elle a le meme aspect.

using System.Runtime.InteropServices;

namespace OpenForesia.Installer.Interface;

internal static unsafe partial class Natif
{
    // --- messages ---------------------------------------------------------------------------------
    internal const uint WM_DESTROY = 0x0002, WM_CLOSE = 0x0010, WM_SETFONT = 0x0030, WM_COMMAND = 0x0111,
                        WM_CTLCOLORSTATIC = 0x0138, WM_APP = 0x8000;
    internal const uint BM_GETCHECK = 0x00F0, BM_SETCHECK = 0x00F1, BM_SETSTYLE = 0x00F4;
    internal const uint PBM_SETPOS = 0x0402, PBM_SETRANGE32 = 0x0406;
    internal const int EN_CHANGE = 0x0300, BN_CLICKED = 0;
    internal const int IDOK = 1, IDCANCEL = 2, IDYES = 6;

    // --- styles -----------------------------------------------------------------------------------
    internal const uint WS_OVERLAPPED = 0, WS_CAPTION = 0x00C00000, WS_SYSMENU = 0x00080000,
                        WS_MINIMIZEBOX = 0x00020000, WS_CHILD = 0x40000000, WS_VISIBLE = 0x10000000,
                        WS_TABSTOP = 0x00010000, WS_DISABLED = 0x08000000;
    internal const uint WS_EX_CLIENTEDGE = 0x200, WS_EX_CONTROLPARENT = 0x10000;
    internal const uint ES_AUTOHSCROLL = 0x80, ES_READONLY = 0x800;
    internal const uint BS_PUSHBUTTON = 0, BS_DEFPUSHBUTTON = 1, BS_AUTOCHECKBOX = 3;
    internal const uint SS_LEFT = 0, SS_ETCHEDHORZ = 0x10, SS_NOPREFIX = 0x80;

    internal const int SW_HIDE = 0, SW_SHOW = 5;
    internal const int COLOR_BTNFACE = 15;
    internal const int TRANSPARENT = 1;

    internal const uint MB_OK = 0, MB_YESNO = 4, MB_ICONERROR = 0x10, MB_ICONWARNING = 0x30,
                        MB_ICONINFORMATION = 0x40, MB_DEFBUTTON2 = 0x100;

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

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT { public int left, top, right, bottom; }

    [StructLayout(LayoutKind.Sequential)]
    internal struct INITCOMMONCONTROLSEX { public uint dwSize, dwICC; }

    [LibraryImport("user32.dll", EntryPoint = "RegisterClassExW")] internal static partial ushort RegisterClassEx(WNDCLASSEXW* classe);
    [LibraryImport("user32.dll", EntryPoint = "CreateWindowExW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr CreateWindowEx(uint exStyle, string classe, string? texte, uint style,
        int x, int y, int largeur, int hauteur, IntPtr parent, IntPtr menu, IntPtr instance, IntPtr param);
    [LibraryImport("user32.dll", EntryPoint = "DefWindowProcW")] internal static partial IntPtr DefWindowProc(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);
    [LibraryImport("user32.dll", EntryPoint = "GetMessageW")] internal static partial int GetMessage(MSG* msg, IntPtr hwnd, uint min, uint max);
    [LibraryImport("user32.dll", EntryPoint = "IsDialogMessageW")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool IsDialogMessage(IntPtr hwnd, MSG* msg);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool TranslateMessage(MSG* msg);
    [LibraryImport("user32.dll", EntryPoint = "DispatchMessageW")] internal static partial IntPtr DispatchMessage(MSG* msg);
    [LibraryImport("user32.dll", EntryPoint = "PostMessageW")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool PostMessage(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);
    [LibraryImport("user32.dll", EntryPoint = "SendMessageW")] internal static partial IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);
    [LibraryImport("user32.dll")] internal static partial void PostQuitMessage(int code);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool ShowWindow(IntPtr hwnd, int commande);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool DestroyWindow(IntPtr hwnd);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool EnableWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool actif);
    [LibraryImport("user32.dll")] internal static partial IntPtr SetFocus(IntPtr hwnd);
    [LibraryImport("user32.dll", EntryPoint = "SetWindowTextW", StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool SetWindowText(IntPtr hwnd, string texte);
    [LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW")] internal static partial int GetWindowTextLength(IntPtr hwnd);
    [LibraryImport("user32.dll", EntryPoint = "GetWindowTextW")] internal static partial int GetWindowText(IntPtr hwnd, char* tampon, int taille);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool AdjustWindowRectEx(RECT* rect, uint style, [MarshalAs(UnmanagedType.Bool)] bool menu, uint exStyle);
    [LibraryImport("user32.dll")] internal static partial int GetSystemMetrics(int index);
    [LibraryImport("user32.dll")] internal static partial uint GetDpiForSystem();
    [LibraryImport("user32.dll")] internal static partial IntPtr GetSysColorBrush(int index);
    [LibraryImport("user32.dll")] internal static partial uint GetSysColor(int index);
    [LibraryImport("user32.dll", EntryPoint = "LoadCursorW")] internal static partial IntPtr LoadCursor(IntPtr instance, IntPtr nom);
    [LibraryImport("user32.dll", EntryPoint = "LoadIconW")] internal static partial IntPtr LoadIcon(IntPtr instance, IntPtr nom);
    [LibraryImport("user32.dll", EntryPoint = "MessageBoxW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial int MessageBox(IntPtr hwnd, string texte, string titre, uint type);

    [LibraryImport("gdi32.dll", EntryPoint = "CreateFontW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr CreateFont(int hauteur, int largeur, int echappement, int orientation, int graisse,
        uint italique, uint souligne, uint barre, uint jeu, uint precision, uint decoupe, uint qualite, uint famille, string nom);
    [LibraryImport("gdi32.dll")] internal static partial uint SetTextColor(IntPtr hdc, uint couleur);
    [LibraryImport("gdi32.dll")] internal static partial uint SetBkColor(IntPtr hdc, uint couleur);

    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW")] internal static partial IntPtr GetModuleHandle(IntPtr nom);
    [LibraryImport("comctl32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool InitCommonControlsEx(INITCOMMONCONTROLSEX* init);

    internal static uint Rgb(byte r, byte g, byte b) => (uint)(r | (g << 8) | (b << 16));

    internal static string LireTexte(IntPtr hwnd)
    {
        int n = GetWindowTextLength(hwnd);
        if (n == 0) return "";
        char* tampon = stackalloc char[n + 1];
        GetWindowText(hwnd, tampon, n + 1);
        return new string(tampon, 0, n);
    }
}
