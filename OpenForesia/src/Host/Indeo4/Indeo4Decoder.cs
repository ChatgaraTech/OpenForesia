// =====================================================================
// Décodeur Indeo Video 4.1 (`IV41`) — notre échafaudage, pas une transcription de Blaze.exe.
//
// Traduit de FFmpeg (https://ffmpeg.org), `libavcodec/indeo4.c` et `libavcodec/ivi.c`, révision
// b0d154591161153b0ec55d9527535bc776504899. Copyright (c) 2009-2011 Maxim Poliakovski.
//
// Ce fichier est distribué sous la GNU Lesser General Public License, version 2.1 ou (à votre
// choix) toute version ultérieure, comme les sources dont il est tiré. Voir `LICENSE-LGPL.txt` dans
// ce répertoire, et `LISEZMOI.md` pour ce qui a été porté et comment c'est vérifié.
//
// Il remplace `ir41_32.ax`, le codec d'Intel que le jeu d'origine appelait par DirectShow.
//
// **Ce qu'il rend, c'est du YVU9** — la sortie native d'Indeo : un plan Y plein et deux plans de
// chrominance au quart dans chaque sens —, pas du RGB. C'est à ce niveau qu'il se vérifie à l'octet
// près contre `ir41_32.ax`, qui rend ce même format quand on lui demande `IF09` (son RGB24 est
// tramé) : `OpenForesia.Tools indeo <film.avi>`.
//
// La structure suit FFmpeg fonction pour fonction — les noms d'origine sont rappelés en tête de
// chacune —, y compris ses bizarreries (voir `mcType` qui survit d'un macrobloc à l'autre dans
// `DecodeBlocks`) : c'est la condition pour que l'écart, s'il y en a un, se cherche ligne à ligne.
// =====================================================================

namespace OpenForesia.Blaze;

/// <summary>
/// Une image YVU9 : le plan Y en pleine résolution, puis V et U au quart de chaque dimension, tous
/// rangés de haut en bas, sans marge en fin de ligne.
/// </summary>
internal sealed class Yvu9Image
{
    internal readonly int Width, Height;
    internal readonly int ChromaWidth, ChromaHeight;
    internal readonly byte[] Y, V, U;

    internal Yvu9Image(int width, int height)
    {
        Width = width;
        Height = height;
        // Arrondi au-dessus, comme la chrominance d'Indeo quand une dimension n'est pas multiple
        // de quatre. Les films du jeu font 256×224 : la question ne s'y pose pas.
        ChromaWidth = (width + 3) >> 2;
        ChromaHeight = (height + 3) >> 2;
        Y = new byte[width * height];
        V = new byte[ChromaWidth * ChromaHeight];
        U = new byte[ChromaWidth * ChromaHeight];
    }

    internal void CopyTo(Yvu9Image other)
    {
        Y.CopyTo(other.Y, 0);
        V.CopyTo(other.V, 0);
        U.CopyTo(other.U, 0);
    }
}

/// <summary>IVIMbInfo. Les champs gardent les largeurs de FFmpeg : un vecteur est un <c>int8_t</c>, et il tronque.</summary>
internal struct Indeo4MbInfo
{
    internal short XPos, YPos;
    internal uint BufOffs;
    internal byte Type;      // 0 INTRA, 1 INTER, 2 et 3 : bidirectionnels
    internal byte Cbp;
    internal sbyte QDelta;
    internal sbyte MvX, MvY;
    internal sbyte BMvX, BMvY;
}

internal sealed class Indeo4Tile
{
    internal int XPos, YPos, Width, Height, MbSize;
    internal bool IsEmpty;
    internal int DataSize, NumMbs;
    internal Indeo4MbInfo[] Mbs = [];
    /// <summary>Les macroblocs de la tuile correspondante de la première bande de luminance (vecteurs et quantif. hérités).</summary>
    internal Indeo4MbInfo[]? RefMbs;
}

/// <summary>Table run/valeur (RVMapDesc). Le décodeur en garde une copie, parce qu'une bande peut la corriger.</summary>
internal sealed class Indeo4RvMap
{
    internal byte EobSym, EscSym;
    internal readonly byte[] Run = new byte[256];
    internal readonly sbyte[] Val = new sbyte[256];
}

internal sealed class Indeo4Band
{
    internal int Plane, BandNum;
    internal int Width, Height, AHeight, Pitch, BufSize;
    internal short[]? Buf, RefBuf, BRefBuf;
    internal readonly short[]?[] Bufs = new short[]?[4];
    internal bool IsEmpty;
    internal int MbSize, BlkSize;
    internal int IsHalfpel;
    internal bool InheritMv, InheritQdelta;
    internal int QuantMat, GlobQuant;
    internal byte[]? Scan;
    internal int ScanSize;
    internal readonly Indeo4HuffTab BlkVlc = new();
    internal int NumCorr;
    internal readonly byte[] Corr = new byte[61 * 2];
    internal int RvmapSel;
    internal Indeo4RvMap? RvMap;
    internal Indeo4Tile[] Tiles = [];
    internal int TransformId = -1, TransformSize;
    internal bool Is2dTrans;
    internal bool ChecksumPresent;
    internal int Checksum;
    internal ushort[] IntraBase = [], InterBase = [];
}

internal sealed class Indeo4Plane
{
    internal int Width, Height;
    internal Indeo4Band[] Bands = [];
}

/// <summary>
/// Un flux Indeo 4 en cours de décodage. Les images se décodent **dans l'ordre** : une image non
/// clé se compose sur celles qui la précèdent, que le décodeur garde lui-même.
/// </summary>
internal sealed class Indeo4Decoder
{
    // IVI4_FRAMETYPE_*
    private const int FrameIntra = 0, FrameIntra1 = 1, FrameInter = 2, FrameBidir = 3,
                      FrameInterNoRef = 4, FrameNullFirst = 5, FrameNullLast = 6;
    private const int PicSizeEsc = 7;

    private record struct PicConfig(int PicWidth, int PicHeight, int ChromaWidth, int ChromaHeight,
                                    int TileWidth, int TileHeight, int LumaBands, int ChromaBands);

    internal readonly int Width, Height;

    private Indeo4BitReader _gb = new([], 0, 0);
    private readonly Indeo4RvMap[] _rvmapTabs = new Indeo4RvMap[9];
    private int _frameType, _prevFrameType;
    private int _dataSize, _frameNum;
    private bool _isScalable;
    private PicConfig _picConf;
    private readonly Indeo4Plane[] _planes = { new(), new(), new() };
    private int _dstBuf = 0, _refBuf = 1, _bRefBuf = 3;   // le tampon 2 sert au mode échelonnable
    private readonly Indeo4HuffTab _mbVlc = new(), _blkVlc = new();
    private int _rvmapSel;
    private bool _inImf, _inQ;
    private int _picGlobQuant, _unknown1, _checksum;
    private readonly bool[] _bufInvalid = new bool[4];
    private Yvu9Image? _pFrame;
    private bool _gotPFrame;

