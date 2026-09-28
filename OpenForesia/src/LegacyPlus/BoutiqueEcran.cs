// =====================================================================
// LP-008, étape 3a — l'écran de la boutique : la liste, les pages, le curseur, la bourse.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-inn-shop, design D6-D9).
// Le jeu n'a aucun écran de boutique, et son moteur de boîtes n'a pas de code de choix : l'écran est
// dessiné ici avec les outils de dessin du jeu (Prims.DrawTextRun, la police 8×8 de l'écran de
// création du groupe) et un rectangle semi-transparent (le paquet GPU 0x62 du voile plein écran,
// FillScreenQuad_0051F940), par la couture DessinSurImpression, en fin de panneau d'interface.
//
// Le monde est gelé tant que la boutique est ouverte (FreezeAllEntities). Le gel saute le pas des
// joueurs, donc la manette de l'acheteur est lue ICI, à chaque image dessinée.
//
//   haut / bas      choisir                gauche / droite   page précédente / suivante
//   action          acheter (BoutiqueAchat, étape 3c)       menu (bouton 3)   sortir
//
// L'action pose d'abord la question « Ton choix se porte sur cet objet ? Oui / Non » (flèches pour
// choisir, action pour valider, menu = non) ; l'achat ou la vente n'a lieu que sur « Oui ».
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Jeu = OpenForesia.Blaze.Blaze;

namespace OpenForesia.LegacyPlus;

internal static unsafe class BoutiqueEcran
{
    private const int ParPage = 10;

    // La mise en page, sur l'écran logique de 320 × 240, en police 8 × 8. Agrandie pour LP-010 (demande
    // de Vincent) : sous la liste, la description et les valeurs de l'objet sous le curseur.
    private const int CadreX = 8, CadreY = 8, CadreL = 304, CadreH = 224;
    private const int MargeG = 16, MargeD = 304;
    private const int LigneTitre = 16, LignePremier = 32, Pas = 11, LigneMessage = 142, LignePied = 220;
    private const int LigneDescription = 156, LigneValeurs = 188, Interligne = 10, LignesMax = 3;
    private const int Colonnes = (MargeD - MargeG) / 8;   // 36 caractères
    private const int NomMax = 20;                      // caractères
    private const int PrixD = 232;                      // bord droit de la colonne des prix

    // L'aperçu : l'icône de l'objet sous le curseur, 24 × 24 agrandie deux fois, à droite de la liste.
    private const int ApercuX = 248, ApercuY = LignePremier, Zoom = 2;

    /// <summary>Le fond : bleu sombre (0xBBGGRR), à moitié transparent.</summary>
    private const uint Fond = 0x401808;

    // Les boutons, sur l'octet des appuis de l'image (pad[2]) : disposition PlayStation,
    // confirmée par s_PadDirection_00562dc8 ; 0x08 est le bouton qui ouvre le menu du jeu.
    private const byte Haut = 0x10, Droite = 0x20, Bas = 0x40, Gauche = 0x80, Menu = 0x08;

    private static bool s_Ouvert;
    private static uint* s_Acheteur;
    private static byte* s_PerChar;
    private static int s_Joueur;
    private static int s_Curseur;
    private static string s_Message = "";
    private static string s_Titre = "";

    // La validation (demande de Vincent, 2026-09-28) : l'action ne fait qu'ouvrir la question, un
    // second appui sur « Oui » achète ou vend. Menu ou « Non » revient à la liste.
    private static bool s_Question;
    private static bool s_Oui;
    private const string Question = "Ton choix se porte sur cet objet ?";

    /// <summary>
    /// Une ligne de l'écran : le libellé affiché, le nom exact de l'objet, le prix, et — pour une
    /// vente (LP-011) — la fiche et la vignette de l'exemplaire lui-même, lues dans l'emplacement.
    /// Sans fiche, l'aperçu la cherche dans l'index des objets par le nom.
    /// </summary>
    internal sealed record Ligne(string Affiche, string Nom, int Prix, byte[]? Fiche = null, byte[]? Vignette = null);

    private static IReadOnlyList<Ligne> s_Lignes = Array.Empty<Ligne>();
    private static Func<IReadOnlyList<Ligne>>? s_Lister;     // mode vente : la liste, recalculée après chaque vente
    private static Func<Ligne, string>? s_Vendre;            // mode vente : l'action, rend le message

    private static uint* s_Atlas;                       // la police 8×8 : {0x40803000, 0x02300000}, comme s_GlyphAtlas_00566c28
    private static byte* s_Texte;                       // une ligne, en octets, terminée par un zéro

