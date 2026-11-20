using ExportCarte;

// La lecture d'une sous-zone pour l'exporteur de cartes (`exporter-carte`), hors jeu. Chaque lecteur
// recopie, sans l'appeler, une formule du jeu ; la ligne « Miroir de : » nomme laquelle. Rien ici
// ne touche a src/Blaze/.
internal static partial class Bestiaire
{
    /// <summary>Un point dans l'espace des sommets du jeu (x a droite, y vers le bas, z).</summary>
    private readonly record struct V3(float X, float Y, float Z);

    /// <summary>Un triangle texture : trois sommets, trois coordonnees de texture (texels 0..255), un materiau.</summary>
    private sealed class Tri
    {
        public V3 A, B, C;
        public float Ua, Va, Ub, Vb, Uc, Vc;
        public int Mat;
    }

    /// <summary>Un materiau : une page de texture et une palette, ou une couleur unie, et la semi-transparence.</summary>
    private readonly record struct Materiau(ushort TPage, ushort Clut, bool Texture, bool Semi);

    private sealed class Instance
    {
        public int Modele, Section, Rang;
        public ushort Drapeaux;        // le mot +0x1e de l'enregistrement : decalage de palette
        public float[] R = new float[9];
        public V3 T;
        public int MeshCle;            // index dans Carte.MaillagesDecor
    }

    private sealed class Poly
    {
        public V3[] P = [];
        public bool Sol;
        public int Genre, Morceau;
    }

    private sealed class Repere
    {
        public string Genre = "", Code = "", Nom = "";
        public V3 P;
        public Dictionary<string, object> Infos = new();
    }

    private sealed class Carte
    {
        public int Zone, SousZone;
        public string NomZone = "", Lieu = "";
        public readonly Vram Vram = new();
        public readonly List<Materiau> Materiaux = new();
        public readonly Dictionary<Materiau, int> IndexMateriau = new();
        public readonly List<Tri> Affichage = new();
        public readonly List<List<Tri>> Modeles = new();               // modeles de decor, en local, materiau sans palette
        public readonly List<(uint Attr, Blob Corps)> ModelesBruts = new();
        public readonly Dictionary<(int Modele, ushort Drapeaux), List<Tri>> MaillagesDecor = new();
        public readonly List<(int Modele, ushort Drapeaux)> ClesDecor = new();
        public readonly List<Instance> Instances = new();
        public readonly List<Poly> Collision = new();
        public readonly List<Repere> Reperes = new();
        public readonly List<string> Avertissements = new();
        public int IndicesHors, FacesSansPage, MorceauxCollision, MorceauxAffichage;

        public int Mat(Materiau m)
        {
            if (!IndexMateriau.TryGetValue(m, out int i)) { i = Materiaux.Count; Materiaux.Add(m); IndexMateriau[m] = i; }
            return i;
        }
    }

    /// <summary>
    /// Les noms des zones, tels que le jeu les affiche (releves dans les donnees et consignes dans
    /// <c>Conception/06-Monde.md</c>). Au-dela, la zone garde son numero seul.
    /// </summary>
    private static readonly string[] NomsDeZone =
    [
        "L'auberge", "La mine abandonnée", "La montagne du dragon de feu", "La forêt", "La vieille tour",
        "Le labyrinthe des morts", "Le château de l'immortel", "Les ruines", "La vallée d'argent",
        "Les ruines dans le lac", "La grotte scellée",
    ];

    internal static string NomDeZone(int z) => z < NomsDeZone.Length ? NomsDeZone[z] : $"Zone {z}";

