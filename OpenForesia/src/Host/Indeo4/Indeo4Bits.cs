// =====================================================================
// Décodeur Indeo 4 — lecteur de bits et codes de Huffman.
//
// Traduit de FFmpeg (https://ffmpeg.org), `libavcodec/ivi.c` (ivi_create_huff_from_desc,
// ff_ivi_dec_huff_desc) et du comportement de `get_bits.h` / `vlc.c` en mode petit-boutiste,
// révision b0d154591161153b0ec55d9527535bc776504899. Copyright (c) 2009-2011 Maxim Poliakovski.
//
// Ce fichier est distribué sous la GNU Lesser General Public License, version 2.1 ou (à votre
// choix) toute version ultérieure, comme la source dont il est tiré. Voir `LICENSE-LGPL.txt` dans
// ce répertoire.
//
// **Indeo lit ses bits poids faible en tête** (`BITSTREAM_READER_LE`) : le premier bit lu est le
// bit 0 du premier octet, et un champ de n bits a son premier bit lu en bit 0. Les codes de Huffman,
// eux, sont décrits poids FORT en tête (le mot `prefix | j` se lit de son bit haut vers son bit
// bas) : la table d'accès est donc indexée par le code **retourné**. C'est ce que fait FFmpeg avec
// `VLC_INIT_OUTPUT_LE`.
// =====================================================================

namespace OpenForesia.Blaze;

/// <summary>Lecteur de bits petit-boutiste. Au-delà de la fin, il lit des zéros.</summary>
internal sealed class Indeo4BitReader
{
    private readonly byte[] _data;
    private readonly int _start, _sizeBits;
    private int _pos;

    internal Indeo4BitReader(byte[] data, int start, int length)
    {
        _data = data;
        _start = start;
        _sizeBits = length * 8;
    }

    internal int BitsCount => _pos;
    internal int BitsLeft => _sizeBits - _pos;

    private int ByteAt(int index)
    {
        int i = _start + index;
        return index < _sizeBits / 8 && i < _data.Length ? _data[i] : 0;
    }

    /// <summary>Les <paramref name="n"/> bits suivants (n ≤ 25), sans avancer.</summary>
    internal uint ShowBits(int n)
    {
        int b = _pos >> 3;
        uint word = (uint)(ByteAt(b) | ByteAt(b + 1) << 8 | ByteAt(b + 2) << 16 | ByteAt(b + 3) << 24);
        return (word >> (_pos & 7)) & (uint)((1L << n) - 1);
    }

    internal uint GetBits(int n)
    {
        if (n == 0) return 0;
        if (n > 25)
        {
            uint low = GetBits(16);
            return low | GetBits(n - 16) << 16;
        }
        uint v = ShowBits(n);
        _pos += n;
        return v;
    }

    internal int GetBit() => (int)GetBits(1);

    internal void Skip(int n) => _pos += n;

    internal void Align() => _pos = (_pos + 7) & ~7;

    /// <summary>get_vlc2(…, IVI_VLC_BITS, 1) : rend le symbole, ou -1 sur un code inconnu (sans avancer).</summary>
    internal int GetVlc(Indeo4Vlc vlc)
    {
        int index = (int)ShowBits(Indeo4Vlc.Bits);
        int len = vlc.Length[index];
        if (len == 0) return -1;
        _pos += len;
        return vlc.Symbol[index];
    }
}

/// <summary>Une table de Huffman d'Indeo, prête à l'accès direct sur 13 bits.</summary>
internal sealed class Indeo4Vlc
{
    internal const int Bits = 13;   // IVI_VLC_BITS
    internal readonly short[] Symbol = new short[1 << Bits];
    internal readonly byte[] Length = new byte[1 << Bits];

    /// <summary>
    /// ivi_create_huff_from_desc : la ligne i du dictionnaire porte 2^xbits[i] codes, préfixés de
    /// i bits à 1, puis d'un 0 sauf sur la dernière ligne.
    /// </summary>
    internal static Indeo4Vlc FromDescriptor(ReadOnlySpan<byte> xbits)
    {
        var vlc = new Indeo4Vlc();
        int pos = 0;
        for (int i = 0; i < xbits.Length; i++)
        {
            int codesPerRow = 1 << xbits[i];
            int notLastRow = i != xbits.Length - 1 ? 1 : 0;
            int prefix = ((1 << i) - 1) << (xbits[i] + notLastRow);

            for (int j = 0; j < codesPerRow; j++)
            {
                if (pos >= 256) break;   // seul Indeo 5 dépasse ; 256 codes au plus

                int bits = i + xbits[i] + notLastRow;
                if (bits > Bits)
                    throw new InvalidDataException("Indeo 4 : dictionnaire de Huffman invalide (code de plus de 13 bits)");
                int code = prefix | j;
                if (bits == 0) bits = 1;

                // Le code se lit de son bit haut vers son bit bas ; dans l'index petit-boutiste, son
                // premier bit est le bit 0. On le retourne.
                int reversed = 0;
                for (int k = 0; k < bits; k++)
                    if ((code & (1 << (bits - 1 - k))) != 0) reversed |= 1 << k;

                for (int fill = 0; fill < 1 << (Bits - bits); fill++)
                {
                    int index = reversed | fill << bits;
                    vlc.Symbol[index] = (short)pos;
                    vlc.Length[index] = (byte)bits;
                }
                pos++;
            }
        }
        return vlc;
    }

    private static readonly Indeo4Vlc[] s_Mb = Build(Indeo4Tables.MbHuffDesc);
    private static readonly Indeo4Vlc[] s_Blk = Build(Indeo4Tables.BlkHuffDesc);

    private static Indeo4Vlc[] Build(byte[][] descriptors)
    {
        var tables = new Indeo4Vlc[descriptors.Length];
        for (int i = 0; i < tables.Length; i++) tables[i] = FromDescriptor(descriptors[i]);
        return tables;
    }

    internal static Indeo4Vlc Static(bool block, int index) => block ? s_Blk[index] : s_Mb[index];
}

/// <summary>IVIHuffTab : l'une des huit tables prédéfinies, ou une table transmise dans le flux.</summary>
internal sealed class Indeo4HuffTab
{
    internal int TabSel;
    internal Indeo4Vlc? Tab;
    internal byte[] CustomDesc = [];
    internal Indeo4Vlc? CustomTab;

    /// <summary>ff_ivi_dec_huff_desc.</summary>
    internal void Decode(Indeo4BitReader gb, bool descCoded, bool block)
    {
        if (!descCoded)
        {
            Tab = Indeo4Vlc.Static(block, 7);
            return;
        }

        TabSel = (int)gb.GetBits(3);
        if (TabSel != 7)
        {
            Tab = Indeo4Vlc.Static(block, TabSel);
            return;
        }

        int numRows = (int)gb.GetBits(4);
        if (numRows == 0) throw new InvalidDataException("Indeo 4 : table de Huffman transmise vide");
        var desc = new byte[numRows];
        for (int i = 0; i < numRows; i++) desc[i] = (byte)gb.GetBits(4);

        // Même table que la fois précédente : on garde celle qui est construite.
        if (CustomTab is null || !desc.AsSpan().SequenceEqual(CustomDesc))
        {
            CustomDesc = desc;
            CustomTab = Indeo4Vlc.FromDescriptor(desc);
        }
        Tab = CustomTab;
    }
}
