// =====================================================================
// `OpenForesia.Tools indeo <film.avi>` — l'oracle du décodeur Indeo 4 en C#.
//
// Chaque image du film est décodée deux fois : par `ir41_32.ax` (le codec d'Intel, sortie IF09)
// et par `Host/Indeo4/Indeo4Decoder.cs` (le portage de FFmpeg). Les trois plans YVU9 sont comparés
// **à l'octet près**, et le premier écart est désigné : image, genre d'image, plan, position.
//
// Par défaut on s'arrête à la première image qui diffère : les images non clés se composent sur les
// précédentes, donc tout ce qui suit un écart hérite de lui et n'apprend plus rien. `--continuer`
// passe outre.
//
// `--reference-seule` ne touche pas au décodeur C# : il fait tourner DEUX instances du codec d'Intel
// côte à côte et vérifie qu'elles s'accordent. Un oracle doit être déterministe avant qu'on lui
// demande quoi que ce soit.
//
// Le codec est un binaire 32 bits : ce verbe ne marche que parce que l'outil est compilé en x86.
// =====================================================================

using System.IO.Compression;
using OpenForesia.Blaze;

namespace OpenForesia.Tools.Indeo;

internal static class ComparaisonIndeo
{
    internal const string CodecParDefaut = @"Legacy\Binaries\ir41_32.ax";

    // Les noms des genres d'image, tels que `indeo4.c` les numérote (IVI4_FRAMETYPE_*).
    private static readonly string[] s_FrameTypes =
        { "INTRA", "INTRA1", "INTER", "BIDIR", "INTER_NOREF", "NULL_FIRST", "NULL_LAST", "?7" };

    /// <summary>
    /// Le genre d'une image, lu dans son en-tête : 18 bits de synchronisation valant 0x3FFF8, puis
    /// 3 bits de genre, le tout lu **poids faible en tête** (le lecteur de bits d'Indeo 4 est
    /// petit-boutiste). Rend -1 si la synchronisation manque.
    /// </summary>
    private static int FrameType(ReadOnlySpan<byte> frame)
    {
        if (frame.Length < 3) return -1;
        uint bits = (uint)(frame[0] | frame[1] << 8 | frame[2] << 16);
        if ((bits & 0x3FFFF) != 0x3FFF8) return -1;
        return (int)(bits >> 18) & 7;
    }

    private static string TypeName(int type) => type < 0 ? "sans-synchro" : s_FrameTypes[type];

