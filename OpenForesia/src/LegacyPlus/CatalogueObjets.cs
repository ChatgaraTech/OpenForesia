// =====================================================================
// LP-008, étape 3b — l'index des objets : nom → fiche (0x140 octets) et vignette (0x140 octets).
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-inn-shop, design D10).
// Chaque carte a son propre catalogue d'objets, et le jeu ne charge que celui de la carte courante :
// un objet d'une autre zone n'a, en carte 82, ni sa fiche ni son icône en mémoire. Cet index les lit
// dans LES FICHIERS DU JEU INSTALLÉS (French\SYSTEM.INI et French\BLAZE.ALL, à côté de l'exécutable),
// par la chaîne que le jeu suit lui-même :
//
//   SYSTEM.INI (racine) +0x48 → état 3 +0x48 → zones +0x48 → sous-zones (répertoires)
//   répertoire de sous-zone +0xb0 → blocs bruts : un compte, puis des décalages → un objet par morceau
//   morceau : fiche 0x140 | palette 16 couleurs (+0x140) | image 24×24 en 4 bits (+0x160)
//
// C'est le lecteur de `OpenForesia.Tools bestiaire` (Bestiaire.cs : Blob, Fixup, ItemCatalog,
// ItemDisplayName), porté tel quel : le nom trouvé ici est EXACTEMENT celui d'objets.csv, donc celui
// de Boutique.ini. Aucune donnée du jeu n'est dans le dépôt ni dans la release.
//
// Construit une fois, au premier accès ; s'arrête dès que tous les noms demandés sont trouvés.
// =====================================================================

using System.Text;

namespace OpenForesia.LegacyPlus;

internal static class CatalogueObjets
{
    /// <summary>Un objet : sa fiche et sa vignette, les 0x280 octets qu'un emplacement d'inventaire recopie.</summary>
    internal sealed record Objet(byte[] Fiche, byte[] Vignette);

    private static Dictionary<string, Objet>? s_Index;

    /// <summary>L'objet de ce nom, ou null s'il n'a pas été trouvé dans les fichiers du jeu.</summary>
    internal static Objet? Trouver(string nom) => s_Index != null && s_Index.TryGetValue(nom, out var o) ? o : null;

    internal static bool Construit => s_Index != null;

    /// <summary>Construit l'index des noms demandés ; rend le nombre trouvé. Une erreur de lecture laisse un index vide.</summary>
    internal static int Construire(IEnumerable<string> noms, StreamWriter? journal)
    {
        s_Index = new Dictionary<string, Objet>();
        var voulus = new HashSet<string>(noms);
        string french = Path.Combine(AppContext.BaseDirectory, "French");
        try
        {
            using var archive = new FileStream(Path.Combine(french, "BLAZE.ALL"), FileMode.Open, FileAccess.Read, FileShare.Read);
            var racine = new Blob(File.ReadAllBytes(Path.Combine(french, "SYSTEM.INI")));
            racine.Fixup();

            var etats = ListeEntrees(racine, 0x48);
            if (etats.Count <= 3) throw new InvalidDataException("SYSTEM.INI n'a pas d'état 3");
            var etat = Charger(archive, racine, etats[3], true);

            foreach (int zone in ListeEntrees(etat, 0x48))
            {
                var blocZone = Charger(archive, etat, zone, true);
                foreach (int sousZone in ListeEntrees(blocZone, 0x48))
                {
                    var repertoire = Charger(archive, blocZone, sousZone, true);
                    LireCatalogue(archive, repertoire, voulus);
                    if (voulus.Count == 0) goto fini;
                }
            }
        fini:
            journal?.WriteLine($"index des objets : {s_Index.Count} trouvés" +
                               (voulus.Count > 0 ? $", introuvables : {string.Join(" | ", voulus)}" : ""));
        }
        catch (Exception ex)
        {
            journal?.WriteLine($"index des objets impossible ({ex.Message}) — pas de fiches ni de vignettes");
        }
        return s_Index.Count;
    }

    /// <summary>Le catalogue d'une sous-zone (Bestiaire.cs ItemCatalog) : on ne garde que les noms voulus.</summary>
    private static void LireCatalogue(FileStream archive, Blob repertoire, HashSet<string> voulus)
    {
        if (!repertoire.In(0xb0, 4)) return;
        int liste = repertoire.I32(0xb0);
        if (liste == 0 || !repertoire.In(liste, 4)) return;

        for (int i = 0; i < 64; i++)
        {
            int at = liste + i * 4;
            if (!repertoire.In(at, 4)) break;
            int entree = repertoire.I32(at);
            if (entree == 0) break;

            var bloc = Charger(archive, repertoire, entree, false);
            if (bloc.Len < 4) continue;
            int compte = bloc.I32(0);
            if (compte <= 0 || compte > 4096) continue;
            int base0 = 4 + compte * 4;

            for (int k = 0; k < compte; k++)
            {
                if (!bloc.In(4 + k * 4, 4)) break;
                int morceau = base0 + bloc.I32(4 + k * 4);
                if (!bloc.In(morceau, 0x280)) continue;

                string nom = NomAffiche(bloc, morceau);
                if (!voulus.Remove(nom)) continue;

                s_Index![nom] = new Objet(bloc.Data[morceau..(morceau + 0x140)], bloc.Data[(morceau + 0x140)..(morceau + 0x280)]);
            }
        }
    }

