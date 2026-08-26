// =====================================================================
// Lecteur de conteneur AVI — notre échafaudage, pas une transcription.
//
// L'original ne démultiplexe rien lui-même : il passe le chemin du fichier à DirectShow, qui
// s'occupe du conteneur comme du codec. DirectShow ne sait plus décoder l'Indeo 4 des films du CD
// (voir Documentation/Reference/cinematiques.md), donc on reprend les deux moitiés : le conteneur
// ici, le codec par `Platform/IndeoCodec.cs`.
//
// **Le fichier est lu en flux, jamais chargé entier.** `opening.avi` pèse 463 Mo sur le CD alors
// que son `RIFF` n'en déclare que 28 : le reste est du bourrage de pressage. Le charger en mémoire
// coûterait un demi-gigaoctet pour vingt-huit mégaoctets utiles, et sur le chemin de démarrage,
// où la mémoire basse est déjà disputée, c'est exactement ce qu'il ne faut pas faire.
// =====================================================================

using System.Text;

namespace OpenForesia.Blaze;

/// <summary>
/// Un fichier AVI ouvert en lecture séquentielle : les en-têtes de flux, puis les blocs de la
/// liste <c>movi</c> rendus un par un.
/// </summary>
internal sealed class AviReader : IDisposable
{
    private readonly FileStream _file;
    private readonly byte[] _header = new byte[8];
    private long _moviPos, _moviEnd;

    /// <summary>Dimensions de l'image, telles que le <c>BITMAPINFOHEADER</c> les annonce.</summary>
    internal int Width { get; private set; }
    internal int Height { get; private set; }

    /// <summary>Le code de compression du flux vidéo — <c>IV41</c> pour les films du jeu.</summary>
    internal string VideoCompression { get; private set; } = "";

    /// <summary>Durée d'une image en microsecondes. 66 667 (15 im/s) pour les films du jeu.</summary>
    internal int MicroSecPerFrame { get; private set; }

    internal int TotalFrames { get; private set; }

    /// <summary>Format du flux audio, s'il y en a un. <c>2</c> = MS-ADPCM.</summary>
    internal int AudioFormatTag { get; private set; }
    internal int AudioChannels { get; private set; }
    internal int AudioSampleRate { get; private set; }
    internal int AudioBitsPerSample { get; private set; }

    /// <summary>Taille d'un bloc ADPCM, en octets. 512 pour les films du jeu.</summary>
    internal int AudioBlockAlign { get; private set; }

    /// <summary>Échantillons rendus par bloc. 1012 pour les films du jeu.</summary>
    internal int AudioSamplesPerBlock { get; private set; }

    /// <summary>
    /// Les couples de coefficients du prédicteur MS-ADPCM, **lus dans le fichier** et non
    /// codés en dur : le format les transporte précisément pour qu'un encodeur puisse en choisir
    /// d'autres. Ceux des films du jeu sont les sept couples habituels.
    /// </summary>
    internal short[,] AudioCoefficients { get; private set; } = new short[0, 2];

    private AviReader(FileStream file) => _file = file;

