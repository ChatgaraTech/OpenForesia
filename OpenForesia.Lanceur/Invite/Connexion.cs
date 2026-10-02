// La connexion de l'invite a l'hote : sa socket, la cle de session, son identifiant et son compteur
// de paquets (taches 4.3 et 4.4).
//
// Chaque paquet est scelle au nom de l'invite (son identifiant, jamais 0) ; seuls les paquets scelles
// par l'hote (emetteur 0) sont acceptes. L'identifiant ne change pas quand la connexion est refaite :
// c'est a lui que l'hote reconnait l'invite, et lui rend son joueur.

using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using OpenForesia.Lanceur.Commun;

namespace OpenForesia.Lanceur.Invite;

internal sealed class Connexion : IDisposable
{
    private readonly IPEndPoint _hote;
    private readonly Chiffrement _chiffrement;
    private readonly byte[] _envoi = new byte[Protocole.TailleMax];
    private readonly byte[] _recu = new byte[Protocole.TailleMax + Chiffrement.Surcout];
    private readonly byte[] _clair = new byte[Protocole.TailleMax];
    private Socket _socket;
    private uint _numero;

    internal uint Id { get; } = (uint)Random.Shared.Next() | 1;
    internal IPEndPoint Hote => _hote;

    internal Connexion(Invitation invitation)
    {
        _hote = new IPEndPoint(invitation.Adresse, invitation.Port);
        _chiffrement = new Chiffrement(invitation.Cle);
        _socket = Ouvrir();
    }

    private Socket Ouvrir()
    {
        var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        s.ReceiveBufferSize = 4 << 20;                  // une image clé arrive en rafale de morceaux
        s.Connect(_hote);
        const int SioUdpConnreset = unchecked((int)0x9800000C);
        s.IOControl(SioUdpConnreset, [0, 0, 0, 0], null);
        return s;
    }

    /// <summary>Une socket neuve, vers le même hôte : après un changement de réseau, l'ancienne peut viser une adresse locale qui n'existe plus.</summary>
    internal void Refaire()
    {
        _socket.Dispose();
        _socket = Ouvrir();
    }

    /// <summary>Envoie un paquet : l'identifiant de l'invité, puis <paramref name="suite"/>.</summary>
    internal void Envoyer(Genre genre, ReadOnlySpan<byte> suite)
    {
        Span<byte> clair = _envoi.AsSpan(0, Protocole.Entete + 4 + suite.Length);
        Protocole.EcrireEntete(clair, genre, ++_numero);
        BinaryPrimitives.WriteUInt32LittleEndian(clair[Protocole.Entete..], Id);
        suite.CopyTo(clair[(Protocole.Entete + 4)..]);
        Span<byte> scelle = stackalloc byte[clair.Length + Chiffrement.Surcout];
        int n = _chiffrement.Sceller(clair, Id, scelle);
        try { _socket.Send(scelle[..n]); }
        catch (SocketException) { /* l'hôte n'est pas joignable pour l'instant : on réessaiera */ }
    }

    /// <summary>
    /// Le prochain paquet de l'hôte, ouvert et vérifié ; faux quand il n'y a plus rien à lire. Les
    /// paquets qui ne viennent pas de l'hôte, ou pas scellés avec la clé de la partie, sont sautés.
    /// </summary>
    internal bool Recevoir(out Protocole.Paquet paquet)
    {
        paquet = default;
        while (_socket.Available > 0)
        {
            int n;
            try { n = _socket.Receive(_recu); }
            catch (SocketException) { return false; }
            if (!_chiffrement.Ouvrir(_recu.AsSpan(0, n), _clair, out int longueur, out uint emetteur)) continue;
            if (emetteur != Chiffrement.Hote) continue;
            if (Protocole.Lire(_clair.AsSpan(0, longueur), out paquet)) return true;
        }
        return false;
    }

    public void Dispose()
    {
        _socket.Dispose();
        _chiffrement.Dispose();
    }
}
