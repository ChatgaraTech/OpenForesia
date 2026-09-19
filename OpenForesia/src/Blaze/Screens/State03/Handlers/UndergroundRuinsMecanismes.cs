// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MECANISMES DES RUINES SOUTERRAINES — zone 7, sous-zones 0 a 2.
//
// Ouvert le 2026-09-19 avec la zone, sur le patron de `ImmortalCastleMecanismes.cs`, et par le test
// d'appartenance d'ARCHITECTURE.md : les deux corps d'ici prennent la PARTIE en charge.
//
//   0x0042EF90   creneau 32 de 0x0055a4a8   le reveil des entites de scene de type 2 — il fait
//                                            avancer leur animation et LANCE LEUR SCRIPT
//   0x00541C90   creneau 0 de 0x0055a3a8    LA FABRIQUE DES SOSIES : chaque image, en sous-zone 2,
//                                            il recopie les fiches du groupe dans les quatre
//                                            premiers modeles de la zone
//
// Dumps : Legacy/Raw/Blaze/UndergroundRuins/.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LE REVEIL DES ENTITES DE SCENE DE TYPE 2 — creneau 32 du bloc 0x0055a4a8
    //
    // Le meme mecanisme que les reveils de `Entities/WakeState.cs` (FUN_00404f70 et ses soeurs) :
    // on arme l'etat depuis la consigne, puis on aiguille sur les trente bits bas de +0x5c. Mais
    // les trois pas appeles NE SONT PAS ceux de WakeState, et le desassemblage le montre a trois
    // endroits :
    //
    //   - chacun commence par `test esi,esi ; jne ; mov esi,5DE558h` : un enregistrement nul est
    //     remplace par la liste d'entites, garde que les pas de WakeState n'ont pas ;
    //   - le script de fin part sur l'entite +0x70, et non +0x6c ;
    //   - le second pas n'existe pas ailleurs : une sequence jouee, une pause de dix images, puis
    //     la meme sequence REJOUEE A L'ENVERS.
    //
    // Ils sont donc ecrits a part plutot que de glisser un parametre dans les corps communs.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0042ef90 @ 0x0042ef90 — creneau <b>32</b> du bloc <c>0x0055a4a8</c> (table
    /// <c>0x0056b0c8</c>). Decompilee dans <c>Blaze.c</c>, 41 instructions.
    ///
    /// <para>
    /// <b>Une seule passe, puis il s'eteint.</b> Il balaie les trente-deux entites de scene de
    /// <c>0x005ecaa0</c>, et pour chacune de <b>type 2</b> (<c>+0x0c</c>), vivante et portant le
    /// bit <c>8</c>, il arme l'etat (<see cref="ArmSceneEntityState_0042F220"/>) puis joue l'etat
    /// 1 (<see cref="AdvanceSceneEntityState_0042F010"/>, sequence 0) ou l'etat 2
    /// (<see cref="PlaySceneEntityStateThereAndBack_0042F130"/>, sequence 3). Les deux tests
    /// relisent <c>+0x5c</c> : le premier pas peut faire passer a l'etat 2 dans le meme tour.
    /// </para>
    /// <para>
    /// Il efface son propre bit vivant a la fin de <b>chaque</b> appel — il ne tourne donc
    /// qu'une image, a moins que quelqu'un ne le reveille.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042EF90(byte* e)
    {
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            uint flags = *(uint*)record;
            if (record[0x0c] != 2 || (flags & 0x80000000) == 0 || (flags & 8) == 0) continue;

            ArmSceneEntityState_0042F220(record);                      // Ghidra: FUN_0042f220
            if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 1)
                AdvanceSceneEntityState_0042F010(record, 0);           // Ghidra: FUN_0042f010
            if ((*(uint*)(record + 0x5c) & 0x3fffffff) == 2)
                PlaySceneEntityStateThereAndBack_0042F130(record, 3);  // Ghidra: FUN_0042f130
        }

        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0042f220 @ 0x0042f220 — <b>arme l'etat d'animation</b>, comme
    /// <see cref="ArmSceneEntityState_004051D0"/> a une garde pres : un enregistrement nul devient
    /// <see cref="Resources.s_EntityList_005de558"/>. Decompilee dans <c>Blaze.c</c>.
    /// </summary>
    private static void ArmSceneEntityState_0042F220(byte* record)
    {
        if (record == null) record = Res.s_EntityList_005de558;

        uint wanted = (uint)(*(short*)(record + 0x10) & 0xff);

        if ((*(uint*)(record + 0x5c) & 0x3fffffff) != wanted
            && (*(uint*)(record + 0x5c) & 0x80000000) != 0)
        {
            *(int*)(record + 0x48) = *(short*)(record + 0x14);
            *(uint*)(record + 0x5c) = wanted;
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
    /// Ghidra: FUN_0042f010 @ 0x0042f010 — <b>le pas qui va jusqu'au bout</b>, la variante de
    /// <see cref="AdvanceSceneEntityState_00404FE0"/> propre aux ruines. Decompilee dans
    /// <c>Blaze.c</c>. Meme compteur <c>+0x48</c> a double usage, meme fin en deux regimes selon
    /// <c>+0x4c</c> (reboucler ou se figer) ; <b>deux ecarts</b> : la garde de l'enregistrement nul,
    /// et le script de fin qui part sur <c>+0x70</c> au lieu de <c>+0x6c</c>.
    /// </summary>
    /// <returns>1 si un script a demarre, 0 sinon. Ghidra rend la moitie haute du registre en
    /// residu ; aucun appelant ne la lit.</returns>
    private static uint AdvanceSceneEntityState_0042F010(byte* record, short kind)
    {
        if (record == null) record = Res.s_EntityList_005de558;

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

        if (*(int*)(record + 0x4c) == 0)
        {
            *(short*)(record + 0x1a) = 0;
            *(uint*)(record + 0x5c) |= 0x80000000;
            if (*(int*)(record + 0x44) == 0) return 0;
        }
        else
        {
            *(ushort*)(record + 0x1a) = (ushort)(frames - 1);
            *(uint*)(record + 0x5c) |= 0x80000000;
            if (*(int*)(record + 0x44) == 0)
            {
                *(int*)(record + 0x48) = 2;
                return 0;
            }
        }

        RunEntityScript(s_Unknown_005e97cc, *(byte**)(record + 0x70), (uint)*(int*)(record + 0x44));   // Ghidra: FUN_00513dd0
        *(int*)(record + 0x44) = 0;
        *(uint*)(record + 0x5c) |= 0x80000000;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_0042f130 @ 0x0042f130 — <b>l'aller, la pause, le retour</b>. Decompilee dans
    /// <c>Blaze.c</c> ; sans equivalent dans <c>WakeState.cs</c>.
    ///
    /// <para>
    /// Meme amorce que les autres pas (compteur <c>+0x48</c> a double usage, sequence rangee en
    /// <c>+0x18</c>, image <c>+0x1a</c> partie de -1), puis <b>trois phases</b> dans
    /// <c>+0x48</c> :
    /// </para>
    /// <list type="table">
    ///   <item><term>1</term><description>l'image avance d'une unite par tour ; a la dixieme,
    ///     <c>+0x9a = 10</c> et phase 2. <b>Le nombre d'images de la sequence n'est jamais
    ///     lu</b> : c'est dix, quelle que soit sa longueur.</description></item>
    ///   <item><term>2</term><description><c>+0x9a</c> decompte ; passe sous zero, phase 3 —
    ///     onze tours de pause.</description></item>
    ///   <item><term>3</term><description>l'image <b>recule</b> ; sous 1, elle est remise a zero,
    ///     l'etat annonce sa fin (<c>0x80000000</c>) et le script <c>+0x44</c> part sur
    ///     <c>+0x70</c>, une fois.</description></item>
    /// </list>
    /// <para>
    /// Les trois tests se suivent sans <c>else</c> : une phase qui s'acheve passe la main a la
    /// suivante dans le meme tour. Et la phase 3 n'a pas de sortie sans script — sans script,
    /// elle reste en phase 3 et redescend <c>+0x1a</c> a chaque appel, remis a zero a chaque fois.
    /// </para>
    /// </summary>
    /// <returns>1 si un script a demarre, 0 sinon.</returns>
    private static uint PlaySceneEntityStateThereAndBack_0042F130(byte* record, short kind)
    {
        if (record == null) record = Res.s_EntityList_005de558;

        if (*(int*)(record + 0x48) > 9)
        {
            int counter = *(int*)(record + 0x48) - 1;
            *(int*)(record + 0x48) = counter;
            if (counter < 10) *(int*)(record + 0x48) = 0;
        }

        if (*(int*)(record + 0x48) == 0)
        {
            *(int*)(record + 0x48) = 1;
            *(short*)(record + 0x1a) = -1;
            *(short*)(record + 0x18) = kind;
        }

        if (*(int*)(record + 0x48) == 1)
        {
            *(short*)(record + 0x1a) += 1;
            if (*(short*)(record + 0x1a) > 9)
            {
                *(short*)(record + 0x9a) = 10;
                *(int*)(record + 0x48) = 2;
            }
        }

        if (*(int*)(record + 0x48) == 2)
        {
            *(short*)(record + 0x9a) -= 1;
            if (*(short*)(record + 0x9a) < 0) *(int*)(record + 0x48) = 3;
        }

        if (*(int*)(record + 0x48) != 3) return 0;

        *(short*)(record + 0x1a) -= 1;
        if (*(short*)(record + 0x1a) >= 1) return 0;

        *(short*)(record + 0x1a) = 0;
        *(uint*)(record + 0x5c) |= 0x80000000;
        if (*(int*)(record + 0x44) == 0) return 0;

        RunEntityScript(s_Unknown_005e97cc, *(byte**)(record + 0x70), (uint)*(int*)(record + 0x44));   // Ghidra: FUN_00513dd0
        *(int*)(record + 0x44) = 0;
        *(uint*)(record + 0x5c) |= 0x80000000;
        return 1;
    }

    // =====================================================================
    // LA FABRIQUE DES SOSIES — creneau 0 du bloc 0x0055a3a8
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00541c90</c> — creneau <b>0</b> du bloc <c>0x0055a3a8</c> (table
    /// <c>0x0056afc8</c>, piscine <c>0x005f2d40</c>). <b>Absent de <c>Blaze.c</c></b> ; 79
    /// instructions, <c>0x00541c90..0x00541db5</c>,
    /// <c>Legacy/Raw/Blaze/UndergroundRuins/FUN_00541C90.asm</c>.
    ///
    /// <para>
    /// <b>Il ne fait rien hors de la sous-zone 2</b> (<see cref="s_Unknown_005de52c"/>). La, a
    /// chaque image et pour chaque membre du groupe (<see cref="s_PartySize_005e9020"/>, relu a
    /// chaque tour), il <b>refait les statistiques derivees de la fiche de personnage</b> — les trois
    /// memes appels que la montee de niveau, sans la mise a l'echelle de difficulte ni la
    /// finition d'equipement — puis <b>recopie ce membre dans le modele de meme rang</b> du
    /// repertoire de zone :
    /// </para>
    /// <list type="bullet">
    ///   <item>ses trois ressources, choisies par l'octet d'identite <c>+0x150</c> dans la table
    ///     <c>+0x84</c> (entrees a <c>+0</c>, <c>+0x80</c>, <c>+0x100</c>), vont dans les tables
    ///     <c>+0x10</c> (a partir de <c>+0x20</c>), <c>+0x2c</c> et <c>+0x7c</c> au rang du membre —
    ///     les memes tables que <see cref="LoadPartyMemberResources"/> lit pour le groupe
    ///     lui-meme ;</item>
    ///   <item>onze champs de la fiche vont dans la fiche de modele de <c>0x60</c> octets
    ///     (table <c>+0x98</c>) : <c>+0x144</c> sur le rang <c>+0x10</c>, <c>+0x148</c> et
    ///     <c>+0x14a</c> sur <c>+0x14</c> et <c>+0x16</c> (PV et PM des colonnes de
    ///     <c>modeles.csv</c>), et sept autres.</item>
    /// </list>
    /// <para>
    /// <b>C'est ce qui fait les Sosies.</b> Le CD donne a la sous-zone 2 quatre modeles nommes
    /// « Sosie », tous a 1 PV et 1 PM (<c>Documentation/Reference/Bestiaire/bestiaire.md</c>) : des
    /// coquilles, que ce corps remplit du groupe a chaque image. L'intelligence qui les anime,
    /// <see cref="TickSceneEntityAi_004DF680"/>, lit ensuite la classe du membre de meme rang
    /// pour choisir entre frapper et lancer un sort.
    /// </para>
    /// <para>
    /// <b>Le desassemblage du recensement deborde de ce corps</b> : les « appelees inconnues »
    /// <c>0x005420B0</c> et <c>0x00545C5C</c> que <c>Zone7ParComplexite.md</c> lui prete sont
    /// celles de la fonction suivante, qui commence a <c>0x00541dc0</c> derriere le <c>ret</c> de
    /// <c>0x00541db5</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00541C90(byte* e)
    {
        if (s_Unknown_005de52c != 2) return;

        byte* character = GetMainMemPtr() + 0xf0000;              // Ghidra: [[0x006087b4]] + 0xf0000

        for (int i = 0; i < s_PartySize_005e9020; i++, character += 0x2000)
        {
            CommitStagedStatBlock(character);                     // Ghidra: FUN_00538ab0
            ComputeDerivedStats(character);                       // Ghidra: FUN_00538c50
            ApplyEquipmentStatBonuses(character);                 // Ghidra: FUN_00538e20

            byte* resources = *(byte**)(s_Unknown_005e97cc + 0x84) + character[0x150] * 8;

            *(int*)(*(byte**)(s_Unknown_005e97cc + 0x10) + i * 8 + 0x20) = *(int*)resources;
            *(int*)(*(byte**)(s_Unknown_005e97cc + 0x2c) + i * 8) = *(int*)(resources + 0x80);
            *(int*)(*(byte**)(s_Unknown_005e97cc + 0x7c) + i * 4) = *(int*)(resources + 0x100);

            byte* model = *(byte**)(s_Unknown_005e97cc + 0x98) + i * 0x60;

            *(short*)(model + 0x10) = *(short*)(character + 0x144);
            *(short*)(model + 0x14) = *(short*)(character + 0x148);
            *(short*)(model + 0x16) = *(short*)(character + 0x14a);
            *(short*)(model + 0x1a) = *(short*)(character + 0x12e);
            *(short*)(model + 0x18) = *(short*)(character + 0x12c);
            *(int*)(model + 0x30) = *(int*)(character + 0x130);
            *(int*)(model + 0x34) = *(int*)(character + 0x134);
            *(int*)(model + 0x38) = *(int*)(character + 0x138);
            *(int*)(model + 0x3c) = *(int*)(character + 0x13c);
            *(int*)(model + 0x28) = *(int*)(character + 0x118);
            *(int*)(model + 0x2c) = *(int*)(character + 0x11c);
        }
    }
}
