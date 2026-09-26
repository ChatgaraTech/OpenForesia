// D'ou viennent les fichiers du jeu (l'executable et son runtime) que l'installeur depose.
//
// Dans une release, du zip `jeu.zip` embarque dans l'installeur par publish-release.ps1 : la sortie
// de `dotnet publish OpenForesia -c Release -r win-x86 --self-contained`. En developpement, tant
// que ce zip n'existe pas, d'un dossier de build passe par `--jeu <dossier>` — dont on ecarte ce
// qui n'est pas le jeu (les donnees extraites, les sauvegardes, les reglages, les symboles).

using System.IO.Compression;

namespace OpenForesia.Installer.Installation;

internal sealed record FichierDuJeu(string CheminRelatif, long Taille, Func<Stream> Ouvrir);

internal sealed class SourceDuJeu
{
    internal const string RessourceZip = "jeu.zip";

    private static readonly string[] s_DossiersEcartes = ["French", "Snd", "Music", "save"];
    // `.ini` : un Affichage.ini d'essai dans la build ne doit pas ecraser celui du joueur — le
    // fichier par defaut vient du modele embarque (Installation.DeposerReglagesAffichage).
    private static readonly string[] s_ExtensionsEcartees = [".cfg", ".pdb", ".log", ".txt", ".ini"];

    internal IReadOnlyList<FichierDuJeu> Fichiers { get; }
    internal long Taille => Fichiers.Sum(f => f.Taille);

    private SourceDuJeu(IReadOnlyList<FichierDuJeu> fichiers) => Fichiers = fichiers;

    /// <summary>Le jeu embarque, ou null si cet installeur n'en porte pas (build de developpement).</summary>
    internal static SourceDuJeu? Embarquee()
    {
        Stream? flux = typeof(SourceDuJeu).Assembly.GetManifestResourceStream(RessourceZip);
        if (flux == null) return null;

        // L'archive reste ouverte pour la duree du processus : c'est une ressource du module.
        var archive = new ZipArchive(flux, ZipArchiveMode.Read);
        var fichiers = archive.Entries
            .Where(e => !e.FullName.EndsWith('/'))
            .Select(e => new FichierDuJeu(e.FullName.Replace('/', '\\'), e.Length, e.Open))
            .ToList();
        return new SourceDuJeu(fichiers);
    }

    /// <summary>
    /// En developpement, sans jeu embarque ni --jeu : la build du jeu du depot, retrouvee en
    /// remontant jusqu'a <c>OpenForesia.sln</c> depuis l'installeur. Null hors du depot, ou si le
    /// jeu n'a pas ete compile — une release, elle, embarque toujours son jeu.
    /// </summary>
    internal static SourceDuJeu? DuDepot()
    {
        for (var dossier = new DirectoryInfo(AppContext.BaseDirectory); dossier != null; dossier = dossier.Parent)
        {
            if (!File.Exists(Path.Combine(dossier.FullName, "OpenForesia.sln"))) continue;
            string build = Path.Combine(dossier.FullName, "OpenForesia", "bin", "Debug", "net10.0");
            return File.Exists(Path.Combine(build, "OpenForesia.exe")) ? Dossier(build) : null;
        }
        return null;
    }

    /// <summary>Les fichiers du jeu pris dans un dossier de build (developpement).</summary>
    internal static SourceDuJeu Dossier(string dossier)
    {
        dossier = Path.GetFullPath(dossier);
        if (!File.Exists(Path.Combine(dossier, "OpenForesia.exe")))
            throw new FileNotFoundException($"OpenForesia.exe est introuvable dans {dossier}.");

        var fichiers = new List<FichierDuJeu>();
        foreach (string chemin in Directory.EnumerateFiles(dossier, "*", SearchOption.AllDirectories))
        {
            string relatif = Path.GetRelativePath(dossier, chemin);
            string premier = relatif.Split('\\')[0];
            if (relatif.Contains('\\') && s_DossiersEcartes.Contains(premier, StringComparer.OrdinalIgnoreCase)) continue;
            if (s_ExtensionsEcartees.Contains(Path.GetExtension(chemin), StringComparer.OrdinalIgnoreCase)) continue;

            string source = chemin;
            fichiers.Add(new FichierDuJeu(relatif, new FileInfo(chemin).Length, () => File.OpenRead(source)));
        }
        return new SourceDuJeu(fichiers);
    }
}
