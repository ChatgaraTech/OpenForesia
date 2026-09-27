// =====================================================================
// LP-008 — la boutique de l'auberge : l'armoire de la carte 82 (quête 0, « Le coffre »).
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-inn-shop). Contenu nouveau,
// demandé par Vincent : dans le jeu de 1998, l'or ne sert à rien. La carte 82 ne charge aucun
// modèle de personnage (étape 1), donc le point de vente est l'armoire, un décor relevé en jeu en
// (−1372, 400, 900).
//
// Ce fichier, c'est L'OUVERTURE (étape 2) : un membre debout devant l'armoire, qui la regarde et
// appuie sur son bouton d'action, ouvre la boutique ; l'appui est consommé, le joueur ne frappe pas
// l'armoire. L'écran lui-même est dans BoutiqueEcran.cs (étape 3a).
//
// L'étape 2 ouvrait une boîte de texte du jeu (BindMenuLabels, validée en jeu le 2026-09-27) ; le
// moteur de boîtes n'ayant pas de code de choix, l'étape 3a la remplace par l'écran. Ce qu'on y a
// appris du format des programmes de texte est dans le design du change (D3).
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using Jeu = OpenForesia.Blaze.Blaze;

namespace OpenForesia.LegacyPlus;

internal static unsafe class Boutique
{
    internal const string JournalFileName = "legacyplus-boutique.log";

    // Où : quête 0, carte 82 ; l'armoire, dans les unités des relevés (position monde >> 12).
    private const int Quete = 0, Carte = 82;
    private const int ArmoireX = -1372, ArmoireZ = 900;
    private const int Portee = 120;                     // unités
    private const int DemiAngle = 0x200;                // 45°, en 4096e de tour

    private static StreamWriter? s_Journal;

    internal static void Installer()
    {
        ReglagesBoutique.Lire();
        if (!ReglagesBoutique.Actif) return;

        try
        {
            s_Journal = new StreamWriter(Path.Combine(AppContext.BaseDirectory, JournalFileName), append: false) { AutoFlush = true };
            s_Journal.WriteLine($"# OpenForesia — la boutique de l'auberge ; {ReglagesBoutique.Catalogue.Count} objets au catalogue");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[BOUTIQUE] {JournalFileName} impossible à ouvrir ({ex.Message})");
        }

        BoutiqueEcran.Installer(s_Journal);

        Coutures.Pas precedent = Coutures.PasDuMembre;
        Coutures.PasDuMembre = (membre, pad, perChar, descripteur) =>
        {
            precedent(membre, pad, perChar, descripteur);
            if (!DevantLArmoire(membre, pad, perChar, descripteur)) return;

            // L'appui est consommé avant que le pas du membre ne le lise (le tampon pad[0x0e] se
            // remplit plus loin, depuis pad + 2).
            *(ushort*)(pad + 2) &= (ushort)~*(ushort*)(perChar + 0x1fc4);
            BoutiqueEcran.Ouvrir(membre, perChar);
        };
    }

    /// <summary>Le déclenchement (design D2).</summary>
    private static bool DevantLArmoire(uint* membre, byte* pad, byte* perChar, byte* descripteur)
    {
        if (BoutiqueEcran.Ouvert) return false;
        if ((Jeu.Res.s_Unknown_005de528 & 0xff) != Quete || Jeu.s_Unknown_005de52c != Carte) return false;

        ushort action = *(ushort*)(perChar + 0x1fc4);
        if ((*(ushort*)(pad + 2) & action) == 0) return false;                 // pas d'appui à cette image

        if ((*membre & 0x80000000) == 0 || (*membre & 0x50000000) != 0) return false;   // debout
        if ((descripteur[0x8c7] & 1) != 0) return false;                        // une boîte du jeu est ouverte

        byte* m = (byte*)membre;
        int dx = ArmoireX - (*(int*)(m + 0x1c) >> 12);
        int dz = ArmoireZ - (*(int*)(m + 0x24) >> 12);
        if (dx * dx + dz * dz > Portee * Portee) return false;

        // Avancer, c'est +sin(cap) en x et +cos(cap) en z : la direction de l'armoire est atan2(dx, dz).
        int versArmoire = (int)Math.Round(Math.Atan2(dx, dz) * 4096 / (2 * Math.PI)) & 0xfff;
        int cap = *(short*)(m + 0x32) & 0xfff;
        int ecart = (versArmoire - cap) & 0xfff;
        if (ecart > 0x800) ecart = 0x1000 - ecart;
        return ecart <= DemiAngle;
    }
}
