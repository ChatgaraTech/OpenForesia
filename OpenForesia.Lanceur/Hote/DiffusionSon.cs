// La diffusion du son du jeu (tache 3.1) : un fil a part, qui capture le son de l'arbre de
// processus du jeu (le lanceur et le vrai jeu, son enfant) et l'envoie par paquets de 5 ms.
//
// Le son est lu en continu, meme sans invite, pour que rien ne s'accumule chez Windows ; il n'est
// envoye que s'il y a quelqu'un pour l'entendre.

using OpenForesia.Lanceur.Commun;
using OpenForesia.Lanceur.Natif;

namespace OpenForesia.Lanceur.Hote;

internal sealed class DiffusionSon : IDisposable
{
    private readonly uint _processus;
    private readonly Serveur _serveur;
    private readonly Thread _fil;
    private volatile bool _arret;

    internal DiffusionSon(uint processus, Serveur serveur)
    {
        _processus = processus;
        _serveur = serveur;
        _fil = new Thread(Tourner) { IsBackground = true, Name = "Son" };
        _fil.Start();
    }

    private void Tourner()
    {
        Com.InitialiserFil();
        CaptureSonProcessus capture;
        try { capture = new CaptureSonProcessus(_processus); }
        catch (Exception e)
        {
            Console.Out.WriteLine($"[HÔTE] son du jeu impossible à capturer ({e.Message}) : les invités n'entendront rien. Windows 10 version 2004 ou plus récent est nécessaire.");
            return;
        }
        Console.Out.WriteLine("[HÔTE] capture du son du jeu");

        using (capture)
        {
            var pcm = new List<byte>(Protocole.OctetsSon * 8);
            var morceau = new byte[Protocole.OctetsSon];
            while (!_arret)
            {
                capture.Lire(pcm);
                int envoye = 0;
                while (pcm.Count - envoye >= Protocole.OctetsSon)
                {
                    pcm.CopyTo(envoye, morceau, 0, Protocole.OctetsSon);
                    _serveur.DiffuserSon(morceau);
                    envoye += Protocole.OctetsSon;
                }
                pcm.RemoveRange(0, envoye);
                Thread.Sleep(2);
            }
        }
    }

    public void Dispose()
    {
        _arret = true;
        _fil.Join(2000);
    }
}
