// Une session d'hote, sur son propre fil : les touches fantomes ecrites, le jeu lance, l'image et
// le son diffuses, les invites servis et la manette du joueur 1 relayee jusqu'a la fermeture du jeu
// ou l'arret demande — puis les touches remises, si le jeu est ferme (sinon au lancement suivant du
// lanceur : add-launcher-solo-gamepad, design D6). La fenetre de l'hote (ou la console) garde la
// main pendant ce temps et lit l'etat : les joueurs connectes, la fin.

using OpenForesia.Lanceur.Invite;

namespace OpenForesia.Lanceur.Hote;

internal sealed class Session : IDisposable
{
    private readonly string _dossier;
    private readonly int _port;
    private readonly byte[] _cle;
    private readonly Commun.Qualite _qualite;
    private readonly ConfigManette _config;
    private readonly Thread _fil;
    private volatile bool _arret;
    private volatile Serveur? _serveur;
    private volatile Relais? _relais;

    /// <summary>La session est finie : le jeu est fermé, l'arrêt demandé, ou une erreur l'a interrompue.</summary>
    internal bool Terminee { get; private set; }

    /// <summary>Ce qui l'a interrompue, s'il y a eu une erreur.</summary>
    internal string? Erreur { get; private set; }

    /// <summary>Les joueurs (2 à 4) tenus par un invité en ce moment.</summary>
    internal int[] Joueurs => _serveur?.Joueurs ?? [];

    /// <summary>Le relais de la manette du joueur 1, tant que la partie tourne.</summary>
    internal Relais? Relais => _relais;

    internal Session(string dossier, int port, byte[] cle, Commun.Qualite qualite, ConfigManette config)
    {
        _dossier = dossier;
        _port = port;
        _cle = cle;
        _qualite = qualite;
        _config = config;
        _fil = new Thread(Tourner) { IsBackground = true, Name = "Session" };
        _fil.Start();
    }

    private void Tourner()
    {
        string cfg = Path.Combine(_dossier, "OpenForesia.cfg");
        Jeu? lance = null;
        Natif.Win32.timeBeginPeriod(1);
        try
        {
            ConfigTouches.Ecrire(cfg, [1, 2, 3, 4]);
            using var serveur = new Serveur(_port, _cle);
            Jeu jeu = lance = Jeu.Lancer(_dossier);         // libéré dans finally, après la question « fermé ? »
            using var relais = new Relais(jeu, _config);
            using var diffusion = new Diffusion(jeu.Fenetre, serveur, _qualite);
            using var son = new DiffusionSon(jeu.Pid, serveur);
            _serveur = serveur;
            _relais = relais;

            Console.Out.WriteLine($"[HÔTE] jeu lancé depuis {_dossier}");
            serveur.Executer(() => !_arret && !jeu.Termine, jeu.AuPremierPlan);
            Console.Out.WriteLine(jeu.Termine ? "[HÔTE] le jeu est fermé : fin de la session" : "[HÔTE] arrêt demandé : fin de la session");
        }
        catch (Exception e)
        {
            Erreur = e.Message;
            Console.Error.WriteLine($"[HÔTE] session interrompue : {e.GetType().Name} : {e.Message}");
        }
        finally
        {
            _serveur = null;
            _relais = null;
            // Le jeu réécrit [Controls] depuis sa mémoire en sortant de ses Options : remettre le cfg
            // pendant qu'il tourne laisserait les touches fantômes y revenir, sans sauvegarde.
            if (lance == null || lance.Termine) ConfigTouches.Remettre(cfg);
            else Console.Out.WriteLine("[HÔTE] le jeu tourne encore : les touches seront remises au prochain lancement du lanceur");
            lance?.Dispose();
            Natif.Win32.timeEndPeriod(1);
            Terminee = true;
        }
    }

    /// <summary>Demande l'arrêt : les touches des invités et du joueur 1 sont relâchées, le jeu continue seul.</summary>
    internal void Arreter() => _arret = true;

    /// <summary>Attend la fin de la session.</summary>
    internal void Attendre() => _fil.Join();

    public void Dispose()
    {
        Arreter();
        _fil.Join(5000);
    }
}