    private static Carte LireCarte(byte[] archive, Blob a, int z, int s)
    {
        var carte = new Carte { Zone = z, SousZone = s, NomZone = NomDeZone(z) };
        carte.Lieu = PlaceNames(a).FirstOrDefault() ?? "";

        // ---- les textures -------------------------------------------------------------------
        // Miroir de : LoadWorldMapSubArea (Screens/State03/WorldMap.cs), boucle +0x10 — une entree
        // de 8 octets par texture, le second dword portant les trois octets que DecodeResourceChunk
        // lit ; l'emplacement i de la table de decodage recoit la i-eme.
        for (int e = a.I32(0x10); a.In(e, 8) && a.I32(e) != 0; e += 8)
        {
            var t = LoadEntryRaw(archive, a, a.I32(e));
            uint meta = (uint)a.I32(e + 4);
            if (!carte.Vram.LoadTim(t.Data, (byte)meta, (byte)(meta >> 8), (byte)(meta >> 16)))
                carte.Avertissements.Add($"texture {carte.Vram.Slots.Count - 1} : ce n'est pas une image reconnue");
        }

        // ---- les quatre listes de morceaux ----------------------------------------------------
        // Miroir de : LoadWorldMapSubArea, boucle +0x14..+0x20 (charges bruts, puis RelocateModelEntry :
        // les pointeurs du morceau sont relatifs a son debut, et +0x3c recoit le dword `entree + 4`).
        // Liste 0 : la collision (TryPlaceEntity lit le morceau 0). Listes 1 et 2 : balayage de la
        // grille de cellules (FUN_0051fa40, FUN_00521000). Liste 3 : faces a la file (FUN_00522140).
        int morceaux = 0;
        for (int list = 0; list < 4; list++)
        {
            int k = 0;
            for (int e = a.I32(0x14 + list * 4); a.In(e, 8) && a.I32(e) != 0; e += 8, k++, morceaux++)
            {
                var c = LoadEntryRaw(archive, a, a.I32(e));
                if (c.Len < 0x40) continue;
                uint attr = (uint)a.I32(e + 4);
                if (list == 0) { LireCollision(carte, c, k); carte.MorceauxCollision++; }
                else { LireMorceau(carte, c, attr, cellules: list != 3); carte.MorceauxAffichage++; }
            }
        }

        // ---- les modeles de decor -------------------------------------------------------------
        // Miroir de : LoadWorldMapSubArea, boucle +0x24 — seuls les modeles du DERNIER paquet entrent
        // dans la table des morceaux, a la suite des quatre listes ; RelocateModelBundle fusionne
        // les bits de page de l'entree de liste dans chaque descripteur.
        int last = 0;
        for (int e = a.I32(0x24); a.In(e, 8) && a.I32(e) != 0; e += 8) last = e;
        if (last != 0)
        {
            var b = LoadEntryRaw(archive, a, a.I32(last));
            uint pageBits = (uint)a.I32(last + 4);
            int n = b.Len >= 4 ? b.I32(0) : 0;
            for (int j = 0; j < n && b.In((n + 1 + j) * 4, 4); j++)
            {
                int off = b.I32((1 + j) * 4) + n * 8 + 4;
                uint attr = (uint)b.I32((n + 1 + j) * 4) | pageBits;
                if (!b.In(off, 0x40)) { carte.ModelesBruts.Add((attr, new Blob([]))); continue; }
                carte.ModelesBruts.Add((attr, new Blob(b.Data[off..])));
            }
        }

        LirePlacements(carte, a, z, s);
        return carte;
    }

    /// <summary>
    /// Miroir de : TryPlaceEntity (FUN_005330d0) — faces de 16 octets : indices i0 = mot0 bas,
    /// i2 = mot0 haut, i1 = mot1 bas, i3 = mot1 haut (si l'attribut porte le bit 0) ; la matiere en
    /// +8 ; sol quand le short <c>matieres + 2 + mat * 8</c> est negatif ; le genre en +0xe. Le
    /// polygone se parcourt i0, i2, i1, i3 — l'ordre des tests de cote du jeu.
    /// </summary>
    private static void LireCollision(Carte carte, Blob c, int morceau)
    {
        int nv = c.U16(8);
        int count = c.U16(4), faces = c.I32(0x28), materials = c.I32(0x24);
        for (int f = 0; f < count; f++)
        {
            int fo = faces + f * 16;
            if (!c.In(fo, 16)) break;
            uint w0 = (uint)c.I32(fo), w1 = (uint)c.I32(fo + 4), attr = (uint)c.I32(fo + 12);
            int mat = c.U16(fo + 8);
            var idx = new List<int> { (int)(w0 & 0xffff), (int)(w0 >> 16), (int)(w1 & 0xffff) };
            if ((attr & 1) != 0) idx.Add((int)(w1 >> 16));
            if (idx.Any(i => i >= nv || !c.In(0x40 + i * 8, 6))) { carte.IndicesHors++; continue; }
            carte.Collision.Add(new Poly
            {
                P = idx.Select(i => Sommet(c, i)).ToArray(),
                Sol = c.In(materials + 2 + mat * 8, 2) && c.I16(materials + 2 + mat * 8) < 0,
                Genre = c.U8(fo + 14),
                Morceau = morceau,
            });
        }
    }

