// =====================================================================
// Sortie audio des cinématiques, par `waveOut` de winmm.
//
// Notre échafaudage. Le chemin audio du jeu est DirectSound, piloté par la simulation image par
// image ; les cinématiques tournent avant lui, sur un thread qui ne fait que pomper des messages,
// et leur son a sa propre horloge. Les brancher l'un sur l'autre reviendrait à faire dépendre le
// démarrage d'un sous-système que le film n'a aucune raison d'attendre.
//
// `waveOut` est la sortie la plus simple que Windows offre : quatre appels, aucune dépendance, et
// surtout **une position de lecture en échantillons** — c'est elle qui sert d'horloge au film, ce
// qui évite toute dérive entre l'image et le son sur trois minutes de projection.
// =====================================================================

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

internal sealed unsafe class WaveOut : IDisposable
{
    [DllImport("winmm.dll")] private static extern int waveOutOpen(
        out IntPtr device, uint deviceId, byte* format, IntPtr callback, IntPtr instance, uint flags);
    [DllImport("winmm.dll")] private static extern int waveOutPrepareHeader(IntPtr device, byte* header, uint size);
    [DllImport("winmm.dll")] private static extern int waveOutUnprepareHeader(IntPtr device, byte* header, uint size);
    [DllImport("winmm.dll")] private static extern int waveOutWrite(IntPtr device, byte* header, uint size);
    [DllImport("winmm.dll")] private static extern int waveOutReset(IntPtr device);
    [DllImport("winmm.dll")] private static extern int waveOutClose(IntPtr device);
    [DllImport("winmm.dll")] private static extern int waveOutGetPosition(IntPtr device, byte* time, uint size);

    private const uint WaveMapper = 0xFFFFFFFF;
    private const uint WhdrDone = 0x00000001;
    private const int HeaderBytes = 32;      // WAVEHDR, x86
    private const int MmTimeBytes = 12;      // MMTIME, x86
    private const uint TimeSamples = 2;

    // Seize tampons : à 46 ms de son par bloc ADPCM et un ou deux blocs par bloc AVI, cela fait à
    // peu près une seconde d'avance possible. Assez pour absorber une image lente, assez peu pour
    // que l'arrêt sur une touche soit immédiat.
    private const int SlotCount = 16;

    private readonly IntPtr _device;
    private readonly IntPtr[] _headers = new IntPtr[SlotCount];
    private readonly IntPtr[] _buffers = new IntPtr[SlotCount];
    private readonly int[] _capacity = new int[SlotCount];
    private readonly IntPtr _time;
    private readonly int _sampleRate;
    private readonly int _channels;
    private bool _closed;

    private WaveOut(IntPtr device, int sampleRate, int channels)
    {
        _device = device;
        _sampleRate = sampleRate;
        _channels = channels;
        _time = Marshal.AllocHGlobal(MmTimeBytes);
    }

    /// <summary>
    /// Ouvre une sortie PCM 16 bits. Rend <c>null</c> si le périphérique refuse — auquel cas le
    /// film se joue sans son plutôt que pas du tout.
    /// </summary>
    internal static WaveOut? Open(int sampleRate, int channels)
    {
        // WAVEFORMATEX : format, canaux, fréquence, octets par seconde, alignement de bloc, bits.
        byte* format = stackalloc byte[18];
        for (int i = 0; i < 18; i++) format[i] = 0;
        int blockAlign = channels * 2;
        *(ushort*)(format + 0) = 1;                          // WAVE_FORMAT_PCM
        *(ushort*)(format + 2) = (ushort)channels;
        *(uint*)(format + 4) = (uint)sampleRate;
        *(uint*)(format + 8) = (uint)(sampleRate * blockAlign);
        *(ushort*)(format + 12) = (ushort)blockAlign;
        *(ushort*)(format + 14) = 16;
        *(ushort*)(format + 16) = 0;                         // cbSize

        int rc = waveOutOpen(out IntPtr device, WaveMapper, format, IntPtr.Zero, IntPtr.Zero, 0 /* CALLBACK_NULL */);
        if (rc != 0 || device == IntPtr.Zero)
        {
            Console.Error.WriteLine($"[FILM] waveOutOpen = {rc} : le film se jouera sans son");
            return null;
        }
        return new WaveOut(device, sampleRate, channels);
    }

