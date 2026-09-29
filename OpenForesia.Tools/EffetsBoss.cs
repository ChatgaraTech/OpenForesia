using Iced.Intel;
using System.Text;

/// <summary>
/// Le verbe <c>effets-boss</c> (LP-013, tâche 8.1.4) : pour chaque boss de l'arène, tout ce que son
/// combat emprunte à SA sous-zone, lu hors jeu dans <c>Blaze.exe</c> et dans l'archive.
///
/// <para>
/// On part de l'IA de chaque espèce du combat (la table <c>0x0056ad48</c> de sa sous-zone), et on
/// désassemble par descente récursive : sauts, sauts conditionnels, tables de sauts. On suit les
/// appels directs vers le code du jeu (sous <c>0x00530000</c> ; au-delà, le moteur, le son et la CRT
/// sont des feuilles). À chaque naissance d'effet — les trois jumelles <c>FUN_0047a9d0</c>,
/// <c>FUN_0047aae0</c> et <c>FUN_0047ac40</c> —, on lit le sous-type poussé : une constante, ou
/// <c>0xc0 + octet k</c> du premier bloc de <c>+0xcc</c>, lu dans la sous-zone. On le résout comme le
/// jeu : sous <c>0xc0</c> dans la table statique du pool (<c>0x00551c30</c>, <c>0x00551f30</c>,
/// <c>0x00552230</c>), au-dessus dans la table que la sous-zone installe (<c>0x0056b0c8</c>,
/// <c>0x0056b1c8</c>, <c>0x0056b2c8</c>, par <c>ResolveEntityHandler</c>), et on descend dans le
/// gestionnaire trouvé.</para>
/// <para>
/// Ce que le parcours relève, fonction par fonction : les sons de niveau (id ≥ 60, poussés en
/// constante), les champs du répertoire de sous-zone lus (<c>[0x005e97cc] + d</c> : <c>+0xa8</c> les
/// sons de la sous-zone, <c>+0xcc</c> le catalogue d'effets, <c>+0x2c</c>, <c>+0x38</c>… les
/// maillages et atlas), et les planches posées en <c>+0x3c</c> — chaque image désigne sa texture par
/// une entrée de la table de décodage <c>0x005e8f00</c> (octet 2 de son second mot) : de 0 à 0x0f la
/// sous-zone, de 0x10 à 0x17 l'état 3, de 0x18 à 0x2f la zone, au-delà le chargement global.</para>
/// <para>
/// C'est une lecture STATIQUE et approchée : l'ordre des instructions est celui des adresses, pas
/// celui de l'exécution ; un sous-type lu dans l'entité est dit « variable » ; la longueur d'une
/// planche n'est pas établie (on lit ses huit premières images). Il sert à chiffrer, pas à prouver.
/// </para>
/// </summary>
internal static partial class Bestiaire
{
    private sealed record BossArene(string Nom, int Zone, int SousZone, int[] Especes);

    /// <summary>Les boss de l'arène (legacy.md, LP-013) : les faits d'abord, pour contrôle.</summary>
    private static readonly BossArene[] s_BossArene =
    [
        new("Méchant ours", 3, 5, [0, 4]),
        new("Behemoth", 3, 9, [0]),
        new("Sylphide et Wybarn", 4, 7, [1, 2]),
        new("Fougre", 8, 1, [1, 0]),
        new("Le gros poilu", 10, 12, [0]),
        new("Jeune dragon", 1, 4, [2]),
        new("Loup-garou et Tigre", 6, 5, [0, 1]),
        new("Nocturio", 6, 11, [0, 2]),
        new("Durahan", 5, 4, [2, 3]),
        new("Troll", 1, 9, [3, 0]),
        new("L'Ange noir", 10, 14, [0]),
        new("Eifridt", 10, 11, [0]),
        new("Kraaken", 9, 0, [2, 3, 4]),
        new("Roi-zombie", 5, 9, [1, 0, 2, 3]),
        new("Lordémon", 10, 13, [0]),
        new("Cerbère", 10, 9, [0, 1]),
        new("Belzébuth", 10, 15, [0, 1]),
        new("Dragon zombie", 10, 4, [0, 1]),
        new("Dragon rouge", 2, 0, [0, 2]),
    ];

