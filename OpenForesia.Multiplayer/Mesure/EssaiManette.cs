// L'ecran de la manette seul, sans reseau ni partie : pour le voir, et pour configurer sa manette
// hors partie. La configuration est celle de %APPDATA%\OpenForesia\manette.json, et les changements
// y sont enregistres. Se ferme avec la fenetre, ou apres <secondes> si on en donne.

using System.Diagnostics;
using OpenForesia.Multiplayer.Invite;

namespace OpenForesia.Multiplayer.Mesure;

internal static class EssaiManette
{
    internal static int Executer(int? secondes)
    {
        ConfigManette config = ConfigManette.Charger() ?? new ConfigManette();
        using var fenetre = new FenetreInvite("OpenForesia — manette");
        var ecran = new EcranManette(config);
        fenetre.Ecran = ecran;
        ecran.Basculer();
        fenetre.Redessiner();

        var actives = new HashSet<Entree>();
        var montre = Stopwatch.StartNew();
        while (fenetre.Pomper() && (secondes is not { } s || montre.ElapsedMilliseconds < s * 1000L))
        {
            if (!ecran.Ouvert) break;                           // « Fermer » ou F1
            Entrees.Relever(actives, fenetre.Handle);
            ecran.Tour(actives);
            if (ecran.AChange) { ecran.AChange = false; fenetre.Redessiner(); }
            Thread.Sleep(8);
        }
        return 0;
    }
}
