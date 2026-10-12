// Le relais de la manette du joueur 1 (openspec/changes/add-launcher-solo-gamepad, design D3, D4) :
// l'invite sans le reseau. Un fil releve les manettes toutes les 4 ms, compose le mot de 16 bits par
// la configuration de « ma manette » (manette.json) et le rejoue en touches fantomes dans la table 1
// du joueur 1 — la meme chaine que l'invite, Entrees -> ConfigManette.Mot -> Injection.
//
// Jamais le clavier : le jeu lit lui-meme celui du joueur 1. Le mot est nul tant que l'ecran de
// configuration est ouvert ou que le jeu n'est pas au premier plan : Injection relache alors ce qui
// etait enfonce, et la manette n'agit que sur le jeu. Tout est relache a l'arret.
//
// L'ecran de configuration modifie ses liaisons sur le fil de la fenetre ; le relais travaille donc
// sur une copie, remplacee quand l'ecran se ferme (Reprendre). Ecran ouvert, il n'en lit aucune.

using OpenForesia.Lanceur.Invite;
using OpenForesia.Lanceur.Natif;

namespace OpenForesia.Lanceur.Hote;

internal sealed class Relais : IDisposable
{
    private const int Periode = 4;                          // ms : plus vite qu'une image à 60 Hz
    private const int PeriodeManettes = 500;                // ms : la recherche des manettes branchées

    private readonly Jeu _jeu;
    private readonly Thread _fil;
    private volatile ConfigManette _config;
    private volatile bool _arret, _pause;
    private volatile string? _manette;

    /// <summary>
    /// La manette que le relais voit : la première branchée qui a au moins un bouton configuré, ou
    /// null — aucune branchée, ou aucune configurée.
    /// </summary>
    internal string? Manette => _manette;

    /// <summary>Le relais tourne encore : ni arrêté, ni le jeu fermé.</summary>
    internal bool Actif => _fil.IsAlive;

    internal Relais(Jeu jeu, ConfigManette config)
    {
        _jeu = jeu;
        _config = Copie(config);
        _fil = new Thread(Tourner) { IsBackground = true, Name = "Relais du joueur 1" };
        _fil.Start();
    }

    /// <summary>L'écran de configuration s'ouvre : plus rien n'est envoyé au jeu jusqu'à <see cref="Reprendre"/>.</summary>
    internal void Suspendre() => _pause = true;

    /// <summary>L'écran de configuration s'est fermé : le relais reprend, avec la configuration qu'il y a laissée.</summary>
    internal void Reprendre(ConfigManette config)
    {
        _config = Copie(config);
        _pause = false;
    }

    private static ConfigManette Copie(ConfigManette config) =>
        new() { Liaisons = new Dictionary<string, Entree>(config.Liaisons) };

    private void Tourner()
    {
        Win32.timeBeginPeriod(1);
        var actives = new HashSet<Entree>();
        ushort applique = 0;
        long prochaineRecherche = 0;
        try
        {
            while (!_arret && !_jeu.Termine)
            {
                if (Environment.TickCount64 >= prochaineRecherche)
                {
                    _manette = Chercher(_config);
                    prochaineRecherche = Environment.TickCount64 + PeriodeManettes;
                }

                Entrees.Relever(actives, IntPtr.Zero);
                ConfigManette config = _config;
                ushort mot = _pause || !_jeu.AuPremierPlan() ? (ushort)0 : config.Mot(actives);
                applique = Injection.Appliquer(1, applique, mot);
                Thread.Sleep(Periode);
            }
        }
        finally
        {
            Injection.Appliquer(1, applique, 0);
            _manette = null;
            Win32.timeEndPeriod(1);
        }
    }

    /// <summary>La première manette branchée dont un bouton est lié dans <paramref name="config"/>.</summary>
    private static string? Chercher(ConfigManette config)
    {
        foreach (var (source, appareil) in Entrees.Manettes())
            if (config.Liaisons.Values.Any(e => e.Source == source && e.Appareil == appareil))
                return Entrees.NomManette(source, appareil);
        return null;
    }

    /// <summary>Arrête le relais : toutes les touches du joueur 1 relâchées. Le jeu continue au clavier.</summary>
    public void Dispose()
    {
        _arret = true;
        if (_fil.IsAlive && Thread.CurrentThread != _fil) _fil.Join(1000);
    }
}
