# Sort une image d'une cinematique du jeu en PNG, avec le codec Indeo 4 D'ORIGINE -- celui du CD.
#
# Voir Documentation/Reference/cinematiques.md pour le contexte complet. En resume :
#
#   - les quatre .avi du CD sont en Indeo Video 4.1 (`IV41`), que Windows ne sait plus decoder
#     depuis que Microsoft a retire les codecs Indeo -- c'est pour ca que les cinematiques ne
#     passent plus, ni dans le jeu d'origine ni chez nous ;
#   - `ir41_32.ax`, le decodeur Indeo 4 de 1998, est sur le CD du jeu, dans les cabinets de
#     `DXMedia\DXMEDIA.exe`. Il exporte `DriverProc` : c'est donc AUSSI un codec Video for Windows,
#     et `ICOpenFunction` accepte qu'on lui passe ce `DriverProc` directement. Rien n'est installe,
#     rien n'est enregistre dans le systeme, rien n'est telecharge.
#
# Le codec est en 32 bits : ce script doit tourner sous le PowerShell x86.
#
#   & C:\Windows\SysWOW64\WindowsPowerShell\v1.0\powershell.exe -File OpenForesia.Tools\decode-movie.ps1 `
#       -Avi OpenForesia\bin\Debug\net10.0\French\Teps.AVI -Out scratchpad\teps.png -Frame 120
#
# Les films sont ceux du dossier du jeu, extraits de l'ISO par OpenForesia-Setup --extraire-donnees.
#
# Le decodage est SEQUENTIEL : atteindre l'image N coute le decodage des N precedentes, puisque
# seules les images cles se decodent seules.

param(
    [string]$Avi   = 'OpenForesia\bin\Debug\net10.0\French\Teps.AVI',
    [string]$Out   = 'frame.png',
    [int]   $Frame = 0,
    [string]$Codec = 'Legacy\Binaries\ir41_32.ax'
)

$ErrorActionPreference = 'Stop'

if ([Environment]::Is64BitProcess) {
    throw "ce script doit tourner en 32 bits (SysWOW64\WindowsPowerShell\v1.0\powershell.exe)"
}

$cp = New-Object System.CodeDom.Compiler.CompilerParameters
$cp.CompilerOptions = '/unsafe'
$cp.GenerateInMemory = $true
$null = $cp.ReferencedAssemblies.Add('System.dll')
$null = $cp.ReferencedAssemblies.Add('System.Core.dll')

Add-Type -CompilerParameters $cp -TypeDefinition @"
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;

public static unsafe class IndeoVfw
{
    // ------------------------------------------------------------------ Video for Windows
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern IntPtr LoadLibraryW(string path);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern bool SetDllDirectoryW(string path);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    static extern IntPtr GetProcAddress(IntPtr module, string name);

    [DllImport("msvfw32.dll")] static extern IntPtr ICOpenFunction(uint fccType, uint fccHandler, uint mode, IntPtr proc);
    [DllImport("msvfw32.dll")] static extern int ICClose(IntPtr hic);
    [DllImport("msvfw32.dll")] static extern int ICSendMessage(IntPtr hic, uint msg, IntPtr dw1, IntPtr dw2);

    const uint FCC_VIDC = 0x63646976;   // 'vidc', poids faible en tete
    const uint FCC_IV41 = 0x31345649;   // 'IV41'
    const uint ICMODE_DECOMPRESS = 4;
    const uint DRV_USER = 0x4000;
    const uint ICM_DECOMPRESS_QUERY = DRV_USER + 11;
    const uint ICM_DECOMPRESS_BEGIN = DRV_USER + 12;
    const uint ICM_DECOMPRESS      = DRV_USER + 13;
    const uint ICM_DECOMPRESS_END  = DRV_USER + 14;
    const uint ICDECOMPRESS_NOTKEYFRAME = 0x08000000;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    delegate int DriverProcDel(IntPtr id, IntPtr hdrv, uint msg, IntPtr lp1, IntPtr lp2);

    [StructLayout(LayoutKind.Sequential)]
    struct BitmapInfoHeader
    {
        public uint biSize;
        public int biWidth, biHeight;
        public ushort biPlanes, biBitCount;
        public uint biCompression, biSizeImage;
        public int biXPelsPerMeter, biYPelsPerMeter;
        public uint biClrUsed, biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct IcDecompress
    {
        public uint dwFlags;
        public IntPtr lpbiInput, lpInput, lpbiOutput, lpOutput;
        public uint ckid;
    }