    private static V3 Sommet(Blob c, int i) => new(c.I16(0x40 + i * 8), c.I16(0x40 + i * 8 + 2), c.I16(0x40 + i * 8 + 4));

    /// <summary>
    /// Un morceau d'affichage. Miroir de : DrawChunk (Engine/Model3d.cs) et ses deux emetteurs —
    /// faces de 0x20 octets, sommets a +0x40, placement identite (les sommets sont deja dans le
    /// monde). Avec la grille (+0x34), chaque cellule donne ses indices de face apres en avoir saute
    /// les quatre premiers comptes ; sans, les quatre seaux suivent les quatre seaux sautes.
    /// </summary>
    private static void LireMorceau(Carte carte, Blob c, uint attr, bool cellules)
    {
        int faces = c.I32(0x28);
        int grid = cellules ? c.I32(0x34) : 0;
        var (tpageWord, clutWord, ok) = MotsDePage(carte, attr);
        if (!ok) carte.FacesSansPage++;

        if (grid != 0 && c.In(grid, 0x1000))
        {
            var vues = new HashSet<int>();
            for (int i = 0; i < 0x400; i++)
            {
                int cell = grid + c.I32(grid + i * 4);
                if (!c.In(cell, 8)) continue;
                uint sk = (uint)c.I32(cell), bk = (uint)c.I32(cell + 4);
                if ((sk | bk) == 0) continue;
                int skipped = (int)((sk & 0xff) + ((sk >> 8) & 0xff) + ((sk >> 16) & 0xff) + (sk >> 24));
                int ip = cell + 8 + skipped * 2;
                for (int bucket = 0; bucket < 4; bucket++)
                    for (int n = (int)((bk >> (bucket * 8)) & 0xff); n > 0; n--, ip += 2)
                    {
                        if (!c.In(ip, 2)) break;
                        int index = c.U16(ip);
                        if (vues.Add(index)) FaceDeMorceau(carte, c, faces + index * 0x20, (bucket & 1) != 0, tpageWord, clutWord);
                    }
            }
            return;
        }

        int skip = c.U16(0x10) + c.U16(0x12) + c.U16(0x14) + c.U16(0x16);
        int fo = faces + skip * 0x20;
        for (int bucket = 0; bucket < 4; bucket++)
            for (int n = c.U16(0x18 + bucket * 2); n > 0; n--, fo += 0x20)
                FaceDeMorceau(carte, c, fo, (bucket & 1) != 0, tpageWord, clutWord);
    }

    /// <summary>Miroir de : DrawChunk — <c>slot = table[(attr &gt;&gt; 16) &amp; 0xff]</c>, le mot de palette
    /// en est la moitie haute, le mot de page la moitie basse avec le bit 0x800.</summary>
    private static (uint TPageWord, uint ClutWord, bool Ok) MotsDePage(Carte carte, uint attr)
    {
        int slot = (int)((attr >> 16) & 0xff);
        if (slot >= carte.Vram.Slots.Count) return (0, 0, false);
        var (tp, cl) = carte.Vram.Slots[slot];
        uint packed = (uint)cl << 16 | tp;
        return (((packed | 0x800) & 0xffff) << 16, packed & 0xffff0000, true);
    }

