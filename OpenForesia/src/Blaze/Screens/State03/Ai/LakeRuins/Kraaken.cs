// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE KRAAKEN — les ruines dans le lac (zone 9, sous-zones 0 et 1). Trois routines, une creature :
//
//     0x004E4F20   le Kraaken          (corps)
//     0x004E40E0   le Kraaken bras
//     0x004E4890   le Kraaken pied
//
// Les noms viennent de Comportement.md. Toutes trois absentes de Blaze.c, transcrites au
// desassembleur le 2026-09-19 : Legacy/Raw/Blaze/LakeRuins/FUN_004E4F20.asm, FUN_004E40E0.asm,
// FUN_004E4890.asm.
//
// LA CHARPENTE EST CELLE DES BETES VOLANTES (Ai/Forest/Ladybug.cs) : integration par
// IntegrateFlyingEntityMotion_0049A8E0, interruption par InterruptFlyingEntity_004A1CF0. Mais le
// Kraaken NE VOLE PAS : apres l'integration, chacune des trois routines ECRASE sa hauteur par la
// constante 0xffe70000 (-25 unites) — c'est une bete qui nage a la surface. Et le drapeau 0x2000 de
// +0x90 est BASCULE (xor) a chaque image, puis 0x20000000 efface.
//
// Les trois partagent leurs quatre postures, a une difference pres (la posture 3 du pied vire vers
// un cap tire au hasard, celles du bras et du corps vers la cible), et le meme geste de base : une
// RUEE (+0x10 = 1 : a une image precise, 0x28000 de vitesse dans le cap) suivie d'une CHARGE
// (+0x10 = 0 : tant que la vitesse horizontale depasse 3, la bete balaie sa sonde). Ce qui les
// distingue :
//
//                     image de ruee   sonde        au coup
//     corps           9               0x00552e28   30 % : demi-coup, projection, effet 0x39
//     pied            13              0x00552e08   20 % : idem ; et 10 % de lancer un sort a la ruee
//     bras, geste 0   4               0x00552df0   un coup, entre les images 5 et 7
//     bras, geste 1   12              0x00552dfc   un coup, puis l'effet d'etat 7
//     bras, geste 2   —               —            fait naitre un creneau de debordement de la zone
//
// Le corps a en plus UNE INCANTATION (+0x10 de 3 a 0 sous le bit 0x804), la fiche 0/12.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>Ghidra: <c>DAT_00552e20</c> — le mot de sort du pied, lu en seize bits :
    /// famille 0 (octet haut), indice 2 (octet bas). Releve dans <c>Blaze.exe</c>.</summary>
    private const ushort c_KraakenFootSpell_00552E20 = 0x0002;

    /// <summary>Ghidra: <c>DAT_00552e40</c> — le mot de sort du corps : famille 0, indice 12.
    /// Releve dans <c>Blaze.exe</c>.</summary>
    private const ushort c_KraakenSpell_00552E40 = 0x000c;

    // =============================================================================================
    // LE CORPS — 0x004E4F20
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004e4f20</c> — <b>le Kraaken</b>. Voir le bandeau. Tables de sauts a
    /// <c>0x004e5694</c> (incantation) et <c>0x004e56a4</c> (postures).
    /// </summary>
    private static void TickSceneEntityAi_004E4F20(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);   // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        if (speed != 0) DecideKraakenAction_004E4F73(e, model, boldness, distance, heading, traits);

        SettleKraaken(e, speed, stats);

        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        // ---------------------------------------------------------------- replanifier
        KraakenAim(e, out distance, out heading);
        int roll = CrtRand() % 100;

        if (roll < 0x14)
        {
            if (((heading + 0x100) & 0xfff) <= 0x200) StartKraakenCharge(e);
            else StartKraakenTurn(e);
            return;
        }

        if (roll < 0x5a)
        {
            StartKraakenSwim(e);
            return;
        }

        // L'incantation : +0x10 part de 3, le bit 0x804 la garde.
        uint flags = *(uint*)e;
        *(int*)(e + 0x18) = 1;
        *(short*)(e + 0x10) = 3;
        *(uint*)e = flags | 0x1000804;
    }

    /// <summary>Ghidra: <c>0x004e4f73..0x004e553c</c> — le premier temps du corps.</summary>
    private static void DecideKraakenAction_004E4F73(
        byte* e, byte* model, int boldness, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste : ruee, charge
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
                return;
            }

            if (e[0x42] != 0) return;

            switch (*(short*)(e + 0x10))
            {
                case 1:
                    *(int*)(e + 0x5c) = 0;
                    if (*(short*)(e + 0x1a) == 9) KraakenLunge(e);
                    KraakenTurnWithin(e, heading, 0x80);
                    return;

                case 0:
                {
                    if (!KraakenChargeGoing(e)) return;
                    if ((*(uint*)e & 0x20) != 0) return;

                    KraakenTurnWithin(e, heading, 0x28);

                    short* probe = stackalloc short[6];   // Ghidra: &DAT_00552e28
                    probe[0] = 0; probe[1] = 0; probe[2] = 0x3c; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x64;

                    byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
                    if (prey != null) *(uint*)e |= 0x20;
                    if (*Mem.Camera(0x005de0b8) == 0) return;

                    *(ushort*)(prey + 0x96) = 8;
                    *(uint*)prey |= 0x40;
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

                    if (CrtRand() % 100 < 0x1e) KraakenHalfHitAndThrow(e, prey, model, heading);
                    else ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);             // Ghidra: FUN_00539b80

                    // Ghidra: 0x004e51f7 — le meme son, une seconde fois, dans tous les cas.
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                    return;
                }

                default:
                    return;
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

            short step = *(short*)(e + 0x10);
            switch ((uint)(int)step)
            {
                // Le sort parti : l'animation acheve, le bit 0x800 tombe.
                case 0:
                    if ((*(uint*)e & 0x2000000) != 0) *(uint*)e &= 0xfffff7ff;
                    return;

                // La posture d'incantation : a l'image 16, le sort.
                case 1:
                    if (*(short*)(e + 0x18) != 0)
                    {
                        *(int*)(e + 0x18) = 0;
                        *(uint*)e &= 0xfefffffb;
                        return;
                    }

                    if (*(short*)(e + 0x1a) == 0x10)
                    {
                        ushort spell = *(ushort*)(e + 6);
                        CastSpell_0053A980((uint*)e, (spell >> 8) & 0xff, spell & 0xff, -1, -1);   // Ghidra: FUN_0053a980
                        (*(short*)(e + 0x10))--;
                    }
                    return;

                // L'attente : +0x43 images en virant vers la cible.
                case 2:
                    e[0x43]--;
                    if (e[0x43] == 0) *(short*)(e + 0x10) = (short)(step - 1);
                    KraakenTurnWithin(e, heading, boldness * 4 + 0x32);
                    return;

                // L'entree : l'aureole 0xbd, le mot de sort, et la duree que dit sa fiche (+0x13).
                case 3:
                {
                    SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);            // Ghidra: FUN_0047aae0
                    CrtRand();                                          // Ghidra: 0x004e5317, tirage jete

                    ushort spell = c_KraakenSpell_00552E40;
                    *(ushort*)(e + 6) = spell;

                    byte* sheets = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c) + ((spell >> 8) & 0xff) * 4);
                    (*(short*)(e + 0x10))--;
                    e[0x43] = sheets[(spell & 0xff) * 0x30 + 0x13];
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                default:
                    return;
            }
        }

        if ((flags & 1) != 0) return;

        HoldKraakenPosture(e, boldness, distance, heading, heading);
    }

    // =============================================================================================
    // LE BRAS — 0x004E40E0
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004e40e0</c> — <b>le Kraaken bras</b>. Voir le bandeau. Table de sauts des
    /// postures a <c>0x004e4878</c>.
    /// </summary>
    private static void TickSceneEntityAi_004E40E0(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);     // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        if (speed != 0) DecideKraakenArmAction_004E4133(e, boldness, distance, heading, traits);

        SettleKraaken(e, speed, stats);

        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        // ---------------------------------------------------------------- replanifier
        KraakenAim(e, out distance, out heading);
        int roll = CrtRand() % 100;

        if (roll < 0x1e)
        {
            if (((heading + 0x100) & 0xfff) > 0x200)
            {
                StartKraakenTurn(e);
                return;
            }

            // Les gestes 0 et 1 a pile ou face : l'animation 2 ou 3.
            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
            int coin = CrtRand() & 1;
            *(short*)(e + 0x10) = 1;
            e[0x42] = (byte)coin;
            *(int*)(e + 0x18) = coin + 2;
            return;
        }

        if (roll < 0x28)
        {
            uint flags = *(uint*)e;
            *(int*)(e + 0x18) = 2;
            e[0x42] = 2;
            *(short*)(e + 0x10) = 1;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        if (roll < 0x50)
        {
            StartKraakenSwim(e);
            return;
        }

        e[0x42] = 2;
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);
    }

    /// <summary>Ghidra: <c>0x004e4133..0x004e46f2</c> — le premier temps du bras : trois
    /// gestes, puis les postures.</summary>
    private static void DecideKraakenArmAction_004E4133(byte* e, int boldness, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        if ((flags & 2) == 0)
        {
            if ((flags & 0x11) != 0) return;
            if ((flags & 0x804) != 0) return;
            HoldKraakenPosture(e, boldness, distance, heading, heading);
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
            // ------------------------------------------------------------ geste 0 : le coup court
            case 0:
                if (*(short*)(e + 0x10) == 1)
                {
                    *(int*)(e + 0x5c) = 0;
                    if (*(short*)(e + 0x1a) == 4) KraakenLunge(e);
                    KraakenTurnWithin(e, heading, 0x80);
                    return;
                }

                if (*(short*)(e + 0x10) != 0) return;
                if (!KraakenChargeGoing(e)) return;

                KraakenTurnWithin(e, heading, 0x28);

                if (*(short*)(e + 0x1a) < 5 || *(short*)(e + 0x1a) > 7) return;
                {
                    short* probe = stackalloc short[6];   // Ghidra: &DAT_00552df0
                    probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x82;
                    KraakenArmStrike(e, probe, statusEffect: false);
                }
                return;

            // ------------------------------------------------------------ geste 1 : le coup qui empoisonne
            case 1:
                if (*(short*)(e + 0x10) == 1)
                {
                    *(int*)(e + 0x5c) = 0;
                    if (*(short*)(e + 0x1a) == 0x0c) KraakenLunge(e);
                    KraakenTurnWithin(e, heading, 0x80);
                    return;
                }

                if (*(short*)(e + 0x10) != 0) return;
                if (!KraakenChargeGoing(e)) return;

                KraakenTurnWithin(e, heading, 0x28);

                if (*(short*)(e + 0x1a) < 0x0c) return;
                {
                    short* probe = stackalloc short[6];   // Ghidra: &DAT_00552dfc
                    probe[0] = 0; probe[1] = 0; probe[2] = 0x32; probe[3] = 0x64; probe[4] = 0; probe[5] = 0x50;
                    KraakenArmStrike(e, probe, statusEffect: true);
                }
                return;

            // ------------------------------------------------------------ geste 2 : l'appel
            case 2:
            {
                short phase = *(short*)(e + 0x10);

                if (phase == 1)
                {
                    if ((flags & 0x2000000) != 0)
                    {
                        e[0x43] = 0x28;
                        *(short*)(e + 0x10) = (short)(phase - 1);
                    }

                    if (*(short*)(e + 0x1a) == 6)
                    {
                        short* anchor = ModelAnchorRow_00535770(e, 0);   // Ghidra: FUN_00535770
                        *(int*)(e + 0x44) = anchor[0] << 0xc;
                        *(int*)(e + 0x48) = anchor[1] << 0xc;
                        *(int*)(e + 0x4c) = anchor[2] << 0xc;

                        byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xcc)))[0x0a] + 0xc0);
                        SpawnEffectEntity(e, subtype, 0, 1);                                  // Ghidra: FUN_0047a9d0
                        Aud.PlayPositionalSound(8, 0x653, 0x1800, (int*)(e + 0x1c));          // Ghidra: FUN_0053db20
                    }

                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (phase == 0)
                {
                    e[0x43]--;
                    if (e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;
                }

                *(int*)(e + 0x5c) = 0;
                return;
            }

            default:
                return;
        }
    }

    /// <summary>Ghidra: la frappe des gestes 0 et 1 du bras — la marque et le verrou poses AVANT
    /// le coup ; pour le geste 1, l'effet d'etat 7 ensuite (<c>e[7] = 7</c> puis
    /// <see cref="ApplyStatusEffect_0053B810"/>) ; et le son sur la proie.</summary>
    private static void KraakenArmStrike(byte* e, short* probe, bool statusEffect)
    {
        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
        if (prey != null) *(uint*)e |= 0x20;
        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(prey + 0x96) = 8;
        *(uint*)prey |= 0x40;
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);           // Ghidra: FUN_00539b80

        if (statusEffect)
        {
            e[7] = 7;
            ApplyStatusEffect_0053B810((uint*)e, (uint*)prey, 0);  // Ghidra: FUN_0053b810
        }

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
    }

    // =============================================================================================
    // LE PIED — 0x004E4890
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004e4890</c> — <b>le Kraaken pied</b>. Voir le bandeau. Table de sauts des
    /// postures a <c>0x004e4f10</c>.
    /// </summary>
    private static void TickSceneEntityAi_004E4890(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);   // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        if (speed != 0) DecideKraakenFootAction_004E48E3(e, model, boldness, distance, heading, traits);

        SettleKraaken(e, speed, stats);

        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        // ---------------------------------------------------------------- replanifier
        KraakenAim(e, out distance, out heading);
        int roll = CrtRand() % 100;

        if (roll < 0x1e)
        {
            if (((heading + 0x100) & 0xfff) <= 0x200 && distance < 0x190) StartKraakenCharge(e);
            else StartKraakenTurn(e);
            return;
        }

        if (roll < 0x46)
        {
            StartKraakenSwim(e);
            return;
        }

        e[0x42] = 2;
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);
    }

    /// <summary>Ghidra: <c>0x004e48e3..0x004e4b9d</c> et <c>0x004e4d0c..</c> — le premier temps
    /// du pied.</summary>
    private static void DecideKraakenFootAction_004E48E3(
        byte* e, byte* model, int boldness, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        if ((flags & 2) == 0)
        {
            if ((flags & 0x11) != 0) return;
            if ((flags & 0x804) != 0) return;

            // Ghidra: 0x004e4ec6 — la posture 3 du pied vire vers SON cap +0x10, pas vers la cible.
            int course = (*(short*)(e + 0x10) - *(short*)(e + 0x32)) & 0xfff;
            HoldKraakenPosture(e, boldness, distance, heading, course);
            return;
        }

        if ((traits & 0x10) != 0)
        {
            e[0x43] = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        if (e[0x42] != 0) return;

        switch (*(short*)(e + 0x10))
        {
            case 1:
                *(int*)(e + 0x5c) = 0;
                if (*(short*)(e + 0x1a) == 0x0d)
                {
                    KraakenLunge(e);

                    // Un dixieme des ruees lance le sort du pied.
                    if (CrtRand() % 100 < 10)
                    {
                        CrtRand();                                  // Ghidra: 0x004e498b, tirage jete
                        ushort spell = c_KraakenFootSpell_00552E20;
                        *(ushort*)(e + 6) = spell;
                        CastSpell_0053A980((uint*)e, (spell >> 8) & 0xff, spell & 0xff, -1, -1);   // Ghidra: FUN_0053a980
                    }
                }

                KraakenTurnWithin(e, heading, 0x80);
                return;

            case 0:
            {
                if (!KraakenChargeGoing(e)) return;
                if ((*(uint*)e & 0x20) != 0) return;

                KraakenTurnWithin(e, heading, 0x28);

                short* probe = stackalloc short[6];   // Ghidra: &DAT_00552e08
                probe[0] = 0; probe[1] = 0x1e; probe[2] = 0x28; probe[3] = 0x64; probe[4] = 0; probe[5] = 0xa0;

                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);
                if (prey != null) *(uint*)e |= 0x20;
                if (*Mem.Camera(0x005de0b8) == 0) return;

                *(ushort*)(prey + 0x96) = 8;
                *(uint*)prey |= 0x40;
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20

                if (CrtRand() % 100 < 0x14) KraakenHalfHitAndThrow(e, prey, model, heading);
                else ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);             // Ghidra: FUN_00539b80
                return;
            }

            default:
                return;
        }
    }

    // =============================================================================================
    // CE QUE LES TROIS PARTAGENT — recopie en ligne dans chacune, sorti ici pour qu'on lise les
    // ecarts plutot que les ressemblances.
    // =============================================================================================

    /// <summary>
    /// Ghidra: la queue commune — l'integration volante, <b>la hauteur ecrasee a
    /// <c>0xffe70000</c></b>, la pose, le drapeau <c>0x2000</c> de <c>+0x90</c> bascule et
    /// <c>0x20000000</c> efface, puis l'interruption volante.
    /// </summary>
    private static void SettleKraaken(byte* e, int speed, byte* stats)
    {
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);            // Ghidra: FUN_0049a8e0
        *(int*)(e + 0x20) = unchecked((int)0xffe70000);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940
        *(uint*)(e + 0x90) = (*(uint*)(e + 0x90) ^ 0x2000) & 0xdfffffff;
        InterruptFlyingEntity_004A1CF0((uint*)e, stats);           // Ghidra: FUN_004a1cf0
    }

    /// <summary>Ghidra: la tete de la replanification — distance a plat et cap relatif vers la
    /// cible <c>+0x6c</c>.</summary>
    private static void KraakenAim(byte* e, out int distance, out int heading)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));             // Ghidra: FUN_00545a10
        heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;     // Ghidra: FUN_00545a22
    }

    /// <summary>Le geste de base : animation 2, bit d'attaque, geste 0, phase de ruee.</summary>
    private static void StartKraakenCharge(byte* e)
    {
        uint flags = *(uint*)e;
        *(int*)(e + 0x18) = 2;
        e[0x42] = 0;
        *(short*)(e + 0x10) = 1;
        *(uint*)e = (flags & 0xfeffffff) | 2;
    }

    /// <summary>La posture 1, une image : se tourner d'un coup.</summary>
    private static void StartKraakenTurn(byte* e)
    {
        e[0x42] = 1;
        e[0x43] = 1;
    }

    /// <summary>La posture 3 : nager 10 a 19 images vers un cap tire au hasard, range en
    /// <c>+0x10</c>.</summary>
    private static void StartKraakenSwim(byte* e)
    {
        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 10);
        *(short*)(e + 0x10) = (short)CrtRand();
    }

    /// <summary>
    /// Ghidra: la ruee — <c>0x28000</c> de vitesse ajoutes dans le cap (sinus sur X, cosinus sur
    /// Z, au douzieme bit), la poussee ramenee a <c>0x5000</c>, et la phase <c>+0x10</c> passee a
    /// la charge.
    /// </summary>
    private static void KraakenLunge(byte* e)
    {
        *(int*)(e + 0x5c) = 0x28000;
        *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c) >> 0xc;   // Ghidra: FUN_00545a1c
        *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c) >> 0xc;        // Ghidra: FUN_00545a16
        *(int*)(e + 0x5c) = 0x5000;
        (*(short*)(e + 0x10))--;
    }

    /// <summary>
    /// Ghidra: la tete de la charge — la vitesse horizontale (racine de la somme des carres, au
    /// douzieme bit). A 5 ou moins et l'animation achevee, les bits <c>0x02</c> et <c>0x20</c>
    /// tombent : le geste est fini. <b>Sous 3, rien d'autre ne se passe.</b>
    /// </summary>
    private static bool KraakenChargeGoing(byte* e)
    {
        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;
        int pace = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));             // Ghidra: FUN_00545a10

        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0 && pace <= 5) *(uint*)e = flags & 0xffffffdd;

        return pace >= 3;
    }

    /// <summary>
    /// Ghidra: le virage borne, sous toutes ses formes du fichier — si l'ecart
    /// <paramref name="heading"/> tient dans <c>±step</c>, le cap l'absorbe ; sinon il tourne de
    /// <c>step</c> vers lui. Le test est <c>((ecart + step) &amp; 0xfff) &lt;= 2 * step</c>.
    /// </summary>
    private static void KraakenTurnWithin(byte* e, int heading, int step)
    {
        if (((heading + step) & 0xfff) <= step * 2) *(short*)(e + 0x32) += (short)heading;
        else *(short*)(e + 0x32) += (short)((heading & 0xfff) >= 0x800 ? -step : step);
    }

    /// <summary>
    /// Ghidra: le coup du corps (30 %) et du pied (20 %) — <b>la force du modele
    /// (<c>+0x30</c>) est divisee par deux le temps du coup</b>, puis rendue. Si la proie n'est pas
    /// hors d'atteinte (<c>0x40000000</c>), elle est <b>projetee</b> : 30 dans la direction de la
    /// bete vers elle, et <c>0x18000</c> vers le haut si elle etait au sol ; et l'effet
    /// <c>0x39</c> nait <b>a sa place</b> — la bete y est deplacee le temps de la naissance, puis
    /// remise.
    /// </summary>
    private static void KraakenHalfHitAndThrow(byte* e, byte* prey, byte* model, int heading)
    {
        short strength = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(strength >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);           // Ghidra: FUN_00539b80
        *(short*)(model + 0x30) = strength;

        if ((*(uint*)prey & 0x40000000) != 0) return;

        int direction = heading + *(short*)(e + 0x32);
        *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(direction) * 30;    // Ghidra: FUN_00545a1c
        *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(direction) * 30;         // Ghidra: FUN_00545a16

        uint preyFlags = *(uint*)prey;
        if ((preyFlags & 1) == 0)
        {
            *(int*)(prey + 0x54) = 0x18000;
            *(uint*)prey = preyFlags | 1;
        }

        int keptX = *(int*)(e + 0x1c);
        int keptY = *(int*)(e + 0x20);
        int keptZ = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) = *(int*)(prey + 0x1c);
        *(int*)(e + 0x20) = *(int*)(prey + 0x20);
        *(int*)(e + 0x24) = *(int*)(prey + 0x24);

        SpawnEffectEntity(e, 0x39, 0, -1);                          // Ghidra: FUN_0047a9d0

        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x20) = keptY;
        *(int*)(e + 0x24) = keptZ;
    }

    /// <summary>
    /// Ghidra: les quatre postures, identiques dans les trois routines (tables de sauts
    /// <c>0x004e56a4</c>, <c>0x004e4878</c>, <c>0x004e4f10</c>) a un ecart pres :
    /// <paramref name="swimCourse"/>, l'ecart vers lequel la posture 3 vire — la cible pour le
    /// corps et le bras, le cap tire <c>+0x10</c> pour le pied.
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///   <item><term>0</term><description>flotter : a moins de 300 de la cible, l'hesitation tombe ;
    ///     sinon elle decompte. La poussee s'amortit d'un seizieme.</description></item>
    ///   <item><term>1</term><description>se tourner, a <c>2 * hardiesse + 0x80</c> par
    ///     image ; aligne, l'hesitation tombe. Poussee amortie.</description></item>
    ///   <item><term>2</term><description>approcher : a moins de 300, l'hesitation tombe ; sinon
    ///     virer a <c>hardiesse / 4 + 0x38</c> et monter la poussee vers <c>0x10000</c> par
    ///     seiziemes.</description></item>
    ///   <item><term>3</term><description>nager : un coup de <c>0x3000</c> dans le cap
    ///     <c>+0x10</c>, poussee a zero, et virer a <c>hardiesse / 4 + 0x40</c>.</description></item>
    /// </list>
    /// </remarks>
    private static void HoldKraakenPosture(byte* e, int boldness, int distance, int heading, int swimCourse)
    {
        uint flags = *(uint*)e;

        switch (e[0x42])
        {
            case 0:
                KraakenRestPose(e, flags, 0);
                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                DampFlyingEntityPush(e);
                return;

            case 1:
            {
                KraakenRestPose(e, flags, 0);

                int step = boldness * 2 + 0x80;
                if (((step + heading) & 0xfff) <= step * 2)
                {
                    *(short*)(e + 0x32) += (short)heading;
                    e[0x43] = 0;
                }
                else
                {
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) >= 0x800 ? -step : step);
                }

                DampFlyingEntityPush(e);
                return;
            }

            case 2:
            {
                KraakenRestPose(e, flags, 0);
                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                KraakenTurnWithin(e, heading, (boldness >> 2) + 0x38);

                int push = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = ((0x10000 - push) >> 4) + push;
                return;
            }

            case 3:
            {
                KraakenRestPose(e, flags, 0);
                if (e[0x43] != 0) e[0x43]--;

                *(int*)(e + 0x5c) = 0x3000;
                *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * *(int*)(e + 0x5c) >> 0xc;   // Ghidra: FUN_00545a1c
                *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * *(int*)(e + 0x5c) >> 0xc;        // Ghidra: FUN_00545a16
                *(int*)(e + 0x5c) = 0;

                KraakenTurnWithin(e, swimCourse, (boldness >> 2) + 0x40);
                return;
            }

            default:
                return;
        }
    }

    /// <summary>Ghidra: l'entree de chaque posture — si l'animation <c>+0x18</c> n'est pas
    /// celle voulue (toujours 0 ici), elle est posee et le bit <c>0x1000000</c> la fait
    /// boucler.</summary>
    private static void KraakenRestPose(byte* e, uint flags, int animation)
    {
        if (*(short*)(e + 0x18) == animation) return;

        *(int*)(e + 0x18) = animation;
        *(uint*)e = flags | 0x1000000;
    }
}
