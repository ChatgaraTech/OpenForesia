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
    // LE TAS — DEUX PASSERELLES PROVISOIRES, ET C'EST DELIBERE.
    //
    // `Malloc` (export 124) est `_malloc` suivi d'un remplissage a zero, `Free` (export 89) est
    // `_free` sous un cadre SEH monte a la main. Les deux sont transcriptibles en trois lignes.
    //
    // On ne le fait pas, et la raison n'est pas la difficulte : tant qu'une seule fonction de
    // `wsyskern.dll` reste native, elle alloue dans LE TAS DE LA DLL. `LoadBitmapFile` et
    // `LoadArray` y sont encore, et le jeu rend leurs blocs par `WsysFree` — c'est-a-dire par
    // `Free`. Porter `Free` sur le tas de .NET ferait rendre a un tas un bloc pris dans l'autre :
    // une corruption qui ne se diagnostique pas, et qui ne se manifeste pas au premier appel.
    //
    // Un seul tas, donc, jusqu'a ce que la DLL soit portee en entier. Ces deux renvois
    // disparaissent avec le dernier `[DllImport]` de wsyskern, pas avant. Voir LISEZMOI.md,
    // « ne pas couper une representation en deux ».
    // =====================================================================

    // Ghidra: wsyskern.c Malloc @ 0x1000bbc0 (export 124) — `_malloc` puis remplissage a zero,
    // avec `outofmemory` si le tas rend nul. Passerelle : voir le pave ci-dessus.
    internal static void* Malloc(uint size)
    {
        void* block = Blaze.Wsyskern.Malloc((int)size);
        if (block == null) Messages.OutOfMemory(size);
        return block;
    }

    // Ghidra: wsyskern.c Free @ 0x1000bc30 (export 89) — `_free`. Passerelle : voir ci-dessus.
    internal static void Free(void* block) => Blaze.Wsyskern.WsysFree((byte*)block);
}
