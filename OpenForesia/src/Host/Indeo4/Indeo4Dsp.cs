// =====================================================================
// Décodeur Indeo 4 — transformées inverses, compensation de mouvement, recomposition.
//
// Traduit de FFmpeg (https://ffmpeg.org), `libavcodec/ivi_dsp.c`, révision
// b0d154591161153b0ec55d9527535bc776504899. Copyright (c) 2009-2011 Maxim Poliakovski.
//
// Ce fichier est distribué sous la GNU Lesser General Public License, version 2.1 ou (à votre
// choix) toute version ultérieure, comme la source dont il est tiré. Voir `LICENSE-LGPL.txt` dans
// ce répertoire.
//
// **Les macros de FFmpeg nomment leurs paramètres dans le désordre** : `IVI_INV_SLANT8(s1, s4, s8,
// s5, s2, s6, s3, s7, …)` reçoit ses huit entrées dans l'ordre du tableau, mais les baptise selon
// leur rôle dans le papillon. Les noyaux ci-dessous prennent les entrées dans l'ordre du tableau
// (`a0` … `a7`) et refont ce baptême en tête, ligne pour ligne. Toute l'exactitude à l'octet tient
// à ce détail.
//
// Les tampons de bande sont en `short`, comme les `int16_t` de FFmpeg : chaque écriture tronque.
// =====================================================================

namespace OpenForesia.Blaze;

internal static class Indeo4Dsp
{
    // ------------------------------------------------------------------ noyaux 1D
    // Chacun rend t1..t8 (ou t1..t4) bruts ; l'appelant applique COMPENSATE et range.

    /// <summary>INV_HAAR8. Entrées dans l'ordre du tableau : s1, s5, s3, s7, s2, s4, s6, s8.</summary>
    private static void Haar8(int a0, int a1, int a2, int a3, int a4, int a5, int a6, int a7, Span<int> d)
    {
        int s1 = a0, s5 = a1, s3 = a2, s7 = a3, s2 = a4, s4 = a5, s6 = a6, s8 = a7;
        int t0, t1, t2, t3, t4, t5, t6, t7, t8;
        t1 = s1 * 2; t5 = s5 * 2;
        t0 = (t1 - t5) >> 1; t1 = (t1 + t5) >> 1; t5 = t0;
        t0 = (t1 - s3) >> 1; t1 = (t1 + s3) >> 1; t3 = t0;
        t0 = (t5 - s7) >> 1; t5 = (t5 + s7) >> 1; t7 = t0;
        t0 = (t1 - s2) >> 1; t1 = (t1 + s2) >> 1; t2 = t0;
        t0 = (t3 - s4) >> 1; t3 = (t3 + s4) >> 1; t4 = t0;
        t0 = (t5 - s6) >> 1; t5 = (t5 + s6) >> 1; t6 = t0;
        t0 = (t7 - s8) >> 1; t7 = (t7 + s8) >> 1; t8 = t0;
        d[0] = t1; d[1] = t2; d[2] = t3; d[3] = t4; d[4] = t5; d[5] = t6; d[6] = t7; d[7] = t8;
    }

    /// <summary>INV_HAAR4. Entrées : s1, s3, s5, s7.</summary>
    private static void Haar4(int s1, int s3, int s5, int s7, Span<int> d)
    {
        int t0, t1, t2, t3, t4;
        t4 = (s1 - s3) >> 1; t0 = (s1 + s3) >> 1; t1 = t4;
        t4 = (t0 - s5) >> 1; t2 = (t0 + s5) >> 1; t3 = t4;
        d[0] = t2;
        d[1] = t3;
        t4 = (t1 - s7) >> 1; t2 = (t1 + s7) >> 1; t3 = t4;
        d[2] = t2;
        d[3] = t3;
    }

