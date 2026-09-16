// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA COCCINELLE — 0x0049A360, sous-type 1 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// La PREMIERE BETE VOLANTE du depot. Son vol, `BeatWings_0049A68F`, est partage au mot
// pres avec l'Abeille tueuse du bloc C (`KillerBee.cs`).
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
    // 0x0049A360 — LA COCCINELLE, sous-type 1 du bloc A de la foret. Transcrite le 2026-08-29 au
    // desassembleur (Legacy/Raw/Blaze/FUN_0049A360.asm, 1392 octets de corps ; la table de sauts a
    // quatre entrees suit a 0x0049a8d0, et 0x0049a8e0 est deja la fonction suivante).
    //
    // C'EST LA PREMIERE BETE VOLANTE ECRITE, et cela change trois choses par rapport aux deux
    // routines terrestres deja transcrites :
    //
    //   1. elle integre son mouvement par FUN_0049a8e0 et non FUN_00488160 — meme amortissement,
    //      mais SANS le rappel de +0x9a qui plaque une bete au sol ;
    //   2. elle n'appelle pas la fin de geste (FUN_004dfaf0) : a la place, un bloc de VOL fait
    //      osciller sa hauteur autour d'un plafond, par le sinus d'une phase que +0x14 accumule ;
    //   3. son interruption est FUN_004a1cf0 et non FUN_004e3770 — deux masques differents.
    //
    // Sa sonde le dit aussi : probe[4] vaut -0x100 la ou les deux terrestres ont 0, donc son gabarit
    // de frappe est decale en hauteur.
    // =============================================================================================

    /// <summary>
    /// Ghidra: FUN_0049a8e0 @ 0x0049a8e0 — <b>l'integration du mouvement d'une entite volante</b>,
    /// la jumelle de <see cref="IntegrateSceneEntityMotion_00488160"/> que la documentation de
    /// celle-ci annoncait deja.
    ///
    /// <para>
    /// <b>Meme piege de signature</b> : <c>Blaze.c</c> la declare a un parametre puis lit
    /// <c>unaff_retaddr</c> et <c>unaff_EBP</c>. Le desassemblage donne deux parametres — l'entite
    /// et l'echelle de vitesse — et <c>unaff_EBP</c> n'est que le sinus dont Ghidra a jete la valeur
    /// de retour.
    /// </para>
    /// <para>
    /// <b>La difference avec sa jumelle tient en un terme absent</b> : la composante verticale
    /// n'est qu'amortie, sans le rappel de <c>+0x9a</c>. Rien ne ramene la bete au sol — c'est le
    /// bloc de vol de <see cref="TickSceneEntityAi_0049A360"/> qui tient sa hauteur.
    /// </para>
    /// </summary>
    private static void IntegrateFlyingEntityMotion_0049A8E0(byte* e, int speed)
    {
        short heading = *(short*)(e + 0x32);

        int sin = (int)Winsys3d.GetSinTableEntry(heading);
        int cos = Winsys3d.GetCosTableEntry(heading);

        int vx = *(int*)(e + 0x50);
        vx = vx - (vx >> 3) - (vx >> 8);
        *(int*)(e + 0x50) = vx;

        int vy = *(int*)(e + 0x54);
        vy = vy - (vy >> 8) - (vy >> 3);

        int vz = *(int*)(e + 0x58);
        int push = *(int*)(e + 0x5c) * speed >> 0xc;

        vz = vz - (vz >> 3) - (vz >> 8);
        *(int*)(e + 0x58) = vz;

        *(int*)(e + 0x1c) += (push * sin >> 0xc) + vx;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += (push * cos >> 0xc) + vz;
    }

    /// <summary>
    /// Ghidra: FUN_004a1cf0 @ 0x004a1cf0 — <b>la remise a l'attente sur coup recu</b> des betes
    /// volantes, la sœur de <see cref="InterruptSceneEntity_004E3770"/>.
    ///
    /// <para>
    /// Deux masques les separent, et rien d'autre : <c>0x8882</c> au lieu de <c>0x2000a882</c> sur
    /// les drapeaux de vue <c>+0x90</c>, et <c>0x816</c> au lieu de <c>0x817</c> sur les drapeaux
    /// d'entite — le bit 1, que cette routine pose elle-meme a chaque image, est donc exclu du
    /// garde.
    /// </para>
    /// </summary>
    private static void InterruptFlyingEntity_004A1CF0(uint* entity, byte* stats)
    {
        byte* e = (byte*)entity;

        if ((entity[0x24] & 0x8882) != 0)
        {
            if ((*entity & 0x816) == 0)
            {
                e[0x42] = 1;
                e[0x43] = 1;
            }

            entity[0x17] = 0;
        }

        if (*(short*)(e + 0x96) != 0
            && (*(uint*)(stats + 0x14) & 0x80000000) == 0
            && (*entity & 0x816) == 0)
        {
            if ((*entity & 0x40) != 0)
            {
                e[0x43] = 0;
                return;
            }

            e[0x42] = 0;
            e[0x43] = 1;
            entity[0x17] = 0;
        }
    }

    /// <summary>Ghidra: <c>0x0049a4fb</c> et ses trois jumeaux — <b>l'amortissement de la
    /// poussee</b>, un seizieme par image. La ou les deux betes terrestres remettent
    /// <c>+0x5c</c> a zero d'un coup, la Coccinelle ralentit : elle vole, elle ne pile pas.</summary>
    private static void DampFlyingEntityPush(byte* e)
    {
        int push = *(int*)(e + 0x5c);
        *(int*)(e + 0x5c) = push - (push >> 4);
    }

    /// <summary>Ghidra: <c>0x0049a469</c> et ses jumeaux — <b>la montee de la poussee</b> vers une
    /// consigne, par seiziemes. Trois consignes selon la posture : <c>+0x280</c> en frappant,
    /// <c>+0x1a0</c> en approchant, <c>+0x140</c> en errant, chacune decalee de la hardiesse et
    /// multipliee par 0x80.</summary>
    private static void RampFlyingEntityPush(byte* e, int target)
    {
        int push = *(int*)(e + 0x5c);
        *(int*)(e + 0x5c) = push + ((target - push) >> 4);
    }

    /// <summary>Ghidra: <c>0x0049a802</c> et ses deux jumeaux — le compte a rebours de posture de
    /// cette espece. <b>Ce n'est pas <see cref="SceneEntityDelay"/></b> : la timidite y compte pour
    /// un quart et non pour une moitie, et le plancher est 10 au lieu de 0x14. La Coccinelle change
    /// donc d'avis nettement plus vite que ses voisines.</summary>
    private static byte FlyingEntityDelay(int timid)
        => (byte)(CrtRand() % 10 + (byte)(timid >> 2) + 0x0a);

    /// <summary>
    /// Ghidra: <c>0x0049a360</c> — <b>l'intelligence de la Coccinelle</b>, sous-type 1 de la table
    /// installee <c>0x0056ad48</c> en foret. 1392 octets, <b>absente de <c>Blaze.c</c></b> comme
    /// toute la famille ; releve garde sous <c>Legacy/Raw/Blaze/FUN_0049A360.asm</c>.
    ///
    /// <para>
    /// Meme charpente en trois temps que ses deux voisines — decider, appliquer, replanifier — et
    /// quatre postures, comme <see cref="TickSceneEntityAi_00499DE0"/> : 0 attendre, 1 se tourner,
    /// 2 s'approcher, 3 errer vers un cap range en <c>+0x12</c>. <b>Ce n'est pas <c>+0x10</c></b>
    /// comme chez la sœur : le champ change d'une espece a l'autre, et la replanification ecrit
    /// bien <c>+0x12</c>.
    /// </para>
    /// <para>
    /// <b>Elle pose le bit 1 de ses drapeaux a chaque image</b>, des l'entree, avant toute lecture.
    /// Aucune des deux voisines ne le fait ; c'est aussi ce bit que son interruption exclut de son
    /// garde (<c>0x816</c> et non <c>0x817</c>), les deux choix allant ensemble.
    /// </para>
    /// <para>
    /// <b>Son coup ne connait pas d'image de frappe.</b> Les deux terrestres attendent la huitieme
    /// ou la douzieme image du geste pour balayer le groupe ; celle-ci balaie a <b>chaque</b> image
    /// tant que le geste dure, et accelere pendant ce temps. Un contact prolonge, pas un coup
    /// porte — ce qui va avec une bete qui fonce en vol.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049A360(byte* record)
    {
        byte* e = record;

        // Ghidra: `or eax,edi` avec edi = 1, avant meme la resolution des blocs.
        *(uint*)e |= 1;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x50;
        probe[3] = 0x64;
        probe[4] = unchecked((short)0xff00);   // le gabarit est decale en hauteur : elle vole
        probe[5] = 0x50;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideFlyingEntityAction_0049A360(e, probe, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        BeatWings_0049A68F(e);
        InterruptFlyingEntity_004A1CF0((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanFlyingEntityAction_0049A360(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049a68f..0x0049a710</c> — <b>le vol</b>, et ce qui remplace la fin de geste
    /// chez cette espece.
    ///
    /// <para>
    /// <c>+0x14</c> accumule une <b>phase</b> — de <c>0x100</c> a <c>0x1ff</c> par image, donc
    /// jamais tout a fait la meme — et le sinus de cette phase fait osciller la hauteur autour d'un
    /// plafond. Le plafond est le plus haut de <c>+0x60</c> (le sol sous la bete) et de
    /// <c>+0x48</c>, recopie du <c>+0x60</c> de la cible tant que celle-ci ne porte pas le bit 0 :
    /// <b>la Coccinelle se cale sur l'altitude de ce qu'elle poursuit</b>, sans jamais descendre
    /// sous son propre sol.
    /// </para>
    /// <para>
    /// Pendant un geste, l'oscillation cesse : la hauteur rejoint le plafond majore de
    /// <c>0x28000</c> par huitiemes — elle prend de la hauteur pour foncer. Hors geste elle
    /// rejoint plafond + sinus par seiziemes.
    /// </para>
    /// </summary>
    private static void BeatWings_0049A68F(byte* e)
    {
        *(uint*)(e + 0x90) &= 0xdfffd8ff;
        HoverOnWings(e);
    }

    /// <summary>
    /// Ghidra: <c>0x0049a695..0x0049a710</c> — <b>le vol proprement dit</b>, sans l'effacement des
    /// drapeaux de vue qui le precede chez la Coccinelle.
    ///
    /// <para>
    /// La separation n'est pas cosmetique : <see cref="TickSceneEntityAi_004A35B0"/> emet
    /// exactement ce bloc-ci, mais <b>n'efface les drapeaux de vue que dans une de ses deux
    /// branches d'interruption</b>. Les deux corps partagent donc le vol et pas le masque.
    /// </para>
    /// </summary>
    private static void HoverOnWings(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);
        if ((*chased & 1) == 0) *(int*)(e + 0x48) = *(int*)(chased + 0x60);

        *(short*)(e + 0x14) += (short)((CrtRand() & 0xff) + 0x100);

        int ceiling = *(int*)(e + 0x60);
        if (*(int*)(e + 0x48) >= ceiling) ceiling = *(int*)(e + 0x48);

        int y = *(int*)(e + 0x20);

        if ((*(uint*)e & 2) != 0)
        {
            *(int*)(e + 0x20) = y + ((ceiling - y + 0x28000) >> 3);
            return;
        }

        int lift = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) + 0x3200) << 6;
        *(int*)(e + 0x20) = y + ((lift + ceiling - y) >> 4);
    }

    /// <summary>
    /// Ghidra: <c>0x0049a3f2..0x0049a67a</c> — <b>le premier temps</b> de
    /// <see cref="TickSceneEntityAi_0049A360"/>.
    /// </summary>
    private static void DecideFlyingEntityAction_0049A360(
        byte* e, short* probe, int boldness, int distance, int heading, int speed, uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
                return;
            }

            // Aucun controle d'image du geste : le balayage a lieu a CHAQUE image.
            byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(victim + 0x96) = 0x10;
                *(uint*)victim |= 0x40;

                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
            }

            // Ghidra: LAB_0049a469 — elle ACCELERE pendant le geste, la ou ses voisines remettent
            // la poussee a zero.
            RampFlyingEntityPush(e, (boldness + 0x280) << 7);

            if (e[0x43] == 0) return;
            if (--e[0x43] != 0) return;

            *(uint*)e &= 0xfffffffd;
            return;
        }

        // ================================================================ la posture tenue
        if ((flags & 0x10) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte held = e[0x42];
        if (held > 3) return;

        // Le cone de virage des postures 1 et 2, identique pour les deux.
        int cone = (boldness >> 2) + 0x38;

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
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                DampFlyingEntityPush(e);
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (TurnSceneEntityTowardTarget(e, cone, heading)) e[0x43] = 0;
                DampFlyingEntityPush(e);
                return;

            // ---------------------------------------------------------- 2 : s'approcher
            case 2:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                // La distance tenue est une CONSTANTE ici, la ou la sœur la lit dans +0x10.
                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, cone, heading);
                RampFlyingEntityPush(e, (boldness + 0x1a0) << 7);
                return;

            // ---------------------------------------------------------- 3 : errer
            default:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                RampFlyingEntityPush(e, (boldness + 0x140) << 7);

                // Le cap vise est en +0x12, et non en +0x10 comme chez la sœur.
                int bearing = (*(ushort*)(e + 0x12) - *(ushort*)(e + 0x32)) & 0xfff;
                if (((bearing + 0x100) & 0xfff) <= 0x200) return;

                int step = bearing >= 0x800 ? -0x38 - (boldness >> 2) : (boldness >> 2) + 0x38;
                *(ushort*)(e + 0x32) += (ushort)step;
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0049a742..0x0049a8cf</c> — <b>le troisieme temps</b> : quelle posture prendre
    /// pour la suite.
    ///
    /// <para>
    /// Trois plages de distance, comme chez ses voisines, mais des seuils a elle : <b>sous
    /// 0x12c</b> une chance sur 0x3c d'engager — et l'engagement demande en plus un cap dans le
    /// cone et une distance sous <c>0xfa</c> <b>diminuee</b> d'un tirage (la sœur, elle,
    /// <i>augmente</i> son seuil du meme tirage : plus une Coccinelle hesite, plus elle doit etre
    /// pres) ; sinon elle erre vers un cap tire autour de la cible. <b>Entre 0x12c et 0x258</b>
    /// elle approche, sans condition. <b>Au-dela</b> elle attend, hesite ou erre au hasard.
    /// </para>
    /// <para>
    /// Son engagement ne pose <b>que</b> le bit 2 et un compte a rebours de <c>timidite/8 + 0x10</c> :
    /// ni animation forcee, ni remise a zero de la poussee — elle garde son elan.
    /// </para>
    /// <para>
    /// Le tirage jete de la plage moyenne (<c>call rand</c> dont le resultat n'est pas lu) est
    /// transcrit : il fait avancer le generateur, donc le retirer changerait toute la suite.
    /// </para>
    /// </summary>
    private static void ReplanFlyingEntityAction_0049A360(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        // ================================================================ le corps a corps
        if (distance < 0x12c)
        {
            if (CrtRand() % 100 >= 0x3c)
            {
                // Errer vers un cap tire AUTOUR de la cible : la largeur du tirage croit avec la
                // hardiesse, et il est centre en retranchant sa moitie.
                e[0x42] = 3;
                e[0x43] = FlyingEntityDelay(timid);

                int spread = (boldness + 0x80) << 4;
                int offset = CrtRand() % spread - (spread >> 1);

                *(short*)(e + 0x12) = (short)(offset + heading + *(short*)(e + 0x32));
                return;
            }

            if (((heading + timid * 4 + 0x100) & 0xfff) > timid * 8 + 0x200)
            {
                HesitateSceneEntity(e);
                return;
            }

            if (distance >= 0xfa - CrtRand() % (timid * 2 + 1))
            {
                HesitateSceneEntity(e);
                return;
            }

            *(uint*)e |= 2;
            e[0x43] = (byte)((timid >> 3) + 0x10);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x258)
        {
            CrtRand();                       // Ghidra: `call 00547140h` dont le resultat est jete
            e[0x42] = 2;
            e[0x43] = FlyingEntityDelay(timid);
            return;
        }

        // ================================================================ l'errance
        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            e[0x42] = 0;
            e[0x43] = FlyingEntityDelay(timid);
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        e[0x42] = 3;
        e[0x43] = FlyingEntityDelay(timid);
        *(short*)(e + 0x12) = (short)CrtRand();
    }
}
