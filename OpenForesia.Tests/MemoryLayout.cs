using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenForesia.Tests;

/// <summary>
/// Relève la disposition mémoire <b>déclarée</b> par les sources de <c>src/Blaze</c>.
///
/// <para>
/// <b>Pourquoi lire les sources et non l'exécution.</b> Le défaut d'aliasing est un défaut de
/// <i>déclaration</i> : deux <c>NativeMemory.Alloc</c> pour une seule adresse du binaire. Il est donc
/// entièrement visible dans le texte, sans rien exécuter.
/// </para>
/// <para>
/// Réfléchir sur les statiques de <c>Blaze</c> aurait été plus direct, mais y toucher déclenche
/// l'initialisation de tout le type — dont <c>AllocLowZeroed</c>, qui dispute une fenêtre mémoire
/// basse et <b>échoue par intermittence</b> (voir <c>test-protocol.md</c>). Des tests de disposition
/// qui échouent une fois sur cinq pour une raison sans rapport ne valent rien.
/// </para>
/// <para>
/// La limite est assumée : ces tests prouvent ce que le code <i>déclare</i>, pas ce qu'il alloue à
/// l'exécution. Pour la classe de défaut visée, c'est la même chose.
/// </para>
/// </summary>
internal static class MemoryLayout
{
    /// <summary>Un symbole du binaire modélisé par la reconstruction.</summary>
    internal sealed record Symbol(
        string Name,
        uint Address,
        string File,
        int Line,
        SymbolKind Kind,
        uint DeclaredSize,
        string? ViaAccessor)
    {
        public override string ToString() => $"{Name} (0x{Address:x8}, {File}:{Line})";
    }

    internal enum SymbolKind
    {
        /// <summary>Une allocation propre : <c>NativeMemory.Alloc*</c> ou <c>new byte[...]</c>.</summary>
        OwnAllocation,
        /// <summary>Une vue : passe par l'accesseur d'une arène.</summary>
        ArenaView,
        /// <summary>Un champ simple, sans allocation ni accesseur.</summary>
        PlainField,
    }

    /// <summary>Un bloc adressé par décalage depuis une allocation unique.</summary>
    internal sealed record Arena(string Name, string Accessor, uint Base, uint Size)
    {
        public uint End => Base + Size;
        public bool Covers(uint va) => va >= Base && va < End;
        public override string ToString() => $"{Name} [0x{Base:x8}..0x{End:x8})";
    }

