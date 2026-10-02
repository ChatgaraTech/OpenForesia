// Media Foundation a la main : types de media, echantillons, tampons, et les transformations (MFT)
// synchrones qu'utilisent l'encodeur et le decodeur H.264 (tache 2.2, design D3).
//
// Rangs de vtable (mfobjects.h, mftransform.h, strmif.h) :
//   IMFAttributes    GetUINT32 7, GetGUID 10, GetBlobSize 14, GetBlob 15, SetUINT32 21, SetUINT64 22, SetGUID 24
//   IMFSample        (IMFAttributes, 33 rangs) SetSampleTime 36, SetSampleDuration 38,
//                    ConvertToContiguousBuffer 41, AddBuffer 42
//   IMFMediaBuffer   Lock 3, Unlock 4, GetCurrentLength 5, SetCurrentLength 6
//   IMFTransform     GetOutputStreamInfo 7, GetAttributes 8, GetOutputAvailableType 14, SetInputType 15,
//                    SetOutputType 16, ProcessMessage 23, ProcessInput 24, ProcessOutput 25
//   ICodecAPI        SetValue 9

using System.Runtime.InteropServices;
using static OpenForesia.Lanceur.Natif.Com;

namespace OpenForesia.Lanceur.Natif;

internal static unsafe partial class MF
{
    [LibraryImport("mfplat.dll")] private static partial int MFStartup(uint version, uint drapeaux);
    [LibraryImport("mfplat.dll")] internal static partial int MFCreateMediaType(void** type);
    [LibraryImport("mfplat.dll")] internal static partial int MFCreateSample(void** echantillon);
    [LibraryImport("mfplat.dll")] internal static partial int MFCreateMemoryBuffer(uint taille, void** tampon);

    private const uint VersionMF = 0x00020070;

    internal static readonly Guid MT_MAJOR_TYPE = new("48eba18e-f8c9-4687-bf11-0a74c9f96a8f");
    internal static readonly Guid MT_SUBTYPE = new("f7e34c9a-42e8-4714-b74b-cb29d72c35e5");
    internal static readonly Guid MT_FRAME_SIZE = new("1652c33d-d6b2-4012-b834-72030849a37d");
    internal static readonly Guid MT_FRAME_RATE = new("c459a2e8-3d2c-4e44-b132-fee5156c7bb0");
    internal static readonly Guid MT_AVG_BITRATE = new("20332624-fb0d-4d9e-bd0d-cbf6786c102e");
    internal static readonly Guid MT_INTERLACE_MODE = new("e2724bb8-e676-4806-b4b2-a8d6efb44ccd");
    internal static readonly Guid MT_PIXEL_ASPECT_RATIO = new("c6376a1e-8d0a-4027-be45-6d9a0ad39bb6");
    internal static readonly Guid MT_MPEG2_PROFILE = new("ad76a80b-2d5c-4e0b-b375-64e520137036");
    internal static readonly Guid MT_MPEG_SEQUENCE_HEADER = new("3c036de7-3ad0-4c9e-9216-ee6d6ac21cb3");
    internal static readonly Guid MediaType_Video = new("73646976-0000-0010-8000-00AA00389B71");
    internal static readonly Guid VideoFormat_H264 = new("34363248-0000-0010-8000-00AA00389B71");
    internal static readonly Guid VideoFormat_NV12 = new("3231564E-0000-0010-8000-00AA00389B71");
    internal static readonly Guid LowLatency = new("9c27891a-ed7a-40e1-88e8-b22727a024ee");      // MF_LOW_LATENCY = CODECAPI_AVLowLatencyMode

    internal static readonly Guid IID_IMFTransform = new("bf94c121-5b05-4e6f-8000-ba598961414d");
    internal static readonly Guid IID_ICodecAPI = new("901db4c7-31ce-41a2-85dc-8fa0bf41b8da");

    internal const int NeedMoreInput = unchecked((int)0xC00D6D72);        // MF_E_TRANSFORM_NEED_MORE_INPUT
    internal const int StreamChange = unchecked((int)0xC00D6D61);         // MF_E_TRANSFORM_STREAM_CHANGE
    internal const uint MessageFlush = 0, BeginStreaming = 0x10000000, StartOfStream = 0x10000003;

    [StructLayout(LayoutKind.Sequential)]
    internal struct OutputStreamInfo { public uint Drapeaux, Taille, Alignement; }

