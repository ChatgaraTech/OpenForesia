// Une image H.264 en paquets Image, et retour (tache 2.3, design D5).
//
// L'hote coupe chaque image en morceaux d'au plus 1 200 octets, numerotes. L'invite les recolle ;
// une image dont un morceau manque quand la suivante commence est jetee, et l'invite n'accepte plus
// que des images cles jusqu'a en recevoir une entiere — decoder une image dont la precedente manque
// donnerait des dechets a l'ecran. C'est lui qui la demande (ImageCle) ; pas de retransmission.

using System.Buffers.Binary;
using OpenForesia.Multiplayer.Commun;

namespace OpenForesia.Multiplayer.Video;

internal static class Morceaux
{
    internal const byte DrapeauCle = 1;

    /// <summary>Écrit chaque morceau de <paramref name="image"/> dans <paramref name="tampon"/> et le remet à <paramref name="envoyer"/>.</summary>
    internal static void Decouper(byte[] image, uint numeroImage, bool cle, byte[] tampon, Func<uint> numeroPaquet,
                                  Action<ReadOnlyMemory<byte>> envoyer)
    {
        int total = (image.Length + Protocole.MorceauMax - 1) / Protocole.MorceauMax;
        if (total > ushort.MaxValue) return;
        for (int i = 0; i < total; i++)
        {
            int debut = i * Protocole.MorceauMax, longueur = Math.Min(Protocole.MorceauMax, image.Length - debut);
            Span<byte> t = tampon.AsSpan(0, Protocole.Entete + Protocole.EnteteImage + longueur);
            Protocole.EcrireEntete(t, Genre.Image, numeroPaquet());
            Span<byte> c = t[Protocole.Entete..];
            BinaryPrimitives.WriteUInt32LittleEndian(c, numeroImage);
            BinaryPrimitives.WriteUInt16LittleEndian(c[4..], (ushort)i);
            BinaryPrimitives.WriteUInt16LittleEndian(c[6..], (ushort)total);
            c[8] = cle ? DrapeauCle : (byte)0;
            image.AsSpan(debut, longueur).CopyTo(c[Protocole.EnteteImage..]);
            envoyer(tampon.AsMemory(0, t.Length));
        }
    }
}

/// <summary>Le recollage, côté invité : une image à la fois.</summary>
internal sealed class Recolleur
{
    private uint _numero;
    private ushort _total;
    private bool _cle;
    private readonly List<byte[]?> _morceaux = new();
    private int _recus;
    private bool _attendCle = true;                 // au départ, rien ne se décode avant une image clé

    /// <summary>Vrai depuis une perte, jusqu'à la prochaine image clé complète : il faut en demander une.</summary>
    internal bool AttendCle => _attendCle;

    /// <summary>Ajoute un morceau ; rend l'image complète quand c'en est le dernier, et qu'elle peut être décodée.</summary>
    internal byte[]? Ajouter(ReadOnlySpan<byte> contenu)
    {
        uint numero = BinaryPrimitives.ReadUInt32LittleEndian(contenu);
        ushort rang = BinaryPrimitives.ReadUInt16LittleEndian(contenu[4..]);
        ushort total = BinaryPrimitives.ReadUInt16LittleEndian(contenu[6..]);
        bool cle = (contenu[8] & Morceaux.DrapeauCle) != 0;
        if (total == 0 || rang >= total) return null;

        if (numero != _numero || _morceaux.Count == 0)
        {
            if ((int)(numero - _numero) < 0 && _morceaux.Count > 0) return null;       // une image plus ancienne, en retard
            if (_morceaux.Count > 0 && _recus < _total) _attendCle = true;               // la précédente restera incomplète
            _numero = numero;
            _total = total;
            _cle = cle;
            _morceaux.Clear();
            for (int i = 0; i < total; i++) _morceaux.Add(null);
            _recus = 0;
        }

        if (_morceaux[rang] != null) return null;
        _morceaux[rang] = contenu[Protocole.EnteteImage..].ToArray();
        if (++_recus < _total) return null;

        if (_attendCle && !_cle) return null;
        _attendCle = false;
        int longueur = _morceaux.Sum(m => m!.Length);
        var image = new byte[longueur];
        int o = 0;
        foreach (byte[]? m in _morceaux) { m!.CopyTo(image, o); o += m.Length; }
        return image;
    }
}
