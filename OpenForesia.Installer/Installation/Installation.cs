// L'installation elle-meme, sans interface : les controles du dossier, puis le depot du jeu, des
// donnees, de la desinstallation, des raccourcis et de l'entree « Applications installees ».
//
// Tout est fait pour l'utilisateur courant, sans droits d'administrateur : dossier sous
// %LOCALAPPDATA% par defaut, raccourcis de l'utilisateur, cle sous HKCU.

using Microsoft.Win32;
using OpenForesia.Installer.Editions;
using OpenForesia.Installer.Extraction;
using OpenForesia.Installer.Iso;

namespace OpenForesia.Installer.Installation;

internal sealed record Avancement(long Fait, long Total, string Etape);

internal static class Installation
{
    internal const string NomJeu = "OpenForesia";
    internal const string Executable = "OpenForesia.exe";
    internal const string ScriptDesinstallation = "Desinstaller.ps1";
    internal const string CleDesinstallation = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\OpenForesia";

    /// <summary>Au-dela, un chemin du jeu risquerait de depasser les 260 caracteres de Windows.</summary>
    internal const int LongueurMaxDossier = 200;

    private const long Marge = 16L << 20;

    internal static string DossierParDefaut => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", NomJeu);

    /// <summary>Le dossier d'une installation precedente, lu dans son entree de desinstallation.</summary>
    internal static string? DossierExistant()
    {
        using RegistryKey? cle = Registry.CurrentUser.OpenSubKey(CleDesinstallation);
        return cle?.GetValue("InstallLocation") as string;
    }

    internal static long TailleRequise(Edition edition, SourceDuJeu jeu) => edition.TailleTotale + jeu.Taille + Marge;

    /// <summary>
    /// Le probleme bloquant de ce dossier, en une phrase pour le joueur, ou null. <paramref name="protege"/>
    /// signale un dossier systeme ou le jeu ne pourrait pas ecrire ses sauvegardes : ce n'est pas
    /// bloquant, l'interface demande confirmation.
    /// </summary>
    internal static string? Verifier(string dossier, long requis, out bool protege)
    {
        protege = false;
        if (string.IsNullOrWhiteSpace(dossier)) return "Choisissez un dossier d'installation.";

        string complet;
        try
        {
            if (!Path.IsPathFullyQualified(dossier)) return "Indiquez un chemin complet, par exemple C:\\Jeux\\OpenForesia.";
            complet = Path.GetFullPath(dossier);
        }
        catch (Exception e) when (e is ArgumentException or NotSupportedException or PathTooLongException)
        {
            return "Ce chemin n'est pas valide.";
        }

        if (complet.Length > LongueurMaxDossier)
            return $"Ce chemin est trop long ({complet.Length} caractères, {LongueurMaxDossier} au plus). Choisissez un dossier plus court.";

        if (File.Exists(complet)) return "Ce chemin désigne un fichier, pas un dossier.";

        string? racine = Path.GetPathRoot(complet);
        if (racine != null && !racine.StartsWith(@"\\", StringComparison.Ordinal))
        {
            var lecteur = new DriveInfo(racine);
            if (!lecteur.IsReady) return $"Le lecteur {racine} n'est pas disponible.";
            if (lecteur.AvailableFreeSpace < requis)
                return $"Il n'y a pas assez de place sur {racine} : il faut {Mo(requis)}, " +
                       $"il en reste {Mo(lecteur.AvailableFreeSpace)}.";
        }

        protege = new[]
            {
                Environment.SpecialFolder.ProgramFiles, Environment.SpecialFolder.ProgramFilesX86,
                Environment.SpecialFolder.Windows,
            }
            .Select(Environment.GetFolderPath)
            .Where(d => d.Length > 0)
            .Any(d => complet.StartsWith(d.TrimEnd('\\') + "\\", StringComparison.OrdinalIgnoreCase)
                      || string.Equals(complet.TrimEnd('\\'), d.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase));
        return null;
    }

    internal static string Mo(long octets) => $"{(octets + (1 << 20) - 1) >> 20:N0} Mo";

