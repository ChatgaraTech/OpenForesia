// L'encodeur H.264 de l'hote (tache 2.2, design D3) : l'encodeur logiciel de Windows
// (CLSID_MSH264EncoderMFT), une transformation synchrone — une image NV12 entre, une image H.264
// sort, sans file ni evenement. Profil Baseline (donc sans images B), mode basse latence, debit
// constant, une image cle toutes les deux secondes et a la demande (un invite qui a perdu un morceau).
//
// La sortie est un flux Annex B (NAL precedees de 00 00 00 01). Une image cle part toujours avec
// ses parametres de sequence (SPS, PPS) : un invite qui arrive en cours de route decode des la
// premiere image cle recue.

using static OpenForesia.Lanceur.Natif.Com;
using OpenForesia.Lanceur.Natif;

namespace OpenForesia.Lanceur.Video;

internal sealed unsafe class EncodeurH264 : IDisposable
{
    private static readonly Guid CLSID_MSH264EncoderMFT = new("6CA50344-051A-4DED-9779-A43305165E35");
    private static readonly Guid AVEncVideoForceKeyFrame = new("398c1b98-8353-475a-9ef2-8f265d260345");
    private static readonly Guid AVEncMPVGOPSize = new("95f31b26-95a4-41aa-9303-246a7fc6eef1");
    private static readonly Guid AVEncCommonRateControlMode = new("1c0608e9-370c-4710-8a58-cb6181c42423");
    private static readonly Guid AVEncCommonMeanBitRate = new("f7222374-2144-4815-b550-a37f8e12ee52");
    private static readonly Guid AVEncMPVDefaultBPictureCount = new("8d390aac-dc5c-4200-b57f-814d04babab2");
    private static readonly Guid SampleCleanPoint = new("9cdf01d8-a0f0-43ba-b077-eaa06cbd728a");     // MFSampleExtension_CleanPoint

    private const uint ProfilBaseline = 66, Progressif = 2, DebitConstant = 0;

    private void* _mft;
    private byte[] _entetes = [];                         // SPS et PPS, en Annex B (le type de sortie courant de l'encodeur)
    private readonly int _largeur, _hauteur;
    private readonly uint _tailleNv12;
    private long _images;

    internal int Largeur => _largeur;

    /// <summary>Images entrées moins images sorties : ce que l'encodeur garde en réserve (0 ou 1 en basse latence).</summary>
    internal long Retard => _images - _sorties;
    private long _sorties;
    internal int Hauteur => _hauteur;

    internal EncodeurH264(int largeur, int hauteur, int imagesParSeconde, int debit)
    {
        _largeur = largeur;
        _hauteur = hauteur;
        _tailleNv12 = (uint)(largeur * hauteur * 3 / 2);
        MF.Demarrer();
        _mft = Creer(CLSID_MSH264EncoderMFT, MF.IID_IMFTransform, "encodeur H.264");

        // Les réglages AVANT les types : l'encodeur ne relit pas la basse latence ni le mode de débit
        // une fois ses types posés. Réglés après au premier jet, il gardait six images en réserve
        // (mesuré, --essai-image) : une seconde de retard à l'essai de Vincent, le 2026-09-30.
        // Un réglage que l'encodeur ne connaîtrait pas est sans gravité : il garde le sien.
        bool basseLatence = MF.ReglerBooleen(_mft, MF.LowLatency, true);
        MF.Regler(_mft, AVEncCommonRateControlMode, DebitConstant);
        MF.Regler(_mft, AVEncCommonMeanBitRate, (uint)debit);
        MF.Regler(_mft, AVEncMPVGOPSize, (uint)(imagesParSeconde * 2));
        MF.Regler(_mft, AVEncMPVDefaultBPictureCount, 0);
        if (!basseLatence) Console.Error.WriteLine("[VIDÉO] l'encodeur refuse la basse latence : l'image aura du retard");

        // Un encodeur veut son type de sortie avant son type d'entrée.
        void* sortie = MF.NouveauType();
        try
        {
            MF.PoserGuid(sortie, MF.MT_MAJOR_TYPE, MF.MediaType_Video);
            MF.PoserGuid(sortie, MF.MT_SUBTYPE, MF.VideoFormat_H264);
            MF.PoserU32(sortie, MF.MT_AVG_BITRATE, (uint)debit);
            MF.PoserPaire(sortie, MF.MT_FRAME_RATE, (uint)imagesParSeconde, 1);
            MF.PoserPaire(sortie, MF.MT_FRAME_SIZE, (uint)largeur, (uint)hauteur);
            MF.PoserU32(sortie, MF.MT_INTERLACE_MODE, Progressif);
            MF.PoserPaire(sortie, MF.MT_PIXEL_ASPECT_RATIO, 1, 1);
            MF.PoserU32(sortie, MF.MT_MPEG2_PROFILE, ProfilBaseline);
            Verifier(MF.PoserTypeSortie(_mft, sortie), "encodeur : type de sortie H.264");

        }
        finally { Liberer(sortie); }

        void* entree = MF.NouveauType();
        try
        {
            MF.PoserGuid(entree, MF.MT_MAJOR_TYPE, MF.MediaType_Video);
            MF.PoserGuid(entree, MF.MT_SUBTYPE, MF.VideoFormat_NV12);
            MF.PoserPaire(entree, MF.MT_FRAME_RATE, (uint)imagesParSeconde, 1);
            MF.PoserPaire(entree, MF.MT_FRAME_SIZE, (uint)largeur, (uint)hauteur);
            MF.PoserU32(entree, MF.MT_INTERLACE_MODE, Progressif);
            MF.PoserPaire(entree, MF.MT_PIXEL_ASPECT_RATIO, 1, 1);
            Verifier(MF.PoserTypeEntree(_mft, entree), "encodeur : type d'entrée NV12");
        }
        finally { Liberer(entree); }

        MF.Message(_mft, MF.BeginStreaming);
        MF.Message(_mft, MF.StartOfStream);
        LireEntetes();
    }