    // Ce que FFmpeg affiche une fois en mode verbeux (show_indeo4_info) : on le garde pour l'outil.
    internal bool IsScalable => _isScalable;
    internal bool UsesTiling { get; private set; }
    internal bool HasBFrames { get; private set; }
    internal bool HasTransparency { get; private set; }
    internal bool UsesHaar { get; private set; }
    internal bool UsesFullpel { get; private set; }
    /// <summary>Combien d'images INTRA transportaient une image INTER glissée à leur suite.</summary>
    internal int EmbeddedFrames { get; private set; }

    // Les entrées nulles de `transforms[18]` : DCT et « pas de transformée 4×4 », non gérées.
    private static readonly bool[] s_TransformExists =
        { true, true, true, true, true, true, true, false, false, false, true, true, false, true, true, true, true, false };
    private static readonly bool[] s_TransformIs2d =
        { true, false, false, true, true, true, true, false, false, false, true, true, false, false, false, false, false, false };

    private static readonly byte[][] s_ScanIndexToTab =
    {
        // 8×8
        Indeo4Tables.ZigzagDirect, Indeo4Tables.AlternateScan8x8, Indeo4Tables.HorizontalScan8x8,
        Indeo4Tables.VerticalScan8x8, Indeo4Tables.ZigzagDirect,
        // 4×4
        Indeo4Tables.DirectScan4x4, Indeo4Tables.AlternateScan4x4, Indeo4Tables.VerticalScan4x4,
        Indeo4Tables.HorizontalScan4x4, Indeo4Tables.DirectScan4x4,
        // « TODO: check if those are needed » (sic)
        Indeo4Tables.HorizontalScan8x8, Indeo4Tables.HorizontalScan8x8, Indeo4Tables.HorizontalScan8x8,
        Indeo4Tables.HorizontalScan8x8, Indeo4Tables.HorizontalScan8x8,
    };

    // Tampons de travail de ivi_decode_coded_blocks.
    private readonly int[] _trvec = new int[64];
    private readonly byte[] _colFlags = new byte[8];

    /// <summary>decode_init.</summary>
    internal Indeo4Decoder(int width, int height)
    {
        Width = width;
        Height = height;
        for (int i = 0; i < 9; i++)
        {
            var map = new Indeo4RvMap { EobSym = Indeo4Tables.RvMapEob[i], EscSym = Indeo4Tables.RvMapEsc[i] };
            Indeo4Tables.RvMapRun[i].CopyTo(map.Run, 0);
            Indeo4Tables.RvMapVal[i].CopyTo(map.Val, 0);
            _rvmapTabs[i] = map;
        }
    }

    /// <summary>
    /// Décode l'image compressée suivante du flux dans <paramref name="output"/>. Une image vide
    /// (`NULL_FIRST`) laisse <paramref name="output"/> intact : on garde l'image précédente.
    /// Lève <see cref="InvalidDataException"/> sur un flux que FFmpeg refuserait aussi.
    /// </summary>
    internal void Decode(ReadOnlySpan<byte> frame, Yvu9Image output)
    {
        if (output.Width != Width || output.Height != Height)
            throw new ArgumentException("Indeo 4 : image de sortie aux mauvaises dimensions", nameof(output));
        byte[] data = frame.ToArray();
        DecodeFrame(data, 0, data.Length, output);
    }

    private static int ToSigned(int val) => -((val >> 1) ^ -(val & 1));   // IVI_TOSIGNED

    private static int ScaleMv(int mv, int mvScale) => (mv + (mv > 0 ? 1 : 0) + (mvScale - 1)) >> mvScale;   // ivi_scale_mv

    private static InvalidDataException Invalid(string what) => new("Indeo 4 : " + what);

    // ================================================================== ff_ivi_decode_frame

    /// <summary>Rend vrai si une image a été écrite dans <paramref name="output"/>.</summary>
    private bool DecodeFrame(byte[] data, int start, int length, Yvu9Image output)
    {
        _gb = new Indeo4BitReader(data, start, length);

        DecodePicHeader();

        if (_frameType == FrameNullLast)
        {
            if (!_gotPFrame) return false;
            _pFrame!.CopyTo(output);
            _gotPFrame = false;
            return true;
        }

        if (_planes[0].Bands.Length == 0) throw Invalid("plans de couleur pas encore initialises");

        SwitchBuffers();

        bool nonNull = _frameType < FrameNullFirst;
        if (nonNull)
        {
            _bufInvalid[_dstBuf] = true;
            for (int p = 0; p < 3; p++)
                foreach (Indeo4Band band in _planes[p].Bands)
                    DecodeBand(band);
            _bufInvalid[_dstBuf] = false;
        }
        else
        {
            if (_isScalable) throw Invalid("image vide dans un flux echelonnable");
            for (int p = 0; p < 3; p++)
                if (_planes[p].Bands[0].Buf is null) throw Invalid("image vide sans image precedente");
        }
        if (_bufInvalid[_dstBuf]) throw Invalid("tampon de destination invalide");

        if (!nonNull) return false;

        if (_planes[0].Width != output.Width || _planes[0].Height != output.Height)
            throw Invalid($"l'image fait {_planes[0].Width}x{_planes[0].Height}, pas {output.Width}x{output.Height}");

        if (_isScalable)
        {
            Indeo4Band[] b = _planes[0].Bands;
            Indeo4Dsp.RecomposeHaar(b[0].Buf!, b[1].Buf!, b[2].Buf!, b[3].Buf!, b[0].Pitch,
                                    _planes[0].Width, _planes[0].Height, output.Y, output.Width);
        }
        else
        {
            OutputPlane(_planes[0], output.Y, output.Width);
        }
        // FFmpeg range le plan 2 en U et le plan 1 en V : l'ordre d'Indeo est Y, V, U.
        OutputPlane(_planes[2], output.U, output.ChromaWidth);
        OutputPlane(_planes[1], output.V, output.ChromaWidth);

        // En mode bidirectionnel, une image INTRA porte à sa suite l'image INTER qui la suit
        // (« That's exactly the same Intel decoders do »). Elle est décodée maintenant et rendue
        // à la prochaine image NULL_LAST.
        if (_frameType == FrameIntra)
        {
            // la chaîne de version
            while (_gb.GetBits(8) != 0)
            {
                if (_gb.BitsLeft < 8) return true;   // FFmpeg rend une erreur, l'image est déjà écrite
            }
            int left = _gb.BitsCount & 0x18;
            _gb.Skip(64 - left);
            if (_gb.BitsLeft > 18 && _gb.ShowBits(21) == 0xBFFF8)   // synchro + genre INTER
            {
                int offset = start + (_gb.BitsCount >> 3);
                int size = _gb.BitsLeft >> 3;
                EmbeddedFrames++;
                _gotPFrame = false;
                _pFrame ??= new Yvu9Image(Width, Height);
                // FFmpeg ignore le code de retour de cet appel.
                try { _gotPFrame = DecodeFrame(data, offset, size, _pFrame); }
                catch (InvalidDataException) { _gotPFrame = false; }
            }
        }

        return true;
    }

