using System.Reflection.PortableExecutable;
using System.Text;

// Verb dispatch. A bare path keeps the original behaviour (dump the export table) because
// Documentation/DLL/*.md documents that exact invocation for resolving ordinal-only imports.
if (args.Length == 0)
{
    PrintUsage();
    return;
}

if (args[0] is "sections")
{
    if (args.Length != 2) { PrintUsage(); return; }
    DumpSections(args[1]);
    return;
}

if (args[0] is "at")
{
    if (args.Length is < 3 or > 4) { PrintUsage(); return; }
    // Le compteur se lit en decimal (contrairement aux adresses, hexa par defaut comme Ghidra) :
    // "at <fichier> <va> 32" doit lire 32 octets, pas 0x32.
    var requestedBytes = 64;
    if (args.Length == 4 && !TryParseByteCount(args[3], out requestedBytes))
    {
        Console.WriteLine($"Nombre d'octets invalide : {args[3]}");
        return;
    }
    DumpAt(args[1], args[2], requestedBytes);
    return;
}

if (args[0] is "disasm")
{
    if (args.Length is < 3 or > 4) { PrintUsage(); return; }
    var requestedBytes = 128;
    if (args.Length == 4 && !TryParseByteCount(args[3], out requestedBytes))
    {
        Console.WriteLine($"Nombre d'octets invalide : {args[3]}");
        return;
    }
    Disassemble(args[1], args[2], requestedBytes);
    return;
}

// Les donnees que le jeu lisait encore dans les fichiers de winsys3d.dll et wsyskern.dll, pour ne
// plus livrer ces fichiers. Voir ExtractionDonnees.cs.
if (args[0] is "extraire-donnees")
{
    if (args.Length != 3) { PrintUsage(); return; }
    ExtractionDonnees.Run(args[1], args[2]);
    return;
}

// Le bestiaire ne lit pas Blaze.exe mais les donnees du CD : SYSTEM.INI (le repertoire racine) et
// BLAZE.ALL (l'archive). Voir Bestiaire.cs pour la chaine a quatre niveaux qu'il reproduit.
if (args[0] is "bestiaire")
{
    if (args.Length < 2) { PrintUsage(); return; }

    int? zone = null;
    string? outDir = null;
    var regions = false;
    for (var i = 2; i < args.Length; i++)
    {
        if (args[i] is "--zone" && i + 1 < args.Length && int.TryParse(args[i + 1], out var z)) { zone = z; i++; }
        else if (args[i] is "--sortie" && i + 1 < args.Length) { outDir = args[i + 1]; i++; }
        else if (args[i] is "--regions") { regions = true; }
        else { Console.WriteLine($"Option inconnue : {args[i]}"); PrintUsage(); return; }
    }

    Bestiaire.Run(args[1], zone, outDir, regions);
    return;
}

// Le simulateur de montee de niveau : le seul verbe qui ouvre une fenetre. Il ne lit ni le binaire
// ni le CD — il rejoue FUN_005391f0 sur un personnage qu'on compose a l'ecran. Voir
// MonteeDeNiveau/Simulateur.cs et Documentation/Reference/MonteeDeNiveau.md.
if (args[0] is "montee")
{
    if (args.Length != 1) { PrintUsage(); return; }

    // Les instructions de haut niveau ne sont pas STA, et WinForms l'exige : la fenetre part sur
    // son propre fil, et le verbe rend la main quand elle se ferme.
    var interfaceGraphique = new Thread(() =>
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.Run(new OpenForesia.Tools.MonteeDeNiveau.FenetreSimulateur());
    });
    interfaceGraphique.SetApartmentState(ApartmentState.STA);
    interfaceGraphique.Start();
    interfaceGraphique.Join();
    return;
}

if (args.Length != 1)
{
    PrintUsage();
    return;
}

var dllPath = Path.GetFullPath(args[0]);

if (!File.Exists(dllPath))
{
    Console.WriteLine($"DLL introuvable : {dllPath}");
    return;
}

