using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ExportCarte;

// L'ecriture des modeles animes : un dossier par modele (glTF anime, texture, donnees de la page),
// et une page index.html qui les liste tous et les fait jouer. Le verbe `exporter-modeles` est en bas.
internal static partial class Bestiaire
{
    /// <summary>Une rotation 3 x 3 (lignes) vers un quaternion [x, y, z, w].</summary>
    private static float[] Quaternion(double[] m)
    {
        double tr = m[0] + m[4] + m[8], x, y, z, w;
        if (tr > 0) { double s = Math.Sqrt(tr + 1) * 2; w = s / 4; x = (m[7] - m[5]) / s; y = (m[2] - m[6]) / s; z = (m[3] - m[1]) / s; }
        else if (m[0] > m[4] && m[0] > m[8]) { double s = Math.Sqrt(1 + m[0] - m[4] - m[8]) * 2; w = (m[7] - m[5]) / s; x = s / 4; y = (m[1] + m[3]) / s; z = (m[2] + m[6]) / s; }
        else if (m[4] > m[8]) { double s = Math.Sqrt(1 + m[4] - m[0] - m[8]) * 2; w = (m[2] - m[6]) / s; x = (m[1] + m[3]) / s; y = s / 4; z = (m[5] + m[7]) / s; }
        else { double s = Math.Sqrt(1 + m[8] - m[0] - m[4]) * 2; w = (m[3] - m[1]) / s; x = (m[2] + m[6]) / s; y = (m[5] + m[7]) / s; z = s / 4; }
        double n = Math.Sqrt(x * x + y * y + z * z + w * w);
        return [(float)(x / n), (float)(y / n), (float)(z / n), (float)(w / n)];
    }

    /// <summary>
    /// La place de chaque partie a une image d'une animation, en glTF : la matrice monde de son os
    /// (W[i] = W[parent] · L[i]), passee en y vers le haut (C W C, translation C t a l'echelle) et
    /// decomposee en translation, rotation et echelle uniforme. Null pour une partie sans os.
    /// </summary>
    private static (float[] T, float[] R, float S)?[] PosesDesParties(Clip c, int f, int nParties)
    {
        var W = new (double[] R, double[] T)[c.Os];
        for (int i = 0; i < c.Os; i++)
        {
            var p = c.Poses[c.Indices[f * c.Os + i]];
            double s = p.Echelle / 4096.0;
            var L = Rotation(p.Rx, p.Ry, p.Rz).Select(v => v * s).ToArray();
            double[] lt = [p.Tx, p.Ty, p.Tz];
            if (i == 0 || c.Parents[i] >= i) { W[i] = (L, lt); continue; }
            var (pr, pt) = W[c.Parents[i]];
            var R = new double[9];
            for (int a = 0; a < 3; a++) for (int b = 0; b < 3; b++) R[a * 3 + b] = pr[a * 3] * L[b] + pr[a * 3 + 1] * L[3 + b] + pr[a * 3 + 2] * L[6 + b];
            var T = new double[3];
            for (int a = 0; a < 3; a++) T[a] = pr[a * 3] * lt[0] + pr[a * 3 + 1] * lt[1] + pr[a * 3 + 2] * lt[2] + pt[a];
            W[i] = (R, T);
        }

        var r = new (float[] T, float[] R, float S)?[nParties];
        double[] c3 = [1, -1, -1];
        for (int j = 0; j < nParties; j++)
        {
            if (j >= c.OsDesParties.Length || c.OsDesParties[j] >= c.Os) continue;
            var (wr, wt) = W[c.OsDesParties[j]];
            var m = new double[9];
            for (int a = 0; a < 3; a++) for (int b = 0; b < 3; b++) m[a * 3 + b] = c3[a] * wr[a * 3 + b] * c3[b];
            double s = Math.Sqrt(m[0] * m[0] + m[3] * m[3] + m[6] * m[6]);
            if (s < 1e-9) s = 1e-9;
            var (x, y, z) = G(new V3((float)wt[0], (float)wt[1], (float)wt[2]));
            r[j] = ([x, y, z], Quaternion(m.Select(v => v / s).ToArray()), (float)s);
        }
        return r;
    }


