// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c.
//
// ============================================================================================
// LA SECTION CRITIQUE — ordinaux 40 et 41
//
// Deux corps d'une ligne chacun, sur la meme section `&DAT_00b6d6d8` :
//
//     FUN_00a04fd0 (40)   EnterCriticalSection(&DAT_00b6d6d8)
//     FUN_00a04fe0 (41)   LeaveCriticalSection(&DAT_00b6d6d8)
//     FUN_00a04ff0        InitializeCriticalSection(&DAT_00b6d6d8)   — NON EXPORTE
//
// **Cette section ne protege rien a l'interieur de la DLL.** `DAT_00b6d6d8` n'apparait que trois
// fois dans les 27 000 lignes de `winsys3d.c`, et ce sont exactement ces trois corps : aucune
// autre fonction de la DLL ne la prend. Elle n'existe que pour que le JEU puisse serialiser ses
// propres appels — elle lui est preteee, pas interne. C'est ce qui rend ce portage possible alors
// que `InitTrigTables` (337) reste bloque : il n'y a aucun participant natif a exclure.
//
// L'initialisation n'etant pas exportee, elle se fait chez l'original au chargement de la DLL.
// Ici, un `static` la porte — meme moment observable, la premiere prise.
//
// ============================================================================================
// POURQUOI KERNEL32 ET NON UN `lock` C#
//
// Le jeu **relache une fois de trop**. `Platform/Startup.cs` prend la section une fois (ligne 384,
// l'installation des gestionnaires de manette) et la relache DEUX fois : ligne 406, celle qui va
// avec, et ligne 541, sur le chemin d'extinction, sans prise correspondante.
//
// Les deux mondes ne traitent pas cela pareil :
//
//   - `LeaveCriticalSection` sur une section qu'on ne possede pas est un comportement **non
//     defini** au sens de la documentation, mais en pratique Win32 ne dit rien et ne leve rien ;
//   - `Monitor.Exit` sans possession leve `SynchronizationLockException`, immediatement.
//
// Un `lock` ou un `Monitor` ferait donc **planter l'extinction** la ou l'original passe sans
// broncher. On appelle les vraies fonctions Win32, sur notre propre `CRITICAL_SECTION` — ce qui
// est d'ailleurs la transcription litterale des deux corps, et ce que la regle du depot demande :
// pour un appel Win32 reconnu, on appelle Win32.
//
// **Ne pas « corriger » l'appel en trop.** Il est dans `Blaze.c`, il est donc dans le jeu de 1998,
// et le projet reproduit ses defauts au lieu de les reparer.
// ============================================================================================

using System.Runtime.InteropServices;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dSectionCritique
{
    /// <summary>
    /// &amp;DAT_00b6d6d8 — la section elle-meme. `RTL_CRITICAL_SECTION` fait 24 octets en 32 bits ;
    /// on en reserve 64 pour que la structure reste valable si l'ABI s'elargit, et parce que le
    /// cout est nul. Le bloc n'est jamais rendu : celui de la DLL ne l'etait pas non plus, il
    /// vivait dans son segment de donnees jusqu'a la fin du processus.
    /// </summary>
    private static readonly void* s_Section = Initialiser();

    private static void* Initialiser()
    {
        // Ghidra: winsys3d.c FUN_00a04ff0 @ 0x00a04ff0 — `InitializeCriticalSection`. Le corps
        // n'est pas exporte : chez l'original il tourne au chargement de la DLL. Ici c'est
        // l'initialiseur statique qui en tient lieu, donc au premier des deux appels.
        void* section = NativeMemory.AllocZeroed(64);
        Win32.InitializeCriticalSection(section);
        return section;
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a04fd0 @ 0x00a04fd0 (ordinal 40) — prend la section.</summary>
    internal static void Entrer() => Win32.EnterCriticalSection(s_Section);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04fe0 @ 0x00a04fe0 (ordinal 41) — relache la section.
    ///
    /// Appele une fois de plus qu'`Entrer` sur le chemin d'extinction du jeu ; voir le pave de
    /// tete. C'est pour cela que ce corps passe par `kernel32` et non par `Monitor`.
    /// </summary>
    internal static void Sortir() => Win32.LeaveCriticalSection(s_Section);
}
