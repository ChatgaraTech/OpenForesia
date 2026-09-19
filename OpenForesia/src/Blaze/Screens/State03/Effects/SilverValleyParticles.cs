// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DE LA VALLEE D'ARGENT — zone 8, sous-zones 0 et 1.
//
// Ouvert le 2026-09-19 avec la zone, sur le patron de `LakeRuinsParticles.cs`. Quinze corps, tous
// des creneaux de debordement (0x0056b0c8, 0x0056b1c8, 0x0056b2c8). La vallee est enneigee, et ca se
// voit : la moitie de ces corps sont des flocons, des bouffees de poudreuse, des ondes de froid.
//
// Dumps : Legacy/Raw/Blaze/SilverValley/.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES PLANCHES DE LA ZONE
    //
    // Relevees par consommateur, chacune bornee par un changement de forme :
    //
    //     0x00550980   0x1f x 0x1f                     (borne basse ; celle des ruines souterraines)
    //     0x00550988   0x08 x 0x08, couche 0x10        la bulle, UN enregistrement
    //     0x005509c8   0x18 x 0x18 prises a y = 0xc8   les bouffees, quatre images
    //     0x005509e8   0x18 x 0x18 a (0x18, 0)         le flocon, UN enregistrement
    //     0x005509f0   0x08 x 0x08                     le grain, quatre images
    //     0x00550a70   0x2f x 0x0f, page 5             le cristal qui pulse, neuf images
    //     0x00550ad0   0x1f x 0x1f, page 5             l'etoile de givre, sept images
    //     0x00550b08   0x20 x 0x20                     (borne haute)
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00550988</c> — <b>la bulle de la vallee</b>, un seul
    /// enregistrement : 0x00550990 change de tuile. Voir <see cref="EntityHandler_0042FF50"/>.</summary>
    private static readonly byte[] s_Descriptors_00550988 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550988  <- 0x0042FF50
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005509c8</c> — <b>les bouffees de poudreuse</b>, quatre images
    /// que les trois corps de la famille parcourent de 0 a 3 avant de mourir a 4. Bornee en bas
    /// par une tuile de la couche <c>0x01</c> a <c>y = 0x30</c>, en haut par le flocon.</summary>
    private static readonly byte[] s_Descriptors_005509C8 =
    [
        0x30, 0xc8, 0x18, 0x18, 0x01, 0x01, 0x00, 0x00,   // 0x005509c8  <- 0x004301E0, 0x00430040, 0x00430360
        0x48, 0xc8, 0x18, 0x18, 0x01, 0x01, 0x00, 0x00,   // 0x005509d0
        0x60, 0xc8, 0x18, 0x18, 0x01, 0x01, 0x00, 0x00,   // 0x005509d8
        0x78, 0xc8, 0x18, 0x18, 0x01, 0x01, 0x00, 0x00,   // 0x005509e0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005509e8</c> — <b>le flocon</b>, un seul enregistrement,
    /// puis <b>le grain</b> a <c>+8</c> (<c>&amp;DAT_005509f0</c>), quatre images. Releves d'un
    /// seul tenant : ils se touchent.</summary>
    private static readonly byte[] s_Descriptors_005509E8 =
    [
        0x18, 0x00, 0x18, 0x18, 0x01, 0x03, 0x10, 0x00,   // 0x005509e8  <- 0x0042FDB0, le flocon
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005509f0  <- 0x0042FCC0, le grain
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005509f8
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550a00
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550a08
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550a70</c> — <b>le cristal qui pulse</b>, neuf images
    /// <c>0x2f x 0x0f</c> de la page 5, que <see cref="EntityHandler_0042FA10"/> parcourt en
    /// va-et-vient de 0 a 8. Bornee en bas par les tuiles <c>0x0f x 0x0f</c> prises a
    /// <c>y = 0xf0</c>, en haut par une <c>0x1f x 0x1f</c>.</summary>
    private static readonly byte[] s_Descriptors_00550A70 =
    [
        0x00, 0x90, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550a70  <- 0x0042FA10
        0x30, 0x90, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550a78
        0x60, 0x90, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550a80
        0x90, 0x90, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550a88
        0xc0, 0x90, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550a90
        0x00, 0xa0, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550a98
        0x30, 0xa0, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550aa0
        0x60, 0xa0, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550aa8
        0x90, 0xa0, 0x2f, 0x0f, 0x01, 0x00, 0x05, 0x00,   // 0x00550ab0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550ad0</c> — <b>l'etoile de givre</b>, sept images
    /// <c>0x1f x 0x1f</c> de la page 5 au pas de <c>0x20</c> en X, que les deux corps de la
    /// famille parcourent de 0 a 6 et quittent a 7. Bornee en haut par <c>0x00550b08</c>, une
    /// tuile <c>0x20 x 0x20</c>.</summary>
    private static readonly byte[] s_Descriptors_00550AD0 =
    [
        0x00, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x05, 0x00,   // 0x00550ad0  <- 0x0042F970, 0x0042F860
        0x20, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x05, 0x00,   // 0x00550ad8
        0x40, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x05, 0x00,   // 0x00550ae0
        0x60, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x05, 0x00,   // 0x00550ae8
        0x80, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x05, 0x00,   // 0x00550af0
        0xa0, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x05, 0x00,   // 0x00550af8
        0xc0, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x05, 0x00,   // 0x00550b00
    ];

    private static byte* s_Descriptors00550988Storage;
    private static byte* s_Descriptors005509C8Storage;
    private static byte* s_Descriptors005509E8Storage;
    private static byte* s_Descriptors00550A70Storage;
    private static byte* s_Descriptors00550AD0Storage;

    private static byte* Descriptors00550988() => NativeCopy(s_Descriptors_00550988, ref s_Descriptors00550988Storage);
    private static byte* Descriptors005509C8() => NativeCopy(s_Descriptors_005509C8, ref s_Descriptors005509C8Storage);
    private static byte* Descriptors005509E8() => NativeCopy(s_Descriptors_005509E8, ref s_Descriptors005509E8Storage);
    private static byte* Descriptors00550A70() => NativeCopy(s_Descriptors_00550A70, ref s_Descriptors00550A70Storage);
    private static byte* Descriptors00550AD0() => NativeCopy(s_Descriptors_00550AD0, ref s_Descriptors00550AD0Storage);

    // =====================================================================
    // LA BULLE ET LE GRAIN
    // =====================================================================

    /// <summary>Ghidra: FUN_0042ff50 @ 0x0042ff50 — la bulle de la vallee.
    /// <see cref="EntityHandler_004079E0"/> a la planche pres : <c>0x00550988</c>. Comme sa
    /// mere, elle n'ecrit jamais <c>+0x40</c> ; son creneau de <c>0x0056b2c8</c> n'est atteint
    /// que par la piscine <c>0x005f2d40</c>, que <see cref="SpawnStatusEffectEntity_0047AC40"/>
    /// remplit en posant <c>+0x40 = 0</c>.</summary>
    private static void EntityHandler_0042FF50(byte* e)
        => TickRisingBubble(e, Descriptors00550988());           // Ghidra: &DAT_00550988

    /// <summary>Ghidra: <c>0x0042fcc0</c> — le grain de la vallee. Absent de <c>Blaze.c</c> ;
    /// <see cref="EntityHandler_004070F0"/> a la planche pres : <c>0x005509f0</c>.</summary>
    private static void EntityHandler_0042FCC0(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors005509E8() + 8); // Ghidra: &DAT_005509f0

    // =====================================================================
    // LE FLOCON
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0042fdb0 @ 0x0042fdb0 — creneau <b>51</b> du bloc de <c>0x0056b2c8</c>, donc le
    /// sous-type <c>0xf3</c>. Decompilee dans <c>Blaze.c</c>, 118 instructions.
    ///
    /// <para>
    /// <b>DEUX flocons pour une entite.</b> A chaque appel, avant toute chose, le corps <b>echange
    /// sa position</b> (<c>+0x1c</c>..<c>+0x24</c>) avec celle qu'il garde en <c>+0x44</c>..<c>+0x4c</c> :
    /// il n'avance donc qu'un flocon par image, et l'autre attend son tour. A la naissance, les
    /// deux sont semes autour de la camera (<c>0x005de068</c>) — ±750 unites a plat, le premier de
    /// 400 a 1599 au-dessus, le second de 1200 a 1599.
    /// </para>
    /// <para>
    /// Ensuite le flocon descend de <c>+0x12</c> (10 a 39) par image et tremble de ±5 <b>en unites
    /// fines</b> — pas decalees de douze bits, contrairement a la descente. Il s'eteint 400 unites
    /// sous la camera.
    /// </para>
    /// <para>
    /// <b>Le decor 0x00466C30 le fait naitre avec une planche qui pointe dans sa propre pile</b>
    /// (un <c>lea edx,[esp+8]</c> sur un local jamais ecrit, voir
    /// <c>Handlers/SilverValley.cs</c>) ; ce corps ecrase <c>+0x3c</c> a la naissance, avant tout
    /// dessin, et le pointeur ne sert jamais.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042FDB0(byte* e)
    {
        int x = *(int*)(e + 0x1c);
        int y = *(int*)(e + 0x20);
        int z = *(int*)(e + 0x24);
        *(int*)(e + 0x1c) = *(int*)(e + 0x44);
        *(int*)(e + 0x44) = x;
        *(int*)(e + 0x20) = *(int*)(e + 0x48);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c);
        *(int*)(e + 0x48) = y;
        *(int*)(e + 0x4c) = z;

        int* camera = Mem.Camera(0x005de068);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors005509E8();          // Ghidra: &DAT_005509e8

            *(int*)(e + 0x1c) = (CrtRand() % 0x5dc - 0x2ee) * 0x1000 + camera[0];
            *(int*)(e + 0x20) = (CrtRand() % 0x4b0 + 400) * 0x1000 + camera[1];
            *(int*)(e + 0x24) = (CrtRand() % 0x5dc - 0x2ee) * 0x1000 + camera[2];
            *(int*)(e + 0x44) = (CrtRand() % 0x5dc - 0x2ee) * 0x1000 + camera[0];
            *(int*)(e + 0x48) = (CrtRand() % 400 + 0x4b0) * 0x1000 + camera[1];

            int draw = CrtRand();
            *(short*)(e + 0x28) = 0x400;
            *(short*)(e + 0x2a) = 0x400;
            *(int*)(e + 0x4c) = (draw % 0x5dc - 0x2ee) * 0x1000 + camera[2];

            int fall = CrtRand();
            *(short*)(e + 0x14) = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = (short)(fall % 0x1e + 10);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x1000;
        *(int*)(e + 0x1c) += CrtRand() % 10 - 5;
        *(int*)(e + 0x24) += CrtRand() % 10 - 5;

        if (*(int*)(e + 0x20) < camera[1] - 0x190000) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // L'ETOILE DE GIVRE — deux corps, une planche de sept images
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0042f970</c> — creneau 3 du bloc de <c>0x0056b2c8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 51 instructions.
    ///
    /// <para>
    /// <b>L'etoile lente, sur place.</b> Elle nait noire, en echelle <c>0x800</c>, role 1, et
    /// avance d'une image toutes les quatre : elle s'eclaire de 6 par canal et par image tant que
    /// son image vaut 0 a 2, s'assombrit de 10 a partir de 3, et s'eteint a la septieme.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042F970(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) BirthFrostStar(e);
        if (*(short*)(e + 0x10) != 1) return;

        if (e[0x40] <= 2) BrightenEntityTint_00404DE0(e, 6, 6, 6);        // Ghidra: FUN_00404de0
        if (e[0x40] >= 3) BrightenEntityTint_00404DE0(e, -10, -10, -10);

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) < 4) return;

        *(short*)(e + 0x12) = 0;
        e[0x40]++;
        if (e[0x40] == 7) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0042f860</c> — creneau 2 du bloc de <c>0x0056b2c8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 88 instructions.
    ///
    /// <para>
    /// <b>L'etoile vive, qui eclate.</b> La meme etoile que <see cref="EntityHandler_0042F970"/>,
    /// semee a ±100 unites a plat et 200 a 249 au-dessus de sa naissance, qui change d'image a
    /// CHAQUE appel (le seuil de l'horloge est 1), s'eclaire de <c>0x20</c> puis s'assombrit de
    /// <c>0x14</c>. A la septieme image elle fait naitre l'effet <c>0xc1</c> a sa place, sans
    /// planche, et s'eteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042F860(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            BirthFrostStar(e);

            *(int*)(e + 0x1c) += (CrtRand() % 200 - 100) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 200 - 100) << 0xc;
            *(int*)(e + 0x20) += (CrtRand() % 0x32 + 200) << 0xc;
        }

        if (*(short*)(e + 0x10) != 1) return;

        if (e[0x40] <= 2) BrightenEntityTint_00404DE0(e, 0x20, 0x20, 0x20);   // Ghidra: FUN_00404de0
        if (e[0x40] >= 3) BrightenEntityTint_00404DE0(e, -0x14, -0x14, -0x14);

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) < 1) return;

        *(short*)(e + 0x12) = 0;
        e[0x40]++;
        if (e[0x40] != 7) return;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc1, 0, -1);   // Ghidra: FUN_0047ac40
        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: la naissance commune aux deux etoiles, recopiee en ligne — planche
    /// <c>0x00550ad0</c> a l'image 0, role 1, echelle <c>0x800</c>, teinte noire.</summary>
    private static void BirthFrostStar(byte* e)
    {
        *(short*)(e + 0x10) = 1;
        *(byte**)(e + 0x3c) = Descriptors00550AD0();             // Ghidra: &DAT_00550ad0
        e[0x40] = 0;
        e[0x0a] = 1;
        e[0x0b] = 0;
        *(short*)(e + 0x28) = 0x800;
        *(short*)(e + 0x2a) = 0x800;
        *(short*)(e + 0x12) = 0;
        e[0x38] = 0;
        e[0x39] = 0;
        e[0x3a] = 0;
    }

    // =====================================================================
    // LE CRISTAL QUI PULSE
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0042fa10 @ 0x0042fa10 — creneau 1 du bloc de <c>0x0056b2c8</c>. Decompilee dans
    /// <c>Blaze.c</c>, 100 instructions.
    ///
    /// <para>
    /// <b>Un cristal qui monte en derivant, et dont l'image fait l'aller-retour.</b> Il nait a
    /// l'image 5 de <see cref="s_Descriptors_00550A70"/>, role <c>0x0e</c> ou <c>0x0f</c> au hasard,
    /// teinte <c>0x80</c>, et tire trois vitesses : ±3 a plat, 3 a 8 vers le haut. Toutes les deux
    /// images son image avance de <c>+0x12</c> (1 ou -1), et <c>+0x12</c> change de signe a 0 et a
    /// 8.
    /// </para>
    /// <para>
    /// Il vit de 20 a 49 images — <b>le compte <c>+0x14</c> decroit a chaque appel, naissance
    /// comprise</b> —, puis fait naitre l'effet <c>0xc3</c> a sa place et s'eteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042FA10(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00550A70();          // Ghidra: &DAT_00550a70
            e[0x40] = 5;

            int draw = CrtRand();
            e[0x0b] = 0;
            e[0x0a] = (byte)(SignedMask(draw, 1) + 0x0e);
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;

            *(int*)(e + 0x50) = CrtRand() % 6 - 3;
            *(int*)(e + 0x58) = CrtRand() % 6 - 3;
            int rise = CrtRand();
            *(short*)(e + 0x12) = 1;
            *(int*)(e + 0x54) = rise % 6 + 3;

            int life = CrtRand();
            *(short*)(e + 0x16) = 0;
            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;
            *(short*)(e + 0x14) = (short)(life % 0x1e + 0x14);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) += *(int*)(e + 0x50) * 0x1000;
            *(int*)(e + 0x24) += *(int*)(e + 0x58) * 0x1000;
            (*(short*)(e + 0x16))++;
            *(int*)(e + 0x20) += *(int*)(e + 0x54) * -0x1000;

            if (*(short*)(e + 0x16) > 1)
            {
                sbyte frame = (sbyte)(e[0x40] + (byte)*(short*)(e + 0x12));
                *(short*)(e + 0x16) = 0;
                e[0x40] = (byte)frame;
                if (frame == 8 || frame == 0) *(short*)(e + 0x12) = (short)-*(short*)(e + 0x12);
            }

            if (*(short*)(e + 0x14) < 1)
            {
                *(uint*)e &= 0x7fffffff;
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc3, 0, -1);   // Ghidra: FUN_0047ac40
            }
        }

        (*(short*)(e + 0x14))--;
    }

    // =====================================================================
    // LES BOUFFEES DE POUDREUSE — trois corps, une planche de quatre images
    // =====================================================================

    /// <summary>Ghidra: <c>0x004301e0</c> — creneau 50 du bloc de <c>0x0056b2c8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 117 instructions. La petite bouffee — voir
    /// <see cref="TickPowderPuff"/>.</summary>
    private static void EntityHandler_004301E0(byte* e)
        => TickPowderPuff(e, 0x5dc, 0, 5, 0, 5, 0xd000);

    /// <summary>Ghidra: <c>0x00430040</c> — creneau 48 du bloc de <c>0x0056b2c8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 123 instructions. La grosse bouffee, jetee a l'arriere — voir
    /// <see cref="TickPowderPuff"/>.</summary>
    private static void EntityHandler_00430040(byte* e)
        => TickPowderPuff(e, 0x800, 0x800, 0x28, 0xa000, 0x28, 0x14000);

    /// <summary>
    /// Le corps commun de <see cref="EntityHandler_004301E0"/> et
    /// <see cref="EntityHandler_00430040"/>, deux fonctions du binaire identiques a six constantes
    /// pres.
    ///
    /// <para>
    /// <b>A la naissance</b> : deux echelles tirees de <paramref name="scaleBase"/> a
    /// <c>+599</c> ; un cap disperse d'un quart de tour de part et d'autre ; la bouffee se pose a
    /// <paramref name="reach"/> dans ce cap, retourne de <paramref name="courseOffset"/> (un
    /// demi-tour pour la grosse), avec ±10 unites de jeu ; elle descend de
    /// <paramref name="drop"/> plus 0 a 19 unites ; son rayon <c>+0x14</c> part de
    /// <paramref name="radius"/>, et <c>+0x12</c> — 8 a 12 — est ce qu'il perd par image.
    /// </para>
    /// <para>
    /// <b>Ensuite</b>, elle tourne autour de son point de naissance a ce rayon qui fond (et passe
    /// en negatif : la bouffee traverse son centre), monte de <paramref name="rise"/> par image, et
    /// son image est son compte d'images : elle s'eteint a la quatrieme.
    /// </para>
    /// <para>
    /// <b>La petite bouffee pose son rayon a 5 par accident apparent</b> : le binaire ecrit
    /// <c>mov [esi+14h],cx</c> avec <c>ecx</c> encore egal au diviseur du tirage qui precede
    /// (<c>mov ecx,5</c>). La grosse, elle, pose <c>0x28</c> en immediat. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickPowderPuff(
        byte* e, int scaleBase, int courseOffset, int reach, int drop, short radius, int rise)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors005509C8();          // Ghidra: &DAT_005509c8

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x258 + scaleBase);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x258 + scaleBase);
            *(short*)(e + 0x32) += (short)(SignedMask(CrtRand(), 0x7ff) - 0x400);

            int sinReach = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) + courseOffset) * reach;   // Ghidra: FUN_00545a1c
            *(int*)(e + 0x1c) += ((CrtRand() % 0x14 - 10) << 0xc) + sinReach;
            int cosReach = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) + courseOffset) * reach;         // Ghidra: FUN_00545a16
            *(int*)(e + 0x24) += ((CrtRand() % 0x14 - 10) << 0xc) + cosReach;

            int sink = CrtRand();
            e[0x0a] = 6;
            e[0x0b] = 0;
            *(int*)(e + 0x20) += -drop - ((sink % 0x14) << 0xc);

            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            int shrink = CrtRand();
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = radius;
            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)(shrink % 5 + 8);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short r = *(short*)(e + 0x14);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) + courseOffset) * r + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) + courseOffset) * r + *(int*)(e + 0x58);
        *(int*)(e + 0x20) += rise;

        (*(short*)(e + 0x16))++;
        *(short*)(e + 0x14) = (short)(r - *(short*)(e + 0x12));
        e[0x40] = (byte)*(short*)(e + 0x16);

        if (e[0x40] == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00430360</c> — creneau 49 du bloc de <c>0x0056b2c8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 460 instructions.
    ///
    /// <para>
    /// <b>La poudreuse sur le contour d'une etoile.</b> A la naissance, le corps choisit un cote
    /// d'une etoile a cinq branches centree sur l'entite, orientee par son cap, de rayon 200 :
    /// ses sommets sont a <c>±0x2bc</c> et <c>±0x514</c> du cap, chacun a ±10 unites pres. Puis il
    /// pose la bouffee a un cran tire sur ce cote, compte depuis le premier sommet :
    /// </para>
    /// <list type="table">
    ///   <item><term>cote 0 ou 4</term><description><c>+0x2bc</c> vers <c>-0x2bc</c>, huit
    ///     crans</description></item>
    ///   <item><term>cote 1 (et 6, 7)</term><description><c>-0x2bc</c> vers <c>-0x514</c>, quatre
    ///     crans</description></item>
    ///   <item><term>cote 2 ou 5</term><description><c>-0x514</c> vers <c>+0x514</c>, huit
    ///     crans</description></item>
    ///   <item><term>cote 3</term><description><c>+0x514</c> vers <c>+0x2bc</c>, quatre
    ///     crans</description></item>
    /// </list>
    /// <para>
    /// Le cote est tire sur six — sur HUIT pour la variante <c>+0x12 = 2</c>, dont les cotes 6 et 7
    /// retombent sur le cote 1. Deux cotes sur six valent donc le meme segment, ce qui charge la
    /// base de l'etoile.
    /// </para>
    /// <para>
    /// La variante <c>+0x12</c> regle aussi l'echelle (0 : telle quelle, 1 : <c>+0x400</c>, 2 :
    /// <c>-0x320</c>), est rangee en <c>+0x9a</c>, et double la montee pour la variante 0. La
    /// bouffee nait <b>cachee</b> (<c>0x4000</c>) : les variantes 0 et 1 se montrent passe la
    /// troisieme image ; toutes passent a l'etat 2 passe la huitieme ou sur un tirage sur dix.
    /// En etat 2, elle monte de <c>+0x12</c> par image, tremble de ±10 a plat, et avance d'une image
    /// toutes les deux ; elle s'eteint a la quatrieme.
    /// </para>
    /// </summary>
    private static void EntityHandler_00430360(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005509C8();          // Ghidra: &DAT_005509c8
            *(short*)(e + 0x28) = (short)(CrtRand() % 0x258 + 0x800);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x258 + 0x800);

            short side = *(short*)(e + 0x12) == 2
                ? (short)SignedMask(CrtRand(), 7)
                : (short)(CrtRand() % 6);

            short heading = *(short*)(e + 0x32);

            // Ghidra: les registres ne sont ecrits que par la branche qui s'applique ; a defaut,
            // le binaire y laisse le numero de cote (0x004304b6..0x004304c6). Aucun cote de 0 a 7
            // ne tombe dans ce defaut.
            int fromX = side, fromZ = side, toX = side, toZ = side, notch = side, notches = side;

            if (side == 0 || side == 4)
            {
                StarVertex(heading + 0x2bc, out fromX, out fromZ);
                StarVertex(heading - 0x2bc, out toX, out toZ);
                notch = SignedMask(CrtRand(), 7);
                notches = 8;
            }

            if (side == 1 || (*(short*)(e + 0x12) == 2 && side >= 6))
            {
                StarVertex(heading - 0x2bc, out fromX, out fromZ);
                StarVertex(heading - 0x514, out toX, out toZ);
                notch = SignedMask(CrtRand(), 3);
                notches = 4;
            }

            if (side == 2 || side == 5)
            {
                StarVertex(heading - 0x514, out fromX, out fromZ);
                StarVertex(heading + 0x514, out toX, out toZ);
                notch = SignedMask(CrtRand(), 7);
                notches = 8;
            }

            if (side == 3)
            {
                StarVertex(heading + 0x514, out fromX, out fromZ);
                StarVertex(heading + 0x2bc, out toX, out toZ);
                notch = SignedMask(CrtRand(), 3);
                notches = 4;
            }

            int step = (short)notch + 1;
            int count = (short)notches;

            int ax = fromX >> 0xc, az = fromZ >> 0xc;
            *(int*)(e + 0x1c) += (ax - (ax - (toX >> 0xc)) / count * step) << 0xc;
            *(int*)(e + 0x24) += (az - (az - (toZ >> 0xc)) / count * step) << 0xc;

            int sink = CrtRand();
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            e[0x0a] = 6;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) += -0xa000 - ((sink % 0x14) << 0xc);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            short variant = *(short*)(e + 0x12);
            if (variant == 0) *(short*)(e + 0x9a) = 0;
            if (variant == 1)
            {
                *(short*)(e + 0x9a) = 1;
                *(short*)(e + 0x28) += 0x400;
                *(short*)(e + 0x2a) += 0x400;
            }
            if (variant == 2)
            {
                *(short*)(e + 0x9a) = 2;
                *(short*)(e + 0x28) += -0x320;
                *(short*)(e + 0x2a) += -0x320;
            }

            int factor = *(short*)(e + 0x9a) == 1 || *(short*)(e + 0x9a) == 2 ? 1 : 2;

            int climb = CrtRand();
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)((climb % 5 + 8) * factor);
        }

        // ---------------------------------------------------------------- 1 : cachee
        if (*(short*)(e + 0x10) == 1)
        {
            uint flags = *(uint*)e | 0x4000;
            (*(short*)(e + 0x14))++;
            *(uint*)e = flags;

            if (*(short*)(e + 0x14) > 3 && (*(short*)(e + 0x9a) == 0 || *(short*)(e + 0x9a) == 1))
                *(uint*)e = flags & 0xffffbfff;

            int roll = CrtRand() % 10;
            if (*(short*)(e + 0x14) > 8 || (short)roll == 0)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0xffffbfff;
                *(short*)(e + 0x14) = 0;
            }
        }

        // ---------------------------------------------------------------- 2 : elle monte
        if (*(short*)(e + 0x10) != 2) return;

        *(int*)(e + 0x20) += *(short*)(e + 0x12) << 0xc;
        *(int*)(e + 0x1c) += (CrtRand() % 0x14 - 10) << 0xc;
        *(int*)(e + 0x24) += (CrtRand() % 0x14 - 10) << 0xc;

        (*(short*)(e + 0x16))++;
        byte frame = (byte)(*(short*)(e + 0x16) >> 1);
        e[0x40] = frame;
        if (frame == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: un sommet de l'etoile de <see cref="EntityHandler_00430360"/>, recopie
    /// huit fois en ligne — 200 unites dans la direction <paramref name="angle"/>
    /// (<c>sinus * 25 * 8</c> : deux <c>lea *5</c> puis un <c>lea *8</c>), a ±10 unites
    /// pres.</summary>
    private static void StarVertex(int angle, out int x, out int z)
    {
        int sin = (int)Winsys3d.GetSinTableEntry(angle) * 25;     // Ghidra: FUN_00545a1c
        x = ((CrtRand() % 0x14 - 10) << 0xc) + sin * 8;
        int cos = Winsys3d.GetCosTableEntry(angle) * 25;          // Ghidra: FUN_00545a16
        z = ((CrtRand() % 0x14 - 10) << 0xc) + cos * 8;
    }

    // =====================================================================
    // LES DEUX ONDES — des modeles qui enflent, sans planche
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0042fc10 @ 0x0042fc10 — table <c>0x0056b1c8</c>. Decompilee dans
    /// <c>Blaze.c</c>, 47 instructions. Un jumeau non ecrit ailleurs.
    ///
    /// <para>
    /// <b>L'onde blanche.</b> Le premier modele propre a la zone
    /// (<see cref="s_Unknown_005edec4"/>), role 3, en echelle 3000 sur les trois axes et teinte
    /// pleine. Chaque image elle grandit de <c>+0x12</c> — 900 d'abord, puis 40 de plus par image,
    /// plafonne a 1000 — et palit de 12 par canal ; a 30000 elle s'eteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042FC10(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[4] = s_Unknown_005edec4;                             // Ghidra: DAT_005edec4
            *(short*)(e + 0x28) = 3000;
            *(short*)(e + 0x2a) = 3000;
            *(short*)(e + 0x2c) = 3000;
            e[0x0a] = 3;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 900;
            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, -12, -12, -12);        // Ghidra: FUN_00404de0
            GrowColdWave(e, 0x28);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0042fb50 @ 0x0042fb50 — table <c>0x0056b1c8</c>. Decompilee dans
    /// <c>Blaze.c</c>, 57 instructions.
    ///
    /// <para>
    /// <b>L'onde teintee.</b> Le second modele de la zone (<c>DAT_005edec4 + 1</c>), role 5, 100
    /// unites plus haut, de zero, teinte <c>(0xfa, 0xbe, 0xbe)</c>. A la naissance elle fait
    /// naitre l'entite d'incantation <c>0xc2</c> — qui herite d'un <c>+0x12</c> a zero —, puis
    /// pose son propre pas a 700. Elle grandit de 20 de plus par image, palit de
    /// <c>(8, 12, 12)</c>, et s'eteint a 30000.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042FB50(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[4] = (byte)(s_Unknown_005edec4 + 1);                 // Ghidra: DAT_005edec4 + 1
            e[0x39] = 0xbe;
            e[0x3a] = 0xbe;
            e[0x0a] = 5;
            e[0x0b] = 0;
            e[0x38] = 0xfa;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(int*)(e + 0x20) += 0x64000;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);              // Ghidra: FUN_0047aae0
            *(short*)(e + 0x12) = 700;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, -8, -12, -12);         // Ghidra: FUN_00404de0
            GrowColdWave(e, 0x14);
        }
    }

    /// <summary>Ghidra: la croissance commune aux deux ondes, recopiee en ligne — les trois
    /// echelles grandissent de <c>+0x12</c>, <c>+0x12</c> de <paramref name="acceleration"/>
    /// plafonne a 1000, et a 30000 l'onde se fige et s'eteint.</summary>
    private static void GrowColdWave(byte* e, short acceleration)
    {
        short step = *(short*)(e + 0x12);
        *(short*)(e + 0x28) += step;
        *(short*)(e + 0x2a) += step;
        *(short*)(e + 0x2c) += step;

        *(short*)(e + 0x12) = (short)(step + acceleration);
        if ((short)(step + acceleration) > 999) *(short*)(e + 0x12) = 1000;

        if (*(short*)(e + 0x28) > 29999)
        {
            *(short*)(e + 0x28) = 30000;
            *(short*)(e + 0x2a) = 30000;
            *(short*)(e + 0x2c) = 30000;
            *(uint*)e &= 0x7fffffff;
        }
    }

    // =====================================================================
    // LE SOUFFLE ET LE JET
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0042f2a0 @ 0x0042f2a0 — table <c>0x0056b0c8</c>. Decompilee dans
    /// <c>Blaze.c</c>, 77 instructions.
    ///
    /// <para>
    /// <b>Le souffle qui crache.</b> Il retient sa place et son cap a la naissance et y revient a
    /// chaque image ; de la, il se tourne au hasard de ±350 et monte de 150 a 299 unites, et une
    /// image sur deux fait naitre l'entite d'incantation <c>0xc4</c> — vingt images en tout. Deux
    /// sons <c>0x23</c> de portee : un a la naissance, puis un toutes les deux images. Son
    /// porteur <c>+0x74</c> est recopie de <c>+0x6c</c> a chaque appel.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042F2A0(byte* e)
    {
        *(int*)(e + 0x74) = *(int*)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x14;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x48) = *(short*)(e + 0x32);
            TryPlaySoundNearEntity((uint*)e, 0x23, 0xaa8, 0x1500, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) > 1)
        {
            *(short*)(e + 0x98) = 0;
            TryPlaySoundNearEntity((uint*)e, 0x23, 0x1550, 0x1800, 0, 0);
        }

        (*(short*)(e + 0x12))--;
        *(int*)(e + 0x1c) = *(int*)(e + 0x50);
        *(int*)(e + 0x20) = *(int*)(e + 0x54);
        *(int*)(e + 0x24) = *(int*)(e + 0x58);
        *(short*)(e + 0x32) = (short)*(int*)(e + 0x48);
        *(short*)(e + 0x32) += (short)(CrtRand() % 700 - 0x15e);
        *(int*)(e + 0x20) += (CrtRand() % 0x96 + 0x96) * 0x1000;

        if ((e[0x12] & 1) != 0) SpawnCastEntity_0047AAE0(e, 0xc4, 0, -1);   // Ghidra: FUN_0047aae0
        if (*(short*)(e + 0x12) < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0042f3b0</c> — table <c>0x0056b1c8</c>. <b>Absent de <c>Blaze.c</c></b> ;
    /// 188 instructions.
    ///
    /// <para>
    /// <b>Le jet de glace.</b> Le modele <c>0x2a</c>, role 1, en echelles tirees de 7000 a 12999,
    /// incline de ±400, teinte <c>(0xbe, -0x42 + 0..39, 0xbe)</c> — l'octet vert est tire puis
    /// DECALE de <c>-0x42</c>, en arithmetique d'octet. Il file de 40 unites par image dans son cap
    /// pendant trente et une images, et sa teinte perd un huitieme (arrondi vers le haut) par
    /// canal et par image.
    /// </para>
    /// <para>
    /// Tant que le rouge depasse <c>0x14</c>, il frappe <b>chaque membre debout</b> (sans
    /// <c>0x50000000</c>, sans verrou <c>+0x96</c>, <c>+0x12</c> nul) a moins de 250 unites en trois
    /// dimensions (le membre compte 60 unites plus haut) — au nom de son porteur <c>+0x74</c>, a
    /// <b>trois quarts de la force</b> du modele de celui-ci, verrou de <c>0x10</c>. Sauf si le
    /// membre <b>fait face</b> au jet (<see cref="IsGuardingToward_004375A0"/>) : alors le jet
    /// meurt sur lui et la boucle continue avec le suivant.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042F3B0(byte* e)
    {
        byte* host = *(byte**)(e + 0x74);
        if (host == null) host = Res.s_EntityList_005de558;

        if (*(short*)(e + 0x10) == 0)
        {
            e[4] = 0x2a;
            e[0x0a] = 1;
            e[0x0b] = 1;
            e[0x38] = 0xbe;
            int green = CrtRand();
            e[0x3a] = 0xbe;
            *(short*)(e + 0x10) = 1;
            e[0x39] = (byte)((byte)(green % 0x28) - 0x42);

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x1770 + 0x1b58);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x1770 + 0x1b58);
            int depth = CrtRand();
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x2c) = (short)(depth % 0x1770 + 0x1b58);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            int tilt = CrtRand();
            *(short*)(e + 0x12) = 0x1e;
            *(short*)(e + 0x34) = (short)(tilt % 800 - 400);
        }

        if (*(short*)(e + 0x10) != 1) return;

        // Ghidra: `neg ; sar 3 ; add dl,al` — un huitieme arrondi vers le haut, par canal.
        e[0x38] = (byte)(e[0x38] + (-e[0x38] >> 3));
        e[0x39] = (byte)(e[0x39] + (-e[0x39] >> 3));
        e[0x3a] = (byte)(e[0x3a] + (-e[0x3a] >> 3));

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 40;   // Ghidra: FUN_00545a1c
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * 40;        // Ghidra: FUN_00545a16

        if (e[0x38] > 0x14)
        {
            byte* member = Inp.s_SlotFlags_005e93c0;
            for (int i = 4; i != 0; i--, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
                if (*(short*)(member + 0x12) != 0) continue;

                int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dy = (*(int*)(member + 0x20) - *(int*)(e + 0x20) + 0x3c000) >> 0xc;
                int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));   // Ghidra: FUN_00545a10

                if (*(short*)(member + 0x96) != 0) continue;
                if (distance >= 0xfa) continue;

                if (IsGuardingToward_004375A0(member, *(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24)) != 0)
                {
                    *(uint*)e &= 0x7fffffff;
                    continue;
                }

                ResolveEntityBlocks(host, out _, out _, out byte* model);   // Ghidra: FUN_00538980
                short strength = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(strength - (strength >> 2));
                ResolveMeleeHit_00539B80((uint*)host, (uint*)member);        // Ghidra: FUN_00539b80
                *(short*)(model + 0x30) = strength;

                *(ushort*)(member + 0x96) = 0x10;
                *(uint*)member |= 0x40;
            }
        }

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < 0) *(uint*)e &= 0x7fffffff;
    }
}
