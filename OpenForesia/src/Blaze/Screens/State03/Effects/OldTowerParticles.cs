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
        => TickTighteningSpiral_00417E10(e, Descriptors0054FA58(), 0xc8, 0xfa, false);  // &DAT_0054fa58

    /// <summary>Ghidra: <c>0x00419a20</c> — le meme corps pour les sous-zones 5, 6, 8 et 10 ; le
    /// diff ne rend que <c>mov dword [esi+3Ch],54FC00h</c>.</summary>
    private static void EntityHandler_00419A20(byte* e)
        => TickTighteningSpiral_00417E10(e, Descriptors0054FC00(), 0xc8, 0xfa, false);  // &DAT_0054fc00

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
    private static void TickTighteningSpiral_00417E10(
        byte* e, byte* plate, int radiusSpan, int radiusBase, bool majorAxisVertical)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;

            int radius = CrtRand() % radiusSpan;              // Ghidra: FUN_00547140
            *(short*)(e + 0x14) = 0x0a;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)(radius + radiusBase);
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

        // L'axe long — le seul a prendre le rayon entier et le CAP au lieu de la phase — est le Z
        // pour 0x00417E10 et le Y pour 0x00417CA0. Le binaire n'echange rien d'autre : les deux
        // instructions qui different sont `mov ecx,[esi+58h]` / `[esi+54h]` et les deux `mov`
        // d'ecriture. Voir le bandeau de TickTighteningSpiral_00417CA0.
        int major = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * radiusNow;
        int minor = Winsys3d.GetCosTableEntry(phase) * (radiusNow >> 2);

        if (majorAxisVertical)
        {
            *(int*)(e + 0x20) = major + *(int*)(e + 0x54);
            *(int*)(e + 0x24) = minor + *(int*)(e + 0x58);
        }
        else
        {
            *(int*)(e + 0x24) = major + *(int*)(e + 0x58);
            *(int*)(e + 0x20) = minor + *(int*)(e + 0x54);
        }

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

    // =====================================================================
    // LA CATEGORIE C DE LA TABLE 0x0056b2c8 (2026-09-17, lot 2)
    //
    // Neuf formes pour treize creneaux. Quatre paires inter-blocs sont des QUASI-copies : le
    // desassemblage tronque au dernier `ret` ne rend que la planche et, pour deux d'entre elles, une
    // constante. Elles sont donc partagees, avec la difference NOMMEE en parametre.
    //
    //   0x00417CA0 / 0x004198B0  planche + rayon (0x96+0xAA contre 0xC8+0xFA) + AXE LONG
    //   0x00417B10 / 0x00419720  planche seule
    //   0x00417770 / 0x00419380  planche seule (plus un reordonnancement du compilateur)
    //   0x00417420 / 0x00419030  planche + l'octet +0x0a (5 contre 0xA)
    //
    // Les cinq autres n'existent que dans un bloc.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054fa80</c> — quatre tuiles <c>0x20 x 0x20</c> en rangee, page
    /// <c>0x03</c>, role 1, atlas <c>0x18</c>. Bornee par <c>0x0054faa0</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FA80 =
    [
        0x00, 0x00, 0x20, 0x20, 0x01, 0x03, 0x18, 0x00,   // 0x0054fa80  <- 0x00417770
        0x20, 0x00, 0x20, 0x20, 0x01, 0x03, 0x18, 0x00,   // 0x0054fa88
        0x40, 0x00, 0x20, 0x20, 0x01, 0x03, 0x18, 0x00,   // 0x0054fa90
        0x60, 0x00, 0x20, 0x20, 0x01, 0x03, 0x18, 0x00,   // 0x0054fa98
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fc28</c> — la copie de <see cref="s_Descriptors_0054FA80"/>
    /// pour les sous-zones 5, 6, 8 et 10, memes octets. Bornee par <c>0x0054fc48</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FC28 =
    [
        0x00, 0x00, 0x20, 0x20, 0x01, 0x03, 0x18, 0x00,   // 0x0054fc28  <- 0x00419380
        0x20, 0x00, 0x20, 0x20, 0x01, 0x03, 0x18, 0x00,   // 0x0054fc30
        0x40, 0x00, 0x20, 0x20, 0x01, 0x03, 0x18, 0x00,   // 0x0054fc38
        0x60, 0x00, 0x20, 0x20, 0x01, 0x03, 0x18, 0x00,   // 0x0054fc40
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054faa0</c> — quatre tuiles <c>0x1f x 0x1f</c> de la seconde
    /// rangee, page <c>0x03</c>, atlas <c>0x18</c>. Bornee par <c>0x0054fac0</c>, la planche du feu
    /// follet.</summary>
    private static readonly byte[] s_Descriptors_0054FAA0 =
    [
        0x00, 0x20, 0x1f, 0x1f, 0x01, 0x03, 0x18, 0x00,   // 0x0054faa0  <- 0x00417420
        0x20, 0x20, 0x1f, 0x1f, 0x01, 0x03, 0x18, 0x00,   // 0x0054faa8
        0x40, 0x20, 0x1f, 0x1f, 0x01, 0x03, 0x18, 0x00,   // 0x0054fab0
        0x60, 0x20, 0x1f, 0x1f, 0x01, 0x03, 0x18, 0x00,   // 0x0054fab8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fc48</c> — la copie de <see cref="s_Descriptors_0054FAA0"/>
    /// pour les sous-zones 5, 6, 8 et 10. Bornee par <c>0x0054fc68</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FC48 =
    [
        0x00, 0x20, 0x1f, 0x1f, 0x01, 0x03, 0x18, 0x00,   // 0x0054fc48  <- 0x00419030
        0x20, 0x20, 0x1f, 0x1f, 0x01, 0x03, 0x18, 0x00,   // 0x0054fc50
        0x40, 0x20, 0x1f, 0x1f, 0x01, 0x03, 0x18, 0x00,   // 0x0054fc58
        0x60, 0x20, 0x1f, 0x1f, 0x01, 0x03, 0x18, 0x00,   // 0x0054fc60
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fb60</c> — quatre tuiles <c>0x1f x 0x1f</c> de la page
    /// <c>0x02</c>, atlas <c>0x18</c>. Bornee par <c>0x0054fb80</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FB60 =
    [
        0x80, 0x00, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054fb60  <- 0x00418120
        0xa0, 0x00, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054fb68
        0xc0, 0x00, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054fb70
        0xe0, 0x00, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054fb78
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fbf0</c> — <b>une seule tuile</b> : la suivante,
    /// <c>0x0054fbf8</c>, appartient a <see cref="s_Descriptors_0054FBF8"/> et ne differe que par sa
    /// page. Le corps qui l'installe laisse <c>+0x40</c> a zero.</summary>
    private static readonly byte[] s_Descriptors_0054FBF0 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x0054fbf0  <- 0x00419720
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fbf8</c> — une seule tuile, page <c>0x02</c>. Bornee par
    /// <c>0x0054fc00</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FBF8 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054fbf8  <- 0x00418AF0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fd80</c> — quatre tuiles <c>0x20 x 0x20</c>, page
    /// <c>0x02</c>, atlas <c>0x18</c>. Bornee par <c>0x0054fda0</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FD80 =
    [
        0x00, 0x00, 0x20, 0x20, 0x01, 0x02, 0x18, 0x00,   // 0x0054fd80  <- 0x0041C0E0
        0x20, 0x00, 0x20, 0x20, 0x01, 0x02, 0x18, 0x00,   // 0x0054fd88
        0x40, 0x00, 0x20, 0x20, 0x01, 0x02, 0x18, 0x00,   // 0x0054fd90
        0x60, 0x00, 0x20, 0x20, 0x01, 0x02, 0x18, 0x00,   // 0x0054fd98
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054fe00</c> — <b>douze enregistrements d'un seul tenant</b>, et c'est une
    /// decision, pas une commodite.
    ///
    /// <para>
    /// Le binaire y voit deux planches : <c>0x0054fe00</c> (cinq tuiles <c>0x2f x 0x2f</c>) et
    /// <c>0x0054fe28</c> (sept tuiles <c>0x1f x 0x1f</c>). Mais
    /// <see cref="TickTwoFacedPlume_0041B7C0"/> mene <c>+0x40</c> de 0 a <b>6</b> pour les deux, si
    /// bien que la premiere <b>deborde de deux enregistrements sur la seconde</b>. Les deux plages
    /// etant contigues dans <c>.data</c>, le jeu affiche alors les deux premieres tuiles de
    /// <c>0x0054fe28</c> — c'est ce que fait le binaire, et le declarer en deux tableaux separes
    /// produirait de la memoire non initialisee. Une seule allocation, deux vues.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054FE00 =
    [
        0x00, 0x00, 0x2f, 0x2f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe00  <- 0x0041B7C0, variante 1
        0x30, 0x00, 0x2f, 0x2f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe08
        0x60, 0x00, 0x2f, 0x2f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe10
        0x90, 0x00, 0x2f, 0x2f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe18
        0xc0, 0x00, 0x2f, 0x2f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe20
        0x00, 0x50, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe28  <- 0x0041B7C0, variante 0
        0x20, 0x50, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe30
        0x40, 0x50, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe38
        0x60, 0x50, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe40
        0x80, 0x50, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe48
        0xa0, 0x50, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe50
        0xc0, 0x50, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x0054fe58
    ];

    private static byte* s_Descriptors0054FA80Storage;
    private static byte* s_Descriptors0054FAA0Storage;
    private static byte* s_Descriptors0054FB60Storage;
    private static byte* s_Descriptors0054FBF0Storage;
    private static byte* s_Descriptors0054FBF8Storage;
    private static byte* s_Descriptors0054FC28Storage;
    private static byte* s_Descriptors0054FC48Storage;
    private static byte* s_Descriptors0054FD80Storage;
    private static byte* s_Descriptors0054FE00Storage;

    private static byte* Descriptors0054FA80()
        => NativeCopy(s_Descriptors_0054FA80, ref s_Descriptors0054FA80Storage);

    private static byte* Descriptors0054FAA0()
        => NativeCopy(s_Descriptors_0054FAA0, ref s_Descriptors0054FAA0Storage);

    private static byte* Descriptors0054FB60()
        => NativeCopy(s_Descriptors_0054FB60, ref s_Descriptors0054FB60Storage);

    private static byte* Descriptors0054FBF0()
        => NativeCopy(s_Descriptors_0054FBF0, ref s_Descriptors0054FBF0Storage);

    private static byte* Descriptors0054FBF8()
        => NativeCopy(s_Descriptors_0054FBF8, ref s_Descriptors0054FBF8Storage);

    private static byte* Descriptors0054FC28()
        => NativeCopy(s_Descriptors_0054FC28, ref s_Descriptors0054FC28Storage);

    private static byte* Descriptors0054FC48()
        => NativeCopy(s_Descriptors_0054FC48, ref s_Descriptors0054FC48Storage);

    private static byte* Descriptors0054FD80()
        => NativeCopy(s_Descriptors_0054FD80, ref s_Descriptors0054FD80Storage);

    private static byte* Descriptors0054FE00()
        => NativeCopy(s_Descriptors_0054FE00, ref s_Descriptors0054FE00Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054fe28</c> — la seconde vue de
    /// <see cref="s_Descriptors_0054FE00"/>, a <c>+0x28</c>. Voir son bandeau pour pourquoi les deux
    /// vivent dans une seule allocation.</summary>
    private static byte* Descriptors0054FE28() => Descriptors0054FE00() + 0x28;

    // ---------------------------------------------------------------- les quatre quasi-copies

    /// <summary>Ghidra: <c>0x00417ca0</c> — la spirale DRESSEE des sous-zones 0, 1, 2 et 4.
    /// <b>Absente de <c>Blaze.c</c></b>. Corps commun avec
    /// <see cref="TickTighteningSpiral_00417E10"/>.</summary>
    private static void EntityHandler_00417CA0(byte* e)
        => TickTighteningSpiral_00417E10(e, Descriptors0054FA58(), 0x96, 0xaa, true);

    /// <summary>Ghidra: <c>0x004198b0</c> — la meme, sous-zones 5, 6, 8 et 10.</summary>
    private static void EntityHandler_004198B0(byte* e)
        => TickTighteningSpiral_00417E10(e, Descriptors0054FC00(), 0x96, 0xaa, true);

    /// <summary>Ghidra: FUN_00417b10 @ 0x00417b10 — le scintillement lointain des sous-zones 0, 1,
    /// 2 et 4. Decompilee dans <c>Blaze.c</c>.</summary>
    private static void EntityHandler_00417B10(byte* e)
        => TickTwinSparkle_00417B10(e, Descriptors0054FA50());               // Ghidra: &DAT_0054fa50

    /// <summary>Ghidra: FUN_00419720 @ 0x00419720 — la meme, sous-zones 5, 6, 8 et 10 ; le diff ne
    /// rend que <c>mov dword [esi+3Ch],54FBF0h</c>.</summary>
    private static void EntityHandler_00419720(byte* e)
        => TickTwinSparkle_00417B10(e, Descriptors0054FBF0());               // Ghidra: &DAT_0054fbf0

    /// <summary>
    /// Le corps des deux : <b>une entite qui dessine DEUX points</b>, une image sur deux chacun.
    ///
    /// <para>
    /// C'est tout l'interet de la fonction, et elle le fait avant meme de regarder son etat :
    /// <b>elle echange sa position avec une seconde rangee en <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c></b>.
    /// Le dessin d'une image porte donc sur l'une, celui de la suivante sur l'autre — un seul creneau
    /// d'entite pour deux scintillements, et c'est pourquoi les deux positions sont tirees separement
    /// a la naissance.
    /// </para>
    /// <para>
    /// Les tirages sont larges et <b>ne sont pas centres</b> : X dans <c>[-3100, -2301]</c>,
    /// Y dans <c>[-200, 999]</c>, Z dans <c>[-2000, 1999]</c> pour la premiere, et Z dans
    /// <c>[-2000, 1500]</c> pour la seconde. Ce sont des coordonnees absolues, pas des ecarts : le
    /// scintillement est place dans le decor, pas autour de son lanceur.
    /// </para>
    /// <para>
    /// Ensuite il monte de <c>+0x12</c> unites par image — la valeur est <b>soustraite</b> —, derive
    /// en Z de <c>0x11ffb</c> plus un bruit, et <b>le bit <c>0x4000</c> s'allume et s'eteint selon
    /// que le Z sort de la plage <c>[-0x320000, 0x578000]</c></b>. Il meurt quand sa hauteur passe
    /// sous <c>-0x190000</c>.
    /// </para>
    /// <para>
    /// <b>Les deux derives ne sont pas mises a l'echelle</b> (<c>rand % 10 - 5</c> ajoute tel quel a
    /// <c>+0x1c</c>, sans <c>&lt;&lt; 0xc</c>), contrairement a tout le reste du fichier. Transcrit
    /// tel quel : a cette echelle c'est un bruit sous l'unite, ce qui est peut-etre l'intention.
    /// </para>
    /// </summary>
    private static void TickTwinSparkle_00417B10(byte* e, byte* plate)
    {
        // L'echange, en tete et sans condition.
        int x = *(int*)(e + 0x1c);
        int y = *(int*)(e + 0x20);
        int z = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) = *(int*)(e + 0x44);
        *(int*)(e + 0x44) = x;
        *(int*)(e + 0x20) = *(int*)(e + 0x48);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c);
        *(int*)(e + 0x48) = y;
        *(int*)(e + 0x4c) = z;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;

            *(int*)(e + 0x1c) = (CrtRand() % 800 - 0xc1c) * 0x1000;
            *(int*)(e + 0x20) = (CrtRand() % 0x4b0 - 200) * 0x1000;
            *(int*)(e + 0x24) = (CrtRand() % 4000 - 2000) * 0x1000;
            *(int*)(e + 0x44) = (CrtRand() % 800 - 0xc1c) * 0x1000;
            *(int*)(e + 0x48) = (CrtRand() % 0x4b0 - 200) * 0x1000;

            int far = CrtRand();
            *(short*)(e + 0x28) = 0x400;
            *(short*)(e + 0x2a) = 0x400;
            *(int*)(e + 0x4c) = (far % 0xdac - 2000) * 0x1000;

            int rise = CrtRand();
            *(short*)(e + 0x14) = 0;
            e[0x0b] = 0;
            e[0x0a] = 9;
            *(short*)(e + 0x12) = (short)(rise % 0x1e + 10);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int depth = *(int*)(e + 0x24);
            *(uint*)e = depth < -0x320000 || depth > 0x578000
                ? *(uint*)e | 0x4000
                : *(uint*)e & 0xffffbfff;

            *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x1000;
            *(int*)(e + 0x1c) += CrtRand() % 10 - 5;

            int drift = CrtRand();
            int height = *(int*)(e + 0x20);
            *(int*)(e + 0x24) += drift % 10 + 0x11ffb;

            if (height < -0x190000) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>Ghidra: <c>0x00417770</c> — l'orbite clignotante des sous-zones 0, 1, 2 et 4.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_00417770.asm</c>.</summary>
    private static void EntityHandler_00417770(byte* e)
        => TickBlinkingOrbit_00417770(e, Descriptors0054FA80());             // Ghidra: &DAT_0054fa80

    /// <summary>Ghidra: <c>0x00419380</c> — la meme, sous-zones 5, 6, 8 et 10. Le diff rend la
    /// planche et un <b>reordonnancement du compilateur</b> sur le calcul de <c>+0x98</c> : les
    /// memes cinq instructions, dans un autre ordre, pour le meme resultat.</summary>
    private static void EntityHandler_00419380(byte* e)
        => TickBlinkingOrbit_00417770(e, Descriptors0054FC28());             // Ghidra: &DAT_0054fc28

    /// <summary>
    /// Le corps des deux : <b>une lueur qui attend, puis monte en tournant et clignote en
    /// mourant</b>.
    ///
    /// <para>
    /// Elle nait 50 unites plus haut, opaque (<c>0x4000</c>), avec une phase <c>+0x12</c> et une
    /// echelle tirees, un rayon <c>+0x16</c> de 15 a 22, une vitesse <c>+0x14</c> de 10 a 20 et une
    /// attente <c>+0x9a</c> de 0 a 9 images.
    /// </para>
    /// <para>
    /// <b>Sa cadence d'image est calculee a la naissance</b>, et par le plus petit des deux termes :
    /// <c>+0x98</c> vaut <c>(vitesse + 20) / 4</c> si celui-ci est inferieur au rayon, sinon
    /// <c>rayon / 4</c>. Le binaire choisit par un <c>cmp</c> entre la somme et le rayon, pas entre
    /// les quotients — transcrit tel quel.
    /// </para>
    /// <para>
    /// L'etat 2 fait monter la lueur de <c>+0x14</c> unites par image, la vitesse decroissant d'une
    /// par image jusqu'a passer sous <c>-20</c>, ou elle meurt ; la position tourne autour de sa
    /// propre position precedente (les deux termes sont <b>ajoutes</b>, pas recalcules depuis une
    /// ancre). <b>Et des que l'image atteint 2, le bit <c>0x4000</c> suit la parite de la
    /// vitesse</b> : la lueur clignote une image sur deux en finissant.
    /// </para>
    /// <para>
    /// Le test <c>di &lt; 0</c> sur le rayon ne peut jamais etre vrai — le rayon est tire entre 15 et
    /// 22 et n'est jamais reecrit — mais il est conserve : c'est le binaire qui le porte.
    /// </para>
    /// </summary>
    private static void TickBlinkingOrbit_00417770(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x0a] = 1;
            e[0x0b] = 0;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);
            e[0x40] = 0;

            short scale = (short)(CrtRand() % 0x400 + 0x708);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(short*)(e + 0x14) = (short)(CrtRand() % 0x0b + 0x0a);
            *(short*)(e + 0x16) = (short)(CrtRand() % 8 + 0x0f);
            *(short*)(e + 0x9a) = (short)(CrtRand() % 10);

            *(int*)(e + 0x5c) = 0;

            int reach = *(short*)(e + 0x14) + 0x14;
            short radius = *(short*)(e + 0x16);
            *(short*)(e + 0x98) = reach < radius ? (short)(reach >> 2) : (short)(radius >> 2);

            *(int*)(e + 0x20) += 0x32000;
            *(uint*)e |= 0x4000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int waited = *(int*)(e + 0x5c) + 1;
            *(int*)(e + 0x5c) = waited;

            if (*(short*)(e + 0x9a) <= waited)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0xffffbfff;
                *(int*)(e + 0x5c) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        byte frame = e[0x40];
        short speed = (short)(*(short*)(e + 0x14) - 1);
        *(short*)(e + 0x14) = speed;

        if (frame >= 2)
        {
            // Ghidra: `test al,1 ; or ch,40h` / `and ch,0BFh` — la parite de la vitesse.
            *(uint*)e = (speed & 1) == 0 ? *(uint*)e | 0x4000 : *(uint*)e & 0xffffbfff;
        }

        *(int*)(e + 0x20) += speed * 0x1000;

        short phase = *(short*)(e + 0x12);
        short radiusNow = *(short*)(e + 0x16);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(phase) * radiusNow;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(phase) * radiusNow;

        if (radiusNow < 0 || *(short*)(e + 0x14) < -0x14) *(uint*)e &= 0x7fffffff;

        int ticks = *(int*)(e + 0x5c) + 1;
        *(int*)(e + 0x5c) = ticks;

        if (ticks >= *(short*)(e + 0x98))
        {
            *(int*)(e + 0x5c) = 0;
            byte next = (byte)(e[0x40] + 1);
            e[0x40] = next;
            if (next >= 4) e[0x40] = 3;
        }
    }

    /// <summary>Ghidra: <c>0x00417420</c> — le feu follet invoque des sous-zones 0, 1, 2 et 4.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_00417420.asm</c>.</summary>
    private static void EntityHandler_00417420(byte* e)
        => TickSummonedWisp_00417420(e, Descriptors0054FAA0(), 5);           // Ghidra: &DAT_0054faa0

    /// <summary>Ghidra: <c>0x00419030</c> — la meme, sous-zones 5, 6, 8 et 10 ; le diff rend la
    /// planche et <c>mov byte [esi+0Ah],0Ah</c> au lieu de <c>5</c>.</summary>
    private static void EntityHandler_00419030(byte* e)
        => TickSummonedWisp_00417420(e, Descriptors0054FC48(), 0x0a);        // Ghidra: &DAT_0054fc48

    /// <summary>
    /// Le corps des deux : <b>une lueur qui recule de son invocateur, puis rejoint un point fixe du
    /// decor et y tourne en s'eteignant</b>.
    ///
    /// <para>
    /// <b>Le recul est en deux temps et au meme cap.</b> A la naissance elle saute de
    /// <b>80 unites de table</b> en arriere du cap <c>+0x32</c> et de 80 unites en hauteur ; puis,
    /// dix images durant, elle recule encore de <b>15</b> par image. Le binaire ecrit les deux par
    /// des chaines <c>neg ; shl ; sub ; shl</c> — d'ou les facteurs, qui ne sont pas des constantes
    /// lisibles.
    /// </para>
    /// <para>
    /// <b>Puis elle est teleportee</b> : l'ancre <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> recoit
    /// <c>(0x898000, 0x1a4000, 0)</c>, trois coordonnees <b>en dur</b> — un point precis de la carte,
    /// le meme dans les deux blocs. A partir de la, position X et Z sont <b>recalculees</b> a
    /// 150 unites de table autour de ce point, sur une phase <c>+0x14</c> qui avance de
    /// <c>+0x16</c> (50 a 149) par image, pendant que la hauteur monte de <c>0x8000</c>.
    /// </para>
    /// <para>
    /// Apres 28 images de tour, l'echelle perd <c>0xa0</c> par image et la lueur disparait quand elle
    /// atteint zero. L'image de la planche avance toutes les trois images et boucle sur quatre, et
    /// les trois axes prennent un brouillage de ±10 unites <b>a chaque image et dans tous les
    /// etats</b> — le bloc est apres les trois <c>if</c>.
    /// </para>
    /// </summary>
    private static void TickSummonedWisp_00417420(byte* e, byte* plate, byte role)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;

            short course = *(short*)(e + 0x32);

            // Ghidra: `neg ecx ; shl ecx,2 ; sub ecx,eax ; shl ecx,4` — soit -80 fois la table.
            int sin = (int)Winsys3d.GetSinTableEntry(course);
            *(int*)(e + 0x1c) += ((-sin << 2) - sin) << 4;

            int cos = Winsys3d.GetCosTableEntry(course);
            *(int*)(e + 0x24) += ((-cos << 2) - cos) << 4;

            *(int*)(e + 0x20) += 0x50000;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
            *(short*)(e + 0x12) = 0;
            e[0x0a] = role;
            e[0x0b] = 0;
            *(short*)(e + 0x9a) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short course = *(short*)(e + 0x32);

            // Ghidra: `shl ecx,4 ; sub ecx,eax ; neg ecx` — soit -15 fois la table.
            int sin = (int)Winsys3d.GetSinTableEntry(course);
            *(int*)(e + 0x1c) += -((sin << 4) - sin);

            int cos = Winsys3d.GetCosTableEntry(course);
            *(int*)(e + 0x24) += -((cos << 4) - cos);

            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) >= 10)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(int*)(e + 0x50) = 0x898000;
                *(int*)(e + 0x54) = 0x1a4000;
                *(int*)(e + 0x58) = 0;
                *(short*)(e + 0x14) = (short)(CrtRand() % 0x1000);
                *(short*)(e + 0x16) = (short)(CrtRand() % 0x64 + 0x32);
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short phase = *(short*)(e + 0x14);

            // Ghidra: trois `lea` puis `lea edx,[ecx+eax*2]` — 3, 15, 75, puis le doublement : 150.
            *(int*)(e + 0x1c) = *(int*)(e + 0x50) + (int)Winsys3d.GetSinTableEntry(phase) * 150;
            *(int*)(e + 0x24) = *(int*)(e + 0x58) + Winsys3d.GetCosTableEntry(phase) * 150;

            *(short*)(e + 0x14) += *(short*)(e + 0x16);
            *(int*)(e + 0x20) += 0x8000;

            if (*(short*)(e + 0x14) >= 0x1000) *(short*)(e + 0x14) -= 0x1000;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 0x1c)
            {
                *(short*)(e + 0x28) -= 0xa0;
                *(short*)(e + 0x2a) -= 0xa0;
                if (*(short*)(e + 0x28) <= 0) *(uint*)e &= 0x7fffffff;
            }
        }

        (*(short*)(e + 0x9a))++;
        if (*(short*)(e + 0x9a) >= 3)
        {
            *(short*)(e + 0x9a) = 0;
            byte next = (byte)(e[0x40] + 1);
            e[0x40] = next;
            if (next == 4) e[0x40] = 0;
        }

        *(int*)(e + 0x1c) += (CrtRand() % 0x14 - 0x0a) * 0x1000;
        *(int*)(e + 0x20) += (CrtRand() % 0x14 - 0x0a) * 0x1000;
        *(int*)(e + 0x24) += (CrtRand() % 0x14 - 0x0a) * 0x1000;
    }

    // ---------------------------------------------------------------- les cinq formes uniques

    /// <summary>
    /// Ghidra: <c>0x00418120</c> — <b>la braise qui attend, monte et s'eteint</b>, sous-zones 0, 1, 2
    /// et 4. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_00418120.asm</c>.
    ///
    /// <para>
    /// Trois etats nets. A la naissance : une echelle tiree entre <c>0xbb8</c> et <c>0xfb7</c>, un
    /// brouillage de ±100 unites sur X et Z, une attente <c>+0x12</c> de 0 a 5 images, une vitesse
    /// de montee <c>+0x14</c> de 12 a 21, un <b>assombrissement</b> <c>+0x16</c> de -3 a -12 par
    /// image, une teinte de depart a <c>0x80</c> sur les trois canaux, et une cadence d'image
    /// <c>+0x5c</c> de 2 a 4.
    /// </para>
    /// <para>
    /// L'etat 1 est l'attente, opaque (<c>0x4000</c>) ; l'etat 2 monte de <c>+0x14</c> unites par
    /// image, la vitesse perdant une unite par image, en assombrissant la teinte de <c>+0x16</c>.
    /// <b>C'est la teinte qui la tue</b>, pas le compteur : la braise disparait quand son canal rouge
    /// atteint zero.
    /// </para>
    /// </summary>
    private static void TickDimmingEmber_00418120(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054FB60();                     // Ghidra: &DAT_0054fb60
            e[0x40] = 0;

            short scale = (short)(CrtRand() % 0x400 + 0xbb8);
            e[0x0a] = 7;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            int wait = CrtRand() % 6;
            *(uint*)e |= 0x4000;
            *(short*)(e + 0x12) = (short)wait;

            *(int*)(e + 0x1c) += (CrtRand() % 0xc8 - 0x64) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0xc8 - 0x64) * 0x1000;

            *(short*)(e + 0x14) = (short)(CrtRand() % 0x0a + 0x0c);
            *(short*)(e + 0x16) = (short)(-3 - CrtRand() % 0x0a);

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;

            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = CrtRand() % 3 + 2;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))--;
            if (*(short*)(e + 0x12) <= 0)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0xffffbfff;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        short speed = *(short*)(e + 0x14);
        *(int*)(e + 0x20) += speed * 0x1000;
        *(short*)(e + 0x14) = (short)(speed - 1);

        short fade = *(short*)(e + 0x16);
        BrightenEntityTint_00404DE0(e, fade, fade, fade);                    // Ghidra: FUN_00404de0

        (*(short*)(e + 0x9a))++;
        if (*(short*)(e + 0x9a) >= *(int*)(e + 0x5c))
        {
            *(short*)(e + 0x9a) = 0;
            byte next = (byte)(e[0x40] + 1);
            e[0x40] = next;
            if (next == 4) e[0x40] = 0;
        }

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0041c0e0</c> — <b>la volute qui s'ouvre en montant</b>, sous-zones 3, 7 et 9.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0041C0E0.asm</c>.
    ///
    /// <para>
    /// Elle nait jusqu'a 80 unites <b>plus bas</b> que son point, fige la son ancre, et tourne autour
    /// d'elle sur une phase <c>+0x12</c> tiree — mais <b>le rayon est <c>+0x9a</c>, qui grandit de
    /// <c>+0x14</c> a chaque image</b> : la spirale s'ouvre au lieu de se fermer.
    /// </para>
    /// <para>
    /// La montee <c>+0x16</c> part de 5 a 24 et perd une unite par image, <b>bornee a -20</b> : la
    /// volute monte, s'arrete, puis redescend a vitesse constante. Elle s'assombrit de 10 par image
    /// et meurt sur sa teinte, comme <see cref="TickDimmingEmber_00418120"/>.
    /// </para>
    /// <para>
    /// <b>Le pas de croissance se lit sur l'ancien <c>+0x5c</c></b> (<c>lea ecx,[edi+4]</c> et
    /// <c>sar edi,1</c> avant que <c>+0x5c</c> ne soit remis a zero) : <c>rand % (v + 4) + v / 2</c>,
    /// ou <c>v</c> est ce que le lanceur y avait laisse. C'est le seul champ que ce corps herite.
    /// </para>
    /// <para>L'image avance <b>deux images sur quatre</b> — le binaire teste le bit 1 du compteur, pas
    /// un modulo — et se bloque a 3.</para>
    /// </summary>
    private static void TickOpeningVolute_0041C0E0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054FD80();                     // Ghidra: &DAT_0054fd80
            e[0x0a] = 2;
            e[0x0b] = 0;
            e[0x40] = 0;

            int drop = *(int*)(e + 0x20) - ((CrtRand() % 0x50) << 0xc);
            *(int*)(e + 0x20) = drop;
            *(int*)(e + 0x54) = drop;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);

            // Ghidra: `mov edi,[esi+5Ch]` AVANT la remise a zero — le pas vient du lanceur.
            int seed = *(int*)(e + 0x5c);
            *(short*)(e + 0x14) = (short)(CrtRand() % (seed + 4) + (seed >> 1));

            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
            *(short*)(e + 0x16) = (short)(CrtRand() % 0x14 + 5);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short phase = *(short*)(e + 0x12);
        short radius = *(short*)(e + 0x9a);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(phase) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(phase) * radius + *(int*)(e + 0x58);

        short rise = *(short*)(e + 0x16);
        *(int*)(e + 0x20) += rise * 0x1000;
        rise--;
        *(short*)(e + 0x16) = rise;
        if (rise < -0x14) *(short*)(e + 0x16) = -0x14;

        *(short*)(e + 0x9a) = (short)(*(short*)(e + 0x14) + radius);
        BrightenEntityTint_00404DE0(e, -10, -10, -10);                       // Ghidra: FUN_00404de0

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;

        int ticks = *(int*)(e + 0x5c) + 1;
        *(int*)(e + 0x5c) = ticks;

        // Ghidra: `test al,2 ; jne` — l'octet bas du compteur, donc deux images sur quatre.
        if ((ticks & 2) != 0) return;

        byte next = (byte)(e[0x40] + 1);
        e[0x40] = next;
        if (next >= 4) e[0x40] = 3;
    }

    /// <summary>
    /// Ghidra: FUN_00416920 @ 0x00416920 — <b>l'eclat qui derive en ligne droite</b>, sous-zones 0,
    /// 1, 2 et 4. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Le plus simple du lot. A la naissance il se disperse de ±25 unites a l'horizontale, monte de
    /// 60 a 79, prend une echelle de <c>0xc00</c> a <c>0x13ff</c>, une teinte de <c>0x5a</c> sur les
    /// trois canaux, et <b>trois vitesses constantes</b> rangees dans <c>+0x50</c>, <c>+0x54</c> et
    /// <c>+0x58</c> : -2 a 1 en X et Z, 1 a 3 en hauteur. Il vit de 5 a 9 images, s'assombrit de 10
    /// par image, et <b>tire son image au sort a chaque tour</b> (<c>+0x40 = rand &amp; 3</c>) — le
    /// seul corps du fichier qui ne suit pas une cadence.
    /// </para>
    /// <para>Son role de dessin est <c>0xfb</c> en <c>+0x0b</c>, une valeur qu'aucun autre corps de la
    /// tour ne pose.</para>
    /// </summary>
    private static void TickDriftingShard_00416920(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054FB88();                     // Ghidra: &DAT_0054fb88
            e[0x40] = 0;
            e[0x0a] = 7;
            e[0x0b] = 0xfb;

            *(int*)(e + 0x1c) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x14 + 0x3c) * 0x1000;

            int scaleDraw = CrtRand();
            *(short*)(e + 0x10) = 1;
            short scale = (short)((scaleDraw & 0x7ff) + 0xc00);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x50) = (CrtRand() & 3) - 2;
            *(int*)(e + 0x58) = (CrtRand() & 3) - 2;

            int rise = CrtRand();
            e[0x38] = 0x5a;
            e[0x39] = 0x5a;
            e[0x3a] = 0x5a;
            *(int*)(e + 0x54) = rise % 3 + 1;

            *(short*)(e + 0x12) = (short)(CrtRand() % 5 + 5);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50) * 0x1000;
        *(int*)(e + 0x20) += *(int*)(e + 0x54) * 0x1000;
        *(int*)(e + 0x24) += *(int*)(e + 0x58) * 0x1000;

        BrightenEntityTint_00404DE0(e, -10, -10, -10);                       // Ghidra: FUN_00404de0

        int frameDraw = CrtRand();
        (*(short*)(e + 0x12))--;
        e[0x40] = (byte)(frameDraw & 3);

        if (*(short*)(e + 0x12) < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0041b7c0</c> — <b>le panache a deux visages</b>, sous-zones 3, 7 et 9.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0041B7C0.asm</c>.
    ///
    /// <para>
    /// <b>Deux naissances dans un seul corps</b>, choisies par le <c>+0x14</c> que le lanceur a
    /// laisse. La variante <b>0</b> prend la planche <c>0x0054fe28</c>, une echelle a partir de
    /// <c>0x1000</c>, une duree de vie de 20 a 39 images, et <b>descend</b> de 20 a 39 unites ; la
    /// variante <b>1</b> prend <c>0x0054fe00</c>, une echelle a partir de <c>0xc00</c>, une duree de
    /// vie fixe de <b>14</b> images, et <b>monte</b> de 0 a 119 unites.
    /// </para>
    /// <para>
    /// La variante 0 ecrit ensuite son propre <c>+0x14</c> (20..39, le rayon de rotation), ce qui
    /// fait que le test de la variante 1 qui la suit ne peut plus etre vrai : <b>les deux branches
    /// s'excluent</b> bien qu'elles se suivent sans <c>else</c>.
    /// </para>
    /// <para>
    /// Ensuite les deux font la meme chose : la teinte perd 3 par canal et par image — <b>et si le
    /// canal rouge passe sous zero, les trois sont mis a zero d'un coup</b>, pas seulement le
    /// rouge —, la phase avance de <c>0x40</c>, la position tourne a <c>+0x14</c> autour de l'ancre,
    /// la hauteur monte de <c>0xa000</c>, l'image avance une image sur deux et boucle sur
    /// <b>sept</b>, et le panache meurt quand son compte <c>+0x5c</c> atteint zero.
    /// </para>
    /// <para>
    /// <b>Sept images sur une planche de cinq</b> pour la variante 1 : voir
    /// <see cref="s_Descriptors_0054FE00"/>, ou les deux plages sont declarees d'un seul tenant pour
    /// que le debordement lise ce que le binaire lit.
    /// </para>
    /// </summary>
    private static void TickTwoFacedPlume_0041B7C0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            if (*(short*)(e + 0x14) == 0)
            {
                *(byte**)(e + 0x3c) = Descriptors0054FE28();                 // Ghidra: &DAT_0054fe28
                e[0x40] = 0;
                e[0x0a] = 1;
                e[0x0b] = 0;

                short scale = (short)(CrtRand() % 0x200 + 0x1000);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);

                int life = CrtRand();
                e[0x38] = 0x60;
                e[0x39] = 0x60;
                e[0x3a] = 0x60;
                *(short*)(e + 0x14) = (short)(life % 0x1e + 0x1e);

                *(int*)(e + 0x5c) = CrtRand() % 0x14 + 0x14;
                *(int*)(e + 0x20) += -0x14000 - ((CrtRand() % 0x14) << 0xc);
            }

            if (*(short*)(e + 0x14) == 1)
            {
                *(byte**)(e + 0x3c) = Descriptors0054FE00();                 // Ghidra: &DAT_0054fe00
                e[0x40] = 0;
                e[0x0a] = 1;
                e[0x0b] = 0;

                short scale = (short)(CrtRand() % 0x200 + 0xc00);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);

                int life = CrtRand();
                e[0x38] = 0x60;
                e[0x39] = 0x60;
                e[0x3a] = 0x60;
                *(int*)(e + 0x5c) = 0x0e;
                *(short*)(e + 0x14) = (short)(life % 0x1e + 0x1e);

                *(int*)(e + 0x20) += (CrtRand() % 0x78) << 0xc;
            }

            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x16) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        // Ghidra: `movzx` sur les trois canaux, `sub …,3`, puis un SEUL `test ax,ax ; jge`.
        short r = (short)(e[0x38] - 3);
        short g = (short)(e[0x39] - 3);
        short b = (short)(e[0x3a] - 3);
        if (r < 0) { r = 0; g = 0; b = 0; }

        *(short*)(e + 0x12) += 0x40;
        e[0x38] = (byte)r;
        e[0x39] = (byte)g;
        e[0x3a] = (byte)b;

        if (*(short*)(e + 0x12) >= 0x1000) *(short*)(e + 0x12) -= 0x1000;

        short phase = *(short*)(e + 0x12);
        short radius = *(short*)(e + 0x14);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(phase) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(phase) * radius + *(int*)(e + 0x58);
        *(int*)(e + 0x20) += 0xa000;

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) == 2)
        {
            *(short*)(e + 0x16) = 0;
            byte next = (byte)(e[0x40] + 1);
            e[0x40] = next;
            if (next == 7) e[0x40] = 0;
        }

        int life2 = *(int*)(e + 0x5c) - 1;
        *(int*)(e + 0x5c) = life2;
        if (life2 <= 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00418af0 @ 0x00418af0 — <b>la bouffee qui part de biais et s'eteint</b>,
    /// sous-zones 5, 6, 8 et 10. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Elle nait dispersee de ±30 unites, jusqu'a 19 plus haut, avec une echelle de 800 a 1799, un
    /// <b>cap devie de ±400</b> autour de celui qu'elle a recu, une vitesse <c>+0x14</c> de 8 a 15 et
    /// une montee <c>+0x16</c> de 1 a 3. Ensuite elle avance au cap, monte, et <b>perd deux unites de
    /// vitesse par image</b> en s'assombrissant de 30 : elle disparait au bout de quatre a sept
    /// images.
    /// </para>
    /// <para>
    /// <b>Sa mort n'est pas un masque mais une affectation</b> : <c>*param_1 = 0x7fffffff</c> ecrase
    /// le mot d'etat entier au lieu d'en effacer le bit de vie. Tous les autres bits partent avec.
    /// Transcrit tel quel — c'est ce que le binaire ecrit.
    /// </para>
    /// </summary>
    private static void TickSlantedPuff_00418AF0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054FBF8();                     // Ghidra: &DAT_0054fbf8
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;

            short scale = (short)(CrtRand() % 1000 + 800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(short*)(e + 0x32) += (short)(CrtRand() % 800 - 400);
            *(short*)(e + 0x14) = (short)((CrtRand() & 7) + 8);
            *(short*)(e + 0x16) = (short)(CrtRand() % 3 + 1);

            *(int*)(e + 0x1c) += (CrtRand() % 0x3c - 0x1e) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x3c - 0x1e) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x14) * 0x1000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        short course = *(short*)(e + 0x32);
        short speed = *(short*)(e + 0x14);

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * speed;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * speed;
        *(int*)(e + 0x20) += *(short*)(e + 0x16) * 0x1000;

        *(short*)(e + 0x14) = (short)(speed - 2);
        BrightenEntityTint_00404DE0(e, -30, -30, -30);                       // Ghidra: FUN_00404de0

        // Ghidra: `mov dword [esi],7FFFFFFFh` — une AFFECTATION, pas un masque.
        if (*(short*)(e + 0x14) < 1) *(uint*)e = 0x7fffffff;
    }
}
