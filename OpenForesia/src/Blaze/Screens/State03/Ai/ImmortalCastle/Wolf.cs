// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE LOUP (ET LE LOUP GRIS) — 0x004CC650, chateau de l'immortel (zone 6, sous-zones 0 a 4), et ses
// deux copies : 0x004D4E10 (chateau, sous-zones 5 et 11) et 0x004DDE30 (ruines souterraines,
// sous-zones 0 a 2).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004CC650 — LE LOUP. Transcrit le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004CC650.asm, 934 instructions, absente de Blaze.c),
    // table de sauts a douze entrees a 0x004CD230.
    //
    // TROIS CORPS D'UN COUP. 0x004D4E10 et 0x004DDE30 ne different de 0x004CC650 que par
    // l'allocation des registres de l'integration (meme calcul), par les adresses de leur table de
    // sauts et par leur chercheur de proie — 0x004C8D80, 0x004D5A20, 0x004DC4D0, trois copies
    // de FUN_004a1020 aux etiquettes pres.
    //
    // UNE LECTURE NEUVE SUR LA CHARPENTE COMMUNE. Aucune routine ecrite n'en est proche (la plus
    // proche, le Gremlin de la grotte, est a 537 lignes sur 960) ; mais la tete (sonde, situation,
    // distance a trois dimensions) et la queue (integration FUN_00488160, pose au sol, heurt au
    // masque 0x2000ad82) sont celles de la famille, a deux details pres dans la queue : le heurt en
    // l'air fait tomber le bit d'attaque, et le heurt au sol ne garde la marche en cours que pour
    // l'espece 11 (voir SettleSceneEntityOnGroundAndWalls). Ce qui fait le loup :
    //
    //   - IL NE S'ARRETE JAMAIS POUR SE TOURNER : la posture 1 pose une poussee de 0x5000 pendant
    //     qu'il vire, la ou toute la famille la met a zero ;
    //   - IL COURT : approche et marche a 0xc000 plus 0x90 fois la hardiesse (la famille : 0x5000),
    //     louvoiement a 0x6000 ;
    //   - DEUX ATTAQUES QUI PARTENT A L'IMAGE 10 : le coup (posture 11), qu'un loup hardi (plus de
    //     50) lance directement a l'image 10, et LE BOND (posture 6), qui est ici une attaque :
    //     bit 2 pose, 0x14000 de vitesse verticale et 0x14000 plus huit fois la hardiesse vers
    //     l'avant. La sonde de coup travaille a chaque image passe la dixieme, tant que le bit
    //     d'attaque est pose — en l'air compris ;
    //   - AU CONTACT, IL TOURNE AUTOUR : a moins de 100 unites, la replanification de pres est
    //     ecrasee par un louvoiement court (5 a 9 images).
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004cc650</c> (et ses copies <c>0x004d4e10</c>, <c>0x004dde30</c>) —
    /// <b>l'intelligence du Loup</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004CC650(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x58] — la sonde, celle de la famille.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004c8d80 (FUN_004d5a20, FUN_004dc4d0), copies de FUN_004a1020
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

        // Ghidra: 0x004cc751 — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideWolfAction_004CC79E(e, probe, boldness, distance, heading, speed);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep,
                                          dropAttackOnAirborneHit: true, keepWalkingSpecies: 0x0b);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanWolfAction_004CCEAE(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004cc79e..0x004ccc7a</c> — <b>le premier temps</b> du Loup. Douze cases, dont
    /// sept servies : 0 attendre, 1 se tourner en marchant, 2 approcher, 3 louvoyer, 6 bondir, 7
    /// marcher droit, 11 le coup.
    /// </summary>
    private static void DecideWolfAction_004CC79E(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ la sonde, des l'image 10
        // Ghidra: 0x004cc822 — `cmp [esi+1Ah],bx ; jl` avec bx = 10 : la sonde balaie A CHAQUE
        // image a partir de la dixieme, et ne frappe qu'a la dixieme. Pas de fin de geste ici : ce
        // sont les postures 6 et 11 qui levent le bit d'attaque.
        if ((e[0] & 2) != 0 && *(short*)(e + 0x1a) >= 10)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 10)
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

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner en marchant
            case 1:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                }

                // Ghidra: 0x004cc914 — la poussee reste a 0x5000 : il vire sans s'arreter.
                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0x5000;
                    return;
                }

                *(int*)(e + 0x5c) = 0x5000;
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    if (distance < *(short*)(e + 0x10))
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        if (e[0x43] != 0) e[0x43]--;

                        if (((heading + 0x100) & 0xfff) > 0x200)
                        {
                            *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                                ? boldness * 2 + 0x32
                                : -0x32 - boldness * 2);
                        }
                    }
                }

                // Ghidra: 0x004cc9c6 — c'est la phase LUE A L'ENTREE qui est testee, pas relue.
                if (phase != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0xc000;
                return;
            }

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

                *(short*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 6 : bondir, en attaquant
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
                        *(short*)(e + 0x18) = 3;
                        *(short*)(e + 0x1a) = 0;
                        *(short*)(e + 0x12) = 1;
                    }
                }

                if (*(short*)(e + 0x12) == 1 && *(short*)(e + 0x1a) >= 10)
                {
                    *(int*)(e + 0x54) += 0x14000;
                    *(short*)(e + 0x12) = 2;

                    int thrust = boldness * 8 + 0x14000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(uint*)e |= 3;
                }

                if (*(short*)(e + 0x12) == 2 && (*(uint*)e & 1) == 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }
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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0xc000;
                return;
            }

            // ---------------------------------------------------------- 11 : le coup
            case 11:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x1a) = 0;
                    *(uint*)e &= 0xfeffffff;
                    *(short*)(e + 0x12) = 1;
                    *(short*)(e + 0x18) = 3;

                    // Ghidra: 0x004ccc05 — le loup hardi saute directement a l'image de frappe.
                    if (boldness > 0x32) *(short*)(e + 0x1a) = 10;
                }

                if (*(short*)(e + 0x12) == 1 && *(short*)(e + 0x1a) >= 10)
                {
                    *(uint*)e |= 2;
                    *(short*)(e + 0x12) = 2;
                    *(int*)(e + 0x5c) = 0x1e000;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }
                }

                if (*(short*)(e + 0x12) == 2 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }
                return;

            // ---------------------------------------------------------- 4, 5, 8, 9, 10 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004ccfed</c> — <b>le louvoiement court</b> du Loup, celui de pres : cinq a
    /// neuf images, sans la timidite. Les louvoiements de mi-distance et de loin sont ceux de la
    /// famille (<see cref="StrafeSalamander"/>).
    /// </summary>
    private static void StrafeWolfClose(byte* e)
    {
        e[0x42] = 3;
        *(short*)(e + 0x12) = 0;
        e[0x43] = (byte)(CrtRand() % 5 + 10);
        *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
    }

    /// <summary>
    /// Ghidra: <c>0x004cceae..0x004cd22f</c> — <b>le troisieme temps</b> du Loup. Le bit d'attaque
    /// tombe d'abord ; puis trois plages, la premiere plus courte que dans la famille (<c>0xe6</c>
    /// au lieu de <c>0xfa</c>).
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : le coup dans le cone et a portee sur un tirage sous 30 ; une courte
    /// attente sur un tirage de 30 a 49 ; se tourner sinon — mais seulement s'il ne se tournait pas
    /// deja. Les tirages de 60 a 69 bondissent au-dela de <c>hardiesse + 100</c> ; le reste
    /// louvoie court. <b>Et sous 100 unites, tout cela est ecrase</b> par un louvoiement court.</item>
    /// <item><b>A mi-distance</b> : se tourner, approcher, bondir, louvoyer, ou attendre.</item>
    /// <item><b>De loin</b> : un tirage sous 30 attend — ou approche, si le tirage est sous
    /// <c>2 × hardiesse + 10</c> ; puis louvoyer, se tourner, approcher, bondir.</item>
    /// </list>
    /// </summary>
    private static void ReplanWolfAction_004CCEAE(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;
        *(uint*)e &= 0xfffffffd;

        // ================================================================ de pres
        if (distance < 0xe6)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 100)
                {
                    if (roll < 0x1e)
                    {
                        e[0x42] = 0x0b;
                        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                        e[0x43] = 1;
                        *(int*)(e + 0x5c) = 0;
                        *(short*)(e + 0x18) = 3;
                        *(short*)(e + 0x1a) = 0;
                    }
                    else
                    {
                        e[0x42] = 0;
                        int wait = CrtRand() % 10;
                        *(short*)(e + 0x10) = 0x50;
                        *(short*)(e + 0x12) = 0;
                        e[0x43] = (byte)(wait + (timid >> 2));
                    }
                }
                else if (e[0x42] != 1)
                {
                    e[0x42] = 1;
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
                StrafeWolfClose(e);
            }

            // Ghidra: 0x004cd023 — au contact, le louvoiement court ecrase ce qui precede.
            if (distance < 100) StrafeWolfClose(e);
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0xe6)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                e[0x42] = 1;
                e[0x43] = 1;
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
                *(short*)(e + 0x10) = 0x50;
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
                StrafeSalamander(e, timid);                         // Ghidra: 0x004cd0e0
            }
            else
            {
                e[0x42] = 0;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 0x50;
            }
        }

        // ================================================================ de loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x1e && far >= boldness * 2 + 10)
        {
            e[0x42] = 0;
            int rest = CrtRand() % 10;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(rest + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x1e)
        {
            ApproachWolf_004CD1EE(e, timid);                        // Ghidra: 0x004cd157, le hardi
            return;
        }

        if (far < 0x28)
        {
            StrafeSalamander(e, timid);                             // Ghidra: 0x004cd194
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
            ApproachWolf_004CD1EE(e, timid);
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }

    /// <summary>Ghidra: <c>LAB_004cd1ee</c> — approcher de loin, a portee 100.</summary>
    private static void ApproachWolf_004CD1EE(byte* e, int timid)
    {
        e[0x42] = 2;
        int wait = CrtRand() % 10;
        *(short*)(e + 0x10) = 100;
        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
    }
}