    /// <summary>Rend 0 si tout concorde, 1 sur un écart, 2 sur une erreur.</summary>
    internal static int Run(string aviPath, string codecPath, int maxImages, string? outDir,
                            bool referenceOnly, bool keepGoing)
    {
        using AviReader? reader = AviReader.Open(aviPath);
        if (reader is null)
        {
            Console.WriteLine($"[INDEO] {aviPath} : pas un AVI lisible");
            return 2;
        }
        if (reader.VideoCompression != "IV41")
        {
            Console.WriteLine($"[INDEO] {aviPath} : video {reader.VideoCompression}, pas IV41");
            return 2;
        }

        int w = reader.Width, h = reader.Height;
        Console.WriteLine($"[INDEO] {Path.GetFileName(aviPath)} : {w}x{h}, {reader.TotalFrames} images annoncees");
        Console.WriteLine($"[INDEO] reference : {Path.GetFullPath(codecPath)} (sortie IF09)");
        Console.WriteLine(referenceOnly
            ? "[INDEO] mode : reference seule (deux instances du codec d'Intel, determinisme)"
            : "[INDEO] mode : codec d'Intel contre Indeo4Decoder");

        using var reference = new ReferenceIr41(codecPath, w, h);
        using ReferenceIr41? twin = referenceOnly ? new ReferenceIr41(codecPath, w, h) : null;
        Indeo4Decoder? ours = referenceOnly ? null : new Indeo4Decoder(w, h);

        var expected = new Yvu9Image(w, h);
        var actual = new Yvu9Image(w, h);
        var typeCounts = new int[9];
        int flagFrames = 0, divergent = 0, index = 0;
        byte[] buffer = new byte[64 * 1024];

        while (maxImages < 0 || index < maxImages)
        {
            AviReader.ChunkKind kind = reader.NextChunk(ref buffer, out int length);
            if (kind == AviReader.ChunkKind.End) break;
            if (kind != AviReader.ChunkKind.Video) continue;

            ReadOnlySpan<byte> frame = buffer.AsSpan(0, length);
            int type = FrameType(frame);
            typeCounts[type < 0 ? 8 : type]++;

            reference.Decode(frame, expected, out ReadOnlySpan<byte> flags);
            if (flags.IndexOfAnyExcept((byte)0) >= 0) flagFrames++;

            if (twin is not null)
            {
                twin.Decode(frame, actual, out _);
            }
            else
            {
                try
                {
                    ours!.Decode(frame, actual);
                }
                catch (NotImplementedException e)
                {
                    Console.WriteLine($"[INDEO] image {index} : le decodeur C# n'est pas pret");
                    Console.WriteLine($"        {e.Message}");
                    Console.WriteLine("[INDEO] `--reference-seule` fait tourner le cote Intel seul.");
                    return 2;
                }
            }

            if (!Report(index, type, length, expected, actual, outDir))
            {
                divergent++;
                if (!keepGoing) { index++; break; }
            }
            index++;
        }

        Console.WriteLine($"[INDEO] {index} images decodees, {divergent} differentes");
        Console.WriteLine("[INDEO] genres : " + string.Join(", ",
            Enumerable.Range(0, 9).Where(t => typeCounts[t] > 0)
                      .Select(t => $"{(t == 8 ? "sans-synchro" : s_FrameTypes[t])} {typeCounts[t]}")));
        Console.WriteLine($"[INDEO] plan d'indicateurs IF09 non nul sur {flagFrames} image(s)");
        if (ours is not null)
        {
            // Ce que le flux a exercé dans le portage : une case jamais vue n'est pas vérifiée.
            Console.WriteLine($"[INDEO] flux : echelonnable {ours.IsScalable}, tuiles {ours.UsesTiling}, " +
                              $"images B {ours.HasBFrames}, transparence {ours.HasTransparency}, " +
                              $"Haar {ours.UsesHaar}, plein pixel {ours.UsesFullpel}, " +
                              $"images INTER glissees apres une INTRA {ours.EmbeddedFrames}");
        }
        Console.WriteLine(divergent == 0 ? "[INDEO] IDENTIQUE" : "[INDEO] DIFFERENT");
        return divergent == 0 ? 0 : 1;
    }

