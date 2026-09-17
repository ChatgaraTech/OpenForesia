// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DE LA VIEILLE TOUR — zone 4.
//
// Ouvert le 2026-09-17, sur le patron de `MineParticles.cs` et de `FireMountainParticles.cs` : ce
// qui nait, scintille et meurt dans la zone 4, et ne decide de rien. La tour installe trois blocs
// de sous-zones — 0, 1, 2, 4 / 5, 6, 8, 10 / 3, 7, 9 — et le compilateur y a emis une fois par
// bloc des particules deja ecrites pour la mine.
//
// CE QUI EST ICI AUJOURD'HUI : la categorie A de `Documentation/Etat/Zone4ParComplexite.md`, les
// corps deja ecrits ailleurs. Chaque gestionnaire ne differe de son modele que par ses ADRESSES DE
// PLANCHE — diff STRICT a l'appui — et chaque planche est relevee pour elle-meme : a une adresse
// differente, les octets le sont souvent aussi (page 0x03 ici, page 0x02 dans la mine).
//
// LES BORNES DES PLANCHES viennent d'un BALAYAGE DU BINAIRE ENTIER, comme en zone 2 : une adresse
// citee comme immediate quatre octets par au moins un corps est une base, et une plage court
// jusqu'a la base suivante.
//
//   0x0054fa58  5 enreg.  bulle a +0, grain a +8          bases suivantes : 0x0054fa80
//   0x0054fba8  2         fumee violette                  0x0054fbb8
//   0x0054fc00  5         bulle a +0, grain a +8          0x0054fc28
//   0x0054fd08  2         gerbe a cinq phases, +0 et +8   0x0054fd50
//   0x0054fd58  5         bulle a +0, grain a +8          0x0054fd80
//   0x0054fe80  2         planches de role 2 des gerbes   0x0054fe90
//
// Les trois plages bulle + grain sont modelisees d'un seul tenant, et non en deux planches : les
// adresses 0x0054fa58, 0x0054fc00 et 0x0054fd58 sont aussi installees par d'autres corps de la tour
// pas encore ecrits (0x00416EE0, 0x00417CA0, 0x00417E10, 0x00418290 pour la premiere), et rien ne
// dit encore jusqu'ou ils lisent. Une seule allocation par plage les couvre d'avance.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES PLANCHES
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054fa58</c> — <b>la bulle et le grain des sous-zones 0, 1, 2 et
    /// 4</b> : la tuile <c>8 x 8</c> a l'origine de la page <c>0x03</c>, puis les quatre coins d'un
    /// carre de <c>0x10</c>. Bornee en haut par <c>0x0054fa80</c>, ou la tuile passe a
    /// <c>0x20 x 0x20</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FA58 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa58  <- 0x00417A20 (bulle)
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa60  <- 0x00417930 (grain)
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa68
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa70
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa78
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fba8</c> — <b>la fumee violette des sous-zones 5, 6, 8 et
    /// 10</b>, deux enregistrements : la tuile <c>8 x 8</c> et le separateur <c>0x3f x 0x3f</c> de
    /// la page <c>0x09</c>. La forme de <c>0x0054f2d8</c> dans la mine, bornee en haut par
    /// <c>0x0054fbb8</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FBA8 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054fba8  <- 0x004186A0
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054fbb0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fc00</c> — la bulle et le grain des sous-zones 5, 6, 8 et 10.
    /// Memes octets que <see cref="s_Descriptors_0054FA58"/> ; bornee par <c>0x0054fc28</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FC00 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc00  <- 0x00419630 (bulle)
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc08  <- 0x00419540 (grain)
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc10
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc18
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc20
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fd08</c> — <b>la planche de la gerbe a cinq phases des
    /// sous-zones 3, 7 et 9</b>, deux enregistrements, la forme de <c>0x0054f450</c> dans la mine.
    /// Le corps pose <c>+8</c> dans son role 3 et <b>ecrit l'octet 6 de ce second
    /// enregistrement</b> (<c>mov [54FD16h],al</c>) : les deux doivent vivre dans la meme
    /// allocation.</summary>
    private static readonly byte[] s_Descriptors_0054FD08 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054fd08  <- la gerbe
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054fd10  <- la gerbe, role 3
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fd58</c> — la bulle et le grain des sous-zones 3, 7 et 9.
    /// Memes octets que <see cref="s_Descriptors_0054FA58"/> ; bornee par <c>0x0054fd80</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FD58 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fd58  <- 0x0041C650 (bulle)
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fd60  <- 0x0041C560 (grain)
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fd68
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fd70
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054fd78
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fe80</c> — <b>les deux planches de role 2 des amorces de la
    /// gerbe</b>, deux tuiles <c>0x0f x 0x0f</c> de la page <c>0x09</c>, la forme de
    /// <c>0x0054f620</c> dans la mine a d'autres coordonnees (<c>0x30</c> et <c>0x40</c> au lieu de
    /// <c>0x00</c> et <c>0x10</c>). Bornee en haut par <c>0x0054fe90</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FE80 =
    [
        0x30, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054fe80  <- 0x0041A560
        0x40, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054fe88  <- 0x0041B420
    ];

    private static byte* s_Descriptors0054FA58Storage;
    private static byte* s_Descriptors0054FBA8Storage;
    private static byte* s_Descriptors0054FC00Storage;
    private static byte* s_Descriptors0054FD08Storage;
    private static byte* s_Descriptors0054FD58Storage;
    private static byte* s_Descriptors0054FE80Storage;

    private static byte* Descriptors0054FA58()
        => NativeCopy(s_Descriptors_0054FA58, ref s_Descriptors0054FA58Storage);

    private static byte* Descriptors0054FBA8()
        => NativeCopy(s_Descriptors_0054FBA8, ref s_Descriptors0054FBA8Storage);

    private static byte* Descriptors0054FC00()
        => NativeCopy(s_Descriptors_0054FC00, ref s_Descriptors0054FC00Storage);

    private static byte* Descriptors0054FD08()
        => NativeCopy(s_Descriptors_0054FD08, ref s_Descriptors0054FD08Storage);

    private static byte* Descriptors0054FD58()
        => NativeCopy(s_Descriptors_0054FD58, ref s_Descriptors0054FD58Storage);

    private static byte* Descriptors0054FE80()
        => NativeCopy(s_Descriptors_0054FE80, ref s_Descriptors0054FE80Storage);

    // =====================================================================
    // LES BULLES — le corps de 0x004079E0 (mine), une fois par bloc
    // =====================================================================

    /// <summary>Ghidra: FUN_00417a20 @ 0x00417a20 — <b>la bulle des sous-zones 0, 1, 2 et 4</b>.
    /// Decompilee dans <c>Blaze.c</c>. Le diff STRICT avec <see cref="EntityHandler_004079E0"/> ne
    /// rend qu'une ligne : <c>mov dword [esi+3Ch],54FA58h</c>. Le corps est
    /// <see cref="TickRisingBubble"/>.</summary>
    private static void EntityHandler_00417A20(byte* e)
        => TickRisingBubble(e, Descriptors0054FA58());          // Ghidra: &DAT_0054fa58

    /// <summary>Ghidra: FUN_00419630 @ 0x00419630 — la bulle des sous-zones 5, 6, 8 et 10, planche
    /// <c>0x0054fc00</c>.</summary>
    private static void EntityHandler_00419630(byte* e)
        => TickRisingBubble(e, Descriptors0054FC00());          // Ghidra: &DAT_0054fc00

    /// <summary>Ghidra: FUN_0041c650 @ 0x0041c650 — la bulle des sous-zones 3, 7 et 9, planche
    /// <c>0x0054fd58</c>.</summary>
    private static void EntityHandler_0041C650(byte* e)
        => TickRisingBubble(e, Descriptors0054FD58());          // Ghidra: &DAT_0054fd58

    // =====================================================================
    // LES GRAINS — le corps de 0x004070F0 (mine), une fois par bloc
    // =====================================================================

    /// <summary>Ghidra: <c>0x00417930</c> — <b>le grain des sous-zones 0, 1, 2 et 4</b>. Absente de
    /// <c>Blaze.c</c>. Le diff STRICT avec <see cref="EntityHandler_004070F0"/> ne rend qu'une ligne :
    /// <c>mov dword [esi+3Ch],54FA60h</c>, soit le second enregistrement de
    /// <see cref="s_Descriptors_0054FA58"/>. Le grain parcourt quatre images et meurt a l'egalite a
    /// 4 : les quatre enregistrements qui suivent sa base.</summary>
    private static void EntityHandler_00417930(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors0054FA58() + 8);   // Ghidra: &DAT_0054fa60

    /// <summary>Ghidra: <c>0x00419540</c> — le grain des sous-zones 5, 6, 8 et 10, planche
    /// <c>0x0054fc08</c>. Absente de <c>Blaze.c</c>.</summary>
    private static void EntityHandler_00419540(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors0054FC00() + 8);   // Ghidra: &DAT_0054fc08

    /// <summary>Ghidra: <c>0x0041c560</c> — le grain des sous-zones 3, 7 et 9, planche
    /// <c>0x0054fd60</c>. Absente de <c>Blaze.c</c>.</summary>
    private static void EntityHandler_0041C560(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors0054FD58() + 8);   // Ghidra: &DAT_0054fd60

    // =====================================================================
    // LA FUMEE VIOLETTE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004186a0</c> — <b>l'amorce de la fumee violette des sous-zones 5, 6, 8 et 10</b>.
    /// Absente de <c>Blaze.c</c> ; cinq instructions qui empilent l'enregistrement et appellent
    /// <c>FUN_004186b0</c>, seize octets plus loin.
    ///
    /// <para>
    /// <c>FUN_004186b0</c> est <see cref="TickPurpleSmoke"/> a une adresse de planche pres : le diff
    /// STRICT avec <c>FUN_00408050</c> (la mine) rend exactement trois lignes, les trois
    /// <c>mov dword [esi+3Ch],54FBA8h</c>. Meme forme d'amorce que
    /// <see cref="EntityHandler_00408040"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_004186A0(byte* e)
        => TickPurpleSmoke(e, Descriptors0054FBA8());   // Ghidra: FUN_004186b0, &DAT_0054fba8

    // =====================================================================
    // LA GERBE A CINQ PHASES — sous-zones 3, 7 et 9
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0041a560</c> — <b>la premiere amorce de la gerbe a cinq phases</b>, sous-zones
    /// 3, 7 et 9. Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// La forme exacte de <see cref="EntityHandler_0040B1B0"/> (mine), a trois constantes pres :
    /// <c>+0x98 = 0x3d</c> (les phases 2 et 3 feront naitre <c>0x3d + 0xc0 = 0xfd</c>),
    /// <c>+0x96 = 6</c>, et la planche de role 2 <c>0x0054fe80</c>. Le corps appele,
    /// <c>FUN_0041a5a0</c>, est <see cref="TickFivePhaseBurst"/> : le diff STRICT avec
    /// <c>FUN_0040b1f0</c> ne rend que les cinq adresses de planche (<c>0x0054fd08</c>, et
    /// <c>0x0054fd10</c> / <c>0x0054fd16</c> dans le role 3).
    /// </para>
    /// </summary>
    private static void EntityHandler_0041A560(byte* e)
    {
        *(short*)(e + 0x98) = 0x3d;
        *(short*)(e + 0x96) = 6;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054FE80();       // Ghidra: &DAT_0054fe80
        }

        TickFivePhaseBurst(e, Descriptors0054FD08());          // Ghidra: FUN_0041a5a0
    }

    /// <summary>
    /// Ghidra: <c>0x0041b420</c> — <b>la seconde amorce</b>, meme corps appele. <c>+0x98 = 3</c>,
    /// <c>+0x96 = 8</c>, planche de role 2 <c>0x0054fe88</c>, le second enregistrement de
    /// <see cref="s_Descriptors_0054FE80"/>. Absente de <c>Blaze.c</c>.
    /// </summary>
    private static void EntityHandler_0041B420(byte* e)
    {
        *(short*)(e + 0x98) = 3;
        *(short*)(e + 0x96) = 8;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054FE80() + 8;   // Ghidra: &DAT_0054fe88
        }

        TickFivePhaseBurst(e, Descriptors0054FD08());          // Ghidra: FUN_0041a5a0
    }
}