    // ------------------------------------------------------------------ demultiplexage AVI
    static byte[] _b;
    static long _movi, _moviEnd;
    public static int Width, Height, UsPerFrame, TotalFrames;
    public static string Compression, Info;

    static string Fcc(int o) { return Encoding.ASCII.GetString(_b, o, 4); }
    static int I32(int o) { return BitConverter.ToInt32(_b, o); }
    static int I16(int o) { return BitConverter.ToUInt16(_b, o); }

    public static void Open(string path)
    {
        _b = File.ReadAllBytes(path);
        if (Fcc(0) != "RIFF" || Fcc(8) != "AVI ") throw new InvalidDataException("pas un RIFF/AVI");
        long riffEnd = Math.Min(8L + (uint)I32(4), _b.LongLength);
        var sb = new StringBuilder();
        long p = 12;
        while (p + 8 <= riffEnd)
        {
            string id = Fcc((int)p);
            long size = (uint)I32((int)p + 4);
            long body = p + 8;
            if (id == "LIST")
            {
                string t = Fcc((int)body);
                if (t == "hdrl") Hdrl(body + 4, body + size, sb);
                else if (t == "movi") { _movi = body + 4; _moviEnd = body + size; }
            }
            p = body + size + (size & 1);
        }
        Info = sb.ToString();
    }

    static void Hdrl(long start, long end, StringBuilder sb)
    {
        long p = start;
        while (p + 8 <= end)
        {
            string id = Fcc((int)p);
            long size = (uint)I32((int)p + 4);
            long body = p + 8;
            if (id == "avih") { UsPerFrame = I32((int)body); TotalFrames = I32((int)body + 16); }
            else if (id == "LIST" && Fcc((int)body) == "strl")
            {
                long q = body + 4, qe = body + size;
                string kind = null;
                while (q + 8 <= qe)
                {
                    string qid = Fcc((int)q);
                    long qs = (uint)I32((int)q + 4);
                    long qb = q + 8;
                    if (qid == "strh") kind = Fcc((int)qb);
                    else if (qid == "strf" && kind == "vids")
                    {
                        Width = I32((int)qb + 4);
                        Height = I32((int)qb + 8);
                        Compression = Fcc((int)qb + 16);
                    }
                    q = qb + qs + (qs & 1);
                }
            }
            p = body + size + (size & 1);
        }
    }

    public static List<byte[]> VideoChunks(int n)
    {
        var list = new List<byte[]>();
        long p = _movi;
        while (p + 8 <= _moviEnd && list.Count < n)
        {
            string id = Fcc((int)p);
            long size = (uint)I32((int)p + 4);
            long body = p + 8;
            if (id == "LIST") { p = body + 4; continue; }
            if ((id.EndsWith("dc") || id.EndsWith("db")) && body + size <= _moviEnd)
            {
                byte[] d = new byte[size];
                Array.Copy(_b, body, d, 0, size);
                list.Add(d);
            }
            p = body + size + (size & 1);
        }
        return list;
    }

    // ------------------------------------------------------------------ decodage
    static void Say(string s) { Console.WriteLine(s); }