    /// <summary>Miroir de : EmitChunkTriangles / EmitChunkQuads — indices i0 | i1 &lt;&lt; 16, i2 | i3 &lt;&lt; 16 ;
    /// UV de v0/v1 dans le mot 4, de v2/v3 dans le mot 5 ; page et palette corrigees par le mot 7.
    /// Le quadrilatere part au GPU dans l'ordre v1, v0, v2, v3.</summary>
    private static void FaceDeMorceau(Carte carte, Blob c, int fo, bool quad, uint tpageWord, uint clutWord)
    {
        if (!c.In(fo, 0x20)) { carte.IndicesHors++; return; }
        int nv = c.U16(8);
        uint w0 = (uint)c.I32(fo), w1 = (uint)c.I32(fo + 4), uvA = (uint)c.I32(fo + 16), uvB = (uint)c.I32(fo + 20);
        uint fa = (uint)c.I32(fo + 28);
        int[] i = [(int)(w0 & 0xffff), (int)(w0 >> 16), (int)(w1 & 0xffff), (int)(w1 >> 16)];
        int n = quad ? 4 : 3;
        for (int k = 0; k < n; k++) if (i[k] >= nv || !c.In(0x40 + i[k] * 8, 6)) { carte.IndicesHors++; return; }

        uint code = fa >> 24;
        ushort clut = (ushort)((((fa & 0xfcf0) * 0x1000) + clutWord) >> 16);
        ushort tpage = (ushort)(((fa & 0xef0000) + tpageWord) >> 16);
        var mat = carte.Mat(new Materiau(tpage, clut, (code & 0x04) != 0, (code & 0x02) != 0));
        uint[] uv = [uvA & 0xffff, uvA >> 16, uvB & 0xffff, uvB >> 16];
        var p = i.Take(n).Select(k => Sommet(c, k)).ToArray();

        if (!quad) carte.Affichage.Add(MakeTri(p[0], p[1], p[2], uv[0], uv[1], uv[2], mat));
        else
        {
            carte.Affichage.Add(MakeTri(p[1], p[0], p[2], uv[1], uv[0], uv[2], mat));
            carte.Affichage.Add(MakeTri(p[0], p[2], p[3], uv[0], uv[2], uv[3], mat));
        }
    }

    private static Tri MakeTri(V3 a, V3 b, V3 c, uint ua, uint ub, uint uc, int mat) => new()
    {
        A = a, B = b, C = c,
        Ua = ua & 0xff, Va = (ua >> 8) & 0xff, Ub = ub & 0xff, Vb = (ub >> 8) & 0xff, Uc = uc & 0xff, Vc = (uc >> 8) & 0xff,
        Mat = mat,
    };

    /// <summary>
    /// Un modele de decor pour une palette donnee. Miroir de : DrawStaticModel (FUN_0051c420) —
    /// faces de 16 octets apres les quatre seaux sautes : quatre indices sur un octet, UV de v0/v1
    /// dans le mot 2, de v2/v3 dans le mot 3 ; page = moitie basse de l'emplacement | (attr &amp; 0xff) &lt;&lt; 5 ;
    /// palette = moitie haute + le decalage que porte le mot <c>+0x0a</c> de l'entite ; semi-transparence
    /// par le bit 0x200 de l'attribut. Et AdjustModelTexturePages a deja ajoute
    /// <c>(attr &gt;&gt; 8) &amp; 0xc0</c> au v de chaque coordonnee.
    /// </summary>
    private static List<Tri> MaillageDecor(Carte carte, int modele, ushort drapeaux)
    {
        if (carte.MaillagesDecor.TryGetValue((modele, drapeaux), out var deja)) return deja;
        var tris = new List<Tri>();
        carte.MaillagesDecor[(modele, drapeaux)] = tris;
        carte.ClesDecor.Add((modele, drapeaux));
        var (attr, m) = carte.ModelesBruts[modele];
        if (m.Len < 0x40) return tris;

        int slot = (int)((attr >> 16) & 0xff);
        if (slot >= carte.Vram.Slots.Count) { carte.FacesSansPage++; return tris; }
        var (tp, cl) = carte.Vram.Slots[slot];
        ushort tpage = (ushort)(tp | ((attr & 0xff) << 5));
        ushort clut = (ushort)(cl + (((ushort)((short)drapeaux >> 2)) & 0xffc0) + (drapeaux & 0xff));
        int mat = carte.Mat(new Materiau(tpage, clut, true, (attr & 0x200) != 0));
        uint adjust = (attr >> 8) & 0xc0;

        int nv = m.U16(8);
        int skip = m.U16(0x10) + m.U16(0x12) + m.U16(0x14) + m.U16(0x16);
        int ro = m.I32(0x28) + skip * 0x10;
        for (int bucket = 0; bucket < 4; bucket++)
            for (int n = m.U16(0x18 + bucket * 2); n > 0; n--, ro += 0x10)
            {
                if (!m.In(ro, 0x10)) { carte.IndicesHors++; continue; }
                bool quad = (bucket & 1) != 0;
                uint packed = (uint)m.I32(ro), r2 = (uint)m.I32(ro + 8), r3 = (uint)m.I32(ro + 12);
                int[] i = [(int)(packed & 0xff), (int)((packed >> 8) & 0xff), (int)((packed >> 16) & 0xff), (int)(packed >> 24)];
                int k = quad ? 4 : 3;
                if (i.Take(k).Any(x => x >= nv || !m.In(0x40 + x * 8, 6))) { carte.IndicesHors++; continue; }
                uint V(uint w) => (w & 0xff) | ((((w >> 8) + adjust) & 0xff) << 8);
                uint[] uv = [V(r2 & 0xffff), V(r2 >> 16), V(r3 & 0xffff), V(r3 >> 16)];
                var p = i.Take(k).Select(x => Sommet(m, x)).ToArray();
                if (!quad) tris.Add(MakeTri(p[0], p[1], p[2], uv[0], uv[1], uv[2], mat));
                else
                {
                    tris.Add(MakeTri(p[1], p[0], p[2], uv[1], uv[0], uv[2], mat));
                    tris.Add(MakeTri(p[0], p[2], p[3], uv[0], uv[2], uv[3], mat));
                }
            }
        return tris;
    }

