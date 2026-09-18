// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DU CHATEAU DE L'IMMORTEL — zone 6, table 0x0056adc8.
//
// Ouvert le 2026-09-18 avec le palier 1, sur le patron de `Mine.cs` et d'`OldTower.cs` : ce qui
// meuble le chateau et ce qui y sonne. Ce que le decor DECLENCHE — les mecanismes, ce qui consulte
// le groupe — ira dans un fichier separe le jour ou il s'en presentera, comme au Labyrinthe
// (`Labyrinth.cs` / `LabyrinthMecanismes.cs`).
//
// LES QUATRE CORPS DE CE PALIER SONT DES VARIANTES DE CORPS DEJA ECRITS AILLEURS, et chacun a
// exactement UN ecart avec son modele. Ils valent surtout par ce que leur relevé a coute :
//
//   0x00463C80  <- 0x004508B0 (foret)    l'echelle est posee en +0x2a, pas en +0x2c, et vaut 9000
//   0x00465180  <- 0x0045A310 (tour)     le son de zone est le creneau 0x28, pas 0x30
//   0x00466A10  <- 0x00452A90 (mine)     l'appel de tete, ClearEntityVelocity, N'EST PAS LA
//   0x00463E80  <- 0x00453000 (mine)     le son part par 0x00468080 et NON par 0x004580e0
//
// LE DERNIER EST LE PIEGE DU LOT, ET IL SE REPETERA. La normalisation qui sert a apparier les corps
// masque les cibles d'appel — c'est ce qui rend un corps recompile comparable a son modele. Mais
// elle masque AUSSI un changement de callee : 0x00463E80 et 0x00453000 se ressemblent a six lignes
// pres, et ces six lignes ne disent rien de l'essentiel, qui est que l'un appelle le son de
// proximite a portee variable et l'autre le son a rayon fixe de 2000. Contre un corps « presque
// identique », RESOUDRE LES CIBLES D'APPEL A PART avant de conclure.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_00463c80 @ 0x00463c80 — creneau <b>40</b> du bloc <c>0x00558e28</c>
    /// (table <c>0x0056adc8</c>, sous-zones 0 a 4). Decompilee dans <c>Blaze.c</c> ; six
    /// instructions.
    ///
    /// <para>
    /// <b>La forme de <see cref="EntityHandler_004508B0"/></b> (foret), a une instruction pres, et
    /// les deux moities de cette instruction different : l'echelle est posee en <c>+0x2a</c> et non
    /// en <c>+0x2c</c>, et elle vaut <c>0x2328</c> — 9000 — la ou la foret pose <c>0x320</c>.
    /// C'est donc un decor <b>tres haut</b>, pas une copie a l'echelle pres.
    /// </para>
    /// <para>
    /// Le bit est pose par <c>or ch,40h</c>, sur le <b>deuxieme octet</b> de <c>ecx</c> :
    /// <c>0x4000</c>, pas <c>0x40</c>. Meme piege de lecture que chez son modele.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463C80(byte* e)
    {
        *(ushort*)(e + 0x2a) = 0x2328;
        *(uint*)e |= 0x4000;
    }

    /// <summary>
    /// Ghidra: FUN_00465180 @ 0x00465180 — creneau <b>49</b> du bloc <c>0x005594a8</c>
    /// (sous-zones 6 a 10). Decompilee dans <c>Blaze.c</c> ; 49 instructions.
    ///
    /// <para>
    /// <b>Le son au hasard de <see cref="EntityHandler_0045A310"/></b> (vieille tour), au creneau
    /// de son sonore pres : <c>0x28</c> au lieu de <c>0x30</c>. Tout le reste est identique
    /// instruction pour instruction — le premier tour pose <c>0x6000</c> sur le mot d'etat, un
    /// compteur monte d'un par image, et passe 30 il y a une chance sur soixante par image de
    /// sonner, au plus tard a 51. Hauteur tiree entre 500 et 699, gain entre <c>0x9c4</c> et
    /// <c>0x1193</c>, deux tirages dans cet ordre.
    /// </para>
    /// </summary>
    private static void EntityHandler_00465180(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            *(uint*)e |= 0x6000;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;

        if (((short)(CrtRand() % 0x3c) == 1 && *(short*)(e + 0x12) > 0x1e) || *(short*)(e + 0x12) > 0x32)
        {
            *(short*)(e + 0x12) = 0;

            int pitch = (short)(CrtRand() % 200 + 500);
            int gain = (short)(CrtRand() % 2000 + 0x9c4);
            Aud.PlaySoundEffect(ZoneSound(0x28), pitch, gain, gain);           // Ghidra: FUN_0053cea0
        }
    }

    /// <summary>
    /// Ghidra: FUN_00466a10 @ 0x00466a10 — creneau <b>20</b> du bloc <c>0x00559b28</c>
    /// (sous-zones 6 a 10). Decompilee dans <c>Blaze.c</c> ; 44 instructions.
    ///
    /// <para>
    /// <b>Le halo vacillant de <see cref="TickFlickeringGlow"/></b> (mine), <b>moins son appel de
    /// tete</b> : les 41 instructions qui suivent sont les memes une a une, mais le chateau
    /// n'annule pas la vitesse de l'entite avant de commencer. Trois instructions de moins, et la
    /// seule difference du corps.
    /// </para>
    /// <para>
    /// Deux gestionnaires du chateau portent l'autre forme, celle QUI annule la vitesse :
    /// <c>0x00464B30</c> et <c>0x00466470</c> sont des doublons octet pour octet de
    /// <c>0x00452A90</c> — memes 47 instructions, memes trois cibles d'appel — et le repartiteur
    /// les envoie directement sur le corps de la mine.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466A10(byte* e) => TickFlickeringGlow(e, clearVelocity: false);

    /// <summary>
    /// Ghidra: FUN_00463e80 @ 0x00463e80 — creneau <b>30</b> du bloc <c>0x00558e28</c>
    /// (sous-zones 0 a 4). Decompilee dans <c>Blaze.c</c> ; 31 instructions.
    ///
    /// <para>
    /// <b>L'ossature de <see cref="TickRepeatingDistanceSound"/></b> (mine) : le premier tour pose
    /// l'etat a 1, le bit <c>0x4000</c> sur le mot d'etat et le compteur a zero ; ensuite le
    /// compteur avance d'un par image et, a la dixieme, revient a zero et lache le son. Le premier
    /// son part donc a la dixieme image, pas a la naissance.
    /// </para>
    /// <para>
    /// <b>MAIS CE N'EST PAS LE MEME SON.</b> La mine appelle <c>0x004580e0</c>, le son de
    /// proximite a <b>portee variable</b>, non positionnel, attenue lineairement ; le chateau
    /// appelle <c>0x00468080</c>, <see cref="TryPlaySoundNearEntity"/>, dont le rayon est
    /// <b>fige a 2000</b>, qui joue un son <b>positionnel</b> et qui <b>double le gain</b>. La
    /// normalisation qui appariait les deux corps masquait la cible de l'appel : c'est en la
    /// resolvant a part que l'ecart est apparu. Les trois autres immediates suivent — creneau
    /// sonore <c>0x18</c> au lieu de <c>0x16</c>, gain <c>0x1000</c> au lieu de <c>0x2000</c>, et
    /// le cinquieme argument vaut <b>zero</b> au lieu de <c>0x7d0</c> ; celui-la n'est lu nulle
    /// part, ni ici ni chez sa jumelle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463E80(byte* e) => TickCastleRepeatingSound(e);

    /// <summary>Ghidra: FUN_004657b0 @ 0x004657b0 — creneau <b>30</b> du bloc <c>0x005594a8</c>,
    /// la copie du precedent pour les sous-zones 6 a 10. Comparee instruction par instruction a
    /// <see cref="EntityHandler_00463E80"/>, cible d'appel comprise : <b>zero
    /// difference</b>.</summary>
    private static void EntityHandler_004657B0(byte* e) => TickCastleRepeatingSound(e);

    /// <summary>Le corps commun de <see cref="EntityHandler_00463E80"/> et de
    /// <see cref="EntityHandler_004657B0"/>. Voir le resume du premier pour ce qui le separe de
    /// <see cref="TickRepeatingDistanceSound"/>.</summary>
    private static void TickCastleRepeatingSound(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(uint*)e |= 0x4000;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 0x0a) return;

        *(short*)(e + 0x12) = 0;
        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x18), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
    }

    // =====================================================================
    // PALIER 2, LOT 4 (2026-09-18) — LE DECOR DU CHATEAU, TABLE 0x0056adc8
    //
    // Quarante-quatre creneaux pour TRENTE-QUATRE formes, et la zone se lit en deux blocs miroir :
    // 0x00558e28 sert les sous-zones 0 a 4, 0x005594a8 les sous-zones 6 a 10, au MEME numero de
    // creneau. Quatorze paires en sortent, dont onze sont identiques instruction pour instruction ;
    // les trois autres different d'une constante, et c'est chaque fois la constante qui dit ce que
    // le chateau fait de son cote est ou de son cote ouest.
    //
    // La plus belle est la FAMILLE DE SIX : les creneaux 22, 23 et 24 des deux blocs portent le
    // meme corps a UN nombre pres — l'image ou la plate-forme commence a freiner (0x19, 0x29,
    // 0x39). Six gestionnaires, une lecture.
    //
    // Deux corps de cette table lancent un script de zone et sont donc ailleurs, dans
    // `ImmortalCastleMecanismes.cs` : 0x004647F0 et 0x00466130.
    // =====================================================================

    /// <summary>
    /// Pose le drapeau de script <paramref name="index"/> <b>sans le garde a 32 octets</b> de
    /// <see cref="ScriptFlagRaise"/>. Les plaques du chateau ecrivent la banque directement, sans
    /// verifier que l'octet vise est dans la moitie basse ; c'est le binaire qui est comme cela, et
    /// non une simplification.
    /// </summary>
    private static void CastleFlagRaise(short index)
    {
        int cell = DivideTowardZeroBy8(index);
        int bit = (index - cell * 8) & 0x1f;
        Mem.s_StateBank0_005e96e0[(short)cell] |= (byte)(1 << bit);
    }

    /// <summary>Retire le drapeau de script <paramref name="index"/> — le pendant de
    /// <see cref="CastleFlagRaise"/>, et le seul effaceur de la banque 0 du depot.</summary>
    private static void CastleFlagLower(short index)
    {
        int cell = DivideTowardZeroBy8(index);
        int bit = (index - cell * 8) & 0x1f;
        Mem.s_StateBank0_005e96e0[(short)cell] &= (byte)~(1 << bit);
    }

    // ---------------------------------------------------------------------
    // CE QUI REGARDE UN DRAPEAU
    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00465290"/>. Decompile dans <c>Blaze.c</c>,
    /// 53 instructions.
    ///
    /// <para>
    /// <b>Une bouche qui crache tant qu'elle a de l'elan.</b> Le drapeau de script range en
    /// <c>+0x14</c> l'arme ; elle se donne alors soixante-dix images et, a chacune,
    /// fait naitre <c>(+0x12 / 35) + 1</c> eclats du sous-type <c>0x3b</c> par
    /// <see cref="SpawnEffectNearCamera_00468110"/> — donc <b>trois au debut, un a la fin</b>. Le
    /// decompte epuise, elle passe a l'etat 2 et ne repart jamais.
    /// </para>
    /// </summary>
    private static void SpitCastleShards(byte* e)
    {
        if (*(short*)(e + 0x10) != 1) return;

        int bursts = *(short*)(e + 0x12) / 0x23;
        if (bursts >= 0)
        {
            for (int i = bursts + 1; i != 0; i--)
            {
                SpawnEffectNearCamera_00468110(e, 0x3b, 0, -1);   // Ghidra: FUN_00468110
            }
        }

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x10) = 2;
    }

    /// <summary>Ghidra: FUN_00465290 @ 0x00465290 — creneau <b>46</b> du bloc <c>0x005594a8</c>
    /// (sous-zones 6 a 10). Le drapeau vu, soixante-dix images d'eclats. Voir
    /// <see cref="SpitCastleShards"/>.</summary>
    private static void EntityHandler_00465290(byte* e)
    {
        if (ScriptFlagSet(*(short*)(e + 0x14)) && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x46;
        }

        SpitCastleShards(e);
    }

    /// <summary>
    /// Ghidra: FUN_00463770 @ 0x00463770 — creneau <b>46</b> du bloc <c>0x00558e28</c>
    /// (sous-zones 0 a 4). Decompile dans <c>Blaze.c</c>, 65 instructions.
    ///
    /// <para>
    /// <b>La meme bouche que <see cref="EntityHandler_00465290"/>, avec une cadence par-dessus.</b>
    /// Elle nait dans l'etat <b>99</b> — pas 1 — et fait tourner un compteur <c>+0x98</c> qui monte
    /// d'un par image et <b>revient a 10</b>, jamais a zero, passe 2000. Quand le drapeau
    /// arrive :
    /// </para>
    /// <list type="bullet">
    ///   <item>si <c>+0x98</c> est encore <b>sous 6</b> — donc dans les cinq premieres images de la
    ///     vie de l'entite, et seulement la — elle passe directement a l'etat 2 : <b>elle ne crache
    ///     pas</b> ;</item>
    ///   <item>sinon elle part pour ses soixante-dix images.</item>
    /// </list>
    /// <para>
    /// Le compteur ne repassant plus jamais sous 6 apres le rebouclage a 10, cette porte ne se
    /// referme qu'une fois : elle interdit le crachat a une entite dont le drapeau est <b>deja</b>
    /// pose a sa naissance.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463770(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 99;
            *(short*)(e + 0x98) = 0;
        }

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) > 2000) *(short*)(e + 0x98) = 10;

        if (ScriptFlagSet(*(short*)(e + 0x14)) && *(short*)(e + 0x10) == 99)
        {
            if (*(short*)(e + 0x98) < 6)
            {
                *(short*)(e + 0x10) = 2;
            }
            else
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 0x46;
            }
        }

        SpitCastleShards(e);
    }

    /// <summary>
    /// Ghidra: <c>0x00465610</c> — creneau <b>40</b> du bloc <c>0x005594a8</c>. Absent de
    /// <c>Blaze.c</c> ; <b>douze</b> instructions, le plus court corps de la zone.
    ///
    /// <para>
    /// Il pose une echelle de <c>0x1770</c> en <c>+0x28</c>, s'allume (<c>0x4000</c>), et
    /// <b>ne se montre (<c>0x2000</c>) que si les QUATRE bits <c>0x3c</c> de
    /// <c>DAT_005e9702</c> sont poses ensemble</b> — le test est <c>(x &amp; 0x3c) == 0x3c</c>, pas
    /// un bit isole. Il ne retire jamais le bit : une fois montre, il le reste.
    /// </para>
    /// </summary>
    private static void EntityHandler_00465610(byte* e)
    {
        *(short*)(e + 0x28) = 0x1770;
        *(uint*)e |= 0x4000;

        if ((Mem.s_StateBank0_005e96e0[0x22] & 0x3c) == 0x3c) *(uint*)e |= 0x2000;
    }

    /// <summary>
    /// Ghidra: <c>0x00465150</c> — creneau <b>50</b> du bloc <c>0x005594a8</c>. Absent de
    /// <c>Blaze.c</c> ; treize instructions.
    ///
    /// <para>
    /// Il s'allume, puis recopie <b>a chaque image</b> le bit <c>0x80</c> de <c>DAT_005e96fc</c>
    /// dans son bit <c>0x2000</c> : il apparait et disparait avec le drapeau, dans les deux sens —
    /// c'est ce qui le separe de <see cref="EntityHandler_00465610"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00465150(byte* e)
    {
        *(uint*)e |= 0x4000;

        if ((Mem.s_StateBank0_005e96e0[0x1c] & 0x80) != 0) *(uint*)e |= 0x2000;
        else *(uint*)e &= 0xffffdfff;
    }

    /// <summary>
    /// Ghidra: <c>0x00465640</c> — creneau <b>39</b> du bloc <c>0x00558e28</c>, sert les deux
    /// groupes de sous-zones. Absent de <c>Blaze.c</c> ; vingt instructions.
    ///
    /// <para>
    /// <b>Il copie la visibilite de sa voisine.</b> Le nombre range en <c>+0x14</c> n'est pas un
    /// drapeau ici mais un <b>ecart en rangs</b> : le corps lit le mot d'etat de l'entite
    /// <c>+0x14</c> places plus loin dans la meme table (<c>[e + 0x9c * n]</c>, calcule par
    /// <c>lea/shl/sub</c> a la place d'une multiplication) et recopie son bit <c>0x2000</c>.
    /// </para>
    /// <para>
    /// C'est le troisieme corps du depot a adresser une voisine par son rang apres
    /// <c>0x00463150</c> (Labyrinthe) et <see cref="TickCastlePressurePlate_00463B30"/> ci-dessous,
    /// qui fait grandir l'echelle de la sienne.
    /// </para>
    /// </summary>
    private static void EntityHandler_00465640(byte* e)
    {
        *(uint*)e |= 0x4000;

        byte* neighbour = e + *(short*)(e + 0x14) * 0x9c;
        if ((*(uint*)neighbour & 0x2000) != 0) *(uint*)e |= 0x2000;
        else *(uint*)e &= 0xffffdfff;
    }

    /// <summary>
    /// Ghidra: <c>0x00463ae0</c> — creneau <b>43</b> du bloc <c>0x00558e28</c>, les deux groupes.
    /// Absent de <c>Blaze.c</c> ; vingt et une instructions.
    ///
    /// <para>
    /// <b>Un pan qui coulisse en Z, et il faut DEUX drapeaux pour l'ouvrir.</b> Les bits
    /// <c>0x10</c> et <c>0x20</c> de <c>DAT_005e9703</c> doivent etre poses <b>tous les deux</b> ;
    /// alors <c>+0x24</c> avance de <c>0x14000</c> par image jusqu'a la butee
    /// <c>-0x1e14000</c>. Des que l'un des deux retombe, il repart en sens inverse jusqu'a
    /// <c>-0x1eeb000</c>.
    /// </para>
    /// <para>
    /// <b>Les deux butees sont des positions absolues du monde</b>, pas des ecarts : le pan ne
    /// retient rien de sa pose et ne sert donc qu'a un seul endroit de la carte.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463AE0(byte* e)
    {
        byte flags = Mem.s_StateBank0_005e96e0[0x23];

        if ((flags & 0x10) != 0 && (flags & 0x20) != 0)
        {
            int opened = *(int*)(e + 0x24) + 0x14000;
            *(int*)(e + 0x24) = opened;
            if (opened > unchecked((int)0xfe1ec000)) *(int*)(e + 0x24) = unchecked((int)0xfe1ec000);
            return;
        }

        int closed = *(int*)(e + 0x24) - 0x14000;
        *(int*)(e + 0x24) = closed;
        if (closed < unchecked((int)0xfe115000)) *(int*)(e + 0x24) = unchecked((int)0xfe115000);
    }

    /// <summary>
    /// Ghidra: FUN_00465330 @ 0x00465330 — creneau <b>45</b> du bloc <c>0x005594a8</c>. Decompile
    /// dans <c>Blaze.c</c>, 54 instructions.
    ///
    /// <para>
    /// <b>Les quatre coins.</b> Le corps remet sa hauteur a zero, puis examine <b>quatre bits</b> de
    /// <c>DAT_005e9702</c> — <c>4</c>, <c>8</c>, <c>0x10</c>, <c>0x20</c> — et, pour chacun qui est
    /// pose, se <b>teleporte</b> sur un coin fixe de la carte avant d'y faire naitre un effet du
    /// sous-type <c>0x3c</c>. Les quatre naissances se font donc dans la meme image, chacune a sa
    /// position, et l'entite finit l'image sur le dernier coin.
    /// </para>
    /// <para>
    /// Les quatre coins sont <c>(-150, 225)</c>, <c>(-150, -225)</c>, <c>(150, -225)</c> et
    /// <c>(150, 225)</c> en cases : un rectangle centre sur l'origine.
    /// </para>
    /// </summary>
    private static void EntityHandler_00465330(byte* e)
    {
        *(int*)(e + 0x20) = 0;

        byte flags = Mem.s_StateBank0_005e96e0[0x22];

        if ((flags & 4) != 0)
        {
            *(int*)(e + 0x1c) = unchecked((int)0xff6a0000);
            *(int*)(e + 0x24) = 0x00e10000;
            SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);
        }

        if ((flags & 8) != 0)
        {
            *(int*)(e + 0x1c) = unchecked((int)0xff6a0000);
            *(int*)(e + 0x24) = unchecked((int)0xff1f0000);
            SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);
        }

        if ((flags & 0x10) != 0)
        {
            *(int*)(e + 0x1c) = 0x00960000;
            *(int*)(e + 0x24) = unchecked((int)0xff1f0000);
            SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);
        }

        if ((flags & 0x20) != 0)
        {
            *(int*)(e + 0x1c) = 0x00960000;
            *(int*)(e + 0x24) = 0x00e10000;
            SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00463850</c> — creneau <b>45</b> du bloc <c>0x00558e28</c>, le miroir est de
    /// <see cref="EntityHandler_00465330"/>. Absent de <c>Blaze.c</c> ; 123 instructions, dump dans
    /// <c>/tmp/census-duplicates/raw/0x00463850.asm</c>.
    ///
    /// <para>
    /// <b>Les memes quatre coins, et chacun sonne une fois.</b> Les quatre teleportations et les
    /// quatre naissances du sous-type <c>0x3c</c> sont celles du miroir, dans le meme ordre et aux
    /// memes coins. Ce que ce corps ajoute : un <b>etat par coin</b>, <c>+0x10</c>, <c>+0x12</c>,
    /// <c>+0x14</c>, <c>+0x16</c>, tous mis a 1 a l'amorcage ; le premier passage d'un coin pendant
    /// que son etat vaut 1 joue le son <b><c>0x2d</c> — un numero absolu, pas un creneau de la
    /// zone</b> — hauteur <c>0x80</c>, gain <c>0x1800</c>, et passe l'etat a 2. Chaque coin ne sonne
    /// donc qu'une fois dans la vie de l'entite.
    /// </para>
    /// <para>
    /// <b>Le son attend dix images.</b> <c>+0x98</c> compte de 1 a 19 puis retombe a 10, et le son
    /// n'est permis que strictement au-dessus de 10 : muet pendant les dix premieres images, puis
    /// muet encore <b>une image sur dix</b>, celle de la retombee. Un bit pose a ce moment-la sonne
    /// a l'image suivante.
    /// </para>
    /// <para>
    /// Contrairement au miroir, l'octet <c>DAT_005e9702</c> est <b>relu a chaque test</b>
    /// (<c>mov al,[5E9702h]</c> puis <c>test byte [5E9702h],…</c>) : transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463850(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 1;
            *(short*)(e + 0x14) = 1;
            *(short*)(e + 0x16) = 1;
            *(short*)(e + 0x98) = 0;
        }

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) >= 0x14) *(short*)(e + 0x98) = 10;

        *(int*)(e + 0x20) = 0;

        if ((Mem.s_StateBank0_005e96e0[0x22] & 4) != 0)
        {
            ChimeCastleCorner_00463850(e, 0x10, unchecked((int)0xff6a0000), 0x00e10000);
        }

        if ((Mem.s_StateBank0_005e96e0[0x22] & 8) != 0)
        {
            ChimeCastleCorner_00463850(e, 0x12, unchecked((int)0xff6a0000), unchecked((int)0xff1f0000));
        }

        if ((Mem.s_StateBank0_005e96e0[0x22] & 0x10) != 0)
        {
            ChimeCastleCorner_00463850(e, 0x14, 0x00960000, unchecked((int)0xff1f0000));
        }

        if ((Mem.s_StateBank0_005e96e0[0x22] & 0x20) != 0)
        {
            ChimeCastleCorner_00463850(e, 0x16, 0x00960000, 0x00e10000);
        }
    }

    /// <summary>Un coin de <see cref="EntityHandler_00463850"/> : le binaire deroule quatre fois le
    /// meme bloc, seuls changent l'etat du coin et sa position.</summary>
    private static void ChimeCastleCorner_00463850(byte* e, int stateOffset, int x, int z)
    {
        *(int*)(e + 0x1c) = x;
        *(int*)(e + 0x24) = z;
        SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);

        if (*(short*)(e + stateOffset) != 1 || *(short*)(e + 0x98) <= 10) return;

        TryPlaySoundNearEntity((uint*)e, 0x2d, 0x80, 0x1800, 0, 0);   // Ghidra: FUN_00468080
        *(short*)(e + stateOffset) = 2;
    }

    // ---------------------------------------------------------------------
    // LES PLAQUES DE PRESSION
    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00463A10"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 72 et 66 instructions.
    ///
    /// <para>
    /// <b>Une dalle qui s'enfonce de trente unites quand on marche dessus</b>, et qui leve le
    /// drapeau de script range en <c>+0x14</c> tant qu'elle est chargee. Relachee, elle remonte a
    /// sa hauteur de naissance — gardee en <c>+0x12</c> des la premiere image, en <b>cases</b> et
    /// non en seiziemes — et retire le drapeau.
    /// </para>
    /// <para>
    /// <b>Les deux exemplaires ne pesent pas pareil.</b> Celui des sous-zones 0 a 4 demande deux
    /// mesures : quelqu'un a moins de <c>0x20</c> <b>et personne</b> a moins de <c>0x10</c> — donc
    /// on la declenche en s'en approchant, pas en montant dessus. Celui des sous-zones 6 a 10
    /// appelle aussi la premiere mesure mais <b>jette son resultat</b> et ne garde que la seconde :
    /// il faut monter dessus. L'appel perdu est dans le binaire, et il consomme le meme balayage du
    /// groupe.
    /// </para>
    /// </summary>
    /// <param name="ringOnly">Vrai pour le corps des sous-zones 0 a 4, qui exige la couronne
    /// <c>0x20</c> ET l'absence dans <c>0x10</c>.</param>
    private static void TickCastlePressurePlate_00463A10(byte* e, bool ringOnly)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        int near = CountPartyMembersMatching(e, 0x20, 0x40000000);   // Ghidra: FUN_00535000
        int onto = CountPartyMembersMatching(e, 0x10, 0x40000000);

        bool pressed = ringOnly ? (near != 0 && onto == 0) : onto != 0;

        if (pressed)
        {
            if (*(int*)(e + 0x20) >> 0xc == *(short*)(e + 0x12)) *(int*)(e + 0x20) -= 0x1e000;
            CastleFlagRaise(*(short*)(e + 0x14));
            return;
        }

        if (*(int*)(e + 0x20) >> 0xc != *(short*)(e + 0x12))
        {
            *(int*)(e + 0x20) = *(short*)(e + 0x12) << 0xc;
        }

        CastleFlagLower(*(short*)(e + 0x14));
    }

    /// <summary>Ghidra: FUN_00463a10 @ 0x00463a10 — creneau <b>42</b> du bloc <c>0x00558e28</c>.
    /// La dalle qui se declenche a l'approche.</summary>
    private static void EntityHandler_00463A10(byte* e) => TickCastlePressurePlate_00463A10(e, ringOnly: true);

    /// <summary>Ghidra: FUN_004653f0 @ 0x004653f0 — creneau <b>42</b> du bloc <c>0x005594a8</c>.
    /// La dalle sur laquelle il faut monter — et qui pese quand meme deux fois.</summary>
    private static void EntityHandler_004653F0(byte* e) => TickCastlePressurePlate_00463A10(e, ringOnly: false);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00463B30"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 94 instructions.
    ///
    /// <para>
    /// <b>Une dalle a verrou, et DEUX roles dans un seul corps</b>, choisis par <c>+0x16</c> :
    /// </para>
    /// <list type="table">
    ///   <item><term><c>+0x16 == 0</c></term><description><b>la dalle</b> : elle s'enfonce de
    ///     trente unites sous le poids et fait <b>grandir sa voisine</b> de 500 par image jusqu'a
    ///     <c>0x1000</c> ; relachee, elle remonte et la fait retrecir jusqu'a zero. La voisine est
    ///     designee par un <b>ecart en rangs</b> range en <c>+0x14</c>, et ce sont ses deux
    ///     echelles <c>+0x28</c> et <c>+0x2c</c> qui bougent.</description></item>
    ///   <item><term>autre</term><description><b>le verrou</b> : des que quelqu'un le touche, il
    ///     pose le bit dans <c>DAT_005e9703</c> et ne fait plus rien d'autre.</description></item>
    /// </list>
    /// <para>
    /// <b>Le bit pose par le verrou tient la dalle enfoncee pour toujours</b> : la dalle le lit au
    /// debut de chaque image et se croit chargee. C'est le seul mecanisme du lot qui garde son etat
    /// ailleurs que dans l'entite.
    /// </para>
    /// <para>
    /// Les deux exemplaires n'emploient <b>pas le meme bit</b> : <c>8</c> pour les sous-zones 0 a
    /// 4, <c>0x80</c> pour les sous-zones 6 a 10. C'est leur unique difference, et elle tient en
    /// deux immediates.
    /// </para>
    /// </summary>
    private static void TickCastlePressurePlate_00463B30(byte* e, byte latch)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        int onto = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000

        if (*(short*)(e + 0x16) != 0)
        {
            // Le verrou : il ne fait que poser le bit.
            if (onto != 0) Mem.s_StateBank0_005e96e0[0x23] |= latch;
            return;
        }

        if ((Mem.s_StateBank0_005e96e0[0x23] & latch) != 0) onto = 1;

        short* neighbourX = (short*)(e + 0x28 + *(short*)(e + 0x14) * 0x9c);
        short* neighbourZ = (short*)(e + 0x2c + *(short*)(e + 0x14) * 0x9c);

        if (onto == 0)
        {
            if (*(int*)(e + 0x20) >> 0xc != *(short*)(e + 0x12))
            {
                *(int*)(e + 0x20) = *(short*)(e + 0x12) << 0xc;
            }

            *neighbourX -= 500;
            *neighbourZ -= 500;

            if (*neighbourX < 1)
            {
                *neighbourX = 0;
                *neighbourZ = 0;
            }

            return;
        }

        if (*(int*)(e + 0x20) >> 0xc == *(short*)(e + 0x12)) *(int*)(e + 0x20) -= 0x1e000;

        *neighbourX += 500;
        *neighbourZ += 500;

        if (*neighbourX > 0xfff)
        {
            *neighbourX = 0x1000;
            *neighbourZ = 0x1000;
        }
    }

    /// <summary>Ghidra: FUN_00463b30 @ 0x00463b30 — creneau <b>41</b> du bloc <c>0x00558e28</c>,
    /// verrou sur le bit <c>8</c>.</summary>
    private static void EntityHandler_00463B30(byte* e) => TickCastlePressurePlate_00463B30(e, 8);

    /// <summary>Ghidra: FUN_004654c0 @ 0x004654c0 — creneau <b>41</b> du bloc <c>0x005594a8</c>,
    /// verrou sur le bit <c>0x80</c>.</summary>
    private static void EntityHandler_004654C0(byte* e) => TickCastlePressurePlate_00463B30(e, 0x80);

    // ---------------------------------------------------------------------
    // CE QUI SONNE
    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00463CA0"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 73 instructions.
    ///
    /// <para>
    /// <b>Un craquement en deux temps, une fois de temps en temps.</b> Toutes les dix images, un
    /// tirage : une chance sur <paramref name="odds"/> de partir. La hauteur du son est alors tiree
    /// une fois pour les deux temps — <c>rand % 0x400 + </c><paramref name="floor"/> — et posee en
    /// <c>+0x16</c>.
    /// </para>
    /// <para>
    /// Le premier temps part <b>a l'image meme</b>, a la hauteur tiree, gain <c>0x400</c> ; le
    /// second <b>trois images plus tard</b>, gain <c>0x500</c>, et <b>0x400 plus bas</b>. Puis le
    /// corps revient a l'attente. Le son est <c>0x1e</c> de la zone dans les deux cas.
    /// </para>
    /// <para>
    /// <b>Les deux exemplaires n'ont ni la meme chance ni le meme grave</b> : une sur vingt a
    /// partir de <c>0xc00</c> pour les sous-zones 0 a 4, une sur cinq a partir de <c>0x1000</c>
    /// pour les sous-zones 6 a 10. Le cote ouest craque quatre fois plus souvent, et plus aigu.
    /// </para>
    /// </summary>
    private static void TickCastleCreak_00463CA0(byte* e, int odds, int floor)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(uint*)e |= 0x4000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;

            if (*(short*)(e + 0x12) == 10)
            {
                *(short*)(e + 0x12) = 0;

                if ((short)(CrtRand() % odds) == 1)
                {
                    *(short*)(e + 0x10) = 2;
                    *(short*)(e + 0x14) = 0;
                    *(short*)(e + 0x16) = (short)(CrtRand() % 0x400 + floor);
                }
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        if (*(short*)(e + 0x14) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1e), 0x400, *(short*)(e + 0x16), 0, 0);
        }

        if (*(short*)(e + 0x14) == 3)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1e), 0x500,
                                   (short)(*(short*)(e + 0x16) - 0x400), 0, 0);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        (*(short*)(e + 0x14))++;
    }

    /// <summary>Ghidra: FUN_00463ca0 @ 0x00463ca0 — creneau <b>35</b> du bloc <c>0x00558e28</c> :
    /// une chance sur vingt, grave a partir de <c>0xc00</c>.</summary>
    private static void EntityHandler_00463CA0(byte* e) => TickCastleCreak_00463CA0(e, 0x14, 0xc00);

    /// <summary>Ghidra: FUN_004656c0 @ 0x004656c0 — creneau <b>35</b> du bloc <c>0x005594a8</c> :
    /// une chance sur cinq, grave a partir de <c>0x1000</c>.</summary>
    private static void EntityHandler_004656C0(byte* e) => TickCastleCreak_00463CA0(e, 5, 0x1000);

    /// <summary>
    /// Ghidra: FUN_00465670 @ 0x00465670 — creneau <b>47</b> du bloc <c>0x005594a8</c>. Decompile
    /// dans <c>Blaze.c</c>, 25 instructions.
    ///
    /// <para>
    /// <b>Un battement toutes les seize images</b>, et rien d'autre : le corps s'allume, joue le
    /// son <c>0x18</c> de la zone (hauteur <c>0x300</c>, gain <c>0x1300</c>) a chaque fois que son
    /// compteur repasse par zero, et rend la main. <c>+0x10</c> sert ici de <b>compteur</b> et non
    /// d'etat — il monte de 0 a 15 puis revient.
    /// </para>
    /// </summary>
    private static void EntityHandler_00465670(byte* e)
    {
        *(uint*)e |= 0x4000;

        if (*(short*)(e + 0x10) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x18), 0x300, 0x1300, 0, 0);
        }

        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) > 0x0f) *(short*)(e + 0x10) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00465220</c> — creneau <b>48</b> du bloc <c>0x005594a8</c>. Absent de
    /// <c>Blaze.c</c> ; vingt-six instructions.
    ///
    /// <para>
    /// La meme chose toutes les <b>vingt</b> images, avec le son <c>0x17</c> (hauteur <c>0x600</c>,
    /// gain <c>0x500</c>), et <b>un vrai etat</b> cette fois : <c>+0x10</c> reste a 1 et c'est
    /// <c>+0x98</c> qui compte.
    /// </para>
    /// </summary>
    private static void EntityHandler_00465220(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) != 0x14) return;

        *(short*)(e + 0x98) = 0;
        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x17), 0x600, 0x500, 0, 0);
    }

    /// <summary>
    /// Ghidra: FUN_00466970 @ 0x00466970 — creneau <b>37</b> du bloc <c>0x00559b28</c>
    /// (sous-zones 5 et 11). Decompile dans <c>Blaze.c</c>, 49 instructions.
    ///
    /// <para>
    /// <b>Un cri, une fois par seconde au plus, et jamais si le drapeau est la.</b> Toutes les
    /// soixante images, le corps tire a pile ou face ; s'il gagne, et si le bit <c>0x10</c> de
    /// <c>DAT_005e96fa</c> est <b>absent</b>, il joue le son <c>0x1d</c> de la zone a une hauteur
    /// tiree elle aussi a pile ou face : <c>0x200</c> ou <c>0x2c8</c>.
    /// </para>
    /// <para>
    /// <b>Une bizarrerie fidele</b> : le binaire garde le son derriere <c>hauteur != 200</c>, mais
    /// la hauteur ne vaut jamais 200 — elle vaut <c>0x200</c> (512) ou <c>0x2c8</c> (712). La garde
    /// ne refuse donc jamais rien. Transcrite telle quelle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466970(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;

        int toss = CrtRand();
        if (*(short*)(e + 0x12) != 0x3c) return;

        *(short*)(e + 0x12) = 0;
        if ((short)(toss % 2) != 1) return;
        if ((Mem.s_StateBank0_005e96e0[0x1a] & 0x10) != 0) return;

        short pitch = (short)((short)(CrtRand() % 2) * 200 + 0x200);
        if (pitch != 200)   // Ghidra: une garde qui ne refuse jamais — voir le resume.
        {
            Aud.PlaySoundEffect(ZoneSound(0x1d), pitch, 0x800, 0x800);
        }
    }

    // ---------------------------------------------------------------------
    // LES DEUX ECLAIRS
    // ---------------------------------------------------------------------

    /// <summary>
    /// Ghidra: FUN_00463d90 @ 0x00463d90 — creneau <b>34</b> du bloc <c>0x00558e28</c>, les deux
    /// groupes. Decompile dans <c>Blaze.c</c>, 51 instructions.
    ///
    /// <para>
    /// <b>L'orage.</b> A sa naissance le corps met de cote les trois composantes du fond de scene
    /// (<c>0x005e92b8</c>) dans <c>+0x50</c>, <c>+0x54</c> et <c>+0x58</c>. Ensuite, a la
    /// <b>vingtieme</b> image et avec une chance sur dix, il joue le coup de tonnerre — son
    /// <c>0x1c</c>, hauteur <c>0x400</c> — et <b>peint le fond en blanc</b> (<c>0xfa</c> sur les
    /// trois canaux, mode 1).
    /// </para>
    /// <para>
    /// A la <b>vingt-cinquieme</b>, le fond retrouve ses valeurs d'origine, le mode repasse a 2 et
    /// le compteur repart. <b>La restauration n'est pas conditionnee a l'eclair</b> : elle a lieu
    /// toutes les vingt-cinq images, qu'il y ait eu eclair ou non, et repose donc les memes
    /// couleurs sur elles-memes.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463D90(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        int toss = CrtRand();

        if (*(short*)(e + 0x12) == 0x14 && (short)(toss % 10) == 1)
        {
            Aud.PlaySoundEffect(ZoneSound(0x1c), 0x400, 0x1000, 0x1000);
            SetSceneBackdrop(0xfa, 0xfa, 0xfa, 1);
        }

        if (*(short*)(e + 0x12) == 0x19)
        {
            *(short*)(e + 0x12) = 0;
            SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54),
                             (short)*(int*)(e + 0x58), 2);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00466aa0 @ 0x00466aa0 — creneau <b>44</b> du bloc <c>0x00559b28</c>
    /// (sous-zones 5 et 11). Decompile dans <c>Blaze.c</c>, 73 instructions.
    ///
    /// <para>
    /// <b>Le meme eclair, mais declenche par la PALETTE.</b> La condition n'est ni un drapeau ni un
    /// tirage : le corps lit la <b>premiere entree de la table de couleurs animee</b> de la
    /// sous-zone — <c>MainMem + (descripteur &amp; 0xff) * 0x20 + ((banque &amp; 0xff) + 0xe10) *
    /// 0x200</c>, la formule d'adresse que <c>WorldMap.cs</c> emploie pour faire tourner ces memes
    /// tables — et attend qu'elle vaille <c>0x835a</c>. Quand la rotation de la palette amene cette
    /// couleur-la, l'orage part.
    /// </para>
    /// <para>
    /// Alors : deux cent cinquante images d'attente (etat 1), le coup de tonnerre et le blanc
    /// (etat 2), quatre images (etat 3), et le fond revient. Le corps retombe a l'etat 0 et
    /// reguette la couleur.
    /// </para>
    /// <para>
    /// <b>La mise de cote du fond n'a lieu qu'a l'etat 0</b>, donc a chaque tour de guet : les
    /// couleurs rendues sont celles d'avant ce tour-ci, pas celles du chargement.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466AA0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x50) = *(short*)Mem.Arena(0x005e92b8);
            *(int*)(e + 0x54) = *(short*)Mem.Arena(0x005e92ba);
            *(int*)(e + 0x58) = *(short*)Mem.Arena(0x005e92bc);
        }

        // Ghidra: *(short *)((*(uint *)**(undefined4 **)(DAT_005e97cc + 100) & 0xff) * 0x20 +
        //         (((*(undefined4 **)(DAT_005e97cc + 100))[1] & 0xff) + 0xe10) * 0x200 + MainMem)
        byte* mainMem = GetMainMemPtr();
        uint** list = *(uint***)(s_Unknown_005e97cc + 0x64);
        ushort* clut = (ushort*)(mainMem
            + (*list[0] & 0xff) * 0x20
            + (((uint)(nuint)list[1] & 0xff) + 0xe10) * 0x200);

        if (*(short*)clut == -0x7ca6 && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) == 0xfa)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            Aud.PlaySoundEffect(ZoneSound(0x1c), 0x400, 0x1000, 0x1000);
            SetSceneBackdrop(0xfa, 0xfa, 0xfa, 1);
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 3;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) == 4)
            {
                SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54),
                                 (short)*(int*)(e + 0x58), 2);
                *(short*)(e + 0x10) = 0;
            }
        }
    }

    // ---------------------------------------------------------------------
    // CE QUI BOUGE
    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps de <see cref="EntityHandler_004642C0"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 74 instructions.
    ///
    /// <para>
    /// <b>Un balancier qui frappe, et qui avance de DEUX pas par image.</b> Tout le corps est ecrit
    /// deux fois a la suite : la vitesse <c>+0x12</c> gagne <c>+0x14</c> (qui vaut <c>±1</c>),
    /// l'angle <c>+0x34</c> gagne la vitesse, et chaque fois que la vitesse atteint <c>±0x28</c>
    /// elle se retourne et le son <c>0x1a</c> de la zone part (hauteur <c>0x400</c>, gain
    /// <c>0x1000</c>). Le battement est donc double a chaque image.
    /// </para>
    /// <para>
    /// <b>Il frappe a chaque image, pas seulement en bout de course</b> :
    /// <see cref="StrikePartyMembers_0045DA40"/> est appele a la naissance <b>et</b> a la fin de
    /// chaque image, pour <c>10 %</c> des points de vie maximum. La vitesse de l'entite est remise
    /// a zero en tete par <see cref="ClearEntityVelocity_00460FC0"/> : il tourne sur place.
    /// </para>
    /// </summary>
    private static void TickCastlePendulum_004642C0(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x28;
            *(short*)(e + 0x14) = -1;
            StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);   // Ghidra: FUN_0045da40
            return;
        }

        for (int half = 0; half < 2; half++)
        {
            *(short*)(e + 0x12) += *(short*)(e + 0x14);

            if (*(short*)(e + 0x12) == -0x28 || *(short*)(e + 0x12) == 0x28)
            {
                *(short*)(e + 0x14) = (short)-*(short*)(e + 0x14);
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1a), 0x400, 0x1000, 0, 0);
            }

            *(short*)(e + 0x34) += *(short*)(e + 0x12);
        }

        StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);
    }

    /// <summary>Ghidra: FUN_004642c0 @ 0x004642c0 — creneau <b>8</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_004642C0(byte* e) => TickCastlePendulum_004642C0(e);

    /// <summary>Ghidra: FUN_00465bf0 @ 0x00465bf0 — creneau <b>8</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b> avec le precedent.</summary>
    private static void EntityHandler_00465BF0(byte* e) => TickCastlePendulum_004642C0(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_004643B0"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 43 instructions.
    ///
    /// <para>
    /// <b>Un decor qui dodeline.</b> A la naissance il se penche d'un angle tire entre <c>-10</c>
    /// et <c>+9</c>, et se donne un sens <c>±1</c> tire a pile ou face. Ensuite l'inclinaison
    /// <c>+0x14</c> avance d'un cran par image et l'angle <c>+0x34</c> de <b>quatre</b> ; aux
    /// butees <c>±0x14</c>, le sens se retourne.
    /// </para>
    /// <para>
    /// <b>Un tirage est perdu a chaque image</b> : le corps appelle <c>rand()</c> juste avant de
    /// rendre la main et ne lit pas le resultat. Ce n'est pas un artefact du desassemblage — c'est
    /// un appel qui decale le flux des tirages de tout ce qui suit dans l'image. Reproduit.
    /// </para>
    /// </summary>
    private static void TickCastleNodding_004643B0(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            short tilt = (short)(CrtRand() % 0x14 - 10);
            int toss = CrtRand();

            *(short*)(e + 0x14) = tilt;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x34) += tilt;
            *(short*)(e + 0x12) = (short)((toss % 2) * 2 - 1);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short step = *(short*)(e + 0x12);
            *(short*)(e + 0x14) += step;
            *(short*)(e + 0x34) += (short)(step * 4);

            if (*(short*)(e + 0x14) == 0x14 || *(short*)(e + 0x14) == -0x14)
            {
                *(short*)(e + 0x12) = (short)-step;
            }
        }

        CrtRand();   // Ghidra: tirage PERDU a chaque image, jamais lu
    }

    /// <summary>Ghidra: FUN_004643b0 @ 0x004643b0 — creneau <b>9</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_004643B0(byte* e) => TickCastleNodding_004643B0(e);

    /// <summary>Ghidra: FUN_00465ce0 @ 0x00465ce0 — creneau <b>9</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b>.</summary>
    private static void EntityHandler_00465CE0(byte* e) => TickCastleNodding_004643B0(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00464440"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 48 et 52 instructions.
    ///
    /// <para>
    /// <b>Un panneau qui bascule une fois pour toutes.</b> Il retient son angle de depart dans
    /// <c>+0x14</c>, puis l'etat 1 <b>accelere</b> — <c>+0x12</c> monte d'un par image et l'angle
    /// gagne <c>+0x12</c> — jusqu'a <c>depart + 0x514</c> ; l'etat 2 <b>freine</b> de la meme facon
    /// jusqu'a <c>depart + 0x800</c>. Alors le bit <c>0x40000000</c> est pose et le corps ne fait
    /// plus rien : il ne se rejouera pas.
    /// </para>
    /// <para>
    /// <b>Les deux exemplaires ne s'arretent pas au meme endroit.</b> Celui des sous-zones 0 a 4
    /// repose l'angle <b>sur son depart</b> — il revient d'ou il vient. Celui des sous-zones 6 a 10
    /// le laisse a <c>depart + 0x800</c>, un demi-tour plus loin, et <b>replie les deux angles sur
    /// douze bits</b> a la naissance (<c>&amp; 0xfff</c>), ce que l'autre ne fait pas.
    /// </para>
    /// </summary>
    /// <param name="wrapAtBirth">Vrai pour la copie des sous-zones 6 a 10 seulement.</param>
    /// <param name="finalOffset">Ce qui s'ajoute a l'angle de depart quand tout est fini :
    /// <c>0</c> d'un cote, <c>0x800</c> de l'autre.</param>
    private static void TickCastleTiltOnce_00464440(byte* e, bool wrapAtBirth, short finalOffset)
    {
        ClearEntityVelocity_00460FC0(e);

        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = *(short*)(e + 0x34);
            *(short*)(e + 0x12) = 0;

            if (wrapAtBirth)
            {
                *(short*)(e + 0x14) &= 0xfff;
                *(short*)(e + 0x34) = (short)(*(short*)(e + 0x34) & 0xfff);
            }
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;
            *(short*)(e + 0x34) += *(short*)(e + 0x12);

            if (*(short*)(e + 0x34) >= *(short*)(e + 0x14) + 0x514) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x12))--;
        *(short*)(e + 0x34) += *(short*)(e + 0x12);

        if (*(short*)(e + 0x34) < *(short*)(e + 0x14) + 0x800) return;

        *(uint*)e |= 0x40000000;
        *(short*)(e + 0x34) = (short)(*(short*)(e + 0x14) + finalOffset);
        *(short*)(e + 0x10) = 0;
    }

    /// <summary>Ghidra: FUN_00464440 @ 0x00464440 — creneau <b>10</b> du bloc <c>0x00558e28</c> :
    /// il revient sur son angle de depart.</summary>
    private static void EntityHandler_00464440(byte* e)
        => TickCastleTiltOnce_00464440(e, wrapAtBirth: false, finalOffset: 0);

    /// <summary>Ghidra: FUN_00465d70 @ 0x00465d70 — creneau <b>10</b> du bloc <c>0x005594a8</c> :
    /// il s'arrete un demi-tour plus loin, et replie ses angles sur douze bits.</summary>
    private static void EntityHandler_00465D70(byte* e)
        => TickCastleTiltOnce_00464440(e, wrapAtBirth: true, finalOffset: 0x800);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00464660"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, dix-sept instructions.
    ///
    /// <para>
    /// <b>Un demi-tour en trente-deux images, une seule fois.</b> Le cap <c>+0x32</c> gagne
    /// <c>0x10</c> par image ; au trente-deuxieme tour, le compteur revient a zero et le bit
    /// <c>0x40000000</c> est pose — le corps ne repassera plus. <c>+0x10</c> sert de compteur, pas
    /// d'etat.
    /// </para>
    /// </summary>
    private static void TickCastleHalfTurn_00464660(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if ((*(uint*)e & 0x40000000) != 0) return;

        *(short*)(e + 0x32) += 0x10;
        (*(short*)(e + 0x10))++;

        if (*(short*)(e + 0x10) != 0x20) return;

        *(short*)(e + 0x10) = 0;
        *(uint*)e |= 0x40000000;
    }

    /// <summary>Ghidra: FUN_00464660 @ 0x00464660 — creneau <b>13</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_00464660(byte* e) => TickCastleHalfTurn_00464660(e);

    /// <summary>Ghidra: FUN_00465fa0 @ 0x00465fa0 — creneau <b>13</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b>.</summary>
    private static void EntityHandler_00465FA0(byte* e) => TickCastleHalfTurn_00464660(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00464900"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 77 instructions. Les deux exemplaires sont <b>identiques</b>.
    ///
    /// <para>
    /// <b>Une trappe qui s'ouvre d'un coup et se rabat doucement.</b> L'etat 1 ouvre : l'angle
    /// <c>+0x34</c> gagne un pas qui grossit de <b>six</b> par image, pendant que l'entite descend
    /// de <c>0x8000</c> — position et vitesse ensemble, par
    /// <see cref="AddEntityVelocity_00454580"/>, pour que ce qui est pose dessus suive. La course
    /// s'arrete a <c>depart + 0x352</c>.
    /// </para>
    /// <para>
    /// L'etat 3 rabat : l'angle perd <c>0x41</c> par image et l'entite remonte de <c>0xa000</c>.
    /// <b>Deux fins doivent se produire ensemble</b> — l'angle revenu a son depart et la hauteur
    /// revenue a celle de la naissance (gardee en <c>+0x16</c>, en cases) — et c'est un
    /// <b>compteur</b> qui les additionne : <c>cVar3</c> vaut 0, 1 ou 2, et seul 2 pose le bit
    /// <c>0x40000000</c>. Les deux bornes sont donc verifiees dans la meme image, pas l'une puis
    /// l'autre.
    /// </para>
    /// </summary>
    private static void TickCastleTrapdoor_00464900(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = *(short*)(e + 0x34);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 6;
            *(short*)(e + 0x34) += *(short*)(e + 0x12);
            *(int*)(e + 0x20) -= 0x8000;
            AddEntityVelocity_00454580(e, 0, -0x8000, 0);   // Ghidra: FUN_00454580

            if (*(short*)(e + 0x34) > *(short*)(e + 0x14) + 0x352)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x34) = (short)(*(short*)(e + 0x14) + 0x352);
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 3) return;

        *(short*)(e + 0x34) -= 0x41;
        *(int*)(e + 0x20) += 0xa000;
        AddEntityVelocity_00454580(e, 0, 0xa000, 0);

        short rest = *(short*)(e + 0x14);
        int done = 0;

        if (*(short*)(e + 0x34) <= rest)
        {
            *(short*)(e + 0x34) = rest;
            done++;
        }

        int floor = *(short*)(e + 0x16) * 0x1000;
        if (floor <= *(int*)(e + 0x20))
        {
            done++;
            *(int*)(e + 0x20) = floor;
        }

        if (done != 2) return;

        *(short*)(e + 0x10) = 0;
        *(uint*)e |= 0x40000000;
    }

    /// <summary>Ghidra: FUN_00464900 @ 0x00464900 — creneau <b>16</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_00464900(byte* e) => TickCastleTrapdoor_00464900(e);

    /// <summary>Ghidra: FUN_00466240 @ 0x00466240 — creneau <b>16</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b>.</summary>
    private static void EntityHandler_00466240(byte* e) => TickCastleTrapdoor_00464900(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00464AC0"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 34 instructions.
    ///
    /// <para>
    /// <b>Une nacelle qui va et vient entre le sol et <c>0x320000</c></b>, et son mouvement n'a pas
    /// de ressort : <c>+0x12</c> est une vitesse entiere en cases, qui gagne <b>un</b> chaque fois
    /// que la nacelle passe sous zero et en perd <b>un</b> chaque fois qu'elle passe au-dessus de
    /// <c>0x31ffff</c>. Elle accelere donc tant qu'elle est hors bornes et derive librement entre
    /// les deux.
    /// </para>
    /// <para>
    /// La position et la vitesse avancent ensemble (<see cref="AddEntityVelocity_00454580"/>), ce
    /// qui emmene ce qui est pose dessus.
    /// </para>
    /// </summary>
    private static void TickCastleLift_00464AC0(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x20) += *(short*)(e + 0x12) * 0x1000;
        AddEntityVelocity_00454580(e, 0, *(short*)(e + 0x12) << 0xc, 0);

        if (*(int*)(e + 0x20) > 0x31ffff) (*(short*)(e + 0x12))--;
        if (*(int*)(e + 0x20) < 1) (*(short*)(e + 0x12))++;
    }

    /// <summary>Ghidra: FUN_00464ac0 @ 0x00464ac0 — creneau <b>19</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_00464AC0(byte* e) => TickCastleLift_00464AC0(e);

    /// <summary>Ghidra: FUN_00466400 @ 0x00466400 — creneau <b>19</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b>.</summary>
    private static void EntityHandler_00466400(byte* e) => TickCastleLift_00464AC0(e);

    /// <summary>
    /// Ghidra: <c>0x00464bc0</c> — creneau <b>21</b> du bloc <c>0x00558e28</c>, les deux groupes.
    /// Absent de <c>Blaze.c</c> ; trente-sept instructions.
    ///
    /// <para>
    /// <b>Une montee en quarante images, avec un fondu aux deux bouts.</b> La vitesse
    /// <c>+0x54</c> part de <c>0x3000</c> et gagne <b>un huitieme d'elle-meme</b> par image tant que
    /// le compteur est sous 13, puis en <b>perd</b> un huitieme au-dela : une exponentielle
    /// montante puis descendante, sans jamais atteindre zero.
    /// </para>
    /// <para>
    /// A la quarantieme image le bit <c>0x40000000</c> est pose et la hauteur n'avance <b>pas</b>
    /// cette image-la : le corps sort avant l'addition. Les trente-neuf autres ajoutent
    /// <c>+0x54</c> a <c>+0x20</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00464BC0(byte* e)
    {
        if ((*(uint*)e & 0x40000000) != 0) return;

        short step = *(short*)(e + 0x10);
        if (step == 0) *(int*)(e + 0x54) = 0x3000;

        int speed = *(int*)(e + 0x54);
        *(int*)(e + 0x54) = step > 0x0c ? speed - (speed >> 3) : speed + (speed >> 3);

        *(short*)(e + 0x10) = (short)(step + 1);

        if ((short)(step + 1) == 0x28)
        {
            *(uint*)e |= 0x40000000;
            return;
        }

        *(int*)(e + 0x20) += *(int*)(e + 0x54);
    }

    /// <summary>
    /// Le corps des <b>six</b> plates-formes coulissantes des creneaux 22, 23 et 24. Decompile
    /// dans <c>Blaze.c</c>, 62 instructions chacune.
    ///
    /// <para>
    /// <b>Une navette qui fait l'aller-retour en deux cents images.</b> <c>+0x10</c> n'est pas un
    /// etat mais une <b>horloge</b> qui monte de 0 a 199 et reboucle ; <c>+0x12</c> est la vitesse.
    /// Sur la premiere moitie (0 a 99) elle accelere pendant dix images, croise, puis <b>freine a
    /// partir de <paramref name="brake"/></b> jusqu'a l'arret — et l'horloge saute alors a 101.
    /// Sur la seconde moitie, tout se refait en sens inverse.
    /// </para>
    /// <para>
    /// Le deplacement suit le cap <c>+0x32</c> : <c>+0x24</c> gagne <c>sinus × vitesse</c> et
    /// <c>+0x1c</c> <b>perd</b> <c>cosinus × vitesse</c> — un quart de tour d'ecart avec la
    /// convention habituelle. <see cref="AddEntityVelocity_00454580"/> recoit le meme vecteur, de
    /// sorte que ce qui est pose dessus voyage avec.
    /// </para>
    /// <para>
    /// <b>Les trois formes ne different que par <paramref name="brake"/></b> — <c>0x19</c>,
    /// <c>0x29</c>, <c>0x39</c> —, donc par la longueur du palier a vitesse pleine : plus le
    /// freinage est tardif, plus la navette va loin. Les trois sont emises deux fois, une par
    /// groupe de sous-zones, et les six corps sont sinon identiques.
    /// </para>
    /// </summary>
    /// <param name="brake">L'image ou le freinage commence sur la premiere moitie. La seconde
    /// moitie freine a <c>brake + 0x64</c>.</param>
    private static void TickCastleShuttle_00464C30(byte* e, short brake)
    {
        ClearEntityVelocity_00460FC0(e);

        (*(short*)(e + 0x10))++;
        short clock = *(short*)(e + 0x10);

        if (clock < 100)
        {
            if (clock < 0x0b) (*(short*)(e + 0x12))++;

            if (clock > brake)
            {
                (*(short*)(e + 0x12))--;
                if (*(short*)(e + 0x12) == 0) *(short*)(e + 0x10) = 0x65;
            }
        }

        clock = *(short*)(e + 0x10);

        if (clock > 99)
        {
            if (clock < 0x6f) (*(short*)(e + 0x12))--;

            if (clock > brake + 0x64)
            {
                (*(short*)(e + 0x12))++;
                if (*(short*)(e + 0x12) == 0) *(short*)(e + 0x10) = 0;
            }
        }

        int speed = *(short*)(e + 0x12);
        int alongZ = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * speed;
        int alongX = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * speed;

        *(int*)(e + 0x24) += alongZ;
        *(int*)(e + 0x1c) -= alongX;
        AddEntityVelocity_00454580(e, -alongX, 0, alongZ);   // Ghidra: FUN_00454580
    }

    /// <summary>Ghidra: FUN_00464c30 @ 0x00464c30 — creneau <b>22</b> du bloc <c>0x00558e28</c>,
    /// freinage a <c>0x19</c> : la navette courte.</summary>
    private static void EntityHandler_00464C30(byte* e) => TickCastleShuttle_00464C30(e, 0x19);

    /// <summary>Ghidra: FUN_00464cf0 @ 0x00464cf0 — creneau <b>23</b>, freinage a
    /// <c>0x29</c>.</summary>
    private static void EntityHandler_00464CF0(byte* e) => TickCastleShuttle_00464C30(e, 0x29);

    /// <summary>Ghidra: FUN_00464db0 @ 0x00464db0 — creneau <b>24</b>, freinage a <c>0x39</c> :
    /// la navette longue.</summary>
    private static void EntityHandler_00464DB0(byte* e) => TickCastleShuttle_00464C30(e, 0x39);

    /// <summary>Ghidra: FUN_00466500 @ 0x00466500 — creneau <b>22</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b> avec <see cref="EntityHandler_00464C30"/>.</summary>
    private static void EntityHandler_00466500(byte* e) => TickCastleShuttle_00464C30(e, 0x19);

    /// <summary>Ghidra: FUN_004665c0 @ 0x004665c0 — creneau <b>23</b> du bloc
    /// <c>0x005594a8</c>.</summary>
    private static void EntityHandler_004665C0(byte* e) => TickCastleShuttle_00464C30(e, 0x29);

    /// <summary>Ghidra: FUN_00466680 @ 0x00466680 — creneau <b>24</b> du bloc
    /// <c>0x005594a8</c>.</summary>
    private static void EntityHandler_00466680(byte* e) => TickCastleShuttle_00464C30(e, 0x39);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00465020"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 23 instructions.
    ///
    /// <para>
    /// <b>Une respiration sur un seul axe.</b> L'horloge <c>+0x10</c> monte sans fin ; l'echelle
    /// <c>+0x2c</c> gagne <c>0xbe</c> par image pendant les vingt-cinq premieres, ne bouge plus
    /// jusqu'a la quarantieme, en perd autant de la quarante et unieme a la soixante-cinquieme,
    /// puis l'horloge revient a zero a la quatre-vingtieme.
    /// </para>
    /// <para>
    /// <b>La remise a zero est gardee par trois tests dont deux sont morts</b> : le binaire ecrit
    /// <c>0x42 &lt; n &amp;&amp; n &lt; 0x51 &amp;&amp; n == 0x50</c>. Seul le dernier compte.
    /// Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickCastleBreathing_00465020(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        (*(short*)(e + 0x10))++;
        short clock = *(short*)(e + 0x10);

        if (clock < 0x1a) *(short*)(e + 0x2c) += 0xbe;
        if (clock > 0x28 && clock < 0x42) *(short*)(e + 0x2c) -= 0xbe;

        // Ghidra: `0x42 < n && n < 0x51 && n == 0x50` — les deux premieres comparaisons ne
        // refusent rien que la troisieme n'ait deja refuse.
        if (clock > 0x42 && clock < 0x51 && clock == 0x50) *(short*)(e + 0x10) = 0;
    }

    /// <summary>Ghidra: FUN_00465020 @ 0x00465020 — creneau <b>27</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_00465020(byte* e) => TickCastleBreathing_00465020(e);

    /// <summary>Ghidra: FUN_00466840 @ 0x00466840 — creneau <b>27</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b>.</summary>
    private static void EntityHandler_00466840(byte* e) => TickCastleBreathing_00465020(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00465070"/> et de son miroir. Absent de
    /// <c>Blaze.c</c> ; 58 instructions. Les deux exemplaires sont <b>identiques</b>.
    ///
    /// <para>
    /// <b>Un vantail qui tourne de huit crans, dans le sens que le placement lui donne.</b>
    /// <c>+0x12</c> ne compte pas : il <b>choisit</b>. A <c>1</c> le vantail avance de <c>0x78</c>
    /// par image, a <c>2</c> il recule de <c>0x78</c>, a <c>0</c> il se remet a zero et attend.
    /// </para>
    /// <para>
    /// <b>Le son ne part qu'une fois</b> — le son <c>0x24</c> de la zone, hauteur <c>0x400</c>,
    /// gain <c>0x1000</c> — grace a <c>+0x98</c>, qui passe a 1 au premier coup et n'est remis a
    /// zero qu'a la fin de la course. Huit crans plus tard (<c>+0x10 == 8</c>), tout est remis a
    /// zero et le bit <c>0x40000000</c> est pose.
    /// </para>
    /// </summary>
    private static void TickCastleLeaf_00465070(byte* e)
    {
        if (*(short*)(e + 0x12) == 0)
        {
            *(short*)(e + 0x10) = 0;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x12) == 1)
        {
            *(short*)(e + 0x34) += 0x78;
            (*(short*)(e + 0x10))++;

            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x24), 0x400, 0x1000, 0, 0);
                *(short*)(e + 0x98) = 1;
            }
        }

        if (*(short*)(e + 0x12) == 2)
        {
            *(short*)(e + 0x34) -= 0x78;
            (*(short*)(e + 0x10))++;

            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x24), 0x400, 0x1000, 0, 0);
                *(short*)(e + 0x98) = 1;
            }
        }

        if (*(short*)(e + 0x10) != 8) return;

        *(short*)(e + 0x10) = 0;
        *(uint*)e |= 0x40000000;
        *(short*)(e + 0x12) = 0;
        *(short*)(e + 0x98) = 0;
    }

    /// <summary>Ghidra: <c>0x00465070</c> — creneau <b>28</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_00465070(byte* e) => TickCastleLeaf_00465070(e);

    /// <summary>Ghidra: <c>0x00466890</c> — creneau <b>28</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b>.</summary>
    private static void EntityHandler_00466890(byte* e) => TickCastleLeaf_00465070(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00464A10"/> et de son miroir. Absent de
    /// <c>Blaze.c</c> ; 58 instructions.
    ///
    /// <para>
    /// <b>Une plate-forme qui flotte entre quatre repères.</b> Sa hauteur de naissance, en cases,
    /// est gardee en <c>+0x16</c> ; <c>+0x12</c> est la vitesse et <c>+0x14</c> l'acceleration,
    /// toutes deux en cases entieres. Chaque image, la hauteur gagne la vitesse — position et
    /// vitesse ensemble, pour emmener ce qui est pose dessus.
    /// </para>
    /// <list type="bullet">
    ///   <item>sous <c>depart + </c><paramref name="lowPush"/> : la vitesse gagne
    ///     l'acceleration ;</item>
    ///   <item>au-dessus de <c>depart + 0x258</c> : elle la perd ;</item>
    ///   <item>sous le depart meme : vitesse et acceleration sont <b>remises a 1</b> — la
    ///     plate-forme est relancee vers le haut ;</item>
    ///   <item>au-dessus de <c>depart + 0x320</c> : vitesse <c>-1</c>, acceleration <c>1</c>.</item>
    /// </list>
    /// <para>
    /// <b>Les deux exemplaires n'ont pas la meme relance basse</b> : <c>0xc8</c> pour les
    /// sous-zones 0 a 4, <c>0xa0</c> pour les sous-zones 6 a 10. Une immediate, et c'est tout.
    /// </para>
    /// </summary>
    private static void TickCastleFloat_00464A10(byte* e, int lowPush)
    {
        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        if (*(short*)(e + 0x10) != 1) return;

        int lift = *(short*)(e + 0x12) << 0xc;
        *(int*)(e + 0x20) += lift;
        AddEntityVelocity_00454580(e, 0, lift, 0);   // Ghidra: FUN_00454580

        int height = *(int*)(e + 0x20) >> 0xc;
        int start = *(short*)(e + 0x16);

        if (height <= start + lowPush) *(short*)(e + 0x12) += *(short*)(e + 0x14);
        if (height >= start + 0x258) *(short*)(e + 0x12) -= *(short*)(e + 0x14);

        if (height <= start)
        {
            *(short*)(e + 0x12) = 1;
            *(short*)(e + 0x14) = 1;
        }

        if (height >= start + 0x320)
        {
            *(short*)(e + 0x12) = -1;
            *(short*)(e + 0x14) = 1;
        }
    }

    /// <summary>Ghidra: <c>0x00464a10</c> — creneau <b>18</b> du bloc <c>0x00558e28</c>, relance
    /// basse a <c>0xc8</c>.</summary>
    private static void EntityHandler_00464A10(byte* e) => TickCastleFloat_00464A10(e, 0xc8);

    /// <summary>Ghidra: <c>0x00466350</c> — creneau <b>18</b> du bloc <c>0x005594a8</c>, relance
    /// basse a <c>0xa0</c>.</summary>
    private static void EntityHandler_00466350(byte* e) => TickCastleFloat_00464A10(e, 0xa0);

    /// <summary>
    /// Ghidra: <c>0x00466740</c> — creneau <b>26</b> du bloc <c>0x005594a8</c>. Absent de
    /// <c>Blaze.c</c> ; 81 instructions.
    ///
    /// <para>
    /// <b>Une navette a horloge de quatre-vingts images</b>, plus simple que celle des creneaux 22
    /// a 24 : pas d'acceleration, un pas fixe de <b>huit fois</b> le sinus et le cosinus du cap.
    /// </para>
    /// <list type="table">
    ///   <item><term>0 a 25</term><description>elle <b>recule</b> — <c>+0x1c</c> et <c>+0x24</c>
    ///     perdent le pas ;</description></item>
    ///   <item><term>26</term><description>le son <c>0x13</c> de la zone (hauteur <c>0x400</c>,
    ///     gain <c>0x1000</c>) : le claquement de la butee ;</description></item>
    ///   <item><term>41 a 65</term><description>elle <b>avance</b> du meme pas ;</description></item>
    ///   <item><term>67</term><description>le meme son ;</description></item>
    ///   <item><term>80</term><description>l'horloge revient a zero.</description></item>
    /// </list>
    /// <para>
    /// <b>Elle ne passe pas par <see cref="AddEntityVelocity_00454580"/></b> : sa position bouge,
    /// sa vitesse non — ce qui est pose dessus ne suit donc pas. C'est ce qui la separe des six
    /// navettes.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466740(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        (*(short*)(e + 0x10))++;

        int alongX = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) << 3;
        int alongZ = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) << 3;

        short clock = *(short*)(e + 0x10);

        if (clock <= 0x19)
        {
            *(int*)(e + 0x1c) -= alongX;
            *(int*)(e + 0x24) -= alongZ;
        }
        else if (clock < 0x29 && clock == 0x1a)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x13), 0x400, 0x1000, 0, 0);
        }

        clock = *(short*)(e + 0x10);

        if (clock >= 0x29 && clock <= 0x41)
        {
            *(int*)(e + 0x1c) += alongX;
            *(int*)(e + 0x24) += alongZ;
        }

        if (clock > 0x42 && clock <= 0x50)
        {
            if (clock == 0x43)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x13), 0x400, 0x1000, 0, 0);
            }

            if (*(short*)(e + 0x10) == 0x50) *(short*)(e + 0x10) = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00464e70 @ 0x00464e70 — creneau <b>26</b> du bloc <c>0x00558e28</c>, le miroir
    /// est de <see cref="EntityHandler_00466740"/>. Decompile dans <c>Blaze.c</c>, 141 instructions.
    ///
    /// <para>
    /// <b>La meme navette a horloge de quatre-vingts images, mais celle-ci cogne.</b> Le mouvement
    /// et les deux claquements sont ceux du miroir, au meme pas de huit fois le sinus et le cosinus
    /// du cap. Deux choses en plus :
    /// </para>
    /// <list type="bullet">
    ///   <item><b>de l'image 26 a l'image 40</b>, pendant la butee, elle frappe par
    ///     <see cref="StrikePartyMembers_0045DA40"/> — 1 %, les membres de son demi-octet bas de
    ///     contact, un verrou de <b>six</b> images ;</item>
    ///   <item><b>a chaque image</b>, elle <b>projette</b> tout membre vivant dont le bit de contact
    ///     <c>+9</c> est pose : bit <c>1</c> sur le membre, vitesse verticale <c>0xa000</c>, et une
    ///     poussee de <c>0x28</c> fois le sinus et le cosinus du cap <b>tourne d'un demi-tour</b>
    ///     (<c>+0x800</c>) — dans le sens ou elle recule. Elle garde alors le bit <c>0x2000</c> deux
    ///     images (<c>+0x12</c>), puis le rend.</item>
    /// </list>
    /// <para>
    /// <b>Les deux se genent.</b> <see cref="StrikePartyMembers_0045DA40"/> remet l'octet de contact
    /// a zero en sortant ; pendant les quinze images de butee, la projection qui suit ne voit donc
    /// jamais personne. Elle ne projette qu'en course. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00464E70(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        (*(short*)(e + 0x10))++;

        int alongX = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));   // Ghidra: Ordinal_451
        int alongZ = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32));        // Ghidra: Ordinal_450

        short clock = *(short*)(e + 0x10);

        if (clock < 0x1a)
        {
            *(int*)(e + 0x1c) -= alongX * 8;
            *(int*)(e + 0x24) -= alongZ * 8;
        }
        else if (clock < 0x29)
        {
            if (clock == 0x1a)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x13), 0x400, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            }

            StrikePartyMembers_0045DA40(e, 1, 0, 0, 6);   // Ghidra: FUN_0045da40
        }

        clock = *(short*)(e + 0x10);

        if (clock > 0x28 && clock < 0x42)
        {
            *(int*)(e + 0x1c) += alongX * 8;
            *(int*)(e + 0x24) += alongZ * 8;
        }

        if (clock > 0x42 && clock < 0x51)
        {
            if (clock == 0x43)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x13), 0x400, 0x1000, 0, 0);
            }

            if (*(short*)(e + 0x10) == 0x50) *(short*)(e + 0x10) = 0;
        }

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) < 0)
        {
            *(short*)(e + 0x12) = 0;
            *(uint*)e &= 0xffffdfff;
        }

        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;
            if (((byte)(1 << i) & e[9]) == 0) continue;

            *(short*)(e + 0x12) = 2;
            *(uint*)e |= 0x2000;

            *(uint*)member |= 1;
            *(int*)(member + 0x54) = 0xa000;
            *(int*)(member + 0x50) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32) + 0x800) * 0x28;
            *(int*)(member + 0x58) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32) + 0x800) * 0x28;
        }

        e[9] = 0;
    }

    // =====================================================================
    // PALIER 3 (2026-09-18) — CE QUI BOUGE, SUITE
    // =====================================================================

    /// <summary>
    /// Le corps de <see cref="EntityHandler_004646A0"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 106 instructions ; les deux exemplaires sont <b>identiques</b>.
    ///
    /// <para>
    /// <b>Une plate-forme qui monte a sa place, puis flotte.</b> Deux vies, choisies par le bit
    /// <c>0x40000000</c> de l'entite :
    /// </para>
    /// <list type="table">
    ///   <item><term>bit eteint — la montee</term><description>a l'amorcage (etat 0 ou 1) elle passe
    ///     a l'etat 2 avec un elan de <b>dix</b> et allume <c>0x10000000</c>. Elle commence par
    ///     <b>s'enfoncer</b> de 9, 8, … 0 unites, puis monte de <b>5 par image</b> jusqu'a la hauteur
    ///     <c>+0x14</c> du placement ; arrivee, elle s'y fixe, allume <c>0x40000000</c> et passe a la
    ///     seconde vie.</description></item>
    ///   <item><term>bit allume — le flottement</term><description>a l'etat 0 (une plate-forme
    ///     placee deja montee) elle prend sa hauteur courante pour plafond. Puis elle oscille d'une
    ///     unite par image entre ce plafond et <b>vingt unites</b> au-dessous. Chaque image ou
    ///     personne n'est pose dessus (bit <c>0x10</c>), elle eteint <c>0x10000000</c>.</description></item>
    /// </list>
    /// <para>
    /// Chaque deplacement passe aussi par <see cref="AddEntityVelocity_00454580"/> : ce qui est pose
    /// dessus est emporte. <c>+0x12</c> recoit 100 a l'arrivee et n'est plus jamais lu.
    /// </para>
    /// </summary>
    private static void TickCastleRisingPlatform_004646A0(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if ((*(uint*)e & 0x40000000) == 0)
        {
            if (*(short*)(e + 0x10) == 1 || *(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 10;
                *(uint*)e |= 0x10000000;
            }

            if (*(short*)(e + 0x10) != 2) return;

            if (*(short*)(e + 0x12) > 0)
            {
                short impetus = (short)(*(short*)(e + 0x12) - 1);
                *(short*)(e + 0x12) = impetus;
                *(int*)(e + 0x20) += impetus * -0x1000;
                AddEntityVelocity_00454580(e, 0, impetus * -0x1000, 0);   // Ghidra: FUN_00454580
                return;
            }

            *(int*)(e + 0x20) += 0x5000;
            AddEntityVelocity_00454580(e, 0, 0x5000, 0);

            int top = *(short*)(e + 0x14) * 0x1000;
            if (top <= *(int*)(e + 0x20))
            {
                *(int*)(e + 0x20) = top;
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x16) = -1;
                *(uint*)e |= 0x40000000;
                *(short*)(e + 0x12) = 100;
            }

            return;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x16) = -1;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x20) += *(short*)(e + 0x16) * 0x1000;
            AddEntityVelocity_00454580(e, 0, *(short*)(e + 0x16) << 0xc, 0);

            if (*(short*)(e + 0x14) * 0x1000 <= *(int*)(e + 0x20) ||
                *(int*)(e + 0x20) <= (*(short*)(e + 0x14) - 0x14) * 0x1000)
            {
                *(short*)(e + 0x16) = (short)-*(short*)(e + 0x16);
            }
        }

        if ((*(uint*)e & 0x10) == 0) *(uint*)e &= 0xefffffff;
    }

    /// <summary>Ghidra: FUN_004646a0 @ 0x004646a0 — creneau <b>14</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_004646A0(byte* e) => TickCastleRisingPlatform_004646A0(e);

    /// <summary>Ghidra: FUN_00465fe0 @ 0x00465fe0 — creneau <b>14</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b>.</summary>
    private static void EntityHandler_00465FE0(byte* e) => TickCastleRisingPlatform_004646A0(e);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_004644F0"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 120 instructions ; les deux exemplaires sont <b>identiques</b>.
    ///
    /// <para>
    /// <b>Ce qui bascule quand on s'en approche, et fait trembler l'ecran en tombant.</b>
    /// </para>
    /// <list type="table">
    ///   <item><term>etat 0 — l'attente</term><description>il retient son angle <c>+0x34</c> dans
    ///     <c>+0x14</c>. Des qu'un membre vivant passe a <b>moins de cent unites</b> a plat, il passe
    ///     a l'etat 1 — le test se fait a chaque image, mais ne change que l'etat 0.</description></item>
    ///   <item><term>etat 1 — la chute</term><description>l'angle <c>+0x34</c> perd une vitesse qui
    ///     grandit d'un par image, jusqu'a <b>un quart de tour</b> (<c>0x400</c>) sous son angle de
    ///     depart. Arrive, il s'y fixe, <b>descend de dix unites</b> le temps d'une image et joue le
    ///     son <c>0x14</c> de la zone, hauteur <c>0x200</c>, gain <c>0x1800</c>.</description></item>
    ///   <item><term>etat 2 — l'impact</term><description>a la premiere image il remonte de ces dix
    ///     unites et frappe par <see cref="StrikePartyMembers_0045DA40"/> — <b>5 %</b>, les membres
    ///     de son demi-octet bas de contact, verrou par defaut. Puis il secoue la camera par
    ///     <see cref="TryPublishCameraEffectNearEntity"/> avec une amplitude qui change de signe et
    ///     perd un a chaque image : -9, 8, -7, … 2, -1, 0 ; a zero, il passe a l'etat 3, ou il ne fait
    ///     plus rien.</description></item>
    /// </list>
    /// </summary>
    private static void TickCastleToppler_004644F0(byte* e)
    {
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i < 4; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(member + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;
            int dz = (*(int*)(member + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

            if (distance < 100 && *(short*)(e + 0x10) == 0) *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 0;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = *(short*)(e + 0x34);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;
            *(short*)(e + 0x34) -= *(short*)(e + 0x12);

            if (*(short*)(e + 0x34) < *(short*)(e + 0x14) - 0x400)
            {
                *(short*)(e + 0x34) = (short)(*(short*)(e + 0x14) - 0x400);
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x14) = 10;
                *(int*)(e + 0x20) -= 0xa000;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x14), 0x200, 0x1800, 0, 0);   // Ghidra: FUN_00468080
                return;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        if (*(short*)(e + 0x14) == 10)
        {
            *(int*)(e + 0x20) += 0xa000;
            StrikePartyMembers_0045DA40(e, 5, 0, 0, 0);   // Ghidra: FUN_0045da40
        }

        short swing = (short)-*(short*)(e + 0x14);
        short step = swing < 1 ? (short)1 : (short)-1;
        *(short*)(e + 0x14) = (short)(swing + step);

        TryPublishCameraEffectNearEntity((uint*)e, 999, (short)(swing + step), 999, 0, 0);   // Ghidra: FUN_0041be60

        if (*(short*)(e + 0x14) == 0) *(short*)(e + 0x10) = 3;
    }

    /// <summary>Ghidra: FUN_004644f0 @ 0x004644f0 — creneau <b>12</b> du bloc
    /// <c>0x00558e28</c>.</summary>
    private static void EntityHandler_004644F0(byte* e) => TickCastleToppler_004644F0(e);

    /// <summary>Ghidra: FUN_00465e30 @ 0x00465e30 — creneau <b>12</b> du bloc <c>0x005594a8</c>.
    /// <b>Zero difference</b>.</summary>
    private static void EntityHandler_00465E30(byte* e) => TickCastleToppler_004644F0(e);
}