    // Les trois jumelles de naissance : (fonction, rang du sous-type parmi les arguments, table
    // statique, sélecteur de la table par sous-zone, nom de la table par sous-zone).
    private static readonly (uint Fonction, int Argument, uint Statique, uint Selecteur, string Table)[] s_Naissances =
    [
        (0x0047a9d0, 2, 0x00551c30, 0x00567898, "0x0056b0c8"),
        (0x0047aae0, 2, 0x00551f30, 0x00567b58, "0x0056b1c8"),
        (0x0047ac40, 4, 0x00552230, 0x00567e18, "0x0056b2c8"),
    ];

    private const uint AppelSonPositionnel = 0x0053db20, AppelSon = 0x0053cea0, AppelSort = 0x0053a980;
    private const uint FinCodeDuJeu = 0x00530000;
    private const uint Repertoire = 0x005e97cc;
    private const int ProfondeurMax = 8;

    /// <summary>Un nœud de l'arbre : une fonction, ce qu'elle relève, et ses enfants.</summary>
    private sealed class Noeud(string titre)
    {
        public readonly string Titre = titre;
        public readonly List<string> Releves = [];
        public readonly List<Noeud> Enfants = [];
        public bool Plein => Releves.Count > 0 || Enfants.Any(e => e.Plein);
    }

    /// <summary>Ce qu'un boss demande, cumulé sur tout son arbre.</summary>
    private sealed class Devis
    {
        public readonly SortedSet<string> Gestionnaires = [];     // les effets par sous-zone à greffer
        public readonly SortedSet<int> Sons = [];
        public readonly SortedSet<int> Champs = [];               // les champs du répertoire lus
        public readonly SortedSet<int> Entrees = [];              // les entrées de décodage des planches et modèles
        public readonly SortedSet<int> Modeles = [];              // les modèles de décor de la sous-zone (e[4])
        public readonly SortedSet<string> Inconnus = [];
        public bool Sorts;
    }

    private static readonly uint[] s_ChampsCatalogues = [0xcc, 0xd0, 0xd4];
    private static readonly Dictionary<uint, byte[]> s_Catalogues = [];

    // Le boss en cours : de quoi résoudre un modèle de décor (e[4]) en texture.
    private static int s_ModelesEtat;                       // s_Unknown_005edec4 : les modèles communs de l'état 3
    private static List<int> s_EntreesModelesSousZone = []; // l'entrée de décodage de chaque partie du +0xac de la sous-zone

