// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA CHIMERE — 0x004D2FC0, chateau de l'immortel (zone 6, sous-zones 6 a 10), et sa copie
// 0x004DCF10, ruines souterraines (zone 7, sous-zones 0 a 2).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004D2FC0 — LA CHIMERE. Transcrite le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004D2FC0.asm, 1 130 instructions, absente de Blaze.c),
    // table de sauts a QUINZE entrees a 0x004D3E9C.
    //
    // DEUX CORPS D'UN COUP. 0x004DCF10 (ruines souterraines, sous-zones 0 a 2) est la meme emission
    // a la repartition des registres pres sur le bloc d'integration, et aux DEUX adresses de donnees
    // qu'elle porte : 0x00552CD8 / 0x00552CE0 au lieu de 0x00552C50 / 0x00552C58. Les deux paires
    // portent les memes octets — `0a00 0c00 0b00 0d00` puis `1500` : les deux Chimeres lancent
    // exactement la meme chose.
    //
    // CE QU'ELLE A DE PROPRE, contre le Vampire et la Sacuba qui partagent sa table de sauts :
    //
    //   - QUINZE POSTURES, ONZE SERVIES. Aux neuf habituelles s'ajoutent la POSTURE 5, un geste nu
    //     sur l'animation 6 — c'est elle que la tete signale en levant le bit 0x10 du mot d'etat,
    //     le seul corps du chateau a le faire —, et la POSTURE 14, le RECUL : elle glisse a
    //     cap + 0x800, donc a reculons, sans jamais toucher a sa poussee +0x5c ;
    //   - ELLE N'A PAS DE CHERCHEUR DE PROIE. Aucun appel a FUN_004a1020, aucune mise en place de
    //     +0x41 : elle prend la cible que l'eveil commun lui a laissee en +0x6c et n'en change
    //     jamais ;
    //   - SON COUP FRAPPE A L'IMAGE 23, et il n'a pas de queue : le bit 1 tombe dans le bloc du
    //     coup, pas dans un bloc commun apres la garde ;
    //   - DEUX SORTS, ET ILS NE PARTENT PAS PAREIL. La posture 10 en tire un des quatre de la table
    //     (10, 12, 11, 13 de la premiere famille), et pendant l'incantation ELLE MONTE : sa hauteur
    //     rattrape par huitiemes celle de sa cible plus 0x14000. Depart a l'image 25. La posture 12
    //     lance le sort FIXE 0x0015 — la meme fiche que la Harpie — sans incantation ni animation de
    //     lancer, sitot qu'un geste s'est acheve ;
    //   - LA POSTURE 11 N'EST PAS UN COUP MAIS UNE CHARGE : la poussee part a 0xf000 et perd 0x1000
    //     par image jusqu'a zero ;
    //   - SON BOND EST TROIS FOIS PLUS FORT (hardiesse + 0x280, contre + 0x100) ;
    //   - SA PLAGE MOYENNE VA JUSQU'A 800 unites, et non 600.
    //
    // ET ELLE RETIRE SON PLAN. Trois sites de la replanification (0x004d3bf3, 0x004d3c69,
    // 0x004d3cc3) refusent de retomber en posture 2 quand elle est DEJA en posture 14 : ils sautent
    // a 0x004d3cc9, qui relance toute la replanification depuis le debut tant que +0x43 vaut zero.
    // C'est une vraie boucle dans le binaire, pas un artefact — transcrite comme telle.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552c50</c> — <b>les sorts de la Chimere</b>, quatre mots releves dans
    /// <c>.data</c> : <c>000a 000c 000b 000d</c>, tires par <c>hasard &amp; 3</c> signe. Premiere
    /// famille, fiches 10, 12, 11 et 13.
    /// </summary>
    private static readonly ushort[] s_ChimeraSpells_00552C50 = [0x000a, 0x000c, 0x000b, 0x000d];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552c58</c> — <b>le sort fixe de la posture 12</b>, lu comme un dword
    /// puis tronque a seize bits : <c>0x0015</c>, premiere famille, fiche 21. C'est le meme mot que
    /// celui de la Harpie (<c>0x00552c08</c> et ses copies).
    ///
    /// <para>La copie des ruines le lit a <c>0x00552ce0</c> ; meme valeur.</para>
    /// </summary>
    private const ushort c_ChimeraFixedSpell_00552C58 = 0x0015;

    /// <summary>
    /// Ghidra: <c>0x004d2fc0</c> (et sa copie <c>0x004dcf10</c>) — <b>l'intelligence de la
    /// Chimere</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004D2FC0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x2c] — la sonde. Plus large devant que celle du Vampire, et aussi haute.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x32; probe[2] = 0x50; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        // Ghidra: 0x004d3055 — `mov edi,[esi+6Ch]`, et c'est tout : la Chimere ne choisit jamais sa
        // cible, elle prend celle que l'eveil lui a laissee.
        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004d30be — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideChimeraAction_004D310B(e, chased, probe, boldness, distance, heading, speed);
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

        ReplanChimeraAction_004D3AD4(e, chased, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004d310b..0x004d3896</c> — <b>le premier temps</b> de la Chimere. Quinze cases,
    /// dont onze servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, <b>5 le geste nu</b>,
    /// 6 bondir, 7 marcher droit, 10 le sort monte, 11 la charge, 12 le sort fixe,
    /// <b>14 reculer</b>. Les cases 4, 8, 9 et 13 vont a la sortie commune.
    /// </summary>
    private static void DecideChimeraAction_004D310B(
        byte* e, byte* chased, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // Ghidra: 0x004d3171 — le bit 0x10 suit la posture 5, le geste nu.
        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le coup, a l'image 23
        if ((e[0] & 2) != 0 && *(short*)(e + 0x1a) == 0x17)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x17)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x10;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

                // Ghidra: 0x004d31e6 — le bit 1 tombe ici, et pas dans un bloc commun apres la
                // garde comme chez le Vampire et la Sacuba.
                *(uint*)e &= 0xfffffffd;
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
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004d3894
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

                // Ghidra: 0x004d329d — hardiesse + 0x50 (le Vampire : + 0x80). Elle vire lentement.
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x50 : -0x50 - boldness);
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

            // ---------------------------------------------------------- 5 : le geste nu
            case 5:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1 && e[0x43] != 0 && (*(uint*)e & 0x2000000) != 0) e[0x43]--;

                if (phase != 0) return;

                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x18) = 6;
                return;
            }

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

                        *(short*)(e + 0x18) = 3;
                        *(short*)(e + 0x1a) = 0;

                        // Ghidra: 0x004d34ad — hardiesse + 0x280 (le Vampire : + 0x100).
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

                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;       // Ghidra: LAB_004d3894
                return;
            }

            // ---------------------------------------------------------- 10 : le sort monte
            case 10:
                ChimeraRisingCast_004D357F(e, chased);
                return;

            // ---------------------------------------------------------- 11 : la charge
            case 11:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 1)
                {
                    // Ghidra: 0x004d36f1 — la poussee perd 0x1000 par image et s'arrete a zero.
                    int thrust = *(int*)(e + 0x5c) - 0x1000;
                    *(int*)(e + 0x5c) = thrust;
                    if (thrust < 0) *(int*)(e + 0x5c) = 0;

                    uint f = *(uint*)e;
                    if ((f & 0x2000000) != 0)
                    {
                        *(uint*)e = f & 0xfffffffd;
                        e[0x43] = 0;
                    }
                }

                if (phase != 0) return;

                *(int*)(e + 0x5c) = 0xf000;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;
            }

            // ---------------------------------------------------------- 12 : le sort fixe
            case 12:
                ChimeraFixedCast_004D3748(e, boldness, heading);
                return;

            // ---------------------------------------------------------- 14 : reculer
            case 14:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    // Ghidra: 0x004d3820 — cap + 0x800, soit exactement a reculons. C'est le seul
                    // deplacement du corps qui ne passe pas par la poussee +0x5c.
                    int back = *(short*)(e + 0x32) + 0x800;
                    int sin = (int)Winsys3d.GetSinTableEntry(back);
                    int cos = Winsys3d.GetCosTableEntry(back);

                    int step = (((boldness + 0x60) * speed) << 8) >> 0xc;

                    *(int*)(e + 0x1c) += (step * sin) >> 0xc;
                    *(int*)(e + 0x24) += (step * cos) >> 0xc;
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 4, 8, 9, 13 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004d357f..0x004d36e6</c> — <b>le sort monte</b>, posture 10. L'ossature
    /// habituelle — le balayage des 32 effets, le refus sur <c>0x804</c>, la naissance de l'entite
    /// d'incantation <c>0xbd</c> — avec deux choses en propre :
    ///
    /// <list type="bullet">
    ///   <item><b>elle monte pendant l'incantation</b> : a chaque image sa hauteur rattrape par
    ///     huitiemes celle de sa cible plus <c>0x14000</c>. C'est le seul endroit du corps qui lit
    ///     la position de la cible ;</item>
    ///   <item>incantation sur l'animation <b>4</b>, lancer sur la <b>5</b>, depart a l'image
    ///     <b>25</b>.</item>
    /// </list>
    /// </summary>
    private static void ChimeraRisingCast_004D357F(byte* e, byte* chased)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            byte* effects = Mem.Entity(0x005ff400);

            for (int i = 0; i < 0x20; i++)
            {
                byte* effect = effects + i * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004d3e92
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

            ushort spell = s_ChimeraSpells_00552C50[SignedMask(CrtRand(), 3)];
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
            // Ghidra: 0x004d364d — la montee, avant le decompte.
            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = y + ((*(int*)(chased + 0x20) - y + 0x14000) >> 3);

            (*(short*)(e + 0x10))--;
            if (*(short*)(e + 0x10) < 1)
            {
                *(short*)(e + 0x12) = 2;
                *(uint*)e &= 0xfefffffb;
                *(short*)(e + 0x18) = 5;
                *(short*)(e + 0x1a) = 0;
            }
        }

        if (*(short*)(e + 0x12) == 2 && *(short*)(e + 0x1a) == 0x19)
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
    /// Ghidra: <c>0x004d3748..0x004d3808</c> — <b>le sort fixe</b>, posture 12. Elle prend
    /// l'animation 4, se tourne vers sa cible tant qu'elle la tient, et <b>des que l'animation
    /// s'acheve</b> lance <see cref="c_ChimeraFixedSpell_00552C58"/> — sans entite d'incantation,
    /// sans animation de lancer, sans decompte.
    ///
    /// <para>Le virage de cette posture est <b>la moitie</b> de celui de la posture 1 :
    /// <c>hardiesse / 2 + 0x32</c>, dans le meme cone de ±0xc8.</para>
    /// </summary>
    private static void ChimeraFixedCast_004D3748(byte* e, int boldness, int heading)
    {
        if (*(short*)(e + 0x12) == 2)
        {
            ushort spell = c_ChimeraFixedSpell_00552C58;
            *(ushort*)(e + 6) = spell;
            CastSpell_0053A980((uint*)e, (spell >> 8) & 0xff, spell & 0xff, -1, -1);
            e[0x43] = 0;
        }

        if (*(short*)(e + 0x12) == 1)
        {
            if ((*(uint*)e & 0x2000000) != 0) *(short*)(e + 0x12) = 2;

            if (((heading + 0xc8) & 0xfff) > 0x190)
            {
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? (boldness >> 1) + 0x32
                    : -0x32 - (boldness >> 1));
            }
        }

        if (*(short*)(e + 0x12) != 0) return;

        *(int*)(e + 0x5c) = 0;
        *(short*)(e + 0x1a) = 0;
        *(short*)(e + 0x18) = 4;
        *(uint*)e &= 0xfeffffff;
        *(short*)(e + 0x12) = 1;
    }

    /// <summary>
    /// Ghidra: <c>0x004d3ad4..0x004d3e9a</c> — <b>le troisieme temps</b> de la Chimere, et le seul
    /// du depot qui <b>boucle</b> : les trois sites marques <c>LAB_004d3cc9</c> refusent de la
    /// remettre en posture 2 quand elle est deja en posture 14, et relancent le tirage depuis le
    /// debut tant que <c>+0x43</c> vaut zero — ce qu'il vaut toujours a l'entree, puisque l'appelant
    /// ne replanifie qu'a decompte nul. La boucle sort des qu'un autre tirage tombe.
    ///
    /// <list type="bullet">
    /// <item><b>de pres</b> (&lt; 250) : 0-49 la charge ou le geste nu selon le cone, 50-59 le
    ///   recul si elle est collee, 60-69 le recul si la cible est loin, 70-99 l'approche ;</item>
    /// <item><b>a mi-distance</b> (250 a 800, et non 600) : 0-19 le sort ou le demi-tour, 20-29
    ///   l'approche, 30-49 le recul, 50-89 le louvoiement, 90-99 le sort ;</item>
    /// <item><b>de loin</b> (&gt;= 800) : 0-29 le repos ou l'approche selon la hardiesse, 30-39 le
    ///   louvoiement, 40-59 le demi-tour, 60-79 l'approche, 80-99 le repos.</item>
    /// </list>
    /// </summary>
    private static void ReplanChimeraAction_004D3AD4(byte* e, byte* chased, int boldness, int timid)
    {
        while (true)
        {
            int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
            int bearing = Winsys3d.Ordinal_457(dx, dz);
            *(short*)(e + 0x12) = 0;
            int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

            // ============================================================ de pres
            if (distance < 0xfa)
            {
                int roll = CrtRand() % 100;

                if (roll < 0x32)
                {
                    if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                        && distance < CrtRand() % (timid * 2 + 1) + 100)
                    {
                        if (roll < 0x28)
                        {
                            e[0x42] = 0x0b;
                            e[0x43] = 1;
                            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                            *(int*)(e + 0x5c) = 0;
                            *(short*)(e + 0x18) = 5;
                            *(short*)(e + 0x1a) = 0;
                        }
                        else
                        {
                            e[0x42] = 0x0c;                         // Ghidra: LAB_004d3ba6
                            e[0x43] = 1;
                            *(short*)(e + 0x12) = 0;
                        }
                    }
                    else
                    {
                        e[0x42] = 1;                                // Ghidra: LAB_004d3bb9
                        e[0x43] = 1;
                    }
                }
                else if (roll < 0x3c)
                {
                    if (distance < 0x50)
                    {
                        e[0x42] = 0x0e;
                        e[0x43] = (byte)(CrtRand() % 0x14 + 0x14);
                    }
                    else
                    {
                        // Ghidra: LAB_004d3bf3 — deja en recul : on retire tout le plan.
                        if (e[0x42] == 0x0e)
                        {
                            if (e[0x43] == 0) continue;
                            return;
                        }

                        e[0x42] = 2;
                        int wait = CrtRand() % 10;
                        *(short*)(e + 0x10) = 0x50;
                        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
                    }
                }
                else if (roll < 0x46 && distance > boldness + 100)
                {
                    e[0x42] = 0x0e;                                 // Ghidra: 0x004d3c34
                    e[0x43] = (byte)(CrtRand() % 0x14 + 0x14);
                }
                else if (distance < 0x50)
                {
                    e[0x42] = 0x0e;                                 // Ghidra: LAB_004d3c4c
                    e[0x43] = (byte)(CrtRand() % 0x14 + 0x14);
                }
                else
                {
                    // Ghidra: LAB_004d3c69 — le meme refus qu'en 0x004d3bf3.
                    if (e[0x42] == 0x0e)
                    {
                        if (e[0x43] == 0) continue;
                        return;
                    }

                    e[0x42] = 2;
                    int wait = CrtRand() % 10;
                    *(short*)(e + 0x10) = 0x50;
                    e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
                }
            }

            // ============================================================ a moyenne distance
            if (distance < 0x320 && distance >= 0xfa)
            {
                int roll = CrtRand() % 100;

                if (roll < 0x14)
                {
                    if (((heading + 0x400) & 0xfff) < 0x800)
                    {
                        e[0x42] = 0x0a;                             // Ghidra: LAB_004d3d8b
                        e[0x43] = 1;
                        *(short*)(e + 0x12) = 0;
                    }
                    else
                    {
                        e[0x42] = 1;
                        e[0x43] = 1;
                    }
                }
                else if (roll < 0x1e)
                {
                    // Ghidra: 0x004d3cc3 — troisieme site du refus.
                    if (e[0x42] == 0x0e)
                    {
                        if (e[0x43] == 0) continue;
                        return;
                    }

                    e[0x42] = 2;                                    // Ghidra: LAB_004d3d08
                    int wait = CrtRand() % 10;
                    *(short*)(e + 0x10) = 0x50;
                    e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
                }
                else if (roll < 0x32)
                {
                    e[0x42] = 0x0e;                                 // Ghidra: LAB_004d3d2d
                    e[0x43] = (byte)(CrtRand() % 0x14 + 0x14);
                }
                else if (roll < 0x5a)
                {
                    StrafeSalamander(e, timid);                     // Ghidra: LAB_004d3d4a
                }
                else
                {
                    e[0x42] = 0x0a;                                 // Ghidra: LAB_004d3d8b
                    e[0x43] = 1;
                    *(short*)(e + 0x12) = 0;
                }
            }

            // ============================================================ de loin
            if (distance < 0x320) return;

            int far = CrtRand() % 100;

            if (far < 0x1e)
            {
                if (far >= boldness * 2 + 0x0a)
                {
                    e[0x42] = 0;                                    // Ghidra: LAB_004d3e5c
                    int rest = CrtRand() % 10;
                    *(short*)(e + 0x12) = 0;
                    e[0x43] = (byte)(rest + (timid >> 2));
                    *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                    return;
                }

                e[0x42] = 2;                                        // Ghidra: LAB_004d3e32
                int approach = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x64;
                e[0x43] = (byte)(approach + (timid >> 1) + 0x14);
                return;
            }

            if (far < 0x28)
            {
                StrafeSalamander(e, timid);                         // Ghidra: LAB_004d3dd2
                return;
            }

            if (far < 0x3c)
            {
                e[0x42] = 1;                                        // Ghidra: LAB_004d3e15
                e[0x43] = 1;
                return;
            }

            if (far < 0x50)
            {
                e[0x42] = 2;                                        // Ghidra: LAB_004d3e32
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x64;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
                return;
            }

            e[0x42] = 0;                                            // Ghidra: LAB_004d3e5c
            int idle = CrtRand() % 10;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(idle + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }
    }
}
