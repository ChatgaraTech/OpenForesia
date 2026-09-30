// Le mode invite : configurer sa manette une fois (tache 1.7), puis rejoindre l'hote et lui envoyer
// son mot de 16 bits 120 fois par seconde, avec les trois precedents (tache 1.8).

using System.Buffers.Binary;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using OpenForesia.Multiplayer.Commun;
using OpenForesia.Multiplayer.Natif;
using OpenForesia.Multiplayer.Video;

namespace OpenForesia.Multiplayer.Invite;

internal static unsafe partial class ModeInvite
{
    [LibraryImport("winmm.dll")] private static partial uint timeBeginPeriod(uint ms);
    [LibraryImport("winmm.dll")] private static partial uint timeEndPeriod(uint ms);

    private const long PasManette = 1000 / 120, PasBonjour = 500, PasPresence = 1000, PasDemandeCle = 250, SilenceHote = 3000;
    private const int Echap = 0x1B;

    /// <summary>
    /// Les options de l'invité. Les trois dernières servent aux essais sans personne devant : aucune
    /// manette (rien n'est configuré ni envoyé), une durée au bout de laquelle la fenêtre se ferme,
    /// un fichier où écrire la dernière image décodée, et l'instant (ms) où refaire la connexion
    /// comme après une coupure du réseau (l'essai de la reconnexion).
    /// </summary>
    internal sealed record Options(bool Reconfigurer, bool SansManette = false, int? Duree = null, string? DerniereImage = null,
                                   int? RefaireA = null);

    internal static int Executer(string code, Options options)
    {
        Invitation? invitation = Invitation.Lire(code);
        if (invitation == null)
        {
            Console.Error.WriteLine($"[INVITÉ] ce n'est pas un code d'invitation : {code}");
            return 2;
        }

        timeBeginPeriod(1);
        using var fenetre = new FenetreInvite("OpenForesia — invité");
        try
        {
            ConfigManette? config = options.SansManette ? new ConfigManette()
                                  : options.Reconfigurer ? null : ConfigManette.Charger();
            if (config == null)
            {
                config = Configurer(fenetre);
                if (config == null) return 0;                    // fenêtre fermée pendant la configuration
                config.Enregistrer();
            }
            using var connexion = new Connexion(invitation);
            return Jouer(fenetre, config, connexion, options);
        }
        finally
        {
            timeEndPeriod(1);
        }
    }

    // ---- la configuration ------------------------------------------------------------------------

    /// <summary>
    /// Les quatorze boutons, un par un : l'entrée qui s'active (et n'était pas active juste avant) est
    /// retenue. Échap laisse le bouton sans entrée. Une entrée déjà prise par un autre bouton est
    /// ignorée. Rend null si la fenêtre est fermée.
    /// </summary>
    private static ConfigManette? Configurer(FenetreInvite fenetre)
    {
        var config = new ConfigManette();
        var avant = new HashSet<Entree>();
        var actives = new HashSet<Entree>();

        for (int i = 0; i < Pad.Boutons.Length; i++)
        {
            Pad.Bouton b = Pad.Boutons[i];
            fenetre.Texte($"Configuration de la manette — {i + 1} / {Pad.Boutons.Length}\n\n"
                        + $"Appuie sur le bouton qui sera\n{b.Nom}\n\n(Échap : aucun)");

            Entree? choisie = null;
            bool aucun = false;
            Entrees.Relever(avant, fenetre.Handle);
            var auRepos = new HashSet<Entree>(avant);           // ce qui était actif avant tout appui (un axe décentré au repos)
            while (choisie == null && !aucun)
            {
                if (!fenetre.Pomper()) return null;
                Thread.Sleep(5);
                Entrees.Relever(actives, fenetre.Handle);
                Entree? nouvelle = actives.Where(e => !avant.Contains(e))
                                          .OrderBy(e => e.Source switch { Source.XInput => 0, Source.Joystick => 1, _ => 2 })
                                          .Cast<Entree?>().FirstOrDefault();
                avant.Clear();
                avant.UnionWith(actives);
                if (nouvelle is not { } e) continue;
                if (e.Source == Source.Clavier && e.Element == Echap) aucun = true;
                else if (!config.Liaisons.ContainsValue(e)) choisie = e;
            }

            if (choisie is { } c)
            {
                config.Liaisons[b.Nom] = c;
                fenetre.Texte($"{b.Nom} = {c.Libelle}");
            }

            // Attendre que tout soit revenu au repos, pour qu'un appui ne serve pas deux boutons.
            do
            {
                if (!fenetre.Pomper()) return null;
                Thread.Sleep(5);
                Entrees.Relever(actives, fenetre.Handle);
            } while (actives.Any(e => !auRepos.Contains(e)));
        }
        return config;
    }

    // ---- le jeu ----------------------------------------------------------------------------------

