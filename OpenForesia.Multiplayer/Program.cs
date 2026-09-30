// OpenForesiaMultiplayer : jouer a quatre a distance, par le streaming du jeu de l'hote
// (openspec/changes/add-multiplayer-streaming).
//
// Un seul exe pour heberger et rejoindre. Lance sans rien (double-clic, raccourci « OpenForesia
// Multijoueur »), il ouvre l'accueil : heberger, ou rejoindre avec un code. Les modes
// suivants ecrivent dans la console qui les a lances :
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
using OpenForesia.Multiplayer.Commun;
using OpenForesia.Multiplayer.Hote;
using OpenForesia.Multiplayer.Invite;
using OpenForesia.Multiplayer.Mesure;

namespace OpenForesia.Multiplayer;

internal static class Program
{
    [DllImport("kernel32.dll")] private static extern bool AttachConsole(int processId);
    private const int ProcessusParent = -1;

    [STAThread]
    private static int Main(string[] args)
    {
        AttachConsole(ProcessusParent);
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        switch (args.FirstOrDefault())
        {
            case "--heberger":
                int port = int.TryParse(Option(args, "--port"), out int p0) ? p0 : Protocole.PortParDefaut;
                var publique = System.Net.IPAddress.TryParse(Option(args, "--adresse-publique"), out var a0) ? a0 : null;
                return args.Contains("--sans-fenetre")
                    ? ModeHote.ExecuterConsole(port, Option(args, "--jeu"), publique)
                    : Interface.FenetreHote.Executer(port, Option(args, "--jeu"), publique, upnp: !args.Contains("--sans-upnp"));
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
                // Sans rien (un double-clic, le raccourci) : l'accueil, puis ce qu'il a choisi.
                Interface.FenetreAccueil.Choix? choix = Interface.FenetreAccueil.Demander();
                if (choix == null) return 0;
                if (choix.Heberger) return Interface.FenetreHote.Executer(Protocole.PortParDefaut, null, null, upnp: true);
                return ModeInvite.Executer(choix.Code!, new ModeInvite.Options(Reconfigurer: choix.Reconfigurer));
            default:
                Console.Error.WriteLine("OpenForesiaMultiplayer : mode inconnu. Modes : --heberger, --rejoindre, les essais (voir Program.cs) ; sans rien, l'accueil.");
                return 64;
        }
    }

    /// <summary>La valeur qui suit <paramref name="nom"/> dans la ligne de commande, ou null.</summary>
    private static string? Option(string[] args, string nom)
    {
        int i = Array.IndexOf(args, nom);
        return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    }
}
