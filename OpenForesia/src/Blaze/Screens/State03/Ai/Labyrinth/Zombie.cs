// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE ZOMBIE — 0x004B95E0 (Labyrinthe des morts, sous-zones 0 a 3) et sa copie STRICTE du chateau
// de l'immortel, 0x004CA9C0 (sous-zones 0 a 4).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004B95E0 — LE ZOMBIE. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004B95E0.asm, 836 instructions, absente de Blaze.c), table de
    // sauts a huit entrees a 0x004BA0EC.
    //
    // UN TERRESTRE QUI TITUBE, et deux choses le distinguent de toute la zone :
    //
    //   - IL EST ARRETE PAR LES PORTES. Apres le placement, l'octet <c>+0x0f</c> du porteur
    //     <c>+0x7c</c> est compare a la carte courante (<c>0x005de038</c>) : s'il designe une autre
    //     salle, le mot de contact gagne 0x800 et la bete heurte un mur invisible. C'est la meme
    //     barriere que la Belle bleue de la mine ;
    //   - SA POSTURE 3 TITUBE OU SAUTE : chaque image tire 100, et sous hardiesse / 8 elle bondit
    //     (poussee (hardiesse + 0x200) * 64, <c>+0x54</c> releve de 0xa000) au lieu d'avancer
    //     (poussee (hardiesse + 0x400) * 16). C'est la demarche du zombie ;
    //   - SON COUP PORTE A L'IMAGE 14 OU EN L'AIR, et un coup porte remet la VARIANTE 0x63 a zero —
    //     le seul endroit du depot ou une variante est reecrite en jeu ;
    //   - SA REPLANIFICATION EST ECRASEE SOUS 0x64 : quelle que soit la posture choisie, si la proie
    //     est a moins de 0x64 la bete frappe (dans le cone et a portee tiree) ou se tourne ;
    //   - SON MUR est teste au masque 0x2000ad80 — le bit 2 en moins, celui que sa propre attaque
    //     porte.
    //
    // 0x004CA9C0 est le MEME CORPS AU SENS STRICT : hors cibles de saut et d'appel, les 836
    // instructions coincident, immediates comprises. Il ferme un corps de la zone 6.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004b95e0</c> (et <c>0x004ca9c0</c>) — <b>l'intelligence du Zombie</b>. Voir le
    /// bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004B95E0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde du Sanguinaire.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x50;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 0, 0);   // Ghidra: FUN_004b9220, copie de FUN_004a1020
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideZombieAction_004B96F1(e, probe, boldness, distance, heading, speed);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleZombie_004B9BAD(e, boldness, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        // Ghidra: 0x004b9d87 — l'attente ne fait PAS rendre la main : elle saute la replanification,
        // et l'examen de proximite tourne quand meme, sur la distance et le cap de la lecture de
        // situation au lieu de ceux que la replanification aurait recalcules a plat.
        if (e[0x43] == 0) ReplanZombieAction_004B9D90(e, boldness, timid, &distance, &heading);

        OverrideZombiePlanUpClose(e, timid, heading, distance);
    }

    /// <summary>
    /// Ghidra: <c>0x004b96f1..0x004b9b0c</c> — <b>le premier temps</b> du Zombie. Huit cases, dont
    /// six servies : 0 dormir, 1 se tourner, 2 approcher, 3 tituber, 6 bondir sur la proie,
    /// 7 marcher droit. Les cases 4 et 5 rendent la main.
    /// </summary>
    private static void DecideZombieAction_004B96F1(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ le coup
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 0x0e || (e[0] & 1) != 0)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

                    // Ghidra: 0x004b97b9 — la variante 0x63 retombe a 0 au premier coup porte.
                    if (*(short*)(e + 0x16) == 0x63) *(short*)(e + 0x16) = 0;
                }
            }

            uint f = *(uint*)e;
            if ((f & 1) != 0) return;

            if ((f & 0x2000000) == 0)
            {
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004b9895
                return;
            }

            *(uint*)e = f & 0xfffffffd;
            e[0x43] = 0;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        // Ghidra: LAB_004b97ef — en l'air et sans coup en cours, rien ne se decide.
        if ((e[0] & 1) != 0) return;

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
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004b9895
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
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;                        // `mov [esi+18h],ebx` sur QUATRE octets
                }

                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004b9895
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
                    *(short*)(e + 0x18) = 1;                        // 16 bits : l'image n'est pas touchee
                }

                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                        ? boldness + 0x80
                        : -0x80 - boldness);
                }

                *(int*)(e + 0x5c) = (boldness + 0x300) << 5;        // Ghidra: LAB_004b9b0a
                return;

            // ---------------------------------------------------------- 3 : tituber
            case 3:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;                        // `mov [esi+18h],ebx` sur QUATRE octets
                }

                short course = *(short*)(e + 0x10);

                // Ghidra: 0x004b9942 — un tirage sur hardiesse / 8 fait sauter la demarche.
                if (CrtRand() % 100 < (boldness >> 3))
                {
                    int hop = (boldness + 0x200) << 6;
                    int step = (speed * hop) >> 0xc;
                    *(int*)(e + 0x5c) = hop;

                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x54) += 0xa000;

                    *(uint*)e |= 1;
                    return;
                }

                // Ghidra: LAB_004b99b8 — la demarche.
                int thrust = (boldness + 0x400) << 4;
                int walk = (speed * thrust) >> 0xc;
                *(int*)(e + 0x5c) = thrust;

                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * walk) >> 0xc;
                *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * walk) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x32
                    : -0x32 - boldness * 2);
                return;
            }

            // ---------------------------------------------------------- 6 : bondir sur la proie
            case 6:
            {
                uint f = *(uint*)e & 0xfeffffff;
                *(short*)(e + 0x18) = 3;
                if ((f & 0x2000000) != 0) *(short*)(e + 0x12) = 1;
                *(uint*)e = f;

                int thrust = (boldness + 0x100) << 6;
                int step = (speed * thrust) >> 0xc;
                *(int*)(e + 0x5c) = thrust;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x54) += 0x14000;

                *(uint*)e |= 3;
                return;
            }

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

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = (boldness >> 2) + 0x4000;       // Ghidra: LAB_004b9b0a
                return;
            }

            // ---------------------------------------------------------- 4, 5 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004b9bad..0x004b9d58</c> — <b>la pose au sol et le heurt</b> du Zombie : celle de
    /// la Belle bleue de la mine, <b>barriere de salle comprise</b>, avec un mur teste au masque
    /// <c>0x2000ad80</c> et une sortie en posture 7.
    /// </summary>
    private static void SettleZombie_004B9BAD(byte* e, int boldness, SceneEntityPlacement* keep)
    {
        uint contact = *(uint*)(e + 0x90) & 0xfffffcff;
        *(uint*)(e + 0x90) = contact;

        // Ghidra: 0x004b9bb3 — LA BARRIERE DE SALLE, la meme que celle de la Belle bleue.
        byte area = (*(byte**)(e + 0x7c))[0x0f];
        if (area != 0 && *(byte*)Mem.Camera(0x005de038) != area - 1)
            *(uint*)(e + 0x90) = contact | 0x800;

        uint flags = *(uint*)e;
        bool airborne = (flags & 1) != 0;
        int floor = *(int*)(e + 0x60);

        bool land = airborne
            ? *(int*)(e + 0x20) <= floor
            : *(int*)(e + 0x20) - 0xc000 <= floor;

        if (land && (*(uint*)(e + 0x90) & 0xa000) == 0)
        {
            *(int*)(e + 0x20) = floor;

            if ((flags & 2) == 0 && airborne && e[0x43] != 0) e[0x43] = 0;

            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }
        else if (!airborne)
        {
            *(uint*)e = flags | 1;                                  // Ghidra: LAB_004b9c37
        }

        if ((*(uint*)(e + 0x90) & 0x2000ad80) == 0) return;

        // ================================================================ heurte
        if ((*(uint*)e & 1) != 0)
        {
            // Ghidra: LAB_004b9d31 — heurte EN L'AIR : arret net et 0x28 images d'attente.
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

        if (e[0x42] != 7)
        {
            e[0x42] = 7;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 0x50;
            e[0x43] = (byte)(CrtRand() % 0x28 + 0x14);
        }

        *(uint*)e &= 0xfffffffd;

        RestoreSceneEntityPlacement(e, keep);

        int side = ProbeClearSide_004D5E60(e, 0x2000ad80);
        if ((short)side == 0) side = SignedMask(CrtRand(), 1) * 2 - 1;

        *(short*)(e + 0x32) += (short)((boldness + 0x40) * side);
    }

    /// <summary>
    /// Ghidra: <c>0x004b9d90..0x004ba0eb</c> — <b>le troisieme temps</b> du Zombie, trois plages
    /// (<c>0xFA</c>, <c>0x258</c>) <b>puis un dernier examen qui ecrase tout</b>.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : sous 30, frapper dans le cone et a portee tiree, sinon approcher ;
    /// jusqu'a 49, approcher ; jusqu'a 89, bondir si la proie est au-dela de <c>hardiesse + 100</c> ;
    /// sinon tituber dos a elle.</item>
    /// <item><b>A mi-distance</b> : face a elle attendre, sinon se tourner ; tituber vers elle ;
    /// approcher ; et au-dela de 89, attendre.</item>
    /// <item><b>De loin</b> : approcher sous 50, se tourner jusqu'a 89, bondir au-dela.</item>
    /// <item><b>Et sous <c>0x64</c></b>, quelle que soit la decision : frapper si la proie est dans le
    /// cone et a portee tiree, sinon se tourner.</item>
    /// </list>
    /// </summary>
    private static void ReplanZombieAction_004B9D90(
        byte* e, int boldness, int timid, int* distanceOut, int* headingOut)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(e + 0x32)) & 0xfff;

        // Ghidra: 0x004b9dbd et 0x004b9dd1 — les deux valeurs ECRASENT celles de la lecture de
        // situation, et c'est sur elles que l'examen de proximite travaillera.
        *distanceOut = distance;
        *headingOut = heading;

        // ================================================================ de pres
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    *(short*)(e + 0x18) = 3;
                    *(short*)(e + 0x1a) = 0;                        // `mov [esi+18h],3` sur QUATRE octets
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    *(short*)(e + 0x1a) = 0;
                    return;
                }

                ApproachZombieTarget_004B9E54(e, timid, 0x50);
                return;
            }

            if (roll < 0x32)
            {
                ApproachZombieTarget_004B9E54(e, timid, 0x50);      // Ghidra: LAB_004b9e80
                return;
            }

            if (roll < 0x5a && distance > boldness + 0x64)
            {
                e[0x42] = 6;                                        // Ghidra: 0x004b9ec9
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 0;
                return;
            }

            e[0x42] = 3;                                            // Ghidra: LAB_004b9ee7
            e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            *(short*)(e + 0x1a) = 0;
            *(short*)(e + 0x10) = (short)(ZombieJitter(boldness) + heading + *(short*)(e + 0x32) + 0x800);
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
                    RestZombie_004B9FDF(e, timid);                  // Ghidra: LAB_004b9fdf
                    return;
                }

                e[0x42] = 1;
                e[0x43] = 1;
                *(short*)(e + 0x1a) = 0;
                return;
            }

            if (roll < 0x32)
            {
                // Ghidra: LAB_004b9f84 — la titubation, mais SANS le demi-tour.
                e[0x42] = 3;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x10) = (short)(ZombieJitter(boldness) + heading + *(short*)(e + 0x32));
                return;
            }

            if (roll < 0x5a)
            {
                ApproachZombieTarget_004B9E54(e, timid, 0x64);      // Ghidra: LAB_004ba021
                return;
            }

            RestZombie_004B9FDF(e, timid);                          // Ghidra: LAB_004b9fdf
            return;
        }

        // ================================================================ de loin
        int far = CrtRand() % 100;

        if (far < 0x32)
        {
            ApproachZombieTarget_004B9E54(e, timid, 0x64);          // Ghidra: LAB_004ba021
            return;
        }

        if (far < 0x5a)
        {
            e[0x42] = 1;                                            // Ghidra: LAB_004ba04e
            e[0x43] = 1;
            *(short*)(e + 0x1a) = 0;
            return;
        }

        e[0x42] = 6;                                                // Ghidra: LAB_004ba061
        e[0x43] = 1;
        *(short*)(e + 0x1a) = 0;
        *(short*)(e + 0x10) = (short)CrtRand();
    }

    /// <summary>
    /// Ghidra: <c>0x004ba07a..0x004ba0e0</c> — <b>l'examen de proximite</b> : sous <c>0x64</c>, la
    /// posture qui vient d'etre choisie est jetee. Les branches qui passent par <c>LAB_004ba076</c>
    /// y entrent aussi, mais avec la timidite rechargee — d'ou l'appel commun.
    /// </summary>
    private static void OverrideZombiePlanUpClose(byte* e, int timid, int heading, int distance)
    {
        if (distance >= 0x64) return;

        if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
            && distance < CrtRand() % (timid * 2 + 1) + 0x64)
        {
            *(short*)(e + 0x18) = 3;
            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
            e[0x43] = 1;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        e[0x42] = 1;                                                 // Ghidra: LAB_004ba0d9
        e[0x43] = 1;
    }

    /// <summary>L'amplitude de la titubation : <c>±(hardiesse + 0x80) * 8</c>.</summary>
    private static int ZombieJitter(int boldness)
    {
        int span = (boldness + 0x80) << 4;
        return CrtRand() % span - (span >> 1);
    }

    /// <summary>Ghidra: <c>LAB_004b9e54</c> et <c>LAB_004ba021</c> — l'approche, portee 0x50 ou 0x64.</summary>
    private static void ApproachZombieTarget_004B9E54(byte* e, int timid, short range)
    {
        e[0x42] = 2;
        *(short*)(e + 0x1a) = 0;
        *(short*)(e + 0x10) = range;
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
    }

    /// <summary>Ghidra: <c>LAB_004b9fdf</c> — l'attente, qui remet aussi l'animation a zero.</summary>
    private static void RestZombie_004B9FDF(byte* e, int timid)
    {
        e[0x42] = 0;
        *(short*)(e + 0x18) = 0;
        *(short*)(e + 0x1a) = 0;
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
        *(uint*)e |= 0x1000000;
    }
}