Console.WriteLine("=== OpenForesia DLL Inspector ===");
Console.WriteLine($"DLL : {dllPath}");
Console.WriteLine();

var file = File.ReadAllBytes(dllPath);

using var stream = new MemoryStream(file);
using var peReader = new PEReader(stream);

var headers = peReader.PEHeaders;
var peHeader = headers.PEHeader;

if (peHeader is null)
{
    Console.WriteLine("Ce fichier n'est pas un PE Windows valide.");
    return;
}

Console.WriteLine($"Machine     : {headers.CoffHeader.Machine}");
Console.WriteLine($"Image base  : 0x{peHeader.ImageBase:X}");
Console.WriteLine($"Entry point : 0x{peHeader.AddressOfEntryPoint:X}");

var exportDirectoryEntry = peHeader.ExportTableDirectory;

if (exportDirectoryEntry.RelativeVirtualAddress == 0)
{
    Console.WriteLine("Aucune table d'exports.");
    return;
}

Console.WriteLine($"Export RVA  : 0x{exportDirectoryEntry.RelativeVirtualAddress:X}");
Console.WriteLine($"Export size : 0x{exportDirectoryEntry.Size:X}");
Console.WriteLine();

var exportDirectoryOffset =
    RvaToFileOffset(exportDirectoryEntry.RelativeVirtualAddress, headers);

if (exportDirectoryOffset < 0)
{
    Console.WriteLine("Impossible de localiser la table d'exports.");
    return;
}

// IMAGE_EXPORT_DIRECTORY = 40 bytes
var exportOffset = exportDirectoryOffset;

var characteristics = ReadUInt32(file, exportOffset + 0);
var timeDateStamp = ReadUInt32(file, exportOffset + 4);
var majorVersion = ReadUInt16(file, exportOffset + 8);
var minorVersion = ReadUInt16(file, exportOffset + 10);
var nameRva = ReadUInt32(file, exportOffset + 12);
var ordinalBase = ReadUInt32(file, exportOffset + 16);
var numberOfFunctions = ReadUInt32(file, exportOffset + 20);
var numberOfNames = ReadUInt32(file, exportOffset + 24);
var addressOfFunctionsRva = ReadUInt32(file, exportOffset + 28);
var addressOfNamesRva = ReadUInt32(file, exportOffset + 32);
var addressOfNameOrdinalsRva = ReadUInt32(file, exportOffset + 36);

var dllName = ReadAsciiStringAtRva(file, headers, nameRva);

Console.WriteLine($"Internal name       : {dllName}");
Console.WriteLine($"Ordinal base        : {ordinalBase}");
Console.WriteLine($"Number of functions : {numberOfFunctions}");
Console.WriteLine($"Number of names     : {numberOfNames}");
Console.WriteLine();

var functionsOffset =
    RvaToFileOffset((int)addressOfFunctionsRva, headers);

var namesOffset =
    RvaToFileOffset((int)addressOfNamesRva, headers);

var ordinalsOffset =
    RvaToFileOffset((int)addressOfNameOrdinalsRva, headers);

if (functionsOffset < 0 ||
    namesOffset < 0 ||
    ordinalsOffset < 0)
{
    Console.WriteLine("Impossible de lire les tables d'exports.");
    return;
}

// Associe l'index dans AddressOfFunctions à son nom éventuel.
var namesByFunctionIndex = new Dictionary<int, string>();

for (var i = 0; i < numberOfNames; i++)
{
    var functionNameRva =
        ReadUInt32(file, namesOffset + i * 4);

    var functionIndex =
        ReadUInt16(file, ordinalsOffset + i * 2);

    var functionName =
        ReadAsciiStringAtRva(file, headers, functionNameRva);

    namesByFunctionIndex[functionIndex] = functionName;
}

Console.WriteLine(
    $"{ "Ordinal",-10} {"RVA",-12} {"Address",-12} Name");

