// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA CRAPOUILLE, LA GARGOUILLE ET LE CRAPAUD — 0x004ACC20, une seule routine pour trois especes, dans trois zones.
//
// Sous-type 6 de la vieille tour (zone 4, sous-zones 5, 8, 10) : la Crapouille. Copie
// 0x004CE120 au chateau de l'immortel (sous-type 5) : la Gargouille. Copie 0x004DB610 aux
// ruines souterraines (sous-type 0) : le Crapaud. C'est pourquoi ce fichier n'est sous
// aucun repertoire de zone.
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
    // 0x004ACC20 — LA CRAPOUILLE, LA GARGOUILLE ET LE CRAPAUD : une seule routine pour trois
    // especes. Sous-type 6 de la vieille tour (zone 4, sous-zones 5, 8, 10 : la Crapouille), copie
    // 0x004CE120 au chateau de l'immortel (sous-type 5 : la Gargouille), copie 0x004DB610 aux ruines
    // souterraines (sous-type 0 : le Crapaud). Transcrite le 2026-09-15 au desassembleur
    // (Legacy/Raw/Blaze/FUN_004ACC20.asm, 3 724 octets de code, absente de Blaze.c), table de sauts
    // a treize entrees a 0x004ADAAC.
    //
    // LES TROIS COPIES SONT IDENTIQUES instruction pour instruction, cibles d'appel mises a part,
    // et leurs chercheurs de proie (0x004AC860, 0x004CEFE0, 0x004DC4D0) sont conformes a
    // FUN_004A1020. Rien ne distingue les trois especes dans ce code : ce sont leurs blocs de
    // modele et leurs animations qui les separent.
    //
    // LA CHARPENTE DU GREMLIN SANS LE SORT : meme naissance sur +0x16, meme vol autour de +0x48,
    // meme ondulation (au pas de 0x32 et non 0x64), meme accroche en posture 0x0b, meme heurt qui ne
    // rend pas la hauteur. Ce qui lui appartient :
    //
    //   - UNE SEULE MORSURE, a l'image 8 de n'importe quel geste, clignotement 0x14.
    //   - LA POSTURE 10 : IL SE POSE. Il descend vers son plancher d'un trente-deuxieme de l'ecart
    //     par image — d'un seizieme sous trois unites —, puis, pose, laisse filer son rebours et, a
    //     portee de sa proie, peut s'elancer en pique (hardiesse chances sur 80). La variante 1 ne se
    //     pose jamais, la variante 0 ne s'elance jamais. Et un contact de la famille 0x20002000
    //     pendant la posture 10 rend la HAUTEUR d'avant l'image, seul cas ou ce corps la restaure.
    //   - LE PIQUE PART PLUS HAUT DANS SON ANIMATION (image 6) et sa duree suit la variante :
    //     (variante + 2) * 10 images.
    //   - SA POUSSEE DE PIQUE EST UN DECALAGE QUI RETOMBE TOUJOURS SUR 0x14000 :
    //     `0x14 << (((hardiesse & ~3) << 10) + 12)`, et les dix zeros bas du premier terme disparaissent
    //     sous le masque de cinq bits du decalage. Transcrit tel quel — l'operateur C# masque de la meme
    //     facon.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004acc20</c> (et ses deux copies) — <b>l'intelligence de la Crapouille, de la
    /// Gargouille et du Crapaud</b>.
    /// </summary>
    private static void TickSceneEntityAi_004ACC20(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x28] — la sonde, batie sur la pile : courte (0x50 de large) et longue
        // (0x78 devant).
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x14;
        probe[2] = 0x50;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x78;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 5, 0);   // Ghidra: FUN_004ac860, copie de FUN_004a1020
            *(int*)(e + 0x48) = *(int*)(e + 0x20);

            if (*(short*)(e + 0x16) >= 10)
            {
                *(short*)(e + 0x16) -= 10;
                *(uint*)e &= 0xffffffdf;
            }
            else
            {
                *(uint*)e |= 0x20;
            }
        }

        *(uint*)e &= 0xfffffffe;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideCrapouilleAction_004ACC20(e, probe, chased, boldness, distance, heading, speed);
            HoverCrapouille_004AD452(e, chased);
        }

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleCrapouilleFlight(e, boldness, &keep);

        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanCrapouilleAction_004ACC20(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004ad5b8..0x004ad716</c> — <b>la pose et le heurt</b> de la Crapouille, sortis
    /// en fonction le 2026-09-18 parce que la Harpie (<c>0x004CB4F0</c>, <c>Ai/ImmortalCastle/Harpy.cs</c>)
    /// porte la meme queue instruction pour instruction, masques et champs restaures compris.
    /// </summary>
    private static void SettleCrapouilleFlight(byte* e, int boldness, SceneEntityPlacement* keep)
    {
        uint contact = *(uint*)(e + 0x90);

        // Ghidra: 0x004ad5b8 — posee (posture 10), un contact de cette famille rend la hauteur
        // d'avant l'image et coupe la posture.
        if ((contact & 0x20002000) != 0 && e[0x42] == 0x0a)
        {
            e[0x43] = 0;
            *(int*)(e + 0x20) = keep->Y;
        }

        contact &= 0xfffff8ff;
        *(uint*)(e + 0x90) = contact;

        uint flags = *(uint*)e;
        if ((flags & 0x20) != 0) *(uint*)(e + 0x90) = contact & 0xdfffdfff;

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (*(uint*)(e + 0x90) & 0x8000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((*(uint*)(e + 0x90) & 0x2000a882) != 0)
        {
            // ================================================================ heurtee
            if (e[0x42] != 7)
            {
                e[0x42] = 7;
                int roll = CrtRand();                               // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(roll % 0x28 + 0x14);
            }

            *(uint*)e &= 0xfffffffd;

            // Les dix champs, sans la hauteur — comme le Gremlin.
            *(int*)(e + 0x24) = keep->Z;
            *(int*)(e + 0x7c) = keep->At7c;
            *(uint*)(e + 0x90) = keep->At90;
            *(int*)(e + 0x1c) = keep->X;
            *(int*)(e + 0x80) = keep->At80;
            *(int*)(e + 0x84) = keep->At84;
            *(int*)(e + 0x88) = keep->At88;
            *(int*)(e + 0x8c) = keep->At8c;
            *(int*)(e + 0x64) = keep->At64;
            *(int*)(e + 0x60) = keep->At60;

            int side = ProbeClearSide_004D5E60(e, 0x2000a882);
            if ((short)side == 0) side = (CrtRand() % 2) * 2 - 1;   // Ghidra: FUN_00547140

            *(ushort*)(e + 0x32) += (ushort)((boldness + 0x40) * side);
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004acd9c..0x004ad451</c> — <b>le premier temps</b> : l'animation, la morsure, la
    /// case de la posture. Treize cases dont huit servies : 0 attendre, 1 se tourner, 2 approcher,
    /// 3 filer vers un cap, 7 marcher droit, 10 se poser, 11 l'accroche, 12 le pique.
    /// </summary>
    private static void DecideCrapouilleAction_004ACC20(
        byte* e, short* probe, byte* chased, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                 // Ghidra: 0x004acd9c, en ligne

        // ================================================================ la morsure
        if ((*(uint*)e & 2) != 0)
        {
            if (*(short*)(e + 0x1a) == 8)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 8)
                {
                    *(ushort*)(prey + 0x96) = 0x14;
                    *(uint*)prey |= 0x40;

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                }
            }

            uint gesture = *(uint*)e;
            if ((gesture & 0x2000000) != 0 && e[0x42] != 0x0c)
            {
                *(uint*)e = gesture & 0xfffffffd;
                e[0x43] = 0;
            }
        }

        // ================================================================ la posture
        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(ushort*)(e + 0x12) = 1;
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
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
                    *(ushort*)(e + 0x12) = 1;
                    *(uint*)e |= 0x1000000;
                }

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                }
                else
                {
                    *(ushort*)(e + 0x32) +=
                        (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                }

                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;

                    // La variante accelere : 0x2000 de poussee par cran.
                    *(int*)(e + 0x5c) = (*(short*)(e + 0x16) << 0xd) + boldness * 0x90 + 0x7000;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                // Arrivee a portee : le rebours tombe, mais la poussee n'est PAS remise a zero.
                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x32 : -0x32 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            case 3:
                if (*(ushort*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = (*(short*)(e + 0x16) << 0xd) + boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(ushort*)(e + 0x32) += (ushort)((heading & 0xfff) < 0x800
                            ? boldness * 2 + 0x32
                            : -0x32 - boldness * 2);
                    }
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(ushort*)(e + 0x12) = 1;
                *(ushort*)(e + 0x18) = 0;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                return;

            // ---------------------------------------------------------- 7 : marcher droit
            case 7:
            {
                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 1)
                {
                    if (distance < *(short*)(e + 0x10)) e[0x43] = 0;
                    else if (e[0x43] != 0) e[0x43]--;
                }

                if (phase != 0) return;

                *(ushort*)(e + 0x18) = 0;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 10 : se poser
            case 10:
                CrapouilleLand_004AD155(e, boldness, distance);
                return;

            // ---------------------------------------------------------- 11 : l'accroche
            case 11:
            {
                int y = *(int*)(e + 0x20);
                int preyY = *(int*)(chased + 0x20);

                if (y + 0xb4000 < preyY)
                {
                    e[0x43] = 0;
                    *(uint*)e &= 0xfffffffd;
                    return;
                }

                *(int*)(e + 0x20) = y + ((preyY - y + 0x28000) >> 2);

                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                if (phase != 0) return;

                *(ushort*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 12 : le pique
            case 12:
                CrapouilleDive_004AD2D3(e, chased, boldness, heading);
                return;

            // ---------------------------------------------------------- 4, 5, 6, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004ad155..0x004ad26c</c> — <b>se poser</b>, posture 10, en deux phases.
    ///
    /// <para><b>Phase 1, la descente</b> : la hauteur rejoint le plancher d'un trente-deuxieme de
    /// l'ecart par image (<c>sar 5</c>), et du double quand l'ecart passe sous trois unites, pour ne
    /// pas trainer a la fin. A moins de dix unites, il est pose. La variante 1 refuse et rend la main
    /// tout de suite.</para>
    ///
    /// <para><b>Phase 2, pose</b> : l'animation est tenue pour finie, le rebours descend — et
    /// <b>descend sous zero s'il y etait deja</b>, le <c>dec al</c> du binaire n'etant garde que par
    /// son propre resultat. A moins de 0x50 de sa proie, la variante 0 rend la main ; les autres
    /// s'elancent en pique avec hardiesse chances sur 80, et renoncent sinon.</para>
    /// </summary>
    private static void CrapouilleLand_004AD155(byte* e, int boldness, int distance)
    {
        if (*(ushort*)(e + 0x12) == 0)
        {
            *(uint*)e &= 0xfffffffd;
            *(ushort*)(e + 0x18) = 1;
            *(ushort*)(e + 0x1a) = 0;
            *(ushort*)(e + 0x12) = 1;
        }

        if (*(ushort*)(e + 0x12) == 1)
        {
            if (*(short*)(e + 0x16) == 1)
            {
                e[0x43] = 0;
                return;
            }

            *(uint*)e |= 0x1000000;

            int floor = *(int*)(e + 0x60);
            int y = *(int*)(e + 0x20);
            int step = (floor - y) >> 5;

            *(int*)(e + 0x5c) = 0;
            *(ushort*)(e + 0x18) = 1;

            int magnitude = step < 0 ? -step : step;
            if ((magnitude & unchecked((int)0xfffff000)) < 0x3000) step <<= 1;

            y += step;
            *(int*)(e + 0x20) = y;

            int gap = (floor - y) >> 0xc;
            if (gap < 0) gap = -gap;

            if (gap < 10)
            {
                *(ushort*)(e + 0x12) = 2;
                *(ushort*)(e + 0x18) = 0;
                *(ushort*)(e + 0x1a) = 0;
            }
        }

        if (*(ushort*)(e + 0x12) != 2) return;

        *(uint*)e |= 0x2000000;
        *(ushort*)(e + 0x18) = 0;
        e[0x43]--;                                                  // `dec al`, sans garde a zero

        if (distance >= 0x50) return;

        if (*(short*)(e + 0x16) == 0)
        {
            e[0x43] = 0;
            return;
        }

        if (CrtRand() % 0x50 >= boldness)                           // Ghidra: FUN_00547140
        {
            e[0x43] = 0;
            return;
        }

        e[0x42] = 0x0c;
        *(ushort*)(e + 0x12) = 2;
        *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
        *(ushort*)(e + 0x10) = 0;
        *(ushort*)(e + 0x18) = 3;
        *(ushort*)(e + 0x1a) = 6;
    }

    /// <summary>
    /// Ghidra: <c>0x004ad2d3..0x004ad44f</c> — <b>le pique</b>, posture 12. La forme du pique du
    /// Gremlin, a quatre ecarts pres : la montee ne renonce jamais, la plongee part a l'image 6,
    /// elle vise la hauteur de la proie <b>plus 0x14</b> (et non 0x28) et renonce plus de 0xb4 sous
    /// elle, et elle dure <c>(variante + 2) * 10</c> images. Les virages prennent la hardiesse une
    /// fois.
    ///
    /// <para><b>La poussee de plongee.</b> Le binaire charge 0x14 dans EAX et le decale de
    /// <c>((hardiesse &amp; ~3) &lt;&lt; 10) + 12</c> par <c>shl eax,cl</c> : le compte est masque
    /// sur cinq bits, les dix bits bas du premier terme sont nuls, et il reste 12. La poussee vaut donc
    /// toujours 0x14000, quelle que soit la hardiesse.</para>
    /// </summary>
    private static void CrapouilleDive_004AD2D3(byte* e, byte* chased, int boldness, int heading)
    {
        if (*(ushort*)(e + 0x12) == 1)
        {
            int y = *(int*)(e + 0x20) + 0x3000;
            *(int*)(e + 0x20) = y;

            if ((y - *(int*)(e + 0x60)) >> 0xc > 0xc8)
            {
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                *(ushort*)(e + 0x12) = 2;
                *(ushort*)(e + 0x10) = 0;
                *(ushort*)(e + 0x18) = 3;
                *(ushort*)(e + 0x1a) = 6;
            }

            TurnCrapouilleTowardPrey(e, boldness, heading);
        }

        if (*(ushort*)(e + 0x12) == 2)
        {
            *(int*)(e + 0x5c) = 0x14 << (((boldness & ~3) << 0xa) + 0xc);

            int y = *(int*)(e + 0x20);
            int preyY = *(int*)(chased + 0x20);

            if (y + 0xb4000 < preyY)
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffd;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            *(int*)(e + 0x20) = y + ((preyY - y + 0x14000) >> 2);

            (*(short*)(e + 0x10))++;
            short frames = *(short*)(e + 0x10);

            TurnCrapouilleTowardPrey(e, boldness, heading);

            if (frames >= (*(short*)(e + 0x16) + 2) * 10)
            {
                *(ushort*)(e + 0x12) = 3;
                *(uint*)e &= 0xfffffffd;
            }
        }

        ushort phase = *(ushort*)(e + 0x12);

        if (phase == 3)
        {
            int v = *(int*)(e + 0x5c);
            v -= v >> 3;
            *(int*)(e + 0x5c) = v;
            if ((v & unchecked((int)0xfffff000)) < 0xa000) e[0x43] = 0;
        }

        if (phase != 0) return;

        *(ushort*)(e + 0x1a) = 0;
        *(ushort*)(e + 0x12) = 1;
        *(ushort*)(e + 0x18) = 1;
        *(int*)(e + 0x5c) = 0;
        *(uint*)e |= 0x1000000;
    }

    /// <summary>Ghidra: le virage du pique de la Crapouille — hors d'un cone de <c>±0x100</c>,
    /// <c>hardiesse + 0x32</c> vers la proie.</summary>
    private static void TurnCrapouilleTowardPrey(byte* e, int boldness, int heading)
    {
        if (((heading + 0x100) & 0xfff) <= 0x200) return;

        *(ushort*)(e + 0x32) +=
            (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
    }

    /// <summary>
    /// Ghidra: <c>0x004ad452..0x004ad517</c> — <b>le vol stationnaire</b>, celui du Gremlin a deux
    /// ecarts pres : l'ondulation avance de <c>hasard % 50 + 0x32</c> et non de <c>+ 0x64</c>, et elle
    /// est suspendue pendant la preparation d'un pique <b>et</b> pendant la posture 10 hors geste.
    /// </summary>
    private static void HoverCrapouille_004AD452(byte* e, byte* chased)
    {
        uint flags = *(uint*)e;
        byte posture = e[0x42];
        bool bobbing = (posture != 0x0c && posture != 0x0a) || (flags & 2) != 0;

        if ((flags & 0x20) != 0)
        {
            if (bobbing)
            {
                int wave = (int)Winsys3d.GetSinTableEntry(AdvanceCrapouilleBob(e));
                int y = *(int*)(e + 0x20);
                int target = *(int*)(e + 0x48) + (wave + 0x3000) * 60;
                *(int*)(e + 0x20) = y + ((target - y) >> 3);
            }

            if (((*(int*)(chased + 0x20) ^ *(int*)(chased + 0x64)) & unchecked((int)0xfffff000)) == 0)
            {
                *(int*)(e + 0x48) = *(int*)(chased + 0x64);
            }

            return;
        }

        if (!bobbing) return;

        int groundWave = (int)Winsys3d.GetSinTableEntry(AdvanceCrapouilleBob(e));
        int groundY = *(int*)(e + 0x20);
        *(int*)(e + 0x20) = groundY + ((*(int*)(e + 0x60) + groundWave * 60 - groundY + 0x8c000) >> 3);
    }

    /// <summary>Ghidra: <c>add dx,[esi+14h] / add edx,ecx</c>, ou ECX vaut encore le diviseur
    /// 0x32 — la phase avance de <c>hasard % 50 + 50</c>.</summary>
    private static short AdvanceCrapouilleBob(byte* e)
    {
        int roll = CrtRand() % 0x32;                                // Ghidra: FUN_00547140
        short phase = (short)((roll + *(short*)(e + 0x14) + 0x32) & 0xfff);
        *(short*)(e + 0x14) = phase;
        return phase;
    }

    /// <summary>
    /// Ghidra: <c>0x004ad717..0x004ada9e</c> — <b>le troisieme temps</b>. Trois plages exclusives
    /// bornees a 0x96 et 0x258, comme le Gremlin, mais <b>la variante oriente les choix</b> : la
    /// variante 0 se pose (posture 10) la ou les autres piquent, et la variante 1 approche ou pique
    /// la ou les autres filent. Aucune sortie n'est « attendre » a la mi-distance ou au contact pour
    /// une variante non nulle.
    /// </summary>
    private static void ReplanCrapouilleAction_004ACC20(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        *(ushort*)(e + 0x12) = 0;

        // ================================================================ le contact
        if (distance < 0x96)
        {
            int roll = CrtRand() % 100;                             // Ghidra: FUN_00547140

            if (roll < 0x32)
            {
                bool inCone = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;

                if (!inCone || distance >= CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
                else if (roll < 0x1e)
                {
                    // Ghidra: 0x004ad7c2 — armer L'ACCROCHE.
                    uint flags = *(uint*)e;
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (flags & 0xfeffffff) | 2;
                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                }
                else
                {
                    e[0x42] = 0;
                    e[0x43] = (byte)(CrtRand() % 0x28);             // Ghidra: FUN_00547140
                }
            }
            else if (roll < 0x46)
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);  // Ghidra: FUN_00547140
            }
            else if (100 - roll < (boldness >> 1) + 0x0a && *(short*)(e + 0x16) == 0)
            {
                CrapouilleSettle(e);
            }
            else
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
            }

            return;
        }

        // ================================================================ la mi-distance
        if (distance < 0x258)
        {
            int roll = CrtRand() % 100;                             // Ghidra: FUN_00547140

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) < 0x800)
                {
                    e[0x42] = 0x0c;
                    e[0x43] = 1;
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x28 || (roll < 0x50 && *(short*)(e + 0x16) == 1 && roll < 0x3c))
            {
                e[0x42] = 2;
                e[0x43] = SceneEntityDelay(timid);
                *(ushort*)(e + 0x10) = 0x50;
            }
            else if (roll < 0x50)
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);  // Ghidra: FUN_00547140
            }
            else if (100 - roll < (boldness >> 1) + 0x0a)
            {
                if (*(short*)(e + 0x16) == 0)
                {
                    CrapouilleSettle(e);
                }
                else
                {
                    e[0x42] = 0x0c;
                    e[0x43] = 1;
                }
            }
            else
            {
                e[0x42] = 0;
                e[0x43] = (byte)(CrtRand() % 0x28);                 // Ghidra: FUN_00547140
            }

            return;
        }

        // ================================================================ le lointain
        int far = CrtRand() % 100;                                  // Ghidra: FUN_00547140

        if (far < 0x32 && far >= boldness * 2 + 0x0a)
        {
            // Ghidra: 0x004ad9e7 — l'attente, au rebours court.
            e[0x42] = 0;
            int wait = CrtRand() % 10;                              // Ghidra: FUN_00547140
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = (byte)(wait + (byte)(timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far >= 0x32 && far < 0x4b)
        {
            if (*(short*)(e + 0x16) == 1)
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
                return;
            }

            e[0x42] = 3;
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = SceneEntityDelay(timid);
            *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);      // Ghidra: FUN_00547140
            return;
        }

        // Ghidra: LAB_004ada7e — approcher de loin, au rebours le plus long : + 0x1e au lieu de 0x14.
        e[0x42] = 2;
        int delay = CrtRand() % 10;                                 // Ghidra: FUN_00547140
        *(ushort*)(e + 0x10) = 0x64;
        e[0x43] = (byte)(delay + (byte)(timid >> 1) + 0x1e);
    }

    /// <summary>Ghidra: <c>0x004ad86b</c> et <c>0x004ad97b</c> — armer la posture 10 : se poser,
    /// pour <c>hasard % 30 + 20</c> images.</summary>
    private static void CrapouilleSettle(byte* e)
    {
        e[0x42] = 0x0a;
        int roll = CrtRand();                                       // Ghidra: FUN_00547140
        *(ushort*)(e + 0x12) = 0;
        e[0x43] = (byte)(roll % 0x1e + 0x14);
    }
}