    public static void EffetsBoss(string dossierDonnees, string? sortie)
    {
        if (!LoadBlazeExe()) { Console.WriteLine($"{BlazeExePath} introuvable."); return; }
        string french = Path.Combine(dossierDonnees, "French");
        if (!Directory.Exists(french)) french = dossierDonnees;
        byte[] archive = File.ReadAllBytes(Path.Combine(french, "BLAZE.ALL"));
        var root = new Blob(File.ReadAllBytes(Path.Combine(french, "SYSTEM.INI")));
        root.Fixup();
        var stateDir = LoadEntry(archive, root, EntryList(root, 0x48)[3]);
        // LoadWorldMapResources : s_Unknown_005edec4 = le compte du DERNIER paquet de +0xac de l'état.
        s_ModelesEtat = 0;
        for (int e = stateDir.I32(0xac); stateDir.In(e, 8) && stateDir.I32(e) != 0; e += 8)
            s_ModelesEtat = LoadEntryRaw(archive, stateDir, stateDir.I32(e)).I32(0);
        Console.WriteLine($"modèles communs de l'état 3 (s_Unknown_005edec4) : {s_ModelesEtat}");

        var md = new StringBuilder();
        md.AppendLine("# Ce que chaque boss de l'arène emprunte à sa sous-zone");
        md.AppendLine();
        md.AppendLine("**Généré** par `OpenForesia.Tools effets-boss --sortie Documentation/Etat/EffetsDesBoss.md` ; ne pas éditer à la main.");
        md.AppendLine("Lecture statique de `Blaze.exe` et de l'archive (LP-013, tâche 8.1.4) : voir l'en-tête de `OpenForesia.Tools/EffetsBoss.cs` pour ce qu'elle voit et ce qu'elle ne voit pas.");
        md.AppendLine();
        md.AppendLine("Entrées de décodage : `s` = texture de la sous-zone (à greffer), `z` = texture de la zone (à greffer), `état 3` et `global` = communes à tout le jeu.");
        md.AppendLine();

        var resume = new List<string>();
        foreach (BossArene boss in s_BossArene)
        {
            var zoneBlob = LoadEntry(archive, stateDir, EntryList(stateDir, 0x48)[boss.Zone]);
            var a = LoadEntry(archive, zoneBlob, EntryList(zoneBlob, 0x48)[boss.SousZone]);
            // Les trois catalogues d'effets de la sous-zone : le premier bloc de +0xcc, +0xd0 et +0xd4
            // (MiscMarkers.cs : 0x00483930 lit +0xd0, 0x00484f80 lit +0xd4).
            s_Catalogues.Clear();
            foreach (uint champ in s_ChampsCatalogues)
            {
                int blocOff = a.In(a.I32((int)champ), 4) ? a.I32(a.I32((int)champ)) : -1;
                s_Catalogues[champ] = a.In(blocOff, 64) ? a.Data[blocOff..(blocOff + 64)] : [];
            }
            byte[] bloc = s_Catalogues[0xcc];
            int index = boss.Zone == 0 ? 0 : boss.Zone * 0x10 + boss.SousZone;

            // LoadWorldMapSubArea : le +0xac de la sous-zone, relogé dans 0x00600780 (le dernier paquet).
            s_EntreesModelesSousZone = [];
            for (int e = a.I32(0xac); a.In(e, 8) && a.I32(e) != 0; e += 8)
            {
                var b = LoadEntryRaw(archive, a, a.I32(e));
                int n = b.I32(0);
                s_EntreesModelesSousZone = Enumerable.Range(0, n).Select(j => (b.I32((n + 1 + j) * 4) | a.I32(e + 4)) >> 16 & 0xff).ToList();
            }

            var devis = new Devis();
            var vus = new HashSet<uint>();
            var racine = new Noeud($"{boss.Nom} ({boss.Zone}/{boss.SousZone})");
            var textures = new List<string>();

            int especes = a.I32(0x4c);
            foreach (int esp in boss.Especes)
            {
                int o = especes + esp * 8;
                int maillage = a.U8(o), ia = a.U8(o + 1);
                uint iaVa = ExeU32(ExeU32(0x00566d98 + (uint)index * 4) + (uint)ia * 4);
                int pagination = a.I32(a.I32(0x2c) + maillage * 8 + 4);
                int entree = pagination >> 16 & 0xff;
                textures.Add($"espèce {esp} : texture {Texture(archive, a, zoneBlob, entree)}");
                devis.Entrees.Add(entree);
                racine.Enfants.Add(Parcourir(iaVa, $"espèce {esp}, IA {ia} = 0x{iaVa:x8}", 0, index, bloc, vus, devis));
            }

            md.AppendLine($"## {boss.Nom} ({boss.Zone}/{boss.SousZone})");
            md.AppendLine();
            foreach (string t in textures) md.AppendLine($"- {t}");
            md.AppendLine($"- **effets par sous-zone à greffer** : {(devis.Gestionnaires.Count == 0 ? "aucun" : string.Join(", ", devis.Gestionnaires))}");
            md.AppendLine($"- **sons de niveau** : {(devis.Sons.Count == 0 ? "aucun" : string.Join(", ", devis.Sons.Select(s => $"0x{s:x2}")))}");
            md.AppendLine($"- **champs du répertoire lus** : {(devis.Champs.Count == 0 ? "aucun" : string.Join(", ", devis.Champs.Select(c => $"+0x{c:x2}")))}"
                          + (devis.Champs.Contains(0xa8) ? " — `+0xa8` : les sons de la sous-zone" : ""));
            var entrees = devis.Entrees.Select(e => $"0x{e:x2} ({Texture(archive, a, zoneBlob, e)})").ToList();
            md.AppendLine($"- **entrées de décodage** : {string.Join(" ; ", entrees)}");
            if (devis.Modeles.Count > 0) md.AppendLine($"- **modèles de décor de la sous-zone** : {string.Join(", ", devis.Modeles.Select(m => $"0x{m:x2}"))}");
            if (devis.Sorts) md.AppendLine("- lance des sorts (fiches communes)");
            if (devis.Inconnus.Count > 0) md.AppendLine($"- **à lire à la main** : {string.Join(" ; ", devis.Inconnus)}");
            md.AppendLine();
            md.AppendLine("```");
            Ecrire(md, racine, 0);
            md.AppendLine("```");
            md.AppendLine();

            int aGreffer = devis.Entrees.Count(e => e < 0x10 || (e >= 0x18 && e < 0x30));
            resume.Add($"| {boss.Nom} | {boss.Zone}/{boss.SousZone} | {devis.Gestionnaires.Count} | {devis.Sons.Count}{(devis.Champs.Contains(0xa8) ? " + `+0xa8`" : "")} | {aGreffer} | {devis.Inconnus.Count} |");
        }

        var tete = new StringBuilder();
        tete.AppendLine("| Boss | Sous-zone | Effets à greffer | Sons de niveau | Textures à greffer (espèces comprises) | À lire à la main |");
        tete.AppendLine("|---|---|---|---|---|---|");
        foreach (string r in resume) tete.AppendLine(r);
        tete.AppendLine();
        string texte = md.ToString().Replace("\r\n", "\n");
        int coupe = texte.IndexOf("## ", StringComparison.Ordinal);
        texte = texte[..coupe] + tete.ToString().Replace("\r\n", "\n") + texte[coupe..];

        if (sortie == null) Console.Write(texte);
        else
        {
            File.WriteAllText(sortie, texte, new UTF8Encoding(false));
            Console.WriteLine($"écrit : {sortie}");
            Console.Write(tete.ToString());
        }
    }

