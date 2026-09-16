// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE GREMLIN — 0x004A7190, sous-type 2 de la vieille tour (zone 4, sous-zone 0).
//
// CINQ CORPS D'UN COUP : 0x004A7190, 0x004B22D0, 0x004E8DF0, 0x004EDB60 et 0x004F8270 sont
// la meme routine, emise cinq fois. 3 980 octets, table de sauts a treize entrees.
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
    // 0x004A7190 — LE GREMLIN, sous-type 2 de la vieille tour (zone 4, sous-zone 0), et CINQ CORPS
    // D'UN COUP. Transcrite le 2026-09-15 au desassembleur (Legacy/Raw/Blaze/FUN_004A7190.asm,
    // 3 980 octets de code, absente de Blaze.c), table de sauts a treize entrees a 0x004A811C.
    //
    // LES CINQ SONT LA MEME ROUTINE. 0x004A7190, 0x004B22D0, 0x004E8DF0, 0x004EDB60 et 0x004F8270
    // ont ete compares instruction par instruction, cibles d'appel relatives mises a part : ils ne
    // different QUE par l'adresse de leur table de sorts (0x00552A50, 0x00552A88, 0x00552E98,
    // 0x00552EF8, 0x00552FC0). Les cinq tables ont ete relevees et portent LES MEMES DOUZE OCTETS.
    // Chaque copie appelle aussi sa propre copie du chercheur de proie (0x004A8150, 0x004B3290,
    // 0x004E6400, 0x004EB170, 0x004F9230) : toutes identiques a FUN_004A1020 au saut pres. Un seul
    // corps C# les couvre donc sans rien supposer.
    //
    // CE QUI LE DISTINGUE :
    //
    //   - UNE VOLANTE A DEUX REGIMES, fixes a la naissance par +0x16 : a 10 ou plus, la variante est
    //     ramenee de dix et le bit 0x20 tombe — le Gremlin plane alors a 0x8c000 au-dessus du sol ;
    //     en dessous, le bit 0x20 est pose et il plane autour de l'altitude +0x48, que la hauteur de
    //     sa proie tire vers elle.
    //   - IL FLOTTE : a chaque image ou il ne plonge pas, +0x14 avance d'un pas tire au hasard et un
    //     sinus de cette phase fait onduler la hauteur visee. L'integration est celle des volantes
    //     (FUN_0049A8E0), recopiee en ligne.
    //   - TROIS ATTAQUES : la morsure en piqué (posture 0x0c, a l'image 17 de l'animation 3),
    //     L'ACCROCHE (posture 0x0b : il rejoint la hauteur de sa proie et la frappe A CHAQUE IMAGE
    //     au huitieme de son attaque tant que l'animation dure), et le sort (posture 0x0a).
    //   - SON SORT VIENT DE +0x16 : variante 0, un tirage parmi Atchoum, Chant sirenes, Ecrase et
    //     Dodo ; variante 1, Dodo. AU-DELA, LE REGISTRE QUI PORTE LE SORT N'A PAS ETE CHARGE : il
    //     vaut encore 32, le compte de la boucle qui vient de balayer la table des effets. Le sort
    //     lance est alors famille 0 indice 32, alors que la famille 0 n'a que 29 fiches.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552a50</c> et <c>DAT_00552a58</c> — <b>les sorts du Gremlin</b>,
    /// releves dans <c>.data</c> : <c>0d 00 07 02 08 02 00 02</c> puis <c>00 02 00 00</c>.
    ///
    /// <para>La variante 0 tire <c>hasard % 4</c> dans les quatre mots — famille 0 indice 13
    /// (Atchoum), famille 2 indices 7 (Chant sirenes), 8 (Ecrase) et 0 (Dodo). La variante 1 lit le
    /// dword qui suit, masque sur seize bits : famille 2 indice 0, Dodo.</para>
    ///
    /// <para>Les quatre autres copies du corps lisent leur propre table (<c>0x00552A88</c>,
    /// <c>0x00552E98</c>, <c>0x00552EF8</c>, <c>0x00552FC0</c>) ; les cinq portent les memes
    /// octets, verifies un par un.</para>
    /// </summary>
    private static readonly ushort[] s_GremlinSpells_00552A50 = [0x000d, 0x0207, 0x0208, 0x0200];
    private const ushort s_GremlinSpell_00552A58 = 0x0200;

    /// <summary>
    /// Ghidra: <c>0x004a7190</c> (et ses quatre copies) — <b>l'intelligence du Gremlin</b>.
    ///
    /// <para><b>La charpente est celle du Varan</b> — memes feuilles propres
    /// (<see cref="CancelGestureOnTrait_004D5DE0"/>, <see cref="ProbeClearSide_004D5E60"/>,
    /// <see cref="ResetSceneEntityOnStun_004C3810"/>), meme heurt de mur qui annule l'image — mais la
    /// queue est celle d'une volante : ni pose au sol par <c>+0x60</c> en fin d'image, ni gravite.</para>
    ///
    /// <para><b>La sauvegarde du placement ne garde pas <c>+0x20</c></b> : la hauteur n'est ni
    /// relevee ni rendue par le heurt, contrairement au Varan. Une volante heurtee garde la hauteur
    /// qu'elle vient de prendre.</para>
    ///
    /// <para><b>Le bit <c>1</c> tombe au debut de chaque image</b>, avant tout le reste, et ne se
    /// releve que par le plancher : c'est le bit « en l'air » des terrestres, que la volante n'a pas
    /// l'usage de garder.</para>
    /// </summary>
    private static void TickSceneEntityAi_004A7190(byte* record)
    {
        byte* e = record;

        // Ghidra: [esp+0x2c] — la sonde, batie sur la pile a chaque image.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0x14;
        probe[2] = 0x32;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0x50;

        if (e[0x41] == 0)
        {
            e[0x41] = 1;
            PickChasedPartyMember_004A1020(e, 3, 0);   // Ghidra: FUN_004a8150, copie de FUN_004a1020
            *(int*)(e + 0x48) = *(int*)(e + 0x20);

            if (*(short*)(e + 0x16) >= 10)
            {
                *(short*)(e + 0x16) -= 10;
                *(uint*)e &= 0xffffffdf;
            }
            else
            {
                *(uint*)e |= 0x20;
            }
        }

        *(uint*)e &= 0xfffffffe;

        // Ghidra: 0x004a7203 — le binaire range dix champs et le cap, pas +0x20. La structure
        // commune le lit aussi ; la lecture est sans effet, et la restauration ci-dessous ne
        // l'ecrit pas.
        SceneEntityPlacement keep;
        SaveSceneEntityPlacement(e, &keep);

        byte* chased = *(byte**)(e + 0x6c);

        ResolveEntityBlocks(e, out _, out _, out byte* model);

        int boldness, distance, heading, speed;
        uint traits;
        ReadSceneEntitySituation_00535F20(e, &boldness, &distance, &heading, &speed, &traits);

        int timid = 0x40 - boldness;

        CancelGestureOnTrait_004D5DE0(e, traits);

        // Ghidra: 0x004a72cb — la distance est RECALCULEE, a plat cette fois (le Varan la reprend a
        // trois dimensions).
        int dx = (*(int*)(e + 0x1c) - *(int*)(chased + 0x1c)) >> 0xc;
        int dz = (*(int*)(e + 0x24) - *(int*)(chased + 0x24)) >> 0xc;
        distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        // ---------------------------------------------------------------- 1. decider
        if (speed != 0)
        {
            DecideGremlinAction_004A7190(e, probe, model, chased, boldness, distance, heading, speed);
            HoverGremlin_004A7AF0(e, chased);
        }

        // ---------------------------------------------------------------- 2. appliquer
        // Ghidra: LAB_004a7baf — FUN_0049a8e0 recopiee en ligne, amortissements compris.
        IntegrateFlyingEntityMotion_0049A8E0(e, speed);
        FinalizeEntityPlacement2((uint*)e);                        // Ghidra: FUN_00533940

        uint flags = *(uint*)e;
        uint contact = *(uint*)(e + 0x90) & 0xfffff8ff;
        *(uint*)(e + 0x90) = contact;

        // En vol, deux bits de contact de plus sont ignores : 0x2000 et 0x20000000.
        if ((flags & 0x20) != 0) *(uint*)(e + 0x90) = contact & 0xdfffdfff;

        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (*(uint*)(e + 0x90) & 0x8000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(uint*)e = flags & 0xfffffffe;
            *(int*)(e + 0x54) = 0;
        }

        if ((*(uint*)(e + 0x90) & 0x2000a882) != 0)
        {
            // ================================================================ heurtee
            if (e[0x42] != 7)
            {
                e[0x42] = 7;
                int roll = CrtRand();                               // Ghidra: FUN_00547140
                *(ushort*)(e + 0x12) = 0;
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(roll % 0x28 + 0x14);
            }

            *(uint*)e &= 0xfffffffd;

            // Les dix champs, sans la hauteur.
            *(int*)(e + 0x24) = keep.Z;
            *(int*)(e + 0x1c) = keep.X;
            *(int*)(e + 0x7c) = keep.At7c;
            *(uint*)(e + 0x90) = keep.At90;                         // `and eax,0FFFFh` puis le dword
            *(int*)(e + 0x80) = keep.At80;
            *(int*)(e + 0x84) = keep.At84;
            *(int*)(e + 0x88) = keep.At88;
            *(int*)(e + 0x8c) = keep.At8c;
            *(int*)(e + 0x64) = keep.At64;
            *(int*)(e + 0x60) = keep.At60;

            int side = ProbeClearSide_004D5E60(e, 0x2000a882);
            if ((short)side == 0) side = (CrtRand() % 2) * 2 - 1;   // Ghidra: FUN_00547140

            *(ushort*)(e + 0x32) += (ushort)((boldness + 0x40) * side);
        }

        ResetSceneEntityOnStun_004C3810(e);

        // ---------------------------------------------------------------- 3. replanifier
        if (speed == 0)
        {
            *(short*)(e + 0x32) = keep.Heading;
            return;
        }

        if (e[0x43] != 0) return;

        ReplanGremlinAction_004A7190(e, boldness, timid);
    }

    /// <summary>
    /// Ghidra: <c>0x004a7305..0x004a7aef</c> — <b>le premier temps</b> du Gremlin : l'animation, le
    /// prelude de geste, puis la case de la posture.
    ///
    /// <para><b>Treize cases, dont huit servies</b> : 0 attendre, 1 se tourner, 2 approcher,
    /// 3 filer vers un cap, 7 marcher droit (le contournement), 0x0a le sort, 0x0b l'accroche,
    /// 0x0c le pique. Les cases 4, 5, 6, 8 et 9 de la table pointent la fin du bloc.</para>
    ///
    /// <para>Comme chez le Varan, <b>le prelude ne rend pas la main</b> : la case de la posture
    /// s'execute la meme image. Et <b>il ne ferme pas le geste du pique</b> : l'animation qui finit
    /// efface le bit de geste pour toute posture sauf 0x0c, qui se termine par sa propre course.</para>
    /// </summary>
    private static void DecideGremlinAction_004A7190(
        byte* e, short* probe, byte* model, byte* chased, int boldness, int distance, int heading, int speed)
    {
        // Ghidra: 0x004a7305..0x004a7365 — FUN_004db5a0 recopiee en ligne, au mot pres.
        AdvanceEntityAnimation_004DB5A0((uint*)e);

        // ================================================================ le prelude de geste
        if ((*(uint*)e & 2) != 0)
        {
            if (e[0x42] == 0x0b)
            {
                GremlinCling_004A73EF(e, probe, model);
            }
            else if (*(short*)(e + 0x1a) == 0x11)
            {
                GremlinBite_004A7381(e, probe);
            }

            uint gesture = *(uint*)e;
            if ((gesture & 0x2000000) != 0 && e[0x42] != 0x0c)
            {
                *(uint*)e = gesture & 0xfffffffd;
                e[0x43] = 0;
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

            // ---------------------------------------------------------- 1 : se tourner
            case 1:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                }

                *(int*)(e + 0x5c) = 0;

                if (((heading + 0xc8) & 0xfff) <= 0x190)
                {
                    e[0x43] = 0;
                    return;
                }

                *(ushort*)(e + 0x32) +=
                    (ushort)((heading & 0xfff) < 0x800 ? boldness + 0x80 : -0x80 - boldness);
                return;

            // ---------------------------------------------------------- 2 : approcher
            case 2:
                if (*(ushort*)(e + 0x12) == 0)
                {
                    *(ushort*)(e + 0x18) = 0;
                    *(ushort*)(e + 0x1a) = 0;
                    *(uint*)e |= 0x1000000;
                    *(ushort*)(e + 0x12) = 1;
                    *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
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
                    (ushort)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x80 : -0x80 - boldness * 2);
                return;

            // ---------------------------------------------------------- 3 : filer vers un cap
            case 3:
                if (*(ushort*)(e + 0x12) == 1)
                {
                    if (e[0x43] != 0) e[0x43]--;

                    // Il avance lui-meme le long du cap range en +0x10 et remet sa poussee a zero,
                    // comme le Varan — mais son virage vers la cible est QUATRE fois plus vif.
                    int thrust = boldness * 0x90 + 0x5000;
                    int step = (speed * thrust) >> 0xc;
                    *(int*)(e + 0x5c) = thrust;

                    short course = *(short*)(e + 0x10);
                    *(int*)(e + 0x1c) += ((int)Winsys3d.GetSinTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x24) += (Winsys3d.GetCosTableEntry(course) * step) >> 0xc;
                    *(int*)(e + 0x5c) = 0;

                    if (((heading + 0x100) & 0xfff) > 0x200)
                    {
                        *(ushort*)(e + 0x32) += (ushort)((heading & 0xfff) < 0x800
                            ? boldness * 8 + 0x32
                            : -0x32 - boldness * 8);
                    }
                }

                if (*(ushort*)(e + 0x12) != 0) return;

                *(ushort*)(e + 0x18) = 0;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x12) = 1;
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

                *(ushort*)(e + 0x18) = 0;
                *(ushort*)(e + 0x1a) = 0;
                *(uint*)e |= 0x1000000;
                *(ushort*)(e + 0x12) = 1;
                *(int*)(e + 0x5c) = boldness * 0x90 + 0x5000;
                return;
            }

            // ---------------------------------------------------------- 0x0a : le sort
            case 0x0a:
                GremlinCast_004A7733(e, chased);
                return;

            // ---------------------------------------------------------- 0x0b : l'accroche
            case 0x0b:
            {
                int y = *(int*)(e + 0x20);
                int preyY = *(int*)(chased + 0x20);

                // Trop bas sous sa proie — plus de 0xb4 unites — il lache.
                if (y + 0xb4000 < preyY)
                {
                    e[0x43] = 0;
                    *(uint*)e &= 0xfffffffd;
                    *(int*)(e + 0x5c) = 0;
                    return;
                }

                // Il rejoint la hauteur de sa proie, 0x28 unites au-dessus, d'un quart par image.
                *(int*)(e + 0x20) = y + ((preyY - y + 0x28000) >> 2);

                ushort phase = *(ushort*)(e + 0x12);

                if (phase == 1 && (*(uint*)e & 0x2000000) != 0)
                {
                    *(uint*)e &= 0xfffffffd;
                    e[0x43] = 0;
                }

                if (phase != 0) return;

                *(ushort*)(e + 0x12) = 1;
                *(uint*)e &= 0xfeffffff;
                return;
            }

            // ---------------------------------------------------------- 0x0c : le pique
            case 0x0c:
                GremlinDive_004A7980(e, chased, boldness, heading);
                return;

            // ---------------------------------------------------------- 4, 5, 6, 8, 9 : rien
            default:
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004a7381..0x004a73d3</c> — <b>la morsure</b>, a l'image 17 de tout geste qui
    /// n'est pas l'accroche. Le releve de <c>+0x1a</c> est refait apres le balayage, comme chez le
    /// Varan. Le clignotement pose sur la victime vaut <b>4</b>.
    /// </summary>
    private static void GremlinBite_004A7381(byte* e, short* probe)
    {
        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;
        if (*(short*)(e + 0x1a) != 0x11) return;

        *(ushort*)(prey + 0x96) = 4;
        *(uint*)prey |= 0x40;

        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x004a73ef..0x004a7457</c> — <b>l'accroche</b>, le geste de la posture 0x0b.
    ///
    /// <para><b>Sans condition d'image</b> : tant que le bit de geste tient, la sonde est croisee a
    /// chaque image, et chaque image qui touche porte un coup. L'attaque du bloc de modele est
    /// divisee par <b>HUIT</b> le temps de la resolution — le Varan la divise par quatre, les
    /// betes qui empoignent par deux. C'est une succession de petites morsures, pas un coup.
    /// Le clignotement pose sur la victime vaut 8.</para>
    /// </summary>
    private static void GremlinCling_004A73EF(byte* e, short* probe, byte* model)
    {
        byte* prey = SweepPartyForMeleeHit_00519C30(e, (int*)(e + 0x1c), probe, 0);

        if (*Mem.Camera(0x005de0b8) == 0) return;

        *(ushort*)(prey + 0x96) = 8;
        *(uint*)prey |= 0x40;

        short power = *(short*)(model + 0x30);
        *(short*)(model + 0x30) = (short)(power >> 3);            // `sar ax,3`
        ResolveMeleeHit_00539B80((uint*)e, (uint*)prey);
        *(short*)(model + 0x30) = power;

        Aud.PlayPositionalSound(3, 0x300, 0x1800, (int*)(prey + 0x1c));   // Ghidra: FUN_0053db20
    }

    /// <summary>
    /// Ghidra: <c>0x004a7733..0x004a790b</c> — <b>le sort</b>, posture 0x0a, en quatre phases dans
    /// <c>+0x12</c>.
    ///
    /// <para><b>Phase 0, l'armement, a trois refus.</b> Si une entite de la table des effets
    /// <c>0x005ff400</c> porte encore ce Gremlin en <c>+0x6c</c> et vit, il renonce — un sort a la
    /// fois. S'il porte deja le bit <c>0x800</c> ou <c>0x4</c>, il renonce et les efface. S'il est a
    /// plus de 0xb4 unites sous sa proie, il renonce aussi. Sinon l'entite de lancement <c>0xbd</c>
    /// nait, le sort est choisi, sa duree (<c>+0x13</c> de la fiche) part dans <c>+0x10</c>, et les
    /// bits <c>0x1000804</c> sont leves.</para>
    ///
    /// <para><b>Le registre du sort.</b> Le mot est porte par <c>edi</c>, que la boucle de refus
    /// vient d'utiliser comme compteur : il en sort a <b>32</b>. Les variantes 0 et 1 le rechargent ;
    /// toute autre variante lance donc le mot <c>0x0020</c>, famille 0 indice 32, au-dela des 29
    /// fiches de la famille 0. <b>Defaut probable de 1998</b>, transcrit tel quel et non observe en
    /// jeu : la variante d'un Gremlin n'est pas encore relevee.</para>
    ///
    /// <para><b>Phases 1 a 3</b> : il monte vers la hauteur de sa proie plus 0x14 unites d'un
    /// huitieme par image pendant la duree du sort (phase 1), prend l'animation 6 et lance a son
    /// image 3 (phase 2), puis attend la fin de l'animation pour effacer <c>0x800</c> (phase 3).</para>
    /// </summary>
    private static void GremlinCast_004A7733(byte* e, byte* chased)
    {
        if (*(ushort*)(e + 0x12) == 0)
        {
            byte* effects = Mem.Entity(0x005ff400);
            int scanned = 0;                                        // Ghidra: edi

            for (; scanned < 0x20; scanned++)
            {
                byte* effect = effects + scanned * 0x9c;
                if (*(byte**)(effect + 0x6c) == e && (*(uint*)effect & 0x80000000) != 0)
                {
                    e[0x43] = 0;                                    // Ghidra: LAB_004a8113
                    return;
                }
            }

            uint flags = *(uint*)e;

            if ((flags & 0x804) != 0)
            {
                e[0x43] = 0;
                *(uint*)e = flags & 0xfffff7fb;
                return;
            }

            if (*(int*)(e + 0x20) + 0xb4000 < *(int*)(chased + 0x20))
            {
                *(uint*)e = flags & 0xfffff7f9;
                e[0x43] = 0;
                *(int*)(e + 0x5c) = 0;
                return;
            }

            SpawnCastEntity_0047AAE0(e, 0xbd, 0, -1);
            *(ushort*)(e + 0x12) = 1;

            ushort spell = (ushort)scanned;                         // 32 : le compteur, pas un sort
            if (*(short*)(e + 0x16) == 0)
            {
                spell = s_GremlinSpells_00552A50[CrtRand() % 4];     // Ghidra: FUN_00547140
            }

            if (*(short*)(e + 0x16) == 1)
            {
                spell = s_GremlinSpell_00552A58;
            }

            *(ushort*)(e + 6) = spell;

            byte* sheet = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)
                                    + ((spell >> 8) & 0xff) * 4)
                          + (spell & 0xff) * 0x30;
            *(ushort*)(e + 0x10) = sheet[0x13];

            *(uint*)e |= 0x1000804;
            *(int*)(e + 0x5c) = 0;
            *(ushort*)(e + 0x18) = 5;
            *(ushort*)(e + 0x1a) = 0;
        }

        if (*(ushort*)(e + 0x12) == 1)
        {
            RiseTowardPrey(e, chased);
            (*(short*)(e + 0x10))--;

            if (*(short*)(e + 0x10) < 1)
            {
                *(ushort*)(e + 0x12) = 2;
                *(uint*)e &= 0xfefffffb;
                *(ushort*)(e + 0x18) = 6;
                *(ushort*)(e + 0x1a) = 0;
            }
        }

        if (*(ushort*)(e + 0x12) == 2)
        {
            RiseTowardPrey(e, chased);

            if (*(short*)(e + 0x1a) == 3)
            {
                ushort spell = *(ushort*)(e + 6);
                CastSpell_0053A980((uint*)e, (spell >> 8) & 0xff, spell & 0xff, -1, -1);
                *(ushort*)(e + 0x12) = 3;
            }
        }

        if (*(ushort*)(e + 0x12) != 3) return;

        RiseTowardPrey(e, chased);

        uint done = *(uint*)e;
        if ((done & 0x2000000) == 0) return;

        e[0x43] = 0;
        *(uint*)e = done & 0xfffff7ff;                             // `and ah,0F7h`
    }

    /// <summary>Ghidra: le motif des trois phases du sort — la hauteur rejoint celle de la proie
    /// <b>plus 0x14 unites</b>, d'un huitieme par image.</summary>
    private static void RiseTowardPrey(byte* e, byte* chased)
    {
        int y = *(int*)(e + 0x20);
        *(int*)(e + 0x20) = y + ((*(int*)(chased + 0x20) - y + 0x14000) >> 3);
    }

    /// <summary>
    /// Ghidra: <c>0x004a7980..0x004a7aee</c> — <b>le pique</b>, posture 0x0c, en trois phases.
    ///
    /// <para><b>Phase 1, la montee</b> : +3 unites par image en regardant sa proie, jusqu'a
    /// 0xc8 unites au-dessus du plancher. Il leve alors les bits <c>0x1000002</c> — le geste — et
    /// passe a l'animation 3. Plus de 0x118 unites sous sa proie, il abandonne.</para>
    ///
    /// <para><b>Phase 2, la plongee</b> : poussee fixe <c>0x1e000</c>, la hauteur file vers celle
    /// de la proie plus 0x28 unites d'un quart par image, pendant 0x21 images comptees dans
    /// <c>+0x10</c>. C'est pendant cette phase que le prelude mord, a l'image 17 de l'animation.</para>
    ///
    /// <para><b>Phase 3, le freinage</b> : la poussee perd un huitieme par image, et la posture
    /// rend la main quand sa partie entiere passe sous <c>0xa000</c>.</para>
    /// </summary>
    private static void GremlinDive_004A7980(byte* e, byte* chased, int boldness, int heading)
    {
        if (*(ushort*)(e + 0x12) == 1)
        {
            int y = *(int*)(e + 0x20) + 0x3000;
            *(int*)(e + 0x20) = y;

            if ((y - *(int*)(e + 0x60)) >> 0xc > 0xc8)
            {
                *(uint*)e |= 0x1000002;
                *(ushort*)(e + 0x12) = 2;
                *(ushort*)(e + 0x10) = 0;
                *(ushort*)(e + 0x18) = 3;
                *(ushort*)(e + 0x1a) = 0;
            }

            TurnGremlinTowardPrey(e, boldness, heading);

            if (y + 0x118000 < *(int*)(chased + 0x20))
            {
                e[0x43] = 0;
                *(uint*)e &= 0xfffffffd;
                *(int*)(e + 0x5c) = 0;
                return;
            }
        }

        if (*(ushort*)(e + 0x12) == 2)
        {
            *(int*)(e + 0x5c) = 0x1e000;

            int y = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = y + ((*(int*)(chased + 0x20) - y + 0x28000) >> 2);

            (*(short*)(e + 0x10))++;
            short frames = *(short*)(e + 0x10);

            TurnGremlinTowardPrey(e, boldness, heading);

            if (frames >= 0x21)
            {
                *(ushort*)(e + 0x12) = 3;
                *(uint*)e &= 0xfffffffd;
            }
        }

        ushort phase = *(ushort*)(e + 0x12);

        if (phase == 3)
        {
            int v = *(int*)(e + 0x5c);
            v -= v >> 3;
            *(int*)(e + 0x5c) = v;
            if ((v & unchecked((int)0xfffff000)) < 0xa000) e[0x43] = 0;
        }

        if (phase != 0) return;

        *(ushort*)(e + 0x12) = 1;
        *(ushort*)(e + 0x18) = 4;
        *(ushort*)(e + 0x1a) = 0;
        *(int*)(e + 0x5c) = 0;
        *(uint*)e |= 0x1000000;
    }

    /// <summary>Ghidra: le virage des deux premieres phases du pique — hors d'un cone de
    /// <c>±0x100</c>, <c>hardiesse * 2 + 0x80</c> vers la proie.</summary>
    private static void TurnGremlinTowardPrey(byte* e, int boldness, int heading)
    {
        if (((heading + 0x100) & 0xfff) <= 0x200) return;

        *(ushort*)(e + 0x32) +=
            (ushort)((heading & 0xfff) < 0x800 ? boldness * 2 + 0x80 : -0x80 - boldness * 2);
    }

    /// <summary>
    /// Ghidra: <c>0x004a7af0..0x004a7bae</c> — <b>le vol stationnaire</b>, apres la posture et
    /// avant l'integration.
    ///
    /// <para>A chaque image ou il ne prepare pas un pique (posture 0x0c sans bit de geste), la phase
    /// <c>+0x14</c> avance de <c>hasard % 50 + 0x64</c> sur douze bits, et la hauteur file d'un
    /// huitieme vers une cible qui ondule de <c>sin(phase) * 60</c> :</para>
    /// <list type="bullet">
    /// <item>en vol (bit <c>0x20</c>) autour de <c>+0x48 + 0xb4</c> unites ;</item>
    /// <item>sinon autour de <c>+0x60 + 0x8c</c> unites, a hauteur fixe au-dessus du plancher.</item>
    /// </list>
    /// <para><b>L'altitude de reference <c>+0x48</c> suit la proie</b>, en vol seulement et meme
    /// pendant un pique : quand la hauteur de la proie et son <c>+0x64</c> ont la meme partie
    /// entiere, <c>+0x48</c> prend ce <c>+0x64</c>.</para>
    /// </summary>
    private static void HoverGremlin_004A7AF0(byte* e, byte* chased)
    {
        uint flags = *(uint*)e;
        bool bobbing = e[0x42] != 0x0c || (flags & 2) != 0;

        if ((flags & 0x20) != 0)
        {
            if (bobbing)
            {
                int wave = (int)Winsys3d.GetSinTableEntry(AdvanceGremlinBob(e));
                int y = *(int*)(e + 0x20);
                int target = *(int*)(e + 0x48) + (wave + 0x3000) * 60;
                *(int*)(e + 0x20) = y + ((target - y) >> 3);
            }

            if (((*(int*)(chased + 0x20) ^ *(int*)(chased + 0x64)) & unchecked((int)0xfffff000)) == 0)
            {
                *(int*)(e + 0x48) = *(int*)(chased + 0x64);
            }

            return;
        }

        if (!bobbing) return;

        int groundWave = (int)Winsys3d.GetSinTableEntry(AdvanceGremlinBob(e));
        int groundY = *(int*)(e + 0x20);
        *(int*)(e + 0x20) = groundY + ((*(int*)(e + 0x60) + groundWave * 60 - groundY + 0x8c000) >> 3);
    }

    /// <summary>Ghidra: <c>add dx,[esi+14h] / add edx,64h / and edx,0FFFh</c> — la phase
    /// d'ondulation avance d'un pas tire, et est rangee sur douze bits.</summary>
    private static short AdvanceGremlinBob(byte* e)
    {
        int roll = CrtRand() % 0x32;                                // Ghidra: FUN_00547140
        short phase = (short)((roll + *(short*)(e + 0x14) + 0x64) & 0xfff);
        *(short*)(e + 0x14) = phase;
        return phase;
    }

    /// <summary>
    /// Ghidra: <c>0x004a7d92..0x004a8112</c> — <b>le troisieme temps</b> du Gremlin. Distance et cap
    /// recalcules a plat vers la proie, puis trois plages exclusives.
    ///
    /// <para><b>Au contact, sous 0x96</b> : une fois sur deux il tente l'attaque — dans un cone qui
    /// s'ouvre avec la timidite et a une portee tiree, comme le Varan — et arme l'accroche six fois
    /// sur dix, sinon il se pose. Hors cone, il se tourne. Deux fois sur dix il file ; le reste, la
    /// hardiesse decide entre le pique ou le sort et l'attente.</para>
    ///
    /// <para><b>A mi-distance, de 0x96 a 0x258</b> : une fois sur cinq, face a sa proie, il
    /// <b>lance son sort sans condition de variante</b> — c'est ce chemin qui fait incanter une
    /// variante non nulle.</para>
    ///
    /// <para><b>Au loin</b>, la derniere comparaison du binaire (<c>tirage &lt; 0x3c</c>) est
    /// <b>morte</b> : on n'y arrive qu'avec un tirage d'au moins 0x4b. Transcrite pour memoire.</para>
    /// </summary>
    private static void ReplanGremlinAction_004A7190(byte* e, int boldness, int timid)
    {
        byte* chased = *(byte**)(e + 0x6c);

        int dx = (*(int*)(chased + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(e + 0x24)) >> 0xc;

        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
        int heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(e + 0x32)) & 0xfff;

        *(ushort*)(e + 0x12) = 0;

        // ================================================================ le contact
        if (distance < 0x96)
        {
            int roll = CrtRand() % 100;                             // Ghidra: FUN_00547140

            if (roll < 0x32)
            {
                bool inCone = ((heading + timid * 4 + 0x100) & 0xfff) <= timid * 8 + 0x200;
                bool inReach = inCone && distance < CrtRand() % (timid * 2 + 1) + 0x64;

                if (!inReach)
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
                else if (roll < 0x1e)
                {
                    // Ghidra: 0x004a7e46 — armer L'ACCROCHE.
                    uint flags = *(uint*)e;
                    e[0x42] = 0x0b;
                    e[0x43] = 1;
                    *(int*)(e + 0x5c) = 0;
                    *(uint*)e = (flags & 0xfeffffff) | 2;
                    *(ushort*)(e + 0x18) = 3;
                    *(ushort*)(e + 0x1a) = 0;
                }
                else
                {
                    e[0x42] = 0;
                    e[0x43] = (byte)(CrtRand() % 0x28);             // Ghidra: FUN_00547140
                }
            }
            else if (roll < 0x46)
            {
                e[0x42] = 3;
                *(ushort*)(e + 0x12) = 0;
                e[0x43] = SceneEntityDelay(timid);
                *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);  // Ghidra: FUN_00547140
            }
            else
            {
                ChooseGremlinStrike(e, roll, boldness);
            }

            return;
        }

        // ================================================================ la mi-distance
        if (distance < 0x258)
        {
            int roll = CrtRand() % 100;                             // Ghidra: FUN_00547140

            if (roll < 0x14)
            {
                if (((heading + 0x400) & 0xfff) < 0x800)
                {
                    // Ghidra: LAB_004a7ff9 — face a la proie : LE SORT, quelle que soit la variante.
                    e[0x42] = 0x0a;
                    e[0x43] = 1;
                    *(ushort*)(e + 0x12) = 0;
                }
                else
                {
                    e[0x42] = 1;
                    e[0x43] = 1;
                }
            }
            else if (roll < 0x28)
            {
                e[0x42] = 2;
                int delay = CrtRand() % 10;                         // Ghidra: FUN_00547140
                *(ushort*)(e + 0x10) = 0x50;
                e[0x43] = (byte)(delay + (byte)(timid >> 1) + 0x14);
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
                ChooseGremlinStrike(e, roll, boldness);
            }

            return;
        }

        // ================================================================ le lointain
        int far = CrtRand() % 100;                                  // Ghidra: FUN_00547140

        if (far < 0x32)
        {
            if (far < boldness * 2 + 0x0a)
            {
                e[0x42] = 2;
                int delay = CrtRand() % 10;                         // Ghidra: FUN_00547140
                *(ushort*)(e + 0x10) = 0x64;
                e[0x43] = (byte)(delay + (byte)(timid >> 1) + 0x14);
                return;
            }

            // Ghidra: LAB_004a807e — l'attente, au rebours COURT : timidite / 4, sans les 0x14.
            e[0x42] = 0;
            int wait = CrtRand() % 10;                              // Ghidra: FUN_00547140
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = (byte)(wait + (byte)(timid >> 2));
            *(short*)(e + 0x10) = (short)(boldness * 2 + 0x50);
            return;
        }

        if (far < 0x4b)
        {
            e[0x42] = 3;
            *(ushort*)(e + 0x12) = 0;
            e[0x43] = SceneEntityDelay(timid);
            *(short*)(e + 0x10) = (short)(CrtRand() % 0x1000);      // Ghidra: FUN_00547140
            return;
        }

        // Ghidra: 0x004a80fb — `cmp edx,3Ch / jge`, avec un tirage d'au moins 0x4b : jamais pris.
        if (far < 0x3c)
        {
            e[0x42] = 1;
            e[0x43] = 1;
        }
    }

    /// <summary>Ghidra: <c>0x004a7ed5</c> et <c>0x004a7fdf</c> — le choix commun aux deux plages
    /// proches pour un tirage haut. <c>100 - tirage</c> sous <c>hardiesse / 2 + 10</c> : la
    /// variante 0 lance son sort, toute autre plonge. Sinon l'attente, a rebours
    /// <c>hasard % 0x28</c>.</summary>
    private static void ChooseGremlinStrike(byte* e, int roll, int boldness)
    {
        if (100 - roll < (boldness >> 1) + 0x0a)
        {
            if (*(short*)(e + 0x16) == 0)
            {
                e[0x42] = 0x0a;
                e[0x43] = 1;
                *(ushort*)(e + 0x12) = 0;
            }
            else
            {
                e[0x42] = 0x0c;
                e[0x43] = 1;
            }

            return;
        }

        e[0x42] = 0;
        e[0x43] = (byte)(CrtRand() % 0x28);                         // Ghidra: FUN_00547140
    }
}
