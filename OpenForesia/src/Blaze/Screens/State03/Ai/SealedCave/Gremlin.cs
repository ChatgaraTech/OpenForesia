// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GREMLIN DE LA GROTTE SCELLEE — 0x004F3350, sous-type 4 de la grotte scellee (zone 10, sous-zones 5 et 6).
//
// Deux copies conformes dans la meme zone : 0x004F5630 et 0x004FC9B0. 3 380 octets de
// code, absents de `Blaze.c`, table de sauts a treize entrees.
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
    // 0x004F3350 — LE GREMLIN DE LA GROTTE SCELLEE, sous-type 4 (zone 10, sous-zones 5 et 6), avec
    // deux copies conformes dans la meme zone (0x004F5630, 0x004FC9B0). Transcrite le 2026-09-15 au
    // desassembleur (Legacy/Raw/Blaze/FUN_004F3350.asm, 3 380 octets de code, absente de Blaze.c),
    // table de sauts a treize entrees a 0x004F4064.
    //
    // UN GREMLIN QUI N'EST PAS LE GREMLIN. Le Gremlin de la vieille tour (0x004A7190) est une volante
    // a sort ; celui-ci est un TERRESTRE, sur la charpente du Varan (FUN_00488160, pose au sol,
    // heurt), sans vol, sans sort, sans variante. Deuxieme cas, apres la Crapouille, ou le nom
    // d'espece ne dit rien du code.
    //
    // CE QUI LUI APPARTIENT :
    //
    //   - UN GESTE QUI FRAPPE A CHAQUE IMAGE : tant que le bit 2 tient, la sonde est croisee a chaque
    //     image, a pleine attaque, sans condition d'image d'animation.
    //   - TROIS FACONS DE PORTER CE GESTE : le BOND (posture 6 : il s'elance a l'image 10, poussee
    //     hardiesse * 8 + 0x14000, et frappe jusqu'a retomber), la CHARGE (posture 0x0b : immediate
    //     s'il est hardi, apres l'image 6 sinon, poussee 0x1e000 jusqu'a la fin de l'animation), et
    //     aucun coup a l'arret.
    //   - LA POSTURE 12 FAIT NAITRE L'EFFET 0xdc a la fin de son animation, si la cible est a moins de
    //     0x9c4 — en neutralisant le +0x9a de l'entite le temps de l'appel. Et une entite figee dans
    //     cette posture en sort, a la fin de chaque image ou elle ne bouge pas.
    //   - LE HEURT NE SAUVEGARDE NI +0x60 NI +0x64, et heurte EN L'AIR il efface aussi le bit de
    //     geste — deux ecarts avec SettleSceneEntityOnGroundAndWalls, d'ou la queue ecrite en ligne.
    //   - TROP PRES — sous 0x64 —, LA REPLANIFICATION S'ECRASE : quel que soit le choix de la plage
    //     de contact, la posture devient 3 (filer).
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004f3350</c> (et ses deux copies) — <b>l'intelligence du Gremlin de la grotte
    /// scellee</b>.
    /// </summary>
    private static void TickSceneEntityAi_004F3350(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x58] — la sonde, la meme a six mots que celle du Varan.
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
            PickChasedPartyMember_004A1020(e, 2, 0);   // Ghidra: FUN_004ef860, copie de FUN_004a1020
        }

        // Ghidra: 0x004f339e — neuf champs et le cap : ni +0x60 ni +0x64.
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
        int dy = (*(int*)(e + 0x20) - *(int*)(chased + 0x20)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideCaveGremlinAction_004F3350(e, probe, boldness, distance, heading, speed);
        }

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        SettleCaveGremlin_004F3B71(e, boldness, &keep);
        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;

            // Ghidra: 0x004f3ce4 — a l'arret, la posture 12 est rendue.
            if (e[0x42] == 0x0c)
            {
                e[0x43] = 1;
                e[0x42] = 0;
            }

            return;
        }

        if (e[0x43] != 0) return;

        ReplanCaveGremlinAction_004F3350(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004f349c..0x004f3ad0</c> — <b>le premier temps</b>. Treize cases dont huit
    /// servies : 0 attendre, 1 se tourner en marchant, 2 approcher, 3 filer, 6 bondir, 7 marcher
    /// droit, 11 charger, 12 l'effet.
    /// </summary>
    private static void DecideCaveGremlinAction_004F3350(
        byte* e, short* probe, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                 // Ghidra: 0x004f349c, en ligne

        uint flags = *(uint*)e;
        flags = e[0x42] == 5 ? flags | 0x10 : flags & 0xffffffef;
        *(uint*)e = flags;

        // ================================================================ le geste
        if ((*(uint*)e & 2) != 0)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(ushort*)(prey + 0x96) = 0x10;
                *(uint*)prey |= 0x40;

                ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
            }
        }

        // ================================================================ la posture
        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
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

            // ---------------------------------------------------------- 1 : se tourner en marchant
            case 1:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x18) = 1;
                    *(ushort*)(e + 0x1a) = 0;
                    *(ushort*)(e + 0x12) = 1;

                    // Il ne tourne pas sur place : une petite poussee le fait avancer en virant.
                    *(int*)(e + 0x5c) = 0x3000;
                }

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    return;
                }

                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x32 : -0x32 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 3;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x1a) = 0;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0xc000;
                }

                if (*(ushort*)(e + 0x12) != 1) return;

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) <= 0x200) return;

                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x32 : -0x32 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : filer
            case 3:
                if (*(ushort*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    int thrust = boldness * 0x90 + 0x6000;
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

                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x18) = 1;
                *(ushort*)(e + 0x1a) = 0;
                *(ushort*)(e + 0x12) = 1;
                return;

            // ---------------------------------------------------------- 6 : bondir
            case 6:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    uint f = *(uint*)e;

                    if ((f & 1) != 0)
                    {
                        e[0x43] = 0;
                    }
                    else
                    {
                        *(uint*)e = f & 0xfeffffff;
                        *(ushort*)(e + 0x18) = 0;
                        *(ushort*)(e + 0x1a) = 0;
                        *(ushort*)(e + 0x12) = 1;
                    }
                }

                // A l'image 10 de la prise d'elan, il saute — et le saut EST le geste.
                if (*(ushort*)(e + 0x12) == 1 && *(short*)(e + 0x1a) >= 10)
                {
                    int thrust = boldness * 8 + 0x14000;
                    *(uint*)e &= 0xfeffffff;
                    int step = (speed * thrust) >> 0xc;

                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                    *(ushort*)(e + 0x12) = 2;
                    *(int*)(e + 0x54) += 0x14000;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;

                    *(uint*)e |= 3;
                }

                if (*(ushort*)(e + 0x12) != 2) return;
                if ((*(uint*)e & 1) != 0) return;

                // Retombe : le geste finit.
                *(uint*)e &= 0xfffffffd;
                e[0x43] = 0;
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

                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x18) = 1;
                *(ushort*)(e + 0x1a) = 0;
                *(ushort*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x7000;
                return;
            }

            // ---------------------------------------------------------- 11 : charger
            case 11:
                CaveGremlinCharge_004F38D7(e, boldness, heading);
                return;

            // ---------------------------------------------------------- 12 : l'effet
            case 12:
                CaveGremlinConjure_004F39DD(e, boldness, distance, heading);
                return;

            // ---------------------------------------------------------- 4, 5, 8, 9, 10 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004f38d7..0x004f39dc</c> — <b>la charge</b>, posture 11. Un Gremlin hardi —
    /// plus de 0x32 — s'elance des la premiere image, sur l'animation 5 ; les autres se tournent
    /// d'abord et partent a l'image 7 de leur animation, sur la 3. Dans les deux cas la poussee est
    /// <c>0x1e000</c> et le geste dure jusqu'a la fin de l'animation.
    /// </summary>
    private static void CaveGremlinCharge_004F38D7(byte* e, int boldness, int heading)
    {
        if (*(ushort*)(e + 0x12) == 0)
        {
            uint f = *(uint*)e & 0xfeffffff;
            *(ushort*)(e + 0x12) = 1;
            *(ushort*)(e + 0x18) = 0;
            *(ushort*)(e + 0x1a) = 0;
            *(uint*)e = f;

            if (boldness > 0x32)
            {
                *(uint*)e = (f & 0xfdffffff) | 2;
                *(ushort*)(e + 0x18) = 5;
                *(ushort*)(e + 0x1a) = 0;
                *(ushort*)(e + 0x12) = 2;
                *(int*)(e + 0x5c) = 0x1e000;
            }
        }

        if (*(ushort*)(e + 0x12) == 1)
        {
            TurnCaveGremlinTowardTarget(e, boldness, heading);

            if (*(short*)(e + 0x1a) > 6)
            {
                *(uint*)e = (*(uint*)e & 0xfdffffff) | 2;
                *(ushort*)(e + 0x18) = 3;
                *(ushort*)(e + 0x1a) = 0;
                *(ushort*)(e + 0x12) = 2;
                *(int*)(e + 0x5c) = 0x1e000;
            }
        }

        if (*(ushort*)(e + 0x12) != 2) return;

        TurnCaveGremlinTowardTarget(e, boldness, heading);

        uint g = *(uint*)e;
        if ((g & 0x2000000) == 0) return;

        *(uint*)e = g & 0xfffffffd;
        e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004f39dd..0x004f3ad0</c> — <b>l'effet</b>, posture 12 : l'animation 4 jouee en
    /// entier, en regardant la cible, puis — si la cible est a moins de 0x9c4 — l'entite d'effet
    /// <c>0xdc</c> nait. Le <c>+0x9a</c> de l'entite est mis a zero le temps de l'appel et rendu
    /// ensuite : l'effet ne doit pas heriter de son balancement vertical. La posture tient ensuite
    /// le temps de son rebours.
    /// </summary>
    private static void CaveGremlinConjure_004F39DD(byte* e, int boldness, int distance, int heading)
    {
        if (*(ushort*)(e + 0x12) == 0)
        {
            *(ushort*)(e + 0x18) = 4;
            *(uint*)e &= 0xfcffffff;
            *(ushort*)(e + 0x1a) = 0;
            *(ushort*)(e + 0x12) = 1;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(ushort*)(e + 0x12) == 1)
        {
            TurnCaveGremlinTowardTarget(e, boldness, heading);

            if ((*(uint*)e & 0x2000000) != 0)
            {
                *(ushort*)(e + 0x12) = 2;

                if (distance < 0x9c4)
                {
                    short bob = *(short*)(e + 0x9a);
                    *(short*)(e + 0x9a) = 0;
                    SpawnEffectEntity(e, 0xdc, 0, -1);             // Ghidra: FUN_0047a9d0
                    *(short*)(e + 0x9a) = bob;
                }
            }
        }

        if (*(ushort*)(e + 0x12) != 2) return;

        TurnCaveGremlinTowardTarget(e, boldness, heading);
        if (e[0x43] != 0) e[0x43]--;
    }

    /// <summary>Ghidra: le virage commun a la charge et a l'effet — hors d'un cone de
    /// <c>±0x100</c>, <c>hardiesse * 2 + 0x32</c> vers la cible.</summary>
    private static void TurnCaveGremlinTowardTarget(byte* e, int boldness, int heading)
    {
        if (((heading + 0x100) & 0xfff) <= 0x200) return;

        *(ushort*)(e + 0x32) +=
            (ushort)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x32 : -0x32 - boldness * 2);
    }

    /// <summary>
    /// Ghidra: <c>0x004f3b71..0x004f3cc4</c> — <b>la pose au sol et le heurt</b>, la forme de
    /// <see cref="SettleSceneEntityOnGroundAndWalls"/> au masque <c>0x2000ad82</c>, avec trois ecarts
    /// verifies au desassembleur : heurte en l'air, le bit de geste tombe aussi ; heurte au sol, la
    /// posture 7 et son rebours sont reposes <b>meme s'il y etait deja</b> ; et <c>+0x60</c> et
    /// <c>+0x64</c>, jamais sauvegardes, ne sont pas rendus.
    /// </summary>
    private static void SettleCaveGremlin_004F3B71(byte* e, int boldness, SceneEntityPlacement* keep)
    {
        uint contact = *(uint*)(e + 0x90) & 0xfffffcff;
        *(uint*)(e + 0x90) = contact;

        int floor = *(int*)(e + 0x60);

        if (*(int*)(e + 0x20) <= floor && (contact & 0x8000) == 0)
        {
            uint landing = *(uint*)e;

            *(int*)(e + 0x20) = floor;
            *(uint*)e = landing & 0xfffffffe;
            *(int*)(e + 0x54) = 0;

            if ((landing & 1) != 0) return;
        }

        uint flags = *(uint*)e;
        if ((flags & 1) == 0 && *(int*)(e + 0x20) > floor) *(uint*)e = flags | 1;

        if ((contact & 0x2000ad82) == 0) return;

        uint hit = *(uint*)e;

        if ((hit & 1) != 0)
        {
            *(uint*)e = hit & 0xfffffffd;
            *(int*)(e + 0x5c) = 0;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x58) = 0;
            *(int*)(e + 0x1c) = keep->X;
            *(int*)(e + 0x24) = keep->Z;
            e[0x42] = 0;
            e[0x43] = 0x28;
            *(ushort*)(e + 0x12) = 0;
            *(ushort*)(e + 0x2c) = 0x1000;
            return;
        }

        e[0x42] = 7;
        int roll = CrtRand();                                       // Ghidra: FUN_00547140

        *(uint*)e &= 0xfffffffd;
        *(int*)(e + 0x24) = keep->Z;
        *(int*)(e + 0x20) = keep->Y;
        *(uint*)(e + 0x90) = keep->At90;
        *(int*)(e + 0x80) = keep->At80;
        *(ushort*)(e + 0x12) = 0;
        *(ushort*)(e + 0x10) = 0x50;
        *(int*)(e + 0x84) = keep->At84;
        *(int*)(e + 0x8c) = keep->At8c;
        e[0x43] = (byte)(roll % 0x28 + 0x14);
        *(int*)(e + 0x1c) = keep->X;
        *(int*)(e + 0x7c) = keep->At7c;
        *(int*)(e + 0x88) = keep->At88;

        int side = ProbeClearSide_004D5E60(e, 0x2000ad82);
        if ((short)side == 0) side = (CrtRand() % 2) * 2 - 1;       // Ghidra: FUN_00547140

        *(ushort*)(e + 0x32) += (ushort)((boldness + 0x40) * side);
    }

    /// <summary>
    /// Ghidra: <c>0x004f3d08..0x004f4059</c> — <b>le troisieme temps</b>. Trois plages bornees a
    /// 0xe6 et 0x258.
    ///
    /// <para><b>Au contact</b>, la moitie des tirages tentent la charge dans le cone de timidite —
    /// sans lever le bit de geste : c'est la posture 11 qui le levera en partant. Mais <b>sous 0x64
    /// tout est ecrase</b> par la posture 3, et c'est la seule plage du depot ou la replanification
    /// se reecrit elle-meme. <b>A mi-distance</b>, une fois sur cinq, l'effet (posture 12) pour un
    /// rebours long, <c>hasard % 30 + hardiesse / 2 + 30</c>.</para>
    /// </summary>
    private static void ReplanCaveGremlinAction_004F3350(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        *(ushort*)(e + 0x12) = 0;

        // ================================================================ le contact
        if (distance < 0xe6)
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
                    // Ghidra: 0x004f3db3 — armer LA CHARGE, sans le bit de geste.
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(uint*)e &= 0xfeffffff;
                    *(int*)(e + 0x5c) = 0;
                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                }
                else
                {
                    e[0x42] = 0;
                    int wait = CrtRand() % 10;                      // Ghidra: FUN_00547140
                    *(ushort*)(e + 0x10) = 0x50;
                    *(ushort*)(e + 0x12) = 0;
                    e[0x43] = (byte)(wait + (byte)(timid >> 2));
                }
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 1;
                e[0x43] = 1;
            }
            else if (roll < 0x46 && distance > boldness + 0x64)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();             // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
            }

            // Ghidra: LAB_004f3e3f — trop pres, tout ce qui precede est ecrase.
            if (distance < 0x64)
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = (byte)(CrtRand() % 5 + 10);               // Ghidra: FUN_00547140
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);  // Ghidra: FUN_00547140
            }

            return;
        }

        // ================================================================ la mi-distance
        if (distance < 0x258)
        {
            int roll = CrtRand() % 100;                             // Ghidra: FUN_00547140

            if (roll < 0x14)
            {
                e[0x42] = 0x0c;
                e[0x43] = (byte)(CrtRand() % 0x1e + (byte)(boldness >> 1) + 0x1e);   // Ghidra: FUN_00547140
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                e[0x43] = SceneEntityDelay(timid);
                *(ushort*)(e + 0x10) = 0x50;
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 6;
                e[0x43] = 1;
                *(short*)(e + 0x10) = (short)CrtRand();             // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
            }
            else if (roll < 0x50)
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);  // Ghidra: FUN_00547140
            }
            else
            {
                e[0x42] = 0;
                e[0x43] = 1;
                *(ushort*)(e + 0x12) = 0;
                *(ushort*)(e + 0x10) = 0x50;
            }

            return;
        }

        // ================================================================ le lointain
        int far = CrtRand() % 100;                                  // Ghidra: FUN_00547140

        if (far < 0x1e)
        {
            if (far < boldness * 2 + 0x0a)
            {
                e[0x42] = 2;
                e[0x43] = SceneEntityDelay(timid);
                *(ushort*)(e + 0x10) = 0x64;
                return;
            }

            e[0x42] = 0;
            int wait = CrtRand() % 10;                              // Ghidra: FUN_00547140
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = (byte)(wait + (byte)(timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x28)
        {
            e[0x42] = 3;
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = SceneEntityDelay(timid);
            *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);      // Ghidra: FUN_00547140
            return;
        }

        if (far < 0x3c)
        {
            e[0x42] = 1;
            e[0x43] = 1;
            return;
        }

        if (far < 0x50)
        {
            e[0x42] = 2;
            e[0x43] = SceneEntityDelay(timid);
            *(ushort*)(e + 0x10) = 0x64;
            return;
        }

        e[0x42] = 6;
        e[0x43] = 1;
        *(short*)(e + 0x10) = (short)CrtRand();                     // Ghidra: FUN_00547140
    }
}
