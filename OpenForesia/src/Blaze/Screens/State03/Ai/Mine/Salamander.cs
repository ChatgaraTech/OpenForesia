// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA SALAMANDRE — 0x0048E3A0, sous-type 12 de la mine abandonnee, sous-zone 6.
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, colonne ia = 12). DEUX CORPS D'UN
// COUP : 0x00497EF0 (la montagne du dragon de feu) est identique instruction pour instruction,
// table de sorts comprise. 1 095 instructions, absentes de Blaze.c ; releve
// Legacy/Raw/Blaze/FUN_0048E3A0.asm, table de sauts a treize entrees a 0x0048F1F8.
//
// LA CHARPENTE EST CELLE DU GREMLIN (Ai/OldTower/Gremlin.cs) : volante a deux regimes fixes a la
// naissance par +0x16, sauvegarde du placement sans la hauteur, meme integration de volante, meme
// heurt de mur, meme sort a quatre phases avec le meme registre non charge. Mais aucune posture n'y
// est recopiee telle quelle, et :
//
//   - SES SORTS DEPENDENT DE SA VARIANTE (0x005528B0, deux mots par variante, tirage a pile ou
//     face) : 0 Balle de feu / Mur de flammes, 1 Mur de flammes / Boule de feu, 2 Fleches
//     magiques / Congelo, 3 Congelo / Demon polaire. Au-dela de 3, le sort est le mot 0x0020.
//   - ELLE CRACHE (posture 0x0c) : un effet 0xDF nait a l'image 23 de l'animation 6 si la proie
//     est a moins de 0x9c4 unites.
//   - SA MORSURE (posture 0x0b) arme le geste a l'image 12 et porte a l'image 14, par le prelude.
//   - SON FLOTTEMENT est double : en regime 0x20 elle vise +0x48 plus 0x8c unites, sinon le
//     plancher +0x60 plus 0x6e.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>&amp;DAT_005528b0</c> — <b>les sorts de la Salamandre</b>, deux mots par variante
    /// (<c>+0x16</c>), famille dans l'octet haut, indice dans l'octet bas. Releves dans
    /// <c>.data</c> : <c>0000 000a | 000a 0014 | 0008 000c | 000c 0016</c>. Bornee en bas par la
    /// table de sorts de l'auberge et de la mine (<c>0x00552880</c>, quatre rangees de trois), en haut
    /// par la sonde de <c>0x005528c0</c>.
    /// </summary>
    private static readonly ushort[] s_SalamanderSpells_005528B0 =
    [
        0x0000, 0x000a,   // 0x005528b0 — variante 0 : Balle de feu, Mur de flammes
        0x000a, 0x0014,   // 0x005528b4 — variante 1 : Mur de flammes, Boule de feu
        0x0008, 0x000c,   // 0x005528b8 — variante 2 : Fleches magiques, Congelo
        0x000c, 0x0016,   // 0x005528bc — variante 3 : Congelo, Demon polaire
    ];

    /// <summary>
    /// Ghidra: <c>0x0048e3a0</c> — <b>l'intelligence de la Salamandre</b>. Voir l'en-tete du fichier.
    /// La copie <c>0x00497EF0</c> est branchee sur ce corps.
    /// </summary>
    private static void TickSceneEntityAi_0048E3A0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x28] — la sonde, dont deux mots sont poses avant le quatrieme push.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x32;
        probe[2] = 0x50;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            int phase = CrtRand();
            e[0x41] = 1;
            *(short*)(e + 0x14) = (short)SignedMask(phase, 0xfff);   // la phase du flottement
            PickChasedPartyMember_004A1020(e, 5, 0);   // Ghidra: FUN_0048c170, copie de FUN_004a1020
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

        // La distance est recalculee a plat, comme chez le Gremlin.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        if (speed != 0)
        {
            DecideSalamanderAction_0048E3A0(e, probe, chased, boldness, distance, heading, speed);
            FloatSalamander_0048EBF1(e, chased);
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
            // ================================================================ heurtee
            // Le Gremlin n'epargne que la posture 7 ; la Salamandre epargne aussi son sort.
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

            // Figee en plein crachat, elle le lache et se remet a l'attente.
            if (e[0x42] == 0x0c)
            {
                e[0x43] = 1;
                e[0x42] = 0;
            }

            return;
        }

        if (e[0x43] != 0) return;

        ReplanSalamanderAction_0048EEB6(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0048e52d..0x0048ebf1</c> — <b>le premier temps</b> : l'animation, la morsure
    /// portee a l'image 14, puis la posture. Treize cases, dont sept servies : 0 attendre,
    /// 1 se tourner, 2 approcher, 3 filer vers un cap, 7 marcher droit, 0x0a le sort,
    /// 0x0b la morsure, 0x0c le crachat.
    /// </summary>
    private static void DecideSalamanderAction_0048E3A0(
        byte* e, short* probe, byte* chased, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ la morsure
        if ((*(uint*)e & 2) != 0 && *(short*)(e + 0x1a) == 0x0e)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0
                && *(short*)(e + 0x1a) == 0x0e
                && *(short*)(prey + 0x96) == 0)
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

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 0;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(uint*)e |= 0x1000000;
                }

                *(int*)(e + 0x5c) = 0;

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    return;
                }

                // Quatre fois moins vif que le Gremlin : 0x28 au lieu de 0x80.
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x28 : -0x28 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 0;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0xa000;
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

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x28 : -0x28 - boldness);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x8000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
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
                *(int*)(e + 0x5c) = (boldness + 0x100) * 0x90;
                return;
            }

            // ---------------------------------------------------------- 0x0a : le sort
            case 0x0a:
                SalamanderCast_0048E8C0(e);
                return;

            // ---------------------------------------------------------- 0x0b : la morsure
            case 0x0b:
                SalamanderBite_0048EA7F(e, chased, boldness, distance, heading);
                return;

            // ---------------------------------------------------------- 0x0c : le crachat
            case 0x0c:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e &= 0xfeffffff;
                    *(short*)(e + 0x18) = 6;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    e[0x43] = (byte)(((0x40 - boldness) >> 2) + 0x32);
                }

                if (*(short*)(e + 0x12) != 1) return;

                if (((heading + 0x96) & 0xfff) > 0x12c && distance > 0x50)
                {
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x1e : -0x1e - boldness);
                }

                e[0x43]--;

                if (*(short*)(e + 0x1a) == 0x17 && distance < 0x9c4)
                    SpawnEffectEntity(e, 0xdf, 0, -1);              // Ghidra: FUN_0047a9d0
                return;

            // ---------------------------------------------------------- 4, 5, 6, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0048e8c0..0x0048ea7a</c> — <b>le sort</b>, posture 0x0a, en quatre phases.
    ///
    /// <para>
    /// <b>La meme ossature que <see cref="GremlinCast_004A7733"/>, trois differences.</b> Pas de
    /// refus quand la proie est trop haut ; le sort est tire a pile ou face dans la rangee de la
    /// variante (<see cref="s_SalamanderSpells_005528B0"/>) ; et <b>elle ne monte pas vers sa proie</b>
    /// pendant l'incantation. L'animation d'incantation est la 4, celle du lancer la 5, et le sort
    /// part a l'image 13.
    /// </para>
    /// <para>
    /// <b>Le meme registre non charge</b> : au-dela de la variante 3, le mot du sort est le compteur
    /// de la boucle de refus, <c>0x20</c> — famille 0 indice 32, hors table.
    /// </para>
    /// </summary>
    private static void SalamanderCast_0048E8C0(byte* e)
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
                    e[0x43] = 0;                                    // Ghidra: LAB_0048eb47
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
            short variant = *(short*)(e + 0x16);
            if (variant == 0) spell = s_SalamanderSpells_005528B0[0 + SignedMask(CrtRand(), 1)];
            if (*(short*)(e + 0x16) == 1) spell = s_SalamanderSpells_005528B0[2 + SignedMask(CrtRand(), 1)];
            if (*(short*)(e + 0x16) == 2) spell = s_SalamanderSpells_005528B0[4 + SignedMask(CrtRand(), 1)];
            if (*(short*)(e + 0x16) == 3) spell = s_SalamanderSpells_005528B0[6 + SignedMask(CrtRand(), 1)];

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

        if (*(short*)(e + 0x12) == 2 && *(short*)(e + 0x1a) == 0x0d)
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
    /// Ghidra: <c>0x0048ea7f..0x0048eb4b</c> — <b>la morsure</b>, posture 0x0b.
    ///
    /// <para>
    /// Elle rejoint la hauteur de sa proie <b>0x1e unites au-dessus</b>, d'un quart par image — et
    /// <b>continue meme trop bas</b> : a plus de 0xb4 unites sous sa proie elle perd le geste et la
    /// tenue, mais le rapprochement de l'image a lieu quand meme (le Gremlin, lui, s'arrete).
    /// </para>
    /// <para>
    /// L'image 12 de l'animation <b>arme le geste</b> et une poussee de
    /// <c>((hardiesse &amp; ~3) + 0x50) &lt;&lt; 10</c>. Jusqu'a l'image 13 elle vise ; passe 13 la
    /// poussee s'eteint d'un huitieme par image et, sous <c>0x5000</c>, le geste tombe. Le coup
    /// lui-meme est porte par le prelude de <see cref="DecideSalamanderAction_0048E3A0"/>, a
    /// l'image 14.
    /// </para>
    /// </summary>
    private static void SalamanderBite_0048EA7F(byte* e, byte* chased, int boldness, int distance, int heading)
    {
        int y = *(int*)(e + 0x20);

        if (y + 0xb4000 < *(int*)(chased + 0x20))
        {
            e[0x43] = 0;
            *(uint*)e &= 0xfffffffd;
        }

        *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y + 0x1e000) >> 2) + y;

        short frame = *(short*)(e + 0x1a);

        if (frame == 0x0c)
        {
            *(uint*)e |= 2;
            *(int*)(e + 0x5c) = ((boldness & ~3) + 0x50) << 0xa;
        }

        if (frame <= 0x0d)
        {
            if (((heading + 0x100) & 0xfff) <= 0x200) return;
            if (distance <= 0x50) return;

            *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x1e : -0x1e - boldness);
            return;
        }

        int thrust = *(int*)(e + 0x5c);
        thrust -= thrust >> 3;
        *(int*)(e + 0x5c) = thrust;
        if (thrust >= 0x5000) return;

        *(uint*)e &= 0xfffffffd;
        e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0048ebf1..0x0048ecb5</c> — <b>le flottement</b>.
    ///
    /// <para>
    /// A chaque image, sauf pendant le sort et le crachat quand leur geste est retombe, la phase
    /// <c>+0x14</c> avance de 50 a 99 pas et la hauteur rejoint d'un huitieme une cible qui ondule de
    /// <c>±60</c> unites sur le sinus de cette phase. <b>En regime <c>0x20</c></b>, la cible est
    /// l'altitude <c>+0x48</c> plus <c>0x8c</c> unites — et <c>+0x48</c> suit le plancher de la proie
    /// tant que celle-ci est dans le meme bloc de <c>0x1000</c> que son sol. <b>Sinon</b>, la cible
    /// est le plancher <c>+0x60</c> plus <c>0x6e</c> unites, et seul le sort retient le flottement
    /// (le crachat aussi, dans ce regime).
    /// </para>
    /// </summary>
    private static void FloatSalamander_0048EBF1(byte* e, byte* chased)
    {
        uint flags = *(uint*)e;
        byte posture = e[0x42];

        if ((flags & 0x20) != 0)
        {
            if (posture != 0x0a || (flags & 2) != 0)
            {
                short phase = AdvanceSalamanderBob(e);
                int y = *(int*)(e + 0x20);
                int wave = (int)Winsys3d.GetSinTableEntry(phase) * 60;
                *(int*)(e + 0x20) = ((*(int*)(e + 0x48) + wave - y + 0x8c000) >> 3) + y;
            }

            int floor = *(int*)(chased + 0x64);
            if (((*(int*)(chased + 0x20) ^ floor) & unchecked((int)0xfffff000)) == 0)
                *(int*)(e + 0x48) = floor;
            return;
        }

        if ((posture == 0x0c || posture == 0x0a) && (flags & 2) == 0) return;

        short bob = AdvanceSalamanderBob(e);
        int height = *(int*)(e + 0x20);
        int swing = (int)Winsys3d.GetSinTableEntry(bob) * 60;
        *(int*)(e + 0x20) = ((swing - height + *(int*)(e + 0x60) + 0x6e000) >> 3) + height;
    }

    /// <summary>Le pas de phase du flottement : <c>(phase + hasard % 50 + 50) &amp; 0xfff</c>.</summary>
    private static short AdvanceSalamanderBob(byte* e)
    {
        int step = CrtRand() % 0x32;
        short phase = (short)((*(short*)(e + 0x14) + step + 0x32) & 0xfff);
        *(short*)(e + 0x14) = phase;
        return phase;
    }

    /// <summary>
    /// Ghidra: <c>0x0048eeb6..0x0048f1f6</c> — <b>le troisieme temps</b> de la Salamandre.
    ///
    /// <para>
    /// Trois plages testees l'une apres l'autre (<c>0x96</c>, <c>0x258</c>). De pres, dans le cone
    /// et a portee, elle <b>mord</b> (posture 0x0b) une fois sur trois ; loin du cone elle se
    /// tourne. A moyenne distance, face a sa proie, elle <b>crache</b> ; le sort n'y est choisi que
    /// pour une variante au-dela de 1.
    /// </para>
    /// <para>
    /// <b>Une branche morte, fidele</b> : de loin, un tirage superieur ou egal a <c>0x4b</c> est
    /// ensuite compare a <c>0x3c</c> — la posture 1 qu'il armerait n'est jamais atteinte.
    /// </para>
    /// </summary>
    private static void ReplanSalamanderAction_0048EEB6(byte* e, int boldness, int timid)
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
                        *(uint*)e &= 0xfeffffff;
                        e[0x43] = 1;
                        *(int*)(e + 0x5c) = 0;
                        *(short*)(e + 0x18) = 3;
                        *(short*)(e + 0x1a) = 0;
                    }
                    else
                    {
                        e[0x42] = 0;
                        e[0x43] = 0;
                        *(short*)(e + 0x12) = 0;
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
                if (((heading + 0x1f4) & 0xfff) < 0x800)
                {
                    SpitAtPrey(e);
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
            else if (100 - roll < (boldness >> 1) + 10
                     && *(short*)(e + 0x16) != 0 && *(short*)(e + 0x16) != 1)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                SpitAtPrey(e);
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

    /// <summary>Ghidra: <c>LAB_0048ef9e</c>, <c>LAB_0048f099</c>, <c>LAB_0048f19c</c> — <b>filer
    /// ailleurs</b> : posture 3, une tenue tiree, et un cap entierement tire sur douze bits.</summary>
    private static void StrafeSalamander(byte* e, int timid)
    {
        e[0x42] = 3;
        *(short*)(e + 0x12) = 0;
        int wait = CrtRand() % 10;
        e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
        *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
    }

    /// <summary>Ghidra: <c>LAB_0048f0fc</c> — <b>cracher</b> : posture 0x0c, pour une image de
    /// tenue.</summary>
    private static void SpitAtPrey(byte* e)
    {
        e[0x42] = 0x0c;
        e[0x43] = 1;
        *(short*)(e + 0x12) = 0;
    }
}
