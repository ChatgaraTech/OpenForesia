// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LORDEMON — 0x00501840, grotte scellee (zone 10, sous-zones 13 a 15).
//
// Le nom vient de Comportement.md. 2 512 octets, absents de Blaze.c, transcrits au desassembleur
// le 2026-09-19 (lot 3 de la grotte) : Legacy/Raw/Blaze/SealedCave/FUN_00501840.asm. Trois tables
// de sauts : 0x005021d8 (gestes), 0x005021e8 (incantation), 0x005021f8 (postures).
//
// LA CHARPENTE D'EIFRIDT (Ai/SealedCave/Eifridt.cs) : meme gravite 0x6000, meme incantation a
// quatre phases (l'animation 7 cette fois, le sort a l'image 14), memes postures — sans le bruit
// de pas. Ses QUATRE gestes sont a lui :
//
//     geste 0  animation 3   LE BOND du Cerbere, plus haut (0x30000 devant, 0x18000 vers le
//                            haut) ; en l'air il frappe (sonde 0x005530e0) : en montant a chaque
//                            image, l'animation figee a l'image 7 ; en retombant, tant qu'elle
//                            n'a pas depasse la septieme
//     geste 1  animation 9   a l'image 12, l'entite de l'octet 0x08 du bloc de zone et le script
//                            0x00553118 ; puis vingt images d'arret. Sa phase est portee par +0x43
//     geste 2  animation 5   en fin d'animation, l'entite 0x12, nee a l'attache 0 du modele
//                            (+0x34 = 0xf80, +0x5c = 0x46), et le script 0x00553160 ; 20 images
//     geste 3  animation 7   a l'image 10 le script 0x00553100 ; en fin d'animation l'entite 0x0e,
//                            +0x44 tire de 2 a 8 et +0x48 = 0x3a98 ; 10 images
//
// SES SORTS : Feu d'enfer (0/4) ou Colere (0/26), a pile ou face (0x005530f8).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>Ghidra: <c>&amp;DAT_005530f8</c> — <b>les sorts de Lordemon</b> : 0/4 (Feu
    /// d'enfer), 0/26 (Colere). Pile ou face ; le mot suivant est nul.</summary>
    private static readonly ushort[] s_LordemonSpells_005530F8 = [0x0004, 0x001a];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00553100</c> — le script du geste 3 : trois fois la voix <c>0x15</c>.
    /// Borne par le <c>[1] = 0</c> du troisieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00553100 =
    [
        0x15, 0x01, 0x01, 0x00, 0xd4, 0x02, 0x00, 0x18,   // 0x00553100
        0x15, 0x01, 0x01, 0x00, 0x29, 0x03, 0x00, 0x18,   // 0x00553108
        0x15, 0x00, 0x01, 0x00, 0x7e, 0x03, 0x00, 0x18,   // 0x00553110 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00553100Storage;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00553118</c> — le script du geste 1 : la voix <c>0x20</c>, puis huit fois
    /// la voix <c>0x11</c> a deux images d'intervalle, montant de hauteur. Borne par le
    /// <c>[1] = 0</c> du neuvieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00553118 =
    [
        0x20, 0x01, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x00553118
        0x11, 0x02, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00553120
        0x11, 0x02, 0x01, 0x00, 0xaa, 0x04, 0x00, 0x18,   // 0x00553128
        0x11, 0x02, 0x01, 0x00, 0x54, 0x05, 0x00, 0x18,   // 0x00553130
        0x11, 0x02, 0x01, 0x00, 0xa9, 0x05, 0x00, 0x18,   // 0x00553138
        0x11, 0x02, 0x01, 0x00, 0x53, 0x06, 0x00, 0x18,   // 0x00553140
        0x11, 0x02, 0x01, 0x00, 0xfd, 0x06, 0x00, 0x18,   // 0x00553148
        0x11, 0x02, 0x01, 0x00, 0xa7, 0x07, 0x00, 0x18,   // 0x00553150
        0x11, 0x00, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x00553158 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00553118Storage;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00553160</c> — le script du geste 2 : quatre fois la voix <c>0x20</c>, a
    /// cinq images d'intervalle. Borne par le <c>[1] = 0</c> du quatrieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00553160 =
    [
        0x20, 0x05, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553160
        0x20, 0x05, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553168
        0x20, 0x05, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553170
        0x20, 0x00, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553178 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00553160Storage;

    /// <summary>
    /// Ghidra: <c>0x00501840</c> — <b>l'intelligence de Lordemon</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_00501840(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);     // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideLordemonAction_0050189A(e, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_00501f2c — la gravite d'Eifridt.
        *(ushort*)(e + 0x9a) = 0x6000;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanLordemonAction_00501F86(e);
    }

    /// <summary>
    /// Ghidra: <c>0x0050189a..0x00501f2b</c> — <b>le premier temps</b>.
    /// </summary>
    private static void DecideLordemonAction_0050189A(byte* e, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ hors geste
        if ((flags & 2) == 0)
        {
            // Ghidra: LAB_00501c25
            if ((flags & 0x10) != 0) return;

            if ((flags & 0x804) != 0)
            {
                AdvanceLordemonIncantation_00501C38(e, heading, traits, flags);
                return;
            }

            if ((flags & 1) != 0) return;

            HoldLordemonStance_00501D84(e, distance, heading, flags);
            return;
        }

        if ((traits & 0x10) != 0)
        {
            e[0x43] = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        switch (e[0x42])
        {
            case 0: LordemonLeap_005018D6(e, heading, flags); return;
            case 1: LordemonCall_00501A23(e, flags); return;
            case 2: LordemonBreath_00501AC0(e, flags); return;
            case 3: LordemonSpit_00501B6E(e, flags); return;
            default: return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x005018d6</c> — <b>le bond</b>, geste 0, animation 3 : celui du Cerbere
    /// (<see cref="CerberusLeap_004F7C3B"/>) pour ses phases 2 et 1, plus haut et plus loin ; mais
    /// il frappe EN L'AIR, et non a la retombee.
    /// </summary>
    private static void LordemonLeap_005018D6(byte* e, int heading, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        switch (phase)
        {
            // ---------------------------------------------------------- 2 : se ramasser
            case 2:
            {
                int run = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = run - (run >> 2);
                if (*(short*)(e + 0x1a) == 5) *(short*)(e + 0x10) = (short)(phase - 1);

                TurnSceneEntityTowardTarget(e, 0x40, heading);
                return;
            }

            // ---------------------------------------------------------- 1 : sauter
            case 1:
                *(int*)(e + 0x5c) = 0x30000;
                *(int*)(e + 0x50) +=
                    ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x58) +=
                    ((int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c)) >> 0xc;

                *(short*)(e + 0x10) -= 1;
                *(int*)(e + 0x54) = 0x18000;
                *(uint*)e |= 1;                                      // en l'air
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 0 : en l'air, frapper
            case 0:
                break;

            default:
                return;
        }

        // Ghidra: LAB_00501996
        if ((flags & 1) == 0)
        {
            // Au sol : le geste se ferme en fin d'animation.
            if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
            return;
        }

        // En montant, l'animation est figee a l'image 7 ; en retombant, passe la septieme, il ne
        // frappe plus.
        if (*(int*)(e + 0x54) >= 0)
        {
            if (*(short*)(e + 0x1a) > 7) *(short*)(e + 0x1a) = 7;
        }
        else if (*(short*)(e + 0x1a) > 7)
        {
            return;
        }

        short* probe = stackalloc short[6];                          // Ghidra: &DAT_005530e0
        probe[0] = 0; probe[1] = 0; probe[2] = 0x3c; probe[3] = 0x96; probe[4] = 0; probe[5] = 0x96;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(uint*)prey |= 0x40;
        *(ushort*)(prey + 0x96) = 0x10;
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x00501a23</c> — <b>l'appel</b>, geste 1, animation 9. Sa phase n'est pas
    /// <c>+0x10</c> mais <c>+0x43</c>, posee a 2 par la replanification ; <c>+0x10</c> y sert de
    /// rebours. Il ne touche jamais <c>+0x5c</c>.
    /// </summary>
    private static void LordemonCall_00501A23(byte* e, uint flags)
    {
        byte phase = e[0x43];

        if (phase == 2)
        {
            if (*(short*)(e + 0x1a) == 0x0c)
            {
                SpawnEffectEntity(e, ZombieKingSummonSubtype(0x08), 0xff, -1);   // Ghidra: FUN_0047a9d0
                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_00553118, ref s_DelayedSoundScript00553118Storage),
                    (int*)(e + 0x1c));                               // Ghidra: FUN_0053dbe0
            }

            // Ghidra: LAB_00501a80
            if ((*(uint*)e & 0x2000000) == 0) return;

            *(short*)(e + 0x10) = 0x14;
            e[0x43]--;
            return;
        }

        if (phase != 1) return;

        // Ghidra: LAB_00501a9f
        if (*(short*)(e + 0x10) != 0)
        {
            *(short*)(e + 0x10) -= 1;
            return;
        }

        *(uint*)e = flags & 0xfffffffd;
        e[0x43] = (byte)(phase - 1);
    }

    /// <summary>
    /// Ghidra: <c>LAB_00501c07</c> — <b>le temps mort apres un jet</b>, commun aux gestes 2 et 3.
    /// </summary>
    private static void CountDownLordemonGesture_00501C07(byte* e, uint flags)
    {
        e[0x43]--;
        if (e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00501ac0</c> — <b>le souffle</b>, geste 2, animation 5 : en fin d'animation.
    /// </summary>
    private static void LordemonBreath_00501AC0(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            CountDownLordemonGesture_00501C07(e, flags);
            return;
        }

        if (phase == 1 && (flags & 0x2000000) != 0)
        {
            short savedPitch = *(short*)(e + 0x34);
            int savedRun = *(int*)(e + 0x5c);

            *(short*)(e + 0x34) = 0x0f80;
            *(int*)(e + 0x5c) = 0x46;

            short* anchor = ModelAnchorRow_00535770(e, 0);          // Ghidra: FUN_00535770
            *(int*)(e + 0x44) = anchor[0] << 0xc;
            *(int*)(e + 0x48) = anchor[1] << 0xc;
            *(int*)(e + 0x4c) = anchor[2] << 0xc;

            SpawnEffectEntity(e, ZombieKingSummonSubtype(0x12), 0, 4);   // Ghidra: FUN_0047a9d0

            *(short*)(e + 0x10) -= 1;
            *(short*)(e + 0x34) = savedPitch;
            *(int*)(e + 0x5c) = savedRun;
            e[0x43] = 0x14;

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_DelayedSoundScript_00553160, ref s_DelayedSoundScript00553160Storage),
                (int*)(e + 0x1c));                                   // Ghidra: FUN_0053dbe0

            *(int*)(e + 0x5c) = 0;
            return;
        }

        *(int*)(e + 0x5c) = 0;                                       // Ghidra: LAB_00501e1b
    }

    /// <summary>
    /// Ghidra: <c>0x00501b6e</c> — <b>le jet</b>, geste 3, animation 7 : le script a l'image 10,
    /// l'entite <c>0x0e</c> en fin d'animation.
    /// </summary>
    private static void LordemonSpit_00501B6E(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            CountDownLordemonGesture_00501C07(e, flags);
            return;
        }

        if (phase == 1)
        {
            if (*(short*)(e + 0x1a) == 0x0a)
            {
                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_00553100, ref s_DelayedSoundScript00553100Storage),
                    (int*)(e + 0x1c));                               // Ghidra: FUN_0053dbe0
            }

            if ((*(uint*)e & 0x2000000) != 0)
            {
                int saved44 = *(int*)(e + 0x44);
                int saved48 = *(int*)(e + 0x48);

                int r = CrtRand();
                *(int*)(e + 0x48) = 0x3a98;
                *(int*)(e + 0x44) = r % 7 + 2;

                SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0e), 0, 2);   // Ghidra: FUN_0047a9d0

                *(short*)(e + 0x10) -= 1;
                *(int*)(e + 0x44) = saved44;
                *(int*)(e + 0x48) = saved48;
                e[0x43] = 0x0a;
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }

        *(int*)(e + 0x5c) = 0;                                       // Ghidra: LAB_00501e1b
    }

    /// <summary>
    /// Ghidra: <c>0x00501c38</c> — <b>l'incantation</b>, table <c>0x005021e8</c>. Celle d'Eifridt
    /// (<see cref="AdvanceEifridtIncantation_00500844"/>) sur l'animation 7, le sort a l'image 14.
    /// </summary>
    private static void AdvanceLordemonIncantation_00501C38(byte* e, int heading, uint traits, uint flags)
    {
        if ((traits & 0x400003e8) != 0)
        {
            flags &= 0xfffff7fb;
            e[0x43] = 0;
            *(uint*)e = flags;
        }

        short state = *(short*)(e + 0x10);
        if ((uint)state > 3) return;

        switch (state)
        {
            case 0:
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffff7ff;
                return;

            case 1:
                if (*(ushort*)(e + 0x18) != 7)
                {
                    *(int*)(e + 0x18) = 7;
                    *(uint*)e = *(uint*)e & 0xfefffffb;
                    return;
                }

                if (*(short*)(e + 0x1a) != 0x0e) return;

                {
                    ushort sheet = *(ushort*)(e + 6);
                    CastSpell_0053A980((uint*)e, (sheet >> 8) & 0xff, sheet & 0xff, -1, -1);
                }

                *(short*)(e + 0x10) -= 1;
                return;

            case 2:
                if (--e[0x43] == 0) *(short*)(e + 0x10) = (short)(state - 1);

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                return;

            default:
            {
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                ushort sheet = s_LordemonSpells_005530F8[CrtRand() % 2];
                *(ushort*)(e + 6) = sheet;

                byte* family = ((byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)))
                                   [(sheet >> 8) & 0xff];

                *(short*)(e + 0x10) -= 1;
                e[0x43] = family[(sheet & 0xff) * 0x30 + 0x13];
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00501d84..0x00501f2b</c> — <b>la posture tenue</b>, table <c>0x005021f8</c>.
    /// Celles d'Eifridt, sans le bruit de pas.
    /// </summary>
    private static void HoldLordemonStance_00501D84(byte* e, int distance, int heading, uint flags)
    {
        switch (e[0x42])
        {
            case 0:
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            case 1:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (TurnSceneEntityTowardTarget(e, 0x100, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return;

            case 2:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                TurnSceneEntityTowardTarget(e, 0x80, heading);
                *(int*)(e + 0x5c) = 0x14000;
                return;

            case 3:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                *(int*)(e + 0x5c) = 0x10000;
                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                int cos = (int)Winsys3d.GetCosTableEntry(course);
                int step = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = 0;
                *(int*)(e + 0x24) += (cos * step) >> 0xc;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                return;
            }

            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00501f86..0x005021d5</c> — <b>le troisieme temps</b>. Un tirage sur cent, fait
    /// avant de lire la distance.
    ///
    /// <para>
    /// <b>Sous 300</b> : la moitie des fois le bond (cone de 0x100, sinon pivoter), un quart
    /// l'approche a 200, le reste la marche. <b>Au-dela</b> : 40 % l'un des trois jets — le reste de
    /// ce meme tirage par 3 choisit l'appel, le souffle ou le jet — si la cible est dans le cone de
    /// 0x200 ; 20 % l'approche ; 20 % l'incantation (meme cone) ; 10 % la marche ; 10 % l'attente.
    /// </para>
    /// </summary>
    private static void ReplanLordemonAction_00501F86(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;

        if (distance < 0x12c)
        {
            if (roll < 0x32)
            {
                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    EifridtTurn(e);
                    return;
                }

                EifridtStartGesture(e, 3, 0, 2);
                return;
            }

            if (roll < 0x4b)
            {
                EifridtApproach(e);
                return;
            }

            EifridtWalk(e, heading);
            return;
        }

        // Ghidra: LAB_00502055
        if (roll < 0x28)
        {
            if (((heading + 0x200) & 0xfff) >= 0x400)
            {
                EifridtTurn(e);
                return;
            }

            switch (roll % 3)
            {
                case 0:
                {
                    // Ghidra: LAB_005020d8 — l'appel : sa phase est +0x43, pas +0x10.
                    uint flags = *(uint*)e;
                    *(int*)(e + 0x18) = 9;
                    e[0x42] = 1;
                    e[0x43] = 2;
                    *(uint*)e = (flags & 0xfeffffff) | 2;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                case 1:
                    EifridtStartGesture(e, 5, 2, 1);
                    return;

                default:
                    // Ghidra: `mov [esi+42h],cl` — le 3 du diviseur, encore dans ECX.
                    EifridtStartGesture(e, 7, 3, 1);
                    return;
            }
        }

        if (roll < 0x3c)
        {
            EifridtApproach(e);
            return;
        }

        if (roll < 0x50)
        {
            if (((heading + 0x200) & 0xfff) >= 0x400)
            {
                EifridtTurn(e);
                return;
            }

            *(int*)(e + 0x18) = 6;
            *(short*)(e + 0x10) = 3;
            *(uint*)e |= 0x1000804;
            return;
        }

        if (roll < 0x5a)
        {
            EifridtWalk(e, heading);
            return;
        }

        e[0x42] = 0;
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);
    }
}
