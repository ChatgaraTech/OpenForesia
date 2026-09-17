// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES DEUX SCENES CINEMA DE LA MONTAGNE DU DRAGON DE FEU — zone 2, table 0x0056b0c8.
//
// Ouvert le 2026-09-17, sur le patron de `MineScenes.cs`. Les deux se suivent, et le binaire le
// dit lui-meme :
//
//   0x0040E3A0   creneau 0x02 du bloc 0x00555028   L'ENVOL : l'entite de scene d'espece 0 — le
//                                                  Dragon rouge dans cette sous-zone — monte et
//                                                  s'eloigne ; a la fin elle fait naitre 0xff.
//   0x0040D930   creneau 0x3f du meme bloc         LA SCENE MUETTE, que 0xff designe : la camera
//                                                  cadre un membre du groupe, l'ecran blanchit,
//                                                  la pose de fin, le script 0.
//
// Le fil qui les tient est l'octet 0x00571418 : l'envol y range l'octet +0x09 du dragon, et la
// scene muette le relit comme le membre a cadrer.
//
// LE BLOC PRIVE EST L'ENREGISTREMENT 0x005713f0, et il a demande une decision de plan memoire.
// Il court de 0x005713f0 a 0x0057141f, a cheval sur 0x00571400 — la fin du contenu de .data dans
// le fichier, pas une frontiere de structure. `s_ViewParams_00571270` (GameMemory.cs) a ete
// etendue de 0x190 a 0x1b0 le 2026-09-17 pour le couvrir d'un seul tenant : elle s'arrete
// desormais exactement sur la base du bloc de la foret, 0x00571420. L'enregistrement passe donc
// par Mem.ViewParams, comme celui de la mine juste avant lui.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs` ; les aides de camera, de
// voile et de pose sont dans `ForestScenes.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x005713f0</c> — <b>l'enregistrement de camera des scenes de la montagne</b>,
    /// <c>0x30</c> octets, sur le meme plan que <see cref="ForestSceneRecord"/>. Son <c>+0x28</c>
    /// (<c>0x00571418</c>) est l'octet que l'envol ecrit et que la scene muette relit.
    /// </summary>
    private static byte* FireMountainSceneRecord => Mem.ViewParams(0x005713f0);

    /// <summary>
    /// Ghidra: FUN_0040d930 @ 0x0040d930 — <b>la scene muette de la montagne</b>, creneau
    /// <c>0x3f</c> du bloc <c>0x00555028</c> (table <c>0x0056b0c8</c>, donc le sous-type
    /// <c>0xff</c> du pool <c>0x005ff400</c>). Decompilee dans <c>Blaze.c</c>, 286 instructions.
    ///
    /// <para>
    /// <b>C'est <see cref="TickForestCutscene"/> recompilee</b>, et le desassemblage des deux,
    /// cibles de saut et adresses du bloc masquees, ne differe qu'en quatre endroits :
    /// </para>
    /// <list type="bullet">
    /// <item>le bloc prive est <c>0x005713f0</c> au lieu de <c>0x00571420</c> ;</item>
    /// <item><c>+0x98</c> recoit <c>0x3d</c> au lieu de <c>0x38</c> — et le semis de l'etat 3 pose
    /// le sous-type en <b>constante</b> <c>0xfd</c>, la ou la foret relit <c>+0x98 + 0xc0</c>.
    /// C'est la meme valeur, le compilateur l'a propagee ;</item>
    /// <item>le fondu de sortie emploie d'autres registres — memes valeurs, memes ecritures ;</item>
    /// <item><b>la derniere image n'est pas dans le meme ordre, et ne lance pas le meme
    /// script</b>. La foret appelle le mode de camera, lance le <b>script 1</b> sur le membre, puis
    /// rend la camera. La montagne appelle le mode de camera, rend la camera (pointeurs puis mode),
    /// rappelle le mode <b>une troisieme fois</b>, et lance le <b>script 0</b>.</item>
    /// </list>
    /// </summary>
    private static void EntityHandler_0040D930(byte* e)
    {
        byte* record = FireMountainSceneRecord;

        *(short*)(e + 0x98) = 0x3d;
        ResetPartyAppearance();

        if (*(short*)(e + 0x10) == 0)
        {
            DisarmPartySlots_00423D10();
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = record[0x28];   // Ghidra: `movzx ax,byte [571418h]`
        }

        if (*(short*)(e + 0x10) == 1 && WaitForPartyToSettle()) *(short*)(e + 0x10) = 2;   // Ghidra: FUN_0040b0e0

        if (*(short*)(e + 0x10) == 2) TakeCutsceneCamera(e, record);

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x10) = 4;
            *(short*)(e + 0x14) = 2;

            // Ghidra: `push 0FDh` — la constante, et non la relecture de +0x98.
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfd, 0, -1);
        }

        (*(short*)(e + 0x16))++;

        short beat = *(short*)(e + 0x16);

        if (beat > 0x81 && beat < 0x9b) ShiftCutsceneVeils(+1);

        if (beat == 0x9b)
        {
            byte* mainMem = GetMainMemPtr();
            byte* slot = Inp.s_SlotFlags_005e93c0;

            for (int i = 0; i < 4; i++, slot += 0x9c)
            {
                uint flags = *(uint*)slot;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                ApplyCutscenePose(slot, mainMem[(slot[8] + 0x78) * 0x2000 + 0x150]);
            }
        }

        if (beat > 0x9a && beat < 0xb4) ShiftCutsceneVeils(-1);

        if (beat == 200)
        {
            Mem.s_Unknown_005de028 = 0x01000000;
            Mem.s_Unknown_005de024 = 0x01000000;
        }

        if (beat != 0xca) return;

        // Ghidra: 0x0040dd7a..0x0040ddd2 — trois FUN_005037b0, les pointeurs entre le premier et le
        // deuxieme, le script apres le troisieme.
        CameraModeApply_005037E0(0);                          // Ghidra: FUN_005037b0, le premier
        ReleaseCutsceneCamera();                              // les pointeurs, puis le deuxieme
        CameraModeApply_005037E0(0);                          // le troisieme

        RunEntityScript(
            s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + *(short*)(e + 0x12) * 0x9c, 0);   // Ghidra: FUN_00513dd0

        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0040e3a0</c> — <b>l'envol du Dragon rouge</b>, creneau <b>2</b> du bloc
    /// <c>0x00555028</c> (table <c>0x0056b0c8</c>, sous-type <c>0xc2</c>). <b>Absente de
    /// <c>Blaze.c</c></b> ; 183 instructions, <c>Legacy/Raw/Blaze/FUN_0040E3A0.asm</c>.
    ///
    /// <para>
    /// <b>La cousine de <see cref="TickPartySummon"/> et de <see cref="EntityHandler_0040B940"/></b> :
    /// meme ossature — les deux appels de tete, le balayage des trente-deux entites de scene qui
    /// retient la <b>derniere</b>, le tampon nul de secours, la premiere image qui recopie l'octet
    /// <c>+0x09</c> de la cible dans le bloc prive —, et un corps qui lui est propre.
    /// </para>
    /// <list type="table">
    ///   <item><term>la cible</term><description>l'entite de scene dont <c>+0x0c</c> vaut
    ///     <b>0</b> — l'espece 0 de la sous-zone, le Dragon rouge (<c>especes.csv</c>). Elle prend la
    ///     pose <b>1</b>, le bit <c>0x100000</c>, et son clip est lu au <c>+4</c> de la table
    ///     d'animation (la ou ses cousines lisent <c>+8</c>). Sa position n'est PAS recopiee dans
    ///     l'entite pendant le balayage.</description></item>
    ///   <item><term>la premiere image</term><description>les trois shorts <c>0x005e92b8</c> dans
    ///     <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> (jamais relus ici), la cible remise a blanc, un
    ///     volume de <c>0xe00</c> en <c>+0x14</c>.</description></item>
    ///   <item><term>les battements</term><description>aux images 15 et 34 du clip — les images du
    ///     pas de <see cref="RedDragonStepSound_004998EF"/> —, les sons <c>0x1f</c> et
    ///     <c>0x21</c> au volume courant. Deux images plus tard, un <b>echo</b> : <c>0x1f</c> plus
    ///     aigu, <c>0x200</c> plus bas, et le volume perd <c>0x100</c> pour de bon. Le son
    ///     s'eloigne avec la bete.</description></item>
    ///   <item><term>etat 1, la montee</term><description>la cible monte de <c>0x5000</c> par
    ///     image en glissant dans son cap ; passe la 40e image, l'entite la rejoint.</description></item>
    ///   <item><term>etat 2</term><description>vingt et une images de pause.</description></item>
    ///   <item><term>etat 3, le depart</term><description>montee de <c>0x3000</c> et glissement
    ///     cinq fois plus rapide ; au-dela de cent images, <b>naissance de <c>0xff</c></b> — la
    ///     scene muette <see cref="EntityHandler_0040D930"/> —, extinction, bit
    ///     <c>0x2004000</c> sur le dragon, et le dragon est rejete a <c>(20000, y, 20000)</c>.</description></item>
    /// </list>
    /// <para>
    /// <b>Le clip boucle</b> : passe sa derniere image, <c>+0x1a</c> revient a zero, la ou ses
    /// cousines bornent sur la derniere.
    /// </para>
    /// <para>
    /// <b>Le meme defaut de 1998 que ses cousines</b> : sans cible, le clip est un mot de pile
    /// jamais ecrit (<c>[esp+0Ch]</c>), lu des la premiere image. Le pointeur est laisse nul ici,
    /// pour que l'acces fautif se produise au meme endroit.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040E3A0(byte* e)
    {
        // Le tampon nul du binaire : l'enregistrement de secours quand le balayage ne trouve rien.
        byte* fallback = stackalloc byte[0x9c];
        for (int i = 0; i < 0x9c; i++) fallback[i] = 0;

        MusicFadeStep_0041AF50();                  // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();            // Ghidra: FUN_0040e640

        byte* target = fallback;
        byte* clip = null;                         // Ghidra: `[esp+0Ch]`, non initialise sans cible

        byte* scene = Mem.Entity(0x005ecaa0);
        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 0) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(short*)(scene + 0x18) = 1;
            target = scene;
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 4);
            *(uint*)scene |= 0x100000;
        }

        // ------------------------------------------------- la premiere image
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);

            *(short*)(target + 0x1a) = 0;
            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;

            *(short*)(e + 0x98) = -1;
            *(short*)(e + 0x14) = 0x0e00;
            FireMountainSceneRecord[0x28] = target[9];   // Ghidra: `mov [571418h],dl`
        }

        // ------------------------------------------------- les battements et leur echo
        // Ghidra: LAB_0040e479
        short frame = *(short*)(target + 0x1a);
        if (frame == 0x0f || frame == 0x22)
        {
            Aud.PlaySoundEffect(0x1f, 0x400, *(short*)(e + 0x14), *(short*)(e + 0x14));   // Ghidra: FUN_0053cea0
            Aud.PlaySoundEffect(0x21, 0x16a, *(short*)(e + 0x14), *(short*)(e + 0x14));
            *(short*)(e + 0x98) = 2;
        }

        // Ghidra: LAB_0040e4bc
        if (*(short*)(e + 0x98) == 0)
        {
            int echo = *(short*)(e + 0x14) - 0x200;
            *(short*)(e + 0x98) = -1;
            Aud.PlaySoundEffect(0x1f, 0x3d3, echo, echo);
            *(short*)(e + 0x14) -= 0x100;
        }

        // Ghidra: LAB_0040e4ef — le rebours, borne a -0x1e.
        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < -0x1e) *(short*)(e + 0x98) = -0x1e;

        // ------------------------------------------------- etat 1 : la montee
        // Ghidra: LAB_0040e50c
        if (*(short*)(e + 0x10) == 1)
        {
            int course = *(short*)(target + 0x32);
            *(int*)(target + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 2;   // Ghidra: 0x00545a1c
            *(int*)(target + 0x24) += Winsys3d.GetCosTableEntry(course) * 2;        // Ghidra: 0x00545a16
            *(int*)(target + 0x20) += 0x5000;

            if (*(short*)(e + 0x16) > 0x28)
            {
                *(short*)(e + 0x10) = 2;
                *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
                *(int*)(e + 0x20) = *(int*)(target + 0x20);
                *(int*)(e + 0x24) = *(int*)(target + 0x24);
                *(short*)(e + 0x12) = 0;
            }
        }

        // ------------------------------------------------- etat 2 : la pause
        // Ghidra: LAB_0040e56b
        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x14)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
            }
        }

        // ------------------------------------------------- etat 3 : le depart
        // Ghidra: LAB_0040e58c
        if (*(short*)(e + 0x10) == 3)
        {
            int course = *(short*)(target + 0x32);
            *(int*)(target + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 10;
            *(int*)(target + 0x24) += Winsys3d.GetCosTableEntry(course) * 10;
            *(int*)(target + 0x20) += 0x3000;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x64)
            {
                SpawnEffectEntity(e, 0xff, 0, -1);           // Ghidra: FUN_0047a9d0 — la scene muette

                *(uint*)e &= 0x7fffffff;
                *(uint*)target |= 0x2004000;
                *(int*)(target + 0x1c) = 0x1388000;
                *(int*)(target + 0x24) = 0x1388000;
            }
        }

        // ------------------------------------------------- le clip, qui boucle
        // Ghidra: LAB_0040e607 — `xor ecx,ecx` puis `mov cx,[edx+16h]` : le nombre d'images est
        // etendu sans signe, l'image courante avec.
        (*(short*)(target + 0x1a))++;
        if (*(short*)(target + 0x1a) >= *(ushort*)(clip + 0x16)) *(short*)(target + 0x1a) = 0;

        (*(short*)(e + 0x16))++;
    }
}
