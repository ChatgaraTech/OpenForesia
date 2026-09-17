// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE TENEBREUX — 0x004B6B90, vieille tour (zone 4, sous-zones 3, 7, 9), et sa copie 0x00500E10
// (grotte scellee, sous-zones 11 et 12).
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004B6B90 — LE TENEBREUX. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/OldTower/FUN_004B6B90.asm, 747 instructions, absente de Blaze.c), table de
    // sauts a quatre entrees a 0x004B752C pour les postures.
    //
    // DEUX CORPS D'UN COUP. 0x00500E10 ne differe que par l'adresse de sa sonde (0x00553098) et de ses
    // trois scripts de sons differes (0x005530A8, 0x005530B8, 0x005530C8) : releves octet par octet,
    // ils portent les memes donnees. Table de sauts identique en relatif.
    //
    // LE GRAND FRERE DU WYBARN (Ai/OldTower/Wybarn.cs) : meme milieu de volante, memes trois gestes
    // aiguilles par la posture — mais aucun n'est le meme :
    //
    //   - LA RUEE (posture 0, animation 3) : a l'image 10 elle fond a 0x28000 avec le son 0x25, puis
    //     prend comme le Wybarn — seuil de fin a 8 et non 5, cone de 0x28, projection dans SON cap.
    //   - LA SERRE (posture 1, animation 5) : elle s'ELEVE d'abord — +0x10 grandit de 4 par image et
    //     s'ajoute a la hauteur, bit 0x400 pose pour couper le vol —, puis a partir de l'image 12 elle
    //     frappe a pleine attaque et, sur un tirage, pose l'effet de piege 7 (un coup sur cinq) OU
    //     l'alteration 7 (un sur cinq encore). Elle retombe d'un quart par image vers son plafond.
    //   - LE SOUFFLE (posture 2, animation 4) : a l'image 18, l'entite 0xC0 + catalogue[VARIANTE]
    //     nait de la gueule — la variante choisit l'haleine —, avec +0x9a, +0x34 et +0x5c pretes le
    //     temps de la naissance (0, 0xED4, 0x3C) puis rendues. Quinze images d'attente.
    //   - TROIS SCRIPTS DE SONS DIFFERES, dont un pour ses battements d'ailes a l'image 9 de
    //     l'animation 1.
    //   - ELLE VOLE PLUS BAS que le Wybarn (0x4100 << 5 au lieu de 0x4B00 << 6), et pendant la serre
    //     le vol cesse de la tirer.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x00552ad8</c> — <b>la sonde du Tenebreux</b>, relevee dans <c>.data</c> :
    /// <c>{0, 30, 70, 100, 0, 100}</c>. La ruee et la serre la partagent.
    /// </summary>
    private static readonly short[] s_TenebrousProbe_00552AD8 = [0, 30, 70, 100, 0, 100];

    private static short* s_TenebrousProbeStorage;

    private static short* TenebrousProbe()
    {
        if (s_TenebrousProbeStorage == null)
        {
            s_TenebrousProbeStorage = (short*)NativeMemory.Alloc((nuint)s_TenebrousProbe_00552AD8.Length, sizeof(short));
            for (int i = 0; i < s_TenebrousProbe_00552AD8.Length; i++) s_TenebrousProbeStorage[i] = s_TenebrousProbe_00552AD8[i];
        }

        return s_TenebrousProbeStorage;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552ae8</c> — <b>le souffle</b>, script de sons differes : deux pas, voix
    /// <c>0x23</c>. Borne par le <c>[1] = 0</c> du second.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552AE8 =
    [
        0x23, 0x01, 0x01, 0x00, 0x53, 0x06, 0x00, 0x18,   // 0x00552ae8
        0x23, 0x00, 0x01, 0x00, 0x00, 0x08, 0x00, 0x18,   // 0x00552af0 — [1] = 0 : fin
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552af8</c> — <b>le battement d'ailes</b> : deux pas, voix <c>0x24</c>,
    /// le second deux images apres le premier.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552AF8 =
    [
        0x24, 0x02, 0x01, 0x00, 0xaa, 0x02, 0x00, 0x18,   // 0x00552af8
        0x24, 0x00, 0x01, 0x00, 0xd4, 0x02, 0x00, 0x18,   // 0x00552b00 — [1] = 0 : fin
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552b08</c> — <b>la serre qui porte</b> : trois pas, voix <c>0x36</c>, a
    /// trois hauteurs montantes. Le troisieme borne le script ; la sonde du Wybarn commence juste
    /// apres, a <c>0x00552B20</c>.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552B08 =
    [
        0x36, 0x01, 0x01, 0x00, 0x7e, 0x03, 0x00, 0x18,   // 0x00552b08
        0x36, 0x01, 0x01, 0x00, 0xd3, 0x03, 0x00, 0x18,   // 0x00552b10
        0x36, 0x00, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552b18 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552AE8Storage;
    private static byte* s_DelayedSoundScript00552AF8Storage;
    private static byte* s_DelayedSoundScript00552B08Storage;

    /// <summary>
    /// Ghidra: <c>0x004b6b90</c> (et <c>0x00500e10</c>) — <b>l'intelligence du Tenebreux</b>. Voir
    /// le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004B6B90(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0) DecideTenebrousAction_004B6BE1(e, model, distance, heading, traits);

        // ---------------------------------------------------------------- 2. appliquer
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);
        *(uint*)(e + 0x90) &= 0xdfffd8ff;
        HoverTenebrous_004B72F6(e);
        InterruptFlyingEntity_004A1CF0((uint*)e, stats);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanTenebrousAction_004B73A9(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004b72f6..0x004b7375</c> — <b>le vol</b> du Tenebreux. Hors geste, l'onde vise
    /// <c>(sin + 0x4100) &lt;&lt; 5</c> au-dessus du plafond, par seiziemes ; en geste, le plafond plus
    /// <c>0x28000</c> par huitiemes — <b>sauf quand le bit <c>0x400</c> est pose</b> : la serre monte
    /// par elle-meme et le vol ne la retient pas.
    /// </summary>
    private static void HoverTenebrous_004B72F6(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);
        if ((*chased & 1) == 0) *(int*)(e + 0x48) = *(int*)(chased + 0x60);

        *(short*)(e + 0x14) += (short)((CrtRand() & 0xff) + 0x100);

        int ceiling = *(int*)(e + 0x60);
        if (*(int*)(e + 0x48) >= ceiling) ceiling = *(int*)(e + 0x48);

        uint flags = *(uint*)e;

        if ((flags & 2) != 0)
        {
            if ((flags & 0x400) != 0) return;

            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = y + ((ceiling - y + 0x28000) >> 3);
            return;
        }

        int height = *(int*)(e + 0x20);
        int lift = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) + 0x4100) << 5;
        *(int*)(e + 0x20) = height + ((lift + ceiling - height) >> 4);
    }

    /// <summary>
    /// Ghidra: <c>0x004b6be1..0x004b72d9</c> — <b>le premier temps</b> du Tenebreux.
    /// </summary>
    private static void DecideTenebrousAction_004B6BE1(byte* e, byte* model, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);
        uint flags = *(uint*)e;

        // ================================================================ le geste en cours
        if ((flags & 2) != 0)
        {
            if ((traits & 0x10) != 0)
            {
                *(uint*)e = flags & 0xfffffffd;
                e[0x43] = 0;
                return;
            }

            switch (e[0x42])
            {
                case 0: TenebrousRush_004B6E81(e, model, heading); return;
                case 1: TenebrousTalon_004B6D18(e, heading); return;
                case 2: TenebrousBreath_004B6C2A(e, flags); return;
                default: return;
            }
        }

        if ((flags & 0x10) != 0) return;
        if ((flags & 0x804) != 0) return;

        // ================================================================ la posture tenue
        byte held = e[0x42];
        if (held <= 3)
        {
            switch (held)
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
                    DampFlyingEntityPush(e);
                    break;

                // ---------------------------------------------------------- 1 : se tourner
                case 1:
                    if (*(short*)(e + 0x18) != 0)
                    {
                        *(uint*)e = flags | 0x1000000;
                        *(int*)(e + 0x18) = 0;
                    }

                    if (TurnSceneEntityTowardTarget(e, 0x80, heading)) e[0x43] = 0;
                    DampFlyingEntityPush(e);
                    break;

                // ---------------------------------------------------------- 2 : s'approcher
                case 2:
                    if (*(ushort*)(e + 0x18) != 1)
                    {
                        *(int*)(e + 0x18) = 1;
                        *(uint*)e = flags | 0x1000000;
                    }

                    if (distance < 0x12c)
                    {
                        e[0x43] = 0;
                        break;
                    }

                    if (e[0x43] != 0) e[0x43]--;

                    TurnSceneEntityTowardTarget(e, 0x80, heading);
                    RampFlyingEntityPush(e, 0x10000);
                    break;

                // ---------------------------------------------------------- 3 : filer vers un cap
                default:
                {
                    if (*(ushort*)(e + 0x18) != 0)
                    {
                        *(int*)(e + 0x18) = 0;
                        *(uint*)e = flags | 0x1000000;
                    }

                    if (e[0x43] != 0) e[0x43]--;

                    *(int*)(e + 0x5c) = 0x4000;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                    *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    TurnSceneEntityTowardTarget(e, 0x80, heading);
                    break;
                }
            }
        }

        // Ghidra: LAB_004b72bd — le battement d'ailes, a l'image 9 de l'animation 1.
        if (*(short*)(e + 0x18) == 1 && *(short*)(e + 0x1a) == 9)
        {
            Aud.QueueDelayedSoundScript(
                NativeCopy(s_DelayedSoundScript_00552AF8, ref s_DelayedSoundScript00552AF8Storage),
                (int*)(e + 0x1c));                                          // Ghidra: FUN_0053dbe0
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004b6e81..0x004b70bf</c> — <b>la ruee</b>, geste de la posture 0, deux temps sur
    /// <c>+0x10</c>.
    ///
    /// <list type="number">
    /// <item><b>1</b> — la poussee est coupee ; a l'image 10 elle fond a <c>0x28000</c> selon son cap,
    /// garde <c>0x5000</c> de poussee et crie (son <c>0x25</c>, hauteur <c>0x1000</c>). Elle vise
    /// dans un cone de <c>0x80</c>.</item>
    /// <item><b>0</b> — la prise du Wybarn, avec trois ecarts : le geste se ferme sous 8 unites de
    /// vitesse (5 pour le Wybarn), le cone est <c>0x28</c>, et la victime est projetee dans <b>le cap
    /// du Tenebreux</b> et non vers la cible.</item>
    /// </list>
    /// </summary>
    private static void TenebrousRush_004B6E81(byte* e, byte* model, int heading)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0;

            if (*(short*)(e + 0x1a) == 0x0a)
            {
                *(int*)(e + 0x5c) = 0x28000;

                int course = *(short*)(e + 0x32);
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * *(int*)(e + 0x5c)) >> 0xc;
                *(int*)(e + 0x5c) = 0x5000;
                *(short*)(e + 0x10) -= 1;

                Aud.PlayPositionalSound(0x25, 0x1000, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
            }

            TurnSceneEntityTowardTarget(e, 0x80, heading);
            return;
        }

        if (phase != 0) return;

        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;
        int drift = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));

        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0 && drift <= 8) *(uint*)e = flags & 0xffffffdd;

        if (drift < 3) return;
        if ((*(uint*)e & 0x20) != 0) return;

        TurnSceneEntityTowardTarget(e, 0x28, heading);

        byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), TenebrousProbe(), 0);
        if (victim != null) *(uint*)e |= 0x20;

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(victim + 0x96) = 0x10;
        *(uint*)victim |= 0x40;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 1);
        ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20

        if ((*(uint*)victim & 0x40000000) != 0) return;

        *(int*)(victim + 0x50) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * 30;
        *(int*)(victim + 0x58) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * 30;

        uint victimFlags = *(uint*)victim;
        if ((victimFlags & 1) == 0)
        {
            *(int*)(victim + 0x54) = 0x18000;
            *(uint*)victim = victimFlags | 1;
        }

        int savedX = *(int*)(e + 0x1c);
        int savedY = *(int*)(e + 0x20);
        int savedZ = *(int*)(e + 0x24);

        *(int*)(e + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(e + 0x20) = *(int*)(victim + 0x20);
        *(int*)(e + 0x24) = *(int*)(victim + 0x24);

        SpawnEffectEntity(e, 0x39, 0, -1);                          // Ghidra: FUN_0047a9d0

        *(int*)(e + 0x1c) = savedX;
        *(int*)(e + 0x20) = savedY;
        *(int*)(e + 0x24) = savedZ;
    }

    /// <summary>
    /// Ghidra: <c>0x004b6d18..0x004b6e7c</c> — <b>la serre</b>, geste de la posture 1.
    ///
    /// <para>
    /// Deux sons a l'image 1 (<c>2</c>, hauteur <c>0x32A</c>) et a l'image 10 (<c>0x24</c>). <b>Jusqu'a
    /// l'image 9 elle monte</b> : <c>+0x10</c>, remis a zero par la replanification, prend 4 a chaque
    /// image et s'ajoute a la hauteur en unites — une montee qui accelere —, le bit <c>0x400</c> est
    /// pose, et elle vise dans un cone de <c>0x28</c>.
    /// </para>
    /// <para>
    /// <b>A partir de l'image 12 elle frappe</b> par la sonde, a pleine attaque. Un tirage
    /// <c>hasard % 100</c> sous 20 pose l'effet de piege 7 (<see cref="ApplyTrapEffect_0053C420"/>),
    /// de 20 a 39 l'alteration 7 (<see cref="ApplyStatusEffect_0053B3C0"/>) ; puis le script sonore
    /// <c>0x00552B08</c>. Des l'image 10, la fin de l'animation efface <c>2</c> et <c>0x400</c>, et la
    /// hauteur retombe d'un quart par image vers le plus haut de <c>+0x48</c> et <c>+0x60</c>.
    /// </para>
    /// </summary>
    private static void TenebrousTalon_004B6D18(byte* e, int heading)
    {
        if (*(short*)(e + 0x1a) == 1) Aud.PlayPositionalSound(2, 0x32a, 0x1800, (int*)(e + 0x1c));
        if (*(short*)(e + 0x1a) == 0x0a) Aud.PlayPositionalSound(0x24, 0x400, 0x1800, (int*)(e + 0x1c));

        short frame = *(short*)(e + 0x1a);

        if (frame <= 9)
        {
            *(short*)(e + 0x10) += 4;
            *(int*)(e + 0x20) += *(short*)(e + 0x10) << 0xc;
            *(uint*)e |= 0x400;

            TurnSceneEntityTowardTarget(e, 0x28, heading);
            return;
        }

        if (frame >= 0x0c)
        {
            byte* victim = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), TenebrousProbe(), 0);

            if (*Mem.Camera(0x005de0b8) != 0)
            {
                *(uint*)victim |= 0x40;
                *(ushort*)(victim + 0x96) = 0x10;
                ResolveMeleeHit_00539B80((uint*)e, (uint*)victim);

                int roll = CrtRand() % 100;
                if (roll < 0x14)
                {
                    e[7] = 7;
                    ApplyTrapEffect_0053C420((uint*)e, (uint*)victim, 0);
                }
                else if (roll < 0x28)
                {
                    e[7] = 7;
                    ApplyStatusEffect_0053B3C0((uint*)e, (uint*)victim, 0);
                }

                Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(victim + 0x1c));   // Ghidra: FUN_0053db20
                Aud.QueueDelayedSoundScript(
                    NativeCopy(s_DelayedSoundScript_00552B08, ref s_DelayedSoundScript00552B08Storage),
                    (int*)(e + 0x1c));                                      // Ghidra: FUN_0053dbe0
            }
        }

        uint flags = *(uint*)e;
        if ((flags & 0x2000000) != 0) *(uint*)e = flags & 0xfffffbfd;

        int ceiling = *(int*)(e + 0x48);
        if (ceiling < *(int*)(e + 0x60)) ceiling = *(int*)(e + 0x60);

        int y = *(int*)(e + 0x20);
        *(int*)(e + 0x20) = ((ceiling - y) >> 2) + y;
    }

    /// <summary>
    /// Ghidra: <c>0x004b6c2a..0x004b6d13</c> — <b>le souffle</b>, geste de la posture 2, deux temps
    /// sur <c>+0x10</c>.
    ///
    /// <list type="number">
    /// <item><b>1</b> — la fin de l'animation passe au temps 0 avec un rebours de 15 ; et a l'image
    /// 18, <b>dans le meme temps</b>, le souffle part : <c>+0x9a</c>, <c>+0x34</c> et <c>+0x5c</c>
    /// sont pretes (0, <c>0xED4</c>, <c>0x3C</c>) — l'entite nee les recopie —, l'ancre 0 du modele
    /// est rangee en <c>+0x44..+0x4c</c>, l'entite <c>0xC0 + catalogue[+0x16]</c> nait avec les
    /// parametres 0 et 2, les trois champs sont rendus, et le script sonore <c>0x00552AE8</c> part.</item>
    /// <item><b>0</b> — le rebours descend sans garde et, a zero, ferme le geste.</item>
    /// </list>
    /// <para>La poussee est coupee a chaque image.</para>
    /// </summary>
    private static void TenebrousBreath_004B6C2A(byte* e, uint flags)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0)
        {
            if (--e[0x43] == 0) *(uint*)e = flags & 0xfffffffd;
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if (phase != 1)
        {
            *(int*)(e + 0x5c) = 0;
            return;
        }

        if ((flags & 0x2000000) != 0)
        {
            e[0x43] = 0x0f;
            *(short*)(e + 0x10) = (short)(phase - 1);
        }

        if (*(short*)(e + 0x1a) != 0x12)
        {
            *(int*)(e + 0x5c) = 0;
            return;
        }

        short keptGravity = *(short*)(e + 0x9a);
        short keptPitch = *(short*)(e + 0x34);
        int keptPush = *(int*)(e + 0x5c);

        *(short*)(e + 0x9a) = 0;
        *(short*)(e + 0x34) = 0xed4;
        *(int*)(e + 0x5c) = 0x3c;

        short* mouth = ModelAnchorRow_00535770(e, 0);
        *(int*)(e + 0x44) = mouth[0] << 0xc;
        *(int*)(e + 0x48) = mouth[1] << 0xc;
        *(int*)(e + 0x4c) = mouth[2] << 0xc;

        byte* catalogue = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0xcc));
        SpawnEffectEntity(e, (byte)(catalogue[*(short*)(e + 0x16)] + 0xc0), 0, 2);   // Ghidra: FUN_0047a9d0

        *(short*)(e + 0x9a) = keptGravity;
        *(short*)(e + 0x34) = keptPitch;
        *(int*)(e + 0x5c) = keptPush;

        Aud.QueueDelayedSoundScript(
            NativeCopy(s_DelayedSoundScript_00552AE8, ref s_DelayedSoundScript00552AE8Storage),
            (int*)(e + 0x1c));                                              // Ghidra: FUN_0053dbe0

        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004b73a9..0x004b7523</c> — <b>le troisieme temps</b> du Tenebreux. Un seul tirage,
    /// avant la comparaison a <c>0x12c</c>.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : six fois sur dix la serre si la cible est dans un huitieme de tour (sinon
    /// se tourner) ; le reste file vers un cap de <c>-0x600</c> a <c>+0x5FF</c> autour de la cible.</item>
    /// <item><b>De loin</b> : trois fois sur dix, face a la cible, la ruee sur un tirage impair et le
    /// souffle sur un tirage pair (sinon se tourner) ; cinq fois sur dix s'approcher, 20 a 29 images ;
    /// le reste file.</item>
    /// </list>
    /// </summary>
    private static void ReplanTenebrousAction_004B73A9(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;
        bool facing = ((heading + 0x100) & 0xfff) <= 0x200;

        if (distance < 0x12c)
        {
            if (roll >= 0x3c)
            {
                WanderTenebrous(e, heading);
                return;
            }

            if (!facing)
            {
                HesitateSceneEntity(e);
                return;
            }

            // Ghidra: 0x004b742a — armer LA SERRE.
            uint flags = *(uint*)e;
            *(int*)(e + 0x18) = 5;
            e[0x42] = 1;
            *(ushort*)(e + 0x10) = 0;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        if (roll < 0x1e)
        {
            if (!facing)
            {
                HesitateSceneEntity(e);
                return;
            }

            uint flags = *(uint*)e;

            if ((roll & 1) != 0)
            {
                // Ghidra: 0x004b746d — armer LA RUEE.
                *(int*)(e + 0x18) = 3;
                e[0x42] = 0;
            }
            else
            {
                // Ghidra: LAB_004b7493 — armer LE SOUFFLE.
                *(int*)(e + 0x18) = 4;
                e[0x42] = 2;
            }

            *(ushort*)(e + 0x10) = 1;
            *(uint*)e = (flags & 0xfeffffff) | 2;
            return;
        }

        if (roll < 0x50)
        {
            e[0x42] = 2;
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        WanderTenebrous(e, heading);
    }

    /// <summary>Ghidra: <c>LAB_004b74ed</c> — <b>filer ailleurs</b> : posture 3 pour
    /// <c>hasard % 10 + 10</c> images, cap <c>hasard % 0xC00 + cap vers la cible - 0x600</c>.</summary>
    private static void WanderTenebrous(byte* e, int heading)
    {
        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 10);

        int spread = CrtRand() % 0xc00;
        *(ushort*)(e + 0x10) = (ushort)(spread + heading + *(short*)(e + 0x32) - 0x600);
    }
}