    /// <summary>Les images s'enchainent a 30 par seconde : c'est un choix de l'export, le rythme du jeu n'est pas releve.</summary>
    private const float ImagesParSeconde = 30f;

    private static void EcrireModeleGltf(ModeleAnime ma, string dossier)
    {
        var t = new Tampon();
        var nodes = new JsonArray();
        var meshes = new JsonArray();
        var clip0 = ma.Clips.FirstOrDefault();

        // Les maillages, un par partie, dans l'espace de leur os.
        var meshDePartie = new int[ma.Parties.Count];
        for (int j = 0; j < ma.Parties.Count; j++)
        {
            meshDePartie[j] = -1;
            if (ma.Parties[j].Count == 0) continue;
            var prims = Primitives(ma.Parties[j], [0], t);
            meshes.Add(new JsonObject { ["name"] = $"partie-{j}", ["primitives"] = prims });
            meshDePartie[j] = meshes.Count - 1;
        }

        // Les parties, chacune un noeud enfant de la racine, animees par la matrice monde de leur os.
        // Miroir de : la boucle d'os de DrawAnimatedModel — W[0] = L[0] (l'os 0 pend a l'entite),
        // W[i] = W[parent] · L[i], L = RotMatrix(pose) · echelle, translation de la pose. Chaque
        // animation porte son propre squelette (le nombre d'os change d'une animation a l'autre chez
        // un modele sur deux) : c'est pourquoi les parties sont animees directement, et non les os.
        nodes.Add(new JsonObject { ["name"] = ma.Nom });
        var enfants = new JsonArray();
        var noeudDePartie = new int[ma.Parties.Count];
        var defaut = clip0 != null ? PosesDesParties(clip0, 0, ma.Parties.Count) : null;
        for (int j = 0; j < ma.Parties.Count; j++)
        {
            noeudDePartie[j] = -1;
            if (meshDePartie[j] < 0) continue;
            var n = new JsonObject { ["name"] = $"partie-{j}", ["mesh"] = meshDePartie[j] };
            if (defaut?[j] is { } p)
            {
                n["translation"] = new JsonArray(p.T.Select(v => (JsonNode)v).ToArray());
                n["rotation"] = new JsonArray(p.R.Select(v => (JsonNode)v).ToArray());
                n["scale"] = new JsonArray(p.S, p.S, p.S);
            }
            nodes.Add(n);
            noeudDePartie[j] = nodes.Count - 1;
            enfants.Add(nodes.Count - 1);
        }
        if (enfants.Count > 0) nodes[0]!["children"] = enfants;

        var animations = new JsonArray();
        foreach (var c in ma.Clips)
        {
            int temps = t.Ajouter(Enumerable.Range(0, c.Images).Select(f => f / ImagesParSeconde).ToArray(), 1, true);
            var images = Enumerable.Range(0, c.Images).Select(f => PosesDesParties(c, f, ma.Parties.Count)).ToArray();
            var samplers = new JsonArray();
            var channels = new JsonArray();
            for (int j = 0; j < ma.Parties.Count; j++)
            {
                if (noeudDePartie[j] < 0 || images[0][j] == null) continue;
                var T = new List<float>(); var R = new List<float>(); var S = new List<float>();
                float[]? prev = null;
                foreach (var img in images)
                {
                    var p = img[j]!.Value;
                    var rot = p.R;
                    // Un quaternion et son oppose sont la meme rotation : on garde le plus proche du
                    // precedent, sans quoi l'interpolation ferait un tour complet entre deux images.
                    if (prev != null && prev[0] * rot[0] + prev[1] * rot[1] + prev[2] * rot[2] + prev[3] * rot[3] < 0)
                        rot = rot.Select(v => -v).ToArray();
                    prev = rot;
                    T.AddRange(p.T); R.AddRange(rot); S.AddRange([p.S, p.S, p.S]);
                }
                foreach (var (chemin, data, comps) in new[] { ("translation", T, 3), ("rotation", R, 4), ("scale", S, 3) })
                {
                    samplers.Add(new JsonObject { ["input"] = temps, ["output"] = t.Ajouter(data.ToArray(), comps, false), ["interpolation"] = "LINEAR" });
                    channels.Add(new JsonObject { ["sampler"] = samplers.Count - 1, ["target"] = new JsonObject { ["node"] = noeudDePartie[j], ["path"] = chemin } });
                }
            }
            if (channels.Count > 0)
                animations.Add(new JsonObject { ["name"] = $"animation-{c.Numero:D2}", ["samplers"] = samplers, ["channels"] = channels });
        }

        var pbr = new JsonObject { ["metallicFactor"] = 0, ["roughnessFactor"] = 1 };
        if (ma.Page != null) pbr["baseColorTexture"] = new JsonObject { ["index"] = 0 };
        else pbr["baseColorFactor"] = new JsonArray(0.6, 0.6, 0.6, 1);
        var gltf = new JsonObject
        {
            ["asset"] = new JsonObject { ["version"] = "2.0", ["generator"] = "OpenForesia.Tools exporter-modeles" },
            ["scene"] = 0,
            ["scenes"] = new JsonArray(new JsonObject { ["name"] = ma.Nom, ["nodes"] = new JsonArray(0) }),
            ["nodes"] = nodes, ["meshes"] = meshes,
            ["materials"] = new JsonArray(new JsonObject
            {
                ["name"] = "texture", ["pbrMetallicRoughness"] = pbr, ["doubleSided"] = true, ["alphaMode"] = "MASK", ["alphaCutoff"] = 0.5,
            }),
            ["buffers"] = new JsonArray(new JsonObject { ["uri"] = "modele.bin", ["byteLength"] = t.Bin.Length }),
            ["bufferViews"] = t.Vues, ["accessors"] = t.Accesseurs,
        };
        if (animations.Count > 0) gltf["animations"] = animations;
        if (ma.Page != null)
        {
            gltf["images"] = new JsonArray(new JsonObject { ["uri"] = "texture.png" });
            gltf["textures"] = new JsonArray(new JsonObject { ["source"] = 0, ["sampler"] = 0 });
            gltf["samplers"] = new JsonArray(new JsonObject { ["magFilter"] = 9728, ["minFilter"] = 9728 });
            File.WriteAllBytes(Path.Combine(dossier, "texture.png"), Png.Encode(ma.Page, 256, 256));
        }
        File.WriteAllBytes(Path.Combine(dossier, "modele.bin"), t.Bin.ToArray());
        File.WriteAllText(Path.Combine(dossier, "modele.gltf"), gltf.ToJsonString(s_json));
    }

