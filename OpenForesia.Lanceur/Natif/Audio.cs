// Le son, par WASAPI, sans dependance (taches 3.1 et 3.2, design D4).
//
// Cote hote, la capture « process loopback » (Windows 10 2004+) : le son d'un arbre de processus
// seul — celui du jeu, lanceur compris (LB-056 : le jeu est son enfant) —, pas celui du reste du PC.
// Elle s'active par ActivateAudioInterfaceAsync, qui rend la main tout de suite et rappelle un objet
// COM a nous (IActivateAudioInterfaceCompletionHandler) quand le client audio est pret ; cet objet
// est une vtable de quatre pointeurs, ecrite ici a la main.
//
// Cote invite, une sortie partagee sur le peripherique par defaut. Son tampon est la reserve contre
// l'irregularite du reseau ; au-dela d'une borne, un paquet est jete plutot que d'accumuler du retard.
//
// Format des deux cotes : PCM 16 bits, 48 kHz, stereo — le moteur audio convertit (AUTOCONVERTPCM).
//
// Rangs de vtable (audioclient.h, mmdeviceapi.h) :
//   IAudioClient         Initialize 3, GetBufferSize 4, GetCurrentPadding 6, Start 10, Stop 11, GetService 14
//   IAudioCaptureClient  GetBuffer 3, ReleaseBuffer 4, GetNextPacketSize 5
//   IAudioRenderClient   GetBuffer 3, ReleaseBuffer 4
//   IMMDeviceEnumerator  GetDefaultAudioEndpoint 4 ; IMMDevice Activate 3
//   IActivateAudioInterfaceAsyncOperation  GetActivateResult 3

using System.Runtime.InteropServices;
using static OpenForesia.Lanceur.Natif.Com;

namespace OpenForesia.Lanceur.Natif;

internal static unsafe partial class Audio
{
    internal const int Frequence = 48000, Canaux = 2, OctetsParTrame = 4;

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct WaveFormatEx
    {
        public ushort Format, Canaux;
        public uint Frequence, OctetsParSeconde;
        public ushort Alignement, Bits, Extension;
    }

    internal static WaveFormatEx Pcm16 => new()
    {
        Format = 1, Canaux = Canaux, Frequence = Frequence, OctetsParSeconde = Frequence * OctetsParTrame,
        Alignement = OctetsParTrame, Bits = 16,
    };

    internal static readonly Guid IID_IAudioClient = new("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2");
    internal static readonly Guid IID_IAudioCaptureClient = new("C8ADBD64-E71E-48a0-A4DE-185C395CD317");
    internal static readonly Guid IID_IAudioRenderClient = new("F294ACFC-3146-4483-A7BF-ADDCA7C260E2");

    internal const uint Partage = 0;                              // AUDCLNT_SHAREMODE_SHARED
    internal const uint Loopback = 0x00020000, AutoConversion = 0x80000000, ConversionQualite = 0x08000000;
    internal const uint Silence = 2;                              // AUDCLNT_BUFFERFLAGS_SILENT

    internal static void Initialiser(void* client, uint drapeaux, long duree)
    {
        WaveFormatEx format = Pcm16;
        Verifier(((delegate* unmanaged[Stdcall]<void*, uint, uint, long, long, WaveFormatEx*, Guid*, int>)Vtable(client)[3])(
                     client, Partage, drapeaux, duree, 0, &format, null), "IAudioClient.Initialize");
    }

    internal static void* Service(void* client, Guid iid)
    {
        void* s;
        Verifier(((delegate* unmanaged[Stdcall]<void*, Guid*, void**, int>)Vtable(client)[14])(client, &iid, &s), "IAudioClient.GetService");
        return s;
    }

    internal static void Demarrer(void* client) =>
        Verifier(((delegate* unmanaged[Stdcall]<void*, int>)Vtable(client)[10])(client), "IAudioClient.Start");

