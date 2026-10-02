// Jouer seul avec sa manette (openspec/changes/add-launcher-solo-gamepad, design D3, D5, D6) : les
// touches fantomes du joueur 1 ecrites dans sa table 1, le jeu lance, la manette relayee tant qu'il
// tourne, puis le cfg remis — apres la fin du processus du jeu, jamais avant : le jeu reecrit
// [Controls] depuis sa memoire en sortant de ses Options.

using OpenForesia.Lanceur.Hote;
using OpenForesia.Lanceur.Invite;
using OpenForesia.Lanceur.Natif;

namespace OpenForesia.Lanceur.Solo;

internal static class ModeSolo
{
    /// <summary>
    /// La partie solo depuis l'accueil : la fenêtre de la partie tant qu'elle est ouverte. Fermée
    /// avant le jeu, le relais s'arrête et le lanceur attend la fin du jeu, sans fenêtre, pour
    /// remettre le cfg.
    /// </summary>
    internal static int ExecuterFenetre(string? dossierDonne)
    {
        string? dossier = Jeu.Trouver(dossierDonne);
        if (dossier == null)
        {
            Win32.MessageBox(IntPtr.Zero, $"Le jeu est introuvable : {Jeu.Executable} doit être à côté de ce programme.",
                             "OpenForesia", Win32.MB_OK | Win32.MB_ICONERROR);
            return 2;
        }

        ConfigManette config = ConfigManette.Charger() ?? new ConfigManette();
        return Jouer(dossier, config, (jeu, relais) =>
        {
            Interface.FenetreSolo.Executer(jeu, relais, config);
            relais.Dispose();
            if (!jeu.Termine) Journal("fenêtre fermée : relais arrêté, le jeu continue au clavier. Les touches seront remises à sa fermeture.");
            while (!jeu.Termine) Thread.Sleep(500);
        }, erreur => Win32.MessageBox(IntPtr.Zero, $"La partie s'est interrompue : {erreur}", "OpenForesia", Win32.MB_OK | Win32.MB_ICONERROR));
    }

    /// <summary>La partie solo sans fenêtre (<c>--solo</c>) : tout dans la console.</summary>
    internal static int ExecuterConsole(string? dossierDonne)
    {
        string? dossier = Jeu.Trouver(dossierDonne);
        if (dossier == null)
        {
            Journal($"{Jeu.Executable} introuvable ({dossierDonne ?? "ni à côté de cet exe, ni dans la build du dépôt"}).");
            return 2;
        }

        ConfigManette config = ConfigManette.Charger() ?? new ConfigManette();
        if (config.Liaisons.Count == 0)
            Journal("aucune manette configurée (--manette pour la configurer) : la partie se joue au clavier.");

        return Jouer(dossier, config, (jeu, relais) =>
        {
            // Ctrl+C arrête le relais (le jeu continue au clavier) ; la remise du cfg attend toujours
            // la fin du jeu. Un second Ctrl+C quitte : le prochain lancement du lanceur remettra le cfg.
            bool arrete = false;
            Console.CancelKeyPress += (_, e) =>
            {
                if (arrete) return;
                arrete = e.Cancel = true;
                relais.Dispose();
                Journal("relais arrêté : le jeu continue au clavier. Les touches seront remises à sa fermeture.");
            };

            string? vue = "";
            while (!jeu.Termine)
            {
                string? manette = relais.Manette;
                if (relais.Actif && manette != vue)
                {
                    Journal(manette != null ? $"manette vue : {manette}" : "aucune manette configurée n'est branchée");
                    vue = manette;
                }
                Thread.Sleep(200);
            }
            relais.Dispose();
        }, erreur => { });
    }

    /// <summary>
    /// Le cœur des deux : touches écrites, jeu lancé, relais démarré, <paramref name="pendant"/> qui
    /// rend la main une fois le jeu fermé, puis les touches remises — quoi qu'il arrive.
    /// </summary>
    private static int Jouer(string dossier, ConfigManette config, Action<Jeu, Relais> pendant, Action<string> signaler)
    {
        string cfg = Path.Combine(dossier, "OpenForesia.cfg");
        Jeu? jeu = null;
        try
        {
            ConfigTouches.Ecrire(cfg, [1]);
            jeu = Jeu.Lancer(dossier);
            Journal($"jeu lancé depuis {dossier} ; la manette n'agit que tant que la fenêtre du jeu est au premier plan.");
            using (var relais = new Relais(jeu, config))
                pendant(jeu, relais);
            Journal("le jeu est fermé : fin de la partie");
            return 0;
        }
        catch (Exception e)
        {
            Journal($"partie interrompue : {e.GetType().Name} : {e.Message}");
            signaler(e.Message);
            return 1;
        }
        finally
        {
            jeu?.Dispose();
            ConfigTouches.Remettre(cfg);
        }
    }

    private static void Journal(string message) => Console.Out.WriteLine($"[SOLO] {message}");
}
