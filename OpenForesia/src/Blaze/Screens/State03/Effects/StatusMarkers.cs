// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES MARQUES D'ETAT — la famille 0x0040xxxx, ce qui se voit quand un effet est pose.
//
// Un effet d'etat ne se dessine pas lui-meme : il fait NAITRE une entite, et c'est elle qu'on
// voit. Le sous-type 0x29 marque la pose d'un bit d'etat, le 0xbf marque le coup SANS effet -
// celui qu'une immunite a annule. Les corps sont ici ; ce qui les declenche est dans
// `Combat/StatusEffects.cs`.
//
// LE PIEGE DE CE FICHIER EST LA LONGUEUR DES PLANCHES, PAS LEUR CONTENU. Chaque `s_Descriptors_…`
// est une plage relevee dans `.data`, et un consommateur indexe EN AVANT :
// `*(byte**)(e + 0x3c) + e[0x40] * 8`. Une plage transcrite trop courte ne provoque ni erreur de
// compilation ni exception - le sprite sort simplement de la mauvaise taille, invisible, ou en
// morceaux. Ce defaut a ete expedie trois fois.
//
// Lancer `sh OpenForesia.Tools/check-descriptors.sh` apres toute retouche a une planche : il
// relit chaque tableau dans `Blaze.exe` et dit combien d'enregistrements restent apres le
// decalage de chaque site d'installation. Une marge de 1 n'est sure que si le corps laisse
// `+0x40` a zero - a verifier a la main, le script ne le fait pas.
//
// Voir Documentation/Reference/DataSymbols.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_00403710 @ 0x00403710 — <b>l'effet de soin</b>, et le seul gestionnaire que la
    /// table statique <see cref="s_StaticEntityHandlers_00551c30"/> puisse recevoir <b>par la
    /// branche basse</b> de <see cref="TickEntityTable_005ff400"/> : <c>FUN_0047a9d0</c> est
    /// l'unique ecrivain de l'octet de sous-type <c>0x005ff405</c>, et ses six sites d'appel
    /// passent tous <c>0x59</c>, dont l'entree 89 de la table vaut <c>0x00403710</c>.
    ///
    /// <para>
    /// <b>Cela ne dit rien de la branche haute.</b> Six autres sites de <c>Blaze.c</c> ecrivent
    /// directement dans <c>DAT_005ff400</c> — sans passer par <c>FUN_0047a9d0</c> — et y posent des
    /// sous-types <c>&gt;= 0xC0</c>, qui vont dans la table installee par zone. Le premier observe
    /// est <c>0xe1</c>, dans la salle du barman : voir <see cref="EntityHandler_004055C0"/>.
    /// </para>
    ///
    /// <para>
    /// L'entite est un satellite : elle suit son porteur (<c>+0x6c</c>), dont elle recopie la
    /// position a chaque image, et le compteur <c>+0x16</c> mene la scenette — deux bruitages a
    /// l'ouverture, une gerbe d'entites <c>0x59</c> lancees en spirale tant que le compteur est
    /// impair et sous 10, l'extinction a 40. <c>+0x1a</c> est le <b>pourcentage</b> soigne : 100
    /// veut dire « au maximum », et c'est la seule valeur qui pose le bit 8 du porteur et qui
    /// autorise la gerbe.
    /// </para>
    /// <para>
    /// Le soin lui-meme n'a lieu qu'a l'image 1 : <c>+0x14c</c> et <c>+0x14e</c> du bloc de
    /// personnage recoivent le pourcentage de <c>+0x148</c> et <c>+0x14a</c>, bornes par eux. Ce
    /// bornage tranche le sens de la paire — <c>+0x148</c>/<c>+0x14a</c> sont bien le maximum.
    /// </para>
    /// <para>
    /// La division par 100 s'ecrit dans <c>Blaze.c</c> comme une somme de trois termes
    /// (<c>x / 100 + (x &gt;&gt; 0x1f) - ((longlong)x * 0x51eb851f &gt;&gt; 0x3f)</c>) : les deux
    /// derniers valent tous deux -1 pour un operande negatif et 0 sinon, donc ils s'annulent. Le
    /// binaire confirme l'idiome habituel (<c>imul 0x51EB851F ; sar edx,5 ; shr eax,31 ; add</c>),
    /// une division signee tronquee vers zero — c'est <c>/ 100</c> en C#.
    /// </para>
    /// </summary>
    private static void RestoreVitalsEntity_00403710(byte* effect)
    {
        byte* carrier = *(byte**)(effect + 0x6c);
        if (carrier == null) return;

        *(int*)(effect + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(effect + 0x20) = *(int*)(carrier + 0x20);
        *(int*)(effect + 0x24) = *(int*)(carrier + 0x24);
        *(int*)(effect + 0x60) = *(int*)(carrier + 0x60);

        if (*(short*)(effect + 0x10) == 0)
        {
            // Le binaire compare AVANT d'effacer : `cmp word [esi+18h],0FFFFh` precede
            // `mov [esi+18h],bx`, et le `je` qui suit lit encore ces drapeaux-la.
            short requested = *(short*)(effect + 0x18);
            *(short*)(effect + 0x18) = 0;
            if (requested != -1) *(short*)(effect + 0x1a) = 100;

            *(short*)(effect + 0x10) = 1;
            *(short*)(effect + 0x12) = 0;
            *(short*)(effect + 0x16) = 0;

            if (*(short*)(effect + 0x1a) == 100) *(uint*)carrier |= 8;
        }

        if (*(short*)(effect + 0x10) == 1)
        {
            SpawnStatusEffectEntity_0047AC40(effect, (int*)(effect + 0x1c), null, 0x3b, 0, -1);
        }

        if (*(short*)(effect + 0x16) == 0) Aud.PlaySoundEffect(0x30, 400, 0x1000, 0x1000);
        if (*(short*)(effect + 0x16) == 1) Aud.PlaySoundEffect(0x2f, 0x200, 0x600, 0x600);

        (*(short*)(effect + 0x16))++;

        if (*(short*)(effect + 0x16) < 10 && (*(ushort*)(effect + 0x16) & 1) != 0)
        {
            // Ordinal_451 part en +0x1c et Ordinal_450 en +0x24 — l'inverse de l'appariement de
            // MoveEntityAlongHeading. Le binaire est formel (0x004037df puis 0x004037fa), et les
            // deux fonctions sont transcrites telles qu'elles sont ecrites.
            short heading = *(short*)(effect + 0x12);
            *(int*)(effect + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 0x3c;   // Ordinal_451
            *(int*)(effect + 0x24) += (int)Winsys3d.GetCosTableEntry(heading) * 0x3c;   // Ordinal_450

            if (*(short*)(effect + 0x1a) == 100)
            {
                SpawnCastEntity_0047AAE0(effect, 0x59, 0, -1);
            }

            *(short*)(effect + 0x12) += (short)(CrtRand() % 400 + 800);   // Ghidra: FUN_00547140
        }

        if (*(short*)(effect + 0x16) >= 0x28)
        {
            *(uint*)effect &= 0x7fffffff;
            *(uint*)carrier &= 0xfffffff7;
        }

        if (*(short*)(effect + 0x16) == 1)
        {
            byte* block = GetMainMemPtr() + ((carrier[8] + 0x78) << 0xd);
            short percent = *(short*)(effect + 0x1a);

            short maxHp = *(short*)(block + 0x148);
            *(short*)(block + 0x14c) += (short)(percent * maxHp / 100);
            if (*(short*)(block + 0x14c) >= maxHp) *(short*)(block + 0x14c) = maxHp;

            short maxMp = *(short*)(block + 0x14a);
            *(short*)(block + 0x14e) += (short)(percent * maxMp / 100);
            if (*(short*)(block + 0x14e) >= maxMp) *(short*)(block + 0x14e) = maxMp;
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f158</c> — <b>un seul enregistrement</b>, la tuile <c>8 x 8</c> de
    /// l'origine de la planche, atlas des grains que
    /// <see cref="EntityHandler_004038F0"/> porte.
    ///
    /// <para>
    /// <b>Un, et pour une fois la borne ne se discute pas</b> :
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> pose <c>+0x40 = 0</c> sur toute entite qu'elle
    /// fait naitre, et <c>0x004038f0</c> n'y touche jamais. L'indice d'atlas reste donc zero pour
    /// la vie entiere de l'entite, et aucun autre enregistrement n'est atteignable.
    /// </para>
    /// <para>
    /// La donnee est d'ailleurs isolee : la plage qui la precede
    /// (<c>0x0054f138</c>..<c>0x0054f150</c>) est en <c>0x0f x 0x0f</c>, celle qui la suit
    /// (<c>0x0054f160</c>) en <c>0x18 x 0x20</c>, et le meme motif <c>00 00 08 08</c> reparait a
    /// <c>0x0054f180</c> pour clore cette seconde plage. C'est un <b>separateur</b> autant qu'un
    /// descripteur.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F158 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f158
    ];

    private static byte* s_Descriptors0054F158Storage;

    private static byte* Descriptors0054F158()
        => NativeCopy(s_Descriptors_0054F158, ref s_Descriptors0054F158Storage);

    /// <summary>
    /// Ghidra: FUN_004038f0 @ 0x004038f0 — <b>l'entree 59 (<c>0x3b</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Le grain qui monte quand on gagne un niveau</b>, ou quand on est soigne :
    /// <see cref="RestoreVitalsEntity_00403710"/> en seme un a chaque image de son etat 1, et
    /// c'est le meme effet que la montee de niveau declenche.
    ///
    /// <para>
    /// <b>Il naît sur un cercle et monte en s'accelerant.</b> Sa naissance fige l'ancre
    /// <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> sur sa position d'origine, se choisit un cap sur douze
    /// bits, s'ecarte de <c>0x50</c> fois le sinus, et repart du <b>plancher</b> <c>+0x60</c>. Le
    /// pas de montee <c>+0x14</c> commence a 10 et <b>gagne un par image</b> ; le cap tourne de 100
    /// par image. La trajectoire est donc une helice qui s'ouvre.
    /// </para>
    /// <para>
    /// <b>Son echelle est tiree deux fois dans la meme naissance</b>, et la seconde ecrase la
    /// premiere : <c>0xc00 + rand % 0x5dc</c> puis <c>0x1194 + rand % 0x400</c>. Le premier tirage
    /// ne sert a rien d'autre qu'a consommer un nombre de la suite pseudo-aleatoire — transcrit tel
    /// quel, c'est ce que fait le binaire et la suite est partagee.
    /// </para>
    /// <para>
    /// <b>La teinte depend du pourcentage soigne.</b> Si <c>+0x1a</c> — herite de l'effet parent —
    /// ne vaut pas 100, les trois canaux sont tires entre <c>-106</c> et <c>-7</c>, c'est-a-dire
    /// entre <c>0x96</c> et <c>0xf9</c> une fois lus comme octets. A 100, le grain garde la teinte
    /// heritee. Puis, passe la neuvieme image, il perd dix par canal et meurt quand les trois sont
    /// a zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_004038F0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F158();
            e[0x0a] = 6;
            e[0x0b] = 0;

            // Ce premier tirage d'echelle est ecrase quinze lignes plus bas. Il consomme un
            // nombre de la suite, et c'est son seul effet.
            short discarded = (short)(CrtRand() % 0x5dc + 0xc00);
            *(short*)(e + 0x28) = discarded;
            *(short*)(e + 0x2a) = discarded;

            short heading = (short)(CrtRand() % 0x1000);

            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(short*)(e + 0x12) = heading;
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(heading) * 0x50 + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(heading) * 0x50 + *(int*)(e + 0x58);
            *(int*)(e + 0x20) = *(int*)(e + 0x60);

            short scale = (short)(CrtRand() % 0x400 + 0x1194);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 10;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            if (*(short*)(e + 0x1a) != 100)
            {
                e[0x38] = (byte)(CrtRand() % 100 - 0x6a);
                e[0x39] = (byte)(CrtRand() % 100 - 0x6a);
                e[0x3a] = (byte)(CrtRand() % 100 - 0x6a);
            }
        }

        // La premiere image passe ici aussi : la naissance pose +0x10 = 1.
        if (*(short*)(e + 0x10) == 1)
        {
            short heading = *(short*)(e + 0x12);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(heading) * 0x50 + *(int*)(e + 0x50);

            // Le cosinus est lu AVANT que le cap n'avance : l'ecart d'une image entre X et Z est
            // dans le binaire.
            int cosine = Winsys3d.GetCosTableEntry(heading);
            *(short*)(e + 0x12) = (short)(heading + 100);
            *(short*)(e + 0x16) += 1;
            *(int*)(e + 0x24) = cosine * 0x50 + *(int*)(e + 0x58);

            *(int*)(e + 0x20) += *(short*)(e + 0x14) * 0x1000;
            *(short*)(e + 0x14) += 1;

            if (*(short*)(e + 0x16) > 8)
            {
                BrightenEntityTint_00404DE0(e, -10, -10, -10);

                if (e[0x39] == 0 && e[0x3a] == 0 && e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00403a90</c> — <b>l'entree 89 (<c>0x59</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>, 374 octets, <b>absente de
    /// <c>Blaze.c</c></b>. La gerbe que <see cref="RestoreVitalsEntity_00403710"/> lance par
    /// <see cref="SpawnCastEntity_0047AAE0"/> une image sur deux — donc l'autre moitie de ce qu'on
    /// voit en gagnant un niveau.
    ///
    /// <para>
    /// <b>Le meme numero <c>0x59</c> designe deux corps</b> : dans <c>0x00551c30</c> c'est
    /// <see cref="RestoreVitalsEntity_00403710"/>, l'effet lui-meme, et dans <c>0x00551f30</c> ce
    /// corps-ci. La montee de niveau lache le premier par <see cref="SpawnEffectEntity"/>, et le
    /// premier lache le second par <see cref="SpawnCastEntity_0047AAE0"/> : le pere et le fils
    /// portent le meme sous-type, seule la table de destination les distingue.
    /// </para>
    /// <para><b>Quatre etats, et l'horloge est la teinte</b> :</para>
    /// <list type="number">
    /// <item><b>0</b> — naissance : elle se pose sur le plancher <c>+0x60</c>, se tire deux
    /// echelles, un cap, un delai de 0 a 9, et <b>trois vitesses d'eclaircissement</b> — 4 pour le
    /// rouge, <c>6 + rand % 4</c> pour le vert et pour le bleu ;</item>
    /// <item><b>1</b> — attente : le delai <c>+0x12</c> descend ;</item>
    /// <item><b>2</b> — <b>elle s'allume</b> : <see cref="BrightenEntityTint_00404DE0"/> ajoute les
    /// trois vitesses a chaque image jusqu'a ce que le <b>vert</b> depasse <c>0x6e</c> ;</item>
    /// <item><b>3</b> — un second delai, de 5 a 14 ;</item>
    /// <item><b>4</b> — <b>elle s'eteint</b> : <c>-6</c>, <c>-3</c>, <c>-6</c> par image, et sous
    /// <c>0x3c</c> de vert elle <b>grossit</b> de 40 en s'eteignant. Elle meurt quand le vert
    /// atteint zero.</item>
    /// </list>
    /// <para>
    /// <b>Le vert seul mene tout</b> — les deux seuils et la mort le lisent, jamais les deux autres
    /// canaux. C'est pour cela que sa vitesse est tiree au hasard et celle du rouge fixee a 4 : la
    /// duree varie d'un grain a l'autre, mais chacun garde sa propre couleur.
    /// </para>
    /// </summary>
    private static void EntityHandler_00403A90(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[4] = 0;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x20) = *(int*)(e + 0x60);

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x7d0 + 0x190);
            *(short*)(e + 0x2a) = 0x6d60;
            *(short*)(e + 0x2c) = (short)(CrtRand() % 0x3e8 + 0x190);
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);

            *(short*)(e + 0x36) |= 4;

            int delay = CrtRand() % 10;
            *(short*)(e + 0x16) = 0;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(int*)(e + 0x50) = 4;
            *(short*)(e + 0x12) = (short)delay;

            *(int*)(e + 0x54) = CrtRand() % 4 + 6;
            *(int*)(e + 0x58) = CrtRand() % 4 + 6;
        }

        // Ghidra: LAB_00403b5d
        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) <= 0) *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_00403b77
        if (*(short*)(e + 0x10) == 2)
        {
            BrightenEntityTint_00404DE0(
                e, (short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58));

            if (e[0x39] > 0x6e) *(short*)(e + 0x10) = 3;
        }

        // Ghidra: LAB_00403ba2
        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x10) = 4;
            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 5);
        }

        // Ghidra: LAB_00403bc1
        if (*(short*)(e + 0x10) == 4)
        {
            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) >= 0) return;

            *(short*)(e + 0x12) = 0;
            BrightenEntityTint_00404DE0(e, -6, -3, -6);

            byte green = e[0x39];

            if (green < 0x3c)
            {
                *(short*)(e + 0x28) += 0x28;
                *(short*)(e + 0x2c) += 0x28;
            }

            if (green == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00402fc0</c> — <b>l'entree 84 de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 500 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; transcrite au desassembleur, releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00402FC0.asm</c>.
    ///
    /// <para>
    /// <b>C'est un effet de sort porte par sa cible.</b> Le sous-type <c>0x54</c> ne se trouve nulle
    /// part en dur dans le binaire : il sort du descripteur d'effet que
    /// <see cref="CastSpell_0053A980"/> lit dans le repertoire de zone (<c>+0x9c</c>, pas de 0x30,
    /// octet <c>+0x10</c>). L'effet vit dans la table <c>0x005ff400</c> et garde sa victime en
    /// <c>+0x6c</c> ; si elle a disparu, il ne fait rien.
    /// </para>
    /// <para>
    /// <b>Ce qu'il fait a la victime</b> : il la colle a sa propre position, lui impose l'animation
    /// <b>2</b>, pose les bits <c>0x1008</c> de son mot d'etat — dont le bit 3, celui que
    /// <see cref="RestoreVitalsEntity_00403710"/> emploie aussi pour dire « tenue par un effet » —
    /// et <b>fait avancer son image d'animation lui-meme, une image sur deux</b>, en la bornant au
    /// nombre d'images de la sequence. La victime joue donc son animation 2 au ralenti pendant que
    /// l'effet dure, puis le bit 3 est rendu.
    /// </para>
    /// <para>
    /// <b>Ce qu'il montre</b> : quatre entites d'etat <c>0x38</c> et six entites de sort <c>0x5a</c>
    /// a la premiere image, puis deux <c>0x38</c> de plus une image sur deux, et le son <c>0x25</c>
    /// par la porte de distance de <see cref="TryPlaySoundNearEntity"/>. La duree est tiree entre
    /// 15 et 19 images.
    /// </para>
    /// <para>
    /// <c>+0x14</c> recoit la demi-somme des trois etendues du bloc de <b>modele</b> de la victime
    /// (<c>+0x1c</c>, <c>+0x1e</c>, <c>+0x20</c>) — la taille de la bete, que les entites filles
    /// relisent pour se repartir autour d'elle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00402FC0(byte* effect)
    {
        byte* victim = *(byte**)(effect + 0x6c);
        if (victim == null) return;

        *(int*)(effect + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(effect + 0x20) = *(int*)(victim + 0x20);
        *(int*)(effect + 0x24) = *(int*)(victim + 0x24);

        // La sequence dont l'effet fera defiler les images a la main. L'indice 2 est litteral
        // dans l'instruction (`mov edx,[ecx+8]`), donc c'est bien celle qu'on vient de forcer.
        *(short*)(victim + 0x18) = 2;
        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + victim[4] * 4))[2];

        *(uint*)victim |= 0x1008;

        if (*(short*)(effect + 0x10) == 0)
        {
            *(short*)(effect + 0x10) = 1;
            *(short*)(effect + 0x12) = (short)(CrtRand() % 5 + 0x0f);

            victim[0x0a] = 0;
            *(short*)(victim + 0x1a) = 0;

            ResolveEntityBlocks(victim, out _, out _, out byte* model);

            // Ghidra: `(a + b + c) - ((a + b + c) >> 31) >> 1`, la division par deux tronquee vers
            // zero de l'idiome MSVC.
            int extent = *(short*)(model + 0x1e) + *(short*)(model + 0x1c) + *(short*)(model + 0x20);
            *(short*)(effect + 0x14) = (short)(extent / 2);
            *(short*)(effect + 0x16) = 0;

            for (int i = 0; i < 4; i++)
            {
                SpawnStatusEffectEntity_0047AC40(effect, (int*)(effect + 0x1c), null, 0x38, 0, -1);
            }

            for (int i = 0; i < 6; i++)
            {
                SpawnCastEntity_0047AAE0(effect, 0x5a, 0, -1);
            }

            TryPlaySoundNearEntity((uint*)effect, 0x25, 0x400, 0xa00, 0, 0);
        }

        // Ghidra: LAB_00403132 — la premiere image passe ici aussi, +0x10 venant d'etre pose a 1.
        if (*(short*)(effect + 0x10) == 1)
        {
            byte odd = effect[0x16];
            *(short*)(effect + 0x12) -= 1;

            if ((odd & 1) != 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    SpawnStatusEffectEntity_0047AC40(
                        effect, (int*)(effect + 0x1c), null, 0x38, 0, -1);
                }
            }

            // Ghidra: LAB_00403168 — la duree est epuisee : l'effet s'eteint et rend son bit a la
            // victime. C'est la meme sortie que RestoreVitalsEntity_00403710.
            if (*(short*)(effect + 0x12) < 0)
            {
                *(uint*)effect &= 0x7fffffff;
                *(uint*)victim &= 0xfffffff7;
            }
        }

        // Ghidra: LAB_0040317f — une image d'animation sur deux, bornee a la derniere de la
        // sequence. Le binaire compare l'image SIGNEE au compte NON signe, et decremente le compte
        // sur 32 bits pour n'en reecrire que les seize bas.
        if ((effect[0x16] & 1) != 0)
        {
            *(short*)(victim + 0x1a) += 1;

            ushort frames = *(ushort*)(animation + 0x16);
            if (*(short*)(victim + 0x1a) >= (int)frames)
            {
                *(ushort*)(victim + 0x1a) = (ushort)(frames - 1);
            }
        }

        *(ushort*)(effect + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x004041c0</c> — <b>l'entree 82 de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 330 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; transcrite au desassembleur, releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004041C0.asm</c>. Signalee en jeu le 2026-08-29, en frappant.
    ///
    /// <para>
    /// <b>La sœur sombre de <see cref="EntityHandler_00402FC0"/></b> (entree 84 de la meme table) :
    /// meme squelette — un effet de sort qui tient sa victime par <c>+0x6c</c>, la colle a sa
    /// position, pose les bits <c>0x1008</c> de son mot d'etat, tire sa duree entre 15 et 19
    /// images et fait defiler l'animation de la victime a la main une image sur deux —, mais trois
    /// differences, toutes lues dans le desassemblage :
    /// </para>
    /// <list type="number">
    /// <item><b>Elle noircit la victime</b> au lieu de semer des etincelles autour d'elle :
    /// <see cref="BrightenEntityTint_00404DE0"/> avec <c>-15</c> sur les trois composantes, a
    /// <b>chaque</b> image.</item>
    /// <item><b>Une seule entite d'etat <c>0x34</c> par image</b>, et non quatre puis deux — et des
    /// la premiere image, l'initialisation ne sautant pas par-dessus.</item>
    /// <item><c>+0x14</c> recoit la <b>moyenne</b> des trois etendues du bloc de modele
    /// (<c>imul 55555556h</c>, soit une division par trois), la ou la sœur en prend la
    /// demi-somme.</item>
    /// </list>
    /// <para>
    /// <b>Le piege du pointeur d'animation.</b> Il est resolu <b>avant</b> l'initialisation, donc a
    /// travers <c>*(short*)(victim + 0x18)</c> — l'animation que la victime jouait deja — la ou la
    /// sœur le resout sur l'indice 2 litteral. L'initialisation ecrit ensuite <c>+0x18</c> et
    /// <c>+0x1a</c> d'un seul <c>mov dword</c> : animation 2, image 0. La premiere image borne donc
    /// le compteur sur le nombre d'images de l'<b>ancienne</b> sequence, les suivantes sur celui de
    /// la sequence 2. C'est ecrit ainsi dans le binaire, ce n'est pas une reconstruction.
    /// </para>
    /// <para>
    /// Le son <c>0x1f</c> part par <c>FUN_004031c0</c>, <b>jumeau</b> de
    /// <see cref="TryPlaySoundNearEntity"/> (<c>FUN_00468080</c>) : les deux corps ne different que
    /// par l'ordonnancement des registres — memes arguments 2/3/4 relus apres trois <c>push</c>,
    /// meme gain double, meme <c>FUN_0053db20</c> au meme rayon de 2000. Le compilateur a emis la
    /// meme fonction deux fois ; le releve est dans REVERSE_STATUS.md, suite 5 du 2026-08-29.
    /// </para>
    /// <para>
    /// <b>Le sous-type 0x52 est aussi celui de la trajectoire du jet de Goblin</b>
    /// (<see cref="EntityHandler_0047A270"/>), et ce ne sont pas les memes corps : les deux
    /// passes ne resolvent pas dans la meme table de sauts. <c>0x00551f30</c>[82] donne
    /// <c>0x0047A270</c>, <c>0x00551c30</c>[82] donne celui-ci. Le numero seul aurait fait
    /// conclure « deja fait ».
    /// </para>
    /// </summary>
    private static void EntityHandler_004041C0(byte* effect)
    {
        byte* victim = *(byte**)(effect + 0x6c);
        if (victim == null) return;

        *(int*)(effect + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(effect + 0x20) = *(int*)(victim + 0x20);
        *(int*)(effect + 0x24) = *(int*)(victim + 0x24);

        // Resolu AVANT que l'initialisation n'impose la sequence 2 — voir le commentaire de tete.
        byte* animation =
            ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + victim[4] * 4))
                [*(short*)(victim + 0x18)];

        *(uint*)victim |= 0x1008;

        if (*(short*)(effect + 0x10) == 0)
        {
            *(short*)(effect + 0x10) = 1;
            *(short*)(effect + 0x12) = (short)(CrtRand() % 5 + 0x0f);   // Ghidra: FUN_00547140

            victim[0x0a] = 0;

            // `mov dword [esi+18h],2` : la sequence 2 et l'image 0 en une seule ecriture.
            *(int*)(victim + 0x18) = 2;

            ResolveEntityBlocks(victim, out _, out _, out byte* model);

            // Ghidra: `imul 55555556h` puis le rattrapage par le bit de signe — la division par
            // trois tronquee vers zero de l'idiome MSVC.
            int extent = *(short*)(model + 0x1c) + *(short*)(model + 0x1e) + *(short*)(model + 0x20);
            *(short*)(effect + 0x14) = (short)(extent / 3);
            *(short*)(effect + 0x16) = 0;

            // Ghidra: FUN_004031c0 — jumeau de TryPlaySoundNearEntity, voir le commentaire de tete.
            TryPlaySoundNearEntity((uint*)effect, 0x1f, 0x400, 0xa00, 0, 0);
        }

        // Ghidra: LAB_00404298 — la premiere image passe ici aussi : la branche d'initialisation
        // tombe dedans, il n'y a pas de saut par-dessus.
        *(short*)(effect + 0x12) -= 1;

        SpawnStatusEffectEntity_0047AC40(effect, (int*)(effect + 0x1c), null, 0x34, 0, -1);

        // La duree est epuisee : l'effet s'eteint et rend son bit 3 a la victime. Meme sortie que
        // EntityHandler_00402FC0 et RestoreVitalsEntity_00403710.
        if (*(short*)(effect + 0x12) < 0)
        {
            *(uint*)effect &= 0x7fffffff;
            *(uint*)victim &= 0xfffffff7;
        }

        // Ghidra: LAB_004042c5 — la victime s'assombrit de quinze par image sur les trois
        // composantes. BrightenEntityTint_00404DE0 borne a 0, donc elle finit noire et y reste.
        BrightenEntityTint_00404DE0(victim, -15, -15, -15);

        // Ghidra: LAB_004042ff — meme borne que la sœur : l'image SIGNEE comparee au compte NON
        // signe, decremente sur 32 bits et reecrit sur seize.
        if ((effect[0x16] & 1) != 0)
        {
            *(short*)(victim + 0x1a) += 1;

            ushort frames = *(ushort*)(animation + 0x16);
            if (*(short*)(victim + 0x1a) >= (int)frames)
            {
                *(ushort*)(victim + 0x1a) = (ushort)(frames - 1);
            }
        }

        *(ushort*)(effect + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f0e0</c> — <b>l'atlas de la retombee sombre</b>, sept enregistrements
    /// de huit octets que <see cref="EntityHandler_00404310"/> pose en <c>+0x3c</c>. Meme forme que
    /// <see cref="s_DustDescriptors_0054F118"/>, qui suit immediatement en memoire.
    ///
    /// <para>
    /// <b>Sept, et le corps le dit</b> : le compteur <c>+0x40</c> s'incremente et l'entite s'eteint
    /// quand il atteint 7 — la table s'arrete donc exactement ou commence celle de la poussiere,
    /// a <c>0x0054f118</c>. Les deux premieres images pointent une planche
    /// (<c>0x1717b8c0</c>, <c>0x1717b8d8</c>), les cinq suivantes une autre
    /// (<c>0x1717d060</c> a <c>0x1717d0c0</c>, de 0x18 en 0x18).
    /// </para>
    /// </summary>
    private static readonly byte[] s_Entity34Descriptors_0054F0E0 =
    [
        0xc0, 0xb8, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x0054f0e0
        0xd8, 0xb8, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x0054f0e8
        0x60, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x0054f0f0
        0x78, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x0054f0f8
        0x90, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x0054f100
        0xa8, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x0054f108
        0xc0, 0xd0, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x0054f110
    ];

    private static byte* s_Entity34DescriptorStorage;

    /// <summary>Les sept descripteurs en memoire native : l'entite les garde par pointeur
    /// (<c>+0x3c</c>) et les parcourt par son indice <c>+0x40</c>.</summary>
    private static byte* Entity34Descriptors()
    {
        if (s_Entity34DescriptorStorage == null)
        {
            s_Entity34DescriptorStorage =
                (byte*)NativeMemory.Alloc((nuint)s_Entity34Descriptors_0054F0E0.Length);
            for (int i = 0; i < s_Entity34Descriptors_0054F0E0.Length; i++)
            {
                s_Entity34DescriptorStorage[i] = s_Entity34Descriptors_0054F0E0[i];
            }
        }

        return s_Entity34DescriptorStorage;
    }

    /// <summary>
    /// Ghidra: <c>0x00404310</c> — <b>l'entree 52 (<c>0x34</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, celle de la passe
    /// <see cref="TickEntityTable_005f2d40"/>. 322 octets, <b>absente de <c>Blaze.c</c></b> ;
    /// releve garde sous <c>Legacy/Raw/Blaze/FUN_00404310.asm</c>.
    ///
    /// <para>
    /// C'est <b>la retombee que <see cref="EntityHandler_004041C0"/> lache par une a chaque
    /// image</b> — 3272 appels pour une seule sortie en foret, la plus sollicitee du relevé du
    /// 2026-08-29. Une bouffee qui nait autour de la victime, monte et s'eteint.
    /// </para>
    /// <para>
    /// <b>La naissance</b> se disperse autour du point donne, en lisant l'amplitude <c>+0x14</c> que
    /// le pere y a posee (la moyenne des trois etendues du modele de la victime) : <c>X</c> et
    /// <c>Z</c> sont tires dans <c>[-taille/4, taille/4[</c>, mais <c>Y</c> seulement dans
    /// <c>[0, taille/2[</c> — la bouffee nait <b>au-dessus</b> du point, jamais en dessous. Teinte
    /// de depart <c>0xa0</c> sur les trois composantes, echelle <c>0xbb8</c>.
    /// </para>
    /// <para>
    /// <b>La vie</b> tient en trois gestes par image : elle s'assombrit de deux
    /// (<see cref="BrightenEntityTint_00404DE0"/>), elle monte de <c>0x8000</c>, et tous les
    /// <c>+0x16</c> images — 2 ou 3, tire a la naissance — elle avance d'une image d'atlas. A la
    /// septieme elle s'eteint. Chaque bouffee vit donc 14 a 21 images.
    /// </para>
    /// <para>
    /// <b>Les divisions sont tronquees vers zero</b> (<c>sar</c> precede du rattrapage par le bit
    /// de signe), ce que <c>/</c> fait aussi en C#. Une amplitude inferieure a 2 ferait diviser par
    /// zero — le binaire aussi ; c'est transcrit tel quel plutot que garde.
    /// </para>
    /// </summary>
    private static void EntityHandler_00404310(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            // Le binaire relit `word [esi+14h]` avant chaque tirage ; rien ne l'ecrit entre-temps.
            int size = *(short*)(e + 0x14);

            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Entity34Descriptors();   // Ghidra: &DAT_0054f0e0
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % (size / 2) - size / 4) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % (size / 2) - size / 4) << 0xc;

            *(short*)(e + 0x12) = 0;

            // Pas de recentrage sur Y : la bouffee nait au-dessus du point, jamais en dessous.
            *(int*)(e + 0x20) += (CrtRand() % (size / 2)) << 0xc;

            *(short*)(e + 0x28) = 0xbb8;
            *(short*)(e + 0x2a) = 0xbb8;
            e[0x38] = 0xa0;
            e[0x39] = 0xa0;
            e[0x3a] = 0xa0;

            // Ghidra: l'idiome MSVC du modulo signe, puis +2 — donc 2 ou 3 images par pas.
            *(short*)(e + 0x16) = (short)(CrtRand() % 2 + 2);
        }

        // Ghidra: LAB_00404406 — la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, -2, -2, -2);

            *(short*)(e + 0x12) += 1;
            *(int*)(e + 0x20) += 0x8000;

            if (*(short*)(e + 0x12) >= *(short*)(e + 0x16))
            {
                *(short*)(e + 0x12) = 0;

                // Le binaire incremente PUIS compare a 7 : la septieme image d'atlas n'est jamais
                // affichee, elle sert de borne.
                e[0x40]++;
                if (e[0x40] == 7) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00403c10</c> — <b>l'entree 80 (<c>0x50</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, celle de la passe
    /// <see cref="TickEntityTable_005ff400"/>. 324 octets, <b>absente de <c>Blaze.c</c></b> ;
    /// releve garde sous <c>Legacy/Raw/Blaze/FUN_00403C10.asm</c>.
    ///
    /// <para>
    /// C'est le gestionnaire que <b>tous</b> les relevés du 2026-08-29 rencontrent, banc ou partie
    /// ordinaire — 1203 appels pour une seule sortie en foret. Il <b>suit</b> une cible sans la
    /// tenir : il se recopie sa position a chaque image mais ne touche pas a son mot d'etat, la ou
    /// <see cref="EntityHandler_004041C0"/> et <see cref="EntityHandler_00402FC0"/> posent
    /// <c>0x1008</c> dessus.
    /// </para>
    /// <para>
    /// <b>Il mesure sa cible selon son genre</b>, le champ <c>&amp; 0xfc0000</c> du mot d'etat que
    /// tout le projet lit deja :
    /// </para>
    /// <list type="bullet">
    /// <item><c>0x800000</c>, un <b>membre du groupe</b> : rayon fixe a <c>150</c>, sans mesurer —
    /// les personnages ont tous le meme gabarit ;</item>
    /// <item><c>0x400000</c> ou <c>0x600000</c>, une <b>entite de scene</b> : la moyenne des trois
    /// etendues de son bloc de modele, avec un plancher a <c>5</c> ;</item>
    /// <item><b>tout autre genre</b> : <c>+0x14</c> reste a <c>-1</c> et l'effet <b>s'eteint
    /// immediatement</b>. C'est la seule sortie qui ne consomme aucune image.</item>
    /// </list>
    /// <para>
    /// <b>Deux phases</b>, portees par <c>+0x10</c>. En phase 1 il seme une entite <c>0x32</c> par
    /// image pendant <c>+0x18 / 30 + 2</c> images, borne a 15 ; puis il passe en phase 2, qui
    /// n'est qu'une attente de 30 images avant l'extinction — le temps que les <c>0x32</c> deja
    /// nees finissent leur course.
    /// </para>
    /// <para>
    /// <c>+0x18</c> est le parametre que le lanceur range dans l'effet, le meme champ que
    /// <see cref="RestoreVitalsEntity_00403710"/> relit a sa naissance. La division est un
    /// <c>imul 88888889h</c> suivi d'un <c>add</c> et d'un <c>sar 4</c> : c'est le magique de la
    /// division par <b>30</b>, pas par 15 — verifie par le calcul (2^36 / 30 = 0x88888889).
    /// </para>
    /// </summary>
    private static void EntityHandler_00403C10(byte* e)
    {
        byte* target = *(byte**)(e + 0x6c);
        if (target == null) return;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            short steps = (short)(*(short*)(e + 0x18) / 30 + 2);
            *(short*)(e + 0x12) = steps;
            if (steps >= 0x10) *(short*)(e + 0x12) = 0x0f;

            // -1 veut dire « pas encore mesure » : c'est ce que teste la sortie plus bas.
            *(short*)(e + 0x14) = -1;

            uint kind = *(uint*)target & 0xfc0000;

            if (kind == 0x800000) *(short*)(e + 0x14) = 0x96;

            if (kind == 0x400000 || kind == 0x600000)
            {
                ResolveEntityBlocks(target, out _, out _, out byte* model);

                // Ghidra: `imul 55555556h` puis le rattrapage par le bit de signe — la moyenne des
                // trois etendues, comme dans EntityHandler_004041C0.
                int extent =
                    *(short*)(model + 0x1c) + *(short*)(model + 0x1e) + *(short*)(model + 0x20);
                short radius = (short)(extent / 3);
                *(short*)(e + 0x14) = radius;
                if (radius <= 4) *(short*)(e + 0x14) = 5;
            }

            // Aucun genre reconnu : l'effet n'a rien a entourer, il s'eteint sans jouer.
            if (*(short*)(e + 0x14) == -1) *(uint*)e &= 0x7fffffff;

            *(short*)(e + 0x16) = *(short*)(e + 0x1a);
        }

        // Ghidra: LAB_00403d03 — la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) -= 1;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x32, 0, -1);

            if (*(short*)(e + 0x12) < 0)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0x1e;
            }
        }

        // Ghidra: LAB_00403d36 — l'attente finale. Le binaire teste `jg`, donc l'extinction tombe
        // a zero et non a -1, contrairement a la phase precedente.
        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00403d60</c> — <b>l'entree 50 (<c>0x32</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, celle de la passe
    /// <see cref="TickEntityTable_005f2d40"/>. 277 octets, <b>absente de <c>Blaze.c</c></b> ;
    /// releve garde sous <c>Legacy/Raw/Blaze/FUN_00403D60.asm</c>.
    ///
    /// <para>
    /// C'est ce que <see cref="EntityHandler_00403C10"/> seme, une par image. Elle partage
    /// l'atlas <see cref="s_Entity34Descriptors_0054F0E0"/> et le compteur a sept de
    /// <see cref="EntityHandler_00404310"/> — meme planche, meme mort —, mais elle <b>ne monte
    /// pas</b> : elle nait dispersee et s'assombrit sur place.
    /// </para>
    /// <para>
    /// <b>Ce qui la distingue de sa jumelle</b>, point par point : teinte de depart <c>0x80</c> et
    /// non <c>0xa0</c> ; assombrissement de <b>quatre</b> par image et non de deux ; echelle tiree
    /// (<c>2500 + rand % 1024</c>) au lieu d'etre fixee a <c>0xbb8</c> ; pas d'image sur deux ou
    /// trois mais sur <b>une ou deux</b> — elle vit donc 7 a 14 images, moitie moins ; et
    /// <c>+0x0a</c>, sa couche de dessin, vaut <b>celle de son pere plus quatre</b> au lieu d'une
    /// constante, ce qui la met devant lui.
    /// </para>
    /// <para>
    /// <b>Les decalages ne sont pas calcules pareil non plus.</b> Ici ce sont des <c>sar</c> nus —
    /// <c>taille &gt;&gt; 1</c> et <c>taille &gt;&gt; 2</c>, arrondis vers moins l'infini — la ou
    /// <see cref="EntityHandler_00404310"/> emploie l'idiome tronque vers zero. Sur une taille
    /// positive les deux coincident ; la difference est transcrite parce qu'elle est ecrite.
    /// Et <c>Y</c> est tire sur la taille <b>entiere</b>, non sur la moitie : la bouffee de ce
    /// gestionnaire monte deux fois plus haut que celle de sa jumelle.
    /// </para>
    /// </summary>
    private static void EntityHandler_00403D60(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            // Lu AVANT que +0x16 ne soit reecrit plus bas : c'est la valeur heritee du pere.
            byte layer = (byte)(e[0x16] + 4);

            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Entity34Descriptors();   // Ghidra: &DAT_0054f0e0
            e[0x40] = 0;
            e[0x0b] = 1;
            e[0x0a] = layer;

            short scale = (short)(CrtRand() % 0x400 + 0x9c4);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            // `sar` nus, sans le rattrapage par le bit de signe — voir le commentaire de tete.
            int size = *(short*)(e + 0x14);
            *(int*)(e + 0x1c) += (CrtRand() % (size >> 1) - (size >> 2)) << 0xc;

            size = *(short*)(e + 0x14);
            *(int*)(e + 0x24) += (CrtRand() % (size >> 1) - (size >> 2)) << 0xc;

            // Y : la taille entiere, et pas de recentrage.
            *(int*)(e + 0x20) += (CrtRand() % *(short*)(e + 0x14)) << 0xc;

            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x16) = (short)(CrtRand() % 2 + 1);

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;
            e[0x3b] &= 0xfd;
        }

        // Ghidra: LAB_00403e34 — la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, -4, -4, -4);

            *(short*)(e + 0x12) += 1;

            if (*(short*)(e + 0x12) >= *(short*)(e + 0x16))
            {
                *(short*)(e + 0x12) = 0;

                e[0x40]++;
                if (e[0x40] == 7) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00403e80</c> — <b>l'entree 81 (<c>0x51</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 380 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00403E80.asm</c>.
    ///
    /// <para>
    /// <b>La troisieme sœur de la fratrie 80-85</b>, le bloc contigu d'effets de sort portes par
    /// leur victime : <see cref="EntityHandler_00403C10"/> (80), celui-ci (81),
    /// <see cref="EntityHandler_004041C0"/> (82), <see cref="EntityHandler_00404460"/> (83),
    /// <see cref="EntityHandler_00402FC0"/> (84) et <see cref="EntityHandler_00404920"/> (85).
    /// Meme squelette que la 82 : la victime en <c>+0x6c</c>, l'effet colle a sa position, les bits
    /// <c>0x1008</c> poses sur son mot d'etat, une duree tiree entre 15 et 19 images, et l'image
    /// d'animation de la victime avancee a la main une image sur deux.
    /// </para>
    /// <para>
    /// <b>Cinq differences avec la 82</b>, toutes lues dans le desassemblage :
    /// </para>
    /// <list type="number">
    /// <item><b>La sequence 2 est reimposee a CHAQUE image</b> (<c>mov word [edi+18h],2</c> dans le
    /// prologue) la ou la 82 l'ecrit une seule fois, a l'initialisation. Le pointeur d'animation est
    /// donc resolu sur l'indice <b>2 litteral</b> (<c>[ecx+8]</c>) et non a travers <c>+0x18</c> :
    /// le piege de la sœur n'existe pas ici.</item>
    /// <item><b>Deux sons, un par image</b>, et non un seul a l'initialisation : <c>0x21</c> a
    /// l'image 0, <c>0x23</c> a l'image 1, tous deux par la porte de distance de
    /// <see cref="TryPlaySoundNearEntity"/>.</item>
    /// <item><b>Une entite d'etat une image sur deux</b> (<c>+0x16</c> impair), sous-type
    /// <c>0x33</c> — <see cref="EntityHandler_00404000"/> — et non un <c>0x34</c> a chaque
    /// image.</item>
    /// <item><b>Elle noircit la victime de dix</b> par image, et non de quinze.</item>
    /// <item><b>Le corps est garde par <c>+0x10 == 1</c></b>, la ou la 82 laisse l'initialisation
    /// tomber dans la suite sans garde. La garde ne change rien a la premiere image :
    /// l'initialisation vient de poser <c>+0x10 = 1</c>.</item>
    /// </list>
    /// <para>
    /// <c>+0x14</c> recoit la <b>moyenne</b> des trois etendues du bloc de modele de la victime
    /// (<c>imul 55555556h</c>, la division par trois tronquee vers zero de MSVC) : c'est le germe
    /// que l'entite <c>0x33</c> relit pour se dimensionner.
    /// </para>
    /// </summary>
    private static void EntityHandler_00403E80(byte* effect)
    {
        byte* victim = *(byte**)(effect + 0x6c);
        if (victim == null) return;

        *(int*)(effect + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(effect + 0x20) = *(int*)(victim + 0x20);
        *(int*)(effect + 0x24) = *(int*)(victim + 0x24);

        // La sequence 2, reimposee a chaque image — voir le commentaire de tete.
        *(short*)(victim + 0x18) = 2;

        // `mov ecx,[eax*4+5E9640h] ; mov edx,[ecx+8]` : l'indice 2 en clair, pas +0x18.
        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + victim[4] * 4))[2];

        *(uint*)victim |= 0x1008;

        if (*(short*)(effect + 0x10) == 0)
        {
            *(short*)(effect + 0x10) = 1;
            *(short*)(effect + 0x12) = (short)(CrtRand() % 5 + 0x0f);   // Ghidra: FUN_00547140

            victim[0x0a] = 0;
            *(short*)(victim + 0x1a) = 0;

            ResolveEntityBlocks(victim, out _, out _, out byte* model);

            *(short*)(effect + 0x16) = 0;

            // Ghidra: `imul 55555556h` puis le rattrapage par le bit de signe — la division par
            // trois tronquee vers zero de l'idiome MSVC.
            int extent = *(short*)(model + 0x1c) + *(short*)(model + 0x1e) + *(short*)(model + 0x20);
            *(short*)(effect + 0x14) = (short)(extent / 3);
        }

        // Ghidra: LAB_00403f3c / LAB_00403f5c — les deux sons vivent HORS de l'initialisation :
        // c'est +0x16, le compteur d'images, qui les date. Ils partent aux images 0 et 1.
        if (*(short*)(effect + 0x16) == 0)
        {
            TryPlaySoundNearEntity((uint*)effect, 0x21, 0x329, 0x1000, 0, 0);
        }

        if (*(short*)(effect + 0x16) == 1)
        {
            TryPlaySoundNearEntity((uint*)effect, 0x23, 0x400, 0xa00, 0, 0);
        }

        // Ghidra: LAB_00403f7c — la premiere image passe ici aussi.
        if (*(short*)(effect + 0x10) == 1)
        {
            // L'octet bas de +0x16, lu AVANT le decompte : une image sur deux.
            bool odd = (effect[0x16] & 1) != 0;

            *(short*)(effect + 0x12) -= 1;

            if (odd)
            {
                SpawnStatusEffectEntity_0047AC40(effect, (int*)(effect + 0x1c), null, 0x33, 0, -1);
            }

            // La duree est epuisee : l'effet s'eteint et rend son bit 3 a la victime. Meme sortie
            // que EntityHandler_004041C0 et RestoreVitalsEntity_00403710.
            if (*(short*)(effect + 0x12) < 0)
            {
                *(uint*)effect &= 0x7fffffff;
                *(uint*)victim &= 0xfffffff7;
            }
        }

        // Ghidra: LAB_00403fb7 — hors de la garde precedente : la victime s'assombrit de dix par
        // image sur les trois composantes, meme apres l'extinction de l'effet.
        BrightenEntityTint_00404DE0(victim, -10, -10, -10);

        // Ghidra: LAB_00403fc3 — meme borne que la sœur : l'image SIGNEE comparee au compte NON
        // signe, decremente sur 32 bits et reecrit sur seize.
        if ((effect[0x16] & 1) != 0)
        {
            *(short*)(victim + 0x1a) += 1;

            ushort frames = *(ushort*)(animation + 0x16);
            if (*(short*)(victim + 0x1a) >= (int)frames)
            {
                *(ushort*)(victim + 0x1a) = (ushort)(frames - 1);
            }
        }

        *(ushort*)(effect + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x00404000</c> — <b>l'entree 51 (<c>0x33</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, celle de la passe
    /// <see cref="TickEntityTable_005f2d40"/>. 440 octets, <b>absente de <c>Blaze.c</c></b> ;
    /// releve garde sous <c>Legacy/Raw/Blaze/FUN_00404000.asm</c>.
    ///
    /// <para>
    /// C'est ce que <see cref="EntityHandler_00403E80"/> lache une image sur deux — le pendant
    /// exact de <see cref="EntityHandler_00404310"/> pour la sœur 82. Elle en partage d'ailleurs
    /// l'atlas, <see cref="s_Entity34Descriptors_0054F0E0"/>, et le compteur a sept.
    /// </para>
    /// <para>
    /// <b>Ce qu'elle fait</b> : elle nait sur le point donne, monte de plus en plus vite, enfle, et
    /// s'eteint au bout de 25 a 39 images. La montee est un <b>cumul</b> — <c>+0x54</c> part a 1 ou
    /// 2, <c>Y</c> gagne <c>+0x54 &lt;&lt; 10</c> par image, et <c>+0x54</c> gagne lui-meme
    /// <c>germe / 120 + 1</c> — donc une acceleration constante et non une vitesse. L'echelle,
    /// tiree entre <c>0xc00</c> et <c>0xfff</c>, gagne <c>germe + 0x50</c> par image sur les deux
    /// axes. Le germe est le <c>+0x14</c> que le pere a pose : la moyenne des etendues du modele de
    /// la victime.
    /// </para>
    /// <para>
    /// <b>La teinte</b> part verdatre-bleutee — <c>0x20</c>/<c>0x3c</c>/<c>0x40</c> plus un tirage
    /// sur 25 — et perd <c>2</c>/<c>1</c>/<c>2</c> par image.
    /// </para>
    /// <para>
    /// <b>Le tour de manege ne tourne pas.</b> Les deux appels aux tables de sinus et de cosinus de
    /// winsys3d sont bien la, l'angle <c>+0x12</c> part d'un tirage sur <c>0x1000</c> et gagne
    /// <c>0x64</c> par image — mais le rayon, <c>+0x98</c>, est mis a zero a la naissance et
    /// <b>rien ne le rouvre</b>. Le produit est donc nul et la position retombe exactement sur
    /// l'ancre <c>+0x50</c>/<c>+0x58</c> figee a la naissance. C'est ecrit ainsi dans le binaire :
    /// transcrit tel quel, pas repare.
    /// </para>
    /// <para>
    /// <b>L'atlas defile a chaque image</b>, pour la meme raison : le seuil compare a <c>+0x9a</c>
    /// est <c>+0x16</c>, mis a zero a la naissance et jamais reecrit. L'entite parcourt donc ses
    /// sept images en sept images, puis <b>reboucle</b> — le compteur revient a zero au lieu
    /// d'eteindre l'entite, contrairement a sa jumelle. Seul <c>+0x5c</c> la tue.
    /// </para>
    /// </summary>
    private static void EntityHandler_00404000(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Entity34Descriptors();   // Ghidra: &DAT_0054f0e0
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;

            *(short*)(e + 0x98) = 0;                       // le rayon — voir le commentaire de tete
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);

            // L'ancre : la position de naissance, autour de laquelle le tour est cense se dessiner.
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            short scale = (short)(CrtRand() % 0x400 + 0xc00);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x9a) = 0;
            *(int*)(e + 0x54) = CrtRand() % 2 + 1;

            e[0x38] = (byte)(CrtRand() % 0x19 + 0x20);
            e[0x39] = (byte)(CrtRand() % 0x19 + 0x3c);
            e[0x3a] = (byte)(CrtRand() % 0x19 + 0x40);

            *(int*)(e + 0x5c) = CrtRand() % 0x0f + 0x19;
        }

        // Ghidra: LAB_004040e2 — la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) == 1)
        {
            int rise = *(int*)(e + 0x54);
            *(int*)(e + 0x20) += rise << 0x0a;

            // Ghidra: `imul 88888889h ; add edx,edi ; sar edx,6` plus le rattrapage par le bit de
            // signe — la division par 120 tronquee vers zero de l'idiome MSVC.
            *(int*)(e + 0x54) = rise + *(short*)(e + 0x14) / 120 + 1;

            // Les deux tables lisent le MEME angle : l'increment ne tombe qu'apres le cosinus.
            short yaw = *(short*)(e + 0x12);
            short radius = *(short*)(e + 0x98);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * radius + *(int*)(e + 0x50);
            int across = Winsys3d.GetCosTableEntry(yaw) * radius;

            *(short*)(e + 0x12) += 0x64;
            *(int*)(e + 0x24) = across + *(int*)(e + 0x58);

            *(short*)(e + 0x9a) += 1;

            // `mov ax,[esi+14h]` : le germe relu sur seize bits, puis deux additions seize bits.
            short growth = (short)(*(short*)(e + 0x14) + 0x50);
            *(short*)(e + 0x28) += growth;
            *(short*)(e + 0x2a) += growth;

            if (*(short*)(e + 0x9a) >= *(short*)(e + 0x16))
            {
                *(short*)(e + 0x9a) = 0;

                e[0x40]++;
                if (e[0x40] == 7) e[0x40] = 0;
            }

            // Ghidra: LAB_00404195.
            BrightenEntityTint_00404DE0(e, -2, -1, -2);

            *(int*)(e + 0x5c) -= 1;
            if (*(int*)(e + 0x5c) < 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00404460</c> — <b>l'entree 83 (<c>0x53</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 392 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00404460.asm</c>.
    ///
    /// <para>
    /// <b>La quatrieme de la fratrie 80-85</b>, et la seule qui se joue <b>en deux temps</b>. Elle
    /// tient sa victime comme ses sœurs — position recopiee, sequence <c>2</c> reimposee a chaque
    /// image, image d'animation avancee a la main une image sur deux — mais elle ne pose que le
    /// <b>bit 3</b> de son mot d'etat, pas les <c>0x1008</c> de la 81 et de la 82 : la victime
    /// garde donc son propre defilement d'animation en plus du sien.
    /// </para>
    /// <para>
    /// <b>Phase 1, la montee</b> : le son <c>0x20</c> a la naissance, puis une entite <c>0x35</c>
    /// (<see cref="EntityHandler_004045F0"/>) a chaque image, pendant 6 a 10 images. La duree
    /// epuisee, <c>+0x10</c> passe a 2 et le compteur repart de zero.
    /// </para>
    /// <para>
    /// <b>Phase 2, la rupture</b> : quinze images d'attente, dont la <b>dixieme</b> porte tout —
    /// le son <c>0x25</c> et <b>onze</b> entites <c>0x36</c> d'un coup
    /// (<see cref="EntityHandler_00404730"/>, la spirale). A la quinzieme l'effet s'eteint et rend
    /// son bit 3 a la victime.
    /// </para>
    /// <para>
    /// <c>+0x14</c> recoit la <b>moyenne</b> des trois etendues du bloc de modele de la victime,
    /// comme chez ses sœurs : c'est le germe que les deux enfants relisent pour se repartir autour
    /// d'elle. Elle est la seule des six a ne jamais toucher a la teinte de sa victime.
    /// </para>
    /// </summary>
    private static void EntityHandler_00404460(byte* effect)
    {
        byte* victim = *(byte**)(effect + 0x6c);
        if (victim == null) return;

        *(int*)(effect + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(effect + 0x20) = *(int*)(victim + 0x20);
        *(int*)(effect + 0x24) = *(int*)(victim + 0x24);

        // La sequence 2, reimposee a chaque image, et l'animation resolue sur l'indice 2 litteral.
        *(short*)(victim + 0x18) = 2;
        byte* animation = ((byte**)*(byte***)(Mem.s_AnimationTable_005e9640 + victim[4] * 4))[2];

        // `or ecx,8` seul : le bit « tenue par un effet », sans les 0x1000 de la 81 et de la 82.
        *(uint*)victim |= 8;

        if (*(short*)(effect + 0x10) == 0)
        {
            *(short*)(effect + 0x10) = 1;
            *(short*)(effect + 0x12) = (short)(CrtRand() % 5 + 6);   // Ghidra: FUN_00547140

            victim[0x0a] = 0;
            *(short*)(victim + 0x1a) = 0;

            ResolveEntityBlocks(victim, out _, out _, out byte* model);

            // Ghidra: `imul 55555556h` puis le rattrapage par le bit de signe — la division par
            // trois tronquee vers zero de l'idiome MSVC.
            int extent = *(short*)(model + 0x1c) + *(short*)(model + 0x1e) + *(short*)(model + 0x20);
            *(short*)(effect + 0x14) = (short)(extent / 3);
            *(short*)(effect + 0x16) = 0;

            // Ghidra: FUN_004031c0 — jumeau de TryPlaySoundNearEntity.
            TryPlaySoundNearEntity((uint*)effect, 0x20, 0x800, 0xa00, 0, 0);
        }

        // Ghidra: LAB_0040452d — la premiere image passe ici aussi.
        if (*(short*)(effect + 0x10) == 1)
        {
            *(short*)(effect + 0x12) -= 1;

            SpawnStatusEffectEntity_0047AC40(effect, (int*)(effect + 0x1c), null, 0x35, 0, -1);

            if (*(short*)(effect + 0x12) < 0)
            {
                *(short*)(effect + 0x10) = 2;
                *(short*)(effect + 0x12) = 0;
            }
        }

        // Ghidra: LAB_00404558 — la seconde phase. La premiere image de la phase 1 ne tombe PAS
        // dedans : le passage a 2 se fait en fin de phase 1, et le compteur vaut alors 0.
        if (*(short*)(effect + 0x10) == 2)
        {
            *(short*)(effect + 0x12) += 1;

            if (*(short*)(effect + 0x12) == 0x0a)
            {
                TryPlaySoundNearEntity((uint*)effect, 0x25, 0x400, 0xa00, 0, 0);

                // Ghidra: LAB_00404586 — `mov ebx,0Bh` puis `dec ebx ; jnz` : onze, pas dix.
                for (int i = 0; i < 0x0b; i++)
                {
                    SpawnStatusEffectEntity_0047AC40(
                        effect, (int*)(effect + 0x1c), null, 0x36, 0, -1);
                }
            }

            // Ghidra: LAB_0040459b — `jl`, donc l'extinction tombe a quinze et non a seize.
            if (*(short*)(effect + 0x12) >= 0x0f)
            {
                *(uint*)effect &= 0x7fffffff;
                *(uint*)victim &= 0xfffffff7;
            }
        }

        // Ghidra: LAB_004045b2 — meme borne que ses sœurs : l'image SIGNEE comparee au compte NON
        // signe, decremente sur 32 bits et reecrit sur seize.
        if ((effect[0x16] & 1) != 0)
        {
            *(short*)(victim + 0x1a) += 1;

            ushort frames = *(ushort*)(animation + 0x16);
            if (*(short*)(victim + 0x1a) >= (int)frames)
            {
                *(ushort*)(victim + 0x1a) = (ushort)(frames - 1);
            }
        }

        *(ushort*)(effect + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: <c>0x004045f0</c> — <b>l'entree 53 (<c>0x35</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>, celle de la passe
    /// <see cref="TickEntityTable_005f2d40"/>. 312 octets, <b>absente de <c>Blaze.c</c></b> ;
    /// releve garde sous <c>Legacy/Raw/Blaze/FUN_004045F0.asm</c>.
    ///
    /// <para>
    /// C'est ce que <see cref="EntityHandler_00404460"/> seme pendant sa premiere phase, et c'est
    /// <b>presque mot pour mot <see cref="EntityHandler_00404310"/></b> : meme atlas
    /// (<see cref="s_Entity34Descriptors_0054F0E0"/>), meme dispersion autour du point, meme
    /// echelle <c>0xbb8</c>, meme teinte <c>0xa0</c>, meme perte de deux par image, meme pas de
    /// 2 ou 3 images et meme mort a la septieme image d'atlas.
    /// </para>
    /// <para>
    /// <b>Deux differences, et deux seulement</b> :
    /// </para>
    /// <list type="number">
    /// <item><b>Elle ne monte pas.</b> Le <c>Y += 0x8000</c> par image de la jumelle n'existe pas
    /// ici : la bouffee reste a l'altitude ou elle est nee.</item>
    /// <item><b>Le diviseur est <c>germe / 2 + 1</c></b> et non <c>germe / 2</c>
    /// (<c>sar ecx,1 ; inc ecx</c>). Un germe inferieur a 2 fait diviser par zero chez la jumelle
    /// et pas ici — la meme dispersion, ecrite deux fois, une fois avec la garde.</item>
    /// </list>
    /// </summary>
    private static void EntityHandler_004045F0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            // Le binaire relit `word [esi+14h]` avant chaque tirage ; rien ne l'ecrit entre-temps.
            int size = *(short*)(e + 0x14);

            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Entity34Descriptors();   // Ghidra: &DAT_0054f0e0
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % (size / 2 + 1) - size / 4) << 0xc;

            size = *(short*)(e + 0x14);
            *(int*)(e + 0x24) += (CrtRand() % (size / 2 + 1) - size / 4) << 0xc;

            *(short*)(e + 0x12) = 0;

            // Pas de recentrage sur Y : la bouffee nait au-dessus du point, jamais en dessous.
            size = *(short*)(e + 0x14);
            *(int*)(e + 0x20) += (CrtRand() % (size / 2 + 1)) << 0xc;

            *(short*)(e + 0x28) = 0xbb8;
            *(short*)(e + 0x2a) = 0xbb8;
            e[0x38] = 0xa0;
            e[0x39] = 0xa0;
            e[0x3a] = 0xa0;

            // Ghidra: l'idiome MSVC du modulo signe, puis +2 — donc 2 ou 3 images par pas.
            *(short*)(e + 0x16) = (short)(CrtRand() % 2 + 2);
        }

        // Ghidra: LAB_004046e9 — la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, -2, -2, -2);

            *(short*)(e + 0x12) += 1;

            if (*(short*)(e + 0x12) >= *(short*)(e + 0x16))
            {
                *(short*)(e + 0x12) = 0;

                // Le binaire incremente PUIS compare a 7 : la septieme image d'atlas n'est jamais
                // affichee, elle sert de borne.
                e[0x40]++;
                if (e[0x40] == 7) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f138</c> — <b>l'atlas de la spirale</b> que
    /// <see cref="EntityHandler_00404730"/> pose en <c>+0x3c</c>. Quatre enregistrements de huit
    /// octets, toute la plage <c>0x0054f138</c>–<c>0x0054f157</c>, bornee par
    /// <see cref="s_Descriptors_0054F158"/> qui suit immediatement.
    ///
    /// <para>
    /// <b>Le corps n'en atteint que trois</b> : <c>+0x40</c> s'incremente et <b>reboucle</b> a 3.
    /// Le quatrieme est declare parce que la plage lui appartient — la borne vient de la donnee,
    /// pas du compteur.
    /// </para>
    /// <para>
    /// Les trois derniers sont une meme planche en <c>0x0f x 0x0f</c> (<c>0x0f0fa870</c>,
    /// <c>0x0f0fa880</c>, <c>0x0f0fa890</c>) ; le premier, <c>0x0f0f08f0</c>, est ailleurs dans
    /// l'atlas mais au meme gabarit.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F138 =
    [
        0xf0, 0x08, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x0054f138
        0x70, 0xa8, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x0054f140
        0x80, 0xa8, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x0054f148
        0x90, 0xa8, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x0054f150
    ];

    private static byte* s_Descriptors0054F138Storage;

    private static byte* Descriptors0054F138()
        => NativeCopy(s_Descriptors_0054F138, ref s_Descriptors0054F138Storage);

    /// <summary>
    /// Ghidra: FUN_00404730 @ 0x00404730 — <b>l'entree 54 (<c>0x36</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>La gerbe que <see cref="EntityHandler_00404460"/> lache par onze d'un coup</b>, a la
    /// dixieme image de sa seconde phase.
    ///
    /// <para>
    /// <b>C'est la spirale sur une sphere de <see cref="EntityHandler_004034A0"/></b>, a
    /// l'identique dans sa forme : <c>+0x50</c> le cap, <c>+0x54</c> l'elevation, <c>+0x58</c> le
    /// rayon, l'ancre figee en <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>, et la position entierement
    /// recalculee a chaque image. Le rayon croit de <c>+0x5c</c> (8 a 13) par image : l'etincelle
    /// s'ecarte en tournant.
    /// </para>
    /// <para>
    /// <b>Ce qui la distingue</b> : les deux angles sont <b>figes</b> a la naissance — ni cap ni
    /// elevation ne bougent —, si bien qu'elle ne tourne pas vraiment, elle <b>file en ligne
    /// droite dans une direction tiree au sort</b>, en s'ecartant de plus en plus vite. Elle part
    /// de la moitie du germe (<c>+0x14 &gt;&gt; 1</c>, un <c>sar</c> nu), a une demi-hauteur de
    /// germe au-dessus de son point de naissance, et <b>enfle</b> de 50 par image sur les deux
    /// axes — la jumelle, elle, retrecit.
    /// </para>
    /// <para>
    /// <b>Elle meurt d'etre eteinte.</b> Le germe <c>+0x14</c> est <b>ecrase</b> a la naissance par
    /// un tirage entre 9 et 15 : c'est desormais le pas d'assombrissement, applique aux trois
    /// canaux a chaque image. La teinte part entre <c>0xc8</c> et <c>0xf9</c>
    /// (<c>rand % 50 - 0x38</c>, replie sur l'octet) et l'entite s'eteint quand le canal rouge
    /// touche zero — 14 a 28 images. Un champ, deux roles, comme chez ses sœurs.
    /// </para>
    /// <para>
    /// L'atlas <see cref="s_Descriptors_0054F138"/> defile tous les <c>+0x16</c> images
    /// (1 ou 2) et <b>reboucle a trois</b> : il ne borne pas la vie.
    /// </para>
    /// </summary>
    private static void EntityHandler_00404730(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F138();   // Ghidra: &DAT_0054f138
            e[0x40] = 0;
            e[0x0a] = 0x0f;
            e[0x0b] = 1;

            // Ghidra: `CONCAT31(...) & 0xfffffffe) * 0x800` — le germe sign-etendu, son bit bas
            // tombe, decale de onze. La gerbe nait au-dessus du point qu'on lui a donne.
            *(int*)(e + 0x20) += (*(short*)(e + 0x14) & ~1) << 0x0b;

            *(int*)(e + 0x50) = CrtRand() % 0x1000;   // le cap, fige
            *(int*)(e + 0x54) = CrtRand() % 0x1000;   // l'elevation, figee

            // `sar` nu, arrondi vers moins l'infini — pas l'idiome tronque vers zero.
            *(int*)(e + 0x58) = *(short*)(e + 0x14) >> 1;
            *(int*)(e + 0x5c) = CrtRand() % 6 + 8;

            short scale = (short)(CrtRand() % 0x400 + 0xc00);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            // `(char)(rand % 50) - 0x38` : l'octet deborde, la teinte part donc entre 0xc8 et 0xf9.
            e[0x38] = (byte)(CrtRand() % 0x32 - 0x38);
            e[0x39] = (byte)(CrtRand() % 0x32 - 0x38);

            *(short*)(e + 0x12) = 0;

            e[0x3a] = (byte)(CrtRand() % 0x32 - 0x38);

            *(short*)(e + 0x16) = (short)(CrtRand() % 2 + 1);

            // Le germe est ECRASE ici : il devient le pas d'assombrissement.
            *(short*)(e + 0x14) = (short)(CrtRand() % 7 + 9);

            // L'ancre : la position de naissance, autour de laquelle la spirale se dessine.
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
        }

        // Ghidra: la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) == 1)
        {
            int pitch = *(int*)(e + 0x54);
            int yaw = *(int*)(e + 0x50);
            int radius = *(int*)(e + 0x58);

            // Le produit du rayon par le cosinus de l'elevation est ramene de douze bits ; celui
            // par le sinus ne l'est PAS. Meme asymetrie que chez EntityHandler_004034A0.
            int ring = (Winsys3d.GetCosTableEntry(pitch) * radius) >> 0xc;

            *(int*)(e + 0x1c) = ring * (int)Winsys3d.GetSinTableEntry(yaw) + *(int*)(e + 0x44);
            *(int*)(e + 0x24) = ring * Winsys3d.GetCosTableEntry(yaw) + *(int*)(e + 0x4c);

            *(short*)(e + 0x12) += 1;

            *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(pitch) * radius + *(int*)(e + 0x48);
            *(int*)(e + 0x58) = *(int*)(e + 0x5c) + radius;

            if (*(short*)(e + 0x16) <= *(short*)(e + 0x12))
            {
                *(short*)(e + 0x12) = 0;

                e[0x40]++;
                if (e[0x40] == 3) e[0x40] = 0;
            }

            *(short*)(e + 0x28) += 0x32;
            *(short*)(e + 0x2a) += 0x32;

            short fade = (short)-*(short*)(e + 0x14);
            BrightenEntityTint_00404DE0(e, fade, fade, fade);

            // Le canal rouge est le seul teste : les trois perdent le meme pas, mais ils ne sont
            // pas partis du meme endroit.
            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551a60</c> — l'atlas que <see cref="EntityHandler_004758C0"/> passe a
    /// son enfant <c>0x3f</c>. <b>Trois enregistrements</b>, et c'est le corps de l'enfant qui le
    /// dit : <c>FUN_00475910</c> pose <c>+0x40 = rand % 3</c>, il n'en existe donc que trois. La
    /// donnee le confirme — le quatrieme mot (<c>0x00551a78</c>) change de planche
    /// (<c>1010</c> au lieu de <c>1818</c>).
    /// </summary>
    private static readonly byte[] s_Descriptors_00551A60 =
    [
        0x00, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a60
        0x18, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a68
        0x30, 0x30, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00551a70
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551b00</c> — <b>huit enregistrements</b>, soit toute la plage
    /// <c>0x00551b00</c>–<c>0x00551b3f</c>, jusqu'a la table voisine
    /// <see cref="s_Descriptors_00551B40"/>. Ce n'est pas une table mais <b>deux planches qui se
    /// suivent</b>, et deux consommateurs distincts les adressent par la meme base :
    ///
    /// <list type="bullet">
    /// <item><b>Les cinq premiers</b> (<c>+0x00</c> a <c>+0x20</c>, planche <c>1010</c>) —
    /// <see cref="EntityHandler_00483450"/> les passe a son enfant. Les deux premiers ont
    /// <c>00 00</c> en cinquieme et sixieme octets la ou les trois suivants ont <c>01 02</c>.</item>
    /// <item><b>Les trois derniers</b> (<c>+0x28</c> a <c>+0x38</c>, planche <c>1818</c>) — la
    /// coque <c>0x51</c> de <see cref="TickFireOrb_0047F570"/> designe le premier d'entre eux,
    /// <c>0x00551b28</c>, en clair dans le binaire (<c>push 551B28h</c> a
    /// <c>0x0047f734</c>).</item>
    /// </list>
    ///
    /// <para>
    /// <b>Le changement de planche a <c>0x00551b28</c> n'etait donc pas une borne.</b> Il servait
    /// de fin de table faute de compteur lisible ; Boule de feu prouve le contraire, et le tableau
    /// arrete a <c>0x28</c> octets faisait lire a la coque huit octets de tas jamais ecrits — d'ou
    /// l'absence de boule orange et les eclats parasites autour d'elle.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551B00 =
    [
        0x10, 0x68, 0x10, 0x10, 0x00, 0x00, 0x10, 0x00,   // 0x00551b00
        0x20, 0x68, 0x10, 0x10, 0x00, 0x00, 0x10, 0x00,   // 0x00551b08
        0x00, 0x68, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551b10
        0x10, 0x68, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551b18
        0x20, 0x68, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551b20
        0xa8, 0x18, 0x18, 0x18, 0x00, 0x00, 0x10, 0x00,   // 0x00551b28
        0xc0, 0x18, 0x18, 0x18, 0x00, 0x00, 0x10, 0x00,   // 0x00551b30
        0xd8, 0x18, 0x18, 0x18, 0x00, 0x00, 0x10, 0x00,   // 0x00551b38
    ];

    private static byte* s_Descriptors00551A60Storage;
    private static byte* s_Descriptors00551B00Storage;

    /// <summary>Les trois descripteurs en memoire native.</summary>
    private static byte* Descriptors00551A60()
    {
        if (s_Descriptors00551A60Storage == null)
        {
            s_Descriptors00551A60Storage =
                (byte*)NativeMemory.Alloc((nuint)s_Descriptors_00551A60.Length);
            for (int i = 0; i < s_Descriptors_00551A60.Length; i++)
            {
                s_Descriptors00551A60Storage[i] = s_Descriptors_00551A60[i];
            }
        }

        return s_Descriptors00551A60Storage;
    }

    /// <summary>Les cinq descripteurs en memoire native.</summary>
    private static byte* Descriptors00551B00()
    {
        if (s_Descriptors00551B00Storage == null)
        {
            s_Descriptors00551B00Storage =
                (byte*)NativeMemory.Alloc((nuint)s_Descriptors_00551B00.Length);
            for (int i = 0; i < s_Descriptors_00551B00.Length; i++)
            {
                s_Descriptors00551B00Storage[i] = s_Descriptors_00551B00[i];
            }
        }

        return s_Descriptors00551B00Storage;
    }
}
