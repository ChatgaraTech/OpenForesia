// La fenetre d'installation : quatre etapes — l'image du disque, le dossier, l'installation, la fin.
//
// Pensee pour un joueur (regle des outils pour joueurs, voir OpenForesia.Tools/Html/LISEZMOI.md) :
// des phrases courtes, aucun terme technique, et rien a l'ecran qui ne serve a ce moment-la.
//
// Faite de controles Win32 natifs et non de WinForms (2026-09-26) : WinForms ne s'elague pas et
// pesait une quarantaine de Mo dans l'installeur publie. La disposition, les textes et les couleurs
// sont ceux de la premiere version WinForms ; les coordonnees sont en pixels a 96 ppp, mises a
// l'echelle de l'ecran. Les travaux longs (reconnaissance de l'image, installation) tournent hors du
// fil de la fenetre et lui repondent par PostMessage.

using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenForesia.Installer.Editions;
using OpenForesia.Installer.Installation;
using OpenForesia.Installer.Iso;
using static OpenForesia.Installer.Interface.Natif;
using Inst = OpenForesia.Installer.Installation.Installation;

namespace OpenForesia.Installer.Interface;

internal static unsafe class FenetreInstallation
{
    private enum Etape { Image, Dossier, Installation, Fin }

    private const string Titre = "Installation d'OpenForesia";

    private const int IdParcourirImage = 101, IdParcourirDossier = 102, IdCheminDossier = 103,
                      IdPrecedent = 110, IdSuivant = 111, IdAnnuler = 112, IdLancer = 113;
    private const uint MsgReconnaissance = WM_APP + 1, MsgAvancement = WM_APP + 2, MsgInstallationFinie = WM_APP + 3;

    private static readonly uint Vert = Rgb(0, 100, 0), Rouge = Rgb(178, 34, 34);

    // --- etat ---------------------------------------------------------------------------------------
    private static SourceDuJeu s_Jeu = null!;
    private static CatalogueEditions s_Catalogue = null!;
    private static ImageIso? s_Image;
    private static Edition? s_Edition;
    private static Etape s_Etape;
    private static CancellationTokenSource? s_Annulation;
    private static bool s_FermerApresAnnulation;
    private static string? s_CheminInstalle;

    // Les reponses des travaux de fond, lues par le fil de la fenetre a reception de leur message.
    private static (ImageIso? Image, Edition? Edition, string? Erreur) s_Reconnaissance;
    private static Avancement? s_Avancement;
    private static int s_AvancementEnAttente;
    private static (bool Annulee, string? Erreur) s_FinInstallation;

    // --- fenetre et controles -----------------------------------------------------------------------
    private static IntPtr s_Fenetre, s_Instance, s_Police, s_PoliceTitre;
    private static uint s_Dpi = 96;
    private static IntPtr s_Titre, s_CheminImage, s_ParcourirImage, s_EtatImage;
    private static IntPtr s_CheminDossier, s_ParcourirDossier, s_Espace, s_EtatDossier;
    private static IntPtr s_Barre, s_EtapeInstallation, s_Lancer;
    private static IntPtr s_Precedent, s_Suivant, s_Annuler;
    private static readonly Dictionary<Etape, List<IntPtr>> s_Pages = [];
    private static readonly Dictionary<IntPtr, uint> s_Couleurs = [];

