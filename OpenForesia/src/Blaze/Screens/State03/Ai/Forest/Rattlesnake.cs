// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE CROTALE — 0x0049E920, sous-type 18 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// 1 932 octets, absents de `Blaze.c`. C'etait la SEULE ligne « implemente=non, teste=oui »
// de la zone 3 : la sonde l'avait vu passer avant que son corps n'existe.
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
    // =====================================================================
    // LA QUATRIEME INTELLIGENCE D'ENTITE DE SCENE (2026-09-01) — 0x0049e920, foret, sous-zones 0 a 4.
    //
    // 1932 octets, ABSENTS de `Blaze.c`, quarante-huit etiquettes de saut et huit `return`.
    // Releve garde sous `Legacy/Raw/Blaze/FUN_0049E920.asm`.
    //
    // C'etait la SEULE ligne « implemente=non, teste=oui » de la zone 3 : la sonde l'avait vue
    // passer en sous-zone 2, ce qui veut dire que le jeu y allait vraiment et plantait en y allant.
    //
    // Meme decoupage en trois temps que ses trois sœurs (0x00499de0, 0x0049a980, 0x0049e190) et
    // memes feuilles — mais elle EMPOIGNE sa proie, ce qu'aucune des trois ne fait, et sa phase
    // d'application est ecrite en clair au lieu d'appeler les deux fonctions communes.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0049e920</c> — <b>la quatrieme intelligence d'entite de scene</b> : un
    /// gestionnaire de la table installee <c>0x0056ad48</c>, foret, sous-zones 0 a 4. 1932 octets,
    /// <b>absents de <c>Blaze.c</c></b> ; releve sous <c>Legacy/Raw/Blaze/FUN_0049E920.asm</c>.
    ///
    /// <para>
    /// <b>Elle empoigne.</b> C'est ce qui la separe de ses trois sœurs. Quand sa sonde de geste
    /// trouve un membre du groupe, elle ne se contente pas de le frapper : elle retient l'ecart de
    /// position dans <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>, garde la victime dans <c>+0x6c</c>,
    /// allume le bit <c>1</c> de <c>+0x40</c> — sa marque de prise — et se <b>colle</b> a elle a
    /// chaque image tant que la prise tient. Elle frappe une fois par image pendant ce temps.
    /// </para>
    /// <para>
    /// <b>Et on s'en debarrasse en secouant la manette.</b> Chaque image de prise, le corps
    /// remonte de la victime a sa manette — par l'octet <c>+8</c> de l'emplacement, le
    /// <c>* 0x4c</c> de <see cref="Input.s_PlayerSlots_005e9188"/> puis le <c>* 0x10</c> de la
    /// table d'etat — et regarde les quatre bits <c>0xf0</c> de l'octet <c>+2</c>. Tant qu'ils sont
    /// nuls la prise tient ; des qu'ils bougent, le compteur <c>+0x10</c> descend, et a zero la
    /// bete lache en etant <b>projetee a l'oppose de son propre cap</b> (<c>+0x50</c> et
    /// <c>+0x58</c> recoivent <c>sin/cos(cap + 0x800) &lt;&lt; 5</c>). Le compteur de depart vaut
    /// <c>hasard % 8 + hardiesse / 4 + 8</c> : une bete hardie tient plus longtemps.
    /// </para>
    /// <para>
    /// <b>Un membre du groupe qui porte le bit <c>0x10000</c></b> — le meneur — est retrouve par
    /// <c>DAT_005de538</c> plutot que par son propre <c>+8</c>. C'est ecrit ainsi ; le pourquoi
    /// n'est pas etabli.
    /// </para>
    /// <para>
    /// <b>Trois fois sur dix la prise empoisonne</b> : si <c>+0x16</c> est non nul, un tirage sur
    /// cent sous trente appelle <see cref="ApplyTrapEffect_0053C420"/> — l'evenement <c>0x1d</c> —
    /// apres avoir pose <c>7</c> dans <c>+0x07</c>.
    /// </para>
    /// <para>
    /// <b>Sa sonde de geste est <c>(0, 0x32, 0x23, 0x32, 0, 0x5a)</c></b>, la seule de la famille a
    /// porter un decalage vertical (<c>0x32</c> en <c>+0x02</c> et en <c>+0x06</c>) : elle frappe
    /// en hauteur, ce que ses sœurs ne font pas.
    /// </para>
    /// <para>
    /// <b>Trois bandes de distance</b>, comme ses sœurs, avec ses proportions propres : sous
    /// <c>0xfa</c> elle empoigne <b>six fois sur dix</b> — de loin la plus agressive de la
    /// famille —, tient sa distance a <c>0x5a</c> jusqu'a 79, et erre au-dela. Entre <c>0xfa</c> et
    /// <c>0x1f4</c> elle tient sa distance une fois sur deux, erre jusqu'a 79 et se repose ensuite.
    /// Au-dela de <c>0x1f4</c> elle se repose trois fois sur dix, hesite jusqu'a 59, et part
    /// sinon dans une direction <b>entierement tiree au sort</b>.
    /// </para>
    /// <para>
    /// <b>Elle ne replanifie pas tant qu'elle tient</b> : le bit <c>1</c> de <c>+0x40</c> est un
    /// quatrieme verrou en tete du troisieme temps, en plus des trois que ses sœurs ont.
    /// </para>
    /// <para>
    /// <b>Elle resout les trois blocs de description et n'en lit aucun.</b> Ses sœurs gardent le
    /// bloc de statistiques pour <see cref="InterruptSceneEntity_004E3770"/> ; celle-ci a son
    /// interruption ecrite en clair, qui ne le consulte pas. Les trois emplacements de pile sont
    /// ecrits par <see cref="ResolveEntityBlocks"/> et jamais relus — verifie sur le
    /// desassemblage, aucune instruction ne les touche apres l'appel. L'appel est conserve : il
    /// est dans le corps.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049E920(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out _, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x32;
        probe[2] = 0x23;
        probe[3] = 0x32;
        probe[4] = 0;
        probe[5] = 0x5a;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideSceneEntityAction_0049E920(e, probe, boldness, distance, heading, traits);
        }

        // ---------------------------------------------------------------- 2. appliquer
        SettleSceneEntityGrapple_0049E920(e, speed);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;
        if ((e[0x40] & 1) != 0) return;          // la prise tient : rien a decider

        ReplanSceneEntityAction_0049E920(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049e9a9..0x0049ecc7</c> — <b>le premier temps</b> de
    /// <see cref="TickSceneEntityAi_0049E920"/>.
    ///
    /// <para>
    /// Le geste en cours empoigne (voir le resume du gestionnaire) ; sinon une table de sauts
    /// bornee a 3, celle de <c>0x0049f0ac</c>, aiguille sur la posture <c>+0x42</c>. Le chemin du
    /// milieu — <c>0x804</c>, celui par lequel la sœur <c>0x0049e190</c> lance ses sorts — rend la
    /// main sans rien faire ici.
    /// </para>
    /// </summary>
    private static void DecideSceneEntityAction_0049E920(
        byte* e, short* probe, int boldness, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
                e[0x40] &= 0xfe;                       // la prise est rompue
                return;
            }

            byte* victim = null;

            // Ghidra: LAB_0049e9de
            if ((e[0x40] & 1) != 0)
            {
                // La prise tient : le compte a rebours descend, et a zero la victime est relachee
                // en etant frappee une derniere fois.
                (*(short*)(e + 0x14))--;
                if (*(short*)(e + 0x14) == 0) victim = *(byte**)(e + 0x6c);
            }
            else if (*(short*)(e + 0x1a) >= 9)
            {
                // Ghidra: LAB_0049e9fd
                victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (victim != null)
                {
                    // L'ecart que la phase d'application relira a chaque image pour rester colle.
                    *(int*)(e + 0x44) = *(int*)(e + 0x1c) - *(int*)(victim + 0x1c);
                    *(int*)(e + 0x48) = *(int*)(e + 0x20) - *(int*)(victim + 0x20);
                    *(int*)(e + 0x4c) = *(int*)(e + 0x24) - *(int*)(victim + 0x24);

                    *(short*)(e + 0x10) = (short)((CrtRand() & 7) + (boldness >> 2) + 8);
                    e[0x40] |= 1;

                    *(uint*)victim |= 0x40;
                    *(short*)(victim + 0x96) = 0x10;
                    *(byte**)(e + 0x6c) = victim;

                    // Ghidra: le `mov [esi+6Ch],edi` est glisse entre le test et le saut : il a
                    // lieu quel que soit le verdict.
                    if (*(short*)(e + 0x16) != 0 && CrtRand() % 100 < 0x1e)
                    {
                        e[0x07] = 7;
                        ApplyTrapEffect_0053C420((uint*)e, (uint*)victim, 0);
                    }
                }
            }

            // Ghidra: LAB_0049ea9f
            if (victim != null)
            {
                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));

                if ((*(uint*)victim & 0x40000000) != 0)
                {
                    e[0x40] &= 0xfe;
                    *(uint*)e &= 0xfffffffd;
                }
                else
                {
                    // Ghidra: LAB_0049eadf — la prise repart pour trente images.
                    *(short*)(e + 0x14) = 0x1e;
                }
            }

            // Ghidra: LAB_0049eae5
            uint settled = *(uint*)e;
            if ((settled & 0x2000000) == 0) return;
            if ((e[0x40] & 1) != 0) return;

            *(uint*)e = settled & 0xfffffffd;
            return;
        }

        // ================================================================ la posture tenue
        if ((flags & 0x11) != 0) return;
        if ((flags & 0x804) != 0) return;

        int posture = e[0x42];
        if ((uint)posture > 3) return;

        switch (posture)
        {
            case 0:
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            case 1:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (TurnSceneEntityTowards_0049E384(e, boldness + 0x20, heading)) e[0x43] = 0;

                *(int*)(e + 0x5c) = 0;
                return;

            case 2:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowards_0049E384(e, boldness + 0x40, heading);
                *(int*)(e + 0x5c) = (boldness + 0xe0) << 7;
                return;

            default:
                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                {
                    // La posture 3 marche vers le cap range dans +0x10, et s'y aligne par pas de
                    // hardiesse/2 + 0x20 tant que l'ecart depasse un huitieme de tour.
                    *(int*)(e + 0x5c) = (boldness + 0xe0) << 7;

                    short course = *(short*)(e + 0x32);
                    int gap = (*(short*)(e + 0x10) - course) & 0xfff;

                    if (((gap + 0x100) & 0xfff) <= 0x200) return;

                    int step = (gap & 0xfff) >= 0x800
                        ? -0x20 - (boldness >> 1)
                        : (boldness >> 1) + 0x20;

                    *(short*)(e + 0x32) = (short)(course + step);
                }
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0049ecc8..0x0049ee4b</c> — <b>le deuxieme temps</b> de
    /// <see cref="TickSceneEntityAi_0049E920"/>, ecrit en clair la ou ses sœurs appellent
    /// <see cref="IntegrateSceneEntityMotion_00488160"/>,
    /// <see cref="EndSceneEntityAnimation_004DFAF0"/> et
    /// <see cref="InterruptSceneEntity_004E3770"/>.
    ///
    /// <para>
    /// <b>Ce n'est pas une copie de ces trois-la.</b> La pose au sol lit le <b>plancher</b>
    /// <c>+0x60</c> et non le plafond <c>+0x64</c>, avec une marge de <c>0xc000</c> quand l'entite
    /// n'est pas en cours d'animation ; et l'interruption ne consulte pas le bloc de statistiques.
    /// Les differences sont trop nombreuses pour que ce soit la meme fonction repliee : c'est un
    /// autre corps, et il est transcrit comme tel.
    /// </para>
    /// <para>
    /// <b>Quand la prise tient, le deplacement ordinaire n'a pas lieu du tout</b> : la position est
    /// recalculee depuis celle de la victime et l'ecart retenu, et c'est la manette de la victime
    /// qui decide de la suite.
    /// </para>
    /// </summary>
    private static void SettleSceneEntityGrapple_0049E920(byte* e, int speed)
    {
        if ((e[0x40] & 1) != 0)
        {
            byte* victim = *(byte**)(e + 0x6c);

            *(int*)(e + 0x1c) = *(int*)(e + 0x44) + *(int*)(victim + 0x1c);
            *(int*)(e + 0x20) = *(int*)(e + 0x48) + *(int*)(victim + 0x20);
            *(int*)(e + 0x24) = *(int*)(e + 0x4c) + *(int*)(victim + 0x24);

            // La manette de la victime, par la triple indirection habituelle. Le bit 0x10000
            // designe le meneur, retrouve par DAT_005de538 et non par son propre +8.
            int character = (*(uint*)victim & 0x10000) != 0
                ? (byte)Mem.s_Unknown_005de538
                : victim[8];

            byte* pad = Inp.s_InputStateBase_00604a20
                        + Inp.s_PlayerSlots_005e9188[character * 0x4c] * 0x10;

            if ((pad[2] & 0xf0) != 0)
            {
                // Ghidra: LAB_0049ed2b — le joueur se debat.
                (*(short*)(e + 0x10))--;

                if (*(short*)(e + 0x10) == 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                    e[0x40] &= 0xfe;

                    int away = *(short*)(e + 0x32) + 0x800;
                    *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(away) << 5;
                    *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(away) << 5;
                }
            }
        }
        else
        {
            // Ghidra: LAB_0049ed7e
            IntegrateSceneEntityMotion_00488160(e, speed);
        }

        // Ghidra: LAB_0049ed8e
        FinalizeEntityPlacement2((uint*)e);

        // ---------------------------------------------------------------- la pose au sol
        uint flags = *(uint*)e;
        bool animating = (flags & 1) != 0;

        bool onFloor = animating
            ? *(int*)(e + 0x20) <= *(int*)(e + 0x60)
            : *(int*)(e + 0x20) - 0xc000 <= *(int*)(e + 0x60);

        if (animating && !onFloor)
        {
            // Ghidra: LAB_0049edaa avec ecx non nul — rien a faire.
        }
        else if (onFloor && (*(uint*)(e + 0x90) & 0xa000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            if (animating && e[0x43] != 0) e[0x43] = 0;

            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }
        else if (!animating)
        {
            // Ghidra: LAB_0049ede5
            *(uint*)e = flags | 1;
        }

        // ---------------------------------------------------------------- l'interruption
        bool interrupted;

        if ((e[0x40] & 1) != 0)
        {
            byte* victim = *(byte**)(e + 0x6c);
            interrupted = (*(uint*)victim & 0x50002000) != 0
                       || *(short*)(victim + 0x12) != 0
                       || (*(uint*)(e + 0x90) & 0x2000ad82) != 0;
        }
        else
        {
            interrupted = (*(uint*)(e + 0x90) & 0x2000ad82) != 0;
        }

        if (interrupted)
        {
            // Ghidra: LAB_0049ee11
            *(uint*)e &= 0xfffffffd;
            e[0x42] = 1;
            e[0x43] = 1;
            e[0x40] &= 0xfe;
            *(int*)(e + 0x5c) = 0;
        }

        // Ghidra: LAB_0049ee28 — le clignotement de degat remet l'entite a l'attente.
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
    /// Ghidra: <c>0x0049ee4c..0x0049f0ab</c> — <b>le troisieme temps</b> de
    /// <see cref="TickSceneEntityAi_0049E920"/> : trois bandes de distance, un tirage sur cent
    /// dans chacune.
    ///
    /// <para>
    /// <b>Sous <c>0xfa</c></b> : <b>six fois sur dix</b> elle empoigne — a condition d'etre dans le
    /// cone, dont la largeur croit avec la timidite, et a portee tiree autour de <c>0x64</c> ;
    /// sinon elle erre. De 60 a 79 elle tient sa distance a <c>0x5a</c>, au-dela elle erre.
    /// </para>
    /// <para>
    /// <b>Entre <c>0xfa</c> et <c>0x1f4</c></b> : une fois sur deux elle tient sa distance, de 50 a
    /// 79 elle erre, au-dela elle se repose.
    /// </para>
    /// <para>
    /// <b>Au-dela de <c>0x1f4</c></b> : trois fois sur dix elle se repose, de 30 a 59 elle hesite,
    /// et le reste du temps elle part dans une direction <b>entierement tiree au sort</b> —
    /// <c>+0x10</c> recoit un <c>rand()</c> brut.
    /// </para>
    /// <para>
    /// <b>L'echec du cone ne mene pas a l'hesitation ici</b> mais a l'errance, contrairement a ses
    /// trois sœurs. Elle ne reste jamais sur place quand elle a decide d'attaquer et n'a pas pu.
    /// </para>
    /// </summary>
    private static void ReplanSceneEntityAction_0049E920(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        bool InCone() => ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;

        // ================================================================ le corps a corps
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x3c)
            {
                if (!InCone() || distance >= CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    WanderSceneEntity(e, boldness, timid, heading, 0);
                    return;
                }

                // Ghidra: LAB_0049ef17 — le geste s'arme, et la prise precedente est oubliee.
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(int*)(e + 0x18) = 3;
                e[0x40] &= 0xfe;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            if (roll < 0x50)
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

        // ================================================================ le lointain
        {
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                RestSceneEntity(e, timid);
                return;
            }

            if (roll < 0x3c)
            {
                HesitateSceneEntity(e);
                return;
            }

            e[0x42] = 3;
            e[0x43] = SceneEntityDelay(timid);
            *(ushort*)(e + 0x10) = (ushort)CrtRand();
        }
    }
}