    public static string Decode(string codecPath, string outPath, int wanted)
    {

        // Le codec charge lui-meme ses compagnons (ir41_qcx.dll et consorts) par LoadLibrary, donc
        // sans chemin : sans ce SetDllDirectory ils ne se trouvent pas, et l'echec ne se voit qu'au
        // moment de decoder, sous la forme d'un ICERR_ERROR generique.
        SetDllDirectoryW(Path.GetDirectoryName(Path.GetFullPath(codecPath)));

        IntPtr module = LoadLibraryW(codecPath);
        if (module == IntPtr.Zero)
            throw new Exception("LoadLibrary a echoue (" + Marshal.GetLastWin32Error() + ") : " + codecPath);
        IntPtr proc = GetProcAddress(module, "DriverProc");
        if (proc == IntPtr.Zero) throw new Exception("DriverProc introuvable dans le codec");
        Say("codec charge     : " + codecPath);

        // ICOpenFunction n'envoie que DRV_OPEN au pilote : ni DRV_LOAD, ni DRV_ENABLE. Indeo 4
        // prepare ses tables a la charge, si bien que sans ces deux messages il repond
        // correctement a QUERY et a BEGIN puis rend ICERR_ERROR (-100) au premier decodage --
        // un echec generique, tres loin de sa cause. Les envoyer a la main suffit.
        var drv = (DriverProcDel)Marshal.GetDelegateForFunctionPointer(proc, typeof(DriverProcDel));
        drv(IntPtr.Zero, IntPtr.Zero, 1 /* DRV_LOAD */, IntPtr.Zero, IntPtr.Zero);
        drv(IntPtr.Zero, IntPtr.Zero, 2 /* DRV_ENABLE */, IntPtr.Zero, IntPtr.Zero);
        Say("DRV_LOAD/ENABLE  : envoyes a la main (ICOpenFunction ne le fait pas)");

        var inHdr = new BitmapInfoHeader
        {
            biSize = 40, biWidth = Width, biHeight = Height, biPlanes = 1,
            biBitCount = 24, biCompression = FCC_IV41, biSizeImage = (uint)(Width * Height * 3)
        };
        var outHdr = new BitmapInfoHeader
        {
            biSize = 40, biWidth = Width, biHeight = Height, biPlanes = 1,
            biBitCount = 24, biCompression = 0 /* BI_RGB */, biSizeImage = (uint)(Width * Height * 3)
        };

        IntPtr hic = ICOpenFunction(FCC_VIDC, FCC_IV41, ICMODE_DECOMPRESS, proc);
        if (hic == IntPtr.Zero) throw new Exception("ICOpenFunction a echoue");
        Say("ICOpenFunction   : ouvert sans installation ni enregistrement");

        // On demande au codec le format de sortie qu'il PREFERE, plutot que de lui imposer le
        // notre : c'est lui qui sait, et un refus a l'execution ne dit pas lequel il voulait.
        byte[] suggested = new byte[1024];
        fixed (byte* psug = suggested)
        {
            BitmapInfoHeader* pin0 = &inHdr;
            int need = ICSendMessage(hic, DRV_USER + 10 /* ICM_DECOMPRESS_GET_FORMAT */,
                                     (IntPtr)pin0, IntPtr.Zero);
            int r2 = ICSendMessage(hic, DRV_USER + 10, (IntPtr)pin0, (IntPtr)psug);
            var s = (BitmapInfoHeader*)psug;
            Say("format suggere   : rc=" + r2 + ", taille=" + need + ", " +
                s->biWidth + "x" + s->biHeight + ", " + s->biBitCount + " bits, compression 0x" +
                s->biCompression.ToString("x8") + " (" +
                (s->biCompression == 0 ? "BI_RGB" :
                 Encoding.ASCII.GetString(BitConverter.GetBytes(s->biCompression))) + "), image " +
                s->biSizeImage + " octets");
        }

        int stride = ((Width * 3 + 3) / 4) * 4;   // les lignes d'un DIB sont alignees sur 4 octets
        byte[] rgb = new byte[stride * Height];

        int rc;
        // Des variables locales sont deja sur la pile : leur adresse se prend directement, un
        // `fixed` dessus est refuse par le compilateur.
        BitmapInfoHeader* pin = &inHdr;
        BitmapInfoHeader* pout = &outHdr;
        {
            rc = ICSendMessage(hic, ICM_DECOMPRESS_QUERY, (IntPtr)pin, (IntPtr)pout);
            Say("DECOMPRESS_QUERY : " + rc + (rc == 0 ? " (accepte)" : " (refuse)"));
            if (rc != 0) throw new Exception("le codec refuse la conversion demandee");

            rc = ICSendMessage(hic, ICM_DECOMPRESS_BEGIN, (IntPtr)pin, (IntPtr)pout);
            if (rc != 0) throw new Exception("DECOMPRESS_BEGIN = " + rc);

            var chunks = VideoChunks(wanted + 1);
            if (chunks.Count <= wanted) throw new Exception("image " + wanted + " absente");

            for (int i = 0; i <= wanted; i++)
            {
                byte[] src = chunks[i];
                fixed (byte* ps = src)
                fixed (byte* pd = rgb)
                {
                    inHdr.biSizeImage = (uint)src.Length;
                    var d = new IcDecompress
                    {
                        // Toutes les images sauf la premiere sont marquees « pas une image cle » :
                        // le codec doit alors composer sur l'image precedente, qu'il garde lui-meme.
                        dwFlags = i == 0 ? 0 : ICDECOMPRESS_NOTKEYFRAME,
                        lpbiInput = (IntPtr)pin, lpInput = (IntPtr)ps,
                        lpbiOutput = (IntPtr)pout, lpOutput = (IntPtr)pd, ckid = 0
                    };
                    rc = ICSendMessage(hic, ICM_DECOMPRESS, (IntPtr)(&d), (IntPtr)sizeof(IcDecompress));
                    if (rc != 0) throw new Exception("ICDecompress = " + rc + " a l'image " + i);
                }
            }
            Say("images decodees  : " + (wanted + 1) + " (jusqu'a l'image " + wanted + ")");
            ICSendMessage(hic, ICM_DECOMPRESS_END, IntPtr.Zero, IntPtr.Zero);
        }
        ICClose(hic);

        // Un DIB de hauteur positive est range du BAS vers le haut, et en BGR.
        byte[] png = new byte[Width * 3 * Height];
        for (int y = 0; y < Height; y++)
        {
            int srcRow = (Height - 1 - y) * stride;
            int dstRow = y * Width * 3;
            for (int x = 0; x < Width; x++)
            {
                png[dstRow + x * 3 + 0] = rgb[srcRow + x * 3 + 2];
                png[dstRow + x * 3 + 1] = rgb[srcRow + x * 3 + 1];
                png[dstRow + x * 3 + 2] = rgb[srcRow + x * 3 + 0];
            }
        }
        WritePng(outPath, png, Width, Height, Width * 3);
        Say("ecrit            : " + Path.GetFullPath(outPath));
        return "";
    }

