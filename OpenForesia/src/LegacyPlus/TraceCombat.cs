// =====================================================================
// Diagnostic — le journal des coups de mêlée du groupe, `legacyplus-combat.log`.
//
// Hors reconstruction : un outil de mise au point, pas un tweak (aucun numéro LP). Activé par
// `[Diagnostic] TraceCombat = oui` ; absent du modèle de l'installeur. Il s'empile sur les coutures
// de LP-004 APRÈS le tweak, donc il voit ses décisions, et ne change aucune valeur.
//
// Pour chaque ennemi que le balayage de mêlée (Ghidra: FUN_00519940) examine, une issue :
//   ÉCARTÉ     le filtre d'état l'a écarté (bits restants nommés) ;
//   REFUSÉ     le verrou court et c'est le même geste (ou le verrou d'origine, sans le tweak) ;
//   TOUCHÉ     le coup est parti ;
//   RATÉ       il a passé les deux filtres mais aucun coup n'est parti : hors de portée
//              (`TestMeleeReach_00535A90`), ou paré — la parade exige la garde `0x10` et que
//              l'ennemi fasse face ; le journal dit s'il porte la garde.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class TraceCombat
{
    internal const string FileName = "legacyplus-combat.log";

    private static StreamWriter? s_Journal;
    private static int s_Image;
    private static readonly Dictionary<(nint Source, nint Victime), int> s_Candidats = new();
    private static readonly List<(nint Source, nint Victime)> s_Rates = new();

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.TraceCombat) return;

        try
        {
            s_Journal = new StreamWriter(Path.Combine(AppContext.BaseDirectory, FileName), append: false) { AutoFlush = true };
            s_Journal.WriteLine($"# OpenForesia — journal des coups de mêlée ; InvulnerabiliteEnnemis = {(ReglagesLegacyPlus.InvulnerabiliteEnnemis ? "oui" : "non")}");
            s_Journal.WriteLine("# image | joueur | ennemi (emplacement, espèce) | issue");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LEGACY++] {FileName} impossible à ouvrir ({ex.Message}) — pas de journal");
            return;
        }

        Coutures.Exclusion exclue = Coutures.VictimeExclue;
        Coutures.VictimeExclue = (balayage, source, victime, exclusion) =>
        {
            uint reste = exclue(balayage, source, victime, exclusion);
            if (balayage == Coutures.Balayage.Melee && reste != 0) Ecrire(source, victime, $"ÉCARTÉ ({Bits(reste)})");
            return reste;
        };

        Coutures.Protection protegee = Coutures.VictimeProtegee;
        Coutures.VictimeProtegee = (balayage, source, victime, verrou) =>
        {
            bool refuse = protegee(balayage, source, victime, verrou);
            if (balayage == Coutures.Balayage.Melee)
            {
                if (refuse) Ecrire(source, victime, "REFUSÉ (même geste, ou verrou d'origine)");
                else s_Candidats[((nint)source, (nint)victime)] = s_Image;
            }
            return refuse;
        };

        Coutures.Coup porte = Coutures.CoupPorte;
        Coutures.CoupPorte = (balayage, source, victime) =>
        {
            porte(balayage, source, victime);
            if (balayage != Coutures.Balayage.Melee) return;
            s_Candidats.Remove(((nint)source, (nint)victime));
            Ecrire(source, victime, "TOUCHÉ");
        };

        Action image = Coutures.ImageSuivante;
        Coutures.ImageSuivante = () =>
        {
            image();
            foreach (var (cle, vu) in s_Candidats)
            {
                if (vu < s_Image + 1) s_Rates.Add(cle);
            }
            foreach (var cle in s_Rates)
            {
                s_Candidats.Remove(cle);
                bool garde = (*(uint*)cle.Victime & 0x10) != 0;
                Ecrire((byte*)cle.Source, (byte*)cle.Victime,
                       $"RATÉ — hors de portée, ou paré (garde 0x10 : {(garde ? "oui" : "non")})");
            }
            s_Rates.Clear();
            s_Image++;
        };
    }

    private static void Ecrire(byte* source, byte* victime, string issue) =>
        s_Journal?.WriteLine($"{s_Image,7} | J{source[8]} | E{victime[8]:D2} esp. {victime[0xc]:D3} | {issue}");

    private static string Bits(uint bits)
    {
        var noms = new List<string>();
        if ((bits & 0x40) != 0) noms.Add("0x40 encaisse, hors fenêtre ou même geste");
        if ((bits & 0x40000000) != 0) noms.Add("0x40000000 mort");
        uint autres = bits & ~0x40000040u;
        if (autres != 0) noms.Add($"0x{autres:x}");
        return string.Join(", ", noms);
    }
}
