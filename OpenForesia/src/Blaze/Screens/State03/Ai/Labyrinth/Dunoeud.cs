// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DUNŒUD — 0x004BA110 (sous-zones 0, 1, 2, 3) et 0x004C3840 (sous-zones 4, 9),
// Labyrinthe des morts (zone 5).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004BA110 — LE DUNŒUD. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004BA110.asm, 766 instructions, absente de Blaze.c), table de
    // sauts a HUIT entrees a 0x004BAAB8. C'est la plus courte routine d'intelligence de la zone.
    //
    // UNE BETE A UNE SEULE IDEE : LA CHARGE. Ce qui la distingue de toute sa famille :
    //
    //   - ELLE NE SAUVEGARDE PAS SON PLACEMENT. Pas de SceneEntityPlacement, pas de restauration
    //     apres heurt : seul le cap est mis de cote pour le cas ou la vitesse serait nulle. Un mur
    //     la met simplement en posture 1 et coupe sa poussee ;
    //   - SA POSTURE 7 EST UNE CHARGE EN ONZE TEMPS. Le compteur +0x12 monte de 0 a 10 ; a 7 le bit
    //     d'attaque tombe, a 10 elle bondit (poussee (hardiesse + 0x100) * 5 * 256, +0x54 releve de
    //     0x14000, son 0x24) et le compteur passe a 0x64. La poussee perd alors UN QUART par image,
    //     et sous 0xa000 le compteur passe a 0x65 : le bit d'attaque tombe et l'image 15 rend la
    //     main. Les images sont bridees en chemin — jamais au-dela de 8, puis de 13 ;
    //   - RIEN NE S'EXECUTE PENDANT LA CHARGE. Le corps saute la table de postures des qu'il est en
    //     attaque ou en l'air DANS UNE AUTRE POSTURE QUE 7 : la posture 7 est donc la seule qui
    //     tourne en frappant ;
    //   - ELLE FRAPPE A L'IMAGE 13 OU EN POSTURE 7, et un coup porte tire 100 : sous 11 il applique
    //     l'effet de piege, de 11 a 20 l'effet d'etat ;
    //   - SON POSER AU SOL prend 0xc unites de marge quand elle n'est pas en l'air, et l'atterrissage
    //     rouvre son attente ;
    //   - SA POSTURE 0 EST UN SOMMEIL : aucune animation, aucune poussee, et elle n'en sort que si la
    //     proie passe sous 0x12c.
    //
    // 0x004C3840 est le meme corps, aux registres pres, avec sa propre copie du chercheur de proie.
    // =============================================================================================

    /// <summary>
    /// Ghidra: FUN_004baae0 @ 0x004baae0 — <b>l'effet d'etat, s'il est permis</b> : le filtre de
    /// <see cref="RejectStatusTarget_004A2C30"/>, puis <see cref="ApplyStatusEffect_0053B3C0"/>.
    /// La sœur exacte de <see cref="ApplyTrapEffectIfAllowed_004A2BF0"/>, a l'effet pres.
    /// </summary>
    /// <returns><c>1</c> si la victime a ete refusee, <c>0</c> si l'effet est parti.</returns>
    private static uint ApplyStatusEffectIfAllowed_004BAAE0(uint* caster, uint* victim, int keep)
    {
        if (RejectStatusTarget_004A2C30(caster, victim)) return 1;

        ApplyStatusEffect_0053B3C0(caster, victim, keep);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004ba110</c> (et <c>0x004c3840</c>) — <b>l'intelligence du Dunœud</b>. Voir le
    /// bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004BA110(byte* record)
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

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        short keepHeading = *(short*)(e + 0x32);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideDunoeudAction_004BA1CD(e, probe, boldness, distance, heading, speed);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleDunoeud_004BA726(e);

        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keepHeading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanDunoeudAction_004BA7D3(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004ba1cd..0x004ba685</c> — <b>le premier temps</b> du Dunœud. Huit cases, dont
    /// six servies : 0 dormir, 1 se tourner, 2 approcher, 3 louvoyer, 6 bondir, 7 charger. Les cases
    /// 4 et 5 rendent la main.
    ///
    /// <para><b>La table n'est atteinte qu'au repos</b> : une bete en attaque ou en l'air dans une
    /// autre posture que 7 rend la main avant le <c>switch</c>.</para>
    /// </summary>
    private static void DecideDunoeudAction_004BA1CD(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ le coup
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 0x0d || e[0x42] == 7)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

                    // Ghidra: 0x004ba29b — un coup sur dix empoisonne, un coup sur dix altere.
                    int roll = CrtRand() % 100;

                    if (roll <= 0x0a)
                    {
                        // Ghidra: FUN_004bab20, copie de FUN_004a2bf0.
                        ApplyTrapEffectIfAllowed_004A2BF0((uint*)e, (uint*)prey, 0);
                    }
                    else if (roll <= 0x14)
                    {
                        ApplyStatusEffectIfAllowed_004BAAE0((uint*)e, (uint*)prey, 0);
                    }
                }
            }

            uint f = *(uint*)e;
            if ((f & 0x2000000) != 0 && e[0x42] != 7)
            {
                *(uint*)e = f & 0xfffffffd;
                e[0x43] = 0;
            }
        }

        // Ghidra: 0x004ba2eb — la charge est la seule posture qui tourne en frappant ou en l'air.
        if ((e[0] & 2) != 0 && e[0x42] != 7) return;
        bool airborne = (e[0] & 1) != 0;
        if (airborne && e[0x42] != 7) return;

        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : dormir
            case 0:
                if (distance < 0x12c)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004ba682
                    return;
                }

                if (e[0x43] == 0)
                {
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004ba3b8
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                e[0x43]--;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004ba3b4
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e |= 0x1000000;
                    StartDunoeudAnimation(e, 1);
                }

                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004ba3b4
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e |= 0x1000000;
                    StartDunoeudAnimation(e, 1);
                }

                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                        ? boldness + 0x80
                        : -0x80 - boldness);
                }

                *(int*)(e + 0x5c) = boldness * 0x90 + 0xa000;
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e |= 0x1000000;
                    StartDunoeudAnimation(e, 1);
                }

                int thrust = (boldness + 0x70) << 8;
                int step = (speed * thrust) >> 0xc;
                *(int*)(e + 0x5c) = thrust;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                // Ghidra: 0x004ba4c9 — HUIT fois la hardiesse, comme le Rampant.
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 8 + 0x32
                    : -0x32 - boldness * 8);
                return;
            }

            // ---------------------------------------------------------- 6 : bondir
            case 6:
            {
                if (airborne) return;

                *(uint*)e |= 0x1000000;
                StartDunoeudAnimation(e, 1);

                int thrust = (boldness + 0xc0) << 8;
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
                ChargeDunoeud_004BA56D(e, boldness, heading);
                return;

            // ---------------------------------------------------------- 4, 5 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004ba56d..0x004ba685</c> — <b>la charge</b>, posture 7, en onze temps puis deux
    /// phases. Voir le bandeau.
    /// </summary>
    private static void ChargeDunoeud_004BA56D(byte* e, int boldness, int heading)
    {
        short beat = *(short*)(e + 0x12);

        if (beat == 0) *(int*)(e + 0x5c) = 0;

        if (beat < 0x0a)
        {
            // Ghidra: 0x004ba58c — l'image est bridee a 8 tant que l'elan monte.
            if (*(short*)(e + 0x1a) >= 9) *(short*)(e + 0x1a) = 8;
            *(short*)(e + 0x18) = 3;

            beat++;
            *(short*)(e + 0x12) = beat;

            if (beat == 7) *(uint*)e |= 2;

            if (beat == 0x0a)
            {
                // Ghidra: 0x004ba5b1 — le bond, et le son 0x24.
                *(int*)(e + 0x54) += 0x14000;
                *(short*)(e + 0x12) = 0x64;
                *(int*)(e + 0x5c) = ((boldness + 0x100) * 5) << 8;
                *(uint*)e |= 3;
                Aud.PlayPositionalSound(0x24, 0x600, 0x1200, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
            }

            if (((heading + 0x100) & 0xfff) > 0x200)
            {
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness + 0x32
                    : -0x32 - boldness);
            }
        }

        if (*(short*)(e + 0x12) == 0x64)
        {
            // Ghidra: 0x004ba63c — la poussee perd UN QUART par image.
            int thrust = *(int*)(e + 0x5c);
            *(short*)(e + 0x18) = 3;
            thrust -= thrust >> 2;
            *(int*)(e + 0x5c) = thrust;

            if (*(short*)(e + 0x1a) >= 0x0e) *(short*)(e + 0x1a) = 0x0d;

            if ((thrust & unchecked((int)0xfffff000)) < 0xa000) *(short*)(e + 0x12) = 0x65;
        }

        if (*(short*)(e + 0x12) != 0x65) return;

        *(short*)(e + 0x18) = 3;
        *(uint*)e &= 0xfffffffd;

        if (*(short*)(e + 0x1a) == 0x0f) e[0x43] = 0;               // Ghidra: LAB_004ba682
    }

    /// <summary>Ghidra : les ecritures <c>mov [esi+18h],&lt;dword&gt;</c> du Dunœud portent sur
    /// QUATRE octets — elles posent l'animation ET remettent l'image a zero d'un seul coup.</summary>
    private static void StartDunoeudAnimation(byte* e, short animation)
    {
        *(short*)(e + 0x18) = animation;
        *(short*)(e + 0x1a) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004ba726..0x004ba7a1</c> — <b>la pose au sol</b> du Dunœud, la plus courte de la
    /// zone : pas de restauration de placement, et une marge de <c>0xc</c> unites quand la bete n'est
    /// pas en l'air. Un heurt la met en posture 1 et coupe sa poussee.
    /// </summary>
    private static void SettleDunoeud_004BA726(byte* e)
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

            // Ghidra: 0x004ba770 — l'atterrissage rouvre l'attente, mais pas au milieu d'un coup.
            if ((flags & 2) == 0 && airborne && e[0x43] != 0) e[0x43] = 0;

            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((contact & 0x2000ad82) == 0) return;

        if ((e[0] & 1) == 0)
        {
            e[0x42] = 1;
            e[0x43] = 1;
        }

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004ba7d3..0x004baaae</c> — <b>le troisieme temps</b> du Dunœud, trois plages
    /// (<c>0xFA</c>, <c>0x258</c>).
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
    private static void ReplanDunoeudAction_004BA7D3(byte* e, int boldness, int timid)
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
                    StartDunoeudAnimation(e, 3);
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                // Ghidra: LAB_004ba896 — le cone large arme la charge.
                if (((heading + timid * 4 + 0x200) & 0xfff) > timid * 8 + 0x400)
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004baa59
                    e[0x43] = 1;
                    return;
                }

                e[0x42] = 7;
                *(uint*)e &= 0xfeffffff;
                e[0x43] = 1;
                StartDunoeudAnimation(e, 3);
                *(short*)(e + 0x12) = 0;
                return;
            }

            if (roll < 0x46)
            {
                ApproachDunoeudTarget_004BA9E5(e, timid);
                return;
            }

            if (roll < 0x50)
            {
                e[0x42] = 3;                                        // Ghidra: 0x004ba8ec
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                *(short*)(e + 0x10) = (short)(DunoeudJitter(boldness) + heading + *(short*)(e + 0x32) + 0x800);
                return;
            }

            LeapDunoeud_004BAA9E(e);
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
                    SleepDunoeud_004BAA24(e, timid);
                    return;
                }

                e[0x42] = 1;
                e[0x43] = 1;
                return;
            }

            if (roll < 0x32)
            {
                // Ghidra: LAB_004ba989 — le meme louvoiement, mais SANS le demi-tour.
                e[0x42] = 3;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                *(short*)(e + 0x10) = (short)(DunoeudJitter(boldness) + heading + *(short*)(e + 0x32));
                return;
            }

            if (roll < 0x5a)
            {
                ApproachDunoeudTarget_004BA9E5(e, timid);
                return;
            }

            LeapDunoeud_004BAA9E(e);
            return;
        }

        // ================================================================ de loin
        int far = CrtRand() % 100;

        if (far < 0x0a)
        {
            SleepDunoeud_004BAA24(e, timid);
            return;
        }

        if (far < 0x32)
        {
            e[0x42] = 1;                                            // Ghidra: LAB_004baa59
            e[0x43] = 1;
            return;
        }

        if (far < 0x5a)
        {
            e[0x42] = 3;                                            // Ghidra: LAB_004baa69
            e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            *(short*)(e + 0x10) = (short)CrtRand();
            return;
        }

        LeapDunoeud_004BAA9E(e);
    }

    /// <summary>L'amplitude du louvoiement : <c>±(hardiesse + 0x80) * 8</c>.</summary>
    private static int DunoeudJitter(int boldness)
    {
        int span = (boldness + 0x80) << 4;
        return CrtRand() % span - (span >> 1);
    }

    /// <summary>Ghidra: <c>LAB_004ba9e5</c> — l'approche, portee 0x64.</summary>
    private static void ApproachDunoeudTarget_004BA9E5(byte* e, int timid)
    {
        e[0x42] = 2;
        *(short*)(e + 0x10) = 0x64;
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
    }

    /// <summary>Ghidra: <c>LAB_004baa24</c> — le sommeil, qui remet aussi l'animation a zero.</summary>
    private static void SleepDunoeud_004BAA24(byte* e, int timid)
    {
        e[0x42] = 0;
        StartDunoeudAnimation(e, 0);
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
        *(uint*)e |= 0x1000000;
    }

    /// <summary>Ghidra: <c>LAB_004baa9e</c> — le bond vers un cap tire.</summary>
    private static void LeapDunoeud_004BAA9E(byte* e)
    {
        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }
}
