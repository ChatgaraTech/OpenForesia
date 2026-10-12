// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ECOLE 2 — Les 20 fiches, indices 0 a 19, sous-types 0xa0 a 0xb3, de Dodo a Terre.
//
// Les 20 sont implementees et toutes exercees en jeu, mais le fichier ne porte que QUATORZE
// bandeaux : six fiches partagent le corps d'une voisine et vivent sous son bandeau.
//
//   indices 3 a 6   — Materia feu / terre / air / eau, un seul bloc (`TickFireMateria_004774C0`,
//                     `TickEarthMateria_00477660`, `TickAirMateria_004777A0`,
//                     `TickWaterMateria_004778E0`) sous le bandeau MATERIA FEU ;
//   indices 11 a 13 — Escargot, Ralenti, Turbo, sous le bandeau COUSSIN D'AIR.
//
// Chercher une fiche par son NUMERO plutot que par son nom de bandeau : le tableau complet, avec
// la combinaison de boutons, le cout en PM et l'etat implemente/teste, est dans
// Documentation/Etat/SortsAtteignables.md.
//
// Le repartiteur commun, le ciblage et la naissance de l'entite de lancement sont dans
// `SpellCasting.cs`. Vue d'ensemble : Documentation/Reference/Sorts.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // DODO — la PREMIERE fiche de l'ecole 2, indice 0, sous-type 0xa0
    //
    //   0x00476C70   LE SORT. Sous-type 0xa0 de 0x00551c30, dix-huit lignes. Decompile dans Blaze.c.
    //   0x00476CF0   sa bulle, sous-type 0x53 de 0x00552230. 727 octets, ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00476CF0.asm.
    //
    // L'ECOLE 2 NE RESSEMBLE PAS A L'ECOLE 1, ET LA PREMIERE FICHE LE DIT DEJA. Les vingt-quatre
    // fiches de l'ecole 1 sont des corps qui vivent trente a soixante images, comptent, engendrent
    // du decor et posent leur effet a une image fixee. Dodo ne vit qu'UNE image : il remplit une
    // douzaine de champs, engendre un seul enfant, et s'efface en mettant son mot d'etat a zero.
    // Tout le sort est dans l'enfant.
    //
    // ET CET ENFANT EST UN PROJECTILE A TETE CHERCHEUSE. La bulle avance de dix unites par image
    // dans l'axe de son cap, tourne de 0x40 par image vers la cible que PickTargetInCone lui a
    // trouvee — mais seulement si l'ecart depasse une zone morte de +-0x40 —, ondule verticalement
    // autour de sa hauteur de depart, et seme une eclaboussure par image a une position tiree au
    // sort dans un cube de 0x40 unites. Elle meurt sur le decor, sur son age (quarante images), ou
    // sur sa victime — a qui elle pose l'effet d'etat 11.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551b60</c> — <b>les quatre images de la bulle de sommeil</b>, l'atlas
    /// que <see cref="TickSleep_00476C70"/> passe a son enfant <c>0x53</c>.
    ///
    /// <para>
    /// <b>Quatre, et l'engendreur le dit</b> : le sort pose <c>+0x1a = 4</c> juste avant
    /// d'engendrer, et <see cref="TickSleepBubble_00476CF0"/> fait tourner <c>+0x40</c> en anneau
    /// sur ce compte. La donnee suit : quatre tuiles <c>0x10 x 0x10</c> en <c>v = 0x78</c>, et la
    /// cinquieme entree (<c>0x00551b80</c>) change de forme.
    /// </para>
    /// <para>
    /// Elle commence exactement ou <see cref="s_Descriptors_00551B40"/> finit — la note portee sur
    /// celle-la annonçait deja le passage en <c>0x10 x 0x10</c> a cette frontiere.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00551B60 =
    [
        0x30, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551b60
        0x40, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551b68
        0x50, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551b70
        0x60, 0x78, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551b78
    ];

    private static byte* s_Descriptors00551B60Storage;

    /// <summary>Les quatre descripteurs en memoire native.</summary>
    private static byte* Descriptors00551B60()
        => NativeCopy(s_Descriptors_00551B60, ref s_Descriptors00551B60Storage);

    /// <summary>
    /// Ghidra: FUN_00476c70 @ 0x00476c70 — <b>Dodo</b> : le sous-type <c>0xa0</c>, creneau 160 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>premiere fiche de l'ecole 2</b>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne vit qu'une image.</b> C'est la premiere fiche du depot a le faire : il monte a la
    /// hauteur de tete du lanceur (<see cref="StatusBubbleHeight_00538BC0"/>, arrondie au pair et
    /// portee en <c>0x800</c>), remplit une douzaine de champs pour son enfant, engendre la bulle
    /// <c>0x53</c>, puis <b>met son propre mot d'etat a zero</b> — ce qui l'efface avant l'image
    /// suivante. Aucune des vingt-quatre fiches de l'ecole 1 ne fonctionne ainsi ; toutes comptent
    /// des images.
    /// </para>
    /// <para>
    /// Les champs qu'il pose sont ceux que la bulle relira en naissant : l'atlas
    /// <see cref="Descriptors00551B60"/> et son compte d'images <c>+0x1a = 4</c>, la teinte
    /// <c>0x808080</c>, les echelles, et <c>+0x78 = 0</c> — la bulle n'a <b>pas</b> de cible en
    /// naissant, elle ira la chercher elle-meme.
    /// </para>
    /// </summary>
    private static void TickSleep_00476C70(byte* record)
    {
        byte* e = record;

        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
        *(int*)(e + 0x20) += (int)((uint)height & 0xfffffffe) * 0x800;

        *(short*)(e + 0x2e) = -20;

        *(uint*)(e + 0x38) = 0x00808080;
        *(uint*)(e + 0x28) = 0x20002000;
        *(ushort*)(e + 0x2c) = 0x1f;

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;

        *(ushort*)(e + 0x9a) = 0xd000;
        *(byte**)(e + 0x78) = null;
        *(ushort*)(e + 0x1a) = 4;

        LegacyPlus.Coutures.ProjectileDeFee(e, () => SpawnStatusEffectEntity_0047AC40(   // Couture legacy ++ (LP-015)
            e, (int*)(e + 0x1c), Descriptors00551B60(), 0x53, e[0x0b], -1));

        // ET IL S'EFFACE : le mot d'etat entier a zero, donc plus de 0x80000000.
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00476cf0 @ 0x00476cf0 — <b>la bulle de Dodo</b> : le sous-type <c>0x53</c> de
    /// <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c>. 727 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00476CF0.asm</c>.
    ///
    /// <para>
    /// <b>C'est elle le sort.</b> Elle cherche sa cible seule, a la premiere image, par
    /// <see cref="PickTargetInCone_00474F00"/> avec un masque plein — puis, tant qu'elle en a une,
    /// elle tourne vers elle de <c>0x40</c> par image. <b>La correction a une zone morte</b> :
    /// l'ecart de cap est replie sur douze bits et decale de <c>0x40</c> ; s'il tient dans
    /// <c>0x80</c>, la bulle ne corrige pas. Le sens du virage sort d'un
    /// <c>setge</c>/<c>dec</c>/<c>and 0x80</c> qui vaut <c>+0x40</c> sous un demi-tour et
    /// <c>-0x40</c> au-dela.
    /// </para>
    /// <para>
    /// <b>Elle avance de dix unites par image</b> dans l'axe de son cap, et <b>ondule</b> : sa
    /// hauteur vaut <c>sin(age &lt;&lt; 8) * 32</c> ajoute a la hauteur de depart, que la premiere
    /// image a rangee dans <c>+0x48</c>. Chaque image elle seme une eclaboussure <c>0xbf</c> a une
    /// position tiree au sort dans un cube de <c>0x40</c> unites autour d'elle — trois tirages, un
    /// par axe — en pretant <c>+0x1a = 1</c> le temps de l'appel.
    /// </para>
    /// <para>
    /// <b>Trois facons de mourir</b>, et toutes relachent le lanceur : le decor
    /// (<c>0x40000000</c> pose par <see cref="FinalizeEntityPlacement2"/>, ou le contact
    /// <c>0x8802</c> en <c>+0x90</c>), l'age (quarante images), ou la victime. La hauteur que la
    /// finalisation calcule est jetee — la bulle vole, elle ne se pose pas.
    /// </para>
    /// <para>
    /// <b>Sur la victime</b> : la paire famille/indice est pretee au lanceur <b>en un seul mot</b>
    /// (<c>+0x06</c> et <c>+0x07</c> ensemble, la ou l'ecole 1 les ecrit octet par octet), puis
    /// <see cref="ApplyStatusEffect_0053B730"/> — <b>l'effet d'etat 11</b> — le clignotement, et un
    /// halo <c>0xbe</c> mis a l'echelle de la tete de la victime, <c>(hauteur &lt;&lt; 12) / 75</c>
    /// par la multiplication magique <c>0x1B4E81B5</c>.
    /// </para>
    /// </summary>
    private static void TickSleepBubble_00476CF0(byte* record)
    {
        byte* e = record;

        // La boite de detection, reconstruite sur la pile a chaque image.
        short* probe = stackalloc short[6] { 0, -60, 60, 100, 0, 120 };

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x80;
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;

            // La hauteur de depart, autour de laquelle elle ondulera.
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
        }

        // Ghidra: LAB_00476d40 — la tete chercheuse.
        byte* target = *(byte**)(e + 0x78);

        if (target == null)
        {
            *(byte**)(e + 0x78) = PickTargetInCone_00474F00(e, 0xffffffff);
        }
        else
        {
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            int yaw = *(short*)(e + 0x32);

            // L'ecart de cap, replie sur douze bits — ce masque est aussi ce qui rend inoffensif
            // le mot haut du registre, que le binaire ne recharge pas.
            int error = (Winsys3d.Ordinal_457(dx, dz) - yaw) & 0xfff;

            // LA ZONE MORTE : sous 0x80 de part et d'autre, la bulle ne corrige pas.
            if (((error + 0x40) & 0xfff) > 0x80)
            {
                // Ghidra: `setge dl ; dec edx ; and edx,80h ; add edx,-0x40` — +0x40 sous un
                // demi-tour, -0x40 au-dela.
                int turn = error >= 0x800 ? -0x40 : 0x40;
                *(short*)(e + 0x32) = (short)(turn + yaw);
            }
        }

        // Ghidra: LAB_00476da8 — dix unites par image dans l'axe du cap.
        int heading = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 10;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 10;

        // L'ONDULATION : l'age, porte a huit bits, sert d'angle.
        *(int*)(e + 0x20) =
            ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 8) << 5) + *(int*)(e + 0x48);

        // L'ECLABOUSSURE, une par image, a une position tiree dans un cube de 0x40 unites.
        {
            short lentFrames = *(short*)(e + 0x1a);
            *(ushort*)(e + 0x1a) = 1;

            int* scatter = stackalloc int[3];
            scatter[0] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x1c);
            scatter[1] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x20);
            scatter[2] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x24);

            SpawnStatusEffectEntity_0047AC40(
                e, scatter, Descriptors00551BB8(), 0xbf, e[0x0b], -1);

            *(short*)(e + 0x1a) = lentFrames;
        }

        // La finalisation sert au CONTACT, pas au placement : sa hauteur est jetee.
        int flying = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);
        bool consumed = (*(uint*)e & 0x40000000) != 0;
        *(int*)(e + 0x20) = flying;

        if (!consumed && (*(uint*)(e + 0x90) & 0x8802) == 0)
        {
            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

            if (victim != null)
            {
                // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
                *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

                // Le binaire appelle le lecteur de bloc de statistiques et jette le resultat ; la
                // fonction est pure, l'appel est donc sans effet. Transcrit tel quel.
                _ = GetEntityStatBlock((uint*)victim);

                LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), (uint*)victim, &ApplyStatusEffect_0053B730);   // Couture legacy ++ (LP-015)

                *(uint*)victim |= 0x40;
                *(ushort*)(victim + 0x96) = 0x10;

                // LE HALO, mis a l'echelle de la tete de la victime.
                int height = StatusBubbleHeight_00538BC0((uint*)victim);

                *(int*)(e + 0x44) = 0;
                *(int*)(e + 0x48) = (height & ~1) << 0xb;

                // (hauteur << 12) / 75 — multiplication magique 0x1B4E81B5 puis `sar 3`. Le
                // decalage de 3 donne 75 ; c'est `sar 4` qui donne 150 (Vitesse, Materia feu).
                int scale = (height << 0xc) / 75;

                *(int*)(e + 0x4c) = 0;
                *(int*)(e + 0x50) = 0;
                *(int*)(e + 0x54) = 0;
                *(int*)(e + 0x58) = 0;

                *(int*)(e + 0x28) = (scale << 0x10) | scale;
                *(short*)(e + 0x16) = 0x1f;
                *(uint*)(e + 0x38) = 0x01606060;
                *(ushort*)(e + 0x1a) = 7;
                *(byte**)(e + 0x70) = victim;

                SpawnStatusEffectEntity_0047AC40(
                    e, (int*)(e + 0x1c), TrapBurstDescriptors(), 0xbe, 0, 3);

                Aud.PlayPositionalSound(0x21, 0x0329, 0x1200, (int*)(e + 0x1c));

                *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
                *(uint*)e = 0;
                return;
            }

            // Ghidra: LAB_00476f6e — l'anneau d'images de l'atlas.
            e[0x40] = (byte)(e[0x40] + 1);
            if (e[0x40] >= *(short*)(e + 0x1a)) e[0x40] = 0;

            // Un bruit toutes les quatorze images.
            if (*(short*)(e + 0x10) % 14 == 0)
            {
                Aud.PlayPositionalSound(0x17, 0x0100, 0x1200, (int*)(e + 0x1c));
            }

            *(short*)(e + 0x10) += 1;
            if (*(short*)(e + 0x10) < 0x28) return;
        }

        // Ghidra: LAB_00476fb3 — le decor, ou l'age : le lanceur est relache et la bulle s'efface.
        *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551878</c> — <b>le script de sons differes du sabot</b>, deux pas de
    /// huit octets. Le format est celui que decrit <c>Audio.QueueDelayedSoundScript</c> :
    /// l'identifiant en <c>[0]</c>, le <b>delai avant le pas suivant</b> en <c>[1]</c> (zero
    /// termine), le nombre de sons a jouer d'un coup en <c>[2]</c>, la hauteur en <c>[4]</c> et le
    /// gain en <c>[6]</c>.
    ///
    /// <para>
    /// Deux fois le meme son <c>0x02</c>, espaces de <b>cinq images</b> et le second plus aigu
    /// (<c>0x0400</c> puis <c>0x0554</c>) : le sabot cogne, puis rebondit. La borne est le
    /// <c>[1] = 0</c> du second pas ; il suit sans trou
    /// <see cref="s_ResistSoundScript_00551868"/>, qui se termine de la meme facon.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00551878 =
    [
        0x02, 0x05, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x00551878
        0x02, 0x00, 0x01, 0x00, 0x54, 0x05, 0x00, 0x12,   // 0x00551880 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript00551878Storage;

    /// <summary>Les deux pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00551878()
        => NativeCopy(s_DelayedSoundScript_00551878, ref s_DelayedSoundScript00551878Storage);

    // =====================================================================
    // GROS SABOTS — ecole 2, indice 1, sous-type 0xa1
    //
    //   0x00476FD0   LE SORT. Sous-type 0xa1 de 0x00551c30. 199 octets, ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00476FD0.asm.
    //   0x004770A0   LE SABOT, sous-type 0x7e de 0x00551f30 — donc de l'AUTRE table statique,
    //                celle de la passe qui raccroche ses entites a la grille. 702 octets,
    //                ABSENT de Blaze.c ; releve sous Legacy/Raw/Blaze/FUN_004770A0.asm.
    //
    // LA DEUXIEME FICHE DE L'ECOLE 2 CONFIRME CE QUE DISAIT LA PREMIERE : le sort ne vit qu'une
    // image et tout est dans son enfant. Mais la ou Dodo lachait un projectile qui cherche sa
    // cible, Gros sabots la choisit LUI-MEME — FindNearestOpponent_00474EB0 des la premiere image
    // — puis fait naitre le sabot TROIS CENTS UNITES AU-DESSUS DE LA TETE de cette victime, a
    // l'aplomb exact de ses X/Z, et s'efface.
    //
    // ET LE SABOT TOMBE. Il n'a pas de vitesse propre : le sort lui pose seulement une gravite
    // (+0x9a = 0x6000), et IntegrateWithDrag_00535880 fait le reste. Pendant la chute il grossit
    // d'un quart de l'ecart par image vers une taille reglee sur la stature de la victime —
    // (hauteur << 12) / 75, la meme division magique que le halo de Dodo.
    //
    // A L'IMPACT il APLATIT sa victime : echelles 0x1800 / 0 / 0x1800, c'est-a-dire etalee au sol
    // et sans epaisseur. C'est le seul corps du depot a ecrire les echelles d'une AUTRE entite.
    // Puis six eclaboussures partent en etoile, l'ecran tremble, et pendant quarante images le
    // sabot se dandine sur place avant de retrecir d'un quart par image, tandis que la victime
    // regagne 0x1000 dans les trois axes. Il ne la relache qu'a ce moment-la.
    //
    // SAUF SI ELLE RESISTE : l'effet 20 (ApplyStatusEffect_0053BDA0) rend non nul, le sabot se
    // marque 0x1004000 et meurt a l'image suivante, sans rien aplatir.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00476fd0 @ 0x00476fd0 — <b>Gros sabots</b> : le sous-type <c>0xa1</c>, creneau
    /// 161 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>deuxieme fiche de l'ecole
    /// 2</b>. 199 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne vit qu'une image</b>, comme <see cref="TickSleep_00476C70"/> : la premiere fait
    /// tout, la seconde ne trouve que le bit <c>0x2000000</c> qu'elle a pose et met le mot d'etat
    /// a zero.
    /// </para>
    /// <para>
    /// <b>Il choisit la victime lui-meme</b> — <see cref="FindNearestOpponent_00474EB0"/> avec le
    /// mode <c>1</c>, le seul que la carte du monde emploie — et sans cible il s'eteint sans rien
    /// engendrer. Avec une cible, il se replace a l'aplomb de celle-ci
    /// (<c>+0x1c</c>/<c>+0x24</c> recopies, <c>+0x32</c> aussi) et <b>monte de sa stature plus 300
    /// unites</b> avant d'engendrer le sabot. Ce qu'il laisse dans <c>+0x12</c> est la taille que
    /// l'enfant visera en tombant.
    /// </para>
    /// </summary>
    private static void TickHeavyHoof_00476FD0(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            // La deuxieme image : tout s'est joue a la premiere, l'emplacement redevient libre.
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00476fe9
        byte* target = LegacyPlus.Coutures.CibleDeFee(e, FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 1));   // Couture legacy ++ (LP-015)

        if (target == null)
        {
            // Ghidra: LAB_00477091 — personne en face, et le sort s'efface sans engendrer.
            *(uint*)e = 0;
            return;
        }

        *(byte**)(e + 0x78) = target;

        int height = StatusBubbleHeight_00538BC0((uint*)target);

        // TROIS CENTS UNITES AU-DESSUS DE SA TETE : c'est de la que le sabot tombera.
        *(int*)(e + 0x20) += (height + 0x12c) << 0xc;

        // La taille visee — (hauteur << 12) / 75, multiplication magique 0x1B4E81B5 puis `sar 3`,
        // exactement comme le halo de Dodo.
        *(short*)(e + 0x12) = (short)((height << 0xc) / 75);

        // ET IL NAITRA SANS TAILLE : le sabot grandit en tombant.
        *(int*)(e + 0x28) = 0;
        *(ushort*)(e + 0x2c) = 0;
        *(ushort*)(e + 0x14) = 0;

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;

        // Sa gravite par entite : c'est la seule chose qui le fera descendre.
        *(ushort*)(e + 0x9a) = 0x6000;

        *(short*)(e + 0x32) = *(short*)(target + 0x32);
        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        e[0x04] = 0x31;

        SpawnCastEntity_0047AAE0(e, 0x7e, e[0x0b], -1);

        *(uint*)e |= 0x2000000;
    }

    /// <summary>
    /// Ghidra: FUN_004770a0 @ 0x004770a0 — <b>le sabot de Gros sabots</b> : le sous-type
    /// <c>0x7e</c> de <see cref="s_StaticEntityHandlers_00551f30"/>, la table de
    /// <c>0x005edee0</c>. 702 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004770A0.asm</c>.
    ///
    /// <para>
    /// <b>Deux vies, separees par le bit <c>0x2</c></b> du mot d'etat. Tant qu'il est clair, le
    /// sabot <b>tombe</b> : <see cref="IntegrateWithDrag_00535880"/> l'amene vers le bas sous la
    /// gravite que le sort lui a posee, et sa taille rejoint <c>+0x12</c> d'un quart de l'ecart par
    /// image. A la premiere image il <b>saisit</b> sa victime — <c>victime+0x74</c> pointe sur lui
    /// — et cogne le son <c>0x16</c>.
    /// </para>
    /// <para>
    /// <b>Il touche quand il est passe sous la hauteur de sa victime</b>, et alors il prete la paire
    /// famille/indice au lanceur <b>en un seul mot</b> (<c>+0x06</c> et <c>+0x07</c> ensemble, comme
    /// la bulle de Dodo) et appelle <see cref="ApplyStatusEffect_0053BDA0"/> — l'effet 20, le plus
    /// etroitement immunise de la table des quarante-huit.
    /// </para>
    /// <para>
    /// <b>Si l'effet passe, la victime est APLATIE</b> : <c>0x1800 / 0 / 0x1800</c> dans ses trois
    /// echelles, etalee au sol et sans epaisseur. C'est le seul corps transcrit qui ecrive les
    /// echelles d'une autre entite. Six eclaboussures <c>0xbe</c> partent en etoile — chacune avec
    /// sa propre vitesse, parce que <see cref="SpawnStatusEffectEntity_0047AC40"/> recopie
    /// <c>+0x50</c>..<c>+0x58</c> de la source — le script de sons differes
    /// <see cref="s_DelayedSoundScript_00551878"/> part, et l'ecran tremble.
    /// </para>
    /// <para>
    /// <b>Ensuite il se dandine quarante images</b> (<c>+0x32</c> module par un sinus de son age),
    /// puis retrecit d'un quart par image pendant que la victime regagne <c>0x1000</c> ; il ne
    /// relache <c>victime+0x74</c> et ne s'efface que lorsqu'elle a retrouve sa taille.
    /// <b>Si elle a resiste</b>, il se marque <c>0x1004000</c> et meurt a l'image suivante.
    /// </para>
    /// <para>
    /// <b>Il suit sa victime en X/Z a chaque image</b>, quoi qu'il arrive — la fin de fonction est
    /// commune a tous les chemins, y compris ceux qui viennent de mettre le mot d'etat a zero.
    /// </para>
    /// </summary>
    private static void TickHeavyHoofStomp_004770A0(byte* record)
    {
        byte* e = record;
        byte* victim = *(byte**)(e + 0x78);

        // Le pere pose son 0x2000000 APRES avoir engendre ; le sabot le recupere ici, a sa
        // premiere image, et le garde.
        if ((*(uint*)e & 0x2000000) == 0)
        {
            *(uint*)e |= *(uint*)(*(byte**)(e + 0x6c)) & 0x2000000;
        }

        // Ghidra: LAB_004770c6 — le bit 0x2 separe la chute de l'ecrasement.
        if ((*(uint*)e & 2) != 0)
        {
            if ((*(uint*)e & 0x1000000) != 0)
            {
                // La victime a resiste. Une image de repit — le temps que le 0x2000000 soit la —
                // puis le sabot relache et s'efface.
                if ((*(uint*)e & 0x2000000) != 0)
                {
                    *(byte**)(victim + 0x74) = null;
                    *(uint*)e = 0;
                }
            }
            else
            {
                // Ghidra: LAB_004770f4 — LE DANDINEMENT : l'age sert d'angle, sur huit bits.
                *(short*)(e + 0x32) +=
                    (short)((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 8) >> 5);

                if (*(short*)(e + 0x10) >= 0x28)
                {
                    // Le sabot perd un quart de sa taille par image.
                    short hoof = *(short*)(e + 0x28);
                    hoof = (short)(hoof - (short)(hoof >> 2));
                    *(short*)(e + 0x28) = hoof;
                    *(short*)(e + 0x2a) = hoof;
                    *(short*)(e + 0x2c) = hoof;

                    // Et la victime regagne 0x1000, du quart de l'ecart par image. L'epaisseur
                    // (+0x2a) repart de zero ; la largeur et la profondeur redescendent de 0x1800.
                    short flat = *(short*)(victim + 0x28);
                    flat = (short)(flat + ((0x1000 - flat) >> 2));

                    short thickness = *(short*)(victim + 0x2a);
                    thickness = (short)(((0x1000 - thickness) >> 2) + thickness);

                    *(short*)(victim + 0x28) = flat;
                    *(short*)(victim + 0x2a) = thickness;
                    *(short*)(victim + 0x2c) = flat;

                    // La borne est prise sur la largeur seule, a huit unites pres de 0x1000.
                    if (flat <= 0x1008)
                    {
                        *(byte**)(victim + 0x74) = null;
                        *(uint*)e = 0;
                    }
                }

                // Ghidra: LAB_0047716d — la secousse s'amortit EN CHANGEANT DE SIGNE a chaque
                // image : 8, -6, 4, -3, 2, -2, 1, -1, 0. Le meme amortissement que l'explosion.
                int* shake = Mem.Camera(0x005de0ac);
                if (*shake != 0) *shake = (*shake >> 2) - *shake;
            }
        }
        else
        {
            // Ghidra: LAB_0047718c — LA CHUTE.
            if (*(short*)(e + 0x10) == 0)
            {
                // IL SAISIT SA VICTIME des la premiere image, avant meme de l'avoir touchee.
                *(byte**)(victim + 0x74) = e;
                Aud.PlayPositionalSound(0x16, 0x400, 0x1200, (int*)(e + 0x1c));
            }

            // Ghidra: LAB_004771af — la taille rejoint +0x12 d'un quart de l'ecart par image.
            short scale = *(short*)(e + 0x28);
            scale = (short)(scale + ((*(short*)(e + 0x12) - scale) >> 2));
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
            *(short*)(e + 0x2c) = scale;

            // Il n'a aucune vitesse propre : seule la gravite +0x9a le fait descendre.
            IntegrateWithDrag_00535880(e);

            if (*(int*)(e + 0x20) < *(int*)(victim + 0x20))
            {
                // IL TOUCHE. La hauteur est ramenee sur celle de la victime, et la seconde vie
                // commence — l'age repart de zero.
                *(int*)(e + 0x20) = *(int*)(victim + 0x20);
                *(uint*)e |= 2;
                *(short*)(e + 0x10) = 0;

                // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
                *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

                if (LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), (uint*)victim, &ApplyStatusEffect_0053BDA0) != 0)   // Couture legacy ++ (LP-015)
                {
                    // Resiste : rien n'est aplati, et le sabot mourra a l'image suivante.
                    *(uint*)e |= 0x1004000;
                }
                else
                {
                    // Ghidra: LAB_0047721d — L'APLATISSEMENT.
                    *(short*)(victim + 0x96) = 0x28;
                    *(int*)(victim + 0x28) = 0x1800;      // largeur 0x1800, epaisseur ZERO
                    *(ushort*)(victim + 0x2c) = 0x1800;   // et profondeur 0x1800

                    // Les trois champs que les eclaboussures vont emprunter, mis de cote.
                    int savedScale = *(int*)(e + 0x28);
                    int savedTint = *(int*)(e + 0x38);
                    int savedFrame = *(int*)(e + 0x14);

                    *(byte**)(e + 0x70) = null;
                    *(ushort*)(e + 0x1a) = 7;
                    *(uint*)(e + 0x38) = 0x01606060;
                    *(short*)(e + 0x12) = 3;
                    *(uint*)(e + 0x28) = 0x20002000;
                    *(ushort*)(e + 0x14) = 0x3000;
                    *(short*)(e + 0x16) = 3;

                    // SIX ECLABOUSSURES EN ETOILE. La vitesse est ecrite sur LE SABOT avant
                    // chaque naissance : l'engendreur recopie +0x50..+0x58 de la source, c'est
                    // donc ainsi que chacune part dans sa propre direction. Le sabot garde la
                    // derniere, sans consequence — il n'integre plus rien dans sa seconde vie.
                    int* scatter = stackalloc int[3];

                    for (int remaining = 6; remaining != 0; remaining--)
                    {
                        int angle = CrtRand();      // Ghidra: FUN_00547140

                        *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(angle) << 5;
                        *(int*)(e + 0x54) = 0x20000;
                        *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(angle) << 5;

                        // Le carre de dispersion fait 0x40 unites, et il est PLAT : la hauteur
                        // est reprise telle quelle, seuls X et Z sont tires au sort.
                        scatter[0] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x1c);
                        scatter[1] = *(int*)(e + 0x20);
                        scatter[2] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x24);

                        SpawnStatusEffectEntity_0047AC40(
                            e, scatter, Descriptors00551A08(), 0xbe, 0, 3);
                    }

                    *(int*)(e + 0x28) = savedScale;
                    *(int*)(e + 0x38) = savedTint;
                    *(int*)(e + 0x14) = savedFrame;

                    Aud.QueueDelayedSoundScript(DelayedSoundScript00551878(), (int*)(e + 0x1c));

                    *Mem.Camera(0x005de0ac) = 8;          // et l'ecran tremble
                }

                // Ghidra: LAB_0047732e — le choc, resiste ou non.
                Aud.PlayPositionalSound(0x22, 0x800, 0x1200, (int*)(e + 0x1c));
            }
        }

        // Ghidra: LAB_00477346 — IL SUIT SA VICTIME EN X/Z, sur tous les chemins, y compris ceux
        // qui viennent de mettre le mot d'etat a zero.
        *(int*)(e + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(e + 0x24) = *(int*)(victim + 0x24);
        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551888</c> — <b>le script de sons differes de Vitesse</b>, quatre pas
    /// de huit octets. Meme format que <see cref="s_DelayedSoundScript_00551878"/>, qu'il suit sans
    /// trou : l'identifiant en <c>[0]</c>, le delai avant le pas suivant en <c>[1]</c> (zero
    /// termine), le nombre de sons d'un coup en <c>[2]</c>, la hauteur en <c>[4]</c>, le gain en
    /// <c>[6]</c>.
    ///
    /// <para>
    /// <b>Quatre fois le meme son <c>0x0c</c>, de plus en plus aigu</b> — <c>0x0200</c>,
    /// <c>0x02d4</c>, <c>0x037e</c>, <c>0x0554</c> — et espaces de <b>deux images</b> : un arpege
    /// montant, qui est ce qu'on entend quand l'anneau se ferme. La borne est le <c>[1] = 0</c> du
    /// quatrieme pas.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00551888 =
    [
        0x0c, 0x02, 0x01, 0x00, 0x00, 0x02, 0x00, 0x12,   // 0x00551888
        0x0c, 0x02, 0x01, 0x00, 0xd4, 0x02, 0x00, 0x12,   // 0x00551890
        0x0c, 0x02, 0x01, 0x00, 0x7e, 0x03, 0x00, 0x12,   // 0x00551898
        0x0c, 0x00, 0x01, 0x00, 0x54, 0x05, 0x00, 0x12,   // 0x005518a0 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript00551888Storage;

    /// <summary>Les quatre pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00551888()
        => NativeCopy(s_DelayedSoundScript_00551888, ref s_DelayedSoundScript00551888Storage);

    // =====================================================================
    // VITESSE — ecole 2, indice 2, sous-type 0xa2
    //
    //   0x00477360   LE SORT. Sous-type 0xa2 de 0x00551c30. DECOMPILE dans Blaze.c.
    //   0x00477420   L'ANNEAU, sous-type 0x81 de 0x00551f30. DECOMPILE dans Blaze.c aussi — les
    //                deux corps de cette fiche sont les premiers de l'ecole 2 a l'etre.
    //
    // C'EST LE PREMIER SORT BIENFAISANT DE L'ECOLE 2, et sa cible n'est pas un ennemi :
    // FindTargetAround_00474BA0 en mode 0 fait balayer au lanceur SON PROPRE CAMP, et faute de
    // voisin elle rend le lanceur lui-meme — Vitesse ne rate donc jamais, elle se lance sur soi.
    // L'effet 19 le confirme : c'est le seul de la table des quarante-huit a n'avoir NI masque
    // d'immunite NI jet de sauvegarde.
    //
    // LE SORT NE VIT QU'UNE IMAGE, comme les deux precedentes fiches de l'ecole. Mais la ou Dodo
    // et Gros sabots engendraient un seul enfant, celui-ci en engendre DEUX D'AFFILEE — deux
    // anneaux de tailles differentes, distingues par le mot +0x16 : le premier est haut et etroit
    // (largeur x, hauteur 1,5x), le second large et plat (largeur 1,5x, hauteur 0,75x). Ils
    // partent la meme image et vivent les memes trente-deux.
    //
    // ET SEUL LE SECOND POSE L'EFFET : l'anneau ne le fait que si son +0x16 est non nul. Le
    // premier n'est que du decor. C'est le seul endroit du depot ou deux enfants identiques se
    // partagent ainsi le travail.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00477360 @ 0x00477360 — <b>Vitesse</b> : le sous-type <c>0xa2</c>, creneau 162
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>troisieme fiche de l'ecole 2</b>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle ne vit qu'une image</b> et met son mot d'etat a zero en partant, comme
    /// <see cref="TickSleep_00476C70"/> et <see cref="TickHeavyHoof_00476FD0"/> — mais sans meme
    /// avoir besoin d'un bit de garde, puisqu'elle ne revient jamais.
    /// </para>
    /// <para>
    /// <b>Sa cible est un ALLIE</b> : <see cref="FindTargetAround_00474BA0"/> en mode 0 fait
    /// balayer au lanceur son propre camp, et rend le lanceur lui-meme quand personne n'est a
    /// portee. Elle la range <b>deux fois</b> — en <c>+0x70</c>, dont l'anneau se sert pour se
    /// coller a ses pieds, et en <c>+0x78</c>, qui est ce que l'effet recevra.
    /// </para>
    /// <para>
    /// <b>Deux anneaux, pas un</b>, tailles reglees sur la stature de la cible :
    /// <c>base = (hauteur &lt;&lt; 12) / 150</c> — la meme division magique que Dodo et Gros
    /// sabots — puis <c>haut = base + base/2</c>. Le premier anneau part en
    /// <c>(base, haut, base)</c> avec <c>+0x16 = 0</c>, le second en
    /// <c>(haut, (haut &amp; ~1)/2, haut)</c> avec <c>+0x16 = 1</c>. Le compilateur ecrit ce second
    /// couple comme <c>(haut &amp; ~1) &lt;&lt; 15 | haut</c> : le <c>&amp; ~1</c> garantit que le
    /// decalage ne deborde pas dans le mot bas, ce qui revient a poser la moitie en <c>+0x2a</c>.
    /// </para>
    /// </summary>
    private static void TickHaste_00477360(byte* record)
    {
        byte* e = record;

        // Faute de voisin dans son camp, elle rend le lanceur : Vitesse ne rate jamais.
        byte* target = LegacyPlus.Coutures.CibleDeFee(e, FindTargetAround_00474BA0(e, 0));   // Couture legacy ++ (LP-015)

        *(byte**)(e + 0x78) = target;
        *(byte**)(e + 0x70) = target;

        *(uint*)(e + 0x38) = 0x00f0f0f0;
        e[0x04] = 0x26;

        int height = StatusBubbleHeight_00538BC0((uint*)target);

        // La taille de base — (hauteur << 12) / 150, la meme que les deux fiches precedentes.
        uint low = (uint)((height << 0xc) / 0x96);
        uint high = (uint)(((int)low >> 1) + (int)low);      // une fois et demie

        // LE PREMIER ANNEAU : haut et etroit, et il ne posera RIEN.
        *(short*)(e + 0x2c) = (short)low;
        *(ushort*)(e + 0x16) = 0;
        *(uint*)(e + 0x28) = high * 0x10000 | low;

        SpawnCastEntity_0047AAE0(e, 0x81, e[0x0b], -1);

        // LE SECOND : large et plat, et c'est LUI qui posera l'effet.
        *(uint*)(e + 0x28) = (high & 0xfffffffe) << 0xf | high;
        *(short*)(e + 0x2c) = (short)high;
        *(ushort*)(e + 0x16) = 1;

        SpawnCastEntity_0047AAE0(e, 0x81, e[0x0b], -1);

        Aud.PlayPositionalSound(0x0d, 0x2d4, 0x1200, (int*)(e + 0x1c));

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00477420 @ 0x00477420 — <b>l'anneau de Vitesse</b> : le sous-type <c>0x81</c> de
    /// <see cref="s_StaticEntityHandlers_00551f30"/>, la table de <c>0x005edee0</c>.
    /// <b>Decompile dans <c>Blaze.c</c></b>. Il en naît <b>deux par lancer</b>, que seul le mot
    /// <c>+0x16</c> distingue.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Il ne se deplace pas : il se recolle</b> a sa cible chaque image, et
    /// <b>a ses PIEDS</b> — la hauteur vient de <c>cible+0x60</c>, la cote du sol sous elle, pas de
    /// son <c>+0x20</c>. C'est ce qui fait un anneau qui tourne au ras du sol et non a mi-corps.
    /// Il tourne de <c>age * 8</c> par image, donc de plus en plus vite.
    /// </para>
    /// <para>
    /// <b>Sa teinte est un sinus de son age</b> : <c>sin(age &lt;&lt; 6) * 255 &gt;&gt; 12</c>,
    /// pose sur les trois canaux a la fois — un gris qui monte puis redescend. A l'age
    /// <c>0x20</c>, l'anneau se ferme : le script de sons differes
    /// <see cref="s_DelayedSoundScript_00551888"/> part et il s'efface.
    /// </para>
    /// <para>
    /// <b>Et seul le second anneau agit.</b> Sous <c>+0x16 == 0</c> il ne fait rien de plus ;
    /// sinon il prete la paire famille/indice au lanceur <b>en un seul mot</b> (<c>+0x06</c> et
    /// <c>+0x07</c> ensemble, comme toute l'ecole 2) et appelle
    /// <see cref="ApplyStatusEffect_0053BD10"/> — l'effet 19, duree <c>attaque/8 + 0x28</c> en
    /// <c>+0xa6</c>, bit <c>0x4000</c>, et <b>ni masque d'immunite ni jet de sauvegarde</b>.
    /// </para>
    /// <para>
    /// L'age est incremente <b>apres</b> l'effacement, sur le meme chemin ; c'est ce que fait
    /// l'original et l'emplacement est deja libre, donc sans consequence.
    /// </para>
    /// </remarks>
    private static void TickHasteRing_00477420(byte* record)
    {
        byte* e = record;
        byte* target = *(byte**)(e + 0x70);

        short age = *(short*)(e + 0x10);

        // Il tourne de plus en plus vite : le pas est l'age lui-meme, multiplie par huit.
        *(short*)(e + 0x32) += (short)(age * 8);

        // IL SE COLLE AUX PIEDS DE SA CIBLE : +0x60 est la cote du sol sous elle, pas son +0x20.
        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x60);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        // Ghidra: Ordinal_451 — la table de sinus de winsys3d.
        int grey = (int)Winsys3d.GetSinTableEntry(age << 6) * 0xff >> 0xc;
        *(uint*)(e + 0x38) = (uint)((grey << 8 | grey) << 8 | grey);

        if (age >= 0x20)
        {
            if (*(short*)(e + 0x16) != 0)
            {
                // LE SECOND ANNEAU SEUL. La paire famille/indice, pretee au lanceur en un mot.
                *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

                LegacyPlus.Coutures.EffetDeFee(
                    *(uint**)(e + 0x74), *(uint**)(e + 0x78), &ApplyStatusEffect_0053BD10);   // Couture legacy ++ (LP-015)
            }

            Aud.QueueDelayedSoundScript(DelayedSoundScript00551888(), (int*)(e + 0x1c));
            *(uint*)e = 0;
        }

        *(short*)(e + 0x10) = (short)(age + 1);
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005518a8</c> — <b>le script de sons differes des quatre Materia</b>, six
    /// enregistrements de huit octets en <b>quatre pas</b>. Meme format que
    /// <see cref="s_DelayedSoundScript_00551888"/>, qu'il suit sans trou.
    ///
    /// <para>
    /// <b>Son dernier pas joue TROIS sons d'un coup</b> : son <c>[2]</c> vaut 3, et
    /// <c>Audio.TickDelayedSounds</c> avance alors de trois enregistrements. C'est le premier
    /// script du depot a s'en servir — les cinq autres ont tous <c>[2] = 1</c> partout —, et c'est
    /// ce qui donne les six enregistrements pour quatre pas. La borne reste le <c>[1] = 0</c>, porte
    /// par le quatrieme.
    /// </para>
    /// <para>
    /// Les trois derniers sont le meme son <c>0x1c</c> a trois hauteurs (<c>0x0800</c>,
    /// <c>0x0554</c>, <c>0x0400</c>) et au meme gain plus faible <c>0x0f00</c> : un accord, la ou
    /// les trois premiers pas sont des coups isoles.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_005518A8 =
    [
        0x2c, 0x18, 0x01, 0x00, 0x29, 0x03, 0x00, 0x12,   // 0x005518a8
        0x10, 0x10, 0x01, 0x00, 0xa6, 0x0c, 0x00, 0x12,   // 0x005518b0
        0x0c, 0x03, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x005518b8
        0x1c, 0x00, 0x03, 0x00, 0x00, 0x08, 0x00, 0x0f,   // 0x005518c0 — delai nul : fin, et TROIS sons
        0x1c, 0x00, 0x00, 0x00, 0x54, 0x05, 0x00, 0x0f,   // 0x005518c8 — le deuxieme du lot
        0x1c, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x0f,   // 0x005518d0 — le troisieme
    ];

    private static byte* s_DelayedSoundScript005518A8Storage;

    /// <summary>Les six enregistrements du script en memoire native.</summary>
    private static byte* DelayedSoundScript005518A8()
        => NativeCopy(s_DelayedSoundScript_005518A8, ref s_DelayedSoundScript005518A8Storage);

    // =====================================================================
    // MATERIA FEU — ecole 2, indice 3, sous-type 0xa3
    //
    //   0x004774C0   LE SORT. Sous-type 0xa3 de 0x00551c30. DECOMPILE dans Blaze.c.
    //   0x00477600   LA FLAMME, sous-type 0x56 de 0x00552230. 96 octets, ABSENTE de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00477600.asm.
    //   0x0047BDF0   l'embrasement, sous-type 0x4c — DEJA ECRIT ET DEJA EXERCE : c'est celui de
    //                Feu d'enfer (TickHellfireBlaze_0047BDF0). La Materia le reemploie tel quel.
    //
    // MATERIA FEU EST LA PREMIERE D'UNE FRATRIE DE QUATRE. `0x00477660` (terre), `0x004777a0`
    // (air) et `0x004778e0` (eau) ont le MEME corps, instruction pour instruction : ils ne
    // different que par TROIS constantes — l'identifiant du son joue a la premiere image, sa
    // hauteur, et l'indice de l'effet pose a la derniere (3, 4, 5, 6, les quatre effets « a
    // intensite » de la table des quarante-huit). Ce sont donc quatre enchantements
    // elementaires, pas quatre sorts. Le desassemblage des deux premiers l'etablit octet par
    // octet : hors adresses de saut, ils ne different qu'en ces trois points.
    //
    // ELLE DURE SOIXANTE IMAGES ET SE LANCE SUR UN ALLIE, comme Vitesse : FindTargetAround en
    // mode 0, qui rend le lanceur lui-meme faute de voisin. Les vingt-quatre premieres images
    // sement UNE FLAMME PAR IMAGE autour des pieds de la cible, a un angle et un rayon tires au
    // sort — le rayon dans la moitie de sa stature. A la soixantieme, l'effet est pose.
    //
    // ET LA FLAMME NE COMPTE PAS SES IMAGES : c'est sa TEINTE qui la tue. Elle part de 0xf0f0f0,
    // perd 0x101010 par image, et le corps s'efface quand la soustraction tombe pile sur zero —
    // le `jne` lit les indicateurs de cette addition-la, pas un compteur. Quinze images, donc,
    // et pas une de plus ni de moins.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004774c0 @ 0x004774c0 — <b>Materia feu</b> : le sous-type <c>0xa3</c>, creneau
    /// 163 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>quatrieme fiche de l'ecole
    /// 2</b>. <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Premiere fiche de l'ecole 2 a vivre plus d'une image</b> — soixante, et en trois temps
    /// qui se recouvrent : la mise en place a l'image 0, l'ensemencement jusqu'a la 24e, et l'effet
    /// a la 60e.
    /// </para>
    /// <para>
    /// <b>Sa cible est un allie</b>, par <see cref="FindTargetAround_00474BA0"/> en mode 0 — le
    /// lanceur lui-meme quand personne n'est a portee, comme <see cref="TickHaste_00477360"/>. Elle
    /// la range en <c>+0x78</c> (ce que l'effet recevra) et en <c>+0x70</c> (ce a quoi les flammes
    /// se recolleront).
    /// </para>
    /// <para>
    /// <b>Elle lache d'abord l'embrasement <c>0x4c</c></b>, qui est celui de Feu d'enfer
    /// (<see cref="TickHellfireBlaze_0047BDF0"/>) — deja transcrit, deja exerce en foret. Puis
    /// <b>une flamme <c>0x56</c> par image pendant vingt-quatre images</b>, chacune a un angle tire
    /// au sort et un rayon tire dans la <b>moitie de la stature</b> de la cible (<c>+0x12</c>). La
    /// hauteur de depart est <c>cible+0x60</c>, la cote du sol : les flammes naissent a ses pieds.
    /// </para>
    /// <para>
    /// <b>A la soixantieme image elle pose l'effet 3</b> (<see cref="ApplyStatusEffect_0053B100"/>),
    /// le premier des quatre qui rangent une <b>intensite</b> a cote de leur duree — c'est ce qui
    /// fait de cette fratrie des enchantements elementaires. La paire famille/indice est pretee au
    /// lanceur en un seul mot, comme partout dans l'ecole 2.
    /// </para>
    /// </summary>
    private static void TickFireMateria_004774C0(byte* record)
    {
        byte* e = record;
        byte* target;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -64;

            // Faute de voisin dans son camp, elle rend le lanceur : la Materia ne rate jamais.
            target = LegacyPlus.Coutures.CibleDeFee(e, FindTargetAround_00474BA0(e, 0));   // Couture legacy ++ (LP-015)

            *(byte**)(e + 0x78) = target;
            *(byte**)(e + 0x70) = target;

            // L'EMBRASEMENT, et c'est celui de Feu d'enfer — reemploye tel quel.
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4c, e[0x0b], -1);

            int height = StatusBubbleHeight_00538BC0((uint*)target);

            // La MOITIE de la stature : c'est le rayon dans lequel les flammes seront semees.
            *(short*)(e + 0x12) = (short)(height >> 1);

            // (hauteur << 12) / 150 — multiplication magique 0x1B4E81B5 puis `sar 4`. Ecrit en
            // mot long, donc l'echelle en Y (+0x2a) part a zero : c'est de la que la flamme
            // s'etirera.
            *(int*)(e + 0x28) = (height << 0xc) / 150;

            // Le decalage dont la flamme divisera son etirement restant, a chaque image.
            *(ushort*)(e + 0x14) = 3;

            *(uint*)(e + 0x38) = 0x00f0f0f0;

            Aud.PlayPositionalSound(0x20, 0x400, 0x1200, (int*)(e + 0x1c));
            Aud.QueueDelayedSoundScript(DelayedSoundScript005518A8(), (int*)(e + 0x1c));
        }
        else
        {
            target = *(byte**)(e + 0x78);
        }

        // UNE FLAMME PAR IMAGE, pendant vingt-quatre images.
        if (*(short*)(e + 0x10) < 0x18)
        {
            int angle = CrtRand();                              // Ghidra: FUN_00547140
            int radius = CrtRand() % *(short*)(e + 0x12);

            *(short*)(e + 0x32) = (short)angle;

            // Le decalage horizontal, que la flamme ajoutera a la position de la cible a chaque
            // image de sa vie — il ne bouge plus une fois tire.
            *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(angle) * radius;
            *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(angle) * radius;

            // Et elle naît AUX PIEDS de la cible : +0x60 est la cote du sol sous elle.
            *(int*)(e + 0x20) = *(int*)(target + 0x60);

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519A0(), 0x56, e[0x0b], -1);
        }

        if (*(short*)(e + 0x10) >= 0x3c)
        {
            // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
            *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

            LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), *(uint**)(e + 0x78), &ApplyStatusEffect_0053B100);   // Couture legacy ++ (LP-015)

            *(uint*)e = 0;
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00477600 @ 0x00477600 — <b>la flamme de Materia feu</b> : le sous-type
    /// <c>0x56</c> de <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c>.
    /// 96 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00477600.asm</c>. Les quatre Materia la partagent.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Elle ne compte pas ses images : c'est sa TEINTE qui la tue.</b> Elle perd <c>0x101010</c>
    /// par image, et le corps s'efface quand la soustraction tombe <b>pile sur zero</b> — le
    /// <c>jne</c> final lit les indicateurs de cette addition-la, pas un compteur. Partie de
    /// <c>0xf0f0f0</c>, elle vit donc exactement quinze images. Une flamme qui naitrait d'une autre
    /// teinte ne s'eteindrait jamais : rien d'autre ne la borne.
    /// </para>
    /// <para>
    /// <b>Elle s'etire.</b> Son echelle en Y (<c>+0x2a</c>) part de zero et approche <c>0x7fff</c>
    /// d'une fraction de la distance restante a chaque image — la fraction etant
    /// <c>1 / 2^(+0x14)</c>, et le sort ayant pose <c>+0x14 = 3</c>, un huitieme. Le decalage est
    /// lu comme un <b>octet</b> et sert de compte de decalage tel quel.
    /// </para>
    /// <para>
    /// <b>Elle suit sa cible en X/Z</b> — position de la cible plus le decalage fixe que le sort a
    /// tire pour elle (<c>+0x44</c> et <c>+0x4c</c>) — et <b>monte de vingt unites par image</b>,
    /// sans gravite ni integrateur : <c>+0x20</c> est simplement augmente de <c>0x14000</c>.
    /// </para>
    /// </remarks>
    private static void TickFireMateriaFlame_00477600(byte* record)
    {
        byte* e = record;
        byte* target = *(byte**)(e + 0x70);

        // L'ETIREMENT : un huitieme de ce qui reste jusqu'a 0x7fff, par image.
        short stretch = *(short*)(e + 0x2a);
        stretch = (short)(stretch + ((0x7fff - stretch) >> e[0x14]));
        *(short*)(e + 0x2a) = stretch;

        // Elle se recolle a sa cible, decalee de ce que le sort a tire pour elle.
        *(int*)(e + 0x1c) = *(int*)(e + 0x44) + *(int*)(target + 0x1c);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c) + *(int*)(target + 0x24);

        // Et elle monte de vingt unites par image, sans gravite ni integrateur.
        *(int*)(e + 0x20) += 0x14000;

        // LA TEINTE EST SON COMPTE A REBOURS. `add ecx,0FFEFEFF0h` puis `jne` : c'est cette
        // soustraction-la qui decide, et seul un zero exact eteint la flamme.
        uint tint = *(uint*)(e + 0x38) - 0x00101010;
        *(uint*)(e + 0x38) = tint;

        if (tint == 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00477660 @ 0x00477660 — <b>Materia terre</b> : le sous-type <c>0xa4</c>, creneau
    /// 164 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>cinquieme fiche de l'ecole
    /// 2</b>. <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickFireMateria_004774C0"/>, octet pour octet, a trois constantes
    /// pres.</b> Le desassemblage des deux corps, adresses de saut mises a part, ne differe qu'en
    /// ces trois points : le son de la premiere image (<c>4</c> au lieu de <c>0x20</c>), sa hauteur
    /// (<c>0x190</c> au lieu de <c>0x400</c>), et l'effet pose a la derniere
    /// (<see cref="ApplyStatusEffect_0053B1B0"/>, l'effet 4, au lieu de l'effet 3). Tout le reste
    /// — l'embrasement <c>0x4c</c>, la flamme <c>0x56</c>, le script de sons differes, les vingt-
    /// quatre images d'ensemencement, les soixante de vie — est identique.
    /// </para>
    /// <para>
    /// <b>Le corps est donc ecrit deux fois ici, comme dans le binaire.</b> Le compilateur de 1998
    /// a emis quatre copies ; les factoriser en une fonction parametree donnerait un code plus
    /// court mais une transcription moins fidele, et masquerait qu'un futur relevé d'air ou d'eau
    /// pourrait reserver une quatrieme difference. Le commentaire d'en-tete de la fratrie, porte
    /// par Materia feu, explique ce qui les separe ; celui-ci ne repete que ce qui lui est propre.
    /// </para>
    /// <para>
    /// <b>L'effet 4 range sa duree en <c>+0x88</c> et son intensite en <c>+0xe6</c></b>, la ou
    /// l'effet 3 emploie <c>+0x86</c> et <c>+0xe4</c> : deux emplacements voisins et distincts,
    /// donc les deux enchantements se cumulent au lieu de se remplacer.
    /// </para>
    /// </summary>
    private static void TickEarthMateria_00477660(byte* record)
    {
        byte* e = record;
        byte* target;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -64;

            // Faute de voisin dans son camp, elle rend le lanceur : la Materia ne rate jamais.
            target = LegacyPlus.Coutures.CibleDeFee(e, FindTargetAround_00474BA0(e, 0));   // Couture legacy ++ (LP-015)

            *(byte**)(e + 0x78) = target;
            *(byte**)(e + 0x70) = target;

            // LE MEME embrasement que Materia feu — donc celui de Feu d'enfer.
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4c, e[0x0b], -1);

            int height = StatusBubbleHeight_00538BC0((uint*)target);

            // La MOITIE de la stature : le rayon dans lequel les flammes seront semees.
            *(short*)(e + 0x12) = (short)(height >> 1);

            // (hauteur << 12) / 150 — multiplication magique 0x1B4E81B5 puis `sar 4`.
            *(int*)(e + 0x28) = (height << 0xc) / 150;

            *(ushort*)(e + 0x14) = 3;
            *(uint*)(e + 0x38) = 0x00f0f0f0;

            // LA PREMIERE DES TROIS DIFFERENCES : un autre son, et plus grave.
            Aud.PlayPositionalSound(4, 0x190, 0x1200, (int*)(e + 0x1c));

            Aud.QueueDelayedSoundScript(DelayedSoundScript005518A8(), (int*)(e + 0x1c));
        }
        else
        {
            target = *(byte**)(e + 0x78);
        }

        // UNE FLAMME PAR IMAGE, pendant vingt-quatre images — la meme 0x56 que Materia feu.
        if (*(short*)(e + 0x10) < 0x18)
        {
            int angle = CrtRand();                              // Ghidra: FUN_00547140
            int radius = CrtRand() % *(short*)(e + 0x12);

            *(short*)(e + 0x32) = (short)angle;

            *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(angle) * radius;
            *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(angle) * radius;

            *(int*)(e + 0x20) = *(int*)(target + 0x60);

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519A0(), 0x56, e[0x0b], -1);
        }

        if (*(short*)(e + 0x10) >= 0x3c)
        {
            // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
            *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

            // LA TROISIEME DIFFERENCE : l'effet 4, duree en +0x88 et intensite en +0xe6.
            LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), *(uint**)(e + 0x78), &ApplyStatusEffect_0053B1B0);   // Couture legacy ++ (LP-015)

            *(uint*)e = 0;
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_004777a0 @ 0x004777a0 — <b>Materia air</b> : le sous-type <c>0xa5</c>, creneau
    /// 165 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>sixieme fiche de l'ecole
    /// 2</b>. <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Troisieme copie du corps de <see cref="TickFireMateria_004774C0"/></b>, et la parente est
    /// verifiee au desassembleur comme pour <see cref="TickEarthMateria_00477660"/> : hors adresses
    /// de saut, le diff ne porte que sur <b>les trois memes constantes</b>, aux trois memes
    /// endroits. Le son de la premiere image est <c>9</c> a la hauteur <c>0x100</c> — la plus grave
    /// des trois relevees jusqu'ici (<c>0x400</c> pour feu, <c>0x190</c> pour terre) — et l'effet
    /// pose a la derniere est <see cref="ApplyStatusEffect_0053B260"/>, l'effet 5.
    /// </para>
    /// <para>
    /// <b>Aucune quatrieme difference n'apparait</b> : la crainte gardee en tete depuis terre — que
    /// l'une des copies s'ecarte du patron — ne s'est pas realisee ici. Elle reste a verifier pour
    /// eau, la derniere de la fratrie.
    /// </para>
    /// <para>
    /// <b>L'effet 5 range en <c>+0x8a</c> / <c>+0xe8</c></b>, deux crans plus loin que l'effet 4 :
    /// les quatre enchantements elementaires occupent quatre paires consecutives et se cumulent
    /// donc tous les quatre.
    /// </para>
    /// </summary>
    private static void TickAirMateria_004777A0(byte* record)
    {
        byte* e = record;
        byte* target;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -64;

            // Faute de voisin dans son camp, elle rend le lanceur : la Materia ne rate jamais.
            target = LegacyPlus.Coutures.CibleDeFee(e, FindTargetAround_00474BA0(e, 0));   // Couture legacy ++ (LP-015)

            *(byte**)(e + 0x78) = target;
            *(byte**)(e + 0x70) = target;

            // LE MEME embrasement que les deux precedentes — donc celui de Feu d'enfer.
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4c, e[0x0b], -1);

            int height = StatusBubbleHeight_00538BC0((uint*)target);

            // La MOITIE de la stature : le rayon dans lequel les flammes seront semees.
            *(short*)(e + 0x12) = (short)(height >> 1);

            // (hauteur << 12) / 150 — multiplication magique 0x1B4E81B5 puis `sar 4`.
            *(int*)(e + 0x28) = (height << 0xc) / 150;

            *(ushort*)(e + 0x14) = 3;
            *(uint*)(e + 0x38) = 0x00f0f0f0;

            // LA PREMIERE ET LA DEUXIEME DIFFERENCE : un autre son, et le plus grave des trois.
            Aud.PlayPositionalSound(9, 0x100, 0x1200, (int*)(e + 0x1c));

            Aud.QueueDelayedSoundScript(DelayedSoundScript005518A8(), (int*)(e + 0x1c));
        }
        else
        {
            target = *(byte**)(e + 0x78);
        }

        // UNE FLAMME PAR IMAGE, pendant vingt-quatre images — la meme 0x56 que Materia feu.
        if (*(short*)(e + 0x10) < 0x18)
        {
            int angle = CrtRand();                              // Ghidra: FUN_00547140
            int radius = CrtRand() % *(short*)(e + 0x12);

            *(short*)(e + 0x32) = (short)angle;

            *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(angle) * radius;
            *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(angle) * radius;

            *(int*)(e + 0x20) = *(int*)(target + 0x60);

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519A0(), 0x56, e[0x0b], -1);
        }

        if (*(short*)(e + 0x10) >= 0x3c)
        {
            // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
            *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

            // LA TROISIEME DIFFERENCE : l'effet 5, duree en +0x8a et intensite en +0xe8.
            LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), *(uint**)(e + 0x78), &ApplyStatusEffect_0053B260);   // Couture legacy ++ (LP-015)

            *(uint*)e = 0;
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_004778e0 @ 0x004778e0 — <b>Materia eau</b> : le sous-type <c>0xa6</c>, creneau
    /// 166 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>septieme fiche de l'ecole
    /// 2</b>. <b>Decompilee dans <c>Blaze.c</c></b>. <b>Elle clot la fratrie des quatre
    /// Materia.</b>
    ///
    /// <para>
    /// <b>La quatrieme et derniere copie du corps de <see cref="TickFireMateria_004774C0"/></b>, et
    /// la parente est verifiee au desassembleur comme pour les deux precedentes : hors adresses de
    /// saut, le diff ne porte que sur les trois memes constantes, aux trois memes endroits. Le son
    /// de la premiere image est <c>0x18</c> a la hauteur <c>0x384</c>, et l'effet pose a la
    /// derniere est <see cref="ApplyStatusEffect_0053B310"/>, l'effet 6.
    /// </para>
    /// <para>
    /// <b>Les quatre copies sont donc relevees, et la reponse est nette : il n'y a pas de quatrieme
    /// difference.</b> Ce que le diff etablissait pour terre puis pour air vaut pour la fratrie
    /// entiere. Elle est faite d'un seul comportement et de quatre tables de constantes, et les
    /// quatre effets qu'elle pose (3, 4, 5, 6) sont les quatre « a intensite » de la table des
    /// quarante-huit, rangeant leur duree et leur magnitude en <b>quatre paires consecutives</b> —
    /// <c>+0x86</c>/<c>+0xe4</c>, <c>+0x88</c>/<c>+0xe6</c>, <c>+0x8a</c>/<c>+0xe8</c>,
    /// <c>+0x8c</c>/<c>+0xea</c>. Aucun ne remplace l'autre : les quatre enchantements se cumulent.
    /// </para>
    /// <para>
    /// <b>Ni l'un ni l'autre des deux enfants n'est propre a un element</b> : les quatre lachent le
    /// meme embrasement <c>0x4c</c> — celui de Feu d'enfer — et la meme flamme <c>0x56</c>, avec
    /// les memes atlas. Quelle que soit la Materia lancee, ce qu'on voit a l'ecran est identique ;
    /// seul le son de depart et l'effet range dans le bloc de statistiques changent.
    /// </para>
    /// </summary>
    private static void TickWaterMateria_004778E0(byte* record)
    {
        byte* e = record;
        byte* target;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -64;

            // Faute de voisin dans son camp, elle rend le lanceur : la Materia ne rate jamais.
            target = LegacyPlus.Coutures.CibleDeFee(e, FindTargetAround_00474BA0(e, 0));   // Couture legacy ++ (LP-015)

            *(byte**)(e + 0x78) = target;
            *(byte**)(e + 0x70) = target;

            // LE MEME embrasement que les trois precedentes — donc celui de Feu d'enfer.
            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors00551BA0(), 0x4c, e[0x0b], -1);

            int height = StatusBubbleHeight_00538BC0((uint*)target);

            // La MOITIE de la stature : le rayon dans lequel les flammes seront semees.
            *(short*)(e + 0x12) = (short)(height >> 1);

            // (hauteur << 12) / 150 — multiplication magique 0x1B4E81B5 puis `sar 4`.
            *(int*)(e + 0x28) = (height << 0xc) / 150;

            *(ushort*)(e + 0x14) = 3;
            *(uint*)(e + 0x38) = 0x00f0f0f0;

            // LA PREMIERE ET LA DEUXIEME DIFFERENCE : le son 0x18, a la hauteur 0x384.
            Aud.PlayPositionalSound(0x18, 0x384, 0x1200, (int*)(e + 0x1c));

            Aud.QueueDelayedSoundScript(DelayedSoundScript005518A8(), (int*)(e + 0x1c));
        }
        else
        {
            target = *(byte**)(e + 0x78);
        }

        // UNE FLAMME PAR IMAGE, pendant vingt-quatre images — la meme 0x56 que Materia feu.
        if (*(short*)(e + 0x10) < 0x18)
        {
            int angle = CrtRand();                              // Ghidra: FUN_00547140
            int radius = CrtRand() % *(short*)(e + 0x12);

            *(short*)(e + 0x32) = (short)angle;

            *(int*)(e + 0x44) = (int)Winsys3d.GetSinTableEntry(angle) * radius;
            *(int*)(e + 0x4c) = Winsys3d.GetCosTableEntry(angle) * radius;

            *(int*)(e + 0x20) = *(int*)(target + 0x60);

            SpawnStatusEffectEntity_0047AC40(
                e, (int*)(e + 0x1c), Descriptors005519A0(), 0x56, e[0x0b], -1);
        }

        if (*(short*)(e + 0x10) >= 0x3c)
        {
            // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
            *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

            // LA TROISIEME DIFFERENCE : l'effet 6, duree en +0x8c et intensite en +0xea.
            LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), *(uint**)(e + 0x78), &ApplyStatusEffect_0053B310);   // Couture legacy ++ (LP-015)

            *(uint*)e = 0;
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// <c>&amp;DAT_00551bc0</c>, l'atlas du <b>Chant des sirenes</b> — le <b>cinquieme</b>
    /// enregistrement de la plage <see cref="s_Descriptors_00551BA0"/>, pas une table a lui, comme
    /// <see cref="Descriptors00551BA8"/> est le deuxieme.
    ///
    /// <para>
    /// Le sort et sa note l'emploient tous deux, et tous deux avec <c>+0x1a = 1</c> : un seul
    /// enregistrement est atteint. C'est celui qui n'a <b>pas</b> les octets <c>01 02</c> de ses
    /// voisins — la particularite que la note portee sur la plage signalait sans savoir qui s'en
    /// servait.
    /// </para>
    /// </summary>
    private static byte* Descriptors00551BC0() => Descriptors00551BA0() + 0x20;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005518d8</c> — <b>le script de sons differes du Chant des sirenes</b>,
    /// quatre pas de huit octets. Meme format que <see cref="s_DelayedSoundScript_005518A8"/>,
    /// qu'il suit sans trou.
    ///
    /// <para>
    /// <b>Quatre fois le meme son <c>0x1c</c>, de plus en plus aigu</b> — <c>0x0200</c>,
    /// <c>0x0329</c>, <c>0x0400</c>, <c>0x0653</c> — espaces de <b>trois images</b> : une gamme
    /// montante, ce qu'on entend quand le chant prend. La borne est le <c>[1] = 0</c> du quatrieme
    /// pas ; le suivant (<c>0x005518f8</c>) change de son et de gain, donc de script.
    /// </para>
    /// <para>
    /// C'est le meme son <c>0x1c</c> que l'accord final des quatre Materia
    /// (<see cref="s_DelayedSoundScript_005518A8"/>), joue ici en sequence au lieu d'un coup.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_005518D8 =
    [
        0x1c, 0x03, 0x01, 0x00, 0x00, 0x02, 0x00, 0x12,   // 0x005518d8
        0x1c, 0x03, 0x01, 0x00, 0x29, 0x03, 0x00, 0x12,   // 0x005518e0
        0x1c, 0x03, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x005518e8
        0x1c, 0x00, 0x01, 0x00, 0x53, 0x06, 0x00, 0x12,   // 0x005518f0 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript005518D8Storage;

    /// <summary>Les quatre pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript005518D8()
        => NativeCopy(s_DelayedSoundScript_005518D8, ref s_DelayedSoundScript005518D8Storage);

    // =====================================================================
    // CHANT SIRENES — ecole 2, indice 7, sous-type 0xa7
    //
    //   0x00477A20   LE SORT. Sous-type 0xa7 de 0x00551c30. DECOMPILE dans Blaze.c, dix lignes.
    //   0x00477A90   LA NOTE, sous-type 0x54 de 0x00552230. 656 octets, ABSENTE de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00477A90.asm.
    //
    // C'EST DODO, REECRIT. Apres la fratrie des Materia — quatre copies d'un meme corps —, voici
    // la seconde forme de reemploi de l'ecole 2, et elle est d'une autre nature : le corps n'est
    // pas copie, il est REFAIT. La note du Chant des sirenes et la bulle de Dodo
    // (TickSleepBubble_00476CF0) ont la meme charpente instruction par instruction sur les deux
    // tiers de leur longueur — meme boite de detection {0, -60, 60, 100, 0, 120}, meme saisie du
    // lanceur a la premiere image, meme tete chercheuse a zone morte de +-0x40, meme dix unites
    // par image, meme ondulation sin(age << 8) << 5, meme anneau d'images, meme bruit toutes les
    // quatorze images, meme mort a la quarantieme — mais ce ne sont PAS les memes octets, et les
    // deux corps existent separement dans le binaire.
    //
    // TROIS CHOSES LES SEPARENT, et toutes trois sont ce qui fait le sort :
    //
    //   1. LA BULLE SEMAIT, LA NOTE RESPIRE. Dodo lachait une eclaboussure par image a une
    //      position tiree dans un cube ; la note ne lache rien et fait battre sa propre taille,
    //      0x1800 plus la moitie d'un sinus de son age pris sur NEUF bits (age << 9, deux fois
    //      plus vite que l'ondulation).
    //   2. L'EFFET N'EST PAS LE MEME : l'effet 8 (0x0053b4b0) au lieu du 11. C'est « le seul
    //      effet discret de la famille » — il ne fait naitre que sa marque, sans poussiere — et
    //      son jet de sauvegarde est l'autre des deux (RollSavingThrowB_0053A790).
    //   3. LA VICTIME EST FETEE AUTREMENT. Dodo posait un halo unique a l'echelle de sa tete ;
    //      la note lache SIX eclats 0xbe autour d'elle, chacun avec sa propre vitesse a deux
    //      angles tires au sort, et met en file une gamme montante de quatre notes.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00477a20 @ 0x00477a20 — <b>Chant sirenes</b> : le sous-type <c>0xa7</c>, creneau
    /// 167 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>huitieme fiche de l'ecole
    /// 2</b>. <b>Decompilee dans <c>Blaze.c</c></b>, dix lignes.
    ///
    /// <para>
    /// <b>C'est <see cref="TickSleep_00476C70"/> allege.</b> Meme forme exactement — monter a la
    /// hauteur de tete du lanceur, remplir quelques champs pour l'enfant, l'engendrer, mettre son
    /// propre mot d'etat a zero —, mais quatre des champs que Dodo posait ont disparu : la
    /// profondeur <c>+0x2c</c>, la remise a zero de la vitesse, et surtout <b>la gravite
    /// <c>+0x9a</c></b>, que le sort ne touche pas. La note ne tombe donc pas ; elle n'a d'ailleurs
    /// aucun integrateur.
    /// </para>
    /// <para>
    /// <c>+0x1a = 1</c> : la note ne parcourt qu'un seul enregistrement d'atlas, contre quatre pour
    /// la bulle de Dodo. Son anneau d'images tourne donc sur place.
    /// </para>
    /// </summary>
    private static void TickSirenSong_00477A20(byte* record)
    {
        byte* e = record;

        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));

        *(short*)(e + 0x2e) = -20;
        *(int*)(e + 0x20) += (int)((uint)height & 0xfffffffe) * 0x800;

        *(uint*)(e + 0x38) = 0x00808080;
        *(uint*)(e + 0x28) = 0x20002000;

        // La note n'a PAS de cible en naissant : elle ira la chercher elle-meme.
        *(byte**)(e + 0x78) = null;

        // Un seul enregistrement d'atlas, la ou Dodo en parcourait quatre.
        *(ushort*)(e + 0x1a) = 1;

        LegacyPlus.Coutures.ProjectileDeFee(e, () => SpawnStatusEffectEntity_0047AC40(   // Couture legacy ++ (LP-015)
            e, (int*)(e + 0x1c), Descriptors00551BC0(), 0x54, e[0x0b], -1));

        // ET IL S'EFFACE.
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00477a90 @ 0x00477a90 — <b>la note du Chant des sirenes</b> : le sous-type
    /// <c>0x54</c> de <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c>.
    /// 656 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00477A90.asm</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>C'est la bulle de Dodo refaite</b> (<see cref="TickSleepBubble_00476CF0"/>) : meme boite
    /// de detection, meme saisie du lanceur a la premiere image, meme tete chercheuse — cap corrige
    /// de <c>0x40</c> par image, avec la <b>meme zone morte</b> repliee sur douze bits et decalee
    /// de <c>0x40</c>, et le meme <c>setge</c>/<c>dec</c>/<c>and 0x80</c> pour le sens du virage —,
    /// meme dix unites par image, meme ondulation, meme anneau d'images, meme bruit toutes les
    /// quatorze images, meme mort a la quarantieme. <b>Ce ne sont pourtant pas les memes octets</b>
    /// : le binaire porte les deux corps.
    /// </para>
    /// <para>
    /// <b>Elle ne seme rien : elle respire.</b> La ou la bulle lachait une eclaboussure par image,
    /// la note fait battre sa propre taille — <c>0x1800</c> plus la moitie d'un sinus de son age
    /// pris sur <b>neuf</b> bits, donc deux fois plus vite que l'ondulation qui, elle, en prend
    /// huit. Les deux echelles <c>+0x28</c> et <c>+0x2a</c> recoivent la meme valeur ;
    /// <c>+0x2c</c> garde ce que le sort y avait laisse.
    /// </para>
    /// <para>
    /// <b>Sur la victime</b>, elle marque puis pose <see cref="ApplyStatusEffect_0053B4B0"/> —
    /// l'effet 8, le seul discret de sa famille. <b>Si l'effet passe</b>, elle lache <b>six</b>
    /// eclats <c>0xbe</c> a la position de la victime, chacun avec sa propre vitesse tiree par
    /// <see cref="SetVectorFromAngles_00532AD0"/> sur deux angles, et met en file la gamme montante
    /// <see cref="s_DelayedSoundScript_005518D8"/>. Qu'il passe ou non, la note s'eteint et relache
    /// le lanceur.
    /// </para>
    /// <para>
    /// <b>L'ordre des tirages est celui du binaire</b>, pas celui de la lecture : le <b>premier</b>
    /// appel au generateur donne le <b>tangage</b> (masque par <c>0x3ff</c>) et le second le lacet.
    /// Les ecrire dans l'ordre des arguments C# inverserait deux tirages du meme flux et changerait
    /// toute la suite.
    /// </para>
    /// </remarks>
    private static void TickSirenSongNote_00477A90(byte* record)
    {
        byte* e = record;

        // La boite de detection, reconstruite sur la pile a chaque image — la MEME que Dodo.
        short* probe = stackalloc short[6] { 0, -60, 60, 100, 0, 120 };

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x80;
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;

            // La hauteur de depart, autour de laquelle elle ondulera.
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
        }

        // Ghidra: LAB_00477ae3 — la tete chercheuse.
        byte* target = *(byte**)(e + 0x78);

        if (target == null)
        {
            // Ghidra: LAB_00477b3f
            *(byte**)(e + 0x78) = PickTargetInCone_00474F00(e, 0xffffffff);
        }
        else
        {
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            int yaw = *(short*)(e + 0x32);
            int error = (Winsys3d.Ordinal_457(dx, dz) - yaw) & 0xfff;

            // LA ZONE MORTE : sous 0x80 de part et d'autre, la note ne corrige pas.
            if (((error + 0x40) & 0xfff) > 0x80)
            {
                int turn = error >= 0x800 ? -0x40 : 0x40;
                *(short*)(e + 0x32) = (short)(turn + yaw);
            }
        }

        // Ghidra: LAB_00477b4d — dix unites par image dans l'axe du cap.
        int heading = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 10;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 10;

        // L'ONDULATION : l'age sur huit bits sert d'angle.
        *(int*)(e + 0x20) =
            ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 8) << 5) + *(int*)(e + 0x48);

        // LE BATTEMENT, qui remplace l'eclaboussure de Dodo : l'age sur NEUF bits, donc deux fois
        // plus vite que l'ondulation.
        int pulse = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 9) >> 1) + 0x1800;
        *(uint*)(e + 0x28) = (uint)(pulse << 0x10 | pulse);

        // La finalisation sert au CONTACT, pas au placement : sa hauteur est jetee.
        int flying = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);       // Ghidra: FUN_00533940
        bool consumed = (*(uint*)e & 0x40000000) != 0;
        *(int*)(e + 0x20) = flying;

        if (!consumed && (*(uint*)(e + 0x90) & 0x8802) == 0)
        {
            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

            if (victim != null)
            {
                // La marque est posee AVANT l'effet ici, la ou la bulle de Dodo la posait apres.
                *(uint*)victim |= 0x40;
                *(short*)(victim + 0x96) = 0x10;

                // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
                *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

                if (LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), (uint*)victim, &ApplyStatusEffect_0053B4B0) == 0)   // Couture legacy ++ (LP-015)
                {
                    *(byte**)(e + 0x70) = null;
                    *(int*)(e + 0x44) = 0;
                    *(int*)(e + 0x48) = 0;
                    *(int*)(e + 0x4c) = 0;
                    *(int*)(e + 0x28) = 0;

                    *(uint*)(e + 0x38) = 0x01f0f0f0;
                    *(short*)(e + 0x12) = 2;
                    *(short*)(e + 0x16) = 2;

                    // SIX ECLATS, chacun avec sa propre vitesse : l'engendreur recopie
                    // +0x50..+0x58 de la source, donc c'est ainsi qu'ils partent en tous sens.
                    for (int remaining = 6; remaining != 0; remaining--)
                    {
                        // L'ORDRE DES TIRAGES EST CELUI DU BINAIRE : le tangage d'abord.
                        int pitch = CrtRand() & 0x3ff;     // Ghidra: FUN_00547140
                        int yaw = CrtRand();

                        SetVectorFromAngles_00532AD0((int*)(e + 0x50), yaw, pitch, 0x20);

                        *(ushort*)(e + 0x14) = (ushort)((CrtRand() & 0x7ff) + 0x1400);

                        // Ils naissent SUR LA VICTIME, pas sur la note.
                        SpawnStatusEffectEntity_0047AC40(
                            e, (int*)(victim + 0x1c), Descriptors00551BC0(), 0xbe, e[0x0b], -1);
                    }

                    Aud.QueueDelayedSoundScript(DelayedSoundScript005518D8(), (int*)(e + 0x1c));
                }

                // Qu'il ait passe ou non, la note s'eteint : les deux chemins tombent sur
                // LAB_00477cfb.
            }
            else
            {
                // Ghidra: LAB_00477cb5 — l'anneau d'images de l'atlas.
                e[0x40] = (byte)(e[0x40] + 1);
                if (e[0x40] >= *(short*)(e + 0x1a)) e[0x40] = 0;

                // Un bruit toutes les quatorze images.
                if (*(short*)(e + 0x10) % 14 == 0)
                {
                    Aud.PlayPositionalSound(0x17, 0x0100, 0x1200, (int*)(e + 0x1c));
                }

                *(short*)(e + 0x10) += 1;
                if (*(short*)(e + 0x10) < 0x28) return;
            }
        }

        // Ghidra: LAB_00477cfb — le decor, l'age, ou la victime : le lanceur est relache.
        *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
        *(uint*)e = 0;
    }

    // =====================================================================
    // ECRASE — ecole 2, indice 8, sous-type 0xa8
    //
    //   0x00477D20   LE SORT. Sous-type 0xa8 de 0x00551c30. DECOMPILE dans Blaze.c.
    //   0x00477E40   LA BOULE, sous-type 0x55 de 0x00552230. 618 octets, ABSENTE de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00477E40.asm.
    //   0x00477DA0   LA MARQUE, sous-type 0x7c de 0x00551f30. DECOMPILEE dans Blaze.c. C'est la
    //                PREMIERE PETITE-FILLE de l'ecole 2 : un enfant qui engendre a son tour.
    //
    // LE SORT EST CELUI DE DODO, OCTET POUR OCTET. Le desassemblage des deux corps ne differe
    // qu'en DEUX points : le sous-type engendre (0x55 au lieu de 0x53) et l'atlas passe
    // (0x00551a78 au lieu de 0x00551b60). Tout le reste — la montee a la hauteur de tete, les
    // douze champs, la gravite 0xd000, le +0x1a = 4, l'effacement — est identique instruction
    // pour instruction. C'est le troisieme mode de reemploi que l'ecole 2 montre, et le plus
    // radical : ici le compilateur a emis deux fois le meme code a deux constantes pres, comme
    // pour la fratrie des Materia, mais entre deux sorts qui n'ont rien a voir.
    //
    // LA BOULE, ELLE, EST RECOMPILEE ET NON RECOPIEE. Elle a la charpente de la bulle de Dodo —
    // meme boite de detection, meme saisie du lanceur, meme tete chercheuse a zone morte, meme
    // dix unites par image, meme ondulation, meme eclaboussure par image, meme anneau, meme mort
    // a la quarantieme — mais l'allocation de registres differe (edi et ebp echanges) et trois
    // choses changent : l'eclaboussure prete +0x1a = 4 au lieu de 1, l'effet est le 10 au lieu du
    // 11, et surtout, quand l'effet passe, elle ENGENDRE.
    //
    // ET CE QU'ELLE ENGENDRE EST UNE MARQUE QUI RETRECIT SUR LA VICTIME. La petite-fille se colle
    // a mi-corps de celle-ci, naît a la taille de sa stature et perd un seizieme de cette taille
    // par image. Comme la flamme des Materia, elle ne compte pas ses images : sa teinte part de
    // 0xf0f0f0, perd 0x101010 par image, et le corps s'efface quand la soustraction tombe pile
    // sur zero — quinze images.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00477d20 @ 0x00477d20 — <b>Ecrase</b> : le sous-type <c>0xa8</c>, creneau 168 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>neuvieme fiche de l'ecole 2</b>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickSleep_00476C70"/>, octet pour octet.</b> Le desassemblage des deux
    /// corps ne differe qu'en <b>deux</b> points : <c>push 55h</c> au lieu de <c>push 53h</c>, et
    /// <c>push 551A78h</c> au lieu de <c>push 551B60h</c>. Rien d'autre — meme montee a la hauteur
    /// de tete du lanceur, memes douze champs, meme gravite <c>0xd000</c>, meme <c>+0x1a = 4</c>,
    /// meme effacement a la fin de la seule image qu'il vit.
    /// </para>
    /// <para>
    /// Le corps est donc ecrit deux fois ici, comme dans le binaire, et pour la meme raison que la
    /// fratrie des Materia : ce qui est transcrit est ce que le compilateur a emis. Ce qui separe
    /// reellement les deux sorts vit dans l'enfant.
    /// </para>
    /// </summary>
    private static void TickCrush_00477D20(byte* record)
    {
        byte* e = record;

        int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));
        *(int*)(e + 0x20) += (int)((uint)height & 0xfffffffe) * 0x800;

        *(short*)(e + 0x2e) = -20;

        *(uint*)(e + 0x38) = 0x00808080;
        *(uint*)(e + 0x28) = 0x20002000;
        *(ushort*)(e + 0x2c) = 0x1f;

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;

        *(ushort*)(e + 0x9a) = 0xd000;

        // La boule n'a PAS de cible en naissant : elle ira la chercher elle-meme.
        *(byte**)(e + 0x78) = null;
        *(ushort*)(e + 0x1a) = 4;

        LegacyPlus.Coutures.ProjectileDeFee(e, () => SpawnStatusEffectEntity_0047AC40(   // Couture legacy ++ (LP-015)
            e, (int*)(e + 0x1c), Descriptors00551A78(), 0x55, e[0x0b], -1));

        // ET IL S'EFFACE.
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00477e40 @ 0x00477e40 — <b>la boule d'Ecrase</b> : le sous-type <c>0x55</c> de
    /// <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c>. 618 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00477E40.asm</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>La charpente est celle de <see cref="TickSleepBubble_00476CF0"/></b> — la meme boite de
    /// detection <c>{0, -60, 60, 100, 0, 120}</c>, la meme saisie du lanceur a la premiere image,
    /// la meme tete chercheuse a zone morte de <c>±0x40</c>, les memes dix unites par image, la
    /// meme ondulation <c>sin(age &lt;&lt; 8) &lt;&lt; 5</c>, la meme eclaboussure semee dans un
    /// cube de <c>0x40</c> unites, le meme anneau d'images, le meme bruit toutes les quatorze
    /// images, la meme mort a la quarantieme. <b>Mais ce n'est pas la meme compilation</b> :
    /// <c>edi</c> et <c>ebp</c> y jouent des roles echanges, ce qu'une copie du meme code n'aurait
    /// pas produit.
    /// </para>
    /// <para>
    /// <b>Trois choses la separent de la bulle</b>, et la troisieme est le sort :
    /// l'eclaboussure prete <c>+0x1a = 4</c> au lieu de <c>1</c> — donc quatre images d'atlas au
    /// lieu d'une ; l'effet est <see cref="ApplyStatusEffect_0053B660"/>, le 10, « le plus banal
    /// des quatre » ; et <b>quand l'effet passe, elle engendre</b> la marque <c>0x7c</c>
    /// (<see cref="TickCrushMark_00477DA0"/>), en lui laissant la victime en <c>+0x70</c>.
    /// </para>
    /// <para>
    /// La marque part par <see cref="SpawnCastEntity_0047AAE0"/> et non par
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> : elle vit donc dans l'<b>autre</b> table
    /// d'entites, celle que la passe raccroche a la grille. <b>C'est la premiere petite-fille de
    /// l'ecole 2</b> — jusqu'ici aucun enfant de sort n'avait engendre a son tour.
    /// </para>
    /// </remarks>
    private static void TickCrushOrb_00477E40(byte* record)
    {
        byte* e = record;

        // La boite de detection, reconstruite sur la pile a chaque image — la MEME que Dodo.
        short* probe = stackalloc short[6] { 0, -60, 60, 100, 0, 120 };

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x80;
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;

            // La hauteur de depart, autour de laquelle elle ondulera.
            *(int*)(e + 0x48) = *(int*)(e + 0x20);
        }

        // Ghidra: LAB_00477e92 — la tete chercheuse.
        byte* target = *(byte**)(e + 0x78);

        if (target == null)
        {
            // Ghidra: LAB_00477eec
            *(byte**)(e + 0x78) = PickTargetInCone_00474F00(e, 0xffffffff);
        }
        else
        {
            int dz = (*(int*)(target + 0x24) - *(int*)(e + 0x24)) >> 0xc;
            int dx = (*(int*)(target + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

            int yaw = *(short*)(e + 0x32);
            int error = (Winsys3d.Ordinal_457(dx, dz) - yaw) & 0xfff;

            // LA ZONE MORTE : sous 0x80 de part et d'autre, la boule ne corrige pas.
            if (((error + 0x40) & 0xfff) > 0x80)
            {
                int turn = error >= 0x800 ? -0x40 : 0x40;
                *(short*)(e + 0x32) = (short)(turn + yaw);
            }
        }

        // Ghidra: LAB_00477efa — dix unites par image dans l'axe du cap.
        int heading = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 10;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 10;

        // L'ONDULATION : l'age, porte a huit bits, sert d'angle.
        *(int*)(e + 0x20) =
            ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 8) << 5) + *(int*)(e + 0x48);

        // L'ECLABOUSSURE, une par image, dans un cube de 0x40 unites — et elle prete QUATRE
        // images d'atlas le temps de l'appel, la ou la bulle de Dodo n'en pretait qu'une.
        {
            short lentFrames = *(short*)(e + 0x1a);
            *(ushort*)(e + 0x1a) = 4;

            int* scatter = stackalloc int[3];
            scatter[0] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x1c);
            scatter[1] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x20);
            scatter[2] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x24);

            SpawnStatusEffectEntity_0047AC40(
                e, scatter, Descriptors00551A78(), 0xbf, e[0x0b], -1);

            *(short*)(e + 0x1a) = lentFrames;
        }

        // La finalisation sert au CONTACT, pas au placement : sa hauteur est jetee.
        int flying = *(int*)(e + 0x20);
        FinalizeEntityPlacement2((uint*)e);       // Ghidra: FUN_00533940
        bool consumed = (*(uint*)e & 0x40000000) != 0;
        *(int*)(e + 0x20) = flying;

        if (!consumed && (*(uint*)(e + 0x90) & 0x8802) == 0)
        {
            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);

            if (victim != null)
            {
                *(short*)(victim + 0x96) = 0x10;
                *(uint*)victim |= 0x40;

                // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
                *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

                if (LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), (uint*)victim, &ApplyStatusEffect_0053B660) == 0)   // Couture legacy ++ (LP-015)
                {
                    // ET ELLE ENGENDRE : la marque part dans l'AUTRE table d'entites.
                    e[0x04] = 2;
                    *(byte**)(e + 0x70) = victim;

                    SpawnCastEntity_0047AAE0(e, 0x7c, e[0x0b], -1);

                    Aud.PlayPositionalSound(0x1e, 0x200, 0x1200, (int*)(e + 0x1c));
                }

                // Qu'il ait passe ou non, la boule s'eteint : les deux chemins tombent sur
                // LAB_00478093.
            }
            else
            {
                // Ghidra: LAB_0047804d — l'anneau d'images de l'atlas.
                e[0x40] = (byte)(e[0x40] + 1);
                if (e[0x40] >= *(short*)(e + 0x1a)) e[0x40] = 0;

                // Un bruit toutes les quatorze images.
                if (*(short*)(e + 0x10) % 14 == 0)
                {
                    Aud.PlayPositionalSound(0x17, 0x0100, 0x1200, (int*)(e + 0x1c));
                }

                *(short*)(e + 0x10) += 1;
                if (*(short*)(e + 0x10) < 0x28) return;
            }
        }

        // Ghidra: LAB_00478093 — le decor, l'age, ou la victime : le lanceur est relache.
        *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00477da0 @ 0x00477da0 — <b>la marque d'Ecrase</b> : le sous-type <c>0x7c</c> de
    /// <see cref="s_StaticEntityHandlers_00551f30"/>, la table de <c>0x005edee0</c>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>. <b>La premiere petite-fille de l'ecole 2</b> : elle
    /// naît d'un enfant de sort, pas d'un sort.
    ///
    /// <para>
    /// <b>Elle se colle a mi-corps de la victime</b> — <c>victime+0x20</c> plus la moitie de sa
    /// stature, rangee en <c>+0x48</c> a la premiere image sous la forme
    /// <c>(hauteur &amp; ~1) &lt;&lt; 11</c> — et la suit en X/Z. Elle naît a la taille de cette
    /// stature (<c>(hauteur &lt;&lt; 12) / 150</c>, multiplication magique <c>0x1B4E81B5</c> puis
    /// <c>sar 4</c>) et <b>perd un seizieme de cette taille de depart par image</b>, dans les trois
    /// axes a la fois : le pas de retrait est fige en <c>+0x12</c> a la premiere image, il ne se
    /// recalcule pas.
    /// </para>
    /// <para>
    /// <b>Elle ne compte pas ses images</b>, comme la flamme des Materia
    /// (<see cref="TickFireMateriaFlame_00477600"/>) : sa teinte part de <c>0xf0f0f0</c>, perd
    /// <c>0x101010</c> par image, et le corps s'efface quand la soustraction tombe <b>pile sur
    /// zero</b>. Quinze images, donc — et son <c>+0x10</c> est mis a <c>1</c> a la premiere pour ne
    /// plus jamais bouger, ce qui sert de garde et non de compteur.
    /// </para>
    /// <para>
    /// Quinze images a un seizieme de retrait par image : elle finit a un seizieme de sa taille de
    /// depart au moment ou elle disparaît. Les deux bornes se repondent.
    /// </para>
    /// </summary>
    private static void TickCrushMark_00477DA0(byte* record)
    {
        byte* e = record;
        byte* victim = *(byte**)(e + 0x70);

        if (*(short*)(e + 0x10) == 0)
        {
            // Une garde, pas un compteur : rien ne le fera plus bouger.
            *(ushort*)(e + 0x10) = 1;

            *(uint*)(e + 0x38) = 0x00f0f0f0;

            int height = StatusBubbleHeight_00538BC0((uint*)victim);

            // A MI-CORPS : la moitie de la stature, en 20.12.
            *(int*)(e + 0x48) = (int)((uint)height & 0xfffffffe) << 0xb;

            // (hauteur << 12) / 150 — multiplication magique 0x1B4E81B5 puis `sar 4`.
            int scale = (height << 0xc) / 150;

            *(short*)(e + 0x2c) = (short)scale;
            *(uint*)(e + 0x28) = (uint)(scale << 0x10 | scale);

            // LE PAS DE RETRAIT, fige ici : un seizieme de la taille de depart.
            *(short*)(e + 0x12) = (short)(scale >> 4);
        }

        *(int*)(e + 0x1c) = *(int*)(victim + 0x1c);
        *(int*)(e + 0x20) = *(int*)(victim + 0x20) + *(int*)(e + 0x48);

        // ELLE RETRECIT, dans les trois axes a la fois.
        *(short*)(e + 0x28) -= *(short*)(e + 0x12);

        short shrunk = *(short*)(e + 0x28);
        *(short*)(e + 0x2a) = shrunk;
        *(short*)(e + 0x2c) = shrunk;

        *(int*)(e + 0x24) = *(int*)(victim + 0x24);

        // LA TEINTE EST SON COMPTE A REBOURS, comme la flamme des Materia : seul un zero exact
        // l'eteint.
        uint tint = *(uint*)(e + 0x38) - 0x00101010;
        *(uint*)(e + 0x38) = tint;

        if (tint == 0) *(uint*)e = 0;
    }

    // =====================================================================
    // CHAMP MAGIQUE — ecole 2, indice 9, sous-type 0xa9
    //
    //   0x004780B0   LE SORT, ET IL EST SEUL. 1 333 octets, ABSENT de Blaze.c ; releve garde sous
    //                Legacy/Raw/Blaze/FUN_004780B0.asm. Vingt-six cibles de saut interne, le plus
    //                gros corps de l'ecole 2 et le premier a n'avoir aucun enfant a lui : les deux
    //                creatures qu'il fait naitre sont deja ecrites — l'eclaboussure 0xbf et le
    //                halo 0x84 (TickExorcismHalo_0047F290), tous deux exerces ailleurs.
    //
    // CE N'EST PAS UN SORT QUI FRAPPE, C'EST UNE SENTINELLE. Il se lance sur un allie
    // (FindTargetAround en mode 0, le lanceur lui-meme faute de voisin), tourne autour de lui, et
    // a chaque image BALAIE LES TROIS TABLES D'ENTITES pour y chercher ce qui porte les marques
    // 0x880 et n'a pas encore ete consomme. Il vise le plus proche, tourne d'un quart de l'ecart
    // vers lui en lacet et en tangage, et quand il est a la fois assez proche (les trois quarts de
    // la stature du porteur) et assez bien pointe (a +-0x200 dans les deux axes), il lache quatre
    // etincelles dessus et lui pose 0x40000000 — le bit qui l'eteint. C'est un pare-projectiles.
    //
    // IL EST ENTRETENU COMME ANGE. Passe la seizieme image, il relache le lanceur et se met a
    // couter : quatre PM toutes les vingt images, et le bouton doit rester tenu (le mot +0x1fc4 de
    // la fiche du personnage croise avec l'etat de sa manette). Ange prelevait deux PM toutes les
    // dix images ; c'est le meme mecanisme a une cadence pres. Un lanceur hors groupe n'a ni
    // manette ni PM : cent-vingt images, et c'est tout.
    //
    // ET IL NE MEURT PAS D'UN COUP : le bit 0x2000000 ne l'efface pas, il le fait RETRECIR d'un
    // quart par image, et c'est en passant sous 0x100 qu'il disparait. Tant qu'il retrecit, il
    // continue de se placer et d'emettre son halo — mais il ne cherche plus rien.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004780b0 @ 0x004780b0 — <b>Champ magique</b> : le sous-type <c>0xa9</c>, creneau
    /// 169 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>dixieme fiche de l'ecole
    /// 2</b>. 1 333 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004780B0.asm</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Le plus gros corps de l'ecole 2, et le premier sans enfant a lui</b> : les deux entites
    /// qu'il fait naitre existent deja — l'eclaboussure <c>0xbf</c> avec l'atlas
    /// <see cref="Descriptors00551BA8"/>, et le halo <c>0x84</c>, qui est
    /// <see cref="TickExorcismHalo_0047F290"/>, deja transcrit et deja exerce.
    /// </para>
    /// <para>
    /// <b>C'est une sentinelle, pas une arme.</b> A chaque image il balaie <b>les trois tables
    /// d'entites</b> — <c>0x005ff400</c> (0x20 emplacements), <c>0x005edee0</c> et
    /// <c>0x005f2d40</c> (0x80 chacune) — en retenant tout ce qui est occupe, porte les deux
    /// marques <c>0x880</c> et n'a pas deja <c>0x40000000</c>. Il garde le plus proche du porteur,
    /// tourne d'<b>un quart de l'ecart</b> vers lui en lacet et en tangage, et tire quand les trois
    /// conditions tiennent ensemble : la distance sous <b>les trois quarts de la stature</b> du
    /// porteur, et les deux ecarts d'angle dans <c>±0x200</c>. Le tir est quatre etincelles a la
    /// position de la cible, plus <c>0x40000000</c> pose sur elle.
    /// </para>
    /// <para>
    /// <b>Il est entretenu comme <see cref="TickAngel_0044DC60"/></b> : passe la seizieme image le
    /// lanceur est relache, et le sort coute <b>quatre PM toutes les vingt images</b> tant que le
    /// bouton reste tenu — le mot <c>+0x1fc4</c> de la fiche du personnage croise avec l'etat de sa
    /// manette. Ange prelevait deux PM toutes les dix ; c'est le meme mecanisme, a la cadence pres.
    /// Un lanceur hors groupe (pas de bit <c>0x800000</c>) n'a ni manette ni PM : il tient cent-
    /// vingt images.
    /// </para>
    /// <para>
    /// <b>Et il ne s'efface pas d'un coup.</b> Le bit <c>0x2000000</c> ne le tue pas : il le fait
    /// retrecir d'un quart par image, et c'est en passant sous <c>0x100</c> qu'il disparait. Tant
    /// qu'il retrecit il continue de se placer et d'emettre son halo, mais il ne cherche plus rien.
    /// </para>
    /// <para>
    /// <b>Un defaut d'origine : il vise le DERNIER examine, pas le plus proche.</b> Le balayage
    /// range les trois ecarts de position dans des emplacements de pile a chaque candidat retenu,
    /// <b>avant</b> la comparaison de distance, et la branche « celui-ci est meilleur » ne garde
    /// que le pointeur et la distance. La direction est donc calculee sur le dernier candidat
    /// parcouru, tandis que la portee et la decision de tirer portent sur le plus proche. Transcrit
    /// tel quel ; voir <c>Documentation/Reference/legacy-bug.md</c>.
    /// </para>
    /// </remarks>
    private static void TickMagicField_004780B0(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            // L'EXTINCTION : un quart de moins par image, et la disparition sous 0x100.
            short fading = *(short*)(e + 0x28);
            fading = (short)(fading - (short)(fading >> 2));

            *(short*)(e + 0x28) = fading;
            *(short*)(e + 0x2a) = fading;
            *(short*)(e + 0x2c) = fading;

            if (fading < 0x100)
            {
                *(uint*)e = 0;
                return;
            }

            // Sinon il tombe dans la queue commune — il se place et emet encore, sans chercher.
        }
        else
        {
            // Ghidra: LAB_004780f4
            byte* carrier;

            if (*(short*)(e + 0x10) == 0)
            {
                // Le lanceur est immobilise le temps de la levee.
                *(uint*)(*(byte**)(e + 0x74)) |= 0x80;

                e[0x36] |= 6;
                *(short*)(e + 0x2e) = -20;

                // Faute de voisin dans son camp, elle rend le lanceur : le champ se pose toujours.
                carrier = FindTargetAround_00474BA0(e, 0);

                *(byte**)(e + 0x78) = carrier;
                *(byte**)(e + 0x70) = carrier;

                // La stature du porteur : elle servira de hauteur, de portee et de rayon.
                *(short*)(e + 0x12) = (short)StatusBubbleHeight_00538BC0((uint*)carrier);

                *(int*)(e + 0x28) = 0;

                // Il naît pointe comme son porteur, en lacet comme en tangage.
                *(short*)(e + 0x14) = *(short*)(carrier + 0x32);
                *(short*)(e + 0x16) = *(short*)(carrier + 0x34);
                *(short*)(e + 0x32) = *(short*)(carrier + 0x32);
                *(short*)(e + 0x34) = *(short*)(carrier + 0x34);

                *(ushort*)(e + 0x28) = 0;
                *(int*)(e + 0x38) = 0;

                Aud.PlayPositionalSound(0x0d, 0x800, 0x1200, (int*)(e + 0x1c));
            }
            else
            {
                // Ghidra: LAB_0047816b
                carrier = *(byte**)(e + 0x78);
            }

            // Ghidra: LAB_0047816e — la hauteur, recollee sur le porteur a chaque image : la
            // moitie de sa stature au-dessus de ses pieds.
            int height = *(short*)(e + 0x12);
            *(int*)(e + 0x20) = ((height & ~1) << 0xb) + *(int*)(carrier + 0x20);

            if (*(short*)(e + 0x10) < 0x10)
            {
                // LA LEVEE, seize images : le champ grossit de 0xc0 et s'eclaircit de 0x080808 par
                // image, jusqu'a la butee 0x808080.
                *(short*)(e + 0x28) += 0xc0;

                short grown = *(short*)(e + 0x28);
                *(short*)(e + 0x2a) = grown;
                *(short*)(e + 0x2c) = grown;

                uint tint = *(uint*)(e + 0x38);
                if (tint < 0x00808080) *(uint*)(e + 0x38) = tint + 0x00080808;
            }
            else
            {
                // Ghidra: LAB_004781b3 — le lanceur est relache, et le champ se met a couter.
                *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;

                // IL RESPIRE : 0xc00 moins le huitieme d'un cosinus de son age.
                int breath =
                    0xc00 - (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10) << 8) >> 3);

                *(short*)(e + 0x28) = (short)breath;
                *(short*)(e + 0x2a) = (short)breath;
                *(short*)(e + 0x2c) = (short)breath;

                if ((*(uint*)e & 0x800000) != 0)
                {
                    // L'ENTRETIEN, le meme que celui d'Ange a la cadence pres.
                    byte* caster = *(byte**)(e + 0x74);
                    uint index = ((uint*)caster)[2] & 0xff;
                    byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

                    int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
                    ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

                    if ((Input.CombatMask(*(ushort*)(perChar + 0x1fc4)) & pad) == 0)
                    {
                        *(uint*)e |= 0x2000000;               // le bouton est relache
                    }

                    // Ghidra: LAB_0047822c — la ponction, cadencee sur l'horloge du monde.
                    if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
                    {
                        short mana = *(short*)(perChar + 0x14e);

                        if (mana >= 4)
                        {
                            *(short*)(perChar + 0x14e) = (short)(mana - 4);
                        }
                        else
                        {
                            // Ghidra: LAB_00478257
                            *(ushort*)(perChar + 0x14e) = 0;
                            *(uint*)e |= 0x2000000;
                        }
                    }
                }
                else if (*(short*)(e + 0x10) >= 0x78)
                {
                    // Ghidra: LAB_00478262 — un lanceur hors groupe tient cent-vingt images.
                    *(uint*)e |= 0x2000000;
                }

                // Ghidra: LAB_00478270 — et dans tous les cas, le lanceur doit tenir debout.
                if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
            }

            // ============================================================================
            // Ghidra: LAB_00478283 — LE BALAYAGE DES TROIS TABLES.
            // ============================================================================
            byte* best = null;
            int bestDistance = 0x190;

            // Les ecarts du DERNIER candidat retenu — et c'est bien ceux-la que la visee emploie.
            // Voir la note de defaut d'origine portee sur la fonction.
            int lastDx = 0;
            int lastDy = 0;
            int lastDz = 0;

            for (int table = 3; table != 0; table--)
            {
                // Ghidra: LAB_0047829d — un enchainement de `dec`/`je` dont la branche par defaut
                // (LAB_004782d6) est inatteignable : le compteur ne vaut jamais que 3, 2 ou 1.
                byte* slot;
                int count;

                switch (table)
                {
                    case 3: slot = Mem.Entity(0x005ff400); count = 0x20; break;
                    case 2: slot = Mem.Entity(0x005edee0); count = 0x80; break;
                    default: slot = Mem.Entity(0x005f2d40); count = 0x80; break;
                }

                // Ghidra: LAB_004782e6
                for (; count != 0; count--, slot += 0x9c)
                {
                    uint state = *(uint*)slot;

                    if ((state & 0x80000000) == 0) continue;   // l'emplacement est libre
                    if ((state & 0x880) != 0x880) continue;    // les deux marques sont exigees
                    if ((state & 0x40000000) != 0) continue;   // deja consomme

                    lastDx = (*(int*)(slot + 0x1c) - *(int*)(carrier + 0x1c)) >> 0xc;
                    lastDy = (*(int*)(slot + 0x20) - *(int*)(carrier + 0x20)) >> 0xc;
                    lastDz = (*(int*)(slot + 0x24) - *(int*)(carrier + 0x24)) >> 0xc;

                    int distance = (int)Winsys3d.Ordinal_442(
                        (uint)(lastDx * lastDx + lastDy * lastDy + lastDz * lastDz));

                    if (bestDistance > distance)
                    {
                        bestDistance = distance;
                        best = slot;
                    }
                }
            }

            if (best != null)
            {
                // LA VISEE, sur les ecarts du DERNIER candidat — le defaut d'origine.
                int flat = (int)Winsys3d.Ordinal_442((uint)(lastDx * lastDx + lastDz * lastDz));

                *(short*)(e + 0x14) = (short)(Winsys3d.Ordinal_457(lastDx, lastDz) + 0x400);
                *(short*)(e + 0x16) = (short)Winsys3d.Ordinal_457(lastDy, flat);
            }
            else
            {
                // Ghidra: LAB_004783e4 — sans cible, le champ se range sur son porteur, a
                // quatre-vingt-dix degres de son cap.
                *(short*)(e + 0x14) = (short)(*(short*)(carrier + 0x32) + 0x400);
                *(short*)(e + 0x16) = *(short*)(carrier + 0x34);
            }

            // Ghidra: LAB_004783f9 — LE VIRAGE : un quart de l'ecart par image, dans les deux axes.
            int currentYaw = *(short*)(e + 0x32);
            int currentPitch = *(short*)(e + 0x34);

            int yawError = (*(ushort*)(e + 0x14) - currentYaw) & 0xfff;
            int pitchError = (*(ushort*)(e + 0x16) - currentPitch) & 0xfff;

            int yawStep = yawError < 0x800 ? yawError >> 2 : -((0x1000 - yawError) >> 2);
            *(short*)(e + 0x32) = (short)(currentYaw + yawStep);

            int pitchStep = pitchError < 0x800 ? pitchError >> 2 : -((0x1000 - pitchError) >> 2);
            *(short*)(e + 0x34) = (short)(currentPitch + pitchStep);

            // Ghidra: LAB_00478453 — LE TIR. Trois conditions, et l'ecart d'angle teste est celui
            // d'AVANT le virage.
            if (best != null
                && bestDistance < height - (height >> 2)
                && ((yawError + 0x200) & 0xfff) < 0x400
                && ((pitchError + 0x200) & 0xfff) < 0x400)
            {
                // Les trois champs que les etincelles vont emprunter, mis de cote. Le mot long en
                // +0x2c couvre aussi +0x2e, que la premiere image avait pose a -20.
                int savedScale = *(int*)(e + 0x28);
                int savedDepth = *(int*)(e + 0x2c);
                int savedTint = *(int*)(e + 0x38);

                int* at = (int*)(best + 0x1c);

                *(ushort*)(e + 0x1a) = 1;
                *(ushort*)(e + 0x9a) = 0x2000;
                *(uint*)(e + 0x38) = 0x00f0f0f0;
                *(uint*)(e + 0x28) = 0x10001000;
                *(ushort*)(e + 0x2c) = 0x1f;

                // QUATRE ETINCELLES, chacune avec sa propre vitesse a deux angles et sa propre
                // celerite : l'engendreur recopie +0x50..+0x58 de la source.
                for (int remaining = 4; remaining != 0; remaining--)
                {
                    int yaw = (CrtRand() & 0x1ff) + *(short*)(e + 0x32) - 0x500;
                    int pitch = (CrtRand() & 0x1ff) + *(short*)(e + 0x34) - 0x100;
                    int speed = (CrtRand() & 0x0f) + 0x14;

                    SetVectorFromAngles_00532AD0((int*)(e + 0x50), yaw, pitch, speed);

                    SpawnStatusEffectEntity_0047AC40(
                        e, at, Descriptors00551BA8(), 0xbf, e[0x0b], -1);
                }

                *(int*)(e + 0x2c) = savedDepth;
                *(int*)(e + 0x28) = savedScale;
                *(int*)(e + 0x38) = savedTint;

                // ET LA CIBLE EST ETEINTE : c'est tout ce que le champ lui fait.
                *(uint*)best |= 0x40000000;
            }
        }

        // ================================================================================
        // Ghidra: LAB_0047856f — LA QUEUE, commune a tous les chemins qui ne sont pas morts.
        // ================================================================================
        int radius = *(short*)(e + 0x12);
        radius -= radius >> 2;                       // les trois quarts de la stature

        byte* holder = *(byte**)(e + 0x78);

        int* offset = stackalloc int[3];
        SetVectorFromAngles_00532AD0(
            offset, *(short*)(e + 0x32) - 0x400, *(short*)(e + 0x34), radius);

        // Il tourne autour de son porteur en X/Z ; en hauteur il ne fait qu'AJOUTER, la cote ayant
        // deja ete posee plus haut.
        *(int*)(e + 0x1c) = *(int*)(holder + 0x1c) + offset[0];
        *(int*)(e + 0x20) += offset[1];
        *(int*)(e + 0x24) = *(int*)(holder + 0x24) + offset[2];

        e[0x04] = 0x0e;
        SpawnCastEntity_0047AAE0(e, 0x84, e[0x0b], -1);

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// <c>&amp;DAT_00551bc8</c>, l'atlas du <b>Coussin d'air</b> — le <b>sixieme</b> enregistrement
    /// de la plage <see cref="s_Descriptors_00551BA0"/>, pas une table a lui, comme
    /// <see cref="Descriptors00551BA8"/> en est le deuxieme et <see cref="Descriptors00551BC0"/> le
    /// cinquieme. Il suit ce dernier immediatement et reprend les octets <c>01 02</c> que celui-la
    /// n'a pas.
    /// </summary>
    private static byte* Descriptors00551BC8() => Descriptors00551BA0() + 0x28;

    // =====================================================================
    // COUSSIN D'AIR — ecole 2, indice 10, sous-type 0xaa
    //
    //   0x004785F0   LE SORT, ET IL EST SEUL. 205 octets, ABSENT de Blaze.c ; releve garde sous
    //                Legacy/Raw/Blaze/FUN_004785F0.asm. Un seul saut interne.
    //
    // APRES LE PLUS GROS CORPS DE L'ECOLE, LE PLUS PETIT. Champ magique faisait 1 333 octets et
    // vingt-six sauts ; celui-ci en fait 205 et un seul. Et comme lui, il n'a AUCUN enfant a
    // ecrire : la seule creature qu'il fait naitre est l'etincelle 0xbf, deja transcrite
    // (TickTrailSpark_00475F60) et deja exercee.
    //
    // C'EST UN SOUFFLE SOUS LES PIEDS D'UN ALLIE. Il se pose par FindTargetAround en mode 0 — le
    // lanceur lui-meme faute de voisin, comme Vitesse et les Materia —, puis pendant vingt images
    // il seme UNE ETINCELLE PAR IMAGE a la position du porteur, chacune avec une vitesse
    // horizontale de seize unites dans une direction tiree au sort et une poussee verticale fixe
    // de 0x1e000. A la vingtieme il pose l'effet 28 et s'efface.
    //
    // L'effet 28 n'a ni masque d'immunite ni jet de sauvegarde — comme celui de Vitesse : c'est
    // un bienfait, il ne se refuse pas.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004785f0 @ 0x004785f0 — <b>Coussin d'air</b> : le sous-type <c>0xaa</c>, creneau
    /// 170 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>onzieme fiche de l'ecole
    /// 2</b>. 205 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004785F0.asm</c>.
    ///
    /// <para>
    /// <b>Le plus court corps de l'ecole, et le second d'affilee sans enfant a lui</b> : la seule
    /// creature qu'il fait naitre est l'etincelle <c>0xbf</c>, qui est
    /// <see cref="TickTrailSpark_00475F60"/> — deja transcrite, deja exercee.
    /// </para>
    /// <para>
    /// <b>Vingt et une images, et rien d'autre a faire que souffler.</b> A la premiere il se pose
    /// sur un allie (<see cref="FindTargetAround_00474BA0"/> en mode 0, le lanceur lui-meme faute
    /// de voisin) et se donne une gravite <c>0x4000</c> que les etincelles heriteront. A chacune —
    /// y compris la premiere — il seme <b>une</b> etincelle a la position du porteur, avec seize
    /// unites de vitesse horizontale dans une direction tiree au sort et une poussee verticale fixe
    /// de <c>0x1e000</c> : le souffle monte tout en s'evasant.
    /// </para>
    /// <para>
    /// <b>A la vingtieme il pose l'effet 28</b> (<see cref="ApplyStatusEffect_0053C2A0"/>, duree en
    /// <c>+0xb8</c>, bit <c>0x400000</c>) et s'efface. Comme l'effet 19 de Vitesse, celui-la n'a ni
    /// masque d'immunite ni jet de sauvegarde : c'est un bienfait, il ne se refuse pas.
    /// </para>
    /// <para>
    /// <c>+0x12</c> recoit la stature du porteur a la premiere image et <b>rien ne la relit</b> —
    /// ni ici, ni dans l'etincelle, qui ne recopie ce champ que par la copie en bloc de
    /// l'engendreur. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickAirCushion_004785F0(byte* record)
    {
        byte* e = record;
        byte* carrier;

        if (*(short*)(e + 0x10) == 0)
        {
            // Faute de voisin dans son camp, elle rend le lanceur : le coussin se pose toujours.
            carrier = LegacyPlus.Coutures.CibleDeFee(e, FindTargetAround_00474BA0(e, 0));   // Couture legacy ++ (LP-015)

            *(byte**)(e + 0x78) = carrier;
            *(byte**)(e + 0x70) = carrier;

            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(uint*)(e + 0x28) = 0x10001000;
            *(ushort*)(e + 0x2c) = 0x1f;

            // La gravite que les etincelles heriteront par la copie en bloc.
            *(ushort*)(e + 0x9a) = 0x4000;
            *(ushort*)(e + 0x1a) = 1;

            // Posee, et jamais relue — ni ici ni ailleurs.
            *(short*)(e + 0x12) = (short)StatusBubbleHeight_00538BC0((uint*)carrier);
        }
        else
        {
            // Ghidra: LAB_00478643
            carrier = *(byte**)(e + 0x78);
        }

        // Ghidra: LAB_00478646 — UNE ETINCELLE PAR IMAGE, a la position du porteur.
        int angle = CrtRand();                       // Ghidra: FUN_00547140

        *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(angle) << 4;
        *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(angle) << 4;
        *(int*)(e + 0x54) = 0x1e000;                 // et la poussee vers le haut, elle, est fixe

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(carrier + 0x1c), Descriptors00551BC8(), 0xbf, e[0x0b], -1);

        if (*(short*)(e + 0x10) >= 0x14)
        {
            // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT.
            *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

            LegacyPlus.Coutures.EffetDeFee(*(uint**)(e + 0x74), *(uint**)(e + 0x78), &ApplyStatusEffect_0053C2A0);   // Couture legacy ++ (LP-015)

            *(uint*)e = 0;
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_004786c0 @ 0x004786c0 — <b>Escargot</b> : le sous-type <c>0xab</c>, creneau 171
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>douzieme fiche de l'ecole 2</b>.
    /// 1 364 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004786C0.asm</c>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickMagicField_004780B0"/>, a trois differences pres</b>, et le diff des
    /// deux desassemblages les donne toutes les trois — le reste n'etant que des adresses de saut
    /// et des registres echanges :
    /// </para>
    /// <list type="number">
    /// <item><description>
    /// <b>le son de la premiere image</b> : <c>0x31</c> a la hauteur <c>0x195</c>, la ou Champ
    /// magique joue <c>0x0d</c> a <c>0x800</c> ;
    /// </description></item>
    /// <item><description>
    /// <b>un second son a la douzieme image exactement</b> — <c>0x16</c> a la hauteur <c>0x653</c>,
    /// sur un test d'egalite, pas de seuil. Champ magique n'a pas cette image-la ;
    /// </description></item>
    /// <item><description>
    /// <b>la classe d'entites chassee</b> : <c>(etat &amp; 0xe80) == 0x680</c> au lieu de
    /// <c>(etat &amp; 0x880) == 0x880</c>. Les deux sentinelles ne guettent donc <b>pas la meme
    /// chose</b> : celle-ci exige <c>0x400</c> et <c>0x200</c> et refuse <c>0x800</c>, que l'autre
    /// exige au contraire. C'est la seule difference qui change ce que le sort fait.
    /// </description></item>
    /// </list>
    /// <para>
    /// <b>Elle est recompilee, pas recopiee</b> : <c>ecx</c> et <c>edx</c> y jouent des roles
    /// echanges par rapport a sa jumelle, ce qu'une copie du meme code n'aurait pas produit — le
    /// meme signe que la boule d'Ecrase porte vis-a-vis de la bulle de Dodo.
    /// </para>
    /// <para>
    /// Tout le reste est mot pour mot celui de Champ magique : la pose sur un allie, la levee en
    /// seize images, l'entretien a quatre PM toutes les vingt, le balayage des trois tables, la
    /// visee au quart de l'ecart, le tir a quatre etincelles, l'extinction par retrecissement, et
    /// <b>le meme defaut d'origine</b> — la visee porte sur le dernier candidat balaye et non sur
    /// le plus proche (LB-026).
    /// </para>
    /// </summary>
    private static void TickSnail_004786C0(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            // L'EXTINCTION : un quart de moins par image, et la disparition sous 0x100.
            short fading = *(short*)(e + 0x28);
            fading = (short)(fading - (short)(fading >> 2));

            *(short*)(e + 0x28) = fading;
            *(short*)(e + 0x2a) = fading;
            *(short*)(e + 0x2c) = fading;

            if (fading < 0x100)
            {
                *(uint*)e = 0;
                return;
            }
        }
        else
        {
            // Ghidra: LAB_00478704
            byte* carrier;

            if (*(short*)(e + 0x10) == 0)
            {
                *(uint*)(*(byte**)(e + 0x74)) |= 0x80;

                e[0x36] |= 6;
                *(short*)(e + 0x2e) = -20;

                carrier = FindTargetAround_00474BA0(e, 0);

                *(byte**)(e + 0x78) = carrier;
                *(byte**)(e + 0x70) = carrier;

                *(short*)(e + 0x12) = (short)StatusBubbleHeight_00538BC0((uint*)carrier);

                *(int*)(e + 0x28) = 0;

                *(short*)(e + 0x14) = *(short*)(carrier + 0x32);
                *(short*)(e + 0x16) = *(short*)(carrier + 0x34);
                *(short*)(e + 0x32) = *(short*)(carrier + 0x32);
                *(short*)(e + 0x34) = *(short*)(carrier + 0x34);

                *(ushort*)(e + 0x28) = 0;
                *(int*)(e + 0x38) = 0;

                // LA PREMIERE DIFFERENCE : un autre son, et bien plus grave.
                Aud.PlayPositionalSound(0x31, 0x195, 0x1200, (int*)(e + 0x1c));
            }
            else
            {
                // Ghidra: LAB_0047877b
                carrier = *(byte**)(e + 0x78);
            }

            // Ghidra: LAB_0047877e — la hauteur, recollee sur le porteur a chaque image.
            int height = *(short*)(e + 0x12);
            *(int*)(e + 0x20) = ((height & ~1) << 0xb) + *(int*)(carrier + 0x20);

            if (*(short*)(e + 0x10) < 0x10)
            {
                // LA DEUXIEME DIFFERENCE : un second son a la DOUZIEME image exactement — un test
                // d'egalite, pas un seuil. Champ magique n'a pas cette image-la.
                if (*(short*)(e + 0x10) == 0x0c)
                {
                    Aud.PlayPositionalSound(0x16, 0x653, 0x1200, (int*)(e + 0x1c));
                }

                // Ghidra: LAB_004787b7 — LA LEVEE, seize images.
                *(short*)(e + 0x28) += 0xc0;

                short grown = *(short*)(e + 0x28);
                *(short*)(e + 0x2a) = grown;
                *(short*)(e + 0x2c) = grown;

                uint tint = *(uint*)(e + 0x38);
                if (tint < 0x00808080) *(uint*)(e + 0x38) = tint + 0x00080808;
            }
            else
            {
                // Ghidra: LAB_004787e4 — le lanceur est relache, et le sort se met a couter.
                *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;

                int breath =
                    0xc00 - (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10) << 8) >> 3);

                *(short*)(e + 0x28) = (short)breath;
                *(short*)(e + 0x2a) = (short)breath;
                *(short*)(e + 0x2c) = (short)breath;

                if ((*(uint*)e & 0x800000) != 0)
                {
                    byte* caster = *(byte**)(e + 0x74);
                    uint index = ((uint*)caster)[2] & 0xff;
                    byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

                    int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
                    ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

                    if ((Input.CombatMask(*(ushort*)(perChar + 0x1fc4)) & pad) == 0)
                    {
                        *(uint*)e |= 0x2000000;               // le bouton est relache
                    }

                    // Ghidra: LAB_0047885b — quatre PM toutes les vingt images.
                    if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
                    {
                        short mana = *(short*)(perChar + 0x14e);

                        if (mana >= 4)
                        {
                            *(short*)(perChar + 0x14e) = (short)(mana - 4);
                        }
                        else
                        {
                            // Ghidra: LAB_00478886
                            *(ushort*)(perChar + 0x14e) = 0;
                            *(uint*)e |= 0x2000000;
                        }
                    }
                }
                else if (*(short*)(e + 0x10) >= 0x78)
                {
                    // Ghidra: LAB_00478891 — un lanceur hors groupe tient cent-vingt images.
                    *(uint*)e |= 0x2000000;
                }

                // Ghidra: LAB_0047889f — et dans tous les cas, le lanceur doit tenir debout.
                if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
            }

            // ============================================================================
            // Ghidra: LAB_004788b2 — LE BALAYAGE DES TROIS TABLES.
            // ============================================================================
            byte* best = null;
            int bestDistance = 0x190;

            // Les ecarts du DERNIER candidat retenu — et c'est bien ceux-la que la visee emploie
            // (LB-026), tout comme chez Champ magique.
            int lastDx = 0;
            int lastDy = 0;
            int lastDz = 0;

            for (int table = 3; table != 0; table--)
            {
                // Ghidra: LAB_004788cc — la branche par defaut (LAB_00478905) est inatteignable.
                byte* slot;
                int count;

                switch (table)
                {
                    case 3: slot = Mem.Entity(0x005ff400); count = 0x20; break;
                    case 2: slot = Mem.Entity(0x005edee0); count = 0x80; break;
                    default: slot = Mem.Entity(0x005f2d40); count = 0x80; break;
                }

                // Ghidra: LAB_00478915
                for (; count != 0; count--, slot += 0x9c)
                {
                    uint state = *(uint*)slot;

                    if ((state & 0x80000000) == 0) continue;   // l'emplacement est libre

                    // LA TROISIEME DIFFERENCE, ET LA SEULE QUI CHANGE CE QUE LE SORT FAIT :
                    // Escargot exige 0x400 et 0x200 et REFUSE 0x800, que Champ magique exige.
                    // Les deux sentinelles ne guettent pas la meme classe d'entites.
                    if ((state & 0xe80) != 0x680) continue;

                    if ((state & 0x40000000) != 0) continue;   // deja consomme

                    lastDx = (*(int*)(slot + 0x1c) - *(int*)(carrier + 0x1c)) >> 0xc;
                    lastDy = (*(int*)(slot + 0x20) - *(int*)(carrier + 0x20)) >> 0xc;
                    lastDz = (*(int*)(slot + 0x24) - *(int*)(carrier + 0x24)) >> 0xc;

                    int distance = (int)Winsys3d.Ordinal_442(
                        (uint)(lastDx * lastDx + lastDy * lastDy + lastDz * lastDz));

                    if (bestDistance > distance)
                    {
                        bestDistance = distance;
                        best = slot;
                    }
                }
            }

            if (best != null)
            {
                // LA VISEE, sur les ecarts du DERNIER candidat — le defaut d'origine LB-026.
                int flat = (int)Winsys3d.Ordinal_442((uint)(lastDx * lastDx + lastDz * lastDz));

                *(short*)(e + 0x14) = (short)(Winsys3d.Ordinal_457(lastDx, lastDz) + 0x400);
                *(short*)(e + 0x16) = (short)Winsys3d.Ordinal_457(lastDy, flat);
            }
            else
            {
                // Ghidra: LAB_00478a13
                *(short*)(e + 0x14) = (short)(*(short*)(carrier + 0x32) + 0x400);
                *(short*)(e + 0x16) = *(short*)(carrier + 0x34);
            }

            // Ghidra: LAB_00478a28 — LE VIRAGE : un quart de l'ecart par image, dans les deux axes.
            int currentYaw = *(short*)(e + 0x32);
            int currentPitch = *(short*)(e + 0x34);

            int yawError = (*(ushort*)(e + 0x14) - currentYaw) & 0xfff;
            int pitchError = (*(ushort*)(e + 0x16) - currentPitch) & 0xfff;

            int yawStep = yawError < 0x800 ? yawError >> 2 : -((0x1000 - yawError) >> 2);
            *(short*)(e + 0x32) = (short)(currentYaw + yawStep);

            int pitchStep = pitchError < 0x800 ? pitchError >> 2 : -((0x1000 - pitchError) >> 2);
            *(short*)(e + 0x34) = (short)(currentPitch + pitchStep);

            // Ghidra: LAB_00478a82 — LE TIR, aux memes trois conditions.
            if (best != null
                && bestDistance < height - (height >> 2)
                && ((yawError + 0x200) & 0xfff) < 0x400
                && ((pitchError + 0x200) & 0xfff) < 0x400)
            {
                int savedScale = *(int*)(e + 0x28);
                int savedDepth = *(int*)(e + 0x2c);
                int savedTint = *(int*)(e + 0x38);

                int* at = (int*)(best + 0x1c);

                *(ushort*)(e + 0x1a) = 1;
                *(ushort*)(e + 0x9a) = 0x2000;
                *(uint*)(e + 0x38) = 0x00f0f0f0;
                *(uint*)(e + 0x28) = 0x10001000;
                *(ushort*)(e + 0x2c) = 0x1f;

                // Ghidra: LAB_00478b18 — quatre etincelles.
                for (int remaining = 4; remaining != 0; remaining--)
                {
                    int yaw = (CrtRand() & 0x1ff) + *(short*)(e + 0x32) - 0x500;
                    int pitch = (CrtRand() & 0x1ff) + *(short*)(e + 0x34) - 0x100;
                    int speed = (CrtRand() & 0x0f) + 0x14;

                    SetVectorFromAngles_00532AD0((int*)(e + 0x50), yaw, pitch, speed);

                    SpawnStatusEffectEntity_0047AC40(
                        e, at, Descriptors00551BA8(), 0xbf, e[0x0b], -1);
                }

                *(int*)(e + 0x2c) = savedDepth;
                *(int*)(e + 0x28) = savedScale;
                *(int*)(e + 0x38) = savedTint;

                // ET LA CIBLE EST ETEINTE.
                *(uint*)best |= 0x40000000;
            }
        }

        // ================================================================================
        // Ghidra: LAB_00478b9e — LA QUEUE, commune a tous les chemins qui ne sont pas morts.
        // ================================================================================
        int radius = *(short*)(e + 0x12);
        radius -= radius >> 2;                       // les trois quarts de la stature

        byte* holder = *(byte**)(e + 0x78);

        int* offset = stackalloc int[3];
        SetVectorFromAngles_00532AD0(
            offset, *(short*)(e + 0x32) - 0x400, *(short*)(e + 0x34), radius);

        *(int*)(e + 0x1c) = *(int*)(holder + 0x1c) + offset[0];
        *(int*)(e + 0x20) += offset[1];
        *(int*)(e + 0x24) = *(int*)(holder + 0x24) + offset[2];

        e[0x04] = 0x0e;
        SpawnCastEntity_0047AAE0(e, 0x84, e[0x0b], -1);

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00478c20 @ 0x00478c20 — <b>Ralenti</b> : le sous-type <c>0xac</c>, creneau 172
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>treizieme fiche de l'ecole 2</b>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est Gros sabots, repete.</b> Elle engendre le meme sous-type <c>0x7e</c> —
    /// <see cref="TickHeavyHoofStomp_004770A0"/>, deja transcrit — et remplit pour lui les memes
    /// champs, mais <b>toutes les huit images</b> au lieu d'une fois : le test est
    /// <c>(age &amp; 7) == 0</c>, et rien ne borne le sort. Il ne s'arrete que si le balayage ne
    /// trouve plus personne en face, ou si le lanceur porte <c>0x40000000</c>. Aucun enfant a
    /// ecrire, donc — le quatrieme d'affilee dans ce cas.
    /// </para>
    /// <para>
    /// <b>Trois ecarts avec <see cref="TickHeavyHoof_00476FD0"/></b>, tous les trois lisibles au
    /// desassembleur :
    /// </para>
    /// <list type="number">
    /// <item><description>
    /// <b>le sabot naît depuis le SOL, pas depuis le sort</b> : la hauteur est
    /// <c>(stature + 300) &lt;&lt; 12</c> ajoutee a <c>+0x60</c>, la cote du sol, la ou Gros sabots
    /// l'ajoutait a son propre <c>+0x20</c> ;
    /// </description></item>
    /// <item><description>
    /// <b>la taille visee est divisee par 100</b> et non par 75 — <c>imul 0x51EB851F</c> puis
    /// <c>sar 5</c>, la ou Gros sabots emploie <c>0x1B4E81B5</c> puis <c>sar 3</c>. Les sabots de
    /// Ralenti sont donc les trois quarts de celui de Gros sabots ;
    /// </description></item>
    /// <item><description>
    /// <c>+0x14</c> recoit <c>0x80</c> au lieu de zero.
    /// </description></item>
    /// </list>
    /// <para>
    /// <b>Une consequence des deux corps, notee sans avoir ete observee en jeu</b> : le sabot
    /// herite le bit <c>0x2000000</c> de l'enregistrement de son pere (<c>+0x6c</c>), et ne s'en
    /// sert que pour mourir apres une resistance. Gros sabots pose ce bit juste apres avoir
    /// engendre ; Ralenti ne le pose qu'a la fin du sort. Un sabot de Ralenti dont la victime
    /// resiste attend donc cette fin-la pour disparaitre, au lieu de l'image suivante.
    /// </para>
    /// </summary>
    private static void TickSlow_00478C20(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00478c35 — la mise en place, a la premiere image seulement.
        if (*(ushort*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;

            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = 0;

            *(ushort*)(e + 0x9a) = 0x6000;
            e[0x04] = 0x31;
        }

        // Ghidra: LAB_00478c5f — UN SABOT TOUTES LES HUIT IMAGES.
        if ((*(ushort*)(e + 0x10) & 7) == 0)
        {
            byte* target = FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 1);

            if (target == null)
            {
                // Ghidra: LAB_00478ce3 — plus personne en face : le sort s'arrete.
                *(uint*)e |= 0x2000000;
            }
            else
            {
                *(byte**)(e + 0x78) = target;

                int height = StatusBubbleHeight_00538BC0((uint*)target);

                *(ushort*)(e + 0x14) = 0x80;

                // TROIS CENTS UNITES AU-DESSUS DE SA TETE, MESUREES DEPUIS LE SOL : +0x60, la ou
                // Gros sabots partait de son propre +0x20.
                *(int*)(e + 0x20) = ((height + 0x12c) << 0xc) + *(int*)(e + 0x60);

                // (stature << 12) / 100 — multiplication magique 0x51EB851F puis `sar 5`. Gros
                // sabots divise par 75 ; ses sabots a elle sont donc plus petits d'un quart.
                *(short*)(e + 0x12) = (short)((height << 0xc) / 100);

                *(short*)(e + 0x32) = *(short*)(target + 0x32);
                *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
                *(int*)(e + 0x24) = *(int*)(target + 0x24);

                SpawnCastEntity_0047AAE0(e, 0x7e, e[0x0b], -1);
            }
        }

        // Ghidra: LAB_00478ce9 — et le lanceur doit tenir : son 0x40000000 arrete tout.
        if ((*(uint*)(*(byte**)(e + 0x74)) & 0x40000000) != 0) *(uint*)e |= 0x2000000;

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00478d10 @ 0x00478d10 — <b>Turbo</b> : le sous-type <c>0xad</c>, creneau 173 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>quatorzieme fiche de l'ecole 2</b>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickHaste_00477360"/> dans une boucle</b> : le meme corps mot pour mot —
    /// les deux anneaux <c>0x81</c>, la meme division par 150, le meme couple de tailles
    /// <c>(base, 1,5x)</c> puis <c>(1,5x, 0,75x)</c>, le meme <c>+0x16</c> qui distingue lequel des
    /// deux posera l'effet, le meme son <c>0x0d</c> a <c>0x2d4</c> — enveloppe dans un
    /// <c>do…while</c> qui le rejoue tant qu'il trouve un allie de plus. <b>Vitesse hate un
    /// compagnon ; Turbo hate tout le groupe, en une seule image.</b>
    /// </para>
    /// <para>
    /// <b>Et c'est le marqueur <c>+0x74</c> qui fait avancer la boucle.</b> La recherche employee
    /// n'est pas celle de Vitesse : c'est <see cref="FindNearestOpponent_00474EB0"/> en mode
    /// <c>0</c>, qui pour un lanceur du groupe descend dans
    /// <see cref="FindNearestPartyMember_00474CD0"/> — et celle-la <b>ignore tout membre dont
    /// <c>+0x74</c> est non nul</b>. En posant <c>allie+0x74 = soi</c> sur chaque allie servi, le
    /// sort le retire de la recherche suivante. Quand il ne reste plus personne, la recherche rend
    /// <c>null</c>, le corps retombe sur le lanceur, et la condition <c>trouve != lanceur</c>
    /// ferme la boucle.
    /// </para>
    /// <para>
    /// <b>Les marqueurs ne sont jamais effaces ici, et ce n'est pas un oubli</b> : le pas par image
    /// d'un membre du groupe oublie sa cible <c>+0x74</c> des que l'emplacement visee cesse de
    /// porter <c>0x80000000</c>. Turbo met son propre mot d'etat a zero en sortant de la boucle ;
    /// tous les allies marques se liberent donc d'eux-memes a leur prochain pas. Le marquage ne
    /// dure que le temps de l'image ou la boucle tourne — exactement ce qu'il lui faut.
    /// </para>
    /// <para>
    /// <b>Le <c>do…while</c> garantit un tour</b> : un lanceur seul se hate lui-meme, comme Vitesse
    /// le fait par le repli de sa propre recherche.
    /// </para>
    /// </summary>
    private static void TickTurbo_00478D10(byte* record)
    {
        byte* e = record;
        byte* found;

        do
        {
            // Ghidra: LAB_00478d1b — l'allie le plus proche qui ne porte pas deja un marqueur.
            found = FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 0);

            // Ghidra: LAB_00478d34 — plus personne : on retombe sur le lanceur, et ce sera le
            // dernier tour.
            if (found == null) found = *(byte**)(e + 0x74);

            *(byte**)(e + 0x78) = found;
            *(byte**)(e + 0x70) = found;

            // LE MARQUEUR QUI FAIT AVANCER LA BOUCLE : cet allie sera invisible a la recherche
            // suivante. Son propre pas par image l'effacera, le sort etant mort d'ici la.
            *(byte**)(found + 0x74) = e;

            *(uint*)(e + 0x38) = 0x00f0f0f0;
            e[0x04] = 0x26;

            int height = StatusBubbleHeight_00538BC0((uint*)found);

            // (hauteur << 12) / 150 — la meme division que Vitesse, `sar 4` compris.
            uint low = (uint)((height << 0xc) / 150);
            uint high = (uint)(((int)low >> 1) + (int)low);      // une fois et demie

            // LE PREMIER ANNEAU : haut et etroit, et il ne posera RIEN.
            *(short*)(e + 0x2c) = (short)low;
            *(ushort*)(e + 0x16) = 0;
            *(uint*)(e + 0x28) = high * 0x10000 | low;

            SpawnCastEntity_0047AAE0(e, 0x81, e[0x0b], -1);

            // LE SECOND : large et plat, et c'est LUI qui posera l'effet 19.
            *(uint*)(e + 0x28) = (high & 0xfffffffe) << 0xf | high;
            *(short*)(e + 0x2c) = (short)high;
            *(ushort*)(e + 0x16) = 1;

            SpawnCastEntity_0047AAE0(e, 0x81, e[0x0b], -1);

            Aud.PlayPositionalSound(0x0d, 0x2d4, 0x1200, (int*)(e + 0x1c));
        }
        while (found != *(byte**)(e + 0x74));

        *(uint*)e = 0;
    }

    // =====================================================================
    // SUPERMAN — ecole 2, indice 14, sous-type 0xae
    //
    //   0x00478DF0   LE SORT. Sous-type 0xae de 0x00551c30. DECOMPILE dans Blaze.c.
    //   0x00479030   L'ANNEAU, sous-type 0x82 de 0x00551f30. DECOMPILE. C'est celui de Vitesse a
    //                DEUX differences pres — l'effet, et un script de sons en moins.
    //   0x00478FC0   LA POUSSIERE, sous-type 0x7d de 0x00551f30. 97 octets, ABSENTE de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00478FC0.asm.
    //
    // C'EST VITESSE, EN PLUS GRAND ET EN PLUS LONG. La premiere image est celle de Vitesse mot
    // pour mot — deux anneaux de tailles reglees sur la stature, distingues par +0x16, dont seul
    // le second posera l'effet — a ceci pres que la division est par 100 et non par 150 : les
    // anneaux de Superman sont donc une fois et demie plus larges. Puis le sort NE MEURT PAS : il
    // vit vingt-quatre images, et a chacune il seme UN GRAIN DE POUSSIERE quelque part dans un
    // cube d'une stature de cote autour des pieds du porteur.
    //
    // ET LE SORT RELIT LA STATURE. Il appelle StatusBubbleHeight une SECONDE fois sur la meme
    // cible, pour en tirer une autre taille — celle-la divisee par 75 — qu'il laisse dans +0x2c et
    // +0x28 a l'intention des grains. Les deux appels rendent la meme chose ; le second n'est
    // qu'une relecture, transcrite telle quelle.
    //
    // LA POUSSIERE NE COMPTE PAS SES IMAGES : comme la flamme des Materia et la marque d'Ecrase,
    // c'est sa teinte qui la tue — 0xf0f0f0 moins 0x101010 par image, et un zero exact. Quinze
    // images. Elle retrecit d'un quart par image et tourne de 0x80, sans jamais bouger de son
    // decalage : elle se recolle a chaque image sur le porteur plus l'ecart que le sort lui a tire.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00478df0 @ 0x00478df0 — <b>Superman</b> : le sous-type <c>0xae</c>, creneau 174
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>quinzieme fiche de l'ecole 2</b>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Sa premiere image est celle de <see cref="TickHaste_00477360"/></b>, aux deux memes
    /// anneaux et au meme couple de tailles <c>(base, 1,5x)</c> puis <c>(1,5x, 0,75x)</c>, le meme
    /// <c>+0x16</c> decidant lequel posera l'effet. <b>Deux ecarts</b> : la division est par
    /// <b>100</b> et non par 150 — les anneaux sont une fois et demie plus larges —, et le
    /// sous-type engendre est <c>0x82</c> (<see cref="TickSupermanRing_00479030"/>) au lieu de
    /// <c>0x81</c>.
    /// </para>
    /// <para>
    /// <b>Puis il ne meurt pas.</b> La ou Vitesse s'efface a la fin de son unique image, celui-ci
    /// vit <b>vingt-quatre images</b> et seme a chacune un grain de poussiere <c>0x7d</c>
    /// (<see cref="TickSupermanMote_00478FC0"/>) a une position tiree dans un cube d'<b>une
    /// stature de cote</b> autour des pieds du porteur — X et Z centres, Y toujours positif, donc
    /// au-dessus du sol et jamais dessous. A la vingt-quatrieme il met son mot d'etat a zero.
    /// </para>
    /// <para>
    /// <b>Il relit la stature.</b> <see cref="StatusBubbleHeight_00538BC0"/> est appelee une
    /// <b>seconde</b> fois sur la meme cible, pour en tirer la taille des grains — celle-la divisee
    /// par <b>75</b> (<c>0x1B4E81B5</c> puis <c>sar 3</c>). Les deux appels rendent la meme chose ;
    /// le second n'est qu'une relecture, transcrite telle quelle.
    /// </para>
    /// <para>
    /// <b>Et il siffle une image sur deux</b> a partir de la huitieme : le son <c>0x1f</c>, sur un
    /// test d'age impair.
    /// </para>
    /// </summary>
    private static void TickSuperman_00478DF0(byte* record)
    {
        byte* e = record;
        byte* carrier;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x36] |= 6;
            *(short*)(e + 0x2e) = -64;

            // Faute de voisin dans son camp, elle rend le lanceur : le sort se pose toujours.
            carrier = LegacyPlus.Coutures.CibleDeFee(e, FindTargetAround_00474BA0(e, 0));   // Couture legacy ++ (LP-015)

            *(byte**)(e + 0x78) = carrier;
            *(byte**)(e + 0x70) = carrier;

            *(uint*)(e + 0x38) = 0x00f0f0f0;
            e[0x04] = 0x26;

            int height = StatusBubbleHeight_00538BC0((uint*)carrier);

            // (hauteur << 12) / 100 — 0x51EB851F puis `sar 5`. Vitesse divise par 150.
            uint low = (uint)((height << 0xc) / 100);
            uint high = (uint)(((int)low >> 1) + (int)low);      // une fois et demie

            // LE PREMIER ANNEAU : haut et etroit, et il ne posera RIEN.
            *(short*)(e + 0x2c) = (short)low;
            *(ushort*)(e + 0x16) = 0;
            *(uint*)(e + 0x28) = high * 0x10000 | low;

            SpawnCastEntity_0047AAE0(e, 0x82, e[0x0b], -1);

            // LE SECOND : large et plat, et c'est LUI qui posera l'effet 26.
            *(uint*)(e + 0x28) = (high & 0xfffffffe) << 0xf | high;
            *(short*)(e + 0x2c) = (short)high;
            *(ushort*)(e + 0x16) = 1;

            SpawnCastEntity_0047AAE0(e, 0x82, e[0x0b], -1);

            // LA STATURE EST RELUE — un second appel sur la meme cible, qui rend la meme chose.
            int again = StatusBubbleHeight_00538BC0((uint*)carrier);

            // La demi-stature : c'est le rayon dans lequel les grains seront semes.
            *(short*)(e + 0x12) = (short)(again >> 1);

            // Et leur taille, celle-la divisee par 75 — 0x1B4E81B5 puis `sar 3`.
            int mote = (again << 0xc) / 75;

            *(short*)(e + 0x2c) = (short)mote;
            *(int*)(e + 0x28) = mote * 4;
            *(ushort*)(e + 0x14) = 3;

            Aud.PlayPositionalSound(0x11, 0x22f1, 0x1200, (int*)(e + 0x1c));
            Aud.PlayPositionalSound(0x20, 0x400, 0x1200, (int*)(e + 0x1c));
        }
        else
        {
            // Ghidra: LAB_00478f12
            carrier = *(byte**)(e + 0x78);
        }

        // Ghidra: LAB_00478f15 — UN GRAIN PAR IMAGE, pendant vingt-quatre images.
        if (*(short*)(e + 0x10) < 0x18)
        {
            *(short*)(e + 0x32) = (short)CrtRand();       // Ghidra: FUN_00547140
            *(short*)(e + 0x34) = (short)CrtRand();

            int half = *(short*)(e + 0x12);
            int span = half + half;

            // Le cube fait une stature de cote. X et Z sont centres sur le porteur ; Y ne l'est
            // pas — le reste de la division n'est pas decale, donc le grain naît toujours
            // AU-DESSUS du sol.
            *(int*)(e + 0x44) = (CrtRand() % span - half) << 0xc;
            *(int*)(e + 0x48) = (CrtRand() % span) << 0xc;
            *(int*)(e + 0x4c) = (CrtRand() % span - half) << 0xc;

            *(int*)(e + 0x20) = *(int*)(carrier + 0x60);
            e[0x04] = 6;

            SpawnCastEntity_0047AAE0(e, 0x7d, e[0x0b], -1);
        }
        else
        {
            *(uint*)e = 0;
        }

        // Ghidra: LAB_00478f83 — un sifflement une image sur deux, a partir de la huitieme.
        if (*(short*)(e + 0x10) > 7 && (*(ushort*)(e + 0x10) & 1) != 0)
        {
            Aud.PlayPositionalSound(0x1f, 0x1000, 0x1200, (int*)(e + 0x1c));
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00479030 @ 0x00479030 — <b>l'anneau de Superman</b> : le sous-type <c>0x82</c>
    /// de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>Decompile dans <c>Blaze.c</c></b>. Il
    /// en naît <b>deux par lancer</b>, comme chez Vitesse, et seul le mot <c>+0x16</c> les
    /// distingue.
    ///
    /// <para>
    /// <b>C'est <see cref="TickHasteRing_00477420"/> a deux differences pres</b>, et le diff des
    /// deux desassemblages ne montre rien d'autre : l'effet pose est
    /// <see cref="ApplyStatusEffect_0053C160"/> — le 26, duree en <c>+0xb4</c>, bit
    /// <c>0x200000</c> — au lieu du 19, et <b>il n'y a pas de script de sons differes</b> : la
    /// gamme montante que l'anneau de Vitesse met en file en se fermant n'existe pas ici. Il se
    /// ferme en silence.
    /// </para>
    /// <para>
    /// Tout le reste est identique : il se recolle aux <b>pieds</b> de sa cible
    /// (<c>cible+0x60</c>), tourne de <c>age * 8</c> par image donc de plus en plus vite, sa teinte
    /// est un sinus de son age pose sur les trois canaux, et il se ferme a l'age <c>0x20</c>.
    /// </para>
    /// </summary>
    private static void TickSupermanRing_00479030(byte* record)
    {
        byte* e = record;
        byte* target = *(byte**)(e + 0x70);

        short age = *(short*)(e + 0x10);

        // Il tourne de plus en plus vite : le pas est l'age lui-meme, multiplie par huit.
        *(short*)(e + 0x32) += (short)(age * 8);

        // IL SE COLLE AUX PIEDS DE SA CIBLE : +0x60 est la cote du sol sous elle.
        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x60);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        // Ghidra: Ordinal_451 — la table de sinus de winsys3d.
        int grey = (int)Winsys3d.GetSinTableEntry(age << 6) * 0xff >> 0xc;
        *(uint*)(e + 0x38) = (uint)((grey << 8 | grey) << 8 | grey);

        if (age >= 0x20)
        {
            if (*(short*)(e + 0x16) != 0)
            {
                // LE SECOND ANNEAU SEUL. La paire famille/indice, pretee au lanceur en un mot.
                *(ushort*)(*(byte**)(e + 0x74) + 6) = *(ushort*)(e + 6);

                LegacyPlus.Coutures.EffetDeFee(
                    *(uint**)(e + 0x74), *(uint**)(e + 0x78), &ApplyStatusEffect_0053C160);   // Couture legacy ++ (LP-015)
            }

            // ET IL SE FERME EN SILENCE : pas de script de sons differes, contrairement a Vitesse.
            *(uint*)e = 0;
        }

        *(short*)(e + 0x10) = (short)(age + 1);
    }

    /// <summary>
    /// Ghidra: FUN_00478fc0 @ 0x00478fc0 — <b>le grain de poussiere de Superman</b> : le sous-type
    /// <c>0x7d</c> de <see cref="s_StaticEntityHandlers_00551f30"/>. 97 octets, <b>absent de
    /// <c>Blaze.c</c></b> ; le releve est garde sous <c>Legacy/Raw/Blaze/FUN_00478FC0.asm</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Il ne bouge pas de son ecart.</b> Le sort lui a tire un decalage a trois axes
    /// (<c>+0x44</c>, <c>+0x48</c>, <c>+0x4c</c>) et il se recolle a chaque image sur la position
    /// du porteur plus cet ecart-la — jamais recalcule. Il tourne de <c>0x80</c> par image et
    /// perd un quart de sa taille, en largeur (<c>+0x28</c>) et en profondeur (<c>+0x2c</c>)
    /// seulement : <b><c>+0x2a</c> n'est pas touche</b>, ni ici ni par le sort, qui l'a laisse a
    /// zero en ecrivant <c>+0x28</c> en mot long.
    /// </para>
    /// <para>
    /// <b>Il ne compte pas ses images : c'est sa teinte qui le tue</b> — la meme mecanique que la
    /// flamme des Materia (<see cref="TickFireMateriaFlame_00477600"/>) et que la marque d'Ecrase
    /// (<see cref="TickCrushMark_00477DA0"/>) : <c>0xf0f0f0</c> moins <c>0x101010</c> par image, et
    /// le corps s'efface sur un <b>zero exact</b>. Quinze images.
    /// </para>
    /// </remarks>
    private static void TickSupermanMote_00478FC0(byte* record)
    {
        byte* e = record;
        byte* carrier = *(byte**)(e + 0x70);

        // Il tourne sur lui-meme.
        *(short*)(e + 0x32) += 0x80;

        // Et il perd un quart de sa taille par image — en largeur et en profondeur seulement.
        short width = *(short*)(e + 0x28);
        width = (short)(width - (short)(width >> 2));
        *(short*)(e + 0x28) = width;

        short depth = *(short*)(e + 0x2c);
        depth = (short)(depth - (short)(depth >> 2));
        *(short*)(e + 0x2c) = depth;

        // IL SE RECOLLE SUR SON ECART, jamais recalcule.
        *(int*)(e + 0x1c) = *(int*)(e + 0x44) + *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(e + 0x48) + *(int*)(carrier + 0x20);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c) + *(int*)(carrier + 0x24);

        // LA TEINTE EST SON COMPTE A REBOURS : seul un zero exact l'eteint.
        uint tint = *(uint*)(e + 0x38) - 0x00101010;
        *(uint*)(e + 0x38) = tint;

        if (tint == 0) *(uint*)e = 0;
    }

    // =====================================================================
    // BLINDE — ecole 2, indice 15, sous-type 0xaf
    //
    //   0x004790C0   LE SORT. Sous-type 0xaf de 0x00551c30. DECOMPILE dans Blaze.c.
    //   0x00479250   LA COQUE, sous-type 0x80 de 0x00551f30. DECOMPILEE aussi.
    //
    // IL SE LANCE SUR LUI-MEME, ET C'EST LE SEUL. Toutes les autres fiches de l'ecole cherchent
    // une cible — un adversaire pour Dodo, Gros sabots et Ralenti, un allie pour Vitesse, les
    // Materia, Coussin d'air, Champ magique, Escargot et Superman. Blinde n'en cherche aucune :
    // il appelle l'effet 25 avec le LANCEUR EN ARGUMENT DES DEUX COTES, assaillant et victime.
    //
    // ET SI L'EFFET NE PASSE PAS, LE SORT N'A PAS LIEU : le corps rend la main sur-le-champ, sans
    // rien engendrer et sans meme une image de decor. C'est le seul de l'ecole qui puisse echouer
    // completement.
    //
    // SA DUREE LUI EST RENDUE PAR L'EFFET LUI-MEME. L'effet 25 range sa duree dans le +0xb2 du
    // bloc de statistiques ; le sort relit ce meme +0xb2 juste apres et s'en fait un compte de
    // charges, qu'il decremente toutes les vingt images jusqu'a zero. Les deux bouts se
    // rejoignent : ce qui est ecrit par l'effet est ce qui fait vivre le sort.
    //
    // QUATRE COQUES L'ENTOURENT, deux a l'image 0 et deux a l'image 4 — la premiere de chaque
    // paire a hauteur de tete, la seconde a vingt unites du sol, et le cap tourne d'un demi-tour
    // entre chacune. Elles apparaissent en FONDU (leur teinte monte de 0x101010 par image jusqu'a
    // 0xf0f0f0) et disparaissent de meme quand le sort finit : c'est le seul corps du depot dont
    // la teinte serve de compte a rebours DANS LES DEUX SENS.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004790c0 @ 0x004790c0 — <b>Blinde</b> : le sous-type <c>0xaf</c>, creneau 175 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>seizieme fiche de l'ecole 2</b>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le seul sort de l'ecole qui se lance sur lui-meme</b> : il passe le lanceur des deux
    /// cotes a <see cref="ApplyStatusEffect_0053C0F0"/> — l'effet 25, « le seul de la famille a
    /// poser un bit sans faire naitre de marque », celui que les guerisons interrogent pour refuser
    /// leur soin. <b>Et si l'effet ne passe pas, le sort n'a pas lieu</b> : le corps met son mot
    /// d'etat a zero et rend la main sans rien engendrer.
    /// </para>
    /// <para>
    /// <b>Sa duree lui est rendue par l'effet.</b> L'effet 25 range <c>attaque / 8 + 0x19</c> dans
    /// le <c>+0xb2</c> du bloc de statistiques ; le sort relit ce meme <c>+0xb2</c> juste apres et
    /// s'en fait un <b>compte de charges</b> en <c>+0x14</c>, decremente toutes les vingt images —
    /// sur l'horloge du monde, pas sur son propre age — jusqu'a zero. La resolution du bloc est
    /// celle de <see cref="StatusBubbleHeight_00538BC0"/>, par le bit <c>0x800000</c>.
    /// </para>
    /// <para>
    /// <b>Quatre coques</b> naissent, deux a l'image 0 et deux a l'image 4 : la premiere de chaque
    /// paire a hauteur de tete moins vingt unites, la seconde a vingt unites du sol, avec des
    /// rayons de <c>0x1000</c> et <c>0xc00</c> ; le cap <c>+0x34</c> avance d'un demi-tour apres
    /// chacune. Passe la seizieme image, seuls le compte de charges et
    /// <see cref="CanSustainChannel_00475110"/> peuvent l'arreter.
    /// </para>
    /// <para>
    /// Et a chaque image, quoi qu'il arrive, il se recolle sur le lanceur.
    /// </para>
    /// </summary>
    private static void TickArmour_004790C0(byte* record)
    {
        byte* e = record;
        byte* caster = *(byte**)(e + 0x74);

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            // LA PAIRE FAMILLE/INDICE, pretee au lanceur EN UN SEUL MOT — sur lui-meme, ici.
            *(ushort*)(caster + 6) = *(ushort*)(e + 6);

            // ET IL EST SA PROPRE VICTIME. Si l'effet ne passe pas, le sort n'a pas lieu.
            if (ApplyStatusEffect_0053C0F0((uint*)caster, (uint*)caster, 0) != 0)
            {
                *(uint*)e = 0;
                return;
            }

            SpawnStatusEffectEntity_0047AC40(
                caster, (int*)(caster + 0x1c), SpriteAtlas(0x005648a0), 0x29, 0, 0);

            // Le bloc de statistiques du lanceur, resolu par le bit 0x800000 comme partout.
            byte* stats = (*(uint*)caster & 0x800000) != 0
                ? GetMainMemPtr() + 0xf0100 + caster[8] * 0x2000
                : s_EntityStatBlocks_005ea560 + caster[8] * 0x120;

            // LE COMPTE DE CHARGES, relu la ou l'effet vient d'ecrire sa duree.
            *(ushort*)(e + 0x14) = *(ushort*)(stats + 0xb2);
        }

        // DEUX COQUES A L'IMAGE 0, DEUX A L'IMAGE 4.
        if ((*(ushort*)(e + 0x10) & 3) == 0 && *(short*)(e + 0x10) < 8)
        {
            int height = StatusBubbleHeight_00538BC0((uint*)caster);

            *(short*)(e + 0x16) = (short)height;

            // La premiere de la paire : a hauteur de tete moins vingt unites.
            *(int*)(e + 0x48) = (height - 0x14) * 0x1000;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x4c) = 0;

            // Elles naissent NOIRES : la teinte montera d'elle-meme.
            *(uint*)(e + 0x38) = 0;

            e[0x04] = 7;
            *(short*)(e + 0x2e) = -20;
            *(ushort*)(e + 0x12) = 0x1000;

            SpawnCastEntity_0047AAE0(e, 0x80, e[0x0b], -1);

            *(short*)(e + 0x34) += 0x800;

            // La seconde : plus etroite, et a vingt unites du sol.
            *(ushort*)(e + 0x12) = 0xc00;
            *(int*)(e + 0x48) = 0x14000;

            SpawnCastEntity_0047AAE0(e, 0x80, e[0x0b], -1);

            *(short*)(e + 0x34) += 0x800;
        }

        if (*(short*)(e + 0x10) > 0xf)
        {
            // LE COMPTE DE CHARGES, cadence sur l'horloge du monde et non sur l'age du sort.
            if (*(uint*)Mem.Entity(0x005f2cf0) % 20 == 0)
            {
                *(short*)(e + 0x14) -= 1;
                if (*(short*)(e + 0x14) == 0) *(uint*)e |= 0x2000000;
            }

            // Et le lanceur doit tenir debout.
            if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_00479229 — il se recolle sur le lanceur, sur tous les chemins.
        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
        *(int*)(e + 0x20) = *(int*)(caster + 0x20);

        int z = *(int*)(caster + 0x24);
        *(short*)(e + 0x10) += 1;
        *(int*)(e + 0x24) = z;
    }

    /// <summary>
    /// Ghidra: FUN_00479250 @ 0x00479250 — <b>la coque de Blinde</b> : le sous-type <c>0x80</c> de
    /// <see cref="s_StaticEntityHandlers_00551f30"/>. <b>Decompilee dans <c>Blaze.c</c></b>. Il en
    /// naît <b>quatre par lancer</b>.
    ///
    /// <para>
    /// <b>Sa teinte est un compte a rebours DANS LES DEUX SENS</b>, et c'est le seul corps du depot
    /// a le faire. Tant que le pere n'a pas leve son <c>0x2000000</c>, elle <b>monte</b> de
    /// <c>0x101010</c> par image jusqu'a la butee <c>0xf0f0f0</c> — la coque apparaît en fondu ;
    /// des que le pere l'a leve, elle <b>redescend</b> du meme pas et le corps s'efface sur un zero
    /// exact. La flamme des Materia, la marque d'Ecrase et le grain de Superman n'en font que la
    /// moitie.
    /// </para>
    /// <para>
    /// <b>Elle herite ce bit du pere a chaque image</b>, pas une seule fois : le mot d'etat du
    /// pere est relu tant que la coque ne l'a pas. C'est ce qui lui permet de survivre au sort et
    /// de s'eteindre proprement apres lui.
    /// </para>
    /// <para>
    /// <b>Elle respire</b> : son rayon vaut <c>+0x12</c> plus le huitieme d'un cosinus de son age,
    /// pose sur les trois echelles a la fois. Elle se recolle sur le lanceur plus son ecart fixe
    /// (<c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>), et cogne le son <c>0x17</c> une image sur seize.
    /// </para>
    /// </summary>
    private static void TickArmourShell_00479250(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) == 0)
        {
            // Le bit du pere, relu a CHAQUE image tant qu'elle ne l'a pas.
            *(uint*)e |= *(uint*)(*(byte**)(e + 0x6c)) & 0x2000000;

            // ELLE APPARAIT EN FONDU.
            uint tint = *(uint*)(e + 0x38);
            if (tint < 0x00f0f0f0) *(uint*)(e + 0x38) = tint + 0x00101010;
        }
        else
        {
            // ET ELLE DISPARAIT DE MEME, sur un zero exact.
            uint tint = *(uint*)(e + 0x38) - 0x00101010;
            *(uint*)(e + 0x38) = tint;

            if (tint == 0) *(uint*)e = 0;
        }

        // Ghidra: LAB_00479295 — ELLE RESPIRE : le huitieme d'un cosinus de son age.
        int radius = (Winsys3d.GetCosTableEntry(*(short*)(e + 0x10) << 8) >> 3)
                     + *(short*)(e + 0x12);

        *(short*)(e + 0x2c) = (short)radius;
        *(uint*)(e + 0x28) = (uint)(radius << 0x10 | radius);

        // Elle se recolle sur le lanceur plus son ecart fixe.
        byte* caster = *(byte**)(e + 0x74);

        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + *(int*)(e + 0x44);
        *(int*)(e + 0x20) = *(int*)(caster + 0x20) + *(int*)(e + 0x48);
        *(int*)(e + 0x24) = *(int*)(caster + 0x24) + *(int*)(e + 0x4c);

        // Un choc une image sur seize.
        if ((*(uint*)(e + 0x10) & 0xf) == 0)
        {
            Aud.PlayPositionalSound(0x17, 200, 0x1200, (int*)(e + 0x1c));
        }

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551978</c> — <b>le script de sons differes des quatre elementaires</b>,
    /// cinq pas de huit octets : un coup <c>0x11</c> puis quatre <c>0x18</c> de plus en plus aigus,
    /// espaces de quatre images.
    ///
    /// <para>
    /// <b>Il n'a PAS de terminateur</b>, et c'est un defaut d'origine (LB-027) : aucun de ses cinq
    /// enregistrements ne porte un octet de delai nul. La plage s'arrete la parce que
    /// <see cref="s_Descriptors_005519A0"/> commence a <c>0x005519a0</c> — pas parce qu'une borne
    /// la designe. <c>Audio.TickDelayedSounds</c> continue donc dans la table de descripteurs, dont
    /// le premier enregistrement se lit comme « seize sons d'un coup, puis 192 images d'attente ».
    /// </para>
    /// <para>
    /// <b>Ce port ne reproduit pas cette marche</b> : les deux tables sont ici deux allocations
    /// distinctes, et le script s'arrete a son cinquieme pas. C'est une divergence declaree, pas un
    /// oubli — reproduire la marche demanderait de recopier a la suite 168 octets de tables sans
    /// rapport.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00551978 =
    [
        0x11, 0x01, 0x01, 0x00, 0xbf, 0x01, 0x00, 0x12,   // 0x00551978
        0x18, 0x04, 0x01, 0x00, 0xa6, 0x0c, 0x00, 0x12,   // 0x00551980
        0x18, 0x04, 0x01, 0x00, 0x50, 0x0d, 0x00, 0x12,   // 0x00551988
        0x18, 0x04, 0x01, 0x00, 0xfa, 0x0d, 0x00, 0x12,   // 0x00551990
        0x18, 0x04, 0x01, 0x00, 0xa4, 0x0e, 0x00, 0x12,   // 0x00551998 — et PAS de delai nul
    ];

    private static byte* s_DelayedSoundScript00551978Storage;

    /// <summary>Les cinq pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript00551978()
        => NativeCopy(s_DelayedSoundScript_00551978, ref s_DelayedSoundScript00551978Storage);

    // =====================================================================
    // LIQUIDE — ecole 2, indice 16, sous-type 0xb0
    //
    //   0x00479F70   LE SORT. Sous-type 0xb0 de 0x00551c30. 392 octets, ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00479F70.asm.
    //   0x00479490   L'ELEMENTAIRE, sous-type 0x7f de 0x00551f30. 1 766 octets, ABSENT de
    //                Blaze.c ; releve sous Legacy/Raw/Blaze/FUN_00479490.asm.
    //   0x00479B90   LA COLONNE, sous-type 0x8b de 0x00551f30. 34 octets, ABSENT de Blaze.c.
    //
    // LES QUATRE DERNIERES FICHES DE L'ECOLE PARTAGENT UN SEUL CORPS D'ENFANT, ET C'EST UNE
    // FRATRIE D'UNE AUTRE ESPECE QUE LES MATERIA. Celles-la etaient quatre copies d'un meme corps ;
    // ici il n'y a qu'UN corps, et c'est lui qui se branche : 0x00479490 lit le mot +0x06 de son
    // enregistrement — l'INDICE du sort dans son ecole, 0x10 a 0x13 — et saute dans une table de
    // quatre entrees a 0x00479b78 pour choisir son element. Liquide prend l'entree 0, Flamme la 1,
    // Zephe la 2, Terre la 3.
    //
    // LE SORT FAIT NAITRE TROIS ELEMENTAIRES, a 120 degres l'un de l'autre sur un cercle d'une
    // demi-stature autour du lanceur, trente unites au-dessus du sol. Puis il ne fait plus que
    // payer : DEUX PM TOUTES LES CINQ IMAGES tant que le bouton reste tenu — la cadence la plus
    // chere de toute l'ecole, quatre fois celle de Champ magique.
    //
    // ET L'ELEMENTAIRE EST LE PREMIER VRAI FAMILIER DU DEPOT. Il grossit sur place, previent son
    // pere quand il est pret (il pose le bit 0x2 sur l'enregistrement du pere, qui n'a aucun autre
    // moyen de le savoir), puis CHASSE : il cherche un adversaire, le retient par +0x74, vole vers
    // lui a coups de poussee de huit unites amortie d'un huitieme par image, le frappe au contact
    // par ResolveSpellHit avec la famille et l'indice de son propre sort, et le lache au bout de
    // quarante images pour en chercher un autre. Sans proie, il tourne autour de son lanceur.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00479f70 @ 0x00479f70 — <b>Liquide</b> : le sous-type <c>0xb0</c>, creneau 176
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>dix-septieme fiche de l'ecole
    /// 2</b>. 392 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00479F70.asm</c>.
    ///
    /// <para>
    /// <b>Trois elementaires naissent a la premiere image</b>, a 120 degres l'un de l'autre
    /// (<c>0x555</c> de pas, soit un tiers de tour) sur un cercle d'une <b>demi-stature</b> autour
    /// du lanceur, place trente unites au-dessus du sol. Chacun recoit une phase differente en
    /// <c>+0x12</c>, du meme pas de <c>0x555</c> : c'est ce qui les fera tourner en decalage quand
    /// ils n'auront pas de proie.
    /// </para>
    /// <para>
    /// <b>Ensuite le sort ne fait plus que payer.</b> Il n'engendre plus rien et ne bouge plus :
    /// il preleve <b>deux PM toutes les cinq images</b> tant que le bouton reste tenu — la cadence
    /// la plus chere de l'ecole, quatre fois celle de Champ magique — et s'arrete des que les PM
    /// manquent, que le bouton est relache, ou que
    /// <see cref="CanSustainChannel_00475110"/> refuse.
    /// </para>
    /// <para>
    /// <b>Son age reste a 1.</b> La premiere image le pose et <b>rien ne l'incremente ensuite</b> :
    /// le corps n'a pas de compteur. Le repli des cent-vingt images, prevu pour un lanceur hors
    /// groupe, est donc inatteignable — un tel lanceur n'a de toute facon ni manette ni PM.
    /// Transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Et il attend son enfant.</b> Le test <c>+0x2</c> qui ouvre toutes les images suivantes
    /// n'est pose par personne d'autre que l'elementaire, quand il a fini de grossir
    /// (<see cref="TickElementalFamiliar_00479490"/>, <c>0x004797ff</c>). Tant qu'aucun n'est pret,
    /// le sort ne coute rien.
    /// </para>
    /// </summary>
    private static void TickLiquid_00479F70(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00479f8e
        short age = *(short*)(e + 0x10);

        if (age == 0)
        {
            int savedX = *(int*)(e + 0x1c);
            int savedZ = *(int*)(e + 0x24);
            int angle = *(short*)(e + 0x32);

            *(ushort*)(e + 0x10) = 1;

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));

            *(ushort*)(e + 0x12) = 0;
            e[0x04] = 0x39;

            *(short*)(e + 0x32) += 0x400;

            // TRENTE UNITES AU-DESSUS DU SOL, mesurees depuis +0x60.
            *(int*)(e + 0x20) = ((height + 0x1e) << 0xc) + *(int*)(e + 0x60);

            int radius = height >> 1;

            // TROIS ELEMENTAIRES, a un tiers de tour l'un de l'autre.
            for (int remaining = 3; remaining != 0; remaining--)
            {
                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * radius + savedX;
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * radius + savedZ;

                SpawnCastEntity_0047AAE0(e, 0x7f, e[0x0b], -1);

                // La phase de chacun, du meme pas : ils tourneront en decalage.
                *(short*)(e + 0x12) += 0x555;
                angle += 0x555;
            }

            Aud.QueueDelayedSoundScript(DelayedSoundScript00551978(), (int*)(e + 0x1c));

            // Le lanceur est immobilise ; c'est l'elementaire qui le relachera.
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;
            return;
        }

        // Ghidra: LAB_0047a04c — RIEN NE SE PASSE TANT QU'AUCUN ELEMENTAIRE N'EST PRET.
        if ((*(uint*)e & 2) == 0) return;

        if ((*(uint*)e & 0x800000) != 0)
        {
            byte* caster = *(byte**)(e + 0x74);
            uint index = ((uint*)caster)[2] & 0xff;
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((Input.CombatMask(*(ushort*)(perChar + 0x1fc4)) & pad) == 0)
            {
                *(uint*)e |= 0x2000000;                   // le bouton est relache
            }

            // Ghidra: LAB_0047a09c — DEUX PM TOUTES LES CINQ IMAGES.
            if (*(uint*)Mem.Entity(0x005f2cf0) % 5 == 0)
            {
                short mana = *(short*)(perChar + 0x14e);

                if (mana >= 2)
                {
                    *(short*)(perChar + 0x14e) = (short)(mana - 2);
                }
                else
                {
                    // Ghidra: LAB_0047a0c7
                    *(ushort*)(perChar + 0x14e) = 0;
                    *(uint*)e |= 0x2000000;
                }
            }
        }
        else if (age >= 0x78)
        {
            // Ghidra: LAB_0047a0d2 — inatteignable : l'age reste a 1.
            *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_0047a0df — et le lanceur doit tenir debout.
        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
    }

    /// <summary>
    /// Ghidra: FUN_00479490 @ 0x00479490 — <b>l'elementaire</b> : le sous-type <c>0x7f</c> de
    /// <see cref="s_StaticEntityHandlers_00551f30"/>. 1 766 octets, <b>absent de <c>Blaze.c</c></b>
    /// ; le releve est garde sous <c>Legacy/Raw/Blaze/FUN_00479490.asm</c>.
    /// <b>Les quatre dernieres fiches de l'ecole 2 le partagent.</b>
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Un seul corps, quatre elements.</b> Il lit <c>+0x06</c> — l'<b>indice</b> du sort dans
    /// son ecole, <c>0x10</c> a <c>0x13</c> — et saute dans une table de quatre entrees relevee a
    /// <c>0x00479b78</c> : Liquide (0), Flamme (1), Zephe (2), Terre (3). C'est une fratrie d'une
    /// autre espece que les Materia, qui etaient quatre copies d'un meme corps ; ici il n'y a qu'un
    /// corps, et c'est lui qui se branche.
    /// </para>
    /// <para>
    /// <b>Il a trois vies, dans cet ordre.</b> <b>La levee</b> (bit <c>0x2</c> clair) : il grossit
    /// sur place et seme le decor de son element une image sur deux ou sur quatre selon celui-ci.
    /// Quand son echelle atteint <c>0x7f0</c>, il <b>pose le bit <c>0x2</c> sur l'enregistrement de
    /// son pere</b> — c'est le seul signal que le sort recoit, et sans lui le sort ne coute jamais
    /// rien — relache le lanceur, se donne une gravite et passe a la suite.
    /// </para>
    /// <para>
    /// <b>La chasse</b> (bit <c>0x2</c> pose) : il cherche un adversaire par
    /// <see cref="FindNearestOpponent_00474EB0"/>, le <b>retient par <c>+0x74</c></b>, vise sa
    /// hauteur de tete et s'y pousse de huit unites par image, sa vitesse etant amortie d'un
    /// huitieme. Son cap tourne d'un quart de l'ecart, mais <b>vise 0x400 a cote</b> — le binaire
    /// ajoute un quart de tour a l'erreur avant de la replier. Au contact il frappe par
    /// <see cref="ResolveSpellHit_0053A2B0"/> avec la famille (<c>+0x07</c>) et l'indice
    /// (<c>+0x06</c>) de son propre sort. Il lache sa proie au bout de <b>quarante images</b>, ou
    /// des qu'elle porte <c>0x50000000</c>. <b>Sans proie il tourne autour du lanceur</b>, a la
    /// phase que le sort lui a donnee.
    /// </para>
    /// <para>
    /// <b>L'extinction</b> (bit <c>0x2000000</c>, herite du pere a chaque image) : il perd un quart
    /// de sa taille par image et disparait sous <c>8</c>.
    /// </para>
    /// <para>
    /// <b>Et il respire en permanence</b> : a la fin de chaque image, son echelle avance d'un
    /// huitieme vers <c>(sin(age &lt;&lt; 8) &gt;&gt; 2) + 0x800</c>, dans les trois axes a la fois.
    /// </para>
    /// </remarks>
    private static void TickElementalFamiliar_00479490(byte* record)
    {
        byte* e = record;

        // La boite de detection, reconstruite sur la pile a chaque image — la MEME que Dodo.
        short* probe = stackalloc short[6] { 0, -60, 60, 100, 0, 120 };

        if ((*(uint*)e & 0x2000000) != 0)
        {
            // L'EXTINCTION : un quart de moins par image, et la disparition sous 8.
            short fading = *(short*)(e + 0x28);
            fading = (short)(fading - (short)(fading >> 2));

            *(short*)(e + 0x28) = fading;
            *(short*)(e + 0x2a) = fading;
            *(short*)(e + 0x2c) = fading;

            if (fading < 8) *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_004794f9
        if ((*(uint*)e & 2) != 0)
        {
            // ================================ LA CHASSE ================================
            *(uint*)e |= *(uint*)(*(byte**)(e + 0x6c)) & 0x2000000;

            byte* prey = *(byte**)(e + 0x78);

            if (prey != null)
            {
                // Quarante images par proie, et il la lache aussi des qu'elle porte 0x50000000.
                if ((*(uint*)prey & 0x50000000) != 0 || *(short*)(e + 0x10) >= 0x28)
                {
                    // Ghidra: LAB_00479526
                    *(byte**)(prey + 0x74) = null;
                    *(byte**)(e + 0x78) = null;
                    *(ushort*)(e + 0x10) = 0;
                    prey = null;
                }
            }
            else
            {
                // Ghidra: LAB_00479534
                prey = FindNearestOpponent_00474EB0(*(uint**)(e + 0x74), 0xffffffff, 1);
            }

            // Ghidra: LAB_00479546 — la phase avance, proie ou non.
            *(short*)(e + 0x12) += 0x80;
            short phase = *(short*)(e + 0x12);

            int dx;
            int dy;
            int aimZ;

            if (prey != null)
            {
                *(byte**)(e + 0x78) = prey;
                dx = (*(int*)(prey + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

                // IL LA RETIENT : plus aucune autre recherche ne la verra.
                *(byte**)(prey + 0x74) = e;

                int preyHeight = StatusBubbleHeight_00538BC0((uint*)prey);

                // Il vise la mi-hauteur de sa proie.
                dy = ((((preyHeight & ~1) << 0xb) + *(int*)(prey + 0x20)) - *(int*)(e + 0x20))
                     >> 0xc;

                aimZ = *(int*)(prey + 0x24);
            }
            else
            {
                // Ghidra: LAB_00479588 — sans proie, il tourne autour du lanceur, a sa phase.
                byte* caster = *(byte**)(e + 0x74);
                int casterHeight = StatusBubbleHeight_00538BC0((uint*)caster);

                dx = ((int)Winsys3d.GetSinTableEntry(phase) * casterHeight
                      + *(int*)(caster + 0x1c) - *(int*)(e + 0x1c)) >> 0xc;

                dy = ((casterHeight << 0xc) + *(int*)(caster + 0x20) - *(int*)(e + 0x20)) >> 0xc;

                aimZ = Winsys3d.GetCosTableEntry(phase) * casterHeight + *(int*)(caster + 0x24);
            }

            // Ghidra: LAB_004795e5 — la poussee vers le point vise, huit unites par image.
            int dz = (aimZ - *(int*)(e + 0x24)) >> 0xc;

            int flat = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));
            int aimYaw = Winsys3d.Ordinal_457(dx, dz);
            int aimPitch = Winsys3d.Ordinal_457(dy, flat);

            int* thrust = stackalloc int[3];
            SetVectorFromAngles_00532AD0(thrust, aimYaw, aimPitch, 8);

            int vx = *(int*)(e + 0x50) + thrust[0];
            int vy = *(int*)(e + 0x54) + thrust[1];
            int vz = *(int*)(e + 0x58) + thrust[2];

            *(int*)(e + 0x50) = vx;
            *(int*)(e + 0x54) = vy;
            *(int*)(e + 0x58) = vz;

            // LE VIRAGE, ET IL VISE UN QUART DE TOUR A COTE : le binaire ajoute 0x400 a l'ecart
            // avant de le replier sur douze bits.
            short heading = *(short*)(e + 0x32);
            int error = (aimYaw - heading + 0x400) & 0xfff;

            int step = error < 0x800 ? error >> 2 : -((0x1000 - error) >> 2);
            *(short*)(e + 0x32) = (short)(heading + step);

            // Ghidra: LAB_0047967f — l'amortissement, un huitieme par image sur les trois axes.
            vx -= vx >> 3;
            vy -= vy >> 3;
            vz -= vz >> 3;

            *(int*)(e + 0x50) = vx;
            *(int*)(e + 0x54) = vy;
            *(int*)(e + 0x58) = vz;

            *(int*)(e + 0x1c) += vx;
            *(int*)(e + 0x20) += vy;
            *(int*)(e + 0x24) += vz;

            byte* hit = ProbeMeleeTarget_00474CA0(e, probe);

            if (hit != null)
            {
                // IL FRAPPE AVEC LA FAMILLE ET L'INDICE DE SON PROPRE SORT.
                ResolveSpellHit_0053A2B0(
                    *(uint**)(e + 0x74), (uint*)hit, e[0x07], e[0x06], 0);

                *(uint*)hit |= 0x40;
                *(short*)(hit + 0x96) = 0x10;
            }

            // Ghidra: LAB_004796f9 — une trainee une image sur quatre.
            if ((e[0x10] & 3) == 0)
            {
                int savedScale = *(int*)(e + 0x28);
                int savedDepth = *(int*)(e + 0x2c);
                int savedTint = *(int*)(e + 0x38);

                int ahead = *(short*)(e + 0x32) + 0x400;

                // Quinze unites devant lui, plus une dispersion de 0x40 unites sur les trois axes.
                int lead = (int)Winsys3d.GetSinTableEntry(ahead) * 15;

                int* at = stackalloc int[3];
                at[0] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + lead * 8 + *(int*)(e + 0x1c);
                at[1] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + *(int*)(e + 0x20);

                lead = Winsys3d.GetCosTableEntry(ahead) * 15;
                at[2] = (((CrtRand() & 0x3f) - 0x20) << 0xc) + lead * 8 + *(int*)(e + 0x24);

                *(uint*)(e + 0x28) = 0x18001800;
                *(ushort*)(e + 0x2c) = 0x1f;
                *(uint*)(e + 0x38) = 0x00f0f0f0;
                *(ushort*)(e + 0x1a) = 4;

                SpawnStatusEffectEntity_0047AC40(
                    e, at, Descriptors00551AD8(), 0xbf, e[0x0b], -1);

                *(int*)(e + 0x28) = savedScale;
                *(int*)(e + 0x2c) = savedDepth;
                *(int*)(e + 0x38) = savedTint;
            }
        }
        else
        {
            // ================================ LA LEVEE ================================
            // Ghidra: LAB_004797d1
            short age = *(short*)(e + 0x10);

            if (age == 0)
            {
                *(byte**)(e + 0x78) = null;

                // Terre naît deja teintee ; les trois autres partent du noir.
                *(uint*)(e + 0x38) = e[0x06] == 0x13 ? 0x40000000u : 0u;

                *(int*)(e + 0x28) = 0;
                *(ushort*)(e + 0x2c) = 0;
            }

            // Ghidra: LAB_004797f7 — IL EST PRET, ET IL LE DIT A SON PERE.
            if (*(short*)(e + 0x28) >= 0x7f0)
            {
                // Le seul signal que le sort recoit : sans lui, il ne coute jamais rien.
                *(uint*)(*(byte**)(e + 0x6c)) |= 2;

                // Et le lanceur est relache.
                *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;

                *(uint*)e |= 2;

                *(int*)(e + 0x50) = 0;
                *(int*)(e + 0x54) = 0;
                *(int*)(e + 0x58) = 0;
                *(ushort*)(e + 0x9a) = 0x1000;
            }
            else if (age < 0x14)
            {
                // Ghidra: LAB_00479830 — LE DECOR DE L'ELEMENT, pendant vingt images.
                int savedX = *(int*)(e + 0x1c);
                int savedY = *(int*)(e + 0x20);
                int savedZ = *(int*)(e + 0x24);

                // Ghidra: `jmp dword [eax*4+479B78h]` — la table des quatre elements, relevee.
                int element = e[0x06] - 0x10;

                switch (element)
                {
                    case 0 when (age & 3) == 0:
                    {
                        // LIQUIDE : une colonne d'eau et une eclaboussure qui monte.
                        int savedScale = *(int*)(e + 0x28);
                        int savedDepth = *(int*)(e + 0x2c);
                        int savedTint = *(int*)(e + 0x38);
                        byte savedKind = e[0x04];

                        *(short*)(e + 0x32) += -0x400;

                        *(uint*)(e + 0x28) = 0x40001000;
                        *(ushort*)(e + 0x2c) = 0x1000;
                        *(uint*)(e + 0x38) = 0x01000000;

                        *(int*)(e + 0x1c) += ((CrtRand() & 0x7f) - 0x40) << 0xc;
                        *(int*)(e + 0x20) = *(int*)(e + 0x60);          // au ras du sol
                        *(int*)(e + 0x24) += ((CrtRand() & 0x7f) - 0x40) << 0xc;

                        e[0x04] = 0x1e;
                        SpawnCastEntity_0047AAE0(e, 0x8b, 0, 1);

                        *(ushort*)(e + 0x9a) = 0x6000;

                        int spray = CrtRand();
                        *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(spray) << 4;
                        *(int*)(e + 0x54) = ((CrtRand() & 0x1f) + 0x1e) << 0xc;
                        *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(spray) << 4;

                        *(uint*)(e + 0x38) = 0x01f0f0f0;
                        *(uint*)(e + 0x28) = 0x01801800;
                        *(ushort*)(e + 0x2c) = 0x1f;
                        *(ushort*)(e + 0x1a) = 1;

                        SpawnStatusEffectEntity_0047AC40(
                            e, (int*)(e + 0x1c), Descriptors00551BB8(), 0xbf, 0, 1);

                        *(short*)(e + 0x32) += 0x400;

                        *(int*)(e + 0x28) = savedScale;
                        *(int*)(e + 0x2c) = savedDepth;
                        *(int*)(e + 0x38) = savedTint;
                        e[0x04] = savedKind;
                        break;
                    }

                    case 1 when (age & 1) != 0:
                    {
                        // FLAMME : une braise une image sur deux, dispersee sur les trois axes.
                        int savedScale = *(int*)(e + 0x28);
                        int savedTint = *(int*)(e + 0x38);

                        *(uint*)(e + 0x28) = 0x20002000;
                        *(uint*)(e + 0x38) = 0x01000000;

                        *(int*)(e + 0x1c) += ((CrtRand() & 0x1ff) - 0x100) << 0xc;
                        *(int*)(e + 0x20) += ((CrtRand() & 0x1ff) - 0x100) << 0xc;
                        *(int*)(e + 0x24) += ((CrtRand() & 0x1ff) - 0x100) << 0xc;

                        *(ushort*)(e + 0x1a) = 4;

                        SpawnStatusEffectEntity_0047AC40(
                            e, (int*)(e + 0x1c), Descriptors00551AD8(), 0x57, 0, 2);

                        *(int*)(e + 0x28) = savedScale;
                        *(int*)(e + 0x38) = savedTint;
                        break;
                    }

                    case 2 when (age & 3) == 0:
                    {
                        // ZEPHE : une bourrasque, taille et angles tires au sort.
                        int savedSize = *(short*)(e + 0x12);
                        int savedScale = *(int*)(e + 0x28);
                        int savedTint = *(int*)(e + 0x38);
                        short savedYaw = *(short*)(e + 0x32);
                        short savedPitch = *(short*)(e + 0x34);

                        *(uint*)(e + 0x28) = 0x20002000;
                        *(short*)(e + 0x12) = (short)((CrtRand() & 0x7f) + 0xc8);
                        *(uint*)(e + 0x38) = 0;
                        *(ushort*)(e + 0x1a) = 7;

                        *(short*)(e + 0x32) = (short)CrtRand();
                        *(short*)(e + 0x34) = (short)((CrtRand() & 0x1ff) - 0x100);

                        SpawnStatusEffectEntity_0047AC40(
                            e, (int*)(e + 0x1c), Descriptors00551A08(), 0x50, 0, 4);

                        *(short*)(e + 0x32) = savedYaw;
                        *(short*)(e + 0x34) = savedPitch;
                        *(int*)(e + 0x28) = savedScale;
                        *(int*)(e + 0x38) = savedTint;
                        *(short*)(e + 0x12) = (short)savedSize;
                        break;
                    }

                    case 3 when (age & 3) == 0:
                    {
                        // TERRE : une motte au ras du sol, dispersee en X et Z seulement.
                        int savedScale = *(int*)(e + 0x28);
                        int savedTint = *(int*)(e + 0x38);

                        *(uint*)(e + 0x28) = 0x20002000;
                        *(uint*)(e + 0x38) = 0x01000000;

                        *(int*)(e + 0x1c) += ((CrtRand() & 0xff) - 0x80) << 0xc;
                        *(int*)(e + 0x20) = *(int*)(e + 0x60);          // au ras du sol
                        *(int*)(e + 0x24) += ((CrtRand() & 0xff) - 0x80) << 0xc;

                        *(ushort*)(e + 0x1a) = 7;

                        SpawnStatusEffectEntity_0047AC40(
                            e, (int*)(e + 0x1c), TrapBurstDescriptors(), 0x57, 0, 3);

                        *(int*)(e + 0x28) = savedScale;
                        *(int*)(e + 0x38) = savedTint;
                        break;
                    }
                }

                // Ghidra: LAB_00479b0d — la position est rendue, quelle qu'ait ete la branche.
                *(int*)(e + 0x1c) = savedX;
                *(int*)(e + 0x20) = savedY;
                *(int*)(e + 0x24) = savedZ;
            }

            // Ghidra: LAB_00479b21 — il s'eclaircit de 0x080808 par image, jusqu'a 0xf0f0f0. Le
            // test masque l'octet de poids fort, l'addition non : Terre garde son 0x40000000.
            uint tint = *(uint*)(e + 0x38);
            if ((tint & 0x00ffffff) < 0x00f0f0f0) *(uint*)(e + 0x38) = tint + 0x00080808;
        }

        // Ghidra: LAB_00479b3c — IL RESPIRE, sur tous les chemins vivants : son echelle avance
        // d'un huitieme vers (sin(age << 8) >> 2) + 0x800.
        short scale = *(short*)(e + 0x28);
        int aim = ((int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) << 8) >> 2) - scale + 0x800;

        *(short*)(e + 0x10) += 1;

        short breathed = (short)((aim >> 3) + scale);
        *(short*)(e + 0x28) = breathed;
        *(short*)(e + 0x2a) = breathed;
        *(short*)(e + 0x2c) = breathed;
    }

    /// <summary>
    /// Ghidra: FUN_00479b90 @ 0x00479b90 — <b>la colonne d'eau de Liquide</b> : le sous-type
    /// <c>0x8b</c> de <see cref="s_StaticEntityHandlers_00551f30"/>. 34 octets, <b>absente de
    /// <c>Blaze.c</c></b> — le plus court corps de l'ecole 2, et de loin.
    ///
    /// <para>
    /// <b>Elle ne fait qu'une chose</b> : perdre un quart de sa <b>hauteur</b> par image — l'echelle
    /// en <c>+0x2a</c> seule, ni la largeur ni la profondeur — et disparaitre en passant sous
    /// <c>0x80</c>. Le sort la fait naitre haute (<c>+0x28 = 0x40001000</c>, donc une hauteur de
    /// <c>0x4000</c> pour une largeur de <c>0x1000</c>) : c'est une colonne qui s'affaisse.
    /// </para>
    /// </summary>
    private static void TickLiquidColumn_00479B90(byte* record)
    {
        byte* e = record;

        short tall = *(short*)(e + 0x2a);
        tall = (short)(tall - (short)(tall >> 2));
        *(short*)(e + 0x2a) = tall;

        if (tall < 0x80) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005518f8</c> — <b>le script de sons differes de Flamme</b>, neuf pas de
    /// huit octets. Meme format que <see cref="s_DelayedSoundScript_005518D8"/>, qu'il suit sans
    /// trou.
    ///
    /// <para>
    /// <b>Un souffle puis une gamme de huit.</b> Le premier pas est le <c>0x11</c> grave
    /// (<c>0x01bf</c>) que tous les sorts de l'ecole ouvrent — c'est le meme premier pas que
    /// Liquide (<see cref="s_DelayedSoundScript_00551978"/>) —, puis <b>huit fois le son
    /// <c>0x21</c>, de plus en plus aigu</b>, de <c>0x02d4</c> a <c>0x0400</c> par pas d'environ
    /// <c>0x2a</c>, <b>espaces de cinq images</b> : le crepitement qui monte pendant que les trois
    /// elementaires grossissent. Trente-six images en tout, ce qui recouvre exactement la levee.
    /// </para>
    /// <para>
    /// La borne est le <c>[1] = 0</c> du neuvieme pas — <b>ce script-la se termine proprement</b>,
    /// contrairement a celui de Liquide, dont le dernier pas garde un delai de 4 et deborde sur des
    /// octets qui ne sont pas un script.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_005518F8 =
    [
        0x11, 0x01, 0x01, 0x00, 0xbf, 0x01, 0x00, 0x12,   // 0x005518f8
        0x21, 0x05, 0x01, 0x00, 0xd4, 0x02, 0x00, 0x12,   // 0x00551900
        0x21, 0x05, 0x01, 0x00, 0xff, 0x02, 0x00, 0x12,   // 0x00551908
        0x21, 0x05, 0x01, 0x00, 0x29, 0x03, 0x00, 0x12,   // 0x00551910
        0x21, 0x05, 0x01, 0x00, 0x54, 0x03, 0x00, 0x12,   // 0x00551918
        0x21, 0x05, 0x01, 0x00, 0x7e, 0x03, 0x00, 0x12,   // 0x00551920
        0x21, 0x05, 0x01, 0x00, 0xa9, 0x03, 0x00, 0x12,   // 0x00551928
        0x21, 0x05, 0x01, 0x00, 0xd3, 0x03, 0x00, 0x12,   // 0x00551930
        0x21, 0x00, 0x01, 0x00, 0x00, 0x04, 0x00, 0x12,   // 0x00551938 — delai nul : fin
    ];

    private static byte* s_DelayedSoundScript005518F8Storage;

    /// <summary>Les neuf pas du script en memoire native.</summary>
    private static byte* DelayedSoundScript005518F8()
        => NativeCopy(s_DelayedSoundScript_005518F8, ref s_DelayedSoundScript005518F8Storage);

    // =====================================================================
    // FLAMME — ecole 2, indice 17, sous-type 0xb1
    //
    //   0x00479300   LE SORT. Sous-type 0xb1 de 0x00551c30. 392 octets, ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00479300.asm.
    //   0x00479490   L'ELEMENTAIRE, deja ecrit pour Liquide — TickElementalFamiliar_00479490.
    //                Il se branche seul sur l'element par son +0x06 ; l'entree 1 de la table
    //                0x00479b78 est celle de Flamme, et elle etait deja transcrite.
    //   0x00479BC0   LA BRAISE, sous-type 0x57 de 0x00552230. 131 octets, ABSENTE de Blaze.c ;
    //                releve sous Legacy/Raw/Blaze/FUN_00479BC0.asm. C'est le seul corps qui
    //                manquait vraiment, et Terre l'emploiera aussi.
    //
    // LE SORT EST LE JUMEAU EXACT DE LIQUIDE. Les 392 octets des deux corps sont IDENTIQUES a
    // six deplacements d'appel pres — qui different tous EXACTEMENT du decalage des deux
    // fonctions, donc designent les memes cibles — et a UN SEUL mot : le pointeur de script de
    // sons differes empile, 0x005518f8 ici contre 0x00551978 la.
    // Le compilateur a emis deux fois le meme corps ; la verification est deux `od` et une
    // soustraction, faite le 2026-09-01 (voir « Check for a duplicate before transcribing » dans
    // CLAUDE.md). Tout ce que dit la note de TickLiquid_00479F70 vaut mot pour mot ici : trois
    // elementaires a 120 degres, deux PM toutes les cinq images, l'age fige a 1, et l'attente du
    // bit 0x2 que seul l'enfant pose.
    //
    // CE QUI CHANGE EST DONC ENTIEREMENT DANS L'ENFANT, et l'enfant etait deja ecrit : la branche
    // « case 1 » de TickElementalFamiliar_00479490 seme une braise UNE IMAGE SUR DEUX — deux fois
    // plus dense que la colonne d'eau de Liquide, qui n'en semait qu'une sur quatre — dispersee de
    // +-0x100 unites sur les TROIS axes, la seule des quatre a le faire en hauteur aussi. Il ne
    // manquait que le corps de la braise elle-meme.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00479300 @ 0x00479300 — <b>Flamme</b> : le sous-type <c>0xb1</c>, creneau 177
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>dix-huitieme fiche de l'ecole
    /// 2</b>. 392 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00479300.asm</c>.
    ///
    /// <para>
    /// <b>C'est le meme corps que <see cref="TickLiquid_00479F70"/>, octet pour octet</b>, a la
    /// seule exception du script de sons empile — <see cref="DelayedSoundScript005518F8"/> au lieu
    /// de <see cref="DelayedSoundScript00551978"/>. Le corps n'est pas ecrit une fois et appele
    /// deux : il est present deux fois dans le binaire, et il est donc transcrit deux fois ici,
    /// pour que chaque adresse garde sa tracabilite.
    /// </para>
    /// <para>
    /// <b>Trois elementaires naissent a la premiere image</b>, a 120 degres l'un de l'autre
    /// (<c>0x555</c> de pas) sur un cercle d'une <b>demi-stature</b> autour du lanceur, trente
    /// unites au-dessus du sol, chacun avec sa propre phase en <c>+0x12</c>. Ensuite le sort ne
    /// fait plus que <b>payer deux PM toutes les cinq images</b> tant que le bouton reste tenu, et
    /// <b>seulement une fois qu'un elementaire a pose le bit <c>0x2</c></b> sur son enregistrement.
    /// Son age reste a 1, donc le repli des cent-vingt images est inatteignable — transcrit tel
    /// quel, comme chez Liquide.
    /// </para>
    /// </summary>
    private static void TickFlame_00479300(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_0047931e
        short age = *(short*)(e + 0x10);

        if (age == 0)
        {
            int savedX = *(int*)(e + 0x1c);
            int savedZ = *(int*)(e + 0x24);
            int angle = *(short*)(e + 0x32);

            *(ushort*)(e + 0x10) = 1;

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));

            *(ushort*)(e + 0x12) = 0;
            e[0x04] = 0x39;

            *(short*)(e + 0x32) += 0x400;

            // TRENTE UNITES AU-DESSUS DU SOL, mesurees depuis +0x60.
            *(int*)(e + 0x20) = ((height + 0x1e) << 0xc) + *(int*)(e + 0x60);

            int radius = height >> 1;

            // TROIS ELEMENTAIRES, a un tiers de tour l'un de l'autre.
            for (int remaining = 3; remaining != 0; remaining--)
            {
                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * radius + savedX;
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * radius + savedZ;

                SpawnCastEntity_0047AAE0(e, 0x7f, e[0x0b], -1);

                // La phase de chacun, du meme pas : ils tourneront en decalage.
                *(short*)(e + 0x12) += 0x555;
                angle += 0x555;
            }

            // LA SEULE DIFFERENCE AVEC LIQUIDE : le script du crepitement qui monte.
            Aud.QueueDelayedSoundScript(DelayedSoundScript005518F8(), (int*)(e + 0x1c));

            // Le lanceur est immobilise ; c'est l'elementaire qui le relachera.
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;
            return;
        }

        // Ghidra: LAB_004793dc — RIEN NE SE PASSE TANT QU'AUCUN ELEMENTAIRE N'EST PRET.
        if ((*(uint*)e & 2) == 0) return;

        if ((*(uint*)e & 0x800000) != 0)
        {
            byte* caster = *(byte**)(e + 0x74);
            uint index = ((uint*)caster)[2] & 0xff;
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((Input.CombatMask(*(ushort*)(perChar + 0x1fc4)) & pad) == 0)
            {
                *(uint*)e |= 0x2000000;                   // le bouton est relache
            }

            // Ghidra: LAB_0047942c — DEUX PM TOUTES LES CINQ IMAGES.
            if (*(uint*)Mem.Entity(0x005f2cf0) % 5 == 0)
            {
                short mana = *(short*)(perChar + 0x14e);

                if (mana >= 2)
                {
                    *(short*)(perChar + 0x14e) = (short)(mana - 2);
                }
                else
                {
                    // Ghidra: LAB_00479457
                    *(ushort*)(perChar + 0x14e) = 0;
                    *(uint*)e |= 0x2000000;
                }
            }
        }
        else if (age >= 0x78)
        {
            // Ghidra: LAB_00479462 — inatteignable : l'age reste a 1.
            *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_0047946f — et le lanceur doit tenir debout.
        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
    }

    /// <summary>
    /// Ghidra: FUN_00479bc0 @ 0x00479bc0 — <b>la braise de Flamme</b> : le sous-type <c>0x57</c> de
    /// <see cref="s_StaticEntityHandlers_00552230"/>. 131 octets, <b>absente de <c>Blaze.c</c></b> ;
    /// le releve est garde sous <c>Legacy/Raw/Blaze/FUN_00479BC0.asm</c>.
    /// <b>Terre l'emploie aussi</b>, avec un autre atlas et une autre dispersion.
    ///
    /// <para>
    /// <b>Elle est aspiree par l'elementaire qui l'a semee.</b> A chaque image elle avance d'un
    /// <b>quart</b> de l'ecart qui la separe de son parent (<c>+0x6c</c>) sur les trois axes a la
    /// fois : une convergence geometrique, donc de plus en plus lente, qui la fait lecher
    /// l'elementaire sans jamais l'atteindre exactement. Son image d'atlas avance d'un cran par
    /// image et reboucle sur <c>+0x1a</c> — quatre pour Flamme, sept pour Terre.
    /// </para>
    /// <para>
    /// <b>Et elle meurt de trop pres.</b> Le pas vertical qu'elle vient de faire est compare a
    /// <c>0x400</c> — un quart d'unite — <b>en signe</b> : elle disparait des que ce pas devient
    /// trop petit, donc des qu'elle a rejoint son parent, <b>mais aussi tout de suite si elle est
    /// nee au-dessus de lui</b>, le pas etant alors negatif. Comme la branche Flamme de
    /// <see cref="TickElementalFamiliar_00479490"/> disperse la braise de <c>+-0x100</c> unites en
    /// hauteur aussi, <b>une braise sur deux ne vit qu'une image</b> : ce qui monte est ce qui
    /// reste. Elle meurt egalement si son parent porte <c>0x40000000</c>.
    /// </para>
    /// <para>
    /// <b>L'eclaircissement de <c>+0x38</c> est mort.</b> Le corps ajoute <c>0x202020</c> par image
    /// tant que le mot est sous <c>0xe0e0e0</c>, mais il compare le mot <b>entier</b>, octet de
    /// poids fort compris — alors que <see cref="TickElementalFamiliar_00479490"/>, pour le meme
    /// champ, masque cet octet. Or les deux seuls semeurs de braise la font naitre avec
    /// <c>+0x38 = 0x01000000</c>, deja au-dessus de la borne : la teinte ne bouge jamais.
    /// Transcrit tel quel — voir <c>Documentation/Reference/legacy-bug.md</c>.
    /// </para>
    /// </summary>
    private static void TickFlameEmber_00479BC0(byte* record)
    {
        byte* e = record;

        byte* parent = *(byte**)(e + 0x6c);

        int ownY = *(int*)(e + 0x20);
        int ownZ = *(int*)(e + 0x24);
        int ownX = *(int*)(e + 0x1c);

        // LE QUART DE L'ECART, sur les trois axes. Le pas vertical est retenu : c'est lui qui
        // decide de la mort, plus bas.
        int stepY = (*(int*)(parent + 0x20) - ownY) >> 2;

        *(int*)(e + 0x20) = ownY + stepY;
        *(int*)(e + 0x24) = ((*(int*)(parent + 0x24) - ownZ) >> 2) + ownZ;
        *(int*)(e + 0x1c) = ((*(int*)(parent + 0x1c) - ownX) >> 2) + ownX;

        // L'eclaircissement qui n'a jamais lieu : la comparaison ne masque pas le poids fort.
        uint tint = *(uint*)(e + 0x38);
        if (tint < 0x00e0e0e0) *(uint*)(e + 0x38) = tint + 0x00202020;

        // Ghidra: LAB_00479c12 — l'image d'atlas, un cran par image, bouclee sur +0x1a.
        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if (frame >= *(short*)(e + 0x1a)) e[0x40] = 0;

        // Ghidra: LAB_00479c28 — trop pres, ou nee trop haut, ou le parent s'en va.
        if (stepY < 0x400 || (*(uint*)parent & 0x40000000) != 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551968</c> — <b>le script de sons differes de Zephe</b>, deux pas de
    /// huit octets. <b>Le plus court des quatre elementaires</b>, et de loin.
    ///
    /// <para>
    /// Le <c>0x11</c> grave (<c>0x01bf</c>) que les quatre ouvrent, puis <b>un seul <c>0x23</c>
    /// tres aigu</b> (<c>0x0653</c>) l'image suivante : un coup de vent bref, la ou Flamme etale
    /// huit crepitements sur trente-six images. <b>Ce script-la se termine proprement</b> — le
    /// <c>[1] = 0</c> du second pas est sa borne.
    /// </para>
    /// <para>
    /// C'est aussi <b>celui sur lequel deborde le script de Terre</b> (<c>0x00551940</c>), qui n'a
    /// pas de terminateur — voir LB-027 dans <c>Documentation/Reference/legacy-bug.md</c>. C'est
    /// pourquoi il n'a <b>pas de tableau a lui</b> : il est la <b>fin</b> de
    /// <see cref="s_DelayedSoundScript_00551940"/>, a <c>+0x28</c>, exactement comme les deux se
    /// suivent dans <c>.data</c>. Les separer en deux allocations casserait le debordement, qui
    /// est ici reproductible et sans consequence — a la difference de celui de Liquide, qui sort
    /// de la zone des scripts. Meme facon de faire que <see cref="Descriptors00551BC0"/>.
    /// </para>
    /// </summary>
    private static byte* DelayedSoundScript00551968() => DelayedSoundScript00551940() + 0x28;

    // =====================================================================
    // ZEPHE — ecole 2, indice 18, sous-type 0xb2
    //
    //   0x00479DE0   LE SORT. Sous-type 0xb2 de 0x00551c30. 392 octets, ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00479DE0.asm.
    //   0x00479490   L'ELEMENTAIRE, deja ecrit — TickElementalFamiliar_00479490, entree 2 de la
    //                table 0x00479b78.
    //   0x00482EF0   LA BOURRASQUE, sous-type 0x50 de 0x00552230 — DEJA ECRITE, et pas pour lui :
    //                c'est TickFireOrbTrail_00482EF0, la trainee de BOULE DE FEU.
    //
    // LE TROISIEME JUMEAU. Comme Flamme, les 392 octets sont ceux de Liquide a six deplacements
    // d'appel pres — et les six different EXACTEMENT du decalage des deux fonctions (0x190), donc
    // designent les memes cibles — et a un seul mot : le script de sons, 0x00551968 ici. La
    // verification est deux `od` et une soustraction (2026-09-01).
    //
    // ET CELUI-LA N'A DEMANDE AUCUN CORPS NEUF. Sa branche « case 2 » de l'elementaire etait
    // ecrite, et le sous-type 0x50 qu'elle engendre l'etait aussi — depuis Boule de feu, qui
    // n'a rien a voir avec l'ecole 2. C'est le premier reemploi du depot qui traverse deux
    // ecoles : la trainee spherique de la boule de feu sert de bourrasque a l'elementaire d'air.
    // Le corps convient parce qu'il ne lit que ce que le semeur lui a mis : un rayon en +0x12,
    // un cap et un tangage en +0x32/+0x34, et une teinte en +0x38. L'elementaire les tire au sort
    // a chaque bourrasque — rayon 0xc8 a 0x147, cap quelconque, tangage +-0x100, teinte NULLE.
    //
    // Cette teinte nulle a une consequence : le meme test non masque qui rend l'eclaircissement
    // de la braise inatteignable (LB-028) fonctionne ici sans probleme, parce que la bourrasque
    // part de zero et non de 0x01000000. Le defaut est dans ce que les semeurs ecrivent, pas dans
    // le test.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00479de0 @ 0x00479de0 — <b>Zephe</b> : le sous-type <c>0xb2</c>, creneau 178 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>dix-neuvieme fiche de l'ecole 2</b>.
    /// 392 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00479DE0.asm</c>.
    ///
    /// <para>
    /// <b>C'est le meme corps que <see cref="TickLiquid_00479F70"/> et
    /// <see cref="TickFlame_00479300"/>, octet pour octet</b>, a la seule exception du script de
    /// sons empile — <see cref="DelayedSoundScript00551968"/>. Trois des quatre elementaires
    /// partagent ainsi le meme corps recopie trois fois par le compilateur ; chacun est transcrit
    /// a son adresse pour garder sa tracabilite.
    /// </para>
    /// <para>
    /// <b>Trois elementaires naissent a la premiere image</b>, a 120 degres l'un de l'autre
    /// (<c>0x555</c> de pas) sur un cercle d'une <b>demi-stature</b> autour du lanceur, trente
    /// unites au-dessus du sol, chacun avec sa propre phase en <c>+0x12</c>. Ensuite le sort ne
    /// fait plus que <b>payer deux PM toutes les cinq images</b> tant que le bouton reste tenu, et
    /// <b>seulement une fois qu'un elementaire a pose le bit <c>0x2</c></b> sur son enregistrement.
    /// Son age reste a 1, donc le repli des cent-vingt images est inatteignable — transcrit tel
    /// quel, comme chez les deux autres.
    /// </para>
    /// </summary>
    private static void TickZephyr_00479DE0(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00479dfe
        short age = *(short*)(e + 0x10);

        if (age == 0)
        {
            int savedX = *(int*)(e + 0x1c);
            int savedZ = *(int*)(e + 0x24);
            int angle = *(short*)(e + 0x32);

            *(ushort*)(e + 0x10) = 1;

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));

            *(ushort*)(e + 0x12) = 0;
            e[0x04] = 0x39;

            *(short*)(e + 0x32) += 0x400;

            // TRENTE UNITES AU-DESSUS DU SOL, mesurees depuis +0x60.
            *(int*)(e + 0x20) = ((height + 0x1e) << 0xc) + *(int*)(e + 0x60);

            int radius = height >> 1;

            // TROIS ELEMENTAIRES, a un tiers de tour l'un de l'autre.
            for (int remaining = 3; remaining != 0; remaining--)
            {
                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * radius + savedX;
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * radius + savedZ;

                SpawnCastEntity_0047AAE0(e, 0x7f, e[0x0b], -1);

                // La phase de chacun, du meme pas : ils tourneront en decalage.
                *(short*)(e + 0x12) += 0x555;
                angle += 0x555;
            }

            // LA SEULE DIFFERENCE AVEC LIQUIDE : le coup de vent bref, deux sons.
            Aud.QueueDelayedSoundScript(DelayedSoundScript00551968(), (int*)(e + 0x1c));

            // Le lanceur est immobilise ; c'est l'elementaire qui le relachera.
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;
            return;
        }

        // Ghidra: LAB_00479ebc — RIEN NE SE PASSE TANT QU'AUCUN ELEMENTAIRE N'EST PRET.
        if ((*(uint*)e & 2) == 0) return;

        if ((*(uint*)e & 0x800000) != 0)
        {
            byte* caster = *(byte**)(e + 0x74);
            uint index = ((uint*)caster)[2] & 0xff;
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((Input.CombatMask(*(ushort*)(perChar + 0x1fc4)) & pad) == 0)
            {
                *(uint*)e |= 0x2000000;                   // le bouton est relache
            }

            // Ghidra: LAB_00479f0c — DEUX PM TOUTES LES CINQ IMAGES.
            if (*(uint*)Mem.Entity(0x005f2cf0) % 5 == 0)
            {
                short mana = *(short*)(perChar + 0x14e);

                if (mana >= 2)
                {
                    *(short*)(perChar + 0x14e) = (short)(mana - 2);
                }
                else
                {
                    // Ghidra: LAB_00479f37
                    *(ushort*)(perChar + 0x14e) = 0;
                    *(uint*)e |= 0x2000000;
                }
            }
        }
        else if (age >= 0x78)
        {
            // Ghidra: LAB_00479f42 — inatteignable : l'age reste a 1.
            *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_00479f4f — et le lanceur doit tenir debout.
        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00551940</c> — <b>le script de sons differes de Terre</b>, cinq pas de
    /// huit octets… <b>et les deux de Zephe a la suite</b>, parce que le sien n'a pas de
    /// terminateur.
    ///
    /// <para>
    /// Le <c>0x11</c> grave (<c>0x01bf</c>) que les quatre elementaires ouvrent, puis <b>quatre
    /// fois le son <c>0x04</c></b> — un grondement sourd — de <c>0x0200</c> a <c>0x027f</c>,
    /// <b>espaces de dix images</b> : la montee la plus lente des quatre, quarante images pour le
    /// seul element qui sorte du sol.
    /// </para>
    /// <para>
    /// <b>Le cinquieme pas garde un delai de 10 au lieu de zero</b> : le script ne se termine pas,
    /// et <c>Audio.TickDelayedSounds</c> continue dans les huit octets suivants — qui sont le
    /// <b>script de Zephe</b> (<c>0x00551968</c>). Terre joue donc <b>deux sons de trop</b>, le
    /// <c>0x11</c> d'ouverture puis un <c>0x23</c>, et s'arrete la, sur le delai nul de Zephe.
    /// </para>
    /// <para>
    /// <b>Ce debordement-la est reproduit tel quel</b>, et c'est pour cela que les sept pas sont
    /// declares ensemble : les deux scripts se suivent sans trou dans <c>.data</c>, et
    /// <see cref="DelayedSoundScript00551968"/> est une vue a <c>+0x28</c> de ce meme bloc. C'est
    /// la difference avec celui de Liquide (LB-027), qui deborde lui dans une table de
    /// descripteurs et marche ensuite dans <c>.data</c> sans jamais s'arreter — celui-la est une
    /// divergence declaree, celui-ci n'en est pas une.
    /// </para>
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00551940 =
    [
        0x11, 0x01, 0x01, 0x00, 0xbf, 0x01, 0x00, 0x12,   // 0x00551940
        0x04, 0x0a, 0x01, 0x00, 0x00, 0x02, 0x00, 0x12,   // 0x00551948
        0x04, 0x0a, 0x01, 0x00, 0x2a, 0x02, 0x00, 0x12,   // 0x00551950
        0x04, 0x0a, 0x01, 0x00, 0x55, 0x02, 0x00, 0x12,   // 0x00551958
        0x04, 0x0a, 0x01, 0x00, 0x7f, 0x02, 0x00, 0x12,   // 0x00551960 — delai 10 : PAS de fin
        0x11, 0x01, 0x01, 0x00, 0xbf, 0x01, 0x00, 0x12,   // 0x00551968 — deja le script de Zephe
        0x23, 0x00, 0x01, 0x00, 0x53, 0x06, 0x00, 0x12,   // 0x00551970 — delai nul : fin, enfin
    ];

    private static byte* s_DelayedSoundScript00551940Storage;

    /// <summary>Les cinq pas de Terre et les deux de Zephe, en memoire native et d'un bloc.
    /// <see cref="DelayedSoundScript00551968"/> en est la fin.</summary>
    private static byte* DelayedSoundScript00551940()
        => NativeCopy(s_DelayedSoundScript_00551940, ref s_DelayedSoundScript00551940Storage);

    // =====================================================================
    // TERRE — ecole 2, indice 19, sous-type 0xb3. LA DERNIERE FICHE DE L'ECOLE.
    //
    //   0x00479C50   LE SORT. Sous-type 0xb3 de 0x00551c30. 392 octets, ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00479C50.asm.
    //   0x00479490   L'ELEMENTAIRE, deja ecrit — TickElementalFamiliar_00479490, entree 3 de la
    //                table 0x00479b78.
    //   0x00479BC0   LA MOTTE, sous-type 0x57 de 0x00552230 — DEJA ECRITE : c'est la braise de
    //                Flamme, TickFlameEmber_00479BC0, le meme corps pour les deux elements.
    //
    // LE QUATRIEME ET DERNIER JUMEAU. Les 392 octets sont encore ceux de Liquide, a six
    // deplacements d'appel pres — qui different tous exactement du decalage des deux fonctions
    // (0x320), donc designent les memes cibles — et au seul pointeur de script, 0x00551940 ici.
    // Les QUATRE fiches partagent donc un unique corps, que le compilateur a recopie quatre fois ;
    // c'est la seule fratrie du depot dont aucun membre n'apporte une ligne de code a lui.
    //
    // ET IL EST LE SEUL DES QUATRE A NAITRE DEJA TEINTE. La branche « case 3 » de l'elementaire
    // pose 0x40000000 dans son +0x38 la ou les trois autres partent de zero (0x004797d1) ; le
    // test d'eclaircissement de l'elementaire masque justement ce poids fort, ce qui le laisse
    // s'eclaircir quand meme. Le meme masque manque dans la motte, et c'est LB-028.
    //
    // CE QUE SA BRANCHE SEME. Une motte une image sur quatre — la cadence de Liquide, pas celle de
    // Flamme —, au RAS DU SOL (+0x20 force a +0x60) et dispersee de +-0x80 unites en X et Z
    // seulement. C'est la seule des quatre a ne rien disperser en hauteur, ce qui est cohérent :
    // ses mottes sortent de terre, elles n'ont aucune raison de flotter.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00479c50 @ 0x00479c50 — <b>Terre</b> : le sous-type <c>0xb3</c>, creneau 179 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>vingtieme et derniere fiche de
    /// l'ecole 2</b>. 392 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00479C50.asm</c>.
    ///
    /// <para>
    /// <b>C'est le meme corps que <see cref="TickLiquid_00479F70"/>,
    /// <see cref="TickFlame_00479300"/> et <see cref="TickZephyr_00479DE0"/>, octet pour
    /// octet</b>, a la seule exception du script de sons empile —
    /// <see cref="DelayedSoundScript00551940"/>. Les quatre elementaires partagent un unique
    /// corps, recopie quatre fois par le compilateur et transcrit quatre fois ici, chacun a son
    /// adresse.
    /// </para>
    /// <para>
    /// <b>Trois elementaires naissent a la premiere image</b>, a 120 degres l'un de l'autre
    /// (<c>0x555</c> de pas) sur un cercle d'une <b>demi-stature</b> autour du lanceur, trente
    /// unites au-dessus du sol, chacun avec sa propre phase en <c>+0x12</c>. Ensuite le sort ne
    /// fait plus que <b>payer deux PM toutes les cinq images</b> tant que le bouton reste tenu, et
    /// <b>seulement une fois qu'un elementaire a pose le bit <c>0x2</c></b> sur son enregistrement.
    /// Son age reste a 1, donc le repli des cent-vingt images est inatteignable — transcrit tel
    /// quel, comme chez les trois autres.
    /// </para>
    /// </summary>
    private static void TickEarth_00479C50(byte* record)
    {
        byte* e = record;

        if ((*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00479c6e
        short age = *(short*)(e + 0x10);

        if (age == 0)
        {
            int savedX = *(int*)(e + 0x1c);
            int savedZ = *(int*)(e + 0x24);
            int angle = *(short*)(e + 0x32);

            *(ushort*)(e + 0x10) = 1;

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x74));

            *(ushort*)(e + 0x12) = 0;
            e[0x04] = 0x39;

            *(short*)(e + 0x32) += 0x400;

            // TRENTE UNITES AU-DESSUS DU SOL, mesurees depuis +0x60.
            *(int*)(e + 0x20) = ((height + 0x1e) << 0xc) + *(int*)(e + 0x60);

            int radius = height >> 1;

            // TROIS ELEMENTAIRES, a un tiers de tour l'un de l'autre.
            for (int remaining = 3; remaining != 0; remaining--)
            {
                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * radius + savedX;
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * radius + savedZ;

                SpawnCastEntity_0047AAE0(e, 0x7f, e[0x0b], -1);

                // La phase de chacun, du meme pas : ils tourneront en decalage.
                *(short*)(e + 0x12) += 0x555;
                angle += 0x555;
            }

            // LA SEULE DIFFERENCE AVEC LIQUIDE : le grondement, et il deborde sur celui de Zephe.
            Aud.QueueDelayedSoundScript(DelayedSoundScript00551940(), (int*)(e + 0x1c));

            // Le lanceur est immobilise ; c'est l'elementaire qui le relachera.
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;
            return;
        }

        // Ghidra: LAB_00479d2c — RIEN NE SE PASSE TANT QU'AUCUN ELEMENTAIRE N'EST PRET.
        if ((*(uint*)e & 2) == 0) return;

        if ((*(uint*)e & 0x800000) != 0)
        {
            byte* caster = *(byte**)(e + 0x74);
            uint index = ((uint*)caster)[2] & 0xff;
            byte* perChar = GetMainMemPtr() + (index + 0x78) * 0x2000;

            int slot = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            ushort pad = *(ushort*)(Inp.s_InputStateBase_00604a20 + slot * 0x10);

            if ((Input.CombatMask(*(ushort*)(perChar + 0x1fc4)) & pad) == 0)
            {
                *(uint*)e |= 0x2000000;                   // le bouton est relache
            }

            // Ghidra: LAB_00479d7c — DEUX PM TOUTES LES CINQ IMAGES.
            if (*(uint*)Mem.Entity(0x005f2cf0) % 5 == 0)
            {
                short mana = *(short*)(perChar + 0x14e);

                if (mana >= 2)
                {
                    *(short*)(perChar + 0x14e) = (short)(mana - 2);
                }
                else
                {
                    // Ghidra: LAB_00479da7
                    *(ushort*)(perChar + 0x14e) = 0;
                    *(uint*)e |= 0x2000000;
                }
            }
        }
        else if (age >= 0x78)
        {
            // Ghidra: LAB_00479db2 — inatteignable : l'age reste a 1.
            *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_00479dbf — et le lanceur doit tenir debout.
        if (CanSustainChannel_00475110(e) != 0) *(uint*)e |= 0x2000000;
    }
}
