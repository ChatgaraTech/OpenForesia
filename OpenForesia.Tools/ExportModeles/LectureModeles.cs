using System.Security.Cryptography;
using ExportCarte;

// La lecture des modeles animes pour l'exporteur de modeles (`exporter-modeles`), hors jeu. Comme
// pour les cartes, chaque lecteur recopie une formule du jeu sans l'appeler (« Miroir de : »).
internal static partial class Bestiaire
{
    /// <summary>Une pose d'os : trois angles (4 096 par tour), une translation, une echelle uniforme (0x1000 = 1).</summary>
    private readonly record struct Pose(short Rx, short Ry, short Rz, short Tx, short Ty, short Tz, ushort Echelle);

    /// <summary>Une animation : ses images, et pour chaque image l'indice de pose de chaque os.</summary>
    private sealed class Clip
    {
        public int Images, Os, Numero;
        public int[] Parents = [];          // parent de chaque os (l'os 0 pend a l'entite)
        public int[] OsDesParties = [];     // l'os qui porte chaque partie
        public List<Pose> Poses = new();
        public ushort[] Indices = [];       // Images x Os
    }

    private sealed class ModeleAnime
    {
        public string Nom = "", Famille = "", Dossier = "";
        public readonly List<string> Vu = new();       // ou il apparait
        public readonly List<List<Tri>> Parties = new();
        public readonly List<Clip> Clips = new();
        public byte[]? Page;                           // la texture, 256 x 256 RGBA
        public readonly List<string> Avertissements = new();
        public int Triangles => Parties.Sum(p => p.Count);
    }

    /// <summary>
    /// Une partie de modele anime. Miroir de : DrawModelPart (Engine/Model3d.cs) — seuls les deux
    /// seaux de quads (<c>+0x1a</c> plats, <c>+0x1e</c> degrades) sont dessines, a la suite des quatre
    /// seaux sautes ; faces de 16 octets, indices sur un octet, UV de v0/v1 dans le mot 2 et de v2/v3
    /// dans le mot 3, quad emis dans l'ordre v1, v0, v2, v3. RelocateModelBundle a deja passe
    /// AdjustModelTexturePages : <c>(attr &gt;&gt; 8) &amp; 0xc0</c> s'ajoute au v.
    /// </summary>
    private static List<Tri> LirePartie(Blob m, uint attr, List<string> avert)
    {
        var tris = new List<Tri>();
        if (m.Len < 0x40) return tris;
        int nv = m.U16(8);
        uint adjust = (attr >> 8) & 0xc0;
        int skip = m.U16(0x10) + m.U16(0x12) + m.U16(0x14) + m.U16(0x16);
        if (m.U16(0x18) != 0 || m.U16(0x1c) != 0) avert.Add("partie avec des triangles, que le jeu ne dessine pas sur un modèle animé");
        int ro = m.I32(0x28) + skip * 0x10;
        foreach (int count in new[] { m.U16(0x1a), m.U16(0x1e) })
            for (int n = count; n > 0; n--, ro += 0x10)
            {
                if (!m.In(ro, 0x10)) { avert.Add("face hors de la partie"); continue; }
                uint packed = (uint)m.I32(ro), r2 = (uint)m.I32(ro + 8), r3 = (uint)m.I32(ro + 12);
                int[] i = [(int)(packed & 0xff), (int)((packed >> 8) & 0xff), (int)((packed >> 16) & 0xff), (int)(packed >> 24)];
                if (i.Any(x => x >= nv || !m.In(0x40 + x * 8, 6))) { avert.Add("indice de sommet hors de la partie"); continue; }
                uint V(uint w) => (w & 0xff) | ((((w >> 8) + adjust) & 0xff) << 8);
                uint[] uv = [V(r2 & 0xffff), V(r2 >> 16), V(r3 & 0xffff), V(r3 >> 16)];
                var p = i.Select(x => Sommet(m, x)).ToArray();
                tris.Add(MakeTri(p[1], p[0], p[2], uv[1], uv[0], uv[2], 0));
                tris.Add(MakeTri(p[0], p[2], p[3], uv[0], uv[2], uv[3], 0));
            }
        return tris;
    }