    internal static bool Ouvert => s_Ouvert;

    internal static void Installer()
    {
        s_Atlas = (uint*)NativeMemory.Alloc(8);
        s_Atlas[0] = 0x40803000;
        s_Atlas[1] = 0x02300000;
        s_Texte = (byte*)NativeMemory.AllocZeroed(64);

        Coutures.Dessin precedent = Coutures.DessinSurImpression;
        Coutures.DessinSurImpression = (otBase, curseur) =>
        {
            curseur = precedent(otBase, curseur);
            return s_Ouvert ? Dessiner(otBase, curseur) : curseur;
        };
    }

    /// <summary>
    /// Ouvre l'écran sur le catalogue d'un marchand (LP-010), déjà filtré par l'appelant. Un
    /// catalogue vide ouvre quand même l'écran, avec un message : le joueur sait pourquoi.
    /// </summary>
    internal static void Ouvrir(uint* acheteur, byte* perChar, string titre, IReadOnlyList<(string Nom, int Prix)> catalogue)
    {
        s_Lister = null;
        s_Vendre = null;
        Demarrer(acheteur, perChar, titre, catalogue.Select(o => new Ligne(o.Nom, o.Nom, o.Prix)).ToList(),
                 "Rien a vendre pour toi ici.");
    }

    /// <summary>
    /// Ouvre l'écran en mode RACHAT (LP-011) : <paramref name="lister"/> donne ce que le joueur peut
    /// vendre, <paramref name="vendre"/> vend une ligne et rend le message ; la liste est recalculée
    /// après chaque vente.
    /// </summary>
    internal static void OuvrirVente(uint* vendeur, byte* perChar, string titre,
                                     Func<IReadOnlyList<Ligne>> lister, Func<Ligne, string> vendre)
    {
        s_Lister = lister;
        s_Vendre = vendre;
        Demarrer(vendeur, perChar, titre, lister(), "Rien que je puisse te racheter.");
    }

    private static void Demarrer(uint* acheteur, byte* perChar, string titre, IReadOnlyList<Ligne> lignes, string siVide)
    {
        s_Titre = titre;
        s_Lignes = lignes;
        s_Ouvert = true;
        s_Acheteur = acheteur;
        s_PerChar = perChar;
        s_Joueur = ((byte*)acheteur)[8];
        s_Curseur = 0;
        s_Question = false;
        s_Message = lignes.Count == 0 ? siVide : "";
        Jeu.FreezeAllEntities();
        Journal($"ouverte par J{s_Joueur + 1} ({titre}), bourse {Bourse}");
    }

    private static void Fermer()
    {
        s_Ouvert = false;
        Jeu.UnfreezeAllEntities();
        Journal("fermée");
    }

    private static int Bourse => *(int*)(s_PerChar + 0x26c);

    // ------------------------------------------------------------------ la manette

    private static void LireLaManette()
    {
        byte type = Jeu.Inp.s_PlayerSlots_005e9188[s_Joueur * 0x4c];
        byte* pad = Jeu.Inp.s_InputStateBase_00604a20 + type * 0x10;
        byte appuis = pad[2];
        ushort action = *(ushort*)(s_PerChar + 0x1fc4);
        bool acheter = (*(ushort*)(pad + 2) & action) != 0;

        int n = s_Lignes.Count;
        if (s_Question)
        {
            if ((appuis & (Haut | Bas | Gauche | Droite)) != 0) s_Oui = !s_Oui;
            if ((appuis & Menu) != 0) s_Question = false;
            else if (acheter)
            {
                s_Question = false;
                if (s_Oui) Acheter();
            }
            return;
        }

        if ((appuis & Menu) != 0) { Fermer(); return; }
        if (n == 0) return;
        if ((appuis & Haut) != 0) s_Curseur = (s_Curseur - 1 + n) % n;
        if ((appuis & Bas) != 0) s_Curseur = (s_Curseur + 1) % n;
        if ((appuis & Gauche) != 0) s_Curseur = Math.Max(0, s_Curseur - ParPage);
        if ((appuis & Droite) != 0) s_Curseur = Math.Min(n - 1, s_Curseur + ParPage);
        if ((appuis & (Haut | Bas | Gauche | Droite)) != 0) s_Message = "";
        if (acheter)
        {
            s_Question = true;
            s_Oui = true;
            s_Message = "";
        }
    }

