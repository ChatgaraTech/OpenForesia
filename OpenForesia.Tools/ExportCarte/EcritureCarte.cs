using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ExportCarte;

// L'ecriture d'une sous-zone lue par LectureCarte.cs : textures PNG, modele glTF 2.0, placements JSON,
// vue du dessus (deux PNG) et page de visite. Le verbe `exporter-carte` est en bas.
internal static partial class Bestiaire
{
    /// <summary>
    /// L'echelle de l'export : une unite du jeu = 1 cm (0,01 m en glTF). C'est un choix, pas une mesure
    /// du jeu ; il est ecrit dans placements.json pour qu'on puisse le corriger d'un seul facteur.
    /// </summary>
    private const float Echelle = 0.01f;

    /// <summary>L'espace des sommets du jeu (y vers le bas) vers glTF (y vers le haut) : une rotation
    /// d'un demi-tour autour de x, sans miroir.</summary>
    private static (float X, float Y, float Z) G(V3 v) => (v.X * Echelle, -v.Y * Echelle, -v.Z * Echelle);

    private static readonly JsonSerializerOptions s_json = new() { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

    private static string NomDeFichier(string s)
    {
        foreach (char c in Path.GetInvalidFileNameChars()) s = s.Replace(c, '-');
        return s.Trim().TrimEnd('.');
    }

    /// <summary>Les triangles du monde : la carte, plus chaque instance de decor placee.</summary>
    private static List<Tri> TrianglesDuMonde(Carte carte)
    {
        var tous = new List<Tri>(carte.Affichage);
        foreach (var ins in carte.Instances)
            foreach (var t in carte.MaillagesDecor[carte.ClesDecor[ins.MeshCle]])
                tous.Add(new Tri
                {
                    A = Appliquer(ins, t.A), B = Appliquer(ins, t.B), C = Appliquer(ins, t.C),
                    Ua = t.Ua, Va = t.Va, Ub = t.Ub, Vb = t.Vb, Uc = t.Uc, Vc = t.Vc, Mat = t.Mat,
                });
        return tous;
    }

    private sealed class Textures
    {
        public readonly Dictionary<(ushort, ushort), int> Index = new();
        public readonly List<byte[]> Rgba = new();
        public readonly List<(ushort TPage, ushort Clut)> Cles = new();

        public int Pour(Carte carte, Materiau m)
        {
            if (!m.Texture) return -1;
            var key = ((ushort)(m.TPage & 0x19f), m.Clut);
            if (Index.TryGetValue(key, out int i)) return i;
            i = Rgba.Count;
            Rgba.Add(carte.Vram.Page(key.Item1, key.Item2, out _));
            Cles.Add(key);
            Index[key] = i;
            return i;
        }
    }

    private static void EcrireCarte(Carte carte, string dossier, StringBuilder resume)
    {
        Directory.CreateDirectory(Path.Combine(dossier, "textures"));
        var tex = new Textures();
        var texDeMat = carte.Materiaux.Select(m => tex.Pour(carte, m)).ToArray();
        for (int i = 0; i < tex.Rgba.Count; i++)
            File.WriteAllBytes(Path.Combine(dossier, "textures", $"t{i:D2}.png"), Png.Encode(tex.Rgba[i], 256, 256));

        EcrireGltf(carte, dossier, texDeMat, tex.Rgba.Count);
        var monde = TrianglesDuMonde(carte);
        var placements = EcrirePlacements(carte, dossier);
        var (inconnus, cadre) = EcrireVueDuDessus(carte, dossier, monde, texDeMat, tex);
        EcrirePageVisite(carte, dossier, monde, texDeMat, tex);

        resume.AppendLine(
            $"{carte.Zone,2} / {carte.SousZone,2}  {carte.Affichage.Count,6} triangles  {carte.Instances.Count,4} décors  "
            + $"{carte.Collision.Count,5} faces de collision  {tex.Rgba.Count,3} textures  {placements,4} repères"
            + (carte.IndicesHors > 0 ? $"  {carte.IndicesHors} faces illisibles" : "")
            + (inconnus > 0 ? $"  {inconnus} pixels lus hors des images chargées" : "")
            + (carte.Lieu.Length > 0 ? $"  — {carte.Lieu}" : ""));
        foreach (var w in carte.Avertissements.Distinct()) resume.AppendLine("        " + w);
        _ = cadre;
    }

    // ============================================================================================
    // glTF 2.0
    // ============================================================================================

    private sealed class Tampon
    {
        public readonly MemoryStream Bin = new();
        public readonly JsonArray Vues = new(), Accesseurs = new();

        public int Ajouter(float[] data, int comps, bool minmax)
        {
            while (Bin.Length % 4 != 0) Bin.WriteByte(0);
            long off = Bin.Length;
            var bytes = new byte[data.Length * 4];
            Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);
            Bin.Write(bytes);
            Vues.Add(new JsonObject { ["buffer"] = 0, ["byteOffset"] = off, ["byteLength"] = bytes.Length });
            var acc = new JsonObject
            {
                ["bufferView"] = Vues.Count - 1, ["componentType"] = 5126, ["count"] = data.Length / comps,
                ["type"] = comps switch { 1 => "SCALAR", 2 => "VEC2", 3 => "VEC3", _ => "VEC4" },
            };
            if (minmax)
            {
                var mn = Enumerable.Repeat(float.MaxValue, comps).ToArray();
                var mx = Enumerable.Repeat(float.MinValue, comps).ToArray();
                for (int i = 0; i < data.Length; i++) { mn[i % comps] = Math.Min(mn[i % comps], data[i]); mx[i % comps] = Math.Max(mx[i % comps], data[i]); }
                acc["min"] = new JsonArray(mn.Select(v => (JsonNode)v).ToArray());
                acc["max"] = new JsonArray(mx.Select(v => (JsonNode)v).ToArray());
            }
            Accesseurs.Add(acc);
            return Accesseurs.Count - 1;
        }
    }