    /// <summary>L'entrée de décodage <paramref name="e"/> : ce qu'elle désigne, et sa taille quand elle est à greffer.</summary>
    private static string Texture(byte[] archive, Blob sousZone, Blob zone, int e)
    {
        if (e < 0x10) return $"s{e} {TailleTexture(archive, sousZone, e)}";
        if (e < 0x18) return "état 3";
        if (e < 0x30) return $"z{e - 0x18} {TailleTexture(archive, zone, e - 0x18)}";
        return "global";
    }

    private static string TailleTexture(byte[] archive, Blob dir, int rang)
    {
        int liste = dir.I32(0x10);
        int e = liste + rang * 8;
        if (!dir.In(e, 8) || dir.I32(e) == 0) return "(absente)";
        var t = LoadEntryRaw(archive, dir, dir.I32(e));
        if (t.Len < 8) return "(illisible)";
        int flags = t.I32(4), off = 8;
        if ((flags & 8) != 0 && t.In(off, 12)) off += t.I32(off);
        return t.In(off, 12) ? $"{t.U16(off + 8)}×{t.U16(off + 10)} mots, {(flags & 3) switch { 0 => "4 bits", 1 => "8 bits", _ => "16 bits" }}" : "(illisible)";
    }

    private static void Ecrire(StringBuilder md, Noeud n, int niveau)
    {
        if (!n.Plein && niveau > 0) return;
        string marge = new(' ', niveau * 2);
        md.AppendLine(marge + n.Titre);
        foreach (string r in n.Releves) md.AppendLine(marge + "  · " + r);
        foreach (Noeud e in n.Enfants) Ecrire(md, e, niveau + 1);
    }

    // ---- le parcours -------------------------------------------------------------------------------

