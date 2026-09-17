// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE SANGUINAIRE — 0x004B8540 (sous-zones 0 a 3, ou il sert aussi LE TAS D'OS) et 0x004C2770
// (sous-zones 4 et 9), Labyrinthe des morts (zone 5).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004B8540 — LE SANGUINAIRE. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004B8540.asm, 941 instructions, absente de Blaze.c), table de
    // sauts a douze entrees a 0x004B91EC.
    //
    // LA CHARPENTE DU RAMPANT (Ai/OldTower/Crawler.cs) — meme sonde a un mot pres, meme integration,
    // meme pose au sol au masque 0x2000ad82 — mais LA SEULE BETE DU DEPOT QUI PARE :
    //
    //   - LA PARADE. Toute replanification, quelle qu'en soit l'issue, passe par un dernier examen :
    //     si la cible est ELLE-MEME en train de frapper (son bit 2), que le Sanguinaire lui fait face,
    //     qu'il est au sol, a moins de 0xc8, et qu'un tirage tombe sous sa TIMIDITE, la posture
    //     choisie est jetee pour la posture 5. Une bete hardie ne pare presque jamais ;
    //   - LA POSTURE 5 pose le bit 0x10 du mot d'etat tant qu'elle dure, et son attente ne se
    //     decompte qu'une fois le geste fini. C'est la garde ;
    //   - DEUX LOUVOIEMENTS ECRASENT LE TIRAGE. Les strafes de pres et de mi-distance reutilisent le
    //     registre du tirage pour y mettre leur amplitude, (hardiesse + 0x80) * 16 : apres eux la
    //     parade ne peut plus se declencher. Transcrit tel quel ;
    //   - DE PRES IL FUIT : le cap du louvoiement est le RELEVEMENT PLUS 0x800, c'est-a-dire dos a la
    //     cible. A mi-distance le meme calcul n'ajoute pas le demi-tour ;
    //   - LE COUP PART EN PLEIN BOND, comme chez le Persil, mais sur un tirage modulo 60 ;
    //   - SA NAISSANCE (posture 10) depend de la variante : 1 fait naitre 0x36, 0 fait naitre 0x4b ;
    //   - SON HEURT EN L'AIR LACHE LE BIT D'ATTAQUE — un `and al,0FDh` que nulle autre bete de la
    //     famille ne porte ; voir le parametre de SettleSceneEntityOnGroundAndWalls.
    //
    // 0x004C2770 est le MEME CORPS AU SENS STRICT : hors cibles de saut et d'appel, les 941
    // instructions coincident, immediates comprises. Seule change la copie du chercheur de proie
    // (0x004C3450 pour 0x004B9220), elle-meme identique a FUN_004A1020 jusqu'aux immediates.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004b8540</c> — <b>l'intelligence du Sanguinaire</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004B8540(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde du Rampant, dont le dernier mot vaut 0x50 et non 0x64.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x50;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            // Ghidra: 0x004b8580 — la variante 0 cherche sa proie autrement que les autres.
            PickChasedPartyMember_004A1020(e, *(short*)(e + 0x16) == 0 ? 0 : 2, 0);   // Ghidra: FUN_004b9220
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004b8661 — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideSanguinaireAction_004B86AC(e, probe, boldness, distance, heading, speed);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep, dropAttackOnAirborneHit: true);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanSanguinaireAction_004B8DE0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004b86ac..0x004b8bab</c> — <b>le premier temps</b> du Sanguinaire. Douze cases,
    /// dont neuf servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 5 parer, 6 bondir,
    /// 7 marcher droit, 10 la naissance, 11 le coup. Les cases 4, 8 et 9 rendent la main.
    /// </summary>
    private static void DecideSanguinaireAction_004B86AC(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // Ghidra: 0x004b8710 — le bit 0x10 accompagne la posture 5, et elle seule.
        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le coup
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 8)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 8)
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
                // Ghidra: LAB_004b918e — en l'air le geste tombe, mais l'attente ne se rouvre pas.
                *(uint*)e = f & 0xfffffffd;
                if ((f & 1) == 0) e[0x43] = 0;
            }
        }
        else
        {
            // ============================================================ Ghidra: LAB_004b9198
            // LE COUP PART EN PLEIN BOND : en l'air, avant l'image 3, un tirage modulo 60 sous la
            // hardiesse.
            uint f = *(uint*)e;

            if ((f & 1) != 0 && *(short*)(e + 0x1a) < 3 && CrtRand() % 0x3c < boldness)
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
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004b8b7c
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
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x3c : -0x3c - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x6000;
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

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x3c : -0x3c - boldness);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x6000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness + 0x32
                            : -0x32 - boldness);
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                // Ghidra: 0x004b89c2 — le louvoiement s'ouvre sur l'animation 2, non sur la 1.
                *(short*)(e + 0x18) = 2;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 5 : parer
            case 5:
            {
                short phase = *(short*)(e + 0x12);

                // Ghidra: 0x004b89ea — l'attente de la garde ne se decompte QUE le geste fini.
                if (phase == 1 && e[0x43] != 0 && (*(uint*)e & 0x2000000) != 0) e[0x43]--;

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x18) = 5;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e &= 0xfeffffff;                            // Ghidra: LAB_004b8ba6
                return;
            }

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
                        // Ghidra: LAB_004b8a31 — le bond du Sanguinaire porte l'animation 4.
                        *(uint*)e = f & 0xfeffffff;
                        *(short*)(e + 0x18) = 4;

                        int thrust = (boldness + 0x100) << 6;
                        int step = (speed * thrust) >> 0xc;

                        *(int*)(e + 0x5c) = thrust;
                        *(short*)(e + 0x1a) = 0;

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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x6000;
                return;
            }

            // ---------------------------------------------------------- 10 : la naissance
            case 10:
            {
                if (*(short*)(e + 0x12) == 1 && *(short*)(e + 0x1a) == 7)
                {
                    // Ghidra: 0x004b8b1e — la variante choisit ce qui nait.
                    if (*(short*)(e + 0x16) == 1) SpawnEffectEntity(e, 0x36, 0, -1);   // Ghidra: FUN_0047a9d0
                    if (*(short*)(e + 0x16) == 0) SpawnEffectEntity(e, 0x4b, 0, -1);
                }

                uint f = *(uint*)e;
                if ((f & 0x2000000) != 0) e[0x43] = 0;

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e = f & 0xfeffffff;
                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 0;

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
                *(uint*)e &= 0xfeffffff;                            // Ghidra: LAB_004b8ba6
                return;
            }

            // ---------------------------------------------------------- 4, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004b8de0..0x004b9182</c> — <b>le troisieme temps</b> du Sanguinaire, une cascade
    /// propre de trois plages (<c>0xFA</c>, <c>0x258</c>) et non trois <c>if</c> successifs, suivie
    /// de <b>l'examen de parade</b> par lequel TOUTES les branches passent.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : sous 50, frapper si la cible est dans le cone etroit
    /// (<c>timidite * 8 + 0x200</c>) et a portee tiree ; sinon, dans le cone LARGE
    /// (<c>timidite * 8 + 0x400</c>), se mettre en garde une fois sur <c>timidite / 2 + 32</c> ;
    /// sinon se tourner. De 50 a 69, approcher. Au-dela, bondir si la cible est loin, sinon fuir en
    /// louvoyant dos a elle.</item>
    /// <item><b>A mi-distance</b> : face a la cible la naissance, sinon se tourner ; louvoyer vers
    /// elle ; approcher ; et au-dela de 90, attendre.</item>
    /// <item><b>De loin</b> : attendre, se tourner, bondir, louvoyer vers un cap tire.</item>
    /// </list>
    /// </summary>
    private static void ReplanSanguinaireAction_004B8DE0(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        int roll;                                                   // Ghidra: EDI

        // ================================================================ de pres
        if (distance < 0xfa)
        {
            roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    *(short*)(e + 0x18) = 3;
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    *(short*)(e + 0x1a) = 0;
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                }
                else if (((heading + timid * 4 + 0x200) & 0xfff) <= timid * 8 + 0x400
                         && CrtRand() % 100 < (timid >> 1) + 0x20)
                {
                    // Ghidra: 0x004b8eea — la garde volontaire, dans le cone large.
                    e[0x42] = 5;
                    e[0x43] = (byte)(CrtRand() % 5 + 0x0a);
                }
                else
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004b8f06
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x46)
            {
                e[0x42] = 2;
                *(short*)(e + 0x10) = 0x64;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            }
            else if (roll < 0x5a && distance > boldness + 0x64)
            {
                e[0x42] = 6;                                        // Ghidra: LAB_004b90cf
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
            }
            else
            {
                // Ghidra: LAB_004b8f5c — LA FUITE : le cap est le relevement plus 0x800.
                e[0x42] = 3;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);

                int span = (boldness + 0x80) << 4;
                int jitter = CrtRand() % span - (span >> 1);
                *(short*)(e + 0x10) = (short)(jitter + heading + *(short*)(e + 0x32) + 0x800);

                roll = span;                                        // Ghidra: EDI recycle, pas de parade
            }
        }

        // ================================================================ a moyenne distance
        else if (distance < 0x258)
        {
            roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
                else
                {
                    e[0x42] = 0x0a;                                 // Ghidra: LAB_004b8ff5
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x32)
            {
                // Ghidra: LAB_004b9002 — le meme louvoiement, mais SANS le demi-tour.
                e[0x42] = 3;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);

                int span = (boldness + 0x80) << 4;
                int jitter = CrtRand() % span - (span >> 1);
                *(short*)(e + 0x10) = (short)(jitter + heading + *(short*)(e + 0x32));

                roll = span;                                        // Ghidra: EDI recycle, pas de parade
            }
            else if (roll < 0x5a)
            {
                e[0x42] = 2;                                        // Ghidra: LAB_004b9056
                *(short*)(e + 0x10) = 0x64;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            }
            else
            {
                e[0x42] = 0;                                        // Ghidra: LAB_004b909b
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
            }
        }

        // ================================================================ de loin
        else
        {
            roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                e[0x42] = 0;                                        // Ghidra: LAB_004b909b
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 1;
                e[0x43] = 1;
            }
            else if (roll < 0x50)
            {
                e[0x42] = 6;                                        // Ghidra: LAB_004b90cf
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
            }
            else
            {
                e[0x42] = 3;                                        // Ghidra: LAB_004b90e2
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                *(short*)(e + 0x10) = (short)CrtRand();
            }
        }

        TryGuardSanguinaire_004B9112(e, chased, roll, timid, heading, distance);
    }

    /// <summary>
    /// Ghidra: <c>0x004b9112..0x004b9182</c> — <b>la parade</b>, le dernier mot de la
    /// replanification : elle jette la posture qui vient d'etre choisie.
    ///
    /// <para>Cinq conditions, et la troisieme est la plus parlante : <b>le tirage de la
    /// replanification doit etre tombe sous la timidite</b>. Une bete hardie ne pare presque jamais,
    /// et les deux louvoiements ont detruit ce tirage avant d'arriver ici.</para>
    /// </summary>
    private static void TryGuardSanguinaire_004B9112(
        byte* e, byte* chased, int roll, int timid, int heading, int distance)
    {
        if (((heading + timid * 4 + 0x200) & 0xfff) > timid * 8 + 0x400) return;
        if ((chased[0] & 2) == 0) return;
        if (roll >= timid) return;
        if ((e[0] & 1) != 0) return;
        if (*(short*)(e + 0x18) == 5) return;
        if (distance >= 0xc8) return;

        e[0x42] = 5;
        *(short*)(e + 0x1a) = 6;
        *(short*)(e + 0x18) = 5;
        *(int*)(e + 0x5c) = 0;
        *(short*)(e + 0x12) = 1;
        e[0x43] = (byte)(CrtRand() % 5 + 5);
        *(uint*)e &= 0xfeffffff;
    }
}
