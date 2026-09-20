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

    // =====================================================================
    // LOT 8 (2026-09-20) — SOUS-ZONES 6 ET 7
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0046e890 @ 0x0046e890 — sous-zones 6 et 7. Decompilee dans <c>Blaze.c</c> ;
    /// 129 instructions.
    ///
    /// <para>
    /// <b>L'escalier tournant entre deux paliers.</b> Il ne part que lorsque <b>tout le groupe</b>
    /// est dessus : le compte des rattaches (<see cref="CountPartyMembersMatching"/>, bit
    /// <c>0x10</c>) doit egaler <see cref="ActivePartySlotCount_00466D70"/>. Sa hauteur de depart
    /// decide du sens — a <c>0xc8000</c> il monte (<c>+0x32</c> de <c>0x32</c>, <c>+0x14</c> de
    /// <c>10</c>), sinon il descend (<c>-0x32</c>, <c>-10</c>).
    /// </para>
    /// <para>
    /// <b>Et il se declenche aussi tout seul</b> : au palier haut (<c>0x190000</c>), il suffit
    /// qu'un membre vivant soit <b>plus bas que le palier</b> — quelqu'un est tombe — pour qu'il
    /// parte vers le bas, groupe ou pas.
    /// </para>
    /// <para>
    /// Pendant la course, il tourne et glisse de 20 unites par image sur son cap. <b>Le sens du
    /// glissement suit celui du tour</b> : a la montee, Z gagne le sinus et X perd le cosinus ; a
    /// la descente, l'inverse, plus <b>une unite de moins en X a chaque image</b> — un decalage
    /// d'une unite entiere, pas de <c>0x1000</c>, releve tel quel.
    /// </para>
    /// <para>
    /// A l'arrivee il se <b>repose exactement</b> : <c>(0xdac000, 0xc8000, 0x11f8000)</c> en bas,
    /// <c>(0xeb1000, 0x190000, 0x10fd000)</c> en haut — des coordonnees absolues en dur, donc un
    /// escalier unique. L'etat 2 attend que tout le monde en soit descendu pour se rearmer.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046E890(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            short aboard = (short)CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000
            if (aboard == ActivePartySlotCount_00466D70())                          // Ghidra: FUN_00466d70
            {
                if (*(int*)(e + 0x20) == 0xc8000)
                {
                    *(short*)(e + 0x12) = 0x32;
                    *(short*)(e + 0x14) = 10;
                }
                else
                {
                    *(short*)(e + 0x12) = unchecked((short)0xffce);
                    *(short*)(e + 0x14) = unchecked((short)0xfff6);
                }

                *(short*)(e + 0x10) = 1;
            }

            if (*(int*)(e + 0x20) == 0x190000)
            {
                byte* member = Inp.s_SlotFlags_005e93c0;            // 0x005e93c0

                for (int i = 4; i != 0; i--, member += 0x9c)
                {
                    uint flags = *(uint*)member;
                    if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
                    if (*(int*)(member + 0x20) >= 0x190000) continue;

                    *(short*)(e + 0x12) = unchecked((short)0xffce);
                    *(short*)(e + 0x14) = unchecked((short)0xfff6);
                    *(short*)(e + 0x10) = 1;
                }
            }
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) += *(short*)(e + 0x12);
            *(int*)(e + 0x20) += *(short*)(e + 0x14) * 0x1000;

            short course = *(short*)(e + 0x32);

            if (*(short*)(e + 0x12) < 1)
            {
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 0x14;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * -0x14;
                *(int*)(e + 0x1c) -= 0x1000;
            }
            else
            {
                *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * -0x14;
                *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 0x14;
            }

            if (*(int*)(e + 0x20) == 0xc8000)
            {
                *(short*)(e + 0x10) = 2;
                *(int*)(e + 0x1c) = 0xdac000;
                *(int*)(e + 0x20) = 0xc8000;
                *(int*)(e + 0x24) = 0x11f8000;
            }

            if (*(int*)(e + 0x20) == 0x190000)
            {
                *(short*)(e + 0x10) = 2;
                *(int*)(e + 0x1c) = 0xeb1000;
                *(int*)(e + 0x20) = 0x190000;
                *(int*)(e + 0x24) = 0x10fd000;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        if (CountPartyMembersMatching(e, 0x10, 0x40000000) == 0) *(short*)(e + 0x10) = 0;
    }

    // =====================================================================
    // LOT 10 (2026-09-20) — LA NAVETTE, TROIS COPIES
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0046ad70</c> (sous-zones 2 et 3), <c>0x0046d070</c> (5, 8 et 10) et
    /// <c>0x0046f2e0</c> (6 et 7). <b>Absentes de <c>Blaze.c</c></b> ; 459 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_0046AD70.asm</c>. <b>Les trois corps sont identiques
    /// instruction pour instruction</b>, adresses mises à part. C'est le plus gros corps de la
    /// catégorie « D » après celui de la sous-zone 1.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    /// <b>La navette qui fait la navette.</b> Elle porte le groupe d'un bout à l'autre d'une course
    /// droite et revient. Trois champs de la sous-zone la règlent : <c>+0x16</c> la vitesse
    /// maximale, <c>+0x14</c> la longueur de la course, <c>+0x30</c> le pas vertical (rangé dans
    /// <c>+0x9a</c> à la naissance), et <c>+0x32</c> le cap.
    /// </para>
    /// <para>
    /// <b>Elle calcule son point d'arrivée en déroulant tout le voyage à sa première image.</b> La
    /// première passe est une boucle qui avance jusqu'au bout, puis rend la position de départ. Le
    /// résultat est rangé d'une façon qui mérite d'être dite : <c>+0x44</c>, <c>+0x48</c> et
    /// <c>+0x4c</c> portent chacun <b>deux positions en unités entières</b>, le départ dans les
    /// seize bits hauts (<c>(p &amp; ~0xfff) &lt;&lt; 4</c>) et l'arrivée dans les seize bits bas.
    /// Un dword par axe, deux points.
    /// </para>
    /// <list type="table">
    ///   <item><term>états 8 et 2</term><description>au repos au départ et à l'arrivée. Le groupe
    ///     entier à bord lance la course (états 1 et 3).</description></item>
    ///   <item><term>états 1 et 3</term><description>la course, aller et retour. La vitesse
    ///     <c>+0x5c</c> monte jusqu'à <c>+0x16</c> puis redescend une fois la longueur parcourue ;
    ///     à zéro, l'état 4 ou 5.</description></item>
    ///   <item><term>états 4 et 5</term><description>arrivée : elle attend que <b>plus personne</b>
    ///     ne soit à bord pour se rearmer (2 et 8).</description></item>
    ///   <item><term>états 6 et 7</term><description>quelqu'un est tombé en route. Elle rétrécit de
    ///     200 par axe et par image jusqu'à disparaître, puis <b>reparaît à l'autre bout</b>.</description></item>
    /// </list>
    /// <para>
    /// <b>Et elle se téléporte au bout le plus proche de la caméra.</b> Au repos, si le groupe n'est
    /// pas entier à bord, elle compare la distance en plan de la caméra à chacun de ses deux bouts
    /// et s'y pose — en passant à l'autre état si elle a changé de bout. La navette est donc
    /// toujours du côté où le joueur se trouve, sans qu'on la voie voyager à vide.
    /// </para>
    /// <para>
    /// Hors des états 6 et 7, ses trois échelles rejoignent <c>0x1000</c> par huitièmes à chaque
    /// image : la réapparition se fait en grandissant.
    /// </para>
    /// </remarks>
    private static void TickPartyShuttle_0046AD70(byte* e)
    {
        short state = *(short*)(e + 0x10);

        if (state != 6 && state != 7)
        {
            *(short*)(e + 0x28) = (short)(((0x1000 - *(short*)(e + 0x28)) >> 3) + *(short*)(e + 0x28));
            *(short*)(e + 0x2a) = (short)(((0x1000 - *(short*)(e + 0x2a)) >> 3) + *(short*)(e + 0x2a));
            *(short*)(e + 0x2c) = (short)(((0x1000 - *(short*)(e + 0x2c)) >> 3) + *(short*)(e + 0x2c));
        }

        if (state == 0)
        {
            // ------------------------------------------------- le voyage a blanc
            short lift = *(short*)(e + 0x30);
            int keptX = *(int*)(e + 0x1c);
            int keptY = *(int*)(e + 0x20);
            int keptZ = *(int*)(e + 0x24);

            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x9a) = lift;
            *(short*)(e + 0x30) = 0;

            *(int*)(e + 0x44) = (keptX & unchecked((int)0xfffff000)) << 4;
            *(int*)(e + 0x48) = (keptY & unchecked((int)0xfffff000)) << 4;
            *(int*)(e + 0x4c) = (keptZ & unchecked((int)0xfffff000)) << 4;

            do
            {
                ShuttleTravelStep(e, outward: true);
            }
            while (*(short*)(e + 0x10) == 0);

            *(int*)(e + 0x44) |= (*(int*)(e + 0x1c) >> 0xc) & 0xffff;
            *(int*)(e + 0x48) |= (*(int*)(e + 0x20) >> 0xc) & 0xffff;
            *(int*)(e + 0x4c) |= (*(int*)(e + 0x24) >> 0xc) & 0xffff;

            *(int*)(e + 0x1c) = keptX;
            *(int*)(e + 0x20) = keptY;
            *(int*)(e + 0x24) = keptZ;
        }

        // ------------------------------------------------- les deux bouts, depaquetes
        int startX = *(int*)(e + 0x44) >> 0x10;
        int startY = *(int*)(e + 0x48) >> 0x10;
        int startZ = *(int*)(e + 0x4c) >> 0x10;
        short endX = *(short*)(e + 0x44);
        short endY = *(short*)(e + 0x48);
        short endZ = *(short*)(e + 0x4c);

        int riders = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        if (ActivePartySlotCount_00466D70() != riders && *(short*)(e + 0x10) == 8)
        {
            // Au depart, groupe incomplet : se poser au bout le plus proche de la camera.
            if (CameraDistanceTo(startX, startZ) > CameraDistanceTo(endX, endZ))
            {
                PlaceShuttle(e, endX, endY, endZ);
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0;
            }
            else
            {
                PlaceShuttle(e, startX, startY, startZ);
            }

            return;
        }

        if (ActivePartySlotCount_00466D70() != riders && *(short*)(e + 0x10) == 2)
        {
            if (CameraDistanceTo(startX, startZ) < CameraDistanceTo(endX, endZ))
            {
                PlaceShuttle(e, startX, startY, startZ);
                *(short*)(e + 0x10) = 8;
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0;
            }
            else
            {
                PlaceShuttle(e, endX, endY, endZ);
            }

            return;
        }

        // ------------------------------------------------- l'aller
        if (ActivePartySlotCount_00466D70() == riders && *(short*)(e + 0x10) == 8)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            ShuttleTravelStep(e, outward: true);

            if (ActivePartySlotCount_00466D70() != CountPartyMembersMatching(e, 0x20, 0x40000000))
            {
                *(short*)(e + 0x10) = 6;
                *(int*)(e + 0x5c) = 0;
            }
        }

        // ------------------------------------------------- le retour
        if (ActivePartySlotCount_00466D70() == riders && *(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            ShuttleTravelStep(e, outward: false);

            if (ActivePartySlotCount_00466D70() != CountPartyMembersMatching(e, 0x20, 0x40000000))
            {
                *(short*)(e + 0x10) = 7;
                *(int*)(e + 0x5c) = 0;
            }
        }

        // ------------------------------------------------- les deux attentes et les deux fondus
        if (*(short*)(e + 0x10) == 4 && riders == 0) *(short*)(e + 0x10) = 2;
        if (*(short*)(e + 0x10) == 5 && riders == 0) *(short*)(e + 0x10) = 8;

        if (*(short*)(e + 0x10) == 6 && *(int*)(e + 0x5c) == 0 && ShuttleShrink(e))
        {
            *(short*)(e + 0x10) = 8;
        }

        if (*(short*)(e + 0x10) == 7 && *(int*)(e + 0x5c) == 0 && ShuttleShrink(e))
        {
            *(short*)(e + 0x10) = 2;
        }
    }

    /// <summary>Un pas de course de la navette. Le binaire écrit ce bloc trois fois — une pour le
    /// voyage à blanc, une pour l'aller, une pour le retour, au signe près et à l'état d'arrivée
    /// près (4 à l'aller, 5 au retour ; 8 pour le voyage à blanc).</summary>
    private static void ShuttleTravelStep(byte* e, bool outward)
    {
        if (*(int*)(e + 0x5c) < *(short*)(e + 0x16) && *(short*)(e + 0x12) < *(short*)(e + 0x14))
        {
            *(int*)(e + 0x5c) += 1;
        }

        int speed = *(int*)(e + 0x5c);
        short course = *(short*)(e + 0x32);
        int climb = *(short*)(e + 0x9a) << 0xc;

        if (outward)
        {
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * speed;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * speed;
            *(int*)(e + 0x20) += climb;
        }
        else
        {
            *(int*)(e + 0x1c) -= (int)Winsys3d.GetSinTableEntry(course) * speed;
            *(int*)(e + 0x24) -= Winsys3d.GetCosTableEntry(course) * speed;
            *(int*)(e + 0x20) -= climb;
        }

        *(short*)(e + 0x12) += (short)speed;

        if (*(short*)(e + 0x12) <= *(short*)(e + 0x14)) return;

        *(int*)(e + 0x5c) = speed - 1;
        if (speed - 1 != -1) return;

        // Le voyage a blanc tourne a l'etat 0 : il s'arrete sur 8. Les deux courses reelles
        // s'arretent sur 4 et 5.
        *(short*)(e + 0x10) = *(short*)(e + 0x10) == 0 ? (short)8 : (short)(outward ? 4 : 5);
    }

    /// <summary>La distance en plan de la caméra à un point donné en unités entières.</summary>
    private static int CameraDistanceTo(int x, int z)
    {
        int dx = (*Mem.Camera(0x005de068) >> 0xc) - x;
        int dz = (*Mem.Camera(0x005de070) >> 0xc) - z;

        return (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
    }

    /// <summary>Poser la navette sur un point donné en unités entières.</summary>
    private static void PlaceShuttle(byte* e, int x, int y, int z)
    {
        *(int*)(e + 0x1c) = x << 0xc;
        *(int*)(e + 0x20) = y << 0xc;
        *(int*)(e + 0x24) = z << 0xc;
    }

    /// <summary>Le fondu des états 6 et 7 : 200 de moins par axe et par image.</summary>
    /// <returns><c>true</c> quand la navette a entièrement disparu.</returns>
    private static bool ShuttleShrink(byte* e)
    {
        *(short*)(e + 0x28) -= 200;
        *(short*)(e + 0x2a) -= 200;
        *(short*)(e + 0x2c) -= 200;

        if (*(short*)(e + 0x28) > 0) return false;

        *(short*)(e + 0x28) = 0;
        *(short*)(e + 0x2a) = 0;
        *(short*)(e + 0x2c) = 0;
        return true;
    }

    private static void EntityHandler_0046AD70(byte* e) => TickPartyShuttle_0046AD70(e);
    private static void EntityHandler_0046D070(byte* e) => TickPartyShuttle_0046AD70(e);
    private static void EntityHandler_0046F2E0(byte* e) => TickPartyShuttle_0046AD70(e);

    // =====================================================================
    // LOT 11 (2026-09-20) — LE TRAINEAU QUE LE GROUPE MONTE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00469f60</c> — sous-zone 0. <b>Absente de <c>Blaze.c</c></b> ; 342 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00469F60.asm</c>.
    ///
    /// <para>
    /// <b>Le traineau sur lequel le groupe descend le couloir.</b> C'est le cousin de la luge de la
    /// vallee d'argent : il ne part que lorsque <b>tout le groupe</b> est monte dessus, accelere,
    /// prend les virages par <see cref="SteerSled_0046A3F0"/>, penche la camera dans les courbes par
    /// <see cref="CameraPitchGlide_0046A5A0"/>, et rend la main au bout du voyage.
    /// </para>
    /// <list type="table">
    ///   <item><term>les trois bits de <c>0x00571828</c></term><description><c>0x400</c> decide de
    ///     son dessin — eteint il porte le bit <c>0x2000</c>, allume il le perd ; <c>0x800</c>
    ///     l'autorise a exister du tout, et sans lui le corps pose le bit <c>0x4000</c> et
    ///     <b>rend la main sur-le-champ</b> ; <c>0x200</c> lui coupe la voix
    ///     d'attente.</description></item>
    ///   <item><term>l'attente</term><description>tant que <c>+0x12</c> vaut <c>-1</c>, il compte le
    ///     groupe monte dessus (<see cref="CountPartyMembersMatching"/>, <c>0x10</c> requis,
    ///     <c>0x40000000</c> interdit) et le compare a
    ///     <see cref="ActivePartySlotCount_00466D70"/> : <b>egalite stricte</b>, donc il faut que
    ///     <b>tout</b> le monde soit a bord. Pendant ce temps la voix <c>0x14</c> de la zone claque
    ///     a intervalle <c>max(3, 0x3c - vitesse)</c>.</description></item>
    ///   <item><term>le depart</term><description><c>+0x12</c> change de signe et vaut donc
    ///     <c>1</c>, la vitesse part a <c>5</c>, la distance parcourue
    ///     (<c>0x00571824</c>) et les trois mots de glissade repassent a zero, et le script
    ///     <c>0</c> du repertoire est joue sur <b>le contexte de zone</b> — ce qui fige les
    ///     commandes.</description></item>
    /// </list>
    /// <para>
    /// <b>La vitesse respire avec l'age.</b> L'age <c>+0x10</c> monte d'un par image et se cale a
    /// <c>0x4e20</c> passe <c>0x7530</c>. Une image sur quatre — age multiple de 4 — et seulement
    /// quand <c>+0x16</c> vaut <c>1</c>, la vitesse <b>descend</b> d'un, plancher <c>5</c> ; une
    /// image sur trois elle <b>monte</b> d'un, plafond <c>0x28</c>. Les deux regles se croisent
    /// librement : c'est le freinage dans les virages.
    /// </para>
    /// <para>
    /// <b>Le virage est detecte par le sol, pas par une table.</b> A chaque image le traineau lit
    /// l'octet <c>+0x0e</c> de la face sous lui et s'en sert d'indice dans ses propres dwords
    /// <c>+0x80</c>..<c>+0x8c</c> ; quand ce mot <b>change</b> d'une image a l'autre, <c>+0x16</c>
    /// avance d'un, et aux valeurs <c>2</c> et <c>4</c> la camera pique vers <c>-0x200</c>.
    /// </para>
    /// <para>
    /// La camera pique aussi vers <c>0x100</c> quand le traineau <b>monte</b>, vers <c>-0x100</c>
    /// quand il <b>descend</b> — sauf si <c>+0x16</c> vaut <c>2</c> ou <c>4</c>, ou le virage a
    /// deja la parole — et revient a <c>0</c> a plat.
    /// </para>
    /// <para>
    /// <b>L'arrivee</b> : des que la distance cumulee atteint <c>0x1004</c>, <c>+0x12</c> passe a
    /// <c>2</c> et le script <c>1</c> est joue sur <b>les quatre emplacements du groupe</b>, un par
    /// un. Le corps continue de tourner apres, mais <c>+0x12</c> ne vaut plus <c>1</c> et plus rien
    /// ne bouge.
    /// </para>
    /// </summary>
    private static void TickRideableSled_00469F60(byte* e)
    {
        uint* gate = (uint*)Mem.EntityState(0x00571828);            // Ghidra: DAT_00571828

        if ((*gate & 0x400) == 0) *(uint*)e |= 0x2000;
        if ((*gate & 0x400) != 0) *(uint*)e &= 0xffffdfff;

        if ((*gate & 0x800) == 0)
        {
            *(uint*)e |= 0x4000;
            return;
        }

        *(uint*)e &= 0xffff9fff;

        int* speed = Mem.EntityState(0x005717d0);                   // Ghidra: DAT_005717d0
        int* travelled = Mem.EntityState(0x00571824);               // Ghidra: DAT_00571824

        *(short*)(e + 0x98) += 1;

        int wait = 0x3c - *speed;
        if (wait < 3) wait = 3;

        if (*(short*)(e + 0x98) > wait && (*gate & 0x200) == 0)
        {
            *(short*)(e + 0x98) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x14),
                                   (short)(CrtRand() % 100 + 0x2ce), 0x1200, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = -1;
            *speed = 3;
            *(short*)(e + 0x16) = 0;
            *Mem.EntityState(0x005717ec) = 1;                       // Ghidra: DAT_005717ec
        }

        byte* savedFace = *(byte**)(e + 0x7c);
        int savedGroundX = *(int*)(e + 0x60);
        int savedGroundZ = *(int*)(e + 0x64);
        uint savedContacts = *(ushort*)(e + 0x90);
        int saved80 = *(int*)(e + 0x80);
        int saved84 = *(int*)(e + 0x84);
        int saved88 = *(int*)(e + 0x88);
        int saved8c = *(int*)(e + 0x8c);

        *(short*)(e + 0x10) += 1;
        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);

        if (*(short*)(e + 0x10) >= 0x7530) *(short*)(e + 0x10) = 0x4e20;

        // ------------------------------------------------- l'attente, puis le depart
        if (*(short*)(e + 0x12) == -1)
        {
            int aboard = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

            if (ActivePartySlotCount_00466D70() == aboard)                 // Ghidra: FUN_00466d70
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = (short)-*(short*)(e + 0x12);
                *speed = 5;
                *travelled = 0;
                *(short*)(e + 0x16) = 0;
                *Mem.EntityState(0x005717e0) = 0;                   // Ghidra: DAT_005717e0
                *(short*)(e + 0x14) = 0;
                *Mem.EntityState(0x0057181c) = 0;                   // Ghidra: DAT_0057181c
                *Mem.EntityState(0x00571820) = 0;                   // Ghidra: DAT_00571820

                RunEntityScript(s_Unknown_005e97cc, null, 0);       // Ghidra: FUN_00513dd0
            }
        }

        if (*(short*)(e + 0x12) != 1) return;

        // ------------------------------------------------- le voyage
        *travelled += *speed;
        SteerSled_0046A3F0(e, 0, 0);                                // Ghidra: FUN_0046a3f0
        CameraPitchGlide_0046A5A0(e, 0, 0);                         // Ghidra: FUN_0046a5a0

        short age = *(short*)(e + 0x10);

        // Ghidra: `cdq ; and edx,3 ; sar 2 ; shl 2` — « l'age est-il un multiple de 4 ».
        if (*(short*)(e + 0x16) == 1 && age == ((age + ((age >> 31) & 3)) >> 2) << 2)
        {
            *speed -= 1;
            if (*speed <= 5) *speed = 5;
        }

        // Ghidra: magie 0x55555556 sans decalage — « l'age est-il un multiple de 3 ».
        if (age == age / 3 * 3)
        {
            *speed += 1;
            if (*speed >= 0x28) *speed = 0x28;
        }

        int beforeHeight = *(int*)(e + 0x20);

        *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *speed;   // Ghidra: 0x00545a1c
        *(int*)(e + 0x1c) -= Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *speed;        // Ghidra: 0x00545a16

        // Le mot de sol sous le traineau : l'octet +0x0e de la face choisit lequel des quatre
        // dwords +0x80..+0x8c du traineau lui-meme sert de temoin.
        short beforeMark = *(short*)(e + (*(byte**)(e + 0x7c))[0x0e] * 4 + 0x80);

        *(int*)(e + 0x20) = beforeHeight + 0xc8000;
        FinalizeEntityPlacement2((uint*)e);                         // Ghidra: FUN_00533940
        *(int*)(e + 0x20) = *(int*)(e + 0x60);

        int quarter = (*speed & ~3) == 0 ? 1 : *speed >> 2;
        TryPublishCameraEffectNearEntity((uint*)e, 0x3e7, CrtRand() % quarter, 0x3e7, 0, 0);   // Ghidra: FUN_0041be60

        if (CrtRand() % 0x1e == 1) SmokePuff_0046A630(e, SignedMask(CrtRand(), 3) + 1);   // Ghidra: FUN_0046a630

        // ------------------------------------------------- la camera suit la pente
        int before = beforeHeight >> 0xc;
        int after = *(int*)(e + 0x20) >> 0xc;

        if (before < after) CameraPitchGlide_0046A5A0(e, 1, 0x100);

        if (before > after && *(short*)(e + 0x16) != 4 && *(short*)(e + 0x16) != 2)
        {
            CameraPitchGlide_0046A5A0(e, 1, -0x100);
        }

        if (before == after) CameraPitchGlide_0046A5A0(e, 1, 0);

        // ------------------------------------------------- le virage lu sur le sol
        if (*(short*)(e + (*(byte**)(e + 0x7c))[0x0e] * 4 + 0x80) != beforeMark)
        {
            *(short*)(e + 0x16) += 1;
            if (*(short*)(e + 0x16) == 4 || *(short*)(e + 0x16) == 2)
            {
                CameraPitchGlide_0046A5A0(e, 1, -0x200);
            }
        }

        if ((*(uint*)(e + 0x90) & 0x800) != 0)
        {
            *(byte**)(e + 0x7c) = savedFace;
            *(int*)(e + 0x60) = savedGroundX;
            *(int*)(e + 0x64) = savedGroundZ;
            *(int*)(e + 0x1c) = *(int*)(e + 0x44);
            *(int*)(e + 0x24) = *(int*)(e + 0x4c);
            *(int*)(e + 0x80) = saved80;
            *(uint*)(e + 0x90) = savedContacts & 0xffff;
            *(int*)(e + 0x84) = saved84;
            *(int*)(e + 0x88) = saved88;
            *(int*)(e + 0x8c) = saved8c;
        }

        // ------------------------------------------------- l'arrivee
        if ((uint)*travelled < 0x1004) return;

        *(short*)(e + 0x12) = 2;

        byte* member = Inp.s_SlotFlags_005e93c0;                    // 0x005e93c0
        for (int slot = 0; slot < 4; slot++, member += 0x9c)
        {
            RunEntityScript(s_Unknown_005e97cc, member, 1);         // Ghidra: FUN_00513dd0
        }
    }

    private static void EntityHandler_00469F60(byte* e) => TickRideableSled_00469F60(e);

    /// <summary>
    /// Ghidra: <c>0x00470c20</c> — sous-zone 1. <b>Absente de <c>Blaze.c</c></b> ; 385 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00470C20.asm</c>.
    ///
    /// <para>
    /// <b>Le second traineau, celui de la salle de la machine.</b> Meme charpente que
    /// <see cref="TickRideableSled_00469F60"/> — attente que tout le groupe soit a bord, depart,
    /// derive sur le cap, camera qui suit la pente, virage lu sur le mot de sol —, mais trois
    /// choses le distinguent, et ce sont elles qui comptent.
    /// </para>
    /// <list type="table">
    ///   <item><term>il part vite et ralentit</term><description>la vitesse demarre a <c>0x1e</c> et
    ///     monte jusqu'a <c>0x28</c>, la ou le premier traineau part a <c>5</c>. Il n'accelere pas
    ///     tout seul : <b>c'est le numero du virage qui decide</b>.</description></item>
    ///   <item><term>le profil de vitesse est ecrit a la main</term><description>aux virages
    ///     <c>3</c> a <c>6</c>, <c>0x0b</c>, <c>0x14</c> et <c>0x18</c> il accelere (une image sur
    ///     deux) ; aux virages <c>8</c> a <c>0x0a</c>, <c>0x12</c>, <c>0x16</c> et <c>0x1a</c> il
    ///     freine (une image sur trois). Le freinage de depart — virage <c>1</c>, une image sur
    ///     quatre — est le seul qu'il partage avec son aine. C'est le relief du couloir, releve
    ///     virage par virage par l'auteur de 1998.</description></item>
    ///   <item><term>sa voix n'est pas placee dans l'espace</term><description>elle passe par
    ///     <see cref="Audio.PlaySoundEffect"/> et non par <c>TryPlaySoundNearEntity</c> : le
    ///     grondement est au meme volume partout, ce qui va de soi pour un traineau qu'on
    ///     monte.</description></item>
    /// </list>
    /// <para>
    /// <b>Les trois bits de <c>0x00571978</c></b> jouent le role que <c>0x00571828</c> joue chez
    /// l'aine, decales : <c>0x1000</c> decide du dessin, <c>0x2000</c> de l'existence, <c>0x200</c>
    /// coupe la voix.
    /// </para>
    /// <para>
    /// <b>Au depart il retouche cinq mots de dessin de la scene</b> : <c>0x005fa5e0</c> perd son bit
    /// de tete, et <c>0x005fa850</c>, <c>0x005fa8ec</c>, <c>0x005fa988</c> prennent <c>0x2000</c> —
    /// puis <c>0x005fbb34</c> le prend a chaque image du voyage. Ce sont les quatre ou cinq
    /// morceaux de decor que le traineau fait apparaitre en s'ebranlant.
    /// </para>
    /// <para>
    /// <b>L'arrivee est a <c>0x2d28</c></b>, presque trois fois plus loin que celle de l'aine, et
    /// le script joue est le <c>0</c>, pas le <c>1</c>.
    /// </para>
    /// </summary>
    private static void TickMachineSled_00470C20(byte* e)
    {
        uint* gate = (uint*)Mem.EntityState(0x00571978);            // Ghidra: DAT_00571978

        if ((*gate & 0x1000) == 0) *(uint*)e |= 0x2000;
        if ((*gate & 0x1000) != 0) *(uint*)e &= 0xffffdfff;

        if ((*gate & 0x2000) == 0)
        {
            *(uint*)e |= 0x4000;
            return;
        }

        *(uint*)e &= 0xffff9fff;

        int* speed = Mem.EntityState(0x00571920);                   // Ghidra: DAT_00571920
        int* travelled = Mem.EntityState(0x00571974);               // Ghidra: DAT_00571974

        *(short*)(e + 0x98) += 1;

        int wait = 0x3c - *speed;
        if (wait < 3) wait = 3;

        if (*(short*)(e + 0x98) > wait && (*gate & 0x200) == 0)
        {
            *(short*)(e + 0x98) = 0;
            Aud.PlaySoundEffect(ZoneSound(0x14), CrtRand() % 100 + 0x2ce, 0x1200, 0x1200);   // Ghidra: FUN_0053cea0
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = -1;
            *speed = 0x1e;
            *(short*)(e + 0x16) = 0;
            *Mem.EntityState(0x0057193c) = 1;                       // Ghidra: DAT_0057193c
        }

        byte* savedFace = *(byte**)(e + 0x7c);
        uint savedContacts = *(ushort*)(e + 0x90);
        int savedGroundX = *(int*)(e + 0x60);
        int savedGroundZ = *(int*)(e + 0x64);
        int saved80 = *(int*)(e + 0x80);
        int saved84 = *(int*)(e + 0x84);
        int saved88 = *(int*)(e + 0x88);
        int saved8c = *(int*)(e + 0x8c);

        *(short*)(e + 0x10) += 1;
        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);

        if (*(short*)(e + 0x10) >= 0x7530) *(short*)(e + 0x10) = 0x4e20;

        // ------------------------------------------------- l'attente, puis le depart
        if (*(short*)(e + 0x12) == -1
            && ActivePartySlotCount_00466D70() == CountPartyMembersMatching(e, 0x10, 0x40000000))
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)-*(short*)(e + 0x12);
            *speed = 0x28;
            *travelled = 0;
            *(short*)(e + 0x16) = 0;
            *Mem.EntityState(0x00571930) = 0;                       // Ghidra: DAT_00571930
            *(short*)(e + 0x14) = 0;

            *(uint*)Mem.Entity(0x005fa5e0) &= 0x7fffffff;           // Ghidra: DAT_005fa5e0
            *Mem.EntityState(0x0057196c) = 0;                       // Ghidra: DAT_0057196c
            *(uint*)Mem.Entity(0x005fa850) |= 0x2000;               // Ghidra: DAT_005fa850
            *(uint*)Mem.Entity(0x005fa8ec) |= 0x2000;               // Ghidra: DAT_005fa8ec
            *(uint*)Mem.Entity(0x005fa988) |= 0x2000;               // Ghidra: DAT_005fa988
            *Mem.EntityState(0x00571970) = 0;                       // Ghidra: DAT_00571970
        }

        if (*(short*)(e + 0x12) != 1) return;

        // ------------------------------------------------- le voyage
        *(uint*)Mem.Entity(0x005fbb34) |= 0x2000;                   // Ghidra: DAT_005fbb34
        *travelled += *speed;

        SteerSled_0046A3F0(e, 0, 0, SubArea1Bank, 0x32);            // Ghidra: FUN_00471dd0
        CameraPitchGlide_0046A5A0(e, 0, 0, SubArea1Bank);           // Ghidra: FUN_004725a0

        short age = *(short*)(e + 0x10);
        short turn = *(short*)(e + 0x16);

        // Ghidra: `cdq ; and edx,3 ; sar 2 ; shl 2` — « l'age est-il un multiple de 4 ».
        if (turn == 1 && age == ((age + ((age >> 31) & 3)) >> 2) << 2)
        {
            *speed -= 1;
            if (*speed <= 5) *speed = 5;
        }

        // Ghidra: `cdq ; sub ; sar 1 ; shl 1` — « l'age est-il pair ».
        bool evenFrame = age == (age - (age >> 31) >> 1) << 1;

        if (((turn >= 3 && turn <= 6) || turn == 0x0b || turn == 0x14 || turn == 0x18) && evenFrame)
        {
            *speed += 1;
            if (*speed >= 0x28) *speed = 0x28;
        }

        if (((turn >= 8 && turn <= 0x0a) || turn == 0x12 || turn == 0x16 || turn == 0x1a)
            && age == age / 3 * 3)
        {
            *speed -= 1;
            if (*speed <= 5) *speed = 5;
        }

        int beforeHeight = *(int*)(e + 0x20);

        *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *speed;   // Ghidra: 0x00545a1c
        *(int*)(e + 0x1c) -= Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *speed;        // Ghidra: 0x00545a16

        short beforeMark = *(short*)(e + (*(byte**)(e + 0x7c))[0x0e] * 4 + 0x80);

        *(int*)(e + 0x20) = beforeHeight + 0xc8000;
        FinalizeEntityPlacement2((uint*)e);                         // Ghidra: FUN_00533940
        *(int*)(e + 0x20) = *(int*)(e + 0x60);

        int quarter = (*speed & ~3) == 0 ? 1 : *speed >> 2;
        TryPublishCameraEffectNearEntity((uint*)e, 0x3e7, CrtRand() % quarter, 0x3e7, 0, 0);   // Ghidra: FUN_0041be60

        if (CrtRand() % 0x1e == 1)
        {
            SmokePuff_0046A630(e, SignedMask(CrtRand(), 3) + 1, 0x32);   // Ghidra: FUN_00472630
        }

        // ------------------------------------------------- la camera suit la pente
        int before = beforeHeight >> 0xc;
        int after = *(int*)(e + 0x20) >> 0xc;

        if (before < after) CameraPitchGlide_0046A5A0(e, 1, 0x100, SubArea1Bank);

        if (before > after && *(short*)(e + 0x16) != 4)
        {
            CameraPitchGlide_0046A5A0(e, 1, -0x100, SubArea1Bank);
        }

        if (before == after) CameraPitchGlide_0046A5A0(e, 1, 0, SubArea1Bank);

        // ------------------------------------------------- le virage lu sur le sol
        if (*(short*)(e + (*(byte**)(e + 0x7c))[0x0e] * 4 + 0x80) != beforeMark)
        {
            *(short*)(e + 0x16) += 1;
            if (*(short*)(e + 0x16) == 4) CameraPitchGlide_0046A5A0(e, 1, -0x200, SubArea1Bank);
        }

        if ((*(uint*)(e + 0x90) & 0x800) != 0)
        {
            *(uint*)(e + 0x90) = savedContacts & 0xffff;
            *(byte**)(e + 0x7c) = savedFace;
            *(int*)(e + 0x60) = savedGroundX;
            *(int*)(e + 0x64) = savedGroundZ;
            *(int*)(e + 0x1c) = *(int*)(e + 0x44);
            *(int*)(e + 0x24) = *(int*)(e + 0x4c);
            *(int*)(e + 0x80) = saved80;
            *(int*)(e + 0x84) = saved84;
            *(int*)(e + 0x88) = saved88;
            *(int*)(e + 0x8c) = saved8c;
        }

        // ------------------------------------------------- l'arrivee
        if ((uint)*travelled <= 0x2d28) return;

        *(short*)(e + 0x12) = 2;

        byte* member = Inp.s_SlotFlags_005e93c0;                    // 0x005e93c0
        for (int slot = 0; slot < 4; slot++, member += 0x9c)
        {
            RunEntityScript(s_Unknown_005e97cc, member, 0);         // Ghidra: FUN_00513dd0
        }
    }

    private static void EntityHandler_00470C20(byte* e) => TickMachineSled_00470C20(e);

    // =====================================================================
    // LOT 11 (2026-09-20) — LE TROISIEME TRAINEAU ET SA PISTE
    //
    // 0x00471150 est le plus gros corps du depot : 610 instructions, plus deux feuilles qui ne
    // servent qu'a lui, 0x00471a10 (300) et 0x00471f80 (400). Les trois ensemble sont UNE SEULE
    // CHOSE : la description du couloir, virage par virage.
    //
    // LA FORME EST TOUJOURS LA MEME. Le traineau cumule la distance parcourue dans 0x00571974, et
    // la piste est une suite de FENETRES de distance : quand le cumul tombe dans [lo, hi] et que le
    // virage precedent est fini (0x0057193c == 1), un quart de tour est arme, avec son sens et la
    // coordonnee sur laquelle se recaler en sortie. Rien d'autre. Ce sont des mesures relevees a la
    // main sur la carte en 1998, et c'est pour cela qu'il y en a cinquante.
    //
    // LA PISTE SE DIVISE EN CINQ ROUTES, numerotees dans 0x00571930, et DEUX BITS D'AIGUILLAGE de
    // 0x00571978 choisissent entre des variantes : le bit 0x04 dans la route 0, le bit 0x20 dans la
    // route 1, le bit 0x100 dans les routes 4 et 5. Chaque route finit en passant la main a la
    // suivante, en remettant le cumul a zero.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00471a10</c>. <b>Absente de <c>Blaze.c</c></b> ; 300 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00471A10.asm</c>.
    ///
    /// <para>
    /// <b>La route 0 de la piste</b>, et elle seule : la feuille rend la main sur-le-champ si
    /// <c>0x00571930</c> ne vaut pas <c>0</c>. Le bit <c>0x04</c> de <c>0x00571978</c> choisit entre
    /// deux trajets qui ne partagent que leur premier virage.
    /// </para>
    /// <list type="table">
    ///   <item><term>le trajet court (bit eteint)</term><description>trois virages, puis la route
    ///     s'acheve a <c>0x4bfa</c> ; ses reglages de vitesse portent sur les virages <c>0x18</c>,
    ///     <c>0x24</c>, <c>0x2c</c>, <c>0x2d</c>, <c>0x2f</c> et <c>0x36</c>..<c>0x38</c>
    ///     (plafond <c>0x32</c>), et <c>0x1f</c>..<c>0x22</c>, <c>0x26</c>..<c>0x2a</c> et
    ///     <c>0x3a</c> (plancher <c>5</c>).</description></item>
    ///   <item><term>le trajet long (bit allume)</term><description>cinq virages, fin a
    ///     <c>0x4268</c> ; ses reglages portent sur les virages <c>0x22</c>..<c>0x24</c>
    ///     (plafond <c>0x28</c>) et <c>0x1c</c> seul (plancher <c>5</c>, une image sur
    ///     trois).</description></item>
    /// </list>
    /// <para>
    /// La fin de route est la meme des deux cotes : <c>0x00571930</c> passe a <c>1</c>, le cumul et
    /// le numero de virage repartent de zero, et l'age est <b>force a <c>0x1d1</c></b> — ce qui
    /// remet la parite de l'age a un endroit connu, puisque tous les reglages de vitesse la lisent.
    /// </para>
    /// <para>
    /// Les deux derniers blocs, a partir de <c>0x00471d48</c>, sont <b>communs aux deux trajets</b> :
    /// les virages <c>1</c>, <c>5</c> et <c>9</c> accelerent, les virages <c>3</c> et <c>7</c>
    /// freinent.
    /// </para>
    /// </summary>
    private static void TrackRoute0_00471A10(byte* e)
    {
        int* route = Mem.EntityState(0x00571930);                   // Ghidra: DAT_00571930
        if (*route != 0) return;

        int* travelled = Mem.EntityState(0x00571974);               // Ghidra: DAT_00571974
        int* speed = Mem.EntityState(0x00571920);                   // Ghidra: DAT_00571920
        uint gate = *(uint*)Mem.EntityState(0x00571978);            // Ghidra: DAT_00571978

        TrackTurn(e, 0x10cc, 0x1194, 1, -0x384);

        if ((gate & 4) == 0)
        {
            // ------------------------------------------------- le trajet court
            TrackTurn(e, 0x15fe, 0x16a8, 1, -0xb54);
            TrackTurn(e, 0x22f6, 0x23f0, -1, -0x1194);
            TrackTurn(e, 0x3d54, 0x3e62, -1, 0x1004);

            if ((uint)*travelled >= 0x4bfa) CloseTrackRoute(e, route, travelled);

            short turn = *(short*)(e + 0x16);

            if ((turn == 0x18 || turn == 0x24 || turn == 0x2c || turn == 0x2d || turn == 0x2f
                 || (turn >= 0x36 && turn <= 0x38)) && IsEvenAge(e))
            {
                *speed += 1;
                if (*speed >= 0x32) *speed = 0x32;
            }

            turn = *(short*)(e + 0x16);
            if (((turn >= 0x1f && turn <= 0x22) || (turn >= 0x26 && turn <= 0x2a) || turn == 0x3a)
                && IsEvenAge(e))
            {
                *speed -= 1;
                if (*speed <= 5) *speed = 5;
            }
        }
        else
        {
            // ------------------------------------------------- le trajet long
            TrackTurn(e, 0x1aae, 0x1b58, 1, -0x6a4);
            TrackTurn(e, 0x23f0, 0x24b8, -1, -0xdac);
            TrackTurn(e, 0x2b5c, 0x2c88, -1, 0x1f4);
            TrackTurn(e, 0x30a2, 0x319c, 1, -0x76c);
            TrackTurn(e, 0x3de0, 0x3e80, -1, 0x1004);

            if ((uint)*travelled >= 0x4268) CloseTrackRoute(e, route, travelled);

            short turn = *(short*)(e + 0x16);

            if ((turn == 0x22 || turn == 0x23 || turn == 0x24) && IsEvenAge(e))
            {
                *speed += 1;
                if (*speed >= 0x28) *speed = 0x28;
            }

            // Ghidra: magie 0x55555556 sans decalage — « l'age est-il un multiple de 3 ».
            short age = *(short*)(e + 0x10);
            if (*(short*)(e + 0x16) == 0x1c && age == age / 3 * 3)
            {
                *speed -= 1;
                if (*speed <= 5) *speed = 5;
            }
        }

        // ------------------------------------------------- les deux blocs communs
        short common = *(short*)(e + 0x16);

        if ((common == 1 || common == 5 || common == 9) && IsEvenAge(e))
        {
            *speed += 1;
            if (*speed >= 0x32) *speed = 0x32;
        }

        common = *(short*)(e + 0x16);
        if ((common == 3 || common == 7) && IsEvenAge(e))
        {
            *speed -= 1;
            if (*speed <= 5) *speed = 5;
        }
    }

    /// <summary>Ghidra: <c>cdq ; sub eax,edx ; sar 1 ; shl 1 ; cmp</c> — la question « l'age
    /// <c>+0x10</c> est-il pair », posee telle quelle par le compilateur de 1998 a chacun des
    /// quatorze reglages de vitesse de la piste.</summary>
    private static bool IsEvenAge(byte* e)
    {
        int age = *(short*)(e + 0x10);
        return age == ((age - (age >> 31)) >> 1) << 1;
    }

    /// <summary>Une fenetre de la piste : quand le cumul <c>0x00571974</c> tombe dans
    /// <c>[lo, hi]</c> <b>et</b> que le virage precedent est fini (<c>0x0057193c == 1</c>), un quart
    /// de tour est arme. Le cumul est relu apres coup parce que le binaire le recharge a chaque
    /// fenetre — et pour cause, rien ne l'a change.</summary>
    private static void TrackTurn(byte* e, uint lo, uint hi, int dir, int snap)
    {
        uint travelled = (uint)*Mem.EntityState(0x00571974);        // Ghidra: DAT_00571974
        if (travelled < lo || travelled > hi) return;
        if (*Mem.EntityState(0x0057193c) != 1) return;              // Ghidra: DAT_0057193c

        SteerSled_0046A3F0(e, dir, snap, SubArea1Bank, 0x32);       // Ghidra: FUN_00471dd0
    }

    /// <summary>La fin d'une route de la piste, ecrite deux fois a l'identique dans
    /// <c>0x00471a10</c> : la route suivante est <c>1</c>, le cumul et le numero de virage
    /// repartent de zero, et l'age est cale a <c>0x1d1</c>.</summary>
    private static void CloseTrackRoute(byte* e, int* route, int* travelled)
    {
        *route = 1;
        *travelled = 0;
        *(short*)(e + 0x16) = 0;
        *(short*)(e + 0x10) = 0x1d1;
    }

    /// <summary>
    /// Ghidra: <c>0x00471f80</c>. <b>Absente de <c>Blaze.c</c></b> ; 400 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00471F80.asm</c>.
    ///
    /// <para>
    /// <b>La variante longue des routes 4 et 5</b>, celle que le bit <c>0x100</c> de
    /// <c>0x00571978</c> met en service. Seize virages, dont le cumul va jusqu'a <c>0xa21c</c> — le
    /// quadruple des autres routes. C'est la grande descente.
    /// </para>
    /// <para>
    /// <b>Deux virages se prennent a vitesse doublee</b> : a <c>0x4b64</c> et a <c>0x4e02</c>, la
    /// vitesse est doublee <b>juste avant</b> d'armer le quart de tour et rendue de moitie juste
    /// apres. Comme le pas de cap vaut <c>vitesse * 73 / 64</c>, le virage se boucle en deux fois
    /// moins d'images : ce sont les deux epingles du parcours.
    /// </para>
    /// <para>
    /// <b>Passe <c>0x9d6c</c></b> le traineau est en plus tire de <c>0xb000</c> vers les X negatifs
    /// a chaque image, tant qu'il n'a pas franchi <c>-0xe74000</c> — un rail, ajoute par-dessus les
    /// virages.
    /// </para>
    /// <para>
    /// <b>A <c>0xa21c</c> la course s'arrete</b> : la vitesse perd un point par image et
    /// <b>huit bouffees</b> partent a chaque image ; quand elle atteint zero, <c>+0x12</c> passe a
    /// <c>2</c> et le traineau a fini.
    /// </para>
    /// <para>
    /// Les reglages de vitesse de cette variante sont les plus riches de la piste — plafond
    /// <c>0x3c</c> aux virages <c>0</c>..<c>3</c>, <c>9</c> et <c>0x0a</c>, un <b>bond de 3</b> aux
    /// virages <c>0x39</c>..<c>0x3d</c> ecrete a <c>0x6e</c>, un freinage de <c>3</c> aux virages
    /// <c>0x41</c> et <c>0x47</c>..<c>0x4a</c> de la route <c>5</c> — et trois d'entre eux piquent
    /// la camera au passage.
    /// </para>
    /// <para>
    /// <b>La chute finale</b>, aux virages <c>0x5e</c>..<c>0x60</c> de la route <c>5</c> : le bit
    /// <c>0x200</c> s'allume, <c>+0x14</c> part de <c>0x0a</c> et perd <c>3</c> par image, le
    /// traineau tombe d'autant, la camera tangue de <c>-0x14</c>, et l'arrivee au fond
    /// (<c>-0x320000</c>) claque la voix <c>0x34</c>. Le virage <c>0x61</c> eteint le bit et lache
    /// <b>vingt-deux bouffees</b>.
    /// </para>
    /// </summary>
    private static void TrackRoutes45_00471F80(byte* e)
    {
        int* travelled = Mem.EntityState(0x00571974);               // Ghidra: DAT_00571974
        int* speed = Mem.EntityState(0x00571920);                   // Ghidra: DAT_00571920
        int* route = Mem.EntityState(0x00571930);                   // Ghidra: DAT_00571930
        uint* gate = (uint*)Mem.EntityState(0x00571978);            // Ghidra: DAT_00571978

        TrackTurn(e, 0x166c, 0x1770, -1, 0xdac);
        TrackTurn(e, 0x1e14, 0x1edc, -1, -0xdac);
        TrackTurn(e, 0x2418, 0x24b8, -1, 0x6a4);
        TrackTurn(e, 0x2af8, 0x2bc0, 1, -0x5dc);
        TrackTurn(e, 0x332c, 0x33f4, -1, -0x2bc);
        TrackTurn(e, 0x396c, 0x3a34, -1, 0x12c);
        TrackTurn(e, 0x3c8c, 0x3d54, 1, 0x1f4);

        // Les deux epingles : vitesse doublee le temps du quart de tour, rendue de moitie apres.
        if ((uint)*travelled >= 0x4b64 && (uint)*travelled <= 0x4c2c
            && *Mem.EntityState(0x0057193c) == 1)
        {
            *speed *= 2;
            SteerSled_0046A3F0(e, -1, 0xf3c, SubArea1Bank, 0x32);   // Ghidra: FUN_00471dd0
            *speed = (*speed - (*speed >> 31)) >> 1;
        }

        if ((uint)*travelled >= 0x4e02 && (uint)*travelled <= 0x4e84
            && *Mem.EntityState(0x0057193c) == 1)
        {
            *speed *= 2;
            SteerSled_0046A3F0(e, -1, 0x514, SubArea1Bank, 0x32);
            *speed = (*speed - (*speed >> 31)) >> 1;
        }

        TrackTurn(e, 0x6a40, 0x6b08, -1, -0xf3c);
        TrackTurn(e, 0x7080, 0x7148, -1, -0x1f4);
        TrackTurn(e, 0x7b70, 0x7c38, -1, -0x384);
        TrackTurn(e, 0x7fc6, 0x80e8, 1, 0x384);
        TrackTurn(e, 0x837c, 0x8408, -1, 0x12c);
        TrackTurn(e, 0x8fc0, 0x9088, -1, 0x10cc);
        TrackTurn(e, 0x9d6c, 0x9e34, 1, -0xe74);

        // ------------------------------------------------- le rail de la fin
        if ((uint)*travelled >= 0x9d6c && *(int*)(e + 0x1c) >= -0xe74000)
        {
            *(int*)(e + 0x1c) += -0xb000;
        }

        // ------------------------------------------------- l'arret
        if ((uint)*travelled >= 0xa21c)
        {
            *speed -= 1;

            for (int i = 8; i != 0; i--)
            {
                SmokePuff_0046A630(e, SignedMask(CrtRand(), 3) + 1, 0x32);   // Ghidra: FUN_00472630
            }

            if (*speed <= 0)
            {
                *speed = 0;
                *(short*)(e + 0x12) = 2;
            }
        }

        // ------------------------------------------------- les reglages de vitesse
        short turn = *(short*)(e + 0x16);

        if (((turn >= 1 && turn <= 3) || turn == 9 || turn == 0x0a) && IsEvenAge(e))
        {
            *speed += 1;
            if (*speed >= 0x3c) *speed = 0x3c;
        }

        turn = *(short*)(e + 0x16);
        if (turn >= 0x39 && turn <= 0x3d)
        {
            *speed += 3;
            if (*(short*)(e + 0x16) == 0x3a)
            {
                CameraPitchGlide_0046A5A0(e, 1, -0x200, SubArea1Bank);   // Ghidra: FUN_004725a0
            }
            if (*speed >= 0x6e) *speed = 0x6e;
        }

        turn = *(short*)(e + 0x16);
        if (turn >= 0x2c && turn <= 0x2e)
        {
            *speed -= 1;
            if (*speed <= 0x0a) *speed = 0x0a;
            if (*(short*)(e + 0x16) == 0x2d) CameraPitchGlide_0046A5A0(e, 1, 0x200, SubArea1Bank);
        }

        turn = *(short*)(e + 0x16);
        if ((turn == 0x41 && *route == 5) || (turn >= 0x47 && turn <= 0x4a && *route == 5))
        {
            *speed -= 3;
            if (*speed <= 0x0a) *speed = 0x0a;
            if (*(short*)(e + 0x16) == 0x49) CameraPitchGlide_0046A5A0(e, 1, 0x200, SubArea1Bank);
        }

        turn = *(short*)(e + 0x16);
        if (turn >= 0x5c && turn <= 0x5d && *route == 5)
        {
            for (int i = 8; i != 0; i--)
            {
                SmokePuff_0046A630(e, SignedMask(CrtRand(), 3) + 1, 0x32);
            }

            if (IsEvenAge(e))
            {
                *speed -= 1;
                if (*speed <= 0x0a) *speed = 0x0a;
            }

            if (*(short*)(e + 0x16) == 0x5c)
            {
                CameraPitchGlide_0046A5A0(e, 1, -0x200, SubArea1Bank);
            }
        }

        // ------------------------------------------------- la chute finale
        turn = *(short*)(e + 0x16);
        if (turn >= 0x5e && turn <= 0x60 && *route == 5)
        {
            if ((*gate & 0x200) == 0)
            {
                *gate |= 0x200;
                *(short*)(e + 0x14) = 0x0a;
            }

            *(short*)(e + 0x14) += -3;
            *(short*)(e + 0x34) += -0x14;
            *(int*)(e + 0x20) += *(short*)(e + 0x14) << 0xc;

            // Ghidra: `and eax,0FFFFF000h` avant la comparaison — le test porte sur la hauteur
            // arrondie a l'unite, pas sur sa valeur exacte.
            if ((*(int*)(e + 0x20) & ~0xfff) <= -0x320000)
            {
                *(int*)(e + 0x20) = -0x320000;
                *(short*)(e + 0x34) = 0;
                Aud.PlaySoundEffect(ZoneSound(0x34), 0x300, 0x1500, 0x1500);   // Ghidra: FUN_0053cea0
            }
        }

        if (*(short*)(e + 0x16) == 0x61 && (*gate & 0x200) != 0)
        {
            *gate &= ~0x200u;
            for (int i = 0x0b; i != 0; i--)
            {
                SmokePuff_0046A630(e, 2, 0x32);
                SmokePuff_0046A630(e, 3, 0x32);
            }
        }

        if (*(short*)(e + 0x16) == 0x65 && *route == 5)
        {
            *speed += 1;
            if (*speed >= 0x2d) *speed = 0x2d;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00471150</c> — sous-zone 1. <b>Absente de <c>Blaze.c</c></b> ;
    /// 610 instructions, <b>le plus gros corps du depot</b>,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00471150.asm</c>.
    ///
    /// <para>
    /// <b>Le troisieme traineau, celui qui parcourt toute la piste.</b> Il partage sa charpente avec
    /// <see cref="TickMachineSled_00470C20"/> — attente, depart, derive, camera, virage lu sur le
    /// sol — mais la ou les deux autres suivent un couloir grave dans le decor, celui-ci suit une
    /// <b>piste ecrite</b>, cinq routes de fenetres de distance
    /// (<see cref="TrackRoute0_00471A10"/> et <see cref="TrackRoutes45_00471F80"/>).
    /// </para>
    /// <list type="table">
    ///   <item><term>route 0</term><description>deleguee entierement a
    ///     <see cref="TrackRoute0_00471A10"/>, qui la termine en passant a la route
    ///     1.</description></item>
    ///   <item><term>route 1</term><description>un premier virage commun, puis deux variantes que le
    ///     bit <c>0x20</c> de <c>0x00571978</c> separe. La variante allumee finit par une
    ///     <b>descente</b> aux virages <c>0x22</c>..<c>0x26</c> ; la variante eteinte passe par un
    ///     recalage brutal de <c>+0x24</c> et s'acheve a <c>0x2152</c>.</description></item>
    ///   <item><term>route 3</term><description>un seul virage, et la route 4 a
    ///     <c>0x1068</c>.</description></item>
    ///   <item><term>routes 4 et 5</term><description>quatre virages, ou bien, si le bit
    ///     <c>0x100</c> est allume, toute la grande descente de
    ///     <see cref="TrackRoutes45_00471F80"/>.</description></item>
    /// </list>
    /// <para>
    /// <b>La descente de la route 1</b> merite d'etre lue : au premier passage elle retient la
    /// hauteur entiere dans <c>+0x5c</c> et allume le bit <c>0x200</c> — celui qui, plus bas, fait
    /// <b>geler la hauteur</b> apres le placement, de sorte que le traineau descend par le calcul et
    /// non par le sol. Ensuite <c>+0x14</c> monte d'un par image et la hauteur perd sa moitie, ce
    /// qui donne une chute qui s'accelere ; a <c>-250</c> le bit s'eteint, la camera est secouee et
    /// <b>vingt-deux bouffees</b> partent.
    /// </para>
    /// <para>
    /// <b>Le passage a la route 3</b> est le seul endroit de la piste ou le traineau s'arrete de
    /// tourner : le virage <c>0x27</c> de la route <c>1</c> remet le tangage a plat, le cumul a
    /// zero, la vitesse a <c>0x28</c>, l'age a <c>0x2a0</c>, et claque la voix <c>0x34</c>.
    /// </para>
    /// <para>
    /// <b>Sa voix est double</b> : le grondement <c>0x14</c> comme chez les deux autres, plus un
    /// craquement <c>0x35</c> une image sur <c>0x46</c>, a la hauteur <c>rand % 100 + 0x7ce</c> —
    /// beaucoup plus aigu. Les deux se taisent tant que le traineau attend
    /// (<c>+0x12 == -1</c>) ou qu'il est arrive (<c>+0x12 == 2</c>).
    /// </para>
    /// </summary>
    private static void TickTrackSled_00471150(byte* e)
    {
        int* speed = Mem.EntityState(0x00571920);                   // Ghidra: DAT_00571920
        int* travelled = Mem.EntityState(0x00571974);               // Ghidra: DAT_00571974
        int* route = Mem.EntityState(0x00571930);                   // Ghidra: DAT_00571930
        uint* gate = (uint*)Mem.EntityState(0x00571978);            // Ghidra: DAT_00571978

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = -1;
            *speed = 0x1e;
            *(short*)(e + 0x16) = 0;
            *Mem.EntityState(0x0057193c) = 1;                       // Ghidra: DAT_0057193c
            *(short*)(e + 0x98) = 0;
        }

        *(short*)(e + 0x98) += 1;

        int wait = 0x3c - *speed;
        if (wait < 3) wait = 3;

        if (*(short*)(e + 0x98) > wait && (*gate & 0x200) == 0)
        {
            *(short*)(e + 0x98) = 0;
            if (*(short*)(e + 0x12) != 2 && *(short*)(e + 0x12) != -1)
            {
                Aud.PlaySoundEffect(ZoneSound(0x14), CrtRand() % 100 + 0x2ce, 0x1200, 0x1200);   // Ghidra: FUN_0053cea0
            }
        }

        if (CrtRand() % 0x46 == 1 && *(short*)(e + 0x12) != 2 && *(short*)(e + 0x12) != -1)
        {
            Aud.PlaySoundEffect(ZoneSound(0x35), CrtRand() % 100 + 0x7ce, 0x900, 0x900);
        }

        byte* savedFace = *(byte**)(e + 0x7c);
        uint savedContacts = *(ushort*)(e + 0x90);
        int savedGroundX = *(int*)(e + 0x60);
        int savedGroundZ = *(int*)(e + 0x64);
        int saved80 = *(int*)(e + 0x80);
        int saved84 = *(int*)(e + 0x84);
        int saved88 = *(int*)(e + 0x88);
        int saved8c = *(int*)(e + 0x8c);

        *(short*)(e + 0x10) += 1;
        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);

        if (*(short*)(e + 0x10) >= 0x7530) *(short*)(e + 0x10) = 0x4e20;

        // ------------------------------------------------- l'attente, puis le depart
        if (*(short*)(e + 0x12) == -1
            && ActivePartySlotCount_00466D70() == CountPartyMembersMatching(e, 0x10, 0x40000000))
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)-*(short*)(e + 0x12);
            *speed = 0x19;
            *travelled = 0;
            *(short*)(e + 0x16) = 0;
            *route = 0;
            *(short*)(e + 0x14) = 0;
            *Mem.EntityState(0x0057196c) = 0;                       // Ghidra: DAT_0057196c
            *Mem.EntityState(0x00571970) = 0;                       // Ghidra: DAT_00571970
        }

        if (*(short*)(e + 0x12) == 1)
        {
            // ------------------------------------------------- la piste
            *travelled += *speed;
            SteerSled_0046A3F0(e, 0, 0, SubArea1Bank, 0x32);        // Ghidra: FUN_00471dd0
            CameraPitchGlide_0046A5A0(e, 0, 0, SubArea1Bank);       // Ghidra: FUN_004725a0
            TrackRoute0_00471A10(e);                                // Ghidra: FUN_00471a10

            if (*route == 1)
            {
                TrackTurn(e, 0x0be, 0x122, -1, -0x12c);

                if ((*gate & 0x20) != 0)
                {
                    TrackTurn(e, 0x53c, 0x5aa, 1, 0xa8c);
                    TrackTurn(e, 0xc08, 0xce4, 1, 0x6a4);
                    TrackTurn(e, 0x1220, 0x1324, -1, 0x1194);
                    TrackTurn(e, 0x19b4, 0x1a5e, -1, 0xf3c);

                    short turn = *(short*)(e + 0x16);
                    if ((turn == 0x0d || turn == 0x1d || turn == 0x1e) && IsEvenAge(e))
                    {
                        *speed += 1;
                        if (*speed >= 0x32) *speed = 0x32;
                    }

                    turn = *(short*)(e + 0x16);
                    if ((turn == 0x0f || turn == 0x1c) && IsEvenAge(e))
                    {
                        *speed -= 1;
                        if (*speed <= 5) *speed = 5;
                    }

                    // ------------------------------------------- la descente
                    if (*(short*)(e + 0x16) >= 0x22 && *(short*)(e + 0x16) <= 0x26)
                    {
                        if (*(short*)(e + 0x14) == 0)
                        {
                            *(int*)(e + 0x5c) = *(int*)(e + 0x20) >> 0xc;
                            *gate |= 0x200;
                        }

                        *(short*)(e + 0x14) += 1;
                        *(short*)(e + 0x34) += -7;
                        *(int*)(e + 0x5c) -= *(short*)(e + 0x14) >> 1;
                        *(int*)(e + 0x20) = *(int*)(e + 0x5c) << 0xc;

                        if (*(int*)(e + 0x5c) <= -0xfa)
                        {
                            *gate &= ~0x200u;
                            TryPublishCameraEffectNearEntity((uint*)e, 0x3e7, -0x14, 0x3e7, 0, 0);   // Ghidra: FUN_0041be60

                            for (int i = 0x0b; i != 0; i--)
                            {
                                SmokePuff_0046A630(e, 2, 0x32);     // Ghidra: FUN_00472630
                                SmokePuff_0046A630(e, 3, 0x32);
                            }
                        }
                    }
                }
                else
                {
                    TrackTurn(e, 0xc58, 0xc80, -1, 0x384);
                    TrackTurn(e, 0x1162, 0x122a, -1, -0x758);

                    if ((uint)*travelled >= 0x1770 && (uint)*travelled <= 0x179d)
                    {
                        *(int*)(e + 0x24) = -0x762000;
                    }

                    TrackTurn(e, 0x1d10, 0x1d1a, -1, 0x1004);

                    if ((uint)*travelled >= 0x2152 && (*gate & 0x20) == 0)
                    {
                        *travelled = 1;
                        *(short*)(e + 0x16) = 1;
                    }
                }
            }

            if (*route == 3)
            {
                TrackTurn(e, 0x384, 0x3e8, -1, 0x514);

                if ((uint)*travelled >= 0x1068)
                {
                    *travelled = 0;
                    *route = 4;
                }
            }

            if (*route == 4 || *route == 5)
            {
                if ((*gate & 0x100) == 0)
                {
                    TrackTurn(e, 0x118, 0x1f4, 1, 0x64);
                    TrackTurn(e, 0xa50, 0xa8c, 1, -0x514);
                    TrackTurn(e, 0x1068, 0x10cc, 1, 0x76c);
                    TrackTurn(e, 0x1996, 0x1a2c, 1, 0x514);

                    if ((uint)*travelled >= 0x1e82)
                    {
                        *travelled = 1;
                        *(short*)(e + 0x16) = 0x35;
                    }
                }

                if ((*gate & 0x100) != 0) TrackRoutes45_00471F80(e);   // Ghidra: FUN_00471f80
            }

            // ------------------------------------------------- la derive
            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *speed;   // Ghidra: 0x00545a1c
            *(int*)(e + 0x1c) -= Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *speed;        // Ghidra: 0x00545a16

            int beforeHeight = *(int*)(e + 0x20);
            short beforeMark = *(short*)(e + (*(byte**)(e + 0x7c))[0x0e] * 4 + 0x80);

            *(int*)(e + 0x20) = beforeHeight + 0xc8000;
            FinalizeEntityPlacement2((uint*)e);                     // Ghidra: FUN_00533940
            *(int*)(e + 0x20) = *(int*)(e + 0x60);

            int quarter = (*speed & ~3) == 0 ? 1 : *speed >> 2;
            TryPublishCameraEffectNearEntity((uint*)e, 0x3e7, CrtRand() % quarter, 0x3e7, 0, 0);   // Ghidra: FUN_0041be60

            if (CrtRand() % 0x1e == 1)
            {
                SmokePuff_0046A630(e, SignedMask(CrtRand(), 3) + 1, 0x32);
            }

            // Le bit 0x200 GELE la hauteur : pendant la descente ecrite, le sol n'a plus voix.
            if ((*gate & 0x200) != 0) *(int*)(e + 0x20) = beforeHeight;

            // ------------------------------------------------- la camera suit la pente
            int before = beforeHeight >> 0xc;
            int after = *(int*)(e + 0x20) >> 0xc;

            if ((*gate & 0x100) == 0 || *(short*)(e + 0x16) != 0x2d)
            {
                if (before < after) CameraPitchGlide_0046A5A0(e, 1, 0x100, SubArea1Bank);
                if (before > after) CameraPitchGlide_0046A5A0(e, 1, -0x100, SubArea1Bank);
                if (before == after) CameraPitchGlide_0046A5A0(e, 1, 0, SubArea1Bank);
            }

            // ------------------------------------------------- le virage lu sur le sol
            if (*(short*)(e + (*(byte**)(e + 0x7c))[0x0e] * 4 + 0x80) != beforeMark)
            {
                *(short*)(e + 0x16) += 1;

                if (*route != 5 && *(short*)(e + 0x16) == 0x46 && (*gate & 0x100) != 0)
                {
                    *(short*)(e + 0x16) = 0;
                    *(short*)(e + 0x10) = 0x37e;
                }

                if (*(short*)(e + 0x16) == 0x27 && *route == 1)
                {
                    *(short*)(e + 0x34) = 0;
                    *travelled = 0;
                    *route = 3;
                    *speed = 0x28;
                    *(short*)(e + 0x10) = 0x2a0;
                    Aud.PlaySoundEffect(ZoneSound(0x34), 0x300, 0x1500, 0x1500);   // Ghidra: FUN_0053cea0
                }
            }

            if ((*(uint*)(e + 0x90) & 0x800) != 0)
            {
                *(uint*)(e + 0x90) = savedContacts & 0xffff;
                *(byte**)(e + 0x7c) = savedFace;
                *(int*)(e + 0x60) = savedGroundX;
                *(int*)(e + 0x64) = savedGroundZ;
                *(int*)(e + 0x1c) = *(int*)(e + 0x44);
                *(int*)(e + 0x24) = *(int*)(e + 0x4c);
                *(int*)(e + 0x80) = saved80;
                *(int*)(e + 0x84) = saved84;
                *(int*)(e + 0x88) = saved88;
                *(int*)(e + 0x8c) = saved8c;
            }
        }

        if (*(short*)(e + 0x12) == 2) *(uint*)e |= 0x6000;
    }

    private static void EntityHandler_00471150(byte* e) => TickTrackSled_00471150(e);
}
