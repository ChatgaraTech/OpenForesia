// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE SORCIER — 0x004AE820, vieille tour (zone 4, sous-zones 5, 6, 8, 10 : « Archange noir, Sorcier »),
// et sa copie 0x004B4DB0 (sous-zones 3, 7, 9).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004AE820 — LE SORCIER. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/OldTower/FUN_004AE820.asm, 1 000 instructions, absente de Blaze.c), table de
    // sauts a douze entrees a 0x004AF528.
    //
    // DEUX CORPS D'UN COUP. 0x004B4DB0 ne differe que par l'adresse de sa table de sorts (0x00552AC0
    // contre 0x00552A70, memes octets releves), par deux comparaisons dont les registres sont
    // echanges et par sa copie du chercheur de proie (0x004B3290 contre 0x004AC860, conformes a
    // FUN_004A1020). Tables de sauts identiques en relatif.
    //
    // LA CHARPENTE EST CELLE DE LA SALAMANDRE (Ai/Mine/Salamander.cs) — volante a deux regimes, meme
    // heurt qui epargne le sort, meme replanification a trois plages et la meme branche morte au loin.
    // Ce qui lui appartient :
    //
    //   - SES SORTS (posture 0x0a) : variante 0, un tirage parmi Tenebres, Fleches magiques et Mur de
    //     flammes ; variante 1, parmi Coup de jus, Assaut magique, Eclair, Ventilo et Satanas. Il
    //     S'ELEVE vers sa proie pendant l'incantation (un huitieme par image, 0x14 unites au-dessus),
    //     refuse de lancer si elle est a plus de 0xb4 unites au-dessus de lui, et le sort part a
    //     l'image 16. Au-dela de la variante 1, le mot du sort est encore le compteur 0x20.
    //   - LE PLONGEON (posture 0x0b) remplace la morsure et le crachat : la replanification arme le
    //     geste d'emblee (animation 3), le Sorcier rejoint la hauteur de sa proie a 0x28 unites au-dessus
    //     d'un quart par image, et le prelude frappe a l'image 12. A mi-distance, face a sa proie, il
    //     INCANTE au lieu de cracher.
    //   - IL VOLE PLUS LENTEMENT ET TOURNE PLUS SEC : poussee 0x5000 partout (0xa000, 0x8000 et
    //     0x9000 chez la Salamandre), virages de `hardiesse + 0x32` en posture 1 et de
    //     `hardiesse * 2 + 0x32` en approche.
    //   - SON FLOTTEMENT avance de 100 a 149 pas et vise 0x8c unites au-dessus de sa reference dans
    //     les deux regimes.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552a70</c> et <c>&amp;DAT_00552a78</c> — <b>les sorts du Sorcier</b>,
    /// releves dans <c>.data</c> : <c>0006 0008 000a 0000</c> puis <c>000b 000e 0010 0015 0018</c>.
    /// Famille dans l'octet haut, indice dans l'octet bas : toutes de la famille 0.
    ///
    /// <para>Variante 0 : <c>hasard % 3</c> dans les trois premiers mots — Tenebres, Fleches
    /// magiques, Mur de flammes. Variante 1 : <c>hasard % 5</c> dans les cinq suivants — Coup de jus,
    /// Assaut magique, Eclair, Ventilo, Satanas. Le mot nul qui separe les deux listes n'est jamais
    /// lu.</para>
    ///
    /// <para>La copie <c>0x004B4DB0</c> lit <c>0x00552AC0</c> / <c>0x00552AC8</c>, aux memes
    /// octets.</para>
    /// </summary>
    private static readonly ushort[] s_SorcererSpells_00552A70 = [0x0006, 0x0008, 0x000a];
    private static readonly ushort[] s_SorcererSpells_00552A78 = [0x000b, 0x000e, 0x0010, 0x0015, 0x0018];

    /// <summary>
    /// Ghidra: <c>0x004ae820</c> (et <c>0x004b4db0</c>) — <b>l'intelligence du Sorcier</b>. Voir le
    /// bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004AE820(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x28] — la sonde, celle de la Salamandre.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x32;
        probe[2] = 0x50;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004ac860 (FUN_004b3290), copies de FUN_004a1020
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

        *(uint*)e &= 0xfffffffe;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);
        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004ae95c — la distance recalculee a plat.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        if (speed != 0)
        {
            DecideSorcererAction_004AE820(e, probe, chased, boldness, distance, heading, speed);
            FloatSorcerer_004AEF1E(e, chased);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);             // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        uint flags = *(uint*)e;
        uint contact = *(uint*)(e + 0x90) & 0xfffff8ff;
        *(uint*)(e + 0x90) = contact;
        if ((flags & 0x20) != 0) *(uint*)(e + 0x90) = contact & 0xdfffdfff;

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (*(uint*)(e + 0x90) & 0x8000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((*(uint*)(e + 0x90) & 0x2000a882) != 0)
        {
            // ================================================================ heurte
            if (e[0x42] != 7 && e[0x42] != 0x0a)
            {
                e[0x42] = 7;
                int roll = CrtRand();
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(roll % 0x28 + 0x14);
            }

            *(uint*)e &= 0xfffffffd;

            *(int*)(e + 0x24) = keep.Z;
            *(int*)(e + 0x1c) = keep.X;
            *(int*)(e + 0x7c) = keep.At7c;
            *(uint*)(e + 0x90) = keep.At90;                         // `and eax,0FFFFh` puis le dword
            *(int*)(e + 0x80) = keep.At80;
            *(int*)(e + 0x84) = keep.At84;
            *(int*)(e + 0x88) = keep.At88;
            *(int*)(e + 0x8c) = keep.At8c;
            *(int*)(e + 0x64) = keep.At64;
            *(int*)(e + 0x60) = keep.At60;

            int side = ProbeClearSide_004D5E60(e, 0x2000a882);
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

        ReplanSorcererAction_004AF1CD(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004ae996..0x004aef1c</c> — <b>le premier temps</b> : l'animation, le coup du
    /// plongeon porte a l'image 12, puis la posture. Douze cases, dont sept servies : 0 attendre,
    /// 1 se tourner, 2 approcher, 3 filer vers un cap, 7 marcher droit, 0x0a le sort, 0x0b le
    /// plongeon.
    /// </summary>
    private static void DecideSorcererAction_004AE820(
        byte* e, short* probe, byte* chased, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ le coup
        // Ghidra: 0x004ae9fa — sans le test `+0x96 == 0` de la Salamandre.
        if ((*(uint*)e & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 0x0c)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0c)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }
            }

            uint flags = *(uint*)e;
            if ((flags & 0x2000000) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                e[0x43] = 0;
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
                    *(short*)(e + 0x18) = 0;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                }

                *(int*)(e + 0x5c) = 0;

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    return;
                }

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

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x32
                    : -0x32 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 0x0a : le sort
            case 0x0a:
                SorcererCast_004AED26(e, chased);
                return;

            // ---------------------------------------------------------- 0x0b : le plongeon
            case 0x0b:
            {
                int y = *(int*)(e + 0x20);
                int preyY = *(int*)(chased + 0x20);

                if (y + 0xb4000 < preyY)
                {
                    e[0x43] = 0;
                    *(uint*)e &= 0xfffffffd;
                    return;
                }

                short phase = *(short*)(e + 0x12);
                *(int*)(e + 0x20) = ((preyY - y + 0x28000) >> 2) + y;

                if (phase == 1)
                {
                    uint flags = *(uint*)e;
                    if ((flags & 0x2000000) != 0)
                    {
                        *(uint*)e = flags & 0xfffffffd;
                        e[0x43] = 0;
                    }
                }

                if (phase != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 4, 5, 6, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004aed26..0x004aeec0</c> — <b>le sort</b>, posture 0x0a, en quatre phases.
    ///
    /// <para>
    /// L'ossature de <see cref="SalamanderCast_0048E8C0"/>, avec le refus du Gremlin quand la proie
    /// est plus de <c>0xb4</c> unites plus haut, et la montee du Gremlin pendant l'incantation : la
    /// hauteur rejoint d'un huitieme par image celle de la proie plus <c>0x14</c> unites. Incantation
    /// sur l'animation 4, lancer sur la 5, le sort part a l'image <b>16</b>.
    /// </para>
    /// <para>
    /// <b>Le registre non charge</b> : au-dela de la variante 1, le mot du sort est le compteur de la
    /// boucle de refus, <c>0x20</c> — famille 0 indice 32, hors table.
    /// </para>
    /// </summary>
    private static void SorcererCast_004AED26(byte* e, byte* chased)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            if (*(int*)(e + 0x20) + 0xb4000 < *(int*)(chased + 0x20))
            {
                e[0x43] = 0;
                return;
            }

            byte* effects = Mem.Entity(0x005ff400);
            int scanned = 0;                                        // Ghidra: edi

            for (; scanned < 0x20; scanned++)
            {
                byte* effect = effects + scanned * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004af51f
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
            if (*(short*)(e + 0x16) == 0) spell = s_SorcererSpells_00552A70[CrtRand() % 3];
            if (*(short*)(e + 0x16) == 1) spell = s_SorcererSpells_00552A78[CrtRand() % 5];

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
            int y = *(int*)(e + 0x20);
            int risen = ((*(int*)(chased + 0x20) - y + 0x14000) >> 3) + y;

            (*(short*)(e + 0x10))--;
            *(int*)(e + 0x20) = risen;

            if (*(short*)(e + 0x10) < 1)
            {
                *(short*)(e + 0x12) = 2;
                *(uint*)e &= 0xfefffffb;
                *(short*)(e + 0x18) = 5;
                *(short*)(e + 0x1a) = 0;
            }
        }

        if (*(short*)(e + 0x12) == 2 && *(short*)(e + 0x1a) == 0x10)
        {
            ushort spell = *(ushort*)(e + 6);
            CastSpell_0053A980((uint*)e, (spell >> 8) & 0xff, spell & 0xff, -1, -1);
            *(short*)(e + 0x12) = 3;
        }

        if (*(short*)(e + 0x12) != 3) return;

        uint done = *(uint*)e;
        if ((done & 0x2000000) == 0) return;

        e[0x43] = 0;
        *(uint*)e = done & 0xfffff7ff;                             // `and ah,0F7h`
    }

    /// <summary>
    /// Ghidra: <c>0x004aef1e..0x004aefdb</c> — <b>le flottement</b> du Sorcier.
    ///
    /// <para>
    /// Le pas de phase est <c>hasard % 50 + 100</c> (la Salamandre : <c>+ 50</c>), et la cible est
    /// <b>0x8c unites au-dessus de sa reference dans les deux regimes</b> : <c>+0x48</c> en regime
    /// <c>0x20</c>, le plancher <c>+0x60</c> sinon. Seul le sort retombe retient le flottement, dans
    /// les deux regimes.
    /// </para>
    /// </summary>
    private static void FloatSorcerer_004AEF1E(byte* e, byte* chased)
    {
        uint flags = *(uint*)e;
        byte posture = e[0x42];

        if ((flags & 0x20) != 0)
        {
            if (posture != 0x0a || (flags & 2) != 0)
            {
                short phase = AdvanceSorcererBob(e);
                int y = *(int*)(e + 0x20);
                int wave = (int)Winsys3d.GetSinTableEntry(phase) * 60;
                *(int*)(e + 0x20) = ((*(int*)(e + 0x48) + wave - y + 0x8c000) >> 3) + y;
            }

            int floor = *(int*)(chased + 0x64);
            if (((*(int*)(chased + 0x20) ^ floor) & unchecked((int)0xfffff000)) == 0)
                *(int*)(e + 0x48) = floor;
            return;
        }

        if (posture == 0x0a && (flags & 2) == 0) return;

        short bob = AdvanceSorcererBob(e);
        int height = *(int*)(e + 0x20);
        int swing = (int)Winsys3d.GetSinTableEntry(bob) * 60;
        *(int*)(e + 0x20) = ((*(int*)(e + 0x60) + swing - height + 0x8c000) >> 3) + height;
    }

    /// <summary>Le pas de phase du flottement : <c>(phase + hasard % 50 + 100) &amp; 0xfff</c>. Le
    /// hasard est tire <b>avant</b> la lecture de la hauteur, comme dans le binaire.</summary>
    private static short AdvanceSorcererBob(byte* e)
    {
        int step = CrtRand() % 0x32;
        short phase = (short)((*(short*)(e + 0x14) + step + 0x64) & 0xfff);
        *(short*)(e + 0x14) = phase;
        return phase;
    }

    /// <summary>
    /// Ghidra: <c>0x004af1cd..0x004af51e</c> — <b>le troisieme temps</b> du Sorcier.
    ///
    /// <para>
    /// La replanification de <see cref="ReplanSalamanderAction_0048EEB6"/>, a trois endroits pres :
    /// </para>
    /// <list type="bullet">
    /// <item><b>de pres</b>, le plongeon arme le geste tout de suite (<c>| 2</c>), et le tirage
    /// suivant (de <c>0x1e</c> a <c>0x31</c>) rend une attente de <c>hasard % 40</c> images au lieu
    /// d'une attente nulle ;</item>
    /// <item><b>a mi-distance</b>, face a sa proie (cone de <c>0x400</c> de part et d'autre), il
    /// <b>incante</b> — la Salamandre crachait ;</item>
    /// <item><b>a mi-distance encore</b>, le tirage de hardiesse mene au sort sans condition de
    /// variante, et sinon a une attente de <c>hasard % 40</c>.</item>
    /// </list>
    /// </summary>
    private static void ReplanSorcererAction_004AF1CD(byte* e, int boldness, int timid)
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
                        e[0x43] = (byte)(CrtRand() % 0x28);
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
                StrafeSalamander(e, timid);
            }
            else if (100 - roll < (boldness >> 1) + 10)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 0;
                e[0x43] = (byte)(CrtRand() % 0x28);
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0x96)
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
            else if (roll < 0x50)
            {
                StrafeSalamander(e, timid);
            }
            else if (100 - roll < (boldness >> 1) + 10)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 0;
                e[0x43] = (byte)(CrtRand() % 0x28);
            }
        }

        // ================================================================ de loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x32)
        {
            if (far < boldness * 2 + 10)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 100;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
                return;
            }

            e[0x42] = 0;
            int rest = CrtRand() % 10;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(rest + (timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x4b)
        {
            StrafeSalamander(e, timid);
            return;
        }

        // Ghidra: `cmp edx,3Ch ; jge` — jamais vrai ici, le tirage vaut au moins 0x4b.
        if (far < 0x3c)
        {
            e[0x42] = 1;
            e[0x43] = 1;
        }
    }
}
