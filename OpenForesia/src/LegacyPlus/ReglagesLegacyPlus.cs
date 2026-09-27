// =====================================================================
// Les tweaks choisis par le joueur — `LegacyPlus.ini`, à côté de l'exécutable.
//
// Hors reconstruction (legacy ++, tweaks LP-NNN, openspec/changes/add-legacy-plus-tweaks-difficulty).
// Même contrat qu'`Affichage.ini` : lu une fois, jamais écrit ; fichier absent, clé absente ou
// valeur refusée = la valeur par défaut, une ligne de journal par valeur refusée ; rien ici ne peut
// empêcher le jeu de démarrer. Tous les défauts rendent le jeu de 1998.
//
// Analyseur écrit à la main (UTF-8, commentaires accentués ; release élaguée, pas de réflexion).
// Il n'est pas partagé avec `Host/DisplaySettings.cs`, dont l'analyseur est inséparable de ses
// propres clés : l'en extraire aurait changé un code validé pour rien (design D5).
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static class ReglagesLegacyPlus
{
    internal const string FileName = "LegacyPlus.ini";

    // Un multiplicateur s'écrit `1`, `2`, `1.5` ou `1,5` dans le fichier ; il est gardé en
    // CENTIÈMES (1 = 100) pour que les tweaks calculent en entiers, sans flottant.
    private const int CentiemesMin = 10;       // × 0,1
    private const int CentiemesMax = 1000;     // × 10

    /// <summary>`[Difficulte] PvMonstres` — multiplicateur des PV des entités de scène à leur naissance, en centièmes (LP-002).</summary>
    internal static int PvMonstres { get; private set; } = 100;

    /// <summary>`[Difficulte] DegatsMonstres` — multiplicateur des dégâts reçus par le groupe, en centièmes (LP-001).</summary>
    internal static int DegatsMonstres { get; private set; } = 100;

    /// <summary>Lit `LegacyPlus.ini`. Appelée une fois, par le constructeur statique de <see cref="Coutures"/>.</summary>
    internal static void Lire()
    {
        string path = Path.Combine(AppContext.BaseDirectory, FileName);
        string[] lines;
        try
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"[LEGACY++] {FileName} absent — aucun tweak actif");
                return;
            }
            lines = File.ReadAllLines(path);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LEGACY++] {FileName} illisible ({ex.Message}) — aucun tweak actif");
            return;
        }

        string section = "";
        foreach (string raw in lines)
        {
            string line = raw;
            int comment = line.IndexOf(';');
            if (comment >= 0) line = line[..comment];
            line = line.Trim();
            if (line.Length == 0) continue;

            if (line[0] == '[' && line[^1] == ']')
            {
                section = line[1..^1].Trim();
                continue;
            }

            int equals = line.IndexOf('=');
            if (equals <= 0) continue;
            Appliquer(section, line[..equals].Trim(), line[(equals + 1)..].Trim());
        }

        Console.Error.WriteLine(
            $"[LEGACY++] {FileName} lu — PvMonstres=x{Ecrire(PvMonstres)}, DegatsMonstres=x{Ecrire(DegatsMonstres)}");
    }

    private static string Ecrire(int centiemes) =>
        (centiemes / 100m).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);

    private static void Appliquer(string section, string key, string value)
    {
        bool Est(string s, string k) =>
            section.Equals(s, StringComparison.OrdinalIgnoreCase) &&
            key.Equals(k, StringComparison.OrdinalIgnoreCase);

        int centiemes;
        if (Est("Difficulte", "PvMonstres"))
        {
            if (LireMultiplicateur(value, out centiemes)) PvMonstres = centiemes;
            else Refuser(section, key, value);
        }
        else if (Est("Difficulte", "DegatsMonstres"))
        {
            if (LireMultiplicateur(value, out centiemes)) DegatsMonstres = centiemes;
            else Refuser(section, key, value);
        }
        else
        {
            Console.Error.WriteLine($"[LEGACY++] clé inconnue ignorée : [{section}] {key}");
        }
    }

    /// <summary>
    /// Un multiplicateur de 0,1 à 10 : `1`, `2`, `1.5` ou `1,5`. Rendu en centièmes, arrondi au
    /// plus proche (`1.333` donne 133).
    /// </summary>
    private static bool LireMultiplicateur(string value, out int centiemes)
    {
        centiemes = 0;
        if (!decimal.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.AllowDecimalPoint,
                              System.Globalization.CultureInfo.InvariantCulture, out decimal m))
            return false;
        decimal c = Math.Round(m * 100m, MidpointRounding.AwayFromZero);
        if (c < CentiemesMin || c > CentiemesMax) return false;
        centiemes = (int)c;
        return true;
    }

    private static void Refuser(string section, string key, string value) =>
        Console.Error.WriteLine(
            $"[LEGACY++] valeur refusée, défaut conservé : [{section}] {key} = {value} (attendu : un multiplicateur de 0.1 à 10, 1 = jeu d'origine)");
}
