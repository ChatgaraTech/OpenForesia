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
    /// <b>SIX enregistrements</b>, et la borne haute est <c>0x0054f1b0</c>, la planche de
    /// <see cref="EntityHandler_00406680"/> : une borne prouvee par le code.
    /// </para>
    /// <para>
    /// <b>CORRIGE LE 2026-09-16 : cette plage a d'abord ete relevee a CINQ.</b> Le sixieme
    /// enregistrement, <c>0x0054f1a8</c>, est le motif separateur sur la page <c>0x03</c>, et il
    /// avait ete pris pour la borne — comme le motif de <c>0x0054f180</c> l'est pour la plage
    /// precedente. Puis <see cref="EntityHandler_004079E0"/> est apparu, qui pose exactement
    /// <c>&amp;DAT_0054f1a8</c> en <c>+0x3c</c> : c'est un enregistrement, pas une borne. La lecon
    /// se repete — <b>un motif separateur est une tuile, et quelqu'un finit toujours par s'en
    /// servir</b>.
    /// </para>
    /// <para>
    /// <b>TROIS gestionnaires se partagent ce bloc, chacun a son decalage</b>, et aucun ne lit ce
    /// que lit un autre :
    /// </para>
    /// <list type="table">
    ///   <item><term>+0</term><description><see cref="EntityHandler_00406130"/>, qui ne bouge
    ///     jamais son <c>+0x40</c> : l'enregistrement 0 seul.</description></item>
    ///   <item><term>+2</term><description><see cref="EntityHandler_00407AD0"/>, qui tire son
    ///     <c>+0x40</c> entre 0 et 2 : les trois tuiles <c>0x10 x 0x10</c> de la page
    ///     <c>0x03</c>.</description></item>
    ///   <item><term>+5</term><description><see cref="EntityHandler_004079E0"/>, le motif
    ///     separateur, seul.</description></item>
    /// </list>
    /// <para>
    /// C'est le cas d'ecole de la regle « relever la plage, pas le descripteur », et il s'est
    /// presente <b>trois fois</b> sur ce seul bloc.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F180 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f180  <- 0x00406130
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f188
        0x00, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f190  <- 0x00407AD0, image 0
        0x10, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f198  <- 0x00407AD0, image 1
        0x20, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1a0  <- 0x00407AD0, image 2
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f1a8  <- 0x004079E0
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
    /// ressemblent a deux animations de quatre images. <see cref="EntityHandler_00406680"/> n'en
    /// atteint aucune : son <c>+0x40</c> est pose a zero et n'est jamais avance.
    /// </para>
    /// <para>
    /// <b>MAIS UN AUTRE GESTIONNAIRE JOUE LA SECONDE.</b> <see cref="EntityHandler_004070F0"/> pose
    /// <c>&amp;DAT_0054f1e8</c> en <c>+0x3c</c> — soit <b>cette plage au decalage 7</b>
    /// (<c>0x0054f1e8 - 0x0054f1b0 = 0x38 = 7 * 8</c>) — et parcourt les enregistrements 7 a 10.
    /// C'est exactement le cas que la regle « relever la plage, pas le descripteur » protege : la
    /// plage relevee au plus juste sur ce que lisait le premier corps aurait manque au second.
    /// Les deux se partagent le bloc, chacun a son decalage.
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
        => TickMineSparkle_00406810(e, Descriptors0054F2A0(), Descriptors0054F2B0());

    /// <summary>
    /// Ghidra: FUN_00408680 @ 0x00408680 — <b>le meme eclat, pour les sous-zones 5 a 8</b>.
    /// Sous-type <b>60</b> du bloc <c>0x00554528</c>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Compare a <see cref="EntityHandler_00406810"/> ligne par ligne : deux differences, et ce
    /// sont deux adresses de planche.</b> Et les planches elles-memes sont des copies conformes —
    /// <c>0x0054f3f8</c> reprend les deux enregistrements de <c>0x0054f2a0</c>, <c>0x0054f408</c>
    /// ceux de <c>0x0054f2b0</c>, octet pour octet. Le jeu de 1998 a duplique le code ET la donnee,
    /// une fois par groupe de sous-zones.
    /// </para>
    /// <para>
    /// Le corps est donc ecrit UNE fois, avec ses deux planches en parametres : c'est une unite
    /// reelle, deux sites l'emploient a l'identique.
    /// </para>
    /// </summary>
    private static void EntityHandler_00408680(byte* e)
        => TickMineSparkle_00406810(e, Descriptors0054F3F8(), Descriptors0054F408());

    /// <summary>
    /// Le corps partage par <see cref="EntityHandler_00406810"/> (sous-zones 0 a 2) et
    /// <see cref="EntityHandler_00408680"/> (sous-zones 5 a 8). Voir le premier pour ce qu'il fait.
    /// </summary>
    private static void TickMineSparkle_00406810(byte* e, byte* plateA, byte* plateB)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            if (*(short*)(e + 0x14) == 0)
            {
                *(byte**)(e + 0x3c) = plateA;
                e[0x0a] = 7;
                e[0x0b] = 0;
                *(int*)(e + 0x20) += 0x50000;
            }

            if (*(short*)(e + 0x14) == 1)
            {
                *(byte**)(e + 0x3c) = plateB;
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
        => TickMineWallDust_00406AA0(e, Descriptors0054F280());

    /// <summary>
    /// Ghidra: FUN_00408910 @ 0x00408910 — <b>la meme poussiere, pour les sous-zones 5 a 8</b>.
    /// Sous-type <b>55</b> du bloc <c>0x00554528</c>. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Compare a <see cref="EntityHandler_00406AA0"/> ligne par ligne : UNE difference, et c'est
    /// l'adresse de la planche.</b> Et la planche <c>0x0054f3d8</c> est la copie conforme de
    /// <c>0x0054f280</c> — les quatre memes tuiles, octet pour octet. Code et donnee dupliques,
    /// comme pour <see cref="EntityHandler_00408680"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00408910(byte* e)
        => TickMineWallDust_00406AA0(e, Descriptors0054F3D8());

    /// <summary>
    /// Le corps partage par <see cref="EntityHandler_00406AA0"/> (sous-zones 0 a 2) et
    /// <see cref="EntityHandler_00408910"/> (sous-zones 5 a 8). Voir le premier pour ce qu'il fait.
    /// </summary>
    private static void TickMineWallDust_00406AA0(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;
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

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f230</c> — <b>la troisieme bande de quatre images</b>, celle de
    /// <see cref="EntityHandler_00406DF0"/>. Quatre tuiles <c>0x1f x 0x1f</c> sur la ligne
    /// <c>y = 0x40</c>, <c>x</c> avancant de <c>0x20</c>, meme page que
    /// <see cref="s_Descriptors_0054F250"/> (<c>00 01 18 00</c>).
    ///
    /// <para>
    /// Bornee par <c>0x0054f250</c>, la planche de <see cref="EntityHandler_00406C40"/> : une borne
    /// prouvee par le code. Et le corps confirme le compte autrement que les deux precedents — il
    /// ne parcourt pas la bande, il <b>tire son image au hasard entre 0 et 3</b> a chaque tour, ce
    /// qui exige les quatre en permanence.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F230 =
    [
        0x00, 0x40, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f230
        0x20, 0x40, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f238
        0x40, 0x40, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f240
        0x60, 0x40, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f248
    ];

    private static byte* s_Descriptors0054F230Storage;

    private static byte* Descriptors0054F230()
        => NativeCopy(s_Descriptors_0054F230, ref s_Descriptors0054F230Storage);

    /// <summary>
    /// Ghidra: FUN_00406df0 @ 0x00406df0 — <b>l'eclat qui retombe en scintillant</b>, sous-type
    /// <b>53</b> du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Decompilee dans
    /// <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Meme balistique que <see cref="EntityHandler_00406C40"/></b> : une vitesse sur les trois
    /// axes, la verticale perdant <c>0x1000</c> par image, et une mort a la vingt-quatrieme image
    /// sur un test d'EGALITE. Les vitesses sont simplement plus molles — <c>±10</c> cases au lieu
    /// de <c>±8</c> a l'horizontale, <c>+3</c> a <c>+7</c> au lieu de <c>+6</c> a <c>+15</c> vers
    /// le haut — et la dispersion de naissance plus serree.
    /// </para>
    /// <para>
    /// <b>Ce qui les separe est l'image.</b> Celle de <c>0x00406c40</c> avance aux images 6, 12 et
    /// 18, dans l'ordre. Celle-ci <b>tire la sienne au hasard entre 0 et 3 a chaque tour</b>, et
    /// une fois de plus a la naissance : elle ne joue pas une animation, elle scintille.
    /// </para>
    /// <para>
    /// <b>Elle ne touche jamais sa teinte</b> — ni a la naissance, ni au tour. Ce qu'elle porte en
    /// <c>+0x38</c>/<c>+0x39</c>/<c>+0x3a</c> lui vient de qui la fait naitre.
    /// </para>
    /// <para>
    /// <b>Le corps est dans <see cref="TickFallingGlint_00406DF0"/></b>, planche en parametre
    /// depuis le 2026-09-16 : <c>FUN_00408c60</c> en est la copie conforme a l'adresse de planche
    /// pres.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406DF0(byte* e)
        => TickFallingGlint_00406DF0(e, Descriptors0054F230());   // Ghidra: &DAT_0054f230

    /// <summary>
    /// Le corps partage par <see cref="EntityHandler_00406DF0"/> (sous-zones 0 a 2) et
    /// <see cref="EntityHandler_00408C60"/> (5 a 8). Voir le premier pour ce qu'il fait. La planche
    /// doit porter quatre enregistrements a partir de <paramref name="plate"/> : l'image est tiree
    /// entre 0 et 3 a chaque tour.
    /// </summary>
    private static void TickFallingGlint_00406DF0(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;
            e[0x0a] = 3;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0x1e - 0x0f) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 10 - 5) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x1e - 0x0f) * 0x1000;

            *(int*)(e + 0x50) = (CrtRand() % 0x14 - 10) * 0x1000;   // vitesse X
            *(int*)(e + 0x54) = (CrtRand() % 5 + 3) * 0x1000;       // vitesse Y, vers le haut
            *(int*)(e + 0x58) = (CrtRand() % 0x14 - 10) * 0x1000;   // vitesse Z

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x400 + 0xc00);
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x400 + 0xc00);

            e[0x40] = (byte)(CrtRand() % 4);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) += *(int*)(e + 0x50);
            *(int*)(e + 0x20) += *(int*)(e + 0x54);
            *(short*)(e + 0x12) += 1;
            *(int*)(e + 0x24) += *(int*)(e + 0x58);
            *(int*)(e + 0x54) -= 0x1000;                  // la gravite

            if (*(short*)(e + 0x12) == 0x18) *(uint*)e &= 0x7fffffff;

            e[0x40] = (byte)(CrtRand() % 4);              // elle scintille, elle n'anime pas
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f208</c> — <b>la quatrieme bande de quatre images</b>, celle de
    /// <see cref="EntityHandler_00406F60"/>. Quatre tuiles <c>0x1f x 0x1f</c> sur la ligne
    /// <c>y = 0x00</c>, page <c>00 03 18 00</c>.
    ///
    /// <para>
    /// <b>Bornee par un changement d'attribut</b> : a <c>0x0054f228</c> le cinquieme octet passe de
    /// <c>0x00</c> a <c>0x01</c>. C'est aussi la borne haute de
    /// <see cref="s_Descriptors_0054F1B0"/>, vue par l'autre bout — les deux plages se touchent
    /// sans se recouvrir.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F208 =
    [
        0x00, 0x00, 0x1f, 0x1f, 0x00, 0x03, 0x18, 0x00,   // 0x0054f208
        0x20, 0x00, 0x1f, 0x1f, 0x00, 0x03, 0x18, 0x00,   // 0x0054f210
        0x40, 0x00, 0x1f, 0x1f, 0x00, 0x03, 0x18, 0x00,   // 0x0054f218
        0x60, 0x00, 0x1f, 0x1f, 0x00, 0x03, 0x18, 0x00,   // 0x0054f220
    ];

    private static byte* s_Descriptors0054F208Storage;

    private static byte* Descriptors0054F208()
        => NativeCopy(s_Descriptors_0054F208, ref s_Descriptors0054F208Storage);

    /// <summary>
    /// Ghidra: <c>0x00406f60</c> — <b>la gerbe qui monte haut et noircit</b>, sous-type <b>52</b>
    /// du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Absente de <c>Blaze.c</c> ;
    /// 400 octets releves au desassembleur, <c>Legacy/Raw/Blaze/FUN_00406F60.asm</c>.
    ///
    /// <para>
    /// <b>Troisieme balistique du fichier, et la plus haute</b> : sa vitesse verticale part entre
    /// <c>+5</c> et <c>+24</c> cases, contre <c>+6</c> a <c>+15</c> pour
    /// <see cref="EntityHandler_00406C40"/> et <c>+3</c> a <c>+7</c> pour
    /// <see cref="EntityHandler_00406DF0"/>. Meme gravite de <c>0x1000</c> par image.
    /// </para>
    /// <para>
    /// <b>ELLE INVERSE SINUS ET COSINUS.</b> Sa naissance avance le long du cap <c>+0x32</c> de
    /// cinquante unites, comme plusieurs autres — mais ici <c>Z</c> recoit le <b>sinus</b> et
    /// <c>X</c> le <b>cosinus</b>, la ou tout le reste de cette famille fait l'inverse. Verifie sur
    /// le desassemblage : <c>0x00545a1c</c> (sinus) ecrit dans <c>+0x24</c>, <c>0x00545a16</c>
    /// (cosinus) dans <c>+0x1c</c>. Le placement de naissance est donc tourne d'un quart de tour
    /// par rapport a ses voisines. Transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Elle noircit par debordement.</b> Chaque image ajoute <c>0xdc</c> aux trois canaux — un
    /// <b>ajout d'octet qui deborde</b>, soit <c>-36</c> en pratique, mais sans borne a zero : un
    /// canal qui passe sous zero repart de <c>0xff</c>. Meme convention que le flash de sortie de
    /// zone, ou « les additions debordent sans etre bornees ».
    /// </para>
    /// <para>
    /// <b>Sa mort est au bout de l'animation, pas d'un compteur d'age.</b> <c>+0x12</c> tient
    /// l'image pendant 2 a 4 tours ; quand <c>+0x14</c> l'atteint, <c>+0x40</c> avance, et l'entite
    /// s'eteint des que l'indice atteindrait 4. Elle vit donc entre 8 et 16 images selon le
    /// tirage.
    /// </para>
    /// </summary>
    private static void EntityHandler_00406F60(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x0a] = 1;
            e[0x0b] = 0;
            *(byte**)(e + 0x3c) = Descriptors0054F208();   // Ghidra: &DAT_0054f208

            *(int*)(e + 0x1c) += (CrtRand() % 0x96 - 0x4b) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0xc8 - 0x64) * 0x1000;

            // Le quart de tour : SINUS sur Z, COSINUS sur X. L'inverse de ses voisines.
            short yaw = *(short*)(e + 0x32);
            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(yaw) * 50;
            *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(yaw) * 50;

            *(int*)(e + 0x50) = (CrtRand() % 0x0c - 6) * 0x1000;    // vitesse X
            *(int*)(e + 0x54) = (CrtRand() % 0x14 + 5) * 0x1000;    // vitesse Y, la plus haute
            *(int*)(e + 0x58) = (CrtRand() % 0x0c - 6) * 0x1000;    // vitesse Z

            *(short*)(e + 0x10) = 1;

            short scale = (short)(CrtRand() % 0x800 + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            *(short*)(e + 0x14) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)(CrtRand() % 3 + 2);   // la tenue d'image, 2 a 4
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) += *(int*)(e + 0x50);
            *(int*)(e + 0x20) += *(int*)(e + 0x54);
            *(int*)(e + 0x54) -= 0x1000;                  // la gravite
            *(int*)(e + 0x24) += *(int*)(e + 0x58);

            *(short*)(e + 0x14) += 1;

            // Ajout d'OCTET : il deborde, il ne se borne pas a zero.
            e[0x38] += 0xdc;
            e[0x39] += 0xdc;
            e[0x3a] += 0xdc;

            if (*(short*)(e + 0x14) == *(short*)(e + 0x12))
            {
                *(short*)(e + 0x14) = 0;
                e[0x40]++;
                if (e[0x40] >= 4) *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x004070f0</c> — <b>la petite animation qui ne bouge pas</b>, sous-type <b>62</b>
    /// du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Absente de <c>Blaze.c</c> ;
    /// 240 octets releves au desassembleur, <c>Legacy/Raw/Blaze/FUN_004070F0.asm</c>.
    ///
    /// <para>
    /// <b>C'est le corps le plus simple du fichier.</b> Elle se pose a <c>±20</c> cases de sa
    /// naissance sur les trois axes, se donne deux echelles, et <b>ne se deplace plus jamais</b> :
    /// aucune vitesse, aucune ancre, aucune trigonometrie. Son tour ne fait qu'avancer une image
    /// toutes les deux prises, et mourir au bout de quatre — huit images de vie, fixes.
    /// </para>
    /// <para>
    /// <b>ELLE PARTAGE LA PLANCHE DE <see cref="EntityHandler_00406680"/>, A UN DECALAGE.</b>
    /// Le binaire ecrit <c>&amp;DAT_0054f1e8</c>, qui est <see cref="s_Descriptors_0054F1B0"/> plus
    /// <c>0x38</c>, soit son <b>enregistrement 7</b>. Les quatre images qu'elle parcourt (7 a 10)
    /// sont celles que la plage du 0x00406680 portait sans consommateur connu. Le relevé au plus
    /// juste aurait casse celle-ci ; c'est la demonstration de la regle, prise sur le fait.
    /// </para>
    /// <para>
    /// <b>Ses deux echelles sont tirees separement</b>, et par SOUSTRACTION : <c>0x1400</c> moins
    /// un tirage sur onze bits, donc entre <c>0xc00</c> et <c>0x1400</c>. La plupart de ses
    /// voisines tirent une seule echelle et la posent sur les deux axes ; celle-ci en tire deux,
    /// donc elle peut naitre etiree.
    /// </para>
    /// <para>
    /// <b>Sa mort teste l'EGALITE a 4</b>, pas le depassement. Un <c>+0x40</c> qui arriverait a 5
    /// par un autre chemin ne la tuerait pas — il n'y en a pas, mais c'est ce que le binaire dit.
    /// </para>
    /// </summary>
    private static void EntityHandler_004070F0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            // &DAT_0054f1e8 = s_Descriptors_0054F1B0 + 7 enregistrements.
            *(byte**)(e + 0x3c) = Descriptors0054F1B0() + 0x38;   // Ghidra: &DAT_0054f1e8
            e[0x0a] = 9;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x28 - 0x14) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x28 - 0x14) * 0x1000;

            // Par SOUSTRACTION, et deux tirages distincts : elle peut naitre etiree.
            *(short*)(e + 0x28) = (short)(0x1400 - CrtRand() % 0x800);
            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x2a) = (short)(0x1400 - CrtRand() % 0x800);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) == 2)
            {
                *(short*)(e + 0x12) = 0;
                e[0x40]++;
                if (e[0x40] == 4) *(uint*)e &= 0x7fffffff;   // egalite, pas depassement
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00407800 @ 0x00407800 — <b>la soeur de
    /// <see cref="EntityHandler_00406F60"/></b>, sous-type <b>50</b> du bloc <c>0x00553ea8</c>,
    /// sous-zones 0 a 2 de la mine. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Les deux se reconnaissent a leurs deux signatures communes</b> : elles avancent le long
    /// du cap en mettant le <b>sinus sur Z</b> et le <b>cosinus sur X</b> — l'inverse du reste de
    /// la famille — et elles ecrivent <c>+0x28</c>/<c>+0x2a</c> <b>deux fois</b> a la naissance, le
    /// second tirage ecrasant le premier.
    /// </para>
    /// <para>
    /// Ce qui les separe : celle-ci avance de <c>0x32</c> et non de <c>50</c>, monte plus fort
    /// (<c>+10</c> a <c>+24</c> cases) et <b>sous une gravite DOUBLE</b>, <c>0x2000</c> par image.
    /// Elle noircit deux fois plus vite aussi (<c>-0x24</c> en arithmetique signee d'octet).
    /// </para>
    /// <para>
    /// <b>Deux portes pour mourir</b>, comme <c>EntityHandler_0040F470</c> : passe la dixieme
    /// image, une chance sur six par image ; et de toute facon a la seizieme.
    /// </para>
    /// <para>
    /// <b>Sa planche est <see cref="s_Descriptors_0054F1B0"/> au decalage 1</b>
    /// (<c>0x0054f1b8</c>), et son <c>+0x40</c> ne bouge jamais : elle ne lit que cet
    /// enregistrement-la.
    /// </para>
    /// </summary>
    private static void EntityHandler_00407800(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F1B0() + 8;   // Ghidra: &DAT_0054f1b8

            // Premiere echelle : ECRASEE plus bas.
            *(short*)(e + 0x28) = (short)(CrtRand() % 0x400 + 0xc00);
            e[0x0a] = 0x0d;
            e[0x0b] = 0;
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x400 + 0xc00);

            *(int*)(e + 0x1c) += (CrtRand() % 0x96 - 0x4b) * 0x1000;
            *(int*)(e + 0x20) += (CrtRand() % 0x32 - 0x19) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 200 - 100) * 0x1000;

            // Le quart de tour, comme sa soeur : SINUS sur Z, COSINUS sur X.
            short yaw = *(short*)(e + 0x32);
            *(int*)(e + 0x24) += (int)Winsys3d.GetSinTableEntry(yaw) * 0x32;
            *(int*)(e + 0x1c) += Winsys3d.GetCosTableEntry(yaw) * 0x32;

            *(int*)(e + 0x50) = (CrtRand() % 6 - 3) * 0x1000;
            *(int*)(e + 0x54) = (CrtRand() % 0x0f + 10) * 0x1000;
            *(int*)(e + 0x58) = (CrtRand() % 6 - 3) * 0x1000;

            *(short*)(e + 0x28) = (short)(CrtRand() % 0xc00 + 0x1000);   // la vraie echelle
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0xc00 + 0x1000);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) += *(int*)(e + 0x50);
            *(int*)(e + 0x20) += *(int*)(e + 0x54);
            *(int*)(e + 0x24) += *(int*)(e + 0x58);
            *(short*)(e + 0x12) += 1;

            // Arithmetique d'OCTET SIGNE : elle deborde, elle ne se borne pas.
            e[0x38] = (byte)((sbyte)e[0x38] - 0x24);
            *(int*)(e + 0x54) -= 0x2000;                  // gravite DOUBLE
            e[0x39] = (byte)((sbyte)e[0x39] - 0x24);
            e[0x3a] = (byte)((sbyte)e[0x3a] - 0x24);

            // Deux portes : une chance sur six passe la dixieme image, et la seizieme de toute facon.
            int roll = CrtRand();
            if ((*(short*)(e + 0x12) > 10 && (short)(roll % 6) == 1)
                || *(short*)(e + 0x12) == 0x10)
            {
                *(uint*)e &= 0x7fffffff;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_004079e0 @ 0x004079e0 — <b>la bulle qui monte et se degonfle</b>, sous-type
    /// <b>63</b> du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Decompilee dans
    /// <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Elle monte en ralentissant, puis retrecit, puis disparait</b> — et tout tient dans un
    /// seul compteur, <c>+0x12</c>, qui est sa <b>vitesse</b>. Il part entre 10 et 19, perd un
    /// toutes les deux images, et sert de montee (<c>+0x12 * 0x1000</c> par image) autant que
    /// d'horloge : sous 7 l'echelle perd 200 par image sur les deux axes, sous 3 l'entite s'eteint.
    /// </para>
    /// <para>
    /// <b>Sa planche est <see cref="s_Descriptors_0054F180"/> au decalage 5</b>
    /// (<c>0x0054f1a8</c>) — le motif separateur, pris comme tuile unique. C'est ce corps qui a
    /// prouve que la plage en comptait six et non cinq.
    /// </para>
    /// <para>
    /// <b>Elle n'ecrit jamais <c>+0x40</c></b> : l'indice lui vient de qui la fait naitre. Comme
    /// sa plage ne porte qu'un enregistrement a partir de son decalage, tout indice non nul
    /// sortirait du bloc — le spawner pose donc zero, et le relevé de six enregistrements le
    /// couvre exactement.
    /// </para>
    /// </summary>
    private static void EntityHandler_004079E0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F180() + 0x28;   // Ghidra: &DAT_0054f1a8
            e[0x0a] = 6;
            e[0x0b] = 0;

            *(int*)(e + 0x1c) += (CrtRand() % 0xa0 - 0x50) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0xa0 - 0x50) * 0x1000;

            int lift = CrtRand();
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x28) = 0x9c4;
            *(short*)(e + 0x2a) = 0x9c4;
            *(int*)(e + 0x20) += (lift % 0x14) * 0x1000;

            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 10);   // la vitesse, 10 a 19
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x14) += 1;
            *(int*)(e + 0x20) += *(short*)(e + 0x12) * 0x1000;

            if (*(short*)(e + 0x14) == 2)
            {
                *(short*)(e + 0x14) = 0;
                *(short*)(e + 0x12) -= 1;                 // elle ralentit
            }

            if (*(short*)(e + 0x12) < 7)
            {
                *(short*)(e + 0x28) -= 200;               // puis elle retrecit
                *(short*)(e + 0x2a) -= 200;
            }

            if (*(short*)(e + 0x12) < 3) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00407ad0 @ 0x00407ad0 — <b>l'etincelle qui part en tous sens</b>, sous-type
    /// <b>49</b> du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Decompilee dans
    /// <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Sa vitesse est tiree gros et decalee deux fois.</b> <c>+0x50</c> et <c>+0x54</c> sont
    /// tires en <c>(rand % 10 - 5) * 0x2000</c> puis <b>encore decales d'un bit</b> par un
    /// <c>&lt;&lt; 1</c> separe — soit <c>* 0x4000</c> en tout, ce que <c>+0x58</c> obtient
    /// directement. Le binaire fait le detour ; il est transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>Et elle tremble</b> : a chaque image, en plus de sa vitesse, elle prend <c>±3</c> cases
    /// de gigue sur les trois axes. Son echelle retrecit de 200 par image et elle meurt a la
    /// neuvieme.
    /// </para>
    /// <para>
    /// <b>Sa planche est <see cref="s_Descriptors_0054F180"/> au decalage 2</b>
    /// (<c>0x0054f190</c>), et son <c>+0x40</c> est <b>tire entre 0 et 2</b> a chaque image : elle
    /// scintille entre les trois tuiles <c>0x10 x 0x10</c> de la page <c>0x03</c>. C'est le corps
    /// qui donne un consommateur a ces trois-la.
    /// </para>
    /// <para>
    /// <b>Le corps est dans <see cref="TickJitterSpark_00407AD0"/></b>, planche en parametre depuis
    /// le 2026-09-16 : <c>FUN_00409d00</c> et <c>FUN_0040cc60</c> en sont des copies conformes a
    /// l'adresse de planche pres.
    /// </para>
    /// </summary>
    private static void EntityHandler_00407AD0(byte* e)
        => TickJitterSpark_00407AD0(e, Descriptors0054F180() + 0x10);   // Ghidra: &DAT_0054f190

    /// <summary>
    /// Le corps partage par <see cref="EntityHandler_00407AD0"/> (sous-zones 0 a 2),
    /// <see cref="EntityHandler_00409D00"/> (5 a 8) et <see cref="EntityHandler_0040CC60"/>
    /// (3, 4 et 9). Voir le premier pour ce qu'il fait. La planche doit porter trois
    /// enregistrements a partir de <paramref name="plate"/> : l'image est tiree entre 0 et 2.
    /// </summary>
    private static void TickJitterSpark_00407AD0(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;
            e[0x0a] = 8;
            e[0x0b] = 0;

            *(short*)(e + 0x12) = (short)(CrtRand() % 10 + 10);

            *(int*)(e + 0x50) = (CrtRand() % 10 - 5) * 0x2000;
            *(int*)(e + 0x54) = (CrtRand() % 10 - 5) * 0x2000;
            *(int*)(e + 0x50) <<= 1;                      // le detour du binaire : * 0x2000 puis << 1
            *(int*)(e + 0x54) <<= 1;

            e[0x40] = 0;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x58) = (CrtRand() % 10 - 5) * 0x4000;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x1c) += (CrtRand() % 6 - 3) * 0x1000 + *(int*)(e + 0x50);
            *(int*)(e + 0x20) += (CrtRand() % 6 - 3) * 0x1000 + *(int*)(e + 0x54);
            *(short*)(e + 0x12) += 1;
            *(int*)(e + 0x24) += (CrtRand() % 6 - 3) * 0x1000 + *(int*)(e + 0x58);

            if (*(short*)(e + 0x12) > 8) *(uint*)e &= 0x7fffffff;

            *(short*)(e + 0x28) -= 200;
            *(short*)(e + 0x2a) -= 200;

            e[0x40] = (byte)(CrtRand() % 3);              // elle scintille entre les trois images
        }
    }

    /// <summary>
    /// Ghidra: FUN_00407c10 @ 0x00407c10 — <b>le halo sonore accroche a son porteur</b>, sous-type
    /// <b>31</b> du bloc <c>0x00554328</c>, sous-zones <b>5 a 8</b> de la mine. Decompilee dans
    /// <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Il n'existe que par son porteur</b>, lu en <c>+0x6c</c> : nul, le corps entier est saute.
    /// Il recopie sa position a chaque image — avec un <c>Y</c> releve de <c>0x50000</c> — ainsi que
    /// son cap.
    /// </para>
    /// <para>
    /// <b>C'est d'abord une machine a son.</b> A la naissance il joue <c>0x1f</c>, puis il entretient
    /// un <b>cycle de cinq images</b> : a la premiere de chaque cycle il joue <c>0x23</c> ou
    /// <c>0x20</c> selon le mode qu'il s'est donne. Ce mode vient du <c>+0x16</c> du porteur : 0 ou
    /// 1 donnent le mode 2, tout le reste le mode 1.
    /// </para>
    /// <para>
    /// <b>Il meurt de deux facons</b>, toutes deux dictees par le porteur : si le <c>+0x43</c> de
    /// celui-ci vaut 1, ou si le porteur lui-meme est marque mort ou en retrait
    /// (<c>0x40000000</c> pose, ou <c>0x80000000</c> tombe) — la meme paire de tests que
    /// <see cref="EntityHandler_00413330"/>.
    /// </para>
    /// <para>
    /// <b>Et il seme.</b> A chaque image il avance de <c>0xb4</c> le long du cap du porteur,
    /// descend de <c>0xa000</c>, et fait naitre le sous-type <c>0xdf</c> a cette position par
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/>. Le decalage est applique a l'entite
    /// elle-meme, donc la trainee s'eloigne du porteur image apres image jusqu'a ce que la recopie
    /// du tour suivant la ramene.
    /// </para>
    /// </summary>
    private static void EntityHandler_00407C10(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier == null) return;

        *(byte**)(e + 0x74) = carrier;
        *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
        *(int*)(e + 0x20) = *(int*)(carrier + 0x20) + 0x50000;
        *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
        *(short*)(e + 0x32) = *(short*)(carrier + 0x32);

        if (*(short*)(e + 0x10) == 0)
        {
            short carried = *(short*)(carrier + 0x16);
            *(short*)(e + 0x9a) = (carried == 0 || carried == 1) ? (short)2 : (short)1;

            *(short*)(e + 0x10) = 1;
            TryPlaySoundNearEntity((uint*)e, 0x1f, 0x6fd, 0x1000, 0, 0);   // Ghidra: FUN_00468080
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x98) == 1)
        {
            short sound = *(short*)(e + 0x9a) == 1 ? (short)0x23 : (short)0x20;
            TryPlaySoundNearEntity((uint*)e, sound, 0x1000, 0x800, 0, 0);
        }

        if (*(short*)(e + 0x98) == 5) *(short*)(e + 0x98) = 0;
        *(short*)(e + 0x98) += 1;

        if (carrier[0x43] == 1) *(uint*)e &= 0x7fffffff;

        uint carriedFlags = *(uint*)carrier;
        if ((carriedFlags & 0x40000000) != 0 || (carriedFlags & 0x80000000) == 0)
        {
            *(uint*)e &= 0x7fffffff;
        }

        short yaw = *(short*)(e + 0x32);
        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(yaw) * 0xb4;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(yaw) * 0xb4;
        *(int*)(e + 0x20) -= 0xa000;

        SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xdf, 0, -1);
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f418</c> — <b>la copie conforme de
    /// <see cref="s_Descriptors_0054F2C0"/></b>, celle que
    /// <see cref="EntityHandler_00408490"/> pose en <c>+0x3c</c>.
    ///
    /// <para>
    /// <b>Les trois enregistrements sont identiques octet pour octet</b> a ceux de
    /// <c>0x0054f2c0</c>. Le jeu de 1998 a duplique cette donnee comme il a duplique le corps qui
    /// la lit — c'est le meme geste, deux fois, pour deux groupes de sous-zones.
    /// </para>
    /// <para>
    /// Elle est relevee a part plutot que partagee avec sa jumelle : ce sont deux adresses
    /// distinctes dans <c>.data</c>, et les confondre ferait mentir la carte des symboles.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F418 =
    [
        0x00, 0x18, 0x18, 0x10, 0x02, 0x02, 0x10, 0x00,   // 0x0054f418 — le seul que le corps lit
        0x00, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f420
        0x10, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f428
    ];

    private static byte* s_Descriptors0054F418Storage;

    private static byte* Descriptors0054F418()
        => NativeCopy(s_Descriptors_0054F418, ref s_Descriptors0054F418Storage);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f3d8</c>, <c>&amp;DAT_0054f3f8</c>, <c>&amp;DAT_0054f408</c> — <b>les
    /// trois planches des sous-zones 5 a 8, toutes copies conformes de celles des sous-zones
    /// 0 a 2</b>.
    ///
    /// <para>
    /// <c>0x0054f3d8</c> reprend les quatre tuiles de <see cref="s_Descriptors_0054F280"/>,
    /// <c>0x0054f3f8</c> les deux de <see cref="s_Descriptors_0054F2A0"/>, <c>0x0054f408</c> les
    /// deux de <see cref="s_Descriptors_0054F2B0"/> — <b>octet pour octet</b>, verifie au
    /// desassembleur le 2026-09-16.
    /// </para>
    /// <para>
    /// Elles sont relevees a part plutot que partagees : ce sont des adresses distinctes dans
    /// <c>.data</c>, et les confondre ferait mentir la carte des symboles. C'est le CODE qui est
    /// factorise (<see cref="TickMineSparkle_00406810"/>, <see cref="TickMineWallDust_00406AA0"/>), pas la donnee.
    /// </para>
    /// <para>
    /// <b>La factorisation a d'abord sorti ces planches du controle de marge</b>, puis
    /// <c>check-descriptors.sh</c> a ete corrige le meme jour : il ne reconnaissait que le motif
    /// litteral <c>(e + 0x3c) = DescriptorsXXXX()</c> et ratait donc tout site qui passe la planche
    /// en parametre. Il suit maintenant les deux formes, et ces cinq planches sont de nouveau
    /// mesurees. Rien a faire ici, mais la lecon vaut d'etre gardee : <b>factoriser un corps peut
    /// retirer sa donnee d'un garde-fou, et le garde-fou ne le dit pas</b>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F3D8 =
    [
        0x00, 0xa0, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f3d8
        0x20, 0xa0, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f3e0
        0x40, 0xa0, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f3e8
        0x60, 0xa0, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f3f0
    ];

    /// <summary>La jumelle de <see cref="s_Descriptors_0054F2A0"/> — voir
    /// <see cref="s_Descriptors_0054F3D8"/>.</summary>
    private static readonly byte[] s_Descriptors_0054F3F8 =
    [
        0x3f, 0x20, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f3f8
        0x60, 0x20, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f400
    ];

    /// <summary>La jumelle de <see cref="s_Descriptors_0054F2B0"/> — voir
    /// <see cref="s_Descriptors_0054F3D8"/>.</summary>
    private static readonly byte[] s_Descriptors_0054F408 =
    [
        0x00, 0x20, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f408
        0x20, 0x20, 0x1f, 0x1f, 0x01, 0x02, 0x18, 0x00,   // 0x0054f410
    ];

    private static byte* s_Descriptors0054F3D8Storage;
    private static byte* s_Descriptors0054F3F8Storage;
    private static byte* s_Descriptors0054F408Storage;

    private static byte* Descriptors0054F3D8()
        => NativeCopy(s_Descriptors_0054F3D8, ref s_Descriptors0054F3D8Storage);

    private static byte* Descriptors0054F3F8()
        => NativeCopy(s_Descriptors_0054F3F8, ref s_Descriptors0054F3F8Storage);

    private static byte* Descriptors0054F408()
        => NativeCopy(s_Descriptors_0054F408, ref s_Descriptors0054F408Storage);

    /// <summary>
    /// Ghidra: <c>0x00408490</c> — <b>la marque accrochee a son porteur, seconde emission</b>,
    /// sous-type <b>48</b> du bloc <c>0x00554528</c>, sous-zones <b>5 a 8</b> de la mine. Absente
    /// de <c>Blaze.c</c> ; relevee au desassembleur, <c>Legacy/Raw/Blaze/FUN_00408490.asm</c>.
    ///
    /// <para>
    /// <b>C'est <see cref="EntityHandler_00406570"/> reemis pour l'autre groupe de sous-zones</b>,
    /// a <b>trois</b> differences pres, et aucune n'est une adresse de saut :
    /// </para>
    /// <list type="table">
    ///   <item><term>planche</term><description><c>0x0054f418</c>, qui est la copie conforme de
    ///     <c>0x0054f2c0</c> — donnee dupliquee comme le code.</description></item>
    ///   <item><term>teinte</term><description><c>0x96</c> sur les trois canaux au lieu de
    ///     <c>0x20</c> : elle nait bien plus claire.</description></item>
    ///   <item><term>hauteur</term><description>le <c>Y</c> est relu du <c>+0x64</c> de l'entite,
    ///     et non de son <c>+0x60</c>.</description></item>
    /// </list>
    /// <para>
    /// Le reste est mot pour mot : meme recopie du porteur avec le <c>Y</c> pris a son
    /// <c>+0x60</c>, meme <c>+0x2e</c> a <c>-10</c>, meme <b>hauteur ecrite deux fois</b> dont la
    /// premiere est morte, et elle ne meurt jamais d'elle-meme non plus.
    /// </para>
    /// </summary>
    private static void EntityHandler_00408490(byte* e)
    {
        byte* carrier = *(byte**)(e + 0x6c);
        if (carrier != null)
        {
            *(int*)(e + 0x1c) = *(int*)(carrier + 0x1c);
            *(int*)(e + 0x20) = *(int*)(carrier + 0x60);   // le +0x60 du PORTEUR
            *(int*)(e + 0x24) = *(int*)(carrier + 0x24);
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = Descriptors0054F418();   // Ghidra: &DAT_0054f418
            e[0x40] = 0;
            e[0x0a] = 3;
            e[0x0b] = 0;
            *(short*)(e + 0x2e) = -10;
            e[0x38] = 0x96;      // 0x96 ici, 0x20 chez la jumelle
            e[0x39] = 0x96;
            e[0x3a] = 0x96;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            // +0x64 ici, +0x60 chez la jumelle. Ecrase le Y pris au porteur, comme elle.
            *(int*)(e + 0x20) = *(int*)(e + 0x64);
        }
    }

    /// <summary>
    /// Le point que <see cref="EntityHandler_004071E0"/> calcule huit fois : un decalage a
    /// <c>angle</c>, de rayon <b>150</b>, avec une gigue de <c>±10</c> cases sur chaque composante.
    ///
    /// <para>
    /// Le binaire l'emet <b>en ligne, huit fois</b> — quatre blocs de deux points. C'est une unite
    /// reelle et non un decoupage de confort : les vingt instructions sont identiques a l'angle
    /// pres. Le rayon vient de trois <c>lea</c> (<c>x3</c>, <c>x5</c>, <c>x5</c> = 75) puis d'un
    /// doublement dans le <c>lea</c> final, soit 150.
    /// </para>
    /// <para>
    /// <b>L'ordre des tirages compte</b> et il est respecte : sinus, puis la gigue de X, puis
    /// cosinus, puis la gigue de Z. Deux appels au generateur par point, huit par naissance.
    /// </para>
    /// </summary>
    private static void MineFanPoint_004071E0(short angle, out int x, out int z)
    {
        x = (int)Winsys3d.GetSinTableEntry(angle) * 150 + (CrtRand() % 0x14 - 10) * 0x1000;
        z = Winsys3d.GetCosTableEntry(angle) * 150 + (CrtRand() % 0x14 - 10) * 0x1000;
    }

    /// <summary>
    /// Ghidra: <c>0x004071e0</c> — <b>l'eventail : une naissance placee sur un arc autour du
    /// cap</b>. Sous-type <b>51</b> du bloc <c>0x00553ea8</c>, sous-zones 0 a 2 de la mine. Absente
    /// de <c>Blaze.c</c> ; 1 568 octets releves au desassembleur — le plus gros corps de la mine a
    /// ce jour, <c>Legacy/Raw/Blaze/FUN_004071E0.asm</c>.
    ///
    /// <para>
    /// <b>Sa taille trompe : c'est une cascade de quatre blocs quasi identiques</b>, la meme forme
    /// que <c>FUN_004747F0</c> avait montree. Ce qui coute, c'est le nombre de FORMES distinctes,
    /// pas le nombre d'octets — il n'y en a qu'une ici, emise quatre fois.
    /// </para>
    /// <para>
    /// <b>LE GESTE : choisir un SECTEUR, puis un CRAN dedans.</b> Un tirage donne un secteur —
    /// entre 0 et 5, ou entre 0 et 7 quand <c>+0x12</c> vaut 2 a l'entree. Le secteur designe deux
    /// angles autour du cap <c>+0x32</c>, par pas de <c>0x200</c> (45 degres), et les deux points
    /// correspondants sont calcules par <see cref="MineFanPoint_004071E0"/>. Un second tirage
    /// choisit alors un cran parmi <c>N</c> sur le segment qui les joint, et l'entite s'y pose.
    /// </para>
    /// <list type="table">
    ///   <item><term>0 ou 4</term><description>de <c>+0x200</c> a <c>-0x200</c>, en 8 crans</description></item>
    ///   <item><term>1, ou 6-7 si <c>+0x12</c> = 2</term><description>de <c>-0x200</c> a <c>-0x400</c>, en 4 crans</description></item>
    ///   <item><term>2 ou 5</term><description>de <c>-0x400</c> a <c>+0x400</c>, en 8 crans</description></item>
    ///   <item><term>3</term><description>de <c>+0x400</c> a <c>+0x200</c>, en 4 crans</description></item>
    /// </list>
    /// <para>
    /// Les quatre tests sont <b>mutuellement exclusifs par construction</b> : chaque valeur de
    /// secteur en declenche exactement un, et tous sont couverts. Le bloc de repli du binaire
    /// (<c>LAB_00407348</c>, qui charge les cinq registres avec le secteur lui-meme) n'est donc
    /// jamais la derniere ecriture : il n'est traverse que pour atteindre le test suivant.
    /// </para>
    /// <para>
    /// <b>L'interpolation TRONQUE avant d'interpoler.</b> Les deux points sont ramenes en unites
    /// entieres (<c>&gt;&gt; 0xc</c>), le pas est divise, le resultat remis a l'echelle
    /// (<c>&lt;&lt; 0xc</c>). Les fractions de case sont donc perdues avant le calcul, pas apres.
    /// Transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>TROIS GENRES, lus dans <c>+0x12</c> a l'entree</b>, qui decident de l'echelle, de la
    /// visibilite et de la duree : le genre 0 ne touche a rien ; le genre 1 grossit de
    /// <c>0x400</c> ; le genre 2 retrecit de <c>0x320</c>. Puis <c>+0x12</c> cesse d'etre le genre
    /// et devient la <b>duree</b> : <c>(rand % 5 + 8)</c>, double pour le genre 0.
    /// </para>
    /// <para>
    /// <b>Trois etats.</b> L'etat 1 pose le bit <c>0x4000</c> a chaque image — l'entite ne se
    /// montre pas —, le leve passe la troisieme image pour les genres 0 et 1 seulement, et bascule
    /// en etat 2 des la neuvieme image OU sur une chance sur dix par image, en descendant alors de
    /// <c>0xf000</c>. L'etat 2 derive de <c>±10</c> cases par image et joue sa bande de quatre
    /// images, quatre prises chacune, avant de mourir.
    /// </para>
    /// <para>
    /// <b>Sa planche est <see cref="s_Descriptors_0054F1B0"/> au decalage 3</b>
    /// (<c>0x0054f1c8</c>). Avec elle, les DEUX suites de quatre tuiles <c>8 x 8</c> de ce bloc ont
    /// leur consommateur : celle-ci prend les enregistrements 3 a 6,
    /// <see cref="EntityHandler_004070F0"/> les 7 a 10. Quatre gestionnaires se partagent ce bloc.
    /// </para>
    /// </summary>
    private static void EntityHandler_004071E0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x2e) = 0x14;
            *(byte**)(e + 0x3c) = Descriptors0054F1B0() + 0x18;   // Ghidra: &DAT_0054f1c8

            *(short*)(e + 0x28) = (short)(CrtRand() % 0x258 + 0x1770);
            *(short*)(e + 0x2a) = (short)(CrtRand() % 0x258 + 0x1770);

            // Le genre, lu AVANT d'etre ecrase par la duree en fin de naissance.
            short kind = *(short*)(e + 0x12);

            short sector = kind == 2 ? (short)(CrtRand() % 8) : (short)(CrtRand() % 6);
            short yaw = *(short*)(e + 0x32);

            // Les cinq valeurs que le binaire porte en registres. Le repli de LAB_00407348 les
            // charge avec le secteur lui-meme ; un bloc ecrit toujours par-dessus.
            int ax = sector, az = sector, bx = sector, bz = sector;
            int step = sector, steps = sector;

            if (sector == 0 || sector == 4)
            {
                MineFanPoint_004071E0((short)(yaw + 0x200), out ax, out az);
                MineFanPoint_004071E0((short)(yaw - 0x200), out bx, out bz);
                step = CrtRand() % 8; steps = 8;
            }
            else if (sector == 1 || (kind == 2 && sector >= 6))
            {
                MineFanPoint_004071E0((short)(yaw - 0x200), out ax, out az);
                MineFanPoint_004071E0((short)(yaw - 0x400), out bx, out bz);
                step = CrtRand() % 4; steps = 4;
            }
            else if (sector == 2 || sector == 5)
            {
                MineFanPoint_004071E0((short)(yaw - 0x400), out ax, out az);
                MineFanPoint_004071E0((short)(yaw + 0x400), out bx, out bz);
                step = CrtRand() % 8; steps = 8;
            }
            else if (sector == 3)
            {
                MineFanPoint_004071E0((short)(yaw + 0x400), out ax, out az);
                MineFanPoint_004071E0((short)(yaw + 0x200), out bx, out bz);
                step = CrtRand() % 4; steps = 4;
            }

            // Le cran retenu, et l'interpolation. Tout est TRONQUE en unites entieres d'abord.
            int crans = (short)step + 1;
            int n = (short)steps;

            int a = ax >> 0xc, b = bx >> 0xc;
            *(int*)(e + 0x1c) += (a - (a - b) / n * crans) << 0xc;

            a = az >> 0xc; b = bz >> 0xc;
            *(int*)(e + 0x24) += (a - (a - b) / n * crans) << 0xc;

            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;

            *(int*)(e + 0x20) += -0xa000 - (CrtRand() % 0x14) * 0x1000;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);

            if (kind == 0) *(short*)(e + 0x9a) = 0;

            if (kind == 1)
            {
                *(short*)(e + 0x9a) = 1;
                *(short*)(e + 0x28) += 0x400;
                *(short*)(e + 0x2a) += 0x400;
            }

            if (kind == 2)
            {
                *(short*)(e + 0x9a) = 2;
                *(short*)(e + 0x28) += -0x320;
                *(short*)(e + 0x2a) += -0x320;
            }

            short mode = *(short*)(e + 0x9a);
            int longevity = (mode == 1 || mode == 2) ? 1 : 2;

            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 0;
            e[0x40] = 0;
            *(short*)(e + 0x12) = (short)((CrtRand() % 5 + 8) * longevity);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(uint*)e |= 0x4000;                          // pose a CHAQUE image
            *(short*)(e + 0x14) += 1;

            if (*(short*)(e + 0x14) > 3)
            {
                short mode = *(short*)(e + 0x9a);
                if (mode == 0 || mode == 1) *(uint*)e &= 0xffffbfff;
            }

            // Deux portes vers l'etat 2 : la neuvieme image, ou une chance sur dix par image.
            int roll = CrtRand() % 10;
            if (*(short*)(e + 0x14) > 8 || roll == 0)
            {
                *(uint*)e &= 0xffffbfff;
                *(int*)(e + 0x20) -= 0xf000;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x14) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(int*)(e + 0x1c) += (CrtRand() % 0x14 - 10) * 0x1000;
            *(int*)(e + 0x24) += (CrtRand() % 0x14 - 10) * 0x1000;

            *(short*)(e + 0x16) += 1;
            byte frame = (byte)(*(short*)(e + 0x16) >> 2);
            e[0x40] = frame;
            if (frame == 4) *(uint*)e &= 0x7fffffff;
        }
    }

    // =====================================================================
    // LES COPIES DES SOUS-ZONES 5 A 8, ET 3, 4 ET 9 (2026-09-16)
    //
    // Dix gestionnaires de la mine, et AUCUN corps neuf. Chacun est soit une amorce de quelques
    // instructions, soit un corps que le binaire a emis en double et que le depot avait deja ecrit
    // ailleurs — la foret l'a factorise, avec la planche en parametre. Recherche faite sur les
    // textes de Blaze.c, adresses masquees, puis confirmee par un `diff` des textes bruts : entre
    // chaque copie et son modele, la SEULE difference est l'adresse de planche.
    //
    //   0x00408040 -> FUN_00408050 = TickPurpleSmoke   (FUN_004103e0),  planche 0x0054f2d8
    //   0x0040A670 -> FUN_0040a680 = TickPurpleSmoke,                   planche 0x0054f450
    //   0x0040B1B0 -> FUN_0040b1f0 = TickFivePhaseBurst (FUN_004118a0), planche 0x0054f450
    //   0x0040B900 -> FUN_0040b1f0, meme corps
    //   0x00409C10, 0x0040C820     = TickRisingBubble  (FUN_0040fac0)
    //   0x00409A30, 0x0040CA80     = TickBurstingSpark (FUN_0040f470)
    //   0x00409D00, 0x0040CC60     = TickJitterSpark_00407AD0   (factorise pour elles)
    //   0x00408C60                 = TickFallingGlint_00406DF0  (idem)
    //
    // LES PLANCHES SONT RECOPIEES PAR BLOCS. Le bloc des sous-zones 5 a 8 est celui des sous-zones
    // 0 a 2 decale de 0x158, octet pour octet de 0x0054f180 a 0x0054f2d8 ; celui des sous-zones 3, 4
    // et 9 le reprend decale de 0x340, a partir de 0x0054f190 seulement. Chaque plage relevee
    // ci-dessous est donc bornee comme sa jumelle deja relevee, et les deux bornes sont verifiees
    // dans le binaire : un changement de taille de tuile ou de planche de part et d'autre.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f2d8</c> — <b>la jumelle de <see cref="s_Descriptors_0054F180"/></b>
    /// pour les sous-zones 5 a 8 : les memes six enregistrements, octet pour octet.
    ///
    /// <para>
    /// Bornee en bas par <c>0x0054f2d0</c>, dernier enregistrement de
    /// <see cref="s_Descriptors_0054F2C0"/>, et en haut par <c>0x0054f308</c>, premier de
    /// <see cref="s_Descriptors_0054F308"/> — la meme borne que la jumelle a <c>0x0054f1b0</c>.
    /// Trois consommateurs, aux memes decalages que chez la jumelle :
    /// <see cref="EntityHandler_00408040"/> a <c>+0</c>, <see cref="EntityHandler_00409D00"/> a
    /// <c>+0x10</c> (images 0 a 2), et <see cref="EntityHandler_00409C10"/> a <c>+0x28</c>, le motif
    /// separateur pris seul.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F2D8 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f2d8  <- 0x00408040
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f2e0
        0x00, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f2e8
        0x10, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f2f0
        0x20, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f2f8
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f300  <- 0x00409C10
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f308</c> — <b>la jumelle de <see cref="s_Descriptors_0054F1B0"/></b>
    /// pour les sous-zones 5 a 8, onze enregistrements. Bornee en haut par <c>0x0054f360</c>, ou la
    /// tuile passe en <c>0x1f x 0x1f</c> — la borne de la jumelle a <c>0x0054f208</c>.
    /// <see cref="EntityHandler_00409A30"/> la lit a <c>+8</c>, comme
    /// <see cref="EntityHandler_00407800"/> lit sa jumelle.
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F308 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f308
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f310  <- 0x00409A30
        0x08, 0x08, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x0054f318
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f320
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f328
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f330
        0x10, 0x10, 0x08, 0x07, 0x01, 0x03, 0x10, 0x00,   // 0x0054f338
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f340
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f348
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f350
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f358
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f450</c> — <b>la planche de la fumee et de la gerbe des sous-zones
    /// 3, 4 et 9</b>, deux enregistrements : la tuile <c>8 x 8</c> et le separateur. Meme forme que
    /// <see cref="s_Descriptors_0054F808"/> en foret, et pour la meme raison : la gerbe lit
    /// <c>+8</c> dans son role 3 et ecrit l'octet 6 de ce second enregistrement.
    ///
    /// <para>
    /// Bornee de part et d'autre par des tuiles <c>0x20 x 0x20</c> : <c>0x0054f448</c> en bas,
    /// <c>0x0054f460</c> en haut, dont le sixieme octet passe de <c>0x02</c> a <c>0x01</c>.
    /// <b>Partagee par deux corps</b> — <see cref="EntityHandler_0040A670"/> et les deux amorces de la
    /// gerbe — exactement comme dans le binaire, ou c'est la meme adresse.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F450 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f450  <- fumee, gerbe
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f458  <- gerbe, role 3
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f4d0</c> — <b>l'eclat et la bulle des sous-zones 3, 4 et 9</b>,
    /// quatre enregistrements, jumeaux des quatre derniers de
    /// <see cref="s_Descriptors_0054F180"/>.
    ///
    /// <para>
    /// <b>Le miroir ne commence qu'ici.</b> Les deux enregistrements qui precedent,
    /// <c>0x0054f4c0</c> et <c>0x0054f4c8</c>, ne sont PAS la tuile et le separateur de la jumelle :
    /// ce sont des tuiles <c>0x18 x 0x18</c> d'une autre suite. La plage s'ouvre donc sur la
    /// premiere image de l'etincelle et se ferme sur la bulle, avant
    /// <see cref="s_Descriptors_0054F4F0"/>. <see cref="EntityHandler_0040CC60"/> la lit a <c>+0</c>
    /// (images 0 a 2), <see cref="EntityHandler_0040C820"/> a <c>+0x18</c>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F4D0 =
    [
        0x00, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f4d0
        0x10, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f4d8
        0x20, 0xd0, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f4e0
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f4e8  <- 0x0040C820
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f4f0</c> — <b>la seconde jumelle de
    /// <see cref="s_Descriptors_0054F1B0"/></b>, pour les sous-zones 3, 4 et 9. Onze
    /// enregistrements, bornee en haut par <c>0x0054f548</c> et sa tuile <c>0x1f x 0x1f</c>.
    /// <see cref="EntityHandler_0040CA80"/> la lit a <c>+8</c>.
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F4F0 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x0054f4f0
        0x08, 0x08, 0x10, 0x10, 0x01, 0x03, 0x10, 0x00,   // 0x0054f4f8  <- 0x0040CA80
        0x08, 0x08, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x0054f500
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f508
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f510
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f518
        0x10, 0x10, 0x08, 0x07, 0x01, 0x03, 0x10, 0x00,   // 0x0054f520
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f528
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f530
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f538
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x0054f540
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f620</c> — <b>les deux planches de role 2 des amorces de la gerbe</b>,
    /// sous-zones 3, 4 et 9. Meme forme que <see cref="s_Descriptors_0054F910"/> en foret : deux
    /// tuiles <c>0x0f x 0x0f</c> sur la page <c>0x09</c>, que
    /// <see cref="EntityHandler_0040B1B0"/> pose a <c>+0</c> et <see cref="EntityHandler_0040B900"/>
    /// a <c>+8</c>. Bornee en bas par la tuile <c>0x3f x 0x5f</c> de <c>0x0054f618</c>, en haut par
    /// la paire tuile/separateur de <c>0x0054f630</c>.
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F620 =
    [
        0x00, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f620  <- 0x0040B1B0
        0x10, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x0054f628  <- 0x0040B900
    ];

    private static byte* s_Descriptors0054F2D8Storage;
    private static byte* s_Descriptors0054F308Storage;
    private static byte* s_Descriptors0054F450Storage;
    private static byte* s_Descriptors0054F4D0Storage;
    private static byte* s_Descriptors0054F4F0Storage;
    private static byte* s_Descriptors0054F620Storage;

    private static byte* Descriptors0054F2D8()
        => NativeCopy(s_Descriptors_0054F2D8, ref s_Descriptors0054F2D8Storage);

    private static byte* Descriptors0054F308()
        => NativeCopy(s_Descriptors_0054F308, ref s_Descriptors0054F308Storage);

    private static byte* Descriptors0054F450()
        => NativeCopy(s_Descriptors_0054F450, ref s_Descriptors0054F450Storage);

    private static byte* Descriptors0054F4D0()
        => NativeCopy(s_Descriptors_0054F4D0, ref s_Descriptors0054F4D0Storage);

    private static byte* Descriptors0054F4F0()
        => NativeCopy(s_Descriptors_0054F4F0, ref s_Descriptors0054F4F0Storage);

    private static byte* Descriptors0054F620()
        => NativeCopy(s_Descriptors_0054F620, ref s_Descriptors0054F620Storage);

    /// <summary>
    /// Ghidra: <c>0x00408040</c> — <b>l'amorce de la fumee violette</b>, sous-type <b>57</b> du bloc
    /// <c>0x00554528</c>, sous-zones 5 a 8. Absente de <c>Blaze.c</c> ; cinq instructions qui
    /// empilent l'enregistrement et appellent <c>FUN_00408050</c>, seize octets plus loin —
    /// <c>Legacy/Raw/Blaze/FUN_00408040.asm</c>.
    ///
    /// <para>
    /// <c>FUN_00408050</c> est <b>le corps de <see cref="TickPurpleSmoke"/></b> a une adresse de
    /// planche pres (trois occurrences, toutes <c>0x0054f2d8</c>). Meme creneau 57 que
    /// <see cref="EntityHandler_00406120"/> dans le bloc des sous-zones 0 a 2 : c'est la meme
    /// particule, une fois par groupe de sous-zones.
    /// </para>
    /// </summary>
    private static void EntityHandler_00408040(byte* e)
        => TickPurpleSmoke(e, Descriptors0054F2D8());   // Ghidra: FUN_00408050, &DAT_0054f2d8

    /// <summary>
    /// Ghidra: <c>0x0040a670</c> — <b>la meme amorce pour les sous-zones 3, 4 et 9</b>, sous-type
    /// <b>51</b> du bloc <c>0x00554ba8</c>. Absente de <c>Blaze.c</c> ;
    /// <c>Legacy/Raw/Blaze/FUN_0040A670.asm</c>. Elle appelle <c>FUN_0040a680</c>, lui aussi
    /// <see cref="TickPurpleSmoke"/> a la planche pres : <c>0x0054f450</c>.
    /// </summary>
    private static void EntityHandler_0040A670(byte* e)
        => TickPurpleSmoke(e, Descriptors0054F450());   // Ghidra: FUN_0040a680, &DAT_0054f450

    /// <summary>
    /// Ghidra: <c>0x0040b1b0</c> — <b>la premiere amorce de la gerbe aux cinq phases</b>, sous-zones
    /// 3, 4 et 9 : sous-type <b>0</b> du bloc <c>0x00554ba8</c>, donc <c>0xc0</c>. Absente de
    /// <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_0040B1B0.asm</c>.
    ///
    /// <para>
    /// <b>La forme exacte de <see cref="EntityHandler_00411860"/></b>, avec trois constantes
    /// differentes : <c>+0x98 = 0</c> (les phases 2 et 3 feront naitre <c>0 + 0xc0 = 0xc0</c>,
    /// c'est-a-dire elle-meme), <c>+0x96 = 9</c>, et la planche de role 2
    /// <c>0x0054f620</c>. Le corps appele, <c>FUN_0040b1f0</c>, est
    /// <see cref="TickFivePhaseBurst"/> sur la planche <c>0x0054f450</c> — quatre adresses de
    /// planche, rien d'autre.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040B1B0(byte* e)
    {
        *(short*)(e + 0x98) = 0;
        *(short*)(e + 0x96) = 9;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F620();       // Ghidra: &DAT_0054f620
        }

        TickFivePhaseBurst(e, Descriptors0054F450());          // Ghidra: FUN_0040b1f0
    }

    /// <summary>
    /// Ghidra: <c>0x0040b900</c> — <b>la seconde amorce</b>, sous-type <b>1</b> du meme bloc
    /// (<c>0xc1</c>). <c>+0x98 = 1</c>, donc elle aussi se reproduit elle-meme ; <c>+0x96 = 7</c> ;
    /// planche de role 2 <c>0x0054f628</c>, le second enregistrement de
    /// <see cref="s_Descriptors_0054F620"/>. Meme corps appele. Absente de <c>Blaze.c</c> ;
    /// <c>Legacy/Raw/Blaze/FUN_0040B900.asm</c>.
    /// </summary>
    private static void EntityHandler_0040B900(byte* e)
    {
        *(short*)(e + 0x98) = 1;
        *(short*)(e + 0x96) = 7;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors0054F620() + 8;   // Ghidra: &DAT_0054f628
        }

        TickFivePhaseBurst(e, Descriptors0054F450());          // Ghidra: FUN_0040b1f0
    }

    /// <summary>
    /// Ghidra: FUN_00409c10 @ 0x00409c10 — <b>la bulle des sous-zones 5 a 8</b>, sous-type
    /// <b>63</b> du bloc <c>0x00554528</c>. Decompilee dans <c>Blaze.c</c>, et identique a
    /// <see cref="EntityHandler_004079E0"/> comme a <c>FUN_0040fac0</c> a la planche pres :
    /// <c>0x0054f300</c>, le separateur de <see cref="s_Descriptors_0054F2D8"/>. Le corps est
    /// <see cref="TickRisingBubble"/>.
    /// </summary>
    private static void EntityHandler_00409C10(byte* e)
        => TickRisingBubble(e, Descriptors0054F2D8() + 0x28);   // Ghidra: &DAT_0054f300

    /// <summary>
    /// Ghidra: FUN_0040c820 @ 0x0040c820 — <b>la bulle des sous-zones 3, 4 et 9</b>, sous-type
    /// <b>63</b> du bloc <c>0x00554ba8</c>. Meme corps, planche <c>0x0054f4e8</c> — le dernier
    /// enregistrement de <see cref="s_Descriptors_0054F4D0"/>.
    /// </summary>
    private static void EntityHandler_0040C820(byte* e)
        => TickRisingBubble(e, Descriptors0054F4D0() + 0x18);   // Ghidra: &DAT_0054f4e8

    /// <summary>
    /// Ghidra: FUN_00409a30 @ 0x00409a30 — <b>l'eclat des sous-zones 5 a 8</b>, sous-type
    /// <b>50</b> du bloc <c>0x00554528</c>. Decompilee dans <c>Blaze.c</c> ; identique a
    /// <see cref="EntityHandler_00407800"/> et a <c>FUN_0040f470</c> a la planche pres :
    /// <c>0x0054f310</c>. Le corps est <see cref="TickBurstingSpark"/>.
    /// </summary>
    private static void EntityHandler_00409A30(byte* e)
        => TickBurstingSpark(e, Descriptors0054F308() + 8);   // Ghidra: &DAT_0054f310

    /// <summary>
    /// Ghidra: FUN_0040ca80 @ 0x0040ca80 — <b>l'eclat des sous-zones 3, 4 et 9</b>, sous-type
    /// <b>50</b> du bloc <c>0x00554ba8</c>. Meme corps, planche <c>0x0054f4f8</c>.
    /// </summary>
    private static void EntityHandler_0040CA80(byte* e)
        => TickBurstingSpark(e, Descriptors0054F4F0() + 8);   // Ghidra: &DAT_0054f4f8

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0054f388</c> — <b>la jumelle de <see cref="s_Descriptors_0054F230"/></b>
    /// pour les sous-zones 5 a 8 : les memes quatre tuiles <c>0x1f x 0x1f</c>, octet pour octet.
    /// Bornee en bas par <c>0x0054f380</c>, dont les attributs different (<c>01 03</c> au lieu de
    /// <c>00 01</c>), et en haut par <c>0x0054f3a8</c>, la jumelle de <c>0x0054f250</c> — la borne
    /// de sa jumelle. <see cref="EntityHandler_00408C60"/> tire son image entre 0 et 3 : les quatre
    /// sont atteignables.
    /// </summary>
    private static readonly byte[] s_Descriptors_0054F388 =
    [
        0x00, 0x40, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f388
        0x20, 0x40, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f390
        0x40, 0x40, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f398
        0x60, 0x40, 0x1f, 0x1f, 0x00, 0x01, 0x18, 0x00,   // 0x0054f3a0
    ];

    private static byte* s_Descriptors0054F388Storage;

    private static byte* Descriptors0054F388()
        => NativeCopy(s_Descriptors_0054F388, ref s_Descriptors0054F388Storage);

    /// <summary>
    /// Ghidra: FUN_00409d00 @ 0x00409d00 — <b>l'etincelle qui tremble, sous-zones 5 a 8</b>,
    /// sous-type <b>49</b> du bloc <c>0x00554528</c>. Decompilee dans <c>Blaze.c</c> ; identique a
    /// <see cref="EntityHandler_00407AD0"/> a la planche pres : <c>0x0054f2e8</c>, soit
    /// <see cref="s_Descriptors_0054F2D8"/> au decalage 2, comme la jumelle.
    /// </summary>
    private static void EntityHandler_00409D00(byte* e)
        => TickJitterSpark_00407AD0(e, Descriptors0054F2D8() + 0x10);   // Ghidra: &DAT_0054f2e8

    /// <summary>
    /// Ghidra: FUN_0040cc60 @ 0x0040cc60 — <b>la meme, sous-zones 3, 4 et 9</b>, sous-type
    /// <b>49</b> du bloc <c>0x00554ba8</c>. Planche <c>0x0054f4d0</c>, les trois premiers
    /// enregistrements de <see cref="s_Descriptors_0054F4D0"/>.
    /// </summary>
    private static void EntityHandler_0040CC60(byte* e)
        => TickJitterSpark_00407AD0(e, Descriptors0054F4D0());   // Ghidra: &DAT_0054f4d0

    /// <summary>
    /// Ghidra: FUN_00408c60 @ 0x00408c60 — <b>l'eclat qui retombe en scintillant, sous-zones 5 a
    /// 8</b>, sous-type <b>53</b> du bloc <c>0x00554528</c>. Decompilee dans <c>Blaze.c</c> ;
    /// identique a <see cref="EntityHandler_00406DF0"/> a la planche pres : <c>0x0054f388</c>.
    /// </summary>
    private static void EntityHandler_00408C60(byte* e)
        => TickFallingGlint_00406DF0(e, Descriptors0054F388());   // Ghidra: &DAT_0054f388
}