    /// <summary>
    /// `--rgb` : ce que le jeu AFFICHE. Notre conversion (`Host/Yvu9Rgb.cs`) contre le RGB24 tramé
    /// du codec d'Intel. Pas d'égalité possible — Intel trame, nous pas —, donc une mesure : la
    /// répartition des écarts par canal, et l'image la plus éloignée. Rend 0, ou 2 sur une erreur.
    /// </summary>
    internal static int RunRgb(string aviPath, string codecPath, int maxImages, string? outDir)
    {
        using AviReader? reader = AviReader.Open(aviPath);
        if (reader is null || reader.VideoCompression != "IV41")
        {
            Console.WriteLine($"[INDEO] {aviPath} : pas un AVI IV41 lisible");
            return 2;
        }

        int w = reader.Width, h = reader.Height, stride = ReferenceIr41.RgbStride(w);
        Console.WriteLine($"[INDEO] {Path.GetFileName(aviPath)} : {w}x{h}, RGB d'Intel (trame) contre Yvu9Rgb");

        using var reference = new ReferenceIr41(codecPath, w, h, rgb: true);
        var ours = new Indeo4Decoder(w, h);
        var image = new Yvu9Image(w, h);
        byte[] dib = new byte[stride * h];
        byte[] worstExpected = new byte[dib.Length], worstActual = new byte[dib.Length];

        var histogram = new long[10];   // écarts 0..8, puis 9 et plus
        long total = 0, sum = 0;
        int max = 0, index = 0, worstIndex = -1;
        double worstMean = -1;
        byte[] buffer = new byte[64 * 1024];

        while (maxImages < 0 || index < maxImages)
        {
            AviReader.ChunkKind kind = reader.NextChunk(ref buffer, out int length);
            if (kind == AviReader.ChunkKind.End) break;
            if (kind != AviReader.ChunkKind.Video) continue;

            ReadOnlySpan<byte> frame = buffer.AsSpan(0, length);
            ReadOnlySpan<byte> expected = reference.DecodeRgb(frame);
            ours.Decode(frame, image);
            Yvu9Rgb.ToBottomUpBgr(image, dib, stride);

            long frameSum = 0;
            for (int y = 0; y < h; y++)
            {
                for (int i = y * stride, end = i + w * 3; i < end; i++)
                {
                    int d = Math.Abs(expected[i] - dib[i]);
                    histogram[Math.Min(d, 9)]++;
                    frameSum += d;
                    if (d > max) max = d;
                }
            }
            total += w * 3L * h;
            sum += frameSum;

            double mean = (double)frameSum / (w * 3 * h);
            if (mean > worstMean)
            {
                worstMean = mean;
                worstIndex = index;
                expected.CopyTo(worstExpected);
                dib.CopyTo(worstActual, 0);
            }
            index++;
        }

        Console.WriteLine($"[INDEO] {index} images, {total} valeurs de canal comparees");
        Console.WriteLine($"[INDEO] ecart moyen {(double)sum / Math.Max(1, total):F2}, ecart max {max}");
        Console.WriteLine("[INDEO] repartition : " + string.Join(", ", Enumerable.Range(0, 10).Select(d =>
            $"{(d == 9 ? "9+" : d.ToString())} {100.0 * histogram[d] / Math.Max(1, total):F2} %")));
        Console.WriteLine($"[INDEO] image la plus eloignee : {worstIndex} (ecart moyen {worstMean:F2})");

        if (outDir is not null && worstIndex >= 0)
        {
            Directory.CreateDirectory(outDir);
            string path = Path.Combine(outDir, $"indeo-rgb-{worstIndex:D5}.png");
            WriteRgbComparison(path, worstExpected, worstActual, w, h, stride);
            Console.WriteLine($"[INDEO] Intel | Yvu9Rgb | ecart x8 : {Path.GetFullPath(path)}");
        }
        return 0;
    }

    /// <summary>Trois vignettes côte à côte en couleur, depuis deux DIB BGR rangées du bas vers le haut.</summary>
    private static void WriteRgbComparison(string path, byte[] expected, byte[] actual, int width, int height, int stride)
    {
        int outWidth = width * 3, rowBytes = outWidth * 3;
        byte[] raw = new byte[(rowBytes + 1) * height];
        for (int y = 0; y < height; y++)
        {
            int row = y * (rowBytes + 1) + 1;
            int src = (height - 1 - y) * stride;
            for (int x = 0; x < width; x++)
            {
                for (int k = 0; k < 3; k++)
                {
                    byte e = expected[src + x * 3 + 2 - k], a = actual[src + x * 3 + 2 - k];   // BGR -> RGB
                    raw[row + x * 3 + k] = e;
                    raw[row + (width + x) * 3 + k] = a;
                    raw[row + (2 * width + x) * 3 + k] = (byte)Math.Min(255, Math.Abs(e - a) * 8);
                }
            }
        }
        WritePng(path, raw, outWidth, height, colorType: 2);
    }

