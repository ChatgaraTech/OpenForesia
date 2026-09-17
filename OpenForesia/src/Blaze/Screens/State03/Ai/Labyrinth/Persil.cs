// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE PERSIL — 0x004BAB60 (sous-zones 0, 1, 2, 3) et 0x004C4290 (sous-zones 4, 9),
// Labyrinthe des morts (zone 5).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004BAB60 — LE PERSIL. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004BAB60.asm, 966 instructions, absente de Blaze.c), table de
    // sauts a douze entrees a 0x004BB830.
    //
    // LA CHARPENTE DU RAMPANT (Ai/OldTower/Crawler.cs) — meme sonde, meme integration, meme pose au
    // sol au masque 0x2000ad82 — mais une bete qui SAUTE ET FRAPPE EN L'AIR, et qui n'a pas de sort :
    //
    //   - IL N'ANNULE PAS SON GESTE SUR UN TRAIT : le corps ne passe jamais par
    //     CancelGestureOnTrait_004D5DE0, seul de sa famille ;
    //   - LE COUP PART EN PLEIN BOND. Hors attaque, s'il est en l'air et avant l'image 3, un tirage
    //     sous sa hardiesse arme l'animation 3 ; la lame porte a l'image 15 ;
    //   - SA POSTURE 10 N'EST PAS UNE INCANTATION mais une NAISSANCE : animation 4, et a l'image 17
    //     le sous-type 0x36 nait par le pondeur qui recopie la planche du parent ;
    //   - DEUX POSTURES QUE LE RAMPANT N'A PAS : 8, un vol plane dont la poussee perd un huitieme par
    //     image et qui retombe sous 0x5000 ; 9, un virage doux au huitieme de l'ecart, fini sous 0x32 ;
    //   - IL LOUVOIE EN VIRANT DEUX FOIS SA HARDIESSE (posture 3), la ou le Rampant en vire huit ;
    //   - SA MARCHE DROITE (posture 7) pousse a hardiesse / 2 + 0x4000, non a hardiesse * 0x90 + 0x5000.
    //
    // 0x004C4290 est le meme corps, aux registres pres, avec sa propre copie du chercheur de proie
    // (0x004C3450 pour 0x004B9220) — les deux copies sont identiques a FUN_004A1020 jusqu'aux
    // immediates, et les deux corps portent les memes constantes.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004bab60</c> — <b>l'intelligence du Persil</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004BAB60(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde, la meme que celle du Rampant.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004b9220, copie de FUN_004a1020
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // Ghidra: 0x004bac70 — la distance recalculee a trois dimensions. Pas de
        // CancelGestureOnTrait_004D5DE0 ici : le Persil ne l'appelle pas.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecidePersilAction_004BACB2(e, probe, boldness, distance, heading, speed);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanPersilAction_004BB458(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004bacb2..0x004bb211</c> — <b>le premier temps</b> du Persil. Douze cases, dont
    /// dix servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 6 bondir, 7 marcher droit,
    /// 8 planer, 9 virer doucement, 10 la naissance, 11 le coup. Les cases 4 et 5 rendent la main.
    /// </summary>
    private static void DecidePersilAction_004BACB2(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ le coup
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 0x0f)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0f)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }
            }

            uint f = *(uint*)e;
            if ((f & 0x2000000) != 0)
            {
                // Ghidra: LAB_004bb7d3 — en l'air le geste tombe, mais l'attente ne se rouvre pas.
                *(uint*)e = f & 0xfffffffd;
                if ((f & 1) == 0) e[0x43] = 0;
            }
        }
        else
        {
            // ============================================================ Ghidra: LAB_004bb7dd
            // LE COUP PART EN PLEIN BOND : en l'air, avant l'image 3, un tirage sous la hardiesse.
            uint f = *(uint*)e;

            if ((f & 1) != 0 && *(short*)(e + 0x1a) < 3 && CrtRand() % 0x28 < boldness)
            {
                *(uint*)e = (f & 0xfeffffff) | 2;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 0;
            }
        }

        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 0;
                    *(uint*)e |= 0x1000000;
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
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004bb1df
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
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                }

                if (*(short*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x80
                    : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    // Ghidra: 0x004baf76 — DEUX fois la hardiesse, la ou le Rampant en vire huit.
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 6 : bondir
            case 6:
                if (*(short*)(e + 0x12) == 0)
                {
                    uint f = *(uint*)e;

                    if ((f & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        *(uint*)e = f & 0xfeffffff;

                        int thrust = (boldness + 0x100) << 6;
                        int step = (speed * thrust) >> 0xc;

                        *(short*)(e + 0x18) = 1;
                        *(short*)(e + 0x1a) = 0;
                        *(int*)(e + 0x5c) = thrust;

                        short course = *(short*)(e + 0x10);
                        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 1;
                        *(short*)(e + 0x12) = 1;
                    }
                }

                if ((*(uint*)e & 1) != 0) return;
                e[0x43] = 0;
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

            // ---------------------------------------------------------- 8 : planer
            case 8:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    // Ghidra: 0x004bb0db — la poussee perd un huitieme par image, et le tangage de
                    // +0x2c la suit ; sous 0x5000 le vol plane est fini.
                    int thrust = *(int*)(e + 0x5c);
                    *(short*)(e + 0x18) = 2;
                    *(short*)(e + 0x1a) = 1;

                    thrust -= thrust >> 3;
                    *(int*)(e + 0x5c) = thrust;

                    if (thrust < 0x5000)
                    {
                        e[0x43] = 0;
                        *(int*)(e + 0x5c) = 0;
                        *(ushort*)(e + 0x2c) = 0x1000;
                    }

                    *(ushort*)(e + 0x2c) = (ushort)((*(int*)(e + 0x5c) >> 5) + 0x1000);
                }

                if (phase != 0) return;

                *(short*)(e + 0x18) = 1;
                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;

                // Ghidra: 0x004bb140 — `and al,0FCh` ne masque que l'octet bas ; la hardiesse y tient.
                *(int*)(e + 0x5c) = ((boldness & 0xfc) + (CrtRand() % 0x32) * 4) << 0xa;
                return;
            }

            // ---------------------------------------------------------- 9 : virer doucement
            case 9:
            {
                // Ghidra: 0x004bb154 — un huitieme de l'ecart par image, animation 0 ; sous 0x32
                // d'ecart restant la posture est finie.
                short target = *(short*)(e + 0x10);
                short course = *(short*)(e + 0x32);

                course = (short)(course + ((target - course) >> 3));

                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x32) = course;

                int gap = target - course >= 0 ? target - course : course - target;

                if (gap >= 0x32)
                {
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004bb1df
                    return;
                }

                e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 10 : la naissance
            case 10:
            {
                if (*(short*)(e + 0x12) == 1 && *(short*)(e + 0x1a) == 0x11)
                {
                    SpawnEffectEntity(e, 0x36, 0, -1);              // Ghidra: FUN_0047a9d0
                }

                uint f = *(uint*)e;
                if ((f & 0x2000000) != 0) e[0x43] = 0;

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e = f & 0xfeffffff;
                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x18) = 4;
                *(short*)(e + 0x1a) = 0;

                // Ghidra: 0x004bb1ca — une bete timide amorce la naissance a l'arret.
                if (boldness < 0x1e) *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 11 : le coup
            case 11:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    uint f = *(uint*)e;
                    if ((f & 0x2000000) != 0)
                    {
                        *(uint*)e = f & 0xfffffffd;
                        e[0x43] = 0;
                    }
                }

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 4, 5 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004bb458..0x004bb7cc</c> — <b>le troisieme temps</b> du Persil, trois plages
    /// testees l'une apres l'autre (<c>0xFA</c>, <c>0x258</c>), distance et cap recalcules a deux
    /// dimensions.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : la moitie des tirages frappe — dans le cone de <c>timidite * 8 + 0x200</c>
    /// et sous une portee tiree de <c>0x64 + hasard % (timidite * 2 + 1)</c> —, sinon se tourne ; les
    /// tirages de 60 a 69 bondissent au-dela de <c>hardiesse + 100</c> ; tout le reste approche.</item>
    /// <item><b>A mi-distance</b> : face a la cible attendre, sinon se tourner ; approcher ; bondir ;
    /// louvoyer vers un cap tire ; et sur les hauts tirages la naissance.</item>
    /// <item><b>De loin</b> : attendre (mais une bete hardie approche a la place), louvoyer, se
    /// tourner, la naissance si la cible est dans le cone, et bondir.</item>
    /// </list>
    /// </summary>
    private static void ReplanPersilAction_004BB458(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        // ================================================================ de pres
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 100)
                {
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    *(int*)(e + 0x5c) = 0;
                    *(short*)(e + 0x18) = 3;
                    *(short*)(e + 0x1a) = 0;
                }
                else
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004bb51c
                    e[0x43] = 1;
                }
            }
            else if (roll >= 0x3c && roll < 0x46 && distance > boldness + 100)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 2;                                        // Ghidra: LAB_004bb556
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0xfa)
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
                    e[0x42] = 0;                                    // Ghidra: LAB_004bb5d4
                    e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
                    *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                    *(short*)(e + 0x12) = 0;
                }
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else if (roll < 0x50)
            {
                e[0x42] = 3;
                *(short*)(e + 0x12) = 0;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
            else
            {
                e[0x42] = 0x0a;                                     // Ghidra: LAB_004bb680
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
        }

        // ================================================================ de loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            // Ghidra: 0x004bb6ae — une bete hardie ne se repose pas : sous hardiesse * 2 + 10 elle
            // approche a la place.
            if (far < boldness * 2 + 10)
            {
                ApproachPersilTarget_004BB78B(e, timid, 0x64);
                return;
            }

            e[0x42] = 0;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x28)
        {
            e[0x42] = 3;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            return;
        }

        if (far < 0x3c)
        {
            e[0x42] = 1;
            e[0x43] = 1;
            return;
        }

        if (far < 0x50)
        {
            if (((heading + timid * 4 + 0x100) & 0xfff) > timid * 8 + 0x200)
            {
                ApproachPersilTarget_004BB78B(e, timid, 0x64);
                return;
            }

            e[0x42] = 0x0a;
            e[0x43] = 1;
            *(short*)(e + 0x12) = 0;
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }

    /// <summary>
    /// Ghidra: <c>LAB_004bb78b</c> — <b>l'approche du lointain</b>, la sortie commune des deux refus
    /// de la derniere plage.
    /// </summary>
    private static void ApproachPersilTarget_004BB78B(byte* e, int timid, short range)
    {
        e[0x42] = 2;
        *(short*)(e + 0x10) = range;
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
    }
}
