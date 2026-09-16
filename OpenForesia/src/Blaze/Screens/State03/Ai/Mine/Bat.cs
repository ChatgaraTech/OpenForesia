// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA CHAUVE-SOURIS — 0x00489CA0, sous-type 3 de la mine abandonnee (sous-zones 0 a 3).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, colonne ia = 3), dans les deux blocs
// qui la portent. TROIS CORPS D'UN COUP :
//
//   0x00489CA0  mine, sous-zones 0 a 2      le releve, Legacy/Raw/Blaze/FUN_00489CA0.asm
//   0x004D01C0  chateau de l'immortel       identique instruction pour instruction
//   0x004901B0  mine, sous-zones 3, 4 et 9  28 lignes d'ecart : un chargement de +0x10 avance en
//                                           registre, et deux comparaisons « 100 - tirage » dont
//                                           les operandes sont calcules dans un autre ordre
//
// 1 236 instructions, absentes de Blaze.c ; table de sauts a quatorze entrees a 0x0048AD40. Le
// chercheur de proie FUN_004898E0 est FUN_004A1020 au saut pres.
//
// LA CHARPENTE DU GREMLIN (Ai/OldTower/Gremlin.cs) : volante a deux regimes fixes par +0x16,
// sauvegarde du placement sans la hauteur, integration de volante, meme heurt de mur. Ce qui lui
// appartient :
//
//   - ELLE MORD AU CONTACT, A CHAQUE IMAGE DE GESTE, SANS CONDITION D'IMAGE : le prelude croise sa
//     sonde {0, 0x14, 0x50, 0x64, 0, 0x50} tant que le bit 2 tient, clignotement 4.
//   - ELLE PIQUE (posture 12) : elle monte de 0xc8 unites au-dessus de son sol, puis fond sur sa
//     proie pendant 0x21 images a poussee fixe, et s'eteint en planant.
//   - ELLE S'ACCROCHE EN TOURNANT (posture 13, variante 1 seulement) : une spirale qui se resserre
//     autour de la proie, puis elle se fixe a 80 unites d'elle, 0x28 au-dessus, et mord a l'image 2
//     au QUART de son attaque. Le joueur qui se debat use sa tenue de cinq images d'un coup.
//   - ELLE SE RETOURNE (posture 9) vers un cap tire, et ne marche droit qu'une fois alignee a 20
//     pas pres.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00489ca0</c> (et ses deux copies) — <b>l'intelligence de la Chauve-souris</b>.
    /// Voir l'en-tete du fichier.
    /// </summary>
    private static void TickSceneEntityAi_00489CA0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x2c] — la sonde, dont deux mots sont poses avant le quatrieme push.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x14;
        probe[2] = 0x50;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x50;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004898e0, copie de FUN_004a1020
            *(int*)(e + 0x48) = *(int*)(e + 0x20);

            if (*(short*)(e + 0x16) >= 10)
            {
                *(short*)(e + 0x16) -= 10;
                *(uint*)e &= 0xffffffdf;
            }
            else
            {
                *(uint*)e |= 0x20;
            }
        }

        *(uint*)e &= 0xfffffffe;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);
        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        if (speed != 0)
        {
            DecideBatAction_00489CA0(e, probe, model, chased, boldness, distance, heading, speed);
            HoverBat_0048A68A(e, chased);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);             // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        uint flags = *(uint*)e;
        uint contact = *(uint*)(e + 0x90) & 0xfffff8ff;
        *(uint*)(e + 0x90) = contact;
        if ((flags & 0x20) != 0) *(uint*)(e + 0x90) = contact & 0xdfffdfff;

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (*(uint*)(e + 0x90) & 0x8000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((*(uint*)(e + 0x90) & 0x2000a882) != 0)
        {
            // ================================================================ heurtee
            if (e[0x42] != 7)
            {
                e[0x42] = 7;
                int roll = CrtRand();
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(roll % 0x28 + 0x14);
            }

            *(uint*)e &= 0xfffffffd;

            *(int*)(e + 0x24) = keep.Z;
            *(int*)(e + 0x1c) = keep.X;
            *(int*)(e + 0x7c) = keep.At7c;
            *(uint*)(e + 0x90) = keep.At90;                         // `and eax,0FFFFh` puis le dword
            *(int*)(e + 0x80) = keep.At80;
            *(int*)(e + 0x84) = keep.At84;
            *(int*)(e + 0x88) = keep.At88;
            *(int*)(e + 0x8c) = keep.At8c;
            *(int*)(e + 0x64) = keep.At64;
            *(int*)(e + 0x60) = keep.At60;

            int side = ProbeClearSide_004D5E60(e, 0x2000a882);
            if ((short)side == 0) side = SignedMask(CrtRand(), 1) * 2 - 1;

            *(short*)(e + 0x32) += (short)((boldness + 0x40) * side);
        }

        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanBatAction_0048A927(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x00489e16..0x0048a68a</c> et <c>0x0048acab..0x0048ad38</c> — <b>le premier
    /// temps</b> : l'animation, la morsure, puis la posture. Quatorze cases, dont neuf servies :
    /// 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 7 marcher droit, 9 se retourner,
    /// 0x0b s'agripper, 0x0c piquer, 0x0d tourner et s'accrocher.
    /// </summary>
    private static void DecideBatAction_00489CA0(
        byte* e, short* probe, byte* model, byte* chased, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ la morsure
        if ((*(uint*)e & 2) != 0)
        {
            if (e[0x42] == 0x0d)
            {
                // Ghidra: LAB_0048acab — accrochee : a l'image 2, au quart de l'attaque, et le geste
                // n'est pas clos ici.
                if (*(short*)(e + 0x1a) == 2)
                {
                    byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                    if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 2)
                    {
                        *(ushort*)(prey + 0x96) = 4;
                        *(uint*)prey |= 0x40;

                        short power = *(short*)(model + 0x30);
                        *(short*)(model + 0x30) = (short)(power >> 2);
                        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                        *(short*)(model + 0x30) = power;

                        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                    }
                }
            }
            else
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(ushort*)(prey + 0x96) = 4;
                    *(uint*)prey |= 0x40;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }

                uint gesture = *(uint*)e;
                if ((gesture & 0x2000000) != 0 && e[0x42] != 0x0c)
                {
                    *(uint*)e = gesture & 0xfffffffd;
                    e[0x43] = 0;
                }
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

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 0;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                }

                *(int*)(e + 0x5c) = 0;

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    return;
                }

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x6000;
                }

                if (*(short*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnBatTowardPrey(e, heading, boldness * 2 + 0x28);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    TurnBatTowardPrey(e, heading, boldness * 2 + 0x32);
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0xa000;
                return;
            }

            // ---------------------------------------------------------- 9 : se retourner
            case 9:
                if (*(short*)(e + 0x12) == 0)
                {
                    int roll = CrtRand();
                    *(short*)(e + 0x32) &= 0x0fff;
                    *(short*)(e + 0x12) = 1;
                    *(short*)(e + 0x18) = 0;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x10) = (short)SignedMask(roll, 0xfff);
                    *(uint*)e |= 0x1000000;
                }

                if (*(short*)(e + 0x12) != 1) return;

                {
                    int target = *(short*)(e + 0x10);
                    short yaw = *(short*)(e + 0x32);
                    yaw = (short)(yaw + ((target - yaw) >> 3));
                    *(short*)(e + 0x32) = yaw;

                    // Ecart SIGNE, pas absolu : un cap deja depasse compte comme atteint.
                    if (target - yaw < 0x14)
                    {
                        e[0x42] = 7;
                        e[0x43] = (byte)(CrtRand() % 0x14 + 0x1e);
                    }
                }
                return;

            // ---------------------------------------------------------- 0x0b : s'agripper
            case 0x0b:
            {
                int y = *(int*)(e + 0x20);
                int preyY = *(int*)(chased + 0x20);

                if (y + 0xb4000 < preyY)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                    return;
                }

                *(int*)(e + 0x20) = ((preyY - y + 0x28000) >> 2) + y;

                short phase = *(short*)(e + 0x12);

                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 0x0c : piquer
            case 0x0c:
                BatDive_0048A2A2(e, chased, boldness, heading);
                return;

            // ---------------------------------------------------------- 0x0d : tourner et s'accrocher
            case 0x0d:
                BatCircleAndCling_0048A40B(e, chased, boldness, heading);
                return;

            // ---------------------------------------------------------- 4, 5, 6, 8, 10 : rien
            default:
                return;
        }
    }

    /// <summary>Le virage vers la proie des postures 2 et 3, et du pique : rien dans un cone de
    /// <c>±0x100</c>, sinon <paramref name="amount"/> du bon cote.</summary>
    private static void TurnBatTowardPrey(byte* e, int heading, int amount)
    {
        if (((heading + 0x100) & 0xfff) <= 0x200) return;

        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? amount : -amount);
    }

    /// <summary>
    /// Ghidra: <c>0x0048a2a2..0x0048a406</c> — <b>le pique</b>, posture 0x0c.
    ///
    /// <para>
    /// <b>Phase 1, la montee</b> : trois unites (<c>0x3000</c>) par image jusqu'a <c>0xc8</c> unites au-dessus de son
    /// sol, puis geste arme et phase 2. <b>Phase 2, la chute</b> : poussee fixe de <c>0x19000</c>,
    /// la hauteur rejoint celle de la proie plus <c>0x28</c> d'un quart par image, et au bout de
    /// <c>0x21</c> images le geste retombe. Trop bas sous sa proie, elle renonce. <b>Phase 3</b> :
    /// la poussee s'eteint d'un huitieme par image jusqu'a <c>0xa000</c>.
    /// </para>
    /// </summary>
    private static void BatDive_0048A2A2(byte* e, byte* chased, int boldness, int heading)
    {
        if (*(short*)(e + 0x12) == 1)
        {
            int y = *(int*)(e + 0x20) + 0x3000;
            *(int*)(e + 0x20) = y;

            if ((y - *(int*)(e + 0x60)) >> 0xc > 0xc8)
            {
                *(short*)(e + 0x12) = 2;
                *(short*)(e + 0x10) = 0;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000002;
            }

            TurnBatTowardPrey(e, heading, boldness * 2 + 0x28);
        }

        if (*(short*)(e + 0x12) == 2)
        {
            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x5c) = 0x19000;

            if (y + 0xb4000 < *(int*)(chased + 0x20))
            {
                *(uint*)e &= 0xfffffffd;
                e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y + 0x28000) >> 2) + y;

            *(short*)(e + 0x10) += 1;
            short elapsed = *(short*)(e + 0x10);

            TurnBatTowardPrey(e, heading, boldness * 2 + 0x28);

            if (elapsed >= 0x21)
            {
                *(short*)(e + 0x12) = 3;
                *(uint*)e &= 0xfffffffd;
            }
        }

        short phase = *(short*)(e + 0x12);

        if (phase == 3)
        {
            int thrust = *(int*)(e + 0x5c);
            thrust -= thrust >> 3;
            *(int*)(e + 0x5c) = thrust;
            if ((thrust & unchecked((int)0xfffff000)) < 0xa000) e[0x43] = 0;
        }

        if (phase != 0) return;

        *(short*)(e + 0x1a) = 0;
        *(int*)(e + 0x5c) = 0;
        *(short*)(e + 0x12) = 1;
        *(uint*)e |= 0x1000000;
        *(short*)(e + 0x18) = 1;
    }

    /// <summary>
    /// Ghidra: <c>0x0048a40b..0x0048a687</c> — <b>la spirale et l'accroche</b>, posture 0x0d.
    ///
    /// <para>
    /// <b>Seule la variante 1 y entre</b> ; les autres rendent la main. <c>+0x12</c> porte une phase
    /// d'angle, <b>negative pendant la spirale</b> et positive une fois accrochee — le cap relatif
    /// de la proie, jamais nul.
    /// </para>
    /// <para>
    /// <b>La spirale</b> : elle rejoint d'un huitieme par image un point a 60 unites de la proie sur
    /// cette phase, et la hauteur de la proie plus <c>0x28</c>. Quand l'ecart en X a un point a 80
    /// unites tombe sous <c>0x5000</c>, la phase change de signe, le geste s'arme et l'animation 1
    /// repart. <b>L'accroche</b> : collee a 80 unites de la proie, <c>0x28</c> au-dessus, elle mord
    /// par le prelude. Une proie mourante la libere ; un joueur qui tient une direction lui retire
    /// cinq images de tenue.
    /// </para>
    /// <para>
    /// <b>Le calcul de l'ecart relit le sinus deux fois</b> et teste le signe par
    /// <c>test ecx,0FFFFFFF8h ; jl</c> — c'est-a-dire le signe de l'ecart lui-meme. Transcrit dans le
    /// meme ordre ; les appels a la table sont sans effet.
    /// </para>
    /// </summary>
    private static void BatCircleAndCling_0048A40B(byte* e, byte* chased, int boldness, int heading)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            if (*(short*)(e + 0x16) != 1)
            {
                e[0x43] = 0;
                return;
            }

            short angle = (short)(heading & 0xfff);
            *(short*)(e + 0x12) = angle;
            if (angle == 0) *(short*)(e + 0x12) = 1;

            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x12) = (short)-*(short*)(e + 0x12);
            *(uint*)e |= 0x1000000;
            *(short*)(e + 0x18) = 1;
            *(short*)(e + 0x1a) = 0;
        }

        // ================================================================ la spirale
        if (*(short*)(e + 0x12) < 0)
        {
            int phase = -*(short*)(e + 0x12);

            int x = *(int*)(e + 0x1c);
            *(int*)(e + 0x1c) = (((int)Winsys3d.GetSinTableEntry(phase) * 60 - x + *(int*)(chased + 0x1c)) >> 3) + x;

            int z = *(int*)(e + 0x24);
            *(int*)(e + 0x24) = ((Winsys3d.GetCosTableEntry(-*(short*)(e + 0x12)) * 60 - z + *(int*)(chased + 0x24)) >> 3) + z;

            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y + 0x28000) >> 3) + y;

            int gap = (int)Winsys3d.GetSinTableEntry(-*(short*)(e + 0x12)) * 80 - *(int*)(e + 0x1c) + *(int*)(chased + 0x1c);
            int reach = gap >= 0
                ? ((int)Winsys3d.GetSinTableEntry(-*(short*)(e + 0x12)) * 80 - *(int*)(e + 0x1c) + *(int*)(chased + 0x1c)) >> 3
                : -(((int)Winsys3d.GetSinTableEntry(-*(short*)(e + 0x12)) * 80 - *(int*)(e + 0x1c) + *(int*)(chased + 0x1c)) >> 3);

            if ((reach & unchecked((int)0xfffff000)) < 0x5000)
            {
                *(short*)(e + 0x12) = (short)-*(short*)(e + 0x12);
                *(uint*)e |= 2;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
            }

            TurnBatTowardPrey(e, heading, boldness * 2 + 0x80);

            e[0x43]--;
        }

        // ================================================================ l'accroche
        if (*(short*)(e + 0x12) <= 0) return;

        short hold = *(short*)(e + 0x12);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(hold) * 80 + *(int*)(chased + 0x1c);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12)) * 80 + *(int*)(chased + 0x24);
        *(int*)(e + 0x20) = *(int*)(chased + 0x20) + 0x28000;

        e[0x43]--;

        TurnBatTowardPrey(e, heading, boldness * 2 + 0x80);

        if ((*(uint*)chased & 0x40000000) != 0) e[0x43] = 0;

        byte* pad = Inp.s_InputStateBase_00604a20 + Inp.s_PlayerSlots_005e9188[chased[8] * 0x4c] * 0x10;
        if ((pad[2] & 0xf0) != 0)
        {
            int left = e[0x43] - 5;
            e[0x43] = (byte)(left > 0 ? left : 0);
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0048a68a..0x0048a744</c> — <b>le flottement</b> de la Chauve-souris.
    ///
    /// <para>
    /// En regime <c>0x20</c>, sauf pendant le pique, la hauteur rejoint d'un huitieme l'altitude
    /// <c>+0x48</c> plus <c>0x8c</c> unites, ondulee de <c>±60</c> ; et <c>+0x48</c> suit le plancher
    /// de la proie, <b>meme pendant le pique</b>. Sinon la cible est le plancher <c>+0x60</c> plus
    /// <c>0xaa</c> unites — plus haut que la Salamandre —, et le pique comme la posture 10 retiennent
    /// le flottement tant que leur geste est retombe.
    /// </para>
    /// </summary>
    private static void HoverBat_0048A68A(byte* e, byte* chased)
    {
        uint flags = *(uint*)e;
        byte posture = e[0x42];

        if ((flags & 0x20) != 0)
        {
            if (posture != 0x0c)
            {
                short phase = AdvanceSalamanderBob(e);
                int y = *(int*)(e + 0x20);
                *(int*)(e + 0x20) = ((*(int*)(e + 0x48) + (int)Winsys3d.GetSinTableEntry(phase) * 60 - y + 0x8c000) >> 3) + y;
            }

            int floor = *(int*)(chased + 0x64);
            if (((*(int*)(chased + 0x20) ^ floor) & unchecked((int)0xfffff000)) == 0)
                *(int*)(e + 0x48) = floor;
            return;
        }

        if ((posture == 0x0c || posture == 0x0a) && (flags & 2) == 0) return;

        short bob = AdvanceSalamanderBob(e);
        int height = *(int*)(e + 0x20);
        *(int*)(e + 0x20) = ((*(int*)(e + 0x60) + (int)Winsys3d.GetSinTableEntry(bob) * 60 - height + 0xaa000) >> 3) + height;
    }

    /// <summary>
    /// Ghidra: <c>0x0048a927..0x0048acaa</c> — <b>le troisieme temps</b> de la Chauve-souris.
    ///
    /// <para>
    /// Trois plages testees l'une apres l'autre (<c>0x96</c>, <c>0x258</c>). De pres, dans le cone :
    /// s'agripper trois fois sur cinq ; sinon, <b>si elle est de variante 1 et n'est pas deja
    /// accrochee</b>, tourner autour de la proie pour 40 a 79 images. A mi-distance, face a la
    /// proie, elle pique.
    /// </para>
    /// <para>
    /// <b>La meme branche morte que la Salamandre</b> au loin : un tirage d'au moins <c>0x4b</c>
    /// compare ensuite a <c>0x3c</c>.
    /// </para>
    /// </summary>
    private static void ReplanBatAction_0048A927(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        // ================================================================ de pres
        if (distance < 0x96)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                bool inReach = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                               && distance < CrtRand() % (timid * 2 + 1) + 100;

                if (inReach && roll < 0x1e)
                {
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                }
                else if (*(short*)(e + 0x16) == 1 && e[0x42] != 0x0d)
                {
                    e[0x42] = 0x0d;
                    e[0x43] = (byte)(CrtRand() % 0x28 + 0x28);
                }
                else if (inReach)
                {
                    e[0x42] = 0;
                    e[0x43] = (byte)(CrtRand() % 0x28);
                }
                else
                {
                    e[0x42] = 9;
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x46)
            {
                StrafeBat(e, timid);
            }
            else if (100 - roll < (boldness >> 1) + 10)
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
            }
            else
            {
                e[0x42] = 9;
                e[0x43] = 1;
            }
        }

        // ================================================================ a mi-distance
        if (distance < 0x258 && distance >= 0x96)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                e[0x42] = ((heading + 0x400) & 0xfff) < 0x800 ? (byte)0x0c : (byte)1;
                e[0x43] = 1;
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            }
            else if (roll < 0x50)
            {
                StrafeBat(e, timid);
            }
            else if (100 - roll < (boldness >> 1) + 10)
            {
                e[0x42] = 9;
                e[0x43] = 1;
            }
            else
            {
                e[0x42] = 0;
                e[0x43] = (byte)(CrtRand() % 0x28);
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

            e[0x42] = 0;
            int rest = CrtRand() % 10;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(rest + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x4b)
        {
            StrafeBat(e, timid);
            return;
        }

        // Ghidra: `cmp edx,3Ch ; jge` — jamais vrai ici.
        if (far < 0x3c)
        {
            e[0x42] = 1;
            e[0x43] = 1;
        }
    }

    /// <summary>Ghidra: <c>LAB_0048aa60</c> et ses sœurs — <b>louvoyer</b> : posture 3, une tenue
    /// tiree, un cap entierement tire.</summary>
    private static void StrafeBat(byte* e, int timid)
    {
        e[0x42] = 3;
        *(short*)(e + 0x12) = 0;
        int wait = CrtRand() % 10;
        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
        *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
    }
}
