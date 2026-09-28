// =====================================================================
// LP-011 — le rachat : la Voleuse du hub rachète ce que le joueur porte sur lui (sauf ce qu'il a
// équipé) et ce qu'il a laissé dans son coffre.
//
// Hors reconstruction (legacy ++, demande de Vincent du 2026-09-28). Décisions de Vincent :
//   - le prix : `[Vente] Taux` % (50 par défaut) du prix d'achat de l'objet dans l'une des quatre
//     boutiques ; un objet absent des quatre catalogues n'est PAS proposé ;
//   - une pile se vend en entier, d'un seul appui (prix unitaire × nombre).
//
// L'inventaire est en mémoire : dix emplacements de 0x280 octets à perChar + 0x300, vidés comme le jeu
// les vide — le premier octet à zéro (FormatSauvegarde.md). Un emplacement porté (perChar + 0x2b0..0x2b4
// en désigne cinq par leur numéro) n'est jamais proposé.
//
// Le COFFRE n'est pas en mémoire : ses douze emplacements vivent dans le fichier de sauvegarde du
// personnage (save\<perChar + 0x1fd0>, en 0x2000 + j × 0x280), relus seulement à l'ouverture du coffre
// (WorldMapDepot.cs). Vendre un objet du coffre, c'est donc écrire ce fichier, et l'écrire comme
// l'éditeur de sauvegarde le fait :
//   - l'emplacement vidé (premier octet à zéro), l'or crédité dans la fiche DU FICHIER (+0x26c) — la
//     vente tient même si le joueur ne sauvegarde pas ensuite ;
//   - la somme de contrôle recalculée (0x1ffc : les 0x1ffc premiers octets, signés) et la copie de
//     0x1f00..0x1fff remise en 0x3f00 ;
//   - la même somme recopiée dans le personnage en mémoire (perChar + 0x1ffc) : le coffre compare ce
//     mot à la copie 0x3ffc du fichier, et se croirait vide s'ils divergeaient.
// Un coffre dont la copie ne correspond déjà pas (le coffre du jeu le croirait vide) n'est pas lu.
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Text;
using Jeu = OpenForesia.Blaze.Blaze;

namespace OpenForesia.LegacyPlus;

internal static unsafe class BoutiqueVente
{
    private const int Emplacements = 10, EmplacementsCoffre = 12, Pas = 0x280;
    private const int Coffre = 0x2000, Somme = 0x1ffc, Copie = 0x3f00, BourseMax = 99999;

    /// <summary>D'où vient une ligne : l'emplacement d'inventaire, ou celui du coffre.</summary>
    private sealed record Source(bool DuCoffre, int Emplacement, byte[] Tete, int Total);

    private static readonly Dictionary<BoutiqueEcran.Ligne, Source> s_Sources = new(ReferenceEqualityComparer.Instance);

    /// <summary>Ce que le joueur peut vendre : l'inventaire hors équipement, puis le coffre.</summary>
    internal static IReadOnlyList<BoutiqueEcran.Ligne> Lister(byte* perChar, IReadOnlyDictionary<string, int> prix, int taux)
    {
        s_Sources.Clear();
        var lignes = new List<BoutiqueEcran.Ligne>();

        var portes = new HashSet<int>();
        for (int k = 0; k < 5; k++) if (perChar[0x2b0 + k] != 0xff) portes.Add(perChar[0x2b0 + k]);

        for (int i = 0; i < Emplacements; i++)
        {
            if (portes.Contains(i)) continue;
            byte* e = perChar + 0x300 + i * Pas;
            if (*e == 0) continue;
            var copie = new ReadOnlySpan<byte>(e, Pas).ToArray();
            Ajouter(lignes, copie, prix, taux, false, i);
        }

        byte[]? fichier = LireCoffre(perChar);
        if (fichier != null)
        {
            for (int j = 0; j < EmplacementsCoffre; j++)
            {
                int o = Coffre + j * Pas;
                if (fichier[o] == 0) continue;
                Ajouter(lignes, fichier.AsSpan(o, Pas).ToArray(), prix, taux, true, j);
            }
        }
        return lignes;
    }

    private static void Ajouter(List<BoutiqueEcran.Ligne> lignes, byte[] emplacement, IReadOnlyDictionary<string, int> prix,
                                int taux, bool duCoffre, int n)
    {
        string nom = Nom(emplacement);
        if (!prix.TryGetValue(nom, out int achat)) return;              // absent des catalogues : invendable

        int nombre = (emplacement[0x13] & 0x30) != 0 ? Math.Max(1, (int)BitConverter.ToUInt16(emplacement, 0x10)) : 1;
        int unitaire = Math.Max(1, achat * taux / 100);
        int total = unitaire * nombre;
        string affiche = (duCoffre ? "[C] " : "") + nom + (nombre > 1 ? $" x{nombre}" : "");

        var ligne = new BoutiqueEcran.Ligne(affiche, nom, total, emplacement[..0x140], emplacement[0x140..0x280]);
        s_Sources[ligne] = new Source(duCoffre, n, emplacement[..0x10], total);
        lignes.Add(ligne);
    }

