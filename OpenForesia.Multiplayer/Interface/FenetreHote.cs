// La fenetre de l'hote (tache 5.1, demandee par Vincent le 2026-09-30) : avant de lancer la partie,
// de quoi inviter ses amis — l'ouverture du port par la box (UPnP, a cocher), le port, l'adresse
// publique, et les deux codes d'invitation (internet, reseau local), chacun avec un bouton Copier.
// Pendant la partie, elle reste ouverte : les codes a portee de main, et qui est connecte.
//
// La recherche de la box part des l'ouverture, en tache de fond ; elle previent la fenetre par un
// message (WM_APP) quand elle a fini. Le code se refait a chaque changement du port ou de l'adresse.
// Win32 natif, comme l'installeur : pas de WinForms (il ne s'elague pas).

using System.Net;
using System.Runtime.InteropServices;
using OpenForesia.Multiplayer.Commun;
using OpenForesia.Multiplayer.Hote;
using static OpenForesia.Multiplayer.Natif.Win32;

namespace OpenForesia.Multiplayer.Interface;

internal sealed class FenetreHote
{
    private const int IdUpnp = 10, IdPort = 11, IdAdresse = 12, IdCopierInternet = 13, IdCopierLocal = 14, IdLancer = 15, IdQuitter = 16, IdQualite = 17;
    private const uint MsgBoxPrete = WM_APP + 1, MsgPortPret = WM_APP + 2;
    private const nuint Minuterie = 1;
    private const int Largeur = 700, Hauteur = 362;          // la zone client, à 96 ppp ; le code (43 caractères) doit tenir en entier

    private static FenetreHote? s_Instance;

    private readonly string? _dossier;
    private readonly byte[] _cle = Invitation.NouvelleCle();
    private readonly IntPtr _police, _policeCode;
    private IntPtr _hwnd, _upnp, _port, _adresse, _qualite, _codeInternet, _codeLocal, _etat, _lancer, _copierInternet, _copierLocal;
    private float _echelle;

    // Ce que la recherche de la box a trouvé — écrit par la tâche de fond, lu après MsgBoxPrete.
    private volatile Upnp? _box;
    private volatile bool _recherche;
    private volatile IPAddress? _publiqueBox;
    private volatile string _etatBox = "";
    private int _portOuvert;

    private Session? _session;
    private bool _fermeture;

