// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // DirectSound (confirmed). The interface identities below were open questions until the
    // shutdown path (FUN_00512ff0/FUN_00512ea0) and the buffer-creation path (FUN_00513030/
    // FUN_005133xx) were read: together they exercise enough distinct vtable slots to pin both
    // interfaces against the documented DirectX 3-5 COM layouts with no ambiguity left.
    //
    //   DAT_005db8e4 -> IDirectSound      : 0x08 Release, 0x0c CreateSoundBuffer
    //   DAT_005db900 -> IDirectSoundBuffer: 0x08 Release, 0x24 GetStatus, 0x2c Lock, 0x30 Play,
    //                                       0x3c SetVolume, 0x48 Stop, 0x4c Unlock
    //   DAT_005db904 -> IDirectSoundNotify: 0x08 Release, 0x0c SetNotificationPositions
    //
    // This retroactively confirms the earlier "vtable offset 0x3c is probably DirectSound
    // SetVolume" hypothesis in ApplyCdVolume/ApplyAllSoundBufferVolumes (was TODO(REVERSE) #9).
    //
    // The IDirectSoundNotify identity comes out of FUN_005131d0: the object is obtained by
    // QueryInterface (vtable slot 0) on the streaming buffer and is then only ever used through
    // slot 0x0c with the argument pair (8, &DAT_005db8a0) â€” and DAT_005db8a0..DAT_005db8df is
    // filled in as exactly 8 { dwOffset, hEventNotify } pairs just before that call. That is
    // IDirectSoundNotify::SetNotificationPositions(dwPositionNotifies, pcPositionNotifies) with
    // a DSBPOSITIONNOTIFY[8]; no other DirectSound interface has that shape.
    // =====================================================================

    private const int IUnknown_QueryInterface = 0x00 / 4;
    private const int IDirectSound_Release = 0x08 / 4;
    private const int IDirectSound_CreateSoundBuffer = 0x0c / 4;
    private const int IDirectSoundBuffer_Release = 0x08 / 4;
    private const int IDirectSoundBuffer_GetStatus = 0x24 / 4;
    private const int IDirectSoundBuffer_Lock = 0x2c / 4;
    private const int IDirectSoundBuffer_Play = 0x30 / 4;
    private const int IDirectSoundBuffer_SetVolume = 0x3c / 4;
    private const int IDirectSoundBuffer_Stop = 0x48 / 4;
    private const int IDirectSoundBuffer_Unlock = 0x4c / 4;
    private const int IDirectSoundNotify_Release = 0x08 / 4;
    private const int IDirectSoundNotify_SetNotificationPositions = 0x0c / 4;

    private const uint DSBSTATUS_PLAYING = 1;

    // DSBCAPS_* flag values used by the two DSBUFFERDESC literals in Blaze.c.
    private const uint DSBCAPS_LOCSOFTWARE = 0x00000008;
    private const uint DSBCAPS_CTRLFREQUENCY = 0x00000020;
    private const uint DSBCAPS_CTRLPAN = 0x00000040;
    private const uint DSBCAPS_CTRLVOLUME = 0x00000080;
    private const uint DSBCAPS_CTRLPOSITIONNOTIFY = 0x00000100;

    private const uint DSBLOCK_ENTIREBUFFER = 0x00000002;
    private const uint DSBPLAY_LOOPING = 0x00000001;

    private const ushort WAVE_FORMAT_PCM = 1;
    private const ushort WAVE_FORMAT_ADPCM = 2; // Microsoft ADPCM

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct WAVEFORMATEX
    {
        public ushort wFormatTag;
        public ushort nChannels;
        public uint nSamplesPerSec;
        public uint nAvgBytesPerSec;
        public ushort nBlockAlign;
        public ushort wBitsPerSample;
        public ushort cbSize;
    }

    /// <summary>
    /// The MS-ADPCM `fmt ` chunk body: a <see cref="WAVEFORMATEX"/> followed by wSamplesPerBlock
    /// (+0x12), wNumCoef (+0x14) and aCoef[wNumCoef] (+0x16). Every field offset here is confirmed
    /// by the accesses in FUN_00531bb0/FUN_00531f70/FUN_005131d0.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct ADPCMWAVEFORMAT
    {
        public WAVEFORMATEX wfx;      // 0x00..0x11
        public ushort wSamplesPerBlock; // 0x12
        public ushort wNumCoef;         // 0x14
        // aCoef[] follows at 0x16 as pairs of shorts (iCoef1, iCoef2).
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct DSBUFFERDESC
    {
        public uint dwSize;
        public uint dwFlags;
        public uint dwBufferBytes;
        public uint dwReserved;
        public WAVEFORMATEX* lpwfxFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DSBPOSITIONNOTIFY
    {
        public uint dwOffset;
        public IntPtr hEventNotify;
    }

    // Ghidra: DAT_005db8e4 â€” the IDirectSound device object.
    // TODO(REVERSE): nothing in the reconstructed call graph assigns it â€” no DirectSoundCreate
    // call site has been located yet, so it is still null at runtime and every entry point below
    // takes its "no device" early-out. Same category of coverage gap as DAT_005db85c (TODO #6).
    private static void* s_DirectSound;

    // Ghidra: DAT_005db754 â€” a second COM object released just before the IDirectSound device in
    // FUN_00512ff0. TODO(REVERSE): only ever seen through Release (vtable 0x08), so its concrete
    // type isn't pinned down; position in the teardown order suggests the primary/listener buffer.
    private static void* s_DirectSoundPrimary;

    // Ghidra: DAT_005db900 â€” the streaming-music IDirectSoundBuffer created by FUN_005131d0.
    // (The ini key that drives its volume is "CdVol", hence ApplyCdVolume's name, but the buffer
    // itself is the ADPCM music stream, not a CD-audio object â€” FUN_005131d0 fills it from a
    // .WAV file on disk.)
    private static void* s_MusicStreamBuffer;

    // Ghidra: DAT_005db904 â€” the IDirectSoundNotify of the buffer above.
    private static void* s_MusicStreamNotify;

    // Ghidra: &DAT_005db758, 50 IDirectSoundBuffer pointers. The count is confirmed by
    // FUN_00512ea0's loop bound (`while ((int)piVar3 <= 0x5db81f)` starting at 0x005db758 â€”
    // exactly 50 dword slots).
    private static readonly void*[] s_SoundBuffers = new void*[50];

    /// <summary>Invokes a no-argument <c>HRESULT</c> method at <paramref name="vtableSlot"/> on a COM object.</summary>
    private static int InvokeComMethod(void* comObject, int vtableSlot)
    {
        void** vtable = *(void***)comObject;
        var method = (delegate* unmanaged[Stdcall]<void*, int>)vtable[vtableSlot];
        return method(comObject);
    }

    /// <summary>
    /// Ghidra: FUN_00512ea0 @ 0x00512ea0. Releases all 50 sound buffers. When
    /// <paramref name="stopPlayingBuffers"/> is set, a buffer that is still playing is stopped and
    /// then released; when it is clear, a still-playing buffer is skipped entirely and left
    /// allocated (Ghidra: the `goto LAB_00512edf` path) â€” reproduced exactly.
    /// </summary>
    private static void ReleaseAllSoundBuffers(bool stopPlayingBuffers)
    {
        for (int i = 0; i < s_SoundBuffers.Length; i++)
        {
            void* buffer = s_SoundBuffers[i];
            if (buffer == null) continue;

            uint status;
            void** vtable = *(void***)buffer;
            var getStatus = (delegate* unmanaged[Stdcall]<void*, uint*, int>)vtable[IDirectSoundBuffer_GetStatus];
            getStatus(buffer, &status);

            if ((status & DSBSTATUS_PLAYING) != 0)
            {
                if (!stopPlayingBuffers) continue; // leave a playing buffer alone, allocated
                InvokeComMethod(buffer, IDirectSoundBuffer_Stop);
            }

            InvokeComMethod(buffer, IDirectSoundBuffer_Release);
            s_SoundBuffers[i] = null;
        }
    }


    // Ghidra: TexFilterPossible_exref â€” Blaze.exe imports this `uint` as DATA (not a function)
    // from winsys3d.dll. Confirmed via winsys3d.c: it defines and reads/writes a plain global
    // `TexFilterPossible` (see Documentation/DLL/winsys3d.md). P/Invoke has no direct mechanism
    // for an imported data symbol, so it's resolved lazily via GetProcAddress instead.
    private static uint* s_TexFilterPossible;

    private static uint* GetTexFilterPossiblePtr()
    {
        if (s_TexFilterPossible == null)
        {
            IntPtr module = Win32.LoadLibraryA(ToAnsiZ("winsys3d.dll"));
            if (module != IntPtr.Zero)
            {
                s_TexFilterPossible = (uint*)Win32.GetProcAddress(module, ToAnsiZ("TexFilterPossible"));
            }
        }
        return s_TexFilterPossible;
    }

    private static string GetIniFilePath()
    {
        byte* buffer = stackalloc byte[0x104];
        Win32.GetModuleFileNameA(IntPtr.Zero, buffer, 0x104);
        string exePath = Marshal.PtrToStringAnsi((IntPtr)buffer) ?? string.Empty;
        int dot = exePath.LastIndexOf('.');
        if (dot >= 0) exePath = exePath[..dot];
        return exePath + IniFileExtension;
    }

    private static uint ReadIniInt(string key, int defaultValue, byte* iniPathAnsi)
    {
        byte* sectionAnsi = ToAnsiZ(IniSection);
        byte* keyAnsi = ToAnsiZ(key);
        uint result = Win32.GetPrivateProfileIntA(sectionAnsi, keyAnsi, defaultValue, iniPathAnsi);
        Free(sectionAnsi);
        Free(keyAnsi);
        return result;
    }

    /// <summary>
    /// Ghidra: <c>IDirectSoundBuffer::SetVolume</c> (vtable offset 0x3c), called from FUN_005137b0
    /// and FUN_00513800. The value range (-10000..0, hundredths of a decibel) matches
    /// DSBVOLUME_MIN/DSBVOLUME_MAX. The interface identity â€” an open question when those two
    /// functions were first reconstructed â€” is now confirmed; see the DirectSound banner above.
    /// </summary>
    private static void InvokeSetVolume(void* comObject, int attenuationHundredthDb)
    {
        if (comObject == null) return;
        void** vtable = *(void***)comObject;
        var setVolume = (delegate* unmanaged[Stdcall]<void*, int, int>)vtable[IDirectSoundBuffer_SetVolume];
        setVolume(comObject, attenuationHundredthDb);
    }

    // Ghidra: DAT_005db8ec â€” the current music-stream attenuation, computed by FUN_005137b0 and
    // read back by FUN_005131d0 when it starts a new stream.
    private static int s_MusicAttenuation;

    // Ghidra: DAT_005db8e8 â€” the current sound-effect attenuation, computed by FUN_00513800 and
    // read back by FUN_00513180 every time an effect is played.
    private static int s_SoundEffectAttenuation;

    /// <summary>
    /// Ghidra: FUN_005137b0 @ 0x005137b0. Converts the "CdVol" ini setting (0..128-ish) into a
    /// DirectSound-style attenuation, stores it as the current music attenuation and applies it
    /// to the streaming-music buffer, if one exists yet.
    /// </summary>
    private static void ApplyCdVolume()
    {
        if (s_CdVolumeIni == 0) return;
        s_MusicAttenuation = (int)(s_CdVolumeIni * 2500) / 128 - 2500;
        if (s_MusicStreamBuffer != null)
        {
            // Ghidra special-cases exactly -2000 (not this function's own -2500 floor) to force
            // full mute (-10000). Looks like it may be a copy/paste of FUN_00513800's -2000
            // constant in the original binary, but is preserved exactly as decompiled.
            int applied = s_MusicAttenuation == -2000 ? -10000 : s_MusicAttenuation;
            InvokeSetVolume(s_MusicStreamBuffer, applied);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00513800 @ 0x00513800. Converts the sound-volume ini setting into a
    /// DirectSound-style attenuation, stores it as the current effect attenuation and applies it
    /// to every allocated sound buffer.
    /// </summary>
    private static void ApplyAllSoundBufferVolumes()
    {
        s_SoundEffectAttenuation = (int)(s_SoundVolumeIni * 2000) / 128 - 2000;
        foreach (void* buffer in s_SoundBuffers)
        {
            if (buffer == null) continue;
            int applied = s_SoundEffectAttenuation == -2000 ? -10000 : s_SoundEffectAttenuation;
            InvokeSetVolume(buffer, applied);
        }
    }

    // =====================================================================
    // Sound-effect buffers (Ghidra: FUN_00513030 / FUN_00513110 / FUN_00513150 / FUN_00513180).
    //
    // The four functions form the complete "one-shot sample" API used by the sound-effect code at
    // 0x00537xxx: allocate a slot -> lock it -> fill it with 8-bit stereo PCM -> unlock -> play.
    // The five call sites read back (`FUN_00513110(slot, &ptr, &bytes)`, then writing pairs of
    // unsigned bytes with 0x80 as silence) independently confirm the buffer format literal below.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00513030 @ 0x00513030. Creates a <paramref name="bufferBytes"/>-sized
    /// 11025 Hz / 8-bit / stereo secondary buffer and parks it in the first free slot of
    /// <see cref="s_SoundBuffers"/>, returning that slot index (-1 on any failure).
    ///
    /// TODO(REVERSE): Ghidra's register attribution is wrong in the tail of this function â€” it
    /// writes `piVar3` (which it had just loaded from DAT_005db8e4, the *device*) into the slot
    /// table and releases the same `piVar3` when the table is full, while the newly created
    /// buffer sits unused in the `local_2c` output parameter of CreateSoundBuffer. Storing the
    /// device in a buffer slot would make ReleaseAllSoundBuffers call IDirectSoundBuffer methods
    /// on an IDirectSound and would release the device 50 times over, so the decompiled text
    /// cannot be what the binary does. Transcribed as the evident intent: the *created buffer*
    /// is what gets stored and released.
    /// </summary>
    private static int AllocateSoundBuffer(uint bufferBytes)
    {
        if (s_DirectSound == null) return -1;

        // Frees every slot whose buffer has finished playing; a still-playing one is kept.
        ReleaseAllSoundBuffers(stopPlayingBuffers: false);

        WAVEFORMATEX format;
        format.wFormatTag = WAVE_FORMAT_PCM;
        format.nChannels = 2;
        format.nSamplesPerSec = 0x2b11; // 11025
        format.nAvgBytesPerSec = 0x5622; // 22050 == 11025 * 2 channels * 1 byte
        format.nBlockAlign = 2;
        format.wBitsPerSample = 8;
        format.cbSize = 0;

        DSBUFFERDESC desc;
        desc.dwSize = 0x14; // Ghidra literal; == sizeof(DSBUFFERDESC) in the original 32-bit layout
        desc.dwFlags = DSBCAPS_CTRLFREQUENCY | DSBCAPS_CTRLPAN | DSBCAPS_CTRLVOLUME; // 0xe0 == DSBCAPS_CTRLDEFAULT
        desc.dwBufferBytes = bufferBytes;
        desc.dwReserved = 0;
        desc.lpwfxFormat = &format;

        void* buffer = null;
        void** vtable = *(void***)s_DirectSound;
        var createSoundBuffer = (delegate* unmanaged[Stdcall]<void*, DSBUFFERDESC*, void**, void*, int>)vtable[IDirectSound_CreateSoundBuffer];
        if (createSoundBuffer(s_DirectSound, &desc, &buffer, null) < 0) return -1;

        for (int slot = 0; slot < s_SoundBuffers.Length; slot++)
        {
            if (s_SoundBuffers[slot] == null)
            {
                s_SoundBuffers[slot] = buffer;
                return slot;
            }
        }

        // All 50 slots busy: throw the buffer away again.
        InvokeComMethod(buffer, IDirectSoundBuffer_Release);
        return -1;
    }

    /// <summary>
    /// Ghidra: FUN_00513110 @ 0x00513110 â€” <c>IDirectSoundBuffer::Lock</c> over the whole buffer
    /// (dwFlags 2 == DSBLOCK_ENTIREBUFFER; the second pointer/size pair is passed as NULL, which
    /// is legal precisely because the entire buffer is locked from offset 0).
    /// </summary>
    private static bool LockSoundBuffer(int slot, void** audioPtr, uint* audioBytes)
    {
        void* buffer = s_SoundBuffers[slot];
        if (buffer == null) return false;

        void** vtable = *(void***)buffer;
        var lockBuffer = (delegate* unmanaged[Stdcall]<void*, uint, uint, void**, uint*, void**, uint*, uint, int>)vtable[IDirectSoundBuffer_Lock];
        return lockBuffer(buffer, 0, 0, audioPtr, audioBytes, null, null, DSBLOCK_ENTIREBUFFER) >= 0;
    }

    /// <summary>Ghidra: FUN_00513150 @ 0x00513150 â€” <c>IDirectSoundBuffer::Unlock</c>.</summary>
    private static void UnlockSoundBuffer(int slot, void* audioPtr, uint audioBytes)
    {
        void* buffer = s_SoundBuffers[slot];
        if (buffer == null) return;

        void** vtable = *(void***)buffer;
        var unlockBuffer = (delegate* unmanaged[Stdcall]<void*, void*, uint, void*, uint, int>)vtable[IDirectSoundBuffer_Unlock];
        unlockBuffer(buffer, audioPtr, audioBytes, null, 0);
    }

    /// <summary>
    /// Ghidra: FUN_00513180 @ 0x00513180. Applies the current effect volume and starts playback.
    /// The `-2000 &lt; DAT_005db8e8` guard means a fully attenuated effect is never even started.
    ///
    /// Ghidra decompiled the looping flag as `unaff_retaddr != 0` (a register it failed to
    /// attribute to a parameter); all five call sites pass a literal second argument of 0, which
    /// is what identifies it as a real parameter rather than garbage.
    /// </summary>
    private static void PlaySoundBuffer(int slot, bool looping)
    {
        void* buffer = s_SoundBuffers[slot];
        if (buffer == null || s_SoundEffectAttenuation <= -2000) return;

        InvokeSetVolume(buffer, s_SoundEffectAttenuation);

        void** vtable = *(void***)buffer;
        var play = (delegate* unmanaged[Stdcall]<void*, uint, uint, uint, int>)vtable[IDirectSoundBuffer_Play];
        play(buffer, 0, 0, looping ? DSBPLAY_LOOPING : 0);
    }

    // =====================================================================
    // Streaming music (Ghidra: FUN_005131d0 "start", FUN_00513470 "load", FUN_005135d0 "stop",
    // FUN_00513860 "toggle", plus the MS-ADPCM codec at FUN_00531bb0/FUN_00531f70).
    //
    // What the subsystem does, end to end: a .WAV file holding Microsoft-ADPCM data is read whole
    // into a 5 MB scratch buffer; a 16-bit stereo DirectSound buffer exactly 8 notification
    // segments long is created; the first 80 source blocks are decoded straight into it; playback
    // starts looping and a worker thread then refills whichever segment has just been passed.
    //
    // Everything above is reconstructed. The worker thread itself is not â€” see the TODO(REVERSE)
    // at the CreateThread site in StartMusicStream.
    // =====================================================================

    // Ghidra: DAT_005631d4 â€” nonzero while music is enabled (toggled by FUN_00513860, and read
    // by the options UI at 0x0050f2xx). Recovered from Legacy/Blaze.exe @ 0x005631d4 as **1**:
    // music ships **enabled**. This previously defaulted to 0 on the assumption that the value was
    // unrecoverable, which silently made StartMusicStream take its early-out forever.
    private static uint s_MusicEnabled = 1;

    // Ghidra: DAT_005631d0 â€” nonzero while no stream is running. Note the inverted sense: it is
    // *cleared* at the end of a successful start and *set* at the top of a stop. Left at 0 (its
    // .bss default): a first StartMusicStream always takes the "different track" path, which
    // calls StopMusicStream and so sets the flag before it is tested.
    private static uint s_MusicStreamStopped;

    // Ghidra: DAT_005db8f0 â€” the whole-file scratch buffer. Its size is the literal 0x004c4b40
    // (5,000,000), which Ghidra rendered as `&LAB_004c4b40` because the constant happens to fall
    // inside the .text range; it is used identically as a plain size in the tail-zeroing loop.
    private const uint MusicFileBufferSize = 0x004c4b40;
    private static byte* s_MusicFileBuffer;

    // Ghidra: DAT_005db8f4 / DAT_005db8f8 / DAT_005db8fc â€” the `fmt ` chunk body, the `data`
    // chunk body and the `data` chunk size, all pointing into s_MusicFileBuffer.
    private static ADPCMWAVEFORMAT* s_MusicFormat;
    private static byte* s_MusicData;
    private static int s_MusicDataSize;

    // Ghidra: DAT_005db820 â€” the PCM WAVEFORMATEX describing the *decoded* stream. Kept in native
    // memory because the original is a .bss global whose address is handed to DirectSound.
    private static readonly WAVEFORMATEX* s_MusicOutputFormat = (WAVEFORMATEX*)NativeMemory.AllocZeroed((nuint)sizeof(WAVEFORMATEX));

    // Ghidra: DAT_005db8a0 (DSBPOSITIONNOTIFY[8]) and DAT_005db838 (the same 8 event handles,
    // kept separately so FUN_005135d0 can close them without walking the notify structs).
    private const int MusicNotifyCount = 8;
    private static readonly DSBPOSITIONNOTIFY* s_MusicNotifyPositions = (DSBPOSITIONNOTIFY*)NativeMemory.AllocZeroed(MusicNotifyCount * (nuint)sizeof(DSBPOSITIONNOTIFY));
    private static readonly IntPtr[] s_MusicNotifyEvents = new IntPtr[MusicNotifyCount];

    // Ghidra: DAT_005db858 â€” the "please exit" event signalled by FUN_005135d0.
    private static IntPtr s_MusicStopEvent;

    // Ghidra: DAT_005db880 / DAT_005db8e0 â€” the streaming worker thread and its thread id.
    private static IntPtr s_MusicStreamThread;
    private static uint s_MusicStreamThreadId;

    // Ghidra: DAT_005db860 / DAT_005db750 / DAT_005db89c. The first two are the streaming
    // worker's cursor (source block index reached so far) and its end marker (total number of
    // source blocks); the third is a flag set as playback starts.
    // TODO(REVERSE): all three are only ever *written* in the reconstructed graph â€” their readers
    // live in the missing worker thread body (LAB_00513660), so their exact roles above are read
    // off the values written (0x50 == the 80 blocks pre-decoded, and a block count derived from
    // the data-chunk size / nBlockAlign) rather than from consuming code.
    private static int s_MusicSourceBlockCursor;
    private static int s_MusicTotalBlocks;
    private static int s_MusicPlaying;

    // Ghidra: DAT_005db908 (the file currently loaded into s_MusicFileBuffer) and DAT_005dba08
    // (the most recently requested file, replayed when FUN_005131d0 is called with a null path).
    // 0x100 is the exact gap between DAT_005db908 and the next referenced global, DAT_005dba08.
    private const nuint MusicPathBufferSize = 0x100;
    private static readonly byte* s_LoadedMusicPath = (byte*)NativeMemory.AllocZeroed(MusicPathBufferSize);
    private static readonly byte* s_RequestedMusicPath = (byte*)NativeMemory.AllocZeroed(MusicPathBufferSize);

    // Ghidra: DAT_0054ded8, the riid handed to QueryInterface in FUN_005131d0. The GUID *bytes*
    // are .rdata and therefore absent from Blaze.c (same systemic blocker as the other data-only
    // symbols), but the interface identity is pinned by how the returned object is used â€” see the
    // DirectSound banner above. The value below is the documented IID_IDirectSoundNotify.
    private static readonly Guid IID_IDirectSoundNotify = new Guid("b0210783-89cd-11d0-af08-00a0c925cd16");

    // Ghidra: DAT_0056ac60 (16 shorts), DAT_0056ac80 and DAT_0056ac90 (7 shorts each) â€” read by
    // FUN_00531bb0 and FUN_00531f70 respectively.
    //
    // These are data-only globals, so their bytes are not in Blaze.c either; the values below are
    // the standard Microsoft-ADPCM tables. The identification is structural rather than a guess:
    // FUN_00531f70 only accepts a `fmt ` chunk with wFormatTag == 2 (WAVE_FORMAT_ADPCM),
    // wBitsPerSample == 4 and wNumCoef == 7, and then compares aCoef[0..6].iCoef1/iCoef2 against
    // DAT_0056ac80/DAT_0056ac90 â€” i.e. it verifies the file uses the standard coefficient set so
    // that the decoder's own hardcoded tables apply. The addresses corroborate the sizes exactly:
    // 16 shorts at 0x0056ac60 end at 0x0056ac80, and 7 shorts at 0x0056ac80 end at 0x0056ac8e,
    // padded to the next entry at 0x0056ac90.
    // TODO(REVERSE): confirm the byte values against the binary if it ever becomes available.
    private static readonly short[] MsAdpcmAdaptationTable =
    {
        230, 230, 230, 230, 307, 409, 512, 614,
        768, 614, 512, 409, 307, 230, 230, 230,
    };
    private static readonly short[] MsAdpcmCoefficient1 = { 256, 512, 0, 192, 240, 460, 392 };
    private static readonly short[] MsAdpcmCoefficient2 = { 0, -256, 0, 64, 0, -208, -232 };

    /// <summary>
    /// Ghidra: FUN_00531f70 @ 0x00531f70. Accepts only a `fmt ` chunk that is mono/stereo
    /// 4-bit Microsoft ADPCM using the standard 7-entry coefficient set.
    /// </summary>
    private static bool IsStandardMsAdpcmFormat(ADPCMWAVEFORMAT* format)
    {
        if (format == null) return false;
        if (format->wfx.wFormatTag != WAVE_FORMAT_ADPCM) return false;
        if (format->wfx.wBitsPerSample != 4) return false;
        if (format->wfx.nChannels == 0 || format->wfx.nChannels >= 3) return false;
        if (format->wNumCoef != 7) return false;

        short* aCoef = (short*)((byte*)format + 0x16);
        for (int i = 0; i < 7; i++)
        {
            if (aCoef[i * 2] != MsAdpcmCoefficient1[i]) return false;
            if (aCoef[i * 2 + 1] != MsAdpcmCoefficient2[i]) return false;
        }
        return true;
    }

    /// <summary>
    /// Writes one sample in the destination format and returns the advanced cursor. Ghidra emits
    /// this inline three times (initial sample2, initial sample1, decoded sample); the 8-bit form
    /// `(char)(sample >> 8) + -0x80` is the usual signed-16 to unsigned-8 rebias
    /// (0x8000 -&gt; 0x00, 0x0000 -&gt; 0x80, 0x7fff -&gt; 0xff).
    /// </summary>
    private static byte* WriteDecodedSample(byte* dest, short sample, int destBitsPerSample)
    {
        if (destBitsPerSample == 8)
        {
            *dest = (byte)((byte)((uint)(ushort)sample >> 8) - 0x80);
            return dest + 1;
        }
        *(short*)dest = sample;
        return dest + 2;
    }

    /// <summary>
    /// Ghidra: FUN_00531bb0 @ 0x00531bb0. Decodes Microsoft-ADPCM blocks from
    /// <paramref name="src"/> into <paramref name="dest"/> until <paramref name="srcByteBudget"/>
    /// source bytes have been consumed, and returns the number of bytes written.
    ///
    /// The block layout it walks â€” per channel: 1 predictor index, then a 16-bit iDelta, iSamp1
    /// and iSamp2 â€” plus the nibble-per-sample body with channels interleaved inside each byte,
    /// is standard MS-ADPCM, which is what makes the hardcoded tables above identifiable.
    ///
    /// The original keeps iDelta/iSamp1 in one `short[4]` (deltas at [0..1], samples at [2..3])
    /// and iSamp2 in a `uint[2]` shared with the byte counter. Because the format check restricts
    /// this to at most 2 channels those regions never overlap, so they are split into separate
    /// per-channel arrays here without changing behaviour.
    /// </summary>
    private static int DecodeMsAdpcm(ADPCMWAVEFORMAT* srcFormat, byte* src, WAVEFORMATEX* destFormat, byte* dest, uint srcByteBudget)
    {
        int numCoef = srcFormat->wNumCoef & 0xff;      // Ghidra reads only the low byte
        int channels = srcFormat->wfx.nChannels & 0xff;
        int destBits = destFormat->wBitsPerSample & 0xff;
        int samplesPerBlock = srcFormat->wSamplesPerBlock;
        short* aCoef = (short*)((byte*)srcFormat + 0x16);

        int decodedSamples = 0;
        if (srcByteBudget == 0) return 0;

        short* delta = stackalloc short[2];
        short* sample1 = stackalloc short[2];
        short* sample2 = stackalloc short[2];
        short* coef1 = stackalloc short[2];
        short* coef2 = stackalloc short[2];

        uint consumed = 0;
        uint blockHeaderBytes = (uint)(channels * 7);
        byte* s = src;
        byte* d = dest;

        while (true)
        {
            consumed += blockHeaderBytes;
            if (consumed > srcByteBudget) break;

            for (int ch = 0; ch < channels; ch++)
            {
                byte predictorIndex = *s++;
                if (predictorIndex >= numCoef)
                {
                    // Ghidra explicitly zeroes the sample count before bailing out here, unlike
                    // the "ran out of source" exit further down, which keeps what it decoded.
                    decodedSamples = 0;
                    goto finished;
                }
                coef1[ch] = aCoef[predictorIndex * 2];
                coef2[ch] = aCoef[predictorIndex * 2 + 1];
            }
            for (int ch = 0; ch < channels; ch++) { delta[ch] = *(short*)s; s += 2; }
            for (int ch = 0; ch < channels; ch++) { sample1[ch] = *(short*)s; s += 2; }
            for (int ch = 0; ch < channels; ch++) { sample2[ch] = *(short*)s; s += 2; }

            // The two samples carried in the block header are emitted before anything is decoded,
            // iSamp2 first (it is the older of the two).
            for (int ch = 0; ch < channels; ch++) d = WriteDecodedSample(d, sample2[ch], destBits);
            for (int ch = 0; ch < channels; ch++) d = WriteDecodedSample(d, sample1[ch], destBits);
            decodedSamples += 2;

            bool readNewByte = true;
            int pendingLowNibble = 0;
            for (int sampleIndex = 2; sampleIndex < samplesPerBlock; sampleIndex++)
            {
                for (int ch = 0; ch < channels; ch++)
                {
                    int nibble;
                    if (readNewByte)
                    {
                        consumed++;
                        if (consumed > srcByteBudget) goto finished;
                        int packed = (sbyte)*s++;
                        nibble = (short)packed >> 4;               // high nibble, sign-extended
                        pendingLowNibble = (packed << 0x1c) >> 0x1c; // low nibble, sign-extended
                        readNewByte = false;
                    }
                    else
                    {
                        nibble = pendingLowNibble;
                        readNewByte = true;
                    }

                    short oldDelta = delta[ch];
                    short newDelta = (short)(int)((uint)(MsAdpcmAdaptationTable[nibble & 0xf] * oldDelta) >> 8);
                    delta[ch] = newDelta < 0x10 ? (short)0x10 : newDelta;

                    int predicted = ((coef2[ch] * sample2[ch] + coef1[ch] * sample1[ch]) >> 8) + oldDelta * nibble;
                    if (predicted >= 0x8000) predicted = 0x7fff;
                    else if (predicted < -0x8000) predicted = -0x8000;

                    d = WriteDecodedSample(d, (short)predicted, destBits);
                    sample2[ch] = sample1[ch];
                    sample1[ch] = (short)predicted;
                }
                decodedSamples++;
            }

            if (consumed >= srcByteBudget) break;
        }

    finished:
        // Ghidra: `(((destBits & 0xff) >> 3) << (channels >> 1 & 0x1f)) * decodedSamples` â€” bytes
        // per sample, doubled for stereo, times the number of sample frames emitted.
        return ((destBits >> 3) << (channels >> 1)) * decodedSamples;
    }

    /// <summary>
    /// Ghidra: FUN_00513470 @ 0x00513470. Reads a Microsoft-ADPCM .WAV whole into the 5 MB
    /// scratch buffer and locates its `fmt ` and `data` chunks. The header is read first (0x400
    /// bytes) so the format can be rejected before paying for the rest of the file.
    /// </summary>
    private static bool LoadMusicWaveFile(byte* path)
    {
        if (s_MusicFileBuffer == null) s_MusicFileBuffer = (byte*)Malloc(MusicFileBufferSize);

        IntPtr file = Win32.CreateFileA(path, 0x80000000 /* GENERIC_READ */, 1 /* FILE_SHARE_READ */, null,
            3 /* OPEN_EXISTING */, 0x80 /* FILE_ATTRIBUTE_NORMAL */, IntPtr.Zero);
        if (file == (IntPtr)(-1)) return false;

        Win32.lstrcpyA(s_LoadedMusicPath, path);

        uint fileSize = Win32.GetFileSize(file, null);
        uint bytesRead;
        Win32.ReadFile(file, s_MusicFileBuffer, 0x400, &bytesRead, null);

        // "RIFF" / "WAVE" / "fmt " (little-endian dwords, exactly as compared in Blaze.c).
        if (*(uint*)s_MusicFileBuffer == 0x46464952 &&
            *(uint*)(s_MusicFileBuffer + 8) == 0x45564157 &&
            *(uint*)(s_MusicFileBuffer + 12) == 0x20746d66)
        {
            s_MusicFormat = (ADPCMWAVEFORMAT*)(s_MusicFileBuffer + 20);
            if (IsStandardMsAdpcmFormat(s_MusicFormat))
            {
                // Continues from the current file position (0x400), so this reads the remainder.
                // TODO(REVERSE): the original subtracts unconditionally â€” a file smaller than
                // 0x400 bytes underflows the length, and one larger than 5 MB overruns the
                // buffer. Both are original behaviour and are left as-is rather than "fixed",
                // but they are why the tail-zeroing below is guarded.
                Win32.ReadFile(file, s_MusicFileBuffer + 0x400, fileSize - 0x400, &bytesRead, null);

                // Walk the chunk list from `fmt ` until "data".
                byte* chunk = s_MusicFileBuffer + 12;
                while (*(uint*)chunk != 0x61746164) chunk += *(uint*)(chunk + 4) + 8;
                s_MusicData = chunk + 8;
                s_MusicDataSize = *(int*)(chunk + 4);

                Win32.CloseHandle(file);

                // Clears everything past the file so a shorter track cannot play back the tail of
                // a longer previous one. The original does this unguarded; see the TODO above.
                if (fileSize < MusicFileBufferSize)
                {
                    NativeMemory.Clear(s_MusicFileBuffer + fileSize, MusicFileBufferSize - fileSize);
                }
                return true;
            }
        }

        Win32.CloseHandle(file);
        return false;
    }

    /// <summary>
    /// Ghidra: FUN_005131d0 @ 0x005131d0. Starts (or restarts) the streaming music.
    /// A null <paramref name="path"/> means "replay whatever was last requested".
    ///
    /// Return values follow the original exactly: 1 when nothing needed doing (music disabled, or
    /// the requested track is already streaming), 0 when a stream was set up or the file failed to
    /// load, and -1 when there is no DirectSound device.
    /// </summary>
    private static int StartMusicStream(byte* path)
    {
        if (path == null) path = s_RequestedMusicPath;
        else Win32.lstrcpyA(s_RequestedMusicPath, path);

        if (s_MusicEnabled == 0) return 1;

        // Ghidra: __strcmpi â€” a different track than the one currently in the scratch buffer has
        // to stop the current stream and reload before it can be started.
        if (Win32.lstrcmpiA(path, s_LoadedMusicPath) != 0)
        {
            StopMusicStream();
            // Ghidra shows `FUN_00513470()` with no argument, but the callee's only use of its
            // parameter is the CreateFileA path and this is its sole call site â€” another instance
            // of the parameter-recovery weakness noted in REVERSE_STATUS.md.
            if (!LoadMusicWaveFile(path)) return 0;
        }

        if (s_MusicStreamStopped == 0) return 1;
        if (s_DirectSound == null) return -1;

        // The decoded stream is always described as 16-bit stereo at the source sample rate, even
        // for a mono source â€” preserved as decompiled.
        s_MusicOutputFormat->wFormatTag = WAVE_FORMAT_PCM;
        s_MusicOutputFormat->nChannels = 2;
        s_MusicOutputFormat->nSamplesPerSec = s_MusicFormat->wfx.nSamplesPerSec;
        s_MusicOutputFormat->nAvgBytesPerSec = s_MusicFormat->wfx.nSamplesPerSec << 2;
        s_MusicOutputFormat->nBlockAlign = 4;
        s_MusicOutputFormat->wBitsPerSample = 0x10;
        s_MusicOutputFormat->cbSize = 0;

        int samplesPerBlock = s_MusicFormat->wSamplesPerBlock;
        int sourceBlockAlign = s_MusicFormat->wfx.nBlockAlign;

        DSBUFFERDESC desc;
        desc.dwSize = 0x14;
        desc.dwFlags = DSBCAPS_LOCSOFTWARE | DSBCAPS_CTRLFREQUENCY | DSBCAPS_CTRLPAN |
                       DSBCAPS_CTRLVOLUME | DSBCAPS_CTRLPOSITIONNOTIFY; // 0x1e8
        // 0x140 == 8 notification segments x 0x28 bytes per sample-per-block, i.e. exactly the
        // 80 source blocks pre-decoded below (each block decodes to samplesPerBlock * 4 bytes).
        desc.dwBufferBytes = (uint)(samplesPerBlock * 0x140);
        desc.dwReserved = 0;
        desc.lpwfxFormat = s_MusicOutputFormat;

        void* streamBuffer = null;
        void** deviceVtable = *(void***)s_DirectSound;
        var createSoundBuffer = (delegate* unmanaged[Stdcall]<void*, DSBUFFERDESC*, void**, void*, int>)deviceVtable[IDirectSound_CreateSoundBuffer];
        if (createSoundBuffer(s_DirectSound, &desc, &streamBuffer, null) < 0) return 0;

        s_MusicStreamBuffer = streamBuffer;
        void** bufferVtable = *(void***)streamBuffer;

        void* notify = null;
        Guid iid = IID_IDirectSoundNotify;
        var queryInterface = (delegate* unmanaged[Stdcall]<void*, Guid*, void**, int>)bufferVtable[IUnknown_QueryInterface];
        queryInterface(streamBuffer, &iid, &notify);
        s_MusicStreamNotify = notify;

        for (int i = 0; i < MusicNotifyCount; i++)
        {
            IntPtr signal = Win32.CreateEventA(null, 0, 0, null);
            s_MusicNotifyPositions[i].hEventNotify = signal;
            s_MusicNotifyEvents[i] = signal;
            s_MusicNotifyPositions[i].dwOffset = (uint)(samplesPerBlock * i * 0x28);
        }
        s_MusicStopEvent = Win32.CreateEventA(null, 0, 0, null);

        void** notifyVtable = *(void***)notify;
        var setNotificationPositions = (delegate* unmanaged[Stdcall]<void*, uint, DSBPOSITIONNOTIFY*, int>)notifyVtable[IDirectSoundNotify_SetNotificationPositions];
        setNotificationPositions(notify, MusicNotifyCount, s_MusicNotifyPositions);

        void* audioPtr = null;
        uint audioBytes = 0;
        var lockBuffer = (delegate* unmanaged[Stdcall]<void*, uint, uint, void**, uint*, void**, uint*, uint, int>)bufferVtable[IDirectSoundBuffer_Lock];
        lockBuffer(streamBuffer, 0, 0, &audioPtr, &audioBytes, null, null, DSBLOCK_ENTIREBUFFER);

        // Prime the whole buffer with the first 80 source blocks.
        DecodeMsAdpcm(s_MusicFormat, s_MusicData, s_MusicOutputFormat, (byte*)audioPtr, (uint)(sourceBlockAlign * 0x50));
        s_MusicSourceBlockCursor = 0x50;
        s_MusicTotalBlocks = (s_MusicDataSize - 1) / sourceBlockAlign + 1;

        var unlockBuffer = (delegate* unmanaged[Stdcall]<void*, void*, uint, void*, uint, int>)bufferVtable[IDirectSoundBuffer_Unlock];
        unlockBuffer(streamBuffer, audioPtr, audioBytes, null, 0);

        s_MusicPlaying = 1;
        InvokeSetVolume(streamBuffer, s_MusicAttenuation);

        var play = (delegate* unmanaged[Stdcall]<void*, uint, uint, uint, int>)bufferVtable[IDirectSoundBuffer_Play];
        play(streamBuffer, 0, 0, DSBPLAY_LOOPING);

        s_MusicStreamStopped = 0;

        // TODO(REVERSE): the streaming worker thread's body is missing. Blaze.c passes
        // `&LAB_00513660` to CreateThread but contains no function at that address â€” Ghidra only
        // ever saw it address-taken, the same class of gap as the movie WndProc (TODO #14) and
        // the ordinal-790 device-lost callback (TODO #8). Calling CreateThread with an address we
        // cannot reconstruct would jump into nothing, so the call is skipped exactly like
        // CreateMoviePlayerWindow skips CreateWindowExA, and the handle stays zero.
        //
        // Consequence: everything above is real, so a track would start playing, but nothing ever
        // refills the buffer â€” the primed 80 blocks would loop forever instead of streaming on.
        // s_MusicSourceBlockCursor / s_MusicTotalBlocks / s_MusicPlaying / s_MusicNotifyEvents /
        // s_MusicStopEvent exist for that thread and currently have no reader.
        s_MusicStreamThread = IntPtr.Zero;
        s_MusicStreamThreadId = 0;

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_005135d0 @ 0x005135d0. Signals the worker thread to exit, waits for it, then
    /// tears down the streaming buffer, its notify interface and the 8 notification events.
    /// </summary>
    private static int StopMusicStream()
    {
        if (s_MusicStreamStopped == 0)
        {
            s_MusicStreamStopped = 1;
            Win32.SetEvent(s_MusicStopEvent);

            // With the worker thread not created (see StartMusicStream), the handle is zero and
            // this returns WAIT_FAILED immediately instead of blocking â€” kept as decompiled.
            Win32.WaitForSingleObject(s_MusicStreamThread, 0xffffffff);
            s_MusicStreamThread = IntPtr.Zero;

            if (s_MusicStreamBuffer != null) InvokeComMethod(s_MusicStreamBuffer, IDirectSoundBuffer_Release);
            if (s_MusicStreamNotify != null) InvokeComMethod(s_MusicStreamNotify, IDirectSoundNotify_Release);
            s_MusicStreamBuffer = null;
            s_MusicStreamNotify = null;

            // Only the 8 notification events are closed (Ghidra's loop bound is 0x5db85c, i.e. it
            // stops just before DAT_005db858): the stop event itself is leaked on every restart.
            // Preserved rather than corrected.
            for (int i = 0; i < MusicNotifyCount; i++) Win32.CloseHandle(s_MusicNotifyEvents[i]);
        }
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_00513860 @ 0x00513860. Flips the music-enabled flag and either stops the
    /// current stream or restarts the last requested track.
    /// </summary>
    private static void ToggleMusicEnabled()
    {
        s_MusicEnabled = s_MusicEnabled == 0 ? 1u : 0u;
        if (s_MusicEnabled == 0) StopMusicStream();
        else StartMusicStream(null);
    }

    // =====================================================================
    // Sound-effect layer (Ghidra: FUN_0053cea0 dispatcher + the five FUN_0053dxxx resamplers).
    //
    // PlaySoundEffect(soundId, pitch, gainLeft, gainRight) is the game's single sound entry point
    // â€” around 200 call sites all over Blaze.c. It looks the sample up in the sound directory,
    // then picks one of five hand-written resamplers by pitch band. Each resampler allocates a
    // DirectSound buffer sized for its output, converts the mono 8-bit source into 8-bit stereo
    // while applying the two gains, pads the remainder with silence and plays it.
    //
    // `pitch` is 1.10 fixed point with 0x400 as unity: 0x800 plays an octave up, 0x200 an octave
    // down. That reading is not a guess â€” it falls out of the dispatcher's own thresholds matching
    // each resampler's exact rate ratio, e.g. 0x800 -> Decimated(step 2), 0xc00 -> Decimated(3),
    // 0x600 -> DroppingOneIn(2) (ratio 3/2), 0x555 -> DroppingOneIn(3) (4/3), 0x200 -> Doubled
    // (1/2). Every band lines up.
    //
    // `gainLeft`/`gainRight` are 1.12 fixed point with 0x1000 as unity, halved by the dispatcher
    // before use; call sites pass unequal values to pan and values above 0x1000 to over-drive.
    // =====================================================================

    /// <summary>
    /// One entry of the sound directory at Ghidra `DAT_005e8340`: a byte offset into the sample
    /// bank selected by the sound id, and the sample's length in bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct SoundDirectoryEntry
    {
        public int Offset;
        public int Length;
    }

    private const int SoundGainUnity = 0x1000;

    /// <summary>
    /// Applies a 1.12 fixed-point gain to one unsigned 8-bit PCM sample. Ghidra emits this inline
    /// a dozen times across the five resamplers, always with the same asymmetric clamp to
    /// [-0x7f, +0x7f] (not [-0x80, +0x7f]) before re-biasing by 0x80.
    /// </summary>
    private static byte ApplySoundGain(byte sample, int gain)
    {
        int value = (sample - 0x80) * gain >> 0xc;
        if (value > 0x7f) value = 0x7f;
        if (value < -0x7f) value = -0x7f;
        return (byte)(value + 0x80);
    }

    /// <summary>
    /// The tail shared by all five resamplers: pad whatever is left of the buffer with silence
    /// (0x80 is the unsigned-8-bit zero level), unlock and play.
    ///
    /// Note the guard, which is original behaviour and not a defensive addition: if the converted
    /// audio reached or passed the end of the buffer, the original skips Unlock *and* Play
    /// entirely, leaving the buffer locked and silent. The size formulas all carry a +100 sample
    /// slack that normally keeps this from happening.
    /// </summary>
    private static void FinishAndPlayResampledBuffer(int slot, byte* dest, byte* cursor, int bufferBytes)
    {
        if (cursor >= dest + bufferBytes) return;

        do { *cursor++ = 0x80; } while (cursor < dest + bufferBytes);
        UnlockSoundBuffer(slot, dest, (uint)bufferBytes);
        PlaySoundBuffer(slot, looping: false);
    }

    /// <summary>
    /// Ghidra: FUN_0053d1a0 @ 0x0053d1a0. Keeps every <c>stepMinusOne + 1</c>-th source sample,
    /// i.e. plays the sample back <c>step</c> times faster. `step == 1` is the identity path used
    /// for unity pitch.
    ///
    /// Ghidra types the first two parameters as <c>byte*</c> because the compiler reused their
    /// stack slots for the Lock output pointer and byte count; only <paramref name="src"/> is
    /// really a pointer.
    /// </summary>
    private static void PlaySampleDecimated(int stepMinusOne, byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int step = stepMinusOne + 1;
        int bufferBytes = (int)((srcLength / step + 100U) & 0xffffffe) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 4);

        if (gainLeft == SoundGainUnity && gainRight == SoundGainUnity)
        {
            for (; src < end; src += step)
            {
                cursor[0] = *src;
                cursor[1] = *src;
                cursor += 2;
            }
        }
        else
        {
            for (; src < end; src += step)
            {
                cursor[0] = ApplySoundGain(*src, gainLeft);
                cursor[1] = ApplySoundGain(*src, gainRight);
                cursor += 2;
            }
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053d2c0 @ 0x0053d2c0. Emits <paramref name="n"/> frames then skips one source
    /// sample, i.e. plays back at ratio (n+1)/n â€” the fine pitch-up steps between unity and
    /// double speed.
    ///
    /// The inner loop reproduces Ghidra's `for (; src = src + 1, i != 0; i--)` exactly: the source
    /// pointer advances as part of the *condition*, so each emitted frame reads the already
    /// advanced sample and the loop leaves the pointer one past the n frames it wrote.
    /// </summary>
    private static void PlaySampleDroppingOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int bufferBytes = (int)((uint)(srcLength + (100 - srcLength / (n + 1))) & 0xffffffe) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 5 - n);
        bool unityGain = gainLeft == SoundGainUnity && gainRight == SoundGainUnity;

        while (src < end)
        {
            int remaining = n;
            while (true)
            {
                src++;
                if (remaining == 0) break;

                if (unityGain)
                {
                    cursor[0] = *src;
                    cursor[1] = *src;
                }
                else
                {
                    cursor[0] = ApplySoundGain(*src, gainLeft);
                    cursor[1] = ApplySoundGain(*src, gainRight);
                }
                cursor += 2;
                remaining--;
            }
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053d420 @ 0x0053d420. Per group of n+1 source samples emits n+2 frames â€” the
    /// first sample, an interpolated frame midway to the next, then n more â€” i.e. plays back at
    /// ratio (n+1)/(n+2), the fine pitch-*down* steps between unity and half speed.
    /// </summary>
    private static void PlaySampleInsertingOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int bufferBytes = (int)((uint)(srcLength + srcLength / (n + 1) + 100) & 0xffffffe) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 5 - n);
        bool unityGain = gainLeft == SoundGainUnity && gainRight == SoundGainUnity;

        while (src < end)
        {
            byte midpoint = (byte)(((uint)*src + src[1]) / 2);
            if (unityGain)
            {
                cursor[0] = *src;
                cursor[1] = *src;
                cursor[2] = midpoint;
                cursor[3] = midpoint;
            }
            else
            {
                cursor[0] = ApplySoundGain(*src, gainLeft);
                cursor[1] = ApplySoundGain(*src, gainRight);
                cursor[2] = ApplySoundGain(midpoint, gainLeft);
                cursor[3] = ApplySoundGain(midpoint, gainRight);
            }
            cursor += 4;

            int remaining = n;
            while (true)
            {
                src++;
                if (remaining == 0) break;

                if (unityGain)
                {
                    cursor[0] = *src;
                    cursor[1] = *src;
                }
                else
                {
                    cursor[0] = ApplySoundGain(*src, gainLeft);
                    cursor[1] = ApplySoundGain(*src, gainRight);
                }
                cursor += 2;
                remaining--;
            }
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053d640 @ 0x0053d640. Per group of n source samples emits 2n-1 frames â€” one
    /// plain frame, then n-1 pairs of (sample, interpolated midpoint) â€” i.e. ratio n/(2n-1), the
    /// fine steps just above half speed.
    /// </summary>
    private static void PlaySampleDoubledExceptOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int bufferBytes = (int)((uint)(srcLength * 2 + 100 - srcLength / (n + 1)) & 0xffffffe) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 5 - n);
        bool unityGain = gainLeft == SoundGainUnity && gainRight == SoundGainUnity;

        while (src < end)
        {
            if (unityGain)
            {
                cursor[0] = *src;
                cursor[1] = *src;
            }
            else
            {
                cursor[0] = ApplySoundGain(*src, gainLeft);
                cursor[1] = ApplySoundGain(*src, gainRight);
            }
            cursor += 2;

            int remaining = n - 1;
            while (true)
            {
                src++;
                if (remaining == 0) break;

                byte midpoint = (byte)(((uint)*src + src[1]) / 2);
                if (unityGain)
                {
                    cursor[0] = *src;
                    cursor[1] = *src;
                    cursor[2] = midpoint;
                    cursor[3] = midpoint;
                }
                else
                {
                    cursor[0] = ApplySoundGain(*src, gainLeft);
                    cursor[1] = ApplySoundGain(*src, gainRight);
                    cursor[2] = ApplySoundGain(midpoint, gainLeft);
                    cursor[3] = ApplySoundGain(midpoint, gainRight);
                }
                cursor += 4;
                remaining--;
            }
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053d880 @ 0x0053d880. Emits two frames per source sample (the sample and an
    /// interpolated midpoint) â€” exactly half speed, one octave down.
    /// </summary>
    private static void PlaySampleDoubled(byte* src, int srcLength, int gainLeft, int gainRight)
    {
        int bufferBytes = (srcLength * 2 + 100) * 2;

        int slot = AllocateSoundBuffer((uint)bufferBytes);
        if (slot == -1) return;

        void* locked;
        uint lockedBytes;
        if (!LockSoundBuffer(slot, &locked, &lockedBytes)) return;

        byte* dest = (byte*)locked;
        byte* cursor = dest;
        byte* end = src + (srcLength - 4);
        bool unityGain = gainLeft == SoundGainUnity && gainRight == SoundGainUnity;

        for (; src < end; src++)
        {
            byte midpoint = (byte)(((uint)*src + src[1]) / 2);
            if (unityGain)
            {
                cursor[0] = *src;
                cursor[1] = *src;
                cursor[2] = midpoint;
                cursor[3] = midpoint;
            }
            else
            {
                cursor[0] = ApplySoundGain(*src, gainLeft);
                cursor[1] = ApplySoundGain(*src, gainRight);
                cursor[2] = ApplySoundGain(midpoint, gainLeft);
                cursor[3] = ApplySoundGain(midpoint, gainRight);
            }
            cursor += 4;
        }

        FinishAndPlayResampledBuffer(slot, dest, cursor, bufferBytes);
    }

    /// <summary>
    /// Ghidra: FUN_0053cea0 @ 0x0053cea0. The game's sound entry point: resolves
    /// <paramref name="soundId"/> in the sound directory and dispatches to the resampler whose
    /// rate ratio matches <paramref name="pitch"/> (1.10 fixed point, 0x400 == unity).
    /// </summary>
    private static void PlaySoundEffect(int soundId, int pitch, int gainLeft, int gainRight)
    {
        gainLeft >>= 1;
        gainRight >>= 1;

        void* bank;
        if (soundId < SoundDirectoryBaseCount) bank = s_BaseSoundBank;
        else if (soundId <= 0x4c) bank = s_LevelSoundBank;
        else return; // ids past the end of the directory are silently ignored

        // Both halves of the directory are indexed off the same base; only the bank differs.
        // No null check on the bank here â€” the original has none either, and none is needed: the
        // pointer is only ever dereferenced after a sound buffer has been allocated successfully,
        // which cannot happen before RunApplication has allocated both banks.
        SoundDirectoryEntry entry = s_SoundDirectory[soundId];
        byte* src = (byte*)bank + entry.Offset;
        int srcLength = entry.Length;

        // The bands below are transcribed one-for-one from Blaze.c's if-ladder. Each threshold is
        // the lower bound of the band whose resampler ratio approximates that pitch.
        if (pitch > 0x1bff) { PlaySampleDecimated(6, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x17ff) { PlaySampleDecimated(5, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x13ff) { PlaySampleDecimated(4, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0fff) { PlaySampleDecimated(3, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0bff) { PlaySampleDecimated(2, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x07ff) { PlaySampleDecimated(1, src, srcLength, gainLeft, gainRight); return; }

        if (pitch > 0x05ff) { PlaySampleDroppingOneIn(2, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0554) { PlaySampleDroppingOneIn(3, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x04ff) { PlaySampleDroppingOneIn(4, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x04d0) { PlaySampleDroppingOneIn(5, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x04b3) { PlaySampleDroppingOneIn(6, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0498) { PlaySampleDroppingOneIn(7, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x048a) { PlaySampleDroppingOneIn(8, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0471) { PlaySampleDroppingOneIn(9, src, srcLength, gainLeft, gainRight); return; }

        if (pitch >= 0x0400)
        {
            // Unity pitch: decimation by 1, i.e. a straight mono -> stereo copy.
            PlaySampleDecimated(0, src, srcLength, gainLeft, gainRight);
            return;
        }

        if (pitch > 0x03ce) { PlaySampleInsertingOneIn(0x14, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x03a1) { PlaySampleInsertingOneIn(10, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0371) { PlaySampleInsertingOneIn(6, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0354) { PlaySampleInsertingOneIn(5, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0332) { PlaySampleInsertingOneIn(4, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0301) { PlaySampleInsertingOneIn(3, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x02a9) { PlaySampleInsertingOneIn(2, src, srcLength, gainLeft, gainRight); return; }

        if (pitch >= 0x0268) { PlaySampleDoubledExceptOneIn(2, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0248) { PlaySampleDoubledExceptOneIn(3, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0237) { PlaySampleDoubledExceptOneIn(4, src, srcLength, gainLeft, gainRight); return; }
        if (pitch > 0x0228) { PlaySampleDoubledExceptOneIn(6, src, srcLength, gainLeft, gainRight); return; }
        if (pitch < 0x021a) { PlaySampleDoubled(src, srcLength, gainLeft, gainRight); return; }

        // Ghidra renders this one call as `FUN_0053d640(9)` with its four remaining arguments
        // dropped â€” a register-attribution miss, not a different signature: it is the last arm of
        // the same ladder and every sibling arm passes the identical (src, length, gains) set.
        PlaySampleDoubledExceptOneIn(9, src, srcLength, gainLeft, gainRight);
    }

    /// <summary>Ghidra: FUN_0053db80 @ 0x0053db80 â€” fixed UI sound, id 0x38.</summary>
    private static void PlayUiSound38() => PlaySoundEffect(0x38, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dba0 @ 0x0053dba0 â€” fixed UI sound, id 0x39.</summary>
    private static void PlayUiSound39() => PlaySoundEffect(0x39, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053db60 @ 0x0053db60 â€” fixed UI sound, id 0x3a.</summary>
    private static void PlayUiSound3A() => PlaySoundEffect(0x3a, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dbc0 @ 0x0053dbc0 â€” fixed UI sound, id 0x3b.</summary>
    private static void PlayUiSound3B() => PlaySoundEffect(0x3b, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dbc0 â€” le bip d'action refusÃ©e.</summary>
    private static void PlayUiSoundError() => PlaySoundEffect(0x3b, 0x400, 0x2000, 0x2000);
}
