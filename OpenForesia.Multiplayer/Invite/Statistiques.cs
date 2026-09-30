// Ce que la fenetre de l'invite affiche sous F2 : la qualite du flux, relevee par la boucle de
// l'invite (ModeInvite) et lue par la fenetre (FenetreInvite) au dessin. Un seul fil : pas de verrou.
//
// Deux historiques pour les courbes : l'ecart entre deux images affichees (les 180 dernieres, trois
// secondes a 60 images par seconde) — c'est lui qui dit si l'image est fluide —, et le debit recu,
// un point toutes les 250 ms sur 30 s.

namespace OpenForesia.Multiplayer.Invite;

internal sealed class Statistiques
{
    internal const int NombreEcarts = 180, NombreDebits = 120;
    private const double PasDebit = 250;

    private readonly double[] _ecarts = new double[NombreEcarts];
    private readonly double[] _debits = new double[NombreDebits];
    private readonly Queue<double> _instants = new();         // les images de la dernière seconde
    private int _ecartsPlein, _ecartSuivant, _debitsPlein, _debitSuivant;
    private double _derniereImage = -1, _debutDebit, _decodage;
    private long _octetsTranche;

    /// <summary>Images affichées dans la dernière seconde.</summary>
    internal int ImagesParSeconde => _instants.Count;
    /// <summary>Débit reçu sur la dernière tranche de 250 ms, en Mbit/s.</summary>
    internal double Debit { get; private set; }
    /// <summary>Durée moyenne du décodage et de la conversion d'une image, en ms (moyenne glissante).</summary>
    internal double Decodage => _decodage;
    /// <summary>Le plus grand écart entre deux images sur les trois dernières secondes, en ms.</summary>
    internal double PireEcart { get; private set; }

    internal long Latence = -1;
    internal int LargeurFlux, HauteurFlux, Perdues, DemandesCle, SonsJoues, SonsJetes, Reconnexions;

    /// <summary>Un paquet d'image reçu (entier ou non), de <paramref name="octets"/> octets.</summary>
    internal void Recu(int octets) => _octetsTranche += octets;

    /// <summary>Une image affichée à l'instant <paramref name="t"/> (ms), décodée en <paramref name="ms"/> ms.</summary>
    internal void Image(double t, double ms)
    {
        _decodage = _decodage == 0 ? ms : _decodage * 0.95 + ms * 0.05;
        if (_derniereImage >= 0)
        {
            _ecarts[_ecartSuivant] = t - _derniereImage;
            _ecartSuivant = (_ecartSuivant + 1) % NombreEcarts;
            _ecartsPlein = Math.Min(NombreEcarts, _ecartsPlein + 1);
        }
        _derniereImage = t;
        _instants.Enqueue(t);
    }

    /// <summary>À chaque tour de la boucle : fait vieillir la dernière seconde et ferme la tranche de débit.</summary>
    internal void Tour(double t)
    {
        while (_instants.Count > 0 && t - _instants.Peek() > 1000) _instants.Dequeue();
        if (t - _debutDebit < PasDebit) return;
        Debit = _octetsTranche * 8 / ((t - _debutDebit) / 1000) / 1e6;
        _debits[_debitSuivant] = Debit;
        _debitSuivant = (_debitSuivant + 1) % NombreDebits;
        _debitsPlein = Math.Min(NombreDebits, _debitsPlein + 1);
        _octetsTranche = 0;
        _debutDebit = t;

        double pire = 0;
        for (int i = 0; i < _ecartsPlein; i++) pire = Math.Max(pire, _ecarts[i]);
        PireEcart = pire;
    }

    /// <summary>Les écarts entre images, du plus ancien au plus récent.</summary>
    internal IEnumerable<double> Ecarts => Historique(_ecarts, _ecartsPlein, _ecartSuivant);

    /// <summary>Les débits (Mbit/s), du plus ancien au plus récent.</summary>
    internal IEnumerable<double> Debits => Historique(_debits, _debitsPlein, _debitSuivant);

    private static IEnumerable<double> Historique(double[] valeurs, int plein, int suivant)
    {
        for (int i = 0; i < plein; i++) yield return valeurs[(suivant - plein + i + valeurs.Length) % valeurs.Length];
    }

    /// <summary>Un mot sur la qualité d'ensemble, et s'il faut s'en inquiéter (0 bon, 1 moyen, 2 mauvais).</summary>
    internal (string Mot, int Niveau) Verdict()
    {
        int niveau = 0;
        if (ImagesParSeconde < 50 || PireEcart > 50 || Latence > 80) niveau = 1;
        if (ImagesParSeconde < 30 || PireEcart > 150 || Latence > 150) niveau = 2;
        return (niveau switch { 0 => "excellente", 1 => "correcte", _ => "dégradée" }, niveau);
    }
}
