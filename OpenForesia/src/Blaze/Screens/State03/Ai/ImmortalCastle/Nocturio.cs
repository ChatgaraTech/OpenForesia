// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE NOCTURIO — 0x004DACC0, chateau de l'immortel (zone 6, sous-zones 5 et 11).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004DACC0 — LE NOCTURIO. Transcrit le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004DACC0.asm, 682 instructions, absent de Blaze.c), deux
    // tables de sauts a quatre entrees : 0x004DB578 (l'incantation) et 0x004DB588 (les postures).
    //
    // L'ENCHANTERESSE DE LA VIEILLE TOUR (Ai/OldTower/Enchantress.cs) AVEC LES APPELS DU ROI-ZOMBIE
    // (Ai/Labyrinth/ZombieKing.cs). Le squelette entier vient de la premiere — vol de la Coccinelle
    // (FUN_0049a8e0, FUN_004a1cf0), meme flottement a deux hauteurs, MEMES QUATRE POSTURES au mot
    // pres, meme machine a quatre etats pour l'incantation, meme ruee a l'image 10 —, et sa SONDE
    // 0x00552CC0 porte les six memes mots que celle de l'Enchanteresse : {0, 30, 50, 120, 0, 90}.
    //
    // CE QU'IL A EN PLUS :
    //
    //   - TROIS GESTES AU LIEU D'UN. Le bit 2 n'arme plus seulement la ruee : l'octet +0x42 choisit
    //     entre la RUEE (0) et DEUX APPELS (1 et 2), exactement ceux du Roi-zombie — le premier fait
    //     naitre le sous-type +0x0b du bloc de zone SUR UNE ANCRE DU MODELE (les trois vitesses sont
    //     remplacees par la ligne 0 de FUN_00535770 le temps de la naissance), le second le
    //     sous-type +0x0f avec la proie notee en +0x78. Comme lui, il ne sait pas ce qu'il appelle :
    //     le repertoire de zone 0x005e97cc le lui dit ;
    //   - SA RUEE EMPOISONNE. Apres le coup, un tirage sur cent : sous 20 l'effet de piege
    //     (FUN_0053c420), de 20 a 39 l'effet d'etat (FUN_0053b3c0), au-dela rien — et dans les deux
    //     cas son octet +0x07 passe a 7 avant l'appel ;
    //   - DE LOIN, IL TIRE SON GESTE AU SORT. La ou l'Enchanteresse incantait toujours, le Nocturio
    //     prend `tirage % 3` : l'incantation, le premier appel ou le second.
    //
    // Son incantation lance quatre fiches — Congelo, Demon polaire, Dodo, Chant sirenes —, sur
    // l'animation 6 (l'Enchanteresse : 5) et a l'image 29 (elle : 24).
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552cd0</c> — <b>les quatre tirages de sort du Nocturio</b>, releves dans
    /// <c>.data</c> : <c>000c 0016 0200 0207</c>, tires par <c>hasard &amp; 3</c> signe. Congelo et
    /// Demon polaire de la premiere famille, Dodo et Chant sirenes de la troisieme.
    /// </summary>
    private static readonly ushort[] s_NocturioSpells_00552CD0 = [0x000c, 0x0016, 0x0200, 0x0207];

    /// <summary>
    /// Ghidra: <c>0x004dacc0</c> — <b>l'intelligence du Nocturio</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004DACC0(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideNocturioAction_004DAD1D(e, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940
        *(uint*)(e + 0x90) &= 0xdfffd8ff;

        // Ghidra: 0x004db31b..0x004db395 — le vol de l'Enchanteresse, instruction pour instruction.
        HoverEnchantress_004B8326(e);

        InterruptFlyingEntity_004A1CF0((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanNocturioAction_004DB3C7(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004dad1d..0x004db2fd</c> — <b>le premier temps</b> du Nocturio : le geste arme,
    /// sinon l'incantation, sinon la posture tenue. Le geste se dedouble sur <c>+0x42</c> : la ruee,
    /// puis les deux appels.
    /// </summary>
    private static void DecideNocturioAction_004DAD1D(byte* e, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // Ghidra: appel, pas recopie
        uint flags = *(uint*)e;

        // ================================================================ le geste arme
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
                case 0: NocturioRush_004DAE96(e, heading); return;
                case 1: SummonAnchoredNocturio_004DADD1(e); return;
                case 2: SummonHomingNocturio_004DAD61(e); return;
                default: return;                                    // Ghidra: 0x004dad5b
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
                        *(int*)(e + 0x18) = 6;                      // l'Enchanteresse : animation 5
                        *(uint*)e &= 0xfefffffb;
                        return;
                    }

                    if (*(short*)(e + 0x1a) != 0x1d) return;        // elle : image 24

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

                    ushort sheet = s_NocturioSpells_00552CD0[SignedMask(CrtRand(), 3)];
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
    /// Ghidra: <c>0x004dae96..0x004dafee</c> — <b>la ruee</b>, geste 0 : celle de l'Enchanteresse,
    /// avec un effet d'etat au bout.
    ///
    /// <list type="number">
    /// <item><b>1</b> — a l'image 10 il fond a <c>0x22000</c> selon son cap et garde <c>0x5000</c> de
    /// poussee, puis vise dans un cone de <c>0x80</c>.</item>
    /// <item><b>0</b> — sous 8 unites de vitesse avec l'animation finie, le geste tombe. Tant qu'il
    /// file a 3 unites ou plus, il vise dans un cone de <c>0x28</c> et balaie <b>a chaque image</b>.
    /// Un tirage sur cent decide de l'effet : sous 20 le piege, de 20 a 39 l'etat, au-dela rien. Le
    /// son part dans les trois cas.</item>
    /// </list>
    ///
    /// <para>La sonde <c>0x00552cc0</c> porte les six memes mots que celle de l'Enchanteresse, d'ou
    /// l'appel a <see cref="EnchantressProbe"/>.</para>
    /// </summary>
    private static void NocturioRush_004DAE96(byte* e, int heading)
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

        // Ghidra: 0x004daf9e — l'octet +0x07 passe a 7 avant l'appel, dans les deux branches.
        int roll = CrtRand() % 100;

        if (roll < 0x14)
        {
            e[7] = 7;
            ApplyTrapEffect_0053C420((uint*)e, (uint*)victim, 0);
        }
        else if (roll < 0x28)
        {
            e[7] = 7;
            ApplyStatusEffect_0053B3C0((uint*)e, (uint*)victim, 0);
        }

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x004dadd1..0x004dae7f</c> — <b>le premier appel</b>, geste 1 : celui du Roi-zombie
    /// (<see cref="SummonAnchoredZombieKing_004C7454"/>) au mot pres, sauf que <c>+0x9a</c> est mis a
    /// <b>2</b> et non a 1. Les trois vitesses portent la ligne 0 des ancres du modele, decalee de
    /// douze bits, le temps que le sous-type <c>+0x0b</c> du bloc de zone naisse.
    /// </summary>
    private static void SummonAnchoredNocturio_004DADD1(byte* e)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0) { CountDownZombieKingSummon(e); return; }
        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }
        if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        short bob = *(short*)(e + 0x9a);
        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(short*)(e + 0x9a) = 2;

        short* anchor = ModelAnchorRow_00535770(e, 0);              // Ghidra: FUN_00535770
        *(int*)(e + 0x50) = anchor[0] << 0xc;
        *(int*)(e + 0x54) = anchor[1] << 0xc;
        *(int*)(e + 0x58) = anchor[2] << 0xc;

        SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0b), 0xff, -1);   // Ghidra: FUN_0047a9d0

        (*(short*)(e + 0x10))--;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x58) = vz;
        *(short*)(e + 0x9a) = bob;

        e[0x43] = 0x0a;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004dad61..0x004dadd0</c> — <b>le second appel</b>, geste 2 : la proie est notee
    /// dans <c>+0x78</c> et <c>+0x9a</c> mis a 2 le temps que le sous-type <c>+0x0f</c> du bloc de
    /// zone naisse. C'est <see cref="SummonHomingZombieKing_004C7503"/>.
    /// </summary>
    private static void SummonHomingNocturio_004DAD61(byte* e)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0) { CountDownZombieKingSummon(e); return; }
        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }
        if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);

        short bob = *(short*)(e + 0x9a);
        *(short*)(e + 0x9a) = 2;

        SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0f), 0xff, -1);   // Ghidra: FUN_0047a9d0

        (*(short*)(e + 0x10))--;
        *(short*)(e + 0x9a) = bob;

        e[0x43] = 0x0a;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004db3c7..0x004db577</c> — <b>le troisieme temps</b> du Nocturio. Un seul tirage,
    /// avant la comparaison a <c>0x12c</c>, comme chez l'Enchanteresse.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : six fois sur dix la ruee s'il fait face (sinon se tourner) ; le reste
    /// file vers un cap de <c>-0x600</c> a <c>+0x5FF</c> autour de la cible.</item>
    /// <item><b>De loin</b> : trois fois sur dix un geste s'il fait face — et lequel se tire a
    /// <c>tirage % 3</c> : l'incantation, le premier appel, le second —, cinq fois sur dix
    /// s'approcher 20 a 29 images, le reste file.</item>
    /// </list>
    /// </summary>
    private static void ReplanNocturioAction_004DB3C7(byte* e)
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

            // Ghidra: 0x004db448 — armer LA RUEE.
            uint flags = *(uint*)e;
            *(int*)(e + 0x18) = 3;
            e[0x42] = 0;
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

            // Ghidra: 0x004db48a — trois gestes, tires a `tirage % 3`.
            switch (roll % 3)
            {
                // ---------------------------------------------------------- 0 : l'incantation
                case 0:
                    *(int*)(e + 0x18) = 5;
                    *(uint*)e |= 0x1000804;
                    *(ushort*)(e + 0x10) = 3;
                    return;

                // ---------------------------------------------------------- 1 : le premier appel
                case 1:
                    *(int*)(e + 0x18) = 3;
                    e[0x42] = 1;
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    *(ushort*)(e + 0x10) = 1;
                    return;

                // ---------------------------------------------------------- 2 : le second appel
                case 2:
                    *(int*)(e + 0x18) = 3;
                    e[0x42] = 2;
                    *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                    *(ushort*)(e + 0x10) = 1;
                    return;

                default:
                    return;                                          // Ghidra: 0x004db49f
            }
        }

        if (roll < 0x50)
        {
            e[0x42] = 2;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        WanderTenebrous(e, heading);
    }
}
