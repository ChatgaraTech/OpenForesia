# Verifie notre decodeur MS-ADPCM contre celui de Windows, echantillon par echantillon.
#
# Le son d'une cinematique ne se relit pas a l'oeil : une erreur de prediction donne un resultat
# qui a l'air d'une forme d'onde plausible et sonne faux. Il faut donc une reference, et il y en a
# une sur toute machine Windows : `msadp32.acm`, le decodeur MS-ADPCM du systeme, atteignable par
# l'API ACM (`acmStreamOpen` / `acmStreamConvert`).
#
# Le decodeur teste ici est la transcription EXACTE de OpenForesia/src/Host/MsAdpcm.cs. Si les deux
# rendent les memes echantillons, l'algorithme du jeu est bon.
#
#   powershell -File OpenForesia.Tools\check-movie-audio.ps1 -Avi E:\French\Teps.AVI
#
# Ecrit aussi un WAV si -Wav est donne, pour pouvoir ecouter.

param(
    [string]$Avi = 'E:\French\Teps.AVI',
    [string]$Wav = '',
    [int]$Blocks = 200
)

$ErrorActionPreference = 'Stop'

$cp = New-Object System.CodeDom.Compiler.CompilerParameters
$cp.CompilerOptions = '/unsafe'
$cp.GenerateInMemory = $true
$null = $cp.ReferencedAssemblies.Add('System.dll')

