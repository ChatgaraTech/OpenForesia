// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE POISSON VOLANT — 0x0049C8E0, sous-type 6 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// SON NOM TROMPE : il appelle l'integration TERRESTRE FUN_00488160, pas celle du vol. Il
// ne vole pas, il BONDIT.
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
    // 0x0049C8E0 — LE POISSON VOLANT, sous-type 6 du bloc A de la foret. Transcrite le 2026-08-29
    // au desassembleur (Legacy/Raw/Blaze/FUN_0049C8E0.asm, 1866 octets, absente de Blaze.c).
    //
    // SON NOM TROMPE : elle appelle FUN_00488160, l'integration TERRESTRE, et non FUN_0049a8e0
    // comme la Coccinelle. Elle ne vole pas, elle BONDIT — le bit 1 de ses drapeaux veut dire « en
    // l'air », +0x54 porte sa vitesse verticale, et l'atterrissage se detecte en comparant +0x20 au
    // sol de +0x60. C'est le tri par la fonction d'integration, recommande dans Comportement.md,
    // qui l'a dit en une ligne.
    //
    // ET ELLE S'ACCROCHE. Aucune des trois routines precedentes ne fait cela : quand son bond
    // touche un membre du groupe, elle se colle a lui (bit 0 de +0x40), range l'ecart auquel elle
    // s'est fixee dans +0x44/+0x48/+0x4c, et RECOPIE la position de sa victime a chaque image au
    // lieu d'integrer son propre mouvement. Elle mord tous les 0x1e images. Le joueur s'en
    // debarrasse en REMUANT : la routine lit son propre pad, et chaque image ou une direction est
    // tenue fait descendre le compteur de prise +0x10.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x0049c8e0</c> — <b>l'intelligence du Poisson volant</b>, sous-type 6 de la table
    /// installee <c>0x0056ad48</c> en foret.
    ///
    /// <para>
    /// <b>Trois postures seulement</b>, et <b>aucune table de sauts</b> : l'aiguillage est une
    /// cascade de comparaisons (0 attendre, 1 se tourner, 2 bondir), toute autre valeur ne faisant
    /// rien. C'est la premiere des quatre routines ecrites a se passer de table — donc la recette
    /// « relever la table de sauts qui suit le corps » ne s'applique pas partout.
    /// </para>
    /// <para>
    /// Sa sonde est <c>{0, 0, 0x28, 0x32, 0, 0x3c}</c> : la plus courte des quatre, et sans le
    /// decalage vertical de la Coccinelle. Elle frappe au ras du sol, a bout portant.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049C8E0(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0x28;
        probe[3] = 0x32;
        probe[4] = 0;
        probe[5] = 0x3c;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideLeapingEntityAction_0049C8E0(e, probe, boldness, distance, heading, speed, timid, traits);

        // ---------------------------------------------------------------- 2. appliquer
        ApplyLeapingEntityMotion_0049CD7C(e, speed);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        // Accrochee, elle ne replanifie pas : c'est la prise qui mene, et seule sa rupture rend
        // la main a la decision.
        if ((e[0x40] & 1) != 0) return;

        ReplanLeapingEntityAction_0049C8E0(e, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049c96b..0x0049cd7b</c> — <b>le premier temps</b> du Poisson volant.
    ///
    /// <para>
    /// <b>Le geste en cours a deux visages</b>, selon le bit 0 de <c>+0x40</c>. Tant qu'il est
    /// libre, chaque image cherche une victime sous le gabarit ; des qu'il en tient une, il ne
    /// cherche plus rien et se contente de decompter <c>+0x14</c> jusqu'a la morsure suivante. La
    /// prise se defait de trois facons : l'espece perd le trait <c>0x10</c>, la victime meurt
    /// (bit <c>0x40000000</c>), ou le poisson touche le sol.
    /// </para>
    /// <para>
    /// <b>Le bond se joue en deux images</b>, par le compte a rebours <c>+0x43</c> que la
    /// replanification pose a 2 : a 2 il prend son elan et tourne, a 1 il detend. Et il n'y a pas
    /// un bond mais <b>deux</b>, choisis par le bit <c>0x200</c> des drapeaux de vue — le second
    /// (bit absent) donne une poussee plus faible mais une detente verticale plus haute, melee a
    /// la timidite.
    /// </para>
    /// </summary>
    private static void DecideLeapingEntityAction_0049C8E0(
        byte* e, short* probe, int boldness, int distance, int heading, int speed, int timid,
        uint traits)
    {
        if (speed == 0) return;

        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                e[0x43] = 0;
                e[0x40] &= 0xfe;
                return;
            }

            byte* victim = null;

            if ((e[0x40] & 1) != 0)
            {
                // Deja accrochee : rien a chercher, seulement le compte avant la morsure.
                *(ushort*)(e + 0x14) -= 1;
                if (*(ushort*)(e + 0x14) == 0) victim = *(byte**)(e + 0x6c);
            }
            else
            {
                victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (victim != null)
                {
                    // L'ecart auquel elle se fixe, en coordonnees du monde : 60 fois le sinus et le
                    // cosinus du cap qui va de la victime vers elle, et l'ecart de hauteur tel quel.
                    int dz = (*(int*)(e + 0x24) - *(int*)(victim + 0x24)) >> 0xc;
                    int dx = (*(int*)(e + 0x1c) - *(int*)(victim + 0x1c)) >> 0xc;

                    int angle = Winsys3d.Ordinal_457(dx, dz);

                    *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(angle) * 60;
                    *(int*)(e + 0x48) = *(int*)(e + 0x20) - *(int*)(victim + 0x20);
                    *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(angle) * 60;

                    e[0x40] |= 1;

                    // La force de la prise : de 8 a 15 images de secouage, plus un quart de la
                    // hardiesse. Une bete hardie s'accroche plus longtemps.
                    *(ushort*)(e + 0x10) = (ushort)((CrtRand() & 7) + (boldness >> 2) + 8);

                    *(ushort*)(victim + 0x96) = 0x10;
                    *(uint*)victim |= 0x40;

                    // Elle poursuit desormais celui qu'elle tient.
                    *(byte**)(e + 0x6c) = victim;
                }
            }

            if (victim != null)
            {
                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));

                if ((*(uint*)victim & 0x40000000) != 0)
                {
                    // La victime s'en va : la prise tombe avec elle.
                    e[0x40] &= 0xfe;
                    *(uint*)e &= 0xfffffffd;
                }
                else
                {
                    *(ushort*)(e + 0x14) = 0x1e;
                }
            }

            // Ghidra: LAB_0049ca9f — tant qu'elle est en l'air, le geste continue.
            flags = *(uint*)e;
            if ((flags & 1) != 0) return;

            *(uint*)e = flags & 0xfffffffd;
            e[0x43] = 0;
            e[0x40] &= 0xfe;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        // ================================================================ la posture tenue
        if ((flags & 0x11) != 0) return;
        if ((flags & 0x804) != 0) return;

        // Le cone de virage, commun aux trois postures qui tournent : deux fois la hardiesse.
        int cone = boldness * 2 + 0x80;

        switch (e[0x42])
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

                if (TurnSceneEntityTowardTarget(e, cone, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : bondir
            case 2:
                break;

            default:
                return;
        }

        // ================================================================ le bond
        if ((*(uint*)(e + 0x90) & 0x200) == 0)
        {
            // Ghidra: LAB_0049cc4d — le bond « libre », sans appui : poussee moindre, detente plus
            // haute, et la timidite s'y ajoute par paliers de seize.
            TurnSceneEntityTowardTarget(e, cone, heading);

            *(uint*)e = flags | 0x1000002;
            *(int*)(e + 0x18) = 3;

            int freePush = (boldness + 0x140) << 7;
            *(int*)(e + 0x5c) = freePush;

            AddDirectedImpulse(e, freePush);

            int lift = (((CrtRand() & 7) + 0x10) << 4) + (timid & unchecked((int)0xfffffff0));
            *(int*)(e + 0x54) += lift << 8;

            *(uint*)e |= 1;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (*(ushort*)(e + 0x18) != 1)
        {
            flags |= 0x1000000;
            *(int*)(e + 0x18) = 1;
            *(uint*)e = flags;
        }

        // Ghidra: LAB_0049cb0b — le compte a rebours mene les deux images du bond.
        byte step = e[0x43];

        if (step == 2)
        {
            // L'elan : elle tourne et se donne de la vitesse au sol, sans quitter le sol.
            TurnSceneEntityTowardTarget(e, cone, heading);

            *(int*)(e + 0x5c) = (boldness + 0x280) << 7;
            AddDirectedImpulseThenClear(e);

            e[0x43]--;
            return;
        }

        if (step != 1) return;

        // Ghidra: LAB_0049cba5 — la detente. Le compte ne descend que si elle a assez ralenti :
        // la vitesse au sol doit etre tombee sous un huitieme de la hardiesse.
        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;

        if ((int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz)) <= (boldness >> 3) + 1)
        {
            e[0x43]--;
        }

        // +0x10 porte ici la portee du bond, posee a 0x12c par la replanification.
        if (distance >= *(short*)(e + 0x10)) return;

        int leapPush = (boldness + 0x200) << 7;
        *(int*)(e + 0x5c) = leapPush;
        *(uint*)e |= 0x1000002;
        *(int*)(e + 0x18) = 3;

        AddDirectedImpulse(e, leapPush);

        *(int*)(e + 0x54) = ((CrtRand() & 7) + 0x20) << 0xc;
        *(uint*)e |= 1;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>Ghidra: le motif <c>sin * poussee &gt;&gt; 12</c> ajoute a <c>+0x50</c> et
    /// <c>+0x58</c>, repete trois fois dans le bond.</summary>
    private static void AddDirectedImpulse(byte* e, int push)
    {
        *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * push >> 0xc;
        *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * push >> 0xc;
    }

    /// <summary>Le meme motif, mais la poussee est remise a zero <b>entre</b> les deux composantes
    /// dans le binaire — apres la multiplication du sinus, avant celle du cosinus. Sans
    /// consequence, les deux lisant la meme valeur deja chargee ; transcrit dans cet ordre.</summary>
    private static void AddDirectedImpulseThenClear(byte* e)
    {
        int push = *(int*)(e + 0x5c);

        *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * push >> 0xc;

        int forward = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * push >> 0xc;
        *(int*)(e + 0x5c) = 0;
        *(int*)(e + 0x58) += forward;
    }

    /// <summary>
    /// Ghidra: <c>0x0049cd7c..0x0049cef9</c> — <b>le deuxieme temps</b> : appliquer. Il fait trois
    /// choses qu'aucune des trois autres routines ne fait.
    ///
    /// <para>
    /// <b>1. La prise remplace le mouvement.</b> Accrochee, l'entite ne s'integre pas : sa position
    /// est recopiee de sa victime, plus l'ecart range en <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>.
    /// </para>
    /// <para>
    /// <b>2. Le joueur se debat.</b> La routine remonte jusqu'au <b>pad</b> de la victime — par
    /// <c>0x005e9188</c> puis <c>0x00604a20</c>, l'indexation exacte des sondes d'entree — et
    /// n'use le compteur de prise <c>+0x10</c> que si l'un des quatre bits de direction
    /// (<c>0xf0</c> du troisieme octet) est tenu. A zero, elle lache et part <b>en arriere</b> :
    /// cap + 0x800, et une detente verticale fixe de 0xa000. Le personnage porte par un autre
    /// joueur est reconnu par le bit <c>0x10000</c> de ses drapeaux, auquel cas c'est le pad du
    /// meneur qui compte.
    /// </para>
    /// <para>
    /// <b>3. L'atterrissage.</b> Sous le sol de <c>+0x60</c>, la hauteur y est ramenee, le bit
    /// « en l'air » tombe, la vitesse verticale est annulee, et un geste en cours est interrompu.
    /// Au-dessus, le bit est pose. C'est tout ce qui distingue un bond d'un vol.
    /// </para>
    /// </summary>
    private static void ApplyLeapingEntityMotion_0049CD7C(byte* e, int speed)
    {
        if ((e[0x40] & 1) != 0)
        {
            byte* host = *(byte**)(e + 0x6c);

            *(int*)(e + 0x1c) = *(int*)(host + 0x1c) + *(int*)(e + 0x44);
            *(int*)(e + 0x20) = *(int*)(host + 0x20) + *(int*)(e + 0x48);
            *(int*)(e + 0x24) = *(int*)(host + 0x24) + *(int*)(e + 0x4c);

            uint player = (*(uint*)host & 0x10000) != 0
                ? (uint)(byte)Mem.s_Unknown_005de538
                : host[8];

            byte* pad = Inp.s_InputStateBase_00604a20
                      + Inp.s_PlayerSlots_005e9188[player * 0x4c] * 0x10;

            if ((pad[2] & 0xf0) != 0)
            {
                *(ushort*)(e + 0x10) -= 1;

                if (*(ushort*)(e + 0x10) == 0)
                {
                    int away = *(short*)(e + 0x32) + 0x800;

                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                    e[0x40] &= 0xfe;

                    *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(away) << 5;
                    *(int*)(e + 0x54) = 0xa000;
                    *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(away) << 5;
                }
            }
        }
        else
        {
            IntegrateSceneEntityMotion_00488160(e, speed);
        }

        // Ghidra: LAB_0049ce4c
        FinalizeEntityPlacement2((uint*)e);

        uint view = *(uint*)(e + 0x90) & 0xdfffdbff;
        *(uint*)(e + 0x90) = view;

        int ground = *(int*)(e + 0x60);

        if (*(int*)(e + 0x20) <= ground)
        {
            *(int*)(e + 0x20) = ground;

            uint flags = *(uint*)e;
            if ((flags & 3) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                e[0x43] = 0;
                e[0x40] &= 0xfe;
            }

            *(uint*)e &= 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }
        else if ((*(uint*)e & 1) == 0)
        {
            *(uint*)e |= 1;
        }

        // Ghidra: LAB_0049ce9f — la prise se rompt aussi quand la victime disparait ou s'occupe.
        bool interrupt = (view & 0x8882) != 0;

        if ((e[0x40] & 1) != 0)
        {
            byte* host = *(byte**)(e + 0x6c);
            if ((*(uint*)host & 0x50002000) != 0 || *(ushort*)(host + 0x12) != 0) interrupt = true;
        }

        if (interrupt)
        {
            *(uint*)e &= 0xfffffffd;
            e[0x42] = 1;
            e[0x43] = 1;
            e[0x40] &= 0xfe;
            *(int*)(e + 0x5c) = 0;
        }

        // Ghidra: LAB_0049ced6 — le coup recu, meme forme que FUN_004e3770 mais ecrite ici.
        if (*(short*)(e + 0x96) == 0) return;

        uint state = *(uint*)e;

        if ((state & 0x40) != 0)
        {
            if ((state & 0x43) == 0) e[0x43] = 0;
            return;
        }

        e[0x42] = 0;
        e[0x43] = 1;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0049cefa..0x0049d029</c> — <b>le troisieme temps</b>.
    ///
    /// <para>
    /// Trois plages, et un choix nettement plus tranche que chez ses voisines : <b>sous 0xfa</b>
    /// une chance sur deux de bondir, sinon se tourner ; <b>entre 0xfa et 0x1f4</b> 45 % se
    /// tourner, 45 % bondir, le reste attendre ; <b>au-dela</b> une chance sur deux de se tourner,
    /// sinon attendre. Elle ne se repose jamais et n'erre jamais — deux postures que cette espece
    /// n'a tout simplement pas.
    /// </para>
    /// <para>
    /// Bondir pose toujours le meme couple : posture 2, compte a rebours 2 (les deux images du
    /// bond), et <c>+0x10</c> a <b>0x12c</b> — la portee sous laquelle la detente part.
    /// </para>
    /// <para>
    /// Le cap relatif est calcule puis <b>jamais lu</b>. C'est dans le binaire ; conserve, parce
    /// que le calcul consomme les memes appels que partout ailleurs et que le retirer changerait
    /// la lecture du reste.
    /// </para>
    /// </summary>
    private static void ReplanLeapingEntityAction_0049C8E0(byte* e, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        // Ghidra le range en pile et ne le relit jamais.
        _ = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        if (distance < 0xfa)
        {
            if (CrtRand() % 100 < 0x32)
            {
                LeapAt(e);
                return;
            }

            HesitateSceneEntity(e);
            return;
        }

        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x5a)
            {
                LeapAt(e);
                return;
            }
        }
        else if (CrtRand() % 100 < 0x32)
        {
            HesitateSceneEntity(e);
            return;
        }

        // Ghidra: LAB_0049d003 — l'attente, seule sortie commune aux deux plages lointaines.
        e[0x42] = 0;
        e[0x43] = SceneEntityDelay(timid);
    }

    /// <summary>Ghidra: le triplet <c>+0x42 = 2 ; +0x43 = 2 ; +0x10 = 0x12c</c>, pose a l'identique
    /// aux deux endroits ou la replanification decide de bondir.</summary>
    private static void LeapAt(byte* e)
    {
        e[0x42] = 2;
        e[0x43] = 2;
        *(ushort*)(e + 0x10) = 0x12c;
    }
}
