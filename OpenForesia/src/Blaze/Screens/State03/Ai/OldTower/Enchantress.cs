// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ENCHANTERESSE — 0x004B7E20, vieille tour (zone 4, sous-zones 3, 7, 9).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004B7E20 — L'ENCHANTERESSE. Transcrite le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/OldTower/FUN_004B7E20.asm, 533 instructions, absente de Blaze.c), deux tables
    // de sauts a quatre entrees : 0x004B851C (l'incantation) et 0x004B852C (les postures).
    //
    // LA SŒUR DU TENEBREUX (Ai/OldTower/Tenebrous.cs), dont elle garde le vol (onde 0x4100 << 5) et les
    // quatre postures tenues au mot pres, moins le battement d'ailes. A la place des trois gestes :
    //
    //   - UN SEUL GESTE, LA RUEE, arme sans toucher a la posture : a l'image 10 elle fond a 0x22000,
    //     puis balaie a pleine attaque TANT QU'ELLE FILE a 3 unites ou plus — a chaque image, sans bit
    //     de prise : elle peut toucher plusieurs fois. Pas de projection.
    //   - L'INCANTATION DU BEHEMOTH (bit 0x800, quatre etats sur +0x10), sur neuf tirages ponderes :
    //     Onde de choc, Eclair et Demon polaire deux fois chacun, Meteores une fois, Fusion deux fois.
    //     Animation 5, depart a l'image 24.
    //   - DE LOIN, TROIS FOIS SUR DIX ELLE INCANTE si elle fait face ; de pres, six fois sur dix elle rue.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00552b38</c> — <b>la sonde de l'Enchanteresse</b>, relevee dans <c>.data</c> :
    /// <c>{0, 30, 50, 120, 0, 90}</c>.
    /// </summary>
    private static readonly short[] s_EnchantressProbe_00552B38 = [0, 30, 50, 120, 0, 90];

    private static short* s_EnchantressProbeStorage;

    private static short* EnchantressProbe()
    {
        if (s_EnchantressProbeStorage == null)
        {
            s_EnchantressProbeStorage = (short*)NativeMemory.Alloc((nuint)s_EnchantressProbe_00552B38.Length, sizeof(short));
            for (int i = 0; i < s_EnchantressProbe_00552B38.Length; i++) s_EnchantressProbeStorage[i] = s_EnchantressProbe_00552B38[i];
        }

        return s_EnchantressProbeStorage;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552b48</c> — <b>les neuf tirages de sort de l'Enchanteresse</b>, releves
    /// dans <c>.data</c> : <c>0007 0007 0010 0010 0016 0016 001b 001c 001c</c>, tous de la famille 0.
    /// Onde de choc, Eclair et Demon polaire ont deux chances sur neuf, Meteores une, Fusion deux. Le
    /// corps tire <c>hasard % 9</c> ; le mot qui suit est nul.
    /// </summary>
    private static readonly ushort[] s_EnchantressSpells_00552B48 =
        [0x0007, 0x0007, 0x0010, 0x0010, 0x0016, 0x0016, 0x001b, 0x001c, 0x001c];

    /// <summary>
    /// Ghidra: <c>0x004b7e20</c> — <b>l'intelligence de l'Enchanteresse</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004B7E20(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideEnchantressAction_004B7E72(e, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        *(uint*)(e + 0x90) &= 0xdfffd8ff;
        HoverEnchantress_004B8326(e);
        InterruptFlyingEntity_004A1CF0((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanEnchantressAction_004B83D5(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004b8326..0x004b83a1</c> — <b>le vol</b> : celui du Tenebreux, sans l'exception du
    /// bit <c>0x400</c>.
    /// </summary>
    private static void HoverEnchantress_004B8326(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);
        if ((*chased & 1) == 0) *(int*)(e + 0x48) = *(int*)(chased + 0x60);

        *(short*)(e + 0x14) += (short)((CrtRand() & 0xff) + 0x100);

        int ceiling = *(int*)(e + 0x60);
        if (*(int*)(e + 0x48) >= ceiling) ceiling = *(int*)(e + 0x48);

        if ((*(uint*)e & 2) != 0)
        {
            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = y + ((ceiling - y + 0x28000) >> 3);
            return;
        }

        int height = *(int*)(e + 0x20);
        int lift = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) + 0x4100) << 5;
        *(int*)(e + 0x20) = height + ((lift + ceiling - height) >> 4);
    }

    /// <summary>
    /// Ghidra: <c>0x004b7e72..0x004b8308</c> — <b>le premier temps</b> de l'Enchanteresse : la ruee,
    /// sinon l'incantation, sinon la posture tenue.
    /// </summary>
    private static void DecideEnchantressAction_004B7E72(byte* e, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ la ruee
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                e[0x43] = 0;
                return;
            }

            EnchantressRush_004B7E99(e, heading);
            return;
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
                    if (*(ushort*)(e + 0x18) != 5)
                    {
                        *(int*)(e + 0x18) = 5;
                        *(uint*)e &= 0xfefffffb;
                        return;
                    }

                    if (*(short*)(e + 0x1a) != 0x18) return;

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

                    ushort sheet = s_EnchantressSpells_00552B48[CrtRand() % 9];
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
        byte held = e[0x42];
        if (held > 3) return;

        switch (held)
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                DampFlyingEntityPush(e);
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (TurnSceneEntityTowardTarget(e, 0x80, heading)) e[0x43] = 0;
                DampFlyingEntityPush(e);
                return;

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
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                RampFlyingEntityPush(e, 0x10000);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            default:
            {
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (e[0x43] != 0) e[0x43]--;

                *(int*)(e + 0x5c) = 0x4000;

                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004b7e99..0x004b7fd4</c> — <b>la ruee</b>, deux temps sur <c>+0x10</c>.
    ///
    /// <list type="number">
    /// <item><b>1</b> — la poussee est coupee ; a l'image 10 elle fond a <c>0x22000</c> selon son cap
    /// et garde <c>0x5000</c> de poussee. Elle vise dans un cone de <c>0x80</c>.</item>
    /// <item><b>0</b> — sous 8 unites de vitesse avec l'animation finie, le geste tombe (le seul bit
    /// <c>2</c>). Tant qu'elle file a 3 unites ou plus, elle vise dans un cone de <c>0x28</c> et
    /// balaie <b>a chaque image</b>, a pleine attaque : rien ne retient un second contact.</item>
    /// </list>
    /// </summary>
    private static void EnchantressRush_004B7E99(byte* e, int heading)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0;

            if (*(short*)(e + 0x1a) == 0x0a)
            {
                *(int*)(e + 0x5c) = 0x22000;

                int course = *(short*)(e + 0x32);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0x5000;
                *(short*)(e + 0x10) -= 1;
            }

            TurnSceneEntityTowardTarget(e, 0x80, heading);
            return;
        }

        if (phase != 0) return;

        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;
        int drift = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));

        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0 && drift <= 8) *(uint*)e = flags & 0xfffffffd;

        if (drift < 3) return;

        TurnSceneEntityTowardTarget(e, 0x28, heading);

        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), EnchantressProbe(), 0);
        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(uint*)victim |= 0x40;
        *(ushort*)(victim + 0x96) = 0x10;
        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x004b83d5..0x004b851a</c> — <b>le troisieme temps</b> de l'Enchanteresse. Un seul
    /// tirage, avant la comparaison a <c>0x12c</c>.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : six fois sur dix la ruee si elle fait face (sinon se tourner) — le geste
    /// est arme sans changer de posture ; le reste file vers un cap de <c>-0x600</c> a <c>+0x5FF</c>
    /// autour de la cible.</item>
    /// <item><b>De loin</b> : trois fois sur dix l'incantation si elle fait face (sinon se tourner) ;
    /// six fois sur dix s'approcher, 20 a 29 images ; le reste file.</item>
    /// </list>
    /// </summary>
    private static void ReplanEnchantressAction_004B83D5(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;
        bool facing = ((heading + 0x100) & 0xfff) <= 0x200;

        if (distance < 0x12c)
        {
            if (roll >= 0x3c)
            {
                WanderTenebrous(e, heading);
                return;
            }

            if (!facing)
            {
                HesitateSceneEntity(e);
                return;
            }

            // Ghidra: 0x004b8452 — armer LA RUEE.
            uint flags = *(uint*)e;
            *(int*)(e + 0x18) = 3;
            *(ushort*)(e + 0x10) = 1;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        if (roll < 0x1e)
        {
            if (!facing)
            {
                HesitateSceneEntity(e);
                return;
            }

            // Ghidra: 0x004b848e — armer L'INCANTATION.
            *(int*)(e + 0x18) = 4;
            *(uint*)e |= 0x1000804;
            *(ushort*)(e + 0x10) = 3;
            return;
        }

        if (roll < 0x5a)
        {
            e[0x42] = 2;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        WanderTenebrous(e, heading);
    }
}