    /// <summary>ivi_output_plane : on rajoute le biais de 128 retiré à l'encodage, et on sature.</summary>
    private static void OutputPlane(Indeo4Plane plane, byte[] dst, int dstPitch)
    {
        short[]? src = plane.Bands[0].Buf;
        if (src is null) return;
        int pitch = plane.Bands[0].Pitch;
        for (int y = 0; y < plane.Height; y++)
            for (int x = 0; x < plane.Width; x++)
                dst[y * dstPitch + x] = Indeo4Dsp.ClipByte(src[y * pitch + x] + 128);
    }

    /// <summary>switch_buffers.</summary>
    private void SwitchBuffers()
    {
        bool isPrevRef = _prevFrameType is FrameIntra or FrameIntra1 or FrameInter;
        bool isRef = _frameType is FrameIntra or FrameIntra1 or FrameInter;

        if (isPrevRef && isRef)
        {
            (_dstBuf, _refBuf) = (_refBuf, _dstBuf);
        }
        else if (isPrevRef)
        {
            (_refBuf, _bRefBuf) = (_bRefBuf, _refBuf);
            (_dstBuf, _refBuf) = (_refBuf, _dstBuf);
        }
    }

    // ================================================================== en-tête d'image (indeo4.c)

    /// <summary>decode_plane_subdivision : 1 bande (code 3) ou 4 bandes (codes 2, 3, 3, 3, 3) ; 0 sinon.</summary>
    private int DecodePlaneSubdivision()
    {
        switch (_gb.GetBits(2))
        {
            case 3:
                return 1;
            case 2:
                for (int i = 0; i < 4; i++)
                    if (_gb.GetBits(2) != 3) return 0;
                return 4;
            default:
                return 0;
        }
    }

    private static int ScaleTileSize(int defSize, int sizeFactor) => sizeFactor == 15 ? defSize : (sizeFactor + 1) << 5;

    /// <summary>decode_pic_hdr.</summary>
    private void DecodePicHeader()
    {
        if (_gb.GetBits(18) != 0x3FFF8) throw Invalid("code de debut d'image invalide");

        _prevFrameType = _frameType;
        _frameType = (int)_gb.GetBits(3);
        if (_frameType == 7) throw Invalid("genre d'image 7");

        if (_frameType == FrameBidir) HasBFrames = true;

        HasTransparency = _gb.GetBit() != 0;

        // bit inconnu : le décodeur Mac l'ignore, XANIM rend une erreur
        if (_gb.GetBit() != 0) throw Invalid("bit de synchro positionne");

        _dataSize = _gb.GetBit() != 0 ? (int)_gb.GetBits(24) : 0;

        // les images vides ne contiennent rien d'autre
        if (_frameType >= FrameNullFirst) return;

        // mot de verrouillage : ignoré, les films se décodent quand même
        if (_gb.GetBit() != 0) _gb.Skip(32);

        var conf = new PicConfig();
        int picSizeIndx = (int)_gb.GetBits(3);
        if (picSizeIndx == PicSizeEsc)
        {
            conf.PicHeight = (int)_gb.GetBits(16);
            conf.PicWidth = (int)_gb.GetBits(16);
        }
        else
        {
            conf.PicHeight = Indeo4Tables.CommonPicSizes[picSizeIndx * 2 + 1];
            conf.PicWidth = Indeo4Tables.CommonPicSizes[picSizeIndx * 2];
        }

        UsesTiling = _gb.GetBit() != 0;
        if (UsesTiling)
        {
            conf.TileHeight = ScaleTileSize(conf.PicHeight, (int)_gb.GetBits(4));
            conf.TileWidth = ScaleTileSize(conf.PicWidth, (int)_gb.GetBits(4));
        }
        else
        {
            conf.TileHeight = conf.PicHeight;
            conf.TileWidth = conf.PicWidth;
        }

        // sous-échantillonnage de la chrominance : seul YVU9 (4:4) existe
        if (_gb.GetBits(2) != 0) throw Invalid("seul le format YVU9 est gere");
        conf.ChromaHeight = (conf.PicHeight + 3) >> 2;
        conf.ChromaWidth = (conf.PicWidth + 3) >> 2;

        conf.LumaBands = DecodePlaneSubdivision();
        conf.ChromaBands = 0;
        if (conf.LumaBands != 0) conf.ChromaBands = DecodePlaneSubdivision();

        if (conf.PicWidth <= 0 || conf.PicHeight <= 0) throw Invalid("dimensions d'image nulles");

        _isScalable = conf.LumaBands != 1 || conf.ChromaBands != 1;
        if (_isScalable && (conf.LumaBands != 4 || conf.ChromaBands != 1))
            throw Invalid($"subdivision non geree : {conf.LumaBands} bandes de luminance, {conf.ChromaBands} de chrominance");

        // la disposition a changé : on réalloue
        if (conf != _picConf)
        {
            InitPlanes(conf);
            _picConf = conf;

            // tailles de macrobloc et de bloc par défaut
            for (int p = 0; p <= 2; p++)
                foreach (Indeo4Band band in _planes[p].Bands)
                {
                    band.MbSize = p == 0 ? (!_isScalable ? 16 : 8) : 4;
                    band.BlkSize = p == 0 ? 8 : 4;
                }

            InitTiles(conf.TileWidth, conf.TileHeight);
        }

        _frameNum = _gb.GetBit() != 0 ? (int)_gb.GetBits(20) : 0;

        // decTimeEst
        if (_gb.GetBit() != 0) _gb.Skip(8);

        // dictionnaires de Huffman des macroblocs et des blocs
        _mbVlc.Decode(_gb, _gb.GetBit() != 0, block: false);
        _blkVlc.Decode(_gb, _gb.GetBit() != 0, block: true);

        _rvmapSel = _gb.GetBit() != 0 ? (int)_gb.GetBits(3) : 8;

        _inImf = _gb.GetBit() != 0;
        _inQ = _gb.GetBit() != 0;

        _picGlobQuant = (int)_gb.GetBits(5);

        _unknown1 = _gb.GetBit() != 0 ? (int)_gb.GetBits(3) : 0;

        _checksum = _gb.GetBit() != 0 ? (int)_gb.GetBits(16) : 0;

        // extensions d'en-tête
        while (_gb.GetBit() != 0)
        {
            if (_gb.BitsLeft < 10) throw Invalid("extension d'en-tete tronquee");
            _gb.Skip(8);
        }

        // « Bad blocks bits encountered! » : FFmpeg le signale et continue
        _gb.GetBit();

        _gb.Align();
    }