Console.WriteLine(
    $"{new string('-', 10)} " +
    $"{new string('-', 12)} " +
    $"{new string('-', 12)} " +
    $"{new string('-', 40)}");

var exportStartRva =
    exportDirectoryEntry.RelativeVirtualAddress;

var exportEndRva =
    exportStartRva + exportDirectoryEntry.Size;

for (var functionIndex = 0;
     functionIndex < numberOfFunctions;
     functionIndex++)
{
    var functionRva =
        ReadUInt32(file, functionsOffset + functionIndex * 4);

    var ordinal =
        ordinalBase + (uint)functionIndex;

    namesByFunctionIndex.TryGetValue(
        functionIndex,
        out var name);

    name ??= "<ordinal only>";

    // Une RVA nulle correspond à un trou dans la table.
    if (functionRva == 0)
    {
        Console.WriteLine(
            $"{ordinal,-10} {"<empty>",-12} {"",-12} {name}");

        continue;
    }

    var virtualAddress =
        peHeader.ImageBase + functionRva;

    // Si la RVA pointe dans la table d'exports elle-même,
    // il s'agit d'un forwarded export.
    var isForwarded =
        functionRva >= exportStartRva &&
        functionRva < exportEndRva;

    if (isForwarded)
    {
        var forwardedTo =
            ReadAsciiStringAtRva(file, headers, functionRva);

        Console.WriteLine(
            $"{ordinal,-10} " +
            $"0x{functionRva:X8}   " +
            $"FORWARDED    " +
            $"{name} -> {forwardedTo}");

        continue;
    }

    Console.WriteLine(
        $"{ordinal,-10} " +
        $"0x{functionRva:X8}   " +
        $"0x{virtualAddress:X8}   " +
        $"{name}");
}


// ============================================================
// Helpers
// ============================================================

static void PrintUsage()
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  OpenForesia.Tools <path>                     table d'exports (comportement d'origine)");
    Console.WriteLine("  OpenForesia.Tools sections <path>            sections PE : plages VA, offsets fichier");
    Console.WriteLine("  OpenForesia.Tools at <path> <va> [octets]    dumpe les octets a une adresse virtuelle Ghidra");
    Console.WriteLine("  OpenForesia.Tools disasm <path> <va> [octets] desassemble x86-32 a partir d'une VA Ghidra");
    Console.WriteLine("  OpenForesia.Tools bestiaire <cd> [--zone N] [--sortie <dir>]");
    Console.WriteLine("                                               especes, fiches, formations et butin, lus sur le CD");
    Console.WriteLine("  OpenForesia.Tools montee                     simulateur de montee de niveau (fenetre)");
    Console.WriteLine();
    Console.WriteLine("Exemples:");
    Console.WriteLine("  OpenForesia.Tools Legacy/Binaries/winsys3d.dll");
    Console.WriteLine("  OpenForesia.Tools sections Legacy/Binaries/Blaze.exe");
    Console.WriteLine("  OpenForesia.Tools at Legacy/Binaries/Blaze.exe 0x005631f8");
    Console.WriteLine("  OpenForesia.Tools disasm Legacy/Binaries/Blaze.exe 0x004747f0 723");
    Console.WriteLine();
    Console.WriteLine("Les adresses sont lues en hexadecimal (prefixe 0x optionnel), comme dans Ghidra.");
    Console.WriteLine("Le nombre d'octets est lu en decimal (prefixer 0x pour de l'hexa).");
}


// Accepte "0x005631f8", "005631f8" (hex par defaut, comme Ghidra) ou "d:64" pour un decimal.
static bool TryParseNumber(
    string text,
    out int value)
{
    value = 0;

    if (string.IsNullOrWhiteSpace(text))
    {
        return false;
    }

    if (text.StartsWith("d:", StringComparison.OrdinalIgnoreCase))
    {
        return int.TryParse(text.AsSpan(2), out value);
    }

    var digits = text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
        ? text.AsSpan(2)
        : text.AsSpan();

    return int.TryParse(
        digits,
        System.Globalization.NumberStyles.HexNumber,
        System.Globalization.CultureInfo.InvariantCulture,
        out value);
}