Add-Type -CompilerParameters $cp -TypeDefinition @"
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public static unsafe class MovieAudio
{
    // ---------------------------------------------------------------- ACM (la reference Windows)
    [DllImport("msacm32.dll")] static extern int acmStreamOpen(out IntPtr has, IntPtr had,
        byte* src, byte* dst, IntPtr filter, IntPtr callback, IntPtr instance, uint flags);
    [DllImport("msacm32.dll")] static extern int acmStreamClose(IntPtr has, uint flags);
    [DllImport("msacm32.dll")] static extern int acmStreamSize(IntPtr has, uint input, out uint output, uint flags);
    [DllImport("msacm32.dll")] static extern int acmStreamPrepareHeader(IntPtr has, byte* header, uint flags);
    [DllImport("msacm32.dll")] static extern int acmStreamUnprepareHeader(IntPtr has, byte* header, uint flags);
    [DllImport("msacm32.dll")] static extern int acmStreamConvert(IntPtr has, byte* header, uint flags);

    // ---------------------------------------------------------------- lecture du conteneur
    static byte[] _b;
    public static byte[] Format;        // le WAVEFORMATEX complet du flux audio
    public static int Channels, SampleRate, BlockAlign, SamplesPerBlock;
    public static short[,] Coefficients;
    public static List<byte[]> AudioChunks = new List<byte[]>();

    static string Fcc(int o) { return Encoding.ASCII.GetString(_b, o, 4); }
    static int I32(int o) { return BitConverter.ToInt32(_b, o); }
    static int I16(int o) { return BitConverter.ToUInt16(_b, o); }

    public static void Open(string path, int maxChunks)
    {
        _b = File.ReadAllBytes(path);
        long riffEnd = Math.Min(8L + (uint)I32(4), _b.LongLength);
        long p = 12, movi = 0, moviEnd = 0;
        while (p + 8 <= riffEnd)
        {
            string id = Fcc((int)p);
            long size = (uint)I32((int)p + 4);
            long body = p + 8;
            if (id == "LIST")
            {
                string t = Fcc((int)body);
                if (t == "movi") { movi = body + 4; moviEnd = body + size; }
                else if (t == "hdrl") Hdrl(body + 4, body + size);
            }
            p = body + size + (size & 1);
        }

        p = movi;
        while (p + 8 <= moviEnd && AudioChunks.Count < maxChunks)
        {
            string id = Fcc((int)p);
            long size = (uint)I32((int)p + 4);
            long body = p + 8;
            if (id == "LIST") { p = body + 4; continue; }
            if (id.EndsWith("wb") && size > 0 && body + size <= moviEnd)
            {
                byte[] d = new byte[size];
                Array.Copy(_b, body, d, 0, size);
                AudioChunks.Add(d);
            }
            p = body + size + (size & 1);
        }
    }

    static void Hdrl(long start, long end)
    {
        long p = start;
        while (p + 8 <= end)
        {
            string id = Fcc((int)p);
            long size = (uint)I32((int)p + 4);
            long body = p + 8;
            if (id == "LIST" && Fcc((int)body) == "strl")
            {
                long q = body + 4; string kind = null;
                while (q + 8 <= body + size)
                {
                    string qid = Fcc((int)q);
                    long qs = (uint)I32((int)q + 4);
                    long qb = q + 8;
                    if (qid == "strh") kind = Fcc((int)qb);
                    else if (qid == "strf" && kind == "auds")
                    {
                        Format = new byte[qs];
                        Array.Copy(_b, qb, Format, 0, qs);
                        Channels = I16((int)qb + 2);
                        SampleRate = I32((int)qb + 4);
                        BlockAlign = I16((int)qb + 12);
                        SamplesPerBlock = I16((int)qb + 18);
                        int pairs = I16((int)qb + 20);
                        Coefficients = new short[pairs, 2];
                        for (int i = 0; i < pairs; i++)
                        {
                            Coefficients[i, 0] = BitConverter.ToInt16(_b, (int)qb + 22 + i * 4);
                            Coefficients[i, 1] = BitConverter.ToInt16(_b, (int)qb + 24 + i * 4);
                        }
                    }
                    q = qb + qs + (qs & 1);
                }
            }
            p = body + size + (size & 1);
        }
    }

    // ---------------------------------------------------------------- notre decodeur
    // Transcription exacte de OpenForesia/src/Host/MsAdpcm.cs.
    static readonly int[] Adaptation = {
        230, 230, 230, 230, 307, 409, 512, 614,
        768, 614, 512, 409, 307, 230, 230, 230 };

    public static int DecodeBlock(byte[] block, int offsetIn, int lengthIn, int channels,
                                  short[,] coefficients, short[] destination, int offset)
    {
        if (channels != 1 && channels != 2) return 0;
        int headerBytes = 7 * channels;
        if (lengthIn < headerBytes) return 0;

        int[] coef1 = new int[2], coef2 = new int[2], delta = new int[2];
        int[] sample1 = new int[2], sample2 = new int[2];

        int p = offsetIn;
        for (int c = 0; c < channels; c++)
        {
            int predictor = Math.Min(block[p++], coefficients.GetLength(0) - 1);
            coef1[c] = coefficients[predictor, 0];
            coef2[c] = coefficients[predictor, 1];
        }
        for (int c = 0; c < channels; c++) { delta[c] = BitConverter.ToInt16(block, p); p += 2; }
        for (int c = 0; c < channels; c++) { sample1[c] = BitConverter.ToInt16(block, p); p += 2; }
        for (int c = 0; c < channels; c++) { sample2[c] = BitConverter.ToInt16(block, p); p += 2; }

        int written = 0;
        for (int c = 0; c < channels; c++)
        {
            if (offset + written >= destination.Length) return written;
            destination[offset + written++] = (short)sample2[c];
        }
        for (int c = 0; c < channels; c++)
        {
            if (offset + written >= destination.Length) return written;
            destination[offset + written++] = (short)sample1[c];
        }

        int channel = 0;
        int end = offsetIn + lengthIn;
        for (; p < end; p++)
        {
            for (int half = 0; half < 2; half++)
            {
                int nibble = half == 0 ? block[p] >> 4 : block[p] & 0x0f;
                if (offset + written >= destination.Length) return written;
                int c = channels == 1 ? 0 : channel;

                int predicted = (sample1[c] * coef1[c] + sample2[c] * coef2[c]) >> 8;
                int signed = nibble >= 8 ? nibble - 16 : nibble;
                predicted += signed * delta[c];
                if (predicted < -32768) predicted = -32768;
                if (predicted > 32767) predicted = 32767;

                destination[offset + written++] = (short)predicted;
                sample2[c] = sample1[c];
                sample1[c] = predicted;

                delta[c] = Adaptation[nibble] * delta[c] / 256;
                if (delta[c] < 16) delta[c] = 16;

                if (channels == 2) channel ^= 1;
            }
        }
        return written;
    }

    public static short[] DecodeOurs(out int count)
    {
        var all = new List<short>();
        short[] scratch = new short[SamplesPerBlock * Channels + 64];
        foreach (byte[] chunk in AudioChunks)
        {
            for (int off = 0; off + 1 < chunk.Length; off += BlockAlign)
            {
                int size = Math.Min(BlockAlign, chunk.Length - off);
                int n = DecodeBlock(chunk, off, size, Channels, Coefficients, scratch, 0);
                for (int i = 0; i < n; i++) all.Add(scratch[i]);
            }
        }
        count = all.Count;
        return all.ToArray();
    }

    // ---------------------------------------------------------------- la reference
    public static short[] DecodeAcm(out string note)
    {
        note = "";
        var input = new List<byte>();
        foreach (byte[] c in AudioChunks) input.AddRange(c);
        byte[] src = input.ToArray();

        // WAVEFORMATEX de sortie : PCM 16 bits, memes canaux et meme frequence.
        byte* dstFormat = stackalloc byte[18];
        for (int i = 0; i < 18; i++) dstFormat[i] = 0;
        *(ushort*)(dstFormat + 0) = 1;
        *(ushort*)(dstFormat + 2) = (ushort)Channels;
        *(uint*)(dstFormat + 4) = (uint)SampleRate;
        *(uint*)(dstFormat + 8) = (uint)(SampleRate * Channels * 2);
        *(ushort*)(dstFormat + 12) = (ushort)(Channels * 2);
        *(ushort*)(dstFormat + 14) = 16;

        IntPtr stream;
        fixed (byte* srcFormat = Format)
        {
            int rc = acmStreamOpen(out stream, IntPtr.Zero, srcFormat, dstFormat,
                                   IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0);
            if (rc != 0) { note = "acmStreamOpen = " + rc; return null; }
        }

        uint needed;
        if (acmStreamSize(stream, (uint)src.Length, out needed, 0 /* SOURCE */) != 0)
        { note = "acmStreamSize a echoue"; acmStreamClose(stream, 0); return null; }

        byte[] dst = new byte[needed];
        byte[] header = new byte[84];
        fixed (byte* ps = src)
        fixed (byte* pd = dst)
        fixed (byte* ph = header)
        {
            *(uint*)(ph + 0) = 84;               // cbStruct
            *(IntPtr*)(ph + 12) = (IntPtr)ps;    // pbSrc
            *(uint*)(ph + 16) = (uint)src.Length;
            *(IntPtr*)(ph + 28) = (IntPtr)pd;    // pbDst
            *(uint*)(ph + 32) = needed;

            int rc = acmStreamPrepareHeader(stream, ph, 0);
            if (rc != 0) { note = "prepareHeader = " + rc; acmStreamClose(stream, 0); return null; }

            // BLOCKALIGN | START | END
            rc = acmStreamConvert(stream, ph, 0x00000004 | 0x00000010 | 0x00000020);
            if (rc != 0) { note = "acmStreamConvert = " + rc; acmStreamClose(stream, 0); return null; }

            uint used = *(uint*)(ph + 36);       // cbDstLengthUsed
            acmStreamUnprepareHeader(stream, ph, 0);
            acmStreamClose(stream, 0);

            short[] result = new short[used / 2];
            Buffer.BlockCopy(dst, 0, result, 0, (int)(used & ~1u));
            return result;
        }
    }

    public static void WriteWav(string path, short[] samples, int channels, int rate)
    {
        using (var f = new BinaryWriter(File.Create(path)))
        {
            int data = samples.Length * 2;
            f.Write(Encoding.ASCII.GetBytes("RIFF")); f.Write(36 + data);
            f.Write(Encoding.ASCII.GetBytes("WAVEfmt ")); f.Write(16);
            f.Write((short)1); f.Write((short)channels); f.Write(rate);
            f.Write(rate * channels * 2); f.Write((short)(channels * 2)); f.Write((short)16);
            f.Write(Encoding.ASCII.GetBytes("data")); f.Write(data);
            foreach (short s in samples) f.Write(s);
        }
    }
}
"@

