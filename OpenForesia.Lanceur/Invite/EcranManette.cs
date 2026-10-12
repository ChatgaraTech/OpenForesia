// L'ecran de configuration de la manette, a la maniere d'un emulateur (demande de Vincent,
// 2026-09-30) : une manette PlayStation dessinee au milieu, une case par bouton tout autour, reliee
// a son bouton, qui dit quelle touche ou quel bouton de manette le presse.
//
//   F1                 ouvrir / fermer, a tout moment, meme en pleine partie
//   clic sur une case  « Appuie sur… » : la prochaine entree pressee lui est associee (et retiree de
//                      la case qui l'avait, s'il y en avait une)
//   clic droit         efface la case
//   Echap              annule la saisie en cours
//   Tout reconfigurer  les quatorze cases a la suite
//
// Ce que l'on presse s'allume en direct, sur la manette et dans sa case : on verifie sa
// configuration sans rien envoyer au jeu — pendant que l'ecran est ouvert, le joueur de l'invite
// ne recoit que des boutons relaches. Chaque changement est enregistre aussitot.
//
// Le dessin se fait dans un repere de 960 x 720, mis a l'echelle de la fenetre (proportions
// gardees), dans une image hors ecran copiee d'un bloc : sans scintillement. Les clics sont ramenes
// dans ce repere.

using System.Runtime.InteropServices;
using OpenForesia.Lanceur.Commun;

namespace OpenForesia.Lanceur.Invite;

