// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE SCORPION — 0x004921A0, sous-type 5 du bloc C de la mine (zone 1, sous-zones 3 et 4).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1 : sous-zone 3 modele 0 et sous-zone 4
// modele 0, « Scorpion », 44 PV, 6 PM, rang 25). 1 011 instructions, absentes de Blaze.c ; releve
// Legacy/Raw/Blaze/FUN_004921A0.asm, table de sauts a TREIZE entrees a 0x00492DC0.
//
// C'EST LA CHARPENTE DU VARAN, celle que la Salamandre et le Gremlin portent aussi : placement
// sauvegarde puis annule au heurt, integration ecrite EN LIGNE, treize postures dont six vides,
// +0x12 comme phase d'entree de chaque posture, et une replanification en trois bandes cascadees.
// Le Scorpion, l'Araignee (0x00497370) et le Mille-patte (0x00491650) en sont les trois variantes
// de la mine, et ce fichier est le plus complet des trois : les deux autres se lisent par
// difference.
//
// CE QUI LUI EST PROPRE :
//
//   - IL A DEUX ARMES, ET LA SECONDE EMPOISONNE DE DEUX FACONS. La posture 0x0b est la PINCE, qui
//     ne fait que des degats a l'image 11. La posture 0x0c est le DARD, a l'image 10 : apres les
//     degats il tire a pile ou face sur trois — un tiers l'effet 7 (0x0053b3c0), deux tiers l'effet
//     de piege (0x0053c420). Aucun autre corps du depot ne tire son effet d'etat au sort.
//   - IL BONDIT (posture 6), d'une impulsion unique de (hardiesse + 0x100) << 6 et 0x1e000 de haut,
//     et il reste en posture 6 tant qu'il est en l'air. C'est le bond du Varan, au mot pres.
//   - SES TROIS FACONS DE TOURNER N'ONT PAS LE MEME PAS : la posture 1 tourne de hardiesse + 0x3c,
//     la posture 2 de hardiesse * 2 + 0x28, la posture 3 de hardiesse * 8 + 0x28. Plus il court,
//     plus il vire sec.
//   - HEURTE EN L'AIR, IL TOMBE A PLAT : la branche aerienne du heurt remet a zero ses trois
//     vitesses, le repose ou il etait, et le met en attente pour quarante images avec +0x2c a
//     0x1000. Le Gremlin et la Salamandre n'ont pas cette branche — ils ne bondissent pas.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_00492e00 @ 0x00492e00 — <b>l'effet 7, s'il est permis</b> : le filtre de
    /// <see cref="RejectStatusTarget_004A2C30"/>, puis <see cref="ApplyStatusEffect_0053B3C0"/>.
    ///
    /// <para>
    /// La sixieme enveloppe de ce filtre, et la seule que <see cref="RejectStatusTarget_004A2C30"/>
    /// ne recensait pas : les cinq connues portent l'effet de piege ou l'un des effets d'etat du
    /// chateau. Le Scorpion est le seul a en tirer une <b>au sort</b> contre
    /// <see cref="ApplyTrapEffectIfAllowed_004A2BF0"/>.
    /// </para>
    /// </summary>
    private static uint ApplyStatusEffectIfAllowed_00492E00(uint* caster, uint* victim, int keep)
    {
        if (RejectStatusTarget_004A2C30(caster, victim)) return 1;

        ApplyStatusEffect_0053B3C0(caster, victim, keep);
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004921a0</c> — <b>l'intelligence du Scorpion</b>, sous-type 5 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 3 et 4 de la mine.
    ///
    /// <para>
    /// <b>Sa distance est a TROIS dimensions</b> — <c>dx² + dy² + dz²</c> — la ou la famille
    /// terrestre et la Salamandre l'aplatissent. Celle de la replanification, en revanche, est a
    /// deux : il decide de loin comme les autres, mais il juge de pres en tenant compte de la
    /// hauteur.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004921A0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — le gabarit de portee, construit sur la pile a chaque image, comme
        // chez la Salamandre. Deux mots sont poses avant le quatrieme push et reecrits ensuite.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x64;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            // Ghidra: FUN_00491290, copie de FUN_004a1020. Mode 0 : aucun critere, donc le
            // chercheur retombe sur « n'importe qui de vivant ».
            PickChasedPartyMember_004A1020(e, 0, 0);
        }

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);
        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // La distance est recalculee — et EN TROIS DIMENSIONS, ce qui est unique dans la famille.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        if (speed != 0)
        {
            DecideScorpionAction_004921A0(e, probe, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);               // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        // Ghidra: 0x00492898 — le mot de contact perd son bit 8 avant tout examen.
        uint contact = *(uint*)(e + 0x90) & 0xfffffeff;
        *(uint*)(e + 0x90) = contact;

        bool settled = false;

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (contact & 0x8000) == 0)
        {
            uint state = *(uint*)e;
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e = state & 0xfffffffe;
            *(int*)(e + 0x54) = 0;

            // Ghidra: 0x004928be — S'IL ETAIT EN L'AIR, le heurt de mur n'est meme pas examine
            // cette image-ci : atterrir a la priorite.
            settled = (state & 1) != 0;
        }

        if (!settled)
        {
            // Ghidra: LAB_004928e0 — et l'inverse : au-dessus du plancher, il reprend le bit « en
            // l'air » qu'il vient peut-etre de perdre.
            uint state = *(uint*)e;
            if ((state & 1) == 0 && *(int*)(e + 0x20) > *(int*)(e + 0x60))
            {
                *(uint*)e = state | 1;
            }

            if ((contact & 0x2000ad82) != 0)
            {
                ResolveScorpionWallHit_004928F1(e, &keep, boldness);
            }
        }

        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanScorpionAction_00492A24(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: LAB_004928f1 — <b>le heurt de mur</b>, en deux branches que rien ne rapproche.
    ///
    /// <para>
    /// <b>Au sol</b> (le corps du Gremlin et de la Salamandre) : il passe en posture 7 — marcher
    /// droit — pour <c>rand() % 0x28 + 0x14</c> images, perd son geste, voit son placement annule
    /// des onze champs, et demande a <see cref="ProbeClearSide_004D5E60"/> de quel cote contourner,
    /// avec un pas de <c>hardiesse + 0x40</c>.
    /// </para>
    /// <para>
    /// <b>En l'air</b> : rien de tout cela. Ses trois vitesses tombent a zero, il est repose a
    /// l'horizontale ou il etait — <b>sa hauteur n'est PAS rendue</b>, donc il reste en l'air et
    /// tombera —, et il passe en attente pour quarante images avec <c>+0x2c = 0x1000</c>. Un
    /// Scorpion qui bondit dans un mur s'ecrase.
    /// </para>
    /// </summary>
    private static void ResolveScorpionWallHit_004928F1(
        byte* e, SceneEntityPlacement* keep, int boldness)
    {
        // ================================================================ en l'air
        if ((*(uint*)e & 1) != 0)
        {
            *(int*)(e + 0x5c) = 0;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x58) = 0;

            *(int*)(e + 0x1c) = keep->X;
            *(int*)(e + 0x24) = keep->Z;

            e[0x42] = 0;
            e[0x43] = 0x28;
            *(short*)(e + 0x12) = 0;
            *(ushort*)(e + 0x2c) = 0x1000;
            return;
        }

        // ================================================================ au sol
        if (e[0x42] != 7)
        {
            e[0x42] = 7;
            int roll = CrtRand();
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 0x50;
            e[0x43] = (byte)(roll % 0x28 + 0x14);
        }

        // Ghidra: LAB_0049292f
        *(uint*)e &= 0xfffffffd;

        RestoreSceneEntityPlacement(e, keep);

        int side = ProbeClearSide_004D5E60(e, 0x2000ad82);
        if ((short)side == 0) side = SignedMask(CrtRand(), 1) * 2 - 1;

        *(short*)(e + 0x32) += (short)((boldness + 0x40) * side);
    }

    /// <summary>
    /// Ghidra: <c>0x004922fe..0x004927f0</c> — <b>le premier temps</b> : l'animation, les deux
    /// armes, puis la posture. Treize cases, dont sept servies : 0 attendre, 1 se tourner,
    /// 2 approcher, 3 filer vers un cap, 6 bondir, 7 marcher droit, 0x0b la pince, 0x0c le dard.
    /// Les cases 4, 5, 8, 9 et 0x0a retombent sur la sortie.
    /// </summary>
    private static void DecideScorpionAction_004921A0(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        if ((*(uint*)e & 2) != 0)
        {
            // ============================================================ 0x0b : LA PINCE
            if (e[0x42] == 0x0b && *(short*)(e + 0x1a) == 0x0b)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0b)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
                }
            }

            // ============================================================ 0x0c : LE DARD
            if (e[0x42] == 0x0c && *(short*)(e + 0x1a) == 0x0a)
            {
                byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0a)
                {
                    *(uint*)prey |= 0x40;
                    *(ushort*)(prey + 0x96) = 0x10;
                    ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);

                    // Un tiers l'effet 7, deux tiers l'effet de piege. Le seul corps du depot qui
                    // tire son effet d'etat au sort.
                    if (CrtRand() % 3 == 1)
                    {
                        ApplyStatusEffectIfAllowed_00492E00((uint*)e, (uint*)prey, 0);
                    }
                    else
                    {
                        // Ghidra: FUN_00492160, copie octet pour octet de FUN_004a2bf0.
                        ApplyTrapEffectIfAllowed_004A2BF0((uint*)e, (uint*)prey, 0);
                    }

                    Aud.PlayPositionalSound(3, 0x200, 0x1800, (int*)(prey + 0x1c));
                }
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
                    e[0x43] = 0;                                    // Ghidra: LAB_004927ed
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                return;

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                }

                // Ghidra: LAB_004924e2 — cone 0x190, la moitie de celui de la Salamandre.
                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                *(short*)(e + 0x32) +=
                    (short)((heading & 0xfff) < 0x800 ? boldness + 0x3c : -0x3c - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;
                }

                if (*(short*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                // Ghidra: LAB_0049259e
                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) +=
                    (short)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x28 : -0x28 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    // Ghidra: LAB_004925fc
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
                            ? boldness * 8 + 0x28
                            : -0x28 - boldness * 8);
                    }
                }

                // Ghidra: LAB_00492693
                if (*(short*)(e + 0x12) != 0) return;

                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x1a) = 0;
                *(short*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 6 : bondir
            case 6:
                if (*(short*)(e + 0x12) == 0)
                {
                    uint state = *(uint*)e;

                    // Deja en l'air : le bond est refuse et le rebours tombe.
                    if ((state & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        // Ghidra: LAB_004926d8 — une impulsion UNIQUE, jamais reposee.
                        *(uint*)e = state & 0xfeffffff;
                        *(short*)(e + 0x18) = 1;

                        int thrust = (boldness + 0x100) << 6;
                        int step = (speed * thrust) >> 0xc;

                        *(short*)(e + 0x1a) = 0;
                        *(int*)(e + 0x5c) = thrust;

                        *(int*)(e + 0x50) +=
                            ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x58) +=
                            (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 1;
                        *(short*)(e + 0x12) = 1;
                    }
                }

                // Ghidra: LAB_0049274d — il tient la posture tant qu'il vole.
                if ((*(uint*)e & 1) != 0) return;
                e[0x43] = 0;
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

                // Ghidra: LAB_00492785
                if (phase != 0) return;

                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;       // Ghidra: LAB_004927b0
                return;
            }

            // ---------------------------------------------------------- 0x0b : la pince
            case 0x0b:
            {
                if (*(short*)(e + 0x12) != 0) return;

                uint state = *(uint*)e;
                *(int*)(e + 0x5c) = 0;
                if ((state & 0x2000000) == 0) return;

                e[0x43] = 0;
                *(uint*)e = state & 0xfffffffd;
                return;
            }

            // ---------------------------------------------------------- 0x0c : le dard
            case 0x0c:
                if (*(short*)(e + 0x12) != 0) return;

                *(int*)(e + 0x5c) = 0;
                if ((*(uint*)e & 0x2000000) == 0) return;

                // Ghidra: LAB_004927ed — le dard, LUI, ne rend pas le bit de geste : il ne pose que
                // le rebours a zero, et c'est la replanification qui le sortira de la.
                e[0x43] = 0;
                return;

            // ---------------------------------------------------------- 4, 5, 8, 9, 0x0a : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00492a24..0x00492dbd</c> — <b>le troisieme temps</b> : quelle posture prendre.
    /// La distance et le cap sont recalcules A PLAT ici, contrairement a ceux du premier temps.
    ///
    /// <para>
    /// <b>Trois bandes, ecrites en cascade et non en alternative</b> : sous <c>0xfa</c>, de
    /// <c>0xfa</c> a <c>0x258</c>, au-dela de <c>0x258</c>. Le binaire les teste l'une apres
    /// l'autre sans sortir, et elles se trouvent etre disjointes — mais la forme est gardee, parce
    /// que c'est la meme que celle du Varan, ou elles se chevauchent.
    /// </para>
    /// <para>
    /// <b>Au corps a corps</b>, la piqure n'a lieu que si la proie tient dans le cone
    /// <c>timide * 8 + 0x200</c> ET qu'elle est plus pres que <c>rand() % (timide * 2 + 1) + 0x64</c>.
    /// Le meme tirage choisit ensuite l'arme : sous <c>(timide &gt;&gt; 1) + 0x0a</c> la pince,
    /// au-dela le dard. <b>Plus le Scorpion est hardi, plus il pince ; plus il est timide, plus il
    /// pique.</b>
    /// </para>
    /// </summary>
    private static void ReplanScorpionAction_00492A24(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        *(short*)(e + 0x12) = 0;

        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        // ================================================================ 1. le corps a corps
        if (distance < 0xfa)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x28)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    uint state = *(uint*)e;

                    if (roll < (timid >> 1) + 0x0a)
                    {
                        e[0x42] = 0x0b;                             // la pince
                        *(short*)(e + 0x18) = 3;
                    }
                    else
                    {
                        e[0x42] = 0x0c;                             // le dard
                        *(short*)(e + 0x18) = 4;
                    }

                    e[0x43] = 1;
                    *(uint*)e = (state & 0xfeffffff) | 2;
                    *(int*)(e + 0x5c) = 0;
                    *(short*)(e + 0x1a) = 0;
                }
                else
                {
                    HesitateSceneEntity(e);                         // Ghidra: LAB_00492b33
                }
            }
            else if (roll < 0x3c)
            {
                // Ghidra: LAB_00492b42 — filer vers un cap entierement tire.
                e[0x42] = 3;
                *(short*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
            else if (roll < 0x46 && distance > boldness + 0x64)
            {
                // Ghidra: LAB_00492b87 — LE BOND, et seulement si la proie est assez loin.
                e[0x42] = 6;
                e[0x43] = 1;
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                // Ghidra: LAB_00492bb3
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = SceneEntityDelay(timid);
            }
        }

        // ================================================================ 2. l'approche
        if (distance >= 0xfa && distance < 0x258)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                // On ne se retourne pas vers ce qu'on a deja en face.
                if (((heading + 0x400) & 0xfff) >= 0x800) HesitateSceneEntity(e);
            }
            else if (roll < 0x32)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = SceneEntityDelay(timid);
            }
            else if (roll < 0x46)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else
            {
                // Ghidra: LAB_00492c7b
                e[0x42] = 3;
                *(short*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
        }

        // ================================================================ 3. l'errance
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x1e)
        {
            if (far < boldness * 2 + 0x0a)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x64;
                e[0x43] = SceneEntityDelay(timid);
                return;
            }

            // Ghidra: LAB_00492d0d — se reposer, mais d'un rebours PLUS COURT que partout ailleurs :
            // rand() % 10 + (timide >> 2), sans le 0x14 de SceneEntityDelay.
            e[0x42] = 0;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 10 + (byte)(timid >> 2));
            *(ushort*)(e + 0x10) = (ushort)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x28)
        {
            e[0x42] = 3;
            *(short*)(e + 0x12) = 0;
            e[0x43] = SceneEntityDelay(timid);
            *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        // Ghidra: LAB_00492da3 — de tres loin, il bondit vers un cap entierement tire.
        e[0x42] = 6;
        e[0x43] = 1;
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }
}