    /// <summary>Miroir de : RotMatrix (Platform/Winsys3d.cs) — angles sur 4096, matrice ligne par ligne.</summary>
    internal static float[] Rotation(int ax, int ay, int az)
    {
        static (double s, double c) SC(int a) { double r = (a & 0xfff) * Math.PI * 2 / 4096; return (Math.Sin(r), Math.Cos(r)); }
        var (sx, cx) = SC(ax); var (sy, cy) = SC(ay); var (sz, cz) = SC(az);
        return
        [
            (float)(cy * cz), (float)(-sz * cy), (float)sy,
            (float)(sz * cx + sx * sy * cz), (float)(-sx * sy * sz + cx * cz), (float)(-sx * cy),
            (float)(sx * sz - cx * sy * cz), (float)(sx * cz + cx * sy * sz), (float)(cx * cy),
        ];
    }

    private static V3 Appliquer(Instance ins, V3 v)
    {
        var r = ins.R;
        return new V3(r[0] * v.X + r[1] * v.Y + r[2] * v.Z + ins.T.X,
                      r[3] * v.X + r[4] * v.Y + r[5] * v.Z + ins.T.Y,
                      r[6] * v.X + r[7] * v.Y + r[8] * v.Z + ins.T.Z);
    }

    /// <summary>
    /// Les placements : formations (+0x34), decor (+0x30, enregistrements de 0x20 octets que
    /// SpawnSubAreaEntities lit), points d'entree (+0x40). La position d'une entite est en y vers le
    /// HAUT : DrawStaticModel et TryPlaceEntity prennent son oppose dans l'espace des sommets.
    /// </summary>
    private static void LirePlacements(Carte carte, Blob a, int z, int s)
    {
        // Les noms d'espece : espece (+0x4c, 8 octets) -> fiche (+0x98, 0x60 octets, octet +4 de l'espece).
        int speciesTable = a.In(0x4c, 4) ? a.I32(0x4c) : 0;
        int modelTable = a.In(0x98, 4) ? a.I32(0x98) : 0;
        string NomEspece(int sp)
        {
            int so = speciesTable + sp * 8;
            if (speciesTable == 0 || !a.In(so, 8) || modelTable == 0) return "";
            int mo = modelTable + a.U8(so + 4) * 0x60;
            return a.In(mo, 0x60) ? Name(a, mo) : "";
        }

        int formationTable = a.In(0x34, 4) ? a.I32(0x34) : 0;
        if (formationTable != 0 && a.In(formationTable, 4))
        {
            int tableEnd = RegionEnd(a, formationTable);
            for (int fp = formationTable; fp + 4 <= tableEnd; fp += 4)
            {
                int f = (fp - formationTable) / 4, rec = a.I32(fp);
                if (rec == 0 || !a.In(rec, 0x20)) continue;
                for (int r = 0; r < 64; r++)
                {
                    int ro = rec + r * 0x20;
                    if (!a.In(ro, 0x20) || a.I32(ro) == -1) break;
                    int sp = a.U8(ro);
                    string nom = NomEspece(sp);
                    carte.Reperes.Add(new Repere
                    {
                        Genre = "monstre", Code = $"M{f}", Nom = nom.Length > 0 ? nom : $"espèce {sp}",
                        P = new V3(a.I16(ro + 4), -a.I16(ro + 6), a.I16(ro + 8)),
                        Infos = { ["formation"] = f, ["rang"] = r, ["espece"] = sp, ["orientation"] = a.I16(ro + 0x0c) },
                    });
                }
            }
        }

        var coffres = CoffresConnus(z, s);
        var gestionnaires = DecorConnu(z, s);
        int lists = a.In(0x30, 4) ? a.I32(0x30) : 0;
        if (lists != 0 && a.In(lists, 4))
        {
            int listEnd = RegionEnd(a, lists);
            int baseModeles = 0;
            for (int lp = lists; lp + 4 <= listEnd; lp += 4)
            {
                int section = (lp - lists) / 4, rec = a.I32(lp);
                if (rec == 0 || !a.In(rec, 0x20)) continue;
                int recEnd = RegionEnd(a, rec);
                for (int r = 0; r < 128; r++)
                {
                    int ro = rec + r * 0x20;
                    if (!a.In(ro, 0x20) || ro + 0x20 > recEnd || a.I32(ro) == -1) break;
                    int genre = a.U8(ro) & 3, modele = a.U8(ro + 0x1c), sousType = a.U8(ro + 0x1d);
                    ushort drapeaux = a.U16(ro + 0x1e);
                    int x = a.I16(ro + 4), y = a.I16(ro + 6), zz = a.I16(ro + 8);
                    var rep = new Repere
                    {
                        Genre = "decor", Code = $"D{section}.{r}", P = new V3(x, -y, zz),
                        Nom = genre == 0 ? "effet" : $"modèle {modele}",
                        Infos = { ["section"] = section, ["rang"] = r, ["sousType"] = sousType,
                                  ["forme"] = genre == 0 ? "particule" : genre == 1 ? "modèle" : "modèle trié par face" },
                    };
                    if (gestionnaires.TryGetValue((section, r), out string? g)) rep.Infos["gestionnaire"] = g;
                    if (coffres.TryGetValue((section, r), out var co))
                    {
                        rep.Genre = "coffre"; rep.Code = $"C{co.Id}"; rep.Nom = $"coffre {co.Id}";
                        rep.Infos["coffre"] = co.Id;
                        rep.Infos["contenu"] = co.Contenu;
                    }
                    carte.Reperes.Add(rep);

                    if (genre != 0)
                    {
                        if (modele + baseModeles >= carte.ModelesBruts.Count)
                        {
                            carte.Avertissements.Add($"décor {section}.{r} : modèle {modele} absent du paquet");
                            continue;
                        }
                        var ins = new Instance
                        {
                            Modele = modele, Section = section, Rang = r, Drapeaux = drapeaux,
                            R = Rotation(a.I16(ro + 0x0a), a.I16(ro + 0x0c), a.I16(ro + 0x0e)),
                            T = new V3(x, -y, zz),
                        };
                        MaillageDecor(carte, modele, drapeaux);
                        ins.MeshCle = carte.ClesDecor.IndexOf((modele, drapeaux));
                        carte.Instances.Add(ins);
                    }
                }
            }
        }

        int table = a.In(0x40, 4) ? a.I32(0x40) : 0;
        if (table != 0)
        {
            int fin = RegionEnd(a, table);
            for (int n = 0; a.In(table + n * 0x20, 0x20) && table + (n + 1) * 0x20 <= fin; n++)
            {
                int o = table + n * 0x20;
                bool imposee = a.I32(o + 0x10) != -1 || a.I32(o + 0x14) != -1;
                var rep = new Repere
                {
                    Genre = "entree", Code = $"E{n}", Nom = $"entrée {n}",
                    P = new V3(a.I16(o + 4), imposee ? -a.I16(o + 6) : float.NaN, a.I16(o + 8)),
                    Infos = { ["entree"] = n },
                };
                carte.Reperes.Add(rep);
            }
        }
    }

