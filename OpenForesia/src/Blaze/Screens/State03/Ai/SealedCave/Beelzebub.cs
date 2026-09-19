// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// BELZEBUTH — 0x00502CD0, grotte scellee (zone 10, sous-zones 13 a 15).
//
// Le nom vient de Comportement.md. 2 288 octets, absents de Blaze.c, transcrits au desassembleur
// le 2026-09-19 (lot 3 de la grotte) : Legacy/Raw/Blaze/SealedCave/FUN_00502CD0.asm. Tables de
// sauts a 0x00503594 (six gestes) et 0x005035ac (incantation).
//
// LE PLUS IMMOBILE DES QUATRE MAITRES DES SALLES DU FOND : trois postures (attendre, pivoter,
// approcher), jamais de marche, et il NE REPLANIFIE PAS SUR SES PROPRES MESURES — il reprend la
// distance a trois dimensions et le cap que lui a rendus ReadSceneEntitySituation_00535F20. Il ne
// pose pas non plus sa gravite : +0x9a reste ce que ses gestes en font. Sous 400 de distance, six
// fois sur dix un geste, tire sur six :
//
//     geste 0  animation 3   le coup, sous verrou 0x20, sonde 0x005531f8, a demi-degats ; puis
//                            l'effet 12 (FUN_0053b810, famille 7 posee en +0x07 comme chez le
//                            Kraaken) et la projection du Troll
//     geste 1  animation 4   a l'image 8, l'entite de l'octet 3 du bloc de zone (voix 0x25)
//     geste 2  animation 5   a l'image 9, l'entite 4 et le script 0x00553278
//     geste 3  animation 8   LA DISPARITION, phase portee par +0x43 : il s'enleve (0x18000 vers le
//                            haut, gravite nulle, bit 0x2000) en RETRECISSANT d'un quart par image
//                            (+0x28 / +0x2c) ; sous 0x20 il reparait a 800 unites au-dessus du sol
//                            et retombe en regrandissant, l'animation figee a l'image 12 ; au sol,
//                            l'entite 5 et le script 0x00553230
//     geste 4  animation 6   a l'image 9, l'entite 6 ; puis 20 images d'arret
//     geste 5  animation 6   a l'image 9, l'entite 7 ; puis 42 images d'arret
//
// Entre 400 et 800, pres d'une fois sur deux l'incantation (animation 10, le sort a l'image 18) :
// Mur de flammes, Ventilo, Demon polaire ou Meteores (0x00553208).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>Ghidra: <c>&amp;DAT_00553208</c> — <b>les sorts de Belzebuth</b>, tires sur
    /// quatre : 0/10 Mur de flammes, 0/22 Demon polaire, 0/21 Ventilo, 0/27 Meteores.</summary>
    private static readonly ushort[] s_BeelzebubSpells_00553208 = [0x000a, 0x0016, 0x0015, 0x001b];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00553210</c> — l'envol du geste 3 : quatre fois la voix <c>0x0a</c>, a deux
    /// images d'intervalle, montant. Borne par le <c>[1] = 0</c> du quatrieme.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00553210 =
    [
        0x0a, 0x02, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00553210
        0x0a, 0x02, 0x01, 0x00, 0xaa, 0x04, 0x00, 0x18,   // 0x00553218
        0x0a, 0x02, 0x01, 0x00, 0x54, 0x05, 0x00, 0x18,   // 0x00553220
        0x0a, 0x00, 0x01, 0x00, 0xa9, 0x05, 0x00, 0x18,   // 0x00553228 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00553210Storage;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00553230</c> — <b>la retombee</b> du geste 3. Neuf enregistrements, et
    /// <b>aucun ne termine le script</b> : le neuvieme (<c>0x00553270</c>) porte un delai de 2 et un
    /// compte de 0. Arrive la, la file ne joue plus rien et repasse sur lui toutes les deux images,
    /// sans jamais liberer l'emplacement — jusqu'a ce que l'anneau de 0x20 le reutilise. C'est la
    /// donnee de 1998, transcrite telle quelle ; la lecture ne va jamais au-dela de ces 72 octets.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00553230 =
    [
        0x0a, 0x02, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x00553230
        0x0a, 0x02, 0x01, 0x00, 0xa7, 0x07, 0x00, 0x18,   // 0x00553238
        0x0b, 0x01, 0x01, 0x00, 0x2a, 0x03, 0x00, 0x18,   // 0x00553240
        0x31, 0x01, 0x01, 0x00, 0x6a, 0x01, 0x00, 0x18,   // 0x00553248
        0x31, 0x01, 0x01, 0x00, 0x95, 0x01, 0x00, 0x18,   // 0x00553250
        0x20, 0x02, 0x01, 0x00, 0x53, 0x06, 0x00, 0x18,   // 0x00553258
        0x20, 0x02, 0x01, 0x00, 0x53, 0x06, 0x00, 0x18,   // 0x00553260
        0x20, 0x02, 0x01, 0x00, 0x53, 0x06, 0x00, 0x18,   // 0x00553268
        0x20, 0x02, 0x00, 0x00, 0x53, 0x06, 0x00, 0x18,   // 0x00553270 — compte 0, delai 2 : sans fin
    ];

    private static byte* s_DelayedSoundScript00553230Storage;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00553278</c> — le script du geste 2 : deux fois la voix 5. Borne par le
    /// <c>[1] = 0</c> du second ; <c>0x00553288</c> est une table de pointeurs de code.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00553278 =
    [
        0x05, 0x01, 0x01, 0x00, 0x2a, 0x03, 0x00, 0x18,   // 0x00553278
        0x05, 0x00, 0x01, 0x00, 0x7e, 0x03, 0x00, 0x18,   // 0x00553280 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00553278Storage;

    /// <summary>
    /// Ghidra: <c>0x00502cd0</c> — <b>l'intelligence de Belzebuth</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_00502CD0(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);   // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideBeelzebubAction_00502D2D(e, model, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_00503419 — sans gravite posee.
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanBeelzebubAction_0050346A(e, distance, heading);
    }

    /// <summary>
    /// Ghidra: <c>0x00502d2d..0x00503418</c> — <b>le premier temps</b>.
    /// </summary>
    private static void DecideBeelzebubAction_00502D2D(
        byte* e, byte* model, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ hors geste
        if ((flags & 2) == 0)
        {
            // Ghidra: LAB_00503192 — pas de test du bit « en l'air ».
            if ((flags & 0x10) != 0) return;

            if ((flags & 0x804) != 0)
            {
                AdvanceBeelzebubIncantation_005031A5(e, heading, traits, flags);
                return;
            }

            HoldBeelzebubStance_005032F8(e, distance, heading, flags);
            return;
        }

        if ((traits & 0x10) != 0)
        {
            e[0x43] = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        switch (e[0x42])
        {
            case 0: BeelzebubStrike_00502D69(e, model); return;

            // ---------------------------------------------------------- 1 : l'entite 3
            case 1:
                if (*(short*)(e + 0x1a) == 8)
                {
                    SpawnEffectEntity(e, ZombieKingSummonSubtype(3), 0xff, -1);   // Ghidra: FUN_0047a9d0
                    Aud.PlayPositionalSound(0x25, 0x6fd, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
                }

                CloseBeelzebubGestureOnEnd_00502F58(e);
                return;

            // ---------------------------------------------------------- 2 : l'entite 4
            case 2:
                if (*(short*)(e + 0x1a) == 9)
                {
                    SpawnEffectEntity(e, ZombieKingSummonSubtype(4), 0xff, -1);   // Ghidra: FUN_0047a9d0
                    Aud.QueueDelayedSoundScript(
                        NativeCopy(s_DelayedSoundScript_00553278, ref s_DelayedSoundScript00553278Storage),
                        (int*)(e + 0x1c));                           // Ghidra: FUN_0053dbe0
                }

                CloseBeelzebubGestureOnEnd_00502F58(e);
                return;

            case 3: BeelzebubVanish_00502F6E(e, flags); return;
            case 4: BeelzebubCall_0050309D(e, flags, 6, 0x14); return;
            case 5: BeelzebubCall_0050309D(e, flags, 7, 0x2a); return;
            default: return;
        }
    }

    /// <summary>Ghidra: <c>LAB_00502f58</c> — la fin d'animation ferme le geste.</summary>
    private static void CloseBeelzebubGestureOnEnd_00502F58(byte* e)
    {
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
    }

    /// <summary>
    /// Ghidra: <c>0x00502d69</c> — <b>le coup</b>, geste 0, animation 3. A l'image 10 un cri (voix
    /// <c>0x3c</c>) ; de la dixieme a la fin, une touche sous verrou.
    /// </summary>
    private static void BeelzebubStrike_00502D69(byte* e, byte* model)
    {
        if (*(short*)(e + 0x1a) == 0x0a)
        {
            Aud.PlayPositionalSound(0x3c, 0x400, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
        }

        if (*(short*)(e + 0x1a) >= 0x0a && (*(uint*)e & 0x20) == 0)
        {
            short* probe = stackalloc short[6];                      // Ghidra: &DAT_005531f8
            probe[0] = 0; probe[1] = 0x32; probe[2] = 0x64; probe[3] = 0xc8; probe[4] = 0; probe[5] = 0xc8;

            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
            if (prey != null) *(uint*)e |= 0x20;

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(prey + 0x96) = 0x10;
                *(uint*)prey |= 0x40;

                short power = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(power >> 1);
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                *(short*)(model + 0x30) = power;

                e[7] = 7;
                ApplyStatusEffect_0053B810((uint*)e, (uint*)prey, 0);   // Ghidra: FUN_0053b810

                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

                if ((*(uint*)prey & 0x40000000) == 0) ThrowTrollVictim_0049684C(e, prey);
            }
        }

        // Ghidra: LAB_00502eb8
        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xffffffdd;

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00502f6e</c> — <b>la disparition</b>, geste 3. Sa phase est <c>+0x43</c>, posee a
    /// 2 par la replanification ; les deux echelles <c>+0x28</c> / <c>+0x2c</c> bougent ensemble.
    /// </summary>
    private static void BeelzebubVanish_00502F6E(byte* e, uint flags)
    {
        byte phase = e[0x43];

        switch (phase)
        {
            // ---------------------------------------------------------- 2 : s'enlever en retrecissant
            case 2:
                if ((flags & 1) != 0)
                {
                    int scale = *(short*)(e + 0x28);
                    scale -= scale >> 2;
                    *(short*)(e + 0x28) = (short)scale;
                    *(short*)(e + 0x2c) = (short)scale;

                    if ((short)scale >= 0x20) return;

                    // Il a disparu : il reparait tres haut, sans echelle, et la gravite revient.
                    *(short*)(e + 0x28) = 0;
                    *(short*)(e + 0x2c) = 0;
                    *(int*)(e + 0x20) = *(int*)(e + 0x60) + 0x320000;
                    *(int*)(e + 0x54) = 0;
                    *(ushort*)(e + 0x9a) = 0x7fff;
                    e[0x43] = (byte)(phase - 1);
                    return;
                }

                // Ghidra: LAB_00502fd8 — au sol encore : l'envol.
                *(ushort*)(e + 0x9a) = 0;
                *(int*)(e + 0x54) += 0x18000;
                *(uint*)e = flags | 0x2001;
                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_00553210, ref s_DelayedSoundScript00553210Storage),
                    (int*)(e + 0x1c));                               // Ghidra: FUN_0053dbe0
                return;

            // ---------------------------------------------------------- 1 : retomber en regrandissant
            case 1:
            {
                int scale = *(short*)(e + 0x28);
                scale += (0x1000 - scale) >> 2;
                *(short*)(e + 0x28) = (short)scale;
                *(short*)(e + 0x2c) = (short)scale;

                if ((flags & 1) != 0)
                {
                    if (*(short*)(e + 0x1a) > 0x0c) *(short*)(e + 0x1a) = 0x0c;
                    return;
                }

                // Ghidra: LAB_0050303c — au sol.
                SpawnEffectEntity(e, ZombieKingSummonSubtype(5), 0xff, -1);   // Ghidra: FUN_0047a9d0

                e[0x43]--;
                *(uint*)e &= 0xffffdfff;

                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_00553230, ref s_DelayedSoundScript00553230Storage),
                    (int*)(e + 0x1c));                               // Ghidra: FUN_0053dbe0
                return;
            }

            // ---------------------------------------------------------- 0 : rendre la main
            case 0:
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
                return;

            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0050309d</c> (geste 4) et <c>0x0050310a</c> (geste 5) — deux copies qui ne
    /// different que par l'octet du bloc de zone (6 / 7) et l'arret (20 / 42 images). Phase en
    /// <c>+0x43</c> : 2, la naissance a l'image 9 puis l'arret arme en fin d'animation ; 1, l'arret
    /// s'ecoule en <c>+0x10</c>, puis le geste tombe.
    /// </summary>
    private static void BeelzebubCall_0050309D(byte* e, uint flags, int offset, short hold)
    {
        byte phase = e[0x43];

        if (phase == 2)
        {
            if (*(short*)(e + 0x1a) == 9)
            {
                SpawnEffectEntity(e, ZombieKingSummonSubtype(offset), 0xff, -1);   // Ghidra: FUN_0047a9d0
            }

            if ((*(uint*)e & 0x2000000) == 0) return;

            *(short*)(e + 0x10) = hold;
            e[0x43]--;
            return;
        }

        if (phase != 1) return;

        // Ghidra: LAB_00503171
        if (*(short*)(e + 0x10) != 0)
        {
            *(short*)(e + 0x10) -= 1;
            return;
        }

        *(uint*)e = flags & 0xfffffffd;
        e[0x43] = (byte)(phase - 1);
    }

    /// <summary>
    /// Ghidra: <c>0x005031a5</c> — <b>l'incantation</b>, table <c>0x005035ac</c> : celle d'Eifridt
    /// sur l'animation 10, le sort a l'image 18, quatre sorts.
    /// </summary>
    private static void AdvanceBeelzebubIncantation_005031A5(byte* e, int heading, uint traits, uint flags)
    {
        if ((traits & 0x400003e8) != 0)
        {
            flags &= 0xfffff7fb;
            e[0x43] = 0;
            *(uint*)e = flags;
        }

        short state = *(short*)(e + 0x10);
        if ((uint)state > 3) return;

        switch (state)
        {
            case 0:
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffff7ff;
                return;

            case 1:
                if (*(ushort*)(e + 0x18) != 0x0a)
                {
                    *(int*)(e + 0x18) = 0x0a;
                    *(uint*)e = *(uint*)e & 0xfefffffb;
                    return;
                }

                if (*(short*)(e + 0x1a) != 0x12) return;

                {
                    ushort sheet = *(ushort*)(e + 6);
                    CastSpell_0053A980((uint*)e, (sheet >> 8) & 0xff, sheet & 0xff, -1, -1);
                }

                *(short*)(e + 0x10) -= 1;
                return;

            case 2:
                if (--e[0x43] == 0) *(short*)(e + 0x10) = (short)(state - 1);

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                return;

            default:
            {
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                ushort sheet = s_BeelzebubSpells_00553208[CrtRand() % 4];
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

    /// <summary>
    /// Ghidra: <c>0x005032f8..0x00503418</c> — <b>la posture tenue</b>, trois cases seulement, toutes
    /// sur l'animation 1. Le pivot et l'approche tournent dans un cone de <c>0x100</c>.
    /// </summary>
    private static void HoldBeelzebubStance_005032F8(byte* e, int distance, int heading, uint flags)
    {
        byte stance = e[0x42];
        if (stance > 2) return;

        if (*(short*)(e + 0x18) != 1)
        {
            *(uint*)e = flags | 0x1000000;
            *(int*)(e + 0x18) = 1;
        }

        switch (stance)
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : pivoter
            case 1:
                if (TurnSceneEntityTowardTarget(e, 0x100, heading)) e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 2 : approcher
            default:
                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                TurnSceneEntityTowardTarget(e, 0x100, heading);
                *(int*)(e + 0x5c) = 0x14000;
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0050346a..0x00503590</c> — <b>le troisieme temps</b>, sur la distance et le cap
    /// de la lecture de situation.
    ///
    /// <para>
    /// <b>Sous 400</b> : un tirage sur cent — six gestes par tranches de dix (les gestes 3, 4 et 5
    /// portent leur phase en <c>+0x43 = 2</c>), 10 % pivoter, 30 % l'approche a 400. <b>De 400 a
    /// 800</b> : un second tirage, 45 % l'incantation si la cible est dans le cone de 0x100 (sinon
    /// pivoter), le reste l'approche a 400. <b>Au-dela</b> : l'approche a 200. Toujours trente images.
    /// </para>
    /// </summary>
    private static void ReplanBeelzebubAction_0050346A(byte* e, int distance, int heading)
    {
        int roll = CrtRand() % 100;

        if (distance < 0x190)
        {
            if (roll < 0x3c)
            {
                if (roll < 0x0a) { e[0x42] = 0; *(int*)(e + 0x18) = 3; }
                else if (roll < 0x14) { e[0x42] = 1; *(int*)(e + 0x18) = 4; }
                else if (roll < 0x1e) { e[0x42] = 2; *(int*)(e + 0x18) = 5; }
                else
                {
                    if (roll < 0x28) { e[0x42] = 3; *(int*)(e + 0x18) = 8; }
                    else
                    {
                        e[0x42] = (byte)(roll < 0x32 ? 4 : 5);
                        *(int*)(e + 0x18) = 6;
                    }

                    e[0x43] = 2;
                }

                // Ghidra: LAB_005034ef
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x46)
            {
                EifridtTurn(e);
                return;
            }

            BeelzebubApproach(e, 0x190);
            return;
        }

        if (distance >= 0x320)
        {
            BeelzebubApproach(e, 0xc8);
            return;
        }

        // Ghidra: 0x00503523
        int second = CrtRand() % 100;
        if (second < 0x2d)
        {
            if (((heading + 0x100) & 0xfff) > 0x200)
            {
                EifridtTurn(e);
                return;
            }

            *(int*)(e + 0x18) = 9;
            *(short*)(e + 0x10) = 3;
            *(uint*)e |= 0x1000804;
            return;
        }

        BeelzebubApproach(e, 0x190);
    }

    /// <summary>Ghidra: <c>LAB_00503567</c> / <c>LAB_0050357b</c> — approcher, trente images.</summary>
    private static void BeelzebubApproach(byte* e, short range)
    {
        e[0x42] = 2;
        e[0x43] = 0x1e;
        *(short*)(e + 0x10) = range;
    }
}
