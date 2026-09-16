// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES DESCRIPTEURS D'ATLAS DE LA MOITIE DESSIN, et les scalaires isoles de la boucle.
//
// Huit octets chacun : le premier dword empaquette l'origine et la taille de cellule, le second
// part tel quel comme moitie « attributs » du sprite. Releves dans le binaire — voir
// Documentation/Reference/DataSymbols.md.
//
// Ils sont ici plutot que dans `GameMemory` parce qu'UN SEUL ecran les lit. C'est la meme regle
// qui garde `s_WorldMapDescriptor_005633b0` chez son appelant : une globale ne monte pas d'un
// cran parce qu'elle est une globale, elle monte quand plusieurs ecrans la touchent.
//
// Le fichier porte aussi les scalaires que la boucle par image lit hors arene.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    private static byte* s_SpriteAtlasStorage;

    /// <summary>Ghidra: <c>AU_00564a05._51_4_</c>, le dword de <c>0x00564a56</c> — releve **nul**
    /// dans le binaire. Il garde le glissement du panneau d'interface ; rien de transcrit ne
    /// l'ecrit encore. TODO(REVERSE) : trouver son ecrivain.</summary>
    private static int s_MenuSlideDone_00564a56;

    // Les deux plages de descripteurs, relevees **contigument** dans le binaire le 2026-08-22.
    //
    // La contiguite n'est pas un detail de commodite, c'est la correction d'un defaut : les
    // fonctions de dessin lisent leur descripteur a `bloc[0] + (short)bloc[4] * 8`, c'est-a-dire
    // qu'elles **indexent en avant** a partir de l'adresse qu'on leur donne. La rose des vents s'en
    // sert pour ses huit orientations : elle passe `&DAT_005646c0` et un indice 0..7, et lit donc
    // 0x005646c0, c8, d0, d8, e0, e8, f0, f8 — huit descripteurs voisins dans le binaire.
    //
    // La premiere version de ce relevé rangeait onze descripteurs isoles dans un tableau de onze
    // entrees, dans l'ordre ou je les avais rencontres. Indexer en avant y donnait le mauvais
    // descripteur des l'indice 1, et **sortait du tampon** des l'indice 4 : la fonction lisait un
    // couple couleur/attributs pris dans le tas, et la corruption se voyait une image plus loin,
    // dans la camera. Un descripteur ne se releve pas seul — on releve la plage.
    private const int AtlasA_Base = 0x00564680, AtlasA_End = 0x00564900;
    private const int AtlasB_Base = 0x00566c18, AtlasB_End = 0x00566c40;

    private static byte* s_AtlasBlockB;

    private static byte* SpriteAtlas(int va)
    {
        if (s_SpriteAtlasStorage == null)
        {
            // 0x00564680 .. 0x00564900 — la grande plage. Elle ne contient pas que des
            // descripteurs (0x00564740..0x00564770 sont des couples de coordonnees), mais on la
            // releve telle quelle : c'est ce qui rend l'indexation en avant fidele.
            ReadOnlySpan<uint> blocA =
            [
                0x0000f6c0, 0x00018040, 0x00018040, 0x00003c00,   // 0x00564680
                0x10181800, 0x01100302, 0x17179060, 0x0a110301,   // 0x00564690
                0x17179060, 0x0a110301, 0x17179060, 0x0a110301,   // 0x005646a0
                0x17179060, 0x0a110301, 0x3030a830, 0x0e110001,   // 0x005646b0
                0x0808f8a0, 0x0e110001, 0x0808f8b0, 0x0e110001,   // 0x005646c0
                0x0808f8b8, 0x0e110001, 0x0808f8a8, 0x0e110001,   // 0x005646d0
                0x17179018, 0x06110001, 0x17179030, 0x07110001,   // 0x005646e0
                0x4000c000, 0x003f0300, 0x20207000, 0x00120001,   // 0x005646f0
                0x20207020, 0x00120001, 0x20207040, 0x00120001,   // 0x00564700
                0x20207060, 0x00120001, 0x20209000, 0x00120001,   // 0x00564710
                0x20209020, 0x00120001, 0x20209040, 0x00120001,   // 0x00564720
                0x20209060, 0x00120001, 0x2020b040, 0x00120001,   // 0x00564730
                0x70800000, 0x00120001, 0x002c0068, 0x003c0088,   // 0x00564740
                0x004c0098, 0x006c0090, 0x006c0068, 0x006c0048,   // 0x00564750
                0x004c0040, 0x003c0050, 0x00540068, 0x002c0040,   // 0x00564760
                0x2c38b000, 0x00120000, 0x2430dc00, 0x00120000,   // 0x00564770
                0x2430dc30, 0x00120000, 0x0f0f80c0, 0x0d110000,   // 0x00564780
                0x0f0f80d0, 0x0d110000, 0x0818a860, 0x0a110000,   // 0x00564790
                0x0818b060, 0x0a110000, 0x0818b860, 0x0a110000,   // 0x005647a0
                0x0818c060, 0x0a110000, 0x0818c860, 0x0a110000,   // 0x005647b0
                0x08201880, 0x02300001, 0x082018a0, 0x02300001,   // 0x005647c0
                0x082818c0, 0x02300001, 0x081718e8, 0x02300001,   // 0x005647d0
                0x08222080, 0x02300001, 0x072c62c0, 0x02300001,   // 0x005647e0
                0x082820d0, 0x02300001, 0x08202980, 0x02300001,   // 0x005647f0
                0x081850d0, 0x02300001, 0x082428a0, 0x02300001,   // 0x00564800
                0x082828d0, 0x02300001, 0x08283880, 0x02300001,   // 0x00564810
                0x0728f9aa, 0x02300001, 0x083030c0, 0x02300001,   // 0x00564820
                0x082060a0, 0x02300001, 0x081838a8, 0x02300001,   // 0x00564830
                0x082038c0, 0x02300001, 0x081f38e0, 0x02300001,   // 0x00564840
                0x08204080, 0x02300001, 0x083040a0, 0x02300001,   // 0x00564850
                0x082f40d0, 0x02300001, 0x08204880, 0x02300001,   // 0x00564860
                0x083048a0, 0x02300001, 0x082f48d0, 0x02300001,   // 0x00564870
                0x08205080, 0x02300001, 0x083050a0, 0x02300001,   // 0x00564880
                0x082450d0, 0x02300001, 0x08305880, 0x02300001,   // 0x00564890
                0x082858b2, 0x02300001, 0x082758d8, 0x02300001,   // 0x005648a0
                0x083220a2, 0x02300001, 0x0808f800, 0x06110000,   // 0x005648b0
                0x0808f808, 0x04110000, 0x0808f810, 0x05110000,   // 0x005648c0
                0x0808f818, 0x07110000, 0x0808f820, 0x07110000,   // 0x005648d0
                0x0808f828, 0x09110000, 0x0808f830, 0x09110000,   // 0x005648e0
                0x0808f838, 0x0a110000, 0x0808f848, 0x04110000,   // 0x005648f0
            ];

            // 0x00566c18 .. 0x00566c40 — cinq descripteurs. La plage s'arrete la : 0x00566c40
            // vaut 0x007f007f, qui n'est pas un descripteur.
            ReadOnlySpan<uint> blocB =
            [
                0x40400000, 0x02300100,   // 0x00566c18
                0x08281800, 0x02300000,   // 0x00566c20
                0x40803000, 0x02300000,   // 0x00566c28
                0x08504800, 0x02300000,   // 0x00566c30
                0x0a640880, 0x02300100,   // 0x00566c38
            ];

            s_SpriteAtlasStorage = (byte*)NativeMemory.AllocZeroed((nuint)blocA.Length, sizeof(uint));
            for (int i = 0; i < blocA.Length; i++) ((uint*)s_SpriteAtlasStorage)[i] = blocA[i];

            s_AtlasBlockB = (byte*)NativeMemory.AllocZeroed((nuint)blocB.Length, sizeof(uint));
            for (int i = 0; i < blocB.Length; i++) ((uint*)s_AtlasBlockB)[i] = blocB[i];
        }

        if ((uint)(va - AtlasA_Base) < (uint)(AtlasA_End - AtlasA_Base))
            return s_SpriteAtlasStorage + (va - AtlasA_Base);

        if ((uint)(va - AtlasB_Base) < (uint)(AtlasB_End - AtlasB_Base))
            return s_AtlasBlockB + (va - AtlasB_Base);

        throw new NotImplementedException(
            $"Descripteur d'atlas 0x{va:X8} hors des deux plages relevees " +
            $"(0x{AtlasA_Base:X8}..0x{AtlasA_End:X8} et 0x{AtlasB_Base:X8}..0x{AtlasB_End:X8}). " +
            "Relever la plage — pas le descripteur seul — avec " +
            $"`OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x{va:x8} 64`, et etendre les " +
            "bornes ci-dessus ainsi que DataSymbols.md.");
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00564900</c> — les neuf bits d'etat que la barre des membres teste contre
    /// <c>bloc + 0x140</c>, un pictogramme par bit. Releves dans le binaire ; la borne de la boucle
    /// de l'original (<c>&lt; 0x564924</c>) confirme les neuf entrees.
    /// </summary>
    private static readonly uint[] s_StatusBits_00564900 =
        [1, 2, 4, 8, 0x10, 0x20, 0x40, 0x100, 0x200];

    /// <summary>Ghidra: les trois dispositions en X des panneaux de membre, choisies par
    /// <c>DAT_005de160</c> — la position du menu dans son animation d'ouverture.</summary>
    private static readonly int[] s_PanelX_00564928 = [0x32, -50, 0x172, 0x10e];
    private static readonly int[] s_PanelX_00564938 = [0x5a, -20, 0x154, 0xe6];
    private static readonly int[] s_PanelX_00564948 = [0xa0, 0xa0, 0xa0, 0xa0];
    private static readonly int[] s_PanelX_00564958 = [0xb4, 0x8c, 0xb4, 0x8c];

    /// <summary>Ghidra: DAT_005de020 — le drapeau d'affichage de mise au point, bascule par la
    /// ligne MONITOR de <see cref="RunDebugMenuFrame_0051AA30"/> (Blaze.c 66241/66244). <b>Zero au chargement</b> : l'adresse tombe
    /// dans la queue non initialisee de <c>.data</c>, verifie avec
    /// <c>OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x005de018</c>.</summary>
    private static byte s_DebugOverlayEnabled_005de020;

    /// <summary>Ghidra: DAT_005de540 — l'index du membre du groupe que le pilotage de camera et
    /// l'affichage suivent. Pose en meme temps que <c>DAT_005de538</c> par
    /// <see cref="PrepareLeaderAndPartyFlags"/>, mais relu separement : les deux divergent des que
    /// <see cref="PassLeaderToNextEligible"/> agit, qui ne touche que le second.</summary>
    private static byte s_Unknown_005de540;

    /// <summary>Ghidra: DAT_005de518 et DAT_005de544 — les deux compteurs d'octets que
    /// <see cref="PrepareLeaderAndPartyFlags"/> reconstruit a chaque image : combien de membres
    /// portent le bit <c>0x40000000</c>, et combien sont ecartes du jeu pour une raison ou une
    /// autre. Quand les deux atteignent la taille du groupe, la partie est finie.</summary>
    private static byte s_Unknown_005de518, s_Unknown_005de544;

    /// <summary>Ghidra: DAT_005de6f8 / 6fc / 704 / 70c / 710 / 718 — six dwords remis a l'etat neuf
    /// par le cas 1 du repartiteur, trois a zero et trois a -1. Les trous (0x700, 0x708, 0x714)
    /// portent autre chose, que rien de transcrit ne touche encore : declares un par un plutot
    /// qu'en bloc, faute de connaitre l'etendue reelle. TODO(REVERSE) : leurs roles.</summary>
    private static int s_Unknown_005de6f8, s_Unknown_005de6fc, s_Unknown_005de704;
    private static int s_Unknown_005de70c, s_Unknown_005de710, s_Unknown_005de718;

    /// <summary>Ghidra: DAT_005e8ed8 et DAT_005e8eda — deux <c>short</c> (confirme au
    /// desassemblage : <c>mov [5E8ED8h],cx</c>) ou le cas 1 depose la position du meneur reduite au
    /// quart et bornee a la fenetre de la carte. Sous <c>0x005e9000</c>, donc hors GameArena.</summary>
    private static short s_Unknown_005e8ed8, s_Unknown_005e8eda;
}
