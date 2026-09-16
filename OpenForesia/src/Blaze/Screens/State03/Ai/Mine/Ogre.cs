// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'OGRE — 0x0048F230, sous-type 14 de la mine abandonnee (sous-zones 7 a 9).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, colonne ia = 14), dans les deux
// blocs de la mine qui le portent. QUATRE CORPS D'UN COUP :
//
//   0x0048F230  mine, sous-zones 5 a 8      le releve, Legacy/Raw/Blaze/FUN_0048F230.asm
//   0x004F9C70  grotte scellee              identique instruction pour instruction
//   0x00494D10  mine, sous-zones 3, 4 et 9  22 lignes d'ecart : l'integration charge +0x9a et
//                                           +0x54 dans un autre ordre, la meme formule en sort
//   0x004F23D0  grotte scellee              identique a 0x00494D10
//
// 1 147 instructions, absentes de Blaze.c ; table de sauts a quatorze entrees a 0x00490174.
//
// LA CHARPENTE DU POURPRE PREUX (Ai/ImmortalCastle/PurpleKnight.cs), SANS LA PARADE : meme
// integration (FUN_00488160), meme pose au sol et meme heurt au masque 0x2000ad82
// (SettleSceneEntityOnGroundAndWalls), meme prelude de geste — le coup a l'image 8, le combo a trois
// quarts, la charge a moitie. Ce qui lui appartient :
//
//   - SA SECONDE SONDE PART SOUS LUI ET PORTE LOIN : {0, -100, 0xc8, 0x64, 0, 0xc8}.
//   - SON COMBO frappe a l'image 8 de l'animation 3 puis a l'image 12 de l'animation 6.
//   - SA CHARGE (posture 0x0d) EST UN BOND : a l'image 10 de l'animation 5 il quitte le sol, et la
//     victime touchee est REPOUSSEE LOIN DE LUI d'un huitieme de l'ecart, pas projetee dans son
//     cap.
//   - SES VIRAGES sont a 0x3c ; son effet (posture 10) n'est que 0x36, variante 1, image 10.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x0048f230</c> (et ses trois copies) — <b>l'intelligence de l'Ogre</b>. Voir
    /// l'en-tete du fichier.
    /// </summary>
    private static void TickSceneEntityAi_0048F230(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x34] et [esp+0x40] — les deux sondes, dont trois mots sont poses avant le
        // quatrieme push.
        short* probes = stackalloc short[12];
        probes[0] = 0; probes[1] = 0x1e;  probes[2] = 0x32; probes[3] = 0x64; probes[4] = 0;  probes[5] = 0x64;
        probes[6] = 0; probes[7] = -0x64; probes[8] = 0xc8; probes[9] = 0x64; probes[10] = 0; probes[11] = 0xc8;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_0048c170, copie de FUN_004a1020
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

        // La distance recalculee a trois dimensions, comme chez le Pourpre preux.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        if (speed != 0)
        {
            DecideOgreAction_0048F230(e, probes, model, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);              // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanOgreAction_0048FDB3(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0048f3b5..0x0048fb82</c> — <b>le premier temps</b> de l'Ogre : l'animation, le
    /// prelude de geste, puis la posture. Quatorze cases, dont douze servies : 0 attendre,
    /// 1 se tourner, 2 approcher, 3 louvoyer, 5 (le rebours seul), 6 bondir, 7 marcher droit,
    /// 10 l'effet, 11 le coup, 12 le combo, 13 la charge bondissante.
    /// </summary>
    private static void DecideOgreAction_0048F230(
        byte* e, short* probes, byte* model, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le prelude de geste
        if ((flags & 2) != 0)
        {
            OgreGesture_0048F433(e, probes, model);
        }
        else if ((flags & 1) != 0 && *(short*)(e + 0x1a) < 3)
        {
            // En l'air, dans les trois premieres images, le bond peut se changer en attaque.
            if (CrtRand() % 0x3c < boldness)
            {
                *(short*)(e + 0x18) = 3;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x1a) = 0;
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
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                }

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                }
                else
                {
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x3c : -0x3c - boldness);
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

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x3c : -0x3c - boldness);
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

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 5 : le rebours tombe
            case 5:
                e[0x43] = 0;
                return;

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
                        *(short*)(e + 0x18) = 4;
                        *(short*)(e + 0x1a) = 0;
                        *(int*)(e + 0x5c) = thrust;

                        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 1;
                        *(short*)(e + 0x12) = 1;
                    }
                }

                if ((*(uint*)e & 1) == 0) e[0x43] = 0;
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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 10 : l'effet
            case 10:
            {
                if (*(short*)(e + 0x12) == 1 && *(short*)(e + 0x1a) == 10 && *(short*)(e + 0x16) == 1)
                    SpawnEffectEntity(e, 0x36, 0, -1);              // Ghidra: FUN_0047a9d0

                uint f = *(uint*)e;
                if ((f & 0x2000000) != 0) e[0x43] = 0;

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x18) = 6;
                *(short*)(e + 0x1a) = 0;
                *(uint*)e = f & 0xfeffffff;

                if (boldness < 0x1e) *(int*)(e + 0x5c) = 0;
                if (*(short*)(e + 0x16) == 0) e[0x43] = 0;
                return;
            }

            // ---------------------------------------------------------- 11 : le coup
            case 11:
            {
                short phase = *(short*)(e + 0x12);

                // C'est le prelude qui frappe ; la posture ne fait que clore, et rend le rebours.
                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                if (phase != 0) return;

                *(short*)(e + 0x18) = 3;
                *(uint*)e &= 0xfcffffff;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;
            }

            // ---------------------------------------------------------- 12 : le combo
            case 12:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 2 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                // Premier coup fini : le second enchaine sur l'animation 6, prise a l'image 7.
                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfeffffff;
                    *(short*)(e + 0x12) = 2;
                    *(short*)(e + 0x18) = 6;
                    *(short*)(e + 0x1a) = 7;
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 13 : la charge bondissante
            case 13:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 2)
                {
                    int v = *(int*)(e + 0x5c);
                    *(int*)(e + 0x5c) = v - (v >> 4);
                    if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;
                }

                if (phase == 1)
                {
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }

                    // A la dixieme image de l'animation, il decolle, geste arme.
                    if (*(short*)(e + 0x1a) >= 10)
                    {
                        *(int*)(e + 0x54) += 0x1e000;
                        *(uint*)e |= 3;
                        *(short*)(e + 0x12) = 2;
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x18) = 5;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 4, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0048f433..0x0048f64c</c> — <b>le prelude de geste</b> de l'Ogre, trois blocs
    /// comme chez <see cref="PurpleKnightGesture_004D2175"/>.
    ///
    /// <list type="number">
    /// <item><b>Hors combo et hors charge</b> : a l'image 8, la sonde A a pleine attaque ; la fin
    /// d'animation ferme le geste, et remet le rebours a zero a terre seulement.</item>
    /// <item><b>Le combo</b> (0x0c) : image 8 de l'animation 3 et image <b>12</b> de l'animation
    /// <b>6</b>, a trois quarts de l'attaque ; le premier coup ne fait pas chanceler.</item>
    /// <item><b>La charge</b> (0x0d) : la sonde B a chaque image, a moitie d'attaque. Une victime
    /// qui n'est pas mourante recoit l'effet 0x39, passe en l'air, monte de <c>0x18000</c> et est
    /// <b>repoussee a l'oppose de l'Ogre</b> d'un huitieme de l'ecart sur X et Z.</item>
    /// </list>
    /// </summary>
    private static void OgreGesture_0048F433(byte* e, short* probes, byte* model)
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

        if (e[0x42] == 0x0c && OgreComboStrikeFrame(e))
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && OgreComboStrikeFrame(e))
            {
                if (*(short*)(e + 0x12) == 1)
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

            *(int*)(victim + 0x50) = -((*(int*)(e + 0x1c) - *(int*)(victim + 0x1c)) >> 3);
            *(int*)(victim + 0x54) = 0x18000;
            *(int*)(victim + 0x58) = -((*(int*)(e + 0x24) - *(int*)(victim + 0x24)) >> 3);
        }

        *(uint*)e &= 0xfffffffd;
    }

    /// <summary>Ghidra: la double condition relue deux fois par le combo de l'Ogre — image 8 de
    /// l'animation 3, ou image 12 de l'animation 6.</summary>
    private static bool OgreComboStrikeFrame(byte* e)
    {
        short frame = *(short*)(e + 0x1a);
        return (frame == 8 && *(short*)(e + 0x18) == 3) || (frame == 12 && *(short*)(e + 0x18) == 6);
    }

    /// <summary>
    /// Ghidra: <c>0x0048fdb3..0x00490170</c> — <b>le troisieme temps</b> de l'Ogre.
    ///
    /// <para>
    /// Trois plages testees l'une apres l'autre (<c>0xfa</c>, <c>0x258</c>). Au contact, dans le cone
    /// ouvert par la timidite et a une portee tiree : le coup trois fois sur cinq, le combo sinon ;
    /// a moins de 100 unites, une chance sur quatre de <b>charger</b>. A mi-distance, face a la
    /// proie, il <b>marche droit</b> sur elle. Au loin, dans le cone, <b>l'effet</b>.
    /// </para>
    /// </summary>
    private static void ReplanOgreAction_0048FDB3(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        // ================================================================ au contact
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
                        *(int*)(e + 0x5c) = 0;
                        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                        *(short*)(e + 0x18) = 3;
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
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            }
            else if (roll < 0x5a && distance < 100)
            {
                e[0x42] = 0x0d;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                StrafeOgre(e, timid);
            }
        }

        // ================================================================ a mi-distance
        if (distance < 0x258 && distance >= 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) < 0x800)
                {
                    e[0x42] = 7;
                    e[0x43] = (byte)(CrtRand() % 0x14 + 0x14);
                    *(short*)(e + 0x12) = 0;
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
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
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
                StrafeOgre(e, timid);
            }
            else
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
        }

        // ================================================================ au loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            if (far < boldness * 2 + 10)
            {
                ApproachOgre(e, timid);
                return;
            }

            e[0x42] = 0;
            int rest = CrtRand() % 10;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(rest + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x28)
        {
            StrafeOgre(e, timid);
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
            if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
                return;
            }

            ApproachOgre(e, timid);
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }

    /// <summary>Ghidra: <c>LAB_00490131</c> et ses sœurs — <b>louvoyer</b> : posture 3, une tenue
    /// tiree, et un cap entierement tire sur douze bits.</summary>
    private static void StrafeOgre(byte* e, int timid)
    {
        e[0x42] = 3;
        *(short*)(e + 0x12) = 0;
        int wait = CrtRand() % 10;
        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
        *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
    }

    /// <summary>Ghidra: <c>LAB_004900e9</c> — <b>approcher de loin</b> : posture 2, arret a 100
    /// unites.</summary>
    private static void ApproachOgre(byte* e, int timid)
    {
        e[0x42] = 2;
        int wait = CrtRand() % 10;
        *(short*)(e + 0x10) = 100;
        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
    }
}