// Compteur d'octets : decimal par defaut, "0x" pour l'hexa. Volontairement different de
// TryParseNumber, dont l'hexa-par-defaut a du sens pour une adresse Ghidra mais pas pour un nombre.
static bool TryParseByteCount(
    string text,
    out int value)
{
    value = 0;

    if (string.IsNullOrWhiteSpace(text))
    {
        return false;
    }

    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
    {
        return int.TryParse(
            text.AsSpan(2),
            System.Globalization.NumberStyles.HexNumber,
            System.Globalization.CultureInfo.InvariantCulture,
            out value);
    }

    return int.TryParse(text, out value);
}


// Ouvre le PE et renvoie ses headers + le contenu brut, ou null en cas d'echec (message deja affiche).
static (byte[] File, PEHeaders Headers)? OpenPortableExecutable(
    string path)
{
    var fullPath = Path.GetFullPath(path);

    if (!File.Exists(fullPath))
    {
        Console.WriteLine($"Fichier introuvable : {fullPath}");
        return null;
    }

    var bytes = File.ReadAllBytes(fullPath);

    using var stream = new MemoryStream(bytes);
    using var reader = new PEReader(stream);

    if (reader.PEHeaders.PEHeader is null)
    {
        Console.WriteLine("Ce fichier n'est pas un PE Windows valide.");
        return null;
    }

    Console.WriteLine($"Fichier     : {fullPath}");
    Console.WriteLine($"Image base  : 0x{reader.PEHeaders.PEHeader.ImageBase:X8}");
    Console.WriteLine();

    return (bytes, reader.PEHeaders);
}


// Retrouve la section contenant une RVA. Renvoie null si l'adresse ne tombe dans aucune section :
// c'est ce qui rend une mauvaise adresse diagnostiquable au lieu de silencieusement plausible.
static string? SectionNameForRva(
    int rva,
    PEHeaders headers)
{
    foreach (var section in headers.SectionHeaders)
    {
        var size = Math.Max(
            section.VirtualSize,
            section.SizeOfRawData);

        if (rva >= section.VirtualAddress &&
            rva < section.VirtualAddress + size)
        {
            return section.Name;
        }
    }

    return null;
}


static void DumpSections(
    string path)
{
    var pe = OpenPortableExecutable(path);

    if (pe is null)
    {
        return;
    }

    var headers = pe.Value.Headers;
    var imageBase = headers.PEHeader!.ImageBase;

    Console.WriteLine(
        $"{"Section",-10} {"VA debut",-12} {"VA fin",-12} " +
        $"{"RVA",-12} {"Taille",-10} Offset fichier");

    Console.WriteLine(new string('-', 78));

    foreach (var section in headers.SectionHeaders)
    {
        var size = Math.Max(
            section.VirtualSize,
            section.SizeOfRawData);

        var startVa = imageBase + (ulong)section.VirtualAddress;
        var endVa = startVa + (ulong)size;

        Console.WriteLine(
            $"{section.Name,-10} " +
            $"0x{startVa:X8}   " +
            $"0x{endVa:X8}   " +
            $"0x{section.VirtualAddress:X8}   " +
            $"0x{size:X6}   " +
            $"0x{section.PointerToRawData:X8}");
    }
}


