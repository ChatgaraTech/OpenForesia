// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DURAHAN ET LE DURAHAN-NOIR — 0x004C4F90, Labyrinthe des morts (zone 5, sous-zones 4 et 9).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004C4F90 — LE DURAHAN. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004C4F90.asm, 709 instructions, absente de Blaze.c), DEUX
    // tables de sauts a quatre entrees, 0x004C5A0C et 0x004C5A1C.
    //
    // RIEN DE LA FAMILLE DU RAMPANT. C'est la charpente du Behemoth (Ai/Forest/Behemoth.cs) :
    // FUN_004db5a0, FUN_00488160, FUN_004dfaf0 et FUN_004e3770 sont APPELEES, pas recopiees, il n'y
    // a ni sauvegarde de placement ni pose au sol, et la posture est lue par DEUX tables — une quand
    // le bit d'attaque est pose, une quand il ne l'est pas. La meme posture designe donc un geste ou
    // un deplacement selon l'etat.
    //
    //   - UN COMBO A TROIS TEMPS, chaine par pile ou face. Le premier coup (posture 0, animation 3)
    //     finit soit en lachant l'attaque, soit sur l'animation 6 en posture 1 ; le second coup finit
    //     soit en lachant, soit sur l'animation 7 en posture 2 — LE BOND. Un tirage pair au moment de
    //     la decision entame directement au second coup ;
    //   - CHAQUE COUP A SA SONDE, relevee dans .data : 0x00552b88 et 0x00552b94 pour les deux
    //     premiers ({0, 0x32, 0x3c, 0x96, 0, 0x64}), 0x00552ba0 pour le bond, plus long
    //     ({0, 0x32, 0x50, 0x96, 0, 0x64}) ;
    //   - LE PREMIER COUP EMPOISONNE une fois sur trois (l'octet +7 passe a 7, puis l'effet de piege) ;
    //   - LE SECOND FAIT NAITRE LE SOUS-TYPE DE SON PROPRE MOT +0x14, une fois sur trois ;
    //   - LE BOND FRAPPE A DEMI-PUISSANCE : le mot +0x30 du bloc de MODELE est divise par deux le
    //     temps de l'appel a FUN_00539b80, puis rendu. Il projette ensuite la victime de trente
    //     unites et fait naitre 0x39 A LA POSITION DE LA VICTIME, en pretant a l'entite les
    //     coordonnees de celle-ci le temps de la naissance ;
    //   - LE CRI (posture 3, animation 5) fait naitre le sous-type 0xc0 + l'octet +9 du bloc que le
    //     repertoire 0x005e97cc designe en +0xcc, avec +0x9a mis a zero le temps de la naissance ;
    //   - SON FLOTTEMENT PROPRE EST DESARME A CHAQUE IMAGE : +0x9a est remis a 0x7fff avant
    //     l'integration, ce qui annule le rappel vertical de FUN_00488160 ;
    //   - SON PAS SONNE : en animation 1, les images 4 et 9 jouent 0x47.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552b88</c>, <c>&amp;DAT_00552b94</c> et <c>&amp;DAT_00552ba0</c> — <b>les
    /// trois sondes du Durahan</b>, relevees dans <c>.data</c>. Le bond porte plus loin : son
    /// troisieme mot vaut <c>0x50</c> au lieu de <c>0x3c</c>.
    /// </summary>
    private static readonly short[] s_DurahanProbe_00552B88 = [0, 0x32, 0x3c, 0x96, 0, 0x64];
    private static readonly short[] s_DurahanProbe_00552B94 = [0, 0x32, 0x3c, 0x96, 0, 0x64];
    private static readonly short[] s_DurahanProbe_00552BA0 = [0, 0x32, 0x50, 0x96, 0, 0x64];

    /// <summary>
    /// Ghidra: <c>0x004c4f90</c> — <b>l'intelligence du Durahan</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004C4F90(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        if (speed == 0) return;

        DecideDurahanAction_004C4FE1(e, model, boldness, distance, heading, traits);

        ApplyDurahanMotion_004C55E1(e, stats, speed, boldness);
    }

    /// <summary>
    /// Ghidra: <c>0x004c4fe1..0x004c55e0</c> — <b>le premier temps</b> du Durahan, et sa
    /// particularite : <b>DEUX tables de postures</b>. Celle de <c>0x004c5a0c</c> sert quand le bit
    /// d'attaque est pose (les quatre gestes) ; celle de <c>0x004c5a1c</c> sert sinon (attendre, se
    /// tourner, approcher, louvoyer).
    /// </summary>
    private static void DecideDurahanAction_004C4FE1(
        byte* e, byte* model, int boldness, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // Ghidra: FUN_004db5a0, appelee

        if ((e[0] & 2) != 0)
        {
            // Ghidra: 0x004c4ff4 — le trait 0x10 coupe le geste, comme FUN_004d5de0 le ferait.
            if ((traits & 0x10) != 0)
            {
                *(uint*)e &= 0xfffffffd;
                e[0x43] = 0;
                return;
            }

            switch (e[0x42])
            {
                case 0: SwingDurahan_004C5023(e, heading); return;
                case 1: SwingDurahanAgain_004C5175(e, heading); return;
                case 2: LeapDurahan_004C52CB(e, heading, model); return;
                case 3: CryDurahan_004C54D7(e); return;
                default: return;
            }
        }

        // Ghidra: LAB_004c556e — en l'air, fige ou en incantation, rien ne se decide.
        if ((e[0] & 0x11) != 0) return;
        if ((*(uint*)e & 0x804) != 0) return;

        switch (e[0x42])
        {
            case 0: WaitDurahan_004C558A(e, distance); break;
            case 1: TurnDurahan_004C5882(e, heading); break;
            case 2: ApproachDurahan_004C58EE(e, heading, distance); break;
            case 3: StrafeDurahan_004C5965(e, heading); break;
        }

        StepSoundDurahan_004C55B3(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004c55e1..0x004c563a</c> — <b>le deuxieme et le troisieme temps</b> du Durahan,
    /// communs aux deux tables. <c>+0x9a</c> est remis a <c>0x7fff</c> juste avant l'integration :
    /// le rappel vertical de <see cref="IntegrateSceneEntityMotion_00488160"/> est donc neutralise.
    /// </summary>
    private static void ApplyDurahanMotion_004C55E1(byte* e, byte* stats, int speed, int boldness)
    {
        *(ushort*)(e + 0x9a) = 0x7fff;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanDurahanAction_004C563B(e, boldness);
    }

    /// <summary>
    /// Ghidra : le virage du Durahan, branchless dans le binaire (<c>setge</c>, <c>dec</c>,
    /// <c>and</c>) : <b>sous deux fois le pas, le cap restant est ajoute d'un coup</b> ; au-dela on
    /// tourne du pas, dans le sens le plus court.
    /// </summary>
    private static void TurnDurahanToward(byte* e, int heading, int step)
    {
        int add = ((heading + step) & 0xfff) <= step * 2
            ? heading
            : ((heading & 0xfff) < 0x800 ? step : -step);

        *(short*)(e + 0x32) += (short)add;
    }

    /// <summary>Ghidra : les ecritures <c>mov dword [esi+18h],n</c> portent sur QUATRE octets — elles
    /// posent l'animation ET remettent l'image a zero.</summary>
    private static void StartDurahanAnimation(byte* e, short animation)
    {
        *(short*)(e + 0x18) = animation;
        *(short*)(e + 0x1a) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c5023..0x004c5174</c> — <b>le premier coup</b> (posture 0, animation 3) :
    /// il se guide jusqu'a l'image 6, se porte en avant a l'image 12, et frappe a partir de la 11.
    /// Sa fin tire a pile ou face entre lacher et enchainer sur le second coup.
    /// </summary>
    private static void SwingDurahan_004C5023(byte* e, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame <= 6) TurnDurahanToward(e, heading, 0x40);

        if (frame == 0x0c)
        {
            LungeDurahan(e, 0x18000);
            Aud.PlayPositionalSound(0x24, 0x302, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
        }

        if (*(short*)(e + 0x1a) >= 0x0b)
        {
            fixed (short* probe = s_DurahanProbe_00552B88)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);

                    // Ghidra: 0x004c50fd — un coup sur trois empoisonne.
                    if (CrtRand() % 100 < 0x1e)
                    {
                        e[7] = 7;
                        ApplyTrapEffect_0053C420((uint*)e, (uint*)prey, 0);
                    }

                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                }
            }
        }

        if ((*(uint*)e & 0x2000000) == 0)
        {
            *(int*)(e + 0x5c) = 0;                                  // Ghidra: LAB_004c52bf
            return;
        }

        // Ghidra: 0x004c5143 — pile on lache, face on enchaine.
        if ((CrtRand() & 1) != 0)
        {
            *(int*)(e + 0x5c) = 0;
            *(uint*)e &= 0xfffffffd;
            return;
        }

        StartDurahanAnimation(e, 6);
        e[0x42] = 1;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c5175..0x004c52ca</c> — <b>le second coup</b> (posture 1, animation 6) : le
    /// meme temps que le premier, mais l'image 12 fait naitre le sous-type du mot <c>+0x14</c> une
    /// fois sur trois, et sa fin enchaine sur LE BOND.
    /// </summary>
    private static void SwingDurahanAgain_004C5175(byte* e, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame <= 6) TurnDurahanToward(e, heading, 0x40);

        if (frame == 0x0c)
        {
            LungeDurahan(e, 0x18000);

            // Ghidra: 0x004c51f9 — une naissance sur trois, et le sous-type vient de +0x14.
            if (CrtRand() % 100 < 0x1e)
            {
                SpawnEffectEntity(e, (byte)*(short*)(e + 0x14), 0xff, -1);   // Ghidra: FUN_0047a9d0, `push -1` en 3e argument
            }

            Aud.PlayPositionalSound(0x24, 0x302, 0x1800, (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x1a) >= 0x0b)
        {
            fixed (short* probe = s_DurahanProbe_00552B94)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                }
            }
        }

        if ((*(uint*)e & 0x2000000) == 0)
        {
            *(int*)(e + 0x5c) = 0;                                  // Ghidra: LAB_004c52bf
            return;
        }

        if ((CrtRand() & 1) != 0)
        {
            *(int*)(e + 0x5c) = 0;
            *(uint*)e &= 0xfffffffd;
            return;
        }

        StartDurahanAnimation(e, 7);
        e[0x42] = 2;
        *(short*)(e + 0x10) = 2;
        *(int*)(e + 0x5c) = 0;                                      // Ghidra: LAB_004c52bf
    }

    /// <summary>Ghidra : la poussee d'un coup — <c>+0x5c</c> pose, porte sur <c>+0x50</c> et
    /// <c>+0x58</c> le long du cap, puis remis a zero.</summary>
    private static void LungeDurahan(byte* e, int thrust)
    {
        *(int*)(e + 0x5c) = thrust;

        short course = *(short*)(e + 0x32);
        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * thrust) >> 0xc;
        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * thrust) >> 0xc;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c52cb..0x004c54d6</c> — <b>le bond</b> (posture 2, animation 7), en trois
    /// phases comptees a rebours dans <c>+0x10</c> : 2 la retombee de poussee, 1 le saut, 0 le choc.
    /// </summary>
    private static void LeapDurahan_004C52CB(byte* e, int heading, byte* model)
    {
        short phase = *(short*)(e + 0x10);

        // ---------------------------------------------------------------- 2 : l'elan retombe
        if (phase == 2)
        {
            int thrust = *(int*)(e + 0x5c);
            thrust -= thrust >> 2;
            *(int*)(e + 0x5c) = thrust;

            if (*(short*)(e + 0x1a) == 9) *(short*)(e + 0x10) = (short)(phase - 1);

            TurnDurahanToward(e, heading, 0x40);
            return;
        }

        // ---------------------------------------------------------------- 1 : le saut
        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0x1e000;

            short course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * 0x1e000) >> 0xc;
            *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * 0x1e000) >> 0xc;
            *(int*)(e + 0x54) = 0x32000;

            *(uint*)e |= 1;
            (*(short*)(e + 0x10))--;
            *(int*)(e + 0x5c) = 0;

            Aud.PlayPositionalSound(0x2e, 0xdfa, 0x1800, (int*)(e + 0x1c));   // Ghidra: LAB_004c55d9
            return;
        }

        if (phase != 0) return;

        // ---------------------------------------------------------------- 0 : le choc
        uint flags = *(uint*)e;

        if ((flags & 0x2000000) != 0 && (flags & 1) == 0)
        {
            *(uint*)e = flags & 0xffffffdd;                         // `and al,0DDh` : les bits 1 et 5
        }

        if ((e[0] & 0x20) != 0) return;
        if (*(int*)(e + 0x54) <= 0) return;

        byte* prey;
        fixed (short* probe = s_DurahanProbe_00552BA0)
        {
            prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
        }

        // Ghidra: 0x004c53df — le bit 0x20 marque le choc consomme ; il retombe a la prochaine fin
        // de geste.
        if (prey != null) *(uint*)e |= 0x20;

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(prey + 0x96) = 0x10;
        *(uint*)prey |= 0x40;

        // Ghidra: 0x004c540a — LE CHOC PORTE A DEMI-PUISSANCE : le mot +0x30 du bloc de modele est
        // divise par deux le temps de l'appel, puis rendu tel quel.
        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

        if ((*(uint*)prey & 0x40000000) != 0) return;

        // ---------------------------------------------------------------- la projection
        short bearing = *(short*)(e + 0x32);
        *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(bearing) * 30;
        *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(bearing) * 30;

        if ((*(uint*)prey & 1) == 0)
        {
            *(uint*)prey |= 1;
            *(int*)(prey + 0x54) = 0x18000;
        }

        // Ghidra: 0x004c5488 — 0x39 nait A LA POSITION DE LA VICTIME : l'entite lui prete ses
        // coordonnees le temps de l'appel.
        int keepX = *(int*)(e + 0x1c);
        int keepY = *(int*)(e + 0x20);
        int keepZ = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) = *(int*)(prey + 0x1c);
        *(int*)(e + 0x20) = *(int*)(prey + 0x20);
        *(int*)(e + 0x24) = *(int*)(prey + 0x24);

        SpawnEffectEntity(e, 0x39, 0, -1);                          // Ghidra: FUN_0047a9d0

        *(int*)(e + 0x1c) = keepX;
        *(int*)(e + 0x20) = keepY;
        *(int*)(e + 0x24) = keepZ;
    }

    /// <summary>
    /// Ghidra: <c>0x004c54d7..0x004c556d</c> — <b>le cri</b> (posture 3, animation 5), en deux
    /// phases : 1 l'appel, 0 les dix images d'attente.
    /// </summary>
    private static void CryDurahan_004C54D7(byte* e)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 1)
        {
            if (*(short*)(e + 0x1a) == 0x0a)
            {
                Aud.PlayPositionalSound(0x15, 0x32a, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
            }

            if ((*(uint*)e & 0x2000000) != 0)
            {
                // Ghidra: 0x004c550f — +0x9a est mis a zero le temps de la naissance, et le
                // sous-type vient du repertoire 0x005e97cc.
                short bob = *(short*)(e + 0x9a);
                *(short*)(e + 0x9a) = 0;

                byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
                SpawnEffectEntity(e, (byte)((*(byte**)block)[9] + 0xc0), 0xff, -1);   // Ghidra: FUN_0047a9d0

                (*(short*)(e + 0x10))--;
                *(short*)(e + 0x9a) = bob;
                e[0x43] = 0x0a;
                *(int*)(e + 0x5c) = 0;
            }

            return;
        }

        if (phase == 0)
        {
            // Ghidra: LAB_004c555b — l'attente se decompte SANS test de zero : elle deborde.
            e[0x43]--;
            if (e[0x43] == 0) *(uint*)e &= 0xfffffffd;
        }

        *(int*)(e + 0x5c) = 0;                                      // Ghidra: LAB_004c5569
    }

    /// <summary>
    /// Ghidra: <c>0x004c558a..0x004c55ad</c> — <b>l'attente</b> (posture 0 hors attaque) : animation
    /// 0, et l'attente se rouvre des que la proie passe sous <c>0x12c</c>.
    /// </summary>
    private static void WaitDurahan_004C558A(byte* e, int distance)
    {
        if (*(short*)(e + 0x18) != 0)
        {
            *(uint*)e |= 0x1000000;
            StartDurahanAnimation(e, 0);
        }

        if (distance < 0x12c)
        {
            e[0x43] = 0;
            return;
        }

        if (e[0x43] != 0) e[0x43]--;                                // Ghidra: LAB_004c586e
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c5882..0x004c58ed</c> — <b>se tourner</b> : animation 1, pas de <c>0x80</c>,
    /// et l'attente se rouvre des que le cap est pris.
    /// </summary>
    private static void TurnDurahan_004C5882(byte* e, int heading)
    {
        if (*(short*)(e + 0x18) != 1)
        {
            *(uint*)e |= 0x1000000;
            StartDurahanAnimation(e, 1);
        }

        if (((heading + 0x80) & 0xfff) <= 0x100)
        {
            *(short*)(e + 0x32) += (short)heading;
            e[0x43] = 0;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        *(int*)(e + 0x5c) = 0;
        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? 0x80 : -0x80);
    }

    /// <summary>
    /// Ghidra: <c>0x004c58ee..0x004c5964</c> — <b>approcher</b> : animation 1, poussee
    /// <c>0x12000</c>.
    /// </summary>
    private static void ApproachDurahan_004C58EE(byte* e, int heading, int distance)
    {
        if (*(short*)(e + 0x18) != 1)
        {
            *(uint*)e |= 0x1000000;
            StartDurahanAnimation(e, 1);
        }

        if (distance < *(short*)(e + 0x10))
        {
            e[0x43] = 0;
            return;
        }

        if (e[0x43] != 0) e[0x43]--;

        TurnDurahanToward(e, heading, 0x80);
        *(int*)(e + 0x5c) = 0x12000;
    }

    /// <summary>
    /// Ghidra: <c>0x004c5965..0x004c5a0a</c> — <b>louvoyer</b> : animation 1, et la poussee
    /// <c>0xe000</c> porte directement sur X et Z le long du cap tire de <c>+0x10</c>.
    /// </summary>
    private static void StrafeDurahan_004C5965(byte* e, int heading)
    {
        if (e[0x43] != 0) e[0x43]--;

        if (*(short*)(e + 0x18) != 1)
        {
            *(uint*)e |= 0x1000000;
            StartDurahanAnimation(e, 1);
        }

        *(int*)(e + 0x5c) = 0xe000;

        short course = *(short*)(e + 0x10);
        *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * 0xe000) >> 0xc;
        *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * 0xe000) >> 0xc;

        *(int*)(e + 0x5c) = 0;

        TurnDurahanToward(e, heading, 0x80);
    }

    /// <summary>
    /// Ghidra: <c>0x004c55b3..0x004c55e0</c> — <b>le pas</b> : en animation 1, les images 4 et 9
    /// jouent le son <c>0x47</c>.
    /// </summary>
    private static void StepSoundDurahan_004C55B3(byte* e)
    {
        if (*(short*)(e + 0x18) != 1) return;

        short frame = *(short*)(e + 0x1a);
        if (frame != 4 && frame != 9) return;

        Aud.PlayPositionalSound(0x47, 0x400, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x004c563b..0x004c5865</c> — <b>le troisieme temps</b> du Durahan, DEUX plages
    /// seulement, separees a <c>0x12c</c>.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : sous 50, s'il fait face, il entame le combo — au premier coup sur un
    /// tirage impair, au SECOND sur un tirage pair ; sinon il se tourne. Jusqu'a 74 il approche ;
    /// jusqu'a 89 il entame le combo s'il fait face ; au-dela il louvoie vers le relevement plus
    /// <c>0x200</c>.</item>
    /// <item><b>De loin</b> : sous 20 il CRIE ; jusqu'a 39 il louvoie vers le relevement moins
    /// <c>0x600</c> ; jusqu'a 89 il approche ; au-dela il attend.</item>
    /// </list>
    /// </summary>
    private static void ReplanDurahanAction_004C563B(byte* e, int boldness)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;

        // ================================================================ de pres
        if (distance < 0x12c)
        {
            if (roll < 0x32)
            {
                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004c56e0
                    e[0x43] = 1;
                    return;
                }

                OpenDurahanCombo(e, roll);
                return;
            }

            if (roll < 0x4b)
            {
                e[0x42] = 2;
                *(short*)(e + 0x10) = 0x64;
                e[0x43] = (byte)(CrtRand() % 10 + 0x14);
                return;
            }

            if (roll < 0x5a && ((heading + 0x100) & 0xfff) <= 0x200)
            {
                OpenDurahanCombo(e, roll);
                return;
            }

            // Ghidra: LAB_004c5777 — le louvoiement contourne PAR LA DROITE : +0x200.
            e[0x42] = 3;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            *(short*)(e + 0x10) = (short)(CrtRand() % 0xc00 + heading + *(short*)(e + 0x32) + 0x200);
            return;
        }

        // ================================================================ de loin
        if (roll < 0x14)
        {
            // Ghidra: LAB_004c57b7 — le cri.
            StartDurahanAnimation(e, 5);
            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
            e[0x42] = 3;
            *(short*)(e + 0x10) = 1;
            return;
        }

        if (roll < 0x28)
        {
            // Ghidra: LAB_004c57e2 — et par la gauche : -0x600.
            e[0x42] = 3;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            *(short*)(e + 0x10) = (short)(CrtRand() % 0xc00 + heading + *(short*)(e + 0x32) - 0x600);
            return;
        }

        if (roll < 0x5a)
        {
            e[0x42] = 2;                                            // Ghidra: LAB_004c5827
            *(short*)(e + 0x10) = 0x64;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        e[0x42] = 0;                                                // Ghidra: LAB_004c584f
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);
    }

    /// <summary>
    /// Ghidra: <c>0x004c56c0</c> et <c>LAB_004c5757</c> — <b>l'entree dans le combo</b> : un tirage
    /// impair l'entame au premier coup (animation 3, posture 0), un tirage pair directement au
    /// SECOND (animation 6, posture 1).
    /// </summary>
    private static void OpenDurahanCombo(byte* e, int roll)
    {
        if ((roll & 1) != 0)
        {
            StartDurahanAnimation(e, 3);
            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
            e[0x42] = 0;
            return;
        }

        StartDurahanAnimation(e, 6);
        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
        e[0x42] = 1;
    }
}
