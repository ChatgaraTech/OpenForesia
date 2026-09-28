// =====================================================================
// L'écran de menu — une pile de pages : une liste à curseur, une question Oui / Non, une action.
//
// Hors reconstruction (legacy ++). Tiré le 2026-09-28 de l'écran de boutique de LP-008/LP-010
// (openspec/changes/add-legacy-plus-arena, design D5) : la boutique, le rachat (LP-011) et le menu
// de l'arène (LP-012) en sont trois usages. Le dessin, la mise en page et la manette sont ceux de
// l'écran de boutique, inchangés.
//
// Le jeu n'a aucun écran de choix : l'écran est dessiné avec les outils de dessin du jeu
// (Prims.DrawTextRun, la police 8×8 de l'écran de création du groupe) et un rectangle
// semi-transparent (le paquet GPU 0x62 du voile plein écran, FillScreenQuad_0051F940), par la couture
// DessinSurImpression, en fin de panneau d'interface. Le monde est gelé tant que l'écran est ouvert
// (FreezeAllEntities) ; le gel saute le pas des joueurs, donc la manette du joueur est lue ICI, à
// chaque image dessinée.
//
//   haut / bas   choisir          gauche / droite   dix lignes plus haut / plus bas
//   action       valider — la question de la page d'abord, si elle en a une
//   menu         fermer la question ; sinon la page précédente ; sinon sortir
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Jeu = OpenForesia.Blaze.Blaze;

namespace OpenForesia.LegacyPlus;

internal static unsafe class EcranMenu
{
    /// <summary>
    /// Une ligne : le libellé, le texte de la colonne de droite (un prix, un multiplicateur), et ce
    /// que l'appelant veut retrouver en validant.
    /// </summary>
    internal sealed record Ligne(string Affiche, string Droite = "", object? Donnee = null);

    /// <summary>Ce qu'une validation décide : un message, une page de plus, ou la sortie.</summary>
    internal sealed record Suite(string Message = "", Page? Suivante = null, bool Fermer = false);

    /// <summary>Une page de l'écran.</summary>
    internal sealed class Page
    {
        public required string Titre;

        /// <summary>Le texte à droite du titre (la bourse d'une boutique), relu à chaque image.</summary>
        public Func<string>? EnTete;

        /// <summary>Les lignes, relues à l'ouverture et après chaque validation.</summary>
        public required Func<IReadOnlyList<Ligne>> Lister;

        /// <summary>Le message d'une page sans ligne.</summary>
        public string SiVide = "";

        /// <summary>La question posée avant de valider une ligne ; <c>null</c> : on valide tout de suite.</summary>
        public Func<Ligne, string>? Question;

        /// <summary>
        /// Une page sans liste, réduite à sa question : Oui valide, Non et menu reviennent en arrière
        /// (ou ferment, si c'est la première page). Elle s'ouvre la question posée.
        /// </summary>
        public bool QuestionSeule;

        public required Func<Ligne, Suite> Valider;

        /// <summary>L'objet dont la ligne montre l'icône, la description et les valeurs ; <c>null</c> : rien.</summary>
        public Func<Ligne, CatalogueObjets.Objet?>? Objet;
    }

    private sealed class Etage
    {
        public required Page Page;
        public IReadOnlyList<Ligne> Lignes = Array.Empty<Ligne>();
        public int Curseur;
    }

    private const int ParPage = 10;

    // La mise en page, sur l'écran logique de 320 × 240, en police 8 × 8 (LP-010, demande de Vincent) :
    // sous la liste, la description et les valeurs de l'objet sous le curseur.
    private const int CadreX = 8, CadreY = 8, CadreL = 304, CadreH = 224;
    private const int MargeG = 16, MargeD = 304;
    private const int LigneTitre = 16, LignePremier = 32, Pas = 11, LigneMessage = 142, LignePied = 220;
    private const int LigneDescription = 156, LigneValeurs = 188, Interligne = 10, LignesMax = 3;
    private const int Colonnes = (MargeD - MargeG) / 8;   // 36 caractères
    private const int NomMax = 20;                      // caractères
    private const int PrixD = 232;                      // bord droit de la colonne de droite

    // La question seule : un bandeau au milieu de l'écran.
    private const int BandeauY = 92, BandeauH = 60, BandeauQuestion = 104, BandeauChoix = 132;

    // L'aperçu : l'icône de l'objet sous le curseur, 24 × 24 agrandie deux fois, à droite de la liste.
    private const int ApercuX = 248, ApercuY = LignePremier, Zoom = 2;

