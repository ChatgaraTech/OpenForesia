using System.Text;

/// <summary>
/// Lecture <b>hors-jeu</b> des donnees d'ennemis du jeu d'origine, dans un dossier installe.
///
/// <para>
/// Rien ici n'est invente : le parcours reproduit celui du jeu, fonction par fonction.
/// <c>SYSTEM.INI</c> est le repertoire racine (<c>LoadFirstLevelResources</c>, FUN_0052a030) ;
/// <c>BLAZE.ALL</c> est l'archive adressee au secteur (<c>LoadResourceEntry</c>, FUN_00529b50 —
/// <c>entree[0] &lt;&lt; 11</c> pour l'offset, <c>entree[1]</c> pour la taille) ; chaque blob passe
/// par la remise en place de ses pointeurs (<c>FixupResourceHeader</c>, FUN_00532070).
/// </para>
/// <para>
/// La chaine a quatre niveaux, relevee dans <c>WorldMap.cs</c> :
/// racine <c>+0x48</c> → etat (3 = la carte du monde) ; etat <c>+0x48</c> → zone
/// (<c>DAT_005de528</c>) ; zone <c>+0x48</c> → sous-zone (<c>DAT_005de52c</c>) ; et la sous-zone
/// <b>est</b> le repertoire que <c>ResolveEntityBlocks</c> interroge.
/// </para>
/// <para>
/// Quatre tables y vivent, et ce sont les quatre demandees :
/// <c>+0x34</c> les formations (opcode 13), <c>+0x4c</c> les especes (8 octets : IA, modele,
/// drapeaux), <c>+0x98</c> les modeles (0x60 octets — la fiche, PV en <c>+0x16</c>), et
/// <c>+0xb4</c> les tables de butin (8 rangs par classe, FUN_0053a920).
/// </para>
/// </summary>
internal static class Bestiaire
{
    // Ghidra: DAT_0056aca8, recopie tel quel depuis Engine/Resources.cs — l'octet bas est un index
    // dans les 60 dwords deja relocalises, l'octet haut le pas de parcours de la liste qu'il ouvre.
    private static readonly ushort[] SchemaFixup =
    [
        0x0800, 0x0801, 0x0802, 0x0803, 0x0804, 0x0805, 0x0806, 0x0807,
        0x0408, 0x0409, 0x040a, 0x140b, 0x040e, 0x0410, 0x0411, 0x0414,
        0x0815, 0x0417, 0x0418, 0x0419, 0x041b, 0x041c, 0x081d, 0x041e,
        0x041f, 0x0421, 0x0423, 0x0425, 0x0426, 0x0827, 0x0428, 0x042a,
        0x042b, 0x042d, 0x042f, 0x0430, 0x0431,
    ];

    /// <summary>
    /// Un blob de ressource charge a l'adresse virtuelle <b>0</b> : un pointeur relocalise devient
    /// donc un simple offset dans le tableau. Les pointeurs d'un blob ne sortent jamais de leur
    /// propre blob, ce qui rend la substitution exacte et non approchee.
    /// </summary>
    private sealed class Blob(byte[] data)
    {
        public readonly byte[] Data = data;

        public int Len => Data.Length;
        public bool In(int off, int size = 1) => off >= 0 && size >= 0 && (long)off + size <= Data.Length;

        public byte U8(int off) => Data[off];
        public sbyte I8(int off) => (sbyte)Data[off];
        public ushort U16(int off) => (ushort)(Data[off] | (Data[off + 1] << 8));
        public short I16(int off) => (short)U16(off);
        public int I32(int off) => Data[off] | (Data[off + 1] << 8) | (Data[off + 2] << 16) | (Data[off + 3] << 24);

        public void SetI32(int off, int v)
        {
            Data[off] = (byte)v;
            Data[off + 1] = (byte)(v >> 8);
            Data[off + 2] = (byte)(v >> 16);
            Data[off + 3] = (byte)(v >> 24);
        }

