// La fenetre de l'invite (taches 1.7 et 2.4) : l'image du jeu, au plus grand en gardant ses
// proportions (bandes noires autour), un bandeau d'etat (joueur, latence, pause), et un texte au
// centre tant qu'il n'y a pas d'image (la configuration de la manette, la connexion). Alt+Entree
// bascule en plein ecran. Elle sert aussi de premier plan au clavier : l'invite ne joue au clavier
// que dans cette fenetre.
//
// L'image est dessinee en GDI (StretchDIBits) : sans dependance, et bien assez rapide pour du
// 960 x 720 a 60 images par seconde. Le fond n'est jamais efface par Windows (WM_ERASEBKGND), pour
// ne pas scintiller : les bandes noires sont peintes a la main.

using System.Runtime.InteropServices;
using OpenForesia.Multiplayer.Natif;

namespace OpenForesia.Multiplayer.Invite;

internal sealed unsafe partial class FenetreInvite : IDisposable
{
    [LibraryImport("user32.dll")] private static partial IntPtr BeginPaint(IntPtr hwnd, byte* ps);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool EndPaint(IntPtr hwnd, byte* ps);
    [LibraryImport("user32.dll")] private static partial IntPtr GetDC(IntPtr hwnd);
    [LibraryImport("user32.dll")] private static partial int ReleaseDC(IntPtr hwnd, IntPtr hdc);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool GetClientRect(IntPtr hwnd, Rect* r);
    [LibraryImport("user32.dll", EntryPoint = "DrawTextW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int DrawText(IntPtr hdc, string texte, int n, Rect* r, uint format);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool InvalidateRect(IntPtr hwnd, IntPtr r, [MarshalAs(UnmanagedType.Bool)] bool effacer);
    [LibraryImport("user32.dll")] private static partial int FillRect(IntPtr hdc, Rect* r, IntPtr brosse);
    [LibraryImport("user32.dll", EntryPoint = "GetWindowLongW")] private static partial int GetWindowLong(IntPtr hwnd, int index);
    [LibraryImport("user32.dll", EntryPoint = "SetWindowLongW")] private static partial int SetWindowLong(IntPtr hwnd, int index, int valeur);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool GetWindowRect(IntPtr hwnd, Rect* r);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool SetWindowPos(IntPtr hwnd, IntPtr apres, int x, int y, int l, int h, uint drapeaux);
    [LibraryImport("user32.dll")] private static partial IntPtr MonitorFromWindow(IntPtr hwnd, uint drapeaux);
    [LibraryImport("user32.dll", EntryPoint = "GetMonitorInfoW")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool GetMonitorInfo(IntPtr moniteur, InfoMoniteur* info);
    [LibraryImport("gdi32.dll", EntryPoint = "CreateFontW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr CreateFont(int h, int l, int e, int o, int g, uint i, uint s, uint b, uint jeu, uint p, uint d, uint q, uint f, string nom);
    [LibraryImport("gdi32.dll")] private static partial IntPtr SelectObject(IntPtr hdc, IntPtr objet);
    [LibraryImport("gdi32.dll")] private static partial int SetBkMode(IntPtr hdc, int mode);
    [LibraryImport("gdi32.dll")] private static partial uint SetTextColor(IntPtr hdc, uint couleur);
    [LibraryImport("gdi32.dll")] private static partial IntPtr GetStockObject(int objet);
    [LibraryImport("gdi32.dll")] private static partial int SetStretchBltMode(IntPtr hdc, int mode);
    [LibraryImport("gdi32.dll")]
    private static partial int StretchDIBits(IntPtr hdc, int xd, int yd, int ld, int hd, int xs, int ys, int ls, int hs,
                                             void* bits, InfoBitmap* info, uint usage, uint rop);

    [StructLayout(LayoutKind.Sequential)] private struct Rect { public int Gauche, Haut, Droite, Bas; }
    [StructLayout(LayoutKind.Sequential)] private struct InfoMoniteur { public uint Taille; public Rect Ecran, Travail; public uint Drapeaux; }
    [StructLayout(LayoutKind.Sequential)]
    private struct InfoBitmap
    {
        public uint Taille; public int Largeur, Hauteur; public ushort Plans, Bits; public uint Compression, TailleImage;
        public int XPpm, YPpm; public uint Couleurs, Importantes;
    }

    private const uint WM_PAINT = 0x000F, WM_ERASEBKGND = 0x0014, WM_SIZE = 0x0005;
    private const uint DT_CENTER = 0x1, DT_WORDBREAK = 0x10, DT_CALCRECT = 0x400, DT_LEFT = 0;
    private const int GWL_STYLE = -16, NoirStock = 4, ColorOnColor = 3;
    private const uint SWP_FRAMECHANGED = 0x20, SWP_NOZORDER = 0x4, Srccopy = 0x00CC0020;
    private const uint Blanc = 0x00FFFFFF, Jaune = 0x0000E0FF;

    private static FenetreInvite? s_Instance;
    private static IntPtr s_Police, s_PetitePolice;

    private readonly IntPtr _hwnd;
    private string _texte = "", _etat = "";
    private bool _alerte;
    private byte[]? _image;
    private int _largeurImage, _hauteurImage;
    private bool _pleinEcran;
    private Rect _avantPleinEcran;
    private int _styleAvant;

    internal IntPtr Handle => _hwnd;
    internal bool Fermee { get; private set; }

    internal FenetreInvite(string titre)
    {
        s_Instance = this;
        const string classe = "OpenForesiaInvite";
        fixed (char* nom = classe)
        {
            var wc = new Win32.WNDCLASSEXW
            {
                cbSize = (uint)sizeof(Win32.WNDCLASSEXW),
                lpfnWndProc = &Procedure,
                hInstance = Win32.GetModuleHandle(IntPtr.Zero),
                hCursor = Win32.LoadCursor(IntPtr.Zero, 32512),
                hbrBackground = IntPtr.Zero,
                lpszClassName = nom,
            };
            Win32.RegisterClassEx(&wc);
        }
        s_Police = CreateFont(-22, 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        s_PetitePolice = CreateFont(-16, 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        _hwnd = Win32.CreateWindowEx(0, classe, titre, Win32.WS_OVERLAPPEDWINDOW | Win32.WS_VISIBLE,
            200, 100, 960 + 16, 720 + 39, IntPtr.Zero, IntPtr.Zero, Win32.GetModuleHandle(IntPtr.Zero), IntPtr.Zero);
        Win32.SetForegroundWindow(_hwnd);
    }

    /// <summary>Le texte du centre, montré tant qu'aucune image n'est arrivée.</summary>
    internal void Texte(string texte)
    {
        if (texte == _texte) return;
        _texte = texte;
        if (_image == null) InvalidateRect(_hwnd, IntPtr.Zero, false);
    }

    /// <summary>Le bandeau d'état, en haut de l'image ; <paramref name="alerte"/> le met en jaune.</summary>
    internal void Etat(string etat, bool alerte)
    {
        if (etat == _etat && alerte == _alerte) return;
        _etat = etat;
        _alerte = alerte;
        if (_image != null) InvalidateRect(_hwnd, IntPtr.Zero, false);
    }

    /// <summary>Une nouvelle image BGRA (lignes de largeur × 4 octets), recopiée puis dessinée tout de suite.</summary>
    internal void Image(byte* bgra, int largeur, int hauteur)
    {
        int taille = largeur * hauteur * 4;
        if (_image == null || _image.Length != taille) _image = new byte[taille];
        new ReadOnlySpan<byte>(bgra, taille).CopyTo(_image);
        _largeurImage = largeur;
        _hauteurImage = hauteur;
        if (Ecran is { Ouvert: true }) return;                  // gardée pour la fermeture de l'écran de la manette
        Redessiner();
    }

    /// <summary>L'écran de configuration de la manette, ouvert par F1 par-dessus l'image.</summary>
    internal EcranManette? Ecran { get; set; }

    /// <summary>Dessine tout de suite, sans attendre WM_PAINT.</summary>
    internal void Redessiner()
    {
        IntPtr hdc = GetDC(_hwnd);
        try { Dessiner(hdc); }
        finally { ReleaseDC(_hwnd, hdc); }
    }

    /// <summary>Traite les messages en attente ; faux une fois la fenêtre fermée.</summary>
    internal bool Pomper()
    {
        Win32.MSG msg;
        while (Win32.PeekMessage(&msg, IntPtr.Zero, 0, 0, Win32.PM_REMOVE))
        {
            Win32.TranslateMessage(&msg);
            Win32.DispatchMessage(&msg);
        }
        return !Fermee;
    }

    private void Dessiner(IntPtr hdc)
    {
        Rect r;
        GetClientRect(_hwnd, &r);
        if (Ecran is { Ouvert: true })
        {
            Ecran.Dessiner(hdc, r.Droite, r.Bas);
            return;
        }
        IntPtr noir = GetStockObject(NoirStock);
        SetBkMode(hdc, 1);

        if (_image == null)
        {
            FillRect(hdc, &r, noir);
            SelectObject(hdc, s_Police);
            SetTextColor(hdc, Blanc);
            Rect zone = r;
            zone.Gauche += 24; zone.Droite -= 24;
            Rect mesure = zone;
            DrawText(hdc, _texte, -1, &mesure, DT_CENTER | DT_WORDBREAK | DT_CALCRECT);
            zone.Haut = (r.Bas - (mesure.Bas - mesure.Haut)) / 2;
            DrawText(hdc, _texte, -1, &zone, DT_CENTER | DT_WORDBREAK);
            return;
        }

        // L'image au plus grand en gardant ses proportions, les bandes en noir.
        int lc = r.Droite, hc = r.Bas;
        double echelle = Math.Min((double)lc / _largeurImage, (double)hc / _hauteurImage);
        int l = (int)(_largeurImage * echelle), h = (int)(_hauteurImage * echelle);
        int x = (lc - l) / 2, y = (hc - h) / 2;
        Rect b;
        b = new Rect { Gauche = 0, Haut = 0, Droite = lc, Bas = y }; FillRect(hdc, &b, noir);
        b = new Rect { Gauche = 0, Haut = y + h, Droite = lc, Bas = hc }; FillRect(hdc, &b, noir);
        b = new Rect { Gauche = 0, Haut = y, Droite = x, Bas = y + h }; FillRect(hdc, &b, noir);
        b = new Rect { Gauche = x + l, Haut = y, Droite = lc, Bas = y + h }; FillRect(hdc, &b, noir);

        var info = new InfoBitmap
        {
            Taille = (uint)sizeof(InfoBitmap), Largeur = _largeurImage, Hauteur = -_hauteurImage,   // de haut en bas
            Plans = 1, Bits = 32,
        };
        SetStretchBltMode(hdc, ColorOnColor);
        fixed (byte* pixels = _image)
            StretchDIBits(hdc, x, y, l, h, 0, 0, _largeurImage, _hauteurImage, pixels, &info, 0, Srccopy);

        if (_etat.Length > 0)
        {
            SelectObject(hdc, s_PetitePolice);
            SetTextColor(hdc, _alerte ? Jaune : Blanc);
            Rect t = new() { Gauche = x + 10, Haut = y + 6, Droite = x + l - 10, Bas = y + 40 };
            DrawText(hdc, _etat, -1, &t, DT_LEFT);
        }
    }

    private void BasculerPleinEcran()
    {
        if (!_pleinEcran)
        {
            Rect avant;
            GetWindowRect(_hwnd, &avant);
            _avantPleinEcran = avant;
            _styleAvant = GetWindowLong(_hwnd, GWL_STYLE);
            var info = new InfoMoniteur { Taille = (uint)sizeof(InfoMoniteur) };
            GetMonitorInfo(MonitorFromWindow(_hwnd, 2), &info);
            SetWindowLong(_hwnd, GWL_STYLE, unchecked((int)(Win32.WS_VISIBLE | 0x80000000)));   // WS_POPUP
            SetWindowPos(_hwnd, IntPtr.Zero, info.Ecran.Gauche, info.Ecran.Haut, info.Ecran.Droite - info.Ecran.Gauche,
                         info.Ecran.Bas - info.Ecran.Haut, SWP_FRAMECHANGED | SWP_NOZORDER);
        }
        else
        {
            SetWindowLong(_hwnd, GWL_STYLE, _styleAvant);
            Rect a = _avantPleinEcran;
            SetWindowPos(_hwnd, IntPtr.Zero, a.Gauche, a.Haut, a.Droite - a.Gauche, a.Bas - a.Haut, SWP_FRAMECHANGED | SWP_NOZORDER);
        }
        _pleinEcran = !_pleinEcran;
        InvalidateRect(_hwnd, IntPtr.Zero, false);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static IntPtr Procedure(IntPtr hwnd, uint msg, IntPtr w, IntPtr l)
    {
        FenetreInvite? f = s_Instance;
        switch (msg)
        {
            case WM_ERASEBKGND:
                return 1;
            case WM_SIZE when f != null:
                InvalidateRect(hwnd, IntPtr.Zero, false);
                break;
            case WM_PAINT when f != null:
                byte* ps = stackalloc byte[72];                     // PAINTSTRUCT, 64 octets en 32 bits
                IntPtr hdc = BeginPaint(hwnd, ps);
                f.Dessiner(hdc);
                EndPaint(hwnd, ps);
                return IntPtr.Zero;
            case Win32.WM_KEYDOWN when f?.Ecran != null && (int)w == 0x70 && ((long)l & 0x40000000) == 0:   // F1, sans la répétition
                f.Ecran.Basculer();
                InvalidateRect(hwnd, IntPtr.Zero, false);
                return IntPtr.Zero;
            case 0x0201 or 0x0204 when f?.Ecran is { Ouvert: true }:     // WM_LBUTTONDOWN, WM_RBUTTONDOWN
                Rect zone;
                GetClientRect(hwnd, &zone);
                f.Ecran.Clic((short)((long)l & 0xFFFF), (short)(((long)l >> 16) & 0xFFFF), zone.Droite, zone.Bas, droit: msg == 0x0204);
                if (f.Ecran.AChange || !f.Ecran.Ouvert) { f.Ecran.AChange = false; f.Redessiner(); }
                return IntPtr.Zero;
            case Win32.WM_SYSKEYDOWN when f != null && (int)w == 0x0D:        // Alt+Entrée
                f.BasculerPleinEcran();
                return IntPtr.Zero;
            case Win32.WM_SYSCHAR when (int)w == 0x0D:
                return IntPtr.Zero;                                    // pas de bip
            case Win32.WM_CLOSE when f != null:
                f.Fermee = true;
                Win32.DestroyWindow(hwnd);
                return IntPtr.Zero;
        }
        return Win32.DefWindowProc(hwnd, msg, w, l);
    }

    public void Dispose()
    {
        if (!Fermee) Win32.DestroyWindow(_hwnd);
        s_Instance = null;
    }
}