    // ================================================================== plans et tuiles (ivi.c)

    /// <summary>ff_ivi_init_planes.</summary>
    private void InitPlanes(PicConfig cfg)
    {
        if (cfg.LumaBands < 1 || cfg.ChromaBands < 1) throw Invalid("nombre de bandes nul");

        _planes[0].Width = cfg.PicWidth;
        _planes[0].Height = cfg.PicHeight;
        _planes[1].Width = _planes[2].Width = (cfg.PicWidth + 3) >> 2;
        _planes[1].Height = _planes[2].Height = (cfg.PicHeight + 3) >> 2;

        for (int p = 0; p < 3; p++)
        {
            int numBands = p == 0 ? cfg.LumaBands : cfg.ChromaBands;
            var plane = _planes[p];
            plane.Bands = new Indeo4Band[numBands];

            // une seule bande : pleine taille ; plusieurs : moitié
            int bWidth = numBands == 1 ? plane.Width : (plane.Width + 1) >> 1;
            int bHeight = numBands == 1 ? plane.Height : (plane.Height + 1) >> 1;

            // alignées sur la plus grande taille de macrobloc : 16 en luminance, 8 en chrominance
            int alignFac = p != 0 ? 8 : 16;
            int widthAligned = (bWidth + alignFac - 1) & -alignFac;
            int heightAligned = (bHeight + alignFac - 1) & -alignFac;

            for (int b = 0; b < numBands; b++)
            {
                plane.Bands[b] = new Indeo4Band
                {
                    Plane = p,
                    BandNum = b,
                    Width = bWidth,
                    Height = bHeight,
                    Pitch = widthAligned,
                    AHeight = heightAligned,
                    BufSize = widthAligned * heightAligned,
                };
            }
        }
    }

    /// <summary>ff_ivi_init_tiles et ivi_init_tiles.</summary>
    private void InitTiles(int tileWidth, int tileHeight)
    {
        for (int p = 0; p < 3; p++)
        {
            int tWidth = p == 0 ? tileWidth : (tileWidth + 3) >> 2;
            int tHeight = p == 0 ? tileHeight : (tileHeight + 3) >> 2;

            if (p == 0 && _planes[0].Bands.Length == 4)
            {
                if (tWidth % 2 != 0 || tHeight % 2 != 0) throw Invalid("tuiles de taille impaire");
                tWidth >>= 1;
                tHeight >>= 1;
            }
            if (tWidth <= 0 || tHeight <= 0) throw Invalid("tuiles de taille nulle");

            for (int b = 0; b < _planes[p].Bands.Length; b++)
            {
                Indeo4Band band = _planes[p].Bands[b];
                int xTiles = (band.Width + tWidth - 1) / tWidth;
                int yTiles = (band.Height + tHeight - 1) / tHeight;
                band.Tiles = new Indeo4Tile[xTiles * yTiles];

                // la première bande de luminance sert de référence pour les vecteurs et la quantification
                Indeo4Tile[] refTiles = _planes[0].Bands[0].Tiles;
                int t = 0, r = 0;
                for (int y = 0; y < band.Height; y += tHeight)
                {
                    for (int x = 0; x < band.Width; x += tWidth)
                    {
                        var tile = new Indeo4Tile
                        {
                            XPos = x,
                            YPos = y,
                            MbSize = band.MbSize,
                            Width = Math.Min(band.Width - x, tWidth),
                            Height = Math.Min(band.Height - y, tHeight),
                        };
                        tile.NumMbs = MbsPerTile(tile.Width, tile.Height, band.MbSize);
                        tile.Mbs = new Indeo4MbInfo[tile.NumMbs];

                        if (p != 0 || b != 0)
                        {
                            if (tile.NumMbs != refTiles[r].NumMbs) throw Invalid("tuile de reference incompatible");
                            tile.RefMbs = refTiles[r].Mbs;
                            r++;
                        }
                        band.Tiles[t++] = tile;
                    }
                }
            }
        }
    }

    private static int MbsPerTile(int w, int h, int mbSize) => ((w + mbSize - 1) / mbSize) * ((h + mbSize - 1) / mbSize);

    // ================================================================== bandes

    private short[]? PrepareBuf(Indeo4Band band, int i)
    {
        if (_picConf.LumaBands <= 1 && i == 2) return null;
        return band.Bufs[i] ??= new short[band.BufSize];
    }

    /// <summary>decode_band.</summary>
    private void DecodeBand(Indeo4Band band)
    {
        band.Buf = PrepareBuf(band, _dstBuf) ?? throw Invalid("tampon de bande absent");
        if (_frameType == FrameBidir)
        {
            band.RefBuf = PrepareBuf(band, _bRefBuf);
            band.BRefBuf = PrepareBuf(band, _refBuf) ?? throw Invalid("seconde reference absente");
        }
        else
        {
            band.RefBuf = PrepareBuf(band, _refBuf);
            band.BRefBuf = null;
        }
        if (band.RefBuf is null) throw Invalid("reference absente");

        DecodeBandHeader(band);

        if (band.IsEmpty) throw Invalid("bande vide");

        band.RvMap = _rvmapTabs[band.RvmapSel];

        // corrections de la table run/valeur, défaites en ordre inverse à la fin
        for (int i = 0; i < band.NumCorr; i++)
            SwapRvMap(band.RvMap, band.Corr[i * 2], band.Corr[i * 2 + 1]);

        try
        {
            int pos = _gb.BitsCount;
            int mvScale = (_planes[0].Bands[0].MbSize >> 3) - (band.MbSize >> 3);

            foreach (Indeo4Tile tile in band.Tiles)
            {
                if (tile.MbSize != band.MbSize || _planes[0].Bands[0].MbSize < band.MbSize)
                    throw Invalid($"tailles de macrobloc incompatibles : {band.MbSize} / {tile.MbSize} / {_planes[0].Bands[0].MbSize}");

                tile.IsEmpty = _gb.GetBit() != 0;
                if (tile.IsEmpty)
                {
                    ProcessEmptyTile(band, tile, mvScale);
                }
                else
                {
                    tile.DataSize = DecodeTileDataSize();
                    if (tile.DataSize == 0) throw Invalid("tuile de taille nulle");

                    DecodeMbInfo(band, tile);
                    DecodeBlocks(band, tile);

                    if (((_gb.BitsCount - pos) >> 3) != tile.DataSize) throw Invalid("taille de tuile incoherente");

                    pos += tile.DataSize << 3;
                }
            }
        }
        finally
        {
            for (int i = band.NumCorr - 1; i >= 0; i--)
                SwapRvMap(band.RvMap, band.Corr[i * 2], band.Corr[i * 2 + 1]);
        }

        _gb.Align();
    }

