// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA SYLPHIDE — 0x004B4570, vieille tour (zone 4, sous-zones 3, 7, 9).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004B4570 — LA SYLPHIDE. Transcrite le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/OldTower/FUN_004B4570.asm, 621 instructions, absente de Blaze.c), deux
    // tables de sauts a quatre entrees, 0x004B4D88 (l'incantation) et 0x004B4D98 (les postures).
    //
    // LA CHARPENTE DU BEHEMOTH (Ai/Forest/Behemoth.cs) : une terrestre a deux machines a postures que
    // le bit 0x800 departage, les memes trois feuilles de queue (FUN_00488160, FUN_004dfaf0,
    // FUN_004e3770), les cones de virage constants. Mais tout est plus leger :
    //
    //   - GRAVITE 0x6000, celle du Grizzly, et PAS DE BRUIT DE PAS ;
    //   - DEUX GESTES AU LIEU DE TROIS, et aucun n'ampute l'attaque. Le coup (postures 0 et 1) se
    //     vise jusqu'a l'image 10, s'elance a l'image 11 et frappe a partir de la 13e. LE BOND
    //     (posture 2) se ramasse, saute a 0x18000 et frappe EN L'AIR a partir de l'image 13 — son
    //     animation est retenue a l'image 14 tant qu'elle monte. Les deux par la meme sonde, 0x00552A98.
    //   - SES SORTS (0x00552AB0, un tirage sur six) : Coup de jus, Mur de flammes, Congelo, Dodo,
    //     Gros sabots et Vitesse. Incantation sur l'animation 6, depart a l'image 19, et elle ne
    //     tourne pendant l'incantation que d'un cone fixe de 0x80.
    //   - UN SEUL TIRAGE PAR REPLANIFICATION, fait avant de mesurer la distance, et pas de portee
    //     minimale pour le coup.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00552a98</c> — <b>la sonde de la Sylphide</b>, relevee dans <c>.data</c> :
    /// <c>{0, 0, 0x32, 0x96, 0, 0x64}</c>. Sans avancee, et la plus haute tolerance verticale de la
    /// serie (<c>0x96</c>) — de quoi frapper en l'air. Le gabarit qui la suit, a <c>0x00552AA4</c>,
    /// porte les memes octets que la prise du Behemoth et n'est lu par personne ici.
    /// </summary>
    private static readonly short[] s_SylphideProbe_00552A98 = [0x0000, 0x0000, 0x0032, 0x0096, 0x0000, 0x0064];

    private static short* s_SylphideProbeStorage;

    /// <summary><see cref="s_SylphideProbe_00552A98"/> en memoire native, puisque
    /// <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* SylphideProbe()
    {
        if (s_SylphideProbeStorage == null)
        {
            s_SylphideProbeStorage = (short*)NativeMemory.Alloc((nuint)s_SylphideProbe_00552A98.Length, sizeof(short));
            for (int i = 0; i < s_SylphideProbe_00552A98.Length; i++) s_SylphideProbeStorage[i] = s_SylphideProbe_00552A98[i];
        }

        return s_SylphideProbeStorage;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552ab0</c> — <b>les six sorts de la Sylphide</b>, releves dans
    /// <c>.data</c> : <c>000b 000a 000c 0200 0201 0202</c>. Famille 0 : Coup de jus, Mur de flammes,
    /// Congelo ; famille 2 : Dodo, Gros sabots, Vitesse. Le corps tire <c>hasard % 6</c>.
    /// </summary>
    private static readonly ushort[] s_SylphideSpells_00552AB0 = [0x000b, 0x000a, 0x000c, 0x0200, 0x0201, 0x0202];

    /// <summary>
    /// Ghidra: <c>0x004b4570</c> — <b>l'intelligence de la Sylphide</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004B4570(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideSylphideAction_004B45C8(e, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        *(ushort*)(e + 0x9a) = 0x6000;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanSylphideAction_004B4B9E(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004b45c8..0x004b4b40</c> — <b>le premier temps</b> de la Sylphide : le geste en
    /// cours, sinon l'incantation, sinon la posture tenue.
    /// </summary>
    private static void DecideSylphideAction_004B45C8(byte* e, int distance, int heading, uint traits)
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
                case 0:
                case 1: SylphideStrike_004B4759(e, heading); return;
                case 2: SylphideLeap_004B4610(e, heading, flags); return;
                default: return;
            }
        }

        if ((flags & 0x10) != 0) return;

        // ================================================================ l'incantation
        if ((flags & 0x804) != 0)
        {
            if ((traits & 0x400003e8) != 0)
            {
                *(uint*)e = flags & 0xfffff7fb;
                e[0x43] = 0;
            }

            short state = *(short*)(e + 0x10);
            if ((uint)state > 3) return;

            switch (state)
            {
                // ---------------------------------------------------------- 0 : rendre la main
                case 0:
                {
                    uint f = *(uint*)e;
                    if ((f & 0x2000000) != 0) *(uint*)e = f & 0xfffff7ff;
                    return;
                }

                // ---------------------------------------------------------- 1 : lancer
                case 1:
                    if (*(ushort*)(e + 0x18) != 6)
                    {
                        *(int*)(e + 0x18) = 6;
                        *(uint*)e &= 0xfefffffb;
                        return;
                    }

                    if (*(short*)(e + 0x1a) != 0x13) return;

                    {
                        ushort sheet = *(ushort*)(e + 6);
                        CastSpell_0053A980((uint*)e, (sheet >> 8) & 0xff, sheet & 0xff, -1, -1);
                    }

                    *(short*)(e + 0x10) -= 1;
                    return;

                // ---------------------------------------------------------- 2 : incanter
                case 2:
                    if (--e[0x43] == 0) *(short*)(e + 0x10) = (short)(state - 1);
                    TurnSceneEntityTowardTarget(e, 0x80, heading);
                    return;

                // ---------------------------------------------------------- 3 : choisir le sort
                default:
                {
                    SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                    ushort sheet = s_SylphideSpells_00552AB0[CrtRand() % 6];
                    *(ushort*)(e + 6) = sheet;

                    byte* family = ((byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)))
                                       [(sheet >> 8) & 0xff];

                    *(short*)(e + 0x10) -= 1;
                    e[0x43] = family[(sheet & 0xff) * 0x30 + 0x13];
                    *(int*)(e + 0x5c) = 0;
                    return;
                }
            }
        }

        // ================================================================ la posture tenue
        if ((flags & 1) != 0) return;

        byte held = e[0x42];
        if (held > 3) return;

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
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags | 0x1000000;
                }

                // Ghidra: 0x004b49f1 — un cone de 0x100, le double du Behemoth.
                if (TurnSceneEntityTowardTarget(e, 0x100, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : tenir la distance
            case 2:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags | 0x1000000;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                *(int*)(e + 0x5c) = 0x14000;
                return;

            // ---------------------------------------------------------- 3 : marcher vers un cap
            default:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(ushort*)(e + 0x18) != 1)
                {
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags | 0x1000000;
                }

                *(int*)(e + 0x5c) = 0x10000;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                // Ghidra: 0x004b4b11 — 0x80, la ou le Behemoth tourne de 0x60.
                TurnSceneEntityTowardTarget(e, 0x80, heading);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004b4759..0x004b484a</c> — <b>le coup</b>, geste des postures 0 et 1.
    ///
    /// <para>
    /// Jusqu'a l'image 10 elle se replace (cone <c>0x40</c>) ; a l'image 11 elle s'elance
    /// (<c>0x1C000</c> dans la vitesse horizontale) ; a partir de la 13e elle balaie la sonde et
    /// frappe a pleine attaque. La fin de l'animation ferme le geste.
    /// </para>
    /// </summary>
    private static void SylphideStrike_004B4759(byte* e, int heading)
    {
        short frame = *(short*)(e + 0x1a);

        if (frame <= 0x0a) TurnSceneEntityTowardTarget(e, 0x40, heading);

        if (frame == 0x0b)
        {
            *(int*)(e + 0x5c) = 0x1c000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x1a) >= 0x0d) SylphideSweep(e);

        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0)
        {
            *(int*)(e + 0x5c) = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004b4610..0x004b4754</c> — <b>le bond</b>, geste de la posture 2, trois temps sur
    /// <c>+0x10</c>.
    ///
    /// <list type="number">
    /// <item><b>2</b> — elle se ramasse : la poussee perd un quart par image, elle se tourne (cone
    /// <c>0x40</c>), et l'image 12 passe au temps suivant.</item>
    /// <item><b>1</b> — elle bondit : <c>0x30000</c> dans la vitesse horizontale, <c>0x18000</c> vers le
    /// haut, bit 0 pose. Sans le son du Behemoth.</item>
    /// <item><b>0</b> — en l'air, tant qu'elle monte encore, l'animation est retenue a l'image 14, et a
    /// partir de l'image 13 elle frappe. Au sol, la fin de l'animation ferme le geste.</item>
    /// </list>
    /// </summary>
    private static void SylphideLeap_004B4610(byte* e, int heading, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 2)
        {
            int carried = *(int*)(e + 0x5c);
            *(int*)(e + 0x5c) = carried - (carried >> 2);

            if (*(short*)(e + 0x1a) == 0x0c) *(short*)(e + 0x10) = (short)(phase - 1);

            TurnSceneEntityTowardTarget(e, 0x40, heading);
            return;
        }

        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0x30000;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

            *(short*)(e + 0x10) -= 1;
            *(int*)(e + 0x54) = 0x18000;
            *(uint*)e |= 1;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (phase != 0) return;

        if ((flags & 1) == 0)
        {
            if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
            return;
        }

        if (*(int*)(e + 0x54) >= 0 && *(short*)(e + 0x1a) > 0x0e) *(short*)(e + 0x1a) = 0x0e;

        if (*(short*)(e + 0x1a) < 0x0d) return;

        SylphideSweep(e);
    }

    /// <summary>Ghidra: <c>0x004b46ee</c> et <c>0x004b47e8</c> — le balayage de
    /// <see cref="s_SylphideProbe_00552A98"/> et le coup a pleine attaque, emis deux fois.</summary>
    private static void SylphideSweep(byte* e)
    {
        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), SylphideProbe(), 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(victim + 0x96) = 0x10;
        *(uint*)victim |= 0x40;
        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x004b4b9e..0x004b4d85</c> — <b>le troisieme temps</b> de la Sylphide. Un seul
    /// tirage <c>hasard % 100</c>, fait avant de comparer la distance a <c>0x12c</c>.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : la moitie des tirages porte le coup si la cible est dans un huitieme de
    /// tour — sans condition de portee —, sinon se tourne ; un quart tient la distance a
    /// <c>0xc8</c> ; le reste erre.</item>
    /// <item><b>De loin</b> : le bond sur les tirages sous 30, l'incantation sous 60 (les deux dans un
    /// quart de tour, sinon se tourner), tenir la distance sous 80, errer sous 90, le repos au-dela.</item>
    /// </list>
    /// </summary>
    private static void ReplanSylphideAction_004B4B9E(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;

        if (distance < 0x12c)
        {
            if (roll < 0x32)
            {
                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Ghidra: 0x004b4c1b — armer LE COUP.
                uint flags = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                e[0x42] = 0;
                *(ushort*)(e + 0x10) = 2;
                *(uint*)e = (flags & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x4b)
            {
                HoldSylphideAtRange(e);
                return;
            }

            WanderSylphide(e, heading);
            return;
        }

        if (roll < 0x1e)
        {
            if (((heading + 0x200) & 0xfff) >= 0x400)
            {
                HesitateSceneEntity(e);
                return;
            }

            // Ghidra: 0x004b4c8a — armer LE BOND.
            uint flags = *(uint*)e;
            *(int*)(e + 0x18) = 8;
            e[0x42] = 2;
            *(ushort*)(e + 0x10) = 2;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        if (roll < 0x3c)
        {
            if (((heading + 0x200) & 0xfff) >= 0x400)
            {
                HesitateSceneEntity(e);
                return;
            }

            // Ghidra: 0x004b4ccb — armer L'INCANTATION.
            *(int*)(e + 0x18) = 5;
            *(uint*)e |= 0x1000804;
            *(ushort*)(e + 0x10) = 3;
            return;
        }

        if (roll < 0x50)
        {
            HoldSylphideAtRange(e);
            return;
        }

        if (roll < 0x5a)
        {
            WanderSylphide(e, heading);
            return;
        }

        e[0x42] = 0;
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);
    }

    /// <summary>Ghidra: <c>0x004b4c48</c> et <c>0x004b4cfe</c> — tenir la distance a <c>0xc8</c>,
    /// de 20 a 29 images.</summary>
    private static void HoldSylphideAtRange(byte* e)
    {
        e[0x42] = 2;
        int wait = CrtRand() % 10;
        *(ushort*)(e + 0x10) = 0xc8;
        e[0x43] = (byte)(wait + 0x14);
    }

    /// <summary>
    /// Ghidra: <c>LAB_004b4d28</c> — <b>l'errance de la Sylphide</b> : posture 3 pour
    /// <c>hasard % 10 + 10</c> images, vers un cap <c>hasard % 0xC00 + cap vers la cible + 0x200</c>.
    /// L'eventail est decentre : de <c>+0x200</c> a <c>+0xDFF</c> autour de la cible, jamais droit
    /// dessus.
    /// </summary>
    private static void WanderSylphide(byte* e, int heading)
    {
        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 10);

        int spread = CrtRand() % 0xc00;
        *(ushort*)(e + 0x10) = (ushort)(spread + heading + *(short*)(e + 0x32) + 0x200);
    }
}
