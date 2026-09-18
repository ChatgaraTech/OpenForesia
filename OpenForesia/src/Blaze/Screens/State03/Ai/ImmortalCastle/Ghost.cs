// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE FANTOME — 0x004C9E00, chateau de l'immortel (zone 6, sous-zones 0 a 4).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004C9E00 — LE FANTOME. Transcrit le 2026-09-18 au desassembleur
    // (Legacy/Raw/Blaze/ImmortalCastle/FUN_004C9E00.asm, 861 instructions, absent de Blaze.c),
    // table de sauts a TREIZE entrees a 0x004CA984.
    //
    // LA CHARPENTE DU VAMPIRE (Ai/ImmortalCastle/Vampire.cs) — meme sonde {0, 0x1e, 0x32, 0x64, 0,
    // 0x64}, meme lecture de situation, meme integration, meme pose au sol au masque 0x2000ad82,
    // meme replanification a trois plages, et les postures 0, 1, 2, 3, 7, 11 au mot pres. Il n'a ni
    // le bond ni le sort : SEPT postures servies sur treize (0, 1, 2, 3, 7, 11, 12).
    //
    // CE QUI FAIT LE FANTOME, ET C'EST AVANT LE RESTE :
    //
    //   - IL SCINTILLE ET IL RESPIRE. Chaque image, un mot de phase +0x14 — seme au hasard sur douze
    //     bits a sa premiere image, comme celui de la Sacuba — avance de 100 et retombe a zero
    //     passe 0x1000. De ce mot sortent SIX champs : les trois octets +0x38, +0x39 et +0x3a, qui
    //     valent tous (sinus >> 6) + 0x6e — donc un gris qui palpite, jamais une couleur — et les
    //     trois mots +0x28, +0x2a et +0x2c, qui valent tous (sinus >> 4) + 0x1000 : son echelle,
    //     donc il enfle et se retracte. Le bit 0x1000000 et le bit 0x1000 du mot d'etat sont leves
    //     a chaque image avec, ce que nul autre corps du chateau ne fait ;
    //   - SON COUP FRAPPE A L'IMAGE 6, et c'est A L'IMAGE 8 que le geste tombe — deux images plus
    //     tard, dans un bloc a part qui, lui, s'execute meme si l'image 6 est passee ;
    //   - LES POSTURES 0 A 3 NE TOUCHENT PAS A SON ANIMATION. Seules la marche (posture 7) et le
    //     nuage (posture 12) en changent : il n'a qu'une pose, et il la tient ;
    //   - SON NUAGE EST LE SOUS-TYPE 0xe0, et il ne tire pas son mode : +0x9a est sauve puis rendu
    //     sans avoir ete touche. Il n'attend pas non plus l'image 9 — il nait des que la posture est
    //     armee, et la posture se ferme dans la foulee.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004c9e00</c> — <b>l'intelligence du Fantome</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004C9E00(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — la sonde du Vampire, mot pour mot.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004c8d80, copie de FUN_004a1020
            *(short*)(e + 0x14) = (short)SignedMask(CrtRand(), 0xfff);
        }

        PulseGhost_004C9E68(e);

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004c9fc0 — la distance recalculee a trois dimensions.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideGhostAction_004CA00B(e, probe, boldness, distance, heading, speed);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanGhostAction_004CA678(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004c9e68..0x004c9f04</c> — <b>le scintillement</b>, avant tout le reste et quoi
    /// qu'il fasse. Le mot <c>+0x14</c> avance de <c>100</c> et retombe a zero passe <c>0x1000</c> ;
    /// son sinus donne la teinte (<c>&gt;&gt; 6</c>, plus <c>0x6e</c>, dans les trois octets
    /// <c>+0x38</c>, <c>+0x39</c> et <c>+0x3a</c>) et l'echelle (<c>&gt;&gt; 4</c>, plus
    /// <c>0x1000</c>, dans les trois mots <c>+0x28</c>, <c>+0x2a</c> et <c>+0x2c</c>).
    ///
    /// <para>Le binaire relit le sinus SIX fois, avec le meme argument a chaque fois : les trois
    /// octets sont donc toujours egaux entre eux, et les trois mots aussi. Garde tel quel.</para>
    /// </summary>
    private static void PulseGhost_004C9E68(byte* e)
    {
        *(uint*)e |= 0x1001000;

        *(short*)(e + 0x14) += 0x64;
        if (*(short*)(e + 0x14) > 0x1000) *(short*)(e + 0x14) = 0;

        short phase = *(short*)(e + 0x14);

        e[0x38] = (byte)(((int)Winsys3d.GetSinTableEntry(phase) >> 6) + 0x6e);
        e[0x39] = (byte)(((int)Winsys3d.GetSinTableEntry(phase) >> 6) + 0x6e);
        e[0x3a] = (byte)(((int)Winsys3d.GetSinTableEntry(phase) >> 6) + 0x6e);

        *(short*)(e + 0x28) = (short)(((int)Winsys3d.GetSinTableEntry(phase) >> 4) + 0x1000);
        *(short*)(e + 0x2a) = (short)(((int)Winsys3d.GetSinTableEntry(phase) >> 4) + 0x1000);
        *(short*)(e + 0x2c) = (short)(((int)Winsys3d.GetSinTableEntry(phase) >> 4) + 0x1000);
    }

    /// <summary>
    /// Ghidra: <c>0x004ca00b..0x004ca441</c> — <b>le premier temps</b> du Fantome. Treize cases,
    /// dont sept servies : 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 7 marcher droit,
    /// 11 le coup, 12 le nuage.
    /// </summary>
    private static void DecideGhostAction_004CA00B(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le coup, a l'image 6
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 6)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 6)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }
            }

            // Ghidra: LAB_004ca0e9 — DEUX IMAGES PLUS TARD le geste tombe, et ce bloc est hors de la
            // garde de l'image 6 : il s'execute quelle que soit l'image.
            if (*(short*)(e + 0x1a) == 8)
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffd;
            }
        }

        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = 0;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_004ca35a
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(short*)(e + 0x12) == 0) *(short*)(e + 0x12) = 1;

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004ca1e0
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
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                }

                if (*(short*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004ca1e0
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

                    int thrust = boldness * 0x90 + 0x5000;
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

                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;       // Ghidra: LAB_004ca35a
                return;
            }

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
                GhostSummonCloud_004CA39A(e, distance);
                return;

            // ---------------------------------------------------------- 4, 5, 6, 8, 9, 10 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004ca39a..0x004ca441</c> — <b>le nuage</b>, posture 12 : celui du Vampire prive
    /// de son tirage et de son attente.
    ///
    /// <list type="bullet">
    ///   <item>les deux memes balayages — les 32 effets de <c>0x005ff400</c>, puis l'octet
    ///     <c>+0x42</c> des 32 entites de <c>0x005ecaa0</c>, une seule invocatrice a la fois ;</item>
    ///   <item>le sous-type est <c>0xe0</c> ;</item>
    ///   <item><b>aucune attente et aucun tirage</b> : pas de garde sur l'image 9, et <c>+0x9a</c>
    ///     est sauve puis rendu <b>sans avoir ete touche</b>. Le mot rendu est le mot lu. Garde tel
    ///     quel.</item>
    /// </list>
    /// </summary>
    private static void GhostSummonCloud_004CA39A(byte* e, int distance)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            byte* effects = Mem.Entity(0x005ff400);
            for (int i = 0; i < 0x20; i++)
            {
                byte* effect = effects + i * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004ca43e
                    return;
                }
            }

            // Ghidra: 0x004ca3c9 — `mov ecx,5ECAE2h`, soit 0x005ecaa0 + 0x42.
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

        short mode = *(short*)(e + 0x9a);
        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);
        SpawnEffectEntity(e, 0xe0, 0, -1);                          // Ghidra: FUN_0047a9d0
        *(short*)(e + 0x9a) = mode;

        e[0x43] = 0;                                                // Ghidra: LAB_004ca43e
    }

    /// <summary>
    /// Ghidra: <c>0x004ca678..0x004ca981</c> — <b>le troisieme temps</b> du Fantome, les trois plages
    /// du Vampire avec le nuage partout ou celui-ci bondissait :
    ///
    /// <list type="bullet">
    /// <item><b>de pres</b> : sous 40 le coup, de 40 a 49 le nuage — les deux seulement si la cible
    ///   est dans le cone et assez proche, sinon le demi-tour ; de 60 a 69 le nuage si elle est
    ///   au-dela de <c>hardiesse + 100</c> ; tout le reste louvoie ;</item>
    /// <item><b>a mi-distance</b> : 0-19 le demi-tour, 20-29 le nuage, 30-39 l'approche, et au-dela
    ///   de 70 le nuage sur les hardis — sinon le louvoiement ;</item>
    /// <item><b>de loin</b> : 0-29 le repos, 30-49 le louvoiement, 50-69 le demi-tour, 70-99
    ///   l'approche. Pas de bond : il n'en a pas.</item>
    /// </list>
    /// </summary>
    private static void ReplanGhostAction_004CA678(byte* e, int boldness, int timid)
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
                    if (roll < 0x28)
                    {
                        e[0x42] = 0x0b;
                        e[0x43] = 1;
                        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                        *(int*)(e + 0x5c) = 0;
                    }
                    else
                    {
                        e[0x42] = 0x0c;                             // Ghidra: LAB_004ca739
                        e[0x43] = 1;
                        *(short*)(e + 0x12) = 0;
                    }
                }
                else
                {
                    e[0x42] = 1;                                    // Ghidra: LAB_004ca747
                    e[0x43] = 1;
                }
            }
            else if (roll >= 0x3c && roll < 0x46 && distance > boldness + 100)
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                StrafeSalamander(e, timid);                         // Ghidra: LAB_004ca941
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                e[0x42] = 1;
                e[0x43] = 1;
            }
            else if (roll < 0x1e)
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x14);
            }
            else if (roll >= 0x46 && 100 - roll < (boldness >> 1) + 5)
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                StrafeSalamander(e, timid);                         // Ghidra: LAB_004ca826
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

        if (far < 0x32)
        {
            StrafeSalamander(e, timid);                             // Ghidra: LAB_004ca8b8
            return;
        }

        if (far < 0x46)
        {
            e[0x42] = 1;
            e[0x43] = 1;
            return;
        }

        e[0x42] = 2;                                                // Ghidra: LAB_004ca913
        int approach = CrtRand() % 10;
        *(short*)(e + 0x10) = 0x64;
        e[0x43] = (byte)(approach + (timid >> 1) + 0x14);
    }
}
