// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE POURPRE PREUX — 0x004D1F70, sous-type 11 du chateau de l'immortel (zone 6, sous-zones 7, 9 et 10).
//
// TROIS CORPS D'UN COUP. 4 104 octets de code, absents de `Blaze.c`, table de sauts a
// quatorze entrees.
//
// La charpente commune (postures, replanification, errance, hesitation) est dans
// `Ai/SceneEntityAiCommon.cs` ; la resolution du coup dans `Combat/HitResolution.cs` ;
// le ciblage dans `Entities/Targeting.cs`.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004D1F70 — LE POURPRE PREUX, sous-type 11 du chateau de l'immortel (zone 6, sous-zones 7, 9
    // et 10), et TROIS CORPS D'UN COUP. Transcrite le 2026-09-15 au desassembleur
    // (Legacy/Raw/Blaze/FUN_004D1F70.asm, 4 104 octets de code, absente de Blaze.c), table de sauts
    // a quatorze entrees a 0x004D2F80.
    //
    // LES TROIS SONT LA MEME ROUTINE, SANS MEME UNE DONNEE PROPRE. 0x004E67C0 et 0x004EB530 (grotte
    // scellee) se comparent a 0x004D1F70 instruction pour instruction une fois les cibles d'appel
    // normalisees : zero ligne differente. Leurs chercheurs de proie (0x004CEFE0, 0x004E6400,
    // 0x004EB170) sont identiques a FUN_004A1020.
    //
    // LA CHARPENTE DU VARAN, AU MOT PRES POUR LA QUEUE : meme integration (FUN_00488160), meme pose
    // au sol et meme heurt de mur au masque 0x2000ad82 — SettleSceneEntityOnGroundAndWalls tel quel.
    // Tout ce qui le distingue est dans les gestes :
    //
    //   - UN CHEVALIER A CINQ ATTAQUES. Le coup (0x0b, image 8, sonde A), LE COMBO (0x0c : deux
    //     coups a trois quarts de l'attaque, le premier sans faire chanceler), LA CHARGE (0x0d :
    //     sonde B, a moitie d'attaque, projette la victime et s'arrete au premier contact), le coup
    //     EN L'AIR (un bond qui se change en attaque, hardiesse/60 fois par image), et la posture 10,
    //     qui selon +0x16 fait naitre l'effet 0x4b ou 0x36 — ou rien.
    //   - IL PARE. A la fin de CHAQUE image ou il bouge, qu'il ait replanifie ou non, s'il est pres
    //     de sa cible, face a elle, a terre, dans une posture calme, et que la cible porte son bit de
    //     geste, il prend la posture 5 : l'animation 6 figee a l'image 7, cinq a neuf images. La
    //     posture 5 leve le bit 0x10 — « pas de decision » — comme chez le Varan.
    //   - ET SA PARADE COMPARE LA TIMIDITE A UN REGISTRE. Le seuil est « ebx < timidite », ou ebx
    //     vaut le dernier tirage de la replanification — ou, quand elle n'a pas eu lieu, le pas
    //     d'avance que l'integration vient de calculer. Transcrit tel quel.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004d1f70</c> (et ses deux copies) — <b>l'intelligence du Pourpre preux</b>.
    /// </summary>
    private static void TickSceneEntityAi_004D1F70(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x34] et [esp+0x40] — les deux sondes, baties sur la pile a chaque image. La
        // premiere sert au coup, au combo et au coup en l'air ; la seconde, sans avancee, a la charge.
        short* probes = stackalloc short[12];
        probes[0] = 0; probes[1] = 0x1e; probes[2] = 0x32; probes[3] = 0x64; probes[4] = 0; probes[5] = 0x64;
        probes[6] = 0; probes[7] = 0; probes[8] = 0x50; probes[9] = 0x64; probes[10] = 0; probes[11] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004cefe0, copie de FUN_004a1020
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

        // Ghidra: 0x004d20a9 — la distance recalculee a trois dimensions, comme chez le Varan.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecidePurpleKnightAction_004D1F70(e, probes, model, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: 0x004d2978 — `imul ebx,[esi+5Ch]` / `sar ebx,0Ch` : le pas d'avance de
        // l'integration reste dans EBX, et la parade de fin d'image le relit.
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

        // Ghidra: 0x004d2b7b — un rebours en cours SAUTE la replanification, mais pas la parade.
        if (e[0x43] == 0)
        {
            registerEbx = ReplanPurpleKnightAction_004D1F70(e, boldness, timid, &distance, &heading);
        }

        TryPurpleKnightGuard_004D2EFB(e, chased, timid, heading, distance, registerEbx);
    }

    /// <summary>
    /// Ghidra: <c>0x004d20f3..0x004d293b</c> — <b>le premier temps</b> du Pourpre preux :
    /// l'animation, le prelude de geste, puis la case de la posture.
    ///
    /// <para><b>Quatorze cases, dont onze servies</b> : 0 attendre, 1 se tourner, 2 approcher,
    /// 3 louvoyer, 5 parer, 6 bondir, 7 marcher droit, 10 l'effet, 11 le coup, 12 le combo,
    /// 13 la charge. Les cases 4, 8 et 9 pointent la fin du bloc.</para>
    /// </summary>
    private static void DecidePurpleKnightAction_004D1F70(
        byte* e, short* probes, byte* model, int boldness, int distance, int heading, int speed)
    {
        // Ghidra: 0x004d20f3..0x004d2153 — FUN_004db5a0 recopiee en ligne, au mot pres.
        AdvanceEntityAnimation_004DB5A0((uint*)e);

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le prelude de geste
        if ((flags & 2) != 0)
        {
            PurpleKnightGesture_004D2175(e, probes, model);
        }
        else if ((flags & 1) != 0 && *(short*)(e + 0x1a) < 3)
        {
            // Ghidra: LAB_004d2394 — en l'air, dans les trois premieres images de l'animation, le
            // bond peut se changer en attaque : hardiesse chances sur soixante, a chaque image.
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

                if (((heading + 0xc8) & 0xfff) <= 0x190)
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
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
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

                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    // Le virage prend DEUX fois la hardiesse, la ou le Varan n'en prend qu'une.
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

                // Le rebours ne descend que sur les images ou l'animation est au bout.
                if (phase == 1 && e[0x43] != 0 && (*(uint*)e & 0x2000000) != 0) e[0x43]--;

                if (phase != 0) return;

                *(uint*)e &= 0xfeffffff;
                *(ushort*)(e + 0x12) = 1;
                *(ushort*)(e + 0x18) = 6;
                *(ushort*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
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

                *(ushort*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x1a) = 0;
                *(ushort*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 10 : l'effet
            case 10:
                if (*(ushort*)(e + 0x12) == 1 && *(short*)(e + 0x1a) == 8)
                {
                    if (*(short*)(e + 0x16) == 2) SpawnEffectEntity(e, 0x36, 0, -1);   // Ghidra: FUN_0047a9d0
                    if (*(short*)(e + 0x16) == 1) SpawnEffectEntity(e, 0x4b, 0, -1);   // Ghidra: FUN_0047a9d0
                }

                if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;

                if (*(ushort*)(e + 0x12) != 0) return;

                *(uint*)e &= 0xfeffffff;
                *(ushort*)(e + 0x12) = 1;
                *(ushort*)(e + 0x18) = 5;
                *(ushort*)(e + 0x1a) = 0;

                if (boldness < 0x1e) *(int*)(e + 0x5c) = 0;

                // La variante 0 n'a pas d'effet : la posture rend la main a l'image meme.
                if (*(short*)(e + 0x16) == 0) e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 11 : le coup
            case 11:
            {
                ushort phase = *(ushort*)(e + 0x12);

                // C'est le prelude qui frappe ; la posture se contente de la cloture. Et elle rend
                // la posture 0 au lieu du rebours.
                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    e[0x42] = 0;
                    *(uint*)e &= 0xfffffffd;
                }

                if (phase != 0) return;

                *(uint*)e &= 0xfeffffff;
                *(ushort*)(e + 0x12) = 1;
                return;
            }

            // ---------------------------------------------------------- 12 : le combo
            case 12:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 2 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                // Premier coup fini : le second enchaine sur l'animation 5, prise a l'image 7.
                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfeffffff;
                    *(ushort*)(e + 0x12) = 2;
                    *(ushort*)(e + 0x18) = 5;
                    *(ushort*)(e + 0x1a) = 7;
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(ushort*)(e + 0x12) = 1;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(ushort*)(e + 0x18) = 3;
                *(ushort*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 13 : la charge
            case 13:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 2)
                {
                    // Ghidra: 0x004d2895 — l'elan perd un seizieme par image ; sous 0xa000 la charge
                    // est finie.
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

                    // A la quatrieme image de l'animation, il s'elance : l'elan vient de la variante.
                    if (*(short*)(e + 0x1a) >= 4)
                    {
                        *(uint*)e |= 2;
                        *(ushort*)(e + 0x12) = 2;
                        *(int*)(e + 0x5c) = (*(short*)(e + 0x16) + 0x0f) << 0xd;
                    }
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(uint*)e &= 0xfeffffff;
                *(ushort*)(e + 0x12) = 1;
                *(ushort*)(e + 0x18) = 4;
                *(ushort*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 4, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004d2175..0x004d2392</c> — <b>le prelude de geste</b>, trois blocs successifs.
    ///
    /// <list type="number">
    /// <item><b>Hors combo et hors charge</b> : a l'image 8, la sonde A a pleine attaque, clignotement
    /// 0x10. L'animation finie ferme le geste — et remet le rebours a zero seulement a terre.</item>
    /// <item><b>Le combo</b> (0x0c) : a l'image 8 de l'animation 3 et a l'image 10 de l'animation 5,
    /// la sonde A a <b>trois quarts</b> de l'attaque. Le premier coup (<c>+0x12 == 1</c>) pose un
    /// clignotement nul et ne fait pas chanceler ; le second, 0x10.</item>
    /// <item><b>La charge</b> (0x0d) : la sonde B a chaque image, a moitie d'attaque. Au premier
    /// contact la victime est projetee dans le cap du chevalier a <c>+0x5c &gt;&gt; 13</c>, 0x18000 de
    /// haut, l'effet 0x39 nait, et le geste se ferme.</item>
    /// </list>
    /// </summary>
    private static void PurpleKnightGesture_004D2175(byte* e, short* probes, byte* model)
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
                *(short*)(model + 0x30) = (short)(power - (power >> 2));
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
        *(short*)(model + 0x30) = (short)(attack >> 1);
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

    /// <summary>Ghidra: la double condition relue deux fois par le combo — image 8 de l'animation 3,
    /// ou image 10 de l'animation 5.</summary>
    private static bool ComboStrikeFrame(byte* e)
    {
        short frame = *(short*)(e + 0x1a);
        return (frame == 8 && *(short*)(e + 0x18) == 3) || (frame == 10 && *(short*)(e + 0x18) == 5);
    }

    /// <summary>
    /// Ghidra: <c>0x004d2b81..0x004d2efa</c> — <b>le troisieme temps</b> du Pourpre preux. Rend le
    /// dernier tirage, que <see cref="TryPurpleKnightGuard_004D2EFB"/> relit dans EBX, et remplace
    /// la distance et le cap par ceux qu'il recalcule a plat.
    ///
    /// <para><b>Trois plages exclusives</b>, bornees a 0xfa et 0x258. Au contact, la moitie des
    /// tirages tentent l'attaque dans un cone ouvert par la timidite a une portee tiree : le coup
    /// trois fois sur cinq, le combo sinon. A mi-distance, face a la cible, <b>la charge</b> une
    /// fois sur cinq. Au loin, face a la cible, <b>l'effet</b> (posture 10).</para>
    /// </summary>
    private static int ReplanPurpleKnightAction_004D1F70(
        byte* e, int boldness, int timid, int* distanceOut, int* headingOut)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        *distanceOut = distance;
        *headingOut = heading;

        *(ushort*)(e + 0x12) = 0;

        bool inCone = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;

        // ================================================================ le contact
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;                             // Ghidra: FUN_00547140

            if (roll < 0x32)
            {
                if (!inCone || distance >= CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
                else if (roll < 0x1e)
                {
                    // Ghidra: 0x004d2c2a — armer LE COUP.
                    uint flags = *(uint*)e;
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(uint*)e = (flags & 0xfeffffff) | 2;
                    *(int*)(e + 0x5c) = 0;
                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                }
                else
                {
                    e[0x42] = 0x0c;
                    e[0x43] = 1;
                    *(ushort*)(e + 0x12) = 0;
                }
            }
            else if (roll >= 0x3c && roll < 0x46 && distance > boldness + 0x64)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();             // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 2;
                e[0x43] = SceneEntityDelay(timid);
                *(ushort*)(e + 0x10) = 0x50;
            }

            return roll;
        }

        // ================================================================ la mi-distance
        if (distance < 0x258)
        {
            int roll = CrtRand() % 100;                             // Ghidra: FUN_00547140

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) < 0x800)
                {
                    // Ghidra: LAB_004d2d22 — face a la cible : LA CHARGE.
                    e[0x42] = 0x0d;
                    e[0x43] = 1;
                    *(ushort*)(e + 0x12) = 0;
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                e[0x43] = SceneEntityDelay(timid);
                *(ushort*)(e + 0x10) = 0x50;
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();             // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
            }
            else if (roll < 0x50)
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);  // Ghidra: FUN_00547140
            }
            else
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(ushort*)(e + 0x12) = 0;
            }

            return roll;
        }

        // ================================================================ le lointain
        int far = CrtRand() % 100;                                  // Ghidra: FUN_00547140

        if (far < 0x1e)
        {
            if (far < boldness * 2 + 0x0a)
            {
                e[0x42] = 2;
                e[0x43] = SceneEntityDelay(timid);
                *(ushort*)(e + 0x10) = 0x64;
            }
            else
            {
                e[0x42] = 0;
                int wait = CrtRand() % 10;                          // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = (byte)(wait + (byte)(timid >> 2));
                *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            }
        }
        else if (far < 0x28)
        {
            e[0x42] = 3;
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = SceneEntityDelay(timid);
            *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);      // Ghidra: FUN_00547140
        }
        else if (far < 0x3c)
        {
            e[0x42] = 1;
            e[0x43] = 1;
        }
        else if (far < 0x50)
        {
            if (inCone)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(ushort*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 2;
                e[0x43] = SceneEntityDelay(timid);
                *(ushort*)(e + 0x10) = 0x64;
            }
        }
        else
        {
            e[0x42] = 6;
            e[0x43] = 1;
            *(short*)(e + 0x10) = (short)CrtRand();                 // Ghidra: FUN_00547140
        }

        return far;
    }

    /// <summary>
    /// Ghidra: <c>LAB_004d2efb..0x004d2f73</c> — <b>la parade</b>, derniere chose que fait le
    /// Pourpre preux a chaque image ou il bouge, que sa replanification ait eu lieu ou non.
    ///
    /// <para>Six conditions, toutes necessaires : la cible dans un cone de
    /// <c>timidite * 8 + 0x400</c> centre sur <c>timidite * 4</c> ; <b>la cible porte son bit de
    /// geste</b> (<c>2</c>) ; <paramref name="registerEbx"/> sous la timidite ; lui-meme a terre ; sa
    /// posture 3 ou moins ; la distance sous 0xc8. Il prend alors la posture 5 : animation 6 figee
    /// a l'image 7, <c>hasard % 5 + 5</c> images.</para>
    ///
    /// <para><b>Le troisieme test compare un registre.</b> EBX porte le dernier tirage
    /// <c>hasard % 100</c> de la replanification si elle a eu lieu, sinon le pas d'avance de
    /// l'integration. Un chevalier immobile (pas nul) pare donc des qu'il est timide ; un chevalier
    /// qui vient de replanifier pare selon un tirage qui a deja servi a choisir sa posture.</para>
    /// </summary>
    private static void TryPurpleKnightGuard_004D2EFB(
        byte* e, byte* chased, int timid, int heading, int distance, int registerEbx)
    {
        if (((heading + timid * 4 + 0x200) & 0xfff) > timid * 8 + 0x400) return;
        if ((*chased & 2) == 0) return;
        if (registerEbx >= timid) return;
        if ((*(uint*)e & 1) != 0) return;
        if (e[0x42] > 3) return;
        if (distance >= 0xc8) return;

        e[0x42] = 5;
        int roll = CrtRand();                                       // Ghidra: FUN_00547140
        *(ushort*)(e + 0x18) = 6;
        *(ushort*)(e + 0x1a) = 7;
        *(int*)(e + 0x5c) = 0;
        *(ushort*)(e + 0x12) = 1;
        e[0x43] = (byte)(roll % 5 + 5);
        *(uint*)e &= 0xfeffffff;
    }
}
