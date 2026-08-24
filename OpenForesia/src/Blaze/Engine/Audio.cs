// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// `Blaze` porte encore ce que cette classe appelle : les aides CRT (Malloc, ToAnsiZ, ...), les
// enveloppes Win32, et les deux banques d'echantillons declarees dans Platform/Startup.cs.
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

/// <summary>
/// Le son : DirectSound, les 50 tampons d'effets, le flux de musique MS-ADPCM et les quatre bips
/// d'interface (Ghidra: 0x00512ea0..0x00513880, 0x00531bb0/0x00531f70, 0x0053cea0..0x0053dbc0).
///
/// <para>
/// Un seul membre reste <c>static</c> : <c>InvokeComMethod</c>. Ce n'est pas une contrainte de
/// plateforme mais un fait de portee - il ne touche que ses parametres, et
/// <c>Platform/Startup.cs</c> s'en sert pour liberer le peripherique DirectInput, qui n'a rien a
/// voir avec le son. Le garder statique evite de faire naitre tout le sous-systeme audio pour
/// liberer une manette.
/// </para>
/// <para>
/// <c>ShutdownStage3</c> (<c>FUN_00512ff0</c>) a rejoint ce fichier depuis
/// <c>Platform/Startup.cs</c> : son adresse tombe dans la plage audio, et son corps ne touche que
/// des membres prives d'ici. L'y laisser aurait oblige a exposer <c>s_DirectSound</c> EN ECRITURE
/// a un autre fichier - exactement la fuite que cette conversion supprime. Le corps n'a pas
/// change ; l'oracle indexe par nom seul, un demenagement ne le fait donc pas sonner.
/// </para>
/// <para>
/// <b>La signature dit maintenant tout ce que cette classe touche.</b> Les trois globales de son
/// qu'elle empruntait a d'autres fichiers - <c>s_BaseSoundBank_00604920</c>, <c>s_LevelSoundBank_00600900</c> et
/// <c>s_SoundDirectory_005e8340</c> - ont rejoint <see cref="GameMemory"/> le 2026-08-21, a la conversion
/// de <see cref="Resources"/>. Elles n'ont pas de suffixe VA dans leur nom C# et la regle de tri
/// les avait donc classees « echafaudage » ; ce sont pourtant <c>DAT_00604920</c>,
/// <c>DAT_00600900</c> et <c>DAT_005e8340</c>, du <c>.data</c> pur. C'est le nom qui mentait.
/// </para>
/// <para>
/// <b>TODO(REVERSE) — defaut d'aliasing connu, volontairement NON corrige ici :</b>
/// <c>Screens/State03/WorldMap.cs</c> alloue <c>s_SoundBankHeader_005e8520</c> a part, alors que
/// <c>0x005e8520</c> vaut exactement <c>0x005e8340 + 0x1e0</c> - c'est la queue du repertoire de
/// sons, pas un tableau distinct. Consequence attendue : <see cref="PlaySoundEffect"/> indexe
/// <c>mem.s_SoundDirectory_005e8340</c> jusqu'a l'id <c>0x4c</c>, donc dans une queue que rien ne remplit,
/// et les effets de niveau (<c>0x3c..0x4c</c>) lisent un decalage et une longueur nuls. Le
/// reparer CHANGE le comportement : cela releve d'un changement separe, comme les cinq autres
/// defauts d'aliasing recenses.
/// </para>
/// <para>
/// Instanciee UNE FOIS, par <c>Subsystems.cs</c>.
/// </para>
/// </summary>
internal sealed unsafe class Audio(GameMemory mem, Model3d mdl)
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
    internal const int IDirectSound_Release = 0x08 / 4;
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

    // Ghidra: DAT_005db8e4 - the IDirectSound device object. Cree par InitAudioDevice
    // (LAB_00512ef0), le rappel que winsys3d declenche pendant Ordinal_789. C'etait la lacune
    // TODO(REVERSE) #26, levee le 2026-08-24 en meme temps que #6 (DAT_005db85c, la fenetre
    // principale) : les deux sont ecrites par cette meme fonction.
    private void* s_DirectSound;

    // Ghidra: DAT_005db754 â€” a second COM object released just before the IDirectSound device in
    // FUN_00512ff0. TODO(REVERSE): only ever seen through Release (vtable 0x08), so its concrete
    // type isn't pinned down; position in the teardown order suggests the primary/listener buffer.
    private void* s_DirectSoundPrimary;

    // Ghidra: DAT_005db900 â€” the streaming-music IDirectSoundBuffer created by FUN_005131d0.
    // (The ini key that drives its volume is "CdVol", hence ApplyCdVolume's name, but the buffer
    // itself is the ADPCM music stream, not a CD-audio object â€” FUN_005131d0 fills it from a
    // .WAV file on disk.)
    private void* s_MusicStreamBuffer;

    // Ghidra: DAT_005db904 â€” the IDirectSoundNotify of the buffer above.
    private void* s_MusicStreamNotify;

    // Ghidra: &DAT_005db758, 50 IDirectSoundBuffer pointers. The count is confirmed by
    // FUN_00512ea0's loop bound (`while ((int)piVar3 <= 0x5db81f)` starting at 0x005db758 â€”
    // exactly 50 dword slots).
    private readonly void*[] s_SoundBuffers = new void*[50];

    /// <summary>Invokes a no-argument <c>HRESULT</c> method at <paramref name="vtableSlot"/> on a COM object.</summary>
    internal static int InvokeComMethod(void* comObject, int vtableSlot)
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
    private void ReleaseAllSoundBuffers(bool stopPlayingBuffers)
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

    // =====================================================================
    // Creation du peripherique (Ghidra: LAB_00512ef0 @ 0x00512ef0).
    //
    // Ce corps manquait a Blaze.c : Ghidra ne l'a vu que pris en adresse, dans
    // `Ordinal_790(&LAB_00512ef0)`, et ne l'a donc jamais promu en fonction. C'est la lacune que
    // REVERSE_STATUS #26 decrivait comme « DAT_005db8e4 n'a aucun site de creation » -- il en a
    // un, en 0x00512f20, et c'est le seul appel a DirectSoundCreate de tout le binaire (balayage
    // de .text a la recherche des `call rel32` et des `call [IAT]` vers 0x00545c68).
    //
    // La meme fonction ecrit DAT_005db85c, ce qui repond du meme coup a #6 : la fenetre
    // principale n'est pas creee par Blaze.exe, elle lui est *rendue* par winsys3d.dll. Ordinal
    // 790 range le pointeur dans DAT_011be798, et winsys3d le rappelle a deux endroits de son
    // initialiseur de peripherique (winsys3d.c 5836 et 5874, soit 0x00a06485 et 0x00a065f8), les
    // deux en empilant DAT_011be79c -- sa propre HWND. Ghidra a rate l'argument au premier des
    // deux sites ; le desassemblage montre `push ecx` avant `call eax` aux deux.
    //
    // Le rappel est donc `void __stdcall (HWND)`, et il tombe pendant Ordinal_789 -- apres
    // que le peripherique 3D existe, avant que la boucle de jeu ne demande le moindre son.
    // Transcription ligne a ligne : Legacy/Raw/Blaze/FUN_00512EF0.asm.
    // =====================================================================

    private const int IDirectSound_SetCooperativeLevel = 0x18 / 4;
    private const int IDirectSoundBuffer_SetFormat = 0x38 / 4;

    private const uint DSBCAPS_PRIMARYBUFFER = 0x00000001;
    private const uint DSSCL_PRIORITY = 2;

    // Ghidra: DAT_005db85c -- la fenetre principale, ecrite ici et nulle part ailleurs. En
    // lecture seule au dehors : `Platform/Startup.cs` la detruit a l'extinction.
    internal IntPtr s_MainWindow_005db85c { get; private set; }

    // Ghidra: DAT_005db888 -- le WAVEFORMATEX du tampon primaire (22050 Hz, 16 bits, stereo).
    // En memoire native comme s_MusicOutputFormat, et pour la meme raison : l'original est une
    // globale .bss dont l'adresse part chez DirectSound.
    private readonly WAVEFORMATEX* s_PrimaryFormat_005db888 =
        (WAVEFORMATEX*)NativeMemory.AllocZeroed((nuint)sizeof(WAVEFORMATEX));

    // Ghidra: s_Blaze___Blade_005631d8 -- le titre pose sur la fenetre que winsys3d a creee.
    private readonly byte* s_WindowTitle_005631d8 = ToAnsiZ("Blaze & Blade");

    /// <summary>
    /// Le trampoline que <c>Ordinal_790</c> recoit. <c>[UnmanagedCallersOnly]</c> impose une
    /// methode statique : c'est une contrainte de plateforme, pas un choix de portee, et c'est
    /// la seule raison pour laquelle ce membre-ci echappe a la regle « tout est d'instance »
    /// enoncee en tete de fichier.
    ///
    /// <para>
    /// C'est aussi la couture entre la chaine d'affichage de 1998 et la notre, et le seul endroit
    /// ou elle a lieu. winsys3d passe <c>DAT_011be79c</c>, la fenetre qu'il a creee lui-meme --
    /// mais avec le device synthetique de <c>Host/SyntheticDevice.cs</c>, <c>3dhelp</c> n'est
    /// jamais initialise et cette fenetre n'existe pas : le rappel arrive avec une poignee nulle,
    /// et <c>SetCooperativeLevel</c> rend alors E_INVALIDARG (0x80070057), mesure le 2026-08-24.
    /// La reconstruction possede pourtant une vraie fenetre, celle du dos Direct3D 11 ; c'est
    /// exactement le raisonnement deja tenu dans <c>Platform/Startup.cs</c> pour donner une
    /// fenetre a DirectInput. On la substitue ici, et nulle part plus bas : <c>InitAudioDevice</c>
    /// reste une transcription de <c>LAB_00512ef0</c> qui n'utilise que son parametre.
    /// </para>
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    internal static void DeviceInitCallback(IntPtr mainWindow)
        => Aud.InitAudioDevice(mainWindow != IntPtr.Zero ? mainWindow : D3D11Window.Handle);

    /// <summary>
    /// Ghidra: LAB_00512ef0 @ 0x00512ef0. Cree le peripherique DirectSound, lui donne la fenetre
    /// que winsys3d vient de faire naitre, et impose au tampon primaire le format 22050 Hz /
    /// 16 bits / stereo dans lequel le flux de musique est decode.
    /// </summary>
    internal void InitAudioDevice(IntPtr mainWindow)
    {
        // Ghidra: 0x00512eff `mov [5DB85Ch],esi` puis SetWindowTextA(esi, s_Blaze___Blade_005631d8).
        s_MainWindow_005db85c = mainWindow;
        Win32.SetWindowTextA(mainWindow, s_WindowTitle_005631d8);

        // Ghidra: `rep stosd` de 0x32 mots a partir de 0x005db758 -- les 50 emplacements de
        // tampons d'effets sont remis a zero avant toute chose. Un ré-appel du rappel (winsys3d
        // le declenche aussi sur une reinitialisation de peripherique) repart donc d'une table
        // vide, sans liberer ce qu'elle contenait : fidele, y compris dans cette fuite.
        for (int i = 0; i < s_SoundBuffers.Length; i++) s_SoundBuffers[i] = null;

        // Ghidra: DirectSoundCreate(0, &DAT_005db8e4, 0) -- l'argument de sortie est la globale
        // elle-meme ; le HRESULT n'est pas teste, c'est la nullite du pointeur qui l'est.
        void* device = null;
        int hr = Win32.DirectSoundCreate(null, &device, null);
        s_DirectSound = device;
        // Meme journalisation que Input.CreateInputDevice : un HRESULT d initialisation COM est la
        // seule chose qui distingue « pas de peripherique » de « rappel jamais declenche ».
        Console.Error.WriteLine($"[AUDIO] DirectSoundCreate -> 0x{hr:X8}, fenetre 0x{mainWindow.ToInt64():X}");
        if (s_DirectSound == null) return;

        // Ghidra: le WAVEFORMATEX de 0x005db888, ecrit champ par champ entre 0x00512f36 et
        // 0x00512fa1. 0x5622 == 22050 Hz, 0x15888 == 88200 == 22050 * 2 canaux * 2 octets.
        s_PrimaryFormat_005db888->wFormatTag = WAVE_FORMAT_PCM;
        s_PrimaryFormat_005db888->nChannels = 2;
        s_PrimaryFormat_005db888->nSamplesPerSec = 0x5622;
        s_PrimaryFormat_005db888->nAvgBytesPerSec = 0x15888;
        s_PrimaryFormat_005db888->nBlockAlign = 4;
        s_PrimaryFormat_005db888->wBitsPerSample = 0x10;
        s_PrimaryFormat_005db888->cbSize = 0;

        void** deviceVtable = *(void***)s_DirectSound;
        var setCooperativeLevel =
            (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)deviceVtable[IDirectSound_SetCooperativeLevel];
        hr = setCooperativeLevel(s_DirectSound, mainWindow, DSSCL_PRIORITY);
        if (hr < 0) { Console.Error.WriteLine($"[AUDIO] SetCooperativeLevel -> 0x{hr:X8}"); return; }

        // Ghidra: le DSBUFFERDESC local, remplis a l'envers par le compilateur (dwSize n'est ecrit
        // qu'en 0x00512fae, juste avant l'appel). lpwfxFormat NUL : c'est le tampon primaire, son
        // format se pose apres coup par SetFormat et non a la creation.
        DSBUFFERDESC desc;
        desc.dwSize = 0x14;
        desc.dwFlags = DSBCAPS_PRIMARYBUFFER;
        desc.dwBufferBytes = 0;
        desc.dwReserved = 0;
        desc.lpwfxFormat = null;

        void* primary = null;
        var createSoundBuffer =
            (delegate* unmanaged[Stdcall]<void*, DSBUFFERDESC*, void**, void*, int>)deviceVtable[IDirectSound_CreateSoundBuffer];
        hr = createSoundBuffer(s_DirectSound, &desc, &primary, null);
        if (hr < 0) { Console.Error.WriteLine($"[AUDIO] CreateSoundBuffer(primaire) -> 0x{hr:X8}"); return; }

        // Ghidra: DAT_005db754 -- l'objet dont FUN_00512ff0 fait un Release juste avant celui du
        // peripherique. Sa nature etait notee « non epinglee » ; elle l'est maintenant, c'est le
        // tampon primaire.
        s_DirectSoundPrimary = primary;

        void** primaryVtable = *(void***)primary;
        var setFormat = (delegate* unmanaged[Stdcall]<void*, WAVEFORMATEX*, int>)primaryVtable[IDirectSoundBuffer_SetFormat];
        hr = setFormat(primary, s_PrimaryFormat_005db888);
        Console.Error.WriteLine($"[AUDIO] peripherique pret ; SetFormat(22050/16/stereo) -> 0x{hr:X8}");
    }

    /// <summary>
    /// Ghidra: FUN_00512ff0 @ 0x00512ff0 - post-loop teardown, stage 3: the DirectSound shutdown.
    /// Releases the primary/listener object, then every sound buffer (stopping any that are still
    /// playing), then the IDirectSound device itself.
    ///
    /// <para>
    /// Vivait dans <c>Platform/Startup.cs</c> jusqu'au 2026-08-21, aux cotes des trois autres
    /// etapes d'extinction. Rapatrie ici a la conversion : son adresse tombe dans la plage audio,
    /// et il est le SEUL ecrivain externe de <c>s_DirectSound</c>. Le laisser dehors aurait force
    /// ce champ a rester <c>internal</c> et modifiable de loin.
    /// </para>
    /// </summary>
    internal void ShutdownStage3()
    {
        if (s_DirectSound == null) return;

        if (s_DirectSoundPrimary != null)
        {
            InvokeComMethod(s_DirectSoundPrimary, IDirectSoundBuffer_Release);
            // Ghidra does not null DAT_005db754 here â€” preserved as-is.
        }

        ReleaseAllSoundBuffers(stopPlayingBuffers: true);

        InvokeComMethod(s_DirectSound, IDirectSound_Release);
        s_DirectSound = null;
    }


    // Ghidra: TexFilterPossible_exref â€” Blaze.exe imports this `uint` as DATA (not a function)
    // from winsys3d.dll. Confirmed via winsys3d.c: it defines and reads/writes a plain global
    // `TexFilterPossible` (see Documentation/DLL/winsys3d.md). P/Invoke has no direct mechanism
    // for an imported data symbol, so it's resolved lazily via GetProcAddress instead.
    private uint* s_TexFilterPossible;

    internal uint* GetTexFilterPossiblePtr()
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

    internal string GetIniFilePath()
    {
        byte* buffer = stackalloc byte[0x104];
        Win32.GetModuleFileNameA(IntPtr.Zero, buffer, 0x104);
        string exePath = Marshal.PtrToStringAnsi((IntPtr)buffer) ?? string.Empty;
        int dot = exePath.LastIndexOf('.');
        if (dot >= 0) exePath = exePath[..dot];
        return exePath + IniFileExtension;
    }

    internal uint ReadIniInt(string key, int defaultValue, byte* iniPathAnsi)
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
    private void InvokeSetVolume(void* comObject, int attenuationHundredthDb)
    {
        if (comObject == null) return;
        void** vtable = *(void***)comObject;
        var setVolume = (delegate* unmanaged[Stdcall]<void*, int, int>)vtable[IDirectSoundBuffer_SetVolume];
        setVolume(comObject, attenuationHundredthDb);
    }

    // Ghidra: DAT_005db8ec â€” the current music-stream attenuation, computed by FUN_005137b0 and
    // read back by FUN_005131d0 when it starts a new stream.
    private int s_MusicAttenuation;

    // Ghidra: DAT_005db8e8 â€” the current sound-effect attenuation, computed by FUN_00513800 and
    // read back by FUN_00513180 every time an effect is played.
    private int s_SoundEffectAttenuation;

    /// <summary>
    /// Ghidra: FUN_005137b0 @ 0x005137b0. Converts the "CdVol" ini setting (0..128-ish) into a
    /// DirectSound-style attenuation, stores it as the current music attenuation and applies it
    /// to the streaming-music buffer, if one exists yet.
    /// </summary>
    internal void ApplyCdVolume()
    {
        if (mem.s_CdVolumeIni == 0) return;
        s_MusicAttenuation = (int)(mem.s_CdVolumeIni * 2500) / 128 - 2500;
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
    internal void ApplyAllSoundBufferVolumes()
    {
        s_SoundEffectAttenuation = (int)(mem.s_SoundVolumeIni * 2000) / 128 - 2000;
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
    private int AllocateSoundBuffer(uint bufferBytes)
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
    private bool LockSoundBuffer(int slot, void** audioPtr, uint* audioBytes)
    {
        void* buffer = s_SoundBuffers[slot];
        if (buffer == null) return false;

        void** vtable = *(void***)buffer;
        var lockBuffer = (delegate* unmanaged[Stdcall]<void*, uint, uint, void**, uint*, void**, uint*, uint, int>)vtable[IDirectSoundBuffer_Lock];
        return lockBuffer(buffer, 0, 0, audioPtr, audioBytes, null, null, DSBLOCK_ENTIREBUFFER) >= 0;
    }

    /// <summary>Ghidra: FUN_00513150 @ 0x00513150 â€” <c>IDirectSoundBuffer::Unlock</c>.</summary>
    private void UnlockSoundBuffer(int slot, void* audioPtr, uint audioBytes)
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
    private void PlaySoundBuffer(int slot, bool looping)
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
    // by the options UI at 0x0050f2xx). Recovered from Legacy/Binaries/Blaze.exe @ 0x005631d4 as **1**:
    // music ships **enabled**. This previously defaulted to 0 on the assumption that the value was
    // unrecoverable, which silently made StartMusicStream take its early-out forever.
    internal uint s_MusicEnabled = 1;

    // Ghidra: DAT_005631d0 â€” nonzero while no stream is running. Note the inverted sense: it is
    // *cleared* at the end of a successful start and *set* at the top of a stop. Left at 0 (its
    // .bss default): a first StartMusicStream always takes the "different track" path, which
    // calls StopMusicStream and so sets the flag before it is tested.
    private uint s_MusicStreamStopped;

    // Ghidra: DAT_005db8f0 â€” the whole-file scratch buffer. Its size is the literal 0x004c4b40
    // (5,000,000), which Ghidra rendered as `&LAB_004c4b40` because the constant happens to fall
    // inside the .text range; it is used identically as a plain size in the tail-zeroing loop.
    private const uint MusicFileBufferSize = 0x004c4b40;
    private byte* s_MusicFileBuffer;

    // Ghidra: DAT_005db8f4 / DAT_005db8f8 / DAT_005db8fc â€” the `fmt ` chunk body, the `data`
    // chunk body and the `data` chunk size, all pointing into s_MusicFileBuffer.
    private ADPCMWAVEFORMAT* s_MusicFormat;
    private byte* s_MusicData;
    private int s_MusicDataSize;

    // Ghidra: DAT_005db820 â€” the PCM WAVEFORMATEX describing the *decoded* stream. Kept in native
    // memory because the original is a .bss global whose address is handed to DirectSound.
    private readonly WAVEFORMATEX* s_MusicOutputFormat = (WAVEFORMATEX*)NativeMemory.AllocZeroed((nuint)sizeof(WAVEFORMATEX));


    // Ghidra: DAT_005db8a0 (DSBPOSITIONNOTIFY[8]) et DAT_005db838. Ce second bloc n'est PAS un
    // simple double des huit poignees : c'est un tableau de NEUF poignees contigues, les huit
    // evenements de notification suivis de l'evenement d'arret (DAT_005db858). Deux mesures
    // independantes le disent, et elles se recoupent au dernier octet :
    //   - le thread de flux attend `WaitForMultipleObjects(9, &DAT_005db838, FALSE, INFINITE)`
    //     et traite l'indice 8 comme « il faut sortir » ;
    //   - FUN_005135d0 ferme les poignees tant que le curseur est `< 0x5db85c`, soit 9 tours.
    // 0x005db838 + 9 * 4 == 0x005db85c, l'adresse exacte de la fenetre principale qui suit.
    //
    // Il est donc en memoire native, et d'un seul tenant : c'est ce que WaitForMultipleObjects
    // exige. La note precedente, « seuls les 8 evenements de notification sont fermes, celui
    // d'arret fuit », lisait la borne a l'envers -- corrigee le 2026-08-24.
    private const int MusicNotifyCount = 8;
    private const int MusicWaitHandleCount = MusicNotifyCount + 1;
    private const int MusicStopHandleIndex = MusicNotifyCount;
    private readonly DSBPOSITIONNOTIFY* s_MusicNotifyPositions = (DSBPOSITIONNOTIFY*)NativeMemory.AllocZeroed(MusicNotifyCount * (nuint)sizeof(DSBPOSITIONNOTIFY));
    private readonly IntPtr* s_MusicWaitHandles_005db838 = (IntPtr*)NativeMemory.AllocZeroed(MusicWaitHandleCount * (nuint)sizeof(IntPtr));

    // Ghidra: DAT_005db880 / DAT_005db8e0 â€” the streaming worker thread and its thread id.
    private IntPtr s_MusicStreamThread;
    private uint s_MusicStreamThreadId;

    // Ghidra: DAT_005db860 / DAT_005db750 / DAT_005db89c. Le curseur du flux (indice du bloc
    // source atteint), son terme (nombre total de blocs source) et le drapeau « la lecture vient
    // de demarrer ». Leurs lecteurs vivaient dans le corps manquant du thread ; celui-ci est
    // transcrit depuis le binaire depuis le 2026-08-24 (RunMusicStreamWorker), et les trois roles
    // ci-dessus sont maintenant lus dans du code qui les consomme, non plus deduits.
    private int s_MusicSourceBlockCursor;
    private int s_MusicTotalBlocks;
    private int s_MusicPlaying;

    // Ghidra: DAT_005db908 (the file currently loaded into s_MusicFileBuffer) and DAT_005dba08
    // (the most recently requested file, replayed when FUN_005131d0 is called with a null path).
    // 0x100 is the exact gap between DAT_005db908 and the next referenced global, DAT_005dba08.
    private const nuint MusicPathBufferSize = 0x100;
    private readonly byte* s_LoadedMusicPath = (byte*)NativeMemory.AllocZeroed(MusicPathBufferSize);
    private readonly byte* s_RequestedMusicPath = (byte*)NativeMemory.AllocZeroed(MusicPathBufferSize);

    // Ghidra: DAT_0054ded8, the riid handed to QueryInterface in FUN_005131d0. The GUID *bytes*
    // are .rdata and therefore absent from Blaze.c (same systemic blocker as the other data-only
    // symbols), but the interface identity is pinned by how the returned object is used â€” see the
    // DirectSound banner above. The value below is the documented IID_IDirectSoundNotify.
    private readonly Guid IID_IDirectSoundNotify = new Guid("b0210783-89cd-11d0-af08-00a0c925cd16");

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
    private readonly short[] MsAdpcmAdaptationTable =
    {
        230, 230, 230, 230, 307, 409, 512, 614,
        768, 614, 512, 409, 307, 230, 230, 230,
    };
    private readonly short[] MsAdpcmCoefficient1 = { 256, 512, 0, 192, 240, 460, 392 };
    private readonly short[] MsAdpcmCoefficient2 = { 0, -256, 0, 64, 0, -208, -232 };

    /// <summary>
    /// Ghidra: FUN_00531f70 @ 0x00531f70. Accepts only a `fmt ` chunk that is mono/stereo
    /// 4-bit Microsoft ADPCM using the standard 7-entry coefficient set.
    /// </summary>
    private bool IsStandardMsAdpcmFormat(ADPCMWAVEFORMAT* format)
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
    private byte* WriteDecodedSample(byte* dest, short sample, int destBitsPerSample)
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
    private int DecodeMsAdpcm(ADPCMWAVEFORMAT* srcFormat, byte* src, WAVEFORMATEX* destFormat, byte* dest, uint srcByteBudget)
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
    private bool LoadMusicWaveFile(byte* path)
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
    internal int StartMusicStream(byte* path)
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
            if (!LoadMusicWaveFile(path)) { Console.Error.WriteLine("[AUDIO] LoadMusicWaveFile a echoue"); return 0; }
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
            s_MusicWaitHandles_005db838[i] = signal;
            s_MusicNotifyPositions[i].dwOffset = (uint)(samplesPerBlock * i * 0x28);
        }
        // Ghidra: DAT_005db858 -- la neuvieme poignee du meme tableau, l'evenement « sortir ».
        s_MusicWaitHandles_005db838[MusicStopHandleIndex] = Win32.CreateEventA(null, 0, 0, null);

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

        // ECHAFAUDAGE DE MESURE (2026-08-24). N'existe pas dans l'original : GetStatus est le
        // seul moyen de prouver, sans oreille, que le tampon joue vraiment. 0x5 vaut
        // DSBSTATUS_PLAYING | DSBSTATUS_LOOPING. A retirer quand le son sera routinier.
        {
            uint status = 0;
            var getStatus = (delegate* unmanaged[Stdcall]<void*, uint*, int>)bufferVtable[IDirectSoundBuffer_GetStatus];
            getStatus(streamBuffer, &status);
            Console.Error.WriteLine(
                $"[AUDIO] {Marshal.PtrToStringAnsi((IntPtr)s_LoadedMusicPath)} : {desc.dwBufferBytes} octets, " +
                $"{s_MusicTotalBlocks} blocs, statut 0x{status:X}, attenuation {s_MusicAttenuation}");
        }

        // Ghidra: DAT_005db880 = CreateThread(0, 0, &LAB_00513660, 0, 0, &DAT_005db8e0). Le corps
        // du thread manquait a Blaze.c -- pris en adresse, jamais decompile -- et l'appel etait
        // saute pour cette raison : la musique demarrait alors mais rejouait ses 80 blocs amorces
        // en boucle, rien ne rechargeant le tampon. Le corps est transcrit ci-dessous depuis le
        // binaire (RunMusicStreamWorker, 2026-08-24) ; l'appel peut donc avoir lieu.
        fixed (uint* threadId = &s_MusicStreamThreadId)
        {
            s_MusicStreamThread = Win32.CreateThread(
                null, 0, (IntPtr)(delegate* unmanaged[Stdcall]<void*, uint>)&MusicStreamThreadProc,
                null, 0, threadId);
        }

        return 0;
    }

    /// <summary>
    /// Le point d'entree que <c>CreateThread</c> recoit. Meme contrainte que
    /// <see cref="DeviceInitCallback"/> : <c>[UnmanagedCallersOnly]</c> exige une methode
    /// statique.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static uint MusicStreamThreadProc(void* parameter) => Aud.RunMusicStreamWorker();

    /// <summary>
    /// Ghidra: LAB_00513660 @ 0x00513660. Le thread de flux : il attend qu'un des huit points de
    /// notification du tampon soit franchi, puis redecode dix blocs source dans le segment que la
    /// tete de lecture vient de quitter. Il sort sur la neuvieme poignee.
    ///
    /// <para>
    /// Transcription de Legacy/Raw/Blaze/LAB_00513660.asm. Un seul ecart, note sur place :
    /// <c>ExitThread</c> devient un <c>return</c>.
    /// </para>
    /// </summary>
    private uint RunMusicStreamWorker()
    {
        while (true)
        {
            // Ghidra: WaitForMultipleObjects(9, &DAT_005db838, FALSE, INFINITE).
            uint signalled = Win32.WaitForMultipleObjects(
                MusicWaitHandleCount, s_MusicWaitHandles_005db838, 0, 0xffffffff);
            int index = (int)signalled;

            // Ghidra: `if (esi == 8) ExitThread(0);` -- rendu par un retour. Les deux terminent le
            // thread avec le code 0 ; le retour laisse en plus le CLR derouler proprement le cadre
            // de rappel natif, ce qu'un ExitThread depuis une trame geree ne ferait pas.
            if (index == MusicStopHandleIndex) return 0;

            // Ghidra: DAT_005db89c -- « la lecture vient de demarrer ». La toute premiere
            // notification apres un demarrage porte l'indice 0, et ce segment-la vient justement
            // d'etre amorce : le redecoder l'ecraserait. Le drapeau est efface dans tous les cas.
            if (s_MusicPlaying != 0)
            {
                bool justStartedAtSegmentZero = index == 0;
                s_MusicPlaying = 0;
                if (justStartedAtSegmentZero) continue;
            }

            // Ghidra: les deux gardes `jl` / `jge` sur l'indice -- WAIT_ABANDONED_0 et au-dela
            // sont ignores.
            if (index < 0 || index >= MusicNotifyCount) continue;

            // Le segment a recharger est celui qui PRECEDE la notification franchie.
            int segment = index == 0 ? MusicNotifyCount - 1 : index - 1;

            void* streamBuffer = s_MusicStreamBuffer;
            void** bufferVtable = *(void***)streamBuffer;

            void* audioPtr = null;
            uint audioBytes = 0;
            var lockBuffer = (delegate* unmanaged[Stdcall]<void*, uint, uint, void**, uint*, void**, uint*, uint, int>)bufferVtable[IDirectSoundBuffer_Lock];
            // La taille verrouillee est lue dans s_MusicNotifyPositions[1].dwOffset (Ghidra:
            // DAT_005db8a8) : les decalages etant espaces regulierement, celui de l'entree 1 EST
            // la longueur d'un segment.
            lockBuffer(streamBuffer, s_MusicNotifyPositions[segment].dwOffset,
                       s_MusicNotifyPositions[1].dwOffset, &audioPtr, &audioBytes, null, null, 0);

            int blockAlign = s_MusicFormat->wfx.nBlockAlign;
            int cursor = s_MusicSourceBlockCursor;

            if (cursor + 0x0a < s_MusicTotalBlocks)
            {
                DecodeMsAdpcm(s_MusicFormat, s_MusicData + cursor * blockAlign, s_MusicOutputFormat,
                              (byte*)audioPtr, (uint)(blockAlign * 0x0a));
                s_MusicSourceBlockCursor += 0x0a;
            }
            else
            {
                // Bouclage : ce qui reste jusqu'a la fin du morceau, puis le complement repris au
                // debut des donnees.
                int remaining = s_MusicTotalBlocks - cursor;
                DecodeMsAdpcm(s_MusicFormat, s_MusicData + cursor * blockAlign, s_MusicOutputFormat,
                              (byte*)audioPtr, (uint)(remaining * blockAlign));

                s_MusicSourceBlockCursor = 0x0a - remaining;
                // LEGACY : la seconde passe repart de `audioPtr` et non de la fin de la premiere.
                // Le binaire relit le meme local en 0x0051376d sans lui ajouter la longueur
                // decodee ; les blocs de tete ecrasent donc ceux de queue a chaque bouclage.
                // Transcrit tel quel -- voir Documentation/Reference/legacy-bug.md.
                DecodeMsAdpcm(s_MusicFormat, s_MusicData, s_MusicOutputFormat, (byte*)audioPtr,
                              (uint)(s_MusicSourceBlockCursor * blockAlign));
            }

            var unlockBuffer = (delegate* unmanaged[Stdcall]<void*, void*, uint, void*, uint, int>)bufferVtable[IDirectSoundBuffer_Unlock];
            unlockBuffer(streamBuffer, audioPtr, audioBytes, null, 0);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005135d0 @ 0x005135d0. Signals the worker thread to exit, waits for it, then
    /// tears down the streaming buffer, its notify interface and the 8 notification events.
    /// </summary>
    private int StopMusicStream()
    {
        if (s_MusicStreamStopped == 0)
        {
            s_MusicStreamStopped = 1;
            Win32.SetEvent(s_MusicWaitHandles_005db838[MusicStopHandleIndex]);

            // With the worker thread not created (see StartMusicStream), the handle is zero and
            // this returns WAIT_FAILED immediately instead of blocking â€” kept as decompiled.
            Win32.WaitForSingleObject(s_MusicStreamThread, 0xffffffff);
            s_MusicStreamThread = IntPtr.Zero;

            if (s_MusicStreamBuffer != null) InvokeComMethod(s_MusicStreamBuffer, IDirectSoundBuffer_Release);
            if (s_MusicStreamNotify != null) InvokeComMethod(s_MusicStreamNotify, IDirectSoundNotify_Release);
            s_MusicStreamBuffer = null;
            s_MusicStreamNotify = null;

            // Ghidra: `puVar1 = &DAT_005db838; do { CloseHandle(*puVar1); puVar1++; } while
            // ((int)puVar1 < 0x5db85c);` -- la borne est 0x5db85c, donc NEUF tours : les huit
            // evenements de notification ET l'evenement d'arret. La note precedente lisait
            // « s'arrete juste avant DAT_005db858 » et n'en fermait que huit, en concluant a une
            // fuite qui n'existe pas. Corrige le 2026-08-24.
            for (int i = 0; i < MusicWaitHandleCount; i++) Win32.CloseHandle(s_MusicWaitHandles_005db838[i]);
        }
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_00513860 @ 0x00513860. Flips the music-enabled flag and either stops the
    /// current stream or restarts the last requested track.
    /// </summary>
    internal void ToggleMusicEnabled()
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


    private const int SoundGainUnity = 0x1000;

    /// <summary>
    /// Applies a 1.12 fixed-point gain to one unsigned 8-bit PCM sample. Ghidra emits this inline
    /// a dozen times across the five resamplers, always with the same asymmetric clamp to
    /// [-0x7f, +0x7f] (not [-0x80, +0x7f]) before re-biasing by 0x80.
    /// </summary>
    private byte ApplySoundGain(byte sample, int gain)
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
    private void FinishAndPlayResampledBuffer(int slot, byte* dest, byte* cursor, int bufferBytes)
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
    private void PlaySampleDecimated(int stepMinusOne, byte* src, int srcLength, int gainLeft, int gainRight)
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
    private void PlaySampleDroppingOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
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
    private void PlaySampleInsertingOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
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
    private void PlaySampleDoubledExceptOneIn(int n, byte* src, int srcLength, int gainLeft, int gainRight)
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
    private void PlaySampleDoubled(byte* src, int srcLength, int gainLeft, int gainRight)
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
    internal void PlaySoundEffect(int soundId, int pitch, int gainLeft, int gainRight)
    {
        gainLeft >>= 1;
        gainRight >>= 1;

        void* bank;
        if (soundId < GameMemory.SoundDirectoryBaseCount) bank = mem.s_BaseSoundBank_00604920;
        else if (soundId <= 0x4c) bank = mem.s_LevelSoundBank_00600900;
        else return; // ids past the end of the directory are silently ignored

        // Both halves of the directory are indexed off the same base; only the bank differs.
        // No null check on the bank here â€” the original has none either, and none is needed: the
        // pointer is only ever dereferenced after a sound buffer has been allocated successfully,
        // which cannot happen before RunApplication has allocated both banks.
        GameMemory.SoundDirectoryEntry entry = mem.s_SoundDirectory_005e8340[soundId];
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

    // =====================================================================
    // Son positionnel (Ghidra: FUN_0053da20 / FUN_0053db20 / FUN_0053dc30).
    //
    // C'est ce qui fait entrer `Model3d` dans le constructeur de cette classe : un son place dans
    // le monde se projette avec la camera courante avant d'etre panoramique. L'arete Audio ->
    // Model3d ne ferme pas de cycle, Model3d ne dependant que de GameMemory.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0053da20 @ 0x0053da20 â€” convertit une position monde en une paire de gains
    /// gauche/droite, et rend 0 quand le son ne doit pas etre joue du tout.
    ///
    /// <para><b>Ecrite contre le desassemblage.</b> La pseudo-C rend cette fonction avec deux
    /// parametres, un <c>unaff_SI</c> et un <c>unaff_retaddr</c> ; elle en prend trois, tous lus
    /// sur la pile : arg1 en <c>[ESP+4]</c> la position, arg2 en <c>[ESP+8]</c> le gain de base,
    /// arg3 en <c>[ESP+0xC]</c> la paire de sortie. <c>unaff_SI</c> est en realite l'attenuation
    /// calculee juste avant, et <c>unaff_retaddr</c> le gain de base.</para>
    ///
    /// <para>Trois filtres, dans cet ordre : hors d'un demi-champ de <c>0x400</c> autour du centre
    /// de l'ecran (<c>0xa0</c>, la moitie de 320) le son est mue ; au-dela de <c>0x1000/6</c> en
    /// profondeur l'attenuation passe negative et le son est mue aussi ; enfin les deux gains sont
    /// bornes a zero. Le panoramique est une paire de cosinus pris a l'ecart horizontal, mis a
    /// l'echelle <b>de facon miroir</b> selon le cote : <c>x*8 / x*4</c> a droite du centre,
    /// <c>x*4 / x*8</c> a gauche.</para>
    /// </summary>
    private int ComputePositionalSoundGains(int* position, int gain, short* outGains)
    {
        // Le tampon de projection fait 8 octets : deux short d'ecran, puis la profondeur en dword.
        short* screen = stackalloc short[4];
        mdl.ProjectPoint(position, screen);

        short offsetX = (short)(screen[0] - 0xa0);
        screen[0] = offsetX;
        int magnitude = offsetX < 0 ? -offsetX : offsetX;
        if (magnitude >= 0x400) return 0;

        // `1000h - profondeur*6`, tronque a 16 bits par le `test cx,cx` qui suit dans le binaire.
        short attenuation = (short)(0x1000 - *(int*)(screen + 2) * 6);
        if (attenuation < 0) return 0;

        outGains[0] = (short)Winsys3d.GetCosTableEntry(offsetX >= 0 ? offsetX * 8 : offsetX * 4);
        outGains[1] = (short)Winsys3d.GetCosTableEntry(offsetX >= 0 ? offsetX * 4 : offsetX * 8);
        if (outGains[0] < 0) outGains[0] = 0;
        if (outGains[1] < 0) outGains[1] = 0;

        // Deux mises a l'echelle 1.12 enchainees, avec troncature a 16 bits entre les deux --
        // c'est ce que font les `movsx eax,di` / `movsx ecx,cx` intercales.
        short right = (short)((short)((outGains[1] * attenuation) >> 0xc) * gain >> 0xc);
        short left = (short)((short)((outGains[0] * attenuation) >> 0xc) * gain >> 0xc);
        outGains[1] = right;
        outGains[0] = left;
        return (right | left) != 0 ? 1 : 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053db20 @ 0x0053db20 â€” joue un son a une position du monde. Plusieurs centaines
    /// de sites d'appel dans <c>Blaze.c</c>.
    ///
    /// <para>La paire de gains est ecrite <b>par-dessus l'emplacement de pile de
    /// <paramref name="gain"/></b> : le binaire passe <c>&amp;param_3</c> comme tampon de sortie.
    /// L'argument que lit <see cref="ComputePositionalSoundGains"/> est la copie deja empilee, donc
    /// l'ecrasement n'a pas d'effet de bord ; transcrit tel quel avec une variable locale, pour que
    /// la relecture cote a cote avec <c>Blaze.c</c> reste immediate.</para>
    /// </summary>
    internal void PlayPositionalSound(int soundId, int pitch, int gain, int* position)
    {
        int gains = gain;
        if (ComputePositionalSoundGains(position, gain, (short*)&gains) != 0)
        {
            PlaySoundEffect(soundId, pitch, (short)gains, (short)(gains >> 0x10));
        }
    }

    /// <summary>
    /// Ghidra: &amp;DAT_005dfb90 â€” la file des sons differes : <b>0x20 entrees de 0x14 octets</b>.
    /// Chaque entree porte une position monde (trois <c>int</c> a <c>+0x00</c>), un pointeur de
    /// script a <c>+0x0c</c> et un compte a rebours d'un octet a <c>+0x10</c>.
    ///
    /// <para>La pseudo-C part de <c>&amp;DAT_005dfba0</c> et adresse le reste en negatif
    /// (<c>pcVar4 - 4</c>, <c>pcVar4 - 0x10</c>) : l'entree commence bien 0x10 octets plus bas, ce
    /// que le desassemblage confirme (<c>mov edi,5DFBA0h</c>, puis <c>lea ecx,[edi-10h]</c>).</para>
    ///
    /// <para>TODO(REVERSE) : rien de ce qui <b>remplit</b> cette file n'est encore transcrit. Elle
    /// reste donc nulle et <see cref="TickDelayedSounds"/> ne fait rien â€” ce qui est fidele, pas un
    /// contournement.</para>
    /// </summary>
    internal readonly byte* s_DelayedSounds_005dfb90 = (byte*)NativeMemory.AllocZeroed(0x20 * 0x14);

    /// <summary>
    /// Ghidra: FUN_0053dc30 @ 0x0053dc30 â€” le tour par image de la file des sons differes. Appele
    /// une fois par image par la boucle de la carte du monde.
    ///
    /// <para>Un script est une suite d'enregistrements de 8 octets â€” identifiant, hauteur a
    /// <c>+4</c>, gain a <c>+6</c>. Les octets 1 et 2 du <b>premier</b> enregistrement servent en
    /// meme temps d'en-tete : combien de sons partent ensemble (<c>+2</c>) et dans combien d'images
    /// part le lot suivant (<c>+1</c>). Un delai suivant nul termine le script et libere
    /// l'emplacement. Ce recouvrement entre en-tete et premier enregistrement est bien ce que fait
    /// l'original ; il n'est pas dedouble ici.</para>
    /// </summary>
    internal void TickDelayedSounds()
    {
        byte* entry = s_DelayedSounds_005dfb90;
        for (int remaining = 0x20; remaining != 0; remaining--, entry += 0x14)
        {
            byte* script = *(byte**)(entry + 0x0c);
            if (script == null) continue;

            entry[0x10]--;
            if (entry[0x10] != 0) continue;

            byte nextDelay = script[1];
            for (uint count = script[2]; count != 0; count--)
            {
                PlayPositionalSound(
                    script[0], *(ushort*)(script + 4), *(ushort*)(script + 6), (int*)entry);
                script += 8;
            }

            if (nextDelay == 0) script = null;
            else entry[0x10] = nextDelay;
            *(byte**)(entry + 0x0c) = script;
        }
    }

    /// <summary>Ghidra: FUN_0053db80 @ 0x0053db80 â€” fixed UI sound, id 0x38.</summary>
    internal void PlayUiSound38() => PlaySoundEffect(0x38, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dba0 @ 0x0053dba0 â€” fixed UI sound, id 0x39.</summary>
    internal void PlayUiSound39() => PlaySoundEffect(0x39, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053db60 @ 0x0053db60 â€” fixed UI sound, id 0x3a.</summary>
    internal void PlayUiSound3A() => PlaySoundEffect(0x3a, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dbc0 @ 0x0053dbc0 â€” fixed UI sound, id 0x3b.</summary>
    internal void PlayUiSound3B() => PlaySoundEffect(0x3b, 0x400, 0x2000, 0x2000);

    /// <summary>Ghidra: FUN_0053dbc0 â€” le bip d'action refusÃ©e.</summary>
    internal void PlayUiSoundError() => PlaySoundEffect(0x3b, 0x400, 0x2000, 0x2000);
}
