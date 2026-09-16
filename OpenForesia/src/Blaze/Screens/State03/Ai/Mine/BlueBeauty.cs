// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA BELLE BLEUE — 0x0048B530, sous-type 7 de la mine abandonnee, sous-zones 5 a 8.
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, colonne ia = 7) : « Belle
// bleue » dans les quatre sous-zones du bloc 0x00553fa8. TROIS CORPS D'UN COUP :
// 0x004BC530 (le Labyrinthe des morts) est identique instruction pour instruction, et
// 0x004BE200 ne differe que par l'ordre de deux chargements de la posture 9, sans ecriture
// entre eux. 883 instructions, absentes de Blaze.c ; releve Legacy/Raw/Blaze/FUN_0048B530.asm.
//
// CE QUI LA DISTINGUE DE TOUT LE RESTE DU DEPOT :
//
//   - ELLE FRAPPE PAR CONTACT, A CHAQUE IMAGE. Le bit de geste 0x2 est pose sans condition en
//     tete de routine, donc le balayage du groupe tourne a chaque image ou elle a de la vitesse,
//     quelle que soit sa posture. Sa sonde est {0, -50, 50, 100, 0, 150} : elle part SOUS elle.
//   - ELLE NAGE. Son integration est celle des terrestres (FUN_00488160, recopiee en ligne) : les
//     vitesses sont freinees d'un huitieme et d'un 256e par image, la verticale tend vers le +0x9a
//     de l'entite, et la poussee +0x5c part le long du cap. Ce qui lui est propre vient apres le
//     placement. Un saut (posture 6) ou un bond sur la proie (posture 7) la fait quitter le « sol ».
//   - ELLE ENFLE. La glissade (posture 2) et l'elan (posture 8) gonflent l'echelle Z avec la
//     poussee ; le bond sur la proie fait onduler les trois echelles sur un sinus, et la tient
//     accrochee tant que le joueur ne se debat pas.
//   - UNE COLLISION LA RETOURNE (posture 9, cap + 0x400 + un tirage sur dix bits) ; en l'air,
//     elle est ramenee a sa position d'avant l'image et se repose quarante images.
//
// La charpente commune est dans `Ai/SceneEntityAiCommon.cs` ; le chercheur de proie
// (FUN_0048C170 = FUN_004A1020 au saut pres) dans `Ai/Forest/Varan.cs`.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x0048b530</c> — <b>l'intelligence de la Belle bleue</b>. Voir l'en-tete du
    /// fichier. Les copies <c>0x004BC530</c> et <c>0x004BE200</c> sont branchees sur ce corps.
    /// </summary>
    private static void TickSceneEntityAi_0048B530(byte* record)
    {
        byte* e = record;

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = -0x32;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x96;

        // La premiere image choisit la proie : mode 3, un membre au hasard. Ghidra: FUN_0048c170.
        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);
        }

        // Le geste est TOUJOURS arme : le contact blesse a chaque image.
        *(uint*)e |= 2;

        byte* chased = *(byte**)(e + 0x6c);
        int savedX = *(int*)(e + 0x1c);
        int savedZ = *(int*)(e + 0x24);
        short savedYaw = *(short*)(e + 0x32);

        // Les trois blocs sont resolus et aucun n'est relu : l'appel est garde pour sa forme.
        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);
        int timid = 0x40 - boldness;

        if (speed != 0)
        {
            AdvanceEntityAnimation_004DB5A0((uint*)e);   // recopie en ligne dans le binaire

            if ((*(uint*)e & 2) != 0)
            {
                byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(ushort*)(victim + 0x96) = 0x10;
                    *(uint*)victim |= 0x40;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);

                    if (e[0x42] != 7)
                        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                    else
                        Aud.PlayPositionalSound(ZoneSound(0x3e), 0x200, 0x2600, (int*)(victim + 0x1c));
                }
            }

            DecideBlueBeautyAction_0048B530(e, chased, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- appliquer
        SwimBlueBeauty_0048BC6D(e, speed, savedX, savedZ);

        ResetSceneEntityOnStun_004C3810(e);

        if (speed == 0)
        {
            *(short*)(e + 0x32) = savedYaw;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanBlueBeautyAction_0048BDDE(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0048b6e0..0x0048bc6d</c> — <b>les dix postures</b>, table de sauts a
    /// <c>0x0048c140</c> (les entrees 4 et 5 vont droit a l'integration).
    /// </summary>
    private static void DecideBlueBeautyAction_0048B530(
        byte* e, byte* chased, int boldness, int distance, int heading, int speed)
    {
        switch (e[0x42])
        {
            // ------------------------------------------------------------ 0 : attendre
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

                if (e[0x43] != 0)
                {
                    *(int*)(e + 0x5c) = 0;
                    e[0x43]--;
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                return;

            // ------------------------------------------------------------ 1 : se tourner
            case 1:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                }

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if ((heading & 0xfff) < 0x800)
                {
                    *(int*)(e + 0x5c) = 0;
                    *(short*)(e + 0x32) += (short)(boldness + 0x80);
                    return;
                }

                *(short*)(e + 0x32) += (short)(-0x80 - boldness);
                *(int*)(e + 0x5c) = 0;
                return;

            // ------------------------------------------------------------ 2 : glisser
            case 2:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    int thrust = *(int*)(e + 0x5c);
                    *(short*)(e + 0x18) = 2;
                    thrust -= thrust >> 4;                    // la poussee s'eteint d'un seizieme
                    *(short*)(e + 0x1a) = 1;
                    *(int*)(e + 0x5c) = thrust;

                    if (distance < *(short*)(e + 0x10))
                    {
                        e[0x43] = 0;
                    }
                    else if ((thrust & unchecked((int)0xfffff000)) < 0x6000)
                    {
                        e[0x43] = 0;
                        *(short*)(e + 0x2c) = 0x1000;
                        *(short*)(e + 0x2a) = 0x1000;
                        *(int*)(e + 0x5c) = 0;
                    }

                    *(short*)(e + 0x2c) = (short)((*(int*)(e + 0x5c) >> 5) + 0x1000);
                }

                if (phase != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 2;
                *(short*)(e + 0x1a) = 1;
                *(short*)(e + 0x12) = 1;

                int roll = CrtRand() % 5;
                *(int*)(e + 0x5c) = ((roll + 3) << 0xe) + ((boldness >> 3) & ~3);
                return;
            }

            // ------------------------------------------------------------ 3 : nager vers +0x10
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    *(short*)(e + 0x18) = 2;
                    *(short*)(e + 0x1a) = 1;
                    if (e[0x43] != 0) e[0x43]--;

                    short course = *(short*)(e + 0x10);
                    *(short*)(e + 0x32) = course;

                    int stroke = (*(short*)(e + 0x14) >> 4) * speed;
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * stroke) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * stroke) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    short power = *(short*)(e + 0x14);
                    power -= (short)(power >> 3);
                    *(short*)(e + 0x14) = power;

                    if (power < 5)
                    {
                        e[0x43] = 0;
                        *(short*)(e + 0x2c) = 0x1000;
                        *(short*)(e + 0x2a) = 0x1000;
                    }

                    *(short*)(e + 0x2c) = (short)(power + 0x1000);
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x18) = 2;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 1;
                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x14) = (short)((CrtRand() % 0x14 + boldness + 10) << 4);
                return;

            // ------------------------------------------------------------ 6 : sauter
            case 6:
                if (*(short*)(e + 0x12) == 0)
                {
                    uint flags = *(uint*)e;
                    if ((flags & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        *(uint*)e = flags & 0xfeffffff;
                        *(short*)(e + 0x18) = 2;
                        *(short*)(e + 0x1a) = 0;

                        int impulse = (boldness + 0x100) << 6;
                        int step = (speed * impulse) >> 0xc;
                        *(int*)(e + 0x5c) = impulse;

                        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;
                        *(uint*)e |= 1;
                        *(short*)(e + 0x12) = 1;
                    }
                }

                if ((*(uint*)e & 1) == 0) e[0x43] = 0;
                return;

            // ------------------------------------------------------------ 7 : bondir sur la proie
            case 7:
                LeapOntoPrey_0048B9F2(e, chased, distance);
                return;

            // ------------------------------------------------------------ 8 : prendre de l'elan
            case 8:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    int thrust = *(int*)(e + 0x5c);
                    *(short*)(e + 0x18) = 2;
                    *(short*)(e + 0x1a) = 1;
                    thrust -= thrust >> 3;
                    *(int*)(e + 0x5c) = thrust;

                    if (thrust < 0x5000)
                    {
                        e[0x43] = 0;
                        *(int*)(e + 0x5c) = 0;
                        *(short*)(e + 0x2c) = 0x1000;
                        *(short*)(e + 0x2a) = 0x1000;
                    }

                    *(short*)(e + 0x2c) = (short)((*(int*)(e + 0x5c) >> 5) + 0x1000);
                }

                if (phase != 0) return;

                *(short*)(e + 0x18) = 2;
                *(short*)(e + 0x1a) = 1;
                int roll = CrtRand() % 0x32;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = ((boldness & ~3) + roll * 4) << 0xa;
                return;
            }

            // ------------------------------------------------------------ 9 : se tourner vers +0x10
            case 9:
            {
                *(short*)(e + 0x18) = 0;

                int target = *(short*)(e + 0x10);
                short yaw = *(short*)(e + 0x32);
                yaw = (short)(yaw + ((target - yaw) >> 3));
                *(short*)(e + 0x32) = yaw;

                int gap = target - yaw;
                if (gap < 0) gap = yaw - target;
                if (gap < 0x32) e[0x43] = 0;

                *(int*)(e + 0x5c) = 0;
                short depth = *(short*)(e + 0x2c);
                *(short*)(e + 0x2c) = (short)(((0x1000 - depth) >> 2) + depth);
                return;
            }

            // 4, 5 et tout le reste : l'integration seule.
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0048b9f2..0x0048bb94</c> — <b>la posture 7, le bond sur la proie</b>.
    ///
    /// <para>
    /// <b>Trois phases dans <c>+0x12</c>.</b> A 0 elle decolle de <c>0x28000</c> ; a 1 elle vise la
    /// proie au seizieme de l'ecart par image et, une fois retombee a moins de 100 unites, passe a
    /// 2 avec une tenue de 100 images. A 2 et au-dela, <c>+0x12</c> devient une <b>phase</b> : elle
    /// colle la proie a moitie d'ecart par image, fait onduler ses trois echelles sur le sinus de
    /// cette phase (qui avance de 200, reboucle, et saute 0 a 2), et decompte sa tenue.
    /// </para>
    /// <para>
    /// <b>Le joueur se debat comme contre le Poisson volant</b> : le pad du membre, par
    /// <c>0x005e9188</c> puis <c>0x00604a20</c>, use la tenue de 3 de plus par image quand une
    /// direction est tenue. Une proie mourante (<c>0x40000000</c>) la libere a l'image suivante.
    /// </para>
    /// </summary>
    private static void LeapOntoPrey_0048B9F2(byte* e, byte* chased, int distance)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            uint flags = *(uint*)e;
            if ((flags & 1) != 0)
            {
                e[0x43] = 0;
            }
            else
            {
                *(uint*)e = (flags & 0xfeffffff) | 1;
                *(short*)(e + 0x18) = 2;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x54) += 0x28000;
                *(short*)(e + 0x12) = 1;
            }
        }

        if (*(short*)(e + 0x12) == 1)
        {
            int x = *(int*)(e + 0x1c);
            int z = *(int*)(e + 0x24);
            *(short*)(e + 0x18) = 2;
            *(short*)(e + 0x1a) = 0;
            *(int*)(e + 0x50) = (*(int*)(chased + 0x1c) - x) >> 4;
            *(int*)(e + 0x58) = (*(int*)(chased + 0x24) - z) >> 4;

            if ((e[0] & 1) == 0)
            {
                int dx = (*(int*)(chased + 0x1c) - x) >> 0xc;
                int dz = (*(int*)(chased + 0x24) - z) >> 0xc;
                distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

                if (distance > 100)
                {
                    e[0x43] = 0;
                }
                else
                {
                    *(short*)(e + 0x12) = 2;
                    *(short*)(e + 0x14) = 100;
                }
            }

            if ((*(uint*)(e + 0x90) & 0x2000ad82) != 0) e[0x43] = 0;
        }

        if (*(short*)(e + 0x12) < 2) return;

        *(int*)(e + 0x50) = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 1;
        *(int*)(e + 0x58) = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 1;

        short phase = *(short*)(e + 0x12);
        *(short*)(e + 0x2a) = (short)((int)Winsys3d.GetSinTableEntry(phase) + 0x2000);
        *(short*)(e + 0x28) = (short)((Winsys3d.GetCosTableEntry(phase) >> 2) + 0x1000);
        int wave = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += 0xc8;
        *(int*)(e + 0x5c) = 0;
        *(short*)(e + 0x2c) = (short)((wave >> 2) + 0x1000);

        if (*(short*)(e + 0x12) >= 0x1000) *(short*)(e + 0x12) -= 0x1000;
        if (*(short*)(e + 0x12) <= 2) *(short*)(e + 0x12) += 2;

        byte* pad = Inp.s_InputStateBase_00604a20 + Inp.s_PlayerSlots_005e9188[chased[8] * 0x4c] * 0x10;
        if ((pad[2] & 0xf0) != 0)
        {
            *(short*)(e + 0x14) += -3;
            if (*(short*)(e + 0x14) < 0) *(short*)(e + 0x14) = 1;
        }

        if ((*(uint*)chased & 0x40000000) != 0) *(short*)(e + 0x14) = 1;

        *(short*)(e + 0x14) -= 1;
        if (*(short*)(e + 0x14) == 0)
        {
            e[0x43] = 0;
            *(short*)(e + 0x2a) = 0x1000;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0048bc6d..0x0048bdad</c> et <c>0x0048c08d..0x0048c138</c> — <b>la nage</b>.
    ///
    /// <para>
    /// <b>Les vitesses se freinent en trois termes</b> : <c>v - v/8 - v/256</c> a l'horizontale ;
    /// la verticale tend vers le <c>+0x9a</c> de l'entite, <c>v + g/256 - v/256 - g</c>. La poussee
    /// <c>+0x5c</c>, ponderee de la vitesse de situation, part le long du cap <c>+0x32</c>.
    /// </para>
    /// <para>
    /// <b>Apres le placement</b>, le mot de contact perd <c>0x300</c> et gagne <c>0x800</c> si le
    /// porteur <c>+0x7c</c> designe une autre carte que <c>0x005de038</c>. Au sol elle est posee a
    /// <c>+0x60</c> ; plus de <c>0xc000</c> au-dessus elle repasse en l'air. <b>Une collision
    /// <c>0x2000ad82</c></b> la retourne (au sol) ou la ramene a sa position d'avant l'image et
    /// la pose quarante images (en l'air). Toute echelle est remise a <c>0x1000</c> tant que le
    /// clignotement de degat dure.
    /// </para>
    /// </summary>
    private static void SwimBlueBeauty_0048BC6D(byte* e, int speed, int savedX, int savedZ)
    {
        // Ghidra: 0x0048bc6d..0x0048bd05 — FUN_00488160 recopiee en ligne, au terme pres.
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);   // Ghidra: FUN_00533940

        uint contact = *(uint*)(e + 0x90) & 0xfffffcff;
        *(uint*)(e + 0x90) = contact;

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
            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x20) = floor;
            *(int*)(e + 0x54) = 0;
        }
        else
        {
            // Ghidra: LAB_0048c08d — un non-volant trop haut repasse en l'air.
            if (!airborne && *(int*)(e + 0x20) > *(int*)(e + 0x60))
                *(uint*)e = flags | 1;

            // Ghidra: LAB_0048c09f
            if ((*(uint*)(e + 0x90) & 0x2000ad82) != 0)
            {
                if ((e[0] & 1) == 0)
                {
                    short yaw = *(short*)(e + 0x32);
                    e[0x42] = 9;
                    e[0x43] = 1;
                    *(short*)(e + 0x10) = (short)(SignedMask(CrtRand(), 0x3ff) + yaw + 0x400);
                    if (yaw >= 0x1000) *(short*)(e + 0x32) = (short)(yaw - 0x1000);
                }
                else
                {
                    *(int*)(e + 0x1c) = savedX;
                    *(int*)(e + 0x5c) = 0;
                    *(int*)(e + 0x50) = 0;
                    *(int*)(e + 0x58) = 0;
                    *(int*)(e + 0x24) = savedZ;
                    e[0x42] = 0;
                    e[0x43] = 0x28;
                }

                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x2c) = 0x1000;
                *(short*)(e + 0x2a) = 0x1000;
                *(int*)(e + 0x5c) = 0;

                ResetScalesWhileFlashing(e);
                return;
            }
        }

        ResetScalesWhileFlashing(e);
    }

    /// <summary>Ghidra: <c>LAB_0048bd98</c> — tant que le clignotement de degat dure, les trois
    /// echelles sont ramenees a <c>0x1000</c>.</summary>
    private static void ResetScalesWhileFlashing(byte* e)
    {
        if (*(short*)(e + 0x96) == 0) return;

        *(short*)(e + 0x28) = 0x1000;
        *(short*)(e + 0x2a) = 0x1000;
        *(short*)(e + 0x2c) = 0x1000;
    }

    /// <summary>
    /// Ghidra: <c>0x0048bdde..0x0048c08c</c> — <b>le troisieme temps</b> de la Belle bleue.
    ///
    /// <para>
    /// Trois plages de distance, <b>testees l'une apres l'autre et non en cascade</b> : sous
    /// <c>0xfa</c> un premier tirage, puis entre <c>0xfa</c> et <c>0x258</c> un second, puis au-dela
    /// un troisieme. Les deux premieres plages etant disjointes, un seul tirage compte, mais
    /// chacun consomme le generateur.
    /// </para>
    /// </summary>
    private static void ReplanBlueBeautyAction_0048BDDE(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        // ================================================================ de pres
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            }
            else if (roll < 0x32)
            {
                e[0x42] = 0;
                e[0x43] = (byte)((timid >> 1) + 10);
                *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                *(short*)(e + 0x12) = 0;
            }
            else if (roll < 0x5a && distance > boldness + 100)
            {
                e[0x42] = 7;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 9;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14 && ((heading + 0x400) & 0xfff) >= 0x800)
            {
                e[0x42] = 1;
                e[0x43] = 1;
            }
            else if (roll < 0x14)
            {
                RestBlueBeauty(e, boldness, timid);
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 9;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
            else if (roll < 0x50)
            {
                e[0x42] = 8;
                e[0x43] = 1;
            }
            else
            {
                RestBlueBeauty(e, boldness, timid);
            }
        }

        // ================================================================ de loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x32)
        {
            if (far < boldness * 2 + 10)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 100;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
                return;
            }

            RestBlueBeauty(e, boldness, timid);
            return;
        }

        if (far < 0x46)
        {
            e[0x42] = 8;
            e[0x43] = 1;
            return;
        }

        if (far < 0x5a)
        {
            e[0x42] = 1;
            e[0x43] = 1;
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }

    /// <summary>Ghidra: <c>LAB_0048bf97</c> et <c>LAB_0048c018</c> — <b>le repos</b> de la Belle
    /// bleue : posture 0, une duree courte (<c>timidite / 4</c> plus un tirage sur dix), et une
    /// distance de reveil <c>+0x10 = hardiesse * 2 + 0x50</c>.</summary>
    private static void RestBlueBeauty(byte* e, int boldness, int timid)
    {
        e[0x42] = 0;
        int wait = CrtRand() % 10;
        *(short*)(e + 0x12) = 0;
        e[0x43] = (byte)(wait + (timid >> 2));
        *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
    }
}
