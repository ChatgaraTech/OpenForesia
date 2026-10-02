// La desinstallation : l'installeur lui-meme, copie dans le dossier du jeu sous le nom
// Uninstall.exe et lance avec --desinstaller — comme l'unins000.exe d'Inno Setup.
//
// Un executable ne peut pas effacer son propre fichier pendant qu'il tourne. La copie du dossier du
// jeu se recopie donc dans %TEMP% et se relance de la ; c'est cette seconde copie qui pose les
// questions, retire tout, puis confie son propre effacement a un cmd.exe qui attend qu'elle se
// termine.
//
// En developpement (installeur non publie en fichier unique), rien n'est copie : l'entree de
// desinstallation designe l'installeur du depot, avec le dossier en argument.

using System.Diagnostics;
using Microsoft.Win32;
using OpenForesia.Installer.Interface;
using static OpenForesia.Installer.Interface.Natif;

namespace OpenForesia.Installer.Installation;

internal static class Desinstallation
{
    internal const string Executable = "Uninstall.exe";

    /// <summary>Le nom qu'il portait jusqu'au 2026-09-26 : reconnu au double-clic, retire a la reinstallation.</summary>
    internal const string AncienExecutable = "Desinstaller.exe";
    private const string Titre = "Désinstaller OpenForesia";

    private const uint MB_OKCANCEL = 1, MB_YESNOCANCEL = 3, MB_ICONQUESTION = 0x20;
    private const int IDNO = 7;

    /// <summary>
    /// Vrai pour une release : publie en un seul fichier, l'installeur n'a pas sa DLL a cote de lui.
    /// Seul un installeur en fichier unique peut etre copie tel quel dans le dossier du jeu.
    /// </summary>
    internal static bool EstFichierUnique =>
        !File.Exists(Path.Combine(AppContext.BaseDirectory, "OpenForesia-Setup.dll"));

    /// <summary>Le programme et les arguments qui desinstallent le jeu de <paramref name="dossier"/>.</summary>
    internal static (string Programme, string Arguments) Commande(string dossier) =>
        EstFichierUnique
            ? (Path.Combine(dossier, Executable), "--desinstaller")
            : (Environment.ProcessPath!, $"--desinstaller \"{dossier}\"");

    /// <summary>Depose Uninstall.exe dans le dossier du jeu (release seulement).</summary>
    internal static void Deposer(string dossier, JournalInstallation journal)
    {
        if (!EstFichierUnique) return;
        string cible = Path.Combine(dossier, Executable);
        journal.NoterFichier(cible);
        File.Copy(Environment.ProcessPath!, cible, overwrite: true);
    }

    internal static int Executer(string[] args)
    {
        bool temporaire = args.Contains("--temporaire");
        string? dossier = args.Length >= 2 && !args[1].StartsWith("--", StringComparison.Ordinal)
            ? args[1]
            : Path.GetDirectoryName(Environment.ProcessPath);
        if (dossier == null || !File.Exists(Path.Combine(dossier, Installation.Executable)))
        {
            MessageBox(IntPtr.Zero, "OpenForesia n'est pas installé ici.", Titre, MB_OK | MB_ICONERROR);
            return 1;
        }
        dossier = Path.GetFullPath(dossier);

        // La copie du dossier du jeu passe la main a une copie temporaire, qui pourra l'effacer.
        if (EstFichierUnique && !temporaire)
        {
            string copie = Path.Combine(Path.GetTempPath(), $"OpenForesia-Uninstall-{Guid.NewGuid():N}.exe");
            File.Copy(Environment.ProcessPath!, copie);
            Process.Start(new ProcessStartInfo(copie)
            {
                ArgumentList = { "--desinstaller", dossier, "--temporaire", Environment.ProcessId.ToString() },
                UseShellExecute = false,
            });
            return 0;
        }

        // On ne travaille pas depuis le dossier qu'on va supprimer, et on laisse la copie qui nous
        // a lances se terminer.
        Directory.SetCurrentDirectory(Path.GetTempPath());
        AttendreLeParent(args);

        try
        {
            Desinstaller(dossier);
        }
        finally
        {
            if (temporaire) EffacerApresSortie(Environment.ProcessPath!);
        }
        return 0;
    }

    private static void AttendreLeParent(string[] args)
    {
        int i = Array.IndexOf(args, "--temporaire");
        if (i < 0 || i + 1 >= args.Length || !int.TryParse(args[i + 1], out int pid)) return;
        try { using var parent = Process.GetProcessById(pid); parent.WaitForExit(10_000); }
        catch (ArgumentException) { }   // deja termine
    }

