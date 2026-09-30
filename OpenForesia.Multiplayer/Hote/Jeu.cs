// Le jeu, cote hote : ou il est, son lancement, et s'il est au premier plan (tache 1.4, design D8).
//
// Le jeu lit son clavier en DISCL_FOREGROUND : une frappe simulee ne l'atteint que si l'une de ses
// fenetres est au premier plan. On compare le PROCESSUS de la fenetre au premier plan au sien, pas
// une fenetre donnee — le jeu en ouvre une au demarrage, puis la fenetre D3D11.

using System.Diagnostics;
using OpenForesia.Multiplayer.Natif;

namespace OpenForesia.Multiplayer.Hote;

internal sealed class Jeu : IDisposable
{
    internal const string Executable = "OpenForesia.exe";

    private readonly Process _processus;

    internal string Dossier { get; }
    internal string Cfg => Path.Combine(Dossier, "OpenForesia.cfg");
    internal bool Termine => _processus.HasExited;

    /// <summary>Le lanceur : son arbre de processus porte le vrai jeu (LB-056), et donc son son.</summary>
    internal uint Pid => (uint)_processus.Id;

    private Jeu(string dossier, Process processus)
    {
        Dossier = dossier;
        _processus = processus;
    }

    /// <summary>
    /// Le dossier du jeu : celui donné, sinon celui de cet exe s'il contient le jeu (installation),
    /// sinon la build Debug du dépôt, retrouvée en remontant jusqu'à OpenForesia.sln (développement).
    /// </summary>
    internal static string? Trouver(string? donne)
    {
        if (donne != null) return File.Exists(Path.Combine(donne, Executable)) ? Path.GetFullPath(donne) : null;

        string ici = AppContext.BaseDirectory;
        if (File.Exists(Path.Combine(ici, Executable))) return ici;

        for (var d = new DirectoryInfo(ici); d != null; d = d.Parent)
        {
            if (!File.Exists(Path.Combine(d.FullName, "OpenForesia.sln"))) continue;
            string build = Path.Combine(d.FullName, "OpenForesia", "bin", "Debug", "net10.0");
            return File.Exists(Path.Combine(build, Executable)) ? build : null;
        }
        return null;
    }

    internal static Jeu Lancer(string dossier)
    {
        var info = new ProcessStartInfo(Path.Combine(dossier, Executable))
        {
            WorkingDirectory = dossier,
            UseShellExecute = false,
        };
        Process processus = Process.Start(info) ?? throw new InvalidOperationException($"{Executable} n'a pas pu être lancé");
        return new Jeu(dossier, processus);
    }

    // Le processus au premier plan à la dernière question, et la réponse — pour ne relire la table
    // des processus que lorsqu'il change.
    private uint _dernierPid;
    private bool _dernierEstLeJeu;

    /// <summary>
    /// Une fenêtre du jeu est au premier plan : ses touches simulées l'atteignent.
    /// <para>
    /// Le processus lancé n'est pas le jeu : depuis LB-056 (<c>Host/ReserveBasse.cs</c>),
    /// OpenForesia.exe est un LANCEUR qui crée le vrai jeu en processus enfant, lui réserve la
    /// mémoire basse et l'attend. La fenêtre du jeu appartient donc à l'enfant : on accepte le
    /// lanceur et ses enfants directs (essai de Vincent, 2026-09-30 : l'hôte croyait le jeu en
    /// arrière-plan pendant qu'on y jouait).
    /// </para>
    /// </summary>
    internal bool AuPremierPlan()
    {
        if (_processus.HasExited) return false;
        IntPtr fenetre = Win32.GetForegroundWindow();
        if (fenetre == IntPtr.Zero) return false;
        Win32.GetWindowThreadProcessId(fenetre, out uint pid);
        if (pid == _dernierPid) return _dernierEstLeJeu;

        _dernierPid = pid;
        _dernierEstLeJeu = pid == (uint)_processus.Id || Win32.Parent(pid) == (uint)_processus.Id;
        return _dernierEstLeJeu;
    }

    /// <summary>
    /// La fenêtre du jeu à capturer : la plus grande fenêtre visible du lanceur ou de ses enfants
    /// (le vrai jeu, LB-056), ou zéro tant qu'il n'en a pas. Elle change au démarrage : une surface
    /// d'accueil, puis la fenêtre D3D11.
    /// </summary>
    internal IntPtr Fenetre()
    {
        if (_processus.HasExited) return IntPtr.Zero;
        uint lanceur = (uint)_processus.Id;
        var processus = new HashSet<uint>(Win32.Enfants(lanceur)) { lanceur };
        return Win32.PlusGrandeFenetre(processus);
    }

    public void Dispose() => _processus.Dispose();
}