    /// <summary>Le fond : bleu sombre (0xBBGGRR), à moitié transparent.</summary>
    private const uint Fond = 0x401808;

    // Les boutons, sur l'octet des appuis de l'image (pad[2]) : disposition PlayStation,
    // confirmée par s_PadDirection_00562dc8 ; 0x08 est le bouton qui ouvre le menu du jeu.
    private const byte Haut = 0x10, Droite = 0x20, Bas = 0x40, Gauche = 0x80, Menu = 0x08;

    private static readonly List<Etage> s_Pile = new();
    private static uint* s_Membre;
    private static byte* s_PerChar;
    private static int s_Joueur;
    private static string s_Message = "";
    private static bool s_Question;
    private static bool s_Oui;

    private static uint* s_Atlas;                       // la police 8×8 : {0x40803000, 0x02300000}, comme s_GlyphAtlas_00566c28
    private static byte* s_Texte;                       // une ligne, en octets, terminée par un zéro

    internal static bool Ouvert => s_Pile.Count > 0;

    /// <summary>Le membre qui tient l'écran — celui dont la manette est lue.</summary>
    internal static uint* Membre => s_Membre;

    /// <summary>Une fois, quel que soit le nombre d'usages.</summary>
    internal static void Installer()
    {
        if (s_Atlas != null) return;
        s_Atlas = (uint*)NativeMemory.Alloc(8);
        s_Atlas[0] = 0x40803000;
        s_Atlas[1] = 0x02300000;
        s_Texte = (byte*)NativeMemory.AllocZeroed(64);

        Coutures.Dessin precedent = Coutures.DessinSurImpression;
        Coutures.DessinSurImpression = (otBase, curseur) =>
        {
            curseur = precedent(otBase, curseur);
            return Ouvert ? Dessiner(otBase, curseur) : curseur;
        };
    }

    /// <summary>Ouvre l'écran sur <paramref name="page"/>, tenu par <paramref name="membre"/>.</summary>
    internal static void Ouvrir(uint* membre, byte* perChar, Page page)
    {
        s_Pile.Clear();
        s_Membre = membre;
        s_PerChar = perChar;
        s_Joueur = ((byte*)membre)[8];
        Jeu.FreezeAllEntities();
        Journal($"ouvert par J{s_Joueur + 1} ({page.Titre})");
        Empiler(page);
    }

    internal static void Fermer()
    {
        if (!Ouvert) return;
        s_Pile.Clear();
        s_Question = false;
        Jeu.UnfreezeAllEntities();

        Journal("fermé");
    }

    private static void Empiler(Page page)
    {
        var etage = new Etage { Page = page, Lignes = page.Lister() };
        s_Pile.Add(etage);
        s_Question = page.QuestionSeule;
        s_Oui = true;
        s_Message = !page.QuestionSeule && etage.Lignes.Count == 0 ? page.SiVide : "";
    }

    /// <summary>Menu : la page précédente, ou la sortie.</summary>
    private static void Retour()
    {
        // La dernière page se ferme par Fermer, AVANT d'être retirée : Fermer ne fait rien sur une
        // pile déjà vide (garde « !Ouvert »). Retirer puis fermer laissait le monde gelé, sans
        // « fermé » au journal — le personnage ne répondait plus, seule la caméra bougeait (F2 chez
        // le lutin, Non au téléporteur ; journal de Vincent, 2026-09-28).
        if (s_Pile.Count <= 1) { Fermer(); return; }
        s_Pile.RemoveAt(s_Pile.Count - 1);

        // Revenir sur une page réduite à sa question la repose (le téléporteur : menu depuis la
        // liste des modes) ; sans cela, ni Oui ni Non n'y répondaient plus.
        s_Question = Sommet.Page.QuestionSeule;
        s_Oui = true;
        s_Message = "";
    }

    private static Etage Sommet => s_Pile[^1];

    // ------------------------------------------------------------------ la manette