    private static void SwapRvMap(Indeo4RvMap map, int idx1, int idx2)
    {
        (map.Run[idx1], map.Run[idx2]) = (map.Run[idx2], map.Run[idx1]);
        (map.Val[idx1], map.Val[idx2]) = (map.Val[idx2], map.Val[idx1]);
        if (idx1 == map.EobSym || idx2 == map.EobSym) map.EobSym ^= (byte)(idx1 ^ idx2);
        if (idx1 == map.EscSym || idx2 == map.EscSym) map.EscSym ^= (byte)(idx1 ^ idx2);
    }

    /// <summary>ivi_dec_tile_data_size : un octet, ou 0xFF suivi de 24 bits.</summary>
    private int DecodeTileDataSize()
    {
        int len = 0;
        if (_gb.GetBit() != 0)
        {
            len = (int)_gb.GetBits(8);
            if (len == 255) len = (int)_gb.GetBits(24);
        }
        _gb.Align();
        return len;
    }

    /// <summary>decode_band_hdr (indeo4.c).</summary>
    private void DecodeBandHeader(Indeo4Band band)
    {
        int plane = (int)_gb.GetBits(2);
        int bandNum = (int)_gb.GetBits(4);
        if (band.Plane != plane || band.BandNum != bandNum) throw Invalid("sequence d'en-tetes de bande invalide");

        band.IsEmpty = _gb.GetBit() != 0;
        if (!band.IsEmpty)
        {
            int oldBlkSize = band.BlkSize;

            // taille de l'en-tête : 4 octets si elle n'est pas donnée
            if (_gb.GetBit() != 0) _gb.Skip(16);

            band.IsHalfpel = (int)_gb.GetBits(2);
            if (band.IsHalfpel >= 2) throw Invalid($"resolution de vecteur non geree : {band.IsHalfpel}");
            if (band.IsHalfpel == 0) UsesFullpel = true;

            band.ChecksumPresent = _gb.GetBit() != 0;
            if (band.ChecksumPresent) band.Checksum = (int)_gb.GetBits(16);

            int indx = (int)_gb.GetBits(2);
            if (indx == 3) throw Invalid("taille de bloc invalide");
            band.MbSize = 16 >> indx;
            band.BlkSize = 8 >> (indx >> 1);

            band.InheritMv = _gb.GetBit() != 0;
            band.InheritQdelta = _gb.GetBit() != 0;

            band.GlobQuant = (int)_gb.GetBits(5);

            // Le bit est TOUJOURS lu, même sur une image INTRA.
            bool keepTransform = _gb.GetBit() != 0;
            if (!keepTransform || _frameType == FrameIntra)
            {
                int transformId = (int)_gb.GetBits(5);
                if (transformId >= s_TransformExists.Length || !s_TransformExists[transformId])
                    throw Invalid($"transformee {transformId} non geree");
                if (transformId < 10 && band.BlkSize < 8) throw Invalid("taille de transformee incorrecte");
                if (transformId is >= 0 and <= 2 or 10) UsesHaar = true;

                band.TransformId = transformId;
                band.Is2dTrans = s_TransformIs2d[transformId];
                band.TransformSize = transformId < 10 ? 8 : 4;

                if (band.BlkSize != band.TransformSize)
                    throw Invalid($"transformee et bloc de tailles differentes ({band.TransformSize} != {band.BlkSize})");

                int scanIndx = (int)_gb.GetBits(4);
                if (scanIndx == 15) throw Invalid("balayage transmis dans le flux");
                if (scanIndx > 4 && scanIndx < 10)
                {
                    if (band.BlkSize != 4) throw Invalid("balayage incompatible");
                }
                else if (band.BlkSize != 8)
                {
                    throw Invalid("balayage incompatible");
                }

                band.Scan = s_ScanIndexToTab[scanIndx];
                band.ScanSize = band.BlkSize;

                int quantMat = (int)_gb.GetBits(5);
                if (quantMat == 31) throw Invalid("matrice de quantification transmise dans le flux");
                if (quantMat >= Indeo4Tables.QuantIndexToTab.Length) throw Invalid($"matrice de quantification {quantMat}");
                band.QuantMat = quantMat;
            }
            else if (oldBlkSize != band.BlkSize)
            {
                throw Invalid("taille de bloc differente de la configuration heritee");
            }

            if (Indeo4Tables.QuantIndexToTab[band.QuantMat] > 4 && band.BlkSize == 4)
            {
                band.QuantMat = 0;
                throw Invalid("matrice de quantification invalide pour un bloc 4x4");
            }
            if (band.ScanSize != band.BlkSize) throw Invalid("balayage incompatible");
            if (band.TransformSize == 8 && band.BlkSize < 8) throw Invalid("taille de transformee incompatible");

            // dictionnaire des blocs propre à la bande, ou celui de l'image
            if (_gb.GetBit() == 0)
                band.BlkVlc.Tab = _blkVlc.Tab;
            else
                band.BlkVlc.Decode(_gb, descCoded: true, block: true);

            band.RvmapSel = _gb.GetBit() != 0 ? (int)_gb.GetBits(3) : 8;

            band.NumCorr = 0;
            if (_gb.GetBit() != 0)
            {
                band.NumCorr = (int)_gb.GetBits(8);
                if (band.NumCorr > 61) throw Invalid($"trop de corrections : {band.NumCorr}");
                for (int i = 0; i < band.NumCorr * 2; i++) band.Corr[i] = (byte)_gb.GetBits(8);
            }
        }

        int tab = Indeo4Tables.QuantIndexToTab[band.QuantMat];
        if (band.BlkSize == 8)
        {
            band.IntraBase = Indeo4Tables.Quant8x8Intra[tab];
            band.InterBase = Indeo4Tables.Quant8x8Inter[tab];
        }
        else
        {
            band.IntraBase = Indeo4Tables.Quant4x4Intra[tab];
            band.InterBase = Indeo4Tables.Quant4x4Inter[tab];
        }

        _gb.Align();

        if (band.Scan is null) throw Invalid("balayage non defini");
    }

    // ================================================================== macroblocs (indeo4.c)

