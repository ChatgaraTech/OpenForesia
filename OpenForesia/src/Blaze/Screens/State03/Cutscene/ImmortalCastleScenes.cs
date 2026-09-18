// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES DU CHATEAU DE L'IMMORTEL — zone 6, table 0x0056b0c8.
//
// Ouvert le 2026-09-18 avec le palier 1, sur le patron de `LabyrinthScenes.cs`. L'enregistrement de
// camera de la scene parlante du chateau est 0x005714e0, le suivant de celui du Labyrinthe
// (0x005714b0) au pas de 0x30. La suite est maintenant complete et se lit d'un coup :
//
//     0x00571420  foret 0        0x00571450  foret 1        0x00571480  vieille tour
//     0x005714b0  Labyrinthe     0x005714e0  chateau
//
// plus 0x005713c0 (mine) et 0x005713f0 (montagne du dragon de feu), qui sont SOUS la frontiere de
// .data et passent par Mem.ViewParams — voir `MineScenes.cs`.
//
// CELUI-CI EST LE PREMIER A FRANCHIR 0x00571500. Son +0x20 tombe exactement sur 0x00571500 et son
// +0x28 sur 0x00571508, c'est-a-dire dans la plage que s_EntityState_00571500 couvre AUSSI. Le
// recouvrement latent que GameMemory.cs signale depuis le 2026-09-17 (0x00571500..0x00571667 vu par
// deux noms) reste latent : aucune des trois adresses qu'EntityState sert reellement (0x00571528,
// 0x00571540, 0x00571570) n'est touchee ici. Mais la marge a fondu, et la fusion des deux plages en
// une seule arene 0x00571270..0x00571a38 est le correctif a mener — comme changement delibere, pas
// au detour d'un lot.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x005714e0</c> — <b>l'enregistrement de camera de la scene parlante du
    /// chateau de l'immortel</b>, <c>0x30</c> octets. Son <c>+0x28</c> (<c>0x00571508</c>) est la
    /// memoire d'emplacement que les deux amorces passent comme membre cadre.
    /// </summary>
    private static byte* ImmortalCastleSceneRecord => ForestSceneField(0x005714e0);

    /// <summary>
    /// Ghidra: <c>0x0042c3c0</c> — <b>la premiere amorce de la scene parlante du chateau</b>,
    /// creneau <b>17</b> du bloc <c>0x00559e28</c> (table <c>0x0056b0c8</c>, sous-zones 5 et 11).
    /// Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// Aucun script (<c>-1</c>), la memoire d'emplacement du bloc, <b>la piste de musique
    /// 0x0e</b>, et <c>+0x98 = 0x0a</c>. Le corps appele, <c>FUN_0042c3f0</c>, est
    /// <see cref="TickForestCutsceneWithDialogue"/> : ses 372 instructions ne different de
    /// <c>FUN_0040aaf0</c> (mine) que par les treize adresses de l'enregistrement, toutes decalees
    /// de <c>0x120</c> (<c>0x005713c0</c> -&gt; <c>0x005714e0</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_0042C3C0(byte* e)
    {
        byte* record = ImmortalCastleSceneRecord;
        *(short*)(e + 0x98) = 0x0a;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x0e);   // Ghidra: FUN_0042c3f0
    }

    /// <summary>Ghidra: <c>0x0042d100</c> — <b>la seconde amorce</b>, creneau <b>18</b> du meme
    /// bloc : meme corps et memes arguments, avec <c>+0x98 = 0x0b</c>. Absente de
    /// <c>Blaze.c</c>.</summary>
    private static void EntityHandler_0042D100(byte* e)
    {
        byte* record = ImmortalCastleSceneRecord;
        *(short*)(e + 0x98) = 0x0b;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x0e);   // Ghidra: FUN_0042c3f0
    }

    // =====================================================================
    // PALIER 3 (2026-09-18) — TROIS SCENES DES SOUS-ZONES 5 ET 11
    //
    // Les trois gardent le membre cadre dans ImmortalCastleSceneRecord +0x28 (0x00571508) — c'est
    // leur seule lecture de l'enregistrement de camera — et les deux premieres ont l'ossature des
    // fins de la tour et du Labyrinthe (`EntityHandler_00424DC0`) : balayage des trente-deux
    // entites de scene, cible au repos, clip lu au +8 de la table d'animation, et a la sortie le
    // script 0 du membre garde et 0x2004000 sur la cible.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0042d170 @ 0x0042d170 — creneau <b>13</b> du bloc <c>0x00559e28</c>
    /// (<c>0x0056b0c8</c>, sous-zones 5 et 11). Decompile dans <c>Blaze.c</c>, 140 instructions.
    ///
    /// <para>
    /// <b>Une scene de cent trente images sur l'entite de scene d'espece 1.</b> Le balayage retient
    /// la <b>derniere</b> entite d'espece 1 vivante : position recopiee, pose <c>+0x18 = 2</c>, bit
    /// <c>0x1000</c>, clip au <c>+8</c>. Sans cible, la liste d'entites, et le clip est l'entite
    /// elle-meme (<c>mov edx,[esp+14h]</c>) — comme au Labyrinthe.
    /// </para>
    /// <list type="table">
    ///   <item><term>etat 1</term><description>une image sur deux (bit 0 de <c>+0x16</c>), la cible
    ///     avance d'une image d'animation ; arrivee a la derniere, etat 2.</description></item>
    ///   <item><term>etat 2</term><description>trois sons de numero absolu aux images 19, 20 et 21
    ///     de <c>+0x12</c> — <c>0x14</c> deux fois, puis <c>0x25</c> —, et a l'image 20
    ///     <b>cinquante et un</b> <c>0xc5</c> d'un coup — le creneau 5 de <c>0x0056b2c8</c>,
    ///     l'etincelle qui jaillit, <c>EntityHandler_0042D350</c> — et le bit <c>0x4000</c> sur la
    ///     cible.</description></item>
    ///   <item><term>au-dela de 129</term><description>la sortie commune — et elle est rejouee
    ///     <b>a chaque image</b> : rien n'arrete l'horloge <c>+0x16</c>, et le corps ne fait
    ///     qu'eteindre son bit vivant. C'est le repartiteur qui ne le rappellera plus.</description></item>
    /// </list>
    /// <para>
    /// Le bit d'etat de sortie est ecrit <c>(e &amp; 0x7fffbfff) | 0x4000</c> : il leve le bit
    /// vivant et <b>force</b> <c>0x4000</c>, deux operations dans un seul <c>and</c> suivi d'un
    /// <c>or ah,40h</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042D170(byte* e)
    {
        byte* target = null;

        MusicFadeStep_0041AF50();                                // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* clip = e;                                          // Ghidra: `mov edx,[esp+14h]`
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 1) continue;
            if ((scene[3] & 0x80) == 0) continue;

            target = scene;
            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            *(short*)(scene + 0x18) = 2;
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 8);
            scene[1] |= 0x10;                                    // Ghidra: `or byte [eax+1],10h`
        }

        if (target == null) target = Res.s_EntityList_005de558;

        if (*(short*)(e + 0x10) == 0)
        {
            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = 0;
            *(short*)(target + 0x1a) = 0;
            ImmortalCastleSceneRecord[0x28] = target[9];         // Ghidra: `mov [571508h],al`
        }

        if (*(short*)(e + 0x10) == 1 && (e[0x16] & 1) != 0)
        {
            (*(short*)(target + 0x1a))++;

            // Ghidra: le nombre d'images est etendu SANS signe, l'image courante AVEC.
            ushort frames = *(ushort*)(clip + 0x16);
            if (frames <= *(short*)(target + 0x1a))
            {
                *(short*)(target + 0x1a) = (short)(frames - 1);
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) == 0x13) Aud.PlaySoundEffect(0x14, 0xe6, 0x2000, 0x2000);    // Ghidra: FUN_0053cea0
            if (*(short*)(e + 0x12) == 0x14) Aud.PlaySoundEffect(0x14, 0x200, 0x2000, 0x2000);
            if (*(short*)(e + 0x12) == 0x15) Aud.PlaySoundEffect(0x25, 0x327, 0x2000, 0x2000);

            if (*(short*)(e + 0x12) == 0x14)
            {
                *(short*)(e + 0x10) = 3;
                for (int i = 0; i < 0x33; i++)
                    SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc5, 0, -1);   // Ghidra: FUN_0047ac40

                *(uint*)target |= 0x4000;
            }
        }

        if (*(short*)(e + 0x16) > 0x81)
        {
            *(uint*)e = (*(uint*)e & 0x7fffbfff) | 0x4000;
            CameraModeApply_005037E0(0);                         // Ghidra: FUN_005037b0, sans argument

            byte* member = Inp.s_SlotFlags_005e93c0 + ImmortalCastleSceneRecord[0x28] * 0x9c;
            RunEntityScript(s_Unknown_005e97cc, member, 0);      // Ghidra: FUN_00513dd0

            *(uint*)target |= 0x2004000;
        }

        (*(short*)(e + 0x16))++;
    }

    /// <summary>
    /// Ghidra: <c>0x0042d4e0</c> — creneau <b>11</b> du bloc <c>0x00559e28</c>. Absent de
    /// <c>Blaze.c</c> ; 181 instructions.
    ///
    /// <para>
    /// <b>La scene des eclairs, sur son porteur <c>+0x6c</c></b> — non sur une espece : la cible
    /// est le porteur (ou la liste d'entites), repose et suivie chaque image, clip au <c>+8</c>
    /// garde dans l'emplacement de l'argument. La musique ne s'eteint que si l'octet 1 de la
    /// banque 2 est nul.
    /// </para>
    /// <para>
    /// La premiere image retient le fond d'origine (<c>0x005e92b8</c>..<c>bc</c>), garde le membre
    /// cadre, et arme le compteur de salves <c>+0x5c</c> a 2. Ensuite, a chaque image, un tirage
    /// sur dix : <c>+0x9a</c> monte, et une <b>salve</b> part quand il atteint 45 — ou des 35, sur
    /// un tirage de 1. Une salve : le son <c>0x25</c>, <c>+0x14 = 4</c>, trois incantations
    /// <c>0xc1</c> — le premier maillon de trois eclairs, voir
    /// <c>Effects/ImmortalCastleParticles.cs</c>, <c>EntityHandler_0042D770</c> — et, <b>pour la
    /// premiere salve seulement</b>, trois de plus et le bit <c>0x4000</c> sur la cible. Chaque
    /// salve remet l'horloge <c>+0x16</c> a zero et avance <c>+0x5c</c>.
    /// </para>
    /// <para>
    /// La cible avance d'une image d'animation une image sur deux (bit 0 de <c>+0x16</c>
    /// <b>eteint</b>, a l'inverse de <see cref="EntityHandler_0042D170"/>) et bute sur la derniere.
    /// Apres la premiere salve, le son <c>0x26</c> part toutes les trois images tant que l'horloge
    /// n'a pas atteint 30 ; passe 30, la scene se ferme : script 0 du membre garde,
    /// <c>0x2004000</c> sur la cible, et le fond d'origine rendu en mode 2.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042D4E0(byte* e)
    {
        if (Mem.s_StateBank2_005e9060[1] == 0) MusicFadeStep_0041AF50();   // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                                      // Ghidra: FUN_0040e640

        byte* target = *(byte**)(e + 0x6c);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        *(short*)(target + 0x18) = 2;
        byte* clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + target[4] * 4) + 8);   // Ghidra: `mov [esp+14h],ecx`
        *(uint*)target |= 0x1000;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);

            *(short*)(target + 0x1a) = 0;
            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = 2;
            ImmortalCastleSceneRecord[0x28] = target[9];         // Ghidra: `mov [571508h],al`
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int roll = CrtRand() % 10;                           // Ghidra: FUN_00547140
            (*(short*)(e + 0x9a))++;
            short wait = *(short*)(e + 0x9a);

            if (wait >= 0x2d || (wait >= 0x23 && (short)roll == 1))
            {
                Aud.PlaySoundEffect(0x25, 0x200, 0x1000, 0x1000);   // Ghidra: FUN_0053cea0
                *(short*)(e + 0x9a) = 0;
                *(short*)(e + 0x14) = 4;

                SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);          // Ghidra: FUN_0047aae0
                SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);
                SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);

                if (*(int*)(e + 0x5c) == 2)
                {
                    SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);
                    SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);
                    SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);
                    *(uint*)target |= 0x4000;
                }

                *(short*)(e + 0x16) = 0;
                (*(int*)(e + 0x5c))++;
            }
        }

        if ((e[0x16] & 1) == 0)
        {
            (*(short*)(target + 0x1a))++;

            ushort frames = *(ushort*)(clip + 0x16);
            if (frames <= *(short*)(target + 0x1a)) *(short*)(target + 0x1a) = (short)(frames - 1);
        }

        if (*(short*)(e + 0x16) < 0x1e && *(int*)(e + 0x5c) == 3)
        {
            (*(short*)(e + 0x98))++;
            if (*(short*)(e + 0x98) >= 3)
            {
                *(short*)(e + 0x98) = 0;
                Aud.PlaySoundEffect(0x26, 0x800, 0x600, 0x600);
            }
        }

        if (*(short*)(e + 0x16) > 0x1e && *(int*)(e + 0x5c) >= 3)
        {
            *(uint*)e &= 0x7fffffff;
            CameraModeApply_005037E0(0);                         // Ghidra: FUN_005037b0, sans argument

            byte* member = Inp.s_SlotFlags_005e93c0 + ImmortalCastleSceneRecord[0x28] * 0x9c;
            RunEntityScript(s_Unknown_005e97cc, member, 0);      // Ghidra: FUN_00513dd0

            *(uint*)target |= 0x2004000;
            SetSceneBackdrop(
                (short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
        }

        (*(short*)(e + 0x16))++;
    }

    /// <summary>
    /// Ghidra: FUN_0042dc80 @ 0x0042dc80 — creneau <b>12</b> du bloc <c>0x00559e28</c>. Decompile
    /// dans <c>Blaze.c</c>, 209 instructions.
    ///
    /// <para>
    /// <b>L'entite d'espece 0 s'eleve, tourne face a la camera, puis s'envole et retrecit.</b> Cent
    /// quarante images, et deux choses a chaque image avant tout le reste : le balayage marque
    /// toute entite de scene d'espece 0 vivante (<c>+0x18 = 7</c>, bits <c>0x201000</c>) et retient
    /// la derniere, et <b>le script 1 du membre <c>DAT_005de538</c> est relance</b> — a chaque
    /// image, sans condition. Il faut donc que ce script soit sans effet quand il se rejoue.
    /// </para>
    /// <list type="table">
    ///   <item><term>naissance</term><description>une rotation propre tiree sur dix
    ///     (<c>+0x12</c>), la cible mise au repos, l'orientation de l'entite recopiee de la cible et
    ///     retournee d'un demi-tour, et trois fois le son <c>0x27</c> a trois hauteurs.</description></item>
    ///   <item><term>toujours</term><description>la cible se tourne vers la camera
    ///     (<c>Ordinal_457</c> sur <c>0x005de068</c>/<c>070</c>).</description></item>
    ///   <item><term>etat 1, jusqu'a 70</term><description>la cible glisse d'un trente-deuxieme par
    ///     image vers <c>(0xe28000, 0x514000, 0)</c> ; l'entite pose <c>+0x30 = 400</c> et tourne
    ///     de <c>+0x12</c> par image. A 70, les deux premiers enregistrements de
    ///     <c>0x005fa5e0</c> perdent <c>0x04000000</c> et recoivent <c>+0x14 = 0x14</c>.</description></item>
    ///   <item><term>71 a 89</term><description>une image sur deux, la cible avance son image
    ///     d'animation et reboucle de 8 sur 3.</description></item>
    ///   <item><term>90 et apres</term><description>etat 2 : l'entite se pose sur la cible, qui
    ///     file d'un seizieme par image vers <c>(0x1068000, 0x320000, 0)</c> en perdant trente sur
    ///     chacune de ses trois echelles ; les quatre membres et les trente-deux entites de scene
    ///     prennent <c>0x04000000</c>, a chaque image.</description></item>
    ///   <item><term>140</term><description>fin : <see cref="UnfreezeAllEntities"/>,
    ///     <c>0x2004000</c> sur la cible, la camera. <b>Aucun script</b> a la sortie, contrairement
    ///     aux deux precedentes.</description></item>
    /// </list>
    /// <para>
    /// <b>La constante <c>0x514000</c> n'est pas une adresse</b> : Ghidra l'ecrit
    /// <c>(int)FUN_00514000</c> parce qu'elle tombe dans <c>.text</c>. Le desassemblage dit
    /// <c>mov edx,514000h</c> — une hauteur de 1300 unites.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042DC80(byte* e)
    {
        byte* target = null;

        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* scene = Mem.Entity(0x005ecaa0);
        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 0) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            *(short*)(scene + 0x18) = 7;
            *(uint*)scene |= 0x201000;
            target = scene;
        }

        byte* leader = Inp.s_SlotFlags_005e93c0 + (byte)Mem.s_Unknown_005de538 * 0x9c;
        RunEntityScript(s_Unknown_005e97cc, leader, 1);          // Ghidra: FUN_00513dd0

        if (target == null) target = Res.s_EntityList_005de558;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            int spin = CrtRand() % 10;                           // Ghidra: FUN_00547140
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x12) = (short)spin;

            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;

            *(short*)(e + 0x30) = *(short*)(target + 0x30);
            *(short*)(e + 0x32) = (short)(*(short*)(target + 0x32) + 0x800);
            *(short*)(e + 0x34) = *(short*)(target + 0x34);
            *(short*)(target + 0x1a) = 0;

            Aud.PlaySoundEffect(0x27, 0x200, 0x800, 0x800);     // Ghidra: FUN_0053cea0
            Aud.PlaySoundEffect(0x27, 0x22a, 0x800, 0x800);
            Aud.PlaySoundEffect(0x27, 0x255, 0x800, 0x800);
        }

        *(short*)(target + 0x32) = (short)Winsys3d.Ordinal_457(
            *Mem.Camera(0x005de068) - *(int*)(target + 0x1c),
            *Mem.Camera(0x005de070) - *(int*)(target + 0x24));

        if (*(short*)(e + 0x10) == 1)
        {
            int x = *(int*)(target + 0x1c);
            *(int*)(target + 0x1c) = ((0xe28000 - x) >> 5) + x;
            int y = *(int*)(target + 0x20);
            *(int*)(target + 0x20) = ((0x514000 - y) >> 5) + y;   // Ghidra: `(int)FUN_00514000`, une immediate
            int z = *(int*)(target + 0x24);
            *(int*)(target + 0x24) = (-z >> 5) + z;

            if (*(short*)(e + 0x16) <= 0x46)
            {
                *(short*)(e + 0x30) = 400;
                *(short*)(e + 0x32) += *(short*)(e + 0x12);
            }
        }

        if (*(short*)(e + 0x16) == 0x46)
        {
            byte* anchors = Mem.Entity(0x005fa5e0);
            *(uint*)anchors &= 0xfbffffff;
            *(uint*)(anchors + 0x9c) &= 0xfbffffff;
            *(short*)(anchors + 0x14) = 0x14;                    // Ghidra: DAT_005fa5f4
            *(short*)(anchors + 0x9c + 0x14) = 0x14;             // Ghidra: DAT_005fa690
        }

        short clock = *(short*)(e + 0x16);
        if (clock > 0x46 && clock < 0x5a && (clock & 1) != 0)
        {
            (*(short*)(target + 0x1a))++;
            if (*(short*)(target + 0x1a) > 8) *(short*)(target + 0x1a) = 3;
        }

        if (*(short*)(e + 0x16) == 0x5a)
        {
            *(short*)(e + 0x10) = 2;
            *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
            *(int*)(e + 0x20) = *(int*)(target + 0x20);
            *(int*)(e + 0x24) = *(int*)(target + 0x24);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            int x = *(int*)(target + 0x1c);
            *(int*)(target + 0x1c) = ((0x1068000 - x) >> 4) + x;
            int y = *(int*)(target + 0x20);
            *(int*)(target + 0x20) = ((0x320000 - y) >> 4) + y;

            *(short*)(target + 0x28) -= 0x1e;
            *(short*)(target + 0x2a) -= 0x1e;
            *(short*)(target + 0x2c) -= 0x1e;

            int z = *(int*)(target + 0x24);
            *(int*)(target + 0x24) = (-z >> 4) + z;

            if (*(short*)(target + 0x28) < 0)
            {
                *(short*)(target + 0x28) = 0;
                *(short*)(target + 0x2a) = 0;
                *(short*)(target + 0x2c) = 0;
            }

            byte* member = Inp.s_SlotFlags_005e93c0;
            for (int i = 0; i < 4; i++, member += 0x9c) *(uint*)member |= 0x4000000;

            scene = Mem.Entity(0x005ecaa0);
            for (int i = 0; i < 0x20; i++, scene += 0x9c) *(uint*)scene |= 0x4000000;
        }

        if (*(short*)(e + 0x16) == 0x8c)
        {
            *(uint*)e &= 0x7fffffff;
            UnfreezeAllEntities();                               // Ghidra: FUN_00535400
            *(uint*)target |= 0x2004000;
            CameraModeApply_005037E0(0);                         // Ghidra: FUN_005037b0, sans argument
        }

        (*(short*)(e + 0x16))++;
    }
}
