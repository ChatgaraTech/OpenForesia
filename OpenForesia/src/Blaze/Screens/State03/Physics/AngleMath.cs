// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA TRIGONOMETRIE DE L'ORIGINAL — cinq fonctions, aucune surprise.
//
// Mise a l'echelle d'angles empaquetes, vecteur depuis un ou deux angles, integration avec
// trainee. Elles sont ici parce que tout le reste du repertoire les emploie ; elles ne savent
// rien du jeu.
//
// Respecter les types : ces corps travaillent sur des entiers 32 bits signes avec des decalages,
// pas sur des flottants. Un cast qui semble de trop signale un TYPE mal reconstruit, pas un cast
// manquant.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_00531fe0 @ 0x00531fe0 — <b>deux composantes d'un vecteur, mises a l'echelle</b>.
    /// <c>param_2</c> est un mot empaquete de <see cref="s_LookupTable00600920"/> : sinus dans les
    /// seize bas, cosinus dans les seize hauts. La feuille de
    /// <see cref="SetVectorFromAngle_00532B10"/>.
    /// </summary>
    private static void ScaleAngleComponents_00531FE0(int* destination, int packed, int scale)
    {
        destination[0] = (short)packed * scale;
        destination[2] = (packed >> 0x10) * scale;
    }

    /// <summary>
    /// Ghidra: FUN_00532010 @ 0x00532010 — <b>la version a trois dimensions de
    /// <see cref="ScaleAngleComponents_00531FE0"/></b> : deux angles empaquetes et une vitesse
    /// donnent un vecteur complet. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// X et Z portent le <b>cosinus du tangage</b> en facteur, Y son sinus seul — la conversion
    /// spherique habituelle. Le produit des deux sinus/cosinus est ramene d'un facteur
    /// <c>0x1000</c> par l'idiome de division signee du compilateur de 1998
    /// (<c>t + (t &gt;&gt; 31 &amp; 0xfff)</c> avant le decalage), qui arrondit vers zero et non
    /// vers le bas.
    /// </para>
    /// </summary>
    private static void ScaleAngleComponents3_00532010(
        int* destination, int packedYaw, int packedPitch, int scale)
    {
        int t = (short)packedYaw * (packedPitch >> 0x10);
        destination[0] = ((t + (int)((uint)(t >> 0x1f) & 0xfff)) >> 0xc) * scale;

        t = (packedYaw >> 0x10) * (packedPitch >> 0x10);
        destination[1] = (short)packedPitch * scale;
        destination[2] = ((t + (int)((uint)(t >> 0x1f) & 0xfff)) >> 0xc) * scale;
    }

    /// <summary>
    /// Ghidra: FUN_00532ad0 @ 0x00532ad0 — <b>poser une vitesse a trois dimensions a partir de deux
    /// angles</b>. La sœur de <see cref="SetVectorFromAngle_00532B10"/> : meme table empaquetee,
    /// un angle de plus. Appelee sur <c>+0x50</c>, elle ecrit X, Y et Z en <c>+0x50</c>,
    /// <c>+0x54</c> et <c>+0x58</c> — contrairement a sa sœur, qui laisse Y au site d'appel.
    /// </summary>
    private static void SetVectorFromAngles_00532AD0(
        int* destination, int yaw, int pitch, int scale)
    {
        ScaleAngleComponents3_00532010(
            destination,
            (int)s_LookupTable00600920[yaw & 0xfff],
            (int)s_LookupTable00600920[pitch & 0xfff],
            scale);
    }

    /// <summary>
    /// Ghidra: FUN_00532b10 @ 0x00532b10 — <b>poser une vitesse horizontale a partir d'un cap</b> :
    /// l'angle indexe la table sinus/cosinus empaquetee construite au demarrage, et les deux
    /// composantes partent en <c>+0x00</c> et <c>+0x08</c> du bloc vise. Appelee sur <c>+0x50</c>,
    /// elle ecrit donc <c>X</c> en <c>+0x50</c> et <c>Z</c> en <c>+0x58</c> — <c>Y</c>
    /// (<c>+0x54</c>) est laisse au site d'appel.
    /// </summary>
    private static void SetVectorFromAngle_00532B10(int* destination, int angle, int scale)
    {
        ScaleAngleComponents_00531FE0(
            destination, (int)s_LookupTable00600920[angle & 0xfff], scale);
    }

    /// <summary>
    /// Ghidra: FUN_00535880 @ 0x00535880 — <b>l'integrateur a frottement</b> : il ajoute la vitesse
    /// (<c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>) a la position
    /// (<c>+0x1c</c>/<c>+0x20</c>/<c>+0x24</c>) puis amortit.
    ///
    /// <para>
    /// L'horizontale perd <c>1/8 + 1/256</c> par image — un frottement pur. La verticale non : elle
    /// recoit <c>(g &gt;&gt; 8) - g</c> ou <c>g</c> est le mot <c>+0x9a</c>, plus
    /// <c>-(vy &gt;&gt; 8)</c>. Autrement dit <c>+0x9a</c> est une <b>gravite par entite</b>, et
    /// une entite qui la laisse a zero flotte.
    /// </para>
    /// </summary>
    private static void IntegrateWithDrag_00535880(byte* e)
    {
        int vx = *(int*)(e + 0x50);
        int vy = *(int*)(e + 0x54);
        int vz = *(int*)(e + 0x58);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x24) += vz;
        *(int*)(e + 0x20) += vy;

        *(int*)(e + 0x50) = (vx - (vx >> 3)) - (vx >> 8);

        int gravity = *(short*)(e + 0x9a);
        *(int*)(e + 0x54) = ((gravity >> 8) - (vy >> 8) - gravity) + vy;

        *(int*)(e + 0x58) = (vz - (vz >> 3)) - (vz >> 8);
    }
}