    /// <summary>decode_mb_info : genre, blocs codés, delta de quantification, vecteur, pour chaque macrobloc de la tuile.</summary>
    private void DecodeMbInfo(Indeo4Band band, Indeo4Tile tile)
    {
        int rowOffset = band.MbSize * band.Pitch;
        Indeo4MbInfo[] mbs = tile.Mbs;
        Indeo4MbInfo[]? refMbs = tile.RefMbs;
        int m = 0;
        int offs = tile.YPos * band.Pitch + tile.XPos;

        int blksPerMb = band.MbSize != band.BlkSize ? 4 : 1;
        int mbTypeBits = _frameType == FrameBidir ? 2 : 1;

        // facteur d'échelle des vecteurs
        int mvScale = (_planes[0].Bands[0].MbSize >> 3) - (band.MbSize >> 3);
        int mvX = 0, mvY = 0;

        if (MbsPerTile(tile.Width, tile.Height, band.MbSize) != tile.NumMbs) throw Invalid("nombre de macroblocs incoherent");

        bool firstBand = band.Plane == 0 && band.BandNum == 0;

        for (int y = tile.YPos; y < tile.YPos + tile.Height; y += band.MbSize)
        {
            int mbOffset = offs;

            for (int x = tile.XPos; x < tile.XPos + tile.Width; x += band.MbSize, m++, mbOffset += band.MbSize)
            {
                ref Indeo4MbInfo mb = ref mbs[m];
                mb.XPos = (short)x;
                mb.YPos = (short)y;
                mb.BufOffs = (uint)mbOffset;
                mb.BMvX = mb.BMvY = 0;

                if (_gb.BitsLeft < 1) throw Invalid("donnees de macrobloc tronquees");

                if (_gb.GetBit() != 0)
                {
                    if (_frameType == FrameIntra) throw Invalid("macrobloc vide dans une image INTRA");
                    mb.Type = 1;   // un macrobloc vide est toujours INTER
                    mb.Cbp = 0;

                    mb.QDelta = 0;
                    if (firstBand && _inQ)
                    {
                        mb.QDelta = (sbyte)_gb.GetVlc(_mbVlc.Tab!);
                        mb.QDelta = (sbyte)ToSigned(mb.QDelta);
                    }

                    mb.MvX = mb.MvY = 0;
                    if (band.InheritMv && refMbs is not null)
                        InheritMv(ref mb, refMbs[m], mvScale);
                }
                else
                {
                    if (band.InheritMv)
                    {
                        if (refMbs is null) throw Invalid("macrobloc de reference absent");
                        mb.Type = refMbs[m].Type;
                    }
                    else if (_frameType is FrameIntra or FrameIntra1)
                    {
                        mb.Type = 0;
                    }
                    else
                    {
                        mb.Type = (byte)_gb.GetBits(mbTypeBits);
                    }

                    mb.Cbp = (byte)_gb.GetBits(blksPerMb);

                    mb.QDelta = 0;
                    if (band.InheritQdelta)
                    {
                        if (refMbs is not null) mb.QDelta = refMbs[m].QDelta;
                    }
                    else if (mb.Cbp != 0 || (firstBand && _inQ))
                    {
                        mb.QDelta = (sbyte)_gb.GetVlc(_mbVlc.Tab!);
                        mb.QDelta = (sbyte)ToSigned(mb.QDelta);
                    }

                    if (mb.Type == 0)
                    {
                        mb.MvX = mb.MvY = 0;   // pas de vecteur dans un macrobloc intra
                    }
                    else
                    {
                        if (band.InheritMv)
                        {
                            if (refMbs is not null) InheritMv(ref mb, refMbs[m], mvScale);
                        }
                        else
                        {
                            // les vecteurs sont codés en différence du précédent de la tuile
                            mvY += ToSigned(_gb.GetVlc(_mbVlc.Tab!));
                            mvX += ToSigned(_gb.GetVlc(_mbVlc.Tab!));
                            mb.MvX = (sbyte)mvX;
                            mb.MvY = (sbyte)mvY;
                            if (mb.Type == 3)
                            {
                                mvY += ToSigned(_gb.GetVlc(_mbVlc.Tab!));
                                mvX += ToSigned(_gb.GetVlc(_mbVlc.Tab!));
                                mb.BMvX = (sbyte)-mvX;
                                mb.BMvY = (sbyte)-mvY;
                            }
                        }
                        if (mb.Type == 2)
                        {
                            mb.BMvX = (sbyte)-mb.MvX;
                            mb.BMvY = (sbyte)-mb.MvY;
                            mb.MvX = 0;
                            mb.MvY = 0;
                        }
                    }
                }

                int s = band.IsHalfpel;
                if (mb.Type != 0)
                {
                    if (x + (mb.MvX >> s) + (y + (mb.MvY >> s)) * band.Pitch < 0 ||
                        x + ((mb.MvX + s) >> s) + band.MbSize - 1
                          + (y + band.MbSize - 1 + ((mb.MvY + s) >> s)) * band.Pitch > band.BufSize - 1)
                        throw Invalid($"vecteur {mb.MvX} {mb.MvY} hors de la reference");
                }
            }

            offs += rowOffset;
        }

        _gb.Align();
    }

    private static void InheritMv(ref Indeo4MbInfo mb, in Indeo4MbInfo refMb, int mvScale)
    {
        if (mvScale != 0)
        {
            mb.MvX = (sbyte)ScaleMv(refMb.MvX, mvScale);
            mb.MvY = (sbyte)ScaleMv(refMb.MvY, mvScale);
        }
        else
        {
            mb.MvX = refMb.MvX;
            mb.MvY = refMb.MvY;
        }
    }

    // ================================================================== blocs (ivi.c)

