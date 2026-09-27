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

    /// <summary>`[Difficulte] PvMonstres` — multiplicateur des PV des entités de scène à leur naissance, en centièmes (LP-002).</summary>
    internal static int PvMonstres { get; private set; } = 100;

    /// <summary>`[Difficulte] DegatsMonstres` — multiplicateur des dégâts reçus par le groupe, en centièmes (LP-001).</summary>
    internal static int DegatsMonstres { get; private set; } = 100;

    /// <summary>`[Magie] ScalingMagique` — poids de la MAG dans le montant des sorts du groupe, en centièmes ; 0 = jeu d'origine (LP-003).</summary>
    internal static int ScalingMagique { get; private set; } = 0;

    /// <summary>`[Combat] InvulnerabiliteEnnemis` — `oui` : le verrou partagé d'origine ; `non` : un verrou par attaquant (LP-004).</summary>
    internal static bool InvulnerabiliteEnnemis { get; private set; } = true;

    /// <summary>`[Magie] DeplacementEnIncantation` — part de la vitesse de marche gardée pendant une incantation, en centièmes ; 0 = jeu d'origine (LP-006).</summary>
    internal static int DeplacementEnIncantation { get; private set; } = 0;

    /// <summary>`[Butin] RamassageAuto` — `oui` : pièces et cristaux d'expérience ramassés une fois posés, sans marcher dessus (LP-005).</summary>
    internal static bool RamassageAuto { get; private set; } = false;

    /// <summary>`[Diagnostic] TraceCombat` — `oui` : chaque coup de mêlée du groupe est journalisé dans `legacyplus-combat.log`. Outil de mise au point, absent du modèle de l'installeur.</summary>
    internal static bool TraceCombat { get; private set; } = false;

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
            $"[LEGACY++] {FileName} lu — PvMonstres=x{Ecrire(PvMonstres)}, DegatsMonstres=x{Ecrire(DegatsMonstres)}, " +
            $"ScalingMagique={Ecrire(ScalingMagique)}, InvulnerabiliteEnnemis={(InvulnerabiliteEnnemis ? "oui" : "non")}, " +
            $"RamassageAuto={(RamassageAuto ? "oui" : "non")}, DeplacementEnIncantation={Ecrire(DeplacementEnIncantation)}");
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
            if (LireMultiplicateur(value, 10, 1000, out centiemes)) PvMonstres = centiemes;
            else Refuser(section, key, value, "un multiplicateur de 0.1 à 10, 1 = jeu d'origine");
        }
        else if (Est("Difficulte", "DegatsMonstres"))
        {
            if (LireMultiplicateur(value, 10, 1000, out centiemes)) DegatsMonstres = centiemes;
            else Refuser(section, key, value, "un multiplicateur de 0.1 à 10, 1 = jeu d'origine");
        }
        else if (Est("Magie", "ScalingMagique"))
        {
            if (LireMultiplicateur(value, 0, 500, out centiemes)) ScalingMagique = centiemes;
            else Refuser(section, key, value, "de 0 à 5, 0 = jeu d'origine");
        }
        else if (Est("Combat", "InvulnerabiliteEnnemis"))
        {
            if (value.Equals("oui", StringComparison.OrdinalIgnoreCase)) InvulnerabiliteEnnemis = true;
            else if (value.Equals("non", StringComparison.OrdinalIgnoreCase)) InvulnerabiliteEnnemis = false;
            else Refuser(section, key, value, "oui (jeu d'origine) ou non");
        }
        else if (Est("Magie", "DeplacementEnIncantation"))
        {
            if (LireMultiplicateur(value, 0, 100, out centiemes)) DeplacementEnIncantation = centiemes;
            else Refuser(section, key, value, "de 0 (jeu d'origine) à 1 (vitesse pleine)");
        }
        else if (Est("Butin", "RamassageAuto"))
        {
            if (value.Equals("oui", StringComparison.OrdinalIgnoreCase)) RamassageAuto = true;
            else if (value.Equals("non", StringComparison.OrdinalIgnoreCase)) RamassageAuto = false;
            else Refuser(section, key, value, "oui ou non (jeu d'origine)");
        }
        else if (Est("Diagnostic", "TraceCombat"))
        {
            if (value.Equals("oui", StringComparison.OrdinalIgnoreCase)) TraceCombat = true;
            else if (value.Equals("non", StringComparison.OrdinalIgnoreCase)) TraceCombat = false;
            else Refuser(section, key, value, "oui ou non");
        }
        else
        {
            Console.Error.WriteLine($"[LEGACY++] clé inconnue ignorée : [{section}] {key}");
        }
    }

    /// <summary>
    /// Un multiplicateur : `1`, `2`, `1.5` ou `1,5`. Rendu en centièmes, arrondi au plus proche
    /// (`1.333` donne 133), et refusé hors de [<paramref name="min"/>, <paramref name="max"/>]
    /// centièmes.
    /// </summary>
    private static bool LireMultiplicateur(string value, int min, int max, out int centiemes)
    {
        centiemes = 0;
        if (!decimal.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.AllowDecimalPoint,
                              System.Globalization.CultureInfo.InvariantCulture, out decimal m))
            return false;
        decimal c = Math.Round(m * 100m, MidpointRounding.AwayFromZero);
        if (c < min || c > max) return false;
        centiemes = (int)c;
        return true;
    }

    private static void Refuser(string section, string key, string value, string attendu) =>
        Console.Error.WriteLine(
            $"[LEGACY++] valeur refusée, défaut conservé : [{section}] {key} = {value} (attendu : {attendu})");
}