        /// <summary>Ghidra: FUN_00532070. Base virtuelle 0, donc « adresse » = offset.</summary>
        public void Fixup()
        {
            if (Len < 0x100) return;

            int origin = I32(0);
            for (int i = 0; i < 0x3c; i++)
            {
                int off = 0x10 + i * 4;
                SetI32(off, I32(off) + origin);
            }

            int recordOrigin = I32(4);
            foreach (ushort entry in SchemaFixup)
            {
                int listIndex = entry & 0xff;
                int stride = entry >> 8;

                int node = I32(0x10 + listIndex * 4);
                if (node == 0 || !In(node, 4)) continue;
                int value = I32(node);
                if (value == 0) continue;

                // Lire-puis-avancer, dans l'ordre de l'original : le dword courant est relocalise,
                // puis le maillon suivant est pris a `node + stride`. Inverser change le dword qui
                // termine le parcours. Le garde-fou `seen` n'est pas dans l'original — il protege
                // une lecture hors-jeu contre une liste que le fixup aurait mal enchainee.
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

    /// <summary>
    /// Borne haute d'une table : le plus petit des 60 pointeurs d'en-tete qui lui soit strictement
    /// superieur, ou la fin du blob. Les regions d'un blob se suivent sans trou, donc c'est la
    /// seule borne que la donnee elle-meme fournit — compter jusqu'a un enregistrement nul se
    /// trompe des que la table voisine commence par un zero, ce qui arrive.
    /// </summary>
    private static int RegionEnd(Blob blob, int start)
    {
        int end = blob.Len;
        for (int i = 0; i < 0x3c; i++)
        {
            int p = blob.I32(0x10 + i * 4);
            if (p > start && p < end && blob.In(p)) end = p;
        }
        return end;
    }

    private static Blob LoadEntry(byte[] archive, Blob dir, int entryPtr)
    {
        var blob = LoadEntryRaw(archive, dir, entryPtr);
        blob.Fixup();
        return blob;
    }

    /// <summary>
    /// Le chargement sans remise en place des pointeurs. Tous les blobs n'ont pas d'en-tete de
    /// soixante dwords : les blocs bruts de <c>+0xb0</c> sont un compte suivi d'offsets relatifs, et
    /// <c>UploadRawAreaBlocks</c> ne leur applique jamais <c>FixupResourceHeader</c>.
    /// </summary>
    private static Blob LoadEntryRaw(byte[] archive, Blob dir, int entryPtr)
    {
        if (!dir.In(entryPtr, 8)) return new Blob([]);
        int sector = dir.I32(entryPtr);
        int size = dir.I32(entryPtr + 4);
        long start = (long)sector << 11;
        if (start < 0 || size <= 0 || start + size > archive.Length) return new Blob([]);

        var bytes = new byte[size];
        Array.Copy(archive, start, bytes, 0, size);
        return new Blob(bytes);
    }

    /// <summary>
    /// Le <b>catalogue d'objets de la sous-zone</b>, reconstruit comme <c>UploadRawAreaBlocks</c>
    /// (FUN_005355f0) le fait : la liste <c>areaDir + 0xb0</c> donne des entrees d'archive ; chaque
    /// blob chargé est un compte suivi de <c>compte</c> offsets relatifs ; et chaque morceau ainsi
    /// designe commence par les <b>0x140 octets de la fiche d'objet</b> (la palette et l'icone
    /// 24x24 suivent en <c>+0x140</c> et <c>+0x160</c>, et partent en VRAM).
    ///
    /// <para>
    /// L'indice est <b>cumulatif sur toute la liste</b> : le curseur de destination continue d'un
    /// blob au suivant. C'est cet indice que <c>FUN_00530f20</c> recoit, et donc celui que la table
    /// de butin contient.
    /// </para>
    /// </summary>
    /// <param name="Icon">Les <c>0x140</c> octets qui suivent la fiche : la palette (<c>+0x140</c>,
    /// seize couleurs) et la vignette 24x24 en 4 bits (<c>+0x160</c>) — exactement ce que
    /// <c>StoreItemInInventory_005312D0</c> recapture de la VRAM dans l'emplacement d'inventaire.
    /// Vide si le morceau s'arrete avant.</param>
    internal readonly record struct Item(string Name, string Description, string Hex, string Icon = "");

    private static List<Item> ItemCatalog(byte[] archive, Blob areaDir)
    {
        var items = new List<Item>();
        if (!areaDir.In(0xb0, 4)) return items;

        int list = areaDir.I32(0xb0);
        if (list == 0 || !areaDir.In(list, 4)) return items;

        for (int i = 0; i < 64; i++)
        {
            int at = list + i * 4;
            if (!areaDir.In(at, 4)) break;
            int entryPtr = areaDir.I32(at);
            if (entryPtr == 0) break;

            var blob = LoadEntryRaw(archive, areaDir, entryPtr);
            if (blob.Len < 4) continue;

            int count = blob.I32(0);
            if (count <= 0 || count > 4096) continue;
            int baseAddress = 4 + count * 4;

            for (int k = 0; k < count; k++)
            {
                if (!blob.In(4 + k * 4, 4)) break;
                int chunk = baseAddress + blob.I32(4 + k * 4);
                if (!blob.In(chunk, 0x140)) { items.Add(new Item("(hors blob)", "", "")); continue; }

                items.Add(new Item(
                    ItemDisplayName(blob, chunk),
                    ItemDescription(blob, chunk),
                    string.Join("", Enumerable.Range(0, 0x140).Select(b => blob.U8(chunk + b).ToString("x2"))),
                    blob.In(chunk + 0x140, 0x140)
                        ? string.Join("", Enumerable.Range(0x140, 0x140).Select(b => blob.U8(chunk + b).ToString("x2")))
                        : ""));
            }
        }
        return items;
    }

    /// <summary>Parcourt une liste de pointeurs terminee par 0, comme le fait le jeu.</summary>
    private static List<int> EntryList(Blob blob, int listPtrField)
    {
        var result = new List<int>();
        if (!blob.In(listPtrField, 4)) return result;

        int list = blob.I32(listPtrField);
        if (list == 0 || !blob.In(list, 4)) return result;

        for (int i = 0; i < 4096; i++)
        {
            int at = list + i * 4;
            if (!blob.In(at, 4)) break;
            int v = blob.I32(at);
            if (v == 0) break;
            result.Add(v);
        }
        return result;
    }

    public static void Run(string dossierDonnees, int? onlyZone, string? outDir, bool regions)
    {
        string french = Path.Combine(dossierDonnees, "French");
        if (!Directory.Exists(french)) french = dossierDonnees;

        string systemIni = Path.Combine(french, "SYSTEM.INI");
        string archivePath = Path.Combine(french, "BLAZE.ALL");

        if (!File.Exists(systemIni) || !File.Exists(archivePath))
        {
            Console.WriteLine($"Introuvable : {systemIni} et/ou {archivePath}");
            Console.WriteLine("Donner le dossier des donnees du jeu (celui qui contient French\\) ou le repertoire French lui-meme.");
            return;
        }

        byte[] archive = File.ReadAllBytes(archivePath);
        var root = new Blob(File.ReadAllBytes(systemIni));
        root.Fixup();

        // Ghidra: RunGameLoop — perStateTable1 = *(int**)(racine + 0x48), indexe par DAT_005de510.
        var states = EntryList(root, 0x48);
        Console.WriteLine($"SYSTEM.INI : {root.Len} octets, {states.Count} etat(s).");
        if (states.Count <= 3)
        {
            Console.WriteLine("La table des etats n'a pas l'etat 3 (la carte du monde) — parcours interrompu.");
            return;
        }

        // Etat 3 = la carte du monde (LoadWorldMapResources, FUN_005160e0).
        var stateDir = LoadEntry(archive, root, states[3]);
        var zones = EntryList(stateDir, 0x48);
        Console.WriteLine($"Etat 3 : repertoire de {stateDir.Len} octets, {zones.Count} zone(s).");
        Console.WriteLine();

        var report = new StringBuilder();

        // Les fiches d'objets : FUN_0053a980 les adresse par
        // `*(int*)(*(int*)(repertoire_etat + 0x9c) + liste * 4) + index * 0x30`. C'est cette table
        // que le rang tire par FUN_0053a920 designe — le butin nomme, donc.
        if (regions) DumpItemLists(stateDir, report);

        // Les fiches de SORT vivent dans la meme table `+0x9c`, et c'est CastSpell_0053A980 qui les
        // adresse : `*(int*)(*(int*)(etat + 0x9c) + famille * 4) + indice * 0x30`. Elles ne
        // dependent ni de la zone ni de la sous-zone — le repertoire est celui de l'etat 3 —, donc
        // le releve se fait une fois, ici, avant la boucle des zones.
        var spellCsv = new StringBuilder(
            "famille,indice,nom,sous_type_0x10,param_0x11,param_0x12,duree_0x13,cout_pm_0x14,"
            + "cout_apprentissage_0x1d,combinaison_len_0x1f,combinaison,octets\n");
        DumpSpellSheets(stateDir, report, spellCsv);

        var speciesCsv = new StringBuilder("zone,sous_zone,espece,ia,modele,classe_butin,drapeaux,octets\n");
        var modelCsv = new StringBuilder(
            "zone,sous_zone,modele,nom,rang_0x10,pv_0x14,pm_0x16,champ_0x12,champ_0x18,champ_0x1a,"
            + "butin_0x22,c0,c1,c2,c3,c4,c5,c6,c7,octets\n");
        // param_0x14 / param_0x16 : le sixieme dword de l'enregistrement (+0x18), que
        // ScriptOpSpawnFormation recopie en +0x14 / +0x16 de l'entite (WorldMapScript.cs). Beaucoup
        // de betes y lisent leur VARIANTE — la Momie choisit ainsi son effet d'etat, le Crotale s'il
        // peut empoisonner.
        var formationCsv = new StringBuilder("zone,sous_zone,formation,rang,espece,x,y,z,orientation,param_0x14,param_0x16\n");
        var lootCsv = new StringBuilder("zone,sous_zone,classe,rang,objet_id,objet,pourcent_plage_100\n");
        var itemCsv = new StringBuilder(
            "zone,sous_zone,objet_id,nom,description,att_0x30,def_0x32,elements_0x12,"
            + "bonus_0x20,bonus_0x18,bonus_0x8a,octets\n");
        // Les vignettes a part : census-item-effects.sh lit `octets` comme DERNIER champ d'objets.csv.
        var iconCsv = new StringBuilder("zone,sous_zone,objet_id,vignette\n");
        var dropCsv = new StringBuilder(
            "objet,zone,sous_zone,monstre,classe,rang,pourcent_plage_100,seuil_0x22,placements\n");
        var placeCsv = new StringBuilder("zone,sous_zone,nom\n");
        var chestCsv = new StringBuilder(
            "zone,sous_zone,liste,index,sous_type,gestionnaire,famille,coffre_id,x,y,z,orientation,role,"
            + "libelle_0x14,rangee_0x16,contenu_0x48,memoire_0x44,rang,objet_id,objet,"
            + "pourcent_plage_100\n");

        for (int z = 0; z < zones.Count; z++)
        {
            if (onlyZone is int only && z != only) continue;

            var zoneBlob = LoadEntry(archive, stateDir, zones[z]);
            if (zoneBlob.Len == 0) continue;
            var subs = EntryList(zoneBlob, 0x48);

            report.AppendLine($"## Zone {z} — {zoneBlob.Len} octets, {subs.Count} sous-zone(s)");
            report.AppendLine();

            for (int s = 0; s < subs.Count; s++)
            {
                var areaDir = LoadEntry(archive, zoneBlob, subs[s]);
                if (areaDir.Len == 0) continue;
                DumpSubArea(z, s, areaDir, archive, regions, report, speciesCsv, modelCsv, formationCsv, lootCsv, itemCsv, dropCsv, placeCsv, chestCsv, iconCsv);
            }
        }

        Console.Write(report.ToString());

        if (outDir is not null)
        {
            Directory.CreateDirectory(outDir);
            File.WriteAllText(Path.Combine(outDir, "bestiaire.md"), report.ToString());
            File.WriteAllText(Path.Combine(outDir, "especes.csv"), speciesCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "modeles.csv"), modelCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "formations.csv"), formationCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "butin.csv"), lootCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "objets.csv"), itemCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "butin_par_objet.csv"), dropCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "lieux.csv"), placeCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "coffres.csv"), chestCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "decor.csv"), s_decorCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "sorts.csv"), spellCsv.ToString());
            File.WriteAllText(Path.Combine(outDir, "vignettes.csv"), iconCsv.ToString());
            Console.WriteLine();
            Console.WriteLine($"Ecrit dans {outDir} : bestiaire.md, especes.csv, modeles.csv, "
                              + "formations.csv, butin.csv, objets.csv, butin_par_objet.csv, lieux.csv, "
                              + "coffres.csv, sorts.csv, vignettes.csv");
        }
    }

    /// <summary>
    /// Le verbe <c>terrain</c> : les faces des morceaux d'une sous-zone, telles que
    /// <c>TryPlaceEntity</c> (FUN_005330d0) les lit — seize octets par face, trois ou quatre index de
    /// sommet, un index de matiere, les attributs dans le quatrieme dword (octet <c>+0xe</c> : le
    /// genre). Un sommet fait huit octets : x et y dans le dword de tete, z au short de <c>+4</c>.
    /// Les champs du morceau sont des decalages relatifs a son debut (RelocateModelEntry,
    /// FUN_00532450) : <c>+0x24</c> les matieres, <c>+0x28</c> les faces, <c>+4</c> leur nombre,
    /// les sommets a <c>+0x40</c>. Les morceaux viennent des quatre listes <c>+0x14..+0x20</c> du
    /// repertoire de sous-zone, dans l'ordre ou <c>LoadWorldMapSubArea</c> les empile.
    /// </summary>
    public static void Terrain(string dossierDonnees, int zone, int sousZone)
    {
        string french = Path.Combine(dossierDonnees, "French");
        if (!Directory.Exists(french)) french = dossierDonnees;
        byte[] archive = File.ReadAllBytes(Path.Combine(french, "BLAZE.ALL"));
        var root = new Blob(File.ReadAllBytes(Path.Combine(french, "SYSTEM.INI")));
        root.Fixup();

        var stateDir = LoadEntry(archive, root, EntryList(root, 0x48)[3]);
        var zoneBlob = LoadEntry(archive, stateDir, EntryList(stateDir, 0x48)[zone]);
        var subs = EntryList(zoneBlob, 0x48);
        var a = LoadEntry(archive, zoneBlob, subs[sousZone]);

        Console.WriteLine("liste,morceau,face,sommets,genre,attributs,sol,xmin,xmax,ymin,ymax,zmin,zmax");
        int global = 0;
        for (int list = 0; list < 4; list++)
        {
            int entries = a.I32(0x14 + list * 4);
            for (int k = 0; a.In(entries + k * 8, 8) && a.I32(entries + k * 8) != 0; k++, global++)
            {
                // Ghidra : `LoadResourceEntry((int*)*entry, cursor)` — le dword est un POINTEUR vers
                // l'entree d'archive ; le second est le descripteur passe a RelocateModelEntry.
                var c = LoadEntryRaw(archive, a, a.I32(entries + k * 8));
                if (c.Len < 0x40) continue;
                int count = c.U16(4);
                int faces = c.I32(0x28), materials = c.I32(0x24);
                for (int f = 0; f < count; f++)
                {
                    int fo = faces + f * 16;
                    if (!c.In(fo, 16)) break;
                    uint w0 = (uint)c.I32(fo), w1 = (uint)c.I32(fo + 4);
                    uint attr = (uint)c.I32(fo + 12);
                    int mat = c.U16(fo + 8);
                    bool sol = c.In(materials + 2 + mat * 8, 2) && c.I16(materials + 2 + mat * 8) < 0;
                    var idx = new List<int> { (int)(w0 & 0xffff), (int)(w0 >> 16), (int)(w1 & 0xffff) };
                    if ((attr & 1) != 0) idx.Add((int)(w1 >> 16));
                    int x0 = int.MaxValue, x1 = int.MinValue, y0 = int.MaxValue, y1 = int.MinValue, z0 = int.MaxValue, z1 = int.MinValue;
                    foreach (int v in idx)
                    {
                        int vo = 0x40 + v * 8;
                        if (!c.In(vo, 8)) continue;
                        int x = c.I16(vo), y = c.I16(vo + 2), z = c.I16(vo + 4);
                        x0 = Math.Min(x0, x); x1 = Math.Max(x1, x);
                        y0 = Math.Min(y0, y); y1 = Math.Max(y1, y);
                        z0 = Math.Min(z0, z); z1 = Math.Max(z1, z);
                    }
                    Console.WriteLine($"{list},{global},{f},{idx.Count},{c.U8(fo + 14)},0x{attr & 0xffff:x4},{(sol ? 1 : 0)},{x0},{x1},{y0},{y1},{z0},{z1}");
                }
            }
        }
    }

    // Les seuils de FUN_0053a920 : rand() % plage, compare a 0x18, 0x29, 0x38, 0x45, 0x50, 0x59,
    // 0x5f. Huit rangs, donc, et ce sont ces bornes qui donnent les pourcentages.
    // =============================================================================================
    // Les gestionnaires d'entite de la sous-zone, lus dans Blaze.exe
    // =============================================================================================
    //
    // **Un numero de sous-type ne veut rien dire tout seul.** La passe FUN_0050e5e0 appelle
    // `(&PTR_..._0056adc8)[entite[5]]`, et cette table de 0x40 entrees est RECHARGEE a chaque
    // sous-zone : la branche DAT_00600904 de FUN_0052a660 choisit sept blocs dans sept tables de
    // pointeurs contigues, indexees par `zone * 0x10 + sous-zone`. Le sous-type 13 est un coffre
    // dans une sous-zone et tout autre chose dans la suivante.
    //
    // Ces tables vivent en `.data`, donc pas dans `Blaze.c` — elles se lisent dans le binaire.
    // C'est le meme releve que `OpenForesia.Tools/dump-entity-vtables.sh` fait pour
    // `EntityVTables.cs` ; ici il est refait a la volee plutot que de dupliquer le fichier genere.
    private const string BlazeExePath = "Legacy/Binaries/Blaze.exe";
    private const uint VtSelectorTable_00567058 = 0x00567058;   // -> s_Vt_0056adc8, 0x40 entrees

    /// <summary>Les trois gestionnaires qui remettent un objet au groupe : le coffre (sous-type 13
    /// de la sous-zone observee, <c>FUN_00473ed0</c>) et ses deux quasi-jumeaux
    /// <c>FUN_004502e0</c> et <c>FUN_00450d30</c>. Meme automate, memes champs — voir
    /// <c>Screens/State03/WorldMapTick.cs</c>.</summary>
    private static readonly Dictionary<uint, string> ChestHandlers = new()
    {
        [0x00473ED0] = "coffre",
        [0x004502E0] = "coffre-17",
        [0x00450D30] = "coffre-18",
    };

    private static byte[]? s_exe;
    private static System.Reflection.PortableExecutable.PEHeaders? s_exeHeaders;
    private static readonly Dictionary<int, uint[]?> s_vtCache = [];

    private static bool LoadBlazeExe()
    {
        if (s_exe is not null) return true;
        if (!File.Exists(BlazeExePath)) return false;

        s_exe = File.ReadAllBytes(BlazeExePath);
        using var stream = new MemoryStream(s_exe, writable: false);
        s_exeHeaders = new System.Reflection.PortableExecutable.PEHeaders(stream);
        return true;
    }

    /// <summary>Adresse virtuelle Ghidra vers offset de fichier, par les en-tetes de section.
    /// Rend -1 quand l'adresse tombe dans la queue non initialisee d'une section, ou hors image —
    /// c'est une reponse, pas une erreur.</summary>
    private static int ExeOffset(uint va)
    {
        if (s_exeHeaders is null) return -1;
        int rva = (int)(va - 0x00400000);

        foreach (var section in s_exeHeaders.SectionHeaders)
        {
            int start = section.VirtualAddress;
            int size = Math.Max(section.VirtualSize, section.SizeOfRawData);
            if (rva < start || rva >= start + size) continue;

            int inSection = rva - start;
            if (inSection >= section.SizeOfRawData) return -1;   // queue non initialisee
            return section.PointerToRawData + inSection;
        }
        return -1;
    }

    private static uint ExeU32(uint va)
    {
        int off = ExeOffset(va);
        if (off < 0 || s_exe is null || off + 4 > s_exe.Length) return 0;
        return (uint)(s_exe[off] | (s_exe[off + 1] << 8) | (s_exe[off + 2] << 16) | (s_exe[off + 3] << 24));
    }

    /// <summary>Les 0x40 gestionnaires que la sous-zone installe sur <c>0x0056adc8</c>. Null quand
    /// le binaire n'est pas la, ou quand la sous-zone renvoie au bloc d'arret fatal.</summary>
    private static uint[]? SubAreaEntityHandlers(int zone, int sub)
    {
        int index = zone == 0 ? 0 : zone * 0x10 + sub;
        if (s_vtCache.TryGetValue(index, out uint[]? cached)) return cached;

        uint[]? table = null;
        if (LoadBlazeExe() && sub <= 0xf && index >= 0 && index < 176)
        {
            uint block = ExeU32(VtSelectorTable_00567058 + (uint)index * 4);
            if (block != 0)
            {
                var entries = new uint[0x40];
                for (int i = 0; i < 0x40; i++) entries[i] = ExeU32(block + (uint)i * 4);
                table = entries;
            }
        }

        s_vtCache[index] = table;
        return table;
    }

    /// <summary>Les sept sélecteurs contigus (<c>0x00566d98</c>, 176 pointeurs chacun), un par table
    /// installée — 0x0056ad48 (0x20 entrées) puis 0x0056adc8 .. 0x0056b2c8 (0x40 chacune).</summary>
    private const uint VtSelectors_00566D98 = 0x00566d98;
    private const uint FatalBlock_0056B3C8 = 0x0056b3c8;

    private static readonly Dictionary<int, uint[]?> s_allVtCache = [];

    /// <summary>
    /// Les sept tables de la sous-zone <b>mises bout à bout</b>, dans l'ordre où elles se suivent en
    /// mémoire : <c>0x20 + 6 * 0x40</c> entrées. Un sous-type de décor indexe à partir de
    /// <c>0x0056adc8</c>, soit l'entrée <c>0x20 + sous-type</c> ici ; au-delà de 0x3f il tombe dans
    /// la table suivante, comme dans le jeu. Même relevé que <c>census-entity-handlers.sh</c>.
    /// </summary>
    private static uint[]? SubAreaAllHandlers(int zone, int sub)
    {
        int index = zone == 0 ? 0 : zone * 0x10 + sub;   // meme regle que SubAreaEntityHandlers
        if (s_allVtCache.TryGetValue(index, out uint[]? cached)) return cached;

        uint[]? all = null;
        if (LoadBlazeExe() && sub <= 0xf && index >= 0 && index < 176)
        {
            all = new uint[0x20 + 6 * 0x40];
            int at = 0;
            for (int col = 0; col < 7 && all is not null; col++)
            {
                int size = col == 0 ? 0x20 : 0x40;
                uint block = ExeU32(VtSelectors_00566D98 + (uint)(col * 176 + index) * 4);
                if (block == 0 || block == FatalBlock_0056B3C8) { all = null; break; }
                for (int i = 0; i < size; i++) all[at + i] = ExeU32(block + (uint)i * 4);
                at += size;
            }
        }

        s_allVtCache[index] = all;
        return all;
    }

    private static readonly StringBuilder s_decorCsv =
        new("zone,sous_zone,liste,index,sous_type,gestionnaire,x,y,z\n");

    private static readonly int[] LootThresholds = [0x18, 0x29, 0x38, 0x45, 0x50, 0x59, 0x5f];

    /// <summary>
    /// Le nom d'une fiche d'objet, exactement comme <c>BuildItemDisplayName</c>
    /// (<c>Screens/State06/Inventory.cs</c>) le compose.
    ///
    /// <para>
    /// <b>Le nom long est le vrai nom</b> : il vit en <c>+0x41</c> et se termine par un
    /// <c>'/'</c>, pas par un NUL. C'est celui que le jeu affiche ; le nom court de <c>+0x00</c>
    /// n'est qu'un repli, tronque a seize octets, quand les 0x40 octets defilent sans rencontrer
    /// la barre oblique.
    /// </para>
    /// <para>
    /// <b>Et l'octet de tete du nom court n'est pas une lettre.</b> Quand il depasse <c>0x5f</c>
    /// alors que l'octet 1 reste en dessous, l'original l'emet comme la sequence de controle
    /// <c>0x24 &lt;octet&gt;</c> — un pictogramme — et le nom proprement dit commence en
    /// <c>+1</c>. C'est ce qui explique les quatre valeurs relevees sur 1 618 fiches : <c>w</c>,
    /// <c>m</c> et <c>s</c> sont des pictogrammes, tandis que le <c>L</c> de « La devoreuse » est
    /// bien une lettre — il ne depasse pas <c>0x5f</c>, donc la regle ne s'y applique pas.
    /// </para>
    /// </summary>
    private static string ItemDisplayName(Blob b, int off)
    {
        // Le nom long, jusqu'a la barre oblique.
        var sb = new StringBuilder();
        for (int i = 0; i < 0x40; i++)
        {
            if (!b.In(off + 0x41 + i)) break;
            byte c = b.U8(off + 0x41 + i);
            if (c == 0x2f) return sb.ToString().Trim();      // le '/' terminal
            sb.Append(c >= 0x20 ? (char)c : ' ');
        }

        // Le repli : le nom court, prive de son pictogramme de tete s'il en porte un.
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

    /// <summary>
    /// <b>La description de l'objet</b> : ce qui suit la barre oblique du champ <c>+0x41</c>,
    /// jusqu'au NUL.
    ///
    /// <para>
    /// Le champ ne porte pas deux chaines mais une seule, <c>« nom/description »</c> — d'ou le nom
    /// que <c>FUN_0051bd50</c> porte deja dans la reconstruction, « le nom d'objet, <b>coupe a la
    /// barre oblique</b> ». Ce que cette fonction jette, c'est exactement la description.
    /// </para>
    /// </summary>
    private static string ItemDescription(Blob b, int off)
    {
        // La barre oblique est facultative. Quand elle est la, le champ est « nom/description » et
        // seule la seconde moitie est la description. Quand elle manque, le champ **entier** est la
        // description, et le nom retombe sur le nom court — c'est ce que fait
        // `BuildItemDisplayName`, qui reprend le nom court des que les 0x40 octets defilent sans
        // rencontrer de barre. « Potion med » en est le cas type : son champ dit « Petite Potion »,
        // et le jeu affiche bien « Potion med » en nom et « Petite Potion » en description.
        int slash = -1;
        for (int i = 0; i < 0x40; i++)
        {
            if (!b.In(off + 0x41 + i)) return "";
            byte c = b.U8(off + 0x41 + i);
            if (c == 0) break;
            if (c == 0x2f) { slash = i; break; }
        }

        var sb = new StringBuilder();
        for (int k = off + 0x41 + slash + 1; k < off + 0x140 && b.In(k); k++)
        {
            byte c = b.U8(k);
            if (c == 0) break;
            sb.Append(c >= 0x20 ? (char)c : ' ');
        }
        return sb.ToString().Trim();
    }

    // Relecture d'une chaine hexa deja produite : les fiches d'objets sont conservees sous cette
    // forme, et les bonus s'en relisent sans reouvrir le blob.
    private static byte HexU8(string hex, int off) =>
        Convert.ToByte(hex.Substring(off * 2, 2), 16);

    private static short HexI16(string hex, int off) =>
        (short)(HexU8(hex, off) | (HexU8(hex, off + 1) << 8));

    /// <summary>
    /// Les <b>noms de lieu</b> que le repertoire d'une sous-zone contient. Le jeu les ecrit dans
    /// ses textes sous la forme <c>« - Le labyrinthe des morts - »</c>, suivie du sous-niveau
    /// (<c>« - 1er sous-sol - »</c>). Les relever ici plutot que de les saisir a la main donne
    /// l'orthographe du jeu et, surtout, <b>l'association a l'indice de zone</b> — la seule qu'on
    /// ne puisse pas deviner de l'exterieur.
    ///
    /// <para>
    /// Ce n'est pas une structure declaree : c'est une reconnaissance de motif sur les octets du
    /// blob. Le releve est donc une <b>indication</b>, pas une lecture de table, et les entrees
    /// communes a plusieurs zones (« Le coffre », « La reception ») sont ecartees par l'appelant.
    /// </para>
    /// </summary>
    private static List<string> PlaceNames(Blob a)
    {
        var found = new List<string>();
        var sb = new StringBuilder();

        for (int i = 0; i + 3 < a.Len; i++)
        {
            // Le motif d'ouverture : espace, tiret, espace, puis une majuscule.
            if (a.U8(i) != ' ' || a.U8(i + 1) != '-' || a.U8(i + 2) != ' ') continue;
            byte first = a.U8(i + 3);
            if (first is not (>= (byte)'A' and <= (byte)'Z') && first < 0xc0) continue;

            sb.Clear();
            for (int k = i + 3; k < a.Len && sb.Length < 48; k++)
            {
                byte c = a.U8(k);
                if (c == 0) { sb.Clear(); break; }
                if (c == ' ' && k + 1 < a.Len && a.U8(k + 1) == '-') break;   // le tiret fermant
                sb.Append((char)c);
            }

            string name = sb.ToString().Trim();
            if (name.Length >= 4 && name.Contains(' ')) found.Add(name);
        }
        return found;
    }

    /// <summary>Le nom du catalogue, ou une marque explicite quand l'indice n'y tombe pas.</summary>
    private static string ItemName(List<Item> items, int id) =>
        id >= 0 && id < items.Count ? items[id].Name : "";

    private static string ItemLabel(List<Item> items, int id)
    {
        string name = ItemName(items, id);
        return name.Length > 0 ? $"{name} `{id}`" : $"`{id}` *(hors catalogue)*";
    }

    private static double[] LootTierPercent(int range)
    {
        var pct = new double[8];
        for (int r = 0; r < range; r++)
        {
            int tier = 0;
            foreach (int t in LootThresholds)
            {
                if (r > t) tier++;
            }
            pct[tier] += 100.0 / range;
        }
        return pct;
    }

    private static void DumpSubArea(
        int z, int s, Blob a, byte[] archive, bool regions,
        StringBuilder report, StringBuilder speciesCsv, StringBuilder modelCsv,
        StringBuilder formationCsv, StringBuilder lootCsv, StringBuilder itemCsv, StringBuilder dropCsv,
        StringBuilder placeCsv, StringBuilder chestCsv, StringBuilder iconCsv)
    {
        // Le catalogue d'objets de la sous-zone, resolu avant tout le reste : c'est lui qui donne
        // un nom aux valeurs de la table de butin.
        var spawns = new Dictionary<int, int>();   // espece -> nombre de placements dans la sous-zone
        var items = ItemCatalog(archive, a);
        for (int i = 0; i < items.Count; i++)
        {
            // Les champs de la fiche, aux offsets que **l'ecran d'objet du jeu** lit — pas a ceux
            // qu'on aurait devines. `DrawItemStatRow` (Screens/State06/Inventory.cs) dessine, dans
            // cet ordre :
            //   - huit octets SIGNES a `+0x18`, sur deux rangees de quatre, dans l'ordre
            //     s_StatSlotOrder_00566ad0 = [2,1,0,3,4,5,6,7] ;
            //   - puis **deux valeurs longues** a `+0x30` et `+0x32` : ce sont l'ATT et la DEF.
            // Et `DrawItemElementRow` allume huit temoins depuis les bits de `+0x12`.
            //
            // ApplyEquipmentStatBonuses (FUN_00538e20) relit les memes octets autrement : seize
            // shorts a `+0x20` ajoutes aux statistiques du personnage — l'ATT et la DEF sont donc
            // les indices 8 et 9 de ce bloc — les huit octets de `+0x18` ajoutes a `+0x210` pour
            // les deux derniers emplacements d'equipement seulement, et le short de `+0x8a`.
            string hex = items[i].Hex;
            string bonus16 = hex.Length >= 0x40 * 2
                ? string.Join(";", Enumerable.Range(0, 16).Select(k => HexI16(hex, 0x20 + k * 2)))
                : "";
            string bonus8 = hex.Length >= 0x20 * 2
                ? string.Join(";", Enumerable.Range(0, 8).Select(k => (sbyte)HexU8(hex, 0x18 + k)))
                : "";
            int att = hex.Length >= 0x32 * 2 ? HexI16(hex, 0x30) : 0;
            int def = hex.Length >= 0x34 * 2 ? HexI16(hex, 0x32) : 0;
            int elements = hex.Length >= 0x14 * 2 ? (ushort)HexI16(hex, 0x12) : 0;
            int bonus8a = hex.Length >= 0x8c * 2 ? HexI16(hex, 0x8a) : 0;

            itemCsv.AppendLine(
                $"{z},{s},{i},\"{items[i].Name}\",\"{items[i].Description.Replace("\"", "\"\"")}\","
                + $"{att},{def},0x{elements:x4},\"{bonus16}\",\"{bonus8}\",{bonus8a},{hex}");
            if (items[i].Icon.Length > 0) iconCsv.AppendLine($"{z},{s},{i},{items[i].Icon}");
        }

        int speciesTable = a.In(0x4c, 4) ? a.I32(0x4c) : 0;
        int modelTable = a.In(0x98, 4) ? a.I32(0x98) : 0;
        int lootTable = a.In(0xb4, 4) ? a.I32(0xb4) : 0;
        int formationTable = a.In(0x34, 4) ? a.I32(0x34) : 0;

        report.AppendLine($"### Zone {z}, sous-zone {s} — repertoire de {a.Len} octets");
        report.AppendLine();

        foreach (string place in PlaceNames(a))
        {
            placeCsv.AppendLine($"{z},{s},\"{place.Replace("\"", "\"\"")}\"");
        }

        if (regions) DumpRegions(a, report);

        // ---- les especes ---------------------------------------------------------------------
        // ResolveEntityBlocks : espece = *(areaDir + 0x4c) + index * 8 ; espece[1] = numero d'IA,
        // espece[4] = index de modele, espece[6] = drapeaux. On s'arrete au premier enregistrement
        // entierement nul, faute de compteur explicite dans le repertoire.
        var modelsUsed = new SortedSet<int>();
        int speciesCount = 0;
        if (speciesTable != 0 && a.In(speciesTable, 8))
        {
            var rows = new List<string>();
            int end = RegionEnd(a, speciesTable);
            for (int off = speciesTable; off + 8 <= end; off += 8)
            {
                int i = (off - speciesTable) / 8;
                string raw = string.Join(" ", Enumerable.Range(0, 8).Select(b => a.U8(off + b).ToString("x2")));
                rows.Add($"| {i} | {a.U8(off + 1)} | {a.U8(off + 4)} | {a.U8(off + 5)} | 0x{a.U16(off + 6):x4} | `{raw}` |");
                speciesCsv.AppendLine(
                    $"{z},{s},{i},{a.U8(off + 1)},{a.U8(off + 4)},{a.U8(off + 5)},0x{a.U16(off + 6):x4},{raw.Replace(" ", "")}");
                modelsUsed.Add(a.U8(off + 4));
                speciesCount++;
            }

            if (rows.Count > 0)
            {
                report.AppendLine("**Especes** (`+0x4c`, 8 octets) — l'aiguillage : quelle IA, quelle fiche, quel butin.");
                report.AppendLine();
                report.AppendLine("| espece | IA (`+1`) | modele (`+4`) | classe butin (`+5`) | drapeaux (`+6`) | octets |");
                report.AppendLine("|---:|---:|---:|---:|---:|---|");
                foreach (string r in rows) report.AppendLine(r);
                report.AppendLine();
            }
        }

        // ---- les fiches ----------------------------------------------------------------------
        // ResetEntityStats (FUN_005389e0) recopie le modele dans l'instance ; ce sont les usages de
        // l'instance qui nomment les champs, pas une supposition sur le modele :
        //
        //  - `+0x00..0x0f` -> `stats+0x00` : les seize octets « recopies tels quels » sont **le
        //    nom**. Le code de controle de texte 0x1d insere le nom d'un locuteur a l'offset 0 de
        //    son bloc, et le premier octet du premier modele lu est le 'G' de « Goblin ».
        //  - `+0x14` -> `stats+0x48` (max) et `+0x4c` (courant) : **les points de vie**. C'est la
        //    paire que la petrification draine de cinq pour cent (WorldMapTick.cs:3221).
        //  - `+0x16` -> `stats+0x4a` / `+0x4e` : **les points de magie**. C'est la paire que la
        //    regeneration lente remonte d'un seizieme tous les cent tours (:2931).
        //  - `+0x10` -> `stats+0x44` : le rang, affiche par la surimpression de mise au point a
        //    cote des deux jauges (:2084) et compare au niveau du personnage a la mise a mort.
        //  - `+0x22` : la valeur que FUN_0051a740 compare a `rand() % 1000` pour decider du butin.
        //  - `+0x30..0x3f` -> `stats+0x30` : huit ushorts, les caracteristiques.
        //
        // Les autres champs sont donnes bruts plutot que nommes au juge.
        if (modelTable != 0 && modelsUsed.Count > 0)
        {
            var rows = new List<string>();
            int end = RegionEnd(a, modelTable);
            foreach (int m in modelsUsed)
            {
                int off = modelTable + m * 0x60;
                if (!a.In(off, 0x60) || off + 0x60 > end) continue;

                string name = Name(a, off);
                string caracs = string.Join(" ", Enumerable.Range(0, 8).Select(i => a.U16(off + 0x30 + i * 2)));

                rows.Add(
                    $"| {m} | **{name}** | {a.U16(off + 0x10)} | **{a.U16(off + 0x14)}** | "
                    + $"**{a.U16(off + 0x16)}** | {a.U16(off + 0x12)} | {a.U16(off + 0x18)} | "
                    + $"{a.U16(off + 0x1a)} | {a.U16(off + 0x22)} | `{caracs}` |");

                string raw = string.Join("", Enumerable.Range(0, 0x60).Select(b => a.U8(off + b).ToString("x2")));
                modelCsv.AppendLine(
                    $"{z},{s},{m},\"{name}\",{a.U16(off + 0x10)},{a.U16(off + 0x14)},{a.U16(off + 0x16)},"
                    + $"{a.U16(off + 0x12)},{a.U16(off + 0x18)},{a.U16(off + 0x1a)},{a.U16(off + 0x22)},"
                    + string.Join(",", Enumerable.Range(0, 8).Select(i => a.U16(off + 0x30 + i * 2)))
                    + $",{raw}");
            }

            if (rows.Count > 0)
            {
                report.AppendLine("**Fiches** (`+0x98`, 0x60 octets) — les octets bruts sont dans `modeles.csv`.");
                report.AppendLine();
                report.AppendLine("| modele | nom | rang `+0x10` | **PV `+0x14`** | **PM `+0x16`** | `+0x12` | `+0x18` | `+0x1a` | butin `+0x22` | caracs `+0x30` |");
                report.AppendLine("|---:|---|---:|---:|---:|---:|---:|---:|---:|---|");
                foreach (string r in rows) report.AppendLine(r);
                report.AppendLine();
            }
        }

        // ---- les formations ------------------------------------------------------------------
        // ScriptOpSpawnFormation (opcode 13) : liste d'enregistrements de 0x20 octets terminee par
        // kind == -1. Un enregistrement = un monstre.
        if (formationTable != 0 && a.In(formationTable, 4))
        {
            var rows = new List<string>();
            int tableEnd = RegionEnd(a, formationTable);
            for (int fp = formationTable; fp + 4 <= tableEnd; fp += 4)
            {
                int f = (fp - formationTable) / 4;
                int rec = a.I32(fp);
                if (rec == 0 || !a.In(rec, 0x20)) continue;

                for (int r = 0; r < 64; r++)
                {
                    int ro = rec + r * 0x20;
                    if (!a.In(ro, 0x20)) break;
                    int kind = a.I32(ro);
                    if (kind == -1) break;

                    // slot[0x0c] est lu en OCTET par ResolveEntityBlocks : seul l'octet bas de
                    // `kind` designe l'espece, les trois autres restent de l'etat d'entite.
                    int species = kind & 0xff;
                    spawns[species] = spawns.GetValueOrDefault(species) + 1;
                    rows.Add($"| {f} | {r} | {species} | {a.I16(ro + 4)} | {a.I16(ro + 6)} | {a.I16(ro + 8)} "
                             + $"| {a.I16(ro + 0x0c)} | 0x{kind:x8} |");
                    formationCsv.AppendLine(
                        $"{z},{s},{f},{r},{species},{a.I16(ro + 4)},{a.I16(ro + 6)},{a.I16(ro + 8)},{a.I16(ro + 0x0c)},"
                        + $"{a.I16(ro + 0x18)},{a.I16(ro + 0x1a)}");
                }
            }

            if (rows.Count > 0)
            {
                report.AppendLine("**Formations** (`+0x34`) — ce que l'opcode 13 fait naitre, et ou.");
                report.AppendLine();
                report.AppendLine("| formation | rang | espece | x | y | z | orientation | `kind` |");
                report.AppendLine("|---:|---:|---:|---:|---:|---:|---:|---|");
                foreach (string r in rows) report.AppendLine(r);
                report.AppendLine();
            }
        }

        // ---- le butin ------------------------------------------------------------------------
        // FUN_0053a920(classe, plage) : table a *(areaDir + 0xb4), 8 octets par classe. Les
        // pourcentages sont ceux de la plage 100 ; a la mise a mort la plage vaut 0x38 + l'ecart de
        // niveau borne a 0x2c, donc 56 a 100.
        if (lootTable != 0 && a.In(lootTable, 8))
        {
            double[] pct = LootTierPercent(100);
            var rows = new List<string>();
            int end = RegionEnd(a, lootTable);
            for (int off = lootTable; off + 8 <= end; off += 8)
            {
                int c = (off - lootTable) / 8;
                rows.Add($"| {c} | " + string.Join(" | ", Enumerable.Range(0, 8).Select(b => ItemLabel(items, a.I8(off + b)))) + " |");
                for (int b = 0; b < 8; b++)
                {
                    int id = a.I8(off + b);
                    lootCsv.AppendLine($"{z},{s},{c},{b},{id},\"{ItemName(items, id)}\",{pct[b]:0.##}");
                }
            }

            if (rows.Count > 0)
            {
                report.AppendLine("**Butin** (`+0xb4`, 8 rangs par classe) — le rang est tire par FUN_0053a920, "
                                  + "et la valeur est l'indice que FUN_00530f20 recoit : un objet du catalogue "
                                  + "de la sous-zone.");
                report.AppendLine();
                report.AppendLine("| classe | rang 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 |");
                report.AppendLine("|---:|---|---|---|---|---|---|---|---|");
                report.AppendLine("| *chance, plage 100* | " + string.Join(" | ", pct.Select(p => $"{p:0.#} %")) + " |");
                foreach (string r in rows) report.AppendLine(r);
                report.AppendLine();
            }
        }

        // ---- les coffres ---------------------------------------------------------------------
        // La table `+0x30` est celle que SpawnSubAreaEntities (FUN_005350b0, WorldMap.cs) lit :
        // une liste de listes d'enregistrements de 0x20 octets, terminee par kind == -1, et
        // chaque enregistrement devient une entite de 0x9c octets. La correspondance champ a
        // champ vient de son corps, pas d'une supposition :
        //
        //   record[7]      -> `*(short*)(slot + 1)`      = octets 4-5 de l'entite, dont l'octet 5
        //                     est le SOUS-TYPE que FUN_0050e5e0 donne a la table 0x0056adc8 ;
        //   record[6]      -> `slot[5]`                  = +0x14 (mot bas) et +0x16 (mot haut) ;
        //   record[2] haut -> `slot[0xc]` mot bas        = +0x30 ;
        //   record[3] haut -> `*(ushort*)(slot + 0xd)`   = +0x34.
        //
        // Les trois sous-types qui remettent un objet sont 13 (FUN_00473ed0), 17 (FUN_004502e0) et
        // 18 (FUN_00450d30) : meme automate, memes champs. Leur etat 0 vide +0x30 et +0x34 dans
        // +0x9a, +0x44 et +0x48 — c'est-a-dire que **la fiche du coffre voyage dans les champs de
        // hauteur et de rotation de l'entite**, et n'est lue qu'a la premiere image.
        //
        //   +0x9a = |+0x30|          le numero de coffre, index du bit « deja ouvert »
        //   +0x44 = +0x34 & 0xff     l'index de la memoire de contenu (DAT_005e9860)
        //   +0x48 = +0x34 >> 8       non nul : le coffre fait naitre un effet et ne donne rien
        //   +0x14 = -1               le coffre donne un OBJET ; sinon il affiche un libelle
        //   +0x16                    la rangee de la table de butin, ou l'index du libelle
        //
        // Le tirage est `PickWeightedTableEntry(+0x16, 100)` — FUN_0053a920, exactement celui des
        // monstres : meme table `+0xb4`, memes huit bandes de probabilite. Un coffre et un monstre
        // ne different donc que par la RANGEE, et c'est pourquoi des objets n'apparaissent que
        // dans les coffres : leurs rangees ne sont referencees par aucune espece.
        int chestLists = a.In(0x30, 4) ? a.I32(0x30) : 0;
        uint[]? handlers = SubAreaEntityHandlers(z, s);
        if (chestLists != 0 && a.In(chestLists, 4) && handlers is not null)
        {
            double[] pct = LootTierPercent(100);
            var rows = new List<string>();
            int listEnd = RegionEnd(a, chestLists);

            for (int lp = chestLists; lp + 4 <= listEnd; lp += 4)
            {
                int listIndex = (lp - chestLists) / 4;
                int rec = a.I32(lp);
                if (rec == 0 || !a.In(rec, 0x20)) continue;

                int recEnd = RegionEnd(a, rec);
                for (int r = 0; r < 128; r++)
                {
                    int ro = rec + r * 0x20;
                    if (!a.In(ro, 0x20) || ro + 0x20 > recEnd) break;
                    if (a.I32(ro) == -1) break;

                    int subtype = a.U8(ro + 0x1d);
                    if (subtype >= handlers.Length) continue;
                    if (!ChestHandlers.TryGetValue(handlers[subtype], out string? family)) continue;

                    int kind = a.I32(ro);
                    int chestId = a.I16(ro + 0x0a);
                    int packed = a.I16(ro + 0x0e);
                    int memory = packed & 0xff;
                    int effect = packed >> 8;          // short signe, comme +0x34 dans le jeu
                    int label = a.I16(ro + 0x18);
                    int row = a.I16(ro + 0x1a);

                    // Les trois roles possibles, dans l'ordre ou les gestionnaires les testent.
                    string role = effect != 0 ? "effet" : label == -1 ? "objet" : "message";

                    string common =
                        $"{z},{s},{listIndex},{r},{subtype},0x{handlers[subtype]:x6},{family},{chestId},"
                        + $"{a.I16(ro + 4)},{a.I16(ro + 6)},{a.I16(ro + 8)},{a.I16(ro + 0x0c)},"
                        + $"{role},{label},{row},{effect},{memory}";

                    if (role != "objet" || lootTable == 0)
                    {
                        chestCsv.AppendLine(common + ",,,,");
                        rows.Add($"| {listIndex}.{r} | {family} `{subtype}` | {chestId} | {role} | — | "
                                 + $"{a.I16(ro + 4)} / {a.I16(ro + 6)} / {a.I16(ro + 8)} |");
                        continue;
                    }

                    int classOff = lootTable + row * 8;
                    if (!a.In(classOff, 8) || classOff + 8 > RegionEnd(a, lootTable))
                    {
                        chestCsv.AppendLine(common + ",,,,");
                        rows.Add($"| {listIndex}.{r} | {family} `{subtype}` | {chestId} | {role} "
                                 + $"| *rangee {row} hors table* | "
                                 + $"{a.I16(ro + 4)} / {a.I16(ro + 6)} / {a.I16(ro + 8)} |");
                        continue;
                    }

                    var names = new List<string>();
                    for (int t = 0; t < 8; t++)
                    {
                        int id = a.I8(classOff + t);
                        string name = ItemName(items, id);
                        chestCsv.AppendLine(
                            common + $",{t},{id},\"{name}\",{pct[t]:0.##}");
                        if (name.Length > 0) names.Add($"{name} ({pct[t]:0.#} %)");
                    }

                    rows.Add($"| {listIndex}.{r} | {family} `{subtype}` | {chestId} | {role} "
                             + $"| rangee {row} : " + string.Join(", ", names.Distinct()) + " | "
                             + $"{a.I16(ro + 4)} / {a.I16(ro + 6)} / {a.I16(ro + 8)} |");
                }
            }

            if (rows.Count > 0)
            {
                report.AppendLine("**Coffres** (`+0x30`, sous-types 13, 17 et 18) — le contenu est tire "
                                  + "dans la meme table `+0xb4` que le butin des monstres, a une rangee "
                                  + "que porte l'enregistrement de placement.");
                report.AppendLine();
                report.AppendLine("| coffre | gestionnaire | id | role | contenu | x / y / z |");
                report.AppendLine("|---|---:|---:|---|---|---|");
                foreach (string r in rows) report.AppendLine(r);
                report.AppendLine();
            }
        }

        // ---- le decor : TOUT ce que la table `+0x30` pose ------------------------------------
        // Les coffres ci-dessus n'en sont qu'une famille. Ici chaque enregistrement, quel que soit
        // son gestionnaire, avec sa position : c'est ce qui dit OU se trouve un gestionnaire de decor
        // (`OpenForesia.Tools/Html/guide-gestionnaires.html`). Le sous-type indexe la table
        // 0x0056adc8, et un sous-type >= 0x40 deborde sur les tables suivantes, qui la suivent en
        // memoire : d'ou la resolution par les sept blocs mis bout a bout.
        uint[]? allHandlers = SubAreaAllHandlers(z, s);
        if (chestLists != 0 && a.In(chestLists, 4) && allHandlers is not null)
        {
            int listEnd = RegionEnd(a, chestLists);
            for (int lp = chestLists; lp + 4 <= listEnd; lp += 4)
            {
                int rec = a.I32(lp);
                if (rec == 0 || !a.In(rec, 0x20)) continue;

                int recEnd = RegionEnd(a, rec);
                for (int r = 0; r < 128; r++)
                {
                    int ro = rec + r * 0x20;
                    if (!a.In(ro, 0x20) || ro + 0x20 > recEnd) break;
                    if (a.I32(ro) == -1) break;

                    int subtype = a.U8(ro + 0x1d);
                    int slot = 0x20 + subtype;
                    uint handler = slot < allHandlers.Length ? allHandlers[slot] : 0;
                    s_decorCsv.AppendLine(
                        $"{z},{s},{(lp - chestLists) / 4},{r},{subtype},0x{handler:x8},"
                        + $"{a.I16(ro + 4)},{a.I16(ro + 6)},{a.I16(ro + 8)}");
                }
            }
        }

        // ---- l'index inverse : quel monstre lache quoi ----------------------------------------
        // La chaine complete, chaque maillon releve ailleurs dans ce fichier : l'espece porte sa
        // classe de butin (`+5`) et son modele (`+4`, donc le nom) ; la classe designe une rangee de
        // huit indices ; l'indice nomme un objet du catalogue de la sous-zone. Le compte de
        // placements dit combien d'exemplaires du monstre la sous-zone contient.
        if (speciesTable != 0 && lootTable != 0 && modelTable != 0)
        {
            double[] pct = LootTierPercent(100);
            int spEnd = RegionEnd(a, speciesTable);
            int ltEnd = RegionEnd(a, lootTable);
            int mdEnd = RegionEnd(a, modelTable);

            for (int off = speciesTable; off + 8 <= spEnd; off += 8)
            {
                int speciesIndex = (off - speciesTable) / 8;
                int modelOff = modelTable + a.U8(off + 4) * 0x60;
                int classOff = lootTable + a.U8(off + 5) * 8;
                if (!a.In(modelOff, 0x60) || modelOff + 0x60 > mdEnd) continue;
                if (!a.In(classOff, 8) || classOff + 8 > ltEnd) continue;

                string monster = Name(a, modelOff);
                int threshold = a.U16(modelOff + 0x22);
                int placements = spawns.TryGetValue(speciesIndex, out int n) ? n : 0;

                for (int t = 0; t < 8; t++)
                {
                    int id = a.I8(classOff + t);
                    string item = ItemName(items, id);
                    if (item.Length == 0) continue;

                    dropCsv.AppendLine(
                        $"\"{item}\",{z},{s},\"{monster}\",{a.U8(off + 5)},{t},{pct[t]:0.##},"
                        + $"{threshold},{placements}");
                }
            }
        }

        if (speciesCount == 0 && formationTable == 0)
        {
            report.AppendLine("*Aucune table d'entites dans cette sous-zone.*");
            report.AppendLine();
        }
    }

    /// <summary>
    /// <b>Les fiches de sort</b>, lues dans la table <c>+0x9c</c> du repertoire d'etat exactement
    /// comme <c>CastSpell_0053A980</c> les adresse :
    /// <c>*(int*)(*(int*)(etat + 0x9c) + famille * 4) + indice * 0x30</c>.
    ///
    /// <para>
    /// <b>Le compte de chaque famille vient de la donnee elle-meme</b>, pas d'une table du binaire :
    /// les listes se suivent sans trou, donc la fin de l'une est le debut de la suivante, et la
    /// derniere est bornee par <see cref="RegionEnd"/>. Le recoupement avec <c>DAT_00564600</c>
    /// (<c>1d 18 14</c>) est exact sur les trois ecoles reelles — 29 Sorcier/Elfe, 24 Pretre,
    /// 20 Fee — et c'est ce qui valide la methode.
    /// </para>
    /// <para>
    /// Les champs releves sont ceux de <c>Documentation/Reference/Sorts.md</c> : <c>+0x10</c> le
    /// <b>sous-type d'entite</b> que <c>SpawnEffectEntity</c> fait naitre (c'est lui qui decide si
    /// le sort est implemente ou non), <c>+0x11</c> et <c>+0x12</c> ses deux parametres,
    /// <c>+0x13</c> la duree d'incantation en images, <c>+0x14</c> le cout en PM, <c>+0x1d</c> le
    /// cout d'apprentissage, <c>+0x1f</c> la longueur de la combinaison et <c>+0x20</c> la
    /// combinaison elle-meme.
    /// </para>
    /// <para>
    /// <b>La combinaison n'est pas faite de directions mais des quatre boutons de face.</b> La
    /// saisie est <c>(manette[2..3] &gt;&gt; 8) &amp; 0xf0</c> et le masque teste est
    /// <c>0xf000</c> : ce sont les quatre bits que <c>s_PadButtonMasks_00564f18</c> nomme triangle,
    /// rond, croix et carre. Un octet peut en porter plusieurs — deux boutons dans la meme image.
    /// </para>
    /// </summary>
    private static void DumpSpellSheets(Blob stateDir, StringBuilder report, StringBuilder csv)
    {
        if (!stateDir.In(0x9c, 4)) return;
        int table = stateDir.I32(0x9c);
        if (table == 0 || !stateDir.In(table, 4)) return;

        int tableEnd = RegionEnd(stateDir, table);

        // Les pointeurs de famille, dans l'ordre : ils bornent les listes les uns les autres.
        var lists = new List<int>();
        for (int at = table; at + 4 <= tableEnd; at += 4)
        {
            int list = stateDir.I32(at);
            if (list == 0 || !stateDir.In(list, 0x30)) break;
            lists.Add(list);
        }
        if (lists.Count == 0) return;

        report.AppendLine($"## Fiches de sort — table `+0x9c` a 0x{table:x6}, {lists.Count} famille(s)");
        report.AppendLine();
        report.AppendLine("| famille | pointeur | fiches | premier | dernier |");
        report.AppendLine("|---:|---:|---:|---|---|");

        for (int family = 0; family < lists.Count; family++)
        {
            int list = lists[family];

            // La borne : la famille suivante si elle est bien au-dessus, sinon la fin de region.
            bool bounded = family + 1 < lists.Count && lists[family + 1] > list;
            int end = bounded ? lists[family + 1] : RegionEnd(stateDir, list);
            int count = (end - list) / 0x30;
            if (count <= 0) continue;

            string first = "", last = "";

            for (int index = 0; index < count; index++)
            {
                int off = list + index * 0x30;
                if (!stateDir.In(off, 0x30)) { count = index; break; }

                // LA DERNIERE FAMILLE N'A PAS DE SUCCESSEUR pour la borner, et RegionEnd deborde :
                // le releve brut continuait 200 fiches trop loin, dans la region de textes qui suit
                // (« Feu », « Eau », « Divin »… — les noms d'element). L'invariant qui tranche est
                // celui de la combinaison : le tampon de saisie du joueur est plafonne a 0x10 coups
                // (ApplyGroundMotion_0051A020) et les seize octets tiennent exactement entre +0x20
                // et la fin de la fiche. Une longueur superieure n'est donc pas une fiche.
                if (!bounded && stateDir.U8(off + 0x1f) > 0x10) { count = index; break; }

                string name = Name(stateDir, off);
                if (index == 0) first = name;
                last = name;

                // Un tiret entre les coups : sans lui « TO » ne dirait pas si c'est un coup a deux
                // boutons ou deux coups a un bouton, et les deux existent.
                byte length = stateDir.U8(off + 0x1f);
                var combo = new StringBuilder();
                for (int k = 0; k < length && k < 0x10; k++)
                {
                    if (k != 0) combo.Append('-');
                    combo.Append(Buttons(stateDir.U8(off + 0x20 + k)));
                }

                string raw = string.Join("", Enumerable.Range(0, 0x30).Select(b => stateDir.U8(off + b).ToString("x2")));

                csv.AppendLine(
                    $"{family},{index},\"{name.Replace("\"", "\"\"")}\",{stateDir.U8(off + 0x10)},"
                    + $"{stateDir.U8(off + 0x11)},{stateDir.U8(off + 0x12)},{stateDir.U8(off + 0x13)},"
                    + $"{stateDir.U16(off + 0x14)},{stateDir.U8(off + 0x1d)},{length},"
                    + $"{combo},{raw}");
            }

            report.AppendLine($"| {family} | 0x{list:x6} | {count} | {first} | {last} |");
        }
        report.AppendLine();
    }

    /// <summary>
    /// Un octet de combinaison rendu en lettres : <c>T</c> triangle, <c>O</c> rond, <c>X</c> croix,
    /// <c>C</c> carre — l'ordre des bits de <c>s_PadButtonMasks_00564f18</c> decale de huit. Un
    /// octet a plusieurs bits donne plusieurs lettres, et c'est voulu : deux boutons tenus dans la
    /// meme image ne font qu'un coup dans la combinaison.
    /// </summary>
    private static string Buttons(byte value)
    {
        var text = new StringBuilder();
        if ((value & 0x10) != 0) text.Append('T');
        if ((value & 0x20) != 0) text.Append('O');
        if ((value & 0x40) != 0) text.Append('X');
        if ((value & 0x80) != 0) text.Append('C');
        return text.Length == 0 ? "." : text.ToString();
    }

    /// <summary>
    /// Les listes de fiches d'objets du repertoire d'etat, adressees comme le fait FUN_0053a980 :
    /// <c>*(int*)(*(int*)(etat + 0x9c) + liste * 4) + index * 0x30</c>. Un releve, pas encore une
    /// lecture : la structure des 0x30 octets n'est pas etablie, seul le nom de tete l'est.
    /// </summary>
    private static void DumpItemLists(Blob stateDir, StringBuilder report)
    {
        if (!stateDir.In(0x9c, 4)) return;
        int table = stateDir.I32(0x9c);
        if (table == 0 || !stateDir.In(table, 4)) return;

        int tableEnd = RegionEnd(stateDir, table);
        report.AppendLine($"## Fiches d'objets — table `+0x9c` a 0x{table:x6}, {(tableEnd - table) / 4} liste(s)");
        report.AppendLine();

        for (int lp = table; lp + 4 <= tableEnd; lp += 4)
        {
            int list = stateDir.I32(lp);
            if (list == 0 || !stateDir.In(list, 0x30)) continue;

            int listEnd = RegionEnd(stateDir, list);
            int count = (listEnd - list) / 0x30;
            report.AppendLine($"### Liste {(lp - table) / 4} — {count} fiche(s) de 0x30 octets");
            report.AppendLine();
            report.AppendLine("| index | nom | octets |");
            report.AppendLine("|---:|---|---|");
            for (int i = 0; i < count; i++)
            {
                int off = list + i * 0x30;
                if (!stateDir.In(off, 0x30)) break;
                string raw = string.Join("", Enumerable.Range(0, 0x30).Select(b => stateDir.U8(off + b).ToString("x2")));
                report.AppendLine($"| {i} | {Name(stateDir, off)} | `{raw}` |");
            }
            report.AppendLine();
        }
    }

    /// <summary>
    /// Les seize octets de tete d'une fiche : le nom. Encodage Latin-1 (« Fourmi.géante » porte un
    /// <c>0xe9</c>), et l'octet nul y sert de separateur de mot autant que de fin de chaine — il
    /// est donc rendu par une espace, et seule la fin de la zone termine le nom.
    /// </summary>
    private static string Name(Blob a, int off)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 16; i++)
        {
            byte c = a.U8(off + i);
            if (c == 0) { sb.Append(' '); continue; }
            sb.Append(c < 0x80 ? (char)c : (char)c);   // Latin-1 : l'octet est deja le point de code
        }
        return sb.ToString().Trim();
    }

    /// <summary>
    /// Les 60 pointeurs d'en-tete, tries — la carte des regions du blob. Sert a se reperer quand
    /// une table nouvelle est cherchee : c'est ce releve qui a montre que les noms de monstres
    /// suivent immediatement la table des especes.
    /// </summary>
    private static void DumpRegions(Blob a, StringBuilder report)
    {
        report.AppendLine("**Regions du repertoire** (les 60 dwords d'en-tete, tries).");
        report.AppendLine();
        report.AppendLine("| champ | pointeur | taille | premiers octets |");
        report.AppendLine("|---|---:|---:|---|");

        var fields = new List<(int Field, int Ptr)>();
        for (int i = 0; i < 0x3c; i++)
        {
            int p = a.I32(0x10 + i * 4);
            if (p > 0 && a.In(p)) fields.Add((0x10 + i * 4, p));
        }

        foreach (var (field, ptr) in fields.OrderBy(f => f.Ptr))
        {
            int end = RegionEnd(a, ptr);
            int show = Math.Min(16, end - ptr);
            string hex = string.Join(" ", Enumerable.Range(0, Math.Max(show, 0)).Select(b => a.U8(ptr + b).ToString("x2")));
            string txt = new(Enumerable.Range(0, Math.Max(show, 0))
                .Select(b => a.U8(ptr + b) is >= 0x20 and < 0x7f ? (char)a.U8(ptr + b) : '.').ToArray());
            report.AppendLine($"| `+0x{field:x2}` | 0x{ptr:x6} | {end - ptr} | `{hex}`  `{txt}` |");
        }
        report.AppendLine();

        // Les trois catalogues d'effets par rang : le code de jeu lit `(*(byte**)(dir + champ))[k]`
        // et fait naitre le sous-type `0xc0 + octet` (Behemoth +0xcc[2], Ange noir +0xd4[7],
        // explosions +0xd0[n]). Le sous-type `0xc0 + n` designe l'entree n des tables par
        // sous-zone.
        report.AppendLine("**Catalogues d'effets** (`(*(byte**)(dir + champ))[k]`, en sous-types `0xc0 + octet`).");
        report.AppendLine();
        foreach (int field in new[] { 0xcc, 0xd0, 0xd4 })
        {
            int region = a.In(field, 4) ? a.I32(field) : 0;
            int bytes = region > 0 && a.In(region, 4) ? a.I32(region) : 0;
            if (bytes <= 0 || !a.In(bytes, 1))
            {
                report.AppendLine($"- `+0x{field:x2}` : region 0x{region:x6}, pas de tableau lisible");
                continue;
            }
            int n = Math.Min(32, a.Len - bytes);
            string sub = string.Join(" ", Enumerable.Range(0, n).Select(k => $"{k}:{0xc0 + a.U8(bytes + k):x2}"));
            report.AppendLine($"- `+0x{field:x2}` : region 0x{region:x6} -> 0x{bytes:x6} : {sub}");
        }
        report.AppendLine();

        // Les scripts de la table `+0x8c` (RunScript) : releve HEURISTIQUE de l'opcode 16
        // (ScriptOpSpawnEffect, `10 sous-type p3 p4`) avec un sous-type >= 0xc0. Sans decodeur
        // d'opcodes, un argument qui vaudrait 0x10 donne un faux positif : c'est une piste a
        // verifier, pas une preuve.
        int scripts = a.In(0x8c, 4) ? a.I32(0x8c) : 0;
        if (scripts > 0 && a.In(scripts, 4))
        {
            int tableEnd = RegionEnd(a, scripts);
            var starts = new List<int>();
            for (int p = scripts; p + 4 <= tableEnd; p += 4)
            {
                int s = a.I32(p);
                if (s > 0 && a.In(s)) starts.Add(s);
            }
            var hits = new List<string>();
            for (int i = 0; i < starts.Count; i++)
            {
                int end = starts.Where(x => x > starts[i]).DefaultIfEmpty(RegionEnd(a, starts[i])).Min();
                for (int p = starts[i]; p + 3 < end; p++)
                {
                    if (a.U8(p) == 0x10 && a.U8(p + 1) >= 0xc0)
                        hits.Add($"script {i} +0x{p - starts[i]:x}: `10 {a.U8(p + 1):x2} {a.U8(p + 2):x2} {a.U8(p + 3):x2}`");
                }
            }
            report.AppendLine($"**Scripts `+0x8c`** : {starts.Count} script(s) ; opcode 16 probable vers un sous-type >= 0xc0 : "
                + (hits.Count == 0 ? "aucun" : string.Join(" ; ", hits)));
            report.AppendLine();
        }
    }
}
