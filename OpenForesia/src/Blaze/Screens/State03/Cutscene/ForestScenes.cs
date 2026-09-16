// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES DEUX SCENES CINEMA DE LA FORET.
//
// Ce que le jeu fait quand il reprend la main au joueur : desarmer les creneaux d'equipe,
// attendre que le groupe se pose, imposer une pose, prendre la camera, la rendre, remettre
// l'apparence d'origine. Les deux scenes de la foret sont ecrites a la suite.
//
// Les VERBES qu'elles emploient sont dans `CutsceneVerbs.cs` (table 0x0055eb68, 32 entrees).

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES DEUX SCENES CINEMA DE LA FORET (2026-08-30)
    //
    // Quatre creneaux, deux corps. 0x00410820 et 0x00413c90 sont le meme corps a 286 instructions,
    // decompile dans Blaze.c ; 0x00411240 et 0x004146b0 sont deux AMORCES de dix instructions vers
    // FUN_00411270 et FUN_00414d10, elles aussi decompilees et elles aussi identiques entre elles.
    //
    // Dans les deux cas la seule difference entre les exemplaires est la BASE DU BLOC PRIVE :
    // l'enregistrement 0 (0x00571420) pour les uns, le 1 (0x00571450) pour les autres. Elles sont
    // donc parametrees par un simple indice.
    //
    // Ces quatre-la avaient ete mises de cote le 2026-08-30 pour trois raisons : le bloc prive non
    // modelise, et deux appelees « manquantes ». Aucune des trois ne tenait — FUN_00423d10 et
    // FUN_0040b0e0 sont dans Blaze.c, et font quinze et quarante lignes.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00423d10 @ 0x00423d10 — <b>desarme les quatre emplacements</b>. Decompilee dans
    /// <c>Blaze.c</c>, quinze lignes : elle efface le bit <c>0x4000000</c> sur les quatre fiches,
    /// et rien d'autre.
    ///
    /// <para>C'est la remise a zero du compteur d'attente de <see cref="WaitForPartyToSettle"/> :
    /// tant que les quatre bits ne sont pas revenus, la scene n'avance pas.</para>
    /// </summary>
    private static void DisarmPartySlots_00423D10()
    {
        byte* slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, slot += 0x9c) *(uint*)slot &= 0xfbffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0040b0e0 @ 0x0040b0e0 — <b>attend que les quatre membres soient poses</b>.
    /// Decompilee dans <c>Blaze.c</c>. Rend <c>true</c> quand les quatre portent le bit
    /// <c>0x4000000</c>.
    ///
    /// <para>
    /// <b>Chaque membre tombe dans un des trois cas.</b> Le binaire les calcule par deux tests
    /// enchevetres que Ghidra rend avec des <c>goto</c> ; reconstruits, ils disent :
    /// </para>
    /// <list type="number">
    /// <item><b>a preparer</b> — le membre est vivant, il ne porte aucun des bits d'occupation
    /// <c>0x44000003</c> ni <c>0x50000000</c>. Il est remis en pose neutre : teinte blanche, pose
    /// et image a zero, <c>+0x0a</c> et <c>+0x96</c> effaces, le bit <c>0x40</c> retire et
    /// <c>0x1000000</c> pose. Puis il est marque pret.</item>
    /// <item><b>hors jeu</b> — mort, absent ou occupe autrement : marque pret sans etre
    /// touche.</item>
    /// <item><b>encore occupe</b> — vivant et portant un bit de <c>0x44000003</c> : rien du tout,
    /// et la scene attendra une image de plus.</item>
    /// </list>
    /// <para>
    /// <b>Le mot de drapeaux est relu apres avoir ete ecrit</b> dans le premier cas — le second
    /// masque part de la valeur fraiche, pas de celle du debut. Conserve tel quel.
    /// </para>
    /// </summary>
    private static bool WaitForPartyToSettle()
    {
        byte* slot = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            short verdict = 0;

            if ((flags & 0x44000003) == 0 && (flags & 0x80000000) != 0) verdict = 1;

            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) verdict = 2;

            if (verdict == 1)
            {
                *(uint*)slot = flags & 0xffffffbf | 0x4000000;

                slot[0x0a] = 0;
                slot[0x38] = 0xff;
                slot[0x39] = 0xff;
                slot[0x3a] = 0xff;
                *(ushort*)(slot + 0x36) &= 0xfffb;
                *(short*)(slot + 0x96) = 0;

                // Ghidra relit *puVar2 ici : le masque part de la valeur qui vient d'etre ecrite.
                *(uint*)slot = *(uint*)slot & 0xf7fffffb | 0x1000000;

                *(short*)(slot + 0x18) = 0;
                *(short*)(slot + 0x1a) = 0;
            }

            if (verdict == 2) *(uint*)slot |= 0x4000000;
        }

        short ready = 0;
        slot = Inp.s_SlotFlags_005e93c0;
        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            if ((*(uint*)slot & 0x4000000) != 0) ready++;
        }

        return ready == 4;
    }

    /// <summary>
    /// La pose de fin de scene, par classe. Les deux scenes cinema portent la <b>meme</b> table,
    /// ecrite en toutes lettres dans chacune : seize couples <c>(pose, image)</c>, choisis par les
    /// trois bits bas de l'octet d'identite <c>+0x150</c> de la fiche de personnage, le bit
    /// <c>3</c> departageant les deux moities.
    ///
    /// <para>Aucun des seize couples n'est calculable a partir de la classe : c'est une table, et
    /// elle est relevee comme telle.</para>
    /// </summary>
    private static void ApplyCutscenePose(byte* slot, byte identity)
    {
        int cls = identity & 7;

        short pose, frame;

        if ((identity & 8) == 0)
        {
            switch (cls)
            {
                case 0: pose = 5; frame = 1; break;
                case 1: pose = 6; frame = 0x12; break;
                case 2: pose = 4; frame = 5; break;
                case 3: pose = 3; frame = 3; break;
                case 4: pose = 5; frame = 8; break;
                case 5: pose = 6; frame = 9; break;
                case 6: pose = 6; frame = 0x0b; break;
                default: pose = 6; frame = 0x12; break;
            }
        }
        else
        {
            switch (cls)
            {
                case 0: pose = 6; frame = 4; break;
                case 1: pose = 6; frame = 9; break;
                case 2: pose = 3; frame = 0; break;
                case 3: pose = 6; frame = 0x12; break;
                case 4: pose = 5; frame = 8; break;
                case 5: pose = 7; frame = 5; break;
                case 6: pose = 6; frame = 4; break;
                default: pose = 6; frame = 9; break;
            }
        }

        *(short*)(slot + 0x18) = pose;
        *(short*)(slot + 0x1a) = frame;
    }

    /// <summary>Les quatre membres remis a blanc en tete de chaque image de scene : teinte pleine,
    /// <c>+0x0a</c> et <c>+0x96</c> effaces, bit 2 de <c>+0x36</c> retire. Les deux corps de scene
    /// ouvrent par cette meme boucle.</summary>
    private static void ResetPartyAppearance()
    {
        byte* slot = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, slot += 0x9c)
        {
            slot[0x0a] = 0;
            slot[0x38] = 0xff;
            slot[0x39] = 0xff;
            slot[0x3a] = 0xff;
            *(ushort*)(slot + 0x36) &= 0xfffb;
            *(short*)(slot + 0x96) = 0;
        }
    }

    /// <summary>
    /// La camera privee prend la main : le bloc <paramref name="record"/> recoit la pose du membre
    /// vise, et les trois pointeurs <c>DAT_005e9ae0</c> / <c>DAT_005eca94</c> / <c>DAT_005edebc</c>
    /// y sont detournes. Les deux corps de scene font exactement cela, au bloc pres.
    ///
    /// <para><b>La cible est tiree au sort autour du membre</b> : l'azimut prend <c>+200</c> plus
    /// un tirage sur 400, l'elevation <c>+0x200</c> plus un tirage sur 200, et la distance
    /// <c>+700</c> plus un tirage sur 200. La scene ne cadre donc jamais deux fois pareil.</para>
    /// </summary>
    private static void TakeCutsceneCamera(byte* e, byte* record)
    {
        byte* slots = Inp.s_SlotFlags_005e93c0;
        int index = *(short*)(e + 0x12);
        byte* member = slots + index * 0x9c;

        *(int*)(e + 0x1c) = *(int*)(member + 0x1c);
        *(int*)(e + 0x20) = *(int*)(member + 0x20);
        *(int*)(e + 0x24) = *(int*)(member + 0x24);
        *(short*)(e + 0x30) = *(short*)(member + 0x30);
        *(short*)(e + 0x32) = *(short*)(member + 0x32);

        *(int*)(record + 0x18) = *(int*)(e + 0x1c);

        *(short*)(e + 0x34) = *(short*)(member + 0x34);
        *(short*)(e + 0x10) = 3;
        *(short*)(e + 0x16) = 0;

        *(int*)(record + 0x1c) = *(int*)(e + 0x20);
        *(int*)(record + 0x20) = *(int*)(e + 0x24);

        *(short*)(record + 0) = (short)(CrtRand() % 400 + *(short*)(e + 0x30) + 200);
        *(short*)(record + 2) = (short)(CrtRand() % 200 + *(short*)(e + 0x32) + 0x200);
        *(short*)(record + 4) = *(short*)(e + 0x34);

        int* target = *(int**)Mem.Entity(0x005eca94);
        *(int*)(record + 8) = target[0];
        *(int*)(record + 0x0c) = target[1];
        *(int*)(record + 0x10) = target[2];

        int distance = CrtRand();

        *(byte**)Mem.Entity(0x005eca94) = record + 8;
        *(byte**)Mem.Arena(0x005e9ae0) = record;
        *(byte**)Mem.Entity(0x005edebc) = record + 0x18;

        *(int*)(record + 0x10) = distance % 200 + 700;
    }

    /// <summary>La camera de marche reprend la main : les trois memes pointeurs remis sur
    /// <c>0x005de078</c>, <c>0x00564650</c> et <c>0x005de068</c>, puis le mode de camera
    /// rappele.</summary>
    private static void ReleaseCutsceneCamera()
    {
        *(byte**)Mem.Entity(0x005eca94) = (byte*)Mem.Camera(0x005de078);
        *(byte**)Mem.Arena(0x005e9ae0) = s_WorldMapCamera_00564650;
        *(byte**)Mem.Entity(0x005edebc) = (byte*)Mem.Camera(0x005de068);

        CameraModeApply_005037E0(0);                          // Ghidra: FUN_005037b0
    }

    /// <summary>Le fondu des deux voiles pendant une scene : le second monte ou descend de dix par
    /// canal, le premier de trois. C'est le meme rapport que
    /// <see cref="EntityHandler_0040EDF0"/>.</summary>
    private static void ShiftCutsceneVeils(int step)
    {
        Mem.s_Unknown_005de028 = ShiftScreenVeil(Mem.s_Unknown_005de028, 10 * step);
        Mem.s_Unknown_005de024 = ShiftScreenVeil(Mem.s_Unknown_005de024, 3 * step);
    }

    /// <summary>Ghidra: <c>0x00410820</c> — la premiere scene cinema, bloc prive 0.</summary>
    private static void EntityHandler_00410820(byte* e)
        => TickForestCutscene(e, ForestSceneRecord(0));

    /// <summary>Ghidra: <c>0x00413c90</c> — la seconde, bloc prive 1. Comparee instruction par
    /// instruction : <b>seules les onze adresses du bloc different</b>.</summary>
    private static void EntityHandler_00413C90(byte* e)
        => TickForestCutscene(e, ForestSceneRecord(1));

    /// <summary>
    /// Ghidra: FUN_00410820 @ 0x00410820 (et FUN_00413c90) — <b>la scene cinema muette de la
    /// foret</b>. Decompilee dans <c>Blaze.c</c>, 286 instructions.
    ///
    /// <para>
    /// <b>Cinq etats, puis un compte a rebours de 202 images.</b> L'etat 0 desarme les quatre
    /// emplacements et retient dans <c>+0x12</c> le membre que la memoire du bloc designe ; l'etat
    /// 1 attend que les quatre soient poses ; l'etat 2 detourne la camera ; l'etat 3 seme l'entite
    /// de sous-type <c>+0x98 + 0xc0</c> et passe a 4, ou plus rien ne se fait. Tout le reste est
    /// gouverne par <c>+0x16</c>, qui monte d'un par image sans jamais redescendre.
    /// </para>
    /// <list type="bullet">
    /// <item><b>0x82 a 0x9a</b> — les deux voiles montent, le second de dix par canal, le premier
    /// de trois : l'ecran blanchit en vingt-cinq images.</item>
    /// <item><b>0x9b</b> — la pose de fin est posee sur les quatre membres, par la table de
    /// <see cref="ApplyCutscenePose"/>. C'est l'image ou l'ecran est le plus clair, donc celle ou
    /// le changement ne se voit pas.</item>
    /// <item><b>0x9b a 0xb3</b> — les deux voiles redescendent au meme rapport.</item>
    /// <item><b>0xc8</b> — les deux voiles sont remis a zero net, canaux compris.</item>
    /// <item><b>0xca</b> — la camera de marche revient, le script 1 part sur le membre vise, et
    /// l'entite s'eteint.</item>
    /// </list>
    /// <para>
    /// <b>Le mode de camera est rappele deux fois de suite</b> a la derniere image, une fois avant
    /// le detournement des pointeurs et une fois apres. Ghidra le rend ainsi et le desassemblage le
    /// confirme.
    /// </para>
    /// </summary>
    private static void TickForestCutscene(byte* e, byte* record)
    {
        *(short*)(e + 0x98) = 0x38;
        ResetPartyAppearance();

        if (*(short*)(e + 0x10) == 0)
        {
            DisarmPartySlots_00423D10();
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = record[0x28];
        }

        if (*(short*)(e + 0x10) == 1 && WaitForPartyToSettle()) *(short*)(e + 0x10) = 2;

        if (*(short*)(e + 0x10) == 2) TakeCutsceneCamera(e, record);

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x10) = 4;
            *(short*)(e + 0x14) = 2;
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), null, (byte)(*(short*)(e + 0x98) + 0xc0), 0, -1);
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

        CameraModeApply_005037E0(0);                          // Ghidra: FUN_005037b0, le premier
        RunEntityScript(
            s_Unknown_005e97cc, Inp.s_SlotFlags_005e93c0 + *(short*)(e + 0x12) * 0x9c, 1);

        ReleaseCutsceneCamera();                              // FUN_005037b0, le second
        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00411240</c> — <b>l'amorce de la scene cinema parlante</b>, bloc prive 0.
    /// Absente de <c>Blaze.c</c> ; dix instructions. Elle pose <c>+0x98 = 0x39</c> et appelle
    /// <c>FUN_00411270</c> avec trois constantes : le script <c>1</c>, la memoire d'emplacement du
    /// bloc, et <c>-1</c> pour « ne pas changer de musique ».
    /// </summary>
    private static void EntityHandler_00411240(byte* e)
    {
        byte* record = ForestSceneRecord(0);
        *(short*)(e + 0x98) = 0x39;
        TickForestCutsceneWithDialogue(e, record, 1, record[0x28], 0xffff);
    }

    /// <summary>Ghidra: <c>0x004146b0</c> — la meme amorce sur le bloc prive 1. Les deux corps
    /// appeles, <c>FUN_00411270</c> et <c>FUN_00414d10</c>, ne different eux aussi que par les
    /// adresses du bloc.</summary>
    private static void EntityHandler_004146B0(byte* e)
    {
        byte* record = ForestSceneRecord(1);
        *(short*)(e + 0x98) = 0x39;
        TickForestCutsceneWithDialogue(e, record, 1, record[0x28], 0xffff);
    }

    /// <summary>
    /// Ghidra: FUN_00411270 @ 0x00411270 (et FUN_00414d10) — <b>la scene cinema parlante de la
    /// foret</b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// C'est <see cref="TickForestCutscene"/> avec quatre ajouts, et une soustraction. Le squelette
    /// est identique — desarmement, attente, camera privee, semis, puis le compte de <c>+0x16</c>
    /// avec ses deux fondus et sa pose de fin a <c>0x9b</c>.
    /// </para>
    /// <list type="bullet">
    /// <item><b>Cinq sons</b> ponctuent le debut (<c>0x0f</c>, <c>9</c>, <c>0x2f</c> aux images 0,
    /// 1 et 2) et le fondu au blanc (<c>5</c> deux fois, aux images <c>0x82</c> et <c>0x83</c>,
    /// la seconde plus aigue). La scene muette n'en joue aucun.</item>
    /// <item><b>Une boite de dialogue</b> s'ouvre a l'image 200, au moment ou les voiles retombent
    /// a zero, sur la fiche du membre vise et avec l'etiquette <c>+0x490</c> du repertoire
    /// principal.</item>
    /// <item><b>Le script part sans entite</b> : <c>RunEntityScript(repertoire, null, script)</c>,
    /// la ou la scene muette le lance sur le membre. Et il ne part que si
    /// <paramref name="script"/> vaut autre chose que <c>-1</c>.</item>
    /// <item><b>Un changement de musique optionnel</b>, si <paramref name="music"/> n'est pas
    /// <c>0xffff</c> et que la piste demandee n'est pas deja en cours : la piste demarre et chaque
    /// membre vivant recoit l'effet <c>0x59</c>. Les deux amorces connues passent <c>0xffff</c>,
    /// donc <b>ce bloc ne s'execute jamais par elles</b> — il est transcrit parce qu'il est la, pas
    /// parce qu'on l'a vu tourner.</item>
    /// </list>
    /// <para>
    /// <b>La soustraction</b> : le mode de camera n'est rappele qu'une fois a la fin, contre deux
    /// dans la scene muette.
    /// </para>
    /// <para>
    /// <b>Le meme defaut de descripteur que la gerbe.</b> Le semis de l'etat 3 passe le tampon de
    /// pile <c>local_8</c>, que rien ne remplit — voir <c>LB-014</c> dans <c>legacy-bug.md</c>. La
    /// scene muette, elle, passe proprement <c>0</c> au meme endroit.
    /// </para>
    /// </summary>
    /// <param name="script">L'indice de script lance a la derniere image, ou <c>-1</c> pour n'en
    /// lancer aucun.</param>
    /// <param name="member">Le membre du groupe que la scene cadre. Les deux amorces y passent la
    /// memoire d'emplacement du bloc.</param>
    /// <param name="music">La piste a lancer en sortant, ou <c>0xffff</c> pour n'y pas
    /// toucher.</param>
    private static void TickForestCutsceneWithDialogue(
        byte* e, byte* record, short script, short member, ushort music)
    {
        // Le tampon jamais rempli du binaire, comme dans TickFivePhaseBurst. Voir LB-014.
        byte* scratch = stackalloc byte[8];

        ResetPartyAppearance();

        if (*(short*)(e + 0x10) == 0)
        {
            DisarmPartySlots_00423D10();
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = member;
        }

        if (*(short*)(e + 0x10) == 1 && WaitForPartyToSettle()) *(short*)(e + 0x10) = 2;

        if (*(short*)(e + 0x10) == 2) TakeCutsceneCamera(e, record);

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x10) = 4;
            *(short*)(e + 0x14) = 2;
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), scratch, (byte)(*(short*)(e + 0x98) + 0xc0), 0, -1);
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x0f, 0x200, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(9, 0x100, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x2f, 0x400, 0x1000, 0x1000);

        (*(short*)(e + 0x16))++;

        short beat = *(short*)(e + 0x16);

        if (beat == 0x82) Aud.PlaySoundEffect(5, 0x100, 0x1000, 0x1000);
        if (beat == 0x83) Aud.PlaySoundEffect(5, 0x16a, 0x1000, 0x1000);

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

            byte* labels =
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x490);

            BindMenuLabels(
                labels,
                Inp.s_SlotFlags_005e93c0 + *(short*)(e + 0x12) * 0x9c,
                s_WorldMapDescriptor_005633b0,
                0);
        }

        if (beat != 0xca) return;

        if (script != -1) RunEntityScript(s_Unknown_005e97cc, null, (uint)script);

        if (music != 0xffff)
        {
            s_Unknown_005de534 = (byte)music;

            if (s_Unknown_00566c8c != s_Unknown_005de534)
            {
                s_Unknown_00566c8c = (byte)s_Unknown_005de534;
                PlayMusicTrack(music & 0xff);                 // Ghidra: FUN_00529ad0

                byte* slot = Inp.s_SlotFlags_005e93c0;
                for (int i = 0; i < 4; i++, slot += 0x9c)
                {
                    uint flags = *(uint*)slot;
                    if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                    SpawnEffectEntity(slot, 0x59, 0, -1);     // Ghidra: FUN_0047a9d0
                }
            }
        }

        ReleaseCutsceneCamera();
        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0040ef40</c> — <b>le geyser et sa partition</b>. Absent de <c>Blaze.c</c> ;
    /// releve au desassembleur, 158 instructions. Table <c>0x0056b2c8</c>, foret, sous-zones 0 a 4.
    ///
    /// <para>
    /// <b>Sa planche est le sixieme enregistrement d'un bloc deja releve</b> : <c>0x0054f7f0</c>
    /// est la tuile <c>0x17 x 0x0f</c> que le commentaire de <see cref="s_Descriptors_0054F7C8"/>
    /// signalait deja comme « d'une autre planche ». Troisieme fois que la regle « relever la
    /// plage » evite un trou.
    /// </para>
    /// <para>
    /// <b>Trois etats, et il ne dessine presque rien lui-meme.</b> A la naissance il monte de
    /// <c>0x28000</c> et seme un <c>0xfc</c> ; l'etat 1 seme un <c>0xfb</c> par image pendant
    /// soixante images, monte de <c>0x2000</c> et grandit de cent par axe — l'echelle horizontale
    /// bornee a <c>0x800</c>, la verticale a <c>0x9c4</c>, donc un jet qui s'etire plus haut que
    /// large ; l'etat 2 seme toujours et fait <b>onduler</b> la hauteur au sinus d'un angle qui
    /// avance de <c>0x50</c> par image, jusqu'a <c>0x2710</c>.
    /// </para>
    /// <para>
    /// <b>La partition est en dur, sur un compteur qui n'est jamais remis a zero.</b> Neuf sons
    /// aux images <c>0</c>, <c>1</c>, <c>2</c>, <c>3</c>, puis <c>0x28</c> a <c>0x2b</c>, puis
    /// <c>0x50</c> — deux accords de trois notes montantes suivis d'une basse, puis une reprise.
    /// Elle tourne a chaque image <b>sauf une</b> : le passage de l'etat 1 a l'etat 2 rend la main
    /// avant, et cette image-la est muette.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040EF40(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int spout = *(int*)(e + 0x1c) + 0x28000;

            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F7C8() + 0x28;   // Ghidra: &DAT_0054f7f0
            e[0x40] = 0;
            e[0x0a] = 0x0a;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x12) = 0;

            *(int*)(e + 0x1c) = spout;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfc, 0, -1);

            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfb, 0, -1);

            *(int*)(e + 0x20) += 0x2000;
            *(short*)(e + 0x28) += 100;
            *(short*)(e + 0x2a) += 100;

            if (*(short*)(e + 0x28) >= 0x800) *(short*)(e + 0x28) = 0x800;
            if (*(short*)(e + 0x2a) >= 0x9c4) *(short*)(e + 0x2a) = 0x9c4;

            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) >= 0x3c)
            {
                // Le binaire rend la main ici : la partition ne joue pas a cette image.
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                return;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfb, 0, -1);

            short wave = *(short*)(e + 0x12);
            *(short*)(e + 0x12) += 0x50;
            *(int*)(e + 0x20) += (int)Winsys3d.GetSinTableEntry(wave) * 2;

            if (*(short*)(e + 0x12) > 0x2710) *(uint*)e &= 0x7fffffff;
        }

        GeyserScore(*(short*)(e + 0x98));
        (*(short*)(e + 0x98))++;
    }

    /// <summary>La partition du geyser : neuf notes a neuf images fixes, ecrites en toutes lettres
    /// dans le binaire. Les gains sont les memes partout (<c>0x1000</c> des deux cotes) ; seuls
    /// l'identifiant et la hauteur changent.</summary>
    private static void GeyserScore(short beat)
    {
        if (beat == 0) Aud.PlaySoundEffect(0x27, 0x200, 0x1000, 0x1000);
        if (beat == 1) Aud.PlaySoundEffect(0x27, 0x27f, 0x1000, 0x1000);
        if (beat == 2) Aud.PlaySoundEffect(0x27, 0x329, 0x1000, 0x1000);
        if (beat == 3) Aud.PlaySoundEffect(0x30, 0x200, 0x1000, 0x1000);

        if (beat == 0x28) Aud.PlaySoundEffect(0x27, 0x200, 0x1000, 0x1000);
        if (beat == 0x29) Aud.PlaySoundEffect(0x27, 0x2d4, 0x1000, 0x1000);
        if (beat == 0x2a) Aud.PlaySoundEffect(0x27, 0x37e, 0x1000, 0x1000);
        if (beat == 0x2b) Aud.PlaySoundEffect(0x30, 0x1bf, 0x1000, 0x1000);

        if (beat == 0x50) Aud.PlaySoundEffect(0x30, 0x27f, 0x1000, 0x1000);
    }

    /// <summary>Ghidra: <c>&amp;DAT_0054f840</c> — sept enregistrements jusqu'a
    /// <c>DAT_0054f878</c>, ou commence <see cref="s_Descriptors_0054F878"/>. Les quatre premiers
    /// sont des tuiles <c>0x18 x 0x18</c> en melange additif (<c>01 02</c>), les trois derniers
    /// les memes en opaque (<c>00 01</c>). <see cref="EntityHandler_004101D0"/> parcourt les
    /// enregistrements <b>1 a 3</b> : il pose l'indice a 1, pas a zero.</summary>
    private static readonly byte[] s_Descriptors_0054F840 =
    [
        0x48, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054f840
        0x60, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054f848
        0x78, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054f850
        0x90, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x0054f858
        0x00, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f860
        0x18, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f868
        0x30, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x0054f870
    ];

    private static byte* s_Descriptors0054F840Storage;

    private static byte* Descriptors0054F840()
        => NativeCopy(s_Descriptors_0054F840, ref s_Descriptors0054F840Storage);

    /// <summary>
    /// Ghidra: <c>0x004101d0</c> — <b>la braise qui derive</b>. Absent de <c>Blaze.c</c> ; releve
    /// au desassembleur, 138 instructions. Table <c>0x0056b2c8</c>, foret, sous-zones 6 a 8.
    ///
    /// <para>
    /// <b>Elle part sur la deuxieme image d'atlas, pas la premiere</b> : <c>+0x40</c> est pose a
    /// <c>1</c> a la naissance et l'entite s'eteint a <c>4</c>, donc trois tuiles sont vues sur
    /// les sept que porte la planche.
    /// </para>
    /// <para>
    /// <b>Sa vitesse est fixee une fois pour toutes.</b> La naissance tire une allure sur
    /// <c>[5, 9]</c>, la projette sur le cap <c>+0x32</c> par un cosinus et un sinus, ajoute a
    /// chacune un ecart de <c>[-2, +1]</c> unites entieres, et range le tout dans
    /// <c>+0x50</c>/<c>+0x58</c> ; la composante verticale <c>+0x54</c> est un simple tirage sur
    /// <c>[-5, +4]</c>. Ensuite les trois se contentent de s'additionner a la position — la braise
    /// va tout droit.
    /// </para>
    /// <para>
    /// <b>Le rythme d'image est tire lui aussi</b> : <c>+0x14</c> vaut <c>3</c> a <c>6</c>, et
    /// chaque fois que le compteur le depasse la tuile avance et l'echelle gagne <c>0x100</c> sur
    /// les deux axes. La braise grossit donc en vieillissant, de trois paliers.
    /// </para>
    /// <para>
    /// <b>Elle nait deja sombre et s'eteint vite</b> : la teinte part de
    /// <c>0xc8 / 0x78 / 0x78</c> — un rouge brique — et perd dix par canal a chaque image, y
    /// compris a l'image ou l'entite s'eteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_004101D0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F840();
            e[0x40] = 1;
            e[0x0a] = 0x0a;
            e[0x0b] = 0;

            short scale = (short)(SignedMask(CrtRand(), 0x1ff) + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            int pace = CrtRand() % 5;
            *(int*)(e + 0x20) += 0x46000;
            short speed = (short)(pace + 5);

            *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) << 0xc;

            short heading = *(short*)(e + 0x32);

            int alongZ = Winsys3d.GetCosTableEntry(heading) * speed;
            alongZ += (SignedMask(CrtRand(), 3) - 2) << 0xc;
            *(int*)(e + 0x58) = alongZ;

            int alongX = (int)Winsys3d.GetSinTableEntry(heading) * speed;
            alongX += (SignedMask(CrtRand(), 3) - 2) << 0xc;
            *(int*)(e + 0x50) = alongX;

            *(int*)(e + 0x54) = (CrtRand() % 10 - 5) << 0xc;
            *(short*)(e + 0x12) = 0;

            int hold = CrtRand();
            e[0x38] = 0xc8;
            *(short*)(e + 0x14) = (short)(SignedMask(hold, 3) + 3);
            e[0x39] = 0x78;
            e[0x3a] = 0x78;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);

        (*(short*)(e + 0x12))++;

        if (*(short*)(e + 0x12) > *(short*)(e + 0x14))
        {
            *(short*)(e + 0x28) += 0x100;
            *(short*)(e + 0x2a) += 0x100;

            byte frame = (byte)(e[0x40] + 1);
            *(short*)(e + 0x12) = 0;
            e[0x40] = frame;

            if (frame >= 4) *(uint*)e &= 0x7fffffff;
        }

        // La teinte descend meme a l'image ou l'entite vient de s'eteindre : le binaire ne saute
        // pas ce bloc.
        BrightenEntityTint_00404DE0(e, -10, -10, -10);
    }

    /// <summary>
    /// Ghidra: <c>0x00413330</c> — <b>le stroboscope rouge accroche a son porteur</b>. Absent de
    /// <c>Blaze.c</c> ; releve au desassembleur, 116 instructions. Table <c>0x0056b0c8</c>, foret,
    /// sous-zones 5 et 9.
    ///
    /// <para>
    /// <b>Il colle a son porteur et meurt avec lui.</b> Le champ <c>+0x6c</c> est relu a chaque
    /// image : nul, le corps rend la main sans rien faire ; vivant mais portant
    /// <c>0x40000000</c>, ou simplement mort, le stroboscope s'eteint — et <b>rend l'ecran a sa
    /// couleur</b> si <c>+0x9a</c> vaut 0 ou 1, ce qui evite de laisser un voile rouge en place.
    /// </para>
    /// <para>
    /// <b>Il ne dessine rien : il pousse le voile.</b> Une image sur deux le canal rouge du voile
    /// plein cadre monte a <c>0x1e</c>, l'autre il retombe a zero ; le vert et le bleu restent a
    /// zero en permanence. Trente images de ce regime, puis l'entite s'eteint.
    /// </para>
    /// <para>
    /// <b>Onze invocations d'un coup a la naissance</b>, sous-type <c>0xde</c>, plus une aux
    /// images 0, 3 et 7 — celles-la precedees d'un <c>+0x16</c> qui vaut 0 la premiere fois et 1
    /// ensuite, de sorte que la premiere naissance se distingue des deux autres.
    /// </para>
    /// <para>
    /// <b>Un bloc mort, conserve en commentaire.</b> Juste avant l'extinction, le binaire lit les
    /// quatre fiches d'emplacement et range chaque mot de drapeaux dans un emplacement de pile que
    /// personne ne relit — l'emplacement de l'argument, dont la valeur est deja dans un registre.
    /// La boucle n'a aucun effet et n'est pas transcrite ; elle est signalee ici pour qu'une
    /// relecture du desassemblage ne la prenne pas pour un oubli.
    /// </para>
    /// </summary>
    private static void EntityHandler_00413330(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) return;

        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);

        uint carried = *(uint*)carrier;

        if ((carried & 0x40000000) != 0 || (carried & 0x80000000) == 0)
        {
            *(uint*)e &= 0x7fffffff;

            short mode = *(short*)(e + 0x9a);
            if (mode == 0 || mode == 1) Mem.s_Unknown_005de024 = 0x01000000;

            return;
        }

        if (*(short*)(e + 0x9a) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;

            TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x2c], 0x300, 0x2a00, 0, 0);

            *(int*)(e + 0x5c) = 1;
            *(int*)(e + 0x44) = SignedMask(CrtRand(), 1);

            for (int i = 0x0b; i != 0; i--) SpawnCastEntity_0047AAE0(e, 0xde, 0, -1);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short beat = *(short*)(e + 0x12);

        if (beat == 0 || beat == 3 || beat == 7)
        {
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x16) = (short)(beat == 0 ? 0 : 1);
            SpawnCastEntity_0047AAE0(e, 0xde, 0, -1);
        }

        (*(short*)(e + 0x12))++;

        // Le voile plein cadre : rouge a 0x1e une image sur deux, vert et bleu toujours nuls.
        Mem.s_Unknown_005de024 = (*(short*)(e + 0x12) & 1) != 0 ? 0x0100001eu : 0x01000000u;

        if (*(short*)(e + 0x12) < 0x1e) return;

        // Ghidra: ici la boucle morte sur les quatre fiches d'emplacement. Voir le resume.
        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00474ca0 @ 0x00474ca0 — <b>l'aiguillage des deux sondes de melee</b>. Decompilee
    /// dans <c>Blaze.c</c>, mais <b>Ghidra y a perdu les arguments</b> : la pseudo-C rend
    /// <c>FUN_00519af0()</c> sans aucun. Le desassemblage montre que les <b>quatre</b> arguments
    /// sont empiles une seule fois, avant le branchement, et servent aux deux appels.
    ///
    /// <para>Le bit <c>0x800000</c> de l'entite decide du camp : pose, elle balaie les entites de
    /// scene ; absent, elle balaie le groupe. C'est le meme geste vu des deux cotes.</para>
    /// </summary>
    private static byte* ProbeMeleeTarget_00474CA0(byte* entity, short* probe)
    {
        if ((*(uint*)entity & 0x800000) != 0)
        {
            return SweepSceneEntitiesForMeleeHit_00519AF0(
                entity, (int*)(entity + 0x1c), probe, 0x800);
        }

        return SweepPartyForMeleeHit_00519C30(entity, (int*)(entity + 0x1c), probe, 0x800);
    }

    /// <summary>
    /// Ghidra: <c>0x00483760</c> — <b>la charge qui se materialise, frappe, puis s'efface</b>.
    /// Absent de <c>Blaze.c</c> ; releve au desassembleur, 140 instructions. Table
    /// <c>0x0056b1c8</c>, foret, sous-zones 5 et 9.
    ///
    /// <para>
    /// <b>Il apparait, frappe une fois, disparait.</b> La teinte <c>+0x38</c> monte de
    /// <c>0x80808</c> par image — les trois canaux d'un coup, le champ etant lu en <c>dword</c> —
    /// jusqu'a <c>0x808080</c>, c'est-a-dire le blanc plein de la convention winsys3d. L'image ou
    /// elle y arrive est celle du coup ; ensuite <c>+0x10</c> depasse <c>0x14</c> et la teinte
    /// redescend du meme pas jusqu'a zero, ou l'entite s'eteint en effacant <b>tout</b> son mot de
    /// drapeaux.
    /// </para>
    /// <para>
    /// <b>Le coup passe par la sonde qui vise le groupe</b> (<see cref="ProbeMeleeTarget_00474CA0"/>)
    /// avec une boite de <c>(-0x400, -50, 50, 200, 0, 300)</c> ecrite sur la pile. Il n'est porte
    /// que si <c>DAT_005de0b8</c> est revenu a 1 — donc pas si la cible a pare. Le coup marque la
    /// victime de <c>+0x96 = 8</c> et du bit <c>0x40</c>, et l'attaquant est
    /// <c>+0x74</c> — le lanceur — et non l'entite elle-meme.
    /// </para>
    /// <para>
    /// <b>L'echelle verticale converge au lieu de croitre</b> : <c>+0x2a</c> gagne le quart de ce
    /// qui lui manque pour atteindre <c>0x3000</c>. Elle s'en approche sans jamais l'atteindre,
    /// ce qui donne un etirement qui ralentit.
    /// </para>
    /// <para>
    /// <b>L'etat 4 est une queue de projectiles.</b> Tant que <c>+0x12</c> n'est pas epuise, le
    /// corps se deplace de <c>200</c> unites le long de <c>cap - 0x400</c>, lache <b>deux</b>
    /// invocations dont le cap est ecarte de <c>±0x1ff</c> — la premiere vers la droite, la seconde
    /// vers la gauche, le signe venant du sens de la boucle — puis <b>remet la position et le cap
    /// exactement comme il les a trouves</b>. Le deplacement n'existe que le temps des deux
    /// naissances.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483760(byte* e)
    {
        short* box = stackalloc short[6];
        box[0] = -0x400;
        box[1] = -50;
        box[2] = 50;
        box[3] = 200;
        box[4] = 0;
        box[5] = 300;

        // ------------------------------------------------------------- l'etat 4 : la queue
        if (*(short*)(e + 0x10) == 4 && *(short*)(e + 0x12) != 0)
        {
            short heading = *(short*)(e + 0x32);
            int keepY = *(int*)(e + 0x20);
            int keepZ = *(int*)(e + 0x24);

            (*(short*)(e + 0x12))--;

            int course = heading - 0x400;
            int keepX = *(int*)(e + 0x1c);

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(course) * 200;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(course) * 200;

            short axis = *(short*)(e + 0x32);

            for (int i = 2; i != 0; i--)
            {
                int spread = CrtRand() & 0x1ff;
                if (i != 2) spread = -spread;

                *(short*)(e + 0x32) = (short)(axis + spread);

                byte* table = *(byte**)(s_Unknown_005e97cc + 0xd0);
                byte subtype = (byte)(**(byte**)table + 0xc0);

                SpawnCastEntity_0047AAE0(e, subtype, 1, 1);
            }

            *(int*)(e + 0x1c) = keepX;
            *(int*)(e + 0x20) = keepY;
            *(int*)(e + 0x24) = keepZ;
            *(short*)(e + 0x32) = heading;
        }

        uint tint = *(uint*)(e + 0x38);

        // ------------------------------------------------------------- l'effacement
        if (*(short*)(e + 0x10) >= 0x14)
        {
            if (tint != 0)
            {
                tint -= 0x80808;
                *(uint*)(e + 0x38) = tint;

                // Ghidra ecrit `*param_1 = tint` : la valeur vaut zero a cet endroit, donc tout
                // le mot de drapeaux part avec, 0x80000000 compris.
                if (tint == 0) *(uint*)e = tint;
            }

            (*(short*)(e + 0x10))++;
            return;
        }

        // ------------------------------------------------------------- l'apparition
        if (tint < 0x808080)
        {
            *(uint*)(e + 0x38) = tint + 0x80808;
            (*(short*)(e + 0x10))++;
            return;
        }

        // ------------------------------------------------------------- le coup, une seule image
        short height = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)(height + ((0x3000 - height) >> 2));

        byte* victim = ProbeMeleeTarget_00474CA0(e, box);

        if (*Mem.Camera(0x005de0b8) == 0)
        {
            (*(short*)(e + 0x10))++;
            return;
        }

        ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);

        *(short*)(victim + 0x96) = 8;
        *(uint*)victim |= 0x40;

        Aud.PlayPositionalSound(3, 0x400, 0x1800, (int*)(e + 0x1c));

        (*(short*)(e + 0x10))++;
    }

    /// <summary>
    /// Ghidra: <c>0x00459840</c> — <b>le debris qui retombe en clignotant</b>. Absent de
    /// <c>Blaze.c</c> ; releve au desassembleur, 158 instructions. Table <c>0x0056adc8</c>, foret,
    /// sous-zones 6 a 8.
    ///
    /// <para>
    /// <b>Il ne fait rien tant que le drapeau de script 216 n'est pas leve.</b> Le test est la
    /// toute premiere instruction, avant meme la lecture de l'argument : sans le drapeau le corps
    /// rend la main sans rien toucher, pas meme son etat.
    /// </para>
    /// <para>
    /// <b>Une trajectoire balistique tiree une fois pour toutes.</b> La naissance tire une derive
    /// horizontale sur <c>[-8, +7]</c> unites entieres dans les deux axes, une poussee verticale
    /// sur <c>[5, 19]</c>, et deux vitesses de rotation sur <c>[0, 29]</c> — l'une pour le cap
    /// <c>+0x32</c>, l'autre pour le roulis <c>+0x34</c>. Ensuite la composante verticale
    /// <b>perd <c>0x1000</c> par image</b> : c'est la seule gravite du corps, et elle suffit a le
    /// faire retomber.
    /// </para>
    /// <para>
    /// <b>Seize naissances a la premiere image</b>, sous-type <c>0x31</c>, par la sœur de la sonde
    /// sonore qui ne fait naitre que si la camera est assez proche.
    /// </para>
    /// <para>
    /// <b>Cinq echos, un toutes les trois images.</b> Le binaire n'ecrit pas cinq comparaisons mais
    /// une <b>table de sauts</b> : treize octets d'indices a <c>0x00459a58</c> pointant six entrees
    /// a <c>0x00459a40</c>, indexes par <c>+0x98</c> et bornes a <c>0x0c</c>. Les indices valent
    /// <c>0, 5, 5, 1, 5, 5, 2, 5, 5, 3, 5, 5, 4</c> — donc les images 0, 3, 6, 9 et 12 jouent le
    /// meme son <c>sons[0x1c]</c> avec un gain qui <b>decroit</b> : <c>0x1800</c>, <c>0x1000</c>,
    /// <c>0x0b00</c>, <c>0x0800</c>, <c>0x0400</c>. Toutes les autres tombent sur l'entree 5, qui
    /// ne fait rien. C'est un echo qui s'eteint, ecrit en donnee plutot qu'en code.
    /// </para>
    /// <para>
    /// <b>Il clignote a chaque image.</b> Le bit <c>0x4000</c> suit la parite du compteur de vie
    /// <c>+0x5c</c> : pose quand il est pair, retire quand il est impair. Le debris apparait donc
    /// une image sur deux pendant toute sa chute.
    /// </para>
    /// <para>
    /// <b>Deux arguments de trop.</b> Le site de naissance empile <b>six</b> arguments la ou
    /// <see cref="SpawnEffectNearCamera_00468110"/> n'en lit que quatre — le desassemblage donne
    /// <c>add esp,18h</c> apres l'appel. Les deux derniers zeros ne sont jamais lus ; ils ne sont
    /// pas transcrits, et le fait est note ici pour qu'une relecture ne les cherche pas.
    /// </para>
    /// </summary>
    private static void EntityHandler_00459840(byte* e)
    {
        if (!ScriptFlagSet(0xd8)) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            *(int*)(e + 0x50) = (SignedMask(CrtRand(), 0x0f) - 8) << 0xc;
            *(int*)(e + 0x58) = (SignedMask(CrtRand(), 0x0f) - 8) << 0xc;
            *(int*)(e + 0x54) = (CrtRand() % 15 + 5) << 0xc;

            *(short*)(e + 0x12) = (short)(CrtRand() % 30);
            *(short*)(e + 0x14) = (short)(CrtRand() % 30);

            int life = CrtRand();
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x16) = (short)(life % 10 + 0x14);
            *(uint*)e |= 0x2000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        if (*(short*)(e + 0x98) == 0)
        {
            for (int i = 0x10; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);
        }

        DebrisEcho(e, *(short*)(e + 0x98));

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        (*(short*)(e + 0x98))++;

        *(int*)(e + 0x20) += *(int*)(e + 0x54);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);
        *(int*)(e + 0x54) -= 0x1000;

        *(short*)(e + 0x32) += *(short*)(e + 0x12);
        *(short*)(e + 0x34) += *(short*)(e + 0x14);

        int age = *(int*)(e + 0x5c) + 1;
        *(int*)(e + 0x5c) = age;

        if (age > *(short*)(e + 0x16)) *(uint*)e &= 0x7fffffff;

        // Ghidra: `test al,1` porte sur l'octet bas du compteur de vie qui vient d'etre range,
        // pas sur +0x14 comme la lecture precedente le laisserait croire.
        if ((age & 1) == 0) *(uint*)e |= 0x4000;
        else *(uint*)e &= 0xffffbfff;
    }

    /// <summary>
    /// L'echo du debris : le meme son a un gain qui decroit, une image sur trois. La table de
    /// sauts du binaire est relevee ici sous forme de <c>switch</c> — c'est ce qu'elle calcule, et
    /// ses treize octets d'indices n'encodent rien de plus.
    ///
    /// <para>Les identifiants sortent du repertoire de la sous-zone, <c>sons[0x1c]</c> pour les
    /// cinq. Seul le gain change.</para>
    /// </summary>
    private static void DebrisEcho(byte* e, short beat)
    {
        short gain = beat switch
        {
            0 => 0x1800,
            3 => 0x1000,
            6 => 0x0b00,
            9 => 0x0800,
            12 => 0x0400,
            _ => 0,
        };

        if (gain == 0) return;

        TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x1c], 0x400, gain, 0, 0);
    }

    /// <summary>
    /// Ghidra: <c>0x00459fa0</c> — <b>le pilier tournant a quatre faces</b>. Absent de
    /// <c>Blaze.c</c> ; releve au desassembleur, 177 instructions. Table <c>0x0056adc8</c>, foret,
    /// sous-zones 6 a 8.
    ///
    /// <para>
    /// <b>Son orientation vit dans un drapeau de script, sur deux bits.</b> L'octet
    /// <c>0x005e96fd</c> — les drapeaux 232 a 239 de la banque 0 — porte <b>quatre</b> champs de
    /// deux bits, un par pilier ; <c>+0x14</c> dit lequel. C'est pour cela que l'orientation
    /// survit a une sauvegarde : elle n'est pas dans l'entite, elle est dans l'etat de la partie.
    /// </para>
    /// <para>
    /// <b>Le codage est une bijection avec quatre caps.</b> <c>00</c> vaut <c>0xa00</c>,
    /// <c>01</c> vaut <c>0x600</c>, <c>10</c> vaut <c>0x200</c>, <c>11</c> vaut <c>0xe00</c> —
    /// quatre quarts de tour, et l'ecriture de fin de rotation est exactement l'inverse de la
    /// lecture de l'etat 0. Rien d'autre ne fait le lien entre les deux.
    /// </para>
    /// <para>
    /// <b>Trois etats, dont un qui rend la main tout de suite.</b> L'etat 0 relit le drapeau, pose
    /// le cap et <b>sort</b> — le binaire re-teste l'etat pour ca, alors qu'il ne peut pas avoir
    /// change. L'etat 1 arme le quart de tour : pas de <c>0x20</c>, cible a <c>cap + 0x400</c>.
    /// L'etat 2 tourne, joue <c>sons[0x1e]</c> une image sur quatre, et s'arrete des que la cible
    /// est <b>depassee</b> — le cap est alors recale pile sur elle, ramene sous <c>0x1000</c> si
    /// besoin, l'orientation est reecrite dans le drapeau, et l'etat retombe a 0.
    /// </para>
    /// <para>
    /// <b>La rotation efface le drapeau 217.</b> Un seul bit, retire a chaque quart de tour
    /// acheve : c'est ce qui dit au reste de la zone que la combinaison des piliers vient de
    /// changer. Rien ici ne le repose.
    /// </para>
    /// <para>
    /// <b>Le binaire ecrit les deux bits separement</b>, en quatre blocs de deux operations
    /// (poser ou retirer le bit bas, puis le bit haut) plutot qu'en un masque. Le resultat est le
    /// meme et il est ecrit ici en une affectation, parce que c'est ce que les huit instructions
    /// calculent.
    /// </para>
    /// </summary>
    private static void EntityHandler_00459FA0(byte* e)
    {
        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            short index = *(short*)(e + 0x14);
            byte packed = Mem.s_StateBank0_005e96e0[0x1d];

            if (index >= 0 && index <= 3)
            {
                int facing = (packed >> (index * 2)) & 3;

                *(short*)(e + 0x32) = facing switch
                {
                    0 => 0x0a00,
                    1 => 0x0600,
                    2 => 0x0200,
                    _ => 0x0e00,
                };
            }

            // Ghidra re-teste l'etat ici, alors que rien n'a pu le changer : l'etat 0 rend
            // toujours la main a cet endroit.
            return;
        }

        if (state == 1)
        {
            *(short*)(e + 0x12) = 0x20;
            *(short*)(e + 0x16) = *(short*)(e + 0x32);
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x98))++;

        if (*(short*)(e + 0x98) == 4)
        {
            TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x1e], 0x400, 0x1000, 0, 0);
            *(short*)(e + 0x98) = 0;
        }

        *(short*)(e + 0x32) += *(short*)(e + 0x12);

        if (*(short*)(e + 0x32) <= *(short*)(e + 0x16) + 0x400) return;

        // Le quart de tour est fait : le drapeau 217 tombe, et le cap est recale pile.
        Mem.s_StateBank0_005e96e0[0x1b] &= 0xfd;

        short landed = (short)(*(short*)(e + 0x16) + 0x400);
        *(short*)(e + 0x10) = 0;
        *(short*)(e + 0x32) = landed;

        if (landed >= 0x1000)
        {
            landed = (short)(landed - 0x1000);
            *(short*)(e + 0x32) = landed;
        }

        int code = landed switch
        {
            0x0a00 => 0,
            0x0600 => 1,
            0x0200 => 2,
            0x0e00 => 3,
            _ => -1,
        };

        if (code < 0) return;

        int shift = *(short*)(e + 0x14) * 2;
        byte bank = Mem.s_StateBank0_005e96e0[0x1d];

        Mem.s_StateBank0_005e96e0[0x1d] =
            (byte)((bank & ~(3 << shift)) | (code << shift));
    }

    /// <summary>
    /// Le corps commun des trois eclats de corde.
    ///
    /// <para>
    /// <b>Il se pose sur la corde d'un arc, pas sur l'arc.</b> La naissance tire un cas parmi six
    /// (<c>rand % 6</c>, ou <c>rand &amp; 7</c> signe si <c>+0x12</c> valait 2), et chaque cas
    /// designe <b>deux directions</b> prises autour du cap <c>+0x32</c> — a <c>±0x2bc</c> et
    /// <c>±0x514</c>, soit environ 61 et 114 degres de part et d'autre. Chacune donne un point a
    /// <c>250</c> unites du centre, brouille de <c>±10</c> unites entieres. L'eclat ne se pose pas
    /// sur l'un des deux : il se pose <b>quelque part sur le segment qui les joint</b>, a la
    /// fraction <c>(tirage + 1) / diviseur</c> — le diviseur valant 4 ou 8 selon le cas, et le
    /// tirage pouvant etre negatif, de sorte que le point deborde parfois du segment.
    /// </para>
    /// <para>
    /// <b>La hauteur descend d'un cran fixe.</b> <c>-0xa000</c>, moins un tirage sur vingt unites
    /// entieres : l'eclat nait toujours sous son origine.
    /// </para>
    /// <para>
    /// <b>Trois tailles, choisies par <c>+0x12</c> a l'entree.</b> 0 laisse l'echelle nominale, 1
    /// l'agrandit de <c>0x400</c>, 2 la reduit de <c>0x320</c> ; ces trois cas ecrivent <c>+0x9a</c>,
    /// les autres le laissent tel quel. La vitesse de rotation <c>+0x12</c> est ensuite refaite —
    /// <c>(rand % 5 + 8)</c>, doublee sauf pour les tailles 1 et 2.
    /// </para>
    /// <para>
    /// <b>Deux etats, et un clignotement.</b> L'etat 1 rallume le bit <c>0x4000</c> a chaque image
    /// puis l'eteint passe la quatrieme si la taille est 0 ou 1 ; il bascule vers l'etat 2 apres
    /// huit images, ou plus tot une fois sur dix, en descendant encore de <c>0xf000</c>. L'etat 2
    /// derive au hasard de <c>±10</c> unites par image et fait avancer l'indice d'atlas tous les
    /// quatre pas ; l'entite s'eteint a la quatrieme tuile, donc apres seize images.
    /// </para>
    /// <para>
    /// <b>Une seule ecriture s'ecarte du binaire, et elle est signalee.</b> Quand le cas tire ne
    /// tombe dans aucun des quatre blocs — possible seulement si <c>+0x12</c> valait 2 et que le
    /// tirage est negatif — le binaire recopie le cas dans trois des quatre valeurs et laisse la
    /// <b>quatrieme sur une case de pile jamais ecrite</b>. Elle est mise a zero ici : la valeur
    /// d'origine n'existe pas, elle depend de ce que l'appelant avait laisse. Aucune division par
    /// zero n'en decoule, le cas 0 etant traite par le premier bloc.
    /// </para>
    /// </summary>
    private static void TickArcChordFlash(byte* e, byte* atlas)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x2e) = 0x14;
            *(byte**)(e + 0x3c) = atlas;

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x258 + 0x1770);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x258 + 0x1770);

            short pick = *(short*)(e + 0x12) == 2
                ? (short)SignedMask(CrtRand(), 7)
                : (short)(CrtRand() % 6);

            short heading = *(short*)(e + 0x32);

            int ax, bz, cx, dz;
            int spread, divisor;

            if (pick == 0 || pick == 4)
            {
                ax = ArcChordPoint(heading + 0x2bc, sine: true);
                bz = ArcChordPoint(heading + 0x2bc, sine: false);
                cx = ArcChordPoint(heading - 0x2bc, sine: true);
                dz = ArcChordPoint(heading - 0x2bc, sine: false);
                spread = SignedMask(CrtRand(), 7);
                divisor = 8;
            }
            else if (pick == 1 || (*(short*)(e + 0x12) == 2 && pick >= 6))
            {
                ax = ArcChordPoint(heading - 0x2bc, sine: true);
                bz = ArcChordPoint(heading - 0x2bc, sine: false);
                cx = ArcChordPoint(heading - 0x514, sine: true);
                dz = ArcChordPoint(heading - 0x514, sine: false);
                spread = SignedMask(CrtRand(), 3);
                divisor = 4;
            }
            else if (pick == 2 || pick == 5)
            {
                ax = ArcChordPoint(heading - 0x514, sine: true);
                bz = ArcChordPoint(heading - 0x514, sine: false);
                cx = ArcChordPoint(heading + 0x514, sine: true);
                dz = ArcChordPoint(heading + 0x514, sine: false);
                spread = SignedMask(CrtRand(), 7);
                divisor = 8;
            }
            else if (pick == 3)
            {
                ax = ArcChordPoint(heading + 0x514, sine: true);
                bz = ArcChordPoint(heading + 0x514, sine: false);
                cx = ArcChordPoint(heading + 0x2bc, sine: true);
                dz = ArcChordPoint(heading + 0x2bc, sine: false);
                spread = SignedMask(CrtRand(), 3);
                divisor = 4;
            }
            else
            {
                // Ghidra: `edi = ebp = ebx = edx = eax = pick`, et la quatrieme valeur reste sur sa
                // case de pile. Voir le resume : elle vaut zero ici, faute de valeur d'origine.
                ax = pick;
                bz = pick;
                cx = 0;
                dz = pick;
                spread = pick;
                divisor = pick;
            }

            int steps = (short)spread + 1;
            short arc = (short)divisor;

            ax >>= 0xc;
            bz >>= 0xc;
            cx >>= 0xc;
            dz >>= 0xc;

            *(int*)(e + 0x1c) += (ax - (ax - cx) / arc * steps) << 0xc;
            *(int*)(e + 0x24) += (bz - (bz - dz) / arc * steps) << 0xc;

            int drop = CrtRand() % 0x14;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(int*)(e + 0x20) += unchecked((int)0xffff6000) - (drop << 0xc);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            short size = *(short*)(e + 0x12);

            if (size == 0) *(short*)(e + 0x9a) = 0;

            if (size == 1)
            {
                *(short*)(e + 0x9a) = 1;
                *(short*)(e + 0x28) += 0x400;
                *(short*)(e + 0x2a) += 0x400;
            }

            if (size == 2)
            {
                *(short*)(e + 0x9a) = 2;
                *(short*)(e + 0x28) += -0x320;
                *(short*)(e + 0x2a) += -0x320;
            }

            short kept = *(short*)(e + 0x9a);
            int multiplier = kept == 1 || kept == 2 ? 1 : 2;

            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)((CrtRand() % 5 + 8) * multiplier);
        }

        // ------------------------------------------------------------- l'etat 1, le clignotement
        if (*(short*)(e + 0x10) == 1)
        {
            *(uint*)e |= 0x4000;
            (*(short*)(e + 0x14))++;

            if (*(short*)(e + 0x14) > 3)
            {
                short size = *(short*)(e + 0x9a);
                if (size == 0 || size == 1) *(uint*)e &= 0xffffbfff;
            }

            if (*(short*)(e + 0x14) > 8 || CrtRand() % 10 == 0)
            {
                *(uint*)e &= 0xffffbfff;
                *(int*)(e + 0x20) += unchecked((int)0xffff1000);
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x14) = 0;
            }
        }

        // ------------------------------------------------------------- l'etat 2, la derive
        if (*(short*)(e + 0x10) != 2) return;

        *(int*)(e + 0x1c) += (CrtRand() % 0x14 - 10) << 0xc;
        *(int*)(e + 0x24) += (CrtRand() % 0x14 - 10) << 0xc;

        (*(short*)(e + 0x16))++;
        byte frame = (byte)(short)(*(short*)(e + 0x16) >> 2);
        e[0x40] = frame;

        if (frame == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Un des deux points de la corde : le sinus ou le cosinus du cap donne, porte a <c>250</c>
    /// unites, plus un brouillage de <c>±10</c> unites entieres.
    ///
    /// <para>Le binaire ecrit ce calcul <b>huit fois</b> — quatre blocs de deux points — a
    /// l'identique sauf le decalage de cap et le choix sinus/cosinus. Le facteur <c>250</c> y est
    /// une cascade de trois <c>lea eax,[eax+eax*4]</c> suivie d'un <c>lea .,[edx+.*2]</c> :
    /// <c>125</c> puis le doublement du <c>lea</c>.</para>
    /// </summary>
    private static int ArcChordPoint(int course, bool sine)
    {
        int table = sine ? (int)Winsys3d.GetSinTableEntry(course) : Winsys3d.GetCosTableEntry(course);
        int jitter = (CrtRand() % 0x14 - 10) << 0xc;
        return jitter + table * 125 * 2;
    }


    /// <summary>
    /// La position d'une des deux spirales : le rayon <c>+0x9a</c> autour de l'ancre figee a la
    /// naissance (<c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>), et une hauteur qui bat au sinus d'une
    /// phase propre <c>+0x12</c> ajoutee au cap.
    ///
    /// <para><paramref name="sway"/> vaut <c>0x14</c> pour la spirale rentrante et <c>0x28</c>
    /// pour la sortante : le seul nombre qui les separe ici.</para>
    /// </summary>
    private static void SpiralAroundAnchor(byte* e, int sway)
    {
        short course = *(short*)(e + 0x32);
        short radius = *(short*)(e + 0x9a);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(course) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(course) * radius + *(int*)(e + 0x58);
        *(int*)(e + 0x20) = *(int*)(e + 0x54)
                          + (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12) + course) * sway;
    }

    /// <summary>
    /// Les naissances du chef : <paramref name="count"/> enfants de la variante
    /// <paramref name="variant"/>.
    ///
    /// <para>Le binaire pose <c>+0x14</c> a la variante voulue <b>avant</b> chaque naissance et le
    /// restaure apres — c'est ainsi que l'enfant recoit sa variante, puisque
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> recopie l'entite entiere. Le sous-type, lui,
    /// vient de <c>+0x98</c> plus <c>0xc0</c>.</para>
    /// </summary>
    private static void SpawnSummonChildren(byte* e, short variant, int count)
    {
        short saved = *(short*)(e + 0x14);

        for (int i = 0; i < count; i++)
        {
            *(short*)(e + 0x14) = variant;
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), null, (byte)(*(short*)(e + 0x98) + 0xc0), 0, -1);
            *(short*)(e + 0x14) = saved;
        }
    }

    /// <summary>Le son du tourniquet, <c>sons[0x14]</c>. Le binaire l'ecrit quatre fois.</summary>
    private static void TurnstileSound(byte* e)
        => TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x14], 0x400, 0x1000, 0, 0);

    /// <summary>
    /// Une image de rotation : la vitesse <c>+0x5c</c> gagne un huitieme d'elle-meme pendant les
    /// douze premieres images, en perd un huitieme ensuite, et le cap avance d'autant. Rend
    /// <c>true</c> a la quarantieme image, quand le tour est fait.
    ///
    /// <para>Le binaire ecrit ce bloc <b>trois fois</b> — phases 1, 0xc9 et 0x15 —, a l'identique
    /// sauf la valeur laissee dans <c>+0x16</c>. Le cap n'avance PAS a l'image de fin : le
    /// binaire rend la main avant.</para>
    /// </summary>
    private static bool TurnstileSpin(byte* e)
    {
        int speed = *(int*)(e + 0x5c);

        *(int*)(e + 0x5c) = *(short*)(e + 0x12) < 0x0d
            ? (speed >> 3) + speed
            : speed - (speed >> 3);

        short frame = (short)(*(short*)(e + 0x12) + 1);
        *(short*)(e + 0x12) = frame;

        if (frame == 0x28) return true;

        *(short*)(e + 0x32) += (short)*(int*)(e + 0x5c);
        return false;
    }

    /// <summary>Une image de course d'ascenseur : le son toutes les cinq images, le compteur, et le
    /// pas vertical. Le binaire ecrit ce bloc deux fois, a la montee et a la descente, au signe du
    /// pas pres.</summary>
    private static void TickLiftTravel(byte* e, int step)
    {
        (*(short*)(e + 0x98))++;

        if (*(short*)(e + 0x98) == 5)
        {
            *(short*)(e + 0x98) = 0;
            TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x17], 0x400, 0x1000, 0, 0);
            *(int*)(e + 0x5c) = 1;
        }

        (*(short*)(e + 0x12))++;
        *(int*)(e + 0x20) += step;
    }

    /// <summary>Le meme son que la course, un ton plus grave, joue une seule fois a l'arret.</summary>
    private static void LiftArrivalSound(byte* e)
        => TryPlaySoundNearEntity((uint*)e, SubAreaSounds()[0x17], 0x200, 0x1000, 0, 0);

    /// <summary>Les trois echelles de l'etincelle de <see cref="EntityHandler_004134C0"/>, qui
    /// gagnent 100 par image dans ses deux etats vivants. Le binaire ecrit le bloc deux fois.</summary>
    private static void GrowSparkScales(byte* e)
    {
        *(short*)(e + 0x28) += 100;
        *(short*)(e + 0x2a) += 100;
        *(short*)(e + 0x2c) += 100;
    }

    /// <summary>Ghidra: <c>&amp;DAT_0054f758</c> — quatre enregistrements jusqu'a
    /// <c>DAT_0054f778</c>, quatre tuiles de <c>8 x 8</c> aux coins d'un carre de <c>0x10</c>. Le
    /// compte se lit dans le corps : <see cref="EntityHandler_0040F650"/> tire son indice a
    /// <c>rand &amp; 3</c>. Le quatrieme a une <b>hauteur de 7</b> et non 8 — un pixel de moins,
    /// releve tel quel.</summary>
    private static readonly byte[] s_Descriptors_0054F758 =
    [
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f758
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f760
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f768
        0x10, 0x10, 0x08, 0x07, 0x01, 0x03, 0x10, 0x00,   // 0x0054f770
    ];

    private static byte* s_Descriptors0054F758Storage;

    private static byte* Descriptors0054F758()
        => NativeCopy(s_Descriptors_0054F758, ref s_Descriptors0054F758Storage);

    /// <summary>
    /// Ghidra: FUN_0040f650 @ 0x0040f650 — <b>la gerbe reculee</b>. Table <c>0x0056b2c8</c>, foret.
    /// Decompilee dans <c>Blaze.c</c>, mais avec un <b>appel dont Ghidra a jete le resultat</b> :
    /// le relevé au desassembleur est garde sous <c>Legacy/Raw/Blaze/FUN_0040F650.asm</c>.
    ///
    /// <para>
    /// <b>Ce que la pseudo-C a invente.</b> Elle rend quatre appels dont le deuxieme n'a pas
    /// d'affectation :
    /// </para>
    /// <code>
    /// iVar3 = Ordinal_451(cap + 0x400);
    /// Ordinal_450(cap + 0x400);          /* resultat jete */
    /// </code>
    /// <para>
    /// et remplace plus bas cette valeur perdue par <c>(int)(param_1 + iVar5 * -0x28)</c> — une
    /// arithmetique de pointeur sur le parametre, qui n'a aucun sens ici. Le desassembleur montre
    /// que le cosinus est range dans un mot de pile (<c>[esp+0x14]</c>) et relu vingt lignes plus
    /// bas : la ligne du Z est <b>le miroir exact</b> de celle du X. Transcrire la pseudo-C telle
    /// quelle aurait produit un deplacement faux qu'aucune compilation n'aurait signale.
    /// </para>
    /// <para>
    /// <b>Ce qu'elle fait.</b> La naissance recule l'entite d'un demi-pas dans la direction
    /// <c>cap + 0xc00</c> (<c>0xa0</c> fois sinus et cosinus), puis la decale d'un huitieme de la
    /// difference entre les directions <c>cap + 0x400</c> et <c>cap + 0xc00</c>, multipliee par un
    /// tirage de <c>0 a 7</c>. Les deux quarts de tour de part et d'autre du cap : la gerbe s'ouvre
    /// donc perpendiculairement a ce que regarde son porteur, d'une largeur tiree au hasard.
    /// </para>
    /// <para>
    /// <b>Trois etats, pas deux.</b> L'etat 1 est une <b>attente</b> de <c>rand % 5</c> images,
    /// pendant laquelle le bit <c>0x4000</c> est pose ; a son terme l'etat passe a 2 et le bit
    /// tombe. L'etat 2 change d'image d'atlas a chaque image et compte : il meurt a la vingtieme,
    /// <b>ou</b> avant, sur une chance sur vingt par image. Deux portes, comme
    /// <see cref="EntityHandler_0040F470"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040F650(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x2e) = 0x14;
            *(byte**)(e + 0x3c) = Descriptors0054F758();   // Ghidra: &DAT_0054f758

            *(short*)(e + 0x28) = (short)(CrtRand() % 600 + 7000);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 600 + 7000);

            // Ghidra: `lea ebx,[eax+eax*4] ; shl ebx,5` — les quatre produits valent 0xa0 fois la
            // table. Le second, le cosinus de cap+0x400, est celui que la pseudo-C a perdu.
            int course = *(short*)(e + 0x32);
            int sinNear = (int)Winsys3d.GetSinTableEntry(course + 0x400) * 0xa0;
            int cosNear = Winsys3d.GetCosTableEntry(course + 0x400) * 0xa0;
            int sinFar = (int)Winsys3d.GetSinTableEntry(course + 0xc00) * 0xa0;
            int cosFar = Winsys3d.GetCosTableEntry(course + 0xc00) * 0xa0;

            *(int*)(e + 0x1c) -= sinFar;
            *(int*)(e + 0x24) -= cosFar;

            int spread = SignedMask(CrtRand(), 7);
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(int*)(e + 0x1c) += -DivideTowardZeroBy8(sinNear - sinFar) * spread;
            *(int*)(e + 0x24) += -DivideTowardZeroBy8(cosNear - cosFar) * spread;

            int wait = CrtRand();
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            e[0x40] = 0;
            *(uint*)e |= 0x4000;
            *(short*)(e + 0x12) = (short)(wait % 5);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))--;
            if (*(short*)(e + 0x12) < 0)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0xffffbfff;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x14))++;
        e[0x40] = (byte)SignedMask(CrtRand(), 3);

        int roll = CrtRand();
        if (*(short*)(e + 0x14) == 0x14 || (short)(roll % 0x14) == 1)
        {
            *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>cdq ; and edx,7 ; add eax,edx ; sar eax,3</c> — la division entiere par huit
    /// <b>vers zero</b> que le compilateur de 1998 emet pour un <c>/ 8</c> signe. Un simple
    /// <c>&gt;&gt; 3</c> arrondirait vers moins l'infini et donnerait un resultat different d'une
    /// unite sur les valeurs negatives.
    /// </summary>
    private static int DivideTowardZeroBy8(int value) => (value + ((value >> 0x1f) & 7)) >> 3;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005515a0</c> — <b>les trois images de l'etincelle</b> de
    /// <see cref="EntityHandler_004474C0"/>, huit octets chacune, relevees dans le binaire. Meme
    /// forme d'enregistrement que <see cref="s_LootDescriptors_005646E0"/>.
    ///
    /// <para>
    /// <b>Trois, et pas une :</b> le corps fait tourner <c>+0x40</c> sur 0, 1, 2 — c'est l'indice
    /// d'image — et les trois enregistrements qui se suivent ont des adresses de texture espacees
    /// de <c>0x10</c> (<c>0x0f0fd000</c>, <c>d010</c>, <c>d020</c>). Le quatrieme
    /// (<c>0x0f0fa8a0</c>) rompt la suite, ce qui borne la sequence.
    /// </para>
    /// </summary>
    private static readonly byte[] s_SparkDescriptors_005515A0 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515a0
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515a8
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515b0
    ];

    private static byte* s_SparkDescriptorStorage;

    /// <summary>Les trois images en memoire native : l'etincelle les garde par pointeur
    /// (<c>+0x3c</c>) et choisit la sienne par <c>+0x40</c>.</summary>
    private static byte* SparkDescriptors()
    {
        if (s_SparkDescriptorStorage == null)
        {
            s_SparkDescriptorStorage =
                (byte*)NativeMemory.Alloc((nuint)s_SparkDescriptors_005515A0.Length);

            for (int i = 0; i < s_SparkDescriptors_005515A0.Length; i++)
            {
                s_SparkDescriptorStorage[i] = s_SparkDescriptors_005515A0[i];
            }
        }

        return s_SparkDescriptorStorage;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551528</c> — l'atlas que <see cref="EntityHandler_0044FB10"/> se donne
    /// a lui-meme en naissant. Enregistrements de <b>huit octets</b> — <c>x, y, largeur, hauteur</c>
    /// puis <c>01 02 10 00</c>.
    ///
    /// <para>
    /// <b>L'etendue est deduite de la signature, pas d'un compteur</b> : les quatre octets de queue
    /// valent <c>01 02 10 00</c> sur <b>trente-cinq</b> enregistrements d'affilee, puis changent en
    /// <c>01 00 11 00</c> a <c>0x00551640</c> — c'est la borne. Aucun corps du binaire ne borne
    /// l'indice, qui vient de <c>+0x9a</c> ; la meme methode que pour
    /// <see cref="s_Descriptors_00551B00"/>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551528 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x00551528
        0x08, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x00551530
        0x10, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551538
        0x20, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551540
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x00551548
        0x30, 0x00, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551550
        0x40, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551558
        0x60, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551560
        0x80, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551568
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551570
        0x70, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551578
        0x80, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551580
        0x90, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551588
        0xa0, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551590
        0x60, 0x48, 0x20, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x00551598

        // Les TROIS images de l'eclat de Marteau, pas une. TickHammerDebris_0044A320 pose
        // 0x005515a0 dans son +0x3c et fait tourner son +0x40 sur trois — et le rendu lit
        // `*(byte**)(e + 0x3c) + e[0x40] * 8`. Le tableau s'arretait a la premiere : deux images
        // sur trois lisaient huit octets de tas jamais ecrits, d'ou des etoiles gigantesques.
        // Meme defaut que 0x00551b00 sur Boule de feu, corrige le 2026-09-01.
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515a0
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515a8
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005515b0
    ];

    private static byte* s_Descriptors00551528Storage;

    private static byte* Descriptors00551528()
        => NativeCopy(s_Descriptors_00551528, ref s_Descriptors00551528Storage);

    /// <summary>
    /// Ghidra: FUN_0044fb10 @ 0x0044fb10 — <b>l'etincelle de la bouffee</b> : le sous-type
    /// <c>0xa9</c> que <see cref="EntityHandler_00446790"/> seme par paires. Entree 169 de la table
    /// <b>statique</b> <c>0x00552230</c>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle tire tout son etat au hasard en naissant</b>, en sept appels au generateur : la
    /// vitesse de montee (<c>rand%10 + 12</c>), un cap sur le tour complet et un rayon, la vitesse
    /// d'ouverture (<c>rand%600 + 600</c>), la duree de vie (<c>rand%15 + 10</c> images) et le
    /// plafond d'ouverture (<c>rand%3000 + 4000</c>). Deux etincelles nees la meme image ne se
    /// ressemblent donc jamais.
    /// </para>
    /// <para>
    /// <b>Le cap est un modulo signe ecrit sans division.</b> Ghidra en rend les cinq operations
    /// telles quelles (<c>((u ^ m) - m &amp; 0xfff ^ m) - m</c> avec <c>m</c> le masque de signe) :
    /// c'est <c>x % 0x1000</c> a la mode du C, celui qui tronque vers zero et garde le signe du
    /// dividende. Rendu par ce qu'il calcule.
    /// </para>
    /// <para>
    /// Le rayon est tire modulo <c>+0x14</c>, que la mere a mis a <c>0x50</c> avant de la faire
    /// naitre — puis <b>le meme champ devient la duree de vie</b> quelques lignes plus bas. Un
    /// champ, deux roles successifs, et l'ordre des deux lectures est ce qui les separe.
    /// </para>
    /// <para>
    /// <b>Ensuite elle monte, s'ouvre et s'assombrit.</b> Chaque image : la teinte perd
    /// <c>3 + rand%7</c> sur les trois canaux, les deux rayons <c>+0x28</c> et <c>+0x2a</c>
    /// grandissent de <c>+0x16</c> jusqu'a leur plafond, la hauteur monte de la vitesse courante, et
    /// cette vitesse <b>accelere une image sur deux</b> — sur la parite de la duree de vie
    /// restante — jusqu'a <c>0x1e</c>. A zero, l'entite perd son bit <c>0x80000000</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0044FB10(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00551528();
            e[0x40] = 0;
            e[0x0a] = e[0x9a];
            e[0x0b] = 0;

            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = CrtRand() % 10 + 0xc;

            // Ghidra: les deux tirages sont faits AVANT que l'un ou l'autre ne serve — le cap est
            // calcule sur le premier, le rayon sur le second.
            int spun = CrtRand();
            int spread = CrtRand();

            short heading = (short)(spun % 0x1000);
            short radius = (short)(spread % *(short*)(e + 0x14));

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * radius;

            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;

            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * radius;

            int growth = CrtRand();
            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xb4;
            *(short*)(e + 0x16) = (short)(growth % 600 + 600);

            // Le meme +0x14 qui vient de servir de borne au rayon devient la duree de vie.
            *(short*)(e + 0x14) = (short)(CrtRand() % 0xf + 10);
            *(short*)(e + 0x9a) = (short)(CrtRand() % 3000 + 4000);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short fade = (short)(-3 - CrtRand() % 7);
        BrightenEntityTint_00404DE0(e, fade, fade, fade);

        *(short*)(e + 0x28) += *(short*)(e + 0x16);
        *(short*)(e + 0x2a) += *(short*)(e + 0x16);

        short ceiling = *(short*)(e + 0x9a);
        if (ceiling < *(short*)(e + 0x28)) *(short*)(e + 0x28) = ceiling;
        if (ceiling < *(short*)(e + 0x2a)) *(short*)(e + 0x2a) = ceiling;

        (*(short*)(e + 0x12))++;
        *(int*)(e + 0x20) += *(int*)(e + 0x5c) * 0x1000;

        // Ghidra: la parite est lue sur le mot NON SIGNE de +0x14, la duree de vie restante.
        if ((*(ushort*)(e + 0x14) & 1) != 0)
        {
            int rise = *(int*)(e + 0x5c) + 1;
            *(int*)(e + 0x5c) = rise;
            if (rise > 0x1e) *(int*)(e + 0x5c) = 0x1e;
        }

        short life = (short)(*(ushort*)(e + 0x14) - 1);
        *(short*)(e + 0x14) = life;
        if (life < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00446790 @ 0x00446790 — <b>la bouffee de l'objet utilise</b> : ce que le script
    /// d'une potion fait naitre au-dessus de celui qui s'en sert. Entree de la table <b>statique</b>
    /// <see cref="s_StaticEntityHandlers_00551c30"/>, donc valable dans les onze zones.
    /// Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle se recale sur son porteur a chaque image</b> : les trois mots de position viennent
    /// de l'entite de <c>+0x6c</c>, et <b>si ce champ est nul c'est l'enregistrement modele
    /// <c>0x005de558</c> qui sert de source</b> — une entite orpheline se pose donc a l'origine
    /// plutot que de dereferencer un pointeur nul. C'est une garde du binaire, pas de la
    /// transcription.
    /// </para>
    /// <para>
    /// <b>Sa premiere image seme quatre filles d'un coup</b> (sous-type <c>0xa9</c>, sans atlas),
    /// puis <b>deux de plus a chaque image impaire</b>. Les quatre appels sont ecrits quatre fois
    /// dans l'original avec les memes arguments — ce n'est pas une boucle deroulee par le
    /// compilateur mais quatre appels distincts, et la transcription les rend par une boucle de
    /// quatre tours, qui est le meme calcul.
    /// </para>
    /// <para>
    /// Deux sons, a des images precises et une seule fois chacun : <c>0x30</c> a l'image 0,
    /// <c>0x2f</c> a l'image 2. Puis le compteur <c>+0x16</c> monte, et passe <b>0x28</b> l'entite
    /// s'eteint en perdant son bit <c>0x80000000</c>.
    /// </para>
    /// <para>
    /// L'octet <c>+0x0a</c> — que <see cref="SpawnEffectEntity"/> a pose en naissant — sert deux
    /// fois : il devient le mot <c>+0x9a</c> a la premiere image, et il est le dernier argument de
    /// chaque naissance de fille. C'est la saveur de l'objet, transmise a l'effet visible.
    /// </para>
    /// </summary>
    private static void EntityHandler_00446790(byte* e)
    {
        // Ghidra: le corps entier est sous `if (param_1 != NULL)`.
        if (e == null) return;

        byte* parent = *(byte**)(e + 0x6c);
        if (parent == null) parent = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(parent + 0x1c);
        *(int*)(e + 0x20) = *(int*)(parent + 0x20);
        *(int*)(e + 0x24) = *(int*)(parent + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0x50;
            *(short*)(e + 0x16) = 0;
            *(ushort*)(e + 0x9a) = e[0x0a];

            // Ghidra: quatre appels identiques ecrits quatre fois.
            for (int i = 0; i < 4; i++)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, e[0x0b], e[0x0a]);
            }
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlayPositionalSound(0x30, 0x200, 0x1000, (int*)(e + 0x1c));
        if (*(short*)(e + 0x16) == 2) Aud.PlayPositionalSound(0x2f, 0x653, 0x1500, (int*)(e + 0x1c));

        // Ghidra: `*(byte *)((int)param_1 + 0x16) & 1` — l'octet BAS du compteur, donc une image
        // sur deux.
        if ((e[0x16] & 1) != 0)
        {
            *(short*)(e + 0x14) = 0x50;

            for (int i = 0; i < 2; i++)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, e[0x0b], e[0x0a]);
            }
        }

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) > 0x28) *(uint*)e &= 0x7fffffff;
    }
}
