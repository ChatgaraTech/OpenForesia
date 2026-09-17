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

    // =====================================================================
    // CATEGORIE B — LES EFFETS SIMPLES (2026-09-17, lot 3)
    //
    // Treize corps qui n'ont pas de modele ecrit ailleurs. Ils posent leurs propres planches, que
    // le balayage du binaire borne comme les precedentes :
    //
    //   0x0054fa38  3 enreg.  0x00417650                     base suivante 0x0054fa50
    //   0x0054fa50  1         0x00417000, qui ECRIT 0x0054fa55  0x0054fa58
    //   0x0054fb88  4         0x00416DE0 (et 0x00416920)     0x0054fba8
    //   0x0054fbb8  4         0x00419D30 (et 0x00419B90)     0x0054fbd8
    //   0x0054fbd8  3         0x00419260                     0x0054fbf0
    //   0x0054fd50  1         0x0041B2F0                     0x0054fd58
    //   0x0054fde0  4         0x0041BFA0                     0x0054fe00
    //
    // DEUX CORPS ECRIVENT DANS UNE PLANCHE : l'octet 5 d'un enregistrement — la page de texture —
    // passe a 2. 0x00417000 l'ecrit dans 0x0054fa50, et 0x00416EE0 dans 0x0054fa58, le
    // premier enregistrement de la plage bulle + grain : apres son passage, la bulle de la tour
    // se dessine sur la page 2. Les deux ecritures passent par la meme allocation que les
    // lectures, sans quoi elles ne seraient vues par personne.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054fa38</c> — trois tuiles <c>0x18 x 0x18</c> de la page
    /// <c>0x03</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FA38 =
    [
        0x00, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa38  <- 0x00417650
        0x18, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa40
        0x30, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa48
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fa50</c> — une tuile, dont l'octet de page est reecrit a 2 par
    /// <see cref="EntityHandler_00417000"/>. La valeur ci-dessous est celle du fichier.</summary>
    private static readonly byte[] s_Descriptors_0054FA50 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fa50  <- 0x00417000
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fb88</c> — les quatre coins d'un carre de <c>0x10</c>, page
    /// <c>0x02</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FB88 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054fb88  <- 0x00416DE0
        0x10, 0x08, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054fb90
        0x08, 0x10, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054fb98
        0x10, 0x10, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054fba0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fbb8</c> — quatre tuiles <c>0x18 x 0x18</c> en rangee, page
    /// <c>0x03</c>, octet 4 nul.</summary>
    private static readonly byte[] s_Descriptors_0054FBB8 =
    [
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fbb8  <- 0x00419D30
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fbc0
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fbc8
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fbd0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fbd8</c> — la copie de <see cref="s_Descriptors_0054FA38"/>
    /// pour les sous-zones 5, 6, 8 et 10, memes octets.</summary>
    private static readonly byte[] s_Descriptors_0054FBD8 =
    [
        0x00, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fbd8  <- 0x00419260
        0x18, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fbe0
        0x30, 0x30, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fbe8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fd50</c> — une tuile, la meme que
    /// <see cref="s_Descriptors_0054FA50"/>, juste avant la plage bulle + grain des sous-zones 3, 7
    /// et 9.</summary>
    private static readonly byte[] s_Descriptors_0054FD50 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fd50  <- 0x0041B2F0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fde0</c> — quatre tuiles <c>0x20 x 0x20</c> en rangee, page
    /// <c>0x02</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FDE0 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054fde0  <- 0x0041BFA0
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054fde8
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054fdf0
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054fdf8
    ];

    private static byte* s_Descriptors0054FA38Storage;
    private static byte* s_Descriptors0054FA50Storage;
    private static byte* s_Descriptors0054FB88Storage;
    private static byte* s_Descriptors0054FBB8Storage;
    private static byte* s_Descriptors0054FBD8Storage;
    private static byte* s_Descriptors0054FD50Storage;
    private static byte* s_Descriptors0054FDE0Storage;

    private static byte* Descriptors0054FA38()
        => NativeCopy(s_Descriptors_0054FA38, ref s_Descriptors0054FA38Storage);

    private static byte* Descriptors0054FA50()
        => NativeCopy(s_Descriptors_0054FA50, ref s_Descriptors0054FA50Storage);

    private static byte* Descriptors0054FB88()
        => NativeCopy(s_Descriptors_0054FB88, ref s_Descriptors0054FB88Storage);

    private static byte* Descriptors0054FBB8()
        => NativeCopy(s_Descriptors_0054FBB8, ref s_Descriptors0054FBB8Storage);

    private static byte* Descriptors0054FBD8()
        => NativeCopy(s_Descriptors_0054FBD8, ref s_Descriptors0054FBD8Storage);

    private static byte* Descriptors0054FD50()
        => NativeCopy(s_Descriptors_0054FD50, ref s_Descriptors0054FD50Storage);

    private static byte* Descriptors0054FDE0()
        => NativeCopy(s_Descriptors_0054FDE0, ref s_Descriptors0054FDE0Storage);

    // ---------------------------------------------------------------- sous-zones 0, 1, 2 et 4

    /// <summary>
    /// Ghidra: FUN_00416de0 @ 0x00416de0 — creneau <b>46</b> de la table <c>0x0056b2c8</c>, sous-zones
    /// 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>, 81 instructions.
    ///
    /// <para>
    /// <b>Un scintillement.</b> Il nait disperse de ±15 unites a l'horizontale et jusqu'a 30 vers le
    /// haut, a une echelle tiree entre <c>0xed4</c> et <c>0x12d3</c>, teinte <c>0x808080</c>, pour
    /// quatre a sept images. Chaque image il tire au hasard l'une des quatre tuiles de
    /// <see cref="s_Descriptors_0054FB88"/> et s'assombrit de 7. Un tirage de la naissance est jete.
    /// </para>
    /// </summary>
    private static void EntityHandler_00416DE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054FB88();                         // Ghidra: &DAT_0054fb88
            e[0x0a] = 9;
            e[0x0b] = 0;

            int scaleRoll = CrtRand();                                         // Ghidra: FUN_00547140
            *(short*)(e + 0x10) = 1;
            short scale = (short)(scaleRoll % 0x400 + 0xed4);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            int lifeRoll = CrtRand();
            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)(lifeRoll % 4 + 4);

            CrtRand();                                                         // Ghidra: tirage jete

            *(int*)(e + 0x1c) += (CrtRand() % 0x1e - 0x0f) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x1e) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x1e - 0x0f) * 0x1000;

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;
        }

        if (*(short*)(e + 0x10) != 1) return;

        e[0x40] = (byte)(CrtRand() % 4);
        BrightenEntityTint_00404DE0(e, -7, -7, -7);                           // Ghidra: FUN_00404de0

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < 1) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00416ee0 @ 0x00416ee0 — creneau <b>47</b> de la table <c>0x0056b2c8</c>, sous-zones
    /// 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>, 88 instructions.
    ///
    /// <para>
    /// <b>Une poussiere qui retombe en s'eteignant.</b> Elle nait entre 30 et 49 unites au-dessus de
    /// son point, dispersee de ±30, a une echelle tiree entre <c>0x5dc</c> et <c>0x9db</c> ; elle
    /// descend de 3 a 6 unites par image et s'assombrit d'un pas tire entre 5 et 9, jusqu'a ce que
    /// le rouge tombe a zero.
    /// </para>
    /// <para>
    /// <b>Elle reecrit la page de la bulle</b> : l'octet 5 de <c>0x0054fa58</c> passe a 2 a chacune
    /// de ses naissances.
    /// </para>
    /// </summary>
    private static void EntityHandler_00416EE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            byte* plate = Descriptors0054FA58();
            *(byte**)(e + 0x3c) = plate;                                       // Ghidra: &DAT_0054fa58
            plate[5] = 2;                                                      // Ghidra: DAT_0054fa5d = 2
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0x3c - 0x1e) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x3c - 0x1e) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x14 + 0x1e) * 0x1000;

            short scale = (short)(CrtRand() % 0x400 + 0x5dc);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            int fallRoll = CrtRand();
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(fallRoll % 4 + 3);

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;

            *(short*)(e + 0x14) = (short)(-5 - CrtRand() % 5);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short fade = *(short*)(e + 0x14);
        *(int*)(e + 0x20) -= *(short*)(e + 0x12) * 0x1000;
        BrightenEntityTint_00404DE0(e, fade, fade, fade);

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00417000</c> — creneau <b>58</b> de la table <c>0x0056b2c8</c>, sous-zones 0, 1, 2
    /// et 4. <b>Absente de <c>Blaze.c</c></b> ; 87 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00417000.asm</c>.
    ///
    /// <para>
    /// <b>Une bouffee qui monte et s'etale.</b> Elle nait entre 80 et 119 unites au-dessus de son
    /// point, dispersee de ±40, a une echelle nulle qui gagne 200, 300, 400 ou 500 par image sur les
    /// deux axes ; elle monte de 3 a 7 unites par image et s'assombrit de 6, jusqu'a ce que le rouge
    /// tombe a zero. Elle ecrit 2 dans l'octet de page de sa planche a chaque naissance.
    /// </para>
    /// </summary>
    private static void EntityHandler_00417000(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            byte* plate = Descriptors0054FA50();
            *(byte**)(e + 0x3c) = plate;                                       // Ghidra: `mov dword [esi+3Ch],54FA50h`
            plate[5] = 2;                                                      // Ghidra: `mov byte [54FA55h],2`
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0x50 - 0x28) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0x50 - 0x28) << 0xc;

            int lift = CrtRand();
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(int*)(e + 0x20) += (lift % 0x28 + 0x50) << 0xc;

            int growth = CrtRand();
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)((growth % 4 + 2) * 100);

            int rise = CrtRand();
            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;
            *(short*)(e + 0x14) = (short)(rise % 5 + 3);
        }

        // Ghidra: LAB_004170cd
        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) += *(short*)(e + 0x14) << 0xc;
        *(short*)(e + 0x28) += *(short*)(e + 0x12);
        *(short*)(e + 0x2a) += *(short*)(e + 0x12);
        BrightenEntityTint_00404DE0(e, -6, -6, -6);                           // Ghidra: FUN_00404de0

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00418290 @ 0x00418290 — creneau <b>51</b> de la table <c>0x0056b2c8</c>, sous-zones
    /// 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>, 66 instructions. Voir
    /// <see cref="TickRisingSpark_00418290"/>.
    /// </summary>
    private static void EntityHandler_00418290(byte* e)
        => TickRisingSpark_00418290(e, Descriptors0054FA58());                // Ghidra: &DAT_0054fa58

    /// <summary>Ghidra: FUN_00419e60 @ 0x00419e60 — le meme corps pour les sous-zones 5, 6, 8 et 10 ;
    /// le diff STRICT ne rend que <c>mov dword [esi+3Ch],54FC00h</c>.</summary>
    private static void EntityHandler_00419E60(byte* e)
        => TickRisingSpark_00418290(e, Descriptors0054FC00());                // Ghidra: &DAT_0054fc00

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00418290"/> et <see cref="EntityHandler_00419E60"/>.
    /// <b>Une etincelle qui jaillit en accelerant</b> : nee 80 a 99 unites sous son point, dispersee
    /// de ±50 a l'horizontale, echelle <c>0xc00</c>, elle monte d'une vitesse <c>+0x12</c> partie de
    /// 15 et qui gagne un par image — l'ajout est pris pair (<c>&amp; ~1</c>) et vaut un demi-pas —
    /// et s'eteint a 31. <c>+0x16</c> recoit un tirage sur douze bits que le corps ne relit pas.
    /// </summary>
    private static void TickRisingSpark_00418290(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            *(short*)(e + 0x28) = 0xc00;
            *(short*)(e + 0x2a) = 0xc00;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 0x0f;
            *(short*)(e + 0x14) = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 100 - 0x32) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 100 - 0x32) * 0x1000;

            int drop = CrtRand();
            e[0x40] = 0;
            *(int*)(e + 0x20) += (drop % 0x14) * -0x1000 - 0x50000;

            *(short*)(e + 0x16) = (short)(CrtRand() % 0x1000);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short speed = *(short*)(e + 0x12);
        *(int*)(e + 0x20) += (speed & ~1) * 0x800;
        speed++;
        *(short*)(e + 0x12) = speed;

        if (speed > 0x1e) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00417650 @ 0x00417650 — creneau <b>56</b> de la table <c>0x0056b2c8</c>, sous-zones
    /// 0, 1, 2 et 4. Decompilee dans <c>Blaze.c</c>, 99 instructions. Voir
    /// <see cref="TickHomingGlint_00417650"/>.
    /// </summary>
    private static void EntityHandler_00417650(byte* e)
        => TickHomingGlint_00417650(e, Descriptors0054FA38());                // Ghidra: &DAT_0054fa38

    /// <summary>Ghidra: FUN_00419260 @ 0x00419260 — le meme corps pour les sous-zones 5, 6, 8 et 10 ;
    /// le diff STRICT ne rend que <c>mov dword [esi+3Ch],54FBD8h</c>.</summary>
    private static void EntityHandler_00419260(byte* e)
        => TickHomingGlint_00417650(e, Descriptors0054FBD8());                // Ghidra: &DAT_0054fbd8

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00417650"/> et <see cref="EntityHandler_00419260"/>.
    ///
    /// <para>
    /// <b>Une image, pas davantage.</b> Il n'a pas d'etat de naissance : a chaque passage il pose sa
    /// planche, une echelle tiree entre <c>0xf00</c> et <c>0x10ff</c> sur chaque axe, et prend pour
    /// image l'octet bas de <c>+0x16</c> — une valeur que son lanceur a laissee. Au premier passage
    /// il se rapproche du point <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> d'un sixieme de l'ecart
    /// multiplie par <c>+0x12</c>, tremble de quelques unites et passe a l'etat 1 ; au second, il
    /// disparait.
    /// </para>
    /// <para>
    /// <b>Son indice d'image vient d'ailleurs</b>, et sa planche n'a que trois enregistrements : le
    /// plafond de <c>+0x16</c> depend de celui qui le fait naitre, et ce lanceur n'est pas encore
    /// ecrit. Voir <c>check-descriptors.sh</c>.
    /// </para>
    /// </summary>
    private static void TickHomingGlint_00417650(byte* e, byte* plate)
    {
        *(byte**)(e + 0x3c) = plate;
        *(short*)(e + 0x28) = (short)(CrtRand() % 0x200 + 0xf00);
        e[0x0a] = 9;
        e[0x0b] = 0;
        *(short*)(e + 0x2a) = (short)(CrtRand() % 0x200 + 0xf00);

        // TODO(REVERSE): le plafond de +0x16 depend du lanceur de ce corps (sous-type 0xf8 de la
        // table 0x0056b2c8, pas encore identifie) ; la planche n'a que trois enregistrements, donc
        // une valeur au-dela de 2 lirait la planche suivante. check-descriptors.sh ne le signale pas
        // (marge 3).
        e[0x40] = e[0x16];

        if (*(short*)(e + 0x10) == 1)
        {
            *(uint*)e &= 0x7fffffff;
            return;
        }

        int pull = *(short*)(e + 0x12);
        *(int*)(e + 0x1c) -= (*(int*)(e + 0x1c) - *(int*)(e + 0x50)) / 6 * pull;
        *(int*)(e + 0x20) -= (*(int*)(e + 0x20) - *(int*)(e + 0x54)) / 6 * pull;
        *(int*)(e + 0x24) -= (*(int*)(e + 0x24) - *(int*)(e + 0x58)) / 6 * pull;

        *(int*)(e + 0x1c) += (CrtRand() % 0x1e - 0x0f) * 0x1000;
        *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) * 0x1000;

        int jitter = CrtRand();
        *(short*)(e + 0x10) = 1;
        *(int*)(e + 0x24) += (jitter % 0x1e - 0x0f) * 0x1000;
    }

    // ---------------------------------------------------------------- sous-zones 5, 6, 8 et 10

    /// <summary>
    /// Ghidra: <c>0x00419d30</c> — creneau <b>50</b> de la table <c>0x0056b2c8</c>, sous-zones 5, 6, 8
    /// et 10. <b>Absente de <c>Blaze.c</c></b> ; 97 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00419D30.asm</c>.
    ///
    /// <para>
    /// <b>Un eclat projete.</b> A sa naissance il tourne son cap d'un tirage sur douze bits, pose le
    /// bit <c>0x4000</c>, et <b>avance d'un coup de onze pas</b> de <c>sin/cos × 8</c> dans ce cap.
    /// Puis il attend : une chance sur cinq par image de repartir, et au plus tard a la troisieme.
    /// Alors il retire <c>0x4000</c>, file dans son cap en montant de <c>0x3000</c> par image, change
    /// d'image toutes les dix images et s'eteint a la seconde.
    /// </para>
    /// </summary>
    private static void EntityHandler_00419D30(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054FBB8();                         // Ghidra: `mov dword [esi+3Ch],54FBB8h`
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
            e[0x0a] = 2;
            e[0x0b] = 0;

            int turn = CrtRand();
            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x32) += (short)(turn % 0x1000);
            *(uint*)e |= 0x4000;

            // Ghidra: LAB_00419d8a — la boucle, EDI de 11 a 1.
            for (int i = 11; i != 0; i--)
            {
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) << 3;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) << 3;
            }
        }

        // Ghidra: LAB_00419db7
        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            // Ghidra: `cmp dx,1` — le reste pris sur son mot bas.
            if ((short)(CrtRand() % 5) == 1 || *(short*)(e + 0x12) == 3)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0xffffbfff;
                return;
            }
        }

        // Ghidra: LAB_00419ded
        if (*(short*)(e + 0x10) != 2) return;

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) << 3;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) << 3;
        *(int*)(e + 0x20) += 0x3000;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) < 0x0a) return;

        *(short*)(e + 0x12) = 0;
        e[0x40]++;
        if (e[0x40] == 2) *(uint*)e &= 0x7fffffff;
    }

    // ---------------------------------------------------------------- sous-zones 3, 7 et 9

    /// <summary>
    /// Ghidra: FUN_0041b220 @ 0x0041b220 — creneau <b>59</b> de la table <c>0x0056b2c8</c>, sous-zones
    /// 3, 7 et 9. Decompilee dans <c>Blaze.c</c>, 60 instructions.
    ///
    /// <para>
    /// <b>Une goutte qui tombe.</b> Nee jusqu'a 149 unites sous son point (dispersee de ±10), sur la
    /// bulle de <c>0x0054fd58</c> a l'echelle <c>0x800</c>, elle part d'une vitesse de 20 a 34 qu'elle
    /// perd d'un par image, retranchee de la hauteur ; elle s'eteint a 0.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041B220(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054FD58();                         // Ghidra: &DAT_0054fd58
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;

            *(int*)(e + 0x20) += (CrtRand() % 0x96) * -0x1000;
            *(int*)(e + 0x1c) += (CrtRand() % 0x14 - 10) * 0x1000;

            int drift = CrtRand();
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x24) += (drift % 0x14 - 10) * 0x1000;

            *(short*)(e + 0x14) = (short)(CrtRand() % 0x0f + 0x14);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short speed = *(short*)(e + 0x14);
        *(int*)(e + 0x20) += speed * -0x1000;
        speed--;
        *(short*)(e + 0x14) = speed;

        if (speed < 1) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0041b2f0 @ 0x0041b2f0 — creneau <b>60</b> de la table <c>0x0056b2c8</c>, sous-zones
    /// 3, 7 et 9. Decompilee dans <c>Blaze.c</c>, 73 instructions.
    ///
    /// <para>
    /// <b>Une spirale qui monte.</b> Elle tourne autour du point <c>+0x50</c>/<c>+0x58</c> sur un rayon
    /// de 160, pas <c>0xb4</c>, en montant de <c>0xa000</c> par image ; passe la soixantieme image
    /// de son horloge <c>+0x16</c> (qui avance avant tout, meme a la naissance), le rayon perd 5 par
    /// image et elle s'eteint quand il passe sous zero. Sa hauteur de depart est tiree jusqu'a 399
    /// unites au-dessus, et <c>+0x9a</c> garde la hauteur de naissance, sans la relire.
    /// </para>
    /// <para>
    /// <b>Le centre <c>+0x50</c>/<c>+0x58</c> n'est pas pose par ce corps</b> : c'est ce que le
    /// lanceur a laisse dans les champs de vitesse de l'enregistrement recopie.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041B2F0(byte* e)
    {
        (*(short*)(e + 0x16))++;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054FD50();                         // Ghidra: &DAT_0054fd50
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0x400;
            *(short*)(e + 0x2a) = 0x400;

            int course = CrtRand();
            *(short*)(e + 0x14) = 0xa0;
            *(short*)(e + 0x12) = (short)(course % 0x1000);
            *(short*)(e + 0x9a) = (short)(*(int*)(e + 0x20) >> 0xc);

            *(int*)(e + 0x20) += (CrtRand() % 400) * 0x1000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        short radius = *(short*)(e + 0x14);

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));        // Ghidra: Ordinal_451
        *(int*)(e + 0x1c) = sin * radius + *(int*)(e + 0x50);

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));             // Ghidra: Ordinal_450
        *(short*)(e + 0x12) += 0xb4;
        *(int*)(e + 0x24) = cos * radius + *(int*)(e + 0x58);
        *(int*)(e + 0x20) += 0xa000;

        if (*(short*)(e + 0x12) > 0xfff) *(short*)(e + 0x12) -= 0x1000;

        if (*(short*)(e + 0x16) <= 0x3b) return;

        short shrunk = (short)(radius - 5);
        *(short*)(e + 0x14) = shrunk;
        if (shrunk < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0041b9f0 @ 0x0041b9f0 — creneau <b>2</b> de la table <c>0x0056b2c8</c>, sous-zones
    /// 3, 7 et 9. Decompilee dans <c>Blaze.c</c>, 92 instructions.
    ///
    /// <para>
    /// <b>Une helice.</b> Nee dispersee de ±15, elle retient son point dans <c>+0x50</c>/<c>+0x54</c>/
    /// <c>+0x58</c> et tourne autour sur un rayon de 30 a 74, a un pas tire entre 30 et 59, en
    /// montant de <c>0x8000</c> par image, pendant 30 a 59 images. Echelle <c>0x800</c>, genre
    /// <c>2</c>. Le corps rend une valeur que le repartiteur ignore.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041B9F0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x40] = 0;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054FD58();                         // Ghidra: &DAT_0054fd58
            e[0x0a] = 2;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;

            *(int*)(e + 0x1c) += (CrtRand() % 0x1e - 0x0f) * 0x1000;

            int drift = CrtRand();
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            int z = *(int*)(e + 0x24) + (drift % 0x1e - 0x0f) * 0x1000;
            *(int*)(e + 0x24) = z;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x58) = z;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x2d + 0x1e);
            *(short*)(e + 0x16) = (short)(CrtRand() % 0x1e + 0x1e);
            *(int*)(e + 0x5c) = CrtRand() % 0x1e + 0x1e;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(int*)(e + 0x1c) = sin * *(short*)(e + 0x14) + *(int*)(e + 0x50);

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));
        *(int*)(e + 0x20) += 0x8000;
        *(short*)(e + 0x12) += *(short*)(e + 0x16);
        *(int*)(e + 0x24) = cos * *(short*)(e + 0x14) + *(int*)(e + 0x58);

        (*(int*)(e + 0x5c))--;
        if (*(int*)(e + 0x5c) < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0041bfa0</c> — creneau <b>57</b> de la table <c>0x0056b2c8</c>, sous-zones 3, 7 et
    /// 9. <b>Absente de <c>Blaze.c</c></b> ; 98 instructions, <c>Legacy/Raw/Blaze/FUN_0041BFA0.asm</c>.
    ///
    /// <para>
    /// <b>Une spirale qui s'ouvre.</b> Elle tourne autour de son point de naissance sur un rayon
    /// <c>+0x5c</c> qui part de zero et gagne 10 a 19 par image — <b>la phase, elle, ne bouge pas</b> :
    /// elle file en ligne droite dans un cap tire, en s'ecartant. Elle monte de 3 a 7 unites par image,
    /// perd un cran de teinte, change d'image toutes les quatre images et s'eteint quand l'indice
    /// d'image atteint 4 — seize images en tout. Echelle <c>0x2000</c>, genre <c>2</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041BFA0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(byte**)(e + 0x3c) = Descriptors0054FDE0();                         // Ghidra: `mov dword [esi+3Ch],54FDE0h`
            e[0x40] = 0;
            *(short*)(e + 0x28) = 0x2000;
            *(short*)(e + 0x2a) = 0x2000;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x14) = (short)(CrtRand() % 10 + 10);

            int rise = CrtRand();
            *(int*)(e + 0x5c) = 0;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x9a) = 0;
            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;
            *(short*)(e + 0x16) = (short)(rise % 5 + 3);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
        }

        // Ghidra: LAB_0041c03f
        if (*(short*)(e + 0x10) != 1) return;

        int radius = *(int*)(e + 0x5c);

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));        // Ghidra: 0x00545a1c
        *(int*)(e + 0x1c) = sin * radius + *(int*)(e + 0x50);

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));             // Ghidra: 0x00545a16
        *(int*)(e + 0x24) = cos * radius + *(int*)(e + 0x58);
        *(int*)(e + 0x20) += *(short*)(e + 0x16) << 0xc;

        (*(short*)(e + 0x9a))++;
        *(int*)(e + 0x5c) = *(short*)(e + 0x14) + radius;

        e[0x38]--;
        e[0x39]--;
        e[0x3a]--;

        if (*(short*)(e + 0x9a) <= 3) return;

        *(short*)(e + 0x9a) = 0;
        e[0x40]++;
        if (e[0x40] == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0041bf00</c> — creneau <b>3</b> de la table <c>0x0056b1c8</c>, sous-zones 3, 7 et 9.
    /// <b>Absente de <c>Blaze.c</c></b> ; 50 instructions, <c>Legacy/Raw/Blaze/FUN_0041BF00.asm</c>.
    ///
    /// <para>
    /// <b>Une onde qui s'elargit.</b> Elle ne pose pas de planche — elle garde celle de son lanceur,
    /// que <see cref="SpawnCastEntity_0047AAE0"/> recopie. A la naissance, l'octet bas de
    /// <c>+0x14</c> passe dans <c>+0x04</c> et choisit le genre : <c>2</c> s'il vaut 1, <c>9</c>
    /// sinon. Echelle nulle, 20 unites plus haut, teinte pleine. Puis ses trois echelles gagnent
    /// <c>0x190</c> par image (genre 1) ou <c>0x320</c>, elle s'assombrit de 6 et s'eteint a zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041BF00(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            byte kind = e[0x14];
            bool single = *(short*)(e + 0x14) == 1;

            *(short*)(e + 0x10) = 1;
            e[0x04] = kind;
            e[0x0a] = (byte)(single ? 2 : 9);

            // Ghidra: LAB_0041bf2d
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0;
            *(int*)(e + 0x20) += 0x14000;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x34) = 0;
            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;
        }

        // Ghidra: LAB_0041bf56
        if (*(short*)(e + 0x10) != 1) return;

        short growth = (short)(e[0x04] == 1 ? 0x190 : 0x320);
        *(short*)(e + 0x28) += growth;
        *(short*)(e + 0x2a) += growth;
        *(short*)(e + 0x2c) += growth;

        BrightenEntityTint_00404DE0(e, -6, -6, -6);                           // Ghidra: FUN_00404de0

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LES DEUX FORMES A JUMEAUX DE LA TABLE 0x0056b2c8 (2026-09-17)
    //
    // Categorie C de Zone4ParComplexite.md, prise par forme : quatre creneaux pour deux lectures.
    // Comme les particules deja ecrites de ce fichier, les jumeaux ne different que par l'adresse de
    // leur planche — une ligne de diff, `mov dword [esi+3Ch],…`.
    //
    //   0x00417E10 / 0x00419A20   111 instr.  planches 0x0054FA58 / 0x0054FC00
    //   0x00417F80 / 0x00419B90   131         planches 0x0054FA18 / 0x0054FBB8
    //
    // Les deux blocs n'ont pas de troisieme emission : les sous-zones 3, 7 et 9 laissent ces deux
    // creneaux sur la souche.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054fa18</c> — <b>la copie de <see cref="s_Descriptors_0054FBB8"/>
    /// pour les sous-zones 0, 1, 2 et 4</b>, memes octets, quatre tuiles <c>0x18 x 0x18</c> en rangee
    /// sur la page <c>0x03</c>. Bornee en haut par <c>0x0054fa38</c>, le debut de
    /// <see cref="s_Descriptors_0054FA38"/> : quatre enregistrements, exactement ce que
    /// <see cref="TickCardinalSplash_00417F80"/> consomme en menant <c>+0x40</c> de 0 a 3.</summary>
    private static readonly byte[] s_Descriptors_0054FA18 =
    [
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fa18  <- 0x00417F80
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fa20
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fa28
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x0054fa30
    ];

    private static byte* s_Descriptors0054FA18Storage;

    private static byte* Descriptors0054FA18()
        => NativeCopy(s_Descriptors_0054FA18, ref s_Descriptors0054FA18Storage);

    /// <summary>Ghidra: <c>0x00417e10</c> — la spirale qui se resserre, sous-zones 0, 1, 2 et 4.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump conserve dans
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_00417E10.asm</c>.</summary>
    private static void EntityHandler_00417E10(byte* e)
        => TickTighteningSpiral_00417E10(e, Descriptors0054FA58());           // Ghidra: &DAT_0054fa58

    /// <summary>Ghidra: <c>0x00419a20</c> — le meme corps pour les sous-zones 5, 6, 8 et 10 ; le
    /// diff ne rend que <c>mov dword [esi+3Ch],54FC00h</c>.</summary>
    private static void EntityHandler_00419A20(byte* e)
        => TickTighteningSpiral_00417E10(e, Descriptors0054FC00());           // Ghidra: &DAT_0054fc00

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00417E10"/> et <see cref="EntityHandler_00419A20"/> :
    /// <b>une spirale qui se resserre jusqu'a s'annuler</b>.
    ///
    /// <para>
    /// A la naissance elle fige son ancre (<c>+0x50</c>, <c>+0x54</c>, <c>+0x58</c>) sur sa position,
    /// part d'une echelle nulle, d'un rayon <c>+0x12</c> tire entre <b>250 et 449</b>, d'une phase
    /// <c>+0x16</c> tiree sur douze bits et d'un decompte <c>+0x14</c> a 10.
    /// </para>
    /// <para>
    /// A chaque image l'echelle monte de <c>0x1f4</c> jusqu'a <c>0x1000</c>, ou elle est bornee, et
    /// les trois coordonnees sont <b>recalculees depuis l'ancre</b> — jamais accumulees :
    /// </para>
    /// <list type="bullet">
    /// <item><c>X = sin(+0x16) × (rayon / 4) + ancreX</c> ;</item>
    /// <item><c>Z = cos(<b>+0x32</b>) × rayon + ancreZ</c> ;</item>
    /// <item><c>Y = cos(+0x16) × (rayon / 4) + ancreY</c>.</item>
    /// </list>
    /// <para>
    /// <b>Le Z est le seul a suivre le cap et non la phase</b>, et c'est bien ce que dit le
    /// desassemblage (<c>movsx edx,word [esi+32h]</c> entre les deux lectures de <c>+0x16</c>) : la
    /// figure n'est donc pas un cercle mais une ellipse orientee, et son axe long vaut quatre fois
    /// les deux autres.
    /// </para>
    /// <para>
    /// La phase avance de <b>100</b> par image, ramenee dans <c>0xfff</c> ; le rayon perd la moitie
    /// du decompte, lequel gagne un par image — la fermeture est donc quadratique. Un brouillage de
    /// ±3 unites entieres est ajoute a X et a Z <b>apres</b> le calcul, et la spirale meurt quand le
    /// rayon passe sous zero.
    /// </para>
    /// <para>
    /// <b>Le rayon est lu avant sa mise a jour</b> : le binaire garde l'ancienne valeur dans
    /// <c>DI</c> (<c>mov di,[esi+12h]</c>) pendant tout le bloc des trois coordonnees, et n'ecrit
    /// <c>+0x12</c> qu'apres. Les trois axes voient donc le meme rayon.
    /// </para>
    /// </summary>
    private static void TickTighteningSpiral_00417E10(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;

            int radius = CrtRand() % 0xc8;                    // Ghidra: FUN_00547140
            *(short*)(e + 0x14) = 0x0a;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)(radius + 0xfa);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            *(short*)(e + 0x16) = (short)(CrtRand() % 0x1000);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x28) += 0x1f4;
        if (*(short*)(e + 0x28) > 0x1000) *(short*)(e + 0x28) = 0x1000;

        *(short*)(e + 0x2a) += 0x1f4;
        if (*(short*)(e + 0x2a) > 0x1000) *(short*)(e + 0x2a) = 0x1000;

        short radiusNow = *(short*)(e + 0x12);
        short phase = *(short*)(e + 0x16);

        *(int*)(e + 0x1c) =
            (int)Winsys3d.GetSinTableEntry(phase) * (radiusNow >> 2) + *(int*)(e + 0x50);
        *(int*)(e + 0x24) =
            Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * radiusNow + *(int*)(e + 0x58);
        *(int*)(e + 0x20) =
            Winsys3d.GetCosTableEntry(phase) * (radiusNow >> 2) + *(int*)(e + 0x54);

        short countdown = *(short*)(e + 0x14);
        *(short*)(e + 0x14) = (short)(countdown + 1);
        *(short*)(e + 0x12) = (short)(radiusNow - (countdown >> 1));
        *(short*)(e + 0x16) = (short)((phase + 0x64) & 0xfff);

        *(int*)(e + 0x1c) += (CrtRand() % 6 - 3) * 0x1000;
        *(int*)(e + 0x24) += (CrtRand() % 6 - 3) * 0x1000;

        if (*(short*)(e + 0x12) < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: FUN_00417f80 @ 0x00417f80 — l'eclaboussure cardinale des sous-zones 0, 1, 2
    /// et 4. Decompilee dans <c>Blaze.c</c>.</summary>
    private static void EntityHandler_00417F80(byte* e)
        => TickCardinalSplash_00417F80(e, Descriptors0054FA18());             // Ghidra: &DAT_0054fa18

    /// <summary>Ghidra: FUN_00419b90 @ 0x00419b90 — le meme corps pour les sous-zones 5, 6, 8 et
    /// 10 ; le diff ne rend que <c>mov dword [esi+3Ch],54FBB8h</c>.</summary>
    private static void EntityHandler_00419B90(byte* e)
        => TickCardinalSplash_00417F80(e, Descriptors0054FBB8());             // Ghidra: &DAT_0054fbb8

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00417F80"/> et <see cref="EntityHandler_00419B90"/> :
    /// <b>une eclaboussure projetee vers un des quatre points cardinaux</b>, qui monte, marque un
    /// temps, puis retombe en quatre images.
    ///
    /// <para>
    /// <b>Sa direction vient des bits que son lanceur a laisses dans <c>+0x12</c></b>, et les quatre
    /// sont testes independamment : le bit 1 la pousse de 100 unites en <c>+X</c>, le bit 2 de 100
    /// en <c>+Z</c>, le bit 4 de 100 en <c>-X</c>, le bit 8 de 100 en <c>-Z</c> — et l'axe libre
    /// recoit a chaque fois un brouillage de ±100 unites. Deux bits poses donnent donc une diagonale
    /// bruitee sur les deux axes. Le mot est remis a zero juste apres, car il redevient le compteur
    /// d'images.
    /// </para>
    /// <para>Elle monte de <b>15 a 29 unites</b> a la naissance, prend le bit <c>0x4000</c> et une
    /// echelle de <c>0x800</c> sur les trois axes.</para>
    /// <para>
    /// <b>L'etat 1 est une attente a duree aleatoire</b> : une chance sur cinq par image, et sept
    /// images au plus. En sortant, elle rend le bit <c>0x4000</c>, remet <c>+0x16</c> a zero et
    /// arme une vitesse de chute <c>+0x14</c> tiree entre <b>2 et 11</b>.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est la chute</b> : la hauteur perd <c>+0x14</c> unites par image et la vitesse
    /// gagne deux — une acceleration constante. Toutes les six images l'image de la planche avance
    /// d'un cran, et l'eclaboussure disparait a la quatrieme : c'est exactement le nombre
    /// d'enregistrements de <c>0x0054fa18</c> comme de <c>0x0054fbb8</c>.
    /// </para>
    /// </summary>
    private static void TickCardinalSplash_00417F80(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
            *(short*)(e + 0x2c) = 0x800;
            *(byte**)(e + 0x3c) = plate;
            e[0x0a] = 2;
            e[0x0b] = 1;

            if ((e[0x12] & 1) != 0)
            {
                *(int*)(e + 0x1c) += 0x64000;
                *(int*)(e + 0x24) += (CrtRand() % 200 - 100) * 0x1000;
            }

            if ((e[0x12] & 2) != 0)
            {
                *(int*)(e + 0x1c) += (CrtRand() % 200 - 100) * 0x1000;
                *(int*)(e + 0x24) += 0x64000;
            }

            if ((e[0x12] & 4) != 0)
            {
                *(int*)(e + 0x1c) -= 0x64000;
                *(int*)(e + 0x24) += (CrtRand() % 200 - 100) * 0x1000;
            }

            if ((e[0x12] & 8) != 0)
            {
                *(int*)(e + 0x1c) += (CrtRand() % 200 - 100) * 0x1000;
                *(int*)(e + 0x24) -= 0x64000;
            }

            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;
            *(uint*)e |= 0x4000;

            *(int*)(e + 0x20) += (CrtRand() % 0x0f + 0x0f) * 0x1000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if ((short)(CrtRand() % 5) == 1 || *(short*)(e + 0x12) > 6)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;

                int speed = CrtRand();                       // Ghidra: le tirage precede l'ecriture
                *(short*)(e + 0x16) = 0;
                *(short*)(e + 0x14) = (short)(speed % 10 + 2);
                *(uint*)e &= 0xffffbfff;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x12))++;
            *(int*)(e + 0x20) += *(short*)(e + 0x14) * -0x1000;
            *(short*)(e + 0x14) += 2;

            if (*(short*)(e + 0x12) > 5)
            {
                *(short*)(e + 0x12) = 0;

                sbyte frame = (sbyte)((sbyte)e[0x40] + 1);
                e[0x40] = (byte)frame;

                if (frame == 4) *(uint*)e &= 0x7fffffff;
            }
        }
    }
}
