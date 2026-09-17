// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES CINEMA DE LA VIEILLE TOUR — zone 4.
//
// Ouvert le 2026-09-17, sur le patron de `MineScenes.cs` : le corps des scenes est celui de la
// foret, recompile par zone a la base du bloc prive pres, et ce fichier ne porte que ce qui est
// PROPRE a la tour — les amorces, et l'adresse de leur enregistrement.
//
// LE BLOC PRIVE DE LA TOUR EST 0x00571480, et il n'a demande aucune decision de plan memoire :
// c'est le troisieme enregistrement (0x00571420 + 2 * 0x30) du domaine que
// `s_ForestSceneBlock_00571420` (Effects/ForestParticlesB.cs) couvre deja d'un seul tenant. Il passe
// donc par ForestSceneField, adresse par adresse, comme tout ce qui vit dans ce domaine.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00571480</c> — <b>l'enregistrement de camera de la scene parlante de la
    /// tour</b>, <c>0x30</c> octets, sur le meme plan que <see cref="ForestSceneRecord"/>. Son
    /// <c>+0x28</c> (<c>0x005714a8</c>) est la memoire d'emplacement que les deux amorces passent
    /// comme membre cadre.
    /// </summary>
    private static byte* OldTowerSceneRecord => ForestSceneField(0x00571480);

    /// <summary>
    /// Ghidra: <c>0x00419f40</c> — <b>la premiere amorce de la scene parlante de la tour</b>,
    /// sous-zones 3, 7 et 9, table <c>0x0056b0c8</c>. Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// La forme exacte de <see cref="EntityHandler_0040AAC0"/> (mine) : aucun script (<c>-1</c>), la
    /// memoire d'emplacement du bloc, et une piste de musique — ici <b>la piste 0x10</b>, la ou la
    /// mine demande la 8 — avec <c>+0x98 = 0x3d</c> au lieu de <c>0</c>. Le corps appele,
    /// <c>FUN_00419f70</c>, est <see cref="TickForestCutsceneWithDialogue"/> : le diff STRICT avec
    /// <c>FUN_0040aaf0</c> ne rend que les adresses de l'enregistrement, toutes decalees de
    /// <c>0xc0</c> (<c>0x005713c0</c> -&gt; <c>0x00571480</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_00419F40(byte* e)
    {
        byte* record = OldTowerSceneRecord;
        *(short*)(e + 0x98) = 0x3d;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x10);   // Ghidra: FUN_00419f70
    }

    /// <summary>
    /// Ghidra: <c>0x0041b3f0</c> — <b>la seconde amorce</b>, meme bloc. La forme de
    /// <see cref="EntityHandler_0040B8D0"/> : <b>le script 1</b>, la memoire d'emplacement, et
    /// <c>-1</c> pour ne pas toucher a la musique, avec <c>+0x98 = 3</c>. Absente de
    /// <c>Blaze.c</c>.
    /// </summary>
    private static void EntityHandler_0041B3F0(byte* e)
    {
        byte* record = OldTowerSceneRecord;
        *(short*)(e + 0x98) = 3;
        TickForestCutsceneWithDialogue(e, record, 1, record[0x28], 0xffff);   // Ghidra: FUN_00419f70
    }

    // =====================================================================
    // LES TROIS FINS DE SCENE DE LA TOUR (2026-09-17)
    //
    // Categorie C, tables 0x0056b0c8 (les trois). Meme famille que les fins de la mine
    // (`EntityHandler_0040B940`, Effects/MineParticles.cs) : le tampon nul monte sur la pile, le
    // balayage des 0x20 entites de scene, la fonte de la musique et le reveil des entites. Mais ce
    // ne sont PAS des copies — le diff contre la mine rend 240 a 323 lignes.
    //
    //   0x0041BB20  la sortie breve       107 instr.  genre 1, deux naissances 0xC2
    //   0x0041AC80  l'enlevement          189         genre 1, fond vert, naissances 0xC1
    //   0x0041B460  la chute              232         genre 1, gamme de cinq sons, naissances 0xFA
    //
    // Les trois finissent pareil : extinction, camera au mode voulu, script du membre garde dans
    // 0x005714a8, et le bit 0x2004000 sur la cible.
    // =====================================================================

    /// <summary>
    /// Le balayage commun aux trois fins de scene : les <c>0x20</c> entites de scene
    /// (<c>0x005ecaa0</c>, pas de <c>0x9c</c>), et pour chacune de genre <c>1</c> encore active —
    /// la scene copie sa position, lui pose l'etat <paramref name="pose"/> en <c>+0x18</c> et le
    /// bit <c>0x1000</c>, et la retient comme <b>cible</b>.
    ///
    /// <para>
    /// <b>La cible est la DERNIERE trouvee, pas la premiere</b> : le binaire ne sort pas de la
    /// boucle. Sans aucune correspondance, elle reste le tampon nul monte sur la pile — le meme
    /// idiome que la mine, et la meme consequence : tout ce que la suite ecrit dans la cible part
    /// dans un tampon que personne ne relit.
    /// </para>
    /// </summary>
    private static byte* SweepSceneTargets_OldTower(byte* e, byte* fallback, short pose)
    {
        byte* target = fallback;
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 1) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            target = scene;
            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            *(short*)(scene + 0x18) = pose;
            *(uint*)scene |= 0x1000;
        }

        return target;
    }

    /// <summary>Les trois shorts de fond <c>0x005e9160</c>..<c>0x005e9164</c> et leur mode
    /// <c>0x005e9166</c>, que les trois fins peignent puis restaurent.</summary>
    private static void SetSceneBackdrop(short r, short g, short b, short mode)
    {
        *(short*)Mem.Arena(0x005e9160) = r;
        *(short*)Mem.Arena(0x005e9162) = g;
        *(short*)Mem.Arena(0x005e9164) = b;
        *(short*)Mem.Arena(0x005e9166) = mode;
    }

    /// <summary>La sortie commune aux trois : extinction, camera, script du membre garde dans
    /// <c>0x005714a8</c>, et le bit <c>0x2004000</c> sur la cible.
    ///
    /// <para><c>FUN_005037b0</c> est une enveloppe SANS argument — le zero passe ici est le
    /// parametre fantome que Ghidra prete a <see cref="CameraModeApply_005037E0"/>, et que le corps
    /// ignore.</para></summary>
    private static void EndTowerScene(byte* e, byte* target, uint script)
    {
        *(uint*)e &= 0x7fffffff;
        CameraModeApply_005037E0(0);                             // Ghidra: FUN_005037b0

        byte* member = Inp.s_SlotFlags_005e93c0 + (*Mem.EntityStateBytes(0x005714a8) & 0xff) * 0x9c;
        RunEntityScript(s_Unknown_005e97cc, member, script);     // Ghidra: FUN_00513dd0

        *(uint*)target |= 0x2004000;
    }

    /// <summary>
    /// Ghidra: <c>0x0041bb20</c> — <b>la sortie breve</b>, sous-zones 3, 7 et 9, table
    /// <c>0x0056b0c8</c>. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0041BB20.asm</c>.
    ///
    /// <para>
    /// La plus courte des trois. A la premiere image elle met la cible au repos
    /// (<c>+0x1a</c> et <c>+0x0a</c> a zero, teinte pleine, <c>+0x36</c> a 1), puis <b>a la seconde
    /// seulement</b> — <c>+0x16</c> vaut encore zero une image, parce que l'increment vient apres —
    /// elle joue <c>0x20</c> a la hauteur <c>0x5dc</c> et fait naitre <b>deux fois</b> le sous-type
    /// <c>0xC2</c>, avec <c>+0x14</c> mis a <c>0x39</c> puis a <c>0x1c</c> : c'est ainsi que les
    /// deux enfants recoivent des variantes differentes.
    /// </para>
    /// <para>
    /// Ensuite elle compte : a la 18e image la cible prend <c>0x4000</c>, a la 50e la scene se
    /// termine avec le script <b>2</b> du <b>chef</b>
    /// (<c>0x005de538</c>) — et non du membre garde, comme les deux autres. C'est la seule des trois
    /// a passer par le chef.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041BB20(byte* e)
    {
        byte* fallback = stackalloc byte[0x9c];
        for (int i = 0; i < 0x9c; i++) fallback[i] = 0;

        MusicFadeStep_0041AF50();                                // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* target = SweepSceneTargets_OldTower(e, fallback, 2);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            *(short*)(target + 0x1a) = 0;
            target[0x0a] = 0;
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x20, 0x5dc, 0x800, 0x800);

        if (*(short*)(e + 0x10) == 1 && *(short*)(e + 0x16) == 0)
        {
            *(short*)(e + 0x14) = 0x39;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);            // Ghidra: FUN_0047aae0
            *(short*)(e + 0x14) = 0x1c;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);

            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
        }

        (*(short*)(e + 0x16))++;

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) >= 0x12) *(uint*)target |= 0x4000;

        if (*(short*)(e + 0x12) != 0x32) return;

        // Ghidra: le CHEF (DAT_005de538), pas la memoire d'emplacement du bloc.
        *(uint*)e &= 0x7fffffff;
        CameraModeApply_005037E0(0);                             // Ghidra: FUN_005037b0, sans argument
        RunEntityScript(s_Unknown_005e97cc,
                        Inp.s_SlotFlags_005e93c0 + (uint)(byte)Mem.s_Unknown_005de538 * 0x9c, 2);
        *(uint*)target |= 0x2004000;
    }

    /// <summary>
    /// Ghidra: <c>0x0041ac80</c> — <b>l'enlevement</b>, sous-zones 3, 7 et 9, table
    /// <c>0x0056b0c8</c>. <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0041AC80.asm</c>.
    ///
    /// <para>
    /// <b>Son compteur part a -50</b> (<c>+0x16 = 0xffce</c>), ce qu'aucune autre scene ne fait : les
    /// cinquante premieres images ne declenchent donc rien, et tous les seuils qui suivent — 0, 40,
    /// 100, 140 — se lisent depuis ce zero decale.
    /// </para>
    /// <para>
    /// Le fond est peint en <b>gris-vert uniforme</b> (<c>0xe6</c> sur les trois canaux, mode 2) a
    /// chaque image de l'etat 1, et les trois shorts d'origine, retenus a la naissance depuis
    /// <c>0x005e92b8</c>, sont remis passe la 100e image.
    /// </para>
    /// <para>
    /// <b>Les naissances.</b> Entre la 1re et la 40e image, une image sur deux (le compteur
    /// <c>+0x12</c> est remis a zero a chaque naissance) fait naitre le sous-type <c>0xC1</c> ; la
    /// 40e en fait naitre un de plus, hors de cette fenetre. Le son <c>0x20</c> a la hauteur
    /// <c>0x1bf4</c> part une image sur trois pendant la meme fenetre, et la 20e image joue trois
    /// sons d'un coup (<c>0x30</c>, <c>0x2d</c>, <c>0x10</c>).
    /// </para>
    /// <para>
    /// <b>La cible s'anime une image sur deux</b> et sa derniere image la bloque : le clip vient de
    /// <c>animTable[cible[4]] + 0x24</c>, et son mot <c>+0x16</c> est la longueur. En butant, la
    /// cible prend le bit 2 de <c>+0x3b</c>. Elle s'assombrit de 4 par canal a chaque image passe le
    /// zero du compteur.
    /// </para>
    /// <para>
    /// <b>Et elle porte le meme defaut de 1998 que la mine</b> : sans cible, le clip est un mot de
    /// pile jamais ecrit, lu des la premiere image paire. Le pointeur est laisse nul ici.
    /// </para>
    /// </summary>
    private static void EntityHandler_0041AC80(byte* e)
    {
        byte* fallback = stackalloc byte[0x9c];
        for (int i = 0; i < 0x9c; i++) fallback[i] = 0;

        MusicFadeStep_0041AF50();                                // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* target = fallback;
        byte* clip = null;                                       // Ghidra: `ebx`, non initialise
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 1) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            target = scene;
            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            *(short*)(scene + 0x18) = 9;
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 0x24);
            *(uint*)scene |= 0x1000;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = -50;                           // Ghidra: 0xffce

            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);

            *(short*)(target + 0x1a) = 0;
            target[0x0a] = 0;
            e[0x0b] = 0;

            byte slot = target[9];
            *(int*)(target + 0x38) = 0xffffff;
            *(short*)(target + 0x36) = 1;
            *Mem.EntityStateBytes(0x005714a8) = slot;

            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        SetSceneBackdrop(0xe6, 0xe6, 0xe6, 2);

        (*(short*)(e + 0x12))++;
        short since = *(short*)(e + 0x12);
        (*(short*)(e + 0x16))++;
        short age = *(short*)(e + 0x16);

        if (since >= 2 && age < 0x28 && age > 0)
        {
            (*(short*)(e + 0x14))++;
            *(short*)(e + 0x12) = 0;
            SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);            // Ghidra: FUN_0047aae0
        }

        if (*(short*)(e + 0x16) == 0x28) SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);

        if (*(short*)(e + 0x16) < 0x28 && *(short*)(e + 0x16) > 0)
        {
            if (*(short*)(e + 0x98) == 0) Aud.PlaySoundEffect(0x20, 0x1bf4, 0x800, 0x800);

            (*(short*)(e + 0x98))++;
            if (*(short*)(e + 0x98) == 3) *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x16) == 0x14)
        {
            Aud.PlaySoundEffect(0x30, 0x100, 0x600, 0x600);
            Aud.PlaySoundEffect(0x2d, 0x100, 0x600, 0x600);
            Aud.PlaySoundEffect(0x10, 0x200, 0x600, 0x600);
        }

        if (*(short*)(e + 0x16) > 0x64)
        {
            SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54),
                             (short)*(int*)(e + 0x58), 2);
        }

        if (*(short*)(e + 0x16) >= 0x8c) EndTowerScene(e, target, 0);

        if ((*(short*)(e + 0x16) & 1) == 0)
        {
            (*(short*)(target + 0x1a))++;
            short frame = *(short*)(target + 0x1a);
            short length = *(short*)(clip + 0x16);

            if (frame >= (length & 0xffff))
            {
                *(short*)(target + 0x1a) = (short)(length - 1);
                target[0x3b] |= 2;
            }
        }

        if (*(short*)(e + 0x16) > 0) BrightenEntityTint_00404DE0(target, -4, -4, -4);
    }

    /// <summary>
    /// Ghidra: <c>0x0041b460</c> — <b>la chute</b>, sous-zones 3, 7 et 9, table <c>0x0056b0c8</c>.
    /// <b>Absente de <c>Blaze.c</c></b> ; dump sous
    /// <c>Legacy/Raw/Blaze/OldTower/FUN_0041B460.asm</c>. La plus longue des trois.
    ///
    /// <para>
    /// <b>Elle joue une gamme de cinq notes en boucle</b>, hors de tout etat : le son <c>0x20</c> aux
    /// hauteurs <c>0x255</c>, <c>0x2aa</c>, <c>0x2d4</c>, <c>0x329</c> et <c>0x37e</c>, une toutes
    /// les dix images, le cycle reprenant a la cinquantieme. Le gain est <c>0xa00</c>, plus fort que
    /// partout ailleurs.
    /// </para>
    /// <para>
    /// <b>Le fond clignote</b> : le rouge est retire au sort entre <c>0xb4</c> et <c>0xef</c> a
    /// chaque image, vert et bleu restent a <c>0x96</c>, et le mode est <b>1</b> — le seul endroit de
    /// la tour a employer ce mode. Les trois valeurs d'origine reviennent passe la 100e image, avec
    /// le mode 2.
    /// </para>
    /// <para>
    /// <b>Les naissances.</b> Des la premiere image et jusqu'a la 80e, chaque image fait naitre le
    /// sous-type <c>0xFA</c> ; et une image sur deux, avant la 70e, un <c>0xC2</c> de plus. Le
    /// binaire tire au passage un <c>rand &amp; 3</c> qu'il <b>range sur la pile sans jamais le
    /// relire</b> — du code mort, conserve comme tel dans le commentaire mais pas dans le calcul.
    /// </para>
    /// <para>
    /// La cible s'anime une image sur deux et bute sur la derniere image de son clip — pris ici a
    /// <c>animTable[cible[4]] + 8</c> et non <c>+ 0x24</c> —, <b>sans</b> poser le bit <c>+0x3b</c>
    /// que l'enlevement pose ; ce bit-la est pose une fois pour toutes dans le balayage. Sa teinte
    /// perd <b>2 au rouge et 4 aux deux autres</b> par image, chacun borne a zero separement.
    /// </para>
    /// <para>A la 140e image : extinction, script 0 du membre garde.</para>
    /// </summary>
    private static void EntityHandler_0041B460(byte* e)
    {
        byte* fallback = stackalloc byte[0x9c];
        for (int i = 0; i < 0x9c; i++) fallback[i] = 0;

        MusicFadeStep_0041AF50();                                // Ghidra: FUN_0041af50
        ResetEntityWakeBits_0040E640();                          // Ghidra: FUN_0040e640

        byte* target = fallback;
        byte* clip = null;                                       // Ghidra: [esp+0x10], non initialise
        byte* scene = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, scene += 0x9c)
        {
            if (scene[0x0c] != 1) continue;
            if ((*(uint*)scene & 0x80000000) == 0) continue;

            target = scene;
            *(int*)(e + 0x1c) = *(int*)(scene + 0x1c);
            *(int*)(e + 0x20) = *(int*)(scene + 0x20);
            *(int*)(e + 0x24) = *(int*)(scene + 0x24);

            *(short*)(scene + 0x18) = 2;
            scene[0x3b] |= 2;
            clip = *(byte**)(*(byte**)(Mem.s_AnimationTable_005e9640 + scene[4] * 4) + 8);
            *(uint*)scene |= 0x1000;
        }

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
            *Mem.EntityStateBytes(0x005714a8) = slot;

            *(short*)(e + 0x98) = 0;
        }

        // La gamme, hors de tout etat.
        short beat = *(short*)(e + 0x98);
        if (beat == 0x00) Aud.PlaySoundEffect(0x20, 0x255, 0xa00, 0xa00);
        if (beat == 0x0a) Aud.PlaySoundEffect(0x20, 0x2aa, 0xa00, 0xa00);
        if (beat == 0x14) Aud.PlaySoundEffect(0x20, 0x2d4, 0xa00, 0xa00);
        if (beat == 0x1e) Aud.PlaySoundEffect(0x20, 0x329, 0xa00, 0xa00);
        if (beat == 0x28) Aud.PlaySoundEffect(0x20, 0x37e, 0xa00, 0xa00);

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) == 0x32) *(short*)(e + 0x98) = 0;

        if (*(short*)(e + 0x10) != 1) return;

        SetSceneBackdrop((short)(CrtRand() % 0x3c + 0xb4), 0x96, 0x96, 1);

        (*(short*)(e + 0x12))++;
        short since = *(short*)(e + 0x12);
        (*(short*)(e + 0x16))++;
        short age = *(short*)(e + 0x16);

        if (since >= 1 && age < 0x50)
        {
            (*(short*)(e + 0x14))++;
            short keptVariant = *(short*)(e + 0x14);
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;

            // Ghidra: `rand & 3` rangé dans [esp+0x2c] et jamais relu — code mort.
            CrtRand();

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfa, 0, -1);

            short now = *(short*)(e + 0x16);
            *(short*)(e + 0x14) = keptVariant;

            if ((now & 1) == 0 && now < 0x46)
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc2, 0, -1);
        }

        if (*(short*)(e + 0x16) > 0x64)
        {
            SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54),
                             (short)*(int*)(e + 0x58), 2);
        }

        if (*(short*)(e + 0x16) >= 0x8c) EndTowerScene(e, target, 0);

        if ((*(short*)(e + 0x16) & 1) == 0)
        {
            (*(short*)(target + 0x1a))++;
            short frame = *(short*)(target + 0x1a);
            short length = *(short*)(clip + 0x16);

            if (frame >= (length & 0xffff)) *(short*)(target + 0x1a) = (short)(length - 1);
        }

        // Ghidra: trois `movzx` puis trois soustractions et trois planchers separes — le rouge perd
        // 2, les deux autres 4.
        short r = (short)(target[0x38] - 2);
        short g = (short)(target[0x39] - 4);
        short b = (short)(target[0x3a] - 4);
        if (r < 0) r = 0;
        if (g < 0) g = 0;
        if (b < 0) b = 0;
        target[0x38] = (byte)r;
        target[0x39] = (byte)g;
        target[0x3a] = (byte)b;
    }
}