    /// <summary>IVI_INV_SLANT8. Entrées dans l'ordre du tableau : s1, s4, s8, s5, s2, s6, s3, s7.</summary>
    private static void Slant8(int a0, int a1, int a2, int a3, int a4, int a5, int a6, int a7, Span<int> d)
    {
        int s1 = a0, s4 = a1, s8 = a2, s5 = a3, s2 = a4, s6 = a5, s3 = a6, s7 = a7;
        int t0, t1, t2, t3, t4, t5, t6, t7, t8;

        // IVI_SLANT_PART4(s4, s5, t4, t5)
        t0 = s5 + ((s4 * 4 - s5 + 4) >> 3);
        t5 = s4 + ((-s4 - s5 * 4 + 4) >> 3);
        t4 = t0;

        t0 = s1 - t5; t1 = s1 + t5; t5 = t0;
        t0 = s2 - s6; t2 = s2 + s6; t6 = t0;
        t0 = s7 - s3; t7 = s7 + s3; t3 = t0;
        t0 = t4 - s8; t4 = t4 + s8; t8 = t0;

        t0 = t1 - t2; t1 = t1 + t2; t2 = t0;
        // IVI_IREFLECT(t4, t3, t4, t3)
        t0 = ((t4 + t3 * 2 + 2) >> 2) + t4;
        t3 = ((t4 * 2 - t3 + 2) >> 2) - t3;
        t4 = t0;
        t0 = t5 - t6; t5 = t5 + t6; t6 = t0;
        // IVI_IREFLECT(t8, t7, t8, t7)
        t0 = ((t8 + t7 * 2 + 2) >> 2) + t8;
        t7 = ((t8 * 2 - t7 + 2) >> 2) - t7;
        t8 = t0;

        t0 = t1 - t4; t1 = t1 + t4; t4 = t0;
        t0 = t2 - t3; t2 = t2 + t3; t3 = t0;
        t0 = t5 - t8; t5 = t5 + t8; t8 = t0;
        t0 = t6 - t7; t6 = t6 + t7; t7 = t0;

        d[0] = t1; d[1] = t2; d[2] = t3; d[3] = t4; d[4] = t5; d[5] = t6; d[6] = t7; d[7] = t8;
    }

    /// <summary>IVI_INV_SLANT4. Entrées dans l'ordre du tableau : s1, s4, s2, s3.</summary>
    private static void Slant4(int a0, int a1, int a2, int a3, Span<int> d)
    {
        int s1 = a0, s4 = a1, s2 = a2, s3 = a3;
        int t0, t1, t2, t3, t4;
        t0 = s1 - s2; t1 = s1 + s2; t2 = t0;
        // IVI_IREFLECT(s4, s3, t4, t3)
        t0 = ((s4 + s3 * 2 + 2) >> 2) + s4;
        t3 = ((s4 * 2 - s3 + 2) >> 2) - s3;
        t4 = t0;
        t0 = t1 - t4; t1 = t1 + t4; t4 = t0;
        t0 = t2 - t3; t2 = t2 + t3; t3 = t0;
        d[0] = t1; d[1] = t2; d[2] = t3; d[3] = t4;
    }

    private static int Half(int x) => (x + 1) >> 1;

    private static bool RowIsZero(ReadOnlySpan<int> row) => row.IndexOfAnyExcept(0) < 0;

    // ------------------------------------------------------------------ transformées inverses
    // Toutes : `input` les coefficients (64 ou 16), `output` le tampon de bande à partir de `o`,
    // `pitch` son pas, `flags` les colonnes qui portent au moins un coefficient non nul.