    /// <summary>Bestiaire.cs ItemDisplayName : le nom long jusqu'à la barre oblique, sinon le nom court.</summary>
    private static string NomAffiche(Blob b, int off)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 0x40; i++)
        {
            if (!b.In(off + 0x41 + i)) break;
            byte c = b.U8(off + 0x41 + i);
            if (c == 0x2f) return sb.ToString().Trim();
            sb.Append(c >= 0x20 ? (char)c : ' ');
        }

        int start = b.In(off + 1) && b.U8(off) > 0x5f && b.U8(off + 1) < 0x60 ? 1 : 0;
        var s = new StringBuilder();
        for (int i = start; i < 0x10; i++)
        {
            if (!b.In(off + i)) break;
            byte c = b.U8(off + i);
            if (c == 0) break;
            s.Append((char)c);
        }
        return s.ToString().Trim();
    }

    /// <summary>Une liste de pointeurs terminée par 0 (Bestiaire.cs EntryList).</summary>
    private static List<int> ListeEntrees(Blob blob, int champ)
    {
        var r = new List<int>();
        if (!blob.In(champ, 4)) return r;
        int liste = blob.I32(champ);
        if (liste == 0 || !blob.In(liste, 4)) return r;
        for (int i = 0; i < 4096; i++)
        {
            int at = liste + i * 4;
            if (!blob.In(at, 4)) break;
            int v = blob.I32(at);
            if (v == 0) break;
            r.Add(v);
        }
        return r;
    }

    /// <summary>Une entrée d'archive : secteur (× 2 Ko) et taille, lus dans le répertoire parent.</summary>
    private static Blob Charger(FileStream archive, Blob repertoire, int entree, bool remettreLesPointeurs)
    {
        if (!repertoire.In(entree, 8)) return new Blob([]);
        long debut = (long)repertoire.I32(entree) << 11;
        int taille = repertoire.I32(entree + 4);
        if (debut < 0 || taille <= 0 || debut + taille > archive.Length) return new Blob([]);

        var octets = new byte[taille];
        archive.Seek(debut, SeekOrigin.Begin);
        archive.ReadExactly(octets);
        var blob = new Blob(octets);
        if (remettreLesPointeurs) blob.Fixup();
        return blob;
    }

    /// <summary>Un blob de ressource à l'adresse virtuelle 0 (Bestiaire.cs Blob).</summary>
    private sealed class Blob(byte[] data)
    {
        // Ghidra: DAT_0056aca8, le schéma de remise en place des pointeurs (Engine/Resources.cs).
        private static readonly ushort[] Schema =
        [
            0x0800, 0x0801, 0x0802, 0x0803, 0x0804, 0x0805, 0x0806, 0x0807,
            0x0408, 0x0409, 0x040a, 0x140b, 0x040e, 0x0410, 0x0411, 0x0414,
            0x0815, 0x0417, 0x0418, 0x0419, 0x041b, 0x041c, 0x081d, 0x041e,
            0x041f, 0x0421, 0x0423, 0x0425, 0x0426, 0x0827, 0x0428, 0x042a,
            0x042b, 0x042d, 0x042f, 0x0430, 0x0431,
        ];

        public readonly byte[] Data = data;
        public int Len => Data.Length;
        public bool In(int off, int size = 1) => off >= 0 && size >= 0 && (long)off + size <= Data.Length;
        public byte U8(int off) => Data[off];
        public int I32(int off) => Data[off] | (Data[off + 1] << 8) | (Data[off + 2] << 16) | (Data[off + 3] << 24);

        private void SetI32(int off, int v)
        {
            Data[off] = (byte)v; Data[off + 1] = (byte)(v >> 8); Data[off + 2] = (byte)(v >> 16); Data[off + 3] = (byte)(v >> 24);
        }

        /// <summary>Ghidra: FUN_00532070, base virtuelle 0.</summary>
        public void Fixup()
        {
            if (Len < 0x100) return;
            int origin = I32(0);
            for (int i = 0; i < 0x3c; i++) SetI32(0x10 + i * 4, I32(0x10 + i * 4) + origin);

            int recordOrigin = I32(4);
            foreach (ushort entry in Schema)
            {
                int stride = entry >> 8;
                int node = I32(0x10 + (entry & 0xff) * 4);
                if (node == 0 || !In(node, 4)) continue;
                int value = I32(node);
                if (value == 0) continue;

                var seen = new HashSet<int>();
                do
                {
                    if (!In(node, 4) || !seen.Add(node)) break;
                    SetI32(node, value + recordOrigin + origin);
                    if (!In(node + stride, 4)) break;
                    value = I32(node + stride);
                    node += stride;
                } while (value != 0);
            }
        }
    }
}
