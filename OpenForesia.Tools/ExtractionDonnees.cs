// OpenForesia.Tools — `extraire-donnees` : ce que le jeu lit encore DANS les fichiers des DLL de 1998,
// extrait une fois pour toutes, pour que le programme n'ait plus a livrer ces fichiers.
//
// Depuis le 2026-09-23, aucun code de `winsys3d.dll` ni de `wsyskern.dll` ne s'execute : leurs
// corps sont en C# (`OpenForesia/src/Libraries/`). Il restait leurs DONNEES, que l'image memoire de
// `winsys3d` (`Libraries/winsys3d/Image.cs`) lisait dans le fichier : deux sections, seize
// relocations, la table d'exports, trois ressources ; et un curseur que la fenetre des films
// chargeait depuis `wsyskern.dll`. Ce verbe les ecrit dans un dossier, a embarquer dans
// l'assemblage :
//
//     winsys3d.rdata.bin          `.rdata` brute (RVA 0x2A000)
//     winsys3d.data.bin           la partie initialisee de `.data` (RVA 0x2C000)
//     winsys3d.relocations.txt    les relocations HIGHLOW qui tombent dans ces deux sections
//     winsys3d.exports.txt        la table d'exports : « ordinal rva nom »
//     winsys3d.TIM-65.bin         la police (ressource « TIM » 0x65)
//     winsys3d.KANJI-70.bin       les kanji (ressource « KANJI » 0x70)
//     winsys3d.DIALOG-66.bin      le modele du dialogue de choix du pilote (RT_DIALOG 0x66)
//     wsyskern.CURSOR-10.bin      l'image du curseur que choisit `LoadCursor(wsyskern, 10)`
//
// Rejouable : `extraire-donnees Legacy/Binaries OpenForesia/src/Libraries/Donnees`.

using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

