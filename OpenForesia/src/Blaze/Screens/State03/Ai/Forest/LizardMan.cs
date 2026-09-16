// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'HOMME-LEZARD — 0x0049D030, sous-type 7 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// Le sous-type 7 du bloc B (0x0049F0C0, « Lezard noir ») entre par le MEME corps : le
// repartiteur l'y renvoie. La plus riche de la famille du bloc A.
//
// La charpente commune (postures, replanification, errance, hesitation) est dans
// `Ai/SceneEntityAiCommon.cs` ; la resolution du coup dans `Combat/HitResolution.cs` ;
// le ciblage dans `Entities/Targeting.cs`.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x0049D030 — L'HOMME-LEZARD, sous-type 7 du bloc A de la foret. Transcrite le 2026-08-29 au
    // desassembleur (Legacy/Raw/Blaze/FUN_0049D030.asm, 2 546 octets de corps, absente de Blaze.c).
    //
    // LA PLUS RICHE DE LA FAMILLE, et la premiere a reunir tout ce que les sept precedentes avaient
    // chacune de leur cote : les CINQ postures de la Fourmi geante (0x0049a980), sa branche de
    // recul, sa relance d'attaque en fin de geste, son esquive de cote — ET les deux gestes du
    // Crabe et du Grizzly. Rien de neuf dans la charpente, tout dans la combinaison.
    //
    // SA CHARGE NE FAIT AUCUN DEGAT. C'est le fait le plus inattendu de la routine, et il est net
    // dans le binaire : FUN_00539b80 n'est appele qu'une seule fois dans les 2 546 octets, depuis le
    // geste de posture 0. Le geste de posture 1 — la course — croise pourtant le gabarit a chaque
    // image, mais tout ce qu'il fait d'une victime touchee c'est la faire clignoter, la projeter a
    // trente unites par image, la decoller du sol et poser l'effet 0x39. Elle BOUSCULE, elle ne
    // blesse pas.
    //
    // LA COURSE TOURNE EN BOUCLE. Des que l'animation depasse sa huitieme image, la routine la
    // REMET a 8 : le cycle de course se rejoue tant que la bete file. Elle ne s'arrete pas sur un
    // compte a rebours ni a l'atterrissage, mais quand sa vitesse horizontale — mesuree au
    // sqrt de FUN_00545a10 sur +0x50 et +0x58 — est retombee sous hardiesse/8 + 1. Une bete hardie
    // court donc moins longtemps, ce qui est l'inverse de l'intuition et se lit tel quel.
    //
    // ET ELLE A DEUX SONDES SUR LA PILE, ce qu'aucune des sept precedentes ne fait : le Grizzly
    // avait deux gabarits, mais dans .data. Ici les douze mots sont ecrits a la main dans le cadre,
    // en deux blocs de six, et les zeros du debut du corps (0x0049d033..0x0049d04b) en font partie.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x0049d030</c> — <b>l'intelligence de l'Homme-lezard</b>, sous-type 7 de la table
    /// installee <c>0x0056ad48</c> en foret. Neuvieme gestionnaire d'entite de scene, huitieme
    /// routine d'intelligence.
    ///
    /// <para>
    /// <b>Cinq postures</b>, table de sauts a <c>0x0049da24</c> (<c>0x0049d42d</c>,
    /// <c>0x0049d466</c>, <c>0x0049d4bb</c>, <c>0x0049d543</c>, <c>0x0049d642</c>) : 0 attendre,
    /// 1 se tourner, 2 s'approcher, 3 marcher ou bondir, 4 foncer. C'est la charpente complete de
    /// <see cref="TickSceneEntityAi_0049A980"/>, branche de recul et relance comprises — la seule
    /// des huit routines ecrites a la reprendre entiere.
    /// </para>
    /// <para>
    /// <b>Deux sondes, toutes deux sur la pile</b> : <c>{0, 0x28, 0x32, 0x64, 0, 0x78}</c> pour le
    /// coup de posture 0 — la plus longue portee de la famille, <c>0x78</c> — et
    /// <c>{0, 0, 0x3c, 0x64, 0, 0x5a}</c> pour la course, plus courte et plus basse.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049D030(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        // Ghidra: les deux gabarits occupent 0x18 octets contigus du cadre, et les quatre `mov
        // [esp+…],ax` du prologue sont les zeros de leurs termes 0, 4 et 1.
        short* strikeProbe = stackalloc short[6];
        strikeProbe[0] = 0;
        strikeProbe[1] = 0x28;
        strikeProbe[2] = 0x32;
        strikeProbe[3] = 0x64;
        strikeProbe[4] = 0;
        strikeProbe[5] = 0x78;

        short* rushProbe = stackalloc short[6];
        rushProbe[0] = 0;
        rushProbe[1] = 0;
        rushProbe[2] = 0x3c;
        rushProbe[3] = 0x64;
        rushProbe[4] = 0;
        rushProbe[5] = 0x5a;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideRushingEntityAction_0049D030(
            e, strikeProbe, rushProbe, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanRushingEntityAction_0049D030(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049d0e7..0x0049d69e</c> — <b>le premier temps</b> de l'Homme-lezard.
    ///
    /// <para>
    /// <b>Deux gestes, lus sur la posture</b> comme chez le Crabe et le Grizzly : posture 0 c'est le
    /// coup — il se replace jusqu'a la treizieme image, avance d'un a-coup a la treizieme, et balaie
    /// les images 14 a 16 ; posture 1 c'est la course, qui bouscule sans blesser.
    /// </para>
    /// <para>
    /// <b>Le bit 0 retient le coup.</b> Si la bete est en l'air quand son geste atteint sa fenetre
    /// de contact, l'image d'animation <b>recule</b> d'un cran au lieu d'avancer : le coup attend
    /// qu'elle ait touche terre. Le meme motif existe chez <see cref="TickSceneEntityAi_0049A980"/>,
    /// et il prend ici tout son sens puisque sa posture 3 peut bondir.
    /// </para>
    /// </summary>
    private static void DecideRushingEntityAction_0049D030(
        byte* e, short* strikeProbe, short* rushProbe,
        int boldness, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            // Le trait 0x10 coupe court : l'espece ne poursuit pas son geste.
            if ((traits & 0x10) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
                return;
            }

            byte posture = e[0x42];

            // ------------------------------------------------------------ 0 : LE COUP
            if (posture == 0)
            {
                // Ghidra: LAB_0049d29e — trois images de contact, sans verrou.
                short frame = *(short*)(e + 0x1a);

                if (frame >= 0x0e && frame <= 0x10)
                {
                    byte* victim =
                        SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), strikeProbe, 0);

                    if (*Mem.Camera(0x005de0b8) != 0)
                    {
                        *(uint*)victim |= 0x40;
                        *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                    }

                    // En l'air, le geste ne progresse pas : l'image recule d'un cran et le coup
                    // sera rejoue a la prochaine.
                    flags = *(uint*)e;
                    if ((flags & 1) != 0)
                    {
                        *(short*)(e + 0x1a) -= 1;
                        return;
                    }
                }
                else if ((flags & 1) != 0)
                {
                    return;
                }

                // Ghidra: LAB_0049d310 — au sol seulement.
                short ground = *(short*)(e + 0x1a);

                if (ground < 0x0d) TurnSceneEntityTowardTarget(e, boldness + 0x20, heading);

                if (ground == 0x0d)
                {
                    // L'a-coup en avant, a la treizieme image : le corps suit la griffe.
                    int lunge = (boldness + 0x280) << 7;
                    *(int*)(e + 0x5c) = lunge;

                    int course = *(short*)(e + 0x32);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * lunge) >> 0xc;
                    *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * lunge) >> 0xc;
                    *(int*)(e + 0x5c) = 0;
                }

                // Ghidra: LAB_0049d39a
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
                return;
            }

            if (posture != 1) return;

            // ------------------------------------------------------------ 1 : LA COURSE
            // La vitesse horizontale mene tout ce bloc : elle decide de la fin de la course et de
            // la capacite a bousculer.
            int vx = *(int*)(e + 0x50) >> 0xc;
            int vz = *(int*)(e + 0x58) >> 0xc;
            int rush = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));

            if (*(short*)(e + 0x1a) > 8)
            {
                // Le cycle de course se rejoue : l'image est ramenee a 8 tant que la bete file.
                *(short*)(e + 0x1a) = 8;

                if (rush <= (boldness >> 3) + 1)
                {
                    *(uint*)e = *(uint*)e & 0xfffffffd;
                    return;
                }
            }

            // Ghidra: LAB_0049d167 — bousculer demande d'etre lance : passe la troisieme image du
            // geste, et au-dessus de six unites de vitesse.
            if (*(short*)(e + 0x1a) > 3 && rush >= 6)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), rushProbe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

                    // ET C'EST TOUT : pas de FUN_00539b80 ici. La course projette et renverse,
                    // elle ne resout aucun coup.
                    int impact = *(short*)(e + 0x32);
                    *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(impact) * 30;
                    *(int*)(prey + 0x58) = (int)Winsys3d.GetCosTableEntry(impact) * 30;

                    uint preyFlags = *(uint*)prey;
                    if ((preyFlags & 1) == 0)
                    {
                        *(int*)(prey + 0x54) = 0x18000;         // et elle decolle
                        *(uint*)prey = preyFlags | 1;
                    }

                    // Ghidra: LAB_0049d200 — l'effet 0x39 au point d'impact, par le meme detour que
                    // le Crabe et le Grizzly.
                    int savedX = *(int*)(e + 0x1c);
                    int savedY = *(int*)(e + 0x20);
                    int savedZ = *(int*)(e + 0x24);

                    *(int*)(e + 0x1c) = *(int*)(prey + 0x1c);
                    *(int*)(e + 0x20) = *(int*)(prey + 0x20);
                    *(int*)(e + 0x24) = *(int*)(prey + 0x24);

                    SpawnEffectEntity(e, 0x39, 0, -1);

                    *(int*)(e + 0x1c) = savedX;
                    *(int*)(e + 0x20) = savedY;
                    *(int*)(e + 0x24) = savedZ;
                }
            }

            // Ghidra: LAB_0049d24d / LAB_0049d251 — le lancement n'a lieu qu'a la troisieme image.
            if (*(short*)(e + 0x1a) != 3)
            {
                *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_0049d698
                return;
            }

            // La plus forte poussee de toutes les routines ecrites — 0x360 contre 0x380 pour la
            // ruee du Crabe, mais versee dans les vitesses SANS composante verticale : il court, il
            // ne bondit pas.
            int drive = (boldness + 0x360) << 7;
            *(int*)(e + 0x5c) = drive;

            int bearing = *(short*)(e + 0x32);
            *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(bearing) * drive) >> 0xc;
            *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(bearing) * drive) >> 0xc;

            *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_0049d698
            return;
        }

        // ================================================================ le recul
        // Ghidra: LAB_0049d3b0 — la branche que le Crabe, le Grizzly et l'Abeille n'ont pas : la
        // bete encaisse et se tourne vers ce qui l'a touchee.
        if ((flags & 0x10) != 0)
        {
            if ((flags & 0x2000000) != 0 && e[0x43] != 0)
            {
                e[0x43]--;
                if (e[0x43] == 0)
                {
                    flags &= 0xffffffef;
                    *(uint*)e = flags;
                }
            }

            TurnSceneEntityTowardTarget(e, boldness + 0x40, heading);
            return;
        }

        // ================================================================ la posture tenue
        if ((flags & 0x804) != 0) return;

        if ((flags & 1) != 0)
        {
            // Ghidra: LAB_0049d3e4 — la relance : une chance sur cent par quart de hardiesse de
            // repartir sur un coup des que le geste precedent est fini.
            if (CrtRand() % 100 >= (boldness >> 2)) return;

            uint state = *(uint*)e;
            *(int*)(e + 0x18) = 3;
            e[0x42] = 0;
            *(uint*)e = (state & 0xfeffffff) | 2;
            return;
        }

        // Ghidra: LAB_0049d418 — cinq postures, table de sauts 0x0049da24.
        byte stance = e[0x42];
        if (stance > 4) return;

        switch (stance)
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x18) != 0)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 0;
                    *(uint*)e = flags;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (TurnSceneEntityTowardTarget(e, boldness * 2 + 0x80, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : s'approcher
            case 2:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, boldness * 2 + 0x80, heading);
                *(int*)(e + 0x5c) = (boldness + 0x100) << 7;
                return;

            // ---------------------------------------------------------- 3 : marcher ou bondir
            case 3:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                // Le bond est deux fois plus rare que chez la Fourmi geante — un seizieme de la
                // hardiesse et non un huitieme — mais il pousse plus fort et monte plus haut.
                if (CrtRand() % 100 < (boldness >> 4))
                {
                    int leap = (boldness + 0x140) << 7;
                    *(int*)(e + 0x5c) = leap;

                    int course = *(short*)(e + 0x10);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * leap) >> 0xc;
                    *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * leap) >> 0xc;
                    *(int*)(e + 0x54) = 0x14000;
                    *(uint*)e |= 1;
                    return;
                }

                // La marche : la POSITION avance directement, sans facteur de vitesse.
                int walk = (boldness + 0xc0) << 7;
                *(int*)(e + 0x5c) = walk;

                int walkCourse = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(walkCourse) * walk) >> 0xc;
                *(int*)(e + 0x24) += ((int)Winsys3d.GetCosTableEntry(walkCourse) * walk) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return;
            }

            // ---------------------------------------------------------- 4 : foncer
            default:
            {
                flags = (flags & 0xfeffffff) | 1;
                *(int*)(e + 0x18) = 5;
                *(uint*)e = flags;

                // La seule posture qui ECRASE la vitesse au lieu de s'y ajouter, et la seule ou le
                // pas passe par la vitesse rendue par la lecture de situation.
                int thrust = (boldness + 0x280) << 7;
                int step = (speed * thrust) >> 0xc;
                *(int*)(e + 0x5c) = thrust;

                int course = *(short*)(e + 0x10);
                *(int*)(e + 0x50) = ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x58) = ((int)Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x54) = 0x1c000;

                *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_0049d698
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0049d6f0..0x0049da19</c> — <b>le troisieme temps</b> de l'Homme-lezard.
    ///
    /// <para>
    /// <b>C'est la replanification de <see cref="TickSceneEntityAi_0049A980"/> au complet</b>,
    /// esquive de cote comprise, avec quatre nombres changes et une sortie de plus :
    /// </para>
    /// <list type="bullet">
    /// <item>il tente son coup dans la moitie des tirages au corps a corps, et non dans trois
    /// dixiemes ;</item>
    /// <item>sa portee tiree part de <b><c>0xc8</c></b> et non de 100 — c'est la plus longue allonge
    /// de la famille, et elle va avec sa sonde a <c>0x78</c> ;</item>
    /// <item>l'esquive envoie l'animation 4 au lieu de 5 ;</item>
    /// <item>entre <c>0x46</c> et <c>0x59</c>, une sortie que la Fourmi geante n'a pas :
    /// <b>la course</b>, armee si la cible est dans le cone.</item>
    /// </list>
    /// </summary>
    private static void ReplanRushingEntityAction_0049D030(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        // ================================================================ le corps a corps
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0xc8)
                {
                    uint state = *(uint*)e;
                    *(int*)(e + 0x18) = 3;
                    e[0x42] = 0;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (state & 0xfeffffff) | 2;
                    return;
                }

                // Ghidra: LAB_0049d7ba — l'esquive de cote, dans un cone deux fois plus large.
                if (((heading + timid * 4 + 0x200) & 0xfff) > timid * 8 + 0x400
                    || CrtRand() % 100 >= (timid >> 1) + 0x20)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                uint dodge = *(uint*)e;
                *(int*)(e + 0x18) = 4;
                *(uint*)e = (dodge & 0xfeffffff) | 0x10;
                *(int*)(e + 0x5c) = 0;
                e[0x43] = (byte)(CrtRand() % 5 + 10);
                return;
            }

            // Ghidra: LAB_0049d82a — foncer, mais seulement s'il reste de la place devant.
            if (roll < 0x46 && distance > boldness + 0x64)
            {
                e[0x42] = 4;
                e[0x43] = 1;
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
                return;
            }

            // Ghidra: LAB_0049d857 — LA COURSE, la sortie que la Fourmi geante n'a pas.
            if (roll < 0x5a && ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200)
            {
                uint state = *(uint*)e;
                *(int*)(e + 0x18) = 4;
                e[0x42] = 1;
                *(uint*)e = (state & 0xfeffffff) | 2;
                return;
            }

            WanderSceneEntity(e, boldness, timid, heading, 0x800);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x46)
            {
                WanderSceneEntity(e, boldness, timid, heading, 0);
                return;
            }

            if (roll < 0x5a)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            RestSceneEntity(e, timid);
            return;
        }

        // ================================================================ l'errance
        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            RestSceneEntity(e, timid);
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        // Ghidra: LAB_0049d9f2 — trop loin pour que la cible compte : le cap est entierement tire.
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }
}