    /// <summary>ivi_decode_blocks.</summary>
    private void DecodeBlocks(Indeo4Band band, Indeo4Tile tile)
    {
        // Déclarés hors de la boucle, comme chez FFmpeg : `mcType` et `mcType2` survivent d'un
        // macrobloc au suivant quand rien ne les réécrit.
        int mcType = 0, mcType2 = -1;
        int mvX = 0, mvY = 0, mvX2 = 0, mvY2 = 0;
        int prevDc = 0;   // prédiction intra du coefficient continu
        int blkSize = band.BlkSize;
        int numBlocks = band.MbSize != blkSize ? 4 : 1;

        for (int mbn = 0; mbn < tile.NumMbs; mbn++)
        {
            ref Indeo4MbInfo mb = ref tile.Mbs[mbn];
            bool isIntra = mb.Type == 0;
            int cbp = mb.Cbp;
            int bufOffs = (int)mb.BufOffs;

            // av_clip_uintp2(quant, 5)
            int quant = band.GlobQuant + mb.QDelta;
            quant = quant < 0 ? 0 : quant > 31 ? 31 : quant;

            if (!isIntra)
            {
                mvX = mb.MvX;
                mvY = mb.MvY;
                mvX2 = mb.BMvX;
                mvY2 = mb.BMvY;
                if (band.IsHalfpel != 0)
                {
                    mcType = ((mvY & 1) << 1) | (mvX & 1);
                    mcType2 = ((mvY2 & 1) << 1) | (mvX2 & 1);
                    mvX >>= 1;
                    mvY >>= 1;
                    mvX2 >>= 1;
                    mvY2 >>= 1;   // demi-pixel -> pixel
                }
                if (mb.Type == 2) mcType = -1;
                if (mb.Type != 2 && mb.Type != 3) mcType2 = -1;
                if (mb.Type != 0) CheckMv(band, mb.XPos, mb.YPos, mb.MvX, mb.MvY);
                if (mb.Type is 2 or 3) CheckMv(band, mb.XPos, mb.YPos, mb.BMvX, mb.BMvY);
            }

            for (int blk = 0; blk < numBlocks; blk++)
            {
                // position du bloc dans le macrobloc
                if ((blk & 1) != 0)
                {
                    bufOffs += blkSize;
                }
                else if (blk == 2)
                {
                    bufOffs -= blkSize;
                    bufOffs += blkSize * band.Pitch;
                }

                if ((cbp & 1) != 0)
                {
                    DecodeCodedBlock(band, mvX, mvY, mvX2, mvY2, ref prevDc, isIntra, mcType, mcType2, quant, bufOffs);
                }
                else
                {
                    int bufSize = band.Pitch * band.AHeight - bufOffs;
                    int minSize = (blkSize - 1) * band.Pitch + blkSize;
                    if (minSize > bufSize) throw Invalid("bloc hors du tampon");

                    // bloc non codé : intra, la transformée du seul DC ; inter, la compensation seule
                    if (isIntra)
                        DcTransform(band, prevDc, bufOffs, blkSize);
                    else
                        Mc(band, bufOffs, mvX, mvY, mvX2, mvY2, mcType, mcType2, delta: false);
                }

                cbp >>= 1;
            }
        }

        _gb.Align();
    }

    private static void CheckMv(Indeo4Band band, int xpos, int ypos, int mvX, int mvY)
    {
        int dmvX = mvX >> band.IsHalfpel;
        int dmvY = mvY >> band.IsHalfpel;
        int cx = mvX & band.IsHalfpel;
        int cy = mvY & band.IsHalfpel;
        if (xpos + dmvX < 0 || xpos + dmvX + band.MbSize + cx > band.Pitch ||
            ypos + dmvY < 0 || ypos + dmvY + band.MbSize + cy > band.AHeight)
            throw Invalid("vecteur hors de la bande");
    }

    /// <summary>ivi_decode_coded_blocks : coefficients, déquantification, transformée, compensation.</summary>
    private void DecodeCodedBlock(Indeo4Band band, int mvX, int mvY, int mvX2, int mvY2, ref int prevDc,
                                  bool isIntra, int mcType, int mcType2, int quant, int offs)
    {
        ushort[] baseTab = isIntra ? band.IntraBase : band.InterBase;
        Indeo4RvMap rvmap = band.RvMap!;
        Indeo4Vlc vlc = band.BlkVlc.Tab!;
        int[] trvec = _trvec;
        byte[] colFlags = _colFlags;
        int blkSize = band.BlkSize;
        int numCoeffs = blkSize * blkSize;
        int colMask = blkSize - 1;
        int scanPos = -1;
        int minSize = band.Pitch * (band.TransformSize - 1) + band.TransformSize;
        int bufSize = band.Pitch * band.AHeight - offs;
        int sym = 0;

        if (minSize > bufSize) throw Invalid("bloc hors du tampon");
        byte[] scan = band.Scan ?? throw Invalid("balayage non defini");

        Array.Clear(trvec, 0, numCoeffs);
        Array.Clear(colFlags);

        while (scanPos <= numCoeffs)
        {
            sym = _gb.GetVlc(vlc);
            if (sym == rvmap.EobSym) break;   // fin de bloc

            int run, val;
            if (sym == rvmap.EscSym)
            {
                // échappement : run et valeur codés explicitement en trois codes
                run = _gb.GetVlc(vlc) + 1;
                int lo = _gb.GetVlc(vlc);
                int hi = _gb.GetVlc(vlc);
                val = ToSigned((hi << 6) | lo);
            }
            else
            {
                if ((uint)sym >= 256u) throw Invalid($"symbole invalide : {sym}");
                run = rvmap.Run[sym];
                val = rvmap.Val[sym];
            }

            // dé-zigzag et déquantification
            scanPos += run;
            if (scanPos >= numCoeffs || scanPos < 0) break;
            int pos = scan[scanPos];

            int q = (baseTab[pos] * quant) >> 9;
            if (q > 1)
                val = val * q + (val > 0 ? 1 : -1) * (((q ^ 1) - 1) >> 1);   // FFSIGN(0) vaut -1
            trvec[pos] = val;
            if (val != 0) colFlags[pos & colMask] = 1;
        }

        if (scanPos < 0 || (scanPos >= numCoeffs && sym != rvmap.EobSym)) throw Invalid("donnees de bloc corrompues");

        // prédiction du DC des blocs intra
        if (isIntra && band.Is2dTrans)
        {
            prevDc += trvec[0];
            trvec[0] = prevDc;
            if (prevDc != 0) colFlags[0] = 1;
        }

        if (band.TransformSize > band.BlkSize) throw Invalid("transformee trop grande");

        InverseTransform(band, trvec, offs, colFlags);

        if (!isIntra) Mc(band, offs, mvX, mvY, mvX2, mvY2, mcType, mcType2, delta: true);
    }

    private static void InverseTransform(Indeo4Band band, int[] input, int o, byte[] flags)
    {
        short[] output = band.Buf!;
        int pitch = band.Pitch;
        switch (band.TransformId)
        {
            case 0: Indeo4Dsp.InverseHaar8x8(input, output, o, pitch, flags); break;
            case 1: Indeo4Dsp.RowHaar8(input, output, o, pitch, flags); break;
            case 2: Indeo4Dsp.ColHaar8(input, output, o, pitch, flags); break;
            case 3: Indeo4Dsp.PutPixels8x8(input, output, o, pitch, flags); break;
            case 4: Indeo4Dsp.InverseSlant8x8(input, output, o, pitch, flags); break;
            case 5: Indeo4Dsp.RowSlant8(input, output, o, pitch, flags); break;
            case 6: Indeo4Dsp.ColSlant8(input, output, o, pitch, flags); break;
            case 10: Indeo4Dsp.InverseHaar4x4(input, output, o, pitch, flags); break;
            case 11: Indeo4Dsp.InverseSlant4x4(input, output, o, pitch, flags); break;
            case 13: Indeo4Dsp.RowHaar4(input, output, o, pitch, flags); break;
            case 14: Indeo4Dsp.ColHaar4(input, output, o, pitch, flags); break;
            case 15: Indeo4Dsp.RowSlant4(input, output, o, pitch, flags); break;
            case 16: Indeo4Dsp.ColSlant4(input, output, o, pitch, flags); break;
            default: throw Invalid($"transformee {band.TransformId} non definie");
        }
    }

