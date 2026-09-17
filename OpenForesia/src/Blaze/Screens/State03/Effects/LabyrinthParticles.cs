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
    private static void TickHostDeathBurst(byte* e, byte spawn)
    {
        byte* host = *(byte**)(e + 0x6c);
        byte* source = host == null ? Res.s_EntityList_005de558 : host;   // Ghidra: 5de558h

        *(int*)(e + 0x1c) = *(int*)(source + 0x1c);
        *(int*)(e + 0x20) = *(int*)(source + 0x20);
        *(int*)(e + 0x24) = *(int*)(source + 0x24);

        uint hostFlags = *(uint*)source;

        if ((hostFlags & 0x40000000) != 0 || (hostFlags & 0x80000000) == 0)
        {
            *(uint*)e &= 0x7fffffff;
            return;
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
}