    /// <summary>
    /// Met des échantillons en file. Rend faux si tous les tampons sont occupés — l'appelant a
    /// alors de l'avance et n'a qu'à revenir plus tard, l'échantillon n'étant pas consommé.
    /// </summary>
    internal bool Enqueue(short[] samples, int count)
    {
        if (_closed || count <= 0) return true;

        int slot = FreeSlot();
        if (slot < 0) return false;

        int bytes = count * 2;
        if (_capacity[slot] < bytes)
        {
            if (_buffers[slot] != IntPtr.Zero) Marshal.FreeHGlobal(_buffers[slot]);
            _buffers[slot] = Marshal.AllocHGlobal(bytes);
            _capacity[slot] = bytes;
        }
        Marshal.Copy(samples, 0, _buffers[slot], count);

        if (_headers[slot] == IntPtr.Zero) _headers[slot] = Marshal.AllocHGlobal(HeaderBytes);
        var header = (byte*)_headers[slot];
        for (int i = 0; i < HeaderBytes; i++) header[i] = 0;
        *(IntPtr*)(header + 0) = _buffers[slot];
        *(uint*)(header + 4) = (uint)bytes;

        // Un échec ici perd le lot : il est déjà décodé, et le reproposer indéfiniment bloquerait
        // la lecture. On le dit au journal plutôt que de le taire — un son qui saute sans qu'aucune
        // trace ne l'explique est exactement ce qu'on ne veut pas avoir à chercher deux fois.
        int rc = waveOutPrepareHeader(_device, header, HeaderBytes);
        if (rc != 0)
        {
            Console.Error.WriteLine($"[FILM] waveOutPrepareHeader = {rc}, lot perdu");
            return true;
        }

        rc = waveOutWrite(_device, header, HeaderBytes);
        if (rc != 0)
        {
            Console.Error.WriteLine($"[FILM] waveOutWrite = {rc}, lot perdu");
            waveOutUnprepareHeader(_device, header, HeaderBytes);
        }
        return true;
    }

    private int FreeSlot()
    {
        for (int i = 0; i < SlotCount; i++)
        {
            if (_headers[i] == IntPtr.Zero) return i;

            var header = (byte*)_headers[i];
            uint flags = *(uint*)(header + 16);   // dwFlags
            if ((flags & WhdrDone) != 0)
            {
                waveOutUnprepareHeader(_device, header, HeaderBytes);
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// La position de lecture, en microsecondes. C'est **l'horloge du film** : elle avance au
    /// rythme réel du périphérique, donc l'image ne peut pas dériver du son.
    /// </summary>
    internal long PositionMicroseconds()
    {
        if (_closed) return 0;
        var time = (byte*)_time;
        *(uint*)(time + 0) = TimeSamples;
        if (waveOutGetPosition(_device, time, MmTimeBytes) != 0) return 0;
        if (*(uint*)(time + 0) != TimeSamples) return 0;
        ulong samples = *(uint*)(time + 4);
        return (long)(samples * 1_000_000UL / (ulong)Math.Max(1, _sampleRate));
    }

    public void Dispose()
    {
        if (_closed) return;
        _closed = true;

        waveOutReset(_device);
        for (int i = 0; i < SlotCount; i++)
        {
            if (_headers[i] != IntPtr.Zero)
            {
                waveOutUnprepareHeader(_device, (byte*)_headers[i], HeaderBytes);
                Marshal.FreeHGlobal(_headers[i]);
                _headers[i] = IntPtr.Zero;
            }
            if (_buffers[i] != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_buffers[i]);
                _buffers[i] = IntPtr.Zero;
            }
        }
        waveOutClose(_device);
        Marshal.FreeHGlobal(_time);
    }
}
