// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES EFFETS DU LABYRINTHE DES MORTS — zone 5, tables 0x0056b2c8 et 0x0056b0c8.
//
// Ouvert le 2026-09-17 sur le patron de `OldTowerParticles.cs`. La zone installe ses tables par
// trois groupes de sous-zones — 0, 1, 2, 3 / 5, 6, 7, 8 / 4, 9 — et le compilateur a emis les corps
// d'effets UNE FOIS PAR GROUPE, chacun lisant sa propre planche.
//
// Le premier lot (2026-09-17) est entierement fait de RENVOIS : chaque corps ci-dessous est un corps
// deja ecrit ailleurs, a la planche et a quelques constantes pres, trouve par le tri de parente de
// `Documentation/Etat/Zone5ParComplexite.md` et verifie au diff STRICT. Les planches sont relevees
// dans `Blaze.exe` et bornees par l'adresse connue suivante.
//
// Le tri de la zone par complexite est dans `Documentation/Etat/Zone5ParComplexite.md`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES PLANCHES
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054fec0</c> — <b>l'eclat projete des sous-zones 0 a 3</b>. Memes
    /// octets que <see cref="s_Descriptors_0054FBB8"/> (tour) ; bornee par <c>0x0054fee0</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FEC0 =
    [
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fec0  <- 0x0041EAF0
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fec8
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fed0
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fed8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054ff20</c> — <b>la gerbe large des sous-zones 0 a 3</b>, six
    /// tuiles <c>0x20 x 0x20</c> de la page <c>0x01</c> (la foret les avait en <c>0x18</c>) ; le
    /// corps tire <c>rand % 6</c> a chaque image. Bornee par <c>0x0054ff50</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FF20 =
    [
        0x00, 0x60, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x0054ff20  <- 0x0041E430
        0x20, 0x60, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x0054ff28
        0x04, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x0054ff30
        0x20, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x0054ff38
        0x40, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x0054ff40
        0x40, 0xa0, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x0054ff48
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054ff60</c> — <b>le grain des sous-zones 0 a 3</b>, les quatre
    /// coins d'un carre de <c>0x10</c> (le grain parcourt quatre images). Bornee par
    /// <c>0x0054ff80</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FF60 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054ff60  <- 0x0041E250
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054ff68
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054ff70
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054ff78
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054ff80</c> — <b>l'escarbille lourde des sous-zones 0 a 3</b>.
    /// Le corps ne lit que le premier enregistrement (son <c>+0x40</c> reste a zero) ; les huit
    /// suivants, huit tuiles <c>0x1f</c> de la page <c>0x08</c>, sont declares avec lui jusqu'a
    /// <c>0x0054ffc8</c>, l'adresse connue suivante.</summary>
    private static readonly byte[] s_Descriptors_0054FF80 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054ff80  <- 0x0041E070
        0x00, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054ff88
        0x20, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054ff90
        0x40, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054ff98
        0x60, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054ffa0
        0x80, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054ffa8
        0xa0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054ffb0
        0xc0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054ffb8
        0xe0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054ffc0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054ffc8</c> — <b>l'eclat en ligne droite des sous-zones 0 a 3</b>,
    /// quatre tuiles <c>7 x 7</c> (le corps tire <c>rand &amp; 3</c>). Bornee par
    /// <c>0x0054ffe8</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FFC8 =
    [
        0x08, 0x08, 0x07, 0x07, 0x01, 0x02, 0x10, 0x00,   // 0x0054ffc8  <- 0x0041D920
        0x10, 0x08, 0x07, 0x07, 0x01, 0x02, 0x10, 0x00,   // 0x0054ffd0
        0x08, 0x10, 0x07, 0x07, 0x01, 0x02, 0x10, 0x00,   // 0x0054ffd8
        0x10, 0x10, 0x07, 0x07, 0x01, 0x02, 0x10, 0x00,   // 0x0054ffe0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005500a8</c> — l'eclat projete des sous-zones 5 a 8. Memes octets
    /// que <see cref="s_Descriptors_0054FEC0"/> ; bornee par <c>0x005500c8</c>.</summary>
    private static readonly byte[] s_Descriptors_005500A8 =
    [
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005500a8  <- 0x00421AE0
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005500b0
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005500b8
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005500c0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550108</c> — la gerbe large des sous-zones 5 a 8. Memes octets
    /// que <see cref="s_Descriptors_0054FF20"/> ; bornee par <c>0x00550138</c>.</summary>
    private static readonly byte[] s_Descriptors_00550108 =
    [
        0x00, 0x60, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550108  <- 0x00421420
        0x20, 0x60, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550110
        0x04, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550118
        0x20, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550120
        0x40, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550128
        0x40, 0xa0, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550130
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550148</c> — le grain des sous-zones 5 a 8. Memes octets que
    /// <see cref="s_Descriptors_0054FF60"/> ; bornee par <c>0x00550168</c>.</summary>
    private static readonly byte[] s_Descriptors_00550148 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550148  <- 0x00421240
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550150
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550158
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550160
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550168</c> — l'escarbille lourde des sous-zones 5 a 8. Memes
    /// octets que <see cref="s_Descriptors_0054FF80"/> ; bornee par <c>0x005501b0</c>.</summary>
    private static readonly byte[] s_Descriptors_00550168 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x00550168  <- 0x00421060
        0x00, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550170
        0x20, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550178
        0x40, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550180
        0x60, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550188
        0x80, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550190
        0xa0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550198
        0xc0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x005501a0
        0xe0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x005501a8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005501b0</c> — l'eclat en ligne droite des sous-zones 5 a 8. Memes
    /// octets que <see cref="s_Descriptors_0054FFC8"/> ; bornee par <c>0x005501d0</c>.</summary>
    private static readonly byte[] s_Descriptors_005501B0 =
    [
        0x08, 0x08, 0x07, 0x07, 0x01, 0x02, 0x10, 0x00,   // 0x005501b0  <- 0x00420910
        0x10, 0x08, 0x07, 0x07, 0x01, 0x02, 0x10, 0x00,   // 0x005501b8
        0x08, 0x10, 0x07, 0x07, 0x01, 0x02, 0x10, 0x00,   // 0x005501c0
        0x10, 0x10, 0x07, 0x07, 0x01, 0x02, 0x10, 0x00,   // 0x005501c8
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550280</c> — <b>la planche PARTAGEE de la fumee violette et de la gerbe a
    /// cinq phases des sous-zones 4 et 9</b>, deux enregistrements : la tuile <c>8 x 8</c> et le
    /// separateur <c>0x3f x 0x3f</c> de la page <c>0x09</c>.
    ///
    /// <para>
    /// <b>La tour avait deux planches pour ces deux corps</b> (<c>0x0054fba8</c> et <c>0x0054fd08</c>) ;
    /// le Labyrinthe n'en a qu'une, et <c>FUN_004232b0</c> (la fumee) comme <c>FUN_00423d80</c> (la
    /// gerbe) y pointent. <b>La gerbe ecrit l'octet 6 du second enregistrement</b>
    /// (<c>DAT_0055028e</c>) : la fumee le voit. Une seule allocation porte donc les deux, comme le
    /// binaire. Bornee par <c>0x00550290</c>, un enregistrement d'une autre forme que personne de
    /// connu ne lit.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00550280 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x00550280  <- 0x004232B0, 0x00423D80
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x00550288  <- 0x00423D80, role 3
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550438</c> — <b>les deux planches de role 2 des amorces de la
    /// gerbe</b>, deux tuiles <c>0x0f x 0x0f</c> de la page <c>0x09</c> : la forme de
    /// <see cref="s_Descriptors_0054FE80"/> (tour) a d'autres coordonnees (<c>0x50</c>, <c>0x60</c>).
    /// Bornee par <c>0x00550448</c>.</summary>
    private static readonly byte[] s_Descriptors_00550438 =
    [
        0x50, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x00550438  <- 0x00423D40
        0x60, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x00550440  <- 0x00424490
    ];

    private static byte* s_Descriptors0054FEC0Storage;
    private static byte* s_Descriptors0054FF20Storage;
    private static byte* s_Descriptors0054FF60Storage;
    private static byte* s_Descriptors0054FF80Storage;
    private static byte* s_Descriptors0054FFC8Storage;
    private static byte* s_Descriptors005500A8Storage;
    private static byte* s_Descriptors00550108Storage;
    private static byte* s_Descriptors00550148Storage;
    private static byte* s_Descriptors00550168Storage;
    private static byte* s_Descriptors005501B0Storage;
    private static byte* s_Descriptors00550280Storage;
    private static byte* s_Descriptors00550438Storage;

    private static byte* Descriptors0054FEC0() => NativeCopy(s_Descriptors_0054FEC0, ref s_Descriptors0054FEC0Storage);
    private static byte* Descriptors0054FF20() => NativeCopy(s_Descriptors_0054FF20, ref s_Descriptors0054FF20Storage);
    private static byte* Descriptors0054FF60() => NativeCopy(s_Descriptors_0054FF60, ref s_Descriptors0054FF60Storage);
    private static byte* Descriptors0054FF80() => NativeCopy(s_Descriptors_0054FF80, ref s_Descriptors0054FF80Storage);
    private static byte* Descriptors0054FFC8() => NativeCopy(s_Descriptors_0054FFC8, ref s_Descriptors0054FFC8Storage);
    private static byte* Descriptors005500A8() => NativeCopy(s_Descriptors_005500A8, ref s_Descriptors005500A8Storage);
    private static byte* Descriptors00550108() => NativeCopy(s_Descriptors_00550108, ref s_Descriptors00550108Storage);
    private static byte* Descriptors00550148() => NativeCopy(s_Descriptors_00550148, ref s_Descriptors00550148Storage);
    private static byte* Descriptors00550168() => NativeCopy(s_Descriptors_00550168, ref s_Descriptors00550168Storage);
    private static byte* Descriptors005501B0() => NativeCopy(s_Descriptors_005501B0, ref s_Descriptors005501B0Storage);
    private static byte* Descriptors00550280() => NativeCopy(s_Descriptors_00550280, ref s_Descriptors00550280Storage);
    private static byte* Descriptors00550438() => NativeCopy(s_Descriptors_00550438, ref s_Descriptors00550438Storage);

    // =====================================================================
    // SOUS-ZONES 0, 1, 2, 3 ET 5, 6, 7, 8 — table 0x0056b2c8, une copie par groupe
    // =====================================================================

    /// <summary>Ghidra: <c>0x0041e250</c> — <b>le grain</b>, sous-zones 0 a 3. Absente de
    /// <c>Blaze.c</c> ; le diff STRICT avec <see cref="EntityHandler_004070F0"/> (mine) ne rend que la
    /// planche : <c>mov dword [esi+3Ch],54FF60h</c>. Voir <see cref="TickMineSpeck_004070F0"/>.</summary>
    private static void EntityHandler_0041E250(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors0054FF60());          // Ghidra: &DAT_0054ff60

    /// <summary>Ghidra: <c>0x00421240</c> — le grain, sous-zones 5 a 8, planche
    /// <c>0x00550148</c>.</summary>
    private static void EntityHandler_00421240(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors00550148());          // Ghidra: &DAT_00550148

    /// <summary>Ghidra: FUN_0041e070 @ 0x0041e070 — <b>l'escarbille lourde</b>, sous-zones 0 a 3.
    /// Decompilee dans <c>Blaze.c</c> ; le diff STRICT avec <see cref="EntityHandler_00407800"/>
    /// (mine) ne rend que la planche : <c>mov dword [esi+3Ch],54FF80h</c>. Voir
    /// <see cref="TickHeavySpark_00407800"/>.</summary>
    private static void EntityHandler_0041E070(byte* e)
        => TickHeavySpark_00407800(e, Descriptors0054FF80());         // Ghidra: &DAT_0054ff80

    /// <summary>Ghidra: FUN_00421060 @ 0x00421060 — l'escarbille lourde, sous-zones 5 a 8, planche
    /// <c>0x00550168</c>.</summary>
    private static void EntityHandler_00421060(byte* e)
        => TickHeavySpark_00407800(e, Descriptors00550168());         // Ghidra: &DAT_00550168

    /// <summary>Ghidra: FUN_0041d920 @ 0x0041d920 — <b>l'eclat en ligne droite</b>, sous-zones 0 a 3.
    /// Decompilee dans <c>Blaze.c</c>. Le diff STRICT avec <see cref="TickDriftingShard_00416920"/>
    /// (tour) rend la planche <c>0x0054ffc8</c> et les deux octets de role : <c>+0x0a = 9</c> et
    /// <c>+0x0b = 0</c> au lieu de <c>7</c> et <c>0xfb</c>.</summary>
    private static void EntityHandler_0041D920(byte* e)
        => TickDriftingShard(e, Descriptors0054FFC8(), 9, 0);         // Ghidra: &DAT_0054ffc8

    /// <summary>Ghidra: FUN_00420910 @ 0x00420910 — l'eclat en ligne droite, sous-zones 5 a 8,
    /// planche <c>0x005501b0</c>.</summary>
    private static void EntityHandler_00420910(byte* e)
        => TickDriftingShard(e, Descriptors005501B0(), 9, 0);         // Ghidra: &DAT_005501b0

    /// <summary>Ghidra: <c>0x0041eaf0</c> — <b>l'eclat projete</b>, sous-zones 0 a 3. Absente de
    /// <c>Blaze.c</c>. Le diff STRICT avec <see cref="EntityHandler_00419D30"/> (tour) rend cinq
    /// lignes : la planche <c>0x0054fec0</c>, <c>+0x0a = 3</c>, <b>deux <c>shl eax,4</c></b> dans le
    /// vol (le pas double) et une cadence d'image de <c>5</c> au lieu de <c>0x0a</c>.</summary>
    private static void EntityHandler_0041EAF0(byte* e)
        => TickProjectedShard(e, Descriptors0054FEC0(), 3, 4, 5);     // Ghidra: &DAT_0054fec0

    /// <summary>Ghidra: <c>0x00421ae0</c> — l'eclat projete, sous-zones 5 a 8, planche
    /// <c>0x005500a8</c>.</summary>
    private static void EntityHandler_00421AE0(byte* e)
        => TickProjectedShard(e, Descriptors005500A8(), 3, 4, 5);     // Ghidra: &DAT_005500a8

    /// <summary>
    /// Ghidra: FUN_0041e430 @ 0x0041e430 — <b>la gerbe serree</b>, sous-zones 0 a 3. Decompilee dans
    /// <c>Blaze.c</c>. Le diff avec <see cref="EntityHandler_0040F8E0"/> (foret) rend la planche
    /// <c>0x0054ff20</c> et trois ecarts : la dispersion en X est <c>rand % 100 - 50</c> (la foret :
    /// <c>500</c> et <c>250</c>), celle en Y <c>rand % 50 - 25</c> (<c>70</c> et <c>30</c>), et le
    /// decalage de Z par le sinus du cap est <b>retranche</b> — <c>neg ecx ; shl ecx,2 ; sub ecx,eax ;
    /// lea eax,[ecx+ecx*4] ; shl eax,1</c>, soit <c>-50 × sinus</c>.
    /// </summary>
    private static void EntityHandler_0041E430(byte* e)
        => TickWideSpray_0040F8E0(e, Descriptors0054FF20(), 100, 0x32, 0x32, 0x19, -0x32);   // Ghidra: &DAT_0054ff20

    /// <summary>Ghidra: FUN_00421420 @ 0x00421420 — la gerbe serree, sous-zones 5 a 8, planche
    /// <c>0x00550108</c>.</summary>
    private static void EntityHandler_00421420(byte* e)
        => TickWideSpray_0040F8E0(e, Descriptors00550108(), 100, 0x32, 0x32, 0x19, -0x32);   // Ghidra: &DAT_00550108

    // =====================================================================
    // SOUS-ZONES 4 ET 9 — les amorces
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004232a0</c> — <b>l'amorce de la fumee violette</b>, table <c>0x0056b2c8</c>.
    /// Absente de <c>Blaze.c</c> ; cinq instructions qui appellent <c>FUN_004232b0</c>, decompilee, et
    /// dont le texte ne differe de <c>FUN_00413850</c> (foret) que par la planche
    /// <see cref="s_Descriptors_00550280"/>, trois fois. Voir <see cref="TickPurpleSmoke"/>.
    /// </summary>
    private static void EntityHandler_004232A0(byte* e)
        => TickPurpleSmoke(e, Descriptors00550280());                 // Ghidra: FUN_004232b0

    /// <summary>
    /// Ghidra: <c>0x00423d40</c> — <b>la premiere amorce de la gerbe a cinq phases</b>, table
    /// <c>0x0056b2c8</c>. Absente de <c>Blaze.c</c>. La forme de <see cref="EntityHandler_0041B420"/>
    /// (tour) : <c>+0x98 = 3</c>, <c>+0x96 = 0x0b</c>, planche de role 2 <c>0x00550438</c>. Le corps
    /// appele, <c>FUN_00423d80</c>, ne differe de <c>FUN_0041a5a0</c> que par la planche
    /// <see cref="s_Descriptors_00550280"/> et l'octet qu'il y ecrit (<c>DAT_0055028e</c>).
    /// </summary>
    private static void EntityHandler_00423D40(byte* e)
    {
        *(short*)(e + 0x98) = 3;
        *(short*)(e + 0x96) = 0x0b;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00550438();               // Ghidra: &DAT_00550438
        }

        TickFivePhaseBurst(e, Descriptors00550280());                 // Ghidra: FUN_00423d80
    }

    /// <summary>Ghidra: <c>0x00424490</c> — <b>la seconde amorce</b>, meme corps appele :
    /// <c>+0x98 = 4</c>, <c>+0x96 = 8</c>, planche de role 2 <c>0x00550440</c>.</summary>
    private static void EntityHandler_00424490(byte* e)
    {
        *(short*)(e + 0x98) = 4;
        *(short*)(e + 0x96) = 8;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00550438() + 8;           // Ghidra: &DAT_00550440
        }

        TickFivePhaseBurst(e, Descriptors00550280());                 // Ghidra: FUN_00423d80
    }
}