    /// <summary>Compare une image ; rend vrai si les trois plans sont identiques, sinon décrit l'écart.</summary>
    private static bool Report(int index, int type, int length, Yvu9Image expected, Yvu9Image actual, string? outDir)
    {
        var planes = new (string Name, byte[] Expected, byte[] Actual, int Width)[]
        {
            ("Y", expected.Y, actual.Y, expected.Width),
            ("V", expected.V, actual.V, expected.ChromaWidth),
            ("U", expected.U, actual.U, expected.ChromaWidth),
        };

        bool same = true;
        foreach (var (name, exp, act, width) in planes)
        {
            int first = exp.AsSpan().CommonPrefixLength(act);
            if (first == exp.Length) continue;

            if (same)
                Console.WriteLine($"[INDEO] image {index} ({TypeName(type)}, {length} octets) : DIFFERENTE");
            same = false;

            int count = 0, max = 0;
            for (int i = first; i < exp.Length; i++)
            {
                int d = Math.Abs(exp[i] - act[i]);
                if (d == 0) continue;
                count++;
                if (d > max) max = d;
            }
            Console.WriteLine($"        plan {name} : {count}/{exp.Length} octets differents, ecart max {max} ; " +
                              $"premier en ({first % width}, {first / width}) : attendu {exp[first]}, obtenu {act[first]}");
        }

        if (!same && outDir is not null)
        {
            Directory.CreateDirectory(outDir);
            string path = Path.Combine(outDir, $"indeo-{index:D5}-Y.png");
            WriteComparison(path, expected.Y, actual.Y, expected.Width, expected.Height);
            Console.WriteLine($"        attendu | obtenu | ecart x8 : {Path.GetFullPath(path)}");
        }
        return same;
    }

    /// <summary>Trois vignettes côte à côte, en niveaux de gris : l'attendu, l'obtenu, l'écart grossi huit fois.</summary>
    private static void WriteComparison(string path, byte[] expected, byte[] actual, int width, int height)
    {
        int outWidth = width * 3;
        byte[] raw = new byte[(outWidth + 1) * height];
        for (int y = 0; y < height; y++)
        {
            int row = y * (outWidth + 1) + 1;   // l'octet 0 de chaque ligne : filtre PNG « aucun »
            for (int x = 0; x < width; x++)
            {
                byte e = expected[y * width + x], a = actual[y * width + x];
                raw[row + x] = e;
                raw[row + width + x] = a;
                raw[row + 2 * width + x] = (byte)Math.Min(255, Math.Abs(e - a) * 8);
            }
        }

        WritePng(path, raw, outWidth, height, colorType: 0);
    }

    /// <summary>
    /// Un PNG 8 bits par canal. <paramref name="raw"/> porte déjà l'octet de filtre (0) en tête de
    /// chaque ligne ; <paramref name="colorType"/> 0 niveaux de gris, 2 RGB.
    /// </summary>
    private static void WritePng(string path, byte[] raw, int width, int height, byte colorType)
    {
        using var file = File.Create(path);
        file.Write([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]);
        byte[] ihdr = new byte[13];
        WriteBigEndian(ihdr, 0, (uint)width);
        WriteBigEndian(ihdr, 4, (uint)height);
        ihdr[8] = 8;
        ihdr[9] = colorType;
        WriteChunk(file, "IHDR", ihdr);

        var compressed = new MemoryStream();
        using (var z = new ZLibStream(compressed, CompressionLevel.Optimal, leaveOpen: true))
            z.Write(raw);
        WriteChunk(file, "IDAT", compressed.ToArray());
        WriteChunk(file, "IEND", []);
    }

    private static void WriteBigEndian(byte[] b, int o, uint v)
    {
        b[o] = (byte)(v >> 24); b[o + 1] = (byte)(v >> 16); b[o + 2] = (byte)(v >> 8); b[o + 3] = (byte)v;
    }

    private static void WriteChunk(Stream s, string type, byte[] payload)
    {
        byte[] head = new byte[8];
        WriteBigEndian(head, 0, (uint)payload.Length);
        for (int i = 0; i < 4; i++) head[4 + i] = (byte)type[i];
        s.Write(head);
        s.Write(payload);

        uint crc = 0xFFFFFFFFu;
        foreach (byte b in head.AsSpan(4)) crc = Crc(crc, b);
        foreach (byte b in payload) crc = Crc(crc, b);
        byte[] tail = new byte[4];
        WriteBigEndian(tail, 0, crc ^ 0xFFFFFFFFu);
        s.Write(tail);
    }

    private static uint Crc(uint crc, byte b)
    {
        crc ^= b;
        for (int k = 0; k < 8; k++) crc = (crc & 1) != 0 ? 0xEDB88320u ^ (crc >> 1) : crc >> 1;
        return crc;
    }
}
