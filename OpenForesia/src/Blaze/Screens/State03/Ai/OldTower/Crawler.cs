// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE RAMPANT — 0x004ADAE0, vieille tour (zone 4, sous-zones 5, 6, 8, 10).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004ADAE0 — LE RAMPANT. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/OldTower/FUN_004ADAE0.asm, 986 instructions, absente de Blaze.c), table de
    // sauts a douze entrees a 0x004AE7E8.
    //
    // LA CHARPENTE DU POURPRE PREUX (Ai/ImmortalCastle/PurpleKnight.cs) — integration FUN_00488160,
    // pose au sol et heurt au masque 0x2000ad82 (SettleSceneEntityOnGroundAndWalls tel quel), bit
    // 0x10 en posture 5 — AVEC LE SORT DE LA SALAMANDRE (Ai/Mine/Salamander.cs) en posture 10 :
    //
    //   - UN SEUL COUP, porte par le prelude a l'image 11 (animation 3), et pas de combo ni de charge ;
    //   - SES SORTS dependent de la variante : 0, un tirage parmi Fleches magiques, Coup de jus, Gros
    //     sabots et Vitesse ; 1, parmi Turbo, Ralenti et Ventilo ; au-dela, le mot 0x20 laisse par la
    //     boucle. Depart a l'image 7 de l'animation 5 ; ni montee ni refus de hauteur ;
    //   - IL LOUVOIE EN VIRANT HUIT FOIS SA HARDIESSE (posture 3) ;
    //   - SA REPLANIFICATION est celle de la famille Salamandre, mais le bond (posture 6) y remplace le
    //     crachat, et de loin il peut incanter.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552a60</c> et <c>&amp;DAT_00552a68</c> — <b>les sorts du Rampant</b>,
    /// releves dans <c>.data</c> : <c>0008 000b 0201 0202</c> puis <c>020d 020c 0015</c>.
    /// Variante 0 : <c>hasard &amp; 3</c> signe — Fleches magiques, Coup de jus, Gros sabots,
    /// Vitesse. Variante 1 : <c>hasard % 3</c> — Turbo, Ralenti, Ventilo.
    /// </summary>
    private static readonly ushort[] s_CrawlerSpells_00552A60 = [0x0008, 0x000b, 0x0201, 0x0202];
    private static readonly ushort[] s_CrawlerSpells_00552A68 = [0x020d, 0x020c, 0x0015];

    /// <summary>
    /// Ghidra: <c>0x004adae0</c> — <b>l'intelligence du Rampant</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004ADAE0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde, la premiere du Pourpre preux.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 2, 0);   // Ghidra: FUN_004ac860, copie de FUN_004a1020
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004adbf5 — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideCrawlerAction_004ADC40(e, probe, boldness, distance, heading, speed);

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

        ReplanCrawlerAction_004AE44A(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004adc40..0x004ae212</c> — <b>le premier temps</b> du Rampant. Douze cases, dont
    /// huit servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 6 bondir, 7 marcher droit,
    /// 10 le sort, 11 le coup.
    /// </summary>
    private static void DecideCrawlerAction_004ADC40(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le coup
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 0x0b)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0b)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }
            }

            uint f = *(uint*)e;
            if ((f & 0x2000000) != 0)
            {
                *(uint*)e = f & 0xfffffffd;
                if ((f & 1) == 0) e[0x43] = 0;
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

                *(int*)(e + 0x5c) = 0;
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

                    // Ghidra: 0x004adf3f — huit fois la hardiesse.
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 8 + 0x32
                            : -0x32 - boldness * 8);
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

            // ---------------------------------------------------------- 10 : le sort
            case 10:
                CrawlerCast_004AE07F(e);
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

            // ---------------------------------------------------------- 4, 5, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004ae07f..0x004ae1e4</c> — <b>le sort</b>, posture 10, en quatre phases :
    /// l'ossature de <see cref="SalamanderCast_0048E8C0"/>, un sort par variante, incantation sur
    /// l'animation 4, lancer sur la 5 et depart a l'image <b>7</b>.
    ///
    /// <para><b>Le registre non charge</b> : au-dela de la variante 1, le mot du sort est le compteur
    /// de la boucle de refus, <c>0x20</c>.</para>
    /// </summary>
    private static void CrawlerCast_004AE07F(byte* e)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            byte* effects = Mem.Entity(0x005ff400);
            int scanned = 0;                                        // Ghidra: edi

            for (; scanned < 0x20; scanned++)
            {
                byte* effect = effects + scanned * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004ae1e1
                    return;
                }
            }

            uint flags = *(uint*)e;
            if ((flags & 0x804) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffff7fb;
                return;
            }

            SpawnCastEntity_0047AAE0(e, 0xbd, 0, -1);
            *(short*)(e + 0x12) = 1;

            ushort spell = (ushort)scanned;                         // 32 : le compteur, pas un sort
            if (*(short*)(e + 0x16) == 0) spell = s_CrawlerSpells_00552A60[SignedMask(CrtRand(), 3)];
            if (*(short*)(e + 0x16) == 1) spell = s_CrawlerSpells_00552A68[CrtRand() % 3];

            *(ushort*)(e + 6) = spell;

            byte* sheet = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)
                                    + ((spell >> 8) & 0xff) * 4)
                          + (spell & 0xff) * 0x30;
            *(ushort*)(e + 0x10) = sheet[0x13];

            *(uint*)e |= 0x1000804;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x18) = 4;
            *(short*)(e + 0x1a) = 0;
        }

        if (*(short*)(e + 0x12) == 1)
        {
            (*(short*)(e + 0x10))--;
            if (*(short*)(e + 0x10) < 1)
            {
                *(short*)(e + 0x12) = 2;
                *(uint*)e &= 0xfefffffb;
                *(short*)(e + 0x18) = 5;
                *(short*)(e + 0x1a) = 0;
            }
        }

        if (*(short*)(e + 0x12) == 2 && *(short*)(e + 0x1a) == 7)
        {
            ushort spell = *(ushort*)(e + 6);
            CastSpell_0053A980((uint*)e, (spell >> 8) & 0xff, spell & 0xff, -1, -1);
            *(short*)(e + 0x12) = 3;
        }

        if (*(short*)(e + 0x12) != 3) return;

        uint done = *(uint*)e;
        if ((done & 0x2000000) == 0) return;

        *(uint*)e = done & 0xfffff7ff;                             // `and ah,0F7h`
        e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004ae44a..0x004ae7e1</c> — <b>le troisieme temps</b> du Rampant, trois plages
    /// testees l'une apres l'autre (<c>0xFA</c>, <c>0x258</c>).
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : la moitie des tirages porte le coup dans le cone et a portee (un tiers
    /// pour un tirage sous 30, sinon une attente de <c>hasard % 40 + timidite / 2 + 20</c> a distance
    /// <c>0x50</c>), ou se tourne ; les tirages de 60 a 69 bondissent si la cible est au-dela de
    /// <c>hardiesse + 100</c> ; tout le reste file vers un cap tire.</item>
    /// <item><b>A mi-distance</b> : face a la cible le sort, sinon se tourner ; approcher ; bondir ; et
    /// sur les hauts tirages le sort ou filer, selon la hardiesse.</item>
    /// <item><b>De loin</b> : attendre, filer, se tourner, le sort ou approcher, bondir.</item>
    /// </list>
    /// </summary>
    private static void ReplanCrawlerAction_004AE44A(byte* e, int boldness, int timid)
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
                        e[0x42] = 0x0b;
                        e[0x43] = 1;
                        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                        *(int*)(e + 0x5c) = 0;
                        *(short*)(e + 0x18) = 3;
                        *(short*)(e + 0x1a) = 0;
                    }
                    else
                    {
                        e[0x42] = 0;
                        int wait = CrtRand() % 0x28;
                        *(short*)(e + 0x10) = 0x50;
                        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
                    }
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
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
                StrafeSalamander(e, timid);                         // Ghidra: LAB_004ae7a5
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
                    e[0x42] = 0x0a;
                    e[0x43] = 1;
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
            else if (roll < 0x46)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else if (100 - roll < (boldness >> 1) + 10)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                StrafeSalamander(e, timid);
            }
        }

        // ================================================================ de loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            e[0x42] = 0;
            int rest = CrtRand() % 10;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(rest + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
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
            if (100 - far < (boldness >> 1) + 10)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
                return;
            }

            e[0x42] = 2;
            int wait = CrtRand() % 10;
            *(short*)(e + 0x10) = 100;
            e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }
}
