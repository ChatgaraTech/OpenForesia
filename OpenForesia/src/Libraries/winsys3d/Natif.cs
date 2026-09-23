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
//     etape 3   + `FUN_00a0cdd0`, puis le consommateur `FUN_00a0cd20` FAIT le 2026-09-23
//     etape 4   les 42 primitives GPU du repartiteur, six familles      FAIT le 2026-09-23
//     etape 5   les 5 opcodes 0xe*/0xf9 : rejeux, sprite tourne, fond  FAIT le 2026-09-23
//     etape 6   les 6 appelees de `DrawOTag` : cache de textures, texte FAIT le 2026-09-23
//     etape 7   l'interieur du cache de textures et la liaison        FAIT le 2026-09-23
//     etape 8   le chargement des textures et le qsort de la CRT     FAIT le 2026-09-23
//     etape 9   le constructeur `dde0` (752) et ses 15 producteurs  <- la suite
//     etape 10  la reconstruction `c4f0` et ses producteurs
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

    // `FUN_00a13400`, le repartiteur de primitives, EST PORTE depuis le 2026-09-22 —
    // `CheminAffichage.RejouerNoeud`. Ce sont ses 48 appelees qui sont listees plus bas.

    // ------------------------------------------------------------------------------------------
    // LES 48 APPELEES DU REPARTITEUR `FUN_00a13400`, porte le 2026-09-22.
    //
    // Engendrees mecaniquement depuis `Legacy/Raw/Winsys3d/FUN_00a13400.arites.txt`, pour qu'aucune
    // arite ne soit tapee a la main. Toutes prennent UN argument sauf `FUN_00a0e000` et
    // `FUN_00a133e0`, qui en prennent trois (`ret 0Ch`).
    // ------------------------------------------------------------------------------------------

    // `FUN_00a0cdd0` EST PORTE depuis le 2026-09-23 — `CheminAffichage.RejouerPaquet`, avec le
    // consommateur `FUN_00a0cd20` (`RejouerPaquetCourant`). Ses deux appelees suivent.
    //
    // FAMILLE 1 (`CIVIVCIIIVR`) PORTEE le 2026-09-23 — `Primitives.cs` : les neuf primitives
    // plates en dessin immediat, e580 e6a0 e7f0 ef40 f3f0 10690 10b60 10ca0 111c0. Leur appelee
    // commune `FUN_00a03db0` est portee avec elles (`ChoisirMelange`).
    //
    // FAMILLE 2 (`CIICRCIIVVR`) PORTEE le 2026-09-23 — `Primitives.cs` : les sept texturees
    // semi-transparentes brutes, 118e0 11cd0 12190 126d0 12ac0 12e60 13220. Elles appellent
    // `FUN_00a27880` (plus haut) et `FUN_00a03c80` (ci-dessous).
    //
    // FAMILLES 3 a 6 PORTEES le 2026-09-23 — `Primitives.cs` : les opaques brutes (7), les
    // modulees semi-transparentes (5) et opaques (5), les Gouraud (9). Plus aucune primitive GPU
    // ne passe par ici : 42 des 47 cas du repartiteur sont en C#.
    //
    // ETAPE 5 FAITE le 2026-09-23 — les cinq dernieres appelees du repartiteur : les rejeux
    // `FUN_00a0e000`, `FUN_00a133e0`, `FUN_00a0e040` (`CheminAffichage.cs`), le sprite tourne
    // `FUN_00a0e0c0` (`Primitives.SpriteTourne`) et le fond en tuiles `FUN_00a142a0`
    // (`FondTuiles.cs`). **Le repartiteur n'appelle plus rien ici.** Ce qui reste plus bas est
    // appele par les corps portes : la liaison de texture, le melange selon `abr`, et les deux
    // constructeurs de paquets, porte d'entree des producteurs.

    /// <summary>
    /// winsys3d.c FUN_00a0dde0 en 0x00a0dde0 — NON PORTE (81 lignes, ordinal 752). `ret 0Ch`, trois
    /// arguments. Le CONSTRUCTEUR de paquets : aiguille sur l'un des producteurs du tampon de
    /// commandes, puis pose le bit 31 du premier mot du descripteur (« construit »). Appele par
    /// `CheminAffichage.ConstruireEtRejouer` quand ce bit est nul.
    /// </summary>
    internal static void Fun_00a0dde0(uint* descripteur, uint a, int b)
        => ((delegate* unmanaged[Stdcall]<uint*, uint, int, void>)At(0x00a0dde0))(descripteur, a, b);

    // ------------------------------------------------------------------------------------------
    // LES APPELEES DU CACHE DE TEXTURES (`CacheTextures.cs`, 2026-09-23). Arites lues sur les `ret`
    // et recoupees aux sites d'appel ; TROIS `__thiscall` sur `DAT_012141d4` que Ghidra ne montre
    // pas (`mov ecx,[12141D4h]` avant chaque `call`), et UN `cdecl` (`add esp,10h` apres).
    // ------------------------------------------------------------------------------------------



    /// <summary>
    /// winsys3d.c FUN_00a0c4f0 en 0x00a0c4f0 — NON PORTE (259 lignes). `ret 4`, appele par
    /// `RejouerPaquet` quand le descripteur du paquet ne vaut pas -1 : (re)construit le paquet.
    /// </summary>
    internal static void Fun_00a0c4f0(uint* p)
        => ((delegate* unmanaged[Stdcall]<uint*, void>)At(0x00a0c4f0))(p);

}