    private static int Jouer(FenetreInvite fenetre, ConfigManette config, Connexion connexion, Options options)
    {
        using var decodeur = new DecodeurH264();
        SortieSon? son = null;
        try { son = new SortieSon(); }
        catch (Exception e) { Console.Error.WriteLine($"[INVITÉ] pas de sortie son ({e.Message}) : le jeu sera muet"); }
        uint dernierSon = 0;
        var recolleur = new Recolleur();
        byte[]? bgra = null;
        bool image = false;                              // une image est déjà arrivée
        long derniereDemandeCle = long.MinValue / 2;

        int joueur = -1;                                 // -1 : en attente ; 0 : partie complète
        bool pause = false;
        long latence = -1;
        long dernierMot = 0;                             // la dernière nouvelle de l'hôte
        long prochaineReconnexion = 0;
        ushort[] historique = new ushort[4];
        var actives = new HashSet<Entree>();
        var montre = Stopwatch.StartNew();
        long prochainBonjour = 0, prochaineManette = 0, prochainePresence = 0;
        Span<byte> mots = stackalloc byte[8];
        Span<byte> heure = stackalloc byte[8];

        int recues = 0, decodees = 0, demandesCle = 0, sonsJoues = 0, sonsJetes = 0, reconnexions = 0;
        while (fenetre.Pomper() && (options.Duree is not { } duree || montre.ElapsedMilliseconds < duree * 1000L))
        {
            long t = montre.ElapsedMilliseconds;

            while (connexion.Recevoir(out var p))
            {
                dernierMot = t;
                switch (p.Genre)
                {
                    case Genre.Accueil when BinaryPrimitives.ReadUInt32LittleEndian(p.Contenu) == connexion.Id:
                        joueur = p.Contenu[4];
                        break;
                    case Genre.Echo when BinaryPrimitives.ReadUInt32LittleEndian(p.Contenu) == connexion.Id:
                        latence = t - BinaryPrimitives.ReadInt64LittleEndian(p.Contenu[4..]);
                        break;
                    case Genre.Pause:
                        pause = p.Contenu[0] != 0;
                        break;
                    case Genre.Son when joueur > 0 && son != null:
                        uint numeroSon = BinaryPrimitives.ReadUInt32LittleEndian(p.Contenu);
                        if ((int)(numeroSon - dernierSon) <= 0 && dernierSon != 0) break;     // en retard : déjà dépassé
                        dernierSon = numeroSon;
                        if (son.Jouer(p.Contenu[4..])) sonsJoues++; else sonsJetes++;
                        break;
                    case Genre.Image when joueur > 0:
                        byte[]? h264 = recolleur.Ajouter(p.Contenu);
                        if (h264 == null) break;
                        recues++;
                        decodeur.Decoder(h264, (nv12, l, h) =>
                        {
                            if (bgra == null || bgra.Length != l * h * 4) bgra = new byte[l * h * 4];
                            fixed (byte* b = bgra)
                            {
                                Conversion.Nv12VersBgra((byte*)nv12, l, h, b);
                                fenetre.Image(b, l, h);
                            }
                            image = true;
                            decodees++;
                        });
                        break;
                }
            }

            // L'hôte se tait depuis 3 s : la connexion a pu changer (tâche 4.4). On refait la socket et
            // on se ré-annonce sous le même identifiant — l'hôte rend le même joueur pendant 30 s.
            if (options.RefaireA is { } refaire && t >= refaire && reconnexions == 0) { connexion.Refaire(); reconnexions++; }
            bool perdu = joueur > 0 && t - dernierMot > SilenceHote;
            if (perdu && t >= prochaineReconnexion)
            {
                prochaineReconnexion = t + SilenceHote;
                connexion.Refaire();
                reconnexions++;
            }

            // Une image perdue : l'hôte en renvoie une clé, sans quoi l'écran resterait figé.
            if (joueur > 0 && recolleur.AttendCle && t - derniereDemandeCle >= PasDemandeCle)
            {
                derniereDemandeCle = t;
                connexion.Envoyer(Genre.ImageCle, []);
                demandesCle++;
            }

            if ((joueur < 0 || perdu) && t >= prochainBonjour)
            {
                prochainBonjour = t + PasBonjour;
                connexion.Envoyer(Genre.Bonjour, []);
            }

            if (joueur > 0 && t >= prochaineManette)
            {
                prochaineManette = t + PasManette;
                Entrees.Relever(actives, fenetre.Handle);
                Array.Copy(historique, 0, historique, 1, 3);
                historique[0] = config.Mot(actives);
                for (int i = 0; i < 4; i++) BinaryPrimitives.WriteUInt16LittleEndian(mots[(2 * i)..], historique[i]);
                connexion.Envoyer(Genre.Manette, mots);
            }

            if (joueur > 0 && t >= prochainePresence)
            {
                prochainePresence = t + PasPresence;
                BinaryPrimitives.WriteInt64LittleEndian(heure, t);
                connexion.Envoyer(Genre.Presence, heure);
            }

            fenetre.Texte(joueur switch
            {
                < 0 => $"Connexion à {connexion.Hote}…",
                0 => "La partie est complète : trois invités jouent déjà.",
                _ => $"Tu es le joueur {joueur}\n\nEn attente de l'image du jeu…"
                   + (pause ? "\n\nEn pause : la fenêtre du jeu n'est plus au premier plan chez l'hôte." : ""),
            });
            if (image)
                fenetre.Etat(perdu ? $"Joueur {joueur} — connexion perdue, reconnexion…"
                           : pause ? $"Joueur {joueur} — en pause : la fenêtre du jeu n'est plus au premier plan chez l'hôte"
                           : $"Joueur {joueur}" + (latence >= 0 ? $" — {latence} ms" : ""),
                    alerte: pause || perdu);

            Thread.Sleep(1);
        }

        for (int i = 0; i < 3; i++) connexion.Envoyer(Genre.AuRevoir, []);
        Console.Out.WriteLine($"[INVITÉ] {montre.Elapsed.TotalSeconds:0.0} s : joueur {joueur}, {recues} images reçues entières, {decodees} décodées, "
                            + $"{demandesCle} demandes d'image clé, son : {sonsJoues} paquets joués, {sonsJetes} jetés, "
                            + $"{reconnexions} reconnexions, latence {latence} ms");
        son?.Dispose();
        if (options.DerniereImage != null && bgra != null)
            Mesure.EssaiImage.EcrireBmp(options.DerniereImage, bgra, decodeur.Largeur, decodeur.Hauteur);
        return 0;
    }
}
