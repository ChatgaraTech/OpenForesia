// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE MILLE-PATTE — 0x00491650, sous-type 4 du bloc C de la mine (zone 1, sous-zone 4).
//
// Le nom vient du bestiaire (especes.csv x modeles.csv, zone 1, sous-zone 4, modele 1 :
// « Mille-patte », 51 PV, 3 PM, rang 29). 838 instructions, absentes de Blaze.c ; releve
// Legacy/Raw/Blaze/FUN_00491650.asm, table de sauts a DOUZE entrees a 0x00492130.
//
// LA TROISIEME VARIANTE de la charpente du Varan dans la mine, apres le Scorpion (0x004921A0) et
// l'Araignee (0x00497370) — voir Ai/Mine/Scorpion.cs pour ce qui leur est commun. Mais c'est la
// plus eloignee des trois : elle en differe de 354 et 331 lignes, contre 179 entre les deux autres,
// et les ecarts ne sont pas cosmetiques.
//
// CINQ CHOSES QUI LUI SONT PROPRES :
//
//   1. IL NE MORD QU'EN L'AIR, et c'est la plus jolie trouvaille de ce corps. Son geste (posture
//      0x0b) tourne vers la proie jusqu'a l'image 8, puis BONDIT — et c'est le bond lui-meme qui
//      pose le bit de geste, par un `or eax,3` qui allume « en l'air » et « en geste » ensemble.
//      Le prelude, lui, EFFACE le bit de geste a chaque image ou la bete touche le sol. La sonde de
//      morsure, qui demande ce bit, ne peut donc voir personne tant qu'il rampe : le Mille-patte
//      saute a la gorge, et rate tout s'il est retombe.
//   2. IL POSE LE BIT 0x10 EN POSTURE 5, et le retire partout ailleurs, en tete de corps et sans
//      condition. C'est le bit que toute la famille lit comme « ne rien decider ».
//   3. SA POSTURE 5 TOURNE SUR PLACE VERS UN CAP TIRE. Elle se donne un cap au hasard dans
//      +0x10, vire de +/- (hardiesse * 2 + 0x50) par image du cote que le signe de ce cap indique,
//      et rend la main des que l'ecart passe sous 0x258. Les deux autres n'ont rien de tel — leur
//      posture 3 file droit.
//   4. SA SONDE DE PORTEE EST LA SEULE DES TROIS A PLONGER : {0, -0x32, 0x32, 0x64, 0, 0x82}. Le
//      deuxieme terme est NEGATIF la ou le Scorpion et l'Araignee posent +0x1e, et le sixieme vaut
//      0x82 contre 0x64. Il mord plus bas et plus large — ce qui va bien avec une morsure en plein
//      bond.
//   5. IL VIRE DEUX FOIS PLUS SEC QUE L'ARAIGNEE : hardiesse + 0x80 sur place, hardiesse * 2 + 0x80
//      en marche, contre + 0x28 chez elle. Et il empoisonne une fois sur trois, de l'effet de piege
//      seulement — jamais de l'effet 7 que l'Araignee et le Scorpion peuvent poser.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00491650</c> — <b>l'intelligence du Mille-patte</b>, sous-type 4 de la table
    /// installee <c>0x0056ad48</c> dans la quatrieme sous-zone de la mine.
    /// </summary>
    private static void TickSceneEntityAi_00491650(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x30] — LE SEUL des trois gabarits de la famille dont le deuxieme terme soit
        // negatif : il mord SOUS lui.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = unchecked((short)0xffce);      // -0x32
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x82;

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

        // La distance a trois dimensions, comme ses deux voisines.
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        if (speed != 0)
        {
            DecideCentipedeAction_00491650(e, probe, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);               // recopie en ligne
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        // Ghidra: 0x00491d3c — DEUX bits effaces (8 et 9), la ou le Scorpion n'en efface qu'un.
        uint contact = *(uint*)(e + 0x90) & 0xfffffcff;
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
                // Le corps du Scorpion, mot pour mot — jusqu'a la branche aerienne.
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

        ReplanCentipedeAction_00491EB8(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x00491754..0x00491c8c</c> — <b>le premier temps</b> : l'animation, le bit de
    /// posture figee, la morsure, la fermeture du geste au sol, puis la posture. Douze cases, dont
    /// sept servies : 0 attendre, 1 se tourner, 2 approcher, 5 virer vers un cap tire, 6 bondir,
    /// 7 marcher droit, 0x0b la morsure bondissante. Les cases 3, 4, 8, 9 et 0x0a retombent sur la
    /// sortie — <b>et ce sont 3 et 4 qui sont vides ici, pas 3 tout seul</b> : la table du
    /// Mille-patte n'est pas celle du Scorpion decalee, c'est une autre table.
    /// </summary>
    private static void DecideCentipedeAction_00491650(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopie en ligne

        // Ghidra: LAB_0049181b — le bit « ne rien decider » suit la posture 5, a chaque image.
        if (e[0x42] == 5) *(uint*)e |= 0x10;
        else              *(uint*)e &= 0xffffffef;

        // ================================================================ LA MORSURE
        if ((*(uint*)e & 2) != 0 && *(short*)(e + 0x1a) == 0x0b)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0 && *(short*)(e + 0x1a) == 0x0b)
            {
                *(uint*)prey |= 0x40;
                *(ushort*)(prey + 0x96) = 0x10;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));

                // Une fois sur trois, et TOUJOURS l'effet de piege : pas de tirage entre deux
                // effets comme chez le Scorpion.
                if (CrtRand() % 3 == 1)
                {
                    // Ghidra: FUN_00492160, copie octet pour octet de FUN_004a2bf0.
                    ApplyTrapEffectIfAllowed_004A2BF0((uint*)e, (uint*)prey, 0);
                }
            }
        }

        // Ghidra: LAB_004918b3 — AU SOL, LE GESTE TOMBE. C'est ce qui interdit de mordre en
        // rampant : la sonde ci-dessus ne s'ouvre que si le bit 2 a survecu, et il ne survit qu'en
        // l'air.
        if ((*(uint*)e & 1) == 0) *(uint*)e &= 0xfffffffd;

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
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
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
                    (short)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x12) == 0)
                {
                    *(short*)(e + 0x18) = 1;
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
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
                    (short)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x80 : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 5 : virer vers un cap tire
            case 5:
                if (*(short*)(e + 0x12) == 0)
                {
                    // Ghidra: 0x00491a45 — le cap est tire D'ABORD, et c'est son signe qui decide
                    // du sens du virage pour toute la duree de la posture.
                    *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
                    *(uint*)e |= 0x1000000;
                    *(short*)(e + 0x18) = 1;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                }

                // Ghidra: LAB_00491a8d
                if (*(short*)(e + 0x12) != 1) return;

                *(short*)(e + 0x32) += (short)(*(short*)(e + 0x10) < 0x800
                    ? boldness * 2 + 0x50
                    : -0x50 - boldness * 2);

                {
                    // Ghidra: 0x00491ab5 — l'ecart restant, sur douze bits. Le `neg` qui suit le
                    // masque est MORT : `and eax,0xfff` ne peut pas rendre un negatif, donc le
                    // `jge` est toujours pris. Transcrit comme il s'execute.
                    int off = (*(ushort*)(e + 0x10) - *(ushort*)(e + 0x32)) & 0xfff;
                    if (off >= 0x258) return;
                    e[0x43] = 0;
                }
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

                // Ghidra: LAB_00491b58
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
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 0x0b : la morsure
            case 0x0b:
                BiteLeapGesture_00491BC5(e, boldness, heading);
                return;

            // ---------------------------------------------------------- 3, 4, 8, 9, 0x0a : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: LAB_00491bc5 — <b>la morsure bondissante</b>, posture 0x0b, en quatre phases dans
    /// <c>+0x12</c>.
    ///
    /// <para>
    /// <b>0</b> : se mettre en garde (animation 3). <b>1</b> : se tourner vers la proie — cone
    /// <c>0x200</c>, pas <c>hardiesse * 2 + 0x80</c> — jusqu'a l'image 8. <b>2</b> : SAUTER, et
    /// c'est la seule ecriture du corps qui allume le bit de geste — <c>or eax,3</c>, les deux bits
    /// d'un coup, « en l'air » et « en geste ». <b>3</b> : attendre d'etre retombe, et alors le
    /// geste tombe avec.
    /// </para>
    /// <para>
    /// A la phase 2, si la bete etait <b>deja</b> en l'air, le geste est simplement annule : elle ne
    /// peut pas sauter deux fois.
    /// </para>
    /// </summary>
    private static void BiteLeapGesture_00491BC5(byte* e, int boldness, int heading)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x1a) = 0;
            *(uint*)e &= 0xfeffffff;
            *(short*)(e + 0x18) = 3;
            *(short*)(e + 0x12) = 1;
        }

        // Ghidra: LAB_00491bf1
        if (*(short*)(e + 0x12) == 1)
        {
            if (*(short*)(e + 0x1a) == 8) *(short*)(e + 0x12) = 2;

            if (((heading + 0x100) & 0xfff) > 0x200)
            {
                *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800
                    ? boldness * 2 + 0x80
                    : -0x80 - boldness * 2);
            }
        }

        // Ghidra: LAB_00491c43 — LE SAUT.
        if (*(short*)(e + 0x12) == 2)
        {
            uint state = *(uint*)e;

            if ((state & 1) != 0)
            {
                e[0x43] = 0;
                return;
            }

            *(int*)(e + 0x54) += 0x1e000;
            *(int*)(e + 0x5c) = boldness * 0x90 + 0xe000;
            *(uint*)e = state | 3;                              // en l'air ET en geste
            *(short*)(e + 0x12) = 3;
        }

        // Ghidra: LAB_00491c79 — et le geste ne se ferme qu'au sol.
        if (*(short*)(e + 0x12) != 3) return;

        uint done = *(uint*)e;
        if ((done & 1) != 0) return;

        *(uint*)e = done & 0xfffffffd;
        e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00491eb8..0x0049212e</c> — <b>le troisieme temps</b>, trois bandes cascadees
    /// comme chez ses deux voisines, mais avec d'autres seuils et deux postures de plus dans la
    /// corbeille : la 5 (virer vers un cap tire) et la 7 (marcher droit), que le Scorpion ne tire
    /// jamais de loin.
    ///
    /// <para>
    /// <b>Au corps a corps il n'a qu'une sortie offensive</b>, la morsure, et elle ne pose ni
    /// animation ni bit de geste — seulement la posture, un rebours de 1 et la phase 0. Tout le
    /// reste est le travail du geste lui-meme.
    /// </para>
    /// </summary>
    private static void ReplanCentipedeAction_00491EB8(byte* e, int boldness, int timid)
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

            if (roll < 0x32)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200
                    && distance < CrtRand() % (timid * 2 + 1) + 0x64)
                {
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(short*)(e + 0x12) = 0;
                }
                else
                {
                    HesitateSceneEntity(e);                     // Ghidra: LAB_00491f6f
                }
            }
            else if (roll < 0x50)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = SceneEntityDelay(timid);
            }
            else
            {
                // Ghidra: LAB_00491fa7
                e[0x42] = 6;
                e[0x43] = 1;
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
                *(short*)(e + 0x12) = 0;
            }
        }

        // ================================================================ 2. l'approche
        if (distance >= 0xfa && distance < 0x258)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) >= 0x800) HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x28)
            {
                e[0x42] = 2;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = SceneEntityDelay(timid);
                return;
            }

            if (roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(ushort*)(e + 0x10) = (ushort)CrtRand();
            }
            else if (roll < 0x50)
            {
                e[0x42] = 7;
                e[0x43] = (byte)(CrtRand() % 0x14 + 0x1e);
            }
            else
            {
                e[0x42] = 5;
                e[0x43] = 1;
            }

            *(short*)(e + 0x12) = 0;                            // Ghidra: LAB_00492076
            return;
        }

        // ================================================================ 3. l'errance
        if (distance < 0x258) return;

        int far = CrtRand() % 100;

        if (far < 0x32)
        {
            e[0x42] = 2;
            *(ushort*)(e + 0x10) = 0x64;
            e[0x43] = SceneEntityDelay(timid);
            return;
        }

        if (far < 0x3c)
        {
            e[0x42] = 7;
            *(short*)(e + 0x12) = 0;
            e[0x43] = (byte)(CrtRand() % 0x14 + 0x1e);
            return;
        }

        if (far < 0x50)
        {
            e[0x42] = 5;
            e[0x43] = 1;
            *(short*)(e + 0x12) = 0;
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(ushort*)(e + 0x10) = (ushort)CrtRand();
        *(short*)(e + 0x12) = 0;
    }
}
