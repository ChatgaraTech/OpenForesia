// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DE LA MINE ABANDONNEE — zone 1.
//
// Ouvert le 2026-09-16, sur le meme patron que `ForestParticlesA.cs` / `ForestParticlesB.cs` :
// ce qui nait, scintille et meurt dans la mine, et ne decide de rien. La zone 1 porte 157
// gestionnaires dont l'essentiel reste a ecrire ; les effets viennent ici.
//
// Le flash de sortie de zone, lui, n'est PAS ici bien que la mine l'emploie : il est partage avec
// la foret et vit dans `AreaExitFlash.cs`, avec ses trois amorces.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f180</c> — <b>la planche du sous-type 57 de la mine</b>, celle que
    /// <see cref="EntityHandler_00406130"/> pose en <c>+0x3c</c>.
    ///
    /// <para>
    /// <b>Elle commence sur le separateur, et le separateur est une vraie tuile.</b> Le motif
    /// <c>00 00 08 08 01 02 10 00</c> a <c>0x0054f180</c> etait deja releve — c'est lui qui borne
    /// <see cref="s_Descriptors_0054F160"/> par le haut, note le 2026-08-29 comme
    /// « borne exploitable ». Il sert donc aux deux : il ferme la plage precedente ET il est le
    /// premier enregistrement de celle-ci, une tuile <c>8 x 8</c> prise a l'origine de la planche.
    /// </para>
    /// <para>
    /// <b>Cinq enregistrements</b>, et la borne haute se demontre de la meme facon : le meme motif
    /// reparait a <c>0x0054f1a8</c>, sur la page <c>0x03</c>. Les enregistrements 2 a 4 sont deja
    /// sur cette page <c>0x03</c> ; la plage est donc large, la ou le corps n'en lit qu'un.
    /// </para>
    /// <para>
    /// <b>Le corps ne bouge JAMAIS <c>+0x40</c></b> : il le pose a zero a la naissance et n'y
    /// revient pas, dans aucune des trois variantes. Seul l'enregistrement 0 est atteignable, et
    /// les quatre autres ne sont releves que pour respecter la regle de la plage — un bloc se
    /// releve entier, jamais au plus juste sur ce qu'un corps atteint.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F180 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f180 — le seul que le corps lit
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f188
        0x00, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f190
        0x10, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f198
        0x20, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1a0
    ];

    private static byte* s_Descriptors0054F180Storage;

    private static byte* Descriptors0054F180()
        => NativeCopy(s_Descriptors_0054F180, ref s_Descriptors0054F180Storage);

    /// <summary>
    /// Ghidra: <c>0x00406120</c> — <b>l'amorce</b> du sous-type <b>57</b> du bloc
    /// <c>0x00553ea8</c>, que la table <c>0x0056b2c8</c> installe pour les sous-zones 0, 1 et 2 de
    /// la mine abandonnee. Absente de <c>Blaze.c</c> ; six instructions qui empilent
    /// l'enregistrement et sautent dans <see cref="EntityHandler_00406130"/>, laquelle est, elle,
    /// decompilee.
    ///
    /// <para>
    /// <b>Le corps la suit immediatement</b> — <c>0x00406130</c>, seize octets plus loin. C'est le
    /// cas le plus benin des amorces : le <c>call</c> ne sort meme pas du voisinage.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406120(byte* e) => EntityHandler_00406130(e);

    /// <summary>
    /// Ghidra: FUN_00406130 @ 0x00406130 — <b>trois particules en une fonction</b>, choisies par
    /// le mot <c>+0x14</c>. Decompilee dans <c>Blaze.c</c>. Le role a l'ecran n'est pas etabli :
    /// le nom reste celui de l'adresse.
    ///
    /// <para>
    /// <b>Les trois partagent leur ossature</b> — meme planche, meme ancre figee a la naissance en
    /// <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, meme cap tire sur douze bits en <c>+0x32</c>, meme
    /// echelle tiree en <c>+0x28</c>/<c>+0x2a</c>. Ce qui les separe est le sens de la spirale et
    /// le sens de la teinte :
    /// </para>
    /// <list type="table">
    ///   <item><term>0</term><description>nait <b>noire</b>, monte de <c>0x64000</c> d'un coup,
    ///     rayon 300 a 399 qui <b>decroit</b>, cap qui avance de quatre pas par image. Elle
    ///     <b>s'eclaircit</b> de 20 par canal et par image, bornee a <c>80/8c/aa</c>, et meurt
    ///     quand le rayon passe sous <c>0x50</c>.</description></item>
    ///   <item><term>1</term><description>nait <b>a pleine teinte</b> <c>80/8c/aa</c>, rayon 50 a
    ///     99 qui <b>croit</b>, cap qui recule de deux pas. Elle <b>s'assombrit</b> de 5 par image
    ///     et meurt quand le canal rouge atteint zero.</description></item>
    ///   <item><term>2</term><description>ne tourne pas : elle nait <b>dispersee</b> de
    ///     <c>±0x78 * 0x1000</c> en X et en Z, monte en <b>accelerant</b> (<c>+0x5c</c> gagne
    ///     <c>+0x9a</c>, tire entre 1 et 3, a chaque image), et n'entame son extinction qu'une fois
    ///     un decompte de 5 a 9 images epuise.</description></item>
    /// </list>
    /// <para>
    /// <b>Le produit n'est pas recadre</b> : l'entree de table de sinus multiplie le rayon tel
    /// quel, sans <c>&gt;&gt; 0xc</c>. Meme convention que <see cref="EntityHandler_00404C80"/>,
    /// son plus proche cousin.
    /// </para>
    /// <para>
    /// <b>Une bizarrerie fidele, variante 2</b> : <c>+0x12</c> est ecrit DEUX fois a la naissance
    /// — d'abord un tirage sur douze bits, comme dans les deux autres variantes, puis
    /// <c>rand() % 5 + 5</c> qui l'ecrase aussitot. Le second gagne, et <c>+0x12</c> sert de
    /// decompte et non de cap. Le premier tirage est donc <b>mort</b> : il ne reste que sa
    /// consommation du generateur, qui decale la suite des tirages.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406130(byte* e)
    {
        // ---------------------------------------------------------------- variante 0
        if (*(short*)(e + 0x14) == 0)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = Descriptors0054F180();   // Ghidra: &DAT_0054f180
                e[0x40] = 0;

                // Le Y de naissance sert DEUX fois : la position et l'ancre.
                int born = *(int*)(e + 0x20);
                *(ushort*)(e + 0x32) = (ushort)(CrtRand() % 0x1000);   // le cap
                *(int*)(e + 0x20) = born + 0x64000;
                *(int*)(e + 0x54) = born + 0x64000;
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                *(short*)(e + 0x9a) = (short)(CrtRand() % 100 + 300);  // le rayon, decroissant

                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;
                *(int*)(e + 0x5c) = CrtRand() % 8 + 8;                 // le pas

                e[0x0a] = 2;
                e[0x0b] = 1;
                *(ushort*)(e + 0x12) = (ushort)(CrtRand() % 0x1000);

                short scale = (short)(CrtRand() % 0x400 + 0x800);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, 0x14, 0x14, 0x14);
                if (e[0x38] > 0x7f) e[0x38] = 0x80;
                if (e[0x39] > 0x8b) e[0x39] = 0x8c;
                if (e[0x3a] > 0xa9) e[0x3a] = 0xaa;

                short yaw = *(short*)(e + 0x32);
                short radius = *(short*)(e + 0x9a);

                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * radius + *(int*)(e + 0x50);
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * radius + *(int*)(e + 0x58);
                *(int*)(e + 0x20) = *(int*)(e + 0x54)
                    + (int)Winsys3d.GetSinTableEntry((short)(*(short*)(e + 0x12) + yaw)) * 0x28;

                *(short*)(e + 0x9a) -= (short)*(int*)(e + 0x5c);
                *(short*)(e + 0x32) += (short)(*(int*)(e + 0x5c) * 4);

                if (*(short*)(e + 0x9a) < 0x50) *(uint*)e &= 0x7fffffff;
            }
        }

        // ---------------------------------------------------------------- variante 1
        if (*(short*)(e + 0x14) == 1)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = Descriptors0054F180();   // Ghidra: &DAT_0054f180
                e[0x40] = 0;

                int born = *(int*)(e + 0x20);
                *(ushort*)(e + 0x32) = (ushort)(CrtRand() % 0x1000);
                *(int*)(e + 0x20) = born + 0x64000;
                *(int*)(e + 0x54) = born + 0x64000;
                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                *(short*)(e + 0x9a) = (short)(CrtRand() % 0x32 + 0x32);  // le rayon, croissant

                e[0x38] = 0x80;
                e[0x39] = 0x8c;
                e[0x3a] = 0xaa;
                *(int*)(e + 0x5c) = CrtRand() % 8 + 4;

                e[0x0a] = 2;
                e[0x0b] = 1;
                *(ushort*)(e + 0x12) = (ushort)(CrtRand() % 0x1000);

                short scale = (short)(CrtRand() % 0x418 + 0x800);
                *(short*)(e + 0x28) = scale;
                *(short*)(e + 0x2a) = scale;
            }

            if (*(short*)(e + 0x10) == 1)
            {
                BrightenEntityTint_00404DE0(e, -5, -5, -5);

                short yaw = *(short*)(e + 0x32);
                short radius = *(short*)(e + 0x9a);

                *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * radius + *(int*)(e + 0x50);
                *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * radius + *(int*)(e + 0x58);
                *(int*)(e + 0x20) = *(int*)(e + 0x54)
                    + (int)Winsys3d.GetSinTableEntry((short)(*(short*)(e + 0x12) + yaw)) * 0x28;

                *(short*)(e + 0x9a) += (short)*(int*)(e + 0x5c);
                *(short*)(e + 0x32) += (short)(*(int*)(e + 0x5c) * -2);

                if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
            }
        }

        // ---------------------------------------------------------------- variante 2
        if (*(short*)(e + 0x14) == 2)
        {
            if (*(short*)(e + 0x10) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(byte**)(e + 0x3c) = Descriptors0054F180();   // Ghidra: &DAT_0054f180
                e[0x40] = 0;

                *(ushort*)(e + 0x32) = (ushort)(CrtRand() % 0x1000);

                // Dispersee autour de sa naissance : +-0x78 unites de 0x1000, en X et en Z.
                *(int*)(e + 0x1c) += (CrtRand() % 0xf0 - 0x78) * 0x1000;
                *(int*)(e + 0x24) += (CrtRand() % 0xf0 - 0x78) * 0x1000;

                *(int*)(e + 0x50) = *(int*)(e + 0x1c);
                *(int*)(e + 0x54) = *(int*)(e + 0x20);
                *(int*)(e + 0x58) = *(int*)(e + 0x24);

                *(int*)(e + 0x5c) = 0;                                 // la vitesse, qui s'accumule
                e[0x38] = 0x5a;
                e[0x39] = 0x78;
                e[0x3a] = 0xaa;
                *(short*)(e + 0x9a) = (short)(CrtRand() % 3 + 1);      // l'acceleration

                e[0x0a] = 2;
                e[0x0b] = 1;

                // Premier tirage de +0x12 : ECRASE quinze lignes plus bas. Il ne reste de lui que
                // sa consommation du generateur, qui decale tous les tirages suivants.
                *(ushort*)(e + 0x12) = (ushort)(CrtRand() % 0x1000);

                short scale = (short)(CrtRand() % 0x418);
                *(short*)(e + 0x28) = (short)(scale + 0x800);
                *(short*)(e + 0x2a) = (short)(scale + 0x1000);

                *(short*)(e + 0x12) = (short)(CrtRand() % 5 + 5);      // le decompte, 5 a 9
            }

            if (*(short*)(e + 0x10) == 1)
            {
                *(short*)(e + 0x12) -= 1;
                *(int*)(e + 0x20) += *(int*)(e + 0x5c) * 0x1000;
                *(int*)(e + 0x5c) += *(short*)(e + 0x9a);

                if (*(short*)(e + 0x12) < 0)
                {
                    BrightenEntityTint_00404DE0(e, -10, -10, -10);
                    if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
                }
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f2c0</c> — <b>la planche du sous-type 48 de la mine</b>, celle que
    /// <see cref="EntityHandler_00406570"/> pose en <c>+0x3c</c>.
    ///
    /// <para>
    /// <b>Trois enregistrements</b>, bornes par la meme regle que partout dans cette zone de
    /// <c>.data</c> : le motif separateur <c>00 00 08 08 01 02 10 00</c> reparait a
    /// <c>0x0054f2d8</c> et ferme la plage.
    /// </para>
    /// <para>
    /// <b>Et ce qui suit ce separateur est la copie conforme de ce qui suit celui de
    /// <c>0x0054f180</c></b> : <c>c0 c0 3f 3f 01 02 09 00</c>, puis les trois tuiles
    /// <c>0x10 x 0x10</c> de la page <c>0x03</c>, dans le meme ordre. Le jeu de 1998 a duplique
    /// cette donnee comme il a duplique tant de corps — c'est une confirmation de plus que le motif
    /// est bien une borne et non un enregistrement de remplissage.
    /// </para>
    /// <para>
    /// Le premier enregistrement porte <c>0x02</c> en cinquieme octet la ou presque tous les autres
    /// portent <c>0x01</c> ; il est releve tel quel.
    /// </para>
    /// <para>
    /// <b>Le corps ne lit que l'enregistrement 0</b> : <c>+0x40</c> est pose a zero et n'est jamais
    /// avance.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F2C0 =
    [
        0x00, 0x18, 0x18, 0x10, 0x02, 0x02, 0x10, 0x00,   // 0x0054f2c0 — le seul que le corps lit
        0x00, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f2c8
        0x10, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f2d0
    ];

    private static byte* s_Descriptors0054F2C0Storage;

    private static byte* Descriptors0054F2C0()
        => NativeCopy(s_Descriptors_0054F2C0, ref s_Descriptors0054F2C0Storage);

    /// <summary>
    /// Ghidra: <c>0x00406570</c> — <b>la marque accrochee a son porteur</b>, sous-type <b>48</b> du
    /// bloc <c>0x00553ea8</c>, que la table <c>0x0056b2c8</c> installe pour les sous-zones 0 a 2 de
    /// la mine abandonnee. Absente de <c>Blaze.c</c> ; dix-sept instructions relevees au
    /// desassembleur — <c>Legacy/Raw/Blaze/FUN_00406570.asm</c>.
    ///
    /// <para>
    /// <b>Elle ne vit pas par elle-meme : elle suit.</b> Le champ <c>+0x6c</c> est relu a chaque
    /// image, comme pour <see cref="EntityHandler_00413330"/>. Nul, la recopie est simplement
    /// sautee et le reste du corps s'execute quand meme — contrairement au stroboscope de la foret,
    /// qui rend la main. Elle ne meurt d'ailleurs jamais d'elle-meme : aucun chemin de ce corps
    /// n'efface le bit de vie.
    /// </para>
    /// <para>
    /// <b>La hauteur ne vient pas d'ou on l'attend, et elle est ecrite deux fois.</b> La recopie du
    /// porteur prend <c>X</c> en <c>+0x1c</c> et <c>Z</c> en <c>+0x24</c> comme partout, mais
    /// <c>Y</c> vient du <c>+0x60</c> du porteur et non de son <c>+0x20</c>. Puis, des que l'etat
    /// vaut 1 — c'est-a-dire des la premiere image —, <c>+0x20</c> est <b>reecrit</b> depuis le
    /// <c>+0x60</c> de l'entite elle-meme. La valeur prise au porteur est donc <b>morte</b> : elle
    /// est ecrasee avant d'avoir servi. C'est le binaire, transcrit tel quel.
    /// </para>
    /// <para>
    /// Le reste est une naissance sans surprise : la planche <c>0x0054f2c0</c>, une teinte grise
    /// uniforme <c>0x20</c> sur les trois canaux, et <c>+0x2e</c> a <c>-10</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406570(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier != null)
        {
            *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
            *(int*)(e + 0x20) = *(int*)(carrier + 0x60);   // le +0x60 du PORTEUR, pas son +0x20
            *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F2C0();   // Ghidra: &DAT_0054f2c0
            e[0x40] = 0;
            e[0x0a] = 3;
            e[0x0b] = 0;
            *(short*)(e + 0x2e) = -10;
            e[0x38] = 0x20;
            e[0x39] = 0x20;
            e[0x3a] = 0x20;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            // Ecrase le Y qui vient d'etre pris au porteur. Fidele.
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
        }
    }
}
