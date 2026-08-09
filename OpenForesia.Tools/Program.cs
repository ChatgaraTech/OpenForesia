using System.Reflection.PortableExecutable;
using System.Text;

if (args.Length != 1)
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  OpenForesia.Tools <path-to-dll>");
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
            return section.PointerToRawData
                   + (rva - sectionStart);
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