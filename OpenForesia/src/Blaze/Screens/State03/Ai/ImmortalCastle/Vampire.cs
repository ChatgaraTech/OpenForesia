// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE VAMPIRE (ET LE PETIT VAMPIRE) — 0x004CF3A0, chateau de l'immortel (zone 6, sous-zones 6 a 10),
// et 0x004D8F70, sa copie des sous-zones 5 et 11.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004CF3A0 — LE VAMPIRE. Transcrit le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004CF3A0.asm, 1 039 instructions, absente de Blaze.c),
    // table de sauts a TREIZE entrees a 0x004D0180.
    //
    // DEUX CORPS D'UN COUP. 0x004D8F70 (sous-zones 5 et 11) se compare a 0x004CF3A0 instruction pour
    // instruction une fois les cibles normalisees : zero ligne differente hors de la table de sauts,
    // qui ne differe que par ses adresses. Son chercheur de proie, 0x004D5A20, est
    // PickChasedPartyMember_004A1020 instruction pour instruction, comme 0x004CEFE0 ici.
    //
    // LA CHARPENTE DU RAMPANT (Ai/OldTower/Crawler.cs), AU MOT PRES POUR LA TETE ET LA QUEUE : meme
    // sonde, meme lecture de situation, meme integration FUN_00488160, meme pose au sol et meme heurt
    // de mur au masque 0x2000ad82. 267 lignes d'ecart sur 1 050, et toutes dans les gestes :
    //
    //   - LE COUP PART A L'IMAGE 9, pas 11 ;
    //   - IL SE TOURNE ET APPROCHE MOINS VITE (hardiesse + 0x80, et non deux fois la hardiesse), se
    //     croit face a sa cible des ±0x80 (et non ±0xc8), et louvoie en virant DEUX fois sa hardiesse,
    //     pas huit ;
    //   - SES SORTS SONT DE FEU ET DE GLACE : variante 0, Feu d'enfer, Mur de flammes, Congelo,
    //     Assaut magique ; variante 1, Coup de jus, Lumiere, Boule de feu. Incantation sur
    //     l'animation 0, lancer sur la 3, depart a l'image 9 ;
    //   - ET IL A UNE TREIZIEME POSTURE : LA POSTURE 12, L'INVOCATION DU NUAGE. Sur l'animation 3, a
    //     l'image 9, il fait naitre un effet de sous-type 0xdf — par SpawnEffectEntity, donc dans
    //     la table du decor : le creneau 31 de 0x0056b0c8, les nuages du chateau (0x004271B0,
    //     0x0042B190, Effects/ImmortalCastleParticles.cs). Un seul vampire a la fois : si une autre
    //     entite de scene est deja en posture 12, il renonce. C'est la posture 12 qui remplace le
    //     bond de pres et le sort de mi-distance du Rampant dans la replanification.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552c10</c> et <c>&amp;DAT_00552c18</c> — <b>les sorts du Vampire</b>,
    /// releves dans <c>.data</c> : <c>0004 000a 000c 000e</c> puis <c>000b 000f 0014 000b 000f</c>,
    /// et des zeros jusqu'a <c>0x00552c28</c>, la table de l'Ombre. Variante 0 : <c>hasard &amp; 3</c>
    /// signe — Feu d'enfer, Mur de flammes, Congelo, Assaut magique. Variante 1 : <c>hasard % 5</c>
    /// — Coup de jus, Lumiere, Boule de feu, puis Coup de jus et Lumiere encore : deux chances sur
    /// cinq pour chacun des deux premiers.
    /// </summary>
    private static readonly ushort[] s_VampireSpells_00552C10 = [0x0004, 0x000a, 0x000c, 0x000e];
    private static readonly ushort[] s_VampireSpells_00552C18 = [0x000b, 0x000f, 0x0014, 0x000b, 0x000f];

    /// <summary>
    /// Ghidra: <c>0x004cf3a0</c> (et sa copie <c>0x004d8f70</c>) — <b>l'intelligence du
    /// Vampire</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004CF3A0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde, celle du Rampant et la premiere du Pourpre preux.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 1, 0);   // Ghidra: FUN_004cefe0 (FUN_004d5a20), copies de FUN_004a1020
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

        // Ghidra: 0x004cf4b5 — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideVampireAction_004CF500(e, probe, boldness, distance, heading, speed);

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

        ReplanVampireAction_004CFE04(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004cf500..0x004cfbcd</c> — <b>le premier temps</b> du Vampire. Treize cases,
    /// dont neuf servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 6 bondir, 7 marcher
    /// droit, 10 le sort, 11 le coup, 12 l'invocation du nuage. Les cases 0, 6, 7 et 11 sont celles
    /// du Rampant au mot pres.
    /// </summary>
    private static void DecideVampireAction_004CF500(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le coup, a l'image 9
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 9)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 9)
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

                // Ghidra: 0x004cf678 — face a la cible des ±0x80 (le Rampant : ±0xc8).
                if (((heading + 0x80) & 0xfff) <= 0x190)
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

                // Ghidra: 0x004cf750 — une fois la hardiesse (le Rampant : deux fois).
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness + 0x80
                    : -0x80 - boldness);
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

                    // Ghidra: 0x004cf7fb — deux fois la hardiesse (le Rampant : huit).
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
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
                VampireCast_004CF92E(e);
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

            // ---------------------------------------------------------- 12 : le nuage
            case 12:
                VampireSummonCloud_004CFAD4(e, distance);
                return;

            // ---------------------------------------------------------- 4, 5, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004cf92e..0x004cfa98</c> — <b>le sort</b>, posture 10 : l'ossature de
    /// <see cref="CrawlerCast_004AE07F"/> (Rampant), aux animations et a l'image pres —
    /// incantation sur l'animation <b>0</b>, lancer sur la <b>3</b>, depart a l'image <b>9</b> — et
    /// avec les listes du Vampire.
    ///
    /// <para><b>Le registre non charge</b>, comme chez le Rampant : au-dela de la variante 1, le
    /// mot du sort est le compteur de la boucle de refus, <c>0x20</c>.</para>
    /// </summary>
    private static void VampireCast_004CF92E(byte* e)
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
                    e[0x43] = 0;                                    // Ghidra: LAB_004cfbcb
                    return;
                }
            }

            uint flags = *(uint*)e;
            if ((flags & 0x804) != 0)
            {
                *(uint*)e = flags & 0xfffff7fb;
                e[0x43] = 0;
                return;
            }

            SpawnCastEntity_0047AAE0(e, 0xbd, 0, -1);
            *(short*)(e + 0x12) = 1;

            ushort spell = (ushort)scanned;                         // 32 : le compteur, pas un sort
            if (*(short*)(e + 0x16) == 0) spell = s_VampireSpells_00552C10[SignedMask(CrtRand(), 3)];
            if (*(short*)(e + 0x16) == 1) spell = s_VampireSpells_00552C18[CrtRand() % 5];

            *(ushort*)(e + 6) = spell;

            byte* sheet = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)
                                    + ((spell >> 8) & 0xff) * 4)
                          + (spell & 0xff) * 0x30;
            *(ushort*)(e + 0x10) = sheet[0x13];

            *(uint*)e |= 0x1000804;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x18) = 0;
            *(short*)(e + 0x1a) = 0;
        }

        if (*(short*)(e + 0x12) == 1)
        {
            (*(short*)(e + 0x10))--;
            if (*(short*)(e + 0x10) < 1)
            {
                *(short*)(e + 0x12) = 2;
                *(uint*)e &= 0xfefffffb;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 0;
            }
        }

        if (*(short*)(e + 0x12) == 2 && *(short*)(e + 0x1a) == 9)
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
    /// Ghidra: <c>0x004cfad4..0x004cfbcd</c> — <b>l'invocation du nuage</b>, posture 12, le geste
    /// que le Rampant n'a pas.
    ///
    /// <list type="table">
    ///   <item><term>armer</term><description>il renonce si un effet du decor
    ///     (<c>0x005ff400</c>) porte deja son nom en <c>+0x6c</c>, <b>ou si une autre entite de scene
    ///     est deja en posture 12</b> — le balayage lit l'octet <c>+0x42</c> des 32 entites de
    ///     <c>0x005ecaa0</c> et ne s'excepte que lui-meme, par son rang <c>+0x08</c>. Sinon :
    ///     animation 3, bits <c>0x1000000</c> et <c>0x2000000</c> leves, a l'arret.</description></item>
    ///   <item><term>invoquer</term><description>au-dela de 1000 unites de sa cible, il renonce. A
    ///     l'image 9, il pose sa cible <c>+0x6c</c> en <c>+0x78</c>, choisit le mode du nuage en
    ///     <c>+0x9a</c> — variante 0 : 0 ou 2 ; variante 1 : 1 ou 2 — et fait naitre l'effet
    ///     <c>0xdf</c> par <see cref="SpawnEffectEntity"/>, qui copie l'enregistrement : le nuage nait
    ///     avec ce mode et cette cible. Puis il rend a <c>+0x9a</c> sa valeur. Le mode 2 est le
    ///     nuage a effet 32 ; 0 ou 1, la gerbe (voir <c>StepCastleCloudBurst</c>).</description></item>
    ///   <item><term>fin</term><description>quand l'animation s'acheve.</description></item>
    /// </list>
    /// </summary>
    private static void VampireSummonCloud_004CFAD4(byte* e, int distance)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            byte* effects = Mem.Entity(0x005ff400);
            for (int i = 0; i < 0x20; i++)
            {
                byte* effect = effects + i * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004cfbcb
                    return;
                }
            }

            // Ghidra: 0x004cfb00 — `mov ecx,5ECAE2h`, soit 0x005ecaa0 + 0x42 : la posture de chaque
            // entite de scene. Le rang compare est edx, l'indice de la boucle.
            byte* scene = Mem.Entity(0x005ecaa0);
            for (int i = 0; i < 0x20; i++, scene += 0x9c)
            {
                if (scene[0x42] == 0x0c && i != e[8])
                {
                    e[0x43] = 0;
                    return;
                }
            }

            *(short*)(e + 0x18) = 3;
            *(uint*)e &= 0xfcffffff;
            *(short*)(e + 0x1a) = 0;
            *(short*)(e + 0x12) = 1;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x12) != 1) return;

        if (distance > 1000)
        {
            e[0x43] = 0;
            return;
        }

        if (*(short*)(e + 0x1a) == 9)
        {
            short mode = *(short*)(e + 0x9a);

            if (*(short*)(e + 0x16) == 0) *(short*)(e + 0x9a) = (short)(SignedMask(CrtRand(), 1) << 1);   // Ghidra: FUN_00547140
            if (*(short*)(e + 0x16) == 1) *(short*)(e + 0x9a) = (short)(SignedMask(CrtRand(), 1) + 1);

            *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);
            SpawnEffectEntity(e, 0xdf, 0, -1);                      // Ghidra: FUN_0047a9d0
            *(short*)(e + 0x9a) = mode;
        }

        if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004cfe04..0x004d0178</c> — <b>le troisieme temps</b> du Vampire, les trois
    /// plages du Rampant (<c>0xfa</c>, <c>0x258</c>) avec trois ecarts :
    ///
    /// <list type="bullet">
    /// <item><b>de pres</b>, les tirages de 60 a 69 ne bondissent plus : ils <b>invoquent le
    /// nuage</b> (posture 12), a la meme condition — la cible au-dela de <c>hardiesse + 100</c> ;</item>
    /// <item><b>a mi-distance</b>, le sort face a la cible ne part que sur les tirages sous 15 (le
    /// Rampant : 20), et les hauts tirages hardis invoquent le nuage au lieu du sort ;</item>
    /// <item><b>de loin</b>, les tirages de 60 a 79 approchent toujours : le Rampant y glissait un
    /// sort, le Vampire n'a plus rien d'autre.</item>
    /// </list>
    /// </summary>
    private static void ReplanVampireAction_004CFE04(byte* e, int boldness, int timid)
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
                e[0x42] = 0x0c;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                StrafeSalamander(e, timid);                         // Ghidra: LAB_004d013c
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x0f)
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
                e[0x42] = 0x0c;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                StrafeSalamander(e, timid);                         // Ghidra: LAB_004d0000
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
            StrafeSalamander(e, timid);                             // Ghidra: LAB_004d008d
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
