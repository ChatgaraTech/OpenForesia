// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ECOLE 1 — LE PRETRE. Les 24 fiches, une par une, de Pardon a Ange.
//
// Indices 0 a 23. Les sous-types ne se suivent PAS comme ceux de l'ecole 0 : ils vont de 0x1e a
// 0x35 mais avec deux intrus, Sonde (indice 4, sous-type 0x37) et 3eme oeil (indice 6, sous-type
// 0x38), poses hors de la suite. Le sous-type est lu dans la fiche, jamais deduit de l'indice, et
// c'est exactement pour ce genre de cas.
//
// Le compte de 24 est confirme par `DAT_00564600`. Le fichier s'ouvre sur les TROIS FONCTIONS
// PARTAGEES de l'ecole, avant les fiches.
//
// Chaque fiche a son bandeau `// ====` : c'est le sommaire de ce fichier. Le repartiteur commun,
// le ciblage et la naissance de l'entite de lancement sont dans `SpellCasting.cs`.
//
// Vue d'ensemble : Documentation/Reference/Sorts.md ; feuille de route : Documentation/Etat/SortsAtteignables.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // L'ECOLE 1 — LE PRETRE. Trois fonctions partagees, puis les fiches une a une.
    //
    // L'ecole 0 etait vingt-neuf corps independants, chacun repartant de zero. L'ecole 1 est
    // batie autrement : ses vingt-quatre fiches commencent presque toutes par le meme trio, et
    // c'est lui qu'il faut ecrire d'abord.
    //
    //   0x00448020   L'EXTINCTION AVEC REMISE DU DECOR. Vingt octets.
    //   0x00448040   L'EXTINCTION SUR CONTACT DU DECOR. Vingt et un octets.
    //   0x00447CB0   LE CHOIX DE LA CIBLE. Cent cinquante-deux octets, deux modes.
    //
    // Les trois sont decompilees dans Blaze.c, et les trois sont appelees avec TROIS arguments la
    // ou Ghidra n'en declare qu'un ou deux. Ce n'est pas une signature manquee : l'appel est
    // __cdecl, l'appelant depile lui-meme, et les corps ne lisent que les premiers. Transcrites
    // avec les arguments qu'elles lisent, la difference notee ici.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00448020 @ 0x00448020 — <b>eteindre un sort de pretre et rendre sa couleur au
    /// decor</b>. Vingt octets, decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Elle efface <c>0x80000000</c> — ce que la passe relit pour rendre le creneau au pool — et
    /// remet <c>DAT_005e9166</c> a zero, le mot de mode que les sorts de cette ecole posent a 2
    /// pendant qu'ils teintent le decor.
    /// </para>
    /// </summary>
    private static void EndSpellAndResetDecor_00448020(byte* e)
    {
        *(ushort*)Mem.Arena(0x005e9166) = 0;
        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00448040 @ 0x00448040 — <b>eteindre un sort de pretre que le decor a touche</b>.
    /// Vingt et un octets, decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Un seul test : si <c>0x40000000</c> est pose, <c>0x80000000</c> est efface. C'est la
    /// premiere instruction de presque toutes les fiches de l'ecole 1 — <b>elle ne rend pas la
    /// couleur du decor</b>, contrairement a sa voisine.
    /// </para>
    /// </summary>
    private static void EndSpellOnTerrain_00448040(byte* e)
    {
        if ((*(uint*)e & 0x40000000) != 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00447cb0 @ 0x00447cb0 — <b>le choix de la cible d'un sort de pretre</b>. Cent
    /// cinquante-deux octets, decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle pose d'abord le lanceur comme cible</b> (<c>+0x78 = +0x74</c>), puis, dans les deux
    /// modes connus, l'ecrase par <see cref="FindTargetAround_00474BA0"/>. La valeur de repli
    /// compte : un sort dont la recherche echoue vise son propre lanceur.
    /// </para>
    /// <para>
    /// <b>Le mode 1 ressuscite.</b> Avant la recherche il parcourt les <b>quatre</b> emplacements du
    /// groupe et, pour chacun qui porte <c>0x40000000</c> — mort —, retient le fait et efface
    /// <c>0x40002000</c> ; apres la recherche il repose les bits qu'il avait retires. La cible peut
    /// donc etre un membre mort, ce qu'aucune autre recherche du depot n'autorise : c'est le
    /// mecanisme de <b>Resurection</b> (<c>0x0044CD00</c>), <b>son seul appelant dans tout le
    /// binaire</b> : les quatorze appels decompiles passent <c>0</c>, et <c>0x0044cd20</c> est le
    /// seul <c>push 1</c>. La boucle est ecrite en dur sur quatre membres, sans consulter
    /// <see cref="s_PartySize_005e9020"/>.
    /// </para>
    /// <para>
    /// (Correction du 2026-08-31 : cette note citait aussi Sang d'ange, sur la foi de son nom.
    /// <see cref="TickAngelBlood_0044A4B0"/> appelle le mode <b>0</b> et pose la guerison complete,
    /// pas la resurrection.)
    /// </para>
    /// <para>
    /// <b>Ghidra lui declare deux arguments, les appelants en empilent trois.</b> L'appel est
    /// <c>__cdecl</c> et l'appelant depile ; le troisieme n'est jamais lu. Transcrite avec les deux
    /// qu'elle lit.
    /// </para>
    /// </summary>
    private static void AcquireHolyTarget_00447CB0(byte* e, int mode)
    {
        *(byte**)(e + 0x78) = *(byte**)(e + 0x74);

        if (mode == 0)
        {
            *(byte**)(e + 0x78) = FindTargetAround_00474BA0(e, 0);
        }

        if (mode == 1)
        {
            short* revived = stackalloc short[4];
            byte* slot = Inp.s_SlotFlags_005e93c0;

            for (int member = 0; member < 4; member++, slot += 0x9c)
            {
                uint flags = *(uint*)slot;
                revived[member] = 0;

                if ((flags & 0x40000000) != 0)
                {
                    revived[member] = 1;
                    *(uint*)slot = flags & 0xbfffdfff;
                }
            }

            *(byte**)(e + 0x78) = FindTargetAround_00474BA0(e, 0);

            slot = Inp.s_SlotFlags_005e93c0;

            for (int member = 0; member < 4; member++, slot += 0x9c)
            {
                if (revived[member] == 1) *(uint*)slot |= 0x40002000;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00447a90</c> — <b>l'entree 54 (<c>0x36</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551c30"/></b>, 536 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00447A90.asm</c>.
    ///
    /// <para>
    /// <b>Un sort de pretre qui part en tournant.</b> Il appartient a la famille de
    /// <see cref="AcquireHolyTarget_00447CB0"/> — il l'appelle en mode 0 des sa premiere image, et
    /// il commence par la garde de <see cref="EndSpellOnTerrain_00448040"/>, <b>recopiee dans son
    /// corps</b> plutot qu'appelee : le decor l'eteint.
    /// </para>
    /// <para>
    /// <b>La naissance</b> joue le son <c>0x23</c>, fige l'ancre <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>
    /// sur sa position, prend son cap dans <c>+0x32</c>, <b>ecrase ses trois echelles</b> (le
    /// quart, la moitie, le quart de ce qu'elles valaient), pose la teinte <c>0xfa</c> partout, et
    /// lache <b>onze entites <c>0xa1</c></b> d'un coup — <see cref="EntityHandler_00447D50"/>, les
    /// traineaux qui le suivent. Entre deux, les trois echelles gagnent <c>0x258</c>, la rotation
    /// <c>+0x34</c> avance de <c>0x99</c>, et la teinte perd <c>10</c>/<c>20</c>/<c>20</c> :
    /// les onze enfants naissent donc <b>tous differents</b>, de plus en plus larges, tournes et
    /// sombres. La boucle s'arrete quand la rotation depasse <c>0x1093</c>.
    /// </para>
    /// <para>
    /// <b>La vie</b> est une spirale a plat : le rayon <c>+0x9a</c> part de zero et gagne
    /// <c>+0x5c</c> (30) par image, le cap <c>+0x12</c> ne bouge pas, et <c>X</c>/<c>Z</c> sont
    /// recalcules a chaque image autour de l'ancre. L'altitude est <b>collee au sol</b> :
    /// <see cref="FinalizeEntityPlacement2"/> resout le terrain, et tant que le sol
    /// (<c>+0x60</c>) reste a moins de <c>0x32000</c> de l'entite, <c>Y</c> y est pose. Sortir de
    /// cette fourchette, ou toucher le decor (<c>+0x90</c> sur le masque <c>0x8802</c>), passe
    /// l'entite en phase 2.
    /// </para>
    /// <para>
    /// <b>Le coup</b> est porte a chaque image, en phase 1 comme au moment du passage en phase 2 :
    /// <see cref="SweepPartyForMeleeHit_00519C30"/> avec la sonde de pile
    /// <c>{0, -50, 50, 80, 0, 100}</c>, puis <see cref="ResolveMeleeHit_00539B80"/> au nom du
    /// <b>lanceur</b> (<c>+0x6c</c>) et non du sort lui-meme. Soixante images au plus.
    /// </para>
    /// <para>
    /// <b>Phase 2</b> n'est qu'un compte a rebours de cinq images avant l'extinction — le temps
    /// que les onze traineaux finissent leur course.
    /// </para>
    /// </summary>
    private static void EntityHandler_00447A90(byte* e)
    {
        // La sonde de heurt, batie sur la pile a chaque image : largeur, bas, haut, portee.
        short* probe = stackalloc short[6];
        probe[0] = 0;
        probe[1] = unchecked((short)0xffce);
        probe[2] = 0x32;
        probe[3] = 0x50;
        probe[4] = 0;
        probe[5] = 0x64;

        // Ghidra: la garde de EndSpellOnTerrain_00448040, recopiee et non appelee.
        if ((*(uint*)e & 0x40000000) != 0) *(uint*)e &= 0x7fffffff;

        if (*(short*)(e + 0x10) == 0)
        {
            AcquireHolyTarget_00447CB0(e, 0);

            // Ghidra: `cmp word [esi+10h],0 ; jne LAB_00447ba5` — le meme test, refait apres la
            // recherche de cible. AcquireHolyTarget_00447CB0 n'ecrit pas +0x10 : il ne peut pas
            // echouer. Transcrit parce qu'il est ecrit.
            if (*(short*)(e + 0x10) == 0)
            {
                Aud.PlayPositionalSound(0x23, 0x640, 0x1800, (int*)(e + 0x1c));

                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x54) = *(int*)(e + 0x20);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                // `sar` nus : le quart, la moitie, le quart. Le sort nait donc aplati.
                *(short*)(e + 0x28) = (short)(*(short*)(e + 0x28) >> 2);
                *(short*)(e + 0x2a) = (short)(*(short*)(e + 0x2a) >> 1);
                *(short*)(e + 0x2c) = (short)(*(short*)(e + 0x2c) >> 2);

                *(short*)(e + 0x12) = *(short*)(e + 0x32);

                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x16) = 0;
                *(int*)(e + 0x5c) = 0x1e;
                *(short*)(e + 0x9a) = 0;

                e[0x38] = 0xfa;
                e[0x39] = 0xfa;
                e[0x3a] = 0xfa;

                // Ghidra: LAB_00447b67 — onze tours, la borne etant la rotation et non un compte :
                // 0x0a99 par pas de 0x99 jusqu'a 0x1093 inclus.
                for (int spin = 0x0a99; ; spin += 0x99)
                {
                    SpawnCastEntity_0047AAE0(e, 0xa1, 0, -1);

                    *(short*)(e + 0x28) += 0x258;
                    *(short*)(e + 0x2a) += 0x258;
                    *(short*)(e + 0x2c) += 0x258;

                    *(short*)(e + 0x34) = (short)spin;

                    BrightenEntityTint_00404DE0(e, -10, -20, -20);

                    if ((short)(spin + 0x99) > 0x1093) break;
                }
            }
        }

        // Ghidra: LAB_00447ba5 — la premiere image passe ici aussi.
        if (*(short*)(e + 0x10) == 1)
        {
            short heading = *(short*)(e + 0x12);
            short radius = *(short*)(e + 0x9a);

            *(int*)(e + 0x1c) =
                (int)Winsys3d.GetSinTableEntry(heading) * radius + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(heading) * radius + *(int*)(e + 0x58);

            // `mov dx,[esi+5Ch] ; add dx,di` : l'increment est lu sur seize bits du dword.
            *(short*)(e + 0x9a) = (short)(*(short*)(e + 0x5c) + radius);

            FinalizeEntityPlacement2((uint*)e);       // Ghidra: FUN_00533940

            bool settled = false;

            if ((*(uint*)(e + 0x90) & 0x8802) == 0)
            {
                int ground = *(int*)(e + 0x60);
                int height = *(int*)(e + 0x20);

                if (height >= ground - 0x32000 && height <= ground + 0x32000)
                {
                    // Il rampe : l'altitude est celle du sol, pas la sienne.
                    *(int*)(e + 0x20) = ground;
                    settled = true;
                }
            }

            // Ghidra: LAB_00447c30 — le decor, ou une marche trop haute : l'entite s'arrete. Le
            // balayage qui suit a lieu quand meme, le saut tombant juste apres.
            if (!settled)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 5;
            }

            // Ghidra: LAB_00447c3c — le coup est porte au nom du LANCEUR, pas du sort.
            byte* victim =
                SweepPartyForMeleeHit_00519C30(*(byte**)(e + 0x6c), (int*)(e + 0x1c), probe, 0);

            if (victim != null)
            {
                ResolveMeleeHit_00539B80(*(uint**)(e + 0x6c), (uint*)victim);

                *(uint*)victim |= 0x40;
                *(short*)(victim + 0x96) = 0x10;
            }

            // Ghidra: LAB_00447c72 — soixante images au plus.
            *(short*)(e + 0x16) += 1;
            if (*(short*)(e + 0x16) >= 0x3c)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 5;
            }
        }

        // Ghidra: LAB_00447c89 — cinq images d'attente, puis l'extinction.
        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00447d50 @ 0x00447d50 — <b>l'entree 161 (<c>0xa1</c>) de la table statique
    /// <see cref="s_StaticEntityHandlers_00551f30"/></b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Le traineau que <see cref="EntityHandler_00447A90"/> lache par onze</b> a sa naissance.
    ///
    /// <para>
    /// <b>Il ne se deplace pas tout seul</b> : a chaque image il se recopie la position de son pere
    /// (<c>+0x6c</c>), et si le pere a disparu il prend l'entite de service
    /// <see cref="Resources.s_EntityList_005de558"/> — dont la position est zero. <b>Il meurt avec
    /// son pere</b> : des que le <c>+0x10</c> de celui-ci vaut 2, la phase d'attente du sort, le
    /// traineau s'eteint.
    /// </para>
    /// <para>
    /// <b>Sa naissance</b> lui prend le cap du pere (<c>+0x32</c>) et <b>le fait tourner de
    /// <c>0x400</c> dans le pere</b> — un quart de tour par enfant : les onze naissent donc
    /// repartis sur plusieurs tours complets. Il se donne le modele <c>0x10</c>, la couche de
    /// dessin <c>0x0d</c>, et fige l'ancre <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>.
    /// </para>
    /// <para>
    /// <b>Sa vie tient en une ligne</b> : <c>+0x34</c> — la rotation que le pere lui a posee a la
    /// naissance, mise de cote dans <c>+0x14</c> — est retiree chaque image et <b>retirage a
    /// <c>+/- 75</c> pres</b> (<c>rand % 150 - 75</c>). Il vibre sur place autour de son angle,
    /// il ne tourne pas.
    /// </para>
    /// <para>
    /// <b>Ghidra lui declare une valeur de retour</b> (<c>undefined4 *</c>) : c'est le contenu
    /// residuel du registre, un <c>CONCAT22</c> de deux champs sans rapport. Le repartiteur
    /// l'appelle comme tous les autres, sans rien lire — transcrite <c>void</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00447D50(byte* e)
    {
        byte* parent = *(byte**)(e + 0x6c);
        if (parent == null) parent = Res.s_EntityList_005de558;   // Ghidra: DAT_005de558

        *(int*)(e + 0x1c) = *(int*)(parent + 0x1c);
        *(int*)(e + 0x20) = *(int*)(parent + 0x20);
        *(int*)(e + 0x24) = *(int*)(parent + 0x24);

        // Le pere est passe en phase d'attente : le traineau ne lui survit pas.
        if (*(short*)(parent + 0x10) == 2) *(uint*)e &= 0x7fffffff;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = *(short*)(e + 0x32);
            *(short*)(e + 0x32) += 0x400;

            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);

            *(short*)(e + 0x10) = 1;
            e[4] = 0x10;
            e[0x0a] = 0x0d;
            e[0x0b] = 0;

            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            // La rotation que le pere a posee, mise de cote : +0x34 est reecrit a chaque image.
            *(short*)(e + 0x14) = *(short*)(e + 0x34);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x34) = *(short*)(e + 0x14);
            *(short*)(e + 0x34) += (short)(CrtRand() % 0x96 - 0x4b);   // Ghidra: FUN_00547140
        }
    }

    // =====================================================================
    // PARDON — le trentieme sort, ecole 1 indice 0, sous-type 0x1e
    //
    //   0x0044F060   LE SORT. Sous-type 0x1e de 0x00551c30, 558 octets.
    //   0x0044F290   LE TRAIT, sous-type 0x9e de 0x00551f30, 874 octets. Un seul.
    //                DECOMPILE dans Blaze.c.
    //
    // LE PREMIER SORT QUI CHOISIT SA VICTIME SUR UNE FICHE ET NON SUR UNE DISTANCE SEULE. Il balaie
    // les 0x20 entites de scene, resout pour chacune ses trois blocs de description par
    // ResolveEntityBlocks, et ne retient que celles dont le bloc de statistiques porte 0x3000000 en
    // +0x14. Les autres conditions sont ordinaires — vivante, pas mourante, pas deja prise — mais
    // celle-la ne l'est pas : c'est un sort qui ne mord que sur une espece.
    //
    // ET IL MESURE EN TROIS DIMENSIONS, DEUX FOIS. La candidate doit etre a moins de 0x5dc du
    // LANCEUR, et c'est la plus proche du SORT qui l'emporte — deux distances differentes, toutes
    // deux avec la composante verticale, la ou tout le reste du depot mesure a plat.
    //
    // 0 PM, jamais apprenable (la colonne « appr. » du releve vaut 0) : Pardon n'est pas une fiche
    // qu'un pretre gagne en montant de niveau.
    //
    // SES TROIS PETITS-ENFANTS SONT ECRITS DEPUIS LE 2026-09-01. Le trait 0x9e fait naitre trois
    // sous-types que le sort ne possede pas — 0x9f (0x0044F600, LA COLONNE, ci-dessous) et 0x93
    // (0x0044C110, venu avec une autre fiche de l'ecole), tous deux par la table 0x00551f30, et
    // 0x96 (0x0044F750, LE GRAIN, ci-dessous) par la table 0x00552230.
    //
    // ILS SONT LE POINT AVEUGLE DU RECENSEMENT DES SORTS. SortsAtteignables.md ne resout que le
    // sous-type +0x10 d'une fiche, donc son ENFANT DIRECT ; il ne remonte pas la chaine. Une fiche
    // pouvait s'y lire « implementee » alors que la lancer plantait a la premiere image du trait.
    // C'est ce qui est arrive ici et pour Ange : les trois derniers trous de la magie etaient des
    // petits-enfants, et il a fallu deplier tous les engendrements du code ecrit pour les voir.
    //
    // (Correction du 2026-08-31 : cette note attribuait le 0x96 a 0x0044a320. C'etait un index
    // compte a la main sur la table imprimee. Le creneau 0x96 de 0x00552230 vaut 0x0044F750 ;
    // 0x0044A320 est le creneau 0x8e, l'eclat de Marteau. Se relever une entree de table par le
    // relevé, jamais en comptant les lignes.)
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044f060 @ 0x0044f060 — <b>Pardon</b> : le sous-type <c>0x1e</c>, creneau 30 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et <b>la premiere fiche de l'ecole 1</b>. 558
    /// octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne fait que designer.</b> Trois etats dans <c>+0x10</c> : 0 la mise en place (le decor
    /// vire au violet, un son), 1 la recherche, 2 l'attente, <c>0x63</c> la fin. Le coup, lui,
    /// appartient au trait <c>0x9e</c> qu'il engendre sur la victime trouvee.
    /// </para>
    /// <para>
    /// <b>Le critere de selection est une fiche, pas une portee.</b> Pour chacune des <c>0x20</c>
    /// entites de scene il appelle <see cref="ResolveEntityBlocks"/> et exige
    /// <c>statistiques[0x14] &amp; 0x3000000</c>. Puis deux distances <b>en trois dimensions</b> :
    /// moins de <c>0x5dc</c> depuis le lanceur pour etre eligible, et la plus courte depuis le sort
    /// pour etre choisie.
    /// </para>
    /// <para>
    /// <b>La victime est rattachee au pere du sort, pas au sort.</b> Il ecrit
    /// <c>victime[0x74] = sort[0x6c]</c> — l'entite qui a fait naitre le sort — tout en gardant la
    /// victime dans son propre <c>+0x78</c>.
    /// </para>
    /// <para>
    /// <b>LB-019, une ecriture morte.</b> La branche « aucune cible » pose <c>+0x10 = 0x63</c> puis
    /// tombe dans l'instruction suivante, qui pose <c>+0x10 = 2</c> : l'etat de fin est efface une
    /// instruction plus tard. Le <c>+0x16 = 0x46</c> de la meme branche, lui, survit — ce qui
    /// ramene l'attente de 90 a 20 images au lieu de la supprimer. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickForgive_0044F060(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_0044f089
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            // Le decor vire au violet, et le mot de mode passe a 2 — propre a cette ecole.
            *(ushort*)Mem.Arena(0x005e9160) = 0xa0;
            *(ushort*)Mem.Arena(0x005e9162) = 0xa0;
            *(ushort*)Mem.Arena(0x005e9164) = 0xb4;
            *(ushort*)Mem.Arena(0x005e9166) = 2;

            Aud.PlaySoundEffect(0x0a, 0x653, 0x1000, 0x1000);
        }

        // Ghidra: LAB_0044f0d8 — la recherche, une seule image.
        if (*(short*)(e + 0x10) == 1)
        {
            byte* caster = *(byte**)(e + 0x74);
            if (caster == null) caster = Res.s_EntityList_005de558;

            int nearest = 0x2710;
            int chosen = -1;

            byte* candidate = Mem.Entity(0x005ecaa0);

            for (int index = 0; index < 0x20; index++, candidate += 0x9c)
            {
                ResolveEntityBlocks(candidate, out byte* stats, out byte* species, out byte* model);

                uint flags = *(uint*)candidate;

                if ((flags & 0x80000000) == 0) continue;
                if ((flags & 0x40002000) != 0) continue;
                if (*(int*)(candidate + 0x74) != 0) continue;

                // LE CRITERE D'ESPECE : deux bits du bloc de statistiques, et rien d'autre.
                if ((*(uint*)(stats + 0x14) & 0x3000000) == 0) continue;

                // Premiere distance : de la victime au LANCEUR, en trois dimensions.
                int ax = (*(int*)(candidate + 0x1c) - *(int*)(caster + 0x1c)) >> 0xc;
                int ay = (*(int*)(candidate + 0x20) - *(int*)(caster + 0x20)) >> 0xc;
                int az = (*(int*)(candidate + 0x24) - *(int*)(caster + 0x24)) >> 0xc;

                if ((int)Winsys3d.Ordinal_442((uint)(ax * ax + ay * ay + az * az)) >= 0x5dc)
                {
                    continue;
                }

                // Seconde distance : du SORT a la victime, chaque terme decale AVANT la
                // soustraction — l'inverse de la premiere.
                int bx = (*(int*)(e + 0x1c) >> 0xc) - (*(int*)(candidate + 0x1c) >> 0xc);
                int by = (*(int*)(e + 0x20) >> 0xc) - (*(int*)(candidate + 0x20) >> 0xc);
                int bz = (*(int*)(e + 0x24) >> 0xc) - (*(int*)(candidate + 0x24) >> 0xc);

                int reach = (int)Winsys3d.Ordinal_442((uint)(bx * bx + by * by + bz * bz));

                if (nearest > reach)
                {
                    nearest = reach;
                    chosen = index;
                }
            }

            if ((short)chosen != -1)
            {
                byte* victim = Mem.Entity(0x005ecaa0) + (short)chosen * 0x9c;

                *(byte**)(e + 0x78) = victim;
                *(byte**)(victim + 0x74) = *(byte**)(e + 0x6c);   // le PERE du sort, pas le sort

                SpawnCastEntity_0047AAE0(e, 0x9e, 0, -1);
            }
            else
            {
                // Ghidra: LAB_0044f23e — LB-019 : ce 0x63 est efface deux instructions plus loin.
                *(short*)(e + 0x10) = 0x63;
                *(short*)(e + 0x16) = 0x46;
            }

            // Ghidra: LAB_0044f24a — les DEUX chemins passent ici.
            *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_0044f250
        if (*(short*)(e + 0x10) == 2 && *(short*)(e + 0x16) >= 0x5a) *(short*)(e + 0x10) = 0x63;

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x10) == 0x63 && *(short*)(e + 0x16) >= 0x5a)
        {
            EndSpellAndResetDecor_00448020(e);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044f290 @ 0x0044f290 — <b>le trait de Pardon</b> : le sous-type <c>0x9e</c>,
    /// creneau 158 de <see cref="s_StaticEntityHandlers_00551f30"/>. 874 octets, <b>decompile dans
    /// <c>Blaze.c</c></b>. Un seul, pose sur la victime.
    ///
    /// <para>
    /// <b>Il vit sur sa victime.</b> A chaque image il se recopie la position de celle-ci, cinq
    /// unites plus haut, et lui pose le bit <c>0x1000</c> — l'immobilisation. Des qu'elle porte
    /// <c>0x40000000</c>, ou que lui-meme porte <c>0x2000000</c>, il cesse de la suivre et se marque
    /// pour la fin.
    /// </para>
    /// <para>
    /// <b>Sa taille est celle de la victime</b> : <c>hauteur &#215; 30</c>, bornee entre
    /// <c>0x1000</c> et 30 000. Trois phases dans <c>+0x10</c> — 1 l'ouverture (quatorze images, le
    /// cap et les deux echelles rejoignent leur consigne par quarts), 2 l'execution, 3 la fin.
    /// </para>
    /// <para>
    /// <b>La phase 2 est une partition de quatre-vingts images.</b> Elle joue cinq sons a des
    /// instants precis, engendre deux fois le sous-type <c>0x9f</c> (images 0 et 4, a des hauteurs
    /// differentes), seme un <c>0x96</c> par image de la huitieme a la trentieme, allume le voile
    /// plein cadre <c>DAT_005de024</c> en vert-blanc a la vingtieme avec deux <c>0x93</c>, et
    /// <b>porte le coup a la vingt-deuxieme</b> — un seul, par
    /// <see cref="ResolveSpellHit_0053A2B0"/>, en eteignant le voile dans la meme image. Passe la
    /// soixantieme, il se resorbe et relache la victime ; a la quatre-vingtieme il passe en phase 3.
    /// </para>
    /// </summary>
    private static void TickForgiveBolt_0044F290(byte* record)
    {
        byte* e = record;

        byte* victim = *(byte**)(e + 0x78);
        if (victim == null) victim = Res.s_EntityList_005de558;

        if ((*(uint*)victim & 0x40000000) == 0 && (*(uint*)e & 0x2000000) == 0)
        {
            *(int*)(e + 0x1c) = *(int*)(victim + 0x1c);

            int ground = *(int*)(victim + 0x60);
            *(int*)(e + 0x20) = ground + 0x5000;
            *(int*)(e + 0x60) = ground + 0x5000;

            *(int*)(e + 0x24) = *(int*)(victim + 0x24);
            *(uint*)victim |= 0x1000;                  // elle est clouee sur place
        }
        else
        {
            *(uint*)e |= 0x2000000;
        }

        // Sa taille suit celle de la victime, bornee des deux cotes.
        int span = StatusBubbleHeight_00538BC0((uint*)victim) * 0x1e;
        if (span < 0x1000) span = 0x1000;
        if (span > 30000) span = 30000;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x2b;
            e[0x0a] = 6;
            e[0x0b] = 0;

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0x1000;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x32) = 0;

            // Une victime deja disparue le tue avant meme sa premiere image.
            if ((*(uint*)victim & 0x80000000) == 0 || (*(uint*)victim & 0x40000000) != 0)
            {
                *(uint*)e &= 0x7fffffff;
                *(uint*)victim &= 0xffffefff;
            }
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) += (short)((0x800 - *(short*)(e + 0x32)) >> 2);
            *(short*)(e + 0x28) += (short)((span - *(short*)(e + 0x28)) >> 2);
            *(short*)(e + 0x2c) += (short)((span - *(short*)(e + 0x2c)) >> 2);

            if (*(short*)(e + 0x16) >= 0x0e)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x16) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short beat = *(short*)(e + 0x16);

            if (beat == 0x00) Aud.PlaySoundEffect(0x0f, 0x100, 0x1000, 0x1000);
            if (beat == 0x01) Aud.PlaySoundEffect(0x0f, 0x155, 0x1000, 0x1000);
            if (beat == 0x14) Aud.PlaySoundEffect(0x0b, 0x800, 0x1000, 0x1000);
            if (beat == 0x15) Aud.PlaySoundEffect(0x0c, 0x653, 0x1000, 0x1000);
            if (beat == 0x16) Aud.PlaySoundEffect(0x1c, 0x653, 0x1000, 0x1000);

            if (beat == 0)
            {
                *(ushort*)(e + 0x9a) = 6;
                *(short*)(e + 0x14) = 0x1000;
                *(int*)(e + 0x5c) = 0x5dc;
                *(int*)(e + 0x44) = 30000;
                *(int*)(e + 0x48) = 0x28;
                SpawnCastEntity_0047AAE0(e, 0x9f, 0, -1);
            }

            if (beat == 4)
            {
                *(ushort*)(e + 0x9a) = 6;
                *(short*)(e + 0x14) = 0x0f3c;
                *(int*)(e + 0x5c) = 0x5dc;
                *(int*)(e + 0x44) = 30000;
                *(int*)(e + 0x48) = 0x28;
                SpawnCastEntity_0047AAE0(e, 0x9f, 0, -1);
            }

            if (beat > 7 && beat < 0x1f)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x96, 0, -1);
            }

            if (beat == 0x14)
            {
                // Le voile plein cadre s'allume en vert-blanc — R, V, B et l'octet d'activation
                // tiennent dans le meme mot.
                Mem.s_Unknown_005de024 = 0x01b4c896;

                SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);
                SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);
            }

            if (beat == 0x16)
            {
                // LE COUP, une seule fois, et le voile s'eteint dans la meme image.
                ResolveSpellHit_0053A2B0(
                    *(uint**)(e + 0x74), *(uint**)(e + 0x78), e[7], e[6], 0);

                byte* struck = *(byte**)(e + 0x78);
                *(ushort*)(struck + 0x96) = 0x10;      // le clignotement de degat
                *(uint*)struck |= 0x40;

                Mem.s_Unknown_005de024 = 0x01000000;
            }

            if (beat > 0x3c)
            {
                // La resorption : `(-taille) >> 3` et non `-(taille >> 3)`, comme ecrit.
                *(short*)(e + 0x28) +=
                    (short)((-(int)*(short*)(e + 0x28)) >> 3);
                *(short*)(e + 0x2c) +=
                    (short)((-(int)*(short*)(e + 0x2c)) >> 3);

                *(uint*)(*(byte**)(e + 0x78) + 0x74) = 0;
            }

            if (beat == 0x50) *(short*)(e + 0x10) = 3;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(uint*)e &= 0x7fffffff;
            *(uint*)victim &= 0xffffefff;              // la victime est relachee
        }

        *(short*)(e + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0044f600 @ 0x0044f600 — <b>la colonne de Pardon</b> : le sous-type <c>0x9f</c>,
    /// creneau 159 de <see cref="s_StaticEntityHandlers_00551f30"/>. 327 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; le releve est garde sous <c>Legacy/Raw/Blaze/FUN_0044F600.asm</c>.
    ///
    /// <para>
    /// <b>Elle est posee AU SOL sous le trait</b>, et elle l'y suit : a chaque image elle relit la
    /// position du trait (<c>+0x6c</c>) mais prend sa <b>hauteur de sol</b> (<c>+0x60</c>) et non sa
    /// hauteur a lui. Des que le trait porte <c>0x50000000</c>, ou qu'elle-meme porte deja
    /// <c>0x2000000</c>, elle cesse de le suivre et se marque <c>0x2000000</c> : elle reste ou elle
    /// est et finit sur place.
    /// </para>
    /// <para>
    /// <b>Sa largeur vient de la victime, sa hauteur monte toute seule.</b> Largeur et profondeur
    /// (<c>+0x28</c> et <c>+0x2c</c>) sont fixees une fois pour toutes a
    /// <c>trente fois la stature de la victime, bornee a [0x1000, 0x7530], plus +0x14 moins
    /// 0x1000</c> ; la hauteur (<c>+0x2a</c>) part de zero et avance d'un <b>huitieme de ce qui la
    /// separe de <c>0x7fff</c></b> a chaque image, plafonnee par le mot <c>+0x44</c>. C'est une
    /// colonne qui jaillit et s'arrete net.
    /// </para>
    /// <para>
    /// <b>Le compte a rebours <c>+0x48</c> retourne sa teinte.</b> Tant qu'il reste positif elle
    /// s'eclaircit de dix par image, plafonnee a <c>0xb4</c> ; des qu'il passe sous zero elle
    /// s'assombrit du meme pas et disparait quand sa teinte atteint <b>exactement</b> zero. Le zero
    /// exact tombe toujours parce que <see cref="BrightenEntityTint_00404DE0"/> borne le resultat a
    /// <c>0..0xff</c> : la descente s'arrete sur zero quoi qu'il arrive, meme si le plafond
    /// <c>0xb4</c> n'avait pas ete atteint.
    /// </para>
    /// <para>
    /// <b>Le plafond est lu sur un mot, ecrit sur un demi-mot.</b> Le binaire compare <c>+0x2a</c>
    /// sign-etendu au <b>dword</b> <c>+0x44</c>, puis recopie le <b>word</b> <c>+0x44</c> dans
    /// <c>+0x2a</c>. C'est ecrit ainsi.
    /// </para>
    /// <para>
    /// <b>Le trait ni la colonne ne testent leur porteur</b> : un <c>+0x6c</c> ou un <c>+0x78</c>
    /// nul retombe sur <see cref="Resources.s_EntityList_005de558"/>, l'entite de service, comme
    /// partout ailleurs dans le depot.
    /// </para>
    /// </summary>
    private static void TickForgivePillar_0044F600(byte* record)
    {
        byte* e = record;

        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;   // Ghidra: DAT_005de558

        if ((*(uint*)carrier & 0x50000000) == 0 && (*(uint*)e & 0x2000000) == 0)
        {
            *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
            *(int*)(e + 0x20) = *(int*)(carrier + 0x60);   // AU SOL, pas a la hauteur du trait
            *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
        }
        else
        {
            // Ghidra: LAB_0044f638 — elle se detache et finit sur place.
            *(uint*)e |= 0x2000000;
        }

        // Ghidra: LAB_0044f63e — la largeur vient de la stature de la victime.
        byte* victim = *(byte**)(e + 0x78);
        if (victim == null) victim = Res.s_EntityList_005de558;

        int span = StatusBubbleHeight_00538BC0((uint*)victim) * 30;

        if (span < 0x1000) span = 0x1000;
        if (span > 0x7530) span = 0x7530;

        span += *(short*)(e + 0x14) - 0x1000;

        if (*(short*)(e + 0x10) == 0)
        {
            byte phase = e[0x9a];

            *(ushort*)(e + 0x10) = 1;
            e[0x04] = 5;
            e[0x40] = 0;
            e[0x0a] = phase;
            e[0x0b] = 0;

            *(ushort*)(e + 0x28) = (ushort)span;
            *(ushort*)(e + 0x2c) = (ushort)span;
            *(ushort*)(e + 0x2a) = 0;

            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
        }

        // Ghidra: LAB_0044f6b5
        if (*(short*)(e + 0x10) != 1) return;

        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

        // Ghidra: LAB_0044f6d2 — LA MONTEE, un huitieme de ce qui reste jusqu'a 0x7fff.
        short tall = *(short*)(e + 0x2a);
        tall = (short)(tall + ((0x7fff - tall) >> 3));
        *(short*)(e + 0x2a) = tall;

        // Le plafond est lu sur le dword +0x44 et recopie depuis son word. Tel quel.
        if (tall > *(int*)(e + 0x44)) *(short*)(e + 0x2a) = *(short*)(e + 0x44);

        // Ghidra: LAB_0044f6fb — le compte a rebours retourne la teinte.
        *(int*)(e + 0x48) -= 1;

        if (*(int*)(e + 0x48) < 0)
        {
            BrightenEntityTint_00404DE0(e, -10, -10, -10);
            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
        else
        {
            // Ghidra: LAB_0044f723
            BrightenEntityTint_00404DE0(e, 10, 10, 10);

            if (e[0x38] > 0xb4)
            {
                e[0x38] = 0xb4;
                e[0x39] = 0xb4;
                e[0x3a] = 0xb4;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044f750 @ 0x0044f750 — <b>le grain de Pardon</b> : le sous-type <c>0x96</c>,
    /// creneau 150 de <see cref="s_StaticEntityHandlers_00552230"/>. <b>Decompile dans
    /// <c>Blaze.c</c></b> ; les largeurs d'ecriture viennent du desassemblage, Ghidra ayant rendu
    /// plusieurs <c>CONCAT22</c> qui ne sont que du bruit — le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044F750.asm</c> pour cette raison.
    ///
    /// <para>
    /// <b>Il tourne autour du trait, en montant.</b> Son centre — range dans <c>+0x50</c>..
    /// <c>+0x58</c>, la ou vit d'ordinaire une vitesse — est <b>relu sur le trait a chaque
    /// image</b>, donc il le suit ; son cap <c>+0x14</c> et son rayon <c>+0x16</c> sont tires une
    /// fois pour toutes, et sa montee <c>+0x12</c> gagne deux unites par image, si bien qu'il
    /// s'eleve de plus en plus vite.
    /// </para>
    /// <para>
    /// <b>Il s'etire en montant</b> : sa hauteur <c>+0x2a</c> gagne <c>+0x9a</c> par image —
    /// <c>rand() % 200 + 500</c>, plafonnee a <c>0x7530</c> — pendant que sa largeur <c>+0x28</c>
    /// perd le seizieme du meme nombre. Un grain qui devient un fil.
    /// </para>
    /// <para>
    /// <b>Le rayon depend de la victime</b> : <c>rand() % (trente fois sa stature, bornee a
    /// [0x1000, 0x7530], divise par cent)</c>. La division par cent est le
    /// <c>imul 0x51eb851f ; sar 5</c> du binaire, que Ghidra a laisse fuir dans une variable
    /// intermediaire.
    /// </para>
    /// <para>
    /// <b>Il ecrit dans la table de descripteurs, et sans retour.</b> Sa premiere image pose
    /// <c>2</c> dans l'octet <c>+4</c> du <b>cinquieme</b> enregistrement de
    /// <see cref="s_Descriptors_00551528"/> (<c>0x0055154c</c>), qui vaut <c>1</c> au chargement.
    /// Rien ne le remet jamais a <c>1</c> : c'est un verrou a sens unique sur une table
    /// <b>partagee</b>, que d'autres entites atteignent par leur <c>+0x9a</c>. C'est ce que fait
    /// l'original, et la copie native de la table le rend reproductible ici.
    /// </para>
    /// <para>
    /// <b>Ghidra lui donne un retour</b> (<c>uint</c>) que personne ne lit : les gestionnaires
    /// d'entite sont appeles par une table de sauts qui jette le resultat. Ecrit <c>void</c>.
    /// </para>
    /// </summary>
    private static void TickForgiveMote_0044F750(byte* record)
    {
        byte* e = record;

        // LE CENTRE, relu sur le trait a chaque image : le grain le suit.
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) carrier = Res.s_EntityList_005de558;

        *(int*)(e + 0x50) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x54) = *(int*)(carrier + 0x20);
        *(int*)(e + 0x58) = *(int*)(carrier + 0x24);

        byte* victim = *(byte**)(e + 0x78);
        if (victim == null) victim = Res.s_EntityList_005de558;

        int span = StatusBubbleHeight_00538BC0((uint*)victim) * 30;

        if (span < 0x1000) span = 0x1000;
        if (span > 0x7530) span = 0x7530;

        int reach = span / 100;

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x20;   // Ghidra: &DAT_00551548
            e[0x40] = 0;

            // LE VERROU A SENS UNIQUE sur la table partagee — voir la note.
            Descriptors00551528()[0x24] = 2;                      // Ghidra: DAT_0055154c

            e[0x0a] = 6;
            e[0x0b] = 0;

            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;

            *(ushort*)(e + 0x2a) = 0;
            *(short*)(e + 0x28) = (short)(CrtRand() % 0x800 + 0x800);

            *(short*)(e + 0x9a) = (short)(CrtRand() % 200 + 500);
            *(short*)(e + 0x12) = (short)(CrtRand() % 5 + 5);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x16) = (short)(CrtRand() % reach);

            // Le centre du premier tour est sa propre position ; les images suivantes le
            // reprendront sur le trait.
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            *(ushort*)(e + 0x10) = 1;

            *(int*)(e + 0x5c) = -10 - CrtRand() % 12;
        }

        // Ghidra: LAB_0044f88f
        if (*(short*)(e + 0x10) != 1) return;

        short yaw = *(short*)(e + 0x14);
        short reachNow = *(short*)(e + 0x16);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * reachNow + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * reachNow + *(int*)(e + 0x58);

        // LA MONTEE S'ACCELERE : deux unites de plus a chaque image.
        short lift = *(short*)(e + 0x12);
        *(short*)(e + 0x12) = (short)(lift + 2);

        short stretch = *(short*)(e + 0x9a);

        *(short*)(e + 0x2a) += stretch;
        *(short*)(e + 0x28) -= (short)(stretch >> 4);

        *(int*)(e + 0x20) += lift << 0xc;

        if (*(short*)(e + 0x2a) > 0x7530) *(ushort*)(e + 0x2a) = 0x7530;

        // Ghidra: LAB_0044f907 — la teinte descend du meme pas sur les trois composantes.
        short fade = *(short*)(e + 0x5c);
        BrightenEntityTint_00404DE0(e, fade, fade, fade);

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }


    // =====================================================================
    // SANG PUR — le trente et unieme sort, ecole 1 indice 1, sous-type 0x1f
    //
    //   0x00447DF0   LE SORT. Sous-type 0x1f de 0x00551c30, 487 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_00447DF0.asm.
    //   0x00447FE0   LE SOIN SOUS CONDITION. Soixante et un octets, decompile dans Blaze.c.
    //   0x004364B0   LE GARDE-FOU PARTAGE. Quatre-vingt-onze octets, decompile dans Blaze.c.
    //
    // AUCUN CORPS D'ENFANT NEUF. C'est le premier sort du depot dont les deux sous-types engendres
    // etaient deja ecrits pour d'autres fiches : l'etincelle 0xa9 est celle de la bouffee
    // (EntityHandler_0044FB10) et la bille 0x70 est celle d'Enchant (TickEnchantMote_0047CF30).
    // Sang pur ne coute donc que son propre corps et les deux fonctions de soin.
    //
    // IL SOIGNE A LA PREMIERE IMAGE, PAS A LA DERNIERE. Renfort, son voisin immediat dans le
    // binaire et de meme forme, attend la soixantieme image pour poser son effet ; Sang pur appelle
    // le soin dans son bloc de naissance et passe les quarante images suivantes a n'etre plus
    // qu'un decor. Deux sorts jumeaux par la forme, opposes par le moment.
    //
    // ET IL COLLE A SA CIBLE SANS SE RELEVER. Comme toutes les fiches de l'ecole il recopie chaque
    // image la position de +0x78 dans la sienne — mais SANS le +0x50000 vertical que Renfort et
    // ses voisins ajoutent en +0x20. Sang pur naît donc aux pieds de qui il soigne, la ou les
    // autres naissent a hauteur de poitrine ; la compensation est reportee sur ses billes.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004364b0 @ 0x004364b0 — <b>le garde-fou que tout effet applique franchit
    /// d'abord</b>. Quatre-vingt-onze octets, decompilee dans <c>Blaze.c</c>. Une dizaine
    /// d'enveloppes de la forme « si le garde-fou laisse passer, appliquer » l'appellent
    /// (<c>FUN_00436510</c>, <c>FUN_00436550</c>, <c>FUN_00436590</c>,
    /// <see cref="HealTargetIfEligible_00447FE0"/>, <c>FUN_00448210</c>…), chacune avec son propre
    /// effet derriere.
    ///
    /// <para>
    /// <b>Elle rend 1 pour REFUSER.</b> Quatre motifs, dont un redondant : la cible porte l'un des
    /// bits <c>0x50002000</c> (mourante, immobilisee, prise), elle est deja eteinte
    /// (<c>0x80000000</c> absent), elle est en recul (<c>+0x12</c> non nul), ou l'un des deux —
    /// lanceur ou cible — est eteint. Le troisieme test reprend le bit de vie de la cible que le
    /// premier a deja regarde ; transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Elle n'est pas pure.</b> Avant tout examen elle ecrit <c>lanceur[7] = 7</c> lorsque le
    /// lanceur porte <c>0x400000</c> sans <c>0x800</c> — c'est-a-dire qu'elle force l'ecole de sa
    /// paire famille/indice a 7, la huitieme, celle qui n'est aucune des quatre jouables. Cet effet
    /// de bord a lieu meme quand le garde-fou refuse ensuite.
    /// </para>
    /// </summary>
    private static uint IsEffectPairBlocked_004364B0(uint* caster, uint* victim)
    {
        uint casterFlags = *caster;

        if ((casterFlags & 0x400000) != 0 && (casterFlags & 0x800) == 0)
        {
            ((byte*)caster)[7] = 7;
        }

        uint blocked = 0;
        uint victimFlags = *victim;

        if ((victimFlags & 0x50002000) != 0 || (victimFlags & 0x80000000) == 0) blocked = 1;
        if (*(short*)((byte*)victim + 0x12) != 0) blocked = 1;
        if ((victimFlags & 0x80000000) == 0 || (casterFlags & 0x80000000) == 0) blocked = 1;

        return blocked;
    }

    /// <summary>
    /// Ghidra: FUN_00447fe0 @ 0x00447fe0 — <b>soigner si la cible l'accepte</b>. Soixante et un
    /// octets, decompilee dans <c>Blaze.c</c> : <see cref="IsEffectPairBlocked_004364B0"/>, puis
    /// <see cref="HealTarget_0053CA80"/> si le garde-fou a laisse passer.
    ///
    /// <para>
    /// <b>Le sens de la valeur rendue est inverse d'un chemin a l'autre.</b> Refus du garde-fou :
    /// elle rend <c>true</c> sans rien faire. Passage : elle rend le resultat du soin, qui vaut
    /// <c>1</c> quand <see cref="PrepareCombatPair_00538870"/> a echoue et <c>0</c> quand les
    /// points de vie ont bien ete rendus. <b>Aucun appelant ne la lit</b> — Sang pur jette le
    /// resultat, comme <c>FUN_00448060</c> jette celui de sa jumelle <c>FUN_00448210</c>.
    /// </para>
    /// </summary>
    private static bool HealTargetIfEligible_00447FE0(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return HealTarget_0053CA80(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00447df0 @ 0x00447df0 — <b>Sang pur</b> : le sous-type <c>0x1f</c>, creneau 31 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>deuxieme fiche de l'ecole 1</b>. 487
    /// octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le soin est dans le bloc de naissance.</b> A la premiere image il recopie sa propre paire
    /// famille/indice (<c>+0x06</c>/<c>+0x07</c>) sur le lanceur — c'est ce que
    /// <see cref="SpellSheetOf"/> relira pour trouver le montant en <c>+0x18</c> de la fiche — puis
    /// appelle <see cref="HealTargetIfEligible_00447FE0"/> une fois et une seule. Les quarante
    /// images qui suivent ne sont plus que l'effet visuel.
    /// </para>
    /// <para>
    /// <b>Trois semis differents, sur trois horloges.</b> Quatre etincelles <c>0xa9</c> a la
    /// naissance, deux de plus a chaque image <b>impaire</b>, et une bille <c>0x70</c> par image
    /// jusqu'a la <b>25e</b>. Le champ <c>+0x14</c> arbitre entre les deux especes : pose a
    /// <c>0x50</c> avant chaque etincelle — c'est le rayon dont
    /// <see cref="EntityHandler_0044FB10"/> tire sa spirale — et remis a <b>zero</b> avant chaque
    /// bille, ou il vaut vitesse angulaire et signifie donc « ne tourne pas ». Un seul champ, deux
    /// conventions, reglees a chaque appel.
    /// </para>
    /// <para>
    /// <b>Les deux angles des billes sont tires modulo, ceux d'Enchant ne l'etaient pas.</b>
    /// <c>+0x32</c> vaut <c>rand % 0x400 + 0x400</c> et <c>+0x34</c> vaut <c>rand % 0x1000</c>, la
    /// ou <see cref="TickEnchant_0047CE40"/> ecrit le tirage brut. Le binaire rend le modulo par
    /// l'idiome MSVC en cinq operations (<c>cdq</c>, deux <c>xor</c>/<c>sub</c> autour d'un
    /// <c>and</c>) : c'est <c>x % 2^n</c> a la mode du C, qui garde le signe du dividende.
    /// </para>
    /// <para>
    /// <b>L'ancre des billes est la cible, pas le sort</b> (<c>+0x70 = +0x78</c>), et leur ecart
    /// vertical <c>+0x48</c> est fixe a <c>0x50000</c> — exactement la hauteur que le corps du sort
    /// s'est refusee en recopiant la position de sa cible.
    /// </para>
    /// <para>
    /// <b>Le test de parite est fait sur un octet.</b> <c>test byte [esi+0x16],1</c> ne regarde que
    /// la moitie basse du compteur de seize bits ; celui-ci ne depasse jamais <c>0x29</c>, donc
    /// cela ne change rien. Rendu par l'acces octet, comme le binaire.
    /// </para>
    /// </summary>
    private static void TickPureBlood_00447DF0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_00447e15 — la position est recopiee de la cible A CHAQUE IMAGE, sans le
        // +0x50000 vertical que les autres fiches de l'ecole ajoutent en +0x20.
        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0x50;      // le rayon que l'etincelle 0xa9 tirera
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x9a) = 1;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);

            // La paire famille/indice passe au lanceur : c'est SA fiche que le soin relira. Le
            // binaire relit +0x74 pour chacun des deux octets ; sans consequence.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            HealTargetIfEligible_00447FE0(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete
        }

        // Ghidra: LAB_00447ecc
        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x30, 0x200, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x2f, 0x653, 0x1500, 0x1500);

        // Ghidra: LAB_00447f0b — deux etincelles de plus a chaque image impaire.
        if ((e[0x16] & 1) != 0)
        {
            *(short*)(e + 0x14) = 0x50;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
        }

        // Ghidra: LAB_00447f3d — une bille par image, vingt-cinq images durant.
        if (*(short*)(e + 0x16) < 0x19)
        {
            e[0x04] = 0x3f;                            // le modele, celui des billes d'Enchant
            e[0x0a] = 9;
            e[0x0b] = 0;

            *(byte**)(e + 0x70) = *(byte**)(e + 0x78); // l'ancre : la cible, pas le sort
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0x50000;               // l'ecart vertical, du cote de la bille
            *(int*)(e + 0x4c) = 0;

            *(short*)(e + 0x32) = (short)(CrtRand() % 0x400 + 0x400);   // Ghidra: FUN_00547140
            *(short*)(e + 0x34) = (short)(CrtRand() % 0x1000);          // Ghidra: FUN_00547140

            *(short*)(e + 0x14) = 0;                   // ici le champ vaut vitesse angulaire
            *(short*)(e + 0x12) = 0xc8;                // le rayon de depart
            *(short*)(e + 0x28) = 0x1fff;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0x100;

            SpawnCastEntity_0047AAE0(e, 0x70, 0, -1);
        }

        // Ghidra: LAB_00447fbd
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 0x28) EndSpellAndResetDecor_00448020(e);
    }


    // =====================================================================
    // RENFORT — le trente-deuxieme sort, ecole 1 indice 2, sous-type 0x20
    //
    //   0x00448060   LE SORT. Sous-type 0x20 de 0x00551c30, 425 octets. DECOMPILE dans Blaze.c.
    //   0x00448210   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //   0x00448250   L'ECLAT, sous-type 0xa0 de 0x00551f30, 168 octets. Un seul, a la 30e image.
    //                DECOMPILE dans Blaze.c.
    //   0x0044FE00   L'ETINCELLE, sous-type 0xa7 de 0x00552230, 315 octets. Huit, par paires.
    //                ABSENTE de Blaze.c ; relevee sous Legacy/Raw/Blaze/FUN_0044FE00.asm.
    //
    // TROIS DES QUATRE CORPS SONT DECOMPILES. C'est le premier sort de l'ecole 1 dont le
    // desassembleur ne sert qu'a un seul enfant.
    //
    // ET IL EN OUVRE UN DEUXIEME. Gnoukoule (0x00448300, ecole 1 indice 3) est le MEME corps a
    // quatre valeurs pres : meme trio de tete, meme paire d'etincelles aux images 1, 3, 6 et 9,
    // meme eclat a la 30e, meme fin a la 60e. Il ne differe que par le +0x9a pose a 2 au lieu de 6,
    // deux sons, et l'effet applique a la fin. Les deux enfants ecrits ici sont donc les siens
    // aussi : il ne lui reste que son propre corps et son enveloppe d'effet.
    //
    // LES ETINCELLES NAISSENT PAR PAIRES OPPOSEES. Le sort pose +0x48 — l'angle de depart — a 0
    // puis a 0x800 entre les deux appels, soit un demi-tour : chaque bouffee est deux etincelles
    // diametralement opposees sur le meme cercle. Quatre bouffees, huit etincelles.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00448210 @ 0x00448210 — <b>poser l'effet 17 si la cible l'accepte</b>. Soixante
    /// et un octets, decompilee dans <c>Blaze.c</c>. Jumelle exacte de
    /// <see cref="HealTargetIfEligible_00447FE0"/> : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, un autre effet derriere — ici
    /// <see cref="ApplyStatusEffect_0053BBD0"/>, l'entree 17 de la table des 48, celle qui hausse
    /// l'attaque.
    ///
    /// <para><b>Aucun appelant ne lit ce qu'elle rend</b>, pas plus ici que chez sa jumelle.</para>
    /// </summary>
    private static bool ApplyStatusEffectIfEligible_00448210(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ApplyStatusEffect_0053BBD0(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044fe00 @ 0x0044fe00 — <b>l'etincelle des benedictions du Pretre</b> : le
    /// sous-type <c>0xa7</c>, creneau 167 de <see cref="s_StaticEntityHandlers_00552230"/>. 315
    /// octets, <b>absente de <c>Blaze.c</c></b>. <see cref="TickReinforce_00448060"/> et Gnoukoule
    /// en sement huit chacun, par paires.
    ///
    /// <para>
    /// <b>Elle tombe en tournant.</b> Le centre de son orbite <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>
    /// est recopie de la cible a chaque image, releve de <c>0x3c000</c> ; sa hauteur <c>+0x20</c>
    /// part <c>0xfa000</c> au-dessus de ce centre et descend de <c>0xa000</c> par image jusqu'a s'y
    /// poser — <b>vingt-cinq images de chute</b>. Pendant ce temps sa position horizontale n'est
    /// jamais integree : elle vaut <c>centre + sin/cos(+0x48) &#215; +0x44</c>, l'angle avancant de
    /// <c>+0x5c</c> par image et se repliant sur <c>[0, 0x1000[</c>. Rayon et pas viennent du sort,
    /// qui les pose a <c>0x50</c> et <c>0x96</c> avant de la faire naitre.
    /// </para>
    /// <para>
    /// <b>Le produit n'est pas recadre.</b> <c>imul eax,[esi+44h]</c> n'est suivi d'aucun decalage :
    /// l'entree de table de sinus multiplie le rayon tel quel, la ou le reste du depot ramene le
    /// resultat par <c>&gt;&gt; 0xc</c>. C'est ce qui fait tenir un rayon de <c>0x50</c> a l'echelle
    /// du monde.
    /// </para>
    /// <para>
    /// <b>Elle grandit puis se retracte, et <c>+0x14</c> est la charniere.</b> Ce compteur, pose a
    /// <c>0x28</c> par le sort, perd un par image : tant qu'il reste positif l'echelle monte
    /// (<c>+0x64</c> en X, <c>+0xc8</c> en Y) jusqu'a <c>0x800</c>/<c>0x1000</c> ; passe sous zero
    /// elle redescend du meme pas, et l'entite s'eteint quand <c>+0x28</c> devient negatif. Une
    /// seule valeur commande la croissance, la decroissance et la mort.
    /// </para>
    /// </summary>
    private static void TickHolySpark_0044FE00(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // Ghidra: LAB_0044fe1f — le centre de l'orbite suit la cible, une hauteur d'homme plus haut.
        *(int*)(e + 0x50) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x54) = *(int*)(anchor + 0x20) + 0x3c000;
        *(int*)(e + 0x58) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[0x0a] = e[0x9a];                         // la teinte, que le sort a posee en +0x9a
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x28;   // Ghidra: DAT_00551550
            e[0x40] = 0;
            e[0x0b] = 0;

            *(int*)(e + 0x20) += 0xfa000;              // elle naît tres haut

            *(short*)(e + 0x28) = 0;                   // et sans taille
            *(short*)(e + 0x2a) = 0;

            e[0x38] = 0xc8;
            e[0x39] = 0xc8;
            e[0x3a] = 0xc8;
        }

        // Ghidra: LAB_0044fe7c — tout ce qui suit n'a lieu que dans l'etat 1.
        if (*(short*)(e + 0x10) != 1) return;

        int height = *(int*)(e + 0x20) - 0xa000;
        *(int*)(e + 0x20) = height;
        if (height < *(int*)(e + 0x54)) *(int*)(e + 0x20) = *(int*)(e + 0x54);

        // Ghidra: LAB_0044fe9d — la position horizontale, recalculee et non integree.
        // `call 00545A1C` = winsys3d Ordinal_451, la table de sinus ; `call 00545A16` = son
        // voisin de table d'import, le cosinus.
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(*(int*)(e + 0x48)) * *(int*)(e + 0x44)
                            + *(int*)(e + 0x50);

        *(int*)(e + 0x24) = (int)Winsys3d.GetCosTableEntry(*(int*)(e + 0x48)) * *(int*)(e + 0x44)
                            + *(int*)(e + 0x58);

        int angle = *(int*)(e + 0x48) + *(int*)(e + 0x5c);
        *(int*)(e + 0x48) = angle;

        if (angle >= 0x1000) *(int*)(e + 0x48) = angle - 0x1000;

        // Ghidra: LAB_0044fee1 — le repli par le bas relit le champ, il ne reprend pas `angle`.
        if (*(int*)(e + 0x48) < 0) *(int*)(e + 0x48) += 0x1000;

        // Ghidra: LAB_0044fef0 — un seul compteur pour la croissance, la retraction et la mort.
        *(short*)(e + 0x14) -= 1;

        if (*(short*)(e + 0x14) >= 0)
        {
            // Ghidra: LAB_0044ff17
            *(short*)(e + 0x28) += 0x64;
            short scale = *(short*)(e + 0x28);
            *(short*)(e + 0x2a) += 0xc8;

            if (scale >= 0x800)
            {
                *(short*)(e + 0x28) = 0x800;
                *(short*)(e + 0x2a) = 0x1000;
            }

            return;
        }

        *(short*)(e + 0x28) -= 0x64;
        short shrunk = *(short*)(e + 0x28);
        *(short*)(e + 0x2a) -= 0xc8;

        if (shrunk < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0044fc90 @ 0x0044fc90 — <b>le sous-type <c>0xa8</c>, creneau 168 de
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>. Decompilee dans <c>Blaze.c</c>.
    /// <b>Voisine immediate</b> de <see cref="TickHolySpark_0044FE00"/> (<c>0xa7</c>) et de
    /// <see cref="EntityHandler_0044FB10"/> (<c>0xa9</c>) : le binaire range les cinq enfants de
    /// benediction cote a cote, sous-types <c>0xa5</c> a <c>0xa9</c> par adresses croissantes.
    ///
    /// <para>
    /// <b>Elle tourne autour de sa cible en montant et en descendant.</b> Comme ses sœurs elle
    /// tient sa cible en <c>+0x78</c> — pas <c>+0x6c</c> — et recopie sa position dans l'ancre
    /// <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> a chaque image ; si la cible a disparu elle prend
    /// l'entite de service <see cref="Resources.s_EntityList_005de558"/>. La position est ensuite
    /// entierement recalculee : c'est la spirale sur une sphere de
    /// <see cref="EntityHandler_004034A0"/>, avec le cap <c>+0x4c</c>, l'elevation <c>+0x48</c> et
    /// le rayon <c>+0x44</c>.
    /// </para>
    /// <para>
    /// <b>Ce qui lui appartient : l'elevation n'est pas un champ, c'est une fonction du cap.</b>
    /// A chaque image <c>+0x48</c> vaut <c>sin(cap) &#215; +0x12 &gt;&gt; 12</c>, replie sur douze
    /// bits — l'anneau <b>ondule</b> au lieu de rester a plat, et <c>+0x12</c> (pris du cap de
    /// depart a la naissance) en regle l'amplitude. Le rayon et le pas angulaire <c>+0x5c</c>
    /// viennent du sort, qui les pose avant de la faire naitre.
    /// </para>
    /// <para>
    /// <b>La table de sinus recoit le cap NON replie</b> (<c>Ordinal_451(uVar4)</c> avant le
    /// masque) alors que les trois autres appels recoivent le champ deja masque. Transcrit tel
    /// quel.
    /// </para>
    /// <para>
    /// <b>Quatre etats</b>, et un seul compteur les enchaine : 1 elle enfle de <c>0x5a</c> par
    /// image jusqu'a <c>1000</c> ; 2 elle attend que <c>+0x14</c> — la duree que le sort lui a
    /// donnee — retombe sous zero ; 3 elle se retracte de <c>0x3c</c> par image et s'eteint a
    /// zero. Elle ne meurt d'aucune autre facon : ni le decor ni un heurt ne l'arretent.
    /// </para>
    /// <para>
    /// <b>Son engendreur n'est pas identifie.</b> Aucun site d'appel decompile de
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> ne passe <c>0xa8</c> ; comme pour <c>0xa6</c>,
    /// le pere est un corps que Ghidra n'a pas decompile.
    /// </para>
    /// </summary>
    private static void EntityHandler_0044FC90(byte* e)
    {
        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;   // Ghidra: DAT_005de558

        *(int*)(e + 0x50) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x54) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x58) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x28;   // Ghidra: &DAT_00551550
            e[0x40] = 0;
            e[0x0a] = e[0x9a];                         // la couche, que le sort a posee en +0x9a
            e[0x0b] = 0;

            e[0x38] = 200;
            e[0x39] = 200;
            e[0x3a] = 200;

            // L'amplitude de l'ondulation : l'elevation de depart, gardee sur seize bits.
            *(short*)(e + 0x12) = (short)*(int*)(e + 0x48);

            *(short*)(e + 0x28) = 0;                   // elle nait sans taille
            *(short*)(e + 0x2a) = 0;
        }

        // Ghidra: le cap avance, puis se replie — mais la table de sinus recoit la valeur AVANT
        // le repli. Voir le commentaire de tete.
        int raw = *(int*)(e + 0x4c) + *(int*)(e + 0x5c);
        *(int*)(e + 0x4c) = raw & 0xfff;

        // L'elevation, recalculee du cap a chaque image : l'anneau ondule.
        int pitch = ((int)Winsys3d.GetSinTableEntry(raw) * *(short*)(e + 0x12) >> 0xc) & 0xfff;
        *(int*)(e + 0x48) = pitch;

        int radius = *(int*)(e + 0x44);
        int yaw = *(int*)(e + 0x4c);

        // Le produit du rayon par le cosinus de l'elevation est ramene de douze bits ; celui par
        // le sinus ne l'est pas. Meme asymetrie que chez EntityHandler_004034A0.
        *(int*)(e + 0x1c) = (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc)
                            * (int)Winsys3d.GetSinTableEntry(yaw) + *(int*)(e + 0x50);

        *(int*)(e + 0x24) = (Winsys3d.GetCosTableEntry(*(int*)(e + 0x48)) * *(int*)(e + 0x44) >> 0xc)
                            * Winsys3d.GetCosTableEntry(yaw) + *(int*)(e + 0x58);

        *(int*)(e + 0x20) =
            (int)Winsys3d.GetSinTableEntry(*(int*)(e + 0x48)) * *(int*)(e + 0x44)
            + *(int*)(e + 0x54);

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x28) += 0x5a;
            *(short*)(e + 0x2a) += 0x5a;

            if (*(short*)(e + 0x28) > 1000)
            {
                *(short*)(e + 0x28) = 1000;
                *(short*)(e + 0x2a) = 1000;
                *(short*)(e + 0x10) = 2;
            }
        }

        // Ghidra: le decompte est fait DANS le test — il a lieu des que l'etat vaut 2.
        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x14) -= 1;
            if (*(short*)(e + 0x14) < 0) *(short*)(e + 0x10) = 3;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x28) -= 0x3c;
            *(short*)(e + 0x2a) -= 0x3c;

            if (*(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044ff40 @ 0x0044ff40 — <b>le sous-type <c>0xa6</c>, creneau 166 de
    /// <see cref="s_StaticEntityHandlers_00552230"/></b>. Decompilee dans <c>Blaze.c</c>.
    /// La quatrieme du bloc de cinq (<c>0xa5</c> a <c>0xa9</c>), entre
    /// <see cref="TickHolySpark_0044FE00"/> et <see cref="TickHolyCatchSpark_004500B0"/>.
    ///
    /// <para>
    /// <b>Une bulle qui monte en s'eclaircissant.</b> Elle ne suit personne — aucune cible, aucune
    /// ancre : elle nait ou on la pose, s'ecarte une fois pour toutes d'un cap tire sur douze bits
    /// et d'une distance tiree sur le <c>+0x14</c> qu'elle a herite, puis ne fait plus que monter.
    /// </para>
    /// <para>
    /// <b>Son ascension accelere</b> : <c>+0x5c</c> part entre 12 et 21, <c>Y</c> en gagne
    /// <c>&#215; 0x1000</c> par image, et <c>+0x5c</c> lui-meme gagne un <b>une image sur deux</b>
    /// (la parite de la duree restante), plafonne a <c>0x1e</c>.
    /// </para>
    /// <para>
    /// <b>Elle enfle vite et s'arrete net</b> : les deux echelles gagnent <c>+0x16</c> (400 a 799)
    /// par image et sont ecretees a <c>+0x9a</c> (3000 a 6999) — ici <c>+0x9a</c> porte un
    /// <b>plafond d'echelle</b>, la ou sa voisine <c>0xa8</c> y lit une couche de dessin. Un champ,
    /// deux roles, selon le sous-type.
    /// </para>
    /// <para>
    /// <b>Elle part noire et s'eclaircit de quinze par canal et par image</b>
    /// (<see cref="BrightenEntityTint_00404DE0"/>), et vit entre 8 et 22 images — la duree, tiree
    /// a la naissance, est le meme champ <c>+0x14</c> qui vient de servir a la dispersion. Le
    /// binaire lit donc l'ancienne valeur avant de l'ecraser ; si elle vaut zero, la division de
    /// la dispersion est une division par zero. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_0044FF40(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x10;   // Ghidra: &DAT_00551538
            e[0x40] = 0;
            e[0x0a] = e[0x9a];
            e[0x0b] = 0;

            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = CrtRand() % 10 + 0x0c;            // Ghidra: FUN_00547140

            // Le cap et la distance sont tires AVANT que +0x14 ne devienne la duree.
            int heading = (short)(CrtRand() % 0x1000);
            int spread = (short)(CrtRand() % *(short*)(e + 0x14));

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * spread;

            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;

            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * spread;

            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x16) = (short)(CrtRand() % 400 + 400);   // le pas d'echelle
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x0f + 8);    // la duree ecrase la portee
            *(short*)(e + 0x9a) = (short)(CrtRand() % 4000 + 3000); // le plafond d'echelle
        }

        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, 0x0f, 0x0f, 0x0f);

            *(short*)(e + 0x28) += *(short*)(e + 0x16);
            *(short*)(e + 0x2a) += *(short*)(e + 0x16);

            short ceiling = *(short*)(e + 0x9a);
            if (ceiling < *(short*)(e + 0x28)) *(short*)(e + 0x28) = ceiling;
            if (ceiling < *(short*)(e + 0x2a)) *(short*)(e + 0x2a) = ceiling;

            *(short*)(e + 0x12) += 1;
            *(int*)(e + 0x20) += *(int*)(e + 0x5c) * 0x1000;

            // La parite de la duree restante : une image sur deux, la montee s'accelere.
            if ((*(ushort*)(e + 0x14) & 1) != 0)
            {
                *(int*)(e + 0x5c) += 1;
                if (*(int*)(e + 0x5c) > 0x1e) *(int*)(e + 0x5c) = 0x1e;
            }

            *(short*)(e + 0x14) = (short)(*(ushort*)(e + 0x14) - 1);
            if (*(short*)(e + 0x14) < 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00448250 @ 0x00448250 — <b>l'eclat des benedictions du Pretre</b> : le sous-type
    /// <c>0xa0</c>, creneau 160 de <see cref="s_StaticEntityHandlers_00551f30"/>. 168 octets,
    /// <b>decompilee dans <c>Blaze.c</c></b>. <see cref="TickReinforce_00448060"/> et Gnoukoule en
    /// font naitre <b>un seul</b>, a leur trentieme image.
    ///
    /// <para>
    /// <b>Elle ne fait que s'allumer puis s'eteindre.</b> Trois etats : la naissance pose le modele
    /// <c>2</c>, une echelle de <c>0x11f4</c> sur les trois axes et une teinte <b>noire</b> ;
    /// l'etat 1 ajoute <c>+50</c> aux trois canaux par image jusqu'a ce que le rouge atteigne
    /// <c>0xb4</c> ; l'etat 2 retire <c>-15 / -10 / -15</c> — <b>le vert plus lentement que les deux
    /// autres</b>, ce qui fait virer l'eclat au vert en mourant — et l'entite s'eteint quand ce vert
    /// touche zero.
    /// </para>
    /// <para>
    /// <b>Elle est la seule de la famille a ne pas commencer par
    /// <see cref="EndSpellOnTerrain_00448040"/>.</b> Elle colle a la cible comme les autres
    /// (<c>+0x50000</c> au-dessus), mais le decor ne l'eteint pas.
    /// </para>
    /// </summary>
    private static void TickHolyFlash_00448250(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // Ghidra: LAB_00448261
        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 2;
            e[0x0a] = 6;
            e[0x0b] = 1;

            e[0x38] = 0;                               // elle naît noire
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x28) = 0x11f4;
            *(short*)(e + 0x2a) = 0x11f4;
            *(short*)(e + 0x2c) = 0x11f4;

            *(short*)(e + 0x10) = 1;
        }

        // Ghidra: LAB_004482b1 — la naissance et la premiere montee sont la meme passe.
        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, 0x32, 0x32, 0x32);
            if (e[0x38] >= 0xb4) *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_004482d3
        if (*(short*)(e + 0x10) == 2)
        {
            BrightenEntityTint_00404DE0(e, -15, -10, -15);
            if (e[0x39] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00448060 @ 0x00448060 — <b>Renfort</b> : le sous-type <c>0x20</c>, creneau 32 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>troisieme fiche de l'ecole 1</b>. 425
    /// octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il pose son effet a la soixantieme image, pas a la premiere</b> — l'inverse de
    /// <see cref="TickPureBlood_00447DF0"/>, dont il est pourtant le voisin immediat dans le
    /// binaire et le jumeau de forme. Il recopie alors sa paire famille/indice sur le lanceur et
    /// appelle <see cref="ApplyStatusEffectIfEligible_00448210"/>, l'entree 17 de la table des 48.
    /// Les cinquante-neuf images precedentes sont entierement decoratives.
    /// </para>
    /// <para>
    /// <b>Son calendrier tient dans un seul compteur.</b> <c>+0x16</c> avance d'un par image et
    /// commande tout : un son a la 0 et a la 2, <b>deux etincelles</b> aux images 1, 3, 6 et 9,
    /// l'eclat a la 30, deux sons de plus a la 35 et a la 37, l'effet et l'extinction passe la 60.
    /// Les quatre bouffees sont resserrees au debut et rien ne se produit ensuite pendant vingt
    /// images.
    /// </para>
    /// <para>
    /// <b>Les deux etincelles d'une bouffee sont opposees</b> : <c>+0x48</c>, l'angle de depart
    /// qu'elles heritent par la copie, vaut <c>0</c> pour la premiere et <c>0x800</c> pour la
    /// seconde — un demi-tour d'ecart sur le meme cercle.
    /// </para>
    /// <para>
    /// <b>Il parametre ses enfants dans son propre enregistrement.</b> <c>+0x44 = 0x50</c> le rayon,
    /// <c>+0x5c = 0x96</c> le pas angulaire, <c>+0x14 = 0x28</c> la duree de croissance,
    /// <c>+0x9a = 6</c> la teinte — aucun de ces quatre champs ne lui sert a lui : ils n'existent
    /// que pour etre recopies par <see cref="SpawnStatusEffectEntity_0047AC40"/> dans chaque
    /// etincelle. C'est ce qui distingue Renfort de Gnoukoule, qui pose <c>2</c> en <c>+0x9a</c>.
    /// </para>
    /// </summary>
    private static void TickReinforce_00448060(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_00448087
        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            // Les quatre champs qui ne servent qu'aux etincelles.
            *(short*)(e + 0x14) = 0x28;
            *(int*)(e + 0x44) = 0x50;
            *(int*)(e + 0x5c) = 0x96;
            *(short*)(e + 0x9a) = 6;
        }

        // Ghidra: LAB_004480dd — quatre bouffees, deux etincelles chacune, a un demi-tour d'ecart.
        short step = *(short*)(e + 0x16);

        if (step == 1 || step == 3 || step == 6 || step == 9)
        {
            *(int*)(e + 0x48) = 0;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa7, 0, -1);

            *(int*)(e + 0x48) = 0x800;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa7, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x16, 0x100, 0x1200, 0x1200);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x16, 0x12a, 0x1200, 0x1200);

        // Ghidra: LAB_00448171 — l'eclat, un seul, a mi-parcours.
        if (*(short*)(e + 0x16) == 0x1e) SpawnCastEntity_0047AAE0(e, 0xa0, 0, -1);

        if (*(short*)(e + 0x16) == 0x23) Aud.PlaySoundEffect(0x19, 0x800, 0x1200, 0x1200);
        if (*(short*)(e + 0x16) == 0x25) Aud.PlaySoundEffect(0x1c, 0x400, 0x1000, 0x1000);

        // Ghidra: LAB_004481ca
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 0x3c)
        {
            // La paire famille/indice passe au lanceur : c'est SA fiche que l'effet relira.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ApplyStatusEffectIfEligible_00448210(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete

            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // GNOUKOULE — le trente-troisieme sort, ecole 1 indice 3, sous-type 0x21
    //
    //   0x00448300   LE SORT. Sous-type 0x21 de 0x00551c30, 418 octets. DECOMPILE dans Blaze.c.
    //   0x004484B0   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //
    // DEUX CORPS, ET AUCUN ENFANT NEUF. Ses deux enfants — l'etincelle 0xa7
    // (TickHolySpark_0044FE00) et l'eclat 0xa0 (TickHolyFlash_00448250) — ont ete ecrits avec
    // Renfort, dont il est le clone.
    //
    // CLONE, AU SENS PROPRE. Renfort et lui font la meme chose octet pour octet a QUATRE VALEURS
    // pres, et ce sont exactement les quatre qu'un sort de cette famille est cense porter :
    //
    //   +0x9a         6 chez Renfort, 2 chez lui — la teinte que chaque etincelle recopie en +0x0a
    //   son a la 30e  Renfort n'en joue pas, lui joue 0x0d ; et il n'a pas le couple 0x23/0x25 de
    //                 Renfort mais un seul son a la 32e image
    //   l'effet       17 (0x0053bbd0) chez Renfort, 23 (0x0053bfb0) chez lui
    //
    // Le calendrier, les bouffees, le rayon, le pas angulaire, la duree de croissance, l'eclat de
    // la 30e image et la fin a la 60e sont identiques. Le compilateur a emis deux fois le meme
    // corps plutot que de le partager — le meme constat que pour LAB_00474320 et FUN_00450700,
    // mais ici sur 418 octets.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004484b0 @ 0x004484b0 — <b>poser l'effet 23 si la cible l'accepte</b>. Soixante
    /// et un octets, decompilee dans <c>Blaze.c</c>. Troisieme exemplaire de la meme enveloppe que
    /// <see cref="HealTargetIfEligible_00447FE0"/> et
    /// <see cref="ApplyStatusEffectIfEligible_00448210"/> : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="ApplyStatusEffect_0053BFB0"/> derriere.
    ///
    /// <para><b>Aucun appelant ne lit ce qu'elle rend</b>, comme chez ses deux sœurs.</para>
    /// </summary>
    private static bool ApplyStatusEffectIfEligible_004484B0(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ApplyStatusEffect_0053BFB0(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00448300 @ 0x00448300 — <b>Gnoukoule</b> : le sous-type <c>0x21</c>, creneau 33
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>quatrieme fiche de l'ecole 1</b>.
    /// 418 octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickReinforce_00448060"/> a quatre valeurs pres.</b> Meme trio de tete,
    /// meme copie de la position de la cible <c>0x50000</c> plus haut, memes quatre bouffees de
    /// deux etincelles aux images 1, 3, 6 et 9 avec le meme demi-tour d'ecart, meme rayon
    /// <c>0x50</c>, meme pas angulaire <c>0x96</c>, meme duree de croissance <c>0x28</c>, meme
    /// eclat unique a la 30e image, meme fin passe la 60e. Les differences tiennent en quatre
    /// lignes : la teinte <c>+0x9a</c> vaut <b>2</b> et non 6, le son <c>0x0d</c> accompagne
    /// l'eclat, le couple de sons de la 35e et de la 37e est remplace par un seul a la <b>32e</b>,
    /// et l'effet pose est le <b>23</b> au lieu du 17.
    /// </para>
    /// <para>
    /// <b>Le binaire porte donc deux fois le meme corps.</b> Ce n'est pas une fonction partagee
    /// avec un parametre : ce sont 418 octets recopies, et les quatre valeurs qui les distinguent
    /// sont des immediats. Le meme constat que pour <c>LAB_00474320</c> et
    /// <see cref="ChestPickupDone_00450700"/>, a une autre echelle.
    /// </para>
    /// </summary>
    private static void TickGnoukoule_00448300(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_00448327
        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            // Les quatre champs qui ne servent qu'aux etincelles. Seul le dernier separe cette
            // fiche de Renfort, qui y pose 6.
            *(short*)(e + 0x14) = 0x28;
            *(int*)(e + 0x44) = 0x50;
            *(int*)(e + 0x5c) = 0x96;
            *(short*)(e + 0x9a) = 2;
        }

        // Ghidra: LAB_0044837d — quatre bouffees, deux etincelles chacune, a un demi-tour d'ecart.
        short step = *(short*)(e + 0x16);

        if (step == 1 || step == 3 || step == 6 || step == 9)
        {
            *(int*)(e + 0x48) = 0;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa7, 0, -1);

            *(int*)(e + 0x48) = 0x800;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa7, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x16, 0x100, 0x1200, 0x1200);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x16, 0x12a, 0x1200, 0x1200);

        // Ghidra: LAB_00448411 — l'eclat, et le son que Renfort ne joue pas ici.
        if (*(short*)(e + 0x16) == 0x1e)
        {
            SpawnCastEntity_0047AAE0(e, 0xa0, 0, -1);
            Aud.PlaySoundEffect(0x0d, 0x5a9, 0x1200, 0x1200);
        }

        if (*(short*)(e + 0x16) == 0x20) Aud.PlaySoundEffect(0x2d, 0x400, 0x1000, 0x1000);

        // Ghidra: LAB_00448463
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 0x3c)
        {
            // La paire famille/indice passe au lanceur : c'est SA fiche que l'effet relira.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ApplyStatusEffectIfEligible_004484B0(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete

            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // SONDE — le trente-quatrieme sort, ecole 1 indice 4, sous-type 0x37
    //
    //   0x004484F0   LE SORT. Sous-type 0x37 de 0x00551c30, 704 octets. DECOMPILE dans Blaze.c.
    //   0x004487B0   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //   0x004487F0   LE TRAIT, sous-type 0xa2 de 0x00551f30, 1104 octets. HUIT. Decompile.
    //   0x00448C40   L'EMBRASEMENT, sous-type 0xa3 de 0x00551f30, 510 octets. UN. Decompile.
    //   0x00448E20   L'ETINCELLE, sous-type 0x97 de 0x00552230, 194 octets. ABSENTE de Blaze.c ;
    //                relevee sous Legacy/Raw/Blaze/FUN_00448E20.asm.
    //
    // LE SORT LE PLUS GROS DE L'ECOLE 1 A CE JOUR, et le premier dont le trait tienne quatre etats
    // successifs. Deux PM, et pourtant 2 500 octets de corps.
    //
    // LE SORT SE SERT DE SES PROPRES CHAMPS COMME D'UN PASSE-PLAT. +0x14, +0x16, +0x48 et +0x9a
    // sont ecrits juste avant chaque appel a SpawnCastEntity_0047AAE0 pour que la copie des 0x27
    // dwords les porte dans l'enfant, puis remis a leur role. Le cas de +0x16 est le plus net :
    // c'est le COMPTEUR D'IMAGES du sort, et il sert de fourgon a quatre angles tires au hasard
    // avant d'etre remis a zero. Rien dans la fonction ne signale ce double emploi.
    //
    // HUIT TRAITS EN QUATRE PAIRES, SUR LES QUATRE QUARTS DE TOUR. +0x48 vaut 0, 0x400, 0x800 puis
    // 0xc00 ; a chaque quart, deux traits naissent avec le meme cap mais un modele different
    // (+0x9a = 0x18 puis 0x19), et c'est ce modele qui decidera, cent images plus tard, de
    // laquelle des deux fins le trait prend.
    //
    // LB : LE FUSIBLE DU TRAIT NE PEUT PAS SAUTER. Sa derniere instruction incremente le compteur
    // OCTET +0x43 et le compare a 5000 : la comparaison est faite sur `eax` masque a 0xff, donc sur
    // une valeur bornee a 255. La branche d'extinction de secours est morte. Seul le chemin normal
    // — +0x43 passe 100 dans l'etat 0x63 — eteint un trait.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004487b0 @ 0x004487b0 — <b>poser l'effet 1 si la cible l'accepte</b>. Soixante
    /// et un octets, decompilee dans <c>Blaze.c</c>. Quatrieme exemplaire de la meme enveloppe que
    /// <see cref="HealTargetIfEligible_00447FE0"/>,
    /// <see cref="ApplyStatusEffectIfEligible_00448210"/> et
    /// <see cref="ApplyStatusEffectIfEligible_004484B0"/> : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="ApplyStatusEffect_0053B020"/> derriere.
    /// </summary>
    private static bool ApplyStatusEffectIfEligible_004487B0(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ApplyStatusEffect_0053B020(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00448e20 @ 0x00448e20 — <b>l'etincelle que les traits de Sonde laissent
    /// tomber</b> : le sous-type <c>0x97</c>, creneau 151 de
    /// <see cref="s_StaticEntityHandlers_00552230"/>. 194 octets, <b>absente de <c>Blaze.c</c></b>.
    /// <see cref="TickProbeRing_004487F0"/> en seme une toutes les quatre images, dans ses deux
    /// etats mobiles.
    ///
    /// <para>
    /// <b>Elle naît a cote, pas dessus.</b> Trois tirages independants la deplacent de
    /// <c>rand % 100 - 50</c> unites sur chacun des trois axes — un cube de cent unites de cote
    /// autour du point donne. Elle monte ensuite de <c>0x5000</c> par image en retrecissant de
    /// <c>0x190</c>, et s'eteint des que son echelle passe sous zero : <b>une dizaine d'images</b>.
    /// </para>
    /// <para>
    /// <b>Sa teinte est portee par <c>+0x5c</c>.</b> Le trait pose ce champ a <c>9</c> ou <c>2</c>
    /// juste avant de la faire naitre, uniquement pour qu'elle le recopie ici dans son
    /// <c>+0x0a</c>, puis le remet aussitot a sa valeur de rayon.
    /// </para>
    /// </summary>
    private static void TickProbeMote_00448E20(byte* record)
    {
        byte* e = record;

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551528();
            e[0x0a] = e[0x5c];                         // la teinte, passee par le champ de rayon
            e[0x40] = 0;
            e[0x0b] = 0;

            *(short*)(e + 0x28) = 0x0fa0;
            *(short*)(e + 0x2a) = 0x0fa0;
            *(short*)(e + 0x10) = 1;

            // Ghidra: FUN_00547140 trois fois — un cube de cent unites de cote.
            *(int*)(e + 0x1c) += (CrtRand() % 100 - 0x32) << 0xc;
            *(int*)(e + 0x20) += (CrtRand() % 100 - 0x32) << 0xc;
            *(int*)(e + 0x24) += (CrtRand() % 100 - 0x32) << 0xc;
        }

        // Ghidra: LAB_00448ea5 — la naissance et la premiere image sont la meme passe.
        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x20) += 0x5000;

            *(short*)(e + 0x28) -= 0x190;
            short scale = *(short*)(e + 0x28);
            *(short*)(e + 0x2a) -= 0x190;

            if (scale < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_004487f0 @ 0x004487f0 — <b>le trait de Sonde</b> : le sous-type <c>0xa2</c>,
    /// creneau 162 de <see cref="s_StaticEntityHandlers_00551f30"/>. 1104 octets, <b>decompilee
    /// dans <c>Blaze.c</c></b>. <see cref="TickProbe_004484F0"/> et
    /// <see cref="TickThirdEye_00448EF0"/> en font naitre <b>huit</b> chacun, en quatre paires.
    ///
    /// <para>
    /// <b>Il est en coordonnees spheriques, et il ne s'integre jamais.</b> A chaque image sa
    /// position vaut <c>ancre + vecteur(cap +0x14, tangage +0x16, rayon +0x5c)</c>, l'ancre etant
    /// le SORT — <c>+0x6c</c>, pas <c>+0x78</c> — releve de <c>0x3c000</c>. Le produit final n'est
    /// <b>pas recadre</b> : deux decalages de douze bits pour trois multiplications, comme chez
    /// <see cref="TickHolySpark_0044FE00"/>.
    /// </para>
    /// <para>
    /// <b>Quatre etats, et le rayon est l'horloge des deux premiers.</b> Etat 1, l'approche : le
    /// rayon converge vers <c>0x50000</c> d'un seizieme par image en partant de <c>0x3e8000</c>,
    /// le cap avance de 100 et le tangage se redresse d'un trente-deuxieme ; l'etat passe a 2 des
    /// que le rayon tombe sous <c>0x55000</c>. Etat 2, le rangement : cap et rotation convergent
    /// vers <c>+0x44</c>, le cap d'arrivee que la naissance a fige a partir du cap de depart, et le
    /// <c>+0x34</c> monte vers <c>0x1000</c> ; passe <c>0xf3c</c> l'etat passe a 3. Etat 3, la
    /// pose, deux sons ; etat <c>0x63</c>, la sortie.
    /// </para>
    /// <para>
    /// <b>Le modele decide de la fin.</b> <c>+0x04</c>, pose a la naissance depuis le <c>+0x9a</c>
    /// que le sort a rempli, vaut <c>0x18</c> ou <c>0x19</c> selon le trait. Le <c>0x19</c> quitte
    /// l'etat 3 en avancant son <c>+0x34</c> de 300 par image et s'arrete net a <c>0x640</c> ; le
    /// <c>0x18</c> l'avance de <c>0x28</c> et attend <c>0x1130</c>. Une seule valeur, deux fins.
    /// </para>
    /// <para>
    /// <b>Il seme une etincelle toutes les quatre images</b>, dans l'etat 1 et dans l'etat
    /// <c>0x63</c>, en pretant son champ de rayon <c>+0x5c</c> pour porter la teinte
    /// (<see cref="TickProbeMote_00448E20"/>) et en le restaurant juste apres.
    /// </para>
    /// <para>
    /// <b>LB — son fusible de secours ne peut pas sauter.</b> La derniere instruction incremente le
    /// compteur <b>octet</b> <c>+0x43</c> et compare le resultat, masque a <c>0xff</c>, a
    /// <c>5000</c> : la branche d'extinction est inatteignable. Seul le chemin normal — <c>+0x43</c>
    /// depassant 100 dans l'etat <c>0x63</c> — eteint un trait. Transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Sur les deux <c>CONCAT22</c> de la decompilation.</b> Ghidra rend deux expressions de
    /// l'etat 2 avec la moitie haute d'un registre laissee par un <c>movsx</c> precedent. Elle vaut
    /// <b>toujours zero ici</b> : l'etat 1 masque <c>+0x32</c> et <c>+0x34</c> sur douze bits a
    /// chaque passe et s'execute forcement avant l'etat 2, y compris dans la passe ou il y bascule.
    /// Rendu par ce que le calcul vaut.
    /// </para>
    /// </summary>
    private static void TickProbeRing_004487F0(byte* record)
    {
        byte* e = record;

        byte* origin = *(byte**)(e + 0x6c);            // le SORT, pas la cible
        if (origin == null) origin = Res.s_EntityList_005de558;

        // Ghidra: LAB_00448806
        *(int*)(e + 0x50) = *(int*)(origin + 0x1c);
        *(int*)(e + 0x54) = *(int*)(origin + 0x20) + 0x3c000;
        *(int*)(e + 0x58) = *(int*)(origin + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x28) = 6000;
            *(short*)(e + 0x2a) = 6000;
            *(short*)(e + 0x2c) = 6000;

            e[0x04] = e[0x9a];                         // le modele, 0x18 ou 0x19
            e[0x0a] = e[0x5c];                         // la teinte des etincelles
            e[0x0b] = 0;

            *(int*)(e + 0x5c) = 0x3e8000;              // et le champ reprend son role de RAYON
            *(short*)(e + 0x9a) = 0;

            *(int*)(e + 0x44) = *(ushort*)(e + 0x14) & 0xfff;   // le cap d'arrivee
            *(short*)(e + 0x98) = 0;
        }

        // Ghidra: LAB_00448871
        if (*(short*)(e + 0x98) == 0 && *(short*)(e + 0x10) < 3)
        {
            Aud.PlaySoundEffect(0x0d, 0x5a9, 0x1300, 0x1300);
        }

        *(short*)(e + 0x98) += 1;

        if (*(short*)(e + 0x10) != 0x63)
        {
            PlaceProbeRing(e, *(int*)(e + 0x54));
            *(int*)(e + 0x5c) = ((0x50000 - *(int*)(e + 0x5c)) >> 4) + *(int*)(e + 0x5c);
        }

        // Ghidra: LAB_00448930 — l'approche.
        if (*(short*)(e + 0x10) == 1)
        {
            int radius = ((0x50000 - *(int*)(e + 0x5c)) >> 4) + *(int*)(e + 0x5c);

            *(short*)(e + 0x9a) += 1;
            *(int*)(e + 0x5c) = radius;

            *(short*)(e + 0x32) = (short)((*(short*)(e + 0x32) + 200) & 0xfff);
            *(short*)(e + 0x34) = (short)((*(short*)(e + 0x34) + 100) & 0xfff);

            if (*(short*)(e + 0x9a) > 3)
            {
                *(short*)(e + 0x9a) = 0;
                DropProbeMote(e);
                *(int*)(e + 0x5c) = radius;
            }

            // Ghidra: LAB_004489bb
            *(short*)(e + 0x14) = (short)((*(short*)(e + 0x14) + 100) & 0xfff);
            *(short*)(e + 0x16) =
                (short)(((-(int)*(short*)(e + 0x16) >> 5) + *(short*)(e + 0x16)) & 0xfff);

            if (*(int*)(e + 0x5c) < 0x55000) *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_004489f5 — le rangement sur le cap d'arrivee.
        if (*(short*)(e + 0x10) == 2)
        {
            int landing = *(int*)(e + 0x44);

            *(short*)(e + 0x14) =
                (short)(((landing - *(short*)(e + 0x14)) >> 4) + *(short*)(e + 0x14));
            *(short*)(e + 0x16) =
                (short)((-(int)*(short*)(e + 0x16) >> 4) + *(short*)(e + 0x16));

            short turned = (short)(((landing - *(short*)(e + 0x32)) >> 3) + *(short*)(e + 0x32));

            // Ghidra: `mov [esi+32h],ax ; and eax,0xfff ; mov [esi+32h],ax` — la premiere
            // ecriture est recouverte deux instructions plus loin. Transcrite telle quelle.
            *(short*)(e + 0x32) = turned;
            *(short*)(e + 0x32) = (short)(turned & 0xfff);

            ushort spin = *(ushort*)(e + 0x34);
            spin = (ushort)((((0x1000 - spin) >> 3) + spin) & 0xfff);
            *(ushort*)(e + 0x34) = spin;

            if (spin > 0xf3c)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x98) = 0;
            }
        }

        // Ghidra: LAB_00448a70 — la pose, et les deux fins que le modele separe.
        if (*(short*)(e + 0x10) == 3)
        {
            if (*(short*)(e + 0x98) == 1) Aud.PlaySoundEffect(0x0a, 0x6fd, 0x1300, 0x1300);
            if (*(short*)(e + 0x98) == 6) Aud.PlaySoundEffect(0x0f, 0x100, 0x1300, 0x1300);

            if (e[0x04] == 0x19)
            {
                ushort spin = (ushort)((*(short*)(e + 0x34) + 300) & 0xfff);
                *(ushort*)(e + 0x34) = spin;

                if (spin > 0x640 && spin < 2000)
                {
                    *(short*)(e + 0x34) = 0x640;
                    *(short*)(e + 0x10) = 0x63;
                }
            }
            else
            {
                // Ghidra: LAB_00448af1
                *(short*)(e + 0x34) += 0x28;

                if (*(short*)(e + 0x34) > 0x112f)
                {
                    *(short*)(e + 0x10) = 0x63;
                    *(ushort*)(e + 0x34) &= 0xfff;
                    *(int*)(e + 0x48) = 0;
                }
            }
        }

        // Ghidra: LAB_00448b12 — la sortie : la meme position, decalee du +0x48 qui s'ouvre.
        if (*(short*)(e + 0x10) == 0x63)
        {
            PlaceProbeRing(e, *(int*)(e + 0x48) + *(int*)(e + 0x54));

            int radius = *(int*)(e + 0x5c);
            *(short*)(e + 0x9a) += 1;

            if (*(short*)(e + 0x9a) > 3)
            {
                *(short*)(e + 0x9a) = 0;
                DropProbeMote(e);
                *(int*)(e + 0x5c) = radius;
            }

            // Ghidra: LAB_00448bde — le fondu, cent images apres la naissance.
            if (e[0x43] > 100)
            {
                *(short*)(e + 0x32) += 200;
                *(short*)(e + 0x28) -= 200;
                *(short*)(e + 0x2c) -= 200;      // +0x2c, et non +0x2a : le binaire dit cela
                *(int*)(e + 0x48) += 0xf000;

                if (*(short*)(e + 0x28) < 0)
                {
                    *(short*)(e + 0x28) = 0;
                    *(short*)(e + 0x2a) = 0;
                    *(uint*)e &= 0x7fffffff;
                }
            }
        }

        // Ghidra: LAB_00448c1d — LB : compteur OCTET compare a 5000, la branche est morte.
        e[0x43] += 1;
        if ((e[0x43] & 0xff) >= 5000) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// La position spherique du trait de Sonde, ecrite deux fois dans <c>FUN_004487f0</c> — une
    /// fois pour les etats 1 a 3, une fois pour l'etat <c>0x63</c> — a la hauteur pres. Le
    /// <paramref name="baseY"/> vaut <c>+0x54</c> dans le premier cas et <c>+0x48 + +0x54</c> dans
    /// le second.
    ///
    /// <para>Ghidra: <c>Ordinal_450</c> est le cosinus (winsys3d), <c>Ordinal_451</c> le sinus.
    /// <b>Le produit final n'est pas recadre</b> : deux <c>sar 0xc</c> pour trois
    /// multiplications.</para>
    /// </summary>
    private static void PlaceProbeRing(byte* e, int baseY)
    {
        int pitch = *(short*)(e + 0x16);
        int yaw = *(short*)(e + 0x14);
        int radius = *(int*)(e + 0x5c) >> 0xc;

        *(int*)(e + 0x1c) =
            (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc)
            * (int)Winsys3d.GetSinTableEntry(yaw) + *(int*)(e + 0x50);

        *(int*)(e + 0x24) =
            (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc)
            * Winsys3d.GetCosTableEntry(yaw) + *(int*)(e + 0x58);

        *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(pitch) * radius + baseY;
    }

    /// <summary>
    /// Le semis d'une etincelle par le trait de Sonde, ecrit deux fois dans <c>FUN_004487f0</c>.
    /// Il prete <c>+0x5c</c> — son champ de rayon — pour porter la teinte que
    /// <see cref="TickProbeMote_00448E20"/> recopiera ; l'appelant le restaure aussitot.
    ///
    /// <para><b>Les deux tests sont vivants, un par sort</b> : <c>+0x0a</c> vaut le <c>+0x5c</c>
    /// que le sort avait pose avant d'engendrer le trait — <c>0x0e</c> pour Sonde,
    /// <c>0x0d</c> pour <see cref="TickThirdEye_00448EF0"/>. Les etincelles de l'un naissent donc
    /// avec la teinte <c>9</c>, celles de l'autre avec la teinte <c>2</c>.</para>
    /// </summary>
    private static void DropProbeMote(byte* e)
    {
        if (e[0x0a] == 0x0e) *(int*)(e + 0x5c) = 9;
        if (e[0x0a] == 0x0d) *(int*)(e + 0x5c) = 2;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x97, 0, -1);
    }

    /// <summary>
    /// Ghidra: FUN_00448c40 @ 0x00448c40 — <b>l'embrasement de Sonde</b> : le sous-type
    /// <c>0xa3</c>, creneau 163 de <see cref="s_StaticEntityHandlers_00551f30"/>. 510 octets,
    /// <b>decompilee dans <c>Blaze.c</c></b>. Sonde et 3eme oeil n'en font naitre <b>qu'un</b>
    /// chacun, sur la cible.
    ///
    /// <para>
    /// <b>Il tient sa victime.</b> Tant qu'elle ne porte aucun bit de <c>0x50000000</c> il recopie
    /// sa position et lui pose <c>0x1000</c> — l'immobilisation ; sinon c'est lui qui se marque
    /// <c>0x2000000</c>. <b>Sa hauteur vient de <c>+0x60</c> de la cible</b>, et non de son
    /// <c>+0x20</c> : le seul corps de sort transcrit qui lise ce champ-la pour se placer.
    /// </para>
    /// <para>
    /// <b>Sa taille est celle de la victime</b> : <c>hauteur &#215; 30</c> par
    /// <see cref="StatusBubbleHeight_00538BC0"/>, bornee entre <c>0x1000</c> et <c>30000</c>. Ce
    /// plafond est recalcule a chaque image, meme dans les etats qui ne s'en servent pas.
    /// </para>
    /// <para>
    /// Quatre etats sur un seul compteur : l'ouverture, qui converge d'un quart par image vers
    /// cette taille et vers une rotation de <c>0x800</c> et dure quatorze images ; l'attente, cent
    /// images ; la fermeture, qui ramene tout vers zero au quart et au huitieme. Il s'eteint a la
    /// <c>0x96</c>e image et <b>relache alors sa victime</b>.
    /// </para>
    /// </summary>
    private static void TickProbeBurst_00448C40(byte* record)
    {
        byte* e = record;

        uint* victim = *(uint**)(e + 0x78);
        if (victim == null) victim = (uint*)Res.s_EntityList_005de558;

        // Ghidra: LAB_00448c54
        if ((*victim & 0x50000000) == 0)
        {
            *(int*)(e + 0x1c) = *(int*)((byte*)victim + 0x1c);
            *(int*)(e + 0x20) = *(int*)((byte*)victim + 0x60) + 0x5000;   // +0x60, pas +0x20
            *(int*)(e + 0x24) = *(int*)((byte*)victim + 0x24);

            *victim |= 0x1000;
        }
        else
        {
            *(uint*)e |= 0x2000000;
        }

        int size = StatusBubbleHeight_00538BC0(victim) * 0x1e;
        if (size < 0x1000) size = 0x1000;
        if (size > 30000) size = 30000;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x2b;
            e[0x0a] = e[0x5c];
            e[0x0b] = 0;

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0x1000;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x98) == 0)
            {
                Aud.PlaySoundEffect(0x2c, 0x27f, 0x1200, 0x1200);
                *(short*)(e + 0x98) += 1;
            }

            if (*(short*)(e + 0x98) == 1)
            {
                Aud.PlaySoundEffect(0x2f, 0x200, 0x1200, 0x1200);
                *(short*)(e + 0x98) += 1;
            }

            *(short*)(e + 0x32) += (short)((0x800 - *(short*)(e + 0x32)) >> 2);
            *(short*)(e + 0x28) += (short)((size - *(short*)(e + 0x28)) >> 2);
            *(short*)(e + 0x2c) += (short)((size - *(short*)(e + 0x2c)) >> 2);

            if (*(short*)(e + 0x16) > 0x0d)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x16) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2 && *(short*)(e + 0x16) > 100) *(short*)(e + 0x10) = 3;

        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x32) += (short)(-(int)*(short*)(e + 0x32) >> 2);
            *(short*)(e + 0x28) += (short)(-(int)*(short*)(e + 0x28) >> 3);
            *(short*)(e + 0x2c) += (short)(-(int)*(short*)(e + 0x2c) >> 3);
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 0x95)
        {
            *(uint*)e &= 0x7fffffff;
            *victim &= 0xffffefff;                     // la victime est relachee
        }
    }

    /// <summary>
    /// Ghidra: FUN_004484f0 @ 0x004484f0 — <b>Sonde</b> : le sous-type <c>0x37</c>, creneau 55 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>cinquieme fiche de l'ecole 1</b>. 704
    /// octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne fait rien apres la premiere image, ou presque.</b> Tout son travail tient dans le
    /// bloc de naissance : huit traits, un embrasement, l'immobilisation du lanceur et la teinte du
    /// decor. Les 160 images suivantes n'ajoutent qu'une etincelle <c>0xa9</c> une image sur deux,
    /// entre la 80e et la 140e.
    /// </para>
    /// <para>
    /// <b>Ses propres champs servent de passe-plat.</b> <c>+0x14</c>, <c>+0x16</c>, <c>+0x48</c> et
    /// <c>+0x9a</c> sont ecrits juste avant chaque <see cref="SpawnCastEntity_0047AAE0"/> pour que
    /// la copie des <c>0x27</c> dwords les porte dans l'enfant. Le cas de <c>+0x16</c> est le plus
    /// net : c'est le <b>compteur d'images</b> du sort, et il transporte quatre angles tires au
    /// hasard avant d'etre remis a zero.
    /// </para>
    /// <para>
    /// <b>Huit traits en quatre paires, sur les quatre quarts de tour.</b> Le cap de depart
    /// <c>+0x14</c> vaut le <c>+0x32</c> de la cible plus <c>0</c>, <c>0x400</c>, <c>0x800</c> et
    /// <c>0xc00</c> ; le tangage <c>+0x16</c> est tire au hasard sur le tour entier, un tirage par
    /// paire ; et les deux traits d'une paire ne different que par leur modele, <c>0x18</c> puis
    /// <c>0x19</c>, ce qui leur donnera deux fins differentes.
    /// </para>
    /// <para>
    /// <b>Il immobilise son lanceur puis le relache en deux temps.</b> Le bit <c>0x80</c> est pose
    /// a la naissance, efface a la <c>0x78</c>e image, puis <b>efface une seconde fois</b> a la
    /// <c>0xa0</c>e — et cette seconde branche est la seule qui pose l'effet. L'ecriture de trop
    /// est sans consequence.
    /// </para>
    /// <para>
    /// <b>L'ordre de la fin est l'inverse de celui de ses voisins.</b> Renfort et Gnoukoule posent
    /// l'effet puis s'eteignent ; Sonde s'eteint d'abord — <see cref="EndSpellAndResetDecor_00448020"/>
    /// — et recopie sa paire famille/indice ensuite. Sans consequence, l'extinction ne touchant que
    /// le mot d'etat et le mode du decor.
    /// </para>
    /// </summary>
    private static void TickProbe_004484F0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_00448516 — la position de la cible, sans relevement.
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x5c) = 0x0e;                  // la teinte que les traits porteront

            // Quatre paires, un quart de tour d'ecart, un tangage tire par paire.
            for (int quarter = 0; quarter < 4; quarter++)
            {
                *(short*)(e + 0x14) = (short)(*(short*)(target + 0x32) + quarter * 0x400);
                *(short*)(e + 0x16) = (short)(CrtRand() % 0x1000);   // Ghidra: FUN_00547140
                *(int*)(e + 0x48) = quarter * 0x400;

                *(short*)(e + 0x9a) = 0x18;
                SpawnCastEntity_0047AAE0(e, 0xa2, 0, -1);

                *(short*)(e + 0x9a) = 0x19;
                SpawnCastEntity_0047AAE0(e, 0xa2, 0, -1);
            }

            *(short*)(e + 0x16) = 0;                   // le champ redevient le compteur d'images
            *(int*)(e + 0x5c) = 9;                     // et celui-ci la teinte de l'embrasement

            SpawnCastEntity_0047AAE0(e, 0xa3, 0, -1);

            **(uint**)(e + 0x74) |= 0x80;              // le lanceur est immobilise

            // Le decor vire au violet, et le mot de mode passe a 2 — propre a cette ecole.
            *(ushort*)Mem.Arena(0x005e9160) = 0xa0;
            *(ushort*)Mem.Arena(0x005e9162) = 0xa0;
            *(ushort*)Mem.Arena(0x005e9164) = 0xdc;
            *(ushort*)Mem.Arena(0x005e9166) = 2;
        }

        // Ghidra: LAB_00448717 — une etincelle une image sur deux, de la 80e a la 140e.
        if (*(short*)(e + 0x16) >= 0x50)
        {
            *(short*)(e + 0x14) = 0x50;                // le rayon que l'etincelle 0xa9 tirera
            *(short*)(e + 0x9a) = 9;

            if (*(short*)(e + 0x16) < 0x8c && (e[0x16] & 1) != 0)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            }
        }

        // Ghidra: LAB_00448752
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) >= 0x78) **(uint**)(e + 0x74) &= 0xffffff7f;

        if (*(short*)(e + 0x16) >= 0xa0)
        {
            **(uint**)(e + 0x74) &= 0xffffff7f;        // deja efface vingt images plus tot
            EndSpellAndResetDecor_00448020(e);

            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ApplyStatusEffectIfEligible_004487B0(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete
        }
    }

    // =====================================================================
    // BOBOS FINIS — le trente-cinquieme sort, ecole 1 indice 5, sous-type 0x23
    //
    //   0x004491F0   LE SORT. Sous-type 0x23 de 0x00551c30, 822 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_004491F0.asm.
    //   0x00449530   LE SOIN PARTAGE SOUS CONDITION. Soixante et un octets, decompile.
    //   0x00449570   LE COMPTAGE DES ENTITES D'EFFET VIVANTES. Trente et un octets, decompile.
    //
    // AUCUN CORPS D'ENFANT NEUF, pour la deuxieme fois de l'ecole : l'etincelle 0xa9 est celle de
    // la bouffee et la bille 0x70 celle d'Enchant. Les deux etaient deja ecrites pour Sang pur.
    //
    // C'EST SANG PUR SUR LES QUATRE MEMBRES. La seconde moitie de la fonction est, instruction pour
    // instruction, le bloc de bille de TickPureBlood_00447DF0 — meme modele 0x3f, meme +0x48 a
    // 0x50000, memes deux tirages en +0x32 et +0x34, meme rayon 0xc8 — mais executee une fois par
    // membre eligible au lieu d'une fois pour la cible. Le sort a ete ecrit en recopiant son cadet
    // de trois fiches et en enroulant une boucle autour.
    //
    // DEUX BALAYAGES DU GROUPE, ET UN MASQUE ENTRE LES DEUX. Le premier, a la naissance, tente le
    // soin sur chacun des quatre emplacements et note dans +0x98 le bit de ceux qui l'ont refuse.
    // Le second, une image sur deux pendant quarante images, ne decore que ceux dont le bit est
    // absent : un membre que le soin n'a pas pris ne recoit pas non plus les etincelles.
    //
    // LE SOIN EST LE SOIN PARTAGE, PAS LE SOIN SIMPLE. 0x00449530 mene a HealPartyShare_0053CB30,
    // l'entree 44 — celle qui DIVISE le montant par le nombre de soignables. Appelee quatre fois de
    // suite, une par membre. Quarante-deux PM pour cela.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00449530 @ 0x00449530 — <b>poser le soin partage si la cible l'accepte</b>.
    /// Soixante et un octets, decompilee dans <c>Blaze.c</c>. Cinquieme exemplaire de la meme
    /// enveloppe que <see cref="HealTargetIfEligible_00447FE0"/> et ses sœurs : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="HealPartyShare_0053CB30"/> derriere.
    ///
    /// <para><b>Celle-ci, en revanche, est lue.</b> C'est la seule des cinq dont l'appelant regarde
    /// le resultat : <see cref="TickHealAll_004491F0"/> s'en sert pour marquer, dans son
    /// <c>+0x98</c>, les emplacements que le soin a refuses.</para>
    /// </summary>
    private static bool HealPartyShareIfEligible_00449530(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return HealPartyShare_0053CB30(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00449570 @ 0x00449570 — <b>combien d'entites d'effet vivent</b> : elle balaie les
    /// <c>0x80</c> enregistrements de <c>DAT_005f2d40</c> et compte ceux qui portent
    /// <c>0x80000000</c>. Trente et un octets, decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Son seul appelant jette le resultat.</b> <see cref="TickHealAll_004491F0"/> l'appelle en
    /// premiere instruction, range le compte dans un emplacement de pile — l'un des arguments que
    /// l'appelant avait empiles — et ne le relit jamais. La fonction ne lisant que de la memoire,
    /// l'appel entier est sans effet. L'intention lisible est un plafond au nombre d'etincelles
    /// semees, qui n'a jamais ete cable.
    /// </para>
    /// </summary>
    private static int CountLiveStatusEntities_00449570()
    {
        int live = 0;
        byte* slot = Mem.Entity(0x005f2d40);

        for (int index = 0; index < 0x80; index++, slot += 0x9c)
        {
            if ((*(uint*)slot & 0x80000000) != 0) live++;
        }

        return live;
    }

    /// <summary>
    /// Ghidra: FUN_004491f0 @ 0x004491f0 — <b>Bobos finis</b> : le sous-type <c>0x23</c>, creneau 35
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>sixieme fiche de l'ecole 1</b>. 822
    /// octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickPureBlood_00447DF0"/> sur les quatre emplacements du groupe.</b> Sa
    /// seconde moitie est le bloc de bille de Sang pur instruction pour instruction — modele
    /// <c>0x3f</c>, <c>+0x48</c> a <c>0x50000</c>, les deux memes tirages en <c>+0x32</c> et
    /// <c>+0x34</c>, rayon <c>0xc8</c> — enroule dans une boucle sur les quatre membres.
    /// </para>
    /// <para>
    /// <b>Deux balayages, et un masque entre eux.</b> Le premier, a la naissance, tente le soin sur
    /// chaque emplacement vivant et non mourant, et note dans <c>+0x98</c> le bit de ceux qui l'ont
    /// <b>refuse</b> ; les autres recoivent aussitot deux etincelles. Le second, une image sur deux
    /// pendant quarante images, saute les emplacements dont le bit est pose : un membre que le soin
    /// n'a pas pris n'est pas decore non plus.
    /// </para>
    /// <para>
    /// <b>Le soin est le soin PARTAGE.</b> <see cref="HealPartyShareIfEligible_00449530"/> mene a
    /// l'entree 44, celle qui divise le montant par le nombre de soignables — et elle est appelee
    /// quatre fois de suite, une par emplacement. Sang pur, lui, appelle le soin simple une fois.
    /// </para>
    /// <para>
    /// <b>Il recopie sa paire famille/indice sur le lanceur a chaque tour de boucle</b>, et non une
    /// fois avant : quatre ecritures identiques, dont trois sans objet.
    /// </para>
    /// <para>
    /// <b>LB-021 — une des deux etincelles du second balayage est commandee par de la pile
    /// morte.</b> Le prologue reserve huit octets de locaux dont un seul est ecrit ; l'autre est lu
    /// une fois, en mot, et compare a <c>0x3c</c> pour decider d'un semis. Voir
    /// <c>Documentation/Reference/legacy-bug.md</c>. Le port declare ce mot a zero, ce qui prend la
    /// branche a tous les coups.
    /// </para>
    /// </summary>
    private static void TickHealAll_004491F0(byte* record)
    {
        byte* e = record;

        // Ghidra: `call 00449570h ; mov [esp+20h],eax` — le compte est range dans un emplacement
        // de pile et jamais relu. L'appel ne lit que de la memoire : il est sans effet.
        CountLiveStatusEntities_00449570();

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_00449222
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0x50;      // le rayon que l'etincelle 0xa9 tirera
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x9a) = 1;
            *(short*)(e + 0x98) = 0;         // le masque des emplacements qui refuseront

            // Ghidra: LAB_00449283 — le premier balayage : le soin, quatre fois.
            byte* slot = Inp.s_SlotFlags_005e93c0;

            for (int member = 0; member < 4; member++, slot += 0x9c)
            {
                uint flags = *(uint*)slot;

                if ((flags & 0x80000000) == 0) continue;
                if ((flags & 0x50000000) != 0) continue;

                *(byte**)(e + 0x78) = slot;

                // La paire famille/indice passe au lanceur — a chaque tour, et non une fois.
                byte* caster = *(byte**)(e + 0x74);
                caster[6] = e[6];
                caster[7] = e[7];

                if (HealPartyShareIfEligible_00449530(
                        *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0))
                {
                    // Ghidra: LAB_004492f8 — refuse : on note le bit et on ne decore pas.
                    *(short*)(e + 0x98) |= (short)(1 << member);
                    continue;
                }

                *(int*)(e + 0x1c) = *(int*)(slot + 0x1c);
                *(int*)(e + 0x20) = *(int*)(slot + 0x20);
                *(int*)(e + 0x24) = *(int*)(slot + 0x24);

                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            }

            // Le decor vire au violet, et le mot de mode passe a 2 — propre a cette ecole.
            *(ushort*)Mem.Arena(0x005e9160) = 0x82;
            *(ushort*)Mem.Arena(0x005e9162) = 0x82;
            *(ushort*)Mem.Arena(0x005e9164) = 0xc8;
            *(ushort*)Mem.Arena(0x005e9166) = 2;
        }

        // Ghidra: LAB_0044934b — quatre sons, dont trois sur la meme voix.
        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x30, 0x200, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x2f, 0x653, 0x1500, 0x1500);
        if (*(short*)(e + 0x16) == 0x0c) Aud.PlaySoundEffect(0x2f, 0x800, 0x1500, 0x1500);
        if (*(short*)(e + 0x16) == 0x16) Aud.PlaySoundEffect(0x2f, 0xbfc, 0x1500, 0x1500);

        // Ghidra: LAB_004493ca — le second balayage, une image sur deux.
        if ((e[0x16] & 1) != 0)
        {
            byte* slot = Inp.s_SlotFlags_005e93c0;

            for (int member = 0; member < 4; member++, slot += 0x9c)
            {
                uint flags = *(uint*)slot;

                if ((flags & 0x80000000) == 0) continue;
                if ((flags & 0x50000000) != 0) continue;
                if (((1 << member) & (int)*(short*)(e + 0x98)) != 0) continue;

                *(short*)(e + 0x14) = 0x50;
                *(byte**)(e + 0x78) = slot;

                *(int*)(e + 0x1c) = *(int*)(slot + 0x1c);
                *(int*)(e + 0x20) = *(int*)(slot + 0x20);
                *(int*)(e + 0x24) = *(int*)(slot + 0x24);

                // LB-021 : le binaire teste ici un mot de pile que rien n'a ecrit. Declare a zero,
                // ce qui prend la branche — voir legacy-bug.md.
                short uninitialised = 0;
                if (uninitialised < 0x3c)
                {
                    SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
                }

                // Ghidra: LAB_0044944b — une etincelle de plus une fois sur sept.
                if (CrtRand() % 7 == 1)          // Ghidra: FUN_00547140
                {
                    SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
                }

                // Ghidra: LAB_00449471 — le bloc de bille de Sang pur, verbatim.
                e[0x04] = 0x3f;                            // le modele, celui des billes d'Enchant
                e[0x0a] = 9;
                e[0x0b] = 0;

                *(byte**)(e + 0x70) = *(byte**)(e + 0x78); // l'ancre : le membre soigne
                *(int*)(e + 0x44) = 0;
                *(int*)(e + 0x48) = 0x50000;               // l'ecart vertical, du cote de la bille
                *(int*)(e + 0x4c) = 0;

                *(short*)(e + 0x32) = (short)(CrtRand() % 0x400 + 0x400);   // Ghidra: FUN_00547140
                *(short*)(e + 0x34) = (short)(CrtRand() % 0x1000);          // Ghidra: FUN_00547140

                *(short*)(e + 0x14) = 0;                   // ici le champ vaut vitesse angulaire
                *(short*)(e + 0x12) = 0xc8;                // le rayon de depart
                *(short*)(e + 0x28) = 0x1fff;
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x2c) = 0x100;

                SpawnCastEntity_0047AAE0(e, 0x70, 0, -1);
            }
        }

        // Ghidra: LAB_00449508
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 0x28) EndSpellAndResetDecor_00448020(e);
    }

    // =====================================================================
    // 3EME OEIL — le trente-sixieme sort, ecole 1 indice 6, sous-type 0x38
    //
    //   0x00448EF0   LE SORT. Sous-type 0x38 de 0x00551c30, 701 octets. DECOMPILE dans Blaze.c.
    //   0x004491B0   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //
    // C'EST SONDE, ET LES DEUX FIGURENT DANS LE JEU AVEC LE MEME PRIX : deux PM, la meme forme,
    // les memes huit traits en quatre paires, le meme embrasement, la meme immobilisation du
    // lanceur en deux temps. Cinq differences, pas une de plus :
    //
    //   la teinte des traits    +0x5c = 0x0d chez lui, 0x0e chez Sonde
    //   la teinte de l'embrasement  +0x5c = 2 chez lui, 9 chez Sonde
    //   le decor                0xdc/0xa0/0xa0 chez lui, 0xa0/0xa0/0xdc chez Sonde
    //   les etincelles          UNE PAR IMAGE de la 80e a la 139e, la ou Sonde n'en seme
    //                           qu'une image sur deux — soixante contre trente
    //   l'effet                 2 (0x0053b090) chez lui, 1 (0x0053b020) chez Sonde
    //
    // ET IL REVEILLE UNE BRANCHE MORTE. Le semis d'etincelle du trait teste +0x0a contre 0x0e puis
    // contre 0x0d ; le second test etait inatteignable tant que Sonde etait le seul appelant. Il
    // est desormais celui de 3eme oeil, et ses etincelles naissent donc avec la teinte 2 la ou
    // celles de Sonde naissent avec la teinte 9. Deux sorts jumeaux, deux couleurs.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004491b0 @ 0x004491b0 — <b>poser l'effet 2 si la cible l'accepte</b>. Soixante et
    /// un octets, decompilee dans <c>Blaze.c</c>. Sixieme exemplaire de la meme enveloppe que
    /// <see cref="HealTargetIfEligible_00447FE0"/> et ses sœurs : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="ApplyStatusEffect_0053B090"/> derriere — l'entree 2, voisine immediate de
    /// l'entree 1 que Sonde emploie, de meme duree et de bit voisin.
    /// </summary>
    private static bool ApplyStatusEffectIfEligible_004491B0(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ApplyStatusEffect_0053B090(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00448ef0 @ 0x00448ef0 — <b>3eme oeil</b> : le sous-type <c>0x38</c>, creneau 56
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>septieme fiche de l'ecole 1</b>.
    /// 701 octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickProbe_004484F0"/> a cinq valeurs pres</b>, et les deux fiches coutent
    /// le meme prix : deux PM. Meme passe-plat par <c>+0x14</c>, <c>+0x16</c>, <c>+0x48</c> et
    /// <c>+0x9a</c>, memes huit traits <c>0xa2</c> en quatre paires sur les quatre quarts de tour,
    /// meme embrasement <c>0xa3</c>, meme immobilisation du lanceur posee a la naissance et effacee
    /// deux fois — a la <c>0x78</c>e image puis a la <c>0xa0</c>e.
    /// </para>
    /// <para>
    /// <b>Les cinq differences</b> : la teinte des traits (<c>+0x5c = 0x0d</c> contre <c>0x0e</c>),
    /// celle de l'embrasement (<c>2</c> contre <c>9</c>), la teinte du decor
    /// (<c>0xdc</c>/<c>0xa0</c>/<c>0xa0</c> contre <c>0xa0</c>/<c>0xa0</c>/<c>0xdc</c>), l'effet
    /// pose (2 au lieu de 1), et le <b>rythme des etincelles</b> : 3eme oeil en seme <b>une par
    /// image</b> de la 80e a la 139e, la ou Sonde teste la parite de son compteur et n'en seme
    /// qu'une sur deux. Soixante contre trente.
    /// </para>
    /// <para>
    /// <b>Il reveille une branche morte.</b> <see cref="DropProbeMote"/> teste <c>+0x0a</c> contre
    /// <c>0x0e</c> puis contre <c>0x0d</c> ; le second test etait inatteignable tant que Sonde
    /// etait le seul appelant du trait. Il est celui de 3eme oeil : ses etincelles naissent avec la
    /// teinte <c>2</c>, celles de Sonde avec la teinte <c>9</c>.
    /// </para>
    /// </summary>
    private static void TickThirdEye_00448EF0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_00448f16 — la position de la cible, sans relevement.
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x5c) = 0x0d;                  // la teinte que les traits porteront

            // Quatre paires, un quart de tour d'ecart, un tangage tire par paire.
            for (int quarter = 0; quarter < 4; quarter++)
            {
                *(short*)(e + 0x14) = (short)(*(short*)(target + 0x32) + quarter * 0x400);
                *(short*)(e + 0x16) = (short)(CrtRand() % 0x1000);   // Ghidra: FUN_00547140
                *(int*)(e + 0x48) = quarter * 0x400;

                *(short*)(e + 0x9a) = 0x18;
                SpawnCastEntity_0047AAE0(e, 0xa2, 0, -1);

                *(short*)(e + 0x9a) = 0x19;
                SpawnCastEntity_0047AAE0(e, 0xa2, 0, -1);
            }

            *(short*)(e + 0x16) = 0;                   // le champ redevient le compteur d'images
            *(int*)(e + 0x5c) = 2;                     // et celui-ci la teinte de l'embrasement

            SpawnCastEntity_0047AAE0(e, 0xa3, 0, -1);

            **(uint**)(e + 0x74) |= 0x80;              // le lanceur est immobilise

            // Le decor vire au rouge — les trois canaux dans l'autre ordre que chez Sonde.
            *(ushort*)Mem.Arena(0x005e9160) = 0xdc;
            *(ushort*)Mem.Arena(0x005e9162) = 0xa0;
            *(ushort*)Mem.Arena(0x005e9164) = 0xa0;
            *(ushort*)Mem.Arena(0x005e9166) = 2;
        }

        // Ghidra: LAB_0044911b — une etincelle a CHAQUE image, de la 80e a la 139e.
        if (*(short*)(e + 0x16) >= 0x50)
        {
            *(short*)(e + 0x14) = 0x50;                // le rayon que l'etincelle 0xa9 tirera
            *(short*)(e + 0x9a) = 2;

            if (*(short*)(e + 0x16) < 0x8c)
            {
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            }
        }

        // Ghidra: LAB_00449150
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) >= 0x78) **(uint**)(e + 0x74) &= 0xffffff7f;

        if (*(short*)(e + 0x16) >= 0xa0)
        {
            **(uint**)(e + 0x74) &= 0xffffff7f;        // deja efface vingt images plus tot
            EndSpellAndResetDecor_00448020(e);

            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ApplyStatusEffectIfEligible_004491B0(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete
        }
    }

    // =====================================================================
    // ANTIDOTE — le trente-septieme sort, ecole 1 indice 7, sous-type 0x25
    //
    //   0x00449590   LE SORT. Sous-type 0x25 de 0x00551c30, 400 octets. DECOMPILE dans Blaze.c.
    //   0x00449720   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //   0x00449760   LA BULLE, sous-type 0x8c de 0x00552230, 335 octets. TRENTE. ABSENTE de
    //                Blaze.c ; relevee sous Legacy/Raw/Blaze/FUN_00449760.asm.
    //   0x004501B0   L'AUREOLE, sous-type 0xa9 de 0x00551f30, 275 octets. UNE. ABSENTE de Blaze.c ;
    //                relevee sous Legacy/Raw/Blaze/FUN_004501B0.asm.
    //
    // ATTENTION AU NUMERO 0xa9 : CE N'EST PAS LE MEME QUE PARTOUT AILLEURS. Les sorts precedents
    // semaient un sous-type 0xa9 par SpawnStatusEffectEntity_0047AC40, donc dans la table
    // 0x00552230, ou il designe l'etincelle de la bouffee (EntityHandler_0044FB10). Antidote sonne
    // le meme numero par SpawnCastEntity_0047AAE0, donc dans la table 0x00551f30, ou il designe
    // 0x004501B0 — une aureole qui n'a rien a voir. Le sous-type ne veut rien dire sans sa table.
    //
    // L'AUREOLE EST PARTAGEE PAR TROIS FICHES : Antidote et les deux suivantes (0x004498b0 et
    // 0x00449c30) l'appellent de la meme facon. Elle est donc ecrite une fois pour trois.
    //
    // IL NE FRAPPE PAS ET NE CHERCHE RIEN. Il colle a sa cible, sonne cinq fois, seme une bulle une
    // image sur deux pendant soixante images, et pose l'effet 34 a la centieme — la seule des 48
    // entrees qui EFFACE des etats au lieu d'en poser un.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00449720 @ 0x00449720 — <b>effacer les alterations d'etat si la cible
    /// l'accepte</b>. Soixante et un octets, decompilee dans <c>Blaze.c</c>. Septieme exemplaire de
    /// la meme enveloppe : le meme <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="ClearStatusEffect_0053C740"/> derriere — l'entree 34.
    /// </summary>
    private static bool ClearStatusEffectIfEligible_00449720(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053C740(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00449760 @ 0x00449760 — <b>la bulle d'Antidote</b> : le sous-type <c>0x8c</c>,
    /// creneau 140 de <see cref="s_StaticEntityHandlers_00552230"/>. 335 octets, <b>absente de
    /// <c>Blaze.c</c></b>. <see cref="TickAntidote_00449590"/> en seme <b>trente</b>, une image sur
    /// deux, et c'est son seul appelant.
    ///
    /// <para>
    /// <b>Elle tourne autour du sort et monte.</b> Sa position n'est jamais integree : a chaque
    /// image elle vaut <c>ancre + (sin, cos)(+0x9a + +0x12) &#215; +0x5c</c> pour l'horizontale, et
    /// <c>(+0x44 + age) &lt;&lt; 0xc</c> au-dessus de l'ancre pour la hauteur — donc une montee
    /// <b>lineaire</b> d'une unite par image, sans vitesse ni gravite. L'ancre est le SORT
    /// (<c>+0x6c</c>), dont la hauteur est prise en <c>+0x60</c> et non en <c>+0x20</c>.
    /// </para>
    /// <para>
    /// <b>Trois tirages a la naissance</b> lui donnent son rayon (<c>rand % 30 + 40</c>), son cap
    /// (<c>rand % 0x1000</c>) et son decalage vertical de depart (<c>rand % 120</c>) : trente
    /// bulles, trente trajectoires.
    /// </para>
    /// <para>
    /// <b>Un seul compteur pour grandir, retrecir et mourir.</b> <c>+0x14</c> avance d'un par
    /// image ; jusqu'a <c>0x14</c> l'echelle monte de <c>0x96</c> et se plafonne a <c>0x800</c>,
    /// au-dela elle perd <c>0x96</c> et l'entite s'eteint des que l'echelle passe sous zero.
    /// Vingt images de montee, une quinzaine de retrait.
    /// </para>
    /// <para>
    /// <b>Le produit n'est pas recadre</b> — <c>imul</c> sans <c>&gt;&gt; 0xc</c>, comme chez toutes
    /// les orbites de cette ecole.
    /// </para>
    /// </summary>
    private static void TickHolyBubble_00449760(byte* record)
    {
        byte* e = record;

        byte* origin = *(byte**)(e + 0x6c);            // le SORT, pas la cible
        if (origin == null) origin = Res.s_EntityList_005de558;

        // Ghidra: LAB_00449774 — l'ancre, dont la hauteur est prise en +0x60.
        *(int*)(e + 0x50) = *(int*)(origin + 0x1c);
        *(int*)(e + 0x54) = *(int*)(origin + 0x60);
        *(int*)(e + 0x58) = *(int*)(origin + 0x24);
        *(short*)(e + 0x12) = *(short*)(origin + 0x32);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x18;   // Ghidra: DAT_00551540
            e[0x40] = 0;
            e[0x0a] = 6;
            e[0x0b] = 0;

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0;

            *(int*)(e + 0x5c) = CrtRand() % 0x1e + 0x28;          // Ghidra: FUN_00547140
            *(short*)(e + 0x9a) = (short)(CrtRand() % 0x1000);    // Ghidra: FUN_00547140

            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(int*)(e + 0x44) = CrtRand() % 0x78;                 // Ghidra: FUN_00547140
        }

        // Ghidra: LAB_004497f4 — la naissance et la premiere image sont la meme passe.
        if (*(short*)(e + 0x10) != 1) return;

        int heading = *(short*)(e + 0x9a) + *(short*)(e + 0x12);
        int radius = *(int*)(e + 0x5c);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(heading) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(heading) * radius + *(int*)(e + 0x58);

        short age = *(short*)(e + 0x14);
        *(int*)(e + 0x20) = ((*(int*)(e + 0x44) + age) << 0xc) + *(int*)(e + 0x54);

        *(short*)(e + 0x14) = (short)(age + 1);

        if (*(short*)(e + 0x14) > 0x14)
        {
            // Ghidra: 0x0044985f — le retrait.
            *(short*)(e + 0x28) -= 0x96;
            short scale = *(short*)(e + 0x28);
            *(short*)(e + 0x2a) -= 0x96;

            if (scale < 0)
            {
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2a) = 0;
                *(uint*)e &= 0x7fffffff;
            }

            return;
        }

        // Ghidra: LAB_00449889 — la montee, plafonnee.
        *(short*)(e + 0x28) += 0x96;
        *(short*)(e + 0x2a) += 0x96;

        if (*(short*)(e + 0x28) > 0x800)
        {
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
        }
    }

    /// <summary>
    /// Ghidra: FUN_004501b0 @ 0x004501b0 — <b>l'aureole des benedictions du Pretre</b> : le
    /// sous-type <c>0xa9</c>, creneau 169 de <see cref="s_StaticEntityHandlers_00551f30"/>. 275
    /// octets, <b>absente de <c>Blaze.c</c></b>. Trois fiches de l'ecole l'emploient —
    /// <see cref="TickAntidote_00449590"/> et les deux suivantes du binaire — a raison d'une chacune.
    ///
    /// <para>
    /// <b>Le numero <c>0xa9</c> ne designe pas ici ce qu'il designe ailleurs.</b> Les sorts
    /// precedents le sonnaient par <see cref="SpawnStatusEffectEntity_0047AC40"/>, donc dans la
    /// table <c>0x00552230</c>, ou <c>0xa9</c> est l'etincelle de la bouffee
    /// (<see cref="EntityHandler_0044FB10"/>). Celui-ci passe par
    /// <see cref="SpawnCastEntity_0047AAE0"/> et la table <c>0x00551f30</c>. Un sous-type ne veut
    /// rien dire sans sa table.
    /// </para>
    /// <para>
    /// <b>Elle monte de plus en plus vite, puis a vitesse constante.</b> <c>+0x2a</c> — sa demi-hauteur — croit de
    /// <c>+0x5c</c> par image jusqu'au plafond <c>+0x44</c>, et la position gagne
    /// <c>+0x2a &#215; 150</c> a chaque image : la montee accelere puis se stabilise. Les deux
    /// bornes viennent du sort, qui les pose avant de l'engendrer.
    /// </para>
    /// <para>
    /// <b><c>+0x48</c> est le compte a rebours, et le seul aiguillage.</b> Tant qu'il reste positif
    /// l'aureole s'eclaircit de dix par image et se plafonne a <c>0xb4</c> ; passe sous zero elle
    /// s'assombrit de dix et s'eteint quand le rouge touche zero. Antidote le pose a <c>0x46</c> —
    /// soixante-dix images de montee, une vingtaine d'extinction.
    /// </para>
    /// <para>
    /// <b>Elle se cale sur <c>+0x60</c> de sa cible</b>, comme la bulle et comme l'embrasement de
    /// Sonde, et non sur son <c>+0x20</c>.
    /// </para>
    /// </summary>
    private static void TickHolyHalo_004501B0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // Ghidra: LAB_004501cf
        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            e[0x04] = 5;                               // le modele
            e[0x40] = 0;
            e[0x0a] = e[0x9a];                         // la teinte, posee par le sort
            e[0x0b] = 0;

            *(short*)(e + 0x28) = *(short*)(e + 0x14); // la largeur, posee par le sort
            *(short*)(e + 0x2c) = *(short*)(e + 0x14);
            *(short*)(e + 0x2a) = 0;                   // et la hauteur part de zero

            e[0x38] = 0;                               // elle naît noire
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x34) = 0x800;
        }

        // Ghidra: LAB_00450222
        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x32) += 0x96;
        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

        // Ghidra: LAB_00450245 — la demi-hauteur croit jusqu'au plafond que le sort a pose.
        *(short*)(e + 0x2a) += *(short*)(e + 0x5c);

        if ((int)*(short*)(e + 0x2a) > *(int*)(e + 0x44))
        {
            *(short*)(e + 0x2a) = (short)*(int*)(e + 0x44);
        }

        // Ghidra: LAB_00450260 — `lea` en cascade : x3, x5, x5, x2 = x150.
        *(int*)(e + 0x20) += *(short*)(e + 0x2a) * 150;

        *(int*)(e + 0x48) -= 1;

        if (*(int*)(e + 0x48) < 0)
        {
            BrightenEntityTint_00404DE0(e, -10, -10, -10);
            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            return;
        }

        // Ghidra: LAB_0045029f
        BrightenEntityTint_00404DE0(e, 10, 10, 10);

        if (e[0x38] > 0xb4)
        {
            e[0x38] = 0xb4;
            e[0x39] = 0xb4;
            e[0x3a] = 0xb4;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00449590 @ 0x00449590 — <b>Antidote</b> : le sous-type <c>0x25</c>, creneau 37
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>huitieme fiche de l'ecole 1</b>.
    /// 400 octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne cherche rien et ne frappe rien.</b> Aucune recherche de victime au-dela du choix de
    /// cible ordinaire, aucune immobilisation du lanceur, aucune teinte de decor — le premier sort
    /// de l'ecole 1 qui laisse le decor tranquille. Il colle a sa cible, y compris en reprenant son
    /// cap <c>+0x32</c> a chaque image, et attend cent images.
    /// </para>
    /// <para>
    /// <b>Une aureole, puis trente bulles.</b> L'aureole <c>0xa9</c> naît a la premiere image, avec
    /// ses quatre reglages poses juste avant — largeur <c>0x1000</c>, plafond de hauteur
    /// <c>0x1000</c>, compte a rebours <c>0x46</c>, vitesse <c>300</c>. Les bulles <c>0x8c</c>
    /// suivent, une image sur deux jusqu'a la soixantieme.
    /// </para>
    /// <para>
    /// <b>Cinq sons, dont un qui revient neuf fois.</b> Un a la naissance, un a la premiere image,
    /// puis le meme toutes les dix images de la 2e a la 82e — le seul sort du depot qui enumere
    /// neuf valeurs de compteur en clair plutot que de tester un reste.
    /// </para>
    /// <para>
    /// <b>Il pose l'entree 34</b>, <see cref="ClearStatusEffect_0053C740"/> : la seule des 48 qui
    /// <b>efface</b> des alterations d'etat au lieu d'en poser une.
    /// </para>
    /// </summary>
    private static void TickAntidote_00449590(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_004495b6 — la position ET le cap de la cible, a chaque image.
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);
        *(short*)(e + 0x32) = *(short*)(target + 0x32);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            // Les quatre reglages de l'aureole, poses pour qu'elle les recopie.
            *(short*)(e + 0x9a) = 6;         // sa teinte
            *(short*)(e + 0x14) = 0x1000;    // sa largeur
            *(int*)(e + 0x44) = 0x1000;      // le plafond de sa hauteur
            *(int*)(e + 0x48) = 0x46;        // son compte a rebours
            *(int*)(e + 0x5c) = 300;         // sa vitesse de montee

            SpawnCastEntity_0047AAE0(e, 0xa9, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x30, 0x16a, 0x1200, 0x1200);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x10, 0x200, 0x1200, 0x1200);

        // Ghidra: 0x00449650 — les neuf valeurs sont enumerees en clair dans le binaire.
        short step = *(short*)(e + 0x16);

        if (step == 2 || step == 0x0c || step == 0x16 || step == 0x20 || step == 0x2a
            || step == 0x34 || step == 0x3e || step == 0x48 || step == 0x52)
        {
            Aud.PlaySoundEffect(0x21, 0x200, 0x1200, 0x1200);
        }

        // Une bulle une image sur deux, soixante images durant.
        if ((*(ushort*)(e + 0x16) & 1) != 0 && *(short*)(e + 0x16) < 0x3c)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x8c, 0, -1);
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 99)
        {
            // La paire famille/indice passe au lanceur : c'est SA fiche que l'effet relira.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ClearStatusEffectIfEligible_00449720(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete

            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // LOURDES — le trente-huitieme sort, ecole 1 indice 8, sous-type 0x26
    //
    //   0x004498B0   LE SORT. Sous-type 0x26 de 0x00551c30, 384 octets. DECOMPILE dans Blaze.c.
    //   0x00449A30   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //   0x00449A70   LE GRAIN, sous-type 0x8c de 0x00551f30, 435 octets. SOIXANTE. ABSENT de
    //                Blaze.c ; releve sous Legacy/Raw/Blaze/FUN_00449A70.asm.
    //
    // ET LE MEME PIEGE QU'ANTIDOTE, UN SORT PLUS TARD. Antidote seme un sous-type 0x8c par
    // SpawnStatusEffectEntity_0047AC40 — table 0x00552230, ou 0x8c est la bulle 0x00449760.
    // Lourdes seme le MEME NUMERO par SpawnCastEntity_0047AAE0 — table 0x00551f30, ou 0x8c est
    // 0x00449A70, un grain qui n'a rien a voir. Deux sorts voisins, deux corps differents sous le
    // meme numero, exactement comme le 0xa9 la fiche d'avant. La table est la moitie du sous-type.
    //
    // POUR LE RESTE, C'EST ANTIDOTE. Meme trio de tete, meme copie de la position ET du cap de la
    // cible, memes cinq reglages de l'aureole 0xa9, memes deux premiers sons, meme enumeration en
    // clair des neuf images sonores, meme fin a la centieme image. Quatre differences :
    //
    //   le son repete    0x27 a la hauteur 0x1000, contre 0x21 a 0x200
    //   l'enfant seme    le grain 0x8c de la table 0x00551f30, contre la bulle de la 0x00552230
    //   sa cadence       UNE PAR IMAGE jusqu'a la soixantieme, contre une sur deux
    //   l'effet          35 (0x0053c790), contre 34 (0x0053c740)
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00449a30 @ 0x00449a30 — <b>effacer les alterations d'etat si la cible
    /// l'accepte</b>, seconde variante. Soixante et un octets, decompilee dans <c>Blaze.c</c>.
    /// Huitieme exemplaire de la meme enveloppe : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="ClearStatusEffect_0053C790"/> derriere — l'entree 35, voisine immediate de
    /// l'entree 34 qu'Antidote emploie.
    /// </summary>
    private static bool ClearStatusEffectIfEligible_00449A30(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053C790(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00449a70 @ 0x00449a70 — <b>le grain de Lourdes</b> : le sous-type <c>0x8c</c>
    /// <b>de la table <see cref="s_StaticEntityHandlers_00551f30"/></b>, creneau 140. 435 octets,
    /// <b>absent de <c>Blaze.c</c></b>. <see cref="TickLourdes_004498B0"/> en seme <b>soixante</b>,
    /// un par image, et c'est son seul appelant.
    ///
    /// <para>
    /// <b>Ce n'est pas la bulle d'Antidote</b>, qui porte pourtant le meme numero <c>0x8c</c> —
    /// mais dans <see cref="s_StaticEntityHandlers_00552230"/>
    /// (<see cref="TickHolyBubble_00449760"/>). Le numero seul ne designe rien.
    /// </para>
    /// <para>
    /// <b>Il tombe vers le centre au lieu de tourner autour.</b> Sa position est spherique et n'est
    /// jamais integree — <c>centre + vecteur(cap +0x12, tangage +0x14, rayon +0x9a)</c> — et le
    /// rayon <b>diminue</b> de <c>+0x5c</c> par image jusqu'a zero, ou le grain s'eteint. Rayon de
    /// depart <c>rand % 200 + 100</c>, vitesse de chute <c>rand % 30 + 20</c> : il vit entre deux et
    /// quinze images selon le tirage.
    /// </para>
    /// <para>
    /// <b>Son orientation suit sa position.</b> <c>+0x32</c> vaut son cap plus <c>0xc00</c> et
    /// <c>+0x34</c> vaut <c>0x400</c> moins son tangage : le grain est toujours tourne vers le
    /// centre sur lequel il tombe.
    /// </para>
    /// <para>
    /// <b>Le bloc de naissance efface le centre qu'il vient de calculer.</b> L'en-tete pose
    /// <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> depuis l'ancre — le sort, une hauteur d'homme plus
    /// haut — puis la naissance les remplace par la position propre du grain, celle que la copie
    /// des <c>0x27</c> dwords lui a donnee. L'effet ne dure qu'une image : l'en-tete les recalcule
    /// des la suivante. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickHolyMote_00449A70(byte* record)
    {
        byte* e = record;

        byte* origin = *(byte**)(e + 0x6c);            // le SORT, pas la cible
        if (origin == null) origin = Res.s_EntityList_005de558;

        // Ghidra: LAB_00449a85
        *(int*)(e + 0x50) = *(int*)(origin + 0x1c);
        *(int*)(e + 0x54) = *(int*)(origin + 0x20) + 0x50000;
        *(int*)(e + 0x58) = *(int*)(origin + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            e[0x04] = 0;
            e[0x0a] = 3;
            e[0x0b] = 1;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);        // le cap
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x400);         // le tangage
            *(short*)(e + 0x9a) = (short)(CrtRand() % 0xc8 + 0x64);   // le rayon de depart

            *(short*)(e + 0x28) = 0xc8;
            *(short*)(e + 0x2c) = 0xc8;

            // Le centre calcule par l'en-tete est ecrase par la position propre du grain.
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            *(int*)(e + 0x5c) = CrtRand() % 0x1e + 0x14;              // la vitesse de chute

            e[0x38] = 0;                               // il naît noir
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x3e8 + 0x3e8);
            e[0x3b] &= 0xfd;
        }

        // Ghidra: LAB_00449b50 — la naissance et la premiere image sont la meme passe.
        if (*(short*)(e + 0x10) != 1) return;

        BrightenEntityTint_00404DE0(e, 30, 30, 30);

        *(short*)(e + 0x9a) -= *(short*)(e + 0x5c);

        if (*(short*)(e + 0x9a) < 0)
        {
            *(short*)(e + 0x9a) = 0;
            *(uint*)e &= 0x7fffffff;
        }

        // Ghidra: LAB_00449b8e — spherique, et le produit n'est pas recadre au dernier facteur.
        int yaw = *(short*)(e + 0x12);
        int pitch = *(short*)(e + 0x14);
        int radius = *(short*)(e + 0x9a);

        *(int*)(e + 0x1c) = (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc)
                            * (int)Winsys3d.GetSinTableEntry(yaw) + *(int*)(e + 0x50);

        *(int*)(e + 0x24) = (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc)
                            * Winsys3d.GetCosTableEntry(yaw) + *(int*)(e + 0x58);

        *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(pitch) * radius + *(int*)(e + 0x54);

        // Il regarde le centre sur lequel il tombe.
        *(short*)(e + 0x32) = (short)(*(short*)(e + 0x12) + 0xc00);
        *(short*)(e + 0x34) = (short)(0x400 - *(short*)(e + 0x14));
    }

    /// <summary>
    /// Ghidra: FUN_004498b0 @ 0x004498b0 — <b>Lourdes</b> : le sous-type <c>0x26</c>, creneau 38 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>neuvieme fiche de l'ecole 1</b>. 384
    /// octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickAntidote_00449590"/> a quatre valeurs pres</b> : meme trio de tete,
    /// meme copie de la position <b>et du cap</b> de la cible a chaque image, memes cinq reglages
    /// de l'aureole <c>0xa9</c> — teinte 6, largeur <c>0x1000</c>, plafond <c>0x1000</c>, compte a
    /// rebours <c>0x46</c>, vitesse 300 —, memes deux premiers sons, meme enumeration en clair des
    /// neuf images sonores, meme fin passe la centieme image, et le decor laisse tranquille.
    /// </para>
    /// <para>
    /// <b>Les quatre differences</b> : le son repete (<c>0x27</c> a la hauteur <c>0x1000</c> contre
    /// <c>0x21</c> a <c>0x200</c>), l'enfant seme (<see cref="TickHolyMote_00449A70"/> contre
    /// <see cref="TickHolyBubble_00449760"/>), sa cadence (<b>une par image</b> jusqu'a la
    /// soixantieme, contre une sur deux) et l'effet pose (35 au lieu de 34).
    /// </para>
    /// <para>
    /// <b>Les deux enfants portent le meme numero de sous-type</b>, <c>0x8c</c> — mais Antidote le
    /// sonne par <see cref="SpawnStatusEffectEntity_0047AC40"/> et Lourdes par
    /// <see cref="SpawnCastEntity_0047AAE0"/>, donc dans deux tables differentes. C'est le meme
    /// piege que le <c>0xa9</c> de la fiche precedente, une fiche plus loin.
    /// </para>
    /// </summary>
    private static void TickLourdes_004498B0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_004498d6 — la position ET le cap de la cible, a chaque image.
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);
        *(short*)(e + 0x32) = *(short*)(target + 0x32);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            // Les quatre reglages de l'aureole, poses pour qu'elle les recopie.
            *(short*)(e + 0x9a) = 6;         // sa teinte
            *(short*)(e + 0x14) = 0x1000;    // sa largeur
            *(int*)(e + 0x44) = 0x1000;      // le plafond de sa hauteur
            *(int*)(e + 0x48) = 0x46;        // son compte a rebours
            *(int*)(e + 0x5c) = 300;         // sa vitesse de montee

            SpawnCastEntity_0047AAE0(e, 0xa9, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x30, 0x16a, 0x1200, 0x1200);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x10, 0x200, 0x1200, 0x1200);

        // Ghidra: 0x00449970 — les neuf valeurs sont enumerees en clair, comme chez Antidote.
        short step = *(short*)(e + 0x16);

        if (step == 2 || step == 0x0c || step == 0x16 || step == 0x20 || step == 0x2a
            || step == 0x34 || step == 0x3e || step == 0x48 || step == 0x52)
        {
            Aud.PlaySoundEffect(0x27, 0x1000, 0x1200, 0x1200);
        }

        // Un grain a CHAQUE image, soixante images durant.
        if (*(short*)(e + 0x16) < 0x3c) SpawnCastEntity_0047AAE0(e, 0x8c, 0, -1);

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 99)
        {
            // La paire famille/indice passe au lanceur : c'est SA fiche que l'effet relira.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ClearStatusEffectIfEligible_00449A30(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete

            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // ANTISORT — le trente-neuvieme sort, ecole 1 indice 9, sous-type 0x27
    //
    //   0x00449C30   LE SORT. Sous-type 0x27 de 0x00551c30, 448 octets. DECOMPILE dans Blaze.c.
    //   0x00449DF0   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //   0x00449E30   LA SPIRALE, sous-type 0x8d de 0x00552230, 341 octets. VINGT, par paires.
    //                ABSENTE de Blaze.c ; relevee sous Legacy/Raw/Blaze/FUN_00449E30.asm.
    //
    // LA TROISIEME DU MEME MOULE, ET LA DERNIERE : Antidote, Lourdes et Antisort partagent
    // l'aureole 0xa9, les memes cinq reglages, les memes deux premiers sons, la meme fin a la
    // centieme image et le meme decor laisse tranquille. Chacune a son enfant a elle, son son a
    // elle et son entree d'effet a elle — 34, 35 puis 36, trois entrees consecutives de la table
    // des 48, toutes trois de la famille qui EFFACE des etats.
    //
    // CELUI-CI SE DISTINGUE PAR SON CALENDRIER. Ses vingt spirales ne sortent pas au debut mais
    // AU MILIEU : une paire toutes les deux images, de la 21e a la 39e seulement, precedee de deux
    // sons a la 21e et a la 23e. Les vingt premieres images et les soixante dernieres ne montrent
    // que l'aureole.
    //
    // ET SA SPIRALE EST LA COUSINE DE CELLE DE RENFORT. TickHolySpark_0044FE00 et 0x00449E30 ont
    // la meme forme au demi-octet pres — meme chute de 0xfa000 par 0xa000, meme descripteur
    // 0x00551550, meme orbite non recadree, meme +0x14 charniere entre croissance et retrait. La
    // difference tient en une ligne : ici le RAYON se referme d'une unite par image, plancher a
    // 0x28. La spirale de Renfort tourne a rayon fixe ; celle-ci se resserre.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00449df0 @ 0x00449df0 — <b>effacer les alterations d'etat si la cible
    /// l'accepte</b>, troisieme variante. Soixante et un octets, decompilee dans <c>Blaze.c</c>.
    /// Neuvieme exemplaire de la meme enveloppe : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="ClearStatusEffect_0053C7E0"/> derriere — l'entree 36, la troisieme et derniere
    /// des trois entrees consecutives que se partagent Antidote, Lourdes et Antisort.
    /// </summary>
    private static bool ClearStatusEffectIfEligible_00449DF0(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053C7E0(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00449e30 @ 0x00449e30 — <b>la spirale d'Antisort</b> : le sous-type <c>0x8d</c>,
    /// creneau 141 de <see cref="s_StaticEntityHandlers_00552230"/>. 341 octets, <b>absente de
    /// <c>Blaze.c</c></b>. <see cref="TickAntispell_00449C30"/> en seme <b>vingt</b>, par paires
    /// opposees, et c'est son seul appelant.
    ///
    /// <para>
    /// <b>C'est <see cref="TickHolySpark_0044FE00"/> avec un rayon qui se referme.</b> Meme centre
    /// d'orbite recopie de la cible <c>0x3c000</c> plus haut, meme chute depuis <c>0xfa000</c> par
    /// <c>0xa000</c> jusqu'a s'y poser, meme descripteur <c>0x00551550</c>, meme orbite non
    /// recadree, meme <c>+0x14</c> qui arbitre croissance et retrait. La seule difference de fond :
    /// ici le rayon <c>+0x44</c> <b>perd une unite par image</b>, plancher a <c>0x28</c> — la
    /// spirale de Renfort tourne a rayon fixe, celle-ci se resserre en tombant.
    /// </para>
    /// <para>
    /// <b>Le pas angulaire vient du sort</b> (<c>+0x5c = 0x78</c>), la ou celle de Renfort le
    /// recevait aussi mais a <c>0x96</c> ; l'angle se replie sur <c>[0, 0x1000[</c> par les deux
    /// memes tests.
    /// </para>
    /// <para>
    /// <b>Croissance et retrait vont du meme pas ici</b> : <c>+0x50</c> sur les deux echelles,
    /// plafond <c>0x5dc</c>, alors que celle de Renfort montait de <c>0x64</c> en X et de
    /// <c>0xc8</c> en Y vers deux plafonds distincts.
    /// </para>
    /// </summary>
    private static void TickHolySpiral_00449E30(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // Ghidra: LAB_00449e50 — le centre de l'orbite suit la cible, une hauteur d'homme plus haut.
        *(int*)(e + 0x50) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x54) = *(int*)(anchor + 0x20) + 0x3c000;
        *(int*)(e + 0x58) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            *(int*)(e + 0x20) += 0xfa000;              // elle naît tres haut

            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x28;   // Ghidra: DAT_00551550
            e[0x40] = 0;
            e[0x0a] = e[0x9a];                         // la teinte, que le sort a posee en +0x9a
            e[0x0b] = 0;

            *(short*)(e + 0x28) = 0;                   // et sans taille
            *(short*)(e + 0x2a) = 0;

            e[0x38] = 0xc8;
            e[0x39] = 0xc8;
            e[0x3a] = 0xc8;
        }

        // Ghidra: LAB_00449eab — tout ce qui suit n'a lieu que dans l'etat 1.
        if (*(short*)(e + 0x10) != 1) return;

        int height = *(int*)(e + 0x20) - 0xa000;
        *(int*)(e + 0x20) = height;
        if (height < *(int*)(e + 0x54)) *(int*)(e + 0x20) = *(int*)(e + 0x54);

        // Ghidra: LAB_00449ecd — l'orbite, sur le rayon COURANT, avant qu'il ne se referme.
        int radius = *(int*)(e + 0x44);

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(*(int*)(e + 0x48)) * radius
                            + *(int*)(e + 0x50);

        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(*(int*)(e + 0x48)) * radius
                            + *(int*)(e + 0x58);

        *(int*)(e + 0x44) = radius - 1;
        if (*(int*)(e + 0x44) < 0x28) *(int*)(e + 0x44) = 0x28;

        // Ghidra: LAB_00449f09 — l'angle avance du pas que le sort a pose, et se replie.
        int angle = *(int*)(e + 0x48) + *(int*)(e + 0x5c);
        *(int*)(e + 0x48) = angle;

        if (angle >= 0x1000) *(int*)(e + 0x48) = angle - 0x1000;

        // Ghidra: LAB_00449f25 — le repli par le bas relit le champ, il ne reprend pas `angle`.
        if (*(int*)(e + 0x48) < 0) *(int*)(e + 0x48) += 0x1000;

        // Ghidra: LAB_00449f34 — un seul compteur pour la croissance, le retrait et la mort.
        *(short*)(e + 0x14) -= 1;

        if (*(short*)(e + 0x14) >= 0)
        {
            // Ghidra: LAB_00449f5e
            *(short*)(e + 0x28) += 0x50;
            *(short*)(e + 0x2a) += 0x50;

            if (*(short*)(e + 0x28) >= 0x5dc)
            {
                *(short*)(e + 0x28) = 0x5dc;
                *(short*)(e + 0x2a) = 0x5dc;
            }

            return;
        }

        *(short*)(e + 0x28) -= 0x50;
        short shrunk = *(short*)(e + 0x28);
        *(short*)(e + 0x2a) -= 0x50;

        if (shrunk < 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00449c30 @ 0x00449c30 — <b>Antisort</b> : le sous-type <c>0x27</c>, creneau 39
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>dixieme fiche de l'ecole 1</b>.
    /// 448 octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>La troisieme du meme moule, et la derniere.</b> Comme
    /// <see cref="TickAntidote_00449590"/> et <see cref="TickLourdes_004498B0"/> : meme trio de
    /// tete, meme copie de la position <b>et du cap</b> de la cible, memes cinq reglages de
    /// l'aureole <c>0xa9</c>, memes deux premiers sons, meme fin passe la centieme image, et le
    /// decor laisse tranquille. Les trois posent trois entrees <b>consecutives</b> de la table des
    /// 48 — 34, 35, 36 —, toutes trois de la famille qui efface des etats.
    /// </para>
    /// <para>
    /// <b>Son calendrier est le seul des trois a etre creux aux deux bouts.</b> Ses vingt spirales
    /// sortent au <b>milieu</b> : une paire toutes les deux images, de la 21e a la 39e seulement,
    /// precedee de deux sons a la 21e et a la 23e. Les vingt premieres images et les soixante
    /// dernieres ne montrent que l'aureole. Antidote et Lourdes, eux, semaient sans interruption de
    /// la premiere a la soixantieme.
    /// </para>
    /// <para>
    /// <b>Les deux spirales d'une paire sont opposees</b> : <c>+0x48</c>, l'angle de depart qu'elles
    /// heritent par la copie, vaut <c>0</c> pour la premiere et <c>0x800</c> pour la seconde — le
    /// meme demi-tour que chez <see cref="TickReinforce_00448060"/>.
    /// </para>
    /// <para>
    /// <b>Il repose les cinq reglages avant chaque paire</b>, et ils ne sont pas ceux de l'aureole :
    /// <c>+0x14 = 0x1e</c> la duree de croissance, <c>+0x44 = 0x5a</c> le rayon de depart,
    /// <c>+0x5c = 0x78</c> le pas angulaire. Les memes champs servent deux especes d'enfant a
    /// quelques images d'intervalle.
    /// </para>
    /// </summary>
    private static void TickAntispell_00449C30(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_00449c56 — la position ET le cap de la cible, a chaque image.
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);
        *(short*)(e + 0x32) = *(short*)(target + 0x32);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            // Les cinq reglages de l'aureole, poses pour qu'elle les recopie.
            *(short*)(e + 0x9a) = 6;         // sa teinte
            *(short*)(e + 0x14) = 0x1000;    // sa largeur
            *(int*)(e + 0x44) = 0x1000;      // le plafond de sa hauteur
            *(int*)(e + 0x48) = 0x46;        // son compte a rebours
            *(int*)(e + 0x5c) = 300;         // sa vitesse de montee

            SpawnCastEntity_0047AAE0(e, 0xa9, 0, -1);
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x30, 0x16a, 0x1200, 0x1200);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x10, 0x200, 0x1200, 0x1200);
        if (*(short*)(e + 0x16) == 0x15) Aud.PlaySoundEffect(0x16, 0x100, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x17) Aud.PlaySoundEffect(0x16, 0x12a, 0x1000, 0x1000);

        // Ghidra: 0x00449d5f — dix paires de spirales, de la 21e image a la 39e.
        if (*(short*)(e + 0x16) > 0x14 && *(short*)(e + 0x16) < 0x28
            && (*(ushort*)(e + 0x16) & 1) != 0)
        {
            // Les memes champs, pour l'autre espece d'enfant.
            *(short*)(e + 0x9a) = 6;         // sa teinte
            *(short*)(e + 0x14) = 0x1e;      // sa duree de croissance
            *(int*)(e + 0x44) = 0x5a;        // son rayon de depart
            *(int*)(e + 0x5c) = 0x78;        // son pas angulaire

            *(int*)(e + 0x48) = 0;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x8d, 0, -1);

            *(int*)(e + 0x48) = 0x800;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x8d, 0, -1);
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 99)
        {
            // La paire famille/indice passe au lanceur : c'est SA fiche que l'effet relira.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ClearStatusEffectIfEligible_00449DF0(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete

            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // MARTEAU — le quarantieme sort, ecole 1 indice 10, sous-type 0x28
    //
    //   0x00449F90   LE SORT. Sous-type 0x28 de 0x00551c30, 176 octets. DECOMPILE dans Blaze.c.
    //   0x0044A040   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //   0x0044A080   LA MASSE, sous-type 0xa5 de 0x00551f30, 662 octets. UNE. DECOMPILEE.
    //   0x0044A320   L'ECLAT, sous-type 0x8e de 0x00552230, 386 octets. SIX, d'un coup. ABSENT de
    //                Blaze.c ; releve sous Legacy/Raw/Blaze/FUN_0044A320.asm.
    //
    // LE SORT LE PLUS COURT DE L'ECOLE — cent soixante-seize octets, et il ne fait rien d'autre
    // qu'engendrer la masse et attendre trente images. Tout le spectacle est dans l'enfant.
    //
    // ET LA MASSE ECRASE SA VICTIME POUR DE BON. Elle sauvegarde le +0x2a de la cible — son echelle
    // verticale — a la naissance, le force a 0x800 au moment de l'impact, le laisse revenir d'un
    // quart par image, et le REPOSE exactement a sa valeur d'origine en mourant. Aucun autre corps
    // du depot ne deforme sa victime.
    //
    // ET C'EST LE PREMIER SORT QUI FAIT CLIGNOTER L'ECRAN. A l'impact il pose le voile plein cadre
    // 0x005de024 en rouge sombre (0xff/0x96/0x96), et deux images plus tard le remet a zero — deux
    // images de flash, la seule ecriture de ce champ par un corps de sort.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044a040 @ 0x0044a040 — <b>effacer les alterations d'etat si la cible
    /// l'accepte</b>, quatrieme variante. Soixante et un octets, decompilee dans <c>Blaze.c</c>.
    /// Dixieme exemplaire de la meme enveloppe : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="ClearStatusEffect_0053C830"/> derriere — l'entree 37, la quatrieme d'une suite de
    /// quatre entrees consecutives (34 a 37) que se partagent Antidote, Lourdes, Antisort et
    /// Marteau.
    /// </summary>
    private static bool ClearStatusEffectIfEligible_0044A040(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053C830(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044a320 @ 0x0044a320 — <b>l'eclat de Marteau</b> : le sous-type <c>0x8e</c>,
    /// creneau 142 de <see cref="s_StaticEntityHandlers_00552230"/>. 386 octets, <b>absent de
    /// <c>Blaze.c</c></b>. <see cref="TickHammerHead_0044A080"/> en lache <b>six d'un coup</b> a
    /// l'impact, et c'est son seul appelant.
    ///
    /// <para>
    /// <b>Il part vers l'exterieur et ralentit.</b> Position spherique jamais integree — cap
    /// <c>+0x4c</c> et tangage <c>+0x48</c> tires au hasard une fois pour toutes — autour d'un
    /// centre fige a la naissance, <c>0x78000</c> au-dessus du point d'impact. Le rayon
    /// <c>+0x44</c> part de zero et gagne <c>+0x5c</c> par image, mais <c>+0x5c</c> <b>perd un
    /// quart de lui-meme a chaque image</b> : les six eclats jaillissent d'un coup puis s'arretent
    /// presque sur place. Six images suffisent a epuiser la vitesse.
    /// </para>
    /// <para>
    /// <b>Le tangage est tire dans un secteur, pas sur le tour.</b> <c>rand % 0x4b0 - 0xc8</c> :
    /// entre <c>-0xc8</c> et <c>0x3e7</c>, soit un peu plus d'un quart de tour au-dessus de
    /// l'horizontale et un peu en dessous. Les eclats montent en gerbe.
    /// </para>
    /// <para>
    /// <b>Il naît rouge et s'eteint en s'assombrissant</b> — teinte <c>ff/ff/96</c>, moins vingt
    /// sur les trois canaux par image, mort quand le rouge touche zero, soit une douzaine
    /// d'images. Son image d'atlas <c>+0x40</c> tourne sur trois, une par image.
    /// </para>
    /// </summary>
    private static void TickHammerDebris_0044A320(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x78;   // Ghidra: DAT_005515a0
            e[0x40] = 0;
            e[0x0a] = 6;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            // Le centre de la gerbe, fige a la naissance.
            *(int*)(e + 0x54) = *(int*)(e + 0x20) + 0x78000;
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);

            *(short*)(e + 0x14) = 0;

            short scale = (short)(CrtRand() % 0x400 + 0x800);     // Ghidra: FUN_00547140
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            e[0x3a] = 0x96;                            // il naît rouge
            e[0x38] = 0xff;
            e[0x39] = 0xff;

            *(int*)(e + 0x44) = 0;                                // le rayon
            *(int*)(e + 0x5c) = CrtRand() % 10 + 0x1e;            // sa vitesse
            *(int*)(e + 0x4c) = CrtRand() % 0x1000;               // le cap
            *(int*)(e + 0x48) = CrtRand() % 0x4b0 - 0xc8;         // le tangage, en secteur
        }

        // Ghidra: LAB_0044a3e2
        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x28) += 0x64;
        *(short*)(e + 0x2a) += 0x64;

        int pitch = *(int*)(e + 0x48);
        int yaw = *(int*)(e + 0x4c);
        int radius = *(int*)(e + 0x44);

        *(int*)(e + 0x1c) = (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc)
                            * (int)Winsys3d.GetSinTableEntry(yaw) + *(int*)(e + 0x50);

        *(int*)(e + 0x24) = (Winsys3d.GetCosTableEntry(pitch) * radius >> 0xc)
                            * Winsys3d.GetCosTableEntry(yaw) + *(int*)(e + 0x58);

        *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(pitch) * radius + *(int*)(e + 0x54);

        int speed = *(int*)(e + 0x5c);
        *(int*)(e + 0x44) = radius + speed;
        *(int*)(e + 0x5c) = speed - (speed >> 2);      // il perd un quart de sa vitesse par image

        *(short*)(e + 0x14) += 1;

        e[0x40] += 1;                                  // l'image d'atlas tourne sur trois
        if (e[0x40] >= 3) e[0x40] = 0;

        // Ghidra: LAB_0044a482
        BrightenEntityTint_00404DE0(e, -20, -20, -20);
        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0044a080 @ 0x0044a080 — <b>la masse de Marteau</b> : le sous-type <c>0xa5</c>,
    /// creneau 165 de <see cref="s_StaticEntityHandlers_00551f30"/>. 662 octets, <b>decompilee dans
    /// <c>Blaze.c</c></b>. <see cref="TickHammer_00449F90"/> n'en fait naitre <b>qu'une</b>, et
    /// c'est son seul appelant.
    ///
    /// <para>
    /// <b>Elle est tout le sort.</b> Le corps de Marteau ne fait que l'engendrer ; elle porte la
    /// chute, le choc, la gerbe, le flash et la deformation de la victime.
    /// </para>
    /// <para>
    /// <b>Elle se place a chaque image au-dessus et DEVANT sa cible</b> : <c>0xb4000</c> plus haut,
    /// et cent unites dans la direction ou la cible regarde — <c>sin</c> et <c>cos</c> de son
    /// <c>+0x32</c>, multiplies par cent sans recadrage.
    /// </para>
    /// <para>
    /// <b>Deux etats, et le second porte un sous-compteur.</b> Etat 1, la chute : les echelles X et
    /// Z montent de 400 par image jusqu'a <c>8000</c>, et l'orientation converge d'un quart vers
    /// celle de la cible ; l'etat passe a 2 des que la masse est pleine. Etat 2, le sejour :
    /// <c>+0x34</c> monte de 400, l'orientation converge d'un huitieme, et passe <c>0x76c</c> les
    /// trois echelles perdent 1000 par image jusqu'a la disparition.
    /// </para>
    /// <para>
    /// <b>Le sous-compteur <c>+0x12</c> cadence l'impact</b> : un son au pas 2, <b>les six eclats
    /// et le flash</b> au pas 3, un autre son au pas 4, l'extinction du flash au pas 5. Cinq images
    /// pour toute la scene.
    /// </para>
    /// <para>
    /// <b>ELLE ECRASE SA VICTIME.</b> A la naissance elle sauvegarde le <c>+0x2a</c> de la cible —
    /// son echelle verticale — dans son propre <c>+0x14</c> ; au pas 3 elle le force a
    /// <c>0x800</c> ; chaque image elle le laisse revenir d'un quart vers la valeur sauvegardee ;
    /// et en mourant elle le repose exactement. <b>Aucun autre corps du depot ne deforme sa
    /// victime.</b>
    /// </para>
    /// <para>
    /// <b>Et elle fait clignoter l'ecran</b> : au pas 3 le voile plein cadre
    /// <c>DAT_005de024</c> passe au rouge sombre
    /// (<c>ff</c>/<c>96</c>/<c>96</c>, octet de commande a 1), au pas 5 il revient a zero. Deux
    /// images de flash — la seule ecriture de ce champ par un corps de sort.
    /// </para>
    /// </summary>
    private static void TickHammerHead_0044A080(byte* record)
    {
        byte* e = record;

        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        // Ghidra: LAB_0044a096 — au-dessus et DEVANT la cible, cent unites dans son regard.
        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20) + 0xb4000;
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        int facing = *(short*)(target + 0x32);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(facing) * 100;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(facing) * 100;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x1a;                            // le modele
            e[0x0a] = 0;
            e[0x0b] = 0;

            *(short*)(e + 0x28) = 0;                   // elle naît plate
            *(short*)(e + 0x2a) = 8000;
            *(short*)(e + 0x2c) = 0;

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x32) = 0x2000;
            *(short*)(e + 0x34) = 0;

            // La hauteur de la cible, mise de cote pour lui etre rendue.
            *(short*)(e + 0x14) = *(short*)(target + 0x2a);
            *(short*)(e + 0x98) = 0;

            Aud.PlaySoundEffect(0x16, 0x195, 0x1300, 0x1300);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x28) += 400;
            *(short*)(e + 0x2c) += 400;

            *(short*)(e + 0x32) += (short)
                (((*(short*)(target + 0x32) - *(short*)(e + 0x32)) + 0x400) >> 2);

            if (*(short*)(e + 0x28) > 7999)
            {
                *(short*)(e + 0x28) = 8000;
                *(short*)(e + 0x2a) = 8000;
                *(short*)(e + 0x2c) = 8000;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x34) += 400;

        *(short*)(e + 0x32) += (short)
            (((*(short*)(target + 0x32) - *(short*)(e + 0x32)) + 0x400) >> 3);

        if (*(short*)(e + 0x34) > 0x76c)
        {
            *(short*)(e + 0x28) -= 1000;
            *(short*)(e + 0x2a) -= 1000;
            *(short*)(e + 0x2c) -= 1000;
        }

        if (*(short*)(e + 0x28) < 0)
        {
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0;
            *(uint*)e &= 0x7fffffff;

            // La cible retrouve exactement sa hauteur d'origine.
            *(short*)(target + 0x2a) = *(short*)(e + 0x14);
        }

        // Ghidra: 0x0044a1f2 — l'impact : six eclats, le flash, et la victime ecrasee.
        if (*(short*)(e + 0x12) == 3)
        {
            for (int shard = 0; shard < 6; shard++)
            {
                SpawnStatusEffectEntity_0047AC40(
                    e, (int*)(target + 0x1c), null, 0x8e, 0, -1);
            }

            Mem.s_Unknown_005de024 = 0x019696ff;       // le voile plein cadre, rouge sombre
            *(short*)(target + 0x2a) = 0x800;          // et la cible est ecrasee
        }

        // Ghidra: LAB_0044a297
        if (*(short*)(e + 0x12) == 5) Mem.s_Unknown_005de024 = 0x01000000;

        // Ghidra: LAB_0044a2b7 — la hauteur de la cible revient d'un quart par image.
        *(short*)(target + 0x2a) += (short)
            ((*(short*)(e + 0x14) - *(short*)(target + 0x2a)) >> 2);

        if (*(short*)(e + 0x12) == 2) Aud.PlaySoundEffect(0x22, 0x960, 0x0a00, 0x0a00);
        if (*(short*)(e + 0x12) == 4) Aud.PlaySoundEffect(0x04, 0x4b0, 0x1300, 0x1300);

        *(short*)(e + 0x12) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00449f90 @ 0x00449f90 — <b>Marteau</b> : le sous-type <c>0x28</c>, creneau 40 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>onzieme fiche de l'ecole 1</b>. 176
    /// octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le corps de sort le plus court de l'ecole</b>, et sans doute du depot pour une fiche
    /// jouable. Il choisit sa cible, s'y colle, engendre <see cref="TickHammerHead_0044A080"/> a la
    /// premiere image, compte trente images et pose l'entree 37. Rien d'autre : ni son, ni decor,
    /// ni immobilisation, ni semis. <b>Tout le spectacle appartient a la masse.</b>
    /// </para>
    /// <para>
    /// <b>Il meurt avant elle.</b> Trente images pour le sort, une bonne cinquantaine pour la masse
    /// — qui n'en depend pas, ayant recopie la cible dans son propre <c>+0x78</c> a la naissance.
    /// </para>
    /// </summary>
    private static void TickHammer_00449F90(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_00449fb6
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            SpawnCastEntity_0047AAE0(e, 0xa5, 0, -1);
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 0x1d)
        {
            // La paire famille/indice passe au lanceur : c'est SA fiche que l'effet relira.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ClearStatusEffectIfEligible_0044A040(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete

            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // SANG D'ANGE — le quarante et unieme sort, ecole 1 indice 11, sous-type 0x29
    //
    //   0x0044A4B0   LE SORT. Sous-type 0x29 de 0x00551c30, 419 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044A4B0.asm.
    //   0x0044A660   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //   0x0044A6A0   L'AILE, sous-type 0x8d de 0x00551f30, 256 octets. UNE. DECOMPILEE.
    //
    // MALGRE SON NOM, IL NE RESSUSCITE PAS. Il pose l'entree 45, la GUERISON COMPLETE : les points
    // de vie passent au maximum et le nombre affiche est ce qui manquait. La resurrection est
    // l'entree 46, et elle appartient a Resurection (0x0044CD00), pas a lui.
    //
    // ET IL NE SAIT PAS VISER UN MORT. Il appelle AcquireHolyTarget_00447CB0 en mode 0, le mode
    // ordinaire. Le mode 1 — celui qui efface 0x40002000 sur les quatre emplacements du groupe le
    // temps de la recherche pour qu'un cadavre soit eligible — n'a qu'un seul appelant dans tout le
    // binaire, et c'est Resurection. Verifie au desassembleur : les quatorze appels decompiles
    // passent 0, et 0x0044cd20 est le seul `push 1`.
    //
    // IL RESOUT A LA NAISSANCE, ET RECOPIE SA PAIRE FAMILLE/INDICE A LA MORT. L'ordre est l'inverse
    // de celui de toutes ses sœurs, qui recopient puis appellent. Ce n'est pas un defaut :
    // CastSpell_0053A980 a deja ecrit la paire sur le lanceur au moment de l'incantation, donc la
    // fiche lue a l'image 0 est la bonne. La recopie de la 70e image ne sert plus a ce sort-la.
    //
    // AUCUN CORPS D'ENFANT NEUF A ECRIRE AU DESASSEMBLEUR : l'aile 0x8d est decompilee et
    // l'etincelle 0xa9 est celle de la bouffee, deja ecrite.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044a660 @ 0x0044a660 — <b>guerir completement si la cible l'accepte</b>.
    /// Soixante et un octets, decompilee dans <c>Blaze.c</c>. Onzieme exemplaire de la meme
    /// enveloppe : le meme <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="HealTargetFully_0053CD20"/> derriere — l'entree 45.
    /// </summary>
    private static bool HealTargetFullyIfEligible_0044A660(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return HealTargetFully_0053CD20(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044a6a0 @ 0x0044a6a0 — <b>l'aile de Sang d'ange</b> : le sous-type <c>0x8d</c>
    /// <b>de la table <see cref="s_StaticEntityHandlers_00551f30"/></b>, creneau 141. 256 octets,
    /// <b>decompilee dans <c>Blaze.c</c></b>. <see cref="TickAngelBlood_0044A4B0"/> n'en fait
    /// naitre <b>qu'une</b>, et c'est son seul appelant.
    ///
    /// <para>
    /// <b>Ce n'est pas la spirale d'Antisort</b>, qui porte pourtant le meme numero <c>0x8d</c> —
    /// mais dans <see cref="s_StaticEntityHandlers_00552230"/>
    /// (<see cref="TickHolySpiral_00449E30"/>). Troisieme collision de numero de l'ecole, apres le
    /// <c>0xa9</c> d'Antidote et le <c>0x8c</c> de Lourdes.
    /// </para>
    /// <para>
    /// <b>Elle ne se deplace pas : elle tourne et change de couleur.</b> Sa position est celle de
    /// la cible a chaque image — <c>+0x60</c> pour la hauteur, comme toutes les entites de cette
    /// famille — et son cap <c>+0x32</c> avance de <c>+0x5c</c>, pose a 100 et jamais relu
    /// autrement.
    /// </para>
    /// <para>
    /// <b>Trois etats, et chacun est une teinte qui monte.</b> Elle naît noire ; l'etat 1 la fait
    /// virer au vert (<c>+0x0f</c> par image, plus un filet de bleu) jusqu'a ce que le vert passe
    /// 199 ; l'etat 2 echange le vert contre du bleu (<c>-10</c> et <c>+10</c>) jusqu'a saturation
    /// du bleu a <c>0xff</c> ; l'etat 3 eteint les trois canaux de huit par image, en aplatissant
    /// l'aile — la hauteur perd <c>0x28</c> pendant que la largeur et la profondeur en gagnent
    /// autant. Elle disparait quand les trois canaux sont a zero.
    /// </para>
    /// </summary>
    private static void TickAngelWing_0044A6A0(byte* record)
    {
        byte* e = record;

        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x60);    // +0x60, pas +0x20
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x26;                            // le modele
            e[0x0a] = 3;
            e[0x0b] = 1;

            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x28) = 4000;
            *(short*)(e + 0x2c) = 4000;
            *(short*)(e + 0x2a) = 3000;

            e[0x38] = 0;                               // elle naît noire
            e[0x39] = 0;
            e[0x3a] = 0;

            *(int*)(e + 0x5c) = 100;                   // sa vitesse de rotation
        }

        // La rotation avance dans tous les etats, y compris a la naissance.
        *(short*)(e + 0x32) += (short)*(int*)(e + 0x5c);

        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, 0, 0x0f, 1);
            if (e[0x39] > 199) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            BrightenEntityTint_00404DE0(e, 0, -10, 10);
            if (e[0x3a] == 0xff) *(short*)(e + 0x10) = 3;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            BrightenEntityTint_00404DE0(e, -8, -8, -8);

            *(short*)(e + 0x2a) -= 0x28;               // elle s'aplatit en s'eteignant
            *(short*)(e + 0x28) += 0x28;
            *(short*)(e + 0x2c) += 0x28;

            if (e[0x38] == 0 && e[0x39] == 0 && e[0x3a] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044a4b0 @ 0x0044a4b0 — <b>Sang d'ange</b> : le sous-type <c>0x29</c>, creneau
    /// 41 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>douzieme fiche de l'ecole
    /// 1</b>. 419 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Malgre son nom, il ne ressuscite pas.</b> Il pose l'entree 45,
    /// <see cref="HealTargetFully_0053CD20"/> — les points de vie au maximum, le nombre affiche
    /// etant ce qui manquait. La resurrection est l'entree 46
    /// (<see cref="ReviveTarget_0053CD80"/>), et elle appartient a Resurection
    /// (<c>0x0044CD00</c>).
    /// </para>
    /// <para>
    /// <b>Et il ne sait pas viser un mort.</b> Il appelle
    /// <see cref="AcquireHolyTarget_00447CB0"/> en <b>mode 0</b>. Le mode 1 — celui qui efface
    /// <c>0x40002000</c> sur les quatre emplacements du groupe le temps de la recherche, pour
    /// qu'un cadavre soit eligible — n'a qu'un appelant dans tout le binaire, et c'est
    /// Resurection : les quatorze appels decompiles passent <c>0</c>, et <c>0x0044cd20</c> est le
    /// seul <c>push 1</c>.
    /// </para>
    /// <para>
    /// <b>Il resout a la naissance et recopie sa paire famille/indice a la mort</b>, l'ordre
    /// inverse de celui de toutes ses sœurs. Sans consequence :
    /// <see cref="CastSpell_0053A980"/> a deja ecrit cette paire sur le lanceur au moment de
    /// l'incantation, donc la fiche lue a l'image 0 est la bonne. La recopie de la 70e image ne
    /// sert plus a ce sort-la.
    /// </para>
    /// <para>
    /// <b>Une aile, cinq etincelles, puis une etincelle une image sur deux</b> pendant soixante-dix
    /// images. Trois sons a la 0e, la 1re et la 3e. C'est tout : ni decor teinte, ni immobilisation
    /// du lanceur.
    /// </para>
    /// </summary>
    private static void TickAngelBlood_0044A4B0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_0044a4d8
        byte* target = *(byte**)(e + 0x78);
        if (target == null) target = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(target + 0x1c);
        *(int*)(e + 0x20) = *(int*)(target + 0x20);
        *(int*)(e + 0x24) = *(int*)(target + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = 0x50;      // le rayon que l'etincelle 0xa9 tirera
            *(short*)(e + 0x9a) = 1;

            SpawnCastEntity_0047AAE0(e, 0x8d, 0, -1);

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);

            // L'effet est pose DES LA NAISSANCE, avant toute recopie de paire.
            HealTargetFullyIfEligible_0044A660(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete
        }

        // Ghidra: LAB_0044a5ac
        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x2c, 0x1bf, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x30, 0x200, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 3) Aud.PlaySoundEffect(0x2f, 0x653, 0x1000, 0x1000);

        // Ghidra: LAB_0044a60b — une etincelle de plus a chaque image impaire.
        if ((e[0x16] & 1) != 0)
        {
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
        }

        // Ghidra: LAB_0044a625
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) >= 0x46)
        {
            // La paire famille/indice passe au lanceur — soixante-dix images APRES l'effet.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // LAPECHE — le quarante-deuxieme sort, ecole 1 indice 12, sous-type 0x2a
    //
    //   0x0044A7A0   LE SORT. Sous-type 0x2a de 0x00551c30, 224 octets. DECOMPILE dans Blaze.c.
    //   0x0044A880   LE DOME, sous-type 0x8e de 0x00551f30, 1552 octets. UN. DECOMPILE.
    //   0x004500B0   L'ETINCELLE DE PRISE, sous-type 0xa5 de 0x00552230, 288 octets. DECOMPILEE.
    //
    // IL N'A PAS D'ENVELOPPE D'EFFET. C'est la premiere fiche de l'ecole qui ne passe jamais par
    // IsEffectPairBlocked_004364B0 ni par la table des 48 : son effet EST le dome, et le dome
    // travaille directement sur les quatre tables d'entites.
    //
    // ET C'EST LE PREMIER SORT DU DEPOT QUI DURE TANT QU'ON LE PAIE. Le dome ponctionne deux points
    // au +0x14e de la fiche de personnage toutes les dix images, et s'arrete des que ce compteur
    // tombe sous deux. Trois autres conditions le coupent : la combinaison de boutons relachee, un
    // etat du +0x140 de la fiche, et la mort du lanceur.
    //
    // LE DOME EST UNE NASSE. Une fois ouvert il balaie QUATRE tables a chaque image et attrape tout
    // ce qui passe a moins de 0x136 :
    //
    //   0x005ecaa0 (0x20)  les entites de scene — repoussees : leur vitesse devient le vecteur
    //                      dome->elles divise par huit, et leur +0x96 passe a 0x10
    //   0x005f2d40 (0x80)  les entites d'effet — marquees 0x40000000, donc detruites
    //   0x005edee0 (0x80)  les entites d'incantation — idem
    //   0x005ff400 (0x10)  la quatrieme table — idem, mais sur SEIZE creneaux seulement alors
    //                      qu'elle en compte trente-deux : voir LB-022
    //
    // Chaque prise coute cent au canal bleu du dome et fait naitre l'etincelle 0xa5. Le dome
    // s'eteint quand ce bleu touche zero : ATTRAPER TROP DE CHOSES TUE LA NASSE.
    //
    // LE SORT LUI-MEME NE SAIT PAS QUAND IL FINIT. Son etat 2 — celui qui l'eteint — n'est jamais
    // pose par son propre corps : c'est le dome qui, en mourant, ecrit 2 dans le +0x10 de son pere
    // par le +0x6c. Un signal remontant, comme chez Fusion et Assaut magique.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004500b0 @ 0x004500b0 — <b>l'etincelle de prise</b> : le sous-type <c>0xa5</c>,
    /// creneau 165 de <see cref="s_StaticEntityHandlers_00552230"/>. 288 octets, <b>decompilee dans
    /// <c>Blaze.c</c></b>. <see cref="TickHolyDome_0044A880"/> en fait naitre une par chose
    /// attrapee.
    ///
    /// <para>
    /// <b>Ce n'est pas la masse de Marteau</b>, qui porte pourtant le meme numero <c>0xa5</c> —
    /// mais dans <see cref="s_StaticEntityHandlers_00551f30"/>
    /// (<see cref="TickHammerHead_0044A080"/>). Quatrieme collision de numero de l'ecole.
    /// </para>
    /// <para>
    /// <b>Elle naît a cote de ce qu'elle marque</b> : un demi-metre plus haut, puis decalee de
    /// <c>rand % 40 - 20</c> unites sur chacun des trois axes. Elle grossit de 500 et s'assombrit
    /// de dix <b>une image sur deux</b> — <c>+0x12</c> compte a rebours depuis <c>+0x14</c>, pose a
    /// 2 — et meurt a la quatrieme etape. Huit images en tout.
    /// </para>
    /// </summary>
    private static void TickHolyCatchSpark_004500B0(byte* record)
    {
        byte* e = record;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x0a] = e[0x9a];                         // la teinte, posee par le dome
            *(short*)(e + 0x14) = 2;                   // la cadence
            *(short*)(e + 0x12) = 2;

            *(short*)(e + 0x28) = 5000;
            *(short*)(e + 0x2a) = 5000;

            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x30;   // Ghidra: DAT_00551558
            e[0x40] = 0;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;

            *(int*)(e + 0x20) += 0x50000;

            // Ghidra: FUN_00547140 trois fois — un cube de quarante unites de cote.
            *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) * 0x1000;
        }

        // Le decompte n'a lieu que dans l'etat 1 — l'operateur virgule de Ghidra le dit bien.
        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) -= 1;
        if (*(short*)(e + 0x12) >= 1) return;

        *(short*)(e + 0x28) += 500;
        *(short*)(e + 0x2a) += 500;

        BrightenEntityTint_00404DE0(e, -10, -10, -10);

        byte step = (byte)(e[0x40] + 1);
        *(short*)(e + 0x12) = *(short*)(e + 0x14);
        e[0x40] = step;

        if (step == 4) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0044a880 @ 0x0044a880 — <b>le dome de Lapeche</b> : le sous-type <c>0x8e</c>
    /// <b>de la table <see cref="s_StaticEntityHandlers_00551f30"/></b>, creneau 142. 1552 octets,
    /// <b>decompilee dans <c>Blaze.c</c></b>. <see cref="TickLapeche_0044A7A0"/> n'en fait naitre
    /// <b>qu'un</b>, sur le lanceur lui-meme.
    ///
    /// <para>
    /// <b>Ce n'est pas l'eclat de Marteau</b>, qui porte pourtant le meme numero <c>0x8e</c> — mais
    /// dans <see cref="s_StaticEntityHandlers_00552230"/>
    /// (<see cref="TickHammerDebris_0044A320"/>).
    /// </para>
    /// <para>
    /// <b>Trois etats.</b> L'ouverture (1) : le dome monte de <c>0xc8000</c> au-dessus du lanceur et
    /// redescend d'un neuvieme de <c>0x1000</c> par image en s'elargissant de 500, jusqu'a
    /// <c>11000</c> sur les trois axes ; il vire au bleu en chemin. La tenue (2) : il colle au
    /// lanceur, sa teinte <b>bat</b> — les trois canaux valent <c>sin(+0x12) &gt;&gt; 7 - 0x38</c>,
    /// l'angle avancant de 300 par image — et c'est la qu'il attrape. La fermeture (3) : les trois
    /// canaux perdent dix par image, et quand le bleu touche zero le dome meurt <b>et pose l'etat 2
    /// dans le <c>+0x10</c> de son pere</b>, ce qui eteint le sort.
    /// </para>
    /// <para>
    /// <b>Il se paie en points de magie.</b> Toutes les dix images — sur le compteur global
    /// <c>0x005f2cf0</c> — il retire deux au <c>+0x14e</c> de la fiche de personnage du lanceur, et
    /// passe a l'etat 3 des que ce compteur tombe sous deux. Deux autres conditions le coupent : le
    /// masque de boutons de la fiche (<c>+0x1fc4</c> contre l'entree
    /// <c>s_InputStateBase_00604a20 + creneau * 0x10</c>) tombe a zero — le joueur a relache la
    /// combinaison —, et le lanceur meurt ou porte l'un des bits <c>0x3e8</c> de son
    /// <c>+0x140</c>.
    /// </para>
    /// <para>
    /// <b>La nasse balaie quatre tables.</b> Les <c>0x20</c> entites de scene sont
    /// <b>repoussees</b> — leur vitesse <c>+0x50</c>/<c>+0x58</c> devient le vecteur dome vers elles
    /// divise par huit, leur <c>+0x96</c> passe a <c>0x10</c> et leur mot d'etat perd <c>0x400</c>
    /// pour gagner <c>0x40</c>. Les trois tables d'effet (<c>0x005f2d40</c>, <c>0x005edee0</c>,
    /// <c>0x005ff400</c>) sont simplement <b>marquees <c>0x40000000</c></b>, c'est-a-dire
    /// detruites. Le rayon est le meme partout : <c>0x136</c> en trois dimensions.
    /// </para>
    /// <para>
    /// <b>Chaque prise coute cent au canal bleu</b>, et le dome s'eteint quand ce bleu touche zero :
    /// attraper trop de choses tue la nasse. C'est la seule limite en dehors des points de magie.
    /// </para>
    /// <para>
    /// <b>Il appelle le sinus trois fois avec le meme argument</b> pour ses trois canaux, la ou une
    /// seule aurait suffi. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickHolyDome_0044A880(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 1;                               // le modele
            e[0x0a] = 1;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            e[0x38] = 0;                               // il naît noir
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x28) = 500;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 500;

            // Le binaire refait ici le repli sur DAT_005de558, sur le pointeur d'origine.
            byte* birth = *(byte**)(e + 0x78);
            if (birth == null) birth = Res.s_EntityList_005de558;

            *(int*)(e + 0x1c) = *(int*)(birth + 0x1c);
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x20) = *(int*)(birth + 0x60) + 0xc8000;
            *(int*)(e + 0x24) = *(int*)(birth + 0x24);

            **(uint**)(e + 0x74) |= 0x80;              // le lanceur est immobilise
        }

        // La rotation avance dans tous les etats.
        *(short*)(e + 0x32) += 600;
        if (*(short*)(e + 0x32) > 0xfff) *(short*)(e + 0x32) -= 0x1000;

        if (*(short*)(e + 0x10) == 1)
        {
            byte* rising = *(byte**)(e + 0x78);
            if (rising == null) rising = Res.s_EntityList_005de558;

            *(int*)(e + 0x1c) = *(int*)(rising + 0x1c);
            *(int*)(e + 0x24) = *(int*)(rising + 0x24);

            *(short*)(e + 0x28) += 500;
            *(short*)(e + 0x2c) += 500;

            if (*(short*)(e + 0x28) > 10999)
            {
                *(short*)(e + 0x28) = 11000;
                *(short*)(e + 0x2c) = 11000;
            }

            *(short*)(e + 0x2a) += 500;
            *(short*)(e + 0x12) += 9;

            *(int*)(e + 0x20) =
                *(int*)(rising + 0x60) + *(short*)(e + 0x12) * -0x1000 + 0xc8000;

            if (*(short*)(e + 0x2a) > 10999)
            {
                *(short*)(e + 0x2a) = 11000;
                *(int*)(e + 0x20) = *(int*)(rising + 0x60);
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }

            BrightenEntityTint_00404DE0(e, 0x28, 0x28, 0x3c);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            byte* held = *(byte**)(e + 0x78);
            if (held == null) held = Res.s_EntityList_005de558;

            *(int*)(e + 0x1c) = *(int*)(held + 0x1c);
            *(int*)(e + 0x20) = *(int*)(held + 0x60);
            *(int*)(e + 0x24) = *(int*)(held + 0x24);

            // Ghidra: trois appels a Ordinal_451 avec le MEME argument, un par canal.
            int beat = *(short*)(e + 0x12);
            e[0x38] = (byte)(sbyte)(((int)Winsys3d.GetSinTableEntry(beat) >> 7) - 0x38);
            e[0x39] = (byte)(sbyte)(((int)Winsys3d.GetSinTableEntry(beat) >> 7) - 0x38);
            e[0x3a] = (byte)(sbyte)(((int)Winsys3d.GetSinTableEntry(beat) >> 7) - 0x38);

            *(short*)(e + 0x12) += 300;
            if (*(short*)(e + 0x12) > 0xfff) *(short*)(e + 0x12) -= 0x1000;

            // Le bit 0x800000 du DOME, pas du lanceur : c'est la copie du mot d'etat de celui-ci.
            if ((*(uint*)e & 0x800000) != 0)
            {
                uint* caster = *(uint**)(e + 0x74);
                *caster |= 0x80;

                uint slot = ((byte*)caster)[8];
                byte* sheet = GetMainMemPtr() + (slot + 0x78) * 0x2000;

                // Le masque de boutons tenu : `&DAT_00604a20 + n * 4` en pseudo-C vaut n * 0x10 en
                // octets — le symbole est type undefined4. Meme piege que PollInputAndAudio.
                ushort buttons = *(ushort*)(Inp.s_InputStateBase_00604a20
                                         + Inp.s_PlayerSlots_005e9188[slot * 0x4c] * 0x10);

                if ((*(ushort*)(sheet + 0x1fc4) & buttons) == 0)
                {
                    *(short*)(e + 0x10) = 3;
                    *caster &= 0xffffff7f;
                }

                if ((*(uint*)(sheet + 0x140) & 0x3e8) != 0
                    || (*(uint*)(Inp.s_SlotFlags_005e93c0 + slot * 0x9c) & 0x40000000) != 0)
                {
                    *(short*)(e + 0x10) = 3;
                    **(uint**)(e + 0x74) &= 0xffffff7f;
                }

                // Ghidra: `div` non signe sur DAT_005f2cf0, le compteur d'images du monde.
                if (*(uint*)Mem.Entity(0x005f2cf0) % 10 == 0)
                {
                    if (*(short*)(sheet + 0x14e) < 2)
                    {
                        *(short*)(sheet + 0x14e) = 0;
                        *(short*)(e + 0x10) = 3;
                        **(uint**)(e + 0x74) &= 0xffffff7f;
                    }
                    else
                    {
                        *(short*)(sheet + 0x14e) -= 2;
                    }
                }
            }

            uint* owner = *(uint**)(e + 0x74);
            if (owner == null) owner = (uint*)Res.s_EntityList_005de558;

            if ((*owner & 0x800000) != 0)
            {
                SweepDomeCatch(e, Mem.Entity(0x005ecaa0), 0x20, true);
                SweepDomeCatch(e, Mem.Entity(0x005f2d40), 0x80, false);
                SweepDomeCatch(e, Mem.Entity(0x005edee0), 0x80, false);
                SweepDomeCatch(e, Mem.Entity(0x005ff400), 0x10, false);   // 0x10, pas 0x20 : LB-022
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            byte* closing = *(byte**)(e + 0x78);
            if (closing == null) closing = Res.s_EntityList_005de558;

            *(int*)(e + 0x1c) = *(int*)(closing + 0x1c);
            *(int*)(e + 0x20) = *(int*)(closing + 0x60);
            *(int*)(e + 0x24) = *(int*)(closing + 0x24);

            BrightenEntityTint_00404DE0(e, -10, -10, -10);

            if (e[0x3a] == 0)
            {
                *(uint*)e &= 0x7fffffff;

                // Le signal remontant : le pere passe a l'etat 2, donc s'eteint.
                byte* parent = *(byte**)(e + 0x6c);
                if (parent == null) parent = Res.s_EntityList_005de558;

                *(short*)(parent + 0x10) = 2;
            }
        }
    }

    /// <summary>
    /// Un des quatre balayages de la nasse de <see cref="TickHolyDome_0044A880"/>, ecrits en clair
    /// et presque a l'identique dans le binaire. Le rayon est <c>0x136</c> en trois dimensions dans
    /// les quatre cas, et chaque prise coute cent au canal bleu du dome.
    ///
    /// <para>
    /// <paramref name="sceneEntities"/> separe les deux formes. La table des entites de scene
    /// exige <c>0x80000000</c>, <b>pas</b> <c>0x40000000</c> et <c>0x400000</c>, et la prise
    /// <b>repousse</b> : vitesse <c>+0x50</c>/<c>+0x58</c> egale au vecteur dome vers l'entite
    /// divise par huit, <c>+0x96</c> a <c>0x10</c>, mot d'etat <c>&amp; ~0x400 | 0x40</c>. Les trois
    /// tables d'effet exigent <c>0x80000000</c>, <b>pas</b> <c>0x800</c>, puis <c>0x400000</c>,
    /// <c>0x200</c>, <c>0x400</c> et <c>0x80</c>, et la prise se contente de poser
    /// <c>0x40000000</c>.
    /// </para>
    /// </summary>
    private static void SweepDomeCatch(byte* e, byte* table, int count, bool sceneEntities)
    {
        byte* slot = table;

        for (int index = 0; index < count; index++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;

            if ((flags & 0x80000000) == 0) continue;

            if (sceneEntities)
            {
                if ((flags & 0x40000000) != 0) continue;
                if ((flags & 0x400000) == 0) continue;
            }
            else
            {
                if ((flags & 0x800) != 0) continue;
                if ((flags & 0x400000) == 0) continue;
                if ((flags & 0x200) == 0) continue;
                if ((flags & 0x400) == 0) continue;
                if ((flags & 0x80) == 0) continue;
            }

            int dx = (*(int*)(e + 0x1c) - *(int*)(slot + 0x1c)) >> 0xc;
            int dy = (*(int*)(e + 0x20) - *(int*)(slot + 0x20)) >> 0xc;
            int dz = (*(int*)(e + 0x24) - *(int*)(slot + 0x24)) >> 0xc;

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x136) continue;

            e[0x3a] = (byte)(e[0x3a] - 100);           // chaque prise coute cent au bleu
            *(short*)(e + 0x9a) = 9;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(slot + 0x1c), null, 0xa5, 0, -1);

            if (sceneEntities)
            {
                *(int*)(slot + 0x50) = (*(int*)(slot + 0x1c) - *(int*)(e + 0x1c)) >> 3;
                *(int*)(slot + 0x58) = (*(int*)(slot + 0x24) - *(int*)(e + 0x24)) >> 3;
                *(short*)(slot + 0x96) = 0x10;
                *(uint*)slot = (*(uint*)slot & 0xfffffbff) | 0x40;
            }
            else
            {
                *(uint*)slot |= 0x40000000;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044a7a0 @ 0x0044a7a0 — <b>Lapeche</b> : le sous-type <c>0x2a</c>, creneau 42 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>treizieme fiche de l'ecole 1</b>. 224
    /// octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il vise le lanceur.</b> Il appelle <see cref="AcquireHolyTarget_00447CB0"/> comme les
    /// autres, puis <b>ecrase le resultat</b> par <c>+0x78 = +0x74</c> : la cible est toujours celui
    /// qui lance. La recherche ne sert a rien ; elle est faite quand meme.
    /// </para>
    /// <para>
    /// <b>Il n'a pas d'enveloppe d'effet.</b> Premiere fiche de l'ecole a ne jamais passer par
    /// <see cref="IsEffectPairBlocked_004364B0"/> ni par la table des 48 : son effet <b>est</b> le
    /// dome, et celui-ci travaille directement sur les quatre tables d'entites.
    /// </para>
    /// <para>
    /// <b>Il ne sait pas quand il finit.</b> Son etat 2, celui qui l'eteint, n'est jamais pose par
    /// son propre corps : c'est <see cref="TickHolyDome_0044A880"/> qui, en mourant, l'ecrit dans
    /// le <c>+0x10</c> de son pere par le <c>+0x6c</c>. Un signal remontant, comme chez Fusion et
    /// Assaut magique.
    /// </para>
    /// <para>
    /// Trois sons a la naissance, aux images 0, 1 et 2, et rien d'autre.
    /// </para>
    /// </summary>
    private static void TickLapeche_0044A7A0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0)
        {
            AcquireHolyTarget_00447CB0(e, 0);
            *(byte**)(e + 0x78) = *(byte**)(e + 0x74);   // et la recherche est ecrasee

            // Ghidra teste a nouveau +0x10 ici ; AcquireHolyTarget ne l'ecrit pas, donc il vaut
            // toujours zero. Transcrit tel quel.
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                SpawnCastEntity_0047AAE0(e, 0x8e, 0, -1);
                *(short*)(e + 0x16) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x23, 0x653, 0x1000, 0x1000);
            if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x16, 0x155, 0x1000, 0x1000);
            if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x0f, 0x2ff, 0x1000, 0x1000);

            *(short*)(e + 0x16) += 1;
        }

        // L'etat 2 ne vient que du dome.
        if (*(short*)(e + 0x10) == 2) EndSpellAndResetDecor_00448020(e);
    }

    // =====================================================================
    // PRIERE — le quarante-troisieme sort, ecole 1 indice 13, sous-type 0x2b
    //
    //   0x0044AE90   LE SORT. Sous-type 0x2b de 0x00551c30, 224 octets. DECOMPILE dans Blaze.c.
    //   0x0044AF70   LE DOME, sous-type 0x8f de 0x00551f30, 1440 octets. UN. DECOMPILE.
    //
    // C'EST LAPECHE, ET LES DEUX NASSES SONT COMPLEMENTAIRES. Le dome de Lapeche attrape les
    // entites d'effet qui n'ont PAS le bit 0x800 ; celui de Priere attrape celles qui l'ONT. Deux
    // sorts au meme prix (22 PM), a la meme combinaison de longueur, poses cote a cote dans le
    // binaire, et qui se partagent exactement le champ des cibles. Aucun autre couple du depot ne
    // fait cela.
    //
    // ET IL NE REPOUSSE PAS LES MONSTRES, IL LES NETTOIE. La ou Lapeche donne aux entites de scene
    // une vitesse fuyante et une etincelle, Priere leur retire simplement les bits 0x880 de leur
    // mot d'etat, sans rien faire naitre. Le reste — les trois tables d'effet — est traite pareil :
    // marque 0x40000000 et une etincelle 0xa5 par prise.
    //
    // LES CINQ DIFFERENCES, ET RIEN D'AUTRE :
    //
    //   +0x0a           2 chez lui, 1 chez Lapeche — la teinte du dome
    //   +0x9a           2 chez lui, 9 chez Lapeche — la teinte des etincelles de prise
    //   le troisieme son  0x31 chez lui, 0x0f chez Lapeche
    //   le filtre       0x800 EXIGE chez lui, INTERDIT chez Lapeche
    //   la 4e table     0x20 creneaux chez lui, 0x10 chez Lapeche — voir LB-022
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044af70 @ 0x0044af70 — <b>le dome de Priere</b> : le sous-type <c>0x8f</c>,
    /// creneau 143 de <see cref="s_StaticEntityHandlers_00551f30"/>. 1440 octets, <b>decompilee
    /// dans <c>Blaze.c</c></b>. <see cref="TickPrayer_0044AE90"/> n'en fait naitre <b>qu'un</b>,
    /// sur le lanceur lui-meme.
    ///
    /// <para>
    /// <b>C'est <see cref="TickHolyDome_0044A880"/> a quelques valeurs pres</b> : meme ouverture en
    /// trois etats, meme battement de teinte par <c>sin(+0x12)</c>, meme ponction de deux points
    /// de magie toutes les dix images, memes trois conditions d'arret, meme signal remontant qui
    /// eteint le sort en mourant.
    /// </para>
    /// <para>
    /// <b>Mais les deux nasses sont complementaires.</b> Sur les trois tables d'effet, Lapeche
    /// exige que <c>0x800</c> soit <b>absent</b> et demande en plus <c>0x200</c> et <c>0x400</c> ;
    /// Priere exige que <c>0x800</c> soit <b>present</b> et ne demande rien d'autre que
    /// <c>0x400000</c> et <c>0x80</c>. Les deux sorts se partagent le champ des cibles.
    /// </para>
    /// <para>
    /// <b>Et il ne repousse pas les entites de scene, il les nettoie</b> : il leur retire les bits
    /// <c>0x880</c> (<c>&amp; 0xfffff77f</c>) et ne fait rien naitre, la ou Lapeche leur donne une
    /// vitesse fuyante, un <c>+0x96</c> a <c>0x10</c> et une etincelle. Son filtre est aussi plus
    /// large d'un cran : il ne teste pas <c>0x400000</c>.
    /// </para>
    /// <para>
    /// <b>Il balaie la quatrieme table sur <c>0x20</c> creneaux</b>, la ou Lapeche s'arrete a
    /// <c>0x10</c> — voir <c>LB-022</c> dans <c>Documentation/Reference/legacy-bug.md</c>.
    /// </para>
    /// <para>
    /// <b>Son en-tete pose la hauteur a chaque image</b> (<c>cible[+0x60] + 0xc8000</c>), la ou
    /// celui de Lapeche ne la posait qu'a la naissance. Les etats 1 a 3 la reecrivent aussitot, donc
    /// cela ne se voit que sur l'image ou aucun etat ne s'applique.
    /// </para>
    /// </summary>
    private static void TickHolyWard_0044AF70(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60) + 0xc8000;
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 1;                               // le modele
            e[0x0a] = 2;                               // la teinte — 1 chez Lapeche
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            e[0x38] = 0;                               // il naît noir
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x28) = 500;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 500;

            // Le binaire refait ici le repli sur DAT_005de558, sur le pointeur d'origine.
            byte* birth = *(byte**)(e + 0x78);
            if (birth == null) birth = Res.s_EntityList_005de558;

            *(int*)(e + 0x1c) = *(int*)(birth + 0x1c);
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x20) = *(int*)(birth + 0x60) + 0xc8000;
            *(int*)(e + 0x24) = *(int*)(birth + 0x24);

            **(uint**)(e + 0x74) |= 0x80;              // le lanceur est immobilise
        }

        *(short*)(e + 0x32) += 600;
        if (*(short*)(e + 0x32) > 0xfff) *(short*)(e + 0x32) -= 0x1000;

        if (*(short*)(e + 0x10) == 1)
        {
            byte* rising = *(byte**)(e + 0x78);
            if (rising == null) rising = Res.s_EntityList_005de558;

            *(int*)(e + 0x1c) = *(int*)(rising + 0x1c);
            *(int*)(e + 0x24) = *(int*)(rising + 0x24);

            *(short*)(e + 0x28) += 500;
            *(short*)(e + 0x2c) += 500;

            if (*(short*)(e + 0x28) > 10999)
            {
                *(short*)(e + 0x28) = 11000;
                *(short*)(e + 0x2c) = 11000;
            }

            *(short*)(e + 0x2a) += 500;
            *(short*)(e + 0x12) += 9;

            *(int*)(e + 0x20) =
                *(int*)(rising + 0x60) + *(short*)(e + 0x12) * -0x1000 + 0xc8000;

            if (*(short*)(e + 0x2a) > 10999)
            {
                *(short*)(e + 0x2a) = 11000;
                *(int*)(e + 0x20) = *(int*)(rising + 0x60);
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }

            BrightenEntityTint_00404DE0(e, 0x28, 0x28, 0x3c);
        }

        if (*(short*)(e + 0x10) == 2)
        {
            byte* held = *(byte**)(e + 0x78);
            if (held == null) held = Res.s_EntityList_005de558;

            *(int*)(e + 0x1c) = *(int*)(held + 0x1c);
            *(int*)(e + 0x20) = *(int*)(held + 0x60);
            *(int*)(e + 0x24) = *(int*)(held + 0x24);

            // Ghidra: trois appels a Ordinal_451 avec le MEME argument, un par canal.
            int beat = *(short*)(e + 0x12);
            e[0x38] = (byte)(sbyte)(((int)Winsys3d.GetSinTableEntry(beat) >> 7) - 0x38);
            e[0x39] = (byte)(sbyte)(((int)Winsys3d.GetSinTableEntry(beat) >> 7) - 0x38);
            e[0x3a] = (byte)(sbyte)(((int)Winsys3d.GetSinTableEntry(beat) >> 7) - 0x38);

            *(short*)(e + 0x12) += 300;
            if (*(short*)(e + 0x12) > 0xfff) *(short*)(e + 0x12) -= 0x1000;

            if ((*(uint*)e & 0x800000) != 0)
            {
                uint* caster = *(uint**)(e + 0x74);
                *caster |= 0x80;

                uint slot = ((byte*)caster)[8];
                byte* sheet = GetMainMemPtr() + (slot + 0x78) * 0x2000;

                // `&DAT_00604a20 + n * 4` en pseudo-C vaut n * 0x10 en octets — le symbole est
                // type undefined4. Verifie au desassembleur (`shl ebx,4`).
                ushort buttons = *(ushort*)(Inp.s_InputStateBase_00604a20
                                            + Inp.s_PlayerSlots_005e9188[slot * 0x4c] * 0x10);

                if ((*(ushort*)(sheet + 0x1fc4) & buttons) == 0)
                {
                    *(short*)(e + 0x10) = 3;
                    *caster &= 0xffffff7f;
                }

                if ((*(uint*)(sheet + 0x140) & 0x3e8) != 0
                    || (*(uint*)(Inp.s_SlotFlags_005e93c0 + slot * 0x9c) & 0x40000000) != 0)
                {
                    *(short*)(e + 0x10) = 3;
                    **(uint**)(e + 0x74) &= 0xffffff7f;
                }

                if (*(uint*)Mem.Entity(0x005f2cf0) % 10 == 0)
                {
                    if (*(short*)(sheet + 0x14e) < 2)
                    {
                        *(short*)(sheet + 0x14e) = 0;
                        *(short*)(e + 0x10) = 3;
                        **(uint**)(e + 0x74) &= 0xffffff7f;
                    }
                    else
                    {
                        *(short*)(sheet + 0x14e) -= 2;
                    }
                }
            }

            uint* owner = *(uint**)(e + 0x74);
            if (owner == null) owner = (uint*)Res.s_EntityList_005de558;

            if ((*owner & 0x800000) != 0)
            {
                SweepWardClean(e, Mem.Entity(0x005ecaa0), 0x20);

                SweepWardCatch(e, Mem.Entity(0x005f2d40), 0x80);
                SweepWardCatch(e, Mem.Entity(0x005edee0), 0x80);
                SweepWardCatch(e, Mem.Entity(0x005ff400), 0x20);   // 0x10 chez Lapeche : LB-022
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            byte* closing = *(byte**)(e + 0x78);
            if (closing == null) closing = Res.s_EntityList_005de558;

            *(int*)(e + 0x1c) = *(int*)(closing + 0x1c);
            *(int*)(e + 0x20) = *(int*)(closing + 0x60);
            *(int*)(e + 0x24) = *(int*)(closing + 0x24);

            BrightenEntityTint_00404DE0(e, -10, -10, -10);

            if (e[0x3a] == 0)
            {
                *(uint*)e &= 0x7fffffff;

                byte* parent = *(byte**)(e + 0x6c);
                if (parent == null) parent = Res.s_EntityList_005de558;

                *(short*)(parent + 0x10) = 2;
            }
        }
    }

    /// <summary>
    /// Le balayage des entites de scene par le dome de Priere. Deux tests seulement — vivante et
    /// pas mourante —, puis, sous <c>0x136</c> en trois dimensions, il <b>retire les bits
    /// <c>0x880</c></b> du mot d'etat et ne fait rien naitre.
    ///
    /// <para>C'est l'oppose de ce que fait <see cref="SweepDomeCatch"/> sur la meme table pour
    /// Lapeche : la, une vitesse fuyante, un <c>+0x96</c> a <c>0x10</c> et une etincelle.</para>
    /// </summary>
    private static void SweepWardClean(byte* e, byte* table, int count)
    {
        byte* slot = table;

        for (int index = 0; index < count; index++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;

            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x40000000) != 0) continue;

            int dx = (*(int*)(e + 0x1c) - *(int*)(slot + 0x1c)) >> 0xc;
            int dy = (*(int*)(e + 0x20) - *(int*)(slot + 0x20)) >> 0xc;
            int dz = (*(int*)(e + 0x24) - *(int*)(slot + 0x24)) >> 0xc;

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x136) continue;

            *(short*)(e + 0x9a) = 2;
            e[0x3a] = (byte)(e[0x3a] - 100);
            *(uint*)slot &= 0xfffff77f;
        }
    }

    /// <summary>
    /// Le balayage des trois tables d'effet par le dome de Priere. Il exige <c>0x80000000</c>,
    /// <c>0x400000</c>, <b><c>0x800</c></b> et <c>0x80</c> — la ou <see cref="SweepDomeCatch"/>
    /// exige que <c>0x800</c> soit <b>absent</b> et demande en plus <c>0x200</c> et <c>0x400</c>.
    /// <b>Les deux nasses sont complementaires.</b>
    /// </summary>
    private static void SweepWardCatch(byte* e, byte* table, int count)
    {
        byte* slot = table;

        for (int index = 0; index < count; index++, slot += 0x9c)
        {
            uint flags = *(uint*)slot;

            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x400000) == 0) continue;
            if ((flags & 0x800) == 0) continue;
            if ((flags & 0x80) == 0) continue;

            int dx = (*(int*)(e + 0x1c) - *(int*)(slot + 0x1c)) >> 0xc;
            int dy = (*(int*)(e + 0x20) - *(int*)(slot + 0x20)) >> 0xc;
            int dz = (*(int*)(e + 0x24) - *(int*)(slot + 0x24)) >> 0xc;

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x136) continue;

            e[0x3a] = (byte)(e[0x3a] - 100);
            *(short*)(e + 0x9a) = 2;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(slot + 0x1c), null, 0xa5, 0, -1);

            *(uint*)slot |= 0x40000000;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044ae90 @ 0x0044ae90 — <b>Priere</b> : le sous-type <c>0x2b</c>, creneau 43 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>quatorzieme fiche de l'ecole 1</b>.
    /// 224 octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickLapeche_0044A7A0"/> a un son pres</b> : meme recherche de cible
    /// aussitot ecrasee par <c>+0x78 = +0x74</c>, meme absence d'enveloppe d'effet, meme trio de
    /// sons a la naissance dont seul le troisieme change (<c>0x31</c> au lieu de <c>0x0f</c>), meme
    /// etat 2 qui n'est jamais pose par son propre corps mais par le dome en mourant.
    /// </para>
    /// <para>
    /// Les deux fiches coutent 22 PM, s'apprennent au meme niveau, ont des combinaisons de meme
    /// longueur et se suivent dans le binaire. Ce sont deux moities d'un meme mecanisme — voir
    /// <see cref="TickHolyWard_0044AF70"/> pour le partage des cibles.
    /// </para>
    /// </summary>
    private static void TickPrayer_0044AE90(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0)
        {
            AcquireHolyTarget_00447CB0(e, 0);
            *(byte**)(e + 0x78) = *(byte**)(e + 0x74);   // et la recherche est ecrasee

            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                SpawnCastEntity_0047AAE0(e, 0x8f, 0, -1);
                *(short*)(e + 0x16) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 1)
        {
            if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x23, 0x653, 0x1000, 0x1000);
            if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x16, 0x155, 0x1000, 0x1000);
            if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x31, 0x100, 0x1000, 0x1000);

            *(short*)(e + 0x16) += 1;
        }

        // L'etat 2 ne vient que du dome.
        if (*(short*)(e + 0x10) == 2) EndSpellAndResetDecor_00448020(e);
    }

    // =====================================================================
    // CALGON — le quarante-quatrieme sort, ecole 1 indice 14, sous-type 0x2c
    //
    //   0x0044B510   LE SORT. Sous-type 0x2c de 0x00551c30, 416 octets. DECOMPILE dans Blaze.c.
    //   0x0044B6B0   L'EFFET SOUS CONDITION. Soixante et un octets, decompile.
    //   0x0044B6F0   LA GOUTTE, sous-type 0x90 de 0x00551f30, 544 octets. DIX. DECOMPILEE.
    //   0x0044C800   L'ANNEAU, sous-type 0x95 de 0x00551f30, 145 octets. DEUX. ABSENT de Blaze.c ;
    //                releve sous Legacy/Raw/Blaze/FUN_0044C800.asm.
    //
    // IL NE SE PLACE NULLE PART. Aucune des quatre-cent-seize instructions du sort ne touche a
    // +0x1c, +0x20 ni +0x24 : il reste ou SpawnEffectEntity l'a mis, c'est-a-dire sur le lanceur.
    // C'est le premier corps de sort de l'ecole a ne pas recopier la position de sa cible a chaque
    // image — et par consequent le seul a ne jamais consulter le repli DAT_005de558.
    //
    // ET SON DECOR N'EST PAS FIXE : IL MONTE. Les trois autres sorts de l'ecole qui teintent le
    // decor y posent trois constantes. Calgon pose 0x96 sur deux canaux et calcule le troisieme,
    // (compteur + 0x4b) * 2, qui va de 0x96 a 0xd2 en trente images. La teinte vire donc lentement
    // vers le bleu pendant que le sort dure.
    //
    // ET IL SEME MEME QUAND IL A FINI. Le semis des gouttes est commande par +0x14, un compteur
    // separe de +0x16 qui n'est jamais teste contre la duree : il continue tant que le corps
    // s'execute. Ici sans effet, +0x16 arretant tout a la trentieme image.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044b6b0 @ 0x0044b6b0 — <b>effacer les alterations d'etat si la cible
    /// l'accepte</b>, cinquieme variante. Soixante et un octets, decompilee dans <c>Blaze.c</c>.
    /// Douzieme exemplaire de la meme enveloppe : le meme
    /// <see cref="IsEffectPairBlocked_004364B0"/> devant, ici
    /// <see cref="ClearStatusEffect_0053C890"/> derriere — l'entree 38, la cinquieme d'une suite de
    /// cinq entrees consecutives (34 a 38) que se partagent Antidote, Lourdes, Antisort, Marteau et
    /// Calgon.
    /// </summary>
    private static bool ClearStatusEffectIfEligible_0044B6B0(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053C890(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044b6f0 @ 0x0044b6f0 — <b>la goutte de Calgon</b> : le sous-type <c>0x90</c>,
    /// creneau 144 de <see cref="s_StaticEntityHandlers_00551f30"/>. 544 octets, <b>decompilee dans
    /// <c>Blaze.c</c></b>. <see cref="TickCalgon_0044B510"/> en seme <b>une toutes les trois
    /// images</b>, et c'est son seul appelant.
    ///
    /// <para>
    /// <b>Elle plonge puis remonte.</b> <c>+0x12</c> part de <c>-8</c> et gagne huit par image ; la
    /// hauteur avance de <c>+0x12 &#215; 0x1000</c> a chaque fois. La goutte descend donc une seule
    /// image, s'arrete, puis monte de plus en plus vite — une parabole inversee, ecrite sans
    /// gravite ni vitesse.
    /// </para>
    /// <para>
    /// <b>Elle respire pendant ce temps.</b> Ses deux echelles horizontales valent
    /// <c>sin(+0x14) / 2 + 400</c>, l'angle avancant de <c>0x60</c> par image : elle s'aplatit et
    /// se regonfle trois fois avant de disparaitre a <c>0x76b</c>, soit une trentaine d'images.
    /// L'echelle verticale, elle, ne bouge pas.
    /// </para>
    /// <para>
    /// <b>Le sinus est appele deux fois avec le meme argument</b>, une fois par echelle, la ou une
    /// seule aurait suffi — et les deux appels precedent l'avance de l'angle, donc portent bien la
    /// meme valeur. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickCalgonDroplet_0044B6F0(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 3;                               // le modele
            e[0x0a] = 0x0d;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x28) = 400;
            *(short*)(e + 0x2c) = 400;
            *(short*)(e + 0x2a) = 0x1000;

            *(short*)(e + 0x12) = -8;                  // Ghidra: 0xfff8
            *(short*)(e + 0x14) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        // Ghidra: deux appels a Ordinal_451 avec le MEME argument, tous deux AVANT l'avance.
        int breath = *(short*)(e + 0x14);

        *(short*)(e + 0x28) = (short)(((int)Winsys3d.GetSinTableEntry(breath) >> 1) + 400);
        int second = (int)Winsys3d.GetSinTableEntry(breath);

        *(short*)(e + 0x12) += 8;
        *(short*)(e + 0x14) += 0x60;

        *(short*)(e + 0x2c) = (short)((second >> 1) + 400);

        *(int*)(e + 0x20) += *(short*)(e + 0x12) * 0x1000;

        if (*(short*)(e + 0x14) > 0x76b) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0044c800 @ 0x0044c800 — <b>l'anneau de Calgon</b> : le sous-type <c>0x95</c>,
    /// creneau 149 de <see cref="s_StaticEntityHandlers_00551f30"/>. 145 octets, <b>absent de
    /// <c>Blaze.c</c></b>. <see cref="TickCalgon_0044B510"/> en fait naitre <b>deux</b> a la
    /// premiere image, a deux echelles differentes, et c'est son seul appelant.
    ///
    /// <para>
    /// <b>Il ne fait que s'allumer puis s'eteindre</b>, a hauteur de poitrine de la cible
    /// (<c>+0x60 + 0xa000</c>). Cinq images de montee a <c>+60</c> sur les trois canaux, puis une
    /// descente a <c>-8 / -8 / -6</c> jusqu'a ce que le bleu touche zero. <b>Le bleu descend moins
    /// vite que les deux autres</b> : l'anneau vire au bleu en mourant, et c'est lui qui commande
    /// la fin.
    /// </para>
    /// <para>
    /// <b>Ses trois echelles viennent du sort</b>, par la copie des <c>0x27</c> dwords : celui-ci
    /// les pose a <c>0x157c</c> avant le premier anneau et a <c>0x1450</c> avant le second, ce qui
    /// fait deux cercles concentriques. Son <c>+0x2e</c> a <c>-60</c> est en revanche le sien.
    /// </para>
    /// </summary>
    private static void TickCalgonRing_0044C800(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // Ghidra: LAB_0044c814 — +0x60, pas +0x20, comme toutes les entites de cette famille.
        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60) + 0xa000;
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x0d;                            // le modele
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            e[0x38] = 0;                               // il naît noir
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x2e) = -60;                 // Ghidra: 0xffc4
            *(short*)(e + 0x16) = 0;
        }

        // Ghidra: LAB_0044c859 — la naissance et la premiere image sont la meme passe.
        if (*(short*)(e + 0x10) != 1) return;

        if (*(short*)(e + 0x16) <= 4)
        {
            BrightenEntityTint_00404DE0(e, 60, 60, 60);
        }
        else
        {
            // Ghidra: LAB_0044c86e — le bleu descend moins vite que les deux autres.
            BrightenEntityTint_00404DE0(e, -8, -8, -6);
        }

        if (e[0x3a] == 0) *(uint*)e &= 0x7fffffff;

        *(short*)(e + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0044b510 @ 0x0044b510 — <b>Calgon</b> : le sous-type <c>0x2c</c>, creneau 44 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>quinzieme fiche de l'ecole 1</b>. 416
    /// octets, <b>decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne se place nulle part.</b> Aucune de ses instructions ne touche a <c>+0x1c</c>,
    /// <c>+0x20</c> ni <c>+0x24</c> : il reste ou <c>SpawnEffectEntity</c> l'a mis, sur le lanceur.
    /// C'est le premier corps de sort de l'ecole a ne pas recopier la position de sa cible a chaque
    /// image, et donc le seul a ne jamais consulter le repli
    /// <c>DAT_005de558</c>. Sa recherche de cible ne sert qu'a remplir
    /// <c>+0x78</c> pour ses enfants et pour l'effet.
    /// </para>
    /// <para>
    /// <b>Son decor monte.</b> Les autres fiches de l'ecole posent trois constantes dans
    /// <c>0x005e9160</c> une fois pour toutes ; Calgon les reecrit <b>a chaque image</b>, avec
    /// <c>0x96</c> sur deux canaux et <c>(compteur + 0x4b) &#215; 2</c> sur le troisieme — de
    /// <c>0x96</c> a <c>0xd2</c> en trente images. La teinte vire lentement pendant que le sort
    /// dure.
    /// </para>
    /// <para>
    /// <b>Deux compteurs, deux roles.</b> <c>+0x16</c> est l'age : il commande les quatre sons, la
    /// teinte du decor, les deux anneaux de la premiere image et la fin a la trentieme.
    /// <c>+0x14</c> ne sert qu'a espacer les gouttes — une toutes les trois images — et
    /// <b>n'est jamais compare a la duree</b> : le semis ne s'arrete que parce que le corps cesse
    /// d'etre appele.
    /// </para>
    /// <para>
    /// <b>Les deux anneaux naissent concentriques</b> : le sort pose les trois echelles a
    /// <c>0x157c</c>, engendre le premier, les repose a <c>0x1450</c> et engendre le second.
    /// </para>
    /// </summary>
    private static void TickCalgon_0044B510(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
        }

        // Une goutte toutes les trois images — +0x14 n'est jamais borne par la duree.
        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x14) += 1;

            if (*(short*)(e + 0x14) > 2)
            {
                *(short*)(e + 0x14) = 0;
                SpawnCastEntity_0047AAE0(e, 0x90, 0, -1);
            }
        }

        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x28, 0x1000, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x1c, 0x400, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 3) Aud.PlaySoundEffect(0x28, 0x653, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 4) Aud.PlaySoundEffect(0x23, 0x400, 0x1000, 0x1000);

        // Le decor, reecrit a chaque image : le troisieme canal monte avec l'age.
        *(ushort*)Mem.Arena(0x005e9160) = 0x96;
        *(ushort*)Mem.Arena(0x005e9162) = 0x96;
        *(ushort*)Mem.Arena(0x005e9166) = 2;
        *(ushort*)Mem.Arena(0x005e9164) = (ushort)((*(short*)(e + 0x16) + 0x4b) * 2);

        if (*(short*)(e + 0x16) == 0)
        {
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x34) = 0;
            *(short*)(e + 0x32) = 0x200;

            *(short*)(e + 0x28) = 0x157c;
            *(short*)(e + 0x2c) = 0x157c;
            *(short*)(e + 0x2a) = 0x157c;
            SpawnCastEntity_0047AAE0(e, 0x95, 0, -1);

            *(short*)(e + 0x28) = 0x1450;
            *(short*)(e + 0x2c) = 0x1450;
            *(short*)(e + 0x2a) = 0x1450;
            SpawnCastEntity_0047AAE0(e, 0x95, 0, -1);
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) == 0x1e)
        {
            // La paire famille/indice passe au lanceur : c'est SA fiche que l'effet relira.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ClearStatusEffectIfEligible_0044B6B0(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);   // le resultat est jete

            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // ZOMBIE — le quarante-cinquieme sort, ecole 1 indice 15, sous-type 0x2d
    //
    //   0x0044F930   LE SORT. Sous-type 0x2d de 0x00551c30, 477 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044F930.asm.
    //
    // UN SEUL CORPS, ET AUCUN ENFANT NEUF : c'est PARDON SUR TOUT LE MONDE. Meme critere d'espece
    // (statistiques[0x14] & 0x3000000), meme portee de 0x5dc depuis le LANCEUR, meme trait 0x9e —
    // TickForgiveBolt_0044F290, deja ecrit. La difference tient en une phrase : Pardon retient la
    // candidate la plus proche du sort et n'en frappe qu'une, Zombie les frappe TOUTES.
    //
    // ET IL PORTE LE MEME DEFAUT, LB-019. La branche « aucune cible » pose +0x10 = 0x63 puis tombe
    // dans l'instruction qui pose +0x10 = 2, exactement comme chez Pardon. Le defaut n'etait donc
    // pas propre a cette fiche : les deux corps ont ete ecrits sur le meme patron, defaut compris.
    //
    // ET IL EN AJOUTE UN SECOND, SANS CONSEQUENCE. Le bloc de fin appelle EndSpellAndResetDecor
    // DEUX fois : une sous la garde « +0x16 >= 0x5a », l'autre juste apres, sans garde. Le `jl` ne
    // saute que le premier appel. Cela ne se voit pas : l'etat 0x63 n'est atteint que par ce meme
    // test, donc la garde est toujours vraie quand le bloc s'execute. Transcrit tel quel.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044f930 @ 0x0044f930 — <b>Zombie</b> : le sous-type <c>0x2d</c>, creneau 45 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>seizieme fiche de l'ecole 1</b>. 477
    /// octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>C'est <see cref="TickForgive_0044F060"/> sur tout le monde.</b> Meme balayage des
    /// <c>0x20</c> entites de scene, meme resolution par <see cref="ResolveEntityBlocks"/>, meme
    /// critere d'espece <c>statistiques[0x14] &amp; 0x3000000</c>, meme portee de <c>0x5dc</c>
    /// depuis le <b>lanceur</b>, meme rattachement de la victime au pere du sort
    /// (<c>victime[0x74] = sort[0x6c]</c>), meme trait <c>0x9e</c>. La seule difference : Pardon
    /// mesure une seconde distance pour ne garder que la plus proche, Zombie n'en mesure qu'une et
    /// frappe <b>toutes</b> celles qui passent.
    /// </para>
    /// <para>
    /// <b>Il ne compte que pour savoir s'il a rate.</b> Un compteur de pile s'incremente a chaque
    /// victime ; s'il est reste nul apres le balayage, le sort prend la branche « aucune cible ».
    /// </para>
    /// <para>
    /// <b>LB-019, ici aussi.</b> Cette branche pose <c>+0x10 = 0x63</c> et <c>+0x16 = 0x46</c>, puis
    /// <b>tombe</b> dans l'instruction suivante qui pose <c>+0x10 = 2</c> — le meme defaut, au meme
    /// endroit, que chez Pardon. Voir <c>Documentation/Reference/legacy-bug.md</c>.
    /// </para>
    /// <para>
    /// <b>Et un second appel d'extinction, sans garde.</b> Le bloc de fin appelle
    /// <see cref="EndSpellAndResetDecor_00448020"/> une premiere fois sous la condition
    /// <c>+0x16 &gt;= 0x5a</c>, puis une seconde fois inconditionnellement : le <c>jl</c> ne saute
    /// que le premier appel. Sans consequence — l'etat <c>0x63</c> n'est atteint que par ce meme
    /// test, donc la garde est toujours vraie quand le bloc s'execute. Transcrit tel quel.
    /// </para>
    /// <para>
    /// Son decor est le sien : <c>0x78</c>/<c>0x82</c>/<c>0xb4</c>, la ou Pardon posait
    /// <c>0xa0</c>/<c>0xa0</c>/<c>0xb4</c>.
    /// </para>
    /// </summary>
    private static void TickZombie_0044F930(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_0044f959
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            *(ushort*)Mem.Arena(0x005e9160) = 0x78;
            *(ushort*)Mem.Arena(0x005e9162) = 0x82;
            *(ushort*)Mem.Arena(0x005e9164) = 0xb4;
            *(ushort*)Mem.Arena(0x005e9166) = 2;
        }

        // Ghidra: LAB_0044f990
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x27, 0x200, 0x1000, 0x1000);

        // Ghidra: LAB_0044f9b0 — la recherche, une seule image, et elle prend TOUT.
        if (*(short*)(e + 0x10) == 1)
        {
            byte* caster = *(byte**)(e + 0x74);
            if (caster == null) caster = Res.s_EntityList_005de558;

            int struck = 0;
            byte* candidate = Mem.Entity(0x005ecaa0);

            for (int index = 0; index < 0x20; index++, candidate += 0x9c)
            {
                ResolveEntityBlocks(candidate, out byte* stats, out byte* species, out byte* model);

                uint flags = *(uint*)candidate;

                if ((flags & 0x80000000) == 0) continue;
                if ((flags & 0x40002000) != 0) continue;
                if (*(int*)(candidate + 0x74) != 0) continue;

                // LE CRITERE D'ESPECE, le meme que celui de Pardon.
                if ((*(uint*)(stats + 0x14) & 0x3000000) == 0) continue;

                // Une seule distance, celle au LANCEUR, en trois dimensions.
                int dx = (*(int*)(candidate + 0x1c) - *(int*)(caster + 0x1c)) >> 0xc;
                int dy = (*(int*)(candidate + 0x20) - *(int*)(caster + 0x20)) >> 0xc;
                int dz = (*(int*)(candidate + 0x24) - *(int*)(caster + 0x24)) >> 0xc;

                if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) >= 0x5dc)
                {
                    continue;
                }

                *(byte**)(e + 0x78) = candidate;
                *(byte**)(candidate + 0x74) = *(byte**)(e + 0x6c);   // le PERE du sort

                SpawnCastEntity_0047AAE0(e, 0x9e, 0, -1);

                struck++;
            }

            if (struck == 0)
            {
                // Ghidra: 0x0044fab0 — LB-019 : ce 0x63 est efface l'instruction d'apres.
                *(short*)(e + 0x10) = 0x63;
                *(short*)(e + 0x16) = 0x46;
            }

            // Ghidra: LAB_0044fabc — les DEUX chemins passent ici.
            *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_0044fac2
        if (*(short*)(e + 0x10) == 2 && *(short*)(e + 0x16) >= 0x5a) *(short*)(e + 0x10) = 0x63;

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x10) == 0x63)
        {
            // Le `jl` ne saute que ce premier appel ; le second n'a pas de garde.
            if (*(short*)(e + 0x16) >= 0x5a) EndSpellAndResetDecor_00448020(e);

            // Ghidra: LAB_0044faf8 — inconditionnel.
            EndSpellAndResetDecor_00448020(e);
        }
    }

    // =====================================================================
    // PAROLE SAINTE — le quarante-sixieme sort, ecole 1 indice 16, sous-type 0x2e
    //
    //   0x0044D370   LE SORT. Sous-type 0x2e de 0x00551c30, 1 384 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044D370.asm.
    //   0x0044D8E0   son eclat de depart, sous-type 0x9a de 0x00551f30. 208 octets, ABSENT.
    //   0x0044DB30   l'eclat pose sur chaque victime, sous-type 0x9b de 0x00551f30. 304 octets,
    //                ABSENT de Blaze.c lui aussi.
    //   0x0044D9B0   la poussiere, sous-type 0x94 de 0x00552230 — celle-la EST dans Blaze.c.
    //
    // C'EST LE PREMIER SORT DE L'ECOLE 1 QUI VISE UN CONE, ET NON UNE CIBLE. Les quinze
    // precedents choisissent leur victime par AcquireHolyTarget_00447CB0 ou par un balayage a la
    // ronde ; celui-ci balaie les 0x20 entites de scene et ne retient que ce qui tient a la fois
    // dans un rayon de 0x320 et dans une ouverture de +-0x320 autour de son cap — un huitieme de
    // tour de chaque cote, soit 45 degres.
    //
    // ET IL VERIFIE LA VUE. Une cible dans le cone n'est frappee que si le sort peut l'ATTEINDRE :
    // il deplace sa PROPRE position en quatre pas d'un quart de l'ecart, la repose par
    // FinalizeEntityPlacement2 a chaque pas, et abandonne des que le contact 0x8800 apparait en
    // +0x90. Un mur entre les deux protege donc l'ennemi. Aucun autre corps de l'ecole 1 transcrit
    // ne fait cela — c'est la seule occurrence d'un trait qui se cogne au decor.
    //
    // LE PRIX EN EST UNE DANSE DE SAUVEGARDE. Puisque le sort se sert de lui-meme comme sonde, il
    // doit se rendre intact a chaque tour de boucle : neuf champs partent sur la pile en tete
    // d'iteration (+0x20, +0x60, +0x64, +0x7c, +0x80..+0x8c, +0x90) et la position dans le triplet
    // +0x44/+0x48/+0x4c, tout revient en queue. La restitution de +0x90 passe par `and eax,0FFFFh`
    // — LE MOT HAUT EST DONC EFFACE, 0x20 fois par image.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044d370 @ 0x0044d370 — <b>Parole sainte</b> : le sous-type <c>0x2e</c>, creneau
    /// 46 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>dix-septieme fiche de l'ecole
    /// 1</b>. 1 384 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Un cone, pas une cible.</b> Le sort balaie les <c>0x20</c> entites de <c>0x005ecaa0</c>
    /// et retient celles qui tiennent a la fois dans un rayon de <c>0x320</c> et dans une ouverture
    /// de <c>+-0x320</c> autour de son cap <c>+0x32</c> — l'ecart est mesure par <c>ratan2</c> plus
    /// un demi-tour, moins le cap, puis ramene dans <c>[0, 0x1000)</c>. Un ecart <b>nul</b> est
    /// refuse : le test demande <c>&gt; 0</c> d'un cote et <c>&gt; 0xce0</c> de l'autre, et zero
    /// rate les deux.
    /// </para>
    /// <para>
    /// <b>Et la vue est verifiee.</b> Une candidate retenue n'est frappee que si le sort peut
    /// l'atteindre : il avance <b>sa propre position</b> de quatre pas d'un quart de l'ecart, repose
    /// chacun par <see cref="FinalizeEntityPlacement2"/>, et renonce des que le contact
    /// <c>0x8800</c> apparait en <c>+0x90</c>.
    /// </para>
    /// <para>
    /// <b>Le critere d'espece est <c>0x1000080</c></b>, la ou Pardon et Zombie demandent
    /// <c>0x3000000</c> — une autre famille de bestiaire.
    /// </para>
    /// <para>
    /// <b>La chronologie tient en soixante images</b> : bruit aux images 0, 10, 11 et 12 ; l'eclat
    /// <c>0x9a</c> a la premiere ; le voile plein cadre vert-blanc <c>0x01b4c896</c> a la dixieme,
    /// qui fait passer l'etat de 1 a 2 ; le <b>balayage a la douzieme</b>, une seule image ; le
    /// voile s'eteint dans la foulee (etat 3 puis 4) ; le lanceur, tenu par le bit <c>0x80</c>
    /// depuis la premiere image, est relache a la vingtieme ; extinction a la soixantieme.
    /// </para>
    /// <para>
    /// Sur chaque victime : le coup par <see cref="ResolveSpellHit_0053A2B0"/>, une poussee de
    /// <c>sin/cos * 10</c> sur <c>+0x50</c>/<c>+0x58</c>, le clignotement <c>+0x96 = 0x10</c>, un
    /// eclat <c>0x9b</c> et <b>quatre</b> poussieres <c>0x94</c>. Le sort se prete son propre
    /// <c>+0x78</c> et son propre cap le temps de ces appels, et les rend ensuite.
    /// </para>
    /// </summary>
    private static void TickHolyWord_0044D370(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_0044d399 .. LAB_0044d3fc — quatre coups, a l'image pres.
        if (*(short*)(e + 0x16) == 0x00) Aud.PlaySoundEffect(0x30, 0x800, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x0a) Aud.PlaySoundEffect(0x0c, 0x800, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x0b) Aud.PlaySoundEffect(0x30, 0x0ca6, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x0c) Aud.PlaySoundEffect(0x1c, 0x800, 0x1000, 0x1000);

        // Ghidra: LAB_0044d420 — l'amorce, une seule fois.
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            e[0x38] = 0xfa;
            e[0x39] = 0xfa;
            e[0x3a] = 0xfa;

            SpawnCastEntity_0047AAE0(e, 0x9a, 0, -1);

            // Le demi-tour du cap, replie dans le tour. Le binaire relit le champ apres l'ajout.
            *(ushort*)(e + 0x32) += 0x800;
            if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

            // LE LANCEUR EST TENU : le bit 0x80 le fige jusqu'a la vingtieme image.
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;
        }

        // Ghidra: LAB_0044d46f — le decor du sort, repose a chaque image de l'etat 1.
        if (*(short*)(e + 0x10) == 1)
        {
            *(ushort*)Mem.Arena(0x005e9160) = 0xaa;
            *(ushort*)Mem.Arena(0x005e9164) = 0xaa;
            *(ushort*)Mem.Arena(0x005e9162) = 0xc8;
            *(ushort*)Mem.Arena(0x005e9166) = 2;

            if (*(short*)(e + 0x16) == 0x0a)
            {
                *(short*)(e + 0x10) = 2;

                // Le voile plein cadre s'allume en vert-blanc — les trois canaux et l'octet
                // d'activation tiennent dans le meme mot.
                Mem.s_Unknown_005de024 = 0x01b4c896;

                *(ushort*)Mem.Arena(0x005e9166) = 0;
            }
        }

        // Ghidra: LAB_0044d4d1 — tout le balayage n'a lieu que dans l'etat 2.
        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(e + 0x16) == 0x0c) *(short*)(e + 0x10) = 3;

            byte* candidate = Mem.Entity(0x005ecaa0);

            for (int slot = 0; slot < 0x20; slot++, candidate += 0x9c)
            {
                // Ghidra: LAB_0044d4f7 — LA SAUVEGARDE. Le sort va se servir de lui-meme comme
                // sonde ; neuf champs partent sur la pile et la position dans +0x44/+0x48/+0x4c.
                int saved60 = *(int*)(e + 0x60);
                int saved64 = *(int*)(e + 0x64);
                int saved7c = *(int*)(e + 0x7c);
                int saved80 = *(int*)(e + 0x80);
                int saved84 = *(int*)(e + 0x84);
                int saved88 = *(int*)(e + 0x88);
                int saved8c = *(int*)(e + 0x8c);
                ushort saved90 = *(ushort*)(e + 0x90);
                int savedY = *(int*)(e + 0x20);

                *(int*)(e + 0x44) = *(int*)(e + 0x1c);
                *(int*)(e + 0x48) = *(int*)(e + 0x20);
                *(int*)(e + 0x4c) = *(int*)(e + 0x24);

                // L'ecart au candidat, garde deux fois : brut pour la marche, en unites entieres
                // pour l'angle et la portee.
                int rawX = *(int*)(candidate + 0x1c) - *(int*)(e + 0x1c);
                int rawY = *(int*)(candidate + 0x20) - *(int*)(e + 0x20);
                int rawZ = *(int*)(candidate + 0x24) - *(int*)(e + 0x24);

                int dx = rawX >> 0xc;
                int dy = rawY >> 0xc;
                int dz = rawZ >> 0xc;

                // Ghidra: FUN_00545a22 — winsys3d Ordinal_457, ratan2. Le demi-tour compense celui
                // que l'amorce a ajoute au cap.
                int bearing = Winsys3d.Ordinal_457(dx, dz) + 0x800 - *(short*)(e + 0x32);

                // Le repli dans [0, 0x1000), ecrit comme le binaire : un seul ajout d'un multiple
                // de tour par le bas, une soustraction repetee par le haut.
                if (bearing < 0) bearing += (int)((uint)(0xfff - bearing) >> 0xc << 0xc);

                if (bearing >= 0x1000)
                {
                    uint turns = (uint)bearing >> 0xc;
                    do { bearing -= 0x1000; } while (--turns != 0);
                }

                // Ghidra: FUN_00545a10 — winsys3d Ordinal_442, le SquareRoot0 de libgte. La portee
                // se mesure en trois dimensions, l'angle en deux.
                int range = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

                // Ghidra: 0x0044d5ed — LE CONE, et une seule image pour le franchir. L'ecart nul
                // est refuse : le premier test demande strictement positif, et zero retombe alors
                // dans la fenetre haute, qu'il rate aussi.
                bool aimed = (bearing > 0 && bearing < 0x320)
                             || (bearing < 0x1000 && bearing > 0x0ce0);

                if (*(short*)(e + 0x16) == 0x0c && range < 0x320 && aimed)
                {
                    // Ghidra: LAB_0044d628 — LA MARCHE. Quatre pas d'un quart de l'ecart, chacun
                    // repose et teste le contact ; le compteur saute a 0x63 des qu'il porte, ce qui
                    // sort de la boucle a l'increment suivant.
                    int stepX = rawX >> 2;
                    int stepY = rawY >> 2;
                    int stepZ = rawZ >> 2;

                    bool blocked = false;
                    int step = 0;

                    do
                    {
                        *(int*)(e + 0x1c) += stepX;
                        *(int*)(e + 0x20) += stepY;
                        *(int*)(e + 0x24) += stepZ;

                        FinalizeEntityPlacement2((uint*)e);   // Ghidra: FUN_00533940

                        if ((*(uint*)(e + 0x90) & 0x8800) != 0)
                        {
                            blocked = true;
                            step = 0x63;
                        }

                        step++;
                    }
                    while ((short)step <= 3);

                    if (!blocked)
                    {
                        ResolveEntityBlocks(candidate, out byte* stats, out _, out _);

                        uint flags = *(uint*)candidate;

                        // LE CRITERE D'ESPECE : 0x1000080, et non le 0x3000000 de Pardon.
                        if ((flags & 0x80000000) != 0
                            && (flags & 0x40002000) == 0
                            && (*(uint*)(stats + 0x14) & 0x1000080) != 0)
                        {
                            // Ghidra: 0x0044d6f0 — LE COUP. Le sort se pose sur la victime.
                            *(int*)(e + 0x1c) = *(int*)(candidate + 0x1c);
                            *(int*)(e + 0x20) = *(int*)(candidate + 0x20);
                            *(int*)(e + 0x24) = *(int*)(candidate + 0x24);

                            *(ushort*)(e + 0x14) = 0x32;
                            *(ushort*)(e + 0x9a) = 2;

                            // Le cap et le pointeur de cible sont PRETES le temps des appels : les
                            // deux enfants et le depot les lisent, et les deux sont rendus ensuite.
                            short lentYaw = *(short*)(e + 0x32);
                            byte* lentTarget = *(byte**)(e + 0x78);

                            *(short*)(e + 0x32) = (short)Winsys3d.Ordinal_457(dx, dz);
                            *(byte**)(e + 0x78) = candidate;

                            SpawnCastEntity_0047AAE0(e, 0x9b, 0, -1);

                            // Quatre poussieres, ecrites quatre fois de suite dans le binaire.
                            for (int mote = 0; mote < 4; mote++)
                            {
                                SpawnStatusEffectEntity_0047AC40(
                                    e, (int*)(e + 0x1c), null, 0x94, 0, -1);
                            }

                            ResolveSpellHit_0053A2B0(
                                *(uint**)(e + 0x74), *(uint**)(e + 0x78), e[7], e[6], 0);

                            // LA POUSSEE, dans l'axe du coup et dix fois la table.
                            int shove = *(short*)(e + 0x32);
                            *(int*)(candidate + 0x50) +=
                                (int)Winsys3d.GetSinTableEntry(shove) * 10;
                            *(int*)(candidate + 0x58) +=
                                Winsys3d.GetCosTableEntry(shove) * 10;

                            *(ushort*)(candidate + 0x96) = 0x10;   // le clignotement de degat
                            *(uint*)candidate |= 0x40;

                            *(short*)(e + 0x32) = lentYaw;
                            *(byte**)(e + 0x78) = lentTarget;
                        }
                    }
                }

                // Ghidra: LAB_0044d80a — LA RESTITUTION, sur les deux chemins. La hauteur revient
                // de la pile, l'abscisse et la cote du triplet +0x44/+0x4c ; +0x48 est ecrit en
                // tete d'iteration et JAMAIS RELU.
                *(int*)(e + 0x7c) = saved7c;
                *(int*)(e + 0x20) = savedY;

                // `and eax,0FFFFh` avant l'ecriture : le mot haut de +0x90 est efface.
                *(uint*)(e + 0x90) = saved90;

                *(int*)(e + 0x1c) = *(int*)(e + 0x44);
                *(int*)(e + 0x60) = saved60;
                *(int*)(e + 0x64) = saved64;
                *(int*)(e + 0x84) = saved84;
                *(int*)(e + 0x24) = *(int*)(e + 0x4c);
                *(int*)(e + 0x80) = saved80;
                *(int*)(e + 0x88) = saved88;
                *(int*)(e + 0x8c) = saved8c;
            }
        }

        // Ghidra: LAB_0044d87e — l'etat 3 ne dure pas une image : il eteint le voile et passe a 4.
        if (*(short*)(e + 0x10) == 3)
        {
            Mem.s_Unknown_005de024 = 0x01000000;
            *(short*)(e + 0x10) = 4;
        }

        // Le lanceur est relache a la vingtieme image.
        if (*(short*)(e + 0x16) == 0x14) *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) >= 0x3c) EndSpellAndResetDecor_00448020(e);
    }

    /// <summary>
    /// Ghidra: FUN_0044d8e0 @ 0x0044d8e0 — <b>l'eclat de depart de Parole sainte</b> : le sous-type
    /// <c>0x9a</c>, creneau 154 de <see cref="s_StaticEntityHandlers_00551f30"/>. 208 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044D8E0.asm</c>.
    ///
    /// <para>
    /// Il nait a la premiere image du sort, <b>avant</b> que celui-ci ne retourne son cap : il se
    /// pousse donc de <c>50</c> fois <c>sin</c>/<c>cos</c> devant le lanceur et de <c>0x46000</c>
    /// au-dessus, part a l'echelle pleine <c>0x1000</c> sur les trois axes et noir sur les trois
    /// canaux.
    /// </para>
    /// <para>
    /// <b>Onze images, deux moities.</b> Jusqu'a la dixieme il grossit de <c>100</c> par axe et
    /// s'eclaircit de <c>+20</c> ; ensuite il ne fait plus que s'assombrir de <c>-10</c>, et rend
    /// la main des que son canal rouge <c>+0x38</c> est retombe a zero.
    /// </para>
    /// </summary>
    private static void TickHolyWordFlare_0044D8E0(byte* record)
    {
        byte* e = record;

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x2c;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;

            // Ghidra: call 00545A1C / 00545A16 — les tables de sinus et de cosinus de winsys3d.
            int heading = *(short*)(e + 0x32);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * 50;
            *(short*)(e + 0x16) = 0;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * 50;
            *(int*)(e + 0x20) += 0x46000;

            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            *(short*)(e + 0x2c) = 0x1000;
        }

        // Ghidra: LAB_0044d95c
        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) > 0x0a)
        {
            BrightenEntityTint_00404DE0(e, -10, -10, -10);
            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            return;
        }

        // Ghidra: LAB_0044d98d
        *(short*)(e + 0x28) += 100;
        *(short*)(e + 0x2a) += 100;
        *(short*)(e + 0x2c) += 100;

        BrightenEntityTint_00404DE0(e, 0x14, 0x14, 0x14);
    }

    /// <summary>
    /// Ghidra: FUN_0044db30 @ 0x0044db30 — <b>l'eclat pose sur la victime</b> de Parole sainte : le
    /// sous-type <c>0x9b</c>, creneau 155 de <see cref="s_StaticEntityHandlers_00551f30"/>. 304
    /// octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044DB30.asm</c>.
    ///
    /// <para>
    /// <b>Il ne quitte jamais sa cible.</b> Sa position est recopiee a chaque image de son
    /// <c>+0x78</c> — que le sort avait prete a la candidate juste avant de l'engendrer — avec
    /// <c>0x32000</c> de hauteur en plus. Un <c>+0x78</c> nul le rabat sur
    /// <see cref="Resources.s_EntityList_005de558"/>, l'entite nulle du moteur.
    /// </para>
    /// <para>
    /// <b>Tout le reste est tire au sort</b> : les trois rotations sur un tour complet, et deux
    /// vitesses de rotation dans <c>[-50, 49]</c> qui poussent <c>+0x30</c> et <c>+0x32</c> dans un
    /// sens et <c>+0x34</c> dans l'autre. Cinq images seulement : quatre a <c>+40</c> de teinte,
    /// puis <c>-30</c> jusqu'a extinction.
    /// </para>
    /// </summary>
    private static void TickHolyWordShard_0044DB30(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + 0x32000;
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x1d;
            e[0x0a] = 2;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x28) = 0x0fa0;
            *(short*)(e + 0x2a) = 0x0fa0;
            *(short*)(e + 0x2c) = 0x0fa0;

            *(short*)(e + 0x30) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x34) = (short)(CrtRand() % 0x1000);

            *(short*)(e + 0x12) = (short)(CrtRand() % 100 - 0x32);
            *(short*)(e + 0x2e) = -60;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = (short)(CrtRand() % 100 - 0x32);
        }

        // Ghidra: LAB_0044dc09
        if (*(short*)(e + 0x10) != 1) return;

        short spinA = *(short*)(e + 0x12);
        short spinB = *(short*)(e + 0x14);

        *(short*)(e + 0x30) += spinA;
        *(short*)(e + 0x32) += spinB;
        *(short*)(e + 0x34) -= spinA;

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) < 5)
        {
            BrightenEntityTint_00404DE0(e, 0x28, 0x28, 0x28);
            return;
        }

        // Ghidra: LAB_0044dc41
        BrightenEntityTint_00404DE0(e, -30, -30, -30);
        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0044d9b0 @ 0x0044d9b0 — <b>la poussiere</b> de Parole sainte : le sous-type
    /// <c>0x94</c> de <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c>
    /// ou depose <see cref="SpawnStatusEffectEntity_0047AC40"/>. <b>Presente dans <c>Blaze.c</c></b>,
    /// contrairement aux trois autres corps de ce sort.
    ///
    /// <para>
    /// Le sort en engendre <b>quatre</b> a la position de chaque victime. Chacune se donne l'atlas
    /// <see cref="Descriptors00551528"/>, se disperse d'un ecart de <c>[-50, 49]</c> unites sur les
    /// trois axes, prend un cap a <c>+-150</c> de celui du sort et une vitesse de <c>30</c> a
    /// <c>39</c>, et part en teinte <b>negative</b> — les trois canaux tires dans <c>[-76, -37]</c>.
    /// </para>
    /// <para>
    /// <b>C'est cette teinte negative qui la tue.</b> Elle remonte de <c>-30</c> par image, donc
    /// vers zero, et l'entite rend la main des que <c>+0x38</c>, <b>lu comme un octet signe</b>,
    /// atteint exactement zero. Sa duree de vie depend donc de son propre tirage de depart.
    /// </para>
    /// </summary>
    private static void TickHolyWordMote_0044D9B0(byte* record)
    {
        byte* e = record;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors00551528();   // Ghidra: &DAT_00551528
            e[0x0a] = 9;
            e[0x0b] = 0;

            *(short*)(e + 0x32) += (short)(CrtRand() % 300 - 0x96);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x1e - 0x0f);
            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 0x1e);

            short size = (short)(CrtRand() % 0x400 + 0x1000);
            *(short*)(e + 0x28) = size;
            *(short*)(e + 0x2a) = size;

            // La teinte part NEGATIVE : c'est elle qui compte les images.
            e[0x38] = (byte)(CrtRand() % 0x28 - 0x4c);
            e[0x39] = (byte)(CrtRand() % 0x28 - 0x4c);
            e[0x3a] = (byte)(CrtRand() % 0x28 - 0x4c);

            *(int*)(e + 0x1c) += (CrtRand() % 100 - 0x32) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 100 - 0x32) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 100 - 0x32) * 0x1000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            // Ghidra: Ordinal_451 / Ordinal_450 — les tables de sinus et de cosinus.
            int heading = *(short*)(e + 0x32);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * *(short*)(e + 0x12);
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * *(short*)(e + 0x12);
            *(int*)(e + 0x20) += *(short*)(e + 0x14) * 0x1000;

            BrightenEntityTint_00404DE0(e, -30, -30, -30);

            if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    // =====================================================================
    // DELIVRANCE — le quarante-septieme sort, ecole 1 indice 17, sous-type 0x2f
    //
    //   0x0044B7B0   LE SORT. Sous-type 0x2f de 0x00551c30, 352 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044B7B0.asm.
    //   0x0044B910   SON ENVELOPPE D'EFFET. Cinquante-huit octets, decompilee dans Blaze.c.
    //   0x0044B950   son halo, sous-type 0x8f de 0x00552230. Decompile dans Blaze.c.
    //   0x0044BA00   le petale de la couronne, sous-type 0x91 de 0x00551f30. 226 octets, ABSENT.
    //
    // TOUT LE SORT TIENT EN UN APPEL, ET IL VIENT A LA FIN. Trente images de mise en scene — quatre
    // bruits, un halo, deux couronnes de huit petales — puis, a la trentieme, ClearStatusEffect
    // 0x0053c8e0 leve l'effet d'etat 9 (le bit 0x10 de +0x40, duree en +0x92). C'est l'exacte
    // symetrie de Renfort : la meme forme que Sang pur, mais l'effet a la DERNIERE image au lieu de
    // la premiere.
    //
    // ET IL PRETE SA PAIRE AU LANCEUR AVANT DE FRAPPER. Comme Sang pur, il recopie ses propres
    // +0x06/+0x07 sur le lanceur juste avant l'appel — c'est la paire famille/indice que le depot
    // relit pour retrouver la fiche.
    //
    // LA COURONNE EST UN TOUR ENTIER, ET LE CAP EST RENDU. Aux images 5 et 10, le sort met son cap
    // a zero, engendre huit petales en avancant de 0x200 (un huitieme de tour) entre chacun, puis
    // REPOSE le cap qu'il avait avant. Le meme pret-restitution que Parole sainte, en plus court.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044b910 @ 0x0044b910 — <b>lever l'effet d'etat 9 si la cible l'accepte</b>.
    /// Cinquante-huit octets, decompilee dans <c>Blaze.c</c> : le garde-fou
    /// <see cref="IsEffectPairBlocked_004364B0"/>, puis <see cref="ClearStatusEffect_0053C8E0"/>
    /// s'il a laisse passer. La jumelle exacte de <see cref="HealTargetIfEligible_00447FE0"/>,
    /// avec l'autre effet derriere.
    ///
    /// <para>
    /// <b>Meme inversion du sens rendu</b> que sa jumelle : refus du garde-fou, elle rend
    /// <c>true</c> sans rien faire ; passage, elle rend le resultat de la levee, qui vaut <c>1</c>
    /// quand <see cref="PrepareCombatPair_00538870"/> a echoue et <c>0</c> quand l'effet a bien ete
    /// leve. <b>Son seul appelant, Delivrance, jette le resultat.</b>
    /// </para>
    /// </summary>
    private static bool ClearStatusEffectIfEligible_0044B910(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053C8E0(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044b7b0 @ 0x0044b7b0 — <b>Delivrance</b> : le sous-type <c>0x2f</c>, creneau 47
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>dix-huitieme fiche de l'ecole
    /// 1</b>. 352 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il ne fait qu'une chose, et a la derniere image.</b> Trente images durant il n'est que du
    /// decor ; a la trentieme il recopie sa paire famille/indice sur le lanceur et appelle
    /// <see cref="ClearStatusEffectIfEligible_0044B910"/> sur <c>+0x78</c>, ce qui leve <b>l'effet
    /// d'etat 9</b> — le bit <c>0x10</c> de <c>+0x40</c>, dont la duree vit en <c>+0x92</c>, celui
    /// que <see cref="ApplyStatusEffect_0053B580"/> pose. Puis il s'eteint dans la meme image.
    /// </para>
    /// <para>
    /// <b>C'est Sang pur a l'envers.</b> Meme forme, meme enveloppe a garde-fou, meme pret de la
    /// paire au lanceur — mais Sang pur soigne dans son bloc de naissance et passe le reste a
    /// n'etre qu'un decor, la ou Delivrance passe le decor d'abord et frappe en sortant.
    /// </para>
    /// <para>
    /// <b>La mise en scene</b> : un halo <c>0x8f</c> a la premiere image ; quatre bruits aux images
    /// 0, 1, 2 et 3 ; et <b>deux couronnes de huit petales</b> <c>0x91</c>, aux images 5 et 10. La
    /// couronne se trace en mettant le cap a zero, en avancant de <c>0x200</c> — un huitieme de
    /// tour — entre chaque petale, et en <b>reposant</b> le cap d'origine a la fin.
    /// </para>
    /// </summary>
    private static void TickDeliverance_0044B7B0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_0044b7d8 — l'amorce, une seule fois : le halo et rien d'autre.
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x8f, 0, -1);
        }

        // Ghidra: LAB_0044b806 .. LAB_0044b865 — quatre bruits sur les quatre premieres images.
        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x21, 0x0329, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x0c, 0x0800, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x0f, 0x0400, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 3) Aud.PlaySoundEffect(0x10, 0x0329, 0x1000, 0x1000);

        // Ghidra: LAB_0044b885 — LA COURONNE, deux fois : aux images 5 et 10.
        if (*(short*)(e + 0x16) == 5 || *(short*)(e + 0x16) == 10)
        {
            // Le cap est PRETE le temps du tour, et repose ensuite.
            short lentYaw = *(short*)(e + 0x32);
            *(short*)(e + 0x32) = 0;

            for (int petal = 0; petal < 8; petal++)
            {
                SpawnCastEntity_0047AAE0(e, 0x91, 0, -1);
                *(ushort*)(e + 0x32) += 0x200;   // un huitieme de tour
            }

            *(short*)(e + 0x32) = lentYaw;
        }

        // Ghidra: LAB_0044b8d0
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) == 0x1e)
        {
            // LE SEUL EFFET DU SORT. La paire famille/indice est preteee au lanceur d'abord : c'est
            // elle que le depot relit pour retrouver la fiche.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ClearStatusEffectIfEligible_0044B910(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);

            EndSpellAndResetDecor_00448020(e);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044b950 @ 0x0044b950 — <b>le halo de Delivrance</b> : le sous-type <c>0x8f</c>
    /// de <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c> ou depose
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/>. <b>Decompile dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il colle a la cible, une tete au-dessus.</b> Sa position est recopiee a chaque image de
    /// son <c>+0x78</c> avec <c>0x46000</c> de hauteur en plus ; un <c>+0x78</c> nul le rabat sur
    /// <see cref="Resources.s_EntityList_005de558"/>.
    /// </para>
    /// <para>
    /// <b>Il part enorme et blanc, et se resorbe geometriquement</b> : echelle <c>30000</c> sur
    /// deux axes, teinte pleine <c>0xff</c> sur les trois canaux, puis chaque image un huitieme
    /// d'echelle en moins (<c>x - (x &gt;&gt; 3)</c>) et <c>-10</c> de teinte. Il rend la main
    /// quand son canal rouge <c>+0x38</c>, <b>lu comme un octet signe</b>, atteint zero.
    /// </para>
    /// </summary>
    private static void TickDeliveranceHalo_0044B950(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + 0x46000;
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x70;   // Ghidra: &DAT_00551598
            e[0x38] = 0xff;
            e[0x39] = 0xff;
            e[0x3a] = 0xff;
            e[0x40] = 0;
            e[0x0a] = 7;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x28) = 30000;
            *(short*)(e + 0x2a) = 30000;
            *(short*)(e + 0x2e) = -60;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, -10, -10, -10);

            *(short*)(e + 0x28) -= (short)(*(short*)(e + 0x28) >> 3);
            *(short*)(e + 0x2a) -= (short)(*(short*)(e + 0x2a) >> 3);

            if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044ba00 @ 0x0044ba00 — <b>le petale de la couronne de Delivrance</b> : le
    /// sous-type <c>0x91</c>, creneau 145 de <see cref="s_StaticEntityHandlers_00551f30"/>. 226
    /// octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044BA00.asm</c>.
    ///
    /// <para>
    /// Il est engendre par huit, un huitieme de tour d'ecart, et colle a son <c>+0x78</c> —
    /// <b>sans</b> decalage vertical, contrairement au halo. Ce sont donc huit exemplaires du meme
    /// corps que seul leur cap <c>+0x32</c> distingue.
    /// </para>
    /// <para>
    /// <b>Son minuteur est une echelle.</b> <c>+0x34</c> part de <c>0x400</c> et perd <c>20</c> par
    /// image ; le cap tourne de <c>0x64</c> et <c>+0x2a</c> grandit de <c>0xb4</c>. Sous
    /// <c>0x96</c>, le petale entre dans sa fin : les deux echelles horizontales perdent <c>20</c>,
    /// <c>+0x2a</c> gagne <c>0x32</c> de plus, et la teinte passe de <c>+20</c> a <c>-30</c> net.
    /// Il s'eteint quand <c>+0x34</c> devient negatif — <b>a la lecture d'avant</b> le nouveau test
    /// de <c>0x96</c>, qui relit donc la valeur remise a zero.
    /// </para>
    /// </summary>
    private static void TickDeliverancePetal_0044BA00(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0;
            e[0x0a] = 2;
            e[0x0b] = 1;
            *(short*)(e + 0x28) = 0x0320;
            *(short*)(e + 0x2c) = 0x0320;
            *(short*)(e + 0x2a) = 0x012c;
            *(short*)(e + 0x34) = 0x0400;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
        }

        // Ghidra: LAB_0044ba65
        if (*(short*)(e + 0x10) != 1) return;

        BrightenEntityTint_00404DE0(e, 0x14, 0x14, 0x14);

        *(ushort*)(e + 0x32) += 0x64;
        *(ushort*)(e + 0x2a) += 0xb4;
        *(short*)(e + 0x34) -= 20;

        // Le binaire retient ici les deux valeurs qu'il vient d'ecrire ; c'est CETTE lecture de
        // +0x2a que le bloc de fin reprend, et non une relecture du champ.
        short grown = *(short*)(e + 0x2a);
        short fuse = *(short*)(e + 0x34);

        if (*(short*)(e + 0x32) >= 0x1000) *(short*)(e + 0x32) -= 0x1000;

        // Ghidra: LAB_0044baa5 — le minuteur passe sous zero : le petale rend la main.
        if (fuse < 0)
        {
            *(short*)(e + 0x34) = 0;
            *(uint*)e &= 0x7fffffff;
        }

        // Ghidra: LAB_0044bab7 — le champ est RELU, donc la remise a zero ci-dessus fait entrer
        // dans cette derniere branche l'image ou le petale s'eteint.
        if (*(short*)(e + 0x34) < 0x96)
        {
            *(short*)(e + 0x28) -= 20;
            *(short*)(e + 0x2c) -= 20;
            *(short*)(e + 0x2a) = (short)(grown + 0x32);

            BrightenEntityTint_00404DE0(e, -50, -50, -50);
        }
    }

    // =====================================================================
    // MICRO — le quarante-huitieme sort, ecole 1 indice 18, sous-type 0x30
    //
    //   0x0044BAF0   LE SORT. Sous-type 0x30 de 0x00551c30, 391 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044BAF0.asm.
    //   0x0044BC80   SON ENVELOPPE D'EFFET. Cinquante-huit octets, decompilee dans Blaze.c.
    //   0x0044BCC0   sa bille en orbite, sous-type 0x90 de 0x00552230. Decompilee dans Blaze.c.
    //   0x0044BE40   son eclair, sous-type 0x92 de 0x00551f30. 177 octets, ABSENT de Blaze.c.
    //
    // MICRO ET DELIVRANCE SONT DEUX JUMEAUX EXACTS, ET LE CD LE DIT AVANT LE BINAIRE. Meme cout
    // (40 PM), meme palier (63), meme portee (36), meme longueur de combinaison (onze coups), poses
    // cote a cote dans la table de sorts ET dans le binaire. Le corps confirme : meme forme, meme
    // enveloppe a garde-fou, meme pret de la paire famille/indice au lanceur, et l'effet a la
    // derniere image. La seule difference est CE QU'ILS LEVENT — Delivrance l'etat 9 (bit 0x10,
    // +0x92), Micro l'etat 10 (bit 0x20, +0x94), les deux effets voisins de la table d'etats.
    // C'est le second couple de ce genre releve dans l'ecole, apres Lapeche et Priere.
    //
    // MAIS SA MISE EN SCENE, ELLE, N'A PAS DE JUMELLE. Micro est la SEULE fiche de l'ecole a
    // reprendre sa cible A CHAQUE IMAGE — les autres appellent AcquireHolyTarget une fois, dans le
    // bloc de naissance ; Micro l'appelle sans garde, avant meme de savoir s'il vient de naitre. Et
    // c'est aussi la seule a mettre EndSpellOnTerrain sous condition, exactement a l'inverse.
    //
    // ET IL SE TELEPORTE A L'ORIGINE POUR ENGENDRER SON ECLAIR. Aux images 0 et 4 il pose
    // +0x1c = +0x20 = +0x24 = 0xdac avant d'appeler SpawnCastEntity, et ne les repose pas : c'est
    // l'image suivante qui les reprend du porteur. Sans consequence visible sur l'eclair, qui
    // recalcule sa propre position depuis +0x78 des sa premiere image ; sans consequence non plus
    // sur la visee, FindTargetAround_00474BA0 balayant autour du LANCEUR et non autour du sort.
    // Transcrit tel quel.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044bc80 @ 0x0044bc80 — <b>lever l'effet d'etat 10 si la cible l'accepte</b>.
    /// Cinquante-huit octets, decompilee dans <c>Blaze.c</c> : le garde-fou
    /// <see cref="IsEffectPairBlocked_004364B0"/>, puis <see cref="ClearStatusEffect_0053C930"/>
    /// s'il a laisse passer.
    ///
    /// <para>
    /// <b>Instruction pour instruction la meme fonction que
    /// <see cref="ClearStatusEffectIfEligible_0044B910"/></b> : les deux corps de 58 octets ne
    /// different que par les deplacements de leurs deux <c>call</c>, et le premier des deux resout
    /// vers le meme <c>0x004364b0</c> — seul l'effet appele change. Le compilateur a emis deux fois
    /// le meme corps pour les deux jumeaux. Meme inversion du sens rendu, et son seul appelant jette
    /// le resultat.
    /// </para>
    /// </summary>
    private static bool ClearStatusEffectIfEligible_0044BC80(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053C930(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044baf0 @ 0x0044baf0 — <b>Micro</b> : le sous-type <c>0x30</c>, creneau 48 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>dix-neuvieme fiche de l'ecole 1</b>.
    /// 391 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le jumeau de <see cref="TickDeliverance_0044B7B0"/></b> : meme forme, meme enveloppe a
    /// garde-fou, meme pret de la paire famille/indice au lanceur, meme effet a la derniere image —
    /// la vingt-huitieme ici, la trentieme la-bas. Il leve <b>l'effet d'etat 10</b> (le bit
    /// <c>0x20</c> de <c>+0x40</c>, duree en <c>+0x94</c>), celui que
    /// <see cref="ApplyStatusEffect_0053B660"/> pose, quand Delivrance leve le 9.
    /// </para>
    /// <para>
    /// <b>Deux singularites dans l'ecole, et elles vont par paire.</b> Micro est la seule fiche a
    /// appeler <see cref="AcquireHolyTarget_00447CB0"/> <b>a chaque image</b> — il reprend donc sa
    /// cible en continu, la ou les autres la fixent en naissant — et la seule a mettre
    /// <see cref="EndSpellOnTerrain_00448040"/> <b>sous condition</b>, ne l'appelant qu'a la
    /// premiere image quand les autres l'appellent toujours. Les deux choix sont exactement
    /// inverses de la norme de l'ecole.
    /// </para>
    /// <para>
    /// <b>La mise en scene</b> : quatre bruits aux images 0 a 3 ; un eclair <c>0x92</c> aux images 0
    /// et 4 ; et <b>deux billes <c>0x90</c> toutes les trois images</b>, la seconde a un demi-tour
    /// de la premiere — l'ecart passe par <c>+0x9a</c>, que la bille relit en naissant comme angle
    /// de depart. Le champ reste a <c>0x800</c> apres la salve ; rien ne le repose.
    /// </para>
    /// </summary>
    private static void TickMicro_0044BAF0(byte* record)
    {
        byte* e = record;

        // SOUS CONDITION — la seule fiche de l'ecole a le faire.
        if (*(short*)(e + 0x10) == 0) EndSpellOnTerrain_00448040(e);

        // Ghidra: LAB_0044bb0a — SANS GARDE : la cible est reprise a chaque image.
        AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_0044bb21 — le sort colle a sa cible, sans decalage vertical.
        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = 0;
        }

        // Ghidra: LAB_0044bb49 — DEUX BILLES TOUTES LES TROIS IMAGES, un demi-tour d'ecart. L'angle
        // de depart voyage par +0x9a, que la bille relit en naissant.
        if (*(short*)(e + 0x14) >= 3)
        {
            *(short*)(e + 0x14) = 0;

            *(ushort*)(e + 0x9a) = 0;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x90, 0, -1);

            *(ushort*)(e + 0x9a) = 0x800;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x90, 0, -1);
        }

        // Ghidra: LAB_0044bb8a — L'ECLAIR, aux images 0 et 4. Le sort se pose a l'origine juste
        // avant d'engendrer, et ne se repose pas : c'est l'image suivante qui le reprend du
        // porteur. L'eclair n'en herite pas — il recalcule sa position depuis +0x78.
        if (*(short*)(e + 0x16) == 0 || *(short*)(e + 0x16) == 4)
        {
            *(int*)(e + 0x1c) = 0x0dac;
            *(int*)(e + 0x20) = 0x0dac;
            *(int*)(e + 0x24) = 0x0dac;

            SpawnCastEntity_0047AAE0(e, 0x92, 0, -1);
        }

        // Ghidra: LAB_0044bbb7 .. LAB_0044bc16 — quatre bruits sur les quatre premieres images.
        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x2f, 0x0ca6, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x2f, 0x0d50, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x31, 0x0194, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 3) Aud.PlaySoundEffect(0x16, 0x00ca, 0x1000, 0x1000);

        // Ghidra: LAB_0044bc36
        *(short*)(e + 0x14) += 1;
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) == 0x1c)
        {
            // LE SEUL EFFET DU SORT, comme chez Delivrance : la paire famille/indice est pretee au
            // lanceur, c'est elle que le depot relit pour retrouver la fiche.
            byte* caster = *(byte**)(e + 0x74);
            caster[6] = e[6];
            caster[7] = e[7];

            ClearStatusEffectIfEligible_0044BC80(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);

            EndSpellAndResetDecor_00448020(e);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044bcc0 @ 0x0044bcc0 — <b>la bille en orbite de Micro</b> : le sous-type
    /// <c>0x90</c> de <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c>
    /// ou depose <see cref="SpawnStatusEffectEntity_0047AC40"/>. <b>Decompilee dans
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle tourne, monte et grossit, tout a la fois</b>, sur trois compteurs qui avancent d'un
    /// pas fixe : l'angle <c>+0x12</c> de <c>0xa0</c>, le rayon <c>+0x14</c> de <c>5</c>, et le
    /// compteur d'altitude <c>+0x16</c> de <c>5</c> aussi — la hauteur valant <c>+0x16 * 0x800</c>.
    /// L'angle de depart vient de <c>+0x9a</c>, que le sort a pose avant de l'engendrer : c'est ce
    /// qui met les deux billes d'une salve a un demi-tour l'une de l'autre.
    /// </para>
    /// <para>
    /// <b>Elle orbite autour de deux centres a la fois.</b> La position est d'abord calculee autour
    /// de l'<b>instantane</b> pris a la naissance (<c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>), puis
    /// ramenee d'un quart vers la meme orbite prise autour du <b>porteur vivant</b>
    /// (<c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>, recopies en tete de chaque image). L'instantane
    /// n'est jamais reecrit : la bille reste donc a un quart du chemin, elle ne rattrape pas.
    /// </para>
    /// <para>
    /// <b>Son echelle change de signe a mi-course</b> : <c>+0x96</c> par image tant que
    /// <c>+0x16 &lt;= 0x50</c>, puis <c>-0xbe</c>. Elle rend la main quand <c>+0x16</c> depasse
    /// <c>0x77</c>, soit une vingtaine d'images.
    /// </para>
    /// </summary>
    private static void TickMicroMote_0044BCC0(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // Le porteur vivant, relu a chaque image.
        *(int*)(e + 0x44) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x48) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x4c) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x40] = 0;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);      // l'instantane de naissance
            e[0x0b] = 0;
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(ushort*)(e + 0x12) = *(ushort*)(e + 0x9a);   // l'angle de depart, pose par le sort
            *(short*)(e + 0x14) = 0x14;
            *(short*)(e + 0x16) = 0x14;
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x30;   // Ghidra: &DAT_00551558
            e[0x0a] = 6;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(short*)(e + 0x28) = 700;
            *(short*)(e + 0x2a) = 700;
        }

        if (*(short*)(e + 0x10) != 1) return;

        // Le binaire consulte les deux tables DEUX fois chacune avec le meme argument ; une lecture
        // suffit ici, ce sont des tables.
        int angle = *(short*)(e + 0x12);
        int radius = *(short*)(e + 0x14);
        short lift = *(short*)(e + 0x16);

        int sin = (int)Winsys3d.GetSinTableEntry(angle);   // Ghidra: Ordinal_451
        int cos = Winsys3d.GetCosTableEntry(angle);        // Ghidra: Ordinal_450

        // L'orbite autour de l'instantane de naissance...
        int x = sin * radius + *(int*)(e + 0x50);
        int z = cos * radius + *(int*)(e + 0x58);
        int y = *(int*)(e + 0x54) + lift * 0x800;

        // ... ramenee d'un quart vers la meme orbite autour du porteur vivant. La hauteur est
        // defaite puis refaite : `y + lift * -0x800` redonne l'instantane vertical.
        x -= (x - (sin * radius + *(int*)(e + 0x44))) >> 2;
        y -= ((y + lift * -0x800) - *(int*)(e + 0x48)) >> 2;
        z -= (z - (cos * radius + *(int*)(e + 0x4c))) >> 2;

        *(int*)(e + 0x1c) = x;
        *(int*)(e + 0x20) = y;
        *(int*)(e + 0x24) = z;

        *(short*)(e + 0x12) = (short)(angle + 0xa0);
        *(short*)(e + 0x14) = (short)(radius + 5);

        lift = (short)(lift + 5);
        *(short*)(e + 0x16) = lift;

        short grow = lift > 0x50 ? (short)-0xbe : (short)0x96;
        *(short*)(e + 0x28) += grow;
        *(short*)(e + 0x2a) += grow;

        if (lift > 0x77) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0044be40 @ 0x0044be40 — <b>l'eclair de Micro</b> : le sous-type <c>0x92</c>,
    /// creneau 146 de <see cref="s_StaticEntityHandlers_00551f30"/>. 177 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; le releve est garde sous <c>Legacy/Raw/Blaze/FUN_0044BE40.asm</c>.
    ///
    /// <para>
    /// <b>Il ignore la position que le sort lui a donnee</b> — le <c>0xdac</c> de l'origine — et
    /// recalcule la sienne des sa premiere image depuis son <c>+0x78</c>, une tete au-dessus
    /// (<c>0x46000</c>). C'est ce qui rend le detour du sort par l'origine invisible.
    /// </para>
    /// <para>
    /// <b>Cinq images de flash, puis la resorption</b> : <c>+0x12</c> compte les images, la teinte
    /// monte de <c>+70</c> tant qu'il est sous <c>5</c> et descend de <c>-30</c> ensuite, jusqu'a
    /// ce que le canal rouge <c>+0x38</c> retombe a zero. Le cap <c>+0x32</c> est avance de
    /// <c>0x300</c> une fois pour toutes en naissant, et l'echelle <c>+0x34</c> grandit de
    /// <c>0x12c</c> par image sur les deux chemins.
    /// </para>
    /// </summary>
    private static void TickMicroFlash_0044BE40(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + 0x46000;
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(ushort*)(e + 0x32) += 0x300;
            e[0x04] = 0x11;
            e[0x0a] = 0x0f;
            e[0x0b] = 0;
            *(short*)(e + 0x34) = 0x200;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x2e) = -60;
            *(short*)(e + 0x12) = 0;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
        }

        // Ghidra: LAB_0044bea5
        if (*(short*)(e + 0x10) != 1) return;

        *(ushort*)(e + 0x34) += 0x12c;

        if (*(short*)(e + 0x12) < 5)
        {
            BrightenEntityTint_00404DE0(e, 0x46, 0x46, 0x46);
            *(short*)(e + 0x12) += 1;
            return;
        }

        // Ghidra: LAB_0044bece
        BrightenEntityTint_00404DE0(e, -30, -30, -30);

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;

        // Ghidra: LAB_0044beea — les deux chemins comptent l'image.
        *(short*)(e + 0x12) += 1;
    }

    // =====================================================================
    // COUP DE POUCE — le quarante-neuvieme sort, ecole 1 indice 19, sous-type 0x31
    //
    //   0x0044BF00   LE SORT. Sous-type 0x31 de 0x00551c30. Decompile dans Blaze.c.
    //   0x0044C0D0   SON ENVELOPPE D'EFFET. Cinquante-huit octets, decompilee dans Blaze.c.
    //   0x0044C110   son etincelle, sous-type 0x93 de 0x00551f30. 333 octets, ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044C110.asm.
    //   0x0044C260   sa spirale, sous-type 0x91 de 0x00552230. Decompilee dans Blaze.c.
    //
    // LE PREMIER SORT DE L'ECOLE QUI FRAPPE TOUT LE GROUPE. Les quarante-huit precedents visent une
    // entite — celle qu'AcquireHolyTarget a retenue, ou toutes les entites de SCENE pour Zombie.
    // Coup de pouce, lui, balaie les QUATRE EMPLACEMENTS DU GROUPE (0x005e93c0, pas 0x005ecaa0) et
    // pose l'effet 33 sur chaque membre vivant. C'est un sort d'appui, pas une attaque.
    //
    // ET C'EST LA TROISIEME COPIE DE LA MEME ENVELOPPE. 0x0044B910 (Delivrance), 0x0044BC80 (Micro)
    // et 0x0044C0D0 sont trois corps de 58 octets identiques instruction pour instruction, aux
    // deplacements de leurs deux `call` pres — et le premier des deux resout vers le meme
    // 0x004364b0 dans les trois. Le compilateur a emis le meme patron trois fois de suite.
    //
    // IL DEBOUCHE DEUX GESTIONNAIRES QUE D'AUTRES FICHES ATTENDAIENT. Le sous-type 0x93 est celui
    // que le trait de Pardon (TickForgiveBolt_0044F290) engendre par paires depuis qu'il a ete
    // ecrit, sans corps derriere. Et le 0x91 de la table 0x00552230 n'est PAS celui de la table
    // 0x00551f30 : le petale de Delivrance porte le meme numero dans l'autre table.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044c0d0 @ 0x0044c0d0 — <b>poser l'effet d'etat 33 si la cible l'accepte</b>.
    /// Cinquante-huit octets, decompilee dans <c>Blaze.c</c> : le garde-fou
    /// <see cref="IsEffectPairBlocked_004364B0"/>, puis <see cref="ApplyStatusEffect_0053C6B0"/>
    /// s'il a laisse passer.
    ///
    /// <para>
    /// <b>La troisieme copie du meme corps</b>, apres
    /// <see cref="ClearStatusEffectIfEligible_0044B910"/> et
    /// <see cref="ClearStatusEffectIfEligible_0044BC80"/> : les trois sont identiques instruction
    /// pour instruction, aux deplacements de leurs deux <c>call</c> pres, et le premier des deux
    /// resout vers <c>0x004364b0</c> dans les trois cas. Seul l'effet appele change — ici un
    /// <b>poseur</b> et non une levee, le seul des trois.
    /// </para>
    /// </summary>
    private static bool ApplyStatusEffectIfEligible_0044C0D0(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ApplyStatusEffect_0053C6B0(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044bf00 @ 0x0044bf00 — <b>Coup de pouce</b> : le sous-type <c>0x31</c>, creneau
    /// 49 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>vingtieme fiche de l'ecole
    /// 1</b>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Le premier sort de l'ecole qui prend tout le groupe.</b> Il balaie les quatre
    /// emplacements de <see cref="Input.s_SlotFlags_005e93c0"/> — la table du <b>groupe</b>, pas
    /// celle des entites de scene — et retient chaque membre vivant (<c>0x80000000</c> present,
    /// <c>0x50000000</c> absent). A la <b>dixieme</b> image il pose sur chacun l'effet d'etat 33
    /// par <see cref="ApplyStatusEffectIfEligible_0044C0D0"/> (bit <c>0x1000000</c>, duree en
    /// <c>+0xc2</c>) et fait naitre <b>deux</b> etincelles <c>0x93</c> par membre.
    /// </para>
    /// <para>
    /// <b>Il colle a son LANCEUR, pas a sa cible.</b> C'est <c>+0x74</c> qu'il recopie chaque image
    /// dans sa position, la ou toutes les fiches precedentes de l'ecole recopient <c>+0x78</c>.
    /// <c>+0x78</c> lui sert d'autre chose : de curseur de balayage, reecrit a chaque membre
    /// examine — et c'est la valeur du <b>dernier</b> membre retenu qui y reste apres la boucle.
    /// </para>
    /// <para>
    /// <b>Les spirales alternent d'un cote et de l'autre.</b> Chaque image jusqu'a la quatorzieme,
    /// une spirale <c>0x91</c> nait sur chaque membre vivant ; <c>+0x9a</c>, qui porte leur angle
    /// de depart, bascule entre <c>0x800</c> et <c>0</c> selon la parite de <c>+0x16</c>. Le
    /// <c>+0x5c = 1</c> pose juste avant chaque naissance est lu par la spirale elle-meme, qui n'y
    /// prend qu'un couple de champs d'affichage.
    /// </para>
    /// <para>Cinquante images en tout.</para>
    /// </summary>
    private static void TickHelpingHand_0044BF00(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // LE LANCEUR, et non la cible : c'est +0x74 que ce sort recopie.
        byte* caster = *(byte**)(e + 0x74);
        if (caster == null) caster = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
        *(int*)(e + 0x20) = *(int*)(caster + 0x20);
        *(int*)(e + 0x24) = *(int*)(caster + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;
        }

        // L'angle de depart des spirales bascule d'une image a l'autre.
        *(ushort*)(e + 0x9a) = (*(ushort*)(e + 0x16) & 1) == 0 ? (ushort)0x800 : (ushort)0;

        if (*(ushort*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x16, 0x0095, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x30, 0x0200, 0x0a00, 0x0a00);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x2f, 0x0653, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 3) Aud.PlaySoundEffect(0x2c, 0x0200, 0x1000, 0x1000);

        // L'EFFET, a la dixieme image, sur chaque membre vivant du groupe.
        if (*(short*)(e + 0x16) == 10)
        {
            byte* member = Inp.s_SlotFlags_005e93c0;

            for (int slot = 0; slot < 4; slot++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                *(byte**)(e + 0x78) = member;

                // La paire famille/indice est pretee au lanceur avant chaque pose, membre par
                // membre — le binaire relit +0x74 pour chacune des deux ecritures.
                (*(byte**)(e + 0x74))[6] = e[6];
                (*(byte**)(e + 0x74))[7] = e[7];

                ApplyStatusEffectIfEligible_0044C0D0(
                    *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);

                SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);
                SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);
            }
        }

        // LES SPIRALES, chaque image jusqu'a la quatorzieme, une par membre vivant.
        if (*(short*)(e + 0x16) < 0x0e)
        {
            byte* member = Inp.s_SlotFlags_005e93c0;

            for (int slot = 0; slot < 4; slot++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

                *(byte**)(e + 0x78) = member;
                *(int*)(e + 0x5c) = 1;   // lu par la spirale en naissant

                SpawnStatusEffectEntity_0047AC40(
                    e, (int*)(member + 0x1c), null, 0x91, 0, -1);
            }
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) == 0x32) EndSpellAndResetDecor_00448020(e);
    }

    /// <summary>
    /// Ghidra: FUN_0044c110 @ 0x0044c110 — <b>l'etincelle de benediction</b> : le sous-type
    /// <c>0x93</c>, creneau 147 de <see cref="s_StaticEntityHandlers_00551f30"/>. 333 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044C110.asm</c>.
    ///
    /// <para>
    /// <b>Deux fiches l'engendrent</b>, toujours par paires : <see cref="TickForgiveBolt_0044F290"/>
    /// — le trait de Pardon, qui l'attendait sans corps depuis sa transcription — et
    /// <see cref="TickHelpingHand_0044BF00"/>, sur chaque membre du groupe. Le nom est donc neutre
    /// a dessein.
    /// </para>
    /// <para>
    /// <b>Elle lache sa cible quand celle-ci meurt.</b> Si le porteur porte <c>0x50000000</c>, ou
    /// si elle-meme porte deja <c>0x2000000</c>, elle cesse de recopier sa position et se marque
    /// <c>0x2000000</c> — le drapeau d'extinction. Sinon elle colle au porteur, une tete au-dessus
    /// (<c>0x46000</c>).
    /// </para>
    /// <para>
    /// <b>Tout son etat est tire au sort</b> : les trois rotations sur un tour complet, et deux
    /// vitesses de rotation dans <c>[-150, 149]</c>. Huit images a <c>+50</c> de teinte, puis
    /// <c>-7 / -7 / -4</c> — le seul degrade asymetrique de la famille — jusqu'a ce que son canal
    /// <b>bleu</b> <c>+0x3a</c> retombe a zero. Ses sœurs surveillent le rouge <c>+0x38</c> ;
    /// celle-ci surveille le canal qui descend le moins vite, donc elle vit plus longtemps.
    /// </para>
    /// </summary>
    private static void TickBlessingSpark_0044C110(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // Ghidra: LAB_0044c124 — le porteur mourant, ou le drapeau deja pose, detachent l'etincelle.
        if ((*(uint*)anchor & 0x50000000) != 0 || (*(uint*)e & 0x2000000) != 0)
        {
            *(uint*)e |= 0x2000000;
        }
        else
        {
            *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
            *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + 0x46000;
            *(int*)(e + 0x24) = *(int*)(anchor + 0x24);
        }

        // Ghidra: LAB_0044c154
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x1d;
            e[0x0a] = 3;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x28) = 0x1f40;
            *(short*)(e + 0x2a) = 0x1f40;
            *(short*)(e + 0x2c) = 0x1f40;

            *(short*)(e + 0x30) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x34) = (short)(CrtRand() % 0x1000);

            *(short*)(e + 0x12) = (short)(CrtRand() % 300 - 0x96);
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x2e) = -60;
            *(short*)(e + 0x14) = (short)(CrtRand() % 300 - 0x96);
        }

        // Ghidra: LAB_0044c206
        if (*(short*)(e + 0x10) != 1) return;

        short spinA = *(short*)(e + 0x12);
        short spinB = *(short*)(e + 0x14);

        *(short*)(e + 0x30) += spinA;
        *(short*)(e + 0x32) += spinB;
        *(short*)(e + 0x34) -= spinA;

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) <= 7)
        {
            BrightenEntityTint_00404DE0(e, 0x32, 0x32, 0x32);
            return;
        }

        // Ghidra: LAB_0044c23e — le degrade asymetrique, et la garde sur le BLEU.
        BrightenEntityTint_00404DE0(e, -7, -7, -4);

        if (e[0x3a] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0044c260 @ 0x0044c260 — <b>la spirale de Coup de pouce</b> : le sous-type
    /// <c>0x91</c> de <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c>
    /// ou depose <see cref="SpawnStatusEffectEntity_0047AC40"/>. <b>Decompilee dans
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Meme numero de sous-type que le petale de Delivrance, autre table, autre corps</b> :
    /// <see cref="TickDeliverancePetal_0044BA00"/> est le <c>0x91</c> de <c>0x00551f30</c>, celui
    /// que <see cref="SpawnCastEntity_0047AAE0"/> atteint ; celui-ci est le <c>0x91</c> de
    /// <c>0x00552230</c>. Rien ne les relie.
    /// </para>
    /// <para>
    /// <b>Elle monte en tournant, et son rayon s'ouvre.</b> L'angle <c>+0x12</c> — pris de
    /// <c>+0x9a</c>, que le sort fait basculer d'une image a l'autre — avance de <c>0xfa</c> et se
    /// replie sur le tour ; le rayon <c>+0x14</c> part de <c>0x32</c> et gagne <c>1</c> par image ;
    /// la hauteur vaut <c>+0x16 * 0x2000</c>. Le centre est le <b>porteur vivant</b>, recopie en
    /// tete de chaque image dans <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> — la spirale suit donc son
    /// membre du groupe, contrairement a la bille de Micro qui reste accrochee a un instantane.
    /// </para>
    /// <para>
    /// <b>Deux ruptures a mi-course, a la vingt-cinquieme image</b> : l'echelle cesse de croitre de
    /// <c>0xa0</c> et se met a decroitre d'autant, et le rayon passe a <c>+3</c> par image des
    /// qu'il depasse <c>0x1e</c> — elle s'ecarte donc plus vite en s'effaçant. Quarante images en
    /// tout.
    /// </para>
    /// <para>
    /// <c>+0x5c</c>, que le sort pose a <c>1</c> avant chaque naissance, ne sert qu'a un couple de
    /// champs d'affichage (<c>+0x0a = 1</c>, <c>+0x0b = 0</c>) et n'est lu nulle part ailleurs.
    /// </para>
    /// </summary>
    private static void TickHelpingHandSpiral_0044C260(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // Le centre de la spirale, relu a chaque image : elle suit son porteur.
        *(int*)(e + 0x50) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x54) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x58) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x30;   // Ghidra: &DAT_00551558
            e[0x40] = 0;

            if (*(int*)(e + 0x5c) == 1)
            {
                e[0x0a] = 1;
                e[0x0b] = 0;
            }

            *(short*)(e + 0x10) = 1;
            *(ushort*)(e + 0x12) = *(ushort*)(e + 0x9a);   // l'angle de depart, pose par le sort

            // Le centre est repris de sa propre position, ecrasant la copie du porteur ci-dessus.
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(short*)(e + 0x14) = 0x32;
            *(short*)(e + 0x16) = 0;
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x2e) = -60;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        int angle = *(short*)(e + 0x12);
        short radius = *(short*)(e + 0x14);
        short step = *(short*)(e + 0x16);

        // Ghidra: Ordinal_451 / Ordinal_450.
        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(angle) * radius + *(int*)(e + 0x50);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(angle) * radius + *(int*)(e + 0x58);
        *(int*)(e + 0x20) = step * 0x2000 + *(int*)(e + 0x54);

        *(short*)(e + 0x12) = (short)(angle + 0xfa);
        if (*(short*)(e + 0x12) > 0x0fff) *(short*)(e + 0x12) -= 0x1000;

        *(short*)(e + 0x14) = (short)(radius + 1);

        if (step < 0x19)
        {
            *(short*)(e + 0x28) += 0xa0;
            *(short*)(e + 0x2a) += 0xa0;
        }
        else
        {
            *(short*)(e + 0x28) -= 0xa0;
            *(short*)(e + 0x2a) -= 0xa0;

            // Passe la moitie, le rayon s'ouvre trois fois plus vite — mais seulement une fois
            // qu'il a depasse 0x1e, seuil que le rayon franchit des la trentieme image.
            if ((short)(radius + 1) > 0x1e) *(short*)(e + 0x14) = (short)(radius + 3);
        }

        *(short*)(e + 0x16) = (short)(step + 1);

        if ((short)(step + 1) > 0x27) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // REMONTANT — le cinquantieme sort, ecole 1 indice 20, sous-type 0x32
    //
    //   0x0044C3A0   LE SORT. Sous-type 0x32 de 0x00551c30, 837 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044C3A0.asm.
    //   0x0044C6F0   SON ENVELOPPE D'EFFET. Cinquante-huit octets, decompilee dans Blaze.c.
    //   0x0044C730   son rai, sous-type 0x94 de 0x00551f30. Decompile dans Blaze.c.
    //
    // C'EST LA GRANDE LEVEE : neuf effets d'un coup. Delivrance leve l'etat 9, Micro l'etat 10 ;
    // Remontant appelle ClearStatusEffect_0053C980, qui efface les bits 0x37f du mot d'etat et
    // remet neuf durees a zero. Il coute 62 PM la ou les deux autres en coutent 40, et c'est la
    // seule fiche de l'ecole a lever plus d'un effet.
    //
    // ET C'EST LA QUATRIEME COPIE DE LA MEME ENVELOPPE. 0x0044B910, 0x0044BC80, 0x0044C0D0 et
    // 0x0044C6F0 : quatre corps de 58 octets identiques instruction pour instruction, aux
    // deplacements de leurs deux `call` pres, le premier des deux resolvant vers 0x004364b0 dans
    // les quatre cas. Le patron a ete emis une fois par fiche.
    //
    // UN SEUL CORPS D'ENFANT NEUF SUR QUATRE SOUS-TYPES. Il en engendre quatre — 0x94, 0x95, 0x93
    // et 0x92 — mais trois etaient deja ecrits : 0x95 est l'anneau de Calgon, 0x93 l'etincelle de
    // Coup de pouce et de Pardon, 0x92 l'eclair de Micro. Seul le 0x94 restait, et il ne cherche
    // pas la hauteur de sa cible mais SON PLANCHER, +0x60 — les huit exemplaires se rangent donc
    // sur le sol et non sur le corps.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044c6f0 @ 0x0044c6f0 — <b>lever les neuf effets si la cible l'accepte</b>.
    /// Cinquante-huit octets, decompilee dans <c>Blaze.c</c> : le garde-fou
    /// <see cref="IsEffectPairBlocked_004364B0"/>, puis <see cref="ClearStatusEffect_0053C980"/>
    /// s'il a laisse passer.
    ///
    /// <para>
    /// <b>La quatrieme copie du meme corps</b>, apres
    /// <see cref="ClearStatusEffectIfEligible_0044B910"/>,
    /// <see cref="ClearStatusEffectIfEligible_0044BC80"/> et
    /// <see cref="ApplyStatusEffectIfEligible_0044C0D0"/> : les quatre sont identiques instruction
    /// pour instruction, aux deplacements de leurs deux <c>call</c> pres, et le premier des deux
    /// resout vers <c>0x004364b0</c> dans les quatre cas.
    /// </para>
    /// </summary>
    private static bool ClearAllStatusEffectsIfEligible_0044C6F0(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053C980(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044c3a0 @ 0x0044c3a0 — <b>Remontant</b> : le sous-type <c>0x32</c>, creneau 50
    /// de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>vingt et unieme fiche de l'ecole
    /// 1</b>. 837 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>La grande levee de l'ecole.</b> Ses deux voisines n'enlevent qu'un effet chacune —
    /// <see cref="TickDeliverance_0044B7B0"/> l'etat 9, <see cref="TickMicro_0044BAF0"/> l'etat 10 ;
    /// celle-ci appelle <see cref="ClearStatusEffect_0053C980"/>, qui efface les bits <c>0x37f</c>
    /// du mot d'etat et remet <b>neuf</b> durees a zero. Elle coute 62 PM contre 40 pour les deux
    /// autres. La levee tombe a la soixantieme image, et le lanceur est relache dans la foulee.
    /// </para>
    /// <para>
    /// <b>Le lanceur est tenu du debut a la fin</b> par le bit <c>0x80</c>, pose a la premiere image
    /// et retire apres <see cref="EndSpellAndResetDecor_00448020"/> — le seul corps de l'ecole a le
    /// relacher <b>apres</b> son extinction et non a une image fixee d'avance.
    /// </para>
    /// <para>
    /// <b>Huit rais d'un coup a la naissance</b>, poses en deux series de quatre par les champs
    /// <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, que le rai relit comme un ecart a son porteur :
    /// quatre a <c>0xa0000</c> au-dessus du sol, aux quatre quarts de tour, echelle cible
    /// <c>0x18b0</c> ; puis quatre a <c>0xa000</c> seulement, ecartes de <c>±0xad000</c> sur X et
    /// sur Z, echelle cible <c>0x1a40</c>. Les huit naissent dans la meme image.
    /// </para>
    /// <para>
    /// <b>Puis quatre rendez-vous</b> : a la vingtieme image un anneau <c>0x95</c> (celui de
    /// Calgon), a la vingt et unieme deux etincelles <c>0x93</c> (celles de Coup de pouce et de
    /// Pardon), et aux vingt-deuxieme et vingt-sixieme un eclair <c>0x92</c> (celui de Micro). Le
    /// sort prete a chacun ses propres champs d'echelle et de rotation, qu'il ne repose pas.
    /// </para>
    /// </summary>
    private static void TickRestorative_0044C3A0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        // Ghidra: LAB_0044c3c5 — le sort colle a sa cible, sans decalage vertical.
        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            // LE LANCEUR EST TENU, et il ne sera relache qu'a l'extinction.
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;
        }

        // Ghidra: LAB_0044c400 .. LAB_0044c4be — sept coups, a l'image pres.
        if (*(short*)(e + 0x16) == 0x00) Aud.PlaySoundEffect(0x0a, 0x0800, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x01) Aud.PlaySoundEffect(0x0f, 0x0400, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x02) Aud.PlaySoundEffect(0x10, 0x07a7, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x15) Aud.PlaySoundEffect(0x2f, 0x0b52, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x16) Aud.PlaySoundEffect(0x2f, 0x0bfc, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x17) Aud.PlaySoundEffect(0x30, 0x0329, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x18) Aud.PlaySoundEffect(0x2d, 0x0200, 0x1000, 0x1000);

        // Ghidra: LAB_0044c4de — LES HUIT RAIS, tous dans la meme image. Chacun lit l'ecart que le
        // sort vient de poser dans +0x50/+0x54/+0x58 et l'echelle cible dans +0x9a.
        if (*(short*)(e + 0x10) == 1)
        {
            // La premiere serie : quatre rais hauts, aux quatre quarts de tour.
            *(ushort*)(e + 0x9a) = 0x18b0;
            *(int*)(e + 0x50) = 0;
            *(int*)(e + 0x58) = 0;
            *(int*)(e + 0x54) = 0xa0000;
            *(short*)(e + 0x34) = 0x05dc;

            *(short*)(e + 0x32) = 0x0000;
            SpawnCastEntity_0047AAE0(e, 0x94, 0, -1);

            *(short*)(e + 0x32) = 0x0400;
            SpawnCastEntity_0047AAE0(e, 0x94, 0, -1);

            *(short*)(e + 0x32) = 0x0800;
            SpawnCastEntity_0047AAE0(e, 0x94, 0, -1);

            *(short*)(e + 0x32) = 0x0c00;
            SpawnCastEntity_0047AAE0(e, 0x94, 0, -1);

            // La seconde : quatre rais bas, ecartes de 0xad000 sur les deux axes horizontaux.
            *(ushort*)(e + 0x9a) = 0x1a40;
            *(int*)(e + 0x54) = 0xa000;
            *(short*)(e + 0x34) = 0x0400;

            *(int*)(e + 0x58) = 0x000ad000;
            *(short*)(e + 0x32) = 0x0200;
            SpawnCastEntity_0047AAE0(e, 0x94, 0, -1);

            *(int*)(e + 0x58) = unchecked((int)0xfff53000);   // -0xad000
            *(short*)(e + 0x32) = unchecked((short)0xfa00);   // -0x600
            SpawnCastEntity_0047AAE0(e, 0x94, 0, -1);

            *(int*)(e + 0x58) = 0;
            *(int*)(e + 0x50) = 0x000ad000;
            *(short*)(e + 0x32) = 0x0600;
            SpawnCastEntity_0047AAE0(e, 0x94, 0, -1);

            *(int*)(e + 0x50) = unchecked((int)0xfff53000);
            *(short*)(e + 0x32) = unchecked((short)0xfe00);   // -0x200
            SpawnCastEntity_0047AAE0(e, 0x94, 0, -1);

            *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_0044c5f2 — tout le reste de la vie du sort.
        if (*(short*)(e + 0x10) != 2) return;

        *(short*)(e + 0x16) += 1;

        // L'anneau de Calgon, une fois.
        if (*(short*)(e + 0x16) == 0x14)
        {
            *(short*)(e + 0x28) = 0x1310;
            *(short*)(e + 0x2c) = 0x1310;
            *(short*)(e + 0x2a) = 0x1040;
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x32) = 0x0200;
            *(short*)(e + 0x34) = 0;

            SpawnCastEntity_0047AAE0(e, 0x95, 0, -1);
        }

        // Les deux etincelles.
        if (*(short*)(e + 0x16) == 0x15)
        {
            SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);
            SpawnCastEntity_0047AAE0(e, 0x93, 0, -1);
        }

        // L'eclair, deux fois, a quatre images d'ecart.
        if (*(short*)(e + 0x16) == 0x16 || *(short*)(e + 0x16) == 0x1a)
        {
            *(short*)(e + 0x28) = 0x0bb8;
            *(short*)(e + 0x2a) = 0x0bb8;
            *(short*)(e + 0x2c) = 0x0bb8;
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x34) = 0;
            *(short*)(e + 0x32) = 0x0200;

            SpawnCastEntity_0047AAE0(e, 0x92, 0, -1);
        }

        // Ghidra: LAB_0044c6a3 — LA LEVEE, a la soixantieme image.
        if (*(short*)(e + 0x16) == 0x3c)
        {
            // La paire famille/indice est pretee au lanceur ; le binaire relit +0x74 pour chacune
            // des deux ecritures.
            (*(byte**)(e + 0x74))[6] = e[6];
            (*(byte**)(e + 0x74))[7] = e[7];

            ClearAllStatusEffectsIfEligible_0044C6F0(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);

            EndSpellAndResetDecor_00448020(e);

            // Et le lanceur est relache APRES l'extinction — le seul de l'ecole a le faire ici.
            *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044c730 @ 0x0044c730 — <b>le rai de Remontant</b> : le sous-type <c>0x94</c>,
    /// creneau 148 de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>Decompile dans
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Il se range sur le PLANCHER de son porteur, pas sur son corps.</b> Sa position horizontale
    /// vient de <c>+0x1c</c>/<c>+0x24</c> du porteur, mais sa hauteur de <c>+0x60</c> — la cote du
    /// sol que <see cref="FinalizeEntityPlacement2"/> depose sous chaque entite. C'est le seul corps
    /// de l'ecole a lire ce champ, et c'est ce qui range les huit exemplaires au sol autour de la
    /// cible plutot qu'a hauteur de poitrine. S'y ajoute l'ecart propre a chacun, que le sort a pose
    /// dans <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> avant de l'engendrer.
    /// </para>
    /// <para>
    /// <b>Une colonne mince qui s'etire.</b> Les deux echelles horizontales restent a <c>300</c> et
    /// la verticale part de zero pour <b>approcher</b> d'un quart par image la valeur que le sort a
    /// laissee dans <c>+0x9a</c> — <c>0x18b0</c> pour les quatre du haut, <c>0x1a40</c> pour les
    /// quatre du bas. Passe la quarantieme image il s'assombrit de <c>-10 / -10 / -3</c> et rend la
    /// main quand son canal <b>bleu</b> <c>+0x3a</c> retombe a zero — la meme garde que
    /// <see cref="TickBlessingSpark_0044C110"/>, sur le canal qui descend le moins vite.
    /// </para>
    /// </summary>
    private static void TickRestorativeBeam_0044C730(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // LE PLANCHER, +0x60, et non la hauteur du corps.
        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c) + *(int*)(e + 0x50);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60) + *(int*)(e + 0x54);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24) + *(int*)(e + 0x58);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x28) = 300;
            *(short*)(e + 0x2c) = 300;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2e) = -60;
            *(short*)(e + 0x16) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x16) += 1;

        // L'echelle verticale approche d'un quart la cible que le sort a laissee en +0x9a.
        *(short*)(e + 0x2a) +=
            (short)((*(short*)(e + 0x9a) - *(short*)(e + 0x2a)) >> 2);

        if (*(short*)(e + 0x16) > 0x28)
        {
            BrightenEntityTint_00404DE0(e, -10, -10, -3);

            if ((sbyte)e[0x3a] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    // =====================================================================
    // MIRACLE — le cinquante et unieme sort, ecole 1 indice 21, sous-type 0x33
    //
    //   0x0044C8A0   LE SORT. Sous-type 0x33 de 0x00551c30. Decompile dans Blaze.c.
    //   0x0044CA80   SON ENVELOPPE D'EFFET. Cinquante-huit octets, decompilee dans Blaze.c.
    //   0x0044CAC0   sa colonne, sous-type 0x96 de 0x00551f30. Decompilee dans Blaze.c.
    //   0x0044CBD0   son aureole, sous-type 0x97 de 0x00551f30. Decompilee dans Blaze.c.
    //
    // TOUTE LA FICHE EST DANS BLAZE.C — la premiere de la serie depuis Priere. Rien a
    // desassembler : le sort, son enveloppe et ses deux enfants sont decompiles.
    //
    // IL LEVE LE SEUL EFFET QUI COMPTE AU LIEU DE POSER UN BIT. ClearStatusEffect_0053CA30 ne
    // touche aucun bit du mot d'etat : elle remet a zero la duree +0xc0 et le COMPTEUR +0xec, parce
    // que l'effet 32 (ApplyStatusEffect_0053C5E0) s'empile jusqu'a trois fois au lieu de se marquer
    // d'un bit. Miracle efface donc les trois marques d'un coup.
    //
    // ET C'EST LA CINQUIEME COPIE DE LA MEME ENVELOPPE. 0x0044B910, 0x0044BC80, 0x0044C0D0,
    // 0x0044C6F0 et 0x0044CA80 : cinq corps de 58 octets identiques instruction pour instruction,
    // aux deplacements de leurs deux `call` pres, le premier resolvant vers 0x004364b0 dans les
    // cinq cas. Cinq fiches consecutives, cinq copies.
    //
    // SES DEUX ENFANTS SE POSENT SUR LE PLANCHER, comme le rai de Remontant : la hauteur vient de
    // +0x60 du porteur et non de +0x20. Deux colonnes emboitees, trois aureoles tournantes.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044ca80 @ 0x0044ca80 — <b>lever l'effet 32 si la cible l'accepte</b>.
    /// Cinquante-huit octets, decompilee dans <c>Blaze.c</c> : le garde-fou
    /// <see cref="IsEffectPairBlocked_004364B0"/>, puis <see cref="ClearStatusEffect_0053CA30"/>
    /// s'il a laisse passer.
    ///
    /// <para>
    /// <b>La cinquieme copie du meme corps</b>, apres
    /// <see cref="ClearStatusEffectIfEligible_0044B910"/>,
    /// <see cref="ClearStatusEffectIfEligible_0044BC80"/>,
    /// <see cref="ApplyStatusEffectIfEligible_0044C0D0"/> et
    /// <see cref="ClearAllStatusEffectsIfEligible_0044C6F0"/> : les cinq sont identiques
    /// instruction pour instruction, aux deplacements de leurs deux <c>call</c> pres, et le premier
    /// des deux resout vers <c>0x004364b0</c> dans les cinq cas.
    /// </para>
    /// </summary>
    private static bool ClearCountingEffectIfEligible_0044CA80(uint* caster, uint* victim, int keep)
    {
        if (IsEffectPairBlocked_004364B0(caster, victim) != 0) return true;

        return ClearStatusEffect_0053CA30(caster, victim, keep) != 0;
    }

    /// <summary>
    /// Ghidra: FUN_0044c8a0 @ 0x0044c8a0 — <b>Miracle</b> : le sous-type <c>0x33</c>, creneau 51 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>vingt-deuxieme fiche de l'ecole 1</b>.
    /// <b>Decompilee dans <c>Blaze.c</c></b>, ainsi que ses trois compagnes — la premiere fiche de
    /// la serie a ne rien devoir au desassembleur depuis Priere.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Il leve le seul effet qui compte.</b> <see cref="ClearStatusEffect_0053CA30"/> ne touche
    /// aucun bit du mot d'etat : elle remet a zero la duree <c>+0xc0</c> et le <b>compteur</b>
    /// <c>+0xec</c>, parce que l'effet 32 (<see cref="ApplyStatusEffect_0053C5E0"/>) s'empile
    /// jusqu'a trois fois au lieu de se marquer d'un bit. Miracle efface donc les trois marques
    /// d'un coup. La levee tombe a la soixante-dixieme image.
    /// </para>
    /// <para>
    /// <b>Cinq enfants a la naissance, tous dans la meme image</b> : deux colonnes <c>0x96</c>
    /// emboitees (echelles cibles <c>4000</c> puis <c>2000</c>) et trois aureoles <c>0x97</c>
    /// (<c>0x157c</c> deux fois, puis <c>0x1964</c>). L'echelle cible voyage par <c>+0x9a</c>, que
    /// le sort reecrit avant chaque naissance.
    /// </para>
    /// <para>
    /// <b>Puis cinquante images de poussiere.</b> Tant que <c>+0x16 &lt; 0x32</c>, une etincelle de
    /// bouffee <c>0xa9</c> (<see cref="EntityHandler_0044FB10"/>, deja ecrite) nait chaque image.
    /// Les deux champs que le sort repose avant chacune sont exactement ceux que l'etincelle lit en
    /// naissant : <c>+0x14 = 0x50</c> lui sert de <b>borne de rayon</b>
    /// (<c>tirage % +0x14</c>) et <c>+0x9a = 1</c> devient son <c>+0x0a</c> d'affichage.
    /// </para>
    /// </remarks>
    private static void TickMiracle_0044C8A0(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 0);

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        // Quatre coups, a l'image pres.
        if (*(short*)(e + 0x16) == 0) Aud.PlaySoundEffect(0x10, 0x037e, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 1) Aud.PlaySoundEffect(0x0d, 0x0329, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 2) Aud.PlaySoundEffect(0x0f, 0x0194, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 3) Aud.PlaySoundEffect(0x11, 0x016a, 0x1000, 0x1000);

        // LES CINQ ENFANTS, tous dans l'image de naissance. L'echelle cible voyage par +0x9a.
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            *(ushort*)(e + 0x9a) = 4000;
            SpawnCastEntity_0047AAE0(e, 0x96, 0, -1);

            *(ushort*)(e + 0x9a) = 2000;
            SpawnCastEntity_0047AAE0(e, 0x96, 0, -1);

            *(ushort*)(e + 0x9a) = 0x157c;
            SpawnCastEntity_0047AAE0(e, 0x97, 0, -1);

            *(ushort*)(e + 0x9a) = 0x157c;
            SpawnCastEntity_0047AAE0(e, 0x97, 0, -1);

            *(ushort*)(e + 0x9a) = 0x1964;
            SpawnCastEntity_0047AAE0(e, 0x97, 0, -1);
        }

        // UNE ETINCELLE PAR IMAGE pendant cinquante images. Les deux champs reposes avant chacune
        // sont ceux qu'elle lit en naissant : la borne de son rayon et son champ d'affichage.
        if (*(short*)(e + 0x10) == 1 && *(short*)(e + 0x16) < 0x32)
        {
            *(ushort*)(e + 0x14) = 0x50;
            *(ushort*)(e + 0x9a) = 1;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
        }

        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) == 0x46)
        {
            // La paire famille/indice est pretee au lanceur ; le binaire relit +0x74 pour chacune
            // des deux ecritures.
            (*(byte**)(e + 0x74))[6] = e[6];
            (*(byte**)(e + 0x74))[7] = e[7];

            ClearCountingEffectIfEligible_0044CA80(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);

            EndSpellAndResetDecor_00448020(e);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044cac0 @ 0x0044cac0 — <b>la colonne de Miracle</b> : le sous-type <c>0x96</c>,
    /// creneau 150 de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>Decompilee dans
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Elle se pose sur le PLANCHER de la cible</b>, comme
    /// <see cref="TickRestorativeBeam_0044C730"/> : sa hauteur vient de <c>+0x60</c> du porteur, la
    /// cote du sol, et non de <c>+0x20</c>. Le sort en fait naitre <b>deux</b>, emboitees : leurs
    /// echelles horizontales approchent d'un quart par image les cibles <c>4000</c> et
    /// <c>2000</c> que <c>+0x9a</c> leur a portees, tandis que la verticale reste fixee a
    /// <c>30000</c>. C'est donc un fut, pas un disque.
    /// </para>
    /// <para>
    /// <b>Trente images de montee, puis la resorption.</b> Etat 1 : la teinte monte de <c>+10</c>
    /// par image, mais un second plafond la <b>rabat a <c>0xa0</c></b> — comparaison
    /// <b>non signee</b> sur le canal rouge — la ou l'aureole laisse la sienne saturer a
    /// <c>0xff</c>. Passe la vingt-neuvieme image elle bascule en etat 2 : les deux echelles
    /// horizontales perdent un huitieme d'elles-memes par image, la teinte descend de <c>-5</c>, et
    /// elle rend la main quand <c>+0x38</c> retombe a zero.
    /// </para>
    /// </summary>
    private static void TickMiraclePillar_0044CAC0(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        // LE PLANCHER, +0x60, et non la hauteur du corps.
        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 5;
            e[0x0a] = 0x0f;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x2a) = 30000;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x2e) = -60;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short target = *(short*)(e + 0x9a);

            *(short*)(e + 0x28) += (short)((target - *(short*)(e + 0x28)) >> 2);
            *(short*)(e + 0x2c) += (short)((target - *(short*)(e + 0x2c)) >> 2);

            BrightenEntityTint_00404DE0(e, 10, 10, 10);

            // Le second plafond, lu NON SIGNE : la colonne ne monte qu'a 0xa0.
            if (e[0x38] > 0xa0)
            {
                e[0x38] = 0xa0;
                e[0x39] = 0xa0;
                e[0x3a] = 0xa0;
            }

            if (*(short*)(e + 0x16) > 0x1d) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x28) += (short)(-(int)*(short*)(e + 0x28) >> 3);
            *(short*)(e + 0x2c) += (short)(-(int)*(short*)(e + 0x2c) >> 3);

            BrightenEntityTint_00404DE0(e, -5, -5, -5);

            if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }

        *(short*)(e + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0044cbd0 @ 0x0044cbd0 — <b>l'aureole de Miracle</b> : le sous-type <c>0x97</c>,
    /// creneau 151 de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>Decompilee dans
    /// <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>La jumelle plate de la colonne</b>, et posee sur le meme plancher <c>+0x60</c> : meme
    /// forme a trois etats, meme approche d'un quart, meme resorption d'un huitieme. Trois
    /// differences seulement, et elles se tiennent :
    /// </para>
    /// <list type="bullet">
    /// <item>ses echelles horizontales visent <b>10000</b> en dur, et c'est la <b>verticale</b> qui
    /// prend la valeur de <c>+0x9a</c> (<c>0x157c</c> pour deux d'entre elles, <c>0x1964</c> pour la
    /// troisieme) — l'inverse exact de la colonne ;</item>
    /// <item>elle <b>tourne</b> : son cap part d'un tirage sur le tour complet et avance de
    /// <c>0x96</c> par image, ce qui separe les trois exemplaires nes dans la meme image ;</item>
    /// <item>sa teinte n'a <b>pas</b> le plafond a <c>0xa0</c> de la colonne — elle sature a
    /// <c>0xff</c> dans <see cref="BrightenEntityTint_00404DE0"/>.</item>
    /// </list>
    /// <para>
    /// L'etat 2 resorbe en plus la verticale, que la colonne laisse intacte.
    /// </para>
    /// </summary>
    private static void TickMiracleAureole_0044CBD0(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            e[0x04] = 0x26;
            e[0x0a] = 1;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(ushort*)(e + 0x2a) = *(ushort*)(e + 0x9a);
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x2e) = -60;

            *(ushort*)(e + 0x32) = (ushort)(CrtRand() % 0x1000);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x32) += 0x96;

            *(short*)(e + 0x28) += (short)((10000 - *(short*)(e + 0x28)) >> 2);
            *(short*)(e + 0x2c) += (short)((10000 - *(short*)(e + 0x2c)) >> 2);

            BrightenEntityTint_00404DE0(e, 10, 10, 10);

            if (*(short*)(e + 0x16) > 0x1d) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x28) += (short)(-(int)*(short*)(e + 0x28) >> 3);
            *(short*)(e + 0x2c) += (short)(-(int)*(short*)(e + 0x2c) >> 3);
            *(short*)(e + 0x2a) += (short)(-(int)*(short*)(e + 0x2a) >> 3);

            BrightenEntityTint_00404DE0(e, -5, -5, -5);

            if ((sbyte)e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }

        *(short*)(e + 0x16) += 1;
    }

    // =====================================================================
    // RESURECTION — le cinquante-deuxieme sort, ecole 1 indice 22, sous-type 0x34
    // (l'orthographe est celle du jeu : une seule R)
    //
    //   0x0044CD00   LE SORT. Sous-type 0x34 de 0x00551c30, 475 octets. ABSENT de Blaze.c ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044CD00.asm.
    //   0x0044CEE0   SON ENVELOPPE D'EFFET — ET ELLE N'EST PAS LA SIXIEME COPIE. Decompilee.
    //   0x0044CF30   sa colonne, sous-type 0x98 de 0x00551f30. 249 octets, ABSENT.
    //   0x0044D030   son eclat montant, sous-type 0x99 de 0x00551f30. 315 octets, ABSENT.
    //   0x0044D170   son voile, sous-type 0x93 de 0x00552230. 507 octets, ABSENT.
    //
    // L'ENVELOPPE A DU ETRE ECRITE A PART, ET LA RAISON EST LOGIQUE. Les cinq fiches precedentes
    // partagent le meme corps de 58 octets, qui commence par IsEffectPairBlocked_004364B0 — un
    // garde-fou qui REFUSE une cible portant 0x50002000, c'est-a-dire mourante, immobilisee ou
    // prise. Une cible morte le porte. Resurection ne pouvait donc pas s'en servir : elle a son
    // propre garde, qui accepte le mort et refuse au contraire l'entite deja consommee (0x10000040)
    // ou en recul (+0x12) ou clignotante (+0x96). La seule fiche de la serie a ne pas recopier le
    // patron, et c'est la seule qui avait besoin d'autre chose.
    //
    // MEME RAISON POUR LE MODE 1 D'ACQUIREHOLYTARGET. Resurection est le seul appelant du depot a
    // passer 1 : ce mode efface temporairement 0x40000000 et 0x2000 sur les quatre membres du
    // groupe le temps du balayage, puis les repose. Autrement dit il rend les morts VISIBLES a la
    // recherche de cible, juste le temps de la faire. Le mode existait pour ce sort-la.
    //
    // ET C'EST LE PLUS LONG DE L'ECOLE : 180 images, trois secondes, contre 60 a 70 pour ses
    // voisines. Quatre coups du meme bruit 0x27 a hauteurs montantes, puis deux autres a la fin.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044cee0 @ 0x0044cee0 — <b>ressusciter si la cible l'accepte</b>. Decompilee
    /// dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle n'est PAS la sixieme copie du patron de l'ecole</b>, et c'est la seule de la serie a
    /// s'en ecarter. Les cinq precedentes
    /// (<see cref="ClearStatusEffectIfEligible_0044B910"/> et ses jumelles) appellent
    /// <see cref="IsEffectPairBlocked_004364B0"/>, qui <b>refuse</b> une cible portant l'un des
    /// bits <c>0x50002000</c> — mourante, immobilisee, prise. Une cible morte le porte : le
    /// garde-fou commun aurait donc refuse tout appel de Resurection.
    /// </para>
    /// <para>
    /// Son garde a elle est ecrit sur place et prend l'autre parti : elle refuse si la cible porte
    /// <c>0x10000040</c>, si elle n'est plus vivante au sens <c>0x80000000</c>, si elle est en
    /// recul (<c>+0x12</c>) ou si elle clignote (<c>+0x96</c>). <b>Rien sur <c>0x40000000</c></b>,
    /// le bit de mort — et c'est tout l'objet.
    /// </para>
    /// <para>
    /// La valeur rendue en cas de refus est le <c>CONCAT22</c> du binaire : <c>1</c> dans le mot
    /// bas, et le <b>mot haut du drapeau de la cible</b> dans le mot haut, laisse la par le
    /// registre. Aucun appelant ne la lit ; transcrite telle quelle.
    /// </para>
    /// </summary>
    private static uint ReviveTargetIfEligible_0044CEE0(uint* caster, uint* victim, int keep)
    {
        bool blocked = false;
        uint flags = *victim;

        if ((flags & 0x10000040) != 0 || (flags & 0x80000000) == 0) blocked = true;

        if (*(short*)((byte*)victim + 0x12) != 0 || *(short*)((byte*)victim + 0x96) != 0)
        {
            blocked = true;
        }

        if (!blocked) return ReviveTarget_0053CD80(caster, victim, keep);

        // Ghidra: CONCAT22((short)(flags >> 0x10), 1) — le mot haut est un reste de registre.
        return (flags & 0xffff0000) | 1;
    }

    /// <summary>
    /// Ghidra: FUN_0044cd00 @ 0x0044cd00 — <b>Resurection</b> : le sous-type <c>0x34</c>, creneau
    /// 52 de <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>vingt-troisieme fiche de
    /// l'ecole 1</b>. 475 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le seul appelant du depot a passer <c>1</c> a
    /// <see cref="AcquireHolyTarget_00447CB0"/>.</b> Ce mode efface temporairement
    /// <c>0x40000000</c> et <c>0x2000</c> sur les quatre membres du groupe, lance le balayage, puis
    /// repose les deux bits : il rend les <b>morts visibles</b> a la recherche de cible, le temps
    /// de la faire. Le mode n'existait que pour cette fiche.
    /// </para>
    /// <para>
    /// <b>Le plus long sort de l'ecole</b> : 180 images, contre 60 a 70 pour ses voisines. Quatre
    /// coups du <b>meme</b> bruit <c>0x27</c> a hauteurs montantes
    /// (<c>0x200</c>, <c>0x2aa</c>, <c>0x3d3</c>, <c>0x329</c>) aux images 0, 10, 40 et 50, puis
    /// deux autres aux images 128 et 129 ; une colonne <c>0x98</c> a la naissance ; et a la
    /// vingtieme image deux eclats <c>0x99</c> et un voile <c>0x93</c>.
    /// </para>
    /// <para>
    /// <b>Il tient le lanceur ET la cible</b> par le bit <c>0x80</c>, des la premiere image, et les
    /// relache tous les deux apres <see cref="EndSpellAndResetDecor_00448020"/>. Aucune autre fiche
    /// de l'ecole ne tient sa cible.
    /// </para>
    /// </summary>
    private static void TickResurrection_0044CD00(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        // LE MODE 1 : il rend les morts visibles a la recherche de cible.
        if (*(short*)(e + 0x10) == 0) AcquireHolyTarget_00447CB0(e, 1);

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        // Le meme bruit quatre fois, de plus en plus haut, puis deux autres a la fin.
        if (*(short*)(e + 0x16) == 0x00) Aud.PlaySoundEffect(0x27, 0x0200, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x0a) Aud.PlaySoundEffect(0x27, 0x02aa, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x28) Aud.PlaySoundEffect(0x27, 0x03d3, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x32) Aud.PlaySoundEffect(0x27, 0x0329, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x80) Aud.PlaySoundEffect(0x2d, 0x0200, 0x1000, 0x1000);
        if (*(short*)(e + 0x16) == 0x81) Aud.PlaySoundEffect(0x30, 0x0329, 0x1000, 0x1000);

        // Ghidra: LAB_0044ce0a — l'amorce.
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0;

            SpawnCastEntity_0047AAE0(e, 0x98, 0, -1);

            // LE LANCEUR ET LA CIBLE sont tenus tous les deux.
            *(uint*)(*(byte**)(e + 0x74)) |= 0x80;
            *(uint*)anchor |= 0x80;
        }

        // Ghidra: LAB_0044ce3f
        if (*(short*)(e + 0x10) != 1) return;

        if (*(short*)(e + 0x16) == 0x14)
        {
            SpawnCastEntity_0047AAE0(e, 0x99, 0, -1);
            SpawnCastEntity_0047AAE0(e, 0x99, 0, -1);

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0x93, 0, -1);
        }

        // Ghidra: LAB_0044ce8a
        *(short*)(e + 0x16) += 1;

        if (*(short*)(e + 0x16) == 0xb4)
        {
            // La paire famille/indice est pretee au lanceur ; le binaire relit +0x74 pour chacune
            // des deux ecritures.
            (*(byte**)(e + 0x74))[6] = e[6];
            (*(byte**)(e + 0x74))[7] = e[7];

            ReviveTargetIfEligible_0044CEE0(
                *(uint**)(e + 0x74), *(uint**)(e + 0x78), 0);

            EndSpellAndResetDecor_00448020(e);

            // Les deux sont relaches apres l'extinction.
            *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
            *(uint*)anchor &= 0xffffff7f;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044cf30 @ 0x0044cf30 — <b>la colonne de Resurection</b> : le sous-type
    /// <c>0x98</c>, creneau 152 de <see cref="s_StaticEntityHandlers_00551f30"/>. 249 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044CF30.asm</c>.
    ///
    /// <para>
    /// Posee sur le <b>plancher</b> <c>+0x60</c> du porteur, comme la colonne de Miracle et le rai
    /// de Remontant. Ses deux echelles horizontales approchent d'un quart par image la valeur
    /// <c>0x1000</c>, la verticale est <b>repinglee a <c>0x61a8</c> a chaque image</b> — pas
    /// approchee, ecrite.
    /// </para>
    /// <para>
    /// <b>Elle touche a <c>+0x36</c> en naissant</b> : <c>(x &amp; ~1) | 4</c>. C'est le seul corps
    /// de l'ecole a modifier ce mot de drapeaux d'affichage, et la valeur d'origine est celle que
    /// <see cref="SpawnCastEntity_0047AAE0"/> a recopiee du sort.
    /// </para>
    /// <para>
    /// <b>Cent quarante images de montee, puis vingt de descente</b> : la teinte monte de
    /// <c>+10</c> par image sous un plafond a <c>0x64</c> — comparaison <b>non signee</b>, le meme
    /// motif que <see cref="TickMiraclePillar_0044CAC0"/> mais a un plafond plus bas ; passe
    /// <c>0x8c</c> elle bascule en etat 2, ou la teinte tombe de <c>-20</c> jusqu'a zero.
    /// </para>
    /// </summary>
    private static void TickResurrectionColumn_0044CF30(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60);   // LE PLANCHER
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x2d;
            e[0x0a] = 7;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2c) = 0;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x34) = 0;
            *(short*)(e + 0x16) = 0;

            // Le mot de drapeaux d'affichage, lu avant tout le reste et repose en dernier.
            *(ushort*)(e + 0x36) = (ushort)((*(ushort*)(e + 0x36) & ~1) | 4);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x28) += (short)((0x1000 - *(short*)(e + 0x28)) >> 2);
            *(short*)(e + 0x2c) += (short)((0x1000 - *(short*)(e + 0x2c)) >> 2);

            // La verticale n'est pas approchee : elle est reecrite a chaque image.
            *(short*)(e + 0x2a) = 0x61a8;

            BrightenEntityTint_00404DE0(e, 10, 10, 10);

            // Le plafond, lu NON SIGNE.
            if (e[0x38] >= 0x64)
            {
                e[0x38] = 0x64;
                e[0x39] = 0x64;
                e[0x3a] = 0x64;
            }

            if (*(short*)(e + 0x16) >= 0x8c)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            BrightenEntityTint_00404DE0(e, -20, -20, -20);

            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }

        // Ghidra: LAB_0044d022 — tous les chemins comptent l'image.
        *(short*)(e + 0x16) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_0044d030 @ 0x0044d030 — <b>l'eclat montant de Resurection</b> : le sous-type
    /// <c>0x99</c>, creneau 153 de <see cref="s_StaticEntityHandlers_00551f30"/>. 315 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044D030.asm</c>.
    ///
    /// <para>
    /// <b>C'est l'eclat de Parole sainte qui monte.</b> Meme forme que
    /// <see cref="TickHolyWordShard_0044DB30"/> — trois rotations tirees sur le tour complet, deux
    /// vitesses de rotation dans <c>[-50, 49]</c>, echelle <c>0xfa0</c> — mais deux differences :
    /// il part du <b>plancher</b> <c>+0x60</c> et non du corps, et il <b>s'eleve</b>.
    /// </para>
    /// <para>
    /// <b>La montee ralentit sans jamais s'arreter</b> : <c>+0x5c</c> part de <c>250</c>, la
    /// hauteur gagne <c>+0x5c &lt;&lt; 12</c> par image, et <c>+0x5c</c> perd <c>2</c> — jusqu'a un
    /// <b>plancher a <c>30</c></b>, sous lequel il ne descend plus. L'eclat continue donc de monter
    /// lentement jusqu'a sa cent-vingt-huitieme image, ou il rend la main.
    /// </para>
    /// </summary>
    private static void TickResurrectionMote_0044D030(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60);   // LE PLANCHER
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0x1d;
            e[0x0a] = 2;
            e[0x0b] = 1;
            *(short*)(e + 0x10) = 1;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;

            *(short*)(e + 0x28) = 0x0fa0;
            *(short*)(e + 0x2a) = 0x0fa0;
            *(short*)(e + 0x2c) = 0x0fa0;

            *(short*)(e + 0x30) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);
            *(short*)(e + 0x34) = (short)(CrtRand() % 0x1000);

            *(short*)(e + 0x12) = (short)(CrtRand() % 100 - 0x32);
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x2e) = -60;
            *(int*)(e + 0x5c) = 0xfa;
            *(short*)(e + 0x14) = (short)(CrtRand() % 100 - 0x32);
        }

        // Ghidra: LAB_0044d10a
        if (*(short*)(e + 0x10) == 1)
        {
            short spinA = *(short*)(e + 0x12);
            short spinB = *(short*)(e + 0x14);

            *(short*)(e + 0x30) += spinA;
            *(short*)(e + 0x34) -= spinA;
            *(short*)(e + 0x32) += spinB;

            int climb = *(int*)(e + 0x5c);
            *(int*)(e + 0x20) += climb << 0xc;

            climb -= 2;
            *(int*)(e + 0x5c) = climb;
            if (climb < 0x1e) *(int*)(e + 0x5c) = 0x1e;

            *(short*)(e + 0x16) += 1;

            BrightenEntityTint_00404DE0(e, 8, 8, 8);
        }

        // Ghidra: LAB_0044d15a — hors du bloc d'etat : les deux chemins y passent.
        if (*(short*)(e + 0x16) == 0x80) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_0044d170 @ 0x0044d170 — <b>le voile de Resurection</b> : le sous-type
    /// <c>0x93</c> de <see cref="s_StaticEntityHandlers_00552230"/>, la table de <c>0x005f2d40</c>.
    /// 507 octets, <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044D170.asm</c>.
    ///
    /// <para>
    /// <b>Meme numero que <see cref="TickBlessingSpark_0044C110"/>, autre table, autre corps</b> :
    /// celui-la est le <c>0x93</c> de <c>0x00551f30</c>, que
    /// <see cref="SpawnCastEntity_0047AAE0"/> atteint ; celui-ci est le <c>0x93</c> de
    /// <c>0x00552230</c>.
    /// </para>
    /// <para>
    /// <b>C'est lui qui pilote le voile plein cadre</b>
    /// (<see cref="GameMemory.s_Unknown_005de024"/>), et il le fait <b>canal par canal</b> : seize
    /// images a <c>+15</c> sur les trois, ecretees a <c>0xff</c>, puis seize a <c>-15</c>, planchees
    /// a zero, et un dernier passage qui remet les trois a zero d'un coup. C'est le seul corps du
    /// depot a monter le voile par paliers plutot qu'a lui poser une couleur d'un bloc — et il
    /// <b>ecrete</b> la ou <see cref="ShiftScreenVeil"/>, l'aide des fondus de transition,
    /// <b>enroule</b>. Les deux ne sont donc pas interchangeables.
    /// </para>
    /// <para>
    /// <b>Il monte comme l'eclat</b> — meme <c>+0x5c</c> partant de <c>250</c> et perdant <c>2</c>
    /// par image, meme hauteur gagnee de <c>+0x5c &lt;&lt; 12</c> — mais chez lui le plancher a
    /// <c>30</c> fait <b>basculer l'etat</b> au lieu de simplement borner la vitesse : c'est ce qui
    /// declenche le voile. Il se marque alors <c>0x4000</c> a chaque image de la descente.
    /// </para>
    /// </summary>
    private static void TickResurrectionVeil_0044D170(byte* record)
    {
        byte* e = record;

        byte* anchor = *(byte**)(e + 0x78);
        if (anchor == null) anchor = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x60);   // LE PLANCHER
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x70;   // Ghidra: &DAT_00551598
            e[0x40] = 0;
            e[0x0a] = 7;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x5c) = 0xfa;
            *(short*)(e + 0x28) = 0;
            *(short*)(e + 0x2a) = 0;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x2e) = -60;
        }

        // Ghidra: LAB_0044d1d1 — la montee.
        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x28) += (short)((0x0bb8 - *(short*)(e + 0x28)) >> 2);
            *(short*)(e + 0x2a) += (short)((0x0bb8 - *(short*)(e + 0x2a)) >> 2);

            int climb = *(int*)(e + 0x5c);
            *(int*)(e + 0x20) += climb << 0xc;

            climb -= 2;
            *(int*)(e + 0x5c) = climb;

            if (climb < 0x1e)
            {
                // Le plancher de vitesse fait basculer l'etat, et la fonction rend la main tout de
                // suite : l'image du basculement ne passe pas par le bloc du voile.
                *(int*)(e + 0x5c) = 0x1e;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                return;
            }

            // Ghidra: LAB_0044d235
            BrightenEntityTint_00404DE0(e, 8, 8, 8);

            if (e[0x38] >= 0xa0)
            {
                e[0x38] = 0xa0;
                e[0x39] = 0xa0;
                e[0x3a] = 0xa0;
            }
        }

        // Ghidra: LAB_0044d256 — LE VOILE.
        if (*(short*)(e + 0x10) != 2) return;

        *(int*)(e + 0x20) += *(int*)(e + 0x5c) << 0xc;
        *(short*)(e + 0x28) += 0x01f4;
        *(short*)(e + 0x2a) += 0x01f4;

        if (*(short*)(e + 0x12) <= 0x0f)
        {
            // Seize images de montee, ECRETEES a 0xff — et non enroulees.
            uint veil = Mem.s_Unknown_005de024;
            int r = (byte)veil + 0x0f;
            int g = (byte)(veil >> 8) + 0x0f;
            int b = (byte)(veil >> 0x10) + 0x0f;

            if (r > 0xff) r = 0xff;
            if (g > 0xff) g = 0xff;
            if (b > 0xff) b = 0xff;

            Mem.s_Unknown_005de024 =
                0x01000000u | ((uint)b << 0x10) | ((uint)g << 8) | (uint)r;

            *(short*)(e + 0x12) += 1;
            return;
        }

        // Ghidra: LAB_0044d2e6 — la descente.
        *(uint*)e |= 0x4000;

        {
            uint veil = Mem.s_Unknown_005de024;
            int r = (byte)veil - 0x0f;
            int g = (byte)(veil >> 8) - 0x0f;
            int b = (byte)(veil >> 0x10) - 0x0f;

            if (r < 0) r = 0;
            if (g < 0) g = 0;
            if (b < 0) b = 0;

            Mem.s_Unknown_005de024 =
                0x01000000u | ((uint)b << 0x10) | ((uint)g << 8) | (uint)r;
        }

        if (*(short*)(e + 0x12) == 0x20)
        {
            *(uint*)e &= 0x7fffffff;
            Mem.s_Unknown_005de024 = 0x01000000;
        }

        // Ghidra: LAB_0044d364
        *(short*)(e + 0x12) += 1;
    }

    // =====================================================================
    // ANGE — le cinquante-troisieme et DERNIER sort de l'ecole 1, indice 23, sous-type 0x35
    //
    //   0x0044DC60   LE SORT. Sous-type 0x35 de 0x00551c30, 614 octets. Decompile dans Blaze.c.
    //   0x0044DED0   la silhouette, sous-type 0x9c de 0x00551f30. 2 919 octets, ABSENT ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044DED0.asm.
    //   0x0044EA60   la plume, sous-type 0x9d de 0x00551f30. 1 216 octets, ABSENT ;
    //                releve garde sous Legacy/Raw/Blaze/FUN_0044EA60.asm.
    //   0x0044EF40   L'ETINCELLE que la plume seme, sous-type 0x95 de 0x00552230. Decompilee.
    //                Ecrite le 2026-09-01 : c'etait un PETIT-ENFANT, et le recensement des sorts
    //                ne remonte que jusqu'a l'enfant direct d'une fiche. Ange se lisait donc
    //                « implemente » alors que le lancer plantait des que la plume semait.
    //
    // C'EST LE SEUL SORT DU DEPOT QUI DURE TANT QU'ON TIENT LE BOUTON. Les cinquante-deux autres
    // comptent des images et s'eteignent. Ange, lui, tient tant que quatre conditions restent
    // vraies, et le corps les teste toutes les images a partir de la soixantieme :
    //   - le bouton d'action remappe du lanceur (+0x1fc4 de sa fiche) est toujours enfonce ;
    //   - il lui reste des PM — DEUX toutes les dix images, preleves sur +0x14e ;
    //   - son mot d'etat +0x140 ne porte aucun des bits 0x3e8 ;
    //   - il n'est pas mort (0x40000000 sur son emplacement de groupe).
    // La premiere qui tombe pose +0x12 (0x46, ou 0x28 pour la panne de PM) et passe en etat 3, ou
    // le sort se defait en autant d'images.
    //
    // ET SON COMPTEUR D'IMAGES BOUCLE. +0x16 est ramene de 30000 a 3000 quand il deborde : une
    // invocation tenue plusieurs minutes ne casse rien. Aucune autre fiche n'en a besoin.
    //
    // QUATRE SILHOUETTES NAISSENT, UNE SEULE AGIT. Les quatre 0x9c se distinguent par le couple
    // (+0x5c, +0x9a) que le sort pose avant chaque naissance : (3,0) et (4,0) grossissent a deux
    // vitesses puis se figent dans l'etat 0x63 ; (4,0x14) retrecit et se fige aussi ; seule
    // (4,10) — celle qui monte en bleu jusqu'a 0xff — passe en etat 1 et devient l'ange qui agit.
    //
    // L'ANGE CHERCHE SIX CIBLES ET LES DONNE A SES SIX PLUMES. Il balaie le camp adverse, retient
    // jusqu'a six proies dans un tableau de pile, et emballe leurs indices par PAIRES dans
    // +0x44/+0x48/+0x4c — un saut a sept branches choisit l'arrangement selon le nombre trouve.
    // Chaque plume lit ensuite SA moitie de paire d'apres son propre +0x14 (0 a 5).
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0044dc60 @ 0x0044dc60 — <b>Ange</b> : le sous-type <c>0x35</c>, creneau 53 de
    /// <see cref="s_StaticEntityHandlers_00551c30"/> et la <b>vingt-quatrieme et derniere fiche de
    /// l'ecole 1</b>. <b>Decompilee dans <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Le seul sort du depot qui dure tant qu'on tient le bouton.</b> A partir de la soixantieme
    /// image il verifie quatre conditions a chaque tour : le <b>bouton d'action remappe</b> du
    /// lanceur (le masque <c>+0x1fc4</c> de sa fiche, croise avec l'etat vif de sa manette) est
    /// toujours enfonce ; il lui reste des PM — <b>deux toutes les dix images</b>, preleves sur
    /// <c>+0x14e</c> ; son mot d'etat <c>+0x140</c> ne porte aucun des bits <c>0x3e8</c> ; et il
    /// n'est pas mort. La premiere qui tombe pose <c>+0x12</c> et passe en etat 3.
    /// </para>
    /// <para>
    /// <b>Son compteur d'images boucle</b> : <c>+0x16</c> est ramene de <c>30000</c> a <c>3000</c>
    /// quand il deborde. Aucune autre fiche n'en a besoin — aucune autre ne peut durer.
    /// </para>
    /// <para>
    /// <b>Un lanceur hors groupe n'est soumis a rien de tout cela</b> : la branche
    /// <c>0x800000</c> absente se contente de decompter <c>+0x5c</c>, parti de <c>0x78</c>. Cent
    /// vingt images, et c'est fini.
    /// </para>
    /// <para>
    /// <b>Quatre silhouettes <c>0x9c</c> naissent d'un coup</b>, distinguees par le couple
    /// <c>(+0x5c, +0x9a)</c> que le sort pose avant chacune : <c>(3,0)</c>, <c>(4,0)</c>,
    /// <c>(4,10)</c>, <c>(4,0x14)</c>. Une seule — celle a <c>10</c> — deviendra l'ange qui agit ;
    /// les trois autres se figent dans l'etat <c>0x63</c> et ne bougent plus qu'avec lui.
    /// </para>
    /// </summary>
    private static void TickAngel_0044DC60(byte* record)
    {
        byte* e = record;

        EndSpellOnTerrain_00448040(e);

        if (*(short*)(e + 0x10) == 0)
        {
            AcquireHolyTarget_00447CB0(e, 0);

            // Le champ est RELU apres l'appel : l'amorce n'a lieu que s'il est reste nul.
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x16) = 0;
                *(short*)(e + 0x98) = 0;
                *(int*)(e + 0x5c) = 0x78;
                *(short*)(e + 0x12) = 0;
            }
        }

        // LE COMPTEUR QUI BOUCLE — la seule fiche du depot a en avoir besoin.
        *(short*)(e + 0x16) += 1;
        if (*(short*)(e + 0x16) > 30000) *(short*)(e + 0x16) = 3000;

        // Le meme bruit aux images 0 et 10 d'un cycle de trente, indefiniment.
        if (*(short*)(e + 0x98) == 0)  Aud.PlaySoundEffect(0x23, 0x0800, 0x1000, 0x1000);
        if (*(short*)(e + 0x98) == 10) Aud.PlaySoundEffect(0x23, 0x0800, 0x1000, 0x1000);

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 0x1e) *(short*)(e + 0x98) = 10;

        // LES QUATRE SILHOUETTES, toutes dans la meme image.
        if (*(short*)(e + 0x10) == 1)
        {
            *(ushort*)(e + 0x9a) = 0;
            *(int*)(e + 0x5c) = 3;
            SpawnCastEntity_0047AAE0(e, 0x9c, 0, -1);

            *(int*)(e + 0x5c) = 4;
            SpawnCastEntity_0047AAE0(e, 0x9c, 0, -1);

            *(ushort*)(e + 0x9a) = 10;
            SpawnCastEntity_0047AAE0(e, 0x9c, 0, -1);

            *(ushort*)(e + 0x9a) = 0x14;
            SpawnCastEntity_0047AAE0(e, 0x9c, 0, -1);

            *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            // Soixante images de grace : le lanceur est seulement tenu, rien n'est verifie.
            if (*(short*)(e + 0x16) < 0x3c)
            {
                *(uint*)(*(byte**)(e + 0x74)) |= 0x80;
                return;
            }

            bool sustained;

            if ((*(uint*)e & 0x800000) == 0)
            {
                // Ghidra: LAB_0044de78 — hors groupe, seule la duree compte.
                *(int*)(e + 0x5c) -= 1;
                sustained = *(int*)(e + 0x5c) > 0;
            }
            else
            {
                *(uint*)(*(byte**)(e + 0x74)) |= 0x80;

                int slot = (*(byte**)(e + 0x74))[8];
                byte* character = GetMainMemPtr() + (slot + 0x78) * 0x2000;

                // LE BOUTON TENU. Le masque du troisieme bouton d'action remappe, croise avec
                // l'etat vif de la manette assignee a l'emplacement. **Le pas de la table de
                // manettes est 0x10 et non 4** — le meme piege que celui note sur
                // <see cref="Input.PollInputAndAudio"/>, verifie ici au desassembleur
                // (`shl eax,4` en 0x0044ddfa).
                int pad = Inp.s_PlayerSlots_005e9188[slot * 0x4c];
                ushort held = *(ushort*)(Inp.s_InputStateBase_00604a20 + pad * 0x10);

                if ((*(ushort*)(character + 0x1fc4) & held) == 0)
                {
                    *(short*)(e + 0x12) = 0x46;
                    *(short*)(e + 0x10) = 3;
                }

                // LE PRIX : deux PM toutes les dix images, sur le compteur global d'images.
                if (*(uint*)Mem.Entity(0x005f2cf0) % 10 == 0)
                {
                    if (*(short*)(character + 0x14e) < 2)
                    {
                        *(short*)(character + 0x14e) = 0;
                        *(short*)(e + 0x12) = 0x28;   // la panne de PM coupe plus court
                        *(short*)(e + 0x10) = 3;
                    }
                    else
                    {
                        *(short*)(character + 0x14e) -= 2;
                    }
                }

                sustained =
                    (*(uint*)(character + 0x140) & 0x3e8) == 0
                    && (*(uint*)(Inp.s_SlotFlags_005e93c0 + slot * 0x9c) & 0x40000000) == 0;
            }

            // Ghidra: LAB_0044de85 — les deux chemins tombent ici quand la condition est rompue.
            if (!sustained)
            {
                *(short*)(e + 0x12) = 0x46;
                *(short*)(e + 0x10) = 3;
            }
        }

        // Ghidra: LAB_0044de8f — la defaite, en +0x12 images.
        if (*(short*)(e + 0x10) == 3)
        {
            *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;

            *(short*)(e + 0x12) -= 1;

            if (*(short*)(e + 0x12) < 1)
            {
                // Le binaire relit +0x74 et refait le meme masquage ; sans effet.
                *(uint*)(*(byte**)(e + 0x74)) &= 0xffffff7f;
                EndSpellAndResetDecor_00448020(e);
            }
        }
    }

    /// <summary>
    /// Les <b>six ancrages de plume</b> qu'Ange construit sur sa pile — a l'identique dans
    /// <see cref="TickAngelForm_0044DED0"/> et dans <see cref="TickAngelFeather_0044EA60"/>, qui
    /// les recopient chacun a chaque appel.
    ///
    /// <para>
    /// Trois paires symetriques, tournees par le cap de l'ange : l'ecart lateral
    /// <see cref="s_AngelWingSide"/>, l'ecart avant <see cref="s_AngelWingFront"/> et la hauteur
    /// <see cref="s_AngelWingLift"/>, cette derniere en unites entieres (elle est decalee de douze
    /// bits a l'emploi).
    /// </para>
    /// </summary>
    private static ReadOnlySpan<short> s_AngelWingSide => [0x3e, 0x3e, 0x47, 0x47, 0x54, 0x54];

    /// <summary>L'ecart avant de chaque plume — voir <see cref="s_AngelWingSide"/>.</summary>
    private static ReadOnlySpan<short> s_AngelWingFront => [-0x60, 0x60, -0x96, 0x96, -0x69, 0x69];

    /// <summary>La hauteur de chaque plume — voir <see cref="s_AngelWingSide"/>.</summary>
    private static ReadOnlySpan<short> s_AngelWingLift => [-70, -70, 0x24, 0x24, 0x8f, 0x8f];

    /// <summary>
    /// Ghidra: FUN_0044ded0 @ 0x0044ded0 — <b>la silhouette de l'ange</b> : le sous-type
    /// <c>0x9c</c>, creneau 156 de <see cref="s_StaticEntityHandlers_00551f30"/>. <b>2 919 octets,
    /// absente de <c>Blaze.c</c></b> — le plus gros corps de l'ecole ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0044DED0.asm</c>.
    ///
    /// <para>
    /// <b>Elle flotte au-dessus du lanceur</b>, gagnant chaque image un quart de l'ecart qui l'en
    /// separe, <c>0x14A000</c> plus haut, et copiant son cap. Et elle meurt avec le sort : le
    /// premier geste du corps est de lire l'etat de son <c>+0x6c</c> — la fiche Ange — et de passer
    /// en etat 3 des qu'elle y voit 3.
    /// </para>
    /// <para>
    /// <b>Quatre exemplaires naissent, un seul agit.</b> L'etat 0 se lit sur <c>+0x9a</c>, que le
    /// sort a pose : a <c>0</c> la silhouette grossit vers <c>0xF3C</c> <b>d'un pas dicte par
    /// <c>+0x5c</c></b> (un decalage, donc 3 ou 4 selon l'exemplaire) et se fige a <c>0xED8</c> dans
    /// l'etat <c>0x63</c> ; a <c>0x14</c> elle part de <c>0x1F40</c>, retrecit de <c>0xCD</c> et se
    /// fige de meme ; a <c>10</c> — et a <c>10</c> seulement — elle monte en teinte
    /// <c>(+30, +30, +60)</c> et bascule en <b>etat 1</b> des que son bleu atteint <c>0xff</c>.
    /// </para>
    /// <para>
    /// <b>L'etat 1 seme les six plumes</b> <c>0x9d</c>, chacune a son ancrage tourne par le cap
    /// (<see cref="s_AngelWingSide"/>), et chacune marquee de son rang dans <c>+0x14</c>.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est la chasse</b>, et il tourne a chaque image : une bouffee <c>0xa9</c> lachee
    /// <c>0xA0000</c> plus bas, puis le balayage du camp adverse — les <c>0x20</c> entites de scene
    /// si le lanceur est du groupe (<c>0x800000</c>), les emplacements de groupe sinon. Une proie
    /// est retenue si elle est a moins de <c>0x320</c>, si la silhouette peut l'ATTEINDRE (quatre
    /// pas d'un quart, contact <c>0x8800</c>), et si elle passe le critere d'espece
    /// <c>0x1000080</c> — le meme que Parole sainte. Chaque proie retenue prend le sort en
    /// <c>+0x74</c>.
    /// </para>
    /// <para>
    /// <b>Six places, et la septieme evince la plus lointaine.</b> Passe six proies, le corps
    /// mesure la distance de la nouvelle et la compare a chacune des retenues ; a la premiere plus
    /// lointaine qu'elle, il remplace. Puis le saut a sept branches emballe les indices retenus par
    /// paires dans <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>, ou les plumes viendront les lire.
    /// </para>
    /// <para>
    /// <b>Trois anomalies sont transcrites telles quelles</b>, toutes dans ce corps ; voir
    /// <c>Documentation/Reference/legacy-bug.md</c>.
    /// </para>
    /// </summary>
    private static void TickAngelForm_0044DED0(byte* record)
    {
        byte* e = record;

        ReadOnlySpan<short> side = s_AngelWingSide;
        ReadOnlySpan<short> front = s_AngelWingFront;
        ReadOnlySpan<short> lift = s_AngelWingLift;

        // Le tableau des proies : 0x40 octets remplis de -1 en tete d'appel, six cases utiles.
        short* picks = stackalloc short[32];
        for (int i = 0; i < 32; i++) picks[i] = -1;

        byte* spell = *(byte**)(e + 0x6c);
        if (spell == null) spell = Res.s_EntityList_005de558;

        byte* caster = *(byte**)(e + 0x74);
        if (caster == null) caster = Res.s_EntityList_005de558;

        // Ghidra: LAB_0044df95 — elle gagne un quart du chemin vers le lanceur, une hauteur d'homme
        // et demie au-dessus, et copie son cap.
        *(int*)(e + 0x1c) += (*(int*)(caster + 0x1c) - *(int*)(e + 0x1c)) >> 2;
        *(int*)(e + 0x24) += (*(int*)(caster + 0x24) - *(int*)(e + 0x24)) >> 2;
        *(int*)(e + 0x20) +=
            (*(int*)(caster + 0x20) - *(int*)(e + 0x20) + 0x14a000) >> 2;

        *(ushort*)(e + 0x32) = *(ushort*)(caster + 0x32);

        // ELLE MEURT AVEC LE SORT.
        if (*(short*)(spell + 0x10) == 3) *(short*)(e + 0x10) = 3;

        // Ghidra: LAB_0044dfe2 — l'apparition, pilotee par +0x9a.
        if (*(short*)(e + 0x10) == 0)
        {
            if (*(short*)(e + 0x9a) == 0)
            {
                e[0x04] = 0x2e;
                e[0x0a] = 2;
                e[0x0b] = 1;
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x2c) = 0;
                *(short*)(e + 0x2a) = 0x0fa0;
                *(short*)(e + 0x9a) = 1;
                e[0x38] = 0xfa;
                e[0x39] = 0xfa;
                e[0x3a] = 0xfa;
            }

            if (*(short*)(e + 0x9a) == 1)
            {
                // LE PAS DE CROISSANCE EST UN DECALAGE, et il vient de +0x5c : 3 pour la premiere
                // silhouette, 4 pour la seconde.
                int shift = *(int*)(e + 0x5c) & 0x1f;

                short grown = (short)(*(short*)(e + 0x28) + ((0x0f3c - *(short*)(e + 0x28)) >> shift));
                *(short*)(e + 0x2c) += (short)((0x0f3c - *(short*)(e + 0x2c)) >> shift);
                *(short*)(e + 0x28) = grown;

                if (grown >= 0x0ed8)
                {
                    *(short*)(e + 0x28) = 0x0ed8;
                    *(short*)(e + 0x2c) = 0x0ed8;
                    *(short*)(e + 0x10) = 0x63;   // elle se fige et ne bougera plus qu'avec le sort
                }
            }

            if (*(short*)(e + 0x9a) == 10)
            {
                e[0x04] = 0x2e;
                e[0x0a] = 2;
                e[0x0b] = 1;
                *(short*)(e + 0x28) = 0x1000;
                *(short*)(e + 0x2c) = 0x1000;
                *(short*)(e + 0x2a) = 0x1000;
                *(short*)(e + 0x9a) = 0x0b;
                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;
            }

            if (*(short*)(e + 0x9a) == 0x0b)
            {
                BrightenEntityTint_00404DE0(e, 0x1e, 0x1e, 0x3c);

                // LA SEULE DES QUATRE qui se met a agir.
                if (e[0x3a] == 0xff) *(short*)(e + 0x10) = 1;
            }

            if (*(short*)(e + 0x9a) == 0x14)
            {
                e[0x04] = 0x2e;
                e[0x0a] = 2;
                e[0x0b] = 1;
                *(short*)(e + 0x28) = 0x1f40;
                *(short*)(e + 0x2c) = 0x1f40;
                *(short*)(e + 0x2a) = 0x1f40;
                *(short*)(e + 0x9a) = 0x15;
                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;
            }

            if (*(short*)(e + 0x9a) == 0x15)
            {
                *(short*)(e + 0x28) += -0xcd;
                *(short*)(e + 0x2a) += -0xcd;
                *(short*)(e + 0x2c) += -0xcd;

                BrightenEntityTint_00404DE0(e, 0x1e, 0x1e, 0x3c);

                if (*(short*)(e + 0x28) <= 0x1068 && e[0x38] == 0xff)
                {
                    *(short*)(e + 0x28) = 0x1068;
                    *(short*)(e + 0x2a) = 0x1068;
                    *(short*)(e + 0x2c) = 0x1068;
                    *(short*)(e + 0x10) = 0x63;
                }
            }
        }

        // Ghidra: LAB_0044e165 — LES SIX PLUMES, une seule fois.
        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;

            for (int wing = 0; wing <= 5; wing++)
            {
                int x = *(int*)(e + 0x1c);
                int y = *(int*)(e + 0x20);
                int z = *(int*)(e + 0x24);

                int yaw = *(short*)(e + 0x32);
                int sin = (int)Winsys3d.GetSinTableEntry(yaw);
                int cos = Winsys3d.GetCosTableEntry(yaw);

                // L'ancrage, tourne par le cap.
                *(int*)(e + 0x1c) = x + (cos * front[wing] - sin * side[wing]);
                *(int*)(e + 0x20) = y + (lift[wing] << 0xc);
                *(int*)(e + 0x24) = z - (cos * side[wing] + sin * front[wing]);

                // Le rang de la plume voyage par +0x14, prete puis rendue.
                short lentIndex = *(short*)(e + 0x14);
                *(short*)(e + 0x14) = (short)wing;

                SpawnCastEntity_0047AAE0(e, 0x9d, 0, -1);

                *(short*)(e + 0x14) = lentIndex;

                *(int*)(e + 0x1c) = x;
                *(int*)(e + 0x20) = y;
                *(int*)(e + 0x24) = z;
            }

            *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_0044e258 — LA CHASSE.
        if (*(short*)(e + 0x10) == 2)
        {
            int myY = *(int*)(e + 0x20);

            // La bouffee nait plus bas, puis la hauteur est rendue.
            *(int*)(e + 0x20) = myY - 0xa0000;
            *(ushort*)(e + 0x9a) = 9;
            *(short*)(e + 0x14) = 0x64;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xa9, 0, -1);
            *(int*)(e + 0x20) = myY;

            int kept = 0;

            // Le lanceur du groupe chasse les entites de scene ; un lanceur de l'autre bord chasse
            // le groupe.
            bool huntScene = (*(uint*)caster & 0x800000) != 0;

            int count = huntScene ? 0x20 : 3;
            byte* prey = huntScene ? Mem.Entity(0x005ecaa0) : Inp.s_SlotFlags_005e93c0;

            for (int slot = 0; slot < count; slot++, prey += 0x9c)
            {
                // La sauvegarde : la silhouette se sert d'elle-meme comme sonde.
                int saved60 = *(int*)(e + 0x60);
                int saved64 = *(int*)(e + 0x64);
                int saved7c = *(int*)(e + 0x7c);
                int saved80 = *(int*)(e + 0x80);
                int saved84 = *(int*)(e + 0x84);
                int saved88 = *(int*)(e + 0x88);
                int saved8c = *(int*)(e + 0x8c);
                ushort saved90 = *(ushort*)(e + 0x90);

                int myX = *(int*)(e + 0x1c);
                int myZ = *(int*)(e + 0x24);

                *(int*)(e + 0x44) = myX;
                *(int*)(e + 0x48) = myY;
                *(int*)(e + 0x4c) = myZ;

                int rawX = *(int*)(prey + 0x1c) - myX;
                int rawY = *(int*)(prey + 0x20) - myY;
                int rawZ = *(int*)(prey + 0x24) - myZ;

                int range = (int)Winsys3d.Ordinal_442(
                    (uint)((rawX >> 0xc) * (rawX >> 0xc)
                           + (rawY >> 0xc) * (rawY >> 0xc)
                           + (rawZ >> 0xc) * (rawZ >> 0xc)));

                if (range < 0x320)
                {
                    // LA MARCHE, quatre pas d'un quart, exactement comme Parole sainte.
                    int stepX = rawX >> 2;
                    int stepY = rawY >> 2;
                    int stepZ = rawZ >> 2;

                    bool blocked = false;
                    int step = 0;

                    do
                    {
                        *(int*)(e + 0x1c) += stepX;
                        *(int*)(e + 0x20) += stepY;
                        *(int*)(e + 0x24) += stepZ;

                        FinalizeEntityPlacement2((uint*)e);

                        if ((*(uint*)(e + 0x90) & 0x8800) != 0)
                        {
                            blocked = true;
                            step = 0x63;
                        }

                        step++;
                    }
                    while ((short)step <= 3);

                    if (!blocked)
                    {
                        bool eligible;

                        if (huntScene)
                        {
                            ResolveEntityBlocks(prey, out byte* stats, out _, out _);

                            uint flags = *(uint*)prey;
                            eligible = (flags & 0x80000000) != 0
                                       && (flags & 0x40002000) == 0
                                       && (*(uint*)(stats + 0x14) & 0x1000080) != 0;
                        }
                        else
                        {
                            // Le camp du groupe n'a pas de bloc d'espece a consulter : la fiche est
                            // celle d'un personnage. Le corps se contente du mot d'etat et du recul.
                            uint flags = *(uint*)prey;
                            eligible = (flags & 0x80000000) != 0
                                       && (flags & 0x40002000) == 0
                                       && *(short*)(prey + 0x12) == 0;
                        }

                        if (eligible)
                        {
                            *(byte**)(prey + 0x74) = *(byte**)(e + 0x6c);

                            kept++;

                            if (kept != 7)
                            {
                                picks[kept - 1] = (short)(slot + 1);
                            }
                            else
                            {
                                // LA SEPTIEME EVINCE LA PLUS LOINTAINE. La distance est reprise
                                // depuis la position SAUVEGARDEE, la marche ayant deplace la sonde.
                                int nx = (myX >> 0xc) - (*(int*)(prey + 0x1c) >> 0xc);
                                int ny = (myY >> 0xc) - (*(int*)(prey + 0x20) >> 0xc);
                                int nz = (myZ >> 0xc) - (*(int*)(prey + 0x24) >> 0xc);

                                int newRange = (int)Winsys3d.Ordinal_442(
                                    (uint)(nx * nx + ny * ny + nz * nz));

                                int scan = 0;

                                do
                                {
                                    // LB : LES DEUX MOITIES DECODENT L'INDICE PAR LA TABLE DE
                                    // SCENE, y compris quand les indices retenus viennent du
                                    // GROUPE. Transcrit tel quel.
                                    byte* held = Mem.Entity(0x005ecaa0) + (picks[scan] - 1) * 0x9c;

                                    int hx = (*(int*)(e + 0x1c) >> 0xc) - (*(int*)(held + 0x1c) >> 0xc);
                                    int hy = (*(int*)(e + 0x20) >> 0xc) - (*(int*)(held + 0x20) >> 0xc);
                                    int hz = (*(int*)(e + 0x24) >> 0xc) - (*(int*)(held + 0x24) >> 0xc);

                                    int heldRange = (int)Winsys3d.Ordinal_442(
                                        (uint)(hx * hx + hy * hy + hz * hz));

                                    if (newRange < heldRange)
                                    {
                                        // LB : LES DEUX MARQUES VONT A CONTRESENS DE L'INDICE — la
                                        // nouvelle proie est DEMARQUEE et l'ancienne MARQUEE, alors
                                        // que c'est la nouvelle qui prend la place dans le tableau.
                                        *(int*)(prey + 0x74) = 0;
                                        *(byte**)(held + 0x74) = *(byte**)(e + 0x6c);

                                        picks[scan] = (short)(slot + 1);

                                        scan = 6;   // une seule eviction, puis on sort
                                    }

                                    scan++;
                                }
                                while ((short)scan <= 5);

                                kept = 6;
                            }
                        }
                    }
                }

                // Ghidra: LAB_0044e55b — la restitution, sur tous les chemins.
                *(int*)(e + 0x7c) = saved7c;
                *(uint*)(e + 0x90) = saved90;    // le mot haut est efface, comme chez Parole sainte
                *(int*)(e + 0x1c) = *(int*)(e + 0x44);
                *(int*)(e + 0x60) = saved60;
                *(int*)(e + 0x64) = saved64;
                *(int*)(e + 0x84) = saved84;
                *(int*)(e + 0x24) = *(int*)(e + 0x4c);
                *(int*)(e + 0x20) = myY;
                *(int*)(e + 0x80) = saved80;
                *(int*)(e + 0x88) = saved88;
                *(int*)(e + 0x8c) = saved8c;
            }

            // Ghidra: LAB_0044e8c9 — le saut a sept branches : les indices retenus, emballes par
            // paires dans les trois mots que les plumes viendront lire.
            short p0 = picks[0], p1 = picks[1], p2 = picks[2];
            short p3 = picks[3], p4 = picks[4], p5 = picks[5];

            switch (kept)
            {
                case 0:
                    *(int*)(e + 0x44) = 0;
                    *(int*)(e + 0x48) = 0;
                    *(int*)(e + 0x4c) = 0;
                    break;

                case 1:
                    *(int*)(e + 0x44) = (p0 << 0x10) | (ushort)p0;
                    *(int*)(e + 0x48) = *(int*)(e + 0x44);
                    *(int*)(e + 0x4c) = *(int*)(e + 0x44);
                    break;

                case 2:
                    *(int*)(e + 0x44) = (p1 << 0x10) | (ushort)p0;
                    *(int*)(e + 0x48) = (p0 << 0x10) | (ushort)p1;
                    *(int*)(e + 0x4c) = *(int*)(e + 0x44);
                    break;

                case 3:
                    *(int*)(e + 0x44) = (p0 << 0x10) | (ushort)p1;
                    *(int*)(e + 0x48) = (p2 << 0x10) | (ushort)p0;
                    *(int*)(e + 0x4c) = (p1 << 0x10) | (ushort)p2;
                    break;

                case 4:
                    *(int*)(e + 0x44) = (p0 << 0x10) | (ushort)p1;
                    *(int*)(e + 0x48) = (p2 << 0x10) | (ushort)p3;
                    *(int*)(e + 0x4c) = *(int*)(e + 0x44);
                    break;

                case 5:
                    *(int*)(e + 0x44) = (p0 << 0x10) | (ushort)p0;
                    *(int*)(e + 0x48) = (p1 << 0x10) | (ushort)p2;
                    *(int*)(e + 0x4c) = (p3 << 0x10) | (ushort)p4;
                    break;

                case 6:
                    *(int*)(e + 0x44) = (p0 << 0x10) | (ushort)p1;
                    *(int*)(e + 0x48) = (p2 << 0x10) | (ushort)p3;
                    *(int*)(e + 0x4c) = (p4 << 0x10) | (ushort)p5;
                    break;
            }
        }

        // Ghidra: LAB_0044ea09
        if (*(short*)(e + 0x10) == 3)
        {
            BrightenEntityTint_00404DE0(e, -5, -5, -5);

            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0044ea60 @ 0x0044ea60 — <b>la plume de l'ange</b> : le sous-type <c>0x9d</c>,
    /// creneau 157 de <see cref="s_StaticEntityHandlers_00551f30"/>. 1 216 octets, <b>absente de
    /// <c>Blaze.c</c></b> ; le releve est garde sous <c>Legacy/Raw/Blaze/FUN_0044EA60.asm</c>.
    ///
    /// <para>
    /// <b>Elle se pose a son ancrage</b> — le meme triplet de tables que la silhouette
    /// (<see cref="s_AngelWingSide"/>), tourne par le cap de celle-ci — et lit <b>sa</b> cible dans
    /// le mot que son rang <c>+0x14</c> designe : <c>+0x44</c> pour les rangs 0 et 1, <c>+0x48</c>
    /// pour 2 et 3, <c>+0x4c</c> pour 4 et 5, moitie basse pour les rangs pairs, moitie haute
    /// (<b>decalage arithmetique</b>) pour les impairs.
    /// </para>
    /// <para>
    /// <b>Elle s'etire de sa queue a sa tete.</b> <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c> tiennent le
    /// point de queue, qui gagne chaque image un quart du chemin vers la cible ; l'echelle
    /// verticale <c>+0x2a</c> vaut la distance qui l'en separe, <c>(distance &lt;&lt; 12) / 140</c>,
    /// que le binaire ecrit en multiplication magique <c>0xEA0EA0EB</c> suivie de <c>sar 7</c>. Le
    /// cap et le tangage sont les deux <c>ratan2</c> de ce meme vecteur, le tangage replie dans un
    /// demi-tour par deux boucles.
    /// </para>
    /// <para>
    /// <b>Elle frappe toutes les vingt images</b> : passe <c>+0x16 &gt; 0x14</c> elle appelle
    /// <see cref="ResolveSpellHit_0053A2B0"/> sur sa cible, la fait clignoter, et <b>recule son
    /// compteur de vingt</b> au lieu de le remettre a zero. Une image sur deux, elle seme en plus
    /// une etincelle <c>0x95</c> a son point de queue, un tirage sur trois.
    /// </para>
    /// <para>
    /// <b>Sans cible elle s'efface</b> — teinte a <c>-20</c>, compteur remis a zero, et le bit
    /// <c>0x4000</c> pose quand elle est eteinte. Et son compteur d'images boucle lui aussi, de
    /// <c>0xBB8</c> a <c>0x14</c>, pour la meme raison que celui du sort.
    /// </para>
    /// </summary>
    private static void TickAngelFeather_0044EA60(byte* record)
    {
        byte* e = record;

        ReadOnlySpan<short> side = s_AngelWingSide;
        ReadOnlySpan<short> front = s_AngelWingFront;
        ReadOnlySpan<short> lift = s_AngelWingLift;

        byte* form = *(byte**)(e + 0x6c);
        if (form == null) form = Res.s_EntityList_005de558;

        byte* caster = *(byte**)(e + 0x74);
        if (caster == null) caster = Res.s_EntityList_005de558;

        // Ghidra: LAB_0044eb0e — l'ancrage, repris de la silhouette et tourne par son cap.
        int wing = *(short*)(e + 0x14);

        *(int*)(e + 0x1c) = *(int*)(form + 0x1c);
        *(int*)(e + 0x24) = *(int*)(form + 0x24);
        *(int*)(e + 0x20) = *(int*)(form + 0x20);

        int yaw = *(short*)(form + 0x32);
        int sin = (int)Winsys3d.GetSinTableEntry(yaw);
        int cos = Winsys3d.GetCosTableEntry(yaw);

        *(int*)(e + 0x1c) += cos * front[wing] - sin * side[wing];
        *(int*)(e + 0x24) -= cos * side[wing] + sin * front[wing];
        *(int*)(e + 0x20) += lift[wing] << 0xc;

        // La silhouette disparue, ou entree dans sa defaite, emporte la plume.
        if ((*(uint*)form & 0x80000000) == 0 || *(short*)(form + 0x10) == 3)
        {
            *(short*)(e + 0x10) = 2;
        }

        // Ghidra: LAB_0044ebba — SA cible, dans la moitie de mot que son rang designe.
        int index = wing switch
        {
            0 => *(int*)(form + 0x44) & 0xffff,
            1 => *(int*)(form + 0x44) >> 0x10,
            2 => *(int*)(form + 0x48) & 0xffff,
            3 => *(int*)(form + 0x48) >> 0x10,
            4 => *(int*)(form + 0x4c) & 0xffff,
            5 => *(int*)(form + 0x4c) >> 0x10,

            // Le binaire y relit une case de pile — l'ecart avant de la plume. Le rang venant de la
            // silhouette, qui ne pose que 0 a 5, la branche est inatteignable.
            _ => front[0],
        };

        byte* target;

        if (index == 0)
        {
            // Pas de cible : la plume se suit elle-meme.
            *(byte**)(e + 0x78) = null;
            target = e;
        }
        else
        {
            // Le <b>-1</b> de l'indice est absorbe par la base, comme dans la silhouette.
            target = (*(uint*)caster & 0x800000) != 0
                ? Mem.Entity(0x005ecaa0) + (index - 1) * 0x9c
                : Inp.s_SlotFlags_005e93c0 + (index - 1) * 0x9c;

            *(byte**)(e + 0x78) = target;
        }

        // Ghidra: LAB_0044ec46
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0x012c;
            *(short*)(e + 0x2c) = 0x012c;
            *(short*)(e + 0x2a) = 0x1f40;
            *(short*)(e + 0x34) = 0x0400;
            *(short*)(e + 0x10) = 1;
            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(short*)(e + 0x16) = 0;

            *(int*)(e + 0x50) = *(int*)(target + 0x1c);
            *(int*)(e + 0x54) = *(int*)(target + 0x20);
            e[0x36] &= 0xfe;
            *(int*)(e + 0x58) = *(int*)(target + 0x24);
            *(short*)(e + 0x98) = 0;
        }

        // Ghidra: LAB_0044eca4
        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, 10, 10, 10);

            // Le point de queue gagne un quart du chemin vers la cible.
            *(int*)(e + 0x50) += (*(int*)(target + 0x1c) - *(int*)(e + 0x50)) >> 2;
            *(int*)(e + 0x58) += (*(int*)(target + 0x24) - *(int*)(e + 0x58)) >> 2;
            *(int*)(e + 0x54) += (*(int*)(target + 0x20) - *(int*)(e + 0x54)) >> 2;

            int dx = (*(int*)(e + 0x1c) >> 0xc) - (*(int*)(e + 0x50) >> 0xc);
            int dy = (*(int*)(e + 0x20) >> 0xc) - (*(int*)(e + 0x54) >> 0xc);
            int dz = (*(int*)(e + 0x24) >> 0xc) - (*(int*)(e + 0x58) >> 0xc);

            int flat = (int)Winsys3d.Ordinal_442((uint)(dz * dz + dx * dx));

            // LE TANGAGE, replie dans un demi-tour par deux boucles et non par un modulo.
            *(short*)(e + 0x34) = (short)(Winsys3d.Ordinal_457(dy, flat) + 0x400);

            while (*(short*)(e + 0x34) >= 0x800) *(short*)(e + 0x34) -= 0x800;
            while (*(short*)(e + 0x34) < 0) *(short*)(e + 0x34) += 0x800;

            // Ghidra: LAB_0044ed6a
            *(short*)(e + 0x32) = (short)(Winsys3d.Ordinal_457(dx, dz) + 0x400);

            int span = (int)Winsys3d.Ordinal_442((uint)(dy * dy + dz * dz + dx * dx));

            *(ushort*)(e + 0x9a) = 9;

            // L'ETIREMENT : (distance << 12) / 140, ecrit en multiplication magique 0xEA0EA0EB
            // suivie de `sar 7` — le meme idiome que le /150 de l'aureole d'incantation.
            *(short*)(e + 0x2a) = (short)((span << 0xc) / 140);

            if (*(byte**)(e + 0x78) == null)
            {
                // Ghidra: LAB_0044ee9e — sans cible, elle s'efface.
                *(short*)(e + 0x98) = 0;
                BrightenEntityTint_00404DE0(e, -20, -20, -20);
                *(short*)(e + 0x16) = 0;

                if (e[0x38] == 0)
                {
                    *(uint*)e |= 0x4000;
                    e[0x38] = 0;
                    e[0x39] = 0;
                    e[0x3a] = 0;
                }
            }
            else
            {
                if (*(short*)(e + 0x98) == 0)
                {
                    Aud.PlaySoundEffect(0x0f, 0x0800, 0x1000, 0x1000);
                }

                *(short*)(e + 0x98) += 1;
                if (*(short*)(e + 0x98) == 5) *(short*)(e + 0x98) = 0;

                *(uint*)e &= 0xffffbfff;

                // Une image sur deux, une etincelle au point de queue — un tirage sur trois.
                if ((e[0x16] & 1) != 0)
                {
                    int x = *(int*)(e + 0x1c);
                    int y = *(int*)(e + 0x20);
                    int z = *(int*)(e + 0x24);

                    *(int*)(e + 0x1c) = *(int*)(e + 0x50);
                    *(int*)(e + 0x20) = *(int*)(e + 0x54);
                    *(int*)(e + 0x24) = *(int*)(e + 0x58);

                    if (CrtRand() % 3 == 0)
                    {
                        SpawnStatusEffectEntity_0047AC40(
                            e, (int*)(e + 0x1c), null, 0x95, 0, -1);
                    }

                    *(int*)(e + 0x1c) = x;
                    *(int*)(e + 0x20) = y;
                    *(int*)(e + 0x24) = z;
                }

                // Ghidra: LAB_0044ee5f — LE COUP, toutes les vingt images.
                if (*(short*)(e + 0x16) > 0x14)
                {
                    ResolveSpellHit_0053A2B0(
                        *(uint**)(e + 0x74), *(uint**)(e + 0x78), e[7], e[6], 0);

                    byte* struck = *(byte**)(e + 0x78);
                    *(ushort*)(struck + 0x96) = 0x10;
                    *(uint*)struck |= 0x40;

                    // Le compteur RECULE de vingt ; il n'est pas remis a zero.
                    *(short*)(e + 0x16) += -20;
                }
            }
        }

        // Ghidra: LAB_0044eed2
        if (*(short*)(e + 0x10) == 2)
        {
            BrightenEntityTint_00404DE0(e, -20, -20, -20);

            if (e[0x38] == 0)
            {
                *(uint*)e &= 0x7fffffff;

                // La proie est relachee.
                if (*(byte**)(e + 0x78) != null) *(int*)(*(byte**)(e + 0x78) + 0x74) = 0;
            }
        }

        // Ghidra: LAB_0044ef07 — le compteur boucle, comme celui du sort.
        *(short*)(e + 0x16) += 1;
        if (*(short*)(e + 0x16) >= 0x0bb8) *(short*)(e + 0x16) = 0x14;
    }

    /// <summary>
    /// Ghidra: FUN_0044ef40 @ 0x0044ef40 — <b>l'etincelle d'Ange</b> : le sous-type <c>0x95</c>,
    /// creneau 149 de <see cref="s_StaticEntityHandlers_00552230"/>. <b>Decompilee dans
    /// <c>Blaze.c</c></b>. C'est le dernier corps qui manquait a l'ecole 1.
    ///
    /// <para>
    /// <b>Elle nait dispersee et deja lancee.</b> Sa premiere image la decale de <c>±0x19</c>
    /// unites en X et en Z, la monte de <c>0x28</c> a <c>0x59</c> unites, lui donne une gravite de
    /// <c>-60</c> (<c>+0x2e</c>), une taille tiree entre <c>0x1000</c> et <c>0x17ff</c> — la meme
    /// pour la largeur et la hauteur, jamais pour la profondeur — et une teinte grise de
    /// <c>0x80</c> sur les trois canaux.
    /// </para>
    /// <para>
    /// <b>Elle grandit en s'eteignant</b>, ce qui est l'inverse de la plupart des etincelles du
    /// depot : chaque image sa teinte perd dix et ses deux echelles gagnent <c>0x78</c>.
    /// </para>
    /// <para>
    /// <b>Sa duree tient en quatre battements de un ou deux</b>. Le tirage <c>+0x14</c> vaut 1 ou 2
    /// et sert deux fois : c'est la longueur d'un battement (<c>+0x16</c>, decompte a chaque image)
    /// et c'est ce qui le recharge quand il tombe a zero. Au <b>quatrieme</b> rechargement elle
    /// perd son bit de vie. Elle vit donc <b>quatre ou huit images</b> selon le tirage, et jamais
    /// entre les deux.
    /// </para>
    /// <para>
    /// Le compteur de battements vit dans <c>+0x40</c>, l'octet d'image d'atlas — qu'elle
    /// n'emploie pas comme tel, son descripteur <c>0x00551558</c> n'ayant qu'un enregistrement.
    /// Un champ, deux roles, comme souvent dans ce fichier.
    /// </para>
    /// </summary>
    private static void TickAngelSpark_0044EF40(byte* record)
    {
        byte* e = record;

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00551528() + 0x30;   // Ghidra: &DAT_00551558
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(ushort*)(e + 0x10) = 1;

            // La meme taille pour la largeur et la hauteur ; la profondeur n'est pas touchee.
            short size = (short)(CrtRand() % 0x800 + 0x1000);
            *(short*)(e + 0x28) = size;
            *(short*)(e + 0x2a) = size;

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;

            // UN OU DEUX, et le meme nombre sert de longueur de battement et de recharge.
            short beat = (short)(CrtRand() % 2 + 1);
            *(short*)(e + 0x14) = beat;
            *(short*)(e + 0x16) = beat;

            *(int*)(e + 0x1c) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x32 - 0x19) * 0x1000;

            *(short*)(e + 0x2e) = -60;
            *(int*)(e + 0x20) += (CrtRand() % 0x32 + 0x28) * 0x1000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            // ELLE GRANDIT EN S'ETEIGNANT.
            BrightenEntityTint_00404DE0(e, -10, -10, -10);

            *(short*)(e + 0x28) += 0x78;
            *(short*)(e + 0x2a) += 0x78;

            *(short*)(e + 0x16) -= 1;

            if (*(short*)(e + 0x16) < 1)
            {
                byte beats = (byte)(e[0x40] + 1);

                *(short*)(e + 0x16) = *(short*)(e + 0x14);
                e[0x40] = beats;

                if (beats == 4) *(uint*)e &= 0x7fffffff;
            }
        }
    }
}
