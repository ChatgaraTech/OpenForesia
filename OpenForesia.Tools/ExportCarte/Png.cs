using System.IO.Compression;

namespace ExportCarte;

/// <summary>Un encodeur PNG RGBA 8 bits minimal : un IHDR, un IDAT compressé par zlib, un IEND.</summary>
internal static class Png
{
    public static byte[] Encode(byte[] rgba, int width, int height)
    {
        using var raw = new MemoryStream();
        for (int y = 0; y < height; y++)
        {
            raw.WriteByte(0);   // filtre « aucun »
            raw.Write(rgba, y * width * 4, width * 4);
        }

        using var z = new MemoryStream();
        using (var zl = new ZLibStream(z, CompressionLevel.Optimal, leaveOpen: true))
        {
            raw.Position = 0;
            raw.CopyTo(zl);
        }

        using var png = new MemoryStream();
        png.Write([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]);
        var ihdr = new byte[13];
        WriteBE(ihdr, 0, (uint)width);
        WriteBE(ihdr, 4, (uint)height);
        ihdr[8] = 8; ihdr[9] = 6;   // 8 bits, RGBA
        Chunk(png, "IHDR", ihdr);
        Chunk(png, "IDAT", z.ToArray());
        Chunk(png, "IEND", []);
        return png.ToArray();
    }

    private static void Chunk(Stream s, string type, byte[] data)
    {
        var len = new byte[4];
        WriteBE(len, 0, (uint)data.Length);
        s.Write(len);
        var td = new byte[4 + data.Length];
        for (int i = 0; i < 4; i++) td[i] = (byte)type[i];
        Buffer.BlockCopy(data, 0, td, 4, data.Length);
        s.Write(td);
        var crc = new byte[4];
        WriteBE(crc, 0, Crc(td));
        s.Write(crc);
    }

    private static void WriteBE(byte[] b, int o, uint v)
    {
        b[o] = (byte)(v >> 24); b[o + 1] = (byte)(v >> 16); b[o + 2] = (byte)(v >> 8); b[o + 3] = (byte)v;
    }

    private static uint[]? s_table;

    private static uint Crc(byte[] data)
    {
        if (s_table == null)
        {
            s_table = new uint[256];
            for (uint n = 0; n < 256; n++)
            {
                uint c = n;
                for (int k = 0; k < 8; k++) c = (c & 1) != 0 ? 0xEDB88320u ^ (c >> 1) : c >> 1;
                s_table[n] = c;
            }
        }
        uint crc = 0xFFFFFFFF;
        foreach (byte b in data) crc = s_table[(crc ^ b) & 0xff] ^ (crc >> 8);
        return crc ^ 0xFFFFFFFF;
    }
}
