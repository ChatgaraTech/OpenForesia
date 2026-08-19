// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    // Ghidra: &DAT_00551640.. et &DAT_00571500.. — les deux plages de parametres et d'etat par
    // classe d'entite que les onze initialiseurs remettent a neuf. Une allocation chacune, pas
    // cent champs separes : les initialiseurs les parcourent par pas de dword.
    private const int EntityParamsBase = 0x00551640;
    private static readonly byte* s_EntityParams_00551640 = (byte*)NativeMemory.AllocZeroed(0x200);
    private static int* EntityParam(int va) => (int*)(s_EntityParams_00551640 + (va - EntityParamsBase));

    private const int EntityStateBase = 0x00571500;
    private static readonly byte* s_EntityState_00571500 = (byte*)NativeMemory.AllocZeroed(0x500);
    private static int* EntityState(int va) => (int*)(s_EntityState_00571500 + (va - EntityStateBase));

    // ---------------------------------------------------------------------------------------
    // Les arenes de la carte du monde.
    // ---------------------------------------------------------------------------------------

    /// <summary>
    /// Ghidra: 0x005eca00..0x00600c00 — **une seule region continue**, celle des tableaux
    /// d'entites. Les six <c>FUN_005352c0</c> de <c>FUN_00516350</c> la couvrent bout a bout :
    /// 0x5ecaa0 (0x20 entrees), 0x5edee0 (0x80), 0x5f2d40 (0x80), 0x5f7de0 (0x40), 0x5fa5e0 (0x80),
    /// 0x5ff400 (0x20) — chacune de 0x9c octets, chaque bloc finissant exactement ou le suivant
    /// commence. Une allocation, pas cinquante champs : c'est le defaut d'aliasing habituel evite
    /// d'avance.
    /// </summary>
    private const int EntityArenaBase = 0x005eca00;
    private static readonly byte* s_EntityArena_005eca00 = (byte*)NativeMemory.AllocZeroed(0x14200);
    private static byte* Entity(int va) => s_EntityArena_005eca00 + (va - EntityArenaBase);

    /// <summary>Ghidra: 0x005de030..0x005de0b0 — le bloc de camera et de position du groupe. Son
    /// adresse est stockee telle quelle dans <c>DAT_005edebc</c>, d'ou l'allocation native.</summary>
    private const int CameraBlockBase = 0x005de030;
    private static readonly byte* s_CameraBlock_005de030 = (byte*)NativeMemory.AllocZeroed(0x80);
    private static int* Camera(int va) => (int*)(s_CameraBlock_005de030 + (va - CameraBlockBase));

    /// <summary>Ghidra: 0x006067c0..0x00606800 — le second bloc de vue, dont l'adresse part dans
    /// <c>DAT_005e9ae0</c> et <c>DAT_005eca94</c>.</summary>
    private const int ViewBlockBase = 0x006067c0;
    private static readonly byte* s_ViewBlock_006067c0 = (byte*)NativeMemory.AllocZeroed(0x40);
    private static int* View(int va) => (int*)(s_ViewBlock_006067c0 + (va - ViewBlockBase));

    // Ghidra: DAT_005e85c0 (l'ordre de sélection, un octet par entrée : 0 = non retenu, 1..4 = la
    // place dans le groupe) et DAT_005e8640 (les libellés de ligne, 0x16 octets par entrée). Deux
    // symboles voisins d'une même région, déclarés ensemble plutôt que séparément — c'est le défaut
    // d'aliasing que ce projet a déjà rencontré deux douzaines de fois. Le bloc s'arrête bien avant
    // 0x5e8f00 (s_EntityDecodeTable) et commence après 0x5e85a8, fin de la table des sons.
    private const int PartyListBase = 0x005e85c0;
    private static readonly byte* s_PartyListArena_005e85c0 = (byte*)NativeMemory.AllocZeroed(0x340);

    // Ghidra: &DAT_005de410 — le bloc d'état du menu objets. **Une seule allocation**, pas une
    // vingtaine de champs : FUN_00524210 remet à zéro DAT_005de4c4..DAT_005de50c d'affilée, ce qui
    // les désigne comme contigus, et DAT_005de410/DAT_005de464 vivent dans le même voisinage. Le
    // bloc s'arrête juste avant DAT_005de510, qui est s_GameStateId — d'où les 0x100 octets.
    //
    // C'est le défaut d'aliasing que ce projet a rencontré deux douzaines de fois : déclarer ces
    // adresses en champs séparés marche jusqu'au jour où un site les parcourt.
    //
    // **Etendu vers le bas le 2026-08-18** : le sous-ecran « Invertaire » (FUN_00524880) atteint
    // DAT_005de110 (le tampon de nom d'objet que FUN_0051bcc0 remplit), DAT_005de2d8 (les vingt-
    // deux drapeaux « objet retenu »), DAT_005de390 (le titre de la carte memoire en UTF-16) et
    // DAT_005de428/430/488/4a0. La base descend donc a 0x005de100 et le bloc fait 0x420 octets ;
    // ItemMenuState() etant adresse par VA absolue, aucun site existant ne bouge.
    private const int ItemMenuArenaBase = 0x005de100;
    private static readonly byte* s_ItemMenuArena_005de100 = (byte*)NativeMemory.AllocZeroed(0x460);
    private static int* ItemMenuState(int va) => (int*)(s_ItemMenuArena_005de100 + (va - ItemMenuArenaBase));
    private static byte* ItemMenuBytes(int va) => s_ItemMenuArena_005de100 + (va - ItemMenuArenaBase);
}
