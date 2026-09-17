// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GOLEM-SQUELETTE ET LE GROS DUR — 0x004B0600, vieille tour (zone 4, sous-zones 5, 6, 8, 10).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004B0600 — LE GOLEM-SQUELETTE (et le Gros dur, meme routine). Transcrit le 2026-09-17 au
    // desassembleur (Legacy/Raw/Blaze/OldTower/FUN_004B0600.asm, 1 211 instructions, absente de
    // Blaze.c), table de sauts a quinze entrees a 0x004B15E0. Son appelee 0x004B1620 est ecrite ici.
    //
    // LA CHARPENTE DU POURPRE PREUX (Ai/ImmortalCastle/PurpleKnight.cs) — memes sondes, meme pose au
    // sol, memes postures 5, 6 et 7 au mot pres — MAIS SANS SA PARADE, et avec d'autres gestes :
    //
    //   - LE COUP (posture 11, animation 5), porte par le prelude a l'image 8 ;
    //   - LA FRAPPE QUI ALTERE (posture 12, animation 3) : a CHAQUE image au-dela de la 12e elle
    //     balaie, frappe a pleine attaque et tente l'alteration 7 par 0x004B1620 ;
    //   - LA CHARGE (posture 13) : a l'image 6 elle s'elance a (variante + 6) * 5 << 12, a moitie
    //     d'attaque par la sonde B, projette sa victime ; l'elan perd un seizieme par image et la
    //     charge tombe sous 0x5000. Une bete hardie (plus de 30) corrige encore sa course en chargeant ;
    //   - LE RECUL (posture 14), celui du Garde golem, animation 1 ;
    //   - L'EFFET (posture 10) : 0x36 a l'image 8, quelle que soit la variante.
    //
    // ET UNE REPLANIFICATION QUI SE RELANCE ELLE-MEME : trois de ses branches, quand la bete RECULE
    // deja (posture 14), sautent au debut du troisieme temps et retirent tout — le rebours etant nul,
    // le saut est toujours pris. C'est une boucle du binaire, transcrite en boucle.
    // =============================================================================================

    /// <summary>
    /// Ghidra: FUN_004b1620 @ 0x004b1620 — <b>l'alteration, si elle est permise</b> : le filtre de
    /// <see cref="RejectStatusTarget_004A2C30"/>, puis <see cref="ApplyStatusEffect_0053B810"/>. La
    /// sœur de <see cref="ApplyTrapEffectIfAllowed_004A2BF0"/>, qui fait naitre l'effet de piege.
    /// Dans <c>Blaze.c</c>.
    /// </summary>
    /// <returns><c>1</c> si la victime a ete refusee, <c>0</c> si l'alteration est partie. Le
    /// binaire rend un mot de seize bits (<c>xor ax,ax</c> / <c>mov ax,1</c>).</returns>
    private static short ApplyStatusEffectIfAllowed_004B1620(uint* caster, uint* victim, int keep)
    {
        if (RejectStatusTarget_004A2C30(caster, victim)) return 1;

        ApplyStatusEffect_0053B810(caster, victim, keep);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004b0600</c> — <b>l'intelligence du Golem-squelette</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004B0600(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] et [esp+0x3c] — les deux sondes du Pourpre preux.
        short* probes = stackalloc short[12];
        probes[0] = 0; probes[1] = 0x1e; probes[2] = 0x32; probes[3] = 0x64; probes[4] = 0; probes[5] = 0x64;
        probes[6] = 0; probes[7] = 0; probes[8] = 0x50; probes[9] = 0x64; probes[10] = 0; probes[11] = 0x64;

        // Ghidra: 0x004b0616 — PAS de chercheur de proie : +0x6c est lu tel qu'il est.
        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004b071e — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideSkeletonGolemAction_004B0769(e, probes, model, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- 2. appliquer
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

        if (e[0x43] != 0) return;

        ReplanSkeletonGolemAction_004B11E6(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004b0769..0x004b0f99</c> — <b>le premier temps</b> du Golem-squelette. Quinze
    /// cases, dont onze servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 5 parer, 6 bondir,
    /// 7 marcher droit, 10 l'effet, 11 le coup, 12 la frappe qui altere, 13 la charge, 14 reculer.
    /// </summary>
    private static void DecideSkeletonGolemAction_004B0769(
        byte* e, short* probes, byte* model, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        if ((e[0] & 2) != 0) SkeletonGolemGesture_004B07EB(e, probes, model);

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
                }
                else
                {
                    // Ghidra: 0x004b0a61 — 0x50, et non 0x80 comme le Pourpre preux.
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x50 : -0x50 - boldness);
                }

                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
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

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x80
                    : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 5 : parer
            case 5:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1 && e[0x43] != 0 && (*(uint*)e & 0x2000000) != 0) e[0x43]--;

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x18) = 6;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
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
                        *(uint*)e = f & 0xfeffffff;

                        int thrust = (boldness + 0x100) << 6;
                        int step = (speed * thrust) >> 0xc;

                        *(short*)(e + 0x18) = 1;
                        *(short*)(e + 0x1a) = 0;
                        *(int*)(e + 0x5c) = thrust;

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

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 10 : l'effet
            case 10:
            {
                if (*(short*)(e + 0x12) == 1 && *(short*)(e + 0x1a) == 8)
                {
                    SpawnEffectEntity(e, 0x36, 0, -1);                  // Ghidra: FUN_0047a9d0
                }

                uint f = *(uint*)e;
                if ((f & 0x2000000) != 0) e[0x43] = 0;

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e = f & 0xfeffffff;
                *(short*)(e + 0x18) = 5;
                *(short*)(e + 0x1a) = 0;

                if (boldness < 0x1e) *(int*)(e + 0x5c) = 0;
                if (*(short*)(e + 0x16) == 0) e[0x43] = 0;
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
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 12 : la frappe qui altere
            case 12:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x12) = 1;
                    *(short*)(e + 0x18) = 3;
                    *(short*)(e + 0x1a) = 0;
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    *(int*)(e + 0x5c) = 0;
                }

                if (*(short*)(e + 0x12) != 1) return;

                {
                    uint f = *(uint*)e;
                    if ((f & 0x2000000) == 0) return;

                    *(uint*)e = f & 0xfffffffd;
                    e[0x43] = 0;
                }
                return;

            // ---------------------------------------------------------- 13 : la charge
            case 13:
                SkeletonGolemCharge_004B0E22(e, boldness, heading);
                return;

            // ---------------------------------------------------------- 14 : reculer
            case 14:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) + 0x800);
                    int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) + 0x800);
                    int back = ((boldness + 0x60) * speed << 8) >> 0xc;

                    *(int*)(e + 0x1c) += (back * sin) >> 0xc;
                    *(int*)(e + 0x24) += (back * cos) >> 0xc;
                    return;
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 4, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004b07eb..0x004b09b4</c> — <b>le prelude de geste</b>, trois blocs.
    ///
    /// <list type="number">
    /// <item><b>Hors 12 et 13</b> : a l'image 8, la sonde A a pleine attaque ; l'animation finie ferme
    /// le geste, et le rebours seulement a terre.</item>
    /// <item><b>La frappe qui altere</b> (12) : a <b>chaque</b> image passee la 12e, la sonde A a pleine
    /// attaque puis <see cref="ApplyStatusEffectIfAllowed_004B1620"/>.</item>
    /// <item><b>La charge</b> (13) : la sonde B a chaque image, a moitie d'attaque ; la victime non
    /// portee est projetee a <c>+0x5c &gt;&gt; 13</c> dans le cap du golem, <c>0x18000</c> de haut, et
    /// l'effet <c>0x39</c> nait. <b>Le geste ne se ferme pas au contact</b>, contrairement au Pourpre
    /// preux : c'est l'elan qui la termine.</item>
    /// </list>
    /// </summary>
    private static void SkeletonGolemGesture_004B07EB(byte* e, short* probes, byte* model)
    {
        byte posture = e[0x42];

        if (posture != 0x0c && posture != 0x0d)
        {
            if (*(short*)(e + 0x1a) == 8)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 8)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }
            }

            uint flags = *(uint*)e;
            if ((flags & 0x2000000) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                if ((flags & 1) == 0) e[0x43] = 0;
            }
        }

        if (e[0x42] == 0x0c && *(short*)(e + 0x1a) > 0x0c)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) > 0x0c)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x10;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                ApplyStatusEffectIfAllowed_004B1620((uint*)e, (uint*)prey, 0);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
            }
        }

        if (e[0x42] != 0x0d) return;

        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes + 6, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(victim + 0x96) = 0x10;
        *(uint*)victim |= 0x40;

        short attack = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(attack >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
        *(short*)(model + 0x30) = attack;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20

        if ((*(uint*)victim & 0x40000000) != 0) return;

        SpawnEffectEntity(e, 0x39, 0, -1);                          // Ghidra: FUN_0047a9d0
        *(uint*)victim |= 1;

        short course = *(short*)(e + 0x32);
        *(int*)(victim + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * (*(int*)(e + 0x5c) >> 0xd);
        *(int*)(victim + 0x54) = 0x18000;
        *(int*)(victim + 0x58) = Winsys3d.GetCosTableEntry(course) * (*(int*)(e + 0x5c) >> 0xd);
    }

    /// <summary>
    /// Ghidra: <c>0x004b0e22..0x004b0f0b</c> — <b>la charge</b>, posture 13.
    ///
    /// <list type="bullet">
    /// <item><b>2</b> — l'elan perd un seizieme par image ; sous <c>0x5000</c> la charge tombe, rebours
    /// et poussee a zero. Une bete de hardiesse superieure a 30 corrige sa course de
    /// <c>hardiesse + 0x32</c> hors d'un cone de <c>0x100</c>.</item>
    /// <item><b>1</b> — elle vise (<c>hardiesse * 2 + 0x32</c>) et, a l'image 6, s'elance :
    /// <c>(variante + 6) * 5 &lt;&lt; 12</c>, geste arme.</item>
    /// <item><b>0</b> — l'armement : animation 4, poussee a zero.</item>
    /// </list>
    /// </summary>
    private static void SkeletonGolemCharge_004B0E22(byte* e, int boldness, int heading)
    {
        short phase = *(short*)(e + 0x12);

        if (phase == 2)
        {
            int v = *(int*)(e + 0x5c);
            v -= v >> 4;
            *(int*)(e + 0x5c) = v;

            if (v < 0x5000)
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffd;
                *(int*)(e + 0x5c) = 0;
            }

            if (boldness > 0x1e && ((heading + 0x100) & 0xfff) > 0x200)
            {
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
            }
        }

        if (phase == 1)
        {
            if (((heading + 0x100) & 0xfff) > 0x200)
            {
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x32
                    : -0x32 - boldness * 2);
            }

            if (*(short*)(e + 0x1a) >= 6)
            {
                *(short*)(e + 0x12) = 2;
                *(int*)(e + 0x5c) = ((*(short*)(e + 0x16) + 6) * 5) << 0xc;
                *(uint*)e |= 2;
            }
        }

        if (*(short*)(e + 0x12) != 0) return;

        *(short*)(e + 0x12) = 1;
        *(uint*)e &= 0xfeffffff;
        *(short*)(e + 0x18) = 4;
        *(short*)(e + 0x1a) = 0;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: le motif <c>hasard % 20 + 20</c> — <b>la duree du recul</b>, posture 14.</summary>
    private static void BackAwaySkeletonGolem(byte* e)
    {
        e[0x42] = 0x0e;
        e[0x43] = (byte)(CrtRand() % 0x14 + 0x14);
    }

    /// <summary>
    /// Ghidra: <c>0x004b11e6..0x004b15d4</c> — <b>le troisieme temps</b> du Golem-squelette.
    ///
    /// <para>
    /// <b>Trois de ses branches bouclent</b> : quand la bete recule deja (posture 14) et que le tirage
    /// voudrait l'envoyer approcher, le binaire saute a <c>LAB_004b13db</c>, qui relance tout le
    /// troisieme temps si le rebours est nul — et il l'est toujours ici. Le calcul de distance et de
    /// cap est refait, <c>+0x12</c> remis a zero, et un nouveau tirage est fait. La boucle ne se
    /// termine que sur un tirage qui mene ailleurs.
    /// </para>
    /// <list type="bullet">
    /// <item><b>De pres</b> (sous <c>0xFA</c>) : la moitie des tirages porte le coup (sous 30) ou la
    /// frappe qui altere, dans le cone et a portee, sinon se tourner ; sous 60, reculer si la cible est
    /// a moins de 80, sinon approcher ; de 60 a 69, reculer si la cible est au-dela de
    /// <c>hardiesse + 100</c> ; au-dela, reculer de tout pres ou approcher.</item>
    /// <item><b>A mi-distance</b> : la charge face a la cible, approcher, reculer, filer ou l'effet.</item>
    /// <item><b>De loin</b> : approcher, attendre, filer, se tourner, ou l'effet dans le cone de
    /// timidite.</item>
    /// </list>
    /// </summary>
    private static void ReplanSkeletonGolemAction_004B11E6(byte* e, int boldness, int timid)
    {
        byte* chased;

        while (true)
        {
            chased = *(byte**)(e + 0x6c);

            int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
            int bearing = Winsys3d.Ordinal_457(dx, dz);
            *(short*)(e + 0x12) = 0;
            int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

            bool retry = false;

            // ================================================================ de pres
            if (distance < 0xfa)
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
                            e[0x43] = 1;
                            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                            *(int*)(e + 0x5c) = 0;
                            *(short*)(e + 0x18) = 5;
                            *(short*)(e + 0x1a) = 0;
                        }
                        else
                        {
                            e[0x42] = 0x0c;
                            e[0x43] = 1;
                            *(short*)(e + 0x12) = 0;
                        }
                    }
                    else
                    {
                        e[0x42] = 1;
                        e[0x43] = 1;
                    }
                }
                else if (roll < 0x3c)
                {
                    if (distance < 0x50) BackAwaySkeletonGolem(e);
                    else if (e[0x42] == 0x0e) retry = true;
                    else HoldSkeletonGolemAtRange(e, timid);
                }
                else if (roll < 0x46 && distance > boldness + 100)
                {
                    BackAwaySkeletonGolem(e);
                }
                else if (distance < 0x50)
                {
                    BackAwaySkeletonGolem(e);
                }
                else if (e[0x42] == 0x0e)
                {
                    retry = true;
                }
                else
                {
                    HoldSkeletonGolemAtRange(e, timid);
                }
            }

            // Ghidra: LAB_004b13db — `mov al,[esi+43h] ; test ; je LAB_004b11e6`, sinon rendre la main.
            if (retry)
            {
                if (e[0x43] == 0) continue;
                return;
            }

            // ================================================================ a moyenne distance
            if (distance < 0x258 && distance >= 0xfa)
            {
                int roll = CrtRand() % 100;

                if (roll < 0x14)
                {
                    if (((heading + 0x400) & 0xfff) < 0x800)
                    {
                        e[0x42] = 0x0d;
                        e[0x43] = 1;
                        *(short*)(e + 0x12) = 0;
                    }
                    else
                    {
                        e[0x42] = 1;
                        e[0x43] = 1;
                    }
                }
                else if (roll < 0x1e)
                {
                    if (e[0x42] == 0x0e)
                    {
                        if (e[0x43] == 0) continue;
                        return;
                    }

                    HoldSkeletonGolemAtRange(e, timid);
                }
                else if (roll < 0x32)
                {
                    BackAwaySkeletonGolem(e);
                }
                else if (roll < 0x50)
                {
                    StrafeSalamander(e, timid);
                }
                else
                {
                    e[0x42] = 0x0a;
                    e[0x43] = 1;
                    *(short*)(e + 0x12) = 0;
                }
            }

            // ================================================================ de loin
            if (distance < 0x258) return;

            int far = CrtRand() % 100;

            if (far < 0x1e)
            {
                if (far < boldness * 2 + 10)
                {
                    ApproachSkeletonGolemFromAfar(e, timid);
                    return;
                }

                RestSkeletonGolem(e, boldness, timid);
                return;
            }

            if (far < 0x28)
            {
                StrafeSalamander(e, timid);
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
                if (((heading + timid * 4 + 0x100) & 0xfff) > timid * 8 + 0x200)
                {
                    ApproachSkeletonGolemFromAfar(e, timid);
                    return;
                }

                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
                return;
            }

            RestSkeletonGolem(e, boldness, timid);
            return;
        }
    }

    /// <summary>Ghidra: <c>LAB_004b130f</c>, <c>LAB_004b1381</c>, <c>LAB_004b141f</c> — approcher a
    /// <c>0x50</c>, <c>hasard % 10 + timidite / 2 + 20</c> images.</summary>
    private static void HoldSkeletonGolemAtRange(byte* e, int timid)
    {
        e[0x42] = 2;
        int wait = CrtRand() % 10;
        *(short*)(e + 0x10) = 0x50;
        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
    }

    /// <summary>Ghidra: <c>LAB_004b14e2</c> — approcher a <c>100</c>, de loin.</summary>
    private static void ApproachSkeletonGolemFromAfar(byte* e, int timid)
    {
        e[0x42] = 2;
        int wait = CrtRand() % 10;
        *(short*)(e + 0x10) = 100;
        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
    }

    /// <summary>Ghidra: <c>LAB_004b15a9</c> — le repos : posture 0, <c>hasard % 10 + timidite / 4</c>
    /// images, a distance <c>hardiesse * 2 + 0x50</c>.</summary>
    private static void RestSkeletonGolem(byte* e, int boldness, int timid)
    {
        e[0x42] = 0;
        int rest = CrtRand() % 10;
        *(short*)(e + 0x12) = 0;
        e[0x43] = (byte)(rest + (timid >> 2));
        *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
    }
}
