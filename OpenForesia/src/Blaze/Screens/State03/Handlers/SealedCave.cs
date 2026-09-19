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
}
