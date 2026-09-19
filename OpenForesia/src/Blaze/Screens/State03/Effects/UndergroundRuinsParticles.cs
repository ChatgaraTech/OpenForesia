// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DES RUINES SOUTERRAINES — zone 7, sous-zones 0 a 2.
//
// Ouvert le 2026-09-19 avec la zone, sur le patron de `ImmortalCastleParticles.cs` : ce qui nait,
// scintille et meurt dans les ruines, et ne decide de rien. Six corps, tous des creneaux de
// debordement (tables 0x0056b1c8 et 0x0056b2c8), tous dans 0x0042e600..0x0042ef8f :
//
//     0x0042EEA0   creneau 63 de 0x0055a6a8   la bulle de la mine, planche 0x00550958
//     0x0042EDB0   creneau 62 de 0x0055a6a8   le grain de la mine, planche 0x00550960
//     0x0042EC30   creneau 59 de 0x0055a6a8   l'etincelle qui monte en accelerant
//     0x0042E940   creneau 60 de 0x0055a6a8   le tourbillon qui se resserre puis s'ouvre
//     0x0042E600   creneau 61 de 0x0055a6a8   son frere, qui enfle en tournant
//     0x0042ED20   creneau 63 de 0x0055a5a8   l'onde qui s'etend, sans planche
//
// Dumps : Legacy/Raw/Blaze/UndergroundRuins/.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES PLANCHES DE LA ZONE
    //
    // Le chateau relevait ses planches par blocs de 0xa0 octets, et la zone 7 a un bloc de la meme
    // famille — MAIS PAS LE MEME. 0x005508f0..0x0055097f porte les dix-huit derniers
    // enregistrements du bloc du chateau (`s_Descriptors_00550480` a partir de son troisieme) ;
    // les deux premiers, la spirale et la tuile 0xc0/0x3f, n'y sont pas : 0x005508d8..0x005508ef
    // est `s_Descriptors_005508D8`, la planche d'orbite du chateau (ImmortalCastleParticles.cs).
    // Comparer 0x005508e0 a 0x00550480 sur 0xa0 octets echoue donc des le second octet.
    //
    // Seuls les enregistrements qu'un corps de la zone designe sont releves ici, bornes chacun par
    // un changement de forme :
    //
    //     0x00550950   0x20 x 0x20, couche 0x03      (borne basse de la bulle)
    //     0x00550958   0x08 x 0x08, couche 0x10      la bulle, puis les quatre images du grain
    //     0x00550980   0x1f x 0x1f, page 0x08        les trois corps de la zone qui tournent
    //     0x00550988   0x08 x 0x08, couche 0x10      (borne haute : autre forme, autre bloc)
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550958</c> — <b>la bulle et le grain des ruines</b>, cinq
    /// enregistrements. Le premier est la tuile de la bulle (<see cref="EntityHandler_0042EEA0"/>),
    /// les quatre suivants les quatre images du grain (<see cref="EntityHandler_0042EDB0"/>, a
    /// <c>+8</c>), que son <c>+0x40</c> parcourt de 0 a 3 avant de mourir a 4. Memes octets que
    /// les enregistrements 15 a 19 de <see cref="s_Descriptors_00550480"/>.
    /// </summary>
    private static readonly byte[] s_Descriptors_00550958 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550958  <- 0x0042EEA0, la bulle
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550960  <- 0x0042EDB0, le grain
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550968
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550970
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550978
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550980</c> — <b>la tuile des trois corps tournants</b>
    /// (<see cref="EntityHandler_0042EC30"/>, <see cref="EntityHandler_0042E940"/>,
    /// <see cref="EntityHandler_0042E600"/>) : une seule, <c>0x1f x 0x1f</c> prise a
    /// <c>(0xe0, 0x70)</c>, page <c>0x08</c>. Un enregistrement ; les trois corps posent
    /// eux-memes <c>+0x40 = 0</c> et ne le touchent plus, la marge de un suffit.
    /// </summary>
    private static readonly byte[] s_Descriptors_00550980 =
    [
        0xe0, 0x70, 0x1f, 0x1f, 0x01, 0x02, 0x08, 0x00,   // 0x00550980  <- 0x0042EC30, 0x0042E940, 0x0042E600
    ];

    private static byte* s_Descriptors00550958Storage;
    private static byte* s_Descriptors00550980Storage;

    private static byte* Descriptors00550958() => NativeCopy(s_Descriptors_00550958, ref s_Descriptors00550958Storage);
    private static byte* Descriptors00550980() => NativeCopy(s_Descriptors_00550980, ref s_Descriptors00550980Storage);

    // =====================================================================
    // LA BULLE ET LE GRAIN — creneaux 63 et 62
    //
    // Le meme couple qu'au chateau, trois fois emis la-bas et une fois ici : 69 et 75 instructions,
    // UNE SEULE immediate differente de leurs parents de la mine (0x004079E0, 0x004070F0) —
    // l'adresse de la planche.
    // =====================================================================

    /// <summary>Ghidra: FUN_0042eea0 @ 0x0042eea0 — la bulle des ruines, creneau <b>63</b> du
    /// bloc <c>0x0055a6a8</c>. <see cref="EntityHandler_004079E0"/> a la planche pres :
    /// <c>0x00550958</c>.</summary>
    private static void EntityHandler_0042EEA0(byte* e)
        => TickRisingBubble(e, Descriptors00550958());           // Ghidra: &DAT_00550958

    /// <summary>Ghidra: <c>0x0042edb0</c> — le grain des ruines, creneau <b>62</b> du bloc
    /// <c>0x0055a6a8</c>. Absent de <c>Blaze.c</c> ; <see cref="EntityHandler_004070F0"/> a la
    /// planche pres : <c>0x00550960</c>, et ses quatre images tiennent dans
    /// <see cref="s_Descriptors_00550958"/>.</summary>
    private static void EntityHandler_0042EDB0(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors00550958() + 8); // Ghidra: &DAT_00550960

    // =====================================================================
    // L'ETINCELLE QUI MONTE — creneau 59
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0042ec30 @ 0x0042ec30 — creneau <b>59</b> du bloc <c>0x0055a6a8</c>. Decompilee
    /// dans <c>Blaze.c</c>, 72 instructions.
    ///
    /// <para>
    /// <b>Elle nait a 0x32 unites au-dessus de <c>+0x64</c></b>, pas de sa propre hauteur : le
    /// corps ecrase <c>+0x20</c> par <c>+0x64 + 0x32000</c>. Elle se disperse de ±150 unites a
    /// l'horizontale, prend une echelle de <c>0x800</c> a <c>0xbff</c> et une teinte de
    /// <c>0xb4</c>.
    /// </para>
    /// <para>
    /// Ensuite elle <b>monte en accelerant</b> : son pas <c>+0x12</c>, tire entre 5 et 14, grandit
    /// d'une unite par image. Elle s'assombrit de 3 par canal et meurt quand le <b>rouge</b>
    /// (<c>+0x38</c>) est eteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042EC30(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00550980();          // Ghidra: &DAT_00550980
            e[0x40] = 0;
            e[0x0a] = 0x0b;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 5);

            short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xb4;

            *(int*)(e + 0x1c) += (CrtRand() % 300 - 0x96) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 300 - 0x96) * 0x1000;
            *(int*)(e + 0x20) = *(int*)(e + 0x64) + 0x32000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) += *(short*)(e + 0x12) * 0x1000;
        (*(short*)(e + 0x12))++;

        BrightenEntityTint_00404DE0(e, -3, -3, -3);               // Ghidra: FUN_00404de0

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LES DEUX TOURBILLONS — creneaux 60 et 61
    //
    // Deux corps de la meme famille, sans copie l'un de l'autre : meme orbite, memes champs, mais
    // six constantes et deux gestes de difference. L'entite tourne autour de son point de naissance
    // (+0x50/+0x54/+0x58) sur une sphere : deux phases, +0x12 et +0x14, qu'avancent +0x48 et +0x4c ;
    // un rayon, +0x9a, que +0x5c fait fondre.
    //
    //     X = (cos(+0x14) * rayon >> 12) * sin(+0x12)
    //     Z = (cos(+0x14) * rayon >> 12) * cos(+0x12)
    //     Y =  sin(+0x14) * rayon
    //
    // PHASE 1 : le rayon fond de +0x5c par image, +0x5c decroit d'une unite (plancher 2), la teinte
    // s'eclaircit de 10. PASSE L'IMAGE 60, PHASE 3 : le rayon REGROSSIT de +0x5c, l'echelle fond de
    // deux fois +0x44, la teinte s'eteint (-7, -7, -5), et l'entite meurt quand le bleu s'eteint.
    // Il n'y a pas de phase 2 : le numero saute de 1 a 3.
    //
    // Ce qui les distingue :
    //
    //                          0x0042E940              0x0042E600
    //     rayon de depart      400 a 499               300 a 399
    //     echelle              0x400 a 0x5ff           0x400 a 0x7ff
    //     seuil de rayon       0x118 : la rotation     0xfb : la rotation DOUBLE et
    //                          ralentit de moitie      l'echelle grandit de 2 x +0x44
    //     echelle, phase 1     ne bouge pas            +0x44 par image, inverse a l'image 40
    //     entree en phase 3    +0x5c = 40..59          +0x5c = 40..89
    //                          +0x44 = 70..219         +0x44 = 200..349
    //     +0x44, phase 3       -5, sans plancher       -10, plancher 10
    //     +0x5c, phase 3       constant                perd un huitieme par image
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0042e940</c> — creneau <b>60</b> du bloc <c>0x0055a6a8</c>. <b>Absent de
    /// <c>Blaze.c</c></b> ; 228 instructions, <c>Legacy/Raw/Blaze/UndergroundRuins/FUN_0042E940.asm</c>.
    ///
    /// <para>
    /// <b>Le tourbillon qui se resserre en ralentissant.</b> Voir le bandeau pour la forme commune.
    /// Sous le rayon <c>0x118</c>, chaque image retire aussitot la moitie du pas qu'elle vient
    /// d'ajouter aux deux phases : l'orbite ralentit de moitie en se resserrant. <b>Son
    /// <c>+0x44</c> n'est pas tire a la naissance</b> — seulement a l'entree en phase 3 —, et il
    /// perd 5 par image sans plancher : passe zero, l'echelle se met a REGROSSIR, tant que la
    /// teinte n'est pas eteinte.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042E940(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00550980();          // Ghidra: &DAT_00550980
            e[0x40] = 0;
            e[0x0a] = 0x0b;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x12) = (short)SignedMask(CrtRand(), 0xfff);
            *(short*)(e + 0x14) = (short)SignedMask(CrtRand(), 0x3ff);
            *(short*)(e + 0x9a) = (short)(CrtRand() % 100 + 400);

            int shrink = CrtRand() % 15;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x5c) = shrink + 0x0d;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            int scaleDraw = CrtRand();
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            short scale = (short)(SignedMask(scaleDraw, 0x1ff) + 0x400);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x48) = CrtRand() % 100;
            *(int*)(e + 0x4c) = CrtRand() % 100;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short radius = OrbitAroundBirthPoint(e);
            int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14));   // Ghidra: FUN_00545a1c, avant les deux pas

            *(short*)(e + 0x12) += (short)*(int*)(e + 0x48);
            *(short*)(e + 0x14) += (short)*(int*)(e + 0x4c);
            *(int*)(e + 0x20) = lift * radius + *(int*)(e + 0x54);

            radius -= (short)*(int*)(e + 0x5c);
            *(short*)(e + 0x9a) = radius;

            // Ghidra: 0x0042eac5 — `cmp ax,118h ; jge` : le pas retire est celui qu'on vient
            // d'ajouter, decale d'un bit en arithmetique sur l'entier entier.
            if (radius < 0x118)
            {
                *(short*)(e + 0x12) -= (short)(*(int*)(e + 0x48) >> 1);
                *(short*)(e + 0x14) -= (short)(*(int*)(e + 0x4c) >> 1);
            }

            (*(int*)(e + 0x5c))--;
            BrightenEntityTint_00404DE0(e, 10, 10, 10);           // Ghidra: FUN_00404de0
            if (*(int*)(e + 0x5c) <= 2) *(int*)(e + 0x5c) = 2;

            if (*(short*)(e + 0x16) > 0x3c)
            {
                *(short*)(e + 0x10) = 3;
                *(int*)(e + 0x5c) = CrtRand() % 0x14 + 0x28;
                *(int*)(e + 0x44) = CrtRand() % 0x96 + 0x46;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            short radius = OrbitAroundBirthPoint(e);
            *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) * radius + *(int*)(e + 0x54);
            *(short*)(e + 0x9a) = (short)((short)*(int*)(e + 0x5c) + radius);

            BrightenEntityTint_00404DE0(e, -7, -7, -5);           // Ghidra: FUN_00404de0

            ShrinkOrbitScale(e);

            *(int*)(e + 0x44) += -5;

            if (e[0x3a] == 0) *(uint*)e &= 0x7fffffff;
        }

        (*(short*)(e + 0x16))++;
    }

    /// <summary>
    /// Ghidra: FUN_0042e600 @ 0x0042e600 — creneau <b>61</b> du bloc <c>0x0055a6a8</c>. Decompilee
    /// dans <c>Blaze.c</c>, 255 instructions.
    ///
    /// <para>
    /// <b>Le tourbillon qui enfle en tournant.</b> Voir le bandeau pour la forme commune. Des la
    /// naissance il porte un <c>+0x44</c> (0 a 149) qui fait grandir son echelle a chaque image ;
    /// <b>a l'image 40, ce pas est inverse et divise par deux</b> — l'entite se met a retrecir —,
    /// et sous le rayon <c>0xfb</c> tout double : echelle de deux fois <c>+0x44</c>, et un second
    /// pas sur les deux phases.
    /// </para>
    /// <para>
    /// <b>L'inversion est calculee sur un entier non signe</b> : Ghidra ecrit
    /// <c>(int)-param_1[0x11] &gt;&gt; 1</c>, une negation puis un decalage arithmetique. Le
    /// resultat est le meme qu'en signe.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042E600(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00550980();          // Ghidra: &DAT_00550980
            e[0x40] = 0;
            e[0x0a] = 0x0b;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x12) = (short)SignedMask(CrtRand(), 0xfff);
            *(short*)(e + 0x14) = (short)SignedMask(CrtRand(), 0x3ff);
            *(short*)(e + 0x9a) = (short)(CrtRand() % 100 + 300);

            int shrink = CrtRand();
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x5c) = shrink % 0xf + 0xd;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            int scaleDraw = CrtRand();
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            short scale = (short)(SignedMask(scaleDraw, 0x3ff) + 0x400);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x44) = CrtRand() % 0x96;
            *(int*)(e + 0x48) = CrtRand() % 100;
            *(int*)(e + 0x4c) = CrtRand() % 100;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short radius = OrbitAroundBirthPoint(e);
            int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14));   // Ghidra: Ordinal_451, avant les deux pas

            *(short*)(e + 0x12) += (short)*(int*)(e + 0x48);
            *(short*)(e + 0x14) += (short)*(int*)(e + 0x4c);
            *(int*)(e + 0x20) = lift * radius + *(int*)(e + 0x54);

            radius -= (short)*(int*)(e + 0x5c);
            *(short*)(e + 0x9a) = radius;

            short grow = (short)*(int*)(e + 0x44);
            if (radius < 0xfb)
            {
                *(short*)(e + 0x28) += (short)(grow * 2);
                *(short*)(e + 0x2a) += (short)(grow * 2);
                *(short*)(e + 0x12) += (short)*(int*)(e + 0x48);
                *(short*)(e + 0x14) += (short)*(int*)(e + 0x4c);
            }
            else
            {
                *(short*)(e + 0x28) += grow;
                *(short*)(e + 0x2a) += grow;
            }

            if (*(short*)(e + 0x16) == 0x28) *(int*)(e + 0x44) = (int)(0u - *(uint*)(e + 0x44)) >> 1;

            (*(int*)(e + 0x5c))--;
            BrightenEntityTint_00404DE0(e, 10, 10, 10);           // Ghidra: FUN_00404de0
            if (*(int*)(e + 0x5c) < 3) *(int*)(e + 0x5c) = 2;

            if (*(short*)(e + 0x16) > 0x3c)
            {
                *(short*)(e + 0x10) = 3;
                *(int*)(e + 0x5c) = CrtRand() % 0x32 + 0x28;
                *(int*)(e + 0x44) = CrtRand() % 0x96 + 200;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            short radius = OrbitAroundBirthPoint(e);
            *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) * radius + *(int*)(e + 0x54);
            *(short*)(e + 0x9a) = (short)(radius + (short)*(int*)(e + 0x5c));

            BrightenEntityTint_00404DE0(e, -7, -7, -5);           // Ghidra: FUN_00404de0

            ShrinkOrbitScale(e);

            int grow = *(int*)(e + 0x44) - 10;
            *(int*)(e + 0x44) = grow;
            if (grow < 10) *(int*)(e + 0x44) = 10;

            *(int*)(e + 0x5c) -= *(int*)(e + 0x5c) >> 3;

            if (e[0x3a] == 0) *(uint*)e &= 0x7fffffff;
        }

        (*(short*)(e + 0x16))++;
    }

    /// <summary>
    /// Ghidra: <c>0x0042e600..</c> / <c>0x0042e940..</c> — <b>la position sur l'orbite</b>, en tete
    /// des deux phases des deux tourbillons : X et Z, et le rayon <c>+0x9a</c> lu <b>avant</b>
    /// que la phase ne le change. La hauteur Y reste chez l'appelant, parce que la phase 1 lit son
    /// sinus avant d'avancer les phases, et la phase 3 apres (elle ne les avance pas).
    ///
    /// <para>Ce n'est pas une fonction du binaire : les quatre sites la recopient en ligne, a
    /// l'identique, dans les deux corps. Elle est sortie ici pour que la difference entre les deux
    /// se lise.</para>
    /// </summary>
    private static short OrbitAroundBirthPoint(byte* e)
    {
        short radius = *(short*)(e + 0x9a);
        short pitch = *(short*)(e + 0x14);
        short yaw = *(short*)(e + 0x12);

        *(int*)(e + 0x1c) = (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc) * (int)Winsys3d.GetSinTableEntry(yaw)
                            + *(int*)(e + 0x50);                  // Ghidra: Ordinal_450, Ordinal_451
        *(int*)(e + 0x24) = (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc) * Winsys3d.GetCosTableEntry(yaw)
                            + *(int*)(e + 0x58);                  // Ghidra: Ordinal_450, deux fois
        return radius;
    }

    /// <summary>Ghidra: la phase 3 des deux tourbillons, recopiee en ligne — l'echelle perd deux
    /// fois <c>+0x44</c> sur ses deux axes, <b>en seize bits</b> (<c>shl ax,1</c>), avec un plancher
    /// de <c>0x200</c> compare en signe sur chacun.</summary>
    private static void ShrinkOrbitScale(byte* e)
    {
        short step = (short)(*(short*)(e + 0x44) << 1);
        *(short*)(e + 0x28) -= step;
        *(short*)(e + 0x2a) -= step;
        if (*(short*)(e + 0x28) < 0x200) *(short*)(e + 0x28) = 0x200;
        if (*(short*)(e + 0x2a) < 0x200) *(short*)(e + 0x2a) = 0x200;
    }

    // =====================================================================
    // L'ONDE QUI S'ETEND — creneau 63 du bloc 0x0055a5a8
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0042ed20</c> — creneau <b>63</b> du bloc <c>0x0055a5a8</c> (table
    /// <c>0x0056b1c8</c>). <b>Absent de <c>Blaze.c</c></b> ; 42 instructions,
    /// <c>Legacy/Raw/Blaze/UndergroundRuins/FUN_0042ED20.asm</c>. Un jumeau non ecrit ailleurs.
    ///
    /// <para>
    /// <b>Elle ne pose aucune planche</b> : son role de dessin est <c>3</c> et son octet
    /// <c>+4</c> — le modele — est <see cref="s_Unknown_005edec4"/>, <b>le premier modele propre a
    /// la zone</b>, le meme que <c>0x00424800</c> du Labyrinthe decale de 5. A la naissance ses trois
    /// echelles (<c>+0x28</c>, <c>+0x2a</c>, <c>+0x2c</c>) et <c>+0x34</c> partent de zero, sa
    /// hauteur monte de 0x50 unites, sa teinte est <c>0xb4</c>.
    /// </para>
    /// <para>
    /// Ensuite les trois echelles grandissent de <c>0x578</c> par image pendant que la teinte perd
    /// 10 par canal ; elle meurt quand le rouge est eteint, soit a la dix-neuvieme image.
    /// </para>
    /// <para>
    /// <b>Lu par le champ, pas par l'arene</b> : <c>0x005edec4</c> tombe dans
    /// <c>EntityArena</c>, mais le chargement (<c>WorldMap.cs</c>) ecrit le champ
    /// <see cref="s_Unknown_005edec4"/>, et c'est ce que lisent ses trois autres lecteurs. C'est
    /// l'un des recouvrements que <c>MemoryLayoutTests</c> signale en rouge ; le lire par
    /// <c>Mem.Entity</c> rendrait toujours zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042ED20(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[4] = s_Unknown_005edec4;                             // Ghidra: DAT_005edec4
            *(int*)(e + 0x20) += 0x50000;
            e[0x0a] = 3;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x34) = 0;
            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xb4;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x28) += 0x578;
        *(short*)(e + 0x2a) += 0x578;
        *(short*)(e + 0x2c) += 0x578;

        BrightenEntityTint_00404DE0(e, -10, -10, -10);            // Ghidra: FUN_00404de0

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }
}
