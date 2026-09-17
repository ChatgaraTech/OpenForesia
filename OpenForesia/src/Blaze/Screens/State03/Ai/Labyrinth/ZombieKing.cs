// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE ROI-ZOMBIE — 0x004C7240, Labyrinthe des morts (zone 5, sous-zone 9). Le patron de la zone.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =============================================================================================
    // 0x004C7240 — LE ROI-ZOMBIE. Transcrit le 2026-09-17 au desassembleur
    // (Legacy/Raw/Blaze/Labyrinth/FUN_004C7240.asm, 790 instructions, absente de Blaze.c), QUATRE
    // tables de sauts : 0x004C7CB0 (six gestes), 0x004C7CC8 (les quatre temps du sort), 0x004C7CD8
    // (quatre deplacements), 0x004C7CE8 (le choix du geste).
    //
    // UN VOLANT INCANTATEUR DE LA FAMILLE DE LA COCCINELLE : FUN_0049a8e0 et FUN_004a1cf0 appelees,
    // pas de sauvegarde de placement, pas de pose au sol. Et le repertoire de zone en fait un
    // INVOCATEUR :
    //
    //   - CINQ NAISSANCES, une par geste, et leurs sous-types viennent tous du MEME bloc que le
    //     repertoire 0x005e97cc designe en +0xcc : les octets +0x0b, +0x0f, +0x09, +0x17 et +0x18,
    //     chacun augmente de 0xc0. Le Roi-zombie ne sait donc pas ce qu'il appelle : la zone le lui
    //     dit ;
    //   - LE PREMIER APPEL NAIT SUR UNE ANCRE DU MODELE : les trois vitesses sont remplacees par la
    //     ligne que FUN_00535770 rend pour la ligne 0, decalee de douze bits, le temps de la
    //     naissance ; le second se note la proie dans +0x78 ; le troisieme crie d'abord (script de
    //     sons differes 0x00552bf8) ; le cinquieme attend SOIXANTE images la ou les autres en
    //     attendent dix ;
    //   - SON PLONGEON (geste 0) frappe A LA VITESSE, pas a l'image : il ne balaie que si sa vitesse
    //     depasse 3, et lache le bit d'attaque des qu'elle retombe sous 8 ;
    //   - SON SORT est tire dans une liste de SEPT, et son attente vient de l'octet +0x13 de la
    //     fiche — le Roi-zombie attend donc exactement la duree d'incantation du sort tire ;
    //   - SON VOL TIENT A DEUX HAUTEURS : la plus haute du plancher +0x60 et de la reference +0x48
    //     (celle de la proie, rafraichie tant qu'elle n'est pas en l'air) ; en attaque il descend
    //     d'un huitieme vers elle plus 0x28 unites, sinon il flotte au seizieme vers
    //     (sinus + 0x4100) * 32 au-dessus.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552bd8</c> — <b>la sonde du plongeon</b>, relevee dans <c>.data</c> :
    /// <c>{0, 30, 50, 120, 0, 90}</c>.
    /// </summary>
    private static readonly short[] s_ZombieKingProbe_00552BD8 = [0, 0x1e, 0x32, 0x78, 0, 0x5a];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552be8</c> — <b>les sept sorts du Roi-zombie</b>, releves dans
    /// <c>.data</c> : quatre de la premiere famille (<c>000d</c>, <c>0017</c>, <c>0018</c>,
    /// <c>001a</c>) et trois de la troisieme (<c>0200</c>, <c>0208</c>, <c>020c</c>).
    /// </summary>
    private static readonly ushort[] s_ZombieKingSpells_00552BE8 =
        [0x000d, 0x0017, 0x0018, 0x001a, 0x0200, 0x0208, 0x020c];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552bf8</c> — <b>le cri</b>, script de sons differes : deux pas, voix
    /// <c>0x15</c>. Borne par le <c>[1] = 0</c> du second.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552BF8 =
    [
        0x15, 0x03, 0x01, 0x00, 0x2a, 0x03, 0x00, 0x18,   // 0x00552bf8
        0x15, 0x00, 0x01, 0x00, 0x02, 0x03, 0x00, 0x18,   // 0x00552c00 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552BF8Storage;

    /// <summary>
    /// Ghidra: <c>0x004c7240</c> — <b>l'intelligence du Roi-zombie</b>. Voir le bandeau.
    /// </summary>
    private static void TickSceneEntityAi_004C7240(byte* record)
    {
        byte* e = record;

        ResolveEntityBlocks(e, out byte* stats, out _, out _);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        if (speed != 0) DecideZombieKingAction_004C729D(e, distance, heading, traits);

        ApplyZombieKingMotion_004C79B3(e, stats, speed);
    }

    /// <summary>
    /// Ghidra: <c>0x004c729d..0x004c79b2</c> — <b>le premier temps</b> du Roi-zombie, et ses TROIS
    /// tables : les six gestes quand le bit d'attaque est pose, les quatre temps du sort quand
    /// <c>0x804</c> l'est, les quatre deplacements sinon.
    /// </summary>
    private static void DecideZombieKingAction_004C729D(byte* e, int distance, int heading, uint traits)
    {
        AdvanceEntityAnimation_004DB5A0((uint*)e);                  // Ghidra: FUN_004db5a0, appelee

        if ((e[0] & 2) != 0)
        {
            // Ghidra: 0x004c72b0 — le trait 0x10 coupe le geste.
            if ((traits & 0x10) != 0)
            {
                *(uint*)e &= 0xfffffffd;
                e[0x43] = 0;
                return;
            }

            switch (e[0x42])
            {
                case 0: DiveZombieKing_004C72DF(e, heading); return;
                case 1: SummonAnchoredZombieKing_004C7454(e); return;
                case 2: SummonHomingZombieKing_004C7503(e); return;
                case 3: SummonCryingZombieKing_004C7573(e); return;
                case 4: SummonZombieKing(e, 0x17, 0x0a); return;    // Ghidra: 0x004c75f6
                case 5: SummonZombieKing(e, 0x18, 0x3c); return;    // Ghidra: 0x004c7649
                default: return;
            }
        }

        // Ghidra: LAB_004c76a8 — fige, rien ne se decide.
        if ((e[0] & 0x10) != 0) return;

        if ((*(uint*)e & 0x804) != 0)
        {
            // Ghidra: 0x004c76bb — un trait de 0x400003e8 interrompt l'incantation.
            if ((traits & 0x400003e8) != 0)
            {
                *(uint*)e &= 0xfffff7fb;
                e[0x43] = 0;
            }

            CastZombieKing_004C76D0(e, heading);
            return;
        }

        switch (e[0x42])
        {
            case 0: WaitZombieKing_004C77FB(e, distance); return;
            case 1: TurnZombieKing_004C7838(e, heading); return;
            case 2: ApproachZombieKing_004C78AB(e, distance, heading); return;
            case 3: StrafeZombieKing_004C7924(e, heading); return;
        }
    }

    /// <summary>
    /// Ghidra : le virage du Roi-zombie, branchless dans le binaire : <b>sous deux fois le pas, le
    /// cap restant est ajoute d'un coup</b> ; au-dela on tourne du pas.
    /// </summary>
    private static void TurnZombieKingToward(byte* e, int heading, int step)
    {
        int add = ((heading + step) & 0xfff) <= step * 2
            ? heading
            : ((heading & 0xfff) < 0x800 ? step : -step);

        *(short*)(e + 0x32) += (short)add;
    }

    /// <summary>Ghidra : <c>mov dword [esi+18h],n</c> pose l'animation ET remet l'image a zero.</summary>
    private static void StartZombieKingAnimation(byte* e, short animation)
    {
        *(short*)(e + 0x18) = animation;
        *(short*)(e + 0x1a) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c72df..0x004c7453</c> — <b>le plongeon</b> (geste 0), en deux temps sur
    /// <c>+0x10</c> : 1 l'elan a l'image 11, 0 la course, qui frappe TANT QUE LA VITESSE TIENT.
    /// </summary>
    private static void DiveZombieKing_004C72DF(byte* e, int heading)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 1)
        {
            *(int*)(e + 0x5c) = 0;

            if (*(short*)(e + 0x1a) == 0x0b)
            {
                short course = *(short*)(e + 0x32);
                *(int*)(e + 0x5c) = 0x22000;
                *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * 0x22000) >> 0xc;
                *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * 0x22000) >> 0xc;
                *(int*)(e + 0x5c) = 0x5000;

                (*(short*)(e + 0x10))--;
                Aud.PlayPositionalSound(0x24, 0x195, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20
            }

            TurnZombieKingToward(e, heading, 0x80);
            return;
        }

        if (phase != 0) return;

        // Ghidra: LAB_004c7394 — la vitesse a plat decide de tout.
        int vx = *(int*)(e + 0x50) >> 0xc;
        int vz = *(int*)(e + 0x58) >> 0xc;
        int pace = (int)Winsys3d.Ordinal_442((uint)(vx * vx + vz * vz));

        if ((*(uint*)e & 0x2000000) != 0 && pace <= 8) *(uint*)e &= 0xfffffffd;

        if (pace < 3) return;

        TurnZombieKingToward(e, heading, 0x28);

        fixed (short* probe = s_ZombieKingProbe_00552BD8)
        {
            byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

            if (*Mem.Camera(0x005de0b8) == 0) return;

            *(ushort*)(prey + 0x96) = 0x10;
            *(uint*)prey |= 0x40;
            ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
            Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004c7454..0x004c7502</c> — <b>le premier appel</b> (geste 1) : les trois vitesses
    /// sont remplacees par l'ancre de modele de la ligne 0, decalee de douze bits, et
    /// <c>+0x9a</c> par 1, le temps que le sous-type <c>+0x0b</c> du bloc de zone naisse.
    /// </summary>
    private static void SummonAnchoredZombieKing_004C7454(byte* e)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0) { CountDownZombieKingSummon(e); return; }
        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }
        if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        short bob = *(short*)(e + 0x9a);
        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(short*)(e + 0x9a) = 1;

        short* anchor = ModelAnchorRow_00535770(e, 0);              // Ghidra: FUN_00535770
        *(int*)(e + 0x50) = anchor[0] << 0xc;
        *(int*)(e + 0x54) = anchor[1] << 0xc;
        *(int*)(e + 0x58) = anchor[2] << 0xc;

        SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0b), 0xff, -1);   // Ghidra: FUN_0047a9d0

        (*(short*)(e + 0x10))--;
        *(int*)(e + 0x54) = vy;
        *(int*)(e + 0x50) = vx;
        *(int*)(e + 0x58) = vz;
        *(short*)(e + 0x9a) = bob;

        e[0x43] = 0x0a;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c7503..0x004c7572</c> — <b>le second appel</b> (geste 2) : la proie est notee
    /// dans <c>+0x78</c> et <c>+0x9a</c> mis a 2 le temps que le sous-type <c>+0x0f</c> naisse.
    /// </summary>
    private static void SummonHomingZombieKing_004C7503(byte* e)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0) { CountDownZombieKingSummon(e); return; }
        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }
        if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        short bob = *(short*)(e + 0x9a);
        *(short*)(e + 0x9a) = 2;
        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);

        SpawnEffectEntity(e, ZombieKingSummonSubtype(0x0f), 0xff, -1);

        (*(short*)(e + 0x10))--;
        *(short*)(e + 0x9a) = bob;
        e[0x43] = 0x0a;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c7573..0x004c75f5</c> — <b>le troisieme appel</b> (geste 3) : a l'image 10 il
    /// CRIE (script differe 0x00552bf8), puis <c>+0x9a</c> tombe a zero et le sous-type <c>+0x09</c>
    /// nait.
    /// </summary>
    private static void SummonCryingZombieKing_004C7573(byte* e)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0) { CountDownZombieKingSummon(e); return; }
        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }

        if (*(short*)(e + 0x1a) == 0x0a)
        {
            Aud.QueueDelayedSoundScript(
                NativeCopy(s_DelayedSoundScript_00552BF8, ref s_DelayedSoundScript00552BF8Storage),
                (int*)(e + 0x1c));                                  // Ghidra: FUN_0053dbe0
        }

        if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        short bob = *(short*)(e + 0x9a);
        *(short*)(e + 0x9a) = 0;

        SpawnEffectEntity(e, ZombieKingSummonSubtype(9), 0xff, -1);

        (*(short*)(e + 0x10))--;
        *(short*)(e + 0x9a) = bob;
        e[0x43] = 0x0a;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004c75f6</c> et <c>0x004c7649</c> — <b>les quatrieme et cinquieme appels</b>
    /// (gestes 4 et 5) : les sous-types <c>+0x17</c> et <c>+0x18</c>, sans toucher a <c>+0x9a</c>. Le
    /// cinquieme attend <b>soixante</b> images.
    /// </summary>
    private static void SummonZombieKing(byte* e, int offset, byte wait)
    {
        short phase = *(short*)(e + 0x10);

        if (phase == 0) { CountDownZombieKingSummon(e); return; }
        if (phase != 1) { *(int*)(e + 0x5c) = 0; return; }
        if ((*(uint*)e & 0x2000000) == 0) { *(int*)(e + 0x5c) = 0; return; }

        SpawnEffectEntity(e, ZombieKingSummonSubtype(offset), 0xff, -1);

        (*(short*)(e + 0x10))--;
        e[0x43] = wait;
        *(int*)(e + 0x5c) = 0;
    }

    /// <summary>
    /// Ghidra: <c>LAB_004c7692</c> — <b>l'attente d'un appel</b> : elle se decompte SANS test de
    /// zero, et le bit d'attaque ne tombe qu'a l'instant ou elle l'atteint.
    /// </summary>
    private static void CountDownZombieKingSummon(byte* e)
    {
        e[0x43]--;
        if (e[0x43] == 0) *(uint*)e &= 0xfffffffd;
        *(int*)(e + 0x5c) = 0;                                      // Ghidra: LAB_004c76a0
    }

    /// <summary>
    /// Ghidra: <c>[[DAT_005e97cc + 0xcc]][n] + 0xc0</c> — <b>ce que la ZONE fait appeler</b> : le
    /// Roi-zombie ne porte aucun des cinq sous-types, il les lit dans le bloc de son repertoire.
    /// </summary>
    private static byte ZombieKingSummonSubtype(int offset)
    {
        byte* block = *(byte**)(s_Unknown_005e97cc + 0xcc);
        return (byte)((*(byte**)block)[offset] + 0xc0);
    }

    /// <summary>
    /// Ghidra: <c>0x004c76d0..0x004c77e5</c> — <b>le sort</b>, quatre temps comptes a rebours dans
    /// <c>+0x10</c> : 3 le choix, 2 l'attente, 1 l'incantation sur l'animation 7, 0 la fin.
    /// </summary>
    private static void CastZombieKing_004C76D0(byte* e, int heading)
    {
        switch (*(short*)(e + 0x10))
        {
            // ---------------------------------------------------------- 3 : choisir
            case 3:
            {
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);            // Ghidra: FUN_0047aae0

                ushort spell = s_ZombieKingSpells_00552BE8[CrtRand() % 7];
                *(ushort*)(e + 6) = spell;

                // Ghidra: 0x004c77ae — l'attente est la duree d'incantation de la fiche.
                byte* sheet = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)
                                        + ((spell >> 8) & 0xff) * 4)
                              + (spell & 0xff) * 0x30;

                (*(short*)(e + 0x10))--;
                e[0x43] = sheet[0x13];
                *(int*)(e + 0x5c) = 0;
                return;
            }

            // ---------------------------------------------------------- 2 : attendre
            case 2:
            {
                e[0x43]--;
                if (e[0x43] == 0) *(short*)(e + 0x10) = 1;

                TurnZombieKingToward(e, heading, 0x80);
                return;
            }

            // ---------------------------------------------------------- 1 : incanter
            case 1:
            {
                if (*(short*)(e + 0x18) != 7)
                {
                    StartZombieKingAnimation(e, 7);
                    *(uint*)e &= 0xfefffffb;
                    return;
                }

                if (*(short*)(e + 0x1a) != 0x0e) return;

                ushort spell = *(ushort*)(e + 6);
                CastSpell_0053A980((uint*)e, (spell >> 8) & 0xff, spell & 0xff, -1, -1);
                (*(short*)(e + 0x10))--;
                return;
            }

            // ---------------------------------------------------------- 0 : rendre la main
            case 0:
            {
                uint flags = *(uint*)e;
                if ((flags & 0x2000000) == 0) return;
                *(uint*)e = flags & 0xfffff7ff;                     // `and ah,0F7h`
                return;
            }

            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004c77fb..0x004c7837</c> — <b>attendre</b> : animation 0, poussee amortie d'un
    /// seizieme, et l'attente se rouvre des que la proie passe sous <c>0x12c</c>.
    /// </summary>
    private static void WaitZombieKing_004C77FB(byte* e, int distance)
    {
        if (*(short*)(e + 0x18) != 0)
        {
            *(uint*)e |= 0x1000000;
            StartZombieKingAnimation(e, 0);
        }

        if (distance < 0x12c)
        {
            e[0x43] = 0;
            return;
        }

        if (e[0x43] != 0) e[0x43]--;

        int thrust = *(int*)(e + 0x5c);
        *(int*)(e + 0x5c) = thrust - (thrust >> 4);
    }

    /// <summary>
    /// Ghidra: <c>0x004c7838..0x004c78aa</c> — <b>se tourner</b> : animation 0, pas de <c>0x80</c>,
    /// poussee amortie d'un seizieme, et l'attente se rouvre des que le cap est pris.
    /// </summary>
    private static void TurnZombieKing_004C7838(byte* e, int heading)
    {
        if (*(short*)(e + 0x18) != 0)
        {
            *(uint*)e |= 0x1000000;
            StartZombieKingAnimation(e, 0);
        }

        if (((heading + 0x80) & 0xfff) <= 0x100)
        {
            *(short*)(e + 0x32) += (short)heading;
            e[0x43] = 0;
        }
        else
        {
            *(short*)(e + 0x32) += (short)((heading & 0xfff) < 0x800 ? 0x80 : -0x80);
        }

        int thrust = *(int*)(e + 0x5c);
        *(int*)(e + 0x5c) = thrust - (thrust >> 4);
    }

    /// <summary>
    /// Ghidra: <c>0x004c78ab..0x004c7923</c> — <b>approcher</b> : animation 1, et la poussee MONTE
    /// d'un seizieme de ce qui lui manque pour atteindre <c>0x10000</c>.
    /// </summary>
    private static void ApproachZombieKing_004C78AB(byte* e, int distance, int heading)
    {
        if (*(short*)(e + 0x18) != 1)
        {
            *(uint*)e |= 0x1000000;
            StartZombieKingAnimation(e, 1);
        }

        if (distance < 0x12c)
        {
            e[0x43] = 0;
            return;
        }

        if (e[0x43] != 0) e[0x43]--;

        TurnZombieKingToward(e, heading, 0x80);

        int thrust = *(int*)(e + 0x5c);
        *(int*)(e + 0x5c) = thrust + ((0x10000 - thrust) >> 4);
    }

    /// <summary>
    /// Ghidra: <c>0x004c7924..0x004c79ae</c> — <b>louvoyer</b> : animation 0, et la poussee
    /// <c>0x4000</c> porte sur <c>+0x50</c> et <c>+0x58</c> le long du cap tire de <c>+0x10</c>.
    /// </summary>
    private static void StrafeZombieKing_004C7924(byte* e, int heading)
    {
        if (*(short*)(e + 0x18) != 0)
        {
            *(uint*)e |= 0x1000000;
            StartZombieKingAnimation(e, 0);
        }

        if (e[0x43] != 0) e[0x43]--;

        short course = *(short*)(e + 0x10);
        *(int*)(e + 0x5c) = 0x4000;
        *(int*)(e + 0x50) += ((int)Winsys3d.GetSinTableEntry(course) * 0x4000) >> 0xc;
        *(int*)(e + 0x58) += (Winsys3d.GetCosTableEntry(course) * 0x4000) >> 0xc;
        *(int*)(e + 0x5c) = 0;

        TurnZombieKingToward(e, heading, 0x80);
    }

    /// <summary>
    /// Ghidra: <c>0x004c79b3..0x004c7a7b</c> — <b>le deuxieme temps</b> : l'integration volante, puis
    /// le vol proprement dit. La reference <c>+0x48</c> suit le plancher de la proie tant qu'elle
    /// n'est pas en l'air, et c'est la PLUS HAUTE des deux bornes qui sert de sol.
    /// </summary>
    private static void ApplyZombieKingMotion_004C79B3(byte* e, byte* stats, int speed)
    {
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);              // Ghidra: FUN_0049a8e0, appelee
        FinalizeEntityPlacement2((uint*)e);                          // Ghidra: FUN_00533940

        *(uint*)(e + 0x90) &= 0xdfffd8ff;

        byte* chased = *(byte**)(e + 0x6c);
        if ((chased[0] & 1) == 0) *(int*)(e + 0x48) = *(int*)(chased + 0x60);

        // Ghidra: 0x004c79ec — la phase avance de 0x100 a 0x1ff par image.
        *(short*)(e + 0x14) += (short)((CrtRand() & 0xff) + 0x100);

        int ceiling = *(int*)(e + 0x60);
        if (*(int*)(e + 0x48) >= ceiling) ceiling = *(int*)(e + 0x48);

        short phase = *(short*)(e + 0x14);
        int y = *(int*)(e + 0x20);

        if ((e[0] & 2) != 0)
        {
            *(int*)(e + 0x20) = ((ceiling - y + 0x28000) >> 3) + y;
        }
        else
        {
            int target = ((int)Winsys3d.GetSinTableEntry(phase) + 0x4100) << 5;
            *(int*)(e + 0x20) = ((target + ceiling - y) >> 4) + y;
        }

        InterruptFlyingEntity_004A1CF0((uint*)e, stats);             // Ghidra: FUN_004a1cf0

        if (speed == 0) return;
        if ((*(uint*)e & 0x816) != 0) return;
        if (e[0x43] != 0) return;

        ReplanZombieKingAction_004C7A7C(e);
    }

    /// <summary>
    /// Ghidra: <c>0x004c7a7c..0x004c7ca5</c> — <b>le troisieme temps</b> du Roi-zombie, DEUX plages
    /// separees a <c>0x12c</c>.
    ///
    /// <list type="bullet">
    /// <item><b>De pres</b> : sous 60, il plonge s'il fait face, sinon il se tourne ; au-dela il
    /// louvoie.</item>
    /// <item><b>De loin</b> : sous 30 et s'il fait face, IL TIRE UN DES SIX GESTES — le sort ou l'un
    /// des cinq appels ; jusqu'a 79 il approche ; au-dela il louvoie. S'il ne fait pas face, il se
    /// tourne.</item>
    /// </list>
    /// </summary>
    private static void ReplanZombieKingAction_004C7A7C(byte* e)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(short*)(e + 0x32)) & 0xfff;

        int roll = CrtRand() % 100;
        bool facing = ((heading + 0x100) & 0xfff) <= 0x200;

        // ================================================================ de pres
        if (distance < 0x12c)
        {
            if (roll >= 0x3c)
            {
                StrafeZombieKingPlan_004C7C6F(e, heading);
                return;
            }

            if (!facing)
            {
                e[0x42] = 1;                                        // Ghidra: LAB_004c7c3b
                e[0x43] = 1;
                return;
            }

            StartZombieKingAnimation(e, 3);
            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
            e[0x42] = 0;
            *(short*)(e + 0x10) = 1;
            return;
        }

        // ================================================================ de loin
        if (roll < 0x1e)
        {
            if (!facing)
            {
                e[0x42] = 1;                                        // Ghidra: LAB_004c7c3b
                e[0x43] = 1;
                return;
            }

            // Ghidra: 0x004c7b46 — un des six gestes, a parts egales.
            int pick = CrtRand() % 6;

            if (pick == 0)
            {
                StartZombieKingAnimation(e, 6);
                *(uint*)e |= 0x1000804;
                *(short*)(e + 0x10) = 3;
                return;
            }

            StartZombieKingAnimation(e, 3);
            *(uint*)e = (*(uint*)e & 0xfeffffff) | 2;
            e[0x42] = (byte)pick;
            *(short*)(e + 0x10) = 1;
            return;
        }

        if (roll < 0x50)
        {
            e[0x42] = 2;                                            // Ghidra: LAB_004c7c4b
            e[0x43] = (byte)(CrtRand() % 10 + 0x14);
            return;
        }

        StrafeZombieKingPlan_004C7C6F(e, heading);
    }

    /// <summary>
    /// Ghidra: <c>LAB_004c7c6f</c> — <b>le louvoiement</b> : dix a dix-neuf images, et le cap
    /// contourne par la gauche (relevement moins <c>0x600</c>, secoue de <c>0xc00</c>).
    /// </summary>
    private static void StrafeZombieKingPlan_004C7C6F(byte* e, int heading)
    {
        e[0x42] = 3;
        e[0x43] = (byte)(CrtRand() % 10 + 10);
        *(short*)(e + 0x10) = (short)(CrtRand() % 0xc00 + heading + *(short*)(e + 0x32) - 0x600);
    }
}