    private static JsonArray Primitives(IEnumerable<Tri> tris, int[] matGltf, Tampon t)
    {
        var prims = new JsonArray();
        foreach (var g in tris.GroupBy(x => matGltf[x.Mat]).OrderBy(g => g.Key))
        {
            var pos = new List<float>(); var uv = new List<float>();
            foreach (var tr in g)
                foreach (var (p, u, v) in new[] { (tr.A, tr.Ua, tr.Va), (tr.B, tr.Ub, tr.Vb), (tr.C, tr.Uc, tr.Vc) })
                {
                    var q = G(p); pos.Add(q.X); pos.Add(q.Y); pos.Add(q.Z);
                    uv.Add((u + 0.5f) / 256f); uv.Add((v + 0.5f) / 256f);
                }
            prims.Add(new JsonObject
            {
                ["attributes"] = new JsonObject { ["POSITION"] = t.Ajouter(pos.ToArray(), 3, true), ["TEXCOORD_0"] = t.Ajouter(uv.ToArray(), 2, false) },
                ["material"] = g.Key,
            });
        }
        return prims;
    }

    private static void EcrireGltf(Carte carte, string dossier, int[] texDeMat, int nTex)
    {
        var t = new Tampon();
        var materials = new JsonArray();
        // Un materiau glTF par couple (texture, semi-transparence) ; une couleur unie pour les faces non texturees.
        var cleGltf = new Dictionary<(int, bool), int>();
        var matGltf = new int[carte.Materiaux.Count];
        for (int i = 0; i < carte.Materiaux.Count; i++)
        {
            var key = (texDeMat[i], carte.Materiaux[i].Semi);
            if (!cleGltf.TryGetValue(key, out int g))
            {
                g = materials.Count;
                var pbr = new JsonObject { ["metallicFactor"] = 0, ["roughnessFactor"] = 1 };
                if (key.Item1 >= 0) pbr["baseColorTexture"] = new JsonObject { ["index"] = key.Item1 };
                else pbr["baseColorFactor"] = new JsonArray(0.5, 0.5, 0.5, 1);
                if (key.Item2) pbr["baseColorFactor"] = new JsonArray(1, 1, 1, 0.5);
                var m = new JsonObject
                {
                    ["name"] = key.Item1 >= 0 ? $"t{key.Item1:D2}{(key.Item2 ? "-semi" : "")}" : "uni",
                    ["pbrMetallicRoughness"] = pbr, ["doubleSided"] = true,
                    ["alphaMode"] = key.Item2 ? "BLEND" : "MASK",
                };
                if (!key.Item2) m["alphaCutoff"] = 0.5;
                materials.Add(m);
                cleGltf[key] = g;
            }
            matGltf[i] = g;
        }

        var meshes = new JsonArray();
        var nodes = new JsonArray();
        var racine = new JsonArray();

        meshes.Add(new JsonObject { ["name"] = "affichage", ["primitives"] = Primitives(carte.Affichage, matGltf, t) });
        nodes.Add(new JsonObject { ["name"] = "affichage", ["mesh"] = 0 });
        racine.Add(0);

        // La collision : un primitif par couple (sol, genre), triangule en eventail, avec ses deux
        // proprietes en extras. Deux materiaux de couleur, pour distinguer sols et murs a l'oeil.
        int solMat = materials.Count;
        materials.Add(new JsonObject { ["name"] = "collision-sol", ["doubleSided"] = true, ["alphaMode"] = "BLEND",
            ["pbrMetallicRoughness"] = new JsonObject { ["baseColorFactor"] = new JsonArray(0.2, 0.85, 0.3, 0.5), ["metallicFactor"] = 0, ["roughnessFactor"] = 1 } });
        materials.Add(new JsonObject { ["name"] = "collision-autre", ["doubleSided"] = true, ["alphaMode"] = "BLEND",
            ["pbrMetallicRoughness"] = new JsonObject { ["baseColorFactor"] = new JsonArray(0.9, 0.3, 0.2, 0.5), ["metallicFactor"] = 0, ["roughnessFactor"] = 1 } });
        var cprims = new JsonArray();
        foreach (var g in carte.Collision.GroupBy(p => (p.Sol, p.Genre)).OrderBy(g => g.Key.Genre))
        {
            var pos = new List<float>();
            foreach (var poly in g)
                for (int k = 1; k + 1 < poly.P.Length; k++)
                    foreach (var p in new[] { poly.P[0], poly.P[k], poly.P[k + 1] })
                    { var q = G(p); pos.Add(q.X); pos.Add(q.Y); pos.Add(q.Z); }
            cprims.Add(new JsonObject
            {
                ["attributes"] = new JsonObject { ["POSITION"] = t.Ajouter(pos.ToArray(), 3, true) },
                ["material"] = g.Key.Sol ? solMat : solMat + 1,
                ["extras"] = new JsonObject { ["sol"] = g.Key.Sol, ["genre"] = g.Key.Genre },
            });
        }
        if (cprims.Count > 0)
        {
            meshes.Add(new JsonObject { ["name"] = "collision", ["primitives"] = cprims });
            nodes.Add(new JsonObject { ["name"] = "collision", ["mesh"] = meshes.Count - 1, ["extras"] = new JsonObject { ["calque"] = "collision" } });
            racine.Add(nodes.Count - 1);
        }

        // Le decor : un maillage par modele (et par palette), une instance par enregistrement place.
        var meshDecor = new int[carte.ClesDecor.Count];
        for (int i = 0; i < carte.ClesDecor.Count; i++)
        {
            var tris = carte.MaillagesDecor[carte.ClesDecor[i]];
            meshDecor[i] = -1;
            if (tris.Count == 0) continue;
            meshes.Add(new JsonObject { ["name"] = $"modele-{carte.ClesDecor[i].Modele}" + (carte.ClesDecor[i].Drapeaux != 0 ? $"-p{carte.ClesDecor[i].Drapeaux}" : ""),
                                        ["primitives"] = Primitives(tris, matGltf, t) });
            meshDecor[i] = meshes.Count - 1;
        }
        var enfants = new JsonArray();
        foreach (var ins in carte.Instances)
        {
            if (meshDecor[ins.MeshCle] < 0) continue;
            // C R C, C = diag(1, -1, -1) ; translation C t, a l'echelle. glTF range la matrice par colonnes.
            float[] c = [1, -1, -1];
            var m = new float[16];
            for (int r = 0; r < 3; r++)
                for (int k = 0; k < 3; k++) m[k * 4 + r] = c[r] * ins.R[r * 3 + k] * c[k];
            var tr = G(ins.T);
            m[12] = tr.X; m[13] = tr.Y; m[14] = tr.Z; m[15] = 1;
            nodes.Add(new JsonObject
            {
                ["name"] = $"decor-{ins.Section}.{ins.Rang}", ["mesh"] = meshDecor[ins.MeshCle],
                ["matrix"] = new JsonArray(m.Select(v => (JsonNode)v).ToArray()),
            });
            enfants.Add(nodes.Count - 1);
        }
        if (enfants.Count > 0)
        {
            nodes.Add(new JsonObject { ["name"] = "decor", ["children"] = enfants });
            racine.Add(nodes.Count - 1);
        }

        var images = new JsonArray(Enumerable.Range(0, nTex).Select(i => (JsonNode)new JsonObject { ["uri"] = $"textures/t{i:D2}.png" }).ToArray());
        var textures = new JsonArray(Enumerable.Range(0, nTex).Select(i => (JsonNode)new JsonObject { ["source"] = i, ["sampler"] = 0 }).ToArray());

        var gltf = new JsonObject
        {
            ["asset"] = new JsonObject { ["version"] = "2.0", ["generator"] = "OpenForesia.Tools exporter-carte" },
            ["scene"] = 0,
            ["scenes"] = new JsonArray(new JsonObject { ["name"] = $"{carte.NomZone} — {carte.SousZone}", ["nodes"] = racine }),
            ["nodes"] = nodes, ["meshes"] = meshes, ["materials"] = materials,
            ["buffers"] = new JsonArray(new JsonObject { ["uri"] = "carte.bin", ["byteLength"] = t.Bin.Length }),
            ["bufferViews"] = t.Vues, ["accessors"] = t.Accesseurs,
        };
        if (nTex > 0)
        {
            gltf["images"] = images; gltf["textures"] = textures;
            gltf["samplers"] = new JsonArray(new JsonObject { ["magFilter"] = 9728, ["minFilter"] = 9728 });
        }
        File.WriteAllBytes(Path.Combine(dossier, "carte.bin"), t.Bin.ToArray());
        File.WriteAllText(Path.Combine(dossier, "carte.gltf"), gltf.ToJsonString(s_json));
    }

