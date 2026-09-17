// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE REVENANT — 0x004C1C30 (sous-zones 5, 6, 7, 8) et 0x004C6700 (sous-zones 4, 9),
// Labyrinthe des morts (zone 5).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004C1C30 — LE REVENANT. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004C1C30.asm, 853 instructions, absente de Blaze.c), table de
    // sauts a NEUF entrees a 0x004C2740.
    //
    // LE DUNŒUD EN PLUS LOURD (Ai/Labyrinth/Dunoeud.cs) : meme sommeil sous 0x12c, meme charge en
    // onze temps, meme refus de tourner la table des postures quand il frappe ou qu'il est en l'air
    // ailleurs qu'en posture 7. Ce qui change :
    //
    //   - IL SAUVEGARDE SON PLACEMENT et le rend sur un heurt, comme le Rampant. Le mur le met en
    //     posture 8 — sa marche droite — et non en 7, et un heurt EN L'AIR le repose au sol pour
    //     0x28 images ;
    //   - SA CHARGE PORTE L'ANIMATION 5, images bridees a 5 puis a 10, et le coup part a l'image 12 ;
    //   - SON POSER AU SOL est celui du Dunœud : marge de 0xc unites hors vol, contact examine sur
    //     0xa000 et non 0x8000 ;
    //   - SES POUSSEES sont les plus fortes de la zone : approche (hardiesse + 0x180) * 160,
    //     louvoiement (hardiesse + 0xa0) * 256, bond (hardiesse + 0xf0) * 256 ;
    //   - IL FRAPPE A L'IMAGE 10 ou en posture 7, et n'applique aucun effet d'etat.
    //
    // 0x004C6700 est le meme corps, aux registres pres, avec sa propre copie du chercheur de proie.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004c1c30</c> (et <c>0x004c6700</c>) — <b>l'intelligence du Revenant</b>. Voir le
    /// bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004C1C30(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde du Sanguinaire.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x50;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 4, 0);   // Ghidra: FUN_004bde40, copie de FUN_004a1020
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideRevenantAction_004C1D42(e, probe, boldness, distance, heading, speed);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleRevenant_004C228F(e, boldness, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanRevenantAction_004C243C(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004c1d42..0x004c21ee</c> — <b>le premier temps</b> du Revenant. Neuf cases, dont
    /// sept servies : 0 dormir, 1 se tourner, 2 approcher, 3 louvoyer, 6 bondir, 7 charger,
    /// 8 marcher droit. Les cases 4 et 5 rendent la main.
    /// </summary>
    private static void DecideRevenantAction_004C1D42(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ le coup
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 0x0a || e[0x42] == 7)
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

            uint f = *(uint*)e;
            if ((f & 0x2000000) != 0 && e[0x42] != 7)
            {
                *(uint*)e = f & 0xfffffffd;
                e[0x43] = 0;
            }
        }

        // Ghidra: 0x004c1e2b — la charge est la seule posture qui tourne en frappant.
        if ((e[0] & 2) != 0 && e[0x42] != 7) return;

        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : dormir
            case 0:
                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] == 0)
                {
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004c1ed1
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                e[0x43]--;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e |= 0x1000000;
                    StartRevenantAnimation(e, 1);
                }

                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004c1ed1
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e |= 0x1000000;
                    StartRevenantAnimation(e, 1);
                }

                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                        ? boldness * 2 + 0x80
                        : -0x80 - boldness * 2);
                }

                *(int*)(e + 0x5c) = (boldness + 0x180) * 5 * 0x20;
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e |= 0x1000000;
                    StartRevenantAnimation(e, 1);
                }

                int thrust = (boldness + 0xa0) << 8;
                int step = (speed * thrust) >> 0xc;
                *(int*)(e + 0x5c) = thrust;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 8 + 0x32
                    : -0x32 - boldness * 8);
                return;
            }

            // ---------------------------------------------------------- 6 : bondir
            case 6:
            {
                if ((e[0] & 1) != 0) return;

                *(uint*)e |= 0x1000000;
                StartRevenantAnimation(e, 1);

                int thrust = (boldness + 0xf0) << 8;
                int step = (speed * thrust) >> 0xc;
                *(int*)(e + 0x5c) = thrust;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x54) += 0x1e000;

                *(uint*)e |= 1;
                return;
            }

            // ---------------------------------------------------------- 7 : charger
            case 7:
                ChargeRevenant_004C2090(e, boldness, heading);
                return;

            // ---------------------------------------------------------- 8 : marcher droit
            case 8:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    if (distance < *(short*)(e + 0x10)) e[0x43] = 0;
                    else if (e[0x43] != 0) e[0x43]--;
                }

                if (phase != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = (boldness >> 1) + 0x4000;
                return;
            }

            // ---------------------------------------------------------- 4, 5 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004c2090..0x004c21a0</c> — <b>la charge</b> du Revenant : celle du Dunœud sur
    /// l'animation 5, images bridees a 5 puis a 10, coup a l'image 12.
    /// </summary>
    private static void ChargeRevenant_004C2090(byte* e, int boldness, int heading)
    {
        short beat = *(short*)(e + 0x12);

        if (beat == 0) *(int*)(e + 0x5c) = 0;

        if (beat < 0x0a)
        {
            if (*(short*)(e + 0x1a) >= 6) *(short*)(e + 0x1a) = 5;
            *(short*)(e + 0x18) = 5;

            beat++;
            *(short*)(e + 0x12) = beat;

            if (beat == 7) *(uint*)e |= 2;

            if (beat == 0x0a)
            {
                *(int*)(e + 0x54) += 0x14000;
                *(int*)(e + 0x5c) = ((boldness + 0x100) * 5) << 8;
                *(short*)(e + 0x12) = 0x64;
                *(uint*)e |= 3;
                Aud.PlayPositionalSound(0x24, 0x600, 0x1200, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
            }

            if (((heading + 0x100) & 0xfff) > 0x200)
            {
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x32
                    : -0x32 - boldness * 2);
            }
        }

        if (*(short*)(e + 0x12) == 0x64)
        {
            int thrust = *(int*)(e + 0x5c);
            *(short*)(e + 0x18) = 5;
            thrust -= thrust >> 2;
            *(int*)(e + 0x5c) = thrust;

            if (*(short*)(e + 0x1a) >= 0x0b) *(short*)(e + 0x1a) = 0x0a;

            if ((thrust & unchecked((int)0xfffff000)) < 0xa000) *(short*)(e + 0x12) = 0x65;
        }

        if (*(short*)(e + 0x12) != 0x65) return;

        *(short*)(e + 0x18) = 5;
        *(uint*)e &= 0xfffffffd;

        if (*(short*)(e + 0x1a) == 0x0c) e[0x43] = 0;
    }

    /// <summary>Ghidra : les ecritures <c>mov [esi+18h],&lt;dword&gt;</c> portent sur QUATRE octets —
    /// elles posent l'animation ET remettent l'image a zero.</summary>
    private static void StartRevenantAnimation(byte* e, short animation)
    {
        *(short*)(e + 0x18) = animation;
        *(short*)(e + 0x1a) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c228f..0x004c2404</c> — <b>la pose au sol et le heurt</b> du Revenant : le
    /// sol du Dunœud (marge de <c>0xc</c> unites, contact examine sur <c>0xa000</c>) et le mur du
    /// Rampant (les onze champs rendus, la sonde de contournement), mais avec la <b>posture 8</b> en
    /// sortie de heurt.
    /// </summary>
    private static void SettleRevenant_004C228F(byte* e, int boldness, SceneEntityPlacement* keep)
    {
        uint flags = *(uint*)e;
        uint contact = *(uint*)(e + 0x90) & 0xfffffcff;
        *(uint*)(e + 0x90) = contact;

        bool airborne = (flags & 1) != 0;
        int floor = *(int*)(e + 0x60);

        bool settle = airborne
            ? *(int*)(e + 0x20) <= floor
            : *(int*)(e + 0x20) - 0xc000 <= floor;

        if (settle && (contact & 0xa000) == 0)
        {
            *(int*)(e + 0x20) = floor;

            if ((flags & 2) == 0 && airborne && e[0x43] != 0) e[0x43] = 0;

            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((contact & 0x2000ad82) == 0) return;

        // ================================================================ heurte
        if ((*(uint*)e & 1) != 0)
        {
            // Ghidra: LAB_004c23dd — heurte EN L'AIR : arret net et 0x28 images d'attente.
            *(int*)(e + 0x5c) = 0;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x58) = 0;
            *(int*)(e + 0x1c) = keep->X;
            *(int*)(e + 0x24) = keep->Z;
            e[0x42] = 0;
            e[0x43] = 0x28;
            *(short*)(e + 0x12) = 0;
            *(ushort*)(e + 0x2c) = 0x1000;
            return;
        }

        if (e[0x42] != 8)
        {
            e[0x42] = 8;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 0x50;
            e[0x43] = (byte)(CrtRand() % 0x28 + 0x14);
        }

        *(uint*)e &= 0xfffffffd;

        RestoreSceneEntityPlacement(e, keep);

        int side = ProbeClearSide_004D5E60(e, 0x2000ad82);
        if ((short)side == 0) side = SignedMask(CrtRand(), 1) * 2 - 1;

        *(short*)(e + 0x32) += (short)((boldness + 0x40) * side);
    }

    /// <summary>
    /// Ghidra: <c>0x004c243c..0x004c2732</c> — <b>le troisieme temps</b> du Revenant, celui du
    /// Dunœud aux memes seuils : trois plages (<c>0xFA</c>, <c>0x258</c>).
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : sous 50, frapper d'emblee dans le cone etroit et a portee tiree ; sinon,
    /// dans le cone LARGE, lancer la charge ; sinon se tourner. Jusqu'a 69 approcher ; jusqu'a 79
    /// louvoyer dos a la proie ; au-dela bondir.</item>
    /// <item><b>A mi-distance</b> : face a elle dormir, sinon se tourner ; louvoyer vers elle ;
    /// approcher ; bondir.</item>
    /// <item><b>De loin</b> : dormir sous 10, se tourner jusqu'a 49, louvoyer vers un cap tire
    /// jusqu'a 89, bondir au-dela.</item>
    /// </list>
    /// </summary>
    private static void ReplanRevenantAction_004C243C(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(e + 0x32)) & 0xfff;

        // ================================================================ de pres
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    StartRevenantAnimation(e, 3);
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                // Ghidra: LAB_004c2502 — le cone large arme la charge.
                if (((heading + timid * 4 + 0x200) & 0xfff) > timid * 8 + 0x400)
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004c26d7
                    e[0x43] = 1;
                    return;
                }

                e[0x42] = 7;
                *(uint*)e &= 0xfeffffff;
                e[0x43] = 1;
                StartRevenantAnimation(e, 3);
                *(short*)(e + 0x12) = 0;
                return;
            }

            if (roll < 0x46)
            {
                ApproachRevenantTarget_004C265D(e, timid);
                return;
            }

            if (roll < 0x50)
            {
                e[0x42] = 3;                                        // Ghidra: 0x004c255b
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                *(short*)(e + 0x10) = (short)(RevenantJitter(boldness) + heading + *(short*)(e + 0x32) + 0x800);
                return;
            }

            LeapRevenant_004C2722(e);
            return;
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                if (((heading + 0x400) & 0xfff) < 0x800)
                {
                    SleepRevenant_004C269F(e, timid);
                    return;
                }

                e[0x42] = 1;
                e[0x43] = 1;
                return;
            }

            if (roll < 0x32)
            {
                // Ghidra: LAB_004c25fe — le meme louvoiement, mais SANS le demi-tour.
                e[0x42] = 3;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                *(short*)(e + 0x10) = (short)(RevenantJitter(boldness) + heading + *(short*)(e + 0x32));
                return;
            }

            if (roll < 0x5a)
            {
                ApproachRevenantTarget_004C265D(e, timid);
                return;
            }

            LeapRevenant_004C2722(e);
            return;
        }

        // ================================================================ de loin
        int far = CrtRand() % 100;

        if (far < 0x0a)
        {
            SleepRevenant_004C269F(e, timid);
            return;
        }

        if (far < 0x32)
        {
            e[0x42] = 1;                                            // Ghidra: LAB_004c26d7
            e[0x43] = 1;
            return;
        }

        if (far < 0x5a)
        {
            e[0x42] = 3;                                            // Ghidra: LAB_004c26ea
            e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            *(short*)(e + 0x10) = (short)CrtRand();
            return;
        }

        LeapRevenant_004C2722(e);
    }

    /// <summary>L'amplitude du louvoiement : <c>±(hardiesse + 0x80) * 8</c>.</summary>
    private static int RevenantJitter(int boldness)
    {
        int span = (boldness + 0x80) << 4;
        return CrtRand() % span - (span >> 1);
    }

    /// <summary>Ghidra: <c>LAB_004c265d</c> — l'approche, portee 0x64.</summary>
    private static void ApproachRevenantTarget_004C265D(byte* e, int timid)
    {
        e[0x42] = 2;
        *(short*)(e + 0x10) = 0x64;
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
    }

    /// <summary>Ghidra: <c>LAB_004c269f</c> — le sommeil, qui remet aussi l'animation a zero.</summary>
    private static void SleepRevenant_004C269F(byte* e, int timid)
    {
        e[0x42] = 0;
        StartRevenantAnimation(e, 0);
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
        *(uint*)e |= 0x1000000;
    }

    /// <summary>Ghidra: <c>LAB_004c2722</c> — le bond vers un cap tire.</summary>
    private static void LeapRevenant_004C2722(byte* e)
    {
        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }
}