    private static string B64Short(IEnumerable<short> v) { var a = v.ToArray(); var b = new byte[a.Length * 2]; Buffer.BlockCopy(a, 0, b, 0, b.Length); return Convert.ToBase64String(b); }
    private static string B64UShort(IEnumerable<ushort> v) { var a = v.ToArray(); var b = new byte[a.Length * 2]; Buffer.BlockCopy(a, 0, b, 0, b.Length); return Convert.ToBase64String(b); }

    /// <summary>Les donnees de la page : parties dans l'espace du jeu, poses brutes ; la page refait le calcul du jeu.</summary>
    private static void EcrireDonneesPage(ModeleAnime ma, string dossier)
    {
        var data = new
        {
            nom = ma.Nom, famille = ma.Famille, vu = ma.Vu,
            texture = ma.Page != null ? "data:image/png;base64," + Convert.ToBase64String(Png.Encode(ma.Page, 256, 256)) : null,
            parties = ma.Parties.Select(p => new
            {
                p = B64(p.SelectMany(tr => new[] { tr.A.X, tr.A.Y, tr.A.Z, tr.B.X, tr.B.Y, tr.B.Z, tr.C.X, tr.C.Y, tr.C.Z }).ToArray()),
                t = B64(p.SelectMany(tr => new[] { (tr.Ua + 0.5f) / 256, (tr.Va + 0.5f) / 256, (tr.Ub + 0.5f) / 256, (tr.Vb + 0.5f) / 256, (tr.Uc + 0.5f) / 256, (tr.Vc + 0.5f) / 256 }).ToArray()),
            }).ToArray(),
            clips = ma.Clips.Select(c => new
            {
                numero = c.Numero, images = c.Images, os = c.Os, parents = c.Parents, osParties = c.OsDesParties,
                poses = B64Short(c.Poses.SelectMany(p => new[] { p.Rx, p.Ry, p.Rz, p.Tx, p.Ty, p.Tz, (short)p.Echelle })),
                indices = B64UShort(c.Indices),
            }).ToArray(),
        };
        File.WriteAllText(Path.Combine(dossier, "donnees.js"), "chargerModele(" + JsonSerializer.Serialize(data) + ");\n");
    }

