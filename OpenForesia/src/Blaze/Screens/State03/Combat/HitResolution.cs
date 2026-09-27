// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA RESOLUTION DU COUP — qui touche, avec quoi, et combien ca coute.
//
// La portee (`TestMeleeReach_00535A90` et les deux masques de recouvrement de polygone), les
// elements (`ResolveElementalExchange_005398E0`, `DominantElement_005399E0` et la bascule
// `TiltDominantElement_005398A0`), les degats (`ApplyDamage_00539A20`), puis les deux resolutions
// qui les emploient : `ResolveMeleeHit_00539B80` et `ResolveSpellHit_0053A2B0`.
//
// C'EST LE FICHIER QUE LE BANDEAU « L'INTELLIGENCE DES ENTITES DE SCENE » CACHAIT : 1 300 lignes
// de combat rangees sous un titre d'IA, parce qu'elles avaient ete transcrites le meme jour que la
// premiere routine d'intelligence. Elles ne decident d'aucune action — elles resolvent celle qui a
// deja ete choisie.
//
// L'appariement est dans `CombatPairing.cs`, les effets dans `StatusEffects.cs`, ce que le coup
// fait VOIR dans `Effects/HitAndResistMarkers.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // L'INTELLIGENCE DES ENTITES DE SCENE — la famille 0x0049xxxx
    //
    // Le gestionnaire de sous-type 2 de la table installee 0x0056ad48, atteint par la passe
    // FUN_0050e680 des l'apparition d'un monstre. Ghidra n'a presque RIEN decompile de cette
    // plage : sur 0x00491290..0x004a1020 il ne rend que cinq corps, et pas celui-ci. Tout ce qui
    // suit vient du desassembleur (Legacy/Raw/Blaze/FUN_0049A980.asm et ses deux voisins).
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00535f20 @ 0x00535f20 — <b>la lecture de situation</b> d'une entite de scene :
    /// elle remplit cinq valeurs a partir de l'entite, de sa cible et de son bloc de statistiques.
    /// C'est le seul appel qui precede la decision, et tout
    /// <see cref="TickSceneEntityAi_0049A980"/> raisonne sur ce qu'elle rend.
    ///
    /// <para>
    /// <b>Le corps est dans <c>Blaze.c</c>, mais sa signature ne l'est pas.</b> Ghidra a perdu
    /// <c>EBX</c>, <c>EBP</c> et l'adresse de retour, et — plus trompeur — il a ecrit les
    /// <b>cinq</b> sorties sur le meme <c>*param_2</c>. A le lire ainsi on obtient une fonction qui
    /// ecrase quatre fois sa propre sortie. Le desassemblage tranche : ce sont cinq pointeurs
    /// distincts, et l'arithmetique de pile les separe sans ambiguite
    /// (<c>Legacy/Raw/Blaze/FUN_00535F20.asm</c>).
    /// </para>
    /// </summary>
    /// <param name="boldness">Sortie : de 0 a 0x40. Part de l'ecart entre le niveau du poursuivi
    /// (<c>bloc par personnage + 0x146</c>) et celui de l'entite (<c>stat + 0x44</c>), corrige par
    /// le rapport <c>stat[0x4c] / stat[0x48]</c>, borne aux deux bouts. Le trait <c>0x200</c> la
    /// force a 0x40 avant correction. C'est l'echelle sur laquelle toute la suite lit ses seuils,
    /// et son complement a 0x40 sert de mesure inverse.</param>
    /// <param name="distance">Sortie : la distance a la cible, <b>en trois dimensions</b>.</param>
    /// <param name="heading">Sortie : le cap vers la cible <b>relatif</b> a l'orientation de
    /// l'entite, sur douze bits. Le trait <c>8</c> le remplace par un tirage.</param>
    /// <param name="speed">Sortie : l'echelle de deplacement, <c>0x1000</c> par defaut. Cinq
    /// conditions la modifient et trois la mettent a zero — <b>et zero veut dire « ne pense
    /// pas »</b> : l'appelant renonce a toute la decision.</param>
    /// <param name="traits">Sortie : <c>stat + 0x40</c>, le mot de comportement de l'espece.</param>
    private static void ReadSceneEntitySituation_00535F20(
        byte* entity, int* boldness, int* distance, int* heading, int* speed, uint* traits)
    {
        byte* mainMem = GetMainMemPtr();
        byte* stats = s_EntityStatBlocks_005ea560 + entity[8] * 0x120;
        byte* chased = *(byte**)(entity + 0x6c);

        *traits = *(uint*)(stats + 0x40);

        byte* perChar = mainMem + (chased[8] + 0x78) * 0x2000;
        *boldness = *(ushort*)(perChar + 0x146) - *(ushort*)(stats + 0x44);

        if ((*traits & 0x200) != 0) *boldness = 0x40;

        *boldness += 0x20 - (*(short*)(stats + 0x4c) * 0x20) / *(short*)(stats + 0x48);
        if (*boldness < 0) *boldness = 0;
        else if (*boldness > 0x40) *boldness = 0x40;

        int dx = (*(int*)(chased + 0x1c) - *(int*)(entity + 0x1c)) >> 0xc;
        int dy = (*(int*)(chased + 0x20) - *(int*)(entity + 0x20)) >> 0xc;
        int dz = (*(int*)(chased + 0x24) - *(int*)(entity + 0x24)) >> 0xc;

        *heading = (Winsys3d.Ordinal_457(dx, dz) - *(ushort*)(entity + 0x32)) & 0xfff;
        *distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

        if ((*traits & 8) != 0) *heading = CrtRand() & 0xfff;

        *speed = 0x1000;
        if ((*traits & 0x4000) != 0) *speed = 0x1333;
        if ((*traits & 0x8000) != 0) *speed = 0xccc;
        if ((*traits & 4) != 0) *speed = 0x800;
        if ((*traits & 0x1c0) != 0) *speed = 0;
        if ((*(uint*)entity & 0x80) != 0) *speed = 0;

        // La derniere condition lit le bloc de statistiques, pas les traits : une entite « tenue »
        // (+0x74 non nul) ne bouge plus, sauf si son espece porte le bit de tete de stat[0x14].
        if ((*(uint*)(stats + 0x14) & 0x80000000) == 0 && *(byte**)(entity + 0x74) != null)
        {
            *speed = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00488160 @ 0x00488160 — <b>l'integration du mouvement</b> d'une entite de scene,
    /// appelee sans condition a la fin de chaque decision.
    ///
    /// <para>
    /// <b>Meme piege de signature que sa voisine</b> : <c>Blaze.c</c> la declare a un parametre puis
    /// lit <c>unaff_EBP</c> et <c>unaff_retaddr</c>. Le desassemblage donne deux parametres —
    /// l'entite et l'echelle de vitesse — et <c>unaff_EBP</c> n'est que le sinus dont Ghidra a jete
    /// la valeur de retour, exactement comme il le fait dans <c>FUN_0049a8e0</c>, sa jumelle.
    /// </para>
    /// <para>
    /// Les trois composantes de vitesse (<c>+0x50</c>, <c>+0x54</c>, <c>+0x58</c>) sont amorties
    /// d'un huitieme et d'un deux-cent-cinquante-sixieme par image, puis ajoutees a la position ;
    /// la poussee dirigee de <c>+0x5c</c> s'ajoute a X et Z par le sinus et le cosinus du cap. La
    /// composante verticale porte en plus le rappel de <c>+0x9a</c>, le flottement propre a
    /// l'espece.
    /// </para>
    /// </summary>
    private static void IntegrateSceneEntityMotion_00488160(byte* entity, int speed)
    {
        int course = *(short*)(entity + 0x32);
        int sin = (int)Winsys3d.GetSinTableEntry(course);   // Ghidra: Ordinal_451
        int cos = (int)Winsys3d.GetCosTableEntry(course);   // Ghidra: Ordinal_450

        int vx = *(int*)(entity + 0x50);
        int vy = *(int*)(entity + 0x54);
        int vz = *(int*)(entity + 0x58);
        int bob = *(short*)(entity + 0x9a);
        int thrust = (*(int*)(entity + 0x5c) * speed) >> 0xc;

        int nvx = vx - (vx >> 3) - (vx >> 8);
        int nvy = (bob >> 8) - (vy >> 8) - bob + vy;
        int nvz = vz - (vz >> 8) - (vz >> 3);

        *(int*)(entity + 0x50) = nvx;
        *(int*)(entity + 0x54) = nvy;
        *(int*)(entity + 0x58) = nvz;

        *(int*)(entity + 0x1c) += ((thrust * sin) >> 0xc) + nvx;
        *(int*)(entity + 0x20) += nvy;
        *(int*)(entity + 0x24) += ((thrust * cos) >> 0xc) + nvz;
    }

    /// <summary>
    /// Ghidra: FUN_004dfaf0 @ 0x004dfaf0 — <b>la fin de geste</b> : quand le compteur de
    /// <c>+0x20</c> a rattrape la borne de <c>+0x64</c>, le bit 0 du mot d'etat tombe et l'entite
    /// cesse d'etre « en cours d'animation ». Sinon il est repose.
    /// </summary>
    private static void EndSceneEntityAnimation_004DFAF0(uint* entity)
    {
        if ((int)entity[8] <= (int)entity[0x19] && (entity[0x24] & 0xa000) == 0)
        {
            entity[8] = entity[0x19];
            uint flags = *entity;

            if ((flags & 1) != 0 && (flags & 0x816) == 0)
            {
                ((byte*)entity)[0x43] = 0;
            }

            entity[0x15] = 0;
            *entity = flags & 0xfffffffe;
            return;
        }

        if ((*entity & 1) == 0) *entity |= 1;
    }

    /// <summary>
    /// Ghidra: FUN_004e3770 @ 0x004e3770 — <b>l'interruption</b> : ce qui remet une entite de scene
    /// a l'attente quand quelque chose lui arrive. Deux causes, et la seconde est le clignotement de
    /// degat (<c>+0x96</c>) que <see cref="TickEntityTable_005ecaa0"/> decompte.
    ///
    /// <para>Elle prend le <b>bloc de statistiques</b> en second parametre — celui que
    /// <see cref="ResolveEntityBlocks"/> a resolu au tout debut du gestionnaire, et le seul de ses
    /// trois resultats qui serve.</para>
    /// </summary>
    private static void InterruptSceneEntity_004E3770(uint* entity, byte* stats)
    {
        byte* e = (byte*)entity;

        if ((entity[0x24] & 0x2000a882) != 0)
        {
            if ((*entity & 0x817) == 0)
            {
                e[0x42] = 1;
                e[0x43] = 1;
            }

            entity[0x17] = 0;
        }

        if (*(short*)(e + 0x96) != 0
            && (*(uint*)(stats + 0x14) & 0x80000000) == 0
            && (*entity & 0x817) == 0)
        {
            if ((*entity & 0x40) != 0)
            {
                e[0x43] = 0;
                return;
            }

            e[0x42] = 0;
            e[0x43] = 1;
            entity[0x17] = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00538af0 @ 0x00538af0 — <b>rebase un bloc de statistiques sur son modele</b>, en
    /// appliquant le bonus de difficulte. Les huit resistances elementaires de <c>modele + 0x28</c>
    /// partent elargies en <c>ushort</c> dans <c>stat + 0x110</c> ; les huit <c>short</c> de
    /// <c>modele + 0x30</c> sont mis a l'echelle de <c>100 + bonus</c> pour cent.
    ///
    /// <para>Le bonus ne s'applique que si <c>stat[0x14]</c> porte son bit de tete ; sinon
    /// l'echelle vaut 100 pour cent, c'est-a-dire rien. Le <c>&gt;&gt; 0x3f</c> et le
    /// <c>&gt;&gt; 0x1f</c> que Ghidra ajoute autour de la division sont la correction de signe de
    /// l'idiome de division par 100 : le net est une division tronquee.</para>
    /// </summary>
    private static void RebaseStatBlockOnModel_00538AF0(byte* entity)
    {
        ResolveEntityBlocks(entity, out byte* stats, out _, out byte* model);

        uint bonus;
        int percent;

        if ((*(uint*)(stats + 0x14) & 0x80000000) == 0)
        {
            bonus = 0;
            percent = 100;
        }
        else
        {
            bonus = s_DifficultyBonus_0056d2c0[s_Unknown_005de034];
            percent = (int)bonus + 100;
        }

        for (int i = 0; i < 8; i++)
        {
            *(ushort*)(stats + 0x110 + i * 2) = model[0x28 + i];
        }

        for (int i = 0; i < 8; i++)
        {
            int scaled = *(short*)(model + 0x30 + i * 2) * percent;
            *(short*)(stats + 0x30 + i * 2) = (short)(scaled / 100);
        }

        *(ushort*)(stats + 0x2c) = *(ushort*)(model + 0x18);
        *(short*)(stats + 0x2e) = (short)(*(short*)(model + 0x1a) + (short)((int)bonus >> 3));
    }

    /// <summary>
    /// Ghidra: FUN_00538c10 @ 0x00538c10 — <b>l'empreinte au sol d'une entite</b> : les deux
    /// demi-etendues de sa boite de collision, prises dans une table ou dans l'autre selon qu'elle
    /// est un membre du groupe (bit <c>0x800000</c>) ou une entite de scene.
    ///
    /// <para>
    /// <b>Le meme enregistrement de huit octets sert des deux cotes</b>, et c'est ce qui a resolu
    /// le <c>TODO(REVERSE)</c> pose sur <see cref="s_ClassBodyMetrics_0056d440"/> : <c>+0</c> et
    /// <c>+4</c> sont les demi-etendues, <c>+2</c> la hauteur de la bulle d'etat que
    /// <see cref="StatusBubbleHeight_00538BC0"/> lit deja. Les deux lectures se confirment l'une
    /// l'autre — celle-ci prend <c>stat + 0x58</c> pour une entite de scene, celle-la
    /// <c>stat + 0x5a</c> pour la hauteur, soit le meme bloc a deux octets pres.
    /// </para>
    /// </summary>
    private static byte* EntityFootprint_00538C10(uint* entity)
    {
        if ((*entity & 0x800000) != 0)
        {
            byte characterClass = GetMainMemPtr()[(byte)entity[2] * 0x2000 + 0xf0150];
            return s_ClassBodyMetrics_0056d440 + characterClass * 8;
        }

        return s_EntityStatBlocks_005ea560 + (byte)entity[2] * 0x120 + 0x58;
    }

    /// <summary>Le format de point que <see cref="PolygonOverlapMask_005358F0"/> attend : deux
    /// coordonnees au sol empaquetees dans un dword, X en bas et Z en haut. C'est la disposition
    /// d'un registre <c>SXY</c> du GTE, ou ces points transitent.</summary>
    private static uint PackGroundPoint(int x, int z) => ((uint)z << 0x10) | ((uint)x & 0xffff);

    /// <summary>
    /// Ghidra: un des huit appels du trace de mise au point de <c>FUN_00535a90</c> — le point
    /// empaquete redevient une position en virgule fixe (<c>movsx ecx,ax / shl ecx,0Ch</c> pour X,
    /// <c>sar eax,4 / and eax,0FFFFF000h</c> pour Z), puis un eclat de sous-type 0 y nait.
    /// </summary>
    private static void PlaceReachMarker(byte* entity, int* marker, uint packed, int frame)
    {
        marker[0] = (short)packed << 0xc;
        marker[2] = ((int)packed >> 4) & unchecked((int)0xfffff000);
        SpawnStatusEffectEntity_0047AC40(entity, marker, s_ReachMarkerAtlas_0056aca0, 0, 1, frame);
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056aca0</c> — le descripteur que le trace de mise au point confie a ses
    /// eclats. <b>Un seul enregistrement est releve, et c'est une borne demontree</b> : l'eclat nait
    /// avec <c>+0x40</c> a zero, et son gestionnaire (<see cref="EntityHandler_00482FA0"/>) remplace
    /// <c>+0x3c</c> par son propre atlas des sa premiere image. Au plus l'enregistrement 0 est donc lu,
    /// par un dessin qui tomberait entre la naissance et ce premier passage. Les octets suivants
    /// (<c>00 08 01 08 02 08 03 08</c>…) n'ont pas la forme d'un descripteur.
    /// </summary>
    private static readonly byte* s_ReachMarkerAtlas_0056aca0 = AllocBytes([
        0x18, 0x00, 0x18, 0x18, 0x01, 0x03, 0x10, 0x01,
    ]);

    /// <summary>
    /// Ghidra: le bandeau de mise au point commun a <c>FUN_00539b80</c> et <c>FUN_0053a2b0</c>, allume
    /// par la ligne MONITOR du menu de mise au point : le detail du calcul d'un coup, recopie dans
    /// l'arene a <c>0x005e90c0</c>, puis lie au panneau de la carte du monde
    /// (<c>&amp;DAT_005633b0</c>) avec le flux de texte <c>+0x5f0</c> de la table <c>+0x70</c> du
    /// repertoire d'etat. C'est ce flux qui relit les valeurs par ses <c>$</c>-echappements.
    ///
    /// <code>
    /// +0x00..+0x0e  les huit shorts +0x30..+0x3e du bloc de l'assaillant
    /// +0x10..+0x1e  les huit du bloc de la victime
    /// +0x20 +0x21   les octets +8 des deux entites (leur numero d'emplacement)
    /// +0x22         l'element               +0x23  la chance de critique (melee) ou 0 (sort)
    /// +0x24 +0x26   l'attaque et la defense, sur seize bits
    /// +0x28         les degats
    /// </code>
    /// </summary>
    private static void PostCombatDebugBanner(
        uint* attacker, uint* victim, byte* attackerBlock, byte* victimBlock,
        byte element, byte extra, int attack, int defence, int damage)
    {
        byte* banner = Mem.Arena(0x005e90c0);

        for (int i = 0; i < 8; i++)
        {
            *(short*)(banner + i * 2) = *(short*)(attackerBlock + 0x30 + i * 2);
            *(short*)(banner + 0x10 + i * 2) = *(short*)(victimBlock + 0x30 + i * 2);
        }

        banner[0x20] = (byte)attacker[2];
        banner[0x21] = (byte)victim[2];
        banner[0x22] = element;
        banner[0x23] = extra;
        *(short*)(banner + 0x24) = (short)attack;
        *(short*)(banner + 0x26) = (short)defence;
        *(short*)(banner + 0x28) = (short)damage;

        byte* labels = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x5f0);
        BindMenuLabels(labels, (byte*)attacker, s_WorldMapDescriptor_005633b0, 0);   // Ghidra: FUN_00544350
    }

    /// <summary>
    /// Ghidra: FUN_005358f0 @ 0x005358f0 — <b>de quel cote de chaque arete tombe chaque sommet</b> :
    /// seize produits en croix, un par couple (arete de <paramref name="edges"/>, sommet de
    /// <paramref name="corners"/>), dont seuls les <b>signes</b> sont gardes, empiles dans un mot
    /// de seize bits.
    ///
    /// <para>
    /// <b>Ghidra la declare a un parametre ; elle en prend deux.</b> Le second quadrilatere devient
    /// chez lui un <c>local_24[4]</c> jamais initialise — c'est la meme perte de signature que
    /// <c>FUN_00535f20</c>, et le site d'appel la trahit (<c>push</c>, <c>push</c>,
    /// <c>add esp,8</c>).
    /// </para>
    /// <para>
    /// <b>Le produit en croix passe par le GTE.</b> Les trois points vont dans <c>SXY0</c>,
    /// <c>SXY1</c>, <c>SXY2</c> et le resultat sort de <c>MAC0</c> — c'est <c>NCLIP</c>, l'operation
    /// d'elimination des faces arriere, detournee ici en test de cote. Le portage l'ecrit en x86 et
    /// range quand meme le resultat dans le registre : <see cref="NormalClip"/> reproduit les deux.
    /// </para>
    /// <para>
    /// <b>Le decalage est en retard d'un tour</b>, et ce n'est pas une maladresse : c'est la
    /// latence du coprocesseur d'origine. Chaque tour empile le signe du produit <i>precedent</i>,
    /// et un dernier decalage apres les boucles rattrape le seizieme. Le bit 16 qui en resulte est
    /// le zero initial, toujours nul, donc le mot rendu tient bien sur seize bits.
    /// </para>
    /// </summary>
    private static uint PolygonOverlapMask_005358F0(uint* edges, uint* corners)
    {
        EnsureGteRegisters();

        // Le quatrieme sommet est recopie en tete pour fermer le contour : les quatre aretes se
        // lisent alors comme quatre couples consecutifs.
        uint* loop = stackalloc uint[5];
        loop[0] = edges[0];
        loop[1] = edges[1];
        loop[2] = edges[2];
        loop[3] = edges[3];
        loop[4] = edges[0];

        uint* tested = stackalloc uint[4];
        tested[0] = corners[0];
        tested[1] = corners[1];
        tested[2] = corners[2];
        tested[3] = corners[3];

        uint mask = 0;
        int previous = 0;

        for (int edge = 0; edge < 4; edge++)
        {
            *(uint*)s_GteSxy806 = loop[edge];
            *(uint*)s_GteSxy807 = loop[edge + 1];

            for (int corner = 0; corner < 4; corner++)
            {
                *(uint*)s_GteSxy805 = tested[corner];
                NormalClip();

                mask = mask * 2 | ((uint)previous >> 0x1f);
                previous = ReadMac0();
            }
        }

        return mask * 2 | ((uint)previous >> 0x1f);
    }

    /// <summary>
    /// Ghidra: FUN_00535a40 @ 0x00535a40 — <b>ce que le mot de seize signes veut dire</b>. Trois
    /// reponses, et <see cref="TestMeleeReach_00535A90"/> additionne celles des deux sens :
    ///
    /// <list type="bullet">
    /// <item><c>0xffff</c> — les seize sommets sont du meme cote de toutes les aretes : recouvrement
    /// franc, <b>1</b>.</item>
    /// <item>un quartet entierement nul — un sommet du polygone teste est en dehors d'<b>une</b>
    /// arete, donc separe : <b>-1</b>, et ce -1 annule le +1 de l'autre sens.</item>
    /// <item>sinon, le compte de bits : plus de neuf sur seize vaut <b>1</b>, le reste <b>0</b>.
    /// C'est le cas des recouvrements partiels, tranche a la majorite.</item>
    /// </list>
    /// </summary>
    private static int ClassifyOverlapMask_00535A40(uint mask)
    {
        if (mask == 0xffff) return 1;

        uint quartet = 0xf;
        for (int i = 4; i != 0; i--)
        {
            if ((mask & quartet) == 0) return -1;
            quartet <<= 4;
        }

        int set = 0;
        uint bit = 1;
        for (int i = 0x10; i != 0; i--)
        {
            if ((mask & bit) != 0) set++;
            bit <<= 1;
        }

        return 9 < set ? 1 : 0;
    }

    /// <summary>
    /// Ghidra: FUN_00535a90 @ 0x00535a90 — <b>le test de portee</b> : est-ce que le geste de
    /// l'entite touche ce membre du groupe ? 1158 octets, transcrite au desassembleur
    /// (<c>Legacy/Raw/Blaze/FUN_00535A90.asm</c>).
    ///
    /// <para>
    /// <b>Elle est dans <c>Blaze.c</c> et elle y est inexploitable.</b> Ghidra y a perdu <c>EBX</c>,
    /// <c>ESI</c>, <c>EDI</c> et les arguments des quatre appels a <c>Ordinal_450</c>/<c>451</c>, il
    /// la declare a trois parametres quand <c>FUN_00519c30</c> lui en passe quatre, et il rend une
    /// des deux figures comme un tableau local jamais rempli. Ce qu'il en reste ne se lit pas.
    /// </para>
    ///
    /// <para>
    /// <b>La fonction croise deux figures a plat</b>, apres un rejet en hauteur qui, lui, est bien
    /// rendu : si l'ecart vertical entre le membre et l'entite depasse <c>sonde[3]</c> plus la
    /// demi-hauteur du membre, rien ne peut toucher — c'est le seul endroit ou la troisieme
    /// dimension compte.
    /// </para>
    /// <list type="number">
    /// <item><b>Le quadrilatere du geste</b>, construit sur la sonde : cap de l'entite decale de
    /// <c>sonde[0]</c>, avance de <c>sonde[1]</c>, largeur <c>sonde[2]</c> de part et d'autre, puis
    /// une profondeur <c>sonde[5]</c> prise sur deux caps ouverts de <c>±sonde[4]</c>. Avec la
    /// sonde de <see cref="TickSceneEntityAi_0049A980"/> (<c>0, 0x1e, 0x32, 0x64, 0, 0x50</c>) cela
    /// donne un simple rectangle de 0x64 sur 0x50, pose 0x1e devant la bete.</item>
    /// <item><b>Le rectangle du membre</b>, construit sur son empreinte
    /// (<see cref="EntityFootprint_00538C10"/>) tournee de son cap. Noter le
    /// <c>-(largeur * sinus) &gt;&gt; 12</c> : la negation precede le decalage, ce qui n'est pas la
    /// meme chose que nier le decalage.</item>
    /// <item><b>Le croisement</b>, dans les <b>deux sens</b> — chaque figure jugee contre les aretes
    /// de l'autre. C'est ce qui rattrape le cas ou l'une contient l'autre sans qu'aucun sommet ne
    /// franchisse d'arete.</item>
    /// </list>
    /// </summary>
    private static bool TestMeleeReach_00535A90(int* position, byte* member, short* probe, byte* entity)
    {
        // ---------------------------------------------------------------- le rejet en hauteur
        int half = StatusBubbleHeight_00538BC0((uint*)member) >> 1;
        int gap = ((*(int*)(member + 0x20) - position[1]) >> 0xc) + half;
        if (gap < 0) gap = -gap;
        if (gap > probe[3] + half) return false;

        // ---------------------------------------------------------------- la figure du geste
        int course = *(short*)((byte*)position + 0x16) + probe[0];
        int sin = (int)Winsys3d.GetSinTableEntry(course);   // Ghidra: Ordinal_451
        int cos = (int)Winsys3d.GetCosTableEntry(course);   // Ghidra: Ordinal_450

        // Le centre de la face avant, encore en virgule fixe : la position de l'entite avancee de
        // sonde[1]. Le decalage de douze bits n'intervient qu'apres l'ecart lateral.
        int frontX = probe[1] * sin + position[0];
        int frontZ = probe[1] * cos + position[2];

        int nearRightX = (frontX + probe[2] * cos) >> 0xc;
        int nearLeftX = (frontX - probe[2] * cos) >> 0xc;
        int nearRightZ = (frontZ - probe[2] * sin) >> 0xc;
        int nearLeftZ = (frontZ + probe[2] * sin) >> 0xc;

        int farLeftX = nearLeftX + ((int)Winsys3d.GetSinTableEntry(course - probe[4]) * probe[5] >> 0xc);
        int farLeftZ = nearLeftZ + ((int)Winsys3d.GetCosTableEntry(course - probe[4]) * probe[5] >> 0xc);
        int farRightX = nearRightX + ((int)Winsys3d.GetSinTableEntry(course + probe[4]) * probe[5] >> 0xc);
        int farRightZ = nearRightZ + ((int)Winsys3d.GetCosTableEntry(course + probe[4]) * probe[5] >> 0xc);

        uint* swing = stackalloc uint[4];
        swing[0] = PackGroundPoint(nearRightX, nearRightZ);
        swing[1] = PackGroundPoint(nearLeftX, nearLeftZ);
        swing[2] = PackGroundPoint(farLeftX, farLeftZ);
        swing[3] = PackGroundPoint(farRightX, farRightZ);

        // ---------------------------------------------------------------- la figure du membre
        int memberCourse = *(short*)(member + 0x32);
        int memberSin = (int)Winsys3d.GetSinTableEntry(memberCourse);
        int memberCos = (int)Winsys3d.GetCosTableEntry(memberCourse);

        int memberX = *(int*)(member + 0x1c) >> 0xc;
        int memberZ = *(int*)(member + 0x24) >> 0xc;

        byte* footprint = EntityFootprint_00538C10((uint*)member);
        int across = *(short*)footprint;
        int along = *(short*)(footprint + 4);

        int acrossX = (across * memberCos) >> 0xc;
        int acrossZ = (-(across * memberSin)) >> 0xc;
        int alongX = (along * memberSin) >> 0xc;
        int alongZ = (along * memberCos) >> 0xc;

        int cornerX = memberX + (acrossX >> 1) + (alongX >> 1);
        int cornerZ = memberZ + (acrossZ >> 1) + (alongZ >> 1);

        uint* body = stackalloc uint[4];
        body[0] = PackGroundPoint(cornerX, cornerZ);
        body[1] = PackGroundPoint(cornerX - alongX, cornerZ - alongZ);
        body[2] = PackGroundPoint(cornerX - alongX - acrossX, cornerZ - alongZ - acrossZ);
        body[3] = PackGroundPoint(cornerX - acrossX, cornerZ - acrossZ);

        // ---------------------------------------------------------------- le trace de mise au point
        // Ghidra: 0x00535d1d..0x00535ec8, allume par la ligne MONITOR du menu de mise au point. Les
        // huit sommets des deux figures deviennent chacun un eclat dans la table 0x005f2d40 :
        // `FUN_0047ac40(entite, &point, &DAT_0056aca0, 0, 1, 4..7)`. Le sous-type est 0 et c'est
        // le SIXIEME argument, ecrit en +0x0a, qui va de 4 a 7 — le releve precedent de ce trou
        // les confondait. La hauteur est celle de l'entite pour le geste, celle du membre pour son
        // rectangle ; X et Z sortent du point empaquete.
        if (Mem.s_DebugModeLatch_005633a8 == 0 && s_DebugOverlayEnabled_005de020 != 0)
        {
            int* marker = stackalloc int[3];

            marker[1] = position[1];
            for (int corner = 0; corner < 4; corner++)
            {
                PlaceReachMarker(entity, marker, swing[corner], corner + 4);
            }

            marker[1] = *(int*)(member + 0x20);
            for (int corner = 0; corner < 4; corner++)
            {
                PlaceReachMarker(entity, marker, body[corner], corner + 4);
            }
        }

        // ---------------------------------------------------------------- le croisement
        int forward = ClassifyOverlapMask_00535A40(PolygonOverlapMask_005358F0(swing, body));
        int backward = ClassifyOverlapMask_00535A40(PolygonOverlapMask_005358F0(body, swing));

        return forward + backward > 0;
    }

    /// <summary>Ghidra: &amp;DAT_00564808 — le <b>descripteur d'atlas</b> que
    /// <see cref="ValidateCombatants_0053A860"/> passe a <c>FUN_0047ac40</c> quand un coup arrive
    /// avec un assaillant invalide. Huit octets, releves verbatim dans <c>.data</c> ; les huit
    /// suivants (<c>d0 28 28 08 01 00 30 02</c>) forment un second descripteur de meme forme, non
    /// designe par ce site.</summary>
    private static readonly byte* s_ErrorMarkerAtlas_00564808 = AllocBytes([
        0xa0, 0x28, 0x24, 0x08, 0x01, 0x00, 0x30, 0x02,
    ]);

    /// <summary>
    /// Ghidra: FUN_0053a860 @ 0x0053a860 — <b>le controle d'identite des deux combattants</b>, et
    /// la premiere chose que fait la resolution du coup : elle ne rend zero que si les deux sont
    /// vivants et rangés dans la table qui correspond a leur nature.
    ///
    /// <para>
    /// Le mot rendu est un <b>diagnostic</b>, pas un booleen : quartet bas pour l'assaillant,
    /// quartet haut pour la victime, et dans chaque quartet <c>1</c> pour « pas alloue »,
    /// <c>2</c> pour « membre du groupe hors de sa table », <c>4</c> pour « entite de scene hors de
    /// la sienne », <c>8</c> pour « ni l'un ni l'autre ». L'appelant n'en lit que la nullite, mais
    /// la valeur reste lisible en debogage.
    /// </para>
    /// <para>
    /// <b>Les deux bornes sont des comparaisons de POINTEUR dans l'original</b> — <c>param_1</c>
    /// contre <c>&amp;DAT_005e93c0</c>..<c>&amp;DAT_005e9594</c> et <c>&amp;DAT_005ecaa0</c>..
    /// <c>&amp;DAT_005ede20</c>. Ici les deux tables sont deux allocations distinctes, donc la
    /// comparaison se fait contre leurs etendues reconstruites : quatre emplacements de 0x9c pour
    /// le groupe, trente-deux pour la scene. Les bornes hautes du binaire sont <b>inclusives d'un
    /// emplacement de trop</b> (elles designent le premier octet APRES la table) et c'est transcrit
    /// tel quel.
    /// </para>
    /// </summary>
    private static byte ValidateCombatants_0053A860(uint* attacker, uint* victim)
    {
        byte* partyBase = Inp.s_SlotFlags_005e93c0;          // 0x005e93c0, quatre fois 0x9c
        byte* sceneBase = Mem.Entity(0x005ecaa0);            // 0x005ecaa0, trente-deux fois 0x9c

        byte report = (byte)(((*attacker & 0x80000000) == 0) ? 1 : 0);

        if ((*attacker & 0x800000) == 0)
        {
            if ((*attacker & 0x400000) == 0) report |= 8;
            else if ((byte*)attacker < sceneBase || sceneBase + 0x1380 < (byte*)attacker) report |= 4;
        }
        else if ((byte*)attacker < partyBase || partyBase + 0x1d4 < (byte*)attacker)
        {
            report |= 2;
        }

        if ((*victim & 0x80000000) == 0) report |= 0x10;

        if ((*victim & 0x800000) == 0)
        {
            if ((*victim & 0x400000) == 0) report |= 0x80;
            else if ((byte*)victim < sceneBase || sceneBase + 0x1380 < (byte*)victim) report |= 0x40;
        }
        else if ((byte*)victim < partyBase || partyBase + 0x1d4 < (byte*)victim)
        {
            report |= 0x20;
        }

        // Assaillant douteux mais victime saine : une marque visible est posee sur la victime. Le
        // cas inverse ne dit rien — c'est la seule trace laissee d'une incoherence.
        if ((report & 0x0f) != 0 && (report & 0xf0) == 0)
        {
            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)((byte*)victim + 0x1c), s_ErrorMarkerAtlas_00564808, 0x29, 0, 0);
        }

        return report;
    }

    /// <summary>
    /// Ghidra: FUN_005398a0 @ 0x005398a0 — <b>le penchant elementaire</b> : l'element nomme en
    /// <c>+0x51</c> du bloc gagne la moitie de sa valeur, et son <b>oppose</b> en perd la moitie.
    ///
    /// <para>L'oppose se designe par <c>indice ^ 1</c>, ce qui dit que les huit elements sont
    /// ranges par <b>couples contraires</b> : feu et eau, terre et air, lumiere et tenebres, sacre
    /// et mal. C'est la meme lecture que <see cref="ResolveElementalExchange_005398E0"/>, qui
    /// annule les couples l'un contre l'autre avant de compter.</para>
    /// </summary>
    private static void TiltDominantElement_005398A0(byte* block)
    {
        int element = block[0x51];

        ushort* mine = (ushort*)(block + 0x110 + element * 2);
        *mine = (ushort)((*mine >> 1) + *mine);

        ushort* opposite = (ushort*)(block + 0x110 + (element ^ 1) * 2);
        *opposite = (ushort)(*opposite - (*opposite >> 1));
    }

    /// <summary>
    /// Ghidra: FUN_005398e0 @ 0x005398e0 — <b>l'echange elementaire</b> : les huit elements de
    /// l'assaillant contre les huit de la victime, et le nombre rendu s'ajoute aux degats.
    ///
    /// <para>
    /// <b>Elle commence par vider les couples contraires, dans les DEUX blocs.</b> Feu et eau
    /// s'annulent l'un l'autre jusqu'a ce qu'il n'en reste qu'un ; idem pour les trois autres
    /// couples. Un bloc qui portait 30 de feu et 10 d'eau en ressort avec 20 de feu et rien d'eau.
    /// C'est une <b>modification en place</b> du bloc de statistiques, pas un calcul de cote : la
    /// fonction consomme ce qu'elle lit.
    /// </para>
    /// <para>
    /// <b>Puis le compte, element par element</b> : si la victime porte le meme element, il est
    /// <b>soustrait</b> — elle y resiste ; si elle ne le porte pas, il est ajoute, mais reduit au
    /// <b>quart</b> lorsqu'elle ne porte pas non plus son contraire. Attaquer par le feu paye plein
    /// tarif contre qui porte de l'eau, et au quart contre qui n'a ni l'un ni l'autre.
    /// </para>
    /// </summary>
    private static int ResolveElementalExchange_005398E0(byte* attackerBlock, byte* victimBlock)
    {
        for (int pass = 0; pass < 2; pass++)
        {
            ushort* pair = (ushort*)((pass == 0 ? attackerBlock : victimBlock) + 0x110);

            for (int couple = 0; couple < 4; couple++, pair += 2)
            {
                ushort first = pair[0];
                ushort second = pair[1];

                if (first < second)
                {
                    second = (ushort)(second - first);
                    first = 0;
                }
                else
                {
                    first = (ushort)(first - second);
                    second = 0;
                }

                pair[0] = first;
                pair[1] = second;
            }
        }

        // Le miroir de mise au point : les deux series de huit partent dans l'arene, ou l'ecran
        // « Gurkha » les relit. Le verrou vaut 1 au chargement, donc ce bloc dort en jeu normal.
        if (Mem.s_DebugModeLatch_005633a8 == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                *(ushort*)(Mem.Arena(0x005e90a0) + i * 2) = *(ushort*)(attackerBlock + 0x110 + i * 2);
                *(ushort*)(Mem.Arena(0x005e90b0) + i * 2) = *(ushort*)(victimBlock + 0x110 + i * 2);
            }
        }

        int score = 0;

        for (int i = 0; i < 8; i++)
        {
            int attack = *(ushort*)(attackerBlock + 0x110 + i * 2);
            if (attack == 0) continue;

            int resisted = *(ushort*)(victimBlock + 0x110 + i * 2);

            if (resisted == 0)
            {
                if (*(short*)(victimBlock + 0x110 + (i ^ 1) * 2) == 0) attack >>= 2;
                score += attack;
            }
            else
            {
                score -= resisted;
            }
        }

        return score;
    }

    /// <summary>
    /// Ghidra: FUN_005399e0 @ 0x005399e0 — <b>l'element dominant</b> d'un bloc : l'indice du plus
    /// fort des huit, <b>decale de deux</b>, ou zero si le bloc n'en porte aucun. Le decalage n'est
    /// pas cosmetique — la valeur sert de teinte au nombre de degats que
    /// <see cref="ApplyDamage_00539A20"/> fait apparaitre, et 0 et 1 y designent deja autre chose.
    ///
    /// <para>En cas d'egalite c'est le <b>premier</b> qui gagne : la comparaison est stricte.</para>
    /// </summary>
    private static int DominantElement_005399E0(byte* block)
    {
        ushort best = 0;
        int index = 0;

        for (int i = 0; i < 8; i++)
        {
            ushort value = *(ushort*)(block + 0x110 + i * 2);
            if (best < value)
            {
                index = i;
                best = value;
            }
        }

        return best != 0 ? index + 2 : index;
    }

    /// <summary>
    /// Ghidra: FUN_00539a20 @ 0x00539a20 — <b>l'application des degats</b> : le nombre calcule est
    /// retire des points de vie, affiche, et la mort constatee.
    ///
    /// <para>
    /// <b>Le nombre passe par le bloc de statistiques avant d'etre utilise.</b> Il est ecrit en
    /// <c>+0x100</c> et <c>+0x102</c>, l'evenement d'objet equipe part, <b>puis il est relu</b> —
    /// c'est ce qui permet a un objet de modifier les degats qu'on subit ou qu'on inflige. L'ordre
    /// est le mecanisme, pas un detour.
    /// </para>
    /// <para>
    /// <b>L'evenement ne part pas du meme cote selon la victime</b> : une victime membre du groupe
    /// declenche ses propres objets (evenement 10), une entite de scene declenche ceux de
    /// l'assaillant (evenement 0x14).
    /// </para>
    /// <para>
    /// <b>Deux champs de l'entite servent de boite aux lettres</b> : le nombre part en <c>+0x18</c>
    /// et l'element en <c>+0x1a</c> le temps de l'appel a <c>FUN_0047a9d0</c>, puis le dword
    /// d'origine est remis. Le sauvetage de <c>+0x0a</c> qui l'accompagne est un <b>aller-retour a
    /// vide</b> — rien ne l'ecrit entre les deux — et il est conserve tel quel.
    /// </para>
    /// <para>
    /// A la mort, une entite de scene qui n'est pas marquee <c>0x80000000</c> dans <c>+0x14</c>
    /// laisse un effet dont le sous-type depend de sa famille : <c>0x51</c> a <c>0x54</c>, les tests
    /// se recouvrant volontairement — le dernier qui repond gagne.
    /// </para>
    /// </summary>
    /// <param name="attackerBlock">Jamais lu par le corps. L'appelant le pousse quand meme ; garde
    /// pour que le site d'appel reste lisible contre l'original.</param>
    /// <returns>1 si la victime en meurt, 0 sinon.</returns>
    private static int ApplyDamage_00539A20(
        uint* attacker, uint* victim, byte* attackerBlock, byte* victimBlock, short damage, byte element)
    {
        damage = LegacyPlus.Coutures.DegatsSubis(attacker, victim, damage);   // Couture legacy ++ (LP-001)
        *(short*)(victimBlock + 0x96) = 0;
        *(uint*)(victimBlock + 0x40) &= 0xffffffbf;
        *(short*)(victimBlock + 0x100) = damage;
        victimBlock[0x102] = element;

        *(uint*)Mem.Arena(0x005e99c0) = 0;

        if ((*victim & 0x800000) == 0) TriggerEquippedItems_005238E0(attacker, 0x14);
        else TriggerEquippedItems_005238E0(victim, 10);

        // La relecture : l'evenement ci-dessus a pu changer les deux.
        byte shown = victimBlock[0x102];
        short dealt = *(short*)(victimBlock + 0x100);

        byte* v = (byte*)victim;
        SpawnParticle_0050F070((uint*)(v + 0x1c), (uint)(ushort)dealt, shown);

        uint savedState = *(uint*)(v + 0x18);
        *(short*)(v + 0x18) = dealt;
        ushort savedTint = *(ushort*)(v + 0x0a);
        *(ushort*)(v + 0x1a) = shown;

        *(short*)(victimBlock + 0x4c) -= dealt;

        if (*(short*)(victimBlock + 0x4c) < 1)
        {
            *(short*)(victimBlock + 0x4c) = 0;

            if ((*victim & 0x40000000) == 0)
            {
                *victim |= 0x40000000;
                *(uint*)(victimBlock + 0x40) |= 0x40000000;

                uint family = *(uint*)(victimBlock + 0x14);

                if ((*victim & 0x800000) == 0 && (family & 0x80000000) == 0)
                {
                    int leaving = 0;
                    if ((family & 0x19000) != 0) leaving = 0x51;
                    if ((family & 0xb600000) != 0) leaving = 0x52;
                    if ((family & 0x80000) != 0) leaving = 0x53;
                    if ((family & 0x4966800) != 0) leaving = 0x54;

                    if (leaving != 0) SpawnEffectEntity(v, (byte)leaving, 0, 0);
                }
            }

            *(ushort*)(v + 0x0a) = savedTint;
            *(uint*)(v + 0x18) = savedState;
            return 1;
        }

        SpawnEffectEntity(v, 0x50, 0, 0);
        *(uint*)(v + 0x18) = savedState;
        *(ushort*)(v + 0x0a) = savedTint;
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_00539b80 @ 0x00539b80 — <b>la resolution du coup</b> : ce qui se passe entre
    /// l'instant ou <see cref="TestMeleeReach_00535A90"/> a dit « touche » et l'instant ou la
    /// victime a perdu ses points de vie. C'est le cœur du combat.
    ///
    /// <para>
    /// <b>Elle recalcule les deux fiches avant de frapper</b>, et pas par prudence : les cinq passes
    /// de statistiques (<c>FUN_00538ab0</c>..<c>FUN_00538fa0</c>) reconstruisent equipement, bonus
    /// et derivees a partir de zero, si bien qu'un changement d'arme entre deux coups est pris en
    /// compte au coup suivant sans que rien n'ait a le notifier. Les deux chemins different : un
    /// membre du groupe repasse par les cinq, une entite de scene par trois seulement.
    /// </para>
    /// <para>
    /// <b>Les trois octets <c>+0x2b0</c>, <c>+0x2b1</c> et <c>+0x2b2</c> designent des lignes de
    /// 0x280 octets</b> dans le bloc du personnage, dont huit <c>char</c> signes a <c>+0x318</c>
    /// s'ajoutent aux huit elements de <c>+0x210</c>. Le premier vaut pour l'assaillant, le second
    /// pour la victime, et le <b>troisieme n'est lu que si la victime est debout</b> (posture 0 ou
    /// 6) — c'est la difference entre parer avec son arme et encaisser.
    /// </para>
    /// <para>
    /// <b>Le calcul des degats, dans l'ordre :</b> attaque et defense sont chacune bruitees d'un
    /// quart de leur ecart propre (<c>+0x2c</c>) puis bornees a zero ; un coup critique
    /// (<c>+0x2e</c> pour cent, plus cinq si l'assaillant est sous cinq pour cent de vie)
    /// <b>divise la defense par deux</b> et fait naitre l'effet <c>0xbe</c> ; l'echange elementaire
    /// s'ajoute a la difference ; un <b>type d'arme inadapte</b> (aucun bit commun entre la ligne
    /// d'arme et <c>+0x14</c> de la victime, sur le masque <c>0x6099500</c>) divise le tout
    /// <b>par quatre</b> ; le reste est borne a zero, augmente d'un sixieme de l'attaque moins un
    /// trente-deuxieme de la defense, et borne a <b>un</b>. Personne ne fait jamais zero.
    /// </para>
    /// <para>
    /// <b>Ce qui suit n'arrive que si l'assaillant est un membre du groupe</b> : le son (choisi sur
    /// la classe, six variantes, decalees par le resultat du coup), l'experience — l'ecart entre le
    /// niveau de la victime et celui de l'assaillant, au trente-deuxieme, plus un —, le compte de
    /// victimes, l'effet elementaire visible, et le bandeau de nom.
    /// </para>
    /// <para>
    /// <b>Une entite de scene qui survit garde qui l'a frappee</b> en <c>+0x09</c>, et si son octet
    /// <c>+0x0d</c> designe le genre 10, elle <b>lance son script</b> : c'est ainsi qu'un monstre
    /// reagit au fait d'etre attaque.
    /// </para>
    /// </summary>
    /// <returns>1 si le coup a ete refuse au controle d'identite, 0 sinon.</returns>
    private static int ResolveMeleeHit_00539B80(uint* attacker, uint* victim)
    {
        if (ValidateCombatants_0053A860(attacker, victim) != 0) return 1;

        *(byte**)((byte*)attacker + 0x70) = (byte*)victim;   // chacun retient l'autre
        *(byte**)((byte*)victim + 0x70) = (byte*)attacker;

        byte* attackerBlock = RefreshCombatantBlock(attacker, 0x2b0, false);
        byte* victimBlock = RefreshCombatantBlock(victim, 0x2b1, true);

        // Le bloc par personnage de l'ASSAILLANT, calcule sans egard a sa nature — le binaire le
        // fait aussi pour une entite de scene, et ne s'en sert alors jamais.
        byte* attackerCharacter = GetMainMemPtr() + ((byte)attacker[2] + 0x78) * 0x2000;

        if ((*attacker & 0x800000) != 0)
        {
            *(uint*)Mem.Arena(0x005e99c0) = 0;
            TriggerEquippedItems_005238E0(attacker, 2);
        }

        if ((*victim & 0x800000) != 0)
        {
            *(uint*)Mem.Arena(0x005e99c0) = 0;
            TriggerEquippedItems_005238E0(victim, 0xc);
        }

        // ---------------------------------------------------------------- attaque et defense
        short attackSpread = *(short*)(attackerBlock + 0x2c);
        int attack = *(short*)(attackerBlock + 0x30)
                     + ((CrtRand() % (attackSpread + 1) - (attackSpread >> 1)) >> 2);
        if (attack < 0) attack = 0;

        short defenceSpread = *(short*)(victimBlock + 0x2c);
        int defence = *(short*)(victimBlock + 0x32)
                      + ((CrtRand() % (defenceSpread + 1) - (defenceSpread >> 1)) >> 2);
        if (defence < 0) defence = 0;

        // ---------------------------------------------------------------- le coup critique
        int criticalChance = *(ushort*)(attackerBlock + 0x2e);

        if ((*attacker & 0x800000) != 0
            && *(short*)(attackerBlock + 0x4c) <= *(short*)(attackerBlock + 0x48) * 5 / 100)
        {
            criticalChance += 5;
        }

        sbyte outcome = (sbyte)(CrtRand() % 100 < criticalChance ? 1 : 0);

        if (outcome != 0)
        {
            defence >>= 1;
            SpawnEffectEntity((byte*)victim, 0xbe, 0, -1);
        }

        // ---------------------------------------------------------------- les degats
        int damage = ResolveElementalExchange_005398E0(attackerBlock, victimBlock) + (attack - defence);
        int element = DominantElement_005399E0(attackerBlock);

        if ((*attacker & 0x800000) != 0
            && (*victim & 0x400000) != 0
            && (*(uint*)(victimBlock + 0x14) & 0x6099500) != 0
            && attackerCharacter[0x2b0] != 0xff
            && (*(uint*)(attackerCharacter + attackerCharacter[0x2b0] * 0x280 + 0x314)
                & *(uint*)(victimBlock + 0x14) & 0x6099500) == 0
            && (*(uint*)(attackerBlock + 0x40) & 0x8000000) == 0)
        {
            // L'arme ne mord pas sur cette famille : le quart, et un troisieme son.
            damage >>= 2;
            outcome = 2;
        }

        if (damage < 0) damage = 0;
        damage += *(short*)(attackerBlock + 0x30) / 6 - (*(short*)(victimBlock + 0x32) >> 5);
        if (damage < 1) damage = 1;

        if (Mem.s_DebugModeLatch_005633a8 == 0 && s_DebugOverlayEnabled_005de020 != 0)
        {
            // 0x00539f6d..0x0053a07d. L'octet +0x23 est la CHANCE DE CRITIQUE ([esp+0x20]), et
            // non l'issue ([esp+0x14]), que le bandeau n'emporte pas.
            PostCombatDebugBanner(attacker, victim, attackerBlock, victimBlock,
                (byte)element, (byte)criticalChance, attack, defence, damage);
        }

        // ---------------------------------------------------------------- le coup porte
        int killed = ApplyDamage_00539A20(
            attacker, victim, attackerBlock, victimBlock, (short)damage, (byte)element);

        if (killed != 0 && (*attacker & 0x800000) != 0
            && *(int*)(attackerCharacter + 0x15c) != -1)
        {
            *(int*)(attackerCharacter + 0x15c) += 1;
        }

        // ---------------------------------------------------------------- ce qui n'est qu'au groupe
        if ((*attacker & 0x800000) != 0)
        {
            TriggerEquippedItems_005238E0(attacker, 3);

            int voice = (attackerBlock[0x50] & 7) switch
            {
                0 or 2 or 4 or 5 or 6 => outcome + 0x32,
                _ => outcome + 0x35,
            };

            Aud.PlayPositionalSound(voice, 0x400, 0x1200, (int*)((byte*)attacker + 0x1c));

            int gained = (*(ushort*)(victimBlock + 0x44) - *(ushort*)(attackerBlock + 0x46)) >> 5;
            if (gained < 0) gained = 0;
            *(int*)(attackerCharacter + 0x158) += gained + 1;

            CheckLevelUp_00539190((byte*)attacker, attackerCharacter);

            if (*(short*)(attackerBlock + 0x82) != 0)
            {
                // L'effet elementaire visible, pris sur l'element le plus fort de la VICTIME —
                // pas de l'assaillant, et pas celui qui a servi a teinter le nombre.
                ushort strongest = 0;
                uint which = 0;

                for (uint i = 0; i < 8; i++)
                {
                    ushort value = *(ushort*)(victimBlock + 0x110 + i * 2);
                    if (strongest < value)
                    {
                        which = i;
                        strongest = value;
                    }
                }

                if (strongest != 0)
                {
                    byte* atlas = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x20)
                                  + which * 0x18;

                    SpawnStatusEffectEntity_0047AC40(
                        (byte*)victim, (int*)((byte*)victim + 0x1c), atlas, 0x29, 0, 0);
                }
            }

            if (*(short*)(attackerBlock + 0x84) != 0 || Mem.s_DebugModeLatch_005633a8 == 0)
            {
                // Le bandeau : le nom de la victime, ses points de vie et son maximum. Les seize
                // octets du nom sont recopies tels quels, puis tout zero SUIVI d'un non-zero
                // devient une espace — c'est ainsi que le jeu rejoint deux mots ranges dans un
                // champ a trous.
                byte* banner = Mem.Entity(0x005fa5d0);
                ((uint*)banner)[0] = ((uint*)victimBlock)[0];
                ((uint*)banner)[1] = ((uint*)victimBlock)[1];
                ((uint*)banner)[2] = ((uint*)victimBlock)[2];
                ((uint*)banner)[3] = ((uint*)victimBlock)[3];

                for (int i = 0; i < 0xf; i++)
                {
                    if (banner[i] == 0 && banner[i + 1] != 0) banner[i] = 0x20;
                }

                *(ushort*)Mem.Arena(0x005e9a22) = *(ushort*)(victimBlock + 0x4c);
                *(ushort*)Mem.Arena(0x005e9aec) = *(ushort*)(victimBlock + 0x4e);
                *(byte*)Mem.Camera(0x005de0b4) = 0xff;
            }
        }

        if ((*victim & 0x800000) != 0)
        {
            TriggerEquippedItems_005238E0(victim, 0xd);
            return 0;
        }

        // Une entite de scene retient qui l'a frappee, et reagit si son genre le prevoit.
        ((byte*)victim)[9] = (byte)attacker[2];

        if ((((byte*)victim)[0x0d] & 0xf) == 10)
        {
            RunEntityScript(s_Unknown_005e97cc, (byte*)victim, *(ushort*)((byte*)victim + 0x0e));
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053a2b0 @ 0x0053a2b0 — <b>la resolution du coup d'un SORT</b>. Ce que
    /// <see cref="ResolveMeleeHit_00539B80"/> est a l'arme, celle-ci l'est a la magie : elle prend
    /// la paire famille/indice, va lire la fiche de <c>0x30</c> octets, et en tire l'element et le
    /// montant qui s'ajoutent au calcul.
    ///
    /// <para>
    /// <b>Trois differences avec le coup d'arme</b>, et elles disent ce qu'est un sort :
    /// </para>
    /// <list type="bullet">
    /// <item>l'attaque ne vient pas de <c>+0x30</c> mais de <c>+0x38</c>, <b>divisee par
    /// <c>fiche[0x1e]</c></b> — c'est le cout que la fiche fait payer a la puissance magique du
    /// lanceur. La defense vient de <c>+0x3a</c> et non de <c>+0x32</c> : deux autres
    /// caracteristiques, celles de la magie ;</item>
    /// <item>il n'y a pas de coup critique, mais un <b>jet de sauvegarde</b>
    /// (<see cref="RollSavingThrowB_0053A790"/>). <b>Le rater est bon pour l'assaillant</b> : c'est
    /// le retour nul qui divise la defense par deux ;</item>
    /// <item>l'element de la fiche <b>tord le bloc du lanceur</b> avant le calcul — le montant
    /// <c>fiche[0x18 + 2n]</c> s'ajoute a la case de cet element, et <b>toutes les autres cases
    /// sont mises a zero des deux cotes</b>, sauf celle-la et sa jumelle de paire
    /// (<c>(e-2) ^ 1</c>). Un sort de feu ne frappe qu'en feu, et efface le reste.</item>
    /// </list>
    /// <para>
    /// <b>Le meme montant sert deux fois</b>, et ce n'est pas une erreur de lecture : il tord le
    /// bloc plus haut, puis il se rajoute a la fin — <b>sauf si la victime est deja de cet
    /// element</b>, ce que <see cref="DominantElement_005399E0"/> tranche. Bruler ce qui brule deja
    /// perd le bonus, pas le sort.
    /// </para>
    /// <para>
    /// <b>Un element de 1 efface les huit cases.</b> L'indice conserve est <c>e - 2</c> en
    /// <b>non signe</b> : pour <c>e = 1</c> il vaut <c>0xFFFFFFFF</c>, qu'aucun <c>i</c> de 0 a 7
    /// n'egale. C'est ecrit tel quel, la soustraction non signee comprise.
    /// </para>
    /// </summary>
    /// <param name="variant">Le rang dans la fiche : <c>fiche[0x16 + n]</c> donne l'element et
    /// <c>fiche[0x18 + 2n]</c> le montant. Vaut 0 partout <b>sauf</b> chez
    /// <see cref="TickWrathHead_00481E30"/>, la tete du serpent de Colere, qui passe <c>1</c> —
    /// seul appelant du depot a le faire, releve le 2026-08-31.</param>
    /// <returns>1 si le coup a ete refuse, 0 sinon.</returns>
    private static int ResolveSpellHit_0053A2B0(
        uint* attacker, uint* victim, int family, int index, int variant)
    {
        if (ValidateCombatants_0053A860(attacker, victim) != 0) return 1;

        // Chacun retient l'autre en +0x70. Ecrit a la largeur d'un pointeur : le binaire y range un
        // dword parce qu'un pointeur en fait un.
        *(byte**)((byte*)attacker + 0x70) = (byte*)victim;
        *(byte**)((byte*)victim + 0x70) = (byte*)attacker;

        byte* attackerBlock;

        if ((*attacker & 0x800000) == 0)
        {
            attackerBlock = ResolveSceneCombatBlock_00538830(attacker, 0xff);
        }
        else
        {
            // La paire est (re)posee en +0x06 du lanceur avant que son bloc ne soit resolu : c'est
            // la meme case que CastSpell_0053A980 ecrit, et c'est ce que relira SpellSheetOf.
            *(ushort*)((byte*)attacker + 6) = (ushort)(((family & 0xff) << 8) | (index & 0xffff));
            attackerBlock = ResolvePartyCombatBlock_005387C0(attacker, 0xff, 0);
            TiltDominantElement_005398A0(attackerBlock);
        }

        byte* victimBlock;

        if ((*victim & 0x800000) == 0)
        {
            victimBlock = ResolveSceneCombatBlock_00538830(victim, 0xff);
        }
        else
        {
            // Ghidra rend cet appel SANS arguments (`iVar7 = FUN_005387c0();`). Le desassembleur
            // tranche : les trois `push 0 / push -1 / push ebx` sont faits AVANT le branchement
            // (0x0053a320..0x0053a329), donc les deux branches recoivent les memes trois.
            victimBlock = ResolvePartyCombatBlock_005387C0(victim, 0xff, 0);
            TiltDominantElement_005398A0(victimBlock);
        }

        if ((*attacker & 0x800000) != 0)
        {
            *Mem.Arena(0x005e99c0) = 0;                  // `mov byte [5E99C0h],0` — un octet
            TriggerEquippedItems_005238E0(attacker, 7);
        }

        if ((*victim & 0x800000) != 0)
        {
            *Mem.Arena(0x005e99c0) = 0;
            TriggerEquippedItems_005238E0(victim, 0x11);
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x100000) != 0)
        {
            // Le coup ne passe pas : la marque 0x29 et rien d'autre.
            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)((byte*)victim + 0x1c), SpriteAtlas(0x005648a0), 0x29, 0, 0);
            return 1;
        }

        // ---------------------------------------------------------------- la fiche du sort
        byte* sheet = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c) + family * 4)
                      + index * 0x30;

        byte element = sheet[0x16 + variant];

        if (element != 0)
        {
            *(short*)(attackerBlock + 0x10c + element * 2) +=
                *(short*)(sheet + 0x18 + variant * 2);

            uint kept = (uint)(element - 2);

            for (uint i = 0; i < 8; i++)
            {
                if (i == kept || i == (kept ^ 1)) continue;
                *(ushort*)(attackerBlock + 0x110 + i * 2) = 0;
                *(ushort*)(victimBlock + 0x110 + i * 2) = 0;
            }
        }

        // ---------------------------------------------------------------- attaque et defense
        // L'ordre des deux tirages est celui du binaire : ils partagent une seule suite.
        int magic = *(short*)(attackerBlock + 0x38) / sheet[0x1e];
        short attackSpread = *(short*)(attackerBlock + 0x2c);
        int attack = magic + ((CrtRand() % (attackSpread + 1) - (attackSpread >> 1)) >> 2);
        if (attack < 0) attack = 0;

        short defenceSpread = *(short*)(victimBlock + 0x2c);
        int defence = *(short*)(victimBlock + 0x3a)
                      + ((CrtRand() % (defenceSpread + 1) - (defenceSpread >> 1)) >> 2);
        if (defence < 0) defence = 0;

        // Le jet RATE (retour nul) coupe la defense en deux — le biais est du cote du sort.
        if (RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) == 0)
        {
            defence >>= 1;
        }

        // ---------------------------------------------------------------- les degats
        int damage = attack - defence;
        if (damage < 0) damage = 0;

        damage += ResolveElementalExchange_005398E0(attackerBlock, victimBlock);

        // Le montant de la fiche ne se rajoute que si la victime n'est PAS deja de cet element.
        if (element == 0 || element != DominantElement_005399E0(victimBlock))
        {
            damage += *(ushort*)(sheet + 0x18 + variant * 2);
        }

        damage -= *(short*)(victimBlock + 0x3a) >> 5;
        if (damage < 1) damage = 1;

        if (Mem.s_DebugModeLatch_005633a8 == 0 && s_DebugOverlayEnabled_005de020 != 0)
        {
            // 0x0053a511..0x0053a61f. Ghidra y lit `param_3` et `param_4` — la famille et l'indice
            // du sort —, mais le binaire a REUTILISE ces deux emplacements de pile pour l'attaque
            // (0x0053a44d) et la defense (0x0053a47f). C'est donc la meme paire que le coup de
            // melee qui part dans le bandeau. L'octet +0x23 est nul ici.
            PostCombatDebugBanner(attacker, victim, attackerBlock, victimBlock,
                element, 0, attack, defence, damage);
        }

        // ---------------------------------------------------------------- le coup porte
        byte* attackerCharacter = GetMainMemPtr() + ((byte)attacker[2] + 0x78) * 0x2000;

        int killed = ApplyDamage_00539A20(
            attacker, victim, attackerBlock, victimBlock, (short)damage, element);

        if (killed != 0 && (*attacker & 0x800000) != 0
            && *(int*)(attackerCharacter + 0x15c) != -1)
        {
            *(int*)(attackerCharacter + 0x15c) += 1;
        }

        // Une entite de scene retient qui l'a frappee, et reagit si son genre le prevoit. Le test
        // porte sur 0x400000 et non sur l'absence de 0x800000 : ce n'est pas tout a fait le meme
        // ensemble que celui de FUN_00539b80, et c'est ecrit tel quel.
        if ((*victim & 0x400000) != 0)
        {
            ((byte*)victim)[9] = (byte)attacker[2];

            if ((((byte*)victim)[0x0d] & 0xf) == 10)
            {
                RunEntityScript(s_Unknown_005e97cc, (byte*)victim, *(ushort*)((byte*)victim + 0x0e));
            }
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: les deux blocs jumeaux de <c>FUN_00539b80</c> (0x00539bc0 et 0x00539c60) —
    /// <b>refaire la fiche d'un combattant avant le coup</b>, et rendre le bloc de statistiques sur
    /// lequel tout le calcul va porter.
    ///
    /// <para>Les deux moities du binaire sont ecrites ici en une seule parce qu'elles ne different
    /// que par l'octet de ligne d'arme (<c>+0x2b0</c> pour l'assaillant, <c>+0x2b1</c> pour la
    /// victime) et par le bloc supplementaire de <paramref name="withStanceRow"/>, que seule la
    /// victime porte.</para>
    /// </summary>
    /// <param name="rowByte">L'octet du bloc par personnage qui designe la ligne de 0x280 a
    /// ajouter aux elements.</param>
    /// <param name="withStanceRow">La victime ajoute une seconde ligne, celle de <c>+0x2b2</c>, et
    /// seulement si elle est debout — posture 0 ou 6.</param>
    private static byte* RefreshCombatantBlock(uint* fighter, int rowByte, bool withStanceRow)
    {
        if ((*fighter & 0x800000) == 0)
        {
            byte* stats = s_EntityStatBlocks_005ea560 + (byte)fighter[2] * 0x120;

            RebaseStatBlockOnModel_00538AF0((byte*)fighter);   // Ghidra: FUN_00538af0
            ScaleStatBlockByDifficulty(stats);                 // Ghidra: FUN_00538f50
            FinalizeEquipmentEffects(stats);                   // Ghidra: FUN_00538fa0
            return stats;
        }

        byte* character = GetMainMemPtr() + ((byte)fighter[2] + 0x78) * 0x2000;
        byte* block = character + 0x100;

        CommitStagedStatBlock(character);                      // Ghidra: FUN_00538ab0
        ScaleStatBlockByDifficulty(block);                     // Ghidra: FUN_00538f50
        ComputeDerivedStats(character);                        // Ghidra: FUN_00538c50
        ApplyEquipmentStatBonuses(character);                  // Ghidra: FUN_00538e20
        FinalizeEquipmentEffects(block);                       // Ghidra: FUN_00538fa0

        AddElementRow(character, character[rowByte]);

        if (withStanceRow)
        {
            int stance = character[0x150] & 7;
            if (stance == 0 || stance == 6) AddElementRow(character, character[0x2b2]);
        }

        TiltDominantElement_005398A0(block);                   // Ghidra: FUN_005398a0
        return block;
    }

    /// <summary>Ghidra: la boucle de huit tours qui suit chaque octet de ligne — les huit
    /// <c>char</c> <b>signes</b> de <c>personnage + ligne * 0x280 + 0x318</c> s'ajoutent aux huit
    /// elements de <c>personnage + 0x210</c>. Une ligne de <c>0xff</c> veut dire « aucune ».</summary>
    private static void AddElementRow(byte* character, byte row)
    {
        if (row == 0xff) return;

        for (int i = 0; i < 8; i++)
        {
            *(short*)(character + 0x210 + i * 2) += (short)(sbyte)character[row * 0x280 + 0x318 + i];
        }
    }
}
