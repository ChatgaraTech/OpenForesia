// =====================================================================
// La lecture d'une cinématique, de bout en bout — notre échafaudage.
//
// L'original confiait tout à DirectShow : conteneur, codec, cadence, et la présentation dans une
// surface DirectDraw. Les trois premiers sont repris ici (`AviReader`, `IndeoCodec`, l'horloge) ;
// la présentation passe par la surface GDI de démarrage, comme le reste de l'écran-titre, puisque
// la chaîne Direct3D n'existe pas encore à ce moment-là.
//
// Le cadrage est celui de l'original : les films font 256x224 et sont étirés en 640x480.
// =====================================================================

using System.Diagnostics;

namespace OpenForesia.Blaze;

internal static unsafe class MoviePlayback
{
    // Les dimensions vers lesquelles l'original étire le film — `StretchDIBits(…, 0x280, 0x1e0, …)`.
    private const int PresentWidth = 0x280;   // 640
    private const int PresentHeight = 0x1e0;  // 480

    private static AviReader? s_Reader;
    private static IndeoCodec.Stream? s_Stream;
    private static byte[] s_Compressed = new byte[64 * 1024];
    private static byte[] s_Frame = [];
    private static byte[] s_BitmapInfo = new byte[40];
    private static Stopwatch? s_Clock;
    private static long s_FrameIndex;
    private static IntPtr s_Window;

    private static bool s_Aborted;

    /// <summary>Vrai tant qu'un film est ouvert et n'a pas atteint sa fin.</summary>
    internal static bool Active => s_Reader != null;

    /// <summary>
    /// Vrai si le dernier film s'est arrêté sur une touche plutôt qu'en arrivant au bout. C'est
    /// ce que la séquence d'introduction de l'original distingue : un film qui rend zéro laisse
    /// continuer la suite, un film interrompu la fait sauter.
    /// </summary>
    internal static bool WasAborted => s_Aborted;

    /// <summary>
    /// Interrompt le film en cours. Appelé depuis la procédure de fenêtre du <b>jeu</b>, pas de
    /// celle du lecteur : en démarrage fenêtré c'est notre fenêtre qui a le focus, donc c'est elle
    /// qui reçoit les touches. Même déviation, et pour la même raison, que celle déjà documentée
    /// pour la boucle d'attract.
    /// </summary>
    internal static void Abort()
    {
        if (s_Reader != null) s_Aborted = true;
    }

    /// <summary>
    /// Ouvre un film et affiche sa première image. Rend faux si le fichier est absent, si son codec
    /// n'est pas celui qu'on sait décoder, ou si le décodeur n'est pas là — trois cas que la
    /// séquence d'introduction de l'original traite déjà comme « ce film ne se joue pas ».
    /// </summary>
    internal static bool Start(string path, IntPtr window)
    {
        Stop();
        s_Aborted = false;

        AviReader? reader = AviReader.Open(path);
        if (reader == null)
        {
            Console.Error.WriteLine($"[FILM] {path} : introuvable ou illisible");
            return false;
        }

        if (reader.VideoCompression != "IV41")
        {
            Console.Error.WriteLine($"[FILM] {path} : compression {reader.VideoCompression} non gérée");
            reader.Dispose();
            return false;
        }

        IndeoCodec.Stream? stream = IndeoCodec.OpenStream(reader.Width, reader.Height);
        if (stream == null)
        {
            reader.Dispose();
            return false;
        }

        s_Reader = reader;
        s_Stream = stream;
        s_Window = window;
        s_FrameIndex = 0;
        s_Frame = new byte[stream.Stride * reader.Height];

        // Le `BITMAPINFOHEADER` que `StretchDIBits` relit à chaque image. Hauteur positive : la
        // DIB que le codec écrit est rangée du bas vers le haut, ce que GDI attend par défaut.
        fixed (byte* info = s_BitmapInfo)
        {
            *(uint*)(info + 0) = 40;
            *(int*)(info + 4) = reader.Width;
            *(int*)(info + 8) = reader.Height;
            *(ushort*)(info + 12) = 1;
            *(ushort*)(info + 14) = 24;
            *(uint*)(info + 16) = 0;   // BI_RGB
            *(uint*)(info + 20) = (uint)(stream.Stride * reader.Height);
        }

        Console.Error.WriteLine(
            $"[FILM] {Path.GetFileName(path)} : {reader.Width}x{reader.Height}, " +
            $"{1_000_000.0 / Math.Max(1, reader.MicroSecPerFrame):F2} im/s, {reader.TotalFrames} images" +
            (reader.AudioFormatTag != 0
                ? $", son format 0x{reader.AudioFormatTag:x4} {reader.AudioSampleRate} Hz (non joué)"
                : ""));

        s_Clock = Stopwatch.StartNew();
        return Step();
    }

