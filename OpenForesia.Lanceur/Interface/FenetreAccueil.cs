// L'ecran d'accueil (add-multiplayer-streaming tache 5.1 ; add-launcher-solo-gamepad, design D5, D8) :
// ce qui s'ouvre quand on lance le lanceur sans rien — un double-clic, le raccourci « OpenForesia
// Lanceur ». Quatre choix : jouer seul (la manette du joueur 1 relayee), heberger une partie (la
// fenetre de l'hote), rejoindre celle d'un ami avec le code qu'il a envoye, et configurer sa
// manette. Le dernier code saisi est retenu, pour se reconnecter sans le recoller.
//
// Sans le jeu a cote (un ami qui n'a recu que le lanceur), l'accueil se resserre autour de
// Rejoindre : jouer seul et heberger ne sont pas affiches.

using OpenForesia.Lanceur.Commun;
using OpenForesia.Lanceur.Invite;
using static OpenForesia.Lanceur.Natif.Win32;

namespace OpenForesia.Lanceur.Interface;

internal sealed class FenetreAccueil
{
    internal enum Mode { Solo, Heberger, Rejoindre }

    /// <summary>Ce que le joueur a choisi : rien (fenêtre fermée), jouer seul, héberger, ou rejoindre avec un code.</summary>
    internal sealed record Choix(Mode Mode, string? Code = null);

    // La case « Reconfigurer ma manette » (12) est retirée : le bouton « Configurer ma manette »
    // ouvre le même écran sur le même manette.json, à tout moment.
    private const int IdHeberger = 10, IdCode = 11, IdRejoindre = 13, IdSolo = 14, IdManette = 15;
    // Réduite, l'état de la manette n'a pas la place à droite du bouton (« Manette : Manette
    // Xbox 1. » y passait à la ligne) : il descend dessous, et la manette remonte sous Rejoindre.
    private const int LargeurComplete = 720, LargeurReduite = 380, HauteurComplete = 300, HauteurReduite = 290;
    private const nuint Minuterie = 1;

    private static FenetreAccueil? s_Instance;

    private readonly bool _jeu;
    private readonly int _largeur, _hauteur;
    private readonly ConfigManette _config = ConfigManette.Charger() ?? new ConfigManette();
    private readonly float _echelle = GetDpiForSystem() / 96f;
    private readonly IntPtr _police, _policeTitre, _policeCode;
    private IntPtr _hwnd, _code, _etat, _manette;
    private Choix? _choix;

