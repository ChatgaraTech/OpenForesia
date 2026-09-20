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
    private static void EntityHandler_00437790(byte* e) => TickBurningDeath(e, marksVictim: true, releasesCamera: false, guardsScriptOnSubArea: false);

    /// <summary>Le corps commun de <see cref="EntityHandler_00437790"/> (sous-zones 4 et 9) et de
    /// <see cref="EntityHandler_0043DB30"/> (11 et 12). Trois ecarts, tous portes par les
    /// parametres.</summary>
    private static void TickBurningDeath(byte* e, bool marksVictim, bool releasesCamera, bool guardsScriptOnSubArea)
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

            if (marksVictim) *(uint*)victim |= 0x4000;
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
            if (releasesCamera) CameraModeApply_005037E0(0);        // Ghidra: FUN_005037b0

            if (!guardsScriptOnSubArea || (s_Unknown_005de52c & 0xff) == 0x10)
            {
                RunEntityScript(s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + victim[9] * 0x9c, 0);   // Ghidra: FUN_00513dd0
            }

            *(uint*)victim |= 0x2004000;
            UnfreezeAllEntities();                                  // Ghidra: FUN_00535400
        }

        *(short*)(e + 0x16) += 1;
    }

    // =====================================================================
    // LOT 9 (2026-09-20) — CINQ MORTS DE PLUS, SALLES 11-12 ET 13-14-15
    //
    // Toutes de la meme famille que les deux du lot 8. Le balayage, le repli sur 0x005de558 et la
    // sortie sont communs ; ce qui change est la mise en scene.
    // =====================================================================

    /// <summary>Le balayage commun aux sept scenes : les trente-deux entites de
    /// <c>0x005ecaa0</c>, on retient <b>la derniere</b> dont l'octet <c>+0x0c</c> est nul et qui
    /// vit, on lui recopie sa position, on la met en pose 2 et on lui pose <c>0x1000</c>.
    ///
    /// <para><paramref name="clip"/> sort le bloc d'animation de la victime ; <b>sans victime il
    /// reste le pointeur d'entree de la fonction</b>, ce que le binaire fait aussi — la borne se lit
    /// alors dans le <c>+0x16</c> de la scene. <paramref name="marksSprite"/> ajoute le bit 2 a
    /// l'octet <c>+0x36</c>, que deux des sept seulement posent.</para></summary>
    private static byte* PickSceneVictim(byte* e, out byte* clip, bool marksSprite)
    {
        byte* victim = null;
        clip = e;

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
            if (marksSprite) scene[0x36] |= 2;
            *(uint*)scene |= 0x1000;
            victim = scene;
        }

        return victim != null ? victim : Res.s_EntityList_005de558;   // Ghidra: &DAT_005de558
    }

    /// <summary>L'ouverture commune : les trois shorts de fond retenus dans
    /// <c>+0x50</c>..<c>+0x58</c>, la victime remise a blanc, pose 0, image 0, <c>+0x36</c> a 1.
    /// </summary>
    private static void OpenDeathScene(byte* e, byte* victim)
    {
        *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
        *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
        *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);

        *(short*)(victim + 0x1a) = 0;
        victim[0x0a] = 0;
        *(uint*)(victim + 0x38) = 0xffffff;
        *(short*)(victim + 0x36) = 1;
    }

    /// <summary>Le fond retenu, rendu tel quel, mode 2.</summary>
    private static void RestoreSceneBackdrop(byte* e)
        => SetSceneBackdrop(
            (short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);

    /// <summary>Un cran d'animation de la victime, borne sur la derniere image du clip. Les sept
    /// scenes le font, a la cadence pres.</summary>
    private static void StepVictimClip(byte* victim, byte* clip)
    {
        *(short*)(victim + 0x1a) += 1;
        if (*(short*)(victim + 0x1a) >= *(ushort*)(clip + 0x16))
        {
            *(ushort*)(victim + 0x1a) = (ushort)(*(ushort*)(clip + 0x16) - 1);
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0043db30</c> — sous-zones 11 et 12. Decompilee dans <c>Blaze.c</c> ;
    /// 220 instructions. <b>La combustion du lot 8</b> (<see cref="EntityHandler_00437790"/>) a
    /// trois ecarts : elle ne pose pas le bit <c>0x4000</c> sur la victime, elle <b>rend la
    /// camera</b> a la fin, et son appel au script de la victime est <b>garde par la sous-zone</b> —
    /// il ne part que dans la seizieme.
    /// </summary>
    private static void EntityHandler_0043DB30(byte* e)
        => TickBurningDeath(e, marksVictim: false, releasesCamera: true, guardsScriptOnSubArea: true);

    /// <summary>
    /// Ghidra: FUN_0043c900 @ 0x0043c900 — sous-zones 11 et 12. Decompilee dans <c>Blaze.c</c> ;
    /// 204 instructions.
    ///
    /// <para>
    /// <b>La mort par le froid.</b> Cent vingt images. Pendant les soixante premieres, un
    /// <c>0xc4</c> par image et deux souffles <c>0x20</c> en direct : l'un toutes les six images
    /// (compteur <c>+0x98</c>), l'autre toutes les vingt et une (compteur <c>+0x5c</c>) et plus
    /// grave — deux cadences qui se croisent sans jamais coincider.
    /// </para>
    /// <para>
    /// La victime s'assombrit de <b>1 par canal et par image</b>, sans plancher : quand l'octet
    /// passe sous zero il boucle, et le binaire ne s'en soucie pas. Son animation avance une image
    /// sur deux.
    /// </para>
    /// <para>
    /// <b>A la soixante-dixieme image, la rupture</b> : l'incantation <c>0xc9</c>, le bit
    /// <c>0x4000</c> sur la victime, et quatre claquements a <c>0x46</c>, <c>0x47</c>, <c>0x48</c>
    /// et <c>0x4b</c>. Passe <c>0x77</c>, la camera est rendue, le script de la victime part, le bit
    /// <c>0x2004000</c> est pose et le fond retenu revient.
    /// </para>
    /// <para>
    /// <b>Elle ne rend pas la main aux entites</b> : c'est la seule des sept qui n'appelle pas
    /// <see cref="UnfreezeAllEntities"/>. Releve tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_0043C900(byte* e)
    {
        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        byte* victim = PickSceneVictim(e, out byte* clip, marksSprite: true);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            OpenDeathScene(e, victim);
            *(short*)(e + 0x98) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x16) < 0x3c)
            {
                if (*(short*)(e + 0x98) == 0) Aud.PlaySoundEffect(0x20, 0x800, 0x1200, 0x1200);   // Ghidra: FUN_0053cea0
                if (*(int*)(e + 0x5c) == 0) Aud.PlaySoundEffect(0x20, 800, 0x1000, 0x1000);

                *(short*)(e + 0x98) += 1;
                int slow = *(int*)(e + 0x5c);
                *(int*)(e + 0x5c) = slow + 1;

                if (*(short*)(e + 0x98) == 6) *(short*)(e + 0x98) = 0;
                if (slow + 1 == 0x15) *(int*)(e + 0x5c) = 0;

                if (*(short*)(e + 0x16) < 0x3c)
                {
                    SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc4, 0, -1);   // Ghidra: FUN_0047ac40
                }
            }

            if ((e[0x16] & 2) == 0) StepVictimClip(victim, clip);

            victim[0x38] -= 1;
            victim[0x39] -= 1;
            victim[0x3a] -= 1;
        }

        if (*(short*)(e + 0x16) == 0x46)
        {
            SpawnCastEntity_0047AAE0(e, 0xc9, 0, -1);               // Ghidra: FUN_0047aae0
            *(short*)(e + 0x10) = 2;
            *(uint*)victim |= 0x4000;
            Aud.PlaySoundEffect(0x2e, 0x200, 0x1000, 0x1000);
        }

        if (*(short*)(e + 0x16) == 0x47) Aud.PlaySoundEffect(0x22, 0x400, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x48) Aud.PlaySoundEffect(0x22, 0x6fd, 0x800, 0x800);
        if (*(short*)(e + 0x16) == 0x4b) Aud.PlaySoundEffect(0x2e, 0x2aa, 0x1200, 0x1200);

        *(short*)(e + 0x16) += 1;
        if (*(short*)(e + 0x16) <= 0x77) return;

        *(uint*)e &= 0x7fffffff;
        CameraModeApply_005037E0(0);                                // Ghidra: FUN_005037b0
        RunEntityScript(s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + victim[9] * 0x9c, 0);   // Ghidra: FUN_00513dd0
        *(uint*)victim |= 0x2004000;
        RestoreSceneBackdrop(e);
    }

    /// <summary>
    /// Ghidra: FUN_0043d4d0 @ 0x0043d4d0 — sous-zones 11 et 12. Decompilee dans <c>Blaze.c</c> ;
    /// 246 instructions.
    ///
    /// <para>
    /// <b>La mort par le cri.</b> Deux cents images, et un fond <b>blanc</b> des la premiere —
    /// c'est la seule des sept a repeindre avant de retenir l'ancien. L'incantation <c>0xc1</c>
    /// part aussi a la premiere image.
    /// </para>
    /// <para>
    /// La victime s'anime une image sur deux. <b>Passe la cinquantieme</b>, elle s'etire vers le
    /// haut de 100 par image jusqu'a <c>0x7530</c> et s'assombrit de 8 par canal. <b>Passe la
    /// soixantieme</b>, elle retrecit en largeur de 100 par image ; a zero, le fond retenu revient.
    /// </para>
    /// <para>
    /// <b>La partition.</b> Le son <c>0x16</c> a 0, <c>0x14</c>, <c>0x28</c>, <c>0x3c</c> et
    /// <c>0x50</c> ; le <c>0x0f</c> une image plus tard a chaque fois ; et la voix <c>0x20</c>
    /// monte de ton a <c>3</c>, <c>0x17</c>, <c>0x2b</c>, <c>0x3f</c>, <c>0x49</c> et <c>0x53</c>,
    /// avec deux <c>0x0a</c> a <c>0x40</c> et <c>0x41</c>. Passe 199 images, la camera est rendue et
    /// le script de la victime part.
    /// </para>
    /// </summary>
    private static void EntityHandler_0043D4D0(byte* e)
    {
        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        byte* victim = PickSceneVictim(e, out byte* clip, marksSprite: true);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = 0;
            OpenDeathScene(e, victim);
            *(short*)(e + 0x14) = 0;
            SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);               // Ghidra: FUN_0047aae0
            SetSceneBackdrop(0xfa, 0xfa, 0xfa, 2);                  // Ghidra: DAT_005e9160..66
        }

        if ((e[0x16] & 1) == 0) StepVictimClip(victim, clip);

        if (*(short*)(e + 0x16) > 0x32)
        {
            *(short*)(victim + 0x2a) += 100;
            if (*(short*)(victim + 0x2a) > 30000) *(short*)(victim + 0x2a) = 30000;
            BrightenEntityTint_00404DE0(victim, -8, -8, -8);        // Ghidra: FUN_00404de0
        }

        if (*(short*)(e + 0x16) > 0x3c)
        {
            *(short*)(victim + 0x28) -= 100;
            *(short*)(victim + 0x2c) -= 100;

            if (*(short*)(victim + 0x28) < 0)
            {
                *(short*)(victim + 0x28) = 0;
                *(short*)(victim + 0x2c) = 0;
                RestoreSceneBackdrop(e);
            }
        }

        short beat = *(short*)(e + 0x16);

        if (beat == 0 || beat == 0x14 || beat == 0x28 || beat == 0x3c || beat == 0x50)
        {
            Aud.PlaySoundEffect(0x16, 0x200, 0x1000, 0x1000);       // Ghidra: FUN_0053cea0
        }

        if (beat == 1 || beat == 0x15 || beat == 0x29 || beat == 0x3d || beat == 0x51)
        {
            Aud.PlaySoundEffect(0x0f, 0x200, 0x1000, 0x1000);
        }

        if (beat == 2) Aud.PlaySoundEffect(0x11, 0xfa, 0x1000, 0x1000);
        if (beat == 3) Aud.PlaySoundEffect(0x20, 0x400, 0x1000, 0x1000);
        if (beat == 0x17) Aud.PlaySoundEffect(0x20, 0x4aa, 0x1000, 0x1000);
        if (beat == 0x2b) Aud.PlaySoundEffect(0x20, 0x554, 0x1000, 0x1000);
        if (beat == 0x3f) Aud.PlaySoundEffect(0x20, 0x5fe, 0x1000, 0x1000);
        if (beat == 0x49) Aud.PlaySoundEffect(0x20, 0x400, 0x1000, 0x1000);
        if (beat == 0x53) Aud.PlaySoundEffect(0x20, 0x7a7, 0x1000, 0x1000);
        if (beat == 0x40) Aud.PlaySoundEffect(10, 800, 0x1000, 0x1000);
        if (beat == 0x41) Aud.PlaySoundEffect(10, 0x554, 0x1000, 0x1000);

        *(short*)(e + 0x16) += 1;
        if (*(short*)(e + 0x16) <= 199) return;

        *(uint*)e &= 0x7fffffff;
        CameraModeApply_005037E0(0);                                // Ghidra: FUN_005037b0
        RunEntityScript(s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + victim[9] * 0x9c, 0);   // Ghidra: FUN_00513dd0
        *(uint*)victim |= 0x2004000;
    }

    /// <summary>
    /// Ghidra: <c>0x0043cee0</c> — sous-zones 11 et 12. <b>Absente de <c>Blaze.c</c></b> ;
    /// 196 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_0043CEE0.asm</c>.
    ///
    /// <para>
    /// <b>La mort par la foudre.</b> Cent quarante images sous un ciel qui <b>clignote</b> : le fond
    /// est repeint a <b>chaque image</b> en <c>(0x96, 0x96, r)</c>, le bleu tire entre <c>0xb4</c>
    /// et <c>0xef</c>, <b>mode 1</b>. Passe la centieme image, le fond retenu revient.
    /// </para>
    /// <para>
    /// <b>Un craquement irregulier</b> : a partir de la sixieme image du cycle, soit un tirage sur
    /// dix tombe juste, soit vingt images sont passees — alors la voix <c>0x20</c> part a une
    /// hauteur tiree entre <c>0x200</c> et <c>0x32b</c>, et le cycle repart.
    /// </para>
    /// <para>
    /// <b>La decharge.</b> Deux compteurs montent ensemble, <c>+0x12</c> et <c>+0x16</c>. Quand
    /// <c>+0x12</c> atteint 1 et que <c>+0x16</c> est sous 80, <c>+0x14</c> avance d'un cran,
    /// <c>+0x12</c> repart a zero, et <b>un <c>0xfc</c> nait</b> — suivi d'un <c>0xfd</c> une image
    /// sur deux tant que <c>+0x16</c> reste sous 70. Le tirage qui precede le <c>0xfc</c> est range
    /// dans la pile et jamais relu : Ghidra le montre, le desassemblage le confirme.
    /// </para>
    /// <para>
    /// La victime s'anime une image sur deux et s'assombrit de <c>2 / 4 / 4</c> a chaque image.
    /// Passe la cent-quarantieme, la camera est rendue, le script part et le bit <c>0x2004000</c>
    /// est pose.
    /// </para>
    /// </summary>
    private static void EntityHandler_0043CEE0(byte* e)
    {
        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        byte* victim = PickSceneVictim(e, out byte* clip, marksSprite: true);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            OpenDeathScene(e, victim);
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x98) += 1;

        if (*(short*)(e + 0x98) > 5)
        {
            bool crack = (short)(CrtRand() % 10) == 1 || *(short*)(e + 0x98) > 0x14;
            if (crack)
            {
                *(short*)(e + 0x98) = 0;
                Aud.PlaySoundEffect(0x20, CrtRand() % 0x12c + 0x200, 0x1000, 0x1000);   // Ghidra: FUN_0053cea0
            }
        }

        // Ghidra: DAT_005e9160..66 — le ciel, repeint a chaque image, mode 1.
        SetSceneBackdrop(0x96, 0x96, (short)(CrtRand() % 0x3c + 0xb4), 1);

        *(short*)(e + 0x12) += 1;
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x12) >= 1 && *(short*)(e + 0x16) < 0x50)
        {
            *(short*)(e + 0x14) += 1;
            *(short*)(e + 0x12) = 0;

            _ = SignedMask(CrtRand(), 3);                           // range dans la pile, jamais relu
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfc, 0, -1);   // Ghidra: FUN_0047ac40

            if ((e[0x16] & 1) == 0 && *(short*)(e + 0x16) < 0x46)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfd, 0, -1);
            }
        }

        if (*(short*)(e + 0x16) > 0x64) RestoreSceneBackdrop(e);

        if (*(short*)(e + 0x16) >= 0x8c)
        {
            *(uint*)e &= 0x7fffffff;
            CameraModeApply_005037E0(0);                            // Ghidra: FUN_005037b0
            RunEntityScript(s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + victim[9] * 0x9c, 0);   // Ghidra: FUN_00513dd0
            *(uint*)victim |= 0x2004000;
        }

        if ((e[0x16] & 1) == 0) StepVictimClip(victim, clip);

        BrightenEntityTint_00404DE0(victim, -2, -4, -4);            // Ghidra: FUN_00404de0
    }

    /// <summary>
    /// Ghidra: <c>0x00441eb0</c> — sous-zones 13, 14 et 15. <b>Absente de <c>Blaze.c</c></b> ;
    /// 207 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_00441EB0.asm</c>.
    ///
    /// <para>
    /// <b>La mort dans la lumiere.</b> Cent quarante images sous un fond <b>blanc</b>, repeint a
    /// chaque image. La victime perd 2 par canal et par image jusqu'au noir, s'anime une image sur
    /// deux, et un <c>0xf9</c> nait a chaque image pendant les soixante premieres.
    /// </para>
    /// <para>
    /// Deux incantations ponctuent : <c>0xc3</c> a la septieme image du compteur <c>+0x14</c> — qui
    /// repart alors a zero — si la scene a moins de soixante-dix images, et <c>0xc4</c> a la
    /// vingtieme de <c>+0x12</c>. La partition est <c>0x11</c> a <c>0x14</c> et <c>0x15</c>,
    /// <c>0x16</c> a <c>0x16</c> et <c>0x17</c>, puis <c>0x2d</c> toutes les cinq images entre la
    /// vingt-sixieme et la centieme.
    /// </para>
    /// <para>
    /// <b>Son tampon de secours est sur la pile</b>, pas <c>0x005de558</c> : 0xa0 octets mis a zero
    /// a l'entree, dont <c>edi</c> garde l'adresse quand le balayage ne trouve personne. Et son
    /// appel au script passe <b>zero</b> en victime, la ou les six autres passent l'emplacement du
    /// groupe — releve tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00441EB0(byte* e)
    {
        byte* fallback = stackalloc byte[0xa0];
        for (int i = 0; i < 0xa0; i++) fallback[i] = 0;

        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        byte* victim = fallback;
        byte* clip = e;

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

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            OpenDeathScene(e, victim);
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 1)
        {
            *(short*)(e + 0x16) += 1;
            return;
        }

        short red = (short)(victim[0x38] - 2);
        short green = (short)(victim[0x39] - 2);
        short blue = (short)(victim[0x3a] - 2);

        if (red < 0)
        {
            red = 0;
            green = 0;
            blue = 0;
        }

        victim[0x38] = (byte)red;
        victim[0x39] = (byte)green;
        victim[0x3a] = (byte)blue;

        SetSceneBackdrop(0xfa, 0xfa, 0xfa, 2);                      // Ghidra: DAT_005e9160..66

        *(short*)(e + 0x12) += 1;
        *(short*)(e + 0x14) += 1;

        if (*(short*)(e + 0x16) < 0x3c)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf9, 0, -1);   // Ghidra: FUN_0047ac40
        }

        if (*(short*)(e + 0x14) == 7 && *(short*)(e + 0x16) < 0x46)
        {
            *(short*)(e + 0x14) = 0;
            SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);               // Ghidra: FUN_0047aae0
        }

        if (*(short*)(e + 0x12) == 0x14)
        {
            SpawnCastEntity_0047AAE0(e, 0xc4, 0, -1);
            Aud.PlaySoundEffect(0x11, 0xfa, 0x1000, 0x1000);        // Ghidra: FUN_0053cea0
        }

        if (*(short*)(e + 0x12) == 0x15) Aud.PlaySoundEffect(0x11, 0x320, 0x1000, 0x1000);
        if (*(short*)(e + 0x12) == 0x16) Aud.PlaySoundEffect(0x16, 0x7d, 0x1000, 0x1000);
        if (*(short*)(e + 0x12) == 0x17) Aud.PlaySoundEffect(0x16, 0x190, 0x1000, 0x1000);

        if (*(short*)(e + 0x12) > 0x19 && *(short*)(e + 0x12) < 0x64)
        {
            *(short*)(e + 0x98) += 1;
            if (*(short*)(e + 0x98) == 5)
            {
                Aud.PlaySoundEffect(0x2d, 0x200, 0x1000, 0x1000);
                *(short*)(e + 0x98) = 0;
            }
        }

        if ((e[0x12] & 1) == 0) StepVictimClip(victim, clip);

        if (*(short*)(e + 0x12) >= 0x8c)
        {
            CameraModeApply_005037E0(0);                            // Ghidra: FUN_005037b0
            *(uint*)e &= 0x7fffffff;
            RunEntityScript(s_Unknown_005e97cc, null, 0);           // Ghidra: FUN_00513dd0(dir, 0, 0)
            *(uint*)victim |= 0x2004000;
            RestoreSceneBackdrop(e);
        }

        *(short*)(e + 0x16) += 1;
    }

    /// <summary>Ghidra: la gamme de <see cref="EntityHandler_004417F0"/>, trente hauteurs baties
    /// sur la pile a chaque image — un demi-ton a la fois de <c>0x100</c> a <c>0x1e9</c>, puis un
    /// ton de <c>0x200</c> a <c>0x3d3</c>, puis une quarte de <c>0x400</c> a <c>0x5a9</c>.</summary>
    private static readonly short[] s_ScreamLadder_004417F0 =
    [
        0x100, 0x115, 0x12a, 0x13f, 0x155, 0x16a, 0x17f, 0x195,
        0x1aa, 0x1bf, 0x1d4, 0x1e9, 0x200, 0x22a, 0x255, 0x27f,
        0x2aa, 0x2d4, 0x2ff, 0x329, 0x354, 0x37e, 0x3a9, 0x3d3,
        0x400, 0x455, 0x4aa, 0x4ff, 0x554, 0x5a9,
    ];

    /// <summary>
    /// Ghidra: <c>0x004417f0</c> — sous-zones 13, 14 et 15. <b>Absente de <c>Blaze.c</c></b> ;
    /// 237 instructions, <c>Legacy/Raw/Blaze/SealedCave/FUN_004417F0.asm</c>.
    ///
    /// <para>
    /// <b>La mort qui chante.</b> Deux cent quarante images. L'incantation <c>0xc6</c> part a la
    /// premiere, <c>0xc7</c> revient a chaque image jusqu'a la cent-cinquantieme, et <c>0xc8</c>
    /// prend le relais entre la cent-cinquantieme et la cent-soixante-dixieme, <b>une image sur
    /// deux</b>.
    /// </para>
    /// <para>
    /// <b>La gamme.</b> Aux images impaires, deux regimes : tant que <c>+0x98</c> reste sous 50, il
    /// sert d'indice dans <see cref="s_ScreamLadder_004417F0"/> et la voix <c>0x31</c> monte la
    /// gamme, un degre par image impaire ; arrive a 30, <c>+0x98</c> saute a 100 et la voix tient le
    /// dernier degre (<c>0x5a9</c>) jusqu'a 139. Deux <c>0x2d</c> ouvrent la scene, et deux
    /// <c>0x25</c> la ferment a <c>0x96</c> et <c>0x97</c>.
    /// </para>
    /// <para>
    /// <b>La victime fond</b> : ses trois echelles perdent <c>0x1e</c> par image et sa hauteur monte
    /// de 100 ; a plat, les trois restent a zero. Elle s'anime une image sur deux. Passe la
    /// deux-cent-quarantieme, la camera est rendue et le script part.
    /// </para>
    /// </summary>
    private static void EntityHandler_004417F0(byte* e)
    {
        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        byte* victim = PickSceneVictim(e, out byte* clip, marksSprite: true);

        if (*(short*)(e + 0x10) == 0)
        {
            SpawnCastEntity_0047AAE0(e, 0xc6, 0, -1);               // Ghidra: FUN_0047aae0
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            *(short*)(victim + 0x1a) = 0;
            victim[0x0a] = 0;
            *(uint*)(victim + 0x38) = 0xffffff;
            *(short*)(victim + 0x36) = 1;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x2d, 0x7d, 0x1000, 0x1000);   // Ghidra: FUN_0053cea0
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x2d, 0xc8, 0x1000, 0x1000);

        if ((e[0x16] & 1) != 0
            && *(short*)(e + 0x98) >= 0x64 && *(short*)(e + 0x98) <= 0x8b)
        {
            Aud.PlaySoundEffect(0x31, 0x5a9, 0x1000, 0x1000);
            *(short*)(e + 0x98) += 1;
        }

        if ((e[0x16] & 1) != 0 && *(short*)(e + 0x16) > 3 && *(short*)(e + 0x98) < 0x32)
        {
            Aud.PlaySoundEffect(0x31, s_ScreamLadder_004417F0[*(short*)(e + 0x98)], 0x1000, 0x1000);
            *(short*)(e + 0x98) += 1;
            if (*(short*)(e + 0x98) == 0x1e) *(short*)(e + 0x98) = 0x64;
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) >= 0)
        {
            *(short*)(victim + 0x28) -= 0x1e;
            *(short*)(victim + 0x2a) -= 0x1e;
            *(short*)(victim + 0x2c) -= 0x1e;
            *(int*)(victim + 0x20) += 0x64;

            if (*(short*)(victim + 0x28) < 0)
            {
                *(short*)(victim + 0x28) = 0;
                *(short*)(victim + 0x2a) = 0;
                *(short*)(victim + 0x2c) = 0;
            }
        }

        if (*(short*)(e + 0x16) <= 0x96) SpawnCastEntity_0047AAE0(e, 0xc7, 0, -1);

        if (*(short*)(e + 0x16) >= 0x96 && *(short*)(e + 0x16) <= 0xaa && (e[0x16] & 1) == 0)
        {
            SpawnCastEntity_0047AAE0(e, 0xc8, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0x96) Aud.PlaySoundEffect(0x25, 0x320, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x97) Aud.PlaySoundEffect(0x25, 0x400, 0x1000, 0x1000);

        if ((e[0x16] & 1) == 0) StepVictimClip(victim, clip);

        if (*(short*)(e + 0x16) < 0xf0) return;

        *(uint*)e &= 0x7fffffff;
        CameraModeApply_005037E0(0);                                // Ghidra: FUN_005037b0
        RunEntityScript(s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + victim[9] * 0x9c, 0);   // Ghidra: FUN_00513dd0
        *(uint*)victim |= 0x2004000;
    }
}
