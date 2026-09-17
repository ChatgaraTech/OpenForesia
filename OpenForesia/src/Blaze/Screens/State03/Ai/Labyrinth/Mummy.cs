// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA MOMIE, LA REINE MOMIE, LA SANGLANTE ET LA VIEILLE MOMIE — 0x004C0B60, Labyrinthe des morts
// (zone 5, sous-zones 5, 6, 7, 8). Une routine, quatre especes, quatre variantes.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004C0B60 — LA MOMIE. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004C0B60.asm, 1178 instructions, absente de Blaze.c), table de
    // sauts a QUATORZE entrees a 0x004C1B38 — la plus longue routine d'intelligence de la zone.
    //
    // LA CHARPENTE DU RAMPANT, mais LA VARIANTE Y DECIDE DE TOUT :
    //
    //   - LA VARIANTE 3 SE REECRIT EN VARIANTE 0 des la premiere image ou elle est vue, apres s'etre
    //     donne une echelle de 0x1388 (aplatie a 0x1068 en hauteur) et une teinte (0xdc, 0xdc, 0xaa).
    //     C'est la seule bete du depot qui change de variante hors coup porte ;
    //   - SON COUP ALTERE SELON LA VARIANTE : un tirage sur vingt par effet, et un seul peut partir.
    //     Hors variante 0, l'effet d'etat 9 ; en variante 0, l'effet de piege ; en variante 2 ou 3,
    //     l'effet d'etat 7 ;
    //   - LE COUP PART EN PLEIN BOND, comme chez le Persil, sur un tirage modulo 40 sous la hardiesse ;
    //   - TROIS NAISSANCES, une par posture : 10 fait naitre 0x36 a l'image 17 ; 12 et 13 font naitre
    //     0xdf en se notant la proie dans +0x78 — la premiere avec +0x9a a 2, la seconde a 0 ;
    //   - LA POSTURE 13 REFUSE DEUX FOIS : si un effet du bassin 0x005ff400 la porte encore en +0x6c,
    //     ou si sa propre fiche du bassin 0x005eca80 porte 0x0d en +0x62, elle rend la main sans
    //     rien faire ;
    //   - UN COMPTEUR LIBRE DANS +0x14, de 0x3e8 a 0x7530, remis a 0x3e8 quand il deborde. Les trois
    //     naissances s'en servent de garde : sous 0xc8 elles retombent en attente, au-dela elles
    //     remettent le compteur a zero ;
    //   - SA REPLANIFICATION LOINTAINE CHOISIT LA NAISSANCE PAR VARIANTE : 0 prend la 13, 1 tire
    //     entre 12 et 13, 2 prend la 10, 3 tire entre 10 et 12.
    // =============================================================================================

    /// <summary>
    /// Ghidra: FUN_004c1bb0 @ 0x004c1bb0 — <b>l'effet d'etat 9, s'il est permis</b> : le filtre de
    /// <see cref="RejectStatusTarget_004A2C30"/>, puis <see cref="ApplyStatusEffect_0053B580"/>.
    /// </summary>
    /// <returns><c>1</c> si la victime a ete refusee, <c>0</c> si l'effet est parti.</returns>
    private static uint ApplyStatusEffect9IfAllowed_004C1BB0(uint* caster, uint* victim, int keep)
    {
        if (RejectStatusTarget_004A2C30(caster, victim)) return 1;

        ApplyStatusEffect_0053B580(caster, victim, keep);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c0b60</c> — <b>l'intelligence de la Momie</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004C0B60(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde la plus longue de la zone : {0, 0, 0x32, 0x64, 0, 0x82}.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x82;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            // Ghidra: 0x004c0ba2 — la variante 0 cherche autrement.
            PickChasedPartyMember_004A1020(e, *(short*)(e + 0x16) == 0 ? 2 : 3, 0);   // Ghidra: FUN_004bde40
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        // Ghidra: 0x004c0bd7 — LA VARIANTE 3 SE REECRIT, et se donne au passage sa taille et sa
        // teinte. Ce n'est pas garde par la premiere image : c'est garde par la variante elle-meme,
        // qui ne vaut plus 3 des le prochain appel.
        if (*(short*)(e + 0x16) == 3)
        {
            *(short*)(e + 0x16) = 0;
            *(ushort*)(e + 0x28) = 0x1388;
            *(ushort*)(e + 0x2c) = 0x1388;
            *(ushort*)(e + 0x2a) = 0x1068;
            e[0x38] = 0xdc;
            e[0x39] = 0xdc;
            e[0x3a] = 0xaa;
        }

        // Ghidra: 0x004c0c4c — le compteur libre, de 0x3e8 a 0x7530.
        (*(short*)(e + 0x14))++;
        if (*(short*)(e + 0x14) > 0x7530) *(short*)(e + 0x14) = 0x3e8;

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideMummyAction_004C0CC3(e, probe, boldness, timid, distance, heading, speed);

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

        ReplanMummyAction_004C1701(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004c0cc3..0x004c14c0</c> — <b>le premier temps</b> de la Momie. Quatorze cases,
    /// dont dix servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 6 bondir, 7 marcher
    /// droit, 10 la naissance de <c>0x36</c>, 11 le coup, 12 et 13 les deux naissances de
    /// <c>0xdf</c>. Les cases 4, 5, 8 et 9 rendent la main.
    /// </summary>
    private static void DecideMummyAction_004C0CC3(
        byte* e, short* probe, int boldness, int timid, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        if ((e[0] & 2) != 0)
        {
            // ============================================================ le coup, image 13
            if (*(short*)(e + 0x1a) == 0x0d)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0d)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

                    // Ghidra: 0x004c0da1 — trois tirages sur vingt, et UN SEUL effet peut partir.
                    bool spent = false;
                    short variant = *(short*)(e + 0x16);

                    if (CrtRand() % 0x14 == 1 && variant != 0)
                    {
                        ApplyStatusEffect9IfAllowed_004C1BB0((uint*)e, (uint*)prey, 0);
                        spent = true;
                    }

                    if (CrtRand() % 0x14 == 1 && variant == 0 && !spent)
                    {
                        // Ghidra: FUN_004c1bf0, copie de FUN_004a2bf0.
                        ApplyTrapEffectIfAllowed_004A2BF0((uint*)e, (uint*)prey, 0);
                        spent = true;
                    }

                    if (CrtRand() % 0x14 == 1 && (variant == 2 || variant == 3) && !spent)
                    {
                        // Ghidra: FUN_004c1b70, copie de FUN_004baae0.
                        ApplyStatusEffectIfAllowed_004BAAE0((uint*)e, (uint*)prey, 0);
                    }
                }
            }

            uint f = *(uint*)e;
            if ((f & 0x2000000) != 0)
            {
                // Ghidra: LAB_004c1ada — en l'air le geste tombe, mais l'attente ne se rouvre pas.
                *(uint*)e = f & 0xfffffffd;
                if ((f & 1) == 0) e[0x43] = 0;
            }
        }
        else
        {
            // ============================================================ Ghidra: LAB_004c1ae4
            // LE COUP PART EN PLEIN BOND : en l'air, avant l'image 3, un tirage sous la hardiesse.
            uint f = *(uint*)e;

            if ((f & 1) != 0 && *(short*)(e + 0x1a) < 3 && CrtRand() % 0x28 < boldness)
            {
                *(uint*)e = (f & 0xfeffffff) | 2;
                *(short*)(e + 0x18) = 3;
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

                if (e[0x43] == 0)
                {
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004c0f56
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                e[0x43]--;
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
                    e[0x43] = 0;                                    // Ghidra: LAB_004c0f53
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = (boldness + 0x80) << 7;
                }

                if (*(short*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004c0f53
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                // Ghidra: 0x004c0f96 — un pas FIXE de 0x50, le seul de la zone qui ignore la hardiesse.
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? 0x50 : -0x50);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = (boldness + 0x100) << 6;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    // Ghidra: 0x004c0fd7 — le pas suit LE CAP REEL, pas le cap tire.
                    short course = *(short*)(e + 0x32);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        // Ghidra: 0x004c1022 — et le cap rejoint le cap tire au huitieme de l'ecart.
                        short now = *(short*)(e + 0x32);
                        *(short*)(e + 0x32) = (short)(now + ((*(short*)(e + 0x10) - now) >> 3));
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000000;
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

                        int thrust = (boldness + 0xc0) << 6;
                        int step = (speed * thrust) >> 0xc;

                        *(short*)(e + 0x18) = 1;
                        *(short*)(e + 0x1a) = 0;
                        *(int*)(e + 0x5c) = thrust;

                        short course = *(short*)(e + 0x10);
                        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x14000;

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

                *(short*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = (boldness >> 1) + 0x4000;
                return;
            }

            // ---------------------------------------------------------- 10 : faire naitre 0x36
            case 10:
                TurnMummyToward(e, heading, boldness);

                if (*(short*)(e + 0x12) == 1 && *(short*)(e + 0x1a) == 0x11)
                {
                    SpawnEffectEntity(e, 0x36, 0, -1);              // Ghidra: FUN_0047a9d0
                }

                if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;
                if (*(short*)(e + 0x12) != 0) return;

                OpenMummyBirth(e, boldness, timid);

                // Ghidra: LAB_004c11f3 — les variantes 0 et 1 retombent en attente, les autres non.
                if (*(short*)(e + 0x16) == 0 || *(short*)(e + 0x16) == 1) RestMummy_004C1359(e, boldness, timid);
                return;

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

            // ---------------------------------------------------------- 12 : faire naitre 0xdf
            case 12:
                TurnMummyToward(e, heading, boldness);

                if (*(short*)(e + 0x12) == 1)
                {
                    if (distance > 0x3e8)
                    {
                        e[0x43] = 0;
                        return;
                    }

                    if (*(short*)(e + 0x1a) == 0x11) BirthMummySpawn_004C12B0(e, 2);
                }

                if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;
                if (*(short*)(e + 0x12) != 0) return;

                OpenMummyBirth(e, boldness, timid);

                // Ghidra: LAB_004c1346 — les variantes 0 et 2 retombent en attente.
                if (*(short*)(e + 0x16) == 0 || *(short*)(e + 0x16) == 2) RestMummy_004C1359(e, boldness, timid);
                return;

            // ---------------------------------------------------------- 13 : la naissance qui refuse
            case 13:
                TurnMummyToward(e, heading, boldness);

                if (*(short*)(e + 0x12) == 1)
                {
                    if (distance > 0x3e8)
                    {
                        e[0x43] = 0;
                        return;
                    }

                    if (*(short*)(e + 0x1a) == 0x11) BirthMummySpawn_004C12B0(e, 0);
                }

                if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;
                if (*(short*)(e + 0x12) != 0) return;

                // Ghidra: 0x004c140c — DEUX refus avant d'accepter la naissance.
                if (MummyBirthRefused_004C140C(e))
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004c144e
                    return;
                }

                OpenMummyBirth(e, boldness, timid);

                // Ghidra: LAB_004c14a8 — seule la variante 2 retombe en attente, et son attente est
                // NULLE : elle replanifiera des la prochaine image.
                if (*(short*)(e + 0x16) == 2)
                {
                    e[0x42] = 0;
                    e[0x43] = 0;
                    *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                    *(short*)(e + 0x12) = 0;
                }

                return;

            // ---------------------------------------------------------- 4, 5, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>Ghidra : le virage des trois postures de naissance, <c>±(hardiesse + 0x80)</c>, pose
    /// AVANT tout le reste.</summary>
    private static void TurnMummyToward(byte* e, int heading, int boldness)
    {
        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
    }

    /// <summary>
    /// Ghidra: <c>0x004c12b0</c> et <c>0x004c13c5</c> — <b>la naissance de <c>0xdf</c></b> : la proie
    /// est notee dans <c>+0x78</c> et <c>+0x9a</c> prete sa valeur au nouveau-ne le temps de l'appel.
    /// </summary>
    private static void BirthMummySpawn_004C12B0(byte* e, short bobDuringBirth)
    {
        short bob = *(short*)(e + 0x9a);
        *(short*)(e + 0x9a) = bobDuringBirth;
        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);

        SpawnEffectEntity(e, 0xdf, 0, -1);                          // Ghidra: FUN_0047a9d0

        *(short*)(e + 0x9a) = bob;
    }

    /// <summary>
    /// Ghidra: <c>0x004c1197</c>, <c>0x004c12ea</c> et <c>0x004c1453</c> — <b>l'ouverture d'une
    /// naissance</b>, la meme aux trois postures : animation 4, et <b>le compteur libre decide</b> —
    /// sous <c>0xc8</c> la bete retombe en attente, au-dela le compteur repart de zero.
    /// </summary>
    private static void OpenMummyBirth(byte* e, int boldness, int timid)
    {
        *(uint*)e &= 0xfeffffff;
        *(short*)(e + 0x12) = 1;
        *(short*)(e + 0x18) = 4;
        *(short*)(e + 0x1a) = 0;
        *(int*)(e + 0x5c) = 0;

        if (*(short*)(e + 0x14) >= 0xc8)
        {
            *(short*)(e + 0x14) = 0;                                // Ghidra: LAB_004c11ef
            return;
        }

        e[0x42] = 0;
        *(short*)(e + 0x12) = 0;
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
        *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
    }

    /// <summary>
    /// Ghidra: <c>LAB_004c1359</c> — <b>le retour a l'attente</b> apres une naissance, pour les
    /// variantes que la posture designe.
    /// </summary>
    private static void RestMummy_004C1359(byte* e, int boldness, int timid)
    {
        e[0x42] = 0;
        e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
        *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);         // Ghidra: LAB_004c14b9
        *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c140c..0x004c144d</c> — <b>les deux refus</b> de la posture 13 : un effet du
    /// bassin <c>0x005ff400</c> qui la porte encore, ou sa propre fiche du bassin
    /// <c>0x005eca80</c> portant <c>0x0d</c> en <c>+0x62</c>.
    ///
    /// <para>La seconde boucle parcourt les trente-deux fiches en comptant, et ne s'arrete que sur
    /// celle dont le rang egale l'octet <c>+0x08</c> de la Momie : c'est donc SA PROPRE fiche
    /// qu'elle finit par examiner, par un chemin detourne. Transcrit tel quel.</para>
    /// </summary>
    private static bool MummyBirthRefused_004C140C(byte* e)
    {
        byte* effects = Mem.Entity(0x005ff400);

        for (int i = 0; i < 0x20; i++)
        {
            byte* effect = effects + i * 0x9c;
            if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0) return true;
        }

        byte* pool = Mem.Entity(0x005eca80);

        for (int i = 0; i < 0x20; i++)
        {
            if (pool[i * 0x9c + 0x62] == 0x0d && i == e[8]) return true;
        }

        return false;
    }

    /// <summary>
    /// Ghidra: <c>0x004c1701..0x004c1ace</c> — <b>le troisieme temps</b> de la Momie, trois plages
    /// (<c>0x96</c>, <c>0x2BC</c>) et des seuils plus serres que ceux de ses sœurs.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : sous 50, frapper dans le cone et a portee tiree, sinon se tourner ; de
    /// 60 a 69, bondir si la proie est au-dela de <c>hardiesse + 100</c> ; tout le reste approche.</item>
    /// <item><b>A mi-distance</b> : face a elle attendre, sinon approcher ; approcher ; bondir ;
    /// louvoyer vers un cap tire ; <b>et au-dela de 69 la VARIANTE choisit la naissance</b> — 0 prend
    /// la 13, 1 tire entre 12 et 13, 2 prend la 10, 3 tire entre 10 et 12.</item>
    /// <item><b>De loin</b> : approcher si le tirage tombe sous <c>hardiesse * 2 + 10</c>, sinon
    /// attendre ; louvoyer ; se tourner ; la naissance 10 si elle fait face, sinon approcher ; et
    /// au-dela bondir.</item>
    /// </list>
    /// </summary>
    private static void ReplanMummyAction_004C1701(byte* e, int boldness, int timid)
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
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    e[0x42] = 0x0b;
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    *(short*)(e + 0x18) = 3;
                    *(short*)(e + 0x1a) = 0;
                }
                else
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004c17c5
                    e[0x43] = 1;
                }
            }
            else if (roll >= 0x3c && roll < 0x46 && distance > boldness + 0x64)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
                return;                                             // Ghidra: jmp 004C1828
            }
            else
            {
                ApproachMummyTarget_004C17FF(e, timid, 0x50, 10);    // Ghidra: LAB_004c17ff
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x2bc && distance >= 0x96)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14 && ((heading + 0x400) & 0xfff) < 0x800)
            {
                e[0x42] = 0;                                        // Ghidra: 0x004c1870
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
                *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                *(short*)(e + 0x12) = 0;
                return;
            }

            if (roll < 0x28)
            {
                ApproachMummyTarget_004C17FF(e, timid, 0x50, 10);    // Ghidra: LAB_004c18a0
                return;
            }

            if (roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
                return;
            }

            if (roll < 0x46)
            {
                e[0x42] = 3;
                *(short*)(e + 0x12) = 0;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
                return;
            }

            // Ghidra: LAB_004c1922 — LA VARIANTE CHOISIT LA NAISSANCE.
            short variant = *(short*)(e + 0x16);

            if (variant == 0)
            {
                e[0x42] = 0x0d;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }

            if (variant == 1)
            {
                e[0x42] = (byte)(roll >= 0x55 ? 0x0d : 0x0c);
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }

            if (variant == 2)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }

            if (variant == 3)
            {
                e[0x42] = (byte)(roll >= 0x55 ? 0x0c : 0x0a);
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;                            // Ghidra: LAB_004c197f
            }

            return;
        }

        // ================================================================ de loin
        if (distance < 0x2bc) return;

        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            if (far < boldness * 2 + 0x0a)
            {
                ApproachMummyTarget_004C17FF(e, timid, 0x64, 10);    // Ghidra: 0x004c19b1
                return;
            }

            e[0x42] = 0;                                            // Ghidra: LAB_004c19c5
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x28)
        {
            e[0x42] = 3;                                            // Ghidra: LAB_004c19fa
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            return;
        }

        if (far < 0x50)
        {
            e[0x42] = 1;                                            // Ghidra: LAB_004c1a42
            e[0x43] = 1;
            return;
        }

        if (far < 0x5a)
        {
            if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200)
            {
                e[0x42] = 0x0a;                                     // Ghidra: 0x004c1a7b
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
                return;
            }

            // Ghidra: LAB_004c1a92 — la seule approche qui tire modulo VINGT.
            ApproachMummyTarget_004C17FF(e, timid, 0x64, 0x14);
            return;
        }

        e[0x42] = 6;                                                // Ghidra: LAB_004c1abe
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }

    /// <summary>
    /// Ghidra: <c>LAB_004c17ff</c>, <c>LAB_004c18a0</c> et <c>LAB_004c1aa1</c> — <b>l'approche</b>, a
    /// trois portees et deux modules de tirage.
    /// </summary>
    private static void ApproachMummyTarget_004C17FF(byte* e, int timid, short range, int span)
    {
        e[0x42] = 2;
        *(short*)(e + 0x10) = range;
        e[0x43] = (byte)(CrtRand() % span + (timid >> 1) + 0x14);
    }
}
