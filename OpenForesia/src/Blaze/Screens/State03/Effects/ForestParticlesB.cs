// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DE LA FORET (2/2) — la convocation, la fumee violette, la projection large,
// l'invocation qui emporte un membre, l'eclat sur une corde d'arc, le panache.
//
// Suite de `ForestParticlesA.cs`, meme regle : chaque famille existe en deux a quatre
// exemplaires, code ET donnee dupliques par le compilateur de 1998.
//
// TROIS GENRES DE LACUNE SE CROISENT ICI, et les bandeaux `// ====` les nomment un par un :
//   - le corps ABSENT de `Blaze.c`, releve au desassembleur (la projection large, 77
//     instructions ; l'invocation, 230 ; l'eclat sur la corde, 520) ;
//   - l'AMORCE non decompilee qui saute dans un corps, lui, decompile - verifier ou pointe son
//     `call` coute deux minutes et evite de transcrire a la main ce que Ghidra a deja rendu ;
//   - le corps DUPLIQUE, a reconnaitre avant de le transcrire deux fois.
//
// Deux familles se rangent en CARRE PARFAIT, deux parametres croises (la planche d'un cote, le
// rythme de l'autre) : ce sont bien deux effets distincts qui partagent leur code, pas un seul
// regle deux fois.
//
// Lancer `sh OpenForesia.Tools/check-descriptors.sh` apres toute retouche a une planche.
// Voir Documentation/Reference/DataSymbols.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LA CONVOCATION, EN DEUX EXEMPLAIRES (2026-08-30)
    //
    // 0x00410cd0 et 0x00414140 sont le meme corps a QUATRE adresses de planche pres — deux par
    // copie. Les deux pseudo-C de Blaze.c ne different que par ces quatre lignes.
    //
    // Les planches sont identiques deux a deux : 0x0054f808 et 0x0054f920 portent le meme unique
    // enregistrement, 0x0054f910 et 0x0054fa08 les memes deux. Le jeu de 1998 a duplique le code
    // ET la donnee, comme pour la bulle et l'eclat.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054f808</c> — deux enregistrements jusqu'a
    /// <c>DAT_0054f818</c>. Le second est la grosse tuile <c>0x3f x 0x3f</c> du halo de
    /// <see cref="TickFivePhaseBurst"/>, dont la phase 3 <b>repeint l'octet 6 en place</b>.</summary>
    private static readonly byte[] s_Descriptors_0054F808 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f808
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f810
    ];

    private static byte* s_Descriptors0054F808Storage;

    private static byte* Descriptors0054F808()
        => NativeCopy(s_Descriptors_0054F808, ref s_Descriptors0054F808Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f920</c> — la copie du precedent, memes deux
    /// enregistrements, jusqu'a <c>DAT_0054f930</c>.</summary>
    private static readonly byte[] s_Descriptors_0054F920 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f920
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f928
    ];

    private static byte* s_Descriptors0054F920Storage;

    private static byte* Descriptors0054F920()
        => NativeCopy(s_Descriptors_0054F920, ref s_Descriptors0054F920Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f910</c> — deux enregistrements de <c>0x17 x 0x0f</c>
    /// jusqu'a <c>DAT_0054f920</c>. Seul le premier est atteignable : la variante 2 pose l'indice
    /// a zero et n'y revient pas.</summary>
    private static readonly byte[] s_Descriptors_0054F910 =
    [
        0x90, 0xf0, 0x0f, 0x0f, 0x00, 0x00, 0x09, 0x00,   // 0x0054f910
        0x20, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f918
    ];

    private static byte* s_Descriptors0054F910Storage;

    private static byte* Descriptors0054F910()
        => NativeCopy(s_Descriptors_0054F910, ref s_Descriptors0054F910Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054fa08</c> — la copie du precedent, meme contenu.</summary>
    private static readonly byte[] s_Descriptors_0054FA08 =
    [
        0x90, 0xf0, 0x0f, 0x0f, 0x00, 0x00, 0x09, 0x00,   // 0x0054fa08
        0x20, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054fa10
    ];

    private static byte* s_Descriptors0054FA08Storage;

    private static byte* Descriptors0054FA08()
        => NativeCopy(s_Descriptors_0054FA08, ref s_Descriptors0054FA08Storage);

    /// <summary>Ghidra: FUN_00410cd0 @ 0x00410cd0 — la premiere des deux convocations. Le corps est
    /// dans <see cref="TickSummonEffect"/>.</summary>
    private static void EntityHandler_00410CD0(byte* e)
        => TickSummonEffect(e, Descriptors0054F910(), Descriptors0054F808());

    /// <summary>Ghidra: FUN_00414140 @ 0x00414140 — la seconde.</summary>
    private static void EntityHandler_00414140(byte* e)
        => TickSummonEffect(e, Descriptors0054FA08(), Descriptors0054F920());

    /// <summary>
    /// Le corps commun des deux convocations : <b>quatre effets dans une seule fonction</b>,
    /// choisis par <c>+0x14</c>, dont un est le <b>chef d'orchestre</b> des trois autres.
    ///
    /// <para>
    /// <b>Variante 2 — le chef.</b> Il se teleporte de <c>0x12c000</c> vers le haut et de
    /// <c>0x28</c> unites dans la direction de son cap, puis engendre <b>trente-et-une</b> copies
    /// de lui-meme en variante 0 — il pose <c>+0x14 = 0</c> le temps des naissances, que
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> recopie, et le restaure ensuite. Pendant
    /// quarante-trois images il s'eclaircit ; ensuite il <b>redescend</b> de trois unites par image
    /// tout en lachant une variante 1 a chaque image. Passe l'image <c>0x9a</c>, il lache
    /// trente-et-une variantes 4 et s'eteint.
    /// </para>
    /// <para>
    /// <b>Variantes 0 et 4 — deux spirales opposees.</b> Toutes deux tournent autour d'un centre
    /// fige a la naissance, la hauteur suivant un sinus de phase propre. La 0 <b>rentre</b> : son
    /// rayon perd 2 a 4 par image et elle meurt quand il passe sous zero. La 4 <b>sort</b> : son
    /// rayon gagne 5 a 8, son cap tourne <b>a l'envers</b> et deux fois plus vite, sa hauteur bat
    /// deux fois plus ample, et elle meurt d'avoir noirci — sa teinte perd <c>2 a 4</c> par image
    /// la ou la 0 en gagne dix.
    /// </para>
    /// <para>
    /// <b>Variante 1 — la bouffee.</b> La plus simple : dispersee de <c>±20</c> sur les trois axes,
    /// elle monte d'une unite par image et s'efface en s'assombrissant de six.
    /// </para>
    /// <para>
    /// <b>Le sous-type des enfants vient de l'entite</b> : <c>+0x98</c>, pose a <c>0x38</c> a
    /// chaque image par les deux premieres lignes du corps, plus <c>0xc0</c> — donc <c>0xf8</c>,
    /// une entree de la table installee par zone.
    /// </para>
    /// </summary>
    private static void TickSummonEffect(byte* e, byte* leaderAtlas, byte* childAtlas)
    {
        *(short*)(e + 0x98) = 0x38;
        *(short*)(e + 0x96) = 10;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = leaderAtlas;
        }

        // ---------------------------------------------------------------- variante 0 : la spirale rentrante
        if (*(short*)(e + 0x14) == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = childAtlas;
                e[0x40] = 0;

                *(int*)(e + 0x54) = *(int*)(e + 0x20);
                *(ushort*)(e + 0x32) = (ushort)SignedMask(CrtRand(), 0xfff);
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                *(short*)(e + 0x9a) = (short)(CrtRand() % 0x14 + 0x46);

                int speed = CrtRand();
                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;
                *(int*)(e + 0x5c) = speed % 3 + 2;

                *(ushort*)(e + 0x12) = (ushort)SignedMask(CrtRand(), 0xfff);
                e[0x0a] = 9;
                e[0x0b] = 0;

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 10, 10, 10);
                if (e[0x38] > 0x7f)
                {
                    e[0x38] = 0x80;
                    e[0x39] = 0x80;
                    e[0x3a] = 0x80;
                }

                SpiralAroundAnchor(e, 0x14);

                *(short*)(e + 0x9a) -= (short)*(int*)(e + 0x5c);
                *(short*)(e + 0x32) += (short)(*(int*)(e + 0x5c) * 0x10);

                if (*(short*)(e + 0x9a) < 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // ---------------------------------------------------------------- variante 1 : la bouffee
        if (*(short*)(e + 0x14) == 1)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = childAtlas;
                e[0x40] = 0;
                e[0x0a] = 9;
                e[0x0b] = 0;

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                e[0x38] = 0x80;
                e[0x39] = 0x80;
                e[0x3a] = 0x80;
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) * 0x1000;
                *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) * 0x1000;
                *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                *(int*)(e + 0x20) += 0x1000;
                BrightenEntityTint_00404DE0(e, -6, -6, -6);
                if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // ---------------------------------------------------------------- variante 2 : le chef
        if (*(short*)(e + 0x14) == 2)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                e[0x40] = 0;
                e[0x0a] = 0x0e;
                e[0x0b] = 0;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;

                *(int*)(e + 0x20) += 0x12c000;

                short course = *(short*)(e + 0x32);
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 0x28;
                e[0x38] = 0;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 0x28;

                *(short*)(e + 0x28) = 0x800;
                *(short*)(e + 0x2a) = 0x800;
                e[0x39] = 0;
                e[0x3a] = 0;

                SpawnSummonChildren(e, 0, 0x1f);
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 3, 3, 3);
                if (e[0x38] > 0x7f)
                {
                    e[0x38] = 0x80;
                    e[0x39] = 0x80;
                    e[0x3a] = 0x80;
                }

                (*(short*)(e + 0x12))++;
                if (*(short*)(e + 0x12) > 0x2a)
                {
                    *(short*)(e + 0x10) = 2;
                    *(short*)(e + 0x12) = 0;
                }
            }

            if (*(short*)(e + 0x10) == 2)
            {
                SpawnSummonChildren(e, 1, 1);

                if (*(short*)(e + 0x12) < 0x4b) *(int*)(e + 0x20) -= 0x3000;
                (*(short*)(e + 0x12))++;

                if (*(short*)(e + 0x16) > 0x9a)
                {
                    SpawnSummonChildren(e, 4, 0x1f);
                    *(uint*)e &= 0x7fffffff;
                }
            }
        }

        // ---------------------------------------------------------------- variante 4 : la spirale sortante
        if (*(short*)(e + 0x14) == 4)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = childAtlas;
                e[0x40] = 0;

                *(int*)(e + 0x58) = *(int*)(e + 0x24);
                *(short*)(e + 0x9a) = 0;
                *(ushort*)(e + 0x32) = (ushort)SignedMask(CrtRand(), 0xfff);
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x54) = *(int*)(e + 0x20);

                e[0x38] = 0x80;
                e[0x39] = 0x80;
                e[0x3a] = 0x80;
                *(int*)(e + 0x5c) = SignedMask(CrtRand(), 3) + 5;

                *(ushort*)(e + 0x12) = (ushort)SignedMask(CrtRand(), 0xfff);
                e[0x0a] = 9;
                e[0x0b] = 0;

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;

                *(int*)(e + 0x44) = -(CrtRand() % 3) - 2;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                SpiralAroundAnchor(e, 0x28);

                *(short*)(e + 0x9a) += (short)*(int*)(e + 0x5c);
                *(short*)(e + 0x32) += (short)(*(int*)(e + 0x5c) * -2);

                short fade = (short)*(int*)(e + 0x44);
                BrightenEntityTint_00404DE0(e, fade, fade, fade);

                if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        (*(short*)(e + 0x16))++;
    }


    // =====================================================================
    // LA FUMEE VIOLETTE AUX TROIS ROLES, ET SES DEUX AMORCES (2026-08-30)
    //
    // 0x004103d0 et 0x00413840 sont absents de Blaze.c. Ce sont deux AMORCES de six instructions
    // — `mov eax,[esp+4] ; push eax ; call ... ; add esp,4 ; ret` — sans le moindre traitement :
    // elles n'existent que parce que la table de sauts a besoin d'une adresse a elle. Le corps,
    // FUN_004103e0 et sa copie FUN_00413850, est decompile ; les deux ne different que par leurs
    // trois ecritures de planche.
    //
    // Le releve des amorces est dans Legacy/Raw/Blaze/FUN_004103D0.asm.
    // =====================================================================

    /// <summary>Ghidra: <c>0x004103d0</c> — l'amorce, six instructions, aucun traitement.</summary>
    private static void EntityHandler_004103D0(byte* e)
        => TickPurpleSmoke(e, Descriptors0054F808());   // Ghidra: FUN_004103e0

    /// <summary>Ghidra: <c>0x00413840</c> — la seconde amorce, sur l'autre copie de la
    /// planche.</summary>
    private static void EntityHandler_00413840(byte* e)
        => TickPurpleSmoke(e, Descriptors0054F920());   // Ghidra: FUN_00413850

    /// <summary>
    /// Ghidra: FUN_004103e0 @ 0x004103e0 (et FUN_00413850, sa copie) — <b>la fumee violette aux
    /// trois roles</b>. Decompilee dans <c>Blaze.c</c>, 335 instructions.
    ///
    /// <para>
    /// Meme construction que <see cref="TickFivePhaseBurst"/> : <c>+0x14</c> choisit le role a la
    /// naissance, <c>+0x10</c> est l'etat a l'interieur. Les trois roles partagent la teinte
    /// <c>0x80 / 0x8c / 0xaa</c> — un violet pale — et la meme unique tuile.
    /// </para>
    /// <list type="bullet">
    /// <item><b>0</b> — <b>la spirale rentrante</b> : elle nait <c>0x64000</c> au-dessus de sa
    /// position, sur un rayon de <c>300</c> a <c>399</c> qui se <b>referme</b> de 8 a 15 par
    /// image ; le cap tourne de quatre fois autant. Elle s'allume de vingt par canal, chaque canal
    /// s'arretant a sa propre borne, et meurt quand le rayon passe sous <c>0x50</c> — donc avant
    /// d'avoir atteint le centre.</item>
    /// <item><b>1</b> — <b>la spirale sortante</b> : la meme, a l'envers. Rayon de <c>0x32</c> a
    /// <c>0x63</c> qui croit de 4 a 11 par image, cap tournant dans l'autre sens et deux fois
    /// moins vite, teinte posee pleine des la naissance et perdant cinq par canal. Elle meurt
    /// d'etre eteinte, pas d'etre arrivee quelque part.</item>
    /// <item><b>2</b> — <b>la bouffee qui monte</b> : dispersee sur <c>[-120, +119]</c> unites
    /// entieres en <c>x</c> et en <c>z</c>, elle monte d'une vitesse <c>+0x5c</c> qui
    /// <b>accelere</b> de 1 a 3 par image. Un delai de 5 a 9 images precede la disparition, qui se
    /// fait alors de dix par canal.</item>
    /// </list>
    /// <para>
    /// <b>Le role 2 tire deux fois <c>+0x12</c>.</b> Il y pose d'abord un cap sur douze bits, puis
    /// l'ecrase par le delai <c>(rand % 5) + 5</c> quelques lignes plus bas. Le premier tirage ne
    /// sert donc a rien — sauf a <b>avancer le flux de l'aleatoire</b>, ce que la transcription
    /// doit conserver pour que les images suivantes tombent au meme endroit qu'en 1998.
    /// </para>
    /// <para>
    /// <b>Les hauteurs des roles 0 et 1 ne s'echelonnent pas comme leur rayon</b> : la
    /// composante verticale est un sinus de <c>+0x12 + +0x32</c> multiplie par une constante
    /// <c>0x28</c>, pas par le rayon. La fumee monte et descend du meme creux quelle que soit sa
    /// distance a l'axe.
    /// </para>
    /// </summary>
    private static void TickPurpleSmoke(byte* e, byte* atlas)
    {
        short role = *(short*)(e + 0x14);

        // ------------------------------------------------------------- 0 : la spirale rentrante
        if (role == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = atlas;
                e[0x40] = 0;

                int heading = CrtRand();
                int top = *(int*)(e + 0x20) + 0x64000;
                *(int*)(e + 0x20) = top;
                *(short*)(e + 0x32) = (short)SignedMask(heading, 0xfff);
                *(int*)(e + 0x54) = top;
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                *(short*)(e + 0x9a) = (short)(CrtRand() % 100 + 300);

                int speed = CrtRand();
                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;
                *(int*)(e + 0x5c) = SignedMask(speed, 7) + 8;

                e[0x0a] = 2;
                e[0x0b] = 1;
                *(short*)(e + 0x12) = (short)SignedMask(CrtRand(), 0xfff);

                short scale = (short)(SignedMask(CrtRand(), 0x3ff) + 0x800);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 0x14, 0x14, 0x14);

                // Chaque canal a sa propre borne : le violet reste violet en s'allumant.
                if (e[0x38] > 0x7f) e[0x38] = 0x80;
                if (e[0x39] > 0x8b) e[0x39] = 0x8c;
                if (e[0x3a] > 0xa9) e[0x3a] = 0xaa;

                SpiralSmokeStep(e, +1, 4);

                if (*(short*)(e + 0x9a) < 0x50) *(uint*)e &= 0x7fffffff;
            }
        }

        // ------------------------------------------------------------- 1 : la spirale sortante
        if (role == 1)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = atlas;
                e[0x40] = 0;

                int heading = CrtRand();
                int top = *(int*)(e + 0x20) + 0x64000;
                *(int*)(e + 0x20) = top;
                *(short*)(e + 0x32) = (short)SignedMask(heading, 0xfff);
                *(int*)(e + 0x54) = top;
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                *(short*)(e + 0x9a) = (short)(CrtRand() % 0x32 + 0x32);

                int speed = CrtRand();
                e[0x38] = 0x80;
                e[0x39] = 0x8c;
                e[0x3a] = 0xaa;
                *(int*)(e + 0x5c) = SignedMask(speed, 7) + 4;

                e[0x0a] = 2;
                e[0x0b] = 1;
                *(short*)(e + 0x12) = (short)SignedMask(CrtRand(), 0xfff);

                short scale = (short)(CrtRand() % 0x418 + 0x800);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, -5, -5, -5);

                SpiralSmokeStep(e, -1, -2);

                if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // ------------------------------------------------------------- 2 : la bouffee qui monte
        if (role != 2) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = atlas;
            e[0x40] = 0;

            *(short*)(e + 0x32) = (short)SignedMask(CrtRand(), 0xfff);

            *(int*)(e + 0x1c) += (CrtRand() % 0xf0 - 0x78) * 0x1000;
            int z = *(int*)(e + 0x24) + (CrtRand() % 0xf0 - 0x78) * 0x1000;
            *(int*)(e + 0x24) = z;

            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = z;

            int climb = CrtRand();
            *(int*)(e + 0x5c) = 0;
            e[0x38] = 0x5a;
            e[0x39] = 0x78;
            e[0x3a] = 0xaa;
            *(short*)(e + 0x9a) = (short)(climb % 3 + 1);

            e[0x0a] = 2;
            e[0x0b] = 1;

            // Ce cap est ecrase quatre lignes plus bas ; le tirage n'existe que pour avancer le
            // flux de l'aleatoire. Voir le resume.
            *(short*)(e + 0x12) = (short)SignedMask(CrtRand(), 0xfff);

            short scale = (short)(CrtRand() % 0x418);
            *(short*)(e + 0x28) = (short)(scale + 0x800);
            *(short*)(e + 0x2a) = (short)(scale + 0x1000);

            *(short*)(e + 0x12) = (short)(CrtRand() % 5 + 5);
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))--;
        *(int*)(e + 0x20) += *(int*)(e + 0x5c) * 0x1000;
        *(int*)(e + 0x5c) += *(short*)(e + 0x9a);

        if (*(short*)(e + 0x12) >= 0) return;

        BrightenEntityTint_00404DE0(e, -10, -10, -10);
        if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Une image de spirale de fumee : la position se relit entierement du rayon <c>+0x9a</c> et du
    /// cap <c>+0x32</c> autour de l'ancre <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, puis le rayon
    /// bouge de <paramref name="radiusSign"/> fois la vitesse et le cap de
    /// <paramref name="turnFactor"/> fois.
    ///
    /// <para>Le binaire ecrit ce bloc deux fois — roles 0 et 1 — a ces deux facteurs pres.</para>
    /// </summary>
    private static void SpiralSmokeStep(byte* e, int radiusSign, int turnFactor)
    {
        short heading = *(short*)(e + 0x32);
        short radius = *(short*)(e + 0x9a);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(heading) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = (int)Winsys3d.GetCosTableEntry(heading) * radius + *(int*)(e + 0x58);
        *(int*)(e + 0x20) = *(int*)(e + 0x54)
            + (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12) + heading) * 0x28;

        short speed = (short)*(int*)(e + 0x5c);
        *(short*)(e + 0x9a) += (short)(speed * radiusSign);
        *(short*)(e + 0x32) += (short)(speed * turnFactor);
    }

    // =====================================================================
    // LA PROJECTION LARGE, EN QUATRE EXEMPLAIRES (2026-08-30)
    //
    // 0x004156b0, 0x00415c50, 0x00412240 et 0x00412720 sont le meme corps, absent de Blaze.c ;
    // releve au desassembleur, 77 instructions — Legacy/Raw/Blaze/FUN_004156B0.asm.
    //
    // Les quatre se rangent en un carre parfait, deux parametres croises :
    //
    //              planche 0x0054f8f0     planche 0x0054f9e8
    //   +0x0a = 9      0x00412240             0x004156b0
    //   +0x0a = 2      0x00412720             0x00415c50
    //
    // Aucune autre difference sur les 77 lignes. Les deux planches sont celles des braises
    // (TickRisingEmber) : encore une fois le deuxieme enregistrement d'un bloc dont un troisieme
    // gestionnaire ne lit que le premier.
    // =====================================================================

    /// <summary>Ghidra: <c>0x00412240</c>.</summary>
    private static void EntityHandler_00412240(byte* e)
        => TickWideSpray(e, Descriptors0054F8E8() + 8, 9);   // Ghidra: &DAT_0054f8f0

    /// <summary>Ghidra: <c>0x00412720</c>.</summary>
    private static void EntityHandler_00412720(byte* e)
        => TickWideSpray(e, Descriptors0054F8E8() + 8, 2);

    /// <summary>Ghidra: <c>0x004156b0</c>.</summary>
    private static void EntityHandler_004156B0(byte* e)
        => TickWideSpray(e, Descriptors0054F9E0() + 8, 9);   // Ghidra: &DAT_0054f9e8

    /// <summary>Ghidra: <c>0x00415c50</c>.</summary>
    private static void EntityHandler_00415C50(byte* e)
        => TickWideSpray(e, Descriptors0054F9E0() + 8, 2);

    /// <summary>
    /// Le corps commun des quatre projections. Meme forme que
    /// <see cref="TickFootSplash"/> — une naissance qui disperse, puis un decompte — mais sur une
    /// bien plus grande etendue.
    ///
    /// <para>
    /// <b>Elle ne bouge pas ; tout se joue a la naissance.</b> Les deux echelles partent de
    /// <c>0xc00</c> plus un tirage sur <c>0x7ff</c>, donc toujours <b>plus grandes</b> que la
    /// taille nominale — l'inverse de l'eclaboussure du pas, qui n'est jamais plus grande. Le
    /// plan horizontal est disperse sur <c>[-150, +149]</c> unites entieres dans les deux axes, et
    /// la hauteur <b>seulement vers le haut</b>, sur <c>[0, +149]</c>.
    /// </para>
    /// <para>
    /// <b>Huit images de vie.</b> <c>+0x12</c> compte jusqu'a 2, se remet a zero et fait avancer
    /// l'indice d'atlas <c>+0x40</c> : quatre tuiles, deux images chacune. La comparaison de fin
    /// est <b>non signee</b> (<c>cmp al,4 ; jb</c>), donc l'entite s'eteint des que l'indice
    /// atteint 4 et non seulement s'il l'egale.
    /// </para>
    /// </summary>
    /// <param name="kind">La valeur de <c>+0x0a</c> : 9 ou 2 selon l'exemplaire. C'est le seul
    /// ecart entre les deux moities du carre.</param>
    private static void TickWideSpray(byte* e, byte* atlas, byte kind)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = atlas;
            e[0x40] = 0;

            *(short*)(e + 0x28) = (short)(SignedMask(CrtRand(), 0x7ff) + 0xc00);
            *(short*)(e + 0x2a) = (short)(SignedMask(CrtRand(), 0x7ff) + 0xc00);

            *(int*)(e + 0x1c) += (CrtRand() % 0x12c - 0x96) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0x12c - 0x96) << 0xc;

            int rise = CrtRand();
            e[0x0a] = kind;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x20) += (rise % 0x96) << 0xc;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) < 2) return;

        *(short*)(e + 0x12) = 0;
        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;

        if (frame >= 4) *(uint*)e &= 0x7fffffff;
    }


    // =====================================================================
    // L'INVOCATION QUI EMPORTE UN MEMBRE, EN QUATRE EXEMPLAIRES (2026-08-30)
    //
    // 0x00411f80, 0x00412460, 0x004153f0 et 0x00415990 sont le meme corps, absent de Blaze.c ;
    // releve au desassembleur, 230 instructions — Legacy/Raw/Blaze/FUN_00411F80.asm.
    //
    // Encore un carre parfait, deux parametres croises :
    //
    //                    memoire 0x00571448     memoire 0x00571478
    //   rythme lent          0x00411f80             0x004153f0
    //   rythme vif           0x00412460             0x00415990
    //
    // Le « rythme » n'est pas un seul nombre : il deplace ensemble le debut de la fanfare
    // (0x50 ou 0x3c), sa fin (0x82 ou 0x6e), la tenue d'image (4 ou 6) et les TROIS sous-types
    // semes (0xc1/0xf4/0xf5 ou 0xc2/0xf6/0xf7). Ce sont donc bien deux invocations distinctes,
    // pas une seule reglee deux fois — mais elles partagent leur code au dernier octet pres.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00571420</c> — <b>le bloc prive des scenes de la foret</b>, une suite
    /// d'enregistrements de <c>0x30</c> octets a partir de <c>0x00571420</c>. Zero au chargement :
    /// l'adresse tombe dans la queue non initialisee de <c>.data</c>, donc rien a relever, seule
    /// la taille compte.
    ///
    /// <para>
    /// <b>La forme se lit dans le code, pas dans la donnee.</b> Les cinq exemplaires portent les
    /// memes champs aux memes decalages — <c>+0x00</c>, <c>+0x02</c> et <c>+0x04</c> en
    /// <c>short</c>, puis <c>+0x08</c>, <c>+0x0c</c>, <c>+0x10</c>, <c>+0x18</c>, <c>+0x1c</c>,
    /// <c>+0x20</c> en <c>int</c>, et <c>+0x28</c> en octet. C'est ce dernier que
    /// <see cref="TickPartySummon"/> emploie comme memoire d'emplacement, et le triplet
    /// <c>+0x00</c> / <c>+0x08</c> / <c>+0x18</c> que les scenes cinema font pointer par
    /// <c>DAT_005e9ae0</c>, <c>DAT_005eca94</c> et <c>DAT_005edebc</c> le temps de la scene.
    /// </para>
    /// <para>
    /// <b>Une seule allocation pour tout le domaine, et deliberement.</b> Les deux memoires d'emplacement
    /// <c>0x00571448</c> et <c>0x00571478</c> avaient d'abord ete declarees comme deux octets
    /// isoles ; ce sont en realite le <c>+0x28</c> des exemplaires 0 et 1. Les laisser separes
    /// aurait pose deux noms pour deux adresses d'un meme bloc — exactement le defaut d'aliasing
    /// que <c>MemoryLayoutTests.cs</c> surveille — le jour ou les scenes cinema seront transcrites.
    /// </para>
    /// <para>
    /// <b>La taille est celle du domaine, pas celle d'un compte etabli.</b> L'allocation couvre
    /// <c>0x00571420</c> a <c>0x00571667</c> — jusqu'au voisin connu <c>DAT_00571668</c>, exclu.
    /// Elle valait <c>0x240</c> jusqu'au 2026-09-01, ce qui s'arretait <b>un mot trop tot</b> :
    /// <c>TickPartyRaft_00458200</c> ecrit un <c>int</c> a <c>0x00571660</c>, soit exactement le
    /// premier octet hors de l'ancienne etendue. Portee a <c>0x248</c>. Le pas
    /// de <c>0x30</c> se lit clairement sur les premiers enregistrements, mais <b>leur nombre
    /// n'est pas etabli</b> : le verbe <c>0x00443b20</c> ecrit encore a <c>0x0057150c</c> et
    /// <c>0x00571510</c>, et <see cref="EntityHandler_00458DE0"/> lit <c>0x00571654</c>, qu'aucune
    /// source decompilee ne nomme. Plutot que d'inventer un compte, tout ce qui vit dans ce
    /// domaine passe par <see cref="ForestSceneField"/>, adresse par adresse.
    /// </para>
    /// </summary>
    private static readonly byte* s_ForestSceneBlock_00571420 =
        (byte*)NativeMemory.AllocZeroed(0x248);

    /// <summary>Le <c>+0x28</c> du bloc de camera <paramref name="index"/> : le numero du membre du
    /// groupe que l'invocation a saisi. <c>0x00571448</c> pour 0, <c>0x00571478</c> pour 1.</summary>
    private static byte* CutsceneSlotMemo(int index) => ForestSceneRecord(index) + 0x28;

    /// <summary>La base de l'enregistrement <paramref name="index"/> du bloc : <c>0x00571420</c>
    /// pour 0, <c>0x00571450</c> pour 1. Les deux scenes cinema et les deux invocations ne
    /// different que par cet indice.</summary>
    private static byte* ForestSceneRecord(int index) => s_ForestSceneBlock_00571420 + index * 0x30;

    /// <summary>Un champ du bloc, par son adresse Ghidra. Tout ce qui vit entre <c>0x00571420</c>
    /// et <c>0x00571660</c> passe par ici, pour qu'une seule allocation porte l'ensemble.</summary>
    private static byte* ForestSceneField(int va) => s_ForestSceneBlock_00571420 + (va - 0x00571420);

    /// <summary>Ghidra: <c>0x00411f80</c> — rythme lent, memoire <c>0x00571448</c>.</summary>
    private static void EntityHandler_00411F80(byte* e)
        => TickPartySummon(e, CutsceneSlotMemo(0), 0x50, 0x82, 4, 0xc1);

    /// <summary>Ghidra: <c>0x004153f0</c> — rythme lent, memoire <c>0x00571478</c>.</summary>
    private static void EntityHandler_004153F0(byte* e)
        => TickPartySummon(e, CutsceneSlotMemo(1), 0x50, 0x82, 4, 0xc1);

    /// <summary>Ghidra: <c>0x00412460</c> — rythme vif, memoire <c>0x00571448</c>.</summary>
    private static void EntityHandler_00412460(byte* e)
        => TickPartySummon(e, CutsceneSlotMemo(0), 0x3c, 0x6e, 6, 0xc2);

    /// <summary>Ghidra: <c>0x00415990</c> — rythme vif, memoire <c>0x00571478</c>.</summary>
    private static void EntityHandler_00415990(byte* e)
        => TickPartySummon(e, CutsceneSlotMemo(1), 0x3c, 0x6e, 6, 0xc2);

    /// <summary>
    /// Le corps commun des quatre invocations.
    ///
    /// <para>
    /// <b>Elle saisit une entite de scene et la joue.</b> Chaque image, le corps balaie les
    /// trente-deux entites de <c>0x005ecaa0</c> et retient <b>la derniere</b> dont <c>+0x0c</c>
    /// est nul et qui porte <c>0x80000000</c> — la boucle du binaire ne s'arrete pas au premier
    /// trouve. De cette entite-la il recopie la position dans la sienne, lui pose la pose 2, lui
    /// allume le bit <c>0x1000</c>, et retient son modele d'animation par la table
    /// <c>0x005e9640</c>. C'est cette entite que le reste du corps anime.
    /// </para>
    /// <para>
    /// <b>Le bit est <c>0x1000</c>, pas <c>0x100000</c></b> : le binaire fait <c>or dh,10h</c>, et
    /// <c>dh</c> porte les bits 8 a 15. Lu <c>0x100000</c> jusqu'au 2026-09-26, il faisait passer le
    /// boss saisi sur le chemin « modele du paquet » de la grille : a sa mort, le modele 0 du paquet
    /// d'effets etait dessine a sa place, avec une palette qui n'est pas la sienne — le « rectangle
    /// arc-en-ciel » que Vincent a signale sur le Behemoth et le Grizzly.
    /// </para>
    /// <para>
    /// <b>Le compteur <c>+0x16</c> est une partition.</b> Il monte d'un par image, sans jamais
    /// redescendre, et chaque valeur declenche ce qui lui revient : la fanfare a
    /// <paramref name="start"/>, les trois notes a <c>start</c>, <c>+1</c> et <c>+2</c>, la gerbe
    /// pendant six images de <c>start</c> a <c>start+5</c>, et la fin a <paramref name="finish"/>.
    /// L'etat <c>+0x10</c>, lui, ne sert qu'a separer la montee du reste.
    /// </para>
    /// <para>
    /// <b>Le role est prete puis rendu</b>, comme dans <see cref="TickFivePhaseBurst"/> : les deux
    /// invocations de la fanfare ecrivent <c>+0x14</c> a 7 puis a 1 juste avant
    /// <see cref="SpawnCastEntity_0047AAE0"/>, qui recopie l'enregistrement en entier, puis le
    /// remettent a ce qu'il etait.
    /// </para>
    /// <para>
    /// <b>La fin relance le membre saisi.</b> A <paramref name="finish"/>, l'entite s'eteint, la
    /// camera reprend son mode, et <see cref="RunEntityScript"/> part sur la fiche
    /// <c>0x005e93c0 + n * 0x9c</c> ou <c>n</c> est le numero garde dans
    /// <paramref name="slotMemo"/> depuis la premiere image. L'entite de scene recoit alors
    /// <c>0x2004000</c>.
    /// </para>
    /// <para>
    /// <b>Un defaut de 1998, reproduit.</b> Si aucune entite de scene ne repond au balayage, le
    /// binaire garde dans <c>edi</c> un enregistrement local de <c>0x9c</c> octets mis a zero — ce
    /// qui est inoffensif — mais dans <c>ebp</c> les quatre octets de pile <b>situes juste sous ce
    /// tampon, que rien n'a ecrits</b>. Le corps lit ensuite <c>[ebp+0x16]</c> des que la tenue
    /// d'image est atteinte. Le pointeur de modele est donc laisse nul ici : l'acces fautif se
    /// produira au meme endroit qu'en 1998, en le nommant.
    /// </para>
    /// </summary>
    /// <param name="slotMemo">L'octet global ou le numero de membre est mis de cote —
    /// <c>0x00571448</c> ou <c>0x00571478</c> selon l'exemplaire.</param>
    /// <param name="start">La premiere image de la fanfare : <c>0x50</c> ou <c>0x3c</c>.</param>
    /// <param name="finish">L'image ou tout se termine : <c>0x82</c> ou <c>0x6e</c>.</param>
    /// <param name="hold">Le nombre d'images que tient une image d'animation : 4 ou 6.</param>
    /// <param name="castSubtype">Le sous-type de la fanfare, <c>0xc1</c> ou <c>0xc2</c> ; les deux
    /// autres sous-types semes s'en deduisent — <c>0xf4</c>/<c>0xf5</c> pour <c>0xc1</c>,
    /// <c>0xf6</c>/<c>0xf7</c> pour <c>0xc2</c>.</param>
    private static void TickPartySummon(
        byte* e, byte* slotMemo, short start, short finish, short hold, byte castSubtype)
    {
        byte trailSubtype = (byte)(castSubtype == 0xc1 ? 0xf4 : 0xf6);
        byte burstSubtype = (byte)(castSubtype == 0xc1 ? 0xf5 : 0xf7);

        // Le tampon nul du binaire : l'enregistrement de secours quand le balayage ne trouve rien.
        byte* fallback = stackalloc byte[0x9c];
        for (int i = 0; i < 0x9c; i++) fallback[i] = 0;

        MusicFadeStep_0041AF50();                  // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();            // Ghidra: FUN_0040e640

        // ------------------------------------------------- le balayage, sans arret au premier
        byte* target = fallback;

        // Ghidra: `ebp` est laisse non initialise si rien ne repond. Voir le resume.
        byte* model = null;

        byte* scene = Mem.Entity(0x005ecaa0);
        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 0) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            target = scene;
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            *(short*)(scene + 0x18) = 2;
            model = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 8);
            *(uint*)scene |= 0x1000;                // Ghidra: `or dh,10h` — le bit 0x1000, pas 0x100000
        }

        // ------------------------------------------------- la premiere image
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            byte slot = target[9];
            *(short*)(target + 0x1a) = 0;
            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;
            *slotMemo = slot;
            *(short*)(e + 0x98) = 0;
        }

        // ------------------------------------------------- la montee : le grondement et la trainee
        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x98))++;

            if (CrtRand() % 3 == 0 || *(short*)(e + 0x98) >= 0x14)
            {
                *(short*)(e + 0x98) = 0;
                Aud.PlaySoundEffect(0x0b, 0x3e8, 0x800, 0x800);
            }

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, trailSubtype, 0, -1);

            (*(short*)(e + 0x14))++;

            if (*(short*)(e + 0x14) >= hold)
            {
                *(short*)(e + 0x14) = 0;
                (*(short*)(target + 0x1a))++;

                // Ghidra lit le mot 32 bits de `eax` apres un `mov ax,[ebp+16h]` : seule la
                // moitie basse compte, et le `dec` la ramene sur la derniere image valide.
                short frames = *(short*)(model + 0x16);
                if (*(short*)(target + 0x1a) >= (ushort)frames)
                {
                    *(short*)(target + 0x1a) = (short)(frames - 1);
                }
            }
        }

        // ------------------------------------------------- la fanfare, une seule image
        if (*(short*)(e + 0x16) == start)
        {
            short saved = *(short*)(e + 0x14);

            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x14) = 7;
            SpawnCastEntity_0047AAE0(e, castSubtype, 0, -1);

            *(short*)(e + 0x14) = 1;
            SpawnCastEntity_0047AAE0(e, castSubtype, 0, -1);

            *(short*)(e + 0x14) = saved;

            *(uint*)target |= 0x4000;
            target[0x38] = 0;
            target[0x39] = 0;
            target[0x3a] = 0;
        }

        // ------------------------------------------------- la gerbe, six images durant
        short beat = *(short*)(e + 0x16);

        if (beat >= start && beat <= start + 5)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, burstSubtype, 0, -1);
            }
        }

        // ------------------------------------------------- les trois notes
        if (beat == start) Aud.PlaySoundEffect(0x2e, 0x1bf, 0x1000, 0x1000);
        if (beat == start + 1) Aud.PlaySoundEffect(0x2e, 0x1e9, 0x1000, 0x1000);
        if (beat == start + 2) Aud.PlaySoundEffect(0x2e, 0x200, 0x1000, 0x1000);

        // ------------------------------------------------- la fin
        if (beat >= finish)
        {
            *(uint*)e &= 0x7fffffff;
            CameraModeApply_005037E0(0);           // Ghidra: FUN_005037b0 -> FUN_005037e0

            byte* member = Mem.Arena(0x005e93c0) + *slotMemo * 0x9c;
            RunEntityScript(s_Unknown_005e97cc, member, 0);

            *(uint*)target |= 0x2004000;
        }

        (*(short*)(e + 0x16))++;
    }

    /// <summary>
    /// Ghidra: FUN_0041af50 @ 0x0041af50 — les deux compteurs du bloc de decoupe descendus de 300,
    /// bornes a zero, et le bloc republie. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>Le commentaire de le resume de EntityHandler_00404E50 la donnait pour « pas
    /// transcrite, rien ne l'appelle encore dans le graphe reconstruit ». Ce n'est plus vrai :
    /// <see cref="TickPartySummon"/> l'appelle en tete de chaque image.</para>
    /// </summary>
    private static void MusicFadeStep_0041AF50()
    {
        uint* block = s_MenuScreenBlock_00566c48;

        int left = *(short*)((byte*)block + 0x10) - 300;
        int right = *(short*)((byte*)block + 0x12) - 300;

        *block = 0xc0;

        if (left < 0) left = 0;
        if (right < 0) right = 0;

        *(short*)((byte*)block + 0x10) = (short)left;
        *(short*)((byte*)block + 0x12) = (short)right;

        Winsys3d.Ordinal_762(block);
    }


    // =====================================================================
    // L'ECLAT POSE SUR UNE CORDE D'ARC, EN TROIS EXEMPLAIRES (2026-08-30)
    //
    // 0x0040fbb0, 0x00412d10 et 0x00416300 sont le meme corps, absent de Blaze.c ; releve au
    // desassembleur, 520 instructions — Legacy/Raw/Blaze/FUN_0040FBB0.asm. Compares instruction
    // par instruction, les trois ne different que d'UNE ligne, l'adresse de la planche.
    //
    // C'est la plus grosse des lacunes du bloc 0x0040/0x0041 de la foret, et elle etait aussi la
    // plus regulière : quatre blocs de trente lignes quasi identiques, un par direction, suivis
    // d'une quinzaine de lignes de calcul. La lecon de FUN_004747F0 tient encore — ce qui coute,
    // c'est le nombre de FORMES distinctes, pas le nombre d'octets.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054f878</c> — la deuxieme copie de la planche de
    /// <see cref="s_Descriptors_0054F758"/>, memes quatre enregistrements, quatrieme tuile a
    /// hauteur 7 comprise.</summary>
    private static readonly byte[] s_Descriptors_0054F878 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f878
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f880
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f888
        0x10, 0x10, 0x08, 0x07, 0x01, 0x03, 0x10, 0x00,   // 0x0054f890
    ];

    private static byte* s_Descriptors0054F878Storage;

    private static byte* Descriptors0054F878()
        => NativeCopy(s_Descriptors_0054F878, ref s_Descriptors0054F878Storage);

    /// <summary>Ghidra: <c>&amp;DAT_0054f970</c> — la troisieme copie, meme contenu.</summary>
    private static readonly byte[] s_Descriptors_0054F970 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f970
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f978
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f980
        0x10, 0x10, 0x08, 0x07, 0x01, 0x03, 0x10, 0x00,   // 0x0054f988
    ];

    private static byte* s_Descriptors0054F970Storage;

    private static byte* Descriptors0054F970()
        => NativeCopy(s_Descriptors_0054F970, ref s_Descriptors0054F970Storage);

    /// <summary>Ghidra: <c>0x0040fbb0</c> — le premier des trois eclats de corde.</summary>
    private static void EntityHandler_0040FBB0(byte* e)
        => TickArcChordFlash(e, Descriptors0054F758());   // Ghidra: &DAT_0054f758

    /// <summary>Ghidra: <c>0x00412d10</c> — le deuxieme.</summary>
    private static void EntityHandler_00412D10(byte* e)
        => TickArcChordFlash(e, Descriptors0054F878());

    /// <summary>Ghidra: <c>0x00416300</c> — le troisieme.</summary>
    private static void EntityHandler_00416300(byte* e)
        => TickArcChordFlash(e, Descriptors0054F970());

    /// <summary>
    /// Ghidra: <c>0x00483920</c> — <b>le plus court gestionnaire du jeu</b> : trois instructions,
    /// <c>mov eax,[esp+4] ; mov dword [eax],0 ; ret</c>. Absent de <c>Blaze.c</c>. Table
    /// <c>0x0056b2c8</c>, foret, sous-zones 5 et 9.
    ///
    /// <para>
    /// Il efface le mot de drapeaux en entier, <c>0x80000000</c> compris — l'entite ne se marque
    /// donc pas seulement comme eteinte, elle perd <b>tout</b> son etat de drapeaux d'un coup, la
    /// ou les autres corps se contentent de <c>&amp;= 0x7fffffff</c>. Rien d'autre n'est touche :
    /// ni la position, ni le compteur, ni l'atlas.
    /// </para>
    /// <para>
    /// C'est la forme minimale d'un <b>emplacement bouche</b> : une entree de table qui existe
    /// pour que l'index soit valide, et dont le seul travail est de faire disparaitre ce qui
    /// l'atteint. Il n'y a rien de plus a en tirer, et il n'y a rien qui manque.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483920(byte* e) => *(uint*)e = 0;

    // =====================================================================
    // LE PANACHE QUI ENFLE ET S'ETEINT, EN DEUX EXEMPLAIRES (2026-08-30)
    //
    // 0x00415d40 et 0x004157a0 sont le meme corps, absents de Blaze.c ; releves au
    // desassembleur — Legacy/Raw/Blaze/FUN_00415D40.asm.
    //
    // Le diff normalise tient en UNE ligne, et ce n'est meme pas une adresse : 0x004157a0 ecrit
    // `mov [eax+0Bh],cl` DEUX FOIS de suite, la seconde n'ayant aucun effet. Le compilateur a emis
    // le meme magasin deux fois. C'est la premiere famille de la foret dont les membres ne
    // different par aucune donnee — un seul corps suffit pour les deux.
    // =====================================================================

    /// <summary>Ghidra: <c>0x00415d40</c> — le premier des deux panaches.</summary>
    private static void EntityHandler_00415D40(byte* e) => TickSwellingPlume(e);

    /// <summary>Ghidra: <c>0x004157a0</c> — le second, a un magasin redondant pres.</summary>
    private static void EntityHandler_004157A0(byte* e) => TickSwellingPlume(e);

    /// <summary>
    /// Le corps commun des deux panaches.
    ///
    /// <para>
    /// <b>Le champ <c>+0x14</c> choisit le modele, puis ne sert plus.</b> A la naissance il est
    /// recopie dans <c>+0x04</c> — le numero de modele — et sa valeur decide de <c>+0x0a</c> :
    /// <c>2</c> s'il valait 1, <c>9</c> sinon. Le meme <c>+0x04</c> est relu a chaque image pour
    /// choisir la vitesse d'enflure, <c>0x190</c> contre <c>0x320</c>. Un panache « 1 » est donc
    /// deux fois plus lent a grandir qu'un autre.
    /// </para>
    /// <para>
    /// <b>Il part des trois echelles a zero</b> — <c>+0x28</c>, <c>+0x2a</c> et <c>+0x2c</c>, donc
    /// invisible a sa premiere image — monte de <c>0x14000</c> d'un coup, et grandit ensuite d'un
    /// pas fixe sur les trois axes. Rien ne borne cette croissance : c'est la teinte qui met fin a
    /// l'affaire.
    /// </para>
    /// <para>
    /// <b>La teinte est le seul compteur.</b> Elle part de blanc plein (<c>0xff</c> sur les trois
    /// canaux) et perd <b>six</b> par image et par canal. Le binaire fait la soustraction sur des
    /// mots de 16 bits obtenus par <c>movzx</c>, puis teste le <b>rouge seul</b> : s'il est passe
    /// sous zero, les trois canaux sont remis a zero et l'entite s'eteint. Les trois canaux
    /// partant de la meme valeur et perdant le meme pas, tester le rouge suffit — mais c'est bien
    /// le rouge, pas un minimum.
    /// </para>
    /// <para>
    /// La vie est donc de <c>0xff / 6</c>, soit <b>quarante-trois images</b> exactement, quelle que
    /// soit la variante.
    /// </para>
    /// </summary>
    private static void TickSwellingPlume(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            byte model = e[0x14];
            bool slim = *(short*)(e + 0x14) == 1;

            *(short*)(e + 0x10) = 1;
            e[4] = model;
            e[0x0a] = (byte)(slim ? 2 : 9);
            e[0x0b] = 0;

            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x34) = 0;

            *(int*)(e + 0x20) += 0x14000;

            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;
        }

        if (*(short*)(e + 0x10) != 1) return;

        short step = (short)(e[4] == 1 ? 0x190 : 0x320);

        *(short*)(e + 0x28) += step;
        *(short*)(e + 0x2a) += step;
        *(short*)(e + 0x2c) += step;

        // Ghidra: les trois canaux passent par un `movzx` 16 bits avant la soustraction, donc le
        // resultat est signe et peut descendre sous zero — c'est ce que le test suivant guette.
        short red = (short)(e[0x38] - 6);
        short green = (short)(e[0x39] - 6);
        short blue = (short)(e[0x3a] - 6);

        if (red < 0)
        {
            red = 0;
            green = 0;
            blue = 0;
            *(uint*)e &= 0x7fffffff;
        }

        e[0x38] = (byte)red;
        e[0x39] = (byte)green;
        e[0x3a] = (byte)blue;
    }

    /// <summary>
    /// Ghidra: <c>0x0040f260</c> — <b>le halo accroche a son porteur</b>. Absent de <c>Blaze.c</c> ;
    /// releve au desassembleur, 82 instructions. Table <c>0x0056b2c8</c>, foret.
    ///
    /// <para>
    /// C'est le meme halo que la phase 3 de <see cref="TickFivePhaseBurst"/> — meme tuile
    /// <c>0x3f x 0x3f</c>, meme echelle <c>0x800</c>, meme <c>+0x0a = 7</c>, meme montee de trois
    /// par canal bornee a <c>0x3c</c>, puis meme battement en sinus. Deux differences :
    /// </para>
    /// <list type="bullet">
    /// <item>il <b>suit son porteur en hauteur</b> — <c>+0x6c</c>, relu a chaque image des deux
    /// etats, et sa hauteur recopiee telle quelle ; le porteur peut etre nul, et le binaire le
    /// teste ;</item>
    /// <item>sa planche de couleurs est une <b>constante</b>, <c>8</c> ecrit en dur dans l'octet 6
    /// de l'enregistrement, la ou la gerbe y met le <c>+0x96</c> de l'entite.</item>
    /// </list>
    /// <para>
    /// <b>Il meurt au temps, pas a la teinte.</b> <c>+0x16</c> monte d'un par image sans jamais
    /// etre remis a zero ; a <c>0xc8</c> l'entite s'eteint. La vie est donc de deux cents images
    /// exactement, dont les quarante-trois premieres en montee.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040F260(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            byte* atlas = Descriptors0054F7F8() + 8;     // Ghidra: &DAT_0054f800

            *(byte**)(e + 0x3c) = atlas;
            atlas[6] = 8;                                // Ghidra: DAT_0054f806 = 8

            *(short*)(e + 0x2e) = -0x1e;
            e[0x40] = 0;
            e[0x0a] = 7;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (carrier != null) *(int*)(e + 0x20) = *(int*)(carrier + 0x20);

            BrightenEntityTint_00404DE0(e, 3, 3, 3);

            if (e[0x38] >= 0x3c)
            {
                e[0x38] = 0x3c;
                e[0x39] = 0x3c;
                e[0x3a] = 0x3c;
            }

            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) >= 0x2b)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            if (carrier != null) *(int*)(e + 0x20) = *(int*)(carrier + 0x20);

            // Les trois canaux lisent le meme +0x12 : l'increment ne vient qu'apres les trois
            // appels, comme dans la phase 3 de la gerbe.
            byte beat = (byte)(((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12) << 8) >> 8) + 0x3c);

            e[0x38] = beat;
            e[0x39] = beat;
            e[0x3a] = beat;

            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x16) >= 0xc8) *(uint*)e &= 0x7fffffff;
        }

        (*(short*)(e + 0x16))++;
    }

    /// <summary>
    /// Ghidra: <c>0x00571654</c> — un <c>short</c> que le corps de
    /// <see cref="EntityHandler_00458DE0"/> compare a <c>-100</c>, et <b>que rien n'ecrit dans les
    /// sources decompilees</b>. Il n'apparait meme pas dans la liste des <c>DAT_</c> de
    /// <c>Blaze.c</c> : ses voisins connus sont <c>0x00571640</c> et <c>0x00571668</c>, il n'est
    /// donc rattachable a aucun bloc.
    ///
    /// <para>L'adresse tombe dans la queue non initialisee de <c>.data</c> : zero au chargement.
    /// Tant que rien ne l'ecrit, le test <c>&gt; -100</c> est donc toujours vrai et
    /// <see cref="EntityHandler_00458DE0"/> ne prend jamais sa seconde branche. Si un jour une
    /// fonction encore non transcrite y ecrit, ce sera visible ici.</para>
    /// </summary>
    private static short* Unknown_00571654() => (short*)ForestSceneField(0x00571654);

    /// <summary>
    /// Ghidra: <c>0x00458de0</c> — <b>le decor qui range sa pose et la reprend</b>. Absent de
    /// <c>Blaze.c</c> ; releve au desassembleur, 52 instructions. Table <c>0x0056adc8</c>, foret,
    /// sous-zones 6 a 8.
    ///
    /// <para>
    /// <b>La premiere image demenage six champs.</b> Les trois mots <c>+0x30</c>, <c>+0x32</c> et
    /// <c>+0x34</c> — que la sous-zone a remplis, et qui servent partout ailleurs d'orientation —
    /// sont recopies dans <c>+0x12</c>, <c>+0x14</c>, <c>+0x16</c>, puis <b>remis a zero</b>. Ce
    /// ne sont donc pas des angles ici mais une <b>position de rechange</b>, en unites entieres.
    /// En parallele, la position courante est rangee arrondie dans <c>+0x44</c>, <c>+0x48</c>,
    /// <c>+0x4c</c>.
    /// </para>
    /// <para>
    /// <b>Ensuite il choisit entre les deux, chaque image.</b> Si <c>0x00571654</c> depasse
    /// <c>-100</c>, la position revient de <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c> — c'est-a-dire
    /// la position d'origine <b>arrondie a l'unite entiere</b> par l'aller-retour de decalage.
    /// Sinon elle vient de la position de rechange.
    /// </para>
    /// <para>
    /// <b>Le binaire teste <c>0x00571654</c> deux fois de suite</b> sans que rien ne l'ait modifie
    /// entre-temps. C'est la queue commune d'un <c>if/else</c> que le compilateur a repliee ; le
    /// second test rend donc toujours le meme verdict que le premier, et la branche de rechange
    /// n'est atteinte que par le premier saut. Conserve tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00458DE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            short x = *(short*)(e + 0x30);
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x12) = x;

            short y = *(short*)(e + 0x32);
            *(short*)(e + 0x14) = y;

            short z = *(short*)(e + 0x34);
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x34) = 0;

            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x44) = *(int*)(e + 0x1c) >> 0xc;
            *(int*)(e + 0x48) = *(int*)(e + 0x20) >> 0xc;
            *(short*)(e + 0x16) = z;
            *(int*)(e + 0x4c) = *(int*)(e + 0x24) >> 0xc;
        }

        if (*(short*)(e + 0x10) != 1) return;

        if (*Unknown_00571654() > -100)
        {
            *(int*)(e + 0x1c) = *(int*)(e + 0x44) << 0xc;
            *(int*)(e + 0x20) = *(int*)(e + 0x48) << 0xc;
            *(int*)(e + 0x24) = *(int*)(e + 0x4c) << 0xc;

            // Le second test du binaire. Rien n'a touche la globale entre les deux.
            if (*Unknown_00571654() > -100) return;
        }

        *(int*)(e + 0x1c) = (int)*(short*)(e + 0x12) << 0xc;
        *(int*)(e + 0x20) = (int)*(short*)(e + 0x14) << 0xc;
        *(int*)(e + 0x24) = (int)*(short*)(e + 0x16) << 0xc;
    }

    /// <summary>
    /// Ghidra: <c>0x00459b20</c> — <b>la dalle qui s'enfonce sous le poids</b>. Absent de
    /// <c>Blaze.c</c> ; releve au desassembleur, 73 instructions. Table <c>0x0056adc8</c>, foret,
    /// sous-zones 6 a 8.
    ///
    /// <para>
    /// <b>Trois etats, et un seul aller-retour.</b> <c>+0x12</c> retient la hauteur de repos,
    /// prise a la premiere image et arrondie a l'unite entiere. Ensuite
    /// <see cref="CountPartyMembersMatching"/> compte les membres rattaches a la dalle
    /// (<c>0x10</c> exige, <c>0x40000000</c> interdit) : s'il y en a, l'etat passe a 2 — la dalle
    /// descend de <c>+0x14</c> unites ; s'il n'y en a plus, a 3 — elle remonte d'un coup. Les deux
    /// etats se remettent eux-memes a 1 apres avoir agi, de sorte que la bascule peut se refaire a
    /// chaque image.
    /// </para>
    /// <para>
    /// <b>Elle pose un drapeau de script tant qu'on est dessus.</b> L'indice est <c>+0x16</c>, le
    /// meme decoupage que <see cref="ScriptFlagSet"/> — un octet tous les huit indices dans la
    /// banque d'etat 0 — et il est <b>borne a 32 octets</b> : un indice au-dela de 255 ne pose
    /// rien du tout, silencieusement. Le drapeau n'est jamais retire.
    /// </para>
    /// <para>
    /// <b>Le son ne part qu'une fois par appui</b> : <c>+0x98</c> le retient, et c'est l'etat 3 —
    /// la remontee — qui le remet a zero. L'identifiant vient de la sous-zone,
    /// <c>sons[0x12]</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00459B20(byte* e) => TickPressurePlate_00459B20(e, 0x12, 0x400, 0x1000);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00459B20"/>. Sa copie de la grotte scellee,
    /// <c>0x0046F9D0</c> (sous-zones 6-7, 2026-09-19), n'en differe que par son son : la voix
    /// <c>0x25</c> de la zone, hauteur <c>0x200</c>, gain <c>0x2000</c> — ceux de la dalle de la
    /// grotte <see cref="EntityHandler_00468850"/>, qui fait la meme chose dans un autre ordre.
    /// </summary>
    private static void TickPressurePlate_00459B20(byte* e, int voice, short pitch, short gain)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x98) = 0;
        }

        int riders = CountPartyMembersMatching(e, 0x10, 0x40000000);

        if (riders != 0)
        {
            if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 2;
        }
        else if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x10) = 3;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[voice], pitch, gain, 0, 0);
                *(short*)(e + 0x98) = 1;
            }

            *(int*)(e + 0x20) = (*(short*)(e + 0x12) - *(short*)(e + 0x14)) << 0xc;
            *(short*)(e + 0x10) = 1;

            ScriptFlagRaise(*(short*)(e + 0x16));
        }

        if (*(short*)(e + 0x10) != 3) return;

        *(int*)(e + 0x20) = (int)*(short*)(e + 0x12) << 0xc;
        *(short*)(e + 0x98) = 0;
        *(short*)(e + 0x10) = 1;
    }

    /// <summary>
    /// Pose le drapeau de script <paramref name="index"/> dans la banque d'etat 0 — le pendant
    /// ecrivain de <see cref="ScriptFlagSet"/>.
    ///
    /// <para><b>Le garde a 32 octets est dans le binaire</b>, pas ajoute ici : au-dela de l'indice
    /// 255 le corps rend la main sans rien poser, et sans se plaindre. La banque fait bien
    /// <c>0x40</c> octets, donc la moitie haute reste hors d'atteinte par ce chemin.</para>
    /// </summary>
    private static void ScriptFlagRaise(short index)
    {
        int cell = DivideTowardZeroBy8(index);
        if (cell > 0x1f) return;

        int bit = (index - cell * 8) & 0x1f;
        Mem.s_StateBank0_005e96e0[cell] |= (byte)(1 << bit);
    }

    /// <summary>
    /// Ghidra: <c>0x00458e90</c> — <b>le decor qui flotte, et qui replonge quand on le lache</b>.
    /// Absent de <c>Blaze.c</c> ; releve au desassembleur, 101 instructions. Table
    /// <c>0x0056adc8</c>, foret, sous-zones 6 a 8.
    ///
    /// <para>
    /// <b>Le bit <c>0x40000000</c> partage le corps en deux moities qui ne se rencontrent
    /// jamais.</b> Pose, l'entite <b>flotte</b> ; efface, elle joue la <b>replongee</b> et se
    /// repose. Ce n'est donc pas un automate a etats mais deux comportements qu'un bit exterieur
    /// commute — c'est autre chose qui efface ce bit, pas ce corps-ci.
    /// </para>
    /// <para>
    /// <b>La flottaison.</b> La premiere image retient la hauteur de repos dans <c>+0x14</c>,
    /// arrondie a l'unite entiere, et pose la vitesse <c>+0x16</c> a <c>-1</c>. Ensuite la hauteur
    /// avance de <c>+0x16</c> unites par image, et la vitesse <b>s'inverse</b> des qu'on atteint
    /// le repos par le haut ou le repos moins vingt par le bas. Un aller-retour de quarante
    /// images, un pas d'une unite.
    /// </para>
    /// <para>
    /// <b>La replongee.</b> Elle commence par poser <c>0x10000000</c>, met <c>+0x12</c> a dix et
    /// joue <c>sons[0x18]</c>. Les dix images suivantes font <b>descendre</b> la hauteur de
    /// <c>+0x12</c> unites — donc de neuf, puis huit, puis sept : un enfoncement qui ralentit.
    /// Quand le compte est epuise, la hauteur <b>remonte</b> de <c>0x5000</c> par image jusqu'a
    /// depasser le repos, ou elle s'y colle net ; le bit <c>0x40000000</c> revient alors, la
    /// vitesse repart a <c>-1</c> et <c>+0x12</c> est charge a cent.
    /// </para>
    /// <para>
    /// <b>Deux bits differents dans la meme instruction, et ce n'est pas une faute de lecture.</b>
    /// La fin de la flottaison teste <c>0x10</c> (par <c>test al,10h</c>) et, s'il est absent,
    /// efface <c>0x10000000</c> (par <c>and eax,0efffffffh</c>). Le bit teste n'est pas celui qui
    /// est efface. Le desassemblage le confirme, et la transcription le garde tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00458E90(byte* e)
    {
        uint flags = *(uint*)e;

        if ((flags & 0x40000000) != 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x16) = -1;
                *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);
                *(short*)(e + 0x10) = 1;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                short speed = *(short*)(e + 0x16);
                int height = *(int*)(e + 0x20) + (speed << 0xc);
                *(int*)(e + 0x20) = height;

                int rest = *(short*)(e + 0x14) << 0xc;
                int floor = (*(short*)(e + 0x14) - 20) << 0xc;

                if (height >= rest || height <= floor) *(short*)(e + 0x16) = (short)-speed;
            }

            // Ghidra: `test al,10h` puis `and eax,0efffffffh`. Deux bits differents. Voir le resume.
            if ((flags & 0x10) != 0) return;
            *(uint*)e = flags & 0xefffffff;
            return;
        }

        short state = *(short*)(e + 0x10);

        if (state == 1 || state == 0)
        {
            flags |= 0x10000000;
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 10;
            *(uint*)e = flags;

            TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x18], 0x400, 0x1000, 0, 0);
        }

        if (*(short*)(e + 0x10) != 2) return;

        short remaining = *(short*)(e + 0x12);

        if (remaining > 0)
        {
            remaining--;
            *(short*)(e + 0x12) = remaining;
            *(int*)(e + 0x20) -= remaining << 0xc;
            return;
        }

        int rising = *(int*)(e + 0x20) + 0x5000;
        *(int*)(e + 0x20) = rising;

        int home = *(short*)(e + 0x14) << 0xc;
        if (home > rising) return;

        *(int*)(e + 0x20) = home;
        *(uint*)e |= 0x40000000;
        *(short*)(e + 0x10) = 1;
        *(short*)(e + 0x16) = -1;
        *(short*)(e + 0x12) = 100;
    }
}
