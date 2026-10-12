// « Configurer ma manette », depuis une fenetre du lanceur (openspec/changes/add-launcher-solo-gamepad,
// design D4, D5) : l'ecran de l'invite, inchange (EcranManette), dans sa propre fenetre, sur la
// configuration de manette.json. Chaque changement y est enregistre aussitot.
//
// La fenetre qui l'ouvre est desactivee le temps de la configuration ; sa boucle de messages est
// remplacee par celle-ci, qui sert aussi ses minuteries. Le relais du joueur 1, s'il y en a un,
// n'envoie rien pendant ce temps et reprend a la fermeture avec la nouvelle configuration.

using OpenForesia.Lanceur.Hote;
using OpenForesia.Lanceur.Invite;
using OpenForesia.Lanceur.Natif;

namespace OpenForesia.Lanceur.Interface;

internal static unsafe class ConfigurationManette
{
    private const uint WM_QUIT = 0x0012;

    /// <summary>
    /// Ouvre l'écran de la manette et le mène jusqu'à « Fermer » ou la fermeture de sa fenêtre.
    /// Faux si l'application doit quitter (un WM_QUIT est arrivé pendant ce temps ; il est reposté).
    /// </summary>
    internal static bool Ouvrir(IntPtr proprietaire, ConfigManette config, Relais? relais = null)
    {
        relais?.Suspendre();
        if (proprietaire != IntPtr.Zero) Win32.EnableWindow(proprietaire, false);
        int? quitter = null;
        try
        {
            using var fenetre = new FenetreInvite("OpenForesia — ma manette");
            var ecran = new EcranManette(config, libelleFermer: "Fermer");
            fenetre.Ecran = ecran;
            ecran.Basculer();
            fenetre.Redessiner();

            var actives = new HashSet<Entree>();
            while (ecran.Ouvert && !fenetre.Fermee && quitter == null)
            {
                Win32.MSG msg;
                while (Win32.PeekMessage(&msg, IntPtr.Zero, 0, 0, Win32.PM_REMOVE))
                {
                    if (msg.message == WM_QUIT) { quitter = (int)msg.wParam; break; }
                    Win32.TranslateMessage(&msg);
                    Win32.DispatchMessage(&msg);
                }
                if (fenetre.Fermee || quitter != null) break;
                Entrees.Relever(actives, fenetre.Handle);
                ecran.Tour(actives);
                if (ecran.AChange) { ecran.AChange = false; fenetre.Redessiner(); }
                Thread.Sleep(8);
            }
        }
        finally
        {
            if (proprietaire != IntPtr.Zero)
            {
                Win32.EnableWindow(proprietaire, true);
                Win32.SetForegroundWindow(proprietaire);
            }
            relais?.Reprendre(config);
            if (quitter is { } code) Win32.PostQuitMessage(code);
        }
        return quitter == null;
    }

    /// <summary>
    /// La ligne d'état de la manette : la première manette branchée qui a un bouton configuré, ou
    /// pourquoi il n'y en a pas.
    /// </summary>
    internal static string Etat(ConfigManette config)
    {
        var manettes = config.Liaisons.Values.Where(e => e.Source != Source.Clavier).Select(e => (e.Source, e.Appareil)).ToHashSet();
        if (manettes.Count == 0) return "Manette : aucune manette configurée.";
        foreach (var m in Entrees.Manettes())
            if (manettes.Contains(m)) return $"Manette : {Entrees.NomManette(m.Source, m.Appareil)}.";
        return "Manette : configurée, mais pas branchée.";
    }
}