    // ---------------------------------------------------------------------------------------
    // Localisation du dépôt : on remonte depuis le binaire de test jusqu'au dossier qui porte
    // OpenForesia.sln. Aucun chemin en dur, donc le test survit à un déplacement du dépôt.
    // ---------------------------------------------------------------------------------------
    internal static string RepoRoot { get; } = FindRepoRoot();

    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null && !File.Exists(Path.Combine(dir.FullName, "OpenForesia.sln")))
            dir = dir.Parent;
        if (dir is null)
            throw new InvalidOperationException(
                "OpenForesia.sln introuvable en remontant depuis " + AppContext.BaseDirectory);
        return dir.FullName;
    }

    private static string BlazeRoot => Path.Combine(RepoRoot, "OpenForesia", "src", "Blaze");

    // ---------------------------------------------------------------------------------------
    // Lecture en ISO-8859-1, comme tout l'outillage du dépôt : un octet, un caractère. Les
    // sources portent une région à encodage double, et un décodage UTF-8 y échouerait.
    // ---------------------------------------------------------------------------------------
    private static readonly Encoding Latin1 = Encoding.Latin1;

    private static IEnumerable<(string File, string[] Lines)> SourceFiles()
    {
        foreach (var path in Directory.EnumerateFiles(BlazeRoot, "*.cs", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(BlazeRoot, path).Replace('\\', '/');
            var lines = Latin1.GetString(File.ReadAllBytes(path))
                              .Split('\n')
                              .Select(l => l.TrimEnd('\r'))
                              .ToArray();
            yield return (rel, lines);
        }
    }

    // un membre de premier niveau, éventuellement précédé d'un attribut
    private static readonly Regex MemberRe = new(
        @"^    (?:\[[^\]]*\]\s*)?(?:private|internal|public|protected)\s",
        RegexOptions.Compiled);

    // le suffixe d'adresse : s_Quelquechose_005e9000
    private static readonly Regex AddrRe = new(
        @"\b(s_[A-Za-z0-9_]*?_(00[0-9a-f]{6}))\b", RegexOptions.Compiled);

    // `NativeMemory.AllocZeroed(n)` donne n octets ; la surcharge `(compte, taille)` donne leur
    // produit. Sans le second groupe, `AllocZeroed(0x100, sizeof(int))` se mesurait a 0x100 au lieu
    // de 0x400 — un quart de la taille reelle, donc un recouvrement invisible au test 3.
    private static readonly Regex AllocRe = new(
        @"(?:NativeMemory\.Alloc(?:Zeroed)?\s*\(\s*(0x[0-9a-fA-F]+|\d+)\s*(?:,\s*(?:(0x[0-9a-fA-F]+|\d+)|sizeof\(\s*(\w+)\s*\)))?"
        + @"|new\s+\w+\[\s*(0x[0-9a-fA-F]+|\d+)\s*\])",
        RegexOptions.Compiled);

    private static readonly Dictionary<string, uint> SizeOf = new()
    {
        ["byte"] = 1, ["sbyte"] = 1, ["short"] = 2, ["ushort"] = 2,
        ["int"] = 4, ["uint"] = 4, ["long"] = 8, ["ulong"] = 8,
    };

    /// <summary>La taille en octets qu'une correspondance d'<see cref="AllocRe"/> déclare.</summary>
    private static uint AllocSize(Match alloc)
    {
        if (alloc.Groups[4].Success) return ParseNum(alloc.Groups[4].Value);   // new T[n]
        if (!alloc.Groups[1].Success) return 0;

        uint count = ParseNum(alloc.Groups[1].Value);
        if (alloc.Groups[2].Success) return count * ParseNum(alloc.Groups[2].Value);
        if (alloc.Groups[3].Success && SizeOf.TryGetValue(alloc.Groups[3].Value, out var w))
            return count * w;
        return count;
    }

    private static readonly Regex AccessorRe = new(
        @"\b(Arena|Entity|EntityParam|EntityState|Camera|View|ItemMenuState|ItemMenuBytes)\s*\(\s*0x([0-9a-fA-F]+)\s*\)",
        RegexOptions.Compiled);

    private static uint ParseNum(string s) =>
        s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            ? uint.Parse(s.AsSpan(2), NumberStyles.HexNumber)
            : uint.Parse(s);

    private static List<Symbol>? _symbols;

    /// <summary>Tous les symboles à suffixe d'adresse déclarés dans <c>src/Blaze</c>.</summary>
    internal static IReadOnlyList<Symbol> Symbols => _symbols ??= ReadSymbols();

    /// <summary>
    /// Le symbole trouvé est-il le NOM DÉCLARÉ, ou une simple mention dans l'initialiseur ?
    ///
    /// <para>Un nom déclaré est immédiatement suivi de <c>;</c>, <c>=</c> (donc aussi <c>=&gt;</c>),
    /// <c>{</c>, <c>,</c>, ou de la fin de ligne. Sans ce garde-fou,
    /// <c>internal int* CameraTranslation =&gt; (int*)(s_CameraMatrix_006008e0 + 0x14);</c> se
    /// déclarait comme une seconde déclaration de <c>s_CameraMatrix_006008e0</c> — un faux positif
    /// qui a survécu jusqu'au 2026-08-22.</para>
    /// </summary>
    private static bool IsDeclaredName(string text, Match m)
    {
        int end = m.Index + m.Length;
        while (end < text.Length && text[end] == ' ') end++;
        if (end >= text.Length) return true;
        char c = text[end];
        return c is ';' or '=' or '{' or ',';
    }

    private static List<Symbol> ReadSymbols()
    {
        var found = new List<Symbol>();
        var tails = new List<string>();          // parallèle à `found` : ce que déclare le membre

        foreach (var (file, lines) in SourceFiles())
        {
            for (int i = 0; i < lines.Length; i++)
            {
                var text = lines[i];
                if (!MemberRe.IsMatch(text)) continue;

                // Tous les noms à suffixe d'adresse de la ligne qui sont vraiment des DÉCLARANTS :
                // une ligne peut en porter plusieurs (`private static int s_A_00x, s_B_00y;`).
                var names = AddrRe.Matches(text)
                                  .Where(m => IsDeclaredName(text, m))
                                  .ToList();
                if (names.Count == 0) continue;

                var last = names[^1];
                var tail = text[(last.Index + last.Length)..];

                // Une déclaration qui ne se termine pas sur sa ligne porte un corps de propriété :
                // c'est là que vit l'accesseur d'arène des vues écrites en `{ get => …; set => …; }`.
                if (!tail.Contains(';'))
                {
                    for (int j = i + 1; j < lines.Length && j <= i + 12; j++)
                    {
                        if (MemberRe.IsMatch(lines[j])) break;
                        tail += "\n" + lines[j];
                        if (lines[j].TrimEnd() == "    }") break;
                    }
                }

                var acc = AccessorRe.Match(tail);
                var alloc = AllocRe.Match(tail);

                foreach (var m in names)
                {
                    var name = m.Groups[1].Value;
                    var addr = uint.Parse(m.Groups[2].Value.AsSpan(2), NumberStyles.HexNumber);

                    if (acc.Success)
                        found.Add(new Symbol(name, addr, file, i + 1, SymbolKind.ArenaView, 0, acc.Groups[1].Value));
                    else if (alloc.Success)
                    {
                        found.Add(new Symbol(name, addr, file, i + 1, SymbolKind.OwnAllocation, AllocSize(alloc), null));
                    }
                    else
                        found.Add(new Symbol(name, addr, file, i + 1, SymbolKind.PlainField, 0, null));

                    tails.Add(tail);
                }
            }
        }

        ResolveDerivedViews(found, tails);
        return found;
    }

    /// <summary>
    /// Une vue peut se poser sur une AUTRE vue plutôt que sur l'accesseur d'arène :
    /// <c>s_PlayerSlots_005e9188 =&gt; s_SlotArea_005e9180 + 8</c>. Sans cette passe, ces symboles
    /// se comptaient en champs simples et noyaient le test 5 sous une trentaine de faux positifs.
    /// Itérée jusqu'au point fixe, une vue pouvant en désigner une troisième.
    /// </summary>
    private static void ResolveDerivedViews(List<Symbol> found, List<string> tails)
    {
        for (bool changed = true; changed;)
        {
            changed = false;
            var accessorOfView = found
                .Where(s => s.Kind == SymbolKind.ArenaView && s.ViaAccessor is not null)
                .GroupBy(s => s.Name)
                .ToDictionary(g => g.Key, g => g.First().ViaAccessor!);

            for (int k = 0; k < found.Count; k++)
            {
                if (found[k].Kind != SymbolKind.PlainField) continue;
                foreach (var (viewName, accessor) in accessorOfView)
                {
                    if (viewName == found[k].Name) continue;
                    if (!Regex.IsMatch(tails[k], @"\b" + Regex.Escape(viewName) + @"\b")) continue;
                    found[k] = found[k] with { Kind = SymbolKind.ArenaView, ViaAccessor = accessor };
                    changed = true;
                    break;
                }
            }
        }
    }

    private static List<Arena>? _arenas;

    /// <summary>Les blocs adressés par décalage, relevés dans <c>Memory/GameMemory.cs</c>.</summary>
    internal static IReadOnlyList<Arena> Arenas => _arenas ??= ReadArenas();

    private static List<Arena> ReadArenas()
    {
        var gm = Path.Combine(BlazeRoot, "Memory", "GameMemory.cs");
        var lines = Latin1.GetString(File.ReadAllBytes(gm)).Split('\n').Select(l => l.TrimEnd('\r')).ToArray();

        var bases = new Dictionary<string, uint>();       // nom logique -> base
        var baseRe = new Regex(@"internal const int (\w+?)Base\s*=\s*0x([0-9a-fA-F]+)", RegexOptions.Compiled);
        var sizeRe = new Regex(@"internal const int (\w+?)Size\s*=\s*0x([0-9a-fA-F]+)", RegexOptions.Compiled);
        var sizes = new Dictionary<string, uint>();

        foreach (var l in lines)
        {
            var b = baseRe.Match(l);
            if (b.Success) bases[b.Groups[1].Value] = uint.Parse(b.Groups[2].Value, NumberStyles.HexNumber);
            var s = sizeRe.Match(l);
            if (s.Success) sizes[s.Groups[1].Value] = uint.Parse(s.Groups[2].Value, NumberStyles.HexNumber);
        }

        // la taille d'une arène vient soit d'une const XxxSize, soit de son allocation
        var result = new List<Arena>();
        foreach (var (name, baseAddr) in bases)
        {
            uint size = sizes.TryGetValue(name, out var s) ? s : 0;
            if (size == 0)
            {
                // chercher l'allocation du champ dont le suffixe vaut la base
                var sym = Symbols.FirstOrDefault(
                    x => x.Address == baseAddr && x.Kind == SymbolKind.OwnAllocation);
                if (sym is not null) size = sym.DeclaredSize;
            }
            if (size == 0) continue;                      // pas mesurable : on ne devine pas
            result.Add(new Arena(name, name, baseAddr, size));
        }
        return result.OrderBy(a => a.Base).ToList();
    }

    /// <summary>L'arène qui couvre cette adresse, ou <c>null</c>.</summary>
    internal static Arena? ArenaCovering(uint va) => Arenas.FirstOrDefault(a => a.Covers(va));

    /// <summary>Rend un rapport lisible pour un échec, avec le fichier et la ligne de chaque symbole.</summary>
    internal static string Describe(IEnumerable<Symbol> symbols) =>
        string.Join(Environment.NewLine, symbols.Select(s => "    " + s));
}
