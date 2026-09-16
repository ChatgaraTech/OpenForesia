// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ECOLE 3 — L'HERBORISTERIE. Les sept plantes, TOUTES sur le meme sous-type 0x4f.
//
// C'est ce qui la distingue des trois ecoles jouables : la ou chaque fiche de sort porte son
// propre sous-type en +0x10, les sept fiches de l'herboristerie portent la meme. Une seule entite
// nait donc pour les sept, et c'est l'INDICE de la fiche qui choisit l'effet, a l'arrivee.
//
// Les sept effets eux-memes ne sont pas ici : ils sont dans `Combat/HerbEffects.cs`, indexes par
// `0x0056d4c0` - HUIT DWORDS AVANT la table des 48 effets d'objet, dont les separe un zero. Ils ne
// travaillent d'ailleurs pas sur le meme objet que les effets d'objet : ils ecrivent DIRECTEMENT
// dans la fiche de personnage, sans appareillage de combat.
//
// L'ecole est celle du Chasseur et elle est INATTEIGNABLE en jeu normal - voir
// Documentation/Reference/Sorts.md.
//
// Vue d'ensemble : Documentation/Reference/Sorts.md ; feuille de route : Documentation/Etat/SortsAtteignables.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // L'HERBORISTERIE — ecole 3, les SEPT plantes, toutes sur le sous-type 0x4f
    //
    //   0x0047A5D0   LE SORT, pour les sept. Sous-type 0x4f de 0x00551c30. DECOMPILE dans Blaze.c.
    //   0x0047A7D0   LE HALO, sous-type 0x2c de 0x00552230. DECOMPILE.
    //   0x0047A730   LE GRAIN, sous-type 0x2b de 0x00552230. DECOMPILE.
    //   0x00476140   LA GRAINE QUI TOURNE, sous-type 0x2d de 0x00552230. Decompilee, mais avec
    //                DEUX PARAMETRES PERDUS (unaff_EBX, unaff_retaddr) — resolus au desassembleur,
    //                releve garde sous Legacy/Raw/Blaze/FUN_00476140.asm.
    //
    // UN SEUL CORPS POUR SEPT FICHES, ET IL SE BRANCHE SUR SON +0x06. C'est la meme forme que la
    // fratrie des elementaires, a ceci pres que le branchement ne choisit pas un decor mais
    // L'EFFET : le sort appelle (&PTR_FUN_0056d4c0)[plante] — voir ApplyHerbEffect_0056D4C0 —
    // et rien d'autre ne distingue Lavande de Belladone. Meme mise en scene pour les sept.
    //
    // ET IL EST INJOUABLE. DAT_00564600 ne declare qu'un sort pour l'ecole 3, le Chasseur ne lance
    // rien, et aucune classe n'a l'ecole 3 : ces onze corps ne tournent dans aucune partie. Ils
    // sont transcrits parce qu'ils existent et qu'ils completent la carte, pas parce qu'on les
    // attend a l'ecran. Voir Documentation/Reference/Sorts.md.
    //
    // LA MISE EN SCENE, en vingt-quatre images :
    //   image 0   — il recopie le lanceur de +0x6c vers +0x74 (c'est la que ses enfants iront le
    //               chercher), cherche une cible par FindTargetAround_00474BA0 et la range en
    //               +0x78, POSE L'EFFET DE SA PLANTE, lache le halo 0x2c avec l'atlas propre a la
    //               plante lu sur le CD, puis SIX graines 0x2d en couronne (0x2aa de pas, soit un
    //               sixieme de tour) lancees vers le haut a 0x10000 par image ;
    //   0 a 0x17  — un grain 0x2b par image, et son cap tourne d'un quart de tour a chaque fois ;
    //   0x18      — il s'efface.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0047a5d0 @ 0x0047a5d0 — <b>l'herboristerie</b> : le sous-type <c>0x4f</c>,
    /// creneau 79 de <see cref="s_StaticEntityHandlers_00551c30"/>, et <b>les sept fiches de
    /// l'ecole 3 a elles toutes</b> — Lavande, Sauge, Verveine, Mercuriale, Fenouil, Hysopius,
    /// Belladone.
    ///
    /// <para>
    /// <b>Ce qui distingue les sept plantes ne tient qu'en deux lignes</b> : l'effet, choisi par
    /// <see cref="ApplyHerbEffect_0056D4C0"/> sur l'octet <c>+0x06</c>, et l'atlas du halo, lu au
    /// meme indice dans une table de huit octets du repertoire de ressources
    /// (<c>+0x94</c> → <c>+0x1c</c>). <b>Tout le reste est commun.</b>
    /// </para>
    /// <para>
    /// <b>Il pose son effet a la premiere image, et une seule fois</b> : la cible vient de
    /// <see cref="FindTargetAround_00474BA0"/> en mode 0, elle est rangee en <c>+0x78</c>, et
    /// l'effet lui est applique immediatement. Les vingt-quatre images qui suivent ne sont que du
    /// decor — <b>le sort a deja agi</b>.
    /// </para>
    /// <para>
    /// <b>Le lanceur passe de <c>+0x6c</c> a <c>+0x74</c></b> a la premiere image. C'est necessaire :
    /// <see cref="SpawnEffectEntity"/> range le lanceur en <c>+0x6c</c>, mais
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> ecrase ce meme <c>+0x6c</c> chez l'enfant avec
    /// le <b>sort</b>. Sans la recopie, les graines n'auraient plus aucun moyen de retrouver celui
    /// qui les a semees.
    /// </para>
    /// <para>
    /// <b>Si aucune cible n'est trouvee, le sort deposera un halo autour d'un pointeur nul.</b>
    /// <see cref="TickHerbMote_0047A730"/> lit <c>+0x78</c> sans le tester ; l'original ne teste pas
    /// non plus le retour de la recherche. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickHerbalism_0047A5D0(byte* record)
    {
        byte* e = record;

        if (*(short*)(e + 0x10) == 0)
        {
            // Le lanceur, recopie la ou les enfants iront le chercher.
            *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);

            e[0x40] = 0;
            *(uint*)(e + 0x38) = 0x00f0f0f0;
            *(uint*)(e + 0x28) = 0x10001000;

            byte* target = FindTargetAround_00474BA0(e, 0);
            *(byte**)(e + 0x78) = target;

            // L'EFFET DE LA PLANTE, pose une fois pour toutes. Le binaire passe un troisieme
            // argument nul qu'aucune des sept ne lit.
            ApplyHerbEffect_0056D4C0(e[6], *(uint**)(e + 0x6c), (uint*)target);

            int* here = (int*)(e + 0x1c);

            // LE HALO, avec l'atlas propre a la plante : huit octets par entree.
            byte* atlas = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x1c)
                          + e[6] * 8;

            SpawnStatusEffectEntity_0047AC40(e, here, atlas, 0x2c, 0, 0);

            int height = StatusBubbleHeight_00538BC0(*(uint**)(e + 0x6c));

            *(ushort*)(e + 0x9a) = 0x4000;                // la gravite que les graines heriteront
            *(int*)(e + 0x20) += height * 0x1000;         // et elles naissent a hauteur de tete

            // SIX GRAINES EN COURONNE, un sixieme de tour l'une de l'autre.
            for (int remaining = 6; remaining != 0; remaining--)
            {
                short yaw = *(short*)(e + 0x32);

                *(int*)(e + 0x50) = (int)Winsys3d.GetSinTableEntry(yaw) << 4;
                *(int*)(e + 0x54) = 0x10000;              // et toutes vers le haut, a la meme vitesse
                *(int*)(e + 0x58) = Winsys3d.GetCosTableEntry(yaw) << 4;

                SpawnStatusEffectEntity_0047AC40(
                    e, here, Descriptors005519A0(), 0x2d, e[0x0b], -1);

                *(short*)(e + 0x32) += 0x2aa;
            }

            *(int*)(e + 0x20) -= height * 0x1000;         // la hauteur est rendue

            *(uint*)(e + 0x38) = 0x00b0b0b0;
            *(uint*)(e + 0x28) = 0x20002000;

            Aud.PlayPositionalSound(0x2f, 0x653, 0x3fff, here);
        }

        // UN GRAIN PAR IMAGE, celle-ci comprise.
        SpawnStatusEffectEntity_0047AC40(
            e, (int*)(e + 0x1c), Descriptors005519A0(), 0x2b, e[0x0b], -1);

        *(short*)(e + 0x32) += 0x400;
        *(short*)(e + 0x10) += 1;

        if (*(short*)(e + 0x10) > 0x17) *(uint*)e = 0;
    }

    /// <summary>
    /// Ghidra: FUN_0047a7d0 @ 0x0047a7d0 — <b>le halo de l'herboristerie</b> : le sous-type
    /// <c>0x2c</c>, creneau 44 de <see cref="s_StaticEntityHandlers_00552230"/>.
    ///
    /// <para>
    /// <b>Il reste colle a la tete du lanceur</b> — il relit <c>+0x74</c> a chaque image et se pose
    /// une stature au-dessus de lui, donc il le suit s'il bouge. Sa vie tient en trois temps :
    /// <b>il s'ouvre</b> pendant douze images, son echelle suivant un quart de sinus
    /// (<c>sin(age &lt;&lt; 7) * 0x1200 &gt;&gt; 12</c>) sur la largeur et la hauteur mais
    /// <b>pas sur la profondeur</b> ; il tient ; puis <b>a partir de la trente-deuxieme il s'eteint</b>,
    /// sa teinte descendant de <c>0x20</c> par image et par composante jusqu'a la quarantieme, ou il
    /// s'efface.
    /// </para>
    /// <para>
    /// La teinte de l'extinction est batie a partir d'un seul octet — <c>0x4ff - age * 0x20</c> —
    /// recopie dans les trois composantes, avec un <c>0x200</c> force dans la composante du milieu.
    /// A la premiere image de l'extinction l'octet vaut <c>0xff</c> et le mot fait
    /// <c>0x02ffffff</c> ; a la trente-neuvieme, la derniere ou il vit encore, il vaut <c>0x1f</c> et le mot <c>0x021f1f1f</c>. C'est
    /// ecrit tel quel, decalages compris.
    /// </para>
    /// </summary>
    private static void TickHerbGlow_0047A7D0(byte* record)
    {
        byte* e = record;

        byte* caster = *(byte**)(e + 0x74);

        *(int*)(e + 0x1c) = *(int*)(caster + 0x1c);
        *(int*)(e + 0x20) = StatusBubbleHeight_00538BC0((uint*)caster) * 0x1000
                            + *(int*)(caster + 0x20);
        *(int*)(e + 0x24) = *(int*)(caster + 0x24);

        short age = *(short*)(e + 0x10);

        if (age == 0)
        {
            *(uint*)(e + 0x38) = 0x01ffffff;
            *(uint*)(e + 0x28) = 0;
            *(short*)(e + 0x2e) = -10;
        }

        if (age < 0xc)
        {
            // L'OUVERTURE : un quart de sinus sur douze images, largeur et profondeur seulement.
            short scale = (short)((int)Winsys3d.GetSinTableEntry(age << 7) * 0x1200 >> 0xc);

            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
        }

        age = *(short*)(e + 0x10);

        if (age >= 0x20)
        {
            // L'EXTINCTION : un seul octet, recopie dans les trois composantes.
            uint fade = (uint)(age * -0x20 + 0x4ff);
            *(uint*)(e + 0x38) = (((fade | 0x200) << 8 | fade) << 8) | fade;
        }

        if (age == 0x28) *(uint*)e = 0;

        *(short*)(e + 0x10) = (short)(age + 1);
    }

    /// <summary>
    /// Ghidra: FUN_0047a730 @ 0x0047a730 — <b>le grain de l'herboristerie</b> : le sous-type
    /// <c>0x2b</c>, creneau 43 de <see cref="s_StaticEntityHandlers_00552230"/>. Le sort en lache
    /// <b>un par image</b> pendant les vingt-quatre.
    ///
    /// <para>
    /// <b>Il tourne autour de la CIBLE</b>, pas du lanceur : il lit <c>+0x78</c>, le seul des trois
    /// enfants a le faire. Un cercle de <c>0x32</c> unites, le cap avancant de <c>0x100</c> par
    /// image, <b>et une hauteur qui redescend</b> — <c>sin(0x800 - age * 0x40) * 0xf0</c>, donc un
    /// arc qui part du haut et retombe. Son image d'atlas boucle sur trois.
    /// </para>
    /// <para>
    /// <b>Sa mort tient a un zero exact</b> : sa teinte perd <c>0x101010</c> par image et il ne
    /// s'efface que si le resultat vaut <b>exactement</b> zero. Ca tombe juste parce que le sort la
    /// lui donne a <c>0xb0b0b0</c>, soit onze fois le pas — il vit onze images. <b>La marge est
    /// nulle</b> : une teinte de depart qui ne serait pas un multiple exact ferait un grain
    /// immortel, qui continuerait a descendre dans les negatifs. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickHerbMote_0047A730(byte* record)
    {
        byte* e = record;

        byte* prey = *(byte**)(e + 0x78);
        short yaw = *(short*)(e + 0x32);

        *(int*)(e + 0x1c) = *(int*)(prey + 0x1c) + (int)Winsys3d.GetSinTableEntry(yaw) * 0x32;

        // L'ARC QUI RETOMBE : le sinus est pris a rebours de l'age.
        *(int*)(e + 0x20) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x10) * -0x40 + 0x800) * 0xf0
                            + *(int*)(prey + 0x20);

        *(int*)(e + 0x24) = *(int*)(prey + 0x24) + Winsys3d.GetCosTableEntry(yaw) * 0x32;

        *(short*)(e + 0x32) = (short)(yaw + 0x100);

        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if (frame > 2) e[0x40] = 0;

        // LE ZERO EXACT, et rien d'autre, ne l'arrete.
        int tint = *(int*)(e + 0x38) - 0x101010;
        *(int*)(e + 0x38) = tint;
        if (tint == 0) *(uint*)e = 0;

        *(short*)(e + 0x10) += 1;
    }

    /// <summary>
    /// Ghidra: FUN_00476140 @ 0x00476140 — <b>le grain qui tourne autour d'une victime</b> : le
    /// sous-type <c>0x2e</c>, creneau <b>70</b> de <see cref="s_StaticEntityHandlers_00552230"/>.
    ///
    /// <para>
    /// <b>Corrige le 2026-09-01 : ce corps n'appartient pas a l'herboristerie.</b> Il etait ecrit
    /// et range sous le nom « TickHerbSeed », avec le sous-type <c>0x2d</c> et le creneau 45 — un
    /// decalage d'un cran dans la table, et une famille de rattachement fausse. Le sous-type
    /// <c>0x2d</c> est <see cref="EntityHandler_0047A880"/> ; le seul engendreur de <c>0x2e</c>
    /// dans <b>tout le binaire</b> est <see cref="TickStatusBurst_004760C0"/>, la gerbe de l'etat
    /// <c>0x80</c>, qui en seme <b>quatre</b> en croix. Le corps, lui, etait juste — et sa condition
    /// de survie le disait deja : elle lit le bit <c>0x80</c>, celui de l'effet 12, que jamais une
    /// herbe ne pose.
    /// </para>
    ///
    /// <para>
    /// <b>Ghidra a perdu deux valeurs sur ce corps</b> — il les rend en <c>unaff_EBX</c> et
    /// <c>unaff_retaddr</c>, la marque d'un parametre non reconstruit. Le desassemblage tranche, et
    /// la reponse est que <b>les deux sont la meme</b> : le rayon <c>(short)+0x12 &gt;&gt; 2</c>,
    /// calcule une fois en <c>0x00476155</c> et range sur la pile en <c>[esp+0x18]</c> avant les
    /// deux <c>push</c> qui decalent le cadre. Ghidra a lu les deux relectures comme deux entrees
    /// distinctes. Releve garde sous <c>Legacy/Raw/Blaze/FUN_00476140.asm</c>.
    /// </para>
    /// <para>
    /// <b>Elle tourne autour de la victime</b> (<c>+0x74</c>, que la gerbe a rempli pour elle) : un
    /// cercle du <b>quart</b> de <c>+0x12</c>, le cap avancant de <c>0x80</c> par image, a une
    /// hauteur fixe de <c>+0x12</c> unites au-dessus d'elle. Elle ne se sert donc <b>pas</b> de la
    /// vitesse heritee en <c>+0x50</c>..<c>+0x58</c> ni de la gravite <c>+0x9a</c>
    /// : ces champs sont poses, herites, et jamais lus par elle. Son image d'atlas boucle sur
    /// <c>+0x1a</c>.
    /// </para>
    /// <para>
    /// <b>Elle vit tant que la victime porte son etat</b> : elle relit le bloc de statistiques de
    /// la victime a chaque image et s'efface des que le bit <c>0x80</c> de <c>+0x40</c> tombe, ou
    /// des que le bit <c>0x40000000</c> se pose. Elle n'a pas de duree a elle — c'est sa mere qui
    /// cesse d'en semer.
    /// </para>
    /// </summary>
    private static void TickStatusMote_00476140(byte* record)
    {
        byte* e = record;

        short lift = *(short*)(e + 0x12);
        short yaw = *(short*)(e + 0x32);
        byte* anchor = *(byte**)(e + 0x74);

        // Ghidra: unaff_retaddr et unaff_EBX sont cette meme valeur, spillee en [esp+0x18].
        int radius = lift >> 2;

        *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * radius + *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = (lift << 0xc) + *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * radius + *(int*)(anchor + 0x24);

        *(short*)(e + 0x32) = (short)(yaw + 0x80);

        byte frame = (byte)(e[0x40] + 1);
        e[0x40] = frame;
        if (frame >= *(short*)(e + 0x1a)) e[0x40] = 0;

        uint state = *(uint*)(GetEntityStatBlock((uint*)anchor) + 0x40);

        if ((state & 0x80) == 0 || (state & 0x40000000) != 0) *(uint*)e = 0;
    }
}
