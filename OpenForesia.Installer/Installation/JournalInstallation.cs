// Ce qu'une installation a cree, pour pouvoir le retirer si elle echoue ou si le joueur annule.
//
// Seul ce qui est NEUF y entre : un fichier ou un dossier deja present avant l'installation (une
// installation precedente, des sauvegardes) n'est jamais retire par un nettoyage.

namespace OpenForesia.Installer.Installation;

internal sealed class JournalInstallation
{
    private readonly List<string> _fichiers = [];
    private readonly List<string> _dossiers = [];

    /// <summary>Note un fichier sur le point d'etre ecrit, s'il n'existe pas encore.</summary>
    internal void NoterFichier(string chemin)
    {
        if (!File.Exists(chemin)) _fichiers.Add(chemin);
    }

    /// <summary>Oublie un fichier (un temporaire renomme, par exemple).</summary>
    internal void Oublier(string chemin) => _fichiers.Remove(chemin);

    /// <summary>Cree un dossier et ses parents manquants, en notant chacun de ceux qu'il cree.</summary>
    internal void CreerDossier(string chemin)
    {
        if (Directory.Exists(chemin)) return;
        string? parent = Path.GetDirectoryName(chemin);
        if (parent != null) CreerDossier(parent);
        Directory.CreateDirectory(chemin);
        _dossiers.Add(chemin);
    }

    /// <summary>Retire les fichiers notes, puis les dossiers notes du plus profond au moins
    /// profond, et seulement s'ils sont vides. Ne leve jamais : c'est un chemin d'erreur.</summary>
    internal void Nettoyer()
    {
        foreach (string fichier in _fichiers)
        {
            try { File.Delete(fichier); } catch (IOException) { } catch (UnauthorizedAccessException) { }
        }

        for (int i = _dossiers.Count - 1; i >= 0; i--)
        {
            try
            {
                if (!Directory.EnumerateFileSystemEntries(_dossiers[i]).Any()) Directory.Delete(_dossiers[i]);
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }

        _fichiers.Clear();
        _dossiers.Clear();
    }
}
