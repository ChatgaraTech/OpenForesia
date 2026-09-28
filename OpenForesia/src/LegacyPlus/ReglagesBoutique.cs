// =====================================================================
// Les boutiques des marchands du hub — un fichier par marchand, dans `LegacyPlus\` à côté de
// l'exécutable (LP-010 ; avant lui `Boutique.ini`, la boutique de l'armoire, LP-008, retirée).
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-28-add-legacy-plus-hub-merchants). Même contrat que
// `LegacyPlus.ini` : lu une fois, jamais écrit. Le marchand est là dès que le hub l'est
// (`[Auberge] Hub = oui`) ; fichier absent ou illisible : catalogue vide. La clé `[Boutique] Actif`
// des premiers fichiers est retirée (Vincent, 2026-09-28) : encore présente chez un joueur, ignorée.
// Les modèles sont générés par `OpenForesia.Tools/generer-boutique.mjs` et retouchés à la main.
//
//   [Catalogue]  <nom exact de l'objet> = <prix en pièces d'or>      ; un « ; » retire la ligne
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal sealed class ReglagesBoutique
{
    /// <summary>Le dossier des fichiers de boutique, à côté de l'exécutable.</summary>
    internal const string Dossier = "LegacyPlus";

    internal string Fichier { get; }

    /// <summary>`[Vente] Taux` — la Voleuse (LP-011) rachète au taux de ce pourcentage du prix d'achat ; 50 par défaut.</summary>
    internal int Taux { get; private set; } = 50;

    /// <summary>Le catalogue, dans l'ordre du fichier : le nom exact de l'objet et son prix.</summary>
    internal IReadOnlyList<(string Nom, int Prix)> Catalogue => _catalogue;

    private readonly List<(string Nom, int Prix)> _catalogue = new();

    private ReglagesBoutique(string fichier) => Fichier = fichier;

    /// <summary>Lit `LegacyPlus\<paramref name="fichier"/>` ; absent ou illisible : catalogue vide.</summary>
    internal static ReglagesBoutique Lire(string fichier)
    {
        var r = new ReglagesBoutique(fichier);
        string path = Path.Combine(AppContext.BaseDirectory, Dossier, fichier);
        string[] lines;
        try
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"[BOUTIQUE] {Dossier}\\{fichier} absent — catalogue vide");
                return r;
            }
            lines = File.ReadAllLines(path);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[BOUTIQUE] {Dossier}\\{fichier} illisible ({ex.Message}) — catalogue vide");
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

            if (section.Equals("Vente", StringComparison.OrdinalIgnoreCase)
                     && key.Equals("Taux", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(value, System.Globalization.NumberStyles.None,
                                 System.Globalization.CultureInfo.InvariantCulture, out int taux) && taux >= 1 && taux <= 100)
                    r.Taux = taux;
                else
                    Console.Error.WriteLine($"[BOUTIQUE] {fichier} : valeur refusée, Taux = {value} (attendu : 1 à 100)");
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

        Console.Error.WriteLine($"[BOUTIQUE] {Dossier}\\{fichier} lu — {r._catalogue.Count} objets au catalogue");
        return r;
    }
}
