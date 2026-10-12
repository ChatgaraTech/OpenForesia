// L'essai du lot 2 par le reseau, sans le jeu : un hote qui diffuse la mire au lieu de la fenetre
// du jeu, que rejoint un invite ordinaire (--rejoindre 127.0.0.1 --sans-manette ...).
//
// L'hote reste « en pause » tout du long (le jeu n'est jamais au premier plan, il n'y a pas de jeu) :
// aucune touche n'est simulee, quoi que l'invite envoie.

using System.Diagnostics;
using OpenForesia.Lanceur.Hote;
using OpenForesia.Lanceur.Natif;

namespace OpenForesia.Lanceur.Mesure;

internal static class EssaiDiffusion
{
    internal static int Executer(int secondes, int port)
    {
        Win32.timeBeginPeriod(1);
        IntPtr mire = EssaiImage.CreerMire();
        byte[] cle = Commun.Invitation.NouvelleCle();
        using var serveur = new Serveur(port, cle);
        Console.Out.WriteLine($"[ESSAI] code : {new Commun.Invitation(System.Net.IPAddress.Loopback, port, cle)}");
        using var diffusion = new Diffusion(() => mire, serveur, Commun.Qualite.ParDefaut);

        // Le son : une note jouée par ce processus, capturée sur son propre arbre et diffusée.
        bool fin = false;
        var note = new Thread(() => EssaiSon.JouerNote(() => fin)) { IsBackground = true };
        note.SetApartmentState(ApartmentState.MTA);
        note.Start();
        using var son = new DiffusionSon((uint)Environment.ProcessId, serveur);
        Console.Out.WriteLine($"[ESSAI] la mire et une note de 440 Hz sont diffusées sur le port UDP {port} pendant {secondes} s");

        var montre = Stopwatch.StartNew();
        long prochaine = 0;
        int image = 0;
        serveur.Executer(() =>
        {
            long t = montre.ElapsedMilliseconds;
            if (t >= prochaine)
            {
                prochaine = t + 16;
                EssaiImage.DessinerMire(mire, image++);
                Pomper();
            }
            return t < secondes * 1000L;
        }, premierPlan: () => false);

        fin = true;
        note.Join(1000);
        Win32.DestroyWindow(mire);
        Win32.timeEndPeriod(1);
        return 0;
    }

    private static unsafe void Pomper()
    {
        Win32.MSG msg;
        while (Win32.PeekMessage(&msg, IntPtr.Zero, 0, 0, Win32.PM_REMOVE))
        {
            Win32.TranslateMessage(&msg);
            Win32.DispatchMessage(&msg);
        }
    }
}