    /// <summary>Un paquet de modele : un compte, autant d'offsets, autant de descripteurs. Miroir de : RelocateModelBundle.</summary>
    private static List<(uint Attr, Blob Corps)> PartiesDuPaquet(Blob b, uint pageBits)
    {
        var r = new List<(uint, Blob)>();
        int n = b.Len >= 4 ? b.I32(0) : 0;
        if (n <= 0 || n > 256) return r;
        for (int j = 0; j < n && b.In((n + 1 + j) * 4, 4); j++)
        {
            int off = b.I32((1 + j) * 4) + n * 8 + 4;
            uint attr = (uint)b.I32((n + 1 + j) * 4) | pageBits;
            r.Add((attr, b.In(off, 0x40) ? new Blob(b.Data[off..]) : new Blob([])));
        }
        return r;
    }

    /// <summary>
    /// Un paquet d'animations. Miroir de : RelocateAnimationBundle et RelocateAnimationEntry — un
    /// compte, autant d'offsets ; chaque entree porte huit pointeurs en <c>+0x20</c>, relatifs a
    /// <c>entree + entree[0]</c>. Puis ce que DrawAnimatedModel lit : <c>+0x12</c> le nombre d'os,
    /// <c>+0x14</c> le nombre de parties, <c>+0x16</c> le nombre d'images (les scenes le relisent),
    /// <c>+0x20</c> les poses de 0x10 octets, <c>+0x24</c> les parents puis l'os de chaque partie,
    /// <c>+0x28</c> les indices de pose, image par image.
    /// </summary>
    private static List<Clip> ClipsDuPaquet(Blob b, List<string> avert)
    {
        var r = new List<Clip>();
        int n = b.Len >= 4 ? b.I32(0) : 0;
        if (n <= 0 || n > 512) return r;
        for (int k = 0; k < n; k++)
        {
            int o = b.I32((1 + k) * 4) + n * 4 + 4;
            if (!b.In(o, 0x40)) { avert.Add($"animation {k} hors du paquet"); continue; }
            int origine = b.I32(o);
            int Ptr(int m) => o + origine + b.I32(o + 0x20 + m * 4);
            var c = new Clip { Os = b.U16(o + 0x12), Images = b.U16(o + 0x16), Numero = k + 1 };
            if (c.Images == 0) continue;   // une animation sans image : rien a jouer, son numero reste reserve
            int parties = b.U16(o + 0x14);
            int poses = Ptr(0), table = Ptr(1), indices = Ptr(2);
            if (c.Os == 0 || c.Os > 1024 || parties > 1024 || !b.In(table, (c.Os + parties) * 2) || !b.In(indices, c.Images * c.Os * 2))
            { avert.Add($"animation {k} illisible ({c.Os} os, {parties} parties, {c.Images} images, table {table - o}, indices {indices - o}, paquet {b.Len})"); continue; }
            c.Parents = Enumerable.Range(0, c.Os).Select(i => (int)b.U16(table + i * 2)).ToArray();
            c.OsDesParties = Enumerable.Range(0, parties).Select(j => (int)b.U16(table + (c.Os + j) * 2)).ToArray();
            c.Indices = Enumerable.Range(0, c.Images * c.Os).Select(i => b.U16(indices + i * 2)).ToArray();
            int maxPose = c.Indices.Length == 0 ? -1 : c.Indices.Max();
            for (int p = 0; p <= maxPose; p++)
            {
                int po = poses + p * 0x10;
                if (!b.In(po, 0x10)) { avert.Add($"animation {k} : pose {p} hors du paquet"); break; }
                c.Poses.Add(new Pose(b.I16(po), b.I16(po + 2), b.I16(po + 4), b.I16(po + 8), b.I16(po + 10), b.I16(po + 12), b.U16(po + 14)));
            }
            if (c.Poses.Count <= maxPose) continue;
            for (int i = 1; i < c.Os; i++)
                if (c.Parents[i] >= i) { avert.Add($"animation {k} : l'os {i} pend à un os qui le suit"); break; }
            r.Add(c);
        }
        return r;
    }

