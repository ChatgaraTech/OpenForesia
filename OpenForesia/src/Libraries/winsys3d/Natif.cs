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
//     etape 1   `DrawOTag` en C#, tout le reste natif                 FAIT le 2026-09-22
//     etape 2   + `FUN_00a13400`, le repartiteur de primitives        FAIT le 2026-09-22
//     etape 3   + `FUN_00a0cdd0`, puis le consommateur `FUN_00a0cd20`   <- la suite
//     etape 4   les 48 appelees du repartiteur, par familles de forme
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

    // `FUN_00a13400`, le repartiteur de primitives, EST PORTE depuis le 2026-09-22 —
    // `CheminAffichage.RejouerNoeud`. Ce sont ses 48 appelees qui sont listees plus bas.

    /// <summary>winsys3d.c FUN_00a027e0 en 0x00a027e0 — NON PORTE — ferme le second parcours.</summary>
    internal static void Fun_00a027e0()
        => ((delegate* unmanaged[Stdcall]<void>)At(0x00a027e0))();

    /// <summary>winsys3d.c FUN_00a02820 en 0x00a02820 — NON PORTE (25 lignes) — la queue de l'image.</summary>
    internal static void Fun_00a02820()
        => ((delegate* unmanaged[Stdcall]<void>)At(0x00a02820))();
    // ------------------------------------------------------------------------------------------
    // LES 48 APPELEES DU REPARTITEUR `FUN_00a13400`, porte le 2026-09-22.
    //
    // Engendrees mecaniquement depuis `Legacy/Raw/Winsys3d/FUN_00a13400.arites.txt`, pour qu'aucune
    // arite ne soit tapee a la main. Toutes prennent UN argument sauf `FUN_00a0e000` et
    // `FUN_00a133e0`, qui en prennent trois (`ret 0Ch`).
    // ------------------------------------------------------------------------------------------

    /// <summary>winsys3d.c FUN_00a0cdd0 en 0x00a0cdd0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0cdd0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0cdd0))(p);

    /// <summary>winsys3d.c FUN_00a0e000 en 0x00a0e000 — NON PORTE. `ret 12`.</summary>
    internal static void Fun_00a0e000(uint a, uint b, int c)
        => ((delegate* unmanaged[Stdcall]<uint, uint, int, void>)At(0x00a0e000))(a, b, c);

    /// <summary>winsys3d.c FUN_00a0e040 en 0x00a0e040 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0e040(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0e040))(p);

    /// <summary>winsys3d.c FUN_00a0e0c0 en 0x00a0e0c0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0e0c0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0e0c0))(p);

    /// <summary>winsys3d.c FUN_00a0e580 en 0x00a0e580 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0e580(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0e580))(p);

    /// <summary>winsys3d.c FUN_00a0e6a0 en 0x00a0e6a0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0e6a0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0e6a0))(p);

    /// <summary>winsys3d.c FUN_00a0e7f0 en 0x00a0e7f0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0e7f0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0e7f0))(p);

    /// <summary>winsys3d.c FUN_00a0e970 en 0x00a0e970 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0e970(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0e970))(p);

    /// <summary>winsys3d.c FUN_00a0eaf0 en 0x00a0eaf0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0eaf0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0eaf0))(p);

    /// <summary>winsys3d.c FUN_00a0ecf0 en 0x00a0ecf0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0ecf0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0ecf0))(p);

    /// <summary>winsys3d.c FUN_00a0ef40 en 0x00a0ef40 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0ef40(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0ef40))(p);

    /// <summary>winsys3d.c FUN_00a0f090 en 0x00a0f090 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0f090(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0f090))(p);

    /// <summary>winsys3d.c FUN_00a0f260 en 0x00a0f260 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0f260(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0f260))(p);

    /// <summary>winsys3d.c FUN_00a0f3f0 en 0x00a0f3f0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0f3f0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0f3f0))(p);

    /// <summary>winsys3d.c FUN_00a0f570 en 0x00a0f570 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0f570(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0f570))(p);

    /// <summary>winsys3d.c FUN_00a0f780 en 0x00a0f780 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0f780(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0f780))(p);

    /// <summary>winsys3d.c FUN_00a0f940 en 0x00a0f940 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0f940(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0f940))(p);

    /// <summary>winsys3d.c FUN_00a0fb40 en 0x00a0fb40 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0fb40(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0fb40))(p);

    /// <summary>winsys3d.c FUN_00a0fdc0 en 0x00a0fdc0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0fdc0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0fdc0))(p);

    /// <summary>winsys3d.c FUN_00a0ff50 en 0x00a0ff50 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a0ff50(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0ff50))(p);

    /// <summary>winsys3d.c FUN_00a101c0 en 0x00a101c0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a101c0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a101c0))(p);

    /// <summary>winsys3d.c FUN_00a104c0 en 0x00a104c0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a104c0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a104c0))(p);

    /// <summary>winsys3d.c FUN_00a10690 en 0x00a10690 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a10690(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a10690))(p);

    /// <summary>winsys3d.c FUN_00a107e0 en 0x00a107e0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a107e0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a107e0))(p);

    /// <summary>winsys3d.c FUN_00a109c0 en 0x00a109c0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a109c0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a109c0))(p);

    /// <summary>winsys3d.c FUN_00a10b60 en 0x00a10b60 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a10b60(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a10b60))(p);

    /// <summary>winsys3d.c FUN_00a10ca0 en 0x00a10ca0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a10ca0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a10ca0))(p);

    /// <summary>winsys3d.c FUN_00a10df0 en 0x00a10df0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a10df0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a10df0))(p);

    /// <summary>winsys3d.c FUN_00a11000 en 0x00a11000 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a11000(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a11000))(p);

    /// <summary>winsys3d.c FUN_00a111c0 en 0x00a111c0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a111c0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a111c0))(p);

    /// <summary>winsys3d.c FUN_00a11310 en 0x00a11310 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a11310(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a11310))(p);

    /// <summary>winsys3d.c FUN_00a11520 en 0x00a11520 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a11520(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a11520))(p);

    /// <summary>winsys3d.c FUN_00a116e0 en 0x00a116e0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a116e0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a116e0))(p);

    /// <summary>winsys3d.c FUN_00a118e0 en 0x00a118e0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a118e0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a118e0))(p);

    /// <summary>winsys3d.c FUN_00a11a90 en 0x00a11a90 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a11a90(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a11a90))(p);

    /// <summary>winsys3d.c FUN_00a11cd0 en 0x00a11cd0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a11cd0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a11cd0))(p);

    /// <summary>winsys3d.c FUN_00a11ec0 en 0x00a11ec0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a11ec0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a11ec0))(p);

    /// <summary>winsys3d.c FUN_00a12190 en 0x00a12190 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a12190(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a12190))(p);

    /// <summary>winsys3d.c FUN_00a12350 en 0x00a12350 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a12350(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a12350))(p);

    /// <summary>winsys3d.c FUN_00a126d0 en 0x00a126d0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a126d0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a126d0))(p);

    /// <summary>winsys3d.c FUN_00a128d0 en 0x00a128d0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a128d0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a128d0))(p);

    /// <summary>winsys3d.c FUN_00a12ac0 en 0x00a12ac0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a12ac0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a12ac0))(p);

    /// <summary>winsys3d.c FUN_00a12c70 en 0x00a12c70 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a12c70(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a12c70))(p);

    /// <summary>winsys3d.c FUN_00a12e60 en 0x00a12e60 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a12e60(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a12e60))(p);

    /// <summary>winsys3d.c FUN_00a13010 en 0x00a13010 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a13010(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a13010))(p);

    /// <summary>winsys3d.c FUN_00a13220 en 0x00a13220 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a13220(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a13220))(p);

    /// <summary>winsys3d.c FUN_00a133e0 en 0x00a133e0 — NON PORTE. `ret 12`.</summary>
    internal static void Fun_00a133e0(uint a, uint b, int c)
        => ((delegate* unmanaged[Stdcall]<uint, uint, int, void>)At(0x00a133e0))(a, b, c);

    /// <summary>winsys3d.c FUN_00a142a0 en 0x00a142a0 — NON PORTE. `ret 4`.</summary>
    internal static void Fun_00a142a0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a142a0))(p);
}