    private FenetreHote(string? dossier)
    {
        _dossier = dossier;
        _echelle = GetDpiForSystem() / 96f;
        _police = CreateFont(-Px(12), 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        _policeCode = CreateFont(-Px(13), 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Consolas");
    }

    private int Px(int valeur) => (int)(valeur * _echelle);

    /// <summary>Ouvre la fenêtre et la mène jusqu'à sa fermeture.</summary>
    internal static unsafe int Executer(int port, string? dossierDonne, IPAddress? adresseDonnee, bool upnp)
    {
        var f = new FenetreHote(Jeu.Trouver(dossierDonne));
        s_Instance = f;
        f.Creer(port, adresseDonnee, upnp);

        MSG msg;
        while (GetMessage(&msg, IntPtr.Zero, 0, 0) > 0)
        {
            if (IsDialogMessage(f._hwnd, &msg)) continue;
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
        return 0;
    }

    // ---- la construction ---------------------------------------------------------------------------

    private unsafe void Creer(int port, IPAddress? adresseDonnee, bool upnp)
    {
        const string classe = "OpenForesiaHote";
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
        _hwnd = CreateWindowEx(WS_EX_CONTROLPARENT, classe, "OpenForesia — héberger une partie", style,
            (GetSystemMetrics(0) - l) / 2, (GetSystemMetrics(1) - h) / 2, l, h,
            IntPtr.Zero, IntPtr.Zero, GetModuleHandle(IntPtr.Zero), IntPtr.Zero);

        int x = Px(16), y = Px(16), colonne = Px(170), champ = Px(390);
        Texte("Invite jusqu'à trois amis : envoie-leur un code, ils le saisissent dans « Rejoindre ».", x, y, Px(Largeur - 32), Px(20));
        y += Px(34);
        _upnp = Controle("BUTTON", "Ouvrir le port automatiquement sur ma box (UPnP)", WS_TABSTOP | BS_AUTOCHECKBOX, 0, x, y, Px(420), Px(22), IdUpnp);
        SendMessage(_upnp, BM_SETCHECK, upnp ? 1 : 0, IntPtr.Zero);
        y += Px(32);
        Texte("Port UDP :", x, y + Px(3), colonne - x, Px(20));
        _port = Controle("EDIT", port.ToString(), WS_TABSTOP | ES_NUMBER | ES_AUTOHSCROLL, WS_EX_CLIENTEDGE, colonne, y, Px(80), Px(24), IdPort);
        y += Px(32);
        Texte("Adresse publique :", x, y + Px(3), colonne - x, Px(20));
        _adresse = Controle("EDIT", adresseDonnee?.ToString() ?? "", WS_TABSTOP | ES_AUTOHSCROLL, WS_EX_CLIENTEDGE, colonne, y, Px(160), Px(24), IdAdresse);
        y += Px(32);
        Texte("Qualité de l'image :", x, y + Px(3), colonne - x, Px(20));
        _qualite = Controle("COMBOBOX", "", WS_TABSTOP | CBS_DROPDOWNLIST | WS_VSCROLL, 0, colonne, y, Px(360), Px(200), IdQualite);
        foreach (Qualite q in Qualite.Toutes) SendMessageTexte(_qualite, CB_ADDSTRING, q.Libelle);
        SendMessage(_qualite, CB_SETCURSEL, Array.IndexOf(Qualite.Toutes, Qualite.ParDefaut), IntPtr.Zero);
        y += Px(40);
        Texte("Code (internet) :", x, y + Px(4), colonne - x, Px(20));
        _codeInternet = Controle("EDIT", "", WS_TABSTOP | ES_READONLY | ES_AUTOHSCROLL, WS_EX_CLIENTEDGE, colonne, y, champ + Px(20), Px(26), 0);
        _copierInternet = Controle("BUTTON", "Copier", WS_TABSTOP | BS_PUSHBUTTON, 0, colonne + champ + Px(28), y, Px(84), Px(26), IdCopierInternet);
        y += Px(34);
        Texte("Code (réseau local) :", x, y + Px(4), colonne - x, Px(20));
        _codeLocal = Controle("EDIT", "", WS_TABSTOP | ES_READONLY | ES_AUTOHSCROLL, WS_EX_CLIENTEDGE, colonne, y, champ + Px(20), Px(26), 0);
        _copierLocal = Controle("BUTTON", "Copier", WS_TABSTOP | BS_PUSHBUTTON, 0, colonne + champ + Px(28), y, Px(84), Px(26), IdCopierLocal);
        SendMessage(_codeInternet, WM_SETFONT, _policeCode, 1);
        SendMessage(_codeLocal, WM_SETFONT, _policeCode, 1);
        y += Px(44);
        _etat = Texte("", x, y, Px(Largeur - 32), Px(56));
        _lancer = Controle("BUTTON", "Lancer la partie", WS_TABSTOP | BS_DEFPUSHBUTTON, 0, Px(Largeur) - Px(16) - Px(96) - Px(10) - Px(150), Px(Hauteur) - Px(16) - Px(30), Px(150), Px(30), IdLancer);
        Controle("BUTTON", "Quitter", WS_TABSTOP | BS_PUSHBUTTON, 0, Px(Largeur) - Px(16) - Px(96), Px(Hauteur) - Px(16) - Px(30), Px(96), Px(30), IdQuitter);

        if (_dossier == null)
        {
            EnableWindow(_lancer, false);
            Etat($"Le jeu est introuvable : {Jeu.Executable} doit être à côté de ce programme.");
        }

        RefaireLesCodes();
        ShowWindow(_hwnd, SW_SHOW);
        SetTimer(_hwnd, Minuterie, 500, IntPtr.Zero);
        ChercherLaBox();
    }

    private IntPtr Controle(string classe, string texte, uint style, uint exStyle, int x, int y, int l, int h, int id)
    {
        IntPtr c = CreateWindowEx(exStyle, classe, texte, WS_CHILD | WS_VISIBLE | style, x, y, l, h, _hwnd, id, GetModuleHandle(IntPtr.Zero), IntPtr.Zero);
        SendMessage(c, WM_SETFONT, _police, 1);
        return c;
    }

    private IntPtr Texte(string texte, int x, int y, int l, int h) => Controle("STATIC", texte, SS_LEFT | SS_NOPREFIX, 0, x, y, l, h, 0);

    private void Etat(string texte) => SetWindowText(_etat, texte);

    // ---- les codes ---------------------------------------------------------------------------------

    private int Port => int.TryParse(LireTexte(_port), out int p) && p is >= 1024 and <= 65535 ? p : 0;

    private IPAddress? Adresse =>
        IPAddress.TryParse(LireTexte(_adresse).Trim(), out IPAddress? a) && a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ? a : null;

    private IPAddress? AdresseLocale => _box?.AdresseLocale ?? ModeHote.AdressesLocales().FirstOrDefault();

    private Qualite QualiteChoisie =>
        (int)SendMessage(_qualite, CB_GETCURSEL, 0, IntPtr.Zero) is var i && i >= 0 && i < Qualite.Toutes.Length ? Qualite.Toutes[i] : Qualite.ParDefaut;

    private void RefaireLesCodes()
    {
        int port = Port;
        IPAddress? publique = Adresse, locale = AdresseLocale;
        SetWindowText(_codeInternet, port == 0 ? "— port invalide (1024 à 65535)"
                                   : publique == null ? "— saisis ton adresse publique"
                                   : new Invitation(publique, port, _cle).ToString());
        SetWindowText(_codeLocal, port == 0 || locale == null ? "—" : new Invitation(locale, port, _cle).ToString());
        EnableWindow(_copierInternet, port != 0 && publique != null);
        EnableWindow(_copierLocal, port != 0 && locale != null);
    }

    // ---- la box ------------------------------------------------------------------------------------

    private bool UpnpCoche => SendMessage(_upnp, BM_GETCHECK, 0, IntPtr.Zero) == 1;

    /// <summary>Cherche la box et lui fait ouvrir le port, en tâche de fond ; la fenêtre reçoit MsgBoxPrete.</summary>
    private void ChercherLaBox()
    {
        if (!UpnpCoche || _recherche) { EtatSansUpnp(); return; }
        _recherche = true;
        int port = Port;
        Etat("Recherche de ta box (UPnP)…");
        IntPtr hwnd = _hwnd;
        Task.Run(async () =>
        {
            try
            {
                Upnp? box = _box ?? await Upnp.Trouver();
                string etat;
                IPAddress? publique = null;
                if (box == null)
                    etat = $"Aucune box ne répond en UPnP. Redirige le port UDP {port} de ta box vers ce PC "
                         + $"({AdresseLocale}), puis saisis ton adresse publique.";
                else
                {
                    publique = await box.AdressePublique();
                    bool ouvert = port != 0 && await OuvrirSurLaBox(box, port);
                    etat = !ouvert ? $"Ta box refuse d'ouvrir le port UDP {port} : redirige-le à la main vers {box.AdresseLocale}."
                         : publique != null && Upnp.Privee(publique) ? $"Port ouvert, mais ta box a elle-même une adresse privée ({publique}) : "
                                                                     + "double NAT (4G, certaines fibres). Tes amis hors de ton réseau ne pourront sans doute pas te rejoindre."
                         : $"Box trouvée : port UDP {port} ouvert vers {box.AdresseLocale}. Copie le code internet et envoie-le à tes amis.";
                    if (publique != null && Upnp.Privee(publique)) publique = null;
                }
                _box = box;
                _publiqueBox = publique;
                _etatBox = etat;
            }
            catch (Exception e) { _etatBox = $"La recherche de la box a échoué ({e.Message})."; }
            _recherche = false;
            PostMessage(hwnd, MsgBoxPrete, IntPtr.Zero, IntPtr.Zero);
        });
    }

    /// <summary>Le port ouvert sur la box est celui-ci — l'ancien refermé s'il a changé.</summary>
    private async Task<bool> OuvrirSurLaBox(Upnp box, int port)
    {
        if (_portOuvert == port) return true;
        if (_portOuvert != 0) await box.Fermer();
        bool ouvert = await box.Ouvrir(port);
        _portOuvert = ouvert ? port : 0;
        return ouvert;
    }

    private void BoxPrete()
    {
        if (_publiqueBox != null && Adresse == null) SetWindowText(_adresse, _publiqueBox.ToString());
        Etat(_etatBox);
        RefaireLesCodes();
    }

    private void EtatSansUpnp()
    {
        if (!UpnpCoche) Etat($"UPnP désactivé : redirige le port UDP {Port} de ta box vers ce PC ({AdresseLocale}), puis saisis ton adresse publique.");
    }

    // ---- la partie ---------------------------------------------------------------------------------

    private void Lancer()
    {
        int port = Port;
        if (port == 0 || _dossier == null) { Etat("Le port doit être entre 1024 et 65535."); return; }
        EnableWindow(_lancer, false);
        EnableWindow(_upnp, false);
        EnableWindow(_port, false);
        EnableWindow(_adresse, false);
        EnableWindow(_qualite, false);

        // Le port a pu changer depuis la recherche : la box l'ouvre d'abord, puis la partie démarre.
        if (UpnpCoche && _box is { } box && _portOuvert != port)
        {
            Etat($"Ouverture du port UDP {port} sur ta box…");
            IntPtr hwnd = _hwnd;
            Task.Run(async () =>
            {
                bool ouvert = await OuvrirSurLaBox(box, port);
                _etatBox = ouvert ? "" : $"Ta box refuse d'ouvrir le port UDP {port} : redirige-le à la main.";
                PostMessage(hwnd, MsgPortPret, IntPtr.Zero, IntPtr.Zero);
            });
            return;
        }
        Demarrer();
    }

    private void Demarrer()
    {
        _session = new Session(_dossier!, Port, _cle, QualiteChoisie);
        Etat("Partie lancée. En attente de tes amis…" + (_etatBox.StartsWith("Ta box refuse") ? " " + _etatBox : ""));
    }

    private void Minute()
    {
        if (_session == null) return;
        if (_session.Terminee)
        {
            if (_session.Erreur != null)
                MessageBox(_hwnd, $"La partie s'est interrompue : {_session.Erreur}", "OpenForesia", MB_OK | MB_ICONERROR);
            Fermer();
            return;
        }
        int[] joueurs = _session.Joueurs;
        Etat(joueurs.Length == 0 ? "Partie en cours. En attente de tes amis… Envoie-leur un code."
                                 : $"Partie en cours — {joueurs.Length} ami(s) connecté(s) : joueur(s) {string.Join(", ", joueurs)}. "
                                 + "Garde la fenêtre du jeu au premier plan : c'est là que tes amis jouent.");
    }

    /// <summary>Arrête la partie s'il y en a une, referme le port de la box, et quitte.</summary>
    private void Fermer()
    {
        if (_fermeture) return;
        _fermeture = true;
        _session?.Dispose();
        Upnp? box = _box;
        if (box != null && _portOuvert != 0) Task.Run(() => box.Fermer()).Wait(3000);
        DestroyWindow(_hwnd);
    }

    // ---- les messages ------------------------------------------------------------------------------

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static unsafe IntPtr Procedure(IntPtr hwnd, uint msg, IntPtr w, IntPtr l)
    {
        FenetreHote? f = s_Instance;
        if (f == null || f._hwnd == IntPtr.Zero) return DefWindowProc(hwnd, msg, w, l);
        try
        {
            switch (msg)
            {
                case WM_COMMAND:
                    int id = (int)(w & 0xFFFF), code = (int)((w >> 16) & 0xFFFF);
                    if (code == EN_CHANGE && id is IdPort or IdAdresse) { f.RefaireLesCodes(); return 0; }
                    if (code != BN_CLICKED) break;
                    switch (id)
                    {
                        case IdUpnp: if (f.UpnpCoche) f.ChercherLaBox(); else f.EtatSansUpnp(); return 0;
                        case IdCopierInternet: f.Copie(f._codeInternet); return 0;
                        case IdCopierLocal: f.Copie(f._codeLocal); return 0;
                        case IdLancer: f.Lancer(); return 0;
                        case IdQuitter: PostMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero); return 0;
                    }
                    break;
                case MsgBoxPrete: f.BoxPrete(); return 0;
                case MsgPortPret: f.Demarrer(); return 0;
                case WM_TIMER: f.Minute(); return 0;
                case WM_CTLCOLORSTATIC:
                    SetBkColor(w, GetSysColor(COLOR_BTNFACE));
                    return GetSysColorBrush(COLOR_BTNFACE);
                case WM_CLOSE:
                    if (f._session is { Terminee: false } &&
                        MessageBox(hwnd, "Arrêter la partie ? Tes amis seront déconnectés ; le jeu continue sans eux.",
                                   "OpenForesia", MB_YESNO | MB_ICONWARNING) != IDYES)
                        return 0;
                    f.Fermer();
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

    private void Copie(IntPtr champ)
    {
        string code = LireTexte(champ);
        if (code.StartsWith('—')) return;
        Etat(Copier(_hwnd, code) ? $"Code copié : colle-le dans un message à tes amis ({code})." : "Le presse-papiers est occupé, réessaie.");
    }
}
