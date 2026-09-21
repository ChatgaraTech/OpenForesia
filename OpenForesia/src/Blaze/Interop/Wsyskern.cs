// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La surface P/Invoke de wsyskern.dll. Meme raisonnement que Winsys3d : pas d'interface.
//
// `static` par NECESSITE et non par paresse : [DllImport] exige `extern static`, et il n'y a
// aucun etat a porter. Une classe statique est ici la forme correcte.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

internal static unsafe class Wsyskern
{
    // wsyskern.dll: named "utility kernel" export table (bitmap/resource/hot-region helpers).
    // Ghidra recovered its name directly from the import table. Signature confirmed against
    // wsyskern.c (decompiled DLL source, ordinal 114): loads an RT_BITMAP resource, builds and
    // realizes an HPALETTE from its DIB header, and returns the created HBITMAP.
    // See Documentation/DLL/wsyskern.md.
    /// <summary>
    /// Porte : <c>Libraries/wsyskern/Images.cs</c>, wsyskern.c LoadBitmapEx @ 0x1000bac0.
    ///
    /// <c>out</c> est conserve pour ne pas toucher au site d'appel, mais **le corps d'origine ne
    /// garantit pas l'ecriture** : ressource introuvable, il saute tout le bloc et laisse la
    /// variable de l'appelant telle quelle. Passer son adresse reproduit cela exactement — la
    /// memoire de l'appelant est intacte —, la ou un `out` C# promettrait une affectation. L'unique
    /// site d'appel (<c>Platform/Startup.cs:193</c>) teste le HBITMAP rendu avant de lire la
    /// palette, donc la difference n'est pas observable ; elle est notee parce qu'un second
    /// appelant qui lirait la palette en premier verrait 1998, pas C#.
    /// </summary>
    internal static IntPtr LoadBitmapEx(IntPtr hInstance, IntPtr resourceIdOrName, out IntPtr phPalette)
    {
        fixed (IntPtr* palette = &phPalette)
            return Libraries.Images.LoadBitmapEx(hInstance, (byte*)resourceIdOrName, palette);
    }

    // wsyskern.dll ordinal 115 (named export, wsyskern.c:13885 @ 0x10010500). Reads a whole .BMP
    // file into a wsyskern-heap buffer and returns a pointer to its raw BITMAPFILEHEADER-prefixed
    // image, or null on failure. `mode` selects the failure reporting: 0 = silent, 1 = fatal
    // `filenotfound`/`fileerror` message box, 2 = `warnfilenotfound`/`warnfileerror` warning.
    // The only Blaze.c call site (0x00542c40, the attract title screen) passes 0, i.e. a missing
    // file must fail silently and is a normal, handled outcome.
    //
    // IMPORTANT: the returned buffer comes from wsyskern's own heap (its `Malloc`, ordinal 124,
    // wsyskern.c:9769), so it must be released with wsyskern's `Free` below â€” NOT with this file's
    // CRT-derived Free, which maps to NativeMemory and would corrupt the heap.
    /// <summary>
    /// Porte : <c>Libraries/wsyskern/Fichiers.cs</c>, wsyskern.c LoadBitmapFile @ 0x10010500.
    ///
    /// Le tampon rendu vient toujours du **tas de la DLL** : la version C# alloue par
    /// <c>Wsyskern.Malloc</c>, qui reste une passerelle vers l'export natif tant que la DLL n'est
    /// pas portee en entier. Le rendre par <c>WsysFree</c> reste donc correct, et le restera quand
    /// la paire basculera — les deux basculeront ensemble.
    /// </summary>
    internal static byte* LoadBitmapFile(byte* fileName, uint mode)
        => Libraries.Fichiers.LoadBitmapFile(fileName, mode);

    // wsyskern.dll ordinal 89 (named export, wsyskern.c:9818 @ 0x1000bc30). The matching deallocator
    // for anything wsyskern's `Malloc` produced. Blaze.c calls it exactly twice, both on the attract
    // screen's title bitmap (Blaze.c:89940 and 90131) â€” note it is the capitalized `Free`/`Malloc`
    // pair Ghidra resolved from the import table, a different heap from the lowercase CRT
    // malloc/free chain (FUN_00547220/FUN_00547210) this file maps onto NativeMemory.
    // Passing null is safe: it forwards to a CRT free, and Blaze.c itself calls it unconditionally.
    [DllImport("wsyskern.dll", EntryPoint = "Free")]
    internal static extern void WsysFree(byte* block);

    // wsyskern ordinal 124, `Malloc` : le tas de la DLL. **Pas appele par le jeu** — il est ici
    // parce que `src/Libraries/wsyskern/` en a besoin tant que la DLL n'est pas portee en entier.
    // Un seul tas jusque-la ; voir src/Libraries/LISEZMOI.md.
    [DllImport("wsyskern.dll", ExactSpelling = true)]
    internal static extern void* Malloc(int size);

    // =========================================================================================
    // CE QUI SUIT EST PORTE EN C# — `src/Libraries/wsyskern/`.
    //
    // La facade ne change pas de forme : les appelants ecrivent toujours `Wsyskern.Gamma(...)`, et
    // seul le corps bascule du `[DllImport]` vers le renvoi. C'est ce qui permet de debrancher un
    // point d'entree a la fois sans toucher a un seul site d'appel.
    // =========================================================================================

    /// <summary>Porte : <c>Libraries/wsyskern/Images.cs</c>, wsyskern.c DIBCopyBlock @ 0x10006580.</summary>
    internal static int DIBCopyBlock(void* destinationBits, void* destinationInfo,
        int destinationX, int destinationY, uint width, uint height,
        void* sourceBits, void* sourceInfo,
        int sourceX, int sourceY)
        => Libraries.Images.DIBCopyBlock(destinationBits, destinationInfo, destinationX, destinationY,
                                         width, height, sourceBits, sourceInfo, sourceX, sourceY);

    /// <summary>
    /// wsyskern ordinal 91. Ghidra types it `void`; it returns the adjusted byte in AL.
    /// Porte : <c>Libraries/wsyskern/Images.cs</c>, wsyskern.c Gamma @ 0x100061c0.
    /// </summary>
    internal static byte Gamma(int value, double gamma) => Libraries.Images.Gamma(value, gamma);

    // La famille tableau, portee d'un bloc : `Libraries/wsyskern/Tableaux.cs`. Les quatre partent
    // ENSEMBLE parce qu'elles partagent une representation — un objet C++ de vingt octets avec sa
    // vtable. En porter une seule aurait fait construire par l'un ce que l'autre dereference.

    /// <summary>Porte : wsyskern.c CreateArray @ 0x10009f00.</summary>
    internal static void* CreateArray(int kind, void* capacity)
        => Libraries.Tableaux.CreateArray(kind, (int)capacity);

    /// <summary>Porte : wsyskern.c LoadArray @ 0x1000a190.</summary>
    internal static void* LoadArray(void* array, byte* path, int startIndex, int maxLines, int skip, int flags)
        => Libraries.Tableaux.LoadArray(array, path, startIndex, maxLines, skip, flags);

    /// <summary>Porte : wsyskern.c ArrayToString @ 0x1000a050.</summary>
    internal static void ArrayToString(void* array, int index, byte* buffer, int size)
        => Libraries.Tableaux.ArrayToString(array, index, buffer, size);

    /// <summary>Porte : wsyskern.c DeleteArray @ 0x10009fe0.</summary>
    internal static void DeleteArray(void* array) => Libraries.Tableaux.DeleteArray(array);

}
