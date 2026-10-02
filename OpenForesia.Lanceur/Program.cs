// OpenForesiaLanceur : la porte d'entree du jeu — heberger, rejoindre, et jouer a quatre a distance
// par le streaming du jeu de l'hote (openspec/changes/add-multiplayer-streaming, renomme par
// openspec/changes/add-launcher-solo-gamepad).
//
// Un seul exe pour jouer seul, heberger et rejoindre. Lance sans rien (double-clic, raccourci
// « OpenForesia Lanceur »), il ouvre l'accueil : jouer seul, heberger, rejoindre avec un code,
// configurer sa manette — sans le jeu a cote, seulement rejoindre et la manette. Les modes
// suivants ecrivent dans la console qui les a lances :
//
//   --invite-seul
//       l'accueil comme chez un ami qui n'a que le lanceur : rejoindre et la manette seuls
//
//   --solo [--jeu <dossier>]
//       jouer seul avec sa manette, dans la console : ecrit les touches fantomes du joueur 1 (sa
//       table 1) dans OpenForesia.cfg, lance le jeu et relaie la manette configuree (manette.json)
//       jusqu'a la fermeture du jeu ; remet les touches apres. Le clavier du joueur 1 reste actif
//
//   --heberger [--port <n>] [--jeu <dossier>] [--adresse-publique <ip>] [--sans-upnp] [--sans-fenetre]
//       la fenetre de l'hote : UPnP a cocher, port, adresse publique, codes d'invitation a copier,
//       puis « Lancer la partie ». Avec --sans-fenetre, tout se passe dans la console :
//       ecrit les touches fantomes des joueurs 2 a 4 dans OpenForesia.cfg, lance le jeu et sert les
//       invites sur le port UDP (47200 par defaut) jusqu'a la fermeture du jeu ; remet les touches.
//       Ouvre le port par UPnP et affiche les codes d'invitation ; --adresse-publique fait le code
//       internet quand la box refuse l'UPnP (port redirige a la main)
//
//   --rejoindre <code d'invitation> [--configurer]
//       rejoint l'hote ; configure la manette au premier lancement (ou avec --configurer), puis envoie
//       ses boutons ; la configuration est gardee dans %APPDATA%\OpenForesia\manette.json
//
//       essais sans personne devant : --sans-manette, --duree <s>, --derniere-image <bmp>,
//       --refaire-a <ms> (refait la connexion a cet instant, comme apres une coupure du reseau)
//
//   --essai-image [<secondes>] [<bmp>] [--qualite Haute|Normale|Econome]
//       la chaine d'image en boucle fermee, sur une mire : capture, H.264, morceaux, decodage,
//       affichage ; les durees de chaque etape, et la derniere image en BMP
//
//   --essai-diffusion [<secondes>] [--port <n>]
//       un hote qui diffuse la mire et une note au lieu du jeu, toujours en pause (aucune touche
//       simulee) ; il affiche le code a donner a --rejoindre
//
//   --manette [--duree <s>]
//       l'ecran de configuration de la manette seul (celui de F1 en partie), sans reseau
//
//   --essai-upnp [--port <n>]
//       trouve la box, lui fait ouvrir le port UDP, lit l'adresse publique, referme le port
//
//   --essai-son
//       joue une note de 440 Hz et la capture par la capture de processus de l'hote : les deux
//       moities du son, sans le jeu
//
//   --mesurer-touches [<rapport>]
//       la mesure des touches fantomes (tache 1.1) : frappe chaque code candidat dans une fenetre a
//       nous et releve ce que DirectInput et WM_CHAR en recoivent ; rapport ecrit dans
//       mesure-touches.txt par defaut. Ne touchez pas au clavier pendant les quelques secondes.

using System.Runtime.InteropServices;
using OpenForesia.Lanceur.Commun;
using OpenForesia.Lanceur.Hote;
using OpenForesia.Lanceur.Invite;
using OpenForesia.Lanceur.Mesure;

namespace OpenForesia.Lanceur;

internal static class Program
{
    [DllImport("kernel32.dll")] private static extern bool AttachConsole(int processId);
    private const int ProcessusParent = -1;

