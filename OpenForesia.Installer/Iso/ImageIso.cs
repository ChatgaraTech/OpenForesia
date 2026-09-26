// Lecture d'une image ISO 9660 a secteurs de 2048 octets, avec son extension Joliet.
//
// Juste ce dont l'installeur a besoin : l'etiquette du volume, la liste des fichiers, et la
// lecture d'un fichier en flux. Pas d'ecriture, pas de CD audio, pas de format brut (.bin 2352).
//
// LE PIEGE DE CETTE IMAGE : LES TAILLES DECLAREES MENTENT
//
// L'ISO de reference annonce 3,05 Go de fichiers sur 709 Mo. Trois sortes de mensonges, tous
// voulus au pressage (voir openspec/changes/install-from-iso/design.md) :
//   - `CD.IDX` declare 640 Mo : c'est la protection anticopie, et son etendue recouvre le disque ;
//   - `OPENING.AVI` et `ANI01.AVI` declarent une taille qui court jusqu'au dernier secteur, alors
//     que leur en-tete RIFF n'en revendique que 28 et 31 Mo ;
//   - `FRENCH2\` reprend les memes etendues que `FRENCH\`.
// D'ou `TailleReelle` : un fichier RIFF se lit jusqu'a la fin de son RIFF, pas au-dela.

using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace OpenForesia.Installer.Iso;

/// <summary>Une image refusee, avec un message lisible par un joueur.</summary>
internal sealed class ImageIsoInvalideException(string message) : Exception(message);

/// <summary>Un fichier de l'image : son chemin (noms Joliet, separateur <c>\</c>), son premier
/// secteur et la taille que declare son entree de repertoire.</summary>
internal sealed record FichierIso(string Chemin, long Secteur, long TailleDeclaree);

internal sealed class ImageIso : IDisposable
{
    internal const int TailleSecteur = 2048;
    private const int SecteurDescripteurs = 16;

    private readonly FileStream _flux;

    /// <summary>L'etiquette du volume, lue dans le descripteur primaire (ASCII, sans les espaces de
    /// fin) — <c>BLAZE_BLAZE_F</c> pour l'edition de reference.</summary>
    internal string Etiquette { get; }

    /// <summary>Les fichiers de l'image, dans l'ordre de leur premier secteur.</summary>
    internal IReadOnlyList<FichierIso> Fichiers { get; }

    /// <summary>Taille de l'image, en octets.</summary>
    internal long Taille => _flux.Length;

    private ImageIso(FileStream flux, string etiquette, IReadOnlyList<FichierIso> fichiers)
    {
        _flux = flux;
        Etiquette = etiquette;
        Fichiers = fichiers;
    }

    /// <summary>
    /// Ouvre une image. Leve <see cref="ImageIsoInvalideException"/> si ce n'est pas une ISO 9660
    /// a secteurs de 2048 octets — un <c>.bin</c> brut (2352), par exemple, n'a pas <c>CD001</c> au
    /// secteur 16.
    /// </summary>
    internal static ImageIso Ouvrir(string chemin)
    {
        var flux = new FileStream(chemin, FileMode.Open, FileAccess.Read, FileShare.Read, 1 << 16);
        try
        {
            if (flux.Length % TailleSecteur != 0 || flux.Length < (SecteurDescripteurs + 2) * TailleSecteur)
                throw new ImageIsoInvalideException(
                    "Ce fichier n'est pas une image ISO : il faut un fichier .iso du disque du jeu.");

            string? etiquette = null;
            byte[]? racinePrimaire = null;
            byte[]? racineJoliet = null;

            byte[] descripteur = new byte[TailleSecteur];
            for (long secteur = SecteurDescripteurs; ; secteur++)
            {
                if ((secteur + 1) * TailleSecteur > flux.Length) break;
                LireSecteurs(flux, secteur, descripteur);
                if (Encoding.ASCII.GetString(descripteur, 1, 5) != "CD001") break;

                byte type = descripteur[0];
                if (type == 255) break;   // terminateur de l'ensemble de descripteurs

                if (type == 1)
                {
                    etiquette = Encoding.ASCII.GetString(descripteur, 40, 32).TrimEnd(' ', '\0');
                    racinePrimaire = descripteur.AsSpan(156, 34).ToArray();
                }
                else if (type == 2 && EstJoliet(descripteur))
                {
                    racineJoliet = descripteur.AsSpan(156, 34).ToArray();
                }
            }

            if (etiquette == null || racinePrimaire == null)
                throw new ImageIsoInvalideException(
                    "Ce fichier n'est pas une image ISO : il faut un fichier .iso du disque du jeu.");

            // Joliet porte les noms en casse d'origine (« French\Teps.AVI ») ; le primaire, en
            // majuscules avec « ;1 ». Les etendues sont les memes.
            bool joliet = racineJoliet != null;
            var fichiers = new List<FichierIso>();
            var vus = new HashSet<long>();
            ParcourirDossier(flux, joliet ? racineJoliet! : racinePrimaire, "", joliet, fichiers, vus);
            fichiers.Sort((a, b) => a.Secteur.CompareTo(b.Secteur));

            return new ImageIso(flux, etiquette, fichiers);
        }
        catch
        {
            flux.Dispose();
            throw;
        }
    }

    /// <summary>Un descripteur supplementaire est Joliet quand ses sequences d'echappement
    /// (octets 88..90) valent <c>%/@</c>, <c>%/C</c> ou <c>%/E</c> — les trois niveaux UCS-2.</summary>
    private static bool EstJoliet(byte[] descripteur) =>
        descripteur[88] == 0x25 && descripteur[89] == 0x2f &&
        (descripteur[90] == 0x40 || descripteur[90] == 0x43 || descripteur[90] == 0x45);