static void DumpAt(
    string path,
    string virtualAddressText,
    int byteCount)
{
    if (!TryParseNumber(virtualAddressText, out var virtualAddress))
    {
        Console.WriteLine($"Adresse invalide : {virtualAddressText}");
        return;
    }

    if (byteCount <= 0)
    {
        Console.WriteLine("Le nombre d'octets doit etre positif.");
        return;
    }

    var pe = OpenPortableExecutable(path);

    if (pe is null)
    {
        return;
    }

    var file = pe.Value.File;
    var headers = pe.Value.Headers;

    var imageBase = (long)headers.PEHeader!.ImageBase;
    var rva = (int)(virtualAddress - imageBase);

    if (rva < 0)
    {
        Console.WriteLine(
            $"L'adresse 0x{virtualAddress:X8} est inferieure a l'image base " +
            $"0x{imageBase:X8} — ce n'est pas une adresse virtuelle de ce binaire.");

        return;
    }

    var sectionName = SectionNameForRva(rva, headers);
    var offset = RvaToFileOffset(rva, headers);

    // Echec bruyant plutot qu'un offset plausible mais faux.
    if (sectionName is null)
    {
        Console.WriteLine(
            $"L'adresse 0x{virtualAddress:X8} (RVA 0x{rva:X8}) ne tombe dans aucune " +
            $"section de ce fichier. Verifiez l'adresse, ou listez les sections avec " +
            $"'sections'.");

        return;
    }

    // Cas distinct du precedent, et c'est une *reponse*, pas une erreur : l'adresse est bien dans
    // une section, mais au-dela de ses donnees brutes. La VirtualSize d'une section peut depasser
    // sa SizeOfRawData ; cette queue n'existe pas dans le fichier et le chargeur la remplit de
    // zeros (donnees non initialisees, style BSS). Un symbole qui atterrit la vaut exactement zero
    // au demarrage et est renseigne a l'execution — le confondre avec "adresse invalide" ferait
    // chercher un bug la ou il n'y en a pas.
    if (offset < 0 || offset >= file.Length)
    {
        Console.WriteLine($"VA          : 0x{virtualAddress:X8}");
        Console.WriteLine($"RVA         : 0x{rva:X8}");
        Console.WriteLine($"Section     : {sectionName}");
        Console.WriteLine();
        Console.WriteLine(
            "Donnees NON INITIALISEES : l'adresse est dans la section, mais au-dela de ses");
        Console.WriteLine(
            "octets presents dans le fichier. Le chargeur met cette zone a zero ; la valeur");
        Console.WriteLine(
            "est donc 0 au demarrage et ne peut venir que du code, pas de la section de donnees.");

        return;
    }

    var available = Math.Min(byteCount, file.Length - offset);

    Console.WriteLine($"VA          : 0x{virtualAddress:X8}");
    Console.WriteLine($"RVA         : 0x{rva:X8}");
    Console.WriteLine($"Offset      : 0x{offset:X8} ({offset})");
    Console.WriteLine($"Section     : {sectionName}");
    Console.WriteLine($"Octets lus  : {available}");
    Console.WriteLine();

    // --- hex + ascii ---
    Console.WriteLine("Hex:");

    for (var lineStart = 0; lineStart < available; lineStart += 16)
    {
        var lineLength = Math.Min(16, available - lineStart);

        var hex = new StringBuilder();
        var ascii = new StringBuilder();

        for (var i = 0; i < lineLength; i++)
        {
            var b = file[offset + lineStart + i];

            hex.Append($"{b:x2}");

            if (i % 2 == 1)
            {
                hex.Append(' ');
            }

            ascii.Append(
                b >= 0x20 && b < 0x7f
                    ? (char)b
                    : '.');
        }

        Console.WriteLine(
            $"  0x{virtualAddress + lineStart:X8}: {hex.ToString().PadRight(41)} {ascii}");
    }

    Console.WriteLine();

    // --- chaine C ---
    var stringEnd = offset;

    while (stringEnd < file.Length &&
           stringEnd < offset + available &&
           file[stringEnd] != 0)
    {
        stringEnd++;
    }

    var asciiString = Encoding.ASCII.GetString(
        file,
        offset,
        stringEnd - offset);

    Console.WriteLine(
        stringEnd < offset + available
            ? $"Chaine C    : \"{asciiString}\""
            : $"Chaine C    : \"{asciiString}\" (pas de NUL dans la fenetre lue)");

    Console.WriteLine();

    // --- dwords little-endian ---
    Console.WriteLine("Dwords (little-endian):");

    for (var i = 0; i + 4 <= available; i += 4)
    {
        var dword = ReadUInt32(file, offset + i);

        // Une valeur qui retombe dans l'image est probablement un pointeur : le signaler
        // fait gagner du temps sur les tables de pointeurs (PTR_FUN_*, tables de chaines).
        var looksLikePointer =
            dword >= (uint)imageBase &&
            dword < (uint)imageBase + 0x1000000;

        Console.WriteLine(
            $"  +0x{i:X2}  0x{dword:X8}" +
            (looksLikePointer ? "   <- pointeur potentiel" : string.Empty));
    }
}