    [STAThread]
    private static int Main(string[] args)
    {
        AttachConsole(ProcessusParent);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        RemettreTouches(Option(args, "--jeu"));

        switch (args.FirstOrDefault())
        {
            case "--heberger":
                int port = int.TryParse(Option(args, "--port"), out int p0) ? p0 : Protocole.PortParDefaut;
                var publique = System.Net.IPAddress.TryParse(Option(args, "--adresse-publique"), out var a0) ? a0 : null;
                return args.Contains("--sans-fenetre")
                    ? ModeHote.ExecuterConsole(port, Option(args, "--jeu"), publique)
                    : Interface.FenetreHote.Executer(port, Option(args, "--jeu"), publique, upnp: !args.Contains("--sans-upnp"));
            case "--solo":
                return Solo.ModeSolo.ExecuterConsole(Option(args, "--jeu"));
            case "--rejoindre" when args.Length > 1:
                return ModeInvite.Executer(args[1], new ModeInvite.Options(
                    Reconfigurer: args.Contains("--configurer"),
                    SansManette: args.Contains("--sans-manette"),
                    Duree: int.TryParse(Option(args, "--duree"), out int duree) ? duree : null,
                    DerniereImage: Option(args, "--derniere-image"),
                    RefaireA: int.TryParse(Option(args, "--refaire-a"), out int refaire) ? refaire : null));
            case "--essai-diffusion":
                return EssaiDiffusion.Executer(args.Length > 1 && int.TryParse(args[1], out int d) ? d : 10,
                                               int.TryParse(Option(args, "--port"), out int p) ? p : Protocole.PortParDefaut);
            case "--manette":
                return EssaiManette.Executer(int.TryParse(Option(args, "--duree"), out int dm) ? dm : null);
            case "--essai-upnp":
                return EssaiUpnp.Executer(int.TryParse(Option(args, "--port"), out int pu) ? pu : Protocole.PortParDefaut);
            case "--essai-son":
                return EssaiSon.Executer();
            case "--essai-image":
                return EssaiImage.Executer(args.Length > 1 && int.TryParse(args[1], out int s) ? s : 5, args.Length > 2 && !args[2].StartsWith("--") ? args[2] : null,
                    Qualite.Toutes.FirstOrDefault(q => string.Equals(q.Nom, Option(args, "--qualite"), StringComparison.OrdinalIgnoreCase)) ?? Qualite.ParDefaut);
            case "--mesurer-touches":
                return MesureTouches.Executer(args.Length > 1 ? args[1] : "mesure-touches.txt");
            case null:
            case "--invite-seul":
                // Sans rien (un double-clic, le raccourci) : l'accueil, puis ce qu'il a choisi. Sans
                // le jeu à côté — ou avec --invite-seul, pour l'essayer dans le dépôt —, il n'offre
                // que Rejoindre et la manette (add-launcher-solo-gamepad, design D8).
                bool jeu = args.FirstOrDefault() != "--invite-seul" && Jeu.Trouver(null) != null;
                Interface.FenetreAccueil.Choix? choix = Interface.FenetreAccueil.Demander(jeu);
                return choix?.Mode switch
                {
                    null => 0,
                    Interface.FenetreAccueil.Mode.Solo => Solo.ModeSolo.ExecuterFenetre(null),
                    Interface.FenetreAccueil.Mode.Heberger => Interface.FenetreHote.Executer(Protocole.PortParDefaut, null, null, upnp: true),
                    // Reconfigurer depuis l'accueil, c'est son bouton « Configurer ma manette » ; la
                    // première connexion ouvre de toute façon l'écran si rien n'est configuré.
                    _ => ModeInvite.Executer(choix.Code!, new ModeInvite.Options(Reconfigurer: false)),
                };
            default:
                Console.Error.WriteLine("OpenForesiaLanceur : mode inconnu. Modes : --heberger, --rejoindre, les essais (voir Program.cs) ; sans rien, l'accueil.");
                return 64;
        }
    }

    /// <summary>
    /// Remet les touches qu'une session interrompue a laissées dans le cfg du jeu, avant tout le reste
    /// et quel que soit le mode (add-launcher-solo-gamepad, design D6). Rien si le jeu n'est pas à
    /// côté, ni s'il tourne : une session en cours (un autre lanceur) a encore besoin de ses touches.
    /// </summary>
    private static void RemettreTouches(string? dossierDonne)
    {
        string? dossier = Jeu.Trouver(dossierDonne);
        if (dossier == null) return;
        if (System.Diagnostics.Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Jeu.Executable)).Length > 0) return;
        try { ConfigTouches.Remettre(Path.Combine(dossier, "OpenForesia.cfg")); }
        catch (Exception e) { Console.Error.WriteLine($"[LANCEUR] remise des touches impossible : {e.Message}"); }
    }

    /// <summary>La valeur qui suit <paramref name="nom"/> dans la ligne de commande, ou null.</summary>
    private static string? Option(string[] args, string nom)
    {
        int i = Array.IndexOf(args, nom);
        return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    }
}