    private static Noeud Parcourir(uint va, string titre, int profondeur, int index, byte[] bloc, HashSet<uint> vus, Devis devis)
    {
        var noeud = new Noeud(titre);
        if (!vus.Add(va)) return noeud;                  // déjà parcouru : ses relevés sont plus haut
        if (va == 0x00401fa0) return noeud;
        if (profondeur > ProfondeurMax) { devis.Inconnus.Add($"profondeur dépassée en 0x{va:x8}"); return noeud; }

        List<Instruction> corps = Corps(va);
        var rep = new HashSet<Register>();

        for (int i = 0; i < corps.Count; i++)
        {
            Instruction ins = corps[i];

            // Les champs du répertoire de sous-zone : [0x005e97cc] dans un registre, puis [reg + d].
            if (ins.Mnemonic == Mnemonic.Mov && ins.Op0Kind == OpKind.Register && ins.Op1Kind == OpKind.Memory
                && ins.MemoryBase == Register.None && ins.MemoryIndex == Register.None && ins.MemoryDisplacement32 == Repertoire)
            {
                rep.Add(ins.Op0Register.GetFullRegister32());
                continue;
            }
            if (MemoireLue(ins, out Register baseReg, out uint disp) && rep.Contains(baseReg))
            {
                devis.Champs.Add((int)disp);
                noeud.Releves.Add($"lit le répertoire +0x{disp:x2}");
            }
            if (ins.OpCount > 0 && ins.Op0Kind == OpKind.Register) rep.Remove(ins.Op0Register.GetFullRegister32());

            // Un modèle de décor : mov byte [r + 4], k. Sous s_Unknown_005edec4, un modèle commun de
            // l'état 3 ; au-delà, la partie k - s_Unknown_005edec4 du +0xac de la sous-zone
            // (WorldMapHud, le dessin d'une entité au bit 0x100000).
            if (ins.Mnemonic == Mnemonic.Mov && ins.Op0Kind == OpKind.Memory && ins.MemorySize == MemorySize.UInt8
                && ins.MemoryDisplacement32 == 4 && ins.MemoryBase != Register.None && ins.Op1Kind == OpKind.Immediate8)
            {
                int k = ins.Immediate8;
                if (k < s_ModelesEtat) noeud.Releves.Add($"modèle {k} (commun, état 3)");
                else if (k - s_ModelesEtat < s_EntreesModelesSousZone.Count)
                {
                    int entree = s_EntreesModelesSousZone[k - s_ModelesEtat];
                    devis.Entrees.Add(entree);
                    devis.Modeles.Add(k);
                    noeud.Releves.Add($"modèle {k} = partie {k - s_ModelesEtat} du +0xac de la sous-zone (entrée 0x{entree:x2})");
                }
                else noeud.Releves.Add($"modèle {k} (au-delà des {s_ModelesEtat} + {s_EntreesModelesSousZone.Count} modèles : pas un modèle ?)");
            }

            // Une planche posée en +0x3c.
            if (ins.Mnemonic == Mnemonic.Mov && ins.Op0Kind == OpKind.Memory && ins.MemoryDisplacement32 == 0x3c
                && ins.Op1Kind is OpKind.Immediate32 && EstDonnee(ins.Immediate32))
                Planche(ins.Immediate32, noeud, devis);
            // Une planche probable : une adresse de la zone des planches écrite ailleurs (une table
            // montée sur la pile, 0x00484f80) ou poussée en argument.
            else if (ins.Mnemonic is Mnemonic.Mov or Mnemonic.Push
                     && (ins.Mnemonic == Mnemonic.Push ? ins.Op0Kind : ins.Op1Kind) is OpKind.Immediate32 or OpKind.Immediate8to32
                     && (ins.Mnemonic == Mnemonic.Push || ins.Op0Kind == OpKind.Memory)
                     && ins.Immediate32 >= 0x0054f000 && ins.Immediate32 < 0x00551c30)   // les 221 s_Descriptors_ connus : 0x0054F098..0x00551BA0
                Planche(ins.Immediate32, noeud, devis, probable: true);

            if (ins.Mnemonic != Mnemonic.Call || ins.Op0Kind != OpKind.NearBranch32) continue;
            uint cible = ins.NearBranch32;

            int n = Array.FindIndex(s_Naissances, s => s.Fonction == cible);
            if (n >= 0)
            {
                var naissance = s_Naissances[n];
                if (naissance.Fonction == 0x0047ac40 && Poussee(corps, i, 3) is { } atlas && atlas.Imm is { } ptr && EstDonnee(ptr))
                    Planche(ptr, noeud, devis);
                var sous = SousType(corps, i, naissance.Argument, bloc);
                if (sous.Valeur is not { } v)
                {
                    noeud.Releves.Add($"naissance par 0x{cible:x8} : sous-type {sous.Texte}");
                    devis.Inconnus.Add($"sous-type {sous.Texte} (0x{ins.IP32:x8})");
                    continue;
                }
                uint gestionnaire;
                string ou;
                if (v < 0xc0)
                {
                    gestionnaire = ExeU32(naissance.Statique + (uint)v * 4);
                    ou = $"statique 0x{naissance.Statique:x8}[0x{v:x2}]";
                }
                else
                {
                    uint blocVt = ExeU32(naissance.Selecteur + (uint)index * 4);
                    gestionnaire = ExeU32(blocVt + (uint)(v - 0xc0) * 4);
                    ou = $"PAR SOUS-ZONE {naissance.Table}[{v - 0xc0}]";
                    if (gestionnaire != 0x00401fa0) devis.Gestionnaires.Add($"`0x{gestionnaire:x8}` ({naissance.Table}[{v - 0xc0}])");
                }
                noeud.Enfants.Add(Parcourir(gestionnaire, $"effet 0x{v:x2} ({sous.Texte}) → {ou} = 0x{gestionnaire:x8}"
                                                         + (gestionnaire == 0x00401fa0 ? " (corps vide)" : ""),
                                            profondeur + 1, index, bloc, vus, devis));
                continue;
            }

            if (cible is AppelSonPositionnel or AppelSon)
            {
                var id = Poussee(corps, i, 1);
                if (id?.Imm is { } s && s >= 0x3c && s < 0x100) { devis.Sons.Add((int)s); noeud.Releves.Add($"son de niveau 0x{s:x2}"); }
                continue;
            }
            if (cible == AppelSort) { devis.Sorts = true; continue; }
            if (cible >= FinCodeDuJeu || cible < 0x00401000) continue;

            noeud.Enfants.Add(Parcourir(cible, $"appel 0x{cible:x8}", profondeur + 1, index, bloc, vus, devis));
        }
        return noeud;
    }