    // ---- les coffres et gestionnaires deja releves par `bestiaire` --------------------------------
    // Identifier un coffre demande Blaze.exe (la table de gestionnaires par sous-zone). L'exporteur ne
    // l'exige pas : il relit ce que `bestiaire` a deja ecrit dans Documentation/Reference/Bestiaire/.

    private static Dictionary<(int, int, int, int), (int Id, List<string> Contenu)>? s_coffres;
    private static Dictionary<(int, int, int, int), string>? s_decor;

    private static string? DossierBestiaire()
    {
        for (var d = new DirectoryInfo(AppContext.BaseDirectory); d != null; d = d.Parent)
            if (File.Exists(Path.Combine(d.FullName, "OpenForesia.sln")))
                return Path.Combine(d.FullName, "Documentation", "Reference", "Bestiaire");
        return null;
    }

    private static List<string> CsvChamps(string ligne)
    {
        var r = new List<string>();
        var sb = new System.Text.StringBuilder();
        bool q = false;
        for (int i = 0; i < ligne.Length; i++)
        {
            char ch = ligne[i];
            if (q) { if (ch == '"') { if (i + 1 < ligne.Length && ligne[i + 1] == '"') { sb.Append('"'); i++; } else q = false; } else sb.Append(ch); }
            else if (ch == '"') q = true;
            else if (ch == ',') { r.Add(sb.ToString()); sb.Clear(); }
            else sb.Append(ch);
        }
        r.Add(sb.ToString());
        return r;
    }

