// =====================================================================
// La boutique de l'auberge — `Boutique.ini`, à côté de l'exécutable (LP-008).
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-inn-shop). Même contrat que
// `LegacyPlus.ini` : lu une fois, jamais écrit ; fichier absent ou `Actif = non` : le jeu de 1998.
// Le modèle est généré par `OpenForesia.Tools/generer-boutique.mjs` et retouché à la main.
//
//   [Boutique]   Actif = oui | non
//   [Catalogue]  <nom exact de l'objet> = <prix en pièces d'or>      ; un « ; » retire la ligne
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static class ReglagesBoutique
{
    internal const string FileName = "Boutique.ini";

    internal static bool Actif { get; private set; }

    /// <summary>Le catalogue, dans l'ordre du fichier : le nom exact de l'objet et son prix.</summary>
    internal static IReadOnlyList<(string Nom, int Prix)> Catalogue => s_Catalogue;

    private static readonly List<(string Nom, int Prix)> s_Catalogue = new();

    internal static void Lire()
    {
        string path = Path.Combine(AppContext.BaseDirectory, FileName);
        string[] lines;
        try
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"[BOUTIQUE] {FileName} absent — pas de boutique");
                return;
            }
            lines = File.ReadAllLines(path);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[BOUTIQUE] {FileName} illisible ({ex.Message}) — pas de boutique");
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

            int equals = line.LastIndexOf('=');
            if (equals <= 0) continue;
            string key = line[..equals].Trim();
            string value = line[(equals + 1)..].Trim();

            if (section.Equals("Boutique", StringComparison.OrdinalIgnoreCase)
                && key.Equals("Actif", StringComparison.OrdinalIgnoreCase))
            {
                if (value.Equals("oui", StringComparison.OrdinalIgnoreCase)) Actif = true;
                else if (value.Equals("non", StringComparison.OrdinalIgnoreCase)) Actif = false;
                else Console.Error.WriteLine($"[BOUTIQUE] valeur refusée : Actif = {value} (attendu : oui ou non)");
            }
            else if (section.Equals("Catalogue", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(value, System.Globalization.NumberStyles.None,
                                 System.Globalization.CultureInfo.InvariantCulture, out int prix)
                    && prix >= 1 && prix <= 99999)
                {
                    s_Catalogue.Add((key, prix));
                }
                else
                {
                    Console.Error.WriteLine($"[BOUTIQUE] prix refusé, ligne ignorée : {key} = {value} (attendu : 1 à 99999)");
                }
            }
        }

        Console.Error.WriteLine($"[BOUTIQUE] {FileName} lu — {(Actif ? "active" : "inactive")}, {s_Catalogue.Count} objets au catalogue");
    }
}