    /// <summary>Étape 3c : l'achat réel (BoutiqueAchat), et son message.</summary>
    private static void Acheter()
    {
        if (s_Vendre != null)
        {
            Ligne vendue = s_Lignes[s_Curseur];
            int bourseAvant = Bourse;
            s_Message = s_Vendre(vendue);
            s_Lignes = s_Lister!();
            if (s_Curseur >= s_Lignes.Count) s_Curseur = Math.Max(0, s_Lignes.Count - 1);
            Journal($"vente : {vendue.Affiche} ({vendue.Prix}) — bourse {bourseAvant} → {Bourse} : {s_Message}");
            return;
        }

        var (nom, prix) = (s_Lignes[s_Curseur].Nom, s_Lignes[s_Curseur].Prix);
        int avant = Bourse;
        var resultat = BoutiqueAchat.Acheter(s_Acheteur, s_PerChar, nom, prix);
        s_Message = resultat switch
        {
            BoutiqueAchat.Resultat.Achete => $"Achete : {nom}",
            BoutiqueAchat.Resultat.PasAssezDOr => "Pas assez d'or.",
            BoutiqueAchat.Resultat.InventairePlein => "Inventaire plein.",
            _ => "Objet introuvable dans les donnees du jeu.",
        };
        Journal($"{resultat} : {nom} ({prix}) — bourse {avant} → {Bourse}");
    }

    // ------------------------------------------------------------------ le dessin

    private static uint* Dessiner(uint* otBase, uint* curseur)
    {
        LireLaManette();
        if (!s_Ouvert) return curseur;                   // fermée à cette image

        var catalogue = s_Lignes;
        int page = s_Curseur / ParPage, pages = Math.Max(1, (catalogue.Count + ParPage - 1) / ParPage);

        curseur = Ecrire(otBase, curseur, MargeG, LigneTitre, Ascii(s_Titre));
        string bourse = $"Bourse : {Bourse}";
        curseur = Ecrire(otBase, curseur, MargeD - bourse.Length * 8, LigneTitre, bourse);

        for (int i = 0; i < ParPage; i++)
        {
            int k = page * ParPage + i;
            if (k >= catalogue.Count) break;
            int y = LignePremier + i * Pas;
            string nom = Ascii(catalogue[k].Affiche);
            if (nom.Length > NomMax) nom = nom[..NomMax];
            if (k == s_Curseur) curseur = Ecrire(otBase, curseur, MargeG, y, ">");
            curseur = Ecrire(otBase, curseur, MargeG + 10, y, nom);
            string prix = catalogue[k].Prix.ToString(CultureInfo.InvariantCulture);
            curseur = Ecrire(otBase, curseur, PrixD - prix.Length * 8, y, prix);
        }

        Ligne? courante = catalogue.Count > 0 ? catalogue[s_Curseur] : null;
        var objet = courante?.Fiche != null
            ? new CatalogueObjets.Objet(courante.Fiche, courante.Vignette ?? new byte[0x140])
            : courante != null ? CatalogueObjets.Trouver(courante.Nom) : null;
        if (objet != null)
        {
            curseur = Apercu(otBase, curseur, objet.Vignette);
            if (!s_Question) curseur = Paragraphe(otBase, curseur, LigneDescription, Description(objet.Fiche));
            curseur = Paragraphe(otBase, curseur, LigneValeurs, Valeurs(objet.Fiche));
        }

        if (s_Question)
        {
            // La question prend la place du message, le choix celle de la description.
            curseur = Ecrire(otBase, curseur, MargeG, LigneMessage, Question);
            curseur = Ecrire(otBase, curseur, MargeG + 10, LigneDescription, "Oui");
            curseur = Ecrire(otBase, curseur, MargeG + 74, LigneDescription, "Non");
            curseur = Ecrire(otBase, curseur, s_Oui ? MargeG : MargeG + 64, LigneDescription, ">");
        }
        else if (s_Message.Length > 0) curseur = Ecrire(otBase, curseur, MargeG, LigneMessage, Ascii(s_Message));
        curseur = Ecrire(otBase, curseur, MargeG, LignePied, $"Page {page + 1}/{pages}");

        // Le fond EN DERNIER : ajouté en tête de la liste de l'ordering table, il est dessiné le
        // premier, donc sous le texte.
        return Rectangle(otBase, curseur, CadreX, CadreY, CadreL, CadreH, Fond, 0x62);
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
    private static string Ascii(string s)
    {
        var sb = new StringBuilder();
        foreach (char c in s.Replace("œ", "oe").Replace("Œ", "OE").Normalize(NormalizationForm.FormD))
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark) continue;
            sb.Append(c < 0x80 ? c : '?');
        }
        return sb.ToString();
    }

    private static void Journal(string message) => Console.Error.WriteLine($"[BOUTIQUE] {message}");
}
