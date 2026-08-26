// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// =====================================================================
// Le décodeur Indeo Video 4 des cinématiques, appelé dans sa DLL d'origine.
//
// `ir41_32.ax` vient du CD du jeu, dans les cabinets de `DXMedia\DXMEDIA.exe`. C'est la sixième
// bibliothèque de 1998 que le projet appelle plutôt que de la reconstruire, au même titre que
// `winsys3d.dll` et `wsyskern.dll` : le jeu de 1998 s'en servait, elle est sur le disque du joueur,
// et la réécrire serait un sous-système entier pour un gain nul aujourd'hui.
//
// **Elle n'est ni installée ni enregistrée.** `ir41_32.ax` exporte `DriverProc` : c'est donc aussi
// un codec Video for Windows, et `ICOpenFunction` accepte qu'on lui passe ce `DriverProc`
// directement. Le codec vit dans le seul processus du jeu, et ne voit que les fichiers du CD.
//
// Contexte complet, et la façon de sortir une image en PNG pour vérifier : voir
// `Documentation/Reference/cinematiques.md` et `OpenForesia.Tools/decode-movie.ps1`.
// =====================================================================

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

internal static unsafe class IndeoCodec
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

    private const uint FccVidc = 0x63646976;   // 'vidc', poids faible en tête
    private const uint FccIv41 = 0x31345649;   // 'IV41'
    private const uint IcModeDecompress = 4;

    private const uint DrvLoad = 1, DrvEnable = 2;
    private const uint DrvUser = 0x4000;
    private const uint IcmDecompressQuery = DrvUser + 11;
    private const uint IcmDecompressBegin = DrvUser + 12;
    private const uint IcmDecompress = DrvUser + 13;
    private const uint IcmDecompressEnd = DrvUser + 14;

    private const uint IcDecompressNotKeyFrame = 0x08000000;

    /// <summary>Le nom du fichier tel qu'il est sur le CD ; il est copié à côté de l'exécutable.</summary>
    internal const string FileName = "ir41_32.ax";

    private static IntPtr s_DriverProc;
    private static bool s_Tried;

    /// <summary>
    /// Charge le codec une fois pour toutes. Rend faux s'il est absent — auquel cas les
    /// cinématiques ne se jouent pas, ce que la séquence d'introduction de l'original traite déjà
    /// comme un cas normal.
    /// </summary>
    private static bool EnsureLoaded()
    {
        if (s_Tried) return s_DriverProc != IntPtr.Zero;
        s_Tried = true;

        string directory = AppContext.BaseDirectory;
        string path = Path.Combine(directory, FileName);
        if (!File.Exists(path))
        {
            Console.Error.WriteLine($"[FILM] {FileName} absent de {directory} : pas de cinematique");
            return false;
        }

        // Le codec charge lui-même ses compagnons (`ir41_qcx.dll` et consorts) par `LoadLibrary`,
        // donc sans chemin. Sans ce `SetDllDirectory` ils ne se trouvent pas, et l'échec ne se voit
        // qu'au premier décodage, sous la forme d'un `ICERR_ERROR` générique.
        SetDllDirectoryW(directory);
        IntPtr module = LoadLibraryW(path);
        SetDllDirectoryW(null);

        if (module == IntPtr.Zero)
        {
            Console.Error.WriteLine($"[FILM] LoadLibrary({FileName}) a echoue : {Marshal.GetLastWin32Error()}");
            return false;
        }

        IntPtr proc = GetProcAddress(module, "DriverProc");
        if (proc == IntPtr.Zero)
        {
            Console.Error.WriteLine($"[FILM] {FileName} n'exporte pas DriverProc");
            return false;
        }

        // **Le piège.** `ICOpenFunction` n'envoie au pilote que `DRV_OPEN` — ni `DRV_LOAD`, ni
        // `DRV_ENABLE`. Or Indeo 4 prépare ses tables à la charge : sans ces deux messages, il
        // s'ouvre, accepte `ICM_DECOMPRESS_QUERY`, accepte `ICM_DECOMPRESS_BEGIN`, puis rend
        // `ICERR_ERROR` (-100) au premier décodage, pour tous les formats de sortie qu'il vient
        // pourtant d'accepter. Établi le 2026-08-26 en constatant que seize variantes de format
        // échouaient identiquement.
        var driver = Marshal.GetDelegateForFunctionPointer<DriverProcDelegate>(proc);
        driver(IntPtr.Zero, IntPtr.Zero, DrvLoad, IntPtr.Zero, IntPtr.Zero);
        driver(IntPtr.Zero, IntPtr.Zero, DrvEnable, IntPtr.Zero, IntPtr.Zero);

        s_DriverProc = proc;
        Console.Error.WriteLine($"[FILM] codec Indeo 4 charge depuis {path}");
        return true;
    }

    /// <summary>
    /// Un flux de décodage ouvert : le handle du codec et les deux en-têtes qu'il relit à chaque
    /// image. Les en-têtes vivent en mémoire non gérée parce que le codec en garde l'adresse entre
    /// <c>BEGIN</c> et les décodages qui suivent.
    /// </summary>
    internal sealed class Stream : IDisposable
    {
        internal IntPtr Handle;
        internal IntPtr InputHeader, OutputHeader;
        internal int Width, Height, Stride;

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                ICSendMessage(Handle, IcmDecompressEnd, IntPtr.Zero, IntPtr.Zero);
                ICClose(Handle);
                Handle = IntPtr.Zero;
            }
            if (InputHeader != IntPtr.Zero) { Marshal.FreeHGlobal(InputHeader); InputHeader = IntPtr.Zero; }
            if (OutputHeader != IntPtr.Zero) { Marshal.FreeHGlobal(OutputHeader); OutputHeader = IntPtr.Zero; }
        }
    }

    private static void WriteHeader(byte* p, int width, int height, ushort bits, uint compression, uint sizeImage)
    {
        for (int i = 0; i < HeaderBytes; i++) p[i] = 0;
        *(uint*)(p + 0) = 40;          // biSize
        *(int*)(p + 4) = width;
        *(int*)(p + 8) = height;
        *(ushort*)(p + 12) = 1;        // biPlanes
        *(ushort*)(p + 14) = bits;
        *(uint*)(p + 16) = compression;
        *(uint*)(p + 20) = sizeImage;
    }

    // Le codec réclame 1064 octets pour un format de sortie (`BITMAPINFOHEADER` + 1024), même en
    // vraies couleurs où il n'y a pas de palette. On les lui donne plutôt que 40.
    private const int HeaderBytes = 1064;

    /// <summary>
    /// Ouvre un flux de décodage IV41 → RGB 24 bits aux dimensions données. Rend <c>null</c> si le
    /// codec est absent ou refuse la conversion.
    /// </summary>
    internal static Stream? OpenStream(int width, int height)
    {
        if (!EnsureLoaded()) return null;

        var stream = new Stream
        {
            Width = width,
            Height = height,
            // Les lignes d'une DIB sont alignées sur quatre octets.
            Stride = (width * 3 + 3) & ~3,
            InputHeader = Marshal.AllocHGlobal(HeaderBytes),
            OutputHeader = Marshal.AllocHGlobal(HeaderBytes),
        };

        var input = (byte*)stream.InputHeader;
        var output = (byte*)stream.OutputHeader;
        WriteHeader(input, width, height, 24, FccIv41, 0);
        WriteHeader(output, width, height, 24, 0 /* BI_RGB */, (uint)(stream.Stride * height));

        stream.Handle = ICOpenFunction(FccVidc, FccIv41, IcModeDecompress, s_DriverProc);
        if (stream.Handle == IntPtr.Zero)
        {
            Console.Error.WriteLine("[FILM] ICOpenFunction a echoue");
            stream.Dispose();
            return null;
        }

        int query = ICSendMessage(stream.Handle, IcmDecompressQuery, stream.InputHeader, stream.OutputHeader);
        int begin = query == 0
            ? ICSendMessage(stream.Handle, IcmDecompressBegin, stream.InputHeader, stream.OutputHeader)
            : query;
        if (begin != 0)
        {
            Console.Error.WriteLine($"[FILM] le codec refuse IV41 {width}x{height} -> RGB24 (query {query}, begin {begin})");
            stream.Dispose();
            return null;
        }

        return stream;
    }

    /// <summary>
    /// Décode une image compressée dans <paramref name="destination"/>, une DIB 24 bits rangée du
    /// bas vers le haut et en BGR. Rend faux sur refus du codec.
    /// </summary>
    internal static bool Decode(Stream stream, byte* source, int sourceLength, bool keyFrame, byte* destination)
    {
        // La taille de l'image compressée passe par `biSizeImage` de l'en-tête d'entrée : c'est là
        // que le codec la lit, pas dans la structure ICDECOMPRESS.
        *(uint*)((byte*)stream.InputHeader + 20) = (uint)sourceLength;

        // ICDECOMPRESS, x86 : drapeaux, les deux en-têtes, les deux tampons, l'identifiant de bloc.
        byte* request = stackalloc byte[24];
        *(uint*)(request + 0) = keyFrame ? 0 : IcDecompressNotKeyFrame;
        *(IntPtr*)(request + 4) = stream.InputHeader;
        *(IntPtr*)(request + 8) = (IntPtr)source;
        *(IntPtr*)(request + 12) = stream.OutputHeader;
        *(IntPtr*)(request + 16) = (IntPtr)destination;
        *(uint*)(request + 20) = 0;

        int rc = ICSendMessage(stream.Handle, IcmDecompress, (IntPtr)request, (IntPtr)24);
        if (rc != 0) Console.Error.WriteLine($"[FILM] ICDecompress = {rc}");
        return rc == 0;
    }
}
