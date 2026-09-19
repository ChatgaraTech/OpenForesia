// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE CERBERE — 0x004F7980, grotte scellee (zone 10, sous-zones 4 et 9).
//
// Le nom vient de Comportement.md. 2 288 octets, absents de Blaze.c, transcrits au desassembleur
// le 2026-09-19 (lot 3 de la grotte) : Legacy/Raw/Blaze/SealedCave/FUN_004F7980.asm. Table de
// sauts a quatre entrees a 0x004f8260 (les postures).
//
// La charpente terrestre du Dragon zombie (Ai/SealedCave/ZombieDragon.cs), voisin de sous-zone :
// memes quatre postures, meme application. Mais il LIT SA HARDIESSE (la largeur du cap tire pour
// marcher) et ses trois gestes sont les siens :
//
//     geste 0  animation 3   LE BOND : phase 2 jusqu'a l'image 5 (il freine d'un quart par image),
//                            phase 1 le saut (0x3c000 devant, 0x32000 vers le haut), phase 0 la
//                            chute, ou il balaie la sonde 0x00552f70 A CHAQUE IMAGE, sans verrou :
//                            coup, puis la projection du Troll (ThrowTrollVictim_0049684C)
//     geste 1  animation 5   a l'image 5 le script de sons 0x00552fb0 ; en fin d'animation,
//                            l'entite de l'octet 0x0e (+0x44 = 0xb, +0x48 = 0x4e20 le temps de
//                            la naissance), puis 10 images
//     geste 2  animation 6   LES TROIS TETES : a l'image 16, trois naissances, une par attache
//                            du modele (0, 1, 2), les entites 0x0d / 0x15 / 0x14, ecartees de
//                            -0x100 / 0 / +0x100 par +0x9a ; puis le script de sons 0x00552f80
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552f80</c> — <b>le cri des trois tetes</b>, script de sons differes :
    /// six voix a une image d'intervalle. Borne par le <c>[1] = 0</c> du sixieme ; les octets qui
    /// suivent (<c>0x00552fb0</c>) sont le script suivant.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552F80 =
    [
        0x26, 0x01, 0x01, 0x00, 0x7e, 0x03, 0x00, 0x18,   // 0x00552f80
        0x13, 0x01, 0x01, 0x00, 0x55, 0x02, 0x00, 0x18,   // 0x00552f88
        0x1f, 0x01, 0x01, 0x00, 0xd3, 0x03, 0x00, 0x18,   // 0x00552f90
        0x23, 0x01, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x00552f98
        0x23, 0x01, 0x01, 0x00, 0x55, 0x02, 0x00, 0x18,   // 0x00552fa0
        0x23, 0x00, 0x01, 0x00, 0xaa, 0x02, 0x00, 0x18,   // 0x00552fa8 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552F80Storage;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552fb0</c> — <b>le grondement du jet</b>, deux voix. Borne par le
    /// <c>[1] = 0</c> du second ; <c>0x00552fc0</c> commence autre chose.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552FB0 =
    [
        0x12, 0x01, 0x01, 0x00, 0x55, 0x01, 0x00, 0x18,   // 0x00552fb0
        0x3f, 0x00, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x00552fb8 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552FB0Storage;

    /// <summary>
    /// Ghidra: <c>0x004f7980</c> — <b>l'intelligence du Cerbere</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004F7980(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);     // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideCerberusAction_004F79DB(e, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_004f7fef
        *(ushort*)(e + 0x9a) = 0x7fff;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanCerberusAction_004F8049(e, boldness);
    }

    /// <summary>
    /// Ghidra: <c>0x004f79db..0x004f7fee</c> — <b>le premier temps</b> : mener le geste en cours, ou
    /// tenir la posture.
    /// </summary>
    private static void DecideCerberusAction_004F79DB(byte* e, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ hors geste
        if ((flags & 2) == 0)
        {
            // Ghidra: LAB_004f7df7
            if ((flags & 0x11) != 0) return;
            if ((flags & 0x804) != 0) return;

            HoldCerberusStance_004F7E1F(e, distance, heading, flags);
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
            case 0: CerberusLeap_004F7C3B(e, heading, flags); return;
            case 1: CerberusSpit_004F7B9F(e, flags); return;
            case 2: CerberusThreeHeads_004F7A24(e, flags); return;
            default: return;
        }
    }

    /// <summary>
    /// Ghidra: <c>LAB_004f7c21</c> — <b>le temps mort apres un jet</b>, commun aux gestes 1 et 2.
    /// </summary>
    private static void CountDownCerberusGesture_004F7C21(byte* e, uint flags)
    {
        e[0x43]--;
        if (e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;

        *(int*)(e + 0x5c) = 0;                                       // Ghidra: LAB_004f7c2f
    }

    /// <summary>
    /// Ghidra: <c>LAB_004f7c3b</c> — <b>le bond</b>, geste 0, sur l'animation 3. La phase
    /// <c>+0x10</c> part de 2 (posee par la replanification).
    ///
    /// <para>
    /// <b>La phase 0 ne remet jamais <c>+0x5c</c> a zero</b>, et la phase 2 non plus : elle le
    /// rogne d'un quart par image. Seule la phase 1, d'une image, l'annule.
    /// </para>
    /// </summary>
    private static void CerberusLeap_004F7C3B(byte* e, int heading, uint flags)
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
            {
                *(int*)(e + 0x5c) = 0x3c000;
                *(int*)(e + 0x50) +=
                    ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x58) +=
                    ((int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c)) >> 0xc;

                *(short*)(e + 0x10) -= 1;
                *(int*)(e + 0x54) = 0x32000;
                *(uint*)e |= 1;                                      // en l'air
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 0 : retomber, en frappant
            case 0:
                break;

            default:
                return;
        }

        // Ghidra: LAB_004f7d05 — le geste ne se ferme qu'au sol et en fin d'animation.
        if ((flags & 0x2000000) != 0 && (flags & 1) == 0) *(uint*)e = flags & 0xfffffffd;

        short* probe = stackalloc short[6];                          // Ghidra: &DAT_00552f70
        probe[0] = 0; probe[1] = 0x50; probe[2] = 0x46; probe[3] = 0x96; probe[4] = 0xc8; probe[5] = 0x96;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(uint*)prey |= 0x40;
        *(ushort*)(prey + 0x96) = 0x10;
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

        if ((*(uint*)prey & 0x40000000) != 0) return;

        // Ghidra: 0x004f7d74..0x004f7df2 — la projection du Troll, au mot pres : trente fois le cap,
        // un saut de 0x18000 si la victime etait au sol, et l'effet 0x39 ne SUR elle.
        ThrowTrollVictim_0049684C(e, prey);
    }

    /// <summary>
    /// Ghidra: <c>LAB_004f7b9f</c> — <b>le jet</b>, geste 1, sur l'animation 5.
    /// </summary>
    private static void CerberusSpit_004F7B9F(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);
        if (phase == 0)
        {
            CountDownCerberusGesture_004F7C21(e, flags);
            return;
        }

        if (phase == 1)
        {
            if (*(short*)(e + 0x1a) == 5)
            {
                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_00552FB0, ref s_DelayedSoundScript00552FB0Storage),
                    (int*)(e + 0x1c));                               // Ghidra: FUN_0053dbe0
            }

            if ((*(uint*)e & 0x2000000) != 0)
            {
                int saved44 = *(int*)(e + 0x44);
                int saved48 = *(int*)(e + 0x48);
                *(int*)(e + 0x44) = 0x0b;
                *(int*)(e + 0x48) = 0x4e20;

                SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0e), 0, 2);   // Ghidra: FUN_0047a9d0

                *(short*)(e + 0x10) -= 1;
                *(int*)(e + 0x44) = saved44;
                *(int*)(e + 0x48) = saved48;
                e[0x43] = 0x0a;
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }

        *(int*)(e + 0x5c) = 0;                                       // Ghidra: LAB_004f7c2f
    }

    /// <summary>
    /// Ghidra: <c>0x004f7a24</c> — <b>les trois tetes</b>, geste 2, sur l'animation 6. La fin
    /// d'animation passe a la phase 0 avec un temps mort d'une image ; c'est l'image 16 qui souffle.
    /// </summary>
    private static void CerberusThreeHeads_004F7A24(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);
        if (phase == 0)
        {
            CountDownCerberusGesture_004F7C21(e, flags);
            return;
        }

        if (phase == 1)
        {
            if ((flags & 0x2000000) != 0)
            {
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)(phase - 1);
            }

            // Ghidra: LAB_004f7a4b
            if (*(short*)(e + 0x1a) == 0x10)
            {
                short savedSpread = *(short*)(e + 0x9a);
                short savedPitch = *(short*)(e + 0x34);
                int savedRun = *(int*)(e + 0x5c);

                *(short*)(e + 0x34) = 0x0f00;
                *(int*)(e + 0x5c) = 0x46;

                *(short*)(e + 0x9a) = unchecked((short)0xff00);
                CerberusHeadAnchor(e, 0);
                SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0d), 0, 2);   // Ghidra: FUN_0047a9d0

                *(short*)(e + 0x9a) = 0;
                CerberusHeadAnchor(e, 1);
                SpawnEffectEntity(e, ZombieKingSummonSubtype(0x15), 0, 3);

                *(short*)(e + 0x9a) = 0x100;
                CerberusHeadAnchor(e, 2);
                SpawnEffectEntity(e, ZombieKingSummonSubtype(0x14), 0, 1);

                *(short*)(e + 0x9a) = savedSpread;
                *(short*)(e + 0x34) = savedPitch;
                *(int*)(e + 0x5c) = savedRun;

                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_00552F80, ref s_DelayedSoundScript00552F80Storage),
                    (int*)(e + 0x1c));                               // Ghidra: FUN_0053dbe0

                *(int*)(e + 0x5c) = 0;
                return;
            }
        }

        *(int*)(e + 0x5c) = 0;                                       // Ghidra: LAB_004f7c2f
    }

    /// <summary>Une tete : l'attache <paramref name="row"/> du modele, posee en <c>+0x44..+0x4c</c>
    /// comme point de naissance (Ghidra: FUN_00535770, trois fois en ligne).</summary>
    private static void CerberusHeadAnchor(byte* e, int row)
    {
        short* anchor = ModelAnchorRow_00535770(e, row);
        *(int*)(e + 0x44) = anchor[0] << 0xc;
        *(int*)(e + 0x48) = anchor[1] << 0xc;
        *(int*)(e + 0x4c) = anchor[2] << 0xc;
    }

    /// <summary>
    /// Ghidra: <c>0x004f7e1f..0x004f7fee</c> — <b>la tenue de la posture</b>, par la table de sauts
    /// <c>0x004f8260</c>.
    /// </summary>
    private static void HoldCerberusStance_004F7E1F(byte* e, int distance, int heading, uint flags)
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

                if (TurnSceneEntityTowardTarget(e, 0x80, heading)) e[0x43] = 0;
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

                TurnSceneEntityTowardTarget(e, 0x60, heading);
                break;
            }

            default:
                break;
        }

        // Ghidra: LAB_004f7fc1 — le pas, aux images 6 et 18 de la marche.
        if (*(short*)(e + 0x18) != 1) return;

        short frame = *(short*)(e + 0x1a);
        if (frame == 6 || frame == 0x12)
        {
            Aud.PlayPositionalSound(0x35, 0x200, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004f8049..0x004f825c</c> — <b>le troisieme temps</b>. Distance et cap en deux
    /// dimensions ; un tirage sur cent par plage.
    ///
    /// <para>
    /// <b>Sous 300</b> : la moitie des fois le bond, si la cible est dans le cone de 0x100 (sinon
    /// pivoter), un quart l'approche a 200, le reste la marche. <b>Au-dela</b> : 30 % le bond (cone de
    /// 0x200), 20 % le jet, 20 % les trois tetes si la cible est dans le cone (sinon l'approche),
    /// 20 % la marche, 10 % l'attente.
    /// </para>
    /// </summary>
    private static void ReplanCerberusAction_004F8049(byte* e, int boldness)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        if (distance < 0x12c)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + 0x100) & 0xfff) > 0x200) CerberusTurn(e);
                else CerberusStartLeap(e);
                return;
            }

            if (roll < 0x4b)
            {
                CerberusApproach(e);
                return;
            }

            CerberusWalk(e, boldness, heading);
            return;
        }

        // Ghidra: LAB_004f80f7
        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            if (((heading + 0x200) & 0xfff) >= 0x400) CerberusTurn(e);
            else CerberusStartLeap(e);
            return;
        }

        if (far < 0x32)
        {
            CerberusStartGesture(e, 5, 1);
            return;
        }

        if (far < 0x46)
        {
            if (((heading + 0x200) & 0xfff) < 0x400) CerberusStartGesture(e, 6, 2);
            else CerberusApproach(e);
            return;
        }

        if (far < 0x5a)
        {
            CerberusWalk(e, boldness, heading);
            return;
        }

        e[0x42] = 0;
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);
    }

    /// <summary>Ghidra: <c>LAB_004f814c</c> — pivoter, pour une image.</summary>
    private static void CerberusTurn(byte* e)
    {
        e[0x42] = 1;
        e[0x43] = 1;
    }

    /// <summary>Ghidra: <c>LAB_004f8121</c> — le bond : animation 3, geste 0, phase 2.</summary>
    private static void CerberusStartLeap(byte* e)
    {
        uint flags = *(uint*)e;
        *(int*)(e + 0x18) = 3;
        e[0x42] = 0;
        *(short*)(e + 0x10) = 2;
        *(uint*)e = (flags & 0xfeffffff) | 2;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: <c>LAB_004f815c</c> / <c>0x004f81a4</c> — un jet, phase 1.</summary>
    private static void CerberusStartGesture(byte* e, int animation, byte gesture)
    {
        uint flags = *(uint*)e;
        *(int*)(e + 0x18) = animation;
        e[0x42] = gesture;
        *(short*)(e + 0x10) = 1;
        *(uint*)e = (flags & 0xfeffffff) | 2;
    }

    /// <summary>Ghidra: <c>LAB_004f80c9</c> / <c>LAB_004f81c8</c> — approcher jusqu'a 200.</summary>
    private static void CerberusApproach(byte* e)
    {
        e[0x42] = 2;
        int r = CrtRand();
        *(short*)(e + 0x10) = 0xc8;
        e[0x43] = (byte)(r % 10 + 0x14);
    }

    /// <summary>
    /// Ghidra: <c>LAB_004f81f2</c> — marcher vers un cap tire A L'OPPOSE de la cible (<c>+0x800</c>),
    /// dans un eventail de <c>(hardiesse + 0x80) * 16</c> centre sur lui : plus la bete est hardie,
    /// plus sa fuite part de travers.
    /// </summary>
    private static void CerberusWalk(byte* e, int boldness, int heading)
    {
        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);

        int width = (boldness + 0x80) << 4;
        int r = CrtRand() % width - (width >> 1);
        *(short*)(e + 0x10) = (short)(r + heading + *(ushort*)(e + 0x32) + 0x800);
    }
}
