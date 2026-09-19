// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE SOSIE — 0x004DF680, sous-type 5 du bloc 0x0055a128, ruines souterraines (zone 7, sous-zones 0
// a 2). Le nom vient du CD : les quatre modeles de la sous-zone 2 s'appellent « Sosie »
// (Documentation/Reference/Bestiaire/bestiaire.md).
//
// UNE BETE QUI N'A PAS DE FICHE A ELLE. Ses quatre modeles portent 1 PV et 1 PM ; c'est
// EntityHandler_00541C90 (Handlers/UndergroundRuinsMecanismes.cs) qui, a chaque image de la
// sous-zone 2, les remplit avec le groupe — apparence, rang, PV, PM et sept autres champs. Le Sosie
// de rang n est donc le double du membre n. Cette routine-ci en tire la consequence : elle lit la
// CLASSE du membre de meme rang (`e[8]`, l'octet d'identite de sa fiche de personnage) et se bat
// comme lui.
//
//   - classes 0, 1, 2, 3, 5 et 6 : la frappe au corps a corps, a la fin de chaque geste, avec la
//     sonde que la table 0x00552ce8 donne pour son identite (classe ET sexe, seize entrees) ;
//   - classe 4, le Chasseur : la fiche famille 5 indice 0, quand son animation est 0x100003 (bit
//     de sexe eteint) ou 0xc0003 (allume) ;
//   - classe 7, la Fee : la fiche famille 6 indice 0, sur l'animation 0x30003.
//
// CES DEUX BRANCHES SONT LES POSTURES DU JOUEUR, RECOPIEES EN LIGNE : PostureHandler_0051A6B0 (la
// posture 4 du Chasseur) et PostureHandler_0051A710 (la posture 7, le vol) — memes animations,
// memes fiches (Tick/PostureAndInput.cs). Le Sosie tire donc exactement ce que tire le membre qu'il
// imite. Une difference : le Sosie efface le bit 0x800 aussitot le sort lance, les postures le
// laissent pose.
//
// Il recopie aussi, a chaque image, l'octet +0x0b de l'emplacement de meme rang — le role de
// dessin du membre.
//
// La charpente est celle de la Fourmi geante (`Ai/Forest/GiantAnt.cs`) : quatre postures, la meme
// queue de quatre appels, mais une replanification a deux distances seulement et sans hardiesse.
// Absente de Blaze.c ; 369 instructions, Legacy/Raw/Blaze/UndergroundRuins/FUN_004DF680.asm,
// table de sauts a quatre entrees a 0x004dfad8.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552ce8</c> — <b>la sonde de coup du Sosie, par identite</b> : seize
    /// enregistrements de six mots, indexes par l'octet d'identite entier du membre imite (trois
    /// bits de classe, puis le bit de sexe <c>8</c>). Relevee dans <c>Blaze.exe</c> le 2026-09-19.
    ///
    /// <para>
    /// <b>Les rangs 4, 7, 12 et 15 sont nuls</b> — le Chasseur et la Fee, qui ne frappent pas : la
    /// routine ne les lit jamais. Bornee en bas par le mot de sort de la Chimere des ruines
    /// (<c>0x00552ce0</c>, voir <c>Ai/ImmortalCastle/Chimera.cs</c>), en haut par un changement de
    /// forme a <c>0x00552da8</c>.
    /// </para>
    /// </summary>
    private static readonly short[] s_DoppelgangerProbes_00552CE8 =
    [
        0x0000, 0x0028, 0x003c, 0x0096, unchecked((short)0xffa0), 0x006e,   //  0  Guerrier
        0x0000, 0x001e, 0x001e, 0x0096, 0x0100, 0x005a,                     //  1  Pretre
        0x0000, 0x0000, 0x0046, 0x0096, unchecked((short)0xff40), 0x0064,   //  2  Voleur
        0x0000, 0x0028, 0x003c, 0x0096, unchecked((short)0xff80), 0x005a,   //  3  Sorcier
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     //  4  Chasseur
        0x0000, 0x0032, 0x0032, 0x0096, 0x0000, 0x0064,                     //  5  Elfe
        0x0000, 0x0032, 0x0050, 0x0096, unchecked((short)0xff40), 0x0050,   //  6  Nain
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     //  7  Fee
        0x0000, 0x001e, 0x0028, 0x0096, 0x0000, 0x0064,                     //  8  Guerrier, bit 8
        0x0000, 0x001e, 0x0028, 0x0096, 0x0100, 0x006e,                     //  9  Pretre, bit 8
        0x0000, 0x0000, 0x0046, 0x0096, unchecked((short)0xff40), 0x005a,   // 10  Voleur, bit 8
        0x0000, 0x001e, 0x0046, 0x0096, unchecked((short)0xff00), 0x005a,   // 11  Sorcier, bit 8
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     // 12  Chasseur, bit 8
        0x0000, 0x0032, 0x0032, 0x0096, 0x0000, 0x0064,                     // 13  Elfe, bit 8
        0x0000, 0x0028, 0x0050, 0x0096, unchecked((short)0xff00), 0x005a,   // 14  Nain, bit 8
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     // 15  Fee, bit 8
    ];

    private static byte* s_DoppelgangerProbesStorage;

    private static byte* DoppelgangerProbes()
    {
        if (s_DoppelgangerProbesStorage == null)
        {
            s_DoppelgangerProbesStorage = (byte*)System.Runtime.InteropServices.NativeMemory.Alloc(
                (nuint)(s_DoppelgangerProbes_00552CE8.Length * sizeof(short)));
            fixed (short* source = s_DoppelgangerProbes_00552CE8)
                System.Buffer.MemoryCopy(source, s_DoppelgangerProbesStorage,
                    s_DoppelgangerProbes_00552CE8.Length * sizeof(short),
                    s_DoppelgangerProbes_00552CE8.Length * sizeof(short));
        }

        return s_DoppelgangerProbesStorage;
    }

    /// <summary>
    /// Ghidra: <c>0x004df680</c> — <b>l'intelligence du Sosie</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004DF680(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);      // Ghidra: FUN_00538980

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideDoppelgangerAction_004DF6D3(e, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateSceneEntityMotion_00488160(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940
        EndSceneEntityAnimation_004DFAF0((uint*)e);
        InterruptSceneEntity_004E3770((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x817) != 0) return;
        if (e[0x43] != 0) return;

        ReplanDoppelgangerAction_004DF97C(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004df6d3..0x004df92a</c> — <b>le premier temps</b> du Sosie : le geste en
    /// cours, ou la posture tenue.
    /// </summary>
    private static void DecideDoppelgangerAction_004DF6D3(byte* e, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);

        byte* slot = Inp.s_SlotFlags_005e93c0 + e[8] * 0x9c;
        e[0x0b] = slot[0x0b];                                      // Ghidra: DAT_005e93cb + rang * 0x9c

        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                e[0x43] = 0;                                       // Ghidra: jmp 0x004df927
                return;
            }

            // Ghidra: [[0x006087b4]] + 0xf0150 + rang * 0x2000 — l'octet d'identite du membre.
            byte identity = GetMainMemPtr()[0xf0150 + (e[8] << 0xd)];
            int kind = identity & 7;

            if (kind == 4)
            {
                int animation = *(int*)(e + 0x18);
                if ((animation == 0x100003 && (identity & 8) == 0)
                    || (animation == 0xc0003 && (identity & 8) != 0))
                {
                    *(uint*)e = flags | 0x800;
                    CastSpell_0053A980((uint*)e, 5, 0, -1, -1);    // Ghidra: FUN_0053a980
                    *(uint*)e &= 0xfffff7ff;
                }
            }
            else if (kind == 7 && *(int*)(e + 0x18) == 0x30003)
            {
                *(uint*)e = flags | 0x800;
                CastSpell_0053A980((uint*)e, 6, 0, -1, -1);        // Ghidra: FUN_0053a980
                *(uint*)e &= 0xfffff7ff;
            }

            flags = *(uint*)e;

            // Le geste acheve : les classes qui ne lancent pas frappent, une fois.
            if ((flags & 0x2000000) != 0)
            {
                if (kind != 4 && kind != 7)
                {
                    byte* prey = SweepPartyForMeleeHit_00519C30(
                        e, (int*)(e + 0x1c), (short*)(DoppelgangerProbes() + identity * 12), 0);

                    if (*Mem.Camera(0x005de0b8) != 0)
                    {
                        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
                        *(ushort*)(prey + 0x96) = 0x10;
                        *(uint*)prey |= 0x40;
                        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
                    }
                }

                flags = *(uint*)e;
                if ((flags & 1) != 0)
                {
                    (*(short*)(e + 0x1a))--;
                    return;
                }
            }
            else if ((flags & 1) != 0)
            {
                return;
            }

            if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffffd;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        // ================================================================ la posture tenue
        if ((flags & 0x11) != 0) return;
        if ((flags & 0x804) != 0) return;

        switch (e[0x42])
        {
            // ---------------------------------------------------------- 0 : attendre
            case 0:
                if (*(short*)(e + 0x18) != 0)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 0;
                    *(uint*)e = flags;
                }

                if (distance < 0x12c)
                {
                    e[0x43] = 0;
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0;
                break;

            // ---------------------------------------------------------- 1 : se tourner d'un coup
            // Ghidra: 0x004df86e — le cap est ARRONDI au seizieme de tour : (cap + ecart) & 0xf00.
            case 1:
                e[0x43] = 0;
                *(short*)(e + 0x32) = (short)((heading + *(short*)(e + 0x32)) & 0xf00);
                break;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(short*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (distance < *(short*)(e + 0x10))
                {
                    e[0x43] = 0;
                    break;
                }

                if (e[0x43] != 0) e[0x43]--;

                if (((heading + 0x100) & 0xfff) > 0x200)
                    *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? 0x100 : -0x100);

                *(int*)(e + 0x5c) = 0x14000;
                break;

            // ---------------------------------------------------------- 3 : marcher droit
            case 3:
                if (*(short*)(e + 0x18) != 1)
                {
                    flags |= 0x1000000;
                    *(int*)(e + 0x18) = 1;
                    *(uint*)e = flags;
                }

                if (e[0x43] != 0) e[0x43]--;
                *(int*)(e + 0x5c) = 0x14000;
                break;

            default:
                break;
        }

        // Ghidra: 0x004df91e — l'emplacement du membre imite porte 2 ou 4 : plus d'hesitation.
        if ((slot[0] & 6) != 0) e[0x43] = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004df97c..0x004dfad5</c> — <b>le troisieme temps</b> du Sosie. Distance et cap
    /// sont recalcules a plat vers la cible <c>+0x6c</c>, puis un centieme est tire :
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///   <item><term>a moins de 300</term><description>0..39 : frapper si le cap tient dans
    ///     ±<c>0x100</c>, sinon se tourner ; 40..59 : approcher a 100 ; 60..79 : marcher droit dans
    ///     un cap tire ; au-dela, attendre.</description></item>
    ///   <item><term>au-dela</term><description>0..19 : se tourner ; 20..39 : approcher a 100 ;
    ///     40..79 : marcher droit ; au-dela, attendre.</description></item>
    /// </list>
    /// <para>Aucune hardiesse n'entre dans le calcul, et aucune hesitation n'est tiree pour se
    /// tourner : <c>+0x43 = 1</c>, une image. Les autres postures tiennent de 10 a 19 images.</para>
    /// </remarks>
    private static void ReplanDoppelgangerAction_004DF97C(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));   // Ghidra: FUN_00545a10
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;   // Ghidra: FUN_00545a22

        int roll = CrtRand() % 100;

        if (distance < 0x12c)
        {
            if (roll < 0x28)
            {
                if (((heading + 0x100) & 0xfff) > 0x200)
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                    return;
                }

                uint state = *(uint*)e;
                *(int*)(e + 0x18) = 3;
                e[0x42] = 0;
                *(int*)(e + 0x5c) = 0;
                *(uint*)e = (state & 0xfeffffff) | 2;
                return;
            }

            if (roll < 0x3c) { ApproachAtHundred(e); return; }
            if (roll < 0x50) { WalkStraightAtRandom(e); return; }

            e[0x42] = 0;
            e[0x43] = (byte)(CrtRand() % 10 + 10);
            return;
        }

        if (roll < 0x14)
        {
            e[0x42] = 1;
            e[0x43] = 1;
            return;
        }

        if (roll < 0x28) { ApproachAtHundred(e); return; }
        if (roll < 0x50) { WalkStraightAtRandom(e); return; }

        e[0x42] = 0;
        e[0x43] = (byte)(CrtRand() % 10 + 10);

        // Ghidra: 0x004dfa21 / 0x004dfa64 — deux copies en ligne, a l'identique.
        static void ApproachAtHundred(byte* e)
        {
            e[0x42] = 2;
            int draw = CrtRand();
            *(short*)(e + 0x10) = 100;
            e[0x43] = (byte)(draw % 10 + 10);
        }

        // Ghidra: 0x004dfa8b — le cap est tire sur seize directions : (rand & 0xf) << 8.
        static void WalkStraightAtRandom(byte* e)
        {
            e[0x42] = 3;
            e[0x43] = (byte)(CrtRand() % 10 + 10);
            *(ushort*)(e + 0x32) = (ushort)((CrtRand() & 0xf) << 8);
        }
    }
}
