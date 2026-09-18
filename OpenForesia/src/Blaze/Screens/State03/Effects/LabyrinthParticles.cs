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

    // -----------------------------------------------------------------
    // LOT 2 (2026-09-17) — LES PLANCHES DES NEUF PAIRES MIROIR
    //
    // Le bloc des sous-zones 5 a 8 est le bloc 0 a 3 recopie a +0x1e8, planches comprises : chaque
    // paire ci-dessous porte les MEMES octets. Les longueurs ne viennent pas de l'adresse suivante
    // seule — chaque corps borne sa propre planche par l'indice maximal qu'il ecrit en +0x40, et les
    // deux lectures concordent partout.
    // -----------------------------------------------------------------

    /// <summary>Ghidra: <c>&amp;DAT_0054fe90</c> — <b>la bouffee</b> des sous-zones 0 a 3, trois tuiles
    /// <c>0x20 x 0x20</c> de la page <c>0x03</c>. Bornee par <c>0x0054fea8</c> ET par le corps :
    /// <see cref="TickLabyrinthPuff"/> mene <c>+0x40</c> de 0 a 2.</summary>
    private static readonly byte[] s_Descriptors_0054FE90 =
    [
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fe90  <- 0x0041E610, 0x0041E700
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fe98
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fea0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fea8</c> — <b>l'eventail d'etincelles court</b> des sous-zones
    /// 0 a 3. Memes tuiles que <see cref="s_Descriptors_0054FE90"/> a l'octet 4 pres (<c>0x00</c> au
    /// lieu de <c>0x01</c>). Bornee par <c>0x0054fec0</c> ; <see cref="TickLabyrinthSparkFan"/> mene
    /// <c>+0x40</c> de 0 a 2 sur cette branche.</summary>
    private static readonly byte[] s_Descriptors_0054FEA8 =
    [
        0x60, 0x88, 0x20, 0x20, 0x00, 0x03, 0x10, 0x00,   // 0x0054fea8  <- 0x0041DD40 (+0x16 == 0)
        0x80, 0x88, 0x20, 0x20, 0x00, 0x03, 0x10, 0x00,   // 0x0054feb0
        0xa0, 0x88, 0x20, 0x20, 0x00, 0x03, 0x10, 0x00,   // 0x0054feb8
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054fee0</c> — <b>la braise</b> des sous-zones 0 a 3, cinq tuiles
    /// <c>0x2f x 0x2f</c> de la page <c>0x18</c>.
    ///
    /// <para>
    /// <b>CINQ enregistrements, pas huit.</b> L'adresse connue suivante est <c>0x0054ff20</c>, ce qui
    /// laisserait huit places ; mais les octets changent de forme des <c>0x0054ff08</c>
    /// (<c>0x18 x 0x18</c>, page <c>0x10</c>) et <see cref="TickLabyrinthEmber"/> fait tourner
    /// <c>+0x40</c> de 0 a 4 exactement. Les deux lectures se recoupent : la table s'arrete a cinq.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054FEE0 =
    [
        0x00, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x0054fee0  <- 0x0041E7F0
        0x30, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x0054fee8
        0x60, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x0054fef0
        0x90, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x0054fef8
        0xc0, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x0054ff00
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054ff50</c> — <b>le grain qui monte</b> des sous-zones 0 a 3, deux
    /// tuiles <c>8 x 8</c> de la page <c>0x10</c> qui ne different que par l'octet 5. Bornee par
    /// <c>0x0054ff60</c> ; <see cref="TickLabyrinthRisingMote"/> laisse <c>+0x40</c> a zero.</summary>
    private static readonly byte[] s_Descriptors_0054FF50 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054ff50  <- 0x0041E340
        0x00, 0x00, 0x08, 0x08, 0x01, 0x00, 0x10, 0x00,   // 0x0054ff58
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054ffe8</c> — <b>la nappe posee</b> des sous-zones 0 a 3, quatre
    /// tuiles <c>0x18 x 0x17</c> de la page <c>0x03</c>. C'est la SECONDE planche de
    /// <see cref="TickLabyrinthFallingShroud"/>, celle de l'etat 3 ; bornee par <c>0x00550008</c>, et
    /// le corps borne <c>+0x40</c> a 3 explicitement.</summary>
    private static readonly byte[] s_Descriptors_0054FFE8 =
    [
        0x00, 0xe8, 0x18, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x0054ffe8  <- 0x0041DA80 (etat 3)
        0x18, 0xe8, 0x18, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x0054fff0
        0x30, 0xe8, 0x18, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x0054fff8
        0x48, 0xe8, 0x18, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x00550000
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550008</c> — <b>la nappe qui tombe</b> des sous-zones 0 a 3, la PREMIERE
    /// planche de <see cref="TickLabyrinthFallingShroud"/>.
    ///
    /// <para>
    /// <b>UN seul enregistrement, et c'est une preuve, pas une borne.</b> L'adresse connue suivante
    /// est <c>0x00550078</c> — le debut du banc des sous-zones 5 a 8 —, ce qui laisserait quatorze
    /// places ; mais les octets qui suivent sont d'une autre forme (<c>0x20 x 0x20</c> page
    /// <c>0x10</c>, puis <c>0x10 x 0x10</c>), et surtout le corps met <c>+0x40</c> a zero en posant
    /// cette planche et n'y touche plus : l'etat 1 ne fait que descendre. C'est l'etat 2 qui change de
    /// planche, et c'est lui qui remet le compteur a zero pour l'autre.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00550008 =
    [
        0x00, 0xc0, 0x17, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x00550008  <- 0x0041DA80 (etats 0 et 1)
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550078</c> — la bouffee des sous-zones 5 a 8. Memes octets que
    /// <see cref="s_Descriptors_0054FE90"/> ; bornee par <c>0x00550090</c>.</summary>
    private static readonly byte[] s_Descriptors_00550078 =
    [
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550078  <- 0x00421600, 0x004216F0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550080
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550088
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550090</c> — l'eventail d'etincelles court des sous-zones 5 a 8.
    /// Memes octets que <see cref="s_Descriptors_0054FEA8"/> ; bornee par <c>0x005500a8</c>.</summary>
    private static readonly byte[] s_Descriptors_00550090 =
    [
        0x60, 0x88, 0x20, 0x20, 0x00, 0x03, 0x10, 0x00,   // 0x00550090  <- 0x00420D30 (+0x16 == 0)
        0x80, 0x88, 0x20, 0x20, 0x00, 0x03, 0x10, 0x00,   // 0x00550098
        0xa0, 0x88, 0x20, 0x20, 0x00, 0x03, 0x10, 0x00,   // 0x005500a0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005500c8</c> — la braise des sous-zones 5 a 8. Memes octets que
    /// <see cref="s_Descriptors_0054FEE0"/>, et meme arret a CINQ : les octets changent de forme des
    /// <c>0x005500f0</c>.</summary>
    private static readonly byte[] s_Descriptors_005500C8 =
    [
        0x00, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x005500c8  <- 0x004217E0
        0x30, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x005500d0
        0x60, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x005500d8
        0x90, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x005500e0
        0xc0, 0x40, 0x2f, 0x2f, 0x01, 0x02, 0x18, 0x00,   // 0x005500e8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550138</c> — le grain qui monte des sous-zones 5 a 8. Memes
    /// octets que <see cref="s_Descriptors_0054FF50"/> ; bornee par <c>0x00550148</c>.</summary>
    private static readonly byte[] s_Descriptors_00550138 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550138  <- 0x00421330
        0x00, 0x00, 0x08, 0x08, 0x01, 0x00, 0x10, 0x00,   // 0x00550140
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005501d0</c> — la nappe posee des sous-zones 5 a 8. Memes octets
    /// que <see cref="s_Descriptors_0054FFE8"/> ; bornee par <c>0x005501f0</c>.</summary>
    private static readonly byte[] s_Descriptors_005501D0 =
    [
        0x00, 0xe8, 0x18, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x005501d0  <- 0x00420A70 (etat 3)
        0x18, 0xe8, 0x18, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x005501d8
        0x30, 0xe8, 0x18, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x005501e0
        0x48, 0xe8, 0x18, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x005501e8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005501f0</c> — la nappe qui tombe des sous-zones 5 a 8. Memes
    /// octets que <see cref="s_Descriptors_00550008"/>, et meme preuve : le corps laisse <c>+0x40</c>
    /// a zero tant que cette planche est posee.</summary>
    private static readonly byte[] s_Descriptors_005501F0 =
    [
        0x00, 0xc0, 0x17, 0x17, 0x01, 0x02, 0x03, 0x00,   // 0x005501f0  <- 0x00420A70 (etats 0 et 1)
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550350</c> — le grain qui monte des sous-zones <b>4 et 9</b>.
    /// Memes octets que <see cref="s_Descriptors_0054FF50"/> : le banc des sous-zones 4 et 9 est celui
    /// des sous-zones 0 a 3 recopie a <c>+0x400</c>, la ou celui des sous-zones 5 a 8 l'est a
    /// <c>+0x1e8</c>.</summary>
    private static readonly byte[] s_Descriptors_00550350 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550350  <- 0x00425500
        0x00, 0x00, 0x08, 0x08, 0x01, 0x00, 0x10, 0x00,   // 0x00550358
    ];

    private static byte* s_Descriptors0054FE90Storage;
    private static byte* s_Descriptors0054FEA8Storage;
    private static byte* s_Descriptors0054FEE0Storage;
    private static byte* s_Descriptors0054FF50Storage;
    private static byte* s_Descriptors0054FFE8Storage;
    private static byte* s_Descriptors00550008Storage;
    private static byte* s_Descriptors00550078Storage;
    private static byte* s_Descriptors00550090Storage;
    private static byte* s_Descriptors005500C8Storage;
    private static byte* s_Descriptors00550138Storage;
    private static byte* s_Descriptors005501D0Storage;
    private static byte* s_Descriptors005501F0Storage;
    private static byte* s_Descriptors00550350Storage;

    private static byte* Descriptors0054FE90() => NativeCopy(s_Descriptors_0054FE90, ref s_Descriptors0054FE90Storage);
    private static byte* Descriptors0054FEA8() => NativeCopy(s_Descriptors_0054FEA8, ref s_Descriptors0054FEA8Storage);
    private static byte* Descriptors0054FEE0() => NativeCopy(s_Descriptors_0054FEE0, ref s_Descriptors0054FEE0Storage);
    private static byte* Descriptors0054FF50() => NativeCopy(s_Descriptors_0054FF50, ref s_Descriptors0054FF50Storage);
    private static byte* Descriptors0054FFE8() => NativeCopy(s_Descriptors_0054FFE8, ref s_Descriptors0054FFE8Storage);
    private static byte* Descriptors00550008() => NativeCopy(s_Descriptors_00550008, ref s_Descriptors00550008Storage);
    private static byte* Descriptors00550078() => NativeCopy(s_Descriptors_00550078, ref s_Descriptors00550078Storage);
    private static byte* Descriptors00550090() => NativeCopy(s_Descriptors_00550090, ref s_Descriptors00550090Storage);
    private static byte* Descriptors005500C8() => NativeCopy(s_Descriptors_005500C8, ref s_Descriptors005500C8Storage);
    private static byte* Descriptors00550138() => NativeCopy(s_Descriptors_00550138, ref s_Descriptors00550138Storage);
    private static byte* Descriptors005501D0() => NativeCopy(s_Descriptors_005501D0, ref s_Descriptors005501D0Storage);
    private static byte* Descriptors005501F0() => NativeCopy(s_Descriptors_005501F0, ref s_Descriptors005501F0Storage);
    private static byte* Descriptors00550350() => NativeCopy(s_Descriptors_00550350, ref s_Descriptors00550350Storage);

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

    // =====================================================================
    // LOT 2 (2026-09-17) — LES NEUF PAIRES MIROIR DE LA TABLE 0x0056b2c8
    //
    // Le bloc des sous-zones 5 a 8 est le bloc 0 a 3 RECOPIE A +0x2ff0, corps par corps, creneau par
    // creneau. Dix-neuf corps (les neuf paires, plus le troisieme exemplaire de la troisieme en
    // sous-zones 4 et 9) pour HUIT lectures : la seule difference entre les deux exemplaires d'une
    // paire est la planche — et une fois, un signe.
    //
    // Ce qui suit est donc huit corps partages et dix-neuf points d'entree qui ne font que nommer
    // leur planche. Chaque corps borne sa planche par l'indice qu'il mene en +0x40 ; les longueurs
    // declarees plus haut viennent de la, pas seulement de l'adresse suivante.
    // =====================================================================

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_0041E610"/>, <see cref="EntityHandler_0041E700"/>
    /// et de leurs deux jumeaux des sous-zones 5 a 8. Absent de <c>Blaze.c</c> ; 78 instructions.
    ///
    /// <para>
    /// <b>Une bouffee qui attend, puis se joue en trois images.</b> A la naissance elle se decale au
    /// hasard des trois cotes — <paramref name="xSpread"/> centre en X, <c>0x1e</c> centre en Y,
    /// <c>0x64</c> centre en Z, tout en cases —, se met a l'echelle <c>0x1000</c> et allume le bit
    /// <c>0x4000</c>.
    /// </para>
    /// <para>
    /// <b>L'attente se termine au hasard.</b> Chaque image, une chance sur cinq d'en sortir ; et de
    /// toute facon a la troisieme. Le passage a l'etat 2 retire le bit <c>0x4000</c> et <b>rend la
    /// main tout de suite</b> — le binaire a un <c>ret</c> propre a cette branche, la planche ne
    /// commence donc qu'a l'image suivante.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est la planche</b> : <c>+0x40</c> avance d'une image par tour et la bouffee
    /// s'eteint (bit <c>0x80000000</c>) des qu'il atteint 3. Trois enregistrements suffisent, et la
    /// planche en a exactement trois.
    /// </para>
    /// </summary>
    /// <param name="xSpread">L'etalement en X, en cases ; le tirage est centre sur sa moitie.</param>
    private static void TickLabyrinthPuff(byte* e, byte* plate, int xSpread)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            e[0x0a] = 3;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % xSpread - xSpread / 2) << 0xc;   // Ghidra: FUN_00547140
            *(int*)(e + 0x20) += (CrtRand() % 0x1e - 0xf) << 0xc;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x24) += (CrtRand() % 0x64 - 0x32) << 0xc;
            e[0x40] = 0;
            *(uint*)e |= 0x4000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (CrtRand() % 5 == 1 || *(short*)(e + 0x12) == 3)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0xffffbfff;
                return;                       // Ghidra: `pop esi ; pop ebx ; ret` propre a la branche
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            byte frame = (byte)(e[0x40] + 1);
            e[0x40] = frame;

            if (frame >= 3)                   // Ghidra: `cmp al,3 ; jb` — NON signe
            {
                e[0x40] = 0;
                *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>Ghidra: <c>0x0041e610</c> — la bouffee large, sous-zones 0 a 3, creneau <b>48</b> du
    /// bloc <c>0x00557fa8</c>. Etalement en X de <c>0x190</c> cases.</summary>
    private static void EntityHandler_0041E610(byte* e)
        => TickLabyrinthPuff(e, Descriptors0054FE90(), 0x190);         // Ghidra: &DAT_0054fe90

    /// <summary>Ghidra: <c>0x0041e700</c> — <b>la meme bouffee, deux fois plus serree</b> :
    /// <c>0x0041E610</c> a une constante pres (<c>0xc8</c> au lieu de <c>0x190</c>), meme planche.
    /// Sous-zones 0 a 3, creneau <b>49</b>.</summary>
    private static void EntityHandler_0041E700(byte* e)
        => TickLabyrinthPuff(e, Descriptors0054FE90(), 0xc8);          // Ghidra: &DAT_0054fe90

    /// <summary>Ghidra: <c>0x00421600</c> — la bouffee large, sous-zones 5 a 8, creneau 48 du bloc
    /// <c>0x00558628</c>.</summary>
    private static void EntityHandler_00421600(byte* e)
        => TickLabyrinthPuff(e, Descriptors00550078(), 0x190);         // Ghidra: &DAT_00550078

    /// <summary>Ghidra: <c>0x004216f0</c> — la bouffee serree, sous-zones 5 a 8, creneau 49.</summary>
    private static void EntityHandler_004216F0(byte* e)
        => TickLabyrinthPuff(e, Descriptors00550078(), 0xc8);          // Ghidra: &DAT_00550078

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_0041E340"/> et de ses <b>deux</b> jumeaux.
    /// Absent de <c>Blaze.c</c> ; 73 instructions.
    ///
    /// <para>
    /// <b>Un grain qui monte en ralentissant, et qui retrecit avant de disparaitre.</b> Il nait dans
    /// un carre de <c>0xa0</c> cases centre sur sa position, monte de <c>0 a 0x14</c> cases, et se
    /// donne une vitesse verticale de <c>10 a 19</c> cases par image dans <c>+0x12</c>.
    /// </para>
    /// <para>
    /// <b>La vitesse tombe d'une unite toutes les DEUX images</b> — <c>+0x14</c> est le compteur de
    /// parite, remis a zero a chaque decrement. Sous <c>7</c>, l'echelle perd <c>0xc8</c> par image
    /// sur les deux axes ; sous <c>3</c>, le grain s'eteint. L'echelle de depart est <c>0x9c4</c>.
    /// </para>
    /// <para>
    /// <b>La vitesse est relue apres le decrement</b>, pas avant : le binaire refait un
    /// <c>mov ax,[esi+12h]</c> juste avant les deux comparaisons. C'est ce qui fait que le
    /// retrecissement commence l'image meme ou la vitesse passe sous le seuil.
    /// </para>
    /// </summary>
    private static void TickLabyrinthRisingMote(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x0a] = 6;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0xa0 - 0x50) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0xa0 - 0x50) << 0xc;

            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x28) = 0x9c4;
            *(short*)(e + 0x2a) = 0x9c4;
            e[0x40] = 0;

            *(int*)(e + 0x20) += (CrtRand() % 0x14) << 0xc;            // sans decalage : vers le haut
            *(short*)(e + 0x12) = (short)(CrtRand() % 0xa + 0xa);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short speed = *(short*)(e + 0x12);
            *(int*)(e + 0x20) += speed << 0xc;

            (*(short*)(e + 0x14))++;
            if (*(short*)(e + 0x14) == 2)
            {
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x12) = (short)(speed - 1);
            }

            short now = *(short*)(e + 0x12);                           // Ghidra: relu, LAB_0041e408

            if (now < 7)
            {
                *(short*)(e + 0x28) += -0xc8;
                *(short*)(e + 0x2a) += -0xc8;
            }

            if (now < 3) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>Ghidra: <c>0x0041e340</c> — le grain qui monte, sous-zones 0 a 3, creneau
    /// <b>63</b>.</summary>
    private static void EntityHandler_0041E340(byte* e)
        => TickLabyrinthRisingMote(e, Descriptors0054FF50());          // Ghidra: &DAT_0054ff50

    /// <summary>Ghidra: <c>0x00421330</c> — le meme, sous-zones 5 a 8.</summary>
    private static void EntityHandler_00421330(byte* e)
        => TickLabyrinthRisingMote(e, Descriptors00550138());          // Ghidra: &DAT_00550138

    /// <summary>Ghidra: <c>0x00425500</c> — le meme, sous-zones <b>4 et 9</b>. Le seul des trois blocs
    /// a etre recopie a <c>+0x71c0</c> et non a <c>+0x2ff0</c> : c'est un banc reordonne, pas un
    /// decalage constant, et cette adresse est la seule du lot a en venir.</summary>
    private static void EntityHandler_00425500(byte* e)
        => TickLabyrinthRisingMote(e, Descriptors00550350());          // Ghidra: &DAT_00550350

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_0041DF60"/> et de son jumeau. Absent de
    /// <c>Blaze.c</c> ; 84 instructions.
    ///
    /// <para>
    /// <b>Un grain projete dans le cap de l'entite, puis disperse.</b> La naissance monte d'abord de
    /// <c>0 a 0xc8</c> cases, puis pousse de <b>trente unites</b> le long du cap <c>+0x32</c>
    /// (sinus en X, cosinus en Z), puis ajoute un ecart au hasard de <c>0x3c</c> cases centre sur
    /// chacun des deux axes horizontaux. Les deux echelles sont tirees separement dans
    /// <c>[0x7e8, 0x914]</c> : le grain n'est pas carre.
    /// </para>
    /// <para>
    /// <b>Il ne bouge plus ensuite.</b> L'etat 1 ne fait qu'avancer la planche : une image sur trois,
    /// <c>+0x40</c> monte d'un cran, et le grain s'eteint quand il atteint 4 — la planche en a
    /// quatre, et le binaire ecrit ce quatrieme indice avant d'eteindre, dans la meme image.
    /// </para>
    /// </summary>
    private static void TickLabyrinthSpiralMote(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;

            *(int*)(e + 0x20) += (CrtRand() % 0xc8) << 0xc;

            // Ghidra: `lea eax,[eax+eax*2] ; lea eax,[eax+eax*4] ; shl eax,1` — x30.
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 30;
            *(int*)(e + 0x24) += (int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * 30;

            *(int*)(e + 0x1c) += (CrtRand() % 0x3c - 0x1e) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0x3c - 0x1e) << 0xc;

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x12c + 0x7e8);
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x12c + 0x7e8);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) == 3)
            {
                *(short*)(e + 0x12) = 0;

                byte frame = (byte)(e[0x40] + 1);
                e[0x40] = frame;

                if (frame == 4) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>Ghidra: <c>0x0041df60</c> — le grain projete, sous-zones 0 a 3, creneau
    /// <b>55</b>.</summary>
    private static void EntityHandler_0041DF60(byte* e)
        => TickLabyrinthSpiralMote(e, Descriptors0054FEC0());          // Ghidra: &DAT_0054fec0

    /// <summary>Ghidra: <c>0x00420f50</c> — le meme, sous-zones 5 a 8.</summary>
    private static void EntityHandler_00420F50(byte* e)
        => TickLabyrinthSpiralMote(e, Descriptors005500A8());          // Ghidra: &DAT_005500a8

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_0041E9D0"/> et de son jumeau. Absent de
    /// <c>Blaze.c</c> ; 89 instructions.
    ///
    /// <para>
    /// <b>Il apparait d'un cote ou de l'autre, attend, puis descend en accelerant.</b> Le cote est un
    /// pile ou face : <c>+0x1c</c> prend <c>+0x64000</c> ou <c>-0x64000</c>, rien entre les deux. Le
    /// decalage en Z, lui, est un tirage ordinaire de <c>0xc8</c> cases centre. Les <b>trois</b>
    /// echelles sont mises a <c>0x800</c> — le seul corps du lot a toucher <c>+0x2c</c>.
    /// </para>
    /// <para>
    /// <b>L'attente sort au hasard, comme la bouffee</b> : une chance sur cinq par image, et de toute
    /// facon passe la septieme. La sortie tire une vitesse de depart de <c>2 a 11</c> dans
    /// <c>+0x14</c> et retire le bit <c>0x4000</c>. Ici le binaire <b>ne rend pas la main</b> : la
    /// descente commence dans la meme image.
    /// </para>
    /// <para>
    /// <b>La descente accelere de deux par image</b> (<c>+0x14 += 2</c>), et la planche avance toutes
    /// les six images. Le grain s'eteint quand <c>+0x40</c> atteint 4, ce que la planche de quatre
    /// enregistrements couvre.
    /// </para>
    /// </summary>
    private static void TickLabyrinthHoveringMote(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
            *(short*)(e + 0x2c) = 0x800;
            e[0x0a] = 3;
            e[0x0b] = 0;

            // Ghidra: `lea eax,[eax+eax*4]` deux fois puis `shl eax,0Fh` — (tirage & 1) * 25 * 0x8000,
            // moins 0x64000 : le resultat ne peut valoir que -0x64000 ou +0x64000.
            *(int*)(e + 0x1c) += ((CrtRand() % 2) * 25 << 0xf) - 0x64000;

            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x24) += (CrtRand() % 0xc8 - 0x64) << 0xc;
            e[0x40] = 0;
            *(uint*)e |= 0x4000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (CrtRand() % 5 == 1 || *(short*)(e + 0x12) > 6)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x16) = 0;
                *(short*)(e + 0x14) = (short)(CrtRand() % 0xa + 2);
                *(uint*)e &= 0xffffbfff;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short speed = *(short*)(e + 0x14);
            *(int*)(e + 0x20) -= speed << 0xc;
            *(short*)(e + 0x14) = (short)(speed + 2);

            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) > 5)
            {
                *(short*)(e + 0x12) = 0;

                byte frame = (byte)(e[0x40] + 1);
                e[0x40] = frame;

                if (frame == 4) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>Ghidra: <c>0x0041e9d0</c> — le grain qui attend puis tombe, sous-zones 0 a 3,
    /// creneau <b>51</b>.</summary>
    private static void EntityHandler_0041E9D0(byte* e)
        => TickLabyrinthHoveringMote(e, Descriptors0054FEC0());        // Ghidra: &DAT_0054fec0

    /// <summary>Ghidra: <c>0x004219c0</c> — le meme, sous-zones 5 a 8.</summary>
    private static void EntityHandler_004219C0(byte* e)
        => TickLabyrinthHoveringMote(e, Descriptors005500A8());        // Ghidra: &DAT_005500a8

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_0041E7F0"/> et de son jumeau. Absent de
    /// <c>Blaze.c</c> ; 154 instructions.
    ///
    /// <para>
    /// <b>Une braise qui part dans un cap tire au hasard, freine, grossit et palit.</b> La naissance
    /// devie le cap <c>+0x32</c> de <c>±0x100</c>, se donne une vitesse de <c>0x14 a 0x1d</c> dans
    /// <c>+0x5c</c>, une montee de <c>0xa a 0x13</c> cases par image dans <c>+0x54</c>, une attente
    /// de <c>0 a 3</c> images dans <c>+0x12</c> et une duree d'image de planche de <c>2 a 6</c> dans
    /// <c>+0x14</c>. Elle descend d'abord de <c>0x32</c> cases, puis se decale de <c>0x50</c> cases
    /// centre sur les deux axes horizontaux, et sa teinte part de <c>0x64</c> sur les trois canaux.
    /// </para>
    /// <para>
    /// <b>Le binaire tire le hasard une fois pour rien</b> : deux <c>call 00547140h</c> se suivent a
    /// <c>0x0041e83d</c> et le premier resultat n'est jamais lu. Ce n'est pas un artefact du
    /// desassemblage — c'est un tirage perdu, qui decale la suite du flux. Il est reproduit ici.
    /// </para>
    /// <para>
    /// <b>La course perd deux unites de vitesse par image</b>, plancher a zero, et l'echelle gagne
    /// <c>0x96</c> par image sans borne. La teinte perd 8 par canal et par image par
    /// <see cref="BrightenEntityTint_00404DE0"/> ; c'est <b>elle</b> qui tue la braise, quand le canal
    /// rouge atteint zero. <c>+0x40</c> tourne de 0 a 4 sans fin — cinq enregistrements, exactement
    /// ce que la planche contient.
    /// </para>
    /// <para>
    /// <b>Et c'est ici que les deux exemplaires ne sont pas identiques.</b> Le bloc des sous-zones
    /// 0 a 3 <b>soustrait</b> le deplacement (<c>sub edx,eax</c>), celui des sous-zones 5 a 8
    /// l'<b>ajoute</b> (<c>add edx,eax</c>) : la braise part dans le cap d'un cote et dans le cap
    /// oppose de l'autre. Deux instructions sur 154, et le diff LOOSE ne les rapprochait pas.
    /// </para>
    /// </summary>
    /// <param name="forward">+1 quand le deplacement s'ajoute (sous-zones 5 a 8), -1 quand il se
    /// soustrait (sous-zones 0 a 3).</param>
    private static void TickLabyrinthEmber(byte* e, byte* plate, int forward)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;

            short scale = (short)(CrtRand() % 0x400 + 0x7d0);
            e[0x0a] = 6;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            CrtRand();                        // Ghidra: tirage PERDU, `call` sans lecture du retour
            *(short*)(e + 0x32) += (short)(CrtRand() % 0x200 - 0x100);

            *(int*)(e + 0x5c) = CrtRand() % 0xa + 0x14;
            *(int*)(e + 0x54) = (CrtRand() % 0xa + 0xa) << 0xc;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4);
            *(short*)(e + 0x16) = 0;
            *(uint*)e |= 0x4000;
            *(short*)(e + 0x14) = (short)(CrtRand() % 5 + 2);

            *(int*)(e + 0x20) += -0x32000;
            *(int*)(e + 0x1c) += (CrtRand() % 0x50 - 0x28) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0x50 - 0x28) << 0xc;

            e[0x38] = 0x64;
            e[0x39] = 0x64;
            e[0x3a] = 0x64;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))--;

            if (*(short*)(e + 0x12) <= 0)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0xffffbfff;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            int speed = *(int*)(e + 0x5c);

            *(int*)(e + 0x1c) += forward * ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * speed);
            *(int*)(e + 0x24) += forward * ((int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * speed);

            *(short*)(e + 0x28) += 0x96;
            *(short*)(e + 0x2a) += 0x96;

            int slowed = speed - 2;
            if (slowed < 0) slowed = 0;
            *(int*)(e + 0x5c) = slowed;

            *(int*)(e + 0x20) += *(int*)(e + 0x54);

            BrightenEntityTint_00404DE0(e, -8, -8, -8);                // Ghidra: FUN_00404de0

            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;

            (*(short*)(e + 0x16))++;
            if (*(short*)(e + 0x16) > *(short*)(e + 0x14))
            {
                *(short*)(e + 0x16) = 0;

                byte frame = (byte)(e[0x40] + 1);
                e[0x40] = frame;

                if (frame == 5) e[0x40] = 0;
            }
        }
    }

    /// <summary>Ghidra: <c>0x0041e7f0</c> — la braise, sous-zones 0 a 3, creneau <b>50</b>. Elle part
    /// dans le cap <b>soustrait</b>.</summary>
    private static void EntityHandler_0041E7F0(byte* e)
        => TickLabyrinthEmber(e, Descriptors0054FEE0(), -1);           // Ghidra: &DAT_0054fee0

    /// <summary>Ghidra: <c>0x004217e0</c> — la meme braise, sous-zones 5 a 8, <b>cap ajoute</b> :
    /// deux <c>sub edx,eax</c> devenus <c>add edx,eax</c>.</summary>
    private static void EntityHandler_004217E0(byte* e)
        => TickLabyrinthEmber(e, Descriptors005500C8(), 1);            // Ghidra: &DAT_005500c8

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_0041DA80"/> et de son jumeau. Decompilee dans
    /// <c>Blaze.c</c> ; 96 instructions, <b>deux planches</b>.
    ///
    /// <para>
    /// <b>Une nappe qui tombe puis se pose.</b> Elle nait mille cases au-dessus de sa position
    /// (<c>+0x3e8000</c>), a l'echelle <c>0x800</c>, et descend en accelerant : <c>+0x12</c> gagne 2
    /// par image et se retranche de la hauteur. La chute s'arrete a un plancher, et
    /// <b>c'est le drapeau 236 qui dit lequel</b>.
    /// </para>
    /// <list type="table">
    ///   <item><term>drapeau 236 absent</term><description>plancher <c>-0x12c000</c>, puis elle se
    ///     pose (etat 2)</description></item>
    ///   <item><term>drapeau 236 pose, <c>+0x14 == 0</c></term><description>plancher
    ///     <c>-0x190000</c> : elle disparait <b>sans se poser</b>, et la hauteur est quand meme
    ///     ramenee a <c>-0x12c000</c></description></item>
    ///   <item><term>drapeau 236 pose, <c>+0x14 != 0</c></term><description>plancher
    ///     <c>-0x1e0000</c>, puis elle se pose</description></item>
    /// </list>
    /// <para>
    /// <b>Se poser, c'est changer de planche.</b> L'etat 2 echange <paramref name="falling"/> pour
    /// <paramref name="settled"/>, remet <c>+0x40</c> a zero et tire une echelle de
    /// <c>[0x5dc, 0x9db]</c>. L'etat 3 palit de <c>0x32</c> par canal et par image, grandit de 400
    /// par image, et avance la planche une image sur trois — <c>+0x40</c> est <b>borne a 3</b>
    /// explicitement, ce qui est exactement la longueur de la planche posee. La nappe s'eteint quand
    /// son canal rouge atteint zero.
    /// </para>
    /// </summary>
    private static void TickLabyrinthFallingShroud(byte* e, byte* falling, byte* settled)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[0x0a] = 0x0e;
            *(int*)(e + 0x20) += 0x3e8000;
            e[0x0b] = 0;
            *(byte**)(e + 0x3c) = falling;
            e[0x40] = 0;
            *(short*)(e + 0x12) = 10;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 2;

            int y = *(int*)(e + 0x20) + *(short*)(e + 0x12) * -0x1000;
            *(int*)(e + 0x20) = y;

            if ((Mem.s_StateBank0_005e96e0[0x1d] & 0x10) == 0)
            {
                if (y < -0x12c000)
                {
                    *(short*)(e + 0x10) = 2;
                    *(int*)(e + 0x20) = -0x12c000;
                }
            }
            else if (*(short*)(e + 0x14) == 0)
            {
                if (y < -0x190000)
                {
                    *(int*)(e + 0x20) = -0x12c000;
                    *(uint*)e &= 0x7fffffff;
                }
            }
            else if (y < -0x1e0000)
            {
                *(short*)(e + 0x10) = 2;
                *(int*)(e + 0x20) = -0x1e0000;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            e[0x0a] = 0x0e;
            e[0x0b] = 0;
            *(byte**)(e + 0x3c) = settled;
            e[0x40] = 0;

            short scale = (short)(CrtRand() % 0x400 + 0x5dc);
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x12))++;

            BrightenEntityTint_00404DE0(e, -0x32, -0x32, -0x32);       // Ghidra: FUN_00404de0

            *(short*)(e + 0x28) += 400;
            *(short*)(e + 0x2a) += 400;

            if (*(short*)(e + 0x12) > 2)
            {
                *(short*)(e + 0x12) = 0;

                byte frame = (byte)(e[0x40] + 1);
                e[0x40] = frame;

                if (frame > 3) e[0x40] = 3;                            // borne, pas boucle
            }

            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>Ghidra: FUN_0041da80 @ 0x0041da80 — la nappe qui tombe, sous-zones 0 a 3, creneau
    /// <b>60</b>.</summary>
    private static void EntityHandler_0041DA80(byte* e)
        => TickLabyrinthFallingShroud(e, Descriptors00550008(), Descriptors0054FFE8());

    /// <summary>Ghidra: FUN_00420a70 @ 0x00420a70 — la meme, sous-zones 5 a 8.</summary>
    private static void EntityHandler_00420A70(byte* e)
        => TickLabyrinthFallingShroud(e, Descriptors005501F0(), Descriptors005501D0());

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_0041DBD0"/> et de son jumeau. Decompilee dans
    /// <c>Blaze.c</c> ; 116 instructions.
    ///
    /// <para>
    /// <b>Un flocon de cendre, et son role depend de la carte.</b> Dans la <b>sous-zone 3</b> il
    /// prend <c>+0x0b = 0xf4</c> et un <c>+0x0a</c> qui vaut <c>0x0b</c> plus le <b>nombre de bits
    /// poses parmi les quatre premiers de l'octet <c>0x005e96fe</c></b> (les drapeaux 240 a 243) :
    /// c'est le seul corps du lot dont l'apparence se lit dans l'etat de la partie. Partout ailleurs
    /// c'est <c>+0x0a = 9</c>, <c>+0x0b = 0</c>.
    /// </para>
    /// <para>
    /// <b>Il est projete dans le cap de l'entite, avec une portee tiree au hasard</b> — jusqu'a 99
    /// unites le long de <c>+0x32</c> —, puis disperse de <c>0x32</c> cases centre en X et en Z et
    /// monte de <c>0 a 0x1d</c> cases. Son echelle part de <c>[0x125c, 0x165b]</c> et sa teinte de
    /// <c>0x80</c> sur les trois canaux.
    /// </para>
    /// <para>
    /// <b>Son image de planche est tiree A CHAQUE IMAGE</b> — <c>+0x40 = rand() % 4</c> — et non
    /// avancee : le flocon scintille au lieu de s'animer. Il palit de 7 par canal et par image, et
    /// vit le nombre d'images tire dans <c>+0x12</c>, de 5 a 9.
    /// </para>
    /// </summary>
    private static void TickLabyrinthAshFleck(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;

            byte flags = Mem.s_StateBank0_005e96e0[0x1e];              // Ghidra: DAT_005e96fe
            sbyte set = (sbyte)((flags & 1) != 0 ? 1 : 0);
            if ((flags & 2) != 0) set++;
            if ((flags & 4) != 0) set++;
            if ((flags & 8) != 0) set++;

            if (s_Unknown_005de52c == 3)
            {
                e[0x0b] = 0xf4;
                e[0x0a] = (byte)(set + 0x0b);
            }
            else
            {
                e[0x0a] = 9;
                e[0x0b] = 0;
            }

            *(short*)(e + 0x10) = 1;

            short scale = (short)(CrtRand() % 0x400 + 0x125c);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)(CrtRand() % 5 + 5);

            int reach = (short)(CrtRand() % 100);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * reach;
            *(int*)(e + 0x24) += (int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * reach;

            *(int*)(e + 0x1c) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x1e) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x32 - 0x19) * 0x1000;

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            e[0x40] = (byte)(CrtRand() % 4);

            BrightenEntityTint_00404DE0(e, -7, -7, -7);                // Ghidra: FUN_00404de0

            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) < 1) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>Ghidra: FUN_0041dbd0 @ 0x0041dbd0 — le flocon de cendre, sous-zones 0 a 3, creneau
    /// <b>59</b>.</summary>
    private static void EntityHandler_0041DBD0(byte* e)
        => TickLabyrinthAshFleck(e, Descriptors0054FFC8());            // Ghidra: &DAT_0054ffc8

    /// <summary>Ghidra: FUN_00420bc0 @ 0x00420bc0 — le meme, sous-zones 5 a 8.</summary>
    private static void EntityHandler_00420BC0(byte* e)
        => TickLabyrinthAshFleck(e, Descriptors005501B0());            // Ghidra: &DAT_005501b0

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_0041DD40"/> et de son jumeau. Decompilee dans
    /// <c>Blaze.c</c> ; 163 instructions, <b>deux planches choisies par <c>+0x16</c></b>.
    ///
    /// <para>
    /// <b>Une etincelle d'eventail, en trois eventails possibles.</b> Le cap de reference est
    /// <c>+0x32 + 0x800</c> — un demi-tour —, et la naissance pousse de 200 unites dans ce cap. Un
    /// tirage a trois branches decide ensuite de l'ecart :
    /// </para>
    /// <list type="table">
    ///   <item><term>0</term><description>ecart perpendiculaire tire dans <c>[-200, 199]</c> sur
    ///     chacun des deux axes, et montee de <c>0 a 9</c> cases</description></item>
    ///   <item><term>1</term><description>ecart fixe de <c>+200</c> perpendiculaire, <c>+0x19 * 8</c>
    ///     dans le cap, montee de <c>0 a 199</c> cases</description></item>
    ///   <item><term>2</term><description>le meme, en negatif</description></item>
    /// </list>
    /// <para>
    /// <b>La planche et la duree de vie dependent de <c>+0x16</c></b>, qui vient du placement :
    /// <c>0</c> donne <paramref name="shortPlate"/> (trois images, mort a la quatrieme),
    /// autre chose donne <paramref name="longPlate"/> (six images, mort a la septieme). La duree
    /// d'une image est tiree de 3 a 5 dans <c>+0x14</c>.
    /// </para>
    /// <para>
    /// <b>Le compteur <c>+0x12</c> n'est jamais remis a zero</b> : passe le seuil, l'etincelle avance
    /// d'une image de planche a <b>chaque</b> image, et non plus une fois sur trois. C'est le
    /// binaire, pas une omission.
    /// </para>
    /// </summary>
    private static void TickLabyrinthSparkFan(byte* e, byte* shortPlate, byte* longPlate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = *(short*)(e + 0x16) == 0 ? shortPlate : longPlate;
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;

            short scale = (short)(CrtRand() % 0x400 + 0x708);
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(short*)(e + 0x14) = (short)(CrtRand() % 3 + 3);

            int branchRoll = CrtRand();                                // tire AVANT les deux poussees
            int heading = *(short*)(e + 0x32) + 0x800;

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 200;
            short branch = (short)(branchRoll % 3);
            *(int*)(e + 0x24) += (int)Winsys3d.GetCosTableEntry(heading) * 200;

            int riseRoll, riseMod;

            if (branch == 0)
            {
                *(int*)(e + 0x1c) += (int)Winsys3d.GetCosTableEntry(heading) * (CrtRand() % 400 - 200);
                *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(heading) * (CrtRand() % 400 - 200);
                riseRoll = CrtRand();
                riseMod = 10;
            }
            else
            {
                int along;

                if (branch == 1)
                {
                    *(int*)(e + 0x1c) += (int)Winsys3d.GetCosTableEntry(heading) * 200;
                    along = (int)Winsys3d.GetSinTableEntry(heading) * 0x19;
                }
                else
                {
                    *(int*)(e + 0x1c) += (int)Winsys3d.GetCosTableEntry(heading) * -200;
                    along = (int)Winsys3d.GetSinTableEntry(heading) * -0x19;
                }

                *(int*)(e + 0x24) += along * 8;
                riseRoll = CrtRand();
                riseMod = 200;
            }

            *(int*)(e + 0x20) += (riseRoll % riseMod) * 0x1000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x14) <= *(short*)(e + 0x12))
            {
                byte frame = (byte)(e[0x40] + 1);
                e[0x40] = frame;

                if (frame > 2 && *(short*)(e + 0x16) == 0) *(uint*)e &= 0x7fffffff;
                if (frame > 5 && *(short*)(e + 0x16) != 0) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>Ghidra: FUN_0041dd40 @ 0x0041dd40 — l'etincelle d'eventail, sous-zones 0 a 3, creneau
    /// <b>56</b>.</summary>
    private static void EntityHandler_0041DD40(byte* e)
        => TickLabyrinthSparkFan(e, Descriptors0054FEA8(), Descriptors0054FF20());

    /// <summary>Ghidra: FUN_00420d30 @ 0x00420d30 — la meme, sous-zones 5 a 8.</summary>
    private static void EntityHandler_00420D30(byte* e)
        => TickLabyrinthSparkFan(e, Descriptors00550090(), Descriptors00550108());

    // =====================================================================
    // LOT 3 (2026-09-17) — LE BANC DES SOUS-ZONES 4 ET 9, ET SES PARENTS HORS ZONE
    //
    // Le banc des sous-zones 4 et 9 n'est PAS le banc 0 a 3 recopie : sur ses trente corps, cinq
    // seulement ont un parent proche, et ces parents vivent pour la plupart dans d'AUTRES ZONES —
    // le chateau de l'immortel (6) et la grotte scellee (10). Ecrire un corps ici en ferme donc
    // deux ou quatre, repartis sur trois zones.
    //
    // Les jumeaux hors zone n'ont pas encore de fichier a eux (il n'existe ni
    // `ImmortalCastleParticles.cs` ni `SealedCaveParticles.cs`). Leur point d'entree reste donc
    // aupres du corps, comme le veut la regle 4 d'ARCHITECTURE.md ; il suivra le jour ou ces
    // fichiers seront ouverts.
    //
    // La planche de chaque corps est relevee dans `Blaze.exe` et bornee deux fois : par l'adresse
    // connue suivante ET par l'indice que le corps mene en +0x40.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00550448</c> — <b>la luciole des sous-zones 4 et 9</b>, trois
    /// tuiles <c>0x0f x 0x0f</c> de la page <c>0x10</c>. Bornee par <c>0x00550460</c>, ou les memes
    /// trois octets recommencent pour une autre table ; <see cref="TickTwoHostOrbitMote"/> fait
    /// tourner <c>+0x40</c> de 0 a 2.</summary>
    private static readonly byte[] s_Descriptors_00550448 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550448  <- 0x00422C60
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550450
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550458
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00551500</c> — la meme luciole, <b>grotte scellee</b>. Memes
    /// octets que <see cref="s_Descriptors_00550448"/> ; bornee par <c>0x00551518</c>, ou une autre
    /// forme commence (<c>0xc0 0xa8</c>).</summary>
    private static readonly byte[] s_Descriptors_00551500 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551500  <- 0x0043FA90
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551508
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551510
    ];

    private static byte* s_Descriptors00550448Storage;
    private static byte* s_Descriptors00551500Storage;

    private static byte* Descriptors00550448() => NativeCopy(s_Descriptors_00550448, ref s_Descriptors00550448Storage);
    private static byte* Descriptors00551500() => NativeCopy(s_Descriptors_00551500, ref s_Descriptors00551500Storage);

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_00422C60"/> (Labyrinthe, sous-zones 4 et 9) et de
    /// <see cref="EntityHandler_0043FA90"/> (grotte scellee). Absent de <c>Blaze.c</c> ;
    /// 74 instructions, et le diff ne rend <b>que la planche</b>.
    ///
    /// <para>
    /// <b>Une luciole accrochee a DEUX porteurs a la fois.</b> <c>+0x74</c> donne la position de
    /// reference, <c>+0x6c</c> donne un decalage ; l'un et l'autre, s'ils sont nuls, retombent sur
    /// <see cref="Resources.s_EntityList_005de558"/> — le bloc de trente-neuf mots remis a zero a
    /// chaque chargement, qui sert ici de porteur muet. C'est ce qui evite le dereferencement nul
    /// sans aucun test dans le reste du corps.
    /// </para>
    /// <para>
    /// <b>Elle tourne autour d'eux.</b> Sa position est recalculee <b>a chaque image</b> :
    /// <c>Y</c> est la somme des deux porteurs, <c>X</c> et <c>Z</c> y ajoutent quinze unites dans
    /// le cap <c>+0x5c + +0x32 - 0xc00</c> — un cap qui vient donc pour partie de l'entite et pour
    /// partie du porteur.
    /// </para>
    /// <para>
    /// <b>Elle vit onze images et son image de depart est tiree au hasard</b> (<c>rand() % 3</c>),
    /// puis la planche tourne d'un cran par image en boucle sur trois. L'echelle est <c>0xfa0</c>.
    /// </para>
    /// </summary>
    private static void TickTwoHostOrbitMote(byte* e, byte* plate)
    {
        byte* anchor = *(byte**)(e + 0x74);
        if (anchor == null) anchor = Res.s_EntityList_005de558;       // Ghidra: 5de558h

        byte* offset = *(byte**)(e + 0x6c);
        if (offset == null) offset = Res.s_EntityList_005de558;

        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + *(int*)(offset + 0x54);

        // Ghidra: `lea eax,[eax+eax*2] ; lea ecx,[eax+eax*4]` — x15.
        int heading = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(heading) * 15
                          + *(int*)(anchor + 0x1c) + *(int*)(offset + 0x50);

        heading = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;
        *(int*)(e + 0x24) = (int)Winsys3d.GetCosTableEntry(heading) * 15
                          + *(int*)(anchor + 0x24) + *(int*)(offset + 0x58);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;

            byte start = (byte)(CrtRand() % 3);
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0xfa0;
            *(short*)(e + 0x2a) = 0xfa0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x0b;
            e[0x40] = start;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            byte frame = (byte)(e[0x40] + 1);
            (*(short*)(e + 0x12))--;
            short left = *(short*)(e + 0x12);

            e[0x40] = frame;
            if (frame >= 3) e[0x40] = 0;                              // Ghidra: `cmp cl,3 ; jb` — NON signe

            if (left <= 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>Ghidra: <c>0x00422c60</c> — la luciole, Labyrinthe des morts, sous-zones 4 et
    /// 9.</summary>
    private static void EntityHandler_00422C60(byte* e)
        => TickTwoHostOrbitMote(e, Descriptors00550448());            // Ghidra: &DAT_00550448

    /// <summary>Ghidra: <c>0x0043fa90</c> — la meme luciole, <b>grotte scellee</b> (zone 10). Elle
    /// est ecrite ici parce que c'est le Labyrinthe qui a fait lire le corps ; elle rejoindra un
    /// fichier de sa zone le jour ou il en existera un.</summary>
    private static void EntityHandler_0043FA90(byte* e)
        => TickTwoHostOrbitMote(e, Descriptors00551500());            // Ghidra: &DAT_00551500

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_00422D50"/> (Labyrinthe, sous-zones 4 et 9) et de
    /// <see cref="EntityHandler_0042A440"/> (chateau de l'immortel). Absent de <c>Blaze.c</c> ;
    /// 77 instructions, et le diff ne rend <b>que le sous-type engendre</b>.
    ///
    /// <para>
    /// <b>Une invocation en onze exemplaires, et l'ecran qui tremble pendant trente images.</b> La
    /// naissance joue le son <c>0x2c</c> de la sous-zone, tire un cote dans <c>+0x44</c> (0 ou 1,
    /// rien entre les deux) et fait naitre <b>onze</b> entites de sous-type
    /// <paramref name="spawn"/> d'un coup, par <see cref="SpawnCastEntity_0047AAE0"/>.
    /// </para>
    /// <para>
    /// <b>Trois images de plus en font naitre une chacune</b> — la 0, la 3 et la 7 —, avec
    /// <c>+0x5c</c> remis a zero et <c>+0x16</c> qui vaut 0 la premiere fois et 1 les deux autres.
    /// C'est le seul champ qui distingue les trois.
    /// </para>
    /// <para>
    /// <b>Le tremblement est ecrit directement dans le voile plein cadre</b>
    /// (<see cref="GameMemory.s_Unknown_005de024"/>) : l'octet de poids fort a 1, les deux du
    /// milieu a zero, et l'octet bas qui alterne <c>0x1e</c> / <c>0</c> une image sur deux. Trente
    /// images plus tard l'entite s'eteint — mais <b>elle ne rabaisse pas le voile</b> : c'est a un
    /// autre de le faire.
    /// </para>
    /// </summary>
    private static void TickSummonBurstShake(byte* e, byte spawn)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2c), 0x300, 0x2a00, 0, 0);   // Ghidra: FUN_00468080

            *(int*)(e + 0x5c) = 1;
            *(int*)(e + 0x44) = CrtRand() % 2;

            for (int remaining = 0x0b; remaining != 0; remaining--)
                SpawnCastEntity_0047AAE0(e, spawn, 0, -1);            // Ghidra: FUN_0047aae0
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short tick = *(short*)(e + 0x12);

            if (tick == 0 || tick == 3 || tick == 7)
            {
                *(int*)(e + 0x5c) = 0;
                *(short*)(e + 0x16) = (short)(tick == 0 ? 0 : 1);
                SpawnCastEntity_0047AAE0(e, spawn, 0, -1);
            }

            (*(short*)(e + 0x12))++;

            // Ghidra: DAT_005de027 = 1 ; DAT_005de024 = 0x1e ou 0 ; DAT_005de025 = DAT_005de026 = 0.
            Mem.s_Unknown_005de024 = 0x01000000u
                                   | (uint)((*(short*)(e + 0x12) & 1) != 0 ? 0x1e : 0);

            if (*(short*)(e + 0x12) >= 0x1e) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>Ghidra: <c>0x00422d50</c> — l'invocation, Labyrinthe des morts, sous-zones 4 et 9 ;
    /// sous-type <c>0xdf</c>.</summary>
    private static void EntityHandler_00422D50(byte* e) => TickSummonBurstShake(e, 0xdf);

    /// <summary>Ghidra: <c>0x0042a440</c> — la meme, <b>chateau de l'immortel</b> (zone 6) ;
    /// sous-type <c>0xde</c>. Seule difference sur 77 instructions.</summary>
    private static void EntityHandler_0042A440(byte* e) => TickSummonBurstShake(e, 0xde);

    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_00422330"/> (Labyrinthe, sous-zones 4 et 9),
    /// <see cref="EntityHandler_00429A20"/> (chateau), <see cref="EntityHandler_00436790"/> et
    /// <see cref="EntityHandler_0043F320"/> (grotte scellee). Absent de <c>Blaze.c</c> ;
    /// 125 instructions, et le diff ne rend <b>que le sous-type engendre</b> : quatre corps, une
    /// lecture, trois zones.
    ///
    /// <para>
    /// <b>La mort d'un porteur, en trois temps.</b> L'entite se recolle a chaque image sur la
    /// position de son porteur <c>+0x6c</c> — ou sur
    /// <see cref="Resources.s_EntityList_005de558"/> s'il est nul — et <b>meurt avec lui</b> : elle
    /// s'eteint des que le porteur porte le bit <c>0x40000000</c> ou perd le bit <c>0x80000000</c>.
    /// </para>
    /// <para>
    /// <b>Vingt images de preparation</b> (etat 1), annoncees par le son <c>0x10</c> a gain
    /// <c>0x1300</c>. Pendant tout ce temps, <b>a chaque image</b>, elle se reconfigure entierement —
    /// cap tire dans <c>[0x400, 0x7ff]</c>, orientation <c>+0x34</c> tiree sur un tour complet,
    /// echelles <c>0x1fff</c> / <c>0</c> / <c>0x100</c>, <c>+0x48</c> a <c>0x50000</c> — et fait
    /// naitre une entite de sous-type <c>0x70</c>. Vingt images, vingt naissances.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est l'explosion, et il ne dure qu'une image</b> : deux entites
    /// <paramref name="spawn"/> partent, l'une avec <c>+0x5c = +0x400</c> et l'autre avec
    /// <c>-0x400</c>, puis un son selon <c>+0x98</c>. L'etat 3 compte trente images et eteint.
    /// </para>
    /// <para>
    /// <b>La branche <c>+0x98 == 1</c> est morte dans le cours normal.</b> L'etat 1 met
    /// <c>+0x98</c> a zero en passant la main, et l'etat 2 s'execute dans la meme image : le son
    /// <c>0x1c</c> ne peut partir que si quelqu'un d'autre a pose 1 dans <c>+0x98</c> entre-temps.
    /// C'est transcrit tel quel, pas simplifie.
    /// </para>
    /// </summary>
    private static void TickHostDeathBurst(
        byte* e, byte spawn, AbsentHost absent = AbsentHost.FallBackOnEntityList)
    {
        byte* host = *(byte**)(e + 0x6c);

        // Ghidra: le seul point ou les trois blocs de la zone different — voir AbsentHost.
        if (host != null || absent != AbsentHost.SkipTracking)
        {
            byte* source = host == null && absent == AbsentHost.FallBackOnEntityList
                ? Res.s_EntityList_005de558                               // Ghidra: 5de558h
                : host;

            *(int*)(e + 0x1c) = *(int*)(source + 0x1c);
            *(int*)(e + 0x20) = *(int*)(source + 0x20);
            *(int*)(e + 0x24) = *(int*)(source + 0x24);

            uint hostFlags = *(uint*)source;

            if ((hostFlags & 0x40000000) != 0 || (hostFlags & 0x80000000) == 0)
            {
                *(uint*)e &= 0x7fffffff;
                return;
            }
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0x14;
            *(byte**)(e + 0x74) = host;                               // le pointeur BRUT, nul compris

            TryPlaySoundNearEntity((uint*)e, 0x10, 0xdfa, 0x1300, 0, 0);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x16))--;
            if (*(short*)(e + 0x16) <= 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x98) = 0;
            }

            // Ghidra: LAB_004223be — hors du `if`, il tourne a CHAQUE image de l'etat 1.
            *(byte**)(e + 0x70) = *(byte**)(e + 0x6c);
            e[0x04] = 0x3f;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0x50000;
            *(int*)(e + 0x4c) = 0;

            *(short*)(e + 0x32) = (short)(CrtRand() % 0x400 + 0x400);
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x34) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x12) = 0xc8;
            *(short*)(e + 0x28) = 0x1fff;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0x100;

            SpawnCastEntity_0047AAE0(e, 0x70, 0, -1);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            *(short*)(e + 0x16) = 0x1e;

            *(int*)(e + 0x5c) = 0x400;
            SpawnCastEntity_0047AAE0(e, spawn, 0, -1);

            *(int*)(e + 0x5c) = -0x400;
            SpawnCastEntity_0047AAE0(e, spawn, 0, -1);

            if (*(short*)(e + 0x98) == 1)
            {
                TryPlaySoundNearEntity((uint*)e, 0x1c, 0x1000, 0x1300, 0, 0);
                (*(short*)(e + 0x98))++;
            }

            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, 0x15, 0x800, 0x1300, 0, 0);
                (*(short*)(e + 0x98))++;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x16))--;
            if (*(short*)(e + 0x16) < 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>Ghidra: <c>0x00422330</c> — la mort du porteur, Labyrinthe des morts, sous-zones 4
    /// et 9 ; sous-type <c>0xde</c>.</summary>
    private static void EntityHandler_00422330(byte* e) => TickHostDeathBurst(e, 0xde);

    /// <summary>Ghidra: <c>0x00429a20</c> — la meme, <b>chateau de l'immortel</b> ; sous-type
    /// <c>0xe0</c>.</summary>
    private static void EntityHandler_00429A20(byte* e) => TickHostDeathBurst(e, 0xe0);

    /// <summary>Ghidra: <c>0x00436790</c> — la meme, <b>grotte scellee</b> ; sous-type
    /// <c>0xdf</c>.</summary>
    private static void EntityHandler_00436790(byte* e) => TickHostDeathBurst(e, 0xdf);

    /// <summary>Ghidra: <c>0x0043f320</c> — la meme, <b>grotte scellee</b>, second bloc ; sous-type
    /// <c>0xe0</c>.</summary>
    private static void EntityHandler_0043F320(byte* e) => TickHostDeathBurst(e, 0xe0);

    /// <summary>
    /// Ghidra: <c>0x0041c740</c> — creneau <b>30</b> du bloc <c>0x00557da8</c> (<c>0x0056b0c8</c>,
    /// sous-zones 0 a 3) ; sous-type <c>0xde</c>. <b>Absente de <c>Blaze.c</c></b>, 123
    /// instructions, dump sous <c>Legacy/Raw/Blaze/FUN_0041C740.asm</c> ; lot 8, 2026-09-18.
    /// <see cref="EntityHandler_00422330"/> a <b>une</b> difference pres : sans porteur, le
    /// <c>je</c> de tete saute a <c>0x0041c77b</c> — le debut de la machine a etats, <b>pas</b> la
    /// sortie. L'entite ne se recale ni ne teste la vie de personne, et joue ses trois temps quand
    /// meme.
    /// </summary>
    private static void EntityHandler_0041C740(byte* e) => TickHostDeathBurst(e, 0xde, AbsentHost.SkipTracking);

    /// <summary>
    /// Ghidra: <c>0x0041ec20</c> — creneau <b>30</b> du bloc <c>0x00558428</c> (<c>0x0056b0c8</c>,
    /// sous-zones 5 a 8) ; sous-type <c>0xde</c>. <b>Absente de <c>Blaze.c</c></b>, 121
    /// instructions, dump sous <c>Legacy/Raw/Blaze/FUN_0041EC20.asm</c>. La jumelle de
    /// <see cref="EntityHandler_0041C740"/> <b>sans aucun test</b> du porteur : il est lu tel quel.
    /// </summary>
    private static void EntityHandler_0041EC20(byte* e) => TickHostDeathBurst(e, 0xde, AbsentHost.Unchecked);

    /// <summary>
    /// Ce qu'un corps du Labyrinthe fait de son porteur <c>+0x6c</c> quand il est <b>nul</b>. Les
    /// trois blocs de sous-zones ont ete compiles chacun a sa maniere, et c'est la seule
    /// difference entre des corps par ailleurs identiques. <b>Attention, ce n'est pas une regle de
    /// bloc</b> : les nuages de <see cref="EntityHandler_0041D1A0"/>, meme creneau voisin, font en
    /// sous-zones 0 a 3 une quatrieme chose — <b>sortir</b> —, et ne passent donc pas par ce type.
    /// Lire le <c>je</c> de tete de chaque corps, ne pas le deduire de son bloc.
    /// </summary>
    private enum AbsentHost
    {
        /// <summary>Sous-zones 4 et 9, et les parents du chateau et de la grotte scellee : le
        /// porteur nul devient <c>Res.s_EntityList_005de558</c>.</summary>
        FallBackOnEntityList,

        /// <summary>Sous-zones 0 a 3 : on saute ce qui lit le porteur, et on continue.</summary>
        SkipTracking,

        /// <summary>Sous-zones 5 a 8 : aucun test ; un porteur nul serait une faute de page dans
        /// le binaire, une violation d'acces ici — a la meme lecture.</summary>
        Unchecked,

        /// <summary><c>0x0041C900</c> (sous-zones 0 a 3) : le porteur nul devient un
        /// enregistrement de <c>0x9c</c> octets <b>mis a zero sur la pile</b> a chaque image
        /// (<c>rep stosd</c>) — la conduite des fins de la vieille tour. Ajoute au lot 10.</summary>
        FallBackOnBlankRecord,

        /// <summary><c>0x0041D310</c> (sous-zones 0 a 3) : un pointeur nul fait <b>sortir</b> le
        /// corps sans rien faire — la conduite des nuages de ces sous-zones. Ajoute au lot 11.</summary>
        Abandon,
    }

    // =====================================================================
    // LOT 6 (2026-09-18) — LA CATEGORIE B DU LABYRINTHE, VIDEE
    //
    // Neuf des douze derniers « B » de la zone 5. Les trois autres ne sont pas des effets : deux
    // lancent un script de zone (`Handlers/LabyrinthMecanismes.cs`) et le troisieme est un VERBE de
    // script, pas un gestionnaire d'entite (`WorldMapScript.cs`).
    //
    // Les blocs de la zone dans les trois tables d'effets :
    //
    //   0x0056b0c8   0x00557da8 (0-3)   0x00558428 (5-8)   0x00558aa8 (4,9)
    //   0x0056b1c8   0x00557ea8 (0-3)   0x00558528 (5-8)   0x00558ba8 (4,9)
    //   0x0056b2c8   0x00557fa8 (0-3)   0x00558628 (5-8)   0x00558ca8 (4,9)
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00552690</c> — <b>le script de sons differes</b> que
    /// <see cref="EntityHandler_00483C50"/> met en file a sa mort. <b>Trois</b> enregistrements de
    /// huit octets, et la borne est double : le troisieme porte un <b>delai nul</b> — la marque de
    /// fin que decrit <see cref="s_ExplosionSoundScript_00552638"/> — et <c>0x005526a8</c>, qui
    /// suit, est le script d'un autre site d'appel.</summary>
    private static readonly byte[] s_DelayedSoundScript_00552690 =
    [
        0x15, 0x01, 0x01, 0x00, 0x6a, 0x01, 0x00, 0x18,   // 0x00552690
        0x15, 0x01, 0x01, 0x00, 0x7f, 0x01, 0x00, 0x18,   // 0x00552698
        0x1a, 0x00, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x005526a0 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript00552690Storage;

    /// <summary>Ghidra: <c>&amp;DAT_00550060</c> — la planche de
    /// <see cref="EntityHandler_0041D090"/>, sous-zones 0 a 3. <b>Trois</b> enregistrements : le
    /// corps tire <c>+0x40</c> dans <c>[0, 3[</c>, et <c>0x00550078</c> — deja releve — change de
    /// forme.</summary>
    private static readonly byte[] s_Descriptors_00550060 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550060  <- 0x0041D090
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550068
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550070
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550268</c> — la meme planche pour
    /// <see cref="EntityHandler_0041F520"/>, sous-zones 5 a 8. <b>Memes octets</b> que
    /// <see cref="s_Descriptors_00550060"/>, meme longueur, et <c>0x00550280</c> — deja releve —
    /// borne la plage.</summary>
    private static readonly byte[] s_Descriptors_00550268 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550268  <- 0x0041F520
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550270
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550278
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550360</c> — la planche de
    /// <see cref="EntityHandler_00425410"/>, sous-zones 4 et 9. <b>Quatre</b> enregistrements, et
    /// <b>la borne vient du corps, pas des octets</b> : <c>0x00550380</c> porte un cinquieme
    /// enregistrement de la <i>meme</i> forme (seuls les U/V changent), donc rien dans la donnee ne
    /// dit ou la table s'arrete. C'est <c>+0x40</c>, qui compte 0, 1, 2, 3 puis tue l'entite, qui
    /// le dit.</summary>
    private static readonly byte[] s_Descriptors_00550360 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550360  <- 0x00425410
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550368
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550370
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550378
    ];

    private static byte* s_Descriptors00550060Storage;
    private static byte* s_Descriptors00550268Storage;
    private static byte* s_Descriptors00550360Storage;

    private static byte* Descriptors00550060() => NativeCopy(s_Descriptors_00550060, ref s_Descriptors00550060Storage);
    private static byte* Descriptors00550268() => NativeCopy(s_Descriptors_00550268, ref s_Descriptors00550268Storage);
    private static byte* Descriptors00550360() => NativeCopy(s_Descriptors_00550360, ref s_Descriptors00550360Storage);

    /// <summary>
    /// Ghidra: FUN_00483c50 @ 0x00483c50 — creneau <b>28</b> du bloc <c>0x00558aa8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 36 instructions : le
    /// plus court gestionnaire encore a ecrire de la zone.
    ///
    /// <para>
    /// <b>Elle ne vit qu'une image : elle se transforme et meurt.</b> Le corps n'a ni etat ni garde
    /// — il s'execute en entier a chaque appel et finit par <c>*e = 0</c>, ce qui libere
    /// l'enregistrement. Tout ce qu'il fait avant, c'est <b>preparer ce que l'enfant heritera</b> :
    /// le porteur <c>+0x6c</c> recopie en <c>+0x74</c>, la hauteur reprise de <c>+0x64</c>, les
    /// echelles et la teinte a zero, <c>+0x2e</c> a <c>-0x40</c>, <c>+0x36</c> a 4, et le genre
    /// <c>0x23</c> en <c>+4</c>.
    /// </para>
    /// <para>
    /// <b>Le sous-type de l'enfant vient de la sous-zone, pas du code</b> : c'est l'entree
    /// <c>0x13</c> de la table de sauts que <c>+0xd0</c> du repertoire designe, plus <c>0xc0</c>.
    /// Un numero de sous-type ne veut rien dire tout seul — la table est rechargee par sous-zone.
    /// </para>
    /// <para>
    /// <b>Et elle met un script de sons en file avant de disparaitre</b>
    /// (<see cref="Audio.QueueDelayedSoundScript"/>, <c>&amp;DAT_00552690</c>), a sa propre
    /// position : les trois sons partiront apres elle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483C50(byte* e)
    {
        *(uint*)(e + 0x74) = *(uint*)(e + 0x6c);
        *(short*)(e + 0x36) = 4;
        *(uint*)(e + 0x28) = 0;
        *(short*)(e + 0x2c) = 0;
        *(uint*)(e + 0x38) = 0;
        *(short*)(e + 0x2e) = -0x40;
        *(uint*)(e + 0x5c) = 0;
        *(uint*)(e + 0x20) = *(uint*)(e + 0x64);
        e[0x04] = 0x23;

        // Ghidra: FUN_0047aae0(param_1, *(byte *)(**(int **)(DAT_005e97cc + 0xd0) + 0x13) + 0xc0, 0, 4)
        byte* castTable = *(byte**)*(byte**)(s_Unknown_005e97cc + 0xd0);
        SpawnCastEntity_0047AAE0(e, (byte)(castTable[0x13] + 0xc0), 0, 4);

        Aud.QueueDelayedSoundScript(
            NativeCopy(s_DelayedSoundScript_00552690, ref s_DelayedSoundScript00552690Storage),
            (int*)(e + 0x1c));                                      // Ghidra: FUN_0053dbe0

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00483e90</c> — creneau <b>28</b> du bloc <c>0x00558ba8</c>
    /// (<c>0x0056b1c8</c>, sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b> ; relevee au
    /// desassembleur, <b>37 instructions</b>, dump conserve dans
    /// <c>Legacy/Raw/Blaze/FUN_00483E90.asm</c>.
    ///
    /// <para>
    /// <b>Un halo qui s'ouvre, monte, s'allume puis s'eteint.</b> A la naissance il tourne d'un
    /// quart de tour sur les deux axes (<c>+0x32</c> et <c>+0x34</c>, <c>+0x400</c> chacun), part
    /// d'une <b>largeur nulle</b> avec les deux autres echelles a <c>0x2000</c>, d'une teinte
    /// <b>noire</b>, et prend le genre <c>0x35</c>.
    /// </para>
    /// <para>
    /// <b>Sa largeur <c>+0x28</c> s'approche de <c>0x3000</c> d'un seizieme de l'ecart par
    /// image</b> — une ouverture qui ralentit sans jamais finir — et sa hauteur monte de
    /// <c>0x14000</c>, soit vingt cases, a chaque image. Ni l'une ni l'autre n'a de fin.
    /// </para>
    /// <para>
    /// <b>La teinte est le minuteur, et elle sert dans les deux sens.</b> Le bit
    /// <c>0x2000000</c> du mot d'etat separe les deux moities : sans lui, les trois canaux montent
    /// ensemble de <c>0x080808</c> par image, et le bit est pose quand ils atteignent
    /// <c>0x808080</c> — gris moyen ; avec lui, ils redescendent du meme pas, et a <b>zero</b>
    /// l'enregistrement est libere (<c>*e = 0</c>). <b>Seize images de montee, seize de descente.</b>
    /// </para>
    /// <para>
    /// Les trois canaux sont manipules comme <b>un seul double mot</b> : c'est ce qui rend le pas
    /// <c>0x080808</c> et la comparaison a <c>0x808080</c> exacts, sans retenue d'un canal sur
    /// l'autre.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483E90(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x32) += 0x400;
            *(short*)(e + 0x34) += 0x400;

            // Ghidra: `mov dword [eax+28h],20000000h` — +0x28 a zero et +0x2a a 0x2000 d'un coup.
            *(uint*)(e + 0x28) = 0x20000000;
            *(short*)(e + 0x2c) = 0x2000;

            *(uint*)(e + 0x38) = 0;
            e[0x04] = 0x35;
        }

        short width = *(short*)(e + 0x28);
        *(short*)(e + 0x28) = (short)(width + ((0x3000 - width) >> 4));

        *(int*)(e + 0x20) += 0x14000;

        uint word = *(uint*)e;

        if ((word & 0x2000000) != 0)
        {
            uint dim = *(uint*)(e + 0x38) + 0xfff7f7f8;   // Ghidra: `add ecx,0FFF7F7F8h` — -0x080808
            *(uint*)(e + 0x38) = dim;

            if (dim == 0) *(uint*)e = 0;
            return;
        }

        uint lit = *(uint*)(e + 0x38) + 0x80808;
        *(uint*)(e + 0x38) = lit;

        if (lit == 0x808080) *(uint*)e = word | 0x2000000;
    }

    /// <summary>
    /// Ghidra: <c>0x004840e0</c> — creneau <b>27</b> du bloc <c>0x00558ba8</c>
    /// (<c>0x0056b1c8</c>, sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b> ; relevee au
    /// desassembleur, <b>56 instructions</b>, dump conserve dans
    /// <c>Legacy/Raw/Blaze/FUN_004840E0.asm</c>.
    ///
    /// <para>
    /// <b>Le halo qui entraine son jumeau.</b> Meme minuteur de teinte que
    /// <see cref="EntityHandler_00483E90"/> — montee de <c>0x080808</c> jusqu'a <c>0x808080</c>,
    /// puis descente jusqu'a zero — mais avec <b>un palier au milieu</b> et, surtout, un
    /// <b>partenaire</b> : l'entite que <c>+0x70</c> designe recoit les <i>memes</i> bascules, au
    /// meme moment.
    /// </para>
    /// <list type="number">
    ///   <item>Tant que ni le bit <c>2</c> ni le bit <c>0x2000000</c> ne sont poses : les deux
    ///     echelles <c>+0x28</c> et <c>+0x2c</c> s'approchent de <c>0x5000</c> d'un huitieme de
    ///     l'ecart, et la teinte monte. A <c>0x808080</c>, le bit <b><c>2</c></b> est pose — sur
    ///     elle <i>et</i> sur le partenaire.</item>
    ///   <item>Bit <c>2</c> pose : un palier de <b>40 images</b> pendant lequel plus rien ne bouge
    ///     que <c>+0x32</c>. Au bout, le bit <b><c>0x2000000</c></b> est pose — sur elle
    ///     <i>et</i> sur le partenaire.</item>
    ///   <item>Bit <c>0x2000000</c> pose : les deux echelles perdent un huitieme d'elles-memes par
    ///     image et la teinte redescend. A zero, l'enregistrement est libere.</item>
    /// </list>
    /// <para>
    /// <b><c>+0x32</c> tourne de <c>0x80</c> par image, hors de toute garde</b> : meme apres
    /// l'extinction, tant que le corps tourne encore.
    /// </para>
    /// <para>
    /// <b>Aucun garde sur <c>+0x70</c></b> : le partenaire est lu et ecrit sans verification. C'est
    /// ce que fait le binaire, et le premier exemplaire de la zone dont la naissance ne le
    /// renseignerait pas planterait.
    /// </para>
    /// </summary>
    private static void EntityHandler_004840E0(byte* e)
    {
        uint word = *(uint*)e;

        *(short*)(e + 0x32) += 0x80;

        byte* partner = *(byte**)(e + 0x70);

        if ((word & 0x2000000) != 0)
        {
            short shrunk = (short)(*(short*)(e + 0x28) - (*(short*)(e + 0x28) >> 3));
            *(short*)(e + 0x28) = shrunk;
            *(short*)(e + 0x2c) = shrunk;

            uint dim = *(uint*)(e + 0x38) + 0xfff7f7f8;   // -0x080808
            *(uint*)(e + 0x38) = dim;

            if (dim == 0) *(uint*)e = 0;
            return;
        }

        if ((word & 2) != 0)
        {
            (*(short*)(e + 0x10))++;
            if (*(short*)(e + 0x10) < 0x28) return;

            *(uint*)e = word | 0x2000000;
            *(uint*)partner |= 0x2000000;
            return;
        }

        short grown = (short)(*(short*)(e + 0x28) + ((0x5000 - *(short*)(e + 0x28)) >> 3));
        uint lit = *(uint*)(e + 0x38) + 0x80808;

        *(short*)(e + 0x28) = grown;
        *(short*)(e + 0x2c) = grown;
        *(uint*)(e + 0x38) = lit;

        if (lit != 0x808080) return;

        *(uint*)e = word | 2;

        // Ghidra: `or al,02` — seul l'octet bas du mot du partenaire est touche.
        *(uint*)partner |= 2;
    }

    /// <summary>
    /// La bulle qui enfle et s'efface, partagee par <see cref="EntityHandler_00425270"/> et
    /// <see cref="EntityHandler_00425350"/> (sous-zones 4 et 9, <c>0x0056b1c8</c>). Les deux corps
    /// sont identiques ici a <b>une constante pres</b> — le pas dont la vitesse d'expansion croit —
    /// et leurs naissances, elles, n'ont rien de commun ; elles restent donc chacune chez soi.
    ///
    /// <para>
    /// <b>Les trois echelles grossissent de <c>+0x12</c> par image, et <c>+0x12</c> grossit aussi</b>,
    /// de <paramref name="acceleration"/>, jusqu'a un plafond de <c>0x3e8</c>. La teinte, elle,
    /// perd <c>0x0c</c> sur les trois canaux par image. L'entite meurt quand l'echelle atteint
    /// <c>0x7530</c> — trente mille, sept fois et demie la taille normale — et elle y est calee
    /// exactement avant que le bit <c>0x80000000</c> ne tombe.
    /// </para>
    /// <para>
    /// <b>Le plancher de la teinte ne teste que le rouge.</b> Si <c>+0x38</c> passe sous zero, les
    /// <i>trois</i> canaux sont mis a zero d'un coup. Les trois partant de la meme valeur et
    /// descendant du meme pas, ils restent egaux — mais le corps ne s'appuie pas la-dessus, il
    /// force.
    /// </para>
    /// <para>
    /// <b>L'ordre du binaire compte</b> : les echelles sont augmentees de l'ANCIEN <c>+0x12</c>,
    /// puis <c>+0x12</c> est avance et range, puis plafonne. La premiere image ajoute donc bien la
    /// valeur posee a la naissance.
    /// </para>
    /// </summary>
    private static void TickSwellingBubble(byte* e, short acceleration)
    {
        short red = (short)(e[0x38] - 0x0c);
        short green = (short)(e[0x39] - 0x0c);
        short blue = (short)(e[0x3a] - 0x0c);

        if (red < 0) { red = 0; green = 0; blue = 0; }

        e[0x38] = (byte)red;

        short step = *(short*)(e + 0x12);
        *(short*)(e + 0x28) += step;
        *(short*)(e + 0x2a) += step;
        *(short*)(e + 0x2c) += step;

        step += acceleration;

        e[0x39] = (byte)green;
        e[0x3a] = (byte)blue;

        *(short*)(e + 0x12) = step;
        if (step >= 0x3e8) *(short*)(e + 0x12) = 0x3e8;

        if (*(short*)(e + 0x28) < 0x7530) return;

        *(short*)(e + 0x28) = 0x7530;
        *(short*)(e + 0x2a) = 0x7530;
        *(short*)(e + 0x2c) = 0x7530;

        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00425270</c> — creneau <b>7</b> du bloc <c>0x00558ba8</c> (<c>0x0056b1c8</c>,
    /// sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur,
    /// <b>65 instructions</b>, dump conserve dans <c>Legacy/Raw/Blaze/FUN_00425270.asm</c>.
    ///
    /// <para>
    /// <b>La bulle qui part d'un point et fait naitre autre chose en meme temps.</b> Elle monte de
    /// <c>0x64000</c> — cent cases — a la naissance, part d'une <b>echelle nulle</b> et d'une teinte
    /// <c>0xfa</c>, et fait naitre le sous-type <c>0xc8</c> par
    /// <see cref="SpawnCastEntity_0047AAE0"/> avant de commencer a enfler.
    /// </para>
    /// <para>
    /// <b>Son <c>+0x12</c> est ecrit DEUX fois a la naissance</b> : a zero avant l'appel, puis a
    /// <c>0x2bc</c> apres. Le premier n'est pas mort — la naissance de l'enfant recopie le
    /// champ —, c'est l'enfant qui herite du zero et la mere qui garde <c>0x2bc</c>.
    /// </para>
    /// <para>Son expansion accelere de <c>0x14</c> par image ; voir
    /// <see cref="TickSwellingBubble"/>.</para>
    /// </summary>
    private static void EntityHandler_00425270(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x20) += 0x64000;

            e[0x04] = 2;
            e[0x0a] = 9;
            e[0x0b] = 0;

            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;

            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;

            e[0x38] = 0xfa;
            e[0x39] = 0xfa;
            e[0x3a] = 0xfa;

            SpawnCastEntity_0047AAE0(e, 0xc8, 0, -1);      // Ghidra: FUN_0047aae0

            *(short*)(e + 0x12) = 0x2bc;
        }

        if (*(short*)(e + 0x10) == 1) TickSwellingBubble(e, 0x14);
    }

    /// <summary>
    /// Ghidra: <c>0x00425350</c> — creneau <b>8</b> du bloc <c>0x00558ba8</c> (<c>0x0056b1c8</c>,
    /// sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur,
    /// <b>54 instructions</b>, dump conserve dans <c>Legacy/Raw/Blaze/FUN_00425350.asm</c>.
    ///
    /// <para>
    /// <b>La meme bulle, mais deja grande et deja lancee.</b> Elle ne monte pas, ne fait rien
    /// naitre, et part d'une echelle de <c>0xbb8</c> avec une teinte <b>blanche</b> (<c>0xff</c>)
    /// et un <c>+0x12</c> de <c>0x384</c> — pres du plafond <c>0x3e8</c>, si bien qu'elle atteint
    /// sa taille finale en une trentaine d'images. Son expansion accelere de <c>0x28</c>, le double
    /// de sa sœur.
    /// </para>
    /// <para>
    /// <b>Elle prend le genre <c>3</c> et la forme <c>2</c></b>, la ou sa sœur prend <c>2</c> et
    /// <c>9</c> : ce ne sont pas deux exemplaires du meme objet.
    /// </para>
    /// </summary>
    private static void EntityHandler_00425350(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x28) = 0xbb8;
            *(short*)(e + 0x2a) = 0xbb8;
            *(short*)(e + 0x2c) = 0xbb8;

            e[0x04] = 3;
            e[0x0a] = 2;
            e[0x0b] = 0;

            *(short*)(e + 0x12) = 0x384;

            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;
        }

        if (*(short*)(e + 0x10) == 1) TickSwellingBubble(e, 0x28);
    }

    /// <summary>
    /// L'etincelle accrochee a deux entites, partagee par <see cref="EntityHandler_0041D090"/>
    /// (sous-zones 0 a 3) et <see cref="EntityHandler_0041F520"/> (sous-zones 5 a 8). Les deux
    /// corps ne different que par <b>deux</b> choses : la planche, et le <b>garde sur les deux
    /// pointeurs</b>.
    ///
    /// <para>
    /// <b>Elle n'a pas de position a elle.</b> A chaque image elle la recompose :
    /// <c>+0x74</c> fournit l'ancrage (<c>+0x1c</c>/<c>+0x20</c>/<c>+0x24</c>), <c>+0x6c</c>
    /// fournit un decalage (<c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>), et un <b>rayon de 15
    /// unites</b> autour de cet ancrage suit le cap <c>+0x5c + +0x32 - 0xc00</c> — sinus sur
    /// <c>X</c>, cosinus sur <c>Z</c>, le quart de tour de <c>0xc00</c> etant la correction d'axe
    /// habituelle. La multiplication par quinze est ecrite <c>(x*3)*5</c> par le compilateur.
    /// </para>
    /// <para>
    /// <b>La version des sous-zones 0 a 3 remplace un pointeur nul par un enregistrement VIDE</b>
    /// de <c>0x9c</c> octets pris sur la pile et mis a zero (<c>rep stosd</c>) : l'etincelle tombe
    /// alors a l'origine au lieu de planter. <b>La version des sous-zones 5 a 8 n'a pas ce garde</b>
    /// et dereference les deux pointeurs directement. La difference est dans le binaire, elle n'est
    /// pas une precaution ajoutee ici.
    /// </para>
    /// <para>
    /// <b>Elle vit onze images</b> : <c>+0x12</c> part de <c>0x0b</c> et descend d'un par image ; a
    /// zero, le bit <c>0x80000000</c> tombe. Sa planche avance d'un enregistrement par image en
    /// bouclant sur trois (<b>comparaison non signee</b>), et sa premiere image est tiree au
    /// hasard dans <c>[0, 3[</c>.
    /// </para>
    /// </summary>
    private static void TickAnchoredSpark(byte* e, byte* descriptors, bool guardNulls)
    {
        byte* offset = *(byte**)(e + 0x6c);
        byte* anchor = *(byte**)(e + 0x74);

        if (guardNulls)
        {
            // Ghidra: 0x0041d0a0 — 0x26 dwords plus le mot precedent, soit 0x9c octets, mis a zero.
            byte* blank = stackalloc byte[0x9c];
            new Span<byte>(blank, 0x9c).Clear();

            if (anchor == null) anchor = blank;
            if (offset == null) offset = blank;
        }

        *(int*)(e + 0x20) = *(int*)(offset + 0x54) + *(int*)(anchor + 0x20);

        int course = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;

        // Ghidra: `lea eax,[eax+eax*2] ; lea ecx,[eax+eax*4]` — la multiplication par quinze.
        int swing = (int)Winsys3d.GetSinTableEntry(course) * 15;             // Ghidra: 0x00545a1c
        *(int*)(e + 0x1c) = swing + *(int*)(offset + 0x50) + *(int*)(anchor + 0x1c);

        course = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;

        int reach = Winsys3d.GetCosTableEntry(course) * 15;                  // Ghidra: 0x00545a16
        *(int*)(e + 0x24) = reach + *(int*)(offset + 0x58) + *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = descriptors;

            e[0x0a] = 2;
            e[0x0b] = 0;

            *(short*)(e + 0x28) = 0xfa0;
            *(short*)(e + 0x2a) = 0xfa0;

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x0b;

            e[0x40] = (byte)(CrtRand() % 3);          // Ghidra: FUN_00547140
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))--;

        e[0x40]++;
        if (e[0x40] >= 3) e[0x40] = 0;               // Ghidra: `cmp cl,3 ; jb` — non signee

        if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: <c>0x0041d090</c> — creneau <b>46</b> du bloc <c>0x00557fa8</c>
    /// (<c>0x0056b2c8</c>, sous-zones 0 a 3). <b>Absente de <c>Blaze.c</c></b> ; relevee au
    /// desassembleur, <b>83 instructions</b>, dump conserve dans
    /// <c>Legacy/Raw/Blaze/FUN_0041D090.asm</c>. La version <b>gardee</b> — voir
    /// <see cref="TickAnchoredSpark"/>.</summary>
    private static void EntityHandler_0041D090(byte* e)
        => TickAnchoredSpark(e, Descriptors00550060(), guardNulls: true);     // &DAT_00550060

    /// <summary>Ghidra: <c>0x0041f520</c> — creneau <b>46</b> du bloc <c>0x00558628</c>
    /// (<c>0x0056b2c8</c>, sous-zones 5 a 8), <b>le meme creneau</b> que sa sœur. <b>Absente de
    /// <c>Blaze.c</c></b> ; relevee au desassembleur, <b>69 instructions</b>, dump conserve dans
    /// <c>Legacy/Raw/Blaze/FUN_0041F520.asm</c>. La version <b>non gardee</b>, et les quatorze
    /// instructions d'ecart sont exactement le garde.</summary>
    private static void EntityHandler_0041F520(byte* e)
        => TickAnchoredSpark(e, Descriptors00550268(), guardNulls: false);    // &DAT_00550268

    /// <summary>
    /// Ghidra: <c>0x004249d0</c> — creneau <b>57</b> du bloc <c>0x00558ca8</c>
    /// (<c>0x0056b2c8</c>, sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b> ; relevee au
    /// desassembleur, <b>85 instructions</b>, dump conserve dans
    /// <c>Legacy/Raw/Blaze/FUN_004249D0.asm</c>.
    ///
    /// <para>
    /// <b>Le debris qui attend, puis tombe jusqu'au sol.</b> A la naissance il monte de
    /// <c>0x50000</c> — quatre-vingts cases — puis se disperse de <b><c>±20</c> cases sur les trois
    /// axes</b>, un tirage par axe, dans l'ordre <c>X</c>, <c>Y</c>, <c>Z</c> du binaire. Sa teinte
    /// part <b>noire</b>, son echelle horizontale d'un tirage dans <c>[0x800, 0xc00[</c>, et son
    /// attente d'un tirage dans <c>[4, 24[</c> images.
    /// </para>
    /// <para>
    /// <b>La chute accelere par le compteur lui-meme</b> : l'etat 2 retranche
    /// <c>+0x14 &lt;&lt; 12</c> a la hauteur et avance <c>+0x14</c> d'un, si bien que la vitesse
    /// croit d'une case par image. Elle s'arrete des que la hauteur passe sous <c>+0x48</c> — le
    /// sol que son placement lui a donne — et le bit <c>0x80000000</c> tombe.
    /// </para>
    /// <para>
    /// <b>Sa planche est le SECOND enregistrement de <c>s_Descriptors_00550350</c></b>, pas une
    /// table a elle : <c>+0x3c</c> recoit <c>0x00550358</c> et <c>+0x40</c> reste a zero, donc un
    /// seul enregistrement est lu. Le premier enregistrement de cette table appartient a
    /// <c>0x00425500</c>. L'adjacence est reelle dans le binaire ; elle est ecrite ici plutot que
    /// recopiee.
    /// </para>
    /// </summary>
    private static void EntityHandler_004249D0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            // Ghidra: `mov dword [esi+3Ch],550358h` — le second enregistrement de la table 0x550350.
            *(byte**)(e + 0x3c) = Descriptors00550350() + 8;
            e[0x40] = 0;

            *(int*)(e + 0x20) += 0x50000;

            *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) << 0xc;      // Ghidra: FUN_00547140
            *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) << 0xc;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) << 0xc;

            *(short*)(e + 0x14) = 0;

            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x14 + 4);

            // Ghidra: l'idiome du reste signe sur une puissance de deux, developpe en six
            // instructions par le compilateur.
            short spread = (short)(CrtRand() % 0x400 + 0x800);
            *(short*)(e + 0x28) = spread;
            *(short*)(e + 0x2a) = spread;
        }

        if (*(short*)(e + 0x10) == 1 && --(*(short*)(e + 0x12)) == 0) *(short*)(e + 0x10) = 2;

        if (*(short*)(e + 0x10) != 2) return;

        short speed = *(short*)(e + 0x14);
        int height = *(int*)(e + 0x20) - (speed << 0xc);

        *(short*)(e + 0x14) = (short)(speed + 1);
        *(int*)(e + 0x20) = height;

        if (height < *(int*)(e + 0x48)) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00425410 @ 0x00425410 — creneau <b>62</b> du bloc <c>0x00558ca8</c>
    /// (<c>0x0056b2c8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 71 instructions.
    ///
    /// <para>
    /// <b>Huit images, quatre images de planche, et c'est fini.</b> Elle se disperse de
    /// <b><c>±20</c> cases sur les trois axes</b> — l'ordre des tirages du binaire est <c>X</c>,
    /// <c>Z</c>, <c>Y</c>, et il compte parce qu'il consomme le flux du hasard —, prend une echelle
    /// horizontale et une echelle de profondeur tirees separement dans <c>]0xc00, 0x1400]</c>
    /// (<c>0x1400 - rand() % 0x800</c>), puis avance sa planche <b>toutes les deux images</b>. Au
    /// quatrieme enregistrement, le bit <c>0x80000000</c> tombe.
    /// </para>
    /// <para>
    /// <b>Sa planche est la seule de la zone dont la donnee ne borne pas la table</b> : voir
    /// <see cref="s_Descriptors_00550360"/>. C'est le <c>== 4</c> de ce corps qui est la preuve.
    /// </para>
    /// </summary>
    private static void EntityHandler_00425410(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00550360();          // Ghidra: &DAT_00550360
            e[0x0a] = 9;

            *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) * 0x1000;   // Ghidra: FUN_00547140
            *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) * 0x1000;

            *(ushort*)(e + 0x28) = (ushort)(0x1400 - CrtRand() % 0x800);

            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;

            *(ushort*)(e + 0x2a) = (ushort)(0x1400 - CrtRand() % 0x800);
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 2) return;

        *(short*)(e + 0x12) = 0;

        e[0x40]++;
        if (e[0x40] == 4) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LOT 8 (2026-09-18) — LA CATEGORIE C DES TABLES D'EFFETS
    //
    // Les seize derniers « C » de la zone 5. Deux sont des VERBES de script (`WorldMapScript.cs`),
    // deux des SCENES (`Cutscene/LabyrinthScenes.cs`) ; les douze autres sont ici. Deux corps
    // n'ont demande aucune ligne propre — 0x0041C740 et 0x0041EC20 sont `TickHostDeathBurst`, plus
    // haut —, et 0x0041FCA0 assemble deux corps deja ecrits : le nuage ci-dessous et le
    // stroboscope de la foret (`StepCarrierStrobe`, Cutscene/ForestScenes.cs).
    // =====================================================================

    // ---------------------------------------------------------------------
    // CRENEAU 29 DE 0x0056b0c8, DANS LES TROIS BLOCS — LE NUAGE QUI COLLE A SON PORTEUR
    // ---------------------------------------------------------------------

    /// <summary>
    /// Ghidra: FUN_0041d1a0 @ 0x0041d1a0 — creneau <b>29</b> du bloc <c>0x00557da8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 0 a 3). Decompilee dans <c>Blaze.c</c>, 112 instructions ;
    /// dump sous <c>Legacy/Raw/Blaze/FUN_0041D1A0.asm</c>.
    ///
    /// <para>
    /// <b>Un nuage cale sur son porteur <c>+0x6c</c>, qui porte l'effet 32 a la cible
    /// <c>+0x78</c>.</b> Il reprend chaque image la position du porteur. A la naissance il seme
    /// <b>onze</b> sous-types <c>0xed</c> d'un coup, puis attend au plus <b>cinquante</b> images que
    /// <c>+0x44</c> passe a 1 — ce corps ne l'ecrit jamais, il le remet seulement a zero a la
    /// naissance. <b>Ce sont ses propres semis qui le posent</b> (etabli au lot 11) : un
    /// <c>0xed</c> qui atteint la cible ecrit <c>+0x44 = 1</c> sur son porteur — voir
    /// <see cref="TickCloudSeed"/>. A ce moment il porte
    /// <see cref="ApplyStatusEffect_0053C5E0"/> (le porteur en attaquant) et, si l'effet est
    /// refuse, meurt sur-le-champ. Sinon il continue de semer un <c>0xed</c> <b>toutes les trois
    /// images</b>, pour <c>15 + rand() % 10</c> semis, et s'eteint quand ce compte, qui continue
    /// de descendre, passe sous <c>-0x22</c>.
    /// </para>
    /// <para>
    /// <b>Les cinquante images de l'etat 1 ne font pas <c>return</c></b> : l'entite perd son bit
    /// vivant, mais le test de <c>+0x44</c> s'execute encore dans la meme image — et peut encore
    /// porter l'effet. C'est ecrit ainsi.
    /// </para>
    /// <para>
    /// <b>Quand le porteur meurt</b> (bit <c>0x40000000</c>, ou <c>0x80000000</c> tombe), le nuage
    /// meurt avec lui, et si son <c>+0x9a</c> vaut 0 ou 1 il <b>allume le voile noir</b>
    /// <c>0x005de024</c> — l'octet fort a 1, les trois octets de couleur a zero.
    /// </para>
    /// <para>
    /// <b>Les trois copies different par leur porteur absent</b>, et ce n'est pas un detail :
    /// celle-ci sort sans rien faire, <see cref="EntityHandler_0041F600"/> (5 a 8) ne teste pas du
    /// tout, et <see cref="EntityHandler_00421C10"/> (4 et 9) retombe sur
    /// <c>Res.s_EntityList_005de558</c> — pour le porteur <b>et</b> pour la cible — et passe par
    /// <see cref="IsEffectPairBlocked_004364B0"/> avant l'effet.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041D1A0(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null) return;

        if (!TrackCarrierOrDie(e, host)) return;
        StepStatusCloud(e, host, 0xed, CloudEffectGate.Direct);
    }

    /// <summary>
    /// Ghidra: FUN_0041f600 @ 0x0041f600 — creneau <b>29</b> du bloc <c>0x00558428</c>
    /// (<c>0x0056b0c8</c>, sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 110 instructions.
    /// <see cref="EntityHandler_0041D1A0"/> <b>sans le test du porteur</b> : le binaire lit
    /// <c>[ebx+1Ch]</c> sur un <c>+0x6c</c> qu'il n'a pas verifie. Un porteur nul y serait une
    /// faute de page ; ici, une violation d'acces — la meme chose, a la meme instruction.
    /// </summary>
    private static void EntityHandler_0041F600(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);

        if (!TrackCarrierOrDie(e, host)) return;
        StepStatusCloud(e, host, 0xed, CloudEffectGate.Direct);
    }

    /// <summary>
    /// Ghidra: FUN_00421c10 @ 0x00421c10 — creneau <b>29</b> du bloc <c>0x00558aa8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 116 instructions.
    /// <see cref="EntityHandler_0041D1A0"/> avec le repli de ce bloc : un porteur nul devient
    /// <c>Res.s_EntityList_005de558</c>, comme dans <see cref="EntityHandler_00422C60"/>, et l'effet
    /// passe par <see cref="ApplyStatusEffect32IfAllowed_00421D80"/>.
    /// </summary>
    private static void EntityHandler_00421C10(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null) host = Res.s_EntityList_005de558;

        if (!TrackCarrierOrDie(e, host)) return;
        StepStatusCloud(e, host, 0xed, CloudEffectGate.GuardedWithFallback);
    }

    /// <summary>
    /// Ghidra: FUN_0041fca0 @ 0x0041fca0 — creneau <b>31</b> du bloc <c>0x00558428</c>
    /// (<c>0x0056b0c8</c>, sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 185 instructions ;
    /// dump sous <c>Legacy/Raw/Blaze/FUN_0041FCA0.asm</c>.
    ///
    /// <para>
    /// <b>Deux corps deja ecrits sous un meme toit</b>, aiguilles par <c>+0x9a</c> :
    /// </para>
    /// <list type="bullet">
    /// <item><c>+0x9a == 0</c> — <b>le stroboscope de la foret</b>,
    /// <see cref="EntityHandler_00413330"/>, invocations en <c>0xdf</c> au lieu de <c>0xde</c> :
    /// le voile rouge une image sur deux pendant trente images
    /// (<see cref="StepCarrierStrobe"/>) ;</item>
    /// <item><c>+0x9a == 2</c> — <b>le nuage du creneau 29</b>, semis en <c>0xdf</c> au lieu de
    /// <c>0xed</c>, l'effet 32 garde par <see cref="IsEffectPairBlocked_004364B0"/> mais <b>sans</b>
    /// le repli de la cible.</item>
    /// </list>
    /// <para>
    /// Le porteur est lu sans test, comme dans son voisin <see cref="EntityHandler_0041F600"/>. Sa
    /// seule appelee inconnue, <c>FUN_0041ff00</c>, est <b>octet pour octet</b>
    /// <see cref="ApplyStatusEffect32IfAllowed_00421D80"/> : 61 octets, seuls les deplacements des
    /// deux <c>call</c> different, et ils resolvent vers les memes cibles.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041FCA0(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);

        if (!TrackCarrierOrDie(e, host)) return;

        if (*(short*)(e + 0x9a) == 0) StepCarrierStrobe(e, 0xdf);
        if (*(short*)(e + 0x9a) == 2) StepStatusCloud(e, host, 0xdf, CloudEffectGate.Guarded);
    }

    /// <summary>
    /// La tete commune des nuages et du stroboscope : se recaler sur le porteur resolu, et
    /// <b>mourir avec lui</b> s'il porte <c>0x40000000</c> ou a perdu <c>0x80000000</c> — en
    /// rallumant le voile noir <c>0x005de024</c> (l'octet fort a 1, la couleur a zero) quand
    /// <c>+0x9a</c> vaut 0 ou 1. Rend faux quand l'entite vient de mourir.
    /// </summary>
    private static bool TrackCarrierOrDie(byte* e, byte* host)
    {
        *(int*)(e + 0x1c) = *(int*)(host + 0x1c);
        *(int*)(e + 0x20) = *(int*)(host + 0x20);
        *(int*)(e + 0x24) = *(int*)(host + 0x24);

        uint hostFlags = *(uint*)host;
        if ((hostFlags & 0x40000000) == 0 && (hostFlags & 0x80000000) != 0) return true;

        *(uint*)e &= 0x7fffffff;

        short side = *(short*)(e + 0x9a);
        if (side == 0 || side == 1) Mem.s_Unknown_005de024 = 0x01000000;   // Ghidra: DAT_005de024..27
        return false;
    }

    /// <summary>Comment un nuage porte l'effet 32. Trois emissions, trois gardes — lus au
    /// desassembleur, un par corps.</summary>
    private enum CloudEffectGate
    {
        /// <summary><see cref="ApplyStatusEffect_0053C5E0"/> tout droit (<c>0x0041D1A0</c>,
        /// <c>0x0041F600</c>).</summary>
        Direct,

        /// <summary><see cref="ApplyStatusEffect32IfAllowed_00421D80"/>, sur la cible telle quelle
        /// (<c>0x0041FCA0</c>).</summary>
        Guarded,

        /// <summary>La meme, une cible nulle devenant <c>Res.s_EntityList_005de558</c>
        /// (<c>0x00421C10</c>).</summary>
        GuardedWithFallback,
    }

    /// <summary>
    /// Les trois temps d'un nuage, porteur deja suivi et vivant : <paramref name="seed"/> est le
    /// sous-type seme, <paramref name="gate"/> la facon de porter l'effet 32. Voir
    /// <see cref="EntityHandler_0041D1A0"/>.
    /// </summary>
    private static void StepStatusCloud(byte* e, byte* host, byte seed, CloudEffectGate gate)
    {
        int* position = (int*)(e + 0x1c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = 0;
            for (int i = 0; i < 0xb; i++)
                SpawnStatusEffectEntity_0047AC40(e, position, null, seed, 0, -1);

            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x32) *(uint*)e &= 0x7fffffff;

            if (*(int*)(e + 0x44) == 1)
            {
                *(short*)(e + 0x10) = 2;
                int roll = CrtRand();                                   // Ghidra: FUN_00547140
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x12) = (short)(roll % 10 + 0xf);

                uint* victim = *(uint**)(e + 0x78);
                bool refused;
                switch (gate)
                {
                    case CloudEffectGate.Direct:
                        refused = ApplyStatusEffect_0053C5E0((uint*)host, victim, 0) != 0;
                        break;
                    case CloudEffectGate.Guarded:
                        refused = ApplyStatusEffect32IfAllowed_00421D80((uint*)host, victim, 0);   // Ghidra: FUN_0041ff00
                        break;
                    default:
                        if (victim == null) victim = (uint*)Res.s_EntityList_005de558;
                        refused = ApplyStatusEffect32IfAllowed_00421D80((uint*)host, victim, 0);
                        break;
                }

                if (refused)
                {
                    *(uint*)e &= 0x7fffffff;
                    return;
                }
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x14))++;
        if (*(short*)(e + 0x14) != 3) return;

        *(short*)(e + 0x14) = 0;
        if (*(short*)(e + 0x12) > 0)
            SpawnStatusEffectEntity_0047AC40(e, position, null, seed, 0, -1);

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < -0x22) *(uint*)e &= 0x7fffffff;
    }

    // ---------------------------------------------------------------------
    // CRENEAU 45 (ET 31) DE 0x0056b2c8 — LES SEMIS DES NUAGES
    // ---------------------------------------------------------------------

    /// <summary>Ghidra: <c>&amp;DAT_00550040</c> — la planche de <see cref="EntityHandler_0041D310"/>
    /// (sous-zones 0 a 3). <b>Quatre</b> enregistrements : trois tuiles <c>0x0f</c>, que le mode 0
    /// fait tourner (<c>+0x40</c> de 0 a 2), puis une tuile <c>0x1f</c>, que le mode 1 pose
    /// (<c>+0x40 = 3</c>). Bornee par <c>0x00550060</c>, deja releve.</summary>
    private static readonly byte[] s_Descriptors_00550040 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550040  <- 0x0041D310
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550048
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550050
        0x60, 0x48, 0x1f, 0x1f, 0x01, 0x02, 0x10, 0x00,   // 0x00550058
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550228</c> — la planche de <see cref="EntityHandler_0041FF40"/>
    /// (sous-zones 5 a 8, le semis <c>0xdf</c>). Memes octets que
    /// <see cref="s_Descriptors_00550040"/> ; bornee par <c>0x00550248</c>.</summary>
    private static readonly byte[] s_Descriptors_00550228 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550228  <- 0x0041FF40
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550230
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550238
        0x60, 0x48, 0x1f, 0x1f, 0x01, 0x02, 0x10, 0x00,   // 0x00550240
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550248</c> — la planche de <see cref="EntityHandler_0041F760"/>
    /// (sous-zones 5 a 8, le semis <c>0xed</c>). Memes octets ; bornee par <c>0x00550268</c>,
    /// deja releve.</summary>
    private static readonly byte[] s_Descriptors_00550248 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550248  <- 0x0041F760
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550250
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550258
        0x60, 0x48, 0x1f, 0x1f, 0x01, 0x02, 0x10, 0x00,   // 0x00550260
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550460</c> — la planche de <see cref="EntityHandler_00421DC0"/>
    /// (sous-zones 4 et 9). Memes octets ; <c>0x00550480</c> change de forme
    /// (<c>00 00 08 08</c>).</summary>
    private static readonly byte[] s_Descriptors_00550460 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550460  <- 0x00421DC0
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550468
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550470
        0x60, 0x48, 0x1f, 0x1f, 0x01, 0x02, 0x10, 0x00,   // 0x00550478
    ];

    private static byte* s_Descriptors00550040Storage;
    private static byte* s_Descriptors00550228Storage;
    private static byte* s_Descriptors00550248Storage;
    private static byte* s_Descriptors00550460Storage;

    private static byte* Descriptors00550040() => NativeCopy(s_Descriptors_00550040, ref s_Descriptors00550040Storage);
    private static byte* Descriptors00550228() => NativeCopy(s_Descriptors_00550228, ref s_Descriptors00550228Storage);
    private static byte* Descriptors00550248() => NativeCopy(s_Descriptors_00550248, ref s_Descriptors00550248Storage);
    private static byte* Descriptors00550460() => NativeCopy(s_Descriptors_00550460, ref s_Descriptors00550460Storage);

    /// <summary>
    /// Ghidra: FUN_0041d310 @ 0x0041d310 — creneau <b>45</b> du bloc <c>0x00557fa8</c>
    /// (<c>0x0056b2c8</c>, sous-zones 0 a 3), soit le sous-type <b><c>0xed</c></b>. Decompilee
    /// dans <c>Blaze.c</c>, 431 instructions ; dump sous <c>Legacy/Raw/Blaze/FUN_0041D310.asm</c>.
    /// Lot 11, 2026-09-18. <b>Une cible <c>+0x78</c> nulle fait sortir le corps</b> avant tout
    /// (<c>je near 0041D850h</c>, le <c>ret</c>).
    /// </summary>
    private static void EntityHandler_0041D310(byte* e)
        => TickCloudSeed(e, Descriptors00550040(), AbsentHost.Abandon);

    /// <summary>
    /// Ghidra: FUN_0041f760 @ 0x0041f760 — creneau <b>45</b> du bloc <c>0x00558628</c>
    /// (<c>0x0056b2c8</c>, sous-zones 5 a 8), sous-type <b><c>0xed</c></b>. Decompilee dans
    /// <c>Blaze.c</c>, 430 instructions ; dump sous <c>Legacy/Raw/Blaze/FUN_0041F760.asm</c>. Le
    /// modele de la famille : aucun test de pointeur.
    /// </summary>
    private static void EntityHandler_0041F760(byte* e)
        => TickCloudSeed(e, Descriptors00550248(), AbsentHost.Unchecked);

    /// <summary>
    /// Ghidra: FUN_0041ff40 @ 0x0041ff40 — creneau <b>31</b> du bloc <c>0x00558628</c>
    /// (<c>0x0056b2c8</c>, sous-zones 5 a 8), soit le sous-type <b><c>0xdf</c></b> — celui que seme
    /// <see cref="EntityHandler_0041FCA0"/>. Decompilee dans <c>Blaze.c</c>. <b>Identique</b> a
    /// <see cref="EntityHandler_0041F760"/> a la planche pres.
    /// </summary>
    private static void EntityHandler_0041FF40(byte* e)
        => TickCloudSeed(e, Descriptors00550228(), AbsentHost.Unchecked);

    /// <summary>
    /// Ghidra: FUN_00421dc0 @ 0x00421dc0 — creneau <b>45</b> du bloc <c>0x00558ca8</c>
    /// (<c>0x0056b2c8</c>, sous-zones 4 et 9), sous-type <b><c>0xed</c></b>. Decompilee dans
    /// <c>Blaze.c</c>, 441 instructions. <b>Les pointeurs nuls y retombent sur
    /// <c>Res.s_EntityList_005de558</c></b> : la cible a l'entree et a chacune de ses relectures,
    /// le porteur la ou il est ecrit — mais pas la ou le mode 1 le lit, qui reste sans test.
    /// </summary>
    private static void EntityHandler_00421DC0(byte* e)
        => TickCloudSeed(e, Descriptors00550460(), AbsentHost.FallBackOnEntityList);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005510f8</c> — <b>la planche du semis de la grotte scellee</b>, quatre
    /// enregistrements. Memes octets que <see cref="s_Descriptors_00550460"/> ; bornee par
    /// <c>0x00551118</c>, ou les memes trois tuiles recommencent pour une autre table. Les quatre
    /// sont necessaires : le mode 0 fait tourner <c>+0x40</c> de 0 a 2, le mode 1 le pose a 3.
    /// </summary>
    private static readonly byte[] s_Descriptors_005510F8 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005510f8  <- 0x00439170
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551100
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551108
        0x60, 0x48, 0x1f, 0x1f, 0x01, 0x02, 0x10, 0x00,   // 0x00551110
    ];

    private static byte* s_Descriptors005510F8Storage;

    private static byte* Descriptors005510F8() => NativeCopy(s_Descriptors_005510F8, ref s_Descriptors005510F8Storage);

    /// <summary>
    /// Ghidra: FUN_00439170 @ 0x00439170 — creneau <b>45</b> du bloc <c>0x0055d428</c>
    /// (<c>0x0056b2c8</c>, sous-zones 6 et 7 de la <b>grotte scellee</b>). Decompilee dans
    /// <c>Blaze.c</c>, 441 instructions. Ecrite le 2026-09-18 avec le palier 1 du chateau, parce
    /// qu'elle est <b>le meme corps que <see cref="EntityHandler_0042B430"/></b> a la planche pres
    /// — 441 instructions comparees une a une, un seul <c>diff</c> de deux lignes.
    ///
    /// <para>
    /// <b>Elle protege donc les memes pointeurs que le chateau, et pas ceux du Labyrinthe</b> :
    /// pas de repli a la relecture de <c>+0x78</c> apres le contact, mais un repli sur le porteur
    /// du mode 1. Voir <see cref="EntityHandler_0042B430"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00439170(byte* e)
        => TickCloudSeed(e, Descriptors005510F8(), AbsentHost.FallBackOnEntityList,
                         guardRereadAfterHit: false, guardReturnCarrier: true);

    /// <summary>
    /// Le corps des quatre semis. <b>Deux vies</b>, choisies par <c>+0x44</c> — que le nuage
    /// parent a pose avant de les faire naitre et que l'enfant herite.
    ///
    /// <para>
    /// <b>Mode 0 : l'etincelle qui va chercher sa cible.</b> Elle s'eleve d'abord vers un point
    /// tire autour d'elle (<c>±150</c> unites a plat, <c>80</c> a <c>380</c> plus haut) en
    /// grandissant et en s'allumant, dix images ; puis elle fond sur la cible <c>+0x78</c> — un
    /// point <c>0x3c</c> unites au-dessus d'elle — en s'assombrissant. Dix-huit images plus tard,
    /// si elle est a moins de <c>0x8c</c> unites, elle <b>pose <c>+0x44 = 1</c> sur son porteur
    /// <c>+0x6c</c></b> — c'est ce qui declenche l'effet 32 du nuage,
    /// <see cref="EntityHandler_0041D1A0"/> — et se met a <b>tourner autour de la cible</b> en
    /// violet, rayon croissant vers <c>40..70</c>, pendant <c>30..50</c> images. Manquee, elle
    /// s'eteint quinze images plus tard. Sa planche tourne sur trois images tout du long.
    /// </para>
    /// <para>
    /// <b>La distance est mesuree vers un point SOUS la cible</b> : le binaire vise
    /// <c>cible + 0x3c000</c> mais calcule <c>(y - cible.y) + 0x3c000</c> — un <c>add</c>, pas un
    /// <c>sub</c>, verifie au desassembleur (<c>0x0041f928</c>). Arrivee sur son point, l'etincelle
    /// est donc a <c>0x78</c> unites de ce que la mesure croit, sous un seuil de <c>0x8c</c>. Transcrit
    /// tel quel.
    /// </para>
    /// <para>
    /// <b>Mode 1 : l'etincelle qui revient.</b> Nee sur la cible, elle monte vers un point au-dessus
    /// d'elle, puis revient sur son <b>porteur</b> et s'eteint au bout de vingt images, sans rien
    /// declencher. Le binaire calcule encore une distance a la cible au moment de s'eteindre, et
    /// n'en fait rien. Ce mode vient du stroboscope (<see cref="StepCarrierStrobe"/>), qui tire
    /// <c>+0x44</c> a 0 ou 1 avant chaque naissance.
    /// </para>
    /// </summary>
    private static void TickCloudSeed(
        byte* e, byte* descriptors, AbsentHost absent,
        bool guardRereadAfterHit = true, bool guardReturnCarrier = false)
    {
        byte* target = *(byte**)(e + 0x78);
        if (target == null)
        {
            if (absent == AbsentHost.Abandon) return;
            if (absent == AbsentHost.FallBackOnEntityList) target = Res.s_EntityList_005de558;
        }

        if (*(int*)(e + 0x44) == 0)
        {
            StepSeekingSeed(e, descriptors, target, absent, guardRereadAfterHit);

            e[0x40]++;
            if (e[0x40] == 3) e[0x40] = 0;

            FinalizeEntityPlacement2((uint*)e);                   // Ghidra: FUN_00533940
            if ((*(uint*)(e + 0x90) & 0x8800) != 0) *(uint*)e &= 0x7fffffff;
        }

        if (*(int*)(e + 0x44) == 1) StepReturningSeed(e, descriptors, target, absent, guardReturnCarrier);
    }

    /// <summary>Relit <c>+0x78</c> comme le fait le binaire, avec le repli de son bloc.</summary>
    private static byte* RereadSeedTarget(byte* e, AbsentHost absent)
    {
        byte* target = *(byte**)(e + 0x78);
        if (target == null && absent == AbsentHost.FallBackOnEntityList) target = Res.s_EntityList_005de558;
        return target;
    }

    /// <summary>Le pas d'approche au huitieme, commun aux deux modes : position vers
    /// <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>.</summary>
    private static void ApproachSeedPoint(byte* e)
    {
        *(int*)(e + 0x1c) += (*(int*)(e + 0x50) - *(int*)(e + 0x1c)) >> 3;
        *(int*)(e + 0x20) += (*(int*)(e + 0x54) - *(int*)(e + 0x20)) >> 3;
        *(int*)(e + 0x24) += (*(int*)(e + 0x58) - *(int*)(e + 0x24)) >> 3;
    }

    /// <summary>L'etat 1 des deux modes : l'approche, les deux echelles vers <c>+0x16</c> au
    /// huitieme, et l'allumage de 10 par canal. <paramref name="nextCount"/> est le compte de
    /// l'etat 2 : 18 en mode 0, 20 en mode 1.</summary>
    private static void StepRisingSeed(byte* e, short nextCount)
    {
        ApproachSeedPoint(e);

        *(short*)(e + 0x28) = (short)((*(short*)(e + 0x16) - *(short*)(e + 0x28) >> 3) + *(short*)(e + 0x28));
        (*(short*)(e + 0x12))--;
        *(short*)(e + 0x2a) = (short)((*(short*)(e + 0x16) - *(short*)(e + 0x2a) >> 3) + *(short*)(e + 0x2a));

        if (*(short*)(e + 0x12) < 1)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = nextCount;
        }

        BrightenEntityTint_00404DE0(e, 10, 10, 10);
    }

    /// <summary>Le mode 0 de <see cref="TickCloudSeed"/>, ses quatre etats.</summary>
    private static void StepSeekingSeed(
        byte* e, byte* descriptors, byte* target, AbsentHost absent, bool guardRereadAfterHit)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = descriptors;
            int frame = CrtRand();                                  // Ghidra: FUN_00547140
            e[0x0a] = 0x0d;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            e[0x40] = (byte)(frame % 3);

            *(int*)(e + 0x50) = (CrtRand() % 300 - 0x96) * 0x1000 + *(int*)(e + 0x1c);
            *(int*)(e + 0x58) = (CrtRand() % 300 - 0x96) * 0x1000 + *(int*)(e + 0x24);
            int rise = CrtRand();
            *(short*)(e + 0x12) = 10;
            *(int*)(e + 0x54) = (rise % 300 + 0x50) * 0x1000 + *(int*)(e + 0x20);

            int size = CrtRand() % 0x800;                          // Ghidra: le reste signe, en signe-module
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x16) = (short)(size + 0x1000);
        }

        if (*(short*)(e + 0x10) == 1) StepRisingSeed(e, 0x12);

        if (*(short*)(e + 0x10) == 2)
        {
            *(int*)(e + 0x50) = *(int*)(target + 0x1c);
            *(int*)(e + 0x54) = *(int*)(target + 0x20) + 0x3c000;
            *(int*)(e + 0x58) = *(int*)(target + 0x24);
            ApproachSeedPoint(e);
            (*(short*)(e + 0x12))--;

            if (*(short*)(e + 0x12) < 1)
            {
                int dx = (*(int*)(e + 0x1c) - *(int*)(target + 0x1c)) >> 0xc;
                int dy = (*(int*)(e + 0x20) - *(int*)(target + 0x20) + 0x3c000) >> 0xc;   // Ghidra: `add`, voir le resume
                int dz = (*(int*)(e + 0x24) - *(int*)(target + 0x24)) >> 0xc;
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));   // Ghidra: 0x00545a10

                if (distance < 0x8c)
                {
                    byte* carrier = *(byte**)(e + 0x6c);
                    if (carrier == null && absent == AbsentHost.FallBackOnEntityList) carrier = Res.s_EntityList_005de558;
                    *(int*)(carrier + 0x44) = 1;

                    // Ghidra: 0x0042B430 et 0x00439170 relisent +0x78 SANS repli ici.
                    target = guardRereadAfterHit ? RereadSeedTarget(e, absent) : *(byte**)(e + 0x78);
                    *(short*)(e + 0x10) = 3;
                    *(short*)(e + 0x12) = (short)(CrtRand() % 5 + 3);
                    *(short*)(e + 0x16) = (short)(CrtRand() % 0x1000);   // Ghidra: signe-module
                    int spread = CrtRand();
                    *(int*)(e + 0x48) = 0;
                    *(short*)(e + 0x14) = (short)(spread % 0x1e + 0x28);
                    *(int*)(e + 0x5c) = CrtRand() % 0x14 + 0x1e;

                    *(int*)(e + 0x50) = *(int*)(target + 0x1c);
                    *(int*)(e + 0x58) = *(int*)(target + 0x24);
                    *(int*)(e + 0x20) = *(int*)(target + 0x20) + 0x1e000;
                    e[0x38] = 100;
                    e[0x39] = 0x3c;
                    e[0x3a] = 100;
                }
                else if (*(short*)(e + 0x12) < -0xf)
                {
                    *(uint*)e &= 0x7fffffff;
                }
            }

            BrightenEntityTint_00404DE0(e, -3, -4, -3);
        }

        if (*(short*)(e + 0x10) != 3) return;

        target = RereadSeedTarget(e, absent);
        *(int*)(e + 0x50) = *(int*)(target + 0x1c);
        *(int*)(e + 0x58) = *(int*)(target + 0x24);

        int radius = *(int*)(e + 0x48);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x16)) * radius + *(int*)(e + 0x50);   // Ghidra: Ordinal_451
        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x16));                                               // Ghidra: Ordinal_450
        *(short*)(e + 0x16) += 0x96;
        *(int*)(e + 0x24) = cos * radius + *(int*)(e + 0x58);
        *(int*)(e + 0x20) += *(short*)(e + 0x12) * 0x1000;
        *(int*)(e + 0x48) = ((*(short*)(e + 0x14) - radius) >> 3) + radius;

        int life = *(int*)(e + 0x5c) - 1;
        *(int*)(e + 0x5c) = life;
        if (life < 1) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Le mode 1 de <see cref="TickCloudSeed"/>, ses trois etats. Le porteur y est lu
    /// sans test dans les quatre copies du Labyrinthe ; <b>le chateau et la grotte scellee le
    /// protegent</b>, et c'est leur seul ecart avec elles en plus de la planche.</summary>
    private static void StepReturningSeed(
        byte* e, byte* descriptors, byte* target, AbsentHost absent, bool guardReturnCarrier)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = descriptors;
            e[0x40] = 3;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
            *(int*)(e + 0x20) = *(int*)(target + 0x20);
            *(int*)(e + 0x24) = *(int*)(target + 0x24);

            *(int*)(e + 0x50) = (CrtRand() % 0xa0 - 0x50) * 0x1000 + *(int*)(target + 0x1c);
            *(int*)(e + 0x58) = (CrtRand() % 0xa0 - 0x50) * 0x1000 + *(int*)(target + 0x24);
            int rise = CrtRand();
            *(short*)(e + 0x12) = 10;
            *(int*)(e + 0x54) = (rise % 200 + 0xb4) * 0x1000 + *(int*)(target + 0x20);

            int size = CrtRand() % 1000;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x16) = (short)(size + 2000);
        }

        if (*(short*)(e + 0x10) == 1) StepRisingSeed(e, 0x14);

        if (*(short*)(e + 0x10) != 2) return;

        byte* carrier = *(byte**)(e + 0x6c);
        // Ghidra: seuls 0x0042B430 (chateau) et 0x00439170 (grotte) protegent CETTE lecture-ci.
        if (carrier == null && guardReturnCarrier && absent == AbsentHost.FallBackOnEntityList)
            carrier = Res.s_EntityList_005de558;
        *(int*)(e + 0x50) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x54) = *(int*)(carrier + 0x20) + 0x3c000;
        *(int*)(e + 0x58) = *(int*)(carrier + 0x24);
        ApproachSeedPoint(e);
        (*(short*)(e + 0x12))--;

        if (*(short*)(e + 0x12) < 1)
        {
            *(uint*)e &= 0x7fffffff;

            // Ghidra: une distance a la cible calculee et jetee — la racine est pure, on l'appelle
            // quand meme pour garder l'appel a la DLL a sa place.
            int dx = (*(int*)(e + 0x1c) - *(int*)(target + 0x1c)) >> 0xc;
            int dy = (*(int*)(e + 0x20) - *(int*)(target + 0x20) + 0x3c000) >> 0xc;
            int dz = (*(int*)(e + 0x24) - *(int*)(target + 0x24)) >> 0xc;
            Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
        }

        BrightenEntityTint_00404DE0(e, -6, -10, -6);
    }

    // ---------------------------------------------------------------------
    // CRENEAU 31 DE 0x0056b1c8 (SOUS-ZONES 5 A 8, ET 4 ET 9) — L'ONDE ET SA GERBE
    // ---------------------------------------------------------------------

    /// <summary>
    /// Ghidra: <c>0x00420480</c> — creneau <b>31</b> du bloc <c>0x00558528</c> (<c>0x0056b1c8</c>,
    /// sous-zones 5 a 8). <b>Absente de <c>Blaze.c</c></b>, 261 instructions ; dump sous
    /// <c>Legacy/Raw/Blaze/FUN_00420480.asm</c>. Lot 12, 2026-09-18. Aucun test de pointeur.
    /// </summary>
    private static void EntityHandler_00420480(byte* e) => TickShockwaveOrSpray(e, AbsentHost.Unchecked);

    /// <summary>
    /// Ghidra: <c>0x00422e50</c> — creneau <b>31</b> du bloc <c>0x00558ba8</c> (<c>0x0056b1c8</c>,
    /// sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b>, 266 instructions ; dump sous
    /// <c>Legacy/Raw/Blaze/FUN_00422E50.asm</c>. <see cref="EntityHandler_00420480"/> avec les
    /// replis de son bloc : le porteur nul, puis le porteur du porteur nul, deviennent chacun
    /// <c>Res.s_EntityList_005de558</c>. Ses deux feuilles (<c>0x004231c0</c>,
    /// <c>0x00423200</c>) sont les memes, dans le meme ordre.
    /// </summary>
    private static void EntityHandler_00422E50(byte* e) => TickShockwaveOrSpray(e, AbsentHost.FallBackOnEntityList);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00420480"/> et de <see cref="EntityHandler_00422E50"/>.
    /// Deux vies, choisies par <c>+0x5c</c>.
    ///
    /// <para>
    /// <b>Mode 0 : l'onde.</b> Le modele <c>7</c>, blanc (<c>0xfa</c>), nait plat — echelles
    /// horizontales a zero, profondeur <c>(0x19 - +0x12) × 400</c> — et s'etale : ses deux echelles
    /// rejoignent <c>0x7530</c> par huitiemes pendant qu'elle palit (<c>-7, -7, -5</c>) ; elle
    /// meurt au bleu nul. <b>Tant que son echelle reste sous <c>0x6978</c></b> et que <c>+0x16</c>
    /// est nul, chaque membre vivant a moins de <c>echelle / 50</c> unites a plat — le rayon
    /// grandit avec l'onde — est frappe <b>une seule fois</b> (un bit par emplacement dans
    /// <c>+0x14</c>) au nom du <b>porteur du porteur</b> (<c>+0x6c</c> puis <c>+0x6c</c>) :
    /// l'effet <c>0x0053BB20</c> si <c>+0x44</c> est nul, <c>0x0053BC60</c> sinon, chacun garde
    /// par <see cref="IsEffectPairBlocked_004364B0"/>.
    /// </para>
    /// <para>
    /// <b>Mode 1 : la gerbe.</b> Le modele <c>0x30</c>, trois echelles tirees dans
    /// <c>[200, 400[</c>, un cap et un <c>+0x30</c> tires, une hauteur relevee de
    /// <c>rand() % 200</c>. Il tourne de <c>-0x96</c> par image et grossit de 100 sur les trois
    /// axes : cinq images a s'allumer vers <c>0xfa</c> par moities, vingt a s'eteindre par
    /// huitiemes.
    /// </para>
    /// <para>
    /// <b>La cible est rangee dans l'emplacement de l'argument</b> (<c>[esp+18h]</c>), comme dans
    /// les lots 8 a 10.
    /// </para>
    /// </summary>
    private static void TickShockwaveOrSpray(byte* e, AbsentHost absent, bool clearFlagBit0 = true)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null && absent == AbsentHost.FallBackOnEntityList) host = Res.s_EntityList_005de558;
        byte* caster = *(byte**)(host + 0x6c);
        if (caster == null && absent == AbsentHost.FallBackOnEntityList) caster = Res.s_EntityList_005de558;

        if (*(int*)(e + 0x5c) == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                short depth = (short)((0x19 - *(short*)(e + 0x12)) * 400);   // Ghidra: lea ×5, lea ×5, shl 4
                e[0x04] = 7;
                e[0x0a] = 2;
                e[0x0b] = 0;
                e[0x38] = 0xfa;
                e[0x39] = 0xfa;
                e[0x3a] = 0xfa;
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x2a) = depth;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x2e) = -0x3c;                          // Ghidra: 0xffc4
                // Ghidra: 0x0042A540 (chateau) fait `or byte [esi+36h],2` — il NE nettoie PAS le bit 0.
                e[0x36] = clearFlagBit0 ? (byte)((e[0x36] & 0xfe) | 2) : (byte)(e[0x36] | 2);
            }

            if (*(short*)(e + 0x10) == 1) StepShockwave(e, caster);
        }

        if (*(int*)(e + 0x5c) != 1) return;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x30;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = (short)(CrtRand() % 200 + 200);   // Ghidra: FUN_00547140
            *(short*)(e + 0x2a) = (short)(CrtRand() % 200 + 200);
            *(short*)(e + 0x2c) = (short)(CrtRand() % 200 + 200);
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);      // Ghidra: signe-module
            int spin = CrtRand() % 500;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x30) = (short)spin;
            int lift = CrtRand() % 200;
            *(short*)(e + 0x2e) = -0x3c;
            *(int*)(e + 0x20) += lift << 0xc;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) -= 0x96;
            e[0x38] = (byte)(e[0x38] + ((0xfa - e[0x38]) >> 1));
            e[0x39] = (byte)(e[0x39] + ((0xfa - e[0x39]) >> 1));
            e[0x3a] = (byte)(e[0x3a] + ((0xfa - e[0x3a]) >> 1));

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 5)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }

            *(short*)(e + 0x28) += 100;
            *(short*)(e + 0x2a) += 100;
            *(short*)(e + 0x2c) += 100;
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x32) -= 0x96;
        e[0x38] = (byte)(e[0x38] + (-e[0x38] >> 3));
        e[0x39] = (byte)(e[0x39] + (-e[0x39] >> 3));
        e[0x3a] = (byte)(e[0x3a] + (-e[0x3a] >> 3));

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) > 0x14) *(uint*)e &= 0x7fffffff;

        *(short*)(e + 0x28) += 100;
        *(short*)(e + 0x2a) += 100;
        *(short*)(e + 0x2c) += 100;
    }

    /// <summary>L'etat 1 du mode 0 de <see cref="TickShockwaveOrSpray"/> : l'onde s'etale et
    /// frappe.</summary>
    private static void StepShockwave(byte* e, byte* caster)
    {
        BrightenEntityTint_00404DE0(e, -7, -7, -5);

        short width = *(short*)(e + 0x28);
        width = (short)(((0x7530 - width) >> 3) + width);
        *(short*)(e + 0x28) = width;
        short length = *(short*)(e + 0x2c);
        *(short*)(e + 0x2c) = (short)(((0x7530 - length) >> 3) + length);

        if (e[0x3a] == 0) *(uint*)e &= 0x7fffffff;

        if (*(short*)(e + 0x16) != 0) return;
        if (width >= 0x6978) return;

        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(slot + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dz = (*(int*)(slot + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));   // Ghidra: 0x00545a10

            // Ghidra: `imul 51EB851Fh ; sar edx,4` — la division signee par 50, relue a chaque tour.
            if (distance >= *(short*)(e + 0x28) / 50) continue;

            int bit = 1 << i;
            if ((*(short*)(e + 0x14) & bit) != 0) continue;
            *(ushort*)(e + 0x14) |= (ushort)bit;

            uint* attacker = (uint*)caster;
            uint* victim = (uint*)slot;
            if (IsEffectPairBlocked_004364B0(attacker, victim) != 0) continue;   // les deux feuilles

            if (*(int*)(e + 0x44) == 0) ApplyStatusEffect_0053BB20(attacker, victim, 0);   // Ghidra: FUN_004207e0
            else ApplyStatusEffect_0053BC60(attacker, victim, 0);                          // Ghidra: FUN_00420820
        }
    }

    /// <summary>
    /// Ghidra: FUN_00421d80 @ 0x00421d80 — <b>l'effet 32 si la paire l'accepte</b>. Decompilee
    /// dans <c>Blaze.c</c>, 61 octets ; la seule appelee de <see cref="EntityHandler_00421C10"/>
    /// que le C# ne connaissait pas. La forme de <see cref="HealTargetIfEligible_00447FE0"/> :
    /// refus du garde, <c>true</c> sans rien faire ; sinon le resultat de l'effet, vrai quand il a
    /// ete refuse.
    /// </summary>
    private static bool ApplyStatusEffect32IfAllowed_00421D80(uint* attacker, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(attacker, victim) != 0) return true;

        return ApplyStatusEffect_0053C5E0(attacker, victim, keep) != 0;
    }

    // ---------------------------------------------------------------------
    // CRENEAU 6 DE 0x00558ba8 (0x0056b1c8, SOUS-ZONES 4 ET 9) — LE BALAYAGE DEVANT LE PORTEUR,
    // ET SON JUMEAU DE LA GROTTE SCELLEE
    // ---------------------------------------------------------------------

    /// <summary>
    /// Ghidra: <c>0x004224f0</c> — creneau <b>6</b> du bloc <c>0x00558ba8</c> (<c>0x0056b1c8</c>,
    /// sous-zones 4 et 9) ; seme <c>0xee</c>. <b>Absente de <c>Blaze.c</c></b>, 389 instructions
    /// et une table de sauts a neuf cas en <c>0x004229f0</c> ; dump sous
    /// <c>Legacy/Raw/Blaze/FUN_004224F0.asm</c>. Lot 9, 2026-09-18.
    /// </summary>
    private static void EntityHandler_004224F0(byte* e) => TickOwnerFrontSweep(e, 0xee);

    /// <summary>
    /// Ghidra: <c>0x0043f4e0</c> — <b>grotte scellee</b>, sous-zones 13 a 15 (<c>0x0056b1c8</c>) ;
    /// seme <c>0xdf</c>. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/FUN_0043F4E0.asm</c>. <see cref="EntityHandler_004224F0"/> a <b>une
    /// instruction</b> pres — le <c>push</c> du sous-type seme —, et ses neuf feuilles
    /// (<c>0x0043f060</c>..<c>0x0043fa50</c>, emises dans un autre ordre en memoire) appellent les
    /// memes effets <b>dans le meme ordre de table</b>, verifie cas par cas.
    /// </summary>
    private static void EntityHandler_0043F4E0(byte* e) => TickOwnerFrontSweep(e, 0xdf);

    /// <summary>
    /// Ghidra: <c>0x0041c900</c> — creneau <b>30</b> du bloc <c>0x00557ea8</c> (<c>0x0056b1c8</c>,
    /// sous-zones 0 a 3) ; seme <c>0xee</c>. <b>Absente de <c>Blaze.c</c></b>, 396 instructions ;
    /// dump sous <c>Legacy/Raw/Blaze/FUN_0041C900.asm</c>. Lot 10, 2026-09-18.
    /// <see cref="EntityHandler_004224F0"/> <b>avec un autre repli</b> : proprietaire et ancre nuls
    /// deviennent un enregistrement de <c>0x9c</c> octets mis a zero sur la pile a chaque image
    /// (<c>sub esp,0C0h</c> puis <c>rep stosd</c>, 0x26 mots plus un). Les 211 lignes d'ecart
    /// au modele sont ce tampon et l'allocation de registres qu'il deplace ; les trois
    /// coordonnees gardees changent d'emplacement de pile mais sont rendues aux memes champs, et la
    /// table <c>0x0041ce20</c> appelle ses neuf feuilles (<c>0x0041ce50</c>..<c>0x0041d050</c>)
    /// dans l'ordre du modele — verifie cas par cas.
    /// </summary>
    private static void EntityHandler_0041C900(byte* e)
        => TickOwnerFrontSweep(e, 0xee, AbsentHost.FallBackOnBlankRecord);

    /// <summary>
    /// Ghidra: <c>0x0041edd0</c> — creneau <b>30</b> du bloc <c>0x00558528</c> (<c>0x0056b1c8</c>,
    /// sous-zones 5 a 8) ; seme <c>0xee</c>. <b>Absente de <c>Blaze.c</c></b>, 380 instructions ;
    /// dump sous <c>Legacy/Raw/Blaze/FUN_0041EDD0.asm</c>. Lot 10, 2026-09-18.
    /// <see cref="EntityHandler_004224F0"/> <b>sans aucun repli</b> : <c>+0x74</c> et <c>+0x6c</c>
    /// sont lus tels quels. 69 lignes d'ecart au modele, toutes d'allocation de registres ; table
    /// <c>0x0041f2b0</c>, feuilles <c>0x0041f2e0</c>..<c>0x0041f4e0</c>, meme ordre.
    /// </summary>
    private static void EntityHandler_0041EDD0(byte* e)
        => TickOwnerFrontSweep(e, 0xee, AbsentHost.Unchecked);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_004224F0"/> et de <see cref="EntityHandler_0043F4E0"/>.
    ///
    /// <para>
    /// <b>Une nappe qui se deploie devant son proprietaire et marque qui s'y trouve.</b> Deux
    /// pointeurs, chacun replie sur <c>Res.s_EntityList_005de558</c> s'il est nul : le
    /// <b>proprietaire</b> <c>+0x74</c> — son cap et sa position — et l'<b>ancre</b> <c>+0x6c</c> —
    /// dont seuls <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> sont lus, ajoutes a la position. A chaque
    /// image la nappe se replace a <b>15</b> fois le sinus de <c>+0x5c</c> plus son cap autour du
    /// proprietaire, prend son cap plus <c>0xc00</c>, puis avance de <c>+0x16</c> dans ce cap moins
    /// <c>0xc00</c>. La portee <c>+0x16</c> tend vers <c>0x258</c> et l'echelle <c>+0x2a</c> vers
    /// <c>0x4000</c>, par quarts.
    /// </para>
    /// <para>
    /// <b>Le balayage</b>, etat 1 seulement, depuis la position avancee : chaque membre vivant a
    /// moins de <b>200</b> unites et dans un <b>cone de ±0x320</b> autour du cap du proprietaire
    /// (<c>(ratan2 - cap) &amp; 0xfff</c> dans <c>]0, 0x320[</c> ou <c>]0xce0, 0x1000[</c>) est
    /// marque <b>une seule fois</b> — un bit par emplacement dans <c>+0x44</c> — et recoit, au nom
    /// du proprietaire, un effet choisi par <c>+0x9a</c> : 0, l'effet 7 ; 2, l'effet 8 ; 1, l'un
    /// des neuf de la table, tire par <c>rand() % 9</c>. Chaque feuille passe d'abord par
    /// <see cref="IsEffectPairBlocked_004364B0"/>. La couleur de naissance dit lequel : bleu-vert,
    /// rouge, magenta.
    /// </para>
    /// <para>
    /// <b>La position de balayage n'est pas celle qu'on dessine</b> : apres le balayage et
    /// <see cref="FinalizeEntityPlacement2"/>, les trois coordonnees d'avant l'avancee sont
    /// rendues — gardees dans trois mots de pile. L'etat 1 dure dix images, ou s'interrompt des
    /// que <c>+0x90</c> porte un bit de <c>0x8802</c> (le compte, remis a 10, perd encore un dans
    /// la meme image). L'etat 2 garde la nappe en place, echelle et teinte decroissant vers zero
    /// — d'un quart et d'un huitieme par image —, puis l'eteint apres dix images.
    /// </para>
    /// </summary>
    private static void TickOwnerFrontSweep(
        byte* e, byte seed, AbsentHost absent = AbsentHost.FallBackOnEntityList)
    {
        // Ghidra: le repli de 0x0041C900, 0x9c octets de pile remis a zero a chaque image.
        byte* blank = stackalloc byte[0x9c];
        for (int i = 0; i < 0x9c; i++) blank[i] = 0;

        // Ghidra: le repli est la seule difference des trois blocs — voir AbsentHost.
        byte* fallback = absent == AbsentHost.FallBackOnBlankRecord ? blank : Res.s_EntityList_005de558;

        byte* owner = *(byte**)(e + 0x74);
        if (owner == null && absent != AbsentHost.Unchecked) owner = fallback;   // Ghidra: range dans [esp+34h], l'argument
        byte* anchor = *(byte**)(e + 0x6c);
        if (anchor == null && absent != AbsentHost.Unchecked) anchor = fallback;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0;
            *(short*)(e + 0x28) = 0xfa;
            *(short*)(e + 0x2c) = 0xfa;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x12) = 0x0a;

            short kind = *(short*)(e + 0x9a);
            if (kind == 0) { e[0x38] = 0; e[0x39] = 0x82; e[0x3a] = 0x96; }
            if (kind == 1) { e[0x38] = 0x96; e[0x39] = 0; e[0x3a] = 0; }
            if (kind == 2) { e[0x38] = 0x96; e[0x39] = 0; e[0x3a] = 0x96; }

            e[0x36] &= 0xfe;
            *(short*)(e + 0x16) = 0;
            *(int*)(e + 0x44) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            PlaceInFrontOfOwner(e, owner, anchor);

            if (*(short*)(e + 0x12) == 0x0a)
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, seed, 0, -1);

            int keptX = *(int*)(e + 0x1c);
            int keptY = *(int*)(e + 0x20);
            int keptZ = *(int*)(e + 0x24);

            StepAlongOwnerCourse(e);

            byte* slot = Inp.s_SlotFlags_005e93c0;
            for (int i = 0; i < 4; i++, slot += 0x9c)
            {
                uint flags = *(uint*)slot;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                int dx = (*(int*)(slot + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dy = (*(int*)(slot + 0x20) - *(int*)(e + 0x20)) >> 0xc;
                int dz = (*(int*)(slot + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                int bearing = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(owner + 0x32)) & 0xfff;   // Ghidra: 0x00545a22
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));      // Ghidra: 0x00545a10
                if (distance >= 200) continue;

                bool inCone = (bearing < 0x320 && bearing > 0) || (bearing < 0x1000 && bearing > 0xce0);
                if (!inCone) continue;

                int bit = 1 << i;
                if ((*(int*)(e + 0x44) & bit) != 0) continue;
                *(int*)(e + 0x44) |= bit;

                if (*(short*)(e + 0x9a) == 0) SweepLeaf_004224F0(1, owner, slot);

                if (*(short*)(e + 0x9a) == 1)
                {
                    int leaf = (short)(CrtRand() % 9);                       // Ghidra: FUN_00547140
                    if ((uint)leaf <= 8) SweepLeaf_004224F0(leaf, owner, slot);   // Ghidra: jmp [eax*4+4229F0h]
                }

                if (*(short*)(e + 0x9a) == 2) SweepLeaf_004224F0(3, owner, slot);
            }

            FinalizeEntityPlacement2((uint*)e);                       // Ghidra: FUN_00533940

            *(int*)(e + 0x24) = keptZ;
            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x20) = keptY;

            short scale = *(short*)(e + 0x2a);
            *(short*)(e + 0x2a) = (short)(((0x4000 - scale) >> 2) + scale);
            short reach = *(short*)(e + 0x16);
            *(short*)(e + 0x16) = (short)(((0x258 - reach) >> 2) + reach);

            if ((*(uint*)(e + 0x90) & 0x8802) != 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0x0a;
            }

            (*(short*)(e + 0x12))--;
            if (*(short*)(e + 0x12) <= 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0x0a;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        PlaceInFrontOfOwner(e, owner, anchor);
        StepAlongOwnerCourse(e);
        *(short*)(e + 0x34) = 0xc00;

        short fading = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)((-fading >> 2) + fading);

        e[0x38] = (byte)(e[0x38] + (-e[0x38] >> 3));
        e[0x39] = (byte)(e[0x39] + (-e[0x39] >> 3));
        e[0x3a] = (byte)(e[0x3a] + (-e[0x3a] >> 3));

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>La mise en place que <see cref="TickOwnerFrontSweep"/> ecrit deux fois a
    /// l'identique : inclinaison <c>0x400</c>, cap du proprietaire plus <c>0xc00</c>, hauteur du
    /// proprietaire plus <c>anchor+0x54</c>, et X/Z a 15 fois le sinus/cosinus de
    /// <c>+0x5c + cap - 0xc00</c> autour du proprietaire, plus <c>anchor+0x50</c>/<c>+0x58</c>.</summary>
    private static void PlaceInFrontOfOwner(byte* e, byte* owner, byte* anchor)
    {
        *(short*)(e + 0x34) = 0x400;
        *(short*)(e + 0x32) = (short)(*(short*)(owner + 0x32) + 0xc00);
        *(int*)(e + 0x20) = *(int*)(owner + 0x20) + *(int*)(anchor + 0x54);

        int angle = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * 15 + *(int*)(owner + 0x1c) + *(int*)(anchor + 0x50);   // Ghidra: 0x00545a1c
        angle = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * 15 + *(int*)(owner + 0x24) + *(int*)(anchor + 0x58);        // Ghidra: 0x00545a16
    }

    /// <summary>L'avancee de <c>+0x16</c> dans le cap moins <c>0xc00</c> — le demi-tour que
    /// <see cref="PlaceInFrontOfOwner"/> avait ajoute.</summary>
    private static void StepAlongOwnerCourse(byte* e)
    {
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) - 0xc00) * *(short*)(e + 0x16);
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) - 0xc00) * *(short*)(e + 0x16);
    }

    /// <summary>
    /// Les neuf feuilles de la table de sauts <c>0x004229f0</c>, dans l'ordre des cas. Chacune est
    /// un corps de 61 octets de la forme de <see cref="ApplyStatusEffect32IfAllowed_00421D80"/> —
    /// <see cref="IsEffectPairBlocked_004364B0"/> puis un effet —, et leur resultat n'est pas lu.
    /// Les neuf sont identiques au deplacement des deux <c>call</c> pres.
    /// </summary>
    private static void SweepLeaf_004224F0(int leaf, byte* owner, byte* victim)
    {
        uint* attacker = (uint*)owner;
        uint* target = (uint*)victim;
        if (IsEffectPairBlocked_004364B0(attacker, target) != 0) return;

        switch (leaf)
        {
            case 0: ApplyTrapEffect_0053C420(attacker, target, 0); break;    // Ghidra: FUN_00422be0
            case 1: ApplyStatusEffect_0053B3C0(attacker, target, 0); break;  // Ghidra: FUN_00422a20
            case 2: ApplyStatusEffect_0053BF00(attacker, target, 0); break;  // Ghidra: FUN_00422ba0
            case 3: ApplyStatusEffect_0053B4B0(attacker, target, 0); break;  // Ghidra: FUN_00422a60
            case 4: ApplyStatusEffect_0053B660(attacker, target, 0); break;  // Ghidra: FUN_00422ae0
            case 5: ApplyStatusEffect_0053B580(attacker, target, 0); break;  // Ghidra: FUN_00422aa0
            case 6: ApplyStatusEffect_0053B8F0(attacker, target, 0); break;  // Ghidra: FUN_00422b20
            case 7: ApplyStatusEffect_0053C510(attacker, target, 0); break;  // Ghidra: FUN_00422c20
            case 8: ApplyStatusEffect_0053B9C0(attacker, target, 0); break;  // Ghidra: FUN_00422b60
        }
    }

    // ---------------------------------------------------------------------
    // CRENEAU 6 DE 0x00558ca8 (0x0056b2c8, SOUS-ZONES 4 ET 9) — L'ESCARBILLE QUI RETOMBE
    // ---------------------------------------------------------------------

    /// <summary>Ghidra: <c>&amp;DAT_00550430</c> — la planche de <see cref="EntityHandler_00424C70"/>.
    /// <b>Un</b> enregistrement : le corps pose <c>+0x40</c> a zero et ne le touche plus, et
    /// <c>0x00550438</c> — deja releve, <see cref="s_Descriptors_00550438"/> — suit.</summary>
    private static readonly byte[] s_Descriptors_00550430 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00550430  <- 0x00424C70
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550410</c> — la planche de <see cref="EntityHandler_00424AE0"/>.
    /// <b>Quatre</b> tuiles <c>0x20 x 0x20</c> sur une meme rangee ; le corps compte <c>+0x40</c>
    /// de 0 a 3 et meurt a 4, et <see cref="s_Descriptors_00550430"/> suit.</summary>
    private static readonly byte[] s_Descriptors_00550410 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550410  <- 0x00424AE0
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550418
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550420
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550428
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550388</c> — la planche de <see cref="EntityHandler_004250B0"/>.
    /// <b>Huit</b> tuiles <c>0x1f</c> de la page <c>0x08</c> sur une rangee — les memes que les huit
    /// de queue de <see cref="s_Descriptors_0054FF80"/> ; le corps compte <c>+0x40</c> de 0 a 7 et
    /// meurt a 8. La borne est double : <c>0x005503c8</c> change de forme
    /// (<c>08 08 07 07</c>).</summary>
    private static readonly byte[] s_Descriptors_00550388 =
    [
        0x00, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550388  <- 0x004250B0
        0x20, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550390
        0x40, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550398
        0x60, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x005503a0
        0x80, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x005503a8
        0xa0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x005503b0
        0xc0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x005503b8
        0xe0, 0x30, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x005503c0
    ];

    private static byte* s_Descriptors00550388Storage;
    private static byte* Descriptors00550388() => NativeCopy(s_Descriptors_00550388, ref s_Descriptors00550388Storage);

    private static byte* s_Descriptors00550430Storage;
    private static byte* s_Descriptors00550410Storage;

    private static byte* Descriptors00550430() => NativeCopy(s_Descriptors_00550430, ref s_Descriptors00550430Storage);
    private static byte* Descriptors00550410() => NativeCopy(s_Descriptors_00550410, ref s_Descriptors00550410Storage);

    /// <summary>
    /// Ghidra: <c>0x00424ae0</c> — creneau <b>5</b> du bloc <c>0x00558ca8</c> (<c>0x0056b2c8</c>,
    /// sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>114
    /// instructions</b>, dump sous <c>Legacy/Raw/Blaze/FUN_00424AE0.asm</c>.
    ///
    /// <para>
    /// <b>Une spirale qui s'ouvre en montant, et qui palit.</b> A la naissance elle memorise sa
    /// position (<c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, la hauteur relevee de <c>0x32</c> unites
    /// d'abord), tire un cap dans <c>[0, 0x1000[</c>, un pas d'elargissement de
    /// <c>10 + rand() % 10</c> et une montee de <c>3 + rand() % 5</c>, et prend une teinte grise
    /// <c>0x80</c>. Ensuite, chaque image, elle se replace sur le cercle de rayon <c>+0x5c</c>
    /// autour du point memorise — le rayon grandit de son pas, <b>apres</b> usage —, monte, et
    /// palit de 2 par canal.
    /// </para>
    /// <para>
    /// <b>Le cap ne tourne pas</b> : <c>+0x12</c> n'est ecrit qu'a la naissance. Ce n'est donc pas
    /// une spirale mais une <b>ligne droite oblique</b> qui s'eloigne du centre ; le mot ci-dessus
    /// est celui de l'effet a l'ecran, pas de la trajectoire.
    /// </para>
    /// <para>
    /// Deux sons a ses deux premieres images, le <c>0x22</c> a deux hauteurs ; la planche avance
    /// toutes les quatre images et la quatrieme la tue — seize images de vie.
    /// </para>
    /// </summary>
    private static void EntityHandler_00424AE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(byte**)(e + 0x3c) = Descriptors00550410();          // Ghidra: 550410h
            e[0x40] = 0;
            *(short*)(e + 0x28) = 0x2000;
            *(short*)(e + 0x2a) = 0x2000;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);     // Ghidra: le reste signe, en signe-module
            *(short*)(e + 0x14) = (short)(CrtRand() % 10 + 10);
            *(int*)(e + 0x5c) = 0;
            int rise = CrtRand() % 5;
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x98) = 0;

            *(int*)(e + 0x20) += 0x32000;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(short*)(e + 0x16) = (short)(rise + 3);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;
        }

        if (*(short*)(e + 0x10) != 1) return;

        if (*(short*)(e + 0x98) == 0) Aud.PlaySoundEffect(0x22, 0x400, 0x1000, 0x1000);   // Ghidra: FUN_0053cea0
        if (*(short*)(e + 0x98) == 1) Aud.PlaySoundEffect(0x22, 0x200, 0xe00, 0xe00);
        (*(short*)(e + 0x98))++;

        short course = *(short*)(e + 0x12);
        int radius = *(int*)(e + 0x5c);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);   // Ghidra: 0x00545a1c
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);        // Ghidra: 0x00545a16
        *(int*)(e + 0x20) += *(short*)(e + 0x16) << 0xc;

        *(int*)(e + 0x5c) = *(short*)(e + 0x14) + radius;
        (*(short*)(e + 0x9a))++;

        BrightenEntityTint_00404DE0(e, -2, -2, -2);

        if (*(short*)(e + 0x9a) <= 3) return;

        *(short*)(e + 0x9a) = 0;
        e[0x40]++;
        if (e[0x40] == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00424c70 @ 0x00424c70 — creneau <b>6</b> du bloc <c>0x00558ca8</c>
    /// (<c>0x0056b2c8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 106 instructions.
    ///
    /// <para>
    /// <b>Une escarbille lancee en l'air, qui retombe et s'eteint.</b> A la naissance : une echelle
    /// tiree dans <c>[0x800, 0x1000[</c>, commune aux deux axes ; une vitesse horizontale de
    /// <c>±15</c> cases sur X et Z ; une vitesse verticale de <c>8 + rand() % 5</c> ; et une
    /// hauteur relevee de <c>0x96</c> unites plus <c>(+0x14 &amp; ~0x1f) * 0x80</c>. Ensuite elle
    /// avance, et sa vitesse verticale perd un par image : elle monte, ralentit, redescend.
    /// </para>
    /// <para>
    /// <b>Trois tirages sont jetes</b> : le binaire tire d'abord une vitesse de <c>±5</c> cases et
    /// une montee de <c>15 + rand() % 15</c>, puis les ecrase par les valeurs ci-dessus avant de
    /// les avoir lues. Ils ne changent rien a l'escarbille, mais ils consomment le flux du hasard —
    /// ils sont donc transcrits.
    /// </para>
    /// <para>
    /// <b>La fin depend de <c>+0x14</c></b>, pose par le parent : a zero, l'escarbille commence a
    /// palir quand sa vitesse passe sous <c>-5</c> ; sinon, sous <c>-10</c>. Elle palit de
    /// <c>10</c> par canal et par image (<see cref="BrightenEntityTint_00404DE0"/>) et meurt quand
    /// le canal rouge <c>+0x38</c> touche zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_00424C70(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00550430();          // Ghidra: &DAT_00550430
            e[0x40] = 0;
            e[0x0a] = 0xc;
            e[0x0b] = 0;

            // Ghidra: le reste signe de `rand() % 0x800`, ecrit en arithmetique de signe-module.
            short scale = (short)(CrtRand() % 0x800 + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            // Les trois tirages jetes — voir le resume.
            *(int*)(e + 0x50) = (CrtRand() % 10 - 5) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % 10 - 5) * 0x1000;
            *(int*)(e + 0x54) = CrtRand() % 0xf + 0xf;
            *(int*)(e + 0x20) += 0x96000;

            *(int*)(e + 0x50) = (CrtRand() % 0x1e - 0xf) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % 0x1e - 0xf) * 0x1000;
            *(int*)(e + 0x54) = CrtRand() % 5 + 8;
            *(int*)(e + 0x20) += (int)((*(short*)(e + 0x14) & 0xffffffe0u) * 0x80);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);

        int climb = *(int*)(e + 0x54);
        *(int*)(e + 0x20) += climb * 0x1000;
        *(int*)(e + 0x54) = --climb;

        short tail = *(short*)(e + 0x14);
        if ((climb < -5 && tail == 0) || (climb < -10 && tail != 0))
        {
            BrightenEntityTint_00404DE0(e, -10, -10, -10);
            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    // ---------------------------------------------------------------------
    // CRENEAU 5 DE 0x00558ba8 (0x0056b1c8, SOUS-ZONES 4 ET 9) — LE MODELE QUI S'OUVRE EN TOURNANT
    // ---------------------------------------------------------------------

    /// <summary>
    /// Ghidra: FUN_00424800 @ 0x00424800 — creneau <b>5</b> du bloc <c>0x00558ba8</c>
    /// (<c>0x0056b1c8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 125 instructions.
    ///
    /// <para>
    /// <b>Un modele de zone qui s'ouvre en tournant, tient, puis s'eteint.</b> Pas une planche : le
    /// genre <c>+4</c> est <see cref="s_Unknown_005edec4"/> <c>+ 5</c>, le sixieme modele propre a
    /// la zone — le meme adressage que le decor de la mine qui ajoute <c>+0x14</c> a cette base.
    /// Il nait a echelle nulle sur les trois axes.
    /// </para>
    /// <para>
    /// <b>Etat 1, l'ouverture</b> : l'echelle de profondeur <c>+0x2a</c> gagne <c>0x28a</c> par
    /// image jusqu'au plafond <c>+0x5c</c>, les deux autres gagnent <c>400</c> jusqu'au plafond
    /// <c>+0x9a</c>. Il passe a l'etat 2 quand <b>les deux</b> plafonds sont atteints — le test
    /// ne se fait qu'a l'image ou <c>+0x2a</c> est ramene au sien. Le cap <c>+0x32</c> tourne de
    /// <c>+0x98</c> par image, dans les deux etats. Plafonds et vitesse sont poses par le parent.
    /// </para>
    /// <para>
    /// <b>Etat 2, la tenue</b> : il tourne, et <c>+0x16</c> — qui compte les images depuis la
    /// naissance, etats confondus — decide du reste : sous 100 le cycle de sons continue, au-dela
    /// de <c>0x59</c> il palit de 10 par canal et meurt au rouge nul. Les deux fenetres se
    /// chevauchent de <c>0x5a</c> a <c>0x63</c> : il palit en sonnant encore.
    /// </para>
    /// </summary>
    private static void EntityHandler_00424800(byte* e)
    {
        byte model = s_Unknown_005edec4;                          // Ghidra: lu AVANT le test d'etat

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x0a] = 9;
            e[0x0b] = 0;
            e[0x04] = (byte)(model + 5);
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            StepOpeningModelSounds(e);

            *(short*)(e + 0x2a) += 0x28a;
            *(short*)(e + 0x32) += *(short*)(e + 0x98);
            *(short*)(e + 0x28) += 400;
            *(short*)(e + 0x2c) += 400;

            short ceiling = *(short*)(e + 0x9a);
            if (ceiling < *(short*)(e + 0x28))
            {
                *(short*)(e + 0x28) = ceiling;
                *(short*)(e + 0x2c) = ceiling;
            }

            if (*(int*)(e + 0x5c) < *(short*)(e + 0x2a))
            {
                *(short*)(e + 0x2a) = (short)*(int*)(e + 0x5c);
                if (*(short*)(e + 0x28) == ceiling) *(short*)(e + 0x10) = 2;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(e + 0x16) < 100) StepOpeningModelSounds(e);

            *(short*)(e + 0x32) += *(short*)(e + 0x98);

            if (*(short*)(e + 0x16) > 0x59)
            {
                BrightenEntityTint_00404DE0(e, -10, -10, -10);
                if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        (*(short*)(e + 0x16))++;
    }

    /// <summary>
    /// Ghidra: <c>0x004250b0</c> — creneau <b>58</b> du bloc <c>0x00558ca8</c> (<c>0x0056b2c8</c>,
    /// sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>136
    /// instructions</b>, dump sous <c>Legacy/Raw/Blaze/FUN_004250B0.asm</c>.
    ///
    /// <para>
    /// <b>Une fumee qui derive en tourbillonnant et gonfle.</b> A la naissance : un cap tire sur un
    /// tour, une echelle dans <c>[0x800, 0xc00[</c>, une derive de <c>±5</c> cases sur X et Z, une
    /// vitesse verticale de <c>2 + rand() % 3</c>, une cadence de planche de meme loi, et une
    /// teinte tiree <b>canal par canal</b> dans <c>[0x80, 0xb2[</c>. Elle ne sonne pas.
    /// </para>
    /// <para>
    /// Chaque image elle derive, sa vitesse verticale gagne un, puis elle s'ecarte sur le cercle
    /// de son cap d'un rayon <c>+0x14</c> qui <b>croit d'un par image</b> — mais sans centre
    /// memorise : l'ecart s'ajoute a la position courante, il s'<i>accumule</i>. Le cap tourne de
    /// <c>0x64</c>, la profondeur gagne <c>0x32</c>. Tous les <c>+0x16</c> images, les deux echelles
    /// gagnent encore <c>0x12c</c> et la planche avance ; la huitieme la tue.
    /// </para>
    /// </summary>
    private static void EntityHandler_004250B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00550388();          // Ghidra: 550388h
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;

            int course = CrtRand() % 0x1000;                        // Ghidra: le reste signe, en signe-module
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x12) = (short)course;

            short scale = (short)(CrtRand() % 0x400 + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x50) = CrtRand() % 10 - 5;
            *(int*)(e + 0x58) = CrtRand() % 10 - 5;
            *(int*)(e + 0x54) = CrtRand() % 3 + 2;
            int cadence = CrtRand() % 3;
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x16) = (short)(cadence + 2);

            e[0x38] = (byte)(CrtRand() % 0x32 + 0x80);
            e[0x39] = (byte)(CrtRand() % 0x32 + 0x80);
            e[0x3a] = (byte)(CrtRand() % 0x32 + 0x80);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x24) += *(int*)(e + 0x58) << 0xc;
        *(int*)(e + 0x1c) += *(int*)(e + 0x50) << 0xc;
        int fall = *(int*)(e + 0x54);
        *(int*)(e + 0x20) += fall << 0xc;
        *(int*)(e + 0x54) = fall + 1;

        short swirl = *(short*)(e + 0x14);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12)) * swirl;   // Ghidra: 0x00545a1c
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x12)) * swirl;        // Ghidra: 0x00545a16

        *(short*)(e + 0x12) += 0x64;
        *(short*)(e + 0x2a) += 0x32;
        *(short*)(e + 0x14) = (short)(swirl + 1);
        (*(short*)(e + 0x9a))++;

        if (*(short*)(e + 0x9a) < *(short*)(e + 0x16)) return;

        *(short*)(e + 0x28) += 0x12c;
        *(short*)(e + 0x9a) = 0;
        *(short*)(e + 0x2a) += 0x12c;

        e[0x40]++;
        if (e[0x40] == 8) *(uint*)e &= 0x7fffffff;
    }

    // ---------------------------------------------------------------------
    // CRENEAU 27 DE 0x00558aa8 (0x0056b0c8, SOUS-ZONES 4 ET 9) — LE TIREUR QUI VISE LE PORTEUR DE SON PORTEUR
    // ---------------------------------------------------------------------

    /// <summary>Ghidra: <c>&amp;DAT_005526a8</c> — le script de sons differes de
    /// <see cref="EntityHandler_00483F30"/>, celui que <see cref="s_DelayedSoundScript_00552690"/>
    /// annoncait « d'un autre site d'appel ». <b>Trois</b> enregistrements : le troisieme porte un
    /// <b>delai nul</b>, la marque de fin, et <c>0x005526c0</c> commence le script suivant.</summary>
    private static readonly byte[] s_DelayedSoundScript_005526A8 =
    [
        0x1a, 0x01, 0x01, 0x00, 0x2a, 0x03, 0x00, 0x18,   // 0x005526a8
        0x18, 0x01, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x005526b0
        0x1b, 0x00, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x005526b8 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript005526A8Storage;

    /// <summary>
    /// Ghidra: FUN_00483f30 @ 0x00483f30 — creneau <b>27</b> du bloc <c>0x00558aa8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 139 instructions,
    /// <b>avec un parametre perdu</b> ; dump sous <c>Legacy/Raw/Blaze/FUN_00483F30.asm</c>.
    ///
    /// <para>
    /// <b>Deux vies selon le bit 1 du mot de tete.</b> Sans lui, c'est une naissance qui se
    /// dessine : genre <c>0x36</c> a echelle nulle, il fait naitre l'enfant de la table de zone
    /// designe par l'octet <c>+0x15</c> du repertoire <c>+0xd0</c>, puis se releve de
    /// <c>0x190</c> unites, prend l'echelle <c>0x1800</c> et une teinte grise, devient le genre
    /// <c>0x35</c> et met en file le script de sons <see cref="s_DelayedSoundScript_005526A8"/>.
    /// Cela ne se fait qu'a l'etat 0 ; ensuite, le compteur <c>+0x10</c> monte et rien d'autre.
    /// </para>
    /// <para>
    /// <b>Avec le bit 1, c'est un tireur</b> qui agit une image sur quatre (<c>+0x10 &amp; 3</c>
    /// nul). Sa cible est <b>le porteur de son porteur</b> : <c>+0x6c</c> puis encore
    /// <c>+0x6c</c>, ranges en <c>+0x74</c> et <c>+0x78</c>. Il vise le haut de la cible —
    /// <see cref="StatusBubbleHeight_00538BC0"/>, bit 0 ote, fois <c>0x800</c> —, ajoute au cap un
    /// biais de <c>0xa00</c> plus <c>rand() &amp; 0x3ff</c> et au tangage un bruit de
    /// <c>±0x200</c>, pose la vitesse (<see cref="SetVectorFromAngles_00532AD0"/>, echelle 100, le
    /// cap <b>moins</b> <c>0x400</c>) et fait naitre l'enfant de l'octet <c>+0x16</c>.
    /// </para>
    /// <para>
    /// <b>Le parametre perdu</b> : Ghidra rend le second <c>Ordinal_457</c> par
    /// <c>unaff_EBP</c>. Le binaire range la distance horizontale — le resultat
    /// d'<c>Ordinal_442</c> — <b>dans l'emplacement de son propre argument</b>
    /// (<c>mov [esp+1Ch],eax</c> avec deux mots empiles), puis la relit par <c>[esp+14h]</c>
    /// une fois la pile rendue. Le tangage est donc <c>ratan2(dy, distance)</c>, ce qu'on
    /// attendait.
    /// </para>
    /// <para>
    /// <b>Le bit <c>0x2000000</c> l'efface sans condition</b> — mot de tete a zero —, en
    /// incrementant encore <c>+0x10</c> au passage.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483F30(byte* e)
    {
        uint flags = *(uint*)e;

        if ((flags & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            (*(short*)(e + 0x10))++;
            return;
        }

        if ((flags & 2) == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(byte**)(e + 0x70) = e;
                *(short*)(e + 0x36) = 4;
                *(uint*)(e + 0x28) = 0x50000000;
                *(short*)(e + 0x2c) = 0;
                *(uint*)(e + 0x38) = 0;
                *(short*)(e + 0x2e) = 0;
                *(int*)(e + 0x5c) = 0;
                *(int*)(e + 0x20) = *(int*)(e + 0x64);
                e[0x04] = 0x36;

                byte* castTable = *(byte**)*(byte**)(s_Unknown_005e97cc + 0xd0);
                SpawnCastEntity_0047AAE0(e, (byte)(castTable[0x15] + 0xc0), 0, 2);

                *(int*)(e + 0x20) += 0x190000;
                *(uint*)(e + 0x28) = 0x18001800;
                *(short*)(e + 0x2c) = 0x1800;
                *(uint*)(e + 0x38) = 0x808080;
                e[0x04] = 0x35;

                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_005526A8, ref s_DelayedSoundScript005526A8Storage),
                    (int*)(e + 0x1c));                                  // Ghidra: FUN_0053dbe0
            }

            (*(short*)(e + 0x10))++;
            return;
        }

        if ((*(short*)(e + 0x10) & 3) != 0)
        {
            (*(short*)(e + 0x10))++;
            return;
        }

        byte* host = *(byte**)(e + 0x6c);
        byte* target = *(byte**)(host + 0x6c);
        *(byte**)(e + 0x74) = host;
        *(byte**)(e + 0x78) = target;

        int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        uint bubble = (uint)StatusBubbleHeight_00538BC0((uint*)target);
        int dy = (int)((bubble & 0xfffffffe) * 0x800 + (uint)(*(int*)(target + 0x20) - *(int*)(e + 0x20))) >> 0xc;
        int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));   // Ghidra: 0x00545a10

        int yaw = Winsys3d.Ordinal_457(dx, dz);                                 // Ghidra: 0x00545a22
        *(short*)(e + 0x32) = (short)(yaw + (CrtRand() & 0x3ff) + 0xa00);

        int pitch = Winsys3d.Ordinal_457(dy, distance);
        short tilt = (short)((CrtRand() & 0x3ff) - 0x200 - pitch);
        *(short*)(e + 0x34) = tilt;

        SetVectorFromAngles_00532AD0((int*)(e + 0x50), *(short*)(e + 0x32) - 0x400, tilt, 100);
        byte* shotTable = *(byte**)*(byte**)(s_Unknown_005e97cc + 0xd0);
        SpawnCastEntity_0047AAE0(e, (byte)(shotTable[0x16] + 0xc0), 0, 2);

        (*(short*)(e + 0x10))++;
    }

    // ---------------------------------------------------------------------
    // CRENEAU 29 DE 0x00558ba8 (0x0056b1c8, SOUS-ZONES 4 ET 9) — LE TOURBILLON QUI FRAPPE
    // ---------------------------------------------------------------------

    /// <summary>
    /// Ghidra: <c>0x00483cc0</c> — creneau <b>29</b> du bloc <c>0x00558ba8</c> (<c>0x0056b1c8</c>,
    /// sous-zones 4 et 9). <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>149
    /// instructions</b>, dump sous <c>Legacy/Raw/Blaze/FUN_00483CC0.asm</c>.
    ///
    /// <para>
    /// <b>Deux phases, et c'est le bit <c>0x2000000</c> qui les separe — pas <c>+0x10</c>.</b>
    /// Tant qu'il manque, l'entite <b>grandit</b> — son echelle rejoint <c>0x3000</c> par
    /// huitiemes — et sa teinte monte de <c>0x080808</c> par image ; elle pose elle-meme le bit
    /// quand la teinte vaut <b>exactement</b> <c>0x808080</c>. Partie de zero, cela fait seize
    /// images.
    /// </para>
    /// <para>
    /// <b>Ensuite elle avance et frappe</b>, quarante images : chaque image elle se deplace de
    /// <c>+0x5c</c> dans son cap — un pas qui rejoint <c>0xa000</c> par huitiemes —, sonde la
    /// boite <c>{0, -150, 150, 50, 0, 300}</c> par <see cref="ProbeMeleeTarget_00474CA0"/> et,
    /// si elle trouve quelqu'un, le frappe au nom de <c>+0x74</c>
    /// (<see cref="ResolveMeleeHit_00539B80"/>) et lui pose <c>0x40</c> et <c>+0x96 = 8</c>. Toutes
    /// les huit images elle seme l'enfant de l'octet <c>+0x14</c> du repertoire de zone a
    /// <c>20 + rand() % 100</c> unites dans une direction tiree, tourne vers l'exterieur
    /// (<c>+0x800</c>), puis reprend sa position et son cap d'avant.
    /// </para>
    /// <para>
    /// <b>Apres quarante images</b> le pas decroit d'un huitieme par image et la teinte redescend
    /// de <c>0x080808</c> ; l'entite s'efface — mot de tete a zero — quand elle retombe
    /// <b>exactement</b> a zero, sur le drapeau de zero de l'<c>add</c>. La meme fragilite que
    /// <see cref="EntityHandler_004767E0"/> : une teinte qui ne serait pas un multiple de
    /// <c>0x080808</c> ne s'eteindrait jamais — ici elle l'est par construction.
    /// </para>
    /// <para>
    /// <b>Trois ecritures a lire sur la pile</b> : la boite est a <c>[esp+10h]</c> apres les
    /// quatre <c>push</c> de sauvegarde ; le cap garde est range dans <b>l'emplacement de
    /// l'argument</b> (<c>[esp+2Ch]</c>), relu en <c>[esp+3Ch]</c> sous les quatre arguments de
    /// <see cref="SpawnCastEntity_0047AAE0"/> ; et l'echelle de la premiere phase est ecrite
    /// par un <c>mov [esi+28h],ecx</c> <b>de 32 bits</b> — <c>+0x2a</c> recoit l'extension de
    /// signe, zero en pratique.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483CC0(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = -0x96;
        probe[2] = 0x96;
        probe[3] = 0x32;
        probe[4] = 0;
        probe[5] = 0x12c;

        uint flags = *(uint*)e;

        if ((flags & 0x2000000) == 0)
        {
            int scale = *(short*)(e + 0x28);
            scale += (0x3000 - scale) >> 3;
            *(int*)(e + 0x28) = scale;                            // Ghidra: ecriture de 32 bits
            *(short*)(e + 0x2c) = (short)scale;

            uint tint = *(uint*)(e + 0x38) + 0x80808;
            *(uint*)(e + 0x38) = tint;
            if (tint == 0x808080) *(uint*)e = flags | 0x2000000;
            return;
        }

        short heading = *(short*)(e + 0x32);
        int stride = *(int*)(e + 0x5c);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * stride >> 0xc;   // Ghidra: 0x00545a1c
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * stride >> 0xc;        // Ghidra: 0x00545a16

        if (*(short*)(e + 0x10) >= 0x28)
        {
            *(int*)(e + 0x5c) = stride - (stride >> 3);

            uint tint = *(uint*)(e + 0x38) + 0xfff7f7f8;
            *(uint*)(e + 0x38) = tint;
            if (tint == 0) *(uint*)e = 0;
            return;
        }

        (*(short*)(e + 0x10))++;
        *(int*)(e + 0x5c) = stride + ((0xa000 - stride) >> 3);

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);          // Ghidra: FUN_00474ca0
        if (victim != null)
        {
            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);
            *(short*)(victim + 0x96) = 8;
            *(uint*)victim |= 0x40;
        }

        if ((*(short*)(e + 0x10) & 7) != 0) return;

        // Les quatre valeurs rendues apres la naissance — la derniere dans l'emplacement de
        // l'argument, voir le resume.
        short keptHeading = *(short*)(e + 0x32);
        int keptX = *(int*)(e + 0x1c);
        int keptY = *(int*)(e + 0x20);
        int keptZ = *(int*)(e + 0x24);

        int angle = CrtRand();                                          // Ghidra: FUN_00547140
        int reach = CrtRand() % 100 + 0x14;
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * reach + keptX;
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * reach + keptZ;
        *(short*)(e + 0x32) = (short)(angle + 0x800);

        byte* castTable = *(byte**)*(byte**)(s_Unknown_005e97cc + 0xd0);
        SpawnCastEntity_0047AAE0(e, (byte)(castTable[0x14] + 0xc0), 0, 2);

        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x20) = keptY;
        *(int*)(e + 0x24) = keptZ;
        *(short*)(e + 0x32) = keptHeading;
    }

    // ---------------------------------------------------------------------
    // CRENEAU 26 DE 0x00558ba8 (0x0056b1c8, SOUS-ZONES 4 ET 9) — LE PROJECTILE A TETE CHERCHEUSE
    // ---------------------------------------------------------------------

    /// <summary>
    /// Ghidra: <c>0x00484190</c> — creneau <b>26</b> du bloc <c>0x00558ba8</c> (<c>0x0056b1c8</c>,
    /// sous-zones 4 et 9), soit le sous-type <c>0xda</c>. <b>Absente de <c>Blaze.c</c></b> ;
    /// relevee au desassembleur, <b>252 instructions</b>, dump sous
    /// <c>Legacy/Raw/Blaze/FUN_00484190.asm</c>. Lot 13, 2026-09-18.
    ///
    /// <para>
    /// <b>Un projectile qui se guide sur le haut de sa cible <c>+0x78</c></b> —
    /// <see cref="StatusBubbleHeight_00538BC0"/>, comme le tireur
    /// <see cref="EntityHandler_00483F30"/>. Chaque image il tire une poussee de 12 dans la
    /// direction visee (<see cref="SetVectorFromAngles_00532AD0"/>), l'ajoute a sa vitesse
    /// <c>+0x50</c>..<c>+0x58</c>, freine d'un huitieme, avance, puis <b>reprend comme cap et
    /// tangage la direction de son propre deplacement</b> (plus <c>0x400</c> au cap). Quand ce
    /// deplacement tombe a <c>±0x80</c> pres sur la direction visee, dans les deux plans, il
    /// pose <c>0x10</c> sur son mot de tete : il est <b>verrouille</b> et ne vise plus.
    /// </para>
    /// <para>
    /// <b>Sans cible, ou verrouille</b>, il file dans son cap moins <c>0x400</c> et son tangage,
    /// et compte : a vingt images, il se marque <c>0x40000000</c>. Avec une cible vivante, il
    /// s'inscrit dans son <c>+0x74</c>.
    /// </para>
    /// <para>
    /// <b>Sa fin</b> : au contact d'un membre — la boite <c>{-0x400, -0x80, 50, 200, 0, 0x80}</c>
    /// de <see cref="ProbeMeleeTarget_00474CA0"/> —, il le frappe au nom de <c>+0x74</c>
    /// (<see cref="ResolveMeleeHit_00539B80"/>, <c>0x40</c>, <c>+0x96 = 8</c>) et s'efface. Au sol
    /// (<c>+0x60</c>), sur un bit <c>0x802</c> de <c>+0x90</c> sans <c>0xa000</c>, ou marque
    /// <c>0x40000000</c>, il <b>eclate</b> : le sous-type <c>0x6c</c> nait de lui avec une echelle de
    /// <c>0x1800</c>, le son <c>0x21</c> part a sa position, sa cible perd son <c>+0x74</c>, et il
    /// s'efface.
    /// </para>
    /// <para>
    /// <b>Un defaut de 1998, reproduit — LB-031.</b> Si la cible porte un bit de
    /// <c>0x50000000</c>, le corps defait les deux liens (<c>cible+0x74</c>, <c>+0x78</c>), met
    /// <c>ebp</c> a zero… puis saute dans le calcul de visee, qui lit <c>[ebp+1Ch]</c>. Le chemin
    /// « cible morte » dereference le pointeur qu'il vient de mettre a nul. Transcrit tel quel,
    /// comme LB-007 : ne pas le reparer.
    /// </para>
    /// </summary>
    private static void EntityHandler_00484190(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = -0x400;
        probe[1] = -0x80;
        probe[2] = 0x32;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x80;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[0x36] |= 6;
            *(uint*)e |= 0x80;
        }

        int startX = *(int*)(e + 0x1c);
        int startY = *(int*)(e + 0x20);
        int startZ = *(int*)(e + 0x24);

        // Ghidra: la cible est rangee dans l'emplacement de l'argument ([esp+44h]) et relue a
        // l'eclatement ; c'est elle, pas +0x78, qui perd son +0x74 a la fin.
        byte* target = *(byte**)(e + 0x78);

        int aimYaw;
        int aimPitch;

        if (target != null && (*(uint*)e & 0x10) == 0)
        {
            if ((*(uint*)target & 0x50000000) != 0)
            {
                *(byte**)(target + 0x74) = null;
                *(byte**)(e + 0x78) = null;
                target = null;                                      // Ghidra: `xor ebp,ebp` — voir LB-031
            }
            else
            {
                *(byte**)(target + 0x74) = e;
            }

            // Ghidra: `mov edi,[ebp+1Ch]` — sur le chemin de la cible morte, ebp vaut zero ici.
            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            uint bubble = (uint)StatusBubbleHeight_00538BC0((uint*)target);
            int dy = (int)((bubble & 0xfffffffe) * 0x800 + (uint)*(int*)(target + 0x20) - (uint)*(int*)(e + 0x20)) >> 0xc;
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));   // Ghidra: 0x00545a10
            aimYaw = Winsys3d.Ordinal_457(dx, dz);                                   // Ghidra: 0x00545a22
            aimPitch = Winsys3d.Ordinal_457(dy, distance);
        }
        else
        {
            (*(short*)(e + 0x10))++;
            if (*(short*)(e + 0x10) >= 0x14) *(uint*)e |= 0x40000000;

            aimYaw = *(short*)(e + 0x32) - 0x400;
            aimPitch = *(short*)(e + 0x34);
        }

        int* thrust = stackalloc int[3];
        SetVectorFromAngles_00532AD0(thrust, aimYaw, aimPitch, 0x0c);

        int vx = *(int*)(e + 0x50) + thrust[0];
        int vy = *(int*)(e + 0x54) + thrust[1];
        int vz = *(int*)(e + 0x58) + thrust[2];
        vx -= vx >> 3;
        vy -= vy >> 3;
        vz -= vz >> 3;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x58) = vz;
        *(int*)(e + 0x24) += vz;

        int mx = (*(int*)(e + 0x1c) - startX) >> 0xc;
        int my = (*(int*)(e + 0x20) - startY) >> 0xc;
        int mz = (*(int*)(e + 0x24) - startZ) >> 0xc;

        int travel = (int)Winsys3d.Ordinal_442((uint)(mx * mx + mz * mz));
        *(short*)(e + 0x32) = (short)(Winsys3d.Ordinal_457(mx, mz) + 0x400);
        *(short*)(e + 0x34) = (short)Winsys3d.Ordinal_457(my, travel);

        // Ghidra: `add edi,480h ; and edi,0FFFh ; cmp edi,100h` — a ±0x80 pres, modulo un tour.
        if (((aimYaw - *(short*)(e + 0x32) + 0x480) & 0xfff) < 0x100 &&
            ((aimPitch - *(short*)(e + 0x34) + 0x80) & 0xfff) < 0x100)
        {
            *(uint*)e |= 0x10;
        }

        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60))
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)(e + 0x90) = 0x800;
        }

        uint contact = *(uint*)(e + 0x90);
        bool burst = (*(uint*)e & 0x40000000) != 0
                     || ((contact & 0xa000) == 0 && (contact & 0x802) != 0);

        if (!burst)
        {
            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);        // Ghidra: FUN_00474ca0
            if (victim == null) return;

            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);
            *(short*)(victim + 0x96) = 8;
            *(uint*)victim |= 0x40;
            *(uint*)e = 0;
            return;
        }

        *(byte**)(e + 0x70) = null;
        *(int*)(e + 0x44) = 0;
        *(int*)(e + 0x48) = 0;
        *(int*)(e + 0x4c) = 0;
        *(int*)(e + 0x50) = 0x1800;
        *(int*)(e + 0x54) = 0x1400;
        *(int*)(e + 0x58) = 0x1800;
        *(uint*)(e + 0x38) = 0x808080;
        *(int*)(e + 0x28) = 0;                                       // Ghidra: ecriture de 32 bits
        *(short*)(e + 0x2c) = 0;
        *(short*)(e + 0x34) = 0;
        e[0x04] = 1;

        SpawnCastEntity_0047AAE0(e, 0x6c, 0, 2);                     // Ghidra: FUN_0047aae0
        Aud.PlayPositionalSound(0x21, 0x302, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20

        if (target != null) *(byte**)(target + 0x74) = null;
        *(uint*)e = 0;
    }

    /// <summary>Le cycle de sons de <see cref="EntityHandler_00424800"/>, que le binaire ecrit deux
    /// fois a l'identique : <c>0x20</c> fort, <c>0x13</c>, <c>0x20</c> plus grave sur
    /// <c>+0x14</c> = 0, 1, 2, puis huit images de silence — le compteur revient a zero apres
    /// 10.</summary>
    private static void StepOpeningModelSounds(byte* e)
    {
        if (*(short*)(e + 0x14) == 0) Aud.PlaySoundEffect(0x20, 0x1000, 0x800, 0x800);   // Ghidra: FUN_0053cea0
        if (*(short*)(e + 0x14) == 1) Aud.PlaySoundEffect(0x13, 0x400, 0x400, 0x400);
        if (*(short*)(e + 0x14) == 2) Aud.PlaySoundEffect(0x20, 0x653, 0x600, 0x600);

        (*(short*)(e + 0x14))++;
        if (*(short*)(e + 0x14) > 10) *(short*)(e + 0x14) = 0;
    }
}