    /// <summary>
    /// La texture d'un modele anime. Miroir de : DrawAnimatedModel — calculee une seule fois, sur la
    /// PREMIERE partie (les suivantes reutilisent sa page) : emplacement <c>(attr &gt;&gt; 16) &amp; 0xff</c>
    /// de la table, page = <c>(uv0 &amp; 0xf800) | ((attr &amp; 0xff) | 0x1c0) &lt;&lt; 5 | slot</c>, palette =
    /// moitie haute + le decalage de l'entite (<c>+0x0a</c>), plafonnee a 0x7ec0.
    /// </summary>
    private static byte[]? PageDuModele(Vram vram, (uint Attr, Blob Corps) premiere, ushort extra)
    {
        var (attr, m) = premiere;
        int slot = (int)((attr >> 16) & 0xff);
        if (slot >= vram.Slots.Count || m.Len < 0x40) return null;
        var (tp, cl) = vram.Slots[slot];
        int skip = m.U16(0x10) + m.U16(0x12) + m.U16(0x14) + m.U16(0x16);
        int fo = m.I32(0x28) + skip * 0x10 + 8;
        uint uv0 = m.In(fo, 2) ? m.U16(fo) : 0u;
        uint tpage = ((uv0 & 0xf800) | ((((attr & 0xff) | 0x1c0)) << 5) | tp) & 0xffff;
        uint clut = cl + (uint)(((short)extra >> 2) & unchecked((int)0xffffffc0)) + (uint)(extra & 0xff);
        if (clut > 0x7ec0) clut = 0x7ec0;
        return vram.Page((ushort)(tpage & 0x19f), (ushort)clut, out _);
    }

    private static string Empreinte(params byte[][] blocs)
    {
        using var h = SHA1.Create();
        foreach (var b in blocs) h.TransformBlock(b, 0, b.Length, null, 0);
        h.TransformFinalBlock([], 0, 0);
        return Convert.ToHexString(h.Hash!)[..16];
    }

    private static readonly string[] NomsDeClasse = ["Guerrier", "Prêtre", "Voleur", "Sorcier", "Chasseur", "Elfe", "Nain", "Fée"];