    private static void LireLaManette()
    {
        byte type = Jeu.Inp.s_PlayerSlots_005e9188[s_Joueur * 0x4c];
        byte* pad = Jeu.Inp.s_InputStateBase_00604a20 + type * 0x10;
        byte appuis = pad[2];
        ushort action = *(ushort*)(s_PerChar + 0x1fc4);
        bool valider = (*(ushort*)(pad + 2) & action) != 0;

        Etage etage = Sommet;
        int n = etage.Lignes.Count;

        if (s_Question)
        {
            if ((appuis & (Haut | Bas | Gauche | Droite)) != 0) s_Oui = !s_Oui;
            bool non = (appuis & Menu) != 0 || (valider && !s_Oui);
            if (non)
            {
                if (etage.Page.QuestionSeule) Retour();
                else s_Question = false;
            }
            else if (valider)
            {
                s_Question = false;
                Valider(etage);
            }
            return;
        }

        if ((appuis & Menu) != 0) { Retour(); return; }
        if (n == 0) return;
        if ((appuis & Haut) != 0) etage.Curseur = (etage.Curseur - 1 + n) % n;
        if ((appuis & Bas) != 0) etage.Curseur = (etage.Curseur + 1) % n;
        if ((appuis & Gauche) != 0) etage.Curseur = Math.Max(0, etage.Curseur - ParPage);
        if ((appuis & Droite) != 0) etage.Curseur = Math.Min(n - 1, etage.Curseur + ParPage);
        if ((appuis & (Haut | Bas | Gauche | Droite)) != 0) s_Message = "";
        if (valider)
        {
            s_Message = "";
            if (etage.Page.Question != null) { s_Question = true; s_Oui = true; }
            else Valider(etage);
        }
    }

    private static void Valider(Etage etage)
    {
        Ligne ligne = etage.Lignes.Count > 0 ? etage.Lignes[etage.Curseur] : new Ligne("");
        Suite suite = etage.Page.Valider(ligne);

        if (suite.Fermer) { Fermer(); return; }
        if (suite.Suivante != null) { Empiler(suite.Suivante); return; }

        // La page reste : elle est relue, car la validation a pu la changer (une vente retire une ligne).
        etage.Lignes = etage.Page.Lister();
        if (etage.Curseur >= etage.Lignes.Count) etage.Curseur = Math.Max(0, etage.Lignes.Count - 1);
        s_Message = suite.Message;
        if (etage.Page.QuestionSeule) Retour();
    }

    // ------------------------------------------------------------------ le dessin

    private static uint* Dessiner(uint* otBase, uint* curseur)
    {
        LireLaManette();
        if (!Ouvert) return curseur;                    // fermé à cette image

        Etage etage = Sommet;
        return etage.Page.QuestionSeule ? DessinerQuestion(otBase, curseur, etage) : DessinerListe(otBase, curseur, etage);
    }

    private static uint* DessinerListe(uint* otBase, uint* curseur, Etage etage)
    {
        Page page = etage.Page;
        var lignes = etage.Lignes;
        int numero = etage.Curseur / ParPage, pages = Math.Max(1, (lignes.Count + ParPage - 1) / ParPage);

        curseur = Ecrire(otBase, curseur, MargeG, LigneTitre, Ascii(page.Titre));
        if (page.EnTete != null)
        {
            string entete = Ascii(page.EnTete());
            curseur = Ecrire(otBase, curseur, MargeD - entete.Length * 8, LigneTitre, entete);
        }

        for (int i = 0; i < ParPage; i++)
        {
            int k = numero * ParPage + i;
            if (k >= lignes.Count) break;
            int y = LignePremier + i * Pas;
            string nom = Ascii(lignes[k].Affiche);
            if (nom.Length > NomMax) nom = nom[..NomMax];
            if (k == etage.Curseur) curseur = Ecrire(otBase, curseur, MargeG, y, ">");
            curseur = Ecrire(otBase, curseur, MargeG + 10, y, nom);
            string droite = Ascii(lignes[k].Droite);
            curseur = Ecrire(otBase, curseur, PrixD - droite.Length * 8, y, droite);
        }

        Ligne? courante = lignes.Count > 0 ? lignes[etage.Curseur] : null;
        var objet = courante != null && page.Objet != null ? page.Objet(courante) : null;
        if (objet != null)
        {
            curseur = Apercu(otBase, curseur, objet.Vignette);
            if (!s_Question) curseur = Paragraphe(otBase, curseur, LigneDescription, Description(objet.Fiche));
            curseur = Paragraphe(otBase, curseur, LigneValeurs, Valeurs(objet.Fiche));
        }

        if (s_Question && courante != null)
        {
            // La question prend la place du message, le choix celle de la description.
            curseur = Ecrire(otBase, curseur, MargeG, LigneMessage, Ascii(page.Question!(courante)));
            curseur = Choix(otBase, curseur, LigneDescription);
        }
        else if (s_Message.Length > 0) curseur = Ecrire(otBase, curseur, MargeG, LigneMessage, Ascii(s_Message));
        curseur = Ecrire(otBase, curseur, MargeG, LignePied, $"Page {numero + 1}/{pages}");

        // Le fond EN DERNIER : ajouté en tête de la liste de l'ordering table, il est dessiné le
        // premier, donc sous le texte.
        return Rectangle(otBase, curseur, CadreX, CadreY, CadreL, CadreH, Fond, 0x62);
    }

