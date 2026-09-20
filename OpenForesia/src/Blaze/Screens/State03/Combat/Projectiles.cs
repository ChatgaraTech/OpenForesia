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

    // =============================================================================================
    // LA BOUFFEE QUI BLESSE — un meme corps emis trois fois, dans trois zones, transcrit le
    // 2026-09-16 d'apres le desassembleur. Aucune des trois emissions n'est dans Blaze.c.
    //
    //   0x00407D70  la mine abandonnee, sous-zones 5 a 8      planche 0x0054f430
    //   0x0040D6C0  la montagne du dragon de feu, sous-zone 0 planche 0x0054f6d8
    //   0x00439D30  la grotte scellee, sous-zones 6, 7         planche 0x005510a8
    //
    // Toutes trois au creneau 31 de la table d'effets 0x0056b2c8. Les desassemblages, cibles de
    // saut ramenees au debut, ne different que par l'adresse de planche, et les trois planches
    // portent les memes quatre enregistrements.
    //
    // Elle est ici et non dans Effects/ parce qu'elle FRAPPE : elle resout un coup sur les membres
    // du groupe, et ecrit la fiche de modele de son proprietaire pendant ce temps.
    // =============================================================================================

    /// <summary>
    /// Ghidra: FUN_004375a0 @ 0x004375a0 — <b>le membre du groupe fait-il face a ce point en
    /// garde ?</b> Decompilee dans <c>Blaze.c</c>, mais <b>aucun appelant n'y figure</b> : ses
    /// trois emplois sont les bouffees ci-dessous, toutes hors decompilation. Relevee aussi au
    /// desassembleur, <c>Legacy/Raw/Blaze/FUN_004375A0.asm</c>, pour la valeur de retour.
    ///
    /// <para>
    /// Rend <b>1</b> quand les quatre conditions tiennent : l'entite est un membre du groupe
    /// (<c>0x800000</c>, emplacement 0 a 3), sa posture de fiche <c>+0x150 &amp; 7</c> vaut 0 ou 6
    /// — debout —, son bit de drapeau <c>4</c> est leve, et le point <c>(x, z)</c> tombe dans le
    /// secteur qu'elle regarde : l'ecart entre son cap <c>+0x32</c> et la direction du point, sur
    /// douze bits, est dans <c>]0, 1000[</c> ou dans <c>]0xc18, 0x1000[</c>. Un secteur d'environ
    /// <c>±88°</c>, <b>ecart nul exclu</b> — un point exactement droit devant ne compte pas.
    /// </para>
    /// <para>
    /// <b>La hauteur n'entre pas dans le test</b> : le troisieme parametre n'est jamais lu.
    /// </para>
    /// <para>
    /// Le binaire ne rend que <c>ax</c> — 1 ou 0 ; Ghidra, qui lit <c>eax</c>, y voit des bits hauts
    /// residuels, que l'appelant ne consulte pas (<c>test ax,ax</c>).
    /// </para>
    /// </summary>
    private static short IsGuardingToward_004375A0(byte* member, int x, int y, int z)
    {
        if ((*(uint*)member & 0x800000) == 0) return 0;

        byte slot = member[8];
        if (slot >= 4) return 0;

        byte stance = (byte)(*(GetMainMemPtr() + (slot + 0x78) * 0x2000 + 0x150) & 7);
        if (stance != 0 && stance != 6) return 0;
        if ((*(uint*)member & 4) == 0) return 0;

        int dx = (x - *(int*)(member + 0x1c)) >> 0xc;
        int dz = (z - *(int*)(member + 0x24)) >> 0xc;
        int offset = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(member + 0x32)) & 0xfff;

        if ((offset < 1000 && offset > 0) || (offset < 0x1000 && offset > 0xc18)) return 1;
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f430</c> — <b>la planche de la bouffee dans la mine</b>, quatre
    /// tuiles <c>0x20 x 0x20</c> sur la ligne <c>y = 0x88</c>, page <c>01 02 10 00</c>.
    ///
    /// <para>
    /// <b>Quatre enregistrements, et l'enregistrement 0 n'est jamais lu</b> : l'image nait entre 1
    /// et 3 et reboucle de 3 sur 1. Il est releve quand meme — la plage est ce que la donnee
    /// porte, pas ce qu'un corps en lit. Bornee en bas par la tuile <c>0x0f x 0x0f</c> de
    /// <c>0x0054f428</c> (page <c>09</c>), en haut par <see cref="s_Descriptors_0054F450"/>, une
    /// planche deja relevee.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F430 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f430
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f438
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f440
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f448
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f6d8</c> — la meme planche pour la montagne du dragon de feu, octet
    /// pour octet. Bornee en bas par la tuile <c>8 x 8</c> de <c>0x0054f6d0</c>, en haut par le
    /// motif <c>00 00 08 08 01 03</c> de <c>0x0054f6f8</c>.
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F6D8 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f6d8
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f6e0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f6e8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f6f0
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005510a8</c> — la meme planche pour la grotte scellee, octet pour
    /// octet. Bornee en bas par la tuile <c>0x18 x 0x18</c> de <c>0x005510a0</c>, en haut par celle
    /// de <c>0x005510c8</c>.
    /// </summary>
    private static readonly byte[] s_Descriptors_005510A8 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x005510a8
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x005510b0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x005510b8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x005510c0
    ];

    private static byte* s_Descriptors0054F430Storage;
    private static byte* s_Descriptors0054F6D8Storage;
    private static byte* s_Descriptors005510A8Storage;

    private static byte* Descriptors0054F430()
        => NativeCopy(s_Descriptors_0054F430, ref s_Descriptors0054F430Storage);

    private static byte* Descriptors0054F6D8()
        => NativeCopy(s_Descriptors_0054F6D8, ref s_Descriptors0054F6D8Storage);

    private static byte* Descriptors005510A8()
        => NativeCopy(s_Descriptors_005510A8, ref s_Descriptors005510A8Storage);

    /// <summary>
    /// Ghidra: <c>0x00407d70</c> — <b>la bouffee qui blesse</b>, creneau 31 de la table d'effets
    /// <c>0x0056b2c8</c>, sous-zones 5 a 8 de la mine abandonnee. Absente de <c>Blaze.c</c> ;
    /// 189 instructions, <c>Legacy/Raw/Blaze/FUN_00407D70.asm</c>. Le corps est
    /// <see cref="TickHarmfulPuff_00407D70"/>.
    /// </summary>
    private static void EntityHandler_00407D70(byte* e)
        => TickHarmfulPuff_00407D70(e, Descriptors0054F430());   // Ghidra: &DAT_0054f430

    /// <summary>Ghidra: <c>0x0040d6c0</c> — le meme corps pour la montagne du dragon de feu,
    /// sous-zone 0. Seule difference : <c>mov dword [esi+3Ch],54F6D8h</c>.</summary>
    private static void EntityHandler_0040D6C0(byte* e)
        => TickHarmfulPuff_00407D70(e, Descriptors0054F6D8());   // Ghidra: &DAT_0054f6d8

    /// <summary>Ghidra: <c>0x00439d30</c> — le meme corps pour la grotte scellee,
    /// sous-zones 6 et 7. Seule difference : <c>mov dword [esi+3Ch],5510A8h</c>.</summary>
    private static void EntityHandler_00439D30(byte* e)
        => TickHarmfulPuff_00407D70(e, Descriptors005510A8());   // Ghidra: &DAT_005510a8

    /// <summary>
    /// Le corps partage par <see cref="EntityHandler_00407D70"/>, <see cref="EntityHandler_0040D6C0"/>
    /// et <see cref="EntityHandler_00439D30"/>. Le role a l'ecran n'est pas etabli — qui la fait
    /// naitre n'est pas releve ; le nom dit ce que fait le corps.
    ///
    /// <para>
    /// <b>Elle file droit et grossit en s'eteignant.</b> A la naissance elle fige son point de
    /// depart en <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>. A chaque image sa position est ce point
    /// plus le cap <c>+0x32</c> — recu de qui la fait naitre — multiplie par une distance
    /// <c>+0x12</c> qui gagne <c>0x14</c> : un trajet rectiligne a vitesse constante, <b>sinus sur
    /// X, cosinus sur Z</b>. La hauteur, elle, avance de <c>-0xc000</c> par image, independamment.
    /// L'echelle part de <c>0x5dc</c> et gagne 100 par image ; la teinte part de <c>0xb4</c> et en
    /// perd 7.
    /// </para>
    /// <para>
    /// <b>Trois morts</b>, toutes differees au passage a l'etat 2 et executees en fin de tour : la
    /// teinte rouge a zero (26 images), une collision de placement (<c>+0x90 &amp; 0x8802</c>), ou
    /// une hauteur qui atteint le plancher <c>+0x60</c>. Le balayage du groupe a lieu <b>meme
    /// l'image ou elle passe a l'etat 2</b> : le binaire ne le saute pas.
    /// </para>
    /// <para>
    /// <b>Son image nait entre 1 et 3 et reboucle de 3 sur 1</b>, toutes les cinq images. La
    /// naissance recopie aussi l'octet <c>+0x9a</c> dans <c>+0x0a</c>.
    /// </para>
    /// <para>
    /// <b>Elle frappe les quatre membres du groupe.</b> Un membre vivant (<c>0x80000000</c>), ni
    /// <c>0x50000000</c>, de <c>+0x12</c> nul, sans clignotement de degat en cours (<c>+0x96</c>),
    /// a moins de <c>0x50</c> unites — la hauteur relevee de <c>0x3c</c> avant le calcul — est
    /// touche. Deux issues :
    /// </para>
    /// <list type="table">
    ///   <item><term>en garde</term><description><see cref="IsGuardingToward_004375A0"/> rend 1 :
    ///     <b>la bouffee s'eteint</b> et le membre n'a rien. Le balayage continue quand
    ///     meme sur les membres suivants.</description></item>
    ///   <item><term>sinon</term><description>le coup est resolu au nom du <b>proprietaire</b>
    ///     <c>+0x74</c>, dont l'attaque du bloc de modele <c>+0x30</c> est ramenee aux trois quarts
    ///     le temps de l'appel puis restauree. Le clignotement <c>0x10</c> et le bit
    ///     <c>0x40</c> sont poses <b>apres</b> la resolution, pas avant comme au corps a corps.
    ///     </description></item>
    /// </list>
    /// <para>
    /// <b>Le proprietaire est lu a l'entree du tour</b>, avant tout le reste, et c'est cette copie
    /// qui sert au coup. Rien ne le re-lit entre-temps.
    /// </para>
    /// </summary>
    private static void TickHarmfulPuff_00407D70(byte* e, byte* plate)
    {
        uint* owner = *(uint**)(e + 0x74);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;
            int frame = CrtRand() % 3;
            e[0x0b] = 0;

            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xb4;
            *(short*)(e + 0x28) = 0x5dc;
            *(short*)(e + 0x2a) = 0x5dc;

            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 5;
            e[0x40] = (byte)(frame + 1);
            e[0x0a] = e[0x9a];
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short yaw = *(short*)(e + 0x32);
            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * *(short*)(e + 0x12) + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * *(short*)(e + 0x12)
                + *(int*)(e + 0x58);
            *(int*)(e + 0x20) += -0xc000;
            *(short*)(e + 0x28) += 100;
            *(short*)(e + 0x2a) += 100;

            BrightenEntityTint_00404DE0(e, -7, -7, -7);
            if (e[0x38] == 0) *(short*)(e + 0x10) = 2;

            FinalizeEntityPlacement2((uint*)e);                              // Ghidra: FUN_00533940
            if ((*(uint*)(e + 0x90) & 0x8802) != 0) *(short*)(e + 0x10) = 2;
            if (*(int*)(e + 0x60) >= *(int*)(e + 0x20)) *(short*)(e + 0x10) = 2;

            *(short*)(e + 0x14) -= 1;
            *(short*)(e + 0x12) += 0x14;
            if (*(short*)(e + 0x14) <= 0)
            {
                *(short*)(e + 0x14) = 5;
                e[0x40]++;
                if (e[0x40] == 4) e[0x40] = 1;
            }

            // ------------------------------------------------------------ les quatre membres
            byte* member = Inp.s_SlotFlags_005e93c0;                         // 0x005e93c0
            for (int i = 4; i != 0; i--, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0) continue;
                if ((flags & 0x50000000) != 0) continue;
                if (*(short*)(member + 0x12) != 0) continue;

                int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dy = (*(int*)(member + 0x20) - *(int*)(e + 0x20) + 0x3c000) >> 0xc;
                int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                // Ghidra: FUN_00545a10 — winsys3d Ordinal_442, le SquareRoot0 de libgte. Appele
                // AVANT le test du clignotement : l'ordre du binaire.
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

                if (*(short*)(member + 0x96) != 0) continue;
                if (distance >= 0x50) continue;

                if (IsGuardingToward_004375A0(
                        member, *(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24)) != 0)
                {
                    *(uint*)e &= 0x7fffffff;                                 // paree : elle s'eteint
                    continue;
                }

                ResolveEntityBlocks((byte*)owner, out _, out _, out byte* model);   // Ghidra: FUN_00538980

                short power = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(power - (power >> 2));
                ResolveMeleeHit_00539B80(owner, (uint*)member);
                *(short*)(model + 0x30) = power;

                *(ushort*)(member + 0x96) = 0x10;
                *(uint*)member |= 0x40;
            }
        }

        if (*(short*)(e + 0x10) == 2) *(uint*)e &= 0x7fffffff;
    }

    // =============================================================================================
    // L'ONDE QUI AFFLIGE — le balayage en anneau, transcrit le 2026-09-16 d'apres le desassembleur.
    // Deux emissions, aucune dans Blaze.c :
    //
    //   0x00409F40  la mine abandonnee, sous-zones 3, 4 et 9    marge 2000
    //   0x0040CEA0  la montagne du dragon de feu, sous-zone 0  marge 3000
    //
    // Creneau 30 de la table d'effets 0x0056b1c8. Chacune porte sa propre douzaine d'enveloppes
    // (0x0040A340.., 0x0040D2A0..), qui appellent les memes appliqueurs dans le meme ordre, et sa
    // propre table de sauts — une seule immediate de logique les separe, la marge.
    //
    // C'est la cousine de l'onde de la foret (EntityHandler_004134C0, Effects/ForestParticlesA.cs),
    // qui balaie le groupe de la meme facon mais ne fait que MARQUER. Celle-ci applique un effet
    // d'etat : elle quitte Effects/ pour cela.
    // =============================================================================================

    /// <summary>Ghidra: <c>0x00409f40</c> — l'onde de la mine, sous-zones 3, 4 et 9. Absente de
    /// <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_00409F40.asm</c>. Voir
    /// <see cref="TickAfflictingRing_00409F40"/>.</summary>
    private static void EntityHandler_00409F40(byte* e) => TickAfflictingRing_00409F40(e, 2000);

    /// <summary>Ghidra: <c>0x0040cea0</c> — l'onde de la montagne du dragon de feu, sous-zone 0.
    /// Absente de <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_0040CEA0.asm</c>. Seule difference de
    /// logique avec <see cref="EntityHandler_00409F40"/> : <c>add ecx,0FFFFF448h</c>, une marge de
    /// 3000 au lieu de 2000 — celle de l'onde de la foret.</summary>
    private static void EntityHandler_0040CEA0(byte* e) => TickAfflictingRing_00409F40(e, 3000);

    /// <summary>
    /// Le corps partage par <see cref="EntityHandler_00409F40"/> et
    /// <see cref="EntityHandler_0040CEA0"/>. Deux effets dans un corps, choisis par <c>+0x5c</c>
    /// qui ne change jamais — exactement comme <see cref="EntityHandler_004134C0"/>.
    ///
    /// <para>
    /// <b>Variante 0 — l'anneau.</b> Il ouvre ses echelles <c>+0x28</c> et <c>+0x2c</c> par
    /// huitiemes vers <b>la cible <c>+0x48</c></b> que lui a posee qui l'a fait naitre — la ou
    /// l'onde de la foret visait la constante 30000. Sa teinte part de <c>0xfafafa</c> et perd
    /// 7, 7 et 5 ; le bleu a zero l'eteint. Tant que <c>+0x16</c> est nul et que l'anneau n'est pas
    /// a <paramref name="margin"/> de sa cible, il balaie les quatre membres : un membre vivant, a
    /// moins de <c>echelle / 50</c> unites horizontales, pas encore marque dans <c>+0x14</c>, recoit
    /// son bit — <b>puis l'effet d'etat numero <c>+0x44</c></b>, une fois par membre et par onde.
    /// </para>
    /// <para>
    /// <b>Le lanceur est le grand-parent.</b> Le parent est <c>+0x6c</c>, et c'est SON <c>+0x6c</c>
    /// qui est passe aux appliqueurs ; sans parent, c'est l'onde elle-meme qui sert de lanceur —
    /// mais <b>sans parent l'effet n'est jamais applique</b> : le test du parent nul vient apres
    /// la pose du bit et avant l'aiguillage. Le lanceur par defaut est donc lu et jamais employe.
    /// Un <c>+0x44</c> au-dela de 11 ne fait rien non plus, le bit etant deja pose.
    /// </para>
    /// <para>
    /// <b>L'aiguillage a douze entrees et onze cibles</b> : les entrees 4 et 9 sautent au meme
    /// bloc, l'effet 22. Chaque enveloppe est de la meme forme — le garde-fou
    /// <see cref="IsEffectPairBlocked_004364B0"/>, puis l'appliqueur avec <c>keep = 0</c> — et rend
    /// une valeur que l'onde ne lit pas.
    /// </para>
    /// <para>
    /// <b>Variante 1 — l'etincelle</b>, mot pour mot celle de l'onde de la foret a une difference
    /// pres : elle <b>n'ecrit ni <c>+0x0a</c> ni <c>+0x0b</c></b>, pas plus que la variante 0.
    /// </para>
    /// </summary>
    private static void TickAfflictingRing_00409F40(byte* e, int margin)
    {
        byte* parent = *(byte**)(e + 0x6c);
        uint* caster = parent != null ? *(uint**)(parent + 0x6c) : (uint*)e;

        // ---------------------------------------------------------------- variante 0, l'anneau
        if (*(uint*)(e + 0x5c) == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                e[0x04] = 7;
                e[0x38] = 0xfa;
                e[0x39] = 0xfa;
                e[0x3a] = 0xfa;
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x2a) = (short)((0x19 - *(short*)(e + 0x12)) * 400);
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x2e) = -0x3c;
                *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & 0xfffe) | 2);
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, -7, -7, -5);

                int target = *(int*)(e + 0x48);
                short spread = (short)(((target - *(short*)(e + 0x28)) >> 3) + *(short*)(e + 0x28));
                *(short*)(e + 0x28) = spread;
                *(short*)(e + 0x2c) = (short)(((target - *(short*)(e + 0x2c)) >> 3) + *(short*)(e + 0x2c));

                if (e[0x3a] == 0) *(uint*)e &= 0x7fffffff;

                if (*(short*)(e + 0x16) == 0 && spread < target - margin)
                {
                    byte* member = Inp.s_SlotFlags_005e93c0;                 // 0x005e93c0

                    for (int i = 0; i < 4; i++, member += 0x9c)
                    {
                        uint flags = *(uint*)member;
                        if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                        int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                        int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                        // Ghidra: FUN_00545a10 — winsys3d Ordinal_442. `imul 0x51EB851F ; sar 4` : /50.
                        if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) >= *(short*)(e + 0x28) / 0x32) continue;
                        if ((*(short*)(e + 0x14) & (1 << i)) != 0) continue;

                        *(ushort*)(e + 0x14) |= (ushort)(1 << i);

                        if (parent == null) continue;
                        AfflictMember_00409F40(*(int*)(e + 0x44), caster, (uint*)member);
                    }
                }
            }
        }

        // ---------------------------------------------------------------- variante 1, l'etincelle
        if (*(uint*)(e + 0x5c) != 1) return;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x30;

            *(short*)(e + 0x28) = (short)(CrtRand() % 200 + 200);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 200 + 200);
            *(short*)(e + 0x2c) = (short)(CrtRand() % 200 + 200);

            *(ushort*)(e + 0x32) = (ushort)SignedMask(CrtRand(), 0xfff);

            int seed = CrtRand();
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x30) = (short)(seed % 500);

            *(short*)(e + 0x2e) = -0x3c;
            *(int*)(e + 0x20) += (CrtRand() % 200) * 0x1000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) -= 0x96;

            // `(0xfa - b) >> 1` ajoute a b — la teinte rejoint 0xfa par moities.
            e[0x38] = (byte)(e[0x38] + (byte)((0xfa - (int)e[0x38]) >> 1));
            e[0x39] = (byte)(e[0x39] + (byte)((0xfa - (int)e[0x39]) >> 1));
            e[0x3a] = (byte)(e[0x3a] + (byte)((0xfa - (int)e[0x3a]) >> 1));

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 5)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }

            GrowSparkScales(e);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x32) -= 0x96;

            // `-b >> 3` ajoute a b — la teinte retombe par huitiemes.
            e[0x38] = (byte)(e[0x38] + (byte)((-(int)e[0x38]) >> 3));
            e[0x39] = (byte)(e[0x39] + (byte)((-(int)e[0x39]) >> 3));
            e[0x3a] = (byte)(e[0x3a] + (byte)((-(int)e[0x3a]) >> 3));

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x14) *(uint*)e &= 0x7fffffff;

            GrowSparkScales(e);
        }
    }

    /// <summary>
    /// Ghidra: la table de sauts <c>0x0040a30c</c> et ses douze enveloppes <c>0x0040a340</c> a
    /// <c>0x0040a5c0</c> — <b>l'effet d'etat que l'onde applique</b>, choisi par son <c>+0x44</c>.
    /// Chaque enveloppe fait 61 octets de la meme forme : si
    /// <see cref="IsEffectPairBlocked_004364B0"/> refuse, rendre 1 ; sinon rendre
    /// <c>appliqueur(lanceur, victime, 0) != 0</c>. L'onde ne lit pas ce qui est rendu, et ces
    /// douze valeurs ne sont donc pas transcrites.
    ///
    /// <para>
    /// L'ordre de la table n'est pas celui des adresses : il est releve entree par entree
    /// (<c>od</c> de <c>0x0040a30c</c>, douze mots). Les entrees 4 et 9 designent toutes deux
    /// <c>0x0040a131</c>.
    /// </para>
    /// </summary>
    private static void AfflictMember_00409F40(int kind, uint* caster, uint* member, int keep = 0)
    {
        if ((uint)kind > 11) return;
        if (IsEffectPairBlocked_004364B0(caster, member) != 0) return;

        switch (kind)
        {
            case 0:  ApplyStatusEffect_0053BB20(caster, member, keep); break;   // 0x0040a480, effet 16
            case 1:  ApplyStatusEffect_0053BC60(caster, member, keep); break;   // 0x0040a4c0, effet 18
            case 2:  ApplyTrapEffect_0053C420(caster, member, keep); break;     // 0x0040a5c0
            case 3:  ApplyStatusEffect_0053B3C0(caster, member, keep); break;   // 0x0040a340
            case 4:  ApplyStatusEffect_0053BF00(caster, member, keep); break;   // 0x0040a500, effet 22
            case 5:  ApplyStatusEffect_0053B4B0(caster, member, keep); break;   // 0x0040a380, effet 8
            case 6:  ApplyStatusEffect_0053B660(caster, member, keep); break;   // 0x0040a400, effet 10
            case 7:  ApplyStatusEffect_0053B580(caster, member, keep); break;   // 0x0040a3c0, effet 9
            case 8:  ApplyStatusEffect_0053B8F0(caster, member, keep); break;   // 0x0040a440, effet 13
            case 9:  ApplyStatusEffect_0053BF00(caster, member, keep); break;   // 0x0040a500, effet 22
            case 10: ApplyStatusEffect_0053C040(caster, member, keep); break;   // 0x0040a540, effet 24
            case 11: ApplyStatusEffect_0053C1F0(caster, member, keep); break;   // 0x0040a580, effet 27
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00482ff0</c> — <b>la gerbe qui frappe en grossissant</b>, creneau 31 de la table
    /// d'effets <c>0x0056b1c8</c>, sous-zones 3, 4 et 9 de la mine abandonnee. Absente de
    /// <c>Blaze.c</c> ; 75 instructions, <c>Legacy/Raw/Blaze/FUN_00482FF0.asm</c>. <b>La meme adresse
    /// sert aussi la vieille tour et la grotte scellee</b>, dans la meme table. Le role a
    /// l'ecran n'est pas etabli, ni qui la fait naitre.
    ///
    /// <para>
    /// <b>Huit images, sans etat de naissance.</b> Le compteur est <c>+0x10</c> lui-meme : il part
    /// de ce que le lanceur a laisse, avance d'un par image, et l'emplacement est <b>vide
    /// entierement</b> (<c>*e = 0</c>, pas le seul bit de vie) quand il atteint 8.
    /// </para>
    /// <para>
    /// <b>L'echelle croit comme le carre du temps</b> : <c>(t² + 8) &lt;&lt; 7</c>, plus un tirage
    /// sur dix bits refait a chaque image — elle tremble en grossissant. Elle est ecrite en
    /// <c>+0x2c</c> telle quelle et en <c>+0x28</c> comme un mot double <c>(s &lt;&lt; 17) | s</c> :
    /// <c>+0x2a</c> recoit donc <b>le double</b> de l'echelle, au bit de poids fort pres.
    /// </para>
    /// <para>
    /// <b>Elle avance de sa vitesse <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, puis le placement est
    /// annule en hauteur</b> : la hauteur calculee avant <see cref="FinalizeEntityPlacement2"/> est
    /// reecrite apres, et seulement bornee par le plancher <c>+0x64</c> — ou elle leve
    /// <c>+0x90 = 0x800</c> elle-meme. Une collision <c>0x8802</c> la vide sur-le-champ.
    /// </para>
    /// <para>
    /// <b>Elle frappe au nom de son proprietaire</b> <c>+0x74</c>, par le gabarit
    /// <c>{0, 0, 0x3c, 0xc8, 0, 0x78}</c> et <see cref="SweepOpposingSideForMeleeHit_00474C70"/>,
    /// comme la trajectoire du Goblin — mais sans pousser la victime ni jouer de son, et elle
    /// <b>ne s'arrete pas au premier coup</b> : elle continue ses huit images. Un balayage qui
    /// trouve une victime alors que le drapeau <c>0x005de0b8</c> est nul la vide.
    /// </para>
    /// </summary>
    private static void EntityHandler_00482FF0(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0x3c;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x78;

        int age = *(short*)(e + 0x10);
        int scale = (CrtRand() & 0x3ff) + ((age * age + 8) << 7);
        *(short*)(e + 0x2c) = (short)scale;
        *(int*)(e + 0x28) = (scale << 0x11) | scale;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        int height = *(int*)(e + 0x20) + *(int*)(e + 0x54);
        *(int*)(e + 0x20) = height;
        *(int*)(e + 0x24) += *(int*)(e + 0x58);

        FinalizeEntityPlacement2((uint*)e);                                  // Ghidra: FUN_00533940

        *(int*)(e + 0x20) = height;
        if (height <= *(int*)(e + 0x64))
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x64);
            *(uint*)(e + 0x90) = 0x800;
        }

        if ((*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);
        if (victim != null)
        {
            if (*Mem.Camera(0x005de0b8) == 0)
            {
                *(uint*)e = 0;
                return;
            }

            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;
        }

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 8) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00418f50 @ 0x00418f50 — <b>blesser le groupe autour d'un point</b>, en
    /// pourcentage. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Chaque membre vivant, ni <c>0x50000000</c>, de <c>+0x12</c> nul, a moins de
    /// <paramref name="radius"/> unites du point — <b>la hauteur relevee de <c>0x3c</c></b>, comme
    /// dans les autres balayages du groupe — et sans clignotement en cours, perd
    /// <paramref name="percent"/> % de ses points de vie maximum par
    /// <see cref="ApplyPercentDamage_005397B0"/>, puis recoit le bit <c>0x40</c> et <b>un
    /// clignotement de <c>0x3c</c></b> — quatre fois plus long que celui d'un coup ordinaire, et qui
    /// ecrase le <c>0x10</c> que l'appliqueur vient de poser.
    /// </para>
    /// <para>
    /// Les sites d'appel empilent <b>huit</b> arguments (<c>add esp,20h</c>) ; le corps n'en lit
    /// que six.
    /// </para>
    /// </summary>
    private static void DamagePartyNearPoint_00418F50(int x, int y, int z, short radius, short percent, short effectParam)
    {
        byte* member = Inp.s_SlotFlags_005e93c0;                             // 0x005e93c0

        for (int i = 4; i != 0; i--, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            if (*(short*)(member + 0x12) != 0) continue;

            int dx = (x >> 0xc) - (*(int*)(member + 0x1c) >> 0xc);
            int dz = (z >> 0xc) - (*(int*)(member + 0x24) >> 0xc);
            int dy = (y >> 0xc) - (*(int*)(member + 0x20) >> 0xc) - 0x3c;

            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
            if (distance >= radius || *(short*)(member + 0x96) != 0) continue;

            ApplyPercentDamage_005397B0((uint*)member, percent, (ushort)effectParam);
            *(uint*)member |= 0x40;
            *(short*)(member + 0x96) = 0x3c;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00483a30</c> — <b>la bulle empoisonnee</b>, creneau 31 de la table d'effets
    /// <c>0x0056b1c8</c>, sous-zones 3, 7 et 9 de la vieille tour. Absente de <c>Blaze.c</c> ; 97
    /// instructions, <c>Legacy/Raw/Blaze/FUN_00483A30.asm</c>. Vraisemblablement l'enfant de
    /// <see cref="EntityHandler_00483930"/>, qui fait naitre un <c>0xc0 + n</c> dans cette table.
    ///
    /// <para>
    /// <b>Elle grossit, derive, et empoisonne une fois chaque victime.</b> Son echelle rejoint
    /// <c>0x2000</c> par huitiemes, recopiee sur les trois axes. Elle avance de sa vitesse, freinee
    /// d'un huitieme et d'un deux-cent-cinquante-sixieme par image — la vitesse que son pere lui a
    /// posee. Chaque image elle sonde devant elle (<see cref="ProbeMeleeTarget_00474CA0"/>, gabarit
    /// <c>{-0x400, -0x1e, 0x64, 0x32, 0, 0x3c}</c>) ; une victime qu'elle n'a pas encore touchee
    /// recoit <b>l'effet d'etat 7</b> au compte du lanceur <c>+0x74</c>
    /// (<see cref="ApplyStatusEffect_0053B810"/>), le bit <c>0x40</c> et 8 en <c>+0x96</c>.
    /// </para>
    /// <para>
    /// <b>Le masque de victimes est l'octet <c>+0x09</c></b> : un bit par numero d'emplacement
    /// (<c>victime + 0x08</c>), pose au premier coup. <b>Elle n'inflige aucun degat direct</b> — pas de
    /// <see cref="ResolveMeleeHit_00539B80"/>. Sa teinte perd <c>0x101010</c> par image, et
    /// l'emplacement est vide quand elle vaut exactement zero.
    /// </para>
    /// <para>
    /// <b>Le gabarit se lit sur la pile</b> : son premier mot est ecrit a trois registres sauvegardes
    /// (<c>[esp+0Ch]</c>), les cinq autres apres deux <c>push</c> de plus (<c>[esp+16h]</c> a
    /// <c>[esp+1Eh]</c>) — tous designent la meme zone.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483A30(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = -0x400;

        int scale = *(short*)(e + 0x28);
        scale += (0x2000 - scale) >> 3;
        *(short*)(e + 0x2c) = (short)scale;
        *(uint*)(e + 0x28) = ((uint)scale << 0x10) | (uint)scale;

        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x24) += vz;
        *(int*)(e + 0x20) += vy;

        probe[1] = -0x1e;
        probe[2] = 0x64;
        probe[3] = 0x32;
        probe[4] = 0;
        probe[5] = 0x3c;

        *(int*)(e + 0x50) = vx - (vx >> 8) - (vx >> 3);
        *(int*)(e + 0x54) = vy - (vy >> 3) - (vy >> 8);
        *(int*)(e + 0x58) = vz - (vz >> 8) - (vz >> 3);

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);                    // Ghidra: FUN_00474ca0

        // Ghidra: `mov cl,[edi+8] ; shl edx,cl ; test al,dl` — une victime deja marquee est epargnee.
        if (victim != null && (e[0x09] & (byte)(1 << victim[0x08])) == 0)
        {
            byte* caster = *(byte**)(e + 0x74);
            caster[0x07] = 7;
            ApplyStatusEffect_0053B810((uint*)caster, (uint*)victim, 0);

            e[0x09] |= (byte)(1 << victim[0x08]);
            *(uint*)victim |= 0x40;
            *(short*)(victim + 0x96) = 8;
        }

        // Ghidra: LAB_00483b31 — `add eax,0FFEFEFF0h` puis `jne`.
        uint tint = *(uint*)(e + 0x38) + 0xffefeff0;
        *(uint*)(e + 0x38) = tint;
        if (tint == 0) *(uint*)e = 0;
    }

    // =====================================================================
    // LE FEU FOLLET DE LA VIEILLE TOUR — 0x00417110 et ses DEUX jumeaux (2026-09-17)
    //
    // Le seul triplet de la categorie C de Zone4ParComplexite.md : trois creneaux, 243 instructions,
    // une lecture. Un bloc de sous-zones chacun, et le diff ne rend qu'une ligne — l'adresse de la
    // planche.
    //
    //   0x00417110  sous-zones 0, 1, 2, 4   planche 0x0054FAC0
    //   0x00418C40  sous-zones 5, 6, 8, 10  planche 0x0054FC68
    //   0x0041C250  sous-zones 3, 7, 9      planche 0x0054FDC0
    //
    // IL EST ICI ET NON DANS Effects/ parce qu'il DECIDE QUI PERD DES POINTS DE VIE : sa derniere
    // instruction est un appel a DamagePartyNearPoint_00418F50. C'est le test d'appartenance de
    // ARCHITECTURE.md, pas le sujet apparent, qui choisit le repertoire — ses freres de forme
    // vivent dans Effects/OldTowerParticles.cs.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054fac0</c> — <b>la planche du feu follet des sous-zones 0, 1, 2
    /// et 4</b> : quatre tuiles <c>0x20 x 0x20</c> en rangee sur la page <c>0x03</c>, role 1. Bornee
    /// en haut par <c>0x0054fae0</c>, ou commence la meme rangee sur la page <c>0x02</c> (le jumeau
    /// de <c>s_Descriptors_0054FDE0</c>, <c>Effects/OldTowerParticles.cs</c>). Quatre
    /// enregistrements, et le corps mene <c>+0x40</c> de 0 a 3 en le bornant explicitement :
    /// marge 0, exactement juste.</summary>
    private static readonly byte[] s_Descriptors_0054FAC0 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fac0  <- 0x00417110
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fac8
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fad0
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fad8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fc68</c> — la meme planche pour les sous-zones 5, 6, 8 et 10,
    /// memes octets, bornee par <c>0x0054fc88</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FC68 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc68  <- 0x00418C40
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc70
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc78
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fc80
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0054fdc0</c> — la meme planche pour les sous-zones 3, 7 et 9,
    /// memes octets, bornee par <c>0x0054fde0</c>, le debut de <c>s_Descriptors_0054FDE0</c>.</summary>
    private static readonly byte[] s_Descriptors_0054FDC0 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fdc0  <- 0x0041C250
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fdc8
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fdd0
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x0054fdd8
    ];

    private static byte* s_Descriptors0054FAC0Storage;
    private static byte* s_Descriptors0054FC68Storage;
    private static byte* s_Descriptors0054FDC0Storage;

    private static byte* Descriptors0054FAC0()
        => NativeCopy(s_Descriptors_0054FAC0, ref s_Descriptors0054FAC0Storage);

    private static byte* Descriptors0054FC68()
        => NativeCopy(s_Descriptors_0054FC68, ref s_Descriptors0054FC68Storage);

    private static byte* Descriptors0054FDC0()
        => NativeCopy(s_Descriptors_0054FDC0, ref s_Descriptors0054FDC0Storage);

    /// <summary>Ghidra: FUN_00417110 @ 0x00417110 — le feu follet des sous-zones 0, 1, 2 et 4.
    /// Decompilee dans <c>Blaze.c</c>. Corps : <see cref="TickWanderingFlame_00417110"/>.</summary>
    private static void EntityHandler_00417110(byte* e)
        => TickWanderingFlame_00417110(e, Descriptors0054FAC0());        // Ghidra: &DAT_0054fac0

    /// <summary>Ghidra: FUN_00418c40 @ 0x00418c40 — le meme corps pour les sous-zones 5, 6, 8 et
    /// 10 ; le diff ne rend que <c>mov dword [esi+3Ch],54FC68h</c>.</summary>
    private static void EntityHandler_00418C40(byte* e)
        => TickWanderingFlame_00417110(e, Descriptors0054FC68());        // Ghidra: &DAT_0054fc68

    /// <summary>Ghidra: FUN_0041c250 @ 0x0041c250 — le meme corps pour les sous-zones 3, 7 et 9 ;
    /// le diff ne rend que <c>mov dword [esi+3Ch],54FDC0h</c>.</summary>
    private static void EntityHandler_0041C250(byte* e)
        => TickWanderingFlame_00417110(e, Descriptors0054FDC0());        // Ghidra: &DAT_0054fdc0

    /// <summary>
    /// Le corps des trois : <b>un feu follet qui tourne autour de sa proie, brule le groupe autour de
    /// lui a chaque image, et s'eteint contre un mur</b>.
    ///
    /// <para>
    /// <b>Trois etats et une extinction.</b> L'etat 0 arme la planche, une echelle de <c>0x1000</c>,
    /// le bit <c>0x4000</c>, et fige son ancre (<c>+0x50</c>, <c>+0x54</c>, <c>+0x58</c>). Le
    /// <b>premier</b> follet d'une volee — celui dont <c>+0x14</c> est nul — joue le son <c>0x21</c>
    /// pour toute la volee. L'etat 1 est une attente de <c>2 × +0x14</c> images : c'est ce qui
    /// <b>echelonne le depart</b> d'une volee, chaque follet partant deux images apres son cadet. En
    /// sortant il rend le bit <c>0x4000</c> et remet sa phase <c>+0x5c</c> a zero.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est tout le mouvement, et il ne s'accumule pas : il se recalcule.</b> Position et
    /// ancre sont reecrites l'une depuis l'autre a chaque image —
    /// <c>X = sin(cap) × +0x16 × 2 + ancreX</c>, <c>Z = cos(cap) × +0x16 × 2 + ancreZ</c>,
    /// <c>Y = ancreY</c> —, puis <b>l'ancre recoit la position ainsi obtenue</b>. Le follet tourne
    /// donc autour d'un centre qui le suit, ce qui produit une derive et non un cercle.
    /// </para>
    /// <para>
    /// <b>Il redescend vers la camera</b> : si son ancre en hauteur depasse de plus de 100 unites la
    /// hauteur de camera <c>0x005de06c</c>, l'ecart est reduit d'un seizieme par image. C'est la
    /// seule chose qui le retienne en l'air.
    /// </para>
    /// <para>
    /// <b>Par-dessus, un battement a trois axes</b> pris sur la phase <c>+0x5c</c>, qui avance de
    /// <b>300</b> par image et repasse par zero au-dela de <c>0xfff</c> : la hauteur recoit
    /// <c>sin(phase) × 10</c> en pleine amplitude, X et Z un produit de deux tables <b>divisees par
    /// 64 chacune</b> — le seul endroit du corps ou l'echelle des tables est reduite. Et les trois
    /// axes prennent ensuite un brouillage de ±10 unites entieres.
    /// </para>
    /// <para>
    /// <b>Son image change par marche aleatoire</b>, pas par cycle : toutes les 7 a 11 images
    /// (10 images, ou 7 avec une chance sur cinq), l'indice repart de <c>+0x14 - 1</c> augmente d'un
    /// tirage sur trois, <b>borne a 0..3</b> par deux comparaisons explicites. La planche a quatre
    /// enregistrements : la borne du corps est exactement sa taille.
    /// </para>
    /// <para>
    /// <b>Une image sur cinq seulement, il se pose</b> (<see cref="FinalizeEntityPlacement2"/>) et
    /// regarde son mot de contact <c>+0x90</c> : les bits <c>0x8802</c> le font passer a l'etat 3,
    /// qui eteint l'entite. Le reste du temps il traverse.
    /// </para>
    /// <para>
    /// <b>Et il brule, quoi qu'il arrive</b> : l'appel a
    /// <see cref="DamagePartyNearPoint_00418F50"/> est <b>apres</b> les quatre etats, hors de tout
    /// test — 5 % des points de vie maximum dans un rayon de 50 unites, a chaque image, y compris
    /// pendant l'attente de l'etat 1 et l'image ou il meurt. Les sites d'appel empilent huit
    /// arguments, le corps n'en lit que six.
    /// </para>
    /// </summary>
    private static void TickWanderingFlame_00417110(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x4000;
            e[0x40] = (byte)(sbyte)*(short*)(e + 0x14);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            // Ghidra: FUN_00468080 — le son porte un identifiant en dur, pas un indice de sous-zone.
            if (*(short*)(e + 0x14) == 0)
                TryPlaySoundNearEntity((uint*)e, 0x21, 0x200, 0x1800, 0, 0);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;
            *(short*)(e + 0x98) = 0;

            if (*(short*)(e + 0x14) * 2 < *(short*)(e + 0x12))
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e &= 0xffffbfff;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short flicker = (short)(*(short*)(e + 0x14) * 0x80);
            *(ushort*)(e + 0x28) = (ushort)(CrtRand() % 0x200 + 0xed8 + flicker);
            *(ushort*)(e + 0x2a) = (ushort)(CrtRand() % 0x200 + 0xed8 + flicker);

            short course = *(short*)(e + 0x32);
            short radius = *(short*)(e + 0x16);

            int x = (int)Winsys3d.GetSinTableEntry(course) * radius * 2 + *(int*)(e + 0x50);
            int z = Winsys3d.GetCosTableEntry(course) * radius * 2 + *(int*)(e + 0x58);
            int anchorY = *(int*)(e + 0x54);

            *(int*)(e + 0x1c) = x;
            *(int*)(e + 0x24) = z;
            *(int*)(e + 0x20) = anchorY;

            // L'ancre suit la position : le centre du tour derive avec lui.
            *(int*)(e + 0x50) = x;
            *(int*)(e + 0x58) = z;

            int cameraY = *Mem.Camera(0x005de06c);
            if ((cameraY >> 0xc) + 100 < anchorY >> 0xc)
                *(int*)(e + 0x54) = anchorY - ((anchorY - cameraY) >> 4);

            int phase = *(int*)(e + 0x5c);
            *(int*)(e + 0x20) += (int)Winsys3d.GetSinTableEntry(phase) * 10;
            *(int*)(e + 0x24) +=
                ((int)Winsys3d.GetSinTableEntry(course) >> 6) * (Winsys3d.GetCosTableEntry(phase) >> 6) * 10;
            *(int*)(e + 0x1c) +=
                (Winsys3d.GetCosTableEntry(course) >> 6) * (Winsys3d.GetCosTableEntry(phase) >> 6) * 10;

            *(int*)(e + 0x1c) += (CrtRand() % 0x14 - 10) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x14 - 10) * 0x1000;

            int jitterZ = CrtRand();
            int advanced = phase + 300;
            *(int*)(e + 0x24) += (jitterZ % 0x14 - 10) * 0x1000;
            *(int*)(e + 0x5c) = advanced > 0xfff ? phase - 0xed4 : advanced;

            (*(short*)(e + 0x9a))++;

            int roll = CrtRand();
            if (*(short*)(e + 0x9a) > 10 || ((short)(roll % 5) == 0 && *(short*)(e + 0x9a) > 6))
            {
                ushort seed = *(ushort*)(e + 0x14);
                *(short*)(e + 0x9a) = 0;

                int frame = seed - 1 + CrtRand() % 3;
                if ((short)frame > 3) frame = 3;
                if ((short)frame < 0) frame = 0;
                e[0x40] = (byte)(sbyte)frame;
            }

            (*(short*)(e + 0x98))++;
            if (*(short*)(e + 0x98) > 4)
            {
                *(short*)(e + 0x98) = 0;
                FinalizeEntityPlacement2((uint*)e);                  // Ghidra: FUN_00533940

                if ((*(uint*)(e + 0x90) & 0x8802) != 0) *(short*)(e + 0x10) = 3;
            }
        }

        if (*(short*)(e + 0x10) == 3) *(uint*)e &= 0x7fffffff;

        // Ghidra: FUN_00418f50, hors de tout etat — il brule meme a l'image de sa mort.
        DamagePartyNearPoint_00418F50(
            *(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24), 0x32, 5, 0);
    }

    // =====================================================================
    // LES DEUX CORPS DE LA CATEGORIE C QUI FRAPPENT (2026-09-17, lot 2)
    //
    // Meme test d'appartenance que le feu follet : ils ecrivent dans un bloc de combat, donc ils
    // sont ici et non dans Effects/OldTowerParticles.cs. 0x00416C40 blesse le groupe autour de lui,
    // 0x004850C0 cherche une victime et lui applique une frappe au corps a corps.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0054fae0</c> — quatre tuiles <c>0x20 x 0x20</c>, page <c>0x02</c>.
    /// Memes octets que <c>s_Descriptors_0054FDE0</c> (<c>Effects/OldTowerParticles.cs</c>) ; bornee
    /// par <c>0x0054fb00</c>. Le corps mene <c>+0x40</c> de 1 a 3 : marge 0 sur les quatre.</summary>
    private static readonly byte[] s_Descriptors_0054FAE0 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054fae0  <- 0x00416C40
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054fae8
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054faf0
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054faf8
    ];

    private static byte* s_Descriptors0054FAE0Storage;

    private static byte* Descriptors0054FAE0()
        => NativeCopy(s_Descriptors_0054FAE0, ref s_Descriptors0054FAE0Storage);

    /// <summary>
    /// Ghidra: <c>0x00416c40</c> — <b>la nappe qui descend en tournant et brule ce qu'elle
    /// traverse</b>, sous-zones 0, 1, 2 et 4. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_00416C40.asm</c>.
    ///
    /// <para>
    /// <b>Elle se pose sur le terrain a la naissance</b> (<see cref="TryPlaceEntity"/> avec le masque
    /// <c>0x8000</c>, puis <see cref="FinalizeEntityPlacement2"/>), ce qu'aucune autre particule de
    /// la tour ne fait : c'est une nappe au sol, pas un effet en l'air. Elle fige son ancre, garde sa
    /// hauteur de depart dans <c>+0x60</c> <b>et</b> <c>+0x64</c>, et arme les quatre mots
    /// <c>+0x82</c>, <c>+0x86</c>, <c>+0x8a</c>, <c>+0x8e</c> a <c>0xa000</c> — le gabarit de
    /// collision.
    /// </para>
    /// <para>
    /// Ensuite, a chaque image : la position tourne a <c>+0x12</c> autour de l'ancre, la hauteur perd
    /// <c>0x2000</c>, l'echelle gagne <c>0x64</c> sur les deux axes, la teinte perd 3, et le rayon
    /// <c>+0x12</c> gagne <c>0x14</c> — <b>elle s'elargit en descendant</b>. Deux choses l'arretent :
    /// un contact <c>0x8802</c> dans <c>+0x90</c>, ou la retombee sous sa hauteur de depart.
    /// </para>
    /// <para>
    /// <b>Et elle brule a chaque image</b>, hors de tout etat : 3 % des points de vie maximum dans un
    /// rayon de 50 unites, avec le parametre d'effet 3 — le seul corps de la tour a le passer non
    /// nul. La hauteur qu'elle donne au balayage est celle <b>d'apres</b> la descente.
    /// </para>
    /// <para>Son image part de 1 a 3 (et non de 0) et reboucle sur 1 : le premier enregistrement de la
    /// planche n'est jamais montre.</para>
    /// </summary>
    private static void TickDescendingBurnMat_00416C40(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054FAE0();                     // Ghidra: &DAT_0054fae0

            int frameDraw = CrtRand() % 3;                                   // Ghidra: FUN_00547140
            e[0x0a] = 1;

            e[0x38] = 0xa0;
            e[0x39] = 0xa0;
            e[0x3a] = 0xa0;
            *(short*)(e + 0x28) = 0x1f4;
            *(short*)(e + 0x2a) = 0x1f4;

            int height = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            e[0x0b] = 0;
            *(int*)(e + 0x54) = height;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 5;

            *(ushort*)(e + 0x82) = 0xa000;
            *(ushort*)(e + 0x86) = 0xa000;
            *(ushort*)(e + 0x8a) = 0xa000;
            *(ushort*)(e + 0x8e) = 0xa000;

            *(int*)(e + 0x64) = height;
            *(int*)(e + 0x60) = height;

            e[0x40] = (byte)(frameDraw + 1);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);   // Ghidra: FUN_005330d0
            FinalizeEntityPlacement2((uint*)e);                                  // Ghidra: FUN_00533940
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short course = *(short*)(e + 0x32);
            short radius = *(short*)(e + 0x12);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);

            *(short*)(e + 0x28) += 0x64;
            *(short*)(e + 0x2a) += 0x64;
            *(int*)(e + 0x20) -= 0x2000;

            BrightenEntityTint_00404DE0(e, -3, -3, -3);                      // Ghidra: FUN_00404de0
            FinalizeEntityPlacement2((uint*)e);                              // Ghidra: FUN_00533940

            if ((*(uint*)(e + 0x90) & 0x8802) != 0) *(short*)(e + 0x10) = 2;

            int height = *(int*)(e + 0x20);
            if (*(int*)(e + 0x60) >= height) *(short*)(e + 0x10) = 2;

            (*(short*)(e + 0x14))--;
            *(short*)(e + 0x12) += 0x14;

            if (*(short*)(e + 0x14) <= 0)
            {
                *(short*)(e + 0x14) = 5;
                byte next = (byte)(e[0x40] + 1);
                e[0x40] = next;
                if (next == 4) e[0x40] = 1;
            }

            // Ghidra: FUN_00418f50 — huit arguments empiles, six lus. La hauteur passee est celle
            // d'apres la descente, pas celle du debut du tour.
            DamagePartyNearPoint_00418F50(*(int*)(e + 0x1c), height, *(int*)(e + 0x24), 0x32, 3, 3);
        }

        if (*(short*)(e + 0x10) == 2) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x004850c0</c> — <b>le projectile qui grossit en tombant et frappe qui il
    /// touche</b>, sous-zones 3, 7 et 9. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_004850C0.asm</c>.
    ///
    /// <para>
    /// <b>Il n'a pas d'etat au sens habituel</b> : <c>+0x10</c> est son <b>age</b>, de 0 a 7, et le
    /// corps entier se deroule a chaque image. Zero vaut naissance — il y fige son ancre de vol dans
    /// <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>.
    /// </para>
    /// <para>
    /// <b>Son echelle est ecrite en un seul dword</b> (<c>mov [esi+28h],edx</c>), les deux mots d'un
    /// coup : <c>+0x28</c> recoit <c>t × 3</c> et <c>+0x2a</c> recoit <c>t &gt;&gt; 14</c>, ou
    /// <c>t = (rand &amp; 0x3ff) + ((age² + 8) &lt;&lt; 7)</c>. La meme valeur, decalee de 7 bits de
    /// plus, est ajoutee a la hauteur : <b>il monte et grossit en meme temps, et de plus en plus
    /// vite</b>.
    /// </para>
    /// <para>
    /// L'ancre avance de la vitesse <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, la position s'y recale, et
    /// <see cref="FinalizeEntityPlacement2"/> tranche : le sol <c>+0x64</c> force
    /// <c>+0x90</c> a <c>0x800</c>, et un contact <c>0x8802</c> <b>eteint l'entite d'un coup</b>
    /// (<c>mov dword [esi],0</c> — le mot d'etat entier, pas un masque).
    /// </para>
    /// <para>
    /// <b>La frappe.</b> <see cref="SweepOpposingSideForMeleeHit_00474C70"/> cherche une victime dans
    /// une boite <c>(0, 0, 0x3c, 0xc8, 0, 0x78)</c> montee sur la pile. S'il en trouve une mais que
    /// <c>0x005de0b8</c> est nul, le projectile s'eteint sans rien faire ; sinon
    /// <see cref="ResolveMeleeHit_00539B80"/> applique les degats depuis l'attaquant range en
    /// <c>+0x74</c>, la victime prend le bit <c>0x40</c> et un clignotement de <c>0x10</c>, et
    /// <see cref="SpawnCastEntity_0047AAE0"/> fait naitre le sous-type <c>0x72</c> — l'impact.
    /// </para>
    /// <para>
    /// <b>Deux details du binaire.</b> L'octet <c>+0x04</c> est mis a <c>0x1b</c> le temps de la
    /// naissance puis restaure : c'est ainsi que l'impact herite de la bonne appartenance. Et la
    /// teinte se decolore par un <b>add de dword</b> (<c>+0xffe7e7e8</c>), donc avec retenue d'un
    /// canal sur l'autre — conserve tel quel.
    /// </para>
    /// </summary>
    private static void TickGrowingBolt_004850C0(byte* e)
    {
        // Ghidra: six mots montes sur la pile avant tout, la boite de recherche.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0x3c;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x78;

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
        }

        int age = *(short*)(e + 0x10);
        int swell = (CrtRand() & 0x3ff) + ((age * age + 8) << 7);            // Ghidra: FUN_00547140

        // Ghidra: `lea edx,[eax+eax*2] ; shl ecx,12h ; or edx,ecx ; mov [esi+28h],edx` — une seule
        // ecriture de 32 bits sur les deux mots d'echelle.
        *(uint*)(e + 0x28) = (uint)(swell * 3) | (uint)(swell << 18);

        int anchorX = *(int*)(e + 0x44) + *(int*)(e + 0x50);
        int anchorY = *(int*)(e + 0x48) + *(int*)(e + 0x54);
        int anchorZ = *(int*)(e + 0x4c) + *(int*)(e + 0x58);

        *(int*)(e + 0x44) = anchorX;
        *(int*)(e + 0x48) = anchorY;
        *(int*)(e + 0x4c) = anchorZ;

        int height = (swell << 7) + anchorY;
        *(int*)(e + 0x1c) = anchorX;
        *(int*)(e + 0x20) = height;
        *(int*)(e + 0x24) = anchorZ;

        FinalizeEntityPlacement2((uint*)e);                                  // Ghidra: FUN_00533940

        int ceiling = *(int*)(e + 0x64);
        *(int*)(e + 0x20) = height;
        if (height <= ceiling)
        {
            *(int*)(e + 0x20) = ceiling;
            *(uint*)(e + 0x90) = 0x800;
        }

        if ((*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            *(uint*)e = 0;                                                   // Ghidra: LAB_00485204
            return;
        }

        byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);      // Ghidra: FUN_00474c70

        if (victim != null)
        {
            if (*Mem.Camera(0x005de0b8) == 0)
            {
                *(uint*)e = 0;
                return;
            }

            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);    // Ghidra: FUN_00539b80

            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;

            byte side = e[4];
            e[4] = 0x1b;
            *(byte**)(e + 0x78) = victim;
            SpawnCastEntity_0047AAE0(e, 0x72, 0, 1);                         // Ghidra: FUN_0047aae0
            e[4] = side;
        }

        // Ghidra: `add edx,0FFE7E7E8h` sur le DWORD +0x38 — les trois canaux d'un coup, retenue
        // comprise.
        *(uint*)(e + 0x38) += 0xffe7e7e8;

        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) < 8) return;

        *(uint*)e = 0;
    }

    // =====================================================================
    // LA GROTTE SCELLEE, LOT 4 (2026-09-19) — la flamme qui recule en brulant
    //
    // Meme test d'appartenance que 0x00416C40 ci-dessus : elle blesse le groupe autour d'elle a
    // chaque image, par FUN_00418f50. Elle est donc ici et non dans SealedCaveParticles.cs.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00551158</c> — une tuile <c>0x20 x 0x20</c>, page <c>0x02</c>.
    /// Bornee en bas par une tuile <c>0x10 x 0x10</c> (<c>0x00551150</c>), en haut par la planche de
    /// la bouffee oblique du bloc 1 (<c>0x00551160</c>). Marge 1 : le corps n'ecrit jamais
    /// <c>+0x40</c>, et il nait par <c>FUN_0047ac40</c> (table <c>0x0056b2c8</c>), qui le met a
    /// zero.</summary>
    private static readonly byte[] s_Descriptors_00551158 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551158  <- 0x0043C010
    ];

    private static byte* s_Descriptors00551158Storage;

    private static byte* Descriptors00551158()
        => NativeCopy(s_Descriptors_00551158, ref s_Descriptors00551158Storage);

    /// <summary>
    /// Ghidra: <c>0x0043c010</c> — grotte scellee, sous-zone 1, table <c>0x0056b2c8</c>. Absente de
    /// <c>Blaze.c</c> ; 78 instructions. <b>La flamme qui recule en brulant</b> : nee 160 unites plus
    /// haut, teinte <c>0xc8</c>, echelles <c>0xbb8</c>, elle retient son point de depart
    /// (<c>+0x50</c>, <c>+0x58</c>) et s'en eloigne a l'oppose de son cap — son cap ne change pas —
    /// de 35 unites par image (<c>+0x9a</c>), en montant de <c>+0x5c</c>. A chaque image elle blesse le groupe
    /// a moins de 80 unites (2 %). Sa duree <c>+0x14</c> ecoulee, elle s'assombrit de 20 et meurt
    /// quand l'octet <c>+0x38</c> de sa teinte est nul.
    /// </summary>
    private static void EntityHandler_0043C010(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x38] = 0xc8;
            e[0x39] = 0xc8;
            e[0x3a] = 0xc8;
            *(int*)(e + 0x20) += 0xa0000;
            *(short*)(e + 0x2e) = -20;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(byte**)(e + 0x3c) = Descriptors00551158();            // Ghidra: &DAT_00551158
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x28) = 0x0bb8;
            *(short*)(e + 0x2a) = 0x0bb8;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int course = *(short*)(e + 0x32) + 0x800;
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * *(short*)(e + 0x9a) + *(int*)(e + 0x50);
        int z = (int)Winsys3d.GetCosTableEntry(course) * *(short*)(e + 0x9a) + *(int*)(e + 0x58);
        *(int*)(e + 0x20) += *(int*)(e + 0x5c) << 0xc;
        *(int*)(e + 0x24) = z;

        DamagePartyNearPoint_00418F50(
            *(int*)(e + 0x1c), *(int*)(e + 0x20), z, 0x50, 2, 0);  // Ghidra: FUN_00418f50

        *(short*)(e + 0x9a) += 0x23;
        *(short*)(e + 0x14) -= 1;
        if (*(short*)(e + 0x14) >= 0) return;

        BrightenEntityTint_00404DE0(e, -20, -20, -20);              // Ghidra: FUN_00404de0
        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00484da0</c> — grotte scellee, sous-zones 4 et 9 (lot 5, 2026-09-19). Absente
    /// de <c>Blaze.c</c> ; 67 instructions. <b>La vague qui frappe</b>, en deux temps que separe le
    /// bit <c>0x2000000</c> de son propre mot d'etat :
    /// <list type="bullet">
    /// <item>tant qu'il est eteint, elle grandit (<c>+0x28</c> rejoint <c>0x4000</c> d'un huitieme
    /// par image — ecrit en DOUBLE MOT, ce qui remet <c>+0x2a</c> a zero — et <c>+0x2c</c> suit) et
    /// s'eclaire de <c>0x080808</c> ; a <c>0x808080</c> elle pose le bit ;</item>
    /// <item>pose, elle balaie pendant 40 images la sonde <c>(0, -200, 200, 50, 0, 400)</c> par
    /// <see cref="ProbeMeleeTarget_00474CA0"/> : chaque victime subit le coup de son lanceur
    /// (<c>+0x74</c>) et clignote huit images. Puis elle palit de <c>0x080808</c> par image et
    /// libere l'emplacement (<c>*e = 0</c>) quand sa teinte est nulle.</item>
    /// </list>
    /// </summary>
    private static void EntityHandler_00484DA0(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = -0xc8;
        probe[2] = 0xc8;
        probe[3] = 0x32;
        probe[4] = 0;
        probe[5] = 0x190;

        uint flags = *(uint*)e;

        if ((flags & 0x2000000) == 0)
        {
            int scale = *(short*)(e + 0x28);
            scale += (0x4000 - scale) >> 3;
            *(int*)(e + 0x28) = scale;
            *(short*)(e + 0x2c) = (short)scale;

            uint tint = *(uint*)(e + 0x38) + 0x80808;
            *(uint*)(e + 0x38) = tint;
            if (tint == 0x808080) *(uint*)e = flags | 0x2000000;
            return;
        }

        if (*(short*)(e + 0x10) < 0x28)
        {
            *(short*)(e + 0x10) += 1;

            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);    // Ghidra: FUN_00474ca0
            if (victim == null) return;

            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);   // Ghidra: FUN_00539b80
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 8;
            return;
        }

        // Ghidra: LAB_00484e21 — `add eax,0FFF7F7F8h` puis `jne`.
        uint fade = *(uint*)(e + 0x38) + 0xfff7f7f8;
        *(uint*)(e + 0x38) = fade;
        if (fade == 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00485330</c> — grotte scellee, sous-zones 4 et 9 (lot 5, 2026-09-19). Absente
    /// de <c>Blaze.c</c> ; 82 instructions. <b>La gerbe qui frappe en roulant</b>, cousine de
    /// <see cref="TickGrowingBolt_004850C0"/> : meme sonde <c>(0, 0, 60, 200, 0, 120)</c>, meme
    /// sol et meme heurt (<c>0x8802</c>, et un sol franchi vaut heurt), meme coup venu du lanceur
    /// <c>+0x74</c> et meme decoloration par double mot. Mais elle file sur sa vitesse
    /// (<c>+0x50..+0x58</c>) des sa deuxieme image, s'aplatit (<c>+0x2a</c> et <c>+0x2c</c> perdent
    /// la moitie par image, <c>+0x28</c> rejoint <c>0x4000</c> d'un quart), tire son modele de 8 a
    /// 11 — et ne fait rien naitre. Huit images au plus.
    /// </summary>
    private static void EntityHandler_00485330(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0x3c;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x78;

        if (*(short*)(e + 0x10) != 0)
        {
            short flat = *(short*)(e + 0x2a);
            flat = (short)(flat - (flat >> 1));
            *(short*)(e + 0x2a) = flat;
            *(short*)(e + 0x2c) = flat;

            short wide = *(short*)(e + 0x28);
            *(short*)(e + 0x28) = (short)(wide + ((0x4000 - wide) >> 2));

            *(int*)(e + 0x20) += *(int*)(e + 0x54);
            *(int*)(e + 0x1c) += *(int*)(e + 0x50);
            *(int*)(e + 0x24) += *(int*)(e + 0x58);
        }

        int height = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        int ceiling = *(int*)(e + 0x64);
        *(int*)(e + 0x20) = height;
        if (height <= ceiling)
        {
            *(int*)(e + 0x20) = ceiling;
            *(uint*)(e + 0x90) = 0x800;
        }

        if ((*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            *(uint*)e = 0;                                           // Ghidra: LAB_0048543a
            return;
        }

        byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);      // Ghidra: FUN_00474c70
        if (victim != null)
        {
            if (*Mem.Camera(0x005de0b8) == 0)
            {
                *(uint*)e = 0;
                return;
            }

            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);    // Ghidra: FUN_00539b80
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;
        }

        *(uint*)(e + 0x38) += 0xffe7e7e8;
        e[4] = (byte)((CrtRand() & 3) + 8);

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 8) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00485ec0</c> — grotte scellee, sous-zones 13 a 15 (lot 6, 2026-09-19). Absente
    /// de <c>Blaze.c</c> ; 41 instructions. <b>L'eclair qui palit en frappant</b> : il porte le bit
    /// <c>0x04</c> de <c>+0x36</c> ; tant que sa teinte vaut au moins <c>0x404040</c> il balaie la
    /// sonde <c>(0, -100, 100, 200, 0, 100)</c> (<see cref="ProbeMeleeTarget_00474CA0"/>) et chaque
    /// victime subit le coup de son lanceur <c>+0x74</c> et clignote douze images. Il palit de
    /// <c>0x101010</c> par image et libere l'emplacement a teinte nulle.
    /// </summary>
    private static void EntityHandler_00485EC0(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = -0x64;
        probe[2] = 0x64;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x64;

        e[0x36] |= 4;

        if (*(uint*)(e + 0x38) >= 0x404040)
        {
            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);    // Ghidra: FUN_00474ca0
            if (victim != null)
            {
                ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);   // Ghidra: FUN_00539b80
                *(uint*)victim |= 0x40;
                *(ushort*)(victim + 0x96) = 0x0c;
            }
        }

        uint tint = *(uint*)(e + 0x38) + 0xffefeff0;
        *(uint*)(e + 0x38) = tint;
        if (tint == 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00485680</c> (sonde de 300, grandit en <c>+0x28</c>/<c>+0x2c</c>, frappe tant que
    /// la teinte vaut <c>0x404040</c>, palit de <c>0x101010</c>) et <c>0x00485850</c> (sonde de 200,
    /// <c>+0x2a</c> rejoint <c>+0x12</c>, frappe au-dessus de <c>0x202020</c>, palit de
    /// <c>0x080808</c>) — grotte scellee, sous-zones 13 a 15 (lot 6, 2026-09-19) ; absentes de
    /// <c>Blaze.c</c>, 69 et 72 instructions. <b>Les ondes qui frappent en s'etendant</b> : elles
    /// filent par <see cref="IntegrateWithDrag_00535880"/>, leur sonde
    /// <c>(0, 0, largeur, profondeur, 0, hauteur)</c> suit leurs echelles, et chaque touche porte le
    /// coup du lanceur <c>+0x74</c>, seize images de clignotement et le son 3 — a la position de
    /// l'onde, pas de la victime.
    /// </summary>
    private static void TickStrikingWave_00485680(byte* e, bool wide)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0;
        probe[3] = (short)(wide ? 0x12c : 0xc8);
        probe[4] = 0;
        probe[5] = 0;

        IntegrateWithDrag_00535880(e);                              // Ghidra: FUN_00535880

        uint threshold;
        if (wide)
        {
            int scale = *(short*)(e + 0x28);
            scale += (0x4000 - scale) >> 2;
            *(short*)(e + 0x28) = (short)scale;
            *(short*)(e + 0x2c) = (short)scale;
            probe[5] = (short)((scale * 90) >> 0xc);
            probe[2] = (short)((scale * 80) >> 0xc);
            threshold = 0x404040;
        }
        else
        {
            short tall = *(short*)(e + 0x2a);
            *(short*)(e + 0x2a) = (short)(tall + ((*(short*)(e + 0x12) - tall) >> 2));
            probe[2] = (short)((*(short*)(e + 0x28) * 50) >> 0xc);
            probe[5] = (short)((*(short*)(e + 0x2c) * 50) >> 0xc);
            threshold = 0x202020;
        }

        if (*(uint*)(e + 0x38) >= threshold)
        {
            byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);   // Ghidra: FUN_00474c70
            if (*Mem.Camera(0x005de0b8) != 0)
            {
                ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);   // Ghidra: FUN_00539b80
                *(uint*)victim |= 0x40;
                *(ushort*)(victim + 0x96) = 0x10;
                Aud.PlayPositionalSound(3, 0x400, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
            }
        }

        uint tint = *(uint*)(e + 0x38) + (wide ? 0xffefeff0 : 0xfff7f7f8);
        *(uint*)(e + 0x38) = tint;
        if (tint == 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004869d0</c> — grotte scellee, sous-zones 13 a 15 (lot 6). Absente de
    /// <c>Blaze.c</c> ; 98 instructions. <b>La gerbe qui envoute</b>, cousine de
    /// <see cref="TickGrowingBolt_004850C0"/> (meme ancre qui file, meme gonflement
    /// <c>(hasard &amp; 0x3ff) + ((age² + 8) &lt;&lt; 7)</c>, meme sol, meme heurt, meme sonde, meme
    /// decoloration, huit images) — mais ses echelles valent deux et trois fois le gonflement, sa
    /// hauteur 96 fois, et la touche n'est pas un coup : elle pose l'effet 13
    /// (<see cref="ApplyStatusEffect_0053B8F0"/>, famille 7 posee en <c>+0x07</c> du lanceur).
    /// </summary>
    private static void EntityHandler_004869D0(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0x3c;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x78;

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
        }

        int age = *(short*)(e + 0x10);
        int swell = (CrtRand() & 0x3ff) + ((age * age + 8) << 7);        // Ghidra: FUN_00547140
        *(uint*)(e + 0x28) = ((uint)(swell * 3) << 0x10) | (uint)(swell * 2);

        int anchorX = *(int*)(e + 0x44) + *(int*)(e + 0x50);
        int anchorY = *(int*)(e + 0x48) + *(int*)(e + 0x54);
        int anchorZ = *(int*)(e + 0x4c) + *(int*)(e + 0x58);
        *(int*)(e + 0x44) = anchorX;
        *(int*)(e + 0x48) = anchorY;
        *(int*)(e + 0x4c) = anchorZ;

        int height = ((swell * 3) << 5) + anchorY;
        *(int*)(e + 0x1c) = anchorX;
        *(int*)(e + 0x20) = height;
        *(int*)(e + 0x24) = anchorZ;

        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        int ceiling = *(int*)(e + 0x64);
        *(int*)(e + 0x20) = height;
        if (height <= ceiling)
        {
            *(int*)(e + 0x20) = ceiling;
            *(uint*)(e + 0x90) = 0x800;
        }

        if ((*(uint*)(e + 0x90) & 0x8802) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);      // Ghidra: FUN_00474c70
        if (victim != null)
        {
            if (*Mem.Camera(0x005de0b8) == 0)
            {
                *(uint*)e = 0;
                return;
            }

            byte* caster = *(byte**)(e + 0x74);
            caster[7] = 7;
            ApplyStatusEffect_0053B8F0((uint*)caster, (uint*)victim, 0);    // Ghidra: FUN_0053b8f0
            *(uint*)victim |= 0x40;
            *(ushort*)(victim + 0x96) = 0x10;
        }

        *(uint*)(e + 0x38) += 0xffe7e7e8;
        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 8) *(uint*)e = 0;
    }

    // =============================================================================================
    // LA GROTTE SCELLEE, LOT 7 (2026-09-20) — CE QUI BLESSE LE GROUPE, BLOCS 0 / 2-3 / 5-8-10
    //
    // Cinq formes de la categorie « C » de la zone 10 qui enlevent des points de vie a chaque
    // image : elles sont ici et non dans `Effects/SealedCaveParticles.cs` par le test
    // d'appartenance d'ARCHITECTURE.md, comme la flamme qui recule `0x0043C010` du lot 4 et la
    // braise `0x00416C40` de la vieille tour.
    // =============================================================================================

    // ---------------------------------------------------------------- les planches du lot 7

    /// <summary>Ghidra: <c>&amp;DAT_00550c88</c> (bloc 0), <c>&amp;DAT_00550d78</c> (2-3),
    /// <c>&amp;DAT_00550e88</c> (5-8-10) — <b>la pierre en vol</b> : une seule tuile
    /// <c>0x10 x 0x10</c> a <c>x = 0x48</c>, couche 3. Bornee en bas par le dernier enregistrement
    /// de la planche du grain qui monte (<c>0x00550c80</c>), en haut par la premiere des quatre
    /// tuiles <c>0x08 x 0x08</c> du grain (<c>0x00550c90</c>).
    ///
    /// <para><b>Marge de 1, prouvee</b> : le corps pose <c>+0x40 = 0</c> a la naissance et ne
    /// l'avance qu'a l'etat 3, ou la planche a deja change pour celle de la fumee.</para></summary>
    private static readonly byte[] s_Descriptors_00550C88 =
    [
        0x48, 0x00, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x00550c88  <- 0x004329D0
    ];

    private static readonly byte[] s_Descriptors_00550D78 =
    [
        0x48, 0x00, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x00550d78  <- 0x00433F00
    ];

    private static readonly byte[] s_Descriptors_00550E88 =
    [
        0x48, 0x00, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x00550e88  <- 0x00435AA0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550c08</c> (bloc 0) et <c>&amp;DAT_00550e08</c> (5-8-10) —
    /// <b>le coup qui tourne</b> : quatre tuiles <c>0x20 x 0x20</c> a <c>y = 0x88</c> couche 2,
    /// suivies de quatre <c>0x18 x 0x18</c> a <c>y = 0x30</c> couche 3 <b>dont l'octet 4 est nul</b>
    /// — la meme suite dans les deux blocs, octet pour octet. Bornees en bas par la derniere tuile
    /// de la fumee qui monte (<c>0x00550c00</c>), en haut par la poussiere qui s'ecarte
    /// (<c>0x00550c48</c>, octet 4 = 1).
    ///
    /// <para><b>Marge large</b> : le corps tire <c>+0x40</c> entre 1 et 3 et le fait tourner
    /// 1 → 2 → 3 → 1 ; il ne sort jamais des quatre premiers enregistrements.</para></summary>
    private static readonly byte[] s_Descriptors_00550C08 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550c08  <- 0x00431600
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550c10
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550c18
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550c20
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550c28
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550c30
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550c38
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550c40
    ];

    private static readonly byte[] s_Descriptors_00550E08 =
    [
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550e08  <- 0x00434510
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550e10
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550e18
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00550e20
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550e28
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550e30
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550e38
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550e40
    ];

    private static byte* s_Descriptors00550C88Storage;
    private static byte* s_Descriptors00550D78Storage;
    private static byte* s_Descriptors00550E88Storage;
    private static byte* s_Descriptors00550C08Storage;
    private static byte* s_Descriptors00550E08Storage;

    private static byte* Descriptors00550C88() => NativeCopy(s_Descriptors_00550C88, ref s_Descriptors00550C88Storage);
    private static byte* Descriptors00550D78() => NativeCopy(s_Descriptors_00550D78, ref s_Descriptors00550D78Storage);
    private static byte* Descriptors00550E88() => NativeCopy(s_Descriptors_00550E88, ref s_Descriptors00550E88Storage);
    private static byte* Descriptors00550C08() => NativeCopy(s_Descriptors_00550C08, ref s_Descriptors00550C08Storage);
    private static byte* Descriptors00550E08() => NativeCopy(s_Descriptors_00550E08, ref s_Descriptors00550E08Storage);

    /// <summary>
    /// Ghidra: FUN_00431c30 @ 0x00431c30 (bloc 0), <c>0x00433260</c> (2-3), <c>0x00434e90</c>
    /// (5-8-10) — table <c>0x0056b1c8</c>. Decompilees dans <c>Blaze.c</c> ; 102 instructions,
    /// aucune immediate differente d'un bloc a l'autre.
    ///
    /// <para>
    /// <b>Le mur qui balaie le couloir.</b> Il nait large de <c>4000</c> et haut de <c>0x2770</c>
    /// sur les deux autres axes, tourne d'un demi-tour et demi (<c>+0x32 += 0xc00</c>), et vit
    /// <c>0x1e</c> images. Tant qu'il reste plus de <c>0x1a</c> images il <b>s'aplatit</b> :
    /// <c>+0x28</c> gagne 2000 pendant que <c>+0x2a</c> et <c>+0x2c</c> en perdent autant. Sous 10
    /// images il s'assombrit de 10 par image, et sous zero il passe a l'etat 2, qui vide
    /// l'emplacement.
    /// </para>
    /// <para>
    /// <b>Son image change a chaque image</b> : <c>e[4]</c> est retire au hasard entre 8 et 11, a
    /// la naissance et a chaque tour.
    /// </para>
    /// <para>
    /// <b>La blessure est un couloir, pas une sphere.</b> Au-dela de la dixieme image restante, il
    /// frappe tout membre vivant dont la hauteur est sous <c>0x46000</c> et dont le <b>seul X</b>
    /// tient a moins de <c>0x28000</c> du sien — le Z n'est pas regarde : le mur traverse la salle
    /// de part en part. La victime perd 5 % de ses points de vie maximum
    /// (<see cref="ApplyPercentDamage_005397B0"/>), recoit le bit <c>0x40</c> <b>deux fois</b>, et
    /// clignote <c>0x3c</c> images.
    /// </para>
    /// </summary>
    private static void TickSweepingWall_00431C30(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int roll = CrtRand();                                   // Ghidra: FUN_00547140
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 4000;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x1e;
            *(short*)(e + 0x32) += 0xc00;
            e[4] = (byte)(SignedMask(roll, 3) + 8);
            *(short*)(e + 0x2c) = 0x2770;
            *(short*)(e + 0x2a) = 0x2770;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) -= 1;

            if (*(short*)(e + 0x12) > 0x1a)
            {
                *(short*)(e + 0x28) += 2000;
                *(short*)(e + 0x2a) -= 2000;
                *(short*)(e + 0x2c) -= 2000;
            }

            if (*(short*)(e + 0x12) < 10) BrightenEntityTint_00404DE0(e, -10, -10, -10);   // Ghidra: FUN_00404de0
            if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x10) = 2;

            e[4] = (byte)(SignedMask(CrtRand(), 3) + 8);

            if (*(short*)(e + 0x12) > 10)
            {
                byte* member = Inp.s_SlotFlags_005e93c0;            // 0x005e93c0, quatre fois 0x9c

                for (int i = 4; i != 0; i--, member += 0x9c)
                {
                    uint flags = *(uint*)member;
                    if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
                    if (*(int*)(member + 0x20) >= 0x46000) continue;

                    if (*(int*)(e + 0x1c) - 0x28000 >= *(int*)(member + 0x1c)) continue;
                    if (*(int*)(member + 0x1c) >= *(int*)(e + 0x1c) + 0x28000) continue;
                    if (*(short*)(member + 0x96) != 0) continue;

                    *(uint*)member = flags | 0x40;
                    ApplyPercentDamage_005397B0((uint*)member, 5, 0);   // Ghidra: FUN_005397b0
                    *(uint*)member |= 0x40;
                    *(short*)(member + 0x96) = 0x3c;
                }
            }
        }

        if (*(short*)(e + 0x10) == 2) *(uint*)e &= 0x7fffffff;
    }

    private static void EntityHandler_00431C30(byte* e) => TickSweepingWall_00431C30(e);
    private static void EntityHandler_00433260(byte* e) => TickSweepingWall_00431C30(e);
    private static void EntityHandler_00434E90(byte* e) => TickSweepingWall_00431C30(e);

    /// <summary>
    /// Ghidra: FUN_00432390 @ 0x00432390 — table <c>0x0056b0c8</c>, sous-zones 0, 2, 3, 5, 8 et 10
    /// (le <b>meme</b> creneau dans les trois tables du bloc). Decompilee dans <c>Blaze.c</c> ;
    /// 108 instructions.
    ///
    /// <para>
    /// <b>Le vide sous la salle.</b> A sa premiere image elle retient les trois shorts de fond
    /// (<c>0x005e92b8</c>..<c>0x005e92bc</c>) dans <c>+0x50</c>..<c>+0x58</c> — <b>et ne les relit
    /// jamais</b> —, puis peint le fond en <c>(0x96, 0x32, 0xfa)</c>, mode 2 : le violet de la
    /// grotte.
    /// </para>
    /// <para>
    /// Ensuite, a chaque image, <b>tout membre vivant tombe sous <c>-0xc8000</c> perd 3 % de ses
    /// points de vie maximum</b> (<see cref="ApplyPercentDamage_005397B0"/>) — sans clignotement ni
    /// bit de coup : la chute ronge, elle ne frappe pas. Le compteur <c>+0x12</c> tourne de 0 a
    /// 2000, et <b>une image sur deux</b> (son octet bas impair) elle fait naitre le sous-type
    /// <c>0xef</c> en cinq points fixes de la salle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00432390(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);
            SetSceneBackdrop(0x96, 0x32, 0xfa, 2);                  // Ghidra: DAT_005e9160..66
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        byte* member = Inp.s_SlotFlags_005e93c0;                    // 0x005e93c0

        for (int i = 4; i != 0; i--, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            if (*(int*)(member + 0x20) >= -0xc8000) continue;

            ApplyPercentDamage_005397B0((uint*)member, 3, 0);       // Ghidra: FUN_005397b0
        }

        if ((e[0x12] & 1) != 0)
        {
            SpawnPitFlame(e, unchecked((int)0xffc7c000), unchecked((int)0xffde4000), unchecked((int)0xff3e4000));
            SpawnPitFlame(e, unchecked((int)0xffce0000), unchecked((int)0xffed4000), unchecked((int)0xff6a0000));
            SpawnPitFlame(e, unchecked((int)0xfff9c000), unchecked((int)0xffe0c000), unchecked((int)0xff574000));
            SpawnPitFlame(e, 0x00064000, unchecked((int)0xffed4000), unchecked((int)0xff704000));
            SpawnPitFlame(e, 0x002bc000, unchecked((int)0xffeac000), unchecked((int)0xff574000));
        }

        *(short*)(e + 0x12) += 1;
        if (*(short*)(e + 0x12) > 2000) *(short*)(e + 0x12) = 0;
    }

    /// <summary>Les cinq naissances du vide : le binaire ecrit le point dans <b>sa propre
    /// position</b> <c>+0x1c</c>..<c>+0x24</c> puis passe cette adresse a la naissance — le decor
    /// se deplace donc le temps de l'appel, et garde le dernier des cinq points.</summary>
    private static void SpawnPitFlame(byte* e, int x, int y, int z)
    {
        *(int*)(e + 0x1c) = x;
        *(int*)(e + 0x20) = y;
        *(int*)(e + 0x24) = z;
        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xef, 0, -1);   // Ghidra: FUN_0047ac40
    }

    /// <summary>
    /// Ghidra: FUN_00432630 @ 0x00432630 (bloc 0), <c>0x00433b60</c> (2-3), <c>0x00435700</c>
    /// (5-8-10) — table <c>0x0056b2c8</c>. Decompilees dans <c>Blaze.c</c> ; 114 instructions, a la
    /// planche pres.
    ///
    /// <para>
    /// <b>Le nuage acide qui derive.</b> Planche de quatre tuiles <c>0x18 x 0x18</c>
    /// (<c>0x00550c48</c>), image <c>e[0x40]</c> ouverte a 1, teinte <c>0x40</c> sur les trois
    /// canaux, echelle tiree entre <c>0x800</c> et <c>0x9ff</c>. Il nait <c>0x3c</c> unites plus
    /// haut que son point de depart, avec une derive tiree : de <c>-5</c> a <c>+4</c> par image en
    /// X et en Z, de <c>+5</c> a <c>+9</c> en Y — il <b>monte toujours</b>. Le son <c>0x0b</c>
    /// part a la naissance, identifiant brut et non voix de zone.
    /// </para>
    /// <para>
    /// Chaque image il blesse le groupe autour de lui de 5 % dans un rayon de <c>0x28</c>
    /// (<see cref="DamagePartyNearPoint_00418F50"/>), s'assombrit de 3 et derive. Tous les
    /// <c>+0x14</c> tours (3 a 6, tires une fois) il gagne <c>0x100</c> d'echelle et <b>change
    /// d'image</b> ; a la quatrieme il s'eteint.
    /// </para>
    /// </summary>
    private static void TickDriftingAcidCloud(byte* e, byte* descriptors, short gain)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = descriptors;
            e[0x40] = 1;
            e[0x0a] = 4;
            e[0x0b] = 0;

            short scale = (short)(SignedMask(CrtRand(), 0x1ff) + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x50) = (CrtRand() % 10 - 5) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % 10 - 5) * 0x1000;

            int climb = CrtRand();
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x54) = (climb % 5 + 5) * 0x1000;
            *(int*)(e + 0x20) += 0x3c000;

            *(short*)(e + 0x14) = (short)(SignedMask(CrtRand(), 3) + 3);
            TryPlaySoundNearEntity((uint*)e, 0x0b, 0x200, gain, 0, 0);   // Ghidra: FUN_00468080
            e[0x38] = 0x40;
            e[0x39] = 0x40;
            e[0x3a] = 0x40;
        }

        if (*(short*)(e + 0x10) != 1) return;

        DamagePartyNearPoint_00418F50(*(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24), 0x28, 5, 0);   // Ghidra: FUN_00418f50
        BrightenEntityTint_00404DE0(e, -3, -3, -3);                 // Ghidra: FUN_00404de0

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(short*)(e + 0x12) += 1;
        *(int*)(e + 0x24) += *(int*)(e + 0x58);

        if (*(short*)(e + 0x14) >= *(short*)(e + 0x12)) return;

        *(short*)(e + 0x28) += 0x100;
        *(short*)(e + 0x2a) += 0x100;
        byte frame = (byte)((sbyte)e[0x40] + 1);
        *(short*)(e + 0x12) = 0;
        e[0x40] = frame;
        if (frame > 3) *(uint*)e &= 0x7fffffff;
    }

    private static void EntityHandler_00432630(byte* e) => TickDriftingAcidCloud(e, Descriptors00550C48(), 0x600);
    private static void EntityHandler_00433B60(byte* e) => TickDriftingAcidCloud(e, Descriptors00550D38(), 0x600);
    private static void EntityHandler_00435700(byte* e) => TickDriftingAcidCloud(e, Descriptors00550E48(), 0x600);

    /// <summary>
    /// Ghidra: FUN_004329d0 @ 0x004329d0 (bloc 0), <c>0x00433f00</c> (2-3), <c>0x00435aa0</c>
    /// (5-8-10) — table <c>0x0056b2c8</c>. Decompilees dans <c>Blaze.c</c> ; 213 instructions, aux
    /// deux planches pres.
    ///
    /// <para>
    /// <b>La pierre qui tombe en oblique et eclate.</b> A la naissance elle monte de <c>0xb4</c>,
    /// s'ecarte de 20 unites sur le cap de l'entite plus un demi-tour, et se donne une vitesse : les
    /// composantes horizontales sont ce meme cap au <c>1/32</c> pres, la verticale est la distance
    /// au point vise (<c>+0x50</c>..<c>+0x58</c>) divisee par le trente-deuxieme de la portee —
    /// <b>et forcee a zero si elle est positive</b>, pour qu'elle ne remonte jamais.
    /// </para>
    /// <para>
    /// A partir de la huitieme image elle se pose sur le decor : la huitieme exactement, elle
    /// s'accroche a la grille (<see cref="TryPlaceEntity"/>) et se donne un gabarit de <c>0xa000</c>
    /// aux quatre coins ; ensuite, chaque image, <see cref="FinalizeEntityPlacement2"/> et un
    /// contact <c>0x8802</c> la font eclater. Sans contact elle meurt d'elle-meme a la soixantieme.
    /// </para>
    /// <para>
    /// <b>L'eclatement</b> (etats 2 et 3) : planche de fumee <c>0x00550bd0</c>, un saut de
    /// <c>-15</c> a <c>+14</c> unites sur les trois axes, la voix <c>0x29</c> de la zone, puis
    /// l'image <c>e[0x40]</c> avance de <c>+0x14 / 4</c> par tour et l'emplacement se vide quand
    /// elle vaut <b>exactement</b> 3. <c>+0x14</c> est lu ici <b>en mot de 32 bits</b> alors qu'il
    /// n'est tenu qu'en 16 — le haut, <c>+0x16</c>, entre donc dans le decalage. Releve au
    /// desassembleur (<c>mov eax,[esi+14h]</c> ; <c>sar eax,2</c>) et transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Elle blesse dans tous ses etats</b>, y compris pendant la chute et apres l'eclat : 1 %
    /// dans un rayon de <c>0x28</c> a chaque image, hors de tout test d'etat.
    /// </para>
    /// </summary>
    private static void TickObliqueFallingRock(byte* e, byte* flight, byte* burst, bool landsOnGeometry, short burstGain)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x2e) = unchecked((short)0xffec);
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = flight;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(int*)(e + 0x20) += 0xb4000;

            short course = (short)(*(short*)(e + 0x32) + 0x800);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 0x14;   // Ghidra: Ordinal_451
            *(short*)(e + 0x28) = 0x1000;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 0x14;        // Ghidra: Ordinal_450
            *(short*)(e + 0x2a) = 0x1000;

            int dx = (*(int*)(e + 0x50) >> 0xc) - (*(int*)(e + 0x1c) >> 0xc);
            int dy = (*(int*)(e + 0x54) >> 0xc) - (*(int*)(e + 0x20) >> 0xc);
            int dz = (*(int*)(e + 0x58) >> 0xc) - (*(int*)(e + 0x24) >> 0xc);
            int range = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

            int fall = (*(int*)(e + 0x54) - *(int*)(e + 0x20)) / (range >> 5);
            *(int*)(e + 0x48) = fall;
            if (fall > 0) *(int*)(e + 0x48) = 0;

            *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(course) << 5;
            *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(course) << 5;
            *(short*)(e + 0x14) = 0;
            e[0x40] = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int before = *(int*)(e + 0x20);

            *(int*)(e + 0x1c) += *(int*)(e + 0x44);
            *(int*)(e + 0x20) += *(int*)(e + 0x48);
            *(int*)(e + 0x24) += *(int*)(e + 0x4c);

            if (landsOnGeometry)
            {
                if (*(short*)(e + 0x14) > 7)
                {
                    if (*(short*)(e + 0x14) == 8)
                    {
                        *(int*)(e + 0x64) = *(int*)(e + 0x20);
                        *(short*)(e + 0x82) = unchecked((short)0xa000);
                        *(short*)(e + 0x86) = unchecked((short)0xa000);
                        *(short*)(e + 0x8a) = unchecked((short)0xa000);
                        *(short*)(e + 0x8e) = unchecked((short)0xa000);
                        *(int*)(e + 0x60) = *(int*)(e + 0x20);
                        TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);   // Ghidra: FUN_005330d0
                        FinalizeEntityPlacement2((uint*)e);                                  // Ghidra: FUN_00533940
                    }

                    FinalizeEntityPlacement2((uint*)e);                                      // Ghidra: FUN_00533940
                    if ((*(uint*)(e + 0x90) & 0x8802) != 0) *(short*)(e + 0x10) = 2;
                }
            }
            else
            {
                // Le produit change de signe quand la hauteur visee est franchie : la chute
                // s'arrete au niveau de +0x54, sans consulter le decor.
                if ((*(int*)(e + 0x48) >> 0xc) * ((before + *(int*)(e + 0x48) - *(int*)(e + 0x54)) >> 0xc) > 0)
                {
                    *(short*)(e + 0x10) = 2;
                }
            }

            *(short*)(e + 0x14) += 1;
            if (*(short*)(e + 0x14) > 0x3c) *(uint*)e &= 0x7fffffff;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            *(byte**)(e + 0x3c) = burst;
            e[0x0a] = 9;
            *(int*)(e + 0x1c) += (CrtRand() % 0x1e - 0xf) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x1e - 0xf) * 0x1000;

            int scatter = CrtRand();
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            *(short*)(e + 0x14) = 0;
            e[0x40] = 0;
            *(int*)(e + 0x24) += (scatter % 0x1e - 0xf) * 0x1000;

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x29), 0x800, burstGain, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x14) += 1;
            byte frame = (byte)((sbyte)e[0x40] + (sbyte)(*(int*)(e + 0x14) >> 2));
            e[0x40] = frame;
            if (frame == 3) *(uint*)e &= 0x7fffffff;
        }

        DamagePartyNearPoint_00418F50(*(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24), 0x28, 1, 0);   // Ghidra: FUN_00418f50
    }

    private static void EntityHandler_004329D0(byte* e) => TickObliqueFallingRock(e, Descriptors00550C88(), Descriptors00550BD0(), landsOnGeometry: true, 0x300);   // &DAT_00550c88, &DAT_00550bd0
    private static void EntityHandler_00433F00(byte* e) => TickObliqueFallingRock(e, Descriptors00550D78(), Descriptors00550CE0(), landsOnGeometry: true, 0x300);   // &DAT_00550d78, &DAT_00550ce0
    private static void EntityHandler_00435AA0(byte* e) => TickObliqueFallingRock(e, Descriptors00550E88(), Descriptors00550DD0(), landsOnGeometry: true, 0x300);   // &DAT_00550e88, &DAT_00550dd0

    /// <summary>
    /// Ghidra: <c>0x00431600</c> (bloc 0) et <c>0x00434510</c> (5-8-10) — table <c>0x0056b2c8</c>.
    /// <b>Absents de <c>Blaze.c</c></b> ; 188 et 191 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00431600.asm</c>. Les deux corps ne different que par
    /// <b>deux choses</b> : la planche, et le repli du lanceur — le second remplace un
    /// <c>+0x74</c> nul par l'entite par defaut <c>0x005de558</c>, le premier le lit tel quel.
    ///
    /// <para>
    /// <b>Le coup qui part en tournant et s'enfonce.</b> Planche de huit enregistrements dont il
    /// n'emploie que les images 1 a 3 (<c>e[0x40]</c>, tire entre 1 et 3, puis avance toutes les 5
    /// images et repart a 1 apres 3). Teinte <c>0xb4 / 0xb4 / 0x82</c>, echelles a <c>0x5dc</c> qui
    /// gagnent <c>0xc8</c> par image.
    /// </para>
    /// <para>
    /// Il tourne autour de son point de naissance sur un rayon <c>+0x12</c> qui s'ouvre de
    /// <c>0x14</c> par image, <b>descend de <c>0x6000</c></b> a chaque tour et s'assombrit de 7. Il
    /// s'arrete — etat 2, l'emplacement se vide — quand sa teinte rouge atteint zero, quand
    /// <see cref="FinalizeEntityPlacement2"/> signale un contact <c>0x8802</c>, ou quand le sol
    /// <c>+0x60</c> le rattrape.
    /// </para>
    /// <para>
    /// <b>La frappe est amortie d'un quart.</b> Un membre vivant, de <c>+0x12</c> nul, a moins de
    /// <c>0x50</c> unites (la hauteur relevee de <c>0x3c</c>) et sans clignotement en cours est
    /// frappe — sauf s'il <b>pare</b> (<see cref="IsGuardingToward_004375A0"/>), auquel cas c'est le
    /// coup qui meurt. Sinon le corps abaisse le <c>+0x30</c> du bloc de modele du lanceur d'un
    /// quart, resout le coup (<see cref="ResolveMeleeHit_00539B80"/>), <b>remet la valeur</b>, puis
    /// pose le bit <c>0x40</c> et <c>0x10</c> images de clignotement.
    /// </para>
    /// </summary>
    private static void TickSpinningSinkingStrike(byte* e, byte* descriptors, bool defaultOwner)
    {
        byte* owner = *(byte**)(e + 0x74);
        if (defaultOwner && owner == null) owner = Res.s_EntityList_005de558;   // Ghidra: &DAT_005de558

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = descriptors;
            int frame = CrtRand() % 3;
            e[0x0a] = 2;
            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0x5dc;
            *(short*)(e + 0x2a) = 0x5dc;
            e[0x3a] = 0x82;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 5;
            e[0x40] = (byte)(frame + 1);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short course = *(short*)(e + 0x32);
            short radius = *(short*)(e + 0x12);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);
            *(int*)(e + 0x20) += unchecked((int)0xffffa000);
            *(short*)(e + 0x28) += 0xc8;
            *(short*)(e + 0x2a) += 0xc8;
            BrightenEntityTint_00404DE0(e, -7, -7, -7);             // Ghidra: FUN_00404de0

            if (e[0x38] == 0) *(short*)(e + 0x10) = 2;

            FinalizeEntityPlacement2((uint*)e);                     // Ghidra: FUN_00533940
            if ((*(uint*)(e + 0x90) & 0x8802) != 0) *(short*)(e + 0x10) = 2;
            if (*(int*)(e + 0x60) >= *(int*)(e + 0x20)) *(short*)(e + 0x10) = 2;

            *(short*)(e + 0x14) -= 1;
            short left = *(short*)(e + 0x14);
            *(short*)(e + 0x12) += 0x14;

            if (left <= 0)
            {
                *(short*)(e + 0x14) = 5;
                byte next = (byte)(e[0x40] + 1);
                e[0x40] = next;
                if (next == 4) e[0x40] = 1;
            }

            byte* member = Inp.s_SlotFlags_005e93c0;                // 0x005e93c0

            for (int i = 4; i != 0; i--, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
                if (*(short*)(member + 0x12) != 0) continue;

                int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dy = (*(int*)(member + 0x20) - *(int*)(e + 0x20) + 0x3c000) >> 0xc;
                int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
                if (*(short*)(member + 0x96) != 0) continue;
                if (distance >= 0x50) continue;

                if (IsGuardingToward_004375A0(member, *(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24)) != 0)
                {
                    *(uint*)e &= 0x7fffffff;
                    continue;
                }

                ResolveEntityBlocks(owner, out _, out _, out byte* model);   // Ghidra: FUN_00538980
                short attack = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(attack - (attack >> 2));
                ResolveMeleeHit_00539B80((uint*)owner, (uint*)member);       // Ghidra: FUN_00539b80
                *(short*)(model + 0x30) = attack;

                *(short*)(member + 0x96) = 0x10;
                *(uint*)member |= 0x40;
            }
        }

        if (*(short*)(e + 0x10) == 2) *(uint*)e &= 0x7fffffff;
    }

    private static void EntityHandler_00431600(byte* e) => TickSpinningSinkingStrike(e, Descriptors00550C08(), defaultOwner: false);
    private static void EntityHandler_00434510(byte* e) => TickSpinningSinkingStrike(e, Descriptors00550E08(), defaultOwner: true);

    // =============================================================================================
    // LA GROTTE SCELLEE, LOT 8 (2026-09-20) — CE QUI BLESSE LE GROUPE, BLOCS 4-9 ET 6-7
    // =============================================================================================

    /// <summary>Ghidra: <c>&amp;DAT_005510c8</c> — sous-zones 6 et 7, la poussiere qui s'ecarte :
    /// quatre tuiles <c>0x18 x 0x18</c> a <c>y = 0x30</c> couche 2, les memes que
    /// <c>0x00550c48</c>. Bornees en haut par une <c>0x10 x 0x10</c>.</summary>
    private static readonly byte[] s_Descriptors_005510C8 =
    [
        0x48, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005510c8  <- 0x0043B060
        0x60, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005510d0
        0x78, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005510d8
        0x90, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005510e0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00551068</c> (sous-zones 6-7) et <c>&amp;DAT_00551150</c>
    /// (sous-zone 1) — <b>la pierre en vol</b>, une tuile <c>0x10 x 0x10</c> a <c>x = 0x48</c>
    /// couche 3, la meme que <c>0x00550c88</c> des autres blocs. Bornees en haut par le grain
    /// (<c>0x00551070</c>) et par une <c>0x20 x 0x20</c> couche 2 (<c>0x00551158</c>).</summary>
    private static readonly byte[] s_Descriptors_00551068 =
    [
        0x48, 0x00, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x00551068  <- 0x0043B400
    ];

    private static readonly byte[] s_Descriptors_00551150 =
    [
        0x48, 0x00, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x00551150  <- 0x0043C120
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550fd0</c> — <b>l'eclat de la pierre</b>, sous-zones 6 et 7 :
    /// sept tuiles <c>0x20 x 0x20</c> a <c>y = 0x88</c> couche 3, la meme suite que
    /// <c>0x00550bd0</c>. Bornee par la gerbe en arc (<c>0x00551008</c>).</summary>
    private static readonly byte[] s_Descriptors_00550FD0 =
    [
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550fd0  <- 0x0043B400
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550fd8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550fe0
        0x40, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550fe8
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550ff0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00550ff8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00551000
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00551138</c> — le meme eclat, sous-zone 1, mais <b>trois
    /// enregistrements seulement</b> : la pierre en vol (<c>0x00551150</c>) le borne. Marge exacte
    /// — <c>+0x40</c> va de 0 a 2 et l'emplacement se vide quand il atteint 3.</summary>
    private static readonly byte[] s_Descriptors_00551138 =
    [
        0x60, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00551138  <- 0x0043C120
        0x80, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00551140
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x03, 0x10, 0x00,   // 0x00551148
    ];

    private static byte* s_Descriptors005510C8Storage;
    private static byte* s_Descriptors00551068Storage;
    private static byte* s_Descriptors00551150Storage;
    private static byte* s_Descriptors00550FD0Storage;
    private static byte* s_Descriptors00551138Storage;

    private static byte* Descriptors005510C8() => NativeCopy(s_Descriptors_005510C8, ref s_Descriptors005510C8Storage);
    private static byte* Descriptors00551068() => NativeCopy(s_Descriptors_00551068, ref s_Descriptors00551068Storage);
    private static byte* Descriptors00551150() => NativeCopy(s_Descriptors_00551150, ref s_Descriptors00551150Storage);
    private static byte* Descriptors00550FD0() => NativeCopy(s_Descriptors_00550FD0, ref s_Descriptors00550FD0Storage);
    private static byte* Descriptors00551138() => NativeCopy(s_Descriptors_00551138, ref s_Descriptors00551138Storage);

    /// <summary>Ghidra: <c>0x0043b060</c> — sous-zones 6 et 7. Le nuage acide du lot 7 a la planche
    /// et au gain pres (<c>0x800</c> au lieu de <c>0x600</c>).</summary>
    private static void EntityHandler_0043B060(byte* e) => TickDriftingAcidCloud(e, Descriptors005510C8(), 0x800);

    /// <summary>Ghidra: <c>0x0043b400</c> (sous-zones 6 et 7) et <c>0x0043c120</c> (sous-zone 1) —
    /// la pierre oblique du lot 7 a <b>trois</b> choses pres : les deux planches, le gain de l'eclat
    /// (<c>0x600</c>), et surtout <b>la facon de s'arreter</b>. Ces deux-la ne consultent pas le
    /// decor : elles eclatent quand le produit <c>(+0x48 &gt;&gt; 12) * ((nouvelle hauteur - +0x54)
    /// &gt;&gt; 12)</c> devient positif, c'est-a-dire quand la hauteur visee est franchie.
    /// Decompilees dans <c>Blaze.c</c> ; 194 instructions chacune.</summary>
    private static void EntityHandler_0043B400(byte* e)
        => TickObliqueFallingRock(e, Descriptors00551068(), Descriptors00550FD0(), landsOnGeometry: false, 0x600);

    private static void EntityHandler_0043C120(byte* e)
        => TickObliqueFallingRock(e, Descriptors00551150(), Descriptors00551138(), landsOnGeometry: false, 0x600);

    /// <summary>
    /// Ghidra: FUN_0043abf0 @ 0x0043abf0 — sous-zones 6 et 7. Decompilee dans <c>Blaze.c</c> ;
    /// 123 instructions.
    ///
    /// <para>
    /// <b>Le pilon qui tombe et fait trembler la salle.</b> Son image <c>e[4]</c> est l'octet bas du
    /// mot <c>+0x14</c> que la sous-zone lui a posé — le binaire lit le <b>dword</b> et n'en garde
    /// qu'un octet. Il part cap et rotation de face remis a <c>0</c> et <c>0xc00</c>, tourne de
    /// <c>0xfa</c> par image et tombe d'une vitesse qui gagne 8 unites par image.
    /// </para>
    /// <para>
    /// Chaque image il frappe tout membre vivant, de <c>+0x12</c> nul, a moins de <c>0x78</c> unites
    /// en 3D (<see cref="Distance3D_0043AD70"/>) et sans clignotement : 20 % des points de vie
    /// maximum, le bit <c>0x40</c> et <c>0x3c</c> images de clignotement.
    /// </para>
    /// <para>
    /// <b>L'impact</b> : sous <c>+0x64 + 0x14000</c> — le sol que le placement lui a donne —, il s'y
    /// pose, prend le bit <c>0x4000</c>, fait naitre <c>0xf8</c>, sonne (<c>0x1e</c> brut, gain
    /// <c>0x1800</c>) et passe a l'etat 2 : vingt images de secousse de camera <b>alternee</b> —
    /// l'amplitude se rapproche de zero d'une unite par image et change de signe a chaque fois.
    /// </para>
    /// </summary>
    private static void EntityHandler_0043ABF0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[4] = (byte)*(int*)(e + 0x14);
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x34) = 0xc00;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) += 0xfa;
            *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x1000;
            *(short*)(e + 0x12) += 8;

            byte* member = Inp.s_SlotFlags_005e93c0;                // 0x005e93c0

            for (int i = 4; i != 0; i--, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
                if (*(short*)(member + 0x12) != 0) continue;

                int distance = Distance3D_0043AD70((int*)(e + 0x1c), (int*)(member + 0x1c));
                if (distance >= 0x78 || *(short*)(member + 0x96) != 0) continue;

                *(uint*)member |= 0x40;
                ApplyPercentDamage_005397B0((uint*)member, 0x14, 0);   // Ghidra: FUN_005397b0
                *(uint*)member |= 0x40;
                *(short*)(member + 0x96) = 0x3c;
            }

            if (*(int*)(e + 0x20) < *(int*)(e + 0x64) + 0x14000)
            {
                *(int*)(e + 0x20) = *(int*)(e + 0x64) + 0x14000;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x14) = 0x14;
                *(uint*)e |= 0x4000;
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf8, 0, -1);   // Ghidra: FUN_0047ac40
                TryPlaySoundNearEntity((uint*)e, 0x1e, 0x300, 0x1800, 0, 0);                // Ghidra: FUN_00468080
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        short jolt = (short)(*(short*)(e + 0x14) + (*(short*)(e + 0x14) < 0 ? 1 : -1));
        *(short*)(e + 0x14) = jolt;
        jolt = (short)-jolt;
        *(short*)(e + 0x14) = jolt;

        TryPublishCameraEffectNearEntity((uint*)e, 999, jolt, 999, 0, 0);   // Ghidra: FUN_0041be60
        if (*(short*)(e + 0x14) == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: FUN_0043ad70 @ 0x0043ad70 — <b>la distance entre deux triplets</b>, en
    /// unites entieres. Decompilee dans <c>Blaze.c</c>, ou son type de retour est perdu : le corps
    /// finit sur <c>Ordinal_442(...)</c> dont la valeur reste dans <c>eax</c>, et
    /// <see cref="EntityHandler_0043ABF0"/> la compare a <c>0x78</c>. Les sites d'appel poussent
    /// <b>trois</b> arguments ; le corps n'en lit que deux.</summary>
    private static int Distance3D_0043AD70(int* a, int* b)
    {
        int dx = (a[0] >> 0xc) - (b[0] >> 0xc);
        int dz = (a[2] >> 0xc) - (b[2] >> 0xc);
        int dy = (a[1] >> 0xc) - (b[1] >> 0xc);

        return (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
    }

    /// <summary>
    /// Ghidra: FUN_00438850 @ 0x00438850 — sous-zones 4 et 9. Decompilee dans <c>Blaze.c</c> ;
    /// 129 instructions.
    ///
    /// <para>
    /// <b>La detonation qui rend la main a son porteur.</b> Dix images, pas une de plus. A la
    /// premiere, le son <c>0x0b</c> en direct (<see cref="Aud"/>, pas la voix de zone). Toujours a
    /// la premiere — le compteur <c>+0x16</c> valant zero —, deux incantations <c>0xc2</c> lancees
    /// avec <c>+0x14</c> mis a <b>7</b> puis a <b>1</b>, la valeur d'origine etant rendue ensuite :
    /// <c>+0x14</c> est le canal que l'incantation lit.
    /// </para>
    /// <para>
    /// Les six premieres images, trois naissances de <c>0xf7</c> chacune — dix-huit eclats. A la
    /// dixieme, l'emplacement se vide, <b>tout membre a moins de 300 unites en plan du porteur</b>
    /// (<c>+0x6c</c>, ou l'entite par defaut) perd 10 % de ses points de vie maximum, et le porteur
    /// recoit dans son <c>+0x10</c> le <c>+0x98</c> de la detonation : <b>elle lui dit dans quel
    /// etat reprendre</b>.
    /// </para>
    /// <para>
    /// <b>Le test d'etat de la premiere image est ecrit a l'envers</b> : le binaire compare
    /// <c>+0x16</c> a zero alors qu'il vient de passer l'etat a 2 — la branche se reprend donc a
    /// chaque image ou <c>+0x16</c> vaut zero, ce qui n'arrive qu'une fois. Conserve tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00438850(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            Aud.PlaySoundEffect(0x0b, 0x300, 0x1200, 0x1200);       // Ghidra: FUN_0053cea0
        }

        if (*(short*)(e + 0x16) == 0)
        {
            int channel = *(int*)(e + 0x14);
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x14) = 7;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);               // Ghidra: FUN_0047aae0
            *(short*)(e + 0x14) = 1;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);               // Ghidra: FUN_0047aae0
            *(short*)(e + 0x14) = (short)channel;
        }

        if (*(short*)(e + 0x16) >= 0 && *(short*)(e + 0x16) < 6)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf7, 0, -1);   // Ghidra: FUN_0047ac40
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf7, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf7, 0, -1);
        }

        *(short*)(e + 0x16) += 1;
        if (*(short*)(e + 0x16) <= 9) return;

        byte* owner = *(byte**)(e + 0x6c);
        *(uint*)e &= 0x7fffffff;
        if (owner == null) owner = Res.s_EntityList_005de558;       // Ghidra: &DAT_005de558

        byte* member = Inp.s_SlotFlags_005e93c0;                    // 0x005e93c0

        for (int i = 4; i != 0; i--, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(owner + 0x1c) >> 0xc) - (*(int*)(member + 0x1c) >> 0xc);
            int dz = (*(int*)(owner + 0x24) >> 0xc) - (*(int*)(member + 0x24) >> 0xc);

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) >= 300) continue;
            if (*(short*)(member + 0x96) != 0) continue;

            *(uint*)member |= 0x40;
            ApplyPercentDamage_005397B0((uint*)member, 10, 0);      // Ghidra: FUN_005397b0
            *(uint*)member |= 0x40;
            *(short*)(member + 0x96) = 0x3c;
        }

        *(short*)(owner + 0x10) = *(short*)(e + 0x98);
    }

    // =============================================================================================
    // LOT 8, seconde moitie — ce qui blesse, releve au desassembleur
    // =============================================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00552748</c> — le script de sons differes de
    /// <see cref="EntityHandler_00485450"/> : deux enregistrements, voix <c>0x36</c> a deux images
    /// d'intervalle, le second portant le delai nul qui ferme la file.</summary>
    private static readonly byte[] s_SoundScript_00552748 =
    [
        0x36, 0x02, 0x01, 0x00, 0xd3, 0x03, 0x00, 0x18,   // 0x00552748
        0x36, 0x00, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552750 — delai nul : fin
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005526d0</c> — le script de <see cref="EntityHandler_00484860"/>,
    /// deux enregistrements de voix <c>0x25</c>. Il precede l'emetteur <c>0x005526e0</c> dans
    /// <c>.data</c>.</summary>
    private static readonly byte[] s_SoundScript_005526D0 =
    [
        0x25, 0x02, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x005526d0
        0x25, 0x00, 0x01, 0x00, 0xd3, 0x03, 0x00, 0x18,   // 0x005526d8 — delai nul : fin
    ];

    private static byte* s_SoundScript00552748Storage;
    private static byte* s_SoundScript005526D0Storage;

    /// <summary>
    /// Ghidra: <c>0x00485450</c> — sous-zones 4 et 9. <b>Absente de <c>Blaze.c</c></b> ;
    /// 124 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_00485450.asm</c>.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    /// <b>L'impact qui secoue et empoisonne.</b> Dix images, pas une de plus. A la premiere, elle
    /// range son porteur <c>+0x6c</c> dans <c>+0x74</c>, se donne l'echelle <c>0x2000</c>, la teinte
    /// <c>0x606060</c>, la pose 7, met la <b>secousse de camera <c>0x005de0ac</c> a 10</b> et range
    /// le script de sons <c>0x00552748</c> dans la file differee. Aux images suivantes, la secousse
    /// decroit en changeant de signe : <c>s = (s &gt;&gt; 2) - s</c>.
    /// </para>
    /// <para>
    /// <b>Chaque image elle seme quatre eclats de piege</b> : un cap tire au hasard, un rayon de 0 a
    /// 199, une vitesse de 40 unites sur ce cap et <c>0x1e000</c> vers le haut, et le sous-type
    /// <c>0xbe</c> sur l'atlas de la gerbe de piege (<c>0x005519d0</c>) — le meme que le coffre
    /// piege de la foret.
    /// </para>
    /// <para>
    /// <b>Et elle frappe</b> : le gabarit <c>{0, -0xc8, 0xc8, 0x96, 0, 0x190}</c> cherche une
    /// victime (<see cref="ProbeMeleeTarget_00474CA0"/>) ; elle prend le coup de
    /// <see cref="ResolveMeleeHit_00539B80"/> au compte du porteur, et <b>trente fois sur cent</b>
    /// l'effet de piege (<see cref="ApplyTrapEffect_0053C420"/>) avec le canal 7. Puis le bit
    /// <c>0x40</c> et huit images de clignotement.
    /// </para>
    /// </remarks>
    private static void EntityHandler_00485450(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = unchecked((short)0xff38);
        probe[2] = 0xc8;
        probe[3] = 0x96;
        probe[4] = 0;
        probe[5] = 0x190;

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);
            *(int*)(e + 0x70) = 0;
            *(short*)(e + 0x12) = 3;
            *(short*)(e + 0x28) = 0x2000;
            *(short*)(e + 0x2a) = 0x2000;
            *(short*)(e + 0x14) = 0x4000;
            *(short*)(e + 0x16) = 2;
            *(uint*)(e + 0x38) = 0x01606060;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;
            *(short*)(e + 0x1a) = 7;

            *(int*)Mem.Camera(0x005de0ac) = 0x0a;                   // Ghidra: DAT_005de0ac
            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_00552748, ref s_SoundScript00552748Storage),
                (int*)(e + 0x1c));                                  // Ghidra: FUN_0053dbe0
        }
        else
        {
            int shake = *(int*)Mem.Camera(0x005de0ac);
            *(int*)Mem.Camera(0x005de0ac) = (shake >> 2) - shake;
        }

        for (int i = 4; i != 0; i--)
        {
            short course = (short)CrtRand();
            int radius = CrtRand() % 0xc8;

            int* burst = stackalloc int[3];
            burst[0] = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x1c);
            burst[1] = *(int*)(e + 0x20);
            burst[2] = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x24);

            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * 40;
            *(int*)(e + 0x54) = 0x1e000;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(course) * 40;

            SpawnStatusEffectEntity_0047AC40(e, burst, TrapBurstDescriptors(), 0xbe, 0, 3);   // Ghidra: FUN_0047ac40
        }

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);          // Ghidra: FUN_00474ca0

        if (victim != null)
        {
            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);   // Ghidra: FUN_00539b80

            if (CrtRand() % 100 < 0x1e)
            {
                byte* caster = *(byte**)(e + 0x74);
                caster[7] = 7;
                ApplyTrapEffect_0053C420(*(uint**)(e + 0x74), (uint*)victim, 0);   // Ghidra: FUN_0053c420
            }

            *(short*)(victim + 0x96) = 8;
            *(uint*)victim |= 0x40;
        }

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 0x0a) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00484860</c> — sous-zones 4 et 9. <b>Absente de <c>Blaze.c</c></b> ;
    /// 187 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_00484860.asm</c>.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    /// <b>Un corps, deux vies.</b> L'octet <c>+0x42</c> separe le <b>vol</b> (zero) de l'<b>eclat</b>
    /// (un), et les douze eclats naissent de la boule elle-meme : le corps se donne la vitesse de
    /// chacun avant de le faire naitre, puis pose <c>+0x42 = 1</c> — que les enfants heritent.
    /// </para>
    /// <para>
    /// <b>La physique est commune.</b> La position avance de sa vitesse, les deux composantes
    /// horizontales perdent un huitieme et un quatre-mille-quatre-vingt-seizieme par image, et la
    /// verticale prend la pesanteur <c>+0x9a</c> — <c>+0x54 += (+0x9a &gt;&gt; 8) - (+0x54 &gt;&gt; 12)
    /// - +0x9a</c>.
    /// </para>
    /// <para>
    /// <b>Le vol.</b> <see cref="FinalizeEntityPlacement2"/> avec la hauteur retenue et rendue —
    /// le placement ne doit pas la changer. Un contact <c>0xA000</c>, ou l'absence de contact
    /// <c>0x802</c> avec une hauteur au-dessus du sol <c>+0x60</c>, abandonne : si la boule porte
    /// <c>0x20000000</c> ou est tombee sous <c>-0x4e20000</c>, elle rend la main a son porteur
    /// (<c>0x2000000</c>) et s'eteint ; sinon elle continue. Autrement c'est <b>l'eclat</b> : douze
    /// enfants a <c>0x155</c> de cap d'ecart, chacun a 40 unites sur son cap et <c>0x1e000</c> vers
    /// le haut, sur la planche <c>0x00551bb8</c>, le sous-type <b>lu dans le repertoire de zone</b>
    /// (<c>+0xd4</c>, premier octet <c>+8</c>, plus <c>0xc0</c>). Puis le script <c>0x005526d0</c>
    /// et l'extinction.
    /// </para>
    /// <para>
    /// <b>L'eclat empoisonne une fois chacun.</b> L'echelle rejoint <c>0x5000</c> par huitiemes, et
    /// le gabarit <c>{-0x400, -0x1e, 0x64, 0x32, 0, 0x3c}</c> cherche une victime ; le masque des
    /// victimes est l'octet <c>+0x09</c> <b>du porteur commun</b> <c>+0x70</c>, pas celui de
    /// l'eclat — les douze se partagent donc une seule liste. La victime prend l'effet de piege au
    /// compte du lanceur <c>+0x74</c>, canal 7. La teinte perd <c>0x101010</c> par image et
    /// l'emplacement se vide quand elle vaut exactement zero, en rendant la main au porteur.
    /// </para>
    /// </remarks>
    private static void EntityHandler_00484860(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = -0x400;
        probe[1] = unchecked((short)0xffe2);
        probe[2] = 0x64;
        probe[3] = 0x32;
        probe[4] = 0;
        probe[5] = 0x3c;

        byte* holder = *(byte**)(e + 0x70);

        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        *(int*)(e + 0x50) = vx - (vx >> 3) - (vx >> 0xc);

        int gravity = *(short*)(e + 0x9a);
        *(int*)(e + 0x54) = (gravity >> 8) - (vy >> 0xc) + vy - gravity;

        *(int*)(e + 0x58) = vz - (vz >> 3) - (vz >> 0xc);

        if (e[0x42] != 0)
        {
            // ------------------------------------------------- l'eclat
            int scale = *(short*)(e + 0x28);
            scale += (0x5000 - scale) >> 3;
            *(short*)(e + 0x28) = (short)scale;
            *(short*)(e + 0x2a) = (short)scale;

            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);      // Ghidra: FUN_00474ca0

            if (victim != null)
            {
                byte mask = (byte)(1 << victim[8]);
                if ((holder[9] & mask) == 0)
                {
                    byte* caster = *(byte**)(e + 0x74);
                    caster[7] = 7;
                    ApplyTrapEffect_0053C420(*(uint**)(e + 0x74), (uint*)victim, 0);   // Ghidra: FUN_0053c420

                    holder[9] |= (byte)(1 << victim[8]);
                    *(uint*)victim |= 0x40;
                    *(short*)(victim + 0x96) = 8;
                }
            }

            *(uint*)(e + 0x38) += 0xffefeff0;
            if (*(uint*)(e + 0x38) != 0) return;

            *(uint*)holder |= 0x2000000;
            *(uint*)e = 0;
            return;
        }

        // ------------------------------------------------- le vol
        int height = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940
        *(int*)(e + 0x20) = height;

        bool abort = (*(uint*)(e + 0x90) & 0xa000) != 0;
        if (!abort && (*(uint*)(e + 0x90) & 0x802) == 0 && height > *(int*)(e + 0x60)) abort = true;

        if (abort)
        {
            if ((*(uint*)e & 0x20000000) != 0 || height < unchecked((int)0xfb1e0000))
            {
                *(uint*)holder |= 0x2000000;
                *(uint*)e = 0;
            }

            return;
        }

        // ------------------------------------------------- l'eclatement en douze
        *(byte**)(e + 0x70) = *(byte**)(e + 0x6c);
        *(short*)(e + 0x28) = 0x1000;
        *(short*)(e + 0x2a) = 0x1000;
        *(uint*)(e + 0x38) = 0x00f0f0f0;

        byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xd4)))[8] + 0xc0);

        for (int i = 0x0c; i != 0; i--)
        {
            short course = *(short*)(e + 0x32);

            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * 40;
            *(int*)(e + 0x54) = 0x1e000;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(course) * 40;
            *(short*)(e + 0x9a) = 0x4000;
            e[0x42] = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BB8(), subtype, 0, 3);   // Ghidra: FUN_0047ac40

            *(short*)(e + 0x32) = (short)(course + 0x155);
        }

        Aud.QueueDelayedSoundScript(
            NativeCopy(s_SoundScript_005526D0, ref s_SoundScript005526D0Storage),
            (int*)(e + 0x1c));                                       // Ghidra: FUN_0053dbe0

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004372b0</c> — sous-zones 4 et 9. <b>Absente de <c>Blaze.c</c></b> ;
    /// 228 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_004372B0.asm</c>. <b>Le coup qui tourne
    /// du lot 7</b> (<see cref="TickSpinningSinkingStrike"/>), avec <b>un role de plus</b>.
    ///
    /// <para>
    /// Son <c>+0x9a</c> choisit ce qu'elle fait d'une victime. A zero, c'est le coup amorti d'un
    /// quart, exactement comme <c>0x00431600</c> — mais au compte de <c>+0x78</c> et non de
    /// <c>+0x74</c>. Autrement, c'est <b>l'effet d'etat 13</b>
    /// (<see cref="AfflictWithGuard13"/>), applique <b>une seule fois par emplacement</b> : le
    /// masque des victimes est le mot <c>+0x16</c> du <b>porteur</b> <c>+0x6c</c>, pas le sien.
    /// </para>
    /// <para>
    /// Le role change aussi sa mise : a zero elle est claire et opaque (<c>0xb4 / 0xb4 / 0x82</c>,
    /// <c>e[0x0b] = 0</c>), autrement grise et translucide (<c>0xaa</c>, <c>e[0x0b] = 1</c>).
    /// Planche des etincelles, image tiree entre 1 et 3 puis bouclee.
    /// </para>
    /// </summary>
    private static void EntityHandler_004372B0(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;   // Ghidra: &DAT_005de558

        byte* caster = *(byte**)(e + 0x78);
        if (caster == null) caster = Res.s_EntityList_005de558;

        TickRoleStrike(e, Descriptors00550F68(), carrier, caster, marksCarrier: true);
    }

    /// <summary>
    /// Ghidra: <c>0x0043a160</c> — sous-zones 6 et 7. <b>Absente de <c>Blaze.c</c></b> ;
    /// 203 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0043A160.asm</c>. La sœur de
    /// <see cref="EntityHandler_004372B0"/>, a <b>deux</b> choses pres : la planche
    /// (<c>0x005510a8</c>), et <b>l'absence de masque</b> — elle applique l'effet 13 a chaque image
    /// ou la victime est a portee, sans se souvenir de l'avoir deja fait.
    ///
    /// <para>
    /// Elle ne remplace pas non plus un <c>+0x78</c> nul : le lanceur est pris tel quel. Le
    /// desassemblage le donne sans ambiguite (<c>mov ebp,[esi+78h]</c>, aucun test), et c'est ce que
    /// la transcription fait.
    /// </para>
    /// </summary>
    private static void EntityHandler_0043A160(byte* e)
        => TickRoleStrike(e, Descriptors005510A8(), null, *(byte**)(e + 0x78), marksCarrier: false);

    /// <summary>Le corps commun de <see cref="EntityHandler_004372B0"/> et
    /// <see cref="EntityHandler_0043A160"/>. Il reprend celui de
    /// <see cref="TickSpinningSinkingStrike"/> (lot 7) et lui ajoute le role <c>+0x9a</c>.</summary>
    private static void TickRoleStrike(byte* e, byte* descriptors, byte* carrier, byte* caster, bool marksCarrier)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = descriptors;
            int frame = CrtRand() % 3;
            e[0x0a] = 2;
            e[0x40] = (byte)(frame + 1);

            if (*(short*)(e + 0x9a) == 0)
            {
                e[0x0b] = 0;
                e[0x38] = 0xb4;
                e[0x39] = 0xb4;
                e[0x3a] = 0x82;
            }
            else
            {
                e[0x0b] = 1;
                e[0x38] = 0xaa;
                e[0x39] = 0xaa;
                e[0x3a] = 0xaa;
            }

            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(short*)(e + 0x28) = 0x5dc;
            *(short*)(e + 0x2a) = 0x5dc;
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 5;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short course = *(short*)(e + 0x32);
            short radius = *(short*)(e + 0x12);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);
            *(int*)(e + 0x20) += unchecked((int)0xffffa000);
            *(short*)(e + 0x28) += 0xc8;
            *(short*)(e + 0x2a) += 0xc8;
            BrightenEntityTint_00404DE0(e, -7, -7, -7);             // Ghidra: FUN_00404de0

            if (e[0x38] == 0) *(short*)(e + 0x10) = 2;

            FinalizeEntityPlacement2((uint*)e);                     // Ghidra: FUN_00533940
            if ((*(uint*)(e + 0x90) & 0x8802) != 0) *(short*)(e + 0x10) = 2;
            if (*(int*)(e + 0x60) >= *(int*)(e + 0x20)) *(short*)(e + 0x10) = 2;

            *(short*)(e + 0x14) -= 1;
            short left = *(short*)(e + 0x14);
            *(short*)(e + 0x12) += 0x14;

            if (left <= 0)
            {
                *(short*)(e + 0x14) = 5;
                byte next = (byte)(e[0x40] + 1);
                e[0x40] = next;
                if (next == 4) e[0x40] = 1;
            }

            byte* member = Inp.s_SlotFlags_005e93c0;                // 0x005e93c0

            for (int slot = 0; slot < 4; slot++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
                if (*(short*)(member + 0x12) != 0) continue;

                int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dy = (*(int*)(member + 0x20) - *(int*)(e + 0x20) + 0x3c000) >> 0xc;
                int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
                if (*(short*)(member + 0x96) != 0) continue;
                if (distance >= 0x50) continue;

                if (IsGuardingToward_004375A0(member, *(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24)) != 0)
                {
                    *(uint*)e &= 0x7fffffff;
                    continue;
                }

                if (*(short*)(e + 0x9a) == 0)
                {
                    ResolveEntityBlocks(caster, out _, out _, out byte* model);   // Ghidra: FUN_00538980
                    short attack = *(short*)(model + 0x30);
                    *(short*)(model + 0x30) = (short)(attack - (attack >> 2));
                    ResolveMeleeHit_00539B80((uint*)caster, (uint*)member);       // Ghidra: FUN_00539b80
                    *(short*)(model + 0x30) = attack;

                    *(short*)(member + 0x96) = 0x10;
                    *(uint*)member |= 0x40;
                    continue;
                }

                if (marksCarrier)
                {
                    short mask = (short)(1 << slot);
                    if ((*(short*)(carrier + 0x16) & mask) != 0) continue;
                    *(short*)(carrier + 0x16) |= mask;
                }

                AfflictWithGuard13(caster, member, 0);              // Ghidra: FUN_004365d0 / FUN_0043a400
            }
        }

        if (*(short*)(e + 0x10) == 2) *(uint*)e &= 0x7fffffff;
    }

    // =============================================================================================
    // LA GROTTE SCELLEE, LOT 9 (2026-09-20) — CE QUI BLESSE, BLOC 13-14-15
    // =============================================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00551bb0</c> — une tuile <c>0x18 x 0x18</c> a <c>x = 0x30</c>,
    /// le <b>premier</b> enregistrement de la plage <c>0x00551ba0</c> que la vieille tour a déjà
    /// relevée. <see cref="EntityHandler_00485C00"/> et <see cref="EntityHandler_00485F50"/> la
    /// passent a leurs eclats ; aucun des deux n'ecrit <c>+0x40</c>.</summary>
    private static byte* Descriptors00551BB0() => Descriptors00551BA0() + 0x10;

    private static readonly byte[] s_SoundScript_00552758 =
    [
        0x11, 0x01, 0x01, 0x00, 0xaa, 0x04, 0x00, 0x18,   // 0x00552758
        0x11, 0x00, 0x01, 0x00, 0xff, 0x04, 0x00, 0x18,   // 0x00552760 — delai nul : fin
    ];

    private static readonly byte[] s_SoundScript_00552768 =
    [
        0x25, 0x01, 0x01, 0x00, 0xaa, 0x04, 0x00, 0x18,   // 0x00552768
        0x28, 0x01, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x00552770
        0x28, 0x00, 0x01, 0x00, 0xaa, 0x08, 0x00, 0x18,   // 0x00552778 — fin
    ];

    private static readonly byte[] s_SoundScript_00552780 =
    [
        0x10, 0x01, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x00552780
        0x26, 0x00, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552788 — fin
    ];

    private static readonly byte[] s_SoundScript_00552790 =
    [
        0x10, 0x01, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x00552790
        0x10, 0x00, 0x01, 0x00, 0xaa, 0x08, 0x00, 0x18,   // 0x00552798 — fin
    ];

    private static readonly byte[] s_SoundScript_005527A0 =
    [
        0x2d, 0x03, 0x01, 0x00, 0xa9, 0x05, 0x00, 0x18,   // 0x005527a0
        0x2d, 0x01, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x005527a8
        0x2c, 0x00, 0x01, 0x00, 0xa9, 0x05, 0x00, 0x18,   // 0x005527b0 — fin
    ];

    /// <summary>Neuf enregistrements ; <b>sa queue est le script <c>0x005527f0</c></b>, que le
    /// binaire partage.</summary>
    private static readonly byte[] s_SoundScript_005527B8 =
    [
        0x1f, 0x01, 0x01, 0x00, 0x00, 0x10, 0x00, 0x18,   // 0x005527b8
        0x1e, 0x02, 0x01, 0x00, 0x52, 0x0b, 0x00, 0x18,   // 0x005527c0
        0x1c, 0x01, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x005527c8
        0x25, 0x02, 0x01, 0x00, 0x00, 0x10, 0x00, 0x18,   // 0x005527d0
        0x25, 0x02, 0x01, 0x00, 0x00, 0x10, 0x00, 0x18,   // 0x005527d8
        0x25, 0x02, 0x01, 0x00, 0x00, 0x10, 0x00, 0x18,   // 0x005527e0
        0x25, 0x02, 0x01, 0x00, 0x00, 0x10, 0x00, 0x18,   // 0x005527e8
        0x31, 0x01, 0x01, 0x00, 0x00, 0x01, 0x00, 0x18,   // 0x005527f0
        0x31, 0x00, 0x01, 0x00, 0x2a, 0x01, 0x00, 0x18,   // 0x005527f8 — fin
    ];

    private static readonly byte[] s_SoundScript_005527F0 =
    [
        0x31, 0x01, 0x01, 0x00, 0x00, 0x01, 0x00, 0x18,   // 0x005527f0
        0x31, 0x00, 0x01, 0x00, 0x2a, 0x01, 0x00, 0x18,   // 0x005527f8 — fin
    ];

    private static readonly byte[] s_SoundScript_00552800 =
    [
        0x0d, 0x01, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x00552800
        0x0d, 0x01, 0x01, 0x00, 0xa7, 0x07, 0x00, 0x18,   // 0x00552808
        0x30, 0x01, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x00552810
        0x1b, 0x00, 0x01, 0x00, 0x00, 0x02, 0x00, 0x18,   // 0x00552818 — fin
    ];

    private static readonly byte[] s_SoundScript_00552820 =
    [
        0x18, 0x01, 0x01, 0x00, 0xa9, 0x05, 0x00, 0x18,   // 0x00552820
        0x0b, 0x03, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552828
        0x0b, 0x03, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552830
        0x0b, 0x03, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552838
        0x0b, 0x03, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552840
        0x0b, 0x00, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552848 — fin
    ];

    private static byte* s_SoundScript00552758Storage;
    private static byte* s_SoundScript00552768Storage;
    private static byte* s_SoundScript00552780Storage;
    private static byte* s_SoundScript00552790Storage;
    private static byte* s_SoundScript005527A0Storage;
    private static byte* s_SoundScript005527B8Storage;
    private static byte* s_SoundScript005527F0Storage;
    private static byte* s_SoundScript00552800Storage;
    private static byte* s_SoundScript00552820Storage;

    /// <summary>
    /// Ghidra: <c>0x00486720</c> — sous-zones 13, 14 et 15. <b>Absente de <c>Blaze.c</c></b> ;
    /// 129 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_00486720.asm</c>.
    ///
    /// <para>
    /// <b>Le coup qui part en gerbe.</b> A l'etat 1 et tant que <c>+0x12</c> n'est pas nul, elle
    /// sauve sa position, s'ecarte de 100 unites sur son cap moins un quart de tour, se met a
    /// l'echelle <c>0x200</c>, lance l'incantation que le repertoire de zone designe
    /// (<c>+0xd0</c>, octet <c>+7</c>, plus <c>0xc0</c>), fait varier son cap de <c>±0x200</c>, et
    /// <b>remet tout en place</b>.
    /// </para>
    /// <para>
    /// Sous la dixieme image, sa teinte monte de <c>0x40408</c> par image jusqu'a <c>0x404080</c>,
    /// son echelle rejoint <c>0x2000</c> par quarts, et elle balaie le camp adverse
    /// (<see cref="SweepOpposingSideForMeleeHit_00474C70"/>, gabarit
    /// <c>{-0x400, -0x1e, 0x1e, 0xc8, 0, 0xa0}</c>). Au-dela, la teinte redescend de
    /// <c>0x40408</c> et l'emplacement se vide quand elle atteint exactement zero.
    /// </para>
    /// <para>
    /// <b>Elle ne verifie pas que le balayage a trouve quelqu'un.</b> Le binaire ecrase le resultat
    /// par <c>0x005de0b8</c> et ne teste que celui-la (<c>mov edi,eax ; mov eax,[5DE0B8h] ; test
    /// eax,eax</c>) : sa sœur <see cref="EntityHandler_00485A40"/>, elle, teste la victime. Releve
    /// tel quel — un balayage vide mene donc a un acces fautif, comme dans le binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00486720(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = -0x400;
        probe[1] = unchecked((short)0xffe2);
        probe[2] = 0x1e;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0xa0;

        if (*(short*)(e + 0x10) == 1 && *(short*)(e + 0x12) != 0)
        {
            short savedCourse = *(short*)(e + 0x32);
            short savedScale = *(short*)(e + 0x2a);
            int savedX = *(int*)(e + 0x1c);
            int savedY = *(int*)(e + 0x20);
            int savedZ = *(int*)(e + 0x24);

            *(short*)(e + 0x12) -= 1;

            short spread = (short)(savedCourse - 0x400);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(spread) * 100;
            *(short*)(e + 0x2a) = 0x200;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(spread) * 100;

            *(short*)(e + 0x32) += (short)((CrtRand() & 0x3ff) - 0x200);

            byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xd0)))[7] + 0xc0);
            SpawnCastEntity_0047AAE0(e, subtype, 1, 0x0d);          // Ghidra: FUN_0047aae0

            *(int*)(e + 0x1c) = savedX;
            *(int*)(e + 0x20) = savedY;
            *(int*)(e + 0x24) = savedZ;
            *(short*)(e + 0x32) = savedCourse;
            *(short*)(e + 0x2a) = savedScale;
        }

        if (*(short*)(e + 0x10) >= 0x0a)
        {
            if (*(uint*)(e + 0x38) != 0)
            {
                *(uint*)(e + 0x38) += 0xfffbfbf8;
                if (*(uint*)(e + 0x38) == 0) *(uint*)e = 0;
            }

            *(short*)(e + 0x10) += 1;
            return;
        }

        if (*(uint*)(e + 0x38) >= 0x404080)
        {
            *(short*)(e + 0x10) += 1;
            return;
        }

        *(uint*)(e + 0x38) += 0x40408;

        int scale = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)(((0x2000 - scale) >> 2) + scale);

        byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);   // Ghidra: FUN_00474c70

        if (*Mem.Camera(0x005de0b8) == 0)
        {
            *(short*)(e + 0x10) += 1;
            return;
        }

        ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);     // Ghidra: FUN_00539b80
        *(short*)(victim + 0x96) = 8;
        *(uint*)victim |= 0x40;

        Aud.PlayPositionalSound(3, 0x400, 0x1800, (int*)(e + 0x1c));      // Ghidra: FUN_0053db20
        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00485a40</c> — sous-zones 13, 14 et 15. <b>Absente de <c>Blaze.c</c></b> ;
    /// 139 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_00485A40.asm</c>. La sœur de
    /// <see cref="EntityHandler_00486720"/>, decalee dans le temps et doublee.
    ///
    /// <para>
    /// Elle lance <b>deux</b> incantations a l'etat 4 (l'octet <c>+3</c> du repertoire, plus
    /// <c>0xc0</c>), la premiere avec un ecart de cap positif et la seconde negatif — le binaire
    /// deroule la boucle a l'envers, et le test <c>edi == 2</c> vaut pour la <b>premiere</b>
    /// iteration. Son ecartement est de 200 unites et non 100.
    /// </para>
    /// <para>
    /// Sa teinte monte par <c>0x80808</c> jusqu'a <c>0x808080</c>, en <b>rendant la main a chaque
    /// image</b> — vingt images de montee au lieu de dix. Ensuite l'echelle rejoint <c>0x3000</c>
    /// par quarts et le gabarit <c>{-0x400, -0x1e, 0x1e, 0xc8, 0, 0x104}</c> cherche une victime
    /// (<see cref="ProbeMeleeTarget_00474CA0"/>, qui est ici <b>testee</b>). Au-dela de la
    /// vingtieme image, la teinte redescend de <c>0x80808</c> jusqu'a zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_00485A40(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = -0x400;
        probe[1] = unchecked((short)0xffe2);
        probe[2] = 0x1e;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x104;

        if (*(short*)(e + 0x10) == 4 && *(short*)(e + 0x12) != 0)
        {
            short savedCourse = *(short*)(e + 0x32);
            int savedX = *(int*)(e + 0x1c);
            int savedY = *(int*)(e + 0x20);
            int savedZ = *(int*)(e + 0x24);

            *(short*)(e + 0x12) -= 1;

            short spread = (short)(savedCourse - 0x400);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(spread) * 200;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(spread) * 200;

            byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xd0)))[3] + 0xc0);

            for (int shot = 2; shot != 0; shot--)
            {
                int offset = CrtRand() & 0x1ff;
                if (shot != 2) offset = -offset;

                *(short*)(e + 0x32) = (short)(*(short*)(e + 0x32) + offset);
                SpawnCastEntity_0047AAE0(e, subtype, 1, 1);          // Ghidra: FUN_0047aae0
            }

            *(int*)(e + 0x1c) = savedX;
            *(int*)(e + 0x20) = savedY;
            *(int*)(e + 0x24) = savedZ;
            *(short*)(e + 0x32) = savedCourse;
        }

        if (*(short*)(e + 0x10) >= 0x14)
        {
            if (*(uint*)(e + 0x38) != 0)
            {
                *(uint*)(e + 0x38) += 0xfff7f7f8;
                if (*(uint*)(e + 0x38) == 0) *(uint*)e = 0;
            }

            *(short*)(e + 0x10) += 1;
            return;
        }

        if (*(uint*)(e + 0x38) < 0x808080)
        {
            *(uint*)(e + 0x38) += 0x80808;
            *(short*)(e + 0x10) += 1;
            return;
        }

        int scale = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)(((0x3000 - scale) >> 2) + scale);

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);          // Ghidra: FUN_00474ca0

        if (victim != null)
        {
            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);
            *(short*)(victim + 0x96) = 8;
            *(uint*)victim |= 0x40;
            Aud.PlayPositionalSound(3, 0x400, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00486b10</c> — sous-zones 13, 14 et 15. <b>Absente de <c>Blaze.c</c></b> ;
    /// 174 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_00486B10.asm</c>.
    ///
    /// <para>
    /// <b>Le tourbillon qui se charge puis fauche.</b> Le bit <c>0x2000000</c> de son mot d'etat
    /// separe les deux vies : <b>absent</b>, c'est l'amorce — elle prend son porteur <c>+0x6c</c>
    /// pour lanceur, se met au modele <c>0x3b</c>, lance une incantation
    /// (<c>+0xd0</c>, octet <c>+0x12</c>), tourne de <c>0x80</c>, et range le script
    /// <c>0x00552800</c>. <b>Present</b>, c'est le tourbillon lui-meme.
    /// </para>
    /// <para>
    /// Sous l'etat <c>0x11</c>, il relance la meme incantation a chaque image et tourne de
    /// <c>0x100</c>. A l'etat <c>0x11</c> pile, le script <c>0x00552820</c>. Ensuite, a chaque
    /// image, <b>quatre eclaboussures</b> (<c>0xbe</c> sur la planche <c>0x00551a08</c>) semees sur
    /// un cercle de 50 a 449 unites autour de lui, avec une vitesse de <c>sin/cos &lt;&lt; 6</c> et
    /// <c>0x40000</c> vers le haut ; puis son cap gagne <c>0x100</c> et le gabarit
    /// <c>{0, -0x100, 0x100, 0x96, 0, 0x200}</c> cherche une victime. A l'etat <c>0x21</c>,
    /// l'emplacement se vide.
    /// </para>
    /// </summary>
    private static void EntityHandler_00486B10(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = unchecked((short)0xff00);
        probe[2] = 0x100;
        probe[3] = 0x96;
        probe[4] = 0;
        probe[5] = 0x200;

        byte* owner = *(byte**)(e + 0x6c);
        byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xd0)))[0x12] + 0xc0);

        if ((*(uint*)e & 0x2000000) == 0)
        {
            // ------------------------------------------------- l'amorce
            if (*(short*)(e + 0x10) != 0) return;

            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x74) = owner;
            *(byte**)(e + 0x70) = e;
            *(short*)(e + 0x36) = 4;
            e[4] = 0x3b;
            *(int*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0x200;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;
            *(short*)(e + 0x16) = 0;

            SpawnCastEntity_0047AAE0(e, subtype, 1, 1);             // Ghidra: FUN_0047aae0
            *(short*)(e + 0x32) += 0x80;

            *(byte**)(e + 0x70) = null;
            *(int*)(e + 0x28) = 0x10001800;
            *(short*)(e + 0x2c) = 0x11f8;

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_00552800, ref s_SoundScript00552800Storage),
                (int*)(e + 0x1c));                                  // Ghidra: FUN_0053dbe0
            return;
        }

        // ------------------------------------------------- le tourbillon
        if (*(short*)(e + 0x10) < 0x11)
        {
            *(short*)(e + 0x10) += 1;
            *(short*)(e + 0x16) = 1;
            SpawnCastEntity_0047AAE0(e, subtype, 1, 1);
            *(short*)(e + 0x32) += 0x100;
            return;
        }

        if (*(short*)(e + 0x10) == 0x11)
        {
            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_00552820, ref s_SoundScript00552820Storage),
                (int*)(e + 0x1c));
        }

        *(short*)(e + 0x10) += 1;
        *(short*)(e + 0x1a) = 7;
        short course = *(short*)(e + 0x32);
        *(uint*)(e + 0x38) = 0x01606060;
        *(short*)(e + 0x12) = 3;
        *(int*)(e + 0x28) = 0x20002000;
        *(short*)(e + 0x14) = 0x4000;
        *(short*)(e + 0x16) = 2;

        for (int i = 4; i != 0; i--)
        {
            int radius = CrtRand() % 0x190 + 0x32;
            short spread = (short)((CrtRand() & 0xff) - 0x80 + course);

            int* splash = stackalloc int[3];
            splash[0] = (int)Winsys3d.GetSinTableEntry(spread) * radius + *(int*)(e + 0x1c);
            splash[1] = *(int*)(e + 0x20);
            splash[2] = Winsys3d.GetCosTableEntry(spread) * radius + *(int*)(e + 0x24);

            *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(spread) << 6;
            *(int*)(e + 0x54) = 0x40000;
            *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(spread) << 6;

            SpawnStatusEffectEntity_0047AC40(e, splash, SplashDescriptors(), 0xbe, 1, 1);   // Ghidra: FUN_0047ac40
        }

        *(short*)(e + 0x32) = (short)(course + 0x100);

        byte* victim = ProbeMeleeTarget_00474CA0(e, probe);
        if (victim != null)
        {
            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);
            *(short*)(victim + 0x96) = 8;
            *(uint*)victim |= 0x40;
        }

        if (*(short*)(e + 0x10) >= 0x21) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00485c00 @ 0x00485c00 — sous-zones 13, 14 et 15. Decompilee dans <c>Blaze.c</c>,
    /// mais <b>trois registres y sont perdus</b> (<c>unaff_BX</c>, <c>unaff_retaddr</c>) ; le corps
    /// est repris sur <c>Legacy/Raw/Blaze/SealedCave/FUN_00485C00.asm</c>, 197 instructions.
    ///
    /// <para>
    /// <b>Le geste en trois temps, colle a son porteur.</b> Elle suit la position de son
    /// <c>+0x6c</c> a chaque image. A la premiere, elle se donne une vitesse de 200 unites sur son
    /// cap plus <c>0x12c</c> et <c>0x1ae000</c> vers le haut, un gabarit <c>0x4000</c>, la teinte
    /// <c>0x01f0f0f0</c>, et fait naitre l'eclaboussure <c>0xbe</c> sur <c>0x00551bb0</c> ; le
    /// script <c>0x00552758</c> part.
    /// </para>
    /// <para>
    /// <b>A la quatorzieme image, la charge s'ouvre</b> : l'echelle passe a <c>0x3ffc</c>, l'entite
    /// glisse de sa vitesse, prend le mode de dessin 4 et l'echelle <c>0x6000 / 0x7000</c>, et lance
    /// <b>deux</b> incantations <c>0x83</c> a un demi-tour d'ecart, modele tire entre 8 et 11.
    /// <c>0x005e93a0</c> recoit <c>0x4000ff</c> et <c>0x005e93a4</c> <c>0x40</c> — le fondu rouge.
    /// Script <c>0x00552768</c>.
    /// </para>
    /// <para>
    /// <b>Au-dela de la trentieme image, une fois sur quatre</b>, elle se jette a un cap tire, a une
    /// distance de 0 a <c>0x1ff</c>, se replace sur le sol <c>+0x60</c>, lance l'incantation que le
    /// repertoire designe (<c>+0xd0</c>, octet <c>+4</c>), puis se remet a plat — vitesses nulles,
    /// gabarit <c>0x4000</c> sur les trois axes, teinte <c>0x808080</c>, modele <c>0x26</c> — et
    /// lance <c>0x6c</c>. Script <c>0x00552780</c>. A la soixantieme image, l'emplacement se vide.
    /// </para>
    /// <para>
    /// <b>Les trois registres que Ghidra perd</b> sont tous le meme tirage : le cap est calcule une
    /// fois (<c>[esp+14h]</c>) et relu pour le sinus, le cosinus et <c>+0x32</c>. Et
    /// <c>param_1[0x18]</c> est <c>+0x60</c>, la hauteur de sol.
    /// </para>
    /// </summary>
    private static void EntityHandler_00485C00(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            short course = (short)(*(short*)(e + 0x32) + 0x12c);
            *(byte**)(e + 0x74) = carrier;
            *(byte**)(e + 0x70) = carrier;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = 0;
            *(short*)(e + 0x12) = 0x1f;

            *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(course) * 100;
            *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(course) * 100;
            *(int*)(e + 0x48) = 0x1ae000;
            *(int*)(e + 0x28) = 0;
            *(short*)(e + 0x14) = 0x4000;
            *(short*)(e + 0x16) = 2;
            *(uint*)(e + 0x38) = 0x01f0f0f0;
            *(short*)(e + 0x1a) = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BB0(), 0xbe, 1, 0x0d);   // Ghidra: FUN_0047ac40

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_00552758, ref s_SoundScript00552758Storage),
                (int*)(e + 0x1c));                                            // Ghidra: FUN_0053dbe0
        }

        if (*(short*)(e + 0x10) == 0x0e)
        {
            *(int*)(e + 0x28) = 0x3ffc;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 3;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(short*)(e + 0x1a) = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BB0(), 0xbe, 1, 0x0d);

            *(int*)(e + 0x1c) += *(int*)(e + 0x44);
            *(short*)(e + 0x34) = 0x400;
            *(int*)(e + 0x20) += *(int*)(e + 0x48);
            *(int*)(e + 0x24) += *(int*)(e + 0x4c);
            *(short*)(e + 0x36) = 4;
            *(short*)(e + 0x2e) = unchecked((short)0xfff0);
            *(int*)(e + 0x28) = 0x70006000;
            *(short*)(e + 0x2c) = 0x7000;

            e[4] = (byte)((CrtRand() & 3) + 8);
            SpawnCastEntity_0047AAE0(e, 0x83, 1, 0x0d);             // Ghidra: FUN_0047aae0
            *(short*)(e + 0x32) += 0x400;
            e[4] = (byte)((CrtRand() & 3) + 8);
            SpawnCastEntity_0047AAE0(e, 0x83, 1, 0x0d);

            *(uint*)Mem.Arena(0x005e93a0) = 0x004000ff;             // Ghidra: _DAT_005e93a0
            *(short*)Mem.Arena(0x005e93a4) = 0x40;

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_00552768, ref s_SoundScript00552768Storage),
                (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x10) > 0x1e && (*(ushort*)(e + 0x10) & 3) == 0)
        {
            short course = (short)CrtRand();                        // Ghidra: `unaff_BX` / `unaff_retaddr`
            int reach = CrtRand() & 0x1ff;

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * reach;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * reach;
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(short*)(e + 0x32) = course;
            *(short*)(e + 0x34) = 0x400;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(short*)(e + 0x36) = 4;
            *(short*)(e + 0x2e) = unchecked((short)0xfff0);
            *(int*)(e + 0x28) = 0x70006000;
            *(short*)(e + 0x2c) = 0x7000;

            e[4] = (byte)((CrtRand() & 3) + 8);
            byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xd0)))[4] + 0xc0);
            SpawnCastEntity_0047AAE0(e, subtype, 1, 0x0d);

            *(byte**)(e + 0x70) = null;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;
            *(int*)(e + 0x50) = 0x4000;
            *(int*)(e + 0x54) = 0x4000;
            *(int*)(e + 0x58) = 0x4000;
            *(uint*)(e + 0x38) = 0x00808080;
            *(int*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x34) = 0;
            e[4] = 0x26;

            SpawnCastEntity_0047AAE0(e, 0x6c, 1, 0x0d);

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_00552780, ref s_SoundScript00552780Storage),
                (int*)(e + 0x1c));
        }

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 0x3c) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00485f50 @ 0x00485f50 — sous-zones 13, 14 et 15. Decompilee dans <c>Blaze.c</c> ;
    /// 197 instructions. La sœur longue de <see cref="EntityHandler_00485C00"/> : quatre temps au
    /// lieu de trois, et un <b>visee</b> a la fin.
    ///
    /// <para>
    /// Elle suit son porteur, et sous la quatorzieme image elle <b>frissonne</b> : cap et rotation
    /// tires a chaque image, echelle <c>0x100</c>, teinte noire, modele 0, et l'incantation que le
    /// repertoire designe (<c>+0xd0</c>, octet <c>+5</c>). A la vingtieme, l'eclaboussure
    /// <c>0xbe</c> et le script <c>0x005527a0</c> ; a la trente-quatrieme, une seconde, plus le
    /// fondu <c>0x005e93a0 = 0x400040</c> / <c>0x005e93a4 = 0xff</c> et le script
    /// <c>0x005527b8</c>.
    /// </para>
    /// <para>
    /// <b>Au-dela, elle vise.</b> Elle prend le <c>+0x6c</c> de son propre porteur — la cible de sa
    /// cible —, avance de sa vitesse, et calcule deux angles vers elle : le cap par
    /// <c>Ordinal_457</c> sur l'ecart horizontal, l'elevation par le meme sur la hauteur, corrigee
    /// de la <b>hauteur de bulle</b> de la cible (<see cref="StatusBubbleHeight_00538BC0"/>,
    /// arrondie au pair et decalee de 11). Les deux recoivent un bruit de <c>±0x200</c>, et
    /// <see cref="SetVectorFromAngles_00532AD0"/> en tire la vitesse a 100 unites. Le modele passe
    /// a <c>0x3f</c>, l'incantation <c>+6</c> du repertoire part, les coordonnees sont rendues, et
    /// le script <c>0x005527f0</c> avec. A la quarante-huitieme image, l'emplacement se vide.
    /// </para>
    /// </summary>
    private static void EntityHandler_00485F50(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            short course = (short)(*(short*)(e + 0x32) + 0x12c);
            *(byte**)(e + 0x74) = carrier;
            *(byte**)(e + 0x70) = carrier;

            *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(course) * 100;
            *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(course) * 100;
            *(int*)(e + 0x48) = 0x1ae000;

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_00552790, ref s_SoundScript00552790Storage),
                (int*)(e + 0x1c));                                  // Ghidra: FUN_0053dbe0
        }

        if (*(short*)(e + 0x10) < 0x0e)
        {
            *(short*)(e + 0x32) = (short)CrtRand();
            *(short*)(e + 0x34) = (short)CrtRand();
            *(short*)(e + 0x36) = 4;
            *(int*)(e + 0x28) = 0x100;
            *(short*)(e + 0x2c) = 0x100;
            *(uint*)(e + 0x38) = 0;
            *(short*)(e + 0x12) = 0x320;
            *(short*)(e + 0x14) = 0x40;
            e[4] = 0;

            byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xd0)))[5] + 0xc0);
            SpawnCastEntity_0047AAE0(e, subtype, 1, 7);             // Ghidra: FUN_0047aae0
        }

        if (*(short*)(e + 0x10) == 0x14)
        {
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = 0;
            *(short*)(e + 0x12) = 0x1f;
            *(int*)(e + 0x28) = 0;
            *(short*)(e + 0x14) = 0x4000;
            *(short*)(e + 0x16) = 2;
            *(uint*)(e + 0x38) = 0x01f0f0f0;
            *(short*)(e + 0x1a) = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BB0(), 0xbe, 1, 7);      // Ghidra: FUN_0047ac40

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_005527A0, ref s_SoundScript005527A0Storage),
                (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x10) == 0x22)
        {
            *(int*)(e + 0x28) = 0x3ffc;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 3;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(short*)(e + 0x1a) = 1;

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BB0(), 0xbe, 1, 7);

            *(uint*)Mem.Arena(0x005e93a0) = 0x00400040;             // Ghidra: DAT_005e93a0
            *(short*)Mem.Arena(0x005e93a4) = 0xff;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(int*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0x640;

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_005527B8, ref s_SoundScript005527B8Storage),
                (int*)(e + 0x1c));
        }

        if (*(short*)(e + 0x10) > 0x22)
        {
            byte* holder = *(byte**)(e + 0x6c);
            byte* target = *(byte**)(holder + 0x6c);
            *(byte**)(e + 0x74) = holder;

            int savedX = *(int*)(e + 0x1c);
            int savedY = *(int*)(e + 0x20);
            int savedZ = *(int*)(e + 0x24);

            *(int*)(e + 0x1c) = savedX + *(int*)(e + 0x44);
            *(int*)(e + 0x20) = savedY + *(int*)(e + 0x48);
            *(int*)(e + 0x24) = savedZ + *(int*)(e + 0x4c);
            *(byte**)(e + 0x78) = target;

            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            int bubble = StatusBubbleHeight_00538BC0((uint*)target);   // Ghidra: FUN_00538bc0
            int dy = ((bubble & ~1) << 0x0b) + *(int*)(target + 0x20) - *(int*)(e + 0x20);
            dy >>= 0xc;

            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            short course = (short)(Winsys3d.Ordinal_457(dx, dz) + (CrtRand() & 0x3ff) + 0xa00);
            *(short*)(e + 0x32) = course;

            int pitchBase = Winsys3d.Ordinal_457(dy, flat);
            short pitch = (short)((CrtRand() & 0x3ff) - 0x200 - pitchBase);
            *(short*)(e + 0x34) = pitch;

            SetVectorFromAngles_00532AD0((int*)(e + 0x50), course - 0x400, pitch, 100);   // Ghidra: FUN_00532ad0

            e[4] = 0x3f;
            byte subtype = (byte)((*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xd0)))[6] + 0xc0);
            SpawnCastEntity_0047AAE0(e, subtype, 1, 7);

            *(int*)(e + 0x1c) = savedX;
            *(int*)(e + 0x20) = savedY;
            *(int*)(e + 0x24) = savedZ;

            Aud.QueueDelayedSoundScript(
                NativeCopy(s_SoundScript_005527F0, ref s_SoundScript005527F0Storage),
                (int*)(e + 0x1c));
        }

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) >= 0x30) *(uint*)e = 0;
    }

    // =============================================================================================
    // LA GROTTE SCELLEE, LOT 10 (2026-09-20) — LA BULLE D'EFFET D'ETAT, DEUX FAMILLES DE COPIES
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00436070</c> (sous-zones 4 et 9) et <c>0x0043ec60</c> (13, 14 et 15).
    /// <b>Absentes de <c>Blaze.c</c></b> ; 311 instructions chacune,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00436070.asm</c>. <b>Les deux corps sont identiques
    /// instruction pour instruction</b>, adresses mises à part — y compris leurs deux tables de
    /// sauts.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    /// <b>Un corps, deux vies, séparées par <c>+0x5c</c>.</b> A zéro c'est <b>la bulle qui
    /// s'étend</b>, à un c'est <b>l'éclat qu'elle sème</b>.
    /// </para>
    /// <para>
    /// <b>La bulle.</b> Modèle 7, teinte <c>0xfa</c>, et une hauteur de départ
    /// <c>(0x19 - +0x12) * 400</c> — le <c>+0x12</c> que la sous-zone a posé décide donc de sa
    /// taille verticale. Son <c>+0x36</c> reçoit le bit 2 et perd le bit 0. Chaque image, ses deux
    /// échelles horizontales rejoignent <c>+0x48</c> par huitièmes, sa teinte perd <c>7 / 7 / 5</c>,
    /// et elle s'éteint quand son bleu atteint zéro.
    /// </para>
    /// <para>
    /// <b>Elle n'applique qu'une fois par membre</b>, et seulement une fois assez grande : tant que
    /// <c>+0x28</c> reste sous <c>+0x48 - 0xbb8</c>, rien ne part. Ensuite, tout membre vivant à
    /// moins de <c>+0x28 / 50</c> unités <b>en plan</b> et dont le bit n'est pas encore dans le
    /// masque <c>+0x14</c> reçoit l'effet d'état que <c>+0x44</c> désigne.
    /// </para>
    /// <para>
    /// <b>La table de sauts est celle qu'on connaît déjà.</b> Ses douze entrées, relevées à
    /// <c>0x00436434</c> et <c>0x0043f024</c>, sont <b>exactement</b> celles de
    /// <c>0x0040a30c</c> — mêmes effets, même ordre, et la même entrée 9 qui redouble l'entrée 4.
    /// Les vingt-et-une « appelées inconnues » que le recensement signalait sur ces deux corps sont
    /// leurs deux jeux privés d'enveloppes gardées, identiques aux douze de
    /// <see cref="AfflictMember_00409F40"/>. C'est donc cette fonction qui est appelée, avec
    /// <c>keep</c> — que le binaire remplit ici avec <b>le numéro d'emplacement</b> du membre, et
    /// non avec zéro.
    /// </para>
    /// <para>
    /// <b>Le lanceur est le grand-porteur</b> : <c>+0x6c</c> de son <c>+0x6c</c>, chacun replié sur
    /// l'entité par défaut <c>0x005de558</c> s'il est nul.
    /// </para>
    /// <para>
    /// <b>L'éclat.</b> Modèle <c>0x30</c>, trois échelles tirées entre <c>0xc8</c> et <c>0x18f</c>,
    /// un cap tiré, une teinte partie du noir et un départ relevé de 0 à <c>0xc7</c> unités. Six
    /// images pendant lesquelles il tourne de <c>-0x96</c>, grandit de 100 par axe et sa teinte
    /// <b>rejoint le blanc par moitiés</b> ; puis vingt images où elle <b>retombe au noir par
    /// huitièmes</b>, et l'emplacement se vide.
    /// </para>
    /// </remarks>
    private static void TickStatusBubble_00436070(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;   // Ghidra: &DAT_005de558

        byte* caster = *(byte**)(carrier + 0x6c);
        if (caster == null) caster = Res.s_EntityList_005de558;

        if (*(int*)(e + 0x5c) == 0)
        {
            // ------------------------------------------------- la bulle
            if (*(short*)(e + 0x10) == 0)
            {
                short tall = (short)(0x19 - *(short*)(e + 0x12));
                short mode = (short)(*(short*)(e + 0x36) & ~1);

                e[4] = 7;
                e[0x38] = 0xfa;
                e[0x39] = 0xfa;
                e[0x3a] = 0xfa;
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x2a) = (short)(tall * 400);
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x2e) = unchecked((short)0xffc4);
                *(short*)(e + 0x36) = (short)(mode | 2);
            }

            if (*(short*)(e + 0x10) != 1) return;

            BrightenEntityTint_00404DE0(e, -7, -7, -5);             // Ghidra: FUN_00404de0

            int target = *(int*)(e + 0x48);
            *(short*)(e + 0x28) = (short)(((target - *(short*)(e + 0x28)) >> 3) + *(short*)(e + 0x28));
            *(short*)(e + 0x2c) = (short)(((target - *(short*)(e + 0x2c)) >> 3) + *(short*)(e + 0x2c));

            if (e[0x3a] == 0) *(uint*)e &= 0x7fffffff;

            if (*(short*)(e + 0x16) != 0) return;
            if (*(short*)(e + 0x28) >= target - 0xbb8) return;

            byte* member = Inp.s_SlotFlags_005e93c0;                // 0x005e93c0

            for (int slot = 0; slot < 4; slot++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
                if (distance >= *(short*)(e + 0x28) / 50) continue;

                int bit = 1 << slot;
                if ((*(short*)(e + 0x14) & bit) != 0) continue;
                *(short*)(e + 0x14) |= (short)bit;

                AfflictMember_00409F40(*(int*)(e + 0x44), (uint*)caster, (uint*)member, slot);
            }

            return;
        }

        if (*(int*)(e + 0x5c) != 1) return;

        // ------------------------------------------------- l'eclat
        if (*(short*)(e + 0x10) == 0)
        {
            e[4] = 0x30;
            *(short*)(e + 0x28) = (short)(CrtRand() % 0xc8 + 0xc8);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0xc8 + 0xc8);
            *(short*)(e + 0x2c) = (short)(CrtRand() % 0xc8 + 0xc8);
            *(short*)(e + 0x32) = (short)SignedMask(CrtRand(), 0xfff);

            int lift = CrtRand() % 0x1f4;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x30) = (short)lift;

            *(short*)(e + 0x2e) = unchecked((short)0xffc4);
            *(int*)(e + 0x20) += (CrtRand() % 0xc8) << 0xc;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) += unchecked((short)0xff6a);        // -0x96

            e[0x38] = (byte)(e[0x38] + (byte)((0xfa - e[0x38]) >> 1));
            e[0x39] = (byte)(e[0x39] + (byte)((0xfa - e[0x39]) >> 1));
            e[0x3a] = (byte)(e[0x3a] + (byte)((0xfa - e[0x3a]) >> 1));

            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) > 5)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }

            *(short*)(e + 0x28) += 100;
            *(short*)(e + 0x2a) += 100;
            *(short*)(e + 0x2c) += 100;
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x32) += unchecked((short)0xff6a);

        e[0x38] = (byte)(e[0x38] + (byte)(-e[0x38] >> 3));
        e[0x39] = (byte)(e[0x39] + (byte)(-e[0x39] >> 3));
        e[0x3a] = (byte)(e[0x3a] + (byte)(-e[0x3a] >> 3));

        *(short*)(e + 0x12) += 1;
        if (*(short*)(e + 0x12) > 0x14) *(uint*)e &= 0x7fffffff;

        *(short*)(e + 0x28) += 100;
        *(short*)(e + 0x2a) += 100;
        *(short*)(e + 0x2c) += 100;
    }

    private static void EntityHandler_00436070(byte* e) => TickStatusBubble_00436070(e);
    private static void EntityHandler_0043EC60(byte* e) => TickStatusBubble_00436070(e);

    // =====================================================================
    // LOT 13 (2026-09-20) — LE PROJECTILE A TETE CHERCHEUSE DE LA GROTTE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00486320</c> — sous-zones 13, 14 et 15, table <c>0x0056b1c8</c>. <b>Absente de
    /// <c>Blaze.c</c></b> ; 274 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00486320.asm</c>.
    ///
    /// <para>
    /// <b>Le projectile qui poursuit sa cible.</b> Il porte sa proie dans <c>+0x78</c> et, tant
    /// qu'il ne l'a pas perdue, il <b>recalcule son cap et son tangage a chaque image</b> vers elle,
    /// pose une poussee de <c>12</c> unites dans cette direction, l'ajoute a une vitesse qu'il
    /// <b>amortit des sept huitiemes</b>, et avance. C'est le seul de la grotte qui vise.
    /// </para>
    /// <list type="table">
    ///   <item><term>avec une cible</term><description>l'ecart se mesure en trois termes : X et Z
    ///     directement, la hauteur <b>corrigee de la bulle d'etat</b> de la cible
    ///     (<see cref="StatusBubbleHeight_00538BC0"/>, arrondie au pair et decalee de 11 bits) —
    ///     le projectile vise donc le buste et non les pieds. Il s'inscrit en retour dans le
    ///     <c>+0x74</c> de sa cible.</description></item>
    ///   <item><term>sans cible, ou une fois verrouille</term><description>il garde son cap
    ///     (<c>+0x32 - 0x400</c>) et son tangage, et son <c>+0x10</c> monte ; a <c>0x14</c> il
    ///     prend le bit <c>0x40000000</c>, celui qui declenche la transformation
    ///     finale.</description></item>
    ///   <item><term>une cible morte</term><description>si elle porte <c>0x50000000</c>, le lien est
    ///     coupe des deux cotes et le projectile continue tout droit, sans meme la garde du cap :
    ///     <c>edi</c> et <c>ebp</c> restent a zero, donc il pique vers le nord et
    ///     l'horizon.</description></item>
    /// </list>
    /// <para>
    /// <b>Son echelle suit sa vitesse.</b> Apres le pas, il mesure la distance <b>reellement
    /// parcourue en trois dimensions</b> et pose <c>+0x28 = (distance &lt;&lt; 12) / 30</c> — la
    /// magie <c>0x88888889</c> avec <c>sar 4</c>. Plus il file, plus il est gros.
    /// </para>
    /// <para>
    /// <b>Le verrou.</b> Quand l'ecart de cap <b>et</b> l'ecart de tangage tiennent tous deux dans
    /// une fenetre de <c>0x200</c> <b>et</b> que la distance parcourue est tombee sous <c>0xc8</c>,
    /// le bit <c>0x10</c> s'allume : le projectile cesse de viser. C'est ce qui l'empeche de
    /// tourner en rond autour d'une cible qu'il a rattrapee.
    /// </para>
    /// <para>
    /// <b>La fin a deux issues.</b> Sans le bit <c>0x40000000</c> et si <c>+0x90</c> ne porte ni
    /// <c>0xa000</c> ni <c>0x802</c>, il sonde
    /// (<see cref="ProbeMeleeTarget_00474CA0"/>) ; un touche appelle
    /// <see cref="ResolveMeleeHit_00539B80"/> <b>au nom de son lanceur <c>+0x74</c></b>, pose
    /// <c>0x40</c> et <c>+0x96 = 8</c> sur la victime, et <b>efface le projectile en ecrivant zero
    /// dans son mot de tete</b>. Sinon il <b>se transforme</b> : vitesse relevee a
    /// <c>(0x1800, 0x1400, 0x1800)</c>, teinte grise, echelle et tangage remis a plat, un
    /// <c>0x6c</c> engendre, le son <c>0x2e</c> a sa position — puis il se raye lui aussi, en
    /// liberant d'abord le <c>+0x74</c> de sa cible.
    /// </para>
    /// <para>
    /// <b>Le sol l'arrete par le haut seulement</b> : si sa hauteur descend au niveau du sol trouve
    /// (<c>+0x60</c>), elle y est calee et <c>+0x90</c> prend <c>0x800</c> — le bit meme que la
    /// garde ci-dessus consulte, ce qui envoie droit a la transformation.
    /// </para>
    /// </summary>
    private static void EntityHandler_00486320(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = unchecked((short)0xfc00);
        probe[1] = unchecked((short)0xff80);
        probe[2] = 0x32;
        probe[3] = 0xc8;
        probe[4] = 0;
        probe[5] = 0x80;

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x80;
            *(short*)(e + 0x10) = 1;
            e[0x36] |= 6;
        }

        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);

        byte* target = *(byte**)(e + 0x78);
        byte* held = target;                                        // Ghidra: `[esp+0x44]`, l'emplacement d'argument reutilise

        int aimYaw = 0;
        int aimPitch = 0;

        if (target != null && (*(uint*)e & 0x10) == 0)
        {
            if ((*(uint*)target & 0x50000000) != 0)
            {
                // La cible est morte : le lien se coupe des deux cotes, et les deux angles de
                // visee restent a ZERO — le binaire ne repasse pas par la garde du cap.
                held = null;
                *(byte**)(target + 0x74) = null;
                *(byte**)(e + 0x78) = null;
            }
            else
            {
                *(byte**)(target + 0x74) = e;

                int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

                int bubble = StatusBubbleHeight_00538BC0((uint*)target);   // Ghidra: FUN_00538bc0
                int dy = (((bubble & ~1) << 0x0b) + *(int*)(target + 0x20) - *(int*)(e + 0x20)) >> 0xc;

                int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));   // Ghidra: 0x00545a10
                aimYaw = Winsys3d.Ordinal_457(dx, dz);                             // Ghidra: 0x00545a22
                aimPitch = Winsys3d.Ordinal_457(dy, flat);
            }
        }
        else
        {
            *(short*)(e + 0x10) += 1;
            if (*(short*)(e + 0x10) >= 0x14) *(uint*)e |= 0x40000000;

            aimYaw = *(short*)(e + 0x32) - 0x400;
            aimPitch = *(short*)(e + 0x34);
        }

        // ------------------------------------------------- la poussee, la vitesse, le pas
        int* push = stackalloc int[3];
        SetVectorFromAngles_00532AD0(push, aimYaw, aimPitch, 0x0c);   // Ghidra: FUN_00532ad0

        int vx = *(int*)(e + 0x50) + push[0];
        int vy = *(int*)(e + 0x54) + push[1];
        int vz = *(int*)(e + 0x58) + push[2];

        vx -= vx >> 3;
        vy -= vy >> 3;
        vz -= vz >> 3;

        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x58) = vz;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        // ------------------------------------------------- ce qu'il a reellement parcouru
        int wentX = (*(int*)(e + 0x1c) - savedX) >> 0xc;
        int wentY = (*(int*)(e + 0x20) - savedY) >> 0xc;
        int wentZ = (*(int*)(e + 0x24) - savedZ) >> 0xc;

        int wentFlat = (int)Winsys3d.Ordinal_442((uint)(wentX * wentX + wentZ * wentZ));

        *(short*)(e + 0x32) = (short)(Winsys3d.Ordinal_457(wentX, wentZ) + 0x400);
        *(short*)(e + 0x34) = (short)Winsys3d.Ordinal_457(wentY, wentFlat);

        aimYaw = aimYaw - *(short*)(e + 0x32) + 0x400;
        aimPitch -= *(short*)(e + 0x34);

        int travelled = (int)Winsys3d.Ordinal_442(
            (uint)(wentX * wentX + wentY * wentY + wentZ * wentZ));

        // Ghidra: magie 0x88888889 avec `sar edx,4` — une division signee par 30.
        *(short*)(e + 0x28) = (short)((travelled << 0xc) / 30);

        aimYaw = (aimYaw + 0x100) & 0xfff;

        if (aimYaw < 0x200)
        {
            aimPitch = (aimPitch + 0x100) & 0xfff;
            if (aimPitch < 0x200 && travelled < 0xc8) *(uint*)e |= 0x10;
        }

        FinalizeEntityPlacement2((uint*)e);                         // Ghidra: FUN_00533940

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60))
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(int*)(e + 0x90) = 0x800;
        }

        // ------------------------------------------------- la fin, a deux issues
        bool transforms = (*(uint*)e & 0x40000000) != 0;

        if (!transforms)
        {
            uint contacts = *(uint*)(e + 0x90);
            // Ghidra: `test ah,0A0h` puis `test eax,802h` — la premiere passe court-circuite la
            // seconde, donc 0xa000 mene a la sonde et 0x802 seul a la transformation.
            if ((contacts & 0xa000) == 0 && (contacts & 0x802) != 0) transforms = true;
        }

        if (!transforms)
        {
            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);     // Ghidra: FUN_00474ca0
            if (victim == null) return;

            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);   // Ghidra: FUN_00539b80
            *(uint*)victim |= 0x40;
            *(short*)(victim + 0x96) = 8;
            *(uint*)e = 0;
            return;
        }

        *(byte**)(e + 0x70) = null;
        *(int*)(e + 0x44) = 0;
        *(int*)(e + 0x48) = 0;
        *(int*)(e + 0x4c) = 0;
        *(int*)(e + 0x50) = 0x1800;
        *(int*)(e + 0x54) = 0x1400;
        *(int*)(e + 0x58) = 0x1800;
        *(uint*)(e + 0x38) = 0x808080;
        *(int*)(e + 0x28) = 0;
        *(short*)(e + 0x2c) = 0;
        *(short*)(e + 0x34) = 0;
        e[4] = 1;

        SpawnCastEntity_0047AAE0(e, 0x6c, 1, 7);                    // Ghidra: FUN_0047aae0
        Aud.PlayPositionalSound(0x2e, 0xb52, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20

        if (held != null) *(byte**)(held + 0x74) = null;
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0043b870 @ 0x0043b870 — sous-zone 1, table <c>0x0056b2c8</c>. Decompilee dans
    /// <c>Blaze.c</c> ; 369 instructions.
    ///
    /// <para>
    /// <b>Le sort en trois temps de la salle de la machine.</b> Ce corps sert <b>trois roles
    /// differents</b> selon le <c>+0x14</c> que son engendreur lui pose : la spirale montante
    /// (<c>0</c>), la masse qui tombe et blesse (<c>1</c>), et l'<b>eclair d'ecran</b> qui
    /// empoisonne tout le groupe (<c>2</c>). C'est le seul corps de la grotte a se partager ainsi.
    /// </para>
    /// <para>
    /// <b>Une garde commune les precede</b>, et elle ne vaut que pour les roles <c>0</c> et
    /// <c>2</c> : si le lanceur <c>+0x6c</c> est mort — <c>0x40000000</c> pose, ou
    /// <c>0x80000000</c> absent — l'entite s'efface, et dans le role <c>2</c> elle <b>eteint le
    /// voile au passage</b> pour ne pas laisser l'ecran teinte.
    /// </para>
    /// <list type="table">
    ///   <item><term>role 0, la spirale</term><description>planche <c>0x00551168</c>, image
    ///     <c>rand &amp; 1</c>, teinte grise, echelle <c>0x400</c>. Elle nait <c>0x64</c> unites
    ///     au-dessus de son point de depart, garde ce point dans <c>+0x50</c>..<c>+0x58</c>, et
    ///     tourne autour : le rayon <c>+0x5c</c> part entre <c>0x14</c> et <c>0x31</c> et gagne
    ///     <c>0x14</c> a <c>0x27</c> par image, le pas d'angle <c>+0x16</c> vaut <c>100</c> a
    ///     <c>0x95</c>. Elle monte de <c>8</c> unites par image et enfle de <c>200</c> jusqu'a
    ///     <c>5000</c>. Elle meurt passe l'age <c>0x32</c>, ou des <c>0x1e</c> une fois sur
    ///     cinquante.</description></item>
    ///   <item><term>role 1, la masse</term><description>planche <c>0x00551178</c>, echelle
    ///     <c>3000</c>, cap devie de <c>±100</c>, nee <c>0x78</c> unites plus haut. Elle avance sur
    ///     son cap de <c>+0x12</c>, qui croit de <c>+0x5c</c> (<c>0xf</c> a <c>0x18</c>) par image,
    ///     et <b>tombe de plus en plus vite</b> — <c>+0x9a</c> monte de 2 par image, plafond
    ///     <c>0x1e</c>. Un mur (<c>+0x90 &amp; 0x8802</c>) la remet en place et la repose au
    ///     sol.</description></item>
    ///   <item><term>role 2, l'eclair</term><description>quinze images de compte a rebours ; a
    ///     <c>10</c>, <c>9</c>, <c>8</c> et <c>7</c> le voile plein cadre passe par quatre teintes
    ///     de plus en plus sombres, a <c>6</c> il s'eteint <b>et l'effet 11 frappe les quatre
    ///     membres</b>, a <c>0</c> l'entite s'efface.</description></item>
    /// </list>
    /// <para>
    /// <b>Le role 1 blesse par contact, pas par sonde.</b> Il balaie lui-meme les quatre
    /// emplacements et frappe (<see cref="ResolveMeleeHit_00539B80"/>) tout membre vivant, dont le
    /// <c>+0x12</c> est nul, a moins de <c>0x1e</c> unites <b>en trois dimensions</b> et dont le
    /// <c>+0x96</c> est libre ; il lui pose alors <c>+0x96 = 0x10</c> et le bit <c>0x40</c>.
    /// </para>
    /// <para>
    /// <b>Le rebond du role 1 est ecrit a l'envers</b> : au contact du sol, <c>+0x44</c> — parti de
    /// <c>-20</c> — gagne 10, le rayon est divise par deux, et <b>c'est quand <c>+0x44</c> devient
    /// positif que l'entite s'efface</b>. Elle rebondit donc exactement deux fois. Le
    /// <c>+0x44</c> sert ensuite de vitesse de chute initiale, ce qui fait repartir la masse vers le
    /// haut : <c>+0x9a</c> est recopie depuis lui, donc negatif.
    /// </para>
    /// <para>
    /// <b>L'effet 11</b> passe par l'enveloppe gardee <c>FUN_0043bd90</c> — <c>FUN_004364b0</c>
    /// puis <c>FUN_0053b730</c> —, exactement la forme des vingt et une enveloppes du lot 10.
    /// </para>
    /// </summary>
    private static void EntityHandler_0043B870(byte* e)
    {
        short role = *(short*)(e + 0x14);
        uint caster = **(uint**)(e + 0x6c);

        if ((role == 0 || role == 2) && ((caster & 0x40000000) != 0 || (caster & 0x80000000) == 0))
        {
            *(uint*)e &= 0x7fffffff;

            if (role == 2) Mem.s_Unknown_005de024 = 0x01000000;     // Ghidra: DAT_005de024..27
            return;
        }

        if (role == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(byte**)(e + 0x3c) = Descriptors00551168();        // Ghidra: &DAT_00551168
                e[0x0a] = 10;
                e[0x0b] = 0;
                *(short*)(e + 0x10) = 1;
                e[0x40] = (byte)SignedMask(CrtRand(), 1);

                *(ushort*)(e + 0x12) = (ushort)SignedMask(CrtRand(), 0xfff);
                *(int*)(e + 0x5c) = CrtRand() % 0x1e + 0x14;

                int spin = CrtRand();
                *(short*)(e + 0x9a) = 0;

                int born = *(int*)(e + 0x20);
                *(short*)(e + 0x28) = 0x400;
                *(short*)(e + 0x2a) = 0x400;
                *(int*)(e + 0x20) = born + 0x64000;
                *(int*)(e + 0x54) = born + 0x64000;
                *(short*)(e + 0x16) = (short)(spin % 0x32 + 100);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);

                e[0x38] = 0x80;
                e[0x39] = 0x80;
                e[0x3a] = 0x80;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12))
                                    * *(int*)(e + 0x5c) + *(int*)(e + 0x50);   // Ghidra: Ordinal_451
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12))
                                    * *(int*)(e + 0x5c) + *(int*)(e + 0x58);   // Ghidra: Ordinal_450
                *(int*)(e + 0x20) += 0x8000;
                *(ushort*)(e + 0x12) =
                    (ushort)((*(short*)(e + 0x16) + *(short*)(e + 0x12)) & 0xfff);

                int grow = CrtRand();
                *(short*)(e + 0x28) += 200;
                *(short*)(e + 0x2a) += 200;
                *(int*)(e + 0x5c) += grow % 0x14 + 0x14;

                if (*(short*)(e + 0x28) > 5000)
                {
                    *(short*)(e + 0x28) = 5000;
                    *(short*)(e + 0x2a) = 5000;
                }

                *(short*)(e + 0x9a) += 1;

                int fade = CrtRand();
                if (*(short*)(e + 0x9a) > 0x32
                    || (*(short*)(e + 0x9a) > 0x1e && (short)(fade % 0x32) == 1))
                {
                    *(uint*)e &= 0x7fffffff;
                }
            }
        }

        if (role == 1)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(byte**)(e + 0x3c) = Descriptors00551178();        // Ghidra: &DAT_00551178
                e[0x40] = 0;
                e[0x0a] = 2;
                e[0x0b] = 0;
                *(short*)(e + 0x10) = 1;

                int veer = CrtRand();
                *(short*)(e + 0x12) = 0;
                *(short*)(e + 0x28) = 3000;
                *(short*)(e + 0x2a) = 3000;
                *(short*)(e + 0x32) += (short)(veer % 200 - 100);

                int born = *(int*)(e + 0x20);
                *(int*)(e + 0x20) = born + 0x78000;
                *(int*)(e + 0x54) = born + 0x78000;
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                int speed = CrtRand();
                *(short*)(e + 0x9a) = 0;
                *(short*)(e + 0x16) = 0;
                *(int*)(e + 0x44) = -20;
                *(int*)(e + 0x5c) = speed % 10 + 0x0f;
                *(uint*)e |= 0x680;
            }

            if ((*(uint*)e & 0x40000000) != 0) *(uint*)e &= 0x7fffffff;

            if (*(short*)(e + 0x10) == 1)
            {
                byte* member = Inp.s_SlotFlags_005e93c0;            // 0x005e93c0

                for (int slot = 0; slot < 4; slot++, member += 0x9c)
                {
                    uint flags = *(uint*)member;
                    if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
                    if (*(short*)(member + 0x12) != 0) continue;

                    int dx = (*(int*)(e + 0x1c) - *(int*)(member + 0x1c)) >> 0xc;
                    int dy = (*(int*)(e + 0x20) - *(int*)(member + 0x20)) >> 0xc;
                    int dz = (*(int*)(e + 0x24) - *(int*)(member + 0x24)) >> 0xc;

                    int reach = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
                    if (reach >= 0x1e || *(short*)(member + 0x96) != 0) continue;

                    ResolveMeleeHit_00539B80(*(uint**)(e + 0x6c), (uint*)member);   // Ghidra: FUN_00539b80
                    *(short*)(member + 0x96) = 0x10;
                    *(uint*)member |= 0x40;
                }

                int savedX = *(int*)(e + 0x1c);
                int savedZ = *(int*)(e + 0x24);

                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32))
                                    * *(short*)(e + 0x12) + *(int*)(e + 0x50);
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32))
                                    * *(short*)(e + 0x12) + *(int*)(e + 0x58);

                FinalizeEntityPlacement2((uint*)e);                 // Ghidra: FUN_00533940

                if ((*(uint*)(e + 0x90) & 0x8802) != 0)
                {
                    *(int*)(e + 0x1c) = savedX;
                    *(int*)(e + 0x24) = savedZ;
                    FinalizeEntityPlacement2((uint*)e);
                }

                *(short*)(e + 0x12) += (short)*(int*)(e + 0x5c);
                if (*(int*)(e + 0x5c) < 0) *(int*)(e + 0x5c) = 0;

                short fall = (short)(*(short*)(e + 0x9a) + 2);
                *(int*)(e + 0x20) += *(short*)(e + 0x9a) * -0x1000;
                *(short*)(e + 0x9a) = fall;
                if (fall > 0x1e) *(short*)(e + 0x9a) = 0x1e;

                if (*(int*)(e + 0x20) < *(int*)(e + 0x60))
                {
                    int bounces = *(int*)(e + 0x44);
                    *(int*)(e + 0x44) = bounces + 10;
                    *(int*)(e + 0x5c) >>= 1;

                    if (bounces + 10 > 0) *(uint*)e &= 0x7fffffff;

                    *(int*)(e + 0x20) = *(int*)(e + 0x60);
                    *(short*)(e + 0x9a) = (short)*(int*)(e + 0x44);
                }
            }
        }

        if (role != 2) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x0f;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) -= 1;

        // Ghidra: DAT_005de027 = 1 et les trois octets de couleur — l'octet de poids fort du meme
        // dword commande l'affichage du voile.
        if (*(short*)(e + 0x12) == 10) Mem.s_Unknown_005de024 = 0x016464fa;
        if (*(short*)(e + 0x12) == 9) Mem.s_Unknown_005de024 = 0x015050b4;
        if (*(short*)(e + 0x12) == 8) Mem.s_Unknown_005de024 = 0x013c3c78;
        if (*(short*)(e + 0x12) == 7) Mem.s_Unknown_005de024 = 0x011e1e50;

        if (*(short*)(e + 0x12) == 6)
        {
            Mem.s_Unknown_005de024 = 0x01000000;

            byte* member = Inp.s_SlotFlags_005e93c0;
            for (int slot = 0; slot < 4; slot++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                AfflictEleven_0043BD90(*(uint**)(e + 0x6c), (uint*)member, 0);   // Ghidra: FUN_0043bd90
            }
        }

        if (*(short*)(e + 0x12) == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: FUN_0043bd90 @ 0x0043bd90 — l'<b>enveloppe gardee</b> de l'effet 11,
    /// exactement la forme des vingt et une du lot 10 : la paire est-elle bloquee, sinon applique.
    /// </summary>
    private static void AfflictEleven_0043BD90(uint* caster, uint* member, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, member) != 0) return;   // Ghidra: FUN_004364b0
        ApplyStatusEffect_0053B730(caster, member, keep);                // Ghidra: FUN_0053b730
    }

    /// <summary>
    /// Ghidra: <c>0x004396e0</c> (sous-zones 6 et 7) et <c>0x0043c4a0</c> (11 et 12), table
    /// <c>0x0056b1c8</c>. <b>Absentes de <c>Blaze.c</c></b> ; 289 instructions chacune,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_004396E0.asm</c> et <c>FUN_0043C4A0.asm</c>.
    /// <b>Les deux corps sont le meme</b>, a quatre differences pres, toutes portees en parametres.
    ///
    /// <para>
    /// <b>Le souffle accroche au lanceur.</b> Il ne vole pas : a chaque image il se <b>repose</b>
    /// devant le <c>+0x74</c> qui l'a engendre — quatorze unites en avant, <c>0x73000</c> au-dessus
    /// —, s'etire, balaie le groupe, puis <b>remet sa position ou elle etait</b>. Le deplacement
    /// n'existe que le temps de la sonde.
    /// </para>
    /// <list type="table">
    ///   <item><term>a la naissance</term><description>il perd le bit 1 de <c>+0x36</c>, prend le
    ///     modele <c>0</c>, l'echelle <c>(0xfa, 0, 0xfa)</c>, la teinte <c>(0x96, 0, 0xc8)</c> — un
    ///     violet —, une duree de <c>0x0a</c>, et remet a zero sa portee <c>+0x16</c> et son
    ///     <b>masque de membres deja touches</b> <c>+0x44</c>.</description></item>
    ///   <item><term>etat 1, le souffle</term><description>la portee <c>+0x16</c> monte vers
    ///     <c>0x258</c> et la hauteur <c>+0x2a</c> vers <c>0x4000</c>, toutes deux <b>d'un quart de
    ///     l'ecart par image</b>. A la premiere image seulement, un <c>0xec</c> nait a sa position.
    ///     Un mur (<c>+0x90 &amp; 0x8802</c>) ou la fin du compte le fait passer a
    ///     l'etat 2.</description></item>
    ///   <item><term>etat 2, l'extinction</term><description>la hauteur et les trois canaux de
    ///     teinte perdent leur quart et leur huitieme par image, le tangage passe a <c>0xc00</c>,
    ///     et au bout de dix images il s'efface.</description></item>
    /// </list>
    /// <para>
    /// <b>Le cone se mesure sur le cap du LANCEUR</b>, pas sur le sien : l'angle vers chaque membre
    /// est ramene par <c>caster[0x32]</c>. Il touche a moins de <c>0xc8</c> unites <b>en trois
    /// dimensions</b>, dans un cone de <c>0x320</c> de demi-ouverture, et <b>une seule fois par
    /// membre</b> — c'est a quoi sert le masque <c>+0x44</c>. L'effet appliqué est le <b>8</b>, par
    /// l'enveloppe gardee <c>FUN_00439ac0</c> (<c>FUN_004364b0</c> puis <c>FUN_0053b4b0</c>), la
    /// meme forme que les vingt et une du lot 10.
    /// </para>
    /// <para>
    /// <b>Les quatre differences entre les deux copies</b> : la teinte (violet <c>(0x96, 0, 0xc8)</c>
    /// contre gris <c>(0x96, 0x96, 0x96)</c>), le sous-type engendre (<c>0xec</c> contre <c>0xdf</c>),
    /// le coup porte (l'effet 8 contre <b>5 % des points de vie maximum</b> par
    /// <see cref="ApplyPercentDamage_005397B0"/>), et le champ de decalage angulaire.
    /// </para>
    /// <para>
    /// <b>Ce dernier merite un mot.</b> <c>0x0043c4a0</c> lit le mot <c>+0x9a</c> dans ses DEUX
    /// etats ; <c>0x004396e0</c> lit le <b>dword</b> <c>+0x5c</c> dans l'etat 1 et le mot
    /// <c>+0x9a</c> dans l'etat 2. La copie coherente etant celle de la sous-zone 11, c'est bien
    /// <c>0x004396e0</c> qui devie — son souffle saute donc d'angle en passant a l'extinction.
    /// Aucun des deux champs n'est jamais ecrit par ces corps : ils viennent de l'engendreur.
    /// Releve tel quel.
    /// </para>
    /// </summary>
    private static void TickCasterBreath_004396E0(
        byte* e, byte spawn, byte tintR, byte tintG, byte tintB,
        bool holdsInState1, bool poisons)
    {
        byte* caster = *(byte**)(e + 0x74);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] &= 0xfe;
            e[4] = 0;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x28) = 0xfa;
            *(short*)(e + 0x2c) = 0xfa;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x12) = 0x0a;
            e[0x38] = tintR;
            e[0x39] = tintG;
            e[0x3a] = tintB;
            *(short*)(e + 0x16) = 0;
            *(int*)(e + 0x44) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x34) = 0x400;
            *(short*)(e + 0x32) = (short)(*(short*)(caster + 0x32) + 0xc00);
            *(int*)(e + 0x20) = *(int*)(caster + 0x20) + 0x73000;

            // `shl 3 ; sub` puis `lea [.. + edx*2]` : le rayon d'accrochage vaut 14 unites.
            int offset = holdsInState1 ? *(short*)(e + 0x9a) : *(int*)(e + 0x5c);
            int anchor = offset + *(short*)(e + 0x32) - 0xc00;
            *(int*)(e + 0x1c) = *(int*)(caster + 0x1c)
                                + (int)Winsys3d.GetSinTableEntry(anchor) * 14;   // Ghidra: 0x00545a1c
            anchor = offset + *(short*)(e + 0x32) - 0xc00;
            *(int*)(e + 0x24) = *(int*)(caster + 0x24)
                                + Winsys3d.GetCosTableEntry(anchor) * 14;        // Ghidra: 0x00545a16

            if (*(short*)(e + 0x12) == 0x0a)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, spawn, 0, -1);   // Ghidra: FUN_0047ac40
            }

            int savedX = *(int*)(e + 0x1c);
            int savedY = *(int*)(e + 0x20);
            int savedZ = *(int*)(e + 0x24);

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) - 0xc00)
                                 * *(short*)(e + 0x16);
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) - 0xc00)
                                 * *(short*)(e + 0x16);

            byte* member = Inp.s_SlotFlags_005e93c0;                // 0x005e93c0

            for (int slot = 0; slot < 4; slot++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dy = (*(int*)(member + 0x20) - *(int*)(e + 0x20)) >> 0xc;
                int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                int angle = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(caster + 0x32)) & 0xfff;
                int reach = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

                if (reach >= 0xc8) continue;

                // Le binaire ne rejette pas tout de suite un angle nul ou negatif sous l'ouverture :
                // il tombe sur le second intervalle. Les deux tests sont donc un OU.
                bool inCone = (angle < 0x320 && angle > 0) || (angle < 0x1000 && angle > 0xce0);
                if (!inCone) continue;

                int bit = 1 << slot;
                if ((*(int*)(e + 0x44) & bit) != 0) continue;
                *(int*)(e + 0x44) |= bit;

                if (poisons) AfflictEight_00439AC0((uint*)caster, (uint*)member, 0);   // Ghidra: FUN_00439ac0
                else ApplyPercentDamage_005397B0((uint*)member, 5, 0);                 // Ghidra: FUN_005397b0
            }

            FinalizeEntityPlacement2((uint*)e);                     // Ghidra: FUN_00533940

            *(int*)(e + 0x24) = savedZ;
            *(int*)(e + 0x1c) = savedX;
            *(int*)(e + 0x20) = savedY;

            short height = *(short*)(e + 0x2a);
            *(short*)(e + 0x2a) = (short)(((0x4000 - height) >> 2) + height);

            short span = *(short*)(e + 0x16);
            *(short*)(e + 0x16) = (short)(((0x258 - span) >> 2) + span);

            if ((*(uint*)(e + 0x90) & 0x8802) != 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0x0a;
            }

            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) <= 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0x0a;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x34) = 0x400;
        *(short*)(e + 0x32) = (short)(*(short*)(caster + 0x32) + 0xc00);
        *(int*)(e + 0x20) = *(int*)(caster + 0x20) + 0x73000;

        // L'etat 2 lit +0x9a la ou l'etat 1 lit +0x5c — voir le dernier paragraphe.
        int hold = *(short*)(e + 0x9a) + *(short*)(e + 0x32) - 0xc00;
        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + (int)Winsys3d.GetSinTableEntry(hold) * 14;
        hold = *(short*)(e + 0x9a) + *(short*)(e + 0x32) - 0xc00;
        *(int*)(e + 0x24) = *(int*)(caster + 0x24) + Winsys3d.GetCosTableEntry(hold) * 14;

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) - 0xc00)
                             * *(short*)(e + 0x16);
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) - 0xc00)
                             * *(short*)(e + 0x16);

        *(short*)(e + 0x34) = 0xc00;

        short shrink = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)(((-shrink) >> 2) + shrink);

        e[0x38] = (byte)(((-(int)e[0x38]) >> 3) + e[0x38]);
        e[0x39] = (byte)(((-(int)e[0x39]) >> 3) + e[0x39]);
        e[0x3a] = (byte)(((-(int)e[0x3a]) >> 3) + e[0x3a]);

        *(short*)(e + 0x12) -= 1;
        if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: <c>0x004396e0</c> — sous-zones 6 et 7 : le souffle violet qui empoisonne.
    /// </summary>
    private static void EntityHandler_004396E0(byte* e)
        => TickCasterBreath_004396E0(e, 0xec, 0x96, 0, 0xc8, holdsInState1: false, poisons: true);

    /// <summary>Ghidra: <c>0x0043c4a0</c> — sous-zones 11 et 12 : le souffle gris qui ronge cinq
    /// pour cent des points de vie.</summary>
    private static void EntityHandler_0043C4A0(byte* e)
        => TickCasterBreath_004396E0(e, 0xdf, 0x96, 0x96, 0x96, holdsInState1: true, poisons: false);

    /// <summary>Ghidra: FUN_00439ac0 @ 0x00439ac0 — l'<b>enveloppe gardee</b> de l'effet 8,
    /// decompilee dans <c>Blaze.c</c> : la paire est-elle bloquee, sinon applique.</summary>
    private static void AfflictEight_00439AC0(uint* caster, uint* member, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, member) != 0) return;   // Ghidra: FUN_004364b0
        ApplyStatusEffect_0053B4B0(caster, member, keep);                // Ghidra: FUN_0053b4b0
    }

    /// <summary>
    /// Ghidra: <c>0x00436950</c> — sous-zones 4 et 9, table <c>0x0056b1c8</c>. <b>Absente de
    /// <c>Blaze.c</c></b> ; 436 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00436950.asm</c>.
    ///
    /// <para>
    /// <b>Le souffle elementaire.</b> Meme charpente que
    /// <see cref="TickCasterBreath_004396E0"/> — accroche au lanceur, etirement par quarts, cone
    /// mesure sur le cap du lanceur, masque de membres deja touches, extinction en dix images —
    /// avec deux ajouts qui en font un corps a part.
    /// </para>
    /// <list type="table">
    ///   <item><term>son point d'accroche est <b>donne par une troisieme entite</b></term>
    ///     <description>le <c>+0x6c</c> porte un vecteur local dans <c>+0x50</c> (lateral),
    ///     <c>+0x54</c> (hauteur) et <c>+0x58</c> (avant) que le corps <b>fait tourner dans le
    ///     repere du lanceur</b> avant de l'ajouter. C'est ainsi que plusieurs souffles peuvent
    ///     sortir de points differents d'une meme bete. Le rayon propre vaut <b>15</b> unites, la
    ///     ou celui de <see cref="TickCasterBreath_004396E0"/> en vaut 14.</description></item>
    ///   <item><term>son element se lit dans <c>+0x9a</c></term><description><c>0</c> donne le bleu
    ///     <c>(0, 0x82, 0x96)</c> et l'effet de <c>0x0053b3c0</c> ; <c>1</c> le rouge
    ///     <c>(0x96, 0, 0)</c> et <b>un effet tire au hasard parmi neuf</b> ; <c>2</c> le magenta
    ///     <c>(0x96, 0, 0x96)</c> et l'effet de <c>0x0053b4b0</c>. Toute autre valeur laisse la
    ///     teinte telle quelle et ne fait rien.</description></item>
    /// </list>
    /// <para>
    /// <b>Les neuf effets du tirage</b> sont la table de sauts <c>0x00436ef0</c>, et ses neuf
    /// appelees sont les memes <b>enveloppes gardees</b> que le lot 10 : <c>0x004364b0</c> puis
    /// l'applicateur. Dans l'ordre du tirage : <c>0053c420</c>, <c>0053b3c0</c>, <c>0053bf00</c>,
    /// <c>0053b4b0</c>, <c>0053b660</c>, <c>0053b580</c>, <c>0053b8f0</c>, <c>0053c510</c>,
    /// <c>0053b9c0</c>. Le tirage est <c>rand % 9</c> ; la garde <c>ja 8</c> ne peut pas echouer, un
    /// reste de division par neuf etant toujours dans <c>0..8</c> — <b>sauf sur un <c>rand</c>
    /// negatif</b>, que la bibliotheque de 1998 ne rend jamais.
    /// </para>
    /// <para>
    /// <b>Sans lanceur, le corps travaille sur un enregistrement de rechange</b>
    /// (<c>0x005de558</c>) — mais il continue de lire le cap et la position du lanceur, c'est-a-dire
    /// <b>l'adresse nulle</b>. Le garde-fou de 1998 ne protege donc que les ecritures. Transcrit tel
    /// quel : si ce chemin s'ouvrait un jour, il doit se voir.
    /// </para>
    /// </summary>
    private static void EntityHandler_00436950(byte* e)
    {
        byte* caster = *(byte**)(e + 0x74);
        if (caster == null) e = Res.s_EntityList_005de558;          // Ghidra: DAT_005de558

        byte* anchor = *(byte**)(e + 0x6c);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        if (*(short*)(e + 0x10) == 0)
        {
            short element = *(short*)(e + 0x9a);

            *(short*)(e + 0x28) = 0xfa;
            *(short*)(e + 0x2c) = 0xfa;
            e[4] = 0;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x12) = 0x0a;

            if (element == 0) { e[0x38] = 0; e[0x39] = 0x82; e[0x3a] = 0x96; }
            if (element == 1) { e[0x38] = 0x96; e[0x39] = 0; e[0x3a] = 0; }
            if (element == 2) { e[0x38] = 0x96; e[0x39] = 0; e[0x3a] = 0x96; }

            e[0x36] &= 0xfe;
            *(short*)(e + 0x16) = 0;
            *(int*)(e + 0x44) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x34) = 0x400;
            *(short*)(e + 0x32) = (short)(*(short*)(caster + 0x32) + 0xc00);
            *(int*)(e + 0x20) = *(int*)(caster + 0x20) + *(int*)(anchor + 0x54);

            short course = *(short*)(caster + 0x32);
            int side = *(int*)(anchor + 0x50) >> 0xc;
            int ahead = *(int*)(anchor + 0x58) >> 0xc;

            // Le vecteur local du +0x6c, tourne dans le repere du lanceur.
            int x = *(int*)(caster + 0x1c)
                    - Winsys3d.GetCosTableEntry(course) * side                        // Ghidra: 0x00545a16
                    - (int)Winsys3d.GetSinTableEntry(course) * ahead;                 // Ghidra: 0x00545a1c
            int z = (int)Winsys3d.GetSinTableEntry(course) * side
                    + *(int*)(caster + 0x24)
                    - Winsys3d.GetCosTableEntry(course) * ahead;

            // `lea *3` puis `lea *5` : le rayon propre vaut 15 unites, et non 14.
            int own = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;
            *(int*)(e + 0x1c) = x + (int)Winsys3d.GetSinTableEntry(own) * 15;
            own = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;
            *(int*)(e + 0x24) = z + Winsys3d.GetCosTableEntry(own) * 15;

            if (*(short*)(e + 0x12) == 0x0a)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xdf, 0, -1);   // Ghidra: FUN_0047ac40
            }

            int savedX = *(int*)(e + 0x1c);
            int savedY = *(int*)(e + 0x20);
            int savedZ = *(int*)(e + 0x24);

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) - 0xc00)
                                 * *(short*)(e + 0x16);
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) - 0xc00)
                                 * *(short*)(e + 0x16);

            byte* member = Inp.s_SlotFlags_005e93c0;                // 0x005e93c0

            for (int slot = 0; slot < 4; slot++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
                int dy = (*(int*)(member + 0x20) - *(int*)(e + 0x20)) >> 0xc;
                int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;

                int angle = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(caster + 0x32)) & 0xfff;
                int reach = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

                if (reach >= 0xc8) continue;

                bool inCone = (angle < 0x320 && angle > 0) || (angle < 0x1000 && angle > 0xce0);
                if (!inCone) continue;

                int bit = 1 << slot;
                if ((*(int*)(e + 0x44) & bit) != 0) continue;
                *(int*)(e + 0x44) |= bit;

                StrikeWithElement_00436950(e, (uint*)caster, (uint*)member);
            }

            FinalizeEntityPlacement2((uint*)e);                     // Ghidra: FUN_00533940

            *(int*)(e + 0x24) = savedZ;
            *(int*)(e + 0x1c) = savedX;
            *(int*)(e + 0x20) = savedY;

            short height = *(short*)(e + 0x2a);
            *(short*)(e + 0x2a) = (short)(((0x4000 - height) >> 2) + height);

            short span = *(short*)(e + 0x16);
            *(short*)(e + 0x16) = (short)(((0x258 - span) >> 2) + span);

            if ((*(uint*)(e + 0x90) & 0x8802) != 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0x0a;
            }

            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) <= 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0x0a;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x34) = 0x400;
        *(short*)(e + 0x32) = (short)(*(short*)(caster + 0x32) + 0xc00);
        *(int*)(e + 0x20) = *(int*)(caster + 0x20) + *(int*)(anchor + 0x54);

        short bearing = *(short*)(caster + 0x32);
        int lateral = *(int*)(anchor + 0x50) >> 0xc;
        int forward = *(int*)(anchor + 0x58) >> 0xc;

        int fadeX = *(int*)(caster + 0x1c)
                    - Winsys3d.GetCosTableEntry(bearing) * lateral
                    - (int)Winsys3d.GetSinTableEntry(bearing) * forward;
        int fadeZ = (int)Winsys3d.GetSinTableEntry(bearing) * lateral
                    + *(int*)(caster + 0x24)
                    - Winsys3d.GetCosTableEntry(bearing) * forward;

        int tilt = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;
        *(int*)(e + 0x1c) = fadeX + (int)Winsys3d.GetSinTableEntry(tilt) * 15;
        tilt = *(int*)(e + 0x5c) + *(short*)(e + 0x32) - 0xc00;
        *(int*)(e + 0x24) = fadeZ + Winsys3d.GetCosTableEntry(tilt) * 15;

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) - 0xc00)
                             * *(short*)(e + 0x16);
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) - 0xc00)
                             * *(short*)(e + 0x16);

        *(short*)(e + 0x34) = 0xc00;

        short shrink = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)(((-shrink) >> 2) + shrink);

        e[0x38] = (byte)(((-(int)e[0x38]) >> 3) + e[0x38]);
        e[0x39] = (byte)(((-(int)e[0x39]) >> 3) + e[0x39]);
        e[0x3a] = (byte)(((-(int)e[0x3a]) >> 3) + e[0x3a]);

        *(short*)(e + 0x12) -= 1;
        if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Le coup du souffle elementaire, choisi par le <c>+0x9a</c> de l'entite : un effet
    /// fixe pour <c>0</c> et <c>2</c>, un tirage parmi neuf pour <c>1</c>. Les neuf branches sont la
    /// table de sauts <c>0x00436ef0</c>, et chacune est une <b>enveloppe gardee</b> — la paire
    /// est-elle bloquee (<c>0x004364b0</c>), sinon applique. Les neuf enveloppes ne different que
    /// par leur applicateur, exactement comme les vingt et une du lot 10.</summary>
    private static void StrikeWithElement_00436950(byte* e, uint* caster, uint* member)
    {
        short element = *(short*)(e + 0x9a);

        if (element == 0) AfflictGuarded(caster, member, 0x0053b3c0);   // Ghidra: FUN_00436470

        if (element == 1)
        {
            // Ghidra: `cdq ; idiv 9 ; movsx eax,dx ; cmp eax,8 ; ja ; jmp [eax*4+436EF0h]`.
            int draw = CrtRand() % 9;
            int applier = draw switch
            {
                0 => 0x0053c420,                                    // Ghidra: FUN_00436750
                1 => 0x0053b3c0,                                    // Ghidra: FUN_00436470
                2 => 0x0053bf00,                                    // Ghidra: FUN_00436690
                3 => 0x0053b4b0,                                    // Ghidra: FUN_00436510
                4 => 0x0053b660,                                    // Ghidra: FUN_00436590
                5 => 0x0053b580,                                    // Ghidra: FUN_00436550
                6 => 0x0053b8f0,                                    // Ghidra: FUN_004365d0
                7 => 0x0053c510,                                    // Ghidra: FUN_00436f60
                _ => 0x0053b9c0,                                    // Ghidra: FUN_00436f20
            };

            AfflictGuarded(caster, member, applier);
        }

        if (element == 2) AfflictGuarded(caster, member, 0x0053b4b0);   // Ghidra: FUN_00436510
    }

    /// <summary>L'enveloppe gardee commune aux onze sites du souffle elementaire : la paire
    /// est-elle bloquee par <see cref="IsEffectPairBlocked_004364B0"/>, sinon l'applicateur nomme
    /// par son adresse Ghidra est appele. Les onze corps du binaire sont identiques a cette
    /// indirection pres.</summary>
    private static void AfflictGuarded(uint* caster, uint* member, int applier)
    {
        if (IsEffectPairBlocked_004364B0(caster, member) != 0) return;   // Ghidra: FUN_004364b0

        switch (applier)
        {
            case 0x0053b3c0: ApplyStatusEffect_0053B3C0(caster, member, 0); break;
            case 0x0053b4b0: ApplyStatusEffect_0053B4B0(caster, member, 0); break;
            case 0x0053b580: ApplyStatusEffect_0053B580(caster, member, 0); break;
            case 0x0053b660: ApplyStatusEffect_0053B660(caster, member, 0); break;
            case 0x0053b8f0: ApplyStatusEffect_0053B8F0(caster, member, 0); break;
            case 0x0053b9c0: ApplyStatusEffect_0053B9C0(caster, member, 0); break;
            case 0x0053bf00: ApplyStatusEffect_0053BF00(caster, member, 0); break;
            case 0x0053c420: ApplyTrapEffect_0053C420(caster, member, 0); break;
            case 0x0053c510: ApplyStatusEffect_0053C510(caster, member, 0); break;
        }
    }
}
