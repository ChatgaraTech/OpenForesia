// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ANGE NOIR — 0x00502210, grotte scellee (zone 10, sous-zones 13 a 15).
//
// Le nom vient de Comportement.md. 2 752 octets, absents de Blaze.c, transcrits au desassembleur
// le 2026-09-19 (lot 3 de la grotte) : Legacy/Raw/Blaze/SealedCave/FUN_00502210.asm. Quatre tables
// de sauts : 0x00502c7c (gestes), 0x00502c90 (incantation), 0x00502ca0 (postures), 0x00502cb0 (le
// choix du geste a distance).
//
// UNE BETE VOLANTE (Ai/Forest/Ladybug.cs) avec l'incantation des boss : integration par
// IntegrateFlyingEntityMotion_0049A8E0, le vol de la Coccinelle (BeatWings_0049A68F, drapeaux de
// vue compris), l'interruption InterruptFlyingEntity_004A1CF0. Ses postures glissent : l'attente et
// le pivot freinent d'un seizieme par image, l'approche accelere vers 0x10000 d'un seizieme.
//
//     geste 0  animation 3   LE PIQUE : a l'image 7, 0x28000 dans le cap ; puis tant que sa
//                            vitesse horizontale depasse 3, elle balaie la sonde 0x00553180 a
//                            chaque image, et le geste tombe quand elle passe sous 9 en fin
//                            d'animation
//     geste 1  animation 3   en fin d'animation, l'entite de l'octet 0x0b du bloc de zone, nee a
//                            l'attache 0 du modele — le souffle du Dragon zombie
//     geste 2  animation 6   a l'image 12 le script 0x005531a0 ; en fin d'animation l'entite 0x0e,
//                            +0x44 a pile ou face et +0x48 = 0x3a98
//     geste 3  animation 6   a l'image 1 le script 0x005531b8 ; aux images impaires puis en fin
//                            d'animation, le sous-type de l'octet 7 d'un AUTRE bloc de zone
//                            (+0xd4), par FUN_0047ac40 — +0x14 prete a 0, puis a 2
//     geste 4  animation 6   en fin d'animation, l'entite 0x16
//
// SES CINQ SORTS (0x00553190) : Eclair, Satanas, Fusion, Sang d'ange, Ange.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>Ghidra: <c>&amp;DAT_00553190</c> — <b>les sorts de l'Ange noir</b>, tires sur
    /// cinq : 0/16 Eclair, 0/24 Satanas, 0/28 Fusion, 1/11 Sang d'ange, 1/23 Ange. Le mot suivant
    /// est nul.</summary>
    private static readonly ushort[] s_BlackAngelSpells_00553190 = [0x0010, 0x0018, 0x001c, 0x010b, 0x0117];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005531a0</c> — le script du geste 2 : trois fois la voix <c>0x27</c>.
    /// Borne par le <c>[1] = 0</c> du troisieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_005531A0 =
    [
        0x27, 0x01, 0x01, 0x00, 0xff, 0x04, 0x00, 0x18,   // 0x005531a0
        0x27, 0x01, 0x01, 0x00, 0x54, 0x05, 0x00, 0x18,   // 0x005531a8
        0x27, 0x00, 0x01, 0x00, 0xa9, 0x05, 0x00, 0x18,   // 0x005531b0 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript005531A0Storage;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005531b8</c> — le script du geste 3 : huit fois la voix <c>0x29</c>, a
    /// quatre images d'intervalle. Borne par le <c>[1] = 0</c> du huitieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_005531B8 =
    [
        0x29, 0x04, 0x01, 0x00, 0x00, 0x01, 0x00, 0x18,   // 0x005531b8
        0x29, 0x04, 0x01, 0x00, 0xf5, 0x00, 0x00, 0x18,   // 0x005531c0
        0x29, 0x04, 0x01, 0x00, 0x00, 0x01, 0x00, 0x18,   // 0x005531c8
        0x29, 0x04, 0x01, 0x00, 0x94, 0x01, 0x00, 0x18,   // 0x005531d0
        0x29, 0x04, 0x01, 0x00, 0x55, 0x01, 0x00, 0x18,   // 0x005531d8
        0x29, 0x04, 0x01, 0x00, 0x2a, 0x01, 0x00, 0x18,   // 0x005531e0
        0x29, 0x04, 0x01, 0x00, 0xca, 0x00, 0x00, 0x18,   // 0x005531e8
        0x29, 0x00, 0x01, 0x00, 0xdf, 0x00, 0x00, 0x18,   // 0x005531f0 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript005531B8Storage;

    /// <summary>
    /// Ghidra: <c>0x00502210</c> — <b>l'intelligence de l'Ange noir</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_00502210(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);     // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideBlackAngelAction_00502270(e, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_005029aa
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        BeatWings_0049A68F(e);
        InterruptFlyingEntity_004A1CF0((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanBlackAngelAction_00502A71(e);
    }

    /// <summary>
    /// Ghidra: <c>0x00502270..0x005029a9</c> — <b>le premier temps</b>.
    /// </summary>
    private static void DecideBlackAngelAction_00502270(byte* e, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ hors geste
        if ((flags & 2) == 0)
        {
            // Ghidra: LAB_00502670 — une volante : pas de test du bit « en l'air ».
            if ((flags & 0x10) != 0) return;

            if ((flags & 0x804) != 0)
            {
                AdvanceBlackAngelIncantation_00502683(e, heading, traits, flags);
                return;
            }

            HoldBlackAngelStance_005027DF(e, distance, heading, flags);
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
            case 0: BlackAngelDive_005022B2(e, heading); return;
            case 1: BlackAngelBreath_005023F6(e, flags); return;
            case 2: BlackAngelSpit_005024A8(e, flags); return;
            case 3: BlackAngelRain_00502537(e, flags); return;
            case 4: BlackAngelCall_0050260A(e, flags); return;
            default: return;
        }
    }

    /// <summary>
    /// Ghidra: <c>LAB_00502656</c> — <b>le temps mort apres un jet</b>, commun aux gestes 1 a 4.
    /// </summary>
    private static void CountDownBlackAngelGesture_00502656(byte* e, uint flags)
    {
        e[0x43]--;
        if (e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;                                       // Ghidra: LAB_00502664
    }

    /// <summary>
    /// Ghidra: <c>0x005022b2</c> — <b>le pique</b>, geste 0. Phase 1 : elle se tourne (cone de
    /// <c>0x80</c>) et s'elance a l'image 7 ; phase 0 : elle frappe tant qu'elle file.
    /// </summary>
    private static void BlackAngelDive_005022B2(byte* e, int heading)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0;

            if (*(short*)(e + 0x1a) == 7)
            {
                *(int*)(e + 0x5c) = 0x28000;
                *(int*)(e + 0x50) +=
                    ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x58) +=
                    ((int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0x5000;
                *(short*)(e + 0x10) -= 1;
            }

            TurnSceneEntityTowardTarget(e, 0x80, heading);
            return;
        }

        if (phase != 0) return;

        // Ghidra: LAB_00502335 — la vitesse horizontale, en unites.
        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;
        int run = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));   // Ghidra: FUN_00545a10

        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0 && run <= 8) *(uint*)e = flags & 0xfffffffd;

        if (run < 3) return;

        TurnSceneEntityTowardTarget(e, 0x28, heading);

        short* probe = stackalloc short[6];                          // Ghidra: &DAT_00553180
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x78; probe[4] = 0; probe[5] = 0x5a;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(uint*)prey |= 0x40;
        *(ushort*)(prey + 0x96) = 0x10;
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x005023f6</c> — <b>le souffle</b>, geste 1 : en fin d'animation, l'entite
    /// <c>0x0b</c> nait a l'attache 0 du modele, portee par <c>+0x50..+0x58</c>, <c>+0x9a</c> mis
    /// a zero le temps de la naissance.
    /// </summary>
    private static void BlackAngelBreath_005023F6(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            CountDownBlackAngelGesture_00502656(e, flags);
            return;
        }

        if (phase == 1 && (flags & 0x2000000) != 0)
        {
            short savedLift = *(short*)(e + 0x9a);
            int savedX = *(int*)(e + 0x50);
            int savedY = *(int*)(e + 0x54);
            int savedZ = *(int*)(e + 0x58);

            *(short*)(e + 0x9a) = 0;
            short* anchor = ModelAnchorRow_00535770(e, 0);          // Ghidra: FUN_00535770
            *(int*)(e + 0x50) = anchor[0] << 0xc;
            *(int*)(e + 0x54) = anchor[1] << 0xc;
            *(int*)(e + 0x58) = anchor[2] << 0xc;

            SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0b), 0xff, -1);   // Ghidra: FUN_0047a9d0

            *(short*)(e + 0x10) -= 1;
            *(int*)(e + 0x50) = savedX;
            *(int*)(e + 0x54) = savedY;
            *(int*)(e + 0x58) = savedZ;
            *(short*)(e + 0x9a) = savedLift;
            e[0x43] = 0x0a;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x005024a8</c> — <b>le jet</b>, geste 2 : le script a l'image 12 ; en fin
    /// d'animation l'entite <c>0x0e</c>, <c>+0x44</c> a pile ou face, <c>+0x48 = 0x3a98</c>.
    /// </summary>
    private static void BlackAngelSpit_005024A8(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            CountDownBlackAngelGesture_00502656(e, flags);
            return;
        }

        if (phase == 1)
        {
            if (*(short*)(e + 0x1a) == 0x0c)
            {
                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_005531A0, ref s_DelayedSoundScript005531A0Storage),
                    (int*)(e + 0x1c));                               // Ghidra: FUN_0053dbe0
            }

            if ((*(uint*)e & 0x2000000) != 0)
            {
                int saved44 = *(int*)(e + 0x44);
                int saved48 = *(int*)(e + 0x48);

                int r = CrtRand();
                *(int*)(e + 0x48) = 0x3a98;
                *(int*)(e + 0x44) = r & 1;

                SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0e), 0, 2);   // Ghidra: FUN_0047a9d0

                *(short*)(e + 0x10) -= 1;
                *(int*)(e + 0x44) = saved44;
                *(int*)(e + 0x48) = saved48;
                e[0x43] = 0x0a;
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00502537</c> — <b>la pluie</b>, geste 3. Le sous-type ne vient pas du bloc
    /// <c>+0xcc</c> des autres jets mais du bloc <c>+0xd4</c> du repertoire de zone, octet 7, et il
    /// nait par <see cref="SpawnStatusEffectEntity_0047AC40"/> a la position de l'Ange, parametre 3.
    /// Une naissance a chaque image impaire, <c>+0x14</c> prete a 0 ; puis, en fin d'animation, une
    /// derniere avec <c>+0x14</c> prete a 2, et quinze images de temps mort.
    /// </summary>
    private static void BlackAngelRain_00502537(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            CountDownBlackAngelGesture_00502656(e, flags);
            return;
        }

        if (phase != 1)
        {
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (*(short*)(e + 0x1a) == 1)
        {
            Aud.QueueDelayedSoundScript(
                NativeCopy(s_DelayedSoundScript_005531B8, ref s_DelayedSoundScript005531B8Storage),
                (int*)(e + 0x1c));                                   // Ghidra: FUN_0053dbe0
        }

        if ((*(uint*)e & 0x2000000) != 0)
        {
            short saved = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 2;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, BlackAngelRainSubtype(), 0, 3);   // Ghidra: FUN_0047ac40

            *(short*)(e + 0x10) -= 1;
            *(short*)(e + 0x14) = saved;
            e[0x43] = 0x0f;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        // Ghidra: LAB_005025b9
        if ((*(byte*)(e + 0x1a) & 1) != 0)
        {
            short saved = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 0;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, BlackAngelRainSubtype(), 0, 3);   // Ghidra: FUN_0047ac40

            *(short*)(e + 0x14) = saved;
        }

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: <c>[[0x005e97cc] + 0xd4]</c>, octet 7, plus <c>0xc0</c> — le sous-type de
    /// la pluie, lu dans le second bloc du repertoire de zone (le <c>+0xcc</c> sert aux autres).</summary>
    private static byte BlackAngelRainSubtype()
    {
        byte* block = *(byte**)(s_Unknown_005e97cc + 0xd4);
        return (byte)((*(byte**)block)[7] + 0xc0);
    }

    /// <summary>
    /// Ghidra: <c>0x0050260a</c> — <b>l'appel</b>, geste 4 : en fin d'animation, l'entite
    /// <c>0x16</c> du bloc de zone, parametre 2 ; puis dix images.
    /// </summary>
    private static void BlackAngelCall_0050260A(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            CountDownBlackAngelGesture_00502656(e, flags);
            return;
        }

        if (phase == 1 && (flags & 0x2000000) != 0)
        {
            SpawnEffectEntity(e, ZombieKingSummonSubtype(0x16), 0, 2);   // Ghidra: FUN_0047a9d0

            *(short*)(e + 0x10) -= 1;
            e[0x43] = 0x0a;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00502683</c> — <b>l'incantation</b>, table <c>0x00502c90</c>. Celle d'Eifridt,
    /// l'animation 6 et le sort a l'image 23, sur cinq sorts.
    /// </summary>
    private static void AdvanceBlackAngelIncantation_00502683(byte* e, int heading, uint traits, uint flags)
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
                if (*(ushort*)(e + 0x18) != 6)
                {
                    *(int*)(e + 0x18) = 6;
                    *(uint*)e = *(uint*)e & 0xfefffffb;
                    return;
                }

                if (*(short*)(e + 0x1a) != 0x17) return;

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

                ushort sheet = s_BlackAngelSpells_00553190[CrtRand() % 5];
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
    /// Ghidra: <c>0x005027df..0x005029a9</c> — <b>la posture tenue</b>, table <c>0x00502ca0</c>.
    /// Une volante glisse : elle ne pose pas sa vitesse, elle la rapproche d'un seizieme de sa
    /// cible. Et l'approche tient la distance fixe de 300, pas le <c>+0x10</c>.
    /// </summary>
    private static void HoldBlackAngelStance_005027DF(byte* e, int distance, int heading, uint flags)
    {
        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : planer
            case 0:
            {
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

                int run = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = run - (run >> 4);
                return;
            }

            // ---------------------------------------------------------- 1 : pivoter
            case 1:
            {
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (TurnSceneEntityTowardTarget(e, 0x80, heading)) e[0x43] = 0;

                int run = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = run - (run >> 4);
                return;
            }

            // ---------------------------------------------------------- 2 : approcher
            case 2:
            {
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                TurnSceneEntityTowardTarget(e, 0x80, heading);

                int run = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = ((0x10000 - run) >> 4) + run;
                return;
            }

            // ---------------------------------------------------------- 3 : filer vers un cap
            case 3:
            {
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (e[0x43] != 0) e[0x43]--;

                // La poussee va a la VITESSE (+0x50 / +0x58), pas a la position.
                *(int*)(e + 0x5c) = 0x4000;
                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                int cos = (int)Winsys3d.GetCosTableEntry(course);
                int step = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = 0;
                *(int*)(e + 0x58) += (cos * step) >> 0xc;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                return;
            }

            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00502a71..0x00502c78</c> — <b>le troisieme temps</b>. Distance et cap en deux
    /// dimensions, un tirage sur cent.
    ///
    /// <para>
    /// <b>Sous 300</b> : 60 % le pique si la cible est dans le cone de 0x100 (sinon pivoter), le reste
    /// filer vers un cap tire. <b>Au-dela</b> : 30 % un geste si la cible est dans le meme cone — le
    /// reste du MEME tirage par cinq choisit l'incantation ou l'un des gestes 1 a 4 ; 60 % l'approche
    /// (sans distance posee : la posture tient 300) ; 10 % filer.
    /// </para>
    /// </summary>
    private static void ReplanBlackAngelAction_00502A71(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;
        bool inCone = ((heading + 0x100) & 0xfff) <= 0x200;

        if (distance < 0x12c)
        {
            if (roll >= 0x3c)
            {
                BlackAngelDrift(e, heading);
                return;
            }

            if (!inCone)
            {
                EifridtTurn(e);
                return;
            }

            EifridtStartGesture(e, 3, 0, 1);
            return;
        }

        // Ghidra: LAB_00502b16
        if (roll < 0x1e)
        {
            if (!inCone)
            {
                EifridtTurn(e);
                return;
            }

            switch (roll % 5)
            {
                case 0:
                    // Ghidra: 0x00502b52 — l'incantation.
                    *(int*)(e + 0x18) = 5;
                    *(short*)(e + 0x10) = 3;
                    *(uint*)e |= 0x1000804;
                    return;

                case 1: EifridtStartGesture(e, 3, 1, 1); return;
                case 2: EifridtStartGesture(e, 6, 2, 1); return;
                case 3: EifridtStartGesture(e, 6, 3, 1); return;
                default: EifridtStartGesture(e, 6, 4, 1); return;
            }
        }

        if (roll < 0x5a)
        {
            // Ghidra: LAB_00502c16 — l'approche, sans +0x10.
            e[0x42] = 2;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        BlackAngelDrift(e, heading);
    }

    /// <summary>
    /// Ghidra: <c>LAB_00502c3a</c> — <b>filer</b> vers un cap tire sur les trois quarts du tour, a
    /// partir de <c>-0x600</c>, pour 10 a 19 images.
    /// </summary>
    private static void BlackAngelDrift(byte* e, int heading)
    {
        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 10);
        int r = CrtRand() % 0xc00;
        *(short*)(e + 0x10) = (short)(r + heading + *(ushort*)(e + 0x32) - 0x600);
    }
}
