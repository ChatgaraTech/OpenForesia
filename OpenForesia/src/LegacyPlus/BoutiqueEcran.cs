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

    // La mise en page, sur l'écran logique de 320 × 240, en police 8 × 8.
    private const int CadreX = 16, CadreY = 32, CadreL = 288, CadreH = 176;
    private const int MargeG = 24, MargeD = 296;
    private const int LigneTitre = 40, LignePremier = 58, Pas = 11, LigneMessage = 174, LignePied = 190;
    private const int NomMax = 20;                      // caractères
    private const int PrixD = 232;                      // bord droit de la colonne des prix

    // L'aperçu : l'icône de l'objet sous le curseur, 24 × 24 agrandie deux fois, à droite de la liste.
    private const int ApercuX = 244, ApercuY = LignePremier, Zoom = 2;

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

    private static uint* s_Atlas;                       // la police 8×8 : {0x40803000, 0x02300000}, comme s_GlyphAtlas_00566c28
    private static byte* s_Texte;                       // une ligne, en octets, terminée par un zéro
    private static StreamWriter? s_Journal;

    internal static bool Ouvert => s_Ouvert;

    internal static void Installer(StreamWriter? journal)
    {
        s_Journal = journal;
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

    internal static void Ouvrir(uint* acheteur, byte* perChar)
    {
        if (ReglagesBoutique.Catalogue.Count == 0) return;
        if (!CatalogueObjets.Construit)
            CatalogueObjets.Construire(ReglagesBoutique.Catalogue.Select(o => o.Nom), s_Journal);   // étape 3b, une fois
        s_Ouvert = true;
        s_Acheteur = acheteur;
        s_PerChar = perChar;
        s_Joueur = ((byte*)acheteur)[8];
        s_Curseur = 0;
        s_Message = "";
        Jeu.FreezeAllEntities();
        s_Journal?.WriteLine($"ouverte par J{s_Joueur + 1}, bourse {Bourse}");
    }

    private static void Fermer()
    {
        s_Ouvert = false;
        Jeu.UnfreezeAllEntities();
        s_Journal?.WriteLine("fermée");
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

        int n = ReglagesBoutique.Catalogue.Count;
        if ((appuis & Menu) != 0) { Fermer(); return; }
        if ((appuis & Haut) != 0) s_Curseur = (s_Curseur - 1 + n) % n;
        if ((appuis & Bas) != 0) s_Curseur = (s_Curseur + 1) % n;
        if ((appuis & Gauche) != 0) s_Curseur = Math.Max(0, s_Curseur - ParPage);
        if ((appuis & Droite) != 0) s_Curseur = Math.Min(n - 1, s_Curseur + ParPage);
        if ((appuis & (Haut | Bas | Gauche | Droite)) != 0) s_Message = "";
        if (acheter) Acheter();
    }

    /// <summary>Étape 3c : l'achat réel (BoutiqueAchat), et son message.</summary>
    private static void Acheter()
    {
        var (nom, prix) = ReglagesBoutique.Catalogue[s_Curseur];
        int avant = Bourse;
        var resultat = BoutiqueAchat.Acheter(s_Acheteur, s_PerChar, nom, prix);
        s_Message = resultat switch
        {
            BoutiqueAchat.Resultat.Achete => $"Achete : {nom}",
            BoutiqueAchat.Resultat.PasAssezDOr => "Pas assez d'or.",
            BoutiqueAchat.Resultat.InventairePlein => "Inventaire plein.",
            _ => "Objet introuvable dans les donnees du jeu.",
        };
        s_Journal?.WriteLine($"{resultat} : {nom} ({prix}) — bourse {avant} → {Bourse}");
    }

    // ------------------------------------------------------------------ le dessin

    private static uint* Dessiner(uint* otBase, uint* curseur)
    {
        LireLaManette();
        if (!s_Ouvert) return curseur;                   // fermée à cette image

        var catalogue = ReglagesBoutique.Catalogue;
        int page = s_Curseur / ParPage, pages = (catalogue.Count + ParPage - 1) / ParPage;

        curseur = Ecrire(otBase, curseur, MargeG, LigneTitre, "BOUTIQUE DE L'AUBERGE");
        string bourse = $"Bourse : {Bourse}";
        curseur = Ecrire(otBase, curseur, MargeD - bourse.Length * 8, LigneTitre, bourse);

        for (int i = 0; i < ParPage; i++)
        {
            int k = page * ParPage + i;
            if (k >= catalogue.Count) break;
            int y = LignePremier + i * Pas;
            string nom = Ascii(catalogue[k].Nom);
            if (nom.Length > NomMax) nom = nom[..NomMax];
            if (k == s_Curseur) curseur = Ecrire(otBase, curseur, MargeG, y, ">");
            curseur = Ecrire(otBase, curseur, MargeG + 10, y, nom);
            string prix = catalogue[k].Prix.ToString(CultureInfo.InvariantCulture);
            curseur = Ecrire(otBase, curseur, PrixD - prix.Length * 8, y, prix);
        }

        var objet = CatalogueObjets.Trouver(catalogue[s_Curseur].Nom);
        if (objet != null) curseur = Apercu(otBase, curseur, objet.Vignette);

        if (s_Message.Length > 0) curseur = Ecrire(otBase, curseur, MargeG, LigneMessage, Ascii(s_Message));
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
}
