// Les appels Win32 des fenetres a controles (la fenetre de l'hote) : les controles natifs — STATIC,
// EDIT, BUTTON —, les polices, la minuterie, le presse-papiers. Le manifeste active les controles
// communs version 6 : ils ont le style visuel de Windows, comme ceux de l'installeur.

using System.Runtime.InteropServices;

namespace OpenForesia.Lanceur.Natif;

internal static unsafe partial class Win32
{
    internal const uint WM_SETFONT = 0x0030, WM_COMMAND = 0x0111, WM_TIMER = 0x0113, WM_CTLCOLORSTATIC = 0x0138, WM_APP = 0x8000;
    internal const uint BM_GETCHECK = 0x00F0, BM_SETCHECK = 0x00F1;
    internal const int EN_CHANGE = 0x0300, BN_CLICKED = 0;
    internal const uint WS_CAPTION = 0x00C00000, WS_SYSMENU = 0x00080000, WS_MINIMIZEBOX = 0x00020000,
                        WS_CHILD = 0x40000000, WS_TABSTOP = 0x00010000, WS_DISABLED = 0x08000000;
    internal const uint WS_EX_CLIENTEDGE = 0x200, WS_EX_CONTROLPARENT = 0x10000;
    internal const uint ES_AUTOHSCROLL = 0x80, ES_READONLY = 0x800, ES_NUMBER = 0x2000;
    internal const uint BS_PUSHBUTTON = 0, BS_DEFPUSHBUTTON = 1, BS_AUTOCHECKBOX = 3;
    internal const uint SS_LEFT = 0, SS_NOPREFIX = 0x80;
    internal const uint CBS_DROPDOWNLIST = 3, WS_VSCROLL = 0x00200000;
    internal const uint CB_ADDSTRING = 0x0143, CB_GETCURSEL = 0x0147, CB_SETCURSEL = 0x014E;

    [LibraryImport("user32.dll", EntryPoint = "SendMessageW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr SendMessageChaine(IntPtr hwnd, uint msg, IntPtr w, string l);

    /// <summary>Un message dont le paramètre est un texte (CB_ADDSTRING).</summary>
    internal static IntPtr SendMessageTexte(IntPtr hwnd, uint msg, string texte) => SendMessageChaine(hwnd, msg, IntPtr.Zero, texte);
    internal const uint MB_OK = 0, MB_YESNO = 4, MB_ICONERROR = 0x10, MB_ICONWARNING = 0x30;
    internal const int IDYES = 6, COLOR_BTNFACE = 15, TRANSPARENT = 1;

    [StructLayout(LayoutKind.Sequential)] internal struct RECT { public int Gauche, Haut, Droite, Bas; }

    [LibraryImport("user32.dll", EntryPoint = "GetMessageW")] internal static partial int GetMessage(MSG* msg, IntPtr hwnd, uint min, uint max);
    [LibraryImport("user32.dll", EntryPoint = "IsDialogMessageW")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool IsDialogMessage(IntPtr hwnd, MSG* msg);
    [LibraryImport("user32.dll", EntryPoint = "PostMessageW")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool PostMessage(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);
    [LibraryImport("user32.dll", EntryPoint = "SendMessageW")] internal static partial IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);
    [LibraryImport("user32.dll")] internal static partial void PostQuitMessage(int code);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool EnableWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool actif);
    [LibraryImport("user32.dll", EntryPoint = "SetWindowTextW", StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool SetWindowText(IntPtr hwnd, string texte);
    [LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW")] private static partial int GetWindowTextLength(IntPtr hwnd);
    [LibraryImport("user32.dll", EntryPoint = "GetWindowTextW")] private static partial int GetWindowText(IntPtr hwnd, char* tampon, int taille);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] internal static partial bool AdjustWindowRectEx(RECT* rect, uint style, [MarshalAs(UnmanagedType.Bool)] bool menu, uint exStyle);
    [LibraryImport("user32.dll")] internal static partial int GetSystemMetrics(int index);
    [LibraryImport("user32.dll")] internal static partial uint GetDpiForSystem();
    [LibraryImport("user32.dll")] internal static partial IntPtr GetSysColorBrush(int index);
    [LibraryImport("user32.dll")] internal static partial uint GetSysColor(int index);
    [LibraryImport("user32.dll")] internal static partial nuint SetTimer(IntPtr hwnd, nuint id, uint ms, IntPtr rappel);
    [LibraryImport("user32.dll", EntryPoint = "MessageBoxW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial int MessageBox(IntPtr hwnd, string texte, string titre, uint type);
    [LibraryImport("user32.dll", EntryPoint = "LoadIconW")] internal static partial IntPtr LoadIcon(IntPtr instance, IntPtr nom);

    [LibraryImport("gdi32.dll", EntryPoint = "CreateFontW", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr CreateFont(int hauteur, int largeur, int echappement, int orientation, int graisse,
        uint italique, uint souligne, uint barre, uint jeu, uint precision, uint decoupe, uint qualite, uint famille, string nom);
    [LibraryImport("gdi32.dll")] internal static partial uint SetBkColor(IntPtr hdc, uint couleur);
    [LibraryImport("gdi32.dll")] internal static partial uint SetTextColor(IntPtr hdc, uint couleur);

    internal static string LireTexte(IntPtr hwnd)
    {
        int n = GetWindowTextLength(hwnd);
        if (n == 0) return "";
        char* tampon = stackalloc char[n + 1];
        GetWindowText(hwnd, tampon, n + 1);
        return new string(tampon, 0, n);
    }

    // --- le presse-papiers ------------------------------------------------------------------------
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool OpenClipboard(IntPtr proprietaire);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool EmptyClipboard();
    [LibraryImport("user32.dll")] private static partial IntPtr SetClipboardData(uint format, IntPtr donnees);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool CloseClipboard();
    [LibraryImport("kernel32.dll")] private static partial IntPtr GlobalAlloc(uint drapeaux, nuint taille);
    [LibraryImport("kernel32.dll")] private static partial IntPtr GlobalLock(IntPtr memoire);
    [LibraryImport("kernel32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool GlobalUnlock(IntPtr memoire);
    [LibraryImport("kernel32.dll")] private static partial IntPtr GlobalFree(IntPtr memoire);

    /// <summary>Met <paramref name="texte"/> dans le presse-papiers (CF_UNICODETEXT) ; faux s'il est occupé.</summary>
    internal static bool Copier(IntPtr proprietaire, string texte)
    {
        const uint CfUnicodeText = 13, GmemMoveable = 2;
        if (!OpenClipboard(proprietaire)) return false;
        try
        {
            EmptyClipboard();
            IntPtr memoire = GlobalAlloc(GmemMoveable, (nuint)((texte.Length + 1) * 2));
            if (memoire == IntPtr.Zero) return false;
            char* p = (char*)GlobalLock(memoire);
            texte.AsSpan().CopyTo(new Span<char>(p, texte.Length));
            p[texte.Length] = '\0';
            GlobalUnlock(memoire);
            if (SetClipboardData(CfUnicodeText, memoire) == IntPtr.Zero)
            {
                GlobalFree(memoire);                  // le presse-papiers ne l'a pas prise
                return false;
            }
            return true;
        }
        finally { CloseClipboard(); }
    }
}
