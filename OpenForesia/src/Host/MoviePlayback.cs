// =====================================================================
// La lecture d'une cinématique, de bout en bout — notre échafaudage.
//
// L'original confiait tout à DirectShow : conteneur, codec, cadence, son, et la présentation dans
// une surface DirectDraw. Tout est repris ici — `AviReader` pour le conteneur, `IndeoCodec` pour
// l'image, `MsAdpcm` et `WaveOut` pour le son — sauf la présentation, qui passe par la surface GDI
// de démarrage comme le reste de l'écran-titre, la chaîne Direct3D n'existant pas encore.
//
// **Trois horloges possibles, une seule juste.** Le chronomètre du programme dérive du son ; le
// rythme d'arrivée des blocs dérive de tout ; seule la position de lecture du périphérique audio
// dit où en est réellement le film. C'est elle qui décide quelle image est due.
//
// **On anticipe les données, jamais l'affichage.** Le son a besoin d'avance pour ne pas hoqueter,
// et lire en avance oblige à traverser les blocs vidéo entrelacés — mais ceux-là sont mis de côté
// *compressés*, et décodés seulement quand leur tour vient. Décoder en avance ferait courir l'image
// devant le son, ce que la première version faisait.
// =====================================================================

using System.Diagnostics;

namespace OpenForesia.Blaze;

internal static unsafe class MoviePlayback
{
    // Les dimensions vers lesquelles l'original étire le film — `StretchDIBits(…, 0x280, 0x1e0, …)`.
    private const int PresentWidth = 0x280;   // 640
    private const int PresentHeight = 0x1e0;  // 480

    // Deux secondes d'images compressées en réserve : à 10 Ko l'image, c'est 300 Ko, et cela suffit
    // largement à couvrir l'avance dont le son a besoin.
    private const int VideoQueueLimit = 30;

    private static AviReader? s_Reader;
    private static IndeoCodec.Stream? s_Stream;
    private static byte[] s_Compressed = new byte[64 * 1024];
    private static byte[] s_Frame = [];
    private static readonly byte[] s_BitmapInfo = new byte[40];
    private static Stopwatch? s_Clock;
    private static long s_FrameIndex;
    private static IntPtr s_Window;
    private static bool s_Aborted;

    private static readonly Queue<byte[]> s_VideoQueue = new();

    private static WaveOut? s_Sound;
    private static short[] s_Samples = [];
    private static int s_PendingSamples;   // décodés mais pas encore acceptés par la sortie

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

        // Le son. MS-ADPCM seulement : c'est ce que portent les quatre films du jeu, et écrire un
        // décodeur pour un format qu'on n'a pas sous la main n'aurait aucun moyen d'être vérifié.
        string soundNote = "sans son";
        if (reader.AudioFormatTag == 2 && reader.AudioChannels > 0 && reader.AudioCoefficients.GetLength(0) > 0)
        {
            s_Sound = WaveOut.Open(reader.AudioSampleRate, reader.AudioChannels);
            if (s_Sound != null)
            {
                s_Samples = new short[Math.Max(8192, reader.AudioSamplesPerBlock * reader.AudioChannels * 8)];
                soundNote = $"MS-ADPCM {reader.AudioSampleRate} Hz " +
                            $"{(reader.AudioChannels == 1 ? "mono" : "stéréo")}";
            }
        }
        else if (reader.AudioFormatTag != 0)
        {
            soundNote = $"son format 0x{reader.AudioFormatTag:x4} non géré";
        }

        Console.Error.WriteLine(
            $"[FILM] {Path.GetFileName(path)} : {reader.Width}x{reader.Height}, " +
            $"{1_000_000.0 / Math.Max(1, reader.MicroSecPerFrame):F2} im/s, {reader.TotalFrames} images, " +
            soundNote);

        // Amorçage : remplir la sortie audio avant de lâcher l'horloge. Sans ça, l'horloge du son
        // part de zéro alors que rien n'a été écrit, et la première seconde hoquette le temps que
        // l'avance se constitue.
        PumpAhead();

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