    private static void Desinstaller(string dossier)
    {
        if (MessageBox(IntPtr.Zero, $"Désinstaller OpenForesia de ce dossier ?\n\n{dossier}", Titre,
                MB_OKCANCEL | MB_ICONQUESTION) != IDOK)
            return;

        // Le jeu doit etre ferme : Windows refuse d'effacer un executable en cours.
        string exe = Path.Combine(dossier, Installation.Executable);
        string lanceur = Path.Combine(dossier, Installation.ExecutableLanceur);
        string multijoueur = Path.Combine(dossier, Installation.AncienExecutableMultijoueur);
        while (JeuEnCours(exe) || JeuEnCours(lanceur) || JeuEnCours(multijoueur))
        {
            if (MessageBox(IntPtr.Zero, "OpenForesia est en cours d'exécution. Fermez le jeu et le lanceur, puis cliquez sur OK.",
                    Titre, MB_OKCANCEL | MB_ICONWARNING) != IDOK)
                return;
        }

        string sauvegardes = Path.Combine(dossier, "save");
        bool garder = false;
        if (Directory.Exists(sauvegardes) && Directory.EnumerateFileSystemEntries(sauvegardes).Any())
        {
            int reponse = MessageBox(IntPtr.Zero, $"Conserver vos sauvegardes ?\n\nElles resteront dans :\n{sauvegardes}",
                Titre, MB_YESNOCANCEL | MB_ICONQUESTION);
            if (reponse == IDCANCEL) return;
            garder = reponse == IDYES;
        }

        // Raccourcis et entree « Applications installees ».
        Supprimer(() => File.Delete(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Installation.NomJeu + ".lnk")));
        Supprimer(() => File.Delete(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Installation.NomLanceur + ".lnk")));
        Supprimer(() => File.Delete(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Installation.AncienNomMultijoueur + ".lnk")));
        Supprimer(() => Directory.Delete(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Programs), Installation.NomJeu), recursive: true));
        Supprimer(() => Registry.CurrentUser.DeleteSubKeyTree(Installation.CleDesinstallation, throwOnMissingSubKey: false));

        // Le dossier du jeu, sauvegardes exceptees si le joueur les garde.
        var echecs = new List<string>();
        foreach (string element in Directory.EnumerateFileSystemEntries(dossier).ToList())
        {
            string nom = Path.GetFileName(element);
            if (garder && string.Equals(nom, "save", StringComparison.OrdinalIgnoreCase)) continue;
            if (!SupprimerAvecReprise(element)) echecs.Add(nom);
        }
        if (!Directory.EnumerateFileSystemEntries(dossier).Any()) Supprimer(() => Directory.Delete(dossier));

        if (echecs.Count > 0)
            MessageBox(IntPtr.Zero, "OpenForesia est désinstallé, mais ces éléments n'ont pas pu être supprimés :\n\n" +
                                    string.Join("\n", echecs) + $"\n\nDossier : {dossier}", Titre, MB_OK | MB_ICONWARNING);
        else if (garder)
            MessageBox(IntPtr.Zero, $"OpenForesia est désinstallé. Vos sauvegardes sont conservées dans :\n{sauvegardes}",
                Titre, MB_OK | MB_ICONINFORMATION);
        else
            MessageBox(IntPtr.Zero, "OpenForesia est désinstallé.", Titre, MB_OK | MB_ICONINFORMATION);
    }

    private static bool JeuEnCours(string exe)
    {
        foreach (Process processus in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(exe)))
        {
            using (processus)
            {
                try
                {
                    if (string.Equals(processus.MainModule?.FileName, exe, StringComparison.OrdinalIgnoreCase)) return true;
                }
                catch (Exception e) when (e is InvalidOperationException or System.ComponentModel.Win32Exception) { }
            }
        }
        return false;
    }

    private static void Supprimer(Action action)
    {
        try { action(); }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException) { }
    }

    /// <summary>Supprime un fichier ou un dossier, en reessayant un peu : Uninstall.exe peut
    /// appartenir encore quelques instants au processus qui vient de nous lancer.</summary>
    private static bool SupprimerAvecReprise(string chemin)
    {
        for (int essai = 0; essai < 10; essai++)
        {
            try
            {
                if (Directory.Exists(chemin)) Directory.Delete(chemin, recursive: true);
                else File.Delete(chemin);
                return true;
            }
            catch (Exception e) when (e is IOException or UnauthorizedAccessException)
            {
                Thread.Sleep(300);
            }
        }
        return false;
    }

    /// <summary>
    /// Confie l'effacement de la copie temporaire a un cmd.exe cache, qui attend deux secondes que
    /// ce processus soit termine. Sans droits d'administrateur, c'est la seule maniere : l'effacement
    /// au redemarrage (MoveFileEx) en demande.
    /// </summary>
    private static void EffacerApresSortie(string fichier)
    {
        Process.Start(new ProcessStartInfo(Path.Combine(Environment.SystemDirectory, "cmd.exe"))
        {
            Arguments = $"/c ping -n 3 127.0.0.1 >nul & del /f /q \"{fichier}\"",
            CreateNoWindow = true,
            UseShellExecute = false,
            WorkingDirectory = Path.GetTempPath(),
        });
    }
}