    // ============================================================================================
    // Placements
    // ============================================================================================

    private static object? PosJson(V3 p)
    {
        var (x, y, z) = G(p);
        return float.IsNaN(p.Y) ? new { x, z } : new { x, y, z };
    }

    private static int EcrirePlacements(Carte carte, string dossier)
    {
        var doc = new
        {
            zone = carte.Zone, nomZone = carte.NomZone, sousZone = carte.SousZone, lieu = carte.Lieu,
            repere = "glTF : x à droite, y vers le haut, z vers l'avant ; une unité du jeu = 1 cm (choix de l'export, pas du jeu)",
            echelle = Echelle,
            reperes = carte.Reperes.Select(r => new Dictionary<string, object?>
            {
                ["genre"] = r.Genre, ["code"] = r.Code, ["nom"] = r.Nom, ["position"] = PosJson(r.P),
                ["positionJeu"] = float.IsNaN(r.P.Y) ? new { x = r.P.X, z = r.P.Z } : (object)new { x = r.P.X, y = -r.P.Y, z = r.P.Z },
            }.Concat(r.Infos.Select(kv => new KeyValuePair<string, object?>(kv.Key, kv.Value))).ToDictionary(kv => kv.Key, kv => kv.Value)).ToArray(),
        };
        File.WriteAllText(Path.Combine(dossier, "placements.json"), JsonSerializer.Serialize(doc, s_json));
        return carte.Reperes.Count;
    }

