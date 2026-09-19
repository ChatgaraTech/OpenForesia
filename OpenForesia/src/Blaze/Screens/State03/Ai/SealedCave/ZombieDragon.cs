// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DRAGON ZOMBIE — 0x004F6740, grotte scellee (zone 10, sous-zones 4 et 9).
//
// Le nom vient de Comportement.md. 1 824 octets, absents de Blaze.c, transcrits au desassembleur
// le 2026-09-19 (lot 3 de la grotte) : Legacy/Raw/Blaze/SealedCave/FUN_004F6740.asm. Table de
// sauts a quatre entrees a 0x004f6e44 (les postures). Aucune parente parmi les corps ecrits.
//
// LA CHARPENTE EST CELLE DU DRAGON ROUGE (Ai/FireMountain/RedDragon.cs) : famille terrestre, la
// gravite 0x7fff, les quatre appels de fin. Mais la bete ne lit NI sa hardiesse NI son bloc de
// modele, et elle ne frappe jamais au corps a corps : ses trois gestes font tous NAITRE une entite
// de la zone (le repertoire 0x005e97cc, +0xcc — voir ZombieKingSummonSubtype) :
//
//     geste 0  animation 3   aux images 7 et 22, l'entite de l'octet 0x19, parametre 4
//     geste 1  animation 5   en fin d'animation, l'entite de l'octet 0x0b, nee a l'attache 2 du
//                            modele (+0x9a mis a 2 le temps de la naissance), puis 40 images
//     geste 2  animation 4   en fin d'animation, l'une des entites 0x10 / 0x0c / 0x11 au hasard,
//                            visant l'attache 0 du modele (+0x44..+0x4c), puis 10 images
//
// Le geste 1 est le souffle du Roi zombie du labyrinthe (meme octet 0x0b, meme -1 / -1).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x004f6740</c> — <b>l'intelligence du Dragon zombie</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004F6740(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);     // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideZombieDragonAction_004F6798(e, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_004f6b84
        *(ushort*)(e + 0x9a) = 0x7fff;

        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanZombieDragonAction_004F6BDE(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004f6798..0x004f6b83</c> — <b>le premier temps</b> : mener le geste en cours, ou
    /// tenir la posture.
    /// </summary>
    private static void DecideZombieDragonAction_004F6798(byte* e, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ hors geste
        if ((flags & 2) == 0)
        {
            // Ghidra: LAB_004f6998
            if ((flags & 0x11) != 0) return;
            if ((flags & 0x804) != 0) return;

            HoldZombieDragonStance_004F69C0(e, distance, heading, flags);
            return;
        }

        // Le trait 0x10 coupe court.
        if ((traits & 0x10) != 0)
        {
            e[0x43] = 0;
            *(uint*)e = flags & 0xfffffffd;
            return;
        }

        switch (e[0x42])
        {
            // ---------------------------------------------------------- geste 0
            case 0:
            {
                // Ghidra: LAB_004f694d
                short frame = *(short*)(e + 0x1a);
                if (frame == 7 || frame == 0x16)
                {
                    SpawnEffectEntity(e, ZombieKingSummonSubtype(0x19), 0, 4);   // Ghidra: FUN_0047a9d0
                }

                flags = *(uint*)e;
                if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;

                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- geste 1 : le souffle
            case 1:
            {
                // Ghidra: LAB_004f6883
                short state = *(short*)(e + 0x10);
                if (state == 0)
                {
                    CountDownZombieDragonGesture_004F6937(e, flags);
                    return;
                }

                if (state == 1 && (flags & 0x2000000) != 0)
                {
                    int savedLift = *(short*)(e + 0x9a);
                    int savedX = *(int*)(e + 0x50);
                    int savedY = *(int*)(e + 0x54);
                    int savedZ = *(int*)(e + 0x58);

                    *(ushort*)(e + 0x9a) = 2;
                    short* anchor = ModelAnchorRow_00535770(e, 2);          // Ghidra: FUN_00535770
                    *(int*)(e + 0x50) = anchor[0] << 0xc;
                    *(int*)(e + 0x54) = anchor[1] << 0xc;
                    *(int*)(e + 0x58) = anchor[2] << 0xc;

                    SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0b), 0xff, -1);   // Ghidra: FUN_0047a9d0

                    *(short*)(e + 0x10) -= 1;
                    *(int*)(e + 0x54) = savedY;
                    *(int*)(e + 0x58) = savedZ;
                    *(int*)(e + 0x50) = savedX;
                    *(short*)(e + 0x9a) = (short)savedLift;
                    e[0x43] = 0x28;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(int*)(e + 0x5c) = 0;                               // Ghidra: LAB_004f6990
                return;
            }

            // ---------------------------------------------------------- geste 2 : le jet
            case 2:
            {
                // Ghidra: 0x004f67da
                short state = *(short*)(e + 0x10);
                if (state == 0)
                {
                    CountDownZombieDragonGesture_004F6937(e, flags);
                    return;
                }

                if (state == 1 && (flags & 0x2000000) != 0)
                {
                    // Le tirage rend 0, 1 ou 2 ; la quatrieme branche du corps original (qui lit
                    // un local jamais ecrit, [esp+0x18]) est donc morte.
                    int offset;
                    switch (CrtRand() % 3)
                    {
                        case 0: offset = 0x10; break;
                        case 1: offset = 0x0c; break;
                        default: offset = 0x11; break;
                    }

                    short* anchor = ModelAnchorRow_00535770(e, 0);          // Ghidra: FUN_00535770
                    *(int*)(e + 0x44) = anchor[0] << 0xc;
                    *(int*)(e + 0x48) = anchor[1] << 0xc;
                    *(int*)(e + 0x4c) = anchor[2] << 0xc;

                    SpawnEffectEntity(e, ZombieKingSummonSubtype(offset), 0, 3);   // Ghidra: FUN_0047a9d0

                    *(short*)(e + 0x10) -= 1;
                    e[0x43] = 0x0a;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                *(int*)(e + 0x5c) = 0;                               // Ghidra: LAB_004f6990
                return;
            }

            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>LAB_004f6937</c> — <b>le temps mort apres un jet</b>, commun aux gestes 1 et 2 :
    /// le compte <c>+0x43</c> descend ; a zero, le geste prend fin.
    /// </summary>
    private static void CountDownZombieDragonGesture_004F6937(byte* e, uint flags)
    {
        e[0x43]--;
        if (e[0x43] != 0)
        {
            *(int*)(e + 0x5c) = 0;
            return;
        }

        *(int*)(e + 0x5c) = 0;
        *(uint*)e = flags & 0xfffffffd;
    }

    /// <summary>
    /// Ghidra: <c>0x004f69c0..0x004f6b83</c> — <b>la tenue de la posture</b>, par la table de sauts
    /// <c>0x004f6e44</c>. Chaque posture pose son animation (<c>+0x18</c>, en relancant le
    /// deroulement par le bit <c>0x1000000</c> quand elle change).
    /// </summary>
    private static void HoldZombieDragonStance_004F69C0(byte* e, int distance, int heading, uint flags)
    {
        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(short*)(e + 0x18) != 0)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 0;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                break;

            // ---------------------------------------------------------- 1 : pivoter
            case 1:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                if (TurnSceneEntityTowardTarget(e, 0x80, heading)) e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                break;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                // +0x10 porte la distance a tenir (100, posee par la replanification).
                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;
                TurnSceneEntityTowardTarget(e, 0x80, heading);
                *(int*)(e + 0x5c) = 0x12000;
                break;

            // ---------------------------------------------------------- 3 : marcher vers un cap
            case 3:
            {
                if (e[0x43] != 0) e[0x43]--;

                if (*(short*)(e + 0x18) != 1)
                {
                    *(uint*)e = flags | 0x1000000;
                    *(int*)(e + 0x18) = 1;
                }

                // Le pas est porte directement sur la position, dans le cap +0x10 — la vitesse
                // +0x5c n'y sert que de facteur, et retombe a zero aussitot.
                *(int*)(e + 0x5c) = 0xe000;
                short course = *(short*)(e + 0x10);
                *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                int cos = (int)Winsys3d.GetCosTableEntry(course);
                int step = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = 0;
                *(int*)(e + 0x24) += (cos * step) >> 0xc;

                TurnSceneEntityTowardTarget(e, 0x80, heading);
                break;
            }

            default:
                break;
        }

        // Ghidra: LAB_004f6b57 — le pas lourd, aux images 1 et 20 de la marche.
        if (*(short*)(e + 0x18) != 1) return;

        short frame = *(short*)(e + 0x1a);
        if (frame == 1 || frame == 0x14)
        {
            Aud.PlayPositionalSound(0x42, 0x200, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004f6bde..0x004f6e3f</c> — <b>le troisieme temps</b>. Distance et cap sont
    /// recalcules en deux dimensions ; un tirage sur cent, et deux plages de distance.
    ///
    /// <para>
    /// <b>Sous 300</b> : un geste 0 si la cible est dans le cone de 0x100 (sinon pivoter), 25 % d'une
    /// approche a 100, le reste une marche vers un cap tire devant. <b>Au-dela</b> : 45 % d'un jet —
    /// geste 1 ou 2 a pile ou face, si la cible est dans le cone de 0x200 —, 25 % d'une marche vers un
    /// cap tire, 20 % d'une approche, 10 % d'attente.
    /// </para>
    /// </summary>
    private static void ReplanZombieDragonAction_004F6BDE(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;

        if (distance < 0x12c)
        {
            if (roll < 0x32)
            {
                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    TurnZombieDragon(e);
                    return;
                }

                *(int*)(e + 0x18) = 3;
                e[0x42] = 0;
                *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x4b)
            {
                ApproachZombieDragon(e);
                return;
            }

            // Ghidra: 0x004f6ca6 et LAB_004f6d02 — les deux branches du corps original (tirage
            // sous 0x5a et dans le cone, ou non) ecrivent exactement la meme chose.
            WalkZombieDragon(e, heading, 0x200);
            return;
        }

        // Ghidra: LAB_004f6d41
        if (roll < 0x2d)
        {
            if (((heading + 0x200) & 0xfff) > 0x400)
            {
                TurnZombieDragon(e);
                return;
            }

            uint flags = *(uint*)e;
            if ((roll & 1) != 0)
            {
                *(int*)(e + 0x18) = 5;
                e[0x42] = 1;
            }
            else
            {
                *(int*)(e + 0x18) = 4;
                e[0x42] = 2;
            }

            *(short*)(e + 0x10) = 1;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        if (roll < 0x46)
        {
            WalkZombieDragon(e, heading, -0x600);
            return;
        }

        if (roll < 0x5a)
        {
            ApproachZombieDragon(e);
            return;
        }

        e[0x42] = 0;
        e[0x43] = (byte)(CrtRand() % 10 + 10);
    }

    /// <summary>Ghidra: <c>LAB_004f6da8</c> — pivoter, pour une image.</summary>
    private static void TurnZombieDragon(byte* e)
    {
        e[0x42] = 1;
        e[0x43] = 1;
    }

    /// <summary>Ghidra: <c>LAB_004f6c7f</c> / <c>LAB_004f6dfc</c> — approcher jusqu'a 100.</summary>
    private static void ApproachZombieDragon(byte* e)
    {
        e[0x42] = 2;
        int r = CrtRand();
        *(short*)(e + 0x10) = 100;
        e[0x43] = (byte)(r % 10 + 10);
    }

    /// <summary>
    /// Ghidra: <c>0x004f6cc3</c> / <c>LAB_004f6db8</c> — marcher vers un cap tire sur les trois
    /// quarts du tour, decale de <paramref name="shift"/> (0x200 de pres, -0x600 de loin).
    /// </summary>
    private static void WalkZombieDragon(byte* e, int heading, int shift)
    {
        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 10);
        int r = CrtRand() % 0xc00;
        *(short*)(e + 0x10) = (short)(r + heading + *(ushort*)(e + 0x32) + shift);
    }
}
