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
                if (!ReadExact(buffer, 16)) return false;
                AudioFormatTag = BitConverter.ToUInt16(buffer, 0);
                AudioChannels = BitConverter.ToUInt16(buffer, 2);
                AudioSampleRate = BitConverter.ToInt32(buffer, 4);
                AudioBitsPerSample = BitConverter.ToUInt16(buffer, 14);
            }

            _file.Position = body + size + (size & 1);
        }
        return true;
    }

    /// <summary>
    /// Le prochain bloc **vidéo** de la liste <c>movi</c>, recopié dans <paramref name="buffer"/>,
    /// qui est agrandi si besoin. Rend la longueur utile, ou <c>-1</c> à la fin du film.
    ///
    /// <para>
    /// Le nom d'un bloc est <c>NNcc</c> : les deux chiffres donnent le numéro de flux, les deux
    /// lettres le genre — <c>dc</c> image compressée, <c>db</c> image brute, <c>wb</c> son. Les
    /// blocs audio sont sautés ici ; le son n'est pas encore joué (voir MoviePlayback).
    /// </para>
    /// </summary>
    internal int NextVideoChunk(ref byte[] buffer)
    {
        while (_file.Position + 8 <= _moviEnd)
        {
            if (!ReadExact(_header, 8)) return -1;
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

            if ((id.EndsWith("dc", StringComparison.Ordinal) || id.EndsWith("db", StringComparison.Ordinal))
                && body + size <= _moviEnd)
            {
                if (buffer.Length < size) buffer = new byte[Math.Max(size, buffer.Length * 2)];
                if (!ReadExact(buffer, (int)size)) return -1;
                _file.Position = body + size + (size & 1);
                return (int)size;
            }

            _file.Position = body + size + (size & 1);
        }
        return -1;
    }

    public void Dispose() => _file.Dispose();
}