    /// <summary>Vend la ligne ; rend le message de l'écran.</summary>
    internal static string Vendre(uint* vendeur, byte* perChar, BoutiqueEcran.Ligne ligne)
    {
        if (!s_Sources.TryGetValue(ligne, out Source? source)) return "Objet introuvable.";
        int bourse = *(int*)(perChar + 0x26c);
        if (bourse + source.Total > BourseMax) return "Ta bourse est pleine.";

        if (!source.DuCoffre)
        {
            byte* e = perChar + 0x300 + source.Emplacement * Pas;
            if (!new ReadOnlySpan<byte>(e, 0x10).SequenceEqual(source.Tete)) return "Cet objet n'est plus la.";
            *e = 0;                                                     // l'emplacement vidé, comme le jeu le vide
        }
        else
        {
            string? erreur = VendreDuCoffre(perChar, source);
            if (erreur != null) return erreur;
        }

        *(int*)(perChar + 0x26c) = bourse + source.Total;
        Jeu.Aud.PlayPositionalSound(0x29, 0x400, 0x1300, (int*)((byte*)vendeur + 0x1c));
        return $"Vendu : {ligne.Nom} (+{source.Total})";
    }

    // ---- le coffre, dans le fichier de sauvegarde -----------------------------------------------

    private static string? Chemin(byte* perChar)
    {
        var nom = new StringBuilder();
        for (int i = 0; i < 0x2c && perChar[0x1fd0 + i] != 0; i++) nom.Append((char)perChar[0x1fd0 + i]);
        return nom.Length == 0 ? null : Path.Combine(AppContext.BaseDirectory, "save", nom.ToString());
    }

    /// <summary>Le fichier du personnage, s'il existe et si son coffre est celui que le jeu lirait.</summary>
    private static byte[]? LireCoffre(byte* perChar)
    {
        try
        {
            string? chemin = Chemin(perChar);
            if (chemin == null || !File.Exists(chemin)) return null;
            byte[] f = File.ReadAllBytes(chemin);
            if (f.Length < Copie + 0x100) return null;
            if (BitConverter.ToInt32(f, Copie + 0xfc) != *(int*)(perChar + Somme)) return null;   // le coffre se croirait vide
            return f;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[VENTE] coffre illisible ({ex.Message})");
            return null;
        }
    }

    private static string? VendreDuCoffre(byte* perChar, Source source)
    {
        string? chemin = Chemin(perChar);
        byte[]? f = LireCoffre(perChar);
        if (chemin == null || f == null) return "Coffre illisible.";

        int o = Coffre + source.Emplacement * Pas;
        if (!f.AsSpan(o, 0x10).SequenceEqual(source.Tete)) return "Cet objet n'est plus la.";

        f[o] = 0;
        int or = BitConverter.ToInt32(f, 0x26c);
        BitConverter.GetBytes(Math.Min(BourseMax, or + source.Total)).CopyTo(f, 0x26c);

        int somme = 0;
        for (int i = 0; i < Somme; i++) somme += (sbyte)f[i];
        BitConverter.GetBytes(somme).CopyTo(f, Somme);
        Array.Copy(f, 0x1f00, f, Copie, 0x100);

        try { File.WriteAllBytes(chemin, f); }
        catch (Exception ex) { return $"Ecriture impossible ({ex.Message})."; }

        *(int*)(perChar + Somme) = somme;                                // le coffre comparera ce mot à 0x3ffc
        return null;
    }

    // ---- le nom ---------------------------------------------------------------------------------

    /// <summary>
    /// Le nom affiché d'un objet, comme BuildItemDisplayName le compose (et `OpenForesia.Tools
    /// bestiaire`, ItemDisplayName) : le nom long de +0x41 jusqu'à la barre oblique ; à défaut, le nom
    /// court de +0x00, privé de son pictogramme de tête. Latin-1 — le nom exact des catalogues.
    /// </summary>
    internal static string Nom(byte[] fiche)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 0x40 && 0x41 + i < fiche.Length; i++)
        {
            byte c = fiche[0x41 + i];
            if (c == 0x2f) return sb.ToString().Trim();
            sb.Append(c >= 0x20 ? (char)c : ' ');
        }
        int debut = fiche[0] > 0x5f && fiche[1] < 0x60 ? 1 : 0;
        var court = new StringBuilder();
        for (int i = debut; i < 0x10 && fiche[i] != 0; i++) court.Append((char)fiche[i]);
        return court.ToString().Trim();
    }
}