    /// <summary>
    /// Tous les modeles animes du jeu : les seize heros (repertoire de l'etat 1), puis, sous-zone par
    /// sous-zone, les modeles <c>+0x2c</c> avec leurs animations <c>+0x7c</c>, nommes par les especes
    /// qui les emploient. Un meme modele (memes octets, memes animations, meme texture) n'est garde
    /// qu'une fois.
    /// </summary>
    private static List<ModeleAnime> LireTousLesModeles(byte[] archive, Blob root)
    {
        var tous = new List<ModeleAnime>();
        var parCle = new Dictionary<string, ModeleAnime>();

        // ---- les heros : Miroir de LoadPartyMemberResources (Screens/DebugParty.cs), pour l'emplacement 0
        var d = LoadEntry(archive, root, EntryList(root, 0x48)[1]);
        int tex = d.I32(0x84), mod = d.I32(0x2c), anim = d.I32(0x7c);
        for (int id = 0; id < 16; id++)
        {
            var vram = new Vram();
            uint m1 = (uint)d.I32(tex + id * 8 + 4);
            vram.LoadTimSlot(LoadEntryRaw(archive, d, d.I32(tex + id * 8)).Data, 0x20, (byte)m1, (byte)(m1 >> 8), 0);
            uint m2 = (uint)d.I32(tex + 0x80 + id * 8 + 4);
            int clutY = (byte)((byte)(m2 >> 8) + 0x10) + 0x1c0;
            vram.LoadTimAt(LoadEntryRaw(archive, d, d.I32(tex + 0x80 + id * 8)).Data, 0x24, 0, clutY, 0x40,
                ((int)m2 & 0xf) << 6, (((int)m2 & 0x10) + 3 * 4) << 4);
            uint pageBits = (uint)d.I32(mod + id * 8 + 4) & 0xff0000ffu;   // emplacement 0 : octet 1 a 0, octet 2 + 0
            pageBits |= (uint)d.I32(mod + id * 8 + 4) & 0x00ff0000u;
            var ma = new ModeleAnime
            {
                Nom = $"{NomsDeClasse[id & 7]} ({((id >> 3) == 0 ? "homme" : "femme")})", Famille = "Héros", Dossier = "Héros",
            };
            ma.Vu.Add("le groupe");
            var parts = PartiesDuPaquet(LoadEntryRaw(archive, d, d.I32(mod + id * 8)), pageBits);
            foreach (var p in parts) ma.Parties.Add(LirePartie(p.Corps, p.Attr, ma.Avertissements));
            ma.Clips.AddRange(ClipsDuPaquet(LoadEntryRaw(archive, d, d.I32(anim + id * 4)), ma.Avertissements));
            if (parts.Count > 0) ma.Page = PageDuModele(vram, parts[0], 0);
            tous.Add(ma);
        }

        // ---- les sous-zones
        var stateDir = LoadEntry(archive, root, EntryList(root, 0x48)[3]);
        var zones = EntryList(stateDir, 0x48);
        for (int z = 0; z < zones.Count; z++)
        {
            var zb = LoadEntry(archive, stateDir, zones[z]);
            var subs = EntryList(zb, 0x48);
            for (int s = 0; s < subs.Count; s++)
            {
                var a = LoadEntry(archive, zb, subs[s]);
                if (a.Len < 0x100) continue;
                string lieu = PlaceNames(a).FirstOrDefault() ?? $"sous-zone {s}";

                var modeles = new List<(int Entree, uint PageBits)>();
                for (int e = a.I32(0x2c); a.In(e, 8) && a.I32(e) != 0; e += 8) modeles.Add((a.I32(e), (uint)a.I32(e + 4)));
                var anims = new List<int>();
                for (int e = a.I32(0x7c); a.In(e, 4) && a.I32(e) != 0; e += 4) anims.Add(a.I32(e));
                if (modeles.Count == 0) continue;

                var vram = new Vram();
                for (int e = a.I32(0x10); a.In(e, 8) && a.I32(e) != 0; e += 8)
                {
                    uint meta = (uint)a.I32(e + 4);
                    vram.LoadTim(LoadEntryRaw(archive, a, a.I32(e)).Data, (byte)meta, (byte)(meta >> 8), (byte)(meta >> 16));
                }

                // Les especes : octet 0 = le modele, octets 2-3 = le decalage de palette (Miroir de :
                // SpawnEntityFormation, `slot+4 = espece[0..3]`), octet 4 = la fiche (le nom).
                var usages = new List<(int Kind, ushort Extra, string Nom)>();
                int speciesTable = a.In(0x4c, 4) ? a.I32(0x4c) : 0, modelTable = a.In(0x98, 4) ? a.I32(0x98) : 0;
                if (speciesTable != 0 && a.In(speciesTable, 8))
                {
                    int end = RegionEnd(a, speciesTable);
                    for (int so = speciesTable; so + 8 <= end; so += 8)
                    {
                        int kind = a.U8(so);
                        ushort extra = (ushort)(a.U8(so + 3) | a.U8(so + 2) << 8);
                        int mo = modelTable + a.U8(so + 4) * 0x60;
                        string nom = modelTable != 0 && a.In(mo, 0x60) ? Name(a, mo) : "";
                        if (nom.Count(char.IsLetter) < 2) nom = "";   // les fiches sans nom (les gens de l'auberge portent « £ »)
                        if (kind < modeles.Count && nom.Length > 0) usages.Add((kind, extra, nom));
                    }
                }
                for (int k = 0; k < modeles.Count; k++)
                    if (!usages.Any(u => u.Kind == k)) usages.Add((k, 0, ""));

                var bundles = new Dictionary<int, (Blob M, Blob A)>();
                foreach (var (kind, extra, nom) in usages.Distinct())
                {
                    if (!bundles.TryGetValue(kind, out var ba))
                    {
                        ba = (LoadEntryRaw(archive, a, modeles[kind].Entree), kind < anims.Count ? LoadEntryRaw(archive, a, anims[kind]) : new Blob([]));
                        bundles[kind] = ba;
                    }
                    if (ba.M.Len < 8) continue;
                    var parts = PartiesDuPaquet(ba.M, modeles[kind].PageBits);
                    if (parts.Count == 0) continue;
                    var page = PageDuModele(vram, parts[0], extra);
                    string cle = Empreinte(ba.M.Data, ba.A.Data, page ?? []);
                    string ou = $"{NomDeZone(z)} — {lieu}";
                    if (parCle.TryGetValue(cle, out var deja))
                    {
                        if (!deja.Vu.Contains(ou)) deja.Vu.Add(ou);
                        if (deja.Famille == "Sans espèce" && nom.Length > 0) { deja.Nom = nom; deja.Famille = "Monstres et personnages"; }
                        continue;
                    }
                    var ma = new ModeleAnime
                    {
                        Nom = nom.Length > 0 ? nom : $"{(z == 0 ? "Personnage" : "Modèle")} {kind} ({lieu})",
                        Famille = nom.Length > 0 ? "Monstres et personnages" : "Sans espèce",
                        Dossier = $"{z:D2} - {NomDeZone(z)}", Page = page,
                    };
                    ma.Vu.Add(ou);
                    foreach (var p in parts) ma.Parties.Add(LirePartie(p.Corps, p.Attr, ma.Avertissements));
                    ma.Clips.AddRange(ClipsDuPaquet(ba.A, ma.Avertissements));
                    parCle[cle] = ma;
                    tous.Add(ma);
                }
            }
        }
        return tous;
    }
}