    private static Dictionary<(int, int), (int Id, List<string> Contenu)> CoffresConnus(int z, int s)
    {
        if (s_coffres == null)
        {
            s_coffres = new();
            string? dir = DossierBestiaire();
            string path = dir == null ? "" : Path.Combine(dir, "coffres.csv");
            if (File.Exists(path))
            {
                var lignes = File.ReadAllLines(path);
                var h = CsvChamps(lignes[0]);
                int Col(string n) => h.IndexOf(n);
                foreach (var l in lignes.Skip(1))
                {
                    var c = CsvChamps(l);
                    if (c.Count < h.Count) continue;
                    var key = (int.Parse(c[0]), int.Parse(c[1]), int.Parse(c[Col("liste")]), int.Parse(c[Col("index")]));
                    if (!s_coffres.TryGetValue(key, out var v))
                    {
                        v = (int.TryParse(c[Col("coffre_id")], out int id) ? id : -1, new List<string>());
                        s_coffres[key] = v;
                    }
                    string objet = c[Col("objet")];
                    string pct = c[Col("pourcent_plage_100")];
                    if (objet.Length > 0) v.Contenu.Add(objet + "	" + pct);
                }
            }
        }
        return s_coffres.Where(kv => kv.Key.Item1 == z && kv.Key.Item2 == s)
                        .ToDictionary(kv => (kv.Key.Item3, kv.Key.Item4), kv => (kv.Value.Id, Regrouper(kv.Value.Contenu)));
    }

    /// <summary>Les rangs de tirage qui donnent le meme objet se cumulent : huit fois la meme aiguille font 100 %.</summary>
    private static List<string> Regrouper(List<string> brut) =>
        brut.Select(l => l.Split('	'))
            .GroupBy(p => p[0])
            .Select(g => g.All(p => double.TryParse(p[1], System.Globalization.CultureInfo.InvariantCulture, out _))
                ? $"{g.Key} ({g.Sum(p => double.Parse(p[1], System.Globalization.CultureInfo.InvariantCulture)).ToString("0.#", System.Globalization.CultureInfo.InvariantCulture)} %)"
                : g.Key)
            .ToList();

    private static Dictionary<(int, int), string> DecorConnu(int z, int s)
    {
        if (s_decor == null)
        {
            s_decor = new();
            string? dir = DossierBestiaire();
            string path = dir == null ? "" : Path.Combine(dir, "decor.csv");
            if (File.Exists(path))
                foreach (var l in File.ReadLines(path).Skip(1))
                {
                    var c = l.Split(',');
                    if (c.Length < 6) continue;
                    s_decor[(int.Parse(c[0]), int.Parse(c[1]), int.Parse(c[2]), int.Parse(c[3]))] = c[5];
                }
        }
        return s_decor.Where(kv => kv.Key.Item1 == z && kv.Key.Item2 == s)
                      .ToDictionary(kv => (kv.Key.Item3, kv.Key.Item4), kv => kv.Value);
    }
}
