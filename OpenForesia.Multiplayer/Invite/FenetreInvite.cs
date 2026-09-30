// La fenetre de l'invite (taches 1.7 et 2.4) : l'image du jeu, au plus grand en gardant ses
// proportions (bandes noires autour), un bandeau d'etat (joueur, latence, pause), et un texte au
// centre tant qu'il n'y a pas d'image (la configuration de la manette, la connexion). Alt+Entree
// bascule en plein ecran. Elle sert aussi de premier plan au clavier : l'invite ne joue au clavier
// que dans cette fenetre.
//
// F2 montre la qualite du flux (images par seconde, debit, ping, pertes, deux courbes) ; F3 cache le
// bandeau du haut — sauf s'il signale une pause ou une coupure.
//
// L'image est dessinee en GDI (StretchDIBits) : sans dependance, et bien assez rapide pour du
// 960 x 720 a 60 images par seconde. Elle part d'abord dans une image en memoire, avec le bandeau et
// les statistiques par-dessus, puis a l'ecran d'un seul coup : dessines directement sur la fenetre,
// ils clignotaient. Le fond n'est jamais efface par Windows (WM_ERASEBKGND), pour la meme raison.
//
// La fenetre s'ouvre a la taille exacte du flux (zone cliente, cadre en plus) : une image a 1:1 est
// la seule parfaitement nette. Ouverte cadre compris a 960 x 720, comme au premier jet, sa zone
// cliente etait un peu plus petite que l'image — des lignes et des colonnes sautaient, et les textes
// en souffraient. Plus petite que le flux, l'image est reduite en HALFTONE (moyennee) ; plus grande,
// au plus proche voisin (COLORONCOLOR), qui ne perd rien et ne coute rien.

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
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool AdjustWindowRectEx(Rect* r, uint style, [MarshalAs(UnmanagedType.Bool)] bool menu, uint exStyle);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool IsZoomed(IntPtr hwnd);
    [LibraryImport("gdi32.dll", EntryPoint = "CreateFontW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr CreateFont(int h, int l, int e, int o, int g, uint i, uint s, uint b, uint jeu, uint p, uint d, uint q, uint f, string nom);
    [LibraryImport("gdi32.dll")] private static partial IntPtr SelectObject(IntPtr hdc, IntPtr objet);
    [LibraryImport("gdi32.dll")] private static partial int SetBkMode(IntPtr hdc, int mode);
    [LibraryImport("gdi32.dll")] private static partial uint SetTextColor(IntPtr hdc, uint couleur);
    [LibraryImport("gdi32.dll")] private static partial IntPtr GetStockObject(int objet);
    [LibraryImport("gdi32.dll")] private static partial int SetStretchBltMode(IntPtr hdc, int mode);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool SetBrushOrgEx(IntPtr hdc, int x, int y, void* ancien);
    [LibraryImport("gdi32.dll")]
    private static partial int StretchDIBits(IntPtr hdc, int xd, int yd, int ld, int hd, int xs, int ys, int ls, int hs,
                                             void* bits, InfoBitmap* info, uint usage, uint rop);
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreateCompatibleDC(IntPtr hdc);
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreateCompatibleBitmap(IntPtr hdc, int l, int h);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool DeleteDC(IntPtr hdc);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool DeleteObject(IntPtr objet);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool BitBlt(IntPtr d, int x, int y, int l, int h, IntPtr s, int xs, int ys, uint rop);
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreateSolidBrush(uint couleur);
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreatePen(int style, int largeur, uint couleur);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool MoveToEx(IntPtr hdc, int x, int y, void* ancien);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool LineTo(IntPtr hdc, int x, int y);
    [LibraryImport("msimg32.dll")] [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool AlphaBlend(IntPtr d, int xd, int yd, int ld, int hd, IntPtr s, int xs, int ys, int ls, int hs, uint melange);

    [StructLayout(LayoutKind.Sequential)] private struct Rect { public int Gauche, Haut, Droite, Bas; }
    [StructLayout(LayoutKind.Sequential)] private struct InfoMoniteur { public uint Taille; public Rect Ecran, Travail; public uint Drapeaux; }
    [StructLayout(LayoutKind.Sequential)]
    private struct InfoBitmap
    {
        public uint Taille; public int Largeur, Hauteur; public ushort Plans, Bits; public uint Compression, TailleImage;
        public int XPpm, YPpm; public uint Couleurs, Importantes;
    }

    private const uint WM_PAINT = 0x000F, WM_ERASEBKGND = 0x0014, WM_SIZE = 0x0005;
    private const uint DT_CENTER = 0x1, DT_WORDBREAK = 0x10, DT_CALCRECT = 0x400, DT_LEFT = 0, DT_UNELIGNE = 0x20, DT_NOPREFIX = 0x800;
    private const int GWL_STYLE = -16, NoirStock = 4, ColorOnColor = 3, Halftone = 4;
    private const uint SWP_FRAMECHANGED = 0x20, SWP_NOZORDER = 0x4, SWP_NOMOVE = 0x2, Srccopy = 0x00CC0020;
    private const int VK_F1 = 0x70, VK_F2 = 0x71, VK_F3 = 0x72;
    private const uint Blanc = 0x00FFFFFF, Jaune = 0x0000E0FF, Gris = 0x00A0A0A0, Grille = 0x00585858;
    private const uint Vert = 0x0060D860, Orange = 0x0000C0FF, Rouge = 0x004858FF, Bleu = 0x00FFC050;
    private const int LargeurDepart = 960, HauteurDepart = 720;

    private static FenetreInvite? s_Instance;
    private static IntPtr s_Police, s_PetitePolice, s_PoliceStats, s_PoliceGraphe;
    private static IntPtr s_BrosseVert, s_BrosseOrange, s_BrosseRouge, s_BrosseGrille, s_CrayonBleu, s_CrayonGrille;

    private readonly IntPtr _hwnd;
    private string _texte = "", _etat = "";
    private bool _alerte;
    private byte[]? _image;
    private int _largeurImage, _hauteurImage;
    private bool _pleinEcran, _tailleAjustee;
    private Rect _avantPleinEcran;
    private int _styleAvant;

    // L'image en mémoire où tout se compose avant l'écran, et un pixel noir pour les voiles.
    private IntPtr _memoire, _tampon, _ancienTampon, _noir, _pixelNoir, _ancienPixel;
    private int _largeurTampon, _hauteurTampon;

    internal IntPtr Handle => _hwnd;
    internal bool Fermee { get; private set; }

    /// <summary>Les chiffres que F2 montre ; relevés par la boucle de l'invité.</summary>
    internal Statistiques? Statistiques { get; set; }

    /// <summary>Vrai pendant que F2 montre les statistiques : la boucle redessine alors même sans image nouvelle.</summary>
    internal bool StatistiquesVisibles { get; private set; }

    /// <summary>Vrai quand F3 a caché le bandeau du haut.</summary>
    internal bool BandeauCache { get; private set; }

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
        if (s_Police == IntPtr.Zero)
        {
            s_Police = CreateFont(-22, 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
            s_PetitePolice = CreateFont(-16, 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
            s_PoliceStats = CreateFont(-15, 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Consolas");
            s_PoliceGraphe = CreateFont(-12, 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
            s_BrosseVert = CreateSolidBrush(Vert);
            s_BrosseOrange = CreateSolidBrush(Orange);
            s_BrosseRouge = CreateSolidBrush(Rouge);
            s_BrosseGrille = CreateSolidBrush(Grille);
            s_CrayonBleu = CreatePen(0, 2, Bleu);
            s_CrayonGrille = CreatePen(2, 1, Grille);                           // PS_DOT
        }

        var r = new Rect { Droite = LargeurDepart, Bas = HauteurDepart };
        AdjustWindowRectEx(&r, Win32.WS_OVERLAPPEDWINDOW, false, 0);
        _hwnd = Win32.CreateWindowEx(0, classe, titre, Win32.WS_OVERLAPPEDWINDOW | Win32.WS_VISIBLE,
            200, 100, r.Droite - r.Gauche, r.Bas - r.Haut, IntPtr.Zero, IntPtr.Zero, Win32.GetModuleHandle(IntPtr.Zero), IntPtr.Zero);
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
        if (!_tailleAjustee)
        {
            _tailleAjustee = true;
            AjusterAuFlux(largeur, hauteur);
        }
        if (Ecran is { Ouvert: true }) return;                  // gardée pour la fermeture de l'écran de la manette
        Redessiner();
    }

    /// <summary>
    /// À la première image : la zone cliente prend la taille du flux, si la fenêtre a encore sa
    /// taille de départ (ni agrandie, ni en plein écran, ni retouchée) et que le flux tient à l'écran.
    /// </summary>
    private void AjusterAuFlux(int largeur, int hauteur)
    {
        Rect client;
        GetClientRect(_hwnd, &client);
        if (_pleinEcran || IsZoomed(_hwnd) || client.Droite != LargeurDepart || client.Bas != HauteurDepart) return;
        if (largeur == LargeurDepart && hauteur == HauteurDepart) return;
        var r = new Rect { Droite = largeur, Bas = hauteur };
        AdjustWindowRectEx(&r, (uint)GetWindowLong(_hwnd, GWL_STYLE), false, 0);
        var info = new InfoMoniteur { Taille = (uint)sizeof(InfoMoniteur) };
        GetMonitorInfo(MonitorFromWindow(_hwnd, 2), &info);
        if (r.Droite - r.Gauche > info.Travail.Droite - info.Travail.Gauche || r.Bas - r.Haut > info.Travail.Bas - info.Travail.Haut) return;
        SetWindowPos(_hwnd, IntPtr.Zero, 0, 0, r.Droite - r.Gauche, r.Bas - r.Haut, SWP_NOMOVE | SWP_NOZORDER);
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

        int lc = r.Droite, hc = r.Bas;
        if (lc <= 0 || hc <= 0) return;                         // réduite
        IntPtr m = Tampon(hdc, lc, hc);
        SetBkMode(m, 1);

        // L'image au plus grand en gardant ses proportions, les bandes en noir.
        double echelle = Math.Min((double)lc / _largeurImage, (double)hc / _hauteurImage);
        int l = (int)Math.Round(_largeurImage * echelle), h = (int)Math.Round(_hauteurImage * echelle);
        int x = (lc - l) / 2, y = (hc - h) / 2;
        Rect b;
        b = new Rect { Gauche = 0, Haut = 0, Droite = lc, Bas = y }; FillRect(m, &b, noir);
        b = new Rect { Gauche = 0, Haut = y + h, Droite = lc, Bas = hc }; FillRect(m, &b, noir);
        b = new Rect { Gauche = 0, Haut = y, Droite = x, Bas = y + h }; FillRect(m, &b, noir);
        b = new Rect { Gauche = x + l, Haut = y, Droite = lc, Bas = y + h }; FillRect(m, &b, noir);

        var info = new InfoBitmap
        {
            Taille = (uint)sizeof(InfoBitmap), Largeur = _largeurImage, Hauteur = -_hauteurImage,   // de haut en bas
            Plans = 1, Bits = 32,
        };
        bool reduite = l < _largeurImage;
        SetStretchBltMode(m, reduite ? Halftone : ColorOnColor);
        if (reduite) SetBrushOrgEx(m, 0, 0, null);              // exigé après HALFTONE
        fixed (byte* pixels = _image)
            StretchDIBits(m, x, y, l, h, 0, 0, _largeurImage, _hauteurImage, pixels, &info, 0, Srccopy);

        int haut = y;
        if (_etat.Length > 0 && (!BandeauCache || _alerte))
        {
            Voile(m, x, y, l, 30, 150);
            SelectObject(m, s_PetitePolice);
            SetTextColor(m, _alerte ? Jaune : Blanc);
            Rect t = new() { Gauche = x + 10, Haut = y + 6, Droite = x + l - 10, Bas = y + 30 };
            DrawText(m, _etat, -1, &t, DT_LEFT | DT_UNELIGNE | DT_NOPREFIX);
            haut = y + 30;
        }
        if (StatistiquesVisibles && Statistiques != null)
            DessinerStatistiques(m, Statistiques, x + l - 10, haut + 10, lc, hc);

        BitBlt(hdc, 0, 0, lc, hc, m, 0, 0, Srccopy);
    }

    /// <summary>L'image en mémoire, à la taille de la zone cliente (refaite quand elle change).</summary>
    private IntPtr Tampon(IntPtr hdc, int largeur, int hauteur)
    {
        if (_memoire == IntPtr.Zero)
        {
            _memoire = CreateCompatibleDC(hdc);
            _noir = CreateCompatibleDC(hdc);
            _pixelNoir = CreateCompatibleBitmap(hdc, 1, 1);
            _ancienPixel = SelectObject(_noir, _pixelNoir);
            var un = new Rect { Droite = 1, Bas = 1 };
            FillRect(_noir, &un, GetStockObject(NoirStock));
        }
        if (_tampon == IntPtr.Zero || _largeurTampon != largeur || _hauteurTampon != hauteur)
        {
            if (_tampon != IntPtr.Zero)
            {
                SelectObject(_memoire, _ancienTampon);
                DeleteObject(_tampon);
            }
            _tampon = CreateCompatibleBitmap(hdc, largeur, hauteur);
            _ancienTampon = SelectObject(_memoire, _tampon);
            _largeurTampon = largeur;
            _hauteurTampon = hauteur;
        }
        return _memoire;
    }

    /// <summary>Assombrit un rectangle (opacité du noir : <paramref name="opacite"/> sur 255).</summary>
    private void Voile(IntPtr hdc, int x, int y, int l, int h, byte opacite) =>
        AlphaBlend(hdc, x, y, l, h, _noir, 0, 0, 1, 1, (uint)opacite << 16);      // BLENDFUNCTION : AC_SRC_OVER, alpha constant

    // ---- F2 : les statistiques -----------------------------------------------------------------------

    private const int LargeurPanneau = 340, Marge = 12, Interligne = 19, HauteurEcarts = 64, HauteurDebits = 48;

    /// <summary>Le panneau des statistiques, calé en haut à droite (<paramref name="droite"/>, <paramref name="haut"/>).</summary>
    private void DessinerStatistiques(IntPtr m, Statistiques s, int droite, int haut, int lc, int hc)
    {
        (string mot, int niveau) = s.Verdict();
        string latence = s.Latence >= 0 ? $"{s.Latence} ms" : "—";
        string[] lignes =
        [
            $"Images    {s.ImagesParSeconde,3} /s   pire écart {s.PireEcart:0} ms",
            $"Débit     {s.Debit,5:0.00} Mbit/s",
            $"Ping      {latence}",
            $"Flux      {s.LargeurFlux} × {s.HauteurFlux}  →  {_largeurTampon} × {_hauteurTampon}",
            $"Décodage  {s.Decodage:0.0} ms par image",
            $"Pertes    {s.Perdues} images, {s.DemandesCle} demandes de clé",
            $"Son       {s.SonsJoues} paquets joués, {s.SonsJetes} jetés",
        ];
        if (s.Reconnexions > 0) lignes = [.. lignes, $"Réseau    {s.Reconnexions} reconnexions"];

        int hauteur = Marge + 24 + lignes.Length * Interligne + 8 + (16 + HauteurEcarts) + 8 + (16 + HauteurDebits) + Marge;
        int gauche = Math.Max(0, droite - LargeurPanneau);
        Voile(m, gauche, haut, LargeurPanneau, hauteur, 190);

        int x = gauche + Marge, l = LargeurPanneau - 2 * Marge, y = haut + Marge;
        SelectObject(m, s_PetitePolice);
        SetTextColor(m, niveau switch { 0 => Vert, 1 => Orange, _ => Rouge });
        Ecrire(m, $"Qualité du flux : {mot}", x, y, l, 22);
        y += 24;

        SelectObject(m, s_PoliceStats);
        SetTextColor(m, Blanc);
        foreach (string ligne in lignes)
        {
            Ecrire(m, ligne, x, y, l, Interligne);
            y += Interligne;
        }
        y += 8;

        // L'écart entre deux images : une barre par image, 16,7 ms (60 i/s) et 33,3 ms (30 i/s) en pointillé.
        SelectObject(m, s_PoliceGraphe);
        SetTextColor(m, Gris);
        Ecrire(m, "Écart entre deux images (0 à 50 ms)", x, y, l, 16);
        y += 16;
        const double MaxEcart = 50;
        Cadre(m, x, y, l, HauteurEcarts);
        foreach (double repere in new[] { 1000.0 / 60, 1000.0 / 30 })
            Pointilles(m, x, y + HauteurEcarts - (int)(repere / MaxEcart * HauteurEcarts), l);
        int i = 0;
        double pasEcart = (double)l / Statistiques.NombreEcarts;
        foreach (double e in s.Ecarts)
        {
            int bh = Math.Max(1, (int)(Math.Min(e, MaxEcart) / MaxEcart * HauteurEcarts));
            int bx = x + (int)(i * pasEcart), bl = Math.Max(1, x + (int)((i + 1) * pasEcart) - bx);
            var barre = new Rect { Gauche = bx, Haut = y + HauteurEcarts - bh, Droite = bx + bl, Bas = y + HauteurEcarts };
            FillRect(m, &barre, e <= 20 ? s_BrosseVert : e <= 40 ? s_BrosseOrange : s_BrosseRouge);
            i++;
        }
        y += HauteurEcarts + 8;

        // Le débit, sur 30 s, à l'échelle de son maximum.
        double max = 1;
        foreach (double d in s.Debits) max = Math.Max(max, d);
        max = Math.Ceiling(max * 1.2);
        Ecrire(m, $"Débit reçu, 30 dernières secondes (0 à {max:0} Mbit/s)", x, y, l, 16);
        y += 16;
        Cadre(m, x, y, l, HauteurDebits);
        IntPtr ancien = SelectObject(m, s_CrayonBleu);
        i = 0;
        double pasDebit = (double)l / (Statistiques.NombreDebits - 1);
        foreach (double d in s.Debits)
        {
            int px = x + (int)(i * pasDebit), py = y + HauteurDebits - (int)(d / max * HauteurDebits);
            if (i == 0) MoveToEx(m, px, py, null); else LineTo(m, px, py);
            i++;
        }
        SelectObject(m, ancien);
    }

    private static void Ecrire(IntPtr hdc, string texte, int x, int y, int l, int h)
    {
        var r = new Rect { Gauche = x, Haut = y, Droite = x + l, Bas = y + h };
        DrawText(hdc, texte, -1, &r, DT_LEFT | DT_UNELIGNE | DT_NOPREFIX);
    }

    /// <summary>La ligne de base et le bord gauche d'un graphe.</summary>
    private static void Cadre(IntPtr hdc, int x, int y, int l, int h)
    {
        var bas = new Rect { Gauche = x, Haut = y + h, Droite = x + l, Bas = y + h + 1 };
        FillRect(hdc, &bas, s_BrosseGrille);
        var gauche = new Rect { Gauche = x, Haut = y, Droite = x + 1, Bas = y + h };
        FillRect(hdc, &gauche, s_BrosseGrille);
    }

    private static void Pointilles(IntPtr hdc, int x, int y, int l)
    {
        IntPtr ancien = SelectObject(hdc, s_CrayonGrille);
        MoveToEx(hdc, x, y, null);
        LineTo(hdc, x + l, y);
        SelectObject(hdc, ancien);
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
        bool premiere = ((long)l & 0x40000000) == 0;                    // pas la répétition d'une touche tenue
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
            case Win32.WM_KEYDOWN when f?.Ecran != null && (int)w == VK_F1 && premiere:
                f.Ecran.Basculer();
                InvalidateRect(hwnd, IntPtr.Zero, false);
                return IntPtr.Zero;
            case Win32.WM_KEYDOWN when f != null && (int)w == VK_F2 && premiere:
                f.StatistiquesVisibles = !f.StatistiquesVisibles;
                InvalidateRect(hwnd, IntPtr.Zero, false);
                return IntPtr.Zero;
            case Win32.WM_KEYDOWN when f != null && (int)w == VK_F3 && premiere:
                f.BandeauCache = !f.BandeauCache;
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
        if (_memoire != IntPtr.Zero)
        {
            if (_tampon != IntPtr.Zero) { SelectObject(_memoire, _ancienTampon); DeleteObject(_tampon); }
            SelectObject(_noir, _ancienPixel);
            DeleteObject(_pixelNoir);
            DeleteDC(_noir);
            DeleteDC(_memoire);
            _memoire = _tampon = _noir = _pixelNoir = IntPtr.Zero;
        }
        if (!Fermee) Win32.DestroyWindow(_hwnd);
        s_Instance = null;
    }
}
