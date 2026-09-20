// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES DE LA GROTTE SCELLEE — zone 10, table 0x0056b0c8.
//
// Ouvert le 2026-09-20 au lot 8 de la zone, avec ses deux premieres scenes : les deux morts de
// membre des sous-zones 4 et 9. ARCHITECTURE.md prevoyait ce fichier pour la grotte — « les scenes
// et les mecanismes s'ouvriront avec leur premier corps » — et c'est ce corps-la.
//
// Ces deux-la sont de la famille de EntityHandler_0040E3A0 (montagne du dragon de feu) : meme
// ossature — les deux appels de tete, le balayage des trente-deux entites de scene qui retient la
// DERNIERE, le tampon de secours DAT_005de558, et la sortie par RunEntityScript +
// UnfreezeAllEntities + le bit 0x2004000 sur la victime.
//
// Dumps : Legacy/Raw/Blaze/SealedCave/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_00437dd0 @ 0x00437dd0 — sous-zones 4 et 9, table <c>0x0056b0c8</c>. Decompilee
    /// dans <c>Blaze.c</c> ; 204 instructions.
    ///
    /// <para>
    /// <b>La mort par dessiccation.</b> Cent quarante images pendant lesquelles un membre du groupe
    /// se dessèche et part en poussiere. La victime est <b>la derniere</b> entite de scene vivante
    /// dont l'octet <c>+0x0c</c> est nul : le balayage ne s'arrete pas au premier, et c'est elle
    /// qui donne sa position a la scene.
    /// </para>
    /// <list type="table">
    ///   <item><term>l'ouverture</term><description>le fond passe en <c>(0xfa, 0xe6, 0xd2)</c> —
    ///     le sable —, l'ancien etant range dans <c>+0x50</c>..<c>+0x58</c> ; la victime est remise a
    ///     blanc (<c>0xffffff</c>), pose 0, image 0, et son <c>+0x36</c> passe a 1. Le son
    ///     <c>0x16</c> en direct.</description></item>
    ///   <item><term>chaque image</term><description>une image sur deux, un <c>0xf4</c> ; tant que
    ///     le compte est sous 100, un <c>0xf5</c> une fois sur deux au hasard ; et une image sur
    ///     deux l'animation de la victime avance d'un cran, bornee sur sa derniere.</description></item>
    ///   <item><term>passe la vingtieme</term><description>la victime perd 2 par canal et par image
    ///     jusqu'au noir — <b>le test de plancher ne regarde que le rouge</b>, et met les trois a
    ///     zero ensemble.</description></item>
    ///   <item><term>a chaque image, quoi qu'il arrive</term><description>elle s'aplatit de
    ///     <c>0x14</c> et s'elargit de 10 sur les deux autres axes ; a plat, la hauteur reste a
    ///     zero.</description></item>
    ///   <item><term>la boucle de sons</term><description><c>0x23</c> a la dix-neuvieme image du
    ///     cycle, <c>0x0f</c> a la vingtieme, puis le cycle repart.</description></item>
    ///   <item><term>la fin</term><description>passe <c>0x8b</c>, extinction, le script de la
    ///     victime (<see cref="RunEntityScript"/>), <see cref="UnfreezeAllEntities"/>, le bit
    ///     <c>0x2004000</c> sur elle, et le fond rendu.</description></item>
    /// </list>
    /// <para>
    /// <b>Le meme defaut de 1998 que ses cousines</b> : sans victime, le clip d'animation reste le
    /// pointeur d'entree de la fonction — Ghidra le rend par <c>puVar9 = param_1</c> —, et la borne
    /// de l'animation se lit alors dans le <c>+0x16</c> de la scene elle-meme. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00437DD0(byte* e)
    {
        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        byte* victim = null;
        byte* clip = e;                                             // Ghidra: `puVar9 = param_1`

        byte* scene = Mem.Entity(0x005ecaa0);
        for (int i = 0x20; i != 0; i--, scene += 0x9c)
        {
            if (scene[0x0c] != 0) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);
            *(short*)(scene + 0x18) = 2;
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 8);
            scene[0x36] |= 2;
            *(uint*)scene |= 0x1000;
            victim = scene;
        }

        if (victim == null) victim = Res.s_EntityList_005de558;     // Ghidra: &DAT_005de558

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);
            SetSceneBackdrop(0xfa, unchecked((short)0xe6), unchecked((short)0xd2), 2);   // Ghidra: DAT_005e9160..66

            *(short*)(victim + 0x1a) = 0;
            victim[0x0a] = 0;
            *(uint*)(victim + 0x38) = 0xffffff;
            *(short*)(victim + 0x36) = 1;
            *(short*)(e + 0x98) = 0;

            Aud.PlaySoundEffect(0x16, 300, 0x1300, 0x1300);         // Ghidra: FUN_0053cea0
        }

        if (*(short*)(e + 0x16) == 4) Aud.PlaySoundEffect(0x2d, 0x80, 0x1200, 0x1200);

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;

            if ((*(ushort*)(e + 0x12) & 1) != 0 && *(short*)(e + 0x16) < 0x6e)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf4, 0, -1);   // Ghidra: FUN_0047ac40
            }

            if (*(short*)(e + 0x16) < 100 && SignedMask(CrtRand(), 1) == 0)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf5, 0, -1);
            }

            if ((e[0x16] & 1) == 0)
            {
                *(short*)(victim + 0x1a) += 1;
                if (*(short*)(victim + 0x1a) >= *(ushort*)(clip + 0x16))
                {
                    *(ushort*)(victim + 0x1a) = (ushort)(*(ushort*)(clip + 0x16) - 1);
                }
            }
        }

        if (*(short*)(e + 0x16) > 0x14)
        {
            short red = (short)(victim[0x38] - 2);
            byte green = (byte)(victim[0x39] - 2);
            byte blue = (byte)(victim[0x3a] - 2);

            if (red < 0)
            {
                red = 0;
                green = 0;
                blue = 0;
            }

            victim[0x38] = (byte)red;
            victim[0x39] = green;
            victim[0x3a] = blue;
        }

        *(short*)(victim + 0x2a) -= 0x14;
        *(short*)(victim + 0x28) += 10;
        *(short*)(victim + 0x2c) += 10;
        if (*(short*)(victim + 0x2a) < 0) *(short*)(victim + 0x2a) = 0;

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 0x13) Aud.PlaySoundEffect(0x23, 800, 0x1000, 0x1000);

        if (*(short*)(e + 0x98) == 0x14)
        {
            Aud.PlaySoundEffect(0x0f, 400, 0x1000, 0x1000);
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x16) > 0x8b)
        {
            *(uint*)e &= 0x7fffffff;
            RunEntityScript(s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + victim[9] * 0x9c, 0);   // Ghidra: FUN_00513dd0
            UnfreezeAllEntities();                                  // Ghidra: FUN_00535400
            *(uint*)victim |= 0x2004000;

            SetSceneBackdrop(
                (short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
        }

        *(short*)(e + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00437790 @ 0x00437790 — sous-zones 4 et 9, table <c>0x0056b0c8</c>. Decompilee
    /// dans <c>Blaze.c</c> ; 217 instructions. <b>La sœur de la precedente</b> : meme ossature, meme
    /// sortie, mais cent dix images et un tout autre deroule.
    ///
    /// <para>
    /// <b>La mort par combustion.</b> La victime — meme choix, la derniere entite de scene de
    /// <c>+0x0c</c> nul — est remise a blanc, pose 2, et brule : un <c>0xf6</c> par image, un
    /// crepitement <c>0x0b</c> une fois sur trois ou au plus toutes les vingt images, et son
    /// animation qui avance d'un cran toutes les quatre images, bornee sur la derniere.
    /// </para>
    /// <para>
    /// <b>A la cinquantieme image, l'eclat.</b> Deux incantations <c>0xc2</c> partent sur les canaux
    /// <c>+0x14</c> 7 puis 1 — la valeur d'origine rendue ensuite —, la victime prend le bit
    /// <c>0x4000</c> et passe au noir. Les six images suivantes, trois <c>0xf7</c> chacune et le
    /// fond force au blanc <c>(0xfa, 0xfa, 0xfa)</c> ; trois claquements <c>0x2e</c> de plus en plus
    /// aigus aux images 50, 51 et 52. A la <c>0x55</c>e, le fond retenu est rendu.
    /// </para>
    /// <para>
    /// <b>Elle ne repeint pas le fond a l'ouverture</b>, contrairement a sa sœur : elle ne fait que
    /// le retenir, et ne le change qu'au moment de l'eclat.
    /// </para>
    /// </summary>
    private static void EntityHandler_00437790(byte* e)
    {
        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        byte* victim = null;
        byte* clip = e;                                             // Ghidra: `puVar4 = param_1`

        byte* scene = Mem.Entity(0x005ecaa0);
        for (int i = 0x20; i != 0; i--, scene += 0x9c)
        {
            if ((sbyte)scene[0x0c] != 0) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);
            *(short*)(scene + 0x18) = 2;
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 8);
            *(uint*)scene |= 0x1000;
            victim = scene;
        }

        if (victim == null) victim = Res.s_EntityList_005de558;     // Ghidra: &DAT_005de558

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            *(short*)(victim + 0x1a) = 0;
            victim[0x0a] = 0;
            *(uint*)(victim + 0x38) = 0xffffff;
            *(short*)(victim + 0x36) = 1;

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x98) += 1;

            if ((short)(CrtRand() % 3) == 1 || *(short*)(e + 0x98) > 0x13)
            {
                *(short*)(e + 0x98) = 0;
                Aud.PlaySoundEffect(0x0b, 1000, 0x800, 0x800);      // Ghidra: FUN_0053cea0
            }

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf6, 0, -1);   // Ghidra: FUN_0047ac40

            *(short*)(e + 0x14) += 1;
            if (*(short*)(e + 0x14) > 3)
            {
                *(short*)(e + 0x14) = 0;
                *(short*)(victim + 0x1a) += 1;
                if (*(short*)(victim + 0x1a) >= *(ushort*)(clip + 0x16))
                {
                    *(ushort*)(victim + 0x1a) = (ushort)(*(ushort*)(clip + 0x16) - 1);
                }
            }
        }

        if (*(short*)(e + 0x16) == 0x32)
        {
            int channel = *(int*)(e + 0x14);
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x14) = 7;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);               // Ghidra: FUN_0047aae0
            *(short*)(e + 0x14) = 1;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);
            *(short*)(e + 0x14) = (short)channel;

            *(uint*)victim |= 0x4000;
            victim[0x38] = 0;
            victim[0x39] = 0;
            victim[0x3a] = 0;
        }

        if (*(short*)(e + 0x16) > 0x31 && *(short*)(e + 0x16) < 0x38)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf7, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf7, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf7, 0, -1);
            SetSceneBackdrop(0xfa, 0xfa, 0xfa, 2);                  // Ghidra: DAT_005e9160..66
        }

        if (*(short*)(e + 0x16) == 0x32) Aud.PlaySoundEffect(0x2e, 0x1bf, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x33) Aud.PlaySoundEffect(0x2e, 0x1e9, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x34) Aud.PlaySoundEffect(0x2e, 0x200, 0x1000, 0x1000);

        if (*(short*)(e + 0x16) == 0x55)
        {
            SetSceneBackdrop(
                (short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
        }

        if (*(short*)(e + 0x16) > 0x6d)
        {
            *(uint*)e &= 0x7fffffff;
            RunEntityScript(s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + victim[9] * 0x9c, 0);   // Ghidra: FUN_00513dd0
            *(uint*)victim |= 0x2004000;
            UnfreezeAllEntities();                                  // Ghidra: FUN_00535400
        }

        *(short*)(e + 0x16) += 1;
    }
}
