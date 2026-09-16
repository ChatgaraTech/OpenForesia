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

    /// <summary>
    /// Ghidra: <c>0x004065d0</c> — <b>la meme marque, mais pilotee par l'etat de son porteur</b>.
    /// Sous-type <b>47</b> du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Absente de
    /// <c>Blaze.c</c> ; relevee au desassembleur —
    /// <c>Legacy/Raw/Blaze/FUN_004065D0.asm</c>.
    ///
    /// <para>
    /// <b>Elle partage tout avec <see cref="EntityHandler_00406570"/></b> : la meme planche
    /// <c>0x0054f2c0</c>, le meme <c>+0x2e</c> a <c>-10</c>, les memes <c>+0x0a</c>/<c>+0x0b</c>, et
    /// la meme hauteur ecrite deux fois — <c>Y</c> pris au <c>+0x60</c> du porteur puis aussitot
    /// ecrase par le <c>+0x60</c> de l'entite. Les deux corps sont manifestement le meme geste,
    /// emis deux fois.
    /// </para>
    /// <para>
    /// <b>Ce qui la distingue tient en une ligne de recopie et trois tests.</b> Elle copie aussi
    /// l'ETAT du porteur — <c>+0x10</c> du porteur vers son propre <c>+0x12</c> — puis s'en sert
    /// comme d'une consigne :
    /// </para>
    /// <list type="table">
    ///   <item><term>1</term><description>elle <b>s'eclaircit</b> de 2 par canal et par image</description></item>
    ///   <item><term>2</term><description>elle <b>s'assombrit</b> de 3 par canal et par image</description></item>
    ///   <item><term>0x65</term><description>elle est <b>remise a noir</b> d'un coup</description></item>
    /// </list>
    /// <para>
    /// Les trois tests ne sont pas exclusifs dans le binaire — ce sont trois <c>if</c> a la suite,
    /// et non un <c>switch</c> —, mais les trois valeurs s'excluent d'elles-memes. Transcrits dans
    /// la meme forme.
    /// </para>
    /// <para>
    /// <b>Elle nait NOIRE</b>, la ou <see cref="EntityHandler_00406570"/> nait a <c>0x20</c> sur
    /// les trois canaux : c'est la seule difference de naissance entre les deux. Elle apparait donc
    /// invisible et ne se montre que si son porteur passe par l'etat 1.
    /// </para>
    /// <para>
    /// Comme sa jumelle, <b>elle ne meurt jamais d'elle-meme</b> : aucun chemin n'efface le bit de
    /// vie.
    /// </para>
    /// </summary>
    private static void EntityHandler_004065D0(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier != null)
        {
            *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
            *(int*)(e + 0x20) = *(int*)(carrier + 0x60);   // le +0x60 du PORTEUR, comme la jumelle
            *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
            *(short*)(e + 0x12) = *(short*)(carrier + 0x10);   // l'etat du porteur devient la consigne
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F2C0();   // Ghidra: &DAT_0054f2c0
            e[0x40] = 0;
            e[0x0a] = 3;
            e[0x0b] = 0;
            *(short*)(e + 0x2e) = -10;
            e[0x38] = 0;      // noire a la naissance, la ou sa jumelle nait a 0x20
            e[0x39] = 0;
            e[0x3a] = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            // Ecrase le Y qui vient d'etre pris au porteur. Fidele, comme chez la jumelle.
            *(int*)(e + 0x20) = *(int*)(e + 0x60);

            if (*(short*)(e + 0x12) == 1) BrightenEntityTint_00404DE0(e, 2, 2, 2);
            if (*(short*)(e + 0x12) == 2) BrightenEntityTint_00404DE0(e, -3, -3, -3);

            if (*(short*)(e + 0x12) == 0x65)
            {
                e[0x38] = 0;
                e[0x39] = 0;
                e[0x3a] = 0;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f1b0</c> — <b>la planche du sous-type 61 de la mine</b>, celle que
    /// <see cref="EntityHandler_00406680"/> pose en <c>+0x3c</c>.
    ///
    /// <para>
    /// <b>Elle commence encore sur le motif separateur</b>, <c>00 00 08 08 01 02 10 00</c>, cette
    /// fois a <c>0x0054f1b0</c> — le troisieme cas du genre releve dans cette zone de <c>.data</c>,
    /// apres <c>0x0054f180</c> et <c>0x0054f2d8</c>. Le motif borne, et il sert.
    /// </para>
    /// <para>
    /// <b>Onze enregistrements</b>, et la borne haute n'est PAS un separateur cette fois : c'est un
    /// <b>changement de famille d'attributs</b>. A <c>0x0054f208</c> le cinquieme octet passe de
    /// <c>0x01</c> a <c>0x00</c>, le septieme de <c>0x10</c> a <c>0x18</c>, et la tuile passe en
    /// <c>0x1f x 0x1f</c> : plus rien a voir. C'est l'autre borne exploitable que
    /// <c>DataSymbols.md</c> nomme — le changement de planche.
    /// </para>
    /// <para>
    /// Les enregistrements 3 a 6 et 7 a 10 forment deux suites de quatre tuiles <c>8 x 8</c> qui
    /// ressemblent a deux animations de quatre images. <b>Le corps n'en atteint aucune</b> :
    /// <c>+0x40</c> est pose a zero et n'est jamais avance, donc seul l'enregistrement 0 sert. Les
    /// dix autres sont releves parce que la regle veut la plage entiere.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F1B0 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f1b0 — le seul que le corps lit
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1b8
        0x08, 0x08, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x0054f1c0
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1c8
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1d0
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1d8
        0x10, 0x10, 0x08, 0x07, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1e0
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1e8
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1f0
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1f8
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f200
    ];

    private static byte* s_Descriptors0054F1B0Storage;

    private static byte* Descriptors0054F1B0()
        => NativeCopy(s_Descriptors_0054F1B0, ref s_Descriptors0054F1B0Storage);

    /// <summary>
    /// Ghidra: <c>0x00406680</c> — <b>la grande spirale qui rentre</b>, sous-type <b>61</b> du bloc
    /// <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Absente de <c>Blaze.c</c> ; 400 octets
    /// releves au desassembleur — <c>Legacy/Raw/Blaze/FUN_00406680.asm</c>.
    ///
    /// <para>
    /// <b>C'est la cousine de la variante 0 de <see cref="EntityHandler_00406130"/></b>, pas sa
    /// copie : meme geste — naitre noire, tourner autour d'une ancre figee, s'eclaircir, mourir
    /// quand le rayon se referme — mais <b>aucune constante en commun</b>, et une naissance qui la
    /// distingue nettement :
    /// </para>
    /// <list type="table">
    ///   <item><term>naissance</term><description>elle se DISPERSE EN HAUTEUR, de
    ///     <c>-380</c> a <c>+379</c> unites de <c>0x1000</c> autour de son <c>Y</c>. La variante 0
    ///     de l'autre, elle, montait d'un <c>+0x64000</c> fixe.</description></item>
    ///   <item><term>tour</term><description>le cap avance de <c>+0x5c * 20</c> par image, contre
    ///     <c>* 4</c> chez l'autre — elle tourne cinq fois plus vite.</description></item>
    ///   <item><term>hauteur</term><description>la houle verticale vaut <c>sin * 50</c>, contre
    ///     <c>sin * 0x28</c>.</description></item>
    ///   <item><term>mort</term><description>sous un rayon de <c>0x32</c>, contre
    ///     <c>0x50</c>.</description></item>
    ///   <item><term>plafond</term><description>les trois canaux sont bornes a <c>0x80</c>
    ///     ENSEMBLE, des que le rouge y touche — la ou l'autre bornait chaque canal a sa propre
    ///     valeur, <c>80/8c/aa</c>.</description></item>
    /// </list>
    /// <para>
    /// <b>Le produit n'est pas recadre</b> : l'entree de table de sinus multiplie le rayon tel
    /// quel, sans <c>&gt;&gt; 0xc</c>. Meme convention que partout dans cette famille.
    /// </para>
    /// <para>
    /// <b>Le pas est ecrit sur 32 bits et relu sur 16.</b> <c>+0x5c</c> recoit un
    /// <c>mov [esi+5Ch],edx</c> a la naissance, mais le tour le relit par <c>mov cx,[esi+5Ch]</c>
    /// et <c>mov dx,[esi+5Ch]</c>. Les deux largeurs sont transcrites telles quelles ; la valeur
    /// tenant sur quatre bits, cela ne change rien, mais c'est ce que le binaire fait.
    /// </para>
    /// <para>
    /// <b>Les deux appels de trigonometrie sont des sauts par la table d'imports</b> :
    /// <c>0x00545a1c</c> saute par <c>[0x6087bc]</c> et <c>0x00545a16</c> par <c>[0x6087c0]</c>.
    /// Le recoupement se fait sur <see cref="EntityHandler_00404C80"/>, que Ghidra decompile : elle
    /// appelle la meme paire dans le meme ordre, et Ghidra y nomme <c>Ordinal_451</c> puis
    /// <c>Ordinal_450</c> — donc sinus pour <c>X</c>, cosinus pour <c>Z</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406680(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F1B0();   // Ghidra: &DAT_0054f1b0
            e[0x40] = 0;

            *(short*)(e + 0x32) = (short)(CrtRand() % 0x1000);       // le cap

            // La dispersion verticale : -380 a +379 unites de 0x1000.
            int scattered = *(int*)(e + 0x20) + (CrtRand() % 0x2f8 - 0x17c) * 0x1000;
            *(int*)(e + 0x20) = scattered;
            *(int*)(e + 0x54) = scattered;
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            *(short*)(e + 0x9a) = (short)(CrtRand() % 0x64 + 0x12c);  // le rayon, 300 a 399

            e[0x38] = 0;
            e[0x39] = 0;
            e[0x3a] = 0;
            *(int*)(e + 0x5c) = CrtRand() % 0x0a + 5;                 // le pas, 5 a 14

            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);        // le dephasage de la houle

            short scale = (short)(CrtRand() % 0x400 + 0x1000);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, 0x14, 0x14, 0x14);
            if (e[0x38] >= 0x80)
            {
                e[0x38] = 0x80;
                e[0x39] = 0x80;
                e[0x3a] = 0x80;
            }

            short yaw = *(short*)(e + 0x32);
            short radius = *(short*)(e + 0x9a);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(yaw) * radius + *(int*)(e + 0x50);
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(yaw) * radius + *(int*)(e + 0x58);
            *(int*)(e + 0x20) = *(int*)(e + 0x54)
                + (int)Winsys3d.GetSinTableEntry((short)(*(short*)(e + 0x12) + yaw)) * 50;

            // Le pas est relu sur SEIZE bits, la ou la naissance l'a ecrit sur trente-deux.
            *(short*)(e + 0x32) += (short)(*(short*)(e + 0x5c) * 0x14);
            *(short*)(e + 0x9a) -= *(short*)(e + 0x5c);

            if (*(short*)(e + 0x9a) < 0x32) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f2a0</c> et <c>&amp;DAT_0054f2b0</c> — <b>les deux planches du
    /// sous-type 60</b>, une par variante, que <see cref="EntityHandler_00406810"/> pose en
    /// <c>+0x3c</c>.
    ///
    /// <para>
    /// <b>Deux enregistrements chacune, et les bornes viennent du CODE.</b> Toute cette region de
    /// <c>.data</c> est une suite uniforme de tuiles <c>0x1f x 0x1f</c> sur la meme page
    /// (<c>01 02 18 00</c>) : aucun changement d'attribut, aucun motif separateur ne s'y lit. Ce qui
    /// coupe, ce sont les adresses que du code reference — <c>0x0054f2b0</c> ferme la premiere
    /// plage, et <c>0x0054f2c0</c> ferme la seconde parce que c'est la planche de
    /// <see cref="EntityHandler_00406570"/>, deja relevee.
    /// </para>
    /// <para>
    /// <b>Deux, c'est exactement ce qu'il faut</b>, et c'est le corps qui le prouve : son
    /// <c>+0x40</c> est <b>retire au hasard entre 0 et 1 a chaque image</b>. Une plage d'un seul
    /// enregistrement rendrait du tas non initialise une image sur deux.
    /// <c>check-descriptors.sh</c> signalera « marge courte » sur ces deux lignes : c'est attendu,
    /// la marge de 2 est juste, et il ne faut pas l'allonger.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F2A0 =
    [
        0x3f, 0x20, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f2a0
        0x60, 0x20, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f2a8
    ];

    /// <summary>La planche de la variante 1 — voir <see cref="s_Descriptors_0054F2A0"/>.</summary>
    private static readonly byte[] s_Descriptors_0054F2B0 =
    [
        0x00, 0x20, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f2b0
        0x20, 0x20, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f2b8
    ];

    private static byte* s_Descriptors0054F2A0Storage;
    private static byte* s_Descriptors0054F2B0Storage;

    private static byte* Descriptors0054F2A0()
        => NativeCopy(s_Descriptors_0054F2A0, ref s_Descriptors0054F2A0Storage);

    private static byte* Descriptors0054F2B0()
        => NativeCopy(s_Descriptors_0054F2B0, ref s_Descriptors0054F2B0Storage);

    /// <summary>
    /// Ghidra: FUN_00406810 @ 0x00406810 — <b>l'eclat bref qui scintille</b>, sous-type <b>60</b>
    /// du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Deux variantes, choisies par <c>+0x14</c></b>, et elles ne different qu'a la naissance :
    /// la variante 0 prend la planche <c>0x0054f2a0</c>, se pose <c>7</c> en <c>+0x0a</c> et
    /// <b>monte de <c>0x50000</c></b> ; la variante 1 prend <c>0x0054f2b0</c>, se pose <c>2</c>, et
    /// ne monte pas. Tout le reste leur est commun.
    /// </para>
    /// <para>
    /// <b>Elle se place en deux temps.</b> D'abord elle avance le long du cap qu'on lui a donne —
    /// <c>+0x32</c> n'est jamais ecrit par ce corps, il vient de qui la fait naitre — d'une
    /// distance tiree entre 0 et 99. Puis elle se decale d'une boite : <c>±0x19</c> en <c>X</c> et
    /// en <c>Z</c>, et <c>0</c> a <c>0x1d</c> vers le haut, le tout en unites de <c>0x1000</c>.
    /// </para>
    /// <para>
    /// <b>Elle scintille en changeant d'image au hasard.</b> Le tour ne fait presque rien d'autre :
    /// <c>+0x40</c> est <b>retire a pile ou face a chaque image</b>, entre les deux enregistrements
    /// de sa planche. C'est ce qui exige deux enregistrements et pas un — voir
    /// <see cref="s_Descriptors_0054F2A0"/>.
    /// </para>
    /// <para>
    /// <b>Elle meurt de vieillesse, pas d'extinction.</b> Elle nait a <c>0x80</c> sur les trois
    /// canaux et perd 7 par image, mais son compte a rebours <c>+0x12</c> ne lui laisse que 4 a 7
    /// images : elle disparait encore claire, sans jamais atteindre le noir.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406810(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            if (*(short*)(e + 0x14) == 0)
            {
                *(byte**)(e + 0x3c) = Descriptors0054F2A0();   // Ghidra: &DAT_0054f2a0
                e[0x0a] = 7;
                e[0x0b] = 0;
                *(int*)(e + 0x20) += 0x50000;
            }

            if (*(short*)(e + 0x14) == 1)
            {
                *(byte**)(e + 0x3c) = Descriptors0054F2B0();   // Ghidra: &DAT_0054f2b0
                e[0x0a] = 2;
                e[0x0b] = 0;
            }

            *(short*)(e + 0x10) = 1;

            short scale = (short)(CrtRand() % 0x400 + 0x6d4);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4 + 4);   // le compte a rebours, 4 a 7

            // Premier temps : avancer le long du cap recu, de 0 a 99.
            short reach = (short)(CrtRand() % 100);
            short yaw = *(short*)(e + 0x32);
            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(yaw) * reach;
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(yaw) * reach;

            // Second temps : la boite de dispersion.
            *(int*)(e + 0x1c) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x1e) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x32 - 0x19) * 0x1000;

            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            e[0x40] = (byte)(CrtRand() % 2);   // pile ou face entre les deux images
            BrightenEntityTint_00404DE0(e, -7, -7, -7);

            *(short*)(e + 0x12) -= 1;
            if (*(short*)(e + 0x12) < 1) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f280</c> — <b>la bande de quatre images</b> que
    /// <see cref="EntityHandler_00406970"/> parcourt, et que <c>FUN_00406aa0</c> emploie aussi.
    ///
    /// <para>
    /// <b>C'est une vraie animation, et elle se lit dans la donnee seule</b> : quatre tuiles
    /// <c>0x1f x 0x1f</c> prises sur la meme ligne <c>y = 0xa0</c>, a <c>x = 0x00</c>, <c>0x20</c>,
    /// <c>0x40</c>, <c>0x60</c>. Les plages voisines de cette region n'ont ni motif separateur ni
    /// changement d'attribut ; la borne haute est <c>0x0054f2a0</c>, la planche de
    /// <see cref="EntityHandler_00406810"/> — une borne prouvee par le CODE.
    /// </para>
    /// <para>
    /// <b>Le corps confirme le compte</b> : son <c>+0x40</c> avance de 0 a 3 puis <b>s'arrete sur
    /// 3</b>. Il ne reboucle pas, contrairement a <see cref="EntityHandler_00404C80"/> qui remet a
    /// zero des qu'il depasse 3. Donnee et code se bornent l'un l'autre, exactement.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F280 =
    [
        0x00, 0xa0, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f280
        0x20, 0xa0, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f288
        0x40, 0xa0, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f290
        0x60, 0xa0, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f298
    ];

    private static byte* s_Descriptors0054F280Storage;

    private static byte* Descriptors0054F280()
        => NativeCopy(s_Descriptors_0054F280, ref s_Descriptors0054F280Storage);

    /// <summary>
    /// Ghidra: <c>0x00406970</c> — <b>la bouffee qui part de biais, enfle et ralentit</b>,
    /// sous-type <b>56</b> du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Absente de
    /// <c>Blaze.c</c> — alors que sa voisine immediate <c>FUN_00406aa0</c> y est ; 304 octets
    /// releves au desassembleur, <c>Legacy/Raw/Blaze/FUN_00406970.asm</c>.
    ///
    /// <para>
    /// <b>Un seul champ porte deux roles, et c'est ce qui fait tout le mouvement.</b>
    /// <c>+0x14</c> est a la fois la <b>vitesse</b> et le <b>compte a rebours</b> : chaque image,
    /// l'entite avance de <c>sin/cos(+0x12) * (+0x14 * 2)</c> puis <c>+0x14</c> perd un. Elle part
    /// donc vite et <b>ralentit jusqu'a l'arret</b>, ou elle meurt — tiree entre 10 et 14 images de
    /// vie a la naissance.
    /// </para>
    /// <para>
    /// <b>Le cap ne change jamais.</b> <c>+0x12</c> est tire une fois sur douze bits et n'est plus
    /// touche : la trajectoire est une droite, pas une spirale. Seul le <c>Y</c> monte, d'un
    /// <c>0xa000</c> constant.
    /// </para>
    /// <para>
    /// <b>L'image avance a une cadence calculee a la naissance</b> : <c>+0x16 = +0x14 / 3</c>, soit
    /// 3 ou 4 images de tenue par dessin — une division signee par 3, emise par MSVC en
    /// <c>imul 0x55555556</c> suivi du rattrapage de signe. <c>+0x5c</c> compte les images, et
    /// quand il atteint <c>+0x16</c> il repart a zero en avancant <c>+0x40</c>. Celui-ci
    /// <b>s'arrete sur 3</b> et n'y revient pas : la bouffee se fige sur sa derniere image.
    /// </para>
    /// <para>
    /// <b>Elle enfle en s'eteignant</b> : l'echelle gagne <c>0x64</c> par image des deux cotes
    /// pendant que la teinte perd 6, partie de <c>0x5a</c>.
    /// </para>
    /// <para>
    /// <b>La mort ne coupe pas le tour.</b> Le bit de vie est efface au milieu du corps, et tout ce
    /// qui suit — la teinte, l'echelle, l'avance de l'image — s'execute quand meme sur cette
    /// derniere image. C'est le binaire ; transcrit tel quel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406970(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F280();   // Ghidra: &DAT_0054f280
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            e[0x0a] = 5;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);   // le cap, fixe une fois pour toutes

            e[0x40] = 0;
            *(int*)(e + 0x5c) = 0;

            short life = (short)(CrtRand() % 5 + 0x0a);          // 10 a 14
            *(short*)(e + 0x14) = life;                          // vitesse ET compte a rebours
            *(short*)(e + 0x16) = (short)(life / 3);             // la tenue d'image, 3 ou 4

            e[0x38] = 0x5a;
            e[0x39] = 0x5a;
            e[0x3a] = 0x5a;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x20) += 0xa000;

            short yaw = *(short*)(e + 0x12);
            short speed = *(short*)(e + 0x14);

            *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(yaw) * (speed * 2);
            *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(yaw) * (speed * 2);

            speed -= 1;
            *(short*)(e + 0x14) = speed;
            // Le bit de vie tombe ici, mais le tour CONTINUE : tout ce qui suit s'execute encore.
            if (speed <= 0) *(uint*)e &= 0x7fffffff;

            BrightenEntityTint_00404DE0(e, -6, -6, -6);

            *(short*)(e + 0x28) += 0x64;
            *(short*)(e + 0x2a) += 0x64;

            *(int*)(e + 0x5c) += 1;
            if (*(int*)(e + 0x5c) >= *(short*)(e + 0x16))
            {
                *(int*)(e + 0x5c) = 0;
                e[0x40]++;
                if (e[0x40] >= 4) e[0x40] = 3;   // s'arrete sur la derniere image, ne reboucle pas
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00406aa0 @ 0x00406aa0 — <b>la poussiere posee contre une paroi</b>, sous-type
    /// <b>55</b> du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Decompilee dans
    /// <c>Blaze.c</c>. Elle partage la bande de quatre images de
    /// <see cref="EntityHandler_00406970"/> — <see cref="s_Descriptors_0054F280"/>.
    ///
    /// <para>
    /// <b>Sa naissance choisit un AXE, puis s'etale le long de l'autre.</b> Une piece decide si le
    /// pas se fait en <c>X</c> ou en <c>Z</c> ; l'axe tire recoit <c>(rand % 3 - 1) * 0x64000</c>,
    /// soit une case pleine d'un cote, de l'autre, ou rien. L'axe <b>non</b> tire recoit a la place
    /// un etalement de <c>-100</c> a <c>+99</c> cases de <c>0x1000</c>. C'est un placement en
    /// bordure : l'entite se range le long d'un mur plutot qu'au hasard dans le volume.
    /// </para>
    /// <para>
    /// <b>Et il y a un cas ou elle part en diagonale.</b> Si l'axe tire sort un <c>0</c> — une fois
    /// sur trois —, les deux pas sont nuls, donc <b>les deux</b> axes recoivent l'etalement, et le
    /// meme : l'entite se pose sur la diagonale exacte. Le binaire teste les deux
    /// independamment ; transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Trois etats, et le bit <c>0x4000</c> fait l'attente.</b> Meme patron que
    /// <see cref="EntityHandler_0040F650"/> : l'etat 1 attend <c>+0x14</c> images (0 a 9) avec le
    /// bit pose ; a son terme l'etat passe a 2 et le bit tombe. L'etat 2 monte de <c>0x5000</c> par
    /// image et tire son image de <c>+0x12 &gt;&gt; 2</c> — quatre images de tenue chacune —, puis
    /// meurt quand l'indice depasse 3.
    /// </para>
    /// <para>
    /// <b>Le compteur sert deux fois.</b> <c>+0x12</c> compte les images de l'attente, est remis a
    /// zero au passage a l'etat 2, et recompte celles de l'animation.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406AA0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F280();   // Ghidra: &DAT_0054f280
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            e[0x0a] = 5;
            e[0x0b] = 0;

            // Pile ou face : le pas se fait en X ou en Z, jamais dans les deux.
            short stepX = 0;
            short stepZ = 0;
            if (CrtRand() % 2 == 0) stepX = (short)(CrtRand() % 3 - 1);
            else                    stepZ = (short)(CrtRand() % 3 - 1);

            short spread = (short)(CrtRand() % 200 - 100);

            // L'axe qui n'a pas de pas recoit l'etalement. Quand le pas tire vaut 0, LES DEUX en
            // recoivent un, et le meme : l'entite part sur la diagonale.
            *(int*)(e + 0x1c) += stepX * 0x64000 + (stepX == 0 ? spread << 0xc : 0);
            *(int*)(e + 0x24) += stepZ * 0x64000 + (stepZ == 0 ? spread << 0xc : 0);

            // Puis une gigue de +-5 cases sur les trois axes.
            *(int*)(e + 0x1c) += (CrtRand() % 10 - 5) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 10 - 5) * 0x1000;

            e[0x40] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(uint*)e |= 0x4000;                          // pose : l'attente ne se montre pas

            *(int*)(e + 0x24) += (CrtRand() % 10 - 5) * 0x1000;

            e[0x38] = 0x50;
            e[0x39] = 0x50;
            e[0x3a] = 0x50;
            *(short*)(e + 0x14) = (short)(CrtRand() % 10);   // l'attente, 0 a 9 images
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x14) < *(short*)(e + 0x12))
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(uint*)e &= 0xffffbfff;                  // tombe : elle se montre
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) += 1;
            *(int*)(e + 0x20) += 0x5000;

            byte frame = (byte)(*(short*)(e + 0x12) >> 2);   // quatre images de tenue chacune
            e[0x40] = frame;
            if (frame > 3) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f250</c> — <b>la seconde bande de quatre images</b>, celle de
    /// <see cref="EntityHandler_00406C40"/>.
    ///
    /// <para>
    /// Meme forme que <see cref="s_Descriptors_0054F280"/> : quatre tuiles <c>0x1f x 0x1f</c>
    /// alignees, <c>x</c> avancant de <c>0x20</c> — mais sur la ligne <c>y = 0xc0</c>, et sur une
    /// autre page (<c>00 01 18 00</c> au lieu de <c>01 02 18 00</c>).
    /// </para>
    /// <para>
    /// <b>La borne est le debut de la bande suivante</b> : a <c>0x0054f270</c> le <c>y</c> retombe
    /// de <c>0xc0</c> a <c>0x20</c>, donc une autre animation commence. Le corps le confirme au
    /// compte pres — son <c>+0x40</c> avance aux images 6, 12 et 18, soit exactement jusqu'a 3.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F250 =
    [
        0x00, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f250
        0x20, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f258
        0x40, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f260
        0x60, 0xc0, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f268
    ];

    private static byte* s_Descriptors0054F250Storage;

    private static byte* Descriptors0054F250()
        => NativeCopy(s_Descriptors_0054F250, ref s_Descriptors0054F250Storage);

    /// <summary>
    /// Ghidra: <c>0x00406c40</c> — <b>l'eclat qui retombe</b>, sous-type <b>54</b> du bloc
    /// <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Absente de <c>Blaze.c</c> ; 432 octets
    /// releves au desassembleur, <c>Legacy/Raw/Blaze/FUN_00406C40.asm</c>.
    ///
    /// <para>
    /// <b>C'est le premier corps VRAIMENT balistique de ce fichier.</b> Les autres tournent, montent
    /// ou derivent ; celui-ci se donne une vitesse sur les trois axes — <c>±8</c> cases en <c>X</c>
    /// et <c>Z</c>, <c>+6</c> a <c>+15</c> vers le haut — et <b>la vitesse verticale perd
    /// <c>0x1000</c> par image</b>. Il monte, il ralentit, il retombe.
    /// </para>
    /// <para>
    /// <b>Vingt-quatre images, ni plus ni moins</b> : la mort est un test d'egalite sur
    /// <c>+0x12 == 0x18</c>, pas une comparaison. L'image avance aux images <b>6, 12 et 18</b>,
    /// trois tests d'egalite a la suite — l'indice va donc exactement de 0 a 3, et la bande en porte
    /// quatre.
    /// </para>
    /// <para>
    /// <b>Une variante par <c>+0x14</c></b>, et elle ne change qu'une chose : <c>+0x0a</c> vaut
    /// <c>8</c> quand <c>+0x14</c> vaut 1, <c>6</c> sinon.
    /// </para>
    /// <para>
    /// <b>L'echelle est ecrite DEUX fois a la naissance</b>, comme le <c>+0x12</c> de la variante 2
    /// de <see cref="EntityHandler_00406130"/> : d'abord <c>rand() % 0x400 + 0xc00</c> sur
    /// <c>+0x28</c> et <c>+0x2a</c>, puis <c>rand() % 0x800 + 0x800</c> sur les deux, qui ecrase.
    /// Les deux premiers tirages sont <b>morts</b> — il ne reste d'eux que leur consommation du
    /// generateur, qui decale la suite. Transcrit tel quel : c'est la troisieme ecriture morte
    /// relevee dans ce fichier.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406C40(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F250();   // Ghidra: &DAT_0054f250

            // Premiere echelle : ECRASEE plus bas. Seule sa consommation du generateur compte.
            *(short*)(e + 0x28) = (short)(CrtRand() % 0x400 + 0xc00);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x400 + 0xc00);

            e[0x0a] = *(short*)(e + 0x14) == 1 ? (byte)8 : (byte)6;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0x64 - 0x32) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x64 - 0x32) * 0x1000;

            *(int*)(e + 0x50) = (CrtRand() % 0x10 - 8) * 0x1000;    // vitesse X
            *(int*)(e + 0x54) = (CrtRand() % 0x0a + 6) * 0x1000;    // vitesse Y, vers le haut
            *(int*)(e + 0x58) = (CrtRand() % 0x10 - 8) * 0x1000;    // vitesse Z

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x800 + 0x800);   // la vraie echelle
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x800 + 0x800);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) += *(int*)(e + 0x50);
            *(int*)(e + 0x20) += *(int*)(e + 0x54);
            *(int*)(e + 0x54) -= 0x1000;                  // la gravite
            *(int*)(e + 0x24) += *(int*)(e + 0x58);

            *(short*)(e + 0x12) += 1;
            short age = *(short*)(e + 0x12);

            if (age == 0x18) *(uint*)e &= 0x7fffffff;     // egalite, pas comparaison
            if (age == 6 || age == 0x0c || age == 0x12) e[0x40]++;
        }
    }
}
