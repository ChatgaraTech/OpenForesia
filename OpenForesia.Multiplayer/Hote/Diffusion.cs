// La diffusion de l'image du jeu (taches 2.1 a 2.3) : un fil a part, qui capture la fenetre du jeu,
// l'encode une seule fois et remet chaque image au serveur, qui l'envoie a tous les invites.
//
// La fenetre du jeu change au demarrage (une surface d'accueil, puis la fenetre D3D11) : toutes les
// deux secondes, on regarde si la plus grande fenetre du jeu est toujours celle qu'on capture. Rien
// n'est encode tant qu'aucun invite n'est la. Au plus 60 images par seconde, meme si le jeu en
// presente davantage (un ecran a 120 Hz).

using System.Diagnostics;
using OpenForesia.Multiplayer.Natif;
using OpenForesia.Multiplayer.Video;

namespace OpenForesia.Multiplayer.Hote;

internal sealed unsafe class Diffusion : IDisposable
{
    internal const int ImagesParSeconde = 60;
    private const long IntervalleMin = 1000 / ImagesParSeconde - 1, VerificationFenetre = 2000;

    private readonly Func<IntPtr> _fenetre;                  // la fenêtre à capturer (celle du jeu ; la mire, à l'essai)
    private readonly Serveur _serveur;
    private readonly Commun.Qualite _qualite;
    private readonly Thread _fil;
    private volatile bool _arret;

    internal Diffusion(Func<IntPtr> fenetre, Serveur serveur, Commun.Qualite qualite)
    {
        _fenetre = fenetre;
        _serveur = serveur;
        _qualite = qualite;
        _fil = new Thread(Tourner) { IsBackground = true, Name = "Diffusion" };
        _fil.Start();
    }

    private void Tourner()
    {
        Com.InitialiserFil();
        CaptureFenetre? capture = null;
        EncodeurH264? encodeur = null;
        var montre = Stopwatch.StartNew();
        long derniereImage = -IntervalleMin, derniereVerification = -VerificationFenetre;
        long dernierReleve = 0, octets = 0, encodage = 0;
        int images = 0;

        try
        {
            encodeur = new EncodeurH264(_qualite.Largeur, _qualite.Hauteur, ImagesParSeconde, _qualite.Debit);
            int largeur = _qualite.Largeur, hauteur = _qualite.Hauteur;
            while (!_arret)
            {
                long t = montre.ElapsedMilliseconds;

                if (t - derniereVerification >= VerificationFenetre || capture is { Vivante: false })
                {
                    derniereVerification = t;
                    IntPtr fenetre = _fenetre();
                    if (fenetre != IntPtr.Zero && Win32.IsWindow(fenetre) && (capture == null || capture.Fenetre != fenetre || !capture.Vivante))
                    {
                        capture?.Dispose();
                        capture = null;
                        try
                        {
                            capture = new CaptureFenetre(fenetre);
                            _serveur.DemanderCle();
                            Journal($"capture de la fenêtre 0x{fenetre:X} du jeu");
                        }
                        catch (Exception e) { Journal($"capture impossible ({e.Message}), nouvel essai dans 2 s"); }
                    }
                }

                if (capture == null || !_serveur.AUnInvite || t - derniereImage < IntervalleMin)
                {
                    Thread.Sleep(1);
                    continue;
                }

                byte[]? h264 = null;
                bool cle = false;
                bool forcer = _serveur.PrendreDemandeCle();
                EncodeurH264 enc = encodeur;
                bool lue = capture.Lire((pixels, pas, l, h) =>
                    h264 = enc.Encoder(nv12 => Conversion.BgraVersNv12((byte*)pixels, pas, l, h, (byte*)nv12, largeur, hauteur),
                                       forcer, out cle));
                if (!lue)
                {
                    if (forcer) _serveur.DemanderCle();              // la demande attendra l'image suivante
                    Thread.Sleep(1);
                    continue;
                }
                derniereImage = t;
                if (h264 != null) _serveur.DiffuserImage(h264, cle);

                // Toutes les 5 s, au journal : ce que l'hôte produit vraiment.
                images++;
                octets += h264?.Length ?? 0;
                encodage += montre.ElapsedMilliseconds - t;
                if (t - dernierReleve >= 5000)
                {
                    double s = (t - dernierReleve) / 1000.0;
                    Journal($"diffusion : {images / s:0} images/s, {encodage / (double)Math.Max(1, images):0.0} ms par image (capture et encodage), {octets * 8 / s / 1e6:0.00} Mbit/s par invité");
                    dernierReleve = t;
                    images = 0;
                    octets = 0;
                    encodage = 0;
                }
            }
        }
        catch (Exception e)
        {
            Journal($"diffusion arrêtée : {e.GetType().Name} : {e.Message}");
        }
        finally
        {
            capture?.Dispose();
            encodeur?.Dispose();
        }
    }

    private static void Journal(string message) => Console.Out.WriteLine($"[HÔTE] {message}");

    public void Dispose()
    {
        _arret = true;
        _fil.Join(2000);
    }
}
