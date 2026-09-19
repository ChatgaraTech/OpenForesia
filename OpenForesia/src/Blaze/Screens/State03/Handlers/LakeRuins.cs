// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DES RUINES DANS LE LAC — zone 9, sous-zones 0 et 1, table 0x0056adc8.
//
// Ouvert le 2026-09-19 avec la zone. Cinq corps propres a la zone :
//
//   0x00467C10   le clapotis a sept coups, sur une horloge tiree de 20 a 79 images
//   0x00467DB0   le son d'ambiance a une seule note, a volume tire
//   0x00467E80   le poisson qui saute une fois, et que la banque 0 se rappelle
//   0x004681A0   l'armement des cinq piliers
//   0x00468210   le pilier qui bascule, un par valeur de +0x14
//
// LES CINQ PILIERS SE PARLENT PAR UN SEUL MOT, 0x005717c8 (EntityState, remis a zero au chargement
// par Init_00467c00). Ses bits 0x01 a 0x10 disent « le pilier n est arme », ses bits 0x20 a 0x200
// « le pilier n est tombe ». Le premier jeu est pose par 0x004681A0, le second par 0x00468210
// lui-meme a la fin de sa chute. Un pilier dont le bit « tombe » est deja pose se remet
// directement a sa place d'arrivee.
//
// Aucun de ces corps ne consulte le groupe pour decider : le pilier frappe le groupe a l'impact
// (StrikePartyMembers_0045DA40), mais « abimer n'est pas decider » (ARCHITECTURE.md). Pas de
// fichier de mecanismes pour la zone.
//
// Le son d'une zone se lit par `ZoneSound` (`Mine.cs`) : meme classe partielle.
// Dumps : Legacy/Raw/Blaze/LakeRuins/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>Ghidra: <c>DAT_005717c8</c> — <b>le mot des cinq piliers</b>, voir le bandeau.</summary>
    private static int* LakePillarFlags => Mem.EntityState(0x005717c8);

    // =====================================================================
    // LES DEUX SONS D'AMBIANCE
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00467c10 @ 0x00467c10 — creneau de la table <c>0x0056adc8</c>, sous-zones 0 et
    /// 1. Decompilee dans <c>Blaze.c</c>, 96 instructions, une table de sauts.
    ///
    /// <para>
    /// <b>Sept coups de la voix <c>0x10</c> de la zone, a huit images d'intervalle</b>, puis un
    /// silence tire de 20 a 79 images. Le volume de base <c>+0x14</c> est tire a chaque salve
    /// (0 a <c>0x3ff</c>) et chaque coup y ajoute sa part, en cloche :
    /// <c>+0, +0x100, +0x200, +0x400, +0x200, +0x100, +0</c> aux images 0 a 48.
    /// </para>
    /// <para>
    /// L'etat 1 ne dure qu'une image : il tire le silence et passe a 2 dans le meme appel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00467C10(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int draw = CrtRand();
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x12) = (short)(draw % 0x3c + 0x14);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x14))++;
            if (*(short*)(e + 0x12) == *(short*)(e + 0x14))
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = (short)SignedMask(CrtRand(), 0x3ff);
            }
        }

        if (*(short*)(e + 0x10) != 3) return;

        int gain;
        bool strike = true;
        switch (*(short*)(e + 0x12))
        {
            case 0:
            case 0x30: gain = *(short*)(e + 0x14); break;
            case 0x08: gain = *(short*)(e + 0x14) + 0x100; break;
            case 0x10: gain = *(short*)(e + 0x14) + 0x200; break;
            case 0x18: gain = *(short*)(e + 0x14) + 0x400; break;
            case 0x20: gain = *(short*)(e + 0x14) + 0x200; break;
            case 0x28: gain = *(short*)(e + 0x14) + 0x100; break;
            default: gain = 0; strike = false; break;
        }

        if (strike) Aud.PlaySoundEffect(ZoneSound(0x10), 0x400, gain, gain);   // Ghidra: FUN_0053cea0

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) == 0x31) *(short*)(e + 0x10) = 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00467db0</c> — creneau de la table <c>0x0056adc8</c>, sous-zones 0 et 1.
    /// <b>Absent de <c>Blaze.c</c></b> ; 61 instructions.
    ///
    /// <para>
    /// <b>Une note, puis un silence de 80 a 239 images</b>. La note est la voix <c>0x11</c> de la
    /// zone, hauteur <c>0x300</c>, a un volume tire a chaque cycle : <c>0x300</c> plus 0 a
    /// <c>0x3ff</c>. L'etat 3 ne dure qu'une image — il joue quand <c>+0x12</c> vaut zero,
    /// l'incremente, et revient a l'etat 1 des qu'il vaut un.
    /// </para>
    /// </summary>
    private static void EntityHandler_00467DB0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int draw = CrtRand();
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x12) = (short)(draw % 0xa0 + 0x50);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x14))++;
            if (*(short*)(e + 0x12) == *(short*)(e + 0x14))
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = (short)SignedMask(CrtRand(), 0x3ff);
            }
        }

        if (*(short*)(e + 0x10) != 3) return;

        if (*(short*)(e + 0x12) == 0)
        {
            int gain = *(short*)(e + 0x14) + 0x300;
            Aud.PlaySoundEffect(ZoneSound(0x11), 0x300, gain, gain);   // Ghidra: FUN_0053cea0
        }

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) == 1) *(short*)(e + 0x10) = 1;
    }

    // =====================================================================
    // LE POISSON QUI SAUTE
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00467e80 @ 0x00467e80 — creneau de la table <c>0x0056adc8</c>, sous-zones 0 et
    /// 1. Decompilee dans <c>Blaze.c</c>, 156 instructions.
    ///
    /// <para>
    /// <b>Un saut, une seule fois par partie.</b> A la naissance, <c>+0x14</c> designe un bit de la
    /// <b>banque 0</b> (<c>0x005e96e0</c>, octet <c>n / 8</c>, bit <c>n % 8</c>) ; le corps le
    /// pose, puis remet <c>+0x14</c> a zero pour s'en servir d'angle. Une entite qui porte deja
    /// <c>0x40000000</c> ne fait que relire ce bit — et s'eteint s'il est pose.
    /// </para>
    /// <list type="table">
    ///   <item><term>etat 1</term><description>l'attente : de 11 a 29 images, un tirage sur vingt
    ///     par image, 30 au plus.</description></item>
    ///   <item><term>le depart</term><description>un angle de roulis (-60..59) et de lacet
    ///     (-25..24) par image, le son <c>0x13</c> de la zone, et 6 a 10 gerbes <c>0x31</c>
    ///     pres de la camera.</description></item>
    ///   <item><term>etat 2</term><description>la course : la hauteur perd <c>+0x12</c> unites
    ///     par image, et <c>+0x12</c> part de 5 et grandit de deux — un mouvement qui accelere
    ///     toujours dans le meme sens, en tournant (<c>+0x34</c>, <c>+0x32</c>). Passe sous zero,
    ///     il prend la teinte <c>(0x50, 0x14, 0x96)</c>, le bit <c>0x2000</c>, et 6 a 10 gerbes
    ///     <c>0x32</c> une fois. Sous -500 il s'eteint.</description></item>
    /// </list>
    /// <para>
    /// <b>L'octet et le bit sont calcules en division C</b>, arrondie vers zero : un <c>+0x14</c>
    /// negatif donnerait un decalage hors de 0..7, masque a cinq bits. Aucune fiche de placement ne
    /// le fait, mais c'est le calcul du binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00467E80(byte* e)
    {
        if ((*(uint*)e & 0x40000000) != 0)
        {
            if ((Mem.s_StateBank0_005e96e0[LakeBankByte(e)] & LakeBankBit(e)) != 0)
                *(uint*)e &= 0x7fffffff;
            return;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
            Mem.s_StateBank0_005e96e0[LakeBankByte(e)] |= LakeBankBit(e);
            *(short*)(e + 0x14) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int draw = CrtRand();
            (*(short*)(e + 0x12))++;

            if ((draw % 0x14 == 1 && *(short*)(e + 0x12) > 10) || *(short*)(e + 0x12) == 0x1e)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 5;
                *(short*)(e + 0x14) = (short)(CrtRand() % 0x78 - 0x3c);
                *(short*)(e + 0x16) = (short)(CrtRand() % 0x32 - 0x19);

                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x13), 0x400, 0x1800, 0, 0);   // Ghidra: FUN_00468080

                short sprays = (short)(CrtRand() % 5 + 5);
                for (int i = 0; i <= sprays; i++)
                    SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);            // Ghidra: FUN_00468110
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x34) += *(short*)(e + 0x14);
        int height = *(int*)(e + 0x20) - *(short*)(e + 0x12) * 0x1000;
        *(short*)(e + 0x32) += *(short*)(e + 0x16);
        *(short*)(e + 0x12) += 2;
        *(int*)(e + 0x20) = height;

        if (height < 0)
        {
            e[0x38] = 0x50;
            e[0x39] = 0x14;
            e[0x3a] = 0x96;
            *(uint*)e |= 0x2000;

            if (*(int*)(e + 0x5c) == 0)
            {
                *(int*)(e + 0x5c) = 1;
                short sprays = (short)(CrtRand() % 5 + 5);
                for (int i = 0; i <= sprays; i++)
                    SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);
            }
        }

        if (*(int*)(e + 0x20) < -0x1f4000) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>L'octet de la banque 0 que designe <c>+0x14</c> : <c>n / 8</c>, arrondi vers zero
    /// (Ghidra : <c>(n + ((n &gt;&gt; 31) &amp; 7)) &gt;&gt; 3</c>), puis tronque a seize bits.</summary>
    private static int LakeBankByte(byte* e) => (short)(*(short*)(e + 0x14) / 8);

    /// <summary>Le bit correspondant : <c>1 &lt;&lt; ((n - octet * 8) &amp; 0x1f)</c>, sur un
    /// octet.</summary>
    private static byte LakeBankBit(byte* e)
    {
        short n = *(short*)(e + 0x14);
        int quotient = n / 8;
        return (byte)(1 << ((byte)((byte)n - (byte)(quotient * 8)) & 0x1f));
    }

    // =====================================================================
    // LES CINQ PILIERS
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004681a0</c> — creneau de la table <c>0x0056adc8</c>, sous-zones 0 et 1.
    /// <b>Absent de <c>Blaze.c</c></b> ; 24 instructions.
    ///
    /// <para>
    /// <b>L'armement des piliers.</b> Tant que l'entite ne porte pas <c>0x40000000</c>, elle pose
    /// dans <see cref="LakePillarFlags"/> le bit du pilier que designe son <c>+0x14</c> — 1 a 5
    /// donnent <c>0x01</c>, <c>0x02</c>, <c>0x04</c>, <c>0x08</c>, <c>0x10</c> —, a chaque image.
    /// Une autre valeur ne pose rien. Les cinq tests relisent <c>+0x14</c> chacun.
    /// </para>
    /// </summary>
    private static void EntityHandler_004681A0(byte* e)
    {
        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x14) == 1) *LakePillarFlags |= 0x01;
        if (*(short*)(e + 0x14) == 2) *LakePillarFlags |= 0x02;
        if (*(short*)(e + 0x14) == 3) *LakePillarFlags |= 0x04;
        if (*(short*)(e + 0x14) == 4) *LakePillarFlags |= 0x08;
        if (*(short*)(e + 0x14) == 5) *LakePillarFlags |= 0x10;
    }

    /// <summary>
    /// Ghidra: <c>0x00468210</c> — creneau de la table <c>0x0056adc8</c>, sous-zones 0 et 1.
    /// <b>Absent de <c>Blaze.c</c></b> ; 248 instructions, table de sauts a cinq entrees a
    /// <c>0x00468558</c>.
    ///
    /// <para>
    /// <b>Le pilier qui bascule</b>, un par valeur de <c>+0x14</c> (1 a 5). Tant que son bit
    /// « arme » n'est pas pose dans <see cref="LakePillarFlags"/>, il ne fait rien. Si son bit
    /// « tombe » l'est deja, il se pose directement a sa place d'arrivee — une position fixe par
    /// pilier, inscrite dans le corps, et une inclinaison <c>+0x34 = 0x464</c> — et s'arrete la.
    /// </para>
    /// <list type="table">
    ///   <item><term>etat 1</term><description>douze images de tremblement de camera (sans
    ///     amplitude : <c>(999, 0, 999)</c>), pendant lesquelles il descend d'une unite, glisse de
    ///     trois dans son cap, et s'incline de plus en plus vite ; le son <c>0x14</c> de la zone
    ///     part a la premiere.</description></item>
    ///   <item><term>etat 2</term><description>la chute : l'inclinaison accelere de deux par
    ///     image ; a <c>0x464</c>, l'impact — le son <c>0x12</c>, <b>dix pour cent de degats</b> au
    ///     groupe (<see cref="StrikePartyMembers_0045DA40"/>), trente et une gerbes <c>0x30</c>,
    ///     et une secousse de 8. La hauteur ne descend pas sous <c>+0x16</c>.</description></item>
    ///   <item><term>etat 3</term><description>la secousse s'amortit en changeant de signe ;
    ///     a zero, le bit « tombe » est pose et l'etat passe a 4, ou plus rien ne
    ///     bouge.</description></item>
    /// </list>
    /// <para>
    /// <b>Le tremblement de l'etat 1, et l'increment de <c>+0x12</c> qui le suit, partent a chaque
    /// image de TOUS les etats</b> : ils sont avant les tests d'etat, derriere le seul test des
    /// bits. En etat 3, <c>+0x12</c> est l'amplitude de la secousse : chaque image l'augmente
    /// d'un, puis l'amortit d'un vers zero, puis en change le signe. Partie de 8 a l'impact, elle
    /// publie -7, 5, -5, 3, -3, 1, -1, puis 0 en passant a l'etat 4. En etat 4 <c>+0x12</c> grimpe
    /// sans fin, sans effet.
    /// </para>
    /// </summary>
    private static void EntityHandler_00468210(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 1;
            *(short*)(e + 0x9a) = 0;
        }

        short pillar = *(short*)(e + 0x14);
        int flags = *LakePillarFlags;

        // Le bit « arme ».
        if (pillar == 1 && (flags & 0x01) == 0) return;
        if (pillar == 2 && (flags & 0x02) == 0) return;
        if (pillar == 3 && (flags & 0x04) == 0) return;
        if (pillar == 4 && (flags & 0x08) == 0) return;
        if (pillar == 5 && (flags & 0x10) == 0) return;

        // Le bit « tombe » : directement a la place d'arrivee.
        if (pillar == 1 && (flags & 0x20) != 0) { PlaceFallenPillar(e, unchecked((int)0xffb24000), 0x0f9000, unchecked((int)0xff63c000)); return; }
        if (pillar == 2 && (flags & 0x40) != 0) { PlaceFallenPillar(e, unchecked((int)0xffbec000), 0x0f9000, unchecked((int)0xff574000)); return; }
        if (pillar == 3 && (flags & 0x80) != 0) { PlaceFallenPillar(e, unchecked((int)0xffb24000), 0x0f9000, 0x1f4000); return; }
        if (pillar == 4 && (flags & 0x100) != 0) { PlaceFallenPillar(e, unchecked((int)0xfff0c000), 0x1c1000, 0x5dc000); return; }
        if (pillar == 5 && (flags & 0x200) != 0) { PlaceFallenPillar(e, 0x0f4000, 0x1c1000, 0xc1c000); return; }

        TryPublishCameraEffectNearEntity((uint*)e, 999, 0, 999, 0, 0);   // Ghidra: FUN_0041be60

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) >= 0x0d && *(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 2;

        // ---------------------------------------------------------------- 1 : il se descelle
        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x9a) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x14), 0x400, 0x1800, 0, 0);   // Ghidra: FUN_00468080
                *(short*)(e + 0x9a) = 1;
            }

            *(int*)(e + 0x20) += -0x1000;
            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 3;   // Ghidra: FUN_00545a1c
            *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * 3;        // Ghidra: FUN_00545a16

            (*(int*)(e + 0x5c))++;
            *(short*)(e + 0x34) += (short)*(int*)(e + 0x5c);
        }

        // ---------------------------------------------------------------- 2 : il tombe
        if (*(short*)(e + 0x10) == 2)
        {
            *(int*)(e + 0x5c) += 2;
            *(short*)(e + 0x34) += (short)*(int*)(e + 0x5c);

            if (*(short*)(e + 0x34) >= 0x464)
            {
                *(short*)(e + 0x34) = 0x464;
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 8;

                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x12), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
                StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);                              // Ghidra: FUN_0045da40

                for (int i = 0x1f; i != 0; i--)
                    SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);                       // Ghidra: FUN_00468110
            }

            int floor = *(short*)(e + 0x16) << 0xc;
            if (*(int*)(e + 0x20) <= floor) *(int*)(e + 0x20) = floor;
        }

        // ---------------------------------------------------------------- 3 : la secousse
        if (*(short*)(e + 0x10) != 3) return;

        if (*(short*)(e + 0x12) > 0) (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < 0) (*(short*)(e + 0x12))++;

        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x10) = 4;
            switch (*(short*)(e + 0x14) - 1)
            {
                case 0: *LakePillarFlags |= 0x020; break;
                case 1: *LakePillarFlags |= 0x040; break;
                case 2: *LakePillarFlags |= 0x080; break;
                case 3: *LakePillarFlags |= 0x100; break;
                case 4: *LakePillarFlags |= 0x200; break;
            }
        }

        short jolt = (short)-*(short*)(e + 0x12);
        *(short*)(e + 0x12) = jolt;
        TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);   // Ghidra: FUN_0041be60
    }

    /// <summary>La place d'arrivee d'un pilier deja tombe : trois coordonnees inscrites dans le
    /// corps de <see cref="EntityHandler_00468210"/>, et l'inclinaison finale <c>0x464</c>.</summary>
    private static void PlaceFallenPillar(byte* e, int x, int y, int z)
    {
        *(int*)(e + 0x1c) = x;
        *(int*)(e + 0x20) = y;
        *(int*)(e + 0x24) = z;
        *(short*)(e + 0x34) = 0x464;
    }
}
