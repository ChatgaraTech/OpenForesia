// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA HARPIE — 0x004CB4F0, chateau de l'immortel (zone 6, sous-zones 0 a 4), et ses deux copies :
// 0x004D60A0 (chateau, sous-zones 5 et 11) et 0x004FDFE0 (grotte scellee).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004CB4F0 — LA HARPIE. Transcrite le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004CB4F0.asm, 1 308 instructions, absente de Blaze.c),
    // table de sauts a QUINZE entrees a 0x004CC608.
    //
    // TROIS CORPS D'UN COUP. 0x004D60A0 et 0x004FDFE0 ne different de 0x004CB4F0 que par l'ordre des
    // registres, les adresses de leur table de sauts, leur chercheur de proie (copies de
    // FUN_004a1020) — et l'adresse du mot de sort qu'ils lisent : 0x00552c08, 0x00552c80,
    // 0x00553018. Les trois mots valent 0x0015 : la meme harpie, trois fois.
    //
    // LA CHARPENTE DE LA CRAPOUILLE (Ai/Amphibians.cs) : meme naissance sur +0x16, meme sonde, meme
    // vol stationnaire (HoverCrapouille_004AD452), meme integration volante, et la meme queue — pose
    // et heurt au masque 0x2000a882, sortie en SettleCrapouilleFlight le meme jour. L'accroche
    // (posture 11) et le pique (posture 12) sont les siens, aux virages pres. Ce qui fait la harpie :
    //
    //   - SA MORSURE N'A PAS D'IMAGE : la sonde frappe a chaque image ou le bit d'attaque est pose.
    //   - LA POSTURE 13, LE CRI : animation 4, le son 0x3d de la zone, puis une image sur deux un
    //     sous-type 0xe0 — LA FAMILLE A TROIS VIES du chateau (Effects/ImmortalCastleParticles.cs),
    //     qu'elle fait naitre en vie 0 : un feu follet. Le cri acheve, un dernier 0xe0 en vie 2 :
    //     L'ECLAIR ROUGE, qui porte l'effet d'etat 11 a tout le groupe. Une seule harpie crie a la
    //     fois, et seulement a moins de 800 unites.
    //   - LA POSTURE 14, LE SORT : Ventilo (0x0015), en remontant vers la hauteur de sa proie.
    //   - EN APPROCHE ET EN FILANT, la variante 0 lache une fois sur quarante un 0xe0 en vie 1 : le
    //     projectile qui rebondit.
    //   - ELLE SE POSE AUTREMENT (posture 10) : sur l'animation 5, et une fois posee elle repart en
    //     cri, une chance sur vingt, si sa proie est a plus de 180 unites.
    //   - Et dans la sous-zone 5 du chateau, chaque image, quatre champs de placement a 0xa000 et
    //     TryPlaceEntity au masque 0x8000 : c'est la salle ou elle vole au-dessus du vide.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004cb4f0</c> et ses copies — <b>l'intelligence de la Harpie</b>. Voir le
    /// bandeau.
    /// </summary>
    /// <param name="spell">Le mot de sort de la copie, lu dans <c>.data</c> :
    /// <c>DAT_00552c08</c>, <c>DAT_00552c80</c> ou <c>DAT_00553018</c>. Les trois valent
    /// <c>0x0015</c>, Ventilo.</param>
    private static void TickSceneEntityAi_004CB4F0(byte* record, ushort spell)
    {
        byte* e = record;

        // Ghidra: [esp+0x28] — la sonde de la Crapouille.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x14; probe[2] = 0x50; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x78;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004c8d80 (FUN_004d5a20, FUN_004ff140), copies de FUN_004a1020
            *(int*)(e + 0x48) = *(int*)(e + 0x20);

            if (*(short*)(e + 0x16) >= 10)
            {
                *(uint*)e &= 0xffffffdf;
                *(short*)(e + 0x16) -= 10;
            }
            else
            {
                *(uint*)e |= 0x20;
            }
        }

        byte* chased = *(byte**)(e + 0x6c);

        // Ghidra: 0x004cb561 — la sous-zone 5 du chateau.
        if ((byte)Res.s_Unknown_005de528 == 6 && (byte)s_Unknown_005de52c == 5)
        {
            *(short*)(e + 0x82) = unchecked((short)0xa000);
            *(short*)(e + 0x86) = unchecked((short)0xa000);
            *(short*)(e + 0x8a) = unchecked((short)0xa000);
            *(short*)(e + 0x8e) = unchecked((short)0xa000);
            TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);   // Ghidra: FUN_005330d0
        }

        *(uint*)e &= 0xfffffffe;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideHarpyAction_004CB6B4(e, probe, chased, boldness, distance, heading, speed, spell);
            HoverCrapouille_004AD452(e, chased);                   // Ghidra: 0x004cbfea, la meme
        }

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleCrapouilleFlight(e, boldness, &keep);                // Ghidra: 0x004cc147, la meme

        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanHarpyAction_004CC2B0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004cb6b4..0x004cbfe9</c> — <b>le premier temps</b> de la Harpie. Quinze cases,
    /// dont dix servies : 0 attendre, 1 se tourner, 2 approcher, 3 filer vers un cap, 7 marcher
    /// droit, 10 se poser, 11 l'accroche, 12 le pique, 13 le cri, 14 le sort.
    /// </summary>
    private static void DecideHarpyAction_004CB6B4(
        byte* e, short* probe, byte* chased, int boldness, int distance, int heading, int speed,
        ushort spell)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ la morsure, sans image
        if ((*(uint*)e & 2) != 0)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x14;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
            }

            uint gesture = *(uint*)e;
            if ((gesture & 0x2000000) != 0 && e[0x42] != 0x0c)
            {
                *(uint*)e = gesture & 0xfffffffd;
                e[0x43] = 0;
            }
        }

        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
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
                    *(short*)(e + 0x18) = 0;
                    *(uint*)e |= 0x1000000;
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
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 0;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = (*(short*)(e + 0x16) << 0xd) + boldness * 0x90 + 0x7000;
                }

                if (*(short*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    // Ghidra: 0x004cb8c3 — la poussee tombe AVEC le rebours, a l'inverse de la
                    // Crapouille.
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                }
                else
                {
                    if (e[0x43] != 0) e[0x43]--;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness + 0x80
                            : -0x80 - boldness);
                    }
                }

                DropHarpyBouncer_004CB90E(e);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = (*(short*)(e + 0x16) << 0xd) + boldness * 0x90 + 0x5000;
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

                    DropHarpyBouncer_004CB90E(e);
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x18) = 0;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
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
                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 10 : se poser
            case 10:
                HarpyLand_004CBAB5(e, distance);
                return;

            // ---------------------------------------------------------- 11 : l'accroche
            case 11:
            {
                int y = *(int*)(e + 0x20);
                int preyY = *(int*)(chased + 0x20);

                if (y + 0xb4000 < preyY)
                {
                    e[0x43] = 0;
                    *(uint*)e &= 0xfffffffd;
                    return;
                }

                *(int*)(e + 0x20) = y + ((preyY - y + 0x28000) >> 2);

                short phase = *(short*)(e + 0x12);

                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 12 : le pique
            case 12:
                HarpyDive_004CBBE4(e, chased, boldness, heading);
                return;

            // ---------------------------------------------------------- 13 : le cri
            case 13:
                HarpyScreech_004CBD6B(e, distance);
                return;

            // ---------------------------------------------------------- 14 : le sort
            case 14:
                HarpyCast_004CBE64(e, chased, spell);
                return;

            // ---------------------------------------------------------- 4, 5, 6, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004cb90e</c> et <c>0x004cb9fd</c> — <b>le projectile lache en vol</b>. Une fois
    /// sur quarante, et pour la variante 0 seulement, un sous-type <c>0xe0</c> nait avec
    /// <c>+0x14</c> pose au reste du tirage — donc 1, la vie du projectile qui rebondit (voir
    /// <c>TickCastleThreeLives_00425E50</c>). La valeur d'origine de <c>+0x14</c> est rendue apres
    /// la naissance.
    /// </summary>
    private static void DropHarpyBouncer_004CB90E(byte* e)
    {
        int roll = CrtRand() % 0x28;                                // Ghidra: FUN_00547140
        if (roll != 1 || *(short*)(e + 0x16) != 0) return;

        short life = *(short*)(e + 0x14);
        *(short*)(e + 0x14) = (short)roll;
        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xe0, 0, -1);   // Ghidra: FUN_0047ac40
        *(short*)(e + 0x14) = life;
    }

    /// <summary>
    /// Ghidra: <c>0x004cbab5..0x004cbb7f</c> — <b>se poser</b>, posture 10. La descente de
    /// <see cref="CrapouilleLand_004AD155"/> (un trente-deuxieme de l'ecart, le double pres du
    /// sol), mais sans le refus de la variante 1, et sur l'animation 0 puis 5.
    ///
    /// <para><b>Posee</b>, le geste est tenu pour fini et le rebours descend — le binaire le
    /// remet a zero s'il vient d'y tomber, ce qui ne change rien. Au-dela de 180 unites de sa
    /// proie, une chance sur vingt par image de repartir <b>en cri</b> (posture 13).</para>
    /// </summary>
    private static void HarpyLand_004CBAB5(byte* e, int distance)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x18) = 0;
            *(short*)(e + 0x1a) = 0;
            *(short*)(e + 0x12) = 1;
        }

        if (*(short*)(e + 0x12) == 1)
        {
            *(uint*)e |= 0x1000000;

            int floor = *(int*)(e + 0x60);
            int y = *(int*)(e + 0x20);
            int step = (floor - y) >> 5;

            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x18) = 0;

            int magnitude = step < 0 ? -step : step;
            if ((magnitude & unchecked((int)0xfffff000)) < 0x3000) step <<= 1;

            y += step;
            *(int*)(e + 0x20) = y;

            int gap = (floor - y) >> 0xc;
            if (gap < 0) gap = -gap;

            if (gap < 10)
            {
                *(short*)(e + 0x12) = 2;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x18) = 5;
            }
        }

        if (*(short*)(e + 0x12) != 2) return;

        *(uint*)e |= 0x2000000;
        *(short*)(e + 0x18) = 5;
        e[0x43]--;                                                  // Ghidra: `dec cl ; jne` puis 0 — sans effet

        if (distance <= 0xb4) return;
        if (CrtRand() % 0x14 != 0) return;                          // Ghidra: FUN_00547140

        e[0x42] = 0x0d;
        *(short*)(e + 0x12) = 0;
        e[0x43] = 1;
    }

    /// <summary>
    /// Ghidra: <c>0x004cbbe4..0x004cbd6a</c> — <b>le pique</b>, posture 12. La forme exacte de
    /// <see cref="CrapouilleDive_004AD2D3"/> — montee de trois unites par image jusqu'a 200 au-dessus
    /// du sol, plongee vers la proie plus 0x14 a une poussee qui retombe toujours sur 0x14000,
    /// <c>(variante + 2) * 10</c> images, puis freinage — a une difference pres : <b>les virages
    /// prennent deux fois la hardiesse, plus 0x80</b>, la ou la Crapouille prend une fois la
    /// hardiesse plus 0x32.
    /// </summary>
    private static void HarpyDive_004CBBE4(byte* e, byte* chased, int boldness, int heading)
    {
        if (*(short*)(e + 0x12) == 1)
        {
            int y = *(int*)(e + 0x20) + 0x3000;
            *(int*)(e + 0x20) = y;

            if ((y - *(int*)(e + 0x60)) >> 0xc > 0xc8)
            {
                *(short*)(e + 0x12) = 2;
                *(short*)(e + 0x10) = 0;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 6;
            }

            TurnHarpyTowardPrey(e, boldness, heading);
        }

        if (*(short*)(e + 0x12) == 2)
        {
            *(int*)(e + 0x5c) = 0x14 << (((boldness & ~3) << 0xa) + 0xc);

            int y = *(int*)(e + 0x20);
            int preyY = *(int*)(chased + 0x20);

            if (y + 0xb4000 < preyY)
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffd;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            *(int*)(e + 0x20) = y + ((preyY - y + 0x14000) >> 2);

            (*(short*)(e + 0x10))++;
            short frames = *(short*)(e + 0x10);

            TurnHarpyTowardPrey(e, boldness, heading);

            if (frames >= (*(short*)(e + 0x16) + 2) * 10)
            {
                *(short*)(e + 0x12) = 3;
                *(uint*)e &= 0xfffffffd;
            }
        }

        short phase = *(short*)(e + 0x12);

        if (phase == 3)
        {
            int v = *(int*)(e + 0x5c);
            v -= v >> 3;
            *(int*)(e + 0x5c) = v;
            if ((v & unchecked((int)0xfffff000)) < 0xa000) e[0x43] = 0;
        }

        if (phase != 0) return;

        *(short*)(e + 0x1a) = 0;
        *(short*)(e + 0x12) = 1;
        *(short*)(e + 0x18) = 1;
        *(int*)(e + 0x5c) = 0;
        *(uint*)e |= 0x1000000;
    }

    /// <summary>Le virage du pique de la Harpie — hors d'un cone de <c>±0x100</c>, deux fois la
    /// hardiesse plus <c>0x80</c> vers la proie.</summary>
    private static void TurnHarpyTowardPrey(byte* e, int boldness, int heading)
    {
        if (((heading + 0x100) & 0xfff) <= 0x200) return;

        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x80 : -0x80 - boldness * 2);
    }

    /// <summary>
    /// Ghidra: <c>0x004cbd6b..0x004cbe63</c> — <b>le cri</b>, posture 13, sur l'animation 4.
    ///
    /// <list type="table">
    ///   <item><term>armer</term><description>une seule harpie a la fois : si une autre entite de
    ///     scene est deja en posture 13 (octet <c>+0x42</c> des 32 de <c>0x005ecaa0</c>, rang
    ///     <c>+0x08</c> excepte), elle renonce ; au-dela de 800 unites de sa proie aussi. Sinon le
    ///     son <c>0x3d</c> de la zone, hauteur <c>0x300</c>, gain <c>0x3000</c>, a sa
    ///     position.</description></item>
    ///   <item><term>crier</term><description>les images impaires de l'animation, un sous-type
    ///     <c>0xe0</c> en vie 0 — un feu follet. Quand l'animation s'acheve, un dernier
    ///     <c>0xe0</c> en <b>vie 2 : l'eclair rouge</b>, qui porte l'effet d'etat 11 a tout le
    ///     groupe, et quinze images d'attente.</description></item>
    /// </list>
    /// <para>Les deux naissances passent par <c>+0x14</c>, pose le temps de l'appel puis
    /// rendu.</para>
    /// </summary>
    private static void HarpyScreech_004CBD6B(byte* e, int distance)
    {
        int* position = (int*)(e + 0x1c);

        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x12) = 1;
            *(uint*)e &= 0xfeffffff;
            *(short*)(e + 0x18) = 4;
            *(short*)(e + 0x1a) = 0;

            // Ghidra: 0x004cbd8d — `mov ecx,5ECAE2h`, soit 0x005ecaa0 + 0x42.
            byte* scene = Mem.Entity(0x005ecaa0);
            for (int i = 0; i < 0x20; i++, scene += 0x9c)
            {
                if (scene[0x42] == 0x0d && i != e[8])
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004cc5fc
                    return;
                }
            }

            if (distance > 0x320)
            {
                e[0x43] = 0;
                return;
            }

            Aud.PlayPositionalSound(ZoneSound(0x3d), 0x300, 0x3000, position);   // Ghidra: FUN_0053db20
        }

        if (*(short*)(e + 0x12) == 1)
        {
            if ((e[0x1a] & 1) != 0)
            {
                short life = *(short*)(e + 0x14);
                *(short*)(e + 0x14) = 0;
                SpawnStatusEffectEntity_0047AC40(e, position, null, 0xe0, 0, -1);   // Ghidra: FUN_0047ac40
                *(short*)(e + 0x14) = life;
            }

            *(int*)(e + 0x5c) = 0;

            if ((*(uint*)e & 0x2000000) != 0)
            {
                short life = *(short*)(e + 0x14);
                *(short*)(e + 0x12) = 2;
                e[0x43] = 0x0f;
                *(short*)(e + 0x14) = 2;
                SpawnStatusEffectEntity_0047AC40(e, position, null, 0xe0, 0, -1);
                *(short*)(e + 0x14) = life;
            }
        }

        if (*(short*)(e + 0x12) == 2) e[0x43]--;
    }

    /// <summary>
    /// Ghidra: <c>0x004cbe64..0x004cbfe9</c> — <b>le sort</b>, posture 14 : l'ossature du sort de
    /// la famille (refus si un effet porte deja son nom ou si <c>0x804</c> est pose, aureole
    /// <c>0xbd</c>, duree lue au <c>+0x13</c> de la fiche), avec le mot de sort de la copie, et
    /// <b>une remontee vers sa proie a chaque phase</b> : d'un huitieme de l'ecart a la hauteur de la
    /// proie plus 0x14 pendant l'incantation, plus 0x28 ensuite. Lancer sur l'animation 0 a
    /// l'image 3.
    /// </summary>
    private static void HarpyCast_004CBE64(byte* e, byte* chased, ushort spell)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            byte* effects = Mem.Entity(0x005ff400);
            for (int i = 0; i < 0x20; i++)
            {
                byte* effect = effects + i * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;
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

            SpawnCastEntity_0047AAE0(e, 0xbd, 0, -1);                // Ghidra: FUN_0047aae0
            *(short*)(e + 0x12) = 1;
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
            int y = *(int*)(e + 0x20);
            y += (*(int*)(chased + 0x20) - y + 0x14000) >> 3;

            (*(short*)(e + 0x10))--;
            *(int*)(e + 0x20) = y;

            if (*(short*)(e + 0x10) < 1)
            {
                *(short*)(e + 0x12) = 2;
                *(uint*)e &= 0xfefffffb;
                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;
            }
        }

        if (*(short*)(e + 0x12) == 2)
        {
            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = y + ((*(int*)(chased + 0x20) - y + 0x28000) >> 3);

            if (*(short*)(e + 0x1a) == 3)
            {
                ushort cast = *(ushort*)(e + 6);
                CastSpell_0053A980((uint*)e, (cast >> 8) & 0xff, cast & 0xff, -1, -1);
                *(short*)(e + 0x12) = 3;
            }
        }

        if (*(short*)(e + 0x12) != 3) return;

        int yy = *(int*)(e + 0x20);
        *(int*)(e + 0x20) = yy + ((*(int*)(chased + 0x20) - yy + 0x28000) >> 3);

        uint done = *(uint*)e;
        if ((done & 0x2000000) == 0) return;

        e[0x43] = 0;
        *(uint*)e = done & 0xfffff7ff;                             // `and ah,0F7h`
    }

    /// <summary>
    /// Ghidra: <c>0x004cc2b0..0x004cc5fb</c> — <b>le troisieme temps</b> de la Harpie, les trois
    /// plages de la Crapouille (0x96, 0x258). Le lointain est le sien au mot pres ; le contact et la
    /// mi-distance different :
    ///
    /// <list type="bullet">
    /// <item><b>au contact</b>, les hauts tirages piquent toujours — la Crapouille s'y posait
    /// parfois ;</item>
    /// <item><b>a mi-distance</b>, une harpie de variante non nulle mal orientee lance son sort au
    /// lieu de se tourner ; au-dela de 80, elle se pose si le tirage le permet, et sinon attend
    /// (variante 0) ou lance son sort.</item>
    /// </list>
    /// </summary>
    private static void ReplanHarpyAction_004CC2B0(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        // ================================================================ le contact
        if (distance < 0x96)
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
                        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                        e[0x43] = 1;
                        *(int*)(e + 0x5c) = 0;
                        *(short*)(e + 0x18) = 3;
                        *(short*)(e + 0x1a) = 0;
                    }
                    else
                    {
                        e[0x42] = 0;
                        e[0x43] = (byte)(CrtRand() % 0x28);         // Ghidra: FUN_00547140
                    }
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x46)
            {
                StrafeSalamander(e, timid);                         // Ghidra: 0x004cc39f
            }
            else
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
            }
        }

        // ================================================================ la mi-distance
        if (distance < 0x258 && distance >= 0x96)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) < 0x800) e[0x42] = 0x0c;
                else if (*(short*)(e + 0x16) != 0) e[0x42] = 0x0e;
                else e[0x42] = 1;

                e[0x43] = 1;
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            }
            else if (roll < 0x50)
            {
                StrafeSalamander(e, timid);                         // Ghidra: 0x004cc47b
            }
            else if (100 - roll > (boldness >> 2))
            {
                CrapouilleSettle(e);                                // Ghidra: 0x004cc4c7, la meme
            }
            else if (*(short*)(e + 0x16) == 0)
            {
                e[0x42] = 0;
                e[0x43] = (byte)(CrtRand() % 0x28);
            }
            else
            {
                e[0x42] = 0x0e;
                e[0x43] = 1;
            }
        }

        // ================================================================ le lointain
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x32 && far >= boldness * 2 + 0x0a)
        {
            e[0x42] = 0;
            int wait = CrtRand() % 10;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(wait + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far >= 0x32 && far < 0x4b)
        {
            if (*(short*)(e + 0x16) == 1)
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
                return;
            }

            StrafeSalamander(e, timid);                             // Ghidra: 0x004cc58d
            return;
        }

        // Ghidra: LAB_004cc5d0 — approcher de loin, + 0x1e.
        e[0x42] = 2;
        int delay = CrtRand() % 10;
        *(short*)(e + 0x10) = 100;
        e[0x43] = (byte)(delay + (timid >> 1) + 0x1e);
    }
}