    /// <summary>
    /// Ouvre un AVI. Rend <c>null</c> si le fichier n'existe pas ou n'est pas un RIFF/AVI — un film
    /// absent n'est pas une erreur : le CD peut ne pas être là, et la séquence d'introduction de
    /// l'original traite déjà l'échec d'un film comme un cas normal.
    /// </summary>
    internal static AviReader? Open(string path)
    {
        FileStream? file = null;
        try
        {
            file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 64 * 1024);
            var reader = new AviReader(file);
            return reader.ReadHeaders() ? reader : null;
        }
        catch (IOException) { file?.Dispose(); return null; }
        catch (UnauthorizedAccessException) { file?.Dispose(); return null; }
    }

    private string Fourcc(byte[] b, int o) => Encoding.ASCII.GetString(b, o, 4);

    private bool ReadExact(byte[] buffer, int count)
    {
        int done = 0;
        while (done < count)
        {
            int n = _file.Read(buffer, done, count - done);
            if (n <= 0) return false;
            done += n;
        }
        return true;
    }

    private bool ReadHeaders()
    {
        var head = new byte[12];
        if (!ReadExact(head, 12)) return false;
        if (Fourcc(head, 0) != "RIFF" || Fourcc(head, 8) != "AVI ") return false;

        // La taille déclarée par le RIFF fait autorité, pas celle du fichier. Voir l'en-tête.
        long riffEnd = Math.Min(8L + (uint)BitConverter.ToInt32(head, 4), _file.Length);

        while (_file.Position + 8 <= riffEnd)
        {
            if (!ReadExact(_header, 8)) return false;
            string id = Fourcc(_header, 0);
            long size = (uint)BitConverter.ToInt32(_header, 4);
            long body = _file.Position;

            if (id == "LIST")
            {
                var type = new byte[4];
                if (!ReadExact(type, 4)) return false;
                string listType = Fourcc(type, 0);

                if (listType == "hdrl")
                {
                    if (!ReadHeaderList(body + 4, body + size)) return false;
                }
                else if (listType == "movi")
                {
                    // On s'arrête ici : la lecture des blocs reprendra à cette position.
                    _moviPos = body + 4;
                    _moviEnd = body + size;
                    _file.Position = _moviPos;
                    return Width > 0 && Height > 0;
                }
            }

            _file.Position = body + size + (size & 1);
        }
        return false;
    }

    private bool ReadHeaderList(long start, long end)
    {
        _file.Position = start;
        var buffer = new byte[256];

        while (_file.Position + 8 <= end)
        {
            if (!ReadExact(_header, 8)) return false;
            string id = Fourcc(_header, 0);
            long size = (uint)BitConverter.ToInt32(_header, 4);
            long body = _file.Position;

            if (id == "avih" && size >= 32)
            {
                if (!ReadExact(buffer, 32)) return false;
                MicroSecPerFrame = BitConverter.ToInt32(buffer, 0);
                TotalFrames = BitConverter.ToInt32(buffer, 16);
            }
            else if (id == "LIST")
            {
                var type = new byte[4];
                if (!ReadExact(type, 4)) return false;
                if (Fourcc(type, 0) == "strl" && !ReadStream(body + 4, body + size)) return false;
            }

            _file.Position = body + size + (size & 1);
        }
        return true;
    }

    private bool ReadStream(long start, long end)
    {
        _file.Position = start;
        var buffer = new byte[256];
        string kind = "";

        while (_file.Position + 8 <= end)
        {
            if (!ReadExact(_header, 8)) return false;
            string id = Fourcc(_header, 0);
            long size = (uint)BitConverter.ToInt32(_header, 4);
            long body = _file.Position;

            if (id == "strh" && size >= 8)
            {
                if (!ReadExact(buffer, 8)) return false;
                kind = Fourcc(buffer, 0);
            }
            else if (id == "strf" && size >= 40 && kind == "vids")
            {
                if (!ReadExact(buffer, 40)) return false;
                Width = BitConverter.ToInt32(buffer, 4);
                Height = BitConverter.ToInt32(buffer, 8);
                VideoCompression = Fourcc(buffer, 16);
            }
            else if (id == "strf" && size >= 16 && kind == "auds")
            {
                int wanted = (int)Math.Min(size, buffer.Length);
                if (!ReadExact(buffer, wanted)) return false;
                AudioFormatTag = BitConverter.ToUInt16(buffer, 0);
                AudioChannels = BitConverter.ToUInt16(buffer, 2);
                AudioSampleRate = BitConverter.ToInt32(buffer, 4);
                AudioBlockAlign = BitConverter.ToUInt16(buffer, 12);
                AudioBitsPerSample = BitConverter.ToUInt16(buffer, 14);

                // La queue propre a MS-ADPCM : le nombre d'echantillons par bloc, puis le nombre de
                // couples de coefficients, puis les couples eux-memes.
                if (AudioFormatTag == 2 && wanted >= 22)
                {
                    int extra = BitConverter.ToUInt16(buffer, 16);
                    if (extra >= 4 && wanted >= 18 + extra)
                    {
                        AudioSamplesPerBlock = BitConverter.ToUInt16(buffer, 18);
                        int pairs = BitConverter.ToUInt16(buffer, 20);
                        if (pairs > 0 && wanted >= 22 + pairs * 4)
                        {
                            var coefficients = new short[pairs, 2];
                            for (int i = 0; i < pairs; i++)
                            {
                                coefficients[i, 0] = BitConverter.ToInt16(buffer, 22 + i * 4);
                                coefficients[i, 1] = BitConverter.ToInt16(buffer, 24 + i * 4);
                            }
                            AudioCoefficients = coefficients;
                        }
                    }
                }
            }

            _file.Position = body + size + (size & 1);
        }
        return true;
    }

    internal enum ChunkKind { End, Video, Audio }

    /// <summary>
    /// Le prochain bloc de la liste <c>movi</c>, recopié dans <paramref name="buffer"/>, qui est
    /// agrandi si besoin. Rend sa longueur et son genre, ou <c>End</c> à la fin du film.
    ///
    /// <para>
    /// Le nom d'un bloc est <c>NNcc</c> : les deux chiffres donnent le numéro de flux, les deux
    /// lettres le genre — <c>dc</c> image compressée, <c>db</c> image brute, <c>wb</c> son. Les
    /// deux genres sortent **entrelacés dans l'ordre du fichier**, qui est celui du montage : le
    /// son y précède légèrement l'image qu'il accompagne, ce qui donne l'avance dont la lecture a
    /// besoin pour ne pas manquer de son.
    /// </para>
    /// </summary>
    internal ChunkKind NextChunk(ref byte[] buffer, out int length)
    {
        length = 0;
        while (_file.Position + 8 <= _moviEnd)
        {
            if (!ReadExact(_header, 8)) return ChunkKind.End;
            string id = Fourcc(_header, 0);
            long size = (uint)BitConverter.ToInt32(_header, 4);
            long body = _file.Position;

            // Une liste `rec ` regroupe les blocs d'une même image : on descend dedans plutôt que
            // de la sauter, sinon toutes les images disparaissent d'un coup.
            if (id == "LIST")
            {
                _file.Position = body + 4;
                continue;
            }

            ChunkKind kind = ChunkKind.End;
            if (id.EndsWith("dc", StringComparison.Ordinal) || id.EndsWith("db", StringComparison.Ordinal))
                kind = ChunkKind.Video;
            else if (id.EndsWith("wb", StringComparison.Ordinal))
                kind = ChunkKind.Audio;

            if (kind != ChunkKind.End && size > 0 && body + size <= _moviEnd)
            {
                if (buffer.Length < size) buffer = new byte[Math.Max(size, buffer.Length * 2)];
                if (!ReadExact(buffer, (int)size)) return ChunkKind.End;
                _file.Position = body + size + (size & 1);
                length = (int)size;
                return kind;
            }

            _file.Position = body + size + (size & 1);
        }
        return ChunkKind.End;
    }

    public void Dispose() => _file.Dispose();
}