    internal static void Arreter(void* client) => ((delegate* unmanaged[Stdcall]<void*, int>)Vtable(client)[11])(client);
}

/// <summary>La capture du son d'un arbre de processus (hôte, tâche 3.1).</summary>
internal sealed unsafe partial class CaptureSonProcessus : IDisposable
{
    [LibraryImport("mmdevapi.dll", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int ActivateAudioInterfaceAsync(string chemin, Guid* iid, Propvariant* parametres, void* rappel, void** operation);

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    private struct Propvariant
    {
        [FieldOffset(0)] public ushort Vt;
        [FieldOffset(8)] public uint TailleBlob;
        [FieldOffset(12)] public void* DonneesBlob;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct ParametresActivation { public int Genre; public uint Processus; public int Mode; }

    private const string CheminLoopback = "VAD\\Process_Loopback";
    private const ushort VtBlob = 65;
    private const int ActivationLoopbackProcessus = 1, InclureArbre = 0;

    private static readonly Guid IID_IUnknown = new("00000000-0000-0000-C000-000000000046");
    private static readonly Guid IID_Rappel = new("41D949AB-9862-444A-80F6-C261334DA5EB");
    private static readonly Guid IID_IAgileObject = new("94ea2b94-e9cc-49e0-c0ff-ee64ca8f5b90");

    private void* _client, _capture;

    /// <summary>
    /// Active la capture du son de <paramref name="processus"/> et de ses descendants. Bloque jusqu'à
    /// ce que Windows ait rappelé (au plus 5 s). Le fil doit être en appartement multithread.
    /// </summary>
    internal CaptureSonProcessus(uint processus)
    {
        var parametres = new ParametresActivation { Genre = ActivationLoopbackProcessus, Processus = processus, Mode = InclureArbre };
        var pv = new Propvariant { Vt = VtBlob, TailleBlob = (uint)sizeof(ParametresActivation), DonneesBlob = &parametres };
        Guid iid = Audio.IID_IAudioClient;

        using var fait = new ManualResetEventSlim(false);
        void* rappel = Rappel.Creer(fait);
        void* operation = null;
        try
        {
            Verifier(ActivateAudioInterfaceAsync(CheminLoopback, &iid, &pv, rappel, &operation), "ActivateAudioInterfaceAsync");
            if (!fait.Wait(5000)) throw new TimeoutException("l'activation de la capture du son n'a pas répondu");

            int resultat;
            void* inconnu;
            Verifier(((delegate* unmanaged[Stdcall]<void*, int*, void**, int>)Vtable(operation)[3])(operation, &resultat, &inconnu), "GetActivateResult");
            Verifier(resultat, "activation de la capture du son du jeu");
            _client = Interroger(inconnu, Audio.IID_IAudioClient);
            Liberer(inconnu);
            if (_client == null) throw new COMException("IAudioClient introuvable");
        }
        finally
        {
            Liberer(operation);
            Liberer(rappel);
        }

        Audio.Initialiser(_client, Audio.Loopback | Audio.AutoConversion | Audio.ConversionQualite, 200_000);   // 20 ms
        _capture = Audio.Service(_client, Audio.IID_IAudioCaptureClient);
        Audio.Demarrer(_client);
    }

    /// <summary>Tout ce qui est arrivé depuis la dernière fois, ajouté à <paramref name="sortie"/> (PCM 16 bits stéréo).</summary>
    internal void Lire(List<byte> sortie)
    {
        void** vt = Vtable(_capture);
        while (true)
        {
            uint paquet;
            if (((delegate* unmanaged[Stdcall]<void*, uint*, int>)vt[5])(_capture, &paquet) < 0 || paquet == 0) return;

            byte* donnees;
            uint trames, drapeaux;
            if (((delegate* unmanaged[Stdcall]<void*, byte**, uint*, uint*, ulong*, ulong*, int>)vt[3])(
                    _capture, &donnees, &trames, &drapeaux, null, null) < 0) return;
            int octets = (int)trames * Audio.OctetsParTrame;
            if ((drapeaux & Audio.Silence) != 0) sortie.AddRange(new byte[octets]);
            else sortie.AddRange(new ReadOnlySpan<byte>(donnees, octets));
            ((delegate* unmanaged[Stdcall]<void*, uint, int>)vt[4])(_capture, trames);
        }
    }

    public void Dispose()
    {
        if (_client != null) Audio.Arreter(_client);
        Liberer(_capture);
        Liberer(_client);
        _capture = _client = null;
    }

    /// <summary>
    /// L'objet COM que Windows rappelle quand le client audio est prêt : IUnknown, puis
    /// ActivateCompleted (3). Il se dit agile (IAgileObject), sans quoi Windows refuse de le rappeler
    /// depuis son propre fil.
    /// </summary>
    private static class Rappel
    {
        private static readonly Dictionary<nint, ManualResetEventSlim> s_Evenements = new();
        private static void** s_Vtable;

        /// <summary>Le compte de références, dans le mot qui suit la vtable.</summary>
        private static ref int Compte(void* objet) => ref *(int*)((nint*)objet + 1);

        internal static void* Creer(ManualResetEventSlim evenement)
        {
            if (s_Vtable == null)
            {
                s_Vtable = (void**)NativeMemory.Alloc(4, (nuint)sizeof(void*));
                s_Vtable[0] = (delegate* unmanaged[Stdcall]<void*, Guid*, void**, int>)&Interroger;
                s_Vtable[1] = (delegate* unmanaged[Stdcall]<void*, uint>)&AjouterRef;
                s_Vtable[2] = (delegate* unmanaged[Stdcall]<void*, uint>)&Relacher;
                s_Vtable[3] = (delegate* unmanaged[Stdcall]<void*, void*, int>)&Termine;
            }
            // L'objet : la vtable, puis son compte de références.
            var objet = (nint*)NativeMemory.Alloc(2, (nuint)sizeof(nint));
            objet[0] = (nint)s_Vtable;
            objet[1] = 0;
            Compte(objet) = 1;
            lock (s_Evenements) s_Evenements[(nint)objet] = evenement;
            return objet;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
        private static int Interroger(void* self, Guid* iid, void** resultat)
        {
            if (*iid == IID_IUnknown || *iid == IID_Rappel || *iid == IID_IAgileObject)
            {
                Interlocked.Increment(ref Compte(self));
                *resultat = self;
                return 0;
            }
            *resultat = null;
            return unchecked((int)0x80004002);                        // E_NOINTERFACE
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
        private static uint AjouterRef(void* self) => (uint)Interlocked.Increment(ref Compte(self));

        [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
        private static uint Relacher(void* self)
        {
            int reste = Interlocked.Decrement(ref Compte(self));
            if (reste == 0)
            {
                lock (s_Evenements) s_Evenements.Remove((nint)self);
                NativeMemory.Free(self);
            }
            return (uint)reste;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
        private static int Termine(void* self, void* operation)
        {
            ManualResetEventSlim? e;
            lock (s_Evenements) s_Evenements.TryGetValue((nint)self, out e);
            e?.Set();
            return 0;
        }
    }
}

/// <summary>La sortie du son chez l'invité (tâche 3.2).</summary>
internal sealed unsafe class SortieSon : IDisposable
{
    private static readonly Guid CLSID_MMDeviceEnumerator = new("BCDE0395-E52F-467C-8E3D-C4579291692E");
    private static readonly Guid IID_IMMDeviceEnumerator = new("A95664D2-9614-4F35-A746-DE8DB63617E6");

    /// <summary>Au-delà de cette réserve, un paquet est jeté : le retard ne s'accumule pas.</summary>
    private const int ReserveMaxMs = 80;

    /// <summary>
    /// La réserve que la sortie se refait quand elle est à sec : 40 ms de silence avant de reprendre.
    /// Sans elle, les paquets arrivant au rythme exact du jeu, la moindre avance perdue vidait la
    /// sortie entre deux paquets — des trous, des craquements (mesuré par --essai-son).
    /// </summary>
    private const int ReserveMinMs = 40;

    /// <summary>Les fois où la sortie s'est trouvée à sec et a dû refaire sa réserve.</summary>
    internal int Assechements { get; private set; }

    private void* _client, _rendu;
    private readonly uint _tailleTampon;

    internal SortieSon()
    {
        Com.InitialiserFil();
        void* enumerateur = Creer(CLSID_MMDeviceEnumerator, IID_IMMDeviceEnumerator, "MMDeviceEnumerator");
        void* peripherique;
        try { Verifier(((delegate* unmanaged[Stdcall]<void*, int, int, void**, int>)Vtable(enumerateur)[4])(enumerateur, 0, 0, &peripherique), "GetDefaultAudioEndpoint"); }
        finally { Liberer(enumerateur); }

        try
        {
            Guid iid = Audio.IID_IAudioClient;
            void* client;
            Verifier(((delegate* unmanaged[Stdcall]<void*, Guid*, uint, void*, void**, int>)Vtable(peripherique)[3])(peripherique, &iid, 23 /* CLSCTX_ALL */, null, &client), "IMMDevice.Activate");
            _client = client;
        }
        finally { Liberer(peripherique); }

        Audio.Initialiser(_client, Audio.AutoConversion | Audio.ConversionQualite, 1_000_000);   // 100 ms de tampon
        uint taille;
        Verifier(((delegate* unmanaged[Stdcall]<void*, uint*, int>)Vtable(_client)[4])(_client, &taille), "GetBufferSize");
        _tailleTampon = taille;
        _rendu = Audio.Service(_client, Audio.IID_IAudioRenderClient);
        Audio.Demarrer(_client);
    }

    /// <summary>Joue <paramref name="pcm"/> (16 bits stéréo, 48 kHz) à la suite de ce qui attend ; jeté si la réserve est pleine.</summary>
    internal bool Jouer(ReadOnlySpan<byte> pcm)
    {
        uint trames = (uint)(pcm.Length / Audio.OctetsParTrame);
        uint enAttente;
        if (((delegate* unmanaged[Stdcall]<void*, uint*, int>)Vtable(_client)[6])(_client, &enAttente) < 0) return false;
        if (enAttente > Audio.Frequence * ReserveMaxMs / 1000 || enAttente + trames > _tailleTampon) return false;

        byte* donnees;
        if (enAttente == 0)
        {
            // À sec : la réserve d'abord, en silence (AUDCLNT_BUFFERFLAGS_SILENT, rien à écrire).
            uint reserve = Audio.Frequence * ReserveMinMs / 1000;
            if (((delegate* unmanaged[Stdcall]<void*, uint, byte**, int>)Vtable(_rendu)[3])(_rendu, reserve, &donnees) >= 0)
                ((delegate* unmanaged[Stdcall]<void*, uint, uint, int>)Vtable(_rendu)[4])(_rendu, reserve, Audio.Silence);
            Assechements++;
        }
        if (((delegate* unmanaged[Stdcall]<void*, uint, byte**, int>)Vtable(_rendu)[3])(_rendu, trames, &donnees) < 0) return false;
        pcm[..(int)(trames * Audio.OctetsParTrame)].CopyTo(new Span<byte>(donnees, (int)(trames * Audio.OctetsParTrame)));
        ((delegate* unmanaged[Stdcall]<void*, uint, uint, int>)Vtable(_rendu)[4])(_rendu, trames, 0);
        return true;
    }

    public void Dispose()
    {
        if (_client != null) Audio.Arreter(_client);
        Liberer(_rendu);
        Liberer(_client);
        _rendu = _client = null;
    }
}
