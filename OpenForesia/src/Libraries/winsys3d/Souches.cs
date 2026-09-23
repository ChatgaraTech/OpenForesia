// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c.
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000 (ImageBase du PE, verifiee contre
// `/* entry @ 00a01bb0 */`). Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES SOUCHES VIDES — HUIT CORPS POUR 223 EXPORTS, DONT SEPT TRANSCRITS
//
// `winsys3d.dll` est le portage sur Win32 de la bibliotheque PSY-Q de la PlayStation. Une grande
// part de cette bibliotheque n'a aucun sens sur PC : le coprocesseur de mouvement, les canaux DMA,
// les commandes du SPU, le controle du lecteur CD, les rappels de synchronisation verticale. Les
// porteurs ont garde les exports — pour que le source du jeu compile sans etre touche — et leur
// ont donne un corps vide.
//
// Le compte est verifiable et c'est le resultat le plus utile du releve : sur les **830 exports**
// de la DLL, **223 pointent sur l'un de ces huit corps**, que les commentaires d'ordinal de
// Ghidra enumerent un par un dans chaque corps :
//
//     adresse      forme                    rend / arite               ordinaux  importe
//     0x00a02490   `xor eax,eax ; ret`      0, AUCUN argument              22     46, 64, 116, 129
//     0x00a02870   `ret 4`                  rien, UN argument              41     63, 712, 720, 762
//     0x00a02e10   `xor eax,eax ; ret 8`    0, DEUX arguments              22     765
//     0x00a02e20   `xor eax,eax ; ret 4`    0, UN argument                 53     dix ordinaux
//     0x00a02e40   `ret 8`                  rien, DEUX arguments           41     767
//     0x00a26470   `ret`                    rien, AUCUN argument           33     66, 138, 771
//     0x00a046e0   `xor eax,eax ; ret 0Ch`  0, TROIS arguments              9     97
//     0x00a046f0   `mov eax,1 ; ret 4`      1, UN argument                  2     aucun
//
// **Quatre de ces huit ont ete trouvees le 2026-09-22**, et le releve du 2026-09-21 n'en voyait
// que quatre parce qu'il avait cherche par l'ordinal importe et non par le corps. Deux d'entre
// elles (`0x00a046e0`, `0x00a046f0`) vivent au milieu du `libapi`, juste apres l'ordinal 25 ; les
// deux autres (`0x00a02e10`, `0x00a02e40`) sont les jumelles a DEUX arguments de `0x00a02e20` et
// `0x00a02870`, a trente octets de distance, et rien ne les distingue dans la decompilation —
// Ghidra decompile les six exactement pareil, `return 0;` ou `return;`, sans parametre.
//
// **Comment on les trouve toutes** : ce ne sont pas les ordinaux qu'il faut compter mais les
// corps. `grep -c "0x2e10  *[0-9]" winsys3d.c` donne les 22 ordinaux de la troisieme ligne ; le
// meme grep sur chaque adresse de souche donne la colonne. La lecon vaut au-dela de ce fichier :
// **compter des references quand la question porte sur des corps** est l'erreur que
// `ListeAffichage.cs` documente deja pour 677/662, et elle s'est reproduite ici a l'envers.
//
// Sept des huit sont transcrites. La huitieme (`0x00a046f0`, ordinaux 28 et 73) ne l'est pas :
// personne ne l'importe, et une souche sans appelant n'a pas de raison d'exister en C#.
//
// **L'ARITE EST PROUVEE, PAS SUPPOSEE**, et elle l'etait deja. En `stdcall` l'appele nettoie la
// pile, donc un corps partage par des ordinaux d'arites differentes serait impossible : il ne
// pourrait pas retirer tantot 0, tantot 4 octets. Le `ret N` de chaque corps tranche — deux corps
// a `ret 0`, trois a `ret 4`, deux a `ret 8`, un a `ret 0Ch` —, et les declarations
// d'`Interop/Winsys3d.cs` concordent toutes.
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

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a02e10 @ 0x00a02e10 — `xor eax,eax ; ret 8`. Rend zero et retire
    /// DEUX arguments. Vingt-deux ordinaux, **dont un seul importe : 765**.
    /// </summary>
    internal static uint RendZeroDeuxArguments() => 0;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a02e40 @ 0x00a02e40 — `ret 8`. Ne fait rien et retire DEUX
    /// arguments. Quarante et un ordinaux, **dont un seul importe : 767**.
    /// </summary>
    internal static void RienDeuxArguments() { }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a046e0 @ 0x00a046e0 — `xor eax,eax ; ret 0Ch`. Rend zero et
    /// retire TROIS arguments. **Une cinquieme souche, trouvee le 2026-09-22** en portant la
    /// famille de fichiers : elle vit au milieu du `libapi` (juste apres l'ordinal 25) et non avec
    /// les quatre autres, ce qui explique qu'elle ait echappe au releve du 2026-09-21.
    ///
    /// Neuf ordinaux y aboutissent — 26, 33, 34, 97, 102, 105, 107, 126, 249 —, **dont un seul
    /// importe : 97**. Sa voisine `FUN_00a046f0` (`mov eax,1 ; ret 4`, ordinaux 28 et 73) est une
    /// sixieme souche, que rien n'importe ; elle n'est pas transcrite pour cette raison.
    ///
    /// L'arite sort du `ret 0Ch` et de nulle part ailleurs : Ghidra decompile ce corps
    /// `undefined4 FUN_00a046e0(void)`, sans parametre. Les trois arguments de la declaration
    /// d'`Interop/Winsys3d.cs` etaient donc justes, et c'est le desassemblage qui le prouve — le
    /// meme controle qui avait attrape `Ordinal_45` le 2026-08-21.
    /// </summary>
    internal static uint RendZeroTroisArguments() => 0;
}
