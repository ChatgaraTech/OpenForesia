// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DE LA GROTTE SCELLEE — zone 10, table 0x0056adc8.
//
// Ouvert le 2026-09-19 avec le lot 1 de la zone. Ce que la grotte DECLENCHE — ce qui compte le
// groupe ou lance un script — ira dans `SealedCaveMecanismes.cs` le jour ou un corps le demandera.
//
// Le son d'une zone se lit par `ZoneSound` (`Mine.cs`) : meme classe partielle.
// Dumps : Legacy/Raw/Blaze/SealedCave/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_0046c800 @ 0x0046c800 — sous-zones 5, 8 et 10. Decompilee dans <c>Blaze.c</c>,
    /// 13 instructions. <b>Une amorce qui reste</b> : a sa premiere image elle fait naitre l'entite
    /// d'incantation <c>0xfd</c> (<see cref="SpawnCastEntity_0047AAE0"/>) et passe a l'etat 1, ou
    /// elle ne fait plus rien — sans s'eteindre.
    /// </summary>
    private static void EntityHandler_0046C800(byte* e)
    {
        if (*(short*)(e + 0x10) != 0) return;

        SpawnCastEntity_0047AAE0(e, 0xfd, 0, -1);                   // Ghidra: FUN_0047aae0
        *(short*)(e + 0x10) = 1;
    }

    // =====================================================================
    // LOT 2 (2026-09-19)
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00468a20</c> (bloc 0), <c>0x0046b570</c> (2-3), <c>0x0046e6d0</c> (6-7) — trois
    /// corps sans aucune immediate differente. Absents de <c>Blaze.c</c> ; 44 instructions.
    ///
    /// <para>
    /// <b>Le souffle qui dresse.</b> A l'etat 0 il se tient a plat (<c>+0x2a = 0</c>) et arme sa
    /// phase a <c>0x80</c> ; <b>rien dans le corps ne le fait passer a l'etat 1</b> — c'est un
    /// script ou un voisin qui le reveille. A l'etat 1, un son (voix <c>0x27</c> de la zone), puis
    /// son echelle verticale suit deux fois le sinus d'une phase qui avance d'un seizieme de tour par
    /// image ; passe un demi-tour, il revient a l'etat 0, a plat.
    /// </para>
    /// </summary>
    private static void EntityHandler_00468A20(byte* e)
    {
        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x12) = 0x80;
            *(short*)(e + 0x98) = 0;
            return;
        }

        if (state != 1) return;

        if (*(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x27), 0x400, 0x2000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 1;
        }

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));              // Ghidra: FUN_00545a1c
        *(short*)(e + 0x12) += 0x100;
        *(short*)(e + 0x2a) = (short)((sin << 0xc) >> 0xb);

        if (*(short*)(e + 0x12) <= 0x800) return;

        *(short*)(e + 0x10) = 0;
        *(short*)(e + 0x2a) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0046c020</c> (sous-zones 2-3) et <c>0x0046de80</c> (5-8-10) — deux corps sans
    /// aucune immediate differente. Absents de <c>Blaze.c</c> ; 97 instructions.
    ///
    /// <para>
    /// <b>Le battant qui frappe.</b> Il retient son cap de repos, et vise ce cap plus
    /// <c>+0x14</c>. Il y tourne en accelerant de 10 par image, puis revient au repos de meme, en
    /// sonnant (voix <c>0x1f</c>) a la naissance et a chaque bout de course. <b>Si son
    /// <c>+0x14</c> vaut <c>0x1000</c> — un tour entier — il ne revient pas</b> : arrive au bout, il
    /// est remis au repos et repart, il tourne sans fin.
    /// </para>
    /// <para>
    /// <b>A chaque image, il frappe le groupe</b> (<see cref="StrikePartyMembers_0045DA40"/>, dix pour
    /// cent, les membres que son octet de contact designe). Abimer n'est pas decider : c'est du
    /// decor.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046C020(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            short sweep = *(short*)(e + 0x14);
            short rest = *(short*)(e + 0x32);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = rest;
            *(int*)(e + 0x5c) = sweep == 0x1000 ? 1 : 0;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = (short)(sweep + rest);
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short speed = *(short*)(e + 0x16);
            *(short*)(e + 0x32) += speed;
            *(short*)(e + 0x16) = (short)(speed + 10);

            if (*(short*)(e + 0x32) > *(short*)(e + 0x14))
            {
                *(short*)(e + 0x32) = *(short*)(e + 0x14);

                if (*(int*)(e + 0x5c) == 0)
                {
                    *(short*)(e + 0x10) = 2;
                    TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x400, 0x1000, 0, 0);
                }
                else
                {
                    *(short*)(e + 0x32) = *(short*)(e + 0x12);
                }

                *(short*)(e + 0x16) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short speed = *(short*)(e + 0x16);
            *(short*)(e + 0x32) -= speed;
            *(short*)(e + 0x16) = (short)(speed + 10);

            if (*(short*)(e + 0x32) < *(short*)(e + 0x12))
            {
                *(short*)(e + 0x32) = *(short*)(e + 0x12);
                *(short*)(e + 0x10) = 1;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x400, 0x1000, 0, 0);
                *(short*)(e + 0x16) = 0;
            }
        }

        StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);               // Ghidra: FUN_0045da40
    }

    // =====================================================================
    // LOT 4 (2026-09-19) — les « B » des blocs 0 a 3
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00470be0</c> — sous-zone 1. Absente de <c>Blaze.c</c> ; 24 instructions.
    /// <b>Ce qui n'existe que dans un etat du lieu</b> : tant que le mot <c>+0x12</c> du premier
    /// enregistrement de <c>0x005fa5e0</c> ne vaut pas 2, l'entite porte les bits <c>0x6000</c> de
    /// son mot d'etat (leur sens n'est pas etabli) ; quand il vaut 2, elle les perd — et si son
    /// sous-type est <c>0x25</c>, son <c>+0x34</c> rejoint <c>0x514</c> d'un huitieme par image.
    /// </summary>
    private static void EntityHandler_00470BE0(byte* e)
    {
        if (*(short*)(Mem.Entity(0x005fa5e0) + 0x12) != 2)          // Ghidra: DAT_005fa5f2
        {
            *(uint*)e |= 0x6000;
            return;
        }

        *(uint*)e &= 0xffff9fff;
        if (e[8] != 0x25) return;

        short pitch = *(short*)(e + 0x34);
        *(short*)(e + 0x34) = (short)(pitch + ((0x514 - pitch) >> 3));
    }

    /// <summary>
    /// Ghidra: <c>0x00470260</c> — sous-zone 1. Presente dans <c>Blaze.c</c> ; 29 instructions.
    /// <b>Une amorce qui attend un script</b> : a sa naissance elle range sa vitesse de depart
    /// (<c>+0x14</c>, <c>+0x30</c>, <c>+0x16</c> vers <c>+0x50..+0x58</c>) et se met a l'etat
    /// <c>0x63</c>, qu'aucune ligne du corps ne quitte. Si l'on pose l'etat 1 depuis dehors, elle
    /// sonne (voix <c>0x26</c>), fait naitre l'incantation <c>0xff</c> et retourne a <c>0x63</c>.
    /// </summary>
    private static void EntityHandler_00470260(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int vy = *(short*)(e + 0x30);
            int vx = *(short*)(e + 0x14);
            int vz = *(short*)(e + 0x16);
            *(short*)(e + 0x10) = 0x63;
            *(int*)(e + 0x54) = vy;
            *(short*)(e + 0x30) = 0;
            *(int*)(e + 0x50) = vx;
            *(int*)(e + 0x58) = vz;
        }

        if (*(short*)(e + 0x10) != 1) return;

        Aud.PlaySoundEffect(0x26, 0x5dc, 0x1500, 0x1500);          // Ghidra: FUN_0053cea0
        SpawnCastEntity_0047AAE0(e, 0xff, 0, -1);                   // Ghidra: FUN_0047aae0
        *(short*)(e + 0x10) = 0x63;
    }

    /// <summary>
    /// Ghidra: <c>0x0046c260</c> — sous-zones 2 et 3. Presente dans <c>Blaze.c</c> ; 39
    /// instructions. <b>La roue qui tourne sur place</b> : elle se deplace par
    /// <see cref="MoveSceneEntity_00455B30"/> (masque <c>0x8842</c> en sous-zone 7, <c>0xa842</c>
    /// ailleurs) abaissee de 100 unites le temps du deplacement, tourne de <c>0x44</c> par image,
    /// et passe ses volumes de declenchement. A la naissance, <c>+0x14</c> est range en
    /// <c>+0x12</c>.
    /// <para>
    /// <b>Deux lectures de sinus sans effet</b> : le corps appelle deux fois la table de sinus sur son
    /// cap et jette le resultat. Transcrites, parce que ce sont des appels du binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046C260(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            _ = Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));    // Ghidra: resultat jete
            *(int*)(e + 0x20) += unchecked((int)0xfff9c000);
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 0;
        }

        MoveSceneEntity_00455B30(e, (byte)s_Unknown_005de52c != 7 ? 0xa842u : 0x8842u);   // Ghidra: FUN_00455b30

        *(short*)(e + 0x32) += 0x44;
        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

        _ = Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));        // Ghidra: resultat jete
        *(int*)(e + 0x20) += 0x64000;

        TestTriggerVolumes_00534CD0((uint*)e);                      // Ghidra: FUN_00534cd0
    }

    /// <summary>
    /// Ghidra: <c>0x0046abc0</c> — sous-zones 0, 2 et 3. Absente de <c>Blaze.c</c> ; 42
    /// instructions. <b>Ce qui remonte</b> : a la naissance il retient sa hauteur (<c>+0x16</c>),
    /// prend le bit <c>0x4000</c>, et l'octet <c>+0x07</c> passe de <c>0x10</c> a <c>0x20</c> ;
    /// un <c>+0x14</c> non nul le fige a l'etat <c>0x63</c>, le bit <c>0x10</c> de son mot d'etat
    /// l'envoie a l'etat 1. Il monte alors de 10 unites par image et, passe <c>-0x3c</c> unites,
    /// retombe a sa hauteur retenue avec <c>+0x54 = 0x14</c>, et l'octet <c>+0x07</c> revient.
    /// </summary>
    private static void EntityHandler_0046ABC0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[7] = (byte)((e[7] & 0xef) | 0x20);
            uint flags = *(uint*)e | 0x4000;
            *(uint*)e = flags;
            if (*(short*)(e + 0x14) != 0) *(short*)(e + 0x10) = 0x63;

            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x20) >> 0xc);
            if ((flags & 0x10) != 0) *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) += 0xa000;
        if (*(int*)(e + 0x20) <= -0x3c000) return;

        *(int*)(e + 0x20) = *(short*)(e + 0x16) << 0xc;
        e[7] = (byte)((e[7] & 0xdf) | 0x10);
        *(short*)(e + 0x10) = 0;
        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0x14;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00470860</c> — sous-zone 1. Absente de <c>Blaze.c</c> ; 44 instructions.
    /// <b>Le battant qui s'ouvre sur un drapeau</b> : son <c>+0x14</c> (1, 2 ou 3) designe le bit
    /// <c>0x01</c>, <c>0x08</c> ou <c>0x40</c> de l'octet <c>0x00571978</c> ; tant que ce bit est
    /// eteint il ne fait rien. Pose, il retient son cap et pivote d'un quart de tour, de plus en
    /// plus vite (40, puis +10 par image), puis se fige a l'etat 2.
    /// </summary>
    private static void EntityHandler_00470860(byte* e)
    {
        byte gate = *(byte*)Mem.EntityState(0x00571978);            // Ghidra: DAT_00571978, lu en octet
        short which = *(short*)(e + 0x14);

        if (which == 1 && (gate & 0x01) == 0) return;
        if (which == 2 && (gate & 0x08) == 0) return;
        if (which == 3 && (gate & 0x40) == 0) return;

        short state = *(short*)(e + 0x10);
        if (state == 2) return;

        if (state == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = *(short*)(e + 0x32);
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0x28;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int turn = *(int*)(e + 0x5c);
        *(short*)(e + 0x32) += (short)turn;
        *(int*)(e + 0x5c) = turn + 10;

        short rest = *(short*)(e + 0x16);
        if (*(short*)(e + 0x32) < rest + 0x400) return;

        *(short*)(e + 0x10) = 2;
        *(short*)(e + 0x32) = (short)(rest + 0x400);
    }

    /// <summary>
    /// Ghidra: <c>0x0046bf80</c> — sous-zones 2 et 3. Presente dans <c>Blaze.c</c> ; 45
    /// instructions. <b>Ce qui tourne en rond</b> : il retient sa position de naissance comme
    /// centre, puis se place a 450 unites de lui dans son cap, et le cap recule de 15 par image —
    /// un tour complet en 273 images.
    /// </summary>
    private static void EntityHandler_0046BF80(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x1c) >> 0xc);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x24) >> 0xc);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short course = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) = (*(short*)(e + 0x12) << 0xc) + (int)Winsys3d.GetSinTableEntry(course) * 450;
        *(int*)(e + 0x24) = (*(short*)(e + 0x16) << 0xc) + (int)Winsys3d.GetCosTableEntry(course) * 450;

        *(short*)(e + 0x32) -= 0x0f;
        if (*(short*)(e + 0x32) < 0) *(short*)(e + 0x32) += 0x1000;
    }

    /// <summary>
    /// Ghidra: <c>0x00469930</c> — sous-zone 0. Absente de <c>Blaze.c</c> ; 53 instructions.
    /// <b>La pierre qui se detache</b> : rien tant que le bit de rang <c>+0x14</c> du mot
    /// <c>0x0057182c</c> est eteint. Allume, elle sonne (voix <c>0x2c</c> de la zone), tire un
    /// roulis (-1, 0 ou 1 par image) et une rotation de 8 a 17, puis tombe de plus en plus vite —
    /// <c>+0x12</c> unites de plus a chaque image — et disparait passe 3 500 unites sous l'origine.
    /// </summary>
    private static void EntityHandler_00469930(byte* e)
    {
        uint gate = *(uint*)Mem.EntityState(0x0057182c);           // Ghidra: DAT_0057182c
        if (((gate >> (e[0x14] & 0x1f)) & 1) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = CrtRand() % 10 + 8;
            *(short*)(e + 0x16) = (short)(CrtRand() % 3 - 1);
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2c), 0x200, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) += 1;
        *(short*)(e + 0x32) += *(short*)(e + 0x16);
        *(int*)(e + 0x20) -= *(short*)(e + 0x12) << 0xc;
        *(short*)(e + 0x34) += (short)*(int*)(e + 0x5c);

        if (*(int*)(e + 0x20) <= unchecked((int)0xff254000)) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00469af0</c> — sous-zone 0. Absente de <c>Blaze.c</c> ; 54 instructions.
    /// <b>Ce qui s'abaisse une fois</b>, sur le mot <c>0x00571828</c> : rien tant que son bit
    /// <c>0x400</c> est eteint. Allume, une voix (<c>0x39</c>) au depart, puis <c>+0x34</c> descend
    /// de <c>0x46</c> par image ; passe zero, une seconde voix (<c>0x3a</c>), et le corps pose
    /// lui-meme le bit <c>0x800</c> du mot. Ce bit pose, deux images plus tard l'entite prend les
    /// bits <c>0x6000</c> de son mot d'etat — et le refait toutes les deux images.
    /// </summary>
    private static void EntityHandler_00469AF0(byte* e)
    {
        uint* word = (uint*)Mem.EntityState(0x00571828);           // Ghidra: DAT_00571828
        uint gate = *word;

        if ((gate & 0x400) == 0) return;

        if ((gate & 0x800) != 0)
        {
            *(short*)(e + 0x10) += 1;
            if (*(short*)(e + 0x10) != 2) return;

            *(short*)(e + 0x10) = 0;
            *(uint*)e |= 0x6000;
            return;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x39), 0x400, 0x1800, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x10) = 1;
        }

        *(short*)(e + 0x34) -= 0x46;
        if (*(short*)(e + 0x34) >= 0) return;

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x3a), 0x400, 0x1800, 0, 0);
        *(short*)(e + 0x34) = 0;
        *word |= 0x800;
        *(short*)(e + 0x10) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0046c2f0</c> — sous-zones 2 et 3. Presente dans <c>Blaze.c</c> ; 57
    /// instructions. <b>La roue qui roule sur place et se note</b> : la roue de
    /// <see cref="EntityHandler_0046C260"/>, masque fixe <c>0xa842</c>, mais cette fois la hauteur
    /// suit deux fois le sinus du cap (le sinus est lu, pas jete) ; et chaque image elle recopie sa
    /// position dans sa fiche de placement de la sous-zone (liste <c>+0x14</c>, rang <c>+0x16</c>),
    /// les quatre mots <c>+0x10..+0x16</c> de la fiche a -1. <b>Rien ne pose l'etat 1</b> dans le
    /// corps : la premiere moitie attend qu'on le lui donne.
    /// </summary>
    private static void EntityHandler_0046C2F0(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));
            *(int*)(e + 0x20) += unchecked((int)0xfff9c000) - sin * 2;
        }

        MoveSceneEntity_00455B30(e, 0xa842);                        // Ghidra: FUN_00455b30

        *(short*)(e + 0x32) += 0x44;
        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));
        *(int*)(e + 0x20) += lift * 2 + 0x64000;

        TestTriggerVolumes_00534CD0((uint*)e);                      // Ghidra: FUN_00534cd0

        byte* record = ((byte**)*(byte***)(s_Unknown_005e97cc + 0x30))[*(short*)(e + 0x14)]
                       + *(short*)(e + 0x16) * 0x20;

        *(short*)(record + 0x04) = (short)(*(int*)(e + 0x1c) >> 0xc);
        *(short*)(record + 0x06) = (short)(*(int*)(e + 0x20) >> 0xc);
        *(short*)(record + 0x08) = (short)(*(int*)(e + 0x24) >> 0xc);
        *(short*)(record + 0x10) = -1;
        *(short*)(record + 0x12) = -1;
        *(short*)(record + 0x14) = -1;
        *(short*)(record + 0x16) = -1;
    }

    /// <summary>
    /// Ghidra: <c>0x0046b610</c> — sous-zones 2 et 3. Absente de <c>Blaze.c</c> ; 59 instructions.
    /// <b>Le plateau qui fait demi-tour</b> : il retient son cap, attend <c>+0x16</c> images, tourne
    /// d'un demi-tour a <c>+0x14</c> par image, attend de nouveau, revient, et recommence. Quatre
    /// etats en boucle (1 a 4).
    /// </summary>
    private static void EntityHandler_0046B610(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x32);
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x5c) += 1;
            if (*(int*)(e + 0x5c) >= *(short*)(e + 0x16)) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short rest = *(short*)(e + 0x12);
            *(short*)(e + 0x32) += *(short*)(e + 0x14);

            if (*(short*)(e + 0x32) >= rest + 0x800)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x32) = (short)(rest + 0x800);
                *(int*)(e + 0x5c) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(int*)(e + 0x5c) += 1;
            if (*(int*)(e + 0x5c) >= *(short*)(e + 0x16)) *(short*)(e + 0x10) = 4;
        }

        if (*(short*)(e + 0x10) != 4) return;

        *(short*)(e + 0x32) -= *(short*)(e + 0x14);
        if (*(short*)(e + 0x32) > *(short*)(e + 0x12)) return;

        *(short*)(e + 0x32) = *(short*)(e + 0x12);
        *(short*)(e + 0x10) = 1;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00470b00</c> — sous-zone 1. Absente de <c>Blaze.c</c> ; 60 instructions. La
    /// jumelle de <see cref="EntityHandler_00469AF0"/> sur le mot <c>0x00571978</c> : bit
    /// <c>0x1000</c> pour partir, bit <c>0x2000</c> pose en fin de course, memes deux voix.
    /// <para>
    /// <b>Sa branche finale differe</b> : le compteur y est remis a zero a CHAQUE image, si bien que
    /// seul le cas « 1 » (bit <c>0x4000</c> du mot d'etat) se produit ; le cas « 2 » (bits
    /// <c>0x6000</c>) est ecrit dans le corps et jamais atteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_00470B00(byte* e)
    {
        uint* word = (uint*)Mem.EntityState(0x00571978);           // Ghidra: DAT_00571978
        uint gate = *word;

        if ((gate & 0x1000) == 0) return;

        if ((gate & 0x2000) != 0)
        {
            *(short*)(e + 0x10) += 1;
            short count = *(short*)(e + 0x10);
            if (count == 1) *(uint*)e |= 0x4000;
            if (count == 2) *(uint*)e |= 0x6000;
            *(short*)(e + 0x10) = 0;
            return;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x39), 0x400, 0x1800, 0, 0);   // Ghidra: FUN_00468080
        }

        *(short*)(e + 0x34) -= 0x46;
        if (*(short*)(e + 0x34) >= 0) return;

        *(short*)(e + 0x34) = 0;
        *word |= 0x2000;
        *(short*)(e + 0x10) = 0;
        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x3a), 0x400, 0x1800, 0, 0);
    }

    /// <summary>
    /// Ghidra: <c>0x00470900</c> — sous-zone 1. Absente de <c>Blaze.c</c> ; 73 instructions.
    /// <b>Le levier</b> : son <c>+0x14</c> (1, 2, 3) designe le bit <c>0x02</c>, <c>0x10</c> ou
    /// <c>0x80</c> du mot <c>0x00571978</c> qui l'arme. Arme, il sonne (voix <c>0x24</c>) et bascule
    /// (<c>+0x34</c> descend de 60, puis de 10 de plus a chaque image) ; passe 700 unites, il se
    /// fige et pose a son tour le bit <c>0x04</c>, <c>0x20</c> ou <c>0x100</c> du meme mot.
    /// </summary>
    private static void EntityHandler_00470900(byte* e)
    {
        uint* word = (uint*)Mem.EntityState(0x00571978);           // Ghidra: DAT_00571978
        byte gate = *(byte*)word;
        short which = *(short*)(e + 0x14);

        if (which == 1 && (gate & 0x02) == 0) return;
        if (which == 2 && (gate & 0x10) == 0) return;
        if (which == 3 && (gate & 0x80) == 0) return;

        short state = *(short*)(e + 0x10);
        if (state == 2) return;

        if (state == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0x3c;
            *(short*)(e + 0x16) = *(short*)(e + 0x34);
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x24), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x34) -= (short)*(int*)(e + 0x5c);
        *(int*)(e + 0x5c) += 10;
        if (*(short*)(e + 0x34) > *(short*)(e + 0x16) - 0x2bc) return;

        *(short*)(e + 0x10) = 2;

        switch (*(short*)(e + 0x14))
        {
            case 1: *word |= 0x004; return;
            case 2: *word |= 0x020; return;
            case 3: *word |= 0x100; return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00468950</c> — sous-zone 0. Absente de <c>Blaze.c</c> ; 65 instructions.
    /// <b>Le tremplin</b>. A chaque image il efface le bit <c>0x2000</c> de son mot d'etat et le
    /// bit <c>0x10</c> de son octet <c>+0x07</c>, et tient <c>+0x2a = 0x1770</c>. Il ne s'eveille que
    /// sous le bit <c>0x10</c> de son mot d'etat : il reveille alors l'enregistrement situe
    /// <c>+0x14</c> rangs plus loin dans sa table (son <c>+0x10</c> passe a 1), retient sa hauteur, et
    /// bondit d'une demi-onde de sinus (256 unites au sommet). Au quart d'onde il pose le bit
    /// <c>0x10</c> de <c>+0x07</c> et une vitesse montante de <c>+0x16</c> (20 par defaut) ; a la
    /// demi-onde il retombe a sa hauteur.
    /// </summary>
    private static void EntityHandler_00468950(byte* e)
    {
        e[7] &= 0xef;
        uint flags = *(uint*)e & 0xffffdfff;
        *(uint*)e = flags;
        *(short*)(e + 0x2a) = 0x1770;

        if ((flags & 0x10) == 0 && *(short*)(e + 0x10) == 0) return;

        BounceSpringboard_0046897F(e);
    }

    /// <summary>
    /// Ghidra: <c>LAB_0046897f</c> — <b>le bond du tremplin</b>, la moitie de
    /// <see cref="EntityHandler_00468950"/> qui suit l'eveil. Emise une seconde fois, a l'identique,
    /// par <see cref="EntityHandler_0046B440"/> (<c>LAB_0046b4c8</c>).
    /// </summary>
    private static void BounceSpringboard_0046897F(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;

            // Ghidra: `[esi + (+0x14) * 0x9c + 0x10]` — un voisin de la meme table, reveille.
            *(short*)(e + *(short*)(e + 0x14) * 0x9c + 0x10) = 1;

            *(short*)(e + 0x9a) = (short)(*(int*)(e + 0x20) >> 0xc);
            if (*(short*)(e + 0x16) == 0) *(short*)(e + 0x16) = 0x14;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int rest = *(short*)(e + 0x9a);
        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += 0x100;
        *(int*)(e + 0x20) = (sin + (rest << 4)) << 8;

        short phase = *(short*)(e + 0x12);
        if (phase > 0x800)
        {
            *(short*)(e + 0x10) = 0;
            *(int*)(e + 0x20) = rest << 0xc;
        }

        if (phase != 0x400) return;

        *(int*)(e + 0x50) = 0;
        e[7] |= 0x10;
        *(int*)(e + 0x58) = 0;
        *(int*)(e + 0x54) = *(short*)(e + 0x16) << 0xc;
    }

    /// <summary>
    /// Ghidra: <c>0x00470750</c> — sous-zone 1. Absente de <c>Blaze.c</c> ; 74 instructions.
    /// <b>La roue qui transmet</b> : armee par les memes bits que le battant
    /// <see cref="EntityHandler_00470860"/> (<c>0x01</c>, <c>0x08</c>, <c>0x40</c> du mot
    /// <c>0x00571978</c>, selon <c>+0x14</c>), elle sonne (voix <c>0x20</c>) et tourne de plus en plus
    /// vite. Passe un cinquieme de tour, elle pose — a chaque image — le bit suivant de la chaine
    /// (<c>0x02</c>, <c>0x10</c>, <c>0x80</c>), celui qui arme le levier
    /// <see cref="EntityHandler_00470900"/> ; passe deux cinquiemes, elle s'arrete, sans se caler.
    /// </summary>
    private static void EntityHandler_00470750(byte* e)
    {
        uint* word = (uint*)Mem.EntityState(0x00571978);           // Ghidra: DAT_00571978
        byte gate = *(byte*)word;
        short which = *(short*)(e + 0x14);

        if (which == 1 && (gate & 0x01) == 0) return;
        if (which == 2 && (gate & 0x08) == 0) return;
        if (which == 3 && (gate & 0x40) == 0) return;

        short state = *(short*)(e + 0x10);
        if (state == 2) return;

        if (state == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0x28;
            *(short*)(e + 0x16) = *(short*)(e + 0x32);
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x20), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) != 1) return;

        int turn = *(int*)(e + 0x5c);
        *(short*)(e + 0x32) += (short)turn;
        *(int*)(e + 0x5c) = turn + 10;

        if (*(short*)(e + 0x32) >= *(short*)(e + 0x16) + 0x320)
        {
            switch (*(short*)(e + 0x14))
            {
                case 1: *word |= 0x02; break;
                case 2: *word |= 0x10; break;
                case 3: *word |= 0x80; break;
            }
        }

        // Ghidra: LAB_00470837
        if (*(short*)(e + 0x32) >= *(short*)(e + 0x16) + 0x640) *(short*)(e + 0x10) = 2;
    }

    /// <summary>
    /// Ghidra: <c>0x0046c160</c> — sous-zones 2 et 3. Presente dans <c>Blaze.c</c> ; 77
    /// instructions. <b>Le balancier</b> : son inclinaison <c>+0x34</c> est le sinus d'une phase
    /// qui avance de <c>+0x16</c> par image, divise par <c>+0x14</c> (6 au moins) — ou, un aller
    /// sur deux, par <c>+0x14 - 5</c> : <b>les oscillations alternent</b>, une petite, une grande. Il
    /// sonne (voix <c>0x23</c>) a chaque demi-periode.
    /// <para>
    /// <b>Un <c>+0x14</c> de 6 donne un diviseur de 1 sur l'aller large</b>, et le sinus entier
    /// passe dans l'inclinaison. Le binaire ne borne rien d'autre.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046C160(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
            if (*(short*)(e + 0x14) <= 5) *(short*)(e + 0x14) = 6;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int divisor = *(int*)(e + 0x5c);
        if (divisor == 0) divisor = *(short*)(e + 0x14);

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += *(short*)(e + 0x16);
        *(short*)(e + 0x34) = (short)(sin / divisor);

        if (*(short*)(e + 0x12) > 0x800 && *(short*)(e + 0x98) == 0)
        {
            *(short*)(e + 0x98) = 1;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x23), 0x500, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x12) < 0x1000) return;

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x23), 0x500, 0x1000, 0, 0);
        *(short*)(e + 0x12) -= 0x1000;
        *(short*)(e + 0x98) = 0;

        if (*(int*)(e + 0x5c) != 0)
        {
            *(int*)(e + 0x5c) = 0;
            return;
        }

        *(int*)(e + 0x5c) = *(short*)(e + 0x14) - 5;
    }

    /// <summary>
    /// Ghidra: FUN_00470640 @ 0x00470640 — sous-zone 1. Decompilee dans <c>Blaze.c</c> ;
    /// 80 instructions. <b>La minuterie qui ouvre la chaine</b> du mot <c>0x00571978</c>. A sa
    /// naissance elle prend le bit <c>0x2000</c>, retient son <c>+0x34</c> et sonne (voix
    /// <c>0x24</c>) ; son <c>+0x34</c> tourne alors de <c>0x80</c> par image jusqu'a un quart de
    /// tour et s'y cale. Au bout de <c>+0x16</c> images elle pose, selon son <c>+0x14</c>, DEUX bits
    /// du mot — <c>0x01</c> et <c>0x04</c>, <c>0x08</c> et <c>0x20</c>, ou <c>0x40</c> et
    /// <c>0x100</c> — et prend le bit <c>0x40000000</c>, qui l'endort pour de bon.
    /// <para>
    /// Le premier bit arme le battant <see cref="EntityHandler_00470860"/> et la roue
    /// <see cref="EntityHandler_00470750"/> ; la roue pose le bit qui arme le levier
    /// <see cref="EntityHandler_00470900"/>, et le levier pose... le second bit, que la minuterie a
    /// deja pose elle-meme. Le compteur <c>+0x5c</c> est remis a zero passe 10 000, ce qui ne sert
    /// qu'a une minuterie dont le <c>+0x16</c> depasserait ce nombre.
    /// </para>
    /// </summary>
    private static void EntityHandler_00470640(byte* e)
    {
        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x2000;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x34);
            *(int*)(e + 0x5c) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x24), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        *(int*)(e + 0x5c) += 1;

        if (*(int*)(e + 0x5c) >= *(short*)(e + 0x16))
        {
            uint* word = (uint*)Mem.EntityState(0x00571978);       // Ghidra: DAT_00571978

            switch (*(short*)(e + 0x14))
            {
                case 1: *word |= 0x01; break;
                case 2: *word |= 0x08; break;
                case 3: *word |= 0x40; break;
            }

            switch (*(short*)(e + 0x14))
            {
                case 1: *word |= 0x004; break;
                case 2: *word |= 0x020; break;
                case 3: *word |= 0x100; break;
            }

            *(uint*)e |= 0x40000000;
        }

        // Ghidra: LAB_0047070c
        if (*(int*)(e + 0x5c) >= 10000) *(int*)(e + 0x5c) = 0;

        short rest = *(short*)(e + 0x12);
        *(short*)(e + 0x34) += 0x80;
        if (*(short*)(e + 0x34) >= rest + 0x400) *(short*)(e + 0x34) = (short)(rest + 0x400);
    }

    /// <summary>
    /// Ghidra: <c>0x0046bcd0</c> — sous-zones 2 et 3. Absente de <c>Blaze.c</c> ; 92 instructions.
    /// <b>Le bloc qui culbute d'un palier a l'autre</b>, entre deux hauteurs : <c>+0x14</c> (le
    /// palier haut, retenu en <c>+0x12</c>) et 550 unites plus bas.
    /// <list type="bullet">
    /// <item>1 — a plat (<c>+0x34 = 0</c>) il monte de 10 par image et, passe le palier haut, part en
    /// culbute descendante (2) ; renverse (<c>+0x34 = 0x800</c>) il descend de 10 et, passe le palier
    /// bas, part en culbute montante (3).</item>
    /// <item>2 — il tourne de <c>-0x40</c> par image, sa hauteur suit le palier haut moins 90 fois le
    /// sinus de sa rotation, et il avance de <c>0x6400</c> (6,25 unites) par image en X (cap 0) ou en Z (cap
    /// <c>0xc00</c>) ; a un demi-tour il se pose renverse.</item>
    /// <item>3 — la meme culbute, dans l'autre sens et depuis le palier bas, jusqu'a plat.</item>
    /// </list>
    /// Un cap autre que 0 et <c>0xc00</c> culbute sur place.
    /// </summary>
    private static void EntityHandler_0046BCD0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short roll = *(short*)(e + 0x34);

            if (roll == 0)
            {
                *(int*)(e + 0x20) += 0xa000;
                if (*(int*)(e + 0x20) >> 0xc > *(short*)(e + 0x12)) *(short*)(e + 0x10) = 2;
            }

            if (roll == 0x800)
            {
                *(int*)(e + 0x20) -= 0xa000;
                if (*(int*)(e + 0x20) >> 0xc < *(short*)(e + 0x12) - 0x226) *(short*)(e + 0x10) = 3;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short course = *(short*)(e + 0x32);
            if (course == 0) *(int*)(e + 0x1c) -= 0x6400;
            if (course == 0x0c00) *(int*)(e + 0x24) -= 0x6400;

            int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x34));
            *(short*)(e + 0x34) -= 0x40;
            *(int*)(e + 0x20) = (*(short*)(e + 0x12) << 0xc) - sin * 90;

            if (*(short*)(e + 0x34) <= -0x800)
            {
                *(short*)(e + 0x34) = 0x800;
                *(short*)(e + 0x10) = 1;
            }
        }

        if (*(short*)(e + 0x10) != 3) return;

        short back = *(short*)(e + 0x32);
        if (back == 0) *(int*)(e + 0x1c) += 0x6400;
        if (back == 0x0c00) *(int*)(e + 0x24) += 0x6400;

        int s = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x34));
        *(short*)(e + 0x34) -= 0x40;
        *(int*)(e + 0x20) = ((*(short*)(e + 0x12) - 0x226) << 0xc) - s * 90;

        if (*(short*)(e + 0x34) > 0) return;

        *(short*)(e + 0x34) = 0;
        *(short*)(e + 0x10) = 1;
    }

    // =====================================================================
    // LOT 5 (2026-09-19) — les « B » des blocs 4 a 10
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00470130</c> (sans rien semer) et <c>0x004700c0</c> (semant <c>0x3c</c> pres
    /// de la camera a chaque image) — sous-zones 6 et 7, 24 et 32 instructions. <b>Ce qui tangue sur
    /// place</b> : sa phase <c>+0x32</c> avance de <c>0x44</c> par image (et repasse sous
    /// <c>0x1000</c> quand elle le DEPASSE — <c>0x1000</c> lui-meme est garde une image), et sa
    /// hauteur prend le sinus de la phase decale de <c>+0x14</c> bits (retenu en <c>+0x12</c> a la
    /// naissance). La hauteur n'est jamais rendue : elle derive de la somme des sinus.
    /// </summary>
    private static void TickSwayingFloat_00470130(byte* e, bool seeds)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 0;
        }

        *(short*)(e + 0x32) += 0x44;
        if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) -= 0x1000;

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));
        *(int*)(e + 0x20) += sin << (e[0x12] & 0x1f);

        if (seeds) SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);  // Ghidra: FUN_00468110
    }

    /// <summary>
    /// Ghidra: <c>0x0046e2c0</c> — sous-zones 6 et 7. Absente de <c>Blaze.c</c> ; 46 instructions.
    /// <b>Le grondement lointain</b> : passe 150 images, une chance sur deux cents par image — et
    /// au plus tard a la 251e —, la voix <c>0x19</c> de la zone, hauteur tiree de 500 a 699, gain
    /// de 2 500 a 4 499, sans position. Le compte repart de zero.
    /// </summary>
    private static void EntityHandler_0046E2C0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) += 1;
        bool lucky = (short)(CrtRand() % 0xc8) == 1;
        if (!(lucky && *(short*)(e + 0x12) > 0x96) && *(short*)(e + 0x12) <= 0xfa) return;

        *(short*)(e + 0x12) = 0;
        short pitch = (short)(CrtRand() % 0xc8 + 0x1f4);
        short gain = (short)(CrtRand() % 0x7d0 + 0x9c4);
        Aud.PlaySoundEffect(ZoneSound(0x19), pitch, gain, gain);   // Ghidra: FUN_0053cea0
    }

    /// <summary>
    /// Ghidra: <c>0x0046dfc0</c> (sous-zones 5, 8, 10) et <c>0x00470190</c> (6-7) — absentes de
    /// <c>Blaze.c</c> ; 48 et 53 instructions. <b>La roue qui roule, troisieme forme</b> : celle de
    /// <see cref="EntityHandler_0046C2F0"/>, mais l'amplitude du sinus est un decalage de
    /// <c>+0x14</c> bits (retenu en <c>+0x12</c> a la naissance), et la naissance est la. Les deux
    /// copies different par leurs masques de deplacement — <c>0x8840 / 0xa840</c> et
    /// <c>0x0802 / 0xa842</c>, le premier en sous-zone 7 — et la seconde meurt quand elle porte le bit
    /// <c>0x20000000</c>.
    /// </summary>
    private static void TickShiftedWheel_0046DFC0(byte* e, uint maskInSeven, uint maskElsewhere, bool diesOnFlag)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));
            *(int*)(e + 0x20) += unchecked((int)0xfff9c000) - (sin << (e[0x12] & 0x1f));
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 0;
        }

        MoveSceneEntity_00455B30(e, (byte)s_Unknown_005de52c != 7 ? maskElsewhere : maskInSeven);   // Ghidra: FUN_00455b30

        *(short*)(e + 0x32) += 0x44;
        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));
        *(int*)(e + 0x20) += (lift << (e[0x12] & 0x1f)) + 0x64000;

        TestTriggerVolumes_00534CD0((uint*)e);                      // Ghidra: FUN_00534cd0

        if (diesOnFlag && (*(uint*)e & 0x20000000) != 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0046ce60</c> — sous-zones 5, 8 et 10. Presente dans <c>Blaze.c</c> ;
    /// 49 instructions. <b>Ce qui tourne en ellipse</b> : il retient sa position de naissance comme
    /// centre, et son <c>+0x30</c> comme vitesse angulaire ; il se place ensuite a
    /// <c>+0x14</c> unites en X et <c>+0x16</c> en Z du centre, sur une phase qui part de son cap et
    /// tourne dans les deux sens.
    /// </summary>
    private static void EntityHandler_0046CE60(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x32);
            *(int*)(e + 0x5c) = *(short*)(e + 0x30);
            *(short*)(e + 0x30) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        short phase = *(short*)(e + 0x12);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(phase) * *(short*)(e + 0x14) + *(int*)(e + 0x44);
        int z = (int)Winsys3d.GetCosTableEntry(phase) * *(short*)(e + 0x16) + *(int*)(e + 0x4c);
        *(short*)(e + 0x12) += (short)*(int*)(e + 0x5c);
        *(int*)(e + 0x24) = z;

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
        if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x12) += 0x1000;
    }

    /// <summary>
    /// Ghidra: <c>0x0046e360</c> — sous-zones 6 et 7. Absente de <c>Blaze.c</c> ; 54 instructions.
    /// <b>Ce qui flotte en tournant</b> : a sa naissance il fait naitre l'incantation <c>0xff</c>
    /// 30 unites au-dessus de lui (sa hauteur est pretee, puis rendue) et tire une phase ; ensuite
    /// il tourne de <c>0x64</c> par image et sa hauteur ondule de 5 unites autour de celle de sa
    /// naissance, la phase avancant de <c>0x64</c> aussi.
    /// </summary>
    private static void EntityHandler_0046E360(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(int*)(e + 0x5c) = CrtRand() % 0x1000;
            *(int*)(e + 0x20) += 0x1e000;
            SpawnCastEntity_0047AAE0(e, 0xff, 0, -1);               // Ghidra: FUN_0047aae0
            *(int*)(e + 0x20) -= 0x1e000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        SpinAndBob_0046E3C1(e);
    }

    /// <summary>Ghidra: <c>0x0046e3c1</c> / <c>0x0046e16e</c> — <b>tourner et onduler</b>, le pas par
    /// image commun a <see cref="EntityHandler_0046E360"/> et a <see cref="EntityHandler_0046E100"/> :
    /// cap et phase avancent de <c>0x64</c>, la hauteur ondule de 5 unites autour de <c>+0x12</c>.</summary>
    private static void SpinAndBob_0046E3C1(byte* e)
    {
        *(short*)(e + 0x32) += 0x64;
        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

        int sin = (int)Winsys3d.GetSinTableEntry(*(int*)(e + 0x5c));
        *(int*)(e + 0x20) = sin * 5 + (*(short*)(e + 0x12) << 0xc);

        int phase = *(int*)(e + 0x5c) + 0x64;
        *(int*)(e + 0x5c) = phase;
        if (phase >= 0x1000) *(int*)(e + 0x5c) = phase - 0x1000;
    }

    /// <summary>
    /// Ghidra: <c>0x0046e100</c> — sous-zones 4 et 9. Absente de <c>Blaze.c</c> ; 57 instructions.
    /// <see cref="EntityHandler_0046E360"/> en trois etats au lieu de deux : la naissance tire la
    /// phase (etat 1), l'image suivante fait naitre l'incantation <c>0xff</c> 30 unites plus haut
    /// (etat 2), puis il tourne et ondule.
    /// </summary>
    private static void EntityHandler_0046E100(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x20) += 0x1e000;
            SpawnCastEntity_0047AAE0(e, 0xff, 0, -1);               // Ghidra: FUN_0047aae0
            *(short*)(e + 0x10) = 2;
            *(int*)(e + 0x20) -= 0x1e000;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(int*)(e + 0x5c) = CrtRand() % 0x1000;
        }

        if (*(short*)(e + 0x10) != 2) return;

        SpinAndBob_0046E3C1(e);
    }

    /// <summary>
    /// Ghidra: <c>0x0046e610</c> — sous-zones 6 et 7. Absente de <c>Blaze.c</c> ; 60 instructions.
    /// <b>Le tremplin, troisieme forme</b> : celui de <see cref="EntityHandler_00468950"/>, mais sa
    /// hauteur de repos est retenue en <c>+0x16</c> (et non en <c>+0x9a</c>), et la vitesse qu'il
    /// donne au quart d'onde est fixe, <c>0x14000</c> — celle que le premier ne donne que par
    /// defaut.
    /// </summary>
    private static void EntityHandler_0046E610(byte* e)
    {
        e[7] &= 0xef;
        uint flags = *(uint*)e & 0xffffdfff;
        *(uint*)e = flags;
        *(short*)(e + 0x2a) = 0x1770;

        if ((flags & 0x10) == 0 && *(short*)(e + 0x10) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + *(short*)(e + 0x14) * 0x9c + 0x10) = 1;   // le voisin, reveille
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        if (*(short*)(e + 0x10) != 1) return;

        int rest = *(short*)(e + 0x16);
        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));
        *(short*)(e + 0x12) += 0x100;
        *(int*)(e + 0x20) = (sin + (rest << 4)) << 8;

        short phase = *(short*)(e + 0x12);
        if (phase > 0x800)
        {
            *(short*)(e + 0x10) = 0;
            *(int*)(e + 0x20) = rest << 0xc;
        }

        if (phase != 0x400) return;

        *(int*)(e + 0x54) = 0x14000;
        e[7] |= 0x10;
        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0046ea40</c> — sous-zones 6 et 7. Absente de <c>Blaze.c</c> ; 61 instructions.
    /// <b>Ce qui se deplace quand la camera approche</b>. Il porte en permanence les bits
    /// <c>0x6000</c> de son mot d'etat. Quand la camera (<c>0x005de068</c>) passe a moins de 150
    /// unites — ou une fois parti —, il compte dix images et pose son <c>+0x24</c> a 5 unites, dix
    /// de plus et le pose a 405, puis s'arrete (etat 3). Ce sont des AFFECTATIONS de la coordonnee
    /// Z, pas des deplacements.
    /// </summary>
    private static void EntityHandler_0046EA40(byte* e)
    {
        int* camera = Mem.Camera(0x005de068);
        int dx = (*(int*)(e + 0x1c) >> 0xc) - (camera[0] >> 0xc);
        int dy = (*(int*)(e + 0x20) >> 0xc) - (camera[1] >> 0xc);
        int dz = (*(int*)(e + 0x24) >> 0xc) - (camera[2] >> 0xc);
        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));   // Ghidra: FUN_00545a10

        *(uint*)e |= 0x6000;

        if (distance > 0x96 && *(short*)(e + 0x10) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) >= 0x0a)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(int*)(e + 0x24) = 0x5000;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x12) += 1;
        if (*(short*)(e + 0x12) < 0x0a) return;

        *(short*)(e + 0x10) = 3;
        *(short*)(e + 0x12) = 0;
        *(int*)(e + 0x24) = 0x195000;
    }

    /// <summary>
    /// Ghidra: <c>0x0046dd90</c> — sous-zones 5, 8 et 10. Absente de <c>Blaze.c</c> ; 68
    /// instructions. <b>Le piston</b> : il retient son point de depart (X, Z) et sa course par
    /// image (<c>+0x14</c> vers <c>+0x12</c>), puis s'ecarte du point dans son cap jusqu'a 240
    /// unites, s'arrete deux images, revient jusqu'a zero, s'arrete quinze images, et repart en
    /// ayant inverse sa course (et efface le bit <c>0x2000</c> de son mot d'etat). Sa vitesse
    /// <c>+0x50..+0x58</c> est tenue a zero.
    /// <para>
    /// <b>Les axes sont croises</b> : le sinus du cap va sur Z et le cosinus sur X, a l'inverse des
    /// deplacements ordinaires — la course part donc a angle droit du cap.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046DD90(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = *(short*)(e + 0x14);
            *(short*)(e + 0x14) = 0;
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = 0;

            short reach = *(short*)(e + 0x14);
            short course = *(short*)(e + 0x32);
            *(int*)(e + 0x24) = (int)Winsys3d.GetSinTableEntry(course) * reach + *(int*)(e + 0x4c);
            *(int*)(e + 0x1c) = (int)Winsys3d.GetCosTableEntry(course) * reach + *(int*)(e + 0x44);

            short next = (short)(*(short*)(e + 0x12) + reach);
            *(short*)(e + 0x14) = next;

            if (next > 0xf0)
            {
                *(short*)(e + 0x14) = 0xf0;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x9a) = 2;
            }

            if (*(short*)(e + 0x14) < 0)
            {
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x9a) = 0x0f;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x9a) -= 1;
        if (*(short*)(e + 0x9a) != 0) return;

        *(short*)(e + 0x12) = (short)-*(short*)(e + 0x12);
        *(short*)(e + 0x10) = 1;
        *(uint*)e &= 0xffffdfff;
    }

    /// <summary>
    /// Ghidra: <c>0x0046e1c0</c> — sous-zones 4 et 9. Absente de <c>Blaze.c</c> ; 73 instructions.
    /// <see cref="EntityHandler_0046E100"/> une fois de plus, mais l'incantation nee a la seconde
    /// image est <c>0xfe</c>, a sa hauteur ; et tant que le bit de rang <c>+0x14</c> de la banque 0
    /// n'est PAS pose, il seme <c>0x3b</c> pres de la camera a chaque image.
    /// </summary>
    private static void EntityHandler_0046E1C0(byte* e)
    {
        if (*(short*)(e + 0x10) == 1)
        {
            SpawnCastEntity_0047AAE0(e, 0xfe, 0, -1);               // Ghidra: FUN_0047aae0
            *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(int*)(e + 0x5c) = CrtRand() % 0x1000;
        }

        if (*(short*)(e + 0x10) != 2) return;

        SpinAndBob_0046E3C1(e);

        // Ghidra: le bit se lit en division C (n / 8, n % 8), comme la dalle du lot 2.
        int flag = *(short*)(e + 0x14);
        short cell = (short)(flag / 8);
        if ((Mem.s_StateBank0_005e96e0[cell] & (byte)(1 << ((flag - cell * 8) & 0x1f))) != 0) return;

        SpawnEffectNearCamera_00468110(e, 0x3b, 0, -1);            // Ghidra: FUN_00468110
    }

    /// <summary>
    /// Ghidra: <c>0x0046d640</c> (sous-zones 5, 8, 10 ; gain <c>0x1000</c>) et <c>0x0046f8b0</c>
    /// (6-7 ; gain <c>0x1500</c>) — absentes de <c>Blaze.c</c>, 86 instructions, a cette seule
    /// immediate pres. <b>La herse qui tombe</b>, sur le bit de rang <c>+0x16</c> de la banque 0 :
    /// tant qu'il est eteint, rien. Pose, elle bascule (<c>+0x34</c> descend de 5, puis 7, 9... par
    /// image) jusqu'a <c>+0x14</c> sous sa position retenue, s'y cale en sonnant (voix <c>0x21</c>),
    /// et secoue la camera (<see cref="TryPublishCameraEffectNearEntity"/>) d'une amplitude qui part
    /// de 8, change de signe et decroit d'un a chaque image ; a ±1, un dernier effet a zero, et elle
    /// se fige (etat 3).
    /// </summary>
    private static void TickFallingPortcullis_0046D640(byte* e, short gain)
    {
        int flag = *(short*)(e + 0x16);
        int cell = flag / 8;                                        // Ghidra: division C
        if ((Mem.s_StateBank0_005e96e0[cell] & (byte)(1 << ((flag - cell * 8) & 0x1f))) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 5;
            *(int*)(e + 0x5c) = *(short*)(e + 0x34);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short step = *(short*)(e + 0x12);
            *(short*)(e + 0x34) -= step;
            *(short*)(e + 0x12) = (short)(step + 2);

            short drop = *(short*)(e + 0x14);
            if (*(short*)(e + 0x34) < *(int*)(e + 0x5c) - drop)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 8;
                *(short*)(e + 0x34) = (short)((short)*(int*)(e + 0x5c) - drop);
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x21), 0x400, gain, 0, 0);   // Ghidra: FUN_00468080
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        TryPublishCameraEffectNearEntity((uint*)e, 0x3e7, *(short*)(e + 0x12), 0x3e7, 0, 0);   // Ghidra: FUN_0041be60

        short jolt = (short)-*(short*)(e + 0x12);
        jolt = jolt > 0 ? (short)(jolt - 1) : (short)(jolt + 1);
        *(short*)(e + 0x12) = jolt;
        if (jolt != 1 && jolt != -1) return;

        TryPublishCameraEffectNearEntity((uint*)e, 0x3e7, 0, 0x3e7, 0, 0);
        *(short*)(e + 0x10) = 3;
    }

    /// <summary>
    /// Ghidra: <c>0x0046f070</c> — sous-zones 6 et 7. Absente de <c>Blaze.c</c> ; 88 instructions.
    /// <b>Le bruit qui rode</b> : l'entite porte le bit <c>0x4000</c> ; une image sur cent vingt, elle
    /// se deplace pour un instant a ±300 unites de sa place, cap tire de <c>0xb38</c> a
    /// <c>0xd2b</c>, y fait entendre la voix BRUTE <c>0x1f</c> ou <c>0x22</c> (hauteur 246 a 265,
    /// gain <c>0x2000</c>), y seme <c>0x32</c> pres de la camera, et revient a sa place et a son cap.
    /// <para>
    /// Le tirage qui suit la sauvegarde de la position est jete — un appel de plus au generateur.
    /// Et l'identifiant de la voix est range dans l'emplacement de l'argument sur la pile : sans
    /// effet, le corps ne relit plus son argument.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046F070(byte* e)
    {
        *(uint*)e |= 0x4000;

        if (CrtRand() % 0x78 != 1) return;

        short heading = *(short*)(e + 0x32);
        int x = *(int*)(e + 0x1c);
        int z = *(int*)(e + 0x24);
        int y = *(int*)(e + 0x20);

        _ = CrtRand();                                              // Ghidra: resultat jete
        *(int*)(e + 0x1c) += (CrtRand() % 0x258 - 0x12c) << 0xc;
        *(int*)(e + 0x24) += (CrtRand() % 0x258 - 0x12c) << 0xc;
        *(short*)(e + 0x32) = (short)(CrtRand() % 0x1f4 + 0xb38);

        short voice = (short)((CrtRand() % 2) * 3 + 0x1f);
        short pitch = (short)(CrtRand() % 0x14 + 0xf6);
        TryPlaySoundNearEntity((uint*)e, voice, pitch, 0x2000, 0, 0);   // Ghidra: FUN_00468080

        *(short*)(e + 0x16) = 3;
        SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);            // Ghidra: FUN_00468110

        *(int*)(e + 0x20) = y;
        *(int*)(e + 0x24) = z;
        *(int*)(e + 0x1c) = x;
        *(short*)(e + 0x32) = heading;
    }

    /// <summary>
    /// Ghidra: <c>0x0046e770</c> — sous-zones 6 et 7. Absente de <c>Blaze.c</c> ; 90 instructions.
    /// <b>Le geyser</b>. Rien tant que l'octet <c>0x005de038</c> (la carte courante) n'est pas nul.
    /// La camera a plus de 3 000 unites le remet au repos ; plus pres, il s'eveille et compte :
    /// entre la 31e et la 41e image il fait naitre <c>0xcb</c> (le bouillonnement) ; passe la 50e,
    /// une chance sur cent par image — et au plus tard passe la 100e — il jaillit : 10 a 14 images
    /// de <c>0xca</c>, puis retour au repos.
    /// </summary>
    private static void EntityHandler_0046E770(byte* e)
    {
        if (*(byte*)Mem.Camera(0x005de038) != 0) return;

        int* camera = Mem.Camera(0x005de068);
        int dx = (camera[0] >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(e + 0x24) >> 0xc);
        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));   // Ghidra: FUN_00545a10

        if (distance >= 0xbb8)
        {
            *(short*)(e + 0x10) = 0;
        }
        else if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;
            short age = *(short*)(e + 0x12);
            if (age > 0x1e && age < 0x2a) SpawnEffectEntity(e, 0xcb, 0, -1);   // Ghidra: FUN_0047a9d0

            short roll = (short)(CrtRand() % 100);
            age = *(short*)(e + 0x12);
            if (age > 100 || (age > 0x32 && roll == 1))
            {
                int r = CrtRand();
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = (short)(r % 5 + 10);
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x12) -= 1;
        if (*(short*)(e + 0x12) >= 0)
        {
            SpawnEffectEntity(e, 0xca, 0, -1);                      // Ghidra: FUN_0047a9d0
            return;
        }

        *(short*)(e + 0x10) = 0;
    }

    // =====================================================================
    // LOT 7 (2026-09-20) — LE DECOR « C » DES BLOCS 0 / 2-3 / 5-8-10
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0046be10 @ 0x0046be10 — sous-zones 2 et 3. Decompilee dans <c>Blaze.c</c> ;
    /// 108 instructions.
    ///
    /// <para>
    /// <b>Le pendule qui decrit une ellipse.</b> A sa premiere image il retient sa position en
    /// <c>+0x12</c>, <c>+0x14</c>, <c>+0x16</c> (en unites entieres) et choisit un etat selon son
    /// cap : 1 si <c>+0x32</c> vaut <c>0</c> ou <c>0x800</c>, 2 autrement. <b>Les deux etats ont le
    /// meme corps, instruction pour instruction</b> — le choix ne change rien, et c'est releve tel
    /// quel.
    /// </para>
    /// <para>
    /// Chaque image le cap avance de <c>0xc</c> et se replie sous <c>0x1000</c> ; X et Z tournent
    /// sur un rayon de <c>0x2b2</c> et Y suit le sinus du <b>double</b> du cap sur <c>200</c> — une
    /// ellipse parcourue deux fois plus vite en hauteur qu'en largeur.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046BE10(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = (short)(*(short*)(e + 0x32) == 0 || *(short*)(e + 0x32) == 0x800 ? 1 : 2);
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x1c) >> 0xc);
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x24) >> 0xc);
        }

        // Les deux etats sont le meme corps : le binaire l'ecrit deux fois.
        if (*(short*)(e + 0x10) != 1 && *(short*)(e + 0x10) != 2) return;

        SwingPendulumStep(e);
    }

    /// <summary>Un pas du pendule de <see cref="EntityHandler_0046BE10"/>. Le binaire recopie ce
    /// bloc pour l'etat 1 et pour l'etat 2 sans une immediate de difference.</summary>
    private static void SwingPendulumStep(byte* e)
    {
        short course = *(short*)(e + 0x32);

        *(int*)(e + 0x1c) = *(short*)(e + 0x12) * 0x1000 + (int)Winsys3d.GetSinTableEntry(course) * 0x2b2;
        *(int*)(e + 0x24) = *(short*)(e + 0x16) * 0x1000 + Winsys3d.GetCosTableEntry(course) * 0x2b2;

        int lift = (int)Winsys3d.GetSinTableEntry((short)(course << 1));
        *(short*)(e + 0x32) += 0xc;
        *(int*)(e + 0x20) = *(short*)(e + 0x14) * 0x1000 + lift * 200;

        if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) -= 0x1000;
    }

    /// <summary>
    /// Ghidra: FUN_0046c3b0 @ 0x0046c3b0 — sous-zones 2 et 3. Decompilee dans <c>Blaze.c</c> ;
    /// 117 instructions.
    ///
    /// <para>
    /// <b>L'eboulement qui secoue.</b> A la naissance, la voix <c>0x22</c> de la zone a la hauteur
    /// <c>0x400</c>. Les <b>huit premieres images</b> il tombe de <c>0x7</c> unites, derive de
    /// <c>10</c> sur son cap avec un tremblement de <c>-2</c> a <c>+2</c> unites en Z et de
    /// <c>-4</c> a <c>0</c> plus <c>2</c> en X, et publie une secousse de camera de <c>-3</c> a
    /// <c>+3</c> (<see cref="TryPublishCameraEffectNearEntity"/>, portee <c>999</c>).
    /// </para>
    /// <para>
    /// Passe la huitieme, la secousse retombe a zero, le cap gagne 3 et la rotation de face
    /// <c>+0x34</c> perd <c>0x23</c> par image ; la chute s'accelere — <c>0x7000</c> plus
    /// <c>0x800</c> par image paire depuis la huitieme (<c>(+0x12 - 8) &amp; 0x1ffffe</c>) — et la
    /// derive tombe a 9. <b>A la cent-vingtieme image l'emplacement se vide.</b>
    /// </para>
    /// <para>
    /// <b>Ghidra rate deux signatures ici</b>, et de la meme facon : <c>FUN_00547140(999,0,0)</c> et
    /// <c>FUN_0041be60(param_1,999,0,999,0,0)</c>. Le tirage est <c>rand()</c> sans argument, et la
    /// secousse prend six arguments dont Ghidra n'ecrit qu'une partie — corrige sur les sites
    /// d'appel deja transcrits.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046C3B0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x22), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        *(short*)(e + 0x12) += 1;

        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x12) < 8)
            {
                *(int*)(e + 0x20) -= 0x7000;
                *(int*)(e + 0x24) += (CrtRand() % 5 - 2) * 0x1000
                                     + (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 10;
                *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * -10
                                     + 0x2000 + (CrtRand() % 5) * -0x1000;
                TryPublishCameraEffectNearEntity((uint*)e, 999, (short)(CrtRand() % 7 - 3), 999, 0, 0);   // Ghidra: FUN_0041be60
            }

            if (*(short*)(e + 0x12) > 7)
            {
                TryPublishCameraEffectNearEntity((uint*)e, 999, 0, 999, 0, 0);        // Ghidra: FUN_0041be60
                *(short*)(e + 0x32) += 3;
                *(short*)(e + 0x34) -= 0x23;
                *(int*)(e + 0x20) += (int)((uint)(*(short*)(e + 0x12) - 8) & 0x1ffffe) * -0x800 - 0x7000;
                *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 9;
                *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * -9;
            }
        }

        if (*(short*)(e + 0x12) > 0x77) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0046c690</c> — sous-zones 5, 8 et 10. <b>Absente de <c>Blaze.c</c></b> ;
    /// 107 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0046C690.asm</c>.
    ///
    /// <para>
    /// <b>La porte qui suit son drapeau.</b> A sa premiere image elle range son cap dans
    /// <c>+0x16</c>, son numero de drapeau dans <c>+0x14</c> (depuis le <c>+0x30</c> que la
    /// sous-zone a pose), et remet <c>+0x30</c> a zero. Le cap vise <c>+0x12</c> vaut le cap range,
    /// plus un demi-tour si le bit <c>+0x14</c> de <c>0x005e96e0</c> est mis : <b>elle s'ouvre
    /// quand le drapeau tombe et se referme quand il se leve</b>, ou l'inverse selon la pose. Son
    /// cap reel rejoint le cap vise d'un seizieme par image.
    /// </para>
    /// <para>
    /// <b>Toutes les dix images elle grince</b> — voix <c>0x3b</c> de la zone, hauteur <c>0x400</c>,
    /// et un gain qui depend de <b>trois</b> choses : le drapeau, le cote de la camera
    /// (<c>0x005de068</c> contre son X), et le numero du drapeau — <c>0x112</c> inverse le sens de
    /// la comparaison. Le gain vaut <c>0x800</c> ou <c>0x2000</c> selon le cas, et <b>tombe a
    /// <c>0x1400</c> des que la camera monte au-dessus de <c>0xbe000</c></b>, quel que soit le
    /// reste.
    /// </para>
    /// <para>
    /// Enfin elle fait naitre a chaque image l'entite d'incantation <c>0xfc</c> si son drapeau est
    /// <c>0x112</c>, <c>0xfb</c> sinon.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046C690(byte* e)
    {
        short entryState = *(short*)(e + 0x10);

        if (entryState == 0)
        {
            *(short*)(e + 0x16) = *(short*)(e + 0x32);
            *(short*)(e + 0x14) = *(short*)(e + 0x30);
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x98) = 0;
        }

        short flag = *(short*)(e + 0x14);
        int cell = (short)((flag + (flag >> 0x1f & 7)) >> 3);
        byte bit = (byte)(1 << ((flag - cell * 8) & 0x1f));
        bool raised = (Mem.s_StateBank0_005e96e0[cell] & bit) != 0;

        short target = *(short*)(e + 0x16);
        if (raised) target += 0x800;
        *(short*)(e + 0x12) = target;

        if (entryState == 0)
        {
            *(short*)(e + 0x32) = target;
            *(short*)(e + 0x10) = 1;
        }

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 0x0a)
        {
            // Le cote de la camera, et le sens de la comparaison que le drapeau 0x112 inverse.
            bool farSide = flag == 0x112
                ? *Mem.Camera(0x005de068) > *(int*)(e + 0x1c)
                : *Mem.Camera(0x005de068) <= *(int*)(e + 0x1c);

            short gain = farSide
                ? (short)(raised ? 0x0800 : 0x2000)
                : (short)(raised ? 0x2000 : 0x0800);

            if (*Mem.Camera(0x005de06c) >= 0xbe000) gain = 0x1400;

            Aud.PlaySoundEffect(ZoneSound(0x3b), 0x400, gain, gain);   // Ghidra: FUN_0053cea0
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        short course = *(short*)(e + 0x32);
        *(short*)(e + 0x32) = (short)(((*(short*)(e + 0x12) - course) >> 4) + course);

        SpawnCastEntity_0047AAE0(e, (byte)(*(short*)(e + 0x14) == 0x112 ? 0xfc : 0xfb), 0, -1);   // Ghidra: FUN_0047aae0
    }

    /// <summary>
    /// Ghidra: <c>0x0046a810</c> — sous-zones 2 et 3. <b>Absente de <c>Blaze.c</c></b> ;
    /// 148 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0046A810.asm</c>. Sa jumelle
    /// <c>0x0046a9e0</c> (2-3) et la copie de celle-ci, <c>0x0046ca80</c> (5-8-10), n'ajoutent
    /// <b>qu'une instruction</b> : la naissance des grains se fait <c>0x82</c> unites plus haut.
    ///
    /// <para>
    /// <b>Le lien qui tient tant que son voisin reste pres.</b> Le champ <c>+0x14</c> designe un
    /// <b>voisin dans la meme table d'entites</b> — <c>e + 0x9c * n</c>, le pas d'un
    /// enregistrement —, et <c>+0x16</c> le numero de drapeau, range en <c>+0x5c</c> a la premiere
    /// image. La portee est <c>+0x30</c> si la sous-zone en a pose une, <c>300</c> sinon.
    /// </para>
    /// <para>
    /// Tant que le voisin est <b>plus pres</b> que la portee : six grains par image
    /// (<see cref="SpawnEffectNearCamera_00468110"/>, sous-type <c>0x30</c>), le drapeau est
    /// <b>abaisse</b>, et toutes les treize images la voix <c>0x37</c> de la zone. Des qu'il
    /// s'eloigne, le drapeau est <b>leve</b> — et rien d'autre. L'etat 2 remet <c>+0x30</c> a zero
    /// et ne fait rien de plus : c'est l'arret.
    /// </para>
    /// <para>
    /// <b>Le corps se sert de <c>+0x50</c>..<c>+0x58</c> comme d'un brouillon</b> : il y recopie la
    /// position du voisin pour calculer la distance, puis restaure les trois mots — et
    /// <c>+0x20</c>, qu'il n'a pourtant modifie que dans la variante haute. Releve tel quel.
    /// </para>
    /// </summary>
    private static void TickNeighbourTether(byte* e, int spawnLift, bool guardsOnHeight = false)
    {
        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x30) = 0;
            return;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x5c) = *(short*)(e + 0x16);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x9a) = 0;

            if (*(short*)(e + 0x30) != 0)
            {
                *(short*)(e + 0x98) = *(short*)(e + 0x30);
                *(short*)(e + 0x30) = 0;
            }
            else
            {
                *(short*)(e + 0x98) = 0x12c;
            }
        }

        int savedX = *(int*)(e + 0x50);
        int savedY = *(int*)(e + 0x54);
        int savedZ = *(int*)(e + 0x58);
        int savedHeight = *(int*)(e + 0x20);

        byte* neighbour = e + *(short*)(e + 0x14) * 0x9c;
        *(int*)(e + 0x50) = *(int*)(neighbour + 0x1c);
        *(int*)(e + 0x54) = *(int*)(neighbour + 0x20);
        *(int*)(e + 0x58) = *(int*)(neighbour + 0x24);

        int dx = (*(int*)(e + 0x50) >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (*(int*)(e + 0x54) >> 0xc) - (savedHeight >> 0xc);
        int dz = (*(int*)(e + 0x58) >> 0xc) - (*(int*)(e + 0x24) >> 0xc);
        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        int flag = *(int*)(e + 0x5c);
        int cell = (short)((flag + (flag >> 0x1f & 7)) >> 3);

        if (distance < *(short*)(e + 0x98))
        {
            *(int*)(e + 0x20) += spawnLift;

            // La garde de 0x0046e410 : dans la salle 7, un voisin bas fait taire la gerbe.
            bool sows = !guardsOnHeight
                        || *(int*)(neighbour + 0x20) > 0x258000
                        || (s_Unknown_005de52c & 0xff) != 7;        // Ghidra: DAT_005de52c

            if (sows)
            {
                for (short grain = 1; grain <= 6; grain++)
                {
                    *(short*)(e + 0x12) = grain;
                    *(short*)(e + 0x16) = (short)(CrtRand() % 3);
                    SpawnEffectNearCamera_00468110(e, 0x30, 0x10, -1);   // Ghidra: FUN_00468110
                }
            }

            if (flag != 0)
            {
                Mem.s_StateBank0_005e96e0[cell] &= (byte)~(1 << ((flag - cell * 8) & 0x1f));
            }

            *(short*)(e + 0x9a) += 1;
            if (*(short*)(e + 0x9a) == 0x0d)
            {
                *(short*)(e + 0x9a) = 0;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x37), 0x200, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            }
        }
        else if (flag != 0)
        {
            Mem.s_StateBank0_005e96e0[cell] |= (byte)(1 << ((flag - cell * 8) & 0x1f));
        }

        *(int*)(e + 0x20) = savedHeight;
        *(int*)(e + 0x50) = savedX;
        *(int*)(e + 0x54) = savedY;
        *(int*)(e + 0x58) = savedZ;
    }

    private static void EntityHandler_0046A810(byte* e) => TickNeighbourTether(e, 0);
    private static void EntityHandler_0046A9E0(byte* e) => TickNeighbourTether(e, 0x82000);
    private static void EntityHandler_0046CA80(byte* e) => TickNeighbourTether(e, 0x82000);

    /// <summary>
    /// Ghidra: <c>0x0046cc60</c> — sous-zones 5, 8 et 10. <b>Absente de <c>Blaze.c</c></b> ;
    /// 159 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0046CC60.asm</c>.
    ///
    /// <para>
    /// <b>Le nid qui lache ses betes quand on approche.</b> Rien ne se passe tant qu'aucun membre
    /// vivant n'est a moins de <c>0x4b0</c> unites <b>en plan</b> — la hauteur n'entre pas dans la
    /// distance.
    /// </para>
    /// <para>
    /// Quand quelqu'un est la, il sauve sa position et son cap, les <b>brouille</b> — cap decale de
    /// <c>-200</c> a <c>+199</c>, X et Z de <c>-50</c> a <c>+49</c> unites, hauteur de
    /// <c>0x82</c> —, fait naitre le sous-type <c>0x35</c>
    /// (<see cref="SpawnInheritedEffectNearCamera_0045C000"/>), puis <b>remet tout en place</b> :
    /// le nid ne bouge pas, seul ce qui en sort part d'ailleurs.
    /// </para>
    /// <para>
    /// <b>La cadence depend de la place restante.</b> Un compteur <c>+0x5c</c> tourne de 0 a 999 ;
    /// la naissance a lieu quand il est multiple de <b>10</b>, ou de <b>25</b> si la table d'effets
    /// <c>0x005f2d40</c> porte deja <c>0x14</c> entites vivantes ou plus. Et toutes les
    /// <c>0x15</c> images, le souffle <c>0x20</c> — <b>identifiant brut, pas voix de zone</b>.
    /// </para>
    /// <para>
    /// <b>La montee de <c>0x82</c> est ecrite d'une facon qui merite d'etre dite</b> :
    /// <c>0x82 &lt;&lt; ((((rand() % 100) &lt;&lt; 12) - 0x31ff4) &amp; 0x1f)</c>. Les douze bits
    /// bas du terme de gauche etant nuls, le decalage vaut <b>toujours 12</b>, et l'addition
    /// toujours <c>0x82000</c> — la meme que celle de <see cref="TickNeighbourTether"/>. Transcrit
    /// tel quel : c'est ce que le binaire calcule.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046CC60(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x98) = (short)(CrtRand() % 0x14);
        }

        // Combien d'entites d'effet vivent deja.
        int busy = 0;
        byte* effect = Mem.Entity(0x005f2d40);
        for (int i = 0x80; i != 0; i--, effect += 0x9c)
        {
            if ((*(uint*)effect & 0x80000000) != 0) busy++;
        }

        bool near = false;
        byte* member = Inp.s_SlotFlags_005e93c0;                    // 0x005e93c0

        for (int i = 4; i != 0; i--, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) < 0x4b0) near = true;
        }

        if (!near) return;

        short savedCourse = *(short*)(e + 0x32);
        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);

        *(short*)(e + 0x32) += (short)(CrtRand() % 0x190 - 0xc8);

        // 0x82 << ((((rand % 100) << 12) - 0x31ff4) & 0x1f) — le decalage vaut toujours 12.
        int shift = (((CrtRand() % 0x64) << 0xc) - 0x31ff4) & 0x1f;
        *(int*)(e + 0x20) += 0x82 << shift;

        *(int*)(e + 0x1c) += (CrtRand() % 0x64 - 0x32) << 0xc;
        *(int*)(e + 0x24) += (CrtRand() % 0x64 - 0x32) << 0xc;

        int tick = *(int*)(e + 0x5c) + 1;
        *(int*)(e + 0x5c) = tick;
        if (tick >= 0x3e8) *(int*)(e + 0x5c) = 0;

        tick = *(int*)(e + 0x5c);
        int period = busy < 0x14 ? 10 : 25;

        if ((short)(tick / period * period) == tick)
        {
            *(short*)(e + 0x9a) = 0;
            SpawnInheritedEffectNearCamera_0045C000(e, 0x35, 0, -1);   // Ghidra: FUN_0045c000
        }

        *(short*)(e + 0x98) += 1;

        *(int*)(e + 0x1c) = savedX;
        *(int*)(e + 0x20) = savedY;
        *(int*)(e + 0x24) = savedZ;
        *(short*)(e + 0x32) = savedCourse;

        if (*(short*)(e + 0x98) <= 0x14) return;

        *(short*)(e + 0x98) = 0;
        TryPlaySoundNearEntity((uint*)e, 0x20, 0x400, 0xa00, 0, 0);   // Ghidra: FUN_00468080
    }

    /// <summary>
    /// Ghidra: <c>0x0046ba50</c> (sous-zones 2 et 3) et <c>0x0046fe40</c> (6 et 7). <b>Absentes de
    /// <c>Blaze.c</c></b> ; 193 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0046BA50.asm</c>.
    /// Les deux corps n'ont aucune immediate differente.
    ///
    /// <para>
    /// <b>La chose qui erre et rebondit sur les murs.</b> A chaque image elle vide sa vitesse
    /// (<see cref="ClearEntityVelocity_00460FC0"/>), <b>sauve tout ce qu'un choc pourrait abimer</b>
    /// — position, appui <c>+0x60</c>/<c>+0x64</c>, face <c>+0x7c</c>, contacts <c>+0x80</c> a
    /// <c>+0x90</c> —, puis avance de <c>+0x12</c> et <c>+0x14</c> unites, tirees entre 5 et 17 et
    /// signees au hasard, et tourne de <c>+0x16</c> fois <c>0x80</c> avec repli entre <c>0</c> et
    /// <c>0x1000</c>.
    /// </para>
    /// <para>
    /// Si <see cref="FinalizeEntityPlacement2"/> rapporte un contact <c>0x880</c> ou un octet
    /// <c>+0x0f</c> de face non nul, <b>tout est remis comme avant</b> — sauf la hauteur
    /// <c>+0x48</c>, sauvee mais jamais rendue —, un nouveau cap est tire et le sens de rotation
    /// s'inverse. Elle grogne toutes les dix images (voix <c>0x12</c> de la zone) et frappe les
    /// membres que son octet de contact designe (<see cref="StrikePartyMembers_0045DA40"/>,
    /// puissance <c>0x0a</c>).
    /// </para>
    /// </summary>
    private static void TickWallBouncingRoamer(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);                            // Ghidra: FUN_00460fc0

        int savedGroundX = *(int*)(e + 0x60);
        int savedGroundZ = *(int*)(e + 0x64);
        byte* savedFace = *(byte**)(e + 0x7c);
        uint savedContacts = *(ushort*)(e + 0x90);
        int saved80 = *(int*)(e + 0x80);
        int saved84 = *(int*)(e + 0x84);
        int saved88 = *(int*)(e + 0x88);
        int saved8c = *(int*)(e + 0x8c);

        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(RandomSign() * (CrtRand() % 0x0d + 5));
            *(short*)(e + 0x14) = (short)(RandomSign() * (CrtRand() % 0x0d + 5));
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x16) = (short)RandomSign();
        }

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 0x0a)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x12), 0x100, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 0;
        }

        int stepX = *(short*)(e + 0x12) << 0xc;
        int stepZ = *(short*)(e + 0x14) << 0xc;
        *(int*)(e + 0x1c) += stepX;
        *(int*)(e + 0x24) += stepZ;
        AddEntityVelocity_00454580(e, stepX, 0, stepZ);             // Ghidra: FUN_00454580

        *(short*)(e + 0x32) += (short)(*(short*)(e + 0x16) << 7);
        if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) = 0;
        if (*(short*)(e + 0x32) < 0) *(short*)(e + 0x32) = 0x1000;

        FinalizeEntityPlacement2((uint*)e);                         // Ghidra: FUN_00533940

        bool blocked = (*(uint*)(e + 0x90) & 0x880) != 0 || (*(byte**)(e + 0x7c))[0x0f] != 0;

        if (blocked)
        {
            *(int*)(e + 0x60) = savedGroundX;
            *(int*)(e + 0x64) = savedGroundZ;
            *(int*)(e + 0x1c) = *(int*)(e + 0x44);
            *(int*)(e + 0x24) = *(int*)(e + 0x4c);
            *(int*)(e + 0x80) = saved80;
            *(int*)(e + 0x84) = saved84;
            *(int*)(e + 0x88) = saved88;
            *(int*)(e + 0x8c) = saved8c;
            *(byte**)(e + 0x7c) = savedFace;
            *(uint*)(e + 0x90) = savedContacts & 0xffff;

            *(short*)(e + 0x12) = (short)(RandomSign() * (CrtRand() % 0x0d + 5));
            *(short*)(e + 0x14) = (short)(RandomSign() * (CrtRand() % 0x0d + 5));
            *(short*)(e + 0x16) = (short)-*(short*)(e + 0x16);
        }

        StrikePartyMembers_0045DA40(e, 0x0a, 0, 0, 0);              // Ghidra: FUN_0045da40
    }

    private static void EntityHandler_0046BA50(byte* e) => TickWallBouncingRoamer(e);
    private static void EntityHandler_0046FE40(byte* e) => TickWallBouncingRoamer(e);

    /// <summary>Le signe tire au hasard des deux errants et du rocher :
    /// <c>(rand() &amp; 1) * 2 - 1</c>, avec la valeur absolue du binaire
    /// (<c>cdq ; xor ; sub ; and 1 ; xor ; sub</c>), soit <c>-1</c> ou <c>+1</c>.</summary>
    private static int RandomSign() => SignedMask(CrtRand(), 1) * 2 - 1;

    /// <summary>
    /// Ghidra: <c>0x00468e30</c> — sous-zone 0. <b>Absente de <c>Blaze.c</c></b> ; 217 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00468E30.asm</c>.
    ///
    /// <para>
    /// <b>Le rocher qui roule d'un mur a l'autre.</b> Meme charpente que
    /// <see cref="TickWallBouncingRoamer"/> — sauvegarde complete, avance, contact, restauration —
    /// mais il n'avance que sur <b>un axe</b>, le Z, de <c>+0x14</c> unites tirees entre 10 et 19,
    /// et il <b>tourne</b> : <c>+0x32</c> gagne <c>+0x16</c> fois <c>0xf0</c> par image. Il porte le
    /// bit <c>0x2000</c> des sa premiere image.
    /// </para>
    /// <para>
    /// Il gronde toutes les neuf images (voix <c>0x2d</c> de la zone). Au choc, tout est remis en
    /// place, <c>+0x14</c> change de signe, la voix <c>0x30</c> claque et <b>huit bouffees</b>
    /// <c>0x34</c> naissent ; une fois sur sept une <c>0x35</c> s'y ajoute — <b>ce dernier tirage a
    /// lieu meme sans choc</b>, le binaire le place apres la fusion des deux chemins.
    /// </para>
    /// <para>
    /// <b>Son octet de contact <c>+0x09</c> est recalcule a chaque image</b> : un bit par membre
    /// vivant a moins de <c>0x3c</c> unites en plan et dont l'ecart de hauteur, releve de
    /// <c>0x46</c> et pris en valeur absolue, tient sous <c>0x50</c>. C'est ce masque que
    /// <see cref="StrikePartyMembers_0045DA40"/> lit pour frapper a <c>0x0a</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00468E30(byte* e)
    {
        int savedGroundX = *(int*)(e + 0x60);
        int savedGroundZ = *(int*)(e + 0x64);
        byte* savedFace = *(byte**)(e + 0x7c);
        uint savedContacts = *(ushort*)(e + 0x90);
        int saved80 = *(int*)(e + 0x80);
        int saved84 = *(int*)(e + 0x84);
        int saved88 = *(int*)(e + 0x88);
        int saved8c = *(int*)(e + 0x8c);

        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = (short)(RandomSign() * (CrtRand() % 0x0a + 0x0a));
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x16) = (short)RandomSign();
            *(uint*)e |= 0x2000;
        }

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 9)
        {
            *(short*)(e + 0x98) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2d), 0x400, 0x800, 0, 0);   // Ghidra: FUN_00468080
        }

        *(short*)(e + 0x32) += (short)(*(short*)(e + 0x16) * 0xf0);
        *(int*)(e + 0x24) += *(short*)(e + 0x14) << 0xc;

        if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) = 0;
        if (*(short*)(e + 0x32) < 0) *(short*)(e + 0x32) = 0x1000;

        FinalizeEntityPlacement2((uint*)e);                         // Ghidra: FUN_00533940

        bool blocked = (*(uint*)(e + 0x90) & 0x880) != 0 || (*(byte**)(e + 0x7c))[0x0f] != 0;

        if (blocked)
        {
            *(int*)(e + 0x1c) = *(int*)(e + 0x44);
            *(int*)(e + 0x60) = savedGroundX;
            *(int*)(e + 0x64) = savedGroundZ;
            *(int*)(e + 0x84) = saved84;
            *(int*)(e + 0x24) = *(int*)(e + 0x4c);
            *(int*)(e + 0x80) = saved80;
            *(byte**)(e + 0x7c) = savedFace;
            *(uint*)(e + 0x90) = savedContacts & 0xffff;
            *(int*)(e + 0x88) = saved88;
            *(int*)(e + 0x8c) = saved8c;
            *(short*)(e + 0x14) = (short)-*(short*)(e + 0x14);

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x30), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            for (int i = 8; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x34, 0, -1);   // Ghidra: FUN_00468110
        }

        if (CrtRand() % 7 == 1) SpawnEffectNearCamera_00468110(e, 0x35, 0, -1);   // Ghidra: FUN_00468110

        // ------------------------------------------------- l'octet de contact, refait chaque image
        e[9] = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;                    // 0x005e93c0

        for (int slot = 0; slot < 4; slot++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(member + 0x1c) >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
            int dz = (*(int*)(member + 0x24) >> 0xc) - (*(int*)(e + 0x24) >> 0xc);
            int plan = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            int dy = (*(int*)(member + 0x20) >> 0xc) - (*(int*)(e + 0x20) >> 0xc) + 0x46;
            if (dy < 0) dy = -dy;

            if (plan >= 0x3c || dy >= 0x50) continue;

            e[9] |= (byte)(1 << slot);
        }

        StrikePartyMembers_0045DA40(e, 0x0a, 0, 0, 0);              // Ghidra: FUN_0045da40
    }

    /// <summary>
    /// Ghidra: FUN_0046dbd0 @ 0x0046dbd0 — sous-zones 5, 8 et 10. Decompilee dans <c>Blaze.c</c> ;
    /// 139 instructions.
    ///
    /// <para>
    /// <b>Le piston qui sort et rentre.</b> A sa premiere image il retient sa position en unites
    /// entieres (<c>+0x44</c>, <c>+0x48</c>, <c>+0x4c</c>) et arme sa phase. Chaque image la phase
    /// <c>+0x12</c> avance de <c>+0x16</c> et se replie sous <c>0x1000</c> ; le piston se deplace
    /// sur son cap <c>+0x32</c> de la <b>valeur absolue</b> de <c>sin(phase) * +0x14</c> — il ne
    /// sort donc que d'un cote, deux fois par tour.
    /// </para>
    /// <para>
    /// <b>Il frappe deux fois par tour</b>, a la phase <c>0x800</c>..<c>0x9c4</c> et au passage de
    /// <c>0x1000</c> : la voix <c>0x2a</c> de la zone et <see cref="StrikePartyMembers_0045DA40"/>
    /// a <c>10</c>. Le verrou <c>+0x98</c> interdit le doublon, et se rearme sous <c>0x400</c> ou
    /// entre <c>0xbb8</c> et <c>0xdac</c>.
    /// </para>
    /// <para>
    /// <b>Les deux frappes sont conditionnees par le cap</b> : rien ne part si <c>+0x32</c> depasse
    /// <c>0x800</c>. Le verrou, lui, se pose <b>dans tous les cas</b> — un piston tourne vers
    /// l'arriere avance donc en silence, sans jamais blesser. La hauteur retenue <c>+0x48</c> n'est
    /// jamais relue : seuls X et Z bougent.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046DBD0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x44) = *(int*)(e + 0x1c) >> 0xc;
            *(int*)(e + 0x48) = *(int*)(e + 0x20) >> 0xc;
            *(int*)(e + 0x4c) = *(int*)(e + 0x24) >> 0xc;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) += *(short*)(e + 0x16);

        if (*(short*)(e + 0x12) > 0x800 && *(short*)(e + 0x12) < 0x9c4 && *(short*)(e + 0x98) == 0)
        {
            PistonStrike(e);
            *(short*)(e + 0x98) = 1;
        }

        if (*(short*)(e + 0x12) > 0x1000 && *(short*)(e + 0x98) == 0)
        {
            PistonStrike(e);
            *(short*)(e + 0x98) = 1;
        }

        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;

        short phase = *(short*)(e + 0x12);
        if (phase < 0x400) *(short*)(e + 0x98) = 0;
        if (phase > 3000 && phase < 0xdac) *(short*)(e + 0x98) = 0;

        int reach = (int)Winsys3d.GetSinTableEntry(phase) * *(short*)(e + 0x14);   // Ghidra: Ordinal_451
        reach = reach < 0 ? -(reach >> 0xc) : reach >> 0xc;

        short course = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * reach + *(int*)(e + 0x44) * 0x1000;
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * reach + *(int*)(e + 0x4c) * 0x1000;
    }

    /// <summary>La frappe du piston, ecrite deux fois dans le binaire : la voix <c>0x2a</c> et le
    /// coup a <c>10</c>, <b>tous deux sous la meme condition de cap</b>.</summary>
    private static void PistonStrike(byte* e)
    {
        if (*(short*)(e + 0x32) >= 0x801) return;

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2a), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);                              // Ghidra: FUN_0045da40
    }

    // =====================================================================
    // LOT 8 (2026-09-20) — LE DECOR « C » DES BLOCS 4-9 ET 6-7
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00439bd0 @ 0x00439bd0 — sous-zones 6 et 7. Decompilee dans <c>Blaze.c</c> ;
    /// 104 instructions.
    ///
    /// <para>
    /// <b>Le souffle qui suit son porteur.</b> Il se colle a chaque image sur le <c>+0x6c</c> qui
    /// l'a fait naitre — position, plus <c>0x50</c> de hauteur, et son cap —, et range ce porteur
    /// dans son <c>+0x74</c> pour que ce qu'il seme en herite. Sans porteur, il suit l'entite par
    /// defaut <c>0x005de558</c>.
    /// </para>
    /// <para>
    /// <b>Sa voix depend de l'etat du porteur.</b> A la premiere image, le <c>+0x16</c> du porteur
    /// valant 0 ou 1 lui donne le timbre 2, sinon le timbre 1 ; le souffle part alors sur
    /// <c>0x1f</c> (identifiant brut, hauteur <c>0x6fd</c>). Ensuite, une image sur cinq, le timbre
    /// choisi sonne — <c>0x23</c> pour le timbre 1, <c>0x20</c> pour le timbre 2.
    /// </para>
    /// <para>
    /// <b>Il meurt avec son porteur</b> : l'octet <c>+0x43</c> du porteur a 1, ou le porteur marque
    /// <c>0x40000000</c>, ou eteint. Sinon il avance de <c>0xb4</c> unites sur son cap, descend de
    /// <c>0xa000</c>, et fait naitre <c>0xdf</c> a sa nouvelle position — a chaque image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00439BD0(byte* e)
    {
        byte* raw = *(byte**)(e + 0x6c);
        byte* carrier = raw != null ? raw : Res.s_EntityList_005de558;   // Ghidra: &DAT_005de558

        *(byte**)(e + 0x74) = raw;

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
        *(short*)(e + 0x32) = *(short*)(carrier + 0x32);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x9a) = (short)(*(short*)(carrier + 0x16) == 0 || *(short*)(carrier + 0x16) == 1 ? 2 : 1);
            *(short*)(e + 0x10) = 1;
            TryPlaySoundNearEntity((uint*)e, 0x1f, 0x6fd, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x98) == 1)
        {
            short voice = (short)(*(short*)(e + 0x9a) == 1 ? 0x23 : 0x20);
            TryPlaySoundNearEntity((uint*)e, voice, 0x1000, 0x800, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x98) == 5) *(short*)(e + 0x98) = 0;
        *(short*)(e + 0x98) += 1;

        if (carrier[0x43] == 1) *(uint*)e &= 0x7fffffff;
        if ((*(uint*)carrier & 0x40000000) != 0 || (*(uint*)carrier & 0x80000000) == 0)
        {
            *(uint*)e &= 0x7fffffff;
        }

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 0xb4;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * 0xb4;
        *(int*)(e + 0x20) -= 0xa000;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xdf, 0, -1);   // Ghidra: FUN_0047ac40
    }

    /// <summary>
    /// Ghidra: FUN_0046eb10 @ 0x0046eb10 — sous-zones 6 et 7. Decompilee dans <c>Blaze.c</c> ;
    /// 232 instructions.
    ///
    /// <para>
    /// <b>L'effondrement en quatre temps.</b> Rien ne bouge tant que la camera
    /// (<c>0x005edebc</c>) n'est pas a moins de 500 unites. Alors :
    /// </para>
    /// <list type="table">
    ///   <item><term>etat 2, trente images</term><description>il glisse de 5 unites sur le cap
    ///     perpendiculaire, s'enfonce d'un pas qui gagne <c>0x708</c> par image, secoue la camera
    ///     de <c>-2</c> a <c>+2</c> et seme <c>0x3d</c> une image sur deux. La voix <c>0x22</c> de
    ///     la zone ouvre et ferme le temps, grave a l'ouverture, plus grave encore a la
    ///     fermeture.</description></item>
    ///   <item><term>etat 3, vingt images</term><description>il bascule — <c>+0x34</c> gagne un pas
    ///     qui monte jusqu'a <c>0x28</c> — pendant que la secousse retombe de 20 a 0 <b>en changeant
    ///     de signe a chaque image</b>.</description></item>
    ///   <item><term>etat 4, dix images</term><description>il seme six <c>0x3d</c> par image depuis
    ///     un point releve de <c>0x46</c> et avance de <c>0x3c</c> sur le cap <c>+0x32 + 0x400</c>,
    ///     <b>puis remet ses trois coordonnees en place</b> ; la secousse retombe une seconde fois,
    ///     et l'etat 99 l'arrete.</description></item>
    /// </list>
    /// <para>
    /// <b>Ghidra perd la sauvegarde de Z</b> : sa pseudo-C ecrit <c>*(undefined4 *)(param_1 + 0x24)
    /// = unaff_EBP;</c> sans que <c>unaff_EBP</c> vienne de nulle part. Le desassemblage le donne —
    /// <c>mov eax,[esi+24h]</c> rangé dans <c>[esp+18h]</c> a l'entree de l'etat, rendu par
    /// <c>mov [esi+24h],eax</c> a la fin. Transcrit d'apres le binaire.
    /// </para>
    /// <para>
    /// <b>Et il rate deux fois la signature de la secousse</b>, comme ailleurs :
    /// <c>FUN_00547140(999,0,0)</c> est <c>rand()</c> sans argument, et <c>FUN_0041be60</c> en prend
    /// six.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046EB10(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x10) = 1;

        int* camera = *(int**)Mem.Entity(0x005edebc);
        int dx = (camera[0] >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
        int dy = (camera[1] >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
        int dz = (camera[2] >> 0xc) - (*(int*)(e + 0x24) >> 0xc);

        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) < 500
            && *(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x22), 0x400, 0xa00, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short course = *(short*)(e + 0x32);
            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(course) * 5;
            *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(course) * -5;
            *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x708;

            TryPublishCameraEffectNearEntity((uint*)e, 999, CrtRand() % 5 - 2, 999, 0, 0);   // Ghidra: FUN_0041be60
            if ((e[0x12] & 1) != 0) SpawnEffectNearCamera_00468110(e, 0x3d, 0, -1);          // Ghidra: FUN_00468110

            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) > 0x1d)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x22), 0x200, 0x1000, 0, 0);
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x16) = 0x14;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            if (*(short*)(e + 0x16) > 0)
            {
                short left = (short)(*(short*)(e + 0x16) - 1);
                *(short*)(e + 0x16) = left;
                int jolt = (e[0x14] & 1) != 0 ? -left : left;
                TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);
            }

            *(short*)(e + 0x34) += *(short*)(e + 0x12);
            short faster = (short)(*(short*)(e + 0x12) + 1);
            *(short*)(e + 0x14) += 1;
            *(short*)(e + 0x12) = faster;
            if (faster > 0x28) *(short*)(e + 0x12) = 0x28;

            if (*(short*)(e + 0x14) == 0x14)
            {
                *(short*)(e + 0x10) = 4;
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x16) = 10;
            }
        }

        if (*(short*)(e + 0x10) != 4) return;

        int savedY = *(int*)(e + 0x20);
        int savedX = *(int*)(e + 0x1c);
        int savedZ = *(int*)(e + 0x24);                             // Ghidra: `unaff_EBP`

        *(int*)(e + 0x20) = savedY + 0x46000;
        short spread = (short)(*(short*)(e + 0x32) + 0x400);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(spread) * 0x3c;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(spread) * 0x3c;

        for (int i = 6; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x3d, 0, -1);   // Ghidra: FUN_00468110

        *(int*)(e + 0x1c) = savedX;
        *(int*)(e + 0x24) = savedZ;
        *(int*)(e + 0x20) = savedY;

        if (*(short*)(e + 0x16) > 0)
        {
            short left = (short)(*(short*)(e + 0x16) - 1);
            *(short*)(e + 0x16) = left;
            if (left == 0) *(short*)(e + 0x10) = 99;

            int jolt = (e[0x14] & 1) != 0 ? -left : left;
            TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);
        }

        *(short*)(e + 0x14) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00438ea0 @ 0x00438ea0 — sous-zones 6 et 7. Decompilee dans <c>Blaze.c</c> ;
    /// 193 instructions.
    ///
    /// <para>
    /// <b>Deux corps dans un.</b> L'entite se colle a la position de son porteur <c>+0x6c</c> a
    /// chaque image, et son <c>+0x9a</c> — pose par la sous-zone — choisit lequel des deux
    /// comportements elle joue. Si le porteur meurt (bit <c>0x40000000</c>, ou eteint), elle
    /// s'eteint aussi, et <b>pour les roles 0 et 1 elle repeint le ciel</b> :
    /// <c>0x005de027</c> a 1, les trois octets <c>0x005de024</c>..<c>26</c> a zero.
    /// </para>
    /// <para>
    /// <b>Role 0 — l'incantation qui se charge.</b> Vingt images pendant lesquelles elle prend
    /// l'image <c>0x3f</c>, se donne une vitesse de <c>0x50000</c> vers le haut, un cap tire entre
    /// <c>0x400</c> et <c>0x7ff</c>, une rotation de face au hasard, une echelle <c>0x1fff</c> a
    /// plat, et lance <c>0x70</c> a chaque image. Puis deux <c>0xdf</c> ouverts a <c>+200</c> et
    /// <c>-200</c> sur <c>+0x5c</c>, et trente images d'attente.
    /// </para>
    /// <para>
    /// <b>Role 2 — la volee d'eclats.</b> Onze <c>0xed</c> d'un coup, puis cinquante images ; si le
    /// mot <c>+0x44</c> passe a 1, elle applique l'effet d'etat que <see cref="AfflictWithGuard"/>
    /// porte (<c>FUN_00439130</c>), et <b>s'eteint si l'effet a pris</b>. Sinon elle seme un
    /// <c>0xed</c> toutes les trois images pendant un compte tire entre 15 et 24, et s'eteint quand
    /// ce compte passe sous <c>-0x22</c>.
    /// </para>
    /// <para>
    /// <b>Le lanceur de l'effet</b> est l'entite par defaut <c>0x005de558</c> quand <c>+0x78</c> est
    /// nul, le porteur sinon — mais c'est <c>+0x78</c> lui-meme qui est passe en victime.
    /// </para>
    /// </summary>
    private static void EntityHandler_00438EA0(byte* e)
    {
        byte* raw = *(byte**)(e + 0x6c);
        byte* carrier = raw != null ? raw : Res.s_EntityList_005de558;   // Ghidra: &DAT_005de558

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        if ((*(uint*)carrier & 0x40000000) != 0 || (*(uint*)carrier & 0x80000000) == 0)
        {
            *(uint*)e &= 0x7fffffff;

            if (*(short*)(e + 0x9a) == 0 || *(short*)(e + 0x9a) == 1)
            {
                // Ghidra: DAT_005de027 = 1 puis DAT_005de024..26 = 0 — le voile plein cadre, noir et affiche.
                Mem.s_Unknown_005de024 = 0x01000000;
            }

            return;
        }

        if (*(short*)(e + 0x9a) == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x16) = 0x14;
                *(byte**)(e + 0x74) = raw;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                *(short*)(e + 0x16) -= 1;
                if (*(short*)(e + 0x16) < 1) *(short*)(e + 0x10) = 2;

                e[4] = 0x3f;
                e[0x0a] = 2;
                e[0x0b] = 0;
                *(byte**)(e + 0x70) = raw;
                *(int*)(e + 0x44) = 0;
                *(int*)(e + 0x48) = 0x50000;
                *(int*)(e + 0x4c) = 0;

                *(short*)(e + 0x32) = (short)(SignedMask(CrtRand(), 0x3ff) + 0x400);
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x34) = (short)SignedMask(CrtRand(), 0xfff);
                *(short*)(e + 0x12) = 200;
                *(short*)(e + 0x28) = 0x1fff;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0x100;

                SpawnCastEntity_0047AAE0(e, 0x70, 0, -1);           // Ghidra: FUN_0047aae0
            }

            if (*(short*)(e + 0x10) == 2)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x16) = 0x1e;
                *(int*)(e + 0x5c) = 200;
                SpawnCastEntity_0047AAE0(e, 0xdf, 0, -1);
                *(int*)(e + 0x5c) = -200;
                SpawnCastEntity_0047AAE0(e, 0xdf, 0, -1);
            }

            if (*(short*)(e + 0x10) == 3)
            {
                *(short*)(e + 0x16) -= 1;
                if (*(short*)(e + 0x16) < 0) *(uint*)e &= 0x7fffffff;
            }
        }

        if (*(short*)(e + 0x9a) != 2) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = 0;
            for (int i = 0x0b; i != 0; i--)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xed, 0, -1);   // Ghidra: FUN_0047ac40
            }

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) > 0x32) *(uint*)e &= 0x7fffffff;

            if (*(int*)(e + 0x44) == 1)
            {
                *(short*)(e + 0x10) = 2;
                int spread = CrtRand();
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x12) = (short)(spread % 10 + 0x0f);

                byte* caster = *(byte**)(e + 0x78) != null ? carrier : Res.s_EntityList_005de558;
                if (AfflictWithGuard(caster, *(byte**)(e + 0x78), 0))   // Ghidra: FUN_00439130
                {
                    *(uint*)e &= 0x7fffffff;
                    return;
                }
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x14) += 1;
        if (*(short*)(e + 0x14) != 3) return;

        *(short*)(e + 0x14) = 0;
        if (*(short*)(e + 0x12) > 0)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xed, 0, -1);
        }

        *(short*)(e + 0x12) -= 1;
        if (*(short*)(e + 0x12) < -0x22) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: FUN_00439130 @ 0x00439130 — <b>l'effet d'etat 32 sous garde</b> : si
    /// <see cref="IsEffectPairBlocked_004364B0"/> refuse la paire, rendre <c>true</c> tout de
    /// suite ; sinon rendre « l'appliqueur a pris ». C'est la meme enveloppe de 22 instructions que
    /// <c>FUN_0043a400</c> et <c>FUN_004365d0</c>, qui appliquent l'effet <b>13</b> a la
    /// place.</summary>
    private static bool AfflictWithGuard(byte* caster, byte* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0((uint*)caster, (uint*)victim) != 0) return true;
        return ApplyStatusEffect_0053C5E0((uint*)caster, (uint*)victim, keep) != 0;
    }

    /// <summary>La jumelle des deux autres enveloppes, <c>FUN_0043a400</c> (sous-zones 6-7) et
    /// <c>FUN_004365d0</c> (4-9) : meme garde, mais l'effet <b>13</b>.</summary>
    private static bool AfflictWithGuard13(byte* caster, byte* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0((uint*)caster, (uint*)victim) != 0) return true;
        return ApplyStatusEffect_0053B8F0((uint*)caster, (uint*)victim, keep) != 0;
    }

    // =====================================================================
    // LOT 8, seconde moitie — le decor releve au desassembleur
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004370e0</c> (sous-zones 4 et 9) et <c>0x00439fa0</c> (6 et 7). <b>Absentes de
    /// <c>Blaze.c</c></b> ; 140 et 136 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_004370E0.asm</c>. <b>La sœur de
    /// <see cref="EntityHandler_00439BD0"/></b> : meme ossature, mais deux hauteurs au lieu d'une,
    /// et le porteur range dans <c>+0x78</c> et non <c>+0x74</c>.
    ///
    /// <para>
    /// Son <c>+0x9a</c> — pose par la sous-zone — choisit la suspension : role 0, elle se tient
    /// <c>0x96</c> unites au-dessus de son porteur et glisse de 50 unites par image sur son cap ;
    /// role 1, <c>0x64</c> unites au-dessus et 220 par image. Les deux descendent de <c>0xa000</c>
    /// et font naitre <c>0xde</c> a chaque image.
    /// </para>
    /// <para>
    /// <b>Une seule instruction les separe, et elle change le son.</b> <c>0x004370e0</c> ouvre par
    /// <c>if (+0x10 == 0) { +0x10 = 1; +0x16 = 0; }</c> — ce que sa jumelle n'a pas. Le bloc suivant
    /// teste lui aussi <c>+0x10 == 0</c> pour sonner la naissance (voix <c>0x1f</c>) et rearmer
    /// <c>+0x98</c> : pour <c>0x004370e0</c> ce test est <b>toujours faux</b>, si bien qu'elle
    /// <b>ne sonne jamais</b> et que son compteur de voix part de ce que l'emplacement contenait.
    /// Defaut de 1998, reproduit.
    /// </para>
    /// </summary>
    private static void EntityHandler_004370E0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
        }

        TickCarriedBreath(e);
    }

    private static void EntityHandler_00439FA0(byte* e) => TickCarriedBreath(e);

    /// <summary>Le corps commun des deux souffles portes.</summary>
    private static void TickCarriedBreath(byte* e)
    {
        byte* raw = *(byte**)(e + 0x6c);
        byte* carrier = raw != null ? raw : Res.s_EntityList_005de558;   // Ghidra: &DAT_005de558

        *(byte**)(e + 0x78) = raw;

        short role = *(short*)(e + 0x9a);

        if (role == 0)
        {
            *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
            *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x96000;
            *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
            *(short*)(e + 0x32) = *(short*)(carrier + 0x32);
        }

        if (role == 1)
        {
            *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
            *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x64000;
            *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
            *(short*)(e + 0x32) = *(short*)(carrier + 0x32);
        }

        if (*(short*)(e + 0x10) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, 0x1f, 0x6fd, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x98) == 1)
        {
            short voice = (short)(*(short*)(e + 0x9a) == 1 ? 0x23 : 0x20);
            TryPlaySoundNearEntity((uint*)e, voice, 0x1000, 0x800, 0, 0);
        }

        if (*(short*)(e + 0x98) == 5) *(short*)(e + 0x98) = 0;
        *(short*)(e + 0x98) += 1;

        if (carrier[0x43] == 1) *(uint*)e &= 0x7fffffff;
        if ((*(uint*)carrier & 0x40000000) != 0 || (*(uint*)carrier & 0x80000000) == 0)
        {
            *(uint*)e &= 0x7fffffff;
        }

        short course = *(short*)(e + 0x32);

        if (role == 0)
        {
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 50;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 50;
            *(int*)(e + 0x20) -= 0xa000;
        }

        if (role == 1)
        {
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 220;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 220;
            *(int*)(e + 0x20) -= 0xa000;
        }

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xde, 0, -1);   // Ghidra: FUN_0047ac40
    }

    /// <summary>
    /// Ghidra: <c>0x0046e410</c> — sous-zones 6 et 7. <b>Absente de <c>Blaze.c</c></b> ;
    /// 157 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0046E410.asm</c>. <b>Le lien du lot 7</b>
    /// (<see cref="TickNeighbourTether"/>) dans sa variante haute, plus <b>une garde</b>.
    ///
    /// <para>
    /// Quand le voisin est assez pres, les six grains ne partent que si <b>le voisin est au-dessus
    /// de <c>0x258000</c></b>, ou si la sous-zone courante (<c>0x005de52c</c>) n'est pas la
    /// septieme. Dans la salle 7, un voisin bas fait donc taire la gerbe — le drapeau, lui, tombe
    /// quand meme.
    /// </para>
    /// </summary>
    private static void EntityHandler_0046E410(byte* e) => TickNeighbourTether(e, 0x82000, guardsOnHeight: true);

    /// <summary>
    /// Ghidra: <c>0x0046ede0</c> — sous-zones 6 et 7. <b>Absente de <c>Blaze.c</c></b> ;
    /// 183 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0046EDE0.asm</c>.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    /// <b>La chose qui remonte le couloir en flottant.</b> Elle avance sur Z de <c>+0x14</c> unites
    /// par image, reste dans une gouttiere en X — sous <c>-0x69000</c> elle est repoussee de
    /// <c>0x3000</c>, au-dessus de <c>-0x5f000</c> elle l'est de <c>-0x3000</c>, les bornes se
    /// lisant sur les douze bits hauts — et <b>ondule</b> : son cap <c>+0x32</c> gagne <c>0x44</c>
    /// par image et sa hauteur suit <c>(sin(+0x32) &lt;&lt; +0x12) + 0x64000</c>, <c>+0x12</c> etant
    /// l'amplitude que la sous-zone a posee.
    /// </para>
    /// <para>
    /// <b>Elle accelere quand elle frotte.</b> Toutes les quatre images, si le contact <c>0x100</c>
    /// est present, <c>+0x14</c> gagne 1 jusqu'a <c>0x28</c>. Et elle garde dans <c>+0x9a</c> sa
    /// hauteur en unites entieres, <b>relevee avant le placement</b>.
    /// </para>
    /// <para>
    /// <b>Au choc</b> (<c>0x8800</c>), tout est remis comme avant — position, appui, face, contacts —
    /// et les deux vitesses horizontales <b>changent de signe</b>. Sans contact <c>0x100</c> et avec
    /// <c>+0x5c</c> nul, elle retient sa hauteur dans <c>+0x5c</c> : c'est le debut d'une chute, que
    /// la suite deroule en posant <c>+0x20 = +0x5c &lt;&lt; 12</c> et en retirant 13 a <c>+0x5c</c>
    /// par image, plancher <c>-10000</c>, tout en ralentissant <c>+0x14</c> d'une unite toutes les
    /// deux images jusqu'a l'arret.
    /// </para>
    /// <para>
    /// Le bit <c>0x20000000</c> l'eteint. L'etat <b>99</b> la remet a
    /// <c>(-0x64000, -0xff0000, -0x871000)</c> avec <c>+0x12</c> nul et saute tout le reste — le
    /// retour au depart —, et c'est le seul etat ou <see cref="TestTriggerVolumes_00534CD0"/> n'est
    /// pas appele.
    /// </para>
    /// </remarks>
    private static void EntityHandler_0046EDE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0x63)
        {
            *(int*)(e + 0x1c) = unchecked((int)0xfff9c000);
            *(int*)(e + 0x20) = unchecked((int)0xff010000);
            *(int*)(e + 0x24) = unchecked((int)0xff78f000);
            *(short*)(e + 0x12) = 0;
        }
        else
        {
            if (*(short*)(e + 0x10) == 2)
            {
                int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) << e[0x12];
                *(int*)(e + 0x20) += unchecked((int)0xfff9c000) - wave;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x14) = 5;
                *(short*)(e + 0x16) = 0;
                *(int*)(e + 0x5c) = 0;
            }

            int saved80 = *(int*)(e + 0x80);
            int saved84 = *(int*)(e + 0x84);
            int saved88 = *(int*)(e + 0x88);
            int saved8c = *(int*)(e + 0x8c);
            int savedGroundX = *(int*)(e + 0x60);
            int savedGroundZ = *(int*)(e + 0x64);
            byte* savedFace = *(byte**)(e + 0x7c);
            uint savedContacts = *(ushort*)(e + 0x90);

            int x = *(int*)(e + 0x1c);
            int y = *(int*)(e + 0x20);
            int z = *(int*)(e + 0x24);

            *(int*)(e + 0x44) = x;
            *(int*)(e + 0x48) = y;
            *(int*)(e + 0x4c) = z;

            if ((x & unchecked((int)0xfffff000)) < unchecked((int)0xfff97000))
            {
                x += 0x3000;
                *(int*)(e + 0x1c) = x;
            }

            x = *(int*)(e + 0x1c);
            if ((x & unchecked((int)0xfffff000)) > unchecked((int)0xfffa1000))
            {
                x += unchecked((int)0xffffd000);
                *(int*)(e + 0x1c) = x;
            }

            *(int*)(e + 0x24) = (*(short*)(e + 0x14) << 0xc) + z;

            *(short*)(e + 0x16) += 1;
            if (*(short*)(e + 0x16) > 3 && (*(uint*)(e + 0x90) & 0x100) != 0)
            {
                short faster = (short)(*(short*)(e + 0x14) + 1);
                *(short*)(e + 0x16) = 0;
                *(short*)(e + 0x14) = faster;
                if (faster > 0x28) *(short*)(e + 0x14) = 0x28;
            }

            *(short*)(e + 0x9a) = (short)(y >> 0xc);
            FinalizeEntityPlacement2((uint*)e);                     // Ghidra: FUN_00533940

            if ((*(uint*)(e + 0x90) & 0x8800) != 0)
            {
                *(int*)(e + 0x60) = savedGroundX;
                *(int*)(e + 0x1c) = *(int*)(e + 0x44);
                *(int*)(e + 0x24) = *(int*)(e + 0x4c);
                *(int*)(e + 0x64) = savedGroundZ;
                *(int*)(e + 0x84) = saved84;
                *(int*)(e + 0x88) = saved88;
                *(int*)(e + 0x80) = saved80;
                *(int*)(e + 0x8c) = saved8c;
                *(byte**)(e + 0x7c) = savedFace;
                *(uint*)(e + 0x90) = savedContacts & 0xffff;

                *(int*)(e + 0x50) = -*(int*)(e + 0x50);
                *(int*)(e + 0x58) = -*(int*)(e + 0x58);
            }

            if ((*(uint*)(e + 0x90) & 0x100) == 0 && *(int*)(e + 0x5c) == 0)
            {
                *(int*)(e + 0x5c) = *(short*)(e + 0x9a);
            }

            if ((*(uint*)e & 0x20000000) != 0) *(uint*)e &= 0x7fffffff;

            if (*(int*)(e + 0x20) > *(int*)(e + 0x60)) *(int*)(e + 0x20) = *(int*)(e + 0x60);

            int drop = *(int*)(e + 0x5c);
            if (drop != 0)
            {
                *(int*)(e + 0x20) = drop << 0xc;
                *(int*)(e + 0x5c) = drop - 13;
                if (*(int*)(e + 0x5c) < -10000) *(int*)(e + 0x5c) = -10000;

                if (*(int*)(e + 0x20) < *(int*)(e + 0x60)) *(int*)(e + 0x20) = *(int*)(e + 0x60);

                *(short*)(e + 0x16) += 1;
                if (*(short*)(e + 0x16) > 1)
                {
                    *(short*)(e + 0x14) -= 1;
                    *(short*)(e + 0x16) = 0;
                    if (*(short*)(e + 0x14) < 0) *(short*)(e + 0x14) = 0;
                }
            }
        }

        *(short*)(e + 0x32) += 0x44;
        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

        *(int*)(e + 0x20) += ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) << e[0x12]) + 0x64000;

        if (*(short*)(e + 0x10) != 0x63) TestTriggerVolumes_00534CD0((uint*)e);   // Ghidra: FUN_00534cd0
    }

    // =====================================================================
    // LOT 10 (2026-09-20) — LA TOURELLE, CINQ COPIES
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00468ac0</c> (bloc 0), <c>0x0046b6e0</c> (2-3), <c>0x0046d860</c> (5-8-10),
    /// <c>0x0046fad0</c> (6-7) et <c>0x004702d0</c> (sous-zone 1). <b>Absentes de <c>Blaze.c</c></b> ;
    /// 261 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_00468AC0.asm</c>. <b>Les cinq corps sont
    /// identiques instruction pour instruction</b>, adresses mises à part.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    /// <b>La tourelle qui suit un membre du groupe.</b> Trois champs que la sous-zone lui pose
    /// deviennent ses réglages à la première image : <c>+0x14</c> la <b>portée</b> (rangée dans
    /// <c>+0x44</c>), <c>+0x30</c> le <b>décalage de vitesse de rotation</b> (rangé dans
    /// <c>+0x48</c>, puis remis à zéro), et <c>+0x16</c> la <b>demi-ouverture du cône de vue</b> —
    /// celle-là est relue à chaque image, jamais rangée. Son cap de repos est <c>+0x12</c>.
    /// </para>
    /// <list type="table">
    ///   <item><term>état 1, la recherche</term><description>elle mesure les quatre membres vivants
    ///     <b>en plan</b>, garde ceux qui sont plus près que la portée et dans le cône —
    ///     l'angle relatif au cap de repos doit tomber entre <c>0</c> et l'ouverture, ou entre
    ///     <c>0x1000 - ouverture</c> et <c>0x1000</c> —, et retient <b>le plus proche</b> ; à égalité,
    ///     le dernier des quatre l'emporte, le binaire ne s'arrêtant pas au premier. Sans personne,
    ///     elle revient vers son cap de repos d'un huitième par image.</description></item>
    ///   <item><term>état 2, la poursuite</term><description>elle tourne vers sa cible de l'écart
    ///     divisé par <c>2 ^ +0x48</c>. Quand son cap s'est stabilisé — moins de 5 d'écart avec
    ///     celui de l'image précédente, gardé dans <c>+0x4c</c> — elle arme un tir de trente images.
    ///     Perdre la cible, hors de portée ou hors du cône <b>élargi de <c>0x12c</c></b>, la ramène à
    ///     l'état 1.</description></item>
    ///   <item><term>le tir</term><description>tant que <c>+0x9a</c> descend, elle publie la
    ///     position de sa cible dans <c>+0x50</c>..<c>+0x58</c> et sème <c>0x31</c> à chaque image,
    ///     avec la voix <c>0x28</c> de la zone <b>une seule fois</b> (verrou <c>+0x98</c>). Une cible
    ///     qui passe plus haut que la tourelle plus <c>0xb4</c> interrompt le tir sur-le-champ.</description></item>
    /// </list>
    /// <para>
    /// <b>Une garde qui ne peut pas échouer.</b> L'armement du tir teste aussi l'octet haut de
    /// <c>+0x48</c> (<c>test ch,0FFh</c>) : comme <c>+0x48</c> vient d'un <c>movsx</c> d'un petit
    /// décalage, cet octet est toujours nul et le test passe toujours. Relevé tel quel.
    /// </para>
    /// <para>
    /// <b>Le verrou de son</b> est remis à zéro à chaque image où <c>+0x5c</c> vaut <c>-1</c>,
    /// c'est-à-dire tant qu'aucune cible n'est retenue.
    /// </para>
    /// </remarks>
    private static void TickBeamTurret_00468AC0(byte* e)
    {
        short fov = *(short*)(e + 0x16);
        short rest = (short)(0x1000 - fov);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = *(short*)(e + 0x32);
            *(int*)(e + 0x44) = *(short*)(e + 0x14);
            *(int*)(e + 0x48) = *(short*)(e + 0x30);
            *(short*)(e + 0x30) = 0;
            *(int*)(e + 0x4c) = 0;
            *(int*)(e + 0x5c) = -1;
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x10) = 1;
        }

        if (*(int*)(e + 0x5c) == -1) *(short*)(e + 0x98) = 0;

        if (*(short*)(e + 0x10) == 1)
        {
            // ------------------------------------------------- la recherche
            *(int*)(e + 0x5c) = -1;

            short* seen = stackalloc short[4];
            byte* member = Inp.s_SlotFlags_005e93c0;                // 0x005e93c0

            for (int slot = 0; slot < 4; slot++, member += 0x9c)
            {
                seen[slot] = 0x270f;

                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
                int angle = Winsys3d.Ordinal_457(dz, dx) + (0x800 - *(short*)(e + 0x12));

                if (distance >= *(int*)(e + 0x44)) continue;

                // Le binaire ne rejette pas tout de suite un angle nul ou negatif sous l ouverture :
                // il tombe sur le second intervalle. Les deux tests sont donc un OU.
                bool inCone = (angle < fov && angle > 0) || (angle < 0x1000 && angle > rest);

                if (inCone) seen[slot] = (short)distance;
            }

            short nearest = 0x270f;
            for (int slot = 0; slot < 4; slot++)
            {
                if (seen[slot] < nearest) nearest = seen[slot];
            }

            if (nearest == 0x270f)
            {
                short course = *(short*)(e + 0x32);
                *(short*)(e + 0x32) = (short)(((*(short*)(e + 0x12) - course) >> 3) + course);
                return;
            }

            // Le binaire ne s'arrete pas au premier : a egalite, le dernier l'emporte.
            for (int slot = 0; slot < 4; slot++)
            {
                if (seen[slot] == nearest) *(int*)(e + 0x5c) = slot;
            }

            *(short*)(e + 0x10) = 2;
            return;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            // ------------------------------------------------- la poursuite
            byte* target = Inp.s_SlotFlags_005e93c0 + *(int*)(e + 0x5c) * 0x9c;

            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int swing = ((Winsys3d.Ordinal_457(dz, dx) - *(short*)(e + 0x32)) & 0xfff) - 0x800;
            *(short*)(e + 0x32) += (short)(swing >> *(int*)(e + 0x48));

            int drift = (short)(*(int*)(e + 0x4c) - *(short*)(e + 0x32));
            if (drift < 0) drift = -drift;

            if (drift < 5)
            {
                // Ghidra: `test ch,0FFh` sur +0x48 — toujours nul, la garde passe toujours.
                if (*(short*)(e + 0x9a) == 0 && ((*(int*)(e + 0x48) >> 8) & 0xff) == 0)
                {
                    *(short*)(e + 0x9a) = 0x1e;
                }
            }
            else if (*(short*)(e + 0x9a) == 0)
            {
                *(short*)(e + 0x98) = 0;
            }

            int angle = Winsys3d.Ordinal_457(dz, dx) + (0x800 - *(short*)(e + 0x12));
            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            bool lost = distance > *(int*)(e + 0x44);
            if (!lost && angle > fov + 0x12c && angle < rest - 0x12c) lost = true;

            if (lost)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x9a) = 0;
            }

            *(int*)(e + 0x4c) = *(short*)(e + 0x32);
        }

        // ------------------------------------------------- le tir
        if (*(short*)(e + 0x9a) <= 0) return;

        *(short*)(e + 0x9a) -= 1;

        byte* aim = Inp.s_SlotFlags_005e93c0 + *(int*)(e + 0x5c) * 0x9c;
        *(int*)(e + 0x50) = *(int*)(aim + 0x1c);
        *(int*)(e + 0x54) = *(int*)(aim + 0x20);
        *(int*)(e + 0x58) = *(int*)(aim + 0x24);

        if ((*(int*)(e + 0x20) >> 0xc) + 0xb4 > (*(int*)(e + 0x54) >> 0xc))
        {
            SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);         // Ghidra: FUN_00468110

            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x28), 0x800, 0x1000, 0, 0);   // Ghidra: FUN_00468080
                *(short*)(e + 0x98) = 1;
            }
        }
        else
        {
            *(short*)(e + 0x9a) = 0;
        }

        if (*(short*)(e + 0x9a) == 0) *(short*)(e + 0x10) = 1;
    }

    private static void EntityHandler_00468AC0(byte* e) => TickBeamTurret_00468AC0(e);
    private static void EntityHandler_0046B6E0(byte* e) => TickBeamTurret_00468AC0(e);
    private static void EntityHandler_0046D860(byte* e) => TickBeamTurret_00468AC0(e);
    private static void EntityHandler_0046FAD0(byte* e) => TickBeamTurret_00468AC0(e);
    private static void EntityHandler_004702D0(byte* e) => TickBeamTurret_00468AC0(e);

    // =====================================================================
    // LOT 11 (2026-09-20) — LA SOUS-ZONE 0 : TROIS FEUILLES ET QUATRE CORPS
    //
    // Les quatre gros corps de la sous-zone 0 — le rocher a deux axes, le radeau qui monte, le
    // traineau (dans `SealedCaveMecanismes.cs`) et le tambour qui ecrase — partagent trois
    // feuilles qui vivent toutes sur les mots 0x005717d0..0x0057182c de `s_EntityState_00571500`.
    //
    // LA SOUS-ZONE 1 REJOUE LES MEMES TROIS FEUILLES, 0x150 PLUS HAUT. 0x004725a0, 0x00472630 et
    // 0x00471dd0 sont les copies exactes de 0x0046a5a0, 0x0046a630 et 0x0046a3f0 — verifie
    // instruction par instruction le 2026-09-20 —, avec DEUX differences et deux seulement :
    // chacune de leurs globales vaut celle de la sous-zone 0 PLUS 0x150, et la bouffee fait naitre
    // le sous-type 0x32 au lieu de 0x34. D ou les parametres `bank` et `puff/subtype` ci-dessous,
    // plutot que trois corps de plus. (Le virage recopie intervertit deux `shr` — v/8 et v/64 sont
    // additionnes dans l autre ordre, la somme ne bouge pas : allocation de registres, pas
    // semantique.)
    // =====================================================================

    /// <summary>Le decalage qui separe les globales de la sous-zone 1 de celles de la sous-zone 0 :
    /// toutes, sans exception, valent la meme plus <c>0x150</c>.</summary>
    private const int SubArea1Bank = 0x150;

    /// <summary>
    /// Ghidra: <c>0x0046a5a0</c>. <b>Absente de <c>Blaze.c</c></b> ; 33 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_0046A5A0.asm</c>.
    ///
    /// <para>
    /// <b>Le glissement du tangage de l'entite vers une valeur visee</b>, en <b>quatre images</b>.
    /// Deux mots globaux portent tout l'etat : <c>0x00571818</c> la valeur visee et
    /// <c>0x0057181c</c> le pas par image. La feuille sert donc <b>une seule glissade a la fois</b>
    /// pour toute la sous-zone — ce qui suffit, les quatre corps ne tournent jamais ensemble.
    /// </para>
    /// <list type="table">
    ///   <item><term><c>mode != 0</c> et pas nul</term><description>elle <b>arme</b> : la visee
    ///     devient <c>target</c> et le pas <c>(target - +0x34) / 4</c>, arrondi vers zero. Armer
    ///     pendant une glissade en cours <b>ne fait rien</b> — le pas n'est pas nul, et la fonction
    ///     tombe dans la branche d'avance, qui exige <c>mode == 0</c>.</description></item>
    ///   <item><term><c>mode == 0</c> et pas non nul</term><description>elle <b>avance</b> :
    ///     <c>+0x34</c> gagne le pas, et des que le tangage a <b>franchi</b> la visee — dans le sens
    ///     du pas — il y est cale net et le pas repasse a zero, ce qui desarme.</description></item>
    ///   <item><term>les deux autres cas</term><description>rien du tout.</description></item>
    /// </list>
    /// <para>
    /// <b>La comparaison de franchissement se lit sur le signe du pas</b>, pas sur la distance :
    /// un pas positif s'arrete quand le tangage atteint la visee par en dessous, un pas negatif
    /// quand il l'atteint par au-dessus. Un pas exactement nul ne peut pas arriver ici, la branche
    /// d'avance l'ayant deja ecarte.
    /// </para>
    /// </summary>
    private static void CameraPitchGlide_0046A5A0(byte* e, int mode, int target, int bank = 0)
    {
        int* aim = Mem.EntityState(0x00571818 + bank);              // Ghidra: DAT_00571818 / DAT_00571968
        int* step = Mem.EntityState(0x0057181c + bank);             // Ghidra: DAT_0057181c / DAT_0057196c

        if (mode != 0 && *step == 0)
        {
            *aim = target;
            int gap = target - *(short*)(e + 0x34);
            // Ghidra: `cdq ; and edx,3 ; add ; sar 2` — la division signee par 4 du compilateur 1998.
            *step = (gap + ((gap >> 31) & 3)) >> 2;
            return;
        }

        if (mode != 0 || *step == 0) return;

        *(short*)(e + 0x34) += (short)*step;

        short pitch = *(short*)(e + 0x34);
        bool arrived = *step > 0 ? pitch >= *aim : pitch <= *aim;

        if (!arrived) return;

        *(short*)(e + 0x34) = (short)*aim;
        *step = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0046a630</c>. <b>Absente de <c>Blaze.c</c></b> ; 74 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_0046A630.asm</c>.
    ///
    /// <para>
    /// <b>La bouffee de poussiere lachee a cote de l'entite.</b> Le parametre <c>which</c> designe
    /// un <b>coin</b> : <c>1</c> a <c>4</c> valent <c>0x600</c>, <c>0xa00</c>, <c>0xe00</c> et
    /// <c>0x1200</c> par une table de sauts a <c>0x0046a75c</c> ; toute autre valeur est prise
    /// telle quelle comme un angle.
    /// </para>
    /// <para>
    /// La feuille <b>deplace l'entite elle-meme</b> le temps de semer : elle range <c>+0x1c</c>,
    /// <c>+0x20</c>, <c>+0x24</c> et <c>+0x32</c>, se porte a <c>100</c> unites sur
    /// <c>which + cap</c>, bouscule le cap de <c>SignedMask(rand, 0xff) + 0x380</c>, fait naitre
    /// <b>sept</b> <c>0x34</c>, puis remet les quatre champs en place. Les sept effets naissent donc
    /// tous au meme endroit, avec le meme cap bouscule — c'est leur propre corps qui les eparpille.
    /// </para>
    /// <para>
    /// <b>Une image sur dix</b> la voix <c>0x36</c> de la zone claque, a la hauteur
    /// <c>rand % 100 + 0x3ce</c> et au gain <c>0x800</c>.
    /// </para>
    /// <para>
    /// <c>+0x20</c> est range et remis alors que rien ne l'a touche : Ghidra ne recupere pas les
    /// deux emplacements de pile reutilises et les rend en <c>unaff_retaddr</c> (<c>+0x20</c>) et
    /// <c>unaff_EBX</c> (<c>+0x24</c>) — lus ici dans le desassemblage, a <c>0046a679</c>,
    /// <c>0046a681</c> et <c>0046a73f</c>.
    /// </para>
    /// </summary>
    private static void SmokePuff_0046A630(byte* e, int which, short subtype = 0x34)
    {
        int angle = which switch
        {
            1 => 0x600,
            2 => 0x0a00,
            3 => 0x0e00,
            4 => 0x1200,
            _ => which,
        };

        short savedCourse = *(short*)(e + 0x32);
        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(angle + savedCourse) * 100;      // Ghidra: 0x00545a1c
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(angle + *(short*)(e + 0x32)) * 100;   // Ghidra: 0x00545a16

        *(short*)(e + 0x32) += (short)(SignedMask(CrtRand(), 0xff) + 0x380);

        for (int i = 7; i != 0; i--) SpawnEffectNearCamera_00468110(e, subtype, 0, -1);   // Ghidra: FUN_00468110

        if (CrtRand() % 10 == 1)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x36),
                                   (short)(CrtRand() % 100 + 0x3ce), 0x800, 0, 0);   // Ghidra: FUN_00468080
        }

        *(int*)(e + 0x1c) = savedX;
        *(int*)(e + 0x20) = savedY;
        *(int*)(e + 0x24) = savedZ;
        *(short*)(e + 0x32) = savedCourse;
    }

    /// <summary>
    /// Ghidra: <c>0x0046a3f0</c>. <b>Absente de <c>Blaze.c</c></b> ; 100 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_0046A3F0.asm</c>.
    ///
    /// <para>
    /// <b>Le virage a angle droit du traineau</b>, tenu par six mots globaux :
    /// <c>0x005717d0</c> la vitesse courante (ecrite par le traineau lui-meme),
    /// <c>0x005717e4</c> le compte a rebours avant la prochaine bouffee,
    /// <c>0x005717e8</c> le cap vise, <c>0x005717ec</c> le drapeau « virage fini »,
    /// <c>0x005717f0</c> la coordonnee de sortie et <c>0x005717f4</c> le pas de cap par image.
    /// </para>
    /// <list type="table">
    ///   <item><term><c>dir == 1</c> / <c>dir == -1</c></term><description>elle <b>arme</b> un
    ///     quart de tour : le pas vaut <c>vitesse * 73 / 64</c> — le compilateur l'ecrit
    ///     <c>v + v/8 + v/64</c> en decalages non signes —, dans un sens ou dans l'autre, le cap
    ///     vise est le cap courant plus ou moins <c>0x400</c>, le drapeau retombe et le compte a
    ///     rebours part a <c>rand % 9</c>.</description></item>
    ///   <item><term><c>dir == 0</c></term><description>elle <b>avance</b>, et seulement tant que le
    ///     drapeau est eteint.</description></item>
    /// </list>
    /// <para>
    /// A l'avance, le compte a rebours descend ; quand il touche <c>-1</c> il repart a
    /// <c>SignedMask(rand, 7) + 1</c> et <b>deux bouffees</b> partent — les coins <c>1</c> et
    /// <c>2</c> quand le pas est positif, <c>3</c> et <c>4</c> quand il est negatif, c'est-a-dire
    /// du cote vers lequel le traineau se penche.
    /// </para>
    /// <para>
    /// Puis le cap gagne le pas, et <b>des qu'il a franchi la visee</b> il y est cale, le drapeau
    /// se leve, le cap est replie dans <c>0..0x1000</c>, et la <b>coordonnee de sortie</b> est
    /// posee : sur <c>+0x1c</c> quand le cap final n'est ni <c>0</c> ni <c>0x800</c>, sur
    /// <c>+0x24</c> sinon. C'est le recalage sur la grille du couloir apres le quart de tour.
    /// </para>
    /// </summary>
    private static void SteerSled_0046A3F0(byte* e, int dir, int endValue, int bank = 0, short puff = 0x34)
    {
        uint speed = *(uint*)Mem.EntityState(0x005717d0 + bank);    // Ghidra: DAT_005717d0 / DAT_00571920
        int* countdown = Mem.EntityState(0x005717e4 + bank);        // Ghidra: DAT_005717e4 / DAT_00571934
        int* aim = Mem.EntityState(0x005717e8 + bank);              // Ghidra: DAT_005717e8 / DAT_00571938
        int* settled = Mem.EntityState(0x005717ec + bank);          // Ghidra: DAT_005717ec / DAT_0057193c
        int* exitValue = Mem.EntityState(0x005717f0 + bank);        // Ghidra: DAT_005717f0 / DAT_00571940
        int* step = Mem.EntityState(0x005717f4 + bank);             // Ghidra: DAT_005717f4 / DAT_00571944

        // Ghidra: `shr 6` et `shr 3` NON signes sur la vitesse, puis deux additions — v * 73 / 64.
        int swing = (int)(speed + (speed >> 6) + (speed >> 3));

        if (dir == 1)
        {
            *step = swing;
            *aim = *(short*)(e + 0x32) + 0x400;
            *settled = 0;
            *exitValue = endValue;
            *countdown = CrtRand() % 9;
        }

        if (dir == -1)
        {
            *step = -swing;
            *aim = *(short*)(e + 0x32) - 0x400;
            *settled = 0;
            *exitValue = endValue;
            *countdown = CrtRand() % 9;
        }

        if (dir != 0 || *settled != 0) return;

        int side = *step <= 0 ? 1 : 0;
        *countdown -= 1;

        if (*countdown == -1)
        {
            *countdown = SignedMask(CrtRand(), 7) + 1;
            SmokePuff_0046A630(e, side * 2 + 1, puff);              // Ghidra: FUN_0046a630 / FUN_00472630
            SmokePuff_0046A630(e, side * 2 + 2, puff);
        }

        *(short*)(e + 0x32) += (short)*step;

        short course = *(short*)(e + 0x32);
        bool arrived = *step < 0 ? course <= *aim : *step > 0 && course >= *aim;
        if (!arrived) return;

        *(short*)(e + 0x32) = (short)*aim;
        *settled = 1;

        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;
        if (*(short*)(e + 0x32) < 0) *(short*)(e + 0x32) += 0x1000;

        if (*(short*)(e + 0x32) != 0 && *(short*)(e + 0x32) != 0x800)
        {
            *(int*)(e + 0x1c) = *exitValue << 0xc;
            return;
        }

        *(int*)(e + 0x24) = *exitValue << 0xc;
    }

    /// <summary>
    /// Ghidra: <c>0x00469100</c> — sous-zone 0. <b>Absente de <c>Blaze.c</c></b> ; 284 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00469100.asm</c>.
    ///
    /// <para>
    /// <b>Le rocher qui roule sur deux axes.</b> C'est le cousin de
    /// <see cref="EntityHandler_00468E30"/>, qui ne roule que sur le Z : meme charpente —
    /// sauvegarde de dix champs, avance, contact, restauration — mais il avance
    /// <b>en X et en Z a la fois</b>, il emporte ce qui se tient dessus, et il garde une
    /// <b>vitesse de base</b> dans <c>+0x5c</c>.
    /// </para>
    /// <list type="table">
    ///   <item><term>a la naissance</term><description><c>+0x5c</c> prend le <c>+0x14</c> que la
    ///     sous-zone a pose — c'est la base autour de laquelle les deux vitesses sont tirees.
    ///     <c>+0x12</c> et <c>+0x14</c> valent chacun <c>signe * (base + rand % 0x17)</c>,
    ///     <c>+0x16</c> vaut <c>±1</c>, et le bit <c>0x2000</c> s'allume.</description></item>
    ///   <item><term>chaque image</term><description>il gronde toutes les neuf images (voix
    ///     <c>0x2d</c>), avance de <c>+0x12</c> en X et <c>+0x14</c> en Z, <b>publie le meme
    ///     deplacement en vitesse</b> par <see cref="AddEntityVelocity_00454580"/> pour que le
    ///     groupe pose dessus suive, et tourne de <c>+0x16 * 0xf0</c>.</description></item>
    ///   <item><term>au choc</term><description>les dix champs reviennent, les <b>deux</b> vitesses
    ///     sont <b>retirees au sort</b> (et non pas seulement inversees, comme chez le cousin), le
    ///     sens de rotation change de signe, la voix <c>0x30</c> claque et <b>sept</b> bouffees
    ///     <c>0x34</c> naissent.</description></item>
    /// </list>
    /// <para>
    /// La <c>0x35</c> une fois sur sept, l'octet de contact <c>+0x09</c> refait a chaque image et la
    /// frappe a <c>0x0a</c> sont identiques au cousin, au bit pres : meme cylindre de
    /// <c>0x3c</c> unites en plan, meme fenetre de hauteur relevee de <c>0x46</c> et large de
    /// <c>0x50</c>.
    /// </para>
    /// <para>
    /// <see cref="ClearEntityVelocity_00460FC0"/> en tete efface <c>+0x50</c>..<c>+0x58</c> avant
    /// tout : la vitesse publiee ne vaut donc que pour l'image en cours.
    /// </para>
    /// </summary>
    private static void EntityHandler_00469100(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);                            // Ghidra: FUN_00460fc0

        int savedGroundX = *(int*)(e + 0x60);
        int savedGroundZ = *(int*)(e + 0x64);
        byte* savedFace = *(byte**)(e + 0x7c);
        uint savedContacts = *(ushort*)(e + 0x90);
        int saved80 = *(int*)(e + 0x80);
        int saved84 = *(int*)(e + 0x84);
        int saved88 = *(int*)(e + 0x88);
        int saved8c = *(int*)(e + 0x8c);

        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = *(short*)(e + 0x14);

            *(short*)(e + 0x12) = (short)((CrtRand() % 0x17 + *(short*)(e + 0x5c)) * RandomSign());
            *(short*)(e + 0x14) = (short)((CrtRand() % 0x17 + *(short*)(e + 0x5c)) * RandomSign());
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x16) = (short)RandomSign();
            *(uint*)e |= 0x2000;
        }

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 9)
        {
            *(short*)(e + 0x98) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2d), 0x400, 0x800, 0, 0);   // Ghidra: FUN_00468080
        }

        *(int*)(e + 0x1c) += *(short*)(e + 0x12) << 0xc;
        *(int*)(e + 0x24) += *(short*)(e + 0x14) << 0xc;
        AddEntityVelocity_00454580(e, *(short*)(e + 0x12) << 0xc, 0,
                                      *(short*)(e + 0x14) << 0xc);   // Ghidra: FUN_00454580

        *(short*)(e + 0x32) += (short)(*(short*)(e + 0x16) * 0xf0);
        if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) = 0;
        if (*(short*)(e + 0x32) < 0) *(short*)(e + 0x32) = 0x1000;

        FinalizeEntityPlacement2((uint*)e);                         // Ghidra: FUN_00533940

        bool blocked = (*(uint*)(e + 0x90) & 0x880) != 0 || (*(byte**)(e + 0x7c))[0x0f] != 0;

        if (blocked)
        {
            *(int*)(e + 0x60) = savedGroundX;
            *(int*)(e + 0x64) = savedGroundZ;
            *(int*)(e + 0x1c) = *(int*)(e + 0x44);
            *(int*)(e + 0x24) = *(int*)(e + 0x4c);
            *(int*)(e + 0x80) = saved80;
            *(byte**)(e + 0x7c) = savedFace;
            *(uint*)(e + 0x90) = savedContacts & 0xffff;
            *(int*)(e + 0x84) = saved84;
            *(int*)(e + 0x88) = saved88;
            *(int*)(e + 0x8c) = saved8c;

            *(short*)(e + 0x12) = (short)((CrtRand() % 0x17 + *(short*)(e + 0x5c)) * RandomSign());
            *(short*)(e + 0x14) = (short)((CrtRand() % 0x17 + *(short*)(e + 0x5c)) * RandomSign());
            *(short*)(e + 0x16) = (short)-*(short*)(e + 0x16);

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x30), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            for (int i = 7; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x34, 0, -1);   // Ghidra: FUN_00468110
        }

        if (CrtRand() % 7 == 1) SpawnEffectNearCamera_00468110(e, 0x35, 0, -1);   // Ghidra: FUN_00468110

        // ------------------------------------------------- l'octet de contact, refait chaque image
        e[9] = 0;
        byte* member = Inp.s_SlotFlags_005e93c0;                    // 0x005e93c0

        for (int slot = 0; slot < 4; slot++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(member + 0x1c) >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
            int dz = (*(int*)(member + 0x24) >> 0xc) - (*(int*)(e + 0x24) >> 0xc);
            int plan = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            int dy = (*(int*)(member + 0x20) >> 0xc) - (*(int*)(e + 0x20) >> 0xc) + 0x46;
            if (dy < 0) dy = -dy;

            if (plan >= 0x3c || dy >= 0x50) continue;

            e[9] |= (byte)(1 << slot);
        }

        StrikePartyMembers_0045DA40(e, 0x0a, 0, 0, 0);              // Ghidra: FUN_0045da40
    }

    /// <summary>
    /// Ghidra: <c>0x00469bc0</c> — sous-zone 0. <b>Absente de <c>Blaze.c</c></b> ; 263 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00469BC0.asm</c>.
    ///
    /// <para>
    /// <b>Le radeau qui s'eloigne et se dissout.</b> Il derive tout seul sur son cap, en ralentissant,
    /// pendant que la camera le suit du regard ; passe un certain age il fume par ses quatre coins,
    /// s'arrete, puis s'efface.
    /// </para>
    /// <list type="table">
    ///   <item><term>a la naissance</term><description>la vitesse <c>+0x5c</c> part a <c>0x1e</c>,
    ///     l'age <c>+0x9a</c> et le compteur de voix <c>+0x98</c> a zero, les deux pas de glissade
    ///     de tangage sont eteints, <c>+0x12</c> retient la hauteur en unites entieres, et les deux
    ///     mots de dessin <c>+0x9c</c> et <c>+0x138</c> prennent les bits
    ///     <c>0x6000</c>.</description></item>
    ///   <item><term>le regard</term><description>chaque image il fait avancer
    ///     <see cref="CameraPitchGlide_0046A5A0"/>, puis, <b>selon que sa hauteur a change ou
    ///     non</b>, il en <b>arme</b> une nouvelle : vers <c>0x200</c> ou <c>0x100</c> tant qu'il
    ///     monte — le choix depend de deux fenetres d'age, <c>0x1e..0x4b</c> et
    ///     <c>0x64..0x85</c> —, et vers <c>0</c> des qu'il s'est stabilise.</description></item>
    ///   <item><term>le flottement</term><description>il se hisse de <c>0xc8000</c> le temps de
    ///     <see cref="FinalizeEntityPlacement2"/>, puis <b>redescend sur le sol trouve</b>
    ///     (<c>+0x20 = +0x64</c>). C'est ainsi qu'il epouse le relief sans jamais s'y
    ///     enfoncer.</description></item>
    ///   <item><term>la voix</term><description>la <c>0x14</c> de la zone toutes les
    ///     <c>0x28 - rand % 15</c> images, hauteur <c>rand % 100 + 0x2ce</c>, gain
    ///     <c>0x1200</c>.</description></item>
    /// </list>
    /// <para>
    /// <b>Le ralentissement a deux regimes.</b> Avant l'age <c>0xd2</c>, <c>+0x5c</c> perd un point
    /// toutes les onze images et s'arrete a un <b>plancher de 8</b>. Apres, il en perd un toutes les
    /// deux images, <b>quatre bouffees de coin partent a chaque cran</b>
    /// (<see cref="SmokePuff_0046A630"/>, coins 1 a 4), et quand la vitesse passe sous zero l'etat
    /// devient 2.
    /// </para>
    /// <para>
    /// La derive vaut <c>sin(+0x32) * +0x5c</c> en Z et <c>cos(+0x32) * +0x5c</c> retranche en X, et
    /// la camera tremble a chaque image de <c>rand % (vitesse / 4)</c> — le quart valant <c>1</c>
    /// des que la vitesse tombe sous <c>4</c>, ce qui evite la division par zero au dernier cran.
    /// </para>
    /// <para>
    /// <b>L'effacement</b>, a l'etat 2 : le bit <c>0x2000</c> du mot global <c>0x005fa7b4</c>
    /// s'eteint, <c>*e</c> prend les bits <c>0x6000</c> que <c>+0x9c</c> et <c>+0x138</c> perdent,
    /// et <c>+0x16c</c> monte de <c>0x46</c> par image jusqu'a <c>0x514</c>, ou l'etat passe a
    /// <c>0x63</c>.
    /// </para>
    /// <para>
    /// <b>La restauration finale ne regarde que le bit <c>0x800</c></b> de <c>+0x90</c>, la ou les
    /// rochers de la meme sous-zone testent <c>0x880</c> : le radeau traverse donc ce qui n'est pas
    /// un mur franc.
    /// </para>
    /// </summary>
    private static void EntityHandler_00469BC0(byte* e)
    {
        byte* savedFace = *(byte**)(e + 0x7c);
        int savedGroundX = *(int*)(e + 0x60);
        int savedGroundZ = *(int*)(e + 0x64);
        uint savedContacts = *(ushort*)(e + 0x90);
        int saved80 = *(int*)(e + 0x80);
        int saved84 = *(int*)(e + 0x84);
        int saved88 = *(int*)(e + 0x88);
        int saved8c = *(int*)(e + 0x8c);

        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0x1e;
            *(short*)(e + 0x9a) = 0;
            *Mem.EntityState(0x0057181c) = 0;                       // Ghidra: DAT_0057181c
            *Mem.EntityState(0x00571820) = 0;                       // Ghidra: DAT_00571820
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(uint*)(e + 0x9c) |= 0x6000;
            *(uint*)(e + 0x138) |= 0x6000;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x9a) += 1;
            CameraPitchGlide_0046A5A0(e, 0, 0);                     // Ghidra: FUN_0046a5a0

            *(short*)(e + 0x98) += 1;
            if (*(short*)(e + 0x98) > 0x28 - CrtRand() % 15)
            {
                *(short*)(e + 0x98) = 0;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x14),
                                       (short)(CrtRand() % 100 + 0x2ce), 0x1200, 0, 0);   // Ghidra: FUN_00468080
            }

            short heldHeight = *(short*)(e + 0x12);
            int height = *(int*)(e + 0x20) >> 0xc;

            if (heldHeight != height)
            {
                short age = *(short*)(e + 0x9a);
                // Les deux fenetres se lisent en cascade, exactement comme le binaire les branche.
                bool wide = (age > 0x1e && age < 0x4b) || (age > 0x64 && age < 0x85);
                CameraPitchGlide_0046A5A0(e, 1, wide ? 0x200 : 0x100);
            }

            if (heldHeight == height) CameraPitchGlide_0046A5A0(e, 1, 0);

            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(int*)(e + 0x20) += 0xc8000;
            FinalizeEntityPlacement2((uint*)e);                     // Ghidra: FUN_00533940
            *(int*)(e + 0x20) = *(int*)(e + 0x64);

            if (*(short*)(e + 0x9a) > 0xd2)
            {
                *(short*)(e + 0x14) += 1;
                if (*(short*)(e + 0x14) > 1)
                {
                    *(short*)(e + 0x14) = 0;
                    SmokePuff_0046A630(e, 1);                       // Ghidra: FUN_0046a630
                    SmokePuff_0046A630(e, 2);
                    SmokePuff_0046A630(e, 3);
                    SmokePuff_0046A630(e, 4);

                    *(int*)(e + 0x5c) -= 1;
                    if (*(int*)(e + 0x5c) < 0)
                    {
                        *(int*)(e + 0x5c) = 0;
                        *(short*)(e + 0x10) = 2;
                    }
                }
            }
            else
            {
                *(short*)(e + 0x14) += 1;
                if (*(short*)(e + 0x14) > 0x0a)
                {
                    *(short*)(e + 0x14) = 0;
                    *(int*)(e + 0x5c) -= 1;
                    if (*(int*)(e + 0x5c) < 8) *(int*)(e + 0x5c) = 8;
                }
            }

            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c);   // Ghidra: 0x00545a1c
            *(int*)(e + 0x1c) -= Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c);        // Ghidra: 0x00545a16

            int quarter = (*(int*)(e + 0x5c) & ~3) == 0 ? 1 : *(int*)(e + 0x5c) >> 2;
            TryPublishCameraEffectNearEntity((uint*)e, 0x3e7, CrtRand() % quarter, 0x3e7, 0, 0);   // Ghidra: FUN_0041be60

            if (CrtRand() % 0x1e == 1) SmokePuff_0046A630(e, SignedMask(CrtRand(), 3) + 1);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(uint*)Mem.Entity(0x005fa7b4) &= ~0x2000u;             // Ghidra: DAT_005fa7b4
            *(short*)(e + 0x16c) += 0x46;
            *(uint*)e |= 0x6000;
            *(uint*)(e + 0x9c) &= 0xffff9fff;
            *(uint*)(e + 0x138) &= 0xffff9fff;

            if (*(short*)(e + 0x16c) >= 0x514)
            {
                *(short*)(e + 0x16c) = 0x514;
                *(short*)(e + 0x10) = 0x63;
            }
        }

        if ((*(uint*)(e + 0x90) & 0x800) == 0) return;

        *(byte**)(e + 0x7c) = savedFace;
        *(int*)(e + 0x60) = savedGroundX;
        *(uint*)(e + 0x90) = savedContacts & 0xffff;
        *(int*)(e + 0x64) = savedGroundZ;
        *(int*)(e + 0x1c) = *(int*)(e + 0x44);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c);
        *(int*)(e + 0x80) = saved80;
        *(int*)(e + 0x84) = saved84;
        *(int*)(e + 0x88) = saved88;
        *(int*)(e + 0x8c) = saved8c;
    }

    /// <summary>
    /// Ghidra: <c>0x00469480</c> — sous-zone 0. <b>Absente de <c>Blaze.c</c></b> ; 363 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00469480.asm</c>.
    ///
    /// <para>
    /// <b>Le rouleau qui grandit, devale le couloir et s'ecrase.</b> Il publie sa progression dans
    /// le mot global <c>0x0057182c</c> — le meme que lisent les pierres qui se detachent de
    /// <see cref="EntityHandler_00469930"/> —, un bit par tranche de <c>200</c> unites parcourues.
    /// Il frappe le groupe a <c>0x0a</c> a chaque image, dans <b>tous</b> ses etats.
    /// </para>
    /// <list type="table">
    ///   <item><term>etat 0 puis 1</term><description>le mot de progression est efface, les trois
    ///     echelles <c>+0x28</c>..<c>+0x2c</c> sont mises a zero, la vitesse <c>+0x5c</c> part a
    ///     <c>0x0a</c> ; l'etat 1 ne dure qu'<b>une seule image</b>.</description></item>
    ///   <item><term>etat 2, il grandit</term><description>les trois echelles montent de
    ///     <c>0xc8</c> par image et le tangage recule de <c>0x1e</c> ; a <c>0x1000</c> d'echelle il
    ///     passe a l'etat 3, avec <c>+0x14 = -1</c> et <c>+0x16 = -0xc8</c>.</description></item>
    ///   <item><term>etat 3, il roule</term><description>le tangage tourne de <c>+0x5c * -10</c>, la
    ///     position avance de <c>sin/cos(+0x32) * +0x5c</c>, et chaque fois que le Z a franchi
    ///     <c>+0x16</c> : le bit <c>(+0x16 + 0x320) / 200</c> s'allume dans <c>0x0057182c</c>,
    ///     <c>+0x16</c> gagne <c>0xc8</c>, la vitesse gagne 1.</description></item>
    ///   <item><term>etat 4 puis 5</term><description>les echelles redescendent de <c>0xc8</c> par
    ///     image jusqu'a zero.</description></item>
    /// </list>
    /// <para>
    /// <b>Le couloir est decrit par des nudges de cap codes en dur</b> : huit valeurs de
    /// <c>+0x16</c> — <c>-0xc8</c>, <c>0</c>, <c>0xc8</c>, <c>0x190</c>, <c>0x258</c>, <c>0x320</c>,
    /// <c>0x3e8</c> et <c>0x4b0</c> — donnent chacune un tour de cap different, et <c>0x708</c>
    /// termine la course. Ce sont les virages du couloir, releves un par un a la main par
    /// l'auteur de 1998.
    /// </para>
    /// <para>
    /// <b>Les trois rebonds</b>, sur <c>+0x14</c> : a <c>-1</c> il compte dix images puis tombe a
    /// <c>0</c> ; a <c>0</c> il descend de <c>+0x12</c> par image, accelerant, jusqu'au plancher
    /// <c>-0x208000</c>, ou la voix <c>0x1a</c> claque et <b>six</b> <c>0x36</c> giclent ; a
    /// <c>1</c> il remonte de <c>+0x12</c> qui decroit de 2 par image, et retouche le plancher a
    /// <c>3</c> ; a <c>±3</c> il rebondit une derniere fois, avec <b>deux</b> <c>0x36</c>.
    /// </para>
    /// <para>
    /// Le dernier rebond <b>tire douze nombres au hasard et les jette</b> (quatre tours de trois
    /// appels) : ce sont les trois tirages de la gerbe, laisses en place alors que le corps ne fait
    /// plus naitre personne. Reproduit tel quel — retirer ces appels decalerait le flux du
    /// generateur pour tout ce qui suit.
    /// </para>
    /// <para>
    /// La gerbe de l'etat 0 disperse sur <c>x ± 0x4b</c>, <c>y - 0x14 - 100</c> et
    /// <c>z - 0x64</c>, avec <b>un decalage supplementaire de <c>-0x50</c> en Z</b> dans la gerbe a
    /// deux du premier rebond — c'est la seule difference entre les deux boucles.
    /// </para>
    /// </summary>
    private static void EntityHandler_00469480(byte* e)
    {
        TryPublishCameraEffectNearEntity((uint*)e, 0x3e7, 0, 0x3e7, 0, 0);   // Ghidra: FUN_0041be60

        if (*(short*)(e + 0x10) == 0)
        {
            *Mem.EntityState(0x0057182c) = 0;                       // Ghidra: DAT_0057182c
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(int*)(e + 0x5c) = 0x0a;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) == 1)
            {
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x10) = 2;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x34) += -0x1e;
            if (*(short*)(e + 0x34) < 0) *(short*)(e + 0x34) += 0x1000;

            *(short*)(e + 0x28) += 0xc8;
            *(short*)(e + 0x2a) += 0xc8;
            *(short*)(e + 0x2c) += 0xc8;

            if (*(short*)(e + 0x28) >= 0x1000)
            {
                *(short*)(e + 0x28) = 0x1000;
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = -1;
                *(short*)(e + 0x16) = -0xc8;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            if (*(short*)(e + 0x14) == 3 || *(short*)(e + 0x14) == -3)
            {
                *(short*)(e + 0x14) = (short)-*(short*)(e + 0x14);
                TryPublishCameraEffectNearEntity(
                    (uint*)e, 0x3e7, *(short*)(e + 0x14) / 3 * 2, 0x3e7, 0, 0);

                for (int i = 2; i != 0; i--) SprayGrit_00469480(e, -0x50);
            }

            // ------------------------------------------------- la course
            *(short*)(e + 0x34) += (short)(*(short*)(e + 0x5c) * -10);
            if (*(short*)(e + 0x34) < 0) *(short*)(e + 0x34) += 0x1000;

            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c);   // Ghidra: 0x00545a1c
            *(int*)(e + 0x1c) -= Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *(int*)(e + 0x5c);        // Ghidra: 0x00545a16

            if (*(int*)(e + 0x24) > *(short*)(e + 0x16) << 0xc)
            {
                // Ghidra: magie 0x51EB851F avec `sar edx,6` — une division signee par 200.
                int bit = (*(short*)(e + 0x16) + 0x320) / 200;
                *(uint*)Mem.EntityState(0x0057182c) |= 1u << bit;

                *(short*)(e + 0x16) += 0xc8;
                if (*(short*)(e + 0x16) == 0xc8) *(short*)(e + 0x32) = 0x400;
                *(int*)(e + 0x5c) += 1;
            }

            // ------------------------------------------------- les virages du couloir
            short mark = *(short*)(e + 0x16);
            if (mark == -0xc8) *(short*)(e + 0x32) += 0x0f;
            if (mark == 0) *(short*)(e + 0x32) += -0x14;
            if (mark == 0xc8) *(short*)(e + 0x32) += -0x19;
            if (mark == 0x190) *(short*)(e + 0x32) += 0x19;
            if (mark == 0x258) *(short*)(e + 0x32) += -0x23;
            if (mark == 0x320) *(short*)(e + 0x32) += 0x23;
            if (mark == 0x3e8) *(short*)(e + 0x32) += -0x19;
            if (mark == 0x4b0) *(short*)(e + 0x32) += 0x19;
            if (mark == 0x708) *(short*)(e + 0x10) = 4;

            // ------------------------------------------------- les rebonds
            if (*(short*)(e + 0x14) == -1)
            {
                *(short*)(e + 0x12) += 1;
                if (*(short*)(e + 0x12) == 0x0a)
                {
                    *(short*)(e + 0x14) = 0;
                    *(short*)(e + 0x12) = 2;
                }
            }

            if (*(short*)(e + 0x14) == 0)
            {
                *(short*)(e + 0x12) += 1;
                *(int*)(e + 0x20) -= *(short*)(e + 0x12) << 0xc;

                if (*(int*)(e + 0x20) <= -0x208000)
                {
                    *(int*)(e + 0x20) = -0x208000;
                    *(short*)(e + 0x14) = 1;
                    TryPublishCameraEffectNearEntity(
                        (uint*)e, 0x3e7, *(short*)(e + 0x12), 0x3e7, 0, 0);
                    TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1a), 0x400, 0x1200, 0, 0);   // Ghidra: FUN_00468080

                    for (int i = 6; i != 0; i--) SprayGrit_00469480(e, 0);
                }
            }

            if (*(short*)(e + 0x14) == 1)
            {
                *(short*)(e + 0x12) += -2;
                *(int*)(e + 0x20) += *(short*)(e + 0x12) << 0xc;

                if (*(short*)(e + 0x12) < 0 && *(int*)(e + 0x20) <= -0x208000)
                {
                    *(int*)(e + 0x20) = -0x208000;
                    *(short*)(e + 0x14) = 3;
                    TryPublishCameraEffectNearEntity(
                        (uint*)e, 0x3e7, *(short*)(e + 0x12), 0x3e7, 0, 0);

                    // Douze tirages jetes : la gerbe a ete retiree, ses tirages sont restes.
                    for (int i = 4; i != 0; i--) { CrtRand(); CrtRand(); CrtRand(); }
                }
            }
        }

        if (*(short*)(e + 0x10) == 4)
        {
            *(short*)(e + 0x28) += -0xc8;
            *(short*)(e + 0x2a) += -0xc8;
            *(short*)(e + 0x2c) += -0xc8;

            if (*(short*)(e + 0x28) <= 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x10) = 5;
            }
        }

        StrikePartyMembers_0045DA40(e, 0x0a, 0, 0, 0);              // Ghidra: FUN_0045da40
    }

    /// <summary>Un grain de la gerbe du rouleau : l'entite se porte a un point tire au hasard,
    /// fait naitre un <c>0x36</c> et revient. <paramref name="depthBias"/> vaut <c>-0x50</c> dans la
    /// gerbe a deux du premier rebond et <c>0</c> dans celle a six du second — c'est leur seule
    /// difference.</summary>
    private static void SprayGrit_00469480(byte* e, int depthBias)
    {
        int ox = CrtRand() % 0x96 - 0x4b;
        int oy = CrtRand() % 0x28 - 0x14 - 100;
        int oz = CrtRand() % 0xc8 - 0x64 + depthBias;

        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) = (ox << 0xc) + savedX;
        *(int*)(e + 0x20) = (oy << 0xc) + savedY;
        *(int*)(e + 0x24) = (oz << 0xc) + savedZ;

        SpawnEffectNearCamera_00468110(e, 0x36, 0, -1);             // Ghidra: FUN_00468110

        *(int*)(e + 0x24) = savedZ;
        *(int*)(e + 0x1c) = savedX;
        *(int*)(e + 0x20) = savedY;
    }
}
