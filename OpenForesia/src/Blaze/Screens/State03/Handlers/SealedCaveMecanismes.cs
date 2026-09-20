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

    // =====================================================================
    // LOT 7 (2026-09-20) — LES « C » QUI COMPTENT LE GROUPE, BLOCS 0 / 5-8-10
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0046c540 @ 0x0046c540 — sous-zones 5, 8 et 10. Decompilee dans <c>Blaze.c</c> ;
    /// 104 instructions.
    ///
    /// <para>
    /// <b>Le levier qu'on abaisse en montant dessus.</b> Ses trois echelles sont forcees a
    /// <c>0x1194</c> a chaque image. A sa premiere il retient sa hauteur en <c>+0x12</c>, range son
    /// numero de drapeau (<c>+0x30</c>) dans <c>+0x16</c> et remet <c>+0x30</c> a zero.
    /// </para>
    /// <para>
    /// <see cref="CountPartyMembersMatching"/> (bit <c>0x10</c> requis, <c>0x40000000</c> interdit)
    /// decide : quelqu'un dessus fait passer l'etat de 1 a 2, plus personne le ramene de 1 a 3.
    /// L'etat 2 enfonce le levier de <c>+0x14</c> unites, sonne <b>une fois</b> (voix <c>0x25</c> de
    /// la zone, gain <c>0x2000</c>) et <b>bascule</b> le bit que designe <c>+0x16</c> dans la banque
    /// 0 — mis s'il etait libre, efface s'il etait pose. L'etat 3 le releve et rearme les deux
    /// verrous (<c>+0x98</c> pour le son, <c>+0x5c</c> pour la bascule).
    /// </para>
    /// <para>
    /// <b>La bascule ne se joue qu'une fois par appui</b> : le drapeau <c>+0x5c</c> l'interdit tant
    /// que le levier n'a pas ete relache. Le levier de la grotte <b>efface</b> donc le bit au second
    /// appui, la ou la dalle de pression <see cref="EntityHandler_00468850"/> ne fait que le poser.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046C540(byte* e)
    {
        *(short*)(e + 0x28) = 0x1194;
        *(short*)(e + 0x2a) = 0x1194;
        *(short*)(e + 0x2c) = 0x1194;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x16) = *(short*)(e + 0x30);
            *(short*)(e + 0x30) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x25), 0x200, 0x2000, 0, 0);   // Ghidra: FUN_00468080
                *(short*)(e + 0x98) = 1;
            }

            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) = (*(short*)(e + 0x12) - *(short*)(e + 0x14)) * 0x1000;

            short flag = *(short*)(e + 0x16);
            if (flag != 0 && *(int*)(e + 0x5c) == 0)
            {
                int cell = (flag + (flag >> 0x1f & 7)) >> 3;
                byte bit = (byte)(1 << ((flag - cell * 8) & 0x1f));
                byte bank = Mem.s_StateBank0_005e96e0[cell];

                Mem.s_StateBank0_005e96e0[cell] = (bank & bit) == 0
                    ? (byte)(bit | bank)
                    : (byte)(~bit & bank);

                *(int*)(e + 0x5c) = 1;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x98) = 0;
            *(int*)(e + 0x20) = *(short*)(e + 0x12) << 0xc;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0;
        }

        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        if (aboard != 0)
        {
            if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 2;
            return;                                     // le binaire teste deux fois le meme zero
        }

        if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 3;
    }

    /// <summary>
    /// Ghidra: <c>0x0046cf10</c> — sous-zones 5, 8 et 10. <b>Absente de <c>Blaze.c</c></b> ;
    /// 119 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0046CF10.asm</c>.
    ///
    /// <para>
    /// <b>La plate-forme qui monte tant qu'on est dessus, et redescend des qu'on la quitte.</b> Elle
    /// compte deux fois les membres rattaches (<see cref="CountPartyMembersMatching"/>) : une fois
    /// avec le bit <c>0x10</c>, dont elle se sert, et <b>une fois avec le bit <c>0x20</c> dont le
    /// resultat est jete</b> — l'appel est conserve, le binaire le fait.
    /// </para>
    /// <para>
    /// Au moins un passager a l'arret (etat 0) declenche la montee (etat 1) ; plus personne en haut
    /// (etat 2) declenche la descente (etat 3). Les deux courses ont le meme profil, au signe pres :
    /// la vitesse <c>+0x12</c> gagne 1 par image jusqu'a <c>0x14</c> tant que la distance parcourue
    /// <c>+0x5c</c> n'a pas atteint la course <c>+0x14</c>, puis perd 1 par image — arrivee a zero,
    /// la plate-forme s'arrete.
    /// </para>
    /// <para>
    /// <b>Elle derive aussi a l'horizontale</b>, de <c>+0x16</c> unites par image sur son cap
    /// <c>+0x32</c> : en avant a la montee, en arriere a la descente. Une plate-forme oblique.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046CF10(byte* e)
    {
        int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000
        CountPartyMembersMatching(e, 0x20, 0x40000000);                // Ghidra: FUN_00535000, resultat jete

        if (aboard >= 1 && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (aboard == 0 && *(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 1) TickSealedCaveLiftTravel(e, rising: true);
        if (*(short*)(e + 0x10) == 3) TickSealedCaveLiftTravel(e, rising: false);
    }

    /// <summary>Une image de course de la plate-forme oblique. Le binaire ecrit ce bloc deux fois,
    /// au signe du pas et a l'etat d'arrivee pres — 2 en haut, 0 en bas.</summary>
    private static void TickSealedCaveLiftTravel(byte* e, bool rising)
    {
        short speed = *(short*)(e + 0x12);

        if (rising) *(int*)(e + 0x20) += speed << 0xc;
        else *(int*)(e + 0x20) -= speed << 0xc;

        int travelled = *(int*)(e + 0x5c) + speed;
        *(int*)(e + 0x5c) = travelled;

        short course = *(short*)(e + 0x14);

        if (travelled > course)
        {
            short slower = (short)(speed - 1);
            *(short*)(e + 0x12) = slower;
            if (slower < 0) *(short*)(e + 0x10) = (short)(rising ? 2 : 0);
        }

        if (*(short*)(e + 0x12) < 0x14 && *(int*)(e + 0x5c) < course)
        {
            *(short*)(e + 0x12) += 1;
        }

        short drift = *(short*)(e + 0x16);
        short heading = *(short*)(e + 0x32);

        if (rising)
        {
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * drift;   // Ghidra: FUN_00545a1c
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * drift;        // Ghidra: FUN_00545a16
        }
        else
        {
            *(int*)(e + 0x1c) -= (int)Winsys3d.GetSinTableEntry(heading) * drift;
            *(int*)(e + 0x24) -= Winsys3d.GetCosTableEntry(heading) * drift;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0046c830 @ 0x0046c830 — sous-zones 5, 8 et 10. Decompilee dans <c>Blaze.c</c> ;
    /// 175 instructions. <b>L'ascenseur de la foret</b> (<see cref="EntityHandler_00458FC0"/>) a
    /// quatre choses pres : les bornes, le seuil d'arrivee, la forme du <c>+0x44</c> — et
    /// <b>l'absence totale de son</b>.
    ///
    /// <para>
    /// Le champ <c>+0x44</c>, fige a la premiere image depuis le <c>+0x14</c> de la sous-zone, ne
    /// choisit pas ici une seconde course : <b>non nul, il fige l'ascenseur</b>. Toute la logique
    /// d'appel et les deux butees sont gardees par <c>+0x44 == 0</c> ; un ascenseur marque fait
    /// donc ses courses sans jamais s'arreter de lui-meme.
    /// </para>
    /// <para>
    /// <b>Il part quand tout le monde est monte</b> : le compte des rattaches
    /// (<see cref="CountPartyMembersMatching"/>) est compare a
    /// <see cref="ActivePartySlotCount_00466D70"/> <b>moins</b> ceux qui sont deja arrives, un
    /// arrive etant ici un membre proche <b>de hauteur positive</b> — le seuil est zero, le haut de
    /// la course. Et il redescend seul si un rattache tombe plus de <c>0x27000</c> sous lui, si tout
    /// le groupe est dessus a hauteur nulle, ou — par l'autre butee — si la camera
    /// (<c>0x005de06c</c>) passe au-dessus de <c>-0x1000</c>.
    /// </para>
    /// <para>
    /// La course va de <c>0</c> (haut) a <c>-0x168000</c> (bas), par pas de <c>0x7000</c>, et le
    /// verrou <c>+0x5c</c> se pose toutes les cinq images comme en foret — <b>mais plus rien ne le
    /// lit</b>, puisqu'il n'y a pas de son d'arrivee. Releve tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046C830(byte* e)
    {
        if (*(short*)(e + 0x16) == 0)
        {
            *(int*)(e + 0x44) = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 1;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x98) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        // ------------------------------------------------- qui est proche, et qui est deja arrive
        short arrived = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;                    // 0x005e93c0

        for (int i = 4; i != 0; i--, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(member + 0x1c) >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
            int dy = (*(int*)(member + 0x20) >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
            int dz = (*(int*)(member + 0x24) >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x4b0) continue;
            if (*(int*)(e + 0x44) != 0) continue;

            int height = *(int*)(member + 0x20);
            if (height >= 0) arrived++;

            if (height < *(int*)(e + 0x20) - 0x27000 && *(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 100;
            }
        }

        short riding = (short)CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        bool skipCall = false;
        if (*(short*)(e + 0x14) != 0)
        {
            if (riding == 0) *(short*)(e + 0x14) = 0;
            if (*(short*)(e + 0x14) != 0) skipCall = true;
        }

        if (!skipCall && *(short*)(e + 0x10) == 0)
        {
            int active = ActivePartySlotCount_00466D70();            // Ghidra: FUN_00466d70
            if (riding == active - arrived && ActivePartySlotCount_00466D70() != arrived)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x14) = 1;
            }
        }

        // ------------------------------------------------- les deux rappels, sous garde de +0x44
        if (*(int*)(e + 0x44) == 0)
        {
            if (*(short*)(e + 0x14) == 0 && *(short*)(e + 0x10) == 0
                && riding == ActivePartySlotCount_00466D70() && *(int*)(e + 0x20) == 0)
            {
                *(short*)(e + 0x10) = 100;
                *(short*)(e + 0x14) = 1;
            }

            if (*Mem.Camera(0x005de06c) > -0x1001 && *(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
            }
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            SilentLiftTravel(e, 0x7000);

            int height = *(int*)(e + 0x20);
            if (*(int*)(e + 0x44) == 0 && height > 0)
            {
                *(int*)(e + 0x20) = 0;
                *(short*)(e + 0x10) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 100)
        {
            *(short*)(e + 0x10) = 0x65;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) != 0x65) return;

        SilentLiftTravel(e, -0x7000);

        if (*(int*)(e + 0x44) == 0 && *(int*)(e + 0x20) < -0x168000)
        {
            *(int*)(e + 0x20) = -0x168000;
            *(short*)(e + 0x10) = 0;
        }
    }

    /// <summary>Une image de course de l'ascenseur de la grotte : le verrou toutes les cinq images,
    /// le compteur, et le pas vertical. C'est <c>TickLiftTravel</c> de la foret <b>sans le
    /// son</b> ; le binaire ecrit ce bloc deux fois, au signe du pas pres.</summary>
    private static void SilentLiftTravel(byte* e, int step)
    {
        *(short*)(e + 0x98) += 1;

        if (*(short*)(e + 0x98) == 5)
        {
            *(short*)(e + 0x98) = 0;
            *(int*)(e + 0x5c) = 1;
        }

        *(short*)(e + 0x12) += 1;
        *(int*)(e + 0x20) += step;
    }

    /// <summary>
    /// Ghidra: FUN_00468580 @ 0x00468580 — sous-zone 0. Decompilee dans <c>Blaze.c</c> ;
    /// 202 instructions.
    ///
    /// <para>
    /// <b>Ce qu'on actionne, et qui gele la partie.</b> Le compteur <c>+0x16</c> tourne sans fin
    /// (repli a <c>0x12c</c> au-dela de 3000) et sert de delai d'armement : sous 5 images, rien ne
    /// repond. Son <c>+0x14</c> designe un bit de la banque 0 ; <b>si le bit est deja pose,
    /// l'entite passe a l'etat 99</b>, qui lui met <c>0x6000</c> et la laisse la : ce qui a ete fait
    /// ne se refait pas.
    /// </para>
    /// <para>
    /// Sur contact (<c>0x40</c>) a l'etat 0, elle prend le membre qui l'a touchee — ou l'entite par
    /// defaut <c>0x005de558</c> — et regarde <b>son bloc de pose</b> : les trois bits bas de
    /// l'octet <c>+0x150</c> de son entree <c>0x2000</c> de la memoire principale. Valant 6 et sans
    /// le bit <c>0x10000</c>, c'est <b>le bon geste</b> : la table de libelles <c>+0x4a8</c> s'ouvre
    /// (<see cref="BindMenuLabels"/>), <c>0x005e99c8</c> recoit <c>0x40</c>, le bit du
    /// drapeau se pose et l'etat passe a 1. Sinon c'est la table <c>+0x4</c> qui s'ouvre, l'etat
    /// passe a 100 et un delai de 5 images s'arme : <b>le refus a son propre message</b>.
    /// </para>
    /// <para>
    /// Les etats 1 et 2 sont l'animation : le membre est mis en pose 6, l'entite gele tout
    /// (<see cref="FreezeAllEntities"/>), puis avance l'image du membre jusqu'au bout de son
    /// clip — la longueur se lit dans le bloc de modele que <c>0x005e9680</c> designe pour lui. Au
    /// bout, le son <c>8</c>, le bit <c>0x2000000</c> sur le membre, et
    /// <see cref="UnfreezeAllEntities"/>.
    /// </para>
    /// <para>
    /// L'etat 3 est la recompense : cinq images pendant lesquelles l'entite <b>monte de
    /// <c>0x1e</c></b>, seme neuf fois l'effet <c>0x3b</c>
    /// (<see cref="SpawnEffectNearCamera_00468110"/>) et redescend aussitot — la hauteur est rendue
    /// a la fin de chaque image. Puis l'etat 99, qui l'eteint pour de bon.
    /// </para>
    /// <para>
    /// <b>Ghidra ne recupere pas le bloc de pose.</b> Sa pseudo-C ecrit <c>iVar6</c> — l'adresse de
    /// l'entree de memoire principale — sans le declarer dans la branche qui le lit, et lit
    /// <c>*(int *)(&amp;DAT_005e9680 + (byte)puVar7[1] * 4)</c> pour le bloc de modele du membre.
    /// Les deux lectures sont transcrites telles quelles ; le champ <c>+0x16</c> du bloc est la
    /// longueur du clip.
    /// </para>
    /// </summary>
    private static void EntityHandler_00468580(byte* e)
    {
        byte* toucher = null;

        *(short*)(e + 0x16) += 1;
        if (*(short*)(e + 0x16) > 3000) *(short*)(e + 0x16) = 0x12c;

        short flag = *(short*)(e + 0x14);
        int cell = (short)((flag + (flag >> 0x1f & 7)) >> 3);
        byte bit = (byte)(1 << ((flag - cell * 8) & 0x1f));

        if ((Mem.s_StateBank0_005e96e0[cell] & bit) != 0 && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 99;
        }

        if ((*(uint*)e & 0x40) != 0 && *(short*)(e + 0x10) == 0)
        {
            if (*(short*)(e + 0x16) <= 4)
            {
                *(short*)(e + 0x10) = 99;
            }
            else
            {
                toucher = *(byte**)(e + 0x6c);
                if (toucher == null) toucher = Res.s_EntityList_005de558;   // Ghidra: &DAT_005de558

                byte* slot = GetMainMemPtr() + (toucher[8] + 0x78) * 0x2000;

                if ((slot[0x150] & 7) == 6 && (*(uint*)toucher & 0x10000) == 0)
                {
                    BindMenuLabels(
                        *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x4a8),
                        toucher, s_WorldMapDescriptor_005633b0, 0);          // Ghidra: FUN_00544350
                    *(uint*)Mem.Arena(0x005e99c8) = 0x40;
                    *(short*)(e + 0x10) = 1;
                    Mem.s_StateBank0_005e96e0[cell] |= bit;
                }

                if ((slot[0x150] & 7) == 6) return;
                if ((*(uint*)toucher & 0x10000) != 0) return;

                BindMenuLabels(
                    *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 4),
                    toucher, s_WorldMapDescriptor_005633b0, 0);              // Ghidra: FUN_00544350
                *(uint*)Mem.Arena(0x005e99c8) = 0x40;
                *(short*)(e + 0x10) = 100;
                *(int*)(e + 0x5c) = 5;
                return;
            }
        }

        byte* clip = null;

        if (*(short*)(e + 0x10) == 1 || *(short*)(e + 0x10) == 2)
        {
            toucher = *(byte**)(e + 0x6c);
            if (toucher == null) toucher = Res.s_EntityList_005de558;

            *(short*)(toucher + 0x18) = 6;
            clip = *(byte**)(*(byte**)(s_PartyAnimationResult_005e9680 + toucher[4] * 4) + 0x18);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            byte* held = *(byte**)(e + 0x6c);
            *(short*)(e + 0x10) = 2;
            *(uint*)e |= 0x2000;
            *(short*)(held + 0x18) = 6;
            *(short*)(held + 0x1a) = 0;
            FreezeAllEntities();                                     // Ghidra: FUN_00535300(0)
            *(uint*)e &= 0xfbffffff;
            return;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(toucher + 0x1a) + 1 < *(ushort*)(clip + 0x16))
            {
                *(short*)(toucher + 0x1a) += 1;
            }
            else
            {
                TryPlaySoundNearEntity((uint*)e, 8, 0x300, 0x1000, 0, 0);   // Ghidra: FUN_00468080
                *(uint*)toucher |= 0x2000000;
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
                UnfreezeAllEntities();                                // Ghidra: FUN_00535400
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            int height = *(int*)(e + 0x20);
            *(short*)(e + 0x12) += 1;
            *(uint*)e |= 0x4000;
            *(int*)(e + 0x20) = height + 0x1e000;

            for (int i = 9; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x3b, 0, -1);   // Ghidra: FUN_00468110

            if (*(short*)(e + 0x12) == 5) *(short*)(e + 0x10) = 99;
            *(int*)(e + 0x20) = height;
        }

        if (*(short*)(e + 0x10) == 99) *(uint*)e |= 0x6000;

        if (*(short*)(e + 0x10) == 100)
        {
            int left = *(int*)(e + 0x5c);
            *(int*)(e + 0x5c) = left - 1;
            if (left - 1 < 0) *(short*)(e + 0x10) = 0;
        }
    }
}