    private static void DcTransform(Indeo4Band band, int dc, int o, int blkSize)
    {
        short[] output = band.Buf!;
        int pitch = band.Pitch;
        switch (band.TransformId)
        {
            case 0: case 1: case 2: case 10: case 13: case 14:
                Indeo4Dsp.DcHaar2d(dc, output, o, pitch, blkSize); break;
            case 3: Indeo4Dsp.PutDcPixel8x8(dc, output, o, pitch, blkSize); break;
            case 4: case 11: Indeo4Dsp.DcSlant2d(dc, output, o, pitch, blkSize); break;
            case 5: case 15: Indeo4Dsp.DcRowSlant(dc, output, o, pitch, blkSize); break;
            case 6: case 16: Indeo4Dsp.DcColSlant(dc, output, o, pitch, blkSize); break;
            default: throw Invalid($"transformee {band.TransformId} non definie");
        }
    }

    /// <summary>ivi_mc : compensation de mouvement d'un bloc, sur une ou deux références.</summary>
    private static void Mc(Indeo4Band band, int offs, int mvX, int mvY, int mvX2, int mvY2,
                           int mcType, int mcType2, bool delta)
    {
        int size = band.BlkSize;
        int pitch = band.Pitch;
        int refOffs = offs + mvY * pitch + mvX;
        int bufSize = pitch * band.AHeight;
        int minSize = pitch * (size - 1) + size;
        int refSize = (mcType > 1 ? 1 : 0) * pitch + (mcType & 1);

        if (mcType != -1)
        {
            if (offs < 0 || refOffs < 0 || band.RefBuf is null ||
                bufSize - minSize < offs || bufSize - minSize - refSize < refOffs)
                throw Invalid("compensation hors du tampon");
        }

        if (mcType2 == -1)
        {
            // mcType == -1 ici ne fait rien : le switch de IVI_MC_TEMPLATE n'a pas de cas pour lui
            if (mcType != -1)
                Indeo4Dsp.Mc(band.Buf!, offs, pitch, band.RefBuf!, refOffs, pitch, mcType, size, delta);
            return;
        }

        int refOffs2 = offs + mvY2 * pitch + mvX2;
        int refSize2 = (mcType2 > 1 ? 1 : 0) * pitch + (mcType2 & 1);
        if (offs < 0 || refOffs2 < 0 || band.BRefBuf is null) throw Invalid("seconde reference absente");
        if (bufSize - minSize - refSize2 < refOffs2) throw Invalid("compensation hors du tampon");

        if (mcType == -1)
            Indeo4Dsp.Mc(band.Buf!, offs, pitch, band.BRefBuf, refOffs2, pitch, mcType2, size, delta);
        else
            Indeo4Dsp.McAvg(band.Buf!, offs, band.RefBuf!, refOffs, band.BRefBuf, refOffs2, pitch, mcType, mcType2, size, delta);
    }

    /// <summary>ivi_process_empty_tile : une tuile sans données se recopie de la référence, ou se compense si des vecteurs sont hérités.</summary>
    private void ProcessEmptyTile(Indeo4Band band, Indeo4Tile tile, int mvScale)
    {
        bool clearFirst = band.Plane == 0 && band.BandNum == 0;   // !qdelta_present (Indeo 5 seulement)
        int mbSize = band.MbSize;
        int xend = tile.XPos + tile.Width;
        int pitch = band.Pitch;

        if (tile.NumMbs != MbsPerTile(tile.Width, tile.Height, mbSize)) throw Invalid("taille de tuile vide incoherente");

        int offs = tile.YPos * pitch + tile.XPos;
        Indeo4MbInfo[]? refMbs = tile.RefMbs;
        int rowOffset = mbSize * pitch;
        bool needMc = false;
        int m = 0;

        for (int y = tile.YPos; y < tile.YPos + tile.Height; y += mbSize)
        {
            int mbOffset = offs;
            for (int x = tile.XPos; x < xend; x += mbSize, m++, mbOffset += mbSize)
            {
                ref Indeo4MbInfo mb = ref tile.Mbs[m];
                mb.XPos = (short)x;
                mb.YPos = (short)y;
                mb.BufOffs = (uint)mbOffset;
                mb.Type = 1;
                mb.Cbp = 0;

                if (clearFirst)
                {
                    mb.QDelta = (sbyte)band.GlobQuant;
                    mb.MvX = 0;
                    mb.MvY = 0;
                }

                if (refMbs is not null)
                {
                    if (band.InheritQdelta) mb.QDelta = refMbs[m].QDelta;

                    if (band.InheritMv)
                    {
                        InheritMv(ref mb, refMbs[m], mvScale);
                        needMc |= mb.MvX != 0 || mb.MvY != 0;
                        CheckMv(band, mb.XPos, mb.YPos, mb.MvX, mb.MvY);
                    }
                }
            }
            offs += rowOffset;
        }

        if (band.InheritMv && needMc)
        {
            int numBlocks = mbSize != band.BlkSize ? 4 : 1;
            for (int mbn = 0; mbn < tile.NumMbs; mbn++)
            {
                ref Indeo4MbInfo mb = ref tile.Mbs[mbn];
                int mvX = mb.MvX, mvY = mb.MvY, mcType;
                if (band.IsHalfpel == 0)
                {
                    mcType = 0;
                }
                else
                {
                    mcType = ((mvY & 1) << 1) | (mvX & 1);
                    mvX >>= 1;
                    mvY >>= 1;
                }

                for (int blk = 0; blk < numBlocks; blk++)
                {
                    int o = (int)mb.BufOffs + band.BlkSize * ((blk & 1) + ((blk & 2) != 0 ? 1 : 0) * pitch);
                    Mc(band, o, mvX, mvY, 0, 0, mcType, -1, delta: false);
                }
            }
        }
        else
        {
            // recopie de la tuile de référence
            short[] src = band.RefBuf!, dst = band.Buf!;
            int o = tile.YPos * pitch + tile.XPos;
            for (int y = 0; y < tile.Height; y++, o += pitch)
                Array.Copy(src, o, dst, o, tile.Width);
        }
    }
}
