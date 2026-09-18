// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE TIGRE — 0x004D80D0, chateau de l'immortel (zone 6, sous-zones 5 et 11).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004D80D0 — LE TIGRE. Transcrit le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004D80D0.asm, 1 075 instructions, absent de Blaze.c),
    // table de sauts a QUINZE entrees a 0x004D8EEC. C'est le frere du Loup-garou (0x004D7200,
    // Ai/ImmortalCastle/Werewolf.cs), emis juste avant lui : meme charpente, memes quinze cases,
    // meme hardiesse ECRASEE par la constante 0x40 (0x004d81cc), meme fin de pas. Ce qui change :
    //
    //   - UNE SEULE SONDE, celle du Vampire {0, 0x1e, 0x32, 0x64, 0, 0x64}. Il ne projette
    //     personne : la posture 13 n'est plus un bond mais un coup a terre ;
    //   - DES POUSSEES PLUS FORTES : 0xe000 a l'approche et au louvoiement (le Loup-garou :
    //     0xa000 et 0x7000), 0xc000 en marchant droit (0x5000), et (0x40 + 0x400) << 6 au bond
    //     (0x280) ;
    //   - LE COUP DE PATTE (posture 12) frappe DEUX FOIS, a deux images exactes : l'image 12 de
    //     l'animation 3 (clignotement 2), puis l'image 18 de l'animation 5 (clignotement 16), l'une
    //     et l'autre au quart de l'attaque du modele. Le geste enchaine de lui-meme la seconde
    //     animation quand la premiere s'acheve ;
    //   - LA GRIFFE (posture 13), animation 7, frappe a l'image 10 a DEMI-attaque, puis
    //     l'effet 12 part par le filtre de 0x004D8F30 (copie de 0x004B1620) ;
    //   - LE RUGISSEMENT (posture 14) est le hurlement du Loup-garou a 0x500 de volume au lieu de
    //     0x400 ;
    //   - SA REPLANIFICATION est retouchee de partout (voir ReplanTigerAction_004D8B9C).
    //
    // LE BOND DE PRES EST MORT DEUX FOIS. Chez les deux freres, il demande une distance au-dela de
    // hardiesse + 100, soit 164, dans la plage ou elle est sous 100 : il ne part jamais. Le Tigre
    // y ajoute un tirage `>= 0x55` ET `< 0x46` (0x004d8c85) — le Loup-garou avait `>= 0x3c` au
    // meme endroit —, qu'aucun tirage ne satisfait non plus. Transcrit tel quel.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004d80d0</c> — <b>l'intelligence du Tigre</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004D80D0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x50] — la sonde du Vampire. Les deux 0x64 sont ecrits AVANT le `push edi`
        // et tombent donc sur les mots 3 et 5.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

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

        // Ghidra: 0x004d81cc — LA HARDIESSE EST ECRASEE, comme chez le Loup-garou.
        boldness = 0x40;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004d81db — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideTigerAction_004D8226(e, model, probe, boldness, distance, heading, speed);
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

        ReplanTigerAction_004D8B9C(e, boldness);
    }

    /// <summary>
    /// Ghidra: <c>0x004d8226..0x004d8971</c> — <b>le premier temps</b> du Tigre : les trois coups,
    /// puis la posture. Quinze cases, dont dix servies : 0 attendre, 1 se tourner, 2 approcher,
    /// 3 louvoyer, 6 bondir, 7 marcher droit, 11 le coup, 12 le coup de patte, 13 la griffe,
    /// 14 le rugissement.
    /// </summary>
    private static void DecideTigerAction_004D8226(
        byte* e, byte* model, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        if ((e[0] & 2) != 0)
        {
            // ============================================================ le coup ordinaire
            // Ghidra: 0x004d8295 — les postures 12 et 13 sautent ce bloc ET sa queue.
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

            // ============================================================ le coup de patte, posture 12
            // Ghidra: 0x004d832c — deux images exactes : la 12e de l'animation 3, la 18e de la 5.
            if (e[0x42] == 0x0c && IsTigerPawFrame(e))
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && IsTigerPawFrame(e))
                {
                    *(ushort*)(prey + 0x96) = (ushort)(*(short*)(e + 0x18) == 3 ? 2 : 0x10);
                    *(uint*)prey |= 0x40;

                    // Ghidra: 0x004d83bc — le mot +0x30 du BLOC DE MODELE, divise par quatre le
                    // temps du calcul de degat, puis rendu.
                    short attack = *(short*)(model + 0x30);
                    *(short*)(model + 0x30) = (short)(attack >> 2);

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);

                    *(short*)(model + 0x30) = attack;

                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                }
            }

            // ============================================================ la griffe, posture 13
            // Ghidra: 0x004d83fb — la sonde balaie des l'image 10, le coup ne part qu'a l'image 10.
            if (e[0x42] == 0x0d && *(short*)(e + 0x1a) >= 0x0a)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0a)
                {
                    *(ushort*)(prey + 0x96) = 0x10;
                    *(uint*)prey |= 0x40;

                    short attack = *(short*)(model + 0x30);
                    *(short*)(model + 0x30) = (short)(attack >> 1);

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);

                    *(short*)(model + 0x30) = attack;

                    // Ghidra: 0x004d8467 — FUN_004d8f30, copie de FUN_004b1620 : l'effet 12.
                    ApplyStatusEffectIfAllowed_004B1620((uint*)e, (uint*)prey, 0);

                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
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
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004d88d6
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
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0xe000;   // le Loup-garou : 0xa000
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

                    int thrust = boldness * 0x90 + 0xe000;          // le Loup-garou : 0x7000
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

                        int thrust = (boldness + 0x400) << 6;       // le Loup-garou : 0x280
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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0xc000;       // le Loup-garou : 0x5000
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

                        // Ghidra: 0x004d87fc — c'est la POSTURE qui retombe a zero, comme chez le
                        // Loup-garou.
                        e[0x42] = 0;
                    }
                }

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 12 : le coup de patte
            case 12:
            {
                if (distance > 0x96) e[0x43] = 0;

                short phase = *(short*)(e + 0x12);

                if (phase == 2 && (*(uint*)e & 0x2000000) != 0) e[0x43] = 0;

                // Ghidra: 0x004d8843 — la premiere animation achevee enchaine la seconde, deja
                // avancee a son image 11.
                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfeffffff;
                    *(short*)(e + 0x12) = 2;
                    *(short*)(e + 0x18) = 5;
                    *(short*)(e + 0x1a) = 0x0b;
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 13 : la griffe
            case 13:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1 && (*(uint*)e & 0x2000000) != 0) e[0x43] = 0;

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x18) = 7;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004d88d6
                return;
            }

            // ---------------------------------------------------------- 14 : le rugissement
            case 14:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 2)
                {
                    *(int*)(e + 0x5c) = 0;

                    // Ghidra: 0x004d88ed — `dec cl` SANS test de zero, comme chez le Loup-garou.
                    e[0x43]--;
                }

                if (phase == 1)
                {
                    *(int*)(e + 0x5c) = 0;

                    if ((*(uint*)e & 0x2000000) != 0)
                    {
                        byte* sounds = *(byte**)(*(byte***)(s_Unknown_005e97cc + 0xa8));
                        Aud.PlaySoundEffect(sounds[0x2c], 0x500, 0x2000, 0x2000);   // Ghidra: FUN_0053cea0

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
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 4, 5, 8, 9, 10 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004d8336</c> et <c>0x004d837c</c> — le meme test ecrit deux fois : l'image 12
    /// de l'animation 3, ou l'image 18 de l'animation 5.
    /// </summary>
    private static bool IsTigerPawFrame(byte* e)
    {
        short animation = *(short*)(e + 0x18);
        short frame = *(short*)(e + 0x1a);
        return (animation == 3 && frame == 0x0c) || (animation == 5 && frame == 0x12);
    }

    /// <summary>
    /// Ghidra: <c>0x004d8b9c..0x004d8eeb</c> — <b>le troisieme temps</b> du Tigre. Les trois plages
    /// du Loup-garou (100 et 600), aux tirages retouches :
    ///
    /// <list type="bullet">
    /// <item><b>de pres</b> (&lt; 100) : sous 75, s'il fait face, le coup (0-29), le coup de patte
    ///   (30-59) ou la griffe (60-74), sinon le demi-tour ; tout le reste approche (le bond est hors
    ///   d'atteinte, voir le bandeau) ;</item>
    /// <item><b>a mi-distance</b> (100 a 600) : 0-19 le demi-tour s'il ne fait PAS face, l'approche
    ///   sinon ; 20-39 l'approche ; 40-59 le bond ; 60-99 le louvoiement. Pas de rugissement a
    ///   cette distance ;</item>
    /// <item><b>de loin</b> (&gt;= 600) : 0-29 l'approche (le repos est hors d'atteinte, comme chez
    ///   le Loup-garou) ; 30-39 le louvoiement ; 40-59 l'approche ; 60-64 le rugissement s'il fait
    ///   face, sinon l'approche ; 65-99 le bond.</item>
    /// </list>
    /// </summary>
    private static void ReplanTigerAction_004D8B9C(byte* e, int boldness)
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

            if (roll < 0x4b)
            {
                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004d8c7b
                    e[0x43] = 1;
                }
                else
                {
                    // Ghidra: 0x004d8c28 — un tirage jete, puis une comparaison qui ne peut pas
                    // etre vraie (la distance est deja sous 100). Le tirage deplace le flux.
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
                        *(short*)(e + 0x18) = 3;
                        *(short*)(e + 0x1a) = 0;
                    }
                    else if (roll < 0x3c)
                    {
                        e[0x42] = 0x0c;                             // Ghidra: LAB_004d8c5a
                        e[0x43] = 1;
                        *(short*)(e + 0x12) = 0;
                    }
                    else
                    {
                        e[0x42] = 0x0d;                             // Ghidra: LAB_004d8c6d
                        e[0x43] = 1;
                        *(short*)(e + 0x12) = 0;
                    }
                }
            }
            // Ghidra: 0x004d8c85 — `>= 0x55` puis `< 0x46` : jamais vrai. Voir le bandeau.
            else if (roll >= 0x55 && roll < 0x46 && distance > boldness + 100)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 2;                                        // Ghidra: LAB_004d8cb5
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0x64)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14 && ((heading + 0x400) & 0xfff) >= 0x800)
            {
                e[0x42] = 1;
                e[0x43] = 1;
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;                                        // Ghidra: LAB_004d8d1f
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
            else
            {
                e[0x42] = 3;                                        // Ghidra: LAB_004d8d5a
                *(short*)(e + 0x12) = 0;
                e[0x43] = (byte)(CrtRand() % 10 + 0x14);
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
        }

        // ================================================================ de loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            // Ghidra: 0x004d8da9 — `tirage >= 2*hardiesse + 10`, soit 138 : jamais vrai.
            if (far >= boldness * 2 + 0x0a)
            {
                e[0x42] = 0;                                        // Ghidra: LAB_004d8ddb
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
            e[0x42] = 3;                                            // Ghidra: LAB_004d8e09
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            return;
        }

        if (far < 0x3c)
        {
            e[0x42] = 2;                                            // Ghidra: LAB_004d8e4b
            *(short*)(e + 0x10) = 0x64;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        if (far < 0x41)
        {
            if (((heading + 0x100) & 0xfff) <= 0x200)
            {
                e[0x42] = 0x0e;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
                return;
            }

            e[0x42] = 2;                                            // Ghidra: LAB_004d8eaa
            *(short*)(e + 0x10) = 0x64;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        e[0x42] = 6;                                                // Ghidra: LAB_004d8ed0
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();
    }
}
