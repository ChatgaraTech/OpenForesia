// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// FOUGRE — 0x004E20F0, vallee d'argent (zone 8, sous-zones 0 et 1). Le nom vient de
// Comportement.md.
//
// Absent de Blaze.c ; transcrit au desassembleur le 2026-09-19
// (Legacy/Raw/Blaze/SilverValley/FUN_004E20F0.asm, 3 184 octets), table de sauts a quatorze
// entrees a 0x004e2d24.
//
// LA SŒUR DU GRIFON (`Griffin.cs`, dont elle emprunte les aides) : meme queue, memes postures
// de base, meme pique. Ce qui la fait :
//
//   - SA HARDIESSE EST ECRASEE PAR 0x40 juste apres la lecture de la situation, comme celle du
//     Loup-garou et du Tigre du chateau ; sa timidite n'est jamais calculee ;
//   - une sonde plus longue et plus haute, (0, 80, 100, 150, 0, 150) ;
//   - L'ACCROCHE NE FRAPPE PAS : la posture 0x0b rejoint la hauteur de la proie, et le prelude se
//     contente d'y relever +0x1a dans un emplacement de pile que personne ne relit ;
//   - UNE POSTURE DE PLUS, 0x0d : un souffle — a l'image 20 de son animation 8, l'effet 0xc4 ;
//   - un pique plus court (monte a 180, fond vingt images, figee a l'image 13) et plus lent a
//     s'arreter (sous 0xf000 au lieu de 0xa000).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x004e20f0</c> — <b>l'intelligence de Fougre</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004E20F0(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x28] — la sonde.
        short* probe = stackalloc short[6];
        probe[0] = 0; probe[1] = 0x50; probe[2] = 0x64; probe[3] = 0x96; probe[4] = 0; probe[5] = 0x96;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);             // Ghidra: FUN_004e0750, copie de FUN_004a1020
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
        }

        *(uint*)e &= 0xfffffffe;

        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);   // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // Ghidra: 0x004e21f9 — `mov dword [esp+10h],40h` : la hardiesse lue est jetee.
        boldness = 0x40;

        CancelGestureOnTrait_004D5DE0(e, traits);
        distance = ValleyFlyerFlatDistance(e, chased);

        if (speed != 0)
        {
            DecideFougreAction_004E2245(e, probe, model, chased, boldness, distance, heading, speed);
            BobValleyFlyer(e, chased, 0x0c, 0x0b, 0x0d);
        }

        if (!SettleValleyFlyer(e, speed, &keep, boldness)) return;

        ReplanFougreAction_004E2B48(e);
    }

    /// <summary>Ghidra: <c>0x004e2245..0x004e28fc</c> — le premier temps de Fougre. Quatorze
    /// cases, neuf servies : celles du Grifon (a d'autres animations) et le souffle
    /// <c>0x0d</c>.</summary>
    private static void DecideFougreAction_004E2245(
        byte* e, short* probe, byte* model, byte* chased, int boldness, int distance, int heading, int speed)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // recopiee en ligne

        // ================================================================ le prelude de geste
        // Ghidra: 0x004e22b6 — pour l'accroche, le prelude range +0x1a dans [esp+0x68] et rien
        // ne relit cet emplacement. Il n'y a pas de coup.
        if ((*(uint*)e & 2) != 0 && e[0x42] == 0x0c) ValleyFlyerDiveStrike(e, probe, model);

        switch (e[0x42])
        {
            case 0x00: HoldValleyFlyerStill(e, distance, animation: 5); break;
            case 0x01: TurnValleyFlyer(e, boldness, heading, animation: 5); break;
            case 0x02: ApproachValleyFlyer(e, boldness, distance, heading, animation: 6); break;
            case 0x03: RunValleyFlyerCourse(e, boldness, heading, speed, animation: 6); break;
            case 0x07: WalkValleyFlyer(e, boldness, distance, animation: 6); break;

            // ---------------------------------------------------------- 9 : virer d'un quart de tour
            // A la difference du Grifon, le cap vise est RELATIF : le cap courant plus ±0x400.
            case 0x09:
                if (*(short*)(e + 0x12) == 0)
                {
                    short yaw = *(short*)(e + 0x32);
                    int draw = CrtRand();
                    *(short*)(e + 0x12) = 1;
                    *(short*)(e + 0x18) = 6;
                    *(short*)(e + 0x1a) = 0;
                    *(short*)(e + 0x10) = (short)(SignedMask(draw, 0x7ff) + yaw - 0x400);
                    *(short*)(e + 0x32) = (short)(yaw & 0xfff);
                    *(uint*)e |= 0x1000000;
                }

                SteerValleyFlyerToCourse(e);
                break;

            case 0x0b: ClingValleyFlyer(e, chased, animation: 3); break;
            case 0x0c: DiveValleyFlyer(e, boldness, heading, chased, climb: 0xb4, diveFrames: 0x14,
                                        diveFlags: 2, keepFlags: 0xfeffffff, diveAnimation: 6,
                                        clampFrame: 0x0d, slowFloor: 0xf000, restAnimation: 5); break;

            // ---------------------------------------------------------- 0x0d : le souffle
            case 0x0d:
                if (*(short*)(e + 0x12) == 1)
                {
                    if (*(short*)(e + 0x1a) == 0x14) SpawnEffectEntity(e, 0xc4, 0, -1);   // Ghidra: FUN_0047a9d0
                    if ((*(uint*)e & 0x2000000) != 0) e[0x43] = 0;

                    int y = *(int*)(e + 0x20);
                    *(int*)(e + 0x20) = ((*(int*)(chased + 0x20) - y) >> 2) + y;
                }

                if (*(short*)(e + 0x12) != 0) break;

                *(short*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                *(short*)(e + 0x18) = 8;
                *(short*)(e + 0x1a) = 0;
                *(int*)(e + 0x5c) = 0;
                break;

            default: break;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004e2b48..0x004e2d22</c> — <b>le troisieme temps</b> de Fougre. Sans
    /// hardiesse ni timidite : les delais sont fixes (20 a 29 images).
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///   <item><term>sous 150</term><description>moitie des cas, face a la proie et a moins de
    ///     100 : l'accroche (3 sur 5) ou l'attente ; sinon, ou de 50 a 69, filer ; au-dela,
    ///     le pique.</description></item>
    ///   <item><term>150 a 799</term><description>un sur dix le souffle, un sur dix le pique, deux
    ///     sur cinq approcher, un sur cinq filer, un sur cinq le souffle.</description></item>
    ///   <item><term>800 et plus</term><description>approcher a 100.</description></item>
    /// </list>
    /// <para>Un tirage est jete (<c>0x004e2bcc</c>) entre le test de cap et le test de
    /// distance.</para>
    /// </remarks>
    private static void ReplanFougreAction_004E2B48(byte* e)
    {
        ValleyFlyerAim(e, out int distance, out int heading);

        if (distance < 0x96)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x32)
            {
                bool facing = ((heading + 0x100) & 0xfff) <= 0x200;
                if (facing) CrtRand();                               // Ghidra: 0x004e2bcc, tirage jete

                if (facing && distance < 100)
                {
                    if (roll < 0x1e)
                    {
                        uint flags = *(uint*)e;
                        e[0x42] = 0x0b;
                        e[0x43] = 1;
                        *(int*)(e + 0x5c) = 0;
                        *(uint*)e = (flags & 0xfeffffff) | 2;
                        *(short*)(e + 0x18) = 3;
                        *(short*)(e + 0x1a) = 0;
                    }
                    else
                    {
                        e[0x42] = 0;
                        e[0x43] = (byte)(CrtRand() % 0x28);
                    }
                }
                else
                {
                    StartFougreCourse(e);
                }
            }
            else if (roll < 0x46)
            {
                StartFougreCourse(e);
            }
            else
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
            }
        }

        if (distance < 0x320 && distance >= 0x96)
        {
            int roll = CrtRand() % 100;

            if (roll < 0x14)
            {
                e[0x42] = roll < 10 ? (byte)0x0d : (byte)0x0c;
                e[0x43] = 1;
            }
            else if (roll < 0x3c)
            {
                e[0x42] = 2;
                int draw = CrtRand();
                *(short*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(draw % 10 + 0x14);
            }
            else if (roll < 0x50)
            {
                StartFougreCourse(e);
            }
            else
            {
                e[0x42] = 0x0d;
                e[0x43] = 1;
            }
        }

        if (distance < 0x320) return;

        e[0x42] = 2;
        int far = CrtRand();
        *(short*)(e + 0x10) = 100;
        e[0x43] = (byte)(far % 10 + 0x14);
    }

    /// <summary>La posture 3 de Fougre : un cap tire sur un tour entier, 20 a 29
    /// images.</summary>
    private static void StartFougreCourse(byte* e)
    {
        e[0x42] = 3;
        *(short*)(e + 0x12) = 0;
        e[0x43] = (byte)(CrtRand() % 10 + 0x14);
        *(short*)(e + 0x10) = (short)SignedMask(CrtRand(), 0xfff);
    }
}
