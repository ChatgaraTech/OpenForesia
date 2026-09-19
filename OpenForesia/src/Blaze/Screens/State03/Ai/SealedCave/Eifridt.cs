// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// EIFRIDT — 0x005003E0, grotte scellee (zone 10, sous-zones 11 et 12).
//
// Le nom vient de Comportement.md. 2 608 octets, absents de Blaze.c, transcrits au desassembleur
// le 2026-09-19 (lot 3 de la grotte) : Legacy/Raw/Blaze/SealedCave/FUN_005003E0.asm. Tables de
// sauts a 0x00500de8 (incantation) et 0x00500df8 (postures).
//
// LA CHARPENTE DU DRAGON ROUGE (Ai/FireMountain/RedDragon.cs), presque entiere : trois gestes,
// l'incantation a quatre phases (le lanceur 0xbd, le rebours de la fiche, l'animation 6 cette
// fois, le sort a l'image 13), les quatre postures. Ce qui est a lui :
//
//   - SA GRAVITE N'EST PAS 0x7fff MAIS 0x6000 : il retombe moins vite que toutes les betes de la
//     famille terrestre.
//   - SES DEUX COUPS partagent le meme a-coup (0x28000 a l'image 13, et le script de sons
//     0x00553058), mais pas leur portee : le geste 0 (animation 3) frappe A CHAQUE IMAGE apres la
//     treizieme, degats pleins, sonde 0x00553038 ; le geste 1 (animation 4) une seule fois, sous
//     verrou 0x20, a DEMI-DEGATS, sonde 0x00553044, et projette sa victime comme le Troll.
//     Les deux font clignoter la victime SIX images, la ou le Dragon rouge en donne seize.
//   - SON SOUFFLE (geste 2, animation 7) part a l'image 13 : l'entite de l'octet 0x0d du bloc de
//     zone, nee a l'attache 0 du modele, +0x34 = 0xf80 et +0x5c = 0x46 le temps de la naissance,
//     avec le script de sons 0x00553070 ; puis vingt images de temps mort.
//   - SES SORTS : Mur de flammes ou Boule de feu, a pile ou face (0x00553050).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>Ghidra: <c>&amp;DAT_00553050</c> — <b>les sorts d'Eifridt</b>, famille dans
    /// l'octet haut : 0/10 (Mur de flammes), 0/20 (Boule de feu). Le tirage est un pile ou face ;
    /// le mot suivant (<c>0x00553054</c>) est nul.</summary>
    private static readonly ushort[] s_EifridtSpells_00553050 = [0x000a, 0x0014];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00553058</c> — <b>l'a-coup</b>, script de sons differes des deux coups :
    /// trois fois la voix 5. Borne par le <c>[1] = 0</c> du troisieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00553058 =
    [
        0x05, 0x01, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x00553058
        0x05, 0x01, 0x01, 0x00, 0x2a, 0x03, 0x00, 0x18,   // 0x00553060
        0x05, 0x00, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00553068 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00553058Storage;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00553070</c> — <b>le souffle</b> : cinq fois la voix <c>0x20</c>, a cinq
    /// images d'intervalle. Borne par le <c>[1] = 0</c> du cinquieme ; <c>0x00553098</c> est une
    /// sonde d'un autre corps.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00553070 =
    [
        0x20, 0x05, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553070
        0x20, 0x05, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553078
        0x20, 0x05, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553080
        0x20, 0x05, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553088
        0x20, 0x00, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x00553090 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00553070Storage;

    /// <summary>
    /// Ghidra: <c>0x005003e0</c> — <b>l'intelligence d'Eifridt</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_005003E0(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);   // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideEifridtAction_00500438(e, model, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_00500b72 — la gravite d'Eifridt, 0x6000.
        *(ushort*)(e + 0x9a) = 0x6000;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanEifridtAction_00500BCC(e);
    }

    /// <summary>
    /// Ghidra: <c>0x00500438..0x00500b71</c> — <b>le premier temps</b> : le geste en cours,
    /// l'incantation, ou la posture.
    /// </summary>
    private static void DecideEifridtAction_00500438(
        byte* e, byte* model, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ hors geste
        if ((flags & 2) == 0)
        {
            // Ghidra: LAB_00500831
            if ((flags & 0x10) != 0) return;

            if ((flags & 0x804) != 0)
            {
                AdvanceEifridtIncantation_00500844(e, heading, traits, flags);
                return;
            }

            if ((flags & 1) != 0) return;

            HoldEifridtStance_005009B3(e, distance, heading, flags);
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
            case 0: EifridtStrike_00500734(e, heading); return;
            case 1: EifridtLockedStrike_00500564(e, model, heading); return;
            case 2: EifridtBreath_0050047A(e, flags); return;
            default: return;
        }
    }

    /// <summary>
    /// Ghidra: <c>LAB_00500734</c> — <b>le coup sans verrou</b>, geste 0, animation 3. Il se replace
    /// jusqu'a l'image 10, s'elance a la treizieme, puis frappe a chaque image jusqu'a la fin.
    /// </summary>
    private static void EifridtStrike_00500734(byte* e, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame < 0x0b) TurnSceneEntityTowardTarget(e, 0x40, heading);

        if (frame == 0x0d) EifridtLunge(e);

        // Ghidra: LAB_005007c5
        if (*(short*)(e + 0x1a) >= 0x0d)
        {
            short* probe = stackalloc short[6];                      // Ghidra: &DAT_00553038
            probe[0] = 0; probe[1] = 0; probe[2] = 0x3c; probe[3] = 0x96; probe[4] = 0; probe[5] = 0x96;

            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 6;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
            }
        }

        // Ghidra: LAB_0050081c
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>LAB_00500564</c> — <b>le coup sous verrou</b>, geste 1, animation 4. Replace
    /// jusqu'a l'image 9, meme a-coup a la treizieme ; puis une touche, une seule (bit
    /// <c>0x20</c>), a demi-degats, et la projection du Troll. La fin d'animation efface le geste ET
    /// le verrou.
    /// </summary>
    private static void EifridtLockedStrike_00500564(byte* e, byte* model, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame < 0x0a) TurnSceneEntityTowardTarget(e, 0x40, heading);

        if (frame == 0x0d) EifridtLunge(e);

        // Ghidra: LAB_005005f5
        if (*(short*)(e + 0x1a) >= 0x0d && (*(uint*)e & 0x20) == 0)
        {
            short* probe = stackalloc short[6];                      // Ghidra: &DAT_00553044
            probe[0] = 0; probe[1] = 0x32; probe[2] = 0x46; probe[3] = 0x96; probe[4] = 0x100; probe[5] = 0xaa;

            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
            if (prey != null) *(uint*)e |= 0x20;

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(prey + 0x96) = 6;
                *(uint*)prey |= 0x40;

                short power = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(power >> 1);
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                *(short*)(model + 0x30) = power;

                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

                if ((*(uint*)prey & 0x40000000) == 0) ThrowTrollVictim_0049684C(e, prey);
            }
        }

        // Ghidra: LAB_00500714
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xffffffdd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: <c>0x005005a8</c> / <c>0x00500778</c> — l'a-coup des deux coups : celui du
    /// Dragon rouge a <c>0x28000</c>, plus le script de sons <c>0x00553058</c>.</summary>
    private static void EifridtLunge(byte* e)
    {
        RedDragonLunge(e, 0x28000);

        Aud.QueueDelayedSoundScript(
            NativeCopy(s_DelayedSoundScript_00553058, ref s_DelayedSoundScript00553058Storage),
            (int*)(e + 0x1c));                                       // Ghidra: FUN_0053dbe0
    }

    /// <summary>
    /// Ghidra: <c>0x0050047a</c> — <b>le souffle</b>, geste 2, animation 7. La fin d'animation passe
    /// a la phase 0 avec vingt images de temps mort ; c'est l'image 13 qui souffle.
    /// </summary>
    private static void EifridtBreath_0050047A(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            // Ghidra: LAB_0050054a
            e[0x43]--;
            if (e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (phase == 1)
        {
            if ((flags & 0x2000000) != 0)
            {
                e[0x43] = 0x14;
                *(short*)(e + 0x10) = (short)(phase - 1);
            }

            // Ghidra: LAB_005004a0
            if (*(short*)(e + 0x1a) == 0x0d)
            {
                short savedLift = *(short*)(e + 0x9a);
                short savedPitch = *(short*)(e + 0x34);
                int savedRun = *(int*)(e + 0x5c);

                *(short*)(e + 0x9a) = 0;
                *(short*)(e + 0x34) = 0x0f80;
                *(int*)(e + 0x5c) = 0x46;

                short* anchor = ModelAnchorRow_00535770(e, 0);          // Ghidra: FUN_00535770
                *(int*)(e + 0x44) = anchor[0] << 0xc;
                *(int*)(e + 0x48) = anchor[1] << 0xc;
                *(int*)(e + 0x4c) = anchor[2] << 0xc;

                SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0d), 0, 2);   // Ghidra: FUN_0047a9d0

                *(short*)(e + 0x9a) = savedLift;
                *(short*)(e + 0x34) = savedPitch;
                *(int*)(e + 0x5c) = savedRun;

                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_00553070, ref s_DelayedSoundScript00553070Storage),
                    (int*)(e + 0x1c));                               // Ghidra: FUN_0053dbe0

                *(int*)(e + 0x5c) = 0;
                return;
            }
        }

        *(int*)(e + 0x5c) = 0;                                       // Ghidra: LAB_00500829
    }

    /// <summary>
    /// Ghidra: <c>0x00500844</c> — <b>l'incantation</b>, table de sauts <c>0x00500de8</c>. Celle du
    /// Dragon rouge (<see cref="AdvanceRedDragonIncantation_00499763"/>) a trois nombres pres :
    /// l'animation 6 et non 7, le sort a l'image 13 et non 19, et le virage de la phase 2 dans un
    /// cone fixe de <c>0x80</c>, sans hardiesse.
    /// </summary>
    private static void AdvanceEifridtIncantation_00500844(byte* e, int heading, uint traits, uint flags)
    {
        if ((traits & 0x400003e8) != 0)
        {
            flags &= 0xfffff7fb;
            e[0x43] = 0;
            *(uint*)e = flags;
        }

        // Ghidra: `movsx ecx,ax` puis `cmp ecx,3` / `ja` : une valeur negative sort.
        short state = *(short*)(e + 0x10);
        if ((uint)state > 3) return;

        switch (state)
        {
            // ---------------------------------------------------------- 0 : rendre la main
            case 0:
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffff7ff;
                return;

            // ---------------------------------------------------------- 1 : lancer
            case 1:
                if (*(ushort*)(e + 0x18) != 6)
                {
                    *(int*)(e + 0x18) = 6;
                    *(uint*)e = *(uint*)e & 0xfefffffb;
                    return;
                }

                if (*(short*)(e + 0x1a) != 0x0d) return;

                {
                    ushort sheet = *(ushort*)(e + 6);
                    CastSpell_0053A980((uint*)e, (sheet >> 8) & 0xff, sheet & 0xff, -1, -1);
                }

                *(short*)(e + 0x10) -= 1;
                return;

            // ---------------------------------------------------------- 2 : incanter
            case 2:
                if (--e[0x43] == 0) *(short*)(e + 0x10) = (short)(state - 1);

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                return;

            // ---------------------------------------------------------- 3 : choisir le sort
            default:
            {
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                // Ghidra: `cdq ; xor ; sub ; and eax,ebx ; xor ; sub` — le reste signe d'une
                // division par deux ; le tirage est positif, c'est donc un pile ou face.
                ushort sheet = s_EifridtSpells_00553050[CrtRand() % 2];
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
    /// Ghidra: <c>0x005009b3..0x00500b71</c> — <b>la posture tenue</b>, table de sauts
    /// <c>0x00500df8</c>. Celles du Dragon rouge, a ceci pres que le pivot prend un cone de
    /// <c>0x100</c> et que le pas sonne a l'image 16 (voix <c>0x24</c>).
    /// </summary>
    private static void HoldEifridtStance_005009B3(byte* e, int distance, int heading, uint flags)
    {
        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                break;

            // ---------------------------------------------------------- 1 : pivoter
            case 1:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (TurnSceneEntityTowardTarget(e, 0x100, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                break;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;
                TurnSceneEntityTowardTarget(e, 0x80, heading);
                *(int*)(e + 0x5c) = 0x14000;
                break;

            // ---------------------------------------------------------- 3 : marcher vers un cap
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
                break;
            }

            default:
                break;
        }

        // Ghidra: LAB_00500b4d — le pas, a l'image 16 de la marche.
        if (*(short*)(e + 0x18) != 1) return;
        if (*(short*)(e + 0x1a) != 0x10) return;

        Aud.PlayPositionalSound(0x24, 0x16a, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x00500bcc..0x00500de4</c> — <b>le troisieme temps</b>. Distance et cap en deux
    /// dimensions ; un seul tirage sur cent, fait AVANT de lire la distance.
    ///
    /// <para>
    /// <b>Sous 300</b> : la moitie des fois un coup (le tirage pair donne le geste 1, impair le
    /// geste 0) si la cible est dans le cone de 0x100 (sinon pivoter), un quart l'approche a 200,
    /// le reste la marche. <b>Au-dela</b> : 40 % le souffle (cone de 0x200), 20 % l'approche,
    /// 20 % l'incantation (meme cone), 10 % la marche, 10 % l'attente.
    /// </para>
    /// </summary>
    private static void ReplanEifridtAction_00500BCC(byte* e)
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

                if ((roll & 1) != 0) EifridtStartGesture(e, 3, 0, 2);
                else EifridtStartGesture(e, 4, 1, 2);
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

        // Ghidra: LAB_00500cc9
        if (roll < 0x28)
        {
            if (((heading + 0x200) & 0xfff) >= 0x400) EifridtTurn(e);
            else EifridtStartGesture(e, 7, 2, 1);
            return;
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

            // Ghidra: 0x00500d53 — l'incantation. Le bit 0x1000000 est POSE, pas efface.
            *(int*)(e + 0x18) = 5;
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

    /// <summary>Ghidra: <c>LAB_00500d71</c> — pivoter, pour une image.</summary>
    private static void EifridtTurn(byte* e)
    {
        e[0x42] = 1;
        e[0x43] = 1;
    }

    /// <summary>Ghidra: <c>0x00500c4f</c> / <c>LAB_00500c75</c> / <c>0x00500cea</c> — armer un
    /// geste : animation, geste, phase.</summary>
    private static void EifridtStartGesture(byte* e, int animation, byte gesture, short phase)
    {
        uint flags = *(uint*)e;
        *(int*)(e + 0x18) = animation;
        e[0x42] = gesture;
        *(short*)(e + 0x10) = phase;
        *(uint*)e = (flags & 0xfeffffff) | 2;
    }

    /// <summary>Ghidra: <c>0x00500ca4</c> / <c>LAB_00500d0c</c> — approcher jusqu'a 200.</summary>
    private static void EifridtApproach(byte* e)
    {
        e[0x42] = 2;
        int r = CrtRand();
        *(short*)(e + 0x10) = 0xc8;
        e[0x43] = (byte)(r % 10 + 0x14);
    }

    /// <summary>Ghidra: <c>LAB_00500d86</c> — marcher vers un cap tire sur les trois quarts du
    /// tour, a partir de <c>+0x200</c>.</summary>
    private static void EifridtWalk(byte* e, int heading)
    {
        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);
        int r = CrtRand() % 0xc00;
        *(short*)(e + 0x10) = (short)(r + heading + *(ushort*)(e + 0x32) + 0x200);
    }
}
