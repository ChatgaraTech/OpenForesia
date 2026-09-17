// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE MORT-VIVANT — 0x004BB860 (sous-zones 0, 1, 2, 3) et 0x004BD170 (sous-zones 5, 6, 7, 8),
// Labyrinthe des morts (zone 5).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004BB860 — LE MORT-VIVANT. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004BB860.asm, 970 instructions, absente de Blaze.c), table de
    // sauts a DIX entrees a 0x004BC508.
    //
    // UN VOLANT DE LA FAMILLE DU SORCIER (Ai/OldTower/Sorcerer.cs) — meme regime 0x20 pose a la
    // premiere image par la variante, meme flottement au huitieme vers 0x8c unites au-dessus de sa
    // reference, meme integration sans rappel vertical, meme pose au sol au masque 0x2000a882 —
    // mais une bete qui ne lance rien et ne frappe qu'en plongeant :
    //
    //   - IL FRAPPE A CHAQUE IMAGE. La balayeuse de corps a corps n'est gardee par AUCUNE condition
    //     d'image : tant que le bit 2 est pose, chaque appel cherche une victime. C'est le plongeon
    //     (posture 8) qui pose ce bit et qui le retire quand sa poussee retombe sous 0x7000 ;
    //   - SON PLONGEON DESCEND VERS LA PROIE. En regime 0x20, posture 8 et bit 2 retombe, le
    //     flottement est remplace par une descente : la hauteur rejoint d'un huitieme le plancher de
    //     la proie plus 0x28 unites, tant que le plafond du Mort-vivant reste au-dessus d'elle ;
    //   - DEUX PAS DE PHASE, un par regime : hasard % 100 + 100 en regime 0x20, hasard % 50 + 50
    //     sinon. Le Sorcier n'en a qu'un ;
    //   - SA POSTURE 3 EST UNE ONDULATION QUI S'ETEINT : l'amplitude de +0x14 perd un huitieme par
    //     image et la posture finit sous 5. Le meme mot +0x14 sert de phase au flottement — le corps
    //     l'ecrit des deux cotes, et c'est transcrit tel quel ;
    //   - IL FAIT NAITRE DEUX CHOSES A MI-DISTANCE, sur des tranches de tirage voisines : 0xde sur
    //     19 a 24 (vitesses mises a zero et +0x54 arme a 0x3c000 le temps de la naissance, +0x9a
    //     laisse a 2), et 0xdd sur 27, apres s'etre note la proie dans +0x78.
    //
    // 0x004BD170 est le meme corps : hors cibles de saut, deux decalages echanges dans un seul bloc
    // arithmetique (`sar eax,3` / `sar eax,8`), c'est-a-dire le meme calcul sous un autre ordre de
    // registres.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004bb860</c> (et <c>0x004bd170</c>) — <b>l'intelligence du Mort-vivant</b>. Voir
    /// le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004BB860(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x28] — la sonde du Sanguinaire.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x50;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004b9220, copie de FUN_004a1020
            *(int*)(e + 0x48) = *(int*)(e + 0x20);

            // Ghidra: 0x004bb8b6 — la variante au-dela de 10 retombe de 10 et perd le regime 0x20.
            if (*(short*)(e + 0x16) >= 10)
            {
                *(short*)(e + 0x16) -= 10;
                *(uint*)e &= 0xffffffdf;
            }
            else
            {
                *(uint*)e |= 0x20;
            }
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // Ghidra: 0x004bb989 — ni CancelGestureOnTrait_004D5DE0 ni recalcul de distance : le
        // Mort-vivant garde celle que la lecture de situation lui a rendue.
        if (speed != 0)
        {
            DecideUndeadAction_004BB995(e, probe, boldness, distance, heading, speed);
            FloatUndead_004BBE28(e, chased);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);             // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        uint flags = *(uint*)e;
        uint contact = *(uint*)(e + 0x90) & 0xfffff8ff;
        *(uint*)(e + 0x90) = contact;
        if ((flags & 0x20) != 0) *(uint*)(e + 0x90) = contact & 0xdfffdfff;

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (*(uint*)(e + 0x90) & 0x8000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((*(uint*)(e + 0x90) & 0x2000a882) != 0)
        {
            // ================================================================ heurte
            if (e[0x42] != 7 && e[0x42] != 0x0a)
            {
                e[0x42] = 7;
                int roll = CrtRand();
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(roll % 0x28 + 0x14);
            }

            *(uint*)e &= 0xfffffffd;

            *(int*)(e + 0x24) = keep.Z;
            *(int*)(e + 0x1c) = keep.X;
            *(int*)(e + 0x7c) = keep.At7c;
            *(uint*)(e + 0x90) = keep.At90;                         // `and eax,0FFFFh` puis le dword
            *(int*)(e + 0x80) = keep.At80;
            *(int*)(e + 0x84) = keep.At84;
            *(int*)(e + 0x88) = keep.At88;
            *(int*)(e + 0x8c) = keep.At8c;
            *(int*)(e + 0x64) = keep.At64;
            *(int*)(e + 0x60) = keep.At60;

            int side = ProbeClearSide_004D5E60(e, 0x2000a882);
            if ((short)side == 0) side = SignedMask(CrtRand(), 1) * 2 - 1;

            *(short*)(e + 0x32) += (short)((boldness + 0x40) * side);
        }

        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanUndeadAction_004BC106(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004bb995..0x004bbe27</c> — <b>le premier temps</b> du Mort-vivant. Dix cases,
    /// dont sept servies : 0 attendre, 1 se tourner, 2 approcher, 3 onduler, 7 marcher droit,
    /// 8 plonger, 9 virer doucement. Les cases 4, 5 et 6 rendent la main.
    ///
    /// <para><b>Le coup n'a pas d'image</b> : la balayeuse part a chaque appel ou le bit 2 est
    /// pose.</para>
    /// </summary>
    private static void DecideUndeadAction_004BB995(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ le coup, sans image
        if ((e[0] & 2) != 0)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x10;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
            }
        }

        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = 0;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] == 0)
                {
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004bbe25
                    return;
                }

                e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                }

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004bbe22
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004bbe25
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 2;
                    *(short*)(e + 0x1a) = 1;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0xa000;
                }

                if (*(short*)(e + 0x12) != 1) return;

                // Ghidra: 0x004bbb60 — l'image 1 de l'animation 2 est reposee a chaque appel.
                *(short*)(e + 0x1a) = 1;
                *(short*)(e + 0x18) = 2;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x80
                    : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : onduler
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    *(short*)(e + 0x18) = 1;
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = (boldness + 0x70) << 8;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }

                    // Ghidra: 0x004bbc74 — l'amplitude perd un huitieme par image.
                    short swing = *(short*)(e + 0x14);
                    swing -= (short)(swing >> 3);
                    *(short*)(e + 0x14) = swing;

                    if (swing < 5)
                    {
                        e[0x43] = 0;
                        *(ushort*)(e + 0x2c) = 0x1000;
                    }

                    *(ushort*)(e + 0x2c) = (ushort)(swing + 0x1000);
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x14) = (short)((CrtRand() % 0x14 + boldness + 0x0a) << 4);
                return;

            // ---------------------------------------------------------- 7 : marcher droit
            case 7:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    if (distance < *(short*)(e + 0x10)) e[0x43] = 0;
                    else if (e[0x43] != 0) e[0x43]--;
                }

                if (phase != 0) return;

                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = (boldness >> 1) + 0x4000;
                return;
            }

            // ---------------------------------------------------------- 8 : plonger
            case 8:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    // Ghidra: 0x004bbd45 — la poussee perd un SEIZIEME par image ; sous 0x7000 le
                    // plongeon rend la main ET lache le bit de coup.
                    int thrust = *(int*)(e + 0x5c);
                    *(short*)(e + 0x18) = 0;
                    thrust -= thrust >> 4;
                    *(int*)(e + 0x5c) = thrust;

                    if (thrust < 0x7000)
                    {
                        e[0x43] = 0;
                        *(uint*)e &= 0xfffffffd;
                        *(int*)(e + 0x5c) = 0;
                        *(ushort*)(e + 0x2c) = 0x1000;
                    }

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x80
                            : -0x80 - boldness * 2);
                    }

                    *(ushort*)(e + 0x2c) = (ushort)((*(int*)(e + 0x5c) >> 5) + 0x1000);
                }

                if (phase != 0) return;

                *(short*)(e + 0x18) = 0;
                *(uint*)e |= 2;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;

                // Ghidra: 0x004bbde1 — `and al,0FCh` ne masque que l'octet bas.
                *(int*)(e + 0x5c) = ((boldness & 0xfc) + (CrtRand() % 0x32) * 4) << 0xa;
                return;
            }

            // ---------------------------------------------------------- 9 : virer doucement
            case 9:
            {
                short target = *(short*)(e + 0x10);
                short course = *(short*)(e + 0x32);

                course = (short)(course + ((target - course) >> 3));

                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x32) = course;

                int gap = target - course >= 0 ? target - course : course - target;

                if (gap < 0x32) e[0x43] = 0;                        // Ghidra: LAB_004bbe22
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004bbe25
                return;
            }

            // ---------------------------------------------------------- 4, 5, 6 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004bbe28..0x004bbf13</c> — <b>le flottement</b> du Mort-vivant, celui du Sorcier
    /// a deux details pres : le pas de phase differe d'un regime a l'autre, et le plongeon ne se
    /// contente pas de retenir la hauteur, il la fait DESCENDRE vers la proie.
    /// </summary>
    private static void FloatUndead_004BBE28(byte* e, byte* chased)
    {
        uint flags = *(uint*)e;
        byte posture = e[0x42];

        if ((flags & 0x20) != 0)
        {
            if (posture == 8 && (flags & 2) == 0)
            {
                // Ghidra: 0x004bbe3e — la descente du plongeon.
                int y = *(int*)(e + 0x20);

                if (y + 0x8c000 > *(int*)(chased + 0x20))
                {
                    *(int*)(e + 0x20) = ((*(int*)(chased + 0x60) - y + 0x28000) >> 3) + y;
                }
            }
            else
            {
                short phase = AdvanceUndeadBob(e, 0x64);
                int y = *(int*)(e + 0x20);
                int wave = (int)Winsys3d.GetSinTableEntry(phase) * 60;
                *(int*)(e + 0x20) = ((*(int*)(e + 0x48) + wave - y + 0x8c000) >> 3) + y;
            }

            int floor = *(int*)(chased + 0x64);
            if (((*(int*)(chased + 0x20) ^ floor) & unchecked((int)0xfffff000)) == 0)
                *(int*)(e + 0x48) = floor;
            return;
        }

        if (posture == 8 && (flags & 2) == 0) return;

        short bob = AdvanceUndeadBob(e, 0x32);
        int height = *(int*)(e + 0x20);
        int swing = (int)Winsys3d.GetSinTableEntry(bob) * 60;
        *(int*)(e + 0x20) = ((*(int*)(e + 0x60) + swing - height + 0x8c000) >> 3) + height;
    }

    /// <summary>Le pas de phase du flottement : <c>(phase + hasard % pas + pas) &amp; 0xfff</c>. Le
    /// pas vaut 100 en regime <c>0x20</c> et 50 sinon — le Sorcier, lui, tire toujours modulo 50 et
    /// ajoute toujours 100.</summary>
    private static short AdvanceUndeadBob(byte* e, int span)
    {
        int step = CrtRand() % span;
        short phase = (short)((*(short*)(e + 0x14) + step + span) & 0xfff);
        *(short*)(e + 0x14) = phase;
        return phase;
    }

    /// <summary>
    /// Ghidra: <c>0x004bc106..0x004bc4fb</c> — <b>le troisieme temps</b> du Mort-vivant, trois
    /// plages testees l'une apres l'autre (<c>0xFA</c>, <c>0x258</c>). Les trois echelles de
    /// <c>+0x28</c>, <c>+0x2a</c> et <c>+0x2c</c> sont remises a <c>0x1000</c> d'entree.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : une fois sur deux fuir en ondulant dos a la proie ; jusqu'a 69,
    /// attendre ; jusqu'a 89, plonger si elle est assez loin ; sinon se tourner vers un cap tire.</item>
    /// <item><b>A mi-distance</b> : face a elle attendre, sinon se tourner ; approcher, et sur cette
    /// meme tranche faire naitre <c>0xde</c> (sous 25) puis <c>0xdd</c> (sur 27) ; se tourner ;
    /// plonger ; et au-dela de 89, fuir.</item>
    /// <item><b>De loin</b> : approcher si le tirage tombe sous <c>hardiesse * 2 + 10</c>, sinon
    /// attendre ; fuir ; se tourner.</item>
    /// </list>
    /// </summary>
    private static void ReplanUndeadAction_004BC106(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        *(ushort*)(e + 0x28) = 0x1000;
        *(ushort*)(e + 0x2a) = 0x1000;
        *(ushort*)(e + 0x2c) = 0x1000;

        // ================================================================ de pres
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                e[0x42] = 3;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                SetUndeadFleeCourse(e, boldness, heading);
            }
            else if (roll < 0x46)
            {
                e[0x42] = 0;
                e[0x43] = (byte)((timid >> 1) + 0x0a);
                *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                *(short*)(e + 0x12) = 0;
            }
            else if (roll < 0x5a && distance > boldness + 0x64)
            {
                e[0x42] = 8;
                e[0x43] = 1;
            }
            else
            {
                e[0x42] = 1;                                        // Ghidra: LAB_004bc214
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
                e[0x43] = 1;
            }
        }

        // ================================================================ a moyenne distance
        else if (distance < 0x258)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
                else
                {
                    e[0x42] = 0;                                    // Ghidra: LAB_004bc28a
                    *(short*)(e + 0x12) = 0;
                    e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
                    *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                }
            }
            else if (roll < 0x23)
            {
                e[0x42] = 2;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);

                if (roll < 0x19)
                {
                    // Ghidra: 0x004bc2eb — les trois vitesses sont mises de cote, +0x54 arme a
                    // 0x3c000 et +0x9a a 2, le temps que 0xde naisse ; seules les vitesses reviennent.
                    int vx = *(int*)(e + 0x50);
                    int vy = *(int*)(e + 0x54);
                    int vz = *(int*)(e + 0x58);

                    *(int*)(e + 0x50) = 0;
                    *(int*)(e + 0x54) = 0x3c000;
                    *(int*)(e + 0x58) = 0;
                    *(short*)(e + 0x9a) = 2;

                    SpawnEffectEntity(e, 0xde, 0, -1);              // Ghidra: FUN_0047a9d0

                    *(int*)(e + 0x50) = vx;
                    *(int*)(e + 0x54) = vy;
                    *(int*)(e + 0x58) = vz;
                }

                if (roll > 0x1a && roll < 0x1c)
                {
                    // Ghidra: 0x004bc356 — la proie est notee dans +0x78 avant la naissance.
                    *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);
                    SpawnEffectEntity(e, 0xdd, 0, -1);              // Ghidra: FUN_0047a9d0
                }
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 1;                                        // Ghidra: LAB_004bc36f
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
            else if (roll < 0x5a)
            {
                e[0x42] = 8;
                e[0x43] = 1;
            }
            else
            {
                e[0x42] = 3;                                        // Ghidra: LAB_004bc3a4
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                SetUndeadFleeCourse(e, boldness, heading);
            }
        }

        // ================================================================ de loin
        else
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (roll < boldness * 2 + 0x0a)
                {
                    e[0x42] = 2;
                    *(short*)(e + 0x10) = 0x64;
                    e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                    return;
                }

                e[0x42] = 0;                                        // Ghidra: LAB_004bc448
                *(short*)(e + 0x12) = 0;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
                *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                return;
            }

            if (roll < 0x46)
            {
                e[0x42] = 3;                                        // Ghidra: LAB_004bc47e
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                SetUndeadFleeCourse(e, boldness, heading);
                return;
            }

            e[0x42] = 1;                                            // Ghidra: LAB_004bc4d8
            if (roll >= 0x5a) *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            e[0x43] = 1;
        }
    }

    /// <summary>
    /// Le cap de la fuite, le meme dans les trois plages : le <b>relevement plus un demi-tour</b>,
    /// secoue de <c>±(hardiesse + 0x80) * 8</c>.
    /// </summary>
    private static void SetUndeadFleeCourse(byte* e, int boldness, int heading)
    {
        int span = (boldness + 0x80) << 4;
        int jitter = CrtRand() % span - (span >> 1);
        *(short*)(e + 0x10) = (short)(jitter + heading + *(short*)(e + 0x32) + 0x800);
    }
}
