// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'OMBRE — 0x004BFE90 (Labyrinthe des morts, sous-zones 5 a 8), L'OMBRE ROUGE — 0x004C5A30
// (sous-zones 4 et 9), et leurs deux copies de la grotte scellee, 0x004E7810 (sous-zone 0) et
// 0x004EC580 (sous-zones 2 et 3).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004BFE90 — L'OMBRE. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004BFE90.asm, 970 instructions, absente de Blaze.c), table de
    // sauts a onze entrees a 0x004C0B28.
    //
    // UN VOLANT INCANTATEUR — la charpente du Sorcier (Ai/OldTower/Sorcerer.cs), mais un fantome :
    //
    //   - IL SE POSE LE BIT 0x1000 A CHAQUE IMAGE, avant meme de sauvegarder son placement ;
    //   - IL SCINTILLE. Apres le flottement, les trois octets +0x38, +0x39 et +0x3a recoivent
    //     (sinus de la phase >> 6) + 100 — trois appels au sinus pour le meme argument ;
    //   - IL NE TOMBE JAMAIS : +0x54 est remis a zero a chaque image, il n'a pas de pose au sol, et
    //     un heurt lui rend AUSSI sa hauteur, ce que le Sorcier ne fait pas ;
    //   - SON FLOTTEMENT A TROIS CIBLES : en regime 0x20 hors incantation, +0x48 plus 0xa0 unites ;
    //     pendant l'incantation, +0x48 plus 0x8c unites et la phase FIGEE ; hors regime 0x20, le
    //     plancher +0x60 plus 0x8c unites ;
    //   - SA MARCHE DROITE pousse a ((hardiesse & ~7) + 0x20) * 512 — le seul corps de la zone a
    //     arrondir sa hardiesse vers le bas par huit ;
    //   - SON SORT EST TIRE DANS UNE LISTE DE DIX-SEPT : la variante 0 tire entre les deux premieres
    //     (Feu follet, Souffle glace), la variante 1 sur les dix-sept. AU-DELA DE 1, le registre
    //     d'indice n'est jamais charge et garde le compte de la boucle de refus, 32 — la lecture
    //     tombe alors seize mots apres la fin de la liste, et c'est LA SEULE CHOSE QUI DISTINGUE LES
    //     QUATRE COPIES : 0x0000 pour l'Ombre, 0x0200 pour l'Ombre rouge, 0x000d pour les deux de la
    //     grotte scellee. Leurs dix-sept mots utiles, eux, portent les memes octets.
    //
    // Hors cet unique mot, les quatre corps sont LOOSE-identiques : memes instructions a leurs
    // adresses pres. Ils ferment le groupe « 3 corps, 970 » du releve de doublons du 2026-09-02.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552b60</c> — <b>les sorts de l'Ombre</b>, dix-sept mots releves dans
    /// <c>.data</c> : huit fois <c>0006</c> / <c>0208</c>, puis <c>0018</c>. Les trois autres copies
    /// (<c>0x00552bb0</c>, <c>0x00552e58</c>, <c>0x00552eb8</c>) portent les memes octets.
    /// </summary>
    private static readonly ushort[] s_ShadowSpells_00552B60 =
    [
        0x0006, 0x0208, 0x0006, 0x0208, 0x0006, 0x0208, 0x0006, 0x0208,
        0x0006, 0x0208, 0x0006, 0x0208, 0x0006, 0x0208, 0x0006, 0x0208,
        0x0018,
    ];

    /// <summary>
    /// Ghidra: <c>0x004bfe90</c> (et <c>0x004c5a30</c>, <c>0x004e7810</c>, <c>0x004ec580</c>) —
    /// <b>l'intelligence de l'Ombre</b>. Voir le bandeau.
    /// </summary>
    /// <param name="strayCast">Le mot lu seize places apres la liste de sorts quand la variante
    /// depasse 1 — la seule difference entre les quatre copies. Voir le bandeau.</param>
    private static void TickSceneEntityAi_004BFE90(byte* record, ushort strayCast)
    {
        byte* e = record;

        // Ghidra: [esp+0x24] — la sonde du Sanguinaire.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x50;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 1, 0);   // Ghidra: FUN_004bde40, copie de FUN_004a1020
            *(int*)(e + 0x48) = *(int*)(e + 0x20);

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

        // Ghidra: 0x004bff15 — `or ch,10h`, pose a chaque image et jamais retire.
        *(uint*)e |= 0x1000;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        if (speed != 0)
        {
            DecideShadowAction_004BFFD3(e, probe, boldness, distance, heading, speed, strayCast);
            FloatShadow_004C04F1(e, chased);
            ShimmerShadow_004C05DA(e);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);             // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        uint contact = *(uint*)(e + 0x90) & 0xfffff8ff;
        *(uint*)(e + 0x90) = contact;
        if ((e[0] & 0x20) != 0) *(uint*)(e + 0x90) = contact & 0xdfffdfff;

        // Ghidra: 0x004c06da — la vitesse verticale est annulee A CHAQUE IMAGE, heurt ou non.
        bool hit = (*(uint*)(e + 0x90) & 0x2000a882) != 0;
        *(int*)(e + 0x54) = 0;

        if (hit)
        {
            // ================================================================ heurte
            *(ushort*)(e + 0x2c) = 0x1000;

            if (e[0x42] != 7)
            {
                e[0x42] = 7;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(CrtRand() % 0x28 + 0x14);
            }

            *(uint*)e &= 0xfffffffd;

            *(int*)(e + 0x20) = keep.Y;                             // le Sorcier ne rend pas la hauteur
            *(int*)(e + 0x1c) = keep.X;
            *(int*)(e + 0x24) = keep.Z;
            *(uint*)(e + 0x90) = keep.At90;                         // `and eax,0FFFFh` puis le dword
            *(int*)(e + 0x7c) = keep.At7c;
            *(int*)(e + 0x84) = keep.At84;
            *(int*)(e + 0x80) = keep.At80;
            *(int*)(e + 0x88) = keep.At88;
            *(int*)(e + 0x8c) = keep.At8c;
            *(int*)(e + 0x64) = keep.At64;
            *(int*)(e + 0x60) = keep.At60;

            // Ghidra: 0x004c076e — la sonde de contournement porte 0x2000a880, un bit de MOINS que
            // le masque d'entree.
            int side = ProbeClearSide_004D5E60(e, 0x2000a880);
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

        ReplanShadowAction_004C07E7(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004bffd3..0x004c04f0</c> — <b>le premier temps</b> de l'Ombre. Onze cases, dont
    /// sept servies : 0 attendre, 1 se tourner, 2 approcher, 3 onduler, 7 marcher droit, 9 virer
    /// doucement, 10 le sort. Le coup porte a l'image 4.
    /// </summary>
    private static void DecideShadowAction_004BFFD3(
        byte* e, short* probe, int boldness, int distance, int heading, int speed, ushort strayCast)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ le coup
        if ((e[0] & 2) != 0 && *(short*)(e + 0x1a) == 4)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 4)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x10;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
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
                    *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_004c03a5
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                e[0x43]--;
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

                // Ghidra: 0x004c0120 — un cone DEUX FOIS plus large que celui de ses sœurs :
                // 0x320 centre sur 0x190, la ou elles prennent 0x190 centre sur 0xc8.
                if (((heading + 0x190) & 0xfff) <= 0x320)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004c03a2
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
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

                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x80
                    : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : onduler
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    *(short*)(e + 0x18) = 0;
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = (boldness + 0x70) << 8;
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

                    short swing = *(short*)(e + 0x14);
                    swing -= (short)(swing >> 3);
                    *(short*)(e + 0x14) = swing;

                    if (swing < 5)
                    {
                        e[0x43] = 0;
                        *(ushort*)(e + 0x2c) = 0x1000;
                    }

                    *(ushort*)(e + 0x2c) = (ushort)(swing + 0x1000);
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x14) = (short)((CrtRand() % 0x14 + boldness + 5) << 4);
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
                *(int*)(e + 0x5c) = ((boldness & ~7) + 0x20) << 9;
                return;
            }

            // ---------------------------------------------------------- 9 : virer doucement
            case 9:
            {
                short target = *(short*)(e + 0x10);
                short course = *(short*)(e + 0x32);

                course = (short)(course + ((target - course) >> 3));

                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x32) = course;

                int gap = target - course >= 0 ? target - course : course - target;

                if (gap < 0x32) e[0x43] = 0;                        // Ghidra: LAB_004c03a2
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004c03a5
                return;
            }

            // ---------------------------------------------------------- 10 : le sort
            case 10:
                CastShadowSpell_004C03AD(e, strayCast);
                return;

            // ---------------------------------------------------------- 4, 5, 6, 8 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004c03ad..0x004c04ef</c> — <b>le sort</b> de l'Ombre, en quatre phases : la
    /// boucle de refus sur les trente-deux effets, la naissance de <c>0xbd</c>, l'incantation dont la
    /// duree vient de l'octet <c>+0x13</c> de la fiche, puis le lancer.
    ///
    /// <para><b>Le registre non charge</b> : au-dela de la variante 1, l'indice du sort est le
    /// compteur de la boucle, 32 — seize mots apres la fin de la liste. Voir le bandeau.</para>
    /// </summary>
    private static void CastShadowSpell_004C03AD(byte* e, ushort strayCast)
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
                    e[0x43] = 0;                                    // Ghidra: LAB_004c0b1e
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

            int index = scanned;                                    // 32 : le compteur, pas un indice
            if (*(short*)(e + 0x16) == 0) index = SignedMask(CrtRand(), 1);
            if (*(short*)(e + 0x16) == 1) index = CrtRand() % 0x11;

            ushort spell = index < s_ShadowSpells_00552B60.Length
                ? s_ShadowSpells_00552B60[index]
                : strayCast;

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
            if (*(short*)(e + 0x10) < 1) *(short*)(e + 0x12) = 2;
        }

        if (*(short*)(e + 0x12) == 2)
        {
            ushort spell = *(ushort*)(e + 6);
            *(uint*)e &= 0xfffffffb;
            CastSpell_0053A980((uint*)e, (spell >> 8) & 0xff, spell & 0xff, -1, -1);
            *(short*)(e + 0x12) = 3;
        }

        if (*(short*)(e + 0x12) != 3) return;

        e[0x43] = 0;
        *(uint*)e &= 0xfffff7ff;                                    // `and ah,0F7h`
    }

    /// <summary>
    /// Ghidra: <c>0x004c04f1..0x004c05d9</c> — <b>le flottement</b> de l'Ombre, trois cibles selon le
    /// regime et la posture. Le pas de phase est celui du Sorcier, <c>hasard % 50 + 100</c>, et
    /// <b>l'incantation le fige</b> : elle lit la phase sans l'avancer.
    /// </summary>
    private static void FloatShadow_004C04F1(byte* e, byte* chased)
    {
        uint flags = *(uint*)e;
        byte posture = e[0x42];

        if ((flags & 0x20) != 0)
        {
            int y = *(int*)(e + 0x20);
            int target;

            if (posture == 0x0a)
            {
                // Ghidra: 0x004c0503 — la phase n'avance pas, et la cible retombe a 0x8c unites.
                short phase = *(short*)(e + 0x14);
                target = (int)Winsys3d.GetSinTableEntry(phase) * 60 - y + *(int*)(e + 0x48) + 0x8c000;
            }
            else
            {
                short phase = AdvanceShadowBob(e);
                target = (int)Winsys3d.GetSinTableEntry(phase) * 60 - y + *(int*)(e + 0x48) + 0xa0000;
            }

            *(int*)(e + 0x20) = (target >> 3) + y;

            int floor = *(int*)(chased + 0x64);
            if (((*(int*)(chased + 0x20) ^ floor) & unchecked((int)0xfffff000)) == 0)
                *(int*)(e + 0x48) = floor;
            return;
        }

        if ((posture == 0x0c || posture == 0x0a) && (flags & 2) == 0) return;

        short bob = AdvanceShadowBob(e);
        int height = *(int*)(e + 0x20);
        int swing = (int)Winsys3d.GetSinTableEntry(bob) * 60;
        *(int*)(e + 0x20) = ((*(int*)(e + 0x60) + swing - height + 0x8c000) >> 3) + height;
    }

    /// <summary>Le pas de phase du flottement : <c>(phase + hasard % 50 + 100) &amp; 0xfff</c>.</summary>
    private static short AdvanceShadowBob(byte* e)
    {
        int step = CrtRand() % 0x32;
        short phase = (short)((*(short*)(e + 0x14) + step + 0x64) & 0xfff);
        *(short*)(e + 0x14) = phase;
        return phase;
    }

    /// <summary>
    /// Ghidra: <c>0x004c05da..0x004c060f</c> — <b>le scintillement</b> : les trois octets
    /// <c>+0x38</c>, <c>+0x39</c> et <c>+0x3a</c> recoivent <c>(sinus de la phase &gt;&gt; 6) + 100</c>.
    /// Le binaire appelle le sinus trois fois pour le meme argument ; c'est reproduit tel quel, le
    /// sinus etant une table.
    /// </summary>
    private static void ShimmerShadow_004C05DA(byte* e)
    {
        short phase = *(short*)(e + 0x14);

        e[0x38] = (byte)(((int)Winsys3d.GetSinTableEntry(phase) >> 6) + 0x64);
        e[0x39] = (byte)(((int)Winsys3d.GetSinTableEntry(phase) >> 6) + 0x64);
        e[0x3a] = (byte)(((int)Winsys3d.GetSinTableEntry(phase) >> 6) + 0x64);
    }

    /// <summary>
    /// Ghidra: <c>0x004c07e7..0x004c0b12</c> — <b>le troisieme temps</b> de l'Ombre, trois plages
    /// testees l'une apres l'autre (<c>0xFA</c>, <c>0x258</c>).
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : sous 50 fuir en ondulant dos a la proie ; jusqu'a 69 attendre ; de 70 a
    /// 89 fuir encore SI elle est au-dela de <c>hardiesse + 100</c> ; sinon se tourner vers un cap
    /// tire.</item>
    /// <item><b>A mi-distance</b> : face a elle attendre, sinon se tourner ; approcher ; INCANTER sur
    /// la tranche 30 a 59 ; fuir au-dela.</item>
    /// <item><b>De loin</b> : approcher si le tirage tombe sous <c>hardiesse * 2 + 10</c>, sinon
    /// attendre ; fuir ; se tourner.</item>
    /// </list>
    /// </summary>
    private static void ReplanShadowAction_004C07E7(byte* e, int boldness, int timid)
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

            if (roll < 0x32 || (roll >= 0x46 && roll < 0x5a && distance > boldness + 0x64))
            {
                e[0x42] = 3;                                        // Ghidra: LAB_004c0890
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                SetShadowFleeCourse(e, boldness, heading);
            }
            else if (roll < 0x46)
            {
                e[0x42] = 0;
                e[0x43] = (byte)((timid >> 1) + 0x0a);
                *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 1;                                        // Ghidra: LAB_004c08e0
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
        }

        // ================================================================ a moyenne distance
        else if (distance < 0x258)
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
                    e[0x42] = 0;                                    // Ghidra: LAB_004c0954
                    *(short*)(e + 0x12) = 0;
                    e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
                    *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                }
            }
            else if (roll < 0x1e)
            {
                e[0x42] = 2;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
            }
            else
            {
                e[0x42] = 3;                                        // Ghidra: LAB_004c09bc
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                SetShadowFleeCourse(e, boldness, heading);
            }
        }

        // ================================================================ de loin
        else
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (roll < boldness * 2 + 0x0a)
                {
                    e[0x42] = 2;
                    *(short*)(e + 0x10) = 0x64;
                    e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                    return;
                }

                e[0x42] = 0;                                        // Ghidra: LAB_004c0a60
                *(short*)(e + 0x12) = 0;
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 2));
                *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
                return;
            }

            if (roll < 0x46)
            {
                e[0x42] = 3;                                        // Ghidra: LAB_004c0a95
                e[0x43] = (byte)(CrtRand() % 10 + (timid >> 1) + 0x14);
                SetShadowFleeCourse(e, boldness, heading);
                return;
            }

            e[0x42] = 1;                                            // Ghidra: LAB_004c0aef
            if (roll >= 0x5a) *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            e[0x43] = 1;
        }
    }

    /// <summary>
    /// Le cap de la fuite, le meme dans les trois plages : le <b>relevement plus un demi-tour</b>,
    /// secoue de <c>±(hardiesse + 0x80) * 8</c>.
    /// </summary>
    private static void SetShadowFleeCourse(byte* e, int boldness, int heading)
    {
        int span = (boldness + 0x80) << 4;
        int jitter = CrtRand() % span - (span >> 1);
        *(short*)(e + 0x10) = (short)(jitter + heading + *(short*)(e + 0x32) + 0x800);
    }
}
