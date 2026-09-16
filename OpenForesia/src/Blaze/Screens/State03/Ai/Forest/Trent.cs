// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE TRENT — 0x0049FAD0, sous-type 10 du bloc B de la foret (zone 3, sous-zones 6 a 8).
//
// LANCEUR DE SORTS - l'un des quatre du depot, avec le Behemoth, le Varan et le sous-type
// 1 de l'auberge. Tous quatre partagent le meme bloc d'incantation, recopie et non reecrit.
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
    // 0x0049FAD0 — LE TRENT, sous-type 10 du bloc B de la foret (sous-zones 6 a 8). Transcrite le
    // 2026-09-01 au desassembleur (Legacy/Raw/Blaze/FUN_0049FAD0.asm, 2 437 octets, absente de
    // Blaze.c). Quarante-sept etiquettes, et DEUX tables de sauts a quatre entrees, 0x004A0458 et
    // 0x004A0468.
    //
    // LE CRIBLE L'ANNONCAIT A 454 LIGNES DU GOBLIN-SORCIER, ET C'ETAIT LE BON INDICE : c'est la
    // SECONDE bete du depot qui lance un sort, apres le Behemoth, et elle le fait par le meme
    // mecanisme exactement — le bit 0x800 des drapeaux bascule sur une seconde machine a postures
    // lue dans +0x10, quatre etats comptes de 3 vers 0, la duree de l'incantation prise dans la
    // fiche de sort elle-meme, et le meme masque de traits 0x400003e8 qui l'annule.
    //
    // DEUX ROUTINES ECRITES A DEUX JOURS D'INTERVALLE PARTAGENT DONC UN MECANISME ENTIER. Ce n'est
    // pas une famille de hasard : le Behemoth est en bloc C, le Trent en bloc B, et leurs deux
    // tables de sorts sont distinctes (0x00552A30 contre 0x005529A8) mais de meme forme et de meme
    // longueur.
    //
    // CE QUI LUI EST PROPRE :
    //
    //   - SA CHARGE NE FRAPPE QU'EN MOUVEMENT. Le geste de la posture 1 mesure sa propre vitesse a
    //     plat et n'ouvre le balayage que si elle depasse 6 ; il se ferme quand l'animation est
    //     finie ET que la vitesse est retombee sous hardiesse/8 + 1. C'est un elan, pas un coup.
    //
    //   - SON COUP GELE L'IMAGE EN L'AIR. Le geste de la posture 0 decremente +0x1a a chaque image
    //     ou le bit 0 est pose, ce qui annule exactement l'avance de AdvanceEntityAnimation : le
    //     geste attend d'avoir touche terre.
    //
    //   - ELLE SAUTE, ET LA HARDIESSE DECIDE. Sa posture 3 tire hasard % 100 contre hardiesse / 8 :
    //     sous le seuil elle BONDIT (+0x54 augmente de 0x10000, bit 0 pose), sinon elle marche. Un
    //     Trent hardi saute donc jusqu'a huit fois sur cent, un Trent timide jamais.
    //
    //   - ET ELLE N'A PAS DE GRAVITE PROPRE. Aucune ecriture dans +0x9a, la ou le Grizzly pose
    //     0x6000 et le Behemoth 0x7fff. Elle retombe de ce que la simulation lui donne.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00552990</c> et <c>0x0055299c</c> — <b>les deux gabarits de portee du
    /// Trent</b>, releves dans <c>.data</c>.
    ///
    /// <para>
    /// <b>Ils ferment <see cref="s_SpellGestures_00552978"/> par le haut.</b> Le releve du
    /// 2026-08-29 notait que la donnee « change de forme » apres les douze mots de cette table, et
    /// qu'on y lisait des triplets <c>0x14 / 0x3c / 0x64</c> ressemblant a des seuils de
    /// pourcentage. Ces trois nombres sont les termes <c>[1]</c>, <c>[2]</c> et <c>[3]</c> du
    /// gabarit ci-dessous : la table des gestes de sort tient donc bien en douze mots exactement,
    /// et ce qui la suit est une sonde.
    /// </para>
    /// </summary>
    private static readonly short[] s_TrentProbes_00552990 =
    [
        0x0000, 0x0014, 0x003c, 0x0064, 0x0000, 0x0050,   // 0x00552990 — le coup
        0x0000, 0x0028, 0x0032, 0x0064, 0x0000, 0x0046,   // 0x0055299c — la charge
    ];

    private static short* s_TrentProbeStorage;

    /// <summary>Une des deux entrees de <see cref="s_TrentProbes_00552990"/> en memoire native,
    /// puisque <see cref="SweepPartyForMeleeHit_00519C30"/> la recoit par pointeur.</summary>
    private static short* TrentProbe(int row)
    {
        if (s_TrentProbeStorage == null)
        {
            s_TrentProbeStorage =
                (short*)NativeMemory.Alloc((nuint)s_TrentProbes_00552990.Length, sizeof(short));

            for (int i = 0; i < s_TrentProbes_00552990.Length; i++)
            {
                s_TrentProbeStorage[i] = s_TrentProbes_00552990[i];
            }
        }

        return s_TrentProbeStorage + row * 6;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005529a8</c> — <b>les trois sorts du Trent</b> : <c>0x0003</c>,
    /// <c>0x000d</c>, <c>0x0013</c>, suivis d'un mot nul.
    ///
    /// <para>
    /// Meme forme et meme longueur que <see cref="s_BehemothSpells_00552A30"/>, et bornee de la
    /// meme facon deux fois — le corps tire <c>hasard % 3</c>, et le quatrieme mot est nul. Les
    /// trois sont de la <b>famille 0</b>, la premiere ecole, aux indices <b>3, 13 et 19</b>.
    /// </para>
    /// <para>
    /// <b>Les deux tables se recouvrent partiellement</b> : le Behemoth tire 13, 19 ou 23, le Trent
    /// 3, 13 ou 19. Deux sorts sur trois leur sont communs.
    /// </para>
    /// </summary>
    private static readonly ushort[] s_TrentSpells_005529A8 = [0x0003, 0x000d, 0x0013];

    /// <summary>
    /// Ghidra: <c>0x0049fad0</c> — <b>l'intelligence du Trent</b>, sous-type 10 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 6 a 8 de la foret. <b>Seconde bete du depot
    /// qui lance un sort</b>, apres le Behemoth.
    ///
    /// <para>
    /// <b>Deux machines a postures</b>, comme lui, et le meme bit <c>0x800</c> pour choisir :
    /// degage, quatre postures lues dans <c>+0x42</c> (table <c>0x004A0468</c>) ; pose, quatre
    /// etats d'incantation lus dans <c>+0x10</c> (table <c>0x004A0458</c>).
    /// </para>
    /// <para>
    /// <b>Elle n'ecrit jamais <c>+0x9a</c></b> : pas de gravite propre, contrairement au Grizzly, au
    /// Mechant ours, au Geant et au Behemoth.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049FAD0(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideChargingCasterAction_0049FAD0(e, model, boldness, distance, heading, speed, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanChargingCasterAction_0049FAD0(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049fb37..0x004a014d</c> — <b>le premier temps</b> du Trent.
    /// </summary>
    private static void DecideChargingCasterAction_0049FAD0(
        byte* e, byte* model, int boldness, int distance, int heading, int speed, uint traits)
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

            switch (e[0x42])
            {
                case 0: TrentSwipeGesture_0049FD33(e, flags); return;
                case 1: TrentChargeGesture_0049FB72(e, model, boldness); return;
                default: return;
            }
        }

        if ((flags & 0x10) != 0) return;

        // ================================================================ l'incantation
        if ((flags & 0x804) != 0)
        {
            AdvanceTrentIncantation_0049FDDF(e, boldness, heading, traits, flags);
            return;
        }

        // ================================================================ la posture tenue
        if ((flags & 1) != 0) return;

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

                if (e[0x43] != 0)
                {
                    *(int*)(e + 0x5c) = 0;
                    e[0x43]--;
                    return;
                }

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

            // ---------------------------------------------------------- 2 : tenir la distance
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

            // ---------------------------------------------------------- 3 : marcher, ou bondir
            default:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                short course = *(short*)(e + 0x10);

                // Ghidra: LAB_004a006a — LE SAUT, et c'est la HARDIESSE qui en decide la frequence :
                // hasard % 100 contre hardiesse / 8, donc huit chances sur cent au plus.
                if (CrtRand() % 100 < boldness >> 3)
                {
                    int leap = (boldness + 0x100) << 7;
                    *(int*)(e + 0x5c) = leap;

                    *(int*)(e + 0x50) +=
                        ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                    *(int*)(e + 0x58) +=
                        (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;

                    // +0x54 est AUGMENTE, pas pose — et +0x5c reste charge.
                    *(int*)(e + 0x54) += 0x10000;
                    *(uint*)e |= 1;
                    return;
                }

                // Ghidra: LAB_004a00d5 — la marche : la position avance le long de +0x10, puis le
                // corps se tourne vers la cible.
                int stride = (boldness + 0x80) << 7;
                *(int*)(e + 0x5c) = stride;

                *(int*)(e + 0x1c) +=
                    ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x24) +=
                    (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0;

                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0049fd33..0x0049fdb6</c> — <b>le coup</b>, le geste de la posture 0.
    ///
    /// <para>
    /// Il balaie a partir de la treizieme image, et <b>gele son animation tant qu'il est en
    /// l'air</b> : chaque image ou le bit 0 est pose, <c>+0x1a</c> est decremente, ce qui annule
    /// exactement l'avance que <see cref="AdvanceEntityAnimation_004DB5A0"/> vient de faire. Le
    /// geste ne se ferme qu'une fois au sol et l'animation finie.
    /// </para>
    /// <para>
    /// <b>Les drapeaux sont relus apres le balayage, et pas avant</b> : quand l'image est
    /// anterieure a la treizieme, le corps travaille sur la valeur lue en tete de la decision.
    /// La distinction est sans effet aujourd'hui, mais elle est dans le desassemblage.
    /// </para>
    /// </summary>
    private static void TrentSwipeGesture_0049FD33(byte* e, uint flags)
    {
        if (*(short*)(e + 0x1a) >= 0x0d)
        {
            byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), TrentProbe(0), 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(victim + 0x96) = 0x10;
                *(uint*)victim |= 0x40;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
            }

            flags = *(uint*)e;
            if ((flags & 1) != 0) *(short*)(e + 0x1a) -= 1;
        }

        if ((flags & 1) != 0) return;

        if ((flags & 0x2000000) == 0)
        {
            *(int*)(e + 0x5c) = 0;
            return;
        }

        e[0x43] = 0;
        *(uint*)e = flags & 0xfffffffd;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0049fb72..0x0049fd32</c> — <b>la charge</b>, le geste de la posture 1.
    ///
    /// <para>
    /// <b>Elle ne frappe qu'en mouvement.</b> Le corps mesure sa propre vitesse a plat — la racine
    /// de <c>(+0x50 &gt;&gt; 12)² + (+0x58 &gt;&gt; 12)²</c> — et le balayage n'a lieu que si elle
    /// depasse <b>6</b>, apres la dix-septieme image. Le geste se ferme quand l'animation est finie
    /// <b>et</b> que cette vitesse est retombee sous <c>hardiesse / 8 + 1</c> : le meme verrou que
    /// la ruee de l'abeille tueuse, mais sur un geste de contact.
    /// </para>
    /// <para>
    /// <b>L'impulsion arrive a la seizieme image</b>, une seule : <c>(hardiesse + 0x360) &lt;&lt; 7</c>
    /// verse dans les deux composantes de vitesse. C'est la plus forte poussee horizontale de la
    /// famille.
    /// </para>
    /// <para>
    /// La prise, la moitie d'attaque du bloc de modele, la projection a trente fois le sinus et le
    /// detour par la position de la victime pour y semer l'effet <c>0x39</c> sont ceux du Grizzly,
    /// au mot pres.
    /// </para>
    /// </summary>
    private static void TrentChargeGesture_0049FB72(byte* e, byte* model, int boldness)
    {
        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;
        int rush = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));

        uint flags = *(uint*)e;

        if ((flags & 0x2000000) != 0 && rush <= (boldness >> 3) + 1)
        {
            *(uint*)e = flags & 0xffffffdd;              // le geste ET la prise
            return;
        }

        if (*(short*)(e + 0x1a) >= 0x11 && rush >= 6 && (flags & 0x20) == 0)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), TrentProbe(1), 0);
            if (prey != null) *(uint*)e |= 0x20;

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(prey + 0x96) = 0x10;
                *(uint*)prey |= 0x40;

                short power = *(short*)(model + 0x30);
                *(short*)(model + 0x30) = (short)(power >> 1);
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                *(short*)(model + 0x30) = power;

                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

                if ((*(uint*)prey & 0x40000000) == 0)
                {
                    int impact = *(short*)(e + 0x32);
                    *(int*)(prey + 0x50) = (int)Winsys3d.GetSinTableEntry(impact) * 30;
                    *(int*)(prey + 0x58) = Winsys3d.GetCosTableEntry(impact) * 30;

                    uint preyFlags = *(uint*)prey;
                    if ((preyFlags & 1) == 0)
                    {
                        *(int*)(prey + 0x54) = 0x18000;
                        *(uint*)prey = preyFlags | 1;
                    }

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
        }

        // Ghidra: LAB_0049fcdc — l'impulsion, a la seizieme image et une seule fois.
        if (*(short*)(e + 0x1a) == 0x10)
        {
            *(int*)(e + 0x5c) = (boldness + 0x360) << 7;

            int course = *(short*)(e + 0x32);
            *(int*)(e + 0x50) +=
                ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
            *(int*)(e + 0x58) +=
                (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
        }

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x0049fdca..0x0049ff12</c> — <b>l'incantation du Trent</b>, la seconde machine a
    /// postures.
    ///
    /// <para>
    /// <b>C'est le mecanisme du Behemoth</b> (<see cref="AdvanceIncantation_004A5919"/>), etat pour
    /// etat : le compteur <c>+0x10</c> descend de 3 a 0, l'etat 3 fait naitre l'entite de lancement
    /// <c>0xbd</c> et tire un sort dont la <b>fiche</b> donne la duree (<c>+0x13</c>), l'etat 2
    /// ecoule le rebours en se tournant vers la cible, l'etat 1 lance, l'etat 0 efface le bit
    /// <c>0x800</c>. Le meme masque de traits <c>0x400003e8</c> annule tout avant le dispatch.
    /// </para>
    /// <para>
    /// <b>Deux constantes les separent</b> : l'animation de lancement est <c>6</c> ici et <c>7</c>
    /// chez le Behemoth, et le sort part a la <b>dix-septieme</b> image et non a la quatorzieme.
    /// </para>
    /// </summary>
    private static void AdvanceTrentIncantation_0049FDDF(
        byte* e, int boldness, int heading, uint traits, uint flags)
    {
        if ((traits & 0x400003e8) != 0)
        {
            flags &= 0xfffff7fb;
            e[0x43] = 0;
            *(uint*)e = flags;
        }

        short state = *(short*)(e + 0x10);
        if ((uint)state > 3) return;

        switch (state)
        {
            // ---------------------------------------------------------- 0 : rendre la main
            case 0:
                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffff7ff;
                return;

            // ---------------------------------------------------------- 1 : lancer
            case 1:
                if (*(ushort*)(e + 0x18) != 6)
                {
                    *(int*)(e + 0x18) = 6;
                    *(uint*)e = *(uint*)e & 0xfefffffb;
                    return;
                }

                if (*(short*)(e + 0x1a) != 0x11) return;

                {
                    ushort sheet = *(ushort*)(e + 6);
                    CastSpell_0053A980((uint*)e, (sheet >> 8) & 0xff, sheet & 0xff, -1, -1);
                }

                *(short*)(e + 0x10) -= 1;
                return;

            // ---------------------------------------------------------- 2 : incanter
            case 2:
                if (--e[0x43] == 0) *(short*)(e + 0x10) -= 1;

                TurnSceneEntityTowardTarget(e, boldness * 4 + 0x32, heading);
                return;

            // ---------------------------------------------------------- 3 : choisir le sort
            default:
            {
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                ushort sheet = s_TrentSpells_005529A8[CrtRand() % 3];
                *(ushort*)(e + 6) = sheet;

                byte* family = ((byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)))
                                   [(sheet >> 8) & 0xff];

                e[0x43] = family[(sheet & 0xff) * 0x30 + 0x13];

                *(short*)(e + 0x10) -= 1;
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a019f..0x004a044c</c> — <b>le troisieme temps</b> du Trent.
    ///
    /// <para>
    /// <b>Trois plages, et le meme cone d'engagement partout</b> : <c>timidite * 4 + 0x100</c>
    /// contre <c>timidite * 8 + 0x200</c>, aux quatre sites qui engagent. Sous <c>0xfa</c> elle
    /// frappe, entre <c>0xfa</c> et <c>0x1f4</c> elle incante ou elle charge, au-dela elle incante
    /// ou elle s'en va.
    /// </para>
    /// <para>
    /// <b>L'incantation est son recours de loin</b> : c'est la seule chose qu'elle sache faire
    /// au-dela de <c>0x1f4</c>, et elle y a une chance sur trois d'y penser.
    /// </para>
    /// </summary>
    private static void ReplanChargingCasterAction_0049FAD0(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        bool inCone = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;

        // ================================================================ le corps a corps
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                if (!inCone)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                if (distance >= 0x64 + CrtRand() % (timid * 2 + 1))
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Ghidra: LAB_004a023f — armer LE COUP : posture 0, animation 3.
                uint flags = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                e[0x42] = 0;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (flags & 0xfeffffff) | 2;
                return;
            }

            if (roll >= 0x3c)
            {
                WanderSceneEntity(e, boldness, timid, heading, 0x800);
                return;
            }

            HoldSceneEntityAtRange(e, timid, 0x64);
            return;
        }

        // ================================================================ la distance moyenne
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                if (!inCone)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                if (roll < 0x1e)
                {
                    ArmTrentIncantation(e);
                    return;
                }

                // Ghidra: LAB_004a0305 — armer LA CHARGE : posture 1, animation 4. Aucun compte a
                // rebours : c'est la vitesse qui ferme le geste.
                uint flags = *(uint*)e;
                *(int*)(e + 0x18) = 4;
                e[0x42] = 1;
                *(uint*)e = (flags & 0xfeffffff) | 2;
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

        // ================================================================ le lointain
        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            if (!inCone)
            {
                HesitateSceneEntity(e);
                return;
            }

            ArmTrentIncantation(e);
            return;
        }

        if (far < 0x3c)
        {
            RestSceneEntity(e, timid);
            return;
        }

        // Ghidra: LAB_004a0429 — trop loin pour que la cible compte : le cap est entierement tire.
        e[0x42] = 3;
        e[0x43] = SceneEntityDelay(timid);
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }

    /// <summary>Ghidra: LAB_004a03d3 — <b>armer l'incantation</b> : l'animation 5, le bit
    /// <c>0x800</c> qui bascule la machine a postures, le bit de geste, le bit de bouclage, et
    /// <c>+0x10</c> mis a 3. Les deux sites qui incantent partagent ce corps.
    ///
    /// <para>Le Behemoth arme la sienne avec l'animation <b>6</b> ; c'est la seule difference entre
    /// les deux armements.</para></summary>
    private static void ArmTrentIncantation(byte* e)
    {
        uint flags = *(uint*)e;
        *(int*)(e + 0x18) = 5;
        *(uint*)e = flags | 0x1000804;
        *(ushort*)(e + 0x10) = 3;
    }
}
