using System.Runtime.CompilerServices;
using OpenForesia.Blaze;

// Écart legacy ++ (LB-056) : le processus lancé par le joueur ne joue pas. Il crée le jeu suspendu,
// lui réserve sa place sous 0x01000000 avant que le runtime ne s'y installe, et l'attend
// (Host/ReserveBasse.cs). S'il n'y parvient pas, il joue lui-même par le chemin ci-dessous.
if (ReserveBasse.EstLanceur)
{
    int? code = ReserveBasse.Lancer();
    if (code is int c) return c;
    Startup.ClaimDisplayListArenaNow();
}

// The arena has to be claimed before anything else allocates; the module initializer below did
// that. If it lost the race for low memory, a fresh process is a genuine second chance, because
// the address-space layout differs run to run. Bounded by an attempt counter in the environment:
// measured ~1 loss in 4 per attempt, so three attempts put the failure rate under 2%.
if (!Startup.ArenaClaimed)
{
    int attempt = int.TryParse(Environment.GetEnvironmentVariable(Startup.RetryGuard), out int n) ? n : 0;
    if (attempt < Startup.MaxAttempts - 1)
    {
        var restart = new System.Diagnostics.ProcessStartInfo(Environment.ProcessPath!)
        {
            UseShellExecute = false,
            WorkingDirectory = Environment.CurrentDirectory,
        };
        restart.Environment[Startup.RetryGuard] = (attempt + 1).ToString();
        foreach (string a in Environment.GetCommandLineArgs()[1..]) restart.ArgumentList.Add(a);

        Console.Error.WriteLine(
            $"display-list arena: lost the race for low memory, relaunching " +
            $"(attempt {attempt + 2} of {Startup.MaxAttempts})");
        using var child = System.Diagnostics.Process.Start(restart);
        child!.WaitForExit();
        return child.ExitCode;
    }
    // Out of attempts: fall through so Blaze.entry throws the descriptive arena error.
}

// Les journaux sont en français : sans cela ils partent dans la page de code OEM, et la fenêtre
// d'exécution de Rider, qui lit de l'UTF-8, affiche « pr�s » pour « près ».
try { Console.OutputEncoding = System.Text.Encoding.UTF8; } catch (IOException) { }

Console.WriteLine("=== OpenForesia ===");

// ECHAFAUDAGE TEMPORAIRE (tâche 2.6) : vérification de bout en bout du backend Direct3D 11, sans
// démarrer le jeu. `3dhelp` reste en place et n'est pas sollicité par ce chemin.
if (Environment.GetCommandLineArgs().Contains("--d3d11-selftest"))
{
    D3D11Backend.SelfTest();
    return 0;
}

// ---------------------------------------------------------------------------------------------
// Composition root : PAS ENCORE, et pour une raison mesuree.
//
// L'etape E du refactor prevoyait de construire ici les objets du jeu, dans un ordre explicite.
// Essaye le 2026-08-19 avec le seul objet qui existe a ce stade, `GameMemory` : le jeu plante au
// demarrage. Le module initializer ci-dessus reclame l'arene de liste d'affichage, et cette
// reclamation touche DELIBEREMENT Mem.s_UiDescriptors_00560ff0 et Mem.s_StatusDescriptor_00560728
// - ils portent leur ordering table a +0xa4, masquee sur 24 bits, donc ils doivent etre pris sous
// 0x01000000 au meme instant precoce que l'arene.
//
// `GameMemory` doit donc etre construit PENDANT l'initialisation statique de Blaze, pas apres.
// Son initialiseur reste dans src/Blaze/Memory/Globals.cs, ou la contrainte est ecrite.
//
// Un composition root qui ne composerait rien serait un rituel vide. Il sera ecrit quand Engine/
// passera en classes d'instance (etape C) et qu'il y aura reellement un ordre a decider - et il
// devra alors reprendre cette contrainte, pas la decouvrir a nouveau.
// ---------------------------------------------------------------------------------------------

Blaze.entry();
return 0;

internal static class Startup
{
    internal const string RetryGuard = "OPENFORESIA_ARENA_RETRY";
    internal const int MaxAttempts = 3;

    /// <summary>Whether the arena was secured. See the relaunch above when it was not.</summary>
    internal static bool ArenaClaimed { get; private set; }

    /// <summary>
    /// Runs before Main and before any other static constructor. The display list is addressed on
    /// 24 bits, so its arena has to live below 0x01000000 — a 16 MB window the .NET runtime also
    /// allocates into. Claiming it here, at the earliest managed instant, is what makes the race
    /// winnable at all: measured 2 failures in 5 runs when it was claimed from a static field
    /// initializer instead, and roughly 1 in 4 from here. See Blaze.EnsureDisplayListArena.
    /// </summary>
    /// <remarks>
    /// Le lanceur (LB-056) ne réclame rien : ce n'est pas lui qui joue, et la place qu'il prendrait
    /// dans SA mémoire ne servirait à personne. S'il doit jouer quand même, il réclame par
    /// <see cref="ClaimDisplayListArenaNow"/>, plus tard que d'habitude — c'est le repli.
    /// </remarks>
    [ModuleInitializer]
    internal static void ClaimDisplayListArena()
    {
        if (ReserveBasse.EstLanceur) return;
        ArenaClaimed = Blaze.TryPrepareDisplayListArena();
    }

    /// <summary>Le repli du lanceur quand la réserve n'a pas pu être posée.</summary>
    internal static void ClaimDisplayListArenaNow() => ArenaClaimed = Blaze.TryPrepareDisplayListArena();
}