    private FenetreAccueil(bool jeu)
    {
        _jeu = jeu;
        _largeur = jeu ? LargeurComplete : LargeurReduite;
        _hauteur = jeu ? HauteurComplete : HauteurReduite;
        _police = CreateFont(-Px(12), 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        _policeTitre = CreateFont(-Px(16), 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        _policeCode = CreateFont(-Px(12), 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Consolas");
    }

    private int Px(int valeur) => (int)(valeur * _echelle);

    private static string CheminDernierCode => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OpenForesia", "dernier-code.txt");

    /// <summary>
    /// Ouvre l'accueil et rend le choix du joueur (null s'il a fermé la fenêtre). Sans
    /// <paramref name="jeu"/> (le jeu n'est pas à côté), seuls Rejoindre et la manette sont offerts.
    /// </summary>
    internal static unsafe Choix? Demander(bool jeu)
    {
        var f = new FenetreAccueil(jeu);
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
        var r = new RECT { Droite = Px(_largeur), Bas = Px(_hauteur) };
        AdjustWindowRectEx(&r, style, false, WS_EX_CONTROLPARENT);
        int l = r.Droite - r.Gauche, h = r.Bas - r.Haut;
        _hwnd = CreateWindowEx(WS_EX_CONTROLPARENT, classe, "OpenForesia", style,
            (GetSystemMetrics(0) - l) / 2, (GetSystemMetrics(1) - h) / 2, l, h,
            IntPtr.Zero, IntPtr.Zero, GetModuleHandle(IntPtr.Zero), IntPtr.Zero);

        int marge = Px(20), largeurColonne = Px(LargeurComplete / 2) - 2 * marge;
        int x = marge;

        // Jouer, à gauche — seulement si le jeu est à côté.
        if (_jeu)
        {
            Controle("STATIC", "Jouer", SS_LEFT, 0, marge, marge, largeurColonne, Px(26), 0, _policeTitre);
            Controle("STATIC", "Sur ce PC, au clavier ou à la manette. En hébergeant, jusqu'à trois amis te rejoignent avec un code que tu leur envoies.",
                     SS_LEFT | SS_NOPREFIX, 0, marge, marge + Px(34), largeurColonne, Px(60), 0, _police);
            Controle("BUTTON", "Jouer seul", WS_TABSTOP | BS_DEFPUSHBUTTON, 0, marge, marge + Px(100), Px(200), Px(32), IdSolo, _police);
            Controle("BUTTON", "Héberger une partie", WS_TABSTOP | BS_PUSHBUTTON, 0, marge, marge + Px(140), Px(200), Px(32), IdHeberger, _police);
            x = Px(LargeurComplete / 2) + marge;
        }

        // Rejoindre, à droite (ou seul).
        // La colonne va jusqu'à la marge de droite, quelle que soit la largeur de la fenêtre.
        int largeurRejoindre = Px(_largeur) - x - marge;
        Controle("STATIC", "Rejoindre", SS_LEFT, 0, x, marge, largeurRejoindre, Px(26), 0, _policeTitre);
        Controle("STATIC", "Un ami t'a envoyé un code :", SS_LEFT | SS_NOPREFIX, 0, x, marge + Px(34), largeurRejoindre, Px(20), 0, _police);
        string dernier = File.Exists(CheminDernierCode) ? File.ReadAllText(CheminDernierCode).Trim() : "";
        _code = Controle("EDIT", dernier, WS_TABSTOP | ES_AUTOHSCROLL, WS_EX_CLIENTEDGE, x, marge + Px(56), largeurRejoindre, Px(26), IdCode, _policeCode);
        Controle("BUTTON", "Rejoindre", WS_TABSTOP | (_jeu ? BS_PUSHBUTTON : BS_DEFPUSHBUTTON), 0, x, marge + Px(92), Px(140), Px(32), IdRejoindre, _police);

        // La manette, en bas, pour tous les choix.
        // Sous la colonne Jouer quand elle est là ; sinon juste sous le bouton Rejoindre.
        int y = marge + Px(_jeu ? 194 : 150);
        Controle("BUTTON", "Configurer ma manette", WS_TABSTOP | BS_PUSHBUTTON, 0, marge, y, Px(200), Px(30), IdManette, _police);
        // L'état de la manette : à droite du bouton quand la fenêtre est large, dessous sinon.
        if (_jeu)
            _manette = Controle("STATIC", "", SS_LEFT | SS_NOPREFIX, 0, marge + Px(212), y + Px(6), Px(_largeur) - 2 * marge - Px(212), Px(20), 0, _police);
        else
            _manette = Controle("STATIC", "", SS_LEFT | SS_NOPREFIX, 0, marge, y + Px(38), Px(_largeur) - 2 * marge, Px(20), 0, _police);

        _etat = Controle("STATIC", "", SS_LEFT | SS_NOPREFIX, 0, marge, Px(_hauteur) - marge - Px(24), Px(_largeur) - 2 * marge, Px(24), 0, _police);
        EtatManette();
        ShowWindow(_hwnd, SW_SHOW);
        SetTimer(_hwnd, Minuterie, 1000, IntPtr.Zero);
    }

    private IntPtr Controle(string classe, string texte, uint style, uint exStyle, int x, int y, int l, int h, int id, IntPtr police)
    {
        IntPtr c = CreateWindowEx(exStyle, classe, texte, WS_CHILD | WS_VISIBLE | style, x, y, l, h, _hwnd, id, GetModuleHandle(IntPtr.Zero), IntPtr.Zero);
        SendMessage(c, WM_SETFONT, police, 1);
        return c;
    }

    private void EtatManette() => SetWindowText(_manette, ConfigurationManette.Etat(_config));

    private void Configurer()
    {
        if (!ConfigurationManette.Ouvrir(_hwnd, _config)) return;
        EtatManette();
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
        Terminer(new Choix(Mode.Rejoindre, code));
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
                    case IdSolo: f.Terminer(new Choix(Mode.Solo)); return 0;
                    case IdHeberger: f.Terminer(new Choix(Mode.Heberger)); return 0;
                    case IdRejoindre: f.Rejoindre(); return 0;
                    case IdManette: f.Configurer(); return 0;
                }
                break;
            case WM_TIMER:
                f.EtatManette();
                return 0;
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
