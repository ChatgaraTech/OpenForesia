using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

// ECHAFAUDAGE — la trace de cadence (2026-11-21), pour l'emballement de LB-061.
//
// Sur une machine Windows 10, après environ une heure de jeu, le jeu s'est mis à aller quatre à cinq
// fois trop vite, l'image continuant de bouger et le son restant à sa vitesse. L'hypothèse : à un
// moment, `Present(1, 0)` cesse d'attendre le balayage. Cette trace le dira : elle mesure chaque
// présentation et écrit, dans `trace-cadence.log` à côté de l'exe :
//
//   - un en-tête (heure, Windows, fréquence du compteur) ;
//   - toute seconde ANORMALE : un retour de `Present` différent de S_OK, plus de la moitié des
//     présentations rendues en moins de 2 ms (elles n'attendaient plus l'écran), ou plus de
//     70 images dans la seconde ;
//   - tout changement de premier plan ou de réduction de la fenêtre ;
//   - une ligne de vie par minute, qui date aussi un éventuel plantage (la dernière ligne écrite).
//
// Chaque ligne est écrite tout de suite (AutoFlush) : un plantage n'emporte rien. Active seulement
// en Debug (`Diagnostic.Actif`) : aucun journal dans le jeu du joueur. Ne lève jamais : une erreur
// d'écriture éteint la trace, pas le jeu.

/// <summary>Mesure chaque présentation et note dans `trace-cadence.log` les secondes où `Present` n'attend plus l'écran.</summary>
internal static class TraceCadence
{
    private static readonly double s_MsParTop = 1000.0 / Stopwatch.Frequency;
    private static StreamWriter? s_Fichier;
#if TRACE_CADENCE
    private static bool s_Eteinte = false;   // exe de test publie avec -p:TraceCadence=1
#else
    private static bool s_Eteinte = !Diagnostic.Actif;
#endif

    private static long s_DebutSeconde, s_DerniereVie;
    private static int s_Images, s_Rapides, s_Erreurs, s_DernierHr;
    private static double s_PresentMax, s_PresentTotal, s_AttenteTotal;
    private static int s_PremierPlan = -1, s_Reduite = -1;

    /// <summary>
    /// Une présentation : <paramref name="hr"/> le retour de `Present`, puis trois instants du
    /// compteur — avant `Present`, après `Present`, après le limiteur.
    /// </summary>
    internal static void Image(int hr, long avant, long apres, long finAttente)
    {
        if (s_Eteinte) return;
        try
        {
            if (s_Fichier == null) Ouvrir(avant);

            double presentMs = (apres - avant) * s_MsParTop;
            s_Images++;
            if (presentMs < 2.0) s_Rapides++;
            if (presentMs > s_PresentMax) s_PresentMax = presentMs;
            s_PresentTotal += presentMs;
            s_AttenteTotal += (finAttente - apres) * s_MsParTop;
            if (hr != 0) { s_Erreurs++; s_DernierHr = hr; }

            IntPtr fenetre = D3D11Window.Handle;
            int premierPlan = GetForegroundWindow() == fenetre ? 1 : 0;
            int reduite = IsIconic(fenetre) ? 1 : 0;
            if (premierPlan != s_PremierPlan || reduite != s_Reduite)
            {
                if (s_PremierPlan != -1)
                    Ecrire($"fenetre : premier plan={(premierPlan == 1 ? "oui" : "non")} reduite={(reduite == 1 ? "oui" : "non")}");
                s_PremierPlan = premierPlan;
                s_Reduite = reduite;
            }

            if (finAttente - s_DebutSeconde < Stopwatch.Frequency) return;

            bool anormale = s_Erreurs > 0 || s_Rapides * 2 > s_Images || s_Images > 70;
            if (anormale)
                Ecrire($"ANORMALE  {Bilan()}" + (s_Erreurs > 0 ? $" ; Present a rendu 0x{s_DernierHr:X8} {s_Erreurs} fois" : ""));
            if (finAttente - s_DerniereVie >= 60 * Stopwatch.Frequency)
            {
                Ecrire($"vie       {Bilan()}");
                s_DerniereVie = finAttente;
            }

            s_DebutSeconde = finAttente;
            s_Images = s_Rapides = s_Erreurs = 0;
            s_PresentMax = s_PresentTotal = s_AttenteTotal = 0;
        }
        catch
        {
            s_Eteinte = true;
        }
    }

    private static string Bilan()
    {
        int n = Math.Max(s_Images, 1);
        return $"images/s={s_Images} rapides(<2ms)={s_Rapides} " +
               $"Present moy={s_PresentTotal / n:0.0}ms max={s_PresentMax:0.0}ms " +
               $"limiteur moy={s_AttenteTotal / n:0.0}ms premier-plan={(s_PremierPlan == 1 ? "oui" : "non")}";
    }

    private static void Ouvrir(long maintenant)
    {
        s_Fichier = new StreamWriter("trace-cadence.log", append: true) { AutoFlush = true };
        s_Fichier.WriteLine();
        Ecrire($"=== debut : {Environment.OSVersion.VersionString}, compteur {Stopwatch.Frequency} Hz ===");
        s_DebutSeconde = s_DerniereVie = maintenant;
    }

    private static void Ecrire(string ligne) => s_Fichier!.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}  {ligne}");

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsIconic(IntPtr fenetre);
}
