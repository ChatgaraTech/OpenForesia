// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA GORGONE — 0x004F6E60, grotte scellee (zone 10, sous-zones 4 et 9).
//
// Le nom vient de Comportement.md. 2 848 octets, absents de Blaze.c, transcrits au desassembleur
// le 2026-09-19 (lot 3 de la grotte) : Legacy/Raw/Blaze/SealedCave/FUN_004F6E60.asm. Table de
// sauts a treize entrees a 0x004f7944 (les postures).
//
// LA CHARPENTE DU VARAN (Ai/Forest/Varan.cs), troisieme membre apres la Fleur du mal : pose et
// sauvegarde du placement, distance refaite a trois dimensions, la case de posture qui s'execute
// geste ou non, l'integration de FUN_00488160 recopiee en ligne (au mot pres), le heurt des murs
// de SettleSceneEntityOnGroundAndWalls au masque 0x2000ad82 — A UNE IMMEDIATE PRES : sa marche de
// contournement tient 0xc8 et non 0x50. D'ou le parametre `wallWalkRange` ajoute ce jour-la.
//
// Son chercheur de proie 0x004F6380 est FUN_004a1020 instruction pour instruction (meme table de
// sauts a neuf entrees, aux memes ecarts) ; elle l'appelle en MODE 1 a sa naissance.
//
// Ce qui est a elle :
//
//     posture 8    LA CHARGE : elle vise (pas de 0x64 + hardiesse), puis s'elance (animation 4,
//                  bit de geste) en SEMANT le sous-type 0xee a chaque image, et une fois sur
//                  trois un second ; elle freine d'un huitieme par image. Tant que le geste
//                  tient, elle balaie la sonde du Varan : un coup, une projection a la mesure de
//                  sa vitesse, l'effet 0x39 nait 180 unites devant elle — et le geste tombe.
//     posture 9    le louvoiement : un virage fixe tire a la naissance, (hasard % 10 - 5) * 10
//     posture 11   le coup simple, animation 3
//     posture 12   LE REGARD : l'entite 0xdc nait (+0x9a mis a 1 le temps de la naissance), puis
//                  dix images a la tenir en face. La replanification ne la choisit jamais deux fois
//                  de suite, et une bete immobilisee qui la tenait retombe a l'attente.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x004f6e60</c> — <b>l'intelligence de la Gorgone</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004F6E60(byte* record)
    {
        byte* e = record;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 1, 0);                // Ghidra: FUN_004f6380, copie de FUN_004a1020
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);                // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004f6f75 — la distance refaite a trois dimensions, de l'entite vers la cible.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideGorgonAction_004F6FC3(e, boldness, distance, heading);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_004f75f7 — FUN_00488160 en ligne.
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep, wallWalkRange: 0xc8);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;

            // Immobilisee pendant son regard : elle retombe a l'attente, pour une image.
            if (e[0x42] == 0x0c)
            {
                e[0x42] = 0;
                e[0x43] = 1;
            }

            return;
        }

        if (e[0x43] != 0) return;

        ReplanGorgonAction_004F7851(e, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004f6fc3..0x004f75f6</c> — <b>le premier temps</b>. Le bit <c>0x10</c> du mot
    /// d'etat suit la posture 5 (qu'aucune case ne mene) ; puis le coup de la charge ; puis la
    /// case de la posture, geste ou non.
    /// </summary>
    private static void DecideGorgonAction_004F6FC3(byte* e, int boldness, int distance, int heading)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                   // en ligne dans le corps original

        byte stance = e[0x42];
        if (stance == 5) *(uint*)e |= 0x10;
        else *(uint*)e &= 0xffffffef;

        if ((*(uint*)e & 2) != 0 && stance == 8) GorgonChargeStrike_004F704D(e);

        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
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
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = 0x5000;
                    e[0x43] = (byte)(CrtRand() % 0x1e + 0x14);
                }

                e[0x43]--;
                GorgonTurn(e, heading, 0xc8, boldness + 0x32);
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
                GorgonTurn(e, heading, 0xc8, boldness + 0x32);
                return;

            // ---------------------------------------------------------- 3, 6 : rendre la main
            case 3:
            case 6:
                e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 7 : contourner
            case 7:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 1)
                {
                    if (distance < *(short*)(e + 0x10)) e[0x43] = 0;
                    else if (e[0x43] != 0) e[0x43]--;
                }

                if (phase != 0) return;

                *(ushort*)(e + 0x1a) = 0;
                *(ushort*)(e + 0x18) = 1;
                *(ushort*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                *(uint*)e = (*(uint*)e & 0xfffffffd) | 0x1000000;
                return;
            }

            // ---------------------------------------------------------- 8 : la charge
            case 8:
                GorgonCharge_004F7329(e, boldness, heading);
                return;

            // ---------------------------------------------------------- 9 : louvoyer
            case 9:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x10) = (short)((CrtRand() % 10 - 5) * 10);
                    int r = CrtRand();
                    *(ushort*)(e + 0x12) = 1;
                    e[0x43] = (byte)(r % 10 + 0x14);
                    *(int*)(e + 0x5c) = ((boldness & ~3) + 0x14) << 0xa;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                *(short*)(e + 0x32) += *(short*)(e + 0x10);
                e[0x43]--;
                return;

            // ---------------------------------------------------------- 0x0b : le coup simple
            case 0x0b:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x12) = 1;
                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                uint g = *(uint*)e;
                if ((g & 0x2000000) == 0) return;

                e[0x43] = 0;
                *(uint*)e = g & 0xfffffffd;
                return;

            // ---------------------------------------------------------- 0x0c : le regard
            case 0x0c:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    short savedLift = *(short*)(e + 0x9a);
                    *(short*)(e + 0x9a) = 1;

                    SpawnEffectEntity(e, 0xdc, 0, -1);               // Ghidra: FUN_0047a9d0

                    *(uint*)e = (*(uint*)e & 0xfffffffd) | 0x1000000;
                    *(short*)(e + 0x9a) = savedLift;
                    *(ushort*)(e + 0x18) = 2;
                    *(ushort*)(e + 0x1a) = 0;
                    *(ushort*)(e + 0x12) = 1;
                    e[0x43] = 0x0a;
                    *(int*)(e + 0x5c) = 0;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                e[0x43]--;
                GorgonTurn(e, heading, 0x100, boldness + 0x32);
                return;

            // ---------------------------------------------------------- 4, 5, 10 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Le virage de la Gorgone : hors d'un cone de <c>2 * half</c> centre sur la cible, un pas de
    /// <paramref name="step"/> du cote ou elle se trouve. Six sites du corps, deux largeurs de cone
    /// (<c>0xc8</c> / <c>0x100</c>) et deux pas (<c>hardiesse + 0x32</c> / <c>+ 0x64</c>).
    /// </summary>
    private static void GorgonTurn(byte* e, int heading, int half, int step)
    {
        if (((heading + half) & 0xfff) <= half * 2) return;

        *(ushort*)(e + 0x32) += (ushort)((heading & 0xfff) < 0x800 ? step : -step);
    }

    /// <summary>
    /// Ghidra: <c>0x004f7329</c> — <b>la charge</b>, posture 8, en trois phases de <c>+0x12</c>.
    ///
    /// <para>
    /// <b>1, viser</b> : elle se tourne, et le rebours pose par la replanification descend ; a zero,
    /// l'elan — <c>((hardiesse &amp; ~3) + 0x78) &lt;&lt; 10</c>, l'animation 4, le bit de geste et
    /// 30 a 39 images de course. <b>2, courir</b> : elle seme le sous-type <c>0xee</c> par
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/>, un par image et un second une fois sur
    /// trois ; au-dessus de 10 images restantes elle corrige encore son cap ; a une image, phase 3.
    /// <b>3, freiner</b> : un huitieme de l'elan par image, et sous <c>0x5000</c> elle rend la main.
    /// </para>
    /// </summary>
    private static void GorgonCharge_004F7329(byte* e, int boldness, int heading)
    {
        if (*(ushort*)(e + 0x12) == 0)
        {
            *(uint*)e |= 0x1000000;
            *(ushort*)(e + 0x18) = 0;
            *(ushort*)(e + 0x1a) = 0;
            *(ushort*)(e + 0x12) = 1;
            *(int*)(e + 0x5c) = 0;
        }

        // ---------------------------------------------------------------- 1 : viser
        if (*(ushort*)(e + 0x12) == 1)
        {
            GorgonTurn(e, heading, 0x100, boldness + 0x64);

            if (e[0x43] != 0)
            {
                e[0x43]--;
                if (e[0x43] == 0)
                {
                    *(int*)(e + 0x5c) = ((boldness & ~3) + 0x78) << 0xa;
                    *(ushort*)(e + 0x12) = 2;
                    *(ushort*)(e + 0x18) = 4;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;

                    int r = CrtRand();
                    e[0x43] = (byte)(r % 10 + 0x1e);
                    *(uint*)e |= 2;
                }
            }
        }

        // ---------------------------------------------------------------- 2 : courir
        if (*(ushort*)(e + 0x12) == 2)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xee, 0, -1);   // Ghidra: FUN_0047ac40

            if (CrtRand() % 3 == 1)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xee, 0, -1);
            }

            byte left = e[0x43];
            if (left > 0x0a) GorgonTurn(e, heading, 0x100, boldness + 0x32);

            e[0x43] = (byte)(left - 1);
            if (e[0x43] == 1) *(ushort*)(e + 0x12) = 3;
        }

        // ---------------------------------------------------------------- 3 : freiner
        if (*(ushort*)(e + 0x12) != 3) return;

        int run = *(int*)(e + 0x5c);
        run += -run >> 3;
        *(int*)(e + 0x5c) = run;
        if (run < 0x5000) e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004f704d..0x004f7152</c> — <b>le coup de la charge</b>, tant que le bit de geste
    /// tient en posture 8. La sonde est celle du Varan, <c>{0, 0x1e, 0x32, 0x64, 0, 0x64}</c>,
    /// batie sur la pile.
    ///
    /// <para>
    /// A la touche : degats pleins, le son 3, puis — sauf si la victime est protegee
    /// (<c>0x40000000</c>) — l'effet <c>0x39</c> nait <b>180 unites devant la Gorgone</b> (elle
    /// se prete cette position le temps de la naissance) et la victime est projetee en l'air a la
    /// mesure de l'elan (<c>+0x5c &gt;&gt; 11</c> fois le cap). Dans tous les cas le geste tombe :
    /// une charge ne touche qu'une fois.
    /// </para>
    /// </summary>
    private static void GorgonChargeStrike_004F704D(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x64;

        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(uint*)prey |= 0x40;
        *(ushort*)(prey + 0x96) = 0x10;
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

        if ((*(uint*)prey & 0x40000000) == 0)
        {
            int savedX = *(int*)(e + 0x1c);
            int savedY = *(int*)(e + 0x20);
            int savedZ = *(int*)(e + 0x24);

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 180;
            *(int*)(e + 0x24) += (int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * 180;

            SpawnEffectEntity(e, 0x39, 0, -1);                       // Ghidra: FUN_0047a9d0

            *(int*)(e + 0x1c) = savedX;
            *(int*)(e + 0x20) = savedY;
            *(int*)(e + 0x24) = savedZ;

            *(uint*)prey |= 1;
            *(int*)(prey + 0x50) =
                (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * (*(int*)(e + 0x5c) >> 0xb);
            *(int*)(prey + 0x54) = 0x18000;
            *(int*)(prey + 0x58) =
                (int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * (*(int*)(e + 0x5c) >> 0xb);
        }

        *(uint*)e &= 0xfffffffd;                                     // Ghidra: LAB_004f7149
    }

    /// <summary>
    /// Ghidra: <c>0x004f7851..0x004f7943</c> — <b>le troisieme temps</b>. Distance et cap en deux
    /// dimensions ; <c>+0x12</c> remis a zero.
    ///
    /// <para>
    /// Un seul tirage sur cent, lu par <b>trois tests qui se suivent</b> et non par une cascade :
    /// sous 50 la charge est armee (rebours <c>hasard % 10 + timidite / 4</c>) ; de 50 a 79 elle se
    /// tourne, et c'est fini. Sinon — y compris juste apres avoir arme la charge — sous
    /// <c>0x226</c> de distance et hors d'un regard, le regard l'emporte ; a defaut, la charge est
    /// (re)armee, avec un second tirage de rebours.
    /// </para>
    /// </summary>
    private static void ReplanGorgonAction_004F7851(byte* e, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        // Le cap est recalcule et range, puis jamais relu par ce temps.
        _ = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        *(ushort*)(e + 0x12) = 0;

        int roll = CrtRand() % 100;

        if (roll < 0x32)
        {
            e[0x42] = 8;
            e[0x43] = (byte)(CrtRand() % 10 + (byte)(timid >> 2));
            *(ushort*)(e + 0x12) = 0;
        }

        if (roll >= 0x32 && roll < 0x50)
        {
            e[0x42] = 1;
            e[0x43] = 1;
            return;
        }

        if (distance < 0x226 && e[0x42] != 0x0c)
        {
            e[0x42] = 0x0c;
            e[0x43] = 1;
            return;
        }

        e[0x42] = 8;
        int r = CrtRand();
        *(ushort*)(e + 0x12) = 0;
        e[0x43] = (byte)(r % 10 + (byte)(timid >> 2));
    }
}