    private static bool EstDonnee(uint va) => va >= 0x0054e000 && va < 0x00600000;

    private static void Planche(uint va, Noeud noeud, Devis devis, bool probable = false)
    {
        var entrees = new SortedSet<int>();
        for (int k = 0; k < 8; k++)
        {
            uint image = ExeU32(va + (uint)k * 8), attributs = ExeU32(va + (uint)k * 8 + 4);
            if (image == 0 && attributs == 0) break;
            entrees.Add((int)(attributs >> 16 & 0xff));
        }
        foreach (int e in entrees) devis.Entrees.Add(e);
        noeud.Releves.Add($"{(probable ? "planche probable" : "planche")} 0x{va:x8} : entrées {string.Join(", ", entrees.Select(e => $"0x{e:x2}"))}");
    }

    private static bool MemoireLue(Instruction ins, out Register baseReg, out uint disp)
    {
        for (int k = 0; k < ins.OpCount; k++)
        {
            if (ins.GetOpKind(k) != OpKind.Memory || ins.MemoryIndex != Register.None || ins.MemoryBase == Register.None) continue;
            baseReg = ins.MemoryBase.GetFullRegister32();
            disp = ins.MemoryDisplacement32;
            return true;
        }
        baseReg = Register.None;
        disp = 0;
        return false;
    }

    /// <summary>La n-ième poussée avant l'appel (1 = la dernière, donc le premier argument).</summary>
    private static (uint? Imm, Register Reg, int Rang)? Poussee(List<Instruction> corps, int appel, int n)
    {
        int vues = 0;
        for (int j = appel - 1; j >= 0 && j >= appel - 24; j--)
        {
            Instruction p = corps[j];
            if (p.FlowControl is FlowControl.Call or FlowControl.Return or FlowControl.UnconditionalBranch) break;
            if (p.Mnemonic != Mnemonic.Push) continue;
            if (++vues < n) continue;
            if (p.Op0Kind is OpKind.Immediate8to32 or OpKind.Immediate32 or OpKind.Immediate8) return ((uint)p.GetImmediate(0), Register.None, j);
            if (p.Op0Kind == OpKind.Register) return (null, p.Op0Register.GetFullRegister32(), j);
            return (null, Register.None, j);
        }
        return null;
    }