internal sealed unsafe partial class EcranManette
{
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreateCompatibleDC(IntPtr hdc);
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreateCompatibleBitmap(IntPtr hdc, int l, int h);
    [LibraryImport("gdi32.dll")] private static partial IntPtr SelectObject(IntPtr hdc, IntPtr objet);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool DeleteDC(IntPtr hdc);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool DeleteObject(IntPtr objet);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool BitBlt(IntPtr d, int x, int y, int l, int h, IntPtr s, int xs, int ys, uint rop);
    [LibraryImport("gdi32.dll")] private static partial int SetMapMode(IntPtr hdc, int mode);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool SetWindowExtEx(IntPtr hdc, int x, int y, void* ancien);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool SetViewportExtEx(IntPtr hdc, int x, int y, void* ancien);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool SetViewportOrgEx(IntPtr hdc, int x, int y, void* ancien);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool RoundRect(IntPtr hdc, int g, int h, int d, int b, int l, int ht);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool Ellipse(IntPtr hdc, int g, int h, int d, int b);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool Rectangle(IntPtr hdc, int g, int h, int d, int b);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool Polygon(IntPtr hdc, Point* points, int n);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool MoveToEx(IntPtr hdc, int x, int y, void* ancien);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool LineTo(IntPtr hdc, int x, int y);
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreateSolidBrush(uint couleur);
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreatePen(int style, int largeur, uint couleur);
    [LibraryImport("gdi32.dll")] private static partial IntPtr GetStockObject(int objet);
    [LibraryImport("gdi32.dll")] private static partial int SetBkMode(IntPtr hdc, int mode);
    [LibraryImport("gdi32.dll")] private static partial uint SetTextColor(IntPtr hdc, uint couleur);
    [LibraryImport("gdi32.dll", EntryPoint = "CreateFontW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial IntPtr CreateFont(int h, int l, int e, int o, int g, uint i, uint s, uint b, uint jeu, uint p, uint d, uint q, uint f, string nom);
    [LibraryImport("user32.dll", EntryPoint = "DrawTextW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int DrawText(IntPtr hdc, string texte, int n, Rect* r, uint format);
    [LibraryImport("user32.dll")] private static partial int FillRect(IntPtr hdc, Rect* r, IntPtr brosse);

    [StructLayout(LayoutKind.Sequential)] private struct Rect { public int G, H, D, B; }
    [StructLayout(LayoutKind.Sequential)] private struct Point { public int X, Y; }

    private const int L = 960, Ht = 720;                                       // le repère virtuel
    private const int Anisotrope = 8, Transparent = 1, NullBrush = 5, NullPen = 8;
    private const uint Centre = 0x1, VCentre = 0x4, UneLigne = 0x20, Coupe = 0x10, FinEllipse = 0x8000;
    private const int VkEchap = 0x1B, VkF1 = 0x70;

    // Les couleurs (COLORREF : 0x00BBGGRR).
    private const uint Fond = 0x241E1E, Case = 0x342C2C, CaseBord = 0x5A5050, Texte = 0xF0F0F0, TexteDoux = 0xA8A0A0,
                       Allume = 0x3CB4FF, Saisie = 0x00D7FF, Corps = 0xD2CCC8, CorpsOmbre = 0x9C9690, Croix = 0x3C3C3C,
                       Trait = 0x7A7070, Vert = 0x7CC43A, Rouge = 0x3C3CE0, Bleu = 0xD89040, Rose = 0xC080E0;

    /// <summary>Une case : son bouton, son rectangle, et le point de la manette où son trait aboutit.</summary>
    private readonly record struct Emplacement(string Bouton, int G, int H, int D, int B, int Px, int Py);

    private static readonly Emplacement[] s_Cases =
    [
        new("L2", 40, 140, 250, 195, 375, 187),
        new("L1", 40, 205, 250, 260, 375, 212),
        new("HAUT", 40, 270, 250, 325, 390, 297),
        new("GAUCHE", 40, 335, 250, 390, 360, 320),
        new("DROITE", 40, 400, 250, 455, 420, 320),
        new("BAS", 40, 465, 250, 520, 390, 343),
        new("R2", 710, 140, 920, 195, 585, 187),
        new("R1", 710, 205, 920, 260, 585, 212),
        new("TRIANGLE", 710, 270, 920, 325, 570, 290),
        new("ROND", 710, 335, 920, 390, 600, 320),
        new("CROIX", 710, 400, 920, 455, 570, 350),
        new("CARRE", 710, 465, 920, 520, 540, 320),
        new("SELECT", 290, 570, 470, 625, 455, 325),
        new("START", 490, 570, 670, 625, 505, 325),
    ];

    private static readonly Rect BoutonTout = new() { G = 40, H = 650, D = 250, B = 695 };
    private static readonly Rect BoutonFermer = new() { G = 710, H = 650, D = 920, B = 695 };

    private readonly ConfigManette _config;
    private readonly string _libelleFermer;               // « Fermer (F1) » en partie, « Continuer » avant de rejoindre
    private readonly IntPtr _police, _policeNom, _policeTitre;
    private readonly HashSet<Entree> _avant = new();
    private HashSet<Entree> _actives = new();
    private readonly Queue<string> _suite = new();            // « tout reconfigurer » : les cases à venir
    private string? _saisie;                                   // la case qui attend une entrée
    private bool _echapTenu;

    internal bool Ouvert { get; private set; }

    /// <summary>L'écran a changé et doit être redessiné.</summary>
    internal bool AChange { get; set; }

    internal EcranManette(ConfigManette config, string libelleFermer = "Fermer (F1)")
    {
        _config = config;
        _libelleFermer = libelleFermer;
        _police = CreateFont(-15, 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        _policeNom = CreateFont(-12, 0, 0, 0, 400, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
        _policeTitre = CreateFont(-22, 0, 0, 0, 600, 0, 0, 0, 1, 0, 0, 5, 0, "Segoe UI");
    }

    internal void Basculer()
    {
        Ouvert = !Ouvert;
        _saisie = null;
        _suite.Clear();
        AChange = true;
    }

    // ---- les entrées ---------------------------------------------------------------------------------

    /// <summary>
    /// À chaque relevé des entrées, écran ouvert : ce qui est pressé s'allume, et une case en attente
    /// prend la première entrée nouvellement pressée (XInput, puis les autres manettes, puis le clavier).
    /// </summary>
    internal void Tour(HashSet<Entree> actives)
    {
        if (!Ouvert) return;
        if (!actives.SetEquals(_actives)) AChange = true;
        _actives = new HashSet<Entree>(actives);

        // Échap : un appui annule la saisie (et la suite de « tout reconfigurer »).
        bool echap = actives.Contains(new Entree(Source.Clavier, 0, VkEchap));
        if (echap && !_echapTenu && _saisie != null) { _saisie = null; _suite.Clear(); AChange = true; }
        _echapTenu = echap;

        if (_saisie != null)
        {
            Entree? nouvelle = actives.Where(e => !_avant.Contains(e) && !Reservee(e))
                                      .OrderBy(e => e.Source switch { Source.XInput => 0, Source.Joystick => 1, _ => 2 })
                                      .Cast<Entree?>().FirstOrDefault();
            if (nouvelle is { } e)
            {
                foreach (string autre in _config.Liaisons.Where(l => l.Value == e).Select(l => l.Key).ToList())
                    _config.Liaisons.Remove(autre);
                _config.Liaisons[_saisie] = e;
                _config.Enregistrer();
                _saisie = _suite.Count > 0 ? _suite.Dequeue() : null;
                AChange = true;
            }
        }
        _avant.Clear();
        _avant.UnionWith(actives);
    }

    /// <summary>F1 et Échap commandent l'écran : ils ne peuvent pas devenir un bouton.</summary>
    private static bool Reservee(Entree e) => e.Source == Source.Clavier && e.Element is VkEchap or VkF1;

    /// <summary>Un clic, en coordonnées de la zone client ; <paramref name="droit"/> pour le bouton droit.</summary>
    internal void Clic(int x, int y, int largeur, int hauteur, bool droit)
    {
        if (!Ouvert) return;
        (double echelle, int ox, int oy) = Echelle(largeur, hauteur);
        int vx = (int)((x - ox) / echelle), vy = (int)((y - oy) / echelle);

        foreach (Emplacement c in s_Cases)
        {
            if (vx < c.G || vx > c.D || vy < c.H || vy > c.B) continue;
            if (droit)
            {
                _config.Liaisons.Remove(c.Bouton);
                _config.Enregistrer();
                if (_saisie == c.Bouton) _saisie = null;
            }
            else
            {
                _suite.Clear();
                _saisie = c.Bouton;
            }
            AChange = true;
            return;
        }
        if (droit) return;
        if (Dans(BoutonTout, vx, vy)) ToutReconfigurer();
        else if (Dans(BoutonFermer, vx, vy)) Basculer();
    }

    /// <summary>Les quatorze cases à la suite, dans l'ordre de la manette (CROIX d'abord).</summary>
    internal void ToutReconfigurer()
    {
        _suite.Clear();
        foreach (Pad.Bouton b in Pad.Boutons.Skip(1)) _suite.Enqueue(b.Nom);
        _saisie = Pad.Boutons[0].Nom;
        AChange = true;
    }

    private static bool Dans(Rect r, int x, int y) => x >= r.G && x <= r.D && y >= r.H && y <= r.B;

    private static (double Echelle, int Ox, int Oy) Echelle(int largeur, int hauteur)
    {
        double e = Math.Min((double)largeur / L, (double)hauteur / Ht);
        return (e, (int)((largeur - L * e) / 2), (int)((hauteur - Ht * e) / 2));
    }

    // ---- le dessin -----------------------------------------------------------------------------------

    /// <summary>Dessine l'écran entier dans <paramref name="hdc"/> (zone client de <paramref name="largeur"/> × <paramref name="hauteur"/>).</summary>
    internal void Dessiner(IntPtr hdc, int largeur, int hauteur)
    {
        if (largeur <= 0 || hauteur <= 0) return;
        IntPtr memoire = CreateCompatibleDC(hdc);
        IntPtr image = CreateCompatibleBitmap(hdc, largeur, hauteur);
        IntPtr ancienne = SelectObject(memoire, image);
        try
        {
            IntPtr fond = CreateSolidBrush(Fond);
            var tout = new Rect { D = largeur, B = hauteur };
            FillRect(memoire, &tout, fond);
            DeleteObject(fond);

            (double e, int ox, int oy) = Echelle(largeur, hauteur);
            SetMapMode(memoire, Anisotrope);
            SetWindowExtEx(memoire, L, Ht, null);
            SetViewportExtEx(memoire, (int)(L * e), (int)(Ht * e), null);
            SetViewportOrgEx(memoire, ox, oy, null);
            SetBkMode(memoire, Transparent);

            ushort presses = _config.Mot(_actives);
            DessinerTitre(memoire);
            DessinerTraits(memoire);
            DessinerManette(memoire, presses);
            foreach (Emplacement c in s_Cases) DessinerCase(memoire, c, presses);
            DessinerBouton(memoire, BoutonTout, "Tout reconfigurer");
            DessinerBouton(memoire, BoutonFermer, _libelleFermer);
            Ecrire(memoire, _policeNom, TexteDoux, "Clic : changer  ·  Clic droit : effacer  ·  Échap : annuler",
                   new Rect { G = 250, H = 655, D = 710, B = 690 }, Centre | VCentre | UneLigne);

            SetMapMode(memoire, 1);                                             // MM_TEXT, pour la copie
            SetViewportOrgEx(memoire, 0, 0, null);
            BitBlt(hdc, 0, 0, largeur, hauteur, memoire, 0, 0, 0x00CC0020);     // SRCCOPY
        }
        finally
        {
            SelectObject(memoire, ancienne);
            DeleteObject(image);
            DeleteDC(memoire);
        }
    }

    private void DessinerTitre(IntPtr hdc)
    {
        Ecrire(hdc, _policeTitre, Texte, "Configuration de la manette", new Rect { G = 0, H = 30, D = L, B = 64 }, Centre | UneLigne);
        string aide = _saisie != null
            ? $"Appuie sur la touche ou le bouton qui sera {Nom(_saisie)}"
            : "Clique une case, puis appuie sur le bouton voulu. Ce que tu presses s'allume.";
        Ecrire(hdc, _police, _saisie != null ? Saisie : TexteDoux, aide, new Rect { G = 0, H = 72, D = L, B = 100 }, Centre | UneLigne);
    }

    private void DessinerTraits(IntPtr hdc)
    {
        IntPtr crayon = CreatePen(0, 2, Trait);
        IntPtr ancien = SelectObject(hdc, crayon);
        foreach (Emplacement c in s_Cases)
        {
            bool gauche = c.D <= 300, droite = c.G >= 700;
            int x = gauche ? c.D : droite ? c.G : (c.G + c.D) / 2;
            int y = gauche || droite ? (c.H + c.B) / 2 : c.H;
            MoveToEx(hdc, x, y, null);
            if (gauche || droite)
            {
                int coude = gauche ? 300 : 660;                                 // un coude au bord de la manette
                LineTo(hdc, coude, y);
                LineTo(hdc, c.Px, c.Py);
            }
            else LineTo(hdc, c.Px, c.Py);
        }
        SelectObject(hdc, ancien);
        DeleteObject(crayon);
    }

    private void DessinerManette(IntPtr hdc, ushort presses)
    {
        IntPtr sansTrait = GetStockObject(NullPen);
        IntPtr ancienCrayon = SelectObject(hdc, sansTrait);

        // Les gâchettes, derrière le corps.
        Forme(hdc, presses, "L2", CorpsOmbre, () => RoundRect(hdc, 335, 178, 415, 200, 10, 10));
        Forme(hdc, presses, "L1", CorpsOmbre, () => RoundRect(hdc, 335, 202, 415, 226, 10, 10));
        Forme(hdc, presses, "R2", CorpsOmbre, () => RoundRect(hdc, 545, 178, 625, 200, 10, 10));
        Forme(hdc, presses, "R1", CorpsOmbre, () => RoundRect(hdc, 545, 202, 625, 226, 10, 10));

        // Le corps et ses deux poignées.
        Remplir(hdc, Corps, () => Ellipse(hdc, 305, 300, 415, 480));
        Remplir(hdc, Corps, () => Ellipse(hdc, 545, 300, 655, 480));
        Remplir(hdc, Corps, () => RoundRect(hdc, 310, 220, 650, 400, 80, 80));

        // La croix directionnelle.
        Remplir(hdc, Croix, () => RoundRect(hdc, 374, 280, 406, 360, 8, 8));
        Remplir(hdc, Croix, () => RoundRect(hdc, 350, 304, 430, 336, 8, 8));
        Forme(hdc, presses, "HAUT", Croix, () => Polygone(hdc, (390, 286), (380, 300), (400, 300)));
        Forme(hdc, presses, "BAS", Croix, () => Polygone(hdc, (390, 354), (380, 340), (400, 340)));
        Forme(hdc, presses, "GAUCHE", Croix, () => Polygone(hdc, (356, 320), (370, 310), (370, 330)));
        Forme(hdc, presses, "DROITE", Croix, () => Polygone(hdc, (424, 320), (410, 310), (410, 330)));

        // SELECT et START.
        Forme(hdc, presses, "SELECT", Croix, () => RoundRect(hdc, 440, 318, 470, 332, 12, 12));
        Forme(hdc, presses, "START", Croix, () => RoundRect(hdc, 490, 318, 520, 332, 12, 12));

        // Les quatre boutons, fond sombre, symbole en couleur.
        (string Nom, int X, int Y, uint Couleur)[] faces = [("TRIANGLE", 570, 290, Vert), ("ROND", 600, 320, Rouge), ("CROIX", 570, 350, Bleu), ("CARRE", 540, 320, Rose)];
        foreach (var f in faces)
        {
            Forme(hdc, presses, f.Nom, Croix, () => Ellipse(hdc, f.X - 14, f.Y - 14, f.X + 14, f.Y + 14));
            IntPtr crayon = CreatePen(0, 2, f.Couleur);
            IntPtr ancien = SelectObject(hdc, crayon);
            IntPtr sansFond = GetStockObject(NullBrush);
            IntPtr ancienFond = SelectObject(hdc, sansFond);
            switch (f.Nom)
            {
                case "TRIANGLE": Polygone(hdc, (f.X, f.Y - 7), (f.X - 7, f.Y + 5), (f.X + 7, f.Y + 5)); break;
                case "ROND": Ellipse(hdc, f.X - 7, f.Y - 7, f.X + 7, f.Y + 7); break;
                case "CROIX":
                    MoveToEx(hdc, f.X - 6, f.Y - 6, null); LineTo(hdc, f.X + 7, f.Y + 7);
                    MoveToEx(hdc, f.X + 6, f.Y - 6, null); LineTo(hdc, f.X - 7, f.Y + 7);
                    break;
                case "CARRE": Rectangle(hdc, f.X - 6, f.Y - 6, f.X + 7, f.Y + 7); break;
            }
            SelectObject(hdc, ancienFond);
            SelectObject(hdc, ancien);
            DeleteObject(crayon);
        }
        SelectObject(hdc, ancienCrayon);
    }

    private void DessinerCase(IntPtr hdc, Emplacement c, ushort presses)
    {
        bool presse = Presse(presses, c.Bouton), saisie = _saisie == c.Bouton;
        IntPtr fond = CreateSolidBrush(presse ? Allume : Case);
        IntPtr crayon = CreatePen(0, saisie ? 3 : 1, saisie ? Saisie : presse ? Allume : CaseBord);
        IntPtr ancienFond = SelectObject(hdc, fond), ancienCrayon = SelectObject(hdc, crayon);
        RoundRect(hdc, c.G, c.H, c.D, c.B, 12, 12);
        SelectObject(hdc, ancienFond);
        SelectObject(hdc, ancienCrayon);
        DeleteObject(fond);
        DeleteObject(crayon);

        uint couleurNom = presse ? Fond : TexteDoux, couleurLiaison = presse ? Fond : Texte;
        Ecrire(hdc, _policeNom, couleurNom, Nom(c.Bouton), new Rect { G = c.G + 12, H = c.H + 6, D = c.D - 10, B = c.H + 24 }, UneLigne);
        string liaison = saisie ? "Appuie…" : _config.Liaisons.TryGetValue(c.Bouton, out Entree e) ? e.Libelle : "—";
        Ecrire(hdc, _police, saisie ? Saisie : couleurLiaison, liaison,
               new Rect { G = c.G + 12, H = c.H + 24, D = c.D - 10, B = c.B - 4 }, UneLigne | FinEllipse);
    }

    private void DessinerBouton(IntPtr hdc, Rect r, string texte)
    {
        IntPtr fond = CreateSolidBrush(Case);
        IntPtr crayon = CreatePen(0, 1, CaseBord);
        IntPtr ancienFond = SelectObject(hdc, fond), ancienCrayon = SelectObject(hdc, crayon);
        RoundRect(hdc, r.G, r.H, r.D, r.B, 12, 12);
        SelectObject(hdc, ancienFond);
        SelectObject(hdc, ancienCrayon);
        DeleteObject(fond);
        DeleteObject(crayon);
        Ecrire(hdc, _police, Texte, texte, r, Centre | VCentre | UneLigne);
    }

    private static string Nom(string bouton) => bouton switch
    {
        "CARRE" => "CARRÉ",
        _ => bouton,
    };

    private static bool Presse(ushort presses, string bouton) =>
        Array.Find(Pad.Boutons, b => b.Nom == bouton) is var b && (presses & b.Masque) != 0;

    /// <summary>Remplit la forme d'une couleur, ou de la couleur « allumée » si son bouton est pressé.</summary>
    private static void Forme(IntPtr hdc, ushort presses, string bouton, uint couleur, Action dessin) =>
        Remplir(hdc, Presse(presses, bouton) ? Allume : couleur, dessin);

    private static void Remplir(IntPtr hdc, uint couleur, Action dessin)
    {
        IntPtr brosse = CreateSolidBrush(couleur);
        IntPtr ancienne = SelectObject(hdc, brosse);
        dessin();
        SelectObject(hdc, ancienne);
        DeleteObject(brosse);
    }

    private static void Polygone(IntPtr hdc, params (int X, int Y)[] sommets)
    {
        Point* p = stackalloc Point[sommets.Length];
        for (int i = 0; i < sommets.Length; i++) p[i] = new Point { X = sommets[i].X, Y = sommets[i].Y };
        Polygon(hdc, p, sommets.Length);
    }

    private static void Ecrire(IntPtr hdc, IntPtr police, uint couleur, string texte, Rect r, uint format)
    {
        IntPtr ancienne = SelectObject(hdc, police);
        SetTextColor(hdc, couleur);
        DrawText(hdc, texte, -1, &r, format);
        SelectObject(hdc, ancienne);
    }
}
