// L'essai du son (lot 3), sans le jeu ni le reseau : ce processus joue une note de 440 Hz par la
// sortie de l'invite (SortieSon) et la capture en meme temps par la capture de l'hote
// (CaptureSonProcessus, sur son propre arbre de processus). La capture doit retrouver la note :
// sa frequence (par les passages a zero) et un niveau non nul.
//
// La note est discrete (-30 dB) et dure deux secondes.

using System.Diagnostics;
using OpenForesia.Multiplayer.Commun;
using OpenForesia.Multiplayer.Natif;

namespace OpenForesia.Multiplayer.Mesure;

internal static class EssaiSon
{
    private const double Note = 440, Amplitude = 0.03;

    internal static int Executer()
    {
        int resultat = 1;
        // La capture de processus s'active depuis un fil multithread ; Main est STA.
        var fil = new Thread(() => resultat = Tourner());
        fil.SetApartmentState(ApartmentState.MTA);
        fil.Start();
        fil.Join();
        return resultat;
    }

    /// <summary>La note, jouée par paquets de 5 ms jusqu'à ce que <paramref name="fini"/> dise vrai (fil multithread).</summary>
    internal static void JouerNote(Func<bool> fini)
    {
        Com.InitialiserFil();
        using var sortie = new SortieSon();
        var paquet = new byte[Protocole.OctetsSon];
        int trame = 0;
        var montre = Stopwatch.StartNew();
        long prochain = 0;
        while (!fini())
        {
            if (montre.ElapsedMilliseconds >= prochain)
            {
                prochain += 5;
                RemplirNote(paquet, ref trame);
                sortie.Jouer(paquet);
            }
            Thread.Sleep(1);
        }
    }

    private static void RemplirNote(byte[] paquet, ref int trame)
    {
        for (int i = 0; i < paquet.Length / 4; i++, trame++)
        {
            short v = (short)(Math.Sin(2 * Math.PI * Note * trame / Audio.Frequence) * Amplitude * short.MaxValue);
            BitConverter.TryWriteBytes(paquet.AsSpan(4 * i), v);
            BitConverter.TryWriteBytes(paquet.AsSpan(4 * i + 2), v);
        }
    }

    private static int Tourner()
    {
        Com.InitialiserFil();
        using var capture = new CaptureSonProcessus((uint)Environment.ProcessId);
        using var sortie = new SortieSon();

        var capte = new List<byte>();
        var paquet = new byte[Protocole.OctetsSon];
        int trame = 0, joues = 0, jetes = 0;
        var montre = Stopwatch.StartNew();
        long prochain = 0;
        while (montre.ElapsedMilliseconds < 2000)
        {
            // 5 ms de note à chaque tour de 5 ms, comme les paquets de l'hôte.
            if (montre.ElapsedMilliseconds >= prochain)
            {
                prochain += 5;
                RemplirNote(paquet, ref trame);
                if (sortie.Jouer(paquet)) joues++; else jetes++;
            }
            capture.Lire(capte);
            Thread.Sleep(1);
        }
        capture.Lire(capte);

        // Canal gauche : niveau, et passages montants avec hystérésis — de sous -seuil à au-dessus de
        // +seuil — pour qu'un trou de silence ne compte pas comme un passage.
        byte[] octets = capte.ToArray();
        int n = octets.Length / 4;
        double somme = 0;
        int passages = 0;
        bool bas = false;
        int premier = -1, dernier = -1;
        short seuil = (short)(Amplitude * short.MaxValue * 0.3);
        for (int i = 0; i < n; i++)
        {
            short v = BitConverter.ToInt16(octets, 4 * i);
            somme += (double)v * v;
            if (v < -seuil) bas = true;
            else if (v > seuil && bas)
            {
                bas = false;
                passages++;
                if (premier < 0) premier = i;
                dernier = i;
            }
        }
        double efficace = n > 0 ? Math.Sqrt(somme / n) / short.MaxValue : 0;
        double frequence = passages > 1 ? (passages - 1) * (double)Audio.Frequence / (dernier - premier) : 0;

        Console.Out.WriteLine($"[ESSAI] sortie : {joues} paquets de 5 ms joués, {jetes} jetés, {sortie.Assechements} fois à sec (réserve refaite)");
        Console.Out.WriteLine($"[ESSAI] capture : {n / (double)Audio.Frequence:0.00} s de son, niveau efficace {efficace:0.0000} "
                            + $"(attendu ~{Amplitude / Math.Sqrt(2):0.0000}), fréquence {frequence:0} Hz (attendu {Note})");
        return Math.Abs(frequence - Note) < 10 && efficace > 0.005 ? 0 : 1;
    }
}
