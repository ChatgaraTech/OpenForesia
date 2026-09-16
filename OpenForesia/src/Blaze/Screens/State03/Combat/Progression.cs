// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA PROGRESSION — le niveau qui monte, et ce qu'il fait gagner.
//
// `CheckLevelUp_00539190` teste le palier, `LevelUpCharacter_005391F0` applique les sept gains,
// `GrowCharacteristic` tire chacun, `TryLearnSpell` apprend — et c'est le SEUL chemin
// d'apprentissage d'un sort en jeu normal.
//
// LE PLAFOND EST FIXE A LA CREATION, pas au niveau : il culmine a 510 pour un depart de 80 a 83.
// Et la chance est la seule statistique qui peut BAISSER.
//
// `ApplyPercentDamage_005397B0` et `CollectEntityHit_005197D0` sont ici parce qu'ils partagent le
// meme voisinage d'origine, pas par parente de sujet.
//
// Ce que ces fonctions calculent est documente en detail dans
// Documentation/Reference/MonteeDeNiveau.md, et rejouable hors ligne par le verbe `montee` de
// OpenForesia.Tools.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_005397b0 @ 0x005397b0 — <b>applique des degats en pourcentage des points de vie
    /// maximaux</b>, avec le verrou d'invulnerabilite qui va avec. Le corps est dans
    /// <c>Blaze.c</c> ; il etait stube ici sous le nom <c>SetEntityPose_005397B0</c>, qui <b>etait
    /// faux</b> : il n'y a aucune pose la-dedans.
    ///
    /// <para>
    /// Ce que la lecture du corps donne, et que l'adresse seule ne donnait pas : <c>+0x96</c> est un
    /// <b>verrou d'invulnerabilite</b>. Non nul, le coup ne passe pas du tout ; la fonction le pose
    /// a <c>0x10</c> des qu'elle frappe. Les deux appelants deja transcrits le confirment — ils le
    /// mettent a zero juste avant l'appel et le remettent a <c>0x10</c> juste apres, pour forcer un
    /// coup a travers.
    /// </para>
    /// <para>
    /// Les degats sont <c>PV_max * pourcentage / 100</c>, avec un <b>plancher a 1</b> : un
    /// pourcentage trop petit pour valoir un point en vaut quand meme un. A zero point de vie, le
    /// bit <c>0x40000000</c> part <b>a la fois</b> sur le bloc du personnage et sur son entite.
    /// </para>
    /// <para>
    /// <b>Deux champs sont sauves et rendus</b> autour de la naissance de l'effet <c>0x50</c> :
    /// <c>+0x18</c> et <c>+0x0a</c>. L'effet herite d'une copie de l'entite
    /// (<see cref="SpawnEffectEntity"/>), donc le binaire y glisse <c>100</c> et
    /// <paramref name="effectParam"/> le temps de l'appel, puis remet les valeurs d'origine. Sans
    /// cette restitution, une entite frappee garderait la teinte de son propre effet de degats.
    /// </para>
    /// </summary>
    private static void ApplyPercentDamage_005397B0(uint* entity, int percent, ushort effectParam)
    {
        byte* e = (byte*)entity;

        if (*(short*)(e + 0x96) != 0) return;
        *(short*)(e + 0x96) = 0x10;

        // Ghidra: `((byte)param_1[2] + 0x78) * 0x2000 + MainMem` — l'octet +0x8 est l'index de
        // membre, et 0x78 * 0x2000 vaut 0xf0000, le meme decalage de bloc de personnage que
        // AreaVerb_0053DEA0 ecrit `membre * 0x2000 + 0xf02bc`.
        byte* character = GetMainMemPtr() + (e[8] + 0x78) * 0x2000;

        int damage = *(short*)(character + 0x148) * percent / 100;
        if (damage == 0) damage = 1;

        *(short*)(character + 0x14c) -= (short)damage;
        if (*(short*)(character + 0x14c) < 1)
        {
            *(ushort*)(character + 0x14c) = 0;
            *(uint*)(character + 0x140) |= 0x40000000;
            *(uint*)e |= 0x40000000;
        }

        *(ushort*)(character + 0x196) = 0;
        *(uint*)(character + 0x140) &= 0xffffffbf;

        SpawnParticle_0050F070((uint*)(e + 0x1c), (uint)damage, 0);

        uint savedTint = *(uint*)(e + 0x18);
        ushort savedFacing = *(ushort*)(e + 0x0a);
        *(ushort*)(e + 0x18) = 100;
        *(ushort*)(e + 0x1a) = effectParam;

        SpawnEffectEntity(e, 0x50, 0, 0);                 // Ghidra: FUN_0047a9d0

        *(uint*)(e + 0x18) = savedTint;
        *(ushort*)(e + 0x0a) = savedFacing;
    }


    /// <summary>
    /// Ghidra: FUN_005197d0 @ 0x005197d0 — <b>la collecte du coup recu</b> : un balayage 3x3 des
    /// cellules de la seconde grille spatiale autour du personnage, qui rend la premiere entite
    /// armee (bit <c>0x80000</c>) assez proche pour toucher, ou zero.
    ///
    /// <para>
    /// « Assez proche » se lit en deux temps : moins de 0x28 unites au sol, et une difference de
    /// hauteur dans une bande de <c>±0x50</c> — le test <c>(dy + 0x50) &lt; 0xa0</c> en <b>non
    /// signe</b>, qui rejette les deux debordements d'un coup.
    /// </para>
    /// <para>
    /// <b>Chaque entite ne touche qu'une fois.</b> L'octet <c>+0x09</c> de la candidate porte un
    /// bit par emplacement de personnage : il est pose au moment du coup et n'est efface que
    /// lorsque la distance repasse au-dessus du seuil. Une entite qui reste collee ne blesse donc
    /// pas a chaque image.
    /// </para>
    /// <para>
    /// <b>La constante de grille est <c>0x389f</c> et non <c>0x38a0</c></b> : le <c>-1</c> du
    /// balayage est replie dedans, la ou <see cref="ApplyEntityMotion_00519DF0"/> l'ecrit a part.
    /// La consequence est reelle et conservee : pour un personnage dans la rangee 0, la premiere
    /// des trois rangees balayees est <c>0x389f</c>, c'est-a-dire <b>la derniere rangee de l'autre
    /// grille</b> — les deux pages etant adjacentes (0x1c4000 et 0x1c5000). Le garde
    /// <c>row &lt; 0x71800</c> ne borne que le haut.
    /// </para>
    /// </summary>
    private static byte* CollectEntityHit_005197D0(uint* entityPtr)
    {
        byte* entity = (byte*)entityPtr;
        byte* mainMem = GetMainMemPtr();

        int y = *(int*)(entity + 0x20);
        int x = *(int*)(entity + 0x1c);
        int z = *(int*)(entity + 0x24);

        uint cellX = (uint)(((((x >> 0xc) + 0x2000) >> 9) - 1));
        uint row = (uint)((((z >> 0xc) + 0x2000) >> 9) + 0x389f) * 0x20;

        byte mask = (byte)(1 << (entity[8] & 0x1f));

        for (int outer = 3; outer != 0; outer--)
        {
            uint column = cellX;

            for (int inner = 3; inner != 0; inner--)
            {
                if (column < 0x20 && row < 0x71800)
                {
                    for (byte* candidate = *(byte**)(mainMem + (column + row) * 4);
                         candidate != null;
                         candidate = *(byte**)(candidate + 0x68))
                    {
                        int candidateY = *(int*)(candidate + 0x20);
                        int dx = (x - *(int*)(candidate + 0x1c)) >> 0xc;
                        int dz = (z - *(int*)(candidate + 0x24)) >> 0xc;
                        int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

                        if ((*(uint*)candidate & 0x80000) == 0) continue;

                        if (distance < 0x28 && (uint)(((y - candidateY) >> 0xc) + 0x50) < 0xa0)
                        {
                            if ((candidate[9] & mask) == 0)
                            {
                                candidate[9] |= mask;
                                *Mem.Camera(0x005de0b8) = 1;
                                return candidate;
                            }
                        }
                        else
                        {
                            candidate[9] &= (byte)~mask;
                        }
                    }
                }

                column++;
            }

            cellX = column - 3;
            row += 0x20;
        }

        // Ghidra: `DAT_005de0b8 = local_14;` — le compteur de boucle, nul a la sortie.
        *Mem.Camera(0x005de0b8) = 0;
        return null;
    }

    /// <summary>
    /// Ghidra: FUN_00539190 @ 0x00539190 — <b>la verification de palier apres un gain
    /// d'experience</b>, en basculant l'allocateur de la DLL sur le tampon de travail le temps du
    /// calcul. Exactement la meme enveloppe que <see cref="RunEntityScript"/> autour de
    /// <c>RunScript</c> : <c>Ordinal_46</c> rend l'etat courant de l'allocateur, negatif voulant
    /// dire « rien a preserver ».
    ///
    /// <para>
    /// <b>Elle s'appelait <c>ApplyDamageToMember_00539190</c> et ce nom etait faux</b> — elle
    /// n'applique aucun degat. Le site d'appel le montre : <c>+0x158</c> vient de recevoir
    /// l'experience du coup, et c'est cela qu'elle examine. Troisieme nom invente tombe le
    /// 2026-08-29, tous les trois sur le chemin « taper sur un monstre ».
    /// </para>
    /// <para>
    /// <b>Ghidra rend le second appel comme <c>FUN_005391f0(unaff_retaddr, param_1)</c></b>, ce qui
    /// n'a pas de sens. Le desassemblage tranche : les deux branches font
    /// <c>push [esp+8] ; push [esp+4] ; call</c>, donc les deux passent <c>(param_1, param_2)</c>.
    /// L'artefact vient de ce que Ghidra a perdu le niveau de pile apres l'appel <b>stdcall</b> a
    /// <c>Ordinal_45</c>, qui depile lui-meme son argument.
    /// </para>
    /// </summary>
    private static void CheckLevelUp_00539190(byte* member, byte* character)
    {
        if (Winsys3d.Ordinal_46() < 0)
        {
            LevelUpCharacter_005391F0(member, character);
            return;
        }

        *(int*)Mem.Arena(0x005e9318) = Winsys3d.Ordinal_46();
        Winsys3d.Ordinal_45(GetMainMemPtr() + 0x1c0000);
        LevelUpCharacter_005391F0(member, character);
        Winsys3d.Ordinal_45((void*)(nint)(*(int*)Mem.Arena(0x005e9318)));
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056d308</c> — <b>la caracteristique favorisee par un ELEMENT</b>, un
    /// octet pour chacun des six premiers. Relevee dans le binaire : <c>00 01 04 03 02 05</c>, une
    /// permutation de 0..5 — chaque element pousse une caracteristique differente d'un cinquieme a
    /// la montee de niveau, et les elements 6 et 7 poussent les six d'un vingtieme.
    ///
    /// <para>
    /// <b>Le champ indexeur est <c>+0x151</c>, et il porte l'element, pas une sous-classe</b> —
    /// <c>DebugParty</c> et l'ecran de creation le remplissent tous deux depuis la <b>huitieme</b>
    /// colonne de <c>s_ClassBaseStats_0056c7e0</c>, celle que le bestiaire a identifiee comme
    /// l'element. Ce symbole s'appelait <c>s_SubClassFavouredStat_0056D308</c> jusqu'au
    /// 2026-08-29 : le nom etait une supposition, pas un releve.
    /// </para>
    /// </summary>
    private static readonly byte[] s_ElementFavouredStat_0056D308 = [0, 1, 4, 3, 2, 5];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056d310</c> — <b>les chances de progression par classe</b>, un dword
    /// pour chacune des huit, releve dans le binaire. Chaque dword porte quatre octets lus deux par
    /// deux : <c>(base physique, diviseur physique, base mentale, diviseur mental)</c>, d'ou
    /// <c>chance = septieme caracteristique / diviseur + base</c>.
    ///
    /// <para>Trois valeurs seulement pour huit classes — <c>0x04280241</c>, <c>0x023c0423</c> et
    /// <c>0x03320332</c> — reparties en 0,6 · 1,3,7 · 2,4,5. Les classes se rangent donc en trois
    /// familles de progression.</para>
    /// </summary>
    private static readonly uint[] s_ClassGrowthOdds_0056D310 =
    [
        0x04280241, 0x023c0423, 0x03320332, 0x023c0423,
        0x03320332, 0x03320332, 0x04280241, 0x023c0423,
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056d330</c> — <b>combien de sorts d'une ecole sont accessibles a un
    /// palier</b>, cinq octets par ecole, releves dans le binaire.
    ///
    /// <para>
    /// <c>05 0a 0f 14 1a · 05 0b 10 13 16 · 03 07 09 0c 10</c> — trois ecoles, et la suite est
    /// nulle. Le corps se garde lui-meme (<c>if (compte != 0)</c>), donc une quatrieme ecole
    /// n'apprendrait simplement rien ; les cinq octets nuls sont conserves pour que l'indexation
    /// reste celle du binaire.
    /// </para>
    /// </summary>
    private static readonly byte[] s_SpellsByTier_0056D330 =
    [
        0x05, 0x0a, 0x0f, 0x14, 0x1a,
        0x05, 0x0b, 0x10, 0x13, 0x16,
        0x03, 0x07, 0x09, 0x0c, 0x10,
        0x00, 0x00, 0x00, 0x00, 0x08,
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005648a8</c> — le descripteur de <b>l'effet visuel de montee de
    /// niveau</b>, onze enregistrements de huit octets releves dans le binaire. La borne se lit
    /// dans la donnee : ses voisins connus (<c>DAT_00564800</c> a <c>DAT_00564860</c>) avancent
    /// tous de huit, et le bloc s'arrete a 0x00564900 ou commence une suite <c>1, 2, …</c> qui n'a
    /// plus cette forme.
    /// </summary>
    private static readonly byte[] s_LevelUpEffect_005648A8 =
    [
        0xd8, 0x58, 0x27, 0x08, 0x01, 0x00, 0x30, 0x02,
        0xa2, 0x20, 0x32, 0x08, 0x01, 0x00, 0x30, 0x02,
        0x00, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x06,
        0x08, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x04,
        0x10, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x05,
        0x18, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x07,
        0x20, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x07,
        0x28, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x09,
        0x30, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x09,
        0x38, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x0a,
        0x48, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x04,
    ];

    private static byte* s_LevelUpEffectStorage;

    /// <summary>Le descripteur en memoire native : l'entite nee le garde par pointeur
    /// (<c>+0x3c</c>), donc il doit survivre a l'appel.</summary>
    private static byte* LevelUpEffectDescriptor()
    {
        if (s_LevelUpEffectStorage == null)
        {
            s_LevelUpEffectStorage = (byte*)NativeMemory.Alloc((nuint)s_LevelUpEffect_005648A8.Length);
            for (int i = 0; i < s_LevelUpEffect_005648A8.Length; i++)
            {
                s_LevelUpEffectStorage[i] = s_LevelUpEffect_005648A8[i];
            }
        }

        return s_LevelUpEffectStorage;
    }

    /// <summary>Ghidra: DAT_005e85a8 et DAT_005e85aa — les deux chances de progression du tour,
    /// physique et mentale. Reserve non initialisee, ecrite et relue par la seule
    /// <see cref="LevelUpCharacter_005391F0"/> : c'est du brouillon que le compilateur de 1998 a
    /// mis en donnees plutot que sur la pile.</summary>
    private static short s_LevelUpPhysicalOdds_005e85a8, s_LevelUpMentalOdds_005e85aa;

    /// <summary>Ghidra: DAT_005e85b0..DAT_005e85bc — les sept gains tires pour le tour : six
    /// caracteristiques et, en septieme, le pas de la valeur de <c>+0x28c</c>. Meme nature de
    /// brouillon que la paire ci-dessus.</summary>
    private static readonly short[] s_LevelUpGains_005e85b0 = new short[7];

    /// <summary>
    /// Ghidra: FUN_005391f0 @ 0x005391f0 — <b>la montee de niveau</b>. Le corps entier est une
    /// boucle (<c>goto LAB_00539207</c> en queue de fonction). Vue d'ensemble dans
    /// <c>Documentation/Reference/MonteeDeNiveau.md</c>.
    ///
    /// <para>
    /// <b>Le palier</b> est <c>niveau / 4 + 0x30 + niveau * 2</c>, borne a 9999, et l'experience
    /// (<c>+0x158</c>) est <b>remise a zero</b> des le franchissement — ce n'est pas un total cumule
    /// mais une jauge, et le surplus est perdu. Le niveau vit en <c>+0x146</c> et son plancher en
    /// <c>+0x144</c> : le second avance d'un a chaque montee et tire le premier vers le haut s'il
    /// avait ete abaisse.
    /// </para>
    /// <para>
    /// <b>La boucle ne tourne donc jamais deux fois.</b> La remise a zero est <i>a l'interieur</i>
    /// du corps, et le palier vaut au minimum <c>0x30</c> : le second tour ne peut pas repasser le
    /// test. Un seul coup ne fait gagner qu'un niveau, quelle que soit l'experience rapportee. Le
    /// commentaire porte l'affirmation inverse jusqu'au 2026-08-29 — la forme du code l'avait
    /// emporte sur ce qu'il fait.
    /// </para>
    /// <para>
    /// <b>Le septieme champ de <c>+0x2a0</c> mene tout le reste.</b> Les six premiers sont les
    /// caracteristiques ; le septieme (<c>+0x2ac</c>) sert de facteur commun — il elargit les
    /// tirages de gain, il divise dans le calcul des deux chances, et il pese dans le tirage de
    /// <c>+0x28c</c>. Une seule valeur decide donc du rythme de progression du personnage.
    /// </para>
    /// <para>
    /// <b>Une caracteristique ne monte que si elle est SOUS un plafond</b> et si un tirage passe.
    /// Les six alternent entre la chance physique et la chance mentale dans l'ordre physique,
    /// mentale, mentale, physique, physique, mentale.
    /// </para>
    /// <para>
    /// <b>Le plafond se calcule sur la valeur de DEPART, pas sur la valeur courante</b> :
    /// <c>(v/8 - v²/16) + (v+10)*10</c> ou <c>v</c> vient de <c>+0x2a0</c>, le bloc fige a la
    /// creation — <c>psVar9 = (short *)(param_2 + 0x2a0)</c> dans <c>Blaze.c</c>. Il est donc
    /// <b>constant pour toute la partie</b>, et c'est la valeur de depart choisie a la creation qui
    /// decide, seule, jusqu'ou la caracteristique pourra monter. La courbe culmine a <b>510</b> pour
    /// un depart de 80 a 83 et <b>redescend</b> ensuite : un depart de 99 plafonne a 490, moins
    /// qu'un depart de 80.
    /// </para>
    /// <para>
    /// <b>La valeur de <c>+0x28c</c> peut BAISSER</b> : au-dessous de 0x1f au tirage elle perd le
    /// septieme gain, entre 0x1f et 0x46 ce gain est remis a zero pour la suite, au-dessus elle
    /// l'ajoute. Bornee a 0..200. C'est le seul champ du bloc qui recule a une montee de niveau.
    /// </para>
    /// <para>
    /// <b>L'apprentissage de sort ne concerne que les classes 1, 3, 5 et 7</b>, et la classe 5 voit
    /// son niveau ramene a 0x50 au-dela de 0x6d avant le calcul du palier — un plafonnement propre
    /// a elle, transcrit tel quel. Le premier sort de l'ecole encore inconnu dont le cout tient
    /// dans le tirage est appris, et un seul par montee (<c>break</c>).
    /// </para>
    /// </summary>
    private static void LevelUpCharacter_005391F0(byte* member, byte* c)
    {
        bool leveled = false;

        // `local_10` de Ghidra : le brouillon du tour, hisse hors de la boucle (un stackalloc par
        // tour ferait deborder la pile sur un gain de plusieurs niveaux).
        short* scratch = stackalloc short[8];

        while (true)
        {
            ushort level = *(ushort*)(c + 0x146);

            if (level == 9999)
            {
                *(int*)(c + 0x158) = 0;
                break;
            }

            uint needed = (uint)((level >> 2) + 0x30 + level * 2);
            if (needed > 9999) needed = 9999;
            if (needed > *(uint*)(c + 0x158)) break;

            // ------------------------------------------------------------ le palier est franchi
            *(short*)(c + 0x144) += 1;
            leveled = true;
            *(int*)(c + 0x158) = 0;
            *(ushort*)(c + 0x146) = (ushort)(level + 1);

            if ((ushort)(level + 1) < *(ushort*)(c + 0x144))
            {
                *(ushort*)(c + 0x146) = *(ushort*)(c + 0x144);
            }

            // +0x2a0 : le bloc FIGE a la creation. Tout ce qui suit s'y refere — la taille des
            // gains ET les plafonds —, jamais aux valeurs courantes de +0x280.
            short* start = (short*)(c + 0x2a0);
            int seventh = start[6];

            for (int i = 0; i < 7; i++) scratch[i] = (short)((start[i] >> 3) + 1);

            for (int i = 0; i < 7; i++)
            {
                s_LevelUpGains_005e85b0[i] = (short)(
                    CrtRand() % scratch[i] + 1 + CrtRand() % ((seventh >> 3) + 1));
            }

            // ------------------------------------------------------------ les jauges
            *(short*)(c + 0x148) += (short)(CrtRand() % s_LevelUpGains_005e85b0[4] + 2);
            *(short*)(c + 0x14a) += (short)(CrtRand() % s_LevelUpGains_005e85b0[5] + 1);

            if (*(short*)(c + 0x148) > 9999) *(short*)(c + 0x148) = 9999;
            if (*(short*)(c + 0x14a) > 9999) *(short*)(c + 0x14a) = 9999;

            // ------------------------------------------------------------ les deux chances
            uint odds = s_ClassGrowthOdds_0056D310[c[0x150] & 7];

            s_LevelUpPhysicalOdds_005e85a8 =
                (short)(seventh / (int)(odds >> 8 & 0xff) + (short)(odds & 0xff));
            s_LevelUpMentalOdds_005e85aa =
                (short)(seventh / (int)(odds >> 0x18) + (short)(odds >> 0x10 & 0xff));

            // ------------------------------------------------------------ les plafonds
            //
            // Sur les valeurs de DEPART (+0x2a0), pas sur les courantes : le plafond de chaque
            // caracteristique est donc le meme a tous les niveaux. Voir MonteeDeNiveau.md.
            for (int i = 0; i < 6; i++)
            {
                short v = start[i];
                scratch[i] = (short)((v >> 3) - (short)(v * v >> 4) + (v + 10) * 10);
            }

            byte element = c[0x151];

            if (element < 6)
            {
                int favoured = s_ElementFavouredStat_0056D308[element];
                scratch[favoured] += (short)(scratch[favoured] / 5);
            }
            else if (element == 6 || element == 7)
            {
                for (int i = 0; i < 6; i++) scratch[i] += (short)(scratch[i] / 0x14);
            }

            // ------------------------------------------------------------ les caracteristiques
            short* carac = (short*)(c + 0x280);

            GrowCharacteristic(carac + 0, scratch[0], s_LevelUpPhysicalOdds_005e85a8, 0);
            GrowCharacteristic(carac + 1, scratch[1], s_LevelUpMentalOdds_005e85aa, 1);
            GrowCharacteristic(carac + 2, scratch[2], s_LevelUpMentalOdds_005e85aa, 2);
            GrowCharacteristic(carac + 3, scratch[3], s_LevelUpPhysicalOdds_005e85a8, 3);
            GrowCharacteristic(carac + 4, scratch[4], s_LevelUpPhysicalOdds_005e85a8, 4);
            GrowCharacteristic(carac + 5, scratch[5], s_LevelUpMentalOdds_005e85aa, 5);

            for (int i = 0; i < 6; i++)
            {
                if (carac[i] > 999) carac[i] = 999;
            }

            // ------------------------------------------------------------ le champ qui recule
            int roll = CrtRand() % 100 + (seventh >> 2);
            int value = *(short*)(c + 0x28c);

            if (roll < 0x47)
            {
                if (roll < 0x1f) value -= s_LevelUpGains_005e85b0[6];
                else s_LevelUpGains_005e85b0[6] = 0;
            }
            else
            {
                value += s_LevelUpGains_005e85b0[6];
            }

            if (value < 0) value = 0;
            if (value > 200) value = 200;
            *(short*)(c + 0x28c) = (short)value;

            // ------------------------------------------------------------ les derivees, refaites
            CommitStagedStatBlock(c);                          // Ghidra: FUN_00538ab0
            ScaleStatBlockByDifficulty(c + 0x100);             // Ghidra: FUN_00538f50
            ComputeDerivedStats(c);                            // Ghidra: FUN_00538c50
            ApplyEquipmentStatBonuses(c);                      // Ghidra: FUN_00538e20
            FinalizeEquipmentEffects(c + 0x100);               // Ghidra: FUN_00538fa0

            TryLearnSpell(member, c);
        }

        if (!leveled) return;

        // Ghidra: l'aureole de montee de niveau, puis une gerbe d'entites 0x59 — le meme sous-type
        // que la gerbe de soin de RestoreVitalsEntity_00403710, lance ici avec +0x18 force.
        SpawnStatusEffectEntity_0047AC40(
            member, (int*)(member + 0x1c), LevelUpEffectDescriptor(), 0x29, 0, 0);

        uint saved = *(uint*)(member + 0x18);
        *(ushort*)(member + 0x18) = 0xffff;
        *(ushort*)(member + 0x1a) = 0x65;
        SpawnEffectEntity(member, 0x59, 0, 0);                 // Ghidra: FUN_0047a9d0
        *(uint*)(member + 0x18) = saved;
    }

    /// <summary>Une caracteristique monte si elle est sous son plafond et qu'un centieme passe.
    /// Les six sites ne different que par le plafond, la chance et le gain.</summary>
    private static void GrowCharacteristic(short* value, short ceiling, short odds, int gain)
    {
        if (*value < ceiling && CrtRand() % 100 <= odds)
        {
            *value += s_LevelUpGains_005e85b0[gain];
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0053962b..0x005396f7</c> — <b>l'apprentissage de sort</b> de la montee de
    /// niveau, reserve aux classes 1, 3, 5 et 7. <b>C'est le seul chemin par lequel un sort
    /// s'apprend en jeu normal</b> ; vue d'ensemble dans
    /// <c>Documentation/Reference/Sorts.md</c>.
    ///
    /// <para>
    /// Le palier vient du <b>plancher de niveau</b> (<c>+0x144</c>) compare a quatre seuils
    /// (0x13, 0x31, 0x4f, 0x6d), ce qui donne 0 a 4. La table de 0x0056d330 dit alors combien de
    /// sorts de l'ecole sont accessibles.
    /// </para>
    /// <para>
    /// <b>La classe 5 est plafonnee au palier 3</b>, pas 2 : son niveau est ramene a <c>0x50</c>
    /// (80) des qu'il depasse <c>0x6d</c> (109), et 80 franchit encore le troisieme seuil
    /// (<c>80 &gt; 0x4f</c>) sans atteindre le quatrieme. Elle s'arrete donc a 20 sorts sur les 26
    /// que son ecole offre au palier 4 — six de moins que la classe 3, qui partage pourtant la meme
    /// ecole. Ce commentaire portait « palier 2 » jusqu'au 2026-08-29.
    /// </para>
    /// <para>
    /// Les sorts de l'ecole vivent dans le repertoire principal (<c>+0x9c</c>), enregistrements de
    /// 0x30 octets dont <c>+0x1d</c> est le cout d'apprentissage. Le premier encore inconnu dont le
    /// cout tient dans <c>tirage % (+0x122 + 1) / 8 + (+0x154)</c> est appris — <b>un seul par
    /// montee</b>.
    /// </para>
    /// </summary>
    private static void TryLearnSpell(byte* member, byte* c)
    {
        byte characterClass = (byte)(c[0x150] & 7);

        if (characterClass != 1 && characterClass != 3
            && characterClass != 5 && characterClass != 7)
        {
            return;
        }

        ushort level = *(ushort*)(c + 0x144);
        if (characterClass == 5 && level > 0x6d) level = 0x50;

        uint tier = level > 0x13 ? 1u : 0u;
        if (level > 0x31) tier++;
        if (level > 0x4f) tier++;
        if (level > 0x6d) tier++;

        uint school = c[0x2b5];
        if (tier + school * 5 >= s_SpellsByTier_0056D330.Length) return;

        byte available = s_SpellsByTier_0056D330[tier + school * 5];
        if (available == 0) return;

        byte* spell = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c) + school * 4) + 0x1d;
        uint* known = (uint*)(c + 0x160 + school * 8);

        for (uint i = 0; i < available; i++, spell += 0x30)
        {
            if (*spell == 0) continue;
            if ((*known & 1u << (int)(i & 0x1f)) != 0) continue;

            int reach = (CrtRand() % (*(short*)(c + 0x122) + 1) >> 3) + *(short*)(c + 0x154);
            if (*spell > reach) continue;

            *known |= 1u << (int)(i & 0x1f);

            // Le temps de l'effet, l'entite porte le descripteur de l'ecole : mis de cote et
            // rendu tout de suite apres.
            uint savedDescriptor = *(uint*)(member + 0x3c);

            *(byte*)(member + 0x40) = (byte)i;
            *(uint*)(member + 0x3c) = *(uint*)(
                *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x10 + c[0x2b5] * 4);

            SpawnEffectEntity(member, 0xbc, 0, 0);             // Ghidra: FUN_0047a9d0

            *(uint*)(member + 0x3c) = savedDescriptor;
            break;
        }
    }
}