    [StructLayout(LayoutKind.Sequential)]
    internal struct OutputDataBuffer { public uint Flux; public void* Echantillon; public uint Etat; public void* Evenements; }

    /// <summary>VARIANT d'un processus 32 bits : vt, trois réservés, puis 8 octets de valeur.</summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    internal struct Variant
    {
        [FieldOffset(0)] public ushort Vt;
        [FieldOffset(8)] public uint U4;
        [FieldOffset(8)] public ulong U8;
    }

    private static int s_Demarre;

    internal static void Demarrer()
    {
        InitialiserFil();
        if (Interlocked.Exchange(ref s_Demarre, 1) == 0) Verifier(MFStartup(VersionMF, 0), "MFStartup");
    }

    // ---- attributs ---------------------------------------------------------------------------------

    internal static void PoserGuid(void* a, Guid cle, Guid valeur) =>
        Verifier(((delegate* unmanaged[Stdcall]<void*, Guid*, Guid*, int>)Vtable(a)[24])(a, &cle, &valeur), "SetGUID");

    internal static void PoserU32(void* a, Guid cle, uint valeur) =>
        Verifier(((delegate* unmanaged[Stdcall]<void*, Guid*, uint, int>)Vtable(a)[21])(a, &cle, valeur), "SetUINT32");

    internal static void PoserU64(void* a, Guid cle, ulong valeur) =>
        Verifier(((delegate* unmanaged[Stdcall]<void*, Guid*, ulong, int>)Vtable(a)[22])(a, &cle, valeur), "SetUINT64");

    /// <summary>Deux entiers de 32 bits rangés dans un UINT64, comme MFSetAttributeSize et MFSetAttributeRatio.</summary>
    internal static void PoserPaire(void* a, Guid cle, uint haut, uint bas) => PoserU64(a, cle, ((ulong)haut << 32) | bas);

    internal static ulong LireU64(void* a, Guid cle)
    {
        ulong valeur;
        return ((delegate* unmanaged[Stdcall]<void*, Guid*, ulong*, int>)Vtable(a)[8])(a, &cle, &valeur) < 0 ? 0 : valeur;
    }

    internal static Guid LireGuid(void* a, Guid cle)
    {
        Guid valeur;
        return ((delegate* unmanaged[Stdcall]<void*, Guid*, Guid*, int>)Vtable(a)[10])(a, &cle, &valeur) < 0 ? Guid.Empty : valeur;
    }

    internal static byte[]? LireBlob(void* a, Guid cle)
    {
        uint taille;
        if (((delegate* unmanaged[Stdcall]<void*, Guid*, uint*, int>)Vtable(a)[14])(a, &cle, &taille) < 0) return null;
        var blob = new byte[taille];
        fixed (byte* b = blob)
            if (((delegate* unmanaged[Stdcall]<void*, Guid*, byte*, uint, uint*, int>)Vtable(a)[15])(a, &cle, b, taille, &taille) < 0) return null;
        return blob;
    }

    internal static void* NouveauType()
    {
        void* t;
        Verifier(MFCreateMediaType(&t), "MFCreateMediaType");
        return t;
    }

    // ---- échantillons ------------------------------------------------------------------------------

    /// <summary>Un échantillon portant un tampon mémoire de <paramref name="taille"/> octets.</summary>
    internal static void* NouvelEchantillon(uint taille, out void* tampon)
    {
        void* e, t;
        Verifier(MFCreateSample(&e), "MFCreateSample");
        Verifier(MFCreateMemoryBuffer(taille, &t), "MFCreateMemoryBuffer");
        Verifier(((delegate* unmanaged[Stdcall]<void*, void*, int>)Vtable(e)[42])(e, t), "AddBuffer");
        tampon = t;
        return e;
    }

    internal static void PoserTemps(void* echantillon, long temps, long duree)
    {
        Verifier(((delegate* unmanaged[Stdcall]<void*, long, int>)Vtable(echantillon)[36])(echantillon, temps), "SetSampleTime");
        Verifier(((delegate* unmanaged[Stdcall]<void*, long, int>)Vtable(echantillon)[38])(echantillon, duree), "SetSampleDuration");
    }

    internal static byte* Verrouiller(void* tampon, out uint max, out uint courant)
    {
        byte* p;
        uint m, c;
        Verifier(((delegate* unmanaged[Stdcall]<void*, byte**, uint*, uint*, int>)Vtable(tampon)[3])(tampon, &p, &m, &c), "IMFMediaBuffer.Lock");
        max = m;
        courant = c;
        return p;
    }