    /// <summary>
    /// Le sous-type poussé : une constante, <c>0xc0 + bloc[k]</c> (un octet lu à <c>[r + k]</c>, après
    /// un <c>+0xcc</c>, puis <c>add r, 0xc0</c>), une constante chargée dans le registre, ou « variable ».
    /// </summary>
    private static (int? Valeur, string Texte) SousType(List<Instruction> corps, int appel, int argument, byte[] bloc)
    {
        if (Poussee(corps, appel, argument) is not { } p) return (null, "introuvable");
        if (p.Imm is { } imm) return ((int)(imm & 0xff), $"0x{imm & 0xff:x2}");
        if (p.Reg == Register.None) return (null, "variable (mémoire)");

        _ = bloc;
        int ajout = 0;
        for (int j = p.Rang - 1; j >= 0 && j >= p.Rang - 24; j--)
        {
            Instruction d = corps[j];
            if (d.OpCount == 0 || d.Op0Kind != OpKind.Register || d.Op0Register.GetFullRegister32() != p.Reg) continue;
            if (d.Mnemonic == Mnemonic.Add && d.Op1Kind is OpKind.Immediate32 or OpKind.Immediate8to32) { ajout += (int)d.Immediate32; continue; }
            if (d.Mnemonic == Mnemonic.Xor && d.Op1Kind == OpKind.Register) continue;
            if (d.Mnemonic == Mnemonic.Mov && d.Op1Kind is OpKind.Immediate32 or OpKind.Immediate8) return ((int)((d.Immediate32 + (uint)ajout) & 0xff), $"0x{(d.Immediate32 + ajout) & 0xff:x2}");
            if (d.Mnemonic is Mnemonic.Mov or Mnemonic.Movzx or Mnemonic.Movsx && d.Op1Kind == OpKind.Memory && d.MemoryIndex == Register.None)
            {
                // L'octet lu à [r + k] : le catalogue est le plus proche des trois champs lus plus haut
                // (+0xcc, +0xd0, +0xd4), parfois chargé bien avant, en tête de fonction.
                int off = (int)d.MemoryDisplacement32;
                uint? champ = null;
                for (int k = j - 1; k >= 0 && k >= j - 60 && champ == null; k--)
                    if (corps[k].Op1Kind == OpKind.Memory && s_ChampsCatalogues.Contains(corps[k].MemoryDisplacement32))
                        champ = corps[k].MemoryDisplacement32;
                if (champ is { } c && ajout == 0xc0 && off < s_Catalogues[c].Length)
                {
                    int v = s_Catalogues[c][off] + 0xc0;
                    return (v & 0xff, $"0xc0 + catalogue +0x{c:x2}[0x{off:x2}] = 0x{v & 0xff:x2}");
                }
                return (null, $"variable ([{d.MemoryBase.ToString().ToLowerInvariant()} + 0x{off:x}]{(ajout != 0 ? $" + 0x{ajout:x}" : "")})");
            }
            return (null, $"variable ({d.Mnemonic.ToString().ToLowerInvariant()})");
        }
        return (null, "variable");
    }

    /// <summary>Le corps d'une fonction, par descente récursive, dans l'ordre des adresses.</summary>
    private static List<Instruction> Corps(uint entree)
    {
        var faites = new SortedDictionary<uint, Instruction>();
        var aFaire = new Stack<uint>();
        aFaire.Push(entree);
        while (aFaire.Count > 0 && faites.Count < 20000)
        {
            uint ip = aFaire.Pop();
            while (!faites.ContainsKey(ip))
            {
                int off = ExeOffset(ip);
                if (off < 0 || s_exe is null) break;
                var dec = Iced.Intel.Decoder.Create(32, new ByteArrayCodeReader(s_exe, off, Math.Min(16, s_exe.Length - off)));
                dec.IP = ip;
                Instruction ins = dec.Decode();
                if (ins.IsInvalid) break;
                faites[ip] = ins;
                uint suivante = (uint)ins.NextIP;

                switch (ins.FlowControl)
                {
                    case FlowControl.Return:
                    case FlowControl.Interrupt:
                        goto fin;
                    case FlowControl.UnconditionalBranch:
                        if (ins.Op0Kind == OpKind.NearBranch32) aFaire.Push(ins.NearBranch32);
                        goto fin;
                    case FlowControl.ConditionalBranch:
                        if (ins.Op0Kind == OpKind.NearBranch32) aFaire.Push(ins.NearBranch32);
                        break;
                    case FlowControl.IndirectBranch:
                        // Une table de sauts : jmp [r*4 + table] ; ses entrées tant qu'elles tombent près.
                        if (ins.Op0Kind == OpKind.Memory && ins.MemoryIndexScale == 4 && ins.MemoryDisplacement32 != 0)
                            for (uint t = ins.MemoryDisplacement32, k = 0; k < 64; k++, t += 4)
                            {
                                uint c = ExeU32(t);
                                if (c < entree || c > entree + 0x8000) break;
                                aFaire.Push(c);
                            }
                        goto fin;
                }
                ip = suivante;
            }
            fin:;
        }
        return faites.Values.ToList();
    }
}
