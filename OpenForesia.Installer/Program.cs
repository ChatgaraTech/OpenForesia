// OpenForesia-Setup : l'installeur du jeu depuis l'ISO du joueur.
//
// Sans argument, il ouvre la fenetre d'installation, avec le jeu qu'il embarque. En developpement,
// tant que publish-release.ps1 n'a pas produit ce jeu embarque, il prend la build du jeu du depot
// (OpenForesia\bin\Debug\net10.0, retrouve en remontant jusqu'a OpenForesia.sln), ou :
//
//   --jeu <dossier>
//       ouvre la fenetre en prenant les fichiers du jeu dans un dossier de build
//       (OpenForesia\bin\Debug\net10.0 ; les donnees, save\ et le .cfg en sont ecartes)
//
//   --desinstaller [<dossier>]
//       desinstalle le jeu ; c'est ainsi que Uninstall.exe (une copie de cet installeur, deposee
//       dans le dossier du jeu) est lance par le menu Demarrer et par « Applications installees »
//
// Deux modes caches servent au developpeur et ecrivent dans la console qui l'a lance :
//
//   --generer-edition <iso> <editions.json> [--nom "<nom>"]
//       ajoute (ou remplace) l'edition de cette image dans le catalogue
//   --extraire-donnees <iso> <dossier>
//       extrait et verifie les donnees, sans fenetre — pour une installation de developpement

using System.Runtime.InteropServices;
using OpenForesia.Installer.Editions;
using OpenForesia.Installer.Extraction;
using OpenForesia.Installer.Installation;
using OpenForesia.Installer.Interface;
using OpenForesia.Installer.Iso;

namespace OpenForesia.Installer;

internal static class Program
{
    [DllImport("kernel32.dll")] private static extern bool AttachConsole(int processId);
    private const int ProcessusParent = -1;

    [STAThread]
    private static int Main(string[] args)
    {
        // Copie dans le dossier du jeu sous le nom Uninstall.exe, l'installeur desinstalle — y
        // compris sur un simple double-clic, sans argument : c'est ce que le joueur attend de ce nom.
        if (args.Length > 0 && args[0] == "--desinstaller")
            return Desinstallation.Executer(args);
        string nom = Path.GetFileName(Environment.ProcessPath) ?? "";
        if (string.Equals(nom, Desinstallation.Executable, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(nom, Desinstallation.AncienExecutable, StringComparison.OrdinalIgnoreCase))
            return Desinstallation.Executer(["--desinstaller"]);

        if (args.Length > 0 && args[0].StartsWith("--", StringComparison.Ordinal) && args[0] != "--jeu")
            return ModeCache(args);

        // Le jeu a deposer : celui qu'embarque une release, ou — en developpement, tant que
        // publish-release.ps1 n'a pas produit de zip — un dossier de build passe par --jeu.
        SourceDuJeu? jeu;
        try
        {
            jeu = args.Length >= 2 && args[0] == "--jeu"
                ? SourceDuJeu.Dossier(args[1])
                : SourceDuJeu.Embarquee() ?? SourceDuJeu.DuDepot();
        }
        catch (FileNotFoundException e)
        {
            Natif.MessageBox(IntPtr.Zero, e.Message, "OpenForesia", Natif.MB_OK | Natif.MB_ICONERROR);
            return 1;
        }
        if (jeu == null)
        {
            Natif.MessageBox(IntPtr.Zero, "Cet installeur ne contient pas le jeu. Téléchargez OpenForesia-Setup.exe " +
                                          "depuis la page des versions du projet.", "OpenForesia",
                Natif.MB_OK | Natif.MB_ICONERROR);
            return 1;
        }

        FenetreInstallation.Executer(jeu);
        return 0;
    }

    private static int ModeCache(string[] args)
    {
        // Un WinExe n'a pas de console : on se rattache a celle de l'appelant. Quand la sortie est
        // redirigee (un script), les poignees heritees suffisent deja.
        AttachConsole(ProcessusParent);
        var sortie = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
        var erreurs = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true };

        try
        {
            switch (args[0])
            {
                case "--generer-edition" when args.Length >= 3:
                    return GenererEdition(args[1], args[2], LireOption(args, "--nom"), sortie);
                case "--extraire-donnees" when args.Length >= 3:
                    return ExtraireDonnees(args[1], args[2], sortie);
                default:
                    erreurs.WriteLine("usage : --generer-edition <iso> <editions.json> [--nom \"<nom>\"]");
                    erreurs.WriteLine("        --extraire-donnees <iso> <dossier>");
                    return 2;
            }
        }
        catch (ImageIsoInvalideException e)
        {
            erreurs.WriteLine(e.Message);
            return 1;
        }
    }

    private static string? LireOption(string[] args, string nom)
    {
        int i = Array.IndexOf(args, nom);
        return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    }

    private static int GenererEdition(string iso, string catalogue, string? nom, TextWriter sortie)
    {
        using ImageIso image = ImageIso.Ouvrir(iso);
        sortie.WriteLine($"image : {image.Etiquette}, {image.Taille:N0} octets, {image.Fichiers.Count} fichiers");

        Edition edition = GenerationEdition.Generer(image, nom ?? image.Etiquette, sortie);

        CatalogueEditions editions = CatalogueEditions.Lire(catalogue);
        editions.Editions.RemoveAll(e => e.Etiquette == edition.Etiquette && e.ArchiveMd5 == edition.ArchiveMd5);
        editions.Editions.Add(edition);
        editions.Ecrire(catalogue);

        sortie.WriteLine($"edition « {edition.Nom} » : {edition.Fichiers.Count} fichiers, " +
                         $"{edition.TailleTotale:N0} octets, archive {edition.ArchiveMd5}");
        sortie.WriteLine($"ecrit : {catalogue}");
        return 0;
    }

    private static int ExtraireDonnees(string iso, string dossier, TextWriter sortie)
    {
        using ImageIso image = ImageIso.Ouvrir(iso);
        Edition edition = CatalogueEditions.Embarque().Reconnaitre(image);
        sortie.WriteLine($"edition reconnue : {edition.Nom}");

        long total = edition.TailleTotale, fait = 0;
        int dernierPourcent = -1;
        ExtractionDonnees.Extraire(image, edition, dossier, n =>
        {
            fait += n;
            int pourcent = (int)(fait * 100 / total);
            if (pourcent / 10 != dernierPourcent / 10) sortie.WriteLine($"  {pourcent} %");
            dernierPourcent = pourcent;
        });

        sortie.WriteLine($"{edition.Fichiers.Count} fichiers extraits et verifies dans {dossier}");
        return 0;
    }
}