    internal static void Executer(SourceDuJeu jeu)
    {
        s_Jeu = jeu;
        s_Catalogue = CatalogueEditions.Embarque();
        s_Instance = GetModuleHandle(IntPtr.Zero);
        s_Dpi = GetDpiForSystem();

        var init = new INITCOMMONCONTROLSEX { dwSize = (uint)sizeof(INITCOMMONCONTROLSEX), dwICC = 0x20 /* ICC_PROGRESS_CLASS */ };
        InitCommonControlsEx(&init);

        s_Police = CreateFont(-(int)(9 * s_Dpi / 72), 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        s_PoliceTitre = CreateFont(-(int)(14 * s_Dpi / 72), 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");

        fixed (char* nomClasse = "OpenForesiaInstallation")
        {
            var classe = new WNDCLASSEXW
            {
                cbSize = (uint)sizeof(WNDCLASSEXW),
                lpfnWndProc = &Procedure,
                hInstance = s_Instance,
                hCursor = LoadCursor(IntPtr.Zero, 32512 /* IDC_ARROW */),
                hIcon = LoadIcon(IntPtr.Zero, 32512 /* IDI_APPLICATION */),
                hbrBackground = COLOR_BTNFACE + 1,
                lpszClassName = nomClasse,
            };
            RegisterClassEx(&classe);
        }

        const uint style = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX;
        var cadre = new RECT { right = E(560), bottom = E(330) };
        AdjustWindowRectEx(&cadre, style, false, WS_EX_CONTROLPARENT);
        int largeur = cadre.right - cadre.left, hauteur = cadre.bottom - cadre.top;
        s_Fenetre = CreateWindowEx(WS_EX_CONTROLPARENT, "OpenForesiaInstallation", Titre, style,
            (GetSystemMetrics(0) - largeur) / 2, (GetSystemMetrics(1) - hauteur) / 2, largeur, hauteur,
            IntPtr.Zero, IntPtr.Zero, s_Instance, IntPtr.Zero);

        CreerControles();
        SetWindowText(s_CheminDossier, Inst.DossierExistant() ?? Inst.DossierParDefaut);
        Afficher(Etape.Image);
        ShowWindow(s_Fenetre, SW_SHOW);

        MSG msg;
        while (GetMessage(&msg, IntPtr.Zero, 0, 0) > 0)
        {
            if (IsDialogMessage(s_Fenetre, &msg)) continue;   // Tab, Entree, Echap
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    /// <summary>Une dimension a 96 ppp, mise a l'echelle de l'ecran.</summary>
    private static int E(int valeur) => (int)(valeur * s_Dpi / 96);

    private static IntPtr Controle(string classe, string texte, uint style, int x, int y, int l, int h,
        int id = 0, uint exStyle = 0, IntPtr police = default)
    {
        IntPtr hwnd = CreateWindowEx(exStyle, classe, texte, WS_CHILD | WS_VISIBLE | style,
            E(x), E(y), E(l), E(h), s_Fenetre, id, s_Instance, IntPtr.Zero);
        SendMessage(hwnd, WM_SETFONT, police == default ? s_Police : police, 1);
        return hwnd;
    }

    private static IntPtr Texte(string texte, int x, int y, int l, int h) =>
        Controle("STATIC", texte, SS_LEFT | SS_NOPREFIX, x, y, l, h);

    private static IntPtr Bouton(string texte, int x, int y, int l, int h, int id, uint style = BS_PUSHBUTTON) =>
        Controle("BUTTON", texte, style | WS_TABSTOP, x, y, l, h, id);

    private static void CreerControles()
    {
        s_Titre = Controle("STATIC", "", SS_LEFT | SS_NOPREFIX, 24, 18, 512, 32, police: s_PoliceTitre);

        // Les pages sont a (24, 64) ; les positions ci-dessous sont celles de la version WinForms,
        // decalees d'autant.
        const string introImage = "OpenForesia a besoin des données du jeu d'origine, Blaze & Blade (1998). " +
                                  "Choisissez l'image ISO de votre disque.";
        s_Pages[Etape.Image] =
        [
            Texte(introImage, 24, 64, 512, 44),
            s_CheminImage = Controle("EDIT", "", ES_AUTOHSCROLL | ES_READONLY | WS_TABSTOP, 24, 134, 400, 23, exStyle: WS_EX_CLIENTEDGE),
            s_ParcourirImage = Bouton("Parcourir…", 436, 133, 100, 26, IdParcourirImage),
            s_EtatImage = Texte("", 24, 172, 512, 60),
        ];
        s_Pages[Etape.Dossier] =
        [
            Texte("OpenForesia sera installé dans ce dossier. Vos sauvegardes y seront aussi enregistrées.", 24, 64, 512, 44),
            s_CheminDossier = Controle("EDIT", "", ES_AUTOHSCROLL | WS_TABSTOP, 24, 134, 400, 23, IdCheminDossier, WS_EX_CLIENTEDGE),
            s_ParcourirDossier = Bouton("Parcourir…", 436, 133, 100, 26, IdParcourirDossier),
            s_Espace = Texte("", 24, 170, 512, 20),
            s_EtatDossier = Texte("", 24, 196, 512, 60),
        ];
        s_Pages[Etape.Installation] =
        [
            Texte("Patientez pendant l'installation. Cela ne prend que quelques secondes.", 24, 64, 512, 44),
            s_Barre = Controle("msctls_progress32", "", 0, 24, 124, 512, 22),
            s_EtapeInstallation = Texte("", 24, 156, 512, 20),
        ];
        s_Pages[Etape.Fin] =
        [
            Texte("OpenForesia est installé. Un raccourci a été ajouté sur le bureau et dans le menu Démarrer.", 24, 64, 512, 44),
            s_Lancer = Bouton("Lancer OpenForesia", 24, 158, 300, 22, IdLancer, BS_AUTOCHECKBOX),
        ];
        SendMessage(s_Lancer, BM_SETCHECK, 1, 0);
        SendMessage(s_Barre, PBM_SETRANGE32, 0, 100);

        Controle("STATIC", "", SS_ETCHEDHORZ, 0, 272, 560, 2);
        s_Precedent = Bouton("< Précédent", 228, 288, 100, 28, IdPrecedent);
        s_Suivant = Bouton("Suivant >", 336, 288, 100, 28, IdSuivant, BS_DEFPUSHBUTTON);
        s_Annuler = Bouton("Annuler", 448, 288, 100, 28, IdAnnuler);
    }

    // --- procedure de fenetre -----------------------------------------------------------------------

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static IntPtr Procedure(IntPtr hwnd, uint msg, IntPtr w, IntPtr l)
    {
        try
        {
            switch (msg)
            {
                case WM_COMMAND:
                    Commande((int)(w & 0xffff), (int)((w >> 16) & 0xffff));
                    return 0;

                case WM_CTLCOLORSTATIC:
                    SetTextColor(w, s_Couleurs.TryGetValue(l, out uint couleur) ? couleur : GetSysColor(18 /* COLOR_BTNTEXT */));
                    SetBkColor(w, GetSysColor(COLOR_BTNFACE));
                    return GetSysColorBrush(COLOR_BTNFACE);

                case MsgReconnaissance: FinReconnaissance(); return 0;
                case MsgAvancement: MontrerAvancement(); return 0;
                case MsgInstallationFinie: FinInstallation(); return 0;

                case WM_CLOSE:
                    Fermeture();
                    return 0;

                case WM_DESTROY:
                    s_Image?.Dispose();
                    PostQuitMessage(0);
                    return 0;
            }
        }
        catch (Exception e)
        {
            // Une exception ne doit pas traverser la frontiere native : elle tuerait le processus
            // sans un mot.
            MessageBox(s_Fenetre, "Une erreur inattendue est survenue :\n\n" + e.Message, Titre, MB_OK | MB_ICONERROR);
        }
        return DefWindowProc(hwnd, msg, w, l);
    }

    private static void Commande(int id, int notification)
    {
        switch (id)
        {
            case IdParcourirImage when notification == BN_CLICKED: ChoisirImage(); break;
            case IdParcourirDossier when notification == BN_CLICKED: ChoisirDossier(); break;
            case IdCheminDossier when notification == EN_CHANGE: VerifierDossier(); break;
            case IdPrecedent: Afficher(s_Etape - 1); break;
            case IdSuivant:
            case IDOK:
                if (s_SuivantActif && s_Etape != Etape.Installation) Suivant();
                break;
            case IdAnnuler:
            case IDCANCEL:
                Annuler();
                break;
        }
    }

    // --- navigation ---------------------------------------------------------------------------------

    private static bool s_SuivantActif;

    private static void ActiverSuivant(bool actif)
    {
        s_SuivantActif = actif;
        EnableWindow(s_Suivant, actif);
    }

    private static void Etat(IntPtr etiquette, string texte, uint couleur)
    {
        s_Couleurs[etiquette] = couleur;
        SetWindowText(etiquette, texte);
    }

    private static void Afficher(Etape etape)
    {
        s_Etape = etape;
        foreach (var (page, controles) in s_Pages)
            foreach (IntPtr controle in controles)
                ShowWindow(controle, page == etape ? SW_SHOW : SW_HIDE);

        SetWindowText(s_Titre, etape switch
        {
            Etape.Image => "Le disque du jeu",
            Etape.Dossier => "Où installer OpenForesia ?",
            Etape.Installation => "Installation en cours",
            _ => "C'est prêt",
        });

        ShowWindow(s_Precedent, etape == Etape.Dossier ? SW_SHOW : SW_HIDE);
        SetWindowText(s_Suivant, etape switch { Etape.Dossier => "Installer", Etape.Fin => "Terminer", _ => "Suivant >" });
        ShowWindow(s_Suivant, etape != Etape.Installation ? SW_SHOW : SW_HIDE);
        ShowWindow(s_Annuler, etape != Etape.Fin ? SW_SHOW : SW_HIDE);
        ActiverSuivant(etape switch
        {
            Etape.Image => s_Edition != null,
            Etape.Dossier => VerifierDossier(),
            _ => true,
        });
        SetFocus(etape == Etape.Installation ? s_Annuler : s_Suivant);
    }

    private static void Suivant()
    {
        switch (s_Etape)
        {
            case Etape.Image: Afficher(Etape.Dossier); break;
            case Etape.Dossier: LancerInstallation(); break;
            case Etape.Fin: Terminer(); break;
        }
    }

    // --- etape 1 : l'image --------------------------------------------------------------------------

    private static void ChoisirImage()
    {
        string? chemin = Com.ChoisirFichier(s_Fenetre, "Choisir l'image du disque",
            [("Image de disque (*.iso)", "*.iso"), ("Tous les fichiers (*.*)", "*.*")]);
        if (chemin == null) return;

        s_Image?.Dispose();
        s_Image = null;
        s_Edition = null;
        SetWindowText(s_CheminImage, chemin);
        Etat(s_EtatImage, "Vérification de l'image…", GetSysColor(18));
        EnableWindow(s_ParcourirImage, false);
        ActiverSuivant(false);

        CatalogueEditions catalogue = s_Catalogue;
        Task.Run(() =>
        {
            try
            {
                ImageIso image = ImageIso.Ouvrir(chemin);
                try { s_Reconnaissance = (image, catalogue.Reconnaitre(image), null); }
                catch { image.Dispose(); throw; }
            }
            catch (ImageIsoInvalideException e) { s_Reconnaissance = (null, null, e.Message); }
            catch (Exception e) when (e is IOException or UnauthorizedAccessException)
            {
                s_Reconnaissance = (null, null, "Ce fichier ne peut pas être lu : " + e.Message);
            }
            PostMessage(s_Fenetre, MsgReconnaissance, 0, 0);
        });
    }

    private static void FinReconnaissance()
    {
        (s_Image, s_Edition, string? erreur) = s_Reconnaissance;
        if (s_Edition != null) Etat(s_EtatImage, $"✓  {s_Edition.Nom} — image reconnue.", Vert);
        else Etat(s_EtatImage, erreur ?? "", Rouge);
        EnableWindow(s_ParcourirImage, true);
        if (s_Etape == Etape.Image) ActiverSuivant(s_Edition != null);
    }

    // --- etape 2 : le dossier -----------------------------------------------------------------------

    private static void ChoisirDossier()
    {
        string? dossier = Com.ChoisirDossier(s_Fenetre, "Choisir le dossier d'installation");
        if (dossier == null) return;

        // Comme tout installeur : on installe DANS un dossier OpenForesia du dossier choisi.
        if (!string.Equals(Path.GetFileName(dossier.TrimEnd('\\')), Inst.NomJeu, StringComparison.OrdinalIgnoreCase))
            dossier = Path.Combine(dossier, Inst.NomJeu);
        SetWindowText(s_CheminDossier, dossier);
    }

    private static bool VerifierDossier()
    {
        if (s_Edition == null) return false;
        long requis = Inst.TailleRequise(s_Edition, s_Jeu);
        SetWindowText(s_Espace, $"Espace nécessaire : {Inst.Mo(requis)}");

        string? probleme = Inst.Verifier(LireTexte(s_CheminDossier).Trim(), requis, out _);
        Etat(s_EtatDossier, probleme ?? "", Rouge);
        bool valide = probleme == null;
        if (s_Etape == Etape.Dossier) ActiverSuivant(valide);
        return valide;
    }

    private static bool ConfirmerDossier(string dossier)
    {
        Inst.Verifier(dossier, 0, out bool protege);
        if (protege && !Question(
                "Ce dossier est protégé par Windows : OpenForesia ne pourra pas y enregistrer vos " +
                "sauvegardes. Installer quand même ?"))
            return false;

        bool installationExistante = File.Exists(Path.Combine(dossier, Inst.Executable));
        bool nonVide = Directory.Exists(dossier) && Directory.EnumerateFileSystemEntries(dossier).Any();
        if (nonVide && !installationExistante &&
            !Question("Ce dossier contient déjà d'autres fichiers. Installer OpenForesia dedans quand même ?"))
            return false;

        return true;
    }

    private static bool Question(string texte) =>
        MessageBox(s_Fenetre, texte, Titre, MB_YESNO | MB_ICONWARNING | MB_DEFBUTTON2) == IDYES;

    // --- etape 3 : l'installation -------------------------------------------------------------------

    private sealed class Relais : IProgress<Avancement>
    {
        public void Report(Avancement valeur)
        {
            s_Avancement = valeur;
            // Un seul message en attente a la fois : la fenetre lit toujours la derniere valeur.
            if (Interlocked.Exchange(ref s_AvancementEnAttente, 1) == 0)
                PostMessage(s_Fenetre, MsgAvancement, 0, 0);
        }
    }

    private static void LancerInstallation()
    {
        string dossier = Path.GetFullPath(LireTexte(s_CheminDossier).Trim());
        if (s_Image == null || s_Edition == null || !VerifierDossier() || !ConfirmerDossier(dossier)) return;

        Afficher(Etape.Installation);
        SendMessage(s_Barre, PBM_SETPOS, 0, 0);
        SetWindowText(s_EtapeInstallation, "");
        s_Annulation = new CancellationTokenSource();

        ImageIso image = s_Image;
        Edition edition = s_Edition;
        SourceDuJeu jeu = s_Jeu;
        CancellationToken jeton = s_Annulation.Token;
        Task.Run(() =>
        {
            try
            {
                Inst.Installer(image, edition, jeu, dossier, new Relais(), jeton);
                s_CheminInstalle = dossier;
                s_FinInstallation = (false, null);
            }
            catch (OperationCanceledException) { s_FinInstallation = (true, null); }
            catch (Exception e)
            {
                s_FinInstallation = (false, e is IOException or UnauthorizedAccessException
                    ? "Un fichier n'a pas pu être écrit. Si OpenForesia est ouvert, fermez-le, puis réessayez."
                    : e.Message);
            }
            PostMessage(s_Fenetre, MsgInstallationFinie, 0, 0);
        });
    }

    private static void MontrerAvancement()
    {
        Interlocked.Exchange(ref s_AvancementEnAttente, 0);
        if (s_Avancement is not { } a) return;
        SendMessage(s_Barre, PBM_SETPOS, (IntPtr)(a.Fait * 100 / Math.Max(1, a.Total)), 0);
        SetWindowText(s_EtapeInstallation, a.Etape);
    }

    private static void FinInstallation()
    {
        s_Annulation?.Dispose();
        s_Annulation = null;
        (bool annulee, string? erreur) = s_FinInstallation;

        if (annulee)
        {
            MessageBox(s_Fenetre, "L'installation a été annulée. Rien n'a été laissé sur le disque.", Titre,
                MB_OK | MB_ICONINFORMATION);
            if (s_FermerApresAnnulation) { DestroyWindow(s_Fenetre); return; }
            Afficher(Etape.Dossier);
        }
        else if (erreur != null)
        {
            MessageBox(s_Fenetre, "L'installation n'a pas abouti. Rien n'a été laissé sur le disque.\n\n" + erreur,
                Titre, MB_OK | MB_ICONERROR);
            Afficher(Etape.Dossier);
        }
        else
        {
            Afficher(Etape.Fin);
        }
    }

    // --- fin et annulation --------------------------------------------------------------------------

    private static void Terminer()
    {
        if (SendMessage(s_Lancer, BM_GETCHECK, 0, 0) == 1 && s_CheminInstalle != null)
        {
            Process.Start(new ProcessStartInfo(Path.Combine(s_CheminInstalle, Inst.Executable))
            {
                WorkingDirectory = s_CheminInstalle,
                UseShellExecute = true,
            });
        }
        DestroyWindow(s_Fenetre);
    }

    private static void Annuler()
    {
        if (s_Etape == Etape.Installation)
        {
            if (s_Annulation != null && Question("Arrêter l'installation ?")) s_Annulation.Cancel();
            return;
        }
        if (s_Etape != Etape.Fin) DestroyWindow(s_Fenetre);
    }

    private static void Fermeture()
    {
        // Pendant l'installation, la croix vaut « Annuler » : on attend le nettoyage avant de fermer.
        if (s_Etape == Etape.Installation && s_Annulation != null)
        {
            if (Question("Arrêter l'installation ?"))
            {
                s_FermerApresAnnulation = true;
                s_Annulation.Cancel();
            }
            return;
        }
        DestroyWindow(s_Fenetre);
    }
}