    private static void ParcourirDossier(FileStream flux, byte[] entreeDossier, string prefixe, bool joliet,
        List<FichierIso> fichiers, HashSet<long> vus)
    {
        long secteur = BinaryPrimitives.ReadUInt32LittleEndian(entreeDossier.AsSpan(2));
        long taille = BinaryPrimitives.ReadUInt32LittleEndian(entreeDossier.AsSpan(10));

        // Un dossier deja parcouru ne l'est pas deux fois : une image piegee pourrait boucler.
        if (!vus.Add(secteur)) return;
        if (secteur * TailleSecteur + taille > flux.Length)
            throw new ImageIsoInvalideException("L'image ISO est incomplete ou endommagee.");

        byte[] contenu = new byte[(taille + TailleSecteur - 1) / TailleSecteur * TailleSecteur];
        LireSecteurs(flux, secteur, contenu);

        int position = 0;
        while (position < taille)
        {
            int longueur = contenu[position];
            if (longueur == 0)
            {
                // Une entree ne chevauche jamais deux secteurs : un zero signale la fin de celui-ci.
                position = (position / TailleSecteur + 1) * TailleSecteur;
                continue;
            }
            if (position + longueur > contenu.Length || longueur < 34)
                throw new ImageIsoInvalideException("L'image ISO est incomplete ou endommagee.");

            var entree = contenu.AsSpan(position, longueur);
            int longueurNom = entree[32];
            var nomBrut = entree.Slice(33, longueurNom);
            bool pointOuPointPoint = longueurNom == 1 && (nomBrut[0] == 0 || nomBrut[0] == 1);

            if (!pointOuPointPoint)
            {
                string nom = LireNom(nomBrut, joliet);
                bool estDossier = (entree[25] & 2) != 0;
                string chemin = prefixe.Length == 0 ? nom : prefixe + "\\" + nom;

                if (estDossier)
                {
                    ParcourirDossier(flux, entree.ToArray(), chemin, joliet, fichiers, vus);
                }
                else
                {
                    long debut = BinaryPrimitives.ReadUInt32LittleEndian(entree.Slice(2));
                    long declaree = BinaryPrimitives.ReadUInt32LittleEndian(entree.Slice(10));
                    fichiers.Add(new FichierIso(chemin, debut, declaree));
                }
            }

            position += longueur;
        }
    }

    /// <summary>Le nom d'une entree : UCS-2 grand-boutiste en Joliet, ASCII sinon, sans le
    /// suffixe de version <c>;1</c> ni le point final d'un nom sans extension.</summary>
    private static string LireNom(ReadOnlySpan<byte> brut, bool joliet)
    {
        string nom = joliet ? Encoding.BigEndianUnicode.GetString(brut) : Encoding.ASCII.GetString(brut);
        int version = nom.IndexOf(';');
        if (version >= 0) nom = nom[..version];
        return nom.TrimEnd('.');
    }

    private static void LireSecteurs(FileStream flux, long secteur, byte[] destination)
    {
        flux.Position = secteur * TailleSecteur;
        flux.ReadExactly(destination);
    }

    /// <summary>
    /// La taille a lire pour un fichier : celle de son en-tete RIFF quand il en a un et qu'elle est
    /// plus petite que la taille declaree (les films gonfles au pressage), la taille declaree sinon.
    /// Leve une exception si l'etendue qui en resulte sort de l'image.
    /// </summary>
    internal long TailleReelle(FichierIso fichier, out bool coupeRiff)
    {
        coupeRiff = false;
        long taille = fichier.TailleDeclaree;

        if (taille >= 12)
        {
            Span<byte> entete = stackalloc byte[8];
            _flux.Position = fichier.Secteur * TailleSecteur;
            _flux.ReadExactly(entete);
            if (entete[0] == (byte)'R' && entete[1] == (byte)'I' && entete[2] == (byte)'F' && entete[3] == (byte)'F')
            {
                long riff = 8L + BinaryPrimitives.ReadUInt32LittleEndian(entete[4..]);
                if (riff < taille)
                {
                    taille = riff;
                    coupeRiff = true;
                }
            }
        }

        if (fichier.Secteur * TailleSecteur + taille > _flux.Length)
            throw new ImageIsoInvalideException(
                $"L'image ISO est incomplete ou endommagee : {fichier.Chemin} en sort.");
        return taille;
    }

    /// <summary>
    /// Copie les <paramref name="taille"/> premiers octets d'un fichier vers
    /// <paramref name="destination"/> par blocs d'1 Mo, et rend leur MD5 en hexadecimal minuscule.
    /// <paramref name="progression"/> recoit le nombre d'octets de chaque bloc.
    /// </summary>
    internal string Copier(FichierIso fichier, long taille, Stream? destination,
        Action<long>? progression = null, CancellationToken annulation = default)
    {
        using var md5 = IncrementalHash.CreateHash(HashAlgorithmName.MD5);
        byte[] bloc = new byte[1 << 20];
        _flux.Position = fichier.Secteur * TailleSecteur;

        long reste = taille;
        while (reste > 0)
        {
            annulation.ThrowIfCancellationRequested();
            int n = (int)Math.Min(bloc.Length, reste);
            _flux.ReadExactly(bloc, 0, n);
            md5.AppendData(bloc, 0, n);
            destination?.Write(bloc, 0, n);
            reste -= n;
            progression?.Invoke(n);
        }

        return Convert.ToHexStringLower(md5.GetHashAndReset());
    }

    public void Dispose() => _flux.Dispose();
}
