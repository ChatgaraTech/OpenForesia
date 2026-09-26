// L'extraction des donnees d'une edition vers le dossier d'installation, verifiee fichier par
// fichier. Tout ce qu'elle a ecrit est retire si elle echoue ou si le joueur annule.

using OpenForesia.Installer.Editions;
using OpenForesia.Installer.Installation;
using OpenForesia.Installer.Iso;

namespace OpenForesia.Installer.Extraction;

internal static class ExtractionDonnees
{
    /// <summary>Le dossier des sauvegardes, relatif au dossier du jeu (<c>s_SaveFolderName_00563170</c>).</summary>
    internal const string DossierSauvegardes = "save";

    /// <summary>
    /// Extrait chaque fichier de l'edition dans <paramref name="dossier"/>, en comparant taille et
    /// MD5 a la liste. Un fichier s'ecrit d'abord sous un nom temporaire, renomme une fois verifie.
    /// <para>
    /// Sans <paramref name="journal"/>, l'extraction nettoie elle-meme ce qu'elle a cree en cas
    /// d'echec ou d'annulation. Avec, elle y note tout et laisse l'appelant decider : l'installeur
    /// retire alors aussi le jeu qu'il a depose avant.
    /// </para>
    /// </summary>
    internal static void Extraire(ImageIso image, Edition edition, string dossier,
        Action<long>? progression = null, Action<string>? fichierCourant = null,
        CancellationToken annulation = default, JournalInstallation? journal = null)
    {
        var parChemin = image.Fichiers.ToDictionary(f => f.Chemin, StringComparer.OrdinalIgnoreCase);
        bool journalPropre = journal == null;
        journal ??= new JournalInstallation();

        try
        {
            journal.CreerDossier(dossier);

            foreach (FichierEdition attendu in edition.Fichiers)
            {
                annulation.ThrowIfCancellationRequested();
                fichierCourant?.Invoke(attendu.Chemin);

                if (!parChemin.TryGetValue(attendu.Iso, out FichierIso? source))
                    throw new ImageIsoInvalideException($"Le fichier {attendu.Iso} manque dans l'image.");

                string cible = Path.Combine(dossier, attendu.Chemin);
                journal.CreerDossier(Path.GetDirectoryName(cible)!);

                string temporaire = cible + ".part";
                journal.NoterFichier(temporaire);
                string md5;
                using (var sortie = new FileStream(temporaire, FileMode.Create, FileAccess.Write, FileShare.None, 1 << 16))
                    md5 = image.Copier(source, attendu.Taille, sortie, progression, annulation);

                if (md5 != attendu.Md5)
                    throw new ImageIsoInvalideException(
                        $"Le fichier {attendu.Chemin} ne correspond pas à l'original : l'image est peut-être endommagée.");

                journal.NoterFichier(cible);
                File.Move(temporaire, cible, overwrite: true);
                journal.Oublier(temporaire);
            }

            journal.CreerDossier(Path.Combine(dossier, DossierSauvegardes));
        }
        catch
        {
            if (journalPropre) journal.Nettoyer();
            throw;
        }
    }
}
