// =====================================================================
// La lecture d'une cinématique, de bout en bout — notre échafaudage.
//
// L'original confiait tout à DirectShow : conteneur, codec, cadence, son, et la présentation dans
// une surface DirectDraw. Tout est repris ici — `AviReader` pour le conteneur, `Indeo4Decoder`
// (`Host/Indeo4/`, le décodeur de FFmpeg porté en C#) pour l'image et `Yvu9Rgb` pour sa couleur,
// `MsAdpcm` et `WaveOut` pour le son — sauf la présentation, qui passe par la surface GDI de
// démarrage comme le reste de l'écran-titre, la chaîne Direct3D n'existant pas encore.
//
// Aucun binaire de 1998 n'intervient plus : jusqu'au 2026-09-24, l'image passait par
// `ir41_32.ax`, le codec d'Intel tiré du CD. Le décodeur C# lui est identique à l'octet près sur
// les quatre films (`OpenForesia.Tools indeo`).
//
// **L'horloge est le chronomètre du programme.** La position de lecture du périphérique audio est
// plus juste sur le papier — elle ne dérive pas — mais elle se **fige** quand la sortie se vide, et
// une horloge figée gèle tout le lecteur au lieu de le laisser rattraper. Voir `Step`.
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
    private static Indeo4Decoder? s_Decoder;
    private static Yvu9Image? s_Image;
    private static byte[] s_Compressed = new byte[64 * 1024];
    private static byte[] s_Frame = [];
    private static int s_Stride;
    private static readonly byte[] s_BitmapInfo = new byte[40];
    private static Stopwatch? s_Clock;
    private static long s_FrameIndex;
    private static IntPtr s_Window;
    private static bool s_Aborted;

    private static readonly Queue<byte[]> s_VideoQueue = new();

    private static WaveOut? s_Sound;
    private static short[] s_Samples = [];

    // Les lots décodés que la sortie n'a pas encore acceptés, dans l'ordre. **Une file, et non un
    // seul tampon** : la première version gardait le lot en attente dans `s_Samples`, que le
    // décodage du lot suivant écrasait aussitôt. Le son sautait alors des morceaux — ce qui
    // s'entend comme un son brouillon et trop rapide, pas comme un silence.
    private static readonly Queue<short[]> s_PendingSound = new();

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
    /// Ouvre un film et affiche sa première image. Rend faux si le fichier est absent ou si son
    /// codec n'est pas celui qu'on sait décoder — deux cas que la séquence d'introduction de
    /// l'original traite déjà comme « ce film ne se joue pas ».
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

        s_Reader = reader;
        s_Decoder = new Indeo4Decoder(reader.Width, reader.Height);
        s_Image = new Yvu9Image(reader.Width, reader.Height);
        s_Window = window;
        s_FrameIndex = 0;
        // Les lignes d'une DIB sont alignées sur quatre octets.
        s_Stride = (reader.Width * 3 + 3) & ~3;
        s_Frame = new byte[s_Stride * reader.Height];

        // Le `BITMAPINFOHEADER` que `StretchDIBits` relit à chaque image. Hauteur positive : la
        // DIB que `Yvu9Rgb` écrit est rangée du bas vers le haut, ce que GDI attend par défaut.
        fixed (byte* info = s_BitmapInfo)
        {
            *(uint*)(info + 0) = 40;
            *(int*)(info + 4) = reader.Width;
            *(int*)(info + 8) = reader.Height;
            *(ushort*)(info + 12) = 1;
            *(ushort*)(info + 14) = 24;
            *(uint*)(info + 16) = 0;   // BI_RGB
            *(uint*)(info + 20) = (uint)(s_Stride * reader.Height);
        }

        // Le son. MS-ADPCM seulement : c'est ce que portent les quatre films du jeu, et écrire un
        // décodeur pour un format qu'on n'a pas sous la main n'aurait aucun moyen d'être vérifié.
        string soundNote = "sans son";
        if (reader.AudioFormatTag == 2 && reader.AudioChannels > 0 && reader.AudioCoefficients.GetLength(0) > 0)
        {
            s_Sound = WaveOut.Open(reader.AudioSampleRate, reader.AudioChannels);
            if (s_Sound != null)
            {
                // Une taille de départ seulement : QueueSound l’agrandit selon le bloc réel.
                s_Samples = new short[Math.Max(8192, reader.AudioSamplesPerBlock * reader.AudioChannels * 4)];
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
        if (s_Reader == null || s_Decoder == null || s_Image == null || s_Clock == null) return false;
        if (s_Aborted) { Stop(); return false; }

        // **L'horloge est le chronomètre, pas la position du périphérique audio.** La première
        // version prenait la seconde, au motif qu'elle ne dérive pas — mais elle se **fige** quand
        // la sortie se vide, et une fois figée l'image ne progresse plus, donc plus aucune image
        // n'est consommée, donc la réserve d'images reste pleine, donc plus rien n'est lu, donc le
        // son ne repart jamais. Un simple hoquet devenait un blocage définitif : image gelée, son
        // muet, au bout de trois secondes. Le chronomètre, lui, avance quoi qu'il arrive.
        //
        // Ce qu'on perd est négligeable : un quartz et une carte son s'écartent de l'ordre de
        // 0,01 %, soit une vingtaine de millisecondes sur les trois minutes d'`OPENING.AVI`.
        long elapsed = s_Clock.ElapsedMilliseconds * 1000;

        long dueFrame = s_Reader.MicroSecPerFrame > 0 ? elapsed / s_Reader.MicroSecPerFrame : s_FrameIndex;

        bool decoded = false;
        while (s_FrameIndex <= dueFrame)
        {
            byte[]? frame = TakeVideoFrame();
            if (frame == null) { Stop(); return false; }

            // Le décodeur tient lui-même ses images de référence : chaque image dit dans son
            // en-tête si elle est clé, il n'y a rien à lui annoncer.
            try
            {
                s_Decoder.Decode(frame, s_Image);
            }
            catch (InvalidDataException e)
            {
                Console.Error.WriteLine($"[FILM] image {s_FrameIndex} : {e.Message}");
                Stop();
                return false;
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
            // Un lot qui ne passe pas veut dire que la sortie est pleine : le son a assez d'avance.
            if (!DrainPendingSound()) return;

            // Sans piste audio, rien ne freinerait cette boucle : la réserve d'images le fait.
            if (s_Sound == null && s_VideoQueue.Count > 0) return;

            AviReader.ChunkKind kind = s_Reader.NextChunk(ref s_Compressed, out int length);
            if (kind == AviReader.ChunkKind.End) return;

            if (kind == AviReader.ChunkKind.Video) s_VideoQueue.Enqueue(s_Compressed[..length]);
            else QueueSound(length);
        }

        // La réserve d'images est pleine, mais le son, lui, ne doit jamais cesser d'être servi :
        // c'est ce couplage qui bloquait tout.
        DrainPendingSound();
    }

    /// <summary>
    /// Pousse vers la sortie autant de lots en attente qu'elle en accepte. Rend faux dès qu'elle
    /// refuse — il reste alors du son en attente, et il ne faut surtout pas en décoder d'autre.
    /// </summary>
    private static bool DrainPendingSound()
    {
        while (s_PendingSound.Count > 0)
        {
            if (s_Sound == null) { s_PendingSound.Clear(); return true; }
            short[] batch = s_PendingSound.Peek();
            if (!s_Sound.Enqueue(batch, batch.Length)) return false;
            s_PendingSound.Dequeue();
        }
        return true;
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

        // **Le tampon se dimensionne sur le bloc, pas sur une taille supposée.** Un bloc AVI porte
        // autant de blocs ADPCM que le monteur a voulu : `ANI01.AVI` en met un par image, mais
        // `TEPS.AVI` en met une seconde entière — 21 668 échantillons. Un tampon fixe de 8 192 en
        // perdait alors les deux tiers, **sans un mot** : le son sautait en avant en permanence,
        // ce qui s'entend comme un son brouillon et trop rapide, et vidait la sortie.
        int blocks = (length + blockAlign - 1) / blockAlign;
        int capacity = blocks * Math.Max(1, s_Reader.AudioSamplesPerBlock) * s_Reader.AudioChannels + 16;
        if (s_Samples.Length < capacity) s_Samples = new short[capacity];

        int written = 0;
        for (int offset = 0; offset + 1 < length; offset += blockAlign)
        {
            int size = Math.Min(blockAlign, length - offset);
            int produced = MsAdpcm.DecodeBlock(
                s_Compressed.AsSpan(offset, size), s_Reader.AudioChannels,
                s_Reader.AudioCoefficients, s_Samples, written);
            written += produced;

            // Le décodeur s'arrête net quand la place manque. Le silence sur cette troncature est
            // ce qui a rendu le défaut précédent si long à voir : maintenant il parle.
            if (produced == 0 && size >= 7 * s_Reader.AudioChannels)
            {
                Console.Error.WriteLine(
                    $"[FILM] bloc audio tronqué : {written} échantillons écrits sur {capacity} de place");
                break;
            }
        }

        if (written <= 0) return;

        // Le lot part dans la file d'attente, jamais directement : `s_Samples` sert au décodage du
        // lot suivant, et le garder comme tampon d'attente le ferait écraser.
        s_PendingSound.Enqueue(s_Samples[..written]);
        DrainPendingSound();
    }

    private static void Present()
    {
        if (s_Reader == null || s_Image == null) return;

        IntPtr dc = StartupSurface.EnsureDc(PresentWidth, PresentHeight);
        if (dc == IntPtr.Zero) return;

        // La conversion de couleur n'est faite que pour l'image affichée : celles qu'on décode
        // pour rattraper un retard n'en ont pas besoin.
        Yvu9Rgb.ToBottomUpBgr(s_Image, s_Frame, s_Stride);

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
        // L'écart entre les deux horloges à la fin de la projection. C'est la mesure qui dit si le
        // choix du chronomètre tient : un écart de quelques dizaines de millisecondes est la dérive
        // attendue entre un quartz et une carte son, un écart qui se compte en secondes voudrait
        // dire que la sortie s'est vidée en route.
        if (s_Sound != null && s_Clock != null && s_FrameIndex > 0)
        {
            long played = s_Sound.PositionMicroseconds();
            if (played > 0)
            {
                long drift = s_Clock.ElapsedMilliseconds * 1000 - played;
                Console.Error.WriteLine(
                    $"[FILM] {s_FrameIndex} images, son joué {played / 1000} ms, " +
                    $"écart horloge {drift / 1000} ms");
            }
        }

        s_Sound?.Dispose();
        s_Sound = null;
        s_Decoder = null;
        s_Image = null;
        s_Reader?.Dispose();
        s_Reader = null;
        s_Clock = null;
        s_FrameIndex = 0;
        s_PendingSound.Clear();
        s_VideoQueue.Clear();
    }
}
