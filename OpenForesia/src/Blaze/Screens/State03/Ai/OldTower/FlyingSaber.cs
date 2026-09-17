// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE SABRE VOLANT — 0x004B1660, vieille tour (zone 4, sous-zones 5, 6, 8, 10).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004B1660 — LE SABRE VOLANT. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/OldTower/FUN_004B1660.asm, 942 instructions, absente de Blaze.c), table de
    // sauts a douze entrees a 0x004B2294.
    //
    // LE COUSIN DU GARDE GOLEM (Ai/OldTower/GuardGolem.cs), sans sentinelle ni recul : les memes
    // pique (posture 10) et plongeon (posture 11), le meme vol plane au-dessus du plus haut des deux
    // planchers, et la replanification de la meme famille. Ce qui lui appartient :
    //
    //   - LES DEUX REGIMES DU GREMLIN, fixes a la naissance par +0x16 (a 10 et plus, bit 0x20 retire),
    //     avec le masquage des bits de contact 0x2000 et 0x20000000 en regime 0x20 ;
    //   - UN SEUL COUP, porte par le prelude a l'image 5, sonde {0, 0, 0x50, 0x64, 0, 0xA0} ;
    //   - SON PIQUE MONTE DEUX FOIS MOINS VITE (5 unites par image) et fond pendant 33 images d'un
    //     quart d'ecart par image, a une poussee fixe de 0x1E000, en ne visant que les 20 premieres ;
    //   - +0x16 DEVIENT LA PHASE D'ONDULATION DES LA PREMIERE IMAGE : la variante est lue a la
    //     naissance puis ecrasee de 0xC8 par image. Le test « variante 0 » de la fin du pique lit donc
    //     cette phase, et pas la variante — transcrit comme il s'execute ;
    //   - AU CONTACT, IL NE REPLONGE PAS s'il plonge deja : il file vers un cap tire ; et son plongeon
    //     prend l'animation 1 sur les tirages sous 10, 2 sinon.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004b1660</c> — <b>l'intelligence du Sabre volant</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004B1660(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x28] — la sonde.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0; probe[2] = 0x50; probe[3] = 0x64; probe[4] = 0; probe[5] = 0xa0;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 1, 0);   // Ghidra: FUN_004ac860, copie de FUN_004a1020

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

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004b1793 — la distance recalculee a plat.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        if (speed != 0)
        {
            DecideFlyingSaberAction_004B17CD(e, probe, chased, boldness, distance, heading, speed);
            HoverFlyingSaber_004B1CF8(e, chased);
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
            // ================================================================ heurte
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

        ReplanFlyingSaberAction_004B1F66(e, boldness, timid);
    }

    /// <summary>Le virage du Sabre volant : <c>hardiesse * 2 + 0x1E</c> hors d'un cone de
    /// <c>0x100</c>.</summary>
    private static void TurnFlyingSaber(byte* e, int boldness, int heading)
    {
        if (((heading + 0x100) & 0xfff) <= 0x200) return;

        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
            ? boldness * 2 + 0x1e
            : -0x1e - boldness * 2);
    }

    /// <summary>
    /// Ghidra: <c>0x004b17cd..0x004b1cf6</c> — <b>le premier temps</b> : l'animation, le coup porte a
    /// l'image 5, puis la posture. Douze cases, dont sept servies : 0 attendre, 1 se tourner,
    /// 2 approcher, 3 filer vers un cap, 7 marcher droit, 10 le pique, 11 le plongeon.
    /// </summary>
    private static void DecideFlyingSaberAction_004B17CD(
        byte* e, short* probe, byte* chased, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le coup
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 5)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 5)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }
            }

            uint f = *(uint*)e;
            if ((f & 0x2000000) != 0 && e[0x42] != 0x0a)
            {
                *(uint*)e = f & 0xfffffffd;
                e[0x43] = 0;
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
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 0;
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

                TurnFlyingSaber(e, boldness, heading);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
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

                    TurnFlyingSaber(e, boldness, heading);
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
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

                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 10 : le pique
            case 10:
                FlyingSaberStoop_004B1B77(e, chased, boldness, heading);
                return;

            // ---------------------------------------------------------- 11 : le plongeon
            case 11:
            {
                int y = *(int*)(e + 0x20);
                *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y + 0x28000) >> 2) + y;

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

            // ---------------------------------------------------------- 4, 5, 6, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004b1b77..0x004b1cad</c> — <b>le pique</b>, posture 10.
    ///
    /// <para>
    /// <b>Phase 1, la montee</b> : cinq unites par image, en virant. Au-dela de 200 unites au-dessus de
    /// son plancher le geste s'arme (animation 0), <c>+0x14</c> repart de zero et la phase 2 commence
    /// dans la meme image.
    /// </para>
    /// <para>
    /// <b>Phase 2, la descente</b> : poussee fixe <c>0x1E000</c>, la hauteur rejoint d'un quart par
    /// image celle de la proie plus 40 unites, et <c>+0x14</c> compte les images : il vise pendant
    /// les 20 premieres, et a la 33e le geste tombe avec le rebours.
    /// </para>
    /// <para>
    /// <b>Phase 0</b> : l'armement (animation 1, bit <c>0x1000000</c>), et la tenue s'annule si
    /// <c>+0x16</c> vaut zero — mais <c>+0x16</c> est la phase d'ondulation a ce stade, pas la
    /// variante (voir <see cref="HoverFlyingSaber_004B1CF8"/>).
    /// </para>
    /// </summary>
    private static void FlyingSaberStoop_004B1B77(byte* e, byte* chased, int boldness, int heading)
    {
        if (*(short*)(e + 0x12) == 1)
        {
            int y = *(int*)(e + 0x20) + 0x5000;
            *(int*)(e + 0x20) = y;

            if ((y - *(int*)(e + 0x60)) >> 0xc > 0xc8)
            {
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x12) = 2;
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;
            }

            TurnFlyingSaber(e, boldness, heading);
        }

        short phase = *(short*)(e + 0x12);

        if (phase == 2)
        {
            *(int*)(e + 0x5c) = 0x1e000;

            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y + 0x28000) >> 2) + y;

            (*(short*)(e + 0x14))++;
            short count = *(short*)(e + 0x14);

            if (count < 0x14) TurnFlyingSaber(e, boldness, heading);

            if (count >= 0x21)
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffd;
            }
        }

        if (phase != 0) return;

        *(short*)(e + 0x12) = 1;
        *(short*)(e + 0x18) = 1;
        *(short*)(e + 0x1a) = 0;
        *(int*)(e + 0x5c) = 0;
        *(uint*)e |= 0x1000000;

        if (*(short*)(e + 0x16) == 0) e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004b1cf8..0x004b1d77</c> — <b>le vol plane</b>, sauf pendant la montee du pique.
    ///
    /// <para>
    /// <b>La phase est <c>+0x16</c>, le champ de la variante</b> : elle avance de <c>0xC8</c> par image,
    /// repliee sous <c>0x1000</c>. La hauteur rejoint d'un huitieme par image
    /// <c>(sin(phase) + 0x6000) * 30</c> — 180 unites, a 30 pres — au-dessus du plus haut des deux
    /// planchers <c>+0x60</c>, le sien ou celui de la proie.
    /// </para>
    /// </summary>
    private static void HoverFlyingSaber_004B1CF8(byte* e, byte* chased)
    {
        if (e[0x42] == 0x0a && (e[0] & 2) == 0) return;

        *(short*)(e + 0x16) += 0xc8;
        if (*(short*)(e + 0x16) >= 0x1000) *(short*)(e + 0x16) -= 0x1000;

        int y = *(int*)(e + 0x20);
        int wave = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x16)) + 0x6000) * 30;

        int floor = *(int*)(chased + 0x60) > *(int*)(e + 0x60) ? *(int*)(chased + 0x60) : *(int*)(e + 0x60);
        *(int*)(e + 0x20) = ((wave - y + floor) >> 3) + y;
    }

    /// <summary>
    /// Ghidra: <c>0x004b1f66..0x004b2289</c> — <b>le troisieme temps</b> du Sabre volant, la
    /// replanification du Garde golem a quatre endroits pres : de pres le plongeon n'est pas relance
    /// s'il est deja en cours (il file alors vers un cap tire) et son animation depend du tirage ; le
    /// tirage suivant attend <c>hasard % 40</c> images ; au-dela de la moitie, il approche ; a
    /// mi-distance, une tranche de plus mene a l'approche et la suivante au pique.
    /// </summary>
    private static void ReplanFlyingSaberAction_004B1F66(byte* e, int boldness, int timid)
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

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 100)
                {
                    if (roll < 0x1e)
                    {
                        if (e[0x42] == 0x0b)
                        {
                            StrafeSalamander(e, timid);             // Ghidra: LAB_004b2057
                        }
                        else
                        {
                            e[0x42] = 0x0b;
                            e[0x43] = 1;
                            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                            *(int*)(e + 0x5c) = 0;
                            *(short*)(e + 0x18) = (short)(roll < 10 ? 1 : 2);
                            *(short*)(e + 0x1a) = 0;
                        }
                    }
                    else
                    {
                        e[0x42] = 0;
                        e[0x43] = (byte)(CrtRand() % 0x28);
                    }
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
            }
            else
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) < 0x800)
                {
                    StoopGuardGolem(e);
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x32)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            }
            else if (roll >= 0x3c && roll < 0x50)
            {
                StrafeSalamander(e, timid);
            }
            else
            {
                StoopGuardGolem(e);
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
            StrafeSalamander(e, timid);
            return;
        }

        // Ghidra: `cmp edx,3Ch ; jge` — jamais vrai ici, le tirage vaut au moins 0x4b.
        if (far < 0x3c)
        {
            e[0x42] = 1;
            e[0x43] = 1;
        }
    }
}