[MovieAudio]::Open($Avi, $Blocks)
Write-Host ("fichier          : {0}" -f $Avi)
Write-Host ("format audio     : {0} canal(aux), {1} Hz, bloc {2} octets, {3} echantillons/bloc, {4} couples" -f `
    [MovieAudio]::Channels, [MovieAudio]::SampleRate, [MovieAudio]::BlockAlign,
    [MovieAudio]::SamplesPerBlock, [MovieAudio]::Coefficients.GetLength(0))
Write-Host ("blocs audio lus  : {0}" -f [MovieAudio]::AudioChunks.Count)

$count = 0
$ours = [MovieAudio]::DecodeOurs([ref]$count)
Write-Host ("notre decodeur   : {0} echantillons" -f $ours.Length)

$note = ''
$acm = [MovieAudio]::DecodeAcm([ref]$note)
if ($null -eq $acm) {
    Write-Host ("reference ACM    : indisponible ({0})" -f $note)
} else {
    Write-Host ("reference ACM    : {0} echantillons" -f $acm.Length)
    $n = [Math]::Min($ours.Length, $acm.Length)
    $diff = 0; $maxDiff = 0
    for ($i = 0; $i -lt $n; $i++) {
        $d = [Math]::Abs([int]$ours[$i] - [int]$acm[$i])
        if ($d -ne 0) { $diff++; if ($d -gt $maxDiff) { $maxDiff = $d } }
    }
    Write-Host ""
    if ($diff -eq 0 -and $ours.Length -eq $acm.Length) {
        Write-Host "VERDICT : IDENTIQUE -- $n echantillons, aucun ecart avec le decodeur de Windows"
    } else {
        Write-Host ("VERDICT : ECART -- {0} echantillons sur {1} different, ecart maximum {2}" -f $diff, $n, $maxDiff)
        Write-Host ("          longueurs : nous {0}, ACM {1}" -f $ours.Length, $acm.Length)
    }
}

if ($Wav -ne '') {
    [MovieAudio]::WriteWav($Wav, $ours, [MovieAudio]::Channels, [MovieAudio]::SampleRate)
    Write-Host ("WAV ecrit        : {0}" -f (Resolve-Path $Wav))
}
