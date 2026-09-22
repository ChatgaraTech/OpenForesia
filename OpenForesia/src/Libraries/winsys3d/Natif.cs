// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c.
//
// ============================================================================================
// APPELER CE QUI N'EST PAS ENCORE PORTE — les corps INTERNES de `winsys3d.dll`, par adresse
//
// L'aliasing des globales (`Data.cs`) rend le chemin d'affichage divisible, mais il ne suffit
// pas : un corps porte appelle des corps qui ne le sont pas encore, et ceux-la **ne sont pas
// exportes**. Aucun `[DllImport]` ne peut les atteindre — ni par nom, ni par ordinal.
//
// Ce fichier les atteint par leur adresse : base reelle du module plus RVA, exactement comme
// `Data.cs` pour les donnees. C'est ce qui permet de porter le lot **de haut en bas**, un corps
// a la fois, en verifiant a l'ecran a chaque etape :
//
//     etape 1   `DrawOTag` en C#, tout le reste natif        <- on y est
//     etape 2   + `FUN_00a13400`, qui appelle le reste natif
//     etape 3   + `FUN_00a0cdd0`, puis le consommateur
//     ...       jusqu'a ce que plus rien n'appelle ici, et ce fichier disparait
//
// **Ce fichier est un echafaudage, pas une transcription.** Chaque entree qu'on y ajoute est une
// dette : elle dit « ce corps-la n'est pas encore ecrit ». Quand le perimetre sera porte, il ne
// devra plus rien rester ici. La liste qui suit est donc aussi un tableau de bord.
//
// ============================================================================================
// LES ARITES SONT PROUVEES PAR LE DESASSEMBLAGE, PAS DEDUITES DE GHIDRA
//
// En `stdcall` l'appele nettoie la pile. Un `call` sans `add esp, N` derriere prouve donc a la
// fois le nombre d'arguments pousses ET que la convention est `stdcall`. C'est le controle qui
// avait attrape `Ordinal_45` le 2026-08-21 ; il vaut ici mot pour mot, et il est d'autant plus
// necessaire qu'une erreur d'arite sur un appel par adresse ne donne aucun message, juste une
// pile corrompue.
//
// Releve dans `DrawOTag` (0x00a061b0), ou les sept appels tiennent :
//
//     call 00A26490h                 aucun push        0 argument
//     push edi ; call 00A26B20h      un push, pas d'`add esp,4`   1 argument, stdcall
//     call 00A027A0h                 aucun push        0 argument
//     call 00A26F20h                 aucun push        0 argument
//     push esi ; call 00A13400h      un push, pas d'`add esp,4`   1 argument, stdcall
//     call 00A027E0h                 aucun push        0 argument
//     call 00A02820h                 aucun push        0 argument
// ============================================================================================
//
// ============================================================================================
// UN PIEGE ATTRAPE LE JOUR MEME : NE PAS ECRIRE LA VIGNETTE CANONIQUE ICI
//
// La premiere version de ce fichier portait la vignette `winsys3d.c <nom> @ <adresse>` sur chaque
// entree — la **forme canonique** que `census-dll-surface.sh` lit pour sa colonne `implemente`.
// Resultat : le recensement a compte huit corps portes d'un coup, alors qu'un seul l'etait. Ces
// sept-la ne sont pas transcrits, ils sont APPELES.
//
// D'ou la forme `FUN_xxx en 0xyyy — NON PORTE` ci-dessous, qui ne matche pas. C'est la meme
// lecon que `LISEZMOI.md` regle 1 — « une mention n'est pas une transcription » — prise par
// l'autre bout : ici la mention ressemblait trop a une transcription.

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dNatif
{
    private const nuint ImageBase = 0x00a00000;

    private static byte* s_Base;

    /// <summary>
    /// La base de chargement reelle, resolue paresseusement — meme raison que dans `Data.cs` :
    /// la DLL n'est pas forcement chargee quand cette classe l'est.
    /// </summary>
    private static byte* Base
    {
        get
        {
            if (s_Base != null) return s_Base;

            IntPtr module = Win32.GetModuleHandleA(Blaze.Blaze.ToAnsiZ("winsys3d.dll"));
            if (module == IntPtr.Zero) module = Win32.LoadLibraryA(Blaze.Blaze.ToAnsiZ("winsys3d.dll"));
            if (module == IntPtr.Zero)
                throw new InvalidOperationException(
                    "winsys3d.dll n'est pas chargee : les corps internes appeles par adresse " +
                    "n'ont pas de point d'entree. Voir src/Libraries/winsys3d/Natif.cs.");

            s_Base = (byte*)module;
            return s_Base;
        }
    }

    private static void* At(nuint va) => Base + (va - ImageBase);

    // ----------------------------------------------------------------------------------------
    // LA DETTE — un corps par ligne, chacun encore natif.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// winsys3d.c FUN_00a26490 en 0x00a26490 — NON PORTE — appele par `DrawOTag` juste avant le premier
    /// parcours de l'OT, sans argument. C'est l'ouverture de la passe de textures.
    /// </summary>
    internal static void Fun_00a26490()
        => ((delegate* unmanaged[Stdcall]<void>)At(0x00a26490))();

    /// <summary>
    /// winsys3d.c FUN_00a26b20 en 0x00a26b20 — NON PORTE (217 lignes) — appele sur chaque nœud de l'OT dont
    /// l'octet de poids fort est non nul, pendant la passe de textures.
    /// </summary>
    internal static void Fun_00a26b20(uint* noeud)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a26b20))(noeud);

    /// <summary>winsys3d.c FUN_00a027a0 en 0x00a027a0 — NON PORTE — ferme la passe de textures.</summary>
    internal static void Fun_00a027a0()
        => ((delegate* unmanaged[Stdcall]<void>)At(0x00a027a0))();

    /// <summary>winsys3d.c FUN_00a26f20 en 0x00a26f20 — NON PORTE (111 lignes) — la suit immediatement.</summary>
    internal static void Fun_00a26f20()
        => ((delegate* unmanaged[Stdcall]<void>)At(0x00a26f20))();

    /// <summary>
    /// winsys3d.c FUN_00a13400 en 0x00a13400 — NON PORTE (540 lignes) — **le rejeu d'un nœud de l'OT**, et la
    /// prochaine piece a porter. C'est lui qui mene, par `FUN_00a0cdd0`, au consommateur du
    /// paquet `FUN_00a0cd20`.
    /// </summary>
    internal static void Fun_00a13400(uint* noeud)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a13400))(noeud);

    /// <summary>winsys3d.c FUN_00a027e0 en 0x00a027e0 — NON PORTE — ferme le second parcours.</summary>
    internal static void Fun_00a027e0()
        => ((delegate* unmanaged[Stdcall]<void>)At(0x00a027e0))();

    /// <summary>winsys3d.c FUN_00a02820 en 0x00a02820 — NON PORTE (25 lignes) — la queue de l'image.</summary>
    internal static void Fun_00a02820()
        => ((delegate* unmanaged[Stdcall]<void>)At(0x00a02820))();
}
