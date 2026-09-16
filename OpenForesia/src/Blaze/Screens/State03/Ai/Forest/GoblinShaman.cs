// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GOBLIN-SORCIER — 0x0049E190, sous-type 9 du bloc A de la foret (zone 3, sous-zones 0 a 4).
//
// 572 instructions, absente de `Blaze.c`, relevee au desassembleur. Meme decoupage en
// trois temps que les deux charpentes de reference, memes feuilles.
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
    // LA TROISIEME INTELLIGENCE D'ENTITE DE SCENE (2026-08-30)
    //
    // 0x0049e190, 572 instructions, absente de Blaze.c — relevee au desassembleur,
    // Legacy/Raw/Blaze/FUN_0049E190.asm.
    //
    // Meme decoupage en trois temps que TickSceneEntityAi_0049A980 et _00499DE0, memes feuilles,
    // meme queue de quatre appels. Elle n'est PAS factorisee avec elles pour la meme raison
    // qu'elles ne le sont pas entre elles : ce sont trois fonctions distinctes du binaire.
    //
    // Comparee a ses deux sœurs par la forme des instructions seules, elle en differe sur 416
    // lignes de 672 et 345 de 572 : ce n'est pas une variante a constantes pres, c'est un autre
    // corps.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0049e190</c> — <b>la troisieme intelligence d'entite de scene</b> : un
    /// gestionnaire de la table installee <c>0x0056ad48</c>, foret, sous-zones 0 a 4.
    ///
    /// <para>
    /// <b>Ce qui la distingue de ses deux sœurs.</b> Sa sonde de geste est <c>(0, 0, 0x32, 0x64,
    /// 0, 0x5a)</c> — pas de decalage vertical, donc un coup a plat. Elle lance des <b>sorts</b>,
    /// ce qu'aucune des deux autres ne fait : sa posture 1 attend l'image 7 de l'animation puis
    /// appelle <see cref="CastSpell_0053A980"/> avec la paire famille-indice rangee en <c>+0x06</c>,
    /// et sa posture 3 tire cette paire au sort dans <see cref="s_SpellGestures_00552978"/> avant
    /// d'invoquer le sous-type <c>0xbd</c>.
    /// </para>
    /// <para>
    /// <b>Le delai d'apres-sort ne vient pas d'un tirage</b> mais du repertoire : l'octet
    /// <c>+0x13</c> de la fiche du sort, atteinte par <c>DAT_005e97c4 + 0x9c</c> puis la famille
    /// puis l'indice, avec un pas de <c>0x30</c>. C'est le sort lui-meme qui dit combien de temps
    /// la bete reste immobile apres l'avoir lance.
    /// </para>
    /// <para>
    /// <b>Trois bandes de distance, comme ses sœurs, mais d'autres proportions</b> : sous
    /// <c>0xfa</c> elle frappe trois fois sur dix et tient sa distance sinon ; entre <c>0xfa</c> et
    /// <c>0x1f4</c> elle charge presque une fois sur deux ; au-dela elle charge trois fois sur dix
    /// et se repose ou erre le reste du temps.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_0049E190(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x5a;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideSceneEntityAction_0049E190(e, probe, boldness, distance, heading, traits);
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

        ReplanSceneEntityAction_0049E190(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x0049e21b..0x0049e5ff</c> — <b>le premier temps</b> de
    /// <see cref="TickSceneEntityAi_0049E190"/>.
    ///
    /// <para>
    /// <b>Deux tables de sauts</b>, toutes deux bornees a 3 : celle de <c>0x0049e900</c> sur
    /// l'etat <c>+0x10</c> quand l'entite tient un geste arme, celle de <c>0x0049e910</c> sur la
    /// posture <c>+0x42</c> sinon.
    /// </para>
    /// <para>
    /// <b>Une subtilite du binaire, conservee.</b> Quand l'animation n'a pas atteint son image 8,
    /// le corps saute par-dessus la sonde <b>et</b> par-dessus la relecture du mot de drapeaux : le
    /// test du bit 1 porte alors sur la valeur lue avant la sonde, et <c>+0x1a</c> n'est pas
    /// decremente. Les deux chemins ne different donc pas seulement par le coup porte.
    /// </para>
    /// </summary>
    private static void DecideSceneEntityAction_0049E190(
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
                return;
            }

            uint settled;

            if (*(short*)(e + 0x1a) >= 8)
            {
                byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(short*)(victim + 0x96) = 0x10;
                    *(uint*)victim |= 0x40;

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                }

                settled = *(uint*)e;
                if ((settled & 1) != 0) (*(short*)(e + 0x1a))--;
            }
            else
            {
                // Ghidra: le saut court vers LAB_0049e2a7 garde la lecture d'avant la sonde.
                settled = flags;
            }

            if ((settled & 1) != 0) return;

            if ((settled & 0x2000000) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = settled & 0xfffffffd;
            }

            *(int*)(e + 0x5c) = 0;
            return;
        }

        // ================================================================ le geste qui s'arme
        if ((flags & 0x10) != 0) return;

        if ((flags & 0x804) != 0)
        {
            if ((traits & 0x400003e8) != 0)
            {
                flags &= 0xfffff7fb;
                e[0x43] = 0;
                *(uint*)e = flags;
            }

            int stage = *(short*)(e + 0x10);
            if ((uint)stage > 3) return;

            switch (stage)
            {
                case 0:
                {
                    uint held = *(uint*)e;
                    if ((held & 0x2000000) == 0) return;
                    *(uint*)e = held & 0xfffff7ff;
                    return;
                }

                case 1:
                    if (*(short*)(e + 0x18) != 5)
                    {
                        *(int*)(e + 0x18) = 5;
                        *(uint*)e &= 0xfefffffb;
                        return;
                    }

                    if (*(short*)(e + 0x1a) != 7) return;

                    {
                        ushort gesture = *(ushort*)(e + 6);
                        CastSpell_0053A980((uint*)e, (gesture >> 8) & 0xff, gesture & 0xff, -1, -1);
                        (*(short*)(e + 0x10))--;
                    }
                    return;

                case 2:
                {
                    byte left = (byte)(e[0x43] - 1);
                    e[0x43] = left;
                    if (left == 0) *(short*)(e + 0x10) = (short)(stage - 1);

                    TurnSceneEntityTowards_0049E384(e, boldness * 4 + 0x32, heading);
                    return;
                }

                default:
                {
                    SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                    int pick = CrtRand() % 3;
                    int species = *(short*)(e + 0x14);
                    ushort gesture = s_SpellGestures_00552978[pick + species * 3];
                    *(ushort*)(e + 6) = gesture;

                    byte* directory = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c);
                    byte* family = *(byte**)(directory + ((gesture >> 8) & 0xff) * 4);
                    byte after = family[(gesture & 0xff) * 0x30 + 0x13];

                    (*(short*)(e + 0x10))--;
                    e[0x43] = after;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }
            }
        }

        // ================================================================ la posture tenue
        if ((flags & 1) != 0) return;

        int posture = e[0x42];
        if ((uint)posture > 3) return;

        switch (posture)
        {
            case 0:
                if (*(short*)(e + 0x18) != 0)
                {
                    *(int*)(e + 0x18) = 0;
                    *(uint*)e = flags | 0x1000000;
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
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags | 0x1000000;
                }

                if (TurnSceneEntityTowards_0049E384(e, boldness * 2 + 0x80, heading)) e[0x43] = 0;

                *(int*)(e + 0x5c) = 0;
                return;

            case 2:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags | 0x1000000;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowards_0049E384(e, boldness * 2 + 0x80, heading);
                *(int*)(e + 0x5c) = (boldness + 0xc0) << 7;
                return;

            default:
                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags | 0x1000000;
                }

                {
                    short course = *(short*)(e + 0x10);
                    int push = (boldness + 0x80) << 7;
                    *(int*)(e + 0x5c) = push;

                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * push) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * push) >> 0xc;

                    *(int*)(e + 0x5c) = 0;
                    TurnSceneEntityTowards_0049E384(e, boldness * 4 + 0x32, heading);
                }
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>LAB_0049e384</c>, <c>LAB_0049e5ec</c> et leurs deux copies — <b>se tourner vers
    /// la cible d'au plus <paramref name="step"/></b>. Le binaire partage bien ces deux etiquettes
    /// entre plusieurs postures, donc la factorisation est la sienne.
    ///
    /// <para>
    /// Si l'ecart <paramref name="heading"/> tient dans la fourchette, le cap y saute d'un coup ;
    /// sinon il avance de <paramref name="step"/>, dans le sens que la moitie du cercle indique.
    /// </para>
    /// </summary>
    /// <returns><c>true</c> quand le cap a saute d'un coup, ce dont la posture 1 se sert pour
    /// remettre son delai a zero.</returns>
    private static bool TurnSceneEntityTowards_0049E384(byte* e, int step, int heading)
    {
        if (((step + heading) & 0xfff) <= step * 2)
        {
            *(short*)(e + 0x32) += (short)heading;
            return true;
        }

        if ((heading & 0xfff) >= 0x800) step = -step;

        *(short*)(e + 0x32) += (short)step;
        return false;
    }

    /// <summary>
    /// Ghidra: <c>0x0049e651..0x0049e8f7</c> — <b>le troisieme temps</b> de
    /// <see cref="TickSceneEntityAi_0049E190"/> : trois bandes de distance, un tirage sur cent
    /// dans chacune.
    ///
    /// <para>
    /// <b>Sous <c>0xfa</c></b> : trois fois sur dix elle frappe — a condition d'etre dans le cone,
    /// dont la largeur croit avec la timidite, et a portee tiree ; sinon elle hesite. De 30 a 59
    /// elle tient sa distance a <c>0x64</c>. Au-dela elle recule (<see cref="WanderSceneEntity"/>
    /// avec un demi-tour).
    /// </para>
    /// <para>
    /// <b>Entre <c>0xfa</c> et <c>0x1f4</c></b> : quarante-cinq fois sur cent elle charge, de 45 a
    /// 69 elle avance, de 70 a 89 elle tient sa distance, au-dela elle se repose.
    /// </para>
    /// <para>
    /// <b>Au-dela de <c>0x1f4</c></b> : trois fois sur dix elle charge, de 30 a 59 elle se repose,
    /// et le reste du temps elle part dans une direction <b>entierement tiree au sort</b> —
    /// <c>+0x10</c> recoit un <c>rand()</c> brut, pas un ecart autour de la cible.
    /// </para>
    /// </summary>
    private static void ReplanSceneEntityAction_0049E190(byte* e, int boldness, int timid)
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

            if (roll < 0x1e)
            {
                if (!InCone() || distance >= CrtRand() % (timid * 2 + 1) + 0x50)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                uint state = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (state & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x3c)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            WanderSceneEntity(e, boldness, timid, heading, 0x800);
            return;
        }

        // ================================================================ l'approche
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                if (!InCone()) HesitateSceneEntity(e);
                else ChargeSceneEntity_0049E876(e);
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
        {
            int roll = CrtRand() % 100;

            if (roll < 0x1e)
            {
                if (!InCone()) HesitateSceneEntity(e);
                else ChargeSceneEntity_0049E876(e);
                return;
            }

            if (roll < 0x3c)
            {
                RestSceneEntity(e, timid);
                return;
            }

            e[0x42] = 3;
            e[0x43] = SceneEntityDelay(timid);
            *(ushort*)(e + 0x10) = (ushort)CrtRand();
        }
    }

    /// <summary>Ghidra: <c>LAB_0049e876</c> — <b>la charge</b> : pose <c>0x1000804</c> d'un coup et
    /// arme l'etat 3 du geste. Le binaire partage cette etiquette entre les deux bandes qui
    /// chargent.</summary>
    private static void ChargeSceneEntity_0049E876(byte* e)
    {
        *(int*)(e + 0x18) = 4;
        *(uint*)e |= 0x1000804;
        *(short*)(e + 0x10) = 3;
    }
}
