// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE CHEVALIER NOIR — 0x004AF560, vieille tour (zone 4, sous-zones 5, 6, 8, 10), et sa copie
// 0x004B5AF0, le Platinum (sous-zones 3, 7, 9).
//
// Le Pourpre preux du chateau (`Ai/ImmortalCastle/PurpleKnight.cs`) retouche. Sa replanification et
// sa parade sont celles du Pourpre preux A L'INSTRUCTION PRES et sont appelees telles quelles ; seuls
// le premier temps et le prelude de geste sont ecrits ici.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004AF560 — LE CHEVALIER NOIR, et 0x004B5AF0 — LE PLATINUM. Transcrits le 2026-09-17 au
    // desassembleur (Legacy/Raw/Blaze/OldTower/FUN_004AF560.asm, absente de Blaze.c), table de sauts
    // a quatorze entrees a 0x004B05C4.
    //
    // LES DEUX SONT LA MEME ROUTINE. 0x004B5AF0 ne differe de 0x004AF560 que par l'ordre de quelques
    // instructions de l'integration (memes termes, memes emplacements de pile) et par la copie du
    // chercheur de proie qu'elle appelle (0x004B3290 contre 0x004AC860, tous deux conformes a
    // FUN_004A1020) ; les deux tables de sauts sont identiques en relatif.
    //
    // CONTRE LE POURPRE PREUX (0x004D1F70), UN DIFF NORMALISE DE 408 LIGNES — dont presque toutes sont
    // des registres permutes (EBP porte la timidite ici, zero la-bas) et des emplacements de pile
    // renumerotes (hardiesse, distance et timidite tournent entre 0x10, 0x14 et 0x18). Ce qui reste :
    //
    //   - IL PARCOURT PLUS VITE : la poussee de marche est 0x7000 et non 0x5000 (postures 2, 3, 7) ;
    //   - IL SE TOURNE PLUS TOT : la posture 1 s'arrete a 0x80 du cap au lieu de 0xc8 ;
    //   - SES COUPS SPECIAUX FRAPPENT A UN QUART : le combo et la charge prennent chacun
    //     `attaque >> 2`, la ou le Pourpre preux frappait a trois quarts et a moitie ;
    //   - LE COUP (0x0b) REND LE REBOURS, pas la posture 0 : `e[0x43] = 0` au lieu de `e[0x42] = 0` ;
    //   - LE COMBO FAIT NAITRE 0xBA a l'image 6 de son second coup, et ce second coup reprend
    //     l'animation 5 a l'image 5 au lieu de 7 ;
    //   - LA POSTURE 10 ne connait qu'un effet : 0x36 pour la variante 1, rien pour les autres ;
    //   - LA CHARGE s'elance a `(variante + 6) * 5 << 12` au lieu de `(variante + 15) << 13`, et UN
    //     CHEVALIER DE VARIANTE 0 NE CHARGE PAS : a l'armement, il se rabat sur l'approche (posture 2)
    //     avec un rebours de `hasard % 10 + timidite / 2 + 20`.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004af560</c> (et <c>0x004b5af0</c>) — <b>l'intelligence du Chevalier noir</b>.
    /// La charpente de <see cref="TickSceneEntityAi_004D1F70"/> au mot pres, avec son propre premier
    /// temps.
    /// </summary>
    private static void TickSceneEntityAi_004AF560(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x34] et [esp+0x40] — les deux sondes, les memes que celles du Pourpre preux.
        short* probes = stackalloc short[12];
        probes[0] = 0; probes[1] = 0x1e; probes[2] = 0x32; probes[3] = 0x64; probes[4] = 0; probes[5] = 0x64;
        probes[6] = 0; probes[7] = 0; probes[8] = 0x50; probes[9] = 0x64; probes[10] = 0; probes[11] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004ac860 (FUN_004b3290), copies de FUN_004a1020
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004af699 — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideBlackKnightAction_004AF560(e, probes, model, boldness, timid, distance, heading, speed);
        }

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: 0x004affb6 — le pas d'avance reste dans EBX pour la parade, comme chez le Pourpre preux.
        int registerEbx = (speed * *(int*)(e + 0x5c)) >> 0xc;

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

        if (e[0x43] == 0)
        {
            // Ghidra: 0x004b01c5..0x004b053e — identique a 0x004d2b81..0x004d2efa.
            registerEbx = ReplanPurpleKnightAction_004D1F70(e, boldness, timid, &distance, &heading);
        }

        // Ghidra: 0x004b053f..0x004b05b7 — identique a LAB_004d2efb.
        TryPurpleKnightGuard_004D2EFB(e, chased, timid, heading, distance, registerEbx);
    }

    /// <summary>
    /// Ghidra: <c>0x004af6e3..0x004aff80</c> — <b>le premier temps</b> du Chevalier noir. Les
    /// quatorze cases de <see cref="DecidePurpleKnightAction_004D1F70"/>, onze servies ; les
    /// differences sont signalees case par case.
    /// </summary>
    private static void DecideBlackKnightAction_004AF560(
        byte* e, short* probes, byte* model, int boldness, int timid, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le prelude de geste
        if ((flags & 2) != 0)
        {
            BlackKnightGesture_004AF763(e, probes, model);
        }
        else if ((flags & 1) != 0 && *(short*)(e + 0x1a) < 3)
        {
            if (CrtRand() % 0x3c < boldness)                        // Ghidra: FUN_00547140
            {
                *(ushort*)(e + 0x18) = 3;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(ushort*)(e + 0x1a) = 0;
            }
        }

        // ================================================================ la posture
        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
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

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                }

                // Ghidra: 0x004afa4f — 0x80 et non 0xc8 : il cesse de tourner plus tot.
                if (((heading + 0x80) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                }
                else
                {
                    *(ushort*)(e + 0x32) +=
                        (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                }

                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x1a) = 0;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x80 : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(ushort*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x7000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(ushort*)(e + 0x32) += (ushort)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(ushort*)(e + 0x18) = 1;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 5 : parer
            case 5:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 1 && e[0x43] != 0 && (*(uint*)e & 0x2000000) != 0) e[0x43]--;

                if (phase != 0) return;

                *(ushort*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                *(ushort*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                *(ushort*)(e + 0x18) = 6;
                return;
            }

            // ---------------------------------------------------------- 6 : bondir
            case 6:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    uint f = *(uint*)e;

                    if ((f & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        int thrust = (boldness + 0x100) << 6;
                        int step = (speed * thrust) >> 0xc;
                        *(int*)(e + 0x5c) = thrust;

                        *(uint*)e = f & 0xfeffffff;
                        *(ushort*)(e + 0x18) = 1;
                        *(ushort*)(e + 0x1a) = 0;

                        short course = *(short*)(e + 0x10);
                        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 1;
                        *(ushort*)(e + 0x12) = 1;
                    }
                }

                if ((*(uint*)e & 1) != 0) return;
                e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 7 : marcher droit
            case 7:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 1)
                {
                    if (distance < *(short*)(e + 0x10)) e[0x43] = 0;
                    else if (e[0x43] != 0) e[0x43]--;
                }

                if (phase != 0) return;

                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x18) = 1;
                *(ushort*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;
                return;
            }

            // ---------------------------------------------------------- 10 : l'effet
            case 10:
                // Ghidra: 0x004afd5e — un seul effet, 0x36, et pour la variante 1. Le Pourpre preux
                // faisait naitre 0x36 pour la variante 2 et 0x4b pour la 1.
                if (*(ushort*)(e + 0x12) == 1 && *(short*)(e + 0x1a) == 8 && *(short*)(e + 0x16) == 1)
                {
                    SpawnEffectEntity(e, 0x36, 0, -1);                  // Ghidra: FUN_0047a9d0
                }

                if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;

                if (*(ushort*)(e + 0x12) != 0) return;

                *(uint*)e &= 0xfeffffff;
                *(ushort*)(e + 0x12) = 1;
                *(ushort*)(e + 0x18) = 5;
                *(ushort*)(e + 0x1a) = 0;

                if (boldness < 0x1e) *(int*)(e + 0x5c) = 0;

                if (*(short*)(e + 0x16) == 0) e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 11 : le coup
            case 11:
            {
                ushort phase = *(ushort*)(e + 0x12);

                // Ghidra: 0x004afde7 — le rebours, et non la posture 0 comme chez le Pourpre preux.
                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                if (phase != 0) return;

                *(ushort*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 12 : le combo
            case 12:
                if (*(ushort*)(e + 0x12) == 2)
                {
                    // Ghidra: 0x004afe17 — l'etincelle du second coup, absente chez le Pourpre preux.
                    if (*(short*)(e + 0x1a) == 6) SpawnEffectEntity(e, 0xba, 0, -1);   // Ghidra: FUN_0047a9d0

                    if ((*(uint*)e & 0x2000000) != 0)
                    {
                        *(uint*)e &= 0xfffffffd;
                        e[0x43] = 0;
                    }
                }

                // Le binaire RELIT +0x12 ici ; le Pourpre preux reprenait la valeur d'entree.
                if (*(ushort*)(e + 0x12) == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfeffffff;
                    *(ushort*)(e + 0x12) = 2;
                    *(ushort*)(e + 0x18) = 5;
                    *(ushort*)(e + 0x1a) = 5;
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(ushort*)(e + 0x12) = 1;
                *(ushort*)(e + 0x18) = 3;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 13 : la charge
            case 13:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 2)
                {
                    int v = *(int*)(e + 0x5c);
                    v -= v >> 4;
                    *(int*)(e + 0x5c) = v;

                    if (v < 0xa000)
                    {
                        e[0x43] = 0;
                        *(uint*)e &= 0xfffffffd;
                        *(int*)(e + 0x5c) = 0;
                    }
                }

                if (phase == 1)
                {
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(ushort*)(e + 0x32) += (ushort)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }

                    if (*(short*)(e + 0x1a) >= 4)
                    {
                        // Ghidra: 0x004aff17 — `(variante + 6) * 5 << 12`.
                        *(ushort*)(e + 0x12) = 2;
                        *(int*)(e + 0x5c) = ((*(short*)(e + 0x16) + 6) * 5) << 0xc;
                        *(uint*)e |= 2;
                    }
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(ushort*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                *(ushort*)(e + 0x18) = 4;
                *(ushort*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;

                if (*(short*)(e + 0x16) != 0) return;

                // Ghidra: 0x004aff5c — la variante 0 renonce a la charge et approche.
                e[0x42] = 2;
                int roll = CrtRand();                                   // Ghidra: FUN_00547140
                *(ushort*)(e + 0x10) = 0x50;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = (byte)(roll % 10 + (byte)(timid >> 1) + 0x14);
                return;
            }

            // ---------------------------------------------------------- 4, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004af763..0x004af988</c> — <b>le prelude de geste</b> du Chevalier noir. Les trois
    /// blocs de <see cref="PurpleKnightGesture_004D2175"/>, a deux fractions pres : <b>le combo et la
    /// charge frappent chacun a un quart de l'attaque</b> (<c>sar ax,2</c>).
    /// </summary>
    private static void BlackKnightGesture_004AF763(byte* e, short* probes, byte* model)
    {
        byte posture = e[0x42];

        if (posture != 0x0c && posture != 0x0d)
        {
            if (*(short*)(e + 0x1a) == 8)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 8)
                {
                    *(ushort*)(prey + 0x96) = 0x10;
                    *(uint*)prey |= 0x40;

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                }
            }

            uint flags = *(uint*)e;
            if ((flags & 0x2000000) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                if ((flags & 1) == 0) e[0x43] = 0;
            }
        }

        if (e[0x42] == 0x0c && ComboStrikeFrame(e))
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && ComboStrikeFrame(e))
            {
                if (*(ushort*)(e + 0x12) == 1)
                {
                    *(ushort*)(prey + 0x96) = 0;
                }
                else
                {
                    *(ushort*)(prey + 0x96) = 0x10;
                    *(uint*)prey |= 0x40;
                }

                short power = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(power >> 2);
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                *(short*)(model + 0x30) = power;

                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
            }
        }

        if (e[0x42] != 0x0d) return;

        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes + 6, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(victim + 0x96) = 0x10;
        *(uint*)victim |= 0x40;

        short attack = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(attack >> 2);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
        *(short*)(model + 0x30) = attack;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));

        if ((*(uint*)victim & 0x40000000) == 0)
        {
            SpawnEffectEntity(e, 0x39, 0, -1);                      // Ghidra: FUN_0047a9d0
            *(uint*)victim |= 1;

            short course = *(short*)(e + 0x32);
            *(int*)(victim + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * (*(int*)(e + 0x5c) >> 0xd);
            *(int*)(victim + 0x54) = 0x18000;
            *(int*)(victim + 0x58) = Winsys3d.GetCosTableEntry(course) * (*(int*)(e + 0x5c) >> 0xd);
        }

        *(uint*)e &= 0xfffffffd;
    }
}
