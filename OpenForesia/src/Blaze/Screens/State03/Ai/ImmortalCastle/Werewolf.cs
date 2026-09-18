// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE LOUP-GAROU — 0x004D7200, chateau de l'immortel (zone 6, sous-zones 5 et 11).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004D7200 — LE LOUP-GAROU. Transcrit le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004D7200.asm, 1 106 instructions, absent de Blaze.c),
    // table de sauts a QUINZE entrees a 0x004D8088. Le Tigre (0x004D80D0) est son frere : meme
    // charpente, voir Ai/ImmortalCastle/Tiger.cs.
    //
    // IL N'A PAS DE HARDIESSE. Le binaire lit la situation comme tout le monde, PUIS ECRASE le
    // creneau de la hardiesse par la constante 0x40 (0x004d731f, `mov dword [esp+10h],40h`) — et
    // c'est cette constante qui sert partout ensuite. Il n'a donc pas non plus de timidite : les
    // attentes de sa replanification sont des `hasard % 10 + 20` nus. Deux consequences mortes en
    // decoulent, transcrites telles quelles :
    //
    //   - DE LOIN, IL NE SE REPOSE JAMAIS : la branche du repos demande `tirage >= 2*0x40 + 10`,
    //     soit 138, sur un tirage borne a 29 ;
    //   - DE PRES, un `hasard % 100` est tire et jete, puis une comparaison qui ne peut pas etre
    //     vraie (la distance, deja inferieure a 100, comparee a 100) garde la branche. Le tirage
    //     mort deplace quand meme le flux du generateur : il est transcrit.
    //
    // TROIS COUPS, ET CHACUN A SA SONDE ET SA FORCE :
    //
    //   - LE COUP ORDINAIRE, a l'image 11, sonde {0, 0x1e, 0x32, 0x64, 0, 0x64} — celle du
    //     Vampire — et pleine attaque. Les postures 12 et 13 le sautent ;
    //   - LA MORSURE (posture 12), a partir de l'image 12, meme sonde, clignotement 6, et
    //     L'ATTAQUE DU MODELE DIVISEE PAR QUATRE le temps du calcul : le mot +0x30 du bloc de
    //     modele est mis de cote, decale, puis rendu ;
    //   - LA PROJECTION (posture 13), SANS GARDE D'IMAGE — elle balaie a chaque image —, avec la
    //     seconde sonde {0, 0, 0x50, 0x64, 0, 0x64}, l'attaque du modele DE MOITIE, et, si la proie
    //     ne porte pas 0x40000000 : l'effet 0x39 nait, la proie passe en l'air et part a la vitesse
    //     du Loup-garou — sinus et cosinus de son cap fois sa poussee decalee de onze, et 0x18000
    //     vers le haut. C'est le seul corps du chateau qui ecrit la vitesse de sa victime.
    //
    // ET IL HURLE (posture 14) : quand le geste s'acheve, le son +0x2c du repertoire de zone part a
    // plein volume et l'effet 0xde nait, puis vingt images de recuperation — decomptees SANS test de
    // zero, donc le compte passe par 0xff s'il est deja nul.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004d7200</c> — <b>l'intelligence du Loup-garou</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004D7200(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] et [esp+0x3c] — DEUX sondes. La premiere est celle du Vampire.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

        short* throwProbe = stackalloc short[6];
        throwProbe[0] = 0; throwProbe[1] = 0; throwProbe[2] = 0x50;
        throwProbe[3] = 0x64; throwProbe[4] = 0; throwProbe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004d5a20, copie de FUN_004a1020
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // Ghidra: 0x004d731f — LA HARDIESSE EST ECRASEE. Voir le bandeau.
        boldness = 0x40;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004d732e — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideWerewolfAction_004D7379(e, model, probe, throwProbe, boldness, distance, heading, speed);
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

        ReplanWerewolfAction_004D7D39(e, boldness, heading);
    }

    /// <summary>
    /// Ghidra: <c>0x004d7379..0x004d7b01</c> — <b>le premier temps</b> du Loup-garou : les trois
    /// coups, puis la posture. Quinze cases, dont dix servies : 0 attendre, 1 se tourner,
    /// 2 approcher, 3 louvoyer, 6 bondir, 7 marcher droit, 11 le coup, 12 la morsure,
    /// 13 la projection, 14 le hurlement.
    /// </summary>
    private static void DecideWerewolfAction_004D7379(
        byte* e, byte* model, short* probe, short* throwProbe,
        int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        if ((e[0] & 2) != 0)
        {
            // ============================================================ le coup ordinaire
            // Ghidra: 0x004d73e6 — les postures 12 et 13 sautent ce bloc ET sa queue.
            if (e[0x42] != 0x0c && e[0x42] != 0x0d)
            {
                if (*(short*)(e + 0x1a) == 0x0b)
                {
                    byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                    if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0b)
                    {
                        *(uint*)prey |= 0x40;
                        *(ushort*)(prey + 0x96) = 0x10;
                        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                    }
                }

                uint f = *(uint*)e;
                if ((f & 0x2000000) != 0)
                {
                    *(uint*)e = f & 0xfffffffd;
                    if ((f & 1) == 0) e[0x43] = 0;
                }
            }

            // ============================================================ la morsure, posture 12
            if (e[0x42] == 0x0c && *(short*)(e + 0x1a) >= 0x0c)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0c)
                {
                    *(ushort*)(prey + 0x96) = 6;
                    *(uint*)prey |= 0x40;

                    // Ghidra: 0x004d74be — le mot +0x30 du BLOC DE MODELE, divise par quatre le
                    // temps du calcul de degat, puis rendu.
                    short attack = *(short*)(model + 0x30);
                    *(short*)(model + 0x30) = (short)(attack >> 2);

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);

                    *(short*)(model + 0x30) = attack;

                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                }
            }

            // ============================================================ la projection, posture 13
            if (e[0x42] == 0x0d)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), throwProbe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(ushort*)(prey + 0x96) = 0x10;
                    *(uint*)prey |= 0x40;

                    short attack = *(short*)(model + 0x30);
                    *(short*)(model + 0x30) = (short)(attack >> 1);

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);

                    *(short*)(model + 0x30) = attack;

                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

                    // Ghidra: 0x004d7578 — LA PROJECTION. Elle n'a lieu que si la proie ne porte
                    // pas 0x40000000.
                    if ((*(uint*)prey & 0x40000000) == 0)
                    {
                        SpawnEffectEntity(e, 0x39, 0, -1);          // Ghidra: FUN_0047a9d0
                        *(uint*)prey |= 1;

                        int course = *(short*)(e + 0x32);
                        int push = *(int*)(e + 0x5c) >> 0xb;

                        *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * push;
                        *(int*)(prey + 0x54) = 0x18000;
                        *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(course) * push;
                    }
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
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004d7aff
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
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
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
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0xa000;   // le Vampire : 0x5000
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

                    int thrust = boldness * 0x90 + 0x7000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness + 0x32
                            : -0x32 - boldness);
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
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

                        *(short*)(e + 0x18) = 4;
                        *(short*)(e + 0x1a) = 0;

                        int thrust = (boldness + 0x280) << 6;
                        int step = (speed * thrust) >> 0xc;
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

                        // Ghidra: 0x004d7926 — c'est la POSTURE qui retombe a zero, pas le
                        // compte a rebours : il rend la main a l'attente, pas a la replanification.
                        e[0x42] = 0;
                    }
                }

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 12 : la morsure
            case 12:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;
                    if (distance > 0x96) e[0x43] = 0;
                }

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004d7aff
                return;
            }

            // ---------------------------------------------------------- 13 : la projection
            case 13:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 2 && (*(uint*)e & 1) == 0) e[0x43] = 0;

                if (phase == 1)
                {
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                        ? boldness + 0x32
                        : -0x32 - boldness);

                    if (*(short*)(e + 0x1a) >= 6)
                    {
                        int thrust = (boldness + 0x3c0) << 6;
                        int step = (speed * thrust) >> 0xc;
                        *(int*)(e + 0x5c) = thrust;

                        short course = *(short*)(e + 0x10);
                        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 3;
                        *(short*)(e + 0x12) = 2;
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x10) = (short)heading;               // le cap du bond : la cible
                *(short*)(e + 0x18) = 5;
                *(short*)(e + 0x1a) = 0;
                return;
            }

            // ---------------------------------------------------------- 14 : le hurlement
            case 14:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 2)
                {
                    *(int*)(e + 0x5c) = 0;

                    // Ghidra: 0x004d7a83 — `dec cl` SANS test de zero : le compte passe par 0xff
                    // s'il est deja nul. Garde tel quel.
                    e[0x43]--;
                }

                if (phase == 1)
                {
                    *(int*)(e + 0x5c) = 0;

                    if ((*(uint*)e & 0x2000000) != 0)
                    {
                        byte* sounds = *(byte**)(*(byte***)(s_Unknown_005e97cc + 0xa8));
                        Aud.PlaySoundEffect(sounds[0x2c], 0x400, 0x2000, 0x2000);   // Ghidra: FUN_0053cea0

                        SpawnEffectEntity(e, 0xde, 0, -1);          // Ghidra: FUN_0047a9d0

                        *(short*)(e + 0x12) = 2;
                        e[0x43] = 0x14;
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x18) = 6;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004d7aff
                return;
            }

            // ---------------------------------------------------------- 4, 5, 8, 9, 10 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004d7d39..0x004d8084</c> — <b>le troisieme temps</b> du Loup-garou. Sa plage de
    /// pres s'arrete a <b>100</b> unites, et non 250 : hors de portee de morsure, il est deja a
    /// mi-distance.
    ///
    /// <list type="bullet">
    /// <item><b>de pres</b> (&lt; 100) : sous 50, s'il fait face, le coup (0-29) ou la morsure
    ///   (30-49), sinon le demi-tour ; 60-69 le bond si la cible est au-dela de 164 — jamais, puisqu'on
    ///   est sous 100 ; tout le reste approche ;</item>
    /// <item><b>a mi-distance</b> (100 a 600) : 0-19 la projection s'il fait face, sinon le
    ///   demi-tour ; 20-39 l'approche ; 40-59 le bond ; 60-84 le louvoiement ; 85-99 le
    ///   hurlement ;</item>
    /// <item><b>de loin</b> (&gt;= 600) : 0-29 l'approche (le repos est hors d'atteinte, voir le
    ///   bandeau) ; 30-39 le louvoiement ; 40-59 l'approche ; 60-69 le hurlement s'il fait face,
    ///   sinon l'approche ; 70-99 le bond.</item>
    /// </list>
    /// </summary>
    private static void ReplanWerewolfAction_004D7D39(byte* e, int boldness, int _)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        // ================================================================ de pres
        if (distance < 0x64)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004d7dfc
                    e[0x43] = 1;
                }
                else
                {
                    // Ghidra: 0x004d7dbc — UN TIRAGE JETE, puis une comparaison qui ne peut pas
                    // etre vraie : la distance est deja sous 100. Voir le bandeau. Le tirage n'a
                    // lieu QUE s'il fait face (0x004d7dba saute par-dessus) : l'ordre compte pour
                    // le flux du generateur.
                    CrtRand();

                    if (distance >= 0x64)
                    {
                        e[0x42] = 1;
                        e[0x43] = 1;
                    }
                    else if (roll < 0x1e)
                    {
                        e[0x42] = 0x0b;
                        e[0x43] = 1;
                        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                        *(int*)(e + 0x5c) = 0;
                        *(short*)(e + 0x18) = 7;
                        *(short*)(e + 0x1a) = 0;
                    }
                    else
                    {
                        e[0x42] = 0x0c;                             // Ghidra: LAB_004d7dee
                        e[0x43] = 1;
                        *(short*)(e + 0x12) = 0;
                    }
                }
            }
            else if (roll >= 0x3c && roll < 0x46 && distance > boldness + 100)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 2;                                        // Ghidra: LAB_004d7e36
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0x64)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
                else
                {
                    e[0x42] = 0x0d;                                 // Ghidra: LAB_004d7e9e
                    e[0x43] = 1;
                    *(short*)(e + 0x12) = 0;
                }
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else if (roll < 0x55)
            {
                e[0x42] = 3;
                *(short*)(e + 0x12) = 0;
                e[0x43] = (byte)(CrtRand() % 10 + 0x14);
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
            else
            {
                e[0x42] = 0x0e;                                     // Ghidra: LAB_004d7f19
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
        }

        // ================================================================ de loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            // Ghidra: 0x004d7f42 — `tirage >= 2*hardiesse + 10`, soit 138 : jamais vrai.
            if (far >= boldness * 2 + 0x0a)
            {
                e[0x42] = 0;                                        // Ghidra: LAB_004d7f74
                *(short*)(e + 0x12) = 0;
                e[0x43] = (byte)(CrtRand() % 10);
                *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                return;
            }

            e[0x42] = 2;
            *(short*)(e + 0x10) = 0x64;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        if (far < 0x28)
        {
            e[0x42] = 3;                                            // Ghidra: LAB_004d7fa2
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            return;
        }

        if (far < 0x3c)
        {
            e[0x42] = 2;                                            // Ghidra: LAB_004d7fe4
            *(short*)(e + 0x10) = 0x64;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        if (far < 0x46)
        {
            if (((heading + 0x100) & 0xfff) <= 0x200)
            {
                e[0x42] = 0x0e;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
                return;
            }

            e[0x42] = 2;                                            // Ghidra: LAB_004d8043
            *(short*)(e + 0x10) = 0x64;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        e[0x42] = 6;                                                // Ghidra: LAB_004d8069
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }
}