    /// <summary>
    /// Avance le film jusqu'à l'image due par l'horloge et l'affiche. Rend faux quand le film est
    /// fini ou qu'aucun film n'est ouvert — c'est le signal d'arrêt de la boucle de lecture.
    ///
    /// <para>
    /// Le décodage est **séquentiel et obligatoire** : une image qui n'est pas une image clé se
    /// compose sur la précédente, donc sauter une image en retard n'est pas une option — on la
    /// décode sans l'afficher. C'est le comportement d'un lecteur qui rattrape son retard.
    /// </para>
    /// </summary>
    internal static bool Step()
    {
        if (s_Reader == null || s_Stream == null || s_Clock == null) return false;
        if (s_Aborted) { Stop(); return false; }

        long dueFrame = s_Reader.MicroSecPerFrame > 0
            ? s_Clock.ElapsedMilliseconds * 1000 / s_Reader.MicroSecPerFrame
            : s_FrameIndex;

        bool decoded = false;
        while (s_FrameIndex <= dueFrame)
        {
            int length = s_Reader.NextVideoChunk(ref s_Compressed);
            if (length < 0)
            {
                Stop();
                return false;
            }

            fixed (byte* source = s_Compressed)
            fixed (byte* destination = s_Frame)
            {
                // Seule la première image est annoncée comme image clé. Marquer à tort une image
                // clé « pas clé » est sans effet — elle ne référence rien — alors que l'inverse
                // ferait décoder une différence sur un fond inexistant.
                if (!IndeoCodec.Decode(s_Stream, source, length, s_FrameIndex == 0, destination))
                {
                    Stop();
                    return false;
                }
            }

            s_FrameIndex++;
            decoded = true;
        }

        if (decoded) Present();
        return true;
    }

    private static void Present()
    {
        if (s_Reader == null || s_Stream == null) return;

        IntPtr dc = StartupSurface.EnsureDc(PresentWidth, PresentHeight);
        if (dc == IntPtr.Zero) return;

        fixed (byte* bits = s_Frame)
        fixed (byte* info = s_BitmapInfo)
        {
            Win32.StretchDIBits(dc, 0, 0, PresentWidth, PresentHeight,
                                0, 0, s_Reader.Width, s_Reader.Height,
                                bits, info, 0 /* DIB_RGB_COLORS */, 0x00CC0020 /* SRCCOPY */);
        }
        StartupSurface.MarkDrawn();

        if (s_Window != IntPtr.Zero)
        {
            (int width, int height) = D3D11Window.ClientSize();
            if (width > 0 && height > 0) StartupSurface.PresentTo(s_Window, width, height);
        }
    }

    /// <summary>Ferme le film en cours, s'il y en a un. Sans effet sinon.</summary>
    internal static void Stop()
    {
        s_Stream?.Dispose();
        s_Stream = null;
        s_Reader?.Dispose();
        s_Reader = null;
        s_Clock = null;
        s_FrameIndex = 0;
    }
}
