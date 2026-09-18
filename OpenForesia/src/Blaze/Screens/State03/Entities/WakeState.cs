// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'EVEIL DES ENTITES DE SCENE — la machine a huit genres, 0x00404E50 a 0x00405FE0.
//
// Une entite de scene n'est pas toujours active : elle dort jusqu'a ce qu'une condition l'eveille.
// `WakeSceneEntityKind0` a `Kind7` sont les huit conditions, `ArmSceneEntityState_004051D0` arme,
// `AdvanceSceneEntityState_00404FE0` avance, `HoldSceneEntityState_004050D0` tient,
// `CountDownSceneEntityState_00405420` decompte, `CycleSceneEntityAnimations_004052F0` fait
// tourner les images.
//
// C'est l'infrastructure sous TOUTES les betes : aucune routine d'`Ai/` ne tourne tant que son
// entite n'est pas eveillee. Les trois remises a zero des bits d'eveil sont dans
// `Handlers/Common.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_00445860 @ 0x00445860 — l'entree <b>0x9d</b> de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/>, celle de la passe
    /// <see cref="TickEntityTable_005f2d40"/>. C'est le <b>premier</b> des 121 corps de gestionnaire
    /// de ces trois tables que <c>Blaze.c</c> decompile <b>et</b> dont un chemin reconstruit
    /// demontre qu'il est atteint : <see cref="EvaluatePartyDispersal"/> (etat 0xcd de
    /// <see cref="EntityAnimTail_00472E80"/>) engendre dans <c>0x005f2d40</c> avec ce sous-type.
    ///
    /// <para>
    /// L'entite est un <b>afficheur d'enonce</b> accroche a son porteur. Elle pose en <c>+0x3c</c>
    /// le bloc <c>&amp;DAT_00551640</c>, dont le flux d'etiquettes dit « 開かない！？ » — « ça ne
    /// s'ouvre pas ! ». Les huit premiers octets du bloc precedent le flux ; voir
    /// <c>BuildEntityParams_00551640</c> dans <c>Memory/GameMemory.cs</c>, ou les 0x200 octets sont
    /// releves.
    /// </para>
    /// <para>
    /// <b>Le porteur nul retombe sur <c>&amp;DAT_005de558</c></b> — l'entite factice de
    /// <see cref="Resources.s_EntityList_005de558"/>, deja rencontree ailleurs dans ce fichier. Ce
    /// n'est pas une garde defensive ajoutee par la transcription : le binaire fait
    /// <c>test eax,eax ; jne ; mov eax,5DE558h</c>, et tout le corps travaille ensuite sur ce
    /// substitut, y compris l'avance d'animation.
    /// </para>
    /// <para>
    /// Le porteur avance d'une image (<c>+0x1a</c>), bouclee sur le nombre d'images que declare sa
    /// planche d'animation — lue par <c>*(&amp;DAT_005e9680)[porteur[+4]]</c> puis <c>+0x16</c>. Les
    /// trois octets de teinte du porteur repartent a <c>0xff</c> et son bit <c>0x04</c> de
    /// <c>+0x36</c> tombe. Sans le bit <c>0x4000000</c> sur le porteur, l'afficheur s'eteint tout
    /// de suite : il ne vit qu'aussi longtemps que ce qu'il commente.
    /// </para>
    /// <para>
    /// L'echelle <c>+0x28</c>/<c>+0x2a</c> pulse a <c>0x1000 + sin(angle) / 4</c>, l'angle avancant
    /// de 400 par image sur douze bits. Le binaire appelle <c>Ordinal_451</c> <b>deux fois</b> avec
    /// le meme angle — table de sinus pure, donc deux fois la meme valeur ; les deux appels sont
    /// conserves.
    /// </para>
    /// </summary>
    private static void ShowEntityLabel_00445860(byte* effect)
    {
        byte* carrier = *(byte**)(effect + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;

        carrier[0x36] &= 0xfb;
        carrier[0x38] = 0xff;
        carrier[0x39] = 0xff;
        carrier[0x3a] = 0xff;
        *(ushort*)(carrier + 0x18) = 0;

        // Ghidra: `**(int **)(&DAT_005e9680 + (uint)(byte)puVar1[1] * 4)` — la planche
        // d'animation de la famille du porteur, dont +0x16 porte le nombre d'images.
        byte* sheet = *(byte**)*(byte***)(Mem.Arena(0x005e9680) + carrier[4] * 4);

        (*(short*)(carrier + 0x1a))++;
        if (*(short*)(carrier + 0x1a) >= (int)*(ushort*)(sheet + 0x16) - 1)
        {
            *(short*)(carrier + 0x1a) = 0;
        }

        if ((*(uint*)carrier & 0x4000000) == 0) *(uint*)effect &= 0x7fffffff;

        *(int*)(effect + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(effect + 0x20) = *(int*)(carrier + 0x60) + 0x96000;
        *(int*)(effect + 0x24) = *(int*)(carrier + 0x24);

        if (*(short*)(effect + 0x10) == 0)
        {
            *(byte**)(effect + 0x3c) = Mem.s_EntityParams_00551640;   // Ghidra: &DAT_00551640
            effect[0x0a] = 10;
            effect[0x0b] = 0;
            *(short*)(effect + 0x10) = 1;
            *(short*)(effect + 0x12) = 0;
            effect[0x38] = 0xff;
            effect[0x39] = 0xff;
            effect[0x3a] = 0xff;
        }

        if (*(short*)(effect + 0x10) == 1)
        {
            short angle = *(short*)(effect + 0x12);
            *(short*)(effect + 0x28) = (short)(((int)Winsys3d.GetSinTableEntry(angle) >> 2) + 0x1000);
            *(short*)(effect + 0x2a) = (short)(((int)Winsys3d.GetSinTableEntry(angle) >> 2) + 0x1000);
            *(ushort*)(effect + 0x12) = (ushort)((angle + 400) & 0xfff);
        }
    }

    /// <summary>
    /// Ghidra: 0x00404e50 — <b>le changement de musique differe</b>. Absent de <c>Blaze.c</c> ;
    /// releve au desassembleur, 151 octets (<c>0x00404e50</c>..<c>0x00404ee6</c>) —
    /// <c>Legacy/Raw/Blaze/FUN_00404E50.asm</c>.
    ///
    /// <para>Atteint le 2026-08-25 par la femme a la petite harpe de la salle derriere le barman,
    /// entree 1 de la table installee par zone (<c>0x005ff400</c>) : elle propose de changer de
    /// musique, et c'est cette entite-ci qui execute le changement une fois qu'on accepte.</para>
    ///
    /// <para><b>Deux moities, et la seconde attend la premiere.</b> A chaque image il decompte
    /// <b>300</b> sur les deux <c>short</c> de <c>+0x10</c> et <c>+0x12</c> du bloc d'ecran
    /// <see cref="s_MenuScreenBlock_00566c48"/>, en les bornant a zero par le bas, remet le mot de
    /// tete du bloc a <c>0xc0</c> et le republie par <c>Ordinal_762</c>. Tant que l'un des deux est
    /// encore non nul, il s'arrete la — l'entite reste armee et repassera a l'image suivante.
    /// Quand les <b>deux</b> sont a zero, il choisit la piste et la lance, puis se desarme.</para>
    ///
    /// <para><b>La premiere moitie existe deja seule</b>, dans <c>Blaze.c</c> cette fois :
    /// <c>FUN_0041af50</c> (ligne 11043) en est le decompte nu, sans entite, sans musique et sans
    /// desarmement. Elle n'est pas transcrite — rien ne l'appelle encore dans le graphe reconstruit
    /// — mais elle confirme la lecture du bloc et des deux champs.</para>
    ///
    /// <para><b>Le choix de piste est le meme calcul qu'ailleurs, ecrit autrement.</b> Ici c'est une
    /// cascade de <c>cmp al,1</c> / <c>cmp al,2</c>, la ou <c>ScriptOpAreaVerbMusic</c> emploie un
    /// <c>setne / dec / and 2</c> sans branchement. Les deux calculent l'identite bornee : 0, 1 et 2
    /// se rendent eux-memes, <b>toute autre valeur rend 0</b>. Deux codegens pour une meme regle,
    /// ce qui la confirme.</para>
    ///
    /// <para><b><c>Ordinal_762</c> ne fait rien dans cette version de la DLL</b> — etabli le
    /// 2026-09-18 : son export tombe sur <c>0x00a02870</c>, un <c>ret 4</c> nu que partagent des
    /// dizaines d'ordinaux (<c>Documentation/DLL/winsys3d.md</c>). Publier le bloc est donc sans
    /// effet ; le decompte, lui, compte, puisque ce corps le relit. Les deux champs sont transcrits
    /// pour ce qu'ils sont — deux <c>short</c> du bloc — sans leur preter un role.</para>
    /// </summary>
    private static void EntityHandler_00404E50(uint* entity)
    {
        byte* block = (byte*)s_MenuScreenBlock_00566c48;

        // Ghidra: `movsx ebx,word [566C58h]` — la lecture est SIGNEE, et c'est ce qui rend la
        // borne a zero necessaire : sans elle le decompte passerait sous zero et repartirait.
        int a = *(short*)(block + 0x10) - 300;
        int b = *(short*)(block + 0x12) - 300;

        *(uint*)block = 0xc0;

        if (a < 0) a = 0;
        if (b < 0) b = 0;

        *(short*)(block + 0x10) = (short)a;
        *(short*)(block + 0x12) = (short)b;

        Winsys3d.Ordinal_762(block);

        if (a != 0 || b != 0) return;                      // encore en cours : on repassera

        byte source = *Mem.Arena(0x005e96fe);
        byte track = source <= 2 ? source : (byte)0;       // Ghidra: la cascade cmp al,1 / cmp al,2

        s_Unknown_005de534 = track;
        s_Unknown_00566c8c = track;
        PlayMusicTrack(track);                             // Ghidra: FUN_00529ad0

        entity[0] &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00404b90</c> — <b>le sous-type <c>0xfe</c></b> de la passe qui lit
    /// <see cref="s_StaticEntityHandlers_00551c30"/> sous <c>0xc0</c> et la table installee
    /// <c>0x0056adc8</c> au-dela : <c>0x0056adc8 + 0xfe * 4</c> deborde sur <c>0x0056b0c8</c>,
    /// creneau 62. 232 octets, <b>absente de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00404B90.asm</c>. Voisine de <see cref="EntityHandler_00404E50"/>,
    /// qui est le <c>0xff</c> de la meme table.
    ///
    /// <para>
    /// <b>Elle se colle a l'entite de scene 0</b> — <c>0x005ecabc</c>, <c>0x005ecac0</c> et
    /// <c>0x005ecac4</c> sont les trois coordonnees du <b>premier</b> enregistrement de
    /// <c>0x005ecaa0</c>, lues en absolu et non par un pointeur qu'on lui aurait passe. Elle ne
    /// suit donc pas « sa » cible : elle suit le creneau 0, quel qu'il soit.
    /// </para>
    /// <para>
    /// <b>Trente images, et rien d'autre a faire que semer.</b> Elle lache <b>trois</b> entites
    /// <c>0xff</c> (<see cref="EntityHandler_00404C80"/>) a <b>chaque</b> image, sur sa propre
    /// position — quatre-vingt-dix en tout — et s'eteint quand <c>+0x16</c> retombe a zero.
    /// </para>
    /// <para>
    /// <b>Trois notes, pas trois bruits.</b> Le meme son <c>0x20</c> part aux images 30, 22 et 14
    /// du decompte, a trois hauteurs montantes — <c>0x255</c>, <c>0x2aa</c>, <c>0x2d4</c> — par
    /// <see cref="Audio.PlaySoundEffect"/> et non par la porte de distance : le son ne depend pas
    /// de l'endroit ou l'on est. C'est un arpege, joue en clair dans le corps.
    /// </para>
    /// </summary>
    private static void EntityHandler_00404B90(byte* e)
    {
        // Ghidra: trois adresses absolues, 0x005ecabc / 0x005ecac0 / 0x005ecac4 — le +0x1c, +0x20
        // et +0x24 de l'entite de scene 0.
        byte* anchor = Mem.Entity(0x005ecaa0);

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0x1e;
        }

        // Ghidra: LAB_00404bc5 — les trois naissances sont ecrites l'une apres l'autre, pas en
        // boucle, et la premiere image passe ici aussi.
        for (int i = 0; i < 3; i++)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xff, 0, -1);
        }

        // Ghidra: FUN_0053cea0 en direct — hauteur montante aux images 30, 22 et 14.
        if (*(short*)(e + 0x16) == 0x1e) Aud.PlaySoundEffect(0x20, 0x255, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x16) Aud.PlaySoundEffect(0x20, 0x2aa, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x0e) Aud.PlaySoundEffect(0x20, 0x2d4, 0x1000, 0x1000);

        *(short*)(e + 0x16) -= 1;
        if (*(short*)(e + 0x16) == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f160</c> — <b>l'atlas de l'etincelle <c>0xff</c></b>, celui que
    /// <see cref="EntityHandler_00404C80"/> pose en <c>+0x3c</c>. <b>Quatre enregistrements</b>, et
    /// les deux bouts de la plage se demontrent : elle commence ou finit
    /// <see cref="s_Descriptors_0054F158"/>, et elle est close par la <b>reapparition du meme
    /// separateur</b> <c>00 00 08 08 01 02 10 00</c> a <c>0x0054f180</c> — le motif releve le
    /// 2026-08-29 en meme temps que son jumeau. Le corps le confirme : <c>+0x40</c> reboucle des
    /// qu'il depasse 3.
    ///
    /// <para>Les quatre tuiles sont la meme planche <c>0x18 x 0x20</c> prise de <c>0x00</c> en
    /// <c>0x18</c> : <c>0x20184800</c>, <c>0x20184818</c>, <c>0x20184830</c>, <c>0x20184848</c>.</para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F160 =
    [
        0x00, 0x48, 0x18, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f160
        0x18, 0x48, 0x18, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f168
        0x30, 0x48, 0x18, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f170
        0x48, 0x48, 0x18, 0x20, 0x01, 0x02, 0x10, 0x00,   // 0x0054f178
    ];

    private static byte* s_Descriptors0054F160Storage;

    private static byte* Descriptors0054F160()
        => NativeCopy(s_Descriptors_0054F160, ref s_Descriptors0054F160Storage);

    /// <summary>
    /// Ghidra: FUN_00404c80 @ 0x00404c80 — <b>le sous-type <c>0xff</c></b> de la passe
    /// <see cref="TickEntityTable_005f2d40"/> : <c>0x0056afc8 + 0xff * 4</c> deborde sur
    /// <c>0x0056b2c8</c>, creneau 63. Decompilee dans <c>Blaze.c</c>. C'est ce que
    /// <see cref="EntityHandler_00404B90"/> lache par trois a chaque image.
    ///
    /// <para>
    /// <b>Une etincelle qui monte en tournant et s'eteint d'etre noircie.</b> Elle fige son ancre
    /// <c>+0x50</c>/<c>+0x58</c> a la naissance, se donne un rayon (20 a 59), un pas angulaire
    /// (100 a 199) et un cap tire sur douze bits, puis recalcule <c>X</c> et <c>Z</c> a chaque
    /// image autour de l'ancre pendant que <c>Y</c> gagne <c>+0x54 &times; 0x1000</c> — une montee
    /// constante, tiree entre 20 et 39 a la naissance.
    /// </para>
    /// <para>
    /// <b>Le produit n'est pas recadre</b> : l'entree de table de sinus multiplie le rayon tel
    /// quel, sans <c>&gt;&gt; 0xc</c>. Meme convention que <see cref="TickHolySpark_0044FE00"/>,
    /// et c'est ce qui fait tenir un rayon de quelques dizaines a l'echelle du monde.
    /// </para>
    /// <para>
    /// <b>Sa mort est dans sa teinte.</b> Elle part entre <c>0x5a</c> et <c>0xa9</c> sur les trois
    /// canaux et perd <c>+0x9a</c> — un pas <b>negatif</b>, entre -10 et -19, tire a la naissance —
    /// par image. <see cref="BrightenEntityTint_00404DE0"/> borne a zero, et l'entite s'eteint des
    /// que le canal rouge y touche : elle vit donc 4 a 17 images selon les deux tirages.
    /// </para>
    /// <para>
    /// <b>Ghidra passe un <c>CONCAT22</c> aux trois arguments de teinte</b> : le binaire empile un
    /// registre 32 bits dont seuls les seize bits bas portent <c>+0x9a</c>, les hauts trainant le
    /// <c>Y</c> qui vient d'etre calcule. La fonction appelee ne lit que seize bits ; c'est un
    /// artefact de decompilation, pas un argument compose.
    /// </para>
    /// </summary>
    private static void EntityHandler_00404C80(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F160();   // Ghidra: &DAT_0054f160
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x28 + 0x14);   // le rayon
            *(short*)(e + 0x14) = (short)(CrtRand() % 100 + 100);     // le pas angulaire
            *(ushort*)(e + 0x16) = (ushort)(CrtRand() % 0x1000);      // le cap

            // L'ancre : la position de naissance, autour de laquelle le tour se dessine.
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x54) = CrtRand() % 0x14 + 0x14;              // la montee par image
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            byte tint = (byte)(CrtRand() % 0x50 + 0x5a);
            e[0x38] = tint;
            e[0x39] = tint;
            e[0x3a] = tint;

            // Le pas d'assombrissement, negatif : -10 a -19.
            *(short*)(e + 0x9a) = (short)(-10 - CrtRand() % 10);

            short scale = (short)(CrtRand() % 0x400 + 0xdac);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short yaw = *(short*)(e + 0x16);
            short radius = *(short*)(e + 0x12);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * radius + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * radius + *(int*)(e + 0x58);

            *(int*)(e + 0x20) += *(int*)(e + 0x54) * 0x1000;

            *(short*)(e + 0x16) += *(short*)(e + 0x14);

            short fade = *(short*)(e + 0x9a);
            BrightenEntityTint_00404DE0(e, fade, fade, fade);

            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;

            e[0x40]++;
            if (e[0x40] > 3) e[0x40] = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00404ef0 @ 0x00404ef0 — le gestionnaire <b>0x404EF0</b> de la table des effets
    /// <c>0x005ff400</c> : il ne touche pas a sa propre entite mais <b>balaie les 0x20
    /// enregistrements de la table <c>0x005ecaa0</c></b> et reveille ceux qui portent a la fois le
    /// bit <c>0x80000000</c> et le bit <c>8</c>, en aiguillant sur leur octet <c>+0x0c</c>.
    ///
    /// <para>Il se desarme lui-meme en sortant (bit <c>0x80000000</c> de son propre mot d'etat), ce
    /// qui en fait une impulsion : une passe, une seule fois.</para>
    /// </summary>
    private static void EntityHandler_00404EF0(uint* entity)
    {
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            uint flags = *(uint*)record;
            if ((flags & 0x80000000) == 0 || (flags & 8) == 0) continue;

            switch (record[0x0c])
            {
                case 0: WakeSceneEntityKind0_00404F70(i); break;
                case 1: WakeSceneEntityKind1_00405240(i); break;
                case 2: WakeSceneEntityKind2_00405510(i); break;
            }
        }

        entity[0] &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00404f70 @ 0x00404f70 — <b>le reveil d'une entite de scene de type 0</b> : il
    /// arme l'etat, puis aiguille sur les trente bits bas du mot <c>+0x5c</c>.
    ///
    /// <para>Les trois tests relisent <c>+0x5c</c> a chaque fois, et ce n'est pas une maladresse de
    /// transcription : <see cref="AdvanceSceneEntityState_00404FE0"/> et
    /// <see cref="HoldSceneEntityState_004050D0"/> ecrivent ce meme mot, donc un etat peut en
    /// declencher un autre dans le meme tour. Conserve tel quel.</para>
    /// </summary>
    private static void WakeSceneEntityKind0_00404F70(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2) HoldSceneEntityState_004050D0(record, 0);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) AdvanceSceneEntityState_00404FE0(record, 3);
    }

    /// <summary>
    /// Ghidra: FUN_004051d0 @ 0x004051d0 — <b>arme l'etat d'animation</b> d'une entite de scene
    /// depuis ses trois champs de consigne, avant que l'un des deux pas ci-dessous ne le fasse
    /// avancer.
    ///
    /// <para>Le mot <c>+0x5c</c> porte trois choses a la fois : ses trente bits bas sont le
    /// <b>numero d'etat</b>, le bit <c>0x40000000</c> dit « la consigne est deja lue » et le bit
    /// <c>0x80000000</c> « l'etat vient de changer ». Le dernier est efface en sortant, si bien
    /// qu'il ne vaut que pour un tour.</para>
    /// </summary>
    private static void ArmSceneEntityState_004051D0(byte* record)
    {
        uint wanted = (uint)(*(short*)(record + 0x10) & 0xff);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) != wanted
            && (*(uint*)(record + 0x5c) & 0x80000000) != 0)
        {
            *(uint*)(record + 0x5c) = wanted;
            *(int*)(record + 0x48) = *(short*)(record + 0x14);
        }

        if ((*(uint*)(record + 0x5c) & 0x40000000) == 0)
        {
            *(int*)(record + 0x44) = *(short*)(record + 0x10) >> 8;
            *(int*)(record + 0x4c) = *(short*)(record + 0x12);
            *(uint*)(record + 0x5c) |= 0x40000000;
        }

        *(uint*)(record + 0x5c) &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00404fe0 @ 0x00404fe0 — <b>le pas d'animation qui va jusqu'au bout</b> : il
    /// avance le compteur d'images et, arrive a la derniere, lance le script accroche a l'entite.
    ///
    /// <para>Le champ <c>+0x48</c> est un compteur a double usage — au-dessus de 9 c'est un delai
    /// qui decompte, en dessous c'est un numero de phase (0 = a amorcer, 1 = en cours, 2 = fini et
    /// gele). Le passage de delai a phase se fait par le test <c>&lt; 10</c> qui le remet a zero.</para>
    ///
    /// <para><b>La fin de sequence a deux regimes</b>, selon <c>+0x4c</c> : a zero l'animation
    /// reboucle (compteur remis a zero), sinon elle <b>se fige sur sa derniere image</b>. Dans les
    /// deux cas, si <c>+0x44</c> porte un script, il part et le champ est vide pour qu'il ne parte
    /// qu'une fois.</para>
    ///
    /// <para>Ghidra rend la valeur de retour en <c>CONCAT22</c> avec une moitie haute qui n'est que
    /// du residu de registre ; <b>aucun appelant ne la lit</b>, et seule la moitie basse a un sens :
    /// 1 si un script a demarre, 0 sinon.</para>
    /// </summary>
    private static uint AdvanceSceneEntityState_00404FE0(byte* record, short kind)
    {
        int counter = *(int*)(record + 0x48);
        if (counter > 9)
        {
            counter--;
            *(int*)(record + 0x48) = counter;
            if (counter < 10) *(int*)(record + 0x48) = 0;
        }

        if (*(int*)(record + 0x48) == 0)
        {
            *(int*)(record + 0x48) = 1;
            *(short*)(record + 0x1a) = -1;
            *(short*)(record + 0x18) = kind;
        }

        if (*(int*)(record + 0x48) != 1) return 0;

        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + record[4] * 4))
                              [*(short*)(record + 0x18)];

        *(short*)(record + 0x1a) += 1;

        ushort frames = *(ushort*)(animation + 0x16);
        if (frames - 1 > *(short*)(record + 0x1a)) return 0;

        int script;
        if (*(int*)(record + 0x4c) == 0)
        {
            script = *(int*)(record + 0x44);
            *(short*)(record + 0x1a) = 0;
            *(uint*)(record + 0x5c) |= 0x80000000;
            if (script == 0) return 0;
        }
        else
        {
            *(ushort*)(record + 0x1a) = (ushort)(frames - 1);
            script = *(int*)(record + 0x44);
            *(uint*)(record + 0x5c) |= 0x80000000;
            if (script == 0)
            {
                *(int*)(record + 0x48) = 2;
                return 0;
            }
        }

        RunEntityScript(s_Unknown_005e97cc, *(byte**)(record + 0x6c), (uint)script);
        *(uint*)(record + 0x5c) |= 0x80000000;
        *(int*)(record + 0x44) = 0;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_004050d0 @ 0x004050d0 — <b>la jumelle qui n'attend pas la fin de la sequence</b>.
    /// Meme compteur a double usage, meme fin en deux regimes, mais elle declenche des la
    /// <b>premiere</b> image (<c>0 &lt; +0x1a</c>) au lieu d'attendre la derniere.
    ///
    /// <para>Autre difference, plus discrete : elle lit l'entree d'animation <b>comme une valeur</b>
    /// et ne la dereference que dans la branche qui fige (<c>*(short *)(uVar1 + 0x16)</c>). Dans
    /// l'autre branche l'entree n'est jamais touchee — transcrit tel quel.</para>
    /// </summary>
    private static uint HoldSceneEntityState_004050D0(byte* record, short kind)
    {
        int counter = *(int*)(record + 0x48);
        if (counter > 9)
        {
            counter--;
            *(int*)(record + 0x48) = counter;
            if (counter < 10) *(int*)(record + 0x48) = 0;
        }

        if (*(int*)(record + 0x48) == 0)
        {
            *(int*)(record + 0x48) = 1;
            *(short*)(record + 0x1a) = -1;
            *(short*)(record + 0x18) = kind;
        }

        if (*(int*)(record + 0x48) != 1) return 0;

        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + record[4] * 4))
                              [*(short*)(record + 0x18)];

        *(short*)(record + 0x1a) += 1;
        if (*(short*)(record + 0x1a) <= 0) return 0;

        if (*(int*)(record + 0x4c) == 0)
        {
            *(short*)(record + 0x1a) = 0;
            *(uint*)(record + 0x5c) |= 0x80000000;

            if (*(int*)(record + 0x44) == 0) return 0;
        }
        else
        {
            *(short*)(record + 0x1a) = (short)(*(short*)(animation + 0x16) - 1);
            *(uint*)(record + 0x5c) |= 0x80000000;

            if (*(int*)(record + 0x44) == 0)
            {
                *(int*)(record + 0x48) = 2;
                return 0;
            }
        }

        RunEntityScript(s_Unknown_005e97cc, *(byte**)(record + 0x6c), (uint)*(int*)(record + 0x44));
        *(uint*)(record + 0x5c) |= 0x80000000;
        *(int*)(record + 0x44) = 0;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_00405240 @ 0x00405240 — <b>le reveil d'une entite de scene de type 1</b>. Meme
    /// forme que <see cref="WakeSceneEntityKind0_00404F70"/> : on arme l'etat, puis on aiguille sur
    /// les trente bits bas de <c>+0x5c</c>, cinq branches cette fois au lieu de trois, chacune
    /// relisant le mot parce que la precedente a pu le changer.
    /// </summary>
    private static void WakeSceneEntityKind1_00405240(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
            CycleSceneEntityAnimations_004052F0(record, 2, 3, (short)(CrtRand() % 10));
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) CountDownSceneEntityState_00405420(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) AdvanceSceneEntityState_00404FE0(record, 3);
    }

    /// <summary>
    /// Ghidra: FUN_00405510 @ 0x00405510 — <b>le reveil d'une entite de scene de type 2</b>, la
    /// jumelle de la precedente. Les cinq branches sont les memes a leurs arguments pres : l'etat 3
    /// avance ici au lieu de decompter, et les numeros d'animation sont decales d'un cran.
    /// </summary>
    private static void WakeSceneEntityKind2_00405510(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
            CycleSceneEntityAnimations_004052F0(record, 3, 5, (short)(CrtRand() % 10));
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) AdvanceSceneEntityState_00404FE0(record, 3);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 4);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) AdvanceSceneEntityState_00404FE0(record, 5);
    }

    /// <summary>
    /// Ghidra: FUN_004055c0 @ 0x004055c0 — <b>le second balayeur d'entites de scene</b>, jumeau de
    /// <see cref="EntityHandler_00404EF0"/> : meme forme, meme table <c>0x005ecaa0</c>, meme
    /// auto-desarmement en sortant, mais il reveille les types <b>3, 4, 5, 7 et 6</b> la ou son
    /// jumeau reveille 0, 1 et 2.
    ///
    /// <para>
    /// <b>Il n'est pas atteint par la branche basse.</b> C'est l'entree <c>0xe1</c> — donc
    /// <c>&gt;= 0xC0</c> — de la table installee par zone, celle qui deborde de trois tables et
    /// retombe dans <c>s_Vt_0056b0c8</c>. Le sous-type ne vient donc pas de <c>FUN_0047a9d0</c>,
    /// l'engendreur qui pose toujours <c>0x59</c> : il vient d'un des six sites qui ecrivent
    /// directement dans <c>DAT_005ff400</c> au chargement de la zone.
    /// </para>
    /// <para>
    /// Les cinq tests <b>relisent</b> le mot d'etat et l'octet de type a chaque fois. Ce n'est pas
    /// une maladresse de transcription : un reveil peut changer l'un comme l'autre, et le binaire
    /// recharge <c>[esi]</c> avant chaque comparaison.
    /// </para>
    /// <para>L'ordre 3, 4, 5, <b>7</b>, 6 est celui du binaire. Conserve tel quel.</para>
    /// </summary>
    private static void EntityHandler_004055C0(uint* entity)
    {
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if (record[0x0c] == 3 && IsSceneEntityAwake(record)) WakeSceneEntityKind3_00405680(i);
            if (record[0x0c] == 4 && IsSceneEntityAwake(record)) WakeSceneEntityKind4_00405770(i);
            if (record[0x0c] == 5 && IsSceneEntityAwake(record)) WakeSceneEntityKind5_00405840(i);
            if (record[0x0c] == 7 && IsSceneEntityAwake(record)) WakeSceneEntityKind7_004059A0(i);
            if (record[0x0c] == 6 && IsSceneEntityAwake(record)) WakeSceneEntityKind6_004058F0(i);
        }

        entity[0] &= 0x7fffffff;
    }

    /// <summary>Les deux bits que les balayeurs testent avant chaque reveil — vivante
    /// (<c>0x80000000</c>) et eveillee (<c>8</c>). Relus a chaque test.</summary>
    private static bool IsSceneEntityAwake(byte* record)
        => (*(uint*)record & 0x80000000) != 0 && (*(uint*)record & 8) != 0;

    /// <summary>
    /// Ghidra: FUN_00405680 @ 0x00405680 — <b>le reveil d'une entite de scene de type 3</b>, et le
    /// seul des huit qui <b>deplace</b> son entite : aux etats 1 et 3, si le pas d'animation vient
    /// d'atteindre sa fin (<c>+0x48 == 1</c>) et que le bit « l'etat vient de changer » est retombe,
    /// il decale <c>+0x32</c> de <c>-0x22</c> puis de <c>+0x22</c>.
    ///
    /// <para>Aller-retour de 34 unites, donc, sur ce seul champ : une porte qui s'ouvre et se
    /// referme, ou un battant. Les deux decalages sont des additions <b>16 bits</b> dans le binaire
    /// (<c>add word [esi+32h],0FFDEh</c>), pas des ecritures de dword.</para>
    ///
    /// <para>Il sert aussi de type 0 au troisieme balayeur, <c>FUN_00405a70</c>, non transcrit.</para>
    /// </summary>
    private static void WakeSceneEntityKind3_00405680(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1)
        {
            AdvanceSceneEntityState_00404FE0(record, 1);
            if (*(int*)(record + 0x48) == 1 && (*(uint*)(record + 0x5c) & 0x80000000) == 0)
            {
                *(short*)(record + 0x32) -= 0x22;
            }
        }

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
            CycleSceneEntityAnimations_004052F0(record, 2, 4, (short)(CrtRand() % 10));

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3)
        {
            CountDownSceneEntityState_00405420(record, 1);
            if (*(int*)(record + 0x48) == 1 && (*(uint*)(record + 0x5c) & 0x80000000) == 0)
            {
                *(short*)(record + 0x32) += 0x22;
            }
        }

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) AdvanceSceneEntityState_00404FE0(record, 3);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 6) AdvanceSceneEntityState_00404FE0(record, 4);
    }

    /// <summary>
    /// Ghidra: FUN_00405770 @ 0x00405770 — <b>type 4</b>. Exactement
    /// <see cref="WakeSceneEntityKind3_00405680"/> <b>sans les deux decalages de <c>+0x32</c></b> :
    /// memes six etats, memes numeros d'animation. Ce qui distingue les deux types est donc le
    /// deplacement, et rien d'autre.
    /// </summary>
    private static void WakeSceneEntityKind4_00405770(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
            CycleSceneEntityAnimations_004052F0(record, 2, 4, (short)(CrtRand() % 10));
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) CountDownSceneEntityState_00405420(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) AdvanceSceneEntityState_00404FE0(record, 3);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 6) AdvanceSceneEntityState_00404FE0(record, 4);
    }

    /// <summary>
    /// Ghidra: FUN_00405840 @ 0x00405840 — <b>type 5</b>. Cinq etats au lieu de six, et une plage
    /// d'inactivite plus courte (animations 1 a 2). Le decompte de l'etat 3 porte sur l'animation 2.
    /// </summary>
    private static void WakeSceneEntityKind5_00405840(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
            CycleSceneEntityAnimations_004052F0(record, 1, 2, (short)(CrtRand() % 10));
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) CountDownSceneEntityState_00405420(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) AdvanceSceneEntityState_00404FE0(record, 2);
    }

    /// <summary>
    /// Ghidra: FUN_004058f0 @ 0x004058f0 — <b>type 6</b>, et le seul des huit dont la pause
    /// d'inactivite soit <b>decalee de dix</b> : <c>rand % 10 + 10</c> au lieu de <c>rand % 10</c>.
    /// Il se fige donc deux fois plus longtemps sur chaque animation.
    ///
    /// <para>Son etat 4 avance l'animation <b>0</b>, ce qu'aucun autre ne fait.</para>
    /// </summary>
    private static void WakeSceneEntityKind6_004058F0(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 4);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
            CycleSceneEntityAnimations_004052F0(record, 2, 3, (short)(CrtRand() % 10 + 10));
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) CountDownSceneEntityState_00405420(record, 4);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 0);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) AdvanceSceneEntityState_00404FE0(record, 5);
    }

    /// <summary>
    /// Ghidra: FUN_004059a0 @ 0x004059a0 — <b>type 7</b>. Six etats, plage d'inactivite 3 a 4, et
    /// l'etat 6 qui avance l'animation 2 — le seul a redescendre apres les etats 4 et 5.
    /// </summary>
    private static void WakeSceneEntityKind7_004059A0(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
            CycleSceneEntityAnimations_004052F0(record, 3, 4, (short)(CrtRand() % 10));
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) CountDownSceneEntityState_00405420(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 3);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) AdvanceSceneEntityState_00404FE0(record, 4);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 6) AdvanceSceneEntityState_00404FE0(record, 2);
    }

    /// <summary>
    /// Ghidra: FUN_00405a70 @ 0x00405a70 — <b>le troisieme balayeur d'entites de scene</b>. Meme
    /// forme et meme table <c>0x005ecaa0</c> que <see cref="EntityHandler_00404EF0"/> et
    /// <see cref="EntityHandler_004055C0"/>, meme auto-desarmement en sortant. Il reveille les types
    /// <b>0 a 4</b>, dans l'ordre.
    ///
    /// <para><b>Les numeros de type sont relatifs au balayeur, pas au corps.</b> Son type 0 est
    /// <see cref="WakeSceneEntityKind3_00405680"/> — la meme fonction que le second balayeur appelle
    /// pour son type 3. Le suffixe d'adresse est donc la seule identite stable des huit corps, et
    /// c'est pourquoi les quatre nouveaux ci-dessous ne portent <b>pas</b> de numero de type : il
    /// n'y en a pas un seul a porter.</para>
    ///
    /// <para>Atteint le 2026-08-25 dans la salle derriere le barman, entree 0 de la table installee
    /// par zone (<c>0x005ff400</c>).</para>
    /// </summary>
    private static void EntityHandler_00405A70(uint* entity)
    {
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if (record[0x0c] == 0 && IsSceneEntityAwake(record)) WakeSceneEntityKind3_00405680(i);
            if (record[0x0c] == 1 && IsSceneEntityAwake(record)) WakeSceneEntity_00405B30(i);
            if (record[0x0c] == 2 && IsSceneEntityAwake(record)) WakeSceneEntity_00405BE0(i);
            if (record[0x0c] == 3 && IsSceneEntityAwake(record)) WakeSceneEntity_00405CB0(i);
            if (record[0x0c] == 4 && IsSceneEntityAwake(record)) WakeSceneEntity_00405D20(i);
        }

        entity[0] &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00405b30 @ 0x00405b30 — trois etats seulement, et <b>le seul des douze corps de
    /// reveil qui fasse du bruit</b> : a l'etat 1, si l'animation courante est exactement la 2 et
    /// que son compteur d'images vaut 0x12, il joue le son <c>+0x10</c> de la table de la zone.
    ///
    /// <para>La condition est etroite a dessein — une image precise d'une animation precise. C'est
    /// un bruitage cale sur un geste : le pas d'un pied, le choc d'un objet pose.</para>
    ///
    /// <para>Les deux champs testes sont <c>+0x1a</c> (le compteur d'images, celui que
    /// <see cref="AdvanceSceneEntityState_00404FE0"/> fait courir) et <c>+0x18</c> (le numero
    /// d'animation). Ghidra les rend en <c>(&amp;DAT_005ecaba)[i * 0x4e]</c> et
    /// <c>(&amp;DAT_005ecab8)[i * 0x4e]</c> — des <b>short</b>, pas des octets.</para>
    /// </summary>
    private static void WakeSceneEntity_00405B30(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1)
        {
            AdvanceSceneEntityState_00404FE0(record, 2);

            if (*(short*)(record + 0x1a) == 0x12 && *(short*)(record + 0x18) == 2)
            {
                byte* sounds = *(byte**)(*(byte***)(s_Unknown_005e97cc + 0xa8));
                TryPlaySoundNearEntity((uint*)record, sounds[0x10], 0x400, 0x1000, 0, 0);
            }
        }

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2) AdvanceSceneEntityState_00404FE0(record, 0);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) AdvanceSceneEntityState_00404FE0(record, 1);
    }

    /// <summary>
    /// Ghidra: FUN_00405be0 @ 0x00405be0 — six etats, plage d'inactivite 2 a 4. C'est
    /// <see cref="WakeSceneEntityKind3_00405680"/> <b>sans les deux decalages de <c>+0x32</c></b>,
    /// donc le meme corps que <see cref="WakeSceneEntityKind4_00405770"/> — <b>a la lettre</b>.
    ///
    /// <para>Verifie plutot que suppose, comme la regle du depot le demande : les deux corps font
    /// 208 octets, et le desassemblage des deux ne differe que par les <c>jne short</c> internes,
    /// relogeables par nature. <b>Aucun <c>call</c> ne differe</b> — memes cibles, meme ordre.
    /// Le compilateur a emis le corps deux fois ; les deux adresses restent distinctes parce que la
    /// table de sauts les distingue.</para>
    /// </summary>
    private static void WakeSceneEntity_00405BE0(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
            CycleSceneEntityAnimations_004052F0(record, 2, 4, (short)(CrtRand() % 10));
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) CountDownSceneEntityState_00405420(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) AdvanceSceneEntityState_00404FE0(record, 3);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 6) AdvanceSceneEntityState_00404FE0(record, 4);
    }

    /// <summary>
    /// Ghidra: FUN_00405cb0 @ 0x00405cb0 — trois etats, sans inactivite : deux pas d'animation puis
    /// un decompte. Le plus court des corps a plusieurs branches.
    /// </summary>
    private static void WakeSceneEntity_00405CB0(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) CountDownSceneEntityState_00405420(record, 1);
    }

    /// <summary>
    /// Ghidra: FUN_00405d20 @ 0x00405d20 — <b>un seul etat</b>. Il arme, et si l'etat vaut 1 il
    /// avance l'animation 0. Un decor qui joue une boucle et rien d'autre.
    /// </summary>
    private static void WakeSceneEntity_00405D20(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 0);
    }


    /// <summary>
    /// Ghidra: 0x00405d60 — <b>le quatrieme balayeur d'entites de scene</b>, et le seul des six qui
    /// soit <b>absent de <c>Blaze.c</c></b> : Ghidra ne l'a pas reconnu comme fonction parce qu'il
    /// n'est atteint que par la table. Releve au desassembleur, 64 octets —
    /// <c>Legacy/Raw/Blaze/FUN_00405D60.asm</c>.
    ///
    /// <para>Un seul type reveille, le <b>0</b>. C'est le plus depouille de la famille : meme
    /// boucle de 0x20 emplacements, meme garde « vivante et eveillee », meme auto-desarmement, mais
    /// un seul test par emplacement.</para>
    ///
    /// <para>Atteint le 2026-08-25 dans la chambre de l'homme qui analyse les objets.</para>
    /// </summary>
    private static void EntityHandler_00405D60(uint* entity)
    {
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if (record[0x0c] == 0 && IsSceneEntityAwake(record)) WakeSceneEntity_00405DA0(i);
        }

        entity[0] &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00405e70 @ 0x00405e70 — <b>le cinquieme balayeur</b>. Deux types, 0 et 1, dans
    /// cet ordre.
    /// </summary>
    private static void EntityHandler_00405E70(uint* entity)
    {
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if (record[0x0c] == 0 && IsSceneEntityAwake(record)) WakeSceneEntity_00405ED0(i);
            if (record[0x0c] == 1 && IsSceneEntityAwake(record)) WakeSceneEntity_00405F50(i);
        }

        entity[0] &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00405fe0 @ 0x00405fe0 — <b>le sixieme et dernier balayeur</b>. Six types, et
    /// c'est le seul qui commence par le type <b>5</b> : l'ordre du binaire est 5, 1, 2, 3, 4, 0.
    ///
    /// <para>Ses cinq derniers appels sont exactement ceux de
    /// <see cref="EntityHandler_00405A70"/> ; il n'ajoute que le type 5. Conserver l'ordre compte —
    /// un reveil peut changer l'octet de type, et le binaire le relit avant chaque test.</para>
    /// </summary>
    private static void EntityHandler_00405FE0(uint* entity)
    {
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if (record[0x0c] == 5 && IsSceneEntityAwake(record)) WakeSceneEntity_00405DA0(i);
            if (record[0x0c] == 1 && IsSceneEntityAwake(record)) WakeSceneEntity_00405B30(i);
            if (record[0x0c] == 2 && IsSceneEntityAwake(record)) WakeSceneEntity_00405BE0(i);
            if (record[0x0c] == 3 && IsSceneEntityAwake(record)) WakeSceneEntity_00405CB0(i);
            if (record[0x0c] == 4 && IsSceneEntityAwake(record)) WakeSceneEntity_00405D20(i);
            if (record[0x0c] == 0 && IsSceneEntityAwake(record)) WakeSceneEntityKind3_00405680(i);
        }

        entity[0] &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00405da0 @ 0x00405da0 — <b>sept etats</b>, le plus long des quinze corps de
    /// reveil. Cinq pas d'animation (0, 1, 2, 3 puis 4) et <b>deux</b> decomptes, aux etats 5 et 7.
    /// Aucune inactivite : ce corps ne passe jamais par
    /// <see cref="CycleSceneEntityAnimations_004052F0"/>.
    /// </summary>
    private static void WakeSceneEntity_00405DA0(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 0);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) AdvanceSceneEntityState_00404FE0(record, 3);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 5) CountDownSceneEntityState_00405420(record, 3);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 6) AdvanceSceneEntityState_00404FE0(record, 4);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 7) CountDownSceneEntityState_00405420(record, 1);
    }

    /// <summary>
    /// Ghidra: FUN_00405ed0 @ 0x00405ed0 — quatre etats : trois pas d'animation puis un decompte.
    /// C'est <see cref="WakeSceneEntity_00405CB0"/> avec une animation de plus intercalee — le
    /// decompte y est a l'etat 3, ici a l'etat 4.
    /// </summary>
    private static void WakeSceneEntity_00405ED0(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2) AdvanceSceneEntityState_00404FE0(record, 2);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3) AdvanceSceneEntityState_00404FE0(record, 3);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) CountDownSceneEntityState_00405420(record, 1);
    }

    /// <summary>
    /// Ghidra: FUN_00405f50 @ 0x00405f50 — quatre etats, et le seul de la famille dont l'inactivite
    /// ait une pause <b>fixe</b> : <c>FUN_004052f0(…, 2, 3, 1)</c>. Les <b>neuf</b> sites qui
    /// appellent cette primitive sont recenses ; les huit autres passent <c>rand() % 10</c> (un avec
    /// un <c>+ 10</c>). Ce decor-la est le seul qui ne respire pas au hasard.
    /// </summary>
    private static void WakeSceneEntity_00405F50(int index)
    {
        byte* record = Mem.Entity(0x005ecaa0) + index * 0x9c;

        ArmSceneEntityState_004051D0(record);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1) AdvanceSceneEntityState_00404FE0(record, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2) AdvanceSceneEntityState_00404FE0(record, 0);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 3)
            CycleSceneEntityAnimations_004052F0(record, 2, 3, 1);
        if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 4) CountDownSceneEntityState_00405420(record, 2);
    }
    /// <summary>
    /// Ghidra: FUN_004052f0 @ 0x004052f0 — <b>parcourt une plage d'animations en marquant une pause
    /// sur chacune</b>. C'est l'inactivite d'un personnage non joueur : il joue l'animation
    /// <paramref name="first"/>, se fige quelques images sur sa derniere image, passe a la suivante,
    /// et recommence a <paramref name="first"/> une fois <paramref name="last"/> depassee.
    ///
    /// <para><b>La signature de Ghidra est fausse et il ne faut pas la suivre.</b> Il declare six
    /// parametres en <c>__fastcall</c> alors que les deux sites d'appel en poussent quatre. Le
    /// desassemblage tranche : <c>call</c> suivi de <c>add esp,0x10</c>, donc <b>cdecl a quatre
    /// arguments</b>, et le prologue lit l'enregistrement en <c>[esp+0x0c]</c> puis le premier
    /// numero en <c>[esp+0x14]</c>. Les <c>param_1</c> et <c>param_2</c> de Ghidra sont des
    /// registres fantomes ; ses <c>param_3</c> a <c>param_6</c> sont les quatre vrais arguments,
    /// dans l'ordre.</para>
    ///
    /// <para>Le champ <c>+0x9a</c>, propre a cette fonction, est le <b>reste de pause</b>. A zero on
    /// avance d'une image ; arrive au bout de la sequence on y charge <paramref name="hold"/> et la
    /// derniere image reste affichee tant qu'il decroit. Le tirage au hasard est fait par
    /// l'appelant, une fois par passage, si bien que deux entites de la meme scene ne respirent pas
    /// en cadence.</para>
    ///
    /// <para>Le champ <c>+0x4c</c> decide de la fin : a 1 la sequence <b>s'arrete</b> sur
    /// <paramref name="last"/> au lieu de reboucler sur <paramref name="first"/>. Dans les deux cas
    /// le bit <c>0x80000000</c> de <c>+0x5c</c> est pose et le script accroche en <c>+0x44</c>, s'il
    /// y en a un, part une fois puis est efface.</para>
    ///
    /// <para>Comme ses voisines, la fonction rend une valeur dont Ghidra n'expose que du residu de
    /// registre en moitie haute ; <b>aucun des deux appelants ne la lit</b>, et seule la moitie
    /// basse a un sens : 1 si un script a demarre, 0 sinon.</para>
    /// </summary>
    private static uint CycleSceneEntityAnimations_004052F0(byte* record, short first, short last, short hold)
    {
        int counter = *(int*)(record + 0x48);
        if (counter > 9)
        {
            counter--;
            *(int*)(record + 0x48) = counter;
            if (counter < 10) *(int*)(record + 0x48) = 0;
        }

        if (*(int*)(record + 0x48) == 0)
        {
            *(int*)(record + 0x48) = 1;
            *(short*)(record + 0x18) = first;
            *(short*)(record + 0x1a) = -1;
            *(short*)(record + 0x9a) = 0;
        }

        if (*(int*)(record + 0x48) != 1) return 0;

        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + record[4] * 4))
                              [*(short*)(record + 0x18)];

        if (*(short*)(record + 0x9a) == 0)
        {
            // Pas de pause en cours : on avance, et arrive a la derniere image on l'arme.
            *(short*)(record + 0x1a) += 1;

            if (*(ushort*)(animation + 0x16) - 1 <= *(short*)(record + 0x1a))
            {
                *(short*)(record + 0x9a) = hold;
                *(short*)(record + 0x1a) = (short)(*(short*)(animation + 0x16) - 1);
            }

            return 0;
        }

        // Pause en cours : l'image reste la derniere de la sequence, et le reste decroit.
        short remaining = (short)(*(short*)(record + 0x9a) - 1);
        *(short*)(record + 0x9a) = remaining;
        *(short*)(record + 0x1a) = (short)(*(short*)(animation + 0x16) - 1);

        if (remaining >= 1) return 0;

        short next = (short)(*(short*)(record + 0x18) + 1);
        *(short*)(record + 0x9a) = 0;
        *(short*)(record + 0x1a) = 0;
        *(short*)(record + 0x18) = next;

        if (last >= next) return 0;

        *(short*)(record + 0x18) = first;
        *(uint*)(record + 0x5c) |= 0x80000000;

        if (*(int*)(record + 0x4c) == 1) *(short*)(record + 0x18) = last;

        if (*(int*)(record + 0x44) == 0) return 0;

        RunEntityScript(s_Unknown_005e97cc, *(byte**)(record + 0x6c), (uint)*(int*)(record + 0x44));
        *(int*)(record + 0x44) = 0;
        *(uint*)(record + 0x5c) |= 0x80000000;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_00405420 @ 0x00405420 — <b>la variante qui decompte au lieu d'avancer</b>. Le
    /// champ <c>+0x1a</c> part du nombre d'images de la sequence et descend ; les trois autres pas
    /// d'animation de ce fichier le font monter. Le resultat visible est le meme, mais le champ ne
    /// designe plus l'image courante — c'est ce qui reste a tenir.
    ///
    /// <para>Elle ne sert qu'a l'etat 3 de <see cref="WakeSceneEntityKind1_00405240"/>. Sa jumelle
    /// de type 2 y met un <see cref="AdvanceSceneEntityState_00404FE0"/> ordinaire : c'est la seule
    /// difference de fond entre les deux reveils.</para>
    ///
    /// <para>Comme <see cref="AdvanceSceneEntityState_00404FE0"/>, elle amorce <c>+0x1a</c> avec le
    /// nombre d'images <b>entier</b> (et non diminue de un) puis le decremente aussitot : la
    /// premiere image affichee est donc bien la derniere de la sequence.</para>
    /// </summary>
    private static uint CountDownSceneEntityState_00405420(byte* record, short kind)
    {
        int counter = *(int*)(record + 0x48);
        if (counter > 9)
        {
            counter--;
            *(int*)(record + 0x48) = counter;
            if (counter < 10) *(int*)(record + 0x48) = 0;
        }

        if (*(int*)(record + 0x48) == 0)
        {
            *(short*)(record + 0x18) = kind;
            *(short*)(record + 0x1a) =
                *(short*)(((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + record[4] * 4))[kind] + 0x16);
            *(int*)(record + 0x48) = 1;
        }

        if (*(int*)(record + 0x48) != 1) return 0;

        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + record[4] * 4))
                              [*(short*)(record + 0x18)];

        *(short*)(record + 0x1a) -= 1;
        if (*(short*)(record + 0x1a) >= 1) return 0;

        int script = *(int*)(record + 0x44);

        if (*(int*)(record + 0x4c) == 0)
        {
            *(short*)(record + 0x1a) = (short)(*(short*)(animation + 0x16) - 1);
        }
        else
        {
            *(short*)(record + 0x1a) = 0;
        }

        *(uint*)(record + 0x5c) |= 0x80000000;

        if (script == 0) return 0;

        RunEntityScript(s_Unknown_005e97cc, *(byte**)(record + 0x6c), (uint)script);
        *(int*)(record + 0x44) = 0;
        *(uint*)(record + 0x5c) |= 0x80000000;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_004db5a0 @ 0x004db5a0 — <b>absente de <c>Blaze.c</c></b>, transcrite depuis le
    /// desassemblage (112 octets, 0x004db5a0..0x004db610). <b>L'avance d'une image d'animation</b>,
    /// partagee par les gestionnaires d'entite.
    ///
    /// <para>Le compteur d'images est <c>+0x1a</c>, la limite le champ <c>+0x16</c> de l'entree
    /// d'animation que designe <c>+0x18</c> dans la table du sous-type. Le bit <c>0x1000000</c> des
    /// drapeaux choisit le regime : pose, l'animation <b>boucle</b> (retour a zero) ; degage, elle
    /// <b>s'arrete</b> sur sa derniere image et pose le bit <c>0x2000000</c> pour l'annoncer. Le bit
    /// <c>0x2000000</c> est efface a l'entree dans les deux cas.</para>
    /// </summary>
    private static void AdvanceEntityAnimation_004DB5A0(uint* entity)
    {
        byte* e = (byte*)entity;

        uint flags = entity[0] & 0xfdffffff;
        entity[0] = flags;

        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + e[4] * 4))
                              [*(short*)(e + 0x18)];

        *(short*)(e + 0x1a) += 1;

        if ((flags & 0x1000000) != 0)
        {
            if (*(short*)(e + 0x1a) >= *(ushort*)(animation + 0x16))
            {
                *(short*)(e + 0x1a) = 0;
            }
        }
        else if (*(short*)(e + 0x1a) >= *(ushort*)(animation + 0x16))
        {
            *(short*)(e + 0x1a) -= 1;
            entity[0] = flags | 0x2000000;
        }
    }
}
