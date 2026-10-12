// La fenetre de la partie solo (openspec/changes/add-launcher-solo-gamepad, design D5) : elle dit que
// la partie est en cours et si la manette est vue, porte « Configurer ma manette », et se ferme avec
// le jeu. La fermer pendant la partie arrete le relais : le jeu continue au clavier.
// Win32 natif, comme le reste du lanceur : pas de WinForms (il ne s'elague pas).

using System.Runtime.InteropServices;
using OpenForesia.Lanceur.Hote;
using OpenForesia.Lanceur.Invite;
using static OpenForesia.Lanceur.Natif.Win32;

namespace OpenForesia.Lanceur.Interface;

internal sealed class FenetreSolo
{
    private const int IdManette = 10, IdFermer = 11;
    private const nuint Minuterie = 1;
    private const int Largeur = 480, Hauteur = 150;

    private static FenetreSolo? s_Instance;

    private readonly Jeu _jeu;
    private readonly Relais _relais;
    private readonly ConfigManette _config;
    private readonly float _echelle = GetDpiForSystem() / 96f;
    private readonly IntPtr _police;
    private IntPtr _hwnd, _etat;

    private FenetreSolo(Jeu jeu, Relais relais, ConfigManette config)
    {
        _jeu = jeu;
        _relais = relais;
        _config = config;
        _police = CreateFont(-Px(12), 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
    }

    private int Px(int valeur) => (int)(valeur * _echelle);

    /// <summary>Ouvre la fenêtre et la mène jusqu'à sa fermeture : par le joueur, ou avec le jeu.</summary>
    internal static unsafe void Executer(Jeu jeu, Relais relais, ConfigManette config)
    {
        var f = new FenetreSolo(jeu, relais, config);
        s_Instance = f;
        f.Creer();

        MSG msg;
        while (GetMessage(&msg, IntPtr.Zero, 0, 0) > 0)
        {
            if (IsDialogMessage(f._hwnd, &msg)) continue;
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
        s_Instance = null;
    }

    private unsafe void Creer()
    {
        const string classe = "OpenForesiaSolo";
        fixed (char* nom = classe)
        {
            var wc = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                lpfnWndProc = &Procedure,
                hInstance = GetModuleHandle(IntPtr.Zero),
                hCursor = LoadCursor(IntPtr.Zero, 32512),
                hIcon = LoadIcon(IntPtr.Zero, 32512),
                hbrBackground = COLOR_BTNFACE + 1,
                lpszClassName = nom,
            };
            RegisterClassEx(&wc);
        }

        const uint style = WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX;
        var r = new RECT { Droite = Px(Largeur), Bas = Px(Hauteur) };
        AdjustWindowRectEx(&r, style, false, WS_EX_CONTROLPARENT);
        int l = r.Droite - r.Gauche, h = r.Bas - r.Haut;
        _hwnd = CreateWindowEx(WS_EX_CONTROLPARENT, classe, "OpenForesia — partie en cours", style,
            (GetSystemMetrics(0) - l) / 2, (GetSystemMetrics(1) - h) / 2, l, h,
            IntPtr.Zero, IntPtr.Zero, GetModuleHandle(IntPtr.Zero), IntPtr.Zero);

        int x = Px(16), y = Px(16), largeur = Px(Largeur - 32);
        Controle("STATIC", "Partie en cours. Ta manette joue tant que la fenêtre du jeu est au premier plan ; ton clavier marche aussi.",
                 SS_LEFT | SS_NOPREFIX, 0, x, y, largeur, Px(40), 0);
        y += Px(48);
        _etat = Controle("STATIC", "", SS_LEFT | SS_NOPREFIX, 0, x, y, largeur, Px(20), 0);
        int bas = Px(Hauteur) - Px(16) - Px(30);
        Controle("BUTTON", "Configurer ma manette", WS_TABSTOP | BS_PUSHBUTTON, 0, x, bas, Px(200), Px(30), IdManette);
        Controle("BUTTON", "Fermer", WS_TABSTOP | BS_PUSHBUTTON, 0, Px(Largeur) - Px(16) - Px(96), bas, Px(96), Px(30), IdFermer);

        Minute();
        // Sans voler le premier plan au jeu, qui démarre en même temps.
        ShowWindow(_hwnd, SW_SHOWNOACTIVATE);
        SetTimer(_hwnd, Minuterie, 500, IntPtr.Zero);
    }

    private const int SW_SHOWNOACTIVATE = 4;

    private IntPtr Controle(string classe, string texte, uint style, uint exStyle, int x, int y, int l, int h, int id)
    {
        IntPtr c = CreateWindowEx(exStyle, classe, texte, WS_CHILD | WS_VISIBLE | style, x, y, l, h, _hwnd, id, GetModuleHandle(IntPtr.Zero), IntPtr.Zero);
        SendMessage(c, WM_SETFONT, _police, 1);
        return c;
    }

    private void Minute()
    {
        if (_jeu.Termine) { DestroyWindow(_hwnd); return; }
        string etat = ConfigurationManette.Etat(_config);
        SetWindowText(_etat, _relais.Actif ? etat : "Manette arrêtée : le jeu continue au clavier.");
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static IntPtr Procedure(IntPtr hwnd, uint msg, IntPtr w, IntPtr l)
    {
        FenetreSolo? f = s_Instance;
        if (f == null || f._hwnd == IntPtr.Zero) return DefWindowProc(hwnd, msg, w, l);
        try
        {
            switch (msg)
            {
                case WM_COMMAND when (int)((w >> 16) & 0xFFFF) == BN_CLICKED:
                    switch ((int)(w & 0xFFFF))
                    {
                        case IdManette:
                            if (ConfigurationManette.Ouvrir(hwnd, f._config, f._relais)) f.Minute();
                            return 0;
                        case IdFermer:
                            PostMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            return 0;
                    }
                    break;
                case WM_TIMER:
                    f.Minute();
                    return 0;
                case WM_CTLCOLORSTATIC:
                    SetBkColor(w, GetSysColor(COLOR_BTNFACE));
                    return GetSysColorBrush(COLOR_BTNFACE);
                case WM_CLOSE:
                    if (!f._jeu.Termine &&
                        MessageBox(hwnd, "Fermer ? La manette s'arrête ; le jeu continue au clavier.",
                                   "OpenForesia", MB_YESNO | MB_ICONWARNING) != IDYES)
                        return 0;
                    DestroyWindow(hwnd);
                    return 0;
                case WM_DESTROY:
                    PostQuitMessage(0);
                    return 0;
            }
        }
        catch (Exception e)
        {
            MessageBox(hwnd, e.Message, "OpenForesia", MB_OK | MB_ICONERROR);
        }
        return DefWindowProc(hwnd, msg, w, l);
    }
}