    private static uint* DessinerQuestion(uint* otBase, uint* curseur, Etage etage)
    {
        // La question sur deux lignes au plus (36 caractères chacune), coupée aux espaces.
        string question = etage.Page.Question?.Invoke(new Ligne("")) ?? etage.Page.Titre;
        curseur = Paragraphe(otBase, curseur, BandeauQuestion, question);
        curseur = Choix(otBase, curseur, BandeauChoix);
        return Rectangle(otBase, curseur, CadreX, BandeauY, CadreL, BandeauH, Fond, 0x62);
    }

    private static uint* Choix(uint* otBase, uint* curseur, int y)
    {
        curseur = Ecrire(otBase, curseur, MargeG + 10, y, "Oui");
        curseur = Ecrire(otBase, curseur, MargeG + 74, y, "Non");
        return Ecrire(otBase, curseur, s_Oui ? MargeG : MargeG + 64, y, ">");
    }

    /// <summary>
    /// L'icône de l'objet, dessinée sans passer par la mémoire vidéo : la vignette porte une palette
    /// de seize couleurs PlayStation (BGR 5-5-5, +0x00) et une image 24 × 24 en 4 bits (+0x20, pixel
    /// de gauche dans le quartet bas). Chaque ligne est découpée en bandes de même couleur, une bande
    /// = un rectangle plat opaque. La couleur 0x0000 est transparente, comme sur la PlayStation.
    /// </summary>
    private static uint* Apercu(uint* otBase, uint* curseur, byte[] vignette)
    {
        Span<uint> couleurs = stackalloc uint[16];
        Span<bool> vides = stackalloc bool[16];
        for (int i = 0; i < 16; i++)
        {
            int c = vignette[i * 2] | (vignette[i * 2 + 1] << 8);
            vides[i] = c == 0;
            couleurs[i] = (uint)(((c & 31) << 3) | (((c >> 5) & 31) << 11) | (((c >> 10) & 31) << 19));
        }

        for (int y = 0; y < 24; y++)
        {
            int x = 0;
            while (x < 24)
            {
                int indice = Pixel(vignette, x, y), debut = x;
                while (x < 24 && Pixel(vignette, x, y) == indice) x++;
                if (vides[indice]) continue;
                curseur = Rectangle(otBase, curseur, ApercuX + debut * Zoom, ApercuY + y * Zoom,
                                    (x - debut) * Zoom, Zoom, couleurs[indice], 0x60);
            }
        }
        return curseur;
    }

    private static int Pixel(byte[] vignette, int x, int y)
    {
        byte paire = vignette[0x20 + y * 12 + (x >> 1)];
        return (x & 1) == 0 ? paire & 0xf : paire >> 4;
    }

    // ------------------------------------------------------------------ la fiche de l'objet

    /// <summary>
    /// La description : ce qui suit la barre oblique du champ <c>+0x41</c> (« nom/description »),
    /// jusqu'au zéro ; sans barre oblique, le champ entier — la règle de <c>BuildItemDisplayName</c>,
    /// portée depuis <c>OpenForesia.Tools bestiaire</c> (ItemDescription). Texte Latin-1.
    /// </summary>
    private static string Description(byte[] fiche)
    {
        int debut = 0x41;
        for (int i = 0; i < 0x40 && 0x41 + i < fiche.Length; i++)
        {
            byte c = fiche[0x41 + i];
            if (c == 0) break;
            if (c == 0x2f) { debut = 0x42 + i; break; }
        }
        var sb = new StringBuilder();
        for (int k = debut; k < fiche.Length && fiche[k] != 0; k++) sb.Append(fiche[k] >= 0x20 ? (char)fiche[k] : ' ');
        return System.Text.RegularExpressions.Regex.Replace(sb.ToString(), @"\s+", " ").Trim();
    }

    private static readonly string[] s_Caracs = ["FO", "INT", "VO", "AGI", "CON", "PM", "CH"];
    private static readonly string[] s_Elements = ["Feu", "Eau", "Terre", "Air", "Lumiere", "Tenebres", "Sacre", "Mal"];