    /// <summary>
    /// <c>exporter-modeles [dossier-jeu] --sortie &lt;dossier&gt;</c> : tous les modeles animes du jeu,
    /// un dossier par modele, rangés par zone (les heros a part), et <c>index.html</c> a la racine.
    /// </summary>
    public static int ExporterModeles(string dossierDonnees, string sortie)
    {
        string french = Path.Combine(dossierDonnees, "French");
        if (!Directory.Exists(french)) french = dossierDonnees;
        foreach (var f in new[] { "BLAZE.ALL", "SYSTEM.INI" })
            if (!File.Exists(Path.Combine(french, f)))
            { Console.WriteLine($"Données du jeu absentes : {Path.Combine(french, f)} introuvable. Rien n'a été écrit."); return 1; }
        string full = Path.GetFullPath(sortie);
        if (DansLeDepot(full))
        { Console.WriteLine($"Refusé : {full} est dans le dépôt. Les modèles sont des données du jeu — choisir un dossier hors du dépôt. Rien n'a été écrit."); return 1; }

        byte[] archive = File.ReadAllBytes(Path.Combine(french, "BLAZE.ALL"));
        var root = new Blob(File.ReadAllBytes(Path.Combine(french, "SYSTEM.INI")));
        root.Fixup();
        var modeles = LireTousLesModeles(archive, root);

        var catalogue = new List<object>();
        var resume = new StringBuilder("Export des modèles animés\n\n");
        var pris = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var ma in modeles)
        {
            string nom = NomDeFichier(ma.Nom), rel = Path.Combine(NomDeFichier(ma.Dossier), nom);
            for (int n = 2; !pris.Add(rel); n++) rel = Path.Combine(NomDeFichier(ma.Dossier), $"{nom} ({n})");
            string dir = Path.Combine(full, rel);
            Directory.CreateDirectory(dir);
            EcrireModeleGltf(ma, dir);
            EcrireDonneesPage(ma, dir);
            catalogue.Add(new
            {
                nom = ma.Nom, famille = ma.Famille, groupe = ma.Dossier, vu = ma.Vu,
                chemin = rel.Replace('\\', '/'), animations = ma.Clips.Count, triangles = ma.Triangles,
            });
            resume.AppendLine($"{rel,-60} {ma.Triangles,5} triangles  {ma.Parties.Count,3} parties  {ma.Clips.Count,3} animations"
                              + (ma.Page == null ? "  sans texture" : ""));
            foreach (var w in ma.Avertissements.Distinct()) resume.AppendLine("        " + w);
        }
        resume.AppendLine();
        resume.AppendLine($"{modeles.Count} modèles, {modeles.Sum(m => m.Clips.Count)} animations.");

        string json = JsonSerializer.Serialize(catalogue).Replace("</", "<\\/");
        File.WriteAllText(Path.Combine(full, "index.html"), PageModeles.Modele.Replace("@@CATALOGUE@@", json));
        File.WriteAllText(Path.Combine(full, "resume.txt"), resume.ToString());
        Console.WriteLine(resume.ToString());
        return 0;
    }

    private static bool DansLeDepot(string full)
    {
        for (var d = new DirectoryInfo(AppContext.BaseDirectory); d != null; d = d.Parent)
        {
            if (!File.Exists(Path.Combine(d.FullName, "OpenForesia.sln"))) continue;
            string repo = d.FullName.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            return (full.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar).StartsWith(repo, StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }
}
