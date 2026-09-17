// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE WYBARN — 0x004B7540, vieille tour (zone 4, sous-zones 3, 7, 9).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004B7540 — LE WYBARN. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/OldTower/FUN_004B7540.asm, 476 instructions, absente de Blaze.c), table de
    // sauts a quatre entrees a 0x004B7E10 pour les postures ; les gestes sont une cascade.
    //
    // UNE VOLANTE AUX GESTES DE BEHEMOTH. Le milieu est celui de la Coccinelle (Ai/Forest/Ladybug.cs) —
    // FUN_0049a8e0, FUN_004a1cf0, le vol cale sur l'altitude de la proie —, mais la charpente est celle
    // du Behemoth : trois gestes aiguilles par la posture, chacun sa petite machine sur +0x10.
    //
    //   - LA MORSURE (posture 0) : elle vise dans un cone de 0x40, balaie a chaque image, et UNE FOIS SUR
    //     CINQ A PEU PRES (tirage sous 30) le coup porte l'effet 7 de la table des pieges — un poison,
    //     tres probablement. Sa poussee rejoint 0x1E000 par quarts ; un rebours de 16 images la ferme.
    //   - LA PRISE (posture 1) : a l'image 8 elle fond sur sa proie a 0x38000, puis balaie tant qu'elle
    //     file a plus de 3 unites par image. Au premier contact elle PROJETTE la victime dans son propre
    //     cap vers la cible (30x), a moitie d'attaque, et seme 0x39 sur elle — la prise du Behemoth, en vol.
    //   - LE SOUFFLE (posture 2) : a la fin de l'animation, l'ancre 0 du modele (sa gueule) est rangee
    //     en +0x44/+0x48/+0x4c et l'entite 0xC0 + catalogue[0x0c] nait avec le parametre 2. Dix images
    //     d'attente ferment le geste. +0x48 est aussi l'altitude de vol : le souffle la reecrit.
    //   - ELLE VOLE PLUS HAUT QUE LA COCCINELLE (0x4B00 au lieu de 0x3200 dans l'onde, 0x64000 au lieu
    //     de 0x28000 en geste), et LES BATTEMENTS SONNENT : le son 0x47 a l'image 0 de l'animation 0.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00552b20</c> et <c>0x00552b2c</c> — <b>les deux sondes du Wybarn</b>, relevees
    /// dans <c>.data</c> : la morsure <c>{0, 100, 80, 150, 0, 100}</c>, la prise
    /// <c>{0, 50, 50, 150, 0, 100}</c>. Toutes deux a la tolerance verticale de 150.
    /// </summary>
    private static readonly short[] s_WybarnProbes_00552B20 =
    [
        0, 100, 80, 150, 0, 100,   // 0x00552b20 — la morsure
        0, 50, 50, 150, 0, 100,    // 0x00552b2c — la prise
    ];

    private static short* s_WybarnProbeStorage;

    /// <summary>Une rangee de <see cref="s_WybarnProbes_00552B20"/> en memoire native.</summary>
    private static short* WybarnProbe(int row)
    {
        if (s_WybarnProbeStorage == null)
        {
            s_WybarnProbeStorage = (short*)NativeMemory.Alloc((nuint)s_WybarnProbes_00552B20.Length, sizeof(short));
            for (int i = 0; i < s_WybarnProbes_00552B20.Length; i++) s_WybarnProbeStorage[i] = s_WybarnProbes_00552B20[i];
        }

        return s_WybarnProbeStorage + row * 6;
    }

    /// <summary>
    /// Ghidra: <c>0x004b7540</c> — <b>l'intelligence du Wybarn</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004B7540(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideWybarnAction_004B7593(e, model, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        *(uint*)(e + 0x90) &= 0xdfffd8ff;
        HoverWybarn_004B78A3(e);
        InterruptFlyingEntity_004A1CF0((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanWybarnAction_004B7950(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004b78a3..0x004b791c</c> — <b>le vol</b>. <see cref="HoverOnWings"/> a deux
    /// constantes pres : en geste la hauteur vise le plafond plus <c>0x64000</c> (100 unites), hors
    /// geste l'onde est centree sur <c>0x4B00</c> au lieu de <c>0x3200</c> — le Wybarn tient
    /// <c>0x12c</c> unites au-dessus de son plafond, la Coccinelle 200.
    /// </summary>
    private static void HoverWybarn_004B78A3(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);
        if ((*chased & 1) == 0) *(int*)(e + 0x48) = *(int*)(chased + 0x60);

        *(short*)(e + 0x14) += (short)((CrtRand() & 0xff) + 0x100);

        int ceiling = *(int*)(e + 0x60);
        if (*(int*)(e + 0x48) >= ceiling) ceiling = *(int*)(e + 0x48);

        if ((*(uint*)e & 2) != 0)
        {
            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = y + ((ceiling - y + 0x64000) >> 3);
            return;
        }

        int height = *(int*)(e + 0x20);
        int lift = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) + 0x4b00) << 6;
        *(int*)(e + 0x20) = height + ((lift + ceiling - height) >> 4);
    }

    /// <summary>
    /// Ghidra: <c>0x004b7593..0x004b7e08</c> — <b>le premier temps</b> du Wybarn : le geste en cours,
    /// sinon la posture tenue. Une entite qui incante ou se tient (<c>0x804</c>, <c>0x10</c>) ne fait
    /// rien de plus.
    /// </summary>
    private static void DecideWybarnAction_004B7593(byte* e, byte* model, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                e[0x43] = 0;
                return;
            }

            switch (e[0x42])
            {
                case 0: WybarnBite_004B7B03(e, heading); return;
                case 1: WybarnSeize_004B7657(e, model, heading); return;
                case 2: WybarnBreath_004B75D4(e, flags); return;
                default: return;
            }
        }

        if ((flags & 0x10) != 0) return;
        if ((flags & 0x804) != 0) return;

        // ================================================================ la posture tenue
        byte held = e[0x42];
        if (held <= 3)
        {
            switch (held)
            {
                // ---------------------------------------------------------- 0 : attendre
                case 0:
                    if (*(ushort*)(e + 0x18) != 0)
                    {
                        *(int*)(e + 0x18) = 0;
                        *(uint*)e = flags | 0x1000000;
                    }

                    if (distance < 0x12c)
                    {
                        e[0x43] = 0;
                        break;
                    }

                    if (e[0x43] != 0) e[0x43]--;
                    DampFlyingEntityPush(e);
                    break;

                // ---------------------------------------------------------- 1 : se tourner
                case 1:
                    if (*(ushort*)(e + 0x18) != 0)
                    {
                        *(int*)(e + 0x18) = 0;
                        *(uint*)e = flags | 0x1000000;
                    }

                    if (TurnSceneEntityTowardTarget(e, 0x80, heading)) e[0x43] = 0;
                    DampFlyingEntityPush(e);
                    break;

                // ---------------------------------------------------------- 2 : s'approcher
                case 2:
                    if (*(ushort*)(e + 0x18) != 1)
                    {
                        *(int*)(e + 0x18) = 1;
                        *(uint*)e = flags | 0x1000000;
                    }

                    if (distance < 0x12c)
                    {
                        e[0x43] = 0;
                        break;
                    }

                    if (e[0x43] != 0) e[0x43]--;

                    TurnSceneEntityTowardTarget(e, 0x80, heading);
                    RampFlyingEntityPush(e, 0xf000);
                    break;

                // ---------------------------------------------------------- 3 : filer vers un cap
                default:
                {
                    if (*(ushort*)(e + 0x18) != 0)
                    {
                        *(int*)(e + 0x18) = 0;
                        *(uint*)e = flags | 0x1000000;
                    }

                    if (e[0x43] != 0) e[0x43]--;

                    // Ghidra: 0x004b7d69 — le cap +0x10 pousse la VITESSE, pas la position.
                    *(int*)(e + 0x5c) = 0x5000;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                    *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    TurnSceneEntityTowardTarget(e, 0x80, heading);
                    break;
                }
            }
        }

        // Ghidra: LAB_004b7ddc — le battement d'ailes, a l'image 0 de l'animation 0.
        if (*(short*)(e + 0x18) == 0 && *(short*)(e + 0x1a) == 0)
        {
            Aud.PlayPositionalSound(0x47, 0x400, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004b7b03..0x004b7bea</c> — <b>la morsure</b>, geste de la posture 0.
    ///
    /// <para>
    /// L'animation 4 est posee une fois ; a chaque image elle vise (cone <c>0x40</c>) et balaie la
    /// sonde. Un contact frappe a pleine attaque et, sur un tirage <c>hasard % 100</c> sous 30, pose
    /// l'effet de piege <b>7</b> par <see cref="ApplyTrapEffect_0053C420"/> — le type d'effet est range
    /// dans l'octet <c>+0x07</c> de la bete juste avant. La poussee rejoint <c>0x1E000</c> par quarts,
    /// et le rebours <c>+0x43</c>, arme a 16 par la replanification, ferme le geste en arrivant a zero.
    /// </para>
    /// </summary>
    private static void WybarnBite_004B7B03(byte* e, int heading)
    {
        if (*(ushort*)(e + 0x18) != 4)
        {
            *(int*)(e + 0x18) = 4;
            *(uint*)e &= 0xfeffffff;
        }

        TurnSceneEntityTowardTarget(e, 0x40, heading);

        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), WybarnProbe(0), 0);

        if (*Mem.Camera(0x005de0b8) != 0)
        {
            *(ushort*)(victim + 0x96) = 0x10;
            *(uint*)victim |= 0x40;
            ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);

            if (CrtRand() % 100 < 0x1e)
            {
                e[7] = 7;
                ApplyTrapEffect_0053C420((uint*)e, (uint*)victim, 0);
            }

            Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20
        }

        int push = *(int*)(e + 0x5c);
        *(int*)(e + 0x5c) = push + ((0x1e000 - push) >> 2);

        if (e[0x43] == 0) return;
        if (--e[0x43] != 0) return;

        *(uint*)e &= 0xfffffffd;
    }

    /// <summary>
    /// Ghidra: <c>0x004b7657..0x004b7887</c> — <b>la prise</b>, geste de la posture 1, deux temps sur
    /// <c>+0x10</c>.
    ///
    /// <list type="number">
    /// <item><b>1</b> — la poussee est coupee ; a l'image 8, elle fond : <c>0x38000</c> dans la vitesse
    /// horizontale selon son cap, puis une poussee residuelle de <c>0x3000</c>, et le temps suivant.
    /// Elle vise dans un cone de <c>0x40</c>.</item>
    /// <item><b>0</b> — elle file. Sa vitesse horizontale, mesuree en unites, retombee a 5 ou moins
    /// avec l'animation finie, ferme le geste et la prise ; sous 3, elle ne cherche plus rien. Tant
    /// qu'aucune victime n'est prise (bit <c>0x20</c>), elle vise et balaie : le premier contact pose
    /// <c>0x20</c>, frappe a <b>moitie d'attaque</b>, projette la victime dans le cap
    /// <c>cap + ecart vers la cible</c> a 30 fois le sinus — et la fait decoller si elle etait au
    /// sol —, puis fait naitre <c>0x39</c> a la position de la victime.</item>
    /// </list>
    /// </summary>
    private static void WybarnSeize_004B7657(byte* e, byte* model, int heading)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0;

            if (*(short*)(e + 0x1a) == 8)
            {
                *(int*)(e + 0x5c) = 0x38000;

                int course = *(short*)(e + 0x32);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0x3000;
                *(short*)(e + 0x10) -= 1;
            }

            TurnSceneEntityTowardTarget(e, 0x40, heading);
            return;
        }

        if (phase != 0) return;

        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;
        int drift = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));

        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0 && drift <= 5) *(uint*)e = flags & 0xffffffdd;

        if (drift < 3) return;
        if ((*(uint*)e & 0x20) != 0) return;

        TurnSceneEntityTowardTarget(e, 0x40, heading);

        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), WybarnProbe(1), 0);
        if (victim != null) *(uint*)e |= 0x20;

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(victim + 0x96) = 0x10;
        *(uint*)victim |= 0x40;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20

        if ((*(uint*)victim & 0x40000000) != 0) return;

        *(int*)(victim + 0x50) = (int)Winsys3d.GetSinTableEntry(heading + *(short*)(e + 0x32)) * 30;
        *(int*)(victim + 0x58) = Winsys3d.GetCosTableEntry(heading + *(short*)(e + 0x32)) * 30;

        uint victimFlags = *(uint*)victim;
        if ((victimFlags & 1) == 0)
        {
            *(int*)(victim + 0x54) = 0x18000;
            *(uint*)victim = victimFlags | 1;
        }

        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(e + 0x20) = *(int*)(victim + 0x20);
        *(int*)(e + 0x24) = *(int*)(victim + 0x24);

        SpawnEffectEntity(e, 0x39, 0, -1);                          // Ghidra: FUN_0047a9d0

        *(int*)(e + 0x1c) = savedX;
        *(int*)(e + 0x20) = savedY;
        *(int*)(e + 0x24) = savedZ;
    }

    /// <summary>
    /// Ghidra: <c>0x004b75d4..0x004b7652</c> — <b>le souffle</b>, geste de la posture 2, deux temps
    /// sur <c>+0x10</c>.
    ///
    /// <list type="number">
    /// <item><b>1</b> — a la fin de l'animation, l'ancre 0 du modele (<see cref="ModelAnchorRow_00535770"/>)
    /// est rangee en <c>+0x44</c>, <c>+0x48</c>, <c>+0x4c</c> en unites de <c>0x1000</c>, et l'entite
    /// <c>0xC0 + catalogue[0x0c]</c> nait avec les parametres 0 et 2 ; le rebours passe a 10.</item>
    /// <item><b>0</b> — le rebours <c>+0x43</c> descend <b>sans garde</b> (un rebours deja nul repart
    /// de 255), et a zero le geste tombe.</item>
    /// </list>
    /// <para>La poussee est coupee a chaque image.</para>
    /// </summary>
    private static void WybarnBreath_004B75D4(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            if (--e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (phase != 1 || (flags & 0x2000000) == 0)
        {
            *(int*)(e + 0x5c) = 0;
            return;
        }

        short* mouth = ModelAnchorRow_00535770(e, 0);
        *(int*)(e + 0x44) = mouth[0] << 0xc;
        *(int*)(e + 0x48) = mouth[1] << 0xc;
        *(int*)(e + 0x4c) = mouth[2] << 0xc;

        byte* catalogue = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xcc));
        SpawnEffectEntity(e, (byte)(catalogue[0x0c] + 0xc0), 0, 2);  // Ghidra: FUN_0047a9d0

        *(short*)(e + 0x10) -= 1;
        e[0x43] = 0x0a;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004b7950..0x004b7b02</c> — <b>le troisieme temps</b> du Wybarn. Un seul tirage,
    /// fait avant de comparer la distance a <c>0xFA</c>.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : six fois sur dix la morsure si la cible est dans un huitieme de tour
    /// (sinon se tourner) ; le reste file vers un cap tire de <c>-0x600</c> a <c>+0x5FF</c> autour de
    /// la cible.</item>
    /// <item><b>De loin</b> : six fois sur dix, face a la cible, la prise sur un tirage impair et le
    /// souffle sur un tirage pair — hors du cone, s'approcher. Le reste file vers un cap a
    /// <c>±0x200</c> de la cible.</item>
    /// </list>
    /// </summary>
    private static void ReplanWybarnAction_004B7950(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;
        bool facing = ((heading + 0x100) & 0xfff) <= 0x200;

        if (distance < 0xfa)
        {
            if (roll < 0x3c)
            {
                if (!facing)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Ghidra: 0x004b79cd — armer LA MORSURE.
                e[0x42] = 0;
                *(uint*)e |= 2;
                e[0x43] = 0x10;
                return;
            }

            e[0x42] = 3;
            e[0x43] = (byte)(CrtRand() % 10 + 10);
            int spread = CrtRand() % 0xc00;
            *(ushort*)(e + 0x10) = (ushort)(spread + heading + *(short*)(e + 0x32) - 0x600);
            return;
        }

        if (roll < 0x3c)
        {
            if (!facing)
            {
                e[0x42] = 2;
                e[0x43] = (byte)(CrtRand() % 10 + 10);
                return;
            }

            uint flags = *(uint*)e;

            if ((roll & 1) != 0)
            {
                // Ghidra: 0x004b7a55 — armer LA PRISE.
                *(int*)(e + 0x18) = 3;
                e[0x42] = 1;
                *(ushort*)(e + 0x10) = 1;
            }
            else
            {
                // Ghidra: LAB_004b7a7b — armer LE SOUFFLE.
                *(int*)(e + 0x18) = 5;
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 1;
            }

            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 10);
        int offset = SignedMask(CrtRand(), 0x3ff);
        *(ushort*)(e + 0x10) = (ushort)(offset + heading + *(short*)(e + 0x32) - 0x200);
    }
}
