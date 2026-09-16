// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE TIR ET LE BALAYAGE DE MELEE — ce qui part et ce qui rencontre.
//
// Le choix de cible en cone (`PickTargetInCone_00474F00`), les deux balayages qui cherchent qui
// est dans le gabarit (`SweepSceneEntitiesForMeleeHit_00519AF0`,
// `SweepOpposingSideForMeleeHit_00474C70`), et les trois corps que le jet du Goblin fait naitre
// l'un apres l'autre.
//
// Ces fonctions TROUVENT une cible ; elles ne resolvent pas le coup. La resolution est dans
// `HitResolution.cs`, le ciblage general dans `Entities/Targeting.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // LE TIR — les trois corps que le jet du Goblin (FUN_0049DA40, sous-type 8) fait naitre l'un
    // apres l'autre, transcrits le 2026-08-29. Aucun des trois n'est dans Blaze.c ; les deux
    // feuilles qu'ils appellent, si.
    //
    //   0x0047A100  le LANCEUR (sous-type 0x49) : choisit une cible dans un cone, oriente le tir,
    //               fait naitre le projectile et s'eteint dans la meme image.
    //   0x0047A270  la TRAJECTOIRE (sous-type 0x52) : vole, se dirige, touche, puis se PLANTE dans
    //               sa victime et la suit vingt images.
    //   0x00476320  l'IMPACT (sous-type 0xbd) : sept images de sprite et c'est fini.
    //
    // C'EST LE PREMIER SOUS-SYSTEME D'ATTAQUE A DISTANCE DU JEU, et il ne sert pas qu'au Goblin :
    // le Goblin-sorcier (sous-type 9) est le suivant du bloc de la foret.
    // =============================================================================================

    /// <summary>
    /// Ghidra: FUN_00474f00 @ 0x00474f00 — <b>choisir une cible dans un cone</b>. Elle balaie le
    /// <b>camp d'en face</b> : le groupe si l'appelant est une entite de scene, la table de scene si
    /// l'appelant porte le bit <c>0x800000</c> du groupe.
    ///
    /// <para>
    /// Trois filtres, dans cet ordre : l'emplacement doit etre vivant et sans les etats de
    /// <c>0x50002040</c> ; son bloc de statistiques doit partager un bit avec
    /// <paramref name="mask"/> en <c>+0x14</c> ; il ne doit etre ni tenu (<c>+0x74</c>) ni en train
    /// de clignoter (<c>+0x96</c>).
    /// </para>
    /// <para>
    /// <b>Le cone est etroit et le site large</b> : l'ecart de cap doit tenir dans <c>±0x100</c>
    /// (un trente-deuxieme de tour de chaque cote), l'ecart de site dans une bande de <c>0x301</c>
    /// prise a partir de <c>-0x100</c>. La portee maximale est <b>600</b>, et c'est la plus proche
    /// des candidates qui l'emporte.
    /// </para>
    /// </summary>
    /// <returns>L'emplacement retenu, ou <c>null</c>.</returns>
    private static byte* PickTargetInCone_00474F00(byte* shooter, uint mask)
    {
        byte* slot;
        byte* stats;
        int stride;
        int count;

        if ((*(uint*)shooter & 0x800000) == 0)
        {
            slot = Inp.s_SlotFlags_005e93c0;
            stride = 0x2000;
            stats = GetMainMemPtr() + 0xf0100;
            count = s_PartySize_005e9020;
        }
        else
        {
            slot = Mem.Entity(0x005ecaa0);
            stats = s_EntityStatBlocks_005ea560;
            stride = 0x120;
            count = 0x20;
        }

        int x = *(int*)(shooter + 0x1c);
        int y = *(int*)(shooter + 0x20);
        int z = *(int*)(shooter + 0x24);
        short heading = *(short*)(shooter + 0x32);

        byte* found = null;
        int nearest = 0x258;

        for (; count != 0; count--, slot += 0x9c, stats += stride)
        {
            if ((*(uint*)slot & 0x80000000) == 0) continue;
            if ((*(uint*)slot & 0x50002040) != 0) continue;
            if ((*(uint*)(stats + 0x14) & mask) == 0) continue;
            if (*(uint*)(slot + 0x74) != 0) continue;
            if (*(short*)(slot + 0x96) != 0) continue;

            int dx = (*(int*)(slot + 0x1c) - x) >> 0xc;
            int dz = (*(int*)(slot + 0x24) - z) >> 0xc;

            if (((((Winsys3d.Ordinal_457(dx, dz) - heading) & 0xfff) + 0x100) & 0xfff) >= 0x201)
            {
                continue;
            }

            int dy = (*(int*)(slot + 0x20) - y) >> 0xc;
            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            if (((uint)(Winsys3d.Ordinal_457(dy, flat) + 0x100) & 0xfff) >= 0x301) continue;

            int range = (int)Winsys3d.Ordinal_442((uint)(dy * dy + flat * flat));
            if (range < nearest)
            {
                nearest = range;
                found = slot;
            }
        }

        return found;
    }

    /// <summary>
    /// Ghidra: FUN_00519af0 @ 0x00519af0 — <b>le balayage des entites de scene</b>, la sœur de
    /// <see cref="SweepPartyForMeleeHit_00519C30"/> pour le camp d'en face.
    ///
    /// <para>
    /// <b>Elle ne parcourt pas une table mais la grille spatiale</b> : les neuf cases autour de la
    /// position, dans le meme decoupage que <see cref="LinkIntoWorldGrid"/> — d'ou le
    /// <c>0x389f * 0x20</c> du binaire, qui est <c>0x1c5000 / 4</c> moins une rangee, c'est-a-dire
    /// la case de rang <c>Z - 1</c>. Elle rend <b>la premiere</b> entite acceptee, pas la plus
    /// proche : la grille sert d'index, pas de tri.
    /// </para>
    /// <para>
    /// La parade est ici aussi, sur le bit <c>0x10</c> de l'entite au lieu du bit 4 d'un membre du
    /// groupe, et elle remet <c>DAT_005de0b8</c> a zero apres avoir joue son son.
    /// </para>
    /// </summary>
    private static byte* SweepSceneEntitiesForMeleeHit_00519AF0(
        byte* entity, int* position, short* probe, uint flags)
    {
        byte* mainMem = GetMainMemPtr();

        uint column = (uint)(((position[0] >> 0xc) + 0x2000) >> 9) - 1;
        uint row = (uint)((((position[2] >> 0xc) + 0x2000) >> 9) + 0x389f) * 0x20;

        for (int bands = 3; ; bands--, row += 0x20)
        {
            uint cell = column;

            for (int step = 3; step != 0; step--, cell++)
            {
                if (cell >= 0x20 || row >= 0x71800) continue;

                for (byte* found = *(byte**)(mainMem + (cell + row) * 4);
                     found != null;
                     found = *(byte**)(found + 0x68))
                {
                    if ((*(uint*)found & 0x400000) == 0) continue;
                    if ((*(uint*)found & 0x50102040) != 0) continue;
                    if (*(short*)(found + 0x96) != 0) continue;
                    if (!TestMeleeReach_00535A90(position, found, probe, entity)) continue;

                    if ((*(uint*)found & 0x10) != 0 && (flags & 0x800) == 0
                        && (uint)((*(short*)(found + 0x32) - *(short*)((byte*)position + 0x16)
                                   - *probe - 0x400) & 0xfff) < 0x800)
                    {
                        Aud.PlayPositionalSound(8, 0x800, 0x1000, (int*)(found + 0x1c));
                        *Mem.Camera(0x005de0b8) = 0;
                        return null;
                    }

                    *Mem.Camera(0x005de0b8) = 1;
                    return found;
                }
            }

            // Ghidra: `uVar5 = uVar6 - 2` — la colonne repart de X - 1 pour la rangee suivante.
            column = cell - 3;

            if (bands == 1)
            {
                *Mem.Camera(0x005de0b8) = 0;
                return null;
            }
        }
    }

    /// <summary>Ghidra: FUN_00474c70 @ 0x00474c70 — <b>balayer le camp d'en face</b>, l'aiguillage
    /// d'une ligne entre les deux balayages selon que l'appelant appartient au groupe
    /// (<c>0x800000</c>) ou a la scene. Les deux receveuses ont la meme signature, et le site
    /// d'appel pousse les memes quatre arguments dans les deux branches.</summary>
    private static byte* SweepOpposingSideForMeleeHit_00474C70(byte* entity, short* probe)
    {
        if ((*(uint*)entity & 0x800000) != 0)
        {
            return SweepSceneEntitiesForMeleeHit_00519AF0(entity, (int*)(entity + 0x1c), probe, 0);
        }

        return SweepPartyForMeleeHit_00519C30(entity, (int*)(entity + 0x1c), probe, 0);
    }

    /// <summary>
    /// Ghidra: <c>0x0047a100</c> — <b>l'entree 73 de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 0x170 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_0047A100.asm</c>.
    ///
    /// <para>
    /// <b>C'est le LANCEUR</b>, l'effet <c>0x49</c> que le geste de jet du Goblin fait naitre a la
    /// septieme image. Il ne vit qu'une image : il choisit une cible, oriente le tir, fait naitre le
    /// projectile et <b>s'eteint</b> (<c>*e = 0</c>) avant de rendre la main.
    /// </para>
    /// <para>
    /// <b>Cap ET site.</b> <c>+0x32</c> recoit l'azimut de la cible, <c>+0x34</c> son elevation —
    /// c'est la premiere fois qu'une entite reconstruite vise en trois dimensions. Les trois
    /// vitesses en decoulent : <c>+0x50</c> et <c>+0x58</c> par le produit du sinus ou du cosinus
    /// d'azimut avec le cosinus de site, <c>+0x54</c> par le sinus de site seul. Sans cible, le
    /// site vaut <c>0x100</c> et le tir part droit devant.
    /// </para>
    /// <para>
    /// <b>Deux idiomes a ne pas normaliser.</b> Le <c>sar eax,5</c> suivi de <c>and al,80h</c> est
    /// un decalage de cinq bits puis un arrondi vers le bas au multiple de <c>0x80</c> — il ne
    /// touche que les sept bits bas. Et l'elevation ajoute <c>distance / 4</c> a l'angle : un tir
    /// long part plus haut, ce qui est une garde-au-but grossiere.
    /// </para>
    /// <para>
    /// Le depart est releve a la hauteur de bulle de la bete
    /// (<see cref="StatusBubbleHeight_00538BC0"/>, arrondie a un multiple de deux puis decalee de
    /// onze bits) : le projectile part de la main, pas des pieds.
    /// </para>
    /// </summary>
    private static void EntityHandler_0047A100(byte* e)
    {
        byte* target = PickTargetInCone_00474F00(e, 0xffffffff);

        *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & ~4) | 2);
        *(uint*)e |= 2;

        *(short*)(e + 0x28) = 0x380;
        *(short*)(e + 0x2a) = 0x200;
        *(short*)(e + 0x2c) = 0x200;
        e[4] = 0x3a;                                        // le modele du projectile

        if (target != null)
        {
            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dy = (*(int*)(target + 0x20) - *(int*)(e + 0x20)) >> 0xc;
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
            short course = *(short*)(e + 0x32);

            // L'azimut est repris a partir du cap courant, l'ecart etant borne a un tour.
            *(short*)(e + 0x32) = (short)(((Winsys3d.Ordinal_457(dx, dz) - course) & 0xfff) + course);

            // Ghidra: `sar edi,2 ; add eax,edi` — l'elevation monte d'un quart de la distance.
            *(short*)(e + 0x34) = (short)(Winsys3d.Ordinal_457(dy, flat) + (flat >> 2));

            *(byte**)(e + 0x78) = target;
        }
        else
        {
            *(short*)(e + 0x34) = 0x100;
            *(uint*)(e + 0x78) = 0;
        }

        // Ghidra: LAB_0047a1cf
        int pitch = *(short*)(e + 0x34);
        int yaw = *(short*)(e + 0x32);

        *(short*)(e + 0x9a) = 0x7fff;                       // le rappel que la trajectoire amortit

        int rise = Winsys3d.GetCosTableEntry(pitch);

        *(int*)(e + 0x50) = (((int)Winsys3d.GetSinTableEntry(yaw) * rise) >> 5) & ~0x7f;
        *(int*)(e + 0x54) = (int)Winsys3d.GetSinTableEntry(pitch) << 7;
        *(int*)(e + 0x58) = ((Winsys3d.GetCosTableEntry(yaw) * rise) >> 5) & ~0x7f;

        byte* owner = *(byte**)(e + 0x6c);
        int muzzle = (StatusBubbleHeight_00538BC0((uint*)owner) & ~1) << 0x0b;

        *(short*)(e + 0x32) += 0x400;
        *(byte**)(e + 0x74) = owner;                        // qui repondra des degats
        *(int*)(e + 0x20) += muzzle;

        SpawnCastEntity_0047AAE0(e, 0x52, 1, 8);
        Aud.PlayPositionalSound(0x1f, 0x2000, 0x1200, (int*)(e + 0x1c));

        *(uint*)e = 0;                                      // le lanceur ne vit qu'une image
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551a08</c> — <b>les sept images de l'eclaboussure</b> de
    /// <see cref="EntityHandler_00476320"/>, huit octets chacune. Sept, parce que le corps compte
    /// <c>+0x40</c> jusqu'a 7 avant de s'eteindre et que
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> le pose a zero. La huitieme entree
    /// (<c>0x00551a40</c>) n'a plus la meme forme — son cinquieme octet vaut 0 et son huitieme 1 —,
    /// ce qui borne la sequence.
    /// </summary>
    private static readonly byte[] s_SplashDescriptors_00551A08 =
    [
        0xc0, 0xb8, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a08
        0xd8, 0xb8, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a10
        0x60, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a18
        0x78, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a20
        0x90, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a28
        0xa8, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a30
        0xc0, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a38
    ];

    private static byte* s_SplashDescriptorStorage;

    /// <summary>Les sept images en memoire native : l'eclaboussure les garde par pointeur
    /// (<c>+0x3c</c>) et choisit la sienne par <c>+0x40</c>.</summary>
    private static byte* SplashDescriptors()
    {
        if (s_SplashDescriptorStorage == null)
        {
            s_SplashDescriptorStorage =
                (byte*)NativeMemory.Alloc((nuint)s_SplashDescriptors_00551A08.Length);

            for (int i = 0; i < s_SplashDescriptors_00551A08.Length; i++)
            {
                s_SplashDescriptorStorage[i] = s_SplashDescriptors_00551A08[i];
            }
        }

        return s_SplashDescriptorStorage;
    }

    /// <summary>
    /// Ghidra: <c>0x0047a270</c> — <b>l'entree 82 de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>, 0x360 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_0047A270.asm</c>.
    ///
    /// <para>
    /// <b>C'est la TRAJECTOIRE</b>, et elle a deux vies separees par le bit <c>0x10000000</c> :
    /// avant, elle vole ; apres, elle est <b>plantee</b> dans quelque chose et le suit.
    /// </para>
    /// <para>
    /// <b>Plantee</b> (<c>0x10000000</c> pose) : elle clignote une image sur deux par le bit
    /// <c>0x4000</c>, recopie la position de sa victime augmentee de l'ecart fige a l'impact
    /// (<c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>), et disparait a la vingtieme image.
    /// </para>
    /// <para>
    /// <b>En vol</b>, elle fait trois choses dans cet ordre :
    /// </para>
    /// <list type="number">
    /// <item><b>elle se dirige</b>, si elle a gardé une cible en <c>+0x78</c> : l'ecart de cap
    /// decide d'un virage de <c>±0x20</c> — jamais plus —, et les deux vitesses horizontales sont
    /// refaites a la meme norme. Un tir suit sa cible sans jamais la rattraper d'un coup ;</item>
    /// <item><b>elle avance en HUIT sous-pas</b> d'un huitieme de vitesse chacun, en appelant le
    /// placement a chaque sous-pas et en accumulant les drapeaux de collision de <c>+0x90</c>.
    /// C'est ce qui empeche un projectile rapide de traverser un mur ; l'avant-dernier sous-pas
    /// efface trois bits de l'accumulateur ;</item>
    /// <item><b>elle se reoriente sur le deplacement reellement obtenu</b> — pas sur la consigne —,
    /// le site rejoignant sa valeur par quarts.</item>
    /// </list>
    /// <para>
    /// <b>Elle touche de deux facons.</b> Le decor : les drapeaux accumules portent
    /// <c>0xa000</c>, ou bien elle est passee sous le sol de <c>+0x60</c> ; elle s'y plante alors et
    /// laisse l'eclaboussure <c>0xbd</c>. Une creature : le balayage
    /// <see cref="SweepOpposingSideForMeleeHit_00474C70"/>, et c'est <b>son proprietaire</b>
    /// (<c>+0x74</c>, pose par le lanceur) qui est passe a <c>FUN_00539b80</c> — les degats sont
    /// ceux du Goblin, pas ceux de la fleche.
    /// </para>
    /// <para>
    /// Sa sonde est batie sur la pile : <c>{-0x400, -0x80, 0x32, 0x32, 0, 0x80}</c>. Les deux
    /// premiers termes sont negatifs, ce qu'aucun gabarit de bete ne fait.
    /// </para>
    /// </summary>
    private static void EntityHandler_0047A270(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = unchecked((short)0xfc00);
        probe[1] = unchecked((short)0xff80);
        probe[2] = 0x32;
        probe[3] = 0x32;
        probe[4] = 0;
        probe[5] = 0x80;

        uint flags = *(uint*)e;

        // ================================================================ plantee
        if ((flags & 0x10000000) != 0)
        {
            *(short*)(e + 0x10) += 1;
            short age = *(short*)(e + 0x10);

            // Une image sur deux, le bit de clignotement.
            *(uint*)e = (age & 1) != 0 ? flags | 0x4000 : flags & 0xffffbfff;

            byte* host = *(byte**)(e + 0x78);
            if (host != null)
            {
                *(int*)(e + 0x1c) = *(int*)(e + 0x44) + *(int*)(host + 0x1c);
                *(int*)(e + 0x20) = *(int*)(e + 0x48) + *(int*)(host + 0x20);
                *(int*)(e + 0x24) = *(int*)(e + 0x4c) + *(int*)(host + 0x24);
            }

            if (age >= 0x14) *(uint*)e = 0;
            return;
        }

        // ================================================================ en vol
        byte* aim = *(byte**)(e + 0x78);

        int startX = *(int*)(e + 0x1c);
        int startY = *(int*)(e + 0x20);
        int startZ = *(int*)(e + 0x24);

        *(uint*)e = (flags & 0xfffff7ff) | 0x680;

        if (aim != null)
        {
            // Ghidra: 0x0047a32c — la norme horizontale est conservee, seul le cap tourne.
            int vx = *(int*)(e + 0x50) >> 0xc;
            int vz = *(int*)(e + 0x58) >> 0xc;
            int pace = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));

            int toX = (*(int*)(aim + 0x1c) - startX) >> 0xc;
            int toZ = (*(int*)(aim + 0x24) - startZ) >> 0xc;

            int off = (Winsys3d.Ordinal_457(toX, toZ) - 0x400 - *(short*)(e + 0x32)) & 0xfff;

            // Ghidra: `setge cl ; dec ecx ; and ecx,0FFFFFFC0h ; add ecx,20h` — un virage de
            // +0x20 ou de -0x20, jamais autre chose.
            *(short*)(e + 0x32) += (short)(off >= 0x800 ? 0x20 : -0x20);

            int course = *(short*)(e + 0x32) - 0x400;
            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * pace;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(course) * pace;
        }

        // Ghidra: LAB_0047a3b0 — meme amortissement que l'integration des betes, mais le rappel de
        // +0x9a compte au huitieme et non au deux-cent-cinquante-sixieme : ca tombe vite.
        int bob = *(short*)(e + 0x9a);

        int hx = *(int*)(e + 0x50);
        *(int*)(e + 0x50) = hx - (hx >> 3);

        int hy = *(int*)(e + 0x54);
        *(int*)(e + 0x54) = (bob >> 3) - (hy >> 3) + hy - bob;

        int hz = *(int*)(e + 0x58);
        *(int*)(e + 0x58) = hz - (hz >> 3);

        *(uint*)e &= 0xdfffffff;

        // Huit sous-pas : c'est ce qui empeche de traverser un mur en une image.
        uint hits = 0;
        for (int sub = 8; sub != 0; sub--)
        {
            *(int*)(e + 0x1c) += *(int*)(e + 0x50) >> 3;
            *(int*)(e + 0x20) += *(int*)(e + 0x54) >> 3;
            *(int*)(e + 0x24) += *(int*)(e + 0x58) >> 3;

            FinalizeEntityPlacement2((uint*)e);

            hits |= *(uint*)(e + 0x90);
            if (sub == 2) hits &= 0xfffff8ff;
        }

        *(uint*)(e + 0x90) = hits;

        // Ghidra: 0x0047a455 — le cap et le site sont refaits sur le deplacement OBTENU.
        int wentX = (*(int*)(e + 0x1c) - startX) >> 0xc;
        int wentY = (*(int*)(e + 0x20) - startY) >> 0xc;
        int wentZ = (*(int*)(e + 0x24) - startZ) >> 0xc;

        int span = (int)Winsys3d.Ordinal_442((uint)(wentX * wentX + wentZ * wentZ));

        short pitch = *(short*)(e + 0x34);
        *(short*)(e + 0x32) = (short)(Winsys3d.Ordinal_457(wentX, wentZ) + 0x400);
        *(short*)(e + 0x34) = (short)(pitch + ((Winsys3d.Ordinal_457(wentY, span) - pitch) >> 2));

        // ---------------------------------------------------------------- le decor
        bool misplaced = (hits & 0xa000) != 0;   // Ghidra: `test ah,0A0h ; jne` — un placement invalide interdit la plantee

        if (!misplaced && ((hits & 0x802) != 0 || *(int*)(e + 0x20) <= *(int*)(e + 0x60)))
        {
            // Ghidra: LAB_0047a4c9 — elle ne s'enfonce pas : la hauteur est ramenee au sol.
            if (*(int*)(e + 0x20) <= *(int*)(e + 0x60)) *(int*)(e + 0x20) = *(int*)(e + 0x60);

            *(uint*)e = (*(uint*)e & 0xffffff7f) | 0x10000000;
            *(uint*)(e + 0x78) = 0;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), SplashDescriptors(), 0xbd, 0, 4);
        }

        // ---------------------------------------------------------------- une creature
        // Ghidra: LAB_0047a505 — le balayage a lieu MEME quand elle vient de se planter dans le
        // decor : le binaire ne saute pas ce bloc.
        byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);

        if (victim != null)
        {
            if (*Mem.Camera(0x005de0b8) != 0)
            {
                // C'est le PROPRIETAIRE qui frappe, pas le projectile : +0x74 a ete pose par le
                // lanceur, et c'est lui que FUN_00539b80 lit pour l'attaque et l'experience.
                ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);

                int course = *(short*)(e + 0x32) - 0x400;
                *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(course) << 4;
                *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(course) << 4;

                *(ushort*)(victim + 0x96) = 0x10;
                *(uint*)victim |= 0x40;

                Aud.PlayPositionalSound(3, 0x400, 0x1800, (int*)(e + 0x1c));
            }

            // Ghidra: LAB_0047a586 — touchee ou parée, elle se plante et retient l'ecart.
            *(uint*)e = (*(uint*)e & 0xffffff7f) | 0x10000000;
            *(byte**)(e + 0x78) = victim;

            *(int*)(e + 0x44) = *(int*)(e + 0x1c) - *(int*)(victim + 0x1c);
            *(int*)(e + 0x48) = *(int*)(e + 0x20) - *(int*)(victim + 0x20);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24) - *(int*)(victim + 0x24);
        }

        // Ghidra: LAB_0047a5b7 — sortie de zone : le placement l'a marquee, elle disparait.
        if ((*(uint*)e & 0x60000000) != 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00476320 @ 0x00476320 — <b>l'entree 189 de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, 45 octets : l'<b>eclaboussure</b> que la
    /// trajectoire laisse la ou elle se plante.
    ///
    /// <para>
    /// Elle ne bouge pas et ne compte rien d'autre que ses images : la premiere pose l'echelle
    /// <c>0x1000</c> sur deux axes, et <c>+0x40</c> — l'indice d'image que
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> a mis a zero — monte d'un par image jusqu'a
    /// sept, ou l'entite s'eteint. Sept images, sept enregistrements dans
    /// <see cref="s_SplashDescriptors_00551A08"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476320(byte* e)
    {
        if (*(short*)(e + 0x10) != 0)
        {
            e[0x40] += 1;
            if (e[0x40] == 7) *(uint*)e = 0;
            return;
        }

        *(uint*)(e + 0x28) = 0x10001000;
        *(short*)(e + 0x10) = 1;
    }
}