        long elapsed = s_Sound?.PositionMicroseconds() ?? 0;
        if (elapsed == 0) elapsed = s_Clock.ElapsedMilliseconds * 1000;

        long dueFrame = s_Reader.MicroSecPerFrame > 0 ? elapsed / s_Reader.MicroSecPerFrame : s_FrameIndex;

        bool decoded = false;
        while (s_FrameIndex <= dueFrame)
        {
            byte[]? frame = TakeVideoFrame();
            if (frame == null) { Stop(); return false; }

            fixed (byte* source = frame)
            fixed (byte* destination = s_Frame)
            {
                // Seule la première image est annoncée comme image clé. Marquer à tort une image
                // clé « pas clé » est sans effet — elle ne référence rien — alors que l'inverse
                // ferait décoder une différence sur un fond inexistant.
                if (!IndeoCodec.Decode(s_Stream, source, frame.Length, s_FrameIndex == 0, destination))
                {
                    Stop();
                    return false;
                }
            }

            s_FrameIndex++;
            decoded = true;
        }

        PumpAhead();
        if (decoded) Present();
        return true;
    }

    /// <summary>
    /// La prochaine image compressée : celle qui attend en réserve, ou la prochaine du fichier. Le
    /// son rencontré en chemin part vers la sortie. Rend <c>null</c> à la fin du film.
    /// </summary>
    private static byte[]? TakeVideoFrame()
    {
        if (s_VideoQueue.Count > 0) return s_VideoQueue.Dequeue();

        while (true)
        {
            AviReader.ChunkKind kind = s_Reader!.NextChunk(ref s_Compressed, out int length);
            if (kind == AviReader.ChunkKind.End) return null;
            if (kind == AviReader.ChunkKind.Video) return s_Compressed[..length];
            QueueSound(length);
        }
    }


    /// <summary>
    /// Lit en avance pour tenir la sortie audio pleine. Les images croisées en chemin sont mises en
    /// réserve **compressées** : les décoder ici ferait courir l'image devant le son.
    /// </summary>
    private static void PumpAhead()
    {
        if (s_Reader == null) return;

        while (s_VideoQueue.Count < VideoQueueLimit)
        {
            // Un reste non accepté veut dire que la sortie est pleine : le son a assez d'avance.
            if (s_PendingSamples > 0)
            {
                if (s_Sound == null || !s_Sound.Enqueue(s_Samples, s_PendingSamples)) return;
                s_PendingSamples = 0;
            }

            // Sans piste audio, rien ne freinerait cette boucle : la réserve d'images le fait.
            if (s_Sound == null && s_VideoQueue.Count > 0) return;

            AviReader.ChunkKind kind = s_Reader.NextChunk(ref s_Compressed, out int length);
            if (kind == AviReader.ChunkKind.End) return;

            if (kind == AviReader.ChunkKind.Video) s_VideoQueue.Enqueue(s_Compressed[..length]);
            else QueueSound(length);
        }
    }

    /// <summary>
    /// Décode un bloc audio et le met en file. Un bloc AVI porte un ou plusieurs blocs ADPCM de
    /// <c>nBlockAlign</c> octets, indépendants les uns des autres.
    /// </summary>
    private static void QueueSound(int length)
    {
        if (s_Sound == null || s_Reader == null) return;

        int blockAlign = s_Reader.AudioBlockAlign;
        if (blockAlign <= 0) return;

        int written = 0;
        for (int offset = 0; offset + 1 < length; offset += blockAlign)
        {
            int size = Math.Min(blockAlign, length - offset);
            written += MsAdpcm.DecodeBlock(
                s_Compressed.AsSpan(offset, size), s_Reader.AudioChannels,
                s_Reader.AudioCoefficients, s_Samples, written);
        }

        if (written <= 0) return;
        s_PendingSamples = s_Sound.Enqueue(s_Samples, written) ? 0 : written;
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
        s_Sound?.Dispose();
        s_Sound = null;
        s_Stream?.Dispose();
        s_Stream = null;
        s_Reader?.Dispose();
        s_Reader = null;
        s_Clock = null;
        s_FrameIndex = 0;
        s_PendingSamples = 0;
        s_VideoQueue.Clear();
    }
}