    // ============================================================================================
    // La vue du dessus : un rasteriseur logiciel orthographique
    // ============================================================================================

    /// <summary>
    /// Vue de dessus (depuis le haut, soit -y dans l'espace du jeu), sans miroir : x du jeu vers la
    /// droite, z du jeu vers le haut de l'image. Test alpha (texel 0x0000 transparent), tampon de
    /// profondeur (le plus haut l'emporte), faces semi-transparentes melees a 50 % en second.
    /// </summary>
    private static (int Inconnus, (float X0, float Z0, float K, int W, int H) Cadre) EcrireVueDuDessus(
        Carte carte, string dossier, List<Tri> monde, int[] texDeMat, Textures tex)
    {
        if (monde.Count == 0) return (0, (0, 0, 1, 0, 0));
        float x0 = float.MaxValue, x1 = float.MinValue, z0 = float.MaxValue, z1 = float.MinValue;
        foreach (var t in monde)
            foreach (var p in new[] { t.A, t.B, t.C })
            { x0 = Math.Min(x0, p.X); x1 = Math.Max(x1, p.X); z0 = Math.Min(z0, p.Z); z1 = Math.Max(z1, p.Z); }
        float marge = 16;
        x0 -= marge; z0 -= marge; x1 += marge; z1 += marge;
        float k = Math.Min(2f, 4096f / Math.Max(x1 - x0, z1 - z0));
        int w = Math.Max(1, (int)Math.Ceiling((x1 - x0) * k)), h = Math.Max(1, (int)Math.Ceiling((z1 - z0) * k));

        var rgba = new byte[w * h * 4];
        for (int i = 0; i < w * h; i++) { rgba[i * 4] = 24; rgba[i * 4 + 1] = 24; rgba[i * 4 + 2] = 28; rgba[i * 4 + 3] = 255; }
        var depth = new float[w * h];
        Array.Fill(depth, float.MaxValue);
        int inconnus = 0;

        void Raster(Tri t, bool semi)
        {
            var mat = carte.Materiaux[t.Mat];
            int ti = texDeMat[t.Mat];
            byte[]? page = ti >= 0 ? tex.Rgba[ti] : null;
            float ax = (t.A.X - x0) * k, ay = (z1 - t.A.Z) * k, bx = (t.B.X - x0) * k, by = (z1 - t.B.Z) * k, cx = (t.C.X - x0) * k, cy = (z1 - t.C.Z) * k;
            float area = (bx - ax) * (cy - ay) - (cx - ax) * (by - ay);
            if (Math.Abs(area) < 1e-6f) return;
            int minX = Math.Max(0, (int)Math.Floor(Math.Min(ax, Math.Min(bx, cx)))), maxX = Math.Min(w - 1, (int)Math.Ceiling(Math.Max(ax, Math.Max(bx, cx))));
            int minY = Math.Max(0, (int)Math.Floor(Math.Min(ay, Math.Min(by, cy)))), maxY = Math.Min(h - 1, (int)Math.Ceiling(Math.Max(ay, Math.Max(by, cy))));
            for (int py = minY; py <= maxY; py++)
                for (int px = minX; px <= maxX; px++)
                {
                    float sx = px + 0.5f, sy = py + 0.5f;
                    float l1 = ((cx - bx) * (sy - by) - (cy - by) * (sx - bx)) / area;
                    float l2 = ((ax - cx) * (sy - cy) - (ay - cy) * (sx - cx)) / area;
                    float l3 = 1 - l1 - l2;
                    if (l1 < 0 || l2 < 0 || l3 < 0) continue;
                    float y = l1 * t.A.Y + l2 * t.B.Y + l3 * t.C.Y;
                    int o = py * w + px;
                    if (y > depth[o]) continue;
                    byte r = 128, g = 128, b = 128;
                    if (page != null)
                    {
                        int u = Math.Clamp((int)(l1 * t.Ua + l2 * t.Ub + l3 * t.Uc), 0, 255);
                        int v = Math.Clamp((int)(l1 * t.Va + l2 * t.Vb + l3 * t.Vc), 0, 255);
                        int q = (v * 256 + u) * 4;
                        if (page[q + 3] == 0) continue;
                        r = page[q]; g = page[q + 1]; b = page[q + 2];
                        if (r == 255 && g == 0 && b == 255) inconnus++;
                    }
                    if (semi)
                    {
                        rgba[o * 4] = (byte)((rgba[o * 4] + r) / 2); rgba[o * 4 + 1] = (byte)((rgba[o * 4 + 1] + g) / 2); rgba[o * 4 + 2] = (byte)((rgba[o * 4 + 2] + b) / 2);
                    }
                    else
                    {
                        depth[o] = y;
                        rgba[o * 4] = r; rgba[o * 4 + 1] = g; rgba[o * 4 + 2] = b;
                    }
                }
            _ = mat;
        }

        foreach (var t in monde) if (!carte.Materiaux[t.Mat].Semi) Raster(t, false);
        foreach (var t in monde) if (carte.Materiaux[t.Mat].Semi) Raster(t, true);
        File.WriteAllBytes(Path.Combine(dossier, "vue-du-dessus.png"), Png.Encode(rgba, w, h));

        // La meme, annotee : monstres en rouge, coffres en jaune, entrees en cyan, decor actif en blanc.
        var ann = (byte[])rgba.Clone();
        for (int i = 0; i < w * h * 4; i += 4) { ann[i] = (byte)(ann[i] * 0.7); ann[i + 1] = (byte)(ann[i + 1] * 0.7); ann[i + 2] = (byte)(ann[i + 2] * 0.7); }
        int echelleTexte = Math.Clamp(Math.Max(w, h) / 700, 2, 6);
        var legende = new StringBuilder();
        legende.AppendLine($"{carte.NomZone} — sous-zone {carte.SousZone}{(carte.Lieu.Length > 0 ? " — " + carte.Lieu : "")}");
        legende.AppendLine("Vue de dessus, x du jeu vers la droite, z du jeu vers le haut. M : formation (monstres, ou personnages dans les lieux habités), C : coffre, E : entrée ; points blancs : décor actif.");
        legende.AppendLine();
        foreach (var r in carte.Reperes.OrderBy(r => r.Genre == "decor" ? 1 : 0))
        {
            int px = (int)((r.P.X - x0) * k), py = (int)((z1 - r.P.Z) * k);
            (byte R, byte G, byte B) col = r.Genre switch
            {
                "monstre" => (235, 60, 50), "coffre" => (250, 210, 40), "entree" => (60, 220, 240), _ => (230, 230, 230),
            };
            int rayon = (r.Genre == "decor" ? 3 : 6) * echelleTexte / 2;
            Disque(ann, w, h, px, py, rayon + 1, (0, 0, 0));
            Disque(ann, w, h, px, py, rayon, col);
            if (r.Genre != "decor")
            {
                Texte(ann, w, h, px + rayon + 3, py - 7 * echelleTexte / 2, r.Code, col, echelleTexte);
                legende.AppendLine($"{r.Code,-6} {r.Nom}" + (r.Infos.TryGetValue("contenu", out var ct) && ct is List<string> lc && lc.Count > 0 ? " — " + string.Join(", ", lc) : ""));
            }
        }
        File.WriteAllBytes(Path.Combine(dossier, "vue-du-dessus-reperes.png"), Png.Encode(ann, w, h));
        File.WriteAllText(Path.Combine(dossier, "legende.txt"), legende.ToString());
        return (inconnus, (x0, z0, k, w, h));
    }

