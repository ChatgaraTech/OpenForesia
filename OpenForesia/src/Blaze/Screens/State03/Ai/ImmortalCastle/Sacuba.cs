// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA SACUBA — 0x004D9D90, chateau de l'immortel (zone 6, sous-zones 5 et 11), et ses TROIS copies :
// 0x004D3EE0 (chateau, sous-zones 6 a 10), et dans la grotte scellee (zone 10) 0x004F40A0
// (sous-zones 5, 8, 10) et 0x004FBA80 (sous-zones 6, 7).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004D9D90 — LA SACUBA. Transcrite le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004D9D90.asm, 1 126 instructions, absente de Blaze.c),
    // table de sauts a TREIZE entrees a 0x004DAC40.
    //
    // QUATRE CORPS D'UN COUP, et le tri en annoncait deux. Les quatre creneaux ne different qu'a
    // TROIS endroits, tous au meme rang d'instruction :
    //
    //   - l'appel 22, le chercheur de proie : 0x004D5A20 / 0x004EF860 / 0x004CEFE0 / 0x004F9230,
    //     quatre emissions de FUN_004A1020 (PickChasedPartyMember_004A1020) ;
    //   - l'appel 206, l'effet d'etat du coup : 0x004DAC80 / 0x004F4F90 / 0x004D4DD0 / 0x004FC970,
    //     quatre emissions du meme enrobage de quinze lignes, identique a FUN_00492E00
    //     (ApplyStatusEffectIfAllowed_00492E00, deja ecrit pour le Scorpion) ;
    //   - la table de sorts : 0x00552CA0 / 0x00552F40 / 0x00552C60 / 0x00552FE0, dont les trente
    //     octets utiles sont IDENTIQUES octet pour octet. Les quatre Sacuba lancent la meme chose.
    //
    // Les « 1 129 instructions » et les « 18 lignes d'ecart » du tri etaient du bourrage et la table
    // de sauts decodes au-dela du dernier `ret` ; corps contre corps, l'ecart est de zero ligne de
    // logique, a la repartition des registres pres sur deux blocs de douze instructions.
    //
    // LA CHARPENTE DU VAMPIRE (Ai/ImmortalCastle/Vampire.cs), MEME TABLE DE SAUTS ET MEMES NEUF
    // POSTURES SERVIES (0, 1, 2, 3, 6, 7, 10, 11, 12) : meme sonde, meme lecture de situation, meme
    // pose au sol et meme heurt de mur au masque 0x2000ad82, meme balayage des 32 effets et des 32
    // entites de scene avant d'invoquer. Ce qui fait la Sacuba :
    //
    //   - ELLE VOLE. C'est le seul corps du chateau a porter son flottement en ligne : un mot de
    //     phase en +0x14, seme au hasard sur douze bits a sa PREMIERE image, avance de 70 a 99 par
    //     image, et qui la fait monter et descendre au-dessus du plancher +0x60 tant qu'elle tient
    //     l'animation 1. Voir HoverSacuba_004DA61F, et les deux consequences ailleurs :
    //     l'integration amortit son +0x9a de MOITIE (et pas du tout sur l'animation 1), et la pose
    //     au sol ne la declare jamais en l'air tant qu'elle plane ;
    //   - SON COUP FRAPPE A L'IMAGE 13, pas 9, et pour 3 de clignotement, pas 0x10 ;
    //   - ELLE LANCE UNE SEULE LISTE, QUINZE TIRAGES, sans variante : incantation sur l'animation
    //     5, lancer sur la 6, depart a l'image 9 ;
    //   - SON NUAGE EST LE SOUS-TYPE 0xdb, pas 0xdf, et son mode ne vaut que 0 ou 2 — la variante 1
    //     garde le mode qu'elle porte deja ;
    //   - ELLE BONDIT SUR L'ANIMATION 4, et non la 1 ;
    //   - ELLE EST BEAUCOUP PLUS PRESSEE DE LANCER : de pres, trente tirages sur cent vont droit au
    //     sort la ou le Vampire louvoie ; a mi-distance, le creneau du bond du Vampire est encore
    //     un sort.
    //
    // LE SECOND COUP NE PART JAMAIS (a confirmer en jeu, voir Documentation/Reference/legacy-bug.md).
    // Le bloc de l'image 19, qui frappe pour 0x10 puis tente l'effet d'etat une fois sur trois, est
    // ecrit SOUS la garde « image 13 » (0x004d9f79, `jne` vers 0x004da03e). Rien entre les deux ne
    // touche +0x1a : ni ResolveMeleeHit_00539B80 (verifie dans Blaze.c), ni le son. Le bloc est donc
    // mort dans le binaire vendu, et avec lui le seul moyen qu'a la Sacuba d'appliquer un effet
    // d'etat au corps a corps. Transcrit tel quel, garde comprise.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552ca0</c> — <b>les sorts de la Sacuba</b>, quinze mots releves dans
    /// <c>.data</c> : <c>0014 0011 0014 0011 0102 0103 0200 0207 0208 0200 0207 0208 0200 0207
    /// 0208</c>. Octet haut la famille, octet bas la fiche, exactement ce que
    /// <see cref="CastSpell_0053A980"/> recoit. La borne est le <c>idiv 0Fh</c> qui precede la
    /// lecture : quinze mots, trente octets, et les trente octets sont les memes aux quatre
    /// adresses (<c>0x00552c60</c>, <c>0x00552f40</c>, <c>0x00552fe0</c>).
    ///
    /// <para>Huit sorts pour quinze tirages : <b>Dodo</b>, <b>Chant sirenes</b> et <b>Ecrase</b>
    /// trois fois chacun, <b>Boule de feu</b> et <b>Trouille</b> deux fois, <b>Renfort</b> et
    /// <b>Gnoukoule</b> une fois. Deux sur trois viennent de la troisieme famille.</para>
    /// </summary>
    private static readonly ushort[] s_SacubaSpells_00552CA0 =
    [
        0x0014, 0x0011, 0x0014, 0x0011, 0x0102, 0x0103, 0x0200, 0x0207,
        0x0208, 0x0200, 0x0207, 0x0208, 0x0200, 0x0207, 0x0208,
    ];

    /// <summary>
    /// Ghidra: <c>0x004d9d90</c> (et ses copies <c>0x004f40a0</c>, <c>0x004d3ee0</c>,
    /// <c>0x004fba80</c>) — <b>l'intelligence de la Sacuba</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004D9D90(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde. Celle du Vampire est {0, 0x1e, 0x32, 0x64, 0, 0x64} : la
        // Sacuba porte moins loin devant et moins haut.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x14; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x32;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 6, 0);   // Ghidra: FUN_004d5a20, copie de FUN_004a1020

            // Ghidra: 0x004d9de1 — la phase du flottement, semee au hasard sur douze bits signes.
            // C'est ce qui empeche deux Sacuba d'un meme groupe de monter et descendre ensemble.
            *(short*)(e + 0x14) = (short)SignedMask(CrtRand(), 0xfff);
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

        // Ghidra: 0x004d9ebc — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideSacubaAction_004D9F07(e, probe, boldness, distance, heading, speed);
            HoverSacuba_004DA61F(e);
        }

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSacubaMotion_004DA661(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep, hoverAnimation: 1);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanSacubaAction_004DA8B0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004d9f07..0x004da61a</c> — <b>le premier temps</b> de la Sacuba. Treize cases,
    /// dont neuf servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 6 bondir, 7 marcher
    /// droit, 10 le sort, 11 le coup, 12 l'invocation du nuage. Les cases 0, 3 et 11 sont celles du
    /// Vampire au mot pres ; les autres changent d'animation ou de poussee.
    ///
    /// <para><b>Elle n'a pas le bloc de posture 5 du Vampire</b> : le bit <c>0x10</c> du mot d'etat
    /// n'est ni leve ni baisse ici.</para>
    /// </summary>
    private static void DecideSacubaAction_004D9F07(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ le coup, a l'image 13
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 13)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 13)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 3;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }

                // Ghidra: LAB_004d9fda — LE SECOND COUP, A L'IMAGE 19. Il est sous la garde
                // « image 13 » ci-dessus, et rien entre les deux ne touche +0x1a : il ne part
                // jamais. Voir le bandeau. Transcrit tel quel.
                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x13)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

                    // Ghidra: 0x004da032 — FUN_004dac80, emission de FUN_00492e00.
                    if (CrtRand() % 100 < 0x21)
                    {
                        ApplyStatusEffectIfAllowed_00492E00((uint*)e, (uint*)prey, 0);
                    }
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

                // Ghidra: 0x004da0e1 — face a la cible des ±0xc8 (le Vampire : ±0x80).
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
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;   // le Vampire : 0x5000
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

                    int thrust = boldness * 0x90 + 0x6000;          // le Vampire : 0x5000
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

            // ---------------------------------------------------------- 6 : bondir
            case 6:
                if (*(short*)(e + 0x12) == 0)
                {
                    uint f = *(uint*)e;

                    // Ghidra: 0x004da2c4 — pose sans condition, puis repose dans la branche en
                    // l'air. Le Vampire ne l'ecrit qu'une fois.
                    e[0x43] = 0;

                    if ((f & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        *(uint*)e = f & 0xfeffffff;

                        *(short*)(e + 0x18) = 4;                    // le Vampire : animation 1
                        *(short*)(e + 0x1a) = 0;

                        int thrust = (boldness + 0x100) << 6;
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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;       // le Vampire : 0x5000
                return;
            }

            // ---------------------------------------------------------- 10 : le sort
            case 10:
                SacubaCast_004DA3B4(e);
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
                SacubaSummonCloud_004DA53C(e, distance);
                return;

            // ---------------------------------------------------------- 4, 5, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004da3b4..0x004da503</c> — <b>le sort</b>, posture 10 : l'ossature de
    /// <see cref="VampireCast_004CF92E"/>, sans la variante — <b>une seule liste de quinze
    /// tirages</b> — et avec ses animations : incantation sur la <b>5</b>, lancer sur la <b>6</b>,
    /// depart a l'image <b>9</b>.
    ///
    /// <para>Le Vampire laissait passer le compteur de sa boucle de refus comme mot de sort quand
    /// aucune variante ne repondait ; ici le mot vient toujours de la table, et le registre non
    /// charge n'existe pas.</para>
    /// </summary>
    private static void SacubaCast_004DA3B4(byte* e)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            byte* effects = Mem.Entity(0x005ff400);

            for (int i = 0; i < 0x20; i++)
            {
                byte* effect = effects + i * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004da61b
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

            ushort spell = s_SacubaSpells_00552CA0[CrtRand() % 15];   // Ghidra: FUN_00547140
            *(ushort*)(e + 6) = spell;

            byte* sheet = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)
                                    + ((spell >> 8) & 0xff) * 4)
                          + (spell & 0xff) * 0x30;
            *(ushort*)(e + 0x10) = sheet[0x13];

            *(uint*)e |= 0x1000804;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x18) = 5;
            *(short*)(e + 0x1a) = 0;
        }

        if (*(short*)(e + 0x12) == 1)
        {
            (*(short*)(e + 0x10))--;
            if (*(short*)(e + 0x10) < 1)
            {
                *(short*)(e + 0x12) = 2;
                *(uint*)e &= 0xfefffffb;
                *(short*)(e + 0x18) = 6;
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
    /// Ghidra: <c>0x004da53c..0x004da61a</c> — <b>l'invocation du nuage</b>, posture 12 : celle du
    /// Vampire a deux valeurs pres.
    ///
    /// <list type="bullet">
    ///   <item>le sous-type seme est <c>0xdb</c>, et non <c>0xdf</c> ;</item>
    ///   <item>le mode <c>+0x9a</c> ne vaut que <b>2</b> (cinq fois sur dix) ou <b>0</b>, et
    ///     <b>seule la variante 0 le tire</b> : la variante 1 part avec le mode qu'elle porte
    ///     deja.</item>
    /// </list>
    ///
    /// <para>Le reste est identique : les deux balayages (les 32 effets de <c>0x005ff400</c>, puis
    /// l'octet <c>+0x42</c> des 32 entites de <c>0x005ecaa0</c> — une seule invocatrice a la fois,
    /// et elle ne s'excepte que par son rang <c>+0x08</c>), l'abandon au-dela de 1000 unites, la
    /// cible recopiee en <c>+0x78</c> et le mode rendu apres la naissance.</para>
    /// </summary>
    private static void SacubaSummonCloud_004DA53C(byte* e, int distance)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            byte* effects = Mem.Entity(0x005ff400);
            for (int i = 0; i < 0x20; i++)
            {
                byte* effect = effects + i * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004da61b
                    return;
                }
            }

            // Ghidra: 0x004da56a — `mov ecx,5ECAE2h`, soit 0x005ecaa0 + 0x42.
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

            // Ghidra: 0x004da5df — `setle` sur `hasard % 10 <= 4`, puis `dec`/`and 0xfe`/`add 2` :
            // 2 ou 0, jamais 1.
            if (*(short*)(e + 0x16) == 0)
            {
                *(short*)(e + 0x9a) = (short)(CrtRand() % 10 <= 4 ? 2 : 0);
            }

            *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);
            SpawnEffectEntity(e, 0xdb, 0, -1);                      // Ghidra: FUN_0047a9d0
            *(short*)(e + 0x9a) = mode;
        }

        if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004da61f..0x004da660</c> — <b>le flottement</b>, la seule chose que la Sacuba
    /// fait et que le Vampire ne fait pas. Il n'a lieu que sur l'<b>animation 1</b>.
    ///
    /// <para>Le mot <c>+0x14</c> est une phase de douze bits, semee au hasard a la naissance et
    /// avancee de <c>hasard % 30 + 70</c> par image. Sa hauteur voulue est
    /// <c>(sin(phase) + 0x2800) * 32</c> au-dessus du plancher <c>+0x60</c>, et elle en rattrape
    /// un huitieme par image — donc elle monte et descend sans jamais l'atteindre.</para>
    /// </summary>
    private static void HoverSacuba_004DA61F(byte* e)
    {
        if (*(short*)(e + 0x18) != 1) return;

        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14));   // Ghidra: FUN_00545a1c
        int target = ((lift + 0x2800) << 5) + *(int*)(e + 0x60);

        int wobble = CrtRand() % 0x1e;                                    // Ghidra: FUN_00547140
        int y = *(int*)(e + 0x20);

        *(short*)(e + 0x14) += (short)(wobble + 0x46);
        *(int*)(e + 0x20) = y + ((target - y) >> 3);
    }

    /// <summary>
    /// Ghidra: <c>0x004da661..0x004da71e</c> — <b>l'integration</b>, celle de
    /// <see cref="IntegrateSceneEntityMotion_00488160"/> avec le seul axe vertical change, et
    /// aiguillee par l'animation :
    ///
    /// <list type="bullet">
    ///   <item><b>animation 1</b> (elle plane) : la vitesse verticale s'amortit comme les deux
    ///     autres, <c>vy - (vy &gt;&gt; 8) - (vy &gt;&gt; 3)</c>, et le <c>+0x9a</c> ne pese
    ///     rien — c'est <see cref="HoverSacuba_004DA61F"/> qui tient sa hauteur ;</item>
    ///   <item><b>toute autre animation</b> : le <c>+0x9a</c> pese, mais de <b>moitie</b> —
    ///     <c>(bob &gt;&gt; 9) - (vy &gt;&gt; 8) - (bob &gt;&gt; 1) + vy</c>, la ou le corps commun
    ///     ecrit <c>(bob &gt;&gt; 8) - (vy &gt;&gt; 8) - bob + vy</c>.</item>
    /// </list>
    /// </summary>
    private static void IntegrateSacubaMotion_004DA661(byte* e, int speed)
    {
        int course = *(short*)(e + 0x32);
        int sin = (int)Winsys3d.GetSinTableEntry(course);   // Ghidra: FUN_00545a1c, Ordinal_451
        int cos = Winsys3d.GetCosTableEntry(course);        // Ghidra: FUN_00545a16, Ordinal_450

        int thrust = (speed * *(int*)(e + 0x5c)) >> 0xc;

        int x = *(int*)(e + 0x1c) + ((thrust * sin) >> 0xc);
        int z = *(int*)(e + 0x24) + ((thrust * cos) >> 0xc);

        int vx = *(int*)(e + 0x50);
        int nvx = vx - (vx >> 8) - (vx >> 3);

        // Ghidra: 0x004da6a6 / 0x004da6b3 — le binaire pose deja x et z ici, puis les reecrit plus
        // bas avec la vitesse ajoutee. Garde tel quel.
        *(int*)(e + 0x1c) = x;
        *(int*)(e + 0x24) = z;
        *(int*)(e + 0x50) = nvx;

        int vy = *(int*)(e + 0x54);
        int nvy;

        if (*(short*)(e + 0x18) == 1)
        {
            nvy = vy - (vy >> 8) - (vy >> 3);
        }
        else
        {
            int bob = *(short*)(e + 0x9a);
            nvy = (bob >> 9) - (vy >> 8) - (bob >> 1) + vy;
        }

        *(int*)(e + 0x54) = nvy;

        int vz = *(int*)(e + 0x58);
        int nvz = vz - (vz >> 3) - (vz >> 8);
        *(int*)(e + 0x58) = nvz;

        *(int*)(e + 0x1c) = x + nvx;
        *(int*)(e + 0x20) += nvy;
        *(int*)(e + 0x24) = z + nvz;
    }

    /// <summary>
    /// Ghidra: <c>0x004da8b0..0x004dac3d</c> — <b>le troisieme temps</b> de la Sacuba, les trois
    /// plages du Vampire (<c>0xfa</c>, <c>0x258</c>) redistribuees en faveur du sort :
    ///
    /// <list type="bullet">
    /// <item><b>de pres</b>, 0-49 la charge (coup, attente ou demi-tour), 50-59 louvoient, 60-69
    ///   invoquent le nuage si la cible est au-dela de <c>hardiesse + 100</c> — sinon lancent — et
    ///   <b>70-99 lancent</b>. Le Vampire louvoyait sur tout ce haut de plage ;</item>
    /// <item><b>a mi-distance</b>, le creneau du bond du Vampire (40-69) est <b>encore un
    ///   sort</b> ; au-dela, le nuage sur les hardis, sinon le louvoiement ;</item>
    /// <item><b>de loin</b>, le creneau de l'approche (60-79) rend un <b>sort</b> quand
    ///   <c>100 - tirage &lt; hardiesse / 2 + 10</c>.</item>
    /// </list>
    /// </summary>
    private static void ReplanSacubaAction_004DA8B0(byte* e, int boldness, int timid)
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
            else if (roll < 0x3c)
            {
                StrafeSalamander(e, timid);                         // Ghidra: LAB_004da9b9
            }
            else if (roll < 0x46 && distance > boldness + 100)
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 0x0a;                                     // Ghidra: LAB_004daa16
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
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
                e[0x42] = 0x0a;                                     // Ghidra: LAB_004daaa6
                e[0x43] = 1;
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
                StrafeSalamander(e, timid);                         // Ghidra: LAB_004daad6
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
            StrafeSalamander(e, timid);                             // Ghidra: LAB_004dab68
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

            e[0x42] = 2;                                            // Ghidra: LAB_004dabf3
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
