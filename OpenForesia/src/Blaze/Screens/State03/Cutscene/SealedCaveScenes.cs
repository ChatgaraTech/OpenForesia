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
            // Ghidra ancre son pointeur sur 0x005ecadb, soit l'enregistrement + 0x3b : son
            // `*pbVar4 |= 2` se lit LA, pas au +0x36 du mot de sprite. Binaire, 00437e3x.
            scene[0x3b] |= 2;
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
    /// l'octet <c>+0x3b</c>, que deux des sept seulement posent.</para>
    ///
    /// <para><b>C'est bien <c>+0x3b</c> et non <c>+0x36</c></b>, qui est le mot de sprite
    /// qu'<see cref="OpenDeathScene"/> met a 1. Ghidra ancre son pointeur de balayage sur
    /// <c>0x005ecadb</c>, soit l'enregistrement <b>plus <c>0x3b</c></b>, et son <c>*pbVar |= 2</c>
    /// se lit donc a cet offset-la. Confirme dans le binaire a <c>0043c960</c> —
    /// <c>mov bl,[eax] ; or bl,2 ; mov [eax],bl</c> avec <c>eax = enregistrement + 0x3b</c>.
    /// </para></summary>
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
            if (marksSprite) scene[0x3b] |= 2;
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
            // `or dl,2 ; mov [eax],dl` a 00441f30, eax = enregistrement + 0x3b — pas le +0x36.
            scene[0x3b] |= 2;
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

    // =====================================================================
    // LOT 12 (2026-09-20) — LA HUITIEME SCENE : L'EMBRASEMENT DU FOND
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x0043fb80</c> — sous-zones 13, 14 et 15. Decompilee dans <c>Blaze.c</c> ;
    /// 359 instructions.
    ///
    /// <para>
    /// <b>La huitieme scene de la grotte, et la seule qui ne tue personne.</b> Elle reprend le
    /// balayage et l'ouverture des sept autres — <see cref="PickSceneVictim"/>,
    /// <see cref="OpenDeathScene"/> — mais ce qu'elle met en scene est le <b>fond lui-meme</b> :
    /// pendant cent quatre images elle peint les trois shorts <c>0x005e9160</c>..<c>0x005e9164</c>
    /// a la main, sans passer par <see cref="SetSceneBackdrop"/>, et les rend a la fin depuis
    /// <c>+0x50</c>..<c>+0x58</c>.
    /// </para>
    /// <list type="table">
    ///   <item><term>images 0..0x38</term><description>le fond <b>s'eteint</b> — les trois canaux
    ///     perdent 2 par image, mode 2.</description></item>
    ///   <item><term>images 0x3a..0x3c</term><description>il <b>s'embrase</b> : le bleu gagne
    ///     <c>0x3c</c>, le rouge et le vert <c>0x1e</c>, mode 1, et deux <c>0xca</c>
    ///     naissent.</description></item>
    ///   <item><term>image 0x3d</term><description>le coup : le son <c>0x0b</c> a <c>0x329</c>, la
    ///     secousse armee a <c>10</c>, et <see cref="SpawnCastEntity_0047AAE0"/> lance un
    ///     <c>0xca</c> de plus. L'image <c>0x3e</c> repond par le meme son, plus
    ///     grave.</description></item>
    ///   <item><term>au-dela de 0x41</term><description>le fond redescend, plus vite sur le bleu
    ///     (<c>-7</c>) que sur les deux autres (<c>-4</c>).</description></item>
    /// </list>
    /// <para>
    /// <b>Les vingt premieres images sonnent.</b> Le compteur <c>+0x98</c> tourne de <c>0</c> a
    /// <c>0x12</c> et les cinq premieres valeurs font claquer le son <b>0x20 du catalogue general</b> — un identifiant brut, pas un indice de zone — a des hauteurs
    /// croissantes — <c>0x200</c>, <c>0x200</c>, <c>0x329</c>, <c>0x400</c>, <c>0x653</c> — les
    /// quatorze autres passant en silence. C'est un roulement qui monte, puis se tait.
    /// </para>
    /// <para>
    /// <b>La gerbe est en deux temps</b> : huit <c>0xc8</c> a chacune des cinq premieres images,
    /// puis trois <c>0xc9</c> aux images <c>4</c> et <c>5</c>. Les deux chevauchent a l'image
    /// <c>4</c>, qui en voit donc onze.
    /// </para>
    /// <para>
    /// <b>La secousse est signee a l'envers.</b> <c>+0x12</c> descend vers zero, puis est
    /// <b>nie sur place</b> avant d'etre passe a
    /// <see cref="TryPublishCameraEffectNearEntity"/> : une secousse armee a <c>10</c> alterne donc
    /// de signe a chaque image tant qu'elle dure. Transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>La sortie appelle le script de la victime deux fois</b>, la seconde avec un argument tire
    /// de l'octet <c>0x005e96e2</c> — <c>1</c> quand il est nul. Les sept autres scenes ne
    /// l'appellent qu'une fois ; celle-ci double, et pose le bit <c>0x2004000</c> entre les deux
    /// comme apres.
    /// </para>
    /// </summary>
    private static void EntityHandler_0043FB80(byte* e)
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
            *(short*)(e + 0x98) = 0;
        }

        // ------------------------------------------------- le roulement qui monte
        short beat = *(short*)(e + 0x98);

        if (beat == 0) Aud.PlaySoundEffect(0x20, 0x200, 0x1200, 0x1200);   // Ghidra: FUN_0053cea0
        if (beat == 1) Aud.PlaySoundEffect(0x20, 0x200, 0x1200, 0x1200);
        if (beat == 2) Aud.PlaySoundEffect(0x20, 0x329, 0x1200, 0x1200);
        if (beat == 3) Aud.PlaySoundEffect(0x20, 0x400, 0x1200, 0x1200);
        if (beat == 4) Aud.PlaySoundEffect(0x20, 0x653, 0x1200, 0x1200);

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 0x13) *(short*)(e + 0x98) = 0;

        // ------------------------------------------------- la gerbe, en deux temps
        short age = *(short*)(e + 0x16);

        if (age < 5)
        {
            for (int i = 8; i != 0; i--)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 200, 0, -1);   // Ghidra: FUN_0047ac40
            }
        }

        if (age > 3 && age < 6)
        {
            for (int i = 3; i != 0; i--)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc9, 0, -1);
            }
        }

        // ------------------------------------------------- le fond, peint a la main
        short* fogR = (short*)Mem.Arena(0x005e9160);                // Ghidra: DAT_005e9160
        short* fogG = (short*)Mem.Arena(0x005e9162);                // Ghidra: DAT_005e9162
        short* fogB = (short*)Mem.Arena(0x005e9164);                // Ghidra: DAT_005e9164
        short* fogMode = (short*)Mem.Arena(0x005e9166);             // Ghidra: DAT_005e9166

        if (age < 0x39)
        {
            *fogMode = 2;
            *fogR -= 2;
            *fogG -= 2;
            *fogB -= 2;
        }

        if (age > 0x39 && age < 0x3d)
        {
            *fogB += 0x3c;
            *fogR += 0x1e;
            *fogG += 0x1e;
            *fogMode = 1;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xca, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xca, 0, -1);
        }

        if (age > 0x41)
        {
            *fogMode = 2;
            *fogR -= 4;
            *fogG -= 4;
            *fogB -= 7;
        }

        if (age == 0x3d)
        {
            *(short*)(e + 0x12) = 10;
            Aud.PlaySoundEffect(0x0b, 0x329, 0x1600, 0x1600);
            SpawnCastEntity_0047AAE0(e, 0xca, 0, -1);               // Ghidra: FUN_0047aae0
        }

        if (age == 0x3e) Aud.PlaySoundEffect(0x0b, 0x100, 0x1600, 0x1600);

        // ------------------------------------------------- la secousse, niee sur place
        if (*(short*)(e + 0x12) != 0)
        {
            if (*(short*)(e + 0x12) > 0) *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x12) += 1;

            short shake = *(short*)(e + 0x12);
            *(short*)(e + 0x12) = (short)-shake;

            TryPublishCameraEffectNearEntity((uint*)e, 999, -shake, 999, 0, 0);   // Ghidra: FUN_0041be60
            *(uint*)victim |= 0x4000;
        }

        if ((e[0x16] & 1) != 0) StepVictimClip(victim, clip);

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) <= 0x68) return;

        // ------------------------------------------------- la sortie
        *(uint*)e &= 0x7fffffff;
        CameraModeApply_005037E0(0);                                // Ghidra: FUN_005037b0

        byte* member = Inp.s_SlotFlags_005e93c0 + victim[9] * 0x9c;
        RunEntityScript(s_Unknown_005e97cc, member, 0);             // Ghidra: FUN_00513dd0
        *(uint*)victim |= 0x2004000;

        uint second = *(byte*)Mem.Arena(0x005e96e2) == 0 ? 1u : 0u;   // Ghidra: DAT_005e96e0._2_1_
        RunEntityScript(s_Unknown_005e97cc, member, second);
        *(uint*)victim |= 0x2004000;

        SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54),
                         (short)*(int*)(e + 0x58), 2);
    }

    /// <summary>
    /// Ghidra: <c>0x004424b0</c> — sous-zones 13, 14 et 15, table de debordement. <b>Absente de
    /// <c>Blaze.c</c></b> ; 272 instructions,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_004424B0.asm</c>.
    ///
    /// <para>
    /// <b>Vraisemblablement inatteignable</b> (releve du CD, 2026-09-26, <c>bestiaire --regions</c>).
    /// Elle est le sous-type <c>0xcb</c> du pool d'effets dans les salles 13 a 15, et rien ne l'y fait
    /// naitre : le script de chaque salle lance UNE scene par l'opcode 16 — <c>0xcf</c>
    /// (<see cref="EntityHandler_0043FB80"/>) en 13, <c>0xcc</c> (<see cref="EntityHandler_004417F0"/>)
    /// en 14, <c>0xd0</c> (<see cref="EntityHandler_00440840"/>, la fin du jeu) en 15 ; aucun des
    /// trois catalogues <c>+0xcc/+0xd0/+0xd4</c> ne contient <c>0xcb</c> ; aucune fiche de sort,
    /// aucun coffre (il n'y en a pas dans ces salles), et les coffres laches par les monstres ne
    /// tirent que <c>0xb4..0xb9</c>. Meme conclusion pour <c>0xca</c>,
    /// <see cref="EntityHandler_00441EB0"/>. Ses gerbes (<see cref="EntityHandler_00442870"/>) ne se
    /// voient donc pas dans le jeu d'origine.
    /// </para>
    /// <para>
    /// <b>La neuvieme scene : le membre aspire.</b> Meme ossature que les huit autres — les deux
    /// appels de tete, le balayage qui retient la derniere entite de scene vivante, l'ouverture
    /// commune — mais sa mise en scene tient en trois temps : un accord de trois notes, une
    /// <b>rafale de six ou douze projections</b>, et une sirene qui monte sur quarante-sept images
    /// pendant que la victime s'eteint.
    /// </para>
    /// <list type="table">
    ///   <item><term>les images 0, 1 et 2</term><description>trois sons du catalogue general :
    ///     <c>0x20</c> a <c>0x200</c>, <c>0x20</c> a <c>0x320</c>, <c>0x23</c> a
    ///     <c>0x200</c>.</description></item>
    ///   <item><term>la rafale</term><description>un compteur <c>+0x9a</c> monte ; sous <c>0x23</c>
    ///     rien, entre <c>0x23</c> et <c>0x2c</c> <b>une chance sur dix</b> par image, a partir de
    ///     <c>0x2d</c> c'est certain. Quand elle part : <c>+0x9a</c> saute a <c>0x64</c> — ce qui
    ///     ferme la porte des l'image suivante, la garde superieure etant <c>0x64</c> —,
    ///     <c>+0x14</c> passe a <c>4</c>, et <b>six</b> <c>0xc5</c> naissent, <b>douze</b> si
    ///     <c>+0x5c</c> vaut encore <c>2</c>. Puis <c>+0x5c</c> avance, et c'est lui qui compte les
    ///     temps.</description></item>
    ///   <item><term>la sirene</term><description>de <c>+0x98 = 2</c> a <c>0x30</c>, le son
    ///     <c>0x26</c> a chaque image, <b>alternant de hauteur</b> — <c>0xca6</c> aux valeurs
    ///     impaires, <c>0xdfa</c> aux paires — et au gain <c>0x1300 - (n &lt;&lt; 6)</c>, qui
    ///     decroit donc jusqu'a <c>0x100</c>. C'est un cri qui s'eloigne.</description></item>
    ///   <item><term>l'extinction</term><description>tant que <c>+0x5c</c> ne vaut pas <c>2</c>, la
    ///     victime perd 10 par canal ; au noir, elle prend le bit <c>0x4000</c>. Son animation
    ///     avance une image sur deux.</description></item>
    ///   <item><term>la fin</term><description>passe l'image <c>0x82</c> <b>et</b> <c>+0x5c</c> a
    ///     <c>3</c> : extinction, camera rendue, le script <c>0</c> joue <b>sans entite</b>, le bit
    ///     <c>0x2004000</c> sur la victime, et le fond rendu.</description></item>
    /// </list>
    /// <para>
    /// <b>Elle porte le defaut de 1998 sous sa pire forme.</b> Les huit autres scenes laissent le
    /// clip d'animation pointer sur leur propre enregistrement quand il n'y a pas de victime ;
    /// celle-ci le laisse pointer sur un <b>emplacement de pile jamais ecrit</b> — le
    /// <c>[esp+0x10]</c> que le balayage seul renseigne. Le tampon de secours, lui, est bien mis a
    /// zero (<c>rep stosd</c> sur <c>0x9c</c> octets). Transcrit tel quel : le tampon de secours
    /// sert de clip, ce qui donne une borne d'animation nulle au lieu d'une lecture hors des clous.
    /// </para>
    /// </summary>
    private static void EntityHandler_004424B0(byte* e)
    {
        byte* fallback = stackalloc byte[0x9c];
        for (int i = 0; i < 0x9c; i++) fallback[i] = 0;

        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        byte* victim = fallback;
        // Le binaire laisse [esp+0x10] non initialise ; on lui substitue le tampon de secours, qui
        // est le seul emplacement sur de la pile. Voir le paragraphe de la documentation.
        byte* clip = fallback;

        byte* scene = Mem.Entity(0x005ecaa0);
        for (int i = 0x20; i != 0; i--, scene += 0x9c)
        {
            if (scene[0x0c] != 0) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);
            *(short*)(scene + 0x18) = 2;
            scene[0x3b] |= 2;                                       // `or bl,2 ; mov [eax],bl` a 00442524
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 8);
            *(uint*)scene |= 0x1000;
            victim = scene;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = 0;
            OpenDeathScene(e, victim);
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = 2;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x20, 0x200, 0x1000, 0x1000);   // Ghidra: FUN_0053cea0
            if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x20, 0x320, 0x1000, 0x1000);
            if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x23, 0x200, 0x1000, 0x1000);

            int draw = CrtRand() % 10;
            *(short*)(e + 0x9a) += 1;
            short beat = *(short*)(e + 0x9a);

            // Sous 0x23 jamais, entre 0x23 et 0x2c une fois sur dix, a partir de 0x2d toujours.
            bool fires = beat >= 0x2d || (beat >= 0x23 && draw == 1);

            if (fires && beat <= 0x64)
            {
                *(short*)(e + 0x98) = 1;
                *(short*)(e + 0x9a) = 0x64;
                *(short*)(e + 0x14) = 4;

                for (int i = 6; i != 0; i--)
                {
                    SpawnCastEntity_0047AAE0(e, 0xc5, 0, -1);       // Ghidra: FUN_0047aae0
                }

                if (*(int*)(e + 0x5c) == 2)
                {
                    for (int i = 6; i != 0; i--) SpawnCastEntity_0047AAE0(e, 0xc5, 0, -1);
                }

                *(int*)(e + 0x5c) += 1;
            }
        }

        // ------------------------------------------------- la sirene qui s'eloigne
        short cry = *(short*)(e + 0x98);

        if (cry >= 2 && cry < 0x31)
        {
            short gain = (short)(0x1300 - (cry << 6));
            Aud.PlaySoundEffect(0x26, (cry & 1) != 0 ? 0xca6 : 0xdfa, gain, gain);
            *(short*)(e + 0x98) += 1;
        }

        if (*(short*)(e + 0x98) == 1) *(short*)(e + 0x98) = 2;

        // ------------------------------------------------- la victime s'eteint
        if (*(int*)(e + 0x5c) != 2)
        {
            short red = (short)(victim[0x38] - 10);
            short green = (short)(victim[0x39] - 10);
            short blue = (short)(victim[0x3a] - 10);

            if (red < 0)
            {
                red = 0;
                green = 0;
                blue = 0;
                *(uint*)victim |= 0x4000;                           // `or byte [edi+1],40h`
            }

            victim[0x38] = (byte)red;
            victim[0x39] = (byte)green;
            victim[0x3a] = (byte)blue;
        }

        if ((e[0x16] & 1) == 0) StepVictimClip(victim, clip);

        if (*(short*)(e + 0x16) > 0x82 && *(int*)(e + 0x5c) == 3)
        {
            *(uint*)e &= 0x7fffffff;
            CameraModeApply_005037E0(0);                            // Ghidra: FUN_005037b0
            RunEntityScript(s_Unknown_005e97cc, null, 0);           // Ghidra: FUN_00513dd0
            *(uint*)victim |= 0x2004000;
            RestoreSceneBackdrop(e);
        }

        *(short*)(e + 0x16) += 1;
    }

    // =====================================================================
    // LOT 13 (2026-09-20) — LA DIXIEME SCENE, ET LA DERNIERE DE LA ZONE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00440840</c> — sous-zones 13, 14 et 15, table <c>0x0056b0c8</c>. <b>Absente de
    /// <c>Blaze.c</c></b> ; 477 instructions, <b>le plus gros corps restant de la grotte</b>,
    /// <c>Legacy/Raw/Blaze/SealedCave/FUN_00440840.asm</c>.
    ///
    /// <para>
    /// <b>La scene finale.</b> Cinq cents images, et elle ne se termine pas comme les neuf autres :
    /// au lieu de rendre la main a la carte, elle <b>bascule l'etat de jeu</b> — <c>0x005de510</c> a
    /// <c>7</c>, <c>0x005645f8</c> a <c>0xfd</c>, <c>0x005de520</c> a <c>3</c> — apres avoir noyé
    /// l'ecran de blanc. C'est la sortie de la grotte scellee.
    /// </para>
    /// <para>
    /// <b>Son balayage de victime est le seul a indexer le clip par la pose.</b> Les neuf autres
    /// prennent <c>animTable[scene[4]][2]</c> ; celle-ci prend <c>animTable[scene[4]][pose]</c>, et
    /// la pose est le <c>+0x9a</c> de la scene, qui part de <c>0x0b</c> et monte. La victime joue
    /// donc une <b>suite</b> d'animations, pas une seule.
    /// </para>
    /// <list type="table">
    ///   <item><term>la suite d'animations</term><description>une image sur deux, le clip avance ;
    ///     a sa derniere image la pose monte, <c>0x0d</c> etant saute et <c>0x0f</c> ramene a
    ///     <c>0x0e</c>. La pose <c>0x0e</c> est donc un <b>point fixe</b> : la victime y reste,
    ///     bloquee sur sa derniere image.</description></item>
    ///   <item><term>l'arret du temps</term><description>tant que <c>+0x5c</c> est non nul,
    ///     <c>+0x16</c> <b>recule d'une image a chaque image impaire</b> — le compte de la scene
    ///     fait du sur-place. Il se remplit a <c>0x0a</c> quand la pose vaut <c>0x0e</c>, l'image
    ///     vaut <c>0x1b</c> et le compteur vient de passer a <c>-1</c> : une pause d'une dizaine
    ///     d'images au moment precis du geste.</description></item>
    ///   <item><term>les trois eclats</term><description>a la pose <c>0x0e</c>, une fois la
    ///     derniere image atteinte et tant que <c>+0x16</c> est sous <c>0xbe</c>, trois <c>0xc5</c>
    ///     naissent par image, avec deux sons <c>5</c> qui ne partent qu'une fois chacun (verrou
    ///     <c>+0x98</c>, <c>0</c> puis <c>1</c> puis <c>2</c>).</description></item>
    ///   <item><term>l'image 0xaf</term><description>le fond passe au <b>blanc plein</b> et le
    ///     semeur de <c>0xcb</c> s'arme : une naissance toutes les trois images jusqu'a
    ///     <c>0x12c</c>.</description></item>
    ///   <item><term>0xf0 a 0x118</term><description>un <c>0xc6</c> par image et le troisieme
    ///     son.</description></item>
    ///   <item><term>0x10e a 0x140</term><description>le grondement <c>0x28</c> une image sur sept,
    ///     un <c>0xcd</c> a l'entree, deux <c>0xcf</c> par image jusqu'a <c>0x12c</c>, et un
    ///     <c>0xce</c> <b>lance 0xe6000 plus haut</b> une image impaire sur deux jusqu'a
    ///     <c>0x118</c>.</description></item>
    ///   <item><term>0x118 et 0x11d</term><description>le fond passe au cyan sombre et deux
    ///     <c>0xcc</c> partent, le premier avec <c>+0x14 = 0x10</c>, le second avec
    ///     <c>0x0b</c>.</description></item>
    ///   <item><term>a partir de 0x140</term><description>sept <c>0xc7</c> par image, et le
    ///     <b>voile plein cadre monte de 5 par canal</b> jusqu'au blanc — c'est en l'atteignant que
    ///     la bascule d'etat se fait.</description></item>
    /// </list>
    /// <para>
    /// <b>La secousse de camera a son propre compteur</b>, <c>+0x74</c>, independant de <c>+0x16</c>
    /// : passe <c>0x66</c> elle tremble d'amplitude <c>8</c> une image sur deux, et les deux
    /// premiers sons de la scene partent a <c>0x61</c> et <c>0x62</c>. Une seconde secousse, plus
    /// fine, accompagne la pose <c>0x12</c> une fois son clip fini.
    /// </para>
    /// </summary>
    private static void EntityHandler_00440840(byte* e)
    {
        MusicFadeStep_0041AF50();                                   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                             // Ghidra: FUN_0040e640

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x9a) = 0x0b;
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x98) = 0;
        }

        byte* victim = null;
        byte* clip = null;

        byte* scene = Mem.Entity(0x005ecaa0);
        for (int i = 0x20; i != 0; i--, scene += 0x9c)
        {
            if (scene[0x0c] != 0) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            // Seule scene de la grotte a indexer le clip par la POSE et non par la constante 2.
            *(short*)(scene + 0x18) = *(short*)(e + 0x9a);
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4)
                             + *(short*)(e + 0x9a) * 4);

            *(uint*)scene |= 0x1000;
            victim = scene;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);

            *(short*)(victim + 0x1a) = 0;
            victim[0x0a] = 0;
            *(uint*)(victim + 0x38) = 0xffffff;
            *(short*)(victim + 0x36) = 1;
            *(int*)(e + 0x74) = 0;
        }

        // ------------------------------------------------- la secousse, sur son propre compteur
        *(int*)(e + 0x74) += 1;

        if ((uint)*(int*)(e + 0x74) > 0x66)
        {
            TryPublishCameraEffectNearEntity(
                (uint*)e, 0x3e7, (*(int*)(e + 0x74) & 1) != 0 ? 8 : 0, 0x3e7, 0, 0);   // Ghidra: FUN_0041be60
        }

        if (*(int*)(e + 0x74) == 0x61) Aud.PlaySoundEffect(0x0b, 0x1c2, 0x1800, 0x1800);   // Ghidra: FUN_0053cea0
        if (*(int*)(e + 0x74) == 0x62) Aud.PlaySoundEffect(0x20, 0x200, 0x1800, 0x1800);

        // ------------------------------------------------- la suite d'animations
        if ((e[0x16] & 1) != 0)
        {
            if (*(int*)(e + 0x5c) == 0)
            {
                *(short*)(victim + 0x1a) += 1;

                if (*(short*)(victim + 0x1a) >= *(ushort*)(clip + 0x16))
                {
                    *(short*)(victim + 0x1a) = 0;
                    *(short*)(e + 0x9a) += 1;

                    if (*(short*)(e + 0x9a) == 0x0d) *(short*)(e + 0x9a) = 0x0e;

                    if (*(short*)(e + 0x9a) >= 0x0f)
                    {
                        *(short*)(e + 0x9a) = 0x0e;
                        *(short*)(victim + 0x1a) = (short)(*(ushort*)(clip + 0x16) - 1);
                    }
                }
            }
            else
            {
                *(short*)(e + 0x16) -= 1;                           // le temps fait du sur-place
            }
        }

        *(int*)(e + 0x5c) -= 1;

        if (*(short*)(e + 0x9a) == 0x0e && *(short*)(victim + 0x1a) == 0x1b
            && *(int*)(e + 0x5c) == -1)
        {
            *(int*)(e + 0x5c) = 0x0a;
        }

        if (*(int*)(e + 0x5c) < 0) *(int*)(e + 0x5c) = 0;

        // ------------------------------------------------- les trois eclats du geste
        if (*(short*)(e + 0x9a) == 0x0e
            && *(short*)(victim + 0x1a) >= *(ushort*)(clip + 0x16) - 1
            && *(short*)(e + 0x16) < 0xbe)
        {
            for (int i = 3; i != 0; i--)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc5, 0, -1);   // Ghidra: FUN_0047ac40
            }

            if (*(short*)(e + 0x98) == 1)
            {
                Aud.PlaySoundEffect(5, 0x190, 0x2000, 0x2000);
                *(short*)(e + 0x98) = 2;
            }
            else if (*(short*)(e + 0x98) == 0)
            {
                Aud.PlaySoundEffect(5, 0x100, 0x1400, 0x1400);
                *(short*)(e + 0x98) = 1;
            }
        }

        // ------------------------------------------------- le fond, puis les semis
        short beat = *(short*)(e + 0x16);

        if (beat == 0xaf)
        {
            SetSceneBackdrop(0xff, 0xff, 0xff, 2);                  // Ghidra: DAT_005e9160..66
            *(short*)(e + 0x12) = 0;
        }

        beat = *(short*)(e + 0x16);
        if (beat > 0xaf && beat < 0x12c)
        {
            if (*(short*)(e + 0x12) == 2)
            {
                *(short*)(e + 0x12) = 0;
                SpawnCastEntity_0047AAE0(e, 0xcb, 0, -1);           // Ghidra: FUN_0047aae0
            }
            *(short*)(e + 0x12) += 1;
        }

        beat = *(short*)(e + 0x16);
        if (beat > 0xf0 && beat < 0x118)
        {
            if (*(short*)(e + 0x98) == 2)
            {
                Aud.PlaySoundEffect(0x20, 0x320, 0x1300, 0x1300);
                *(short*)(e + 0x98) = 3;
            }
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc6, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0x118)
        {
            Aud.PlaySoundEffect(5, 0x320, 0x1000, 0x1000);
            SetSceneBackdrop(0, 0x96, 0x96, 2);
            *(short*)(e + 0x14) = 0x10;
            SpawnCastEntity_0047AAE0(e, 0xcc, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0x11d)
        {
            *(short*)(e + 0x14) = 0x0b;
            SpawnCastEntity_0047AAE0(e, 0xcc, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0x10e)
        {
            *(short*)(e + 0x98) = 0;
            SpawnCastEntity_0047AAE0(e, 0xcd, 0, -1);
        }

        beat = *(short*)(e + 0x16);
        if (beat >= 0x10e && beat <= 0x140)
        {
            if (*(short*)(e + 0x98) == 0) Aud.PlaySoundEffect(0x28, 0x100, 0x1000, 0x1000);
            *(short*)(e + 0x98) += 1;
            if (*(short*)(e + 0x98) == 7) *(short*)(e + 0x98) = 0;
        }

        beat = *(short*)(e + 0x16);
        if (beat >= 0x10e && beat <= 0x118 && (beat & 1) != 0)
        {
            int ground = *(int*)(e + 0x20);
            *(int*)(e + 0x20) = ground + 0xe6000;
            SpawnCastEntity_0047AAE0(e, 0xce, 0, -1);
            *(int*)(e + 0x20) = ground;
        }

        beat = *(short*)(e + 0x16);
        if (beat >= 0x10e && beat <= 0x12c)
        {
            SpawnCastEntity_0047AAE0(e, 0xcf, 0, -1);
            SpawnCastEntity_0047AAE0(e, 0xcf, 0, -1);
        }

        // ------------------------------------------------- le blanchiment, puis la bascule
        if (*(short*)(e + 0x16) >= 0x140)
        {
            if (*(short*)(e + 0x16) == 0x140) Mem.s_Unknown_005de024 = 0x01000000;   // Ghidra: DAT_005de024..27

            byte veilR = (byte)Mem.s_Unknown_005de024;

            if (veilR != 0xff)
            {
                byte veilG = (byte)(Mem.s_Unknown_005de024 >> 8);
                byte veilB = (byte)(Mem.s_Unknown_005de024 >> 0x10);

                Mem.s_Unknown_005de024 = (Mem.s_Unknown_005de024 & 0xff000000)
                                         | (uint)(byte)(veilR + 5)
                                         | ((uint)(byte)(veilG + 5) << 8)
                                         | ((uint)(byte)(veilB + 5) << 0x10);
            }
            else
            {
                Mem.s_WorldFlags_005de02c |= 2;                     // Ghidra: DAT_005de02c
                s_WorldMapStep_005645f8 = unchecked((sbyte)0xfd);   // Ghidra: DAT_005645f8
                s_GameStateId_005de510 = 7;                         // Ghidra: DAT_005de510
                Mem.s_Unknown_005de520 = 3;                         // Ghidra: DAT_005de520
            }

            for (int i = 7; i != 0; i--)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc7, 0, -1);
            }
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) >= 0x1f4)
        {
            *(uint*)e &= 0x7fffffff;
            CameraModeApply_005037E0(0);                            // Ghidra: FUN_005037b0

            RunEntityScript(
                s_Unknown_005e97cc,
                Inp.s_SlotFlags_005e93c0 + Mem.s_Unknown_005de538 * 0x9c, 0);   // Ghidra: FUN_00513dd0

            *(uint*)victim |= 0x2004000;

            SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54),
                             (short)*(int*)(e + 0x58), 2);
            Mem.s_Unknown_005de024 = 0x01000000;
        }

        // ------------------------------------------------- la seconde secousse, plus fine
        if (*(short*)(e + 0x9a) == 0x12
            && *(short*)(victim + 0x1a) >= *(ushort*)(clip + 0x16))
        {
            TryPublishCameraEffectNearEntity((uint*)e, 0, CrtRand() % 5 - 2, 1, 0, 0);
        }
    }
}
