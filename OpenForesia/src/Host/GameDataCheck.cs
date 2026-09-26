// =====================================================================
// La vérification des données du jeu, au démarrage — ce qui remplace la recherche du CD.
//
// Le jeu de 1998 balayait les lecteurs à la recherche de son disque et bouclait sur « insérez le
// CD » tant qu'il ne le trouvait pas. OpenForesia lit ses données dans son propre dossier, où
// l'installeur les a extraites depuis l'ISO du joueur (openspec/changes/install-from-iso). Il
// vérifie donc, avant toute fenêtre, que chaque fichier attendu est là et a la bonne taille ; s'il
// en manque, un seul message le dit et le jeu s'arrête.
//
// La liste est `editions.json`, générée par l'installeur depuis l'ISO de référence et embarquée
// ici par un lien (OpenForesia.csproj) : un seul exemplaire, jamais tapé à la main. Seules
// l'existence et la taille sont contrôlées — instantané ; les sommes MD5 sont l'affaire de
// l'installeur.
// =====================================================================

using System.Runtime.InteropServices;
using System.Text.Json;

namespace OpenForesia.Blaze;

internal static class GameDataCheck
{
    private sealed record Fichier(string chemin, long taille);
    private sealed record Edition(string nom, List<Fichier> fichiers);
    private sealed record Catalogue(List<Edition> editions);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);
    private const uint MbIconError = 0x10;

    /// <summary>
    /// Vrai si le répertoire courant contient toutes les données d'une édition connue. Sinon,
    /// affiche le message et rend faux ; l'appelant quitte.
    /// </summary>
    internal static bool Run()
    {
        Catalogue catalogue;
        using (Stream flux = typeof(GameDataCheck).Assembly.GetManifestResourceStream("editions.json")
                   ?? throw new InvalidOperationException("editions.json n'est pas embarqué dans le jeu."))
        {
            catalogue = JsonSerializer.Deserialize<Catalogue>(flux)
                        ?? throw new InvalidOperationException("editions.json est illisible.");
        }

        // L'édition la plus complète sur le disque : c'est elle qu'on décrit si rien ne va.
        List<string>? meilleurs = null;
        foreach (Edition edition in catalogue.editions)
        {
            var problemes = new List<string>();
            foreach (Fichier fichier in edition.fichiers)
            {
                var info = new FileInfo(fichier.chemin);
                if (!info.Exists || info.Length != fichier.taille) problemes.Add(fichier.chemin);
            }

            if (problemes.Count == 0) return true;
            if (meilleurs == null || problemes.Count < meilleurs.Count) meilleurs = problemes;
        }

        MessageBoxW(IntPtr.Zero, Message(meilleurs, catalogue), "OpenForesia", MbIconError);
        return false;
    }

    private static string Message(List<string>? problemes, Catalogue catalogue)
    {
        string dossier = Directory.GetCurrentDirectory();
        int attendus = catalogue.editions.Count == 0 ? 0 : catalogue.editions.Min(e => e.fichiers.Count);

        string constat = problemes == null || problemes.Count >= attendus
            ? $"Les données du jeu sont introuvables dans :\n{dossier}"
            : $"Les données du jeu sont incomplètes dans :\n{dossier}\n\n" +
              $"{problemes.Count} fichier{(problemes.Count > 1 ? "s manquent ou sont abîmés" : " manque ou est abîmé")}, " +
              $"dont :\n{problemes[0]}";

        return constat + "\n\nRéinstallez OpenForesia avec son installeur (OpenForesia-Setup.exe) " +
               "et l'image ISO du disque du jeu.";
    }
}