internal static unsafe class ExtractionDonnees
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr LoadLibraryExW(string nom, IntPtr fichier, uint drapeaux);
    [DllImport("kernel32.dll")] private static extern IntPtr FindResourceW(IntPtr module, IntPtr nom, IntPtr type);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr FindResourceW(IntPtr module, IntPtr nom, string type);
    [DllImport("kernel32.dll")] private static extern IntPtr LoadResource(IntPtr module, IntPtr info);
    [DllImport("kernel32.dll")] private static extern void* LockResource(IntPtr donnees);
    [DllImport("kernel32.dll")] private static extern uint SizeofResource(IntPtr module, IntPtr info);
    [DllImport("user32.dll")] private static extern int LookupIconIdFromDirectoryEx(byte* repertoire, int icone, int cx, int cy, uint drapeaux);

    private const uint LOAD_LIBRARY_AS_DATAFILE = 2;
    private const int RT_CURSOR = 1, RT_DIALOG = 5, RT_GROUP_CURSOR = 12;

    internal static void Run(string dossierBinaires, string sortie)
    {
        Directory.CreateDirectory(sortie);
        string winsys3d = Path.GetFullPath(Path.Combine(dossierBinaires, "winsys3d.dll"));
        string wsyskern = Path.GetFullPath(Path.Combine(dossierBinaires, "wsyskern.dll"));

        byte[] fichier = File.ReadAllBytes(winsys3d);
        using var pe = new PEReader(new MemoryStream(fichier));
        var rdata = pe.PEHeaders.SectionHeaders.Single(s => s.Name == ".rdata");
        var data = pe.PEHeaders.SectionHeaders.Single(s => s.Name == ".data");
        Ecrire(sortie, "winsys3d.rdata.bin", fichier.AsSpan(rdata.PointerToRawData, rdata.SizeOfRawData).ToArray(),
            $"RVA 0x{rdata.VirtualAddress:x}");
        Ecrire(sortie, "winsys3d.data.bin", fichier.AsSpan(data.PointerToRawData, data.SizeOfRawData).ToArray(),
            $"RVA 0x{data.VirtualAddress:x}");

        // Les relocations HIGHLOW dans les octets bruts de ces deux sections.
        var relocations = new List<int>();
        var repertoire = pe.PEHeaders.PEHeader!.BaseRelocationTableDirectory;
        int position = RvaVersFichier(pe, repertoire.RelativeVirtualAddress);
        for (int p = 0; p < repertoire.Size;)
        {
            int page = BitConverter.ToInt32(fichier, position + p), taille = BitConverter.ToInt32(fichier, position + p + 4);
            if (taille == 0) break;
            for (int k = 8; k < taille; k += 2)
            {
                ushort e = BitConverter.ToUInt16(fichier, position + p + k);
                int rva = page + (e & 0xfff);
                bool dedans = (rva >= rdata.VirtualAddress && rva < rdata.VirtualAddress + rdata.SizeOfRawData)
                           || (rva >= data.VirtualAddress && rva < data.VirtualAddress + data.SizeOfRawData);
                if (e >> 12 == 3 && dedans) relocations.Add(rva);
            }
            p += taille;
        }
        Ecrire(sortie, "winsys3d.relocations.txt",
            System.Text.Encoding.ASCII.GetBytes(string.Join("\n", relocations.Select(r => $"{r:x}")) + "\n"),
            $"{relocations.Count} relocations");

        // La table d'exports, entiere : « ordinal rva nom » (nom « - » si l'export n'en a pas).
        var exports = pe.PEHeaders.PEHeader!.ExportTableDirectory;
        int dir = RvaVersFichier(pe, exports.RelativeVirtualAddress);
        int baseOrdinal = BitConverter.ToInt32(fichier, dir + 0x10);
        int nbFonctions = BitConverter.ToInt32(fichier, dir + 0x14), nbNoms = BitConverter.ToInt32(fichier, dir + 0x18);
        int fonctions = RvaVersFichier(pe, BitConverter.ToInt32(fichier, dir + 0x1c));
        int noms = RvaVersFichier(pe, BitConverter.ToInt32(fichier, dir + 0x20));
        int ordinaux = RvaVersFichier(pe, BitConverter.ToInt32(fichier, dir + 0x24));
        var nomParIndice = new Dictionary<int, string>();
        for (int i = 0; i < nbNoms; i++)
        {
            int n = RvaVersFichier(pe, BitConverter.ToInt32(fichier, noms + i * 4));
            int fin = Array.IndexOf(fichier, (byte)0, n);
            nomParIndice[BitConverter.ToUInt16(fichier, ordinaux + i * 2)] = System.Text.Encoding.ASCII.GetString(fichier, n, fin - n);
        }
        var lignes = new List<string>();
        for (int i = 0; i < nbFonctions; i++)
        {
            int rva = BitConverter.ToInt32(fichier, fonctions + i * 4);
            if (rva == 0) continue;
            lignes.Add($"{baseOrdinal + i} {rva:x} {(nomParIndice.TryGetValue(i, out var nom) ? nom : "-")}");
        }
        Ecrire(sortie, "winsys3d.exports.txt", System.Text.Encoding.ASCII.GetBytes(string.Join("\n", lignes) + "\n"),
            $"{lignes.Count} exports");

        // Les ressources, lues comme Windows les lit.
        IntPtr module = LoadLibraryExW(winsys3d, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
        if (module == IntPtr.Zero) throw new InvalidOperationException($"{winsys3d} : chargement comme donnees impossible.");
        Ecrire(sortie, "winsys3d.TIM-65.bin", Ressource(module, FindResourceW(module, 0x65, "TIM")), "police");
        Ecrire(sortie, "winsys3d.KANJI-70.bin", Ressource(module, FindResourceW(module, 0x70, "KANJI")), "kanji");
        Ecrire(sortie, "winsys3d.DIALOG-66.bin", Ressource(module, FindResourceW(module, 0x66, RT_DIALOG)), "dialogue");

        IntPtr kern = LoadLibraryExW(wsyskern, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
        if (kern == IntPtr.Zero) throw new InvalidOperationException($"{wsyskern} : chargement comme donnees impossible.");
        byte[] groupe = Ressource(kern, FindResourceW(kern, 10, RT_GROUP_CURSOR));
        int image;
        fixed (byte* g = groupe) image = LookupIconIdFromDirectoryEx(g, 0, 0, 0, 0);
        Ecrire(sortie, "wsyskern.CURSOR-10.bin", Ressource(kern, FindResourceW(kern, image, RT_CURSOR)),
            $"curseur : groupe 10 -> image {image}");
    }

    private static IntPtr FindResourceW(IntPtr module, int nom, int type) => FindResourceW(module, (IntPtr)nom, (IntPtr)type);
    private static IntPtr FindResourceW(IntPtr module, int nom, string type) => FindResourceW(module, (IntPtr)nom, type);

    private static byte[] Ressource(IntPtr module, IntPtr info)
    {
        if (info == IntPtr.Zero) throw new InvalidOperationException("ressource introuvable.");
        uint taille = SizeofResource(module, info);
        return new ReadOnlySpan<byte>(LockResource(LoadResource(module, info)), (int)taille).ToArray();
    }

    private static int RvaVersFichier(PEReader pe, int rva)
    {
        foreach (var s in pe.PEHeaders.SectionHeaders)
            if (rva >= s.VirtualAddress && rva < s.VirtualAddress + Math.Max(s.VirtualSize, s.SizeOfRawData))
                return rva - s.VirtualAddress + s.PointerToRawData;
        throw new InvalidOperationException($"RVA 0x{rva:x} hors sections.");
    }

    private static void Ecrire(string dossier, string nom, byte[] octets, string note)
    {
        File.WriteAllBytes(Path.Combine(dossier, nom), octets);
        Console.WriteLine($"{nom,-28} {octets.Length,8} octets   {note}");
    }
}