    private static void Point(byte[] img, int w, int h, int x, int y, (byte R, byte G, byte B) c)
    {
        if ((uint)x >= w || (uint)y >= h) return;
        int o = (y * w + x) * 4;
        img[o] = c.R; img[o + 1] = c.G; img[o + 2] = c.B; img[o + 3] = 255;
    }

    private static void Disque(byte[] img, int w, int h, int cx, int cy, int r, (byte, byte, byte) c)
    {
        for (int y = -r; y <= r; y++) for (int x = -r; x <= r; x++) if (x * x + y * y <= r * r) Point(img, w, h, cx + x, cy + y, c);
    }

    // Une police 5 x 7, juste ce que les codes de repere demandent.
    private static readonly Dictionary<char, string[]> s_police = new()
    {
        ['0'] = ["01110", "10001", "10011", "10101", "11001", "10001", "01110"],
        ['1'] = ["00100", "01100", "00100", "00100", "00100", "00100", "01110"],
        ['2'] = ["01110", "10001", "00001", "00010", "00100", "01000", "11111"],
        ['3'] = ["11110", "00001", "00001", "01110", "00001", "00001", "11110"],
        ['4'] = ["00010", "00110", "01010", "10010", "11111", "00010", "00010"],
        ['5'] = ["11111", "10000", "11110", "00001", "00001", "10001", "01110"],
        ['6'] = ["00110", "01000", "10000", "11110", "10001", "10001", "01110"],
        ['7'] = ["11111", "00001", "00010", "00100", "01000", "01000", "01000"],
        ['8'] = ["01110", "10001", "10001", "01110", "10001", "10001", "01110"],
        ['9'] = ["01110", "10001", "10001", "01111", "00001", "00010", "01100"],
        ['M'] = ["10001", "11011", "10101", "10101", "10001", "10001", "10001"],
        ['C'] = ["01110", "10001", "10000", "10000", "10000", "10001", "01110"],
        ['E'] = ["11111", "10000", "10000", "11110", "10000", "10000", "11111"],
        ['D'] = ["11110", "10001", "10001", "10001", "10001", "10001", "11110"],
        ['.'] = ["00000", "00000", "00000", "00000", "00000", "01100", "01100"],
        ['-'] = ["00000", "00000", "00000", "11111", "00000", "00000", "00000"],
    };

