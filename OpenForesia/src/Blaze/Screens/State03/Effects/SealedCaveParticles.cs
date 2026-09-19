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
}