    /// <summary>
    /// Les valeurs de la fiche, aux champs que l'écran d'objet du jeu et
    /// <c>ApplyEquipmentStatBonuses</c> lisent (Documentation/Reference/Bestiaire/LISEZMOI.md) :
    /// ATT <c>+0x30</c>, DEF <c>+0x32</c>, MATT <c>+0x38</c>, MDEF <c>+0x3a</c> (shorts du bloc
    /// <c>+0x20</c>, indices 8, 9, 12, 13) ; les sept caractéristiques, indices 0 à 6 du même bloc ;
    /// les huit éléments, octets signés de <c>+0x18</c>. Les quatre dérivées sans nom établi
    /// (indices 10, 11, 14, 15) ne sont pas montrées. Seules les valeurs non nulles.
    /// </summary>
    private static string Valeurs(byte[] fiche)
    {
        short S(int o) => (short)(fiche[o] | (fiche[o + 1] << 8));
        string Signe(int v) => v > 0 ? $"+{v}" : v.ToString(CultureInfo.InvariantCulture);
        var parts = new List<string>();
        foreach (var (nom, o) in new[] { ("ATT", 0x30), ("DEF", 0x32), ("MATT", 0x38), ("MDEF", 0x3a) })
            if (S(o) != 0) parts.Add($"{nom} {S(o)}");
        for (int i = 0; i < s_Caracs.Length; i++)
            if (S(0x20 + i * 2) != 0) parts.Add($"{s_Caracs[i]} {Signe(S(0x20 + i * 2))}");
        for (int i = 0; i < s_Elements.Length; i++)
            if ((sbyte)fiche[0x18 + i] != 0) parts.Add($"{s_Elements[i]} {Signe((sbyte)fiche[0x18 + i])}");
        return string.Join("  ", parts);
    }

    /// <summary>Un texte coupé aux espaces sur <see cref="Colonnes"/> caractères, au plus trois lignes.</summary>
    private static uint* Paragraphe(uint* otBase, uint* curseur, int y, string texte)
    {
        var lignes = new List<string>();
        var ligne = new StringBuilder();
        foreach (string mot in Ascii(texte).Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            if (ligne.Length > 0 && ligne.Length + 1 + mot.Length > Colonnes)
            {
                lignes.Add(ligne.ToString());
                ligne.Clear();
            }
            if (ligne.Length > 0) ligne.Append(' ');
            ligne.Append(mot.Length > Colonnes ? mot[..Colonnes] : mot);
        }
        if (ligne.Length > 0) lignes.Add(ligne.ToString());

        for (int i = 0; i < Math.Min(lignes.Count, LignesMax); i++)
        {
            string l = lignes[i];
            if (i == LignesMax - 1 && lignes.Count > LignesMax) l = (l.Length > Colonnes - 3 ? l[..(Colonnes - 3)] : l) + "...";
            curseur = Ecrire(otBase, curseur, MargeG, y + i * Interligne, l);
        }
        return curseur;
    }

    /// <summary>Une ligne de texte, par Prims.DrawTextRun, sur la police 8×8.</summary>
    private static uint* Ecrire(uint* otBase, uint* curseur, int x, int y, string texte)
    {
        int n = Math.Min(texte.Length, 63);
        for (int i = 0; i < n; i++) s_Texte[i] = (byte)texte[i];
        s_Texte[n] = 0;
        if (n == 0) return curseur;

        int* bloc = stackalloc int[3];
        bloc[0] = (int)(nint)s_Atlas;
        bloc[1] = 0x1000000;                            // le « mot de base » : texture sans teinte (voir LB-008)
        bloc[2] = (y << 16) | (x & 0xffff);
        return Jeu.Prims.DrawTextRun(otBase, curseur, bloc, s_Texte, 0, 0, -1);
    }

    /// <summary>
    /// Un rectangle plat : le paquet de FillScreenQuad_0051F940 — commande 0x62 (semi-transparent,
    /// le fond) ou 0x60 (opaque, les bandes de l'aperçu).
    /// </summary>
    private static uint* Rectangle(uint* otBase, uint* prim, int x, int y, int l, int h, uint couleur, uint commande)
    {
        prim[1] = 0xe1000200;
        prim[2] = (couleur & 0xffffff) | (commande << 24);
        prim[3] = (uint)(y << 16) | ((uint)x & 0xffff);
        prim[4] = (uint)(h << 16) | ((uint)l & 0xffff);
        *prim = *otBase | 0x4000000;
        *otBase = (uint)(nuint)prim;
        return prim + 5;
    }

    /// <summary>La police 8×8 ne porte pas d'accents sûrs : les lettres accentuées perdent leur accent.</summary>
    internal static string Ascii(string s)
    {
        var sb = new StringBuilder();
        foreach (char c in s.Replace("œ", "oe").Replace("Œ", "OE").Normalize(NormalizationForm.FormD))
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark) continue;
            sb.Append(c < 0x80 ? c : '?');
        }
        return sb.ToString();
    }

    private static void Journal(string message) => Console.Error.WriteLine($"[MENU] {message}");
}
