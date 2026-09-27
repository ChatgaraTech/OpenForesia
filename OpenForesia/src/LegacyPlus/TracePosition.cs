// =====================================================================
// Diagnostic — le relevé de position du joueur 1, `legacyplus-position.log`.
//
// Hors reconstruction : un outil de mise au point, pas un tweak (aucun numéro LP). Activé par
// `[Diagnostic] TracePosition = oui` ; absent du modèle de l'installeur. Écrit pour placer le
// marchand de l'auberge (quête 0, carte 82) : on marche jusqu'à l'endroit voulu, on s'arrête une
// seconde, et la ligne donne la position dans les unités des relevés du bestiaire (`formations.csv`,
// `decor.csv` : la position monde décalée de 12 bits) et le cap.
//
// Il lit le membre du groupe que la couture `ButinAtteint` reçoit à chaque image (le pas d'un membre,
// `PartyMemberStep.cs`) : aucune ligne de plus dans le legacy. Il ne change aucune valeur.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class TracePosition
{
    internal const string FileName = "legacyplus-position.log";

    /// <summary>Images d'immobilité avant d'écrire une ligne : une seconde.</summary>
    private const int ImagesArret = 60;

    private static StreamWriter? s_Journal;

    /// <summary>Écart toléré, en unités, pour juger le joueur immobile.</summary>
    private const int Tolerance = 4;

    /// <summary>Une ligne de secours toutes les cinq secondes.</summary>
    private const int ImagesSecours = 300;

    private static int s_X, s_Z, s_Immobile;
    private static readonly int[] s_Images = new int[4];
    private static bool s_Ecrit;

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.TracePosition) return;

        try
        {
            s_Journal = new StreamWriter(Path.Combine(AppContext.BaseDirectory, FileName), append: false) { AutoFlush = true };
            s_Journal.WriteLine("# OpenForesia — relevé de position du joueur 1 : « arrêt » après une seconde immobile, « position » toutes les cinq secondes");
            s_Journal.WriteLine("# x, y, z : unités de formations.csv / decor.csv ; cap : 0..4095 (4096 = un tour)");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LEGACY++] {FileName} impossible à ouvrir ({ex.Message}) — pas de relevé");
            return;
        }

        Coutures.Contact precedent = Coutures.ButinAtteint;
        Coutures.ButinAtteint = (membre, touche, poolButin) =>
        {
            byte* m = (byte*)membre;
            Observer(m);
            return precedent(membre, touche, poolButin);
        };
    }

    private static void Observer(byte* m)
    {
        int x = *(int*)(m + 0x1c) >> 12, y = *(int*)(m + 0x20) >> 12, z = *(int*)(m + 0x24) >> 12;
        int cap = *(short*)(m + 0x32) & 0xfff;
        int joueur = m[8];

        // La ligne de secours : toutes les cinq secondes, pour chaque joueur, quoi qu'il arrive.
        if (joueur < 4 && ++s_Images[joueur] % ImagesSecours == 0) Ecrire(joueur, "position", x, y, z, cap);
        if (joueur != 0) return;

        // L'arrêt se juge sur le plan du sol seulement, à quelques unités près : la hauteur est
        // recalée sur le sol à chaque image et l'attente fait osciller le personnage.
        if (Math.Abs(x - s_X) > Tolerance || Math.Abs(z - s_Z) > Tolerance)
        {
            s_X = x; s_Z = z;
            s_Immobile = 0;
            s_Ecrit = false;
            return;
        }

        if (s_Ecrit || ++s_Immobile < ImagesArret) return;
        s_Ecrit = true;
        Ecrire(joueur, "arrêt   ", x, y, z, cap);
    }

    private static void Ecrire(int joueur, string quoi, int x, int y, int z, int cap) =>
        s_Journal?.WriteLine($"J{joueur + 1} {quoi} : x = {x}, y = {y}, z = {z}, cap = {cap}");
}
