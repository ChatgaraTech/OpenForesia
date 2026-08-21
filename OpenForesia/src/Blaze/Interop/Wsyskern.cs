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
    [DllImport("wsyskern.dll")]
    internal static extern IntPtr LoadBitmapEx(IntPtr hInstance, IntPtr resourceIdOrName, out IntPtr phPalette);

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
    [DllImport("wsyskern.dll")]
    internal static extern byte* LoadBitmapFile(byte* fileName, uint mode);

    // wsyskern.dll ordinal 89 (named export, wsyskern.c:9818 @ 0x1000bc30). The matching deallocator
    // for anything wsyskern's `Malloc` produced. Blaze.c calls it exactly twice, both on the attract
    // screen's title bitmap (Blaze.c:89940 and 90131) â€” note it is the capitalized `Free`/`Malloc`
    // pair Ghidra resolved from the import table, a different heap from the lowercase CRT
    // malloc/free chain (FUN_00547220/FUN_00547210) this file maps onto NativeMemory.
    // Passing null is safe: it forwards to a CRT free, and Blaze.c itself calls it unconditionally.
    [DllImport("wsyskern.dll", EntryPoint = "Free")]
    internal static extern void WsysFree(byte* block);

    // wsyskern exports, all by name, on the same footing as LoadBitmapFile.
    [DllImport("wsyskern.dll", ExactSpelling = true)]
    internal static extern int DIBCopyBlock(void* destinationBits, void* destinationInfo,
    int destinationX, int destinationY, uint width, uint height,
    void* sourceBits, void* sourceInfo,
    int sourceX, int sourceY);

    /// <summary>wsyskern ordinal 91. Ghidra types it `void`; it returns the adjusted byte in AL.</summary>
    [DllImport("wsyskern.dll", ExactSpelling = true)]
    internal static extern byte Gamma(int value, double gamma);

    [DllImport("wsyskern.dll", ExactSpelling = true)] internal static extern void* CreateArray(int kind, void* capacity);

    [DllImport("wsyskern.dll", ExactSpelling = true)] internal static extern void* LoadArray(void* array, byte* path, int startIndex, int maxLines, int skip, int flags);

    [DllImport("wsyskern.dll", ExactSpelling = true)] internal static extern void ArrayToString(void* array, int index, byte* buffer, int size);

    [DllImport("wsyskern.dll", ExactSpelling = true)] internal static extern void DeleteArray(void* array);

}