    // ------------------------------------------------------------------ PNG
    static uint[] _crc;
    static uint Crc(byte[] d)
    {
        if (_crc == null)
        {
            _crc = new uint[256];
            for (uint n = 0; n < 256; n++)
            {
                uint c = n;
                for (int k = 0; k < 8; k++) c = (c & 1) != 0 ? 0xEDB88320u ^ (c >> 1) : c >> 1;
                _crc[n] = c;
            }
        }
        uint r = 0xFFFFFFFFu;
        for (int i = 0; i < d.Length; i++) r = _crc[(r ^ d[i]) & 0xFF] ^ (r >> 8);
        return r ^ 0xFFFFFFFFu;
    }

    static void Be(Stream s, uint v)
    {
        s.WriteByte((byte)(v >> 24)); s.WriteByte((byte)(v >> 16));
        s.WriteByte((byte)(v >> 8));  s.WriteByte((byte)v);
    }

    static void Chunk(Stream s, string type, byte[] payload)
    {
        Be(s, (uint)payload.Length);
        byte[] typed = new byte[4 + payload.Length];
        for (int i = 0; i < 4; i++) typed[i] = (byte)type[i];
        Array.Copy(payload, 0, typed, 4, payload.Length);
        s.Write(typed, 0, typed.Length);
        Be(s, Crc(typed));
    }

    static void WritePng(string path, byte[] rgb, int w, int h, int stride)
    {
        using (var file = File.Create(path))
        {
            file.Write(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, 0, 8);
            byte[] ihdr = new byte[13];
            ihdr[0] = (byte)(w >> 24); ihdr[1] = (byte)(w >> 16); ihdr[2] = (byte)(w >> 8); ihdr[3] = (byte)w;
            ihdr[4] = (byte)(h >> 24); ihdr[5] = (byte)(h >> 16); ihdr[6] = (byte)(h >> 8); ihdr[7] = (byte)h;
            ihdr[8] = 8; ihdr[9] = 2;
            Chunk(file, "IHDR", ihdr);

            byte[] raw = new byte[(w * 3 + 1) * h];
            for (int y = 0; y < h; y++) Array.Copy(rgb, y * stride, raw, y * (w * 3 + 1) + 1, w * 3);

            var ms = new MemoryStream();
            ms.WriteByte(0x78); ms.WriteByte(0x9C);
            using (var z = new DeflateStream(ms, CompressionMode.Compress, true)) z.Write(raw, 0, raw.Length);
            uint a = 1, b2 = 0;
            for (int i = 0; i < raw.Length; i++) { a = (a + raw[i]) % 65521; b2 = (b2 + a) % 65521; }
            uint adler = (b2 << 16) | a;
            ms.WriteByte((byte)(adler >> 24)); ms.WriteByte((byte)(adler >> 16));
            ms.WriteByte((byte)(adler >> 8));  ms.WriteByte((byte)adler);
            Chunk(file, "IDAT", ms.ToArray());
            Chunk(file, "IEND", new byte[0]);
        }
    }
}
"@

[IndeoVfw]::Open($Avi)
Write-Host ("fichier          : {0}" -f $Avi)
Write-Host ("video            : {0}, {1}x{2}, {3:F2} im/s, {4} images" -f `
    [IndeoVfw]::Compression, [IndeoVfw]::Width, [IndeoVfw]::Height,
    (1000000.0 / [IndeoVfw]::UsPerFrame), [IndeoVfw]::TotalFrames)
Write-Host ([IndeoVfw]::Decode($Codec, $Out, $Frame)).TrimEnd()
