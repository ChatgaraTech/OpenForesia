// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DE LA GROTTE SCELLEE — zone 10, seize sous-zones.
//
// Ouvert le 2026-09-19 avec le lot 1 de la zone : les copies de corps deja ecrits, a la planche
// pres. La grotte se lit en blocs de sous-zones (0 ; 1 ; 2-3 ; 4-9 ; 5-8-10 ; 6-7 ; 11-12 ; 13-15),
// et le compilateur a emis la plupart de ses particules une fois par bloc — d'ou les series
// ci-dessous. Quelques corps de la grotte vivent deja chez leurs parents (ImmortalCastleParticles,
// LabyrinthParticles, MineParticles) : ils y ont ete ecrits avec eux.
//
// A 294 corps, ce fichier grossira : une coupe par bloc de sous-zones sera a proposer passe
// ~3 000 lignes (decision du 2026-09-19).
//
// Dumps : Legacy/Raw/Blaze/SealedCave/.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES PLANCHES DU LOT 1
    //
    // Deux familles, recopiees d'un bloc a l'autre :
    //
    //   - le GRAIN (le corps de 0x004070F0, la mine) : quatre images 0x08 x 0x08, bornees en bas par
    //     une tuile 0x10 x 0x10 a (0x48, 0) et en haut par une 0x10 x 0x10 ou une autre forme ;
    //   - l'ECLAT QUI TOMBE (le corps de 0x00406C40) : quatre images 0x1f x 0x1f de la couche 0x18,
    //     prises a y = 0xc0, bornees en haut par une tuile 0x20 x 0x20. Le corps avance son image
    //     aux images 6, 12 et 18 : exactement jusqu'a 3.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00550bb0</c> — l'eclat qui tombe du bloc 0 (sous-zone 0).</summary>
    private static readonly byte[] s_Descriptors_00550BB0 =
    [
        0x00, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550bb0  <- 0x00431870
        0x20, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550bb8
        0x40, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550bc0
        0x60, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550bc8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550c90</c> — le grain du bloc 0.</summary>
    private static readonly byte[] s_Descriptors_00550C90 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550c90  <- 0x004328E0
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550c98
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550ca0
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550ca8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550cc0</c> — l'eclat qui tombe des sous-zones 2 et 3.</summary>
    private static readonly byte[] s_Descriptors_00550CC0 =
    [
        0x00, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550cc0  <- 0x00432EA0
        0x20, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550cc8
        0x40, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550cd0
        0x60, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550cd8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550d80</c> — le grain des sous-zones 2 et 3.</summary>
    private static readonly byte[] s_Descriptors_00550D80 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550d80  <- 0x00433E10
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550d88
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550d90
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550d98
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550db0</c> — l'eclat qui tombe des sous-zones 5, 8 et 10.</summary>
    private static readonly byte[] s_Descriptors_00550DB0 =
    [
        0x00, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550db0  <- 0x00434790
        0x20, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550db8
        0x40, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550dc0
        0x60, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x00550dc8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550e90</c> — le grain des sous-zones 5, 8 et 10.</summary>
    private static readonly byte[] s_Descriptors_00550E90 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550e90  <- 0x004359B0
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550e98
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550ea0
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550ea8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550f40</c> — le grain des sous-zones 4 et 9. Il suit
    /// <c>0x00550f30</c> (<c>s_Descriptors_00550F30</c>, MineParticles.cs) et la tuile
    /// <c>0x10 x 0x10</c> de <c>0x00550f38</c>.</summary>
    private static readonly byte[] s_Descriptors_00550F40 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550f40  <- 0x004389F0
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550f48
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550f50
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550f58
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00551070</c> — le grain des sous-zones 6 et 7.</summary>
    private static readonly byte[] s_Descriptors_00551070 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00551070  <- 0x0043B310
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00551078
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00551080
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00551088
    ];

    private static byte* s_Descriptors00550BB0Storage;
    private static byte* s_Descriptors00550C90Storage;
    private static byte* s_Descriptors00550CC0Storage;
    private static byte* s_Descriptors00550D80Storage;
    private static byte* s_Descriptors00550DB0Storage;
    private static byte* s_Descriptors00550E90Storage;
    private static byte* s_Descriptors00550F40Storage;
    private static byte* s_Descriptors00551070Storage;

    private static byte* Descriptors00550BB0() => NativeCopy(s_Descriptors_00550BB0, ref s_Descriptors00550BB0Storage);
    private static byte* Descriptors00550C90() => NativeCopy(s_Descriptors_00550C90, ref s_Descriptors00550C90Storage);
    private static byte* Descriptors00550CC0() => NativeCopy(s_Descriptors_00550CC0, ref s_Descriptors00550CC0Storage);
    private static byte* Descriptors00550D80() => NativeCopy(s_Descriptors_00550D80, ref s_Descriptors00550D80Storage);
    private static byte* Descriptors00550DB0() => NativeCopy(s_Descriptors_00550DB0, ref s_Descriptors00550DB0Storage);
    private static byte* Descriptors00550E90() => NativeCopy(s_Descriptors_00550E90, ref s_Descriptors00550E90Storage);
    private static byte* Descriptors00550F40() => NativeCopy(s_Descriptors_00550F40, ref s_Descriptors00550F40Storage);
    private static byte* Descriptors00551070() => NativeCopy(s_Descriptors_00551070, ref s_Descriptors00551070Storage);

    // =====================================================================
    // LE GRAIN — cinq copies de 0x004070F0 (la mine), a la planche pres
    // =====================================================================

    /// <summary>Ghidra: <c>0x004328e0</c> — le grain de la sous-zone 0. Absent de <c>Blaze.c</c> ;
    /// <see cref="EntityHandler_004070F0"/> a la planche pres.</summary>
    private static void EntityHandler_004328E0(byte* e) => TickMineSpeck_004070F0(e, Descriptors00550C90());   // Ghidra: &DAT_00550c90

    /// <summary>Ghidra: <c>0x00433e10</c> — le grain des sous-zones 2 et 3.</summary>
    private static void EntityHandler_00433E10(byte* e) => TickMineSpeck_004070F0(e, Descriptors00550D80());   // Ghidra: &DAT_00550d80

    /// <summary>Ghidra: <c>0x004359b0</c> — le grain des sous-zones 5, 8 et 10.</summary>
    private static void EntityHandler_004359B0(byte* e) => TickMineSpeck_004070F0(e, Descriptors00550E90());   // Ghidra: &DAT_00550e90

    /// <summary>Ghidra: <c>0x004389f0</c> — le grain des sous-zones 4 et 9.</summary>
    private static void EntityHandler_004389F0(byte* e) => TickMineSpeck_004070F0(e, Descriptors00550F40());   // Ghidra: &DAT_00550f40

    /// <summary>Ghidra: <c>0x0043b310</c> — le grain des sous-zones 6 et 7.</summary>
    private static void EntityHandler_0043B310(byte* e) => TickMineSpeck_004070F0(e, Descriptors00551070());   // Ghidra: &DAT_00551070

    // =====================================================================
    // L'ECLAT QUI TOMBE — trois copies de 0x00406C40 (la mine), a la planche pres
    // =====================================================================

    /// <summary>Ghidra: <c>0x00431870</c> — l'eclat de la sous-zone 0. Absent de <c>Blaze.c</c> ;
    /// <see cref="EntityHandler_00406C40"/> a la planche pres.</summary>
    private static void EntityHandler_00431870(byte* e) => TickFallingShard_00406C40(e, Descriptors00550BB0());   // Ghidra: &DAT_00550bb0

    /// <summary>Ghidra: <c>0x00432ea0</c> — l'eclat des sous-zones 2 et 3.</summary>
    private static void EntityHandler_00432EA0(byte* e) => TickFallingShard_00406C40(e, Descriptors00550CC0());   // Ghidra: &DAT_00550cc0

    /// <summary>Ghidra: <c>0x00434790</c> — l'eclat des sous-zones 5, 8 et 10.</summary>
    private static void EntityHandler_00434790(byte* e) => TickFallingShard_00406C40(e, Descriptors00550DB0());   // Ghidra: &DAT_00550db0

    // =====================================================================
    // L'EMETTEUR ET SON SCRIPT DE SONS
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005526e0</c> — <b>le script de sons differes de l'emetteur de la
    /// grotte</b>, six enregistrements jusqu'au delai nul de <c>0x00552708</c>. Memes octets que
    /// <c>0x00552660</c>, la fin du script de l'explosion (<see cref="s_ExplosionSoundScript_00552638"/>),
    /// verifies sur les quarante-huit.
    /// </summary>
    private static readonly byte[] s_EmitterSoundScript_005526E0 =
    [
        0x21, 0x01, 0x01, 0x00, 0x2a, 0x03, 0x00, 0x18,   // 0x005526e0
        0x30, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x005526e8
        0x30, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x005526f0
        0x30, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x005526f8
        0x30, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00552700
        0x30, 0x00, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00552708 — delai nul : fin
    ];

    private static byte* s_EmitterSoundScript005526E0Storage;

    /// <summary>Ghidra: <c>0x00484ab0</c> — sous-zones 4 et 9. Absent de <c>Blaze.c</c> ;
    /// <see cref="EntityHandler_00483930"/> au script de sons pres, <c>0x005526e0</c>.</summary>
    private static void EntityHandler_00484AB0(byte* e)
        => TickSpawningEmitter_00483930(e, NativeCopy(s_EmitterSoundScript_005526E0, ref s_EmitterSoundScript005526E0Storage));   // Ghidra: `push 5526E0h`

    // =====================================================================
    // LOT 2 — L'ECLAT ATTIRE ET LE GRAIN QUI MONTE, cinq blocs chacun
    //
    // Chaque bloc de sous-zones porte, juste avant la planche du grain du lot 1, deux planches :
    // trois tuiles 0x18 x 0x18 a y = 0x30 (l'eclat attire), puis une tuile 0x08 x 0x08 (le grain
    // qui monte). Bornees en bas par une 0x18 x 0x18 a (0x90, 0x30), en haut par la 0x10 x 0x10 a
    // (0x48, 0) qui precede le grain.
    //
    //   - l'ECLAT ATTIRE est TickHomingGlint_00417650 (vieille tour) a une constante pres : ses
    //     deux echelles partent de 0x700 et non de 0xf00 ;
    //   - le GRAIN QUI MONTE est TickLabyrinthRisingMote (Labyrinthe) a la planche pres — l'ecriture
    //     de +0x40 = 0 est placee plus tard dans la naissance, sans lecture entre les deux places.
    //
    // Le bloc 4-9 lit la tuile du grain a 0x00550f30, deja relevee (MineParticles.cs).
    // =====================================================================

    private static readonly byte[] s_Descriptors_00550C68 =
    [
        0x00, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550c68  <- 0x00432D80
        0x18, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550c70
        0x30, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550c78
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550c80  <- 0x00432C90
    ];

    private static readonly byte[] s_Descriptors_00550D58 =
    [
        0x00, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550d58  <- 0x004342B0
        0x18, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550d60
        0x30, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550d68
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550d70  <- 0x004341C0
    ];

    private static readonly byte[] s_Descriptors_00550E68 =
    [
        0x00, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550e68  <- 0x00435E50
        0x18, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550e70
        0x30, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550e78
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550e80  <- 0x00435D60
    ];

    /// <summary>Le bloc 4-9 : trois enregistrements seulement, la tuile du grain qui suit etant
    /// deja <c>s_Descriptors_00550F30</c>.</summary>
    private static readonly byte[] s_Descriptors_00550F18 =
    [
        0x00, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550f18  <- 0x00438BD0
        0x18, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550f20
        0x30, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00550f28
    ];

    private static readonly byte[] s_Descriptors_00551048 =
    [
        0x00, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00551048  <- 0x0043B750
        0x18, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00551050
        0x30, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x00551058
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00551060  <- 0x0043B660
    ];

    private static byte* s_Descriptors00550C68Storage;
    private static byte* s_Descriptors00550D58Storage;
    private static byte* s_Descriptors00550E68Storage;
    private static byte* s_Descriptors00550F18Storage;
    private static byte* s_Descriptors00551048Storage;

    private static byte* Descriptors00550C68() => NativeCopy(s_Descriptors_00550C68, ref s_Descriptors00550C68Storage);
    private static byte* Descriptors00550D58() => NativeCopy(s_Descriptors_00550D58, ref s_Descriptors00550D58Storage);
    private static byte* Descriptors00550E68() => NativeCopy(s_Descriptors_00550E68, ref s_Descriptors00550E68Storage);
    private static byte* Descriptors00550F18() => NativeCopy(s_Descriptors_00550F18, ref s_Descriptors00550F18Storage);
    private static byte* Descriptors00551048() => NativeCopy(s_Descriptors_00551048, ref s_Descriptors00551048Storage);

    /// <summary>Ghidra: <c>0x00432d80</c>, <c>0x004342b0</c>, <c>0x00435e50</c>, <c>0x00438bd0</c>,
    /// <c>0x0043b750</c> — l'eclat attire des blocs 0, 2-3, 5-8-10, 4-9 et 6-7. Absents de
    /// <c>Blaze.c</c> ; <see cref="TickHomingGlint_00417650"/> a la planche et a l'echelle de
    /// base pres (<c>0x700</c>).</summary>
    private static void EntityHandler_00432D80(byte* e) => TickHomingGlint_00417650(e, Descriptors00550C68(), 0x700);
    private static void EntityHandler_004342B0(byte* e) => TickHomingGlint_00417650(e, Descriptors00550D58(), 0x700);
    private static void EntityHandler_00435E50(byte* e) => TickHomingGlint_00417650(e, Descriptors00550E68(), 0x700);
    private static void EntityHandler_00438BD0(byte* e) => TickHomingGlint_00417650(e, Descriptors00550F18(), 0x700);
    private static void EntityHandler_0043B750(byte* e) => TickHomingGlint_00417650(e, Descriptors00551048(), 0x700);

    /// <summary>Ghidra: <c>0x00432c90</c>, <c>0x004341c0</c>, <c>0x00435d60</c>, <c>0x00438ae0</c>,
    /// <c>0x0043b660</c> — le grain qui monte des memes cinq blocs. Absents de <c>Blaze.c</c> ;
    /// <see cref="TickLabyrinthRisingMote"/> a la planche pres.</summary>
    private static void EntityHandler_00432C90(byte* e) => TickLabyrinthRisingMote(e, Descriptors00550C68() + 0x18);   // &DAT_00550c80
    private static void EntityHandler_004341C0(byte* e) => TickLabyrinthRisingMote(e, Descriptors00550D58() + 0x18);   // &DAT_00550d70
    private static void EntityHandler_00435D60(byte* e) => TickLabyrinthRisingMote(e, Descriptors00550E68() + 0x18);   // &DAT_00550e80
    private static void EntityHandler_00438AE0(byte* e) => TickLabyrinthRisingMote(e, Descriptors00550F30());          // &DAT_00550f30
    private static void EntityHandler_0043B660(byte* e) => TickLabyrinthRisingMote(e, Descriptors00551048() + 0x18);   // &DAT_00551060

    // =====================================================================
    // LOT 2 — SIX FAMILLES DE JUMEAUX, une par corps, recopiees d'un bloc a l'autre
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00550bd0</c>, <c>00550ce0</c>, <c>00550dd0</c> — <b>la fumee
    /// qui monte</b>, trois tuiles <c>0x20 x 0x20</c> a <c>y = 0x88</c> par bloc (0 ; 2-3 ; 5-8-10).
    /// Bornees en haut par la meme tuile a <c>x = 0x40</c> qui ouvre la planche suivante.
    /// <b>Le corps ecrit dans sa planche</b> : l'octet 5 du premier enregistrement passe de 3 a 2
    /// a chaque naissance.</summary>
    private static readonly byte[] s_Descriptors_00550BD0 =
    [
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550bd0  <- 0x004320B0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550bd8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550be0
        0x40, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550be8  <- 0x004327A0
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550bf0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550bf8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550c00
    ];

    private static readonly byte[] s_Descriptors_00550CE0 =
    [
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550ce0  <- 0x00433770
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550ce8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550cf0
        0x40, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550cf8  <- 0x00433CD0
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550d00
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550d08
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550d10
    ];

    private static readonly byte[] s_Descriptors_00550DD0 =
    [
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550dd0  <- 0x00435310
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550dd8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550de0
        0x40, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550de8  <- 0x00435870
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550df0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550df8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550e00
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550c48</c>, <c>00550d38</c>, <c>00550e48</c> — <b>la
    /// poussiere qui s'ecarte</b>, quatre tuiles <c>0x18 x 0x18</c> a <c>y = 0x30</c>, couche 2.
    /// Bornees par la meme tuile en couche 3 (<c>0x...40</c>) et par l'eclat attire
    /// (<c>0x...68</c>).</summary>
    private static readonly byte[] s_Descriptors_00550C48 =
    [
        0x48, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550c48  <- 0x00432520
        0x60, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550c50
        0x78, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550c58
        0x90, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550c60
    ];

    private static readonly byte[] s_Descriptors_00550D38 =
    [
        0x48, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550d38  <- 0x00433A50
        0x60, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550d40
        0x78, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550d48
        0x90, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550d50
    ];

    private static readonly byte[] s_Descriptors_00550E48 =
    [
        0x48, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550e48  <- 0x004355F0
        0x60, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550e50
        0x78, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550e58
        0x90, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550e60
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550f68</c> (sous-zones 4 et 9) et <c>&amp;DAT_00551220</c>
    /// (11 et 12) — <b>les etincelles</b>, quatre tuiles <c>0x20 x 0x20</c> a <c>y = 0x88</c>,
    /// couche 2.</summary>
    private static readonly byte[] s_Descriptors_00550F68 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550f68  <- 0x00437AA0
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550f70
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550f78
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550f80
    ];

    private static readonly byte[] s_Descriptors_00551220 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551220  <- 0x0043DE50
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551228
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551230
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551238
    ];

    private static byte* s_Descriptors00550BD0Storage;
    private static byte* s_Descriptors00550CE0Storage;
    private static byte* s_Descriptors00550DD0Storage;
    private static byte* s_Descriptors00550C48Storage;
    private static byte* s_Descriptors00550D38Storage;
    private static byte* s_Descriptors00550E48Storage;
    private static byte* s_Descriptors00550F68Storage;
    private static byte* s_Descriptors00551220Storage;

    private static byte* Descriptors00550BD0() => NativeCopy(s_Descriptors_00550BD0, ref s_Descriptors00550BD0Storage);
    private static byte* Descriptors00550CE0() => NativeCopy(s_Descriptors_00550CE0, ref s_Descriptors00550CE0Storage);
    private static byte* Descriptors00550DD0() => NativeCopy(s_Descriptors_00550DD0, ref s_Descriptors00550DD0Storage);
    private static byte* Descriptors00550C48() => NativeCopy(s_Descriptors_00550C48, ref s_Descriptors00550C48Storage);
    private static byte* Descriptors00550D38() => NativeCopy(s_Descriptors_00550D38, ref s_Descriptors00550D38Storage);
    private static byte* Descriptors00550E48() => NativeCopy(s_Descriptors_00550E48, ref s_Descriptors00550E48Storage);
    private static byte* Descriptors00550F68() => NativeCopy(s_Descriptors_00550F68, ref s_Descriptors00550F68Storage);
    private static byte* Descriptors00551220() => NativeCopy(s_Descriptors_00551220, ref s_Descriptors00551220Storage);

    // ---------------------------------------------------------------- la salve d'incantation

    /// <summary>
    /// Ghidra: <c>0x00431a20</c> (bloc 0), <c>0x00433050</c> (2-3), <c>0x00434c80</c> (5-8-10) —
    /// table <c>0x0056b0c8</c>, trois corps identiques sans aucune immediate differente. Absents de
    /// <c>Blaze.c</c> ; 43 instructions.
    ///
    /// <para><b>Vingt images de naissances <c>0xfc</c></b>, une par image, sans planche ; puis le
    /// son <c>0x26</c> de portee (hauteur <c>0x5dc</c>), l'entite d'incantation <c>0xfe</c>, et
    /// l'extinction.</para>
    /// </summary>
    private static void EntityHandler_00431A20(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x14;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))--;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfc, 0, -1);   // Ghidra: FUN_0047ac40
            if (*(short*)(e + 0x12) <= 0) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) != 2) return;

        TryPlaySoundNearEntity((uint*)e, 0x26, 0x5dc, 0x2000, 0, 0);                 // Ghidra: FUN_00468080
        SpawnCastEntity_0047AAE0(e, 0xfe, 0, -1);                                      // Ghidra: FUN_0047aae0
        *(uint*)e &= 0x7fffffff;
    }

    // ---------------------------------------------------------------- l'eclat qui colle a son porteur

    /// <summary>
    /// Ghidra: <c>0x00432190</c>, <c>0x00433850</c>, <c>0x004353f0</c> — trois corps a la planche
    /// pres (celle de l'eclat attire, trois images). Absents de <c>Blaze.c</c> ; 52 instructions.
    ///
    /// <para><b>Dix images collees a son porteur</b> <c>+0x6c</c> (ou a la liste d'entites), a
    /// chaque image : il en recopie la position, tire une image sur trois et deux echelles de
    /// <c>0x1000</c> a <c>0x17ff</c> — <b>en <c>+0x28</c> et <c>+0x2c</c></b>, pas <c>+0x2a</c>.</para>
    /// </summary>
    private static void TickClingingGlint(byte* e, byte* plate)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null) host = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(host + 0x1c);
        *(int*)(e + 0x20) = *(int*)(host + 0x20);
        *(int*)(e + 0x24) = *(int*)(host + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 10;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))--;
        *(short*)(e + 0x28) = (short)(SignedMask(CrtRand(), 0x7ff) + 0x1000);
        *(short*)(e + 0x2c) = (short)(SignedMask(CrtRand(), 0x7ff) + 0x1000);
        e[0x40] = (byte)(CrtRand() % 3);

        if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
    }

    private static void EntityHandler_00432190(byte* e) => TickClingingGlint(e, Descriptors00550C68());   // Ghidra: &DAT_00550c68
    private static void EntityHandler_00433850(byte* e) => TickClingingGlint(e, Descriptors00550D58());   // Ghidra: &DAT_00550d58
    private static void EntityHandler_004353F0(byte* e) => TickClingingGlint(e, Descriptors00550E68());   // Ghidra: &DAT_00550e68

    // ---------------------------------------------------------------- la fumee qui monte

    /// <summary>
    /// Ghidra: <c>0x004320b0</c>, <c>0x00433770</c>, <c>0x00435310</c> — trois corps a la planche
    /// pres. Absents de <c>Blaze.c</c> ; 66 instructions.
    ///
    /// <para>
    /// Une volute teintee <c>(0x94, 0x8a, 0x80)</c>, role 4, en echelle <c>0x400</c> a
    /// <c>0xbff</c>, qui monte de 3 a 8 unites par image en s'assombrissant de 4, et change
    /// d'image toutes les 3 a 6 images ; a la troisieme, elle s'eteint. <b>A chaque naissance, le
    /// corps ecrit 2 dans l'octet 5 du premier enregistrement de sa planche</b> (<c>mov byte
    /// [550BD5h],2</c>) : la planche de 1998 est modifiee en place, et le reste.
    /// </para>
    /// </summary>
    private static void TickRisingSmoke(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;
            plate[5] = 2;                                          // Ghidra: DAT_00550bd5 (et ses copies)
            e[0x40] = 0;

            int draw = CrtRand();
            e[0x38] = 0x94;
            e[0x39] = 0x8a;
            e[0x3a] = 0x80;
            short scale = (short)(SignedMask(draw, 0x7ff) + 0x400);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            int beat = CrtRand();
            *(short*)(e + 0x10) = 1;
            short step = (short)(SignedMask(beat, 3) + 3);
            *(short*)(e + 0x12) = step;
            *(short*)(e + 0x14) = step;

            int rise = CrtRand();
            e[0x0a] = 4;
            e[0x0b] = 0;
            *(short*)(e + 0x16) = (short)(rise % 6 + 3);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) += *(short*)(e + 0x16) << 0xc;
        BrightenEntityTint_00404DE0(e, -4, -4, -4);               // Ghidra: FUN_00404de0

        (*(short*)(e + 0x14))--;
        if (*(short*)(e + 0x14) > 0) return;

        e[0x40]++;
        *(short*)(e + 0x14) = *(short*)(e + 0x12);
        if (e[0x40] == 3) *(uint*)e &= 0x7fffffff;
    }

    private static void EntityHandler_004320B0(byte* e) => TickRisingSmoke(e, Descriptors00550BD0());   // Ghidra: &DAT_00550bd0
    private static void EntityHandler_00433770(byte* e) => TickRisingSmoke(e, Descriptors00550CE0());   // Ghidra: &DAT_00550ce0
    private static void EntityHandler_00435310(byte* e) => TickRisingSmoke(e, Descriptors00550DD0());   // Ghidra: &DAT_00550dd0

    // ---------------------------------------------------------------- la poussiere qui s'ecarte

    /// <summary>
    /// Ghidra: <c>0x00432520</c>, <c>0x00433a50</c>, <c>0x004355f0</c> — trois corps a la planche
    /// pres. Absents de <c>Blaze.c</c> ; 81 instructions.
    ///
    /// <para>Une bouffee teinte <c>0x40</c>, role 9, en echelle <c>0x1000</c>, qui file a vitesse
    /// constante (±5 a plat, 5 a 9 vers le haut), grandit de 100 par image, s'assombrit de 3, et
    /// change d'image toutes les 2 a 6 images ; a la quatrieme, elle s'eteint.</para>
    /// </summary>
    private static void TickSpreadingDust(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;

            *(int*)(e + 0x50) = CrtRand() % 10 - 5;
            *(int*)(e + 0x58) = CrtRand() % 10 - 5;
            *(int*)(e + 0x54) = CrtRand() % 5 + 5;

            short beat = (short)(CrtRand() % 5 + 2);
            *(short*)(e + 0x14) = beat;
            *(short*)(e + 0x12) = beat;
            e[0x38] = 0x40;
            e[0x39] = 0x40;
            e[0x3a] = 0x40;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50) << 0xc;
        *(int*)(e + 0x24) += *(int*)(e + 0x58) << 0xc;
        *(int*)(e + 0x20) += *(int*)(e + 0x54) << 0xc;
        *(short*)(e + 0x28) += 100;
        *(short*)(e + 0x2a) += 100;
        BrightenEntityTint_00404DE0(e, -3, -3, -3);               // Ghidra: FUN_00404de0

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) > 0) return;

        e[0x40]++;
        *(short*)(e + 0x12) = *(short*)(e + 0x14);
        if (e[0x40] == 4) *(uint*)e &= 0x7fffffff;
    }

    private static void EntityHandler_00432520(byte* e) => TickSpreadingDust(e, Descriptors00550C48());   // Ghidra: &DAT_00550c48
    private static void EntityHandler_00433A50(byte* e) => TickSpreadingDust(e, Descriptors00550D38());   // Ghidra: &DAT_00550d38
    private static void EntityHandler_004355F0(byte* e) => TickSpreadingDust(e, Descriptors00550E48());   // Ghidra: &DAT_00550e48

    // ---------------------------------------------------------------- la bulle qui eclate en arc

    /// <summary>
    /// Ghidra: <c>0x004327a0</c>, <c>0x00433cd0</c>, <c>0x00435870</c> — trois corps a la planche
    /// pres. Absents de <c>Blaze.c</c> ; 87 instructions.
    ///
    /// <para>
    /// <b>Son image vient du placement</b> : <c>3 - (+0x16)</c>, sa planche en compte quatre — une
    /// fiche portant <c>+0x16</c> au-dela de 3 ferait lire au binaire un octet negatif. Deux images
    /// d'attente ; puis, si <c>+0x16</c> valait 3, le son <c>0x1e</c> ; <c>+0x16</c> perd un et, tant
    /// qu'il reste positif, une gerbe <c>0x32</c> pres de la camera. Enfin elle decrit un
    /// <b>demi-cercle vertical</b> de rayon 80 a 129 autour de sa naissance (<c>+0x9a</c> de 0 a
    /// <c>0x800</c> par pas de 100 : cosinus en X, sinus en hauteur) et s'eteint.
    /// </para>
    /// </summary>
    private static void TickArcingBubble(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            byte step = e[0x16];
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = (byte)(3 - step);
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 2;

            int draw = CrtRand();
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            short scale = (short)(SignedMask(draw, 0x3ff) + 0xc00);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            *(int*)(e + 0x5c) = CrtRand() % 0x32 + 0x50;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))--;
            if (*(short*)(e + 0x12) < 0)
            {
                if (*(short*)(e + 0x16) == 3)
                    TryPlaySoundNearEntity((uint*)e, 0x1e, 0x200, 0x1800, 0, 0);   // Ghidra: FUN_00468080

                (*(short*)(e + 0x16))--;
                *(short*)(e + 0x10) = 2;

                if (*(short*)(e + 0x16) < 0) *(short*)(e + 0x16) = 0;
                else SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);           // Ghidra: FUN_00468110
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        short angle = *(short*)(e + 0x9a);
        *(int*)(e + 0x1c) = Winsys3d.GetCosTableEntry(angle) * *(int*)(e + 0x5c) + *(int*)(e + 0x50);        // Ghidra: FUN_00545a16
        *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(angle) * *(int*)(e + 0x5c) + *(int*)(e + 0x54);   // Ghidra: FUN_00545a1c
        *(short*)(e + 0x9a) += 100;

        if (*(short*)(e + 0x9a) > 0x800) *(uint*)e &= 0x7fffffff;
    }

    private static void EntityHandler_004327A0(byte* e) => TickArcingBubble(e, Descriptors00550BD0() + 0x18);   // Ghidra: &DAT_00550be8
    private static void EntityHandler_00433CD0(byte* e) => TickArcingBubble(e, Descriptors00550CE0() + 0x18);   // Ghidra: &DAT_00550cf8
    private static void EntityHandler_00435870(byte* e) => TickArcingBubble(e, Descriptors00550DD0() + 0x18);   // Ghidra: &DAT_00550de8

    // ---------------------------------------------------------------- les etincelles

    /// <summary>
    /// Ghidra: <c>0x00437aa0</c> (sous-zones 4 et 9) et <c>0x0043de50</c> (11 et 12) — deux corps
    /// a la planche pres. Absents de <c>Blaze.c</c> ; 82 instructions.
    ///
    /// <para>
    /// Une etincelle semee a ±150 unites a plat et 0 a 149 au-dessus, en echelle <c>0xc00</c> a
    /// <c>0x13ff</c>, role 6, qui avance d'une image toutes les deux et s'eteint a la quatrieme.
    /// <b>En sous-zone 16, elle serait semee plus serre (±75) et en role 9</b> — mais la grotte n'a
    /// que les sous-zones 0 a 15 : la branche est morte dans cette zone. Transcrite telle quelle.
    /// </para>
    /// </summary>
    private static void TickCaveSpark(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;
            short scale = (short)(SignedMask(CrtRand(), 0x7ff) + 0xc00);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            if (s_Unknown_005de52c == 0x10)
            {
                e[0x0a] = 9;
                *(int*)(e + 0x1c) += (CrtRand() % 0x96 - 0x4b) << 0xc;
                *(int*)(e + 0x24) += (CrtRand() % 0x96 - 0x4b) << 0xc;
            }
            else
            {
                e[0x0a] = 6;
                *(int*)(e + 0x1c) += (CrtRand() % 300 - 0x96) << 0xc;
                *(int*)(e + 0x24) += (CrtRand() % 300 - 0x96) << 0xc;
            }

            int lift = CrtRand();
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x20) += (lift % 0x96) << 0xc;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) < 2) return;

        *(short*)(e + 0x12) = 0;
        e[0x40]++;
        if (e[0x40] >= 4) *(uint*)e &= 0x7fffffff;
    }

    private static void EntityHandler_00437AA0(byte* e) => TickCaveSpark(e, Descriptors00550F68());   // Ghidra: &DAT_00550f68
    private static void EntityHandler_0043DE50(byte* e) => TickCaveSpark(e, Descriptors00551220());   // Ghidra: &DAT_00551220

    // =====================================================================
    // L'AMORCE D'INCANTATION
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0043bf20 @ 0x0043bf20 — table <c>0x0056b0c8</c>, sous-zone 1. Decompilee dans
    /// <c>Blaze.c</c>, 15 instructions. <b>Une amorce</b> : a sa premiere image elle fait naitre
    /// l'entite d'incantation <c>0xff</c> (<see cref="SpawnCastEntity_0047AAE0"/>) et s'eteint. Sa
    /// voisine du decor, <see cref="EntityHandler_0046C800"/>, fait naitre <c>0xfd</c> et reste.
    /// </summary>
    private static void EntityHandler_0043BF20(byte* e)
    {
        if (*(short*)(e + 0x10) != 0) return;

        SpawnCastEntity_0047AAE0(e, 0xff, 0, -1);                   // Ghidra: FUN_0047aae0
        *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LOT 4 (2026-09-19) — les « B » des blocs 0 a 3
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00468810</c> (bloc 0, sous-type <c>0x33</c>) et <c>0x0046f180</c> (6-7,
    /// <c>0x3a</c>) — deux corps a cette seule immediate pres. Absents de <c>Blaze.c</c> ;
    /// 23 instructions. <b>La gerbe au hasard</b> : une image sur trente, neuf effets d'un coup
    /// pres de la camera (<see cref="SpawnEffectNearCamera_00468110"/>). Aucun etat, aucune mort.
    /// </summary>
    private static void TickRandomBurst_00468810(byte* e, short subtype)
    {
        // Ghidra: `cmp dx,1` — le reste de la division, compare sur seize bits.
        if ((short)(CrtRand() % 0x1e) != 1) return;

        for (int i = 9; i != 0; i--) SpawnEffectNearCamera_00468110(e, subtype, 0, -1);   // Ghidra: FUN_00468110
    }

    /// <summary>
    /// Ghidra: <c>0x0046a780</c> — sous-zones 2 et 3. Absente de <c>Blaze.c</c> ; 45 instructions.
    /// <b>L'emetteur qui tousse</b> : une fois sur trois a sa naissance, puis, passe 70 images, une
    /// chance sur vingt par image — et au plus tard a la centieme —, l'effet <c>0x36</c> herite
    /// (<see cref="SpawnInheritedEffectNearCamera_0045C000"/>) ; le compte repart de zero.
    /// </summary>
    private static void EntityHandler_0046A780(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;

            if (CrtRand() % 3 == 1) SpawnInheritedEffectNearCamera_0045C000(e, 0x36, 0, -1);   // Ghidra: FUN_0045c000
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) += 1;
        if (*(short*)(e + 0x12) <= 0x46) return;

        if (CrtRand() % 0x14 != 1 && *(short*)(e + 0x12) <= 0x64) return;

        *(short*)(e + 0x12) = 0;
        SpawnInheritedEffectNearCamera_0045C000(e, 0x36, 0, -1);    // Ghidra: FUN_0045c000
    }

    /// <summary>
    /// Ghidra: <c>0x004333b0</c> — sous-zones 0, 2, 3, 5, 8 et 10, table <c>0x0056b0c8</c>. Presente
    /// dans <c>Blaze.c</c> ; 44 instructions. <b>Deux bouffees, puis rien</b> : tous les 5 a 14
    /// images, une naissance — le sous-type <c>0xfd</c> a sa position si <c>+0x9a</c> est nul,
    /// l'incantation <c>0xff</c> sinon —, et a la seconde elle meurt (bit de vie efface, la
    /// naissance a encore lieu dans la meme image).
    /// </summary>
    private static void EntityHandler_004333B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int r = CrtRand();
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(r % 10 + 5);
        }

        *(short*)(e + 0x12) -= 1;
        if (*(short*)(e + 0x12) >= 0) return;

        int next = CrtRand() % 10 + 5;
        *(short*)(e + 0x14) += 1;
        *(short*)(e + 0x12) = (short)next;
        if (*(short*)(e + 0x14) >= 2) *(uint*)e &= 0x7fffffff;

        if (*(short*)(e + 0x9a) == 0)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfd, 0, -1);   // Ghidra: FUN_0047ac40
            return;
        }

        SpawnCastEntity_0047AAE0(e, 0xff, 0, -1);                   // Ghidra: FUN_0047aae0
    }

    /// <summary>
    /// Ghidra: <c>0x0043bf50</c> — sous-zone 1, table <c>0x0056b1c8</c>. Absente de <c>Blaze.c</c> ;
    /// 64 instructions. <b>L'eclair de modele</b> : ne sur sa vitesse — <c>+0x50</c> devient
    /// <c>+0x34</c>, <c>+0x54</c> devient <c>+0x28</c>, <c>+0x58</c> sa duree —, releve de 160
    /// unites, tourne de trois quarts de tour, echelles <c>0x1770</c>, modele 8, teinte a zero.
    /// Ensuite il tire son modele de 8 a 11 a chaque image et s'eclaire de 50 ; sa duree ecoulee,
    /// il s'assombrit de 30 et meurt des que l'octet <c>+0x38</c> de sa teinte est nul.
    /// </summary>
    private static void EntityHandler_0043BF50(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] &= 0xfe;
            *(short*)(e + 0x32) += 0x0c00;
            *(short*)(e + 0x34) = *(short*)(e + 0x50);
            *(short*)(e + 0x2c) = 0x1770;
            *(short*)(e + 0x2a) = 0x1770;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) += 0xa0000;
            e[4] = 8;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = *(short*)(e + 0x54);
            *(short*)(e + 0x16) = *(short*)(e + 0x58);
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        e[4] = (byte)(CrtRand() % 4 + 8);
        *(short*)(e + 0x16) -= 1;

        if (*(short*)(e + 0x16) >= 0)
        {
            BrightenEntityTint_00404DE0(e, 0x32, 0x32, 0x32);        // Ghidra: FUN_00404de0
            return;
        }

        BrightenEntityTint_00404DE0(e, -30, -30, -30);
        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LA BOUFFEE OBLIQUE — quatre copies de 0x00418AF0 (la vieille tour), table 0x0056b2c8
    //
    // Chacune pose sa planche d'UNE tuile 0x18 x 0x18 page 2 — les memes octets que 0x0054fbf8 —,
    // bornee en haut par une forme differente. Marge 1, et sure : le corps met +0x40 a zero a sa
    // naissance et ne le touche plus.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00550cb8</c> — bloc 0. Bornee par l'eclat qui tombe des
    /// sous-zones 2-3 (<c>0x00550cc0</c>).</summary>
    private static readonly byte[] s_Descriptors_00550CB8 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550cb8  <- 0x00432240
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550da8</c> — sous-zones 2 et 3. Bornee par l'eclat qui tombe
    /// des sous-zones 5-8-10 (<c>0x00550db0</c>).</summary>
    private static readonly byte[] s_Descriptors_00550DA8 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550da8  <- 0x00433900
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550eb8</c> — sous-zones 5, 8 et 10. Bornee par une tuile
    /// <c>0x20 x 0x20</c> page 3 (<c>0x00550ec0</c>).</summary>
    private static readonly byte[] s_Descriptors_00550EB8 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550eb8  <- 0x004354A0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00551160</c> — sous-zone 1. Bornee par une tuile
    /// <c>0x0f x 0x0f</c> (<c>0x00551168</c>).</summary>
    private static readonly byte[] s_Descriptors_00551160 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551160  <- 0x0043BDD0
    ];

    private static byte* s_Descriptors00550CB8Storage;
    private static byte* s_Descriptors00550DA8Storage;
    private static byte* s_Descriptors00550EB8Storage;
    private static byte* s_Descriptors00551160Storage;

    private static byte* Descriptors00550CB8() => NativeCopy(s_Descriptors_00550CB8, ref s_Descriptors00550CB8Storage);
    private static byte* Descriptors00550DA8() => NativeCopy(s_Descriptors_00550DA8, ref s_Descriptors00550DA8Storage);
    private static byte* Descriptors00550EB8() => NativeCopy(s_Descriptors_00550EB8, ref s_Descriptors00550EB8Storage);
    private static byte* Descriptors00551160() => NativeCopy(s_Descriptors_00551160, ref s_Descriptors00551160Storage);

    /// <summary>Ghidra: <c>0x00432240</c> — la bouffee oblique du bloc 0 ; montee de 6 a 11.</summary>
    private static void EntityHandler_00432240(byte* e) => TickSlantedPuff(e, Descriptors00550CB8(), 6, 6);

    /// <summary>Ghidra: <c>0x0043bdd0</c> — celle de la sous-zone 1.</summary>
    private static void EntityHandler_0043BDD0(byte* e) => TickSlantedPuff(e, Descriptors00551160(), 6, 6);

    /// <summary>Ghidra: <c>0x00433900</c> — celle des sous-zones 2 et 3.</summary>
    private static void EntityHandler_00433900(byte* e) => TickSlantedPuff(e, Descriptors00550DA8(), 6, 6);

    /// <summary>Ghidra: <c>0x004354a0</c> — celle des sous-zones 5, 8 et 10.</summary>
    private static void EntityHandler_004354A0(byte* e) => TickSlantedPuff(e, Descriptors00550EB8(), 6, 6);

    /// <summary>
    /// Ghidra: <c>0x004314c0</c> — sous-zone 0. Absente de <c>Blaze.c</c> ; 95 instructions.
    /// <b>La traine qui suit son porteur</b> : chaque image elle se pose sur son porteur
    /// (<c>+0x6c</c>, recopie en <c>+0x74</c> ; a defaut l'enregistrement modele
    /// <c>0x005de558</c>) 150 unites plus haut, prend son cap, avance de 50 unites dans ce cap,
    /// redescend de 10, et seme le sous-type <c>0xde</c> a sa position
    /// (<see cref="SpawnStatusEffectEntity_0047AC40"/>).
    /// <para>
    /// Sons : la voix <b>brute</b> <c>0x1f</c> a la naissance (pas une voix de zone : l'identifiant
    /// est pousse tel quel), puis toutes les cinq images la voix <c>0x23</c> si son <c>+0x9a</c>
    /// vaut 1, <c>0x20</c> sinon. Elle meurt quand le rebours <c>+0x43</c> du porteur vaut 1, ou que
    /// le porteur est mort (<c>0x40000000</c>, ou plus de bit de vie).
    /// </para>
    /// </summary>
    /// <param name="seed">Le sous-type seme : <c>0xde</c> ; <c>0xdf</c> pour sa copie <c>0x004343D0</c>
    /// (sous-zones 5, 8 et 10, lot 5), qui ne differe que par ce <c>push</c>.</param>
    private static void EntityHandler_004314C0(byte* e, byte seed = 0xde)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        *(byte**)(e + 0x74) = carrier;
        if (carrier == null) carrier = Res.s_EntityList_005de558;   // Ghidra: 0x005de558

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x96000;
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
        *(short*)(e + 0x32) = *(short*)(carrier + 0x32);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            TryPlaySoundNearEntity((uint*)e, 0x1f, 0x6fd, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x98) == 1)
        {
            short voice = (short)(*(short*)(e + 0x9a) == 1 ? 0x23 : 0x20);
            TryPlaySoundNearEntity((uint*)e, voice, 0x1000, 0x800, 0, 0);
        }

        if (*(short*)(e + 0x98) == 5) *(short*)(e + 0x98) = 0;
        *(short*)(e + 0x98) += 1;

        if (carrier[0x43] == 1) *(uint*)e &= 0x7fffffff;

        uint life = *(uint*)carrier;
        if ((life & 0x40000000) != 0 || (life & 0x80000000) == 0) *(uint*)e &= 0x7fffffff;

        short course = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 50;
        *(int*)(e + 0x24) += (int)Winsys3d.GetCosTableEntry(course) * 50;
        *(int*)(e + 0x20) -= 0xa000;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, seed, 0, -1);     // Ghidra: FUN_0047ac40
    }

    // =====================================================================
    // LOT 5 (2026-09-19) — six copies retrouvees par la recherche de parente
    //
    // Chacune est le corps d'une autre zone a quelques immediates pres ; le parent a pris ces
    // immediates en parametres, defauts inchanges.
    // =====================================================================

    // La planche de la braise, 0x005510a8, est deja declaree dans Combat/Projectiles.cs (le meme
    // releve sert un corps de la grotte ecrit plus tot) : la braise mene +0x40 de 0 a 3 et meurt a
    // 4, marge exacte sur ses quatre enregistrements.

    /// <summary>Ghidra: <c>&amp;DAT_005511f0</c> — une tuile <c>0x08 x 0x08</c> page 3, celle de
    /// <c>0x0054fd58</c>. Bornee en bas par une tuile <c>0x18 x 0x18</c>, en haut par une
    /// <c>0x10 x 0x10</c> (<c>0x005511f8</c>). Marge 1 : l'helice et la goutte posent
    /// <c>+0x40 = 0</c> et n'y touchent plus.</summary>
    private static readonly byte[] s_Descriptors_005511F0 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005511f0  <- 0x0043D3A0, 0x0043DA60
    ];

    private static byte* s_Descriptors005511F0Storage;

    private static byte* Descriptors005511F0() => NativeCopy(s_Descriptors_005511F0, ref s_Descriptors005511F0Storage);

    /// <summary>Ghidra: <c>0x0043eb60</c> — sous-zones 13 a 15. La salve battante de la mine
    /// (<see cref="EntityHandler_00409E40"/>) semant le sous-type <c>0xe1</c> au lieu de
    /// <c>0xde</c>.</summary>
    private static void EntityHandler_0043EB60(byte* e) => EntityHandler_00409E40(e, 0xe1);

    /// <summary>Ghidra: <c>0x0043adc0</c> — sous-zones 6 et 7. La braise de la foret
    /// (<see cref="TickRisingEmber"/>, famille A), <c>+0x0a = 2</c>, teinte qui ne perd que 2 par
    /// image, planche <c>0x005510a8</c>.</summary>
    private static void EntityHandler_0043ADC0(byte* e) => TickRisingEmber(e, Descriptors005510A8(), 2, false, -2);

    /// <summary>Ghidra: <c>0x0043cd70</c> — sous-zones 11 et 12. L'onde de froid de la vallee
    /// (<see cref="TickColdWave"/>), bleue au lieu de rouge.</summary>
    private static void EntityHandler_0043CD70(byte* e) => TickColdWave(e, 0xca, 0x0f, 0xbe, 0xfa);

    /// <summary>Ghidra: <c>0x0043d3a0</c> — sous-zones 11 et 12. L'helice de la tour
    /// (<see cref="TickHelixMote"/>), <c>+0x0a = 9</c>.</summary>
    private static void EntityHandler_0043D3A0(byte* e) => TickHelixMote(e, Descriptors005511F0(), 9);

    /// <summary>Ghidra: <c>0x0043da60</c> — sous-zones 11 et 12. La goutte de la tour
    /// (<see cref="TickFallingDrop"/>), <c>+0x0a = 6</c>.</summary>
    private static void EntityHandler_0043DA60(byte* e) => TickFallingDrop(e, Descriptors005511F0(), 6);

    /// <summary>
    /// Ghidra: <c>0x0043aef0</c> — sous-zones 6 et 7, table de debordement. Absente de <c>Blaze.c</c> ;
    /// 43 instructions. <b>Les braises aux pieds</b> : une seule image. Pour chaque membre debout,
    /// au sol, dont le mot de contact porte <c>0x100</c>, une chance sur cinq de semer le sous-type
    /// <c>0xef</c> a ses pieds (<see cref="SpawnStatusEffectEntity_0047AC40"/>) ; puis elle meurt.
    /// </summary>
    private static void EntityHandler_0043AEF0(byte* e)
    {
        byte* member = Inp.s_SlotFlags_005e93c0;
        for (int i = 4; i != 0; i--, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            if ((*(uint*)(member + 0x90) & 0x100) == 0) continue;
            if ((flags & 1) != 0) continue;

            // Ghidra: `cmp dx,1` — le reste, compare sur seize bits.
            if ((short)(CrtRand() % 5) != 1) continue;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(member + 0x1c), null, 0xef, 0, -1);   // Ghidra: FUN_0047ac40
        }

        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0043a860</c> — sous-zones 6 et 7. Absente de <c>Blaze.c</c> ; 44 instructions.
    /// <b>L'eclat projete</b> : ne avec pour modele son <c>+0x14</c>, a plat, incline d'un quart de
    /// tour, il file sur sa vitesse entiere (<c>+0x50</c>, <c>+0x58</c> en unites, <c>+0x5c</c> en
    /// demi-unites vers le haut, qui decroit d'une par image) et meurt quand elle passe sous zero —
    /// un arc. Son <c>+0x14</c> (60 a la naissance) est decompte sans etre lu.
    /// </summary>
    private static void EntityHandler_0043A860(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[4] = e[0x14];
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x34) = 0x400;
            *(short*)(e + 0x14) = 0x3c;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int lift = *(int*)(e + 0x5c);
        *(int*)(e + 0x1c) += *(int*)(e + 0x50) << 0xc;
        *(int*)(e + 0x24) += *(int*)(e + 0x58) << 0xc;
        *(int*)(e + 0x20) += lift << 0xb;

        lift--;
        *(short*)(e + 0x14) -= 1;
        *(int*)(e + 0x5c) = lift;
        if (lift < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00434be0</c> — sous-zones 5, 8 et 10, table de debordement. Presente dans
    /// <c>Blaze.c</c> ; 50 instructions. <b>La flamme qui ne s'eteint pas</b> : nee 120 unites plus
    /// haut, teinte <c>0xc8</c>, echelles <c>0x34bc / 0x1770 / 0x1770</c>, elle tire son modele de 8 a
    /// 11 a chaque image ; quand le bit <c>0x02</c> de l'octet <c>0x22</c> de la banque 0 est pose,
    /// elle s'assombrit (-8, -6, -8). Elle ne meurt jamais d'elle-meme.
    /// </summary>
    private static void EntityHandler_00434BE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int r = CrtRand();
            e[0x0a] = 6;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0x34bc;
            *(short*)(e + 0x36) = -2;
            *(short*)(e + 0x10) = 1;
            e[4] = (byte)(r % 4 + 8);
            *(short*)(e + 0x2a) = 0x1770;
            *(short*)(e + 0x2c) = 0x1770;
            *(int*)(e + 0x20) += 0x78000;
            e[0x38] = 0xc8;
            e[0x39] = 0xc8;
            e[0x3a] = 0xc8;
        }

        if (*(short*)(e + 0x10) != 1) return;

        e[4] = (byte)(CrtRand() % 4 + 8);
        if ((Mem.s_StateBank0_005e96e0[0x22] & 2) == 0) return;    // Ghidra: DAT_005e9702

        BrightenEntityTint_00404DE0(e, -8, -6, -8);                 // Ghidra: FUN_00404de0
    }

    /// <summary>
    /// Ghidra: <c>0x00437bb0</c> — sous-zones 4 et 9, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 62 instructions. <b>L'anneau qui s'elargit</b> : modele <c>+0x14</c>, ne
    /// sans echelle, 20 unites plus haut, teinte blanche ; il grandit de <c>0x190</c> par image
    /// (modele 1) ou de <c>0x320</c> (tout autre), et palit de 6 par canal jusqu'a mourir.
    /// <para>
    /// Son <c>+0x0a</c> : 2 pour le modele 1 ; sinon <c>0x0d</c> en sous-zone 16, <c>0x0f</c>
    /// ailleurs — puis 2 quand meme en sous-zone 4. La grotte n'a pas de sous-zone 16 : la branche
    /// <c>0x0d</c> est morte ici, comme celle des etincelles du lot 2.
    /// </para>
    /// </summary>
    /// <param name="fourMeansTwo">Le rattrapage de la sous-zone 4 ; sa copie des salles 11-12,
    /// <c>0x0043DF60</c> (lot 6), ne l'a pas — c'est leur seul ecart.</param>
    private static void EntityHandler_00437BB0(byte* e, bool fourMeansTwo = true)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[4] = e[0x14];

            byte subArea = (byte)s_Unknown_005de52c;
            if (*(short*)(e + 0x14) == 1) e[0x0a] = 2;
            else
            {
                e[0x0a] = subArea == 0x10 ? (byte)0x0d : (byte)0x0f;
                if (fourMeansTwo && subArea == 4) e[0x0a] = 2;
            }

            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x34) = 0;
            *(int*)(e + 0x20) += 0x14000;
            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;
        }

        if (*(short*)(e + 0x10) != 1) return;

        short grow = (short)(e[4] == 1 ? 0x190 : 0x320);
        *(short*)(e + 0x28) += grow;
        *(short*)(e + 0x2a) += grow;
        *(short*)(e + 0x2c) += grow;

        // Ghidra: les trois canaux lus en seize bits, moins 6 ; seul le rouge est teste.
        short r = (short)(e[0x38] - 6);
        short g = (short)(e[0x39] - 6);
        short b = (short)(e[0x3a] - 6);
        if (r < 0)
        {
            r = 0;
            g = 0;
            b = 0;
            *(uint*)e &= 0x7fffffff;
        }

        e[0x38] = (byte)r;
        e[0x39] = (byte)g;
        e[0x3a] = (byte)b;
    }

    /// <summary>
    /// Ghidra: <c>0x0043af70</c> — sous-zones 6 et 7, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 67 instructions. <b>La bouffee qui monte</b> : sur la planche de la braise
    /// (<c>0x005510a8</c>, quatre images), dispersee de ±25 unites, jusqu'a 29 plus haut, echelle de
    /// <c>0x400</c> a <c>0x7ff</c> ; elle monte de 10 unites par image et avance d'une image toutes
    /// les deux, pour mourir a la quatrieme — dans l'image meme ou <c>+0x40</c> atteint 4.
    /// </summary>
    private static void EntityHandler_0043AF70(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005510A8();            // Ghidra: &DAT_005510a8
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 1;

            short scale = (short)(CrtRand() % 0x400 + 0x400);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x1c) += (CrtRand() % 0x32 - 0x19) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0x32 - 0x19) << 0xc;

            int lift = CrtRand() % 0x1e;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 2;
            *(short*)(e + 0x14) = 2;
            *(int*)(e + 0x20) += lift << 0xc;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x14) -= 1;
        *(int*)(e + 0x20) += 0xa000;
        if (*(short*)(e + 0x14) > 0) return;

        byte frame = (byte)(e[0x40] + 1);
        *(short*)(e + 0x14) = *(short*)(e + 0x12);
        e[0x40] = frame;
        if (frame == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552710</c> — le script de <see cref="EntityHandler_00484CD0"/> : les
    /// voix <c>0x21</c> puis <c>0x24</c>. Borne par le <c>[1] = 0</c> du second ; il suit le script
    /// de l'emetteur (<c>0x005526e0</c>, lot 1) et precede un autre (<c>0x00552720</c>).
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552710 =
    [
        0x21, 0x01, 0x01, 0x00, 0x7f, 0x01, 0x00, 0x18,   // 0x00552710
        0x24, 0x00, 0x01, 0x00, 0x7f, 0x01, 0x00, 0x18,   // 0x00552718 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552710Storage;

    /// <summary>
    /// Ghidra: <c>0x00484cd0</c> — sous-zones 4 et 9. Absente de <c>Blaze.c</c> ; 70 instructions.
    /// <b>Le jet d'une image</b> : il se pose sur son lanceur (<c>+0x6c</c>, range en <c>+0x74</c>)
    /// au decalage <c>+0x44..+0x4c</c> — nie en X et Z, tourne par les angles du lanceur —, prend
    /// sa vitesse dans le cap du lanceur au tangage <c>+0x34</c> (portee 40), fait naitre le
    /// sous-type de l'octet 9 du bloc <c>+0xd4</c> de la zone (planche <c>0x00551bc8</c>, parametre
    /// 4), met en file le script <c>0x00552710</c> et libere son emplacement dans la meme image.
    /// </summary>
    private static void EntityHandler_00484CD0(byte* e)
    {
        int* offset = stackalloc int[3];
        byte* caster = *(byte**)(e + 0x6c);

        *(int*)(e + 0x44) = -*(int*)(e + 0x44);
        *(int*)(e + 0x4c) = -*(int*)(e + 0x4c);
        *(byte**)(e + 0x74) = caster;
        *(uint*)(e + 0x28) = 0x18001800;
        *(uint*)(e + 0x38) = 0xf0f0f0;

        RotateVectorByAngles_00536090((short*)(caster + 0x30), (int*)(e + 0x44), offset);   // Ghidra: FUN_00536090

        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + offset[0];
        *(int*)(e + 0x20) = *(int*)(caster + 0x20) + offset[1];
        *(int*)(e + 0x24) = *(int*)(caster + 0x24) + offset[2];

        SetVectorFromAngles_00532AD0(
            (int*)(e + 0x50), *(short*)(caster + 0x32), *(short*)(e + 0x34), 0x28);   // Ghidra: FUN_00532ad0

        *(short*)(e + 0x9a) = 0x3000;

        byte* region = *(byte**)(s_Unknown_005e97cc + 0xd4);
        byte child = (byte)((*(byte**)region)[9] + 0xc0);
        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), Descriptors00551BC8(), child, 0, 4);   // Ghidra: FUN_0047ac40

        Aud.QueueDelayedSoundScript(
            NativeCopy(s_DelayedSoundScript_00552710, ref s_DelayedSoundScript00552710Storage),
            (int*)(e + 0x1c));                                       // Ghidra: FUN_0053dbe0

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005526c0</c> — le script de <see cref="EntityHandler_00484760"/>, les memes
    /// seize octets que <c>0x00552710</c> (voix <c>0x21</c> puis <c>0x24</c>). Il suit la fin du
    /// script du Labyrinthe (<c>0x005526b8</c>) et precede celui de l'emetteur (<c>0x005526e0</c>).
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_005526C0 =
    [
        0x21, 0x01, 0x01, 0x00, 0x7f, 0x01, 0x00, 0x18,   // 0x005526c0
        0x24, 0x00, 0x01, 0x00, 0x7f, 0x01, 0x00, 0x18,   // 0x005526c8 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript005526C0Storage;

    /// <summary>
    /// Ghidra: <c>0x00484760</c> — sous-zones 4 et 9. Absente de <c>Blaze.c</c> ; 82 instructions.
    /// <see cref="EntityHandler_00484CD0"/> qui reste : meme naissance sur son lanceur (en se
    /// rangeant elle-meme en <c>+0x70</c>, <c>+0x42</c> a zero), l'enfant de l'octet 8 du bloc
    /// <c>+0xd4</c> au parametre 3 et le script <c>0x005526c0</c> ; puis elle attend la fin de son
    /// animation pour liberer l'emplacement.
    /// </summary>
    private static void EntityHandler_00484760(byte* e)
    {
        if (*(short*)(e + 0x10) != 0)
        {
            if ((*(uint*)e & 0x2000000) != 0) *(uint*)e = 0;
            return;
        }

        int* offset = stackalloc int[3];
        byte* caster = *(byte**)(e + 0x6c);

        *(int*)(e + 0x44) = -*(int*)(e + 0x44);
        *(int*)(e + 0x4c) = -*(int*)(e + 0x4c);
        *(byte**)(e + 0x74) = caster;
        *(byte**)(e + 0x70) = e;
        *(uint*)(e + 0x28) = 0x18001800;
        *(uint*)(e + 0x38) = 0xf0f0f0;

        RotateVectorByAngles_00536090((short*)(caster + 0x30), (int*)(e + 0x44), offset);   // Ghidra: FUN_00536090

        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + offset[0];
        *(int*)(e + 0x20) = *(int*)(caster + 0x20) + offset[1];
        *(int*)(e + 0x24) = *(int*)(caster + 0x24) + offset[2];

        SetVectorFromAngles_00532AD0(
            (int*)(e + 0x50), *(short*)(caster + 0x32), *(short*)(e + 0x34), 0x28);   // Ghidra: FUN_00532ad0

        *(short*)(e + 0x9a) = 0x3000;
        e[0x42] = 0;

        byte* region = *(byte**)(s_Unknown_005e97cc + 0xd4);
        byte child = (byte)((*(byte**)region)[8] + 0xc0);
        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), Descriptors00551BC8(), child, 0, 3);   // Ghidra: FUN_0047ac40

        Aud.QueueDelayedSoundScript(
            NativeCopy(s_DelayedSoundScript_005526C0, ref s_DelayedSoundScript005526C0Storage),
            (int*)(e + 0x1c));                                       // Ghidra: FUN_0053dbe0

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00485220</c> — sous-zones 4 et 9. Absente de <c>Blaze.c</c> ; 84 instructions.
    /// <b>L'emetteur a vingt images</b> : ne sur son lanceur (decalages nies, lanceur range en
    /// <c>+0x70</c> et <c>+0x74</c>, cap tourne de <c>+0x9a</c>, echelles <c>0x800 / 0x6000 /
    /// 0x6000</c>), il se replace une image sur deux sur le lanceur, pose sa vitesse dans le cap du
    /// lanceur decale de <c>+0x9a</c> (tangage <c>+0x34</c>, portee 100), tire son modele de 8 a 11
    /// et fait naitre l'incantation de l'octet <c>0x11</c> du bloc <c>+0xd0</c> — cap tourne d'un
    /// quart de tour le temps de la naissance. A la vingtieme image l'emplacement est vide.
    /// </summary>
    private static void EntityHandler_00485220(byte* e)
    {
        byte* caster = *(byte**)(e + 0x6c);
        short age = *(short*)(e + 0x10);

        if (age == 0)
        {
            *(int*)(e + 0x44) = -*(int*)(e + 0x44);
            *(short*)(e + 0x32) += *(short*)(e + 0x9a);
            *(byte**)(e + 0x74) = caster;
            *(byte**)(e + 0x70) = caster;
            *(short*)(e + 0x36) = 4;
            *(uint*)(e + 0x28) = 0x60000800;
            *(short*)(e + 0x2c) = 0x6000;
            *(uint*)(e + 0x38) = 0xf0f0f0;
            *(int*)(e + 0x4c) = -*(int*)(e + 0x4c);
        }

        if ((age & 1) != 0)
        {
            int* offset = stackalloc int[3];
            RotateVectorByAngles_00536090((short*)(caster + 0x30), (int*)(e + 0x44), offset);   // Ghidra: FUN_00536090

            *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + offset[0];
            *(int*)(e + 0x20) = *(int*)(caster + 0x20) + offset[1];
            *(int*)(e + 0x24) = *(int*)(caster + 0x24) + offset[2];

            SetVectorFromAngles_00532AD0((int*)(e + 0x50),
                *(short*)(e + 0x9a) + *(short*)(caster + 0x32), *(short*)(e + 0x34), 0x64);   // Ghidra: FUN_00532ad0

            short course = *(short*)(e + 0x32);
            *(short*)(e + 0x32) = (short)(course + 0x400);
            e[4] = (byte)((CrtRand() & 3) + 8);

            byte* directory = *(byte**)(s_Unknown_005e97cc + 0xd0);
            SpawnCastEntity_0047AAE0(e, (byte)((*(byte**)directory)[0x11] + 0xc0), 0, 3);   // Ghidra: FUN_0047aae0
            *(short*)(e + 0x32) = course;
        }

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 0x14) *(uint*)e = 0;
    }

    /// <summary>Ghidra: <c>&amp;DAT_00550f60</c> — une tuile <c>0x1f x 0x1f</c> page 2, entre le
    /// grain des sous-zones 4-9 (<c>0x00550f40</c>, dont le dernier enregistrement est
    /// <c>0x00550f58</c>) et les etincelles (<c>0x00550f68</c>). Marge 1 : le corps pose
    /// <c>+0x40 = 0</c> et n'y touche plus.</summary>
    private static readonly byte[] s_Descriptors_00550F60 =
    [
        0x60, 0x48, 0x1f, 0x1f, 0x01, 0x02, 0x10, 0x00,   // 0x00550f60  <- 0x00438260
    ];

    private static byte* s_Descriptors00550F60Storage;

    private static byte* Descriptors00550F60() => NativeCopy(s_Descriptors_00550F60, ref s_Descriptors00550F60Storage);

    /// <summary>
    /// Ghidra: <c>0x00438260</c> — sous-zones 4 et 9, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 84 instructions. <b>La fumee qui monte vite</b> : dispersee de ±250 unites,
    /// echelle de <c>0x3e8</c> a <c>0xbcf</c>, teinte <c>0xbe / 0x96 / 0x96</c> ; elle monte de 8 a
    /// 17 unites par image et, passe 15 a 24 images, s'assombrit de 8 et retrecit de 100 par image
    /// jusqu'a mourir.
    /// </summary>
    private static void EntityHandler_00438260(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00550F60();            // Ghidra: &DAT_00550f60
            e[0x40] = 0;

            short scale = (short)(CrtRand() % 0x7e8 + 0x3e8);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 8);
            *(int*)(e + 0x1c) += (CrtRand() % 0x1f4 - 0xfa) << 0xc;

            int dz = CrtRand() % 0x1f4;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x14) = 0;
            *(int*)(e + 0x24) += (dz - 0xfa) << 0xc;

            int life = CrtRand() % 10;
            e[0x38] = 0xbe;
            e[0x39] = 0x96;
            e[0x3a] = 0x96;
            *(short*)(e + 0x9a) = (short)(life + 0x0f);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) += *(short*)(e + 0x12) << 0xc;
        *(short*)(e + 0x14) += 1;
        if (*(short*)(e + 0x14) <= *(short*)(e + 0x9a)) return;

        BrightenEntityTint_00404DE0(e, -8, -8, -8);                 // Ghidra: FUN_00404de0
        *(short*)(e + 0x28) -= 100;
        *(short*)(e + 0x2a) -= 100;
        if (*(short*)(e + 0x28) >= 0) return;

        *(short*)(e + 0x28) = 0;
        *(short*)(e + 0x2a) = 0;
        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0043aa90</c> — sous-zones 6 et 7, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 88 instructions. <b>Le semeur d'eboulis</b>, une image : il se pose a ±700
    /// unites de la camera en X et Z, 2 000 unites au-dessus d'elle, avec un sol par defaut a
    /// -1 700 unites, se place dans le decor (<see cref="TryPlaceEntity"/>, masque <c>0x8000</c>,
    /// puis <see cref="FinalizeEntityPlacement2"/> ; le sol trouve est borne a -1 700), redescend a
    /// 1 000 unites au-dessus de la camera, tire ses echelles, et fait naitre deux fois
    /// l'incantation <c>0xc3</c> — son <c>+0x14</c> prete a <c>0x39</c> puis <c>0x1c</c>. Puis il
    /// meurt.
    /// </summary>
    private static void EntityHandler_0043AA90(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int* camera = Mem.Camera(0x005de068);

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            *(int*)(e + 0x1c) = ((CrtRand() % 0x578 - 0x2bc) << 0xc) + camera[0];
            *(int*)(e + 0x24) = ((CrtRand() % 0x578 - 0x2bc) << 0xc) + camera[2];
            *(int*)(e + 0x64) = unchecked((int)0xff95c000);
            *(int*)(e + 0x20) = camera[1] + 0x7d0000;
            *(int*)(e + 0x60) = unchecked((int)0xff95c000);
            *(short*)(e + 0x82) = unchecked((short)0xa000);
            *(short*)(e + 0x86) = unchecked((short)0xa000);
            *(short*)(e + 0x8a) = unchecked((short)0xa000);
            *(short*)(e + 0x8e) = unchecked((short)0xa000);

            TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);   // Ghidra: FUN_005330d0
            FinalizeEntityPlacement2((uint*)e);                      // Ghidra: FUN_00533940

            if ((*(int*)(e + 0x64) & unchecked((int)0xfffff000)) < unchecked((int)0xff95c000))
                *(int*)(e + 0x64) = unchecked((int)0xff95c000);

            *(int*)(e + 0x20) = camera[1] + 0x3e8000;

            int r = CrtRand() % 0x400;
            *(short*)(e + 0x28) = (short)(r + 0x5dc);
            *(short*)(e + 0x2a) = (short)(r + 0x9c4);
            *(short*)(e + 0x2c) = (short)(r + 0x9c4);
        }

        if (*(short*)(e + 0x10) != 1 || *(short*)(e + 0x16) != 0) return;

        *(short*)(e + 0x14) = 0x39;
        SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);                   // Ghidra: FUN_0047aae0
        *(short*)(e + 0x14) = 0x1c;
        SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);

        *(uint*)e &= 0x7fffffff;
        *(short*)(e + 0x10) = 2;
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0043a730</c> — sous-zones 6 et 7, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 90 instructions. Le cousin de <see cref="EntityHandler_0043AA90"/>, sur place :
    /// dispersion de ±100 unites, echelles tirees, une vitesse horizontale de ±20 et une portee
    /// <c>+0x5c</c> de 80 a 178 ; il seme <c>0xf9</c> a sa position, puis fait naitre deux fois
    /// l'incantation <c>0xc5</c> (<c>+0x14</c> prete a <c>0x1c</c> puis <c>0x39</c>) et meurt.
    /// </summary>
    private static void EntityHandler_0043A730(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            *(int*)(e + 0x1c) += (CrtRand() % 0xc8 - 0x64) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0xc8 - 0x64) << 0xc;

            int r = CrtRand() % 0x400;
            *(short*)(e + 0x28) = (short)(r + 0x5dc);
            *(short*)(e + 0x2a) = (short)(r + 0x9c4);
            *(short*)(e + 0x2c) = (short)(r + 0x9c4);

            *(int*)(e + 0x50) = CrtRand() % 0x28 - 0x14;
            *(int*)(e + 0x58) = CrtRand() % 0x28 - 0x14;
            *(int*)(e + 0x5c) = (CrtRand() % 0x32) * 2 + 0x50;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf9, 0, -1);   // Ghidra: FUN_0047ac40
        }

        if (*(short*)(e + 0x10) != 1 || *(short*)(e + 0x16) != 0) return;

        *(short*)(e + 0x14) = 0x1c;
        SpawnCastEntity_0047AAE0(e, 0xc5, 0, -1);                   // Ghidra: FUN_0047aae0
        *(short*)(e + 0x14) = 0x39;
        SpawnCastEntity_0047AAE0(e, 0xc5, 0, -1);

        *(uint*)e &= 0x7fffffff;
        *(short*)(e + 0x10) = 2;
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00438710</c> — sous-zones 4 et 9, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 91 instructions. <b>La flamme qu'un drapeau eteint</b> : modele 8 a 11 tire
    /// a chaque image, tournee de <c>0x600</c> si son <c>+0x14</c> vaut <c>0x109</c> (de
    /// <c>0x200</c> sinon), teinte <c>0xfa</c>. Tant que le bit de rang <c>+0x14</c> de la banque 0
    /// est eteint, elle crepite (voix <c>0x33</c> toutes les 39 images, a partir d'un compte tire).
    /// Pose, elle s'eteint (-10, -10, -7 par image) et meurt quand son bleu est nul — sur-le-champ
    /// si elle a moins de dix images d'age.
    /// </summary>
    private static void EntityHandler_00438710(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[4] = 8;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x34) = 0;
            *(short*)(e + 0x32) = (short)(*(short*)(e + 0x14) == 0x109 ? 0x600 : 0x200);
            *(short*)(e + 0x28) = 0x1e14;
            *(short*)(e + 0x2c) = 0x0fa0;
            *(short*)(e + 0x2a) = 0x0fa0;
            e[0x38] = 0xfa;
            e[0x39] = 0xfa;
            e[0x3a] = 0xfa;
            int r = CrtRand();
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x98) = (short)(r % 0x14);
        }

        *(short*)(e + 0x9a) += 1;
        if (*(short*)(e + 0x10) != 1) return;

        e[4] = (byte)(CrtRand() % 4 + 8);

        int flag = *(short*)(e + 0x14);
        short cell = (short)(flag / 8);
        if ((Mem.s_StateBank0_005e96e0[cell] & (byte)(1 << ((flag - cell * 8) & 0x1f))) != 0)
        {
            BrightenEntityTint_00404DE0(e, -10, -10, -7);           // Ghidra: FUN_00404de0
            if (e[0x3a] == 0) *(uint*)e &= 0x7fffffff;
            if (*(short*)(e + 0x9a) < 0x0a) *(uint*)e &= 0x7fffffff;
            return;
        }

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) != 0x27) return;

        *(short*)(e + 0x98) = 0;
        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x33), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
    }

    /// <summary>Ghidra: <c>&amp;DAT_00551008</c> — quatre tuiles <c>0x20 x 0x20</c> page 2, bornees
    /// en bas par une tuile de page 3 (<c>0x00551000</c>), en haut par une <c>0x18 x 0x18</c>
    /// (<c>0x00551028</c>). L'image est <c>3 - (+0x16)</c>, <c>+0x16</c> allant de 3 a 0 : marge
    /// exacte.</summary>
    private static readonly byte[] s_Descriptors_00551008 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551008  <- 0x0043B1D0
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551010
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551018
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551020
    ];

    private static byte* s_Descriptors00551008Storage;

    private static byte* Descriptors00551008() => NativeCopy(s_Descriptors_00551008, ref s_Descriptors00551008Storage);

    /// <summary>
    /// Ghidra: <c>0x0043b1d0</c> — sous-zones 6 et 7, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 91 instructions. <b>La gerbe qui s'eleve en arc</b> : planche
    /// <c>0x00551008</c>, image <c>3 - (+0x16)</c>, echelle de <c>0xc00</c> a <c>0xfff</c>, teinte
    /// <c>0xfa / 0x78 / 0x78</c>, rayon <c>+0x5c</c> de 80 a 129. Au bout de deux images, la voix
    /// brute <c>0x1e</c> si <c>+0x16</c> vaut 3 ; puis elle seme <c>0x32</c> pres de la camera tant
    /// que <c>+0x16</c>, decremente une fois, reste positif, et decrit un demi-cercle vertical
    /// autour de son point de naissance (X par le cosinus, Y par le sinus, <c>0x64</c> par image)
    /// avant de mourir.
    /// </summary>
    private static void EntityHandler_0043B1D0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00551008();            // Ghidra: &DAT_00551008
            e[0x40] = (byte)(3 - e[0x16]);
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 2;

            int r = CrtRand() % 0x400;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(short*)(e + 0x28) = (short)(r + 0xc00);
            *(short*)(e + 0x2a) = (short)(r + 0xc00);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            int reach = CrtRand() % 0x32;
            e[0x38] = 0xfa;
            e[0x39] = 0x78;
            e[0x3a] = 0x78;
            *(int*)(e + 0x5c) = reach + 0x50;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) < 0)
            {
                if (*(short*)(e + 0x16) == 3)
                {
                    TryPlaySoundNearEntity((uint*)e, 0x1e, 0x200, 0x1800, 0, 0);   // Ghidra: FUN_00468080
                }

                *(short*)(e + 0x16) -= 1;
                *(short*)(e + 0x10) = 2;

                if (*(short*)(e + 0x16) < 0) *(short*)(e + 0x16) = 0;
                else SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);   // Ghidra: FUN_00468110
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        short angle = *(short*)(e + 0x9a);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetCosTableEntry(angle) * *(int*)(e + 0x5c) + *(int*)(e + 0x50);
        int y = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x9a)) * *(int*)(e + 0x5c) + *(int*)(e + 0x54);
        *(short*)(e + 0x9a) += 0x64;
        *(int*)(e + 0x20) = y;

        if (*(short*)(e + 0x9a) > 0x800) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0043a600</c> — sous-zones 6 et 7, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 93 instructions. <b>La cendre qui monte</b> : planche de la braise
    /// (<c>0x005510a8</c>), dispersee de ±75 unites et ±20 en hauteur, echelle de <c>0x800</c> a
    /// <c>0xfff</c>, teinte sombre <c>0x40</c>. Elle monte de 0 a 3 unites par image ; toutes les 3
    /// a 8 images elle s'assombrit de 10 et avance d'une image, pour mourir a la quatrieme.
    /// </summary>
    private static void EntityHandler_0043A600(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors005510A8();            // Ghidra: &DAT_005510a8
            e[0x40] = 0;
            e[0x0a] = 4;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0x96 - 0x4b) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0x96 - 0x4b) << 0xc;
            *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) << 0xc;
            *(short*)(e + 0x12) = (short)(CrtRand() % 6 + 3);
            *(int*)(e + 0x5c) = CrtRand() % 4;
            *(short*)(e + 0x14) = *(short*)(e + 0x12);

            short scale = (short)(CrtRand() % 0x800 + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
            e[0x38] = 0x40;
            e[0x39] = 0x40;
            e[0x3a] = 0x40;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) += *(int*)(e + 0x5c) << 0xc;
        *(short*)(e + 0x14) -= 1;
        if (*(short*)(e + 0x14) != 0) return;

        BrightenEntityTint_00404DE0(e, -10, -10, -10);              // Ghidra: FUN_00404de0
        *(short*)(e + 0x14) = *(short*)(e + 0x12);

        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if (frame == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00434940</c> et <c>0x00434a90</c> — sous-zones 5, 8 et 10, table de
    /// debordement. Presentes dans <c>Blaze.c</c> ; 95 instructions chacune. <b>Le vol qui traverse
    /// la salle</b>, deux fois, en miroir : modele 0, echelles <c>0xc8 / 0xc8</c> et une troisieme
    /// tiree, teinte bleutee. Il nait a une abscisse fixe, a une hauteur de 0 a 179 unites et une
    /// profondeur tiree, puis file en X a 20 a 69 unites par image — dans un sens ou l'autre selon
    /// un bit de l'octet <c>0x22</c> de la banque 0 — et meurt passe la borne de son sens, rangee en
    /// <c>+0x12</c>.
    /// <code>
    ///   corps        X de depart   Z          bit    bit pose : cap, borne   bit eteint : cap, borne
    ///   0x00434940   -100          -3000-r    0x04   0x400, -1100            0xc00, +800
    ///   0x00434A90   +900          -1400-r    0x08   0x400, +1800            0xc00, -200
    /// </code>
    /// Le sens suit le signe de la borne : vers les X negatifs pour une borne negative.
    /// </summary>
    private static void TickCrossingFlight_00434940(
        byte* e, int startX, int depthBase, byte bit, short setBound, short unsetBound, short lowBound, short highBound)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[4] = 0;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x28) = 0xc8;
            *(short*)(e + 0x2a) = 0xc8;

            int r = CrtRand() % 0xbb8;
            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xfa;
            *(short*)(e + 0x2c) = (short)(r + 0xfa0);
            *(int*)(e + 0x1c) = startX;

            bool set = (Mem.s_StateBank0_005e96e0[0x22] & bit) != 0;   // Ghidra: DAT_005e9702
            short bound = set ? setBound : unsetBound;

            *(int*)(e + 0x20) = (CrtRand() % 0xb4) << 0xc;
            *(int*)(e + 0x24) = (depthBase - CrtRand() % 0x258) << 0xc;

            int speed = CrtRand() % 0x32;
            *(short*)(e + 0x32) = (short)(set ? 0x400 : 0xc00);
            *(short*)(e + 0x34) = 0x800;
            *(short*)(e + 0x12) = bound;
            *(int*)(e + 0x50) = bound < 0 ? (-0x14 - speed) << 0xc : (speed + 0x14) << 0xc;

            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int x = *(int*)(e + 0x1c) + *(int*)(e + 0x50);
        *(int*)(e + 0x1c) = x;

        short limit = *(short*)(e + 0x12);
        if (limit == lowBound && x < lowBound << 0xc) *(uint*)e &= 0x7fffffff;
        if (limit == highBound && x > highBound << 0xc) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552720</c> — le script de l'impact de <see cref="EntityHandler_00484E80"/> :
    /// cinq fois la voix <c>0x0e</c>, a trois images d'intervalle, montant. Borne par le
    /// <c>[1] = 0</c> du cinquieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552720 =
    [
        0x0e, 0x03, 0x01, 0x00, 0x80, 0x00, 0x00, 0x18,   // 0x00552720
        0x0e, 0x03, 0x01, 0x00, 0xaa, 0x00, 0x00, 0x18,   // 0x00552728
        0x0e, 0x03, 0x01, 0x00, 0xca, 0x00, 0x00, 0x18,   // 0x00552730
        0x0e, 0x03, 0x01, 0x00, 0xf4, 0x00, 0x00, 0x18,   // 0x00552738
        0x0e, 0x00, 0x01, 0x00, 0x2a, 0x01, 0x00, 0x18,   // 0x00552740 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552720Storage;

    /// <summary>
    /// Ghidra: <c>0x00484e80</c> — sous-zones 4 et 9. Absente de <c>Blaze.c</c> ; 93 instructions.
    /// <b>La pierre lancee</b> : elle file sur sa vitesse (freinee d'un huitieme plus un 4096e par
    /// image en X et Z, tiree vers le bas par <c>+0x9a</c> en Y) et se place dans le decor en gardant
    /// sa hauteur. Un contact <c>0xa000</c> la fait disparaitre si elle porte <c>0x20000000</c> ou
    /// est tombee sous -20 000 unites ; un mur (<c>0x802</c>) ou le sol l'arretent : posee au sol,
    /// ecrasee, eteinte, modele <c>0x23</c>, elle fait naitre l'incantation de l'octet <c>0x0f</c> du
    /// bloc <c>+0xd0</c> (parametre 4), met en file le script <c>0x00552720</c> et libere son
    /// emplacement.
    /// </summary>
    private static void EntityHandler_00484E80(byte* e)
    {
        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        int y = *(int*)(e + 0x20) + vy;
        *(int*)(e + 0x20) = y;
        *(int*)(e + 0x24) += vz;

        int pull = *(short*)(e + 0x9a);
        *(int*)(e + 0x50) = vx - (vx >> 3) - (vx >> 0xc);
        *(int*)(e + 0x54) = (pull >> 8) - (vy >> 0xc) - pull + vy;
        *(int*)(e + 0x58) = vz - (vz >> 3) - (vz >> 0xc);

        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940
        uint contact = *(uint*)(e + 0x90);
        *(int*)(e + 0x20) = y;

        bool grounded = (contact & 0xa000) == 0
                        && ((contact & 0x802) != 0 || y <= *(int*)(e + 0x60));

        if (!grounded)
        {
            // Ghidra: LAB_00484f64
            if ((*(uint*)e & 0x20000000) != 0 || y < unchecked((int)0xfb1e0000)) *(uint*)e = 0;
            return;
        }

        *(int*)(e + 0x20) = *(int*)(e + 0x60);
        *(int*)(e + 0x28) = 0;
        *(short*)(e + 0x2c) = 0;
        *(uint*)(e + 0x38) = 0;
        *(short*)(e + 0x2e) = -0x40;
        e[4] = 0x23;

        byte* directory = *(byte**)(s_Unknown_005e97cc + 0xd0);
        SpawnCastEntity_0047AAE0(e, (byte)((*(byte**)directory)[0x0f] + 0xc0), 0, 4);   // Ghidra: FUN_0047aae0

        Aud.QueueDelayedSoundScript(
            NativeCopy(s_DelayedSoundScript_00552720, ref s_DelayedSoundScript00552720Storage),
            (int*)(e + 0x1c));                                       // Ghidra: FUN_0053dbe0

        *(uint*)e = 0;
    }

    // =====================================================================
    // LOT 6 (2026-09-19) — une premiere moitie des « B » des salles 11 a 15
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00551330</c> — trois tuiles <c>0x0f x 0x0f</c> page 2, bornees
    /// en bas par une <c>0x10 x 0x10</c> (<c>0x00551328</c>), en haut par une <c>0x20 x 0x20</c> de
    /// page 3 (<c>0x00551348</c>). Le corps boucle son image de 0 a 2 : marge exacte.</summary>
    private static readonly byte[] s_Descriptors_00551330 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551330  <- 0x0043C880
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551338
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551340
    ];

    private static byte* s_Descriptors00551330Storage;

    private static byte* Descriptors00551330() => NativeCopy(s_Descriptors_00551330, ref s_Descriptors00551330Storage);

    /// <summary>
    /// Ghidra: <c>0x0043c880</c> — sous-zones 11 et 12, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 33 instructions. <b>L'etincelle qui papillote</b> : planche
    /// <c>0x00551330</c>, image de depart tiree de 0 a 2 puis bouclee, echelle <c>0xfa0</c> ; elle vit
    /// onze images.
    /// </summary>
    private static void EntityHandler_0043C880(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551330();            // Ghidra: &DAT_00551330
            int r = CrtRand();
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0x0fa0;
            *(short*)(e + 0x2a) = 0x0fa0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x0b;
            e[0x40] = (byte)(r % 3);
        }

        if (*(short*)(e + 0x10) != 1) return;

        byte frame = (byte)(e[0x40] + 1);
        *(short*)(e + 0x12) -= 1;
        e[0x40] = frame;
        if (frame >= 3) e[0x40] = 0;

        if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00441450</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 33 instructions. <b>Le tourbillon</b> : modele <c>DAT_005edec4 + 4</c>, ne
    /// sans echelle ; il tourne de <c>0xc8</c> par image et grandit de <c>0x1f4</c> pendant
    /// <c>+0x14</c> images, puis tourne sans grandir. Il vit 500 images.
    /// </summary>
    private static void EntityHandler_00441450(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x0a] = 4;
            e[0x0b] = 0;
            e[4] = (byte)(s_Unknown_005edec4 + 4);                 // Ghidra: DAT_005edec4 + 4
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) += 0xc8;
            *(short*)(e + 0x28) += 0x1f4;
            *(short*)(e + 0x2c) += 0x1f4;
            *(short*)(e + 0x2a) += 0x1f4;
            *(short*)(e + 0x14) -= 1;
            if (*(short*)(e + 0x14) == 0) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2) *(short*)(e + 0x32) += 0xc8;

        *(short*)(e + 0x16) += 1;
        if (*(short*)(e + 0x16) >= 0x1f4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552850</c> — le script de <see cref="EntityHandler_00486D60"/> : les voix
    /// <c>0x30</c>, <c>0x0f</c>, <c>0x0e</c>. Borne par le <c>[1] = 0</c> du troisieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552850 =
    [
        0x30, 0x01, 0x01, 0x00, 0x53, 0x06, 0x00, 0x18,   // 0x00552850
        0x0f, 0x01, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552858
        0x0e, 0x00, 0x01, 0x00, 0xb5, 0x00, 0x00, 0x18,   // 0x00552860 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552850Storage;

    /// <summary>
    /// Ghidra: <c>0x00486d60</c> — sous-zones 13 a 15. Absente de <c>Blaze.c</c> ; 38 instructions.
    /// Deux roles selon <c>+0x16</c>, apres un quart de tour a la naissance :
    /// <list type="bullet">
    /// <item>0 — <b>l'eclosion</b> : <c>+0x28</c> rejoint <c>0x1800</c> d'un quart par image ;
    /// passe <c>0x17c0</c>, elle pose le bit <c>0x2000000</c> de l'entite rangee en
    /// <c>+0x70</c>, met en file le script <c>0x00552850</c> et libere son emplacement ;</item>
    /// <item>1 — elle palit de <c>0x101010</c> par image (double mot) et libere son emplacement a
    /// teinte nulle.</item>
    /// </list>
    /// </summary>
    private static void EntityHandler_00486D60(byte* e)
    {
        byte* owner = *(byte**)(e + 0x70);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x32) += 0x400;
            *(short*)(e + 0x10) = 1;
        }

        short role = *(short*)(e + 0x16);

        if (role == 1)
        {
            uint tint = *(uint*)(e + 0x38) + 0xffefeff0;
            *(uint*)(e + 0x38) = tint;
            if (tint == 0) *(uint*)e = 0;
            return;
        }

        if (role != 0) return;

        int scale = *(short*)(e + 0x28);
        scale += (0x1800 - scale) >> 2;
        *(short*)(e + 0x28) = (short)scale;
        if (scale <= 0x17c0) return;

        *(uint*)owner |= 0x2000000;
        Aud.QueueDelayedSoundScript(
            NativeCopy(s_DelayedSoundScript_00552850, ref s_DelayedSoundScript00552850Storage),
            (int*)(e + 0x1c));                                       // Ghidra: FUN_0053dbe0
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004414e0</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 41 instructions. <b>Le rayon qui s'allonge</b> : modele
    /// <c>DAT_005edec4 + 3</c>, releve de 230 unites, echelle verticale <c>0x1000</c> ; ses deux
    /// autres echelles grandissent de 1 000 par image, et passe <c>0x3a98</c> il palit de 5 ; a
    /// <c>0x7530</c> il se fige et meurt.
    /// </summary>
    private static void EntityHandler_004414E0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x12) = 0;
            e[4] = (byte)(s_Unknown_005edec4 + 3);                 // Ghidra: DAT_005edec4 + 3
            e[0x0a] = 8;
            *(short*)(e + 0x2a) = 0x1000;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) += 0xe6000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x28) += 0x3e8;
        *(short*)(e + 0x2c) += 0x3e8;
        if (*(short*)(e + 0x28) >= 0x3a98) BrightenEntityTint_00404DE0(e, -5, -5, -5);   // Ghidra: FUN_00404de0

        if (*(short*)(e + 0x28) < 0x7530) return;

        *(short*)(e + 0x28) = 0x7530;
        *(short*)(e + 0x2c) = 0x7530;
        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00486680</c> — sous-zones 13 a 15. Absente de <c>Blaze.c</c> ; 42 instructions.
    /// <b>L'eventail</b>, une image : lanceur range en <c>+0x74</c>, modele <c>0x38</c>, eteint, il
    /// fait naitre quatre fois l'incantation de l'octet 7 du bloc <c>+0xd0</c> (parametre 4), son
    /// cap pris a chaque fois dans un quart de tour qui avance de <c>0x100</c> — plus un tirage de 0
    /// a 63 —, puis libere son emplacement. Son cap n'est pas rendu : le dernier reste.
    /// </summary>
    private static void EntityHandler_00486680(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int course = *(short*)(e + 0x32);
            *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);
            e[4] = 0x38;
            *(uint*)(e + 0x38) = 0;
            *(short*)(e + 0x36) = 4;
            *(short*)(e + 0x2e) = -0x10;
            *(uint*)(e + 0x28) = 0x02001000;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x12) = 8;

            course -= 0x180;
            for (int i = 4; i != 0; i--, course += 0x100)
            {
                *(short*)(e + 0x32) = (short)((CrtRand() & 0x3f) + course + 0x3e0);
                byte* directory = *(byte**)(s_Unknown_005e97cc + 0xd0);
                SpawnCastEntity_0047AAE0(e, (byte)((*(byte**)directory)[7] + 0xc0), 0, 4);   // Ghidra: FUN_0047aae0
            }
        }

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004855f0</c> — sous-zones 13 a 15. Absente de <c>Blaze.c</c> ; 44 instructions.
    /// <b>Le jet depuis le porteur</b>, une image : releve a la hauteur de la bulle de son porteur
    /// (<see cref="StatusBubbleHeight_00538BC0"/>, bit bas efface), modele <c>0x2a</c>, sans echelle,
    /// vitesse de 16 dans son cap et nulle en Y (<see cref="SetVectorFromAngle_00532B10"/>), il fait
    /// naitre l'incantation de l'octet 1 du bloc <c>+0xd0</c> (1, 2) et libere son emplacement.
    /// </summary>
    private static void EntityHandler_004855F0(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        *(byte**)(e + 0x74) = carrier;

        int lift = (StatusBubbleHeight_00538BC0((uint*)carrier) & ~1) << 0xb;   // Ghidra: FUN_00538bc0
        *(int*)(e + 0x20) += lift;

        e[4] = 0x2a;
        *(short*)(e + 0x36) = 4;
        *(uint*)(e + 0x38) = 0xf0f0f0;
        *(int*)(e + 0x28) = 0;
        *(short*)(e + 0x2c) = 0;
        *(short*)(e + 0x9a) = 0;

        SetVectorFromAngle_00532B10((int*)(e + 0x50), *(short*)(e + 0x32), 0x10);   // Ghidra: FUN_00532b10
        *(int*)(e + 0x54) = 0;

        byte* directory = *(byte**)(s_Unknown_005e97cc + 0xd0);
        SpawnCastEntity_0047AAE0(e, (byte)((*(byte**)directory)[1] + 0xc0), 1, 2);   // Ghidra: FUN_0047aae0

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00441b80</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 50 instructions. <b>La vague sombre qui se referme</b> : modele
    /// <c>DAT_005edec4 + 1</c>, nee enorme (<c>0x7d00</c>) et noire, octet <c>+0x3b</c> a
    /// <c>0xc0</c> ; elle tourne de <c>0x12c</c> par image et retrecit de 200, s'eclairant de 2 passe
    /// <c>0x6d60</c> ; a zero, elle meurt.
    /// </summary>
    private static void EntityHandler_00441B80(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x0a] = 0x0f;
            e[0x0b] = 0;
            e[4] = (byte)(s_Unknown_005edec4 + 1);                 // Ghidra: DAT_005edec4 + 1
            *(short*)(e + 0x28) = 0x7d00;
            *(short*)(e + 0x2a) = 0x7d00;
            *(short*)(e + 0x2c) = 0x7d00;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            e[0x3b] = 0xc0;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) += 0x12c;
            *(short*)(e + 0x28) -= 200;
            *(short*)(e + 0x2a) -= 200;
            *(short*)(e + 0x2c) -= 200;

            if (*(short*)(e + 0x28) <= 0x6d60) BrightenEntityTint_00404DE0(e, 2, 2, 2);   // Ghidra: FUN_00404de0

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0;
                *(uint*)e &= 0x7fffffff;
            }
        }

        *(short*)(e + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x0043e030</c> — sous-zones 11 et 12, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 99 instructions. La braise de la foret (<see cref="TickRisingEmber"/>,
    /// famille A) sur la planche des etincelles (<c>0x00551220</c>), dont le <c>+0x0a</c> depend de
    /// la sous-zone : 9 en sous-zone 16, 6 ailleurs — donc toujours 6 dans la grotte. Le choix n'est
    /// lu qu'a la naissance, comme dans le corps.
    /// </summary>
    private static void EntityHandler_0043E030(byte* e)
        => TickRisingEmber(e, Descriptors00551220(), (byte)((byte)s_Unknown_005de52c == 0x10 ? 9 : 6), false);

    /// <summary>
    /// Ghidra: <c>0x00485760</c> — sous-zones 13 a 15. Absente de <c>Blaze.c</c> ; 63 instructions.
    /// <b>Le double jet</b>, une image : lanceur range en <c>+0x74</c>, modele <c>0x1e</c>, teinte
    /// <c>0x808080</c> ; il fait naitre deux fois l'incantation de l'octet 2 du bloc <c>+0xd0</c>
    /// (1, 2) dans son cap — d'abord a la vitesse 8 et aux echelles <c>0x3000 / 0x6000</c>, puis a
    /// la vitesse 32 et aux echelles <c>0x4000 / 0x4000</c> —, puis libere son emplacement.
    /// </summary>
    private static void EntityHandler_00485760(byte* e)
    {
        short course = *(short*)(e + 0x32);
        *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);
        e[4] = 0x1e;
        *(short*)(e + 0x2e) = -0x20;
        *(short*)(e + 0x36) = 4;
        *(uint*)(e + 0x38) = 0x808080;
        *(short*)(e + 0x2c) = 0x6000;
        *(short*)(e + 0x9a) = 0;

        byte* directory = *(byte**)(s_Unknown_005e97cc + 0xd0);

        *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(course) << 3;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = (int)Winsys3d.GetCosTableEntry(course) << 3;
        *(int*)(e + 0x28) = 0x3000;
        *(short*)(e + 0x12) = 0x6000;
        SpawnCastEntity_0047AAE0(e, (byte)((*(byte**)directory)[2] + 0xc0), 1, 2);   // Ghidra: FUN_0047aae0

        *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(course) << 5;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = (int)Winsys3d.GetCosTableEntry(course) << 5;
        *(int*)(e + 0x28) = 0x4000;
        *(short*)(e + 0x12) = 0x4000;
        directory = *(byte**)(s_Unknown_005e97cc + 0xd0);
        SpawnCastEntity_0047AAE0(e, (byte)((*(byte**)directory)[2] + 0xc0), 1, 2);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0043c380</c> — sous-zones 11 et 12, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 80 instructions. <b>La gerbe des vingt images</b> : lanceur range en
    /// <c>+0x74</c>. Pendant vingt images, a chaque image, elle se colle a 80 unites au-dessus du
    /// lanceur (<c>+0x70</c>, decalage <c>(0, 0x50000, 0)</c>), tire un cap dans un quart de tour et
    /// un tangage, et fait naitre l'incantation <c>0x70</c> (modele <c>0x3f</c>, echelles
    /// <c>0x1fff / 0 / 0x100</c>). Puis deux <c>0xdf</c>, <c>+0x9a</c> prete a +200 et -200, et
    /// trente images d'attente avant de mourir.
    /// </summary>
    private static void EntityHandler_0043C380(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0x14;
            *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x16) -= 1;
            if (*(short*)(e + 0x16) <= 0) *(short*)(e + 0x10) = 2;

            e[4] = 0x3f;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(byte**)(e + 0x70) = *(byte**)(e + 0x6c);
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0x50000;
            *(int*)(e + 0x4c) = 0;
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x400 + 0x400);

            int tilt = CrtRand() % 0x1000;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x34) = (short)tilt;
            *(short*)(e + 0x12) = 0xc8;
            *(short*)(e + 0x28) = 0x1fff;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0x100;
            SpawnCastEntity_0047AAE0(e, 0x70, 0, -1);               // Ghidra: FUN_0047aae0
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            *(short*)(e + 0x16) = 0x1e;
            *(short*)(e + 0x9a) = 0xc8;
            SpawnCastEntity_0047AAE0(e, 0xdf, 0, -1);
            *(short*)(e + 0x9a) = -0xc8;
            SpawnCastEntity_0047AAE0(e, 0xdf, 0, -1);
        }

        if (*(short*)(e + 0x10) != 3) return;

        *(short*)(e + 0x16) -= 1;
        if (*(short*)(e + 0x16) < 0) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LOT 6, SECONDE MOITIE (2026-09-19) — les quatorze derniers « B » des salles 13 a 15
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_005513b8</c> — une tuile <c>0x08 x 0x08</c> page 3 (celle de
    /// <c>0x0054fd58</c>), bornee par une <c>0x10 x 0x10</c> (<c>0x005513c0</c>). Marge 1 : le corps
    /// pose <c>+0x40 = 0</c> et n'y touche plus.</summary>
    private static readonly byte[] s_Descriptors_005513B8 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005513b8  <- 0x004421B0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005514f0</c> — une tuile <c>0x10 x 0x10</c> page 2, puis
    /// <c>&amp;DAT_005514f8</c>, une <c>0x1f x 0x1f</c> page 2 couche 5, chacune seule de sa forme ;
    /// relevees d'un tenant parce qu'elles se touchent (la suivante, <c>0x00551500</c>, est une
    /// <c>0x0f x 0x0f</c>). Marge 1 chacune : les corps posent <c>+0x40 = 0</c> et n'y touchent
    /// plus.</summary>
    private static readonly byte[] s_Descriptors_005514F0 =
    [
        0x00, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x005514f0  <- 0x00441040, 0x00440F00, 0x00441180
        0xe0, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x05, 0x00,   // 0x005514f8  <- 0x004406B0
    ];

    private static byte* s_Descriptors005513B8Storage;
    private static byte* s_Descriptors005514F0Storage;

    private static byte* Descriptors005513B8() => NativeCopy(s_Descriptors_005513B8, ref s_Descriptors005513B8Storage);
    private static byte* Descriptors005514F0() => NativeCopy(s_Descriptors_005514F0, ref s_Descriptors005514F0Storage);
    private static byte* Descriptors005514F8() => Descriptors005514F0() + 8;

    /// <summary>
    /// Ghidra: <c>0x004421b0</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 68 instructions. <b>Le grain qui tombe en spirale</b> : planche
    /// <c>0x005513b8</c>, echelle <c>0x1000</c> ; il tourne autour de son point de naissance sur un
    /// rayon de 2 000 qui perd 30 par image (<c>0x64</c> de phase par image), sa hauteur rejoignant
    /// celle de la naissance d'un seizieme ; a rayon negatif il meurt. Son <c>+0x9a</c> est tire et
    /// jamais lu.
    /// </summary>
    private static void EntityHandler_004421B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors005513B8();            // Ghidra: &DAT_005513b8
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 1;
            *(short*)(e + 0x14) = 0x7d0;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);
            int r = CrtRand() % 0x3e8;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            *(short*)(e + 0x9a) = (short)r;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short radius = *(short*)(e + 0x14);
        short phase = *(short*)(e + 0x12);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(phase) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = (int)Winsys3d.GetCosTableEntry(phase) * radius + *(int*)(e + 0x58);
        *(short*)(e + 0x12) += 0x64;

        int y = *(int*)(e + 0x20);
        *(int*)(e + 0x20) = y - ((y - *(int*)(e + 0x54)) >> 4);

        short next = (short)(radius - 0x1e);
        *(short*)(e + 0x14) = next;
        if (next < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x004406b0</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 73 instructions. <b>La fumee qui s'envole</b> : planche <c>0x005514f8</c>,
    /// dispersee de ±150 unites, posee 50 unites au-dessus de son sol, echelle de <c>0x800</c> a
    /// <c>0xbff</c>, teinte <c>0xb4</c> ; elle monte de 5 a 14 unites par image, de plus en plus vite,
    /// palit de 3 et meurt quand son rouge est nul.
    /// </summary>
    private static void EntityHandler_004406B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005514F8();            // Ghidra: &DAT_005514f8
            e[0x40] = 0;
            e[0x0a] = 0x0c;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 5);

            short scale = (short)(CrtRand() % 0x400 + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xb4;

            *(int*)(e + 0x1c) += (CrtRand() % 0x12c - 0x96) << 0xc;
            int dz = CrtRand() % 0x12c;
            *(short*)(e + 0x2e) = -0x3c;
            *(int*)(e + 0x24) += (dz - 0x96) << 0xc;
            *(int*)(e + 0x20) = *(int*)(e + 0x64) + 0x32000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        short rise = *(short*)(e + 0x12);
        *(int*)(e + 0x20) += rise << 0xc;
        *(short*)(e + 0x12) = (short)(rise + 1);

        BrightenEntityTint_00404DE0(e, -3, -3, -3);                 // Ghidra: FUN_00404de0
        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00486240</c> — sous-zones 13 a 15. Absente de <c>Blaze.c</c> ; 74 instructions.
    /// <b>Le satellite qui se resserre</b> : chaque image, son cap et son tangage tournent de
    /// <c>+0x14</c>, et il se place autour de son proprietaire (<c>+0x70</c>) a son decalage
    /// <c>+0x44..+0x4c</c> plus un rayon <c>+0x12</c> pris dans ces deux angles (moins un quart de
    /// tour chacun) ; le rayon perd un quart par image, <c>+0x2a</c> rejoint <c>-0x8000</c> d'un
    /// quart. Tant que le rayon vaut 16 ou plus il s'eclaire jusqu'a <c>0xf0f0f0</c> ; ensuite il
    /// palit et libere son emplacement a teinte nulle.
    /// </summary>
    private static void EntityHandler_00486240(byte* e)
    {
        int* orbit = stackalloc int[3];
        byte* owner = *(byte**)(e + 0x70);

        short spin = *(short*)(e + 0x14);
        *(short*)(e + 0x32) += spin;
        *(short*)(e + 0x34) += spin;

        SetVectorFromAngles_00532AD0(orbit,
            *(short*)(e + 0x32) - 0x400, *(short*)(e + 0x34) - 0x400, *(short*)(e + 0x12));   // Ghidra: FUN_00532ad0

        *(int*)(e + 0x1c) = *(int*)(e + 0x44) + *(int*)(owner + 0x1c) + orbit[0];
        *(int*)(e + 0x20) = *(int*)(e + 0x48) + *(int*)(owner + 0x20) + orbit[1];
        *(int*)(e + 0x24) = *(int*)(e + 0x4c) + *(int*)(owner + 0x24) + orbit[2];

        short squash = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)(squash + ((-0x8000 - squash) >> 2));

        short radius = *(short*)(e + 0x12);
        radius = (short)(radius - (radius >> 2));
        *(short*)(e + 0x12) = radius;

        uint tint = *(uint*)(e + 0x38);
        if (radius >= 0x10)
        {
            if (tint < 0xf0f0f0) *(uint*)(e + 0x38) = tint + 0x101010;
            return;
        }

        if (tint == 0) return;

        tint += 0xffefeff0;
        *(uint*)(e + 0x38) = tint;
        if (tint == 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00441570</c> (portee <c>% 800 + 400</c>, palit de 4) et <c>0x00441db0</c>
    /// (<c>% 400 + 200</c>, palit de 2) — sous-zones 13 a 15, table de debordement ; absentes de
    /// <c>Blaze.c</c>, 75 instructions, a ces immediates pres. <b>L'eclat qui s'epanouit</b> : modele
    /// <c>DAT_005edec4</c>, ne sans echelle, cap et tangage tires, deux vitesses de rotation de ±100 ;
    /// il grandit de sa portee par image en palissant, et meurt a <c>0x7530</c>.
    /// </summary>
    private static void TickBloomingShard_00441570(byte* e, int growSpan, int growBase, short fade)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x0a] = 3;
            e[4] = (byte)s_Unknown_005edec4;                       // Ghidra: DAT_005edec4
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);
            int pitch = CrtRand() % 0x1000;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x34) = (short)pitch;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0xc8 - 0x64);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0xc8 - 0x64);
            *(int*)(e + 0x5c) = CrtRand() % growSpan + growBase;
        }

        if (*(short*)(e + 0x10) != 1) return;

        BrightenEntityTint_00404DE0(e, fade, fade, fade);           // Ghidra: FUN_00404de0

        *(short*)(e + 0x34) += *(short*)(e + 0x14);
        short grow = (short)*(int*)(e + 0x5c);
        *(short*)(e + 0x28) += grow;
        *(short*)(e + 0x2a) += grow;
        *(short*)(e + 0x2c) += grow;
        *(short*)(e + 0x32) += *(short*)(e + 0x12);

        if (*(short*)(e + 0x28) < 0x7530) return;

        *(short*)(e + 0x28) = 0x7530;
        *(short*)(e + 0x2a) = 0x7530;
        *(short*)(e + 0x2c) = 0x7530;
        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00485940</c> — sous-zones 13 a 15. Absente de <c>Blaze.c</c> ; 75 instructions.
    /// <b>L'explosion qui secoue la salle</b>. A sa naissance : six incantations de l'octet 3 du bloc
    /// <c>+0xd0</c> (1, 1) en couronne — le cap avance d'un sixieme de tour, plus un tirage de 0 a
    /// 255 —, puis l'incantation <c>0x6c</c> (1, 2) a vitesse <c>0x6000</c> sur les trois axes,
    /// modele 3, et une secousse de camera de 10 (<c>0x005de0ac</c>). Chaque image, la secousse
    /// s'amortit en changeant de signe (<c>s = s/4 - s</c>) ; au bout de dix images l'emplacement
    /// est libere.
    /// </summary>
    private static void EntityHandler_00485940(byte* e)
    {
        int shake;

        if (*(short*)(e + 0x10) == 0)
        {
            int course = *(short*)(e + 0x32);
            *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);
            e[4] = 0x38;
            *(uint*)(e + 0x38) = 0;
            *(short*)(e + 0x36) = 4;
            *(short*)(e + 0x2e) = -0x10;
            *(uint*)(e + 0x28) = 0x04002000;
            *(short*)(e + 0x2c) = 0x2000;
            *(short*)(e + 0x12) = 2;

            for (int i = 6; i != 0; i--, course += 0x2aa)
            {
                *(short*)(e + 0x32) = (short)((CrtRand() & 0xff) + course + 0x380);
                byte* directory = *(byte**)(s_Unknown_005e97cc + 0xd0);
                SpawnCastEntity_0047AAE0(e, (byte)((*(byte**)directory)[3] + 0xc0), 1, 1);   // Ghidra: FUN_0047aae0
            }

            *(byte**)(e + 0x70) = null;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;
            *(int*)(e + 0x50) = 0x6000;
            *(int*)(e + 0x54) = 0x6000;
            *(int*)(e + 0x58) = 0x6000;
            *(uint*)(e + 0x38) = 0x808080;
            *(int*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            e[4] = 3;
            SpawnCastEntity_0047AAE0(e, 0x6c, 1, 2);

            shake = 10;
            *Mem.Camera(0x005de0ac) = shake;
        }
        else
        {
            shake = *Mem.Camera(0x005de0ac);
        }

        if (shake != 0) *Mem.Camera(0x005de0ac) = (shake >> 2) - shake;

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 0x0a) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004868e0</c> — sous-zones 13 a 15. Absente de <c>Blaze.c</c> ; 76 instructions.
    /// <b>La fontaine d'un porteur</b>, vingt images : decalage nie, porteur range en <c>+0x70</c> et
    /// <c>+0x74</c>, echelles <c>0x400 / 0x800</c>, teinte <c>0xf0f0f0</c>. Chaque image il se replace
    /// sur son porteur par les angles de celui-ci, prend sa vitesse dans le cap du porteur (tangage
    /// <c>+0x34</c>, portee <c>+0x5c</c>) et seme, tangage prete a zero, le sous-type de l'octet
    /// <c>0x0a</c> du bloc <c>+0xd4</c> sur la planche de l'eclaboussure a <c>+8</c>
    /// (<c>0x00551a10</c>), parametre 4.
    /// </summary>
    private static void EntityHandler_004868E0(byte* e)
    {
        int* offset = stackalloc int[3];
        byte* carrier = *(byte**)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = -*(int*)(e + 0x44);
            *(int*)(e + 0x4c) = -*(int*)(e + 0x4c);
            *(byte**)(e + 0x74) = carrier;
            *(byte**)(e + 0x70) = carrier;
            *(uint*)(e + 0x28) = 0x08000400;
            *(uint*)(e + 0x38) = 0xf0f0f0;
            *(short*)(e + 0x9a) = 0;
        }

        RotateVectorByAngles_00536090((short*)(carrier + 0x30), (int*)(e + 0x44), offset);   // Ghidra: FUN_00536090

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c) + offset[0];
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + offset[1];
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24) + offset[2];

        SetVectorFromAngles_00532AD0((int*)(e + 0x50),
            *(short*)(carrier + 0x32), *(short*)(e + 0x34), *(int*)(e + 0x5c));   // Ghidra: FUN_00532ad0

        short pitch = *(short*)(e + 0x34);
        *(short*)(e + 0x34) = 0;

        byte* region = *(byte**)(s_Unknown_005e97cc + 0xd4);
        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), SplashDescriptors() + 8,
            (byte)((*(byte**)region)[0x0a] + 0xc0), 0, 4);            // Ghidra: FUN_0047ac40, &DAT_00551a10

        *(short*)(e + 0x10) += 1;
        *(short*)(e + 0x34) = pitch;
        if (*(short*)(e + 0x10) >= 0x14) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00442290</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 90 instructions. <b>La colonne qui s'eleve</b> : modele 0, relevee de 50
    /// unites, largeurs tirees, sans hauteur ; deux sons (voix <c>0x0f</c>, hauteurs <c>0x752</c> puis
    /// <c>0x800</c>) a ses deux premieres images, puis sa hauteur grandit de <c>0xbb8</c> par image
    /// jusqu'a <c>0x61a8</c>. Passe 70 images d'age, elle palit de 15 par canal et meurt a rouge
    /// nul.
    /// </summary>
    private static void EntityHandler_00442290(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) += 0x32000;
            e[4] = 0;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x28) = (short)(CrtRand() % 0x1f4 + 0x200);
            *(short*)(e + 0x2c) = (short)(CrtRand() % 0x1f4 + 0x200);
            *(short*)(e + 0x34) = (short)(CrtRand() % 0x400);
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x12) == 0) Aud.PlaySoundEffect(0x0f, 0x752, 0x1000, 0x1000);   // Ghidra: FUN_0053cea0
            if (*(short*)(e + 0x12) == 1) Aud.PlaySoundEffect(0x0f, 0x800, 0x1000, 0x1000);

            *(short*)(e + 0x2a) += 0x0bb8;
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x2a) > 0x61a8) *(short*)(e + 0x2a) = 0x61a8;

            if (*(short*)(e + 0x16) >= 0x46)
            {
                short r = (short)(e[0x38] - 0x0f);
                short g = (short)(e[0x39] - 0x0f);
                short b = (short)(e[0x3a] - 0x0f);
                if (r <= 0)
                {
                    r = 0;
                    g = 0;
                    b = 0;
                    *(uint*)e &= 0x7fffffff;
                }

                e[0x38] = (byte)r;
                e[0x39] = (byte)g;
                e[0x3a] = (byte)b;
            }
        }

        *(short*)(e + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00441040</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 93 instructions. <b>Le feu follet qui s'enroule</b> : planche
    /// <c>0x005514f0</c>, releve de 100 a 499 unites, teinte violette (<c>0x28 / 0 / 0x80</c>) ; il
    /// tourne autour de son point de naissance sur un rayon de 500 a 799 qui perd 10 par image,
    /// sa hauteur ondulant de 200 unites sur la meme phase, en virant au rouge (+2, 0, -2) ; a rayon
    /// negatif il meurt. Son <c>+0x14</c> tourne sans etre lu.
    /// </summary>
    private static void EntityHandler_00441040(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005514F0();            // Ghidra: &DAT_005514f0
            e[0x40] = 0;
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x1000);
            *(int*)(e + 0x20) += (CrtRand() % 0x190 + 0x64) << 0xc;

            int r = CrtRand() % 0x12c;
            e[0x0a] = 2;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            e[0x38] = 0x28;
            e[0x39] = 0;
            e[0x3a] = 0x80;
            *(short*)(e + 0x12) = (short)(r + 0x1f4);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short phase = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(phase) * *(short*)(e + 0x12) + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = (int)Winsys3d.GetCosTableEntry(phase) * *(short*)(e + 0x12) + *(int*)(e + 0x58);
        int wave = (int)Winsys3d.GetSinTableEntry(phase);

        *(short*)(e + 0x32) += 0x64;
        *(short*)(e + 0x12) -= 0x0a;
        *(short*)(e + 0x14) += 0x64;
        *(int*)(e + 0x20) = *(int*)(e + 0x54) + wave * 200;

        BrightenEntityTint_00404DE0(e, 2, 0, -2);                   // Ghidra: FUN_00404de0

        if (*(short*)(e + 0x14) >= 0x1000) *(short*)(e + 0x14) -= 0x1000;
        if (*(short*)(e + 0x12) < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00440f00</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 95 instructions. <b>L'etincelle projetee</b> : planche <c>0x005514f0</c>,
    /// relevee de 300 unites, echelle de 2 000 a 3 499, cap devie de <c>0x600</c> a <c>0x9ff</c> ;
    /// elle file dans son cap a une vitesse <c>+0x9a</c> qui croit de <c>+0x5c</c> (3 a 7) par
    /// image, monte ou descend de <c>+0x12</c> (±15) ; aux images 4 et 5 son acceleration baisse
    /// d'un, et des la quatrieme elle palit de 10 jusqu'a mourir.
    /// </summary>
    private static void EntityHandler_00440F00(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005514F0();            // Ghidra: &DAT_005514f0
            e[0x40] = 0;
            int turn = CrtRand() % 0x400;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x32) += (short)(turn + 0x600);

            int climb = CrtRand() % 0x1e;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x12) = (short)(climb - 0x0f);

            int accel = CrtRand() % 5;
            *(short*)(e + 0x9a) = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(int*)(e + 0x20) += 0x12c000;
            *(int*)(e + 0x5c) = accel + 3;

            short scale = (short)(CrtRand() % 0x5dc + 0x7d0);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short speed = *(short*)(e + 0x9a);
            short course = *(short*)(e + 0x32);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * speed;
            *(int*)(e + 0x24) += (int)Winsys3d.GetCosTableEntry(course) * speed;
            *(int*)(e + 0x20) += *(short*)(e + 0x12) << 0xc;
            *(short*)(e + 0x9a) = (short)((short)*(int*)(e + 0x5c) + speed);

            short age = *(short*)(e + 0x14);
            if (age >= 4)
            {
                if (age <= 5) *(int*)(e + 0x5c) -= 1;

                BrightenEntityTint_00404DE0(e, -10, -10, -10);      // Ghidra: FUN_00404de0
                if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        *(short*)(e + 0x16) += 1;
        *(short*)(e + 0x14) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00441180</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 96 instructions. <b>La spore qui s'eloigne</b> : planche <c>0x005514f0</c>,
    /// teinte verte (<c>0x64 / 0x82 / 0x32</c>), relevee de 230 unites ; elle s'eloigne de son point
    /// de naissance sur une sphere — cap et elevation tires une fois — a une distance <c>+0x9a</c>
    /// qui croit de 10 a 29 par image. Elle vit deux cents images.
    /// </summary>
    private static void EntityHandler_00441180(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005514F0();            // Ghidra: &DAT_005514f0
            e[0x40] = 0;
            e[0x0a] = 1;
            e[0x0b] = 0;
            e[0x38] = 0x64;
            e[0x39] = 0x82;
            e[0x3a] = 0x32;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);
            int lift = CrtRand() % 0x400;
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x14) = (short)lift;
            int step = CrtRand() % 0x14;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = step + 0x0a;
            *(int*)(e + 0x20) += 0xe6000;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x16) += 1;

        short distance = *(short*)(e + 0x9a);
        short elevation = *(short*)(e + 0x14);
        short course = *(short*)(e + 0x12);

        int flat = ((int)Winsys3d.GetCosTableEntry(elevation) * distance) >> 0xc;
        *(int*)(e + 0x1c) = flat * (int)Winsys3d.GetSinTableEntry(course) + *(int*)(e + 0x50);
        flat = ((int)Winsys3d.GetCosTableEntry(elevation) * distance) >> 0xc;
        *(int*)(e + 0x24) = flat * (int)Winsys3d.GetCosTableEntry(course) + *(int*)(e + 0x58);
        *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(elevation) * distance + *(int*)(e + 0x54);

        *(short*)(e + 0x9a) = (short)((short)*(int*)(e + 0x5c) + distance);
        if (*(short*)(e + 0x16) == 0xc8) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x004423d0</c> — sous-zones 13 a 15, table de debordement. Absente de
    /// <c>Blaze.c</c> ; 59 instructions. <b>Le tourbillon qui enfle et s'effondre</b> : modele 1,
    /// releve de 20 unites, ne sans echelle ; il tourne de <c>0xc8</c> par image et grandit de plus
    /// en plus vite (+10 d'acceleration) jusqu'a <c>0x61a8</c>, marque deux images, puis retrecit
    /// de plus en plus vite (depart 30, +40) jusqu'a disparaitre.
    /// </summary>
    private static void EntityHandler_004423D0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) += 0x14000;
            e[4] = 1;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 0x0a;
            short grow = *(short*)(e + 0x12);
            *(short*)(e + 0x28) += grow;
            *(short*)(e + 0x32) += 0xc8;
            *(short*)(e + 0x2a) += grow;
            *(short*)(e + 0x2c) += grow;

            if (*(short*)(e + 0x28) > 0x61a8)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) += 1;
            *(short*)(e + 0x32) += 0xc8;
            if (*(short*)(e + 0x12) >= 2)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0x1e;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x12) += 0x28;
            short shrink = *(short*)(e + 0x12);
            *(short*)(e + 0x28) -= shrink;
            *(short*)(e + 0x32) += 0xc8;
            *(short*)(e + 0x2a) -= shrink;
            *(short*)(e + 0x2c) -= shrink;

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0;
                *(uint*)e &= 0x7fffffff;
            }
        }

        *(short*)(e + 0x16) += 1;
    }
}