static int RvaToFileOffset(
    int rva,
    PEHeaders headers)
{
    foreach (var section in headers.SectionHeaders)
    {
        var sectionStart = section.VirtualAddress;

        var sectionSize = Math.Max(
            section.VirtualSize,
            section.SizeOfRawData);

        var sectionEnd =
            sectionStart + sectionSize;

        if (rva >= sectionStart &&
            rva < sectionEnd)
        {
            var rvaInSection = rva - sectionStart;

            // DEFAUT CORRIGE LE 2026-08-21, et il rendait des reponses fausses avec aplomb.
            //
            // Sans ce test, une RVA tombant dans la QUEUE NON INITIALISEE d'une section
            // (VirtualSize > SizeOfRawData) donnait quand meme PointerToRawData + decalage — un
            // offset qui atterrit dans les octets bruts de la section SUIVANTE. L'appelant ne
            // rattrapait rien : il ne testait que `offset >= file.Length`, or cet offset-la est
            // parfaitement dans le fichier.
            //
            // Concretement, `at Blaze.exe 0x00571500` rendait une table dense de valeurs
            // 0x00208xxx qui n'a jamais existe a cette adresse : c'etait .idata (RVA 0x00208000),
            // lue par accident. Une note du depot en avait tire une « TABLE INITIALISEE a
            // 0x00571400 » et s'en etait servi pour borner une arene. Rendre -1 ici fait dire a
            // l'appelant ce qui est vrai : zero au chargement.
            if (rvaInSection >= section.SizeOfRawData)
            {
                return -1;
            }

            return section.PointerToRawData
                   + rvaInSection;
        }
    }

    // Certaines RVA peuvent être situées directement
    // dans les headers PE.
    if (rva < headers.PEHeader?.SizeOfHeaders)
    {
        return rva;
    }

    return -1;
}


static string ReadAsciiStringAtRva(
    byte[] file,
    PEHeaders headers,
    uint rva)
{
    var offset =
        RvaToFileOffset((int)rva, headers);

    if (offset < 0)
    {
        return "<invalid RVA>";
    }

    var end = offset;

    while (end < file.Length &&
           file[end] != 0)
    {
        end++;
    }

    return Encoding.ASCII.GetString(
        file,
        offset,
        end - offset);
}


static ushort ReadUInt16(
    byte[] data,
    int offset)
{
    return BitConverter.ToUInt16(
        data,
        offset);
}


static uint ReadUInt32(
    byte[] data,
    int offset)
{
    return BitConverter.ToUInt32(
        data,
        offset);
}

