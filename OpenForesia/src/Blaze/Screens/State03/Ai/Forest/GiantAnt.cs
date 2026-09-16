// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA FOURMI GEANTE — 0x00499DE0, sous-type 0 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// C'est la SECONDE CHARPENTE DE REFERENCE du depot, celle a QUATRE postures - l'autre,
// a cinq, est le Goblin 0x0049A980 dans `SceneEntityAiCommon.cs`. Le Varan et la Fleur du
// mal sont bases sur celle-ci.
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

    /// <summary>
    /// Ghidra: <c>0x00499de0</c> — <b>la seconde intelligence d'entite de scene</b> : le
    /// gestionnaire du sous-type <b>0</b> de la table installee <c>0x0056ad48</c>. 1377 octets,
    /// <b>absente de <c>Blaze.c</c></b>, transcrite au desassembleur
    /// (<c>Legacy/Raw/Blaze/FUN_00499DE0.asm</c>).
    ///
    /// <para>
    /// <b>C'est la sœur de <see cref="TickSceneEntityAi_0049A980"/></b> : meme decoupage en trois
    /// temps, meme lecture de situation, meme queue de quatre appels, et les memes feuilles. Elle
    /// n'est pas factorisee avec elle parce que ce sont <b>deux fonctions distinctes du binaire</b>,
    /// et que l'oracle de <c>compare-bodies.ps1</c> compare des corps — meme raison que les trois
    /// passes <c>FUN_0047aa70</c>/<c>ab70</c>/<c>acf0</c>.
    /// </para>
    ///
    /// <para>
    /// <b>Ce qui la distingue tient en cinq nombres et une absence</b>, et l'ecart dessine une bete
    /// plus petite et plus prudente :
    /// </para>
    /// <list type="bullet">
    /// <item>sa <b>sonde</b> est plus courte et plus basse — <c>0x50</c> de tolerance en hauteur et
    /// <c>0x3c</c> de portee, contre <c>0x64</c> et <c>0x50</c> ;</item>
    /// <item>son coup porte a la <b>douzieme</b> image du geste, pas a la huitieme ;</item>
    /// <item>elle n'a que <b>quatre</b> postures (table de sauts a <c>0x0049a344</c>, quatre entrees
    /// relevees dans le binaire) — il n'y a pas de « foncer » ;</item>
    /// <item>ses cones de virage sont bien plus etroits : <c>hardiesse + 0x20</c> et
    /// <c>hardiesse + 0x40</c>, la ou sa sœur ouvre a <c>hardiesse * 2 + 0x80</c> ;</item>
    /// <item><b>elle ne bondit pas et ne se recule pas</b>. Ni la branche de recul (bit
    /// <c>0x10</c>), ni la relance d'attaque en fin de geste, ni l'esquive de cote n'existent :
    /// le mot d'etat <c>0x11</c> la renvoie directement a la queue.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Sa posture 3 tourne au lieu de marcher</b>, et c'est la seule difference de <i>forme</i>
    /// et non de constante. La sœur y avance en ligne droite ou bondit ; celle-ci pose une poussee
    /// <c>(hardiesse + 0xa0) &lt;&lt; 7</c> et <b>pivote vers un cap range en <c>+0x10</c></b> a
    /// <c>0x20 + hardiesse / 2</c> par image, en s'arretant des que l'ecart tient sous
    /// <c>0x100</c>. Le champ <c>+0x10</c> sert donc ici aussi a deux choses — distance voulue en
    /// posture 2, cap vise en posture 3.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_00499DE0(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x50;
        probe[4] = 0;
        probe[5] = 0x3c;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideSceneEntityAction_00499DE0(e, probe, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanSceneEntityAction_00499DE0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x00499e68..0x0049a0cb</c> — <b>le premier temps</b> de
    /// <see cref="TickSceneEntityAi_00499DE0"/>. Contrairement a sa sœur, aucune sortie ne demande
    /// de remise a zero separee de la poussee : celles qui la veulent l'ecrivent elles-memes.
    /// </summary>
    private static void DecideSceneEntityAction_00499DE0(
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

            // Le coup porte a la douzieme image, et non a la huitieme comme chez la sœur.
            if (*(short*)(e + 0x1a) == 0x0c)
            {
                byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    // Ici le clignotement est pose AVANT la resolution, l'inverse de
                    // TickSceneEntityAi_0049A980. Sans consequence — rien ne les relit entre les
                    // deux — mais c'est bien ce que le binaire fait.
                    *(ushort*)(victim + 0x96) = 0x10;
                    *(uint*)victim |= 0x40;

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                }

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

            if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        // ================================================================ la posture tenue
        // Ghidra: LAB_00499f13 — le bit 0x10 ne mene nulle part chez cette espece : pas de recul,
        // pas de relance d'attaque, la queue directement.
        if ((flags & 0x11) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte held = e[0x42];
        if (held > 3) return;

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

                if (TurnSceneEntityTowardTarget(e, boldness + 0x20, heading)) e[0x43] = 0;
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

                TurnSceneEntityTowardTarget(e, boldness + 0x40, heading);
                *(int*)(e + 0x5c) = (boldness + 0xa0) << 7;
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

                *(int*)(e + 0x5c) = (boldness + 0xa0) << 7;

                // Le cap vise est dans +0x10 — pose par la replanification, pas par la cible.
                int bearing = (*(ushort*)(e + 0x10) - *(ushort*)(e + 0x32)) & 0xfff;
                if (((bearing + 0x100) & 0xfff) <= 0x200) return;

                int step = bearing >= 0x800 ? -0x20 - (boldness >> 1) : (boldness >> 1) + 0x20;
                *(ushort*)(e + 0x32) += (ushort)step;
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0049a11d..0x0049a338</c> — <b>le troisieme temps</b> de
    /// <see cref="TickSceneEntityAi_00499DE0"/>. Meme decoupage en trois plages de distance que sa
    /// sœur, mais un tirage plus simple : ni esquive de cote, ni charge, et le meme errement
    /// dessert les trois plages.
    ///
    /// <para>Sous <c>0x0fa</c>, une chance sur 0x28 de frapper — <b>plus du double</b> de la
    /// sœur — sinon tenir la distance ou errer ; entre <c>0x0fa</c> et <c>0x1f4</c>, tenir,
    /// errer ou se reposer ; au-dela, se reposer, hesiter, ou errer vers un cap entierement tire
    /// au sort.</para>
    /// </summary>
    private static void ReplanSceneEntityAction_00499DE0(byte* e, int boldness, int timid)
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

            if (roll < 0x28
                && ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                && distance < CrtRand() % (timid * 2 + 1) + 100)
            {
                uint state = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (state & 0xfeffffff) | 2;
                return;
            }

            // Entre 0x28 et 0x50 elle tient la distance ; partout ailleurs elle erre. Le
            // court-circuit ci-dessus renvoie donc a l'errement quand le coup ne part pas.
            if (roll >= 0x28 && roll < 0x50)
            {
                HoldSceneEntityAtRange(e, timid, 0x5a);
                return;
            }

            WanderSceneEntity(e, boldness, timid, heading, 0);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                HoldSceneEntityAtRange(e, timid, 0x5a);
                return;
            }

            if (roll < 0x50)
            {
                WanderSceneEntity(e, boldness, timid, heading, 0);
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

        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }
}
