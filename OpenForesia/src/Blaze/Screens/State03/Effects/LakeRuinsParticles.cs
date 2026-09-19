// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DES RUINES DANS LE LAC — zone 9, sous-zones 0 et 1.
//
// Ouvert le 2026-09-19 avec la zone, sur le patron de `UndergroundRuinsParticles.cs`. Neuf corps,
// tous des creneaux de debordement :
//
//     0x00430F10   0x0056b2c8   la bulle de la mine, planche 0x00550b88
//     0x00430E20   0x0056b2c8   le grain de la mine, planche 0x00550b90
//     0x00431000   0x0056b2c8   les bulles qui attendent, puis tremblent (a cote d'un flanc)
//     0x00431170   0x0056b2c8   les memes, dispersees et qui montent
//     0x00430C40   0x0056b2c8   la gerbe d'eau, grosse
//     0x004312E0   0x0056b2c8   la gerbe d'eau, fine
//     0x00484560   0x0056b1c8   LA VAGUE : elle eclabousse, puis enfle et frappe
//     0x004844A0   0x0056b0c8   la couronne de sept vagues
//
// Dumps : Legacy/Raw/Blaze/LakeRuins/.
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
    //     0x00550b18   0x20 x 0x20, couche 0x10        (borne basse)
    //     0x00550b20   0x18 x 0x18, octet 4 = 0        les bulles, quatre images
    //     0x00550b40   0x18 x 0x18, octet 4 = 1        (borne haute)
    //     0x00550b60   0x10 x 0x10, octet 5 = 3        la gerbe fine
    //     0x00550b68   0x10 x 0x10, octet 5 = 2        la gerbe grosse
    //     0x00550b70   0x18 x 0x18, octet 7 = 9        (borne haute)
    //     0x00550b88   0x08 x 0x08                     la bulle, puis les quatre images du grain
    //     0x00550bb0   0x1f x 0x1f, couche 0x18        (borne haute)
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550b20</c> — <b>les bulles qui attendent</b>, quatre images que
    /// <see cref="EntityHandler_00431000"/> et <see cref="EntityHandler_00431170"/> parcourent de 0
    /// a 3 avant de mourir a 4. Bornee en bas par une tuile <c>0x20 x 0x20</c>, en haut par le meme
    /// dessin dont l'octet 4 passe a 1.
    /// </summary>
    private static readonly byte[] s_Descriptors_00550B20 =
    [
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550b20  <- 0x00431000, 0x00431170
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550b28
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550b30
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550b38
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550b60</c> — <b>les deux gerbes d'eau</b>, un enregistrement chacune :
    /// la fine (<see cref="EntityHandler_004312E0"/>, <c>+0</c>) et la grosse
    /// (<see cref="EntityHandler_00430C40"/>, <c>+8</c>, <c>&amp;DAT_00550b68</c>). La meme tuile
    /// <c>0x10 x 0x10</c>, a l'octet 5 pres. Les deux corps posent <c>+0x40 = 0</c> et n'y
    /// touchent plus.
    /// </summary>
    private static readonly byte[] s_Descriptors_00550B60 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x00550b60  <- 0x004312E0
        0x08, 0x08, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00550b68  <- 0x00430C40
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550b88</c> — <b>la bulle et le grain du lac</b>, cinq
    /// enregistrements : la tuile de la bulle, puis les quatre images du grain a <c>+8</c>. Memes
    /// octets que <c>s_Descriptors_00550958</c> des ruines souterraines.
    /// </summary>
    private static readonly byte[] s_Descriptors_00550B88 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550b88  <- 0x00430F10, la bulle
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550b90  <- 0x00430E20, le grain
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550b98
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550ba0
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550ba8
    ];

    private static byte* s_Descriptors00550B20Storage;
    private static byte* s_Descriptors00550B60Storage;
    private static byte* s_Descriptors00550B88Storage;

    private static byte* Descriptors00550B20() => NativeCopy(s_Descriptors_00550B20, ref s_Descriptors00550B20Storage);
    private static byte* Descriptors00550B60() => NativeCopy(s_Descriptors_00550B60, ref s_Descriptors00550B60Storage);
    private static byte* Descriptors00550B88() => NativeCopy(s_Descriptors_00550B88, ref s_Descriptors00550B88Storage);

    // =====================================================================
    // LA BULLE ET LE GRAIN — le couple de la mine, une fois de plus
    // =====================================================================

    /// <summary>Ghidra: FUN_00430f10 @ 0x00430f10 — la bulle du lac.
    /// <see cref="EntityHandler_004079E0"/> a la planche pres : <c>0x00550b88</c>.</summary>
    private static void EntityHandler_00430F10(byte* e)
        => TickRisingBubble(e, Descriptors00550B88());           // Ghidra: &DAT_00550b88

    /// <summary>Ghidra: <c>0x00430e20</c> — le grain du lac. Absent de <c>Blaze.c</c> ;
    /// <see cref="EntityHandler_004070F0"/> a la planche pres : <c>0x00550b90</c>.</summary>
    private static void EntityHandler_00430E20(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors00550B88() + 8); // Ghidra: &DAT_00550b90

    // =====================================================================
    // LES BULLES QUI ATTENDENT — deux corps, une meme horloge
    //
    // Toutes deux naissent CACHEES (bit 0x4000) pour 0 a 4 images, puis se montrent et parcourent
    // les quatre images de s_Descriptors_00550B20 a raison d'une toutes les deux images, en
    // tremblant autour de leur place, et meurent a la quatrieme. Role de dessin 2, deux echelles
    // tirees chacune de 0x5dc a 0xdab.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00431000</c> — creneau de la table <c>0x0056b2c8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 116 instructions.
    ///
    /// <para>
    /// <b>Elle nait d'un seul cote.</b> Si son cap <c>+0x32</c> est nul, elle part de 100 a 399
    /// unites a droite (<c>+X</c>) ; sinon de 100 a 399 a gauche — le binaire ecrit
    /// <c>-0x64000 - tirage</c>. En profondeur ±80, en hauteur ±5. Sur place, elle tremble de ±5
    /// par image sur X et Z.
    /// </para>
    /// </summary>
    private static void EntityHandler_00431000(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            bool straight = *(short*)(e + 0x32) == 0;

            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00550B20();          // Ghidra: &DAT_00550b20
            e[0x0a] = 2;
            e[0x0b] = 0;

            if (straight) *(int*)(e + 0x1c) += (CrtRand() % 300 + 100) << 0xc;
            else *(int*)(e + 0x1c) += -0x64000 - ((CrtRand() % 300) << 0xc);

            *(int*)(e + 0x24) += (CrtRand() % 0xa0 - 0x50) << 0xc;
            *(int*)(e + 0x20) += (CrtRand() % 10 - 5) << 0xc;

            BirthHiddenBubble(e);
        }

        StepHiddenBubbleDelay(e);

        if (*(short*)(e + 0x10) != 2) return;

        *(int*)(e + 0x1c) += (CrtRand() % 10 - 5) << 0xc;
        *(int*)(e + 0x24) += (CrtRand() % 10 - 5) << 0xc;

        StepHiddenBubbleFrame(e);
    }

    /// <summary>
    /// Ghidra: <c>0x00431170</c> — creneau de la table <c>0x0056b2c8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 111 instructions.
    ///
    /// <para>
    /// La soeur de <see cref="EntityHandler_00431000"/> : elle se disperse de ±150 unites autour de
    /// sa naissance, tremble de ±3 sur X et Z, et <b>monte</b> de 0 a 2 unites par image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00431170(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00550B20();          // Ghidra: &DAT_00550b20
            e[0x0a] = 2;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 300 - 0x96) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 300 - 0x96) << 0xc;
            *(int*)(e + 0x20) += (CrtRand() % 10 - 5) << 0xc;

            BirthHiddenBubble(e);
        }

        StepHiddenBubbleDelay(e);

        if (*(short*)(e + 0x10) != 2) return;

        *(int*)(e + 0x1c) += (CrtRand() % 6 - 3) << 0xc;
        *(int*)(e + 0x24) += (CrtRand() % 6 - 3) << 0xc;
        *(int*)(e + 0x20) += (CrtRand() % 3) << 0xc;

        StepHiddenBubbleFrame(e);
    }

    /// <summary>Ghidra: la fin de naissance commune aux deux bulles, recopiee en ligne — deux
    /// echelles tirees separement, l'image 0, le bit cache <c>0x4000</c>, un delai de 0 a
    /// 4.</summary>
    private static void BirthHiddenBubble(byte* e)
    {
        *(short*)(e + 0x28) = (short)(CrtRand() % 0x7d0 + 0x5dc);
        int draw = CrtRand();
        *(short*)(e + 0x14) = 0;
        *(short*)(e + 0x2a) = (short)(draw % 0x7d0 + 0x5dc);

        int delay = CrtRand();
        e[0x40] = 0;
        *(uint*)e |= 0x4000;
        *(short*)(e + 0x12) = (short)(delay % 5);
    }

    /// <summary>Ghidra: l'etat 1 commun — le delai decompte ; sous zero, l'etat 2 et le bit
    /// <c>0x4000</c> leve.</summary>
    private static void StepHiddenBubbleDelay(byte* e)
    {
        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) >= 0) return;

        *(short*)(e + 0x10) = 2;
        *(uint*)e &= 0xffffbfff;
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>Ghidra: la queue commune de l'etat 2 — une image toutes les deux ; a la
    /// quatrieme, <b>egalite</b> et non depassement, l'entite s'eteint.</summary>
    private static void StepHiddenBubbleFrame(byte* e)
    {
        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) < 2) return;

        *(short*)(e + 0x12) = 0;
        e[0x40]++;
        if (e[0x40] == 4) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LES DEUX GERBES D'EAU
    // =====================================================================

    /// <summary>Ghidra: FUN_00430c40 @ 0x00430c40 — <b>la grosse gerbe</b>. Decompilee dans
    /// <c>Blaze.c</c>, 147 instructions. Voir <see cref="TickLakeSplash"/> : role 9, ±100 unites
    /// en X, un jet de 16 a 30.</summary>
    private static void EntityHandler_00430C40(byte* e)
        => TickLakeSplash(e, Descriptors00550B60() + 8, 9, 200, 100, 0x10);   // Ghidra: &DAT_00550b68

    /// <summary>Ghidra: FUN_004312e0 @ 0x004312e0 — <b>la gerbe fine</b>. Decompilee dans
    /// <c>Blaze.c</c>, 147 instructions. Voir <see cref="TickLakeSplash"/> : role <c>0x0d</c>,
    /// ±50 unites en X, un jet de 10 a 24.</summary>
    private static void EntityHandler_004312E0(byte* e)
        => TickLakeSplash(e, Descriptors00550B60(), 0x0d, 100, 0x32, 10);     // Ghidra: &DAT_00550b60

    /// <summary>
    /// Le corps commun de <see cref="EntityHandler_00430C40"/> et
    /// <see cref="EntityHandler_004312E0"/>. Dans <c>Blaze.c</c> ce sont deux fonctions
    /// identiques a cinq constantes pres : la planche, le role de dessin, l'etendue et le
    /// decalage du tirage en X, et la base du jet.
    ///
    /// <para>
    /// A la naissance, elle se disperse (X selon la variante, ±25 en hauteur, ±100 en profondeur),
    /// avance de 50 dans son cap, tire deux echelles une premiere fois (<c>0xc00</c> a
    /// <c>0xfff</c>) puis <b>les retire</b> une seconde (<c>0x1000</c> a <c>0x1bff</c>) — le
    /// premier tirage est ecrase. Trois vitesses : ±3 en X et Z, le jet vers le haut en Y.
    /// </para>
    /// <para>
    /// Ensuite elle file, la vitesse verticale perdant 2 par image, et chaque canal de teinte
    /// perd 2 <b>en arithmetique d'octet</b> — sans plancher : partie de zero, elle passe a
    /// <c>0xfe</c>. Passe la dixieme image, un tirage sur six l'eteint ; a la seizieme, de toute
    /// facon.
    /// </para>
    /// </summary>
    private static void TickLakeSplash(byte* e, byte* plate, byte drawRole, int spreadX, int offsetX, int jetBase)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;
            *(short*)(e + 0x28) = (short)(SignedMask(CrtRand(), 0x3ff) + 0xc00);
            int draw = CrtRand();
            e[0x0a] = drawRole;
            e[0x0b] = 0;
            *(short*)(e + 0x2a) = (short)(SignedMask(draw, 0x3ff) + 0xc00);

            *(int*)(e + 0x1c) += (CrtRand() % spreadX - offsetX) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 200 - 100) * 0x1000;

            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 0x32;   // Ghidra: Ordinal_451
            *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * 0x32;        // Ghidra: Ordinal_450

            *(int*)(e + 0x50) = (CrtRand() % 6 - 3) * 0x1000;
            *(int*)(e + 0x54) = (CrtRand() % 0xf + jetBase) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % 6 - 3) * 0x1000;

            *(short*)(e + 0x28) = (short)(CrtRand() % 0xc00 + 0x1000);
            int second = CrtRand();
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x2a) = (short)(second % 0xc00 + 0x1000);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);
        (*(short*)(e + 0x12))++;
        e[0x38] -= 2;
        *(int*)(e + 0x54) -= 0x2000;
        e[0x39] -= 2;
        e[0x3a] -= 2;

        int roll = CrtRand();
        if ((*(short*)(e + 0x12) > 10 && (short)(roll % 6) == 1) || *(short*)(e + 0x12) == 0x10)
            *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LA VAGUE ET SA COURONNE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00484560</c> — creneau de la table <c>0x0056b1c8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 163 instructions.
    ///
    /// <para>
    /// <b>Deux phases, que le bit <c>0x2000000</c> separe.</b>
    /// </para>
    /// <list type="table">
    ///   <item><term>la montee</term><description>tant que la teinte n'atteint pas
    ///     <c>0x808080</c>, elle gagne <c>0x080808</c> par image — et a chaque image ou
    ///     <c>+0x12</c> est nul, c'est-a-dire toujours, elle fait naitre <b>l'eclaboussure de
    ///     Pieds mouilles</b> (sous-type <c>0x4a</c>, planche <c>0x00551bb8</c>) avec une vitesse
    ///     tiree : 20 dans un cap au hasard, un jet de 20 a 35.</description></item>
    ///   <item><term>la vague</term><description>son echelle X tend vers <c>0x2000</c> d'un
    ///     huitieme par image, son echelle Y ondule autour de <c>0x3000</c> (sinus de
    ///     <c>+0x12 + +0x10 * 256</c>), Z vaut une fois et demie X ; elle avance de 16 dans son
    ///     cap, se pose au sol, et tant que sa teinte vaut au moins <c>0x202020</c>, <b>elle
    ///     frappe</b> avec la sonde <c>(0, -150, 100, 150, 0, 300)</c> au nom de son porteur
    ///     <c>+0x74</c> (<see cref="ResolveMeleeHit_00539B80"/>, verrou de 8 images). A la
    ///     vingtieme image elle palit de <c>0x080808</c> par image, et s'efface en entier
    ///     (<c>*e = 0</c>) a zero.</description></item>
    /// </list>
    /// <para>
    /// <b>L'echelle est ecrite en un seul mot de 32 bits</b> — <c>(Y &lt;&lt; 16) | X</c> dans
    /// <c>+0x28</c> — et X y entre par un <c>or</c> sur tout le registre : un X negatif
    /// ecraserait Y. Transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Le test de sol compare la hauteur a elle-meme</b> : le corps relit <c>+0x20</c> avant la
    /// pose, la recrit apres, puis teste <c>ancienne + 0xa000 &gt; ancienne</c> — toujours vrai
    /// hors debordement. La hauteur est donc ramenee au sol des qu'elle le touche, et le bit
    /// <c>0x800</c> de <c>+0x90</c> n'est jamais pose par ce chemin. Le sort Pieds mouilles
    /// (<c>School0Wizard.cs</c>) compare, lui, a la hauteur APRES la pose.
    /// </para>
    /// </summary>
    private static void EntityHandler_00484560(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = unchecked((short)0xff6a); probe[2] = 0x64;
        probe[3] = 0x96; probe[4] = 0; probe[5] = 0x12c;

        if ((*(uint*)e & 0x2000000) == 0)
        {
            // ------------------------------------------------------------ la montee
            if (*(short*)(e + 0x12) == 0)
            {
                int course = CrtRand();
                *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * 20;   // Ghidra: FUN_00545a1c
                *(int*)(e + 0x54) = ((CrtRand() & 0xf) + 0x14) << 0xc;
                *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(course) * 20;        // Ghidra: FUN_00545a16

                SpawnStatusEffectEntity_0047AC40(
                    e, (int*)(e + 0x1c), Descriptors00551BB8(), 0x4a, e[0x0b], -1);   // Ghidra: FUN_0047ac40, &DAT_00551bb8
            }

            uint tint = *(uint*)(e + 0x38);
            if (tint < 0x808080)
            {
                *(uint*)(e + 0x38) = tint + 0x80808;
                return;
            }

            *(uint*)e |= 0x2000000;
            return;
        }

        // ---------------------------------------------------------------- la vague
        int scaleX = *(short*)(e + 0x28);
        int phase = *(short*)(e + 0x12) + (*(short*)(e + 0x10) << 8);
        int scaleY = *(short*)(e + 0x2a);

        scaleX += (0x2000 - scaleX) >> 3;
        scaleY += ((int)Winsys3d.GetSinTableEntry(phase) * 2 + 0x3000 - scaleY) >> 2;   // Ghidra: FUN_00545a1c

        int keptZ = *(int*)(e + 0x24);
        int keptY = *(int*)(e + 0x20);
        *(uint*)(e + 0x28) = ((uint)scaleY << 0x10) | (uint)scaleX;
        *(short*)(e + 0x2c) = (short)((scaleX >> 1) + scaleX);
        int keptX = *(int*)(e + 0x1c);

        short heading = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) << 4;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) << 4;

        int before = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        int ground = *(int*)(e + 0x60);
        *(int*)(e + 0x20) = before;

        if (before >= ground && (*(uint*)(e + 0x90) & 0x2200) != 0x2000)
        {
            // Ghidra: `lea edx,[edi+0A000h] ; cmp edx,edi ; jg` — l'ancienne hauteur contre elle-meme.
            if (before + 0xa000 > before) *(int*)(e + 0x20) = ground;
            else *(uint*)(e + 0x90) |= 0x800;
        }

        if ((*(uint*)(e + 0x90) & 0x802) != 0)
        {
            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x20) = keptY;
            *(int*)(e + 0x24) = keptZ;
        }

        if (*(uint*)(e + 0x38) >= 0x202020)
        {
            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);   // Ghidra: FUN_00474ca0
            if (victim != null)
            {
                ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);   // Ghidra: FUN_00539b80
                *(uint*)victim |= 0x40;
                *(ushort*)(victim + 0x96) = 8;
            }
        }

        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) < 0x14) return;

        uint faded = *(uint*)(e + 0x38) + 0xfff7f7f8;
        *(uint*)(e + 0x38) = faded;
        if (faded == 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004844a0 @ 0x004844a0 — creneau de la table <c>0x0056b0c8</c>. Decompilee dans
    /// <c>Blaze.c</c>, 61 instructions.
    ///
    /// <para>
    /// <b>La couronne de sept vagues, en une image.</b> L'entite se pose en echelle
    /// <c>0x1000</c> (<c>+0x28</c> en 32 bits, <c>+0x2c</c> en 16), tourne de -<c>0x180</c>,
    /// garde son porteur <c>+0x6c</c> en <c>+0x74</c>, et fait naitre sept entites
    /// d'incantation (<see cref="SpawnCastEntity_0047AAE0"/>) sur un cercle autour du porteur :
    /// rayon <b>100 et 50 en alternance</b> (les tours impairs du compte a rebours, 7, 5, 3, 1,
    /// sont a 50), un huitieme de tour entre deux, <c>+0x12</c> avancant de <c>0x700</c> a
    /// chaque fois. Leur sous-type est l'octet 9 du premier enregistrement de la table
    /// <c>+0xd0</c> du repertoire de zone, plus <c>0xc0</c> : un creneau de debordement choisi
    /// par la donnee de zone.
    /// </para>
    /// <para>
    /// Puis elle s'efface en entier (<c>*e = 0</c>). Les filles heritent de tout ce qui a ete
    /// pose avant leur naissance — echelle, porteur, teinte a zero, <c>+0x12</c>, cap.
    /// </para>
    /// </summary>
    private static void EntityHandler_004844A0(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);

        *(int*)(e + 0x28) = 0x1000;
        *(short*)(e + 0x2c) = 0x1000;
        *(short*)(e + 0x32) += -0x180;
        *(byte**)(e + 0x74) = host;
        *(short*)(e + 0x36) = 4;
        *(int*)(e + 0x38) = 0;
        e[4] = 0x1e;
        *(short*)(e + 0x12) = 0;

        for (int left = 7; left != 0; left--)
        {
            short heading = *(short*)(e + 0x32);
            int radius = (left & 1) != 0 ? 0x32 : 100;

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(heading) * radius + *(int*)(host + 0x1c);   // Ghidra: Ordinal_451
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(heading) * radius + *(int*)(host + 0x24);        // Ghidra: Ordinal_450

            byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xd0)))[9] + 0xc0);
            SpawnCastEntity_0047AAE0(e, subtype, 0, 1);                                                   // Ghidra: FUN_0047aae0

            *(short*)(e + 0x12) += 0x700;
            *(short*)(e + 0x32) = (short)(heading + 0x80);
        }

        *(uint*)e = 0;
    }
}
