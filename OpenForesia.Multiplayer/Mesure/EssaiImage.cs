// L'essai de la chaine d'image en boucle fermee (lot 2), sans le jeu ni le reseau :
//
//   une mire animee (fenetre a nous) ─► capture ─► NV12 ─► H.264 ─► morceaux ─► recollage ─►
//   decodage ─► BGRA ─► la fenetre de l'invite
//
// Il chronometre chaque etape, pendant quelques secondes, et le dit dans la console. Aucune frappe,
// aucun paquet reseau : il peut tourner n'importe quand.

using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenForesia.Multiplayer.Invite;
using OpenForesia.Multiplayer.Natif;
using OpenForesia.Multiplayer.Video;

namespace OpenForesia.Multiplayer.Mesure;

internal static unsafe partial class EssaiImage
{
    [LibraryImport("user32.dll")] private static partial IntPtr GetDC(IntPtr hwnd);
    [LibraryImport("user32.dll")] private static partial int ReleaseDC(IntPtr hwnd, IntPtr hdc);
    [LibraryImport("user32.dll")] private static partial int FillRect(IntPtr hdc, int* r, IntPtr brosse);
    [LibraryImport("gdi32.dll")] private static partial IntPtr CreateSolidBrush(uint couleur);
    [LibraryImport("gdi32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool DeleteObject(IntPtr objet);


    internal static int Executer(int secondes, string? sortie, Commun.Qualite qualite)
    {
        IntPtr mire = CreerMire();
        using var affichage = new FenetreInvite("OpenForesia — essai de l'image (sortie)");
        affichage.Texte("En attente de la première image…");

        using var capture = new CaptureFenetre(mire);
        int Largeur = qualite.Largeur, Hauteur = qualite.Hauteur;
        using var encodeur = new EncodeurH264(Largeur, Hauteur, 60, qualite.Debit);
        using var decodeur = new DecodeurH264();
        var recolleur = new Recolleur();
        var tampon = new byte[Commun.Protocole.TailleMax];
        uint numeroPaquet = 0, numeroImage = 0;
        byte[]? bgra = null;

        var montre = Stopwatch.StartNew();
        var tCapture = new Stopwatch(); var tEncodage = new Stopwatch(); var tDecodage = new Stopwatch();
        int images = 0, decodees = 0, cles = 0;
        long octets = 0, morceaux = 0;
        int image = 0;

        while (montre.Elapsed.TotalSeconds < secondes && affichage.Pomper())
        {
            DessinerMire(mire, image++);

            tCapture.Start();
            byte[]? h264 = null;
            bool cle = false;
            bool lue = capture.Lire((pixels, pas, l, h) =>
            {
                tCapture.Stop();
                tEncodage.Start();
                h264 = encodeur.Encoder(nv12 => Conversion.BgraVersNv12((byte*)pixels, pas, l, h, (byte*)nv12, Largeur, Hauteur),
                                        forcerCle: false, out cle);
                tEncodage.Stop();
            });
            if (!lue) { tCapture.Stop(); Thread.Sleep(2); continue; }
            images++;
            if (h264 == null) continue;
            if (cle) cles++;
            octets += h264.Length;

            byte[]? recollee = null;
            Morceaux.Decouper(h264, ++numeroImage, cle, tampon, () => ++numeroPaquet, paquet =>
            {
                morceaux++;
                if (Commun.Protocole.Lire(paquet.Span, out var p)) recollee = recolleur.Ajouter(p.Contenu) ?? recollee;
            });
            if (recollee == null) continue;

            tDecodage.Start();
            decodeur.Decoder(recollee, (nv12, l, h) =>
            {
                if (bgra == null || bgra.Length != l * h * 4) bgra = new byte[l * h * 4];
                fixed (byte* b = bgra)
                {
                    Conversion.Nv12VersBgra((byte*)nv12, l, h, b);
                    tDecodage.Stop();
                    affichage.Etat($"Essai : image {numeroImage}, {h264.Length / 1024} Ko", alerte: false);
                    affichage.Image(b, l, h);
                    tDecodage.Start();
                }
                decodees++;
            });
            tDecodage.Stop();
            Thread.Sleep(1);
        }

        double s = montre.Elapsed.TotalSeconds;
        Console.Out.WriteLine($"[ESSAI] {s:0.0} s : {images} images capturées ({images / s:0} par seconde), {decodees} décodées, {cles} images clés");
        if (images > 0)
            Console.Out.WriteLine($"[ESSAI] par image : capture {tCapture.Elapsed.TotalMilliseconds / images:0.0} ms, "
                                + $"conversion + encodage {tEncodage.Elapsed.TotalMilliseconds / images:0.0} ms, "
                                + $"décodage + conversion {tDecodage.Elapsed.TotalMilliseconds / Math.Max(1, decodees):0.0} ms");
        Console.Out.WriteLine($"[ESSAI] retard des codecs : encodeur {encodeur.Retard} image(s), décodeur {decodeur.Retard} image(s)");
        Console.Out.WriteLine($"[ESSAI] débit {octets * 8 / s / 1_000_000:0.00} Mbit/s, {morceaux} morceaux, "
                            + $"{(images > 0 ? octets / images : 0)} octets par image en moyenne");
        Win32.DestroyWindow(mire);

        // La dernière image décodée, en BMP, pour la regarder : les chiffres ne disent pas si les
        // couleurs et les proportions sont justes.
        if (sortie != null && bgra != null && decodeur.Largeur > 0)
        {
            EcrireBmp(sortie, bgra, decodeur.Largeur, decodeur.Hauteur);
            Console.Out.WriteLine($"[ESSAI] dernière image décodée : {Path.GetFullPath(sortie)}");
        }
        return decodees > 0 ? 0 : 1;
    }

    internal static void EcrireBmp(string chemin, byte[] bgra, int largeur, int hauteur)
    {
        using var f = new BinaryWriter(File.Create(chemin));
        int taille = largeur * hauteur * 4;
        f.Write((byte)'B'); f.Write((byte)'M'); f.Write(54 + taille); f.Write(0); f.Write(54);
        f.Write(40); f.Write(largeur); f.Write(-hauteur); f.Write((short)1); f.Write((short)32);
        f.Write(0); f.Write(taille); f.Write(2835); f.Write(2835); f.Write(0); f.Write(0);
        f.Write(bgra, 0, taille);
    }

    // ---- la mire ---------------------------------------------------------------------------------

    internal static IntPtr CreerMire()
    {
        const string classe = "OpenForesiaMire";
        fixed (char* nom = classe)
        {
            var wc = new Win32.WNDCLASSEXW
            {
                cbSize = (uint)sizeof(Win32.WNDCLASSEXW),
                lpfnWndProc = &ProcedureMire,
                hInstance = Win32.GetModuleHandle(IntPtr.Zero),
                hCursor = Win32.LoadCursor(IntPtr.Zero, 32512),
                lpszClassName = nom,
            };
            Win32.RegisterClassEx(&wc);
        }
        return Win32.CreateWindowEx(0, classe, "OpenForesia — essai de l'image (source)", Win32.WS_OVERLAPPEDWINDOW | Win32.WS_VISIBLE,
            40, 40, 656, 519, IntPtr.Zero, IntPtr.Zero, Win32.GetModuleHandle(IntPtr.Zero), IntPtr.Zero);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static IntPtr ProcedureMire(IntPtr hwnd, uint msg, IntPtr w, IntPtr l) =>
        msg == 0x0014 ? 1 : Win32.DefWindowProc(hwnd, msg, w, l);          // WM_ERASEBKGND : rien

    /// <summary>Huit bandes de couleur et un carré blanc qui traverse la mire : de quoi voir le mouvement et les couleurs.</summary>
    internal static void DessinerMire(IntPtr hwnd, int image)
    {
        uint[] couleurs = [0xFFFFFF, 0x00FFFF, 0xFFFF00, 0x00FF00, 0xFF00FF, 0x0000FF, 0xFF0000, 0x000000];
        IntPtr hdc = GetDC(hwnd);
        try
        {
            int* r = stackalloc int[4];
            for (int i = 0; i < 8; i++)
            {
                r[0] = i * 80; r[1] = 0; r[2] = i * 80 + 80; r[3] = 480;
                IntPtr b = CreateSolidBrush(couleurs[i]);
                FillRect(hdc, r, b);
                DeleteObject(b);
            }
            int x = image * 4 % 560;
            r[0] = x; r[1] = 200; r[2] = x + 80; r[3] = 280;
            IntPtr blanc = CreateSolidBrush(0x808080);
            FillRect(hdc, r, blanc);
            DeleteObject(blanc);
        }
        finally { ReleaseDC(hwnd, hdc); }
    }
}
