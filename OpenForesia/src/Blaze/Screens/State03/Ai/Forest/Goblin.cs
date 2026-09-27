// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GOBLIN — 0x0049DA40, sous-type 8 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// La PREMIERE BETE QUI ATTAQUE DE LOIN. A ne pas confondre avec le Goblin du sous-type 2
// (0x0049A980), qui est un autre corps - il vit dans `SceneEntityAiCommon.cs`.
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
    // 0x0049DA40 — LE GOBLIN, sous-type 8 du bloc A de la foret. Transcrite le 2026-08-29 au
    // desassembleur (Legacy/Raw/Blaze/FUN_0049DA40.asm, 1 841 octets de corps, absente de Blaze.c).
    //
    // C'EST LA PREMIERE BETE QUI ATTAQUE DE LOIN. Ses deux gestes ne se ressemblent en rien :
    //
    //   - posture 0 en geste, LE COUP : un seul balayage du gabarit, a la neuvieme image.
    //   - posture 1 en geste, LE JET : aucun balayage. A la septieme image elle fait naitre
    //     l'effet 0x49 et n'en fait plus rien. C'est le projectile qui portera le coup, pas elle.
    //
    // Et sa replanification le lui fait lancer depuis les DEUX plages lointaines — l'approche, mais
    // aussi l'errance jusqu'a 0x2bc, ou aucune des huit routines precedentes ne fait autre chose que
    // marcher. Un goblin vous vise de l'autre bout de la clairiere.
    //
    // LA CHANCE DE VISER SE PREND SUR LA HARDIESSE ET NON SUR UN SEUIL FIXE : hardiesse / 2 + 8
    // sur cent, aux deux plages. C'est le seul tirage de la famille dont le seuil bouge avec l'etat
    // de la bete plutot qu'avec sa timidite.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x0049da40</c> — <b>l'intelligence du Goblin</b>, sous-type 8 de la table
    /// installee <c>0x0056ad48</c> en foret. Dixieme gestionnaire d'entite de scene, neuvieme
    /// routine d'intelligence.
    ///
    /// <para>
    /// <b>Cinq postures</b>, table de sauts a <c>0x0049e174</c> (<c>0x0049dc14</c>,
    /// <c>0x0049dc4c</c>, <c>0x0049dc8c</c>, <c>0x0049dd14</c>, <c>0x0049ddb5</c>) : 0 attendre,
    /// 1 se tourner, 2 s'approcher, 3 marcher, 4 foncer. Comme la Fourmi geante et l'Homme-lezard,
    /// mais <b>sans branche de recul</b> : le bit <c>0x10</c> la renvoie directement a
    /// l'application.
    /// </para>
    /// <para>
    /// Sa sonde est <c>{0, 0x1e, 0x32, 0x64, 0, 0x46}</c> — la plus courte portee de la famille
    /// apres celle du Poisson volant. Elle va avec une bete qui prefere lancer.
    /// </para>
    /// <para>
    /// <b>Sa marche et son « foncer » tiennent compte de la vitesse</b> rendue par la lecture de
    /// situation, la ou le Crabe, le Grizzly et l'Homme-lezard l'ignorent dans leur marche. Le
    /// facteur est bien dans les deux corps.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049DA40(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x46;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideThrowingEntityAction_0049DA40(e, probe, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanThrowingEntityAction_0049DA40(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049dacd..0x0049de1b</c> — <b>le premier temps</b> du Goblin.
    ///
    /// <para>
    /// <b>Le jet ne touche personne.</b> La posture 1 en geste ne croise aucun gabarit : elle fait
    /// naitre l'effet <c>0x49</c> a la septieme image et rend la main. Tout ce qui suit — la
    /// trajectoire, la portee, le coup — appartient au projectile.
    /// </para>
    /// <para>
    /// <b>Le bit 0 retient le coup</b>, comme chez <see cref="TickSceneEntityAi_0049D030"/> : en
    /// l'air, l'image d'animation recule d'un cran au lieu d'avancer. Le jet, lui, n'a pas ce
    /// garde.
    /// </para>
    /// </summary>
    private static void DecideThrowingEntityAction_0049DA40(
        byte* e, short* probe, int boldness, int distance, int heading, int speed, uint traits)
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
                if (*(short*)(e + 0x1a) == 9)
                {
                    byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                    if (*Mem.Camera(0x005de0b8) != 0)
                    {
                        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                        *(uint*)victim |= 0x40;
                        *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                    }

                    // En l'air, le geste ne progresse pas : l'image recule et le coup sera rejoue.
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

                // Ghidra: LAB_0049db9b
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;

                *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_0049de19
                return;
            }

            if (posture != 1) return;

            // ------------------------------------------------------------ 1 : LE JET
            // Ghidra: 0x0049db0a — l'effet 0x49 nait a la septieme image, et c'est tout ce que la
            // bete fait de son geste. Aucun gabarit n'est croise ici.
            if (*(short*)(e + 0x1a) == 7)
            {
                SpawnEffectEntity(e, 0x49, 0, -1);
            }

            flags = *(uint*)e;
            if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
            return;
        }

        // ================================================================ la posture tenue
        // Ghidra: LAB_0049dbaf — pas de branche de recul : le bit 0x10 renvoie a l'application.
        if ((flags & 0x10) != 0) return;
        if ((flags & 0x804) != 0) return;

        if ((flags & 1) != 0)
        {
            // La relance : une chance sur cent par quart de hardiesse de repartir sur un coup.
            if (CrtRand() % 100 >= (boldness >> 2)) return;

            uint state = *(uint*)e;
            *(int*)(e + 0x18) = 3;
            e[0x42] = 0;
            *(uint*)e = (state & 0xfeffffff) | 2;
            *(int*)(e + 0x5c) = 0;                              // Ghidra: LAB_0049de19
            return;
        }

        // Ghidra: LAB_0049dbff — cinq postures, table de sauts 0x0049e174.
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
                *(int*)(e + 0x5c) = (boldness + 0xc0) << 7;
                return;

            // ---------------------------------------------------------- 3 : marcher
            case 3:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                int walk = (boldness + 0x80) << 7;
                int step = (speed * walk) >> 0xc;
                *(int*)(e + 0x5c) = walk;

                int course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x24) += ((int)Winsys3d.GetCosTableEntry(course) * step) >> 0xc;

                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_0049de19
                return;
            }

            // ---------------------------------------------------------- 4 : foncer
            default:
            {
                flags = (flags & 0xfeffffff) | 1;
                *(int*)(e + 0x18) = 4;
                *(uint*)e = flags;

                // Elle S'AJOUTE a sa vitesse au lieu de l'ecraser, contrairement au « foncer » de
                // la Fourmi geante et de l'Homme-lezard, et sa detente verticale s'ajoute aussi.
                int thrust = (boldness + 0xc0) << 7;
                int step = (speed * thrust) >> 0xc;
                *(int*)(e + 0x5c) = thrust;

                int course = *(short*)(e + 0x10);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x54) += 0x19000;

                *(int*)(e + 0x5c) = 0;                          // Ghidra: LAB_0049de19
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0049de6d..0x0049e168</c> — <b>le troisieme temps</b> du Goblin.
    ///
    /// <para>
    /// <b>Le jet a deux portes, et ce sont les deux plages lointaines.</b> A l'approche comme dans
    /// l'errance, un tirage de <c>hardiesse / 2 + 8</c> sur cent l'arme — c'est le seul seuil de la
    /// famille qui monte avec la hardiesse au lieu de descendre avec la timidite. De loin il faut
    /// en plus etre a moins de <c>0x2bc</c> et dans le cone ; a l'approche, il suffit d'avoir la
    /// cible dans la moitie avant.
    /// </para>
    /// <para>
    /// Le reste est la replanification ordinaire des terrestres, avec le corps a corps de
    /// <see cref="TickSceneEntityAi_0049A980"/> mais <b>sans son esquive de cote</b> : une attaque
    /// refusee mene droit a l'hesitation.
    /// </para>
    /// </summary>
    private static void ReplanThrowingEntityAction_0049DA40(byte* e, int boldness, int timid)
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

            if (roll < 0x2d)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 100)
                {
                    uint state = *(uint*)e;
                    *(int*)(e + 0x18) = 3;
                    e[0x42] = 0;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (state & 0xfeffffff) | 2;
                    return;
                }

                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x3c)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            if (roll < 0x4b && distance > boldness + 0x64)
            {
                e[0x42] = 4;                                    // foncer
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
                return;
            }

            WanderSceneEntity(e, boldness, timid, heading, 0x800);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < (boldness >> 1) + 8)
            {
                // On ne lance pas sur ce qu'on a dans le dos.
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                ThrowAt(e);
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

        if (far < (boldness >> 1) + 8)
        {
            // Ghidra: LAB_0049e0b4 — de loin, le jet demande une portee ET un cone.
            if (distance >= 0x2bc
                || ((heading + timid * 4 + 0x100) & 0xfff) > timid * 8 + 0x200)
            {
                HesitateSceneEntity(e);
                return;
            }

            ThrowAt(e);
            return;
        }

        if (far < 0x3c)
        {
            RestSceneEntity(e, timid);
            return;
        }

        // Ghidra: LAB_0049e141 — trop loin pour viser : le cap est entierement tire.
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }

    /// <summary>Ghidra: LAB_0049e0eb et son entree de LAB_0049dfff — <b>armer le jet</b> : le
    /// drapeau de geste, l'animation 3 et la posture 1, qui fera naitre le projectile a la septieme
    /// image. Aucun compte a rebours : c'est la fin de l'animation qui rend la main.</summary>
    private static void ThrowAt(byte* e)
    {
        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
        *(int*)(e + 0x18) = 3;
        e[0x42] = 1;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047aa70 @ 0x0047aa70 — la passe sur la table <c>DAT_005ff400</c>, 0x20
    /// enregistrements. Elle ne raccroche <b>rien</b> a la grille spatiale : c'est la seule des six
    /// dans ce cas, donc ces entites ne sont pas cherchees par proximite.
    /// </summary>
    private static void TickEntityTable_005ff400()
    {
        byte* record = Mem.Entity(0x005ff400);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0)
            {
                CallIndexedEntityHandler(
                    0x00551c30, 0x0056adc8, record[5], record,
                    $"FUN_0047aa70, table 0x005ff400 entree {i}");
            }

            // Le mot d'etat est RELU : le gestionnaire a pu eteindre l'entite lui-meme.
            if ((*(uint*)record & 0x80000000) == 0)
            {
                FreeEntitySlot(Mem.Entity(0x005fa5a0), record[8]);
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0047ab70 @ 0x0047ab70 — la passe sur la table <c>DAT_005edee0</c>, 0x80
    /// enregistrements.
    /// </summary>
    private static void TickEntityTable_005edee0()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005edee0);

        for (int i = 0; i < 0x80; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0)
            {
                record[0x36] &= 0x7f;
                CallIndexedEntityHandler(
                    0x00551f30, 0x0056aec8, record[5], record,
                    $"FUN_0047ab70, table 0x005edee0 entree {i}");
            }

            if ((*(uint*)record & 0x80000000) == 0)
            {
                FreeEntitySlot(Mem.Entity(0x006007a0), record[8]);
            }
            else
            {
                LinkIntoWorldGrid(mainMem, 0x1c5000, record);
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0047acf0 @ 0x0047acf0 — la passe sur la table <c>DAT_005f2d40</c>, 0x80
    /// enregistrements. Copie conforme de <see cref="TickEntityTable_005edee0"/> a trois adresses
    /// pres — la table, son pool et sa table de sauts. Les deux sont ecrites separement plutot que
    /// factorisees : ce sont deux fonctions distinctes du binaire, et l'oracle compare des corps.
    /// </summary>
    private static void TickEntityTable_005f2d40()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005f2d40);

        for (int i = 0; i < 0x80; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0)
            {
                record[0x36] &= 0x7f;
                CallIndexedEntityHandler(
                    0x00552230, 0x0056afc8, record[5], record,
                    $"FUN_0047acf0, table 0x005f2d40 entree {i}");
            }

            if ((*(uint*)record & 0x80000000) == 0)
            {
                FreeEntitySlot(Mem.Entity(0x005f7ce0), record[8]);
            }
            else
            {
                LinkIntoWorldGrid(mainMem, 0x1c5000, record);
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050ed70 @ 0x0050ed70 — <b>la rencontre aleatoire</b> : de temps en temps, une
    /// formation d'ennemis apparait autour d'un membre du groupe tire au sort.
    ///
    /// <para>
    /// Le delai entre deux tentatives est <c>DAT_00564540</c>, recharge a <c>(rand()%6 + 10) * 20</c>
    /// par <see cref="TickEntityTable_005ecaa0"/> — soit 200 a 300 images, trois a cinq secondes.
    /// La passe ne fait rien tant qu'il n'est pas retombe a zero.
    /// </para>
    /// <para>
    /// <b>Le placement est teste avant d'etre commis, et c'est tout ou rien.</b> Chaque
    /// enregistrement de 0x20 octets de la formation est pose a 400 unites du groupe dans une
    /// direction tiree au sort, passe a <see cref="FinalizeEntityPlacement2"/>, et compte comme
    /// valide si les drapeaux de vue rendus n'ont aucun bit hors de <c>0x201</c>. La formation
    /// n'est reellement engendree — <see cref="SpawnEntityFormation"/> puis un son — que si
    /// <b>tous</b> les enregistrements ont passe le test.
    /// </para>
    /// <para>
    /// <b>Les trois appels successifs a <see cref="FinalizeEntityPlacement2"/> sont transcrits tels
    /// quels.</b> La pseudo-C n'ecrit rien entre eux, donc les trois partent du meme point et les
    /// trois drapeaux OU-es devraient etre identiques. C'est peut-etre une marche du placement que
    /// Ghidra a rendue invisible ; en l'absence de preuve au desassembleur, rien n'est simplifie
    /// ici. TODO(REVERSE) : verifier ces trois appels sur le binaire.
    /// </para>
    /// </summary>
    private static void TrySpawnRandomEncounter()
    {
        if (LegacyPlus.Coutures.RencontreEnAttente(s_Unknown_00564540 != 0))   // Couture legacy ++ (LP-007)
        {
            s_Unknown_00564540--;
            return;
        }

        if (s_Unknown_005de544 == s_PartySize_005e9020) return;
        if (*(sbyte*)(s_Unknown_005f2ce0 + 0x10) == -1) return;
        if (LegacyPlus.Coutures.RencontreBloquee(*Mem.Entity(0x005f2d00) != 0)) return;   // Couture legacy ++ (LP-007)

        int member = CrtRand() % s_PartySize_005e9020;
        uint* slot = (uint*)(Inp.s_SlotFlags_005e93c0 + member * 0x9c);

        if ((*slot & 0x80000000) == 0) return;
        if ((*slot & 0x50000000) != 0) return;
        if ((*((byte*)slot + 0x90) & 2) != 0) return;

        // La table des formations de la sous-zone courante, puis une de ses huit entrees.
        short* formations = *(short**)(*(byte**)(s_Unknown_005e97cc + 0xb8) + s_Unknown_005f2ce0[0x10] * 4);
        if (formations == null) return;

        uint roll = (uint)CrtRand();
        int* formation = *(int**)(*(byte**)(s_Unknown_005e97cc + 0x34) + formations[roll & 7] * 4);
        if (formation == null) return;

        if ((*slot & 0x80000000) == 0 || (*slot & 0x40000000) != 0) return;

        byte* scratch = Mem.Arena(0x005e9a40);
        int* record = formation;
        int placed = 0;
        int tried = 0;

        for (int kind = *record; kind != -1; kind = *record)
        {
            // L'entite d'essai est une copie du membre tire au sort, deplacee de 400 unites dans
            // une direction aleatoire. Le Y a 0x7fffffff force la recherche de sol a partir du
            // haut.
            Mem.CopyDwords(slot, (uint*)scratch, 0x27);
            *(int*)(scratch + 0x1c) = *Mem.Camera(0x005de068);
            *(int*)(scratch + 0x20) = 0x7fffffff;
            *(int*)(scratch + 0x24) = *Mem.Camera(0x005de070);

            int angle = CrtRand();
            *(int*)(scratch + 0x1c) += (int)Winsys3d.GetSinTableEntry(angle) * 400;
            *(int*)(scratch + 0x24) += Winsys3d.GetCosTableEntry(angle) * 400;

            FinalizeEntityPlacement2((uint*)scratch);
            uint clip1 = *(uint*)(scratch + 0x90);
            FinalizeEntityPlacement2((uint*)scratch);
            uint clip2 = *(uint*)(scratch + 0x90);
            FinalizeEntityPlacement2((uint*)scratch);

            if (((clip1 | clip2 | *(uint*)(scratch + 0x90)) & 0xfffffdfe) == 0)
            {
                int dx = *Mem.Camera(0x005de068) - *(int*)(scratch + 0x1c);
                int dz = *Mem.Camera(0x005de070) - *(int*)(scratch + 0x24);

                *(short*)((byte*)record + 0x0a) = *(short*)(scratch + 0x30);
                *(short*)((byte*)record + 0x0c) = (short)Winsys3d.Ordinal_457(dx >> 0xc, dz >> 0xc);
                *(short*)((byte*)record + 0x0e) = *(short*)(scratch + 0x34);
                *(short*)((byte*)record + 0x04) = (short)(*(int*)(scratch + 0x1c) >> 0xc);
                *(short*)((byte*)record + 0x06) = (short)(*(int*)(scratch + 0x60) >> 0xc);
                *(short*)((byte*)record + 0x08) = (short)(*(int*)(scratch + 0x24) >> 0xc);

                // Les quatre distances de bord : chacune est gardee par le SIGNE de la suivante,
                // et un signe negatif la sature a 0xffff.
                *(short*)((byte*)record + 0x10) = (*(short*)(scratch + 0x82) & 0x8000) != 0
                    ? unchecked((short)0xffff) : *(short*)(scratch + 0x80);
                *(short*)((byte*)record + 0x12) = (*(short*)(scratch + 0x86) & 0x8000) != 0
                    ? unchecked((short)0xffff) : *(short*)(scratch + 0x84);
                *(short*)((byte*)record + 0x14) = (*(short*)(scratch + 0x8a) & 0x8000) != 0
                    ? unchecked((short)0xffff) : *(short*)(scratch + 0x88);
                *(short*)((byte*)record + 0x16) = (*(short*)(scratch + 0x8e) & 0x8000) != 0
                    ? unchecked((short)0xffff) : *(short*)(scratch + 0x8c);

                placed++;
            }

            record += 8;      // 0x20 octets
            tried++;
        }

        if (tried == placed)
        {
            SpawnEntityFormation(s_Unknown_005e97cc, formation, 200, 0);
            Aud.PlayPositionalSound(0x1b, 0x800, 0x2000, Mem.Camera(0x005de068));
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050f0d0 @ 0x0050f0d0 — les 0x40 <b>effets ephemeres</b> de <c>DAT_005e9b60</c>,
    /// 0x28 octets chacun. Chaque effet a une duree de vie d'un octet a <c>+0x27</c>, posee a 0x14
    /// par <c>FUN_0050f070</c>.
    ///
    /// <para>
    /// Tant qu'elle dure, la hauteur de l'effet (<c>+0x04</c>) est deplacee de
    /// <c>(vie - 8) * 0x2000</c> : positif au debut, negatif ensuite — l'effet monte puis
    /// redescend, en passant par l'immobilite exactement a mi-vie. A zero, l'emplacement retourne
    /// au pool <c>DAT_005e99e0</c>.
    /// </para>
    /// </summary>
    private static void TickEffectSlots_005e9b60()
    {
        byte* entry = Mem.Arena(0x005e9b60);

        for (int i = 0; i < 0x40; i++, entry += 0x28)
        {
            if (entry[0x27] == 0) continue;

            byte life = (byte)(entry[0x27] - 1);
            entry[0x27] = life;

            if (life == 0)
            {
                FreeEntitySlot(Mem.Arena(0x005e99e0), entry[0x25]);
            }
            else
            {
                *(int*)(entry + 0x04) += (entry[0x27] - 8) * 0x2000;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00534f90 @ 0x00534f90 — <b>la remise a l'etat neuf des morceaux consommes</b>.
    /// Elle parcourt la meme liste d'attente que le prologue (<c>DAT_005fa560</c>, premier octet =
    /// compte), et pour chaque liste d'enregistrements de 0x10 octets qu'elle designe, elle
    /// retravaille le premier octet de chaque enregistrement jusqu'au terminateur <c>-1</c>.
    ///
    /// <para>
    /// La regle est celle-ci : si les trois bits bas sont nuls, ou s'ils different des trois
    /// suivants, seul le bit 7 survit ; sinon l'octet est efface entierement. Autrement dit
    /// « garder la marque de visite sauf quand les deux champs de trois bits concordent ».
    /// </para>
    /// <para>
    /// Le prologue de <see cref="RunWorldMapTick"/> parcourt exactement la meme structure pour la
    /// mettre a zero. Les deux passes coexistent dans l'original et ne sont pas fusionnees ici.
    /// </para>
    /// </summary>
    private static void ResetPendingChunkRecords()
    {
        byte* pending = Mem.Entity(0x005fa560);
        if (*pending == 0) return;

        for (int i = 1; i <= *pending; i++)
        {
            uint* record = *(uint**)(*(byte**)(s_Unknown_005e97cc + 0x88) + pending[i] * 4);
            if (record == null) return;

            for (uint value = *record; value != 0xffffffff; value = *record)
            {
                if ((value & 7) == 0 || (value >> 3 & 7) != (value & 7))
                {
                    *(byte*)record = (byte)(*(byte*)record & 0x80);
                }
                else
                {
                    *(byte*)record = 0;
                }

                record += 4;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_005036e0 @ 0x005036e0 — <b>le pas de la camera de scenette</b>, celle que
    /// l'opcode 29 installe (voir <c>BeginScriptedCamera</c>). Le script vit a
    /// <c>DAT_005719b4</c> et avance d'un verbe par appel, jusqu'a ce qu'un verbe rende la main.
    ///
    /// <para>
    /// Avant cela vient <b>l'echappement du joueur</b> : si l'entite qui porte la scenette est dans
    /// l'etat <c>0x800000</c>, que sa manette est branchee et que le drapeau de scenette est
    /// retombe, le script d'interruption est lance. S'il n'a pas lui-meme change le script en
    /// cours — le test <c>pbVar1 == DAT_005719b4</c>, qui compare l'avant et l'apres — la scenette
    /// est coupee nette.
    /// </para>
    /// <para>
    /// Vient ensuite <b>la boucle de verbes</b> : elle enchaine les verbes de
    /// <c>PTR_LAB_0055eb68</c> <b>dans le meme appel</b> tant que l'un d'eux ne pose pas
    /// <c>continuer</c> a zero. Un pas de scenette n'est donc pas « un verbe par image » mais
    /// « tout ce qui se fait sans attendre, puis on rend la main ». Voir <c>RunCutsceneVerb</c>.
    /// </para>
    /// </summary>
    private static void TickCutsceneCamera()
    {
        byte* script = *(byte**)Mem.EntityStateBytes(0x005719b4);
        byte* cursor = script;
        int carryOn = 1;

        if (script != null)
        {
            uint* owner = *(uint**)Mem.EntityStateBytes(0x005719c4);
            if (owner != null)
            {
                if ((*owner & 0xfc0000) == 0x800000)
                {
                    byte padSlot = Inp.s_PlayerSlots_005e9188[(byte)owner[2] * 0x4c];

                    if ((Inp.s_InputStateBase_00604a20[padSlot * 0x10 + 3] & 0x80) != 0
                        && (Saves.s_CutsceneActive_00604b78 & 1) == 0)
                    {
                        RunEntityScript(
                            s_Unknown_005e97cc,
                            (byte*)owner,
                            *(ushort*)Mem.EntityStateBytes(0x005719d8));

                        cursor = *(byte**)Mem.EntityStateBytes(0x005719b4);

                        if (script == cursor)
                        {
                            // Le script d'interruption n'a pas repris la main : la scenette
                            // s'arrete ici, et le pointeur reste nul.
                            *(byte**)Mem.EntityStateBytes(0x005719b4) = null;
                            return;
                        }
                    }
                }
                else
                {
                    *(uint**)Mem.EntityStateBytes(0x005719c4) = null;
                }
            }

            while (carryOn != 0) cursor = RunCutsceneVerb(cursor, ref carryOn);
        }

        *(byte**)Mem.EntityStateBytes(0x005719b4) = cursor;
    }
}
