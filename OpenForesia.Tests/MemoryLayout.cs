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

    private static IEnumerable<(string File, int Line, string Text)> SourceLines()
    {
        foreach (var path in Directory.EnumerateFiles(BlazeRoot, "*.cs", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(BlazeRoot, path).Replace('\\', '/');
            var lines = Latin1.GetString(File.ReadAllBytes(path)).Split('\n');
            for (int i = 0; i < lines.Length; i++)
                yield return (rel, i + 1, lines[i].TrimEnd('\r'));
        }
    }

    // un membre de premier niveau, éventuellement précédé d'un attribut
    private static readonly Regex MemberRe = new(
        @"^    (?:\[[^\]]*\]\s*)?(?:private|internal|public|protected)\s",
        RegexOptions.Compiled);

    // le suffixe d'adresse : s_Quelquechose_005e9000
    private static readonly Regex AddrRe = new(
        @"\b(s_[A-Za-z0-9_]*?_(00[0-9a-f]{6}))\b", RegexOptions.Compiled);

    private static readonly Regex AllocRe = new(
        @"(?:NativeMemory\.Alloc(?:Zeroed)?\s*\(\s*(0x[0-9a-fA-F]+|\d+)|new\s+\w+\[\s*(0x[0-9a-fA-F]+|\d+)\s*\])",
        RegexOptions.Compiled);

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

    private static List<Symbol> ReadSymbols()
    {
        var found = new List<Symbol>();
        foreach (var (file, line, text) in SourceLines())
        {
            if (!MemberRe.IsMatch(text)) continue;

            // le nom déclaré est le PREMIER symbole à suffixe d'adresse de la ligne ; ceux qui
            // suivent appartiennent à l'initialiseur.
            var m = AddrRe.Match(text);
            if (!m.Success) continue;

            var name = m.Groups[1].Value;
            var addr = uint.Parse(m.Groups[2].Value.AsSpan(2), NumberStyles.HexNumber);

            var tail = text[(m.Index + m.Length)..];

            var acc = AccessorRe.Match(tail);
            if (acc.Success)
            {
                found.Add(new Symbol(name, addr, file, line, SymbolKind.ArenaView, 0, acc.Groups[1].Value));
                continue;
            }

            var alloc = AllocRe.Match(tail);
            if (alloc.Success)
            {
                var raw = alloc.Groups[1].Success ? alloc.Groups[1].Value : alloc.Groups[2].Value;
                found.Add(new Symbol(name, addr, file, line, SymbolKind.OwnAllocation, ParseNum(raw), null));
                continue;
            }

            found.Add(new Symbol(name, addr, file, line, SymbolKind.PlainField, 0, null));
        }
        return found;
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
