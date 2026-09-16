// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ABEILLE TUEUSE — 0x004A3C30, sous-type 3 du bloc C de la foret (zone 3, sous-zones 5 et 9).
//
// La TROISIEME BETE VOLANTE du depot. Meme vol que la Coccinelle, `BeatWings_0049A68F`
// partage au mot pres ; mais sa posture 3 est une RUEE, et son bond de corps a corps part
// a +0x800 du cap vise - elle recule en restant face au groupe.
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
    // 0x004A3C30 — LA TROISIEME BETE VOLANTE, sous-type de la table 0x0056ad48 des sous-zones 5 et
    // 9 de la foret. Transcrite le 2026-09-01 au desassembleur (Legacy/Raw/Blaze/FUN_004A3C30.asm,
    // 1583 octets, absente de Blaze.c). Quatre postures, table de sauts a 0x004A4260.
    //
    // LES TROIS SONT LA COCCINELLE 0x0049A360, L'ABEILLE 0x0049B220 ET CELLE-CI. Toutes trois
    // partagent la meme queue — IntegrateFlyingEntityMotion_0049A8E0, BeatWings_0049A68F,
    // InterruptFlyingEntity_004A1CF0 —, ce qui reste le tri le plus sur de la famille.
    //
    // ELLE VOLE COMME LA COCCINELLE et se bat comme personne d'autre. Le tri par la fonction
    // d'integration, recommande dans Comportement.md, la classe tout de suite : FUN_0049a8e0 et non
    // FUN_00488160, FUN_004a1cf0 et non FUN_004e3770, et le meme battement d'ailes au mot pres —
    // BeatWings_0049A68F est partage tel quel, les deux corps sont identiques sur ce bloc.
    //
    // TROIS CHOSES LA SEPARENT DE SA SŒUR, ET LA TROISIEME N'APPARTIENT QU'A ELLE :
    //
    //   1. son geste a une IMAGE DE FRAPPE — la seizieme, une seule — la ou la Coccinelle balaie
    //      le groupe a chaque image du geste ; et il finit quand l'ANIMATION finit (le bit
    //      0x2000000 que AdvanceEntityAnimation_004DB5A0 pose sur sa derniere image), non sur un
    //      compte a rebours ;
    //   2. sa sonde ne porte AUCUN decalage vertical (probe[4] = 0, la Coccinelle a -0x100) : elle
    //      vole, mais elle frappe a hauteur d'homme ;
    //   3. sa posture 3 n'est pas une errance, c'est une RUEE. Voir le resume de
    //      TickSceneEntityAi_004A3C30.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004a3c30</c> — <b>l'ABEILLE TUEUSE</b>, sous-type 3 du bloc C de la foret (les
    /// sous-zones 5 et 9, le lieu de coupe et la grotte) et <b>la troisieme bete volante du
    /// depot</b>. L'espece vient du croisement <c>especes.csv</c> x <c>modeles.csv</c> consigne
    /// dans <c>Documentation/Ennemies/Comportement.md</c> ; le bloc A porte une autre « Abeille
    /// tueuse » sous <see cref="TickSceneEntityAi_0049B220"/>, ce qui rappelle que le nom d'espece
    /// ne determine pas le comportement. 1583 octets, <b>absente de <c>Blaze.c</c></b> comme toute
    /// la famille ; releve garde sous <c>Legacy/Raw/Blaze/FUN_004A3C30.asm</c>.
    ///
    /// <para>
    /// Meme charpente en trois temps que ses voisines — decider, appliquer, replanifier — et quatre
    /// postures : <b>0</b> attendre, <b>1</b> se tourner, <b>2</b> tenir la distance, <b>3</b> se
    /// ruer. Le champ <c>+0x10</c> sert aux deux dernieres, mais pas au meme titre : la posture 2 y
    /// lit une <b>distance</b>, la posture 3 un <b>cap</b>.
    /// </para>
    ///
    /// <para>
    /// <b>Sa posture 3 est une ruee, et c'est ce qui la distingue de toute la famille.</b> La ou
    /// ses voisines marchent vers un cap par petits pas, celle-ci se donne une <b>impulsion</b> :
    /// le compte a rebours <c>+0x43</c> vaut 2 a l'entree, l'image ou il vaut 2 pose la poussee
    /// <c>(timidite + 0x120) &lt;&lt; 8</c> et la verse d'un coup dans les deux composantes de
    /// vitesse <c>+0x50</c> et <c>+0x58</c> par le sinus et le cosinus du cap <c>+0x10</c>, puis
    /// <b>remet la poussee a zero</b> ; l'image suivante ne fait plus qu'attendre que la vitesse
    /// retombe sous <c>hardiesse/8 + 1</c>. Elle est <b>lancee</b>, elle ne marche pas.
    /// </para>
    /// <para>
    /// <b>Et pendant toute la ruee elle reste face a la cible</b> : la posture 3 finit par
    /// <c>+0x32 += cap relatif</c>, a chaque image, quelle que soit la direction de l'impulsion.
    /// Comme la ruee du corps a corps part a <c>+0x800</c> du cap vise — un demi-tour, donc a
    /// l'oppose du groupe —, <b>elle recule en vous regardant</b>. Celle de la distance moyenne
    /// part sans decalage : elle fonce. Le meme mecanisme sert au bond en avant et au bond en
    /// arriere, le biais seul change.
    /// </para>
    /// <para>
    /// <b>Ses trois plages de distance sont plus serrees que celles de la Coccinelle</b> :
    /// <c>0xfa</c> et <c>0x1f4</c> au lieu de <c>0x12c</c> et <c>0x258</c>. Sous <c>0xfa</c>, trois
    /// chances sur dix d'engager — et l'engagement demande en plus un cap dans le cone et une
    /// distance sous <c>0x64</c> augmentee d'un tirage ; deux sur dix de tenir la distance ; cinq
    /// sur dix de reculer d'un bond. Entre les deux seuils, elle hesite (45 %), fonce (25 %), tient
    /// (20 %) ou se repose (10 %). Au-dela elle se repose, hesite ou se rue vers un cap
    /// <b>entierement tire au sort</b>.
    /// </para>
    /// <para>
    /// <b>Son engagement ne change pas de posture</b> : il pose le bit 2, force l'animation 3,
    /// efface le bit de bouclage <c>0x1000000</c>, met le compte a rebours a 1 et annule la
    /// poussee. La posture <c>+0x42</c> reste celle d'avant, et c'est elle qui reprend a la fin du
    /// geste.
    /// </para>
    /// </summary>
    private static void TickSceneEntityAi_004A3C30(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x1e;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;          // aucun decalage vertical : elle vole, mais elle frappe a plat
        probe[5] = 0x50;

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        // ---------------------------------------------------------------- 1. decider
        DecideSceneEntityAction_004A3C30(e, probe, boldness, distance, heading, speed, timid, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        BeatWings_0049A68F(e);
        InterruptFlyingEntity_004A1CF0((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanSceneEntityAction_004A3C30(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a3cb8..0x004a3f59</c> — <b>le premier temps</b> de
    /// <see cref="TickSceneEntityAi_004A3C30"/>.
    /// </summary>
    private static void DecideSceneEntityAction_004A3C30(
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
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffffffd;
                return;
            }

            // L'image de frappe est la seizieme, et une seule : la Coccinelle, elle, balaie a
            // chaque image du geste.
            if (*(short*)(e + 0x1a) == 0x10)
            {
                byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

                if (*Mem.Camera(0x005de0b8) != 0)
                {
                    *(ushort*)(victim + 0x96) = 0x10;
                    *(uint*)victim |= 0x40;

                    ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
                    Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));
                }
            }

            // Le geste s'arrete quand l'ANIMATION s'arrete : le bit 0x2000000 est celui que
            // AdvanceEntityAnimation_004DB5A0 pose sur la derniere image d'un clip non boucle.
            flags = *(uint*)e;
            if ((flags & 0x2000000) == 0) return;

            e[0x43] = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        // ================================================================ la posture tenue
        if ((flags & 0x10) != 0) return;
        if ((flags & 0x804) != 0) return;

        byte held = e[0x42];
        if (held > 3) return;

        // Le cone de virage des postures 1 et 2 : deux fois la hardiesse et 0x80, soit bien plus
        // large que celui de la Coccinelle (hardiesse/4 + 0x38). Elle se retourne vite.
        int cone = boldness * 2 + 0x80;

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

            // ---------------------------------------------------------- 2 : tenir la distance
            case 2:
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                // La distance tenue est lue dans +0x10, ou la replanification l'a posee.
                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                TurnSceneEntityTowardTarget(e, cone, heading);
                RampFlyingEntityPush(e, (boldness + 0x200) << 7);
                return;

            // ---------------------------------------------------------- 3 : se ruer
            default:
            {
                if (*(ushort*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (e[0x43] == 1)
                {
                    // La ruee est finie quand la vitesse est retombee. La mesure est en deux
                    // dimensions, sur +0x50 et +0x58, sans la composante verticale du vol.
                    int vx = *(int*)(e + 0x50) >> 0xc;
                    int vz = *(int*)(e + 0x58) >> 0xc;

                    if ((int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz)) <= (boldness >> 3) + 1)
                        e[0x43] = 0;
                }
                else if (e[0x43] == 2)
                {
                    // L'impulsion, une seule image. La poussee est posee, versee dans les deux
                    // composantes de vitesse, puis remise a zero : rien ne la maintient.
                    int push = (timid + 0x120) << 8;
                    *(int*)(e + 0x5c) = push;

                    short bearing = *(short*)(e + 0x10);

                    *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(bearing) * push >> 0xc;

                    int forward = Winsys3d.GetCosTableEntry(bearing) * *(int*)(e + 0x5c);
                    *(int*)(e + 0x5c) = 0;
                    *(int*)(e + 0x58) += forward >> 0xc;

                    e[0x43]--;
                }

                // Elle reste face a la cible pendant toute la ruee, quelle que soit la direction
                // de l'impulsion.
                *(ushort*)(e + 0x32) += (ushort)heading;
                return;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a4023..0x004a4256</c> — <b>le troisieme temps</b> : quelle posture prendre
    /// pour la suite.
    ///
    /// <para>
    /// <b>La distance et le cap sont recalcules ici, en deux dimensions</b>, comme chez toutes ses
    /// voisines : la hauteur est ignoree, une bete volante ne renonce pas parce que vous etes plus
    /// bas qu'elle.
    /// </para>
    /// </summary>
    private static void ReplanSceneEntityAction_004A3C30(byte* e, int boldness, int timid)
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

            if (roll < 0x1e)
            {
                if (((heading + timid * 4 + 0x100) & 0xfff) > timid * 8 + 0x200)
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // Le seuil AUGMENTE du tirage : plus elle est timide, plus loin elle accepte
                // d'engager. C'est l'inverse de la Coccinelle, qui le diminue.
                if (distance >= 0x64 + CrtRand() % (timid * 2 + 1))
                {
                    HesitateSceneEntity(e);
                    return;
                }

                // L'engagement ne touche pas a +0x42 : la posture d'avant reprend a la fin du
                // geste.
                uint flags = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                e[0x43] = 1;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (flags & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x3c)
            {
                HoldSceneEntityAtRange(e, timid, 0x64);
                return;
            }

            // 0x800 est un demi-tour : elle bondit en arriere, tout en restant face au groupe.
            LungeSceneEntity_004A3C30(e, boldness, heading, 0x800);
            return;
        }

        // ================================================================ la distance moyenne
        if (distance < 0x1f4)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x2d)
            {
                HesitateSceneEntity(e);
                return;
            }

            if (roll < 0x46)
            {
                LungeSceneEntity_004A3C30(e, boldness, heading, 0);
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
            RestSceneEntity(e, timid);
            return;
        }

        if (far < 0x3c)
        {
            HesitateSceneEntity(e);
            return;
        }

        // Ghidra: LAB_004a4246 — la seule sortie ou le cap est ENTIEREMENT tire au sort, sans
        // reference a la cible : trop loin pour qu'elle compte.
        e[0x42] = 3;
        e[0x43] = 2;
        *(short*)(e + 0x10) = (short)CrtRand();
    }

    /// <summary>Ghidra: LAB_004a4120 et LAB_004a418a — <b>la ruee</b> : posture 3 pour deux images,
    /// le cap etant celui de la cible plus un ecart tire dans une fourchette large de
    /// <c>(hardiesse + 0x80) * 16</c>, centree, plus <paramref name="bias"/>.
    ///
    /// <para>C'est la jumelle de <see cref="WanderSceneEntity"/>, a un detail pres qui change tout :
    /// le compte a rebours vaut <b>2</b> et non une duree tiree. Deux images, c'est exactement ce
    /// qu'il faut a la posture 3 de cette espece — une pour l'impulsion, une pour attendre qu'elle
    /// retombe.</para></summary>
    private static void LungeSceneEntity_004A3C30(byte* e, int boldness, int heading, int bias)
    {
        e[0x42] = 3;
        e[0x43] = 2;

        int spread = (boldness + 0x80) << 4;
        int offset = CrtRand() % spread - (spread >> 1);

        *(short*)(e + 0x10) = (short)(offset + heading + *(short*)(e + 0x32) + bias);
    }
}
