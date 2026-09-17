// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ARAIGNEE — 0x00497370, sous-type 17 du bloc C de la mine (zone 1, sous-zones 3 et 4).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, sous-zones 3 et 4, modele 3 :
// « Araignee », 32 PV, 2 PM, rang 30 — la plus fragile de la mine). 883 instructions, absentes de
// Blaze.c ; releve Legacy/Raw/Blaze/FUN_00497370.asm, table de sauts a DOUZE entrees a 0x00497EBC.
//
// C'EST LE SCORPION EN PLUS SIMPLE, et c'est la bonne facon de la lire : le diff STRICT des deux
// corps ne laisse, une fois les cibles de saut et les affectations de registre mises de cote, que
// SIX differences de fond. Tout le reste — la charpente du Varan, la distance a trois dimensions,
// le heurt de mur en deux branches, la replanification en trois bandes cascadees — est le meme
// texte. Voir Ai/Mine/Scorpion.cs pour ce qui leur est commun.
//
// LES SIX DIFFERENCES :
//
//   1. UNE SEULE ARME. Le Scorpion a la pince (posture 0x0b) et le dard (0x0c) ; l'Araignee n'a que
//      la MORSURE, a l'image 10. Sa table de sauts a douze entrees au lieu de treize, et sa
//      replanification n'a plus le tirage qui choisissait entre les deux.
//   2. ELLE MORD QUELLE QUE SOIT SA POSTURE. Le prelude du Scorpion teste +0x42 avant la sonde ;
//      celui de l'Araignee ne teste QUE l'image. En pratique c'est sans effet — seule la posture
//      0x0b pose le bit de geste —, mais le texte est bien plus court.
//   3. ELLE EMPOISONNE UNE FOIS SUR QUINZE, et toujours du meme effet (7). Le Scorpion tirait sur
//      trois, entre deux effets.
//   4. ELLE VIRE MOINS SEC EN LIGNE DROITE : posture 3, hardiesse * 2 + 0x28 contre
//      hardiesse * 8 + 0x28. Et elle se tourne plus doucement sur place : posture 1,
//      hardiesse + 0x28 contre hardiesse + 0x3c.
//   5. MAIS ELLE COURT PLUS VITE : (hardiesse + 0x100) * 0x90 a l'approche contre
//      hardiesse * 0x90 + 0x7000, et hardiesse * 0x90 + 0x7000 en ligne droite contre + 0x5000.
//   6. SON CADRE DE PILE EST AUTRE : le cap et la vitesse ont echange leurs cases. Sans effet, mais
//      c'est ce qui fait la moitie du volume du diff.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00497370</c> — <b>l'intelligence de l'Araignee</b>, sous-type 17 de la table
    /// installee <c>0x0056ad48</c> dans les sous-zones 3 et 4 de la mine. Meme ossature que
    /// <see cref="TickSceneEntityAi_004921A0"/> ; voir l'en-tete pour les six ecarts.
    /// </summary>
    private static void TickSceneEntityAi_00497370(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — le meme gabarit que le Scorpion, aux memes six termes.
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
            PickChasedPartyMember_004A1020(e, 0, 0);   // Ghidra: FUN_00491290, copie de FUN_004a1020
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

        // La distance a trois dimensions, comme le Scorpion.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        if (speed != 0)
        {
            DecideSpiderAction_00497370(e, probe, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);               // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        uint contact = *(uint*)(e + 0x90) & 0xfffffeff;
        *(uint*)(e + 0x90) = contact;

        bool settled = false;

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (contact & 0x8000) == 0)
        {
            uint state = *(uint*)e;
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e = state & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
            settled = (state & 1) != 0;
        }

        if (!settled)
        {
            uint state = *(uint*)e;
            if ((state & 1) == 0 && *(int*)(e + 0x20) > *(int*)(e + 0x60))
            {
                *(uint*)e = state | 1;
            }

            if ((contact & 0x2000ad82) != 0)
            {
                // Le meme corps que le Scorpion, jusqu'au masque de sonde.
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

        ReplanSpiderAction_00497A2E(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x00497478..0x00497925</c> — <b>le premier temps</b> : l'animation, la morsure,
    /// puis la posture. Douze cases, dont six servies : 0 attendre, 1 se tourner, 2 approcher,
    /// 3 filer vers un cap, 6 bondir, 7 marcher droit, 0x0b la morsure. Les cases 4, 5, 8, 9 et
    /// 0x0a retombent sur la sortie.
    /// </summary>
    private static void DecideSpiderAction_00497370(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // ================================================================ LA MORSURE
        // Aucun test de posture ici, contrairement au Scorpion : l'image suffit.
        if ((*(uint*)e & 2) != 0 && *(short*)(e + 0x1a) == 0x0a)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0a)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x10;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

                // Une fois sur quinze, et toujours l'effet 7.
                if (CrtRand() % 0x0f == 1)
                {
                    ApplyStatusEffectIfAllowed_00492E00((uint*)e, (uint*)prey, 0);
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
                    e[0x43] = 0;
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

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(int*)(e + 0x5c) = 0;
                *(short*)(e + 0x32) +=
                    (short)((heading & 0xfff) < 0x800 ? boldness + 0x28 : -0x28 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = (boldness + 0x100) * 0x90;
                }

                if (*(short*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(short*)(e + 0x32) +=
                    (short)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x28 : -0x28 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            case 3:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x7000;
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
                            ? boldness * 2 + 0x28
                            : -0x28 - boldness * 2);
                    }
                }

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

                    if ((state & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        *(uint*)e = state & 0xfeffffff;
                        *(short*)(e + 0x18) = 1;

                        int thrust = (boldness + 0x100) << 6;
                        int step = (speed * thrust) >> 0xc;

                        *(int*)(e + 0x5c) = thrust;
                        *(short*)(e + 0x1a) = 0;

                        *(int*)(e + 0x50) +=
                            ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x58) +=
                            (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10)) * step) >> 0xc;
                        *(int*)(e + 0x54) += 0x1e000;

                        *(uint*)e |= 1;
                        *(short*)(e + 0x12) = 1;
                    }
                }

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

                if (phase != 0) return;

                *(short*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(short*)(e + 0x18) = 1;
                *(short*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;
                return;
            }

            // ---------------------------------------------------------- 0x0b : la morsure
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

            // ---------------------------------------------------------- 4, 5, 8, 9, 0x0a : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00497a2e..0x00497eba</c> — <b>le troisieme temps</b>. Le corps du Scorpion, a
    /// une chose pres : le corps a corps ne choisit plus d'arme. Passe le cone et la portee, elle
    /// mord, un point c'est tout.
    /// </summary>
    private static void ReplanSpiderAction_00497A2E(byte* e, int boldness, int timid)
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
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(uint*)e = (state & 0xfeffffff) | 2;
                    *(int*)(e + 0x5c) = 0;
                    *(short*)(e + 0x18) = 3;
                    *(short*)(e + 0x1a) = 0;
                }
                else
                {
                    HesitateSceneEntity(e);
                }
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 3;
                *(short*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
            }
            else if (roll < 0x46 && distance > boldness + 0x64)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
            else
            {
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

        e[0x42] = 6;
        e[0x43] = 1;
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
    }
}
