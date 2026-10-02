// Le mode hote, de bout en bout : une cle de session, le port ouvert par la box (UPnP), les codes
// d'invitation, les touches fantomes ecrites, le jeu lance, les invites servis jusqu'a la fermeture
// du jeu (ou Ctrl+C), la manette du joueur 1 relayee, puis le port referme et les touches remises.

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using OpenForesia.Lanceur.Commun;

namespace OpenForesia.Lanceur.Hote;

internal static class ModeHote
{
    /// <summary>
    /// L'hôte sans fenêtre (<c>--heberger --sans-fenetre</c>) : tout dans la console, pour les essais
    /// et le débogage. La fenêtre de l'hôte (<see cref="Interface.FenetreHote"/>) fait la même chose.
    /// </summary>
    internal static int ExecuterConsole(int port, string? dossierDonne, IPAddress? adressePubliqueDonnee)
    {
        string? dossier = Jeu.Trouver(dossierDonne);
        if (dossier == null)
        {
            Console.Error.WriteLine($"[HÔTE] {Jeu.Executable} introuvable ({dossierDonne ?? "ni à côté de cet exe, ni dans la build du dépôt"}).");
            return 2;
        }

        byte[] cle = Invitation.NouvelleCle();
        Upnp? box = OuvrirLePort(port, adressePubliqueDonnee, out IPAddress? publique);
        AfficherLesCodes(port, cle, publique, box?.AdresseLocale);
        try
        {
            using var session = new Session(dossier, port, cle, Qualite.ParDefaut, Invite.ConfigManette.Charger() ?? new Invite.ConfigManette());
            Console.Out.WriteLine("[HÔTE] les invités ne jouent que tant que la fenêtre du jeu est au premier plan. Ctrl+C ou fermer le jeu pour finir.");
            Console.CancelKeyPress += (_, e) => { e.Cancel = true; session.Arreter(); };
            session.Attendre();
            return session.Erreur == null ? 0 : 1;
        }
        finally
        {
            if (box != null)
            {
                box.Fermer().GetAwaiter().GetResult();
                Console.Out.WriteLine($"[HÔTE] port UDP {port} refermé sur la box");
            }
        }
    }

    /// <summary>
    /// Demande à la box d'ouvrir le port (UPnP) et rend l'adresse publique à mettre dans le code —
    /// celle donnée en option d'abord (une redirection faite à la main).
    /// </summary>
    private static Upnp? OuvrirLePort(int port, IPAddress? donnee, out IPAddress? publique)
    {
        publique = donnee;
        Console.Out.WriteLine("[HÔTE] recherche de la box (UPnP)…");
        Upnp? box = Upnp.Trouver().GetAwaiter().GetResult();
        if (box == null)
        {
            Console.Out.WriteLine("[HÔTE] aucune box ne répond en UPnP.");
            return null;
        }
        if (!box.Ouvrir(port).GetAwaiter().GetResult())
        {
            Console.Out.WriteLine($"[HÔTE] la box refuse d'ouvrir le port UDP {port}.");
            return null;
        }
        Console.Out.WriteLine($"[HÔTE] port UDP {port} ouvert par la box vers {box.AdresseLocale}");

        IPAddress? rendue = box.AdressePublique().GetAwaiter().GetResult();
        if (rendue != null && Upnp.Privee(rendue))
            Console.Out.WriteLine($"[HÔTE] ATTENTION : la box a elle-même une adresse privée ({rendue}) — double NAT (4G, certaines fibres). "
                                + "Les amis hors de ton réseau ne pourront sans doute pas te rejoindre.");
        else if (rendue != null) publique ??= rendue;
        return box;
    }

    private static void AfficherLesCodes(int port, byte[] cle, IPAddress? publique, IPAddress? locale)
    {
        Console.Out.WriteLine();
        if (publique != null)
            Console.Out.WriteLine($"[HÔTE] code d'invitation (internet)       : {new Invitation(publique, port, cle)}");
        else
            Console.Out.WriteLine($"[HÔTE] pas de code pour internet : redirige le port UDP {port} de ta box vers ce PC, "
                                + "puis relance avec --adresse-publique <ton adresse IP publique>.");
        foreach (IPAddress a in locale != null ? [locale] : AdressesLocales())
            Console.Out.WriteLine($"[HÔTE] code d'invitation (réseau local)   : {new Invitation(a, port, cle)}   ({a})");
        Console.Out.WriteLine();
    }

    /// <summary>Les adresses IPv4 de cette machine sur ses réseaux actifs.</summary>
    internal static IEnumerable<IPAddress> AdressesLocales() =>
        NetworkInterface.GetAllNetworkInterfaces()
            .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .SelectMany(n => n.GetIPProperties().UnicastAddresses)
            .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(a.Address))
            .Select(a => a.Address);
}