    private static void Texte(byte[] img, int w, int h, int x, int y, string s, (byte, byte, byte) c, int e)
    {
        foreach (char ch in s)
        {
            if (s_police.TryGetValue(ch, out var g))
                for (int r = 0; r < 7; r++)
                    for (int q = 0; q < 5; q++)
                        if (g[r][q] == '1')
                            for (int dy = -1; dy <= e; dy++) for (int dx = -1; dx <= e; dx++)
                                if (dx < 0 || dy < 0 || dx == e || dy == e) Point(img, w, h, x + q * e + dx, y + r * e + dy, (0, 0, 0));
            if (g != null)
                for (int r = 0; r < 7; r++)
                    for (int q = 0; q < 5; q++)
                        if (g[r][q] == '1')
                            for (int dy = 0; dy < e; dy++) for (int dx = 0; dx < e; dx++) Point(img, w, h, x + q * e + dx, y + r * e + dy, c);
            x += 6 * e;
        }
    }

    // ============================================================================================
    // La page de visite
    // ============================================================================================

    private static string B64(float[] f)
    {
        var b = new byte[f.Length * 4];
        Buffer.BlockCopy(f, 0, b, 0, b.Length);
        return Convert.ToBase64String(b);
    }

    private static void EcrirePageVisite(Carte carte, string dossier, List<Tri> monde, int[] texDeMat, Textures tex)
    {
        var groupes = new List<object>();
        foreach (var g in monde.GroupBy(t => (texDeMat[t.Mat], carte.Materiaux[t.Mat].Semi)))
        {
            var pos = new List<float>(); var uv = new List<float>();
            foreach (var tr in g)
                foreach (var (p, u, v) in new[] { (tr.A, tr.Ua, tr.Va), (tr.B, tr.Ub, tr.Vb), (tr.C, tr.Uc, tr.Vc) })
                { var q = G(p); pos.Add(q.X); pos.Add(q.Y); pos.Add(q.Z); uv.Add((u + 0.5f) / 256f); uv.Add((v + 0.5f) / 256f); }
            groupes.Add(new { tex = g.Key.Item1, semi = g.Key.Item2, p = B64(pos.ToArray()), t = B64(uv.ToArray()) });
        }
        var collision = new List<object>();
        foreach (var g in carte.Collision.GroupBy(p => p.Sol))
        {
            var pos = new List<float>();
            foreach (var poly in g)
                for (int k = 1; k + 1 < poly.P.Length; k++)
                    foreach (var p in new[] { poly.P[0], poly.P[k], poly.P[k + 1] }) { var q = G(p); pos.Add(q.X); pos.Add(q.Y); pos.Add(q.Z); }
            collision.Add(new { sol = g.Key, p = B64(pos.ToArray()) });
        }
        // Un repere sans hauteur connue (une entree sans hauteur imposee) se pose sur la plus haute face sous lui.
        var reperes = carte.Reperes.Select(r =>
        {
            float y = float.IsNaN(r.P.Y) ? HauteurSous(monde, r.P.X, r.P.Z) : r.P.Y;
            var (x, yy, z) = G(new V3(r.P.X, y, r.P.Z));
            string detail = r.Infos.TryGetValue("contenu", out var c) && c is List<string> l && l.Count > 0 ? string.Join(", ", l) : "";
            return new { genre = r.Genre, code = r.Code, nom = r.Nom, detail, x, y = yy, z };
        }).ToArray();

        var data = new
        {
            titre = $"{carte.NomZone} — {(carte.Lieu.Length > 0 ? carte.Lieu : "sous-zone " + carte.SousZone)}",
            textures = tex.Rgba.Select(r => "data:image/png;base64," + Convert.ToBase64String(Png.Encode(r, 256, 256))).ToArray(),
            groupes, collision, reperes,
        };
        string json = JsonSerializer.Serialize(data).Replace("</", "<\\/");
        string page = PageVisite.Modele
            .Replace("@@TITRE@@", System.Net.WebUtility.HtmlEncode(data.titre))
            .Replace("@@DONNEES@@", json);
        File.WriteAllText(Path.Combine(dossier, "visite.html"), page);
    }

