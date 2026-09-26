// Les editions du jeu que l'installeur sait reconnaitre, et la liste exacte de leurs fichiers.
//
// `editions.json` est la source unique : l'installeur l'emploie pour reconnaitre une image et
// verifier ce qu'il extrait, le jeu pour verifier au demarrage que ses donnees sont la. Il ne
// contient aucune donnee du jeu — des chemins, des tailles et des sommes MD5. Il est GENERE depuis
// l'ISO de reference (`OpenForesia-Setup --generer-edition`), jamais tape a la main.

using System.Text.Json;
using System.Text.Json.Serialization;
using OpenForesia.Installer.Iso;

namespace OpenForesia.Installer.Editions;

internal sealed class FichierEdition
{
    /// <summary>Le chemin dans l'image (noms Joliet).</summary>
    [JsonPropertyName("iso")] public string Iso { get; set; } = "";

    /// <summary>Le chemin installe, relatif au dossier du jeu.</summary>
    [JsonPropertyName("chemin")] public string Chemin { get; set; } = "";

    /// <summary>La taille installee — celle du RIFF pour un film coupe.</summary>
    [JsonPropertyName("taille")] public long Taille { get; set; }

    [JsonPropertyName("md5")] public string Md5 { get; set; } = "";

    /// <summary>Vrai quand l'entree de l'image declare plus que le RIFF et qu'on s'arrete au RIFF.</summary>
    [JsonPropertyName("coupeRiff")] public bool CoupeRiff { get; set; }
}

internal sealed class Edition
{
    [JsonPropertyName("nom")] public string Nom { get; set; } = "";

    /// <summary>L'etiquette du volume, premier critere de reconnaissance.</summary>
    [JsonPropertyName("etiquette")] public string Etiquette { get; set; } = "";

    /// <summary>Le chemin dans l'image de l'archive principale, second critere : sa somme MD5
    /// distingue deux pressages qui partageraient une etiquette.</summary>
    [JsonPropertyName("archive")] public string Archive { get; set; } = "";

    [JsonPropertyName("archiveMd5")] public string ArchiveMd5 { get; set; } = "";

    [JsonPropertyName("fichiers")] public List<FichierEdition> Fichiers { get; set; } = [];

    [JsonIgnore] public long TailleTotale => Fichiers.Sum(f => f.Taille);
}

internal sealed class CatalogueEditions
{
    [JsonPropertyName("editions")] public List<Edition> Editions { get; set; } = [];

    private static readonly JsonSerializerOptions s_Options = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    /// <summary>Le catalogue embarque dans l'installeur.</summary>
    internal static CatalogueEditions Embarque()
    {
        using Stream flux = typeof(CatalogueEditions).Assembly.GetManifestResourceStream("editions.json")
            ?? throw new InvalidOperationException("editions.json n'est pas embarque dans l'installeur.");
        return JsonSerializer.Deserialize<CatalogueEditions>(flux, s_Options) ?? new CatalogueEditions();
    }

    internal static CatalogueEditions Lire(string chemin) =>
        File.Exists(chemin)
            ? JsonSerializer.Deserialize<CatalogueEditions>(File.ReadAllText(chemin), s_Options) ?? new CatalogueEditions()
            : new CatalogueEditions();

    internal void Ecrire(string chemin) =>
        File.WriteAllText(chemin, JsonSerializer.Serialize(this, s_Options).Replace("\r\n", "\n") + "\n");

    /// <summary>
    /// L'edition de cette image, ou une exception au message lisible. Deux criteres : l'etiquette du
    /// volume, puis la somme MD5 de l'archive principale.
    /// </summary>
    internal Edition Reconnaitre(ImageIso image)
    {
        var candidates = Editions.Where(e => e.Etiquette == image.Etiquette).ToList();
        if (candidates.Count == 0)
            throw new ImageIsoInvalideException(
                $"Cette image n'est pas un disque de Blaze & Blade reconnu (volume « {image.Etiquette} »). " +
                "L'installeur attend l'image du disque français d'origine.");

        foreach (Edition edition in candidates)
        {
            FichierIso? archive = image.Fichiers.FirstOrDefault(
                f => string.Equals(f.Chemin, edition.Archive, StringComparison.OrdinalIgnoreCase));
            if (archive == null) continue;

            long taille = image.TailleReelle(archive, out _);
            if (image.Copier(archive, taille, null) == edition.ArchiveMd5) return edition;
        }

        throw new ImageIsoInvalideException(
            "Cette image ressemble au disque de Blaze & Blade, mais son contenu est différent de celui " +
            "attendu : elle est peut-être endommagée.");
    }
}
