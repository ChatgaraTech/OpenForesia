// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GARDE GOLEM — 0x004A9150, vieille tour (zone 4, sous-zones 0, 1, 2, 4), et TROIS CORPS D'UN
// COUP : 0x004B3650 et 0x004AB940, les deux « Armure volante » des sous-zones 3, 7, 9 et 5, 6, 8, 10.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004A9150 — LE GARDE GOLEM, et L'ARMURE VOLANTE. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/OldTower/FUN_004A9150.asm, 1 115 instructions, absente de Blaze.c).
    //
    // LES TROIS SONT LA MEME ROUTINE. 0x004B3650 et 0x004AB940 ne different de 0x004A9150 que par
    // l'adresse de leur double table de sauts (un octet d'indice par posture, puis neuf cibles :
    // 0x004AA054/0x004AA030, 0x004B4554/0x004B4530, 0x004AC844/0x004AC820) — les trois tables
    // d'indices portent les memes quinze octets et les trois tables de cibles les memes decalages —,
    // et par la copie du chercheur de proie qu'elles appellent (0x004A8150, 0x004B3290, 0x004AC860,
    // toutes conformes a FUN_004A1020). Le nom d'espece ne dit rien : un golem et une armure.
    //
    // UNE VOLANTE A CINQ GESTES, SANS PARENT ECRIT : aucune routine du depot n'en approche a moins de
    // mille lignes de diff. Ses traits :
    //
    //   - ELLE PLANE BAS : sa hauteur rejoint d'un huitieme par image 130 unites (+-40 d'ondulation)
    //     au-dessus du plus haut des deux planchers, le sien ou celui de sa proie.
    //   - SA VARIANTE (+0x16) CHANGE L'ARME : la variante 1 frappe par la sonde B a chaque image ou
    //     son geste est arme, a moitie d'attaque, et PROJETTE sa victime (0x39 et un elan tire entre
    //     25 et 34) ; les autres frappent par la sonde A a l'image 10 et au-dela.
    //   - LA VARIANTE 2 EST UNE SENTINELLE : elle ne suit pas sa proie mais le premier enregistrement
    //     de la troisieme table d'entites (0x005FA5E0), sans hardiesse (timidite 0x40), porte le bit
    //     0x1000, plane a +-40 unites de la hauteur de ce point, et ignore deux bits de contact de plus
    //     (masque de heurt 0x8882). Sa replanification, elle, vise toujours +0x6c.
    //   - LE PIQUE (posture 10) : elle monte de 0xa unites par image ; passe 200 unites au-dessus de
    //     son plancher elle arme le geste et fond sur sa proie (40 unites au-dessus d'elle, ou 160
    //     unites SOUS elle pour la sentinelle), d'une moitie d'ecart par image, jusqu'a ce que son
    //     elan (0xF000, un trente-deuxieme de moins par image) tombe sous 0xA000.
    //   - LE PLONGEON (posture 11), comme celui du Sorcier, sans son refus quand la proie est haute.
    //   - LE RECUL (posture 14) : elle part a reculons, a `(hardiesse + 0x60) * vitesse / 16`.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004a9150</c> (et <c>0x004b3650</c>, <c>0x004ab940</c>) — <b>l'intelligence du
    /// Garde golem</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004A9150(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x3c] et [esp+0x30] — les deux sondes, celles du Pourpre preux.
        short* probes = stackalloc short[12];
        probes[0] = 0; probes[1] = 0x1e; probes[2] = 0x32; probes[3] = 0x64; probes[4] = 0; probes[5] = 0x64;
        probes[6] = 0; probes[7] = 0; probes[8] = 0x50; probes[9] = 0x64; probes[10] = 0; probes[11] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 0, 0);   // Ghidra: FUN_004a8150 (FUN_004b3290, FUN_004ac860)
        }

        // Ghidra: [esp+0x20] — la cible suivie : la proie, ou pour la sentinelle le premier
        // enregistrement de la troisieme table d'entites.
        byte* chased = *(byte**)(e + 0x6c);
        if (*(short*)(e + 0x16) == 2) chased = Mem.Entity(0x005fa5e0);

        *(uint*)e &= 0xfffffffe;
        if (*(short*)(e + 0x16) == 2) *(uint*)e |= 0x1000;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, timid, distance, heading, speed;
        uint traits;

        if (*(short*)(e + 0x16) != 2)
        {
            ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);
            timid = 0x40 - boldness;
        }
        else
        {
            ReadSentinelSituation_004A92AD(e, stats, &boldness, &timid, &heading, &speed, &traits);
            distance = 0;   // Ghidra: [esp+0x1c] n'est pas ecrit ici — recalcule juste apres
        }

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004a9352 — la distance recalculee a plat, vers la cible suivie.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        if (speed != 0)
        {
            DecideGuardGolemAction_004A9150(e, probes, model, chased, boldness, distance, heading, speed);
            HoverGuardGolem_004A9A71(e, chased);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);             // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        uint contact = *(uint*)(e + 0x90) & 0xfffff8ff;
        *(uint*)(e + 0x90) = contact;

        uint wallMask = 0x2000a882;
        if (*(short*)(e + 0x16) == 2)
        {
            *(uint*)(e + 0x90) = contact & 0xdfffcfff;
            wallMask = 0x8882;
        }

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (*(uint*)(e + 0x90) & 0x8000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e &= 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((*(uint*)(e + 0x90) & wallMask) != 0)
        {
            // ================================================================ heurtee
            if (e[0x42] != 7)
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

            int side = ProbeClearSide_004D5E60(e, wallMask);
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

        ReplanGuardGolemAction_004A9D5C(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a92ad..0x004a9347</c> — <b>la situation de la sentinelle</b> (variante 2), a
    /// la place de <see cref="ReadSceneEntitySituation_00535F20"/>.
    ///
    /// <para>
    /// Hardiesse nulle, timidite <c>0x40</c>, traits lus au bloc de statistiques. Le cap est pris
    /// vers le premier enregistrement de <c>0x005FA5E0</c> (<c>+0x1c</c>, <c>+0x24</c>), et tire au
    /// hasard si les traits portent <c>8</c>. La vitesse ne connait que trois regles sur les six de
    /// la lecture ordinaire : <c>0x8000</c>, <c>4</c> et <c>0x1c0</c> — ni <c>0x4000</c>, ni le bit
    /// <c>0x80</c> de l'entite, ni la tenue <c>+0x74</c>.
    /// </para>
    /// <para>La distance n'est pas calculee : l'appelant la recalcule aussitot.</para>
    /// </summary>
    private static void ReadSentinelSituation_004A92AD(
        byte* e, byte* stats, int* boldness, int* timid, int* heading, int* speed, uint* traits)
    {
        byte* anchor = Mem.Entity(0x005fa5e0);

        *boldness = 0;
        *timid = 0x40;
        *traits = *(uint*)(stats + 0x40);

        int dz = (*(int*)(anchor + 0x24) - *(int*)(e + 0x24)) >> 0xc;
        int dx = (*(int*)(anchor + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        *heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        if ((*traits & 8) != 0) *heading = CrtRand() & 0xfff;

        *speed = 0x1000;
        if ((*traits & 0x8000) != 0) *speed = 0xccc;
        if ((*traits & 4) != 0) *speed = 0x800;
        if ((*traits & 0x1c0) != 0) *speed = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004a938d..0x004a9a6e</c> — <b>le premier temps</b> : l'animation, le coup, puis
    /// la posture. Quinze postures par une table d'indices, huit servies : 0 attendre, 1 se tourner,
    /// 2 approcher, 3 filer vers un cap, 7 marcher droit, 10 le pique, 11 le plongeon, 14 reculer.
    /// </summary>
    private static void DecideGuardGolemAction_004A9150(
        byte* e, short* probes, byte* model, byte* chased, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le coup
        if ((e[0] & 2) != 0)
        {
            if (*(short*)(e + 0x16) != 1)
            {
                if (*(short*)(e + 0x1a) >= 0x0a)
                {
                    byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes, 0);

                    if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0a)
                    {
                        *(ushort*)(prey + 0x96) = 0x10;
                        *(uint*)prey |= 0x40;
                        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                    }
                }
            }
            else
            {
                GuardGolemRam_004A9483(e, probes + 6, model);
            }

            uint f = *(uint*)e;
            if ((f & 0x2000000) != 0 && e[0x42] != 0x0a)
            {
                *(uint*)e = f & 0xfffffffd;
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

                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;
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
                    ? boldness * 2 + 0x80
                    : -0x80 - boldness * 2);
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

                    // Ghidra: 0x004a9790 — huit fois la hardiesse.
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 8 + 0x32
                            : -0x32 - boldness * 8);
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x18) = 0;
                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 10 : le pique
            case 10:
                GuardGolemStoop_004A983C(e, chased, boldness, heading);
                return;

            // ---------------------------------------------------------- 11 : le plongeon
            case 11:
            {
                int y = *(int*)(e + 0x20);
                *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y + 0x28000) >> 2) + y;

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

            // ---------------------------------------------------------- 14 : reculer
            case 14:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) + 0x800);
                    int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) + 0x800);
                    int back = ((boldness + 0x60) * speed << 8) >> 0xc;

                    *(int*)(e + 0x1c) += (back * sin) >> 0xc;
                    *(int*)(e + 0x24) += (back * cos) >> 0xc;
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x18) = 0;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 4, 5, 6, 8, 9, 12, 13 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a9483..0x004a9554</c> — <b>le coup de la variante 1</b>, porte a chaque image
    /// ou le geste est arme, par la sonde B et a moitie d'attaque. Si la victime n'est pas portee
    /// (<c>0x40000000</c>), l'effet <c>0x39</c> nait et elle est projetee dans le cap du golem a
    /// <c>hasard % 10 + 25</c> par axe, <c>0x18000</c> de haut — deux tirages, un par axe.
    /// </summary>
    private static void GuardGolemRam_004A9483(byte* e, short* probe, byte* model)
    {
        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(victim + 0x96) = 0x10;
        *(uint*)victim |= 0x40;

        short attack = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(attack >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
        *(short*)(model + 0x30) = attack;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20

        if ((*(uint*)victim & 0x40000000) != 0) return;

        SpawnEffectEntity(e, 0x39, 0, -1);                          // Ghidra: FUN_0047a9d0
        *(uint*)victim |= 1;

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));
        *(int*)(victim + 0x50) = sin * (CrtRand() % 10 + 0x19);

        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32));
        *(int*)(victim + 0x54) = 0x18000;
        *(int*)(victim + 0x58) = cos * (CrtRand() % 10 + 0x19);
    }

    /// <summary>
    /// Ghidra: <c>0x004a983c..0x004a9991</c> — <b>le pique</b>, posture 10.
    ///
    /// <para>
    /// <b>Phase 1, la montee</b> : dix unites par image. Au-dela de 200 unites au-dessus de son
    /// plancher, le geste s'arme (animation 3 prise a l'image 8), l'elan passe a <c>0xF000</c>,
    /// <c>+0x14</c> repart de zero et la phase 2 commence — <b>dans la meme image</b>, car
    /// <c>+0x12</c> est relu apres.
    /// </para>
    /// <para>
    /// <b>Phase 2, la descente</b> : la hauteur rejoint de moitie par image celle de la cible suivie,
    /// 40 unites au-dessus — ou 160 unites en dessous pour la sentinelle. L'elan perd un
    /// trente-deuxieme par image ; quand sa partie entiere passe sous <c>0xA000</c>, le geste tombe et
    /// le rebours s'annule. <c>+0x14</c> compte les images, et rien ne le relit.
    /// </para>
    /// <para>Dans les deux phases elle vire de <c>hardiesse * 2 + 0x40</c> hors d'un cone de
    /// <c>0x100</c>.</para>
    /// </summary>
    private static void GuardGolemStoop_004A983C(byte* e, byte* chased, int boldness, int heading)
    {
        if (*(short*)(e + 0x12) == 1)
        {
            int y = *(int*)(e + 0x20) + 0xa000;
            *(int*)(e + 0x20) = y;

            if ((y - *(int*)(e + 0x60)) >> 0xc > 0xc8)
            {
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x12) = 2;
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 8;
                *(int*)(e + 0x5c) = 0xf000;
            }

            TurnGuardGolem(e, boldness, heading);
        }

        short phase = *(short*)(e + 0x12);

        if (phase == 2)
        {
            int y = *(int*)(e + 0x20);
            int gap = *(short*)(e + 0x16) == 2
                ? *(int*)(chased + 0x20) - y - 0xa0000
                : *(int*)(chased + 0x20) - y + 0x28000;

            *(int*)(e + 0x20) = (gap >> 1) + y;
            (*(short*)(e + 0x14))++;

            TurnGuardGolem(e, boldness, heading);

            int thrust = *(int*)(e + 0x5c);
            if ((thrust & unchecked((int)0xfffff000)) < 0xa000)
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffd;
            }

            *(int*)(e + 0x5c) = thrust - (thrust >> 5);
        }

        if (phase != 0) return;

        *(short*)(e + 0x12) = 1;
        *(uint*)e |= 0x1000000;
        *(short*)(e + 0x18) = 0;
        *(short*)(e + 0x1a) = 0;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Le virage du pique : <c>hardiesse * 2 + 0x40</c> hors d'un cone de <c>0x100</c>.</summary>
    private static void TurnGuardGolem(byte* e, int boldness, int heading)
    {
        if (((heading + 0x100) & 0xfff) <= 0x200) return;

        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
            ? boldness * 2 + 0x40
            : -0x40 - boldness * 2);
    }

    /// <summary>
    /// Ghidra: <c>0x004a9a71..0x004a9b60</c> — <b>le vol plane</b>, sauf pendant la montee du pique.
    ///
    /// <para>
    /// La phase <c>+0x14</c> avance de 100 a 149 pas, repliee sous <c>0x1000</c> par soustraction.
    /// La hauteur rejoint d'un huitieme par image <c>sin(phase) * 40 + 0x82000</c> au-dessus du plus
    /// haut des deux planchers <c>+0x60</c>, le sien ou celui de la cible — soit 130 unites, a 40
    /// pres. La sentinelle, elle, rejoint <c>sin(phase) * 40</c> autour de la hauteur <c>+0x20</c> du
    /// premier enregistrement de <c>0x005FA5E0</c>.
    /// </para>
    /// </summary>
    private static void HoverGuardGolem_004A9A71(byte* e, byte* chased)
    {
        if (e[0x42] == 0x0a && (e[0] & 2) == 0) return;

        int step = CrtRand() % 0x32;
        *(short*)(e + 0x14) += (short)(step + 0x64);
        if (*(short*)(e + 0x14) >= 0x1000) *(short*)(e + 0x14) -= 0x1000;

        int y = *(int*)(e + 0x20);

        if (*(short*)(e + 0x16) == 2)
        {
            int swing = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) * 40;
            *(int*)(e + 0x20) = ((*(int*)(Mem.Entity(0x005fa5e0) + 0x20) + swing - y) >> 3) + y;
            return;
        }

        if (*(int*)(chased + 0x60) > *(int*)(e + 0x60))
        {
            int wave = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) + 0x3400) * 40;
            *(int*)(e + 0x20) = ((wave - y + *(int*)(chased + 0x60)) >> 3) + y;
        }
        else
        {
            int wave = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) + 0x3400) * 40;
            *(int*)(e + 0x20) = ((wave - y + *(int*)(e + 0x60)) >> 3) + y;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a9d5c..0x004aa028</c> — <b>le troisieme temps</b> du Garde golem. Toujours
    /// vers <c>+0x6c</c>, sentinelle comprise.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> (sous <c>0xFA</c>) : la moitie des tirages tente le plongeon dans le cone
    /// et a portee — ou recule (posture 14) <c>hasard % 40 + 30</c> images sur les tirages de
    /// <c>0x1e</c> a <c>0x31</c> —, sinon se tourne ; un quart recule <c>hasard % 40</c> images ; le
    /// dernier quart approche, un rebours de <c>hasard % 10 + timidite / 2 + 40</c>.</item>
    /// <item><b>A mi-distance</b> : le pique, sauf deux tranches qui filent vers un cap tire ; face a
    /// la proie sur les tirages bas, sinon se tourner.</item>
    /// <item><b>De loin</b> : la meme fin que la Salamandre, branche morte comprise.</item>
    /// </list>
    /// </summary>
    private static void ReplanGuardGolemAction_004A9D5C(byte* e, int boldness, int timid)
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
                        e[0x42] = 0x0e;
                        e[0x43] = (byte)(CrtRand() % 0x28 + 0x1e);
                    }
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x4b)
            {
                e[0x42] = 0x0e;
                e[0x43] = (byte)(CrtRand() % 0x28);
            }
            else
            {
                // Ghidra: LAB_004aa001.
                e[0x42] = 2;
                int wait = CrtRand() % 10;
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(wait + (timid >> 1) + 0x28);
            }
        }

        // ================================================================ a moyenne distance
        if (distance < 0x258 && distance >= 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) < 0x800)
                {
                    StoopGuardGolem(e);
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x28 || (roll >= 0x3c && roll < 0x50))
            {
                StrafeSalamander(e, timid);
            }
            else
            {
                StoopGuardGolem(e);
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

    /// <summary>Ghidra: <c>LAB_004a9f06</c> — <b>armer le pique</b> : posture 10, une image de
    /// tenue.</summary>
    private static void StoopGuardGolem(byte* e)
    {
        e[0x42] = 0x0a;
        e[0x43] = 1;
        *(short*)(e + 0x12) = 0;
    }
}
