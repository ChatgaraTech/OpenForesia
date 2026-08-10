using System.Runtime.CompilerServices;
using OpenForesia.Blaze;

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

Console.WriteLine("=== OpenForesia ===");

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
    [ModuleInitializer]
    internal static void ClaimDisplayListArena() => ArenaClaimed = Blaze.TryPrepareDisplayListArena();
}
