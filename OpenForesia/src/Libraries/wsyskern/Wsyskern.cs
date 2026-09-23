// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c.
//
// SOURCE DE VERITE : `wsyskern.c`, jamais `Blaze.c`. Voir src/Libraries/LISEZMOI.md pour la regle
// de tracabilite et pour les trois pieges de representation partagee.
//
// Base de chargement : 0x10000000 (ImageBase du PE, verifiee contre `/* entry @ 10014eb0 */`).
//
// CE FICHIER porte la racine : l'etat par thread que la DLL expose a son hote, et les deux
// passerelles de tas. Les corps transcrits sont ranges par sujet dans les fichiers voisins.

using System;

namespace OpenForesia.Libraries;

internal static unsafe class Wsyskern
{
    // =====================================================================
    // LES DEUX CROCHETS D'HOTE, et pourquoi ce sont des champs statiques.
    //
    // `ErrorMessage` et `WarningMessage` lisent deux pointeurs de fonction a `+0x11c` et `+0x120`
    // du bloc que la DLL tient PAR THREAD :
    //
    //     iVar1 = *(int *)((int)ThreadLocalStoragePointer + _tls_index * 4);
    //     if (*(code **)(iVar1 + 0x120) == (code *)0x0) { ... MessageBoxA ... }
    //     (**(code **)(iVar1 + 0x11c))();
    //
    // `+0x120` est le gestionnaire de message que l'hote peut installer, `+0x11c` la sortie
    // appelee apres une erreur fatale. Le stockage par thread est celui de la DLL ; le jeu est
    // mono-thread pour tout ce qui passe par ici — un seul thread ouvre des fichiers et charge des
    // images —, donc deux champs statiques reproduisent exactement le comportement observable.
    // Si un jour un second thread entrait ici, ce commentaire est l'endroit ou le dire.
    //
    // TODO(REVERSE): quelle fonction de wsyskern installe `+0x120` et `+0x11c` ? Aucun appelant
    // n'est dans la cloture actuelle, donc le jeu ne les pose vraisemblablement jamais et les deux
    // restent nuls — c'est la branche `MessageBoxA` qui s'execute. A confirmer en relevant les
    // ecritures a ces deux decalages dans wsyskern.c.
    // =====================================================================

    /// <summary>wsyskern.c, bloc par thread +0x120 — le gestionnaire de message de l'hote.</summary>
    internal static delegate* unmanaged<byte*, int, void> MessageHook;

    /// <summary>wsyskern.c, bloc par thread +0x11c — la sortie appelee apres une erreur fatale.</summary>
    internal static delegate* unmanaged<void> FatalExit;

    /// <summary>
    /// wsyskern.c, bloc par thread +0x124 — le caractere d'echappement des lignes de donnees, lu
    /// par `FUN_1000fe80` (voir `Tableaux.RemplacerEchappements`).
    ///
    /// **Zero par defaut, et rien dans la cloture ne l'ecrit** : la branche d'echappement ne se
    /// declenche donc jamais, la boucle s'arretant sur l'octet nul avant. Seuls `~` et `^` sont
    /// traduits en pratique. Meme bloc et meme question ouverte que les deux crochets ci-dessus.
    /// </summary>
    internal static byte CaractereEchappement;

    // =====================================================================
    // LE TAS — PORTE LE 2026-09-23, APRES DEUX PASSERELLES DELIBEREES.
    //
    // `Malloc` (export 124) est `_malloc` suivi d'un remplissage a zero, `Free` (export 89) est
    // `_free` sous un cadre SEH monte a la main.
    //
    // Ils sont restes des passerelles vers l'export natif tant qu'un code natif allouait ou
    // liberait dans le tas de la DLL un bloc qui traversait vers le C# : un seul tas, sinon l'un
    // rend a l'autre ce qu'il n'a pas pris. Le dernier cas est tombe le 2026-09-23 avec le portage
    // des rappels de `winsys3d` (la procedure de fenetre liberait des emplacements du cache, le
    // rappel des pilotes allouait des noms que le C# libere). Releve au desassembleur, ce qui reste
    // natif et touche au tas se referme sur lui-meme : le point d'entree de `winsys3d` alloue la
    // VRAM et la libere lui-meme, et le C# ne la libere jamais. Voir `byebye-dll.md`.
    //
    // `_malloc` et `_free` sont la CRT liee dans la DLL : ils se REBRANCHENT (regle 4 du chantier)
    // sur l'allocateur de la CRT du processus, `NativeMemory`, au lieu de se transcrire. Le reste
    // des deux corps — la mise a zero, `outofmemory` — est transcrit.
    // =====================================================================

    /// <summary>
    /// Ghidra: wsyskern.c Malloc @ 0x1000bbc0 (export 124) — `_malloc(n)`, `outofmemory(n)` s'il rend
    /// nul, puis la mise a zero des `n` octets (mots puis octets). Un bloc de 0 octet est un
    /// pointeur valide, comme chez la CRT.
    /// </summary>
    internal static void* Malloc(uint size)
    {
        void* block = System.Runtime.InteropServices.NativeMemory.Alloc(size);
        if (block == null) Messages.OutOfMemory(size);
        new Span<byte>(block, (int)size).Clear();
        return block;
    }

    /// <summary>
    /// Ghidra: wsyskern.c Free @ 0x1000bc30 (export 89) — `_free`, sous un cadre SEH qui n'attrape
    /// rien d'autre que ce que `_free` leverait. Nul est accepte, comme par la CRT.
    /// </summary>
    internal static void Free(void* block) => System.Runtime.InteropServices.NativeMemory.Free(block);
}
