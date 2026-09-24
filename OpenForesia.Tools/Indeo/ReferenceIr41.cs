// =====================================================================
// Le décodeur Indeo 4 d'Intel, `ir41_32.ax`, appelé pour sa sortie NATIVE — l'oracle.
//
// Le jeu lui demandait du RGB24 (`Platform/IndeoCodec.cs`, retiré le 2026-09-24). Ici on lui demande `IF09` : c'est du YVU9
// suivi d'un plan d'indicateurs, et c'est ce qu'il produit avant toute conversion de couleur. Le
// RGB24 est tramé selon un motif 4×4 ; seul `IF09` se compare à l'octet près. Établi le 2026-09-24,
// voir `Documentation/DLL/ir41_32.md`, section « Les formats de sortie ».
//
// Même piège qu'en jeu : `ICOpenFunction` n'envoie que `DRV_OPEN`, il faut `DRV_LOAD` et
// `DRV_ENABLE` à la main, sinon chaque décodage rend `ICERR_ERROR`.
// =====================================================================

using System.Runtime.InteropServices;
using OpenForesia.Blaze;

namespace OpenForesia.Tools.Indeo;

internal sealed unsafe class ReferenceIr41 : IDisposable
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr LoadLibraryW(string path);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern bool SetDllDirectoryW(string? path);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
    private static extern IntPtr GetProcAddress(IntPtr module, string name);

    [DllImport("msvfw32.dll")]
    private static extern IntPtr ICOpenFunction(uint fccType, uint fccHandler, uint mode, IntPtr proc);
    [DllImport("msvfw32.dll")]
    private static extern int ICClose(IntPtr hic);
    [DllImport("msvfw32.dll")]
    private static extern int ICSendMessage(IntPtr hic, uint msg, IntPtr dw1, IntPtr dw2);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int DriverProcDelegate(IntPtr id, IntPtr driver, uint msg, IntPtr lp1, IntPtr lp2);

    private const uint DrvUser = 0x4000;
    private const uint IcmDecompressQuery = DrvUser + 11;
    private const uint IcmDecompressBegin = DrvUser + 12;
    private const uint IcmDecompress = DrvUser + 13;
    private const uint IcmDecompressEnd = DrvUser + 14;
    private const uint IcDecompressNotKeyFrame = 0x08000000;

    // Le codec réclame 1064 octets par en-tête (BITMAPINFOHEADER + 1024), même sans palette.
    private const int HeaderBytes = 1064;

    private static IntPtr s_DriverProc;

    private IntPtr _handle, _inputHeader, _outputHeader;
    private readonly byte[] _output;   // IF09, ou la DIB RGB24 en mode RGB
    private readonly bool _rgb;
    private readonly int _width, _height;
    private int _frameIndex;

    private static uint Fourcc(string s) => (uint)(s[0] | s[1] << 8 | s[2] << 16 | s[3] << 24);

    private static void Load(string codecPath)
    {
        if (s_DriverProc != IntPtr.Zero) return;
        string full = Path.GetFullPath(codecPath);
        if (!File.Exists(full)) throw new FileNotFoundException("codec introuvable", full);

        // Le codec cherche ses compagnons par `LoadLibrary` sans chemin.
        SetDllDirectoryW(Path.GetDirectoryName(full));
        IntPtr module = LoadLibraryW(full);
        SetDllDirectoryW(null);
        if (module == IntPtr.Zero)
            throw new InvalidOperationException($"LoadLibrary({full}) a echoue : {Marshal.GetLastWin32Error()}");

        IntPtr proc = GetProcAddress(module, "DriverProc");
        if (proc == IntPtr.Zero) throw new InvalidOperationException("DriverProc introuvable dans le codec");

        var driver = Marshal.GetDelegateForFunctionPointer<DriverProcDelegate>(proc);
        driver(IntPtr.Zero, IntPtr.Zero, 1 /* DRV_LOAD */, IntPtr.Zero, IntPtr.Zero);
        driver(IntPtr.Zero, IntPtr.Zero, 2 /* DRV_ENABLE */, IntPtr.Zero, IntPtr.Zero);
        s_DriverProc = proc;
    }

    private static void WriteHeader(byte* p, int width, int height, ushort bits, uint compression, uint sizeImage)
    {
        new Span<byte>(p, HeaderBytes).Clear();
        *(uint*)(p + 0) = 40;
        *(int*)(p + 4) = width;
        *(int*)(p + 8) = height;
        *(ushort*)(p + 12) = 1;
        *(ushort*)(p + 14) = bits;
        *(uint*)(p + 16) = compression;
        *(uint*)(p + 20) = sizeImage;
    }

    /// <summary>Taille d'une image IF09 : Y, V, U, puis le plan d'indicateurs, de la taille d'un plan de chrominance.</summary>
    internal static int If09Size(int width, int height)
    {
        int chroma = ((width + 3) >> 2) * ((height + 3) >> 2);
        return width * height + 3 * chroma;
    }

    /// <param name="rgb">
    /// Faux : sortie IF09, la référence à l'octet près. Vrai : la sortie RGB24 tramée que le jeu
    /// demandait, pour mesurer l'écart de notre propre conversion de couleur (`Host/Yvu9Rgb.cs`).
    /// </param>
    internal ReferenceIr41(string codecPath, int width, int height, bool rgb = false)
    {
        Load(codecPath);
        _width = width;
        _height = height;
        _rgb = rgb;
        _output = new byte[rgb ? RgbStride(width) * height : If09Size(width, height)];

        _inputHeader = Marshal.AllocHGlobal(HeaderBytes);
        _outputHeader = Marshal.AllocHGlobal(HeaderBytes);
        WriteHeader((byte*)_inputHeader, width, height, 24, Fourcc("IV41"), 0);
        // Hauteur positive : pour un format YUV, c'est de haut en bas (vérifié contre le RGB24,
        // qui lui est un DIB rangé de bas en haut).
        if (rgb) WriteHeader((byte*)_outputHeader, width, height, 24, 0 /* BI_RGB */, (uint)_output.Length);
        else WriteHeader((byte*)_outputHeader, width, height, 9, Fourcc("IF09"), (uint)_output.Length);

        _handle = ICOpenFunction(Fourcc("vidc"), Fourcc("IV41"), 4 /* ICMODE_DECOMPRESS */, s_DriverProc);
        if (_handle == IntPtr.Zero) throw new InvalidOperationException("ICOpenFunction a echoue");

        int query = ICSendMessage(_handle, IcmDecompressQuery, _inputHeader, _outputHeader);
        if (query != 0) throw new InvalidOperationException($"le codec refuse IV41 -> {(rgb ? "RGB24" : "IF09")} (query {query})");
        int begin = ICSendMessage(_handle, IcmDecompressBegin, _inputHeader, _outputHeader);
        if (begin != 0) throw new InvalidOperationException($"le codec refuse IV41 -> {(rgb ? "RGB24" : "IF09")} (begin {begin})");
    }

    /// <summary>Les lignes d'une DIB sont alignées sur quatre octets.</summary>
    internal static int RgbStride(int width) => (width * 3 + 3) & ~3;

    /// <summary>
    /// Décode l'image suivante. Rend le plan d'indicateurs d'IF09 à part, pour qu'on puisse
    /// signaler s'il sert un jour — il est à zéro sur tout ce qu'on a vu.
    /// </summary>
    internal void Decode(ReadOnlySpan<byte> frame, Yvu9Image output, out ReadOnlySpan<byte> flags)
    {
        if (_rgb) throw new InvalidOperationException("instance ouverte en RGB24");
        DecodeRaw(frame);

        int y = _width * _height, c = output.V.Length;
        _output.AsSpan(0, y).CopyTo(output.Y);
        _output.AsSpan(y, c).CopyTo(output.V);
        _output.AsSpan(y + c, c).CopyTo(output.U);
        flags = _output.AsSpan(y + 2 * c, c);
    }

    /// <summary>Décode l'image suivante en RGB24 : une DIB BGR rangée du bas vers le haut, tramée par le codec.</summary>
    internal ReadOnlySpan<byte> DecodeRgb(ReadOnlySpan<byte> frame)
    {
        if (!_rgb) throw new InvalidOperationException("instance ouverte en IF09");
        DecodeRaw(frame);
        return _output;
    }

    private void DecodeRaw(ReadOnlySpan<byte> frame)
    {
        *(uint*)((byte*)_inputHeader + 20) = (uint)frame.Length;

        fixed (byte* source = frame)
        fixed (byte* destination = _output)
        {
            byte* request = stackalloc byte[24];
            *(uint*)(request + 0) = _frameIndex == 0 ? 0 : IcDecompressNotKeyFrame;
            *(IntPtr*)(request + 4) = _inputHeader;
            *(IntPtr*)(request + 8) = (IntPtr)source;
            *(IntPtr*)(request + 12) = _outputHeader;
            *(IntPtr*)(request + 16) = (IntPtr)destination;
            *(uint*)(request + 20) = 0;

            int rc = ICSendMessage(_handle, IcmDecompress, (IntPtr)request, (IntPtr)24);
            if (rc != 0) throw new InvalidOperationException($"ICDecompress = {rc} a l'image {_frameIndex}");
        }
        _frameIndex++;
    }

    public void Dispose()
    {
        if (_handle != IntPtr.Zero)
        {
            ICSendMessage(_handle, IcmDecompressEnd, IntPtr.Zero, IntPtr.Zero);
            ICClose(_handle);
            _handle = IntPtr.Zero;
        }
        if (_inputHeader != IntPtr.Zero) { Marshal.FreeHGlobal(_inputHeader); _inputHeader = IntPtr.Zero; }
        if (_outputHeader != IntPtr.Zero) { Marshal.FreeHGlobal(_outputHeader); _outputHeader = IntPtr.Zero; }
    }
}
