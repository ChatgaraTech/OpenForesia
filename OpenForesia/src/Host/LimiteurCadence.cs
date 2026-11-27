using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

// Écart legacy ++ (LB-061) : le limiteur à 60 images par seconde.
//
// Le jeu de 1998 n'a pas d'horloge : il avance d'un cran par image, et sa seule attente était le
// `Flip` DirectDraw, calé sur le balayage d'un écran à 60 Hz (le mode de cadence de winsys3d reste à
// 0, voir `D3D11Backend.Present`). Le port reproduisait cela par `Present(1, 0)` — avec deux
// conséquences que l'original n'avait pas : sur un écran à 120 Hz ou plus, tout le jeu va deux fois
// plus vite ou davantage ; et si Windows ou le pilote cesse un instant d'honorer la synchronisation
// (vécu une fois, en partie à trois : jeu fluide, quatre à cinq fois trop rapide), plus rien ne
// freine. Le limiteur ajoute, APRÈS la présentation, une attente jusqu'à l'échéance suivante d'une
// horloge à 60 Hz ; sur un écran à 60 Hz il n'attend presque jamais, `Present` l'ayant déjà fait.

/// <summary>Une échéance toutes les 1/60 s, tenue par une minuterie haute résolution puis une courte attente active.</summary>
internal static class LimiteurCadence
{
    private const double ImagesParSeconde = 60.0;

    private static readonly long s_Periode = (long)Math.Round(Stopwatch.Frequency / ImagesParSeconde);
    private static long s_Echeance;
    private static IntPtr s_Minuterie;
    private static bool s_MinuterieEssayee;

    /// <summary>À appeler une fois par image présentée : rend la main à l'échéance suivante, jamais avant.</summary>
    internal static void Attendre()
    {
        long maintenant = Stopwatch.GetTimestamp();
        if (s_Echeance == 0 || maintenant - s_Echeance > 4 * s_Periode)
        {
            // Premier appel, ou retour d'un long arrêt (chargement, fenêtre déplacée, film) : on
            // repart de maintenant plutôt que d'enchaîner des images sans attendre pour « rattraper ».
            s_Echeance = maintenant + s_Periode;
            return;
        }

        long reste = s_Echeance - maintenant;
        if (reste > 0)
        {
            // L'essentiel du reste en dormant, à la minuterie haute résolution ; on se réveille
            // 1,5 ms avant pour finir en attente active, la minuterie pouvant déborder un peu.
            long marge = Stopwatch.Frequency * 15 / 10000;
            if (reste > marge && Minuterie() is var t && t != IntPtr.Zero)
            {
                long centaines = -(reste - marge) * 10_000_000 / Stopwatch.Frequency;   // relatif, en 100 ns
                if (SetWaitableTimerEx(t, ref centaines, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0))
                    WaitForSingleObject(t, 0xffffffff);
            }
            while (Stopwatch.GetTimestamp() < s_Echeance) Thread.SpinWait(64);
        }
        s_Echeance += s_Periode;
    }

    private static IntPtr Minuterie()
    {
        if (s_MinuterieEssayee) return s_Minuterie;
        s_MinuterieEssayee = true;
        // CREATE_WAITABLE_TIMER_HIGH_RESOLUTION (Windows 10 1803+) ; à défaut, l'attente active seule.
        s_Minuterie = CreateWaitableTimerExW(IntPtr.Zero, null, 0x2, 0x1F0003);
        return s_Minuterie;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr CreateWaitableTimerExW(IntPtr attributs, string? nom, uint options, uint acces);

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetWaitableTimerEx(IntPtr minuterie, ref long echeance, int periode,
        IntPtr rappel, IntPtr argument, IntPtr contexte, uint delaiTolere);

    [DllImport("kernel32.dll")]
    private static extern uint WaitForSingleObject(IntPtr objet, uint millisecondes);
}
