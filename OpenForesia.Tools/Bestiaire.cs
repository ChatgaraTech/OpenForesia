using System.Text;

/// <summary>
/// Lecture <b>hors-jeu</b> des donnees d'ennemis du CD d'origine.
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
    internal readonly record struct Item(string Name, string Description, string Hex);

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
                    string.Join("", Enumerable.Range(0, 0x140).Select(b => blob.U8(chunk + b).ToString("x2")))));
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

    public static void Run(string cdRoot, int? onlyZone, string? outDir, bool regions)
    {
        string french = Path.Combine(cdRoot, "French");
        if (!Directory.Exists(french)) french = cdRoot;

        string systemIni = Path.Combine(french, "SYSTEM.INI");
        string archivePath = Path.Combine(french, "BLAZE.ALL");

        if (!File.Exists(systemIni) || !File.Exists(archivePath))
        {
            Console.WriteLine($"Introuvable : {systemIni} et/ou {archivePath}");
            Console.WriteLine("Donner la racine du CD (celle qui contient French\\) ou le repertoire French lui-meme.");
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

        var speciesCsv = new StringBuilder("zone,sous_zone,espece,ia,modele,classe_butin,drapeaux,octets\n");
        var modelCsv = new StringBuilder(
            "zone,sous_zone,modele,nom,rang_0x10,pv_0x14,pm_0x16,champ_0x12,champ_0x18,champ_0x1a,"
            + "butin_0x22,c0,c1,c2,c3,c4,c5,c6,c7,octets\n");
        var formationCsv = new StringBuilder("zone,sous_zone,formation,rang,espece,x,y,z,orientation\n");
        var lootCsv = new StringBuilder("zone,sous_zone,classe,rang,objet_id,objet,pourcent_plage_100\n");
        var itemCsv = new StringBuilder(
            "zone,sous_zone,objet_id,nom,description,att_0x30,def_0x32,elements_0x12,"
            + "bonus_0x20,bonus_0x18,bonus_0x8a,octets\n");
        var dropCsv = new StringBuilder(
            "objet,zone,sous_zone,monstre,classe,rang,pourcent_plage_100,seuil_0x22,placements\n");
        var placeCsv = new StringBuilder("zone,sous_zone,nom\n");

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
                DumpSubArea(z, s, areaDir, archive, regions, report, speciesCsv, modelCsv, formationCsv, lootCsv, itemCsv, dropCsv, placeCsv);
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
            Console.WriteLine();
            Console.WriteLine($"Ecrit dans {outDir} : bestiaire.md, especes.csv, modeles.csv, "
                              + "formations.csv, butin.csv, objets.csv, butin_par_objet.csv, lieux.csv");
        }
    }

    // Les seuils de FUN_0053a920 : rand() % plage, compare a 0x18, 0x29, 0x38, 0x45, 0x50, 0x59,
    // 0x5f. Huit rangs, donc, et ce sont ces bornes qui donnent les pourcentages.
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
        StringBuilder placeCsv)
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
                        $"{z},{s},{f},{r},{species},{a.I16(ro + 4)},{a.I16(ro + 6)},{a.I16(ro + 8)},{a.I16(ro + 0x0c)}");
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
    }
}
