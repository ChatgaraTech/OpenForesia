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
    private static void AfflictMember_00409F40(int kind, uint* caster, uint* member)
    {
        if ((uint)kind > 11) return;
        if (IsEffectPairBlocked_004364B0(caster, member) != 0) return;

        switch (kind)
        {
            case 0:  ApplyStatusEffect_0053BB20(caster, member, 0); break;   // 0x0040a480, effet 16
            case 1:  ApplyStatusEffect_0053BC60(caster, member, 0); break;   // 0x0040a4c0, effet 18
            case 2:  ApplyTrapEffect_0053C420(caster, member, 0); break;     // 0x0040a5c0
            case 3:  ApplyStatusEffect_0053B3C0(caster, member, 0); break;   // 0x0040a340
            case 4:  ApplyStatusEffect_0053BF00(caster, member, 0); break;   // 0x0040a500, effet 22
            case 5:  ApplyStatusEffect_0053B4B0(caster, member, 0); break;   // 0x0040a380, effet 8
            case 6:  ApplyStatusEffect_0053B660(caster, member, 0); break;   // 0x0040a400, effet 10
            case 7:  ApplyStatusEffect_0053B580(caster, member, 0); break;   // 0x0040a3c0, effet 9
            case 8:  ApplyStatusEffect_0053B8F0(caster, member, 0); break;   // 0x0040a440, effet 13
            case 9:  ApplyStatusEffect_0053BF00(caster, member, 0); break;   // 0x0040a500, effet 22
            case 10: ApplyStatusEffect_0053C040(caster, member, 0); break;   // 0x0040a540, effet 24
            case 11: ApplyStatusEffect_0053C1F0(caster, member, 0); break;   // 0x0040a580, effet 27
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
}