    internal static void InverseHaar8x8(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> tmp = stackalloc int[64];
        Span<int> d = stackalloc int[8];
        for (int i = 0; i < 8; i++)
        {
            if (flags[i] != 0)
            {
                int shift = (i & 4) == 0 ? 1 : 0;
                int sp1 = input[i] * (1 << shift);
                int sp2 = input[i + 8] * (1 << shift);
                int sp3 = input[i + 16] * (1 << shift);
                int sp4 = input[i + 24] * (1 << shift);
                Haar8(sp1, sp2, sp3, sp4, input[i + 32], input[i + 40], input[i + 48], input[i + 56], d);
                for (int k = 0; k < 8; k++) tmp[i + 8 * k] = d[k];
            }
            else
            {
                for (int k = 0; k < 8; k++) tmp[i + 8 * k] = 0;
            }
        }

        for (int i = 0; i < 8; i++, o += pitch)
        {
            var s = tmp.Slice(i * 8, 8);
            if (RowIsZero(s))
            {
                output.AsSpan(o, 8).Clear();
                continue;
            }
            Haar8(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], d);
            for (int k = 0; k < 8; k++) output[o + k] = (short)d[k];
        }
    }

    internal static void RowHaar8(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> d = stackalloc int[8];
        for (int i = 0; i < 8; i++, o += pitch)
        {
            var s = input.AsSpan(i * 8, 8);
            if (RowIsZero(s))
            {
                output.AsSpan(o, 8).Clear();
                continue;
            }
            Haar8(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], d);
            for (int k = 0; k < 8; k++) output[o + k] = (short)d[k];
        }
    }

    internal static void ColHaar8(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> d = stackalloc int[8];
        for (int i = 0; i < 8; i++)
        {
            if (flags[i] != 0)
            {
                Haar8(input[i], input[i + 8], input[i + 16], input[i + 24],
                      input[i + 32], input[i + 40], input[i + 48], input[i + 56], d);
                for (int k = 0; k < 8; k++) output[o + i + k * pitch] = (short)d[k];
            }
            else
            {
                for (int k = 0; k < 8; k++) output[o + i + k * pitch] = 0;
            }
        }
    }

    internal static void InverseHaar4x4(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> tmp = stackalloc int[16];
        Span<int> d = stackalloc int[4];
        for (int i = 0; i < 4; i++)
        {
            if (flags[i] != 0)
            {
                int shift = (i & 2) == 0 ? 1 : 0;
                int sp1 = input[i] * (1 << shift);
                int sp2 = input[i + 4] * (1 << shift);
                Haar4(sp1, sp2, input[i + 8], input[i + 12], d);
                for (int k = 0; k < 4; k++) tmp[i + 4 * k] = d[k];
            }
            else
            {
                for (int k = 0; k < 4; k++) tmp[i + 4 * k] = 0;
            }
        }

        for (int i = 0; i < 4; i++, o += pitch)
        {
            var s = tmp.Slice(i * 4, 4);
            if (RowIsZero(s))
            {
                output.AsSpan(o, 4).Clear();
                continue;
            }
            Haar4(s[0], s[1], s[2], s[3], d);
            for (int k = 0; k < 4; k++) output[o + k] = (short)d[k];
        }
    }

    internal static void RowHaar4(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> d = stackalloc int[4];
        for (int i = 0; i < 4; i++, o += pitch)
        {
            var s = input.AsSpan(i * 4, 4);
            if (RowIsZero(s))
            {
                output.AsSpan(o, 4).Clear();
                continue;
            }
            Haar4(s[0], s[1], s[2], s[3], d);
            for (int k = 0; k < 4; k++) output[o + k] = (short)d[k];
        }
    }

    internal static void ColHaar4(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> d = stackalloc int[4];
        for (int i = 0; i < 4; i++)
        {
            if (flags[i] != 0)
            {
                Haar4(input[i], input[i + 4], input[i + 8], input[i + 12], d);
                for (int k = 0; k < 4; k++) output[o + i + k * pitch] = (short)d[k];
            }
            else
            {
                for (int k = 0; k < 4; k++) output[o + i + k * pitch] = 0;
            }
        }
    }

    internal static void InverseSlant8x8(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> tmp = stackalloc int[64];
        Span<int> d = stackalloc int[8];
        for (int i = 0; i < 8; i++)
        {
            if (flags[i] != 0)
            {
                Slant8(input[i], input[i + 8], input[i + 16], input[i + 24],
                       input[i + 32], input[i + 40], input[i + 48], input[i + 56], d);
                for (int k = 0; k < 8; k++) tmp[i + 8 * k] = d[k];
            }
            else
            {
                for (int k = 0; k < 8; k++) tmp[i + 8 * k] = 0;
            }
        }

        for (int i = 0; i < 8; i++, o += pitch)
        {
            var s = tmp.Slice(i * 8, 8);
            if (RowIsZero(s))
            {
                output.AsSpan(o, 8).Clear();
                continue;
            }
            Slant8(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], d);
            for (int k = 0; k < 8; k++) output[o + k] = (short)Half(d[k]);
        }
    }

    internal static void InverseSlant4x4(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> tmp = stackalloc int[16];
        Span<int> d = stackalloc int[4];
        for (int i = 0; i < 4; i++)
        {
            if (flags[i] != 0)
            {
                Slant4(input[i], input[i + 4], input[i + 8], input[i + 12], d);
                for (int k = 0; k < 4; k++) tmp[i + 4 * k] = d[k];
            }
            else
            {
                for (int k = 0; k < 4; k++) tmp[i + 4 * k] = 0;
            }
        }

        for (int i = 0; i < 4; i++, o += pitch)
        {
            var s = tmp.Slice(i * 4, 4);
            if (RowIsZero(s))
            {
                output.AsSpan(o, 4).Clear();
                continue;
            }
            Slant4(s[0], s[1], s[2], s[3], d);
            for (int k = 0; k < 4; k++) output[o + k] = (short)Half(d[k]);
        }
    }

    internal static void RowSlant8(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> d = stackalloc int[8];
        for (int i = 0; i < 8; i++, o += pitch)
        {
            var s = input.AsSpan(i * 8, 8);
            if (RowIsZero(s))
            {
                output.AsSpan(o, 8).Clear();
                continue;
            }
            Slant8(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], d);
            for (int k = 0; k < 8; k++) output[o + k] = (short)Half(d[k]);
        }
    }

    internal static void ColSlant8(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> d = stackalloc int[8];
        for (int i = 0; i < 8; i++)
        {
            if (flags[i] != 0)
            {
                Slant8(input[i], input[i + 8], input[i + 16], input[i + 24],
                       input[i + 32], input[i + 40], input[i + 48], input[i + 56], d);
                for (int k = 0; k < 8; k++) output[o + i + k * pitch] = (short)Half(d[k]);
            }
            else
            {
                for (int k = 0; k < 8; k++) output[o + i + k * pitch] = 0;
            }
        }
    }

    internal static void RowSlant4(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> d = stackalloc int[4];
        for (int i = 0; i < 4; i++, o += pitch)
        {
            var s = input.AsSpan(i * 4, 4);
            if (RowIsZero(s))
            {
                output.AsSpan(o, 4).Clear();
                continue;
            }
            Slant4(s[0], s[1], s[2], s[3], d);
            for (int k = 0; k < 4; k++) output[o + k] = (short)Half(d[k]);
        }
    }

    internal static void ColSlant4(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        Span<int> d = stackalloc int[4];
        for (int i = 0; i < 4; i++)
        {
            if (flags[i] != 0)
            {
                Slant4(input[i], input[i + 4], input[i + 8], input[i + 12], d);
                for (int k = 0; k < 4; k++) output[o + i + k * pitch] = (short)Half(d[k]);
            }
            else
            {
                for (int k = 0; k < 4; k++) output[o + i + k * pitch] = 0;
            }
        }
    }

    internal static void PutPixels8x8(int[] input, short[] output, int o, int pitch, byte[] flags)
    {
        for (int y = 0; y < 8; y++, o += pitch)
            for (int x = 0; x < 8; x++)
                output[o + x] = (short)input[y * 8 + x];
    }

    // ------------------------------------------------------------------ transformées du seul DC
    // `dc` est la valeur prédite (le `*in` de FFmpeg, qui y pointe sur `prev_dc`).

    internal static void DcHaar2d(int dc, short[] output, int o, int pitch, int blkSize)
    {
        short value = (short)((dc + 0) >> 3);
        for (int y = 0; y < blkSize; y++, o += pitch)
            output.AsSpan(o, blkSize).Fill(value);
    }

    internal static void DcSlant2d(int dc, short[] output, int o, int pitch, int blkSize)
    {
        short value = (short)((dc + 1) >> 1);
        for (int y = 0; y < blkSize; y++, o += pitch)
            output.AsSpan(o, blkSize).Fill(value);
    }

    internal static void DcRowSlant(int dc, short[] output, int o, int pitch, int blkSize)
    {
        short value = (short)((dc + 1) >> 1);
        output.AsSpan(o, blkSize).Fill(value);
        o += pitch;
        for (int y = 1; y < blkSize; y++, o += pitch)
            output.AsSpan(o, blkSize).Clear();
    }

    internal static void DcColSlant(int dc, short[] output, int o, int pitch, int blkSize)
    {
        short value = (short)((dc + 1) >> 1);
        for (int y = 0; y < blkSize; y++, o += pitch)
        {
            output[o] = value;
            output.AsSpan(o + 1, blkSize - 1).Clear();
        }
    }

    /// <summary>ff_ivi_put_dc_pixel_8x8 : ignore la taille de bloc, écrit toujours 8×8.</summary>
    internal static void PutDcPixel8x8(int dc, short[] output, int o, int pitch, int blkSize)
    {
        output[o] = (short)dc;
        output.AsSpan(o + 1, 7).Clear();
        o += pitch;
        for (int y = 1; y < 8; y++, o += pitch)
            output.AsSpan(o, 8).Clear();
    }

    // ------------------------------------------------------------------ compensation de mouvement

    /// <summary>
    /// IVI_MC_TEMPLATE : un bloc <paramref name="size"/>×<paramref name="size"/> pris dans la
    /// référence, au demi-pixel selon <paramref name="mcType"/> (0 plein, 1 horizontal,
    /// 2 vertical, 3 les deux ; toute autre valeur ne fait rien). <paramref name="add"/> distingue
    /// `_delta` (on ajoute au résidu déjà décodé) de `_no_delta` (on remplace).
    /// </summary>
    internal static void Mc(short[] buf, int b, int dpitch, short[] rf, int r, int pitch, int mcType, int size, bool add)
    {
        switch (mcType)
        {
            case 0:
                for (int i = 0; i < size; i++, b += dpitch, r += pitch)
                    for (int j = 0; j < size; j++)
                        Op(buf, b + j, rf[r + j], add);
                break;
            case 1:
                for (int i = 0; i < size; i++, b += dpitch, r += pitch)
                    for (int j = 0; j < size; j++)
                        Op(buf, b + j, (rf[r + j] + rf[r + j + 1]) >> 1, add);
                break;
            case 2:
                for (int i = 0; i < size; i++, b += dpitch, r += pitch)
                    for (int j = 0; j < size; j++)
                        Op(buf, b + j, (rf[r + j] + rf[r + pitch + j]) >> 1, add);
                break;
            case 3:
                for (int i = 0; i < size; i++, b += dpitch, r += pitch)
                    for (int j = 0; j < size; j++)
                        Op(buf, b + j, (rf[r + j] + rf[r + j + 1] + rf[r + pitch + j] + rf[r + pitch + j + 1]) >> 2, add);
                break;
        }
    }

    private static void Op(short[] buf, int i, int value, bool add)
    {
        buf[i] = add ? (short)(buf[i] + value) : (short)value;
    }

    /// <summary>IVI_MC_AVG_TEMPLATE : la moyenne de deux références (images bidirectionnelles).</summary>
    internal static void McAvg(short[] buf, int b, short[] rf1, int r1, short[] rf2, int r2, int pitch,
                               int mcType, int mcType2, int size, bool add)
    {
        short[] tmp = new short[size * size];
        Mc(tmp, 0, size, rf1, r1, pitch, mcType, size, add: false);
        Mc(tmp, 0, size, rf2, r2, pitch, mcType2, size, add: true);
        for (int i = 0; i < size; i++, b += pitch)
            for (int j = 0; j < size; j++)
                Op(buf, b + j, tmp[i * size + j] >> 1, add);
    }

    // ------------------------------------------------------------------ recomposition

    /// <summary>ff_ivi_recompose_haar : les quatre bandes d'un plan de luminance échelonnable.</summary>
    internal static void RecomposeHaar(short[] b0, short[] b1, short[] b2, short[] b3, int pitch,
                                       int width, int height, byte[] dst, int dstPitch)
    {
        int row = 0, d = 0;
        for (int y = 0; y < height; y += 2, row += pitch, d += dstPitch * 2)
        {
            for (int x = 0, indx = 0; x < width; x += 2, indx++)
            {
                int c0 = b0[row + indx], c1 = b1[row + indx], c2 = b2[row + indx], c3 = b3[row + indx];
                int p0 = (c0 + c1 + c2 + c3 + 2) >> 2;
                int p1 = (c0 + c1 - c2 - c3 + 2) >> 2;
                int p2 = (c0 - c1 + c2 - c3 + 2) >> 2;
                int p3 = (c0 - c1 - c2 + c3 + 2) >> 2;
                dst[d + x] = ClipByte(p0 + 128);
                if (x + 1 < width) dst[d + x + 1] = ClipByte(p1 + 128);
                if (y + 1 < height)
                {
                    dst[d + dstPitch + x] = ClipByte(p2 + 128);
                    if (x + 1 < width) dst[d + dstPitch + x + 1] = ClipByte(p3 + 128);
                }
            }
        }
    }

    internal static byte ClipByte(int v) => (byte)(v < 0 ? 0 : v > 255 ? 255 : v);
}
