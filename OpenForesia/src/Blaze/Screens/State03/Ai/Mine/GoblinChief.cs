// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE CHEF-GOBLIN — 0x004889A0, sous-type 2 du bloc A de la mine (zone 1, sous-zones 1 et 2).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, sous-zone 1 modele 3 et sous-zone 2
// modele 3 : « Chef-Goblin », 62 PV, 25 PM, rang 36). 1 152 instructions, absentes de Blaze.c ;
// releve Legacy/Raw/Blaze/FUN_004889A0.asm, table de sauts a QUATORZE entrees a 0x004898A0.
//
// C'EST LE CORPS DE L'OGRE (Ai/Mine/Ogre.cs) — meme charpente que le Pourpre preux, memes quatorze
// cases, meme prelude a trois blocs, meme pose au sol par SettleSceneEntityOnGroundAndWalls au
// masque 0x2000ad82. Le diff STRICT ne laisse que 156 lignes sur 1 152, et elles se rangent en
// SEPT ecarts :
//
//   1. SA SECONDE SONDE NE PLONGE PAS. {0, 0, 0x50, 0x64, 0, 0x64} contre
//      {0, -0x64, 0xc8, 0x64, 0, 0xc8} chez l'Ogre : le Chef-Goblin charge a hauteur d'homme et sur
//      la moitie de la portee. Il est deux fois plus petit, et sa charge le dit.
//   2. SA CHARGE (posture 0x0d) NE QUITTE PAS LE SOL. L'Ogre bondissait a l'image 10 ; le
//      Chef-Goblin se donne a l'image 6 une poussee que SA VARIANTE fixe — (+0x16 + 4) * 5 << 12 —
//      et cette poussee DECROIT d'un trente-deuxieme par image jusqu'a tomber sous 0xa000, ce qui
//      ferme le geste. C'est la seule charge amortie du depot.
//   3. ET SA CHARGE PROJETTE DANS SON PROPRE CAP, d'un elan proportionnel a ce qui lui reste de
//      poussee (+0x5c >> 13), la ou l'Ogre repoussait la victime a l'oppose de lui-meme.
//   4. SON COMBO frappe a l'image 8 de l'animation 3 puis a l'image 10 de l'animation 5 — l'Ogre
//      enchainait sur l'image 12 de l'animation 6.
//   5. SON EFFET (posture 0x0a) EN A DEUX : 0x36 pour la variante 1, 0x4b pour la variante 0. Ce
//      sont exactement les deux effets du Pourpre preux, aux variantes pres. L'Ogre n'avait que le
//      premier.
//   6. IL VIRE DEUX FOIS PLUS SEC : hardiesse + 0x80 sur place et hardiesse * 2 + 0x80 a
//      l'approche, contre hardiesse + 0x3c dans les deux cas chez l'Ogre.
//   7. SES DEUX PLAGES DE REPLANIFICATION ONT ECHANGE LEUR ARME. Au contact, la ou l'Ogre chargeait
//      sous 100 unites, le Chef-Goblin BONDIT (posture 6) si la proie est au-dela de
//      hardiesse + 0x64 ; a mi-distance, la ou l'Ogre marchait droit, il CHARGE. Il garde donc sa
//      charge pour l'espace qu'elle demande.
//
// Deux details de moins, sans effet visible : sa posture 5 est vide (l'Ogre y remettait le rebours
// a zero) alors que le prelude lui pose quand meme le bit 0x10, et sa posture 0x0b n'ecrit pas
// d'animation en entree — elle se contente de la phase.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x004889a0</c> — <b>l'intelligence du Chef-Goblin</b>, sous-type 2 de la table
    /// installee <c>0x0056ad48</c> dans les deux premieres sous-zones peuplees de la mine. Voir
    /// l'en-tete du fichier, et <see cref="TickSceneEntityAi_0048F230"/> pour la charpente.
    /// </summary>
    private static void TickSceneEntityAi_004889A0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x34] et [esp+0x40] — les deux sondes, dont quatre mots sont poses avant le
        // quatrieme push. La seconde est A PLAT et courte, contrairement a celle de l'Ogre.
        short* probes = stackalloc short[12];
        probes[0] = 0; probes[1] = 0x1e; probes[2] = 0x32; probes[3] = 0x64; probes[4] = 0;  probes[5] = 0x64;
        probes[6] = 0; probes[7] = 0;    probes[8] = 0x50; probes[9] = 0x64; probes[10] = 0; probes[11] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004898e0, copie de FUN_004a1020
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);
        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // La distance recalculee a trois dimensions, comme chez l'Ogre.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        if (speed != 0)
        {
            DecideGoblinChiefAction_004889A0(e, probes, model, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);              // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        SettleSceneEntityOnGroundAndWalls(e, boldness, 0x2000ad82, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanGoblinChiefAction_0048951C(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x00488b82..0x004892e8</c> — <b>le premier temps</b> : l'animation, le bit de
    /// posture figee, le prelude de geste, puis la posture. Quatorze cases, dont dix servies :
    /// 0 attendre, 1 se tourner, 2 approcher, 3 louvoyer, 6 bondir, 7 marcher droit, 10 l'effet,
    /// 11 le coup, 12 le combo, 13 la charge amortie. <b>Les cases 4, 5, 8 et 9 sont vides</b> — et
    /// la 5 l'est vraiment, contrairement a celle de l'Ogre, alors que le prelude lui pose quand
    /// meme le bit <c>0x10</c>.
    /// </summary>
    private static void DecideGoblinChiefAction_004889A0(
        byte* e, short* probes, byte* model, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le prelude de geste
        if ((flags & 2) != 0)
        {
            GoblinChiefGesture_00488BA2(e, probes, model);
        }
        else if ((flags & 1) != 0 && *(short*)(e + 0x1a) < 3)
        {
            // En l'air, dans les trois premieres images, le bond peut se changer en attaque — sur
            // l'animation 5 ici, la 3 chez l'Ogre.
            if (CrtRand() % 0x3c < boldness)
            {
                *(short*)(e + 0x18) = 5;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x1a) = 0;
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
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                }

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                }
                else
                {
                    *(short*)(e + 0x32) +=
                        (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                }

                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
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

                *(short*)(e + 0x32) +=
                    (short)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x80 : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : louvoyer
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    *(int*)(e + 0x1c) +=
                        ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                    *(int*)(e + 0x24) +=
                        (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x18) = 1;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 6 : bondir
            case 6:
                if (*(short*)(e + 0x12) == 0)
                {
                    uint f = *(uint*)e;

                    if ((f & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        *(uint*)e = f & 0xfeffffff;

                        int thrust = (boldness + 0x100) << 6;
                        int step = (speed * thrust) >> 0xc;
                        *(int*)(e + 0x5c) = thrust;
                        *(short*)(e + 0x18) = 1;            // animation 1, la 4 chez l'Ogre
                        *(short*)(e + 0x1a) = 0;

                        *(int*)(e + 0x50) +=
                            ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x58) +=
                            (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 1;
                        *(short*)(e + 0x12) = 1;
                    }
                }

                if ((*(uint*)e & 1) == 0) e[0x43] = 0;
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

            // ---------------------------------------------------------- 10 : l'effet
            case 10:
            {
                if (*(short*)(e + 0x12) == 1 && *(short*)(e + 0x1a) == 8)
                {
                    // Les deux effets du Pourpre preux, l'un pour chaque variante.
                    if (*(short*)(e + 0x16) == 1) SpawnEffectEntity(e, 0x36, 0, -1);
                    if (*(short*)(e + 0x16) == 0) SpawnEffectEntity(e, 0x4b, 0, -1);
                }

                uint f = *(uint*)e;
                if ((f & 0x2000000) != 0) e[0x43] = 0;

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(short*)(e + 0x18) = 5;
                *(short*)(e + 0x1a) = 0;
                *(uint*)e = f & 0xfeffffff;

                if (boldness < 0x1e) *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 11 : le coup
            case 11:
            {
                short phase = *(short*)(e + 0x12);

                // C'est le prelude qui frappe ; la posture ne fait que clore.
                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                if (phase != 0) return;

                // Aucune animation posee ici, contrairement a l'Ogre : c'est la replanification qui
                // l'a deja ecrite en armant le geste.
                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 12 : le combo
            case 12:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 2 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                // Premier coup fini : le second enchaine sur l'animation 5, prise a l'image 7.
                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfeffffff;
                    *(short*)(e + 0x12) = 2;
                    *(short*)(e + 0x18) = 5;
                    *(short*)(e + 0x1a) = 7;
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(short*)(e + 0x18) = 3;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 13 : la charge amortie
            case 13:
            {
                short phase = *(short*)(e + 0x12);

                if (phase == 2)
                {
                    // Un trente-deuxieme par image, et le geste se ferme quand la poussee passe
                    // sous 0xa000. L'Ogre amortissait d'un seizieme et attendait la fin
                    // d'animation ; ici c'est la VITESSE qui met fin a la charge.
                    int v = *(int*)(e + 0x5c);
                    v -= v >> 5;
                    *(int*)(e + 0x5c) = v;

                    if (v < 0xa000)
                    {
                        e[0x43] = 0;
                        *(uint*)e &= 0xfffffffd;
                        *(int*)(e + 0x5c) = 0;
                    }
                }

                if (phase == 1)
                {
                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }

                    // A la sixieme image, il se lance — SANS quitter le sol, et a une vitesse que
                    // sa variante fixe.
                    if (*(short*)(e + 0x1a) >= 6)
                    {
                        *(short*)(e + 0x12) = 2;
                        *(int*)(e + 0x5c) = (*(short*)(e + 0x16) + 4) * 5 << 0xc;
                        *(uint*)e |= 2;
                    }
                }

                if (*(short*)(e + 0x12) != 0) return;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x18) = 4;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 4, 5, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00488ba2..0x00488dab</c> — <b>le prelude de geste</b>, trois blocs comme chez
    /// <see cref="OgreGesture_0048F433"/>.
    ///
    /// <list type="number">
    /// <item><b>Hors combo et hors charge</b> : a l'image 8, la sonde A a pleine attaque.</item>
    /// <item><b>Le combo</b> (0x0c) : image 8 de l'animation 3 et image <b>10</b> de l'animation
    /// <b>5</b>, a trois quarts de l'attaque ; le premier coup ne fait pas chanceler.</item>
    /// <item><b>La charge</b> (0x0d) : la sonde B a chaque image, a moitie d'attaque — et le geste
    /// n'est <b>pas</b> referme ici, contrairement a l'Ogre : la charge du Chef-Goblin frappe tant
    /// qu'elle dure, et c'est son amortissement qui l'arrete.</item>
    /// </list>
    /// </summary>
    private static void GoblinChiefGesture_00488BA2(byte* e, short* probes, byte* model)
    {
        byte posture = e[0x42];

        if (posture != 0x0c && posture != 0x0d)
        {
            if (*(short*)(e + 0x1a) == 8)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 8)
                {
                    *(ushort*)(prey + 0x96) = 0x10;
                    *(uint*)prey |= 0x40;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                }
            }

            // Ghidra: LAB_00488c04
            uint flags = *(uint*)e;
            if ((flags & 0x2000000) != 0)
            {
                if ((flags & 1) == 0) e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
            }
        }

        if (e[0x42] == 0x0c && GoblinChiefComboStrikeFrame(e))
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && GoblinChiefComboStrikeFrame(e))
            {
                if (*(short*)(e + 0x12) == 1)
                {
                    *(ushort*)(prey + 0x96) = 0;
                }
                else
                {
                    *(ushort*)(prey + 0x96) = 0x10;
                    *(uint*)prey |= 0x40;
                }

                short power = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(power - (power >> 2));
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                *(short*)(model + 0x30) = power;
            }
        }

        if (e[0x42] != 0x0d) return;

        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probes + 6, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(victim + 0x96) = 0x10;
        *(uint*)victim |= 0x40;

        short attack = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(attack >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
        *(short*)(model + 0x30) = attack;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));

        if ((*(uint*)victim & 0x40000000) != 0) return;

        SpawnEffectEntity(e, 0x39, 0, -1);                          // Ghidra: FUN_0047a9d0
        *(uint*)victim |= 1;

        // Projetee DANS SON CAP, d'un elan proportionnel a ce qui lui reste de poussee.
        int course = *(short*)(e + 0x32);
        int impulse = *(int*)(e + 0x5c) >> 0xd;

        *(int*)(victim + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * impulse;
        *(int*)(victim + 0x54) = 0x18000;
        *(int*)(victim + 0x58) = Winsys3d.GetCosTableEntry(course) * impulse;
    }

    /// <summary>Ghidra: la double condition relue deux fois par le combo du Chef-Goblin — image 8
    /// de l'animation 3, ou image 10 de l'animation 5.</summary>
    private static bool GoblinChiefComboStrikeFrame(byte* e)
    {
        short frame = *(short*)(e + 0x1a);
        return (frame == 8 && *(short*)(e + 0x18) == 3) || (frame == 10 && *(short*)(e + 0x18) == 5);
    }

    /// <summary>
    /// Ghidra: <c>0x0048951c..0x004898a0</c> — <b>le troisieme temps</b>. Les trois plages de
    /// l'Ogre, avec ses deux armes echangees de plage : le BOND au contact, la CHARGE a
    /// mi-distance.
    /// </summary>
    private static void ReplanGoblinChiefAction_0048951C(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int bearing = Winsys3d.Ordinal_457(dx, dz);
        *(short*)(e + 0x12) = 0;
        int heading = (bearing - *(short*)(e + 0x32)) & 0xfff;

        // ================================================================ au contact
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0x64)
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
                        e[0x42] = 0x0c;
                        e[0x43] = 1;
                        *(short*)(e + 0x12) = 0;
                    }
                }
                else
                {
                    HesitateSceneEntity(e);
                }
            }
            else if (roll >= 0x3c && roll < 0x46 && distance > boldness + 0x64)
            {
                // Ghidra: 0x0048962d — LE BOND, la ou l'Ogre chargeait.
                e[0x42] = 6;
                e[0x43] = 1;
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                ApproachGoblinChief_00489874(e, timid);              // Ghidra: LAB_00489874
            }
        }

        // ================================================================ a mi-distance
        if (distance >= 0xfa && distance < 0x258)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    HesitateSceneEntity(e);
                }
                else
                {
                    // Ghidra: LAB_00489697 — LA CHARGE, la ou l'Ogre marchait droit.
                    e[0x42] = 0x0d;
                    e[0x43] = 1;
                    *(short*)(e + 0x12) = 0;
                }
            }
            else if (roll < 0x28)
            {
                ApproachGoblinChief_00489874(e, timid);
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else if (roll < 0x50)
            {
                StrafeGoblinChief_004896E7(e, timid);
            }
            else
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
            }
        }

        // ================================================================ au loin
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            if (far < boldness * 2 + 0x0a)
            {
                // Ghidra: LAB_0048982c — l'approche lointaine s'arrete a 0x64, pas a 0x50.
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x64;
                e[0x43] = SceneEntityDelay(timid);
                return;
            }

            e[0x42] = 0;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + (byte)(timid >> 2));
            *(ushort*)(e + 0x10) = (ushort)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x28)
        {
            StrafeGoblinChief_004896E7(e, timid);
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        if (far < 0x50)
        {
            if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(short*)(e + 0x12) = 0;
                return;
            }

            e[0x42] = 2;
            *(ushort*)(e + 0x10) = 0x64;
            e[0x43] = SceneEntityDelay(timid);
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }

    /// <summary>Ghidra: LAB_00489874 et LAB_004896a5 — <b>approcher</b> : posture 2, arret a 0x50
    /// unites, tenue tiree.</summary>
    private static void ApproachGoblinChief_00489874(byte* e, int timid)
    {
        e[0x42] = 2;
        *(ushort*)(e + 0x10) = 0x50;
        e[0x43] = SceneEntityDelay(timid);
    }

    /// <summary>Ghidra: 0x004896e7 et LAB_00489799 — <b>louvoyer</b> : posture 3, une tenue tiree,
    /// et un cap entierement tire sur douze bits.</summary>
    private static void StrafeGoblinChief_004896E7(byte* e, int timid)
    {
        e[0x42] = 3;
        *(short*)(e + 0x12) = 0;
        e[0x43] = SceneEntityDelay(timid);
        *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
    }
}
