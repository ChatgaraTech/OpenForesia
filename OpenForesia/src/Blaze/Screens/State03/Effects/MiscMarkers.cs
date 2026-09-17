// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// CINQ GESTIONNAIRES DE MARQUE SANS FAMILLE — 0x00476770, 0x004767E0, 0x004758C0, 0x00483450,
// 0x004834D0.
//
// Ils ne forment pas un sujet : ce sont les cinq corps que la lecture du fichier d'origine a
// laisses entre les marques d'etat et l'appariement de combat, et dont le role commun n'est pas
// etabli. Ils restent ensemble plutot que d'etre disperses dans des fichiers ou ils mentiraient
// sur leur voisinage - regle de routage (4) d'ARCHITECTURE.md.
//
// Quand le role de l'un d'eux sera etabli, il part rejoindre sa famille et ce fichier maigrit.
// S'il se vide, il disparait.
//
// PLUS UN TRIO — 0x004830F0, 0x00483360, 0x00483260 (2026-09-17), les voisins d'adresse de
// 0x00483450 et 0x004834D0, et de la meme forme : des enfants a sous-type 0xc0 + n, resolus par
// les tables de sous-zone de la montagne du dragon de feu. Eux forment bien une famille — un pere
// qui lance, ce qui tombe, et l'anneau d'eclats qui frappe au point d'impact. Si elle grossit, ou
// quand son lanceur sera connu, elle merite son propre fichier.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_00476770 @ 0x00476770 — <b>l'entree 186 (<c>0xba</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>C'est une naissance, pas une vie</b> : elle regle une dizaine de champs, fait naitre une
    /// entite de sort <c>0xba</c> et <b>s'efface</b> (<c>*e = 0</c>, l'emplacement redevient libre).
    /// Le vrai comportement est celui de l'enfant, <see cref="EntityHandler_004767E0"/> — meme
    /// numero de sous-type, autre table de sauts (<c>0x00551f30</c>, celle ou
    /// <see cref="SpawnCastEntity_0047AAE0"/> depose).
    /// </para>
    /// <para>
    /// Elle part du cap deja range en <c>+0x32</c> et en tire une vitesse horizontale de <c>12</c>
    /// par <see cref="SetVectorFromAngle_00532B10"/>, met la verticale a zero et la gravite
    /// (<c>+0x9a</c>) a zero aussi : <b>ce qu'elle lance ne tombe pas</b>. La hauteur de depart
    /// vient de <see cref="StatusBubbleHeight_00538BC0"/>, arrondie au pair — la meme mesure que
    /// les bulles d'etat emploient pour se poser au-dessus d'une bete.
    /// </para>
    /// </summary>
    private static void EntityHandler_00476770(byte* e)
    {
        *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);

        uint height = (uint)StatusBubbleHeight_00538BC0(*(uint**)(e + 0x6c));
        *(int*)(e + 0x20) += (int)((height & 0xfffffffe) * 0x800);

        e[0x04] = 0x2a;
        *(short*)(e + 0x36) = 4;
        *(uint*)(e + 0x38) = 0x808080;
        *(uint*)(e + 0x28) = 0;
        *(short*)(e + 0x2c) = 0;

        SetVectorFromAngle_00532B10((int*)(e + 0x50), *(short*)(e + 0x32), 0xc);

        *(uint*)(e + 0x54) = 0;
        *(short*)(e + 0x9a) = 0;

        SpawnCastEntity_0047AAE0(e, 0xba, 1, 2);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004767e0</c> — <b>l'entree 186 (<c>0xba</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>, 192 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_004767E0.asm</c>.
    ///
    /// <para>
    /// C'est l'enfant de <see cref="EntityHandler_00476770"/>, et c'est lui qui <b>porte le
    /// coup</b> : chaque image il avance par <see cref="IntegrateWithDrag_00535880"/>, grossit d'un
    /// quart de ce qui lui reste pour atteindre <c>0x1000</c>, et tant que sa teinte n'est pas
    /// descendue sous <c>0x202020</c> il balaie le camp d'en face par
    /// <see cref="SweepOpposingSideForMeleeHit_00474C70"/>. Une entite touchee prend les degats de
    /// <see cref="ResolveMeleeHit_00539B80"/> — porte au compte du <b>lanceur</b>, range en
    /// <c>+0x74</c> par le pere, et non de l'entite elle-meme —, recoit le bit <c>0x40</c> et
    /// <c>0x0c</c> en <c>+0x96</c>.
    /// </para>
    /// <para>
    /// <b>Sa duree ne se compte pas, elle se soustrait</b> : la teinte perd <c>0x080808</c> par
    /// image, et l'entite meurt quand elle vaut <b>exactement</b> zero — le test est sur le drapeau
    /// de zero du <c>add</c>, pas sur un signe. Partie de <c>0x808080</c> que le pere a posee, cela
    /// fait <c>0x808080 / 0x080808 = 16</c> images pile, dont les treize premieres frappent
    /// (<c>0x808080 - 12 × 0x080808 = 0x202020</c>, et le test est un <c>jb</c> non signe).
    /// <b>Une teinte de depart qui ne serait pas un multiple de <c>0x080808</c> ferait vivre
    /// l'entite pour toujours</b> : c'est une fragilite du binaire d'origine, transcrite telle
    /// quelle.
    /// </para>
    /// <para>
    /// La sonde passee au balayage est une pile de six mots, dont <b>deux seulement</b> sont
    /// ecrits par ce corps : <c>+0x04</c> (<c>sonde[2]</c>, la demi-largeur) recoit
    /// <c>echelle × 80 &gt;&gt; 12</c> et <c>+0x0a</c> (<c>sonde[5]</c>, la profondeur) recoit
    /// <c>echelle × 90 &gt;&gt; 12</c> — la portee grandit donc avec l'entite. Le <c>0x64</c> pose
    /// a la naissance en <c>+0x06</c> (<c>sonde[3]</c>) est la <b>tolerance en hauteur</b> et
    /// <b>survit</b> jusqu'a l'appel ; les trois autres mots restent a zero.
    /// </para>
    /// <para>
    /// <b>Les deux decalages se lisent sur la pile, pas dans le pseudo-code.</b> Les six mots sont
    /// ecrits alors que l'argument de <c>FUN_00535880</c> est encore empile, et l'adresse de la
    /// sonde n'est prise (<c>lea ecx,[esp+0Ch]</c>) qu'apres le <c>add esp,4</c> qui le depile —
    /// d'ou un ecart de quatre octets entre les deux vues. La premiere transcription avait lu les
    /// deux ecritures dans la vue d'avant l'appel et les avait rangees en <c>sonde[0]</c> et
    /// <c>sonde[3]</c> : la demi-largeur et la profondeur tombaient a zero, le quadrilatere de
    /// frappe se reduisait a un <b>point</b>, et l'onde traversait les ennemis sans les toucher.
    /// Corrige le 2026-08-30.
    /// </para>
    /// </summary>
    private static void EntityHandler_004767E0(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = 0;
        probe[2] = 0;
        probe[3] = 0x64;
        probe[4] = 0;
        probe[5] = 0;

        IntegrateWithDrag_00535880(e);

        // Ghidra: `1000h - taille` puis `sar 2` — elle rejoint 0x1000 par quarts, sans jamais
        // l'atteindre tout a fait.
        int scale = *(short*)(e + 0x28);
        scale += (0x1000 - scale) >> 2;
        *(short*)(e + 0x28) = (short)scale;
        *(short*)(e + 0x2c) = (short)scale;

        // Ghidra: `mov [esp+16h],ax` puis `mov [esp+10h],dx`, dans la vue de pile d'APRES le
        // depilement de l'argument de FUN_00535880 — soit sonde[5] et sonde[2], pas sonde[3] et
        // sonde[0]. Voir l'en-tete.
        probe[5] = (short)(scale * 90 >> 0xc);
        probe[2] = (short)(scale * 80 >> 0xc);

        // Ghidra: `jb` — comparaison NON signee.
        if (*(uint*)(e + 0x38) >= 0x202020)
        {
            byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);

            // DAT_005de0b8 est le drapeau « quelque chose a ete touche » que le balayage pose ;
            // c'est lui, et non le retour, qui garde le dereferencement.
            if (*Mem.Camera(0x005de0b8) != 0)
            {
                ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);

                *(uint*)victim |= 0x40;
                *(short*)(victim + 0x96) = 0x0c;
            }
        }

        // Ghidra: LAB_0047688a — `add eax,0FFF7F7F8h` puis `jne` : la mort tient au drapeau de zero.
        uint tint = *(uint*)(e + 0x38) + 0xfff7f7f8;
        *(uint*)(e + 0x38) = tint;
        if (tint == 0) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_004758c0 @ 0x004758c0 — <b>l'entree 182 (<c>0xb6</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Meme forme que <see cref="EntityHandler_00476770"/> : une naissance qui s'efface. Elle
    /// depose une entite d'etat <c>0x3f</c> avec l'atlas <see cref="s_Descriptors_00551A60"/> et le
    /// parametre <c>9</c>, teinte de depart <c>0xf0f0f0</c> et echelle <c>0x1800</c> sur les deux
    /// axes.
    /// </para>
    /// <para>
    /// Elle range sa cible en <c>+0x78</c> et non en <c>+0x74</c> comme les deux autres — c'est le
    /// champ que l'enfant relit pour se placer.
    /// </para>
    /// </summary>
    private static void EntityHandler_004758C0(byte* e)
    {
        *(byte**)(e + 0x78) = *(byte**)(e + 0x6c);

        *(short*)(e + 0x0a) = 0;
        *(uint*)(e + 0x38) = 0xf0f0f0;
        *(uint*)(e + 0x28) = 0x18001800;
        e[0x40] = 0;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), Descriptors00551A60(), 0x3f, 0, 9);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00483450 @ 0x00483450 — <b>l'entree 223 (<c>0xdf</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Troisieme naissance de la meme forme, et la seule dont <b>le sous-type de l'enfant n'est pas
    /// une constante</b> : il vaut <c>0xc0 + n</c>, ou <c>n</c> est un octet lu dans le repertoire
    /// de zone (<c>+0xd4</c>, premier pointeur, octet <c>+2</c>). Comme <c>0xc0</c> vaut 192 et que
    /// les tables statiques n'ont que 192 creneaux, <b>l'enfant est toujours resolu dans la table
    /// rechargee par sous-zone</b>, jamais dans la table statique. Quel gestionnaire cela designe
    /// depend donc du lieu : ici la regle « un sous-type ne veut rien dire tout seul » est
    /// structurelle, et non accidentelle.
    /// </para>
    /// <para>
    /// Le cap <c>+0x32</c> est calcule et non recopie : <c>Ordinal_457</c> (le <c>ratan2</c> de
    /// libgte) sur la vitesse deja posee, <c>+0x50</c> en ordonnee et <c>+0x58</c> en abscisse.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483450(byte* e)
    {
        *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);

        uint height = (uint)StatusBubbleHeight_00538BC0(*(uint**)(e + 0x6c));
        *(int*)(e + 0x20) += (int)((height & 0xfffffffe) * 0x800);

        *(uint*)(e + 0x28) = 0x0a000a00;
        *(uint*)(e + 0x38) = 0x01000000;

        *(short*)(e + 0x32) =
            (short)Winsys3d.Ordinal_457(*(int*)(e + 0x50) >> 0xc, *(int*)(e + 0x58) >> 0xc);

        // Ghidra: `*(byte *)(**(int **)(DAT_005e97cc + 0xd4) + 2) + 0xc0`.
        byte* region = *(byte**)(s_Unknown_005e97cc + 0xd4);
        byte* first = *(byte**)region;
        byte subtype = (byte)(first[2] + 0xc0);

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors00551B00(), subtype, 0, 3);

        *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x004834d0</c> — <b>l'enfant de <see cref="EntityHandler_00483450"/></b>, 384
    /// octets, <b>absente de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_004834D0.asm</c>.
    ///
    /// <para>
    /// <b>Elle confirme le mecanisme annonce en suite 10.</b> Le pere engendre <c>0xc0 + n</c> ;
    /// dans la foret (zone 3, sous-zone 0) <c>n</c> vaut <c>0x1f</c>, donc <c>0xdf</c>, un numero
    /// au-dela des 192 creneaux statiques — c'est la <b>table rechargee par sous-zone</b> qui l'a
    /// resolu, et elle a designe ce corps. Dans un autre lieu le meme <c>0xdf</c> designerait autre
    /// chose. C'est la premiere fois que ce chemin-la est observe de bout en bout.
    /// </para>
    /// <para>
    /// <b>C'est un projectile qui roule et qui meurt de toucher.</b> Il integre sa vitesse
    /// lui-meme, avec un frottement plus faible que <see cref="IntegrateWithDrag_00535880"/> —
    /// <c>1/8 + 1/4096</c> au lieu de <c>1/8 + 1/256</c>, ecrit en clair ici plutot que factorise,
    /// parce que ce n'est pas la meme fonction.
    /// </para>
    /// <para>
    /// <b>Il tient au sol.</b> <see cref="FinalizeEntityPlacement2"/> resout la face portante, et
    /// le corps <b>sauvegarde <c>+0x20</c> avant l'appel pour le remettre apres</b> : la hauteur
    /// que la finalisation calcule est ignoree, seule celle du plancher <c>+0x60</c> compte. Quand
    /// il retombe dessous, il rebondit — <c>+0x54</c> remis a <c>0xa000</c>, les bits
    /// <c>0x2010</c> poses, l'altitude ramenee au plancher.
    /// </para>
    /// <para>
    /// <b>Sa mort est la regle inverse des autres.</b> Toutes les sorties effacent son mot d'etat
    /// (<c>*e = 0</c>, l'emplacement redevient libre) <b>sauf une</b> : celle ou le balayage ne
    /// trouve personne. Il ne survit donc que tant qu'il ne touche rien — les 2995 appels du releve
    /// du 2026-08-29 sont ce vol-la, repete. Quand il touche : degats au compte du lanceur
    /// (<c>+0x74</c>), bit <c>0x40</c> et <c>0x10</c> en <c>+0x96</c> sur la victime, son <c>3</c>,
    /// et l'eclaboussure <c>0xbd</c> avec l'atlas <see cref="s_SplashDescriptors_00551A08"/> — la
    /// meme que le jet du Goblin.
    /// </para>
    /// </summary>
    private static void EntityHandler_004834D0(byte* e)
    {
        // La sonde du balayage : six mots, poses avant tout le reste.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = -0x28;
        probe[2] = 0x28;
        probe[3] = 0x28;
        probe[4] = 0;
        probe[5] = 0x50;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            // Ghidra: `and ah,0F7h` puis `or eax,680h` — le bit 0x800 tombe, 0x680 se pose.
            *(uint*)e = (*(uint*)e & 0xfffff7ff) | 0x680;
        }

        // Ghidra: LAB_0048351c — l'integration, ecrite ici et non appelee.
        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        int gravity = *(short*)(e + 0x9a);

        *(int*)(e + 0x50) = (vx - (vx >> 0xc)) - (vx >> 3);
        *(int*)(e + 0x54) = ((gravity >> 8) - (vy >> 0xc)) + vy - gravity;
        *(int*)(e + 0x58) = (vz - (vz >> 0xc)) - (vz >> 3);

        // L'altitude est relue AVANT la finalisation et remise APRES : ce que celle-ci ecrit en
        // +0x20 est deliberement jete.
        int height = *(int*)(e + 0x20);

        FinalizeEntityPlacement2((uint*)e);       // Ghidra: FUN_00533940

        uint terrain = *(uint*)(e + 0x90);
        *(int*)(e + 0x20) = height;

        if ((terrain & 0xa000) == 0)
        {
            uint blocked = terrain & 0x802;

            if (blocked != 0 || height <= *(int*)(e + 0x60))
            {
                // Ghidra: LAB_004835a1 — le contact avec le plancher.
                uint state = *(uint*)e;

                if ((state & 0x10) != 0 || blocked != 0)
                {
                    *(uint*)e = 0;
                    return;
                }

                *(uint*)e = state | 0x2010;
                *(int*)(e + 0x54) = 0xa000;
                *(int*)(e + 0x20) = *(int*)(e + 0x60);
            }
        }

        // Ghidra: LAB_004835c9
        if ((*(uint*)e & 0x60000000) != 0)
        {
            *(uint*)e = 0;
            return;
        }

        byte* victim = SweepOpposingSideForMeleeHit_00474C70(e, probe);

        // LA seule sortie qui n'efface pas : personne de touche, il vole une image de plus.
        if (victim == null) return;

        if (*Mem.Camera(0x005de0b8) == 0)
        {
            *(uint*)e = 0;
            return;
        }

        ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);

        *(uint*)victim |= 0x40;
        *(short*)(victim + 0x96) = 0x10;

        Aud.PlayPositionalSound(3, 0x400, 0x1800, (int*)(e + 0x1c));   // Ghidra: FUN_0053db20

        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), SplashDescriptors(), 0xbd, 0, 4);

        *(uint*)e = 0;
    }

    // =====================================================================
    // LE TRIO DE LA MONTAGNE DU DRAGON DE FEU — zone 2, tables de sous-zone
    //
    //   0x004830F0   creneau 0x1d de 0x0056b0c8   le pere : il lance, attend, fait tourner l'anneau
    //   0x00483360   creneau 0x1e de 0x0056b2c8   ce qui tombe, et dit au pere ou
    //   0x00483260   creneau 0x1f de 0x0056b1c8   un eclat de l'anneau, qui frappe
    //
    // Les trois se tiennent par +0x70 : le pere s'y range lui-meme, et ses enfants le relisent.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552600</c> — <b>le script de sons differes du lancer</b>, que
    /// <see cref="EntityHandler_004830F0"/> met en file a sa naissance. Deux pas, voix <c>0x1f</c>,
    /// le second une image apres le premier. La borne est le delai nul du second
    /// (<c>1f 00</c>) ; <c>0x00552610</c>, qui suit, est un autre script, avec son propre lecteur.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552600 =
    [
        0x1f, 0x01, 0x01, 0x00, 0x00, 0x04, 0x00, 0x18,   // 0x00552600
        0x1f, 0x00, 0x01, 0x00, 0xfd, 0x06, 0x00, 0x18,   // 0x00552608 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552600Storage;

    private static byte* DelayedSoundScript00552600()
        => NativeCopy(s_DelayedSoundScript_00552600, ref s_DelayedSoundScript00552600Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00552610</c> — <b>le script de sons differes de l'impact</b>, que
    /// <see cref="EntityHandler_00483360"/> met en file quand il touche le sol. Cinq pas a cinq
    /// images d'intervalle : trois sur la voix <c>0x2e</c> a des hauteurs qui descendent, un
    /// <c>0x20</c>, et un <c>0x18</c> au delai nul. Il s'arrete juste avant
    /// <see cref="s_ExplosionSoundScript_00552638"/>.
    /// </summary>
    private static readonly byte[] s_DelayedSoundScript_00552610 =
    [
        0x2e, 0x05, 0x01, 0x00, 0x84, 0x03, 0x00, 0x18,   // 0x00552610
        0x2e, 0x05, 0x01, 0x00, 0x2a, 0x03, 0x00, 0x18,   // 0x00552618
        0x2e, 0x05, 0x01, 0x00, 0xd5, 0x02, 0x00, 0x18,   // 0x00552620
        0x20, 0x05, 0x01, 0x00, 0x84, 0x03, 0x00, 0x18,   // 0x00552628
        0x18, 0x00, 0x01, 0x00, 0x00, 0x04, 0x00, 0x10,   // 0x00552630 — [1] = 0 : fin
    ];

    private static byte* s_DelayedSoundScript00552610Storage;

    private static byte* DelayedSoundScript00552610()
        => NativeCopy(s_DelayedSoundScript_00552610, ref s_DelayedSoundScript00552610Storage);

    /// <summary>
    /// Ghidra: <c>0x004830f0</c> — <b>le pere du trio</b>, creneau <b>0x1d</b> du bloc
    /// <c>0x00555028</c> de la table <c>0x0056b0c8</c>, montagne du dragon de feu. <b>Absente de
    /// <c>Blaze.c</c></b> ; 118 instructions, <c>Legacy/Raw/Blaze/FUN_004830F0.asm</c>.
    ///
    /// <para>
    /// <b>A sa premiere image</b> (bit <c>0x1000000</c> absent), il se place par rapport a son
    /// lanceur <c>+0x6c</c> : le decalage <c>+0x44</c>/<c>+0x4c</c> est nie sur X et Z, tourne par
    /// les angles du lanceur (<see cref="RotateVectorByAngles_00536090"/> sur <c>lanceur + 0x30</c>)
    /// et ajoute a la position du lanceur. La vitesse <c>+0x50</c> part du cap du lanceur et du
    /// <c>+0x34</c> de l'entite, a <c>0x28</c>, et la gravite <c>+0x9a</c> vaut <c>0x3000</c>. Il
    /// fait alors naitre <b>ce qui tombe</b> — <see cref="SpawnStatusEffectEntity_0047AC40"/>,
    /// planche <c>0x00551bc8</c>, sous-type <c>0xc0 + n</c> ou <c>n</c> est l'octet <c>+0x0b</c> du
    /// premier enregistrement du repertoire <c>+0xd4</c> —, et met en file
    /// <see cref="s_DelayedSoundScript_00552600"/>. Il s'est range en <c>+0x70</c> avant la
    /// naissance, pour que l'enfant le retrouve.
    /// </para>
    /// <para>
    /// <b>La naissance ecrit deux fois l'echelle et la teinte</b> : <c>0x18001800</c> et
    /// <c>0xf0f0f0</c> avant la naissance de l'enfant, <c>0x70005000</c> / <c>0x5000</c> et
    /// <c>0x808080</c> apres. Le semeur recopie une partie de l'etat du pere : l'ordre est garde.
    /// </para>
    /// <para>
    /// <b>Ensuite, il attend.</b> Rien ne se passe tant que l'enfant n'a pas pose le bit <c>2</c> sur
    /// lui — le signal d'atterrissage, avec la position du point d'impact. A partir de la,
    /// pendant 39 images, il fait naitre a chaque image un <b>eclat</b> par
    /// <see cref="SpawnCastEntity_0047AAE0"/>, sous-type <c>0xc0 + m</c> (<c>m</c> : octet
    /// <c>+0x10</c> du repertoire <c>+0xd0</c>), apres avoir tire un rayon de depart
    /// <c>+0x12</c> de 0 a 63 et un cap <c>+0x32</c> quelconque, dont les eclats heritent.
    /// </para>
    /// <para>
    /// <b>Sa fin est un battement de cœur a l'envers.</b> Chaque image, il pose le bit <c>4</c> ;
    /// chaque eclat vivant l'efface (<see cref="EntityHandler_00483260"/>). Trouver le bit
    /// <c>4</c> encore pose veut dire qu'aucun eclat n'a vecu depuis l'image precedente : il pose
    /// alors <c>0x2000000</c> et son emplacement est libere. L'enfant qui tombe peut aussi poser
    /// <c>0x2000000</c> lui-meme, s'il sort du monde.
    /// </para>
    /// <para>
    /// Qui fait naitre ce pere n'est pas etabli : aucun corps transcrit ne produit le sous-type
    /// <c>0xdd</c> dans cette zone a ce jour.
    /// </para>
    /// </summary>
    private static void EntityHandler_004830F0(byte* e)
    {
        byte* caster = *(byte**)(e + 0x6c);
        *(byte**)(e + 0x74) = caster;

        uint state = *(uint*)e;

        if ((state & 0x1000000) != 0)
        {
            if ((state & 2) != 0)
            {
                if ((state & 4) != 0)
                {
                    *(uint*)e = state | 0x2000000;
                }
                else
                {
                    // Ghidra: LAB_00483118
                    (*(short*)(e + 0x10))++;
                    if (*(short*)(e + 0x10) < 0x28)
                    {
                        *(short*)(e + 0x12) = (short)(CrtRand() & 0x3f);   // Ghidra: FUN_00547140
                        *(short*)(e + 0x32) = (short)CrtRand();

                        // Ghidra: `mov al,[edx+10h]` puis `add eax,0C0h` — octet sans signe.
                        byte* directory = *(byte**)(s_Unknown_005e97cc + 0xd0);
                        byte shard = (byte)((*(byte**)directory)[0x10] + 0xc0);
                        SpawnCastEntity_0047AAE0(e, shard, 0, 2);
                    }

                    // Ghidra: LAB_0048315d — le mot d'etat est RELU apres la naissance.
                    *(uint*)e |= 4;
                }
            }

            // Ghidra: LAB_00483163
            if ((*(uint*)e & 0x2000000) != 0) *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_0048317c — la naissance.
        int* offset = stackalloc int[3];

        *(int*)(e + 0x44) = -*(int*)(e + 0x44);
        *(byte**)(e + 0x70) = e;
        *(int*)(e + 0x4c) = -*(int*)(e + 0x4c);
        *(uint*)(e + 0x28) = 0x18001800;
        *(uint*)(e + 0x38) = 0xf0f0f0;

        RotateVectorByAngles_00536090((short*)(caster + 0x30), (int*)(e + 0x44), offset);

        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + offset[0];
        *(int*)(e + 0x20) = *(int*)(caster + 0x20) + offset[1];
        *(int*)(e + 0x24) = *(int*)(caster + 0x24) + offset[2];

        SetVectorFromAngles_00532AD0(
            (int*)(e + 0x50), *(short*)(caster + 0x32), *(short*)(e + 0x34), 0x28);

        *(short*)(e + 0x9a) = 0x3000;

        byte* region = *(byte**)(s_Unknown_005e97cc + 0xd4);
        byte falling = (byte)((*(byte**)region)[0x0b] + 0xc0);
        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), Descriptors00551BC8(), falling, 0, 2);

        *(uint*)e |= 0x1000000;
        *(uint*)(e + 0x38) = 0x808080;
        *(uint*)(e + 0x28) = 0x70005000;
        *(short*)(e + 0x2c) = 0x5000;
        *(short*)(e + 0x2e) = -0x20;
        e[0x04] = 0x1e;

        Aud.QueueDelayedSoundScript(DelayedSoundScript00552600(), (int*)(e + 0x1c));   // Ghidra: FUN_0053dbe0
    }

    /// <summary>
    /// Ghidra: FUN_00483360 @ 0x00483360 — <b>ce qui tombe</b>, creneau <b>0x1e</b> du bloc
    /// <c>0x00555228</c> de la table <c>0x0056b2c8</c>. Decompilee dans <c>Blaze.c</c>, 87
    /// instructions.
    ///
    /// <para>
    /// Il integre sa vitesse lui-meme, avec le frottement faible de
    /// <see cref="EntityHandler_004834D0"/> (<c>1/8 + 1/4096</c>) et la gravite <c>+0x9a</c> que
    /// le pere a posee. Comme lui, il <b>sauvegarde <c>+0x20</c> autour de</b>
    /// <see cref="FinalizeEntityPlacement2"/> : ce que la finalisation calcule en hauteur est jete.
    /// </para>
    /// <para>
    /// <b>Il ne rebondit pas.</b> Des qu'il touche — un mur (<c>0x802</c>) ou le plancher
    /// <c>+0x60</c>, hors terrain <c>0xa000</c> —, il recopie sa position sur le pere <c>+0x70</c>,
    /// <b>l'altitude etant celle du plancher et non la sienne</b>, pose le bit <c>2</c> du pere,
    /// met en file <see cref="s_DelayedSoundScript_00552610"/> et s'efface. S'il porte
    /// <c>0x20000000</c> ou descend sous <c>-20000</c> unites, il s'efface aussi, mais en posant
    /// <c>0x2000000</c> sur le pere : l'anneau n'aura pas lieu.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483360(byte* e)
    {
        byte* parent = *(byte**)(e + 0x70);

        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x20) += vy;
        *(int*)(e + 0x24) += vz;

        int gravity = *(short*)(e + 0x9a);

        *(int*)(e + 0x50) = (vx - (vx >> 3)) - (vx >> 0xc);
        *(int*)(e + 0x54) = ((gravity >> 8) - (vy >> 0xc)) - gravity + vy;
        *(int*)(e + 0x58) = (vz - (vz >> 3)) - (vz >> 0xc);

        int height = *(int*)(e + 0x20);

        FinalizeEntityPlacement2((uint*)e);       // Ghidra: FUN_00533940

        uint terrain = *(uint*)(e + 0x90);
        *(int*)(e + 0x20) = height;

        if ((terrain & 0xa000) == 0 && ((terrain & 0x802) != 0 || height <= *(int*)(e + 0x60)))
        {
            // Ghidra: LAB_004833f4 — l'impact.
            *(int*)(parent + 0x1c) = *(int*)(e + 0x1c);
            *(int*)(parent + 0x20) = *(int*)(e + 0x60);
            *(int*)(parent + 0x24) = *(int*)(e + 0x24);
            *(uint*)parent |= 2;

            Aud.QueueDelayedSoundScript(DelayedSoundScript00552610(), (int*)(e + 0x1c));   // Ghidra: FUN_0053dbe0

            *(uint*)e = 0;
            return;
        }

        // Ghidra: LAB_00483429 — `cmp ebx,0FB1E0000h` puis `jge`, comparaison signee.
        if ((*(uint*)e & 0x20000000) != 0 || height < -0x4e20000)
        {
            *(uint*)e = 0;
            *(uint*)parent |= 0x2000000;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00483260</c> — <b>un eclat de l'anneau</b>, creneau <b>0x1f</b> du bloc
    /// <c>0x00555128</c> de la table <c>0x0056b1c8</c>. <b>Absente de <c>Blaze.c</c></b> ; 85
    /// instructions, <c>Legacy/Raw/Blaze/FUN_00483260.asm</c>.
    ///
    /// <para>
    /// <b>Il n'integre rien : il se place.</b> A chaque image il se pose sur le cercle de rayon
    /// <c>+0x12</c> autour du pere <c>+0x70</c>, dans son cap <c>+0x32</c> — sinus pour X, cosinus
    /// pour Z, produit non recadre —, et le rayon rattrape <c>400</c> d'un huitieme de l'ecart par
    /// image. L'altitude n'est pas touchee. Ses echelles perdent un seizieme par image.
    /// </para>
    /// <para>
    /// <b>Il entretient le pere</b> en effacant son bit <c>4</c> — voir
    /// <see cref="EntityHandler_004830F0"/>.
    /// </para>
    /// <para>
    /// <b>Il frappe pendant dix images</b>, par <see cref="ProbeMeleeTarget_00474CA0"/> avec la
    /// sonde <c>0, -200, 200, 50, 0, 400</c>. Une victime prend les degats au compte du lanceur
    /// <c>+0x74</c>, recoit le bit <c>0x40</c> et <c>8</c> en <c>+0x96</c>. Ensuite il cesse de
    /// frapper, et sa teinte perd <c>0x080808</c> par image jusqu'a valoir exactement zero, comme
    /// <see cref="EntityHandler_004767E0"/>.
    /// </para>
    /// <para>
    /// <b>C'est le retour du balayage qui garde les degats</b> (<c>test edi,edi</c>), et non le
    /// drapeau <c>0x005de0b8</c> que lit <see cref="EntityHandler_004767E0"/>.
    /// </para>
    /// <para>
    /// <b>La sonde se lit sur la pile.</b> Les six mots sont ecrits quand trois registres sont
    /// sauvegardes (<c>[esp+0Ch]</c> = mot 0) ; l'adresse est prise apres le <c>push edi</c>
    /// (<c>lea eax,[esp+10h]</c>), soit le meme mot 0. Le <c>0xff38</c> ecrit en <c>[esp+0Eh]</c>
    /// est le mot 1.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483260(byte* e)
    {
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = -200;
        probe[2] = 200;
        probe[3] = 0x32;
        probe[4] = 0;
        probe[5] = 400;

        byte* parent = *(byte**)(e + 0x70);
        *(uint*)parent &= 0xfffffffb;

        // Ghidra: `mov eax,190h` / `sub eax,edi` / `sar eax,3` / `add edi,eax` — sur 32 bits.
        int radius = *(short*)(e + 0x12);
        int course = *(short*)(e + 0x32);
        radius += (400 - radius) >> 3;
        *(short*)(e + 0x12) = (short)radius;

        int sin = (int)Winsys3d.GetSinTableEntry(course);   // Ghidra: 0x00545a1c, Ordinal_451
        *(int*)(e + 0x1c) = sin * radius + *(int*)(parent + 0x1c);

        int cos = Winsys3d.GetCosTableEntry(course);        // Ghidra: 0x00545a16, Ordinal_450
        *(int*)(e + 0x24) = cos * radius + *(int*)(parent + 0x24);

        int scaleX = *(short*)(e + 0x28);
        scaleX -= scaleX >> 4;
        *(short*)(e + 0x28) = (short)scaleX;
        *(short*)(e + 0x2c) = (short)scaleX;

        // Ghidra: `sar dx,4` sur le mot — decalage sur 16 bits.
        short scaleY = *(short*)(e + 0x2a);
        *(short*)(e + 0x2a) = (short)(scaleY - (short)(scaleY >> 4));

        if (*(short*)(e + 0x10) < 0x0a)
        {
            (*(short*)(e + 0x10))++;

            byte* victim = ProbeMeleeTarget_00474CA0(e, probe);
            if (victim == null) return;

            ResolveMeleeHit_00539B80(*(uint**)(e + 0x74), (uint*)victim);

            *(uint*)victim |= 0x40;
            *(short*)(victim + 0x96) = 8;
            return;
        }

        // Ghidra: LAB_00483340 — `add eax,0FFF7F7F8h` puis `jne` : la mort tient au drapeau de zero.
        uint tint = *(uint*)(e + 0x38) + 0xfff7f7f8;
        *(uint*)(e + 0x38) = tint;
        if (tint == 0) *(uint*)e = 0;
    }

    // =====================================================================
    // DEUX EMETTEURS DE LA VIEILLE TOUR — tables de sous-zone, sous-zones 3, 7 et 9 (2026-09-17)
    //
    //   0x00483930   creneau 31 de 0x0056b0c8   huit images, un enfant 0xc0 + n toutes les deux
    //   0x00484F80   creneau 29 de 0x0056b0c8   vingt images, un enfant 0xc0 + m a chaque image
    //
    // La meme charpente que le pere du trio : l'entite se range son lanceur +0x6c en +0x70 et
    // +0x74, nie ses decalages +0x44/+0x4c, et se replace chaque image sur le lanceur par ses
    // angles. L'enfant de 0x00483930 est vraisemblablement 0x00483A30 (creneau 31 de 0x0056b1c8,
    // Combat/Projectiles.cs) ; la valeur de l'octet du repertoire qui le designe n'est pas
    // verifiable hors du CD.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00483930</c> — <b>l'emetteur a huit images</b>, creneau 31 de la table
    /// <c>0x0056b0c8</c>, sous-zones 3, 7 et 9 de la vieille tour. Absente de <c>Blaze.c</c> ; 82
    /// instructions, <c>Legacy/Raw/Blaze/FUN_00483930.asm</c>.
    ///
    /// <para>
    /// <b>Sa naissance</b> : cap tourne d'un quart de tour (<c>+0x400</c> sur douze bits), lanceur
    /// range en <c>+0x70</c>/<c>+0x74</c>, genre <c>7</c>, echelle nulle,
    /// teinte <c>0xf0f0f0</c>, tangage <c>0x380</c>, gravite nulle, decalages nies, masque de
    /// victimes <c>+0x09</c> remis a zero, et le script de sons differes <c>0x00552660</c> mis en
    /// file — <b>l'enregistrement 5 du script de l'explosion</b>, pris au milieu.
    /// </para>
    /// <para>
    /// <b>Une image sur deux</b> (<c>test byte [esi+10h],1</c>), il se replace sur son lanceur, pose sa
    /// vitesse dans le cap du lanceur a un tangage de <c>0xf80</c> et une portee de 100, et fait
    /// naitre par <see cref="SpawnCastEntity_0047AAE0"/> l'enfant <c>0xc0 + octet 0x0b</c> du
    /// repertoire <c>+0xd0</c>. A la huitieme image l'emplacement est vide (<c>*e = 0</c>).
    /// </para>
    /// <para>
    /// Son jumeau de la grotte scellee, <c>0x00484AB0</c>, ne differe que par le script,
    /// <c>0x005526e0</c> ; il n'est pas branche.
    /// </para>
    /// </summary>
    private static void EntityHandler_00483930(byte* e)
        => TickSpawningEmitter_00483930(e, ExplosionSoundScript() + 0x28);   // Ghidra: `push 552660h`

    /// <summary>Le corps de <see cref="EntityHandler_00483930"/>, parametre par le script de sons
    /// que sa naissance met en file.</summary>
    private static void TickSpawningEmitter_00483930(byte* e, byte* soundScript)
    {
        int* offset = stackalloc int[3];
        byte* caster = *(byte**)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x32) += 0x400;
            *(byte**)(e + 0x74) = caster;
            *(byte**)(e + 0x70) = caster;
            *(short*)(e + 0x36) = 4;
            e[0x04] = 7;
            *(int*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            *(uint*)(e + 0x38) = 0xf0f0f0;
            *(short*)(e + 0x9a) = 0;
            *(short*)(e + 0x34) = 0x380;
            *(int*)(e + 0x44) = -*(int*)(e + 0x44);
            *(int*)(e + 0x4c) = -*(int*)(e + 0x4c);
            e[0x09] = 0;

            Aud.QueueDelayedSoundScript(soundScript, (int*)(e + 0x1c));   // Ghidra: FUN_0053dbe0
        }

        // Ghidra: LAB_0048399a
        if ((e[0x10] & 1) != 0)
        {
            RotateVectorByAngles_00536090((short*)(caster + 0x30), (int*)(e + 0x44), offset);

            *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + offset[0];
            *(int*)(e + 0x20) = *(int*)(caster + 0x20) + offset[1];
            *(int*)(e + 0x24) = *(int*)(caster + 0x24) + offset[2];

            SetVectorFromAngles_00532AD0((int*)(e + 0x50), *(short*)(caster + 0x32), 0xf80, 0x64);

            byte* directory = *(byte**)(s_Unknown_005e97cc + 0xd0);
            SpawnCastEntity_0047AAE0(e, (byte)((*(byte**)directory)[0x0b] + 0xc0), 0, 2);
        }

        // Ghidra: LAB_00483a17
        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) >= 8) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00484f80</c> — <b>l'emetteur a vingt images</b>, creneau 29 de la table
    /// <c>0x0056b0c8</c>, sous-zones 3, 7 et 9. Absente de <c>Blaze.c</c> ; 92 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00484F80.asm</c>.
    ///
    /// <para>
    /// <b>Sa naissance</b> : lanceur range en <c>+0x70</c>/<c>+0x74</c>, cap decale de <c>+0x9a</c>,
    /// genre <c>0x1e</c>, echelles <c>0x400</c>/<c>0x800</c>/<c>0x400</c>, teinte <c>0xf0f0f0</c>,
    /// decalages nies.
    /// </para>
    /// <para>
    /// <b>A chaque image</b> il se replace sur son lanceur, pose sa vitesse dans le cap du lanceur
    /// plus <c>+0x9a</c>, au tangage <c>+0x34</c> et a la portee <c>+0x5c</c> — puis met son
    /// tangage a zero le temps de faire naitre, par <see cref="SpawnStatusEffectEntity_0047AC40"/>,
    /// l'enfant <c>0xc0 + octet 0x0c</c> du repertoire <c>+0xd4</c> sur <b>l'une de cinq planches
    /// tirees au sort</b> : <c>0x00551b10</c>, <c>0x00551b18</c>, <c>0x00551b20</c> (trois
    /// enregistrements de <see cref="s_Descriptors_00551B00"/>), <c>0x00551bb8</c> et
    /// <c>0x00551ba8</c> (de <see cref="s_Descriptors_00551BA0"/>). La table des cinq adresses est
    /// construite sur la pile a chaque image. A la vingtieme image l'emplacement est vide.
    /// </para>
    /// </summary>
    private static void EntityHandler_00484F80(byte* e)
    {
        byte** plates = stackalloc byte*[5];
        plates[0] = Descriptors00551B00() + 0x10;   // Ghidra: 0x00551b10
        plates[1] = Descriptors00551B00() + 0x18;   // Ghidra: 0x00551b18
        plates[2] = Descriptors00551B00() + 0x20;   // Ghidra: 0x00551b20
        plates[3] = Descriptors00551BA0() + 0x18;   // Ghidra: 0x00551bb8
        plates[4] = Descriptors00551BA0() + 0x08;   // Ghidra: 0x00551ba8

        int* offset = stackalloc int[3];
        byte* caster = *(byte**)(e + 0x6c);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x74) = caster;
            *(int*)(e + 0x44) = -*(int*)(e + 0x44);
            *(int*)(e + 0x4c) = -*(int*)(e + 0x4c);
            *(short*)(e + 0x32) += *(short*)(e + 0x9a);
            *(byte**)(e + 0x70) = caster;
            *(short*)(e + 0x36) = 4;
            e[0x04] = 0x1e;
            *(uint*)(e + 0x28) = 0x08000400;
            *(short*)(e + 0x2c) = 0x400;
            *(uint*)(e + 0x38) = 0xf0f0f0;
        }

        // Ghidra: LAB_00484ffb
        RotateVectorByAngles_00536090((short*)(caster + 0x30), (int*)(e + 0x44), offset);

        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c) + offset[0];
        *(int*)(e + 0x20) = *(int*)(caster + 0x20) + offset[1];
        *(int*)(e + 0x24) = *(int*)(caster + 0x24) + offset[2];

        SetVectorFromAngles_00532AD0(
            (int*)(e + 0x50), *(short*)(caster + 0x32) + *(short*)(e + 0x9a),
            *(short*)(e + 0x34), *(int*)(e + 0x5c));

        short pitch = *(short*)(e + 0x34);
        *(short*)(e + 0x34) = 0;

        byte* region = *(byte**)(s_Unknown_005e97cc + 0xd4);
        byte child = (byte)((*(byte**)region)[0x0c] + 0xc0);
        byte* plate = plates[CrtRand() % 5];                                   // Ghidra: `mov edx,[esp+edx*4+24h]`
        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), plate, child, 0, 1);

        (*(short*)(e + 0x10))++;
        *(short*)(e + 0x34) = pitch;
        if (*(short*)(e + 0x10) >= 0x14) *(uint*)e = 0;
    }
}
