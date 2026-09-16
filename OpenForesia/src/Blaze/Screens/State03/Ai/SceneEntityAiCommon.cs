// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA CHARPENTE D'INTELLIGENCE D'ENTITE DE SCENE, et les feuilles que toutes les betes partagent.
//
// Une routine d'intelligence se lit en trois temps, toujours les memes : DECIDER une action,
// la TENIR le temps de sa posture, puis REPLANIFIER. Ce fichier porte la charpente de reference,
// celle a CINQ postures, et les feuilles qu'elle a introduites : l'attente, le repos, la tenue a
// distance, l'errance, le pivot vers la cible, le tirage du delai.
//
// LA CHARPENTE EST AUSSI UNE BETE. `TickSceneEntityAi_0049A980` n'est pas une abstraction : en
// jeu c'est le GOBLIN, sous-type 2 du bloc A de la foret (zone 3, sous-zones 0 a 4). Elle est
// restee ici parce que les vingt-quatre autres routines s'y adossent - la separer de ses feuilles
// n'aurait rien eclairci. La seconde charpente, a QUATRE postures, est la Fourmi geante
// `Ai/Forest/GiantAnt.cs`.
//
// Le sous-type 8 du meme bloc s'appelle AUSSI « Goblin » et a son propre corps
// (`Ai/Forest/Goblin.cs`) : le nom d'espece ne determine pas le comportement.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_00519c30 @ 0x00519c30 — <b>le balayage du groupe</b> : lequel des membres le
    /// geste en cours touche-t-il ? Elle rend le <b>plus proche</b> de ceux que
    /// <see cref="TestMeleeReach_00535A90"/> accepte, et pose <c>DAT_005de0b8</c> a 1 quand il y en
    /// a un.
    ///
    /// <para>
    /// <b>La parade est ici, pas dans le combat.</b> Si le membre trouve porte le bit 4, que sa
    /// classe (<c>bloc par personnage + 0x150</c>, trois bits bas) vaut 0 ou 6, et qu'il fait face
    /// au coup a moins d'un demi-tour pres, alors il <b>pare</b> : ses statistiques sont refaites,
    /// l'assaillant est note en <c>+0x70</c>, l'evenement d'objet equipe 0x0e part, un son joue —
    /// et <c>DAT_005de0b8</c> est remis a <b>zero</b>, ce qui dit a l'appelant « touche, mais pas
    /// de degats ». C'est la seule difference entre parer et encaisser.
    /// </para>
    /// </summary>
    private static byte* SweepPartyForMeleeHit_00519C30(byte* entity, int* position, short* probe, uint flags)
    {
        byte* found = null;
        int nearest = 0x7fff;

        *Mem.Camera(0x005de0b8) = 0;

        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int n = s_PartySize_005e9020; n != 0; n--, slot += 0x9c)
        {
            if ((*(uint*)slot & 0x50002040) != 0) continue;
            if (*(short*)(slot + 0x12) != 0 || *(short*)(slot + 0x96) != 0) continue;
            if (!TestMeleeReach_00535A90(position, slot, probe, entity)) continue;

            int dx = (*(int*)(slot + 0x1c) - *(int*)(entity + 0x1c)) >> 0xc;
            int dz = (*(int*)(slot + 0x24) - *(int*)(entity + 0x24)) >> 0xc;
            int range = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            if (range < nearest)
            {
                *Mem.Camera(0x005de0b8) = 1;
                found = slot;
                nearest = range;
            }
        }

        if (found == null)
        {
            *Mem.Camera(0x005de0b8) = 0;
            return null;
        }

        if ((flags & 0x800) == 0 && (*found & 4) != 0)
        {
            byte stance = (byte)(GetMainMemPtr()[(found[8] + 0x78) * 0x2000 + 0x150] & 7);

            if ((stance == 0 || stance == 6)
                && (uint)((*(short*)(found + 0x32) - *(short*)((byte*)position + 0x16) - *probe - 0x400) & 0xfff) < 0x800)
            {
                byte index = found[8];
                RebaseStatBlockOnModel_00538AF0(found);
                ScaleStatBlockByDifficulty(s_EntityStatBlocks_005ea560 + index * 0x120);
                FinalizeEquipmentEffects(s_EntityStatBlocks_005ea560 + index * 0x120);

                *(byte**)(found + 0x70) = entity;
                *(uint*)Mem.Arena(0x005e99c0) = 0;
                TriggerEquippedItems_005238E0((uint*)found, 0xe);
                Aud.PlayPositionalSound(8, 0x800, 0x1000, (int*)(found + 0x1c));

                // Le coup a bien porte, mais la parade annule les degats : l'appelant lit ce
                // drapeau et ne va pas jusqu'a la resolution.
                *Mem.Camera(0x005de0b8) = 0;
                return found;
            }
        }

        *Mem.Camera(0x005de0b8) = 1;
        return found;
    }

    /// <summary>
    /// Ghidra: <c>0x0049a980</c> — <b>l'intelligence d'une entite de scene</b> : le gestionnaire du
    /// sous-type 2 de la table installee <c>0x0056ad48</c>, celui que la passe
    /// <see cref="TickEntityTable_005ecaa0"/> appelle a chaque image pour chaque monstre. 2175
    /// octets, <b>absente de <c>Blaze.c</c></b> — Ghidra ne rend que cinq corps sur toute la plage
    /// <c>0x00491290..0x004a1020</c>, et pas celui-ci. Transcrite au desassembleur, releve garde
    /// sous <c>Legacy/Raw/Blaze/FUN_0049A980.asm</c>.
    ///
    /// <para>
    /// <b>La fonction se lit en trois temps</b>, et c'est ce decoupage qui la rend abordable malgre
    /// sa taille : <i>decider</i> ce qu'on fait de l'image en cours, <i>appliquer</i> le mouvement
    /// et l'animation, puis <i>replanifier</i> pour la suite. Les 43 cibles de saut ne sont que les
    /// sorties de la premiere partie, qui convergent toutes sur la deuxieme.
    /// </para>
    ///
    /// <para>
    /// <b>Deux champs portent l'etat</b>, et les confondre rend le tout illisible. <c>+0x42</c> est
    /// la <b>posture</b> : cinq valeurs, aiguillees par une table de sauts a <c>0x0049b200</c>
    /// (relevee dans le binaire) — 0 attendre, 1 se tourner, 2 s'approcher jusqu'a une distance
    /// tenue en <c>+0x10</c>, 3 marcher ou bondir, 4 foncer. <c>+0x43</c> est le <b>compte a
    /// rebours</b> qui la maintient : tant qu'il n'est pas nul, la replanification ne reprend pas
    /// la main.
    /// </para>
    /// <para>
    /// <b>Tout se decide sur deux nombres</b> que <see cref="ReadSceneEntitySituation_00535F20"/>
    /// rend : la <i>hardiesse</i> (0 a 0x40, tiree de l'ecart de niveau et de l'etat de sante) et
    /// son complement a 0x40. La premiere elargit les cones de virage et allonge les pas ; le
    /// second elargit les seuils de tirage. Une bete faible face a un groupe fort hesite, tourne
    /// plus lentement et frappe moins souvent — c'est une seule soustraction qui le produit.
    /// </para>
    /// <para>
    /// <b>Une vitesse nulle veut dire « ne pense pas ».</b> Les trois conditions qui l'annulent
    /// (traits <c>0x1c0</c>, bit <c>0x80</c> de l'entite, entite tenue) court-circuitent la
    /// decision <i>et</i> la replanification, mais pas l'application : l'entite continue d'etre
    /// integree et animee.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049A980(byte* record)
    {
        byte* e = record;

        // Des trois blocs resolus, seul celui de statistiques sert — a la toute fin, en argument
        // de l'interruption. Les deux autres sont ecrits sur la pile et jamais relus.
        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        // La sonde du geste, posee sur la pile avant meme la lecture de situation et relue une
        // seule fois : c'est le gabarit que FUN_00519c30 croise avec chaque membre du groupe.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x50;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        if (DecideSceneEntityAction(e, probe, boldness, distance, heading, speed, traits))
        {
            // Ghidra: LAB_0049ae97 — la poussee est remise a zero sur les seules sorties qui
            // viennent de poser une vitesse propre.
            *(int*)(e + 0x5c) = 0;
        }

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanSceneEntityAction(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049aa0d..0x0049ae96</c> — <b>le premier temps</b> de
    /// <see cref="TickSceneEntityAi_0049A980"/> : ce que l'entite fait de l'image en cours, selon
    /// qu'elle est deja en train d'agir ou qu'elle tient une posture.
    /// </summary>
    /// <returns><c>true</c> si la sortie passe par <c>LAB_0049ae97</c>, qui remet la poussee
    /// <c>+0x5c</c> a zero avant l'application.</returns>
    private static bool DecideSceneEntityAction(
        byte* e, short* probe, int boldness, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return false;

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
                return false;
            }

            byte posture = e[0x42];

            if (posture == 1)
            {
                // A la quatorzieme image du geste, la charge laisse une trainee. Les trois
                // vitesses sont remplacees le temps de la copie puis remises telles quelles :
                // c'est l'EFFET qui part avec la vitesse, l'entite garde la sienne.
                if (*(short*)(e + 0x1a) == 0x0e)
                {
                    int savedX = *(int*)(e + 0x50);
                    int savedY = *(int*)(e + 0x54);
                    int savedZ = *(int*)(e + 0x58);

                    int reach = (CrtRand() & 0x0f) + 0x28;
                    int course = *(short*)(e + 0x32);

                    *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(course) * reach;
                    *(int*)(e + 0x58) = (int)Winsys3d.GetCosTableEntry(course) * reach;
                    *(int*)(e + 0x54) = 0x18000;

                    // Le sous-type de la trainee n'est pas une constante : il est lu dans le
                    // repertoire de zone (+0xcc), premier bloc, deuxieme octet, decale de 0xc0.
                    byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
                    int subtype = (*(byte**)block)[1] + 0xc0;

                    SpawnEffectEntity(e, (byte)subtype, 0xff, -1);

                    *(int*)(e + 0x54) = savedY;
                    *(int*)(e + 0x50) = savedX;
                    *(int*)(e + 0x58) = savedZ;
                }

                // Ghidra: LAB_0049aac8
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
                return false;
            }

            if (posture != 0) return false;

            // Ghidra: LAB_0049aade — la posture 0 pendant un geste, c'est le COUP : a la
            // huitieme image, on regarde qui est sous le gabarit.
            if (*(short*)(e + 0x1a) == 8)
            {
                byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                // Le drapeau vaut 1 quand le coup porte SANS etre pare — la parade le remet a
                // zero elle-meme, en ayant deja joue son propre son.
                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);

                    *(ushort*)(victim + 0x96) = 0x10;      // le clignotement de degat
                    *(uint*)victim |= 0x40;
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                }

                flags = *(uint*)e;

                if ((flags & 1) != 0)
                {
                    *(short*)(e + 0x1a) -= 1;
                    return false;
                }
            }
            else if ((flags & 1) != 0)
            {
                return false;
            }

            // Ghidra: LAB_0049ab47
            if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
            return true;
        }

        // ================================================================ le recul
        // Ghidra: LAB_0049ab5b — le bit 0x10 est pose par l'interruption : l'entite encaisse,
        // et ne fait rien d'autre que se tourner vers ce qui l'a touchee.
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
            return false;
        }

        // ================================================================ la posture tenue
        // Ghidra: LAB_0049abb5
        if ((flags & 0x804) != 0) return false;

        if ((flags & 1) != 0)
        {
            // Le geste vient de finir : une chance sur 100 par quart de hardiesse de relancer.
            if (CrtRand() % 100 >= (boldness >> 2)) return false;

            uint state = *(uint*)e;
            *(int*)(e + 0x18) = 3;
            e[0x42] = 0;
            *(int*)(e + 0x5c) = 0;
            *(uint*)e = (state & 0xfeffffff) | 2;
            return false;
        }

        // Ghidra: LAB_0049ac00 — le repartiteur des cinq postures, table de sauts 0x0049b200.
        byte held = e[0x42];
        if (held > 4) return false;

        switch (held)
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
                    return false;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return false;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                // Le compte a rebours ne repart que lorsque le cap est ATTEINT — c'est ce qui
                // fait qu'une bete desorientee reste tournee vers vous avant de repartir.
                if (TurnSceneEntityTowardTarget(e, boldness * 2 + 0x80, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return false;

            // ---------------------------------------------------------- 2 : s'approcher
            case 2:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                // La distance tenue est dans +0x10, la meme case que la posture 3 remplit d'un
                // cap tire au sort : le champ sert aux deux, jamais en meme temps.
                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return false;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, boldness * 2 + 0x80, heading);
                *(int*)(e + 0x5c) = (boldness + 0xc0) << 7;
                return false;

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

                if (CrtRand() % 100 < (boldness >> 3))
                {
                    // Le bond : la poussee passe par la VITESSE, avec une composante verticale,
                    // et le bit 0 s'allume — l'entite est en l'air.
                    int thrust = (boldness + 0x100) << 7;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    int course = *(short*)(e + 0x10);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x58) += ((int)Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x54) += 0xa000;
                    *(uint*)e |= 1;
                    return true;
                }

                // La marche : la POSITION avance directement, sans passer par la vitesse.
                int walk = (boldness + 0x80) << 7;
                int walkStep = (speed * walk) >> 0xc;
                *(int*)(e + 0x5c) = walk;

                int walkCourse = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(walkCourse) * walkStep) >> 0xc;
                *(int*)(e + 0x24) += ((int)Winsys3d.GetCosTableEntry(walkCourse) * walkStep) >> 0xc;

                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return true;
            }

            // ---------------------------------------------------------- 4 : foncer
            default:
            {
                flags = (flags & 0xfeffffff) | 1;
                *(int*)(e + 0x18) = 6;
                *(uint*)e = flags;

                // La seule posture qui ECRASE la vitesse au lieu de s'y ajouter, et la seule
                // dont le pas se compte sur huit bits plutot que sept.
                int thrust = (boldness + 0xa0) << 8;
                int step = (speed * thrust) >> 0xc;
                *(int*)(e + 0x5c) = thrust;

                int course = *(short*)(e + 0x10);
                *(int*)(e + 0x50) = ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x58) = ((int)Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                *(int*)(e + 0x54) = 0x19000;
                return true;
            }
        }
    }

    /// <summary>
    /// Ghidra: le motif <c>LAB_0049ab78</c> / <c>ac5c</c> / <c>acd6</c> / <c>0049ae07</c> —
    /// <b>tourner vers la cible, au plus de <paramref name="amount"/> par image</b>. Le meme corps
    /// est emis quatre fois par le compilateur, avec quatre largeurs differentes.
    ///
    /// <para>Le test <c>((amount + cap) &amp; 0xfff) &lt;= amount * 2</c> demande « l'ecart tient-il
    /// dans le cone ? » : si oui le cap est pris d'un coup, sinon on avance de la largeur du cone,
    /// dans le sens que donne la moitie du tour ou se trouve la cible.</para>
    /// </summary>
    /// <returns><c>true</c> si le cap a ete pris d'un coup.</returns>
    private static bool TurnSceneEntityTowardTarget(byte* e, int amount, int heading)
    {
        if (((amount + heading) & 0xfff) <= amount * 2)
        {
            *(ushort*)(e + 0x32) += (ushort)heading;
            return true;
        }

        if ((heading & 0xfff) >= 0x800) amount = -amount;
        *(ushort*)(e + 0x32) += (ushort)amount;
        return false;
    }

    /// <summary>
    /// Ghidra: <c>0x0049aeef..0x0049b1fe</c> — <b>le troisieme temps</b> : quelle posture prendre
    /// pour la suite. Elle ne tourne que lorsque le compte a rebours <c>+0x43</c> est epuise et que
    /// l'entite n'est ni en geste ni en recul.
    ///
    /// <para>
    /// <b>La distance et le cap sont RECALCULES ici, et en deux dimensions seulement</b> — la
    /// hauteur est ignoree. Ceux de <see cref="ReadSceneEntitySituation_00535F20"/> etaient a trois
    /// dimensions et sont morts a ce point : c'est deliberé, une bete ne renonce pas a vous
    /// poursuivre parce que vous etes sur une marche.
    /// </para>
    /// <para>
    /// Trois plages de distance, et dans chacune un tirage sur cent : <b>sous 0x0fa</b> le corps a
    /// corps, ou l'entite frappe, esquive de cote, ou recule ; <b>sous 0x1f4</b> l'approche ;
    /// <b>au-dela</b> l'errance, ou elle finit le plus souvent par attendre.
    /// </para>
    /// </summary>
    private static void ReplanSceneEntityAction(byte* e, int boldness, int timid)
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

            if (roll < 0x1e)
            {
                // Frapper : il faut etre dans le cone ET assez pres, la portee tiree au sort
                // s'elargissant avec la timidite — une bete craintive frappe de plus loin.
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

                // Ghidra: LAB_0049afb2 — l'esquive de cote, dans un cone deux fois plus large.
                if (((heading + timid * 4 + 0x200) & 0xfff) > timid * 8 + 0x400
                    || CrtRand() % 100 >= (timid >> 1) + 0x20)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                uint dodge = *(uint*)e;
                *(int*)(e + 0x18) = 5;
                *(uint*)e = (dodge & 0xfeffffff) | 0x10;
                *(int*)(e + 0x5c) = 0;
                e[0x43] = (byte)(CrtRand() % 5 + 10);
                return;
            }

            // Ghidra: LAB_0049b022
            if (roll < 0x3c)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            if (roll < 0x4b && distance > boldness + 100)
            {
                e[0x42] = 4;                                   // foncer
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

            if (roll < (boldness >> 1) + 0x10)
            {
                // Se tourner, mais seulement si la cible est deja dans la moitie avant.
                if (((heading + 0x400) & 0xfff) >= 0x800)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                uint state = *(uint*)e;
                *(int*)(e + 0x18) = 4;
                e[0x42] = 1;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (state & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x46)
            {
                // Le meme errement que plus haut, sans le demi-tour de 0x800 : l'entite part
                // VERS la cible au lieu de s'en ecarter.
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

        // Ghidra: LAB_0049b1d3 — la seule sortie ou le cap est ENTIEREMENT tire au sort, sans
        // reference a la cible : trop loin pour qu'elle compte.
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }

    /// <summary>Ghidra: le motif <c>rand() % 10 + (timide &gt;&gt; 1) + 0x14</c>, emis quatre fois
    /// — <b>la duree d'une posture</b>, de 20 a 61 images selon la timidite.</summary>
    private static byte SceneEntityDelay(int timid) => (byte)(CrtRand() % 10 + (byte)(timid >> 1) + 0x14);

    /// <summary>Ghidra: LAB_0049b1c3 — <b>l'hesitation</b> : posture 1 pour une seule image. C'est
    /// la sortie de secours de toutes les conditions qui echouent.</summary>
    private static void HesitateSceneEntity(byte* e)
    {
        e[0x42] = 1;
        e[0x43] = 1;
    }

    /// <summary>Ghidra: LAB_0049b19b — <b>le repos</b> : posture 0, pour une duree tiree.</summary>
    private static void RestSceneEntity(byte* e, int timid)
    {
        e[0x42] = 0;
        e[0x43] = SceneEntityDelay(timid);
    }

    /// <summary>Ghidra: LAB_0049b160 et LAB_0049a1f9 — <b>tenir la distance</b> : posture 2, avec
    /// la distance voulue posee dans <c>+0x10</c>. Les deux especes ne different que par elle —
    /// 0x64 pour la famille de <see cref="TickSceneEntityAi_0049A980"/>, 0x5a pour celle de
    /// <see cref="TickSceneEntityAi_00499DE0"/>.</summary>
    private static void HoldSceneEntityAtRange(byte* e, int timid, ushort range)
    {
        e[0x42] = 2;
        *(ushort*)(e + 0x10) = range;
        e[0x43] = SceneEntityDelay(timid);
    }

    /// <summary>
    /// Ghidra: LAB_0049b054, LAB_0049b10c et LAB_0049a28a — <b>marcher dans une direction proche de
    /// la cible</b> : posture 3, le cap etant celui de la cible plus un ecart tire dans une
    /// fourchette large de <c>(hardiesse + 0x80) * 16</c>, centree.
    ///
    /// <para>Les trois sites ne different que par <paramref name="bias"/> : <c>0x800</c> est un
    /// demi-tour, donc l'entite s'ELOIGNE — c'est la retraite du corps a corps ; <c>0</c> la fait
    /// avancer.</para>
    /// </summary>
    private static void WanderSceneEntity(byte* e, int boldness, int timid, int heading, int bias)
    {
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);

        int spread = (boldness + 0x80) << 4;
        int offset = CrtRand() % spread - (spread >> 1);

        *(ushort*)(e + 0x10) = (ushort)(offset + heading + *(short*)(e + 0x32) + bias);
    }

    /// <summary>Ghidra: <c>&amp;DAT_00552978</c> — <b>la table des gestes de sort</b> que
    /// <see cref="DecideSceneEntityAction_0049E190"/> tire au sort : trois entrees par espece,
    /// indexees par <c>+0x14</c>, chacune un mot dont l'octet haut est la <b>famille</b> et l'octet
    /// bas l'<b>indice</b> passes a <see cref="CastSpell_0053A980"/>.
    ///
    /// <para>
    /// <b>Sa fin est etablie depuis le 2026-09-01, et elle tombe bien sur ces douze mots.</b> Le
    /// releve initial s'arretait sur un indice — « au-dela on lit des triplets
    /// <c>0x14 / 0x3c / 0x64</c> qui ressemblent a des seuils » —, sans preuve. La transcription du
    /// Trent <see cref="TickSceneEntityAi_0049FAD0"/> l'a fournie : ces trois nombres sont les
    /// termes <c>[1]</c>, <c>[2]</c> et <c>[3]</c> de <see cref="s_TrentProbes_00552990"/>, son
    /// gabarit de portee, qui commence exactement a <c>0x00552990</c>. La table des gestes de sort
    /// tient donc en <b>douze mots exactement</b>, quatre triplets, et ce qui la suit est une sonde.
    /// </para>
    /// <para>Le voisin qui precede, <see cref="s_GrizzlyProbes_00552960"/>, s'arrete bien a
    /// <c>0x00552978</c> : son propre resume le note deja. La table est donc bornee des deux
    /// cotes.</para>
    /// </summary>
    private static readonly ushort[] s_SpellGestures_00552978 =
    [
        0x0000, 0x0003, 0x0008,   // 0x00552978 — espece 0
        0x000a, 0x0010, 0x020c,   // 0x0055297e — espece 1
        0x0003, 0x0008, 0x0200,   // 0x00552984 — espece 2
        0x0008, 0x0200, 0x0101,   // 0x0055298a — espece 3
    ];
}
