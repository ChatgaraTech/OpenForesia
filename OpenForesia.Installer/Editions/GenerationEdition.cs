// La production d'une edition depuis une image : quels fichiers le jeu lit, a quelle taille, avec
// quelle somme. Lancee une fois par edition, par le developpeur, sur l'image de reference.
//
// La selection (design.md, D4) : les dossiers que le jeu lit — la langue, `SND`, `MUSIC` — moins
// ce qui n'est pas une donnee du jeu (programmes d'installation, DLL). Ce qu'elle laisse de cote
// sur l'image de reference, et pourquoi :
//   CD.IDX           la protection anticopie
//   FRENCH2\         un doublon exact de FRENCH\ (meme BLAZE.ALL au MD5 pres)
//   DATA\ (racine)   le lanceur d'installation de 1998 et BBPOOL.RSC, que rien ne lit
//   DIRECTX\, DXMEDIA\, SYSTEM\, SETUP.*, AUTORUN.INF, WSYSKERN.DLL, BLAZE.ICO
//   FRENCH\INSTALL.EXE, FRENCH\GKSL_FRN.DLL, FRENCH\SETUP.DA_, FRENCH\SETUP.OP_

using OpenForesia.Installer.Iso;

namespace OpenForesia.Installer.Editions;

internal static class GenerationEdition
{
    /// <summary>Le dossier de langue que lit le jeu (<c>s_CdDataRootPath</c>, « FRENCH\ »).</summary>
    private const string DossierLangue = "FRENCH";

    private static readonly string[] s_DossiersLus = [DossierLangue, "SND", "MUSIC"];
    private static readonly string[] s_ExtensionsExclues = [".EXE", ".DLL"];

    internal static bool EstUneDonneeDuJeu(string chemin)
    {
        string[] parties = chemin.Split('\\');
        if (parties.Length < 2) return false;
        if (!s_DossiersLus.Contains(parties[0], StringComparer.OrdinalIgnoreCase)) return false;

        string nom = parties[^1];
        if (s_ExtensionsExclues.Contains(Path.GetExtension(nom), StringComparer.OrdinalIgnoreCase)) return false;
        if (nom.StartsWith("SETUP.", StringComparison.OrdinalIgnoreCase)) return false;
        return true;
    }

    internal static Edition Generer(ImageIso image, string nom, TextWriter journal)
    {
        var edition = new Edition
        {
            Nom = nom,
            Etiquette = image.Etiquette,
            Archive = image.Fichiers.First(
                f => string.Equals(f.Chemin, DossierLangue + "\\BLAZE.ALL", StringComparison.OrdinalIgnoreCase)).Chemin,
        };

        foreach (FichierIso fichier in image.Fichiers.OrderBy(f => f.Chemin, StringComparer.OrdinalIgnoreCase))
        {
            if (!EstUneDonneeDuJeu(fichier.Chemin))
            {
                journal.WriteLine($"  ignore   {fichier.Chemin}");
                continue;
            }

            long taille = image.TailleReelle(fichier, out bool coupe);
            string md5 = image.Copier(fichier, taille, null);
            edition.Fichiers.Add(new FichierEdition
            {
                Iso = fichier.Chemin,
                Chemin = fichier.Chemin,
                Taille = taille,
                Md5 = md5,
                CoupeRiff = coupe,
            });
            if (string.Equals(fichier.Chemin, edition.Archive, StringComparison.OrdinalIgnoreCase))
                edition.ArchiveMd5 = md5;

            journal.WriteLine(coupe
                ? $"  retenu   {fichier.Chemin}  {taille:N0} octets (coupé au RIFF, l'image en déclare {fichier.TailleDeclaree:N0})"
                : $"  retenu   {fichier.Chemin}  {taille:N0} octets");
        }

        return edition;
    }
}