    internal static void Deverrouiller(void* tampon) => ((delegate* unmanaged[Stdcall]<void*, int>)Vtable(tampon)[4])(tampon);

    internal static void PoserLongueur(void* tampon, uint longueur) =>
        Verifier(((delegate* unmanaged[Stdcall]<void*, uint, int>)Vtable(tampon)[6])(tampon, longueur), "SetCurrentLength");

    /// <summary>Le premier (et, pour nous, seul) tampon d'un échantillon rendu par une transformation.</summary>
    internal static void* TamponContigu(void* echantillon)
    {
        void* t;
        Verifier(((delegate* unmanaged[Stdcall]<void*, void**, int>)Vtable(echantillon)[41])(echantillon, &t), "ConvertToContiguousBuffer");
        return t;
    }

    // ---- transformations ---------------------------------------------------------------------------

    internal static int PoserTypeEntree(void* mft, void* type) =>
        ((delegate* unmanaged[Stdcall]<void*, uint, void*, uint, int>)Vtable(mft)[15])(mft, 0, type, 0);

    internal static int PoserTypeSortie(void* mft, void* type) =>
        ((delegate* unmanaged[Stdcall]<void*, uint, void*, uint, int>)Vtable(mft)[16])(mft, 0, type, 0);

    /// <summary>Le type de sortie proposé au rang <paramref name="i"/>, ou null au-delà du dernier.</summary>
    internal static void* TypeSortiePropose(void* mft, uint i)
    {
        void* t;
        return ((delegate* unmanaged[Stdcall]<void*, uint, uint, void**, int>)Vtable(mft)[14])(mft, 0, i, &t) < 0 ? null : t;
    }

    internal static OutputStreamInfo InfoSortie(void* mft)
    {
        OutputStreamInfo info;
        Verifier(((delegate* unmanaged[Stdcall]<void*, uint, OutputStreamInfo*, int>)Vtable(mft)[7])(mft, 0, &info), "GetOutputStreamInfo");
        return info;
    }

    /// <summary>Les attributs de la transformation elle-même (où poser MF_LOW_LATENCY), ou null.</summary>
    internal static void* Attributs(void* mft)
    {
        void* a;
        return ((delegate* unmanaged[Stdcall]<void*, void**, int>)Vtable(mft)[8])(mft, &a) < 0 ? null : a;
    }

    internal static void Message(void* mft, uint message) =>
        ((delegate* unmanaged[Stdcall]<void*, uint, nint, int>)Vtable(mft)[23])(mft, message, 0);

    internal static int Entrer(void* mft, void* echantillon) =>
        ((delegate* unmanaged[Stdcall]<void*, uint, void*, uint, int>)Vtable(mft)[24])(mft, 0, echantillon, 0);

    internal static int Sortir(void* mft, OutputDataBuffer* sortie)
    {
        uint etat;
        return ((delegate* unmanaged[Stdcall]<void*, uint, uint, OutputDataBuffer*, uint*, int>)Vtable(mft)[25])(mft, 0, 1, sortie, &etat);
    }

    /// <summary>ICodecAPI.SetValue d'un entier (VT_UI4) ; faux si la transformation ne connaît pas ce réglage.</summary>
    internal static bool Regler(void* mft, Guid reglage, uint valeur) => Regler(mft, reglage, new Variant { Vt = 19 /* VT_UI4 */, U4 = valeur });

    /// <summary>
    /// ICodecAPI.SetValue d'un booléen : VT_BOOL, vrai = 0xFFFF (VARIANT_TRUE). La basse latence en
    /// est un ; envoyée en VT_UI4, l'encodeur la refusait sans bruit et gardait six images en réserve.
    /// </summary>
    internal static bool ReglerBooleen(void* mft, Guid reglage, bool valeur) =>
        Regler(mft, reglage, new Variant { Vt = 11 /* VT_BOOL */, U4 = valeur ? 0xFFFFu : 0 });

    private static bool Regler(void* mft, Guid reglage, Variant v)
    {
        void* api = Interroger(mft, IID_ICodecAPI);
        if (api == null) return false;
        try { return ((delegate* unmanaged[Stdcall]<void*, Guid*, Variant*, int>)Vtable(api)[9])(api, &reglage, &v) >= 0; }
        finally { Liberer(api); }
    }
}