// =================================================================================================
// disasm — le desassembleur x86-32, ajoute le 2026-08-21.
//
// POURQUOI IL EXISTE. `Blaze.c` est une decompilation *incomplete* : Ghidra a laisse des corps
// entiers non decompiles (adresses seulement prises, procedures de fenetre, et surtout
// FUN_004747F0). Jusqu'ici la seule issue documentee etait « une re-passe Ghidra ciblee », c'est-a-
// dire une action manuelle hors du depot, que personne ne pouvait faire depuis une session de
// travail. Ce verbe la remplace : le binaire est dans le depot, la table des sections aussi, il ne
// manquait que le decodeur.
//
// Il ne remplace PAS Ghidra — pas de reconstruction de flot, pas de types, pas de noms. Il rend le
// desassemblage, ce qui suffit a transcrire une fonction a la main quand la pseudo-C manque.
// =================================================================================================
static void Disassemble(
    string path,
    string addressText,
    int byteCount)
{
    if (!TryParseNumber(addressText, out var virtualAddress))
    {
        Console.WriteLine($"Adresse invalide : {addressText}");
        return;
    }

    var opened = OpenPortableExecutable(path);
    if (opened is null) return;
    var (file, headers) = opened.Value;

    var imageBase = (int)headers.PEHeader!.ImageBase;
    var rva = virtualAddress - imageBase;
    var offset = RvaToFileOffset(rva, headers);
    if (offset < 0)
    {
        Console.WriteLine($"L'adresse 0x{virtualAddress:x8} ne tombe dans aucune section.");
        return;
    }

    var available = file.Length - offset;
    if (byteCount > available) byteCount = available;

    var section = SectionNameForRva(rva, headers) ?? "?";
    Console.WriteLine($"{path}  base 0x{imageBase:x8}");
    Console.WriteLine($"0x{virtualAddress:x8}  section {section}  offset fichier 0x{offset:x}  {byteCount} octets");
    Console.WriteLine();

    var code = new byte[byteCount];
    Array.Copy(file, offset, code, 0, byteCount);

    var decoder = Iced.Intel.Decoder.Create(32, new Iced.Intel.ByteArrayCodeReader(code));
    decoder.IP = (ulong)(uint)virtualAddress;
    var end = decoder.IP + (ulong)byteCount;

    var formatter = new Iced.Intel.NasmFormatter();
    formatter.Options.DigitSeparator = "";
    formatter.Options.FirstOperandCharIndex = 10;
    var output = new Iced.Intel.StringOutput();

    // Les cibles de saut internes, relevees en premiere passe : une fonction non decompilee se lit
    // par ses convergences, et savoir quelles adresses sont visees est la moitie du travail.
    var targets = new SortedSet<ulong>();
    var firstPass = Iced.Intel.Decoder.Create(32, new Iced.Intel.ByteArrayCodeReader(code));
    firstPass.IP = (ulong)(uint)virtualAddress;
    while (firstPass.IP < end)
    {
        var probe = firstPass.Decode();
        if (probe.IsJccShortOrNear || probe.Mnemonic == Iced.Intel.Mnemonic.Jmp)
        {
            if (probe.Op0Kind is Iced.Intel.OpKind.NearBranch32 or Iced.Intel.OpKind.NearBranch16)
            {
                var t = probe.NearBranchTarget;
                if (t >= (ulong)(uint)virtualAddress && t < end) targets.Add(t);
            }
        }
    }

    while (decoder.IP < end)
    {
        var ip = decoder.IP;
        var instruction = decoder.Decode();

        if (targets.Contains(ip))
        {
            Console.WriteLine();
            Console.WriteLine($"LAB_{ip:x8}:");
        }

        output.Reset();
        formatter.Format(instruction, output);

        var length = instruction.Length;
        var bytes = new StringBuilder();
        for (var i = 0; i < length; i++)
            bytes.Append(code[(int)(ip - (ulong)(uint)virtualAddress) + i].ToString("x2"));

        Console.WriteLine($"  {ip:x8}  {bytes,-20}  {output.ToStringAndReset()}");

        if (instruction.Code == Iced.Intel.Code.INVALID)
        {
            Console.WriteLine("  (octet invalide — la plage demandee deborde probablement du code)");
            return;
        }
    }

    if (targets.Count > 0)
    {
        Console.WriteLine();
        Console.WriteLine($"{targets.Count} cible(s) de saut interne : " +
                          string.Join(", ", targets.Select(t => $"0x{t:x8}")));
    }
}
