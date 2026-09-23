// OpenForesia.Tools — `extraire-donnees` : ce que le jeu lit encore des fichiers des DLL de 1998,
// transcrit une fois pour toutes, pour que le programme n'ait plus a livrer ces fichiers.
//
// Depuis le 2026-09-23, aucun code de `winsys3d.dll` ni de `wsyskern.dll` ne s'execute : leurs
// corps sont en C# (`OpenForesia/src/Libraries/`). Il restait leurs DONNEES, que l'image memoire de
// `winsys3d` (`Libraries/winsys3d/Image.cs`) reconstruit. Ce verbe les ecrit sous
// `OpenForesia/src/Libraries/` :
//
//     winsys3d/ConstantesRdata.cs    ce que `.rdata` porte hors de la table d'exports      GENERE
//     winsys3d/ValeursInitiales.cs   les valeurs initiales de `.data`                     GENERE
//     winsys3d/Exports.cs            les exports de DONNEES (ordinal, adresse, nom)        GENERE
//     winsys3d/ModeleDialogue.cs     le dialogue 0x66, controle par controle              GENERE
//     wsyskern/Curseur.cs            le curseur 10 de wsyskern, dessine                   GENERE
//     Donnees/winsys3d.TIM-65.bin    la police 8 x 16 (ressource « TIM » 0x65)            binaire
//     Donnees/winsys3d.KANJI-70.bin  la police kanji (ressource « KANJI » 0x70)            binaire
//
// Les fichiers C# sont lisibles mais GENERES : chaque valeur est typee (mot, flottant, double,
// chaine, adresse), et chaque adresse annotee du nom que lui donne `winsys3d/Data.cs`. Les pointeurs
// que le chargeur de Windows relogeait s'ecrivent « adresse de … » : la liste des relocations
// disparait. **Chaque fichier est reencode ici et compare octet par octet a la DLL** : une
// generation qui ne reproduirait pas exactement les octets de 1998 s'arrete en erreur.
//
// Rejouable : `extraire-donnees Legacy/Binaries OpenForesia/src/Libraries`.

