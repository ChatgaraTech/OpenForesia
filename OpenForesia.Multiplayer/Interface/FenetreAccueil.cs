// L'ecran d'accueil (tache 5.1) : ce qui s'ouvre quand on lance le multijoueur sans rien — un
// double-clic, le raccourci « OpenForesia Multijoueur ». Deux choix : heberger une partie (la
// fenetre de l'hote), ou rejoindre celle d'un ami avec le code qu'il a envoye. Le dernier code
// saisi est retenu, pour se reconnecter sans le recoller.

using OpenForesia.Multiplayer.Commun;
using static OpenForesia.Multiplayer.Natif.Win32;

namespace OpenForesia.Multiplayer.Interface;

internal sealed class FenetreAccueil
{
    /// <summary>Ce que le joueur a choisi : rien (fenêtre fermée), héberger, ou rejoindre avec un code.</summary>
    internal sealed record Choix(bool Heberger, string? Code = null, bool Reconfigurer = false);

    private const int IdHeberger = 10, IdCode = 11, IdReconfigurer = 12, IdRejoindre = 13;
    private const int Largeur = 620, Hauteur = 250;

    private static FenetreAccueil? s_Instance;

    private readonly float _echelle = GetDpiForSystem() / 96f;
    private readonly IntPtr _police, _policeTitre, _policeCode;
    private IntPtr _hwnd, _code, _reconfigurer, _etat;
    private Choix? _choix;

    private FenetreAccueil()
    {
        _police = CreateFont(-Px(12), 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        _policeTitre = CreateFont(-Px(16), 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        _policeCode = CreateFont(-Px(12), 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Consolas");
    }

    private int Px(int valeur) => (int)(valeur * _echelle);

    private static string CheminDernierCode => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OpenForesia", "dernier-code.txt");

    /// <summary>Ouvre l'accueil et rend le choix du joueur (null s'il a fermé la fenêtre).</summary>
    internal static unsafe Choix? Demander()
    {
        var f = new FenetreAccueil();
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
        return f._choix;
    }

    private unsafe void Creer()
    {
        const string classe = "OpenForesiaAccueil";
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
        _hwnd = CreateWindowEx(WS_EX_CONTROLPARENT, classe, "OpenForesia — multijoueur", style,
            (GetSystemMetrics(0) - l) / 2, (GetSystemMetrics(1) - h) / 2, l, h,
            IntPtr.Zero, IntPtr.Zero, GetModuleHandle(IntPtr.Zero), IntPtr.Zero);

        int moitie = Px(Largeur / 2), marge = Px(20), largeurColonne = moitie - 2 * marge;

        // Héberger, à gauche.
        Controle("STATIC", "Héberger", SS_LEFT, 0, marge, marge, largeurColonne, Px(26), 0, _policeTitre);
        Controle("STATIC", "Tu joues sur ce PC ; jusqu'à trois amis te rejoignent avec un code que tu leur envoies.",
                 SS_LEFT | SS_NOPREFIX, 0, marge, marge + Px(34), largeurColonne, Px(60), 0, _police);
        Controle("BUTTON", "Héberger une partie", WS_TABSTOP | BS_PUSHBUTTON, 0, marge, marge + Px(120), Px(200), Px(32), IdHeberger, _police);

        // Rejoindre, à droite.
        int x = moitie + marge;
        Controle("STATIC", "Rejoindre", SS_LEFT, 0, x, marge, largeurColonne, Px(26), 0, _policeTitre);
        Controle("STATIC", "Un ami t'a envoyé un code :", SS_LEFT | SS_NOPREFIX, 0, x, marge + Px(34), largeurColonne, Px(20), 0, _police);
        string dernier = File.Exists(CheminDernierCode) ? File.ReadAllText(CheminDernierCode).Trim() : "";
        _code = Controle("EDIT", dernier, WS_TABSTOP | ES_AUTOHSCROLL, WS_EX_CLIENTEDGE, x, marge + Px(56), largeurColonne, Px(26), IdCode, _policeCode);
        _reconfigurer = Controle("BUTTON", "Reconfigurer ma manette", WS_TABSTOP | BS_AUTOCHECKBOX, 0, x, marge + Px(90), largeurColonne, Px(22), IdReconfigurer, _police);
        Controle("BUTTON", "Rejoindre", WS_TABSTOP | BS_DEFPUSHBUTTON, 0, x, marge + Px(120), Px(140), Px(32), IdRejoindre, _police);

        _etat = Controle("STATIC", "", SS_LEFT | SS_NOPREFIX, 0, marge, Px(Hauteur) - marge - Px(24), Px(Largeur) - 2 * marge, Px(24), 0, _police);
        ShowWindow(_hwnd, SW_SHOW);
    }

    private IntPtr Controle(string classe, string texte, uint style, uint exStyle, int x, int y, int l, int h, int id, IntPtr police)
    {
        IntPtr c = CreateWindowEx(exStyle, classe, texte, WS_CHILD | WS_VISIBLE | style, x, y, l, h, _hwnd, id, GetModuleHandle(IntPtr.Zero), IntPtr.Zero);
        SendMessage(c, WM_SETFONT, police, 1);
        return c;
    }

    private void Rejoindre()
    {
        string code = LireTexte(_code).Trim();
        if (Invitation.Lire(code) == null)
        {
            SetWindowText(_etat, code.Length == 0 ? "Colle d'abord le code que ton ami t'a envoyé."
                                                  : "Ce n'est pas un code d'invitation : vérifie-le, ou redemande-le à ton ami.");
            return;
        }
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(CheminDernierCode)!);
            File.WriteAllText(CheminDernierCode, code);
        }
        catch (IOException) { }                           // ne pas le retenir n'empêche pas de jouer
        bool reconfigurer = SendMessage(_reconfigurer, BM_GETCHECK, 0, IntPtr.Zero) == 1;
        Terminer(new Choix(false, code, reconfigurer));
    }

    private void Terminer(Choix choix)
    {
        _choix = choix;
        DestroyWindow(_hwnd);
    }

    [System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static IntPtr Procedure(IntPtr hwnd, uint msg, IntPtr w, IntPtr l)
    {
        FenetreAccueil? f = s_Instance;
        if (f == null || f._hwnd == IntPtr.Zero) return DefWindowProc(hwnd, msg, w, l);
        switch (msg)
        {
            case WM_COMMAND when (int)((w >> 16) & 0xFFFF) == BN_CLICKED:
                switch ((int)(w & 0xFFFF))
                {
                    case IdHeberger: f.Terminer(new Choix(true)); return 0;
                    case IdRejoindre: f.Rejoindre(); return 0;
                }
                break;
            case WM_CTLCOLORSTATIC:
                SetBkColor(w, GetSysColor(COLOR_BTNFACE));
                return GetSysColorBrush(COLOR_BTNFACE);
            case WM_CLOSE:
                DestroyWindow(hwnd);
                return 0;
            case WM_DESTROY:
                PostQuitMessage(0);
                return 0;
        }
        return DefWindowProc(hwnd, msg, w, l);
    }
}