    private static float HauteurSous(List<Tri> monde, float x, float z)
    {
        float best = float.NaN;
        foreach (var t in monde)
        {
            float area = (t.B.X - t.A.X) * (t.C.Z - t.A.Z) - (t.C.X - t.A.X) * (t.B.Z - t.A.Z);
            if (Math.Abs(area) < 1e-3f) continue;
            float l1 = ((t.C.X - t.B.X) * (z - t.B.Z) - (t.C.Z - t.B.Z) * (x - t.B.X)) / area;
            float l2 = ((t.A.X - t.C.X) * (z - t.C.Z) - (t.A.Z - t.C.Z) * (x - t.C.X)) / area;
            float l3 = 1 - l1 - l2;
            if (l1 < 0 || l2 < 0 || l3 < 0) continue;
            float y = l1 * t.A.Y + l2 * t.B.Y + l3 * t.C.Y;
            if (float.IsNaN(best) || y < best) best = y;
        }
        return float.IsNaN(best) ? 0 : best;
    }

    // ============================================================================================
    // Le verbe
    // ============================================================================================

    /// <summary>
    /// <c>exporter-carte [dossier-jeu] [zone [sous-zone]] --sortie &lt;dossier&gt;</c> : sans zone, tout le
    /// jeu ; sans sous-zone, toute la zone. Un dossier par zone (« 03 - La forêt »), un sous-dossier
    /// par sous-zone (« 00 - La forêt aux ruines »). Refuse un dossier de sortie dans le depot.
    /// </summary>
    public static int ExporterCarte(string dossierDonnees, int? zone, int? sousZone, string sortie)
    {
        string french = Path.Combine(dossierDonnees, "French");
        if (!Directory.Exists(french)) french = dossierDonnees;
        foreach (var f in new[] { "BLAZE.ALL", "SYSTEM.INI" })
            if (!File.Exists(Path.Combine(french, f)))
            { Console.WriteLine($"Données du jeu absentes : {Path.Combine(french, f)} introuvable. Rien n'a été écrit."); return 1; }

        string full = Path.GetFullPath(sortie);
        for (var d = new DirectoryInfo(AppContext.BaseDirectory); d != null; d = d.Parent)
        {
            if (!File.Exists(Path.Combine(d.FullName, "OpenForesia.sln"))) continue;
            string repo = d.FullName.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            if ((full.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar).StartsWith(repo, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Refusé : {full} est dans le dépôt. Les cartes sont des données du jeu — choisir un dossier hors du dépôt. Rien n'a été écrit.");
                return 1;
            }
            break;
        }

        byte[] archive = File.ReadAllBytes(Path.Combine(french, "BLAZE.ALL"));
        var root = new Blob(File.ReadAllBytes(Path.Combine(french, "SYSTEM.INI")));
        root.Fixup();
        var stateDir = LoadEntry(archive, root, EntryList(root, 0x48)[3]);
        var zones = EntryList(stateDir, 0x48);
        if (zone is int zz && (zz < 0 || zz >= zones.Count))
        { Console.WriteLine($"Zone {zz} inconnue : le jeu en compte {zones.Count} (0 à {zones.Count - 1}). Rien n'a été écrit."); return 1; }

        var travail = new List<(int Z, Blob ZoneBlob, List<int> Subs)>();
        foreach (int z in zone is int one ? [one] : Enumerable.Range(0, zones.Count))
        {
            var zb = LoadEntry(archive, stateDir, zones[z]);
            travail.Add((z, zb, EntryList(zb, 0x48)));
        }
        if (sousZone is int sz && (sz < 0 || sz >= travail[0].Subs.Count))
        { Console.WriteLine($"Sous-zone {sz} inconnue dans la zone {zone} : elle en compte {travail[0].Subs.Count}. Rien n'a été écrit."); return 1; }

        var resume = new StringBuilder();
        resume.AppendLine("Export des cartes — zone / sous-zone, ce qui a été écrit");
        resume.AppendLine();
        int ok = 0, echecs = 0;
        foreach (var (z, zb, subs) in travail)
        {
            string dz = Path.Combine(full, NomDeFichier($"{z:D2} - {NomDeZone(z)}"));
            foreach (int s in sousZone is int one ? [one] : Enumerable.Range(0, subs.Count))
            {
                try
                {
                    var a = LoadEntry(archive, zb, subs[s]);
                    if (a.Len < 0x100) { resume.AppendLine($"{z,2} / {s,2}  vide — rien à exporter"); continue; }
                    var carte = LireCarte(archive, a, z, s);
                    if (carte.Affichage.Count == 0 && carte.Instances.Count == 0)
                    { resume.AppendLine($"{z,2} / {s,2}  aucune géométrie — sous-zone sans carte"); continue; }
                    string ds = Path.Combine(dz, NomDeFichier($"{s:D2}" + (carte.Lieu.Length > 0 ? $" - {carte.Lieu}" : "")));
                    EcrireCarte(carte, ds, resume);
                    ok++;
                    Console.WriteLine($"  {z:D2}/{s:D2} → {ds}");
                }
                catch (Exception ex)
                {
                    echecs++;
                    resume.AppendLine($"{z,2} / {s,2}  ÉCHEC : {ex.GetType().Name} — {ex.Message}");
                }
            }
        }
        resume.AppendLine();
        resume.AppendLine($"{ok} sous-zone(s) exportée(s), {echecs} échec(s).");
        Directory.CreateDirectory(full);
        File.WriteAllText(Path.Combine(full, "resume.txt"), resume.ToString());
        Console.WriteLine(resume.ToString());
        return echecs == 0 ? 0 : 2;
    }
}
