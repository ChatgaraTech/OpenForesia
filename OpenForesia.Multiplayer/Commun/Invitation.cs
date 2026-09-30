// Le code d'invitation (tache 4.2, design D6) : tout ce qu'un ami doit savoir pour rejoindre —
// l'adresse IPv4 de l'hote, son port, et la cle de session de 128 bits qui chiffre la partie.
//
// 22 octets, ecrits en base 32 (l'alphabet de Crockford, sans I, L, O ni U pour qu'aucun caractere
// ne se lise de deux facons) : 36 caracteres, groupes par cinq. La saisie est tolerante : espaces,
// tirets et casse ignores ; O lu 0, I et L lus 1.

using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace OpenForesia.Multiplayer.Commun;

internal sealed record Invitation(IPAddress Adresse, int Port, byte[] Cle)
{
    private const string Alphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
    private const int Octets = 4 + 2 + 16;

    internal static byte[] NouvelleCle() => RandomNumberGenerator.GetBytes(16);

    public override string ToString()
    {
        var octets = new byte[Octets];
        Adresse.GetAddressBytes().CopyTo(octets, 0);
        octets[4] = (byte)(Port >> 8);
        octets[5] = (byte)Port;
        Cle.CopyTo(octets, 6);

        var texte = new StringBuilder();
        int tampon = 0, bits = 0;
        foreach (byte o in octets)
        {
            tampon = (tampon << 8) | o;
            bits += 8;
            while (bits >= 5)
            {
                texte.Append(Alphabet[(tampon >> (bits - 5)) & 31]);
                bits -= 5;
            }
        }
        if (bits > 0) texte.Append(Alphabet[(tampon << (5 - bits)) & 31]);

        // Groupé par cinq, pour se lire et se dicter.
        var groupes = new StringBuilder();
        for (int i = 0; i < texte.Length; i += 5)
            groupes.Append(i == 0 ? "" : "-").Append(texte.ToString(i, Math.Min(5, texte.Length - i)));
        return groupes.ToString();
    }

    /// <summary>Lit un code d'invitation ; null s'il n'en est pas un.</summary>
    internal static Invitation? Lire(string code)
    {
        var chiffres = new List<int>();
        foreach (char c0 in code.ToUpperInvariant())
        {
            char c = c0 switch { 'O' => '0', 'I' or 'L' => '1', _ => c0 };
            if (c is ' ' or '-' or '\t') continue;
            int v = Alphabet.IndexOf(c);
            if (v < 0) return null;
            chiffres.Add(v);
        }
        if (chiffres.Count != (Octets * 8 + 4) / 5) return null;

        var octets = new byte[Octets];
        int tampon = 0, bits = 0, n = 0;
        foreach (int v in chiffres)
        {
            tampon = (tampon << 5) | v;
            bits += 5;
            if (bits >= 8 && n < Octets)
            {
                octets[n++] = (byte)(tampon >> (bits - 8));
                bits -= 8;
            }
        }
        if (n != Octets) return null;
        int port = (octets[4] << 8) | octets[5];
        if (port == 0) return null;
        return new Invitation(new IPAddress(octets.AsSpan(0, 4)), port, octets[6..]);
    }
}
