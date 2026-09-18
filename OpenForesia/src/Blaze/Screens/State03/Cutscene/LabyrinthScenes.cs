// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES DU LABYRINTHE DES MORTS — zone 5, table 0x0056b0c8.
//
// Ouvert le 2026-09-17 sur le patron de `OldTowerScenes.cs`. L'enregistrement de camera de la scene
// parlante du Labyrinthe est 0x005714b0, le suivant de celui de la tour (0x00571480) au pas de
// 0x30 : il tombe dans `s_ForestSceneBlock_00571420` (Effects/ForestParticlesB.cs), qui le couvre
// deja d'un seul tenant.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x005714b0</c> — <b>l'enregistrement de camera de la scene parlante du
    /// Labyrinthe</b>, <c>0x30</c> octets. Son <c>+0x28</c> (<c>0x005714d8</c>) est la memoire
    /// d'emplacement que les deux amorces passent comme membre cadre.
    /// </summary>
    private static byte* LabyrinthSceneRecord => ForestSceneField(0x005714b0);

    /// <summary>
    /// Ghidra: <c>0x004236f0</c> — <b>la premiere amorce de la scene parlante du Labyrinthe</b>,
    /// sous-zones 4 et 9. Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// Aucun script (<c>-1</c>), la memoire d'emplacement du bloc, <b>la piste de musique 0x17</b>, et
    /// <c>+0x98 = 3</c>. Le corps appele, <c>FUN_00423720</c>, est
    /// <see cref="TickForestCutsceneWithDialogue"/> : son texte ne differe de <c>FUN_00419f70</c>
    /// (tour) que par les adresses de l'enregistrement, toutes decalees de <c>0x30</c>
    /// (<c>0x00571480</c> -&gt; <c>0x005714b0</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_004236F0(byte* e)
    {
        byte* record = LabyrinthSceneRecord;
        *(short*)(e + 0x98) = 3;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x17);   // Ghidra: FUN_00423720
    }

    /// <summary>Ghidra: <c>0x00424460</c> — <b>la seconde amorce</b>, meme corps et memes arguments,
    /// avec <c>+0x98 = 4</c>. Absente de <c>Blaze.c</c>.</summary>
    private static void EntityHandler_00424460(byte* e)
    {
        byte* record = LabyrinthSceneRecord;
        *(short*)(e + 0x98) = 4;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x17);   // Ghidra: FUN_00423720
    }

    /// <summary>
    /// Ghidra: FUN_00424dc0 @ 0x00424dc0 — creneau <b>9</b> du bloc <c>0x00558aa8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 217 instructions ;
    /// dump sous <c>Legacy/Raw/Blaze/FUN_00424DC0.asm</c>. Lot 8, 2026-09-18.
    ///
    /// <para>
    /// <b>Une scene de cent images sur l'entite de scene d'espece 1.</b> L'ossature des fins de la
    /// tour (<see cref="EntityHandler_0041AC80"/>) : les deux appels de tete, le balayage des
    /// trente-deux entites de <c>0x005ecaa0</c> qui retient la <b>derniere</b> dont <c>+0x0c</c>
    /// vaut 1 et qui porte <c>0x80000000</c> — position recopiee, pose <c>+0x18 = 2</c>, bit 2 de
    /// <c>+0x3b</c>, bit <c>0x1000</c>, clip lu au <c>+8</c> de la table d'animation.
    /// </para>
    /// <para>
    /// <b>Deux ecarts avec la tour, et ils changent la conduite sans cible.</b> Le secours n'est pas
    /// un tampon de pile mais <c>Res.s_EntityList_005de558</c> ; et le clip part de <b>l'entite
    /// elle-meme</b> — <c>mov ebp,[esp+14h]</c> avant la boucle —, pas d'un mot de pile non ecrit.
    /// Sans cible, la borne d'animation se lit donc dans le <c>+0x16</c> de la scene : aucune faute,
    /// un nombre absurde.
    /// </para>
    /// <para>
    /// <b>La partition</b>, sur <c>+0x16</c> qui monte d'un par image : le son de zone
    /// <c>0x2d</c> aux images 0, 15, 30 et 45, a une hauteur tiree dans <c>[0x200, 0x400[</c> —
    /// tiree <b>a chaque image</b>, qu'elle serve ou non ; a l'image 60, le son 5, la naissance du
    /// sous-type <c>199</c> (<c>0xc7</c>), le passage a l'etat 2 et le bit <c>0x4000</c> sur la
    /// cible ; a 61, le son 5 plus haut. Pendant l'etat 1 la cible avance d'une image
    /// d'animation par image — sauf quand le bit 2 de <c>+0x16</c> est pose, soit quatre images
    /// sur huit —, bute sur la derniere, fait naitre un <c>0xfa</c> et s'assombrit d'un cran par
    /// canal.
    /// </para>
    /// <para>
    /// <b>Au-dela de 99</b> : extinction, camera, le script 0 du membre garde dans
    /// <c>0x005714d8</c> (<see cref="LabyrinthSceneRecord"/> <c>+0x28</c>), <c>0x2004000</c> sur la
    /// cible, et le fond <c>0x005e9160</c> recoit les trois shorts de <c>0x005e92b8</c> retenus a la
    /// premiere image, en mode 2.
    /// </para>
    /// </summary>
    private static void EntityHandler_00424DC0(byte* e)
    {
        byte* target = null;

        MusicFadeStep_0041AF50();                                // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* clip = e;                                          // Ghidra: `mov ebp,[esp+14h]`
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 1) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            target = scene;
            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            *(short*)(scene + 0x18) = 2;                         // Ghidra: `mov word [eax-23h],2`
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 8);
            scene[0x3b] |= 2;
            *(uint*)scene |= 0x1000;
        }

        if (target == null) target = Res.s_EntityList_005de558;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);

            byte slot = target[9];
            *(short*)(target + 0x1a) = 0;
            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;
            LabyrinthSceneRecord[0x28] = slot;                   // Ghidra: `mov [5714D8h],dl`
        }

        if (*(short*)(e + 0x10) == 1)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfa, 0, -1);

            if ((e[0x16] & 2) == 0)
            {
                (*(short*)(target + 0x1a))++;

                // Ghidra: le nombre d'images est etendu SANS signe, l'image courante AVEC.
                ushort frames = *(ushort*)(clip + 0x16);
                if (frames <= *(short*)(target + 0x1a)) *(short*)(target + 0x1a) = (short)(frames - 1);
            }

            target[0x38]--;
            target[0x39]--;
            target[0x3a]--;
        }

        short pitch = (short)(CrtRand() % 0x200 + 0x200);        // Ghidra: le reste signe, en signe-module
        // Ghidra: l'octet de son de zone est relu a chaque appel, jamais hors d'un appel.
        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(SubAreaSounds()[0x2d], pitch, 0x1500, 0x1500);   // Ghidra: FUN_0053cea0
        if (*(short*)(e + 0x16) == 0x0f) Aud.PlaySoundEffect(SubAreaSounds()[0x2d], pitch, 0x1500, 0x1500);
        if (*(short*)(e + 0x16) == 0x1e) Aud.PlaySoundEffect(SubAreaSounds()[0x2d], pitch, 0x1500, 0x1500);
        if (*(short*)(e + 0x16) == 0x2d) Aud.PlaySoundEffect(SubAreaSounds()[0x2d], pitch, 0x1500, 0x1500);

        if (*(short*)(e + 0x16) == 0x3c)
        {
            Aud.PlaySoundEffect(5, 0xe6, 0x1200, 0x1200);
            SpawnCastEntity_0047AAE0(e, 199, 0, -1);             // Ghidra: FUN_0047aae0
            *(short*)(e + 0x10) = 2;
            *(uint*)target |= 0x4000;
        }

        if (*(short*)(e + 0x16) == 0x3d) Aud.PlaySoundEffect(5, 0x200, 0x1200, 0x1200);

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) <= 99) return;

        *(uint*)e &= 0x7fffffff;
        CameraModeApply_005037E0(0);                             // Ghidra: FUN_005037b0, sans argument

        byte* member = Inp.s_SlotFlags_005e93c0 + LabyrinthSceneRecord[0x28] * 0x9c;
        RunEntityScript(s_Unknown_005e97cc, member, 0);          // Ghidra: FUN_00513dd0

        *(uint*)target |= 0x2004000;
        SetSceneBackdrop(
            (short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
    }

    /// <summary>
    /// Ghidra: FUN_004244d0 @ 0x004244d0 — creneau <b>8</b> du bloc <c>0x00558aa8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 224 instructions ;
    /// dump sous <c>Legacy/Raw/Blaze/FUN_004244D0.asm</c>. Lot 8, 2026-09-18.
    ///
    /// <para>
    /// <b>La voisine de creneau de <see cref="EntityHandler_00424DC0"/>, sur l'espece 2</b>, deux
    /// fois plus longue : deux cents images. Meme balayage (<c>+0x0c == 2</c>, pose 2, bit
    /// <c>0x1000</c>, clip au <c>+8</c>, <b>sans</b> le bit 2 de <c>+0x3b</c>), meme secours
    /// <c>Res.s_EntityList_005de558</c>, meme clip de depart — l'entite elle-meme, gardee dans
    /// l'emplacement de l'argument (<c>mov [esp+14h],edx</c>).
    /// </para>
    /// <para>
    /// <b>La premiere image</b> retient le fond d'origine, met la cible au repos, garde son
    /// numero dans <c>0x005714d8</c>, se hisse de <c>0x14</c> unites, retient la position de la
    /// cible en <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c> et <b>peint le fond en rouge</b> :
    /// <c>(0xfa, 0xaa, 0xaa)</c>, mode 2.
    /// </para>
    /// <para>
    /// <b>Les etats</b> : 1 fait naitre deux <c>0xc5</c> en une image, avec un role prete et
    /// <b>non rendu</b> — <c>+0x9a</c>/<c>+0x5c</c>/<c>+0x98</c> a <c>0xdac</c>/<c>0x1d4c</c>/<c>-100</c>
    /// puis <c>3000</c>/<c>8000</c>/<c>0x50</c>, qui restent. 2 seme deux <c>0xc6</c> par image,
    /// hisse la cible de <c>0x1000</c>, la palit de 10 par canal et l'anime une image sur deux
    /// jusqu'a sa derniere image, puis passe a 3 ; 3 la hisse encore trente images, puis 4, ou
    /// plus rien ne bouge.
    /// </para>
    /// <para>
    /// <b>La partition</b>, sur <c>+0x16</c> : a 100 le fond d'origine revient ; de 110 a 170 un
    /// <c>0xf9</c> par image ; de 170 a 180 deux <c>0xc5</c> et le bit <c>0x4000</c> sur la cible —
    /// les deux fenetres se touchent a 170 ; au-dela de 199 la fin de
    /// <see cref="EntityHandler_00424DC0"/>, fond compris.
    /// </para>
    /// </summary>
    private static void EntityHandler_004244D0(byte* e)
    {
        byte* target = null;

        MusicFadeStep_0041AF50();                                // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* clip = e;                                          // Ghidra: l'emplacement de l'argument
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 2) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            target = scene;
            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            *(short*)(scene + 0x18) = 2;
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 8);
            *(uint*)scene |= 0x1000;
        }

        if (target == null) target = Res.s_EntityList_005de558;

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
            LabyrinthSceneRecord[0x28] = target[9];              // Ghidra: DAT_005714d8

            *(int*)(e + 0x20) += 0x14000;
            *(int*)(e + 0x44) = *(int*)(target + 0x1c);
            *(int*)(e + 0x48) = *(int*)(target + 0x20);
            *(int*)(e + 0x4c) = *(int*)(target + 0x24);

            SetSceneBackdrop(0xfa, 0xaa, 0xaa, 2);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x9a) = 0xdac;
            *(int*)(e + 0x5c) = 0x1d4c;
            *(short*)(e + 0x98) = -100;                          // Ghidra: 0xff9c
            SpawnCastEntity_0047AAE0(e, 0xc5, 0, -1);            // Ghidra: FUN_0047aae0

            *(short*)(e + 0x9a) = 3000;
            *(int*)(e + 0x5c) = 8000;
            *(short*)(e + 0x98) = 0x50;
            SpawnCastEntity_0047AAE0(e, 0xc5, 0, -1);

            *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc6, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc6, 0, -1);

            *(int*)(target + 0x20) += 0x1000;
            BrightenEntityTint_00404DE0(target, -10, -10, -10);

            if ((e[0x16] & 1) == 0)
            {
                (*(short*)(target + 0x1a))++;

                // Ghidra: le nombre d'images est etendu SANS signe, l'image courante AVEC.
                ushort frames = *(ushort*)(clip + 0x16);
                if (frames <= *(short*)(target + 0x1a))
                {
                    *(short*)(target + 0x1a) = (short)(frames - 1);
                    *(short*)(e + 0x10) = 3;
                    *(short*)(e + 0x12) = 0;
                }
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(int*)(target + 0x20) += 0x1000;
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x1d) *(short*)(e + 0x10) = 4;
        }

        if (*(short*)(e + 0x16) == 100)
        {
            SetSceneBackdrop(
                (short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
        }

        if (*(short*)(e + 0x16) > 0x6d && *(short*)(e + 0x16) < 0xab)
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf9, 0, -1);

        if (*(short*)(e + 0x16) > 0xa9 && *(short*)(e + 0x16) < 0xb5)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc5, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc5, 0, -1);
            *(uint*)target |= 0x4000;
        }

        if (*(short*)(e + 0x16) > 199)
        {
            *(uint*)e &= 0x7fffffff;
            CameraModeApply_005037E0(0);                         // Ghidra: FUN_005037b0, sans argument

            byte* member = Inp.s_SlotFlags_005e93c0 + LabyrinthSceneRecord[0x28] * 0x9c;
            RunEntityScript(s_Unknown_005e97cc, member, 0);      // Ghidra: FUN_00513dd0

            *(uint*)target |= 0x2004000;
            SetSceneBackdrop(
                (short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
        }

        (*(short*)(e + 0x16))++;
    }
}