    /// <summary>Les en-têtes de séquence, que l'encodeur pose sur son type de sortie courant (GetOutputCurrentType, rang 18).</summary>
    private void LireEntetes()
    {
        void* type;
        if (((delegate* unmanaged[Stdcall]<void*, uint, void**, int>)Vtable(_mft)[18])(_mft, 0, &type) < 0) return;
        try { _entetes = MF.LireBlob(type, MF.MT_MPEG_SEQUENCE_HEADER) ?? []; }
        finally { Liberer(type); }
    }

    /// <summary>
    /// Encode une image NV12 (<see cref="Largeur"/> × <see cref="Hauteur"/>, remplie par
    /// <paramref name="remplir"/> dans le tampon de l'encodeur) et rend l'image H.264 obtenue — ou
    /// null si l'encodeur n'en rend pas encore. <paramref name="cle"/> dit si c'est une image clé.
    /// </summary>
    internal byte[]? Encoder(Action<IntPtr> remplir, bool forcerCle, out bool cle)
    {
        cle = false;
        if (forcerCle) MF.Regler(_mft, AVEncVideoForceKeyFrame, 1);

        void* echantillon = MF.NouvelEchantillon(_tailleNv12, out void* tampon);
        try
        {
            byte* p = MF.Verrouiller(tampon, out _, out _);
            try { remplir((IntPtr)p); }
            finally { MF.Deverrouiller(tampon); }
            MF.PoserLongueur(tampon, _tailleNv12);
            const long Seconde = 10_000_000;
            MF.PoserTemps(echantillon, _images * Seconde / 60, Seconde / 60);
            _images++;
            Verifier(MF.Entrer(_mft, echantillon), "encodeur : ProcessInput");
        }
        finally
        {
            Liberer(tampon);
            Liberer(echantillon);
        }

        return Recuperer(out cle);
    }

    private byte[]? Recuperer(out bool cle)
    {
        cle = false;
        MF.OutputStreamInfo info = MF.InfoSortie(_mft);
        void* echantillon = MF.NouvelEchantillon(Math.Max(info.Taille, _tailleNv12), out void* tampon);
        try
        {
            var sortie = new MF.OutputDataBuffer { Echantillon = echantillon };
            int hr = MF.Sortir(_mft, &sortie);
            if (hr == MF.NeedMoreInput) return null;
            _sorties++;
            Verifier(hr, "encodeur : ProcessOutput");
            if (sortie.Evenements != null) Liberer(sortie.Evenements);

            uint point = 0;
            Guid cleanPoint = SampleCleanPoint;
            ((delegate* unmanaged[Stdcall]<void*, Guid*, uint*, int>)Vtable(echantillon)[7])(echantillon, &cleanPoint, &point);   // GetUINT32
            cle = point != 0;

            byte* p = MF.Verrouiller(tampon, out _, out uint longueur);
            try
            {
                var donnees = new ReadOnlySpan<byte>(p, (int)longueur);
                // Une image clé sans son SPS en tête reçoit les en-têtes de séquence du type de sortie.
                if (cle && _entetes.Length == 0) LireEntetes();
                bool avecEntetes = cle && _entetes.Length > 0 && !CommenceParSps(donnees);
                var image = new byte[(avecEntetes ? _entetes.Length : 0) + donnees.Length];
                if (avecEntetes) _entetes.CopyTo(image, 0);
                donnees.CopyTo(image.AsSpan(avecEntetes ? _entetes.Length : 0));
                return image;
            }
            finally { MF.Deverrouiller(tampon); }
        }
        finally
        {
            Liberer(tampon);
            Liberer(echantillon);
        }
    }

    /// <summary>La première NAL après un code de départ est un SPS (type 7).</summary>
    private static bool CommenceParSps(ReadOnlySpan<byte> d)
    {
        for (int i = 0; i + 3 < d.Length && i < 64; i++)
        {
            if (d[i] != 0 || d[i + 1] != 0) continue;
            if (d[i + 2] == 1) return (d[i + 3] & 0x1F) == 7;
            if (d[i + 2] == 0 && i + 4 < d.Length && d[i + 3] == 1) return (d[i + 4] & 0x1F) == 7;
        }
        return false;
    }

    public void Dispose()
    {
        Liberer(_mft);
        _mft = null;
    }
}
