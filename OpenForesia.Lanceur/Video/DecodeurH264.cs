// Le decodeur H.264 de l'invite (tache 2.4) : le decodeur de Windows (CLSID_CMSH264DecoderMFT),
// synchrone, en basse latence (MF_LOW_LATENCY : sans elle, il garde plusieurs images en reserve
// avant d'en rendre une). Une image H.264 (Annex B) entre, une image NV12 sort.
//
// A la premiere image cle, le decodeur apprend la taille du flux et le signale
// (MF_E_TRANSFORM_STREAM_CHANGE) : on choisit alors a nouveau la sortie NV12, a la bonne taille.

using static OpenForesia.Lanceur.Natif.Com;
using OpenForesia.Lanceur.Natif;

namespace OpenForesia.Lanceur.Video;

internal sealed unsafe class DecodeurH264 : IDisposable
{
    private static readonly Guid CLSID_CMSH264DecoderMFT = new("62CE7E72-4C71-4d20-B15D-452831A87D9D");

    private void* _mft;
    private long _images;

    internal int Largeur { get; private set; }
    internal int Hauteur { get; private set; }

    /// <summary>Images entrées moins images sorties : ce que le décodeur garde en réserve.</summary>
    internal long Retard => _images - _sorties;
    private long _sorties;

    internal DecodeurH264()
    {
        MF.Demarrer();
        _mft = Creer(CLSID_CMSH264DecoderMFT, MF.IID_IMFTransform, "décodeur H.264");

        void* attributs = MF.Attributs(_mft);
        if (attributs != null)
        {
            MF.PoserU32(attributs, MF.LowLatency, 1);
            Liberer(attributs);
        }

        void* entree = MF.NouveauType();
        try
        {
            MF.PoserGuid(entree, MF.MT_MAJOR_TYPE, MF.MediaType_Video);
            MF.PoserGuid(entree, MF.MT_SUBTYPE, MF.VideoFormat_H264);
            Verifier(MF.PoserTypeEntree(_mft, entree), "décodeur : type d'entrée H.264");
        }
        finally { Liberer(entree); }

        ChoisirSortie();
        MF.Message(_mft, MF.BeginStreaming);
        MF.Message(_mft, MF.StartOfStream);
    }

    /// <summary>La sortie NV12 parmi celles que le décodeur propose, et la taille qu'elle annonce.</summary>
    private void ChoisirSortie()
    {
        for (uint i = 0; ; i++)
        {
            void* type = MF.TypeSortiePropose(_mft, i);
            if (type == null) throw new InvalidOperationException("le décodeur H.264 ne propose pas de sortie NV12");
            try
            {
                if (MF.LireGuid(type, MF.MT_SUBTYPE) != MF.VideoFormat_NV12) continue;
                Verifier(MF.PoserTypeSortie(_mft, type), "décodeur : type de sortie NV12");
                ulong taille = MF.LireU64(type, MF.MT_FRAME_SIZE);
                Largeur = (int)(taille >> 32);
                Hauteur = (int)(uint)taille;
                return;
            }
            finally { Liberer(type); }
        }
    }

    /// <summary>
    /// Décode une image H.264 ; chaque image NV12 rendue est remise à <paramref name="image"/>
    /// (pixels, largeur, hauteur ; lignes de <c>largeur</c> octets, plan UV à la suite) le temps de
    /// l'appel. Faux si le décodeur a refusé l'image (à rattraper par une image clé).
    /// </summary>
    internal bool Decoder(ReadOnlySpan<byte> h264, Action<IntPtr, int, int> image)
    {
        void* echantillon = MF.NouvelEchantillon((uint)h264.Length, out void* tampon);
        try
        {
            byte* p = MF.Verrouiller(tampon, out _, out _);
            try { h264.CopyTo(new Span<byte>(p, h264.Length)); }
            finally { MF.Deverrouiller(tampon); }
            MF.PoserLongueur(tampon, (uint)h264.Length);
            const long Seconde = 10_000_000;
            MF.PoserTemps(echantillon, _images * Seconde / 60, Seconde / 60);
            _images++;
            if (MF.Entrer(_mft, echantillon) < 0) return false;
        }
        finally
        {
            Liberer(tampon);
            Liberer(echantillon);
        }

        while (true)
        {
            int taille = Math.Max((int)MF.InfoSortie(_mft).Taille, Math.Max(Largeur * Hauteur * 3 / 2, 16));
            void* sortieEchantillon = MF.NouvelEchantillon((uint)taille, out void* sortieTampon);
            try
            {
                var sortie = new MF.OutputDataBuffer { Echantillon = sortieEchantillon };
                int hr = MF.Sortir(_mft, &sortie);
                if (sortie.Evenements != null) Liberer(sortie.Evenements);
                if (hr == MF.NeedMoreInput) return true;
                if (hr == MF.StreamChange) { ChoisirSortie(); continue; }
                if (hr < 0) return false;

                byte* p = MF.Verrouiller(sortieTampon, out _, out uint longueur);
                try
                {
                    _sorties++;
                    if (longueur >= Largeur * Hauteur * 3 / 2) image((IntPtr)p, Largeur, Hauteur);
                }
                finally { MF.Deverrouiller(sortieTampon); }
            }
            finally
            {
                Liberer(sortieTampon);
                Liberer(sortieEchantillon);
            }
        }
    }

    public void Dispose()
    {
        Liberer(_mft);
        _mft = null;
    }
}