using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

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
    private const uint ImageBase = 0x00a00000;

    private const string Entete =
        "// GENERE par `OpenForesia.Tools extraire-donnees` depuis Legacy/Binaries — NE PAS EDITER A LA MAIN.\n" +
        "// Voir OpenForesia.Tools/ExtractionDonnees.cs. Chaque valeur a ete reencodee et comparee octet par\n" +
        "// octet a la DLL de 1998 lors de la generation.\n";

    internal static void Run(string dossierBinaires, string racineLibraries)
    {
        string winsys3d = Path.GetFullPath(Path.Combine(dossierBinaires, "winsys3d.dll"));
        string wsyskern = Path.GetFullPath(Path.Combine(dossierBinaires, "wsyskern.dll"));
        string dataCs = Path.Combine(racineLibraries, "winsys3d", "Data.cs");
        var noms = NomsDeDataCs(dataCs);

        byte[] fichier = File.ReadAllBytes(winsys3d);
        using var pe = new PEReader(new MemoryStream(fichier));
        var rdata = pe.PEHeaders.SectionHeaders.Single(s => s.Name == ".rdata");
        var data = pe.PEHeaders.SectionHeaders.Single(s => s.Name == ".data");
        var exportsDir = pe.PEHeaders.PEHeader!.ExportTableDirectory;
        var relocations = Relocations(pe, fichier);

        // .rdata, hors table d'exports ; .data initialisee.
        var rdataOctets = fichier.AsSpan(rdata.PointerToRawData, rdata.SizeOfRawData).ToArray();
        var dataOctets = fichier.AsSpan(data.PointerToRawData, data.SizeOfRawData).ToArray();
        int exportDebut = exportsDir.RelativeVirtualAddress - rdata.VirtualAddress;
        int exportFin = exportDebut + exportsDir.Size;

        EcrireCs(racineLibraries, "winsys3d/ConstantesRdata.cs", Section(
            "Winsys3dConstantesRdata",
            "Ce que la section `.rdata` de `winsys3d.dll` porte hors de sa table d'exports (celle-ci est dans\n// `Exports.cs`) : les constantes flottantes des tables UV et de la trigonometrie, les noms des\n// commandes CD, et la table de portee SEH de la detection MMX (inerte : elle vise du code).",
            rdataOctets, rdata.VirtualAddress, [(0, exportDebut), (exportFin, rdataOctets.Length)], relocations, noms));

        EcrireCs(racineLibraries, "winsys3d/ValeursInitiales.cs", Section(
            "Winsys3dValeursInitiales",
            "Les valeurs initiales de la section `.data` de `winsys3d.dll` — ses globales qui ont une valeur\n// dans le fichier ; tout le reste (8 Mo) vaut zero. Ce ne sont pas des constantes : le jeu les\n// modifie ensuite. Les dernieres plages sont les donnees de la CRT liee a la DLL (table `_ctype`…).",
            dataOctets, data.VirtualAddress, [(0, dataOctets.Length)], relocations, noms));

        EcrireCs(racineLibraries, "winsys3d/Exports.cs", Exports(pe, fichier, noms));

        IntPtr module = LoadLibraryExW(winsys3d, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
        if (module == IntPtr.Zero) throw new InvalidOperationException($"{winsys3d} : chargement comme donnees impossible.");
        EcrireBinaire(racineLibraries, "Donnees/winsys3d.TIM-65.bin", Ressource(module, FindResourceW(module, (IntPtr)0x65, "TIM")));
        EcrireBinaire(racineLibraries, "Donnees/winsys3d.KANJI-70.bin", Ressource(module, FindResourceW(module, (IntPtr)0x70, "KANJI")));
        EcrireCs(racineLibraries, "winsys3d/ModeleDialogue.cs", Dialogue(Ressource(module, FindResourceW(module, (IntPtr)0x66, (IntPtr)RT_DIALOG))));

        IntPtr kern = LoadLibraryExW(wsyskern, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
        if (kern == IntPtr.Zero) throw new InvalidOperationException($"{wsyskern} : chargement comme donnees impossible.");
        byte[] groupe = Ressource(kern, FindResourceW(kern, (IntPtr)10, (IntPtr)RT_GROUP_CURSOR));
        int image;
        fixed (byte* g = groupe) image = LookupIconIdFromDirectoryEx(g, 0, 0, 0, 0);
        EcrireCs(racineLibraries, "wsyskern/Curseur.cs", Curseur(Ressource(kern, FindResourceW(kern, (IntPtr)image, (IntPtr)RT_CURSOR)), image));
    }

    // =========================================================================================
    // Les sections — `.rdata` et `.data`
    // =========================================================================================

    private enum Genre { Mot, Flottant, Double, Qmot, DemiMots, Octets, Chaine }

    /// <summary>Les types que l'analyse ne peut pas deviner : releves au desassembleur et dans `Data.cs`.</summary>
    private static readonly (uint debut, uint fin, Genre genre)[] Indices =
    [
        (0x00a2a000, 0x00a2a02c, Genre.Flottant),   // les constantes UV du point d'entree
        (0x00a2a06c, 0x00a2a0a8, Genre.Flottant),   // celles de 789
        (0x00a2a0b4, 0x00a2a0c8, Genre.Flottant),
        (0x00a2a0cc, 0x00a2a0d0, Genre.Flottant),
        (0x00a2a0d0, 0x00a2a0d8, Genre.Qmot),       // le mot 3 a 0x1000 de la projection MMX
        (0x00a2a0d8, 0x00a2a0f8, Genre.Double),     // 2pi/4096, 4096, 1/4096, 4096/2pi
        (0x00a2c0f0, 0x00a2c0f8, Genre.Chaine),     // « 0 », « 1 » (cle Small)
        (0x00a2c228, 0x00a2c22c, Genre.Flottant),   // le gamma initial, -1.0
        (0x00a2c388, 0x00a2c3a8, Genre.DemiMots),   // le glyphe par defaut, une diagonale
        (0x00a2c5a8, 0x00a2c5ac, Genre.Octets),     // l'alpha par mode de melange
        (0x00a2c654, 0x00a2c658, Genre.Octets),     // les quatre modes de melange a determiner (0xfe)
        (0x00a2c698, 0x00a2c79c, Genre.DemiMots),   // la table `_ctype` de la CRT
    ];

    private record Element(uint Va, Genre Genre, string Valeur, int Taille, string? Cible = null);

    private static string Section(string classe, string description, byte[] octets, int rvaSection,
                                  (int debut, int fin)[] plages, HashSet<int> relocations, Dictionary<uint, string> noms)
    {
        var elements = new List<Element>();
        foreach (var (debut, fin) in plages)
            Analyser(octets, rvaSection, debut, fin, relocations, elements);

        // La preuve : les elements reencodes redonnent exactement les octets de la section.
        var reconstitue = new byte[octets.Length];
        foreach (var e in elements) Reencoder(e, reconstitue, rvaSection);
        foreach (var (debut, fin) in plages)
            for (int i = debut; i < fin; i++)
                if (reconstitue[i] != octets[i])
                    throw new InvalidOperationException($"{classe} : l'octet 0x{ImageBase + rvaSection + i:x8} ne se reconstitue pas.");

        var chaines = elements.Where(e => e.Genre == Genre.Chaine).ToDictionary(e => e.Va, e => e.Valeur);
        var sb = new StringBuilder();
        sb.Append(Entete).Append("//\n// ").Append(description).Append("\n\n");
        sb.Append("namespace OpenForesia.Libraries;\n\n");
        sb.Append($"internal static class {classe}\n{{\n");
        sb.Append("    internal static void Ecrire(EcrivainImage e)\n    {\n");

        for (int i = 0; i < elements.Count;)
        {
            var e = elements[i];
            if (e.Genre is Genre.Mot)
            {
                // Les mots consecutifs (et les adresses consecutives) se regroupent en une ligne.
                bool adresse = e.Cible != null;
                var groupe = new List<Element> { e };
                while (i + groupe.Count < elements.Count)
                {
                    var s = elements[i + groupe.Count];
                    if (s.Genre != Genre.Mot || (s.Cible != null) != adresse || s.Va != groupe[^1].Va + 4 || groupe.Count == 8) break;
                    groupe.Add(s);
                }
                string methode = adresse ? "Adresses" : "Mots";
                string valeurs = string.Join(", ", groupe.Select(g => adresse ? g.Cible! : g.Valeur));
                sb.Append($"        e.{methode}(0x{e.Va:x8}, {valeurs});");
                var notes = new List<string>();
                foreach (var g in groupe) if (NomA(noms, g.Va, 4) is { } n) notes.Add(n);
                if (adresse)
                    notes.AddRange(groupe.Select(g => chaines.TryGetValue(Convert.ToUInt32(g.Cible, 16), out var c) ? $"-> {c}" : $"-> {g.Cible}"));
                if (notes.Count > 0) sb.Append("   // ").Append(string.Join(" ", notes.Distinct()));
                sb.Append('\n');
                i += groupe.Count;
                continue;
            }

            string ligne = e.Genre switch
            {
                Genre.Flottant => $"e.Flottant(0x{e.Va:x8}, {e.Valeur});",
                Genre.Double => $"e.Double(0x{e.Va:x8}, {e.Valeur});",
                Genre.Qmot => $"e.Qmot(0x{e.Va:x8}, {e.Valeur});",
                Genre.DemiMots => $"e.DemiMots(0x{e.Va:x8}, {e.Valeur});",
                Genre.Octets => $"e.Octets(0x{e.Va:x8}, {e.Valeur});",
                Genre.Chaine => $"e.Chaine(0x{e.Va:x8}, {e.Valeur});",
                _ => throw new InvalidOperationException(),
            };
            sb.Append("        ").Append(ligne);
            string? nom = NomA(noms, e.Va, e.Taille);
            string? note = e.Genre switch
            {
                Genre.Flottant => Commentaire(BitConverter.Int32BitsToSingle(Convert.ToInt32(e.Valeur, 16))),
                Genre.Double => Commentaire(BitConverter.Int64BitsToDouble(Convert.ToInt64(e.Valeur, 16))),
                _ => null,
            };
            var morceaux = new[] { nom, note }.Where(x => x != null).ToArray();
            if (morceaux.Length > 0) sb.Append("   // ").Append(string.Join(" — ", morceaux));
            sb.Append('\n');
            i++;
        }
        sb.Append("    }\n}\n");
        return sb.ToString();
    }

    private static string Commentaire(double v) => v.ToString("R", CultureInfo.InvariantCulture);

    private static void Analyser(byte[] o, int rvaSection, int debut, int fin, HashSet<int> relocations, List<Element> elements)
    {
        for (int p = debut; p < fin;)
        {
            uint va = (uint)(ImageBase + rvaSection + p);
            var indice = Indices.FirstOrDefault(h => va >= h.debut && va < h.fin);
            if (indice != default)
            {
                int taille = (int)(indice.fin - va);
                switch (indice.genre)
                {
                    case Genre.Flottant:
                        elements.Add(new Element(va, Genre.Flottant, $"0x{BitConverter.ToUInt32(o, p):x8}", 4)); p += 4; continue;
                    case Genre.Double:
                        elements.Add(new Element(va, Genre.Double, $"0x{BitConverter.ToUInt64(o, p):x16}", 8)); p += 8; continue;
                    case Genre.Qmot:
                        elements.Add(new Element(va, Genre.Qmot, $"0x{BitConverter.ToUInt64(o, p):x16}", 8)); p += 8; continue;
                    case Genre.DemiMots:
                        elements.Add(new Element(va, Genre.DemiMots,
                            string.Join(", ", Enumerable.Range(0, taille / 2).Select(k => $"0x{BitConverter.ToUInt16(o, p + k * 2):x4}")), taille));
                        p += taille; continue;
                    case Genre.Octets:
                        elements.Add(new Element(va, Genre.Octets,
                            string.Join(", ", Enumerable.Range(0, taille).Select(k => $"0x{o[p + k]:x2}")), taille));
                        p += taille; continue;
                    case Genre.Chaine:
                        int n0 = Array.IndexOf(o, (byte)0, p) - p;
                        elements.Add(new Element(va, Genre.Chaine, Litteral(o, p, n0), n0 + 1));
                        p = Aligner(p + n0 + 1); continue;
                }
            }

            if (relocations.Contains(rvaSection + p))
            {
                uint cible = BitConverter.ToUInt32(o, p);
                elements.Add(new Element(va, Genre.Mot, "", 4, $"0x{cible:x8}"));
                p += 4;
                continue;
            }

            if (p + 4 <= fin && BitConverter.ToUInt32(o, p) == 0) { p += 4; continue; }
            if (p + 4 > fin) { if (o[p] != 0) throw new InvalidOperationException($"octet isole a 0x{va:x8}"); p++; continue; }

            int n = LongueurChaine(o, p, fin);
            if (n >= 2)
            {
                elements.Add(new Element(va, Genre.Chaine, Litteral(o, p, n), n + 1));
                p = Aligner(p + n + 1);
                continue;
            }

            elements.Add(new Element(va, Genre.Mot, $"0x{BitConverter.ToUInt32(o, p):x8}", 4));
            p += 4;
        }
    }

    private static int Aligner(int p) => (p + 3) & ~3;

    /// <summary>Une chaine C : au moins deux caracteres imprimables (ou CR/LF), puis un NUL.</summary>
    private static int LongueurChaine(byte[] o, int p, int fin)
    {
        int n = 0;
        while (p + n < fin && (o[p + n] is >= 0x20 and < 0x7f or 0x0d or 0x0a)) n++;
        return p + n < fin && o[p + n] == 0 ? n : 0;
    }

    private static string Litteral(byte[] o, int p, int n)
    {
        var sb = new StringBuilder("\"");
        for (int i = 0; i < n; i++)
            sb.Append(o[p + i] switch { 0x0d => "\\r", 0x0a => "\\n", (byte)'"' => "\\\"", (byte)'\\' => "\\\\", var c => ((char)c).ToString() });
        return sb.Append('"').ToString();
    }

    private static void Reencoder(Element e, byte[] cible, int rvaSection)
    {
        int p = (int)(e.Va - ImageBase) - rvaSection;
        switch (e.Genre)
        {
            case Genre.Mot:
                BitConverter.TryWriteBytes(cible.AsSpan(p), e.Cible != null ? Convert.ToUInt32(e.Cible, 16) : Convert.ToUInt32(e.Valeur, 16)); break;
            case Genre.Flottant:
                BitConverter.TryWriteBytes(cible.AsSpan(p), Convert.ToUInt32(e.Valeur, 16)); break;
            case Genre.Double: case Genre.Qmot:
                BitConverter.TryWriteBytes(cible.AsSpan(p), Convert.ToUInt64(e.Valeur, 16)); break;
            case Genre.DemiMots:
                var d = e.Valeur.Split(", ");
                for (int k = 0; k < d.Length; k++) BitConverter.TryWriteBytes(cible.AsSpan(p + k * 2), Convert.ToUInt16(d[k], 16));
                break;
            case Genre.Octets:
                var b = e.Valeur.Split(", ");
                for (int k = 0; k < b.Length; k++) cible[p + k] = Convert.ToByte(b[k], 16);
                break;
            case Genre.Chaine:
                string s = System.Text.RegularExpressions.Regex.Unescape(e.Valeur[1..^1]);
                for (int k = 0; k < s.Length; k++) cible[p + k] = (byte)s[k];
                break;
        }
    }

    private static HashSet<int> Relocations(PEReader pe, byte[] fichier)
    {
        var resultat = new HashSet<int>();
        var repertoire = pe.PEHeaders.PEHeader!.BaseRelocationTableDirectory;
        int position = RvaVersFichier(pe, repertoire.RelativeVirtualAddress);
        for (int p = 0; p < repertoire.Size;)
        {
            int page = BitConverter.ToInt32(fichier, position + p), taille = BitConverter.ToInt32(fichier, position + p + 4);
            if (taille == 0) break;
            for (int k = 8; k < taille; k += 2)
            {
                ushort e = BitConverter.ToUInt16(fichier, position + p + k);
                if (e >> 12 == 3) resultat.Add(page + (e & 0xfff));
            }
            p += taille;
        }
        return resultat;
    }

    /// <summary>Les noms que `Data.cs` donne aux adresses : le suffixe `_xxxxxxxx` de ses proprietes.</summary>
    private static Dictionary<uint, string> NomsDeDataCs(string chemin)
    {
        var noms = new Dictionary<uint, string>();
        foreach (Match m in Regex.Matches(File.ReadAllText(chemin), @"internal static [^=;]*?\b(\w+_([0-9a-f]{8}))\s*(=>|\{|\()"))
            noms.TryAdd(Convert.ToUInt32(m.Groups[2].Value, 16), m.Groups[1].Value);
        return noms;
    }

    private static string? NomA(Dictionary<uint, string> noms, uint va, int taille)
    {
        var trouves = noms.Where(n => n.Key >= va && n.Key < va + (uint)taille).Select(n => n.Value).ToList();
        return trouves.Count == 0 ? null : string.Join(", ", trouves);
    }

    // =========================================================================================
    // Les exports de donnees
    // =========================================================================================

    private static string Exports(PEReader pe, byte[] fichier, Dictionary<uint, string> noms)
    {
        var exports = pe.PEHeaders.PEHeader!.ExportTableDirectory;
        int dir = RvaVersFichier(pe, exports.RelativeVirtualAddress);
        int baseOrdinal = BitConverter.ToInt32(fichier, dir + 0x10);
        int nbFonctions = BitConverter.ToInt32(fichier, dir + 0x14), nbNoms = BitConverter.ToInt32(fichier, dir + 0x18);
        int fonctions = RvaVersFichier(pe, BitConverter.ToInt32(fichier, dir + 0x1c));
        int tableNoms = RvaVersFichier(pe, BitConverter.ToInt32(fichier, dir + 0x20));
        int ordinaux = RvaVersFichier(pe, BitConverter.ToInt32(fichier, dir + 0x24));
        var nomParIndice = new Dictionary<int, string>();
        for (int i = 0; i < nbNoms; i++)
        {
            int n = RvaVersFichier(pe, BitConverter.ToInt32(fichier, tableNoms + i * 4));
            nomParIndice[BitConverter.ToUInt16(fichier, ordinaux + i * 2)] = Encoding.ASCII.GetString(fichier, n, Array.IndexOf(fichier, (byte)0, n) - n);
        }
        int rvaDonnees = pe.PEHeaders.SectionHeaders.Single(s => s.Name == ".rdata").VirtualAddress;

        var sb = new StringBuilder(Entete);
        sb.Append("//\n// Les exports de DONNEES de `winsys3d.dll` — ceux que le jeu lit par `GetProcAddress` (voir\n");
        sb.Append("// `Winsys3dImage.Exporte`). Les exports de code n'ont plus d'objet : leurs corps sont en C#.\n\n");
        sb.Append("namespace OpenForesia.Libraries;\n\ninternal static class Winsys3dExports\n{\n");
        sb.Append("    /// <summary>(ordinal, adresse, nom — nul si l'export n'en a pas).</summary>\n");
        sb.Append("    internal static readonly (int Ordinal, uint Adresse, string? Nom)[] Donnees =\n    [\n");
        int compte = 0;
        for (int i = 0; i < nbFonctions; i++)
        {
            int rva = BitConverter.ToInt32(fichier, fonctions + i * 4);
            if (rva < rvaDonnees) continue;
            uint va = (uint)(ImageBase + rva);
            string nom = nomParIndice.TryGetValue(i, out var n) ? $"\"{n}\"" : "null";
            sb.Append($"        ({baseOrdinal + i}, 0x{va:x8}, {nom}),");
            if (NomA(noms, va, 1) is { } dataNom) sb.Append($"   // {dataNom}");
            sb.Append('\n');
            compte++;
        }
        sb.Append("    ];\n}\n");
        Console.WriteLine($"  {compte} exports de donnees");
        return sb.ToString();
    }

    // =========================================================================================
    // Le dialogue 0x66 — un DLGTEMPLATE (pas EX)
    // =========================================================================================

    private static string Dialogue(byte[] d)
    {
        int p = 0;
        uint U32() { uint v = BitConverter.ToUInt32(d, p); p += 4; return v; }
        short I16() { short v = BitConverter.ToInt16(d, p); p += 2; return v; }
        ushort U16() { ushort v = BitConverter.ToUInt16(d, p); p += 2; return v; }
        string Sz() { var sb = new StringBuilder(); for (char c; (c = (char)U16()) != 0;) sb.Append(c); return sb.ToString(); }

        uint style = U32(), ex = U32();
        int nb = U16();
        short x = I16(), y = I16(), cx = I16(), cy = I16();
        if (U16() != 0 || U16() != 0) throw new InvalidOperationException("dialogue : menu ou classe inattendus.");
        string titre = Sz();
        ushort taille = U16();
        string police = Sz();

        var sb = new StringBuilder(Entete);
        sb.Append("//\n// Le modele du dialogue de choix du pilote (ressource RT_DIALOG 0x66 de `winsys3d.dll`), un\n");
        sb.Append("// `DLGTEMPLATE` decrit controle par controle. `Winsys3dImage.ModeleDialogue` l'assemble.\n\n");
        sb.Append("namespace OpenForesia.Libraries;\n\ninternal static class Winsys3dModeleDialogue\n{\n");
        sb.Append("    internal static byte[] Construire() => Winsys3dImage.ModeleDialogue(\n");
        sb.Append($"        style: 0x{style:x8}, styleEtendu: 0x{ex:x8}, x: {x}, y: {y}, largeur: {cx}, hauteur: {cy},\n");
        sb.Append($"        titre: \"{titre}\", taillePolice: {taille}, police: \"{police}\",\n");
        var classes = new Dictionary<ushort, string> { [0x80] = "Bouton", [0x81] = "Saisie", [0x82] = "Libelle", [0x83] = "Liste", [0x84] = "Defilement", [0x85] = "Combo" };
        var controles = new List<(ushort atome, ushort id, string texte, short x, short y, short cx, short cy, uint style, uint ex)>();
        for (int i = 0; i < nb; i++)
        {
            p = Aligner(p);
            uint s = U32(), e = U32();
            short ix = I16(), iy = I16(), icx = I16(), icy = I16();
            ushort id = U16();
            if (U16() != 0xffff) throw new InvalidOperationException("dialogue : classe par nom inattendue.");
            ushort atome = U16();
            if (BitConverter.ToUInt16(d, p) == 0xffff) throw new InvalidOperationException("dialogue : texte par ressource inattendu.");
            string texte = Sz();
            if (U16() != 0) throw new InvalidOperationException("dialogue : donnees de creation inattendues.");
            controles.Add((atome, id, texte, ix, iy, icx, icy, s, e));
        }
        sb.Append(string.Join(",\n", controles.Select(c =>
            $"        Winsys3dImage.Controle(Winsys3dImage.Classe{classes[c.atome]}, id: 0x{c.id:x}, \"{c.texte}\", x: {c.x}, y: {c.y}, " +
            $"largeur: {c.cx}, hauteur: {c.cy}, style: 0x{c.style:x8}, styleEtendu: 0x{c.ex:x8})")));
        sb.Append(");\n}\n");

        // La preuve : le meme assemblage que Winsys3dImage.ModeleDialogue, sur les champs decodes,
        // redonne les octets de la ressource.
        var o = new List<byte>();
        void W32(uint v) => o.AddRange(BitConverter.GetBytes(v));
        void W16(ushort v) => o.AddRange(BitConverter.GetBytes(v));
        void WSz(string s) { foreach (char ch in s) W16(ch); W16(0); }
        W32(style); W32(ex); W16((ushort)controles.Count); W16((ushort)x); W16((ushort)y); W16((ushort)cx); W16((ushort)cy);
        W16(0); W16(0); WSz(titre); W16(taille); WSz(police);
        foreach (var c in controles)
        {
            while (o.Count % 4 != 0) o.Add(0);
            W32(c.style); W32(c.ex); W16((ushort)c.x); W16((ushort)c.y); W16((ushort)c.cx); W16((ushort)c.cy); W16(c.id);
            W16(0xffff); W16(c.atome); WSz(c.texte); W16(0);
        }
        if (!o.ToArray().AsSpan().SequenceEqual(d)) throw new InvalidOperationException("dialogue : le modele ne se reconstitue pas.");
        return sb.ToString();
    }

    // =========================================================================================
    // Le curseur — image RT_CURSOR : point chaud, BITMAPINFOHEADER, palette 2 couleurs, XOR puis AND
    // =========================================================================================

    private static string Curseur(byte[] c, int image)
    {
        int largeur = BitConverter.ToInt32(c, 8), hauteur = BitConverter.ToInt32(c, 12) / 2;
        int pas = ((largeur + 31) / 32) * 4;
        int xor = 4 + 40 + 8, and = xor + pas * hauteur;
        bool Bit(int debut, int x, int yBas) => (c[debut + yBas * pas + x / 8] & (0x80 >> (x % 8))) != 0;

        var lignes = new List<string>();
        for (int y = 0; y < hauteur; y++)
        {
            int yBas = hauteur - 1 - y;                      // la DIB est montante : la premiere ligne est en bas
            var sb = new StringBuilder();
            for (int x = 0; x < largeur; x++)
                sb.Append((Bit(and, x, yBas), Bit(xor, x, yBas)) switch
                {
                    (true, false) => '.',                   // transparent
                    (false, false) => '#',                  // noir
                    (false, true) => 'o',                   // blanc
                    (true, true) => '~',                    // inverse
                });
            lignes.Add(sb.ToString());
        }

        var o = new StringBuilder(Entete);
        o.Append($"//\n// Le curseur 10 de `wsyskern.dll` (l'image {image} de son groupe, celle que `LoadCursor` choisit),\n");
        o.Append("// que la fenetre des films prend pour sa classe. Dessine : « . » transparent, « # » noir,\n");
        o.Append("// « o » blanc, « ~ » inverse ; la premiere ligne est le haut. `Wsyskern.ImageCurseur` l'assemble.\n\n");
        o.Append("namespace OpenForesia.Libraries;\n\ninternal static class WsyskernCurseur\n{\n");
        o.Append($"    internal const int PointChaudX = {BitConverter.ToUInt16(c, 0)}, PointChaudY = {BitConverter.ToUInt16(c, 2)};\n\n");
        o.Append("    internal static readonly string[] Dessin =\n    [\n");
        foreach (var l in lignes) o.Append($"        \"{l}\",\n");
        o.Append("    ];\n}\n");

        // La preuve : le meme assemblage que Wsyskern.ImageCurseur redonne les octets.
        byte[] refait = AssemblerCurseur(lignes.ToArray(), BitConverter.ToUInt16(c, 0), BitConverter.ToUInt16(c, 2));
        if (!refait.AsSpan().SequenceEqual(c)) throw new InvalidOperationException("curseur : l'image ne se reconstitue pas.");
        return o.ToString();
    }

    /// <summary>Le meme algorithme que `Wsyskern.ImageCurseur`, pour la verification.</summary>
    private static byte[] AssemblerCurseur(string[] dessin, ushort hx, ushort hy)
    {
        int largeur = dessin[0].Length, hauteur = dessin.Length, pas = ((largeur + 31) / 32) * 4;
        var o = new byte[4 + 40 + 8 + 2 * pas * hauteur];
        BitConverter.TryWriteBytes(o.AsSpan(0), hx);
        BitConverter.TryWriteBytes(o.AsSpan(2), hy);
        BitConverter.TryWriteBytes(o.AsSpan(4), 40);
        BitConverter.TryWriteBytes(o.AsSpan(8), largeur);
        BitConverter.TryWriteBytes(o.AsSpan(12), hauteur * 2);
        BitConverter.TryWriteBytes(o.AsSpan(16), (ushort)1);
        BitConverter.TryWriteBytes(o.AsSpan(18), (ushort)1);
        BitConverter.TryWriteBytes(o.AsSpan(24), pas * hauteur);   // biSizeImage : le masque XOR
        BitConverter.TryWriteBytes(o.AsSpan(36), 2);               // biClrUsed
        BitConverter.TryWriteBytes(o.AsSpan(40), 2);               // biClrImportant
        o[48] = 0xff; o[49] = 0xff; o[50] = 0xff;           // la palette : noir, blanc
        int xor = 52, and = xor + pas * hauteur;
        for (int y = 0; y < hauteur; y++)
            for (int x = 0; x < largeur; x++)
            {
                char ch = dessin[y][x];
                int yBas = hauteur - 1 - y, masque = 0x80 >> (x % 8);
                if (ch is 'o' or '~') o[xor + yBas * pas + x / 8] |= (byte)masque;
                if (ch is '.' or '~') o[and + yBas * pas + x / 8] |= (byte)masque;
            }
        return o;
    }

    // =========================================================================================

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

    private static void EcrireCs(string racine, string chemin, string texte)
    {
        string complet = Path.Combine(racine, chemin);
        File.WriteAllText(complet, texte.Replace("\n", "\r\n"), new UTF8Encoding(false));
        Console.WriteLine($"{chemin,-34} {texte.Split('\n').Length,6} lignes");
    }

    private static void EcrireBinaire(string racine, string chemin, byte[] octets)
    {
        string complet = Path.Combine(racine, chemin);
        Directory.CreateDirectory(Path.GetDirectoryName(complet)!);
        File.WriteAllBytes(complet, octets);
        Console.WriteLine($"{chemin,-34} {octets.Length,6} octets");
    }
}
