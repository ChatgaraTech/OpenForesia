// Le chiffrement de chaque paquet (tache 4.3, design D5) : AES-GCM, la cle de session du code
// d'invitation. Un paquet qui ne s'ouvre pas avec elle est ignore sans reponse : sans le code, on ne
// voit rien et on ne commande rien.
//
//   clair       en-tete (8)                         contenu
//   sur le fil  en-tete (8) | emetteur (4) | contenu chiffre | etiquette (16)
//
// L'en-tete et l'emetteur restent en clair mais sont authentifies (donnees associees). Le nonce
// (12 octets) = emetteur, numero, genre : unique tant qu'un emetteur ne reutilise jamais un numero —
// d'ou un seul compteur par emetteur (l'hote en a un pour tous ses fils). L'hote est l'emetteur 0 ;
// un invite, son identifiant (jamais 0). Un meme paquet envoye a trois invites est chiffre une fois.

using System.Buffers.Binary;
using System.Security.Cryptography;

namespace OpenForesia.Multiplayer.Commun;

internal sealed class Chiffrement : IDisposable
{
    internal const int Surcout = 4 + 16;                  // émetteur et étiquette
    internal const uint Hote = 0;

    private readonly AesGcm _aes;
    private readonly Lock _verrou = new();                 // AesGcm n'est pas sûr entre fils

    internal Chiffrement(byte[] cle) => _aes = new AesGcm(cle, 16);

    /// <summary>
    /// Chiffre le paquet clair <paramref name="clair"/> (en-tête compris) dans <paramref name="sortie"/>,
    /// au nom de <paramref name="emetteur"/> ; rend la longueur écrite.
    /// </summary>
    internal int Sceller(ReadOnlySpan<byte> clair, uint emetteur, Span<byte> sortie)
    {
        int e = Protocole.Entete;
        int contenu = clair.Length - e;
        clair[..e].CopyTo(sortie);
        BinaryPrimitives.WriteUInt32LittleEndian(sortie[e..], emetteur);
        Span<byte> nonce = stackalloc byte[12];
        Nonce(sortie, nonce);
        lock (_verrou)
            _aes.Encrypt(nonce, clair[e..], sortie.Slice(e + 4, contenu), sortie.Slice(e + 4 + contenu, 16), sortie[..(e + 4)]);
        return clair.Length + Surcout;
    }

    /// <summary>
    /// Ouvre un datagramme dans <paramref name="clair"/> (en-tête puis contenu) ; faux s'il n'a pas
    /// été scellé avec cette clé, ou s'il a été altéré. <paramref name="emetteur"/> dit qui l'a scellé.
    /// </summary>
    internal bool Ouvrir(ReadOnlySpan<byte> datagramme, Span<byte> clair, out int longueur, out uint emetteur)
    {
        int e = Protocole.Entete;
        longueur = 0;
        emetteur = 0;
        int contenu = datagramme.Length - e - Surcout;
        if (contenu < 0) return false;

        Span<byte> nonce = stackalloc byte[12];
        Nonce(datagramme, nonce);
        try
        {
            lock (_verrou)
                _aes.Decrypt(nonce, datagramme.Slice(e + 4, contenu), datagramme.Slice(e + 4 + contenu, 16),
                             clair.Slice(e, contenu), datagramme[..(e + 4)]);
        }
        catch (AuthenticationTagMismatchException) { return false; }

        datagramme[..e].CopyTo(clair);
        emetteur = BinaryPrimitives.ReadUInt32LittleEndian(datagramme[e..]);
        longueur = e + contenu;
        return true;
    }

    /// <summary>Émetteur (4), numéro (4), genre (1), puis trois zéros.</summary>
    private static void Nonce(ReadOnlySpan<byte> paquet, Span<byte> nonce)
    {
        nonce.Clear();
        paquet.Slice(Protocole.Entete, 4).CopyTo(nonce);
        paquet.Slice(4, 4).CopyTo(nonce[4..]);
        nonce[8] = paquet[3];
    }

    public void Dispose() => _aes.Dispose();
}
