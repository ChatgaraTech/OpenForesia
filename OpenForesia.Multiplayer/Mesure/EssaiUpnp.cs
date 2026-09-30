// L'essai de l'UPnP (tache 4.1), sans le jeu : trouver la box, lui faire ouvrir le port UDP, lire
// l'adresse publique, puis refermer le port aussitot. Chaque etape dit ce qu'elle a vu.

using System.Net;
using OpenForesia.Multiplayer.Hote;

namespace OpenForesia.Multiplayer.Mesure;

internal static class EssaiUpnp
{
    internal static int Executer(int port)
    {
        Console.Out.WriteLine("[ESSAI] recherche de la box (SSDP, 2 s)…");
        Upnp? box = Upnp.Trouver().GetAwaiter().GetResult();
        if (box == null)
        {
            Console.Out.WriteLine("[ESSAI] aucune box ne répond en UPnP (désactivé dans la box, ou pare-feu qui bloque la multidiffusion).");
            return 1;
        }
        Console.Out.WriteLine($"[ESSAI] box trouvée ; ce PC la voit depuis {box.AdresseLocale}");

        IPAddress? publique = box.AdressePublique().GetAwaiter().GetResult();
        Console.Out.WriteLine(publique == null ? "[ESSAI] la box ne donne pas son adresse publique"
            : Upnp.Privee(publique) ? $"[ESSAI] adresse « publique » privée : {publique} — double NAT, internet hors de portée"
            : $"[ESSAI] adresse publique : {publique}");

        bool ouvert = box.Ouvrir(port).GetAwaiter().GetResult();
        Console.Out.WriteLine(ouvert ? $"[ESSAI] port UDP {port} ouvert vers {box.AdresseLocale}" : $"[ESSAI] la box refuse d'ouvrir le port UDP {port}");
        if (ouvert)
        {
            box.Fermer().GetAwaiter().GetResult();
            Console.Out.WriteLine($"[ESSAI] port UDP {port} refermé");
        }
        return ouvert && publique != null && !Upnp.Privee(publique) ? 0 : 1;
    }
}
