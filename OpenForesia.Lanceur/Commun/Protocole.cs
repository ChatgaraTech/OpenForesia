// Le protocole du multijoueur : des datagrammes UDP, un en-tete de huit octets et un contenu par
// genre (openspec/changes/add-multiplayer-streaming, design D5).
//
//   0   'O' 'F'        la marque, pour ignorer tout ce qui n'est pas a nous
//   2   version
//   3   genre
//   4   numero         uint32, croissant par emetteur (le nonce du chiffrement au lot 4)
//   8   contenu
//
// Les entiers sont petit-boutistes. Le chiffrement (lot 4) enveloppera le datagramme entier ; en
// attendant, un paquet dont la marque, la version ou la longueur ne vont pas est ignore sans reponse.

using System.Buffers.Binary;

namespace OpenForesia.Lanceur.Commun;

internal enum Genre : byte
{
    /// <summary>Invité → hôte : je veux jouer. idClient.</summary>
    Bonjour = 1,
    /// <summary>Hôte → invité : idClient, joueur attribué (2 à 4), ou 0 si la partie est complète.</summary>
    Accueil = 2,
    /// <summary>Invité → hôte : idClient, puis le mot actuel et les trois précédents (une perte ne coûte rien).</summary>
    Manette = 3,
    /// <summary>Invité → hôte : idClient, horodatage de l'invité ; l'hôte le renvoie tel quel en <see cref="Echo"/>.</summary>
    Presence = 4,
    /// <summary>Hôte → invité : la <see cref="Presence"/> renvoyée — la latence est l'écart à l'arrivée.</summary>
    Echo = 5,
    /// <summary>Hôte → invité : 1 si la fenêtre du jeu n'est plus au premier plan (entrées ignorées), 0 à la reprise.</summary>
    Pause = 6,
    /// <summary>Invité → hôte : idClient ; je pars, libère mon joueur.</summary>
    AuRevoir = 7,
    /// <summary>
    /// Hôte → invité : un morceau d'image H.264 — numéro d'image, rang du morceau, nombre de
    /// morceaux, drapeaux (1 = image clé), puis au plus <see cref="Protocole.MorceauMax"/> octets.
    /// </summary>
    Image = 8,
    /// <summary>Invité → hôte : idClient ; une image m'est arrivée incomplète, envoie une image clé.</summary>
    ImageCle = 9,
    /// <summary>Hôte → invité : numéro de paquet de son, puis 5 ms de PCM 16 bits stéréo à 48 kHz.</summary>
    Son = 10,
}

internal static class Protocole
{
    internal const byte Version = 2;                     // 2 : chaque paquet chiffré (lot 4)
    internal const int PortParDefaut = 47200;
    internal const int Entete = 8;
    internal const int TailleMax = 1400;

    /// <summary>L'en-tête propre à un morceau d'image : numéro (4), rang (2), nombre (2), drapeaux (1).</summary>
    internal const int EnteteImage = 9;

    /// <summary>Les octets d'image par paquet : sous la charge utile d'un datagramme sur internet (~1 400).</summary>
    internal const int MorceauMax = 1200;

    /// <summary>Le son par paquet : 5 ms de PCM 16 bits stéréo à 48 kHz — 10 ms dépasseraient un datagramme.</summary>
    internal const int OctetsSon = 48000 / 200 * 4;

    /// <summary>Un paquet reçu, déjà vérifié : son genre, son numéro et son contenu.</summary>
    internal readonly ref struct Paquet(Genre genre, uint numero, ReadOnlySpan<byte> contenu)
    {
        public readonly Genre Genre = genre;
        public readonly uint Numero = numero;
        public readonly ReadOnlySpan<byte> Contenu = contenu;
    }

    /// <summary>Écrit l'en-tête dans <paramref name="tampon"/> ; le contenu suit à partir de <see cref="Entete"/>.</summary>
    internal static void EcrireEntete(Span<byte> tampon, Genre genre, uint numero)
    {
        tampon[0] = (byte)'O';
        tampon[1] = (byte)'F';
        tampon[2] = Version;
        tampon[3] = (byte)genre;
        BinaryPrimitives.WriteUInt32LittleEndian(tampon[4..], numero);
    }

    /// <summary>Faux si le datagramme n'est pas un paquet de cette version du protocole.</summary>
    internal static bool Lire(ReadOnlySpan<byte> datagramme, out Paquet paquet)
    {
        paquet = default;
        if (datagramme.Length < Entete || datagramme[0] != 'O' || datagramme[1] != 'F' || datagramme[2] != Version)
            return false;
        var genre = (Genre)datagramme[3];
        int attendu = genre switch
        {
            Genre.Bonjour or Genre.AuRevoir => 4,
            Genre.Accueil => 5,
            Genre.Manette => 4 + 4 * 2,
            Genre.Presence or Genre.Echo => 4 + 8,
            Genre.Pause => 1,
            Genre.Image => EnteteImage + 1,
            Genre.ImageCle => 4,
            Genre.Son => 4 + 4,
            _ => -1,
        };
        if (attendu < 0 || datagramme.Length - Entete < attendu) return false;
        paquet = new Paquet(genre, BinaryPrimitives.ReadUInt32LittleEndian(datagramme[4..]), datagramme[Entete..]);
        return true;
    }
}