    /// <summary>
    /// Installe tout. En cas d'echec ou d'annulation, ce qui a ete cree par cet appel est retire et
    /// l'exception remonte ; ce qui existait avant (une installation precedente, des sauvegardes)
    /// reste en place.
    /// </summary>
    internal static void Installer(ImageIso image, Edition edition, SourceDuJeu jeu, string dossier,
        IProgress<Avancement> avancement, CancellationToken annulation)
    {
        dossier = Path.GetFullPath(dossier);
        long total = jeu.Taille + edition.TailleTotale, fait = 0;
        var journal = new JournalInstallation();

        try
        {
            journal.CreerDossier(dossier);

            // 1. Le jeu.
            foreach (FichierDuJeu fichier in jeu.Fichiers)
            {
                annulation.ThrowIfCancellationRequested();
                avancement.Report(new Avancement(fait, total, "Copie du jeu…"));
                string cible = Path.Combine(dossier, fichier.CheminRelatif);
                journal.CreerDossier(Path.GetDirectoryName(cible)!);
                journal.NoterFichier(cible);
                using (Stream source = fichier.Ouvrir())
                using (var sortie = new FileStream(cible, FileMode.Create, FileAccess.Write, FileShare.None, 1 << 16))
                    source.CopyTo(sortie, 1 << 20);
                fait += fichier.Taille;
            }

            // 2. Les donnees, extraites de l'ISO et verifiees ; cree aussi save\.
            ExtractionDonnees.Extraire(image, edition, dossier,
                n =>
                {
                    fait += n;
                    avancement.Report(new Avancement(fait, total, "Extraction des données du jeu…"));
                },
                null, annulation, journal);

            // 3. La desinstallation.
            annulation.ThrowIfCancellationRequested();
            avancement.Report(new Avancement(fait, total, "Création des raccourcis…"));
            string script = Path.Combine(dossier, ScriptDesinstallation);
            journal.NoterFichier(script);
            using (Stream modele = typeof(Installation).Assembly.GetManifestResourceStream(ScriptDesinstallation)!)
            using (var sortie = File.Create(script))
                modele.CopyTo(sortie);

            // 4. Les raccourcis.
            string exe = Path.Combine(dossier, Executable);
            string bureau = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), NomJeu + ".lnk");
            string menu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), NomJeu);
            journal.CreerDossier(menu);

            foreach (string raccourci in new[] { bureau, Path.Combine(menu, NomJeu + ".lnk") })
            {
                journal.NoterFichier(raccourci);
                Raccourcis.Creer(raccourci, exe, "", dossier, "Blaze & Blade, reconstruit", exe);
            }

            string desinstaller = Path.Combine(menu, "Désinstaller OpenForesia.lnk");
            journal.NoterFichier(desinstaller);
            Raccourcis.Creer(desinstaller, PowerShell, ArgumentsDesinstallation(script), dossier,
                "Désinstaller OpenForesia", exe);

            // 5. L'entree « Applications installees ». En dernier : rien a defaire apres elle.
            EcrireEntreeDesinstallation(dossier, exe, script, total);
        }
        catch
        {
            journal.Nettoyer();
            throw;
        }
    }

    private static string PowerShell => Path.Combine(Environment.SystemDirectory, @"WindowsPowerShell\v1.0\powershell.exe");

    private static string ArgumentsDesinstallation(string script) =>
        $"-NoProfile -ExecutionPolicy Bypass -WindowStyle Hidden -File \"{script}\"";

    private static void EcrireEntreeDesinstallation(string dossier, string exe, string script, long octets)
    {
        using RegistryKey cle = Registry.CurrentUser.CreateSubKey(CleDesinstallation);
        cle.SetValue("DisplayName", NomJeu);
        cle.SetValue("DisplayVersion", typeof(Installation).Assembly.GetName().Version?.ToString(3) ?? "0.1.0");
        cle.SetValue("Publisher", "Projet OpenForesia");
        cle.SetValue("DisplayIcon", exe);
        cle.SetValue("InstallLocation", dossier);
        cle.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
        cle.SetValue("UninstallString", $"\"{PowerShell}\" {ArgumentsDesinstallation(script)}");
        cle.SetValue("EstimatedSize", (int)(octets >> 10), RegistryValueKind.DWord);
        cle.SetValue("NoModify", 1, RegistryValueKind.DWord);
        cle.SetValue("NoRepair", 1, RegistryValueKind.DWord);
    }
}
