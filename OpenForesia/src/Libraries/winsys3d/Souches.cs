// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c.
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000 (ImageBase du PE, verifiee contre
// `/* entry @ 00a01bb0 */`). Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES SOUCHES VIDES — QUATRE CORPS POUR 149 EXPORTS
//
// `winsys3d.dll` est le portage sur Win32 de la bibliotheque PSY-Q de la PlayStation. Une grande
// part de cette bibliotheque n'a aucun sens sur PC : le coprocesseur de mouvement, les canaux DMA,
// les commandes du SPU, le controle du lecteur CD, les rappels de synchronisation verticale. Les
// porteurs ont garde les exports — pour que le source du jeu compile sans etre touche — et leur
// ont donne un corps vide.
//
// Le compte est verifiable et c'est le resultat le plus utile du releve : sur les **830 exports**
// de la DLL, **149 pointent sur l'un de ces quatre corps**, que les commentaires d'ordinal de
// Ghidra enumerent un par un dans chaque corps :
//
//     0x00a02490   `xor eax,eax ; ret`     rend 0, AUCUN argument      22 ordinaux
//     0x00a02870   `ret 4`                 rend rien, UN argument      41 ordinaux
//     0x00a02e20   `xor eax,eax ; ret 4`   rend 0, UN argument         53 ordinaux
//     0x00a26470   `ret`                   rend rien, AUCUN argument   33 ordinaux
//
// **L'ARITE EST PROUVEE, PAS SUPPOSEE**, et elle l'etait deja. En `stdcall` l'appele nettoie la
// pile, donc un corps partage par des ordinaux d'arites differentes serait impossible : il ne
// pourrait pas retirer tantot 0, tantot 4 octets. Le `ret N` de chaque corps tranche — deux corps
// a `ret 0`, deux a `ret 4` —, et les 21 declarations d'`Interop/Winsys3d.cs` concordent toutes.
//
// Ce controle n'est pas neuf : le depot l'avait fait le **2026-08-21**, et il y avait alors trouve
// un vrai defaut. `Ordinal_45` etait declare avec DEUX arguments, la ou son corps fait `ret 4` :
// chaque appel laissait quatre octets sur la pile. Le commentaire est reste en place dans
// `Interop/Winsys3d.cs` ; le relire avant de toucher a une signature evite de refaire le chemin.
// La transcription n'a fait que verifier que les 21 concordaient toujours.
//
// Ce que ces corps NE disent pas : pourquoi tel ordinal est vide. Un export stub peut etre une
// fonction PlayStation sans equivalent PC, ou une fonction que ce portage-ci n'a pas eu besoin
// d'ecrire. Le distinguer demanderait de retrouver le nom PSY-Q de chaque ordinal, ce que
// `playst.dll` — qui porte les 758 noms de Sony — permettrait sans doute. Non fait.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dSouches
{
    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a02490 @ 0x00a02490 — `xor eax,eax ; ret`. Rend zero, sans
    /// argument. Vingt-deux ordinaux y aboutissent, dont les quatre employes : 46, 64, 116, 129.
    /// </summary>
    internal static uint RendZero() => 0;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a02e20 @ 0x00a02e20 — `xor eax,eax ; ret 4`. Rend zero et retire
    /// un argument. **La plus employee des quatre** : cinquante-trois ordinaux, dont dix importes.
    /// </summary>
    internal static uint RendZeroUnArgument() => 0;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a26470 @ 0x00a26470 — `ret`. Ne fait rien, sans argument.
    /// Trente-trois ordinaux, dont trois importes : 66, 138, 771.
    /// </summary>
    internal static void Rien() { }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a02870 @ 0x00a02870 — `ret 4`. Ne fait rien et retire un argument.
    /// Quarante et un ordinaux, dont quatre importes : 63, 712, 720, 762.
    /// </summary>
    internal static void RienUnArgument() { }
}
