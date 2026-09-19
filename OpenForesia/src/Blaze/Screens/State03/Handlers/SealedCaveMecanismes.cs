// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MECANISMES DE LA GROTTE SCELLEE — zone 10, table 0x0056adc8.
//
// Ouvert le 2026-09-19 au lot 2 de la zone, avec son premier corps qui COMPTE LE GROUPE — la
// dalle de pression 0x00468850 et ses deux copies. C'est le test d'appartenance
// d'ARCHITECTURE.md, et la regle convenue pour la grotte : ce fichier s'ouvre quand un corps le
// demande.
//
// Dumps : Legacy/Raw/Blaze/SealedCave/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00468850</c> (bloc 0), <c>0x0046b340</c> (2-3), <c>0x0046d760</c> (5-8-10) —
    /// trois corps sans aucune immediate differente. Absents de <c>Blaze.c</c> ; 73 instructions.
    ///
    /// <para>
    /// <b>La dalle de pression.</b> Elle retient sa hauteur de repos. Tant qu'un membre au moins
    /// se tient dessus (<see cref="CountPartyMembersMatching"/>, bit <c>0x10</c>), elle s'enfonce de
    /// <c>+0x14</c> unites (etat 2, une image, puis retour a 1 — et de nouveau 2 tant qu'on y reste),
    /// sonne une fois (voix <c>0x25</c> de la zone), et <b>pose dans la banque 0 le bit que designe
    /// son <c>+0x16</c></b> — s'il n'est pas nul. Quand plus personne n'y est, elle remonte (etat 3)
    /// et le son est rearme.
    /// </para>
    /// <para>
    /// Le bit se calcule en division C (<c>n / 8</c>, <c>n % 8</c>), comme le poisson des ruines
    /// dans le lac ; il n'est jamais efface ici : la dalle se souvient d'avoir ete pressee.
    /// </para>
    /// </summary>
    private static void EntityHandler_00468850(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x25), 0x200, 0x2000, 0, 0);   // Ghidra: FUN_00468080
                *(short*)(e + 0x98) = 1;
            }

            *(int*)(e + 0x20) = (*(short*)(e + 0x12) - *(short*)(e + 0x14)) << 0xc;
            *(short*)(e + 0x10) = 1;

            short flag = *(short*)(e + 0x16);
            if (flag != 0)
            {
                int index = flag / 8;
                Mem.s_StateBank0_005e96e0[index] |= (byte)(1 << ((flag - index * 8) & 0x1f));   // Ghidra: DAT_005e96e0
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(int*)(e + 0x20) = *(short*)(e + 0x12) << 0xc;
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x10) = 1;
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        if (aboard != 0)
        {
            if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 2;
            return;
        }

        if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 3;
    }

    // =====================================================================
    // LOT 4 (2026-09-19)
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0046ac50</c> — sous-zones 2 et 3. Absente de <c>Blaze.c</c> ; 83 instructions.
    /// <b>La dalle qui s'effondre sous le groupe</b>. Quand tous les membres presents se tiennent
    /// dessus (<see cref="CountPartyMembersMatching"/> egale
    /// <see cref="ActivePartySlotCount_00466D70"/>), elle retient sa hauteur, attend <c>+0x14</c>
    /// images, puis tombe de plus en plus vite (5 unites, puis 6, ...) en basculant de 10 par image ;
    /// passe la centieme, elle retrecit jusqu'a disparaitre, reparait a sa hauteur, a plat, et
    /// regrandit jusqu'a l'echelle <c>0x1000</c> — prete a recommencer.
    /// </summary>
    private static void EntityHandler_0046AC50(byte* e)
    {
        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000
        int present = ActivePartySlotCount_00466D70();                  // Ghidra: FUN_00466d70

        if (aboard == present && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x9a) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) > *(short*)(e + 0x14))
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 5;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short fall = *(short*)(e + 0x12);
            *(int*)(e + 0x20) -= fall << 0xc;
            fall++;
            *(short*)(e + 0x12) = fall;
            if (fall > 100) *(short*)(e + 0x10) = 3;

            *(short*)(e + 0x34) += 10;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x28) -= 200;
            *(short*)(e + 0x2a) -= 200;
            *(short*)(e + 0x2c) -= 200;

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x10) = 4;
                *(int*)(e + 0x20) = *(short*)(e + 0x9a) << 0xc;
                *(short*)(e + 0x34) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 4) return;

        *(short*)(e + 0x28) += 200;
        *(short*)(e + 0x2a) += 200;
        *(short*)(e + 0x2c) += 200;

        if (*(short*)(e + 0x28) <= 0x1000) return;

        *(short*)(e + 0x28) = 0x1000;
        *(short*)(e + 0x2a) = 0x1000;
        *(short*)(e + 0x2c) = 0x1000;
        *(short*)(e + 0x10) = 0;
    }


    /// <summary>
    /// Ghidra: <c>0x004699f0</c> (sous-zone 0) et <c>0x00470a00</c> (sous-zone 1) — absentes de
    /// <c>Blaze.c</c> ; 84 et 86 instructions. <b>La dalle qui veut tout le monde</b>, deux fois,
    /// sur deux mots differents. Elle compte les membres vivants (bit <c>0x80000000</c>), les
    /// membres debout (vivants, ni <c>0x50000000</c>), et ceux qui se tiennent sur elle :
    /// <list type="bullet">
    /// <item>tous les vivants dessus : elle pose le bit « depart » de son mot ;</item>
    /// <item>sinon, si seuls les debout y sont (et qu'il y en a), une fois : le premier d'entre eux
    /// dit une ligne du flux de texte <c>+0x70</c> de la zone
    /// (<see cref="BindMenuLabels(byte*, byte*, byte*, uint)"/>).</item>
    /// </list>
    /// Et tant que le bit « fini » du mot est pose, elle prend les bits <c>0x6000</c> de son mot
    /// d'etat.
    ///
    /// <code>
    ///   corps        mot          depart   fini     ligne
    ///   0x004699F0   0x00571828   0x0400   0x0800   2      met en marche 0x00469AF0
    ///   0x00470A00   0x00571978   0x1000   0x2000   1      met en marche 0x00470B00
    /// </code>
    /// <para>
    /// Les deux corps ne different que par ces quatre nombres et par la largeur d'une comparaison
    /// (seize bits dans le premier, trente-deux dans le second — sans effet, les comptes vont de 0 a
    /// 4). <b>Deux calculs morts</b> dans chacun : une boucle sur les quatre membres qui masque leur
    /// mot sans rien en faire, et un second compte « sur la dalle » (masque <c>0x40000000</c>) dont
    /// le resultat est jete. Le second est garde, c'est un appel ; la premiere n'a aucun effet et
    /// n'est pas ecrite.
    /// </para>
    /// </summary>
    private static void TickPartyGatePlate(byte* e, uint* word, uint startBit, uint doneBit, int line)
    {
        // Ghidra: les membres pris de 3 a 0 (0x005e9594, pas de -0x9c) : le « premier debout » est
        // donc celui de plus petit rang.
        int standing = 0;
        byte* first = null;
        byte* slot = Inp.s_SlotFlags_005e93c0 + 3 * 0x9c;
        for (int i = 4; i != 0; i--, slot -= 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            standing++;
            first = slot;
        }

        int living = 0;
        slot = Inp.s_SlotFlags_005e93c0 + 3 * 0x9c;
        for (int i = 4; i != 0; i--, slot -= 0x9c)
        {
            if ((*(uint*)slot & 0x80000000) != 0) living++;
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0);         // Ghidra: FUN_00535000
        _ = CountPartyMembersMatching(e, 0x10, 0x40000000);         // Ghidra: resultat jete

        if (aboard == living)
        {
            *word |= startBit;
        }
        else if (aboard == standing && standing != 0 && *(short*)(e + 0x10) == 0)
        {
            byte* labels = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + line * 4);
            BindMenuLabels(labels, first, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
            *(short*)(e + 0x10) = 1;
        }

        if ((*word & doneBit) != 0) *(uint*)e |= 0x6000;
    }

    private static void EntityHandler_004699F0(byte* e)
        => TickPartyGatePlate(e, (uint*)Mem.EntityState(0x00571828), 0x400, 0x800, 2);   // Ghidra: DAT_00571828

    private static void EntityHandler_00470A00(byte* e)
        => TickPartyGatePlate(e, (uint*)Mem.EntityState(0x00571978), 0x1000, 0x2000, 1); // Ghidra: DAT_00571978

    /// <summary>
    /// Ghidra: <c>0x0046b440</c> — sous-zones 2 et 3. Absente de <c>Blaze.c</c> ; 94 instructions.
    /// <b>Le tremplin qui attend le groupe en bas</b> : le tremplin
    /// <see cref="EntityHandler_00468950"/>, au mot pres, avec une porte de plus avant de
    /// s'eveiller — il faut que le nombre de membres sur lui (masque <c>0x40000000</c>) egale le
    /// nombre de membres debout plus bas que -800 unites. Une fois eveille il n'attend plus.
    /// </summary>
    private static void EntityHandler_0046B440(byte* e)
    {
        e[7] &= 0xef;
        uint flags = *(uint*)e & 0xffffdfff;
        *(uint*)e = flags;
        *(short*)(e + 0x2a) = 0x1770;

        if ((flags & 0x10) == 0 && *(short*)(e + 0x10) == 0) return;

        short aboard = (short)CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        short below = 0;
        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 4; i != 0; i--, slot += 0x9c)
        {
            uint f = *(uint*)slot;
            if ((f & 0x80000000) == 0 || (f & 0x50000000) != 0) continue;
            if (*(int*)(slot + 0x20) < unchecked((int)0xffce0000)) below++;
        }

        if (aboard != below && *(short*)(e + 0x10) == 0) return;

        BounceSpringboard_0046897F(e);
    }

    /// <summary>
    /// Ghidra: <c>0x0046f9d0</c> — sous-zones 6 et 7 (lot 5). Absente de <c>Blaze.c</c> ;
    /// 73 instructions. <b>La dalle de pression de la foret</b>
    /// (<see cref="TickPressurePlate_00459B20"/>), qui compte le groupe, avec le son de la grotte.
    /// </summary>
    private static void EntityHandler_0046F9D0(byte* e) => TickPressurePlate_00459B20(e, 0x25, 0x200, 0x2000);

    /// <summary>
    /// Ghidra: <c>0x0046e070</c> — sous-zones 4 et 9. Absente de <c>Blaze.c</c> ; 41 instructions.
    /// <b>Le relais sans etat</b>, deux roles selon son <c>+0x14</c> :
    /// <list type="bullet">
    /// <item>0 — quand les bits <c>0x02</c> et <c>0x04</c> de l'octet <c>0x21</c> de la banque 0
    /// sont poses, il prend le bit <c>0x2000</c> de son mot d'etat ;</item>
    /// <item>1 — tant que l'octet 3 de la banque 2 vaut 1, il lance le script 1 sur chacun des
    /// quatre membres (<see cref="RunEntityScript"/>) — <b>a chaque image</b> : rien dans le corps
    /// ne le retient d'une image a l'autre.</item>
    /// </list>
    /// </summary>
    private static void EntityHandler_0046E070(byte* e)
    {
        short role = *(short*)(e + 0x14);

        if (role == 0)
        {
            byte latches = Mem.s_StateBank0_005e96e0[0x21];         // Ghidra: DAT_005e9701
            if ((latches & 2) != 0 && (latches & 4) != 0) *(uint*)e |= 0x2000;
        }

        if (role != 1) return;
        if (Mem.s_StateBank2_005e9060[3] != 1) return;              // Ghidra: DAT_005e9063

        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, slot += 0x9c)
            RunEntityScript(s_Unknown_005e97cc, slot, 1);           // Ghidra: FUN_00513dd0
    }

    /// <summary>
    /// Ghidra: <c>0x0046f1c0</c> — sous-zones 6 et 7. Absente de <c>Blaze.c</c> ; 89 instructions.
    /// <b>Le monte-charge</b>. Quand tous les membres presents sont dessus, il retient sa hauteur et
    /// monte en accelerant (d'une unite par image, jusqu'a <c>+0x14</c>) ; passe <c>+0x16</c> unites
    /// de course, il ralentit jusqu'a s'arreter en haut (etat <c>0x63</c>). Quand plus personne n'y
    /// est, il redescend en accelerant (jusqu'a 30 unites par image) et se pose a sa hauteur
    /// retenue.
    /// <para>
    /// Le nombre de membres presents est demande deux fois ; la seconde reponse est jetee.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046F1C0(byte* e)
    {
        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        if (aboard == ActivePartySlotCount_00466D70() && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x9a) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        _ = ActivePartySlotCount_00466D70();                        // Ghidra: resultat jete

        if (aboard == 0 && *(short*)(e + 0x10) == 0x63) *(short*)(e + 0x10) = 3;

        short state = *(short*)(e + 0x10);

        if (state == 1)
        {
            short speed = *(short*)(e + 0x12);
            *(int*)(e + 0x20) += speed << 0xc;
            *(int*)(e + 0x5c) += speed;

            speed++;
            *(short*)(e + 0x12) = speed;
            if (speed > *(short*)(e + 0x14)) *(short*)(e + 0x12) = *(short*)(e + 0x14);

            if (*(int*)(e + 0x5c) > *(short*)(e + 0x16))
            {
                *(short*)(e + 0x10) = 2;
                return;
            }
        }

        if (state == 2)
        {
            short speed = *(short*)(e + 0x12);
            *(int*)(e + 0x20) += speed << 0xc;
            speed--;
            *(short*)(e + 0x12) = speed;
            if (speed == 0) *(short*)(e + 0x10) = 0x63;
        }

        if (*(short*)(e + 0x10) != 3) return;

        short fall = *(short*)(e + 0x12);
        *(int*)(e + 0x20) += fall << 0xc;
        fall--;
        *(short*)(e + 0x12) = fall < -0x1e ? (short)-0x1e : fall;

        int rest = *(short*)(e + 0x9a);
        if (*(int*)(e + 0x20) >> 0xc >= rest) return;

        *(int*)(e + 0x20) = rest << 0xc;
        *(short*)(e + 0x10) = 0;
    }
}
