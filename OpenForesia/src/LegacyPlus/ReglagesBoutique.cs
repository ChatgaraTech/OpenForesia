// =====================================================================
// Les boutiques des marchands du hub — un fichier par marchand, dans `LegacyPlus\` à côté de
// l'exécutable (LP-010 ; avant lui `Boutique.ini`, la boutique de l'armoire, LP-008, retirée).
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-28-add-legacy-plus-hub-merchants). Même contrat que
// `LegacyPlus.ini` : lu une fois, jamais écrit ; fichier absent ou `Actif = non` : pas de marchand.
// Les modèles sont générés par `OpenForesia.Tools/generer-boutique.mjs` et retouchés à la main.
//
//   [Boutique]   Actif = oui | non
//   [Catalogue]  <nom exact de l'objet> = <prix en pièces d'or>      ; un « ; » retire la ligne
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal sealed class ReglagesBoutique
{
    /// <summary>Le dossier des fichiers de boutique, à côté de l'exécutable.</summary>
    internal const string Dossier = "LegacyPlus";

    internal string Fichier { get; }
    internal bool Actif { get; private set; }

    /// <summary>Le catalogue, dans l'ordre du fichier : le nom exact de l'objet et son prix.</summary>
    internal IReadOnlyList<(string Nom, int Prix)> Catalogue => _catalogue;

    private readonly List<(string Nom, int Prix)> _catalogue = new();

    private ReglagesBoutique(string fichier) => Fichier = fichier;

    /// <summary>Lit `LegacyPlus\<paramref name="fichier"/>` ; absent ou illisible : boutique inactive.</summary>
    internal static ReglagesBoutique Lire(string fichier)
    {
        var r = new ReglagesBoutique(fichier);
        string path = Path.Combine(AppContext.BaseDirectory, Dossier, fichier);
        string[] lines;
        try
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"[BOUTIQUE] {Dossier}\\{fichier} absent — pas de marchand");
                return r;
            }
            lines = File.ReadAllLines(path);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[BOUTIQUE] {Dossier}\\{fichier} illisible ({ex.Message}) — pas de marchand");
            return r;
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
                if (value.Equals("oui", StringComparison.OrdinalIgnoreCase)) r.Actif = true;
                else if (value.Equals("non", StringComparison.OrdinalIgnoreCase)) r.Actif = false;
                else Console.Error.WriteLine($"[BOUTIQUE] {fichier} : valeur refusée, Actif = {value} (attendu : oui ou non)");
            }
            else if (section.Equals("Catalogue", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(value, System.Globalization.NumberStyles.None,
                                 System.Globalization.CultureInfo.InvariantCulture, out int prix)
                    && prix >= 1 && prix <= 99999)
                {
                    r._catalogue.Add((key, prix));
                }
                else
                {
                    Console.Error.WriteLine($"[BOUTIQUE] {fichier} : prix refusé, ligne ignorée : {key} = {value} (attendu : 1 à 99999)");
                }
            }
        }

        Console.Error.WriteLine($"[BOUTIQUE] {Dossier}\\{fichier} lu — {(r.Actif ? "active" : "inactive")}, {r._catalogue.Count} objets au catalogue");
        return r;
    }
}
